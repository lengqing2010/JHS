Attribute VB_Name = "JhsDBReadMst"
Option Explicit

Public Function C_GetMstData(sTableName As String, strLogFileName As String, strDataFilePath As String) As Boolean
'************************************************************
'* 作 成 日:2013/01/03
'* 作 成 者:楊双
'* 処理概要:指定されたテーブルSELECT
'* 処理内容:指定されたテーブルによりデータファイルが作成されている
'* 変更履歴
'************************************************************
    
    Dim i        As Integer  'FILED ｶｳﾝﾀ
    Dim sSQLText As String   'SQL
    Dim Dynaset  As Object   'ｵﾌﾞｼﾞｪｸﾄ
    
    Dim lngFileNo As Long
    Dim lngTxtNo As Long
    Dim strTxtData As String
    Dim lngCount As Long
    Dim strDataFile As String
    
    On Error GoTo GetMstData_Err
            
    sSQLText = "SELECT "
    sSQLText = sSQLText & " * "
    sSQLText = sSQLText & "FROM " & gOwner & "." & sTableName

    Set Dynaset = gOraDatabase.dbcreatedynaset(sSQLText, 0&)
        
    'If Dynaset.EOF = True Then GoTo Exit_GetMstData
            
    strDataFile = strDataFilePath & sTableName & ".dat"
                                   
    ' **** ログファイル出力 ****
    lngFileNo = FreeFile
    Open strLogFileName For Append As #lngFileNo
    Print #lngFileNo, Format(Now(), "yyyy/mm/dd hh:mm:ss")
    Print #lngFileNo, "*** " & sTableName & "のDATファイル作成 開始 ***"
    Print #lngFileNo, ""
    Close #lngFileNo
    ' **** ログファイル出力 ****
    
    '.datファイル
    lngTxtNo = FreeFile
    Open strDataFile For Output Lock Read Write As #lngTxtNo
    
    lngCount = 0

    If Dynaset.EOF = True Then
        '何をセットしない
    Else
        Dynaset.MoveFirst
        Do While Not Dynaset.EOF
                  
            strTxtData = ""
            For i = 0 To Dynaset.Fields.Count - 1
                If IsNull(Trim(Dynaset.Fields(i))) Then
                    'nullの場合
                    strTxtData = strTxtData & Trim(Dynaset.Fields(i))
                Else
                    '改行は抜きにして
                    strTxtData = strTxtData & Replace(Replace(Trim(Dynaset.Fields(i)), Chr(13) + Chr(10), ""), vbTab, "")
                End If
                If i < Dynaset.Fields.Count - 1 Then
                    strTxtData = strTxtData & vbTab
                End If
            Next i
       
            Print #lngTxtNo, strTxtData
    
            lngCount = lngCount + 1
    
            Dynaset.MoveNext
                        
        Loop
        Close #lngTxtNo
    
        Dynaset.Close
        
    End If
 
    ' **** ログファイル出力 ****
    lngFileNo = FreeFile
    Open strLogFileName For Append As #lngFileNo
    Print #lngFileNo, Format(Now(), "yyyy/mm/dd hh:mm:ss")
    Print #lngFileNo, "*** " & sTableName & "のDATファイル作成 終了 ***"
    Print #lngFileNo, lngCount & "行コピーされました｡"
    Print #lngFileNo, ""
    Close #lngFileNo
    ' **** ログファイル出力 ****
    
Exit_GetMstData:

    C_GetMstData = True
    Set Dynaset = Nothing
    Exit Function

GetMstData_Err:

    ' **** ログファイル出力 ****
    lngFileNo = FreeFile
    Open strLogFileName For Append As #lngFileNo
    Print #lngFileNo, Format(Now(), "yyyy/mm/dd hh:mm:ss")
    Print #lngFileNo, "ｼｽﾃﾑｴﾗｰが発生しました。" & vbCrLf & "ｴﾗｰ番号" & Err & ":" & Error(Err)
    Print #lngFileNo, ""
    Close #lngFileNo
    ' **** ログファイル出力 ****

    C_GetMstData = False
    Set Dynaset = Nothing
    
End Function

