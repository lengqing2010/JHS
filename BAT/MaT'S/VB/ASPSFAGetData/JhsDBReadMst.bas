Attribute VB_Name = "JhsDBReadMst"
Option Explicit

Public Function C_GetMstData(sTableName As String, strLogFileName As String, strDataFilePath As String) As Boolean
'************************************************************
'* �� �� ��:2013/01/03
'* �� �� ��:�k�o
'* �����T�v:�w�肳�ꂽ�e�[�u��SELECT
'* �������e:�w�肳�ꂽ�e�[�u���ɂ��f�[�^�t�@�C�����쐬����Ă���
'* �ύX����
'************************************************************
    
    Dim i        As Integer  'FILED ����
    Dim sSQLText As String   'SQL
    Dim Dynaset  As Object   '��޼ު��
    
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
                                   
    ' **** ���O�t�@�C���o�� ****
    lngFileNo = FreeFile
    Open strLogFileName For Append As #lngFileNo
    Print #lngFileNo, Format(Now(), "yyyy/mm/dd hh:mm:ss")
    Print #lngFileNo, "*** " & sTableName & "��DAT�t�@�C���쐬 �J�n ***"
    Print #lngFileNo, ""
    Close #lngFileNo
    ' **** ���O�t�@�C���o�� ****
    
    '.dat�t�@�C��
    lngTxtNo = FreeFile
    Open strDataFile For Output Lock Read Write As #lngTxtNo
    
    lngCount = 0

    If Dynaset.EOF = True Then
        '�����Z�b�g���Ȃ�
    Else
        Dynaset.MoveFirst
        Do While Not Dynaset.EOF
                  
            strTxtData = ""
            For i = 0 To Dynaset.Fields.Count - 1
                If IsNull(Trim(Dynaset.Fields(i))) Then
                    'null�̏ꍇ
                    strTxtData = strTxtData & Trim(Dynaset.Fields(i))
                Else
                    '���s�͔����ɂ���
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
 
    ' **** ���O�t�@�C���o�� ****
    lngFileNo = FreeFile
    Open strLogFileName For Append As #lngFileNo
    Print #lngFileNo, Format(Now(), "yyyy/mm/dd hh:mm:ss")
    Print #lngFileNo, "*** " & sTableName & "��DAT�t�@�C���쐬 �I�� ***"
    Print #lngFileNo, lngCount & "�s�R�s�[����܂����"
    Print #lngFileNo, ""
    Close #lngFileNo
    ' **** ���O�t�@�C���o�� ****
    
Exit_GetMstData:

    C_GetMstData = True
    Set Dynaset = Nothing
    Exit Function

GetMstData_Err:

    ' **** ���O�t�@�C���o�� ****
    lngFileNo = FreeFile
    Open strLogFileName For Append As #lngFileNo
    Print #lngFileNo, Format(Now(), "yyyy/mm/dd hh:mm:ss")
    Print #lngFileNo, "���Ѵװ���������܂����B" & vbCrLf & "�װ�ԍ�" & Err & ":" & Error(Err)
    Print #lngFileNo, ""
    Close #lngFileNo
    ' **** ���O�t�@�C���o�� ****

    C_GetMstData = False
    Set Dynaset = Nothing
    
End Function

