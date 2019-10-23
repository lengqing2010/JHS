Imports System.Transactions
Imports System.Web.UI

''' <summary>
''' 改良工事画面用のロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class KairyouKoujiLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic
    Dim cbLogic As New CommonBizLogic

    '工事価格データアクセスクラス
    Dim kojDataAcc As New KoujiKakakuDataAccess
    '商品データアクセスクラス
    Dim SyouhinDataAcc As New SyouhinDataAccess

    '更新日時保持用
    Dim dateUpdDateTime As DateTime

    Private Const pStrKojGaisyaAll As String = "ALLAL"
    Private Const pStrTyokuKouji As String = "1"
    Private Const pStrJioTyokuKouji As String = "1J"

#Region "データ更新"
    ''' <summary>
    ''' 地盤テーブル・邸別請求テーブルを更新します
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal jibanRec As JibanRecordKairyouKouji, _
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
                Dim intYosinResult As Integer = YosinLogic.YosinCheck(3, jibanRecOld, jibanRec)
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
                updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordKairyouKouji), jibanRec, list)
                ' 更新日時取得（システム日時）
                dateUpdDateTime = DateTime.Now

                ' 地盤テーブルの更新を行う（新規は採番時、削除は別機能なので地盤本体は更新のみ）
                If dataAccess.UpdateJibanData(updateString, list) = True Then
                    '**************************************************************************
                    ' 邸別請求データの追加・更新
                    '**************************************************************************
                    '邸別請求ロジック
                    Dim tLogic As New TeibetuSyuuseiLogic

                    '改良工事
                    If tLogic.EditTeibetuRecord(sender, _
                                                jibanRec.KairyouKoujiRecord, _
                                                EarthConst.SOUKO_CD_KAIRYOU_KOUJI, _
                                                1, _
                                                jibanRec, _
                                                renkeiTeibetuList, _
                                                GetType(TeibetuSeikyuuRecordKairyouKouji) _
                                                ) = False Then
                        Return False
                    End If

                    '追加工事
                    If tLogic.EditTeibetuRecord(sender, _
                                                jibanRec.TuikaKoujiRecord, _
                                                EarthConst.SOUKO_CD_TUIKA_KOUJI, _
                                                1, _
                                                jibanRec, _
                                                renkeiTeibetuList, _
                                                GetType(TeibetuSeikyuuRecordTuikaKouji) _
                                                ) = False Then
                        Return False
                    End If

                    '工事報告書再発行
                    If tLogic.EditTeibetuRecord(sender, _
                                                jibanRec.KoujiHoukokusyoRecord, _
                                                EarthConst.SOUKO_CD_KOUJI_HOUKOKUSYO, _
                                                1, _
                                                jibanRec, _
                                                renkeiTeibetuList, _
                                                GetType(TeibetuSeikyuuRecordKoujiHoukokusyo) _
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

#Region "追加工事データ自動設定チェック"

    ''' <summary>
    ''' 追加工事データ自動設定チェックを行なう。
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBangou">番号[旧保証書NO]</param>
    ''' <returns>[Boolean]True or False</returns>
    ''' <remarks>
    ''' 邸別請求テーブルより、以下の条件と一致するデータを抽出する。<br/>
    ''' 条件：邸別請求テーブル.区分＝画面.区分<br/>
    ''' 邸別請求テーブル.保証書NO＝画面.保証書NO<br/>
    ''' 邸別請求テーブル.分類ｺｰﾄﾞ＝"140"<br/>
    ''' <br/>
    ''' return False:データが存在する場合、追加工事データ自動設定対象外とする。<br/>
    ''' return True:データが存在しない場合、追加工事データ自動設定対象とする。<br/>
    ''' </remarks>
    Public Function ChkTjDataAutoSetting(ByVal strKbn As String, ByVal strBangou As String) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkTjDataAutoSetting", _
                                                    strKbn, _
                                                    strBangou)

        ' 地盤データ用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        Dim teibetuSeikyuuList As List(Of TeibetuSeikyuuRecord) = _
                    DataMappingHelper.Instance.getMapArray(Of TeibetuSeikyuuRecord)(GetType(TeibetuSeikyuuRecord), _
                    dataAccess.GetTeibetuSeikyuuData(strKbn, strBangou, EarthConst.SOUKO_CD_TUIKA_KOUJI))


        ' 取得レコード分設定を行う
        For Each rec As TeibetuSeikyuuRecord In teibetuSeikyuuList

            ' 分類により格納場所を変更
            Select Case rec.BunruiCd
                Case EarthConst.SOUKO_CD_TUIKA_KOUJI
                    Return False

                Case Else

            End Select
        Next

        Return True
    End Function
#End Region

#Region "改良工事種別変更時処理"

    ''' <summary>
    ''' 改良工事種別変更時処理
    ''' </summary>
    ''' <param name="strHanteiSetuzokuMoji"></param>
    ''' <param name="strKjSyubetu"></param>
    ''' <param name="strKisoSiyouNo1"></param>
    ''' <param name="strKisoSiyouNo2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKairyouKoujiSyubetu( _
                                     ByVal strHanteiSetuzokuMoji As String _
                                     , ByVal strKjSyubetu As String _
                                     , ByVal strKisoSiyouNo1 As String _
                                     , Optional ByVal strKisoSiyouNo2 As String = "" _
                                     ) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKairyouKoujiSyubetu", _
                                            strHanteiSetuzokuMoji, _
                                            strKjSyubetu, _
                                            strKisoSiyouNo1, _
                                            strKisoSiyouNo2)

        '改良工事種別空白時あるいは判定接続文字=2(又は)の場合は未チェック
        If strKjSyubetu = "" Or strHanteiSetuzokuMoji = "2" Then
            Return False
        End If

        ' 基礎仕様データアクセスクラス
        Dim dataAccess As New KisoSiyouDataAccess()

        Return dataAccess.GetHanteiKoujiSyubetuSettei(strHanteiSetuzokuMoji, strKjSyubetu, strKisoSiyouNo1, strKisoSiyouNo2)

    End Function

#End Region

#Region "工事コピー処理チェック用"
    ''' <summary>
    ''' 工事コピー処理チェック用
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <returns>地盤レコード</returns>
    ''' <remarks>地盤レコードには紐付く邸別請求データが格納されております</remarks>
    Public Function GetKoujiCopyData(ByVal kbn, _
                                 ByVal hosyousyoNo) As KoujiCopyRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanData", _
                                            kbn, _
                                            hosyousyoNo)

        ' 地盤データ取得用データアクセスクラス
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' 地盤データの取得
        Dim jibanList As List(Of KoujiCopyRecord) = DataMappingHelper.Instance.getMapArray(Of KoujiCopyRecord)(GetType(KoujiCopyRecord), _
        dataAccess.GetKoujiCopyCheckData(kbn, hosyousyoNo))

        ' 地盤データ保持用のレコードクラス
        Dim copyRec As New KoujiCopyRecord

        If jibanList.Count > 0 Then
            copyRec = jibanList(0)
        End If

        Return copyRec

    End Function
#End Region

#Region "指定工事会社の存在チェック"

    ''' <summary>
    ''' 指定工事会社の存在チェック
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKoujiGaisyaCd">工事会社コード(調査会社コード＋事業所コード)</param>
    ''' <returns>True or False</returns>
    ''' <remarks>該当の調査会社コードが加盟店調査会社設定マスタ.に存在するかを判定する</remarks>
    Public Function ChkExistSiteiKoujiGaisya( _
                                     ByVal strKameitenCd As String _
                                     , ByVal strKoujiGaisyaCd As String _
                                     ) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkExistSiteiKoujiGaisya", _
                                            strKameitenCd _
                                            , strKoujiGaisyaCd _
                                            )

        '工事会社コード=未入力時はチェック不要
        If strKoujiGaisyaCd = "" Then
            Return True
            Exit Function
        End If

        '調査会社コードを取得
        Dim strTyousakaisyaCd As String = strKoujiGaisyaCd.Substring(0, strKoujiGaisyaCd.Length - 2)

        Dim dataAccess As New TyousakaisyaSearchDataAccess

        Return dataAccess.ExistTyousakaisyaCd(strKameitenCd, strKoujiGaisyaCd)

    End Function

#End Region

#Region "工事価格マスタレコード取得"
    ''' <summary>
    ''' 工事価格マスタレコード取得
    ''' </summary>
    ''' <param name="keyRec">検索用KEYレコードクラス</param>
    ''' <param name="resultRec">結果格納用レコードクラス</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKoujiKakakuRecord(ByVal keyRec As KoujiKakakuKeyRecord, _
                                         ByRef resultRec As KoujiKakakuRecord _
                                         ) As EarthEnum.emKoujiKakaku
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKoujiKakakuRecord", _
                                                    keyRec, _
                                                    resultRec)

        '検索結果格納用データテーブル
        Dim table As DataTable
        '検索結果格納用レコードリスト
        Dim syouhinRec As New SyouhinMeisaiRecord

        'パラメータチェック
        If (keyRec.KameitenCd = String.Empty AndAlso keyRec.EigyousyoCd = String.Empty AndAlso keyRec.KeiretuCd = String.Empty) _
            OrElse keyRec.SyouhinCd = String.Empty _
            OrElse keyRec.KojGaisyaCd = String.Empty Then

            ' 未設定の場合、処理終了
            Return EarthEnum.emKoujiKakaku.PrmError
        End If

        '工事タイプ判定
        table = SyouhinDataAcc.GetSyouhinInfo(keyRec.SyouhinCd)
        If table.Rows.Count > 0 Then
            ' 検索結果を格納用リストにセット
            syouhinRec = DataMappingHelper.Instance.getMapArray(Of SyouhinMeisaiRecord)(GetType(SyouhinMeisaiRecord), table)(0)
        End If

        '工事タイプが直工事の場合、工事価格マスタから項目を取得する
        If syouhinRec.KojType = pStrTyokuKouji OrElse syouhinRec.KojType = pStrJioTyokuKouji Then

            '***************************************************************
            ' 工事価格マスタから売上金額・請求有無・工事会社請求有無の取得
            '***************************************************************

            '「加盟店」の工事価格算出
            If Me.GetKoujiKakakuInfo(EarthConst.AITESAKI_KAMEITEN, _
                                             keyRec.KameitenCd, _
                                             keyRec.SyouhinCd, _
                                             keyRec.KojGaisyaCd, _
                                             resultRec _
                                             ) Then
                Return EarthEnum.emKoujiKakaku.Kameiten
            End If

            '「営業所」の工事価格算出
            If Me.GetKoujiKakakuInfo(EarthConst.AITESAKI_EIGYOUSYO, _
                                     keyRec.EigyousyoCd, _
                                     keyRec.SyouhinCd, _
                                     keyRec.KojGaisyaCd, _
                                     resultRec _
                                     ) Then
                Return EarthEnum.emKoujiKakaku.Eigyousyo
            End If

            '「系列」の工事価格算出
            If Me.GetKoujiKakakuInfo(EarthConst.AITESAKI_KEIRETU, _
                                     keyRec.KeiretuCd, _
                                     keyRec.SyouhinCd, _
                                     keyRec.KojGaisyaCd, _
                                     resultRec _
                                     ) Then
                Return EarthEnum.emKoujiKakaku.Keiretu
            End If

            '「指定無」の工事価格算出
            If Me.GetKoujiKakakuInfo(EarthConst.AITESAKI_NASI, _
                                             EarthConst.AITESAKI_NASI_CD, _
                                             keyRec.SyouhinCd, _
                                             keyRec.KojGaisyaCd, _
                                             resultRec _
                                             ) Then
                Return EarthEnum.emKoujiKakaku.SiteiNasi
            End If

            '工事価格マスタにない組み合わせは、商品マスタを見て価格のみを取得
            If syouhinRec.Torikesi = 0 Then
                '売上金額（商品M.標準価格）
                resultRec.UriGaku = syouhinRec.HyoujunKkk
                '税区分
                resultRec.ZeiKbn = syouhinRec.ZeiKbn
                '税率
                resultRec.Zeiritu = syouhinRec.Zeiritu

                '工事会社請求有無
                If keyRec.SyouhinCd = EarthConst.SH_CD_JIO2 Then
                    resultRec.KojGaisyaSeikyuuUmu = 1
                Else
                    resultRec.KojGaisyaSeikyuuUmu = Integer.MinValue
                End If
                '請求有無
                resultRec.SeikyuuUmu = 1

                Return EarthEnum.emKoujiKakaku.TyokuKojSonota
            End If
        Else
            '直工事以外は、今まで通りの取得方法
            Return EarthEnum.emKoujiKakaku.Syouhin
        End If

        Return EarthEnum.emKoujiKakaku.Syouhin
    End Function

    ''' <summary>
    ''' 工事価格の取得
    ''' </summary>
    ''' <param name="intAitesakiSyubetu">相手先種別</param>
    ''' <param name="strAitesakiCd">相手先コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strKojGaisyaCd">工事会社コード</param>
    '''<param name="resultRec">結果格納用レコードクラス</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKoujiKakakuInfo(ByVal intAitesakiSyubetu As Integer, _
                                       ByVal strAitesakiCd As String, _
                                       ByVal strSyouhinCd As String, _
                                       ByVal strKojGaisyaCd As String, _
                                       ByRef resultRec As KoujiKakakuRecord _
                                       ) As Boolean
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKoujiKakakuInfo", _
                                                    intAitesakiSyubetu, _
                                                    strAitesakiCd, _
                                                    strSyouhinCd, _
                                                    strKojGaisyaCd, _
                                                    resultRec _
                                                    )
        Dim dt As New DataTable

        '工事価格算出
        dt = kojDataAcc.GetKoujiKakakuInfo(intAitesakiSyubetu, _
                                         strAitesakiCd, _
                                         strSyouhinCd, _
                                         strKojGaisyaCd _
                                         )

        If dt.Rows.Count > 0 Then
            ' 検索結果を格納用リストにセット
            resultRec = DataMappingHelper.Instance.getMapArray(Of KoujiKakakuRecord)(GetType(KoujiKakakuRecord), dt)(0)
            Return True
        End If

        '工事価格算出(工事会社'ALL')
        dt = kojDataAcc.GetKoujiKakakuInfo(intAitesakiSyubetu, _
                                         strAitesakiCd, _
                                         strSyouhinCd, _
                                         pStrKojGaisyaAll _
                                         )
        If dt.Rows.Count > 0 Then
            ' 検索結果を格納用リストにセット
            resultRec = DataMappingHelper.Instance.getMapArray(Of KoujiKakakuRecord)(GetType(KoujiKakakuRecord), dt)(0)
            Return True
        End If

        Return False
    End Function

#End Region

End Class
