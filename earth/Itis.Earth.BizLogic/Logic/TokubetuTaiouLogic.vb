Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' 特別対応データLogicクラス
''' </summary>
''' <remarks></remarks>
Public Class TokubetuTaiouLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic
    '更新日時保持用
    Dim pUpdDateTime As DateTime
    '特別対応マスタロジック
    Dim mttLogic As New TokubetuTaiouMstLogic
    '特別対応データアクセスクラス
    Dim clsDataAcc As New TokubetuTaiouDataAccess
    '加盟店検索ロジック
    Dim ksLogic As New KameitenSearchLogic
    Dim cbLogic As New CommonBizLogic
    Dim jLogic As New JibanLogic

#Region "特別対応データの取得"
    ''' <summary>
    ''' 該当テーブルの情報をリストで取得します
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="strTokubetuTaiouCds">特別対応コード群(未指定の場合、物件毎の全特別対応データを取得)</param>
    ''' <param name="allCount">全件数</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DB更新用レコードを格納したリスト ／ 0件の場合はNothing</returns>
    ''' <remarks>該当テーブルのリストを取得</remarks>
    Public Function GetTokubetuTaiouDataInfo(ByVal sender As Object, _
                                            ByVal strKbn As String, _
                                            ByVal strHosyousyoNo As String, _
                                            ByVal strTokubetuTaiouCds As String, _
                                            ByRef allCount As Integer, _
                                            Optional ByVal blnTorikesi As Boolean = False _
                                            ) As List(Of TokubetuTaiouRecordBase)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouDataInfo", _
                                                    sender, _
                                                    strKbn, _
                                                    strHosyousyoNo, _
                                                    strTokubetuTaiouCds, _
                                                    allCount, _
                                                    blnTorikesi _
                                                    )
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '取得データ格納用リスト
        Dim list As New List(Of TokubetuTaiouRecordBase)

        Try
            If strKbn = String.Empty OrElse strHosyousyoNo = String.Empty Then
                Return Nothing
            End If

            '検索処理の実行
            dTblResult = clsDataAcc.GetTokubetuTaiouList(strKbn, strHosyousyoNo, strTokubetuTaiouCds, blnTorikesi)

            '総件数をセット
            allCount = dTblResult.Rows.Count

            If allCount = 0 Then
                Return Nothing
            Else
                ' 検索結果を格納用リストにセット
                list = DataMappingHelper.Instance.getMapArray(Of TokubetuTaiouRecordBase)(GetType(TokubetuTaiouRecordBase), dTblResult)
            End If

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' 総件数カウントに-1をセット
            allCount = -1
        End Try

        Return list
    End Function

    ''' <summary>
    ''' 該当テーブルの情報をPKで取得します
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="intTokubetuTaiouCd">特別対応コード</param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>DB更新用レコードを格納したリスト ／ 0件の場合はNothing</returns>
    ''' <remarks>PKで該当テーブルの1レコードを取得</remarks>
    Public Function GetTokubetuTaiouDataRec(ByVal sender As Object, _
                                            ByVal strKbn As String, _
                                            ByVal strHosyousyoNo As String, _
                                            ByVal intTokubetuTaiouCd As Integer, _
                                            ByRef allCount As Integer _
                                            ) As TokubetuTaiouRecordBase
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouDataRec", _
                                                    sender, _
                                                    strKbn, _
                                                    strHosyousyoNo, _
                                                    intTokubetuTaiouCd _
                                                    )
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用レコードクラス
        Dim recResult As New TokubetuTaiouRecord

        Try
            If strKbn = String.Empty OrElse strHosyousyoNo = String.Empty OrElse intTokubetuTaiouCd = Integer.MinValue Then
                Return Nothing
            End If

            '検索処理の実行
            dTblResult = clsDataAcc.GetTokubetuTaiouRec(strKbn, strHosyousyoNo, intTokubetuTaiouCd)

            '総件数をセット
            allCount = dTblResult.Rows.Count

            If allCount = 0 Then
                Return Nothing
            Else
                ' 検索結果を格納用レコードクラスにセット
                recResult = DataMappingHelper.Instance.getMapArray(Of TokubetuTaiouRecord)(GetType(TokubetuTaiouRecord), dTblResult)(0)
            End If

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' 総件数カウントに-1をセット
            allCount = -1
        End Try

        Return recResult
    End Function

#End Region

#Region "加盟店商品調査方法特別対応マスタの取得"
    ''' <summary>
    ''' 加盟店商品調査方法特別対応マスタより対象となる特別対応レコードのみ取得します(新規受注用)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strSyouhinCd">商品1コード</param>
    ''' <param name="strTysHouhouNo">調査方法NO</param>
    ''' <param name="allCount">全件数</param>
    ''' <returns>加盟店商品調査方法特別対応マスタレコードのList(Of KameiTokubetuTaiouRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetDefaultTokubetuTaiouInfo(ByVal sender As Object, _
                                              ByVal strKameitenCd As String, _
                                              ByVal strSyouhinCd As String, _
                                              ByVal strTysHouhouNo As String, _
                                              ByRef allCount As Integer _
                                              ) As List(Of KameiTokubetuTaiouRecord)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDefaultTokubetuTaiouInfo", _
                                                    sender, _
                                                    strKameitenCd, _
                                                    strSyouhinCd, _
                                                    strTysHouhouNo, _
                                                    allCount)
        '検索結果格納用データテーブル
        Dim dTblResult As New DataTable
        '検索結果格納用リスト
        Dim list As New List(Of KameiTokubetuTaiouRecord)
        '加盟店検索結果格納用レコード
        Dim recKameiten As New KameitenSearchRecord

        Try
            dTblResult = clsDataAcc.GetDefalutKameiTokubetuTaiouInfoOnly(strKameitenCd, strSyouhinCd, strTysHouhouNo)

            If dTblResult.Rows.Count > 0 Then
                '総件数をセット
                allCount = dTblResult.Rows.Count
                '検索結果を格納用リストにセット
                list = DataMappingHelper.Instance.getMapArray(Of KameiTokubetuTaiouRecord)(GetType(KameiTokubetuTaiouRecord), dTblResult)

                Return list
            End If

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' 総件数カウントに-1をセット
            allCount = -1
        End Try

        Return Nothing
    End Function

#End Region

#Region "特別対応データ更新"
    ''' <summary>
    ''' データ更新処理
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="listRec">特別対応データのリスト</param>
    ''' <returns>処理成否(Boolean)</returns>
    ''' <remarks>データ更新判断用列挙体の値で、処理を分岐する</remarks>
    Public Function saveData(ByVal sender As Object, _
                             ByVal jibanRec As JibanRecordBase, _
                             ByRef listRec As List(Of TokubetuTaiouRecordBase), _
                             Optional ByVal blnTokubetuTaiouAuto As Boolean = True _
                             ) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".saveData", _
                                                    sender, _
                                                    listRec, _
                                                    blnTokubetuTaiouAuto _
                                                    )
        '排他用データアクセスクラス
        Dim clsJbDataAcc As New JibanDataAccess
        Dim recResult As New TokubetuTaiouRecordBase    'レコードクラス
        Dim recResultMst As New TokubetuTaiouRecordBase     'レコードクラス
        Dim recTmp As New TokubetuTaiouRecordBase       '作業用
        'SQL文自動生成インターフェイス
        Dim IFsqlMaker As ISqlStringHelper
        'SQL文
        Dim strSqlString As String = ""
        'パラメータレコードのリスト
        Dim listParam As New List(Of ParamRecord)
        Dim total_count As Integer = 0 '取得件数

        '*******************
        ' Updateに必要なSQL情報を自動生成するクラス
        Dim upadteMake As New UpdateStringHelper
        ' 排他制御用SQL文
        Dim sqlString As String = ""
        ' 排他チェック用パラメータの情報を格納するList(Of ParamRecord)
        Dim haitaList As New List(Of ParamRecord)
        ' 排他チェック用レコード作成
        Dim jibanHaitaRec As New JibanHaitaRecord
        ' 地盤レコードの同一項目を複製
        RecordMappingHelper.Instance.CopyRecordData(jibanRec, jibanHaitaRec)
        ' 排他チェック用SQL文自動生成
        sqlString = upadteMake.MakeHaitaSQLInfo(GetType(JibanHaitaRecord), jibanHaitaRec, haitaList)
        ' 地盤データ用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess
        ' 特別対応コードのチェック数カウンタ
        Dim TokubetuTaiouChkedCnt As Integer = 0
        '*******************

        Dim strKbn As String = jibanRec.Kbn
        Dim strBangou As String = jibanRec.HosyousyoNo
        Dim strUpdUserId As String = jibanRec.UpdLoginUserId

        ' 連棟物件数チェック
        If jibanRec.RentouBukkenSuu = Nothing OrElse jibanRec.RentouBukkenSuu <= 0 Then
            jibanRec.RentouBukkenSuu = 1
        End If
        ' 処理件数チェック
        If jibanRec.SyoriKensuu = Nothing OrElse jibanRec.SyoriKensuu <= 0 Then
            jibanRec.SyoriKensuu = 0
        End If

        ' 番号(保証書NO) 作業用
        Dim intTmpBangou As Integer = CInt(jibanRec.HosyousyoNo) + jibanRec.SyoriKensuu '番号+処理件数
        Dim strTmpBangou As String = Format(intTmpBangou, "0000000000") 'フォーマット

        Dim strRetBangou As String = jibanRec.HosyousyoNo '画面再描画用

        ' 経由退避用
        Dim intInitKeiyu As Integer = jibanRec.Keiyu

        Dim intCnt As Integer  '処理カウンタ
        Dim intMax As Integer = jibanRec.RentouBukkenSuu '処理最大数

        Try
            'トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '更新日時取得（システム日時）
                pUpdDateTime = DateTime.Now

                Dim htTt As New Dictionary(Of Integer, DateTime)

                If jibanRec.SyoriKensuu <= 0 Then
                    '特別対応レコードリストを退避
                    If Not listRec Is Nothing Then
                        For intTokuCnt As Integer = 0 To listRec.Count - 1
                            If listRec(intTokuCnt).UpdDatetime <> DateTime.MinValue Then
                                htTt.Add(listRec(intTokuCnt).TokubetuTaiouCd, listRec(intTokuCnt).UpdDatetime)
                            End If
                        Next
                    End If
                End If

                '20件ずつ処理
                For intCnt = 1 To intMax
                    '*************************
                    ' 連棟処理対応[START]
                    '*************************
                    '処理件数 >= 連棟物件数 の場合、全処理終了
                    If jibanRec.SyoriKensuu >= jibanRec.RentouBukkenSuu Then
                        jibanRec.SyoriKensuu = jibanRec.RentouBukkenSuu '処理件数 = 連棟物件数
                        Exit For
                    End If

                    TokubetuTaiouChkedCnt = 0 '初期化

                    '更新対象の番号を指定
                    jibanRec.HosyousyoNo = strTmpBangou '地盤テーブル

                    ' 地盤データより区分、保証書NOを取得（空レコード確認用）
                    strKbn = jibanRec.Kbn
                    strBangou = jibanRec.HosyousyoNo

                    '*******************
                    ' 排他チェック実施(地盤データ)
                    Dim haitaKekkaList As List(Of JibanHaitaRecord) = _
                                DataMappingHelper.Instance.getMapArray(Of JibanHaitaRecord)(GetType(JibanHaitaRecord), _
                                dataAccess.CheckHaita(sqlString, haitaList))

                    If haitaKekkaList.Count > 0 Then
                        Dim haitaErrorData As JibanHaitaRecord = haitaKekkaList(0)

                        ' 排他チェックエラー
                        mLogic.CallHaitaErrorMessage(sender, _
                                                           haitaErrorData.UpdLoginUserId, _
                                                           haitaErrorData.UpdDatetime, _
                                                           "地盤テーブル")
                        Return False
                    End If
                    '*******************

                    For Each recTmp In listRec
                        '連棟物件数が2以上の場合、上記コピー後チェック対象の番号をカウントアップ
                        If jibanRec.RentouBukkenSuu > 1 Then
                            With recTmp
                                .Kbn = strKbn
                                .HosyousyoNo = strBangou
                            End With
                        End If

                        '初期化
                        total_count = 0
                        recResult = New TokubetuTaiouRecordBase
                        recResultMst = New TokubetuTaiouRecordBase

                        '更新対象の特別対応データをレコードで取得
                        recResult = Me.GetTokubetuTaiouDataRec(sender, recTmp.Kbn, recTmp.HosyousyoNo, recTmp.TokubetuTaiouCd, total_count)
                        If total_count = -1 Then
                            ' 検索結果件数が-1の場合、エラーなので、処理終了
                            Exit Function
                        End If

                        '更新対象の特別対応マスタをレコードで取得(必ず取得できる)
                        recResultMst = mttLogic.GetTokubetuTaiouMstRec(sender, recTmp.TokubetuTaiouCd, total_count)
                        If total_count = -1 Then
                            ' 検索結果件数が-1の場合、エラーなので、処理終了
                            Exit Function
                        End If

                        '個数チェック
                        If recTmp.CheckJyky AndAlso (recTmp.TokubetuTaiouCd >= 0 And recTmp.TokubetuTaiouCd <= 9) Then
                            TokubetuTaiouChkedCnt = TokubetuTaiouChkedCnt + 1
                            If TokubetuTaiouChkedCnt >= 2 Then
                                mLogic.AlertMessage(sender, Messages.MSG192E, 1)
                                Return False
                            End If
                        End If

                        '特別対応データが存在する場合、UPDATE
                        If Not recResult Is Nothing _
                            AndAlso recResult.Kbn <> String.Empty Then

                            '●排他チェック
                            If htTt.ContainsKey(recResult.TokubetuTaiouCd) Then
                                If recResult.UpdDatetime <> htTt(recResult.TokubetuTaiouCd) Then
                                    ' 排他チェックエラー
                                    mLogic.CallHaitaErrorMessage(sender, recResult.UpdLoginUserId, recResult.UpdDatetime, "特別対応データテーブル")
                                    Return False
                                End If
                            End If

                            'チェック有の場合
                            If recTmp.CheckJyky Then
                                'DBのデータが有効の場合
                                If recResult.Torikesi = 0 Then
                                    '【減額/増額処理】
                                    '●変更状況(金額加算商品コード、工務店請求加算金額、実請求加算金額)
                                    If recTmp.KasanSyouhinCd <> recResult.KasanSyouhinCd _
                                        OrElse recTmp.KoumutenKasanGaku <> recResult.KoumutenKasanGaku _
                                            OrElse recTmp.UriKasanGaku <> recResult.UriKasanGaku Then

                                        'SQL文自動生成インターフェイスを使用し、データを更新
                                        IFsqlMaker = New UpdateStringHelper

                                        '取消(有効)
                                        recTmp.Torikesi = 0
                                        '更新フラグ(更新有)
                                        recTmp.UpdFlg = 1

                                        If recResult.KasanSyouhinCdOld = String.Empty Then
                                            '●Old値はNew値と同一(画面値よりセット)
                                            recTmp.KasanSyouhinCdOld = recTmp.KasanSyouhinCd
                                            recTmp.KoumutenKasanGakuOld = recTmp.KoumutenKasanGaku
                                            recTmp.UriKasanGakuOld = recTmp.UriKasanGaku
                                        Else
                                            '●Old値退避(DB値よりセット)
                                            recTmp.KasanSyouhinCdOld = recResult.KasanSyouhinCdOld
                                            recTmp.KoumutenKasanGakuOld = recResult.KoumutenKasanGakuOld
                                            recTmp.UriKasanGakuOld = recResult.UriKasanGakuOld
                                        End If
                                    Else
                                        '処理をとばす
                                        Continue For
                                    End If
                                Else
                                    '【増額処理】
                                    '特別対応テーブル.取消=1の場合のみ対象
                                    If Not recResult Is Nothing _
                                        AndAlso recResult.TokubetuTaiouCd <> Integer.MinValue _
                                            AndAlso recResult.Torikesi = 1 Then

                                        'SQL文自動生成インターフェイスを使用し、データを更新
                                        IFsqlMaker = New UpdateStringHelper

                                        '取消(有効)
                                        recTmp.Torikesi = 0
                                        '更新フラグ(更新有)
                                        recTmp.UpdFlg = 1

                                        '●Old値はNew値と同一(画面値よりセット)
                                        recTmp.KasanSyouhinCdOld = recTmp.KasanSyouhinCd
                                        recTmp.KoumutenKasanGakuOld = recTmp.KoumutenKasanGaku
                                        recTmp.UriKasanGakuOld = recTmp.UriKasanGaku

                                    Else
                                        '処理をとばす
                                        Continue For
                                    End If

                                End If

                            Else 'チェック無の場合

                                '【減額処理】
                                '特別対応テーブル.取消=0の場合のみ対象
                                If Not recResult Is Nothing _
                                    AndAlso recResult.TokubetuTaiouCd <> Integer.MinValue _
                                        AndAlso recResult.Torikesi = 0 Then

                                    'SQL文自動生成インターフェイスを使用し、データを更新
                                    IFsqlMaker = New UpdateStringHelper

                                    '取消(取消)
                                    recTmp.Torikesi = 1
                                    '更新フラグ(更新有)
                                    recTmp.UpdFlg = 1

                                    '●New値はDB値のまま(DB値よりセット)
                                    recTmp.KasanSyouhinCd = recResult.KasanSyouhinCd
                                    recTmp.KoumutenKasanGaku = recResult.KoumutenKasanGaku
                                    recTmp.UriKasanGaku = recResult.UriKasanGaku

                                    '●Old値はDB値のまま(DB値よりセット)
                                    recTmp.KasanSyouhinCdOld = recResult.KasanSyouhinCdOld
                                    recTmp.KoumutenKasanGakuOld = recResult.KoumutenKasanGakuOld
                                    recTmp.UriKasanGakuOld = recResult.UriKasanGakuOld

                                Else
                                    '処理をとばす
                                    Continue For
                                End If

                            End If

                            '更新ログインユーザーIDを設定
                            recTmp.UpdLoginUserId = recTmp.UpdLoginUserId
                            '更新日時を設定
                            recTmp.UpdDatetime = pUpdDateTime

                        Else 'INSERT

                            'チェック有の場合
                            If recTmp.CheckJyky Then
                                '【増額処理】
                                '特別対応マスタ.取消=0の場合のみ対象
                                If Not recResultMst Is Nothing _
                                    AndAlso recResultMst.mTokubetuTaiouCd <> Integer.MinValue _
                                        AndAlso recResultMst.mTorikesi = 0 Then

                                    'SQL文自動生成インターフェイスを使用し、データを登録
                                    IFsqlMaker = New InsertStringHelper

                                    '登録ログインユーザーIDを設定
                                    recTmp.AddLoginUserId = recTmp.UpdLoginUserId
                                    '登録日時を設定
                                    recTmp.AddDatetime = pUpdDateTime
                                    '取消(有効)
                                    recTmp.Torikesi = 0
                                    '更新フラグ(更新有)
                                    recTmp.UpdFlg = 1

                                    '●Old値はNew値と同一(画面値よりセット)
                                    recTmp.KasanSyouhinCdOld = recTmp.KasanSyouhinCd
                                    recTmp.KoumutenKasanGakuOld = recTmp.KoumutenKasanGaku
                                    recTmp.UriKasanGakuOld = recTmp.UriKasanGaku

                                Else
                                    '処理をとばす
                                    Continue For
                                End If

                            Else
                                '処理をとばす
                                Continue For
                            End If

                        End If

                        '価格処理フラグがたっている場合
                        If recTmp.KkkSyoriFlg = 1 Then
                            '登録/更新用文字列とパラメータの作成
                            strSqlString = IFsqlMaker.MakeUpdateInfo(GetType(TokubetuTaiouKkkSyoriRecord), recTmp, listParam, GetType(TokubetuTaiouRecordBase))
                        Else
                            '登録/更新用文字列とパラメータの作成
                            strSqlString = IFsqlMaker.MakeUpdateInfo(GetType(TokubetuTaiouRecord), recTmp, listParam, GetType(TokubetuTaiouRecordBase))
                        End If

                        'DB反映処理
                        If Not clsJbDataAcc.UpdateJibanData(strSqlString, listParam) Then
                            Return False
                            Exit Function
                        End If
                    Next

                    '*************************
                    ' 連棟処理対応[END]
                    '*************************
                    '番号をカウントアップ
                    intTmpBangou = CInt(strTmpBangou) + 1 'カウンタ
                    strTmpBangou = Format(intTmpBangou, "0000000000") 'フォーマット

                    jibanRec.SyoriKensuu += 1 '処理件数

                Next

                '**********************
                ' 特別対応価格反映処理
                '**********************
                '●特別対応価格対応(ロジックで処理を全て行なう場合：受注画面以外はTrue)
                If blnTokubetuTaiouAuto Then
                    ' 現在の地盤データをDBから再取得する
                    jibanRec = jLogic.GetJibanData(strKbn, strBangou)
                    jibanRec.UpdLoginUserId = strUpdUserId

                    ' 連携テーブルデータ登録用データの格納用
                    Dim renkeiTeibetuList As New List(Of TeibetuSeikyuuRenkeiRecord)

                    Dim listRes As New List(Of TokubetuTaiouRecordBase)
                    Dim intTotalCnt As Integer = 0

                    Dim intTmpKingakuAction As EarthEnum.emKingakuAction = EarthEnum.emKingakuAction.KINGAKU_NOT_ACTION

                    '特別対応マスタ
                    With jibanRec
                        If Not jibanRec Is Nothing AndAlso Not .Syouhin1Record Is Nothing Then
                            listRes = mttLogic.GetTokubetuTaiouInfo(sender, .Kbn, .HosyousyoNo, .KameitenCd, .Syouhin1Record.SyouhinCd, .TysHouhou, intTotalCnt)
                        End If
                    End With

                    '検索結果件数が-1の場合、エラーなので、処理終了
                    If intTotalCnt = -1 Then
                        Return False
                        Exit Function
                    End If

                    '最新の特別対応データを基に、邸別請求データへの反映を行なう
                    '******************************
                    ' 特別対応価格対応共通ロジック
                    '******************************
                    intTmpKingakuAction = cbLogic.pf_ChkTokubetuTaiouKkk(sender, listRes, jibanRec, False)
                    If intTmpKingakuAction = EarthEnum.emKingakuAction.KINGAKU_ALERT Then
                        mLogic.AlertMessage(sender, Messages.MSG200W.Replace("@PARAM1", cbLogic.AccTokubetuTaiouKkkMsg), 0, "KkkException")
                    End If

                    '**************************************************************************
                    ' 邸別請求データの追加・更新(上記までで訂正した邸別請求データを反映)
                    ' ※邸別請求データの削除はしない
                    '**************************************************************************
                    '邸別請求テーブルデータ操作用
                    Dim TeibetuSyuuseiLogic As New TeibetuSyuuseiLogic
                    Dim tempTeibetuRec As New TeibetuSeikyuuRecord

                    '***************************
                    ' 商品コード１
                    '***************************
                    '商品１レコードをテンポラリにセット
                    tempTeibetuRec = jibanRec.Syouhin1Record
                    tempTeibetuRec.Kbn = strKbn
                    tempTeibetuRec.HosyousyoNo = strBangou
                    tempTeibetuRec.UpdLoginUserId = strUpdUserId

                    If Not tempTeibetuRec Is Nothing Then
                        '該当の邸別請求レコードが存在し、かつ未計上の場合
                        With tempTeibetuRec
                            Select Case .KkkHenkouCheck
                                Case EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE '【更新】

                                    If Not .Kbn Is Nothing AndAlso .Kbn <> String.Empty _
                                        AndAlso Not .HosyousyoNo Is Nothing AndAlso .HosyousyoNo <> String.Empty _
                                            AndAlso Not .BunruiCd Is Nothing AndAlso .BunruiCd <> String.Empty _
                                                AndAlso .GamenHyoujiNo > 0 _
                                                    AndAlso .UriKeijyouFlg = 0 Then

                                        '商品１データ処理
                                        If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                                 tempTeibetuRec, _
                                                                                 EarthConst.SOUKO_CD_SYOUHIN_1, _
                                                                                 1, _
                                                                                 jibanRec, _
                                                                                 renkeiTeibetuList, _
                                                                                 GetType(TeibetuSeikyuuRecordTokubetuTaiou) _
                                                                                 ) = False Then
                                            Return False
                                        End If
                                    End If

                            End Select

                        End With
                    End If

                    '***************************
                    ' 商品コード２
                    '***************************
                    Dim i As Integer
                    For i = 1 To EarthConst.SYOUHIN2_COUNT
                        If Not jibanRec.Syouhin2Records Is Nothing AndAlso jibanRec.Syouhin2Records.ContainsKey(i) Then
                            tempTeibetuRec = Nothing

                            '商品２レコードをテンポラリにセット
                            If Not jibanRec.Syouhin2Records.Item(i) Is Nothing Then
                                tempTeibetuRec = jibanRec.Syouhin2Records.Item(i)
                                tempTeibetuRec.Kbn = strKbn
                                tempTeibetuRec.HosyousyoNo = strBangou
                                tempTeibetuRec.UpdLoginUserId = strUpdUserId
                            End If

                            If Not tempTeibetuRec Is Nothing Then
                                '該当の邸別請求レコードが存在し、かつ未計上の場合
                                With tempTeibetuRec
                                    Select Case .KkkHenkouCheck
                                        '【追加】【更新】
                                        Case EarthEnum.emTeibetuSeikyuuKkkJyky.INSERT, EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE

                                            If Not .Kbn Is Nothing AndAlso .Kbn <> String.Empty _
                                                AndAlso Not .HosyousyoNo Is Nothing AndAlso .HosyousyoNo <> String.Empty _
                                                    AndAlso Not .BunruiCd Is Nothing AndAlso .BunruiCd <> String.Empty _
                                                        AndAlso .GamenHyoujiNo > 0 _
                                                            AndAlso .UriKeijyouFlg = 0 Then

                                                ' 商品２の邸別請求データを更新します
                                                If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                                         tempTeibetuRec, _
                                                                                         tempTeibetuRec.BunruiCd, _
                                                                                         i, _
                                                                                         jibanRec, _
                                                                                         renkeiTeibetuList, _
                                                                                         GetType(TeibetuSeikyuuRecordTokubetuTaiou) _
                                                                                 ) = False Then
                                                    Return False
                                                End If

                                            End If

                                    End Select

                                End With
                            End If
                        End If
                    Next

                    '***************************
                    ' 商品コード３
                    '***************************
                    For i = 1 To EarthConst.SYOUHIN3_COUNT
                        If Not jibanRec.Syouhin3Records Is Nothing AndAlso jibanRec.Syouhin3Records.ContainsKey(i) Then
                            tempTeibetuRec = Nothing

                            '商品３レコードをテンポラリにセット
                            If Not jibanRec.Syouhin3Records.Item(i) Is Nothing Then
                                tempTeibetuRec = jibanRec.Syouhin3Records.Item(i)
                                tempTeibetuRec.Kbn = strKbn
                                tempTeibetuRec.HosyousyoNo = strBangou
                                tempTeibetuRec.UpdLoginUserId = strUpdUserId
                            End If

                            If Not tempTeibetuRec Is Nothing Then
                                '該当の邸別請求レコードが存在し、かつ未計上の場合
                                With tempTeibetuRec
                                    Select Case .KkkHenkouCheck
                                        '【追加】【更新】
                                        Case EarthEnum.emTeibetuSeikyuuKkkJyky.INSERT, EarthEnum.emTeibetuSeikyuuKkkJyky.UPDATE

                                            If Not .Kbn Is Nothing AndAlso .Kbn <> String.Empty _
                                                AndAlso Not .HosyousyoNo Is Nothing AndAlso .HosyousyoNo <> String.Empty _
                                                    AndAlso Not .BunruiCd Is Nothing AndAlso .BunruiCd <> String.Empty _
                                                        AndAlso .GamenHyoujiNo > 0 _
                                                            AndAlso .UriKeijyouFlg = 0 Then

                                                '商品コードが設定されている場合
                                                If .SyouhinCd <> String.Empty Then
                                                    ' 商品３の邸別請求データを更新します
                                                    If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                                             tempTeibetuRec, _
                                                                                             tempTeibetuRec.BunruiCd, _
                                                                                             i, _
                                                                                             jibanRec, _
                                                                                             renkeiTeibetuList, _
                                                                                             GetType(TeibetuSeikyuuRecord) _
                                                                                     ) = False Then
                                                        Return False
                                                    End If
                                                End If
                                            End If

                                        Case EarthEnum.emTeibetuSeikyuuKkkJyky.DELETE '【削除】

                                            If Not .Kbn Is Nothing AndAlso .Kbn <> String.Empty _
                                                AndAlso Not .HosyousyoNo Is Nothing AndAlso .HosyousyoNo <> String.Empty _
                                                    AndAlso Not .BunruiCd Is Nothing AndAlso .BunruiCd <> String.Empty _
                                                        AndAlso .GamenHyoujiNo > 0 _
                                                            AndAlso .UriKeijyouFlg = 0 _
                                                                AndAlso (.HattyuusyoGaku = 0 OrElse .HattyuusyoGaku = Integer.MinValue) Then

                                                '邸別請求レコードがあるのに、商品コードが未設定の場合、削除
                                                ' 削除処理用(削除確認は商品３の分類コード何れかで問題なし)
                                                If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                                         Nothing, _
                                                                                         EarthConst.SOUKO_CD_SYOUHIN_3, _
                                                                                         i, _
                                                                                         jibanRec, _
                                                                                         renkeiTeibetuList _
                                                                                 ) = False Then
                                                    Return False
                                                End If
                                            End If
                                    End Select
                                End With
                            End If
                        End If
                    Next

                    ' 邸別請求連携反映対象が存在する場合、反映を行う
                    For Each renkeiTeibetuRec As TeibetuSeikyuuRenkeiRecord In renkeiTeibetuList
                        ' 連携用テーブルに反映する（邸別請求）
                        If dataAccess.EditTeibetuRenkeiData(renkeiTeibetuRec) <> 1 Then
                            ' 登録に失敗したので処理中断
                            Return False
                        End If
                    Next

                    '**********************************************
                    ' 特別対応データの更新(邸別請求データとの紐付)
                    '**********************************************
                    If cbLogic.pf_setKey_TokubetuTaiou_TeibetuSeikyuu(sender, listRes, strUpdUserId, EarthEnum.emGamenInfo.TokubetuTaiou) = False Then
                        Return False
                        Exit Function
                    End If

                End If

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
#End Region

#Region "特別対応データ追加"
    ''' <summary>
    ''' 特別対応データ追加(加盟店商品調査方法特別対応マスタからの取得用)
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="listKtRec">加盟店商品調査方法特別対応マスタ該当レコードのリスト</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="strLoginUserId">ログインユーザーID</param>
    ''' <param name="dtDatetime">更新日時</param>
    ''' <returns>処理成否(Boolean)</returns>
    ''' <remarks>データ更新判断用列挙体の値で、処理を分岐する</remarks>
    Public Function InsertTokubetuTaiou(ByVal sender As Object, _
                                        ByRef listKtRec As List(Of KameiTokubetuTaiouRecord), _
                                        ByVal strKbn As String, _
                                        ByVal strHosyousyoNo As String, _
                                        ByVal strLoginUserId As String, _
                                        ByVal dtDatetime As DateTime _
                                        ) As Boolean
        '排他用データアクセスクラス
        Dim clsJbDataAcc As New JibanDataAccess
        Dim recTokubetuTaiou As New TokubetuTaiouRecord     'レコードクラス
        Dim recTmp As New KameiTokubetuTaiouRecord          '作業用
        Dim recResult As New TokubetuTaiouRecord            '存在チェック用
        Dim total_count As Integer = 0 '取得件数
        Dim intInsCnt As Integer = 0 '対象件数

        'SQL文自動生成インターフェイス
        Dim IFsqlMaker As ISqlStringHelper
        'SQL文
        Dim strSqlString As String = ""
        'パラメータレコードのリスト
        Dim listParam As New List(Of ParamRecord)

        '更新日時取得（システム日時）
        pUpdDateTime = dtDatetime

        Try
            If strKbn = "" OrElse strHosyousyoNo = "" Then
                Return False
                Exit Function
            End If

            For Each recTmp In listKtRec
                '初期化
                total_count = 0
                recTokubetuTaiou = New TokubetuTaiouRecord

                '項目を設定
                With recTokubetuTaiou
                    '区分
                    .Kbn = strKbn
                    '番号
                    .HosyousyoNo = strHosyousyoNo
                    '特別対応コード
                    .TokubetuTaiouCd = recTmp.TokubetuTaiouCd
                    '取消
                    .Torikesi = 0
                    '分類コード
                    .BunruiCd = String.Empty
                    '画面表示NO
                    .GamenHyoujiNo = Integer.MinValue
                    '金額加算商品コード
                    .KasanSyouhinCd = recTmp.KasanSyouhinCd
                    '実請求加算金額
                    .UriKasanGaku = recTmp.UriKasanGaku
                    '工務店請求加算金額
                    .KoumutenKasanGaku = recTmp.KoumutenKasanGaku
                    '金額加算商品コードOld
                    .KasanSyouhinCdOld = .KasanSyouhinCd
                    '実請求加算金額Old
                    .UriKasanGakuOld = .UriKasanGaku
                    '工務店請求加算金額Old
                    .KoumutenKasanGakuOld = .KoumutenKasanGaku
                    '更新フラグ
                    .UpdFlg = 1
                    '価格処理フラグ
                    .KkkSyoriFlg = Integer.MinValue
                    '登録ログインユーザーID
                    .AddLoginUserId = strLoginUserId
                    '登録日時
                    .AddDatetime = pUpdDateTime
                End With

                '存在チェック
                recResult = Me.GetTokubetuTaiouDataRec(sender, strKbn, strHosyousyoNo, recTokubetuTaiou.TokubetuTaiouCd, total_count)

                If total_count = -1 Then
                    ' 検索結果件数が-1の場合、エラーなので、処理終了
                    Return False
                    Exit Function

                ElseIf total_count > 0 Then
                    ' 検索結果件数が0より大きい場合、処理終了
                    Continue For

                ElseIf recResult Is Nothing AndAlso recTokubetuTaiou.TokubetuTaiouCd <> Integer.MinValue Then
                    'SQL文自動生成インターフェイスを使用し、データを登録
                    IFsqlMaker = New InsertStringHelper

                    '更新用文字列とパラメータの作成
                    strSqlString = IFsqlMaker.MakeUpdateInfo(GetType(TokubetuTaiouRecord), recTokubetuTaiou, listParam, GetType(TokubetuTaiouRecord))

                    'DB反映処理
                    If Not clsJbDataAcc.UpdateJibanData(strSqlString, listParam) Then
                        Return False
                        Exit Function
                    End If

                    '個数チェック
                    If recTokubetuTaiou.TokubetuTaiouCd >= 0 And recTokubetuTaiou.TokubetuTaiouCd <= 9 Then
                        intInsCnt += 1
                        If intInsCnt >= 2 Then
                            mLogic.AlertMessage(sender, Messages.MSG192E, 1)
                            Return False
                            Exit Function
                        End If
                    End If

                End If
            Next

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        End Try

        Return True
    End Function

    ''' <summary>
    ''' 特別対応データ追加(特別対応データからの取得用)
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="listKtRec">加盟店商品調査方法特別対応マスタ該当レコードのリスト</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="strLoginUserId">ログインユーザーID</param>
    ''' <param name="dtDatetime">更新日時</param>
    ''' <returns>処理成否(Boolean)</returns>
    ''' <remarks>データ更新判断用列挙体の値で、処理を分岐する</remarks>
    Public Function InsertTokubetuTaiou(ByVal sender As Object, _
                                        ByRef listKtRec As List(Of TokubetuTaiouRecordBase), _
                                        ByVal strKbn As String, _
                                        ByVal strHosyousyoNo As String, _
                                        ByVal strLoginUserId As String, _
                                        ByVal dtDatetime As DateTime _
                                        ) As Boolean
        '排他用データアクセスクラス
        Dim clsJbDataAcc As New JibanDataAccess
        Dim recTokubetuTaiou As New TokubetuTaiouRecord     'レコードクラス
        Dim recTmp As New TokubetuTaiouRecordBase          '作業用
        Dim recResult As New TokubetuTaiouRecord            '存在チェック用
        Dim total_count As Integer = 0 '取得件数
        Dim intInsCnt As Integer = 0 '対象件数

        'SQL文自動生成インターフェイス
        Dim IFsqlMaker As ISqlStringHelper
        'SQL文
        Dim strSqlString As String = ""
        'パラメータレコードのリスト
        Dim listParam As New List(Of ParamRecord)

        '更新日時取得（システム日時）
        pUpdDateTime = dtDatetime

        Try
            If strKbn = "" OrElse strHosyousyoNo = "" Then
                Return False
                Exit Function
            End If

            For Each recTmp In listKtRec
                '初期化
                total_count = 0
                recTokubetuTaiou = New TokubetuTaiouRecord

                '項目を設定
                With recTokubetuTaiou
                    '区分
                    .Kbn = strKbn
                    '番号
                    .HosyousyoNo = strHosyousyoNo
                    '特別対応コード
                    .TokubetuTaiouCd = recTmp.TokubetuTaiouCd
                    '取消
                    .Torikesi = recTmp.Torikesi
                    '分類コード
                    .BunruiCd = String.Empty
                    '画面表示NO
                    .GamenHyoujiNo = Integer.MinValue
                    '金額加算商品コード
                    .KasanSyouhinCd = recTmp.KasanSyouhinCd
                    '実請求加算金額
                    .UriKasanGaku = recTmp.UriKasanGaku
                    '工務店請求加算金額
                    .KoumutenKasanGaku = recTmp.KoumutenKasanGaku
                    '金額加算商品コードOld
                    .KasanSyouhinCdOld = .KasanSyouhinCd
                    '実請求加算金額Old
                    .UriKasanGakuOld = .UriKasanGaku
                    '工務店請求加算金額Old
                    .KoumutenKasanGakuOld = .KoumutenKasanGaku
                    '更新フラグ
                    .UpdFlg = 1
                    '価格処理フラグ
                    .KkkSyoriFlg = Integer.MinValue
                    '登録ログインユーザーID
                    .AddLoginUserId = strLoginUserId
                    '登録日時
                    .AddDatetime = pUpdDateTime
                End With

                '存在チェック
                recResult = Me.GetTokubetuTaiouDataRec(sender, strKbn, strHosyousyoNo, recTokubetuTaiou.TokubetuTaiouCd, total_count)

                If total_count = -1 Then
                    ' 検索結果件数が-1の場合、エラーなので、処理終了
                    Return False
                    Exit Function

                ElseIf total_count > 0 Then
                    ' 検索結果件数が0より大きい場合、処理終了
                    Continue For

                ElseIf recResult Is Nothing AndAlso recTokubetuTaiou.TokubetuTaiouCd <> Integer.MinValue Then
                    'SQL文自動生成インターフェイスを使用し、データを登録
                    IFsqlMaker = New InsertStringHelper

                    '更新用文字列とパラメータの作成
                    strSqlString = IFsqlMaker.MakeUpdateInfo(GetType(TokubetuTaiouRecord), recTokubetuTaiou, listParam, GetType(TokubetuTaiouRecord))

                    'DB反映処理
                    If Not clsJbDataAcc.UpdateJibanData(strSqlString, listParam) Then
                        Return False
                        Exit Function
                    End If

                    '個数チェック
                    If recTokubetuTaiou.TokubetuTaiouCd >= 0 And recTokubetuTaiou.TokubetuTaiouCd <= 9 Then
                        intInsCnt += 1
                        If intInsCnt >= 2 Then
                            mLogic.AlertMessage(sender, Messages.MSG192E, 1)
                            Return False
                            Exit Function
                        End If
                    End If

                End If
            Next

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  'エラーキャッチ：タイムアウト
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        End Try

        Return True
    End Function

#End Region

End Class
