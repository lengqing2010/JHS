Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class HanyouUriageInput
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '権限チェック
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnKengen As Boolean
        blnKengen = commonChk.CommonNinnsyou(strUserID, "keiri_gyoumu_kengen")
        If Not blnKengen Then
            'エラー画面へ遷移して、エラーメッセージを表示する
            Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            Server.Transfer("CommonErr.aspx")
        End If

        If Not IsPostBack Then
            '参照履歴管理テーブルを登録する。
            commonChk.SetURL(Me, strUserID)
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
        Dim hanyouUriageBC As New HanyouUriageLogic
        'アップ管理データを取得
        dtUploadKanri = hanyouUriageBC.GetUploadKanri()
        If dtUploadKanri.Rows.Count > 0 Then
            Me.grdUploadKanri.DataSource = dtUploadKanri
            Me.grdUploadKanri.DataBind()
        End If
        'アップロード管理データ件数を取得する
        intCount = hanyouUriageBC.GetUploadKanriCount()
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
            .AppendLine("   window.open('HanyouUriageErrorDetails.aspx?sendSearchTerms='+strSyoriDatetime+'$$$'+encodeURIComponent(strNyuuryokuFileMei)+'$$$'+strEdiJouhouDate, 'HanyouUriageWindow')")
            .AppendLine("   return false;")
            .AppendLine("}")
            ''「CSV取込」ボタンを押下する場合、入力チェック
     
            .AppendLine("function fncCheckCsvPath(){")
            .AppendLine("   var strPath = document.getElementById('" & Me.fupCsvUpload.ClientID & "').value;")
            .AppendLine("   document.getElementById('" & Me.hidFile.ClientID & "').value = strPath;")
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
            ''確認メッセージ
            '.AppendLine("   var strMsg = '" & Messages.Instance.MSG045C.Replace("@PARAM1", "汎用売上") & "'")
            '.AppendLine("   strMsg = strMsg.replace('@PARAM2',strPath);")
            '.AppendLine("   if(confirm(strMsg)){")
            '.AppendLine("       document.forms[0].submit();")
            '.AppendLine("   }else{")
            '.AppendLine("       return false;")
            '.AppendLine("   }")
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

        Dim strCsvLine() As String                              'CSVファイル内容
        Dim myStream As IO.Stream                               '入出力ストリーム
        Dim myReader As IO.StreamReader                         'ストリームリーダー
        Dim intLineCount As Integer = 0                         'ライン数
        Dim HanyouUriageLogic As New HanyouUriageLogic
        Dim csScript As New StringBuilder
        If HanyouUriageLogic.GetUploadKanri(Me.fupCsvUpload.FileName) > 0 Then
            With csScript
                .AppendLine(" alert('" & Messages.Instance.MSG052E & "');")
            End With
        Else
            With csScript
                '確認メッセージ

                .AppendLine("   var strMsg = '" & Messages.Instance.MSG045C.Replace("@PARAM1", "汎用売上") & "'")
                .AppendLine("   strMsg = strMsg.replace('@PARAM2','" & Replace(hidFile.Value, "\", "\\") & "');")
                .AppendLine("   if(confirm(strMsg)){")
                .AppendLine("       fncShowModal();document.getElementById ('" & btnUpload.ClientID & "').click();")
                .AppendLine("   }")
            End With
            '入出力ストリーム
            myStream = fupCsvUpload.FileContent
            'ストリームリーダー
            myReader = New IO.StreamReader(myStream, System.Text.Encoding.GetEncoding(932))
            Do
                '取込ファイルを読み込む
                ReDim Preserve strCsvLine(intLineCount)
                strCsvLine(intLineCount) = myReader.ReadLine()
                intLineCount += 1
            Loop Until myReader.EndOfStream
            ViewState("strCsvLine") = strCsvLine
            ViewState("FileName") = Me.fupCsvUpload.FileName
        End If
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
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

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim strErrMessage As String
        Dim strUmuFlg As String = ""
        Dim hanyouUriageBC As New HanyouUriageLogic
        strErrMessage = hanyouUriageBC.ChkCsvFile(CType(ViewState("strCsvLine"), String()), CType(ViewState("FileName"), String), strUmuFlg)
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
End Class