Imports System.Transactions
Imports System.Web.UI

''' <summary>
''' 改良工事画面用のロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class IkkatuHenkouKihonLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic
    Dim strlogic As New StringLogic
    Dim jibanLogic As New JibanLogic

    '更新日時保持用
    Dim dateUpdDateTime As DateTime

#Region "データ更新"
    ''' <summary>
    ''' 地盤テーブル・邸別請求テーブルを更新します
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="listJibanRec">地盤レコードのリスト</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal listJibanRec As List(Of JibanRecordIkkatuHenkouKihon)) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SaveJibanData", _
                                                    sender, _
                                                    listJibanRec)

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

        ' 地盤データ用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' 連携テーブルデータ登録用データの格納用
        Dim renkeiJibanRec As New JibanRenkeiRecord
        Dim renkeiTeibetuList As New List(Of TeibetuSeikyuuRenkeiRecord)

        ' ReportJHS連係対象フラグ(デフォルト：False)
        Dim flgEditReportIf As Boolean

        Dim intDBCnt As Integer = 0 '更新件数
        Dim jibanRec As New JibanRecordIkkatuHenkouKihon 'TMP用

        Try
            ' 地盤テーブルと邸別請求の同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '物件数分ループ
                For intDBCnt = 0 To listJibanRec.Count - 1

                    '対象地盤レコードを作業用のレコードクラスに格納する
                    jibanRec = New JibanRecordIkkatuHenkouKihon
                    jibanRec = listJibanRec(intDBCnt)

                    ' 排他チェック用レコード作成
                    Dim jibanHaitaRec As New JibanHaitaRecord
                    ' 地盤レコードの同一項目を複製
                    RecordMappingHelper.Instance.CopyRecordData(jibanRec, jibanHaitaRec)

                    ' 排他チェック用SQL文自動生成
                    sqlString = upadteMake.MakeHaitaSQLInfo(GetType(JibanHaitaRecord), jibanHaitaRec, haitaList)

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
                    '与信チェックに必要なデータを現在DBデータからコピー
                    jibanRec.SyoudakusyoTysDate = jibanRecOld.SyoudakusyoTysDate
                    jibanRec.Syouhin1Record = jibanRecOld.Syouhin1Record
                    jibanRec.Syouhin2Records = jibanRecOld.Syouhin2Records
                    jibanRec.Syouhin3Records = jibanRecOld.Syouhin3Records

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

                    ' ReportJHS連係対象チェックを行う(経由：0,1,5の場合のみ)
                    flgEditReportIf = False '初期化
                    If jibanRec.Keiyu = 0 Or _
                       jibanRec.Keiyu = 1 Or _
                       jibanRec.Keiyu = 5 Then

                        ' 進捗データ生成系データアクセスクラス
                        Dim reportAccess As New ReportIfDataAccess

                        ' 連携調査会社設定マスタの存在チェック
                        If reportAccess.ChkRenkeiTyousaKaisya(jibanRec.TysKaisyaCd, _
                                                               jibanRec.TysKaisyaJigyousyoCd) = True Then
                            ' 対象の場合、フラグを立てる
                            flgEditReportIf = True
                            ' 反映対象なので経由を5に変更
                            jibanRec.Keiyu = 5
                        Else
                            ' 連携調査会社設定マスタの存在チェックでNGになった場合、経由を9に変更
                            jibanRec.Keiyu = 9
                        End If
                    End If

                    ' 更新用UPDATE文自動生成
                    updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordIkkatuHenkouKihon), jibanRec, list)
                    ' 更新日時取得（システム日時）
                    dateUpdDateTime = DateTime.Now

                    ' 地盤テーブルの更新を行う（新規は採番時、削除は別機能なので地盤本体は更新のみ）
                    If dataAccess.UpdateJibanData(updateString, list) = True Then
                        '邸別請求Tの更新処理はなし
                    End If

                    '先にチェックしておいたReportJHS連係対象チェックを元に、進捗データテーブル更新処理を行う
                    If flgEditReportIf Then
                        ' 進捗データテーブル更新処理呼出
                        If jibanLogic.EditReportIfData(jibanRec) = False Then
                            ' エラー発生時処理終了
                            Return False
                        End If
                    End If

                Next

                '物件数分ループ
                For intDBCnt = 0 To listJibanRec.Count - 1

                    '対象地盤レコードを作業用のレコードクラスに格納する
                    Dim jibanRecTmp = New JibanRecordBase
                    jibanRecTmp = listJibanRec(intDBCnt)

                    '●物件名寄状況の最終チェック
                    If jibanLogic.ChkLatestBukkenNayoseJyky(sender, jibanRecTmp) = False Then
                        ' エラー発生時処理終了
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

End Class
