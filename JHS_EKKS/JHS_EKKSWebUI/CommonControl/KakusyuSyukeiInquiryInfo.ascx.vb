
Partial Class CommonControl_KakusyuSyukeiInquiryInfo
    Inherits System.Web.UI.UserControl

    Private dtSource As New Data.DataTable
    Private common As New Common
    Property DataSource() As Data.DataTable
        Get
            Return dtSource
        End Get
        Set(ByVal value As Data.DataTable)
            dtSource = value
            ViewState("dtSource") = value
        End Set
    End Property

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    'End Sub


    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        dtSource = CType(ViewState("dtSource"), Data.DataTable)

        If dtSource.Rows.Count > 0 Then
            Dim strTitle As String = dtSource.Rows(0).Item("Siten").ToString.Trim

            Dim strRitu1 As String = dtSource.Rows(0).Item("koujiHanteiritu").ToString.Trim

            Dim strRitu2 As String = dtSource.Rows(0).Item("koujiJyutyuuritu").ToString.Trim

            Dim strRitu3 As String = dtSource.Rows(0).Item("tyokuKoujiritu").ToString.Trim

            Me.lblTitle.Text = strTitle
            Me.lblRitu1.Text = strRitu1
            Me.lblRitu2.Text = strRitu2
            Me.lblRitu3.Text = strRitu3

            Call Me.SetDataSource()

            Me.grdSyouhinInfo.DataSource = dtSource
            Me.grdSyouhinInfo.DataBind()

            Call Me.GridViewCombine(grdSyouhinInfo, 0)

            Call Me.SetEmpty()

        End If

    End Sub

    Private Sub SetDataSource()

        Dim drValue As Data.DataRow

        For i As Integer = dtSource.Rows.Count To 8
            drValue = dtSource.NewRow
            drValue.Item("Siten") = String.Empty
            drValue.Item("meisyou") = String.Empty
            drValue.Item("syouhin_mei") = String.Empty

            dtSource.Rows.Add(drValue)
        Next

    End Sub

    'Protected Sub grdSyouhinInfo_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSyouhinInfo.DataBound

    '    If Me.grdSyouhinInfo.Rows.Count > 0 Then
    '        Call Me.GridViewCombine(grdSyouhinInfo, 0)
    '    End If

    'End Sub

    Private Sub GridViewCombine(ByRef grdToCombine As GridView, ByVal column As Integer)
        'セルの結合数を記録
        Dim rowpanFlg As Integer = 1
        '臨時的な結合のデータを記録
        Dim curData As String = ""
        '結合されたデータのインデックスを記録
        Dim curIndex As Integer
        curIndex = 0
        For i As Integer = 0 To grdToCombine.Rows.Count - 1
            If (i > 0) AndAlso (Match(curData, CType(grdToCombine.Rows(i).Cells(column).Controls(1), Label).Text)) Then
                rowpanFlg += 1
                grdToCombine.Rows(i).Cells.RemoveAt(column)
                grdToCombine.Rows(curIndex).Cells(column).RowSpan = rowpanFlg
            Else
                rowpanFlg = 1
                curData = CType(grdToCombine.Rows(i).Cells(column).Controls(1), Label).Text
                curIndex = i
            End If
        Next
    End Sub


    Private Function Match(ByVal text As String, ByVal textNext As String) As Boolean
        Return text = textNext
    End Function

    Private Sub SetEmpty()

        If Me.grdSyouhinInfo.Rows.Count > 0 Then
            For i As Integer = 0 To grdSyouhinInfo.Rows.Count - 1
                Dim lbl As Label
                lbl = CType(grdSyouhinInfo.Rows(i).FindControl("lblSyouhinMei"), Label)
                lbl.Text = IIf(lbl.Text.Trim.Equals(String.Empty), "&nbsp;", lbl.Text)
            Next

        End If

    End Sub

    Protected Sub grdSyouhinInfo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSyouhinInfo.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.Cells(0).FindControl("lblTanka"), Label).Text = Common.FormatComma(CType(e.Row.Cells(0).FindControl("lblTanka"), Label).Text, 0)
            CType(e.Row.Cells(0).FindControl("lblTanka"), Label).ToolTip = common.FormatComma(CType(e.Row.Cells(0).FindControl("lblTanka"), Label).Text, 0)

        End If
    End Sub
End Class