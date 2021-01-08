Attribute VB_Name = "jhsOracle"
Option Explicit

Public Function C_Connect() As Boolean
'************************************************************
'* �� �� ��:2013/01/03
'* �� �� ��:�k�o
'* �����T�v:�׸� ��޼ު�č쐬����
'* �������e:
'* �ύX����
'************************************************************
    On Error GoTo ChkErr
    
    '*��޼ު�č쐬
    If C_OraCreate = False Then GoTo ChkErr
    '*�ް��ް�۸޲�
    If C_OraLogin = False Then GoTo ChkErr
    
    C_Connect = True
    Exit Function

ChkErr:
    C_Connect = False
    
End Function

Private Function C_OraLogin() As Boolean
'************************************************************
'* �� �� ��:2013/01/03
'* �� �� ��:�k�o
'* �����T�v:۸޲ݏ���
'* �������e:
'* �ύX����
'************************************************************
    
    On Error GoTo ChkErr
    
    '*�ް��ް��֐ڑ�
    gDatabaseName = gOraHostName                  '*WGS_WAA???_ORCL
    gConnect = gOraUserID & "/" & gOraPassID      '*USER ID/PASS
    '*
    Set gOraDatabase = gOraSession.OpenDatabase(gDatabaseName, _
                                               gConnect, _
                                               0&)
    C_OraLogin = True
    Exit Function

ChkErr:
    C_OraLogin = False
    
End Function

Private Function C_OraCreate() As Boolean
'************************************************************
'* �� �� ��:2013/01/03
'* �� �� ��:�k�o
'* �����T�v:�׸� ��޼ު�č쐬����
'* �������e:
'* �ύX����
'************************************************************
    
    On Error GoTo ChkErr
    '*��޼ު�č쐬
    Set gOraSession = CreateObject("OracleInProcServer.XOraSession")
    C_OraCreate = True
    Exit Function
    
ChkErr:
    C_OraCreate = False
    
End Function

Public Function C_DisConnect() As Boolean
'************************************************************
'* �� �� ��:2013/01/03
'* �� �� ��:�k�o
'* �����T�v:۸޲݊J������(�ؒf)
'* �������e:
'* �ύX����
'************************************************************
    
    On Error GoTo Err_Exit
    '*��޼ު�ĊJ��
    Set gOraDatabase = Nothing      '*�ް��ް��ڑ��J��
    Set gOraSession = Nothing       '*�ް��ް��ڑ��J��
                                   
    C_DisConnect = True
    Exit Function
Err_Exit:
    C_DisConnect = False
    
End Function







