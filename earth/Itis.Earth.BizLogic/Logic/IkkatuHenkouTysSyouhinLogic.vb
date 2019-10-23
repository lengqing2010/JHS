Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' 地盤共通処理クラス
''' </summary>
''' <remarks></remarks>
Public Class IkkatuHenkouTysSyouhinLogic

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic

    'UtilitiesのStringLogicクラス
    Dim sLogic As New StringLogic
    Dim cbLogic As New CommonBizLogic

#Region "Dictionary用Key文字列定数"
    Public ReadOnly dkAddDatetime As String = "AddDatetime"
    Public ReadOnly dkAddDatetimeItem As String = "AddDatetimeItem"
    Public ReadOnly dkAddDatetimeJiban As String = "AddDatetimeJiban"
    Public ReadOnly dkAddLoginUserId As String = "AddLoginUserId"
    Public ReadOnly dkBikou As String = "Bikou"
    Public ReadOnly dkBunruiCd As String = "BunruiCd"
    Public ReadOnly dkGamenHyoujiNo As String = "GamenHyoujiNo"
    Public ReadOnly dkHattyuusyoGaku As String = "HattyuusyoGaku"
    Public ReadOnly dkHattyuusyoKakuninDate As String = "HattyuusyoKakuninDate"
    Public ReadOnly dkHattyuusyoKakuteiFlg As String = "HattyuusyoKakuteiFlg"
    Public ReadOnly dkHosyousyoNo As String = "HosyousyoNo"
    Public ReadOnly dkIkkatuNyuukinFlg As String = "IkkatuNyuukinFlg"
    Public ReadOnly dkIraiTousuu As String = "IraiTousuu"
    Public ReadOnly dkKakakuSetteiBasyo As String = "KakakuSetteiBasyo"
    Public ReadOnly dkKakuteiKbn As String = "KakuteiKbn"
    Public ReadOnly dkKameitenCd As String = "KameitenCd"
    Public ReadOnly dkKbn As String = "Kbn"
    Public ReadOnly dkKoumutenSeikyuuGaku As String = "KoumutenSeikyuuGaku"
    Public ReadOnly dkSeikyuuUmu As String = "SeikyuuUmu"
    Public ReadOnly dkSeikyuusyoHakDate As String = "SeikyuusyoHakDate"
    Public ReadOnly dkSesyuMei As String = "SesyuMei"
    Public ReadOnly dkSiireGaku As String = "SiireGaku"
    Public ReadOnly dkStatusIf As String = "StatusIf"
    Public ReadOnly dkSyouhinCd As String = "SyouhinCd"
    Public ReadOnly dkSyouhinKbn As String = "SyouhinKbn"
    Public ReadOnly dkSyouhinMei As String = "SyouhinMei"
    Public ReadOnly dkTatemonoYoutoNo As String = "TatemonoYoutoNo"
    Public ReadOnly dkTysGaiyou As String = "TysGaiyou"
    Public ReadOnly dkTysHouhou As String = "TysHouhou"
    Public ReadOnly dkTysKaisyaCd As String = "TysKaisyaCd"
    Public ReadOnly dkTysKaisyaJigyousyoCd As String = "TysKaisyaJigyousyoCd"
    Public ReadOnly dkTysMitsyoSakuseiDate As String = "TysMitsyoSakuseiDate"
    Public ReadOnly dkUpdDatetime As String = "UpdDatetime"
    Public ReadOnly dkUpdDatetimeItem As String = "UpdDatetimeItem"
    Public ReadOnly dkUpdDatetimeJiban As String = "UpdDatetimeJiban"
    Public ReadOnly dkUpdLoginUserId As String = "UpdLoginUserId"
    Public ReadOnly dkUriDate As String = "UriDate"
    Public ReadOnly dkUriDateItem1 As String = "UriDateItem1"
    Public ReadOnly dkUriGaku As String = "UriGaku"
    Public ReadOnly dkUriKeijouDate As String = "UriKeijouDate"
    Public ReadOnly dkUriKeijyouFlg As String = "UriKeijyouFlg"
    Public ReadOnly dkUriKeijyouFlgItem1 As String = "UriKeijyouFlgItem1"
    Public ReadOnly dkZeiKbn As String = "ZeiKbn"
    Public ReadOnly dkSeikyuuSaki As String = "SeikyuuSaki"
#End Region

#Region "地盤データ更新"
    ''' <summary>
    ''' 地盤データを更新します。関連する邸別請求データの更新も行われます
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="listJibanRec">更新対象の地盤レコード</param>
    ''' <returns>True:更新成功 False:エラーによる更新失敗</returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal listJibanRec As List(Of JibanRecordIkkatuHenkouTysSyouhin), _
                                  ByVal listBrRec As List(Of BukkenRirekiRecord), _
                                  ByVal listTokuRec As List(Of TokubetuTaiouRecordBase)) As Boolean
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SaveJibanData", _
                                            sender, _
                                            listJibanRec, _
                                            listBrRec, _
                                            listTokuRec)

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

        ' 連携テーブルデータ登録用データの格納用
        Dim renkeiJibanRec As New JibanRenkeiRecord
        Dim renkeiTeibetuList As New List(Of TeibetuSeikyuuRenkeiRecord)

        ' 更新日付取得
        Dim updDateTime As DateTime = DateTime.Now

        ' ReportJHS連係対象フラグ(デフォルト：False)
        Dim flgEditReportIf As Boolean

        Dim intDBCnt As Integer = 0 '更新件数
        Dim jibanRec As New JibanRecordIkkatuHenkouTysSyouhin  'TMP用

        Dim jibanLogic As New JibanLogic

        Dim pUpdDateTime As DateTime
        '更新日時取得（システム日時）
        pUpdDateTime = DateTime.Now

        Try
            ' 地盤テーブルと邸別請求の同期を保つため、トランザクション制御を行う
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '物件数分ループ
                For intDBCnt = 0 To listJibanRec.Count - 1

                    '対象地盤レコードを作業用のレコードクラスに格納する
                    jibanRec = New JibanRecordIkkatuHenkouTysSyouhin
                    jibanRec = listJibanRec(intDBCnt)

                    ' 排他チェック用レコード作成
                    Dim jibanHaitaRec As New JibanHaitaRecord

                    ' 地盤データ用データアクセスクラス
                    Dim dataAccess As JibanDataAccess = New JibanDataAccess

                    ' 新規登録、更新の判定（保証書NO採番時の新規登録を除く）
                    Dim isNew As Boolean
                    isNew = dataAccess.ChkJibanNew(jibanRec.Kbn, jibanRec.HosyousyoNo)

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

                    '与信チェック処理
                    Dim jibanRecOld As New JibanRecord
                    jibanRecOld = jibanLogic.GetJibanData(jibanRec.Kbn, jibanRec.HosyousyoNo)
                    '与信チェックに必要なデータを現在DBデータからコピー
                    jibanRec.TysKibouDate = jibanRecOld.TysKibouDate
                    jibanRec.SyoudakusyoTysDate = jibanRecOld.SyoudakusyoTysDate
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

                    ' 更新履歴テーブルの登録
                    ' 調査会社
                    If dataAccess.AddKoushinRireki(jibanRec.Kbn, _
                                                   jibanRec.HosyousyoNo, _
                                                   JibanDataAccess.enumItemName.TyousaKaisya, _
                                                   EarthConst.RIREKI_TYOUSA_KAISYA, _
                                                   jibanRec.TysKaisyaCd & jibanRec.TysKaisyaJigyousyoCd, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' エラー発生時処理終了
                        Return False
                    End If

                    ' 連携用テーブルに登録する（地盤－更新）
                    renkeiJibanRec.Kbn = jibanRec.Kbn
                    renkeiJibanRec.HosyousyoNo = jibanRec.HosyousyoNo
                    renkeiJibanRec.RenkeiSijiCd = 2
                    renkeiJibanRec.SousinJykyCd = 0
                    renkeiJibanRec.UpdLoginUserId = jibanRec.UpdLoginUserId
                    If dataAccess.EditJibanRenkeiData(renkeiJibanRec, True) <> 1 Then
                        ' 登録に失敗したので処理中断
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
                    updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordIkkatuHenkouTysSyouhin), jibanRec, list)

                    ' 地盤テーブルの更新を行う（新規は採番時、削除は別機能なので地盤本体は更新のみ）
                    If dataAccess.UpdateJibanData(updateString, list) = True Then

                        '**************************************************************************
                        ' 邸別請求データの追加・更新
                        '**************************************************************************
                        '邸別請求テーブルデータ操作用
                        Dim TeibetuSyuuseiLogic As New TeibetuSyuuseiLogic
                        Dim tempTeibetuRec As New TeibetuSeikyuuRecord

                        '***************************
                        ' 商品コード１の追加・更新
                        '***************************
                        '商品１レコードをテンポラリにセット
                        tempTeibetuRec = jibanRec.Syouhin1Record

                        '●請求先/仕入先の設定
                        cbLogic.SetSeikyuuSiireSakiInfo(tempTeibetuRec, jibanRecOld.Syouhin1Record)

                        '●消費税額の設定
                        cbLogic.SetUriageSyouhiZeiGaku(tempTeibetuRec, jibanRecOld.Syouhin1Record)

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
                        ' 商品コード２の追加・更新
                        '***************************
                        Dim i As Integer
                        Dim recOldTeibetuForSyouhiZei As TeibetuSeikyuuRecord = Nothing     '消費税計算用邸別請求レコード
                        For i = 1 To EarthConst.SYOUHIN2_COUNT
                            If jibanRec.Syouhin2Records.ContainsKey(i) = True Then

                                '商品２レコードをテンポラリにセット
                                tempTeibetuRec = jibanRec.Syouhin2Records.Item(i)

                                If jibanRecOld.Syouhin2Records IsNot Nothing Then
                                    If jibanRecOld.Syouhin2Records.ContainsKey(i) = True Then
                                        '●請求先/仕入先の設定
                                        cbLogic.SetSeikyuuSiireSakiInfo(tempTeibetuRec, jibanRecOld.Syouhin2Records.Item(i))
                                        '消費税計算用
                                        recOldTeibetuForSyouhiZei = jibanRecOld.Syouhin2Records.Item(i)
                                    Else
                                        '消費税計算用
                                        recOldTeibetuForSyouhiZei = Nothing
                                    End If
                                Else
                                    '消費税計算用
                                    recOldTeibetuForSyouhiZei = Nothing
                                End If

                                '●消費税額の設定
                                cbLogic.SetUriageSyouhiZeiGaku(tempTeibetuRec, recOldTeibetuForSyouhiZei)

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
                            Else
                                ' 削除処理用(削除確認は商品２の分類コード何れかで問題なし)
                                If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                         Nothing, _
                                                                         EarthConst.SOUKO_CD_SYOUHIN_2_110, _
                                                                         i, _
                                                                         jibanRec, _
                                                                         renkeiTeibetuList _
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

                        '先にチェックしておいたReportJHS連係対象チェックを元に、進捗データテーブル更新処理を行う
                        If flgEditReportIf Then
                            ' 進捗データテーブル更新処理呼出
                            If jibanLogic.EditReportIfData(jibanRec) = False Then
                                ' エラー発生時処理終了
                                Return False
                            End If
                        End If

                    Else
                        Return False
                    End If
                Next

                For intDBCnt = 0 To listBrRec.Count - 1
                    Dim brRec As BukkenRirekiRecord = listBrRec(intDBCnt)

                    ' ●物件履歴テーブル追加(保証有無変更時のみ、物件履歴Tに新規追加する)
                    If Not brRec Is Nothing Then
                        Dim brLogic As New BukkenRirekiLogic

                        '新規追加用スクリプトおよび実行
                        If brLogic.InsertBukkenRireki(brRec) = False Then
                            Return False
                        End If
                    End If
                Next

                '**********************************************
                ' 特別対応データの更新(邸別請求データとの紐付)
                '**********************************************
                If Not listTokuRec Is Nothing Then
                    If cbLogic.pf_setKey_TokubetuTaiou_TeibetuSeikyuu(sender, listTokuRec, jibanRec.UpdLoginUserId, EarthEnum.emGamenInfo.IkkatuTysSyouhinInfo) = False Then
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

    ''' <summary>
    ''' 文字列型列挙体の連結処理
    ''' </summary>
    ''' <param name="IEnum">列挙体（文字列型）</param>
    ''' <returns>連結後の文字列</returns>
    ''' <remarks></remarks>
    Public Function getJoinString(ByVal IEnum As IEnumerator(Of String)) As String
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getJoinString", _
                                            IEnum)
        Dim strJoinStr As String = ""

        IEnum.MoveNext()
        strJoinStr = IEnum.Current

        Do While IEnum.MoveNext
            strJoinStr &= EarthConst.SEP_STRING & IEnum.Current
        Loop

        Return strJoinStr

    End Function

    ''' <summary>
    ''' ダラー($$$)区切りの文字列を配列に変換
    ''' </summary>
    ''' <param name="strDollar">ダラー($$$)区切りの文字列</param>
    ''' <returns>ダラー($$$)で区切った配列</returns>
    ''' <remarks></remarks>
    Public Function getArrayFromDollarSep(ByVal strDollar As String, Optional ByVal delSpace As Boolean = False) As String()
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getArrayFromDollarSep", _
                                            strDollar)
        Dim strWk() As String
        Dim strSep(0) As String
        strSep(0) = EarthConst.SEP_STRING

        If delSpace Then
            strWk = strDollar.Split(strSep, System.StringSplitOptions.RemoveEmptyEntries)
        Else
            strWk = strDollar.Split(strSep, System.StringSplitOptions.None)
        End If


        Return strWk
    End Function

    ''' <summary>
    ''' 計算処理用Hiddenの値を格納した配列をDictionaryに登録
    ''' </summary>
    ''' <param name="strItem1ChgValues">計算処理用Hiddenの値を格納した配列</param>
    ''' <returns>計算処理用Hiddenの値を格納したDictionary</returns>
    ''' <remarks>※画面に表示されている金額以外を登録 ⇒ 計算処理後に発生する金額のみの変更は再計算が不要な為</remarks>
    Public Function getDicItem1(ByVal strItem1ChgValues() As String) As Dictionary(Of String, String)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getDicItem1", _
                                            strItem1ChgValues)

        Dim dic As New Dictionary(Of String, String)

        'IkkatuHenkouTysSyouhin1RecordCtrl.setCtrlFromJibanRecメソッドと同じ順番で登録すること
        With dic
            '地盤テーブル用データの取得
            .Add(dkKbn, strItem1ChgValues(0))
            .Add(dkHosyousyoNo, strItem1ChgValues(1))
            .Add(dkTysHouhou, strItem1ChgValues(2))
            .Add(dkTysKaisyaCd, strItem1ChgValues(3))
            .Add(dkTysKaisyaJigyousyoCd, strItem1ChgValues(4))
            .Add(dkKameitenCd, strItem1ChgValues(5))
            .Add(dkSesyuMei, strItem1ChgValues(6))
            .Add(dkSyouhinKbn, strItem1ChgValues(7))
            .Add(dkTysGaiyou, strItem1ChgValues(8))
            .Add(dkIraiTousuu, strItem1ChgValues(9))
            .Add(dkKakakuSetteiBasyo, strItem1ChgValues(10))
            .Add(dkTatemonoYoutoNo, strItem1ChgValues(11))
            .Add(dkStatusIf, strItem1ChgValues(12))
            .Add(dkAddDatetimeJiban, strItem1ChgValues(13))
            .Add(dkUpdDatetimeJiban, strItem1ChgValues(14))
            '邸別請求テーブル用データの取得
            .Add(dkBunruiCd, strItem1ChgValues(15))
            .Add(dkGamenHyoujiNo, strItem1ChgValues(16))
            .Add(dkSyouhinCd, strItem1ChgValues(17))
            .Add(dkZeiKbn, strItem1ChgValues(18))
            .Add(dkSeikyuusyoHakDate, strItem1ChgValues(19))
            .Add(dkUriDate, strItem1ChgValues(20))
            .Add(dkSeikyuuUmu, strItem1ChgValues(21))
            .Add(dkKakuteiKbn, strItem1ChgValues(22))
            .Add(dkUriKeijyouFlg, strItem1ChgValues(23))
            .Add(dkUriKeijouDate, strItem1ChgValues(24))
            .Add(dkBikou, strItem1ChgValues(25))
            .Add(dkHattyuusyoGaku, strItem1ChgValues(26))
            .Add(dkHattyuusyoKakuninDate, strItem1ChgValues(27))
            .Add(dkIkkatuNyuukinFlg, strItem1ChgValues(28))
            .Add(dkTysMitsyoSakuseiDate, strItem1ChgValues(29))
            .Add(dkHattyuusyoKakuteiFlg, strItem1ChgValues(30))
            .Add(dkAddLoginUserId, strItem1ChgValues(31))
            .Add(dkAddDatetimeItem, strItem1ChgValues(32))
            .Add(dkUpdLoginUserId, strItem1ChgValues(33))
            .Add(dkUpdDatetimeItem, strItem1ChgValues(34))
        End With

        Return dic

    End Function

    ''' <summary>
    ''' 変更確認用Hiddenの金額を格納した配列をDictionaryに登録
    ''' </summary>
    ''' <param name="strItem1ChgValues">変更確認用Hiddenの値を格納した配列</param>
    ''' <returns>変更確認用Hiddenの値を格納したDictionary</returns>
    ''' <remarks></remarks>
    Public Function getDicItem1Kingaku(ByVal strItem1ChgValues) As Dictionary(Of String, String)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getDicItem1Kingaku", _
                                            strItem1ChgValues)
        Dim dic As New Dictionary(Of String, String)
        'IkkatuHenkouTysSyouhin1RecordCtrl.setCtrlFromJibanRecメソッドと同じ順番で登録すること
        With dic
            .Add(dkKoumutenSeikyuuGaku, strItem1ChgValues(0))
            .Add(dkUriGaku, strItem1ChgValues(1))
            .Add(dkSiireGaku, strItem1ChgValues(2))
        End With

        Return dic

    End Function

    ''' <summary>
    ''' 計算処理用Hiddenの値を格納した配列をDictionaryに登録
    ''' </summary>
    ''' <param name="strItem2ChgValues">計算処理用Hiddenの値を格納した配列</param>
    ''' <param name="isMakeBlank">空のDictionary作成判断フラグ</param>
    ''' <returns>計算処理用Hiddenの値を格納したDictionary</returns>
    ''' <remarks></remarks>
    Public Function getDicItem2(ByVal strItem2ChgValues() As String, ByVal isMakeBlank As Boolean) As Dictionary(Of String, String)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getDicItem2", _
                                                    strItem2ChgValues, _
                                                    isMakeBlank)
        Dim dic As New Dictionary(Of String, String)
        'IkkatuHenkouTysSyouhin2RecordCtrl.setCtrlFromJibanRecメソッドと同じ順番で登録すること
        With dic
            If isMakeBlank Then
                '地盤テーブル用データの取得
                .Add(dkKameitenCd, String.Empty)
                '邸別請求テーブル用データの取得(商品１)
                .Add(dkUriKeijyouFlgItem1, String.Empty)
                .Add(dkUriDateItem1, String.Empty)
                '邸別請求テーブル用データの取得(商品２)
                .Add(dkKbn, String.Empty)
                .Add(dkHosyousyoNo, String.Empty)
                .Add(dkBunruiCd, String.Empty)
                .Add(dkGamenHyoujiNo, String.Empty)
                .Add(dkSyouhinCd, String.Empty)
                .Add(dkZeiKbn, String.Empty)
                .Add(dkSeikyuusyoHakDate, String.Empty)
                .Add(dkUriDate, String.Empty)
                .Add(dkSeikyuuUmu, String.Empty)
                .Add(dkKakuteiKbn, String.Empty)
                .Add(dkUriKeijyouFlg, String.Empty)
                .Add(dkUriKeijouDate, String.Empty)
                .Add(dkBikou, String.Empty)
                .Add(dkHattyuusyoGaku, String.Empty)
                .Add(dkHattyuusyoKakuninDate, String.Empty)
                .Add(dkIkkatuNyuukinFlg, String.Empty)
                .Add(dkTysMitsyoSakuseiDate, String.Empty)
                .Add(dkHattyuusyoKakuteiFlg, String.Empty)
                .Add(dkAddLoginUserId, String.Empty)
                .Add(dkAddDatetime, String.Empty)
                .Add(dkUpdLoginUserId, String.Empty)
                .Add(dkUpdDatetime, String.Empty)
                .Add(dkSeikyuuSaki, String.Empty)
            Else
                '地盤テーブル用データの取得
                .Add(dkKameitenCd, strItem2ChgValues(0))
                '邸別請求テーブル用データの取得(商品１)
                .Add(dkUriKeijyouFlgItem1, strItem2ChgValues(1))
                .Add(dkUriDateItem1, strItem2ChgValues(2))
                '邸別請求テーブル用データの取得(商品２)
                .Add(dkKbn, strItem2ChgValues(3))
                .Add(dkHosyousyoNo, strItem2ChgValues(4))
                .Add(dkBunruiCd, strItem2ChgValues(5))
                .Add(dkGamenHyoujiNo, strItem2ChgValues(6))
                .Add(dkSyouhinCd, strItem2ChgValues(7))
                .Add(dkZeiKbn, strItem2ChgValues(8))
                .Add(dkSeikyuusyoHakDate, strItem2ChgValues(9))
                .Add(dkUriDate, strItem2ChgValues(10))
                .Add(dkSeikyuuUmu, strItem2ChgValues(11))
                .Add(dkKakuteiKbn, strItem2ChgValues(12))
                .Add(dkUriKeijyouFlg, strItem2ChgValues(13))
                .Add(dkUriKeijouDate, strItem2ChgValues(14))
                .Add(dkBikou, strItem2ChgValues(15))
                .Add(dkHattyuusyoGaku, strItem2ChgValues(16))
                .Add(dkHattyuusyoKakuninDate, strItem2ChgValues(17))
                .Add(dkIkkatuNyuukinFlg, strItem2ChgValues(18))
                .Add(dkTysMitsyoSakuseiDate, strItem2ChgValues(19))
                .Add(dkHattyuusyoKakuteiFlg, strItem2ChgValues(20))
                .Add(dkAddLoginUserId, strItem2ChgValues(21))
                .Add(dkAddDatetime, strItem2ChgValues(22))
                .Add(dkUpdLoginUserId, strItem2ChgValues(23))
                .Add(dkUpdDatetime, strItem2ChgValues(24))
                .Add(dkSeikyuuSaki, strItem2ChgValues(25))
            End If
        End With

        Return dic

    End Function

    ''' <summary>
    ''' 変更確認用Hiddenの金額を格納した配列をDictionaryに登録
    ''' </summary>
    ''' <param name="strItem2ChgValues">変更確認用Hiddenの値を格納した配列</param>
    ''' <param name="isMakeBlank">空のDictionary作成判断フラグ</param>
    ''' <returns>変更確認用Hiddenの値を格納したDictionary</returns>
    ''' <remarks></remarks>
    Public Function getDicItem2Kingaku(ByVal strItem2ChgValues() As String, ByVal isMakeBlank As Boolean) As Dictionary(Of String, String)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getDicItem2Kingaku", _
                                                    strItem2ChgValues, _
                                                    isMakeBlank)
        Dim dic As New Dictionary(Of String, String)

        'IkkatuHenkouTysSyouhin2RecordCtrl.setCtrlFromJibanRecメソッドと同じ順番で登録すること
        With dic
            If isMakeBlank Then
                .Add(dkGamenHyoujiNo, String.Empty)
                .Add(dkKoumutenSeikyuuGaku, String.Empty)
                .Add(dkUriGaku, String.Empty)
                .Add(dkSiireGaku, String.Empty)
            Else
                .Add(dkGamenHyoujiNo, strItem2ChgValues(0))
                .Add(dkKoumutenSeikyuuGaku, strItem2ChgValues(1))
                .Add(dkUriGaku, strItem2ChgValues(2))
                .Add(dkSiireGaku, strItem2ChgValues(3))
            End If
        End With

        Return dic

    End Function

End Class
