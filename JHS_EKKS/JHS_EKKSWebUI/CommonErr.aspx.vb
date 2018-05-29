Partial Class CommonErr
    Inherits System.Web.UI.Page
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            '---  エラーメッセージ  ---
            Dim strFailureMsg As String
            strFailureMsg = CType(Context.Items("strFailureMsg"), String)
            ViewState("strUrl") = CType(Context.Items("strUrl"), String)
            If strFailureMsg = "" Then
                Server.Transfer("MainMenu.aspx")
            Else
                message.Text = strFailureMsg

                If ViewState("strUrl") = "" Then
                    btnClose.Visible = True
                    btnModoru.Visible = False
                Else
                    btnClose.Visible = False
                    btnModoru.Visible = True
                End If
            End If
            Context.Items("strFailureMsg") = ""
        End If


    End Sub
    ''' <summary>
    ''' 戻る押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnModoru_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModoru.Click
        Server.Transfer(ViewState("strUrl"))
    End Sub

End Class
