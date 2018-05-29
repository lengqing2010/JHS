Option Explicit On
Option Strict On

'---10---+---20---+---30---+---40---+---50---+---60---+---70---+---80---+---90---+--100---+

Partial Class WaitMsg
    Inherits System.Web.UI.Page

    Public htmlQuery As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim url As String = ""

        ' 検索条件入力画面から、検索条件を取得する


        url = Request.QueryString("url")

        ' 検索条件からHTMLQuery文字列を作成
        htmlQuery = Replace(url, "|", "&")
   

    End Sub


End Class
