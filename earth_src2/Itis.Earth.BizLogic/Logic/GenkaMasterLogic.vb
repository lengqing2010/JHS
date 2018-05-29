Imports System.Transactions
Imports Itis.Earth.DataAccess

''' <summary>原価マスタ</summary>
''' <remarks>原価マスタ用機能を提供する</remarks>
''' <history>
''' <para>2011/02/24　車龍(大連情報システム部)　新規作成</para>
''' </history>
Public Class GenkaMasterLogic

    Private genkaMasterDA As New GenkaMasterDataAccess

    Private commonCheck As New CsvInputCheck
    Private ninsyou As New Ninsyou()
    Private userId As String = Ninsyou.GetUserID()
    Private InputDate As String
    'CSVアップロード上限件数
    Private CsvInputMaxLineCount As String = CStr(System.Configuration.ConfigurationSettings.AppSettings("CsvInputMaxLineCount"))


    ''' <summary>調査会社名を取得する</summary>
    ''' <returns>調査会社名データテーブル</returns>
    ''' <history>2011/03/07　車龍(大連情報システム部)　新規作成</history>
    Public Function GetTyousaKaisyaMei(ByVal strTyousaKaisyaCd As String, ByVal strJigyousyoCd As String, ByVal strKensakuTaisyouGai As String) As Data.DataTable

        Return genkaMasterDA.SelTyousaKaisyaMei(strTyousaKaisyaCd, strJigyousyoCd, strKensakuTaisyouGai)

    End Function

    ''' <summary>相手先種別を取得する</summary>
    ''' <returns>相手先種別データテーブル</returns>
    ''' <history>2011/02/24　車龍(大連情報システム部)　新規作成</history>
    Public Function GetAiteSakiSyubetu() As Data.DataTable

        Return genkaMasterDA.SelAiteSakiSyubetu()

    End Function

    ''' <summary>相手先名を取得する</summary>
    ''' <returns>相手先名データテーブル</returns>
    ''' <history>2011/03/07　車龍(大連情報システム部)　新規作成</history>
    Public Function GetAitesakiMei(ByVal intAitesakiSyubetu As Integer, ByVal AitesakiCd As String, ByVal strTorikesiAitesaki As String) As Data.DataTable

        Return genkaMasterDA.SelAitesakiMei(intAitesakiSyubetu, AitesakiCd, strTorikesiAitesaki)

    End Function

    ''' <summary>商品コードを取得する</summary>
    ''' <returns>商品コードデータテーブル</returns>
    ''' <history>2011/02/24　車龍(大連情報システム部)　新規作成</history>
    Public Function GetSyouhinCd() As Data.DataTable

        Return genkaMasterDA.SelSyouhinCd()

    End Function

    ''' <summary>調査方法を取得する</summary>
    ''' <returns>調査方法データテーブル</returns>
    ''' <history>2011/02/24　車龍(大連情報システム部)　新規作成</history>
    Public Function GetTyousaHouhou() As Data.DataTable

        Return genkaMasterDA.SelTyousaHouhou()

    End Function

    ''' <summary>原価情報を取得する</summary>
    ''' <returns>原価情報データテーブル</returns>
    ''' <history>2011/02/25　車龍(大連情報システム部)　新規作成</history>
    Public Function GetGenkaJyouhou(ByVal strKensakuCount As String, ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As GenkaMasterDataSet.GenkaInfoTableDataTable

        Return genkaMasterDA.SelGenkaJyouhou(strKensakuCount, strTysKaisyaCd, strAtesakiSyubetu, strAtesakiCdFrom, strAtesakiCdTo, strSyouhinCd, strHouhouCd, blnKensakuTaisyouGai, blnAitesakiTaisyouGai)

    End Function

    ''' <summary>原価情報件数を取得する</summary>
    ''' <returns>原価情報データテーブル</returns>
    ''' <history>2011/02/25　車龍(大連情報システム部)　新規作成</history>
    Public Function GetGenkaJyouhouCount(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Integer

        Return genkaMasterDA.SelGenkaJyouhouCount(strTysKaisyaCd, strAtesakiSyubetu, strAtesakiCdFrom, strAtesakiCdTo, strSyouhinCd, strHouhouCd, blnKensakuTaisyouGai, blnAitesakiTaisyouGai)

    End Function

    ''' <summary>加盟店件数を取得する</summary>
    ''' <returns>加盟店件数</returns>
    Public Function GetKameitenCount(ByVal strAitesakiCdFrom As String, ByVal strAitesakiCdTo As String, ByVal strTorikesiAitesaki As String) As Integer
        Return genkaMasterDA.SelKameitenCount(strAitesakiCdFrom, strAitesakiCdTo, strTorikesiAitesaki)
    End Function


    ''' <summary>原価情報CSVを取得する</summary>
    ''' <returns>原価情報CSVデータテーブル</returns>
    ''' <history>2011/02/28　車龍(大連情報システム部)　新規作成</history>
    Public Function GetGenkaJyouhouCSV(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Data.DataTable
        Return genkaMasterDA.SelGenkaJyouhouCSV(strTysKaisyaCd, strAtesakiSyubetu, strAtesakiCdFrom, strAtesakiCdTo, strSyouhinCd, strHouhouCd, blnKensakuTaisyouGai, blnAitesakiTaisyouGai)
    End Function


    ''' <summary>未設定も含む原価CSV件数を取得する</summary>
    ''' <returns>未設定も含む原価CSV件数</returns>
    Public Function GetMiSeteiGenkaCSVCount(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Long
        Return genkaMasterDA.SelMiSeteiGenkaCSVCount(strTysKaisyaCd, strAtesakiSyubetu, strAtesakiCdFrom, strAtesakiCdTo, strSyouhinCd, strHouhouCd, blnKensakuTaisyouGai, blnAitesakiTaisyouGai)
    End Function

    ''' <summary>未設定も含む原価CSVデータを取得する</summary>
    ''' <returns>未設定も含む原価CSVデータテーブル</returns>
    Public Function GetMiSeteiGenkaCSVInfo(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Data.DataTable
        Return genkaMasterDA.SelMiSeteiGenkaCSVInfo(strTysKaisyaCd, strAtesakiSyubetu, strAtesakiCdFrom, strAtesakiCdTo, strSyouhinCd, strHouhouCd, blnKensakuTaisyouGai, blnAitesakiTaisyouGai)
    End Function

    ''' <summary>アップロード管理を取得する</summary>
    ''' <returns>アップロード管理データテーブル</returns>
    ''' <history>2011/03/01　車龍(大連情報システム部)　新規作成</history>
    Public Function GetInputKanri() As Data.DataTable

        Return genkaMasterDA.SelInputKanri()

    End Function

    ''' <summary>アップロード管理の件数を取得する</summary>
    ''' <returns>アップロード管理の件数データテーブル</returns>
    ''' <history>2011/03/01　車龍(大連情報システム部)　新規作成</history>
    Public Function GetInputKanriCount() As Integer

        Return genkaMasterDA.SelInputKanriCount()

    End Function

    ''' <summary>CSVファイルをチェックする</summary>
    ''' <returns>CSVファイルをチェックする</returns>
    ''' <history>2011/03/01　車龍(大連情報システム部)　新規作成</history>
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

        '原価マスタを作成
        Call Me.CreateOkDataTable(dtOk)
        '原価エラーテーブルを作成
        Call Me.CreateErrorDataTable(dtError)
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
                    If i > CInt(CsvInputMaxLineCount) Then
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
                    Dim arrLine() As String = strReadLine.Split(",")
                    If strReadLine.Split(",")(0).Length < 10 Then
                        strReadLine = "0" & strReadLine
                    End If
                    '相手先（種別・コード)
                    If arrLine(4) = "3" Then
                        arrLine(5) = "JIO"
                    End If
                    strReadLine = String.Join(",", arrLine)
                    'カンマを追加
                    If strReadLine.Split(",").Length < CsvInputCheck.GENKA_FIELD_COUNT Then
                        strReadLine = strReadLine & StrDup(CsvInputCheck.GENKA_FIELD_COUNT - strReadLine.Split(",").Length, ",")
                    End If

                    'フィールド数チェック
                    If Not commonCheck.ChkFieldCount(strReadLine.Split(",").Length, CsvInputCheck.GENKA) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '項目最大長チェック
                    If Not commonCheck.ChkMaxLength(strReadLine, CsvInputCheck.GENKA) Then
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
                    If Not commonCheck.ChkSuuti(strReadLine, CsvInputCheck.GENKA) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '必須チェック
                    If Not commonCheck.ChkNotNull(strReadLine, CsvInputCheck.GENKA) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '前0埋めn桁変換
                    strReadLine = SetUmekusa(strReadLine)

                    '存在チェック
                    If Not Me.ChkSonZai(strReadLine) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '更新条件チェック
                    If Not commonCheck.ChkKousinJyouken(strReadLine, CsvInputCheck.GENKA) Then

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
    ''' 前0埋めn桁変換
    ''' </summary>
    Public Function SetUmekusa(ByVal StrLine As String) As String

        Dim arrLine() As String = StrLine.Split(",")

        '調査会社コード
        If arrLine(1).ToString <> String.Empty Then
            arrLine(1) = Right("0000" & arrLine(1).Trim, 4)
        End If

        '事業所コード
        If arrLine(2).ToString <> String.Empty Then
            arrLine(2) = Right("00" & arrLine(2).Trim, 2)
        End If

        '相手先（種別・コード)
        Select Case arrLine(4)
            Case "1"
                arrLine(5) = Right("00000" & arrLine(5).Trim, 5)
            Case "7"
                If arrLine(5).Length < 4 Then
                    arrLine(5) = Right("0000" & arrLine(5).Trim, 4)
                End If
        End Select

        Return String.Join(",", arrLine)

    End Function

    ''' <summary>CSVファイルを取込する</summary>
    ''' <returns>CSVファイルを取込する</returns>
    ''' <history>2011/03/01　車龍(大連情報システム部)　新規作成</history>
    Public Function CsvFileInput(ByVal dtOk As Data.DataTable, ByVal dtError As Data.DataTable, ByVal InputDate As String, ByVal userId As String, ByVal strFileMei As String, ByVal strEdiJouhouSakuseiDate As String) As Boolean

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                '原価マスタを登録
                If dtOk.Rows.Count > 0 Then
                    If Not genkaMasterDA.InsUpdGenkaMaster(dtOk) Then
                        Throw New ApplicationException
                    End If

                End If

                '原価エラー情報テーブルを登録
                If dtError.Rows.Count > 0 Then
                    If Not genkaMasterDA.InsGenkaError(dtError) Then
                        Throw New ApplicationException
                    End If
                End If

                'アップロード管理テーブルを登録
                If Not genkaMasterDA.InsInputKanri(InputDate, strFileMei, strEdiJouhouSakuseiDate, CInt(IIf(dtError.Rows.Count, 1, 0)), userId) Then
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

        '調査会社（調査会社コード・事業所コード）
        If Not genkaMasterDA.SelTyousaKaisyaMeiInput(intFieldCount.Split(",")(1).Trim, intFieldCount.Split(",")(2).Trim) Then
            Return False
        End If

        '相手先(種別・コード)
        If Not genkaMasterDA.SelAitesakiSyubetuInput(CInt(intFieldCount.Split(",")(4)), intFieldCount.Split(",")(5).Trim) Then
            Return False
        End If

        '商品コード
        If Not genkaMasterDA.SelSyouhinCdInput(intFieldCount.Split(",")(7).Trim) Then
            Return False
        End If

        '調査方法NO
        If Not genkaMasterDA.SelTyousahouhouNoInput(intFieldCount.Split(",")(9).Trim) Then
            Return False
        End If

        Return True
    End Function


    ''' <summary>エラーライン処理</summary>
    Private Sub ErrorLineSyori(ByVal intLineNo As Integer, ByVal strLine As String, ByRef dtError As Data.DataTable)

        Dim intMaxCount As Integer

        Dim dr As Data.DataRow
        dr = dtError.NewRow


        If strLine.Split(",").Length < CsvInputCheck.GENKA_FIELD_COUNT Then
            intMaxCount = strLine.Split(",").Length
        Else
            intMaxCount = CsvInputCheck.GENKA_FIELD_COUNT
        End If

        For i As Integer = 0 To intMaxCount - 1
            '最大長を切り取る
            dr.Item(i) = commonCheck.CutMaxLength(strLine.Split(",")(i).Trim, commonCheck.GENKA_MAX_LENGTH(i))
        Next


        '行号
        dr.Item(CsvInputCheck.GENKA_FIELD_COUNT) = CStr(intLineNo)
        '処理日時
        dr.Item(CsvInputCheck.GENKA_FIELD_COUNT + 1) = InputDate
        '登録ログインユーザID
        dr.Item(CsvInputCheck.GENKA_FIELD_COUNT + 2) = userId

        dtError.Rows.Add(dr)


    End Sub


    ''' <summary>OKライン処理</summary>
    Private Sub OkLineSyori(ByVal strLine As String, ByRef dtOk As Data.DataTable)

        'DB存在チェック
        If genkaMasterDA.SelGenkaInputJyouhou(strLine.Split(",")(1).Trim, strLine.Split(",")(2).Trim, strLine.Split(",")(4).Trim, strLine.Split(",")(5).Trim, strLine.Split(",")(7).Trim, strLine.Split(",")(9).Trim) Then
            Call Me.SetOkDataRow(strLine, dtOk, "1")
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
        dr.Item("tys_kaisya_cd") = strLine.Split(",")(1).Trim
        dr.Item("jigyousyo_cd") = strLine.Split(",")(2).Trim
        dr.Item("aitesaki_syubetu") = strLine.Split(",")(4).Trim
        dr.Item("aitesaki_cd") = strLine.Split(",")(5).Trim
        dr.Item("syouhin_cd") = strLine.Split(",")(7).Trim
        dr.Item("tys_houhou_no") = strLine.Split(",")(9).Trim
        dr.Item("torikesi") = IIf(strLine.Split(",")(11).Trim.Equals(String.Empty), "0", strLine.Split(",")(11).Trim)
        dr.Item("tou_kkk1") = strLine.Split(",")(12).Trim
        dr.Item("tou_kkk_henkou_flg1") = strLine.Split(",")(13).Trim
        dr.Item("tou_kkk2") = strLine.Split(",")(14).Trim
        dr.Item("tou_kkk_henkou_flg2") = strLine.Split(",")(15).Trim
        dr.Item("tou_kkk3") = strLine.Split(",")(16).Trim
        dr.Item("tou_kkk_henkou_flg3") = strLine.Split(",")(17).Trim
        dr.Item("tou_kkk4") = strLine.Split(",")(18).Trim
        dr.Item("tou_kkk_henkou_flg4") = strLine.Split(",")(19).Trim
        dr.Item("tou_kkk5") = strLine.Split(",")(20).Trim
        dr.Item("tou_kkk_henkou_flg5") = strLine.Split(",")(21).Trim
        dr.Item("tou_kkk6") = strLine.Split(",")(22).Trim
        dr.Item("tou_kkk_henkou_flg6") = strLine.Split(",")(23).Trim
        dr.Item("tou_kkk7") = strLine.Split(",")(24).Trim
        dr.Item("tou_kkk_henkou_flg7") = strLine.Split(",")(25).Trim
        dr.Item("tou_kkk8") = strLine.Split(",")(26).Trim
        dr.Item("tou_kkk_henkou_flg8") = strLine.Split(",")(27).Trim
        dr.Item("tou_kkk9") = strLine.Split(",")(28).Trim
        dr.Item("tou_kkk_henkou_flg9") = strLine.Split(",")(29).Trim
        dr.Item("tou_kkk10") = strLine.Split(",")(30).Trim
        dr.Item("tou_kkk_henkou_flg10") = strLine.Split(",")(31).Trim
        dr.Item("tou_kkk11t19") = strLine.Split(",")(32).Trim
        dr.Item("tou_kkk_henkou_flg11t19") = strLine.Split(",")(33).Trim
        dr.Item("tou_kkk20t29") = strLine.Split(",")(34).Trim
        dr.Item("tou_kkk_henkou_flg20t29") = strLine.Split(",")(35).Trim
        dr.Item("tou_kkk30t39") = strLine.Split(",")(36).Trim
        dr.Item("tou_kkk_henkou_flg30t39") = strLine.Split(",")(37).Trim
        dr.Item("tou_kkk40t49") = strLine.Split(",")(38).Trim
        dr.Item("tou_kkk_henkou_flg40t49") = strLine.Split(",")(39).Trim
        dr.Item("tou_kkk50t") = strLine.Split(",")(40).Trim
        dr.Item("tou_kkk_henkou_flg50t") = strLine.Split(",")(41).Trim
        dr.Item("add_login_user_id") = userId
        dr.Item("upd_login_user_id") = userId

        dt.Rows.Add(dr)

    End Sub

    ''' <summary>OKデータのkeyを設定</summary>
    Public Function GetDataKey(ByVal strLine As String) As String

        Return strLine.Split(",")(1).Trim & "," & strLine.Split(",")(2).Trim & "," & strLine.Split(",")(4).Trim & "," & strLine.Split(",")(5).Trim & "," & strLine.Split(",")(7).Trim & "," & strLine.Split(",")(9).Trim

    End Function


    ''' <summary>原価マスタを作成</summary>
    Public Sub CreateOkDataTable(ByRef dt As Data.DataTable)

        Dim dc As Data.DataColumn

        dc = New Data.DataColumn
        dc.ColumnName = "key" 'キー(調査会社コード + 事業所コード + 相手先種別 + 相手先コード + 商品コード + 調査方法NO)
        dt.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "ins_upd_flg" '追加更新FLG(0:追加; 1:更新)
        dt.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "tys_kaisya_cd" '調査会社コード
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jigyousyo_cd"  '事業所コード
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"  '相手先種別
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"   '相手先コード
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"    '商品コード
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_houhou_no" '調査方法NO
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"  '取消
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk1"  '棟価格1
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg1"   '棟価格変更FLG1
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk2"  '棟価格2
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg2"   '棟価格変更FLG2
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk3"  '棟価格3
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg3"   '棟価格変更FLG3
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk4"  '棟価格4
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg4"   '棟価格変更FLG4
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk5"  '棟価格5
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg5"   '棟価格変更FLG5
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk6"  '棟価格6
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg6"   '棟価格変更FLG6
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk7"  '棟価格7
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg7"   '棟価格変更FLG7
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk8"  '棟価格8
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg8"   '棟価格変更FLG8
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk9"  '棟価格9
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg9"   '棟価格変更FLG9
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk10" '棟価格10
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg10"  '棟価格変更FLG10
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk11t19"  '棟価格11～19
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg11t19"   '棟価格変更FLG11～19
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk20t29"  '棟価格20～29
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg20t29"   '棟価格変更FLG20～29
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk30t39"  '棟価格30～39
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg30t39"   '棟価格変更FLG30～39
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk40t49"  '棟価格40～49
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg40t49"   '棟価格変更FLG40～49
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk50t"    '棟価格50～
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg50t" '棟価格変更FLG50～
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "add_login_user_id" '登録ログインユーザID
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "add_datetime"  '登録日時
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "upd_login_user_id" '更新ログインユーザID
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "upd_datetime"  '更新日時
        dt.Columns.Add(dc)

    End Sub

    ''' <summary>原価エラーテーブルを作成</summary>
    Public Sub CreateErrorDataTable(ByRef dt As Data.DataTable)

        Dim dc As Data.DataColumn

        dc = New Data.DataColumn
        dc.ColumnName = "edi_jouhou_sakusei_date"   'EDI情報作成日
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_kaisya_cd" '調査会社コード
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jigyousyo_cd"  '事業所コード
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_kaisya_mei"    '調査会社名
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"  '相手先種別
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"   '相手先コード
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_mei"  '相手先名
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"    '商品コード
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_mei"   '商品名
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_houhou_no" '調査方法NO
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_houhou"    '調査方法
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"  '取消
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk1"  '棟価格1
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg1"   '棟価格変更FLG1
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk2"  '棟価格2
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg2"   '棟価格変更FLG2
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk3"  '棟価格3
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg3"   '棟価格変更FLG3
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk4"  '棟価格4
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg4"   '棟価格変更FLG4
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk5"  '棟価格5
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg5"   '棟価格変更FLG5
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk6"  '棟価格6
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg6"   '棟価格変更FLG6
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk7"  '棟価格7
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg7"   '棟価格変更FLG7
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk8"  '棟価格8
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg8"   '棟価格変更FLG8
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk9"  '棟価格9
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg9"   '棟価格変更FLG9
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk10" '棟価格10
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg10"  '棟価格変更FLG10
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk11t19"  '棟価格11～19
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg11t19"   '棟価格変更FLG11～19
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk20t29"  '棟価格20～29
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg20t29"   '棟価格変更FLG20～29
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk30t39"  '棟価格30～39
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg30t39"   '棟価格変更FLG30～39
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk40t49"  '棟価格40～49
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg40t49"   '棟価格変更FLG40～49
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk50t"    '棟価格50～
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg50t" '棟価格変更FLG50～
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "gyou_no"   '行NO
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syori_datetime"    '処理日時
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "add_login_user_id" '登録ログインユーザID
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "add_datetime"  '登録日時
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "upd_login_user_id" '更新ログインユーザID
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "upd_datetime"  '更新日時
        dt.Columns.Add(dc)

    End Sub

    ''' <summary>原価エラー情報を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <returns>原価エラーデータテーブル</returns>
    Public Function GetGenkaErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) As Data.DataTable
        Return genkaMasterDA.SelGenkaErr(strEdiJouhouSakuseiDate, strSyoridate)
    End Function

    ''' <summary>原価エラー件数を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <returns>原価エラー件数</returns>
    Public Function GetGenkaErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) As String
        Return genkaMasterDA.SelGenkaErrCount(strEdiJouhouSakuseiDate, strSyoridate)
    End Function

    ''' <summary>原価エラーCSV情報を取得する</summary>
    ''' <returns>原価エラーCSVデータテーブル</returns>
    Public Function SelGenkaErrCsv(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) As Data.DataTable
        Return genkaMasterDA.SelGenkaErrCsv(strEdiJouhouSakuseiDate, strSyoridate)
    End Function

End Class
