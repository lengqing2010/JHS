Imports System.Transactions
Imports System.Web.UI

''' <summary>
''' 邸別入金データ修正ロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class TeibetuNyuukinLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "SQL作成種別"
    ''' <summary>
    ''' SQL作成種別
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum SqlType
        ''' <summary>
        ''' 登録SQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_INSERT = 1
        ''' <summary>
        ''' 更新SQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_UPDATE = 2
        ''' <summary>
        ''' 削除SQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_DELETE = 3
    End Enum
#End Region

#Region "データ取得"
    ''' <summary>
    ''' 地盤と邸別請求に紐づく邸別入金データを取得（地盤にあって邸別請求にない邸別入金データも取得）
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <returns>邸別入金レコードクラスのリスト</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuNyuukinData(ByVal strKbn As String _
                                                , ByVal strHosyousyoNo As String) As List(Of TeibetuNyuukinRecord)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuNyuukinData", _
                                                   strKbn, _
                                                   strHosyousyoNo)
        Dim listNyuukinRec As List(Of TeibetuNyuukinRecord)
        Dim jDtAcc As New JibanDataAccess

        listNyuukinRec = DataMappingHelper.Instance.getMapArray(Of TeibetuNyuukinRecord)(GetType(TeibetuNyuukinRecord) _
                                                            , jDtAcc.GetTeibetuSeikyuuNyuukinData(strKbn, strHosyousyoNo))
        Return listNyuukinRec
    End Function
#End Region

#Region "データ更新"
    ''' <summary>
    ''' 地盤テーブル・邸別入金テーブルを更新します
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal jibanRec As JibanRecordTeibetuNyuukin) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SaveJibanData", _
                                                    sender, _
                                                    jibanRec)
        ' Updateに必要なSQL情報を自動生成するクラス
        Dim upadteMake As New UpdateStringHelper
        ' 排他制御用SQL文
        Dim sqlString As String = ""
        ' Update文
        Dim updateString As String = ""
        ' 排他チェック用パラメータの情報を格納するList(Of ParamRecord)
        Dim haitaList As New List(Of ParamRecord)
        ' 更新用パラメータの情報を格納するList(Of ParamRecord)
        Dim list As New List(Of ParamRecord)

        ' 排他チェック用レコード作成
        Dim jibanHaitaRec As New JibanHaitaRecord
        ' 地盤レコードの同一項目を複製
        RecordMappingHelper.Instance.CopyRecordData(jibanRec, jibanHaitaRec)

        ' 排他チェック用SQL文自動生成
        sqlString = upadteMake.MakeHaitaSQLInfo(GetType(JibanHaitaRecord), jibanHaitaRec, haitaList)

        ' 地盤データ用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' 連携テーブルデータ登録用データの格納用
        Dim renkeiJibanRec As New JibanRenkeiRecord
        Dim renkeiTeibetuList As New List(Of TeibetuNyuukinRenkeiRecord)

        'UtilitiesのMessegeLogicクラス
        Dim mLogic As New MessageLogic

        Try
            ' 地盤テーブルと邸別入金の同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' 排他チェック実施
                Dim haitaKekkaList As List(Of JibanHaitaRecord) = _
                            DataMappingHelper.Instance.getMapArray(Of JibanHaitaRecord)(GetType(JibanHaitaRecord), _
                            dataAccess.CheckHaita(sqlString, haitaList))

                If haitaKekkaList.Count > 0 Then
                    Dim haitaErrorData As JibanHaitaRecord = haitaKekkaList(0)

                    ' 排他チェックエラー
                    ' 排他チェックエラー
                    mLogic.CallHaitaErrorMessage(sender, _
                                                       haitaErrorData.UpdLoginUserId, _
                                                       haitaErrorData.UpdDatetime, _
                                                       "地盤テーブル")

                    Return False
                End If

                ' 地盤データ
                ' 連携用テーブルに登録する（地盤－更新）
                renkeiJibanRec.Kbn = jibanRec.Kbn
                renkeiJibanRec.HosyousyoNo = jibanRec.HosyousyoNo
                renkeiJibanRec.RenkeiSijiCd = 2
                renkeiJibanRec.SousinJykyCd = 0
                renkeiJibanRec.UpdLoginUserId = jibanRec.UpdLoginUserId
                If dataAccess.EditJibanRenkeiData(renkeiJibanRec, True) <> 1 Then
                    ' 登録失敗時、処理を中断する
                    Return False
                End If

                ' 更新用UPDATE文自動生成
                updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordTeibetuNyuukin), jibanRec, list)

                ' 地盤テーブルの更新を行う
                If dataAccess.UpdateJibanData(updateString, list) = False Then
                    Return False
                End If

                ' 邸別入金データ
                If Not jibanRec.TeibetuNyuukinLists Is Nothing Then

                    ' データ設定用のDictionaryです
                    Dim nyuukinRecords As List(Of TeibetuNyuukinUpdateRecord) = jibanRec.TeibetuNyuukinLists
                    For Each rec As TeibetuNyuukinUpdateRecord In nyuukinRecords
                        If EditTeibetuRecord(sender _
                                            , rec.TeibetuNyuukinrecord _
                                            , rec.BunruiCd _
                                            , rec.GamenHyoujiNo _
                                            , jibanRec _
                                            , renkeiTeibetuList) = False Then
                            Return False
                        End If
                    Next
                End If


                ' 邸別入金連携反映対象が存在する場合、反映を行う
                For Each renkeiTeibetuRec As TeibetuNyuukinRenkeiRecord In renkeiTeibetuList

                    Dim renkeiDataAccess As RenkeiKanriDataAccess = New RenkeiKanriDataAccess()

                    'UtilitiesのMessegeLogicクラス
                    Dim messageLogic As New MessageLogic

                    ' 連携データの存在チェック
                    If renkeiDataAccess.SelectTeibetuNyuukinRenkeiData(renkeiTeibetuRec).Rows.Count = 0 Then
                        If renkeiDataAccess.InsertTeibetuNyuukinRenkeiData(renkeiTeibetuRec) < 1 Then
                            ' 登録時エラー
                            messageLogic.DbErrorMessage(sender, _
                                                        "登録", _
                                                        "邸別入金連携", _
                                                        String.Format(EarthConst.TEIBETU_KEY2, _
                                                                     New String() {renkeiTeibetuRec.Kbn, _
                                                                                   renkeiTeibetuRec.HosyousyoNo, _
                                                                                   renkeiTeibetuRec.BunruiCd}))
                            Return False
                        End If
                    Else
                        ' 存在していたら更新
                        If renkeiTeibetuRec.RenkeiSijiCd <> 9 Then
                            '指示コードが削除以外の場合、更新に統一（新規データ対応）
                            renkeiTeibetuRec.RenkeiSijiCd = 2
                        End If
                        If renkeiDataAccess.UpdateTeibetuNyuukinRenkeiData(renkeiTeibetuRec, True) < 1 Then
                            ' 更新時エラー
                            messageLogic.DbErrorMessage(sender, _
                                                        "更新", _
                                                        "邸別入金連携", _
                                                        String.Format(EarthConst.TEIBETU_KEY2, _
                                                                     New String() {renkeiTeibetuRec.Kbn, _
                                                                                   renkeiTeibetuRec.HosyousyoNo, _
                                                                                   renkeiTeibetuRec.BunruiCd}))
                            Return False
                        End If
                    End If

                Next

                ' 更新に成功した場合、トランザクションをコミットする
                scope.Complete()

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    'エラーキャッチ：デッドロック
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            'エラーキャッチ：トランザクションエラー
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True
    End Function

    ''' <summary>
    ''' 邸別入金データの登録・更新・削除を実施します
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="teibetuRec">DB反映対象の邸別入金レコード</param>
    ''' <param name="bunruCd">分類コード</param>
    ''' <param name="jibanRec">邸別データ用地盤レコード</param>
    ''' <param name="renkeiRecList">邸別連携テーブルレコードのリスト（参照渡し）</param>
    ''' <returns>処理結果 true:成功 false:失敗</returns>
    ''' <remarks></remarks>
    Private Function EditTeibetuRecord(ByVal sender As Object, _
                                       ByVal teibetuRec As TeibetuNyuukinRecord, _
                                       ByVal bunruCd As String, _
                                       ByVal gamenHyoujiNo As Integer, _
                                       ByVal jibanRec As JibanRecordTeibetuNyuukin, _
                                       ByRef renkeiRecList As List(Of TeibetuNyuukinRenkeiRecord)) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuRecord", _
                                                    sender, _
                                                    teibetuRec, _
                                                    bunruCd, _
                                                    jibanRec, _
                                                    renkeiRecList)

        ' 地盤データ用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess
        Dim nyuukinDtAcc As New TeibetuNyuukinSyuuseiDataAccess

        ' 現在のレコードを取得
        Dim chkTeibetuList As List(Of TeibetuNyuukinRecord) = _
                DataMappingHelper.Instance.getMapArray(Of TeibetuNyuukinRecord)( _
                GetType(TeibetuNyuukinRecord), _
                dataAccess.GetTeibetuNyuukinDataKey(jibanRec.Kbn _
                                                    , jibanRec.HosyousyoNo _
                                                    , bunruCd _
                                                    , gamenHyoujiNo))

        ' 存在チェック用レコード保持
        Dim checkRec As New TeibetuNyuukinRecord

        ' 現在データを保持
        If chkTeibetuList.Count > 0 Then
            checkRec = chkTeibetuList(0)
        End If


        ' 邸別入金データの登録・更新・削除を実施します
        If teibetuRec Is Nothing Then

            ' 削除されたレコードか確認する
            If chkTeibetuList.Count > 0 Then

                ' 邸別入金 削除実施
                If EditTeibetuNyuukinData(checkRec, SqlType.SQL_DELETE) = False Then

                    'UtilitiesのMessegeLogicクラス
                    Dim messageLogic As New MessageLogic

                    ' 削除時エラー
                    messageLogic.DbErrorMessage(sender, _
                                                "削除", _
                                                "邸別入金", _
                                                String.Format(EarthConst.TEIBETU_KEY, _
                                                             New String() {checkRec.Kbn, _
                                                                           checkRec.HosyousyoNo, _
                                                                           checkRec.BunruiCd, _
                                                                           checkRec.GamenHyoujiNo}))

                    ' 削除に失敗したので処理中断
                    Return False
                End If

                ' 連携テーブルに登録（削除－邸別入金）
                Dim renkeiTeibetuRec As New TeibetuNyuukinRenkeiRecord
                renkeiTeibetuRec.Kbn = checkRec.Kbn
                renkeiTeibetuRec.HosyousyoNo = checkRec.HosyousyoNo
                renkeiTeibetuRec.BunruiCd = checkRec.BunruiCd
                renkeiTeibetuRec.GamenHyoujiNo = checkRec.GamenHyoujiNo
                renkeiTeibetuRec.RenkeiSijiCd = 9
                renkeiTeibetuRec.SousinJykyCd = 0
                renkeiTeibetuRec.UpdLoginUserId = jibanRec.UpdLoginUserId
                renkeiTeibetuRec.IsUpdate = True ' 地盤Sに有り

                ' 更新用リストに格納
                renkeiRecList.Add(renkeiTeibetuRec)

            End If

        Else
            If chkTeibetuList.Count > 0 Then

                ' 排他チェック
                If teibetuRec.UpdDatetime = checkRec.UpdDatetime Then
                    ' 更新
                    If nyuukinDtAcc.UpdTeibetuNyuukin(teibetuRec) <> 1 Then

                        'UtilitiesのMessegeLogicクラス
                        Dim messageLogic As New MessageLogic

                        ' 更新時エラー
                        messageLogic.DbErrorMessage(sender, _
                                                    "更新", _
                                                    "邸別入金", _
                                                    String.Format(EarthConst.TEIBETU_KEY, _
                                                                 New String() {teibetuRec.Kbn, _
                                                                               teibetuRec.HosyousyoNo, _
                                                                               teibetuRec.BunruiCd, _
                                                                               teibetuRec.GamenHyoujiNo}))
                        ' 更新失敗時、処理を中断する
                        Return False
                    End If

                    ' 連携用テーブルに登録する（更新－邸別入金）
                    Dim renkeiTeibetuRec As New TeibetuNyuukinRenkeiRecord
                    renkeiTeibetuRec.Kbn = teibetuRec.Kbn
                    renkeiTeibetuRec.HosyousyoNo = teibetuRec.HosyousyoNo
                    renkeiTeibetuRec.BunruiCd = teibetuRec.BunruiCd
                    renkeiTeibetuRec.GamenHyoujiNo = teibetuRec.GamenHyoujiNo
                    renkeiTeibetuRec.RenkeiSijiCd = 2
                    renkeiTeibetuRec.SousinJykyCd = 0
                    renkeiTeibetuRec.UpdLoginUserId = teibetuRec.UpdLoginUserId
                    renkeiTeibetuRec.IsUpdate = True ' 地盤Sに有り

                    ' 更新用リストに格納
                    renkeiRecList.Add(renkeiTeibetuRec)
                Else
                    ' 排他チェックエラー
                    'UtilitiesのMessegeLogicクラス
                    Dim messageLogic As New MessageLogic

                    ' 排他チェックエラー
                    messageLogic.CallHaitaErrorMessage(sender, _
                                                       checkRec.UpdLoginUserId, _
                                                       checkRec.UpdDatetime, _
                                                       String.Format(EarthConst.TEIBETU_KEY, _
                                                                     New String() {checkRec.Kbn, _
                                                                                   checkRec.HosyousyoNo, _
                                                                                   checkRec.BunruiCd, _
                                                                                   checkRec.GamenHyoujiNo}))
                    Return False
                End If
            Else
                ' 既存データが無いので登録
                If EditTeibetuNyuukinData(teibetuRec, SqlType.SQL_INSERT) = False Then

                    'UtilitiesのMessegeLogicクラス
                    Dim messageLogic As New MessageLogic

                    ' 登録時エラー
                    messageLogic.DbErrorMessage(sender, _
                                                "登録", _
                                                "邸別入金", _
                                                String.Format(EarthConst.TEIBETU_KEY, _
                                                             New String() {teibetuRec.Kbn, _
                                                                           teibetuRec.HosyousyoNo, _
                                                                           teibetuRec.BunruiCd, _
                                                                           teibetuRec.GamenHyoujiNo}))

                    ' 登録失敗時、処理を中断する
                    Return False
                End If

                ' 連携用テーブルに登録する（新規－邸別入金）
                Dim renkeiTeibetuRec As New TeibetuNyuukinRenkeiRecord
                renkeiTeibetuRec.Kbn = teibetuRec.Kbn
                renkeiTeibetuRec.HosyousyoNo = teibetuRec.HosyousyoNo
                renkeiTeibetuRec.BunruiCd = teibetuRec.BunruiCd
                renkeiTeibetuRec.GamenHyoujiNo = teibetuRec.GamenHyoujiNo
                renkeiTeibetuRec.RenkeiSijiCd = 1
                renkeiTeibetuRec.SousinJykyCd = 0
                renkeiTeibetuRec.UpdLoginUserId = jibanRec.UpdLoginUserId
                renkeiTeibetuRec.IsUpdate = False ' 全くの新規

                ' 更新用リストに格納
                renkeiRecList.Add(renkeiTeibetuRec)
            End If
        End If

        Return True
    End Function
#End Region

#Region "邸別入金データ編集"
    ''' <summary>
    ''' 邸別入金データの追加・更新・削除を実行します。
    ''' </summary>
    ''' <param name="teibetuNyuukinRec">邸別入金レコード</param>
    ''' <returns>True:登録成功 False:登録失敗</returns>
    ''' <remarks></remarks>
    Private Function EditTeibetuNyuukinData(ByVal teibetuNyuukinRec As TeibetuNyuukinRecord, _
                                            ByVal _sqlType As SqlType) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuNyuukinData", _
                                            teibetuNyuukinRec)

        ' SQL情報を自動生成するInterface
        Dim sqlMake As ISqlStringHelper = Nothing

        ' 処理によりインスタンスを生成する
        Select Case _sqlType
            Case SqlType.SQL_INSERT
                sqlMake = New InsertStringHelper

                ' Insert時は更新日付情報を登録に移動
                teibetuNyuukinRec.AddDatetime = Now
                teibetuNyuukinRec.AddLoginUserId = teibetuNyuukinRec.UpdLoginUserId
                teibetuNyuukinRec.UpdDatetime = DateTime.MinValue
                teibetuNyuukinRec.UpdLoginUserId = ""

            Case SqlType.SQL_UPDATE
                sqlMake = New UpdateStringHelper
            Case SqlType.SQL_DELETE
                sqlMake = New DeleteStringHelper
        End Select

        Dim sqlString As String = ""
        ' パラメータの情報を格納する List(Of ParamRecord)
        Dim list As New List(Of ParamRecord)

        sqlString = sqlMake.MakeUpdateInfo(GetType(TeibetuNyuukinRecord), teibetuNyuukinRec, list)

        ' 地盤データ取得用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' DB反映処理
        If dataAccess.UpdateJibanData(sqlString, list) = False Then
            Return False
        End If

        Return True

    End Function
#End Region

    ''' <summary>
    ''' 残額を再計算します
    ''' </summary>
    ''' <param name="strSeikyuuGaku"></param>
    ''' <param name="strNyuukinGaku"></param>
    ''' <param name="strHenkinGaku"></param>
    ''' <returns>残額(文字列)</returns>
    ''' <remarks></remarks>
    Public Function CalcZanGaku(ByVal strSeikyuuGaku As String, _
                                 ByVal strNyuukinGaku As String, _
                                 ByVal strHenkinGaku As String) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CalcZangaku", _
                                                    strSeikyuuGaku, _
                                                    strNyuukinGaku, _
                                                    strHenkinGaku)
        Dim retZanGaku As String = ""

        Dim intSeikyuuGaku As Integer
        Dim intNyuukinGaku As Integer
        Dim intHenkinGaku As Integer

        strSeikyuuGaku = strSeikyuuGaku.Replace(",", "")
        strNyuukinGaku = strNyuukinGaku.Replace(",", "")
        strHenkinGaku = strHenkinGaku.Replace(",", "")

        If strSeikyuuGaku = "" Then
            intSeikyuuGaku = 0
        Else
            intSeikyuuGaku = Integer.Parse(strSeikyuuGaku)
        End If

        If strNyuukinGaku = "" Then
            intNyuukinGaku = 0
        Else
            intNyuukinGaku = Integer.Parse(strNyuukinGaku)
        End If

        If strHenkinGaku = "" Then
            intHenkinGaku = 0
        Else
            intHenkinGaku = Integer.Parse(strHenkinGaku)
        End If

        Dim intZanGaku As Integer = intSeikyuuGaku - (intNyuukinGaku - intHenkinGaku)

        retZanGaku = intZanGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        Return retZanGaku

    End Function
End Class
