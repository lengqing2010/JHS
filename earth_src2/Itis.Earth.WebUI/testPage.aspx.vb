Public Partial Class testPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Me.tbxKbn.Text = "E"
            Me.tbxBukenNo.Text = "2011010001"
        End If

        Me.tbxKbn.Focus()
        Me.btn1.Attributes.Add("onclick", "window.open('TyousaMitumorisyoSakuseiInquiry.aspx?sendSearchTerms='+escape(document.getElementById('" & Me.tbxKbn.ClientID & "').value)+'$$$'+escape(document.getElementById('" & Me.tbxBukenNo.ClientID & "').value), 'TyousaMitumorisyoSakuseiInquiry', 'menubar=no,toolbar=no,location=no,status=no,resizable=no,scrollbars=no,width=630px,height=450px,left=0px,top=0px');return false;")
        Me.btn2.Attributes.Add("onclick", "window.open('TyousaMitumoriYouDataSyuturyoku.aspx?sendSearchTerms='+escape(document.getElementById('" & Me.tbxKbn.ClientID & "').value)+'$$$'+escape(document.getElementById('" & Me.tbxBukenNo.ClientID & "').value), 'TyousaMitumoriYouDataSyuturyoku');return false;")
        Me.btn3.Attributes.Add("onclick", "window.open('TyousaSijisyo.aspx?Kbn='+escape(document.getElementById('" & Me.tbxKbn.ClientID & "').value)+'&HosyouSyoNo='+escape(document.getElementById('" & Me.tbxBukenNo.ClientID & "').value),'PintWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=no');return false;")


    End Sub
End Class