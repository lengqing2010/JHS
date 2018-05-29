Imports System.Transactions
Imports Itis.Earth.DataAccess
Public Class HanbaiKakakuMasterLogic
    ''' <summary>販売価格マスタクラスのインスタンス生成 </summary>
    Private HanbaiKakakuMasterDA As New HanbaiKakakuMasterDataAccess
    ''' <summary>CSV取込共通クラスのインスタンス生成 </summary>
    Private commonCheck As New CsvInputCheck

    ''' <summary>相手先種別を取得する</summary>
    ''' <returns>相手先種別データテーブル</returns>
    Public Function GetAiteSakiSyubetu() As Data.DataTable
        Return HanbaiKakakuMasterDA.SelAiteSakiSyubetu()
    End Function
    ''' <summary>商品を取得する</summary>
    ''' <returns>商品データテーブル</returns>
    Public Function GetSyouhin() As Data.DataTable
        Return HanbaiKakakuMasterDA.SelSyouhin()
    End Function
    ''' <summary>調査方法を取得する</summary>
    ''' <returns>調査方法データテーブル</returns>
    Public Function GetTyousaHouhou() As Data.DataTable
        Return HanbaiKakakuMasterDA.SelTyousaHouhou()
    End Function
    '''<summary>相手先情報を取得する</summary>
    ''' <param name="strAitesakiSyubetu">相手先種別</param>
    ''' <param name="strTorikesiAitesaki">相手先取消区分</param>
    ''' <param name="strAitesakiCd">相手先コード</param>
    ''' <returns>相手先情報データテーブル</returns>
    Public Function GetAiteSaki(ByVal strAitesakiSyubetu As String, _
                                ByVal strTorikesiAitesaki As String, _
                                ByVal strAitesakiCd As String) As Data.DataTable
        Return HanbaiKakakuMasterDA.SelAiteSaki(strAitesakiSyubetu, strTorikesiAitesaki, strAitesakiCd)
    End Function
    ''' <summary>販売価格データを取得する</summary>
    ''' <param name="dtHanbaiKakakuInfo">パラメータ</param>
    ''' <returns>販売価格データテーブル</returns>
    Public Function GetHanbaiKakakuInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
            As HanbaiKakakuMasterDataSet.HanbaiKakakuInfoTableDataTable
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuInfo(dtHanbaiKakakuInfo)
    End Function
    ''' <summary>販売価格「系列・営業所・指定なしチェックボックス」=チェックの場合のデータを取得する</summary>
    ''' <param name="dtHanbaiKakakuInfo">パラメータ</param>
    ''' <returns>販売価格データテーブル</returns>
    Public Function GetHanbaiKakakuSeiteNasiInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
            As HanbaiKakakuMasterDataSet.HanbaiKakakuInfoTableDataTable
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuSeiteNasiInfo(dtHanbaiKakakuInfo)
    End Function
    ''' <summary>販売価格データ件数を取得する</summary>
    ''' <param name="dtHanbaiKakakuInfo">パラメータ</param>
    ''' <returns>販売価格データ件数</returns>
    Public Function GetHanbaiKakakuInfoCount(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
            As Integer
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuInfoCount(dtHanbaiKakakuInfo)
    End Function
    ''' <summary>販売価格データ「系列・営業所・指定なしチェックボックス」=チェックの場合の件数を取得する</summary>
    ''' <param name="dtHanbaiKakakuInfo">パラメータ</param>
    ''' <returns>販売価格データ件数</returns>
    Public Function GetHanbaiKakakuSeiteNasiInfoCount(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
            As Integer
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuSeiteNasiinfoCount(dtHanbaiKakakuInfo)
    End Function
    ''' <summary>未設定も含む販売価格CSVデータ件数を取得する</summary>
    ''' <param name="dtHanbaiKakakuInfo">パラメータ</param>
    ''' <returns>未設定も含む販売価格CSVデータ件数</returns>
    Public Function GetMiSeteiHanbaiKakakuCSVCount(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
            As Long
        Return HanbaiKakakuMasterDA.SelMiSeteiHanbaiKakakuCSVCount(dtHanbaiKakakuInfo)
    End Function
    ''' <summary>未設定も含む販売価格CSVデータを取得する</summary>
    ''' <param name="dtHanbaiKakakuInfo">パラメータ</param>
    ''' <returns>未設定も含む販売価格CSVデータテーブル</returns>
    Public Function GetMiSeteiHanbaiKakakuCSVInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
            As HanbaiKakakuMasterDataSet.HanbaiKakakuCSVInfoTableDataTable
        Return HanbaiKakakuMasterDA.SelMiSeteiHanbaiKakakuCSVInfo(dtHanbaiKakakuInfo)
    End Function
    ''' <summary>販売価格CSVデータを取得する</summary>
    ''' <param name="dtHanbaiKakakuInfo">パラメータ</param>
    ''' <returns>販売価格CSVデータテーブル</returns>
    Public Function GetHanbaiKakakuCSVInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
            As HanbaiKakakuMasterDataSet.HanbaiKakakuCSVInfoTableDataTable
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuCSVInfo(dtHanbaiKakakuInfo)
    End Function
    ''' <summary>アップロード管理を取得する</summary>
    ''' <returns>アップロード管理データテーブル</returns>
    Public Function GetUploadKanri() As Data.DataTable
        Return HanbaiKakakuMasterDA.SelUploadKanri()
    End Function
    ''' <summary>アップロード管理の件数を取得する</summary>
    ''' <returns>アップロード管理の件数</returns>
    Public Function GetUploadKanriCount() As Integer
        Return HanbaiKakakuMasterDA.SelUploadKanriCount()
    End Function
    ''' <summary>CSVファイルをチェックする</summary>
    ''' <param name="fupId">アップロード</param>
    ''' <param name="strUmuFlg">エラー有無フラグ</param>
    ''' <returns>CSVファイルをチェックする</returns>
    Public Function ChkCsvFile(ByVal fupId As Web.UI.WebControls.FileUpload, ByRef strUmuFlg As String) As String

        Dim myStream As IO.Stream                               '入出力ストリーム
        Dim myReader As IO.StreamReader                         'ストリームリーダー
        Dim strReadLine As String                               '取込ファイル読込み行
        Dim intLineCount As Integer = 0                         'ライン数
        Dim strCsvLine() As String                              'CSVファイル内容
        Dim strNyuuryokuFileMei As String                       'CSVファイル名
        Dim strEdiJouhouSakuseiDate As String = String.Empty    'EDI情報作成日
        Dim dtHanbaiKakakuOk As New Data.DataTable              '販売価格マスタ
        Dim dtHanbaiKakakuError As New Data.DataTable           '販売価格エラー
        Dim strUploadDate As String                             'アップロード日時
        Dim ninsyou As New Ninsyou()
        Dim strUserId As String                                 'ユーザーID
        Dim strMaxUpload As String                              'CSV取込の上限件数

        'アップロード日時
        strUploadDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")
        'ユーザーID
        strUserId = ninsyou.GetUserID()
        'CSVファイル名
        strNyuuryokuFileMei = commonCheck.CutMaxLength(fupId.FileName, 128)
        'CSV取込の上限件数
        strMaxUpload = System.Configuration.ConfigurationManager.AppSettings("CsvInputMaxLineCount").ToString
        '入出力ストリーム
        myStream = fupId.FileContent
        'ストリームリーダー
        myReader = New IO.StreamReader(myStream, System.Text.Encoding.GetEncoding(932))
        '販売価格マスタを作成する
        Call SetHanbaiKakakuOk(dtHanbaiKakakuOk)
        '販売価格エラーテーブルを作成する
        Call SetHanbaiKakakuError(dtHanbaiKakakuError)

        Try
            Do
                '取込ファイルを読み込む
                ReDim Preserve strCsvLine(intLineCount)
                strCsvLine(intLineCount) = myReader.ReadLine()
                intLineCount += 1
            Loop Until myReader.EndOfStream

            'CSVアップロード上限件数チェック
            For i As Integer = strCsvLine.Length - 1 To 0 Step -1
                If Not strCsvLine(i).Trim.Equals(String.Empty) Then
                    If i > CInt(strMaxUpload) Then
                        Return String.Format(Messages.Instance.MSG040E, strMaxUpload)
                    ElseIf i < 1 Then
                        Return String.Format(Messages.Instance.MSG048E)
                    Else
                        Exit For
                    End If
                End If
            Next

            'EDI情報作成日
            strEdiJouhouSakuseiDate = Right("0" & strCsvLine(1).Split(",")(0), 10)

            'CSVファイルをチェック
            For i As Integer = 1 To strCsvLine.Length - 1
                strReadLine = strCsvLine(i)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                    'EDI情報作成日が前0埋め変換
                    If strReadLine.Split(",")(0).Length < 10 Then
                        strReadLine = "0" & strReadLine
                    End If
                    '桁数埋め変換
                    If strReadLine.Split(",").Length < CsvInputCheck.HANBAI_FIELD_COUNT Then
                        strReadLine = strReadLine & StrDup(CsvInputCheck.HANBAI_FIELD_COUNT - strReadLine.Split(",").Length, ",")
                    End If
                    'フィールド数チェック
                    If Not commonCheck.ChkFieldCount(strReadLine.Split(",").Length, CsvInputCheck.HANBAI) Then
                        'エラーデータを作成する
                        Call SetHanbaiKakakuErrData(i + 1, strReadLine, dtHanbaiKakakuError)
                        Continue For
                    End If
                    '項目最大長チェック
                    If Not commonCheck.ChkMaxLength(strReadLine, CsvInputCheck.HANBAI) Then
                        'エラーデータを作成する
                        Call SetHanbaiKakakuErrData(i + 1, strReadLine, dtHanbaiKakakuError)
                        Continue For
                    End If
                    '禁則文字チェック
                    If Not commonCheck.ChkKinjiMoji(strReadLine) Then
                        'エラーデータを作成する
                        Call SetHanbaiKakakuErrData(i + 1, strReadLine, dtHanbaiKakakuError)
                        Continue For
                    End If
                    '数値型項目チェック
                    If Not commonCheck.ChkSuuti(strReadLine, CsvInputCheck.HANBAI) Then
                        'エラーデータを作成する
                        Call SetHanbaiKakakuErrData(i + 1, strReadLine, dtHanbaiKakakuError)
                        Continue For
                    End If
                    '必須チェック
                    If Not commonCheck.ChkNotNull(strReadLine, CsvInputCheck.HANBAI) Then
                        'エラーデータを作成する
                        Call SetHanbaiKakakuErrData(i + 1, strReadLine, dtHanbaiKakakuError)
                        Continue For
                    End If
                    '相手先コードが前0埋め変換
                    SetAitesakiCd(strReadLine)
                    '存在チェック
                    If Not ChkMstSonZai(strReadLine) Then
                        'エラーデータを作成する
                        Call SetHanbaiKakakuErrData(i + 1, strReadLine, dtHanbaiKakakuError)
                        Continue For
                    End If
                    '更新条件チェック
                    If Not commonCheck.ChkKousinJyouken(strReadLine, CsvInputCheck.HANBAI) Then
                        Continue For
                    End If
                    '更新･追加を判断する
                    Call SetKousinKbn(strReadLine, dtHanbaiKakakuOk)
                End If
            Next
            'エラー有無フラグを設定する
            If dtHanbaiKakakuError.Rows.Count > 0 Then
                strUmuFlg = "1"
            End If
            'CSVファイルを取込
            If Not CsvFileUpload(dtHanbaiKakakuOk, dtHanbaiKakakuError, strUploadDate, strUserId, strNyuuryokuFileMei, strEdiJouhouSakuseiDate) Then
                Return Messages.Instance.MSG050E
            End If
        Catch ex As Exception
            Return Messages.Instance.MSG049E
        End Try

        Return String.Empty
    End Function
    ''' <summary>販売価格テーブルを作成する</summary>
    ''' <param name="dtHanbaiKakakuOk">販売価格テーブル</param>
    Public Sub SetHanbaiKakakuOk(ByRef dtHanbaiKakakuOk As Data.DataTable)

        Dim dc As Data.DataColumn

        dc = New Data.DataColumn
        dc.ColumnName = "key"               'キー(相手先種別 + 相手先コード + 商品コード + 調査方法NO)
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "ins_upd_flg"       '追加更新FLG(0:追加; 1:更新)
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"  '相手先種別
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"       '相手先コード
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"        '商品コード
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_houhou_no"     '調査方法NO
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"          '取消
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koumuten_seikyuu_gaku"  '工務店請求金額
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koumuten_seikyuu_gaku_henkou_flg"   '工務店請求金額変更FLG
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jitu_seikyuu_gaku"  '実請求金額
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jitu_seikyuu_gaku_henkou_flg"   '実請求金額変更FLG
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koukai_flg"        '公開フラグ
        dtHanbaiKakakuOk.Columns.Add(dc)

    End Sub
    ''' <summary>販売価格エラーテーブルを作成する</summary>
    ''' <param name="dtHanbaiKakakuError">販売価格エラーテーブル</param>
    Public Sub SetHanbaiKakakuError(ByRef dtHanbaiKakakuError As Data.DataTable)

        Dim dc As Data.DataColumn
        dc = New Data.DataColumn
        dc.ColumnName = "edi_jouhou_sakusei_date"           'EDI情報作成日
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"                  '相手先種別
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"                       '相手先コード
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_mei"                      '相手先名
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"                        '商品コード
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_mei"                       '商品名
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_houhou_no"                     '調査方法NO
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_houhou"                        '調査方法
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"                          '取消
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koumuten_seikyuu_gaku"             '工務店請求金額
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koumuten_seikyuu_gaku_henkou_flg"  '工務店請求金額変更FLG
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jitu_seikyuu_gaku"                  '実請求金額
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jitu_seikyuu_gaku_henkou_flg"      '実請求金額変更FLG
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koukai_flg"                        '公開フラグ
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "gyou_no"                           '行NO
        dtHanbaiKakakuError.Columns.Add(dc)

    End Sub
    ''' <summary>販売価格エラーデータを作成する</summary>
    ''' <param name="intLineNo">CSVファイルの該当行の行NO</param>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    ''' <param name="dtHanbaiKakakuError">販売価格エラーデータ</param>
    Private Sub SetHanbaiKakakuErrData(ByVal intLineNo As Integer, _
                                       ByVal strLine As String, _
                                       ByRef dtHanbaiKakakuError As Data.DataTable)

        Dim intMaxCount As Integer

        Dim dr As Data.DataRow
        dr = dtHanbaiKakakuError.NewRow

        If strLine.Split(",").Length < CsvInputCheck.HANBAI_FIELD_COUNT Then
            intMaxCount = strLine.Split(",").Length
        Else
            intMaxCount = CsvInputCheck.HANBAI_FIELD_COUNT
        End If

        For i As Integer = 0 To intMaxCount - 1
            '最大長を切り取る
            dr.Item(i) = commonCheck.CutMaxLength(strLine.Split(",")(i).Trim, commonCheck.HANBAI_MAX_LENGTH(i))
        Next
        '行号
        dr.Item(CsvInputCheck.HANBAI_FIELD_COUNT) = CStr(intLineNo)

        dtHanbaiKakakuError.Rows.Add(dr)

    End Sub
    ''' <summary>前0埋め変換</summary>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    Private Sub SetAitesakiCd(ByRef strLine As String)

        Dim arrLine() As String = Split(strLine, ",")

        Select Case arrLine(1)
            Case "1"
                arrLine(2) = Right("00000" & arrLine(2).Trim, 5)
            Case "5"
                If arrLine(2).Length < 4 Then
                    arrLine(2) = Right("0000" & arrLine(2).Trim, 4)
                End If
            Case "7"
                If arrLine(2).Length < 4 Then
                    arrLine(2) = Right("0000" & arrLine(2).Trim, 4)
                End If
        End Select
        strLine = String.Join(",", arrLine)

    End Sub
    ''' <summary>存在チェック</summary>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    Private Function ChkMstSonZai(ByVal strLine As String) As Boolean

        '相手先(種別・コード)
        If Not HanbaiKakakuMasterDA.SelAiteSaki(strLine.Split(",")(1).Trim, strLine.Split(",")(2).Trim) Then
            Return False
        End If

        '商品コード
        If Not HanbaiKakakuMasterDA.SelSyouhinCd(strLine.Split(",")(4).Trim) Then
            Return False
        End If

        '調査方法NO
        If Not HanbaiKakakuMasterDA.SelTysHouhou(strLine.Split(",")(6).Trim) Then
            Return False
        End If

        Return True
    End Function
    ''' <summary>更新･追加を判断する</summary>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    ''' <param name="dtHanbaiKakakuOk">販売価格データ</param> 
    Private Sub SetKousinKbn(ByVal strLine As String, ByRef dtHanbaiKakakuOk As Data.DataTable)

        '販売価格マスタ存在チェック
        If HanbaiKakakuMasterDA.SelHanbaiKakaku(strLine.Split(",")(1).Trim, strLine.Split(",")(2).Trim, strLine.Split(",")(4).Trim, strLine.Split(",")(6).Trim) Then
            '販売価格マスタが存在する場合、更新区分を設定する
            Call SetHanbaiKakakuDataRow(strLine, dtHanbaiKakakuOk, "1")
        Else
            'CSVファイルに行前のデータがある場合
            If dtHanbaiKakakuOk.Rows.Count > 0 Then
                '存在フラグ
                Dim blnSonzaiFlg As Boolean = False

                '行前データに主キーがあるかどうかを判断する
                For i As Integer = 0 To dtHanbaiKakakuOk.Rows.Count - 1
                    If dtHanbaiKakakuOk.Rows(i).Item("key").ToString.Trim.Equals(GetHanbaiKakakuKey(strLine)) Then
                        blnSonzaiFlg = True
                        Exit For
                    End If
                Next

                If blnSonzaiFlg Then
                    '行前データに主キーがある場合、更新区分を設定する
                    Call SetHanbaiKakakuDataRow(strLine, dtHanbaiKakakuOk, "1")
                Else
                    '行前データに主キーがない場合、追加区分を設定する
                    Call SetHanbaiKakakuDataRow(strLine, dtHanbaiKakakuOk, "0")
                End If
            Else
                '販売価格マスタが存在しない、CSVファイルに行前のデータがない場合、追加区分を設定する
                Call SetHanbaiKakakuDataRow(strLine, dtHanbaiKakakuOk, "0")
            End If

        End If

    End Sub
    ''' <summary>販売価格データのkeyを設定する</summary>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    ''' <returns>該当行のデータの主キー</returns>
    Public Function GetHanbaiKakakuKey(ByVal strLine As String) As String

        Return strLine.Split(",")(1).Trim & "," & strLine.Split(",")(2).Trim & "," & strLine.Split(",")(4).Trim & "," & strLine.Split(",")(6).Trim

    End Function
    ''' <summary>販売価格データを作成する</summary>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    ''' <param name="dtHanbaiKakakuOk">販売価格データ</param>
    ''' <param name="strInsUpdFlg">更新・追加区分</param>
    Public Sub SetHanbaiKakakuDataRow(ByVal strLine As String, _
                                      ByRef dtHanbaiKakakuOk As Data.DataTable, _
                                      ByVal strInsUpdFlg As String)

        Dim dr As Data.DataRow
        dr = dtHanbaiKakakuOk.NewRow
        dr.Item("key") = GetHanbaiKakakuKey(strLine)                '主キー
        dr.Item("ins_upd_flg") = strInsUpdFlg                       '更新・追加区分
        dr.Item("aitesaki_syubetu") = strLine.Split(",")(1).Trim    '相手先種別
        dr.Item("aitesaki_cd") = strLine.Split(",")(2).Trim         '相手先コード
        dr.Item("syouhin_cd") = strLine.Split(",")(4).Trim          '商品コード
        dr.Item("tys_houhou_no") = strLine.Split(",")(6).Trim       '調査方法NO
        dr.Item("torikesi") = IIf(strLine.Split(",")(8).Trim.Equals(String.Empty), "0", strLine.Split(",")(8).Trim) '取消
        dr.Item("koumuten_seikyuu_gaku") = strLine.Split(",")(9).Trim               '工務店請求金額
        dr.Item("koumuten_seikyuu_gaku_henkou_flg") = strLine.Split(",")(10).Trim   '工務店請求金額変更FLG
        dr.Item("jitu_seikyuu_gaku") = strLine.Split(",")(11).Trim                  '実請求金額
        dr.Item("jitu_seikyuu_gaku_henkou_flg") = strLine.Split(",")(12).Trim       '実請求金額変更FLG
        dr.Item("koukai_flg") = strLine.Split(",")(13).Trim                         '公開フラグ

        dtHanbaiKakakuOk.Rows.Add(dr)

    End Sub
    ''' <summary>CSVファイルを取込する</summary>
    ''' <param name="dtHanbaiKakakuOk">販売価格データ</param>
    ''' <param name="dtHanbaiKakakuError">販売価格エラーデータ</param>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <param name="strNyuuryokuFileMei">入力ファイル名</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <returns>成功・失敗区分</returns>
    Public Function CsvFileUpload(ByVal dtHanbaiKakakuOk As Data.DataTable, _
                                  ByVal dtHanbaiKakakuError As Data.DataTable, _
                                  ByVal strUploadDate As String, _
                                  ByVal strUserId As String, _
                                  ByVal strNyuuryokuFileMei As String, _
                                  ByVal strEdiJouhouSakuseiDate As String) As Boolean

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                '販売価格マスタを登録する
                If dtHanbaiKakakuOk.Rows.Count > 0 Then
                    If Not HanbaiKakakuMasterDA.InsUpdHanbaiKakaku(dtHanbaiKakakuOk, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                '販売価格エラー情報テーブルを登録する
                If dtHanbaiKakakuError.Rows.Count > 0 Then
                    If Not HanbaiKakakuMasterDA.InsHanbaiKakakuError(dtHanbaiKakakuError, strUploadDate, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                'アップロード管理テーブルを登録する
                If Not HanbaiKakakuMasterDA.InsUploadKanri(strUploadDate, strNyuuryokuFileMei, strEdiJouhouSakuseiDate, CInt(IIf(dtHanbaiKakakuError.Rows.Count, 1, 0)), strUserId) Then
                    Throw New ApplicationException
                End If
                scope.Complete()

            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using

        Return True

    End Function
    ''' <summary>販売価格エラー情報を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>販売価格エラーデータテーブル</returns>
    Public Function GetHanbaiKakakuErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) _
                As HanbaiKakakuMasterDataSet.HanbaiKakakuErrTableDataTable
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuErr(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function
    ''' <summary>販売価格エラー件数を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>販売価格エラー件数</returns>
    Public Function GetHanbaiKakakuErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As String
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuErrCount(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function
    ''' <summary>販売価格エラーCSV情報を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>販売価格エラーCSVデータテーブル</returns>
    Public Function SelHanbaiKakakuErrCsv(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) _
                    As HanbaiKakakuMasterDataSet.HanbaiKakakuErrCSVTableDataTable
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuErrCsv(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function

    '====================2011/05/16 車龍 仕様変更 追加 開始↓===========================
    ''' <summary>販売価格マスタ個別設定データを取得する</summary>
    Public Function GetHanbaiKakakuKobeituSettei(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strTyousaHouhouNo As String) As Data.DataTable

        Return HanbaiKakakuMasterDA.SelHanbaiKakakuKobeituSettei(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strTyousaHouhouNo)

    End Function

    ''' <summary>販売価格マスタ個別設定の存在チェツク</summary>
    Public Function CheckSonzai(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strTyousaHouhouNo As String) As Data.DataTable

        Return HanbaiKakakuMasterDA.CheckSonzai(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strTyousaHouhouNo)

    End Function

    ''' <summary>販売価格マスタ個別設定の登録</summary>
    Public Function SetHanbaiKakakuKobeituSettei(ByVal dtHanbaiKakakuOk As Data.DataTable, ByVal strUserId As String) As Boolean

        Return HanbaiKakakuMasterDA.InsUpdHanbaiKakaku(dtHanbaiKakakuOk, strUserId)

    End Function

    '====================2011/05/16 車龍 仕様変更 追加 終了↑===========================


End Class
