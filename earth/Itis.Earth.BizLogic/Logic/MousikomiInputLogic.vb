Imports System.Transactions
Imports System.Web.UI

''' <summary>
''' 申込入力画面用のロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class MousikomiInputLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic

    Dim strlogic As New StringLogic

    '更新日時保持用
    Dim dateUpdDateTime As DateTime

    '価格設定場所
    Dim intKakakuSetteiBasyo As Integer = Integer.MinValue

    '共通ロジッククラス
    Private cbLogic As New CommonBizLogic

#Region "データ更新"


    ''' <summary>
    ''' 地盤テーブルの追加更新を行います。
    ''' ※区分+番号の自動採番および地盤新規追加、更新処理を行なう。
    ''' </summary>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <param name="listBr">物件履歴レコードのリスト</param>
    ''' <returns>True or False</returns>
    ''' <remarks></remarks>
    Public Function saveData( _
                                    ByVal sender As Object, _
                                    ByRef jibanRec As JibanRecordMousikomiInput, _
                                    ByRef intGenzaiSyoriKensuu As Integer, _
                                    ByRef listBr As List(Of BukkenRirekiRecord) _
                                  ) As Boolean

        Try
            ' 地盤テーブルと邸別請求の同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '地盤更新
                If Me.UpdateJibanData(sender, jibanRec, listBr) = False Then
                    Return False
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

        '処理件数(合計) += 処理件数(当処理)
        intGenzaiSyoriKensuu = jibanRec.SyoriKensuu

        Return True
    End Function

    ''' <summary>
    ''' 地盤データを更新します。関連する邸別請求データの更新も行われます
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="jibanRec">更新対象の地盤レコード</param>
    ''' <param name="listBr">登録対象の物件履歴レコードのリスト</param>
    ''' <returns>True:更新成功 False:エラーによる更新失敗</returns>
    ''' <remarks></remarks>
    Public Function UpdateJibanData(ByVal sender As Object, _
                                    ByRef jibanRec As JibanRecordMousikomiInput, _
                                    ByRef listBr As List(Of BukkenRirekiRecord) _
                                    ) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibanData", _
                                            sender, _
                                            jibanRec, _
                                            listBr)

        Dim jLogic As New JibanLogic

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

        ' 地盤データ用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' 新規登録、更新の判定（保証書NO採番時の新規登録を除く）
        Dim isNew As Boolean
        isNew = dataAccess.ChkJibanNew(jibanRec.Kbn, jibanRec.HosyousyoNo)

        ' 連携テーブルデータ登録用データの格納用
        Dim renkeiJibanRec As New JibanRenkeiRecord
        Dim renkeiTeibetuList As New List(Of TeibetuSeikyuuRenkeiRecord)

        ' ReportJHS連係対象フラグ(デフォルト：False)
        Dim flgEditReportIf As Boolean

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

        ' 区分、保証書NO
        Dim kbn As String = jibanRec.Kbn
        Dim hosyousyoNo As String = jibanRec.HosyousyoNo

        ' 特別対応データ取得用のロジッククラス
        Dim ttMLogic As New TokubetuTaiouMstLogic
        ' 特別対応データ格納用のリスト
        Dim listRec As New List(Of TokubetuTaiouRecordBase)

        ' 経由退避用
        Dim intInitKeiyu As Integer = jibanRec.Keiyu

        Dim intCnt As Integer  '処理カウンタ
        Dim intMax As Integer = 20 '処理最大数

        ' 更新日付取得
        Dim updDateTime As DateTime = DateTime.Now

        Try
            ' 地盤テーブルと邸別請求の同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '*************************
                ' 特別対応データのマスタ取得＆データ登録(ループの外で実行)
                '*************************
                '地盤Tへは登録済みなので、初回のみ全件に対してデフォルト登録する
                If jibanRec.SyoriKensuu = 0 Then
                    If jLogic.insertTokubetuTaiouLogic(sender, _
                                                  kbn, _
                                                  strTmpBangou, _
                                                  jibanRec.KameitenCd, _
                                                  jibanRec.SyouhinCd1, _
                                                  jibanRec.TysHouhou, _
                                                  jibanRec.UpdLoginUserId, _
                                                  updDateTime, _
                                                  jibanRec.RentouBukkenSuu) Then
                    Else
                        'デフォルト登録失敗
                        mLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "特別対応データの登録"), 1)
                        Return False
                    End If
                End If

                '************************************************
                ' 特別対応データの取得
                '************************************************
                Dim total_count As Integer = 0 '取得件数
                listRec = ttMLogic.GetTokubetuTaiouInfo(sender, _
                                                        kbn, _
                                                        strTmpBangou, _
                                                        jibanRec.KameitenCd, _
                                                        jibanRec.SyouhinCd1, _
                                                        jibanRec.TysHouhou, _
                                                        total_count)
                If total_count < 0 Then
                    Return False
                End If

                '************************************************
                ' 特別対応価格の算出処理
                '************************************************
                Dim intTmpKingakuAction As EarthEnum.emKingakuAction = EarthEnum.emKingakuAction.KINGAKU_NOT_ACTION

                '特別対応価格反映処理
                intTmpKingakuAction = cbLogic.pf_ChkTokubetuTaiouKkk(Me, listRec, jibanRec, False)
                If intTmpKingakuAction = EarthEnum.emKingakuAction.KINGAKU_ALERT Then
                    mLogic.AlertMessage(sender, Messages.MSG200W.Replace("@PARAM1", cbLogic.AccTokubetuTaiouKkkMsg), 0, "KkkException")
                End If

                '20件ずつ処理
                For intCnt = 1 To intMax

                    '*************************
                    ' 連棟処理対応
                    '*************************
                    '処理件数 >= 連棟物件数 の場合、全処理終了
                    If jibanRec.SyoriKensuu >= jibanRec.RentouBukkenSuu Then
                        jibanRec.SyoriKensuu = jibanRec.RentouBukkenSuu '処理件数 = 連棟物件数
                        Exit For
                    End If

                    '更新対象の番号を指定
                    jibanRec.HosyousyoNo = strTmpBangou '地盤テーブル

                    ' 地盤データより区分、保証書NOを取得（空レコード確認用）
                    kbn = jibanRec.Kbn
                    hosyousyoNo = jibanRec.HosyousyoNo

                    '●物件名寄コードの自動設定
                    If jibanRec.BukkenNayoseCdFlg Then
                        jibanRec.BukkenNayoseCd = kbn & hosyousyoNo '自物件NOをセット
                    End If

                    '*************************
                    ' 排他チェック処理
                    '*************************
                    ' 地盤レコードの同一項目を複製
                    RecordMappingHelper.Instance.CopyRecordData(jibanRec, jibanHaitaRec)

                    ' 排他チェック用SQL文自動生成
                    sqlString = upadteMake.MakeHaitaSQLInfo(GetType(JibanHaitaRecord), jibanHaitaRec, haitaList)

                    ' 排他チェック実施
                    Dim haitaKekkaList As List(Of JibanHaitaRecord) = _
                                DataMappingHelper.Instance.getMapArray(Of JibanHaitaRecord)(GetType(JibanHaitaRecord), _
                                dataAccess.CheckHaita(sqlString, haitaList))

                    If haitaKekkaList.Count > 0 Then
                        Dim haitaErrorData As JibanHaitaRecord = haitaKekkaList(0)

                        ' 排他チェックエラー
                        mLogic.CallHaitaErrorMessage(sender, haitaErrorData.UpdLoginUserId, haitaErrorData.UpdDatetime, "地盤テーブル")
                        Return False

                    End If

                    '*************************
                    ' 与信チェック処理
                    '*************************
                    Dim jibanRecOld As New JibanRecord
                    jibanRecOld = jLogic.GetJibanData(kbn, hosyousyoNo)

                    Dim YosinLogic As New YosinLogic
                    Dim intYosinResult As Integer = YosinLogic.YosinCheck(1, jibanRecOld, jibanRec)
                    Select Case intYosinResult
                        Case EarthConst.YOSIN_ERROR_YOSIN_OK
                            'エラーなし
                        Case EarthConst.YOSIN_ERROR_YOSIN_NG
                            '与信限度額超過
                            mLogic.AlertMessage(sender, Messages.MSG089E, 1)
                            Return False
                        Case Else
                            '6:与信管理情報取得エラー
                            '7:与信管理テーブル更新エラー
                            '8:メール送信処理エラー
                            '9:その他エラー
                            mLogic.AlertMessage(sender, String.Format(Messages.MSG090E, intYosinResult.ToString()), 1)
                            Return False
                    End Select

                    '*************************
                    ' 更新履歴テーブルの登録
                    '*************************
                    ' 施主名
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.SesyuMei, _
                                                   EarthConst.RIREKI_SESYU_MEI, _
                                                   jibanRec.SesyuMei, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' エラー発生時処理終了
                        Return False
                    End If

                    ' 物件住所
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.Juusyo, _
                                                   EarthConst.RIREKI_BUKKEN_JYUUSYO, _
                                                   jibanRec.BukkenJyuusyo1 & jibanRec.BukkenJyuusyo2, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' エラー発生時処理終了
                        Return False
                    End If

                    ' 加盟店コード
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.KameitenCd, _
                                                   EarthConst.RIREKI_BUKKEN_KAMEITEN_CD, _
                                                   jibanRec.KameitenCd, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' エラー発生時処理終了
                        Return False
                    End If

                    ' 調査会社
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.TyousaKaisya, _
                                                   EarthConst.RIREKI_TYOUSA_KAISYA, _
                                                   jibanRec.TysKaisyaCd & jibanRec.TysKaisyaJigyousyoCd, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' エラー発生時処理終了
                        Return False
                    End If

                    ' 備考
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.Bikou, _
                                                   EarthConst.RIREKI_BIKOU, _
                                                   jibanRec.Bikou, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' エラー発生時処理終了
                        Return False
                    End If

                    ' 連携用テーブルに登録する（地盤－更新）
                    renkeiJibanRec.Kbn = kbn
                    renkeiJibanRec.HosyousyoNo = hosyousyoNo
                    renkeiJibanRec.RenkeiSijiCd = 2
                    renkeiJibanRec.SousinJykyCd = 0
                    renkeiJibanRec.UpdLoginUserId = jibanRec.UpdLoginUserId
                    If dataAccess.EditJibanRenkeiData(renkeiJibanRec, True) <> 1 Then
                        ' 登録に失敗したので処理中断
                        Return False
                    End If

                    '*************************
                    ' R-JHS連携チェック
                    '*************************
                    ' ReportJHS連係対象チェックを行う(経由：0,1,5の場合のみ)
                    flgEditReportIf = False '初期化

                    '経由=0or1or5(先頭物件が反映対象外(経由=9)と判定された場合、以後以下の処理はスルー)
                    If jibanRec.Keiyu = 0 Or _
                        jibanRec.Keiyu = 1 Or _
                        jibanRec.Keiyu = 5 Then

                        'R-JHS連携チェック
                        If jLogic.ChkRJhsRenkei(jibanRec.TysKaisyaCd, jibanRec.TysKaisyaJigyousyoCd) Then
                            ' 対象の場合、フラグを立てる
                            flgEditReportIf = True
                            ' 反映対象なので経由を5に変更
                            jibanRec.Keiyu = 5
                        Else
                            ' 連携調査会社設定マスタの存在チェックでNGになった場合、経由を9に変更
                            jibanRec.Keiyu = 9
                        End If
                    End If

                    '連棟処理の場合
                    If jibanRec.RentouBukkenSuu > 1 Then
                        'R-JHS連携対象の場合
                        If flgEditReportIf Then
                            If intInitKeiyu = 0 AndAlso jibanRec.SyoriKensuu > 0 Then '画面.経由=0でかつ、2棟目以降の場合
                                '連棟時の連携で経由=0の場合、1棟目のみ連携
                                '1棟目：連携し、経由が5or9になる
                                '2棟目以降：連携せず、経由は0のまま
                                jibanRec.Keiyu = intInitKeiyu
                                flgEditReportIf = False
                            End If
                        End If
                    End If

                    '************************************************
                    ' 保証書発行状況、保証書発行状況設定日、保証商品有無の自動設定
                    '************************************************
                    '商品の自動設定後に行なう
                    cbLogic.ps_AutoSetHosyousyoHakJyjy(EarthEnum.emGamenInfo.MousikomiInput, jibanRec)

                    '物件履歴データの自動セット
                    Dim brRec As BukkenRirekiRecord = cbLogic.MakeBukkenRirekiRecHosyouUmu(jibanRec, cbLogic.GetDisplayString(jibanRecOld.HosyouSyouhinUmu), cbLogic.GetDisplayString(jibanRecOld.KeikakusyoSakuseiDate))

                    If Not brRec Is Nothing Then
                        '物件履歴レコードのチェック
                        Dim strErrMsg As String = String.Empty
                        If cbLogic.checkInputBukkenRireki(brRec, strErrMsg) = False Then
                            mLogic.AlertMessage(sender, strErrMsg, 0, "ErrBukkenRireki")
                            Exit Function
                        End If
                    End If

                    '*************************
                    ' 地盤テーブルの更新
                    '*************************                  
                    ' 更新用UPDATE文自動生成
                    updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordMousikomiInput), jibanRec, list)

                    ' 地盤テーブルの更新を行う（新規は採番時、削除は別機能なので地盤本体は更新のみ）
                    If dataAccess.UpdateJibanData(updateString, list) = True Then

                        '**************************************************************************
                        ' 邸別請求データの追加
                        '**************************************************************************
                        '邸別請求テーブルデータ操作用
                        Dim TeibetuSyuuseiLogic As New TeibetuSyuuseiLogic
                        Dim tempTeibetuRec As New TeibetuSeikyuuRecord

                        '***************************
                        ' 商品コード１の追加
                        '***************************
                        '商品１レコードをテンポラリにセット
                        tempTeibetuRec = jibanRec.Syouhin1Record

                        If tempTeibetuRec IsNot Nothing Then
                            '連棟物件数が2以上の場合、上記コピー後チェック対象の番号をカウントアップ
                            If jibanRec.RentouBukkenSuu > 1 Then
                                tempTeibetuRec.HosyousyoNo = strTmpBangou
                            End If
                        End If

                        '商品１データ処理
                        If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                 tempTeibetuRec, _
                                                                 EarthConst.SOUKO_CD_SYOUHIN_1, _
                                                                 1, _
                                                                 jibanRec, _
                                                                 renkeiTeibetuList, _
                                                                 GetType(TeibetuSeikyuuRecord) _
                                                                 ) = False Then
                            Return False
                        End If

                        '***************************
                        ' 商品コード２の追加
                        '***************************
                        Dim i As Integer
                        For i = 1 To EarthConst.SYOUHIN2_COUNT
                            If jibanRec.Syouhin2Records.ContainsKey(i) = True Then

                                '商品２レコードをテンポラリにセット
                                tempTeibetuRec = jibanRec.Syouhin2Records.Item(i)

                                '連棟物件数が2以上の場合、上記コピー後チェック対象の番号をカウントアップ
                                If jibanRec.RentouBukkenSuu > 1 Then
                                    tempTeibetuRec.HosyousyoNo = strTmpBangou
                                End If

                                ' 商品２の邸別請求データを更新します
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
                        Next

                        '***************************
                        ' 商品コード３の追加
                        '***************************
                        For i = 1 To EarthConst.SYOUHIN3_COUNT
                            If Not jibanRec.Syouhin3Records Is Nothing AndAlso jibanRec.Syouhin3Records.ContainsKey(i) = True Then

                                '商品３レコードをテンポラリにセット
                                tempTeibetuRec = jibanRec.Syouhin3Records.Item(i)

                                '連棟物件数が2以上の場合、上記コピー後チェック対象の番号をカウントアップ
                                If jibanRec.RentouBukkenSuu > 1 Then
                                    tempTeibetuRec.HosyousyoNo = strTmpBangou
                                End If

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
                        Next

                        ' 邸別請求連携反映対象が存在する場合、反映を行う
                        For Each renkeiTeibetuRec As TeibetuSeikyuuRenkeiRecord In renkeiTeibetuList
                            ' 連携用テーブルに反映する（邸別請求）
                            If dataAccess.EditTeibetuRenkeiData(renkeiTeibetuRec) <> 1 Then
                                ' 登録に失敗したので処理中断
                                Return False
                            End If
                        Next

                        ' ●物件履歴テーブル追加(保証有無変更時のみ、物件履歴Tに新規追加する)
                        If Not brRec Is Nothing Then
                            Dim brLogic As New BukkenRirekiLogic

                            '新規追加用スクリプトおよび実行
                            brRec.Kbn = jibanRec.Kbn
                            brRec.HosyousyoNo = jibanRec.HosyousyoNo
                            If brLogic.InsertBukkenRireki(brRec) = False Then
                                Return False
                            End If
                        End If

                        ' ●物件履歴テーブル追加(画面情報を元に、物件履歴Tに新規追加する)
                        If Not listBr Is Nothing AndAlso listBr.Count > 0 Then
                            Dim brLogic As New BukkenRirekiLogic
                            Dim brRecUI = New BukkenRirekiRecord
                            Dim intBrCnt As Integer = 0

                            For intBrCnt = 0 To listBr.Count - 1
                                brRecUI = listBr(intBrCnt)

                                '新規追加用スクリプトおよび実行
                                brRecUI.Kbn = jibanRec.Kbn
                                brRecUI.HosyousyoNo = jibanRec.HosyousyoNo
                                If brLogic.InsertBukkenRireki(brRecUI) = False Then
                                    mLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "物件履歴データの登録"), 1)
                                    Return False
                                End If
                            Next

                        End If

                        '***************************
                        ' 特別対応テーブルの更新
                        '***************************
                        If Not listRec Is Nothing AndAlso listRec.Count > 0 Then
                            '連棟用の保証書Noを連番でふりなおす
                            For intTokuCnt As Integer = 0 To listRec.Count - 1
                                listRec(intTokuCnt).HosyousyoNo = strTmpBangou
                                listRec(intTokuCnt).UpdFlg = 1
                            Next

                            '特別対応データ更新
                            If cbLogic.pf_setKey_TokubetuTaiou_TeibetuSeikyuu(sender, listRec, jibanRec.UpdLoginUserId, EarthEnum.emGamenInfo.MousikomiInput) = False Then
                                mLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "特別対応データの更新"), 1)
                                Return False
                            End If
                        End If

                        '先にチェックしておいたReportJHS連係対象チェックを元に、進捗データテーブル更新処理を行う
                        '※ReportJHS連携にて特別対応コードも連携対象のため、最後に処理する
                        If flgEditReportIf Then
                            ' 進捗データテーブル更新処理呼出
                            If jLogic.EditReportIfData(jibanRec) = False Then
                                ' エラー発生時処理終了
                                Return False
                            End If
                        End If

                        '番号をカウントアップ
                        intTmpBangou = CInt(strTmpBangou) + 1 'カウンタ
                        strTmpBangou = Format(intTmpBangou, "0000000000") 'フォーマット

                        jibanRec.SyoriKensuu += 1 '処理件数

                    Else
                        Return False
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

#End Region

#Region "調査会社未指定時の自動設定"

    ''' <summary>
    ''' 指定調査会社、優先調査会社の存在チェック
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strTysGaisyaCd">調査会社コード(調査会社コード＋事業所コード)</param>
    ''' <returns>True or False</returns>
    ''' <remarks>該当の調査会社コードが加盟店調査会社設定マスタ.に存在するかを判定する</remarks>
    Public Function ChkExistSiteiTysGaisya( _
                                     ByVal strKameitenCd As String _
                                     , ByRef strTysGaisyaCd As String _
                                     ) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkExistSiteiTysGaisya", _
                                            strKameitenCd _
                                            , strTysGaisyaCd _
                                            )

        Dim blnResult As Boolean = False
        Dim dataAccess As New TyousakaisyaSearchDataAccess

        '調査会社設定Mより指定調査会社の存在チェックを行なう
        blnResult = dataAccess.GetSiteiTyousakaisyaCd(strKameitenCd, strTysGaisyaCd)

        Return blnResult
    End Function

#End Region

End Class
