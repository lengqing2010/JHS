Imports Itis.Earth.DataAccess
Imports System.Transactions

Public Class KameitenMasterLogic

    Private kameitenMasterDA As New KameitenMasterDataAccess
    Private commonCheck As New CsvInputCheck
    Private ninsyou As New Ninsyou()
    Private userId As String = Ninsyou.GetUserID()
    Private InputDate As String

    'CSVアップロード上限件数
    Private CsvInputMaxLineCount As String = CStr(System.Configuration.ConfigurationSettings.AppSettings("CsvInputMaxLineCount"))

    ''' <summary>アップロード管理を取得する</summary>
    ''' <returns>アップロード管理データテーブル</returns>
    Public Function GetInputKanri() As Data.DataTable

        Return kameitenMasterDA.SelInputKanri()

    End Function

    ''' <summary>アップロード管理の件数を取得する</summary>
    ''' <returns>アップロード管理の件数データテーブル</returns>
    Public Function GetInputKanriCount() As Integer

        Return kameitenMasterDA.SelInputKanriCount()

    End Function

    ''' <summary>加盟店コードを取得する</summary>
    ''' <returns>加盟店コード</returns>
    Public Function GetKameitenCd(ByVal strKameitenCd As String, ByVal strKbn As String) As Boolean

        Return kameitenMasterDA.SelKameitenCd(strKameitenCd, strKbn)

    End Function

    ''' <summary>CSVファイルをチェックする</summary>
    ''' <returns>CSVファイルをチェックする</returns>
    Public Function ChkCsvFile(ByRef strUmuFlg As String, ByVal hidInsLineNo As String, ByVal arrCsvLine() As String, ByVal strFileMei As String) As String

        'Dim myStream As IO.Stream                           '入出力ストリーム
        'Dim myReader As IO.StreamReader                     'ストリームリーダー
        Dim strReadLine As String                           '取込ファイル読込み行
        Dim intLineCount As Integer = 0                     'ライン数
        Dim strCsvLine() As String                          'CSVファイル内容
        '  Dim strFileMei As String                            'CSVファイル名
        Dim strEdiJouhouSakuseiDate As String = String.Empty 'EDI情報作成日
        Dim dtOk As New Data.DataTable                      '加盟店マスタ
        Dim dtError As New Data.DataTable                   '加盟店エラー
        Dim dtInsKameiten As New Data.DataTable             ' 新規登録となる加盟店ｺｰﾄﾞが重複してない場合、登録処理を続ける
        Dim strMsg As String = ""
        '加盟店商品調査方法特別対応マスタを作成
        CreateOkDataTable(dtOk)
        CreateOkDataTable(dtInsKameiten)
        '加盟店商品調査方法特別対応エラーテーブルを作成
        CreateErrorDataTable(dtError)

        '=========2012/05/31 車龍 407553の対応 追加↓===================================
        Dim dtError1 As New Data.DataTable
        CreateErrorDataTable(dtError1)

        '=========2012/05/31 車龍 407553の対応 追加↑===================================
        Try
            'If arrCsvLine Is Nothing Then
            '    'システムDate
            InputDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")

            '    'CSVファイル名
            'strFileMei = strFileMei

            '    '入出力ストリーム
            '    myStream = fupId.FileContent

            '    'ストリームリーダー
            '    myReader = New IO.StreamReader(myStream, System.Text.Encoding.GetEncoding(932))


            '    Do
            '        '取込ファイルを読み込む
            '        ReDim Preserve strCsvLine(intLineCount)
            '        strCsvLine(intLineCount) = myReader.ReadLine()
            '        intLineCount += 1
            '    Loop Until myReader.EndOfStream

            '    'CSVアップロード上限件数チェック
            '    For i As Integer = strCsvLine.Length - 1 To 0 Step -1
            '        If Not strCsvLine(i).Trim.Equals(String.Empty) Then
            '            If i > CsvInputMaxLineCount Then
            '                Return String.Format(Messages.Instance.MSG040E, CStr(CsvInputMaxLineCount))
            '            ElseIf i < 1 Then
            '                Return String.Format(Messages.Instance.MSG048E)
            '            Else
            '                Exit For
            '            End If
            '        End If
            '    Next
            'Else
            strCsvLine = arrCsvLine
            'End If
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
                    If strReadLine.Split(",").Length < CsvInputCheck.KAMEITEN_FIELD_COUNT Then
                        strReadLine = strReadLine & StrDup(CsvInputCheck.KAMEITEN_FIELD_COUNT - strReadLine.Split(",").Length, ",")
                    End If

                    'フィールド数チェック
                    If Not commonCheck.ChkFieldCount(strReadLine.Split(",").Length, CsvInputCheck.KAMEITEN) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    strReadLine = SetUmekusa(strReadLine)

                    '存在チェック
                    If Not Me.ChkSonZai(strReadLine, strMsg) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '項目最大長チェック
                    If Not commonCheck.ChkMaxLength(strReadLine, CsvInputCheck.KAMEITEN) Then
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
                    If Not commonCheck.ChkSuuti(strReadLine, CsvInputCheck.KAMEITEN) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If


                    If Not commonCheck.ChkNotDate(strReadLine, CsvInputCheck.KAMEITEN) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If
                    '必須チェック
                    If Not commonCheck.ChkNotNull(strReadLine, CsvInputCheck.KAMEITEN) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '===========2012/05/08 車龍 407553の対応 追加↓============================
                    '更新日時チェック()
                    If Not Me.ChkUpdDate(strReadLine) Then
                        'エラーデータの処理
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If
                    '===========2012/05/08 車龍 407553の対応 追加↑============================

                    '===========2012/05/23 車龍 407553の対応 追加↓============================
                    '加盟店コード
                    Dim strKameitenCd As String = strReadLine.Split(",")(2).Trim
                    '加盟店の更新日時
                    Dim strKameitenUpdDate As String = strReadLine.Split(",")(91).Trim
                    '加盟店マスタの更新日時
                    Dim dtMstUpdDate As New Data.DataTable
                    dtMstUpdDate = kameitenMasterDA.SelKameitenUpdDate(strKameitenCd)
                    Dim strKameitenMstUpdDate As String

                    If dtMstUpdDate.Rows.Count > 0 Then
                        strKameitenMstUpdDate = kameitenMasterDA.SelKameitenUpdDate(strKameitenCd).Rows(0).Item(0).ToString.Trim
                    Else
                        strKameitenMstUpdDate = String.Empty
                    End If

                    If (Not strKameitenUpdDate.Equals(String.Empty)) OrElse _
                        ((strKameitenUpdDate.Equals(String.Empty)) AndAlso (strKameitenMstUpdDate.Equals(String.Empty))) Then
                        '===========2012/05/23 車龍 407553の対応 追加↑============================
                        If hidInsLineNo.IndexOf(i & ",") <> -1 Then
                            '合格データの処理(insert)
                            If hidInsLineNo.IndexOf(i & ",") = 0 Then
                                Call Me.OkLineSyori(strReadLine, dtInsKameiten)
                            Else
                                If hidInsLineNo.Substring((hidInsLineNo.IndexOf(i & ",") - 1), 1) = "," Then
                                    Call Me.OkLineSyori(strReadLine, dtInsKameiten)
                                Else
                                    Call Me.OkLineSyori(strReadLine, dtOk)
                                End If
                            End If
                        Else
                            '合格データの処理(update)
                            Call Me.OkLineSyori(strReadLine, dtOk)
                        End If




                        '===========2012/05/23 車龍 407553の対応 追加↓============================
                    Else
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError1)
                        Continue For
                    End If
                    '===========2012/05/23 車龍 407553の対応 追加↑============================

                    'For intLoopI As Integer = 0 To hidInsLineNo.Split(",").Length - 1
                    '    If i = hidInsLineNo.Split(",")(intLoopI) Then
                    '        Me.SetOkDataRow(strReadLine, dtInsKameiten, "1")
                    '        Exit For
                    '    End If
                    'Next



                End If
            Next

            '===========2012/05/31 車龍 407553の対応 追加↓============================
            If dtError1.Rows.Count > 0 Then
                dtOk.Rows.Clear()
                dtInsKameiten.Rows.Clear()

                If Not CsvFileInput(dtOk, dtError1, InputDate, userId, strFileMei, strEdiJouhouSakuseiDate, dtInsKameiten) Then
                    Return Messages.Instance.MSG050E
                Else
                    Return Messages.Instance.MSG2073E
                End If

            End If
            '===========2012/05/31 車龍 407553の対応 追加↑============================

            'エラー有無フラグを設定する
            If dtError.Rows.Count > 0 Then
                strUmuFlg = "1"
            End If

            'CSVファイルを取込



            If Not CsvFileInput(dtOk, dtError, InputDate, userId, strFileMei, strEdiJouhouSakuseiDate, dtInsKameiten) Then
                If strMsg <> "" Then
                    Return strMsg
                Else
                    Return Messages.Instance.MSG050E
                End If

            Else
                Return strMsg
            End If

        Catch ex As Exception
            Return Messages.Instance.MSG049E
        End Try

    End Function

    ''' <summary>
    ''' 加盟店コードを"0"で埋める
    ''' </summary>
    Public Function SetUmekusa(ByVal StrLine As String) As String

        Dim arrLine() As String = StrLine.Split(",")
        '加盟店ｺｰﾄﾞ（※ 前0埋め5桁変換）
        If Not String.IsNullOrEmpty(arrLine(2).ToString) Then
            arrLine(2) = Right("00000" & arrLine(2).Trim, 5)
        End If

        '系列ｺｰﾄﾞ（※ 前0埋め4桁変換）
        If Not String.IsNullOrEmpty(arrLine(11).ToString) Then
            If arrLine(1).Trim.ToUpper = "K" Then
                arrLine(11) = Right("00000" & arrLine(11).Trim, 5)
            Else
                arrLine(11) = Right("0000" & arrLine(11).Trim, 4)
            End If

        End If

        '営業所ｺｰﾄﾞ（※ 前0埋め4桁変換）
        If Not String.IsNullOrEmpty(arrLine(13).ToString) Then
            arrLine(13) = Right("0000" & arrLine(13).Trim, 4)
        End If

        '棟区分1、2、3商品ｺｰﾄﾞ（※ 前0埋め5桁変換）
        If Not String.IsNullOrEmpty(arrLine(30).ToString) Then
            arrLine(30) = Right("00000" & arrLine(30).Trim, 5)
        End If
        If Not String.IsNullOrEmpty(arrLine(32).ToString) Then
            arrLine(32) = Right("00000" & arrLine(32).Trim, 5)
        End If
        If Not String.IsNullOrEmpty(arrLine(34).ToString) Then
            arrLine(34) = Right("00000" & arrLine(34).Trim, 5)
        End If

        '追加_備考種別①～⑤（※ 前0埋め2桁変換）
        If Not String.IsNullOrEmpty(arrLine(96).ToString) Then
            arrLine(96) = Right("00" & arrLine(96).Trim, 2)
        End If
        If Not String.IsNullOrEmpty(arrLine(98).ToString) Then
            arrLine(98) = Right("00" & arrLine(98).Trim, 2)
        End If
        If Not String.IsNullOrEmpty(arrLine(100).ToString) Then
            arrLine(100) = Right("00" & arrLine(100).Trim, 2)
        End If
        If Not String.IsNullOrEmpty(arrLine(102).ToString) Then
            arrLine(102) = Right("00" & arrLine(102).Trim, 2)
        End If
        If Not String.IsNullOrEmpty(arrLine(104).ToString) Then
            arrLine(104) = Right("00" & arrLine(104).Trim, 2)
        End If


        '(※調査請求先 区分0の場合:前0埋め5桁変換 区分1の場合:前0埋め4桁変換)
        If Not String.IsNullOrEmpty(arrLine(37).ToString) Then
            If "0".Equals(arrLine(36).ToString) Then
                arrLine(37) = Right("00000" & arrLine(37).Trim, 5)
            End If
            If "1".Equals(arrLine(36).ToString) Then
                arrLine(37) = Right("0000" & arrLine(37).Trim, 4)
            End If

        End If

        '調査請求先枝番※ 前0埋め2桁変換）
        If Not String.IsNullOrEmpty(arrLine(38).ToString) Then
            arrLine(38) = Right("00" & arrLine(38).Trim, 2)
        End If

        '工事請求先コード(※工事請求先 区分0の場合:前0埋め5桁変換区分1の場合:前0埋め4桁変換)
        If Not String.IsNullOrEmpty(arrLine(41).ToString) Then
            If "0".Equals(arrLine(40).ToString) Then
                arrLine(41) = Right("00000" & arrLine(41).Trim, 5)
            End If
            If "1".Equals(arrLine(40).ToString) Then
                arrLine(41) = Right("0000" & arrLine(41).Trim, 4)
            End If
        End If
        '工事請求先枝番（※ 前0埋め2桁変換）
        If Not String.IsNullOrEmpty(arrLine(42).ToString) Then
            arrLine(42) = Right("00" & arrLine(42).Trim, 2)
        End If

        ' 販促品請求先コード(※販促品請求先 区分0の場合:前0埋め5桁変換 区分1の場合:前0埋め4桁変換)
        If Not String.IsNullOrEmpty(arrLine(45).ToString) Then
            If "0".Equals(arrLine(44).ToString) Then
                arrLine(45) = Right("00000" & arrLine(45).Trim, 5)
            End If
            If "1".Equals(arrLine(44).ToString) Then
                arrLine(45) = Right("0000" & arrLine(45).Trim, 4)
            End If
        End If
        '販促品請求先枝番（※ 前0埋め2桁変換）
        If Not String.IsNullOrEmpty(arrLine(46).ToString) Then
            arrLine(46) = Right("00" & arrLine(46).Trim, 2)
        End If

        '建物検査請求先コード(※建物検査請求先 区分0の場合:前0埋め5桁変換 区分1の場合:前0埋め4桁変換)
        If Not String.IsNullOrEmpty(arrLine(49).ToString) Then
            If "0".Equals(arrLine(48).ToString) Then
                arrLine(49) = Right("00000" & arrLine(49).Trim, 5)
            End If
            If "1".Equals(arrLine(48).ToString) Then
                arrLine(49) = Right("0000" & arrLine(49).Trim, 4)
            End If
        End If
        '建物検査請求先枝番（※ 前0埋め2桁変換）
        If Not String.IsNullOrEmpty(arrLine(50).ToString) Then
            arrLine(50) = Right("00" & arrLine(50).Trim, 2)
        End If

        '請求先コード5(※請求先区分5が　0の場合:前0埋め5桁変換 1の場合:前0埋め4桁変換)
        If Not String.IsNullOrEmpty(arrLine(53).ToString) Then
            If "0".Equals(arrLine(52).ToString) Then
                arrLine(53) = Right("00000" & arrLine(53).Trim, 5)
            End If
            If "1".Equals(arrLine(52).ToString) Then
                arrLine(53) = Right("0000" & arrLine(53).Trim, 4)
            End If
        End If
        '請求先枝番5※ 前0埋め2桁変換）
        If Not String.IsNullOrEmpty(arrLine(54).ToString) Then
            arrLine(54) = Right("00" & arrLine(54).Trim, 2)
        End If

        '請求先コード6(※請求先区分6が　0の場合:前0埋め5桁変換 1の場合:前0埋め4桁変換)
        If Not String.IsNullOrEmpty(arrLine(57).ToString) Then
            If "0".Equals(arrLine(56).ToString) Then
                arrLine(57) = Right("00000" & arrLine(57).Trim, 5)
            End If
            If "1".Equals(arrLine(56).ToString) Then
                arrLine(57) = Right("0000" & arrLine(57).Trim, 4)
            End If
        End If
        '請求先枝番6※ 前0埋め2桁変換）
        If Not String.IsNullOrEmpty(arrLine(58).ToString) Then
            arrLine(58) = Right("00" & arrLine(58).Trim, 2)
        End If

        '請求先コード7(※請求先区分7が　0の場合:前0埋め5桁変換 1の場合:前0埋め4桁変換)
        If Not String.IsNullOrEmpty(arrLine(61).ToString) Then
            If "0".Equals(arrLine(60).ToString) Then
                arrLine(61) = Right("00000" & arrLine(61).Trim, 5)
            End If
            If "1".Equals(arrLine(60).ToString) Then
                arrLine(61) = Right("0000" & arrLine(61).Trim, 4)
            End If
        End If
        '請求先枝番7※ 前0埋め2桁変換）
        If Not String.IsNullOrEmpty(arrLine(62).ToString) Then
            arrLine(62) = Right("00" & arrLine(62).Trim, 2)
        End If

        '都道府県ｺｰﾄﾞ（※前0埋め2桁変換）
        If Not String.IsNullOrEmpty(arrLine(17).ToString) Then
            arrLine(17) = Right("00" & arrLine(17).Trim, 2)
        End If

        'ﾋﾞﾙﾀﾞｰNO（※前0埋め5桁変換）
        '================2012/04/06 車龍 要望の対応 修正↓============================
        'If Not String.IsNullOrEmpty(arrLine(9).ToString) Then
        '    arrLine(9) = Right("00000" & arrLine(9).Trim, 5)
        'End If
        If Not String.IsNullOrEmpty(arrLine(9).ToString) Then
            If (arrLine(9).Trim.Length < 5) AndAlso commonCheck.CheckHankaku(arrLine(9).Trim) Then
                arrLine(9) = Right("00000" & arrLine(9).Trim, 5)
            End If
        End If
        '================2012/04/06 車龍 要望の対応 修正↑============================

        '郵便番号※***-****　前半の***部分で3桁取れていない場合　前半0埋め3桁変換
        If Not String.IsNullOrEmpty(arrLine(70).ToString) Then
            arrLine(70) = Right("000" & arrLine(70).Trim, 8)
        End If

        '電話番号※先頭の文字が0以外の数字の場合先頭に0を加える
        If Not String.IsNullOrEmpty(arrLine(77).ToString) AndAlso Not "0".Equals(Left(arrLine(77).Trim, 1)) Then
            arrLine(77) = "0" & arrLine(77).Trim
        End If

        'FAX番号※先頭の文字が0以外の数字の場合先頭に0を加える
        If Not String.IsNullOrEmpty(arrLine(78).ToString) AndAlso Not "0".Equals(Left(arrLine(78).Trim, 1)) Then
            arrLine(78) = "0" & arrLine(78).Trim
        End If

        Return String.Join(",", arrLine)

    End Function
    ''' <summary>CSVファイルを取込する</summary>
    ''' <returns>CSVファイルを取込する</returns>
    Public Function CsvFileInput(ByVal dtOk As Data.DataTable, ByVal dtError As Data.DataTable, ByVal InputDate As String, ByVal userId As String, ByVal strFileMei As String, ByVal strEdiJouhouSakuseiDate As String, ByVal dtInsKameiten As Data.DataTable) As Boolean

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew, New System.TimeSpan(0, 10, 0))
            Try
                '加盟店調査方法特別対応マスタを登録()
                If dtOk.Rows.Count > 0 OrElse dtInsKameiten.Rows.Count > 0 Then
                    If Not SuccessSyori(dtOk, dtInsKameiten) Then
                        Throw New ApplicationException
                    End If

                End If

                '加盟店情報一括取込エラー情報テーブルを登録()
                If dtError.Rows.Count > 0 Then
                    If Not kameitenMasterDA.InstKameitenInfoIttukatuError(dtError) Then
                        Throw New ApplicationException
                    End If
                End If

                'アップロード管理テーブルを登録()
                If Not kameitenMasterDA.InsInputKanri(InputDate, strFileMei, strEdiJouhouSakuseiDate, CInt(IIf(dtError.Rows.Count, 1, 0)), userId) Then
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
    Private Sub AddMsg(ByRef strMsg As String, ByVal strErr As String)
        If strMsg = "" Then
            strMsg = strErr
        Else
            strMsg = strMsg & "\r\n" & strErr
        End If
    End Sub
    Private Sub CHKSeikyuu(ByRef intFieldCount As String, ByVal intSeikyuu As Integer, ByRef strMsg As String, ByVal strErrMsg As String, ByRef successFlg As Boolean)
        Dim strMei As String = ""
        Dim arrLine2() As String = Split(intFieldCount, ",")

        '調査請求先区分・調査請求先ｺｰﾄﾞ・調査請求先枝番
        If intFieldCount.Split(",")(intSeikyuu).Trim = "0" Then
            If Not kameitenMasterDA.SelKameitenCd(intFieldCount.Split(",")(intSeikyuu + 1).Trim, , strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, _
                intFieldCount.Split(",")(intSeikyuu).Trim & "・" & intFieldCount.Split(",")(intSeikyuu + 1).Trim & "・" & intFieldCount.Split(",")(intSeikyuu + 2).Trim, _
                "加盟店", strErrMsg))
                successFlg = False
            Else
                If arrLine2(intSeikyuu + 3) = "" Then
                    arrLine2(intSeikyuu + 3) = strMei
                End If
            End If
        End If
        If intFieldCount.Split(",")(intSeikyuu).Trim = "1" Then
            If Not kameitenMasterDA.SelTyousakaisya(intFieldCount.Split(",")(intSeikyuu + 1).Trim, intFieldCount.Split(",")(intSeikyuu + 2).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, _
                intFieldCount.Split(",")(intSeikyuu).Trim & "・" & intFieldCount.Split(",")(intSeikyuu + 1).Trim & "・" & intFieldCount.Split(",")(intSeikyuu + 2).Trim, _
                "調査会社", strErrMsg))
                successFlg = False
            Else
                If arrLine2(intSeikyuu + 3) = "" Then
                    arrLine2(intSeikyuu + 3) = strMei
                End If
            End If
        End If
        intFieldCount = String.Join(",", arrLine2)
    End Sub
    ''' <summary>存在チェック</summary>
    Private Function ChkSonZai(ByRef intFieldCount As String, ByRef strMsg As String) As Boolean
        Dim arrLine2() As String = Split(intFieldCount, ",")
        Dim strMei As String = ""

        Dim successFlg As Boolean = True


        'ﾋﾞﾙﾀﾞｰNO
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(9).Trim) Then
            If Not kameitenMasterDA.SelKameitenCd(intFieldCount.Split(",")(9).Trim, strMei) Then
                '=================2012/03/09 車龍 エラーの対応 削除↓===========================
                'Return False
                '=================2012/03/09 車龍 エラーの対応 削除↑===========================
            Else
                If arrLine2(10).Trim = "" Then
                    arrLine2(10) = strMei
                End If
            End If
        End If

        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(17).Trim) Then
            If Not kameitenMasterDA.SelTodoufuken(intFieldCount.Split(",")(17).Trim, strMei) Then

                Return False
            Else
                If arrLine2(18).Trim = "" Then
                    arrLine2(18) = strMei
                End If
            End If
        End If

        '系列ｺｰﾄﾞ
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(11).Trim) Then
            If Not kameitenMasterDA.SelKeiretu(intFieldCount.Split(",")(11).Trim, intFieldCount.Split(",")(1).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(11).Trim, "系列", "系列ｺｰﾄﾞ"))
                successFlg = False
            Else
                If arrLine2(12).Trim = "" Then
                    arrLine2(12) = strMei
                End If
            End If
        End If

        '営業所ｺｰﾄﾞ
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(13).Trim) Then
            If Not kameitenMasterDA.SelEigyousyo(intFieldCount.Split(",")(13).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(13).Trim, "営業所", "営業所ｺｰﾄﾞ"))
                successFlg = False
            Else
                If arrLine2(14).Trim = "" Then
                    arrLine2(14) = strMei
                End If
            End If
        End If
        '営業担当者
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(22).Trim) Then
            If Not kameitenMasterDA.SelAccount(intFieldCount.Split(",")(22).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(22).Trim, "地盤認証", "営業担当者"))
                successFlg = False
            Else
                If arrLine2(23).Trim = "" Then
                    arrLine2(23) = strMei
                End If
            End If
        End If

        '旧営業担当者
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(24).Trim) Then
            If Not kameitenMasterDA.SelAccount(intFieldCount.Split(",")(24).Trim, strMei) Then
                '============2012/06/01 車龍 407553の対応 削除↓===========================
                'AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(24).Trim, "地盤認証", "旧営業担当者"))
                'successFlg = False
                '============2012/06/01 車龍 407553の対応 削除↑===========================
            Else
                If arrLine2(25).Trim = "" Then
                    arrLine2(25) = strMei
                End If
            End If
        End If

        '棟区分1、2、3商品コード
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(30).Trim) Then
            If Not kameitenMasterDA.SelSyouhinCd(intFieldCount.Split(",")(30).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(30).Trim, "商品", "棟区分1"))
                successFlg = False
            Else
                If arrLine2(31).Trim = "" Then
                    arrLine2(31) = strMei
                End If
            End If
        End If
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(32).Trim) Then
            If Not kameitenMasterDA.SelSyouhinCd(intFieldCount.Split(",")(32).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(32).Trim, "商品", "棟区分2"))
                successFlg = False
            Else
                If arrLine2(33).Trim = "" Then
                    arrLine2(33) = strMei
                End If
            End If
        End If
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(34).Trim) Then
            If Not kameitenMasterDA.SelSyouhinCd(intFieldCount.Split(",")(34).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(34).Trim, "商品", "棟区分3"))
                successFlg = False
            Else
                If arrLine2(35).Trim = "" Then
                    arrLine2(35) = strMei
                End If
            End If
        End If

        '==================2012/03/13 車龍 追加↓====================================
        'ｱｯﾌﾟﾛｰﾄﾞﾌｧｲﾙ.棟区分1、2、3商品ｺｰﾄﾞ（※ 前0埋め5桁変換）に入力がある際、
        'そのｺｰﾄﾞが商品ﾏｽﾀ.souko_cd = "115'　に存在しない　もしくは　そのｺｰﾄﾞが'00000' ではない場合は、エラーです。

        '棟区分n商品ｺｰﾄﾞ
        Dim strSyouhinCd As String

        '棟区分1
        strSyouhinCd = intFieldCount.Split(",")(30).Trim
        If Not String.IsNullOrEmpty(strSyouhinCd) Then
            If (Not strSyouhinCd.Equals("00000")) AndAlso (Not kameitenMasterDA.SelSoukoCheck(strSyouhinCd)) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG055E, strSyouhinCd.Trim, "棟区分1"))
                successFlg = False
            End If
        End If
        '棟区分2
        strSyouhinCd = intFieldCount.Split(",")(32).Trim
        If Not String.IsNullOrEmpty(strSyouhinCd) Then
            If (Not strSyouhinCd.Equals("00000")) AndAlso (Not kameitenMasterDA.SelSoukoCheck(strSyouhinCd)) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG055E, strSyouhinCd, "棟区分2"))
                successFlg = False
            End If
        End If
        '棟区分3
        strSyouhinCd = intFieldCount.Split(",")(34).Trim
        If Not String.IsNullOrEmpty(strSyouhinCd) Then
            If (Not strSyouhinCd.Equals("00000")) AndAlso (Not kameitenMasterDA.SelSoukoCheck(strSyouhinCd)) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG055E, strSyouhinCd, "棟区分3"))
                successFlg = False
            End If
        End If
        '==================2012/03/13 車龍 追加↑====================================

        intFieldCount = String.Join(",", arrLine2)

        CHKSeikyuu(intFieldCount, 36, strMsg, "調査請求先区分・調査請求先ｺｰﾄﾞ・調査請求先枝番", successFlg)

        CHKSeikyuu(intFieldCount, 40, strMsg, "工事請求先区分・工事請求先ｺｰﾄﾞ・工事請求先枝番", successFlg)

        CHKSeikyuu(intFieldCount, 44, strMsg, "販促品請求先区分・販促品請求先ｺｰﾄﾞ・販促品請求先枝番", successFlg)

        CHKSeikyuu(intFieldCount, 48, strMsg, "建物検査請求先区分・建物検査請求先ｺｰﾄﾞ・建物検査請求先枝番", successFlg)

        CHKSeikyuu(intFieldCount, 52, strMsg, "請求先区分5・請求先ｺｰﾄﾞ5・請求先枝番5", successFlg)

        CHKSeikyuu(intFieldCount, 56, strMsg, "請求先区分6・請求先ｺｰﾄﾞ6・請求先枝番6", successFlg)

        CHKSeikyuu(intFieldCount, 60, strMsg, "請求先区分7・請求先ｺｰﾄﾞ7・請求先枝番7", successFlg)
        arrLine2 = Split(intFieldCount, ",")

        '==========↓2013/03/08 車龍 407584 追加↓======================
        '商品コード(ｱｯﾌﾟﾛｰﾄﾞﾌｧｲﾙ.商品コード　が　商品ﾏｽﾀ.倉庫ｺｰﾄﾞ =　200 以外の場合)
        If Not String.IsNullOrEmpty(arrLine2(84).Trim) Then
            If Not kameitenMasterDA.SelSyouhinSoukoCdCheck(arrLine2(84).Trim, "200") Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG2075E, arrLine2(84).Trim))
                successFlg = False
            End If
        End If
        '==========↑2013/03/08 車龍 407584 追加↑======================

        '追加_備考種別
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(96).Trim) Then
            If Not kameitenMasterDA.SelMeisyouCd(intFieldCount.Split(",")(96).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(96).Trim, "名称", "追加_備考種別①"))
                successFlg = False
            Else
                If arrLine2(97).Trim = "" Then
                    arrLine2(97) = strMei
                End If
            End If
        End If
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(98).Trim) Then
            If Not kameitenMasterDA.SelMeisyouCd(intFieldCount.Split(",")(98).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(98).Trim, "名称", "追加_備考種別②"))
                successFlg = False
            Else
                If arrLine2(99).Trim = "" Then
                    arrLine2(99) = strMei
                End If
            End If
        End If
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(100).Trim) Then
            If Not kameitenMasterDA.SelMeisyouCd(intFieldCount.Split(",")(100).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(100).Trim, "名称", "追加_備考種別③"))
                successFlg = False
            Else
                If arrLine2(101).Trim = "" Then
                    arrLine2(101) = strMei
                End If
            End If
        End If
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(102).Trim) Then
            If Not kameitenMasterDA.SelMeisyouCd(intFieldCount.Split(",")(102).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(102).Trim, "名称", "追加_備考種別④"))
                successFlg = False
            Else
                If arrLine2(103).Trim = "" Then
                    arrLine2(103) = strMei
                End If
            End If
        End If
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(104).Trim) Then
            If Not kameitenMasterDA.SelMeisyouCd(intFieldCount.Split(",")(104).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(104).Trim, "名称", "追加_備考種別⑤"))
                successFlg = False
            Else
                If arrLine2(105).Trim = "" Then
                    arrLine2(105) = strMei
                End If
            End If
        End If

        intFieldCount = String.Join(",", arrLine2)

        If successFlg Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>加盟店存在チェック</summary>
    Public Function ChkKameiten(ByVal fupId As Web.UI.WebControls.FileUpload, _
                                ByRef arrCsvLine() As String, _
                                ByRef insCd As String, ByRef updCd As String, ByRef hidInsLineNo As String) As String


        Dim myStream As IO.Stream                           '入出力ストリーム
        Dim myReader As IO.StreamReader                     'ストリームリーダー
        Dim strReadLine As String                           '取込ファイル読込み行
        Dim intLineCount As Integer = 0                     'ライン数
        Dim strCsvLine() As String                          'CSVファイル内容
        Dim strRtn As String = String.Empty

        'CSV未取込場合
        If arrCsvLine Is Nothing Then
            '入出力ストリーム
            myStream = fupId.FileContent

            'ストリームリーダー
            myReader = New IO.StreamReader(myStream, System.Text.Encoding.GetEncoding(932))

            Try
                'CSV取込
                Do
                    '取込ファイルを読み込む
                    ReDim Preserve strCsvLine(intLineCount)
                    strCsvLine(intLineCount) = myReader.ReadLine()
                    intLineCount += 1
                Loop Until myReader.EndOfStream

                '取込ファイル保存
                arrCsvLine = strCsvLine

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
            Catch ex As Exception
                Return Messages.Instance.MSG049E
            End Try
        Else
            strCsvLine = arrCsvLine
        End If



        'CSVファイルをチェック
        'For i As Integer = intLineNo To strCsvLine.Length - 1
        '    strReadLine = strCsvLine(i)
        '    If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then


        '        'ｯﾌﾟﾛｰﾄﾞﾌｧｲﾙ.区分-ｱｯﾌﾟﾛｰﾄﾞﾌｧｲﾙ.加盟店ｺｰﾄﾞ（※ 前0埋め5桁変換） の組合せが加盟店ﾏｽﾀに存在しない場合
        '        If Not String.IsNullOrEmpty(strReadLine.Split(",")(2).Trim) AndAlso _
        '           Not kameitenMasterDA.SelKameitenCd(Right("00000" & strReadLine.Split(",")(2).Trim, 5), strReadLine.Split(",")(1).Trim) Then


        '            intLineNo = i
        '            kbnAndKameitenCd = strReadLine.Split(",")(1).Trim & "," & strReadLine.Split(",")(2).Trim
        '            exitsFlg = kameitenMasterDA.SelKameitenCd(Right("00000" & strReadLine.Split(",")(2).Trim, 5))
        '            If Not exitsFlg Then
        '                hidInsLineNo = hidInsLineNo & i & ","
        '                Return "UnExits"
        '            Else
        '                Return "Err"
        '            End If

        '        End If
        '    End If
        'Next


        'whereStr

        Dim whereStr As New System.Text.StringBuilder

        For i As Integer = 1 To strCsvLine.Length - 1
            strReadLine = strCsvLine(i)
            If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                Dim kameitenCd As String = Right("00000" & strReadLine.Split(",")(2).Trim, 5)
                Dim kbn As String = strReadLine.Split(",")(1).Trim
                If whereStr.Length = 0 Then
                    whereStr.AppendLine(" WHERE (kameiten_cd='" & kameitenCd & "' AND kbn = '" & kbn & "')")
                Else
                    whereStr.AppendLine(" OR (kameiten_cd='" & kameitenCd & "' AND kbn = '" & kbn & "')")
                End If
            End If
        Next


        If whereStr.Length > 0 Then

            Dim dt As DataTable = kameitenMasterDA.SelKameitenCds(whereStr.ToString)
            For i As Integer = 1 To strCsvLine.Length - 1
                strReadLine = strCsvLine(i)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                    Dim kameitenCd As String = Right("00000" & strReadLine.Split(",")(2).Trim, 5)

                    If dt.Select("kameiten_cd='" & kameitenCd & "'").Length > 0 Then
                        If updCd = "" Then
                            updCd = kameitenCd
                        Else
                            updCd = updCd & "," & kameitenCd
                        End If

                    Else
                        If insCd = "" Then
                            insCd = kameitenCd
                        Else
                            insCd = insCd & "," & kameitenCd
                        End If

                        hidInsLineNo = hidInsLineNo & i & ","

                    End If
                End If
            Next
            Return "Success"
        Else
            Return "CSVデータがないです"
        End If






    End Function

    ''' <summary>OKライン処理</summary>
    Private Sub OkLineSyori(ByVal strLine As String, ByRef dtOk As Data.DataTable)

        'DB存在チェック
        If kameitenMasterDA.SelTatouwaribikiSettei(strLine.Split(",")(2).Trim, "") Then
            Me.SetOkDataRow(strLine, dtOk, "1")
        Else
            Me.SetOkDataRow(strLine, dtOk, "0")
        End If

    End Sub

    ''' <summary>OKデータラインを追加</summary>
    Public Sub SetOkDataRow(ByVal strLine As String, ByRef dt As Data.DataTable, ByVal strInsUpdFlg As String)

        Dim dr As Data.DataRow
        dr = dt.NewRow
        dr.Item("key") = GetDataKey(strLine)
        dr.Item("ins_upd_flg") = strInsUpdFlg
        dr.Item("kbn") = strLine.Split(",")(1).Trim
        dr.Item("kameiten_cd") = strLine.Split(",")(2).Trim
        dr.Item("torikesi") = strLine.Split(",")(3).Trim
        dr.Item("hattyuu_teisi_flg") = strLine.Split(",")(4).Trim
        dr.Item("kameiten_mei1") = strLine.Split(",")(5).Trim
        dr.Item("tenmei_kana1") = strLine.Split(",")(6).Trim
        dr.Item("kameiten_mei2") = strLine.Split(",")(7).Trim
        dr.Item("tenmei_kana2") = strLine.Split(",")(8).Trim
        dr.Item("builder_no") = strLine.Split(",")(9).Trim
        dr.Item("builder_mei") = strLine.Split(",")(10).Trim
        dr.Item("keiretu_cd") = strLine.Split(",")(11).Trim
        dr.Item("keiretu_mei") = strLine.Split(",")(12).Trim
        dr.Item("eigyousyo_cd") = strLine.Split(",")(13).Trim
        dr.Item("eigyousyo_mei") = strLine.Split(",")(14).Trim
        dr.Item("kameiten_seisiki_mei") = strLine.Split(",")(15).Trim
        dr.Item("kameiten_seisiki_mei_kana") = strLine.Split(",")(16).Trim
        dr.Item("todouhuken_cd") = strLine.Split(",")(17).Trim
        dr.Item("todouhuken_mei") = strLine.Split(",")(18).Trim
        '==========↓2013/03/07 車龍 407584 追加↓======================
        dr.Item("nenkan_tousuu") = strLine.Split(",")(19).Trim '年間棟数
        '==========↑2013/03/07 車龍 407584 追加↑======================
        dr.Item("fuho_syoumeisyo_flg") = strLine.Split(",")(20).Trim
        dr.Item("fuho_syoumeisyo_kaisi_nengetu") = strLine.Split(",")(21).Trim
        dr.Item("eigyou_tantousya_mei") = strLine.Split(",")(22).Trim
        dr.Item("eigyou_tantousya_simei") = strLine.Split(",")(23).Trim
        dr.Item("kyuu_eigyou_tantousya_mei") = strLine.Split(",")(24).Trim
        dr.Item("kyuu_eigyou_tantousya_simei") = strLine.Split(",")(25).Trim
        '==========↓2013/03/07 車龍 407584 追加↓======================
        dr.Item("koj_uri_syubetsu") = strLine.Split(",")(26).Trim '工事売上種別
        dr.Item("koj_uri_syubetsu_mei") = strLine.Split(",")(27).Trim '工事売上種別名
        dr.Item("jiosaki_flg") = strLine.Split(",")(28).Trim 'JIO先フラグ
        '==========↑2013/03/07 車龍 407584 追加↑======================
        dr.Item("kaiyaku_haraimodosi_kkk") = strLine.Split(",")(29).Trim
        dr.Item("syouhin_cd1") = strLine.Split(",")(30).Trim
        dr.Item("syouhin_mei1") = strLine.Split(",")(31).Trim
        dr.Item("syouhin_cd2") = strLine.Split(",")(32).Trim
        dr.Item("syouhin_mei2") = strLine.Split(",")(33).Trim
        dr.Item("syouhin_cd3") = strLine.Split(",")(34).Trim
        dr.Item("syouhin_mei3") = strLine.Split(",")(35).Trim
        dr.Item("tys_seikyuu_saki_kbn") = strLine.Split(",")(36).Trim
        dr.Item("tys_seikyuu_saki_cd") = strLine.Split(",")(37).Trim
        dr.Item("tys_seikyuu_saki_brc") = strLine.Split(",")(38).Trim
        dr.Item("tys_seikyuu_saki_mei") = strLine.Split(",")(39).Trim
        dr.Item("koj_seikyuu_saki_kbn") = strLine.Split(",")(40).Trim
        dr.Item("koj_seikyuu_saki_cd") = strLine.Split(",")(41).Trim
        dr.Item("koj_seikyuu_saki_brc") = strLine.Split(",")(42).Trim
        dr.Item("koj_seikyuu_saki_mei") = strLine.Split(",")(43).Trim
        dr.Item("hansokuhin_seikyuu_saki_kbn") = strLine.Split(",")(44).Trim
        dr.Item("hansokuhin_seikyuu_saki_cd") = strLine.Split(",")(45).Trim
        dr.Item("hansokuhin_seikyuu_saki_brc") = strLine.Split(",")(46).Trim
        dr.Item("hansokuhin_seikyuu_saki_mei") = strLine.Split(",")(47).Trim
        dr.Item("tatemono_seikyuu_saki_kbn") = strLine.Split(",")(48).Trim
        dr.Item("tatemono_seikyuu_saki_cd") = strLine.Split(",")(49).Trim
        dr.Item("tatemono_seikyuu_saki_brc") = strLine.Split(",")(50).Trim
        dr.Item("tatemono_seikyuu_saki_mei") = strLine.Split(",")(51).Trim
        dr.Item("seikyuu_saki_kbn5") = strLine.Split(",")(52).Trim
        dr.Item("seikyuu_saki_cd5") = strLine.Split(",")(53).Trim
        dr.Item("seikyuu_saki_brc5") = strLine.Split(",")(54).Trim
        dr.Item("seikyuu_saki_mei5") = strLine.Split(",")(55).Trim
        dr.Item("seikyuu_saki_kbn6") = strLine.Split(",")(56).Trim
        dr.Item("seikyuu_saki_cd6") = strLine.Split(",")(57).Trim
        dr.Item("seikyuu_saki_brc6") = strLine.Split(",")(58).Trim
        dr.Item("seikyuu_saki_mei6") = strLine.Split(",")(59).Trim
        dr.Item("seikyuu_saki_kbn7") = strLine.Split(",")(60).Trim
        dr.Item("seikyuu_saki_cd7") = strLine.Split(",")(61).Trim
        dr.Item("seikyuu_saki_brc7") = strLine.Split(",")(62).Trim
        dr.Item("seikyuu_saki_mei7") = strLine.Split(",")(63).Trim
        '==========↓2013/03/07 車龍 407584 追加↓======================
        dr.Item("hosyou_kikan") = strLine.Split(",")(64).Trim '保証期間
        dr.Item("hosyousyo_hak_umu") = strLine.Split(",")(65).Trim '保証書発行有無
        '==========↑2013/03/07 車龍 407584 追加↑======================
        dr.Item("nyuukin_kakunin_jyouken") = strLine.Split(",")(66).Trim
        dr.Item("nyuukin_kakunin_oboegaki") = strLine.Split(",")(67).Trim
        dr.Item("tys_mitsyo_flg") = strLine.Split(",")(68).Trim
        dr.Item("hattyuusyo_flg") = strLine.Split(",")(69).Trim
        dr.Item("yuubin_no") = strLine.Split(",")(70).Trim
        dr.Item("jyuusyo1") = strLine.Split(",")(71).Trim
        dr.Item("jyuusyo2") = strLine.Split(",")(72).Trim
        dr.Item("jyuusyo_cd") = strLine.Split(",")(73).Trim
        dr.Item("jyuusyo_mei") = strLine.Split(",")(74).Trim
        dr.Item("busyo_mei") = strLine.Split(",")(75).Trim
        dr.Item("daihyousya_mei") = strLine.Split(",")(76).Trim
        dr.Item("tel_no") = strLine.Split(",")(77).Trim
        dr.Item("fax_no") = strLine.Split(",")(78).Trim
        dr.Item("mail_address") = strLine.Split(",")(79).Trim
        dr.Item("bikou1") = strLine.Split(",")(80).Trim
        dr.Item("bikou2") = strLine.Split(",")(81).Trim
        '==========↓2013/03/07 車龍 407584 追加↓======================
        dr.Item("add_date") = strLine.Split(",")(82).Trim '登録日
        dr.Item("seikyuu_umu") = strLine.Split(",")(83).Trim '請求有無
        dr.Item("syouhin_cd") = strLine.Split(",")(84).Trim '商品コード
        dr.Item("syouhin_mei") = strLine.Split(",")(85).Trim '商品名
        dr.Item("uri_gaku") = strLine.Split(",")(86).Trim '売上金額
        dr.Item("koumuten_seikyuu_gaku") = strLine.Split(",")(87).Trim '工務店請求金額
        dr.Item("seikyuusyo_hak_date") = strLine.Split(",")(88).Trim '請求書発行日
        dr.Item("uri_date") = strLine.Split(",")(89).Trim '売上年月日
        dr.Item("bikou") = strLine.Split(",")(90).Trim '備考
        '==========↑2013/03/07 車龍 407584 追加↑======================
        dr.Item("bikou_syubetu1") = strLine.Split(",")(96).Trim
        dr.Item("bikou_syubetu1_mei") = strLine.Split(",")(97).Trim
        dr.Item("bikou_syubetu2") = strLine.Split(",")(98).Trim
        dr.Item("bikou_syubetu2_mei") = strLine.Split(",")(99).Trim
        dr.Item("bikou_syubetu3") = strLine.Split(",")(100).Trim
        dr.Item("bikou_syubetu3_mei") = strLine.Split(",")(101).Trim
        dr.Item("bikou_syubetu4") = strLine.Split(",")(102).Trim
        dr.Item("bikou_syubetu4_mei") = strLine.Split(",")(103).Trim
        dr.Item("bikou_syubetu5") = strLine.Split(",")(104).Trim
        dr.Item("bikou_syubetu5_mei") = strLine.Split(",")(105).Trim
        dr.Item("naiyou1") = strLine.Split(",")(106).Trim
        dr.Item("naiyou2") = strLine.Split(",")(107).Trim
        dr.Item("naiyou3") = strLine.Split(",")(108).Trim
        dr.Item("naiyou4") = strLine.Split(",")(109).Trim
        dr.Item("naiyou5") = strLine.Split(",")(110).Trim

        dr.Item("ssgr_kkk") = strLine.Split(",")(111).Trim
        dr.Item("kaiseki_hosyou_kkk") = strLine.Split(",")(112).Trim
        dr.Item("koj_mitiraisyo_soufu_fuyou") = strLine.Split(",")(113).Trim
        dr.Item("hikiwatasi_inji_umu") = strLine.Split(",")(114).Trim
        dr.Item("hosyousyo_hassou_umu") = strLine.Split(",")(115).Trim
        dr.Item("ekijyouka_tokuyaku_kakaku") = strLine.Split(",")(116).Trim
        dr.Item("hosyousyo_hassou_umu_start_date") = strLine.Split(",")(117).Trim
        dr.Item("taiou_syouhin_kbn") = strLine.Split(",")(118).Trim
        dr.Item("taiou_syouhin_kbn_set_date") = strLine.Split(",")(119).Trim
        dr.Item("campaign_waribiki_flg") = strLine.Split(",")(120).Trim
        dr.Item("campaign_waribiki_set_date") = strLine.Split(",")(121).Trim
        dr.Item("online_waribiki_flg") = strLine.Split(",")(122).Trim
        dr.Item("b_str_yuuryou_wide_flg") = strLine.Split(",")(123).Trim



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
        If strLine.Split(",").Length < CsvInputCheck.KAMEITEN_FIELD_COUNT Then
            intMaxCount = strLine.Split(",").Length
        Else
            intMaxCount = CsvInputCheck.KAMEITEN_FIELD_COUNT
        End If

        For i As Integer = 0 To intMaxCount - 1
            '最大長を切り取る
            dr.Item(i) = commonCheck.CutMaxLength(strLine.Split(",")(i).Trim, commonCheck.KAMEITEN_MAX_LENGTH(i))
        Next

        '行号
        dr.Item(CsvInputCheck.KAMEITEN_FIELD_COUNT) = CStr(intLineNo)
        '処理日時
        dr.Item(CsvInputCheck.KAMEITEN_FIELD_COUNT + 1) = InputDate
        '登録ログインユーザID
        dr.Item(CsvInputCheck.KAMEITEN_FIELD_COUNT + 2) = userId

        dtError.Rows.Add(dr)

    End Sub

    ''' <summary>加盟店商品調査方法特別対応マスタを作成</summary>
    Public Sub CreateOkDataTable(ByRef dtOk As Data.DataTable)
        dtOk.Columns.Add("key")                                      '--キー(加盟店コード)
        dtOk.Columns.Add("ins_upd_flg")                              '--追加更新FLG(0:追加; 1:更新)
        dtOk.Columns.Add("kbn")                                      '区分
        dtOk.Columns.Add("kameiten_cd")                              '加盟店コード
        dtOk.Columns.Add("torikesi")                                 '取消
        dtOk.Columns.Add("hattyuu_teisi_flg")                        '発注停止フラグ
        dtOk.Columns.Add("kameiten_mei1")                            '加盟店名1
        dtOk.Columns.Add("tenmei_kana1")                             '店名カナ1
        dtOk.Columns.Add("kameiten_mei2")                            '加盟店名2
        dtOk.Columns.Add("tenmei_kana2")                             '店名カナ2
        dtOk.Columns.Add("builder_no")                               'ビルダーNO
        dtOk.Columns.Add("builder_mei")                              'ビルダー名
        dtOk.Columns.Add("keiretu_cd")                               '系列コード
        dtOk.Columns.Add("keiretu_mei")                              '系列名
        dtOk.Columns.Add("eigyousyo_cd")                             '営業所コード
        dtOk.Columns.Add("eigyousyo_mei")                            '営業所名
        dtOk.Columns.Add("kameiten_seisiki_mei")                     '正式名称
        dtOk.Columns.Add("kameiten_seisiki_mei_kana")                '正式名称2
        dtOk.Columns.Add("todouhuken_cd")                            '都道府県コード
        dtOk.Columns.Add("todouhuken_mei")                           '都道府県名
        '==========↓2013/03/07 車龍 407584 追加↓======================
        dtOk.Columns.Add("nenkan_tousuu")                            '年間棟数
        '==========↑2013/03/07 車龍 407584 追加↑======================
        dtOk.Columns.Add("fuho_syoumeisyo_flg")                      '付保証明FLG
        dtOk.Columns.Add("fuho_syoumeisyo_kaisi_nengetu")            '付保証明書開始年月
        dtOk.Columns.Add("eigyou_tantousya_mei")                     '営業担当者
        dtOk.Columns.Add("eigyou_tantousya_simei")                   '営業担当者名
        dtOk.Columns.Add("kyuu_eigyou_tantousya_mei")                '旧営業担当者
        dtOk.Columns.Add("kyuu_eigyou_tantousya_simei")              '旧営業担当者名
        '==========↓2013/03/07 車龍 407584 追加↓======================
        dtOk.Columns.Add("koj_uri_syubetsu")                         '工事売上種別
        dtOk.Columns.Add("koj_uri_syubetsu_mei")                     '工事売上種別名
        dtOk.Columns.Add("jiosaki_flg")                              'JIO先フラグ
        '==========↑2013/03/07 車龍 407584 追加↑======================
        dtOk.Columns.Add("kaiyaku_haraimodosi_kkk")                  '解約払戻価格
        dtOk.Columns.Add("syouhin_cd1")                              '棟区分1商品コード
        dtOk.Columns.Add("syouhin_mei1")                             '棟区分1商品名
        dtOk.Columns.Add("syouhin_cd2")                              '棟区分2商品コード
        dtOk.Columns.Add("syouhin_mei2")                             '棟区分2商品名
        dtOk.Columns.Add("syouhin_cd3")                              '棟区分3商品コード
        dtOk.Columns.Add("syouhin_mei3")                             '棟区分3商品名
        dtOk.Columns.Add("tys_seikyuu_saki_kbn")                     '調査請求先区分
        dtOk.Columns.Add("tys_seikyuu_saki_cd")                      '調査請求先コード
        dtOk.Columns.Add("tys_seikyuu_saki_brc")                     '調査請求先枝番
        dtOk.Columns.Add("tys_seikyuu_saki_mei")                     '調査請求先名
        dtOk.Columns.Add("koj_seikyuu_saki_kbn")                     '工事請求先区分
        dtOk.Columns.Add("koj_seikyuu_saki_cd")                      '工事請求先コード
        dtOk.Columns.Add("koj_seikyuu_saki_brc")                     '工事請求先枝番
        dtOk.Columns.Add("koj_seikyuu_saki_mei")                     '工事請求先名
        dtOk.Columns.Add("hansokuhin_seikyuu_saki_kbn")              '販促品請求先区分
        dtOk.Columns.Add("hansokuhin_seikyuu_saki_cd")               '販促品請求先コード
        dtOk.Columns.Add("hansokuhin_seikyuu_saki_brc")              '販促品請求先枝番
        dtOk.Columns.Add("hansokuhin_seikyuu_saki_mei")              '販促品請求先名
        dtOk.Columns.Add("tatemono_seikyuu_saki_kbn")                '建物検査請求先区分
        dtOk.Columns.Add("tatemono_seikyuu_saki_cd")                 '建物検査請求先コード
        dtOk.Columns.Add("tatemono_seikyuu_saki_brc")                '建物検査請求先枝番
        dtOk.Columns.Add("tatemono_seikyuu_saki_mei")                '建物検査請求先名
        dtOk.Columns.Add("seikyuu_saki_kbn5")                        '請求先区分5
        dtOk.Columns.Add("seikyuu_saki_cd5")                         '請求先コード5
        dtOk.Columns.Add("seikyuu_saki_brc5")                        '請求先枝番5
        dtOk.Columns.Add("seikyuu_saki_mei5")                        '請求先名5
        dtOk.Columns.Add("seikyuu_saki_kbn6")                        '請求先区分6
        dtOk.Columns.Add("seikyuu_saki_cd6")                         '請求先コード6
        dtOk.Columns.Add("seikyuu_saki_brc6")                        '請求先枝番6
        dtOk.Columns.Add("seikyuu_saki_mei6")                        '請求先名6
        dtOk.Columns.Add("seikyuu_saki_kbn7")                        '請求先区分7
        dtOk.Columns.Add("seikyuu_saki_cd7")                         '請求先コード7
        dtOk.Columns.Add("seikyuu_saki_brc7")                        '請求先枝番7
        dtOk.Columns.Add("seikyuu_saki_mei7")                        '請求先名7
        '==========↓2013/03/07 車龍 407584 追加↓======================
        dtOk.Columns.Add("hosyou_kikan")                             '保証期間
        dtOk.Columns.Add("hosyousyo_hak_umu")                        '保証書発行有無
        '==========↑2013/03/07 車龍 407584 追加↑======================
        dtOk.Columns.Add("nyuukin_kakunin_jyouken")                  '入金確認条件
        dtOk.Columns.Add("nyuukin_kakunin_oboegaki")                 '入金確認覚書
        dtOk.Columns.Add("tys_mitsyo_flg")                           '調査見積書FLG
        dtOk.Columns.Add("hattyuusyo_flg")                           '発注書FLG                         
        dtOk.Columns.Add("yuubin_no")                                '郵便番号
        dtOk.Columns.Add("jyuusyo1")                                 '住所1
        dtOk.Columns.Add("jyuusyo2")                                 '住所2
        dtOk.Columns.Add("jyuusyo_cd")                               '所在地コード
        dtOk.Columns.Add("jyuusyo_mei")                              '所在地名
        dtOk.Columns.Add("busyo_mei")                                '部署名
        dtOk.Columns.Add("daihyousya_mei")                           '代表者名
        dtOk.Columns.Add("tel_no")                                   '電話番号
        dtOk.Columns.Add("fax_no")                                   'FAX番号
        dtOk.Columns.Add("mail_address")                             '申込担当者
        dtOk.Columns.Add("bikou1")                                   '備考1
        dtOk.Columns.Add("bikou2")                                   '備考2
        '==========↓2013/03/07 車龍 407584 追加↓======================
        dtOk.Columns.Add("add_date")                                 '登録日
        dtOk.Columns.Add("seikyuu_umu")                              '請求有無
        dtOk.Columns.Add("syouhin_cd")                               '商品コード
        dtOk.Columns.Add("syouhin_mei")                              '商品名
        dtOk.Columns.Add("uri_gaku")                                 '売上金額
        dtOk.Columns.Add("koumuten_seikyuu_gaku")                    '工務店請求金額
        dtOk.Columns.Add("seikyuusyo_hak_date")                      '請求書発行日
        dtOk.Columns.Add("uri_date")                                 '売上年月日
        dtOk.Columns.Add("bikou")                                    '備考
        '==========↑2013/03/07 車龍 407584 追加↑======================
        dtOk.Columns.Add("bikou_syubetu1")                           '追加_備考種別①
        dtOk.Columns.Add("bikou_syubetu1_mei")                       '追加_備考種別①名
        dtOk.Columns.Add("bikou_syubetu2")                           '追加_備考種別②
        dtOk.Columns.Add("bikou_syubetu2_mei")                       '追加_備考種別②名
        dtOk.Columns.Add("bikou_syubetu3")                           '追加_備考種別③
        dtOk.Columns.Add("bikou_syubetu3_mei")                       '追加_備考種別③名
        dtOk.Columns.Add("bikou_syubetu4")                           '追加_備考種別④
        dtOk.Columns.Add("bikou_syubetu4_mei")                       '追加_備考種別④名
        dtOk.Columns.Add("bikou_syubetu5")                           '追加_備考種別⑤
        dtOk.Columns.Add("bikou_syubetu5_mei")                       '追加_備考種別⑤名
        dtOk.Columns.Add("naiyou1")                                  '追加_内容①
        dtOk.Columns.Add("naiyou2")                                  '追加_内容②
        dtOk.Columns.Add("naiyou3")                                  '追加_内容③
        dtOk.Columns.Add("naiyou4")                                  '追加_内容④
        dtOk.Columns.Add("naiyou5")                                  '追加_内容⑤
        dtOk.Columns.Add("syori_datetime")                           '--処理日時
        dtOk.Columns.Add("add_login_user_id")                        '--登録者ID
        dtOk.Columns.Add("add_datetime")                             '--登録日時
        dtOk.Columns.Add("upd_login_user_id")                        '--更新者ID
        dtOk.Columns.Add("upd_datetime")                             '--更新日時


        dtOk.Columns.Add("ssgr_kkk") 'SSGR価格
        dtOk.Columns.Add("kaiseki_hosyou_kkk") '解析保証価格
        dtOk.Columns.Add("koj_mitiraisyo_soufu_fuyou") '工事見積依頼書送付不要
        dtOk.Columns.Add("hikiwatasi_inji_umu") '保証書引渡日印字有無
        dtOk.Columns.Add("hosyousyo_hassou_umu") '保証書発送方法
        dtOk.Columns.Add("ekijyouka_tokuyaku_kakaku") '液状化特約費
        dtOk.Columns.Add("hosyousyo_hassou_umu_start_date") '保証書発送方法_適用開始日
        dtOk.Columns.Add("taiou_syouhin_kbn") '対応商品区分
        dtOk.Columns.Add("taiou_syouhin_kbn_set_date") '対応商品区分設定日
        dtOk.Columns.Add("campaign_waribiki_flg") 'キャンペーン割引FLG
        dtOk.Columns.Add("campaign_waribiki_set_date") 'キャンペーン割引設定日
        dtOk.Columns.Add("online_waribiki_flg") 'オンライン割引FLG
        dtOk.Columns.Add("b_str_yuuryou_wide_flg") 'B-STR有料ワイドFLG


    End Sub

    ''' <summary>加盟店情報一括取込エラーマスタを作成</summary>
    Public Sub CreateErrorDataTable(ByRef dtError As Data.DataTable)
        dtError.Columns.Add("edi_jouhou_sakusei_date")                  '--EDI情報作成日
        dtError.Columns.Add("kbn")                                      '区分
        dtError.Columns.Add("kameiten_cd")                              '加盟店コード
        dtError.Columns.Add("torikesi")                                 '取消
        dtError.Columns.Add("hattyuu_teisi_flg")                        '発注停止フラグ
        dtError.Columns.Add("kameiten_mei1")                            '加盟店名1
        dtError.Columns.Add("tenmei_kana1")                             '店名カナ1
        dtError.Columns.Add("kameiten_mei2")                            '加盟店名2
        dtError.Columns.Add("tenmei_kana2")                             '店名カナ2
        dtError.Columns.Add("builder_no")                               'ビルダーNO
        dtError.Columns.Add("builder_mei")                              'ビルダー名
        dtError.Columns.Add("keiretu_cd")                               '系列コード
        dtError.Columns.Add("keiretu_mei")                              '系列名
        dtError.Columns.Add("eigyousyo_cd")                             '営業所コード
        dtError.Columns.Add("eigyousyo_mei")                            '営業所名
        dtError.Columns.Add("kameiten_seisiki_mei")                     '正式名称
        dtError.Columns.Add("kameiten_seisiki_mei_kana")                '正式名称2
        dtError.Columns.Add("todouhuken_cd")                            '都道府県コード
        dtError.Columns.Add("todouhuken_mei")                           '都道府県名
        '==========↓2013/03/07 車龍 407584 追加↓======================
        dtError.Columns.Add("nenkan_tousuu")                            '年間棟数
        '==========↑2013/03/07 車龍 407584 追加↑======================
        dtError.Columns.Add("fuho_syoumeisyo_flg")                      '付保証明FLG
        dtError.Columns.Add("fuho_syoumeisyo_kaisi_nengetu")            '付保証明書開始年月
        dtError.Columns.Add("eigyou_tantousya_mei")                     '営業担当者
        dtError.Columns.Add("eigyou_tantousya_simei")                   '営業担当者名
        dtError.Columns.Add("kyuu_eigyou_tantousya_mei")                '旧営業担当者
        dtError.Columns.Add("kyuu_eigyou_tantousya_simei")              '旧営業担当者名
        '==========↓2013/03/07 車龍 407584 追加↓======================
        dtError.Columns.Add("koj_uri_syubetsu")                         '工事売上種別
        dtError.Columns.Add("koj_uri_syubetsu_mei")                     '工事売上種別名
        dtError.Columns.Add("jiosaki_flg")                              'JIO先フラグ
        '==========↑2013/03/07 車龍 407584 追加↑======================
        dtError.Columns.Add("kaiyaku_haraimodosi_kkk")                  '解約払戻価格
        dtError.Columns.Add("syouhin_cd1")                              '棟区分1商品コード
        dtError.Columns.Add("syouhin_mei1")                             '棟区分1商品名
        dtError.Columns.Add("syouhin_cd2")                              '棟区分2商品コード
        dtError.Columns.Add("syouhin_mei2")                             '棟区分2商品名
        dtError.Columns.Add("syouhin_cd3")                              '棟区分3商品コード
        dtError.Columns.Add("syouhin_mei3")                             '棟区分3商品名
        dtError.Columns.Add("tys_seikyuu_saki_kbn")                     '調査請求先区分
        dtError.Columns.Add("tys_seikyuu_saki_cd")                      '調査請求先コード
        dtError.Columns.Add("tys_seikyuu_saki_brc")                     '調査請求先枝番
        dtError.Columns.Add("tys_seikyuu_saki_mei")                     '調査請求先名
        dtError.Columns.Add("koj_seikyuu_saki_kbn")                     '工事請求先区分
        dtError.Columns.Add("koj_seikyuu_saki_cd")                      '工事請求先コード
        dtError.Columns.Add("koj_seikyuu_saki_brc")                     '工事請求先枝番
        dtError.Columns.Add("koj_seikyuu_saki_mei")                     '工事請求先名
        dtError.Columns.Add("hansokuhin_seikyuu_saki_kbn")              '販促品請求先区分
        dtError.Columns.Add("hansokuhin_seikyuu_saki_cd")               '販促品請求先コード
        dtError.Columns.Add("hansokuhin_seikyuu_saki_brc")              '販促品請求先枝番
        dtError.Columns.Add("hansokuhin_seikyuu_saki_mei")              '販促品請求先名
        dtError.Columns.Add("tatemono_seikyuu_saki_kbn")                '建物検査請求先区分
        dtError.Columns.Add("tatemono_seikyuu_saki_cd")                 '建物検査請求先コード
        dtError.Columns.Add("tatemono_seikyuu_saki_brc")                '建物検査請求先枝番
        dtError.Columns.Add("tatemono_seikyuu_saki_mei")                '建物検査請求先名
        dtError.Columns.Add("seikyuu_saki_kbn5")                        '請求先区分5
        dtError.Columns.Add("seikyuu_saki_cd5")                         '請求先コード5
        dtError.Columns.Add("seikyuu_saki_brc5")                        '請求先枝番5
        dtError.Columns.Add("seikyuu_saki_mei5")                        '請求先名5
        dtError.Columns.Add("seikyuu_saki_kbn6")                        '請求先区分6
        dtError.Columns.Add("seikyuu_saki_cd6")                         '請求先コード6
        dtError.Columns.Add("seikyuu_saki_brc6")                        '請求先枝番6
        dtError.Columns.Add("seikyuu_saki_mei6")                        '請求先名6
        dtError.Columns.Add("seikyuu_saki_kbn7")                        '請求先区分7
        dtError.Columns.Add("seikyuu_saki_cd7")                         '請求先コード7
        dtError.Columns.Add("seikyuu_saki_brc7")                        '請求先枝番7
        dtError.Columns.Add("seikyuu_saki_mei7")                        '請求先名7
        '==========↓2013/03/07 車龍 407584 追加↓======================
        dtError.Columns.Add("hosyou_kikan")                             '保証期間
        dtError.Columns.Add("hosyousyo_hak_umu")                        '保証書発行有無
        '==========↑2013/03/07 車龍 407584 追加↑======================
        dtError.Columns.Add("nyuukin_kakunin_jyouken")                  '入金確認条件
        dtError.Columns.Add("nyuukin_kakunin_oboegaki")                 '入金確認覚書
        dtError.Columns.Add("tys_mitsyo_flg")                           '調査見積書FLG
        dtError.Columns.Add("hattyuusyo_flg")                           '発注書FLG                         
        dtError.Columns.Add("yuubin_no")                                '郵便番号
        dtError.Columns.Add("jyuusyo1")                                 '住所1
        dtError.Columns.Add("jyuusyo2")                                 '住所2
        dtError.Columns.Add("jyuusyo_cd")                               '所在地コード
        dtError.Columns.Add("jyuusyo_mei")                              '所在地名
        dtError.Columns.Add("busyo_mei")                                '部署名
        dtError.Columns.Add("daihyousya_mei")                           '代表者名
        dtError.Columns.Add("tel_no")                                   '電話番号
        dtError.Columns.Add("fax_no")                                   'FAX番号
        dtError.Columns.Add("mail_address")                             '申込担当者
        dtError.Columns.Add("bikou1")                                   '備考1
        dtError.Columns.Add("bikou2")                                   '備考2
        '==========↓2013/03/07 車龍 407584 追加↓======================
        dtError.Columns.Add("add_date")                                 '登録日
        dtError.Columns.Add("seikyuu_umu")                              '請求有無
        dtError.Columns.Add("syouhin_cd")                               '商品コード
        dtError.Columns.Add("syouhin_mei")                              '商品名
        dtError.Columns.Add("uri_gaku")                                 '売上金額
        dtError.Columns.Add("koumuten_seikyuu_gaku")                    '工務店請求金額
        dtError.Columns.Add("seikyuusyo_hak_date")                      '請求書発行日
        dtError.Columns.Add("uri_date")                                 '売上年月日
        dtError.Columns.Add("bikou")                                    '備考
        '==========↑2013/03/07 車龍 407584 追加↑======================
        '==========2012/05/09 車龍 407553の対応 追加↓======================
        dtError.Columns.Add("kameiten_upd_datetime")                    '加盟店_更新日時
        dtError.Columns.Add("tatouwari_upd_datetime_1")                 '多棟割引_更新日時1
        dtError.Columns.Add("tatouwari_upd_datetime_2")                 '多棟割引_更新日時2
        dtError.Columns.Add("tatouwari_upd_datetime_3")                 '多棟割引_更新日時3
        dtError.Columns.Add("kameiten_jyuusyo_upd_datetime")            '加盟店住所_更新日時
        '==========2012/05/09 車龍 407553の対応 追加↑======================
        dtError.Columns.Add("bikou_syubetu1")                           '追加_備考種別①
        dtError.Columns.Add("bikou_syubetu1_mei")                       '追加_備考種別①名
        dtError.Columns.Add("bikou_syubetu2")                           '追加_備考種別②
        dtError.Columns.Add("bikou_syubetu2_mei")                       '追加_備考種別②名
        dtError.Columns.Add("bikou_syubetu3")                           '追加_備考種別③
        dtError.Columns.Add("bikou_syubetu3_mei")                       '追加_備考種別③名
        dtError.Columns.Add("bikou_syubetu4")                           '追加_備考種別④
        dtError.Columns.Add("bikou_syubetu4_mei")                       '追加_備考種別④名
        dtError.Columns.Add("bikou_syubetu5")                           '追加_備考種別⑤
        dtError.Columns.Add("bikou_syubetu5_mei")                       '追加_備考種別⑤名
        dtError.Columns.Add("naiyou1")                                  '追加_内容①
        dtError.Columns.Add("naiyou2")                                  '追加_内容②
        dtError.Columns.Add("naiyou3")                                  '追加_内容③
        dtError.Columns.Add("naiyou4")                                  '追加_内容④
        dtError.Columns.Add("naiyou5")                                  '追加_内容⑤
        dtError.Columns.Add("gyou_no")                                  '--行NO
        dtError.Columns.Add("syori_datetime")                           '--処理日時
        dtError.Columns.Add("add_login_user_id")                        '--登録者ID
        dtError.Columns.Add("add_datetime")                             '--登録日時
        dtError.Columns.Add("upd_login_user_id")                        '--更新者ID
        dtError.Columns.Add("upd_datetime")                             '--更新日時

        dtError.Columns.Add("ssgr_kkk") 'SSGR価格
        dtError.Columns.Add("kaiseki_hosyou_kkk") '解析保証価格
        dtError.Columns.Add("koj_mitiraisyo_soufu_fuyou") '工事見積依頼書送付不要
        dtError.Columns.Add("hikiwatasi_inji_umu") '保証書引渡日印字有無
        dtError.Columns.Add("hosyousyo_hassou_umu") '保証書発送方法
        dtError.Columns.Add("ekijyouka_tokuyaku_kakaku") '液状化特約費
        dtError.Columns.Add("hosyousyo_hassou_umu_start_date") '保証書発送方法_適用開始日
        dtError.Columns.Add("taiou_syouhin_kbn") '対応商品区分
        dtError.Columns.Add("taiou_syouhin_kbn_set_date") '対応商品区分設定日
        dtError.Columns.Add("campaign_waribiki_flg") 'キャンペーン割引FLG
        dtError.Columns.Add("campaign_waribiki_set_date") 'キャンペーン割引設定日
        dtError.Columns.Add("online_waribiki_flg") 'オンライン割引FLG
        dtError.Columns.Add("b_str_yuuryou_wide_flg") 'B-STR有料ワイドFLG

    End Sub

    ''' <summary>OKデータのkeyを設定</summary>
    Public Function GetDataKey(ByVal strLine As String) As String

        Return strLine.Split(",")(1).Trim & "," & strLine.Split(",")(2).Trim & "," & strLine.Split(",")(3).Trim & "," & strLine.Split(",")(5).Trim & "," & strLine.Split(",")(6).Trim

    End Function

    Private Function SuccessSyori(ByVal dtOk As Data.DataTable, ByVal dtInsKameiten As Data.DataTable) As Boolean

        Dim successFlg As Boolean = False

        '加盟店マスタ更新
        successFlg = kameitenMasterDA.UpdKameiten(dtOk)
        If Not successFlg Then
            Return successFlg
        End If
        successFlg = kameitenMasterDA.InsKameiten(dtInsKameiten)
        If Not successFlg Then
            Return successFlg
        End If

        '==========↓2013/03/08 車龍 407584 追加↓======================
        '請求先マスタ
        successFlg = kameitenMasterDA.InsUpdSeikyuuSaki(dtOk)
        If Not successFlg Then
            Return successFlg
        End If
        successFlg = kameitenMasterDA.InsUpdSeikyuuSaki(dtInsKameiten)
        If Not successFlg Then
            Return successFlg
        End If
        '==========↑2013/03/08 車龍 407584 追加↑======================

        '加盟店住所マスタ更新
        successFlg = kameitenMasterDA.UpdKameitenJyuusyo(dtOk)
        If Not successFlg Then
            Return successFlg
        End If
        successFlg = kameitenMasterDA.UpdKameitenJyuusyo(dtInsKameiten)
        If Not successFlg Then
            Return successFlg
        End If

        '多棟割引設定マスタ　へ　棟区分 ごとに更新・登録を行う。
        successFlg = kameitenMasterDA.InsUpdTatouwaribikiSettei(dtOk)
        If Not successFlg Then
            Return successFlg
        End If
        successFlg = kameitenMasterDA.InsUpdTatouwaribikiSettei(dtInsKameiten)
        If Not successFlg Then
            Return successFlg
        End If

        '==========↓2013/03/08 車龍 407584 追加↓======================
        '店別初期請求テーブル
        successFlg = kameitenMasterDA.InsTenbetuSyokiSeikyuu(dtOk)
        If Not successFlg Then
            Return successFlg
        End If
        successFlg = kameitenMasterDA.InsTenbetuSyokiSeikyuu(dtInsKameiten)
        If Not successFlg Then
            Return successFlg
        End If
        '==========↑2013/03/08 車龍 407584 追加↑======================

        '加盟店備考設定マスタ　へ　備考種別(①～⑤) ごとに追加登録を行う。
        successFlg = kameitenMasterDA.InsKameitenBikouSettei(dtOk)
        If Not successFlg Then
            Return successFlg
        End If
        successFlg = kameitenMasterDA.InsKameitenBikouSettei(dtInsKameiten)
        If Not successFlg Then
            Return successFlg
        End If

        Return successFlg

    End Function

    ''' <summary>加盟店情報一括取込エラーデータを取得</summary>
    Public Function GetKameitenInfoIttukatuError(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
        Return kameitenMasterDA.SelKameitenInfoIttukatuError(strEdidate, strSyoridate)
    End Function

    ''' <summary>加盟店情報一括取込エラー件数を取得</summary>
    Public Function GetKameitenInfoIttukatuErrorCount(ByVal strEdidate As String, ByVal strSyoridate As String) As Integer
        Return kameitenMasterDA.SelKameitenInfoIttukatuErrorCount(strEdidate, strSyoridate)
    End Function

    ''' <summary>加盟店情報一括取込エラーCSVデータを取得</summary>
    Public Function GetKameitenInfoIttukatuErrorCsv(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
        Return kameitenMasterDA.SelKameitenInfoIttukatuErrorCsv(strEdidate, strSyoridate)
    End Function

    ''' <summary>
    ''' 更新日時チェック
    ''' </summary>
    ''' <param name="strLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/08 車龍 407553の対応 追加</history>
    Private Function ChkUpdDate(ByRef strLine As String) As Boolean
        Dim arrLine() As String = Split(strLine, ",")

        '加盟店コード
        Dim strKameitenCd As String = arrLine(2).Trim
        '加盟店の更新日時
        Dim strKameitenUpdDate As String = arrLine(91).Trim
        '加盟店住所の更新日時
        Dim strKameitenJyousyoUpdDate As String = arrLine(95).Trim
        '多棟割引設定1の更新日時
        Dim strTatouwaribikiSettei1UpdDate As String = arrLine(92).Trim
        '多棟割引設定2の更新日時
        Dim strTatouwaribikiSettei2UpdDate As String = arrLine(93).Trim
        '多棟割引設定3の更新日時
        Dim strTatouwaribikiSettei3UpdDate As String = arrLine(94).Trim

        '各マスタの更新日時
        Dim dtMstUpdDate As New Data.DataTable
        Dim strMstUpdDate As String = String.Empty

        '加盟店の更新日時チェック
        If Not strKameitenUpdDate.Equals(String.Empty) Then
            'If Not IsDate(strKameitenUpdDate) Then
            '    Return False
            'Else
            '    '加盟店マスタの更新日時を取得する
            '    dtMstUpdDate = kameitenMasterDA.SelKameitenUpdDate(strKameitenCd)
            '    If dtMstUpdDate.Rows.Count > 0 Then
            '        strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            '    Else
            '        strMstUpdDate = String.Empty
            '    End If
            '    If Not strMstUpdDate.Equals(String.Empty) Then
            '        'CSV.更新日時とマスタ.更新日時を比較する
            '        If Date.Compare(Convert.ToDateTime(strKameitenUpdDate), Convert.ToDateTime(strMstUpdDate)) < 0 Then
            '            'CSV.更新日時 < マスタ.更新日時 の場合
            '            Return False
            '        End If
            '    End If
            'End If

            '加盟店マスタの更新日時を取得する
            dtMstUpdDate = kameitenMasterDA.SelKameitenUpdDate(strKameitenCd)
            If dtMstUpdDate.Rows.Count > 0 Then
                strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            Else
                strMstUpdDate = String.Empty
            End If
            'CSV.更新日時とマスタ.更新日時を比較する
            If String.Compare(strKameitenUpdDate, strMstUpdDate) < 0 Then
                'CSV.更新日時 < マスタ.更新日時 の場合
                Return False
            End If
        End If

        '加盟店住所の更新日時チェック
        If Not strKameitenJyousyoUpdDate.Equals(String.Empty) Then
            'If Not IsDate(strKameitenJyousyoUpdDate) Then
            '    Return False
            'Else
            '    '加盟店住所マスタの更新日時を取得する
            '    dtMstUpdDate = kameitenMasterDA.SelKameitenJyuusyoUpdDate(strKameitenCd)
            '    If dtMstUpdDate.Rows.Count > 0 Then
            '        strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            '    Else
            '        strMstUpdDate = String.Empty
            '    End If
            '    If Not strMstUpdDate.Equals(String.Empty) Then
            '        'CSV.更新日時とマスタ.更新日時を比較する
            '        If Date.Compare(Convert.ToDateTime(strKameitenJyousyoUpdDate), Convert.ToDateTime(strMstUpdDate)) < 0 Then
            '            'CSV.更新日時 < マスタ.更新日時 の場合
            '            Return False
            '        End If
            '    End If
            'End If

            '加盟店住所マスタの更新日時を取得する
            dtMstUpdDate = kameitenMasterDA.SelKameitenJyuusyoUpdDate(strKameitenCd)
            If dtMstUpdDate.Rows.Count > 0 Then
                strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            Else
                strMstUpdDate = String.Empty
            End If
            'CSV.更新日時とマスタ.更新日時を比較する
            If String.Compare(strKameitenJyousyoUpdDate, strMstUpdDate) < 0 Then
                'CSV.更新日時 < マスタ.更新日時 の場合
                Return False
            End If
        End If

        '多棟割引設定1の更新日時チェック
        If Not strTatouwaribikiSettei1UpdDate.Equals(String.Empty) Then
            'If Not IsDate(strTatouwaribikiSettei1UpdDate) Then
            '    Return False
            'Else
            '    '多棟割引設定マスタの更新日時を取得する
            '    dtMstUpdDate = kameitenMasterDA.SelTatouwaribikiSetteiUpdDate(strKameitenCd, "1")
            '    If dtMstUpdDate.Rows.Count > 0 Then
            '        strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            '    Else
            '        strMstUpdDate = String.Empty
            '    End If
            '    If Not strMstUpdDate.Equals(String.Empty) Then
            '        'CSV.更新日時とマスタ.更新日時を比較する
            '        If Date.Compare(Convert.ToDateTime(strTatouwaribikiSettei1UpdDate), Convert.ToDateTime(strMstUpdDate)) < 0 Then
            '            'CSV.更新日時 < マスタ.更新日時 の場合
            '            Return False
            '        End If
            '    End If
            'End If

            '多棟割引設定マスタの更新日時を取得する
            dtMstUpdDate = kameitenMasterDA.SelTatouwaribikiSetteiUpdDate(strKameitenCd, "1")
            If dtMstUpdDate.Rows.Count > 0 Then
                strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            Else
                strMstUpdDate = String.Empty
            End If
            'CSV.更新日時とマスタ.更新日時を比較する
            If String.Compare(strTatouwaribikiSettei1UpdDate, strMstUpdDate) < 0 Then
                'CSV.更新日時 < マスタ.更新日時 の場合
                Return False
            End If
        End If

        '多棟割引設定2の更新日時チェック
        If Not strTatouwaribikiSettei2UpdDate.Equals(String.Empty) Then
            'If Not IsDate(strTatouwaribikiSettei2UpdDate) Then
            '    Return False
            'Else
            '    '多棟割引設定マスタの更新日時を取得する
            '    dtMstUpdDate = kameitenMasterDA.SelTatouwaribikiSetteiUpdDate(strKameitenCd, "2")
            '    If dtMstUpdDate.Rows.Count > 0 Then
            '        strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            '    Else
            '        strMstUpdDate = String.Empty
            '    End If
            '    If Not strMstUpdDate.Equals(String.Empty) Then
            '        'CSV.更新日時とマスタ.更新日時を比較する
            '        If Date.Compare(Convert.ToDateTime(strTatouwaribikiSettei2UpdDate), Convert.ToDateTime(strMstUpdDate)) < 0 Then
            '            'CSV.更新日時 < マスタ.更新日時 の場合
            '            Return False
            '        End If
            '    End If
            'End If

            '多棟割引設定マスタの更新日時を取得する
            dtMstUpdDate = kameitenMasterDA.SelTatouwaribikiSetteiUpdDate(strKameitenCd, "2")
            If dtMstUpdDate.Rows.Count > 0 Then
                strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            Else
                strMstUpdDate = String.Empty
            End If
            'CSV.更新日時とマスタ.更新日時を比較する
            If String.Compare(strTatouwaribikiSettei2UpdDate, strMstUpdDate) < 0 Then
                'CSV.更新日時 < マスタ.更新日時 の場合
                Return False
            End If
        End If

        '多棟割引設定3の更新日時チェック
        If Not strTatouwaribikiSettei3UpdDate.Equals(String.Empty) Then
            'If Not IsDate(strTatouwaribikiSettei3UpdDate) Then
            '    Return False
            'Else
            '    '多棟割引設定マスタの更新日時を取得する
            '    dtMstUpdDate = kameitenMasterDA.SelTatouwaribikiSetteiUpdDate(strKameitenCd, "3")
            '    If dtMstUpdDate.Rows.Count > 0 Then
            '        strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            '    Else
            '        strMstUpdDate = String.Empty
            '    End If
            '    If Not strMstUpdDate.Equals(String.Empty) Then
            '        'CSV.更新日時とマスタ.更新日時を比較する
            '        If Date.Compare(Convert.ToDateTime(strTatouwaribikiSettei3UpdDate), Convert.ToDateTime(strMstUpdDate)) < 0 Then
            '            'CSV.更新日時 < マスタ.更新日時 の場合
            '            Return False
            '        End If
            '    End If
            'End If

            '多棟割引設定マスタの更新日時を取得する
            dtMstUpdDate = kameitenMasterDA.SelTatouwaribikiSetteiUpdDate(strKameitenCd, "3")
            If dtMstUpdDate.Rows.Count > 0 Then
                strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            Else
                strMstUpdDate = String.Empty
            End If
            'CSV.更新日時とマスタ.更新日時を比較する
            If String.Compare(strTatouwaribikiSettei3UpdDate, strMstUpdDate) < 0 Then
                'CSV.更新日時 < マスタ.更新日時 の場合
                Return False
            End If
        End If

        Return True
    End Function

End Class