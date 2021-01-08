Attribute VB_Name = "GetData"
Option Explicit

'************************************************************
'* 作 成 日:2013/01/03
'* 作 成 者:楊双
'* 処理概要:JHS　報連相ｼｽﾃﾑ　のﾃﾞｰﾀ　を　ｺﾋﾟ-出来る様にする
'* 処理内容:
'* 変更履歴
'************************************************************
Sub Main()
              
    Dim lngFileNo As Long
    Dim strLogFileName As String    'ログファイルパス
    Dim strTableName As String      'テーブル名称
    Dim strDataFilePath As String   'データファイルパス

    Dim arryWork

    On Error GoTo ChkErr
                                               
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
                                                
    strLogFileName = Trim(arryWork(0))
    strTableName = Trim(arryWork(1))
    strDataFilePath = Trim(arryWork(2))
    'DB情報
    gOraHostName = Trim(arryWork(3))
    gOraUserID = Trim(arryWork(4))
    gOraPassID = Trim(arryWork(5))
    gOwner = Trim(arryWork(6))
                                            
    '*ﾃﾞｰﾀﾍﾞｰｽに接続する
    If C_Connect = False Then GoTo Connect_Err
    
    '指定されたテーブルによりデータファイルが作成されている
    If C_GetMstData(strTableName, strLogFileName, strDataFilePath) = False Then GoTo Exit_Sub

    '*正常ﾒｯｾｰｼﾞ
    ' **** ログファイル出力 ****
    lngFileNo = FreeFile
    Open strLogFileName For Append As #lngFileNo
    Print #lngFileNo, Format(Now(), "yyyy/mm/dd hh:mm:ss")
    Print #lngFileNo, strTableName & " データのエクスポート処理が完了しました。"
    Print #lngFileNo, "success,perfect,excellent"
    Print #lngFileNo, ""
    Close #lngFileNo
    ' **** ログファイル出力 ****
            
    '*ﾃﾞｰﾀﾍﾞｰｽ切断
    Call C_DisConnect
    Exit Sub
    
Exit_Sub:

    '*ﾃﾞｰﾀﾍﾞｰｽ切断
    Call C_DisConnect
    Exit Sub
    
ChkErr:
    ' **** ログファイル出力 ****
    lngFileNo = FreeFile
    Open strLogFileName For Append As #lngFileNo
    Print #lngFileNo, Format(Now(), "yyyy/mm/dd hh:mm:ss")
    Print #lngFileNo, "ｼｽﾃﾑｴﾗｰが発生しました。" & vbCrLf & "ｴﾗｰ番号" & Err & ":" & Error(Err)
    Print #lngFileNo, ""
    Close #lngFileNo
    ' **** ログファイル出力 ****
    Call C_DisConnect  '*切断
    Exit Sub
        
Connect_Err:
    ' **** ログファイル出力 ****
    lngFileNo = FreeFile
    Open strLogFileName For Append As #lngFileNo
    Print #lngFileNo, Format(Now(), "yyyy/mm/dd hh:mm:ss")
    Print #lngFileNo, "ﾃﾞｰﾀﾍﾞｰｽに接続できません。"
    Print #lngFileNo, ""
    Close #lngFileNo
    ' **** ログファイル出力 ****
    Call C_DisConnect  '*切断
    Exit Sub
    
End Sub

