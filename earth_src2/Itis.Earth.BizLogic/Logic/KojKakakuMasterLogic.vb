Imports System.Transactions
Imports Itis.Earth.DataAccess
Public Class KojKakakuMasterLogic
    ''' <summary>工事価格マスタクラスのインスタンス生成 </summary>
    Private KojKakakuMasterDA As New KojKakakuMasterDataAccess
    Public Function GetKojKakakuInfoCount(ByVal dtInfo As DataTable) As Integer
        Return KojKakakuMasterDA.SelKojKakakuInfoCount(dtInfo)
    End Function
    Public Function GetKojKakakuInfo(ByVal dtInfo As DataTable) As Data.DataTable
        Return KojKakakuMasterDA.SelKojKakakuSeiteInfo(dtInfo)
    End Function
    Public Function GetKojKakakuCSVInfo(ByVal dtInfo As DataTable) As Data.DataTable
        Return KojKakakuMasterDA.SelKojKakakuCSVInfo(dtInfo)
    End Function
    ''' <summary>商品を取得する</summary>
    ''' <returns>商品データテーブル</returns>
    Public Function GetSyouhin(Optional ByVal syohuinCd As String = "") As Data.DataTable
        Return KojKakakuMasterDA.SelSyouhin(syohuinCd)
    End Function
    ''' <summary>アップロード管理を取得する</summary>
    ''' <returns>アップロード管理データテーブル</returns>
    Public Function GetUploadKanri() As Data.DataTable
        Return KojKakakuMasterDA.SelUploadKanri()
    End Function
    ''' <summary>アップロード管理の件数を取得する</summary>
    ''' <returns>アップロード管理の件数</returns>
    Public Function GetUploadKanriCount() As Integer
        Return KojKakakuMasterDA.SelUploadKanriCount()
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
        Dim dtKojKakakuOk As New Data.DataTable               '工事価格マスタ
        Dim dtKojKakakuError As New Data.DataTable           '工事価格エラー
        Dim strUploadDate As String                             'アップロード日時
        Dim ninsyou As New Ninsyou()
        Dim strUserId As String                                 'ユーザーID
        Dim strMaxUpload As String                              'CSV取込の上限件数
        Dim commonCheck As New CsvInputCheck
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
        '工事価格マスタを作成する
        Call SetKojKakakuOk(dtKojKakakuOk)
        '工事価格エラーテーブルを作成する
        Call SetKojKakakuError(dtKojKakakuError)

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
            strEdiJouhouSakuseiDate = Right("          " & strCsvLine(1).Split(",")(0), 10)

            'CSVファイルをチェック
            For i As Integer = 1 To strCsvLine.Length - 1
                strReadLine = strCsvLine(i)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                    'EDI情報作成日が前0埋め変換
                    If strReadLine.Split(",")(0).Length < 10 Then
                        Dim arrLine() As String = Split(strReadLine, ",")
                        arrLine(0) = Right("          " & arrLine(0).Trim, 10)
                        strReadLine = String.Join(",", arrLine)
                    End If
                    '桁数埋め変換
                    If strReadLine.Split(",").Length < CsvInputCheck.KOJ_FIELD_COUNT Then
                        strReadLine = strReadLine & StrDup(CsvInputCheck.KOJ_FIELD_COUNT - strReadLine.Split(",").Length, ",")
                    End If
                    'フィールド数チェック
                    If Not commonCheck.ChkFieldCount(strReadLine.Split(",").Length, CsvInputCheck.KOJ) Then
                        'エラーデータを作成する
                        Call SetKojKakakuErrData(i + 1, strReadLine, dtKojKakakuError)
                        Continue For
                    End If
                    '項目最大長チェック
                    If Not commonCheck.ChkMaxLength(strReadLine, CsvInputCheck.KOJ) Then
                        'エラーデータを作成する
                        Call SetKojKakakuErrData(i + 1, strReadLine, dtKojKakakuError)
                        Continue For
                    End If
                    '禁則文字チェック
                    If Not commonCheck.ChkKinjiMoji(strReadLine) Then
                        'エラーデータを作成する
                        Call SetKojKakakuErrData(i + 1, strReadLine, dtKojKakakuError)
                        Continue For
                    End If
                    '数値型項目チェック
                    If Not commonCheck.ChkSuuti(strReadLine, CsvInputCheck.KOJ) Then
                        'エラーデータを作成する
                        Call SetKojKakakuErrData(i + 1, strReadLine, dtKojKakakuError)
                        Continue For
                    End If
                    '必須チェック
                    If Not commonCheck.ChkNotNull(strReadLine, CsvInputCheck.KOJ) Then
                        'エラーデータを作成する
                        Call SetKojKakakuErrData(i + 1, strReadLine, dtKojKakakuError)
                        Continue For
                    End If
                    '相手先コードが前0埋め変換
                    SetAitesakiCd(strReadLine)

                    '存在チェック
                    If Not ChkMstSonZai(strReadLine) Then
                        'エラーデータを作成する
                        Call SetKojKakakuErrData(i + 1, strReadLine, dtKojKakakuError)
                        Continue For
                    End If
                    '更新条件チェック
                    If Not commonCheck.ChkKousinJyouken(strReadLine, CsvInputCheck.KOJ) Then
                        Continue For
                    End If
                    '更新･追加を判断する
                    Call SetKousinKbn(strReadLine, dtKojKakakuOk)
                End If
            Next
            'エラー有無フラグを設定する
            If dtKojKakakuError.Rows.Count > 0 Then
                strUmuFlg = "1"
            End If
            'CSVファイルを取込
            If Not CsvFileUpload(dtKojKakakuOk, dtKojKakakuError, strUploadDate, strUserId, strNyuuryokuFileMei, strEdiJouhouSakuseiDate) Then
                Return Messages.Instance.MSG050E
            End If
        Catch ex As Exception
            Return Messages.Instance.MSG049E
        End Try

        Return String.Empty
    End Function
    ''' <summary>工事価格テーブルを作成する</summary>
    ''' <param name="dtKojKakakuOk">工事価格テーブル</param>
    Public Sub SetKojKakakuOk(ByRef dtKojKakakuOk As Data.DataTable)

        Dim dc As Data.DataColumn

        dc = New Data.DataColumn
        dc.ColumnName = "key"               'キー(相手先種別 + 相手先コード + 商品コード +工事会社コード )
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "ins_upd_flg"       '追加更新FLG(0:追加; 1:更新)
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"  '相手先種別
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"       '相手先コード
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"        '商品コード
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_cd"     '工事会社コード
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_jigyousyo_cd"     '工事会社事業所コード
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"          '取消
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "uri_gaku"  '売上金額
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_seikyuu_umu"   '工事会社請求有無
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "seikyuu_umu"  '請求有無
        dtKojKakakuOk.Columns.Add(dc)
       
    End Sub
    ''' <summary>工事価格エラーテーブルを作成する</summary>
    ''' <param name="dtKojKakakuError">工事価格エラーテーブル</param>
    Public Sub SetKojKakakuError(ByRef dtKojKakakuError As Data.DataTable)

        Dim dc As Data.DataColumn
        dc = New Data.DataColumn
        dc.ColumnName = "edi_jouhou_sakusei_date"           'EDI情報作成日
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"                  '相手先種別
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"                       '相手先コード
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_mei"                      '相手先名
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"                        '商品コード
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_mei"                       '商品名
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "koj_cd"                     '工事会社コード
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_mei"                    '工事会社名
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"                          '取消
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "uri_gaku"                          '売上金額
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_seikyuu_umu"            '工事会社請求有無
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "seikyuu_umu"                       '請求有無
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "gyou_no"                           '行NO
        dtKojKakakuError.Columns.Add(dc)
    End Sub
    ''' <summary>工事価格エラーデータを作成する</summary>
    ''' <param name="intLineNo">CSVファイルの該当行の行NO</param>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    ''' <param name="dtKojKakakuError">工事価格エラーデータ</param>
    Private Sub SetKojKakakuErrData(ByVal intLineNo As Integer, _
                                       ByVal strLine As String, _
                                       ByRef dtKojKakakuError As Data.DataTable)

        Dim intMaxCount As Integer
        Dim commonCheck As New CsvInputCheck
        Dim dr As Data.DataRow
        dr = dtKojKakakuError.NewRow

        If strLine.Split(",").Length < CsvInputCheck.KOJ_FIELD_COUNT Then
            intMaxCount = strLine.Split(",").Length
        Else
            intMaxCount = CsvInputCheck.KOJ_FIELD_COUNT
        End If

        For i As Integer = 0 To intMaxCount - 1
            '最大長を切り取る
            dr.Item(i) = commonCheck.CutMaxLength(strLine.Split(",")(i).Trim, commonCheck.KOJ_MAX_LENGTH(i))
        Next
        '行号
        dr.Item(CsvInputCheck.KOJ_FIELD_COUNT) = CStr(intLineNo)

        dtKojKakakuError.Rows.Add(dr)

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
        If arrLine(6).Trim.ToUpper <> "ALLAL" Then
            arrLine(6) = Right("000000" & arrLine(6).Trim, 6)
        End If
        strLine = String.Join(",", arrLine)

    End Sub
    ''' <summary>存在チェック</summary>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    Private Function ChkMstSonZai(ByVal strLine As String) As Boolean

        Dim HanbaiKakakuMasterDA As New HanbaiKakakuMasterDataAccess


        '相手先(種別・コード)
        If Not HanbaiKakakuMasterDA.SelAiteSaki(strLine.Split(",")(1).Trim, strLine.Split(",")(2).Trim) Then
            Return False
        End If

        '商品コード
        If GetSyouhin(strLine.Split(",")(4).Trim).Rows.Count = 0 Then
            Return False
        End If

        '工事会社NO
        If strLine.Split(",")(6).Trim <> "ALLAL" Then
            If GetKojKaisyaKensaku(strLine.Split(",")(6).Trim).Rows.Count <= 0 Then
                Return False
            End If
        End If


        Return True
    End Function
    Public Function GetKojKaisyaKensaku(ByVal strCd As String) As DataTable

        Return KojKakakuMasterDA.SelKojKaisyaKensaku(strCd)

    End Function

    ''' <summary>更新･追加を判断する</summary>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    ''' <param name="dtKojKakakuOk">工事価格データ</param> 
    Private Sub SetKousinKbn(ByVal strLine As String, ByRef dtKojKakakuOk As Data.DataTable)

        '工事価格マスタ存在チェック
        If KojKakakuMasterDA.SelKojKakaku(strLine.Split(",")(1).Trim, strLine.Split(",")(2).Trim, strLine.Split(",")(4).Trim, strLine.Split(",")(6).Trim) Then
            '工事価格マスタが存在する場合、更新区分を設定する
            Call SetKojKakakuDataRow(strLine, dtKojKakakuOk, "1")
        Else
            'CSVファイルに行前のデータがある場合
            If dtKojKakakuOk.Rows.Count > 0 Then
                '存在フラグ
                Dim blnSonzaiFlg As Boolean = False

                '行前データに主キーがあるかどうかを判断する
                For i As Integer = 0 To dtKojKakakuOk.Rows.Count - 1
                    If dtKojKakakuOk.Rows(i).Item("key").ToString.Trim.Equals(GetKojKakakuKey(strLine)) Then
                        blnSonzaiFlg = True
                        Exit For
                    End If
                Next

                If blnSonzaiFlg Then
                    '行前データに主キーがある場合、更新区分を設定する
                    Call SetKojKakakuDataRow(strLine, dtKojKakakuOk, "1")
                Else
                    '行前データに主キーがない場合、追加区分を設定する
                    Call SetKojKakakuDataRow(strLine, dtKojKakakuOk, "0")
                End If
            Else
                '工事価格マスタが存在しない、CSVファイルに行前のデータがない場合、追加区分を設定する
                Call SetKojKakakuDataRow(strLine, dtKojKakakuOk, "0")
            End If

        End If

    End Sub
    ''' <summary>工事価格データを作成する</summary>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    ''' <param name="dtKojKakakuOk">工事価格データ</param>
    ''' <param name="strInsUpdFlg">更新・追加区分</param>
    Public Sub SetKojKakakuDataRow(ByVal strLine As String, _
                                      ByRef dtKojKakakuOk As Data.DataTable, _
                                      ByVal strInsUpdFlg As String)

        Dim dr As Data.DataRow
        dr = dtKojKakakuOk.NewRow
        dr.Item("key") = GetKojKakakuKey(strLine)                '主キー
        dr.Item("ins_upd_flg") = strInsUpdFlg                       '更新・追加区分
        dr.Item("aitesaki_syubetu") = strLine.Split(",")(1).Trim    '相手先種別
        dr.Item("aitesaki_cd") = strLine.Split(",")(2).Trim         '相手先コード
        dr.Item("syouhin_cd") = strLine.Split(",")(4).Trim          '商品コード
        dr.Item("koj_gaisya_cd") = Left(Right("       " & strLine.Split(",")(6).Trim, 7), 5).Trim        '工事会社コード
        dr.Item("koj_gaisya_jigyousyo_cd") = Right(strLine.Split(",")(6).Trim, 2)     '工事会社事業所コード
        dr.Item("torikesi") = IIf(strLine.Split(",")(8).Trim.Equals(String.Empty), "0", strLine.Split(",")(8).Trim) '取消
        dr.Item("uri_gaku") = strLine.Split(",")(9).Trim               '売上金額
        dr.Item("koj_gaisya_seikyuu_umu") = strLine.Split(",")(10).Trim   '工事会社請求有無
        dr.Item("seikyuu_umu") = strLine.Split(",")(11).Trim                  '請求有無


        dtKojKakakuOk.Rows.Add(dr)

    End Sub
    ''' <summary>工事価格データのkeyを設定する</summary>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    ''' <returns>該当行のデータの主キー</returns>
    Public Function GetKojKakakuKey(ByVal strLine As String) As String

        Return strLine.Split(",")(1).Trim & "," & strLine.Split(",")(2).Trim & "," & strLine.Split(",")(4).Trim & "," & strLine.Split(",")(6).Trim

    End Function

    ''' <summary>CSVファイルを取込する</summary>
    ''' <param name="dtKojKakakuOk">工事価格データ</param>
    ''' <param name="dtKojKakakuError">工事価格エラーデータ</param>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <param name="strNyuuryokuFileMei">入力ファイル名</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <returns>成功・失敗区分</returns>
    Public Function CsvFileUpload(ByVal dtKojKakakuOk As Data.DataTable, _
                                  ByVal dtKojKakakuError As Data.DataTable, _
                                  ByVal strUploadDate As String, _
                                  ByVal strUserId As String, _
                                  ByVal strNyuuryokuFileMei As String, _
                                  ByVal strEdiJouhouSakuseiDate As String) As Boolean

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                '工事価格マスタを登録する
                If dtKojKakakuOk.Rows.Count > 0 Then
                    If Not KojKakakuMasterDA.InsUpdKojKakaku(dtKojKakakuOk, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                '工事価格エラー情報テーブルを登録する
                If dtKojKakakuError.Rows.Count > 0 Then
                    If Not KojKakakuMasterDA.InsKojKakakuError(dtKojKakakuError, strUploadDate, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                'アップロード管理テーブルを登録する
                If Not KojKakakuMasterDA.InsUploadKanri(strUploadDate, strNyuuryokuFileMei, strEdiJouhouSakuseiDate, CInt(IIf(dtKojKakakuError.Rows.Count, 1, 0)), strUserId) Then
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
    ''' <summary>工事価格エラー情報を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>工事価格エラーデータテーブル</returns>
    Public Function GetKojKakakuErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) _
                As DataTable
        Return KojKakakuMasterDA.SelKojKakakuErr(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function
    ''' <summary>工事価格エラー件数を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>工事価格エラー件数</returns>
    Public Function GetKojKakakuErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As Integer
        Return KojKakakuMasterDA.SelKojKakakuErrCount(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function

    ''' <summary>工事価格エラーCSV情報を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>工事価格エラーCSVデータテーブル</returns>
    Public Function SelKojKakakuErrCsv(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) _
                    As DataTable
        Return KojKakakuMasterDA.SelKojKakakuErrCsv(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function

    ''' <summary>工事価格マスタ個別設定データを取得する</summary>
    Public Function GetHanbaiKakakuKobeituSettei(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strKojKaisyaCd As String) As Data.DataTable

        Dim dtParam As New DataTable
        Dim row As DataRow = dtParam.NewRow
        dtParam.Columns.Add(New DataColumn("aitesaki_syubetu", GetType(String)))
        dtParam.Columns.Add(New DataColumn("aitesaki_cd_from", GetType(String)))
        dtParam.Columns.Add(New DataColumn("syouhin_cd", GetType(String)))
        dtParam.Columns.Add(New DataColumn("kojkaisya_cd", GetType(String)))
        dtParam.Columns.Add(New DataColumn("aitesaki_cd_to", GetType(String)))
        dtParam.Columns.Add(New DataColumn("torikesi", GetType(String)))
        dtParam.Columns.Add(New DataColumn("torikesi_aitesaki", GetType(String)))

        '相手先種別を設定する
        row.Item("aitesaki_syubetu") = strAiteSakiSyubetu
        '相手先コードFROMを設定する
        row.Item("aitesaki_cd_from") = strAiteSakiCd
        '商品コードを設定する
        row.Item("syouhin_cd") = strSyouhinCd
        '工事会社
        row.Item("kojkaisya_cd") = strKojKaisyaCd

        row.Item("aitesaki_cd_to") = ""
        row.Item("torikesi") = ""
        row.Item("torikesi_aitesaki") = ""
        dtParam.Rows.Add(row)
        Return KojKakakuMasterDA.SelKojKakakuKobeituSettei(dtParam)

    End Function
    ''' <summary>工事価格マスタ個別設定の存在チェツク</summary>
    Public Function CheckSonzai(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strKojKaisyaCd As String) As Data.DataTable

        Return KojKakakuMasterDA.CheckSonzai(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strKojKaisyaCd)

    End Function

    ''' <summary>工事価格マスタ個別設定の登録</summary>
    Public Function SetKojKakakuKobeituSettei(ByVal dtKojKakakuOk As Data.DataTable, ByVal strUserId As String) As Boolean

        Return KojKakakuMasterDA.InsUpdKojKakaku(dtKojKakakuOk, strUserId)

    End Function

End Class
