Public Partial Class CommonErr
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            '---  エラーメッセージ  ---
            Dim strFailureMsg As String
            strFailureMsg = CType(Context.Items("strFailureMsg"), String)
            ViewState("strUrl") = CType(Context.Items("strUrl"), String)

            message.Text = strFailureMsg

            If ViewState("strUrl") = "" Then
                btnClose.Visible = True
                btnModoru.Visible = False
            Else
                btnClose.Visible = False
                btnModoru.Visible = True
            End If
        End If


    End Sub

    Private Sub btnModoru_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModoru.Click
        Server.Transfer(ViewState("strUrl"))
    End Sub
End Class