Imports Itis.Earth.BizLogic
Partial Public Class search_SAPSiireSaki
    Inherits System.Web.UI.Page
    Private CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic
    Public op_a1_ktokk As String

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            '仕入先コード
            If Request.QueryString("a1_lifnr") IsNot Nothing AndAlso Request.QueryString("a1_lifnr") <> "" Then

                Me.tbxSiireSakiCd.Text = Request.QueryString("a1_lifnr").ToString

            End If
        End If

    End Sub

    '検索
    Private Sub search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles search.Click

        BindMs("a1_ktokk asc")

    End Sub


    '明細設定
    Sub BindMs(ByVal sort As String)
        Dim top As Integer
        If Me.maxSearchCount.SelectedIndex = 0 Then
            top = 100
        Else
            top = 0
        End If

        Dim ds As DataSet = CommonSearchLogic.SelSAPSiireSaki(top, Me.tbxKdGroup.Text, Me.tbxSiireSakiCd.Text, Me.tbxSiireSakiMei.Text, sort)

        If ds.Tables(1).Rows.Count = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "", "<script language='javascript' type='text/javascript'> alert('" & Messages.Instance.MSG020E & "');</script> ")
        Else
            Me.grdBody.DataSource = ds.Tables(1)
            Me.grdBody.DataBind()
        End If

        Me.resultCount.InnerHtml = ds.Tables(1).Rows.Count & "/" & ds.Tables(0).Rows(0).Item(0)

        For i As Integer = 0 To Me.grdBody.Rows.Count - 1
            Me.grdBody.Rows(i).Attributes.Item("onclick") = "selectedLineColor(this);"
            Me.grdBody.Rows(i).Attributes.Item("ondblclick") = "DblClickRow('" & ds.Tables(1).Rows(i).Item(1).ToString & "','" & ds.Tables(1).Rows(i).Item(2).ToString & "')"
        Next
    End Sub




    '勘定ｸﾞﾙｰﾌﾟ sort
    Protected Sub a1_ktokkAsc1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles a1_ktokkAsc.Click
        BindMs("a1_ktokk asc")
    End Sub

    Protected Sub a1_ktokkDesc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles a1_ktokkDesc.Click
        BindMs("a1_ktokk desc")
    End Sub

    '仕入先ｺｰﾄﾞ sort
    Protected Sub a1_lifnrAsc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles a1_lifnrAsc.Click
        BindMs("a1_lifnr asc")
    End Sub


    Protected Sub a1_lifnrDesc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles a1_lifnrDesc.Click
        BindMs("a1_lifnr desc")
    End Sub

    '仕入先名
    Protected Sub a1_a_zz_sortAsc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles a1_a_zz_sortAsc.Click
        BindMs("a1_a_zz_sort asc")
    End Sub


    Protected Sub a1_a_zz_sortDesc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles a1_a_zz_sortDesc.Click
        BindMs("a1_a_zz_sort desc")
    End Sub

    'クリア
    Protected Sub clearWin_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clearWin.ServerClick
        Me.tbxKdGroup.Text = ""
        Me.tbxSiireSakiCd.Text = ""
        Me.tbxSiireSakiMei.Text = ""

    End Sub
End Class