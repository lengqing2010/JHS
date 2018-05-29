Imports Itis.Earth.BizLogic
Partial Public Class HanbaiKakakuMasterInput
    Inherits System.Web.UI.Page

    ''' <summary>販売マスタ取込</summary>
    ''' <remarks>原価マスタ取込用機能を提供する</remarks>
    ''' <history>
    ''' <para>2011/03/07　呉営(大連情報システム部)　新規作成</para>
    ''' </history>
    Private hanbaiKakakuInputLogic As New HanbaiKakakuMasterLogic
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

        If Not IsPostBack Then
            '参照履歴管理テーブルを登録する。
            CommonCheck.SetURL(Me, strUserID)
            '初期化
            Call SetInitData()
        End If
        'javascript作成
        MakeJavascript()
        '「CSV取込」ボタンを押下する場合、入力チェック
        Me.btnCsvUpload.Attributes.Add("onClick", "if(fncCheckCsvPath()){fncShowModal();}else{return false;}")
        '「閉じる」ボタン処理
        Me.btnClose.Attributes.Add("onClick", "return fncClose();")

    End Sub
    ''' <summary>初期データをセットする</summary>
    Protected Sub SetInitData()

        Dim dtUploadKanri As New Data.DataTable
        Dim intCount As Integer

        'アップ管理データを取得
        dtUploadKanri = hanbaiKakakuInputLogic.GetUploadKanri()
        If dtUploadKanri.Rows.Count > 0 Then
            Me.grdUploadKanri.DataSource = dtUploadKanri
            Me.grdUploadKanri.DataBind()
        End If
        'アップロード管理データ件数を取得する
        intCount = hanbaiKakakuInputLogic.GetUploadKanriCount()
        '検索結果件数を設定する
        Call SetKensakuCount(intCount)

    End Sub
    ''' <summary>検索結果件数を設定する</summary>
    ''' <param name="intCount">検索結果件数</param>
    Private Sub SetKensakuCount(ByVal intCount As Integer)

        If intCount > 100 Then
            Me.lblCount.Text = "100 / " & intCount
            Me.lblCount.ForeColor = Drawing.Color.Red
        Else
            Me.lblCount.Text = intCount
            Me.lblCount.ForeColor = Drawing.Color.Black
        End If

    End Sub
    ''' <summary>取込エラー有無を設定する</summary>
    Private Sub grdUploadKanri_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUploadKanri.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblUmu As Web.UI.WebControls.Label = CType(e.Row.Cells(2).Controls(1), Label)
            If lblUmu.Text = "あり" Then
                lblUmu.Attributes.Add("style", "text-decoration:underline; color:Blue; cursor:hand;")
                lblUmu.Attributes.Add("onClick", "fncErrorDetails('" & e.Row.Cells(3).Text & "','" & CType(e.Row.Cells(1).Controls(1), Label).Text & "','" & e.Row.Cells(4).Text & "');return false;")
            End If
            e.Row.Cells(3).Attributes.Add("style", "display:none;")
            e.Row.Cells(4).Attributes.Add("style", "display:none;")
        End If
    End Sub
    ''' <summary>CSV取込ボタン処理</summary>
    Private Sub btnCsvUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvUpload.Click

        Dim strErrMessage As String
        Dim strUmuFlg As String = ""
        strErrMessage = hanbaiKakakuInputLogic.ChkCsvFile(Me.fupCsvUpload, strUmuFlg)
        If strErrMessage = String.Empty Then
            '取込データを表示する
            Call SetInitData()
            '完了メッセージを表示する
            If strUmuFlg = "1" Then
                ShowMessage(Messages.Instance.MSG047C)
            Else
                ShowMessage(Messages.Instance.MSG046C)
            End If
        Else
            ShowMessage(strErrMessage)
        End If
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
    ''' <summary>Javascript作成</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            'クリアボタン処理
            .AppendLine("function fncClose(){")
            .AppendLine("   window.close();")
            .AppendLine("   return false;")
            .AppendLine("}")
            '「あり」リンクを押下する場合、販売価格マスタエラー確認ポップアップ画面を起動する
            .AppendLine("function fncErrorDetails(strSyoriDatetime,strNyuuryokuFileMei,strEdiJouhouDate){")
            .AppendLine("   window.open('HanbaiKakakuMasterErrorDetails.aspx?sendSearchTerms='+strSyoriDatetime+'$$$'+encodeURIComponent(strNyuuryokuFileMei)+'$$$'+strEdiJouhouDate, 'hanbaiKakakuWindow')")
            .AppendLine("   return false;")
            .AppendLine("}")
            '「CSV取込」ボタンを押下する場合、入力チェック
            .AppendLine("function fncCheckCsvPath(){")
            .AppendLine("   var strPath = document.getElementById('" & Me.fupCsvUpload.ClientID & "').value;")
            '必須入力チェック
            .AppendLine("   if(strPath==''){")
            .AppendLine("       alert('" & Messages.Instance.MSG042E & "');")
            .AppendLine("       document.getElementById('" & Me.fupCsvUpload.ClientID & "').focus();")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '拡張子チェック
            .AppendLine("   if(strPath.length < 4){")
            .AppendLine("       alert('" & Messages.Instance.MSG043E & "');")
            .AppendLine("       document.getElementById('" & Me.fupCsvUpload.ClientID & "').focus();")
            .AppendLine("       return false;")
            .AppendLine("   }else{")
            .AppendLine("       if(strPath.substring(strPath.length-4,strPath.length).toLowerCase() != "".csv""){")
            .AppendLine("           alert('" & Messages.Instance.MSG043E & "');")
            .AppendLine("           document.getElementById('" & Me.fupCsvUpload.ClientID & "').focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("   }")
            'ファイル存在チェック
            .AppendLine("   var fso=new ActiveXObject(""Scripting.FileSystemObject"");")
            .AppendLine("   if( (strPath.indexOf(':') < 0) && (strPath.substring(0,2) != '\\\\') ){ ")
            .AppendLine("       alert('" & Messages.Instance.MSG039E & "');")
            .AppendLine("       document.getElementById('" & Me.fupCsvUpload.ClientID & "').focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }else if(!fso.FileExists(strPath)){")
            .AppendLine("       alert('" & Messages.Instance.MSG039E & "');")
            .AppendLine("       document.getElementById('" & Me.fupCsvUpload.ClientID & "').focus();")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '容量チェック
            .AppendLine("   if(fso.GetFile(strPath).size == 0){")
            .AppendLine("       alert('" & Messages.Instance.MSG044E & "');")
            .AppendLine("       document.getElementById('" & Me.fupCsvUpload.ClientID & "').focus();")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '確認メッセージ
            .AppendLine("   var strMsg = '" & Messages.Instance.MSG045C.Replace("@PARAM1", "販売価格") & "'")
            .AppendLine("   strMsg = strMsg.replace('@PARAM2',strPath);")
            .AppendLine("   if(confirm(strMsg)){")
            .AppendLine("       document.forms[0].submit();")
            .AppendLine("   }else{")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .AppendLine("   return true;")
            .AppendLine("}")
            'DIV表示
            .AppendLine("function fncShowModal(){")
            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("   var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("   if(buyDiv.style.display=='none')")
            .AppendLine("   {")
            .AppendLine("       buyDiv.style.display='';")
            .AppendLine("       disable.style.display='';")
            .AppendLine("       disable.focus();")
            .AppendLine("   }else{")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub
End Class