
Partial Class CommonControl_KakusyuSyukeiInquiryData
    Inherits System.Web.UI.UserControl
    Private dtSource As New Data.DataTable
    Private common As New Common

    Property DataSource() As Data.DataTable
        Get
            Return dtSource
        End Get
        Set(ByVal value As Data.DataTable)
            dtSource = value
        End Set
    End Property

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    'End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        If dtSource.Rows.Count > 0 Then

            Call Me.SetDataSource()

            Me.grdSyouhinData.DataSource = dtSource
            Me.grdSyouhinData.DataBind()

        End If

        If Me.grdSyouhinData.Rows.Count > 0 Then
            Call Me.SetEmpty()
        End If

    End Sub

    Private Sub SetDataSource()

        Dim dc As Data.DataColumn

        dc = New Data.DataColumn("data3")
        dtSource.Columns.Add(dc)
        dc = New Data.DataColumn("data6")
        dtSource.Columns.Add(dc)
        dc = New Data.DataColumn("data9")
        dtSource.Columns.Add(dc)

        Dim strkeikakuKensuSyukei As String '計画件数
        Dim strkeikakuKingakuSyukei As String '計画金額
        Dim strkeikakuSoriSyukei As String '計画粗利

        Dim strjiltusekiKensuSyukei As String '実績件数
        Dim strjiltusekiKingakuSyukei As String '実績金額
        Dim strjiltusekiSoriSyukei As String '実績粗利

        Dim strTaltuseirituKensu As String '件数達成率
        Dim strKingakuKensu As String '金額達成率
        Dim strSoriKensu As String '粗利達成率

        For j As Integer = 0 To dtSource.Rows.Count - 1

            strkeikakuKensuSyukei = dtSource.Rows(j).Item("keikakuKensuSyukei").ToString
            strjiltusekiKensuSyukei = dtSource.Rows(j).Item("jiltusekiKensuSyukei").ToString

            strkeikakuKingakuSyukei = dtSource.Rows(j).Item("keikakuKingakuSyukei").ToString
            strjiltusekiKingakuSyukei = dtSource.Rows(j).Item("jiltusekiKingakuSyukei").ToString

            strkeikakuSoriSyukei = dtSource.Rows(j).Item("keikakuSoriSyukei").ToString
            strjiltusekiSoriSyukei = dtSource.Rows(j).Item("jiltusekiSoriSyukei").ToString

            '件数達成率
            If strkeikakuKensuSyukei = 0 Then
                strTaltuseirituKensu = "0.0"
            Else
                strTaltuseirituKensu = Format(((strjiltusekiKensuSyukei / strkeikakuKensuSyukei) * 100), "0.0")
            End If

            '金額達成率
            If strkeikakuKingakuSyukei = 0 Then
                strKingakuKensu = "0.0"
            Else
                strKingakuKensu = Format(((strjiltusekiKingakuSyukei / strkeikakuKingakuSyukei) * 100), "0.0")
            End If

            '粗利達成率
            If strkeikakuSoriSyukei = 0 Then
                strSoriKensu = "0.0"
            Else
                strSoriKensu = Format(((strjiltusekiSoriSyukei / strkeikakuSoriSyukei) * 100), "0.0")
            End If

            dtSource.Rows(j).Item("data3") = strTaltuseirituKensu & "%"
            dtSource.Rows(j).Item("data6") = strKingakuKensu & "%"
            dtSource.Rows(j).Item("data9") = strSoriKensu & "%"

        Next

        Dim drValue As Data.DataRow

        For i As Integer = dtSource.Rows.Count To 8
            drValue = dtSource.NewRow

            drValue.Item("data3") = "&nbsp;"

            dtSource.Rows.Add(drValue)
        Next


    End Sub

    Private Sub SetEmpty()

        If Me.grdSyouhinData.Rows.Count > 0 Then
            For i As Integer = 0 To grdSyouhinData.Rows.Count - 1
                Dim lbl As Label
                lbl = CType(grdSyouhinData.Rows(i).FindControl("lblKeikakuKensuSyukei"), Label)
                lbl.Text = IIf(lbl.Text.Trim.Equals(String.Empty), "&nbsp;", lbl.Text)
            Next

        End If

    End Sub

    Protected Sub grdSyouhinData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSyouhinData.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.Cells(0).FindControl("lblkeikakuKensuSyukei"), Label).Text = common.FormatComma(CType(e.Row.Cells(0).FindControl("lblkeikakuKensuSyukei"), Label).Text, 0)
            CType(e.Row.Cells(0).FindControl("lbljiltusekiKensuSyukei"), Label).Text = common.FormatComma(CType(e.Row.Cells(0).FindControl("lbljiltusekiKensuSyukei"), Label).Text, 0)
            CType(e.Row.Cells(0).FindControl("lblkeikakuKingakuSyukei"), Label).Text = common.FormatComma(CType(e.Row.Cells(0).FindControl("lblkeikakuKingakuSyukei"), Label).Text, 0)
            CType(e.Row.Cells(0).FindControl("lbljiltusekiKingakuSyukei"), Label).Text = common.FormatComma(CType(e.Row.Cells(0).FindControl("lbljiltusekiKingakuSyukei"), Label).Text, 0)
            CType(e.Row.Cells(0).FindControl("lblkeikakuSoriSyukei"), Label).Text = common.FormatComma(CType(e.Row.Cells(0).FindControl("lblkeikakuSoriSyukei"), Label).Text, 0)
            CType(e.Row.Cells(0).FindControl("lbljiltusekiSoriSyukei"), Label).Text = common.FormatComma(CType(e.Row.Cells(0).FindControl("lbljiltusekiSoriSyukei"), Label).Text, 0)

            CType(e.Row.Cells(0).FindControl("lblkeikakuKensuSyukei"), Label).ToolTip = common.FormatComma(CType(e.Row.Cells(0).FindControl("lblkeikakuKensuSyukei"), Label).Text, 0)
            CType(e.Row.Cells(0).FindControl("lbljiltusekiKensuSyukei"), Label).ToolTip = common.FormatComma(CType(e.Row.Cells(0).FindControl("lbljiltusekiKensuSyukei"), Label).Text, 0)
            CType(e.Row.Cells(0).FindControl("lblkeikakuKingakuSyukei"), Label).ToolTip = common.FormatComma(CType(e.Row.Cells(0).FindControl("lblkeikakuKingakuSyukei"), Label).Text, 0)
            CType(e.Row.Cells(0).FindControl("lbljiltusekiKingakuSyukei"), Label).ToolTip = common.FormatComma(CType(e.Row.Cells(0).FindControl("lbljiltusekiKingakuSyukei"), Label).Text, 0)
            CType(e.Row.Cells(0).FindControl("lblkeikakuSoriSyukei"), Label).ToolTip = common.FormatComma(CType(e.Row.Cells(0).FindControl("lblkeikakuSoriSyukei"), Label).Text, 0)
            CType(e.Row.Cells(0).FindControl("lbljiltusekiSoriSyukei"), Label).ToolTip = common.FormatComma(CType(e.Row.Cells(0).FindControl("lbljiltusekiSoriSyukei"), Label).Text, 0)
            CType(e.Row.Cells(0).FindControl("lblData3"), Label).ToolTip = CType(e.Row.Cells(0).FindControl("lblData3"), Label).Text
            CType(e.Row.Cells(0).FindControl("lblData6"), Label).ToolTip = CType(e.Row.Cells(0).FindControl("lblData6"), Label).Text
            CType(e.Row.Cells(0).FindControl("lblData9"), Label).ToolTip = CType(e.Row.Cells(0).FindControl("lblData9"), Label).Text
        End If

    End Sub
End Class
