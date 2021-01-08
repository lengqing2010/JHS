Attribute VB_Name = "jhsOracle"
Option Explicit

Public Function C_Connect() As Boolean
'************************************************************
'* 作 成 日:2013/01/03
'* 作 成 者:楊双
'* 処理概要:ｵﾗｸﾙ ｵﾌﾞｼﾞｪｸﾄ作成処理
'* 処理内容:
'* 変更履歴
'************************************************************
    On Error GoTo ChkErr
    
    '*ｵﾌﾞｼﾞｪｸﾄ作成
    If C_OraCreate = False Then GoTo ChkErr
    '*ﾃﾞｰﾀﾍﾞｰｽﾛｸﾞｲﾝ
    If C_OraLogin = False Then GoTo ChkErr
    
    C_Connect = True
    Exit Function

ChkErr:
    C_Connect = False
    
End Function

Private Function C_OraLogin() As Boolean
'************************************************************
'* 作 成 日:2013/01/03
'* 作 成 者:楊双
'* 処理概要:ﾛｸﾞｲﾝ処理
'* 処理内容:
'* 変更履歴
'************************************************************
    
    On Error GoTo ChkErr
    
    '*ﾃﾞｰﾀﾍﾞｰｽへ接続
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
'* 作 成 日:2013/01/03
'* 作 成 者:楊双
'* 処理概要:ｵﾗｸﾙ ｵﾌﾞｼﾞｪｸﾄ作成処理
'* 処理内容:
'* 変更履歴
'************************************************************
    
    On Error GoTo ChkErr
    '*ｵﾌﾞｼﾞｪｸﾄ作成
    Set gOraSession = CreateObject("OracleInProcServer.XOraSession")
    C_OraCreate = True
    Exit Function
    
ChkErr:
    C_OraCreate = False
    
End Function

Public Function C_DisConnect() As Boolean
'************************************************************
'* 作 成 日:2013/01/03
'* 作 成 者:楊双
'* 処理概要:ﾛｸﾞｲﾝ開放処理(切断)
'* 処理内容:
'* 変更履歴
'************************************************************
    
    On Error GoTo Err_Exit
    '*ｵﾌﾞｼﾞｪｸﾄ開放
    Set gOraDatabase = Nothing      '*ﾃﾞｰﾀﾍﾞｰｽ接続開放
    Set gOraSession = Nothing       '*ﾃﾞｰﾀﾍﾞｰｽ接続開放
                                   
    C_DisConnect = True
    Exit Function
Err_Exit:
    C_DisConnect = False
    
End Function







