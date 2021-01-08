' アクセスされていないファイル／ディレクトリを削除するプログラム
'
' 書式
'	delold.vbs <folder> <diff> [<option>]
' 引数
'	folder	フォルダ
'	diff	現在から差分(単位の初期値は日数)
'	option	オプション(省略可)
'		/d	削除を実行する(省略時は、削除しない)
'		/n	対象ファイル名／フォルダを表示しない
'		/e	空のフォルダを削除する(省略時は、削除しない)
'		/r	読み取り専用ファイル／フォルダも削除する
'			(省略時は、削除しない)
'		/s	サブフォルダを処理する
'		/i	エラーを無視する
'		/v:単位
'			diffの単位(省略時は、日)
'			yyyy(年),q(四半期),m(月),y(年間通算日),d(日)
'			w(週),ww(週日),h(時),n(分),s(秒)
'		/t:日付種類
'			比較する日付の種類(省略時は、最終アクセス日付)
'			c (作成日) a (最終アクセス日) w (最終更新日)
' 仕様
'	folder 以下を検索して最終アクセス日付が現在より
'	diff日以上古いファイルを削除する。
'	例: 
'		c:\tempフォルダの7日以上アクセスされていないファイルを表示する
'		cscript delold.vbs c:\temp 7 /d
'		c:\tempフォルダ以下の7日以上アクセスされていないファイルを削除する
'		cscript delold.vbs c:\temp 7 /d /e /r /s
'		D:\の1年以上アクセスされていないファイルを削除する
'		cscript delold.vbs D:\ 1 /v:yyyy /d /e /r /s
'
Option Explicit

Dim strFolder	' フォルダ
Dim nDiff		' 差分
Dim blnOptDelete	' True:削除を実行する
Dim blnOptPrint		' True:ログを表示する
Dim blnOptDeleteEmptyFolder	'True:空のフォルダを削除する
Dim blnOptDeleteReadOnly	'True:読取専用ファイル・フォルダも削除する
Dim blnOptSubFolder	' True:サブフォルダを処理する
Dim blnOptIgnoreError	' True:エラーを無視する
Dim strInterval	' 差分の単位
Dim strOptDateType	' 日付種類
Dim strPrintBuffer	' 出力用バッファ
Dim objArgs, nArg	' 引数
Dim nFolder	' 削除したフォルダ
Dim nFile	'削除したファイル
Dim nFileSize	' 削除したファイルの合計サイズ

' 変数初期化
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

' エラー処理指定
' On Error Resume Next

' 引数処理
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
				PrintBuffer "エラー: " & objArgs(nArg) & "は単位として指定できません"
				UsagePrint
				Quit 1
			End Select
		Case "/t"
			strOptDateType = LCase(Mid(objArgs(nArg), 4))
			Select Case strOptDateType
			Case "c","a","w"
			Case Else
				PrintBuffer "エラー: " & objArgs(nArg) & "は日付種類として指定できません"
				UsagePrint
				Quit 1
			End Select
		Case "/?"
			UsagePrint
			Quit 0
		Case Else
			PrintBuffer "エラー: " & objArgs(nArg) & "オプションはありません"
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
	PrintBuffer "エラー:<folder> <diff> は省略できません。"
	UsagePrint
	Quit 1
End If
If IsNumeric(nDiff) Then
	nDiff = CLng(nDiff)
Else
	PrintBuffer "エラー:<day>には数値を指定してください"
	UsagePrint
	Quit 1
End If	

' ---- 処理開始
Dim objFileSys
Dim objFolder

Err.Clear
Set objFileSys = CreateObject("Scripting.FileSystemObject")
Set objFolder = objFileSys.GetFolder(strFolder)
If Err.Number <> 0 Then
	PrintDirect "エラー:パスがみつかりません"
	Quit 1
End If
DoFolder objFolder
If Err.Number <> 0 Then
	PrintDirect "エラー"
	Quit 1
End If
If blnOptPrint Then
	PrintBuffer "フォルダ: " & nFolder
	PrintBuffer "ファイル: " & nFile
	PrintBuffer "サイズ　: " & FormatNumber(nFileSize, 0)
End If
Quit 0
' ---- 終了

' ---- フォルダ単位の処理
Sub DoFolder(objFolder)
	Dim objFile
	Dim objSubFolder

	' フォルダ名表示
	If blnOptPrint Then
		' PrintDirect ""
		' PrintDirect "------ " & objFolder.Path
	End If
	' ファイル処理
	For Each objFile In objFolder.Files
		If DateDiff(strInterval, FileDate(objFile, strOptDateType), Now) >= nDiff Then
			nFile = nFile + 1
			nFileSize = nFileSize + objFile.Size
			' ファイル名表示
			If blnOptPrint Then
				PrintDirect FileDate(objFile, strOptDateType) & vbTab & objFile.Path
			End If
			' ファイル削除
			If blnOptDelete Then
					objFile.Delete blnOptDeleteReadOnly
			End If
		End If
		' エラー処理
		If Not blnOptIgnoreError Then
			If Err.Number <> 0 Then
				Exit Sub
			End If
		End If
	Next
	' サブフォルダ処理
	If blnOptSubFolder Then
		For Each objSubFolder In objFolder.SubFolders
			DoFolder objSubFolder
			' エラー処理
			If Not blnOptIgnoreError Then
				If Err.Number <> 0 Then
					Exit Sub
				End If
			End If
		Next
	End If
	' フォルダが空の場合にフォルダ自身を削除
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
	PrintBuffer " delold.vbs : アクセスされていないファイルを削除する。"
	PrintBuffer " 書式"
	PrintBuffer "	delold.vbs <folder> <diff> [<option>]"
	PrintBuffer " 引数"
	PrintBuffer "	folder	フォルダ"
	PrintBuffer "	diff	現在から差分(単位の初期値は日数)"
	PrintBuffer "	option	オプション(省略可)"
	PrintBuffer "		/d	削除を実行する(省略時は、削除しない)"
	PrintBuffer "		/n	対象ファイル名／フォルダを表示しない"
	PrintBuffer "		/e	空のフォルダを削除する(省略時は、削除しない)"
	PrintBuffer "		/r	読み取り専用ファイル／フォルダも削除する"
	PrintBuffer "			(省略時は、削除しない)"
	PrintBuffer "		/s	サブフォルダを処理する"
	PrintBuffer "		/i	エラーを無視する"
	PrintBuffer "		/v:単位"
	PrintBuffer "			diffの単位(省略時は、日)"
	PrintBuffer "			yyyy(年),q(四半期),m(月),y(年間通算日),d(日)"
	PrintBuffer "			w(週),ww(週日),h(時),n(分),s(秒)"
	PrintBuffer "		/t:日付種類"
	PrintBuffer "			比較する日付の種類(省略時は、最終アクセス日付)"
	PrintBuffer "			c (作成日) a (最終アクセス日) w (最終更新日)"
	PrintBuffer " 例:"
	PrintBuffer "	cscript.vbs c:\temp 1 /d /s /v:m"
End Sub

Sub Quit(nExitCode)
	PrintFlush
	Wscript.Quit nExitCode
End Sub
