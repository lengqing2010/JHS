Option Explicit On
Option Strict On

'---10---+---20---+---30---+---40---+---50---+---60---+---70---+---80---+---90---+--100---+

Partial Class WaitMsg
    Inherits System.Web.UI.Page

    Public htmlQuery As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim pgid As String = ""
        Dim kikan As String = ""
        Dim kiKbn As String = ""
        Dim jyoutaiKbn As String = ""
        Dim sosiki As String = ""

        Dim url As String = ""

        ' υπόΝζΚ©ηAυππζΎ·ι
        'pgid = Request.QueryString("pgid")                  'vOID
        'kikan = Request.QueryString("kikan")                'ϊΤ
        'kiKbn = Request.QueryString("kiKbn")                'ϊζͺ
        'jyoutaiKbn = Request.QueryString("jyoutaiKbn")      'σΤζͺ
        'sosiki = Request.QueryString("sosiki")              'gD

        url = Request.QueryString("url")

        ' υπ©ηHTMLQueryΆρπμ¬
        htmlQuery = url
        'htmlQuery = pgid _
        ' & "Output.aspx?kikan=" & kikan _
        ' & "&kiKbn=" & kiKbn _
        ' & "&jyoutaiKbn=" & jyoutaiKbn _
        ' & "&sosiki=" & sosiki

    End Sub


End Class
