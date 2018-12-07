Option Explicit

Dim countZK1 As Integer         '全件1
Dim countZK2 As Integer         '全件2
Dim countXK1 As Integer         '店小計1
Dim countXK2 As Integer         '店小計2
Dim countBK As Integer          '物件
'===========2011/02/18 車龍(大連) 追加 開始↓===============
Dim countZKZN1 As Integer         '全件・残高表示無し1
Dim countZKZN2 As Integer         '全件・残高表示無し2
'===========2011/02/18 車龍(大連) 追加 終了↑===============
'2015/03/05 曹敬仁  EARTH経理改修対応  Start
Dim syoukeiStart1 As Integer      '店別_1ページの小計開始行番号
Dim syoukeiEnd1 As Integer        '店別_1ページの小計終了行番号
Dim syoukeiStart2 As Integer      '店別_複数ページの小計開始行番号
Dim syoukeiEnd2 As Integer        '店別_複数ページの小計終了行番号
'2015/03/05 曹敬仁  EARTH経理改修対応  End
Dim sumZK As Double            '全件合計
Dim sumXK1 As Double             '店小計合計
Dim sumXK2 As Double             '店小計合計
Dim sumXK3 As Double             '店小計合計
Dim intCopy As Integer

Sub Auto_Open()

    Dim strFileName As String       'ファイル名を取得する
    Dim i As Integer                'ループ変数
    Dim aryMeisai() As String       '明細情報
    Dim strSheet As String          'シート情報
    Dim strSheetName As String          'シート情報
    
    countZK1 = 0        '全件1
    countZK2 = 0        '全件2
'===========2011/02/18 車龍(大連) 追加 開始↓===============
    countZKZN1 = 0        '全件・残高表示無し1
    countZKZN2 = 0        '全件・残高表示無し2
'===========2011/02/18 車龍(大連) 追加 終了↑===============
    countXK1 = 0        '店小計1
    countXK2 = 0        '店小計2
    countBK = 0         '物件
    
    sumZK = 0           '全件合計
    sumXK1 = 0
    sumXK2 = 0
    sumXK3 = 0
    intCopy = 0
    
    'ファイル名をセットする
    strFileName = ThisWorkbook.Name
    
    'ファイル名の拡張子をチェックする
    If UCase(Right(strFileName, 4)) <> UCase(".xls") Then
    
        'データの取得
        Call GetCsvData(aryMeisai)
        
        Call SetData(aryMeisai)
        
        Call SetSheet

        'strSheet = Replace(Trim(aryMeisai(0, UBound(aryMeisai, 2) - 1)), "<br>", "")
     
        'If strSheetName = "" Then
        '    strSheetName = "請求書(全件)１"
        'End If
            
        'ThisWorkbook.Worksheets(1).Select
        'ThisWorkbook.Worksheets(1).Cells(1, 1).Select
        MsgBox "帳票が展開されました。", , "EARTH"
    
    End If
End Sub

Private Function testData(ByRef aryMeisai() As String)

        '画面の更新が無効化
        ThisWorkbook.Worksheets("請求書(物件別)").Application.ScreenUpdating = False
        
        'ThisWorkbook.Worksheets("請求書(物件別)").Cells(1, 1).Select
    
        ThisWorkbook.Worksheets("請求書(物件別)").Cells(1, 2) = aryMeisai(0)
        
        'For i = 0 To UBound(arrData)
        '    ThisWorkbook.Worksheets("請求書(物件別)").Cells(100 + i, 1) = arrData(i)
        'Next
        
        '画面の更新が有効化
        ThisWorkbook.Worksheets("請求書(物件別)").Application.ScreenUpdating = True

End Function

'* < TOSTEM >***********************************************************************
'*
'*【関数名】
'*!   GetCsvData
'*
'*【処理概要】
'*?
'*?   明細情報を取得する;
'*
'*【パラメータ】
'*  < Name                  Type     Comment                                   >
'*P   aryMeisai();          String;  明細情報配列;
'*
'*【戻り値】
'*  < Name                  Type     Comment                                   >
'*R   xxxxxxxxxxxxxxxxxxxx; xxxxxxx; xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx;
'*
'***********************************************************************************
'*【変更履歴】
'*  < 日付         EDP-No.     課題管理№  修正№   説明　　     名前(会社名)     >
'*M   2007/09/20; P-32200  　　xxxx                新規作成；     高瑜(大連);
'*
'* < Comment End >******************************************************************
Private Function GetCsvData(ByRef aryMeisai() As String)
                
    Dim objFso As Object            'オブジェクト
    Dim objTs As Object             'オブジェクト
    Dim i, j As Integer             'ループ変数
    Dim aryBuff() As String         'Temp配列
    Dim pageCount As Integer        'count of pages
    Dim linecount As Integer        'count of lines
    Dim strData As String           'data of one page
    
    
    Set objFso = CreateObject("Scripting.FileSystemObject")
    
    '明細情報を読む
    Set objTs = objFso.OpenTextFile(conCsvFilePath & conDataFilename, 1, False)

    pageCount = 0
    'linecount = 0
    Do While Not objTs.AtEndOfLine
    
        Dim strLine As String
        strLine = objTs.readline
        
        If strLine = "[Form Section]" Then
            If pageCount = 0 Then
            
            Else
                ReDim Preserve aryBuff(pageCount - 1)
                aryBuff(pageCount - 1) = strData
                strData = ""

            End If
            pageCount = pageCount + 1
            strData = strLine
            
        Else
        
            If pageCount = 0 Then
            
            Else
                strData = strData & "$$$" & strLine
            End If
            
        End If
        'ReDim Preserve aryBuff(linecount)
        'aryBuff(linecount) = objTs.readline
        'linecount = linecount + 1
    Loop
    
    ReDim Preserve aryBuff(pageCount - 1)
    aryBuff(pageCount - 1) = strData

    'オブジェクトを閉じる
    objTs.Close
    Set objTs = Nothing
    Set objFso = Nothing
    
    '明細情報を取得する
    ReDim Preserve aryMeisai(UBound(aryBuff))
    For j = 0 To UBound(aryBuff)
       ' aryBuff = aryBuff(j)
        'その他の明細情報を取得する
        'For i = 0 To UBound(Split(aryBuff(j), ","))
        '    aryMeisai(j, i) = Replace(Split(aryBuff(j), ",")(i), Chr(34), "")
        'Next
        aryMeisai(j) = aryBuff(j)
    Next

End Function
'* < TOSTEM >***********************************************************************
'*
'*【関数名】
'*!   SetData
'*
'*【処理概要】
'*?
'*?   ＣＳＶファイルに物流代行料データ_20日締めをセットする
'*
'*【パラメータ】
'*  < Name                  Type     Comment                                   >
'*P   aryPara();            String;  ヘーダ情報配列;
'*P   aryMeisai;            String;  明細情報配列;
'*P   intMinRow;            Integer; 最小行;
'*P   intMaxRow;            Integer; 最大行;
'*
'*【戻り値】
'*  < Name                  Type     Comment                                   >
'*R   xxxxxxxxxxxxxxxxxxxx; xxxxxxx; xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx;
'*
'***********************************************************************************
'*【変更履歴】
'*  < 日付         EDP-No.     課題管理№  修正№   説明　　     名前(会社名)     >
'*M   2007/09/20; P-32200  　　xxxx                新規作成；     高瑜(大連);
'*
'* < Comment End >******************************************************************
Private Function SetData(ByRef aryMeisai() As String)
   
    Dim pageCount As Integer
    '2015/03/05 曹敬仁  EARTH経理改修対応  Start
    syoukeiStart1 = 0
    syoukeiEnd1 = 0
    syoukeiStart2 = 0
    syoukeiEnd2 = 0
    
    Dim allPageSuu As Integer
    allPageSuu = UBound(aryMeisai) + 1
    
    '2015/03/05 曹敬仁  EARTH経理改修対応  End
    For pageCount = 0 To UBound(aryMeisai)
    
        Dim arrData() As String
        arrData = Split(aryMeisai(pageCount), "$$$")
             
        Select Case Split(arrData(1), "=")(1)
            Case "SB_SEIKYUSYO_ZK1"
                '全件1
                Call SetZK1(arrData)
                
            Case "SB_SEIKYUSYO_ZK2"
                '全件2
                Call SetZK2(arrData)
'===========2011/02/18 車龍(大連) 追加 開始↓===============
            Case "SB_SEIKYUSYO_ZKZN1"
                '全件1
                Call SetZKZN1(arrData)
                
            Case "SB_SEIKYUSYO_ZKZN2"
                '全件2
                Call SetZKZN2(arrData)
'===========2011/02/18 車龍(大連) 追加 終了↑===============
            Case "SB_SEIKYUSYO_XK1"
                '店小計1
                Call SetXK1(arrData)
            
            Case "SB_SEIKYUSYO_XK2"
                '店小計2
                Call SetXK2(arrData)
            
            Case "SB_SEIKYUSYO_BK"
                '物件
                Call SetBK(arrData, allPageSuu)
                
        End Select
    Next

End Function
 
Private Function SetSheet()
   
    If countZK1 = 0 Then
        Sheets("請求書(全件)１").Select
        ActiveWindow.SelectedSheets.Visible = False
    End If
    If countZK2 = 0 Then
        Sheets("請求書(全件)２").Select
        ActiveWindow.SelectedSheets.Visible = False
    End If
    
'===========2011/02/18 車龍(大連) 追加 開始↓===============
    If countZKZN1 = 0 Then
        Sheets("請求書(全件・残高表示無し)１").Select
        ActiveWindow.SelectedSheets.Visible = False
    End If
    If countZKZN2 = 0 Then
        Sheets("請求書(全件・残高表示無し)２").Select
        ActiveWindow.SelectedSheets.Visible = False
    End If
'===========2011/02/18 車龍(大連) 追加 終了↑===============

    If countXK1 = 0 Then
        Sheets("請求書(全件_店小計)１").Select
        ActiveWindow.SelectedSheets.Visible = False
    End If
    If countXK2 = 0 Then
        Sheets("請求書(全件_店小計)２").Select
        ActiveWindow.SelectedSheets.Visible = False
    End If
    If countBK = 0 Then
        Sheets("請求書(物件別)").Select
        ActiveWindow.SelectedSheets.Visible = False
    End If
    
End Function
  
'全件1
Private Function SetZK1(ByRef arrData() As String)

        countZK1 = countZK1 + 1

        '画面の更新が無効化
        ThisWorkbook.Worksheets("請求書(全件)１").Application.ScreenUpdating = False
        
        'ThisWorkbook.Worksheets("請求書(全件)１").Cells(1, 1).Select
        'ThisWorkbook.Worksheets("請求書(全件)１").Cells(1, countZK1) = countZK1
        'Dim i As Integer
        'For i = 0 To UBound(arrData)
        '    ThisWorkbook.Worksheets("請求書(全件)１").Cells(100 + i, 1) = arrData(i)
        'Next
        
        ThisWorkbook.Worksheets("請求書(全件)１").Select
                
        sumZK = 0           '全件合計
        
        Dim beginLine As Integer
        beginLine = (countZK1 - 1) * 66
        
        If countZK1 = 1 Then
        
        Else
            Rows("1:66").Copy
            Rows(CStr(beginLine + 1) & ":" & CStr(beginLine + 1)).Insert Shift:=xlDown
            ActiveSheet.PageSetup.PrintArea = "$A$1:$BK$" & CStr(countZK1 * 66)
            
            Dim pageCount As Integer
            For pageCount = 1 To countZK1 - 1
                Set ActiveSheet.HPageBreaks(pageCount).Location = Range("A" & CStr(pageCount * 66 + 1))
            Next
        End If
        
        Dim onlyOnePageFlg As Boolean
        
        Dim page As String
        page = Replace(Trim(Split(arrData(5), "=")(1)), "ページ", "")
        
        If UBound(Split(page, "/")) = 0 Then
            onlyOnePageFlg = True
        Else
            onlyOnePageFlg = False
        End If
        
        '[Fixed Data Section]
        Range("I" & CStr(beginLine + 2)) = Split(arrData(4), "=")(1)
        Range("AY" & CStr(beginLine + 2)) = Split(arrData(5), "=")(1)
        Dim nengetu As String
        nengetu = Split(arrData(6), "=")(1)
        If nengetu <> "" Then
            Range("AR" & CStr(beginLine + 4)) = Mid(nengetu, 1, 4) & "/" & Mid(nengetu, 5, 2) & "/" & Mid(nengetu, 7, 2)
        End If
        Range("D" & CStr(beginLine + 5)) = Split(arrData(7), "=")(1)
        Range("D" & CStr(beginLine + 6)) = Split(arrData(8), "=")(1)
        Range("D" & CStr(beginLine + 7)) = Split(arrData(9), "=")(1)
        Range("F" & CStr(beginLine + 8)) = Split(arrData(10), "=")(1)
        Range("F" & CStr(beginLine + 9)) = Split(arrData(11), "=")(1)
        Range("D" & CStr(beginLine + 10)) = Split(arrData(12), "=")(1)
        Range("K" & CStr(beginLine + 13)) = Split(arrData(13), "=")(1)
        Range("C" & CStr(beginLine + 17)) = Split(arrData(14), "=")(1)
        Range("C" & CStr(beginLine + 18)) = Split(arrData(15), "=")(1)
        Range("C" & CStr(beginLine + 19)) = Split(arrData(16), "=")(1)
        Range("AI" & CStr(beginLine + 13)) = Split(arrData(17), "=")(1)
        '2013/6/8 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
        'Range("AO" & CStr(beginLine + 14)) = Split(arrData(18), "=")(1)
        'Range("AQ" & CStr(beginLine + 15)) = Split(arrData(19), "=")(1)
        Range("AI" & CStr(beginLine + 14)) = Split(arrData(18), "=")(1)
        Range("AI" & CStr(beginLine + 15)) = Split(arrData(19), "=")(1)
        '2013/6/8 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑
        Range("AQ" & CStr(beginLine + 17)) = Split(arrData(20), "=")(1)
        Range("AQ" & CStr(beginLine + 18)) = Split(arrData(21), "=")(1)
        Range("AQ" & CStr(beginLine + 19)) = Split(arrData(22), "=")(1)
        Range("B" & CStr(beginLine + 22)) = Trim(Split(arrData(23), "=")(1))
        Range("L" & CStr(beginLine + 22)) = Trim(Split(arrData(24), "=")(1))
        Range("V" & CStr(beginLine + 22)) = Trim(Split(arrData(25), "=")(1))
        Range("AF" & CStr(beginLine + 22)) = Trim(Split(arrData(26), "=")(1))
        Range("AP" & CStr(beginLine + 22)) = Trim(Split(arrData(27), "=")(1))
        '2015/03/05 曹敬仁  EARTH経理改修対応  Start
        'Range("BA" & CStr(beginLine + 22)) = Trim(Split(arrData(28), "=")(1))
        '2015/03/05 曹敬仁  EARTH経理改修対応  End
        
        '[Table Data Section]
        Dim dataLineCount As Integer
        
        Dim total_uriage As Double
        Dim total_sotozei As Double
        Dim total_uriage_sotozei As Double
        
        total_uriage = 0
        total_sotozei = 0
        total_uriage_sotozei = 0
                
        '明細
        For dataLineCount = 0 To 17
            If UBound(arrData) >= (53 + dataLineCount) Then
                '売上年月日
                Dim hiduke As String
                hiduke = Split(arrData(53 + dataLineCount), "&")(0)
                If hiduke <> "" Then
                    Range("A" & CStr(26 + dataLineCount * 2 + beginLine)) = Mid(hiduke, 1, 4) & "/" & Mid(hiduke, 5, 2) & "/" & Mid(hiduke, 7, 2)
                Else
                    Range("A" & CStr(26 + dataLineCount * 2 + beginLine)) = ""
                End If
                '物件番号
                Range("F" & CStr(26 + dataLineCount * 2 + beginLine)) = Split(arrData(53 + dataLineCount), "&")(1)
                '商品名
                Range("K" & CStr(26 + dataLineCount * 2 + beginLine)) = Split(arrData(53 + dataLineCount), "&")(2)
                '物件名
                Range("K" & CStr(26 + dataLineCount * 2 + 1 + beginLine)) = Split(arrData(53 + dataLineCount), "&")(3)
                '数量
                Range("AG" & CStr(26 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(4))
                '単価
                Range("AJ" & CStr(26 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(5))
                '税別金額
                Range("AP" & CStr(26 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(6))
                '税率
                Range("AW" & CStr(26 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(9))
                '消費税
                Range("AY" & CStr(26 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(7))
                '2015/03/05 曹敬仁  EARTH経理改修対応  Start
                '総額
                'Range("BE" & CStr(26 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(8))
                '2015/03/05 曹敬仁  EARTH経理改修対応  End
            Else
                '売上年月日
                Range("A" & CStr(26 + dataLineCount * 2 + beginLine)) = ""
                '物件番号
                Range("F" & CStr(26 + dataLineCount * 2 + beginLine)) = ""
                '商品名
                Range("K" & CStr(26 + dataLineCount * 2 + beginLine)) = ""
                '物件名
                Range("K" & CStr(26 + dataLineCount * 2 + 1 + beginLine)) = ""
                '数量
                Range("AG" & CStr(26 + dataLineCount * 2 + beginLine)) = ""
                '単価
                Range("AJ" & CStr(26 + dataLineCount * 2 + beginLine)) = ""
                '税別金額
                Range("AP" & CStr(26 + dataLineCount * 2 + beginLine)) = ""
                '税率
                Range("AW" & CStr(26 + dataLineCount * 2 + beginLine)) = ""
                '消費税
                Range("AY" & CStr(26 + dataLineCount * 2 + beginLine)) = ""
                '2015/03/05 曹敬仁  EARTH経理改修対応  Start
                '総額
                'Range("BE" & CStr(26 + dataLineCount * 2 + beginLine)) = ""
                '2015/03/05 曹敬仁  EARTH経理改修対応  End
            End If
        Next
 
        '2015/03/05 曹敬仁  EARTH経理改修対応  Start
        '小計(0%)_税別金額
        'Range("AP" & CStr(beginLine + 62)) = Trim(Split(arrData(29), "=")(1))
        '小計(0%)_消費税
        'Range("AY" & CStr(beginLine + 62)) = ""
        '小計(0%)_総額
        'Range("BE" & CStr(beginLine + 62)) = Trim(Split(arrData(31), "=")(1))
        '小計(5%)_税別金額
        'Range("AP" & CStr(beginLine + 63)) = Trim(Split(arrData(32), "=")(1))
        '小計(5%)_消費税
        'Range("AY" & CStr(beginLine + 63)) = Trim(Split(arrData(33), "=")(1))
        '小計(5%)_総額
        'Range("BE" & CStr(beginLine + 63)) = Trim(Split(arrData(34), "=")(1))
        '小計(8%)_税別金額
        'Range("AP" & CStr(beginLine + 64)) = Trim(Split(arrData(35), "=")(1))
        '小計(8%)_消費税
        'Range("AY" & CStr(beginLine + 64)) = Trim(Split(arrData(36), "=")(1))
        '小計(8%)_総額
        'Range("BE" & CStr(beginLine + 64)) = Trim(Split(arrData(37), "=")(1))
        '2015/03/05 曹敬仁  EARTH経理改修対応  End
        
        '総合計
        If Split(arrData(50), "=")(1) = "" Then
            '総合計行を追加する
            Range("A" & CStr(beginLine + 62) & ":BK" & CStr(beginLine + 62)).Select
            Selection.Copy
            Range("A" & CStr(beginLine + 65) & ":BK" & CStr(beginLine + 65)).Select
            ActiveSheet.Paste
            '総合計行をセットする
            Selection.Interior.ColorIndex = 16
            Range("K" & CStr(beginLine + 65)) = "総合計"
            '2015/03/05 曹敬仁  EARTH経理改修対応  Start
            'Range("AP" & CStr(beginLine + 65)) = Trim(Split(arrData(47), "=")(1))
            'Range("AY" & CStr(beginLine + 65)) = Trim(Split(arrData(48), "=")(1))
            'Range("BE" & CStr(beginLine + 65)) = Trim(Split(arrData(49), "=")(1))
            Range("AP" & CStr(beginLine + 65)).Formula = "=SUM(AP62:AP64)"
            Range("AY" & CStr(beginLine + 65)).Formula = "=SUM(AY63:AY64)"
            Range("BE" & CStr(beginLine + 65)).Formula = "=SUM(BE62:BE64)"
            Range("K13").Formula = "=BE65"
            Range("AP22").Formula = "=BE65"
            '2015/03/05 曹敬仁  EARTH経理改修対応  End
        Else
            '総合計行がない
            Range("A" & CStr(beginLine + 65) & ":BK" & CStr(beginLine + 65)).Clear
        End If
     
        '2015/05/27 曹敬仁  EARTH経理改修対応  Start
        Application.CutCopyMode = False    'コピーモード解除
        '2015/05/27 曹敬仁  EARTH経理改修対応  End
        
        '画面の更新が有効化
        ThisWorkbook.Worksheets("請求書(全件)１").Application.ScreenUpdating = True
        ThisWorkbook.Worksheets("請求書(全件)１").Range("A1").Select

End Function


'全件2
Private Function SetZK2(ByRef arrData() As String)

        countZK2 = countZK2 + 1

        '画面の更新が無効化
        ThisWorkbook.Worksheets("請求書(全件)２").Application.ScreenUpdating = False
        
        ThisWorkbook.Worksheets("請求書(全件)２").Select
        
        'ThisWorkbook.Worksheets("請求書(全件)２").Cells(1, 1).Select
    
        'ThisWorkbook.Worksheets("請求書(全件)２").Cells(1, countZK2) = countZK2
        '2015/03/05 曹敬仁  EARTH経理改修対応  Start
        Dim pageCount As Integer
        Dim sumAP0Formula As String
        Dim sumAP5Formula As String
        Dim sumAP8Formula As String
        Dim sumAY5Formula As String
        Dim sumAY8Formula As String
        '2015/03/05 曹敬仁  EARTH経理改修対応  End
        Dim beginLine As Integer
        beginLine = (countZK2 - 1) * 64
        
        If countZK2 = 1 Then
        
        Else
            Rows("1:64").Copy
            Rows(CStr(beginLine + 1) & ":" & CStr(beginLine + 1)).Insert Shift:=xlDown
            ActiveSheet.PageSetup.PrintArea = "$A$1:$BK$" & CStr(countZK2 * 64)

            'Dim pageCount As Integer
            For pageCount = 1 To countZK2 - 1
                Set ActiveSheet.HPageBreaks(pageCount).Location = Range("A" & CStr(pageCount * 64 + 1))
            Next
        End If
     
        Dim onlyOnePageFlg As Boolean
        
        Dim page As String
        page = Replace(Trim(Split(arrData(5), "=")(1)), "ページ", "")
        
        If UBound(Split(page, "/")) = 0 Then
            onlyOnePageFlg = True
        Else
            onlyOnePageFlg = False
        End If
     
        '[Fixed Data Section]
        Range("I" & CStr(beginLine + 2)) = Split(arrData(4), "=")(1)
        Range("AY" & CStr(beginLine + 2)) = Split(arrData(5), "=")(1)
        Dim nengetu As String
        nengetu = Split(arrData(6), "=")(1)
        If nengetu <> "" Then
            Range("AR" & CStr(beginLine + 4)) = Mid(nengetu, 1, 4) & "/" & Mid(nengetu, 5, 2) & "/" & Mid(nengetu, 7, 2)
        End If
        Range("D" & CStr(beginLine + 5)) = Split(arrData(7), "=")(1)
        Range("D" & CStr(beginLine + 6)) = Split(arrData(8), "=")(1)
        Range("D" & CStr(beginLine + 7)) = Split(arrData(9), "=")(1)
        Range("F" & CStr(beginLine + 8)) = Split(arrData(10), "=")(1)
        Range("F" & CStr(beginLine + 9)) = Split(arrData(11), "=")(1)
        Range("D" & CStr(beginLine + 10)) = Split(arrData(12), "=")(1)
     
        '[Table Data Section]
        Dim dataLineCount As Integer
        
        Dim total_uriage As Double
        Dim total_sotozei As Double
        Dim total_uriage_sotozei As Double
        
        total_uriage = 0
        total_sotozei = 0
        total_uriage_sotozei = 0

        '明細
        For dataLineCount = 0 To 19
            If UBound(arrData) >= (53 + dataLineCount) Then
                '売上年月日
                Dim hiduke As String
                hiduke = Split(arrData(53 + dataLineCount), "&")(0)
                If hiduke <> "" Then
                    Range("A" & CStr(16 + dataLineCount * 2 + beginLine)) = Mid(hiduke, 1, 4) & "/" & Mid(hiduke, 5, 2) & "/" & Mid(hiduke, 7, 2)
                Else
                    Range("A" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                End If
                '物件番号
                Range("F" & CStr(16 + dataLineCount * 2 + beginLine)) = Split(arrData(53 + dataLineCount), "&")(1)
                '商品名
                Range("K" & CStr(16 + dataLineCount * 2 + beginLine)) = Split(arrData(53 + dataLineCount), "&")(2)
                '物件名
                Range("K" & CStr(16 + dataLineCount * 2 + 1 + beginLine)) = Split(arrData(53 + dataLineCount), "&")(3)
                '数量
                Range("AG" & CStr(16 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(4))
                '単価
                Range("AJ" & CStr(16 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(5))
                '税別金額
                Range("AP" & CStr(16 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(6))
                '税率
                Range("AW" & CStr(16 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(9))
                '消費税
                Range("AY" & CStr(16 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(7))
                '2015/03/05 曹敬仁  EARTH経理改修対応  Start
                '総額
                'Range("BE" & CStr(16 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(8))
                '2015/03/05 曹敬仁  EARTH経理改修対応  End
            Else
                '売上年月日
                Range("A" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '物件番号
                Range("F" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '商品名
                Range("K" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '物件名
                Range("K" & CStr(16 + dataLineCount * 2 + 1 + beginLine)) = ""
                '数量
                Range("AG" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '単価
                Range("AJ" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '税別金額
                Range("AP" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '税率
                Range("AW" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '消費税
                Range("AY" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '2015/03/05 曹敬仁  EARTH経理改修対応  Start
                '総額
                'Range("BE" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '2015/03/05 曹敬仁  EARTH経理改修対応  End
            End If
        Next
     
        '2015/03/05 曹敬仁  EARTH経理改修対応  Start
        '小計(0%)_税別金額
        'Range("AP" & CStr(beginLine + 56)) = Trim(Split(arrData(29), "=")(1))
        '小計(0%)_消費税
        'Range("AY" & CStr(beginLine + 56)) = ""
        '小計(0%)_総額
        'Range("BE" & CStr(beginLine + 56)) = Trim(Split(arrData(31), "=")(1))
        '小計(5%)_税別金額
        'Range("AP" & CStr(beginLine + 57)) = Trim(Split(arrData(32), "=")(1))
        '小計(5%)_消費税
        'Range("AY" & CStr(beginLine + 57)) = Trim(Split(arrData(33), "=")(1))
        '小計(5%)_総額
        'Range("BE" & CStr(beginLine + 57)) = Trim(Split(arrData(34), "=")(1))
        '小計(8%)_税別金額
        'Range("AP" & CStr(beginLine + 58)) = Trim(Split(arrData(35), "=")(1))
        '小計(8%)_消費税
        'Range("AY" & CStr(beginLine + 58)) = Trim(Split(arrData(36), "=")(1))
        '小計(8%)_総額
        'Range("BE" & CStr(beginLine + 58)) = Trim(Split(arrData(37), "=")(1))
        '2015/03/05 曹敬仁  EARTH経理改修対応  End
        
        '合計、総合計
        If Split(arrData(50), "=")(1) = "" Then
            '合計行を追加する
            Range("A" & CStr(beginLine + 56) & ":BK" & CStr(beginLine + 58)).Select
            Selection.Copy
            Range("A" & CStr(beginLine + 59) & ":BK" & CStr(beginLine + 61)).Select
            ActiveSheet.Paste
            '合計行をセットする
            Selection.Interior.ColorIndex = 15
            '2015/03/05 曹敬仁  EARTH経理改修対応  Start
            '   合計(0%)
            'Range("K" & CStr(beginLine + 59)) = "　　　　　合計（非課税）"
            'Range("AP" & CStr(beginLine + 59)) = Trim(Split(arrData(38), "=")(1)) '税別金額
            'Range("AY" & CStr(beginLine + 59)) = "" '消費税
            'Range("BE" & CStr(beginLine + 59)) = Trim(Split(arrData(40), "=")(1)) '総額
            '   合計(5%)
            'Range("K" & CStr(beginLine + 60)) = "合計"
            'Range("AP" & CStr(beginLine + 60)) = Trim(Split(arrData(41), "=")(1)) '税別金額
            'Range("AY" & CStr(beginLine + 60)) = Trim(Split(arrData(42), "=")(1)) '消費税
            'Range("BE" & CStr(beginLine + 60)) = Trim(Split(arrData(43), "=")(1)) '総額
            '   合計(8%)
            'Range("K" & CStr(beginLine + 61)) = "合計"
            'Range("AP" & CStr(beginLine + 61)) = Trim(Split(arrData(44), "=")(1)) '税別金額
            'Range("AY" & CStr(beginLine + 61)) = Trim(Split(arrData(45), "=")(1)) '消費税
            'Range("BE" & CStr(beginLine + 61)) = Trim(Split(arrData(46), "=")(1)) '総額
            
            For pageCount = 1 To countZK2
                sumAP0Formula = sumAP0Formula & " + '請求書(全件)２'!AP" & CStr((pageCount - 1) * 64 + 56)
                sumAP5Formula = sumAP5Formula & " + '請求書(全件)２'!AP" & CStr((pageCount - 1) * 64 + 57)
                sumAP8Formula = sumAP8Formula & " + '請求書(全件)２'!AP" & CStr((pageCount - 1) * 64 + 58)
                sumAY5Formula = sumAY5Formula & " + '請求書(全件)２'!AY" & CStr((pageCount - 1) * 64 + 57)
                sumAY8Formula = sumAY8Formula & " + '請求書(全件)２'!AY" & CStr((pageCount - 1) * 64 + 58)
            Next
            
            '   合計(0%)
            Range("K" & CStr(beginLine + 59)) = "　　　　　合計（非課税）"
            Range("AP" & CStr(beginLine + 59)).Formula = "='請求書(全件)１'!AP62 " & sumAP0Formula & "" '税別金額
            Range("AY" & CStr(beginLine + 59)) = "" '消費税
            Range("BE" & CStr(beginLine + 59)).Formula = "=AP" & CStr(beginLine + 59) & "+AY" & CStr(beginLine + 59) & "" '総額
            '   合計(5%)
            Range("K" & CStr(beginLine + 60)) = "合計"
            Range("AP" & CStr(beginLine + 60)).Formula = "='請求書(全件)１'!AP63 " & sumAP5Formula & "" '税別金額
            Range("AY" & CStr(beginLine + 60)).Formula = "='請求書(全件)１'!AY63 " & sumAY5Formula & "" '消費税
            Range("BE" & CStr(beginLine + 60)).Formula = "=AP" & CStr(beginLine + 60) & "+AY" & CStr(beginLine + 60) & "" '総額
            '   合計(8%)
            Range("K" & CStr(beginLine + 61)) = "合計"
            Range("AP" & CStr(beginLine + 61)).Formula = "='請求書(全件)１'!AP64 " & sumAP8Formula & "" '税別金額
            Range("AY" & CStr(beginLine + 61)).Formula = "='請求書(全件)１'!AY64 " & sumAY8Formula & "" '消費税
            Range("BE" & CStr(beginLine + 61)).Formula = "=AP" & CStr(beginLine + 61) & "+AY" & CStr(beginLine + 61) & "" '総額
            '2015/03/05 曹敬仁  EARTH経理改修対応  End
            
            '総合計行を追加する
            Range("A" & CStr(beginLine + 56) & ":BK" & CStr(beginLine + 56)).Select
            Selection.Copy
            Range("A" & CStr(beginLine + 62) & ":BK" & CStr(beginLine + 62)).Select
            ActiveSheet.Paste
            '総合計行をセットする
            Selection.Interior.ColorIndex = 16
            Range("K" & CStr(beginLine + 62)) = "総合計"
            '2015/03/05 曹敬仁  EARTH経理改修対応  Start
            'Range("AP" & CStr(beginLine + 62)) = Trim(Split(arrData(47), "=")(1))
            'Range("AY" & CStr(beginLine + 62)) = Trim(Split(arrData(48), "=")(1))
            'Range("BE" & CStr(beginLine + 62)) = Trim(Split(arrData(49), "=")(1))
            Range("AP" & CStr(beginLine + 62)).Formula = "=SUM(AP" & CStr(beginLine + 59) & ":AP" & CStr(beginLine + 61) & ")"
            Range("AY" & CStr(beginLine + 62)).Formula = "=SUM(AY" & CStr(beginLine + 60) & ":AY" & CStr(beginLine + 61) & ")"
            Range("BE" & CStr(beginLine + 62)).Formula = "=SUM(BE" & CStr(beginLine + 59) & ":BE" & CStr(beginLine + 61) & ")"
            ThisWorkbook.Worksheets("請求書(全件)１").Range("K13").Formula = "='請求書(全件)２'!BE" & CStr(beginLine + 62) & ""
            ThisWorkbook.Worksheets("請求書(全件)１").Range("AP22").Formula = "='請求書(全件)２'!BE" & CStr(beginLine + 62) & ""
            '2015/03/05 曹敬仁  EARTH経理改修対応  End
        Else
            '合計、総合計がない
            Range("A" & CStr(beginLine + 59) & ":BK" & CStr(beginLine + 62)).Clear
        End If
        
        '2015/05/27 曹敬仁  EARTH経理改修対応  Start
        Application.CutCopyMode = False    'コピーモード解除
        '2015/05/27 曹敬仁  EARTH経理改修対応  End
        
        '画面の更新が有効化
        ThisWorkbook.Worksheets("請求書(全件)２").Application.ScreenUpdating = True
        ThisWorkbook.Worksheets("請求書(全件)２").Range("A1").Select

End Function

'===========2011/02/18 車龍(大連) 追加 開始↓===============

'全件・残高表示無し1
Private Function SetZKZN1(ByRef arrData() As String)

        countZKZN1 = countZKZN1 + 1

        '画面の更新が無効化
        ThisWorkbook.Worksheets("請求書(全件・残高表示無し)１").Application.ScreenUpdating = False
        
        'ThisWorkbook.Worksheets("請求書(全件)１").Cells(1, 1).Select
        'ThisWorkbook.Worksheets("請求書(全件)１").Cells(1, countZK1) = countZK1
        'Dim i As Integer
        'For i = 0 To UBound(arrData)
        '    ThisWorkbook.Worksheets("請求書(全件)１").Cells(100 + i, 1) = arrData(i)
        'Next
        
        ThisWorkbook.Worksheets("請求書(全件・残高表示無し)１").Select
                
        'sumZK = 0           '全件合計
        
        Dim beginLine As Integer
        beginLine = (countZKZN1 - 1) * 67
        
        If countZKZN1 = 1 Then
        
        Else
            Rows("1:67").Copy
            Rows(CStr(beginLine + 1) & ":" & CStr(beginLine + 1)).Insert Shift:=xlDown
            ActiveSheet.PageSetup.PrintArea = "$A$1:$BK$" & CStr(countZKZN1 * 67)
            
            Dim pageCount As Integer
            For pageCount = 1 To countZKZN1 - 1
                Set ActiveSheet.HPageBreaks(pageCount).Location = Range("A" & CStr(pageCount * 67 + 1))
            Next
        End If
        
        Dim onlyOnePageFlg As Boolean
        
        Dim page As String
        page = Replace(Trim(Split(arrData(5), "=")(1)), "ページ", "")
        
        If UBound(Split(page, "/")) = 0 Then
            onlyOnePageFlg = True
        Else
            onlyOnePageFlg = False
        End If
        
        '[Fixed Data Section]
        Range("I" & CStr(beginLine + 2)) = Split(arrData(4), "=")(1)
        Range("AY" & CStr(beginLine + 2)) = Split(arrData(5), "=")(1)
        Dim nengetu As String
        nengetu = Split(arrData(6), "=")(1)
        If nengetu <> "" Then
            Range("AR" & CStr(beginLine + 4)) = Mid(nengetu, 1, 4) & "/" & Mid(nengetu, 5, 2) & "/" & Mid(nengetu, 7, 2)
        End If
        Range("D" & CStr(beginLine + 5)) = Split(arrData(7), "=")(1)
        Range("D" & CStr(beginLine + 6)) = Split(arrData(8), "=")(1)
        Range("D" & CStr(beginLine + 7)) = Split(arrData(9), "=")(1)
        Range("F" & CStr(beginLine + 8)) = Split(arrData(10), "=")(1)
        Range("F" & CStr(beginLine + 9)) = Split(arrData(11), "=")(1)
        Range("D" & CStr(beginLine + 10)) = Split(arrData(12), "=")(1)
        Range("K" & CStr(beginLine + 13)) = Split(arrData(13), "=")(1)
        Range("C" & CStr(beginLine + 17)) = Split(arrData(14), "=")(1)
        Range("C" & CStr(beginLine + 18)) = Split(arrData(15), "=")(1)
        Range("C" & CStr(beginLine + 19)) = Split(arrData(16), "=")(1)
        Range("AI" & CStr(beginLine + 13)) = Split(arrData(17), "=")(1)
        '2013/6/8 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
        'Range("AO" & CStr(beginLine + 14)) = Split(arrData(18), "=")(1)
        'Range("AQ" & CStr(beginLine + 15)) = Split(arrData(19), "=")(1)
        Range("AI" & CStr(beginLine + 14)) = Split(arrData(18), "=")(1)
        Range("AI" & CStr(beginLine + 15)) = Split(arrData(19), "=")(1)
        '2013/6/8 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑
        Range("AQ" & CStr(beginLine + 17)) = Split(arrData(20), "=")(1)
        Range("AQ" & CStr(beginLine + 18)) = Split(arrData(21), "=")(1)
        Range("AQ" & CStr(beginLine + 19)) = Split(arrData(22), "=")(1)
        'Range("B" & CStr(beginLine + 22)) = Trim(Split(arrData(23), "=")(1))
        'Range("L" & CStr(beginLine + 22)) = Trim(Split(arrData(24), "=")(1))
        'Range("V" & CStr(beginLine + 22)) = Trim(Split(arrData(25), "=")(1))
        'Range("AF" & CStr(beginLine + 22)) = Trim(Split(arrData(26), "=")(1))
        'Range("AP" & CStr(beginLine + 22)) = Trim(Split(arrData(27), "=")(1))
        'Range("BA" & CStr(beginLine + 22)) = Trim(Split(arrData(28), "=")(1))
        
        '[Table Data Section]
        Dim dataLineCount As Integer
        
        'Dim total_uriage As Double
        'Dim total_sotozei As Double
        'Dim total_uriage_sotozei As Double
        
        'total_uriage = 0
        'total_sotozei = 0
        'total_uriage_sotozei = 0
                
        '明細
        For dataLineCount = 0 To 19
            If UBound(arrData) >= (53 + dataLineCount) Then
                '売上年月日
                Dim hiduke As String
                hiduke = Split(arrData(53 + dataLineCount), "&")(0)
                If hiduke <> "" Then
                    Range("A" & CStr(23 + dataLineCount * 2 + beginLine)) = Mid(hiduke, 1, 4) & "/" & Mid(hiduke, 5, 2) & "/" & Mid(hiduke, 7, 2)
                Else
                    Range("A" & CStr(23 + dataLineCount * 2 + beginLine)) = ""
                End If
                '物件番号
                Range("F" & CStr(23 + dataLineCount * 2 + beginLine)) = Split(arrData(53 + dataLineCount), "&")(1)
                '商品名
                Range("K" & CStr(23 + dataLineCount * 2 + beginLine)) = Split(arrData(53 + dataLineCount), "&")(2)
                '物件名
                Range("K" & CStr(23 + dataLineCount * 2 + 1 + beginLine)) = Split(arrData(53 + dataLineCount), "&")(3)
                '数量
                Range("AG" & CStr(23 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(4))
                '単価
                Range("AJ" & CStr(23 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(5))
                '税別金額
                Range("AP" & CStr(23 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(6))
                '税率
                Range("AW" & CStr(23 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(9))
                '消費税
                Range("AY" & CStr(23 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(7))
                '2015/03/05 曹敬仁  EARTH経理改修対応  Start
                '総額
                'Range("BE" & CStr(23 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(8))
                '2015/03/05 曹敬仁  EARTH経理改修対応  End
            Else
                '売上年月日
                Range("A" & CStr(23 + dataLineCount * 2 + beginLine)) = ""
                '物件番号
                Range("F" & CStr(23 + dataLineCount * 2 + beginLine)) = ""
                '商品名
                Range("K" & CStr(23 + dataLineCount * 2 + beginLine)) = ""
                '物件名
                Range("K" & CStr(23 + dataLineCount * 2 + 1 + beginLine)) = ""
                '数量
                Range("AG" & CStr(23 + dataLineCount * 2 + beginLine)) = ""
                '単価
                Range("AJ" & CStr(23 + dataLineCount * 2 + beginLine)) = ""
                '税別金額
                Range("AP" & CStr(23 + dataLineCount * 2 + beginLine)) = ""
                '税率
                Range("AW" & CStr(23 + dataLineCount * 2 + beginLine)) = ""
                '消費税
                Range("AY" & CStr(23 + dataLineCount * 2 + beginLine)) = ""
                '2015/03/05 曹敬仁  EARTH経理改修対応  Start
                '総額
                'Range("BE" & CStr(23 + dataLineCount * 2 + beginLine)) = ""
                '2015/03/05 曹敬仁  EARTH経理改修対応  End
            End If
        Next
 
        '2015/03/05 曹敬仁  EARTH経理改修対応  Start
        '小計(0%)_税別金額
        'Range("AP" & CStr(beginLine + 63)) = Trim(Split(arrData(29), "=")(1))
        '小計(0%)_消費税
        'Range("AY" & CStr(beginLine + 63)) = ""
        '小計(0%)_総額
        'Range("BE" & CStr(beginLine + 63)) = Trim(Split(arrData(31), "=")(1))
        '小計(5%)_税別金額
        'Range("AP" & CStr(beginLine + 64)) = Trim(Split(arrData(32), "=")(1))
        '小計(5%)_消費税
        'Range("AY" & CStr(beginLine + 64)) = Trim(Split(arrData(33), "=")(1))
        '小計(5%)_総額
        'Range("BE" & CStr(beginLine + 64)) = Trim(Split(arrData(34), "=")(1))
        '小計(8%)_税別金額
        'Range("AP" & CStr(beginLine + 65)) = Trim(Split(arrData(35), "=")(1))
        '小計(8%)_消費税
        'Range("AY" & CStr(beginLine + 65)) = Trim(Split(arrData(36), "=")(1))
        '小計(8%)_総額
        'Range("BE" & CStr(beginLine + 65)) = Trim(Split(arrData(37), "=")(1))
        '2015/03/05 曹敬仁  EARTH経理改修対応  End
        
        '総合計
        If Split(arrData(50), "=")(1) = "" Then
            '総合計行を追加する
            Range("A" & CStr(beginLine + 63) & ":BK" & CStr(beginLine + 63)).Select
            Selection.Copy
            Range("A" & CStr(beginLine + 66) & ":BK" & CStr(beginLine + 66)).Select
            ActiveSheet.Paste
            '総合計行をセットする
            Selection.Interior.ColorIndex = 16
            Range("K" & CStr(beginLine + 66)) = "総合計"
            '2015/03/05 曹敬仁  EARTH経理改修対応  Start
            'Range("AP" & CStr(beginLine + 66)) = Trim(Split(arrData(47), "=")(1))
            'Range("AY" & CStr(beginLine + 66)) = Trim(Split(arrData(48), "=")(1))
            'Range("BE" & CStr(beginLine + 66)) = Trim(Split(arrData(49), "=")(1))
            Range("AP" & CStr(beginLine + 66)).Formula = "=SUM(AP63:AP65)"
            Range("AY" & CStr(beginLine + 66)).Formula = "=SUM(AY64:AY65)"
            Range("BE" & CStr(beginLine + 66)).Formula = "=SUM(BE63:BE65)"
            Range("K13").Formula = "=BE66"
            '2015/03/05 曹敬仁  EARTH経理改修対応  End
        Else
            '総合計行がない
            Range("A" & CStr(beginLine + 66) & ":BK" & CStr(beginLine + 66)).Clear
        End If
        
        '2015/05/27 曹敬仁  EARTH経理改修対応  Start
        Application.CutCopyMode = False    'コピーモード解除
        '2015/05/27 曹敬仁  EARTH経理改修対応  End
        
        '画面の更新が有効化
        ThisWorkbook.Worksheets("請求書(全件・残高表示無し)１").Application.ScreenUpdating = True
        ThisWorkbook.Worksheets("請求書(全件・残高表示無し)１").Range("A1").Select

End Function


'全件・残高表示無し2
Private Function SetZKZN2(ByRef arrData() As String)

        countZKZN2 = countZKZN2 + 1

        '画面の更新が無効化
        ThisWorkbook.Worksheets("請求書(全件・残高表示無し)２").Application.ScreenUpdating = False
        
        ThisWorkbook.Worksheets("請求書(全件・残高表示無し)２").Select
        
        'ThisWorkbook.Worksheets("請求書(全件)２").Cells(1, 1).Select
    
        'ThisWorkbook.Worksheets("請求書(全件)２").Cells(1, countZK2) = countZK2
        '2015/03/05 曹敬仁  EARTH経理改修対応  Start
        Dim pageCount As Integer
        Dim sumAP0Formula As String
        Dim sumAP5Formula As String
        Dim sumAP8Formula As String
        Dim sumAY5Formula As String
        Dim sumAY8Formula As String
        '2015/03/05 曹敬仁  EARTH経理改修対応  End
        Dim beginLine As Integer
        beginLine = (countZKZN2 - 1) * 64
        
        If countZKZN2 = 1 Then
        
        Else
            Rows("1:64").Copy
            Rows(CStr(beginLine + 1) & ":" & CStr(beginLine + 1)).Insert Shift:=xlDown
            ActiveSheet.PageSetup.PrintArea = "$A$1:$BK$" & CStr(countZKZN2 * 64)

            'Dim pageCount As Integer
            For pageCount = 1 To countZKZN2 - 1
                Set ActiveSheet.HPageBreaks(pageCount).Location = Range("A" & CStr(pageCount * 64 + 1))
            Next
        End If
     
        Dim onlyOnePageFlg As Boolean
        
        Dim page As String
        page = Replace(Trim(Split(arrData(5), "=")(1)), "ページ", "")
        
        If UBound(Split(page, "/")) = 0 Then
            onlyOnePageFlg = True
        Else
            onlyOnePageFlg = False
        End If
     
        '[Fixed Data Section]
        Range("I" & CStr(beginLine + 2)) = Split(arrData(4), "=")(1)
        Range("AY" & CStr(beginLine + 2)) = Split(arrData(5), "=")(1)
        Dim nengetu As String
        nengetu = Split(arrData(6), "=")(1)
        If nengetu <> "" Then
            Range("AR" & CStr(beginLine + 4)) = Mid(nengetu, 1, 4) & "/" & Mid(nengetu, 5, 2) & "/" & Mid(nengetu, 7, 2)
        End If
        Range("D" & CStr(beginLine + 5)) = Split(arrData(7), "=")(1)
        Range("D" & CStr(beginLine + 6)) = Split(arrData(8), "=")(1)
        Range("D" & CStr(beginLine + 7)) = Split(arrData(9), "=")(1)
        Range("F" & CStr(beginLine + 8)) = Split(arrData(10), "=")(1)
        Range("F" & CStr(beginLine + 9)) = Split(arrData(11), "=")(1)
        Range("D" & CStr(beginLine + 10)) = Split(arrData(12), "=")(1)
     
        '[Table Data Section]
        Dim dataLineCount As Integer
        
        Dim total_uriage As Double
        Dim total_sotozei As Double
        Dim total_uriage_sotozei As Double
        
        total_uriage = 0
        total_sotozei = 0
        total_uriage_sotozei = 0

        '明細
        '明細
        For dataLineCount = 0 To 19
            If UBound(arrData) >= (53 + dataLineCount) Then
                '売上年月日
                Dim hiduke As String
                hiduke = Split(arrData(53 + dataLineCount), "&")(0)
                If hiduke <> "" Then
                    Range("A" & CStr(16 + dataLineCount * 2 + beginLine)) = Mid(hiduke, 1, 4) & "/" & Mid(hiduke, 5, 2) & "/" & Mid(hiduke, 7, 2)
                Else
                    Range("A" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                End If
                '物件番号
                Range("F" & CStr(16 + dataLineCount * 2 + beginLine)) = Split(arrData(53 + dataLineCount), "&")(1)
                '商品名
                Range("K" & CStr(16 + dataLineCount * 2 + beginLine)) = Split(arrData(53 + dataLineCount), "&")(2)
                '物件名
                Range("K" & CStr(16 + dataLineCount * 2 + 1 + beginLine)) = Split(arrData(53 + dataLineCount), "&")(3)
                '数量
                Range("AG" & CStr(16 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(4))
                '単価
                Range("AJ" & CStr(16 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(5))
                '税別金額
                Range("AP" & CStr(16 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(6))
                '税率
                Range("AW" & CStr(16 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(9))
                '消費税
                Range("AY" & CStr(16 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(7))
                '2015/03/05 曹敬仁  EARTH経理改修対応  Start
                '総額
                'Range("BE" & CStr(16 + dataLineCount * 2 + beginLine)) = Trim(Split(arrData(53 + dataLineCount), "&")(8))
                '2015/03/05 曹敬仁  EARTH経理改修対応  End
            Else
                '売上年月日
                Range("A" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '物件番号
                Range("F" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '商品名
                Range("K" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '物件名
                Range("K" & CStr(16 + dataLineCount * 2 + 1 + beginLine)) = ""
                '数量
                Range("AG" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '単価
                Range("AJ" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '税別金額
                Range("AP" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '税率
                Range("AW" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '消費税
                Range("AY" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '2015/03/05 曹敬仁  EARTH経理改修対応  Start
                '総額
                'Range("BE" & CStr(16 + dataLineCount * 2 + beginLine)) = ""
                '2015/03/05 曹敬仁  EARTH経理改修対応  End
            End If
        Next
     
        '2015/03/05 曹敬仁  EARTH経理改修対応  Start
        '小計(0%)_税別金額
        'Range("AP" & CStr(beginLine + 56)) = Trim(Split(arrData(29), "=")(1))
        '小計(0%)_消費税
        'Range("AY" & CStr(beginLine + 56)) = ""
        '小計(0%)_総額
        'Range("BE" & CStr(beginLine + 56)) = Trim(Split(arrData(31), "=")(1))
        '小計(5%)_税別金額
        'Range("AP" & CStr(beginLine + 57)) = Trim(Split(arrData(32), "=")(1))
        '小計(5%)_消費税
        'Range("AY" & CStr(beginLine + 57)) = Trim(Split(arrData(33), "=")(1))
        '小計(5%)_総額
        'Range("BE" & CStr(beginLine + 57)) = Trim(Split(arrData(34), "=")(1))
        '小計(8%)_税別金額
        'Range("AP" & CStr(beginLine + 58)) = Trim(Split(arrData(35), "=")(1))
        '小計(8%)_消費税
        'Range("AY" & CStr(beginLine + 58)) = Trim(Split(arrData(36), "=")(1))
        '小計(8%)_総額
        'Range("BE" & CStr(beginLine + 58)) = Trim(Split(arrData(37), "=")(1))
        '2015/03/05 曹敬仁  EARTH経理改修対応  End
        
        '合計、総合計
        If Split(arrData(50), "=")(1) = "" Then
            '合計行を追加する
            Range("A" & CStr(beginLine + 56) & ":BK" & CStr(beginLine + 58)).Select
            Selection.Copy
            Range("A" & CStr(beginLine + 59) & ":BK" & CStr(beginLine + 61)).Select
            ActiveSheet.Paste
            '合計行をセットする
            Selection.Interior.ColorIndex = 15
            '2015/03/05 曹敬仁  EARTH経理改修対応  Start
            '   合計(0%)
            'Range("K" & CStr(beginLine + 59)) = "　　　　　合計（非課税）"
            'Range("AP" & CStr(beginLine + 59)) = Trim(Split(arrData(38), "=")(1)) '税別金額
            'Range("AY" & CStr(beginLine + 59)) = "" '消費税
            'Range("BE" & CStr(beginLine + 59)) = Trim(Split(arrData(40), "=")(1)) '総額
            '   合計(5%)
            'Range("K" & CStr(beginLine + 60)) = "合計"
            'Range("AP" & CStr(beginLine + 60)) = Trim(Split(arrData(41), "=")(1)) '税別金額
            'Range("AY" & CStr(beginLine + 60)) = Trim(Split(arrData(42), "=")(1)) '消費税
            'Range("BE" & CStr(beginLine + 60)) = Trim(Split(arrData(43), "=")(1)) '総額
            '   合計(8%)
            'Range("K" & CStr(beginLine + 61)) = "合計"
            'Range("AP" & CStr(beginLine + 61)) = Trim(Split(arrData(44), "=")(1)) '税別金額
            'Range("AY" & CStr(beginLine + 61)) = Trim(Split(arrData(45), "=")(1)) '消費税
            'Range("BE" & CStr(beginLine + 61)) = Trim(Split(arrData(46), "=")(1)) '総額
            
            For pageCount = 1 To countZKZN2
                sumAP0Formula = sumAP0Formula & " + '請求書(全件・残高表示無し)２'!AP" & CStr((pageCount - 1) * 64 + 56)
                sumAP5Formula = sumAP5Formula & " + '請求書(全件・残高表示無し)２'!AP" & CStr((pageCount - 1) * 64 + 57)
                sumAP8Formula = sumAP8Formula & " + '請求書(全件・残高表示無し)２'!AP" & CStr((pageCount - 1) * 64 + 58)
                sumAY5Formula = sumAY5Formula & " + '請求書(全件・残高表示無し)２'!AY" & CStr((pageCount - 1) * 64 + 57)
                sumAY8Formula = sumAY8Formula & " + '請求書(全件・残高表示無し)２'!AY" & CStr((pageCount - 1) * 64 + 58)
            Next
            
            '   合計(0%)
            Range("K" & CStr(beginLine + 59)) = "　　　　　合計（非課税）"
            Range("AP" & CStr(beginLine + 59)).Formula = "='請求書(全件・残高表示無し)１'!AP63 " & sumAP0Formula & "" '税別金額
            Range("AY" & CStr(beginLine + 59)) = "" '消費税
            Range("BE" & CStr(beginLine + 59)).Formula = "=AP" & CStr(beginLine + 59) & "+AY" & CStr(beginLine + 59) & "" '総額
            '   合計(5%)
            Range("K" & CStr(beginLine + 60)) = "合計"
            Range("AP" & CStr(beginLine + 60)).Formula = "='請求書(全件・残高表示無し)１'!AP64 " & sumAP5Formula & "" '税別金額
            Range("AY" & CStr(beginLine + 60)).Formula = "='請求書(全件・残高表示無し)１'!AY64 " & sumAY5Formula & "" '消費税
            Range("BE" & CStr(beginLine + 60)).Formula = "=AP" & CStr(beginLine + 60) & "+AY" & CStr(beginLine + 60) & "" '総額
            '   合計(8%)
            Range("K" & CStr(beginLine + 61)) = "合計"
            Range("AP" & CStr(beginLine + 61)).Formula = "='請求書(全件・残高表示無し)１'!AP65 " & sumAP8Formula & "" '税別金額
            Range("AY" & CStr(beginLine + 61)).Formula = "='請求書(全件・残高表示無し)１'!AY65 " & sumAY8Formula & "" '消費税
            Range("BE" & CStr(beginLine + 61)).Formula = "=AP" & CStr(beginLine + 61) & "+AY" & CStr(beginLine + 61) & "" '総額
            '2015/03/05 曹敬仁  EARTH経理改修対応  End
            
            '総合計行を追加する
            Range("A" & CStr(beginLine + 56) & ":BK" & CStr(beginLine + 56)).Select
            Selection.Copy
            Range("A" & CStr(beginLine + 62) & ":BK" & CStr(beginLine + 62)).Select
            ActiveSheet.Paste
            '総合計行をセットする
            Selection.Interior.ColorIndex = 16
            Range("K" & CStr(beginLine + 62)) = "総合計"
            '2015/03/05 曹敬仁  EARTH経理改修対応  Start
            'Range("AP" & CStr(beginLine + 62)) = Trim(Split(arrData(47), "=")(1))
            'Range("AY" & CStr(beginLine + 62)) = Trim(Split(arrData(48), "=")(1))
            'Range("BE" & CStr(beginLine + 62)) = Trim(Split(arrData(49), "=")(1))
            Range("AP" & CStr(beginLine + 62)).Formula = "=SUM(AP" & CStr(beginLine + 59) & ":AP" & CStr(beginLine + 61) & ")"
            Range("AY" & CStr(beginLine + 62)).Formula = "=SUM(AY" & CStr(beginLine + 60) & ":AY" & CStr(beginLine + 61) & ")"
            Range("BE" & CStr(beginLine + 62)).Formula = "=SUM(BE" & CStr(beginLine + 59) & ":BE" & CStr(beginLine + 61) & ")"
            ThisWorkbook.Worksheets("請求書(全件・残高表示無し)１").Range("K13").Formula = "='請求書(全件・残高表示無し)２'!BE" & CStr(beginLine + 62) & ""
            '2015/03/05 曹敬仁  EARTH経理改修対応  End
        Else
            '合計、総合計がない
            Range("A" & CStr(beginLine + 59) & ":BK" & CStr(beginLine + 62)).Clear
        End If
        
        '2015/05/27 曹敬仁  EARTH経理改修対応  Start
        Application.CutCopyMode = False    'コピーモード解除
        '2015/05/27 曹敬仁  EARTH経理改修対応  End
        
        '画面の更新が有効化
        ThisWorkbook.Worksheets("請求書(全件・残高表示無し)２").Application.ScreenUpdating = True
        ThisWorkbook.Worksheets("請求書(全件・残高表示無し)２").Range("A1").Select

End Function
'===========2011/02/18 車龍(大連) 追加 終了↑===============

'店小計1
Private Function SetXK1(ByRef arrData() As String)
        Dim intKaisi As Integer
        countXK1 = countXK1 + 1
        intKaisi = (countXK1 - 1) * 67

        '画面の更新が無効化
        ThisWorkbook.Worksheets("請求書(全件_店小計)１").Application.ScreenUpdating = False
        
        'ThisWorkbook.Worksheets("請求書(全件_店小計)１").Cells(1, 1).Select
'
'        ThisWorkbook.Worksheets("請求書(全件_店小計)１").Cells(1, countXK1) = countXK1
'
'        Dim i As Integer
         ThisWorkbook.Worksheets("請求書(全件_店小計)１").Select
         If countXK1 > 1 Then
        'COPY
        
            Range("A1:BK67").Select
            Selection.Copy
            Range("A" & (countXK1 - 1) * 67 + 1).Select
            ActiveSheet.Paste
            
            ActiveSheet.PageSetup.PrintArea = "$A$1:$BK$" & CStr(countXK1 * 67)
            
            Rows("28:29").Select
            Selection.Copy
            
            Range("A" & (intKaisi + 28) & ":BK" & (intKaisi + 63)).Select
            Selection.PasteSpecial Paste:=xlFormats, Operation:=xlNone, SkipBlanks:= _
            False, Transpose:=False
            Application.CutCopyMode = False
            Selection.ClearContents
            
            Rows("1:26").Select
            Selection.Copy

            Rows((intKaisi + 1) & ":" & (intKaisi + 26)).Select
            Selection.PasteSpecial Paste:=xlFormats, Operation:=xlNone, SkipBlanks:= _
                False, Transpose:=False
            Application.CutCopyMode = False
            
            '合計、総合計を削除する
            Range("A" & (intKaisi + 64) & ":BK" & (intKaisi + 67)).Clear
            
            ThisWorkbook.Worksheets("請求書(全件_店小計)１").HPageBreaks.Add Range("A" & intKaisi + 1)
        End If
        With ThisWorkbook.Worksheets("請求書(全件_店小計)１")
            .Cells(2 + intKaisi, 9) = Split(arrData(4), "=")(1)
            .Cells(2 + intKaisi, 51) = Split(arrData(5), "=")(1)
            .Cells(4 + intKaisi, 44) = Mid(Split(arrData(6), "=")(1), 1, 4) & "/" & Mid(Split(arrData(6), "=")(1), 5, 2) & "/" & Mid(Split(arrData(6), "=")(1), 7, 2)
            .Cells(5 + intKaisi, 4) = Split(arrData(7), "=")(1)
            .Cells(6 + intKaisi, 4) = Split(arrData(8), "=")(1)
            .Cells(7 + intKaisi, 4) = Split(arrData(9), "=")(1)
            .Cells(8 + intKaisi, 6) = Split(arrData(10), "=")(1)
            .Cells(9 + intKaisi, 6) = Split(arrData(11), "=")(1)
            .Cells(10 + intKaisi, 4) = Split(arrData(12), "=")(1)
            .Cells(13 + intKaisi, 11) = Split(arrData(13), "=")(1)
            .Cells(17 + intKaisi, 3) = Split(arrData(14), "=")(1)
            .Cells(18 + intKaisi, 3) = Split(arrData(15), "=")(1)
            .Cells(19 + intKaisi, 3) = Split(arrData(16), "=")(1)
            .Cells(13 + intKaisi, 35) = Split(arrData(17), "=")(1)
            '2013/6/8 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
            '.Cells(14 + intKaisi, 41) = Split(arrData(18), "=")(1)
            '.Cells(15 + intKaisi, 43) = Split(arrData(19), "=")(1)
            .Cells(14 + intKaisi, 35) = Split(arrData(18), "=")(1)
            .Cells(15 + intKaisi, 35) = Split(arrData(19), "=")(1)
            '2013/6/8 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑
    
            .Cells(17 + intKaisi, 43) = Split(arrData(20), "=")(1)
            .Cells(18 + intKaisi, 43) = Split(arrData(21), "=")(1)
            .Cells(19 + intKaisi, 43) = Split(arrData(22), "=")(1)
            
            .Cells(22 + intKaisi, 2) = Split(arrData(23), "=")(1)
            .Cells(22 + intKaisi, 12) = Split(arrData(24), "=")(1)
            .Cells(22 + intKaisi, 22) = Split(arrData(25), "=")(1)
            .Cells(22 + intKaisi, 32) = Split(arrData(26), "=")(1)
            .Cells(22 + intKaisi, 42) = Split(arrData(27), "=")(1)
            '2015/03/05 曹敬仁  EARTH経理改修対応  Start
            '.Cells(22 + intKaisi, 53) = Split(arrData(28), "=")(1)
            '2015/03/05 曹敬仁  EARTH経理改修対応  End
            
            '店名行
            .Cells(27 + intKaisi, 1) = Split(arrData(44), "=")(1)
            Call TenpoStyle(27 + intKaisi)
            
            '明細、小計
            Dim dataLineCount As Integer
            
            For dataLineCount = 0 To 17
                If UBound(arrData) >= (99 + dataLineCount) Then
                    If Replace(Split(arrData(99 + dataLineCount), "&")(2), "　", "") <> "小計" Then
                        '明細
                        
                        '   売上年月日
                        Dim hiduke As String
                        hiduke = Split(arrData(99 + dataLineCount), "&")(0)
                        If hiduke <> "" Then
                            Range("A" & CStr(28 + dataLineCount * 2 + intKaisi)) = Mid(hiduke, 1, 4) & "/" & Mid(hiduke, 5, 2) & "/" & Mid(hiduke, 7, 2)
                        Else
                            Range("A" & CStr(28 + dataLineCount * 2 + intKaisi)) = ""
                        End If
                        '   物件番号
                        Range("A" & CStr(28 + dataLineCount * 2 + 1 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(1)
                        '   商品名
                        Range("H" & CStr(28 + dataLineCount * 2 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(2)
                        '   物件名
                        Range("H" & CStr(28 + dataLineCount * 2 + 1 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(3)
                        '   数量
                        Range("AD" & CStr(28 + dataLineCount * 2 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(4)
                        '   単価
                        Range("AG" & CStr(28 + dataLineCount * 2 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(5)
                        '   税別金額
                        Range("AM" & CStr(28 + dataLineCount * 2 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(6)
                        '   税率
                        Range("AT" & CStr(28 + dataLineCount * 2 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(9)
                        '   消費税
                        Range("AW" & CStr(28 + dataLineCount * 2 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(7)
                        '2015/03/05 曹敬仁  EARTH経理改修対応  Start
                        '   総額
                        'Range("BD" & CStr(28 + dataLineCount * 2 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(8)
                        If syoukeiStart1 = 0 Then
                            syoukeiStart1 = 28 + dataLineCount * 2 + intKaisi
                        End If
                        '2015/03/05 曹敬仁  EARTH経理改修対応  End
                    Else
                        '小計
                        Call GoukeiStyle(28 + dataLineCount * 2 + intKaisi)
                        '   "小計"文字
                        Range("H" & CStr(28 + dataLineCount * 2 + intKaisi)) = Trim(Split(arrData(99 + dataLineCount), "&")(2))
                        '2015/03/05 曹敬仁  EARTH経理改修対応  Start
                        '   税別金額
                        'Range("AM" & CStr(28 + dataLineCount * 2 + intKaisi)) = Trim(Split(arrData(99 + dataLineCount), "&")(6))
                        '   消費税
                        'Range("AW" & CStr(28 + dataLineCount * 2 + intKaisi)) = Trim(Split(arrData(99 + dataLineCount), "&")(7))
                        '   総額
                        'Range("BD" & CStr(28 + dataLineCount * 2 + intKaisi)) = Trim(Split(arrData(99 + dataLineCount), "&")(8))
                        syoukeiEnd1 = 28 + dataLineCount * 2 + intKaisi - 1
                        '   税別金額
                        Range("AM" & CStr(28 + dataLineCount * 2 + intKaisi)).Formula = "=SUM(AM" & CStr(syoukeiStart1) & ":AM" & CStr(syoukeiEnd1) & ")"
                        '   税率
                        Range("AT" & CStr(28 + dataLineCount * 2 + intKaisi)) = "-"
                        '   消費税
                        Range("AW" & CStr(28 + dataLineCount * 2 + intKaisi)).Formula = "=SUM(AW" & CStr(syoukeiStart1) & ":AW" & CStr(syoukeiEnd1) & ")"
                        '   総額
                        Range("BD" & CStr(28 + dataLineCount * 2 + intKaisi)).Formula = "=SUM(BD" & CStr(syoukeiStart1) & ":BD" & CStr(syoukeiEnd1) & ")"
                        syoukeiStart1 = 0
                        syoukeiEnd1 = 0
                        '2015/03/05 曹敬仁  EARTH経理改修対応  End
                         
                        '店名行
                        Call TenpoStyle(28 + dataLineCount * 2 + 1 + intKaisi)
                        Range("A" & CStr(28 + dataLineCount * 2 + 1 + intKaisi)) = Split(arrData(45 + dataLineCount), "=")(1)
                    End If
                Else
                    '空行
                End If
            Next
            
            '合計、総合計
            If Split(arrData(96), "=")(1) = "" Then
                '2015/03/05 曹敬仁  EARTH経理改修対応  Start
                '合計(0%)
                'Call GoukeiStyle(intKaisi + 64, True)
                'Range("H" & CStr(intKaisi + 64)) = "　　　　　合計（非課税）"
                'Range("AM" & CStr(intKaisi + 64)) = FormatNumber(Split(arrData(32), "=")(1), 0) '税別金額
                'Range("AT" & CStr(intKaisi + 64)) = "" '税率
                'Range("AW" & CStr(intKaisi + 64)) = "" '消費税
                'Range("BD" & CStr(intKaisi + 64)) = FormatNumber(Split(arrData(34), "=")(1), 0) '総額
                '合計(5%)
                'Call GoukeiStyle(intKaisi + 65, True)
                'Range("H" & CStr(intKaisi + 65)) = "合計"
                'Range("AM" & CStr(intKaisi + 65)) = FormatNumber(Split(arrData(35), "=")(1), 0) '税別金額
                'Range("AT" & CStr(intKaisi + 65)) = "5%" '税率
                'Range("AW" & CStr(intKaisi + 65)) = FormatNumber(Split(arrData(36), "=")(1), 0) '消費税
                'Range("BD" & CStr(intKaisi + 65)) = FormatNumber(Split(arrData(37), "=")(1), 0) '総額
                '合計(8%)
                'Call GoukeiStyle(intKaisi + 66, True)
                'Range("H" & CStr(intKaisi + 66)) = "合計"
                'Range("AM" & CStr(intKaisi + 66)) = FormatNumber(Split(arrData(38), "=")(1), 0) '税別金額
                'Range("AT" & CStr(intKaisi + 66)) = "8%" '税率
                'Range("AW" & CStr(intKaisi + 66)) = FormatNumber(Split(arrData(39), "=")(1), 0) '消費税
                'Range("BD" & CStr(intKaisi + 66)) = FormatNumber(Split(arrData(40), "=")(1), 0) '総額
                
                '総合計
                'Call GoukeiStyle(intKaisi + 67, True)
                'Range("A" & CStr(intKaisi + 67) & ":BK" & CStr(intKaisi + 67)).Interior.ColorIndex = 16
                'Range("H" & CStr(intKaisi + 67)) = "総合計"
                'Range("AM" & CStr(intKaisi + 67)) = FormatNumber(Split(arrData(41), "=")(1), 0) '税別金額
                'Range("AT" & CStr(intKaisi + 67)) = "" '税率
                'Range("AW" & CStr(intKaisi + 67)) = FormatNumber(Split(arrData(42), "=")(1), 0) '消費税
                'Range("BD" & CStr(intKaisi + 67)) = FormatNumber(Split(arrData(43), "=")(1), 0) '総額
                
                '合計(0%)
                Call GoukeiStyle(intKaisi + 64, True)
                Range("H" & CStr(intKaisi + 64)) = "　　　　　合計（非課税）"
                Range("AM" & CStr(intKaisi + 64)).NumberFormatLocal = "###,###,###,##0"
                Range("AM" & CStr(intKaisi + 64)).Formula = "=SUMIF(AT28:AT63,"""",AM28:AM63)" '税別金額
                Range("AT" & CStr(intKaisi + 64)) = "" '税率
                Range("AW" & CStr(intKaisi + 64)).NumberFormatLocal = "###,###,###,##0"
                Range("AW" & CStr(intKaisi + 64)) = "" '消費税
                Range("BD" & CStr(intKaisi + 64)).NumberFormatLocal = "###,###,###,##0"
                Range("BD" & CStr(intKaisi + 64)).Formula = "=AM64+AW64" '総額
                '合計(5%)
                Call GoukeiStyle(intKaisi + 65, True)
                Range("H" & CStr(intKaisi + 65)) = "合計"
                Range("AM" & CStr(intKaisi + 65)).NumberFormatLocal = "###,###,###,##0"
                Range("AM" & CStr(intKaisi + 65)).Formula = "=SUMIF(AT28:AT63,""5%"",AM28:AM63)" '税別金額
                Range("AT" & CStr(intKaisi + 65)) = "5%" '税率
                Range("AW" & CStr(intKaisi + 65)).NumberFormatLocal = "###,###,###,##0"
                Range("AW" & CStr(intKaisi + 65)).Formula = "=SUMIF(AT28:AT63,""5%"",AW28:AW63)" '消費税
                Range("BD" & CStr(intKaisi + 65)).NumberFormatLocal = "###,###,###,##0"
                Range("BD" & CStr(intKaisi + 65)).Formula = "=AM65+AW65" '総額
                '合計(8%)
                Call GoukeiStyle(intKaisi + 66, True)
                Range("H" & CStr(intKaisi + 66)) = "合計"
                Range("AM" & CStr(intKaisi + 66)).NumberFormatLocal = "###,###,###,##0"
                Range("AM" & CStr(intKaisi + 66)).Formula = "=SUMIF(AT28:AT63,""8%"",AM28:AM63)" '税別金額
                Range("AT" & CStr(intKaisi + 66)) = "8%" '税率
                Range("AW" & CStr(intKaisi + 66)).NumberFormatLocal = "###,###,###,##0"
                Range("AW" & CStr(intKaisi + 66)).Formula = "=SUMIF(AT28:AT63,""8%"",AW28:AW63)" '消費税
                Range("BD" & CStr(intKaisi + 66)).NumberFormatLocal = "###,###,###,##0"
                Range("BD" & CStr(intKaisi + 66)).Formula = "=AM66+AW66" '総額
                
                '総合計
                Call GoukeiStyle(intKaisi + 67, True)
                Range("A" & CStr(intKaisi + 67) & ":BK" & CStr(intKaisi + 67)).Interior.ColorIndex = 16
                Range("H" & CStr(intKaisi + 67)) = "総合計"
                Range("AM" & CStr(intKaisi + 67)).NumberFormatLocal = "###,###,###,##0"
                Range("AM" & CStr(intKaisi + 67)).Formula = "=SUM(AM64:AM66)" '税別金額
                Range("AT" & CStr(intKaisi + 67)) = "" '税率
                Range("AW" & CStr(intKaisi + 67)).NumberFormatLocal = "###,###,###,##0"
                Range("AW" & CStr(intKaisi + 67)).Formula = "=SUM(AW65:AW66)" '消費税
                Range("BD" & CStr(intKaisi + 67)).NumberFormatLocal = "###,###,###,##0"
                Range("BD" & CStr(intKaisi + 67)).Formula = "=SUM(BD64:BD66)" '総額
                Range("K13").Formula = "=BD67"  '御請求金額
                Range("AP22").Formula = "=BD67"  '今回御請求金額
                '2015/03/05 曹敬仁  EARTH経理改修対応  End
            End If
        End With
        
        'For i = 0 To UBound(arrData)
        '    ThisWorkbook.Worksheets("請求書(全件_店小計)１").Cells(100 + i, 1) = arrData(i)
        'Next
        
        '画面の更新が有効化
       ThisWorkbook.Worksheets("請求書(全件_店小計)１").Application.ScreenUpdating = True
       ThisWorkbook.Worksheets("請求書(全件_店小計)１").Range("A1").Select


End Function

'店小計2
Private Function SetXK2(ByRef arrData() As String)
    Dim intKaisi As Integer
            '画面の更新が無効化
        ThisWorkbook.Worksheets("請求書(全件_店小計)２").Application.ScreenUpdating = False
        countXK2 = countXK2 + 1
        intKaisi = (countXK2 - 1) * 68
        ThisWorkbook.Worksheets("請求書(全件_店小計)２").Select
        If countXK2 > 1 Then
        'COPY
            Range("A1:BK68").Select
            Selection.Copy
            Range("A" & (countXK2 - 1) * 68 + 1).Select
            ActiveSheet.Paste
            
            ActiveSheet.PageSetup.PrintArea = "$A$1:$BK$" & CStr(countXK2 * 68)
            
            ThisWorkbook.Worksheets("請求書(全件_店小計)１").Rows("28:29").Copy
            
            Range("A" & (intKaisi + 17) & ":BK" & (intKaisi + 64)).Select
            Selection.PasteSpecial Paste:=xlFormats, Operation:=xlNone, SkipBlanks:= _
            False, Transpose:=False
            Application.CutCopyMode = False
            Selection.ClearContents
            
            Rows("1:16").Select
            Selection.Copy

            Rows((intKaisi + 1) & ":" & (intKaisi + 16)).Select
            Selection.PasteSpecial Paste:=xlFormats, Operation:=xlNone, SkipBlanks:= _
                False, Transpose:=False
            Application.CutCopyMode = False
            
            '合計、総合計を削除する
            Range("A" & (intKaisi + 65) & ":BK" & (intKaisi + 68)).Clear
            
            ThisWorkbook.Worksheets("請求書(全件_店小計)２").HPageBreaks.Add Range("A" & intKaisi + 1)
        End If
        
        With ThisWorkbook.Worksheets("請求書(全件_店小計)２")
            .Cells(2 + intKaisi, 9) = Split(arrData(4), "=")(1)
            .Cells(2 + intKaisi, 51) = Split(arrData(5), "=")(1)
            .Cells(4 + intKaisi, 44) = Mid(Split(arrData(6), "=")(1), 1, 4) & "/" & Mid(Split(arrData(6), "=")(1), 5, 2) & "/" & Mid(Split(arrData(6), "=")(1), 7, 2)
            .Cells(5 + intKaisi, 4) = Split(arrData(7), "=")(1)
            .Cells(6 + intKaisi, 4) = Split(arrData(8), "=")(1)
            .Cells(7 + intKaisi, 4) = Split(arrData(9), "=")(1)
            .Cells(8 + intKaisi, 6) = Split(arrData(10), "=")(1)
            .Cells(9 + intKaisi, 6) = Split(arrData(11), "=")(1)
            .Cells(10 + intKaisi, 4) = Split(arrData(12), "=")(1)
            
            '明細、小計
            Dim dataLineCount As Integer
            
            For dataLineCount = 0 To 23
                If UBound(arrData) >= (99 + dataLineCount) Then
                    If Replace(Split(arrData(99 + dataLineCount), "&")(2), "　", "") <> "小計" Then
                        '明細
                        
                        '   売上年月日
                        Dim hiduke As String
                        hiduke = Split(arrData(99 + dataLineCount), "&")(0)
                        If hiduke <> "" Then
                            Range("A" & CStr(17 + dataLineCount * 2 + intKaisi)) = Mid(hiduke, 1, 4) & "/" & Mid(hiduke, 5, 2) & "/" & Mid(hiduke, 7, 2)
                        Else
                            Range("A" & CStr(17 + dataLineCount * 2 + intKaisi)) = ""
                        End If
                        '   物件番号
                        Range("A" & CStr(17 + dataLineCount * 2 + 1 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(1)
                        '   商品名
                        Range("H" & CStr(17 + dataLineCount * 2 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(2)
                        '   物件名
                        Range("H" & CStr(17 + dataLineCount * 2 + 1 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(3)
                        '   数量
                        Range("AD" & CStr(17 + dataLineCount * 2 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(4)
                        '   単価
                        Range("AG" & CStr(17 + dataLineCount * 2 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(5)
                        '   税別金額
                        Range("AM" & CStr(17 + dataLineCount * 2 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(6)
                        '   税率
                        Range("AT" & CStr(17 + dataLineCount * 2 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(9)
                        '   消費税
                        Range("AW" & CStr(17 + dataLineCount * 2 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(7)
                        '2015/03/05 曹敬仁  EARTH経理改修対応  Start
                        '   総額
                        'Range("BD" & CStr(17 + dataLineCount * 2 + intKaisi)) = Split(arrData(99 + dataLineCount), "&")(8)
                        Range("BD" & CStr(17 + dataLineCount * 2 + intKaisi)).Formula = "=AM" & CStr(17 + dataLineCount * 2 + intKaisi) & "+AW" & CStr(17 + dataLineCount * 2 + intKaisi) & ""
                        If syoukeiStart2 = 0 Then
                            syoukeiStart2 = 17 + dataLineCount * 2 + intKaisi
                        End If
                        '2015/03/05 曹敬仁  EARTH経理改修対応  End
                    Else
                        '小計
                        Call GoukeiStyle(17 + dataLineCount * 2 + intKaisi)
                        '   "小計"文字
                        Range("H" & CStr(17 + dataLineCount * 2 + intKaisi)) = Trim(Split(arrData(99 + dataLineCount), "&")(2))
                        '2015/03/05 曹敬仁  EARTH経理改修対応  Start
                        '   税別金額
                        'Range("AM" & CStr(17 + dataLineCount * 2 + intKaisi)) = Trim(Split(arrData(99 + dataLineCount), "&")(6))
                        '   消費税
                        'Range("AW" & CStr(17 + dataLineCount * 2 + intKaisi)) = Trim(Split(arrData(99 + dataLineCount), "&")(7))
                        '   総額
                        'Range("BD" & CStr(17 + dataLineCount * 2 + intKaisi)) = Trim(Split(arrData(99 + dataLineCount), "&")(8))
                        syoukeiEnd2 = 17 + dataLineCount * 2 + intKaisi - 1
                        If syoukeiStart1 > 0 Then
                            If syoukeiStart2 > 0 Then
                                '   税別金額
                                Range("AM" & CStr(17 + dataLineCount * 2 + intKaisi)).Formula = "=SUM('請求書(全件_店小計)１'!AM" & CStr(syoukeiStart1) & ":AM63) + SUM(AM" & CStr(syoukeiStart2) & ":AM" & CStr(syoukeiEnd2) & ")"
                                '   税率
                                Range("AT" & CStr(17 + dataLineCount * 2 + intKaisi)) = "-"
                                '   消費税
                                Range("AW" & CStr(17 + dataLineCount * 2 + intKaisi)).Formula = "=SUM('請求書(全件_店小計)１'!AW" & CStr(syoukeiStart1) & ":AW63) + SUM(AW" & CStr(syoukeiStart2) & ":AW" & CStr(syoukeiEnd2) & ")"
                                '   総額
                                Range("BD" & CStr(17 + dataLineCount * 2 + intKaisi)).Formula = "=SUM('請求書(全件_店小計)１'!BD" & CStr(syoukeiStart1) & ":BD63) + SUM(BD" & CStr(syoukeiStart2) & ":BD" & CStr(syoukeiEnd2) & ")"
                            Else
                                '   税別金額
                                Range("AM" & CStr(17 + dataLineCount * 2 + intKaisi)).Formula = "=SUM('請求書(全件_店小計)１'!AM" & CStr(syoukeiStart1) & ":AM63)"
                                '   税率
                                Range("AT" & CStr(17 + dataLineCount * 2 + intKaisi)) = "-"
                                '   消費税
                                Range("AW" & CStr(17 + dataLineCount * 2 + intKaisi)).Formula = "=SUM('請求書(全件_店小計)１'!AW" & CStr(syoukeiStart1) & ":AW63)"
                                '   総額
                                Range("BD" & CStr(17 + dataLineCount * 2 + intKaisi)).Formula = "=SUM('請求書(全件_店小計)１'!BD" & CStr(syoukeiStart1) & ":BD63)"
                            End If
                            syoukeiStart1 = 0
                        Else
                            '   税別金額
                            Range("AM" & CStr(17 + dataLineCount * 2 + intKaisi)).Formula = "=SUM(AM" & CStr(syoukeiStart2) & ":AM" & CStr(syoukeiEnd2) & ")"
                            '   税率
                            Range("AT" & CStr(17 + dataLineCount * 2 + intKaisi)) = "-"
                            '   消費税
                            Range("AW" & CStr(17 + dataLineCount * 2 + intKaisi)).Formula = "=SUM(AW" & CStr(syoukeiStart2) & ":AW" & CStr(syoukeiEnd2) & ")"
                            '   総額
                            Range("BD" & CStr(17 + dataLineCount * 2 + intKaisi)).Formula = "=SUM(BD" & CStr(syoukeiStart2) & ":BD" & CStr(syoukeiEnd2) & ")"
                        End If
                        syoukeiStart2 = 0
                        syoukeiEnd2 = 0
                        '2015/03/05 曹敬仁  EARTH経理改修対応  End
                        
                        '店名行
                        Call TenpoStyle(17 + dataLineCount * 2 + 1 + intKaisi)
                        Range("A" & CStr(17 + dataLineCount * 2 + 1 + intKaisi)) = Split(arrData(45 + dataLineCount), "=")(1)
                    End If
                Else
                    '空行
                End If
            Next
            
            '合計、総合計
            If Split(arrData(96), "=")(1) = "" Then
                '2015/03/05 曹敬仁  EARTH経理改修対応  Start
                '合計(0%)
                'Call GoukeiStyle(intKaisi + 65, True)
                'Range("H" & CStr(intKaisi + 65)) = "　　　　　合計（非課税）"
                'Range("AM" & CStr(intKaisi + 65)) = FormatNumber(Split(arrData(32), "=")(1), 0) '税別金額
                'Range("AT" & CStr(intKaisi + 65)) = "" '税率
                'Range("AW" & CStr(intKaisi + 65)) = "" '消費税
                'Range("BD" & CStr(intKaisi + 65)) = FormatNumber(Split(arrData(34), "=")(1), 0) '総額
                '合計(5%)
                'Call GoukeiStyle(intKaisi + 66, True)
                'Range("H" & CStr(intKaisi + 66)) = "合計"
                'Range("AM" & CStr(intKaisi + 66)) = FormatNumber(Split(arrData(35), "=")(1), 0) '税別金額
                'Range("AT" & CStr(intKaisi + 66)) = "5%" '税率
                'Range("AW" & CStr(intKaisi + 66)) = FormatNumber(Split(arrData(36), "=")(1), 0) '消費税
                'Range("BD" & CStr(intKaisi + 66)) = FormatNumber(Split(arrData(37), "=")(1), 0) '総額
                '合計(8%)
                'Call GoukeiStyle(intKaisi + 67, True)
                'Range("H" & CStr(intKaisi + 67)) = "合計"
                'Range("AM" & CStr(intKaisi + 67)) = FormatNumber(Split(arrData(38), "=")(1), 0) '税別金額
                'Range("AT" & CStr(intKaisi + 67)) = "8%" '税率
                'Range("AW" & CStr(intKaisi + 67)) = FormatNumber(Split(arrData(39), "=")(1), 0) '消費税
                'Range("BD" & CStr(intKaisi + 67)) = FormatNumber(Split(arrData(40), "=")(1), 0) '総額
                
                '総合計
                'Call GoukeiStyle(intKaisi + 68, True)
                'Range("A" & CStr(intKaisi + 68) & ":BK" & CStr(intKaisi + 68)).Interior.ColorIndex = 16
                'Range("H" & CStr(intKaisi + 68)) = "総合計"
                'Range("AM" & CStr(intKaisi + 68)) = FormatNumber(Split(arrData(41), "=")(1), 0) '税別金額
                'Range("AT" & CStr(intKaisi + 68)) = "" '税率
                'Range("AW" & CStr(intKaisi + 68)) = FormatNumber(Split(arrData(42), "=")(1), 0) '消費税
                'Range("BD" & CStr(intKaisi + 68)) = FormatNumber(Split(arrData(43), "=")(1), 0) '総額
                 '合計(0%)
                Call GoukeiStyle(intKaisi + 65, True)
                Range("H" & CStr(intKaisi + 65)) = "　　　　　合計（非課税）"
                Range("AM" & CStr(intKaisi + 65)).NumberFormatLocal = "###,###,###,##0"
                Range("AM" & CStr(intKaisi + 65)).Formula = "=SUMIF('請求書(全件_店小計)１'!AT28:AT63,"""",'請求書(全件_店小計)１'!AM28:AM63) + SUMIF(AT17:AT" & CStr(intKaisi + 64) & ","""",AM17:AM" & CStr(intKaisi + 64) & ")" '税別金額
                Range("AT" & CStr(intKaisi + 65)) = "" '税率
                Range("AW" & CStr(intKaisi + 65)).NumberFormatLocal = "###,###,###,##0"
                Range("AW" & CStr(intKaisi + 65)) = "" '消費税
                Range("BD" & CStr(intKaisi + 65)).NumberFormatLocal = "###,###,###,##0"
                Range("BD" & CStr(intKaisi + 65)).Formula = "=AM" & CStr(intKaisi + 65) & "+AW" & CStr(intKaisi + 65) & "" '総額
                '合計(5%)
                Call GoukeiStyle(intKaisi + 66, True)
                Range("H" & CStr(intKaisi + 66)) = "合計"
                Range("AM" & CStr(intKaisi + 66)).NumberFormatLocal = "###,###,###,##0"
                Range("AM" & CStr(intKaisi + 66)).Formula = "=SUMIF('請求書(全件_店小計)１'!AT28:AT63,""5%"",'請求書(全件_店小計)１'!AM28:AM63) + SUMIF(AT17:AT" & CStr(intKaisi + 64) & ",""5%"",AM17:AM" & CStr(intKaisi + 64) & ")" '税別金額
                Range("AT" & CStr(intKaisi + 66)) = "5%" '税率
                Range("AW" & CStr(intKaisi + 66)).NumberFormatLocal = "###,###,###,##0"
                Range("AW" & CStr(intKaisi + 66)).Formula = "=SUMIF('請求書(全件_店小計)１'!AT28:AT63,""5%"",'請求書(全件_店小計)１'!AW28:AW63) + SUMIF(AT17:AT" & CStr(intKaisi + 64) & ",""5%"",AW17:AW" & CStr(intKaisi + 64) & ")" '消費税
                Range("BD" & CStr(intKaisi + 66)).NumberFormatLocal = "###,###,###,##0"
                Range("BD" & CStr(intKaisi + 66)).Formula = "=AM" & CStr(intKaisi + 66) & "+AW" & CStr(intKaisi + 66) & "" '総額
                '合計(8%)
                Call GoukeiStyle(intKaisi + 67, True)
                Range("H" & CStr(intKaisi + 67)) = "合計"
                Range("AM" & CStr(intKaisi + 67)).NumberFormatLocal = "###,###,###,##0"
                Range("AM" & CStr(intKaisi + 67)).Formula = "=SUMIF('請求書(全件_店小計)１'!AT28:AT63,""8%"",'請求書(全件_店小計)１'!AM28:AM63) + SUMIF(AT17:AT" & CStr(intKaisi + 64) & ",""8%"",AM17:AM" & CStr(intKaisi + 64) & ")" '税別金額
                Range("AT" & CStr(intKaisi + 67)) = "8%" '税率
                Range("AW" & CStr(intKaisi + 67)).NumberFormatLocal = "###,###,###,##0"
                Range("AW" & CStr(intKaisi + 67)).Formula = "=SUMIF('請求書(全件_店小計)１'!AT28:AT63,""8%"",'請求書(全件_店小計)１'!AW28:AW63) + SUMIF(AT17:AT" & CStr(intKaisi + 64) & ",""8%"",AW17:AW" & CStr(intKaisi + 64) & ")" '消費税
                Range("BD" & CStr(intKaisi + 67)).NumberFormatLocal = "###,###,###,##0"
                Range("BD" & CStr(intKaisi + 67)).Formula = "=AM" & CStr(intKaisi + 67) & "+AW" & CStr(intKaisi + 67) & "" '総額
                
                '総合計
                Call GoukeiStyle(intKaisi + 68, True)
                Range("A" & CStr(intKaisi + 68) & ":BK" & CStr(intKaisi + 68)).Interior.ColorIndex = 16
                Range("H" & CStr(intKaisi + 68)) = "総合計"
                Range("AM" & CStr(intKaisi + 68)).NumberFormatLocal = "###,###,###,##0"
                Range("AM" & CStr(intKaisi + 68)).Formula = "=SUM(AM" & CStr(intKaisi + 65) & ":AM" & CStr(intKaisi + 67) & ")" '税別金額
                Range("AT" & CStr(intKaisi + 68)) = "" '税率
                Range("AW" & CStr(intKaisi + 68)).NumberFormatLocal = "###,###,###,##0"
                Range("AW" & CStr(intKaisi + 68)).Formula = "=SUM(AW" & CStr(intKaisi + 66) & ":AW" & CStr(intKaisi + 67) & ")" '消費税
                Range("BD" & CStr(intKaisi + 68)).NumberFormatLocal = "###,###,###,##0"
                Range("BD" & CStr(intKaisi + 68)).Formula = "=SUM(BD" & CStr(intKaisi + 65) & ":BD" & CStr(intKaisi + 67) & ")" '総額
                ThisWorkbook.Worksheets("請求書(全件_店小計)１").Range("K13").Formula = "='請求書(全件_店小計)２'!BD" & CStr(intKaisi + 68) & "" '御請求金額
                ThisWorkbook.Worksheets("請求書(全件_店小計)１").Range("AP22").Formula = "='請求書(全件_店小計)２'!BD" & CStr(intKaisi + 68) & ""  '今回御請求金額
                '2015/03/05 曹敬仁  EARTH経理改修対応  End
            End If
        End With

        '画面の更新が有効化
        ThisWorkbook.Worksheets("請求書(全件_店小計)２").Application.ScreenUpdating = True
       ThisWorkbook.Worksheets("請求書(全件_店小計)２").Range("A1").Select


End Function

'物件
Private Function SetBK(ByRef arrData() As String, allPageSuu)
        
        countBK = countBK + 1
        

        '画面の更新が無効化
        ThisWorkbook.Worksheets("請求書(物件別)").Application.ScreenUpdating = False
        
        'ThisWorkbook.Worksheets("請求書(物件別)").Cells(1, 1).Select
    
        'ThisWorkbook.Worksheets("請求書(物件別)").Cells(1, countBK) = countBK
        
        'Dim i As Integer
        
        'For i = 0 To UBound(arrData)
        '    ThisWorkbook.Worksheets("請求書(物件別)").Cells(100 + i, 1) = arrData(i)
        'Next
        
        ThisWorkbook.Worksheets("請求書(物件別)").Select
        
        Dim beginLine As Integer
        beginLine = (countBK - 1) * 47
        
        If countBK = 1 Then
        
        ElseIf countBK = 2 Then
            
            Rows("1:47").Copy
            
            Rows("48:" & (47 * allPageSuu)).Select
            
            ActiveSheet.Paste
            ActiveSheet.PageSetup.PrintArea = "$A$1:$BK$" & CStr(allPageSuu * 47)
             
            'Rows(CStr(beginLine + 1) & ":" & CStr(beginLine + 1)).Insert Shift:=xlDown
            'ActiveSheet.PageSetup.PrintArea = "$A$1:$BK$" & CStr(countBK * 47)
            
            Dim pageCount As Integer
            For pageCount = 1 To allPageSuu - 1
                Set ActiveSheet.HPageBreaks(pageCount).Location = Range("A" & CStr(pageCount * 47 + 1))
            Next
                        
        End If
        
        '[Fixed Data Section]
        Range("AY" & CStr(beginLine + 2)) = Trim(Split(arrData(4), "=")(1))
        Range("I" & CStr(beginLine + 2)) = Trim(Split(arrData(5), "=")(1))
        Range("AR" & CStr(beginLine + 4)) = Trim(Split(arrData(6), "=")(1))
        Range("D" & CStr(beginLine + 5)) = Trim(Split(arrData(7), "=")(1))
        Range("D" & CStr(beginLine + 6)) = Trim(Split(arrData(8), "=")(1))
        Range("D" & CStr(beginLine + 7)) = Trim(Split(arrData(9), "=")(1))
        Range("F" & CStr(beginLine + 8)) = Trim(Split(arrData(10), "=")(1))
        Range("F" & CStr(beginLine + 9)) = Trim(Split(arrData(11), "=")(1))
        Range("D" & CStr(beginLine + 10)) = Trim(Split(arrData(12), "=")(1))
        Range("AG" & CStr(beginLine + 17)) = Trim(Split(arrData(13), "=")(1))
        Range("K" & CStr(beginLine + 13)) = Trim(Split(arrData(14), "=")(1))
        Range("K" & CStr(beginLine + 14)) = Replace(Trim(Split(arrData(15), "=")(1)), "", "・")
        Range("C" & CStr(beginLine + 20)) = Trim(Split(arrData(16), "=")(1))
        Range("C" & CStr(beginLine + 21)) = Trim(Split(arrData(17), "=")(1))
        Range("C" & CStr(beginLine + 22)) = Trim(Split(arrData(18), "=")(1))
        '2013/6/8 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
        'Range("AO" & CStr(beginLine + 18)) = Trim(Split(arrData(19), "=")(1))
        'Range("AQ" & CStr(beginLine + 19)) = Trim(Split(arrData(20), "=")(1))
        Range("AH" & CStr(beginLine + 18)) = Trim(Split(arrData(19), "=")(1))
        Range("AH" & CStr(beginLine + 19)) = Trim(Split(arrData(20), "=")(1))
        '2013/6/8 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑
        Range("AQ" & CStr(beginLine + 20)) = Trim(Split(arrData(21), "=")(1))
        
        '[Table Data Section]
        Dim dataLineCount As Integer
        
        Dim total_uriage As Double
        Dim total_sotozei As Double
        Dim total_uriage_sotozei As Double
        
        total_uriage = 0
        total_sotozei = 0
        total_uriage_sotozei = 0
                
        For dataLineCount = 0 To 16
            If UBound(arrData) >= (35 + dataLineCount) Then
                '商品名
                Range("A" & CStr(25 + dataLineCount + beginLine)) = Trim(Split(arrData(35 + dataLineCount), "&")(0))
                '数量
                Range("V" & CStr(25 + dataLineCount + beginLine)) = Trim(Split(arrData(35 + dataLineCount), "&")(1))
                '単価
                Range("AA" & CStr(25 + dataLineCount + beginLine)) = Trim(Split(arrData(35 + dataLineCount), "&")(2))
                '税別金額
                Range("AI" & CStr(25 + dataLineCount + beginLine)) = Trim(Split(arrData(35 + dataLineCount), "&")(3))
                '税率
                Range("AR" & CStr(25 + dataLineCount + beginLine)) = Trim(Split(arrData(35 + dataLineCount), "&")(6))
                '消費税
                Range("AU" & CStr(25 + dataLineCount + beginLine)) = Trim(Split(arrData(35 + dataLineCount), "&")(4))
                '2015/03/05 曹敬仁  EARTH経理改修対応  Start
                '税込金額
                'Range("BC" & CStr(25 + dataLineCount + beginLine)) = Trim(Split(arrData(35 + dataLineCount), "&")(5))
                '2015/03/05 曹敬仁  EARTH経理改修対応  End
                
                '総合計_税別金額
                If Trim(Split(arrData(35 + dataLineCount), "&")(3)) = "" Then
                    total_uriage = total_uriage + 0
                Else
                    total_uriage = total_uriage + CDbl(Trim(Split(arrData(35 + dataLineCount), "&")(3)))
                End If
                '総合計_消費税
                If Trim(Split(arrData(35 + dataLineCount), "&")(4)) = "" Then
                    total_sotozei = total_sotozei + 0
                Else
                    total_sotozei = total_sotozei + CDbl(Trim(Split(arrData(35 + dataLineCount), "&")(4)))
                End If
                '総合計_税込金額
                If Trim(Split(arrData(35 + dataLineCount), "&")(5)) = "" Then
                    total_uriage_sotozei = total_uriage_sotozei + 0
                Else
                    total_uriage_sotozei = total_uriage_sotozei + CDbl(Trim(Split(arrData(35 + dataLineCount), "&")(5)))
                End If
                
            Else
                '商品名
                Range("A" & CStr(25 + dataLineCount + beginLine)) = ""
                '数量
                Range("V" & CStr(25 + dataLineCount + beginLine)) = ""
                '単価
                Range("AA" & CStr(25 + dataLineCount + beginLine)) = ""
                '税別金額
                Range("AI" & CStr(25 + dataLineCount + beginLine)) = ""
                '税率
                Range("AR" & CStr(25 + dataLineCount + beginLine)) = ""
                '消費税
                Range("AU" & CStr(25 + dataLineCount + beginLine)) = ""
                '2015/05/27 曹敬仁  EARTH経理改修対応  Start
                '税込金額
                'Range("BC" & CStr(25 + dataLineCount + beginLine)) = ""
                '2015/05/27 曹敬仁  EARTH経理改修対応  End
            End If
        Next
        
        '2015/03/05 曹敬仁  EARTH経理改修対応  Start
        '合計(0%)_税別金額
        'Range("AI" & CStr(beginLine + 42)) = Trim(Split(arrData(24), "=")(1))
        '合計(0%)_消費税
        'Range("AU" & CStr(beginLine + 42)) = ""
        '合計(0%)_税込金額
        'Range("BC" & CStr(beginLine + 42)) = Trim(Split(arrData(26), "=")(1))
        
        '合計(5%)_税別金額
        'Range("AI" & CStr(beginLine + 43)) = Trim(Split(arrData(27), "=")(1))
        '合計(5%)_消費税
        'Range("AU" & CStr(beginLine + 43)) = Trim(Split(arrData(28), "=")(1))
        '合計(5%)_税込金額
        'Range("BC" & CStr(beginLine + 43)) = Trim(Split(arrData(29), "=")(1))
        
        '合計(8%)_税別金額
        'Range("AI" & CStr(beginLine + 44)) = Trim(Split(arrData(30), "=")(1))
        '合計(8%)_消費税
        'Range("AU" & CStr(beginLine + 44)) = Trim(Split(arrData(31), "=")(1))
        '合計(8%)_税込金額
        'Range("BC" & CStr(beginLine + 44)) = Trim(Split(arrData(32), "=")(1))
        
        '総合計_税別金額
        'Range("AI" & CStr(45 + beginLine)) = FormatNumber(total_uriage, 0)
        '総合計_消費税
        'Range("AU" & CStr(45 + beginLine)) = FormatNumber(total_sotozei, 0)
        '総合計_税込金額
        'Range("BC" & CStr(45 + beginLine)) = FormatNumber(total_uriage_sotozei, 0)
        
        '御請求金額
        'Range("K" & CStr(16 + beginLine)) = FormatNumber(total_uriage_sotozei, 0)
        '2015/03/05 曹敬仁  EARTH経理改修対応  End
        
        '2015/05/27 曹敬仁  EARTH経理改修対応  Start
        Application.CutCopyMode = False    'コピーモード解除
        '2015/05/27 曹敬仁  EARTH経理改修対応  End
        
        '画面の更新が有効化
        ThisWorkbook.Worksheets("請求書(物件別)").Application.ScreenUpdating = True
        ThisWorkbook.Worksheets("請求書(物件別)").Range("A1").Select


End Function

Sub TenpoStyle(intSetRow As Integer)
    Range("A" & intSetRow & ":BK" & intSetRow).Merge
    Range("A" & intSetRow & ":BK" & intSetRow).Select
    Range("A" & intSetRow & ":BK" & intSetRow).HorizontalAlignment = xlLeft
    Range("A" & intSetRow & ":BK" & intSetRow).EntireRow.AutoFit
    Range("A" & intSetRow & ":BK" & intSetRow).RowHeight = 12.3
    
    Range("AO" & intSetRow & ":BK" & intSetRow).Font.Size = 9
    Range("AO" & intSetRow & ":BK" & intSetRow).ShrinkToFit = True
End Sub
Sub GoukeiStyle(intSetRow As Integer, Optional blnSum As Boolean = False)
    If blnSum Then
        Range("A" & intSetRow & ":BK" & intSetRow).Select
    Else
        Range("A" & intSetRow & ":BK" & intSetRow + 1).Select
            With Selection.Borders(xlInsideHorizontal)
            .LineStyle = xlContinuous
            .Weight = xlThin
            .ColorIndex = xlAutomatic
        End With
    End If

    With Selection
        .VerticalAlignment = xlCenter
        .Orientation = 0
        .AddIndent = False
        .ShrinkToFit = False
        .MergeCells = False
    End With

    With Selection.Borders(xlEdgeLeft)
        .LineStyle = xlContinuous
        .Weight = xlThin
        .ColorIndex = xlAutomatic
    End With
    With Selection.Borders(xlEdgeTop)
        .LineStyle = xlContinuous
        .Weight = xlThin
        .ColorIndex = xlAutomatic
    End With
    With Selection.Borders(xlEdgeBottom)
        .LineStyle = xlContinuous
        .Weight = xlThin
        .ColorIndex = xlAutomatic
    End With
    With Selection.Borders(xlEdgeRight)
        .LineStyle = xlContinuous
        .Weight = xlThin
        .ColorIndex = xlAutomatic
    End With
    With Selection.Borders(xlInsideVertical)
        .LineStyle = xlContinuous
        .Weight = xlThin
        .ColorIndex = xlAutomatic
    End With

    Range("A" & intSetRow & ":G" & intSetRow).Merge
    Range("H" & intSetRow & ":AC" & intSetRow).Merge
    Range("H" & intSetRow & ":AC" & intSetRow).HorizontalAlignment = xlCenter
    Range("AD" & intSetRow & ":AF" & intSetRow).Merge
    Range("AD" & intSetRow & ":AF" & intSetRow).HorizontalAlignment = xlRight
    Range("AG" & intSetRow & ":AL" & intSetRow).Merge
    Range("AG" & intSetRow & ":AL" & intSetRow).HorizontalAlignment = xlRight
    Range("AM" & intSetRow & ":AS" & intSetRow).Merge
    Range("AM" & intSetRow & ":AS" & intSetRow).HorizontalAlignment = xlRight
    Range("AT" & intSetRow & ":AV" & intSetRow).Merge
    Range("AT" & intSetRow & ":AV" & intSetRow).HorizontalAlignment = xlRight
    Range("AW" & intSetRow & ":BC" & intSetRow).Merge
    Range("AW" & intSetRow & ":BC" & intSetRow).HorizontalAlignment = xlRight
    Range("BD" & intSetRow & ":BK" & intSetRow).Merge
    Range("BD" & intSetRow & ":BK" & intSetRow).HorizontalAlignment = xlRight
    Range("A" & intSetRow & ":BK" & intSetRow).Select
    With Selection.Interior
        .ColorIndex = 15
        .Pattern = xlSolid
        .PatternColorIndex = xlAutomatic
    End With
    
    With Selection
        .Font.Name = "ＭＳ ゴシック"
        .Font.Size = 9
        .ShrinkToFit = True
    End With

End Sub





