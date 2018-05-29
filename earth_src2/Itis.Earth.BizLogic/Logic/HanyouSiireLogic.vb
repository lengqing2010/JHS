Imports System.Transactions
Imports Itis.Earth.DataAccess

Public Class HanyouSiireLogic

    '汎用仕入データ取込インスタンス生成 
    Private hanyouSiireDA As New HanyouSiireDataAccess

    ''' <summary>アップロード管理を取得する</summary>
    ''' <returns>アップロード管理データテーブル</returns>
    Public Function GetUploadKanri() As Data.DataTable
        Return hanyouSiireDA.SelUploadKanri()
    End Function

    ''' <summary>アップロード管理の件数を取得する</summary>
    ''' <returns>アップロード管理の件数</returns>
    Public Function GetUploadKanriCount() As Integer
        Return hanyouSiireDA.SelUploadKanriCount()
    End Function

    ''' <summary>汎用仕入エラー情報を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <param name="intTopCount">取得の最大行数</param>
    ''' <returns>汎用仕入エラーデータテーブル</returns>
    Public Function GetHanyouSiireErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String, ByVal intTopCount As Integer) As DataTable
        Return hanyouSiireDA.SelHanyouSiireErr(strEdiJouhouSakuseiDate, strSyoriDate, intTopCount)
    End Function

    ''' <summary>汎用仕入エラー件数を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>汎用仕入エラー件数</returns>
    Public Function GetHanyouSiireErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As Integer
        Return hanyouSiireDA.SelHanyouSiireErrCount(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function

    Public Function GetUploadKanri(ByVal strFileName As String) As Integer
        Return hanyouSiireDA.SelUploadKanri("7", strFileName)
    End Function
 
    Public Function ChkCsvFile(ByVal strCsvLine() As String, ByVal FileName As String, ByRef strUmuFlg As String) As String


        Dim strReadLine As String                               '取込ファイル読込み行


        Dim strNyuuryokuFileMei As String                       'CSVファイル名
        Dim strEdiJouhouSakuseiDate As String = String.Empty    'EDI情報作成日
        Dim dtHanyouSiireOk As New Data.DataTable               '汎用仕入マスタ
        Dim dtHanyouSiireErr As New Data.DataTable              '汎用仕入エラー
        Dim strUploadDate As String                             'アップロード日時
        Dim ninsyou As New Ninsyou()
        Dim strUserId As String                                 'ユーザーID
        Dim strMaxUpload As String                              'CSV取込の上限件数
        Dim commonCheck As New CsvInputCheck
        'アップロード日時
        strUploadDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
        'ユーザーID
        strUserId = ninsyou.GetUserID()
        'CSVファイル名
        strNyuuryokuFileMei = commonCheck.CutMaxLength(FileName, 128)
        'CSV取込の上限件数
        strMaxUpload = System.Configuration.ConfigurationManager.AppSettings("CsvInputMaxLineCount").ToString

        '工事価格マスタを作成する
        Call SetHanyouSiireOk(dtHanyouSiireOk)
        '工事価格エラーテーブルを作成する
        Call SetHanyouSiireErr(dtHanyouSiireErr)

        Try
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
            strEdiJouhouSakuseiDate = Right("          " & strCsvLine(1).Split(",")(0), 40)
            strEdiJouhouSakuseiDate = Replace(strEdiJouhouSakuseiDate, "/", "")
            strEdiJouhouSakuseiDate = Replace(strEdiJouhouSakuseiDate, ":", "")
            strEdiJouhouSakuseiDate = Replace(strEdiJouhouSakuseiDate, " ", "")
            strEdiJouhouSakuseiDate = Right(strEdiJouhouSakuseiDate, 40)
            'CSVファイルをチェック
            For i As Integer = 1 To strCsvLine.Length - 1
                strReadLine = strCsvLine(i)
                Dim arrLine2() As String = Split(strReadLine, ",")
                arrLine2(0) = Replace(arrLine2(0), "/", "")
                arrLine2(0) = Replace(arrLine2(0), ":", "")
                arrLine2(0) = Replace(arrLine2(0), " ", "")
                arrLine2(0) = Right(arrLine2(0), 40)
                arrLine2(5) = Right("0000" & arrLine2(5).Trim, 4)
                arrLine2(6) = Right("00" & arrLine2(6).Trim, 2)
                '===============2013/07/04 車龍 修正↓=========================
                'arrLine2(7) = Right("00000" & arrLine2(7).Trim, 5)
                If (arrLine2(7).Trim.Equals(String.Empty)) OrElse (arrLine2(7).Trim.ToUpper.Equals("NULL")) Then
                    arrLine2(7) = String.Empty
                Else
                    arrLine2(7) = Right("00000" & arrLine2(7).Trim, 5)
                End If
                '===============2013/07/04 車龍 修正↑=========================
                strReadLine = String.Join(",", arrLine2)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                    ''EDI情報作成日が前0埋め変換
                    'If strReadLine.Split(",")(0).Length < 10 Then
                    '    Dim arrLine() As String = Split(strReadLine, ",")
                    '    arrLine(0) = Right("          " & arrLine(0).Trim, 10)
                    '    strReadLine = String.Join(",", arrLine)
                    'End If

                    '桁数埋め変換
                    If strReadLine.Split(",").Length < CsvInputCheck.SIIRE_FIELD_COUNT Then
                        strReadLine = strReadLine & StrDup(CsvInputCheck.SIIRE_FIELD_COUNT - strReadLine.Split(",").Length, ",")
                    End If
                    '施主名を取得
                    If strReadLine.Split(",")(17).Trim = "" Then
                        Dim arrLine() As String = Split(strReadLine, ",")
                        arrLine(17) = hanyouSiireDA.SelSesyuMei(strReadLine.Split(",")(15).Trim, strReadLine.Split(",")(16).Trim)
                        strReadLine = String.Join(",", arrLine)
                    End If
                    'フィールド数チェック
                    If Not commonCheck.ChkFieldCount(strReadLine.Split(",").Length, CsvInputCheck.SIIRE) Then
                        'エラーデータを作成する
                        Call SetHanyouSiireErrData(i + 1, strReadLine, dtHanyouSiireErr)
                        Continue For
                    End If
                    '項目最大長チェック
                    If Not commonCheck.ChkMaxLength(strReadLine, CsvInputCheck.SIIRE) Then
                        'エラーデータを作成する
                        Call SetHanyouSiireErrData(i + 1, strReadLine, dtHanyouSiireErr)
                        Continue For
                    End If
                    '禁則文字チェック
                    If Not commonCheck.ChkKinjiMoji(strReadLine) Then
                        'エラーデータを作成する
                        Call SetHanyouSiireErrData(i + 1, strReadLine, dtHanyouSiireErr)
                        Continue For
                    End If
                    '数値型項目チェック
                    If Not commonCheck.ChkSuuti(strReadLine, CsvInputCheck.SIIRE) Then
                        'エラーデータを作成する
                        Call SetHanyouSiireErrData(i + 1, strReadLine, dtHanyouSiireErr)
                        Continue For
                    End If
                    '必須チェック
                    If Not commonCheck.ChkNotNull(strReadLine, CsvInputCheck.SIIRE) Then
                        'エラーデータを作成する
                        Call SetHanyouSiireErrData(i + 1, strReadLine, dtHanyouSiireErr)
                        Continue For
                    End If

                    '日付チェック
                    If Not commonCheck.ChkNotDate(strReadLine, CsvInputCheck.SIIRE) Then
                        'エラーデータを作成する
                        Call SetHanyouSiireErrData(i + 1, strReadLine, dtHanyouSiireErr)
                        Continue For
                    End If


                    '存在チェック()
                    If Not ChkMstSonZai(strReadLine) Then
                        'エラーデータを作成する
                        Call SetHanyouSiireErrData(i + 1, strReadLine, dtHanyouSiireErr)
                        Continue For
                    End If

                    '行追加
                    Call SetHanyouSiireDataRow(strReadLine, dtHanyouSiireOk)
                End If
            Next
            'エラー有無フラグを設定する
            If dtHanyouSiireErr.Rows.Count > 0 Then
                strUmuFlg = "1"
            End If
            'CSVファイルを取込
            If Not CsvFileUpload(dtHanyouSiireOk, dtHanyouSiireErr, strUploadDate, strUserId, strNyuuryokuFileMei, strEdiJouhouSakuseiDate) Then
                Return Messages.Instance.MSG050E
            End If
        Catch ex As Exception
            Return Messages.Instance.MSG049E
        End Try

        Return String.Empty
    End Function
    ''' <summary>存在チェック</summary>
    Private Function ChkMstSonZai(ByVal intFieldCount As String) As Boolean
        Dim genkaMasterDA As New GenkaMasterDataAccess
        '調査会社（調査会社コード・事業所コード）
        If Not genkaMasterDA.SelTyousaKaisyaMeiInput(intFieldCount.Split(",")(5).Trim, intFieldCount.Split(",")(6).Trim) Then
            Return False
        End If

        '加盟店(加盟店コード)
        If intFieldCount.Split(",")(7).Trim <> "" Then
            If Not hanyouSiireDA.SelKameitenInput(intFieldCount.Split(",")(7).Trim) Then
                Return False
            End If
        End If

        '商品コード
        If Not genkaMasterDA.SelSyouhinCdInput(intFieldCount.Split(",")(8).Trim) Then
            Return False
        End If

        Return True
    End Function
    ''' <summary>汎用仕入テーブルを作成する</summary>
    ''' <param name="dtHanyouSiireOk">汎用仕入テーブル</param>
    Public Sub SetHanyouSiireOk(ByRef dtHanyouSiireOk As Data.DataTable)
        dtHanyouSiireOk.Columns.Add("torikesi")                     '取消
        dtHanyouSiireOk.Columns.Add("tekiyou")                      '摘要
        dtHanyouSiireOk.Columns.Add("siire_date")                   '仕入年月日
        dtHanyouSiireOk.Columns.Add("denpyou_siire_date")           '伝票仕入年月日
        dtHanyouSiireOk.Columns.Add("tys_kaisya_cd")                '調査会社コード		
        dtHanyouSiireOk.Columns.Add("tys_kaisya_jigyousyo_cd")      '調査会社事業所コード	
        dtHanyouSiireOk.Columns.Add("kameiten_cd")                  '加盟店コード		
        dtHanyouSiireOk.Columns.Add("syouhin_cd")                   '商品コード		
        dtHanyouSiireOk.Columns.Add("suu")                          '数量			
        dtHanyouSiireOk.Columns.Add("tanka")                        '単価
        dtHanyouSiireOk.Columns.Add("zei_kbn")                      '税区分			
        dtHanyouSiireOk.Columns.Add("syouhizei_gaku")               '消費税額		
        dtHanyouSiireOk.Columns.Add("siire_keijyou_flg")            '仕入処理FLG(売上計上FLG)	
        dtHanyouSiireOk.Columns.Add("siire_keijyou_date")           '仕入処理日(売上計上日)	
        dtHanyouSiireOk.Columns.Add("kbn")                          '区分
        dtHanyouSiireOk.Columns.Add("bangou")                       '番号
        dtHanyouSiireOk.Columns.Add("sesyu_mei")                    '施主名
    End Sub

    ''' <summary>汎用仕入エラーテーブルを作成する</summary>
    ''' <param name="dtHanyouSiireErr">汎用仕入エラーテーブル</param>
    Public Sub SetHanyouSiireErr(ByRef dtHanyouSiireErr As Data.DataTable)
        dtHanyouSiireErr.Columns.Add("edi_jouhou_sakusei_date")      'EDI情報作成日
        dtHanyouSiireErr.Columns.Add("torikesi")                     '取消
        dtHanyouSiireErr.Columns.Add("tekiyou")                      '摘要
        dtHanyouSiireErr.Columns.Add("siire_date")                   '仕入年月日
        dtHanyouSiireErr.Columns.Add("denpyou_siire_date")           '伝票仕入年月日
        dtHanyouSiireErr.Columns.Add("tys_kaisya_cd")                '調査会社コード		
        dtHanyouSiireErr.Columns.Add("tys_kaisya_jigyousyo_cd")      '調査会社事業所コード
        dtHanyouSiireErr.Columns.Add("kameiten_cd")                  '加盟店コード
        dtHanyouSiireErr.Columns.Add("syouhin_cd")                   '商品コード
        dtHanyouSiireErr.Columns.Add("suu")                          '数量			
        dtHanyouSiireErr.Columns.Add("tanka")                        '単価
        dtHanyouSiireErr.Columns.Add("zei_kbn")                      '税区分			
        dtHanyouSiireErr.Columns.Add("syouhizei_gaku")               '消費税額	
        dtHanyouSiireErr.Columns.Add("siire_keijyou_flg")            '仕入処理FLG(売上計上FLG)	
        dtHanyouSiireErr.Columns.Add("siire_keijyou_date")           '仕入処理日(売上計上日)	
        dtHanyouSiireErr.Columns.Add("kbn")                          '区分
        dtHanyouSiireErr.Columns.Add("bangou")                       '番号
        dtHanyouSiireErr.Columns.Add("sesyu_mei")                    '施主名
        dtHanyouSiireErr.Columns.Add("gyou_no")                      '行NO
    End Sub

    ''' <summary>汎用仕入エラーデータを作成する</summary>
    ''' <param name="intLineNo">CSVファイルの該当行の行NO</param>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    ''' <param name="dtHanyouSiireError">汎用仕入エラーデータ</param>
    Private Sub SetHanyouSiireErrData(ByVal intLineNo As Integer, _
                                       ByVal strLine As String, _
                                       ByRef dtHanyouSiireError As Data.DataTable)
        Dim intMaxCount As Integer
        Dim commonCheck As New CsvInputCheck
        Dim dr As Data.DataRow
        dr = dtHanyouSiireError.NewRow

        If strLine.Split(",").Length < CsvInputCheck.SIIRE_FIELD_COUNT Then
            intMaxCount = strLine.Split(",").Length
        Else
            intMaxCount = CsvInputCheck.SIIRE_FIELD_COUNT
        End If

        For i As Integer = 0 To intMaxCount - 1
            '最大長を切り取る
            dr.Item(i) = commonCheck.CutMaxLength(strLine.Split(",")(i).Trim, commonCheck.SIIRE_MAX_LENGTH(i))
        Next
        '行号
        dr.Item(CsvInputCheck.SIIRE_FIELD_COUNT) = CStr(intLineNo)

        dtHanyouSiireError.Rows.Add(dr)

    End Sub

    ''' <summary>汎用仕入データを作成する</summary>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    ''' <param name="dtHanyouSiireOk">汎用仕入データ</param>
    Public Sub SetHanyouSiireDataRow(ByVal strLine As String, _
                                     ByRef dtHanyouSiireOk As Data.DataTable)
        Dim dr As Data.DataRow
        dr = dtHanyouSiireOk.NewRow

     
        dr.Item("torikesi") = strLine.Split(",")(1).Trim
        dr.Item("tekiyou") = strLine.Split(",")(2).Trim                     '摘要
        dr.Item("siire_date") = strLine.Split(",")(3).Trim                  '仕入年月日
        dr.Item("denpyou_siire_date") = strLine.Split(",")(4).Trim          '伝票仕入年月日
        dr.Item("tys_kaisya_cd") = strLine.Split(",")(5).Trim               '調査会社コード
        dr.Item("tys_kaisya_jigyousyo_cd") = strLine.Split(",")(6).Trim     '調査会社事業所コード
        dr.Item("kameiten_cd") = strLine.Split(",")(7).Trim                 '加盟店コード
        dr.Item("syouhin_cd") = strLine.Split(",")(8).Trim                  '商品コード
        dr.Item("suu") = strLine.Split(",")(9).Trim                         '数量
        dr.Item("tanka") = strLine.Split(",")(10).Trim                      '単価
        dr.Item("zei_kbn") = strLine.Split(",")(11).Trim                    '税区分
        dr.Item("syouhizei_gaku") = strLine.Split(",")(12).Trim             '消費税額
        dr.Item("siire_keijyou_flg") = strLine.Split(",")(13).Trim          '仕入処理FLG
        dr.Item("siire_keijyou_date") = strLine.Split(",")(14).Trim         '仕入処理日
        dr.Item("kbn") = strLine.Split(",")(15).Trim                        '区分
        dr.Item("bangou") = strLine.Split(",")(16).Trim                     '番号
        dr.Item("sesyu_mei") = strLine.Split(",")(17).Trim                  '施主名

        dtHanyouSiireOk.Rows.Add(dr)
    End Sub

    ''' <summary>CSVファイルを取込する</summary>
    ''' <param name="dtHanyouSiireOk">汎用仕入データ</param>
    ''' <param name="dtHanyouSiireErr">汎用仕入エラーデータ</param>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <param name="strNyuuryokuFileMei">入力ファイル名</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <returns>成功・失敗区分</returns>
    Public Function CsvFileUpload(ByVal dtHanyouSiireOk As Data.DataTable, _
                                  ByVal dtHanyouSiireErr As Data.DataTable, _
                                  ByVal strUploadDate As String, _
                                  ByVal strUserId As String, _
                                  ByVal strNyuuryokuFileMei As String, _
                                  ByVal strEdiJouhouSakuseiDate As String) As Boolean

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                '汎用仕入マスタを登録する
                If dtHanyouSiireOk.Rows.Count > 0 Then
                    If Not hanyouSiireDA.InsHanyouSiire(dtHanyouSiireOk, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                '汎用仕入エラー情報テーブルを登録する
                If dtHanyouSiireErr.Rows.Count > 0 Then
                    If Not hanyouSiireDA.InsHanyouSiireErr(dtHanyouSiireErr, strUploadDate, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                'アップロード管理テーブルを登録する
                If Not hanyouSiireDA.InsUploadKanri(strUploadDate, strNyuuryokuFileMei, strEdiJouhouSakuseiDate, CInt(IIf(dtHanyouSiireErr.Rows.Count, 1, 0)), strUserId) Then
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

End Class
