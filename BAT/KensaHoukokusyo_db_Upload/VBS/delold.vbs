' �A�N�Z�X����Ă��Ȃ��t�@�C���^�f�B���N�g�����폜����v���O����
'
' ����
'	delold.vbs <folder> <diff> [<option>]
' ����
'	folder	�t�H���_
'	diff	���݂��獷��(�P�ʂ̏����l�͓���)
'	option	�I�v�V����(�ȗ���)
'		/d	�폜�����s����(�ȗ����́A�폜���Ȃ�)
'		/n	�Ώۃt�@�C�����^�t�H���_��\�����Ȃ�
'		/e	��̃t�H���_���폜����(�ȗ����́A�폜���Ȃ�)
'		/r	�ǂݎ���p�t�@�C���^�t�H���_���폜����
'			(�ȗ����́A�폜���Ȃ�)
'		/s	�T�u�t�H���_����������
'		/i	�G���[�𖳎�����
'		/v:�P��
'			diff�̒P��(�ȗ����́A��)
'			yyyy(�N),q(�l����),m(��),y(�N�ԒʎZ��),d(��)
'			w(�T),ww(�T��),h(��),n(��),s(�b)
'		/t:���t���
'			��r������t�̎��(�ȗ����́A�ŏI�A�N�Z�X���t)
'			c (�쐬��) a (�ŏI�A�N�Z�X��) w (�ŏI�X�V��)
' �d�l
'	folder �ȉ����������čŏI�A�N�Z�X���t�����݂��
'	diff���ȏ�Â��t�@�C�����폜����B
'	��: 
'		c:\temp�t�H���_��7���ȏ�A�N�Z�X����Ă��Ȃ��t�@�C����\������
'		cscript delold.vbs c:\temp 7 /d
'		c:\temp�t�H���_�ȉ���7���ȏ�A�N�Z�X����Ă��Ȃ��t�@�C�����폜����
'		cscript delold.vbs c:\temp 7 /d /e /r /s
'		D:\��1�N�ȏ�A�N�Z�X����Ă��Ȃ��t�@�C�����폜����
'		cscript delold.vbs D:\ 1 /v:yyyy /d /e /r /s
'
Option Explicit

Dim strFolder	' �t�H���_
Dim nDiff		' ����
Dim blnOptDelete	' True:�폜�����s����
Dim blnOptPrint		' True:���O��\������
Dim blnOptDeleteEmptyFolder	'True:��̃t�H���_���폜����
Dim blnOptDeleteReadOnly	'True:�ǎ��p�t�@�C���E�t�H���_���폜����
Dim blnOptSubFolder	' True:�T�u�t�H���_����������
Dim blnOptIgnoreError	' True:�G���[�𖳎�����
Dim strInterval	' �����̒P��
Dim strOptDateType	' ���t���
Dim strPrintBuffer	' �o�͗p�o�b�t�@
Dim objArgs, nArg	' ����
Dim nFolder	' �폜�����t�H���_
Dim nFile	'�폜�����t�@�C��
Dim nFileSize	' �폜�����t�@�C���̍��v�T�C�Y

' �ϐ�������
strFolder = ""
nDiff = ""
blnOptDelete = False
blnOptPrint = True
blnOptDeleteEmptyFolder = False
blnOptDeleteReadOnly = False
blnOptSubFolder = False
blnOptIgnoreError = False
strInterval = "d"
strOptDateType = "a"
nFolder = 0
nFile = 0
nFileSize = 0

' �G���[�����w��
' On Error Resume Next

' ��������
Set objArgs = Wscript.Arguments
For nArg = 0 To objArgs.Count - 1
	Select Case Left(objArgs(nArg),1)
	Case "/"
		Select Case LCase(Left(objArgs(nArg),2))
		Case "/d"
			blnOptDelete = True
		Case "/n"
			blnOptPrint = False
		Case "/e"
			blnOptDeleteEmptyFolder = True
		Case "/r"
			blnOptDeleteReadOnly = True
		Case "/s"
			blnOptSubFolder = True
		Case "/i"
			blnOptIgnoreError = True
		Case "/v"
			strInterval = LCase(Mid(objArgs(nArg), 4))
			Select Case strInterval
			Case "yyyy","q","m","y","d","w","ww","h","n","d","s"
			Case Else
				PrintBuffer "�G���[: " & objArgs(nArg) & "�͒P�ʂƂ��Ďw��ł��܂���"
				UsagePrint
				Quit 1
			End Select
		Case "/t"
			strOptDateType = LCase(Mid(objArgs(nArg), 4))
			Select Case strOptDateType
			Case "c","a","w"
			Case Else
				PrintBuffer "�G���[: " & objArgs(nArg) & "�͓��t��ނƂ��Ďw��ł��܂���"
				UsagePrint
				Quit 1
			End Select
		Case "/?"
			UsagePrint
			Quit 0
		Case Else
			PrintBuffer "�G���[: " & objArgs(nArg) & "�I�v�V�����͂���܂���"
			UsagePrint
			Quit 1
		End Select
	Case Else
		If strFolder = "" Then
			strFolder = objArgs(nArg)
		Else
			nDiff = objArgs(nArg)
		End If
	End Select
Next
If nDiff = "" Then
	PrintBuffer "�G���[:<folder> <diff> �͏ȗ��ł��܂���B"
	UsagePrint
	Quit 1
End If
If IsNumeric(nDiff) Then
	nDiff = CLng(nDiff)
Else
	PrintBuffer "�G���[:<day>�ɂ͐��l���w�肵�Ă�������"
	UsagePrint
	Quit 1
End If	

' ---- �����J�n
Dim objFileSys
Dim objFolder

Err.Clear
Set objFileSys = CreateObject("Scripting.FileSystemObject")
Set objFolder = objFileSys.GetFolder(strFolder)
If Err.Number <> 0 Then
	PrintDirect "�G���[:�p�X���݂���܂���"
	Quit 1
End If
DoFolder objFolder
If Err.Number <> 0 Then
	PrintDirect "�G���["
	Quit 1
End If
If blnOptPrint Then
	PrintBuffer "�t�H���_: " & nFolder
	PrintBuffer "�t�@�C��: " & nFile
	PrintBuffer "�T�C�Y�@: " & FormatNumber(nFileSize, 0)
End If
Quit 0
' ---- �I��

' ---- �t�H���_�P�ʂ̏���
Sub DoFolder(objFolder)
	Dim objFile
	Dim objSubFolder

	' �t�H���_���\��
	If blnOptPrint Then
		' PrintDirect ""
		' PrintDirect "------ " & objFolder.Path
	End If
	' �t�@�C������
	For Each objFile In objFolder.Files
		If DateDiff(strInterval, FileDate(objFile, strOptDateType), Now) >= nDiff Then
			nFile = nFile + 1
			nFileSize = nFileSize + objFile.Size
			' �t�@�C�����\��
			If blnOptPrint Then
				PrintDirect FileDate(objFile, strOptDateType) & vbTab & objFile.Path
			End If
			' �t�@�C���폜
			If blnOptDelete Then
					objFile.Delete blnOptDeleteReadOnly
			End If
		End If
		' �G���[����
		If Not blnOptIgnoreError Then
			If Err.Number <> 0 Then
				Exit Sub
			End If
		End If
	Next
	' �T�u�t�H���_����
	If blnOptSubFolder Then
		For Each objSubFolder In objFolder.SubFolders
			DoFolder objSubFolder
			' �G���[����
			If Not blnOptIgnoreError Then
				If Err.Number <> 0 Then
					Exit Sub
				End If
			End If
		Next
	End If
	' �t�H���_����̏ꍇ�Ƀt�H���_���g���폜
	If Not objFolder.IsRootFolder Then
		If blnOptDeleteEmptyFolder Then
			If blnOptDelete Then
				If objFolder.Files.Count = 0 And objFolder.SubFolders.Count = 0 Then
					objFolder.Delete blnOptDeleteReadOnly
				End If
			End If
			nFolder = nFolder + 1
		End If
	End If
End Sub

Function FileDate(objFile, strDateType)
	Select Case strDateType
	Case "c"
		FileDate = objFile.DateCreated
	Case "a"
		FileDate = objFile.DateLastAccessed
	Case "w"
		FileDate = objFile.DateLastModified
	Case Else
		FileDate = ""
	End Select
End Function

Sub PrintBuffer(strMessage)
	If strPrintBuffer <> "" Then
		strPrintBuffer = strPrintBuffer & vbCrLf
	End If
	strPrintBuffer = strPrintBuffer & strMessage
End Sub

Sub PrintFlush
	If strPrintBuffer <> "" Then
		Wscript.Echo strPrintBuffer
	End If
End Sub

Sub PrintDirect(strMessage)
	Wscript.Echo strMessage
End Sub

Sub UsagePrint
	PrintBuffer " delold.vbs : �A�N�Z�X����Ă��Ȃ��t�@�C�����폜����B"
	PrintBuffer " ����"
	PrintBuffer "	delold.vbs <folder> <diff> [<option>]"
	PrintBuffer " ����"
	PrintBuffer "	folder	�t�H���_"
	PrintBuffer "	diff	���݂��獷��(�P�ʂ̏����l�͓���)"
	PrintBuffer "	option	�I�v�V����(�ȗ���)"
	PrintBuffer "		/d	�폜�����s����(�ȗ����́A�폜���Ȃ�)"
	PrintBuffer "		/n	�Ώۃt�@�C�����^�t�H���_��\�����Ȃ�"
	PrintBuffer "		/e	��̃t�H���_���폜����(�ȗ����́A�폜���Ȃ�)"
	PrintBuffer "		/r	�ǂݎ���p�t�@�C���^�t�H���_���폜����"
	PrintBuffer "			(�ȗ����́A�폜���Ȃ�)"
	PrintBuffer "		/s	�T�u�t�H���_����������"
	PrintBuffer "		/i	�G���[�𖳎�����"
	PrintBuffer "		/v:�P��"
	PrintBuffer "			diff�̒P��(�ȗ����́A��)"
	PrintBuffer "			yyyy(�N),q(�l����),m(��),y(�N�ԒʎZ��),d(��)"
	PrintBuffer "			w(�T),ww(�T��),h(��),n(��),s(�b)"
	PrintBuffer "		/t:���t���"
	PrintBuffer "			��r������t�̎��(�ȗ����́A�ŏI�A�N�Z�X���t)"
	PrintBuffer "			c (�쐬��) a (�ŏI�A�N�Z�X��) w (�ŏI�X�V��)"
	PrintBuffer " ��:"
	PrintBuffer "	cscript.vbs c:\temp 1 /d /s /v:m"
End Sub

Sub Quit(nExitCode)
	PrintFlush
	Wscript.Quit nExitCode
End Sub
