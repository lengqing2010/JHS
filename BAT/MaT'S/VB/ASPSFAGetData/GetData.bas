Attribute VB_Name = "GetData"
Option Explicit

'************************************************************
'* �� �� ��:2013/01/03
'* �� �� ��:�k�o
'* �����T�v:JHS�@��A�����с@���ް��@���@���-�o����l�ɂ���
'* �������e:
'* �ύX����
'************************************************************
Sub Main()
              
    Dim lngFileNo As Long
    Dim strLogFileName As String    '���O�t�@�C���p�X
    Dim strTableName As String      '�e�[�u������
    Dim strDataFilePath As String   '�f�[�^�t�@�C���p�X

    Dim arryWork

    On Error GoTo ChkErr
                                               
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
                                                
    strLogFileName = Trim(arryWork(0))
    strTableName = Trim(arryWork(1))
    strDataFilePath = Trim(arryWork(2))
    'DB���
    gOraHostName = Trim(arryWork(3))
    gOraUserID = Trim(arryWork(4))
    gOraPassID = Trim(arryWork(5))
    gOwner = Trim(arryWork(6))
                                            
    '*�ް��ް��ɐڑ�����
    If C_Connect = False Then GoTo Connect_Err
    
    '�w�肳�ꂽ�e�[�u���ɂ��f�[�^�t�@�C�����쐬����Ă���
    If C_GetMstData(strTableName, strLogFileName, strDataFilePath) = False Then GoTo Exit_Sub

    '*����ү����
    ' **** ���O�t�@�C���o�� ****
    lngFileNo = FreeFile
    Open strLogFileName For Append As #lngFileNo
    Print #lngFileNo, Format(Now(), "yyyy/mm/dd hh:mm:ss")
    Print #lngFileNo, strTableName & " �f�[�^�̃G�N�X�|�[�g�������������܂����B"
    Print #lngFileNo, "success,perfect,excellent"
    Print #lngFileNo, ""
    Close #lngFileNo
    ' **** ���O�t�@�C���o�� ****
            
    '*�ް��ް��ؒf
    Call C_DisConnect
    Exit Sub
    
Exit_Sub:

    '*�ް��ް��ؒf
    Call C_DisConnect
    Exit Sub
    
ChkErr:
    ' **** ���O�t�@�C���o�� ****
    lngFileNo = FreeFile
    Open strLogFileName For Append As #lngFileNo
    Print #lngFileNo, Format(Now(), "yyyy/mm/dd hh:mm:ss")
    Print #lngFileNo, "���Ѵװ���������܂����B" & vbCrLf & "�װ�ԍ�" & Err & ":" & Error(Err)
    Print #lngFileNo, ""
    Close #lngFileNo
    ' **** ���O�t�@�C���o�� ****
    Call C_DisConnect  '*�ؒf
    Exit Sub
        
Connect_Err:
    ' **** ���O�t�@�C���o�� ****
    lngFileNo = FreeFile
    Open strLogFileName For Append As #lngFileNo
    Print #lngFileNo, Format(Now(), "yyyy/mm/dd hh:mm:ss")
    Print #lngFileNo, "�ް��ް��ɐڑ��ł��܂���B"
    Print #lngFileNo, ""
    Close #lngFileNo
    ' **** ���O�t�@�C���o�� ****
    Call C_DisConnect  '*�ؒf
    Exit Sub
    
End Sub

