Imports Itis.Earth.BizLogic
Partial Public Class YosinJyouhouDirectInquiry
    Inherits System.Web.UI.Page
    Private YosinJyouhouInputLogic As New YosinJyouhouInputLogic
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then '
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
            '参照履歴管理テーブルを登録する。
            Dim commonCheck As New CommonCheck
            commonCheck.SetURL(Me, ninsyou.GetUserID())
        End If


        'btnTyuiJikou.Attributes.Add("onclick", "fncGamenSenni();return false;")
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
            .AppendLine("   function fncClear(){")
            .AppendLine(" document.all." & tbxKameitenCd.ClientID & ".value='';")
            .AppendLine("   }")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "InputCheck", sbScript.ToString)

    End Sub
    Private Sub MakeJavascript2(ByVal KameitenMei As String, ByVal nayose_cd As String)
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("   var objwindow;")
            .AppendLine("   function window.onload(){")
            If nayose_cd = "" Then

                .AppendLine("   objwindow=window.open('YosinJyouhouDirectList.aspx?strKameitenCd='+document.all." & tbxKameitenCd.ClientID & ".value,'earthMainWindow2')")
            Else
                .AppendLine("   objwindow=window.open('YosinJyouhouInput.aspx?modoru=YosinJyouhouDirectInquiry.aspx&strKameitenCd='+document.all." & tbxKameitenCd.ClientID & ".value+'&strNayoseCd='+" & nayose_cd & ",'earthMainWindow2')")


            End If
            .AppendLine("   objwindow.focus();")
            .AppendLine("   }")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "InputCheck2", sbScript.ToString)

    End Sub

  
    Protected Sub btnTyuiJikou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTyuiJikou.Click
        Dim dt As New DataTable
        dt = YosinJyouhouInputLogic.GetNayoseInfo(tbxKameitenCd.Text)
        If dt.Rows.Count = 0 Then
            Context.Items("strUrl") = "YosinJyouhouDirectInquiry.aspx"
            Context.Items("strFailureMsg") = Messages.Instance.MSG020E
            Server.Transfer("CommonErr.aspx")
        Else
            With dt.Rows(0)
                If Len(TrimNull(.Item("nayose_saki_cd1")) & TrimNull(.Item("nayose_saki_cd2")) & TrimNull(.Item("nayose_saki_cd3"))) = 5 Then
                    Context.Items("strKameitenCd") = tbxKameitenCd.Text
                    If TrimNull(.Item("nayose_saki_cd1")) <> "" Then
                        MakeJavascript2(.Item("kameiten_cd"), .Item("nayose_saki_cd1"))
                    End If
                    If TrimNull(.Item("nayose_saki_cd2")) <> "" Then
                        MakeJavascript2(.Item("kameiten_cd"), .Item("nayose_saki_cd2"))
                    End If
                    If TrimNull(.Item("nayose_saki_cd3")) <> "" Then
                        MakeJavascript2(.Item("kameiten_cd"), .Item("nayose_saki_cd3"))
                    End If
                    'Server.Transfer("YosinJyouhouInput.aspx")

                Else
                    MakeJavascript2(.Item("kameiten_cd"), "")
                    tbxKameitenCd.Focus()
                End If
            End With
        End If


    End Sub
    Function TrimNull(ByVal obj As Object) As String
        If IsDBNull(obj) Then
            Return ""
        Else
            Return obj.ToString
        End If
    End Function
End Class