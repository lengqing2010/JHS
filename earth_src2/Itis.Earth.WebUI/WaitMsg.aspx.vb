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

        ' 検索条件入力画面から、検索条件を取得する
        'pgid = Request.QueryString("pgid")                  'プログラムID
        'kikan = Request.QueryString("kikan")                '期間
        'kiKbn = Request.QueryString("kiKbn")                '期区分
        'jyoutaiKbn = Request.QueryString("jyoutaiKbn")      '状態区分
        'sosiki = Request.QueryString("sosiki")              '組織

        url = Request.QueryString("url")

        ' 検索条件からHTMLQuery文字列を作成
        htmlQuery = url
        'htmlQuery = pgid _
        ' & "Output.aspx?kikan=" & kikan _
        ' & "&kiKbn=" & kiKbn _
        ' & "&jyoutaiKbn=" & jyoutaiKbn _
        ' & "&sosiki=" & sosiki

    End Sub


End Class
