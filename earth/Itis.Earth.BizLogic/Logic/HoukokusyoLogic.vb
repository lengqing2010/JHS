Imports System.Transactions
Imports System.Web.UI

''' <summary>
''' 報告書画面用のロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class HoukokusyoLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic

    '更新日時保持用
    Dim dateUpdDateTime As DateTime

    Dim cbLogic As New CommonBizLogic

#Region "データ更新"
    ''' <summary>
    ''' 地盤テーブル・邸別請求テーブルを更新します
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal jibanRec As JibanRecordHoukokusyo, _
                                  ByVal brRec As BukkenRirekiRecord, _
                                  ByVal brRecHantei As BukkenRirekiRecord) As Boolean

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
        Dim renkeiTeibetuList As New List(Of TeibetuSeikyuuRenkeiRecord)

        Try
            ' 地盤テーブルと邸別請求の同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                Dim jibanLogic As New JibanLogic

                ' ●排他チェック実施
                Dim haitaKekkaList As List(Of JibanHaitaRecord) = _
                            DataMappingHelper.Instance.getMapArray(Of JibanHaitaRecord)(GetType(JibanHaitaRecord), _
                            dataAccess.CheckHaita(sqlString, haitaList))

                If haitaKekkaList.Count > 0 Then
                    Dim haitaErrorData As JibanHaitaRecord = haitaKekkaList(0)

                    ' 排他チェックエラー
                    mLogic.CallHaitaErrorMessage(sender, haitaErrorData.UpdLoginUserId, haitaErrorData.UpdDatetime, "地盤テーブル")
                    Return False
                End If

                '与信チェック処理
                Dim jibanRecOld As New JibanRecord
                jibanRecOld = jibanLogic.GetJibanData(jibanRec.Kbn, jibanRec.HosyousyoNo)

                Dim YosinLogic As New YosinLogic
                Dim intYosinResult As Integer = YosinLogic.YosinCheck(2, jibanRecOld, jibanRec)
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

                ' ●更新履歴テーブルの登録
                ' ●更新履歴連携テーブルの更新
                ' 施主名
                If dataAccess.AddKoushinRireki(jibanRec.Kbn, _
                                            jibanRec.HosyousyoNo, _
                                            JibanDataAccess.enumItemName.SesyuMei, _
                                            EarthConst.RIREKI_SESYU_MEI, _
                                            jibanRec.SesyuMei, _
                                            jibanRec.UpdLoginUserId) = False Then
                    ' エラー発生時処理終了
                    Return False
                End If

                ' 物件住所
                If dataAccess.AddKoushinRireki(jibanRec.Kbn, _
                                            jibanRec.HosyousyoNo, _
                                            JibanDataAccess.enumItemName.Juusyo, _
                                            EarthConst.RIREKI_BUKKEN_JYUUSYO, _
                                            jibanRec.BukkenJyuusyo1 & jibanRec.BukkenJyuusyo2, _
                                            jibanRec.UpdLoginUserId) = False Then
                    ' エラー発生時処理終了
                    Return False
                End If

                ' 備考
                If dataAccess.AddKoushinRireki(jibanRec.Kbn, _
                                            jibanRec.HosyousyoNo, _
                                            JibanDataAccess.enumItemName.Bikou, _
                                            EarthConst.RIREKI_BIKOU, _
                                            jibanRec.Bikou, _
                                            jibanRec.UpdLoginUserId) = False Then
                    ' エラー発生時処理終了
                    Return False
                End If

                ' 備考2
                If dataAccess.AddKoushinRireki(jibanRec.Kbn, _
                                            jibanRec.HosyousyoNo, _
                                            JibanDataAccess.enumItemName.Bikou2, _
                                            EarthConst.RIREKI_BIKOU2, _
                                            jibanRec.Bikou2, _
                                            jibanRec.UpdLoginUserId) = False Then
                    ' エラー発生時処理終了
                    Return False
                End If

                ' ●地盤連携テーブルに登録する（地盤－更新）
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
                updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordHoukokusyo), jibanRec, list)
                ' 更新日時取得（システム日時）
                dateUpdDateTime = DateTime.Now

                ' 地盤テーブルの更新を行う（新規は採番時、削除は別機能なので地盤本体は更新のみ）
                If dataAccess.UpdateJibanData(updateString, list) = True Then

                    '**************************************************************************
                    ' 邸別請求データの追加・更新
                    '**************************************************************************
                    '邸別請求ロジック
                    Dim tLogic As New TeibetuSyuuseiLogic

                    '報告書
                    If tLogic.EditTeibetuRecord(sender, _
                                                jibanRec.TyousaHoukokusyoRecord, _
                                                EarthConst.SOUKO_CD_TYOUSA_HOUKOKUSYO, _
                                                1, _
                                                jibanRec, _
                                                renkeiTeibetuList, _
                                                GetType(TeibetuSeikyuuRecordTysHoukokusyo) _
                                                ) = False Then
                        Return False
                    End If

                    ' 邸別請求連携反映対象が存在する場合、反映を行う
                    For Each renkeiTeibetuRec As TeibetuSeikyuuRenkeiRecord In renkeiTeibetuList
                        ' 連携用テーブルに反映する（邸別請求）
                        If dataAccess.EditTeibetuRenkeiData(renkeiTeibetuRec) <> 1 Then
                            ' 登録に失敗したので処理中断
                            Return False
                        End If
                    Next

                End If

                ' ●進捗テーブル追加更新(ReportIF)
                ' 更新に成功した場合、進捗テーブルへの反映及び連携データの作成を行う
                ' 進捗テーブルの更新(系列：0,1の場合のみ)
                If jibanRec.Keiyu = 0 Or _
                   jibanRec.Keiyu = 1 Or _
                   jibanRec.Keiyu = 5 Then

                    ' 進捗データ生成系データアクセスクラス
                    Dim reportAccess As New ReportIfDataAccess

                    ' 連携調査会社設定マスタの存在チェック
                    If reportAccess.ChkRenkeiTyousaKaisya(jibanRec.TysKaisyaCd, _
                                                           jibanRec.TysKaisyaJigyousyoCd) = True Then
                        ' 存在する場合、連携テーブルへ反映し経由を5に変更
                        If jibanLogic.EditReportIfData(jibanRec) = False Then
                            ' エラー発生時処理終了
                            Return False
                        End If

                        ' 反映したので経由を5に変更
                        '（データ内容が同一の場合は更新されないが、扱い的には送信済）
                        jibanRec.Keiyu = 5
                    Else
                        ' 連携調査会社設定マスタの存在チェックでNGになった場合、経由を9に変更
                        jibanRec.Keiyu = 9
                    End If
                End If

                ' ●地盤テーブル更新（経由のみ：進捗Tへの更新が条件）
                If dataAccess.UpdateJibanKeiyu(jibanRec.Kbn, _
                                                               jibanRec.HosyousyoNo, _
                                                               jibanRec.UpdLoginUserId, _
                                                               jibanRec.Keiyu) = False Then
                    ' エラー発生時処理終了
                    Return False
                End If

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

                ' ●物件履歴テーブル追加(判定変更時のみ、物件履歴Tに新規追加する)
                If Not brRecHantei Is Nothing Then
                    Dim brLogic As New BukkenRirekiLogic

                    '新規追加用スクリプトおよび実行
                    If brLogic.InsertBukkenRireki(brRecHantei) = False Then
                        Return False
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

#Region "日付マスタ.報告書発送日"
    ''' <summary>
    ''' 日付マスタの報告書発送日を算出します
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strCmpDate">比較対象の日付</param>
    ''' <returns>報告書発送日</returns>
    ''' <remarks>日付マスタの報告書発送日より次回締め日を算出し返却します<br/>
    ''' <example>
    ''' 当日:2009/02/15 締め日:20 の場合    ⇒ 2009/02/20 <br/>
    ''' 当日:2009/02/25 締め日:20 の場合    ⇒ 2009/03/20 <br/>
    ''' 当日:2009/02/25 締め日:不正値の場合 ⇒ 2009/02/28 (当月末)<br/>
    ''' </example>
    ''' </remarks>
    Public Function GetHoukokusyoHassoudate( _
                                            ByVal strKbn As String _
                                            , ByVal strCmpDate As String _
                                            ) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHoukokusyoHassoudate", _
                                            strKbn, _
                                            strCmpDate)

        ' 戻り値
        Dim editDate As Date
        Dim strRet As String = String.Empty

        ' 報告書発送日を取得
        Dim dataAccess As New HidukeSaveDataAccess
        Dim datSstring As String = dataAccess.GetHoukokusyoHassouDate(strKbn)
        '取得できかった場合は処理を抜ける
        If datSstring = String.Empty Then
            Return strRet
        End If

        ' 当月初
        Dim tougetuDate As Date = Date.Parse(Date.Now.Year.ToString & "/" & _
                                   Date.Now.Month.ToString("00") & "/" & _
                                   "01")

        Try
            ' 日付型に変換する
            editDate = Date.Parse(datSstring)
        Catch ex As Exception
            ' 不正値の場合、当月末を締め日とする
            editDate = tougetuDate.AddMonths(1).AddDays(-1)
        End Try

        Dim dtCmp As Date

        If strCmpDate = String.Empty Then
            dtCmp = Today
        Else
            dtCmp = Date.Parse(strCmpDate)
        End If

        '※調査報告書受理日＞上記の日付マスタ.報告書発送日の場合は、日付マスタ.報告書発送日＋1ヶ月を編集する
        If dtCmp > editDate Then
            Try
                datSstring = editDate.AddMonths(1).Year.ToString & "/" & _
                                                          editDate.AddMonths(1).Month.ToString("00") & "/" & _
                                                          editDate.Day.ToString("00")

                ' 日付型に変換する
                editDate = Date.Parse(datSstring)
            Catch ex As Exception
                ' 不正値の場合、翌月末を締め日とする
                editDate = tougetuDate.AddMonths(2).AddDays(-1)
            End Try

        End If

        strRet = Format(editDate, EarthConst.FORMAT_DATE_TIME_9)

        Return strRet

    End Function
#End Region

#Region "工事判定結果FLG判断"

    ''' <summary>
    ''' 判定関連を取得し、工事判定結果FLGを返す
    ''' </summary>
    ''' <param name="strHantei1Cd">判定コード1</param>
    ''' <param name="strHantei2Cd">判定コード2</param>
    ''' <param name="strSetuzokusi">判定接続詞</param>
    ''' <returns>工事判定結果FLG(Integer:0 or 1)</returns>
    ''' <remarks></remarks>
    Public Function GetKojHanteiKekkaFlg(ByVal strHantei1Cd As String, ByVal strHantei2Cd As String, ByVal strSetuzokusi As String) As Integer
        Dim intRet As Integer = Integer.MinValue '戻り値

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKojHanteiKekkaFlg", _
                                                    strHantei1Cd, _
                                                    strHantei2Cd, _
                                                    strSetuzokusi)
        Dim ksLogic As New KisoSiyouLogic
        Dim intKojHanteiFlg1 As Integer = Integer.MinValue
        Dim intKojHanteiFlg2 As Integer = Integer.MinValue

        Dim rec1 As KisoSiyouRecord = Nothing '判定1
        Dim rec2 As KisoSiyouRecord = Nothing '判定2

        '判定1
        If strHantei1Cd <> String.Empty Then
            rec1 = ksLogic.GetKisoSiyouRec(CInt(strHantei1Cd))
            If Not rec1 Is Nothing Then
                intKojHanteiFlg1 = rec1.KojHanteiFlg
            End If
        End If
        '判定2
        If strHantei2Cd <> String.Empty Then
            rec2 = ksLogic.GetKisoSiyouRec(CInt(strHantei2Cd))
            If Not rec2 Is Nothing Then
                intKojHanteiFlg2 = rec2.KojHanteiFlg
            End If
        End If

        '「接続文字列コード=1か3かNull かつ (判定1の工事判定FLG=1か判定2の工事判定FLG=1)か接続文字列コード=2 かつ 判定1の工事判定FLG=1 かつ (判定2の工事判定FLG=1かNull※）のとき1、以外0」
        ' ※（判定2の工事判定FLG=1かNULL）：判定2=NULLも含む
        If ((strSetuzokusi = "1" Or strSetuzokusi = "3" Or strSetuzokusi = String.Empty) _
            And (intKojHanteiFlg1 = 1 Or intKojHanteiFlg2 = 1)) _
            Or (strSetuzokusi = "2" And intKojHanteiFlg1 = 1 And (intKojHanteiFlg2 = 1 Or intKojHanteiFlg2 = Integer.MinValue Or strHantei2Cd = String.Empty)) Then
            intRet = 1
        Else
            intRet = 0
        End If

        Return intRet
    End Function
#End Region
End Class
