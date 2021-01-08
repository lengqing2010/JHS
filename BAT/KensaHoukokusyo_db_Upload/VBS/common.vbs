' ┗◎◎┛=====================================================================
'
' 【ファイル名】
'       common.vbs
'
' 【概要】
'       共通WSHの関数
'
' 【戻り値】
'
' ┗◎◎┛=====================================================================
Option Explicit

Private Const conST_STATUS = "【開始】"
Private Const conEN_STATUS = "【終了】"
Private Const conAB_STATUS = "【異常】"

' *****************************************************************************
' 【処理概要】
'       共通引数を取得する
' *****************************************************************************
Private Sub sbGetCommonParam( ByRef args, ByRef strKey)
    Dim lngLoop

    On Error Resume Next

    ' 引数取得
    ' 宛先ﾒｰﾙｱﾄﾞﾚｽ    = CStr(args(0))
    ' 差出人ﾒｰﾙｱﾄﾞﾚｽ  = CStr(args(1))
    ' 件名            = CStr(args(2))
    ' ｽﾃｰﾀｽﾌﾗｸﾞ       = CStr(args(3))
    ' 本文            = CStr(args(4))
    For lngLoop = 0 To Ubound(strKey)
      strKey(lngLoop) = CStr(args(lngLoop))
    Next

    ' 引数取得ｴﾗｰ判定
    If Err.Number <> 0 Then
      ' イベントログ出力
      Call LogEventOut(Err.Description & vbTab & "sbGetCommonParam")
      ' *****************************
      ' 共通-処理状態更新(異常)
      ' *****************************
      ' ﾘｿｰｽ解放
      Set args = Nothing
      ' WSH異常終了
      Call WScript.Quit(2)
    End If
End Sub

' *****************************************************************************
' 【処理概要】
'       イベントログ出力
' *****************************************************************************
Private Sub LogEventOut(ByVal strMsg)
    Dim objShell

    ' 結果をイベントログに出力します。
    Set objShell = CreateObject("WScript.Shell")
    objShell.LogEvent 1, strMsg
    ' コンポーネントの解放
    Set objShell = Nothing
End Sub

Private Sub sbSendMail(ByVal strKey)
		On Error Resume Next

		Dim objNewMail		'メール送信オブジェクト
		Dim strFrom				'送信元アドレス
		Dim strTo					'宛先メールアドレス(区切り文字はセミコロン";")
		Dim strSubj				'件名
		Dim strBody				'本文
		Dim strStatus			'件名に追加するｽﾃｰﾀｽ情報

		strTo 	= strKey(0)
		strFrom = strKey(1)

		Select Case strKey(3)
		Case "0"
			strStatus = conST_STATUS
		Case "1"
			strStatus = conEN_STATUS
		Case "2"
			strStatus = conAB_STATUS
		Case Else
      ' イベントログ出力
      Call LogEventOut("指定されたｽﾃｰﾀｽﾌﾗｸﾞが不正です。[" & strKey(3) & "]")
      ' WSH異常終了
      Call WScript.Quit(2)
		End Select

		strSubj = strStatus & "【TIOS】" & strKey(2)
		strbody = strKey(4)

		'============================================================================
		Set objNewMail = CreateObject("CDO.Message")
		If Err.Number <> 0 Then
			' イベントログ出力
			Call LogEventOut("CreateObject(CDO.Message)にてｴﾗｰが発生しました。")
			Set objNewMail = Nothing
			' WSH異常終了
			Call WScript.Quit(2)
		End If

		objNewMail.From = strFrom & "@exc.tostem.co.jp"
		objNewMail.To = strTo
		objNewMail.Subject = strSubj
		objNewMail.TextBody  = strBody

'		objNewMail.MailFormat = 1	'0:MIME形式, 1:uninterrupted plain text(default)
'		objNewMail.Importance = 2	'0:Low, 1:Normal(default), 2:High

		objNewMail.Send
		If Err.Number <> 0 Then
			' イベントログ出力
			Call LogEventOut("ﾒｰﾙ送信にてｴﾗｰが発生しました。" & vbCrLf & strSubj)
			Set objNewMail = Nothing
			' WSH異常終了
			Call WScript.Quit(2)
		End If

		Set iConf = Nothing
		Set objNewMail = Nothing
End Sub
