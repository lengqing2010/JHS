Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class KameitenMasterInput
    Inherits System.Web.UI.Page

    ''' <summary>加盟店商品調査方法特別対応マスタ取込</summary>
    ''' <remarks>加盟店商品調査方法特別対応マスタ取込用機能を提供する</remarks>
    ''' <history>
    ''' <para>2011/03/08　ジン登閣(大連情報システム部)　新規作成</para>
    ''' </history>
    Private kameitenMasterBC As New KameitenMasterLogic
    Private commonCheck As New CommonCheck

    ''' <summary>ページロッド</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '権限チェック
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnEigyouKengen As Boolean
        blnEigyouKengen = commonChk.CommonNinnsyou(strUserID, "eigyou_master_kanri_kengen")
        If Not blnEigyouKengen Then
            'エラー画面へ遷移して、エラーメッセージを表示する
            Context.Items("strFailureMsg") = Messages.Instance.MSG020E
            Server.Transfer("CommonErr.aspx")
        End If

        'Javascript作成
        Call Me.MakeJavascript()

        If Not IsPostBack Then
            '参照履歴管理テーブルを登録する。
            CommonCheck.SetURL(Me, strUserID)
            '画面を設定
            Call Me.SetGamen()
        Else
            'DIV非表示
            CloseCover()
        End If

        Me.btnCsvInput.Attributes.Add("onClick", "if(!fncCheckCsvPath()){return false;}else{fncShowModal();}")
        '｢閉じる｣ボタン
        Me.btnClose.Attributes.Add("onClick", "fncClose();return false;")

    End Sub

    ''' <summary>画面を設定</summary>
    Private Sub SetGamen()

        'アップ管理データを取得
        Dim dtInputKanri As New Data.DataTable
        dtInputKanri = kameitenMasterBC.GetInputKanri()

        If dtInputKanri.Rows.Count > 0 Then
            Me.grdInputKanri.DataSource = dtInputKanri
            Me.grdInputKanri.DataBind()


            '検索結果を設定
            Dim intCount As Integer = kameitenMasterBC.GetInputKanriCount()

            If intCount > 100 Then
                Me.lblCount.Text = "100/" & CStr(intCount)
                '赤色
                Me.lblCount.ForeColor = Drawing.Color.Red
            Else
                Me.lblCount.Text = CStr(intCount)
                '黒色
                Me.lblCount.ForeColor = Drawing.Color.Black
            End If

        Else
            '検索結果を設定
            Me.lblCount.Text = "0"
            '黒色
            Me.lblCount.ForeColor = Drawing.Color.Black
        End If

    End Sub


    Private Sub btnCsvInput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvInput.Click
        Dim intLineNo As Integer = 1
        Dim exitsFlg As Boolean = False
        Dim strMsg As String
        Dim kbnAndKameitenCd As String = ""
        Dim arrCsvLine() As String
        Dim csScript As New StringBuilder
        Call Me.SetGamen()
        Dim insCd As String, updCd As String
        Dim hidInsLineNo As String = ""

       
        '加盟店存在チェック
        'strMsg = kameitenMasterBC.ChkKameiten(Me.fupCsvinput, intLineNo, exitsFlg, kbnAndKameitenCd, hidInsLineNo.Value, arrCsvLine)
        strMsg = kameitenMasterBC.ChkKameiten(Me.fupCsvinput, arrCsvLine, insCd, updCd, hidInsLineNo)

        '取込ファイル保存
        ViewState("arrCsvLine") = arrCsvLine
        ViewState("FileName") = Me.fupCsvinput.FileName
        ViewState("hidInsLineNo") = hidInsLineNo


        If strMsg = "Success" Then
            If insCd <> "" AndAlso updCd <> "" Then
                With csScript
                    '確認メッセージ
                    '.AppendLine("   var strMsg = '" & Messages.Instance.MSG045C & "'")
                    .AppendLine("   var strMsg = '新規登録の事象者情報があります。新規登録を行いますか？\n※対象の加盟店コード:" & insCd & "\n既存の加盟店コードが存在しますが上書いてよろしいでしょうか？\n※対象の加盟店コード:" & updCd & "'")
                    .AppendLine("   if(confirm(strMsg)){")
                    .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
                    .AppendLine("   }else{")
                    .AppendLine("   }")
                End With
            ElseIf insCd <> "" Then
                With csScript
                    '確認メッセージ
                    '.AppendLine("   var strMsg = '" & Messages.Instance.MSG045C & "'")
                    .AppendLine("   var strMsg = '新規登録の事象者情報があります。新規登録を行いますか？\n※対象の加盟店コード:" & insCd & "'")
                    .AppendLine("   if(confirm(strMsg)){")
                    .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
                    .AppendLine("   }else{")
                    .AppendLine("   }")
                End With
            ElseIf updCd <> "" Then
                With csScript
                    '確認メッセージ
                    '.AppendLine("   var strMsg = '" & Messages.Instance.MSG045C & "'")
                    .AppendLine("   var strMsg = '既存の加盟店コードが存在しますが上書いてよろしいでしょうか？\n※対象の加盟店コード:" & updCd & "'")
                    .AppendLine("   if(confirm(strMsg)){")
                    .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
                    .AppendLine("   }else{")
                    .AppendLine("   }")
                End With

            End If
        Else
            With csScript
                .AppendLine(" alert('" & strMsg & "');")
            End With

        End If

        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)









        'If "UnExits".Equals(strMsg) OrElse "Err".Equals(strMsg) OrElse "Success".Equals(strMsg) Then
        'Else
        '    ShowMessage(strMsg)
        '    hidLineNo.Value = String.Empty
        '    'hidExitsFlg.Value = String.Empty
        '    hidInsLineNo.Value = String.Empty
        '    ViewState("arrCsvLine") = Nothing
        'End If
        'If "UnExits".Equals(strMsg) Then
        '    '区分:ｱｯﾌﾟﾛｰﾄﾞﾌｧｲﾙ.区分(←実際の値) -加盟店ｺｰﾄﾞ:ｱｯﾌﾟﾛｰﾄﾞﾌｧｲﾙ.加盟店ｺｰﾄﾞ(←実際の値)
        '    Dim kbn As String = kbnAndKameitenCd.Split(",")(0).Trim()
        '    Dim kameitenCd As String = kbnAndKameitenCd.Split(",")(1).Trim()

        '    With csScript
        '        '確認メッセージ
        '        '.AppendLine("   var strMsg = '" & Messages.Instance.MSG045C & "'")
        '        .AppendLine("   var strMsg = '" & String.Format(Messages.Instance.MSG2065E, kbn, kameitenCd) & "'")
        '        .AppendLine("   if(confirm(strMsg)){")
        '        If "Err".Equals(strMsg) Then
        '            .AppendLine(" alert('" & String.Format(Messages.Instance.MSG2066E) & "');")
        '        Else
        '            .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
        '        End If
        '        .AppendLine("   }else{")
        '        .AppendLine("   document.getElementById ('" & hidLineNo.ClientID & "').value = ''")
        '        '.AppendLine("   document.getElementById ('" & hidExitsFlg.ClientID & "').value = ''")
        '        .AppendLine("   document.getElementById ('" & hidInsLineNo.ClientID & "').value = ''")
        '        .AppendLine("   }")
        '    End With
        '    'エラーの行号を保存
        '    hidLineNo.Value = intLineNo
        '    '組合存在しないの場合加盟店コード存在かどかを保存
        '    'hidExitsFlg.Value = exitsFlg
        '    'ページ応答で、クライアント側のスクリプト ブロックを出力します
        '    ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)

        'Else

        '    With csScript
        '        '確認メッセージ
        '        '.AppendLine("   var strMsg = '" & Messages.Instance.MSG045C & "'")
        '        .AppendLine("   var strMsg = '既存の加盟店コードが存在しますが上書いてよろしいでしょうか？ '")
        '        .AppendLine("   if(confirm(strMsg)){")
        '        If "Err".Equals(strMsg) Then
        '            .AppendLine(" alert('" & String.Format(Messages.Instance.MSG2066E) & "');")
        '        Else
        '            .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
        '        End If
        '        .AppendLine("   }else{")
        '        .AppendLine("   document.getElementById ('" & hidLineNo.ClientID & "').value = ''")
        '        '.AppendLine("   document.getElementById ('" & hidExitsFlg.ClientID & "').value = ''")
        '        .AppendLine("   document.getElementById ('" & hidInsLineNo.ClientID & "').value = ''")
        '        .AppendLine("   }")
        '    End With
        '    'エラーの行号を保存
        '    hidLineNo.Value = intLineNo
        '    '組合存在しないの場合加盟店コード存在かどかを保存
        '    'hidExitsFlg.Value = exitsFlg
        '    'ページ応答で、クライアント側のスクリプト ブロックを出力します
        '    ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)

        'End If
        ''Else
        ''    arrCsvLine = ViewState("arrCsvLine")
        ''    intLineNo = hidLineNo.Value + 1
        ''    '加盟店存在チェック
        ''    strMsg = kameitenMasterBC.ChkKameiten(Me.fupCsvinput, intLineNo, exitsFlg, kbnAndKameitenCd, hidInsLineNo.Value, arrCsvLine)
        ''    '取込ファイル保存
        ''    ViewState("arrCsvLine") = arrCsvLine
        ''    If "UnExits".Equals(strMsg) Then
        ''        '区分:ｱｯﾌﾟﾛｰﾄﾞﾌｧｲﾙ.区分(←実際の値) -加盟店ｺｰﾄﾞ:ｱｯﾌﾟﾛｰﾄﾞﾌｧｲﾙ.加盟店ｺｰﾄﾞ(←実際の値)
        ''        Dim kbn As String = kbnAndKameitenCd.Split(",")(0).Trim()
        ''        Dim kameitenCd As String = kbnAndKameitenCd.Split(",")(1).Trim()
        ''        With csScript
        ''            '確認メッセージ
        ''            '.AppendLine("   var strMsg = '" & Messages.Instance.MSG045C & "'")
        ''            .AppendLine("   var strMsg = '区分-加盟店ｺｰﾄﾞの組合せがないものが存在します。新規登録を行いますか。" & kbn & "-" & kameitenCd & "'")
        ''            .AppendLine("   if(confirm(strMsg)){")
        ''            .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
        ''            .AppendLine("   }else{")
        ''            .AppendLine("   document.getElementById ('" & hidLineNo.ClientID & "').value = ''")
        ''            .AppendLine("   document.getElementById ('" & hidExitsFlg.ClientID & "').value = ''")
        ''            .AppendLine("   }")
        ''        End With
        ''        'エラーの行号を保存
        ''        hidLineNo.Value = intLineNo
        ''        '組合存在しないの場合加盟店コード存在かどかを保存
        ''        hidExitsFlg.Value = exitsFlg
        ''        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ''        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
        ''    End If
        ''End If
        ''End If

        ''If "Success".Equals(strMsg) Then
        ''    Dim strUmuFlg As String = String.Empty
        ''    Dim strMessage As String = kameitenMasterBC.ChkCsvFile(strUmuFlg, hidInsLineNo.Value, arrCsvLine, ViewState("FileName").ToString)

        ''    '画面を設定

        ''    Call Me.SetGamen()
        ''    If strMessage = String.Empty Then

        ''        '完了メッセージを表示する
        ''        If strUmuFlg = "1" Then
        ''            ShowMessage(Messages.Instance.MSG047C)
        ''        Else
        ''            ShowMessage(Messages.Instance.MSG046C)
        ''        End If
        ''    Else
        ''        ShowMessage(strMessage)
        ''    End If
        ''    hidLineNo.Value = String.Empty
        ''    'hidExitsFlg.Value = String.Empty
        ''    hidInsLineNo.Value = String.Empty
        ''    ViewState("arrCsvLine") = Nothing
        ''End If


    End Sub
    Private Sub btnCsvCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvCheck.Click
        Dim intLineNo As Integer = 0
        Dim exitsFlg As Boolean = False
        Dim strMsg As String
        Dim kbnAndKameitenCd As String = ""
        Dim arrCsvLine() As String
        Dim csScript As New StringBuilder
        Call Me.SetGamen()
        'If Not String.IsNullOrEmpty(hidExitsFlg.Value) AndAlso "True".Equals(hidExitsFlg.Value) Then
        '    ShowMessage("新規登録の加盟店ｺｰﾄﾞが重複してます")
        '    hidLineNo.Value = String.Empty
        '    hidExitsFlg.Value = String.Empty
        '    hidInsLineNo.Value = String.Empty
        '    ViewState("arrCsvLine") = Nothing
        'Else
        ' If String.IsNullOrEmpty(hidLineNo.Value) Then
        'arrCsvLine = ViewState("arrCsvLine")
        'intLineNo = hidLineNo.Value + 1
        ''加盟店存在チェック
        'strMsg = kameitenMasterBC.ChkKameiten(Me.fupCsvinput, intLineNo, exitsFlg, kbnAndKameitenCd, hidInsLineNo.Value, arrCsvLine)
        ''取込ファイル保存
        ''ViewState("arrCsvLine") = arrCsvLine
        'If "UnExits".Equals(strMsg) OrElse "Err".Equals(strMsg) Then
        '    '区分:ｱｯﾌﾟﾛｰﾄﾞﾌｧｲﾙ.区分(←実際の値) -加盟店ｺｰﾄﾞ:ｱｯﾌﾟﾛｰﾄﾞﾌｧｲﾙ.加盟店ｺｰﾄﾞ(←実際の値)
        '    Dim kbn As String = kbnAndKameitenCd.Split(",")(0).Trim()
        '    Dim kameitenCd As String = kbnAndKameitenCd.Split(",")(1).Trim()

        '    With csScript
        '        '確認メッセージ
        '        .AppendLine("   var strMsg = '" & String.Format(Messages.Instance.MSG2065E, kbn, kameitenCd) & "'")
        '        .AppendLine("   if(confirm(strMsg)){")
        '        If "Err".Equals(strMsg) Then
        '            .AppendLine(" alert('" & String.Format(Messages.Instance.MSG2066E) & "');")
        '        Else
        '            .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
        '        End If
        '        .AppendLine("   }else{")
        '        .AppendLine("   document.getElementById ('" & hidLineNo.ClientID & "').value = ''")
        '        '.AppendLine("   document.getElementById ('" & hidExitsFlg.ClientID & "').value = ''")
        '        .AppendLine("   document.getElementById ('" & hidInsLineNo.ClientID & "').value = ''")
        '        .AppendLine("   }")
        '    End With
        '    'エラーの行号を保存
        '    hidLineNo.Value = intLineNo

        '    'ページ応答で、クライアント側のスクリプト ブロックを出力します
        '    ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
        'End If
        'Else
        'arrCsvLine = ViewState("arrCsvLine")
        'intLineNo = hidLineNo.Value + 1
        ''加盟店存在チェック
        'strMsg = kameitenMasterBC.ChkKameiten(Me.fupCsvinput, intLineNo, exitsFlg, kbnAndKameitenCd, hidInsLineNo.Value, arrCsvLine)
        ''取込ファイル保存
        'ViewState("arrCsvLine") = arrCsvLine
        'If "UnExits".Equals(strMsg) Then
        '    '区分:ｱｯﾌﾟﾛｰﾄﾞﾌｧｲﾙ.区分(←実際の値) -加盟店ｺｰﾄﾞ:ｱｯﾌﾟﾛｰﾄﾞﾌｧｲﾙ.加盟店ｺｰﾄﾞ(←実際の値)
        '    Dim kbn As String = kbnAndKameitenCd.Split(",")(0).Trim()
        '    Dim kameitenCd As String = kbnAndKameitenCd.Split(",")(1).Trim()
        '    With csScript
        '        '確認メッセージ
        '        '.AppendLine("   var strMsg = '" & Messages.Instance.MSG045C & "'")
        '        .AppendLine("   var strMsg = '区分-加盟店ｺｰﾄﾞの組合せがないものが存在します。新規登録を行いますか。" & kbn & "-" & kameitenCd & "'")
        '        .AppendLine("   if(confirm(strMsg)){")
        '        .AppendLine("       fncShowModal();document.getElementById ('" & btnCsvCheck.ClientID & "').click();")
        '        .AppendLine("   }else{")
        '        .AppendLine("   document.getElementById ('" & hidLineNo.ClientID & "').value = ''")

        '        .AppendLine("   }")
        '    End With
        '    'エラーの行号を保存
        '    hidLineNo.Value = intLineNo

        '    'ページ応答で、クライアント側のスクリプト ブロックを出力します
        '    ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
        'End If
        'End If
        'End If

        'If "Success".Equals(strMsg) Then
        Dim strUmuFlg As String = String.Empty
        Dim strMessage As String = kameitenMasterBC.ChkCsvFile(strUmuFlg, ViewState("hidInsLineNo").ToString, ViewState("arrCsvLine"), ViewState("FileName").ToString)

        '画面を設定
        Call Me.SetGamen()

        If strMessage = String.Empty Then

            '完了メッセージを表示する
            If strUmuFlg = "1" Then
                ShowMessage(Messages.Instance.MSG047C)
            Else
                ShowMessage(Messages.Instance.MSG046C)
            End If
        Else
            ShowMessage(strMessage)
        End If

        ViewState("hidInsLineNo") = ""

        'hidLineNo.Value = String.Empty
        'hidExitsFlg.Value = String.Empty
        'hidInsLineNo.Value = String.Empty

        ViewState("arrCsvLine") = Nothing
        ' End If
    End Sub

    Private Sub grdInputKanri_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdInputKanri.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = CDate(e.Row.Cells(0).Text).ToString("yyyy/MM/dd HH:mm:ss")
            Dim linkButton As Web.UI.WebControls.LinkButton = CType(e.Row.Cells(2).Controls(1), LinkButton)
            If linkButton.Text = "なし" Then
                linkButton.Visible = False
                e.Row.Cells(2).Text = "なし"
            Else
                Dim sendSearchTerms As String = CType(e.Row.Cells(2).Controls(5), HiddenField).Value & "$$$" & CType(e.Row.Cells(1).Controls(1), Label).Text _
                                                & "$$$" & CType(e.Row.Cells(2).Controls(3), HiddenField).Value
                linkButton.Attributes.Add("style", "text-decoration:underline; color:Blue; cursor:hand;")
                linkButton.Attributes.Add("onClick", "fncErrorDetails('" & sendSearchTerms & "');return false;")
            End If
        End If
    End Sub

    ''' <summary>Javascript作成</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            'エラー確認画面をポップアップする
            .AppendLine("   function fncErrorDetails(strValue){")
            .AppendLine("       var sendSearchTerms;")
            .AppendLine("       window.open('KameitenMasterErrorDetails.aspx?sendSearchTerms='+encodeURIComponent(strValue),'ErrorDetails','menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("   }")
            'CLOSEボタン処理
            .AppendLine("   function fncClose(){")
            .AppendLine("       window.close();")
            .AppendLine("   }")
            '「CSV取込」ボタンを押下する場合、入力チェック
            .AppendLine("   function fncCheckCsvPath(){")
            .AppendLine("       var strId = '" & Me.fupCsvinput.ClientID & "';")
            .AppendLine("       var strPath = document.getElementById('" & Me.fupCsvinput.ClientID & "').value;")
            .AppendLine("       if(strPath == ''){")
            .AppendLine("           alert('" & Messages.Instance.MSG042E & "');")
            .AppendLine("           document.getElementById(strId).focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '拡張子チェック
            .AppendLine("       if(right(strPath.toLowerCase(),4) != "".csv""){")
            .AppendLine("           alert('" & Messages.Instance.MSG043E & "');")
            .AppendLine("           document.getElementById(strId).focus();")
            .AppendLine("           document.getElementById(strId).select();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            'ファイル存在チェック
            .AppendLine("       if(!FileExist(strPath)){")
            .AppendLine("           alert('" & Messages.Instance.MSG039E & "');")
            .AppendLine("           document.getElementById(strId).focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            'サイズチェック
            .AppendLine("       if(GetSize(strPath) == 0){")
            .AppendLine("           alert('" & Messages.Instance.MSG044E & "');")
            .AppendLine("           document.getElementById(strId).focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '確認メッセージ
            .AppendLine("       if(confirm('" & Messages.Instance.MSG054C.Replace("@PARAM1", "加盟店情報一括取込") & "'.replace('@PARAM2',strPath))){")
            .AppendLine("           return true;}")
            .AppendLine("       else{")
            .AppendLine("           document.getElementById(strId).focus();")
            .AppendLine("           return false;}")
            .AppendLine("   }")
            'ファイル存在チェック関数
            .AppendLine("   function FileExist(fPath){")
            .AppendLine("        if( (fPath.indexOf(':') < 0) && (left(fPath,2) != '\\\\') ) ")
            .AppendLine("        { ")
            .AppendLine("           return false; ")
            .AppendLine("        } ")
            .AppendLine("       var sfso=new ActiveXObject(""Scripting.FileSystemObject"");")
            .AppendLine("       if(sfso.FileExists(fPath)){")
            .AppendLine("            sfso=null;return true; ")
            .AppendLine("       }")
            .AppendLine("       else{")
            .AppendLine("           sfso=null;return false; ")
            .AppendLine("       }")
            .AppendLine("   }")
            'ファイルサイズを取得
            .AppendLine("   function GetSize(files){")
            .AppendLine("       var fso,f;")
            .AppendLine("	    var fSize;   ")
            .AppendLine("       fso=new ActiveXObject(""Scripting.FileSystemObject"");")
            .AppendLine("       f=fso.GetFile(files);")
            .AppendLine("       fSize = f.size; ")
            .AppendLine("       sfso=null;f=null; ")
            .AppendLine("	  return fSize; ")
            .AppendLine("   }")
            .AppendLine("   function LTrim(str){")
            .AppendLine("       var i;")
            .AppendLine("       for(i=0;i<str.length;i++){")
            .AppendLine("            if(str.charAt(i)!=' '&&str.charAt(i)!='　')break;")
            .AppendLine("       }")
            .AppendLine("       str=str.substring(i,str.length);")
            .AppendLine("       return str;")
            .AppendLine("   }")
            .AppendLine("   function RTrim(str){")
            .AppendLine("       var i;")
            .AppendLine("       for(i=str.length-1;i>=0;i--)")
            .AppendLine("           {if(str.charAt(i)!=' '&&str.charAt(i)!='　')break;}")
            .AppendLine("       str=str.substring(0,i+1);")
            .AppendLine("       return str;")
            .AppendLine("   }")
            'スペースを削除
            .AppendLine("   function Trim(str){")
            .AppendLine("       return LTrim(RTrim(str));")
            .AppendLine("   }")
            .AppendLine("   function left(mainStr,lngLen){")
            .AppendLine("       if (lngLen>0) {return mainStr.substring(0,lngLen);}")
            .AppendLine("       else{return null;}")
            .AppendLine("   }")
            .AppendLine("   function right(mainStr,lngLen){")
            .AppendLine("       if (mainStr.length-lngLen>=0 && mainStr.length>=0 && mainStr.length-lngLen<=mainStr.length){")
            .AppendLine("           return mainStr.substring(mainStr.length-lngLen,mainStr.length);}")
            .AppendLine("       else{return null;}")
            .AppendLine("   }")
            .AppendLine("   function mid(mainStr,starnum,endnum){")
            .AppendLine("       if (mainStr.length>=0){")
            .AppendLine("           return mainStr.substr(starnum,endnum);")
            .AppendLine("       }")
            .AppendLine("       else{return null;}")
            .AppendLine("   }")
            'DIV表示
            .AppendLine("   function fncShowModal(){")
            .AppendLine("      var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("      var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("      if(buyDiv.style.display=='none')")
            .AppendLine("      {")
            .AppendLine("       buyDiv.style.display='';")
            .AppendLine("       disable.style.display='';")
            .AppendLine("       disable.focus();")
            .AppendLine("      }else{")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("      }")
            .AppendLine("   }")
            'DIV非表示
            .AppendLine("   function fncClosecover(){")
            .AppendLine("       var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("       var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("   }")

            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub

    ''' <summary>メッセージ表示</summary>
    ''' <param name="strMessage">メッセージ</param>
    Private Sub ShowMessage(ByVal strMessage As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("	alert('" & strMessage & "');")
            .AppendLine("}")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
    End Sub

    ''' <summary>DIV非表示</summary>
    Public Sub CloseCover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat("fncClosecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseCover", csScript.ToString, True)
    End Sub

End Class