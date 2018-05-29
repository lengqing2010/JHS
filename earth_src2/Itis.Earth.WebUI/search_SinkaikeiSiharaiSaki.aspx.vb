Imports Itis.Earth.BizLogic
Partial Public Class search_SinkaikeiSiharaiSaki
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strKbn As String = ""
        Dim strFlg As String = ""
        Dim intCols As Integer = 0
        Dim intWidth(0) As String
        If Not IsPostBack Then
            '画面のForm

            ViewState("strFormName") = Request.QueryString("FormName")
            ViewState("objCd") = Request.QueryString("objCd")
            search_Cd.Text = Request.QueryString("strCd")
            If search_Cd.Text = "" Then
                search_Cd.Text = "YMP8"

            End If

            ViewState("objCd2") = Request.QueryString("objCd2")
            search_Cd2.Text = Request.QueryString("strCd2")
            ViewState("objHidCd2") = Request.QueryString("objHidCd2")

            ViewState("objMei") = Request.QueryString("objMei")

            '加盟店

            intCols = setColWidth(intWidth, "head")

            grdHead.DataSource = CreateHeadDataSource(intCols)

            grdHead.DataBind()

            grdViewStyle(intWidth, grdHead, , False)

        End If

        lblKensaku.Text = "検索条件"
        Me.Title = "新会計支払先検索"
        lblTitle.Text = "新会計支払先検索"

        lblCd.Text = "事業所コード"
        lblCd2.Text = "支払先コード"
        lblMei.Text = "KEY支払先名"
        lblMei2.Text = "支払先名"
        divMeisai.Attributes.Add("style", "overflow-y:auto ; height:177px; width:700px;")

        search_Mei.Attributes.Add("onblur", "fncTokomozi(this)")

        clearWin.Attributes.Add("onclick", "return fncClear('" & search_Cd.ClientID & "','" & search_Cd2.ClientID & "','" & search_Mei.ClientID & "','" & search_Mei2.ClientID & "','" & maxSearchCount.ClientID & "');")
        btnCloseWin.Attributes.Add("onclick", "window.close();")
        MakeJavaScript()

    End Sub
    ''' <summary>検索データを設定</summary>
    Public Function CreateBodyDataSource(ByVal intRow As String) As DataTable
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic

        Dim dtKameitenSearchTable As New DataTable
        dtKameitenSearchTable = CommonSearchLogic.GetSinkaikeiSiharaiSakiInfo(intRow, _
                                                                    search_Cd.Text, _
                                                                    search_Cd2.Text, _
                                                                    search_Mei.Text, _
                                                                    search_Mei2.Text)
        Return dtKameitenSearchTable


    End Function

    ''' <summary>GridViewデータ列の設定</summary>
    Function setColWidth(ByRef intwidth() As String, ByVal strName As String) As Integer
        setColWidth = 2
        ReDim intwidth(setColWidth)
        If strName = "head" Then
            intwidth(0) = "130px"
            intwidth(1) = "130px"
            intwidth(2) = "399px"

        Else
            intwidth(0) = "132px"
            intwidth(1) = "132px"
            intwidth(2) = "401px"

        End If
    End Function
    ''' <summary> GridView内容、フォーマットをセット</summary>
    Sub grdViewStyle(ByVal intwidth() As String, ByVal grd As GridView, Optional ByVal dt As DataTable = Nothing, Optional ByVal blnSort As Boolean = False)
        Dim intCol As Integer = 0
        Dim intRow As Integer = 0
        If IsArray(intwidth) AndAlso grd.Rows.Count > 0 Then
            For intRow = 0 To grd.Rows.Count - 1
                For intCol = 0 To grd.Rows(intRow).Cells.Count - 1
                    grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")
                    If grd.ID = "grdHead" And blnSort Then
                        Dim strSort As String = ""
                        If Not (dt Is Nothing) Then
                            strSort = dt.Columns(intCol).ColumnName
                        End If
                        Dim lbl As New Label
                        lbl.Text = grd.Rows(intRow).Cells(intCol).Text & " "
                        grd.Rows(intRow).Cells(intCol).Controls.Add(lbl)
                        Dim lnkBtn As New LinkButton
                        lnkBtn.Text = "▲"
                        lnkBtn.Font.Underline = False
                        lnkBtn.ForeColor = Drawing.Color.SkyBlue
                        If hidColor.Value <> "" Then
                            If Split(hidColor.Value, ",")(0) = intCol And Split(hidColor.Value, ",")(1) = "1" Then
                                lnkBtn.ForeColor = Drawing.Color.IndianRed
                            End If
                        End If

                        lnkBtn.Attributes.Add("onclick", "return fncSort('" & strSort & " asc" & "','" & intCol & ",1')")
                        grd.Rows(intRow).Cells(intCol).Controls.Add(lnkBtn)
                        Dim lnkBtn2 As New LinkButton
                        lnkBtn2.Text = "▼"
                        lnkBtn2.Font.Underline = False
                        lnkBtn2.ForeColor = Drawing.Color.SkyBlue
                        If hidColor.Value <> "" Then
                            If Split(hidColor.Value, ",")(0) = intCol And Split(hidColor.Value, ",")(1) = "2" Then
                                lnkBtn2.ForeColor = Drawing.Color.IndianRed
                            End If
                        End If

                        lnkBtn2.Attributes.Add("onclick", "return fncSort('" & strSort & " desc" & "','" & intCol & ",2')")
                        grd.Rows(intRow).Cells(intCol).Controls.Add(lnkBtn2)

                    End If
                Next
            Next
        End If
    End Sub

    ''' <summary>ヘーダ－部データを設定</summary>
    Public Function CreateHeadDataSource(ByVal intCols As Integer) As DataTable
        Dim intColCount As Integer = 0
        Dim intRowCount As Integer = 0
        Dim dtHeader As New DataTable
        Dim drTemp As DataRow
        For intColCount = 0 To intCols
            dtHeader.Columns.Add(New DataColumn("col" & intColCount.ToString, GetType(String)))
        Next
        drTemp = dtHeader.NewRow
        With drTemp

            .Item(0) = "事業所コード"
            .Item(1) = "支払先コード"
            .Item(2) = "支払先名"

        End With
        dtHeader.Rows.Add(drTemp)

        Return dtHeader
    End Function
    ''' <summary>GridView行の実装</summary>
    Private Sub grdBody_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBody.RowDataBound
        e.Row.Attributes.Add("onclick", "selectedLineColor(this);")

        e.Row.Attributes.Add("ondblclick", "fncSetItem('" & ViewState("objCd") & _
                                                               "','" & e.Row.Cells(0).ClientID & _
                                                               "','" & ViewState("objCd2") & _
                                                               "','" & ViewState("objHidCd2") & _
                                                               "','" & e.Row.Cells(1).ClientID & _
                                                               "','" & ViewState("objMei") & _
                                                               "','" & e.Row.Cells(2).ClientID & _
                                                               "','" & ViewState("strFormName") & "');")


    End Sub
    ''' <summary>Javascript作成</summary>
    Protected Sub MakeJavaScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .Append("<script language='javascript' type='text/javascript'>  " & vbCrLf)

            .Append("function fncSetItem(objCd,strObjCd,objCd2,objHidCd2,strObjCd2,objMei,strObjMei,FormName)" & vbCrLf)
            .Append("{" & vbCrLf)
            .Append("if(window.opener == null || window.opener.closed){" & vbCrLf)
            .Append("    alert('呼び出し元画面が閉じられた為、値をセットできません。');" & vbCrLf)
            .Append("   return false;" & vbCrLf)
            .Append(" }" & vbCrLf)

            .Append("if (objCd!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objCd).innerText=eval('document.all.'+strObjCd).innerText;" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("if (objCd2!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objCd2).innerText=eval('document.all.'+strObjCd2).innerText;" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objHidCd2).innerText=eval('document.all.'+strObjCd2).innerText;" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("if (objMei!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objMei).innerText=eval('document.all.'+strObjMei).innerText;" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objCd2).select();" & vbCrLf)


            .Append("window.close();" & vbCrLf)
            .Append("}" & vbCrLf)

            .AppendLine("function window.onload(){" & vbCrLf)
            If Not IsPostBack Then
                .Append("eval('document.all.'+'" & search_Mei.ClientID & "').focus();" & vbCrLf)

            End If

            .AppendLine("       }")
            .Append("function fncClear(objcd,objcd2,objmei,objmei2,objddl){" & vbCrLf)
            .Append("eval('document.all.'+objcd).innerText='YMP8';" & vbCrLf)
            .Append("eval('document.all.'+objcd2).innerText='';" & vbCrLf)
            .Append("eval('document.all.'+objmei).innerText='';" & vbCrLf)
            .Append("eval('document.all.'+objmei2).innerText='';" & vbCrLf)
            .Append("eval('document.all.'+objmei).focus();" & vbCrLf)
            .Append("eval('document.all.'+objddl).selectedIndex=0;" & vbCrLf)
            .Append("return false;" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("function fncClose(){" & vbCrLf)
            .Append("self.close();" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("function fncSort(str,str2){" & vbCrLf)
            .Append("eval('document.all.'+'" & hidSort.ClientID & "').value=str;" & vbCrLf)
            .Append("eval('document.all.'+'" & hidColor.ClientID & "').value=str2;" & vbCrLf)
            .Append("document.getElementById ('" & Button.ClientID & "').click();")
            .Append("return false;")
            .Append("}" & vbCrLf)

            .Append("</script>" & vbCrLf)
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub
    Function GetMeisai(ByVal blnSort As Boolean) As DataTable
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic
        Dim strKbn As String = ""
        Dim intRowCount As Integer = 0
        Dim intWidth(0) As String
        Dim intCols As Integer = 0
        Dim dt As New DataTable
        strKbn = ViewState("strKbn")
        setColWidth(intWidth, "body")
        dt = CreateBodyDataSource(maxSearchCount.Value)
        If blnSort Then
            Dim dv As New DataView
            dv = dt.DefaultView
            dv.Sort = hidSort.Value
            grdBody.DataSource = dv
        Else
            grdBody.DataSource = dt
        End If

        grdBody.DataBind()
        '======================================

        intRowCount = CommonSearchLogic.GetSinkaikeiSiharaiSakiCount(search_Cd.Text, _
                                                                    search_Cd2.Text, _
                                                                    search_Mei.Text, _
                                                                    search_Mei2.Text)


        '===============================================
        grdViewStyle(intWidth, grdBody)
        resultCount.Style.Remove("color")
        If maxSearchCount.SelectedIndex = 1 Then
            resultCount.Style("color") = "black"
            resultCount.InnerHtml = grdBody.Rows.Count
        Else
            If intRowCount > grdBody.Rows.Count Then
                resultCount.Style("color") = "red"
                resultCount.InnerHtml = grdBody.Rows.Count & "/" & intRowCount
            Else
                resultCount.Style("color") = "black"
                resultCount.InnerHtml = grdBody.Rows.Count
            End If
        End If
        Return dt
    End Function
    Sub SetValueScript()
        Dim csScript As New StringBuilder

        With csScript
            .Append("<script language='javascript' type='text/javascript'>  " & vbCrLf)
            .Append("if(window.opener == null || window.opener.closed){" & vbCrLf)
            .Append("    alert('呼び出し元画面が閉じられた為、値をセットできません。');" & vbCrLf)
            .Append("  }else{" & vbCrLf)
            If ViewState("objCd") <> "" Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(0).ClientID & "').innerText;" & vbCrLf)
            End If
            If ViewState("objCd2") <> "" Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("objCd2") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(1).ClientID & "').innerText;" & vbCrLf)
            End If

            .Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').select();" & vbCrLf)
            .Append("if ('" & ViewState("objMei") & "'!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+'" & ViewState("objMei") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(2).ClientID & "').innerText;" & vbCrLf)
            .Append(" }" & vbCrLf)


            .Append("window.close();" & vbCrLf)
            .Append(" }" & vbCrLf)
            .Append("</script>" & vbCrLf)
        End With
        ClientScript.RegisterStartupScript(Me.GetType, "", csScript.ToString)
    End Sub

    Private Sub search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles search.Click
        Dim dt As DataTable = GetMeisai(False)
        Dim csScript As New StringBuilder
        If grdBody.Rows.Count = 1 Then
            SetValueScript()
        ElseIf grdBody.Rows.Count = 0 Then
            With csScript
                .Append("<script language='javascript' type='text/javascript'>  " & vbCrLf)

                .Append("alert('" & Messages.Instance.MSG020E & "');" & vbCrLf)
                .Append("</script>" & vbCrLf)
            End With
            ClientScript.RegisterStartupScript(Me.GetType, "", csScript.ToString)
        Else
            Dim intWidth(0) As String
            setColWidth(intWidth, "head")
            grdViewStyle(intWidth, grdHead, dt, True)
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button.Click
        Dim intWidth(0) As String
        setColWidth(intWidth, "head")
        grdViewStyle(intWidth, grdHead, GetMeisai(True), True)
    End Sub

End Class