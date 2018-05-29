Imports Itis.Earth.DataAccess
Imports System.Transactions

Public Class TokubetuTaiouMasterLogic

    Private tokubetuTaiouMasterDA As New TokubetuTaiouMasterDataAccess
    Private commonCheck As New CsvInputCheck
    Private ninsyou As New Ninsyou()
    Private userId As String = Ninsyou.GetUserID()
    Private InputDate As String
    'CSVアップロード上限件数
    Private CsvInputMaxLineCount As String = CStr(System.Configuration.ConfigurationSettings.AppSettings("CsvInputMaxLineCount"))

    ''' <summary>商品コードを取得する</summary>
    ''' <returns>商品コードデータテーブル</returns>
    ''' <history>2011/03/03　ジン登閣(大連情報システム部)　新規作成</history>
    Public Function GetSyouhinCd() As Data.DataTable

        Return tokubetuTaiouMasterDA.SelSyouhinCd()

    End Function

    ''' <summary>調査方法を取得する</summary>
    ''' <returns>調査方法データテーブル</returns>
    ''' <history>2011/03/03　ジン登閣(大連情報システム部)　新規作成</history>
    Public Function GetTyousaHouhou() As Data.DataTable

        Return tokubetuTaiouMasterDA.SelTyousaHouhou()

    End Function

    ''' <summary>加盟店商品調査方法特別対応情報を取得する</summary>
    ''' <returns>加盟店商品調査方法特別対応情報データテープル</returns>
    ''' <remarks></remarks>
    Public Function GetTokubetuTaiouJyouhou(ByVal dtParamlist As Dictionary(Of String, String)) As Data.DataTable

        Return tokubetuTaiouMasterDA.SelTokubetuTaiouJyouhou(dtParamlist)

    End Function

    ''' <summary>相手先種別情報を取得</summary>
    ''' <returns>相手先種別情報データテープル</returns>
    ''' <remarks></remarks>
    Public Function GetAitesakiSyubetuList() As Data.DataTable

        Return tokubetuTaiouMasterDA.SelAitesakiSyubetuList()

    End Function

    ''' <summary>加盟店商品調査方法特別対応全部検索件数を取得する</summary>
    ''' <returns>加盟店商品調査方法特別対応全部検索件数</returns>
    Public Function GetTokubetuTaiouCount(ByVal dtParamlist As Dictionary(Of String, String)) As Integer

        Return tokubetuTaiouMasterDA.SelTokubetuTaiouCount(dtParamlist)

    End Function

    ''' <summary>加盟店商品調査方法特別対応マスタ検索結果をCSVでダウンロード</summary>
    ''' <returns>加盟店商品調査方法特別対応マスタ検索結果をCSVでダウンロードテーブル</returns>
    Public Function GetTokubetuTaiouJyouhouCSV(ByVal dtParamlist As Dictionary(Of String, String)) As Data.DataTable

        Return tokubetuTaiouMasterDA.SelTokubetuTaiouJyouhouCSV(dtParamlist)

    End Function


    ''' <summary>未設定も含む加盟店商品調査方法特別対応マスタCSVデータを取得</summary>
    ''' <returns>未設定も含む加盟店商品調査方法特別対応マスタCSVテーブル</returns>
    Public Function GetTokubetuTaiouCSV(ByVal dtParamlist As Dictionary(Of String, String)) As Data.DataTable

        Return tokubetuTaiouMasterDA.SelTokubetuTaiouCSV(dtParamlist)

    End Function

    ''' <summary>未設定も含む加盟店商品調査方法特別対応マスタCSVデータ件数を取得</summary>
    ''' <returns>未設定も含む加盟店商品調査方法特別対応マスタCSVデータ件数</returns>
    Public Function GetTokubetuTaiouCSVCount(ByVal dtParamlist As Dictionary(Of String, String)) As Long

        Return tokubetuTaiouMasterDA.SelTokubetuTaiouCSVCount(dtParamlist)

    End Function

    ''' <summary>アップロード管理を取得する</summary>
    ''' <returns>アップロード管理データテーブル</returns>
    Public Function GetInputKanri() As Data.DataTable

        Return tokubetuTaiouMasterDA.SelInputKanri()

    End Function

    ''' <summary>アップロード管理の件数を取得する</summary>
    ''' <returns>アップロード管理の件数データテーブル</returns>
    Public Function GetInputKanriCount() As Integer

        Return tokubetuTaiouMasterDA.SelInputKanriCount()

    End Function

    ''' <summary>相手先名を取得する</summary>
    ''' <returns>相手先名データテーブル</returns>
    Public Function GetAitesakiMei(ByVal intAitesakiSyubetu As Integer, ByVal AitesakiCd As String, ByVal strTorikesiAitesaki As String) As Data.DataTable

        Return tokubetuTaiouMasterDA.SelAitesakiMei(intAitesakiSyubetu, AitesakiCd, strTorikesiAitesaki)

    End Function

    ''' <summary>CSVファイルをチェックする</summary>
    ''' <returns>CSVファイルをチェックする</returns>
    Public Function ChkCsvFile(ByVal fupId As Web.UI.WebControls.FileUpload, ByRef strUmuFlg As String) As String

        Dim myStream As IO.Stream                           '入出力ストリーム
        Dim myReader As IO.StreamReader                     'ストリームリーダー
        Dim strReadLine As String                           '取込ファイル読込み行
        Dim intLineCount As Integer = 0                     'ライン数
        Dim strCsvLine() As String                          'CSVファイル内容
        Dim strFileMei As String                            'CSVファイル名
        Dim strEdiJouhouSakuseiDate As String = String.Empty 'EDI情報作成日
        Dim dtOk As New Data.DataTable                      '原価マスタ
        Dim dtError As New Data.DataTable                   '原価エラー

        '加盟店商品調査方法特別対応マスタを作成
        CreateOkDataTable(dtOk)

        '加盟店商品調査方法特別対応エラーテーブルを作成
        CreateErrorDataTable(dtError)

        'システムDate
        InputDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")

        'CSVファイル名
        strFileMei = commonCheck.CutMaxLength(fupId.FileName, 128)

        '入出力ストリーム
        myStream = fupId.FileContent

        'ストリームリーダー
        myReader = New IO.StreamReader(myStream, System.Text.Encoding.GetEncoding(932))

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
                    If i > CsvInputMaxLineCount Then
                        Return String.Format(Messages.Instance.MSG040E, CStr(CsvInputMaxLineCount))
                    ElseIf i < 1 Then
                        Return String.Format(Messages.Instance.MSG048E)
                    Else
                        Exit For
                    End If
                End If
            Next

            'EDI情報作成日
            strEdiJouhouSakuseiDate = Right("0" & strCsvLine(1).Split(",")(0).Trim, 10)
            
            'CSVファイルをチェック
            For i As Integer = 1 To strCsvLine.Length - 1              
                strReadLine = strCsvLine(i)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then

                    If strReadLine.Split(",")(0).Length < 10 Then
                        strReadLine = "0" & strReadLine
                    End If

                    'カンマを追加
                    If strReadLine.Split(",").Length < CsvInputCheck.TAIOU_FIELD_COUNT Then
                        strReadLine = strReadLine & StrDup(CsvInputCheck.TAIOU_FIELD_COUNT - strReadLine.Split(",").Length, ",")
                    End If

                    'フィールド数チェック
                    If Not commonCheck.ChkFieldCount(strReadLine.Split(",").Length, CsvInputCheck.TAIOU) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '項目最大長チェック
                    If Not commonCheck.ChkMaxLength(strReadLine, CsvInputCheck.TAIOU) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '禁則文字チェック
                    If Not commonCheck.ChkKinjiMoji(strReadLine) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '数値型項目チェック
                    If Not commonCheck.ChkSuuti(strReadLine, CsvInputCheck.TAIOU) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '必須チェック
                    If Not commonCheck.ChkNotNull(strReadLine, CsvInputCheck.TAIOU) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    strReadLine = SetUmekusa(strReadLine)

                    '存在チェック
                    If Not Me.ChkSonZai(strReadLine) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '更新条件チェック
                    If Not commonCheck.ChkKousinJyouken(strReadLine, CsvInputCheck.TAIOU) Then

                        Continue For
                    End If

                    '合格データの処理
                    Call Me.OkLineSyori(strReadLine, dtOk)

                End If
            Next

            'エラー有無フラグを設定する
            If dtError.Rows.Count > 0 Then
                strUmuFlg = "1"
            End If

            'CSVファイルを取込
            If Not CsvFileInput(dtOk, dtError, InputDate, userId, strFileMei, strEdiJouhouSakuseiDate) Then
                Return Messages.Instance.MSG050E
            End If

        Catch ex As Exception
            Return Messages.Instance.MSG049E
        End Try

        Return String.Empty

    End Function

    ''' <summary>
    ''' 加盟店コードを"0"で埋める
    ''' </summary>
    Public Function SetUmekusa(ByVal StrLine As String) As String

        Dim arrLine() As String = StrLine.Split(",")

        If "1".Equals(arrLine(1).ToString) AndAlso Not String.IsNullOrEmpty(arrLine(2).ToString) Then
            arrLine(2) = Right("00000" & arrLine(2).Trim, 5)
        End If

        If "5".Equals(arrLine(1).ToString) AndAlso Not String.IsNullOrEmpty(arrLine(2).ToString) Then
            arrLine(2) = Right("00000" & arrLine(2).Trim, 4)
        End If

        If "7".Equals(arrLine(1).ToString) AndAlso Not String.IsNullOrEmpty(arrLine(2).ToString) Then
            arrLine(2) = Right("00000" & arrLine(2).Trim, 4)
        End If
        'If arrLine(1).ToString <> String.Empty Then
        '    arrLine(1) = Right("00000" & arrLine(1).Trim, 5)
        'End If

        Return String.Join(",", arrLine)

    End Function
    ''' <summary>CSVファイルを取込する</summary>
    ''' <returns>CSVファイルを取込する</returns>
    Public Function CsvFileInput(ByVal dtOk As Data.DataTable, ByVal dtError As Data.DataTable, ByVal InputDate As String, ByVal userId As String, ByVal strFileMei As String, ByVal strEdiJouhouSakuseiDate As String) As Boolean

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                '加盟店調査方法特別対応マスタを登録
                If dtOk.Rows.Count > 0 Then
                    If Not tokubetuTaiouMasterDA.InsUpdTokubetuTaiou(dtOk) Then
                        Throw New ApplicationException
                    End If

                End If

                '加盟店調査方法特別対応エラー情報テーブルを登録
                If dtError.Rows.Count > 0 Then
                    If Not tokubetuTaiouMasterDA.InsTokubetuTaiouError(dtError) Then
                        Throw New ApplicationException
                    End If
                End If

                'アップロード管理テーブルを登録
                If Not tokubetuTaiouMasterDA.InsInputKanri(InputDate, strFileMei, strEdiJouhouSakuseiDate, CInt(IIf(dtError.Rows.Count, 1, 0)), userId) Then
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

    ''' <summary>存在チェック</summary>
    Private Function ChkSonZai(ByVal intFieldCount As String) As Boolean

        ''加盟店コード
        'If Not tokubetuTaiouMasterDA.SelKameitenCd(intFieldCount.Split(",")(1).Trim) Then
        '    Return False
        'End If
        '相手先(種別・コード)
        If Not tokubetuTaiouMasterDA.SelAitesakiSyubetuInput(CInt(intFieldCount.Split(",")(1)), intFieldCount.Split(",")(2).Trim) Then
            Return False
        End If

        '商品コード
        If Not tokubetuTaiouMasterDA.SelSyouhinCd(intFieldCount.Split(",")(4).Trim) Then
            Return False
        End If

        '調査方法NO
        If Not tokubetuTaiouMasterDA.SelTyousahouhouNo(intFieldCount.Split(",")(6).Trim) Then
            Return False
        End If

        '特別対応コード
        If Not tokubetuTaiouMasterDA.SelTokubetuCd(intFieldCount.Split(",")(8).Trim) Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>OKライン処理</summary>
    Private Sub OkLineSyori(ByVal strLine As String, ByRef dtOk As Data.DataTable)

        'DB存在チェック
        If tokubetuTaiouMasterDA.SelTokubetuTaiouJyouhou(strLine.Split(",")(1).Trim, strLine.Split(",")(2).Trim, strLine.Split(",")(4).Trim, strLine.Split(",")(6).Trim, strLine.Split(",")(8).Trim) Then
            Me.SetOkDataRow(strLine, dtOk, "1")
        Else
            If dtOk.Rows.Count > 0 Then
                '存在フラグ
                Dim SonzaiFlg As Boolean = False

                For i As Integer = 0 To dtOk.Rows.Count - 1
                    If dtOk.Rows(i).Item("key").ToString.Trim.Equals(GetDataKey(strLine)) Then
                        SonzaiFlg = True
                        Exit For
                    End If
                Next
                If SonzaiFlg.Equals(True) Then
                    Call Me.SetOkDataRow(strLine, dtOk, "1")
                Else
                    Call Me.SetOkDataRow(strLine, dtOk, "0")
                End If
            Else
                Call Me.SetOkDataRow(strLine, dtOk, "0")
            End If

        End If

    End Sub

    ''' <summary>OKデータラインを追加</summary>
    Public Sub SetOkDataRow(ByVal strLine As String, ByRef dt As Data.DataTable, ByVal strInsUpdFlg As String)

        Dim dr As Data.DataRow
        dr = dt.NewRow
        dr.Item("key") = GetDataKey(strLine)
        dr.Item("ins_upd_flg") = strInsUpdFlg
        'dr.Item("kameiten_cd") = strLine.Split(",")(1).Trim
        dr.Item("aitesaki_syubetu") = strLine.Split(",")(1).Trim
        dr.Item("aitesaki_cd") = strLine.Split(",")(2).Trim
        dr.Item("syouhin_cd") = strLine.Split(",")(4).Trim
        dr.Item("tys_houhou_no") = strLine.Split(",")(6).Trim
        dr.Item("tokubetu_taiou_cd") = strLine.Split(",")(8).Trim
        dr.Item("torikesi") = strLine.Split(",")(10).Trim
        dr.Item("kasan_syouhin_cd") = strLine.Split(",")(11).Trim
        dr.Item("syokiti") = strLine.Split(",")(12).Trim
        dr.Item("uri_kasan_gaku") = strLine.Split(",")(13).Trim
        dr.Item("koumuten_kasan_gaku") = strLine.Split(",")(14).Trim
        dr.Item("add_login_user_id") = userId
        dr.Item("upd_login_user_id") = userId

        dt.Rows.Add(dr)
    End Sub

    ''' <summary>エラーライン処理</summary>
    Private Sub ErrorLineSyori(ByVal intLineNo As Integer, ByVal strLine As String, ByRef dtError As Data.DataTable)

        Dim intMaxCount As Integer

        Dim dr As Data.DataRow
        dr = dtError.NewRow

        '最大フィールド設定
        If strLine.Split(",").Length < CsvInputCheck.TAIOU_FIELD_COUNT Then
            intMaxCount = strLine.Split(",").Length
        Else
            intMaxCount = CsvInputCheck.TAIOU_FIELD_COUNT
        End If

        For i As Integer = 0 To intMaxCount - 1
            '最大長を切り取る
            dr.Item(i) = commonCheck.CutMaxLength(strLine.Split(",")(i).Trim, commonCheck.TAIOU_MAX_LENGTH(i))
        Next

        '行号
        dr.Item(CsvInputCheck.TAIOU_FIELD_COUNT + 1) = CStr(intLineNo)
        '処理日時
        dr.Item(CsvInputCheck.TAIOU_FIELD_COUNT + 2) = InputDate
        '登録ログインユーザID
        dr.Item(CsvInputCheck.TAIOU_FIELD_COUNT + 3) = userId

        dtError.Rows.Add(dr)

    End Sub

    ''' <summary>加盟店商品調査方法特別対応マスタを作成</summary>
    Public Sub CreateOkDataTable(ByRef dtOk As Data.DataTable)
        dtOk.Columns.Add("key")                     '--キー(相手先種別、相手先コード、商品コード、調査方法NO、特別対応コード)
        dtOk.Columns.Add("ins_upd_flg")             '--追加更新FLG(0:追加; 1:更新)
        'dtOk.Columns.Add("kameiten_cd")            '--加盟店コード
        dtOk.Columns.Add("aitesaki_syubetu")        '--相手先種別
        dtOk.Columns.Add("aitesaki_cd")             '--相手先コード
        dtOk.Columns.Add("syouhin_cd")              '--商品コード
        dtOk.Columns.Add("tys_houhou_no")           '--調査方法NO
        dtOk.Columns.Add("tokubetu_taiou_cd")       '--特別対応コード
        dtOk.Columns.Add("torikesi")                '--取消
        dtOk.Columns.Add("kasan_syouhin_cd")        '--金額加算商品コード
        dtOk.Columns.Add("syokiti")                 '--初期値
        dtOk.Columns.Add("uri_kasan_gaku")          '--実請求加算金額
        dtOk.Columns.Add("koumuten_kasan_gaku")     '--工務店請求加算金額
        dtOk.Columns.Add("add_login_user_id")       '--登録者ID
        dtOk.Columns.Add("add_datetime")            '--登録日時
        dtOk.Columns.Add("upd_login_user_id")       '--更新者ID
        dtOk.Columns.Add("upd_datetime")            '--更新日時
    End Sub

    ''' <summary>加盟店商品調査方法特別対応エラーマスタを作成</summary>
    Public Sub CreateErrorDataTable(ByRef dtError As Data.DataTable)
        dtError.Columns.Add("edi_jouhou_sakusei_date")      '--EDI情報作成日
        dtError.Columns.Add("aitesaki_syubetu")             '--相手先種別
        dtError.Columns.Add("aitesaki_cd")                  '--相手先コード
        dtError.Columns.Add("aitesaki_mei")                 '--相手先コード
        'dtError.Columns.Add("kameiten_cd")                 '--加盟店コード
        'dtError.Columns.Add("kameiten_mei")                '--加盟店名
        dtError.Columns.Add("syouhin_cd")                   '--商品コード
        dtError.Columns.Add("syouhin_mei")                  '--商品名
        dtError.Columns.Add("tys_houhou_no")                '--調査方法NO
        dtError.Columns.Add("tys_houhou")                   '--調査方法
        dtError.Columns.Add("tokubetu_taiou_cd")            '--特別対応コード
        dtError.Columns.Add("tokubetu_taiou_meisyou")       '--特別対応名称
        dtError.Columns.Add("torikesi")                     '--取消
        dtError.Columns.Add("kasan_syouhin_cd")             '--金額加算商品コード
        dtError.Columns.Add("kasan_syouhin_mei")            '--金額加算商品名
        dtError.Columns.Add("syokiti")                      '--初期値
        dtError.Columns.Add("uri_kasan_gaku")               '--実請求加算金額
        dtError.Columns.Add("koumuten_kasan_gaku")          '--工務店請求加算金額
        dtError.Columns.Add("gyou_no")                      '--行NO
        dtError.Columns.Add("syori_datetime")               '--処理日時
        dtError.Columns.Add("add_login_user_id")            '--登録者ID
        dtError.Columns.Add("add_datetime")                 '--登録日時
        dtError.Columns.Add("upd_login_user_id")            '--更新者ID
        dtError.Columns.Add("upd_datetime")                 '--更新日時
    End Sub

    ''' <summary>OKデータのkeyを設定</summary>
    Public Function GetDataKey(ByVal strLine As String) As String

        Return strLine.Split(",")(1).Trim & "," & strLine.Split(",")(2).Trim & "," & strLine.Split(",")(4).Trim & "," & strLine.Split(",")(6).Trim & "," & strLine.Split(",")(8).Trim & strLine.Split(",")(10).Trim

    End Function

    ''' <summary>加盟店商品調査方法特別対応マスタエラーデータを取得</summary>
    Public Function GetTokubetuTaiouError(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
        Return tokubetuTaiouMasterDA.SelTokubetuTaiouError(strEdidate, strSyoridate)
    End Function

    ''' <summary>加盟店商品調査方法特別対応マスタエラー件数を取得</summary>
    Public Function GetTokubetuTaiouErrorCount(ByVal strEdidate As String, ByVal strSyoridate As String) As Integer
        Return tokubetuTaiouMasterDA.SelTokubetuTaiouErrorCount(strEdidate, strSyoridate)
    End Function

    ''' <summary>加盟店商品調査方法特別対応マスタエラーCSVデータを取得</summary>
    Public Function GetTokubetuTaiouErrorCSV(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
        Return tokubetuTaiouMasterDA.SelTokubetuTaiouErrorCSV(strEdidate, strSyoridate)
    End Function


    ''' <summary>加盟店商品調査方法特別対応マスタ情報を取得(「系列・営業所・指定無しも対象チェックボックス」=チェックの場合)</summary>
    ''' <history>2012/05/23 車龍 407553の対応 追加</history>
    Public Function GetTokubetuTaiouNasiInfo(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable

        '戻り値
        Return tokubetuTaiouMasterDA.SelTokubetuTaiouNasiInfo(dtParamList)
    End Function

    ''' <summary>加盟店商品調査方法特別対応マスタ件数を取得「系列・営業所・指定無しも対象チェックボックス」=チェックの場合</summary>
    ''' <history>2012/05/23 車龍 407553の対応 追加</history>
    Public Function GetTokubetuTaiouNasiCount(ByVal dtParamList As Dictionary(Of String, String)) As Integer

        '戻り値
        Return tokubetuTaiouMasterDA.SelTokubetuTaiouNasiCount(dtParamList)
    End Function

    ''' <summary>
    ''' 旧様式名称取得
    ''' </summary>
    ''' <history>2013/05/20 楊双 407584の対応 追加</history>
    Public Function GetStyleMeisyou() As Object

        '戻り値
        Return tokubetuTaiouMasterDA.SelStyleMeisyou()

    End Function

End Class
