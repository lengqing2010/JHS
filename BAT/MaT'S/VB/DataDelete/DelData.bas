Attribute VB_Name = "DelData"
Option Explicit

Sub Main()

    On Error GoTo ErrorHandler
    Dim arryWork
    
    Dim cnObj As ADODB.Connection
    
    Set cnObj = New ADODB.Connection
    
    Dim strSQL As String

    Dim strErrMsg As String

    Dim strLogFullFileName As String
    
    Dim strPROVIDER As String
    Dim strSERVER As String
    Dim strDATABASE As String
    Dim strUID As String
    Dim strPWD As String

    Dim strTableNameId As String

    Dim lngFileNo As Long
 
    '============================================================================================
    'ｺﾏﾝﾄﾞﾗｲﾝ引数取得
    '============================================================================================
    arryWork = GetCommandLine(6)
    
    If Not IsArray(arryWork) Then
        If InStr(arryWork, "┗●●┛") <> 0 Then
            Err.Raise vbObjectError + 513, "Main", "GetCommandLine/ERROR:ｺﾏﾝﾄﾞﾗｲﾝ引数取得に失敗しました。"
        Else
            Err.Raise vbObjectError + 513, "Main", "Main/ERROR:ｺﾏﾝﾄﾞﾗｲﾝ引数が指定されていません。"
        End If
    End If

    'ﾛｸﾞﾌｧｲﾙ名
    strLogFullFileName = Trim(arryWork(0))
    strPROVIDER = Trim(arryWork(1))
    strSERVER = Trim(arryWork(2))
    strDATABASE = Replace(Replace(Trim(arryWork(3)), "[", ""), "]", "")
    strUID = Trim(arryWork(4))
    strPWD = Trim(arryWork(5))
    strTableNameId = Trim(arryWork(6))
   
    With cnObj
        .ConnectionString = "Provider=" & strPROVIDER & ";" & "Server=" & strSERVER & ";" & "Database=" & strDATABASE & ";" & "uid=" & strUID & ";" & "pwd=" & strPWD
        .CommandTimeout = 600
        .Open
    End With
    
    strSQL = ""
    strSQL = strSQL & "TRUNCATE TABLE "
    strSQL = strSQL & strTableNameId

    cnObj.Execute strSQL
                       
    'cnObj.CommitTrans   'ﾄﾗﾝｻﾞｸｼｮﾝ終了
    
    ' **** ログファイル出力 ****
    lngFileNo = FreeFile
    Open strLogFullFileName For Append As #lngFileNo
    Print #lngFileNo, strTableNameId & "TRUNCATE正常終了"
    Print #lngFileNo, "【return0】"
    Close #lngFileNo
    ' **** ログファイル出力 ****
          
Exit_Sub:
    'On Error Resume Next

    If cnObj.State = adStateOpen Then cnObj.Close
    Set cnObj = Nothing

Exit Sub

ErrorHandler:
    'cnObj.RollbackTrans    'ﾄﾗﾝｻﾞｸｼｮﾝﾛｰﾙﾊﾞｯｸ
    
    ' **** ログファイル出力 ****
    lngFileNo = FreeFile
    Open strLogFullFileName For Append As #lngFileNo
    Print #lngFileNo, strTableNameId & "TRUNCATE失敗"
    Print #lngFileNo, ""
    Close #lngFileNo
    ' **** ログファイル出力 ****

    Dim ErrNo As Long
    Dim ErrSource As String
    Dim ErrDescription As String

    'Resume Nextするためエラー内容退避
    ErrNo = Err.Number
    ErrSource = Err.Source
    ErrDescription = Err.Description

    On Error Resume Next
    '無限ループを避けるため

    If ErrNo = vbObjectError + 513 Then
        'エラーファイル作成
        'ｺﾏﾝﾄﾞﾗｲﾝ引数取得できなかった為、エラーファイルはExeと同ディレクトリに作成される
        
        ' **** ログファイル出力 ****
        lngFileNo = FreeFile
        Open strLogFullFileName For Append As #lngFileNo
        Print #lngFileNo, "Ver." & App.Major & "." & App.Minor & "." & App.Revision
        Print #lngFileNo, "┗●●┛エラー発生！！┗●●┛"
        Print #lngFileNo, Format(Now(), "yyyy/mm/dd hh:mm:ss")
        Print #lngFileNo, ErrSource & "/" & ErrDescription
        Close #lngFileNo
        ' **** ログファイル出力 ****
    Else
        ' **** ログファイル出力 ****
        lngFileNo = FreeFile
        Open strLogFullFileName For Append As #lngFileNo
        Print #lngFileNo, "┗●●┛エラー発生！！┗●●┛"
        Print #lngFileNo, "Ver." & App.Major & "." & App.Minor & "." & App.Revision
        Print #lngFileNo, Format(Now(), "yyyy/mm/dd hh:mm:ss")
        Print #lngFileNo, CStr(ErrNo) & "/" & ErrSource & "/" & ErrDescription
        
        If cnObj.State = adStateOpen Then
            Dim er As Error
            For Each er In cnObj.Errors
                Print #lngFileNo, CStr(er.Number) & "/" & er.Source & "/" & er.Description & "/" & CStr(er.NativeError) & "/" & er.SQLState
            Next
        End If
        
        Close #lngFileNo
        ' **** ログファイル出力 ****

    End If
    
    GoTo Exit_Sub
    
End Sub



