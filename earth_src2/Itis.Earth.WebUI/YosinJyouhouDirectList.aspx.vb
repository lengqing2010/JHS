Imports Itis.Earth.BizLogic
Partial Public Class YosinJyouhouDirectList
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
            Dim strKameitenCd As String = Request.QueryString("strKameitenCd")
            ViewState("strKameitenCd") = strKameitenCd
            Dim YosinJyouhouInputLogic As New YosinJyouhouInputLogic
            Dim dt As New DataTable
            dt = YosinJyouhouInputLogic.GetNayoseInfo(strKameitenCd)
            If dt.Rows.Count > 0 Then

                tbxKameitenCd.Text = TrimNull(dt.Rows(0).Item("kameiten_cd"))
                tbxKameitenMei.Text = TrimNull(dt.Rows(0).Item("kameiten_mei"))
                tbxTyousaSeikyuuCd.Text = TrimNull(dt.Rows(0).Item("nayose_saki_cd1"))
                tbxTyousaSeikyuuMei.Text = TrimNull(dt.Rows(0).Item("nayose_saki_name1"))

                tbxKojSeikyuuCd.Text = TrimNull(dt.Rows(0).Item("nayose_saki_cd2"))
                tbxKojSeikyuuMei.Text = TrimNull(dt.Rows(0).Item("nayose_saki_name2"))
                tbxHansokuhinSeikyuuCd.Text = TrimNull(dt.Rows(0).Item("nayose_saki_cd3"))
                tbxHansokuhinSeikyuuMei.Text = TrimNull(dt.Rows(0).Item("nayose_saki_name3"))
                If Me.tbxTyousaSeikyuuCd.Text <> String.Empty Then
                    Me.tbxTyousaSeikyuuCd.CssClass = "makePopup"
                    Me.tbxTyousaSeikyuuCd.Attributes("ondblclick") = "document.getElementById ('" & hidNayose.ClientID & "').value='" & tbxTyousaSeikyuuCd.Text & "';document.getElementById ('" & Button1.ClientID & "').click();"

                Else
                    Me.tbxTyousaSeikyuuCd.CssClass = "readOnly"
                End If
                If Me.tbxKojSeikyuuCd.Text <> String.Empty Then
                    Me.tbxKojSeikyuuCd.CssClass = "makePopup"
                    Me.tbxKojSeikyuuCd.Attributes("ondblclick") = "document.getElementById ('" & hidNayose.ClientID & "').value='" & tbxKojSeikyuuCd.Text & "';document.getElementById ('" & Button1.ClientID & "').click();"

                Else
                    Me.tbxKojSeikyuuCd.CssClass = "readOnly"
                End If
                If Me.tbxHansokuhinSeikyuuCd.Text <> String.Empty Then
                    Me.tbxHansokuhinSeikyuuCd.CssClass = "makePopup"
                    Me.tbxHansokuhinSeikyuuCd.Attributes("ondblclick") = "document.getElementById ('" & hidNayose.ClientID & "').value='" & tbxHansokuhinSeikyuuCd.Text & "';document.getElementById ('" & Button1.ClientID & "').click();"

                Else
                    Me.tbxHansokuhinSeikyuuCd.CssClass = "readOnly"
                End If
            Else
                Context.Items("strUrl") = ""
                Context.Items("strFailureMsg") = Messages.Instance.MSG020E
                Server.Transfer("CommonErr.aspx")
            End If
        End If
    End Sub
    Function TrimNull(ByVal obj As Object) As String
        If IsDBNull(obj) Then
            Return ""
        Else
            Return obj.ToString
        End If
    End Function

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Context.Items("strKameitenCd") = ViewState("strKameitenCd")
        Context.Items("nayose_cd") = hidNayose.Value
        Context.Items("modoru") = "YosinJyouhouDirectList.aspx"
        Server.Transfer("YosinJyouhouInput.aspx")
    End Sub
End Class