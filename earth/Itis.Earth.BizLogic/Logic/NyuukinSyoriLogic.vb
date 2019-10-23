Imports System.text
Imports System.Transactions

Public Class NyuukinSyoriLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    'メッセージロジック
    Private mLogic As New MessageLogic
    Private sLogic As New StringLogic

#Region "プロパティ"
#Region "画面情報の取得用Setterのみ"
    ''' <summary>
    ''' 請求書発行日FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuFrom As String
    ''' <summary>
    ''' 請求書発行日TO
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuTo As String
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuCd As String
    ''' <summary>
    ''' 画面から取得用 for 請求書発行日FROM
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccSeikyuuFrom() As String
        Set(ByVal value As String)
            strSeikyuuFrom = value
        End Set
    End Property
    ''' <summary>
    ''' 画面から取得用 for 請求書発行日TO
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccSeikyuuTo() As String
        Set(ByVal value As String)
            strSeikyuuTo = value
        End Set
    End Property
    ''' <summary>
    ''' 画面から取得用 for 系列コード
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccKeiretuCd() As String
        Set(ByVal value As String)
            strKeiretuCd = value
        End Set
    End Property
#End Region
#Region "ログインユーザーID"
    ''' <summary>
    ''' ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strLoginUserId As String
    ''' <summary>
    ''' ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> ログインユーザーID</returns>
    ''' <remarks></remarks>
    Public Property LoginUserId() As String
        Get
            Return strLoginUserId
        End Get
        Set(ByVal value As String)
            strLoginUserId = value
        End Set
    End Property
#End Region
#End Region

#Region "ページロード処理"
    ''' <summary>
    ''' 前回入金データ取込情報の取得
    ''' </summary>
    ''' <returns>前回入金データ取込情報のデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetZenkaiTorikomiData() As DataTable
        Dim clsDataAccess As New NyuukinSyoriDataAccess
        Dim zenkaiTorikomiTable As DataTable

        zenkaiTorikomiTable = clsDataAccess.GetZenkaiTorikomiData

        Return zenkaiTorikomiTable
    End Function
#End Region

#Region "一括入金処理"
    ''' <summary>
    ''' 一括入金の対象となるデータを邸別請求テーブルから取得する
    ''' </summary>
    ''' <returns>邸別請求テーブルの処理対象を格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuData() As DataTable
        Dim dtblNyuukinTaisyouData As DataTable
        Dim dtAcc As New NyuukinSyoriDataAccess
        Dim htblParams As Hashtable = setDisplayInfo(dtAcc)

        '対象の邸別請求テーブルのデータを取得
        dtblNyuukinTaisyouData = dtAcc.GetTeibetuSeikyuuTaisyouData(htblParams)

        '例外処理
        If dtblNyuukinTaisyouData.Rows.Count = 0 Then
            '対象データが存在しない場合
            Return Nothing
            Exit Function
        End If

        Return dtblNyuukinTaisyouData
    End Function

    ''' <summary>
    ''' 請求総額（税込）を取得します。
    ''' </summary>
    ''' <param name="calcTable">邸別請求テーブルの処理対象を格納したデータテーブル</param>
    ''' <returns>請求総額（税込）</returns>
    ''' <remarks></remarks>
    Public Function CalcZeikomiUriageGaku(ByVal calcTable As DataTable) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CalcZeikomiUriageGaku" _
                                            , calcTable)
        Dim intZeikomiUriageGaku As Integer
        Dim strRetGaku

        intZeikomiUriageGaku = 0

        Try
            For intCnt As Integer = 0 To calcTable.Rows.Count - 1
                intZeikomiUriageGaku += calcTable.Rows(intCnt).Item("zeikomi_uriage_gaku")
            Next
        Catch ex As System.OverflowException
            strRetGaku = ex.Message
            UnTrappedExceptionManager.Publish(ex)
            Return strRetGaku
            Exit Function
        End Try

        strRetGaku = Format(intZeikomiUriageGaku, EarthConst.FORMAT_KINGAKU_3)
        Return strRetGaku
    End Function

    ''' <summary>
    ''' 一括入金処理
    ''' </summary>
    ''' <param name="intChkUriAgeGaku">請求総額</param>
    ''' <returns>処理結果メッセージ</returns>
    ''' <remarks></remarks>
    Public Function IkkatuNyuukinSyori(ByVal intChkUriAgeGaku As Integer) As String
        Dim strRetMsg As String
        Dim dtAcc As New NyuukinSyoriDataAccess
        Dim clsRenkeiLogic As New RenkeiKanriLogic
        Dim htblParams As Hashtable = setDisplayInfo(dtAcc)
        Dim dtblNyuukinInfo As DataTable
        Dim intSeikyuuSougaku As Integer
        Dim intResult As Integer
        Dim strMakeTmpSql As String
        '連携系TBL
        Dim teibetuNyuukinRenkeiTgtTable As TeibetuNyuukinRenkeiDataSet.TeibetuNyuukinRenkeiTargetDataTable
        Dim teibetuSeikyuuRenkeiTgtTable As TeibetuRenkeiDataSet.TeibetuRenkeiTargetDataTable
        Dim jibanRenkeiTgtTable As JibanRenkeiDataSet.JibanRenkeiTargetDataTable


        Try
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))
                '請求総額を取得
                dtblNyuukinInfo = dtAcc.GetSeikyuuSougaku(htblParams)

                '例外処理
                If Not dtblNyuukinInfo.Rows.Count > 0 Then
                    Return Messages.MSG020E
                    Exit Function
                End If
                '4.二重入金チェック
                If dtblNyuukinInfo.Rows(0).Item("nyuukin_flg") > 0 Then
                    Return Messages.MSG051E
                    Exit Function
                End If

                '請求総額の取得
                intSeikyuuSougaku = dtblNyuukinInfo.Rows(0).Item("zeikomi_uriage_sougaku")

                '請求総額のチェック
                If intChkUriAgeGaku <> intSeikyuuSougaku Then
                    Return Messages.MSG051E
                    Exit Function
                End If

                '入金データ作成用ワークデータの作成
                strMakeTmpSql = dtAcc.setTempTableForNyuukinDate()

                '5.邸別入金テーブルの作成
                '邸別入金テーブルの一括更新
                intResult += dtAcc.UpdTeibetuNyuukin(strMakeTmpSql, htblParams, strLoginUserId)
                '邸別入金テーブル連携管理テーブルの更新対象を取得
                teibetuNyuukinRenkeiTgtTable = dtAcc.GetTeibetuNyuukinUpdTargetKey()
                '邸別入金テーブル連携管理テーブルの更新
                intResult += clsRenkeiLogic.EditTeibetuNyuukinRenkeiRecords(teibetuNyuukinRenkeiTgtTable, strLoginUserId, True)

                '邸別入金テーブル連携管理テーブルの登録対象を取得
                teibetuNyuukinRenkeiTgtTable = dtAcc.GetTeibetuNyuukinInsTargetKey()
                '邸別入金テーブルの一括登録
                intResult += dtAcc.InsTeibetuNyuukin(strLoginUserId)
                '邸別入金テーブル連携管理テーブルの登録
                intResult += clsRenkeiLogic.EditTeibetuNyuukinRenkeiRecords(teibetuNyuukinRenkeiTgtTable, strLoginUserId, False)

                '6.邸別請求テーブルの更新
                intResult += dtAcc.UpdTeibetuSeikyuu(htblParams, strLoginUserId)
                '邸別請求テーブル連携管理テーブルの更新対象を取得
                teibetuSeikyuuRenkeiTgtTable = dtAcc.GetTeibetuSeikyuuRenkeiTarget(htblParams)
                '邸別請求テーブル連携管理テーブルの更新
                intResult += clsRenkeiLogic.EditTeibetuSeikyuuRenkeiRecords(teibetuSeikyuuRenkeiTgtTable, strLoginUserId, True)

                '7.地盤テーブルの更新
                '地盤テーブル更新用ワークテーブルの作成
                intResult += dtAcc.InsJibanUpdTgtKeyToTemp
                '地盤テーブルの更新
                intResult += dtAcc.UpdJibanTable(strLoginUserId)

                '地盤テーブル連携管理テーブルの更新対象を取得
                jibanRenkeiTgtTable = dtAcc.GetJibanRenkeiTarget
                '地盤テーブル連携管理テーブルの更新
                intResult += clsRenkeiLogic.EditJibanRenkeiRecords(jibanRenkeiTgtTable, strLoginUserId, True)

                'テンポラリテーブルの破棄
                intResult += dtAcc.dropTemp

                'scope.Complete()
            End Using

        Catch sqlTimeOut As System.Data.SqlClient.SqlException
            If sqlTimeOut.ErrorCode = -2146232060 _
            AndAlso sqlTimeOut.State = 0 _
            AndAlso sqlTimeOut.Number = -2 _
            AndAlso sqlTimeOut.Message.Contains(EarthConst.TIMEOUT_KEYWORD) = True Then
                strRetMsg = Messages.MSG116E & sLogic.RemoveSpecStr(sqlTimeOut.Message)
                UnTrappedExceptionManager.Publish(sqlTimeOut)
                Return strRetMsg
                Exit Function
            Else
                strRetMsg = Messages.MSG118E & sLogic.RemoveSpecStr(sqlTimeOut.Message)
                UnTrappedExceptionManager.Publish(sqlTimeOut)
                Return strRetMsg
                Exit Function
            End If
        Catch tranTimeOut As System.Transactions.TransactionException
            strRetMsg = Messages.MSG117E & sLogic.RemoveSpecStr(tranTimeOut.Message)
            UnTrappedExceptionManager.Publish(tranTimeOut)
            Return strRetMsg
            Exit Function
        Catch ex As Exception
            strRetMsg = Messages.MSG118E & sLogic.RemoveSpecStr(ex.Message)
            UnTrappedExceptionManager.Publish(ex)
            Return strRetMsg
            Exit Function
        End Try

        strRetMsg = Messages.MSG050S
        Return strRetMsg
    End Function

#Region "プライベートメソッド"
    ''' <summary>
    ''' 画面情報をHashtableに格納
    ''' </summary>
    ''' <param name="dtAcc">入金処理データアクセス</param>
    ''' <returns>画面情報を格納したHashtable</returns>
    ''' <remarks></remarks>
    Private Function setDisplayInfo(ByVal dtAcc As NyuukinSyoriDataAccess) As Hashtable
        Dim htbl As New Hashtable
        Dim dLogic As New DataLogic

        'データアクセスへ渡すパラメータの設定
        htbl.Add(dtAcc.SEIKYUU_FROM, dLogic.str2DtTime(strSeikyuuFrom))
        htbl.Add(dtAcc.SEIKYUU_TO, dLogic.str2DtTime(strSeikyuuTo))

        Return htbl

    End Function

    ''' <summary>
    ''' 邸別請求テーブル用の分類コード抽出条件を取得
    ''' </summary>
    ''' <returns>邸別請求テーブル用の分類コード抽出条件文字列</returns>
    ''' <remarks></remarks>
    Private Function getBunruiCdWhere() As String
        Dim dtblNyuukinTaisyouMst As DataTable
        Dim dtAcc As New NyuukinSyoriDataAccess
        Dim intErrChk As Integer = -1
        Dim strWhereBunruiCd As String

        '画面で設定された系列コードを基に一括入金対象設定マスタのデータを取得
        dtblNyuukinTaisyouMst = dtAcc.GetIkkatuNyuukinTaisyouData(strKeiretuCd)

        '取得出来たレコード件数
        intErrChk = dtblNyuukinTaisyouMst.Rows.Count
        '一括入金対象設定マスタからレコードが取得出来た場合
        If intErrChk > 0 Then
            '一括入金対象設定マスタのレコードに入金処理の対象フラグが立っているかどうかチェック
            For intCnt As Integer = 1 To dtblNyuukinTaisyouMst.Columns.Count - 1
                If dtblNyuukinTaisyouMst.Rows(0).Item(intCnt) = -1 Then
                    intErrChk += 1
                End If
            Next
        End If

        '例外処理
        If Not intErrChk > 0 Then
            Return String.Empty
            Exit Function
        End If

        '分類コードの抽出条件を設定
        strWhereBunruiCd = "(" & setBunruiCdWhere(dtblNyuukinTaisyouMst.Rows(0)) & ")"

        Return strWhereBunruiCd
    End Function

    ''' <summary>
    ''' 邸別請求テーブルの抽出条件（分類コード）を設定
    ''' </summary>
    ''' <param name="tgtDataRow">一括入金対象設定マスタのレコード</param>
    ''' <returns>分類コードの抽出条件</returns>
    ''' <remarks></remarks>
    Private Function setBunruiCdWhere(ByVal tgtDataRow As DataRow) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetBunruiCdWhere" _
                                    , tgtDataRow)
        Dim whereTextSb As New StringBuilder()

        '商品１
        If tgtDataRow.Item("syouhin1") = -1 Then
            whereTextSb.Append("    (TS.bunrui_cd = '100') ")
            'whereTextSb.Append(" OR (TS.bunrui_cd = '110') ")
            'whereTextSb.Append(" OR (TS.bunrui_cd = '115') ")
            'whereTextSb.Append(" OR (TS.bunrui_cd = '180') ")
        End If
        '商品２
        If tgtDataRow.Item("syouhin2") = -1 Then
            If whereTextSb.Length > 0 Then
                whereTextSb.Append(" OR ")
            End If
            whereTextSb.Append(" (TS.bunrui_cd = '110') ")
        End If
        '割引商品２
        If tgtDataRow.Item("waribiki_syouhin2") = -1 Then
            If whereTextSb.Length > 0 Then
                whereTextSb.Append(" OR ")
            End If
            whereTextSb.Append(" (TS.bunrui_cd = '115') ")
        End If
        '商品３
        If tgtDataRow.Item("syouhin3") = -1 Then
            If whereTextSb.Length > 0 Then
                whereTextSb.Append(" OR ")
            End If
            whereTextSb.Append(" (TS.bunrui_cd = '120') ")
        End If
        '改良工事
        If tgtDataRow.Item("kairy_koj") = -1 Then
            If whereTextSb.Length > 0 Then
                whereTextSb.Append(" OR ")
            End If
            whereTextSb.Append(" (TS.bunrui_cd = '130') ")
        End If
        '追加工事
        If tgtDataRow.Item("t_koj") = -1 Then
            If whereTextSb.Length > 0 Then
                whereTextSb.Append(" OR ")
            End If
            whereTextSb.Append(" (TS.bunrui_cd = '140') ")
        End If
        '調査再発行
        If tgtDataRow.Item("tys_saihak") = -1 Then
            If whereTextSb.Length > 0 Then
                whereTextSb.Append(" OR ")
            End If
            whereTextSb.Append(" (TS.bunrui_cd = '150') ")
        End If
        '工事再発行
        If tgtDataRow.Item("koj_saihak") = -1 Then
            If whereTextSb.Length > 0 Then
                whereTextSb.Append(" OR ")
            End If
            whereTextSb.Append(" (TS.bunrui_cd = '160') ")
        End If
        '保証書再発行
        If tgtDataRow.Item("hosyousyo_saihak") = -1 Then
            If whereTextSb.Length > 0 Then
                whereTextSb.Append(" OR ")
            End If
            whereTextSb.Append(" (TS.bunrui_cd = '170') ")
        End If
        '解約払戻
        If tgtDataRow.Item("kaiyaku_harai_modoshi") = -1 Then
            If whereTextSb.Length > 0 Then
                whereTextSb.Append(" OR ")
            End If
            whereTextSb.Append(" (TS.bunrui_cd = '180') ")
        End If
        '商品４
        If tgtDataRow.Item("kaiyaku_harai_modoshi") = -1 Then
            If whereTextSb.Length > 0 Then
                whereTextSb.Append(" OR ")
            End If
            whereTextSb.Append(" (TS.bunrui_cd = '190') ")
        End If

        Return whereTextSb.ToString
    End Function

#End Region

#End Region

#Region "入金データ取り込み処理"
    ''' <summary>
    ''' ファイルのチェックを行います
    ''' </summary>
    ''' <param name="ctlFileUpload">ファイルアップロードコントロール</param>
    ''' <returns>エラーメッセージ</returns>
    ''' <remarks></remarks>
    Public Function ChkFile(ByVal ctlFileUpload As Web.UI.WebControls.FileUpload) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkFile", ctlFileUpload)
        Dim strErrMsg As String = ""

        'エラーメッセージの初期化
        strErrMsg = ""
        'ファイルの指定チェック
        If ctlFileUpload.FileName.Length = 0 Then
            strErrMsg = Messages.MSG056E
            Return strErrMsg
            Exit Function
        End If
        'ファイルの格納チェック
        If (ctlFileUpload.HasFile) = False Then
            strErrMsg = Messages.MSG020E
            Return strErrMsg
            Exit Function
        End If
        'ファイルの容量チェック
        If ctlFileUpload.FileBytes.Length = 0 Then
            strErrMsg = Messages.MSG057E
            Return strErrMsg
            Exit Function
        End If
        Return strErrMsg

    End Function

    ''' <summary>
    ''' 入金データ取り込み処理
    ''' </summary>
    ''' <param name="ctlFileUpload">ファイルアップロードコントロール</param>
    ''' <returns>処理結果メッセージ</returns>
    ''' <remarks></remarks>
    Public Function NyuukinDataTorikomi(ByVal ctlFileUpload As Web.UI.WebControls.FileUpload) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".NyuukinDataTorikomi", ctlFileUpload)
        Dim strRetMsg As String = ""
        Dim myStream As IO.Stream
        Dim myReader As IO.StreamReader
        Dim dtAcc As New NyuukinSyoriDataAccess
        Dim dtblSyouhin As DataTable
        Dim dicSyouhin As New Dictionary(Of String, String)
        Dim intTempResult As Integer
        Dim intReadCnt As Integer
        Dim dtblUpdMngEdi As DataTable
        Dim dicUpdMngEdi As New Dictionary(Of String, String)
        Dim strSoukoCd As String
        Dim strNyuuryokuFileMei As String
        Dim strCsvLine As String
        Dim strCsvArr As String()
        Dim csvRec As New NyuukinDataCsvRecord
        Dim dtTorikomiDate As DateTime = DateTime.Now
        Dim dicJibanKey As New Dictionary(Of String, String)
        Dim strJibanKey As String
        Dim intErrResult As Integer
        Dim intResult As Integer
        Dim dtblChkGassan As DataTable
        Dim NyuukinRenkeiRec As New TeibetuNyuukinRenkeiRecord
        Dim renkeiLogic As New RenkeiKanriLogic
        Dim jibanRenkeiTgtTable As JibanRenkeiDataSet.JibanRenkeiTargetDataTable
        Dim intErrUmu As Integer

        'アップロード機能によりファイルのストリームを取得する
        myStream = ctlFileUpload.FileContent
        myReader = New IO.StreamReader(myStream, System.Text.Encoding.GetEncoding(932))

        Try
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '商品情報の全取得(商品コードと分類コードの紐付け)
                dtblSyouhin = dtAcc.GetSyouhinBunrui()
                If Not dtblSyouhin Is Nothing Then
                    If dtblSyouhin.Rows.Count > 0 Then
                        For intCnt As Integer = 0 To dtblSyouhin.Rows.Count - 1
                            '取得した商品情報を地盤更新用にDictionaryへ格納
                            If Not dicSyouhin.ContainsKey(dtblSyouhin.Rows(intCnt)("syouhin_cd")) Then
                                If IsDBNull(dtblSyouhin.Rows(intCnt)("souko_cd")) Then
                                    strSoukoCd = ""
                                Else
                                    strSoukoCd = dtblSyouhin.Rows(intCnt)("souko_cd")
                                End If
                                dicSyouhin.Add(dtblSyouhin.Rows(intCnt)("syouhin_cd"), strSoukoCd)
                            End If
                        Next
                    Else
                        Return Messages.MSG113E.Replace("@PARAM1", "商品マスタ")
                        Exit Function
                    End If
                Else
                    Return Messages.MSG113E.Replace("@PARAM1", "商品マスタ")
                    Exit Function
                End If

                'アップロード管理テーブルのEDI情報作成日を取得
                dtblUpdMngEdi = dtAcc.getUpdMngEdi()
                If Not dtblUpdMngEdi Is Nothing Then
                    If dtblUpdMngEdi.Rows.Count > 0 Then
                        For intcnt As Integer = 0 To dtblUpdMngEdi.Rows.Count - 1
                            '取得したEDI情報作成日をKEYにDictionaryへ格納
                            If Not dicUpdMngEdi.ContainsKey(dtblUpdMngEdi.Rows(intcnt)("edi_jouhou_sakusei_date")) Then
                                If IsDBNull(dtblUpdMngEdi.Rows(intcnt)("nyuuryoku_file_mei")) Then
                                    strNyuuryokuFileMei = ""
                                Else
                                    strNyuuryokuFileMei = dtblUpdMngEdi.Rows(intcnt)("nyuuryoku_file_mei")
                                End If
                                dicUpdMngEdi.Add(dtblUpdMngEdi.Rows(intcnt)("edi_jouhou_sakusei_date"), strNyuuryokuFileMei)
                            End If
                        Next
                    End If
                Else
                    Return Messages.MSG113E.Replace("@PARAM1", "アップロード管理テーブル")
                    Exit Function
                End If


                'CSV情報格納用テンポラリーテーブルの作成
                intTempResult = dtAcc.MakeTempTable

                'CSVファイルのヘッダ空読み込み
                myReader.ReadLine()
                intReadCnt = 1 '※ヘッダ部は参照しない
                intReadCnt += 1

                '2.取込データの編集
                Do
                    strCsvLine = myReader.ReadLine() & vbCrLf
                    strCsvLine = strCsvLine.Replace("""", "")
                    strCsvArr = strCsvLine.Split(","c)
                    If strCsvArr.Length <> 64 Then
                        Return String.Format(Messages.MSG058E, intReadCnt)
                        Exit Function
                    End If
                    With csvRec
                        '処理日時
                        'CSV 行NO
                        .RowCnt = intReadCnt
                        'CSV 入金日
                        .NyuukinDate = DateTime.Parse(Format(Integer.Parse(strCsvArr(0)), "0000/00/00"))
                        'CSV 入金額
                        .NyuukinGaku = Integer.Parse(strCsvArr(1))
                        'CSV 顧客コード
                        .KokyakuCd = strCsvArr(38)
                        'CSV グループコード
                        .GroupCd = strCsvArr(39)
                        'CSV 手数料額
                        .TesuuRyou = Integer.Parse(strCsvArr(17))
                        'ファイルの重複チェック
                        If intReadCnt = 2 Then '※データ部は2行目以降
                            'CSV ファイル名
                            .FileName = ctlFileUpload.FileName
                            'CSV 取込日時
                            .TorikomiDate = dtTorikomiDate
                            'CSV EDI情報
                            .EdiJouhou = strCsvArr(37)
                            If dicUpdMngEdi.ContainsKey(.EdiJouhou) Then
                                strRetMsg = Messages.MSG059E
                                Return strRetMsg
                                Exit Function
                            End If
                        End If
                        'CSV 請求名目
                        .SeikyuuMeimoku = strCsvArr(50)
                        'CSV 分類コード
                        If strCsvArr(20) <> String.Empty Then
                            .BunruiCd = strCsvArr(20)
                        Else
                            .BunruiCd = dicSyouhin(strCsvArr(50))
                        End If

                        'CSV 画面表示NO
                        .GamenHyoujiNo = strCsvArr(18)

                        'CSV 適用
                        .Tekiyou = strCsvArr(60)
                        If sLogic.GetStrByteSJIS(.Tekiyou) >= 11 Then
                            '11バイト以上ある場合、区分と番号に分割
                            'CSV 区分
                            .Kbn = sLogic.SubstringByByte(.Tekiyou, 0, 1)
                            'CSV 保証書NO
                            .HosyousyoNo = sLogic.SubstringByByte(.Tekiyou, 1, 10)
                            '番号(文字列長：10桁以外、バイト数：10桁以外の場合、エラー)
                            If .HosyousyoNo.Length <> 10 Or sLogic.GetStrByteSJIS(.HosyousyoNo) <> 10 Then
                                strRetMsg = String.Format(Messages.MSG058E, intReadCnt)
                                Return strRetMsg
                                Exit Function
                            End If
                        Else '初期化
                            .Kbn = ""
                            .HosyousyoNo = ""
                        End If
                    End With

                    'CSV取込データをテンポラリテーブルに格納(作業用)
                    intTempResult += dtAcc.InsCsvDataToTemp(csvRec _
                                                            , intReadCnt _
                                                            , dtTorikomiDate)
                    With csvRec
                        '区分と番号の入力がある場合
                        If .Kbn <> "" And .HosyousyoNo <> "" Then
                            '取得したCSV情報を地盤更新用にDictionaryへ格納
                            strJibanKey = .Kbn & "," & .HosyousyoNo
                            If Not dicJibanKey.ContainsValue(strJibanKey) Then
                                dicJibanKey.Add(intReadCnt, strJibanKey)
                            End If
                        End If

                    End With

                    '読み込みカウンタ
                    intReadCnt += 1
                Loop Until myReader.EndOfStream

                '入金エラー情報テーブルへの登録処理
                Dim dtblNyuukinErr As DataTable
                '入金エラー情報の存在チェック
                dtblNyuukinErr = dtAcc.ChkExistNyuukinErrInfo()
                If Not dtblNyuukinErr Is Nothing Then
                    If dtblNyuukinErr.Rows.Count > 0 Then
                        For intCnt As Integer = 0 To dtblNyuukinErr.Rows.Count - 1
                            '取得したCSV情報が邸別請求テーブルに存在しない場合、
                            '抽出したデータをもとに入金エラー情報テーブルに登録する。
                            intErrResult += dtAcc.InsNyuukinError(dtblNyuukinErr.Rows(intCnt), strLoginUserId)
                            '##TEMP_CSV_DATAより入金エラーレコードを削除
                            intErrResult += dtAcc.DelNyuukinErrorData(dtblNyuukinErr.Rows(intCnt))
                        Next

                    End If
                End If
                '邸別入金データの合算処理(※入金エラーレコード削除済み)
                intTempResult += dtAcc.AddTempTableForGassanData

                '合算データを基に、邸別入金テーブルを更新
                intResult += dtAcc.UpdExistTeibetuNyuukinData(strLoginUserId)

                '合算データから邸別入金テーブルに存在するレコードの取得
                dtblChkGassan = dtAcc.ChkExistTeibetuNyuukinData(True)
                If Not dtblChkGassan Is Nothing Then
                    If dtblChkGassan.Rows.Count > 0 Then
                        For intCnt As Integer = 0 To dtblChkGassan.Rows.Count - 1
                            '登録内容を邸別入金連携管理テーブルに反映する
                            With NyuukinRenkeiRec
                                .Kbn = dtblChkGassan.Rows(intCnt)("kbn")
                                .HosyousyoNo = dtblChkGassan.Rows(intCnt)("hosyousyo_no")
                                .BunruiCd = dtblChkGassan.Rows(intCnt)("bunrui_cd")
                                .GamenHyoujiNo = dtblChkGassan.Rows(intCnt)("gamen_hyouji_no")
                                .RenkeiSijiCd = 2
                                .SousinJykyCd = 0
                                .UpdLoginUserId = strLoginUserId
                                .IsUpdate = True
                            End With
                            intResult += renkeiLogic.EditTeibetuNyuukinRenkeiData(NyuukinRenkeiRec)
                        Next
                    End If
                End If

                '合算データから邸別入金テーブルに存在しないレコードの取得
                dtblChkGassan = dtAcc.ChkExistTeibetuNyuukinData(False)
                If Not dtblChkGassan Is Nothing Then
                    If dtblChkGassan.Rows.Count > 0 Then
                        For intCnt As Integer = 0 To dtblChkGassan.Rows.Count - 1

                            '取得したCSV情報が邸別入金テーブルに存在しない場合、邸別入金テーブルの追加を行う。
                            intResult += dtAcc.InsTeibetuNyuukinTorikomi(dtblChkGassan.Rows(intCnt), strLoginUserId)

                            '登録内容を邸別入金連携管理テーブルに反映する
                            With NyuukinRenkeiRec
                                .Kbn = dtblChkGassan.Rows(intCnt)("kbn")
                                .HosyousyoNo = dtblChkGassan.Rows(intCnt)("hosyousyo_no")
                                .BunruiCd = dtblChkGassan.Rows(intCnt)("bunrui_cd")
                                .GamenHyoujiNo = dtblChkGassan.Rows(intCnt)("gamen_hyouji_no")
                                .RenkeiSijiCd = 1
                                .SousinJykyCd = 0
                                .UpdLoginUserId = strLoginUserId
                                .IsUpdate = False
                            End With
                            intResult += renkeiLogic.EditTeibetuNyuukinRenkeiData(NyuukinRenkeiRec)
                        Next
                    End If
                End If

                'Dictionaryに格納したCSV情報を地盤テーブル更新用テンポラリテーブルに格納
                For Each strVal As String In dicJibanKey.Values
                    intTempResult += dtAcc.AddTempTableForTorikomiData(strVal)
                Next

                '3.地盤テーブルの更新
                intResult += dtAcc.UpdJibanTableForNyuukinTorikomi(strLoginUserId)
                '地盤テーブル連携管理テーブルの更新対象を取得
                jibanRenkeiTgtTable = dtAcc.GetJibanRenkeiTargetForNyuukinTorikomi
                '地盤テーブル連携管理テーブルの更新
                intResult += renkeiLogic.EditJibanRenkeiRecords(jibanRenkeiTgtTable, strLoginUserId, True)

                'エラーレコードの有無判定
                If intErrResult > 0 Then
                    intErrUmu = 1
                Else
                    intErrUmu = 0
                End If
                'アップロード管理テーブルに取込情報を登録します
                intResult += dtAcc.InsUpdateKanriTable(csvRec, intErrUmu, strLoginUserId)

                'テンポラリテーブルの破棄
                intResult += dtAcc.dropTempForNyuukinTorikomi

                ' 更新に成功した場合、トランザクションをコミットする
                scope.Complete()
            End Using

        Catch sqlTimeOut As System.Data.SqlClient.SqlException
            If sqlTimeOut.ErrorCode = -2146232060 _
            AndAlso sqlTimeOut.State = 0 _
            AndAlso sqlTimeOut.Number = -2 _
            AndAlso sqlTimeOut.Message.Contains(EarthConst.TIMEOUT_KEYWORD) = True Then
                strRetMsg = Messages.MSG116E & sLogic.RemoveSpecStr(sqlTimeOut.Message)
                UnTrappedExceptionManager.Publish(sqlTimeOut)
                Return strRetMsg
                Exit Function
            Else
                strRetMsg = Messages.MSG118E & sLogic.RemoveSpecStr(sqlTimeOut.Message)
                UnTrappedExceptionManager.Publish(sqlTimeOut)
                Return strRetMsg
                Exit Function
            End If
        Catch tranTimeOut As System.Transactions.TransactionException
            strRetMsg = Messages.MSG117E & sLogic.RemoveSpecStr(tranTimeOut.Message)
            UnTrappedExceptionManager.Publish(tranTimeOut)
            Return strRetMsg
            Exit Function
        Catch ex As Exception
            strRetMsg = Messages.MSG118E & sLogic.RemoveSpecStr(ex.Message)
            UnTrappedExceptionManager.Publish(ex)
            Return strRetMsg
            Exit Function
        End Try


        Return strRetMsg

    End Function
#End Region

End Class
