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

        ' �����������͉�ʂ���A�����������擾����
        'pgid = Request.QueryString("pgid")                  '�v���O����ID
        'kikan = Request.QueryString("kikan")                '����
        'kiKbn = Request.QueryString("kiKbn")                '���敪
        'jyoutaiKbn = Request.QueryString("jyoutaiKbn")      '��ԋ敪
        'sosiki = Request.QueryString("sosiki")              '�g�D

        url = Request.QueryString("url")

        ' ������������HTMLQuery��������쐬
        htmlQuery = url
        'htmlQuery = pgid _
        ' & "Output.aspx?kikan=" & kikan _
        ' & "&kiKbn=" & kiKbn _
        ' & "&jyoutaiKbn=" & jyoutaiKbn _
        ' & "&sosiki=" & sosiki

    End Sub


End Class
