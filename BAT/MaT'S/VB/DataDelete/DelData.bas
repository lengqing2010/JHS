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
    '�����ײ݈����擾
    '============================================================================================
    arryWork = GetCommandLine(6)
    
    If Not IsArray(arryWork) Then
        If InStr(arryWork, "��������") <> 0 Then
            Err.Raise vbObjectError + 513, "Main", "GetCommandLine/ERROR:�����ײ݈����擾�Ɏ��s���܂����B"
        Else
            Err.Raise vbObjectError + 513, "Main", "Main/ERROR:�����ײ݈������w�肳��Ă��܂���B"
        End If
    End If

    '۸�̧�ٖ�
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
                       
    'cnObj.CommitTrans   '��ݻ޸��ݏI��
    
    ' **** ���O�t�@�C���o�� ****
    lngFileNo = FreeFile
    Open strLogFullFileName For Append As #lngFileNo
    Print #lngFileNo, strTableNameId & "TRUNCATE����I��"
    Print #lngFileNo, "�yreturn0�z"
    Close #lngFileNo
    ' **** ���O�t�@�C���o�� ****
          
Exit_Sub:
    'On Error Resume Next

    If cnObj.State = adStateOpen Then cnObj.Close
    Set cnObj = Nothing

Exit Sub

ErrorHandler:
    'cnObj.RollbackTrans    '��ݻ޸���۰��ޯ�
    
    ' **** ���O�t�@�C���o�� ****
    lngFileNo = FreeFile
    Open strLogFullFileName For Append As #lngFileNo
    Print #lngFileNo, strTableNameId & "TRUNCATE���s"
    Print #lngFileNo, ""
    Close #lngFileNo
    ' **** ���O�t�@�C���o�� ****

    Dim ErrNo As Long
    Dim ErrSource As String
    Dim ErrDescription As String

    'Resume Next���邽�߃G���[���e�ޔ�
    ErrNo = Err.Number
    ErrSource = Err.Source
    ErrDescription = Err.Description

    On Error Resume Next
    '�������[�v������邽��

    If ErrNo = vbObjectError + 513 Then
        '�G���[�t�@�C���쐬
        '�����ײ݈����擾�ł��Ȃ������ׁA�G���[�t�@�C����Exe�Ɠ��f�B���N�g���ɍ쐬�����
        
        ' **** ���O�t�@�C���o�� ****
        lngFileNo = FreeFile
        Open strLogFullFileName For Append As #lngFileNo
        Print #lngFileNo, "Ver." & App.Major & "." & App.Minor & "." & App.Revision
        Print #lngFileNo, "���������G���[�����I�I��������"
        Print #lngFileNo, Format(Now(), "yyyy/mm/dd hh:mm:ss")
        Print #lngFileNo, ErrSource & "/" & ErrDescription
        Close #lngFileNo
        ' **** ���O�t�@�C���o�� ****
    Else
        ' **** ���O�t�@�C���o�� ****
        lngFileNo = FreeFile
        Open strLogFullFileName For Append As #lngFileNo
        Print #lngFileNo, "���������G���[�����I�I��������"
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
        ' **** ���O�t�@�C���o�� ****

    End If
    
    GoTo Exit_Sub
    
End Sub



