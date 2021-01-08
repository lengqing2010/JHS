' ��������=====================================================================
'
' �y�t�@�C�����z
'       common.vbs
'
' �y�T�v�z
'       ����WSH�̊֐�
'
' �y�߂�l�z
'
' ��������=====================================================================
Option Explicit

Private Const conST_STATUS = "�y�J�n�z"
Private Const conEN_STATUS = "�y�I���z"
Private Const conAB_STATUS = "�y�ُ�z"

' *****************************************************************************
' �y�����T�v�z
'       ���ʈ������擾����
' *****************************************************************************
Private Sub sbGetCommonParam( ByRef args, ByRef strKey)
    Dim lngLoop

    On Error Resume Next

    ' �����擾
    ' ����Ұٱ��ڽ    = CStr(args(0))
    ' ���o�lҰٱ��ڽ  = CStr(args(1))
    ' ����            = CStr(args(2))
    ' �ð���׸�       = CStr(args(3))
    ' �{��            = CStr(args(4))
    For lngLoop = 0 To Ubound(strKey)
      strKey(lngLoop) = CStr(args(lngLoop))
    Next

    ' �����擾�װ����
    If Err.Number <> 0 Then
      ' �C�x���g���O�o��
      Call LogEventOut(Err.Description & vbTab & "sbGetCommonParam")
      ' *****************************
      ' ����-������ԍX�V(�ُ�)
      ' *****************************
      ' ؿ�����
      Set args = Nothing
      ' WSH�ُ�I��
      Call WScript.Quit(2)
    End If
End Sub

' *****************************************************************************
' �y�����T�v�z
'       �C�x���g���O�o��
' *****************************************************************************
Private Sub LogEventOut(ByVal strMsg)
    Dim objShell

    ' ���ʂ��C�x���g���O�ɏo�͂��܂��B
    Set objShell = CreateObject("WScript.Shell")
    objShell.LogEvent 1, strMsg
    ' �R���|�[�l���g�̉��
    Set objShell = Nothing
End Sub

Private Sub sbSendMail(ByVal strKey)
		On Error Resume Next

		Dim objNewMail		'���[�����M�I�u�W�F�N�g
		Dim strFrom				'���M���A�h���X
		Dim strTo					'���惁�[���A�h���X(��؂蕶���̓Z�~�R����";")
		Dim strSubj				'����
		Dim strBody				'�{��
		Dim strStatus			'�����ɒǉ�����ð�����

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
      ' �C�x���g���O�o��
      Call LogEventOut("�w�肳�ꂽ�ð���׸ނ��s���ł��B[" & strKey(3) & "]")
      ' WSH�ُ�I��
      Call WScript.Quit(2)
		End Select

		strSubj = strStatus & "�yTIOS�z" & strKey(2)
		strbody = strKey(4)

		'============================================================================
		Set objNewMail = CreateObject("CDO.Message")
		If Err.Number <> 0 Then
			' �C�x���g���O�o��
			Call LogEventOut("CreateObject(CDO.Message)�ɂĴװ���������܂����B")
			Set objNewMail = Nothing
			' WSH�ُ�I��
			Call WScript.Quit(2)
		End If

		objNewMail.From = strFrom & "@exc.tostem.co.jp"
		objNewMail.To = strTo
		objNewMail.Subject = strSubj
		objNewMail.TextBody  = strBody

'		objNewMail.MailFormat = 1	'0:MIME�`��, 1:uninterrupted plain text(default)
'		objNewMail.Importance = 2	'0:Low, 1:Normal(default), 2:High

		objNewMail.Send
		If Err.Number <> 0 Then
			' �C�x���g���O�o��
			Call LogEventOut("Ұّ��M�ɂĴװ���������܂����B" & vbCrLf & strSubj)
			Set objNewMail = Nothing
			' WSH�ُ�I��
			Call WScript.Quit(2)
		End If

		Set iConf = Nothing
		Set objNewMail = Nothing
End Sub
