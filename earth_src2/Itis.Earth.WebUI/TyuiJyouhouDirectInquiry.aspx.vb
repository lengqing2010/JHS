Imports Itis.Earth.BizLogic
Partial Public Class TyuiJyouhouDirectInquiry
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dtAccountTable As New DataAccess.CommonSearchDataSet.AccountTableDataTable
        Dim ninsyou As New BizLogic.Ninsyou()
        '権限チェック
        Dim user_info As New LoginUserInfo
        Dim jBn As New Jiban '地盤画面共通クラス
        ' ユーザー基本認証
        jBn.userAuth(user_info)
        If ninsyou.GetUserID() = String.Empty Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        End If
        If user_info Is Nothing Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            Server.Transfer("CommonErr.aspx")
        End If
        Dim commonCheck As New CommonCheck
        If Not IsPostBack Then
            '参照履歴管理テーブルを登録する。
            commonCheck.SetURL(Me, ninsyou.GetUserID())
        End If

        btnTyuiJikou.Attributes.Add("onclick", "fncGamenSenni();return false;")
        btnClear.Attributes.Add("onclick", "fncClear();return false;")
        MakeJavascript()
        tbxKameitenCd.Focus()
        tbxKameitenCd.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnTyuiJikou.ClientID & ".click();return false;}")
    End Sub
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("   var objwindow;")

            .AppendLine("   function fncGamenSenni(){")
            .AppendLine("   objwindow=window.open('TyuiJyouhouInquiry.aspx?strKameitenCd='+document.all." & tbxKameitenCd.ClientID & ".value+'&randomI='+ Math.random()+new Date().getTime(),'earthMainWindow2')")
            .AppendLine("   objwindow.focus();")
            .AppendLine("   }")
            .AppendLine("   function fncClear(){")
            .AppendLine(" document.all." & tbxKameitenCd.ClientID & ".value='';")
            .AppendLine("   }")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "InputCheck", sbScript.ToString)

    End Sub

End Class