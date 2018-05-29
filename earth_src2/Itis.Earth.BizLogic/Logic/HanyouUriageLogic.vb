Imports System.Transactions
Imports Itis.Earth.DataAccess

Public Class HanyouUriageLogic

    '汎用売上データ取込インスタンス生成 
    Private hanyouUriageDA As New HanyouUriageDataAccess

    ''' <summary>アップロード管理を取得する</summary>
    ''' <returns>アップロード管理データテーブル</returns>
    Public Function GetUploadKanri() As Data.DataTable
        Return hanyouUriageDA.SelUploadKanri()
    End Function

    ''' <summary>アップロード管理の件数を取得する</summary>
    ''' <returns>アップロード管理の件数</returns>
    Public Function GetUploadKanriCount() As Integer
        Return hanyouUriageDA.SelUploadKanriCount()
    End Function

    ''' <summary>汎用売上エラー情報を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <param name="intTopCount">取得の最大行数</param>
    ''' <returns>汎用売上エラーデータテーブル</returns>
    Public Function GetHanyouUriageErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String, ByVal intTopCount As Integer) As DataTable
        Return hanyouUriageDA.SelHanyouUriageErr(strEdiJouhouSakuseiDate, strSyoriDate, intTopCount)
    End Function

    ''' <summary>汎用売上エラー件数を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>汎用売上エラー件数</returns>
    Public Function GetHanyouUriageErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As Integer
        Return hanyouUriageDA.SelHanyouUriageErrCount(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function

    Public Function GetUploadKanri(ByVal strFileName As String) As Integer
        Dim HanyouSiireDataAccess As New HanyouSiireDataAccess
        Return HanyouSiireDataAccess.SelUploadKanri("6", strFileName)
    End Function

    Public Function ChkCsvFile(ByVal strCsvLine() As String, ByVal FileName As String, ByRef strUmuFlg As String) As String

        Dim strReadLine As String                               '取込ファイル読込み行


        Dim strNyuuryokuFileMei As String                       'CSVファイル名
        Dim strEdiJouhouSakuseiDate As String = String.Empty    'EDI情報作成日
        Dim dtHanyouUriageOk As New Data.DataTable               '汎用仕入マスタ
        Dim dtHanyouUriageErr As New Data.DataTable              '汎用仕入エラー
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
        Call SetHanyouUriageOk(dtHanyouUriageOk)
        '工事価格エラーテーブルを作成する
        Call SetHanyouUriageErr(dtHanyouUriageErr)

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
                '==========↓2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↓======================
                'If arrLine2(8).Trim = "0" Then
                '    arrLine2(6) = Right("00000" & arrLine2(6).Trim, 5)
                'End If
                'If arrLine2(8).Trim = "1" Then
                '    arrLine2(6) = Right("0000" & arrLine2(6).Trim, 4)
                'End If
                'arrLine2(7) = Right("00" & arrLine2(7).Trim, 2)
                If arrLine2(6).Trim = "0" Then '売上店区分が0の場合
                    arrLine2(7) = Right("00000" & arrLine2(7).Trim, 5) '売上店ｺｰﾄﾞ:前0埋め5桁変換
                End If
                If arrLine2(6).Trim = "1" Then '売上店区分が1の場合
                    arrLine2(7) = Right("000000" & arrLine2(7).Trim, 6) '売上店ｺｰﾄﾞ:前0埋め6桁変換
                End If
                If arrLine2(10).Trim = "0" Then '請求先区分が0の場合
                    arrLine2(8) = Right("00000" & arrLine2(8).Trim, 5) '請求先ｺｰﾄﾞ:前0埋め5桁変換
                End If
                If arrLine2(10).Trim = "1" Then '請求先区分が1の場合
                    arrLine2(8) = Right("0000" & arrLine2(8).Trim, 4) '請求先ｺｰﾄﾞ:前0埋め4桁変換
                End If
                arrLine2(9) = Right("00" & arrLine2(9).Trim, 2) '請求先枝番:前0埋め2桁変換
                '==========↑2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↑======================              
                strReadLine = String.Join(",", arrLine2)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                    'EDI情報作成日が前0埋め変換
                    If strReadLine.Split(",")(0).Length < 10 Then
                        Dim arrLine() As String = Split(strReadLine, ",")
                        arrLine(0) = Right("          " & arrLine(0).Trim, 10)
                        strReadLine = String.Join(",", arrLine)
                    End If
                    '桁数埋め変換
                    If strReadLine.Split(",").Length < CsvInputCheck.URIAGE_FIELD_COUNT Then
                        strReadLine = strReadLine & StrDup(CsvInputCheck.URIAGE_FIELD_COUNT - strReadLine.Split(",").Length, ",")
                    End If
                    'フィールド数チェック
                    If Not commonCheck.ChkFieldCount(strReadLine.Split(",").Length, CsvInputCheck.URIAGE) Then
                        'エラーデータを作成する
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If
                    '項目最大長チェック
                    If Not commonCheck.ChkMaxLength(strReadLine, CsvInputCheck.URIAGE) Then
                        'エラーデータを作成する
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If
                    '禁則文字チェック
                    If Not commonCheck.ChkKinjiMoji(strReadLine) Then
                        'エラーデータを作成する
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If
                    '数値型項目チェック
                    If Not commonCheck.ChkSuuti(strReadLine, CsvInputCheck.URIAGE) Then
                        'エラーデータを作成する
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If
                    '==========↓2015/11/19 chel1 追加↓======================
                    '半角英数字チェック
                    If Not commonCheck.ChkHankakuEisuuji(strReadLine, CsvInputCheck.URIAGE) Then
                        'エラーデータを作成する
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If
                    '==========↑2015/11/19 chel1 追加↑======================
                    '存在チェック
                    If Not ChkMstSonZai(strReadLine) Then
                        'エラーデータを作成する
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If
                    '必須チェック
                    If Not commonCheck.ChkNotNull(strReadLine, CsvInputCheck.URIAGE) Then
                        'エラーデータを作成する
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If

                    '日付チェック
                    If Not commonCheck.ChkNotDate(strReadLine, CsvInputCheck.URIAGE) Then
                        'エラーデータを作成する
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If


                    '行追加
                    Call SetHanyouUriageDataRow(strReadLine, dtHanyouUriageOk)
                End If
            Next
            'エラー有無フラグを設定する
            If dtHanyouUriageErr.Rows.Count > 0 Then
                strUmuFlg = "1"
            End If
            'CSVファイルを取込
            If Not CsvFileUpload(dtHanyouUriageOk, dtHanyouUriageErr, strUploadDate, strUserId, strNyuuryokuFileMei, strEdiJouhouSakuseiDate) Then
                Return Messages.Instance.MSG050E
            End If
        Catch ex As Exception
            Return Messages.Instance.MSG049E
        End Try

        Return String.Empty
    End Function
    ''' <summary>存在チェック</summary>
    Private Function ChkMstSonZai(ByRef intFieldCount As String) As Boolean

        '==========↓2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↓======================
        ''加盟店(加盟店コード)
        'If intFieldCount.Split(",")(7).Trim <> "" Then
        '    If Not hanyouUriageDA.SelSeikyuuSakiInput(intFieldCount.Split(",")(6).Trim, intFieldCount.Split(",")(7).Trim, intFieldCount.Split(",")(8).Trim) Then
        '        Return False
        '    End If
        'End If

        ''商品コード
        'Dim dtReturn As DataTable = hanyouUriageDA.SelSyouhinCdInput(intFieldCount.Split(",")(9).Trim)
        'If dtReturn.Rows.Count = 0 Then
        '    Return False
        'Else
        '    If intFieldCount.Split(",")(10).Trim = "" Then
        '        Dim arrLine() As String = Split(intFieldCount, ",")
        '        arrLine(10) = dtReturn.Rows(0).Item(0).ToString
        '        intFieldCount = String.Join(",", arrLine)
        '    End If
        'End If
        '加盟店(加盟店コード)
        If intFieldCount.Split(",")(9).Trim <> "" Then
            If Not hanyouUriageDA.SelSeikyuuSakiInput(intFieldCount.Split(",")(8).Trim, intFieldCount.Split(",")(9).Trim, intFieldCount.Split(",")(10).Trim) Then
                Return False
            End If
        End If

        '商品コード
        Dim dtReturn As DataTable = hanyouUriageDA.SelSyouhinCdInput(intFieldCount.Split(",")(11).Trim)
        If dtReturn.Rows.Count = 0 Then
            Return False
        Else
            If intFieldCount.Split(",")(12).Trim = "" Then
                Dim arrLine() As String = Split(intFieldCount, ",")
                arrLine(12) = dtReturn.Rows(0).Item(0).ToString
                intFieldCount = String.Join(",", arrLine)
            End If
        End If
        '==========↑2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↑======================

        Return True
    End Function
    ''' <summary>汎用売上テーブルを作成する</summary>
    ''' <param name="dtHanyouUriageOk">汎用売上テーブル</param>
    Public Sub SetHanyouUriageOk(ByRef dtHanyouUriageOk As Data.DataTable)
        dtHanyouUriageOk.Columns.Add("torikesi")                '取消
        dtHanyouUriageOk.Columns.Add("tekiyou")                 '摘要
        dtHanyouUriageOk.Columns.Add("uri_date")                '売上年月日
        dtHanyouUriageOk.Columns.Add("denpyou_uri_date")        '伝票売上年月日
        dtHanyouUriageOk.Columns.Add("seikyuu_date")            '請求年月日
        '==========↓2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↓======================
        dtHanyouUriageOk.Columns.Add("uriage_ten_kbn")          '売上店区分
        dtHanyouUriageOk.Columns.Add("uriage_ten_cd")           '売上店ｺｰﾄﾞ
        '==========↑2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↑======================
        dtHanyouUriageOk.Columns.Add("seikyuu_saki_cd")         '請求先コード	
        dtHanyouUriageOk.Columns.Add("seikyuu_saki_brc")        '請求先枝番	
        dtHanyouUriageOk.Columns.Add("seikyuu_saki_kbn")        '請求先区分
        dtHanyouUriageOk.Columns.Add("syouhin_cd")              '商品コード
        dtHanyouUriageOk.Columns.Add("hin_mei")                 '品名
        dtHanyouUriageOk.Columns.Add("suu")                     '数量			
        dtHanyouUriageOk.Columns.Add("tanka")                   '単価
        dtHanyouUriageOk.Columns.Add("syanai_genka")            '社内原価
        dtHanyouUriageOk.Columns.Add("zei_kbn")                 '税区分			
        dtHanyouUriageOk.Columns.Add("syouhizei_gaku")          '消費税額	
        dtHanyouUriageOk.Columns.Add("uri_keijyou_flg")         '売上処理FLG(売上計上FLG)	
        dtHanyouUriageOk.Columns.Add("uri_keijyou_date")        '売上処理日(売上計上日)	
        dtHanyouUriageOk.Columns.Add("kbn")                     '区分
        dtHanyouUriageOk.Columns.Add("bangou")                  '番号
        dtHanyouUriageOk.Columns.Add("sesyu_mei")               '施主名
    End Sub

    ''' <summary>汎用売上エラーテーブルを作成する</summary>
    ''' <param name="dtHanyouUriageErr">汎用売上エラーテーブル</param>
    Public Sub SetHanyouUriageErr(ByRef dtHanyouUriageErr As Data.DataTable)
        dtHanyouUriageErr.Columns.Add("edi_jouhou_sakusei_date")        'EDI情報作成日
        dtHanyouUriageErr.Columns.Add("torikesi")                       '取消
        dtHanyouUriageErr.Columns.Add("tekiyou")                        '摘要
        dtHanyouUriageErr.Columns.Add("uri_date")                       '売上年月日
        dtHanyouUriageErr.Columns.Add("denpyou_uri_date")               '伝票売上年月日
        dtHanyouUriageErr.Columns.Add("seikyuu_date")                   '請求年月日
        '==========↓2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↓======================
        dtHanyouUriageErr.Columns.Add("uriage_ten_kbn")          '売上店区分
        dtHanyouUriageErr.Columns.Add("uriage_ten_cd")           '売上店ｺｰﾄﾞ
        '==========↑2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↑======================
        dtHanyouUriageErr.Columns.Add("seikyuu_saki_cd")                '請求先コード	
        dtHanyouUriageErr.Columns.Add("seikyuu_saki_brc")               '請求先枝番	
        dtHanyouUriageErr.Columns.Add("seikyuu_saki_kbn")               '請求先区分
        dtHanyouUriageErr.Columns.Add("syouhin_cd")                     '商品コード
        dtHanyouUriageErr.Columns.Add("hin_mei")                        '品名
        dtHanyouUriageErr.Columns.Add("suu")                            '数量			
        dtHanyouUriageErr.Columns.Add("tanka")                          '単価
        dtHanyouUriageErr.Columns.Add("syanai_genka")                   '社内原価
        dtHanyouUriageErr.Columns.Add("zei_kbn")                        '税区分			
        dtHanyouUriageErr.Columns.Add("syouhizei_gaku")                 '消費税額	
        dtHanyouUriageErr.Columns.Add("uri_keijyou_flg")                '売上処理FLG(売上計上FLG)	
        dtHanyouUriageErr.Columns.Add("uri_keijyou_date")               '売上処理日(売上計上日)	
        dtHanyouUriageErr.Columns.Add("kbn")                            '区分
        dtHanyouUriageErr.Columns.Add("bangou")                         '番号
        dtHanyouUriageErr.Columns.Add("sesyu_mei")                      '施主名
        dtHanyouUriageErr.Columns.Add("gyou_no")                        '行NO
    End Sub

    ''' <summary>汎用売上エラーデータを作成する</summary>
    ''' <param name="intLineNo">CSVファイルの該当行の行NO</param>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    ''' <param name="dtHanyouUriageError">汎用売上エラーデータ</param>
    Private Sub SetHanyouUriageErrData(ByVal intLineNo As Integer, _
                                       ByVal strLine As String, _
                                       ByRef dtHanyouUriageError As Data.DataTable)
        Dim intMaxCount As Integer
        Dim commonCheck As New CsvInputCheck
        Dim dr As Data.DataRow
        dr = dtHanyouUriageError.NewRow

        If strLine.Split(",").Length < CsvInputCheck.URIAGE_FIELD_COUNT Then
            intMaxCount = strLine.Split(",").Length
        Else
            intMaxCount = CsvInputCheck.URIAGE_FIELD_COUNT
        End If

        For i As Integer = 0 To intMaxCount - 1
            '最大長を切り取る
            dr.Item(i) = commonCheck.CutMaxLength(strLine.Split(",")(i).Trim, commonCheck.URIAGE_MAX_LENGTH(i))
        Next
        '行号
        dr.Item(CsvInputCheck.URIAGE_FIELD_COUNT) = CStr(intLineNo)

        dtHanyouUriageError.Rows.Add(dr)

    End Sub

    ''' <summary>汎用売上データを作成する</summary>
    ''' <param name="strLine">CSVファイルの該当行のデータ</param>
    ''' <param name="dtHanyouUriageOk">汎用売上データ</param>
    Public Sub SetHanyouUriageDataRow(ByVal strLine As String, _
                                      ByRef dtHanyouUriageOk As Data.DataTable)
        Dim dr As Data.DataRow
        dr = dtHanyouUriageOk.NewRow
        dr.Item("torikesi") = strLine.Split(",")(1).Trim                '取消
        dr.Item("tekiyou") = strLine.Split(",")(2).Trim                     '摘要
        dr.Item("uri_date") = strLine.Split(",")(3).Trim                    '売上年月日
        dr.Item("denpyou_uri_date") = strLine.Split(",")(4).Trim            '伝票売上年月日
        dr.Item("seikyuu_date") = strLine.Split(",")(5).Trim                '請求年月日
        '==========↓2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↓======================
        'dr.Item("seikyuu_saki_cd") = strLine.Split(",")(6).Trim             '請求先コード
        'dr.Item("seikyuu_saki_brc") = strLine.Split(",")(7).Trim            '請求先枝番
        'dr.Item("seikyuu_saki_kbn") = strLine.Split(",")(8).Trim            '請求先区分
        'dr.Item("syouhin_cd") = strLine.Split(",")(9).Trim                  '商品コード
        'dr.Item("hin_mei") = strLine.Split(",")(10).Trim                    '商品コード
        'dr.Item("suu") = strLine.Split(",")(11).Trim                        '数量
        'dr.Item("tanka") = strLine.Split(",")(12).Trim                      '単価
        'dr.Item("syanai_genka") = strLine.Split(",")(13).Trim               '社内原価
        'dr.Item("zei_kbn") = strLine.Split(",")(14).Trim                    '税区分
        'dr.Item("syouhizei_gaku") = strLine.Split(",")(15).Trim             '消費税額
        'dr.Item("uri_keijyou_flg") = strLine.Split(",")(16).Trim            '売上処理FLG
        'dr.Item("uri_keijyou_date") = strLine.Split(",")(17).Trim           '売上処理日
        'dr.Item("kbn") = strLine.Split(",")(18).Trim                        '区分
        'dr.Item("bangou") = strLine.Split(",")(19).Trim                     '番号
        'dr.Item("sesyu_mei") = strLine.Split(",")(20).Trim                  '施主名
        dr.Item("uriage_ten_kbn") = strLine.Split(",")(6).Trim              '売上店区分
        dr.Item("uriage_ten_cd") = strLine.Split(",")(7).Trim               '売上店ｺｰﾄﾞ
        dr.Item("seikyuu_saki_cd") = strLine.Split(",")(8).Trim             '請求先コード
        dr.Item("seikyuu_saki_brc") = strLine.Split(",")(9).Trim            '請求先枝番
        dr.Item("seikyuu_saki_kbn") = strLine.Split(",")(10).Trim           '請求先区分
        dr.Item("syouhin_cd") = strLine.Split(",")(11).Trim                 '商品コード
        dr.Item("hin_mei") = strLine.Split(",")(12).Trim                    '商品コード
        dr.Item("suu") = strLine.Split(",")(13).Trim                        '数量
        dr.Item("tanka") = strLine.Split(",")(14).Trim                      '単価
        dr.Item("syanai_genka") = strLine.Split(",")(15).Trim               '社内原価
        dr.Item("zei_kbn") = strLine.Split(",")(16).Trim                    '税区分
        dr.Item("syouhizei_gaku") = strLine.Split(",")(17).Trim             '消費税額
        dr.Item("uri_keijyou_flg") = strLine.Split(",")(18).Trim            '売上処理FLG
        dr.Item("uri_keijyou_date") = strLine.Split(",")(19).Trim           '売上処理日
        dr.Item("kbn") = strLine.Split(",")(20).Trim                        '区分
        dr.Item("bangou") = strLine.Split(",")(21).Trim                     '番号
        dr.Item("sesyu_mei") = strLine.Split(",")(22).Trim                  '施主名
        '==========↑2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↑======================

        dtHanyouUriageOk.Rows.Add(dr)
    End Sub

    ''' <summary>CSVファイルを取込する</summary>
    ''' <param name="dtHanyouUriageOk">汎用売上データ</param>
    ''' <param name="dtHanyouUriageErr">汎用売上エラーデータ</param>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <param name="strNyuuryokuFileMei">入力ファイル名</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <returns>成功・失敗区分</returns>
    Public Function CsvFileUpload(ByVal dtHanyouUriageOk As Data.DataTable, _
                                  ByVal dtHanyouUriageErr As Data.DataTable, _
                                  ByVal strUploadDate As String, _
                                  ByVal strUserId As String, _
                                  ByVal strNyuuryokuFileMei As String, _
                                  ByVal strEdiJouhouSakuseiDate As String) As Boolean

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                '汎用売上マスタを登録する
                If dtHanyouUriageOk.Rows.Count > 0 Then
                    If Not hanyouUriageDA.InsHanyouUriage(dtHanyouUriageOk, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                '汎用売上エラー情報テーブルを登録する
                If dtHanyouUriageErr.Rows.Count > 0 Then
                    If Not hanyouUriageDA.InsHanyouUriageErr(dtHanyouUriageErr, strUploadDate, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                'アップロード管理テーブルを登録する
                If Not hanyouUriageDA.InsUploadKanri(strUploadDate, strNyuuryokuFileMei, strEdiJouhouSakuseiDate, CInt(IIf(dtHanyouUriageErr.Rows.Count, 1, 0)), strUserId) Then
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
