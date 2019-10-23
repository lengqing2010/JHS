Imports System.Transactions
Imports System.Web.UI
Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' 保証画面用のロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class HosyouLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim mLogic As New MessageLogic
    Dim cbLogic As New CommonBizLogic

    '更新日時保持用
    Dim dateUpdDateTime As DateTime

#Region "地盤データ登録"
    ''' <summary>
    ''' 地盤テーブル・邸別請求テーブルを更新します
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal jibanRec As JibanRecordHosyou, _
                                  ByVal brRec As BukkenRirekiRecord _
                                  ) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SaveJibanData", _
                                                    sender, _
                                                    jibanRec, _
                                                    brRec)

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
                Dim intYosinResult As Integer = YosinLogic.YosinCheck(4, jibanRecOld, jibanRec)
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
                ' 加盟店コード
                If dataAccess.AddKoushinRireki(jibanRec.Kbn, _
                            jibanRec.HosyousyoNo, _
                            JibanDataAccess.enumItemName.KameitenCd, _
                            EarthConst.RIREKI_BUKKEN_KAMEITEN_CD, _
                            jibanRec.KameitenCd, _
                            jibanRec.UpdLoginUserId) = False Then
                    ' エラー発生時処理終了
                    Return False
                End If

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
                updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordHosyou), jibanRec, list)
                ' 更新日時取得（システム日時）
                dateUpdDateTime = DateTime.Now

                ' 地盤テーブルの更新を行う（新規は採番時、削除は別機能なので地盤本体は更新のみ）
                If dataAccess.UpdateJibanData(updateString, list) = True Then

                    '**************************************************************************
                    ' 邸別請求データの追加・更新
                    '**************************************************************************
                    '邸別請求ロジック
                    Dim tLogic As New TeibetuSyuuseiLogic

                    '保証書再発行
                    If tLogic.EditTeibetuRecord(sender, _
                                                jibanRec.HosyousyoRecord, _
                                                EarthConst.SOUKO_CD_HOSYOUSYO, _
                                                1, _
                                                jibanRec, _
                                                renkeiTeibetuList, _
                                                GetType(TeibetuSeikyuuRecordHosyousyoSaihakkou) _
                                                ) = False Then
                        Return False
                    End If

                    '解約払戻
                    If tLogic.EditTeibetuRecord(sender, _
                                                jibanRec.KaiyakuHaraimodosiRecord, _
                                                EarthConst.SOUKO_CD_KAIYAKU_HARAIMODOSI, _
                                                1, _
                                                jibanRec, _
                                                renkeiTeibetuList, _
                                                GetType(TeibetuSeikyuuRecordKaiyakuHaraimodosi) _
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

#Region "地盤データ.発行依頼キャンセル日時更新"
    ''' <summary>
    ''' 地盤データ.発行依頼キャンセル日時のみを更新します。関連する邸別請求データの更新はしません
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateJibanIraiCancel(ByVal sender As Object, _
                                          ByVal jibanRec As JibanRecordHosyou _
                                          ) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibanIraiCancel", _
                                                    sender, _
                                                    jibanRec _
                                                    )

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

                ' 与信チェック処理：やりません
                ' ●更新履歴テーブルの登録：やりません
                ' ●更新履歴連携テーブルの更新：やりません

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
                updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordHosyou), jibanRec, list)
                ' 更新日時取得（システム日時）
                dateUpdDateTime = DateTime.Now

                ' 地盤テーブルの更新を行う（新規は採番時、削除は別機能なので地盤本体は更新のみ）
                If dataAccess.UpdateJibanIraiCancel(jibanRec.Kbn, _
                                                    jibanRec.HosyousyoNo, _
                                                    jibanRec.UpdLoginUserId, _
                                                    jibanRec.HakIraiCanDatetime, _
                                                    jibanRec.UpdDatetime) = True Then

                    '**************************************************************************
                    ' 邸別請求データの追加・更新：やりません
                    '**************************************************************************
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

                ' ●物件履歴テーブル追加(保証有無変更時のみ、物件履歴Tに新規追加する)：やりません

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


#Region "日付マスタ.保証書発行日取得"

    Public Function GetHosyousyoHakkouDate(ByVal strKbn As String) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyousyoHakkouDate", _
                                            strKbn _
                                            )

        Dim dataAccess As New HidukeSaveDataAccess
        Dim strReturn As String = ""

        strReturn = dataAccess.GetHosyousyoHakkouDate(strKbn)

        Return strReturn
    End Function
#End Region

#Region "商品情報の取得"

    ''' <summary>
    ''' 解約払戻価格を取得します
    ''' </summary>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <param name="kbn">区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKaiyakuKakaku(ByVal kameitenCd As String, _
                                     ByVal kbn As String) As Integer

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKaiyakuKakaku", _
                                                    kameitenCd, _
                                                    kbn)

        Dim dataAccess As New KameitenSearchDataAccess

        Return dataAccess.GetKaiyakuKakaku(kameitenCd, kbn)

    End Function

#End Region

    ''' <summary>
    ''' 加盟店マスタ.入金確認条件NOを取得します
    ''' </summary>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <param name="kbn">区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNyuukinKakuninJoukenNo(ByVal kameitenCd As String, _
                                     ByVal kbn As String) As Integer

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetNyuukinKakuninJoukenNo", _
                                                    kameitenCd, _
                                                    kbn)

        Dim dataAccess As New KameitenSearchDataAccess

        Return dataAccess.GetNyuukinKakuninJoukenNo(kameitenCd, kbn)

    End Function

    ''' <summary>
    ''' 加盟店備考設定マスタ.備考種別(=42)のレコードの存在チェックをします
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExistBikouSyubetu(ByVal strKameitenCd As String) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ExistBikouSyubetu", _
                                                        strKameitenCd _
                                                    )

        Dim dataAccess As New KameitenBikouSetteiDataAccess

        Dim intRet As Integer = dataAccess.ChkBikouSyubetu(strKameitenCd, 42)
        If intRet > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

End Class
