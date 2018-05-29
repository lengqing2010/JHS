Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports Lixil.JHS_EKKS.Utilities
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' 支店 月別計画値 ＣＳＶ取込BC
''' </summary>
''' <remarks></remarks>
''' <history>
''' <para>2012/11/26 P-44979 王莎莎 新規作成 </para>
''' </history>
Public Class SitenTukibetuKeikakuchiInputBC

    '支店 月別計画値 ＣＳＶ取込DA
    Private sitenTukibetuKeikakuchiInputDA As New SitenTukibetuKeikakuchiInputDA
    'CommonDA
    Private commonDA As New CommonDA

    'テーブル「支店別月別計画管理テーブル」の項目最大長
    Private SITENBETU_MAX_LENGTH() As Integer = {2, 40, 4, 4, 40, 1, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12}
    'テーブル「支店別月別計画管理表_取込エラー情報テーブル」の項目最大長
    Private SITENBETUERROR_MAX_LENGTH() As Integer = {40, 12, 20, 80, 30, 20, 30, 20}
    '使用禁止文字列配列
    Private arrayKinsiStr() As String = New String() {vbTab, """", "，", "'", "<", ">", "&", "$$$"}
    'テーブル「支店別月別計画管理テーブル」の数値型項目索引
    Private HANBAI_NUM_INDEX() As Integer = {1, 2, 3, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41}
    'テーブル「支店別月別計画管理テーブル」の必須入力項目索引
    Private HANBAI_NOTNULL_INDEX() As Integer = {0, 1, 2, 3, 5}

    ''' <summary>
    ''' 画面一覧データ取得処理
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <returns>アップロード管理テーブルのデータ</returns>
    ''' <remarks>アップロード管理テーブルのデータを取得する</remarks>
    ''' <history>
    ''' <para>2012/11/23 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Function SelTUploadKanri(ByVal strKbn As String) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKbn)

        Return sitenTukibetuKeikakuchiInputDA.SelTUploadKanri(strKbn)

    End Function

    ''' <summary>
    ''' CSV取込処理
    ''' </summary>
    ''' <param name="fupId">取込ファイル</param>
    ''' <param name="strUmuFlg">空白</param>
    ''' <returns>エラーメッセージ番号</returns>
    ''' <remarks>CSV取込処理</remarks>
    ''' <history>
    ''' <para>2012/11/23 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Function ChkCsvFile(ByVal fupId As Web.UI.WebControls.FileUpload, ByRef strUmuFlg As String) As String

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, fupId, strUmuFlg)

        Dim myStream As IO.Stream                                '入出力ストリーム
        Dim myReader As IO.StreamReader                          'ストリームリーダー
        Dim strReadLine As String                                '取込ファイル読込み行
        Dim intLineCount As Integer = 0                          'ライン数
        Dim strCsvLine() As String                               'CSVファイル内容
        Dim strNyuuryokuFileMei As String                        'CSVファイル名
        Dim dtTSitenbetuTukiKeikakuKanriOk As New Data.DataTable '支店別月別計画管理テーブル
        Dim dtTSitenTukibetuTorikomiError As New Data.DataTable  '支店別月別計画管理表_取込エラー情報テーブル
        Dim strUploadDate As String                              'アップロード日時
        Dim ninsyou As New NinsyouBC
        Dim strUserId As String                                  'ユーザーID

        'アップロード日時
        'strUploadDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")
        strUploadDate = commonDA.SelSystemDate().Rows(0).Item(0).ToString
        'ユーザーID
        strUserId = ninsyou.GetUserID()
        'CSVファイル名
        strNyuuryokuFileMei = CutMaxLength(fupId.FileName, 128)
        'EDI情報作成日
        Dim strEdiJouhouSakuseiDate As String = String.Empty
        'エラーFLG
        Dim errorFlg As Integer = 0
        '入出力ストリーム
        myStream = fupId.FileContent
        'ストリームリーダー
        myReader = New IO.StreamReader(myStream, System.Text.Encoding.GetEncoding(932))
        '販売価格マスタを作成する
        Call SetTSitenbetuTukiKeikakuKanri(dtTSitenbetuTukiKeikakuKanriOk)
        '販売価格エラーテーブルを作成する
        Call SetTSitenTukibetuTorikomiError(dtTSitenTukibetuTorikomiError)

        Try
            Do
                '取込ファイルを読み込む
                ReDim Preserve strCsvLine(intLineCount)
                strCsvLine(intLineCount) = myReader.ReadLine()
                intLineCount += 1
            Loop Until myReader.EndOfStream

            'EDI情報作成日
            'strEdiJouhouSakuseiDate = Right("0" & strCsvLine(0).Split(CChar(","))(1), 40)
            If strCsvLine(0).Split(CChar(",")).Length >= 2 Then
                strEdiJouhouSakuseiDate = strCsvLine(0).Split(CChar(","))(1)
            Else
                strEdiJouhouSakuseiDate = String.Empty
            End If

            '営業区分
            Dim strEigyouKbn As String = String.Empty
            Dim intFlg As Integer = 0
            '最大件数チェック
            If strCsvLine.Length > 3 Then
                'エラーメッセージを表示して、処理終了する
                Return String.Format(CommonMessage.MSG061E, "3")
            End If

            'CSVファイルをチェック
            For i As Integer = 0 To strCsvLine.Length - 1
                strReadLine = strCsvLine(i)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                    'CSV種類チェック
                    If strReadLine.Split(CChar(","))(0) <> "A1" Then
                        'エラーメッセージを表示して、処理終了する
                        Return String.Format(CommonMessage.MSG056E, "支店月別計画値取込CSV")
                    End If
                    If strReadLine.Split(CChar(",")).Length >= 4 Then
                        'データが確認済みチェック
                        Dim intDataCount As Integer = sitenTukibetuKeikakuchiInputDA.SelTSitenbetuTukiKeikakuKanriCount(strReadLine.Split(CChar(","))(2), strReadLine.Split(CChar(","))(3))
                        If intDataCount > 0 Then
                            'エラーメッセージを表示して、処理終了する
                            Return CommonMessage.MSG064E
                        End If
                    Else
                        'エラーデータを作成する
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "2", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If

                    '項目数チェック
                    If strReadLine.Split(CChar(",")).Length > 42 Then
                        'エラーデータを作成する
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "1", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    If strReadLine.Split(CChar(",")).Length < 42 Then
                        strReadLine = strReadLine & StrDup(42 - strReadLine.Split(CChar(",")).Length, ",")
                    End If
                    '必須チェック
                    If Not ChkNotNull(strReadLine) Then
                        'エラーデータを作成する
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "2", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    '必須項目(キー項目)部署ｺｰﾄﾞ存在チェック
                    Dim intBusyoCdCount As Integer = sitenTukibetuKeikakuchiInputDA.SelMBusyoKanri(strReadLine.Split(CChar(","))(3))
                    If intBusyoCdCount = 0 Then
                        'エラーデータを作成する
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "3", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    '必須項目(キー項目）営業区分が「1、3、4」以外の場合、
                    If strReadLine.Split(CChar(","))(5) <> "1" AndAlso strReadLine.Split(CChar(","))(5) <> "3" AndAlso strReadLine.Split(CChar(","))(5) <> "4" Then
                        'エラーデータを作成する
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "4", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    
                    '必須項目(キー項目）営業区分が重複な場合
                    If intFlg = 1 AndAlso strEigyouKbn = strReadLine.Split(CChar(","))(5) Then
                        'エラーデータを作成する
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "5", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    If intFlg = 2 AndAlso (strEigyouKbn.Split(CChar(","))(0) = strReadLine.Split(CChar(","))(5) OrElse strEigyouKbn.Split(CChar(","))(1) = strReadLine.Split(CChar(","))(5)) Then
                        'エラーデータを作成する
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "5", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    '禁則文字チェック
                    If Not ChkKinjiMoji(strReadLine) Then
                        'エラーデータを作成する
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "6", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    '数値型項目チェック
                    If Not ChkSuuti(strReadLine) Then
                        'エラーデータを作成する
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "7", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    '項目最大長チェック
                    If Not ChkMaxLength(strReadLine) Then
                        'エラーデータを作成する
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "8", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    '営業区分
                    If strEigyouKbn = String.Empty AndAlso i = 0 Then
                        strEigyouKbn = strReadLine.Split(CChar(","))(5)
                    Else
                        strEigyouKbn = strEigyouKbn + "," + strReadLine.Split(CChar(","))(5)
                    End If
                    intFlg = intFlg + 1
                    'エラーがない場合
                    If errorFlg = 0 Then
                        '更新･追加を判断する
                        Call SetTSitenTukibetuTorikomiDataRow(strReadLine, dtTSitenbetuTukiKeikakuKanriOk)
                    End If

                End If
            Next
            'エラー有無フラグを設定する
            If dtTSitenTukibetuTorikomiError.Rows.Count > 0 Then
                strUmuFlg = "1"
                dtTSitenbetuTukiKeikakuKanriOk.Rows.Clear()
            End If
            'CSVファイルを取込
            If Not CsvFileUpload(dtTSitenbetuTukiKeikakuKanriOk, dtTSitenTukibetuTorikomiError, strUploadDate, strEdiJouhouSakuseiDate, strUserId, strNyuuryokuFileMei) Then

            End If
        Catch ex As Exception
        End Try

        Return String.Empty
    End Function

    ''' <summary>
    ''' データ挿入処理
    ''' </summary>
    ''' <param name="dtTSitenbetuTukiKeikakuKanriOk">支店別月別計画管理データ</param>
    ''' <param name="dtTSitenTukibetuTorikomiError">支店別月別計画管理表_取込エラーデータ</param>
    ''' <param name="strUploadDate">アップロード日時</param>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <param name="strNyuuryokuFileMei">取込ファイル名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CsvFileUpload(ByVal dtTSitenbetuTukiKeikakuKanriOk As Data.DataTable, _
                                  ByVal dtTSitenTukibetuTorikomiError As Data.DataTable, _
                                  ByVal strUploadDate As String, _
                                  ByVal strEdiJouhouSakuseiDate As String, _
                                  ByVal strUserId As String, _
                                  ByVal strNyuuryokuFileMei As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                               dtTSitenbetuTukiKeikakuKanriOk, _
                               dtTSitenTukibetuTorikomiError, _
                               strUploadDate, _
                               strEdiJouhouSakuseiDate, _
                               strUserId, _
                               strNyuuryokuFileMei)

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                '「支店別月別計画管理テーブル」に挿入する
                If dtTSitenbetuTukiKeikakuKanriOk.Rows.Count > 0 Then
                    'EXCEl取込データ挿入処理
                    For i As Integer = 0 To dtTSitenbetuTukiKeikakuKanriOk.Rows.Count - 1
                        If Not sitenTukibetuKeikakuchiInputDA.InsTSitenbetuTukiKeikakuKanri(dtTSitenbetuTukiKeikakuKanriOk.Rows(i), strUserId) Then
                            Throw New ApplicationException
                        End If
                    Next
                End If
                '支店別月別計画管理表_取込エラー情報テーブルを登録する
                If dtTSitenTukibetuTorikomiError.Rows.Count > 0 Then
                    For i As Integer = 0 To dtTSitenTukibetuTorikomiError.Rows.Count - 1
                        'エラーデータ挿入処理
                        If Not sitenTukibetuKeikakuchiInputDA.InsTSitenTukibetuTorikomiError(strUploadDate, strEdiJouhouSakuseiDate, dtTSitenTukibetuTorikomiError.Rows(i).Item(0).ToString, dtTSitenTukibetuTorikomiError.Rows(i).Item(1).ToString, strUserId) Then
                            '失敗の場合
                            scope.Dispose()
                        End If
                    Next
                End If
                'アップロード管理テーブルを登録する
                If Not sitenTukibetuKeikakuchiInputDA.InsTUploadKanri(strUploadDate, strEdiJouhouSakuseiDate, CStr(IIf(dtTSitenTukibetuTorikomiError.Rows.Count > 0, 1, 0)), strNyuuryokuFileMei, strUserId) Then
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

    ''' <summary>
    ''' 最大長を切り取る
    ''' </summary>
    ''' <param name="strValue">取込ファイル詳細</param>
    ''' <param name="intMaxByteCount">"128"を固定</param>
    ''' <returns>取込ファイルの名称</returns>
    ''' <remarks>取込ファイルの名称を取得する</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Function CutMaxLength(ByVal strValue As String, ByVal intMaxByteCount As Integer) As String

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strValue, intMaxByteCount)

        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        Dim intLengthCount As Integer = 0
        For i As Integer = strValue.Length To 0 Step -1
            Dim btBytes As Byte() = hEncoding.GetBytes(Left(strValue, i))
            If btBytes.LongLength <= intMaxByteCount Then
                intLengthCount = i
                Exit For
            End If
        Next

        Return Left(strValue, intLengthCount)
    End Function

    ''' <summary>
    ''' 項目最大長チェック
    ''' </summary>
    ''' <param name="strLine">当り前行</param>
    ''' <returns>TRUE：エラー無し、FALSE：エラー有り</returns>
    ''' <remarks>取込データ当り前行各個項目最大長チェック</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Function ChkMaxLength(ByVal strLine As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine)

        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'For i As Integer = 0 To strLine.Split(CChar(",")).Length - 1
        For i As Integer = 0 To 41
            Dim btBytes As Byte() = hEncoding.GetBytes(strLine.Split(CChar(","))(i))
            If btBytes.LongLength > SITENBETU_MAX_LENGTH(i) Then
                Return False
            End If
        Next

        Return True
    End Function

    ''' <summary>
    ''' 禁則文字チェック
    ''' </summary>
    ''' <param name="target">当り前項目</param>
    ''' <returns>TRUE：エラー無し、FALSE：エラー有り</returns>
    ''' <remarks>取込データ当り前項目禁則文字チェック</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Function ChkKinjiMoji(ByVal target As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, target)

        For Each st As String In arrayKinsiStr

            If target.IndexOf(st) >= 0 Then
                Return False
            End If

        Next

        Return True
    End Function

    ''' <summary>
    ''' 整数チェック
    ''' </summary>
    ''' <param name="inTarget">当り前項目</param>
    ''' <returns>TRUE：エラー無し、FALSE：エラー有り</returns>
    ''' <remarks>取込データ当り前項目整数チェック</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Function CheckHankaku(ByVal inTarget As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, inTarget)

        If inTarget.Length = System.Text.Encoding.Default.GetByteCount(inTarget) And IsNumeric(inTarget) Then
            Try
                Dim intTemp As Long = CLng(inTarget)
            Catch ex As Exception
                Return False
            End Try
            '*********************2013/03/09 指摘No.37 問題対応'*********************
            'If InStr(inTarget, ".") > 0 Or InStr(inTarget, "-") > 0 Or InStr(inTarget, "+") > 0 Then
            If InStr(inTarget, ".") > 0 Or InStr(inTarget, "+") > 0 Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 数値型項目チェック
    ''' </summary>
    ''' <param name="strLine">取込データ当り前行</param>
    ''' <returns>TRUE：エラー無し、FALSE：エラー有り</returns>
    ''' <remarks>取込データ当り前行数値型項目チェック</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Function ChkSuuti(ByVal strLine As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine)

        For Each i As Integer In HANBAI_NUM_INDEX

            If (Not strLine.Split(CChar(","))(i).Trim.Equals(String.Empty)) AndAlso (Not CheckHankaku(strLine.Split(CChar(","))(i))) Then
                Return False
            End If
        Next

        Return True
    End Function

    ''' <summary>
    ''' 必須チェック
    ''' </summary>
    ''' <param name="strLine">取込データ当り前行</param>
    ''' <returns>TRUE：エラー無し、FALSE：エラー有り</returns>
    ''' <remarks>取込データ当り前行必須存在チェック</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Function ChkNotNull(ByVal strLine As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine)

        For Each i As Integer In HANBAI_NOTNULL_INDEX

            If strLine.Split(CChar(","))(i).Trim.Equals(String.Empty) Then
                Return False
            End If
        Next

        Return True
    End Function

    ''' <summary>
    ''' 支店別月別計画管理テーブルを作成する
    ''' </summary>
    ''' <param name="dtTSitenbetuTukiKeikakuKanriOk">支店別月別計画管理テーブル</param>
    ''' <remarks>支店別月別計画管理テーブルを作成する</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Sub SetTSitenbetuTukiKeikakuKanri(ByRef dtTSitenbetuTukiKeikakuKanriOk As Data.DataTable)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtTSitenbetuTukiKeikakuKanriOk)

        Dim dc As Data.DataColumn

        dc = New Data.DataColumn
        dc.ColumnName = "keikaku_nendo"                '計画_年度
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "busyo_cd"                     '部署ｺｰﾄﾞ
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "siten_mei"                    '支店名
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "eigyou_kbn"                   '営業区分
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "4gatu_keikaku_kensuu"         '4月_計画件数
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "4gatu_keikaku_kingaku"        '4月_計画金額
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "4gatu_keikaku_arari"          '4月_計画粗利
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "5gatu_keikaku_kensuu"         '5月_計画件数
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "5gatu_keikaku_kingaku"        '5月_計画金額
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "5gatu_keikaku_arari"          '5月_計画粗利
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "6gatu_keikaku_kensuu"         '6月_計画件数
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "6gatu_keikaku_kingaku"        '6月_計画金額
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "6gatu_keikaku_arari"          '6月_計画粗利
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "7gatu_keikaku_kensuu"         '7月_計画件数
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "7gatu_keikaku_kingaku"        '7月_計画金額
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "7gatu_keikaku_arari"          '7月_計画粗利
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "8gatu_keikaku_kensuu"         '8月_計画件数
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "8gatu_keikaku_kingaku"        '8月_計画金額
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "8gatu_keikaku_arari"          '8月_計画粗利
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "9gatu_keikaku_kensuu"         '9月_計画件数
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "9gatu_keikaku_kingaku"        '9月_計画金額
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "9gatu_keikaku_arari"          '9月_計画粗利
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "10gatu_keikaku_kensuu"        '10月_計画件数
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "10gatu_keikaku_kingaku"       '10月_計画金額
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "10gatu_keikaku_arari"         '10月_計画粗利
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "11gatu_keikaku_kensuu"        '11月_計画件数
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "11gatu_keikaku_kingaku"       '11月_計画金額
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "11gatu_keikaku_arari"         '11月_計画粗利
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "12gatu_keikaku_kensuu"        '12月_計画件数
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "12gatu_keikaku_kingaku"       '12月_計画金額
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "12gatu_keikaku_arari"         '12月_計画粗利
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "1gatu_keikaku_kensuu"         '1月_計画件数
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "1gatu_keikaku_kingaku"        '1月_計画金額
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "1gatu_keikaku_arari"          '1月_計画粗利
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "2gatu_keikaku_kensuu"         '2月_計画件数
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "2gatu_keikaku_kingaku"        '2月_計画金額
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "2gatu_keikaku_arari"          '2月_計画粗利
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "3gatu_keikaku_kensuu"         '3月_計画件数
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "3gatu_keikaku_kingaku"        '3月_計画金額
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "3gatu_keikaku_arari"          '3月_計画粗利
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)

    End Sub

    ''' <summary>
    ''' 支店別月別計画管理表_取込エラー情報テーブルを作成する
    ''' </summary>
    ''' <param name="dtTSitenTukibetuTorikomiError">支店別月別計画管理表_取込エラー情報テーブル</param>
    ''' <remarks>支店別月別計画管理表_取込エラー情報テーブルを作成する</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Sub SetTSitenTukibetuTorikomiError(ByRef dtTSitenTukibetuTorikomiError As Data.DataTable)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtTSitenTukibetuTorikomiError)

        Dim dc As Data.DataColumn

        dc = New Data.DataColumn
        dc.ColumnName = "gyou_no"                '行No
        dtTSitenTukibetuTorikomiError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "kbn"                    '区分
        dtTSitenTukibetuTorikomiError.Columns.Add(dc)

    End Sub

    ''' <summary>
    ''' 支店別月別計画管理表_取込エラー情報テーブルを作成する
    ''' </summary>
    ''' <param name="intLineNo">取込データ当り前行</param>
    ''' <param name="strKbn">エラー区分</param>
    ''' <param name="dtTSitenTukibetuTorikomiError">支店別月別計画管理表_取込エラー情報テーブル</param>
    ''' <remarks>支店別月別計画管理表_取込エラー情報テーブルを作成する</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Private Sub SetTSitenTukibetuTorikomiErrorData(ByVal intLineNo As Integer, _
                                                   ByVal strKbn As String, _
                                                   ByRef dtTSitenTukibetuTorikomiError As Data.DataTable)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, intLineNo, strKbn, dtTSitenTukibetuTorikomiError)

        Dim dr As Data.DataRow
        dr = dtTSitenTukibetuTorikomiError.NewRow
        dr.Item("gyou_no") = intLineNo           '計画_年度
        dr.Item("kbn") = strKbn                  'エラー区分

        dtTSitenTukibetuTorikomiError.Rows.Add(dr)

    End Sub

    ''' <summary>支店別月別計画管理データを作成する</summary>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    ''' <param name="dtTSitenbetuTukiKeikakuKanriOk">支店別月別計画管理データ</param>
    ''' <history>
    ''' <para>2012/12/05 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Sub SetTSitenTukibetuTorikomiDataRow(ByVal strLine As String, _
                                                ByRef dtTSitenbetuTukiKeikakuKanriOk As Data.DataTable)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine, dtTSitenbetuTukiKeikakuKanriOk)

        Dim dr As Data.DataRow
        dr = dtTSitenbetuTukiKeikakuKanriOk.NewRow
        dr.Item("keikaku_nendo") = strLine.Split(CChar(","))(2).Trim           '計画_年度
        dr.Item("busyo_cd") = strLine.Split(CChar(","))(3).Trim                '部署ｺｰﾄﾞ
        dr.Item("siten_mei") = strLine.Split(CChar(","))(4).Trim               '支店名

        dr.Item("eigyou_kbn") = strLine.Split(CChar(","))(5).Trim              '営業区分

        dr.Item("4gatu_keikaku_kensuu") = strLine.Split(CChar(","))(6).Trim    '4月_計画件数
        dr.Item("4gatu_keikaku_kingaku") = strLine.Split(CChar(","))(7).Trim   '4月_計画金額
        dr.Item("4gatu_keikaku_arari") = strLine.Split(CChar(","))(8).Trim     '4月_計画金額
        dr.Item("5gatu_keikaku_kensuu") = strLine.Split(CChar(","))(9).Trim    '5月_計画金額
        dr.Item("5gatu_keikaku_kingaku") = strLine.Split(CChar(","))(10).Trim  '5月_計画金額
        dr.Item("5gatu_keikaku_arari") = strLine.Split(CChar(","))(11).Trim    '5月_計画粗利
        dr.Item("6gatu_keikaku_kensuu") = strLine.Split(CChar(","))(12).Trim   '6月_計画件数
        dr.Item("6gatu_keikaku_kingaku") = strLine.Split(CChar(","))(13).Trim  '6月_計画金額
        dr.Item("6gatu_keikaku_arari") = strLine.Split(CChar(","))(14).Trim    '6月_計画金額

        dr.Item("7gatu_keikaku_kensuu") = strLine.Split(CChar(","))(15).Trim   '7月_計画金額
        dr.Item("7gatu_keikaku_kingaku") = strLine.Split(CChar(","))(16).Trim  '7月_計画金額
        dr.Item("7gatu_keikaku_arari") = strLine.Split(CChar(","))(17).Trim    '7月_計画粗利
        dr.Item("8gatu_keikaku_kensuu") = strLine.Split(CChar(","))(18).Trim   '8月_計画件数
        dr.Item("8gatu_keikaku_kingaku") = strLine.Split(CChar(","))(19).Trim  '8月_計画金額
        dr.Item("8gatu_keikaku_arari") = strLine.Split(CChar(","))(20).Trim    '8月_計画金額
        dr.Item("9gatu_keikaku_kensuu") = strLine.Split(CChar(","))(21).Trim   '9月_計画金額
        dr.Item("9gatu_keikaku_kingaku") = strLine.Split(CChar(","))(22).Trim  '9月_計画金額
        dr.Item("9gatu_keikaku_arari") = strLine.Split(CChar(","))(23).Trim    '9月_計画粗利

        dr.Item("10gatu_keikaku_kensuu") = strLine.Split(CChar(","))(24).Trim  '10月_計画件数
        dr.Item("10gatu_keikaku_kingaku") = strLine.Split(CChar(","))(25).Trim '10月_計画金額
        dr.Item("10gatu_keikaku_arari") = strLine.Split(CChar(","))(26).Trim   '10月_計画金額
        dr.Item("11gatu_keikaku_kensuu") = strLine.Split(CChar(","))(27).Trim  '11月_計画金額
        dr.Item("11gatu_keikaku_kingaku") = strLine.Split(CChar(","))(28).Trim '11月_計画金額
        dr.Item("11gatu_keikaku_arari") = strLine.Split(CChar(","))(29).Trim   '11月_計画粗利
        dr.Item("12gatu_keikaku_kensuu") = strLine.Split(CChar(","))(30).Trim  '12月_計画件数
        dr.Item("12gatu_keikaku_kingaku") = strLine.Split(CChar(","))(31).Trim '12月_計画金額
        dr.Item("12gatu_keikaku_arari") = strLine.Split(CChar(","))(32).Trim   '12月_計画金額

        dr.Item("1gatu_keikaku_kensuu") = strLine.Split(CChar(","))(33).Trim   '1月_計画金額
        dr.Item("1gatu_keikaku_kingaku") = strLine.Split(CChar(","))(34).Trim  '1月_計画金額
        dr.Item("1gatu_keikaku_arari") = strLine.Split(CChar(","))(35).Trim    '1月_計画粗利
        dr.Item("2gatu_keikaku_kensuu") = strLine.Split(CChar(","))(36).Trim   '2月_計画件数
        dr.Item("2gatu_keikaku_kingaku") = strLine.Split(CChar(","))(37).Trim  '2月_計画金額
        dr.Item("2gatu_keikaku_arari") = strLine.Split(CChar(","))(38).Trim    '2月_計画金額
        dr.Item("3gatu_keikaku_kensuu") = strLine.Split(CChar(","))(39).Trim   '3月_計画金額
        dr.Item("3gatu_keikaku_kingaku") = strLine.Split(CChar(","))(40).Trim  '3月_計画金額
        dr.Item("3gatu_keikaku_arari") = strLine.Split(CChar(","))(41).Trim    '3月_計画粗利
        dtTSitenbetuTukiKeikakuKanriOk.Rows.Add(dr)

    End Sub

End Class
