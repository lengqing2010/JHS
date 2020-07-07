Imports Itis.Earth.BizLogic
Partial Public Class search_SAPSiireSaki
    Inherits System.Web.UI.Page
    Private CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            '仕入先コード
            If Request.QueryString("a1_lifnr") IsNot Nothing AndAlso Request.QueryString("a1_lifnr") <> "" Then
                Me.tbxSiireSakiCd.Text = Request.QueryString("a1_lifnr").ToString
            End If

        End If

    End Sub

    Private Sub search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles search.Click

        Dim top As Integer
        If Me.maxSearchCount.SelectedIndex = 0 Then
            top = 100
        Else
            top = 0
        End If

        Dim ds As DataSet = CommonSearchLogic.SelSAPSiireSaki(top, Me.tbxKdGroup.Text, Me.tbxSiireSakiCd.Text, Me.tbxSiireSakiMei.Text)

        If ds.Tables(1).Rows.Count = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "", "<script language='javascript' type='text/javascript'> alert('" & Messages.Instance.MSG020E & "');</script> ")
        Else
            Me.grdBody.DataSource = ds.Tables(1)
            Me.grdBody.DataBind()
        End If

        Me.resultCount.InnerHtml = ds.Tables(1).Rows.Count & "/" & ds.Tables(0).Rows(0).Item(0)

    End Sub






End Class