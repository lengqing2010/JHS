Imports Itis.Earth.BizLogic
Partial Public Class KairyKojSyubetuMaster
    Inherits System.Web.UI.Page
    Private blnBtn As Boolean
    Private intWidth(3) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strNinsyou As String = ""
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        'å†å¿É`ÉFÉbÉNÇ®ÇÊÇ—ê›íË
        blnBtn = commonChk.CommonNinnsyou(strUserID, "kekka_gyoumu_kengen")
        ViewState("UserId") = strUserID
        If Not IsPostBack Then

            ViewState("SyubetuNo") = ""

            ViewState("UniqueID") = Nothing
            ViewState("MeisaiID") = Nothing

            SetHeadData()
            ViewState("dtTable") = SetbodyData(True)

            SetColWidth(intWidth, "body")
            GridViewStyle(intWidth, grdBody)

            SetColWidth(intWidth, "head")
            GridViewStyle(intWidth, grdHead)
            UpdatePanelA.Update()
            SetKakutyou(ddlKoj, "")

        Else
            closecover()
        End If
        divMeisai.Attributes.Add("style", "overflow-y:auto ; height:390px;width:438px;")
        MakeScript()

        btnSearch.Attributes.Add("onclick", "document.getElementById ('" & hidTop.ClientID & "').value =0;")
        Me.divMeisai.Attributes.Add("onscroll", "document.getElementById ('" & hidTop.ClientID & "').value =" & Me.divMeisai.ClientID & ".scrollTop;")
        btnClear.Attributes.Add("onclick", "return itemclear();")
    End Sub
    ''' <summary>ãÛîíÇçÌèú</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function
    ''' <summary>
    ''' CLOSE Div
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub closecover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat( _
                                "closecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, _
                                        Me.GetType(), _
                                        "closecover1", _
                                        csScript.ToString, _
                                        True)
    End Sub
    Sub SetKakutyou(ByVal ddl As DropDownList, ByVal strSyubetu As String)
        Dim KairyKojSyubetuLogic As New Itis.Earth.BizLogic.KairyKojSyubetuLogic
        Dim dtTable As New DataTable
        Dim intCount As Integer = 0
        dtTable = KairyKojSyubetuLogic.SelKojSyubetuInfo(strSyubetu)
        ddl.Items.Clear()
        Dim ddlist As New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddl.Items.Add(ddlist)

        For intCount = 0 To dtTable.Rows.Count - 1
            ddlist = New ListItem

            ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("code")) & ":" & TrimNull(dtTable.Rows(intCount).Item("meisyou"))
            ddlist.Value = TrimNull(dtTable.Rows(intCount).Item("code"))
            ddl.Items.Add(ddlist)
        Next
    End Sub

    ''' <summary>GridViewï¿Ç◊ÇÈïùÇÃê›íË</summary>
    Function SetColWidth(ByRef intwidth() As String, ByVal strName As String) As Integer
        If strName = "head" Then

            intwidth(0) = "114px"
            intwidth(1) = "221px"
            intwidth(2) = "89px"
        Else
            intwidth(0) = "115px"
            intwidth(1) = "215px"
            intwidth(2) = "91px"
        End If
    End Function
    Sub SetHeadData()
        Dim CommonLG As New CommonLogic()
        grdHead.DataSource = CommonLG.CreateHeadDataSource("â¸ó«çHéñéÌï NO,â¸ó«çHéñéÌï ,èàóù")
        grdHead.DataBind()
    End Sub
    Sub SetDropSelect(ByVal ddl As DropDownList, ByVal strValue As String)
        If ddl.Items.FindByValue(strValue) IsNot Nothing Then
            ddl.SelectedValue = strValue
        Else
            ddl.SelectedIndex = 0
        End If
    End Sub
    Sub GridViewStyle(ByVal intwidth() As String, ByVal grd As GridView)
        Dim intCol As Integer = 0
        Dim intRow As Integer = 0

        grd.Attributes.Add("Width", "422px")
        If IsArray(intwidth) AndAlso grd.Rows.Count > 0 Then
            For intRow = 0 To grd.Rows.Count - 1
                For intCol = 0 To grd.Rows(intRow).Cells.Count - 1
                    grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")
                    grd.Rows(intRow).Cells(intCol).Attributes.Add("align", "center")
                Next
            Next
        End If
    End Sub


    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewState("SyubetuNo") = ddlKoj.SelectedValue
        SetHeadData()
        ViewState("dtTable") = SetbodyData(True, Messages.Instance.MSG2034E)

        SetColWidth(intWidth, "body")
        GridViewStyle(intWidth, grdBody)

        SetColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)
        ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "txtfocus", " window.setTimeout('objEBI(\'" & grdBody.Rows(grdBody.Rows.Count - 1).Cells(0).Controls(0).ClientID & "\').focus()',1); ", True)

    End Sub


    ''' <summary> GridViewBODYïîÇÉZÉbÉg</summary>
    Function SetbodyData(ByVal blnKousin As Boolean, Optional ByVal msg As String = "") As DataTable

        Dim CommonSearchLogic As New Itis.Earth.BizLogic.KairyKojSyubetuLogic
        Dim dtTable As New DataTable
        Dim CommonLG As New CommonLogic()
        Dim intRow As Integer = 0
        Dim intColCount As Integer = 0
        If blnKousin Then
            ViewState("MeisaiID") = Nothing
            ViewState("UniqueID") = Nothing

            dtTable = CommonSearchLogic.SelKojSyubetuInfo(ViewState("SyubetuNo").ToString)
            If dtTable.Rows.Count = 0 Then
                If msg <> "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & msg & "');", True)
                End If
            End If
            dtTable.Rows.Add(dtTable.NewRow)
        Else
            dtTable = ViewState("dtTable")
        End If
        grdBody.DataSource = dtTable
        grdBody.DataBind()
        SetbodyData = dtTable

        Dim strUniqueID As String = ""
        Dim MeisaiID As New Dictionary(Of String, String)
        For intRow = 0 To grdBody.Rows.Count - 1
            Dim arrRowId(6) As String
            strUniqueID = ""
            If intRow = grdBody.Rows.Count - 1 Then

                'â¸ó«çHéñéÌï NO


                Dim txtControl As New TextBox
                txtControl.Width = Unit.Pixel(80)
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(0))
                Else

                    txtControl.Text = ""
                End If
                txtControl.CssClass = "hissu"
                txtControl.Attributes.Add("maxlength", "7")
                txtControl.Style.Add("ime-mode", "disabled")
                txtControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl)
                arrRowId(0) = txtControl.ClientID
                strUniqueID = strUniqueID & txtControl.UniqueID & ","
                'â¸ó«çHéñéÌï 
                Dim txtControl1 As New TextBox
                txtControl1.Width = Unit.Pixel(180)
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl1.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(1))
                Else
                    txtControl1.Text = ""
                End If
                txtControl1.CssClass = "hissu"
                txtControl1.Attributes.Add("maxlength", "20")

                txtControl1.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtControl1)
                arrRowId(1) = txtControl1.ClientID
                strUniqueID = strUniqueID & txtControl1.UniqueID & ","

                arrRowId(2) = ""
                arrRowId(3) = intRow

                'ìoò^
                Dim btnControl As New Button
                btnControl.Text = "ìoò^"
                btnControl.Attributes.Add("onclick", "fncDisable();return fncSetRowData('ìoò^','" & _
                                                        arrRowId(0) & "','" & _
                                                        arrRowId(1) & "','" & _
                                                        arrRowId(2) & "','" & _
                                                        arrRowId(3) & "');")
                If Not blnBtn Then
                    btnControl.Enabled = False
                End If

                btnControl.Style.Add("margin-left", "2px")
                grdBody.Rows(intRow).Cells(2).Controls.Add(btnControl)
                If blnBtn Then
                    txtControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControl1.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                End If
                'Lable
                Dim txt As New TextBox
                txt.Style.Add("border-style", "none;")
                txt.Style.Add("background-color", "transparent;")
                txt.Attributes.Add("TabIndex", "-1")
                txt.ReadOnly = True
                txt.Width = Unit.Pixel(40)
                grdBody.Rows(intRow).Cells(2).Controls.Add(txt)
                ViewState("UniqueID") = strUniqueID
    
            Else

                'â¸ó«çHéñéÌï NO
                Dim txtControl As New TextBox
                txtControl.Width = Unit.Pixel(80)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Style.Add("border-bottom", "none;")
                txtControl.Attributes.Add("readonly", "true")
                txtControl.Attributes.Add("TabIndex", "-1")
                If ViewState("MeisaiID") Is Nothing Then
                    txtControl.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(0))
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtControl.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(0))
                    Else
                        txtControl.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(0))
                    End If
                End If
                txtControl.Style.Add("margin-left", "1px;")
                txtControl.Style.Add("text-align", "left;")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl)
                arrRowId(0) = txtControl.ClientID
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"

                'â¸ó«çHéñéÌï 
                txtControl = New TextBox
                txtControl.Width = Unit.Pixel(180)
                txtControl.CssClass = "hissu"


                If ViewState("MeisaiID") Is Nothing Then
                    txtControl.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(1))
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtControl.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(1))
                    Else
                        txtControl.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(1))
                    End If
                End If
                txtControl.Attributes.Add("maxlength", "20")
                txtControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtControl)
                arrRowId(1) = txtControl.ClientID
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"


                'èCê≥
                Dim hidControl As New HiddenField
                Dim hidTime As New HiddenField
                Dim btnControl As New Button
                btnControl.Text = "èCê≥"

                If ViewState("MeisaiID") Is Nothing Then
                    btnControl.Attributes.Add("disabled", "true")
                    hidControl.Value = "true"
                Else

                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        If Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(2) = "" Then
                            btnControl.Enabled = Not CBool(hidBool.Value)
                        Else
                            btnControl.Enabled = Not CBool(Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(2)))
                        End If
                        hidControl.Value = Not btnControl.Enabled
                    Else
                        btnControl.Attributes.Add("disabled", "true")
                        hidControl.Value = "true"
                    End If
                End If
                If Not blnBtn Then
                    btnControl.Enabled = False
                End If

                btnControl.Style.Add("margin-left", "2px")
                hidTime.Value = dtTable.Rows(intRow).Item("upd_datetime").ToString
                grdBody.Rows(intRow).Cells(2).Controls.Add(btnControl)
                grdBody.Rows(intRow).Cells(2).Controls.Add(hidControl)
                grdBody.Rows(intRow).Cells(2).Controls.Add(hidTime)

                arrRowId(2) = hidTime.Value


                arrRowId(3) = intRow
                btnControl.Attributes.Add("onclick", "fncDisable();" & hidControl.ClientID & ".value=" & btnControl.ClientID & ".disabled;return fncSetRowData('èCê≥','" & _
                                                                        arrRowId(0) & "','" & _
                                                                        arrRowId(1) & "','" & _
                                                                        arrRowId(2) & "','" & _
                                                                        arrRowId(3) & "');")

                If blnBtn Then
                    txtControl.Attributes.Add("onPropertyChange", "fncButtonChange(this," & btnControl.ClientID & "," & hidControl.ClientID & ");")

                    txtControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")


                End If
                'çÌèú

                btnControl = New Button
                btnControl.Text = "çÌèú"
                btnControl.Enabled = True

                If Not blnBtn Then
                    btnControl.Enabled = False
                End If

                btnControl.Style.Add("margin-left", "3px")
                grdBody.Rows(intRow).Cells(2).Controls.Add(btnControl)

                MeisaiID.Add(intRow, strUniqueID & hidControl.UniqueID & "|" & hidTime.UniqueID)

                btnControl.Attributes.Add("onclick", "if (confirm('ÉfÅ[É^ÇçÌèúÇµÇ‹Ç∑ÅBÇÊÇÎÇµÇ¢Ç≈Ç∑Ç©ÅH')){fncDisable();return fncSetRowData('çÌèú','" & _
                                                        arrRowId(0) & "','" & _
                                                        arrRowId(1) & "','" & _
                                                        arrRowId(2) & "','" & _
                                                        arrRowId(3) & "');}else{return false;}")
            End If
        Next
        ViewState("MeisaiID") = MeisaiID
    End Function

    ''' <summary>JavascriptçÏê¨</summary>
    Private Sub MakeScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "GetScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            
            .AppendLine("function  fncButtonChange(obj,strBtn,strHid)")
            .AppendLine("{")
            If blnBtn Then
                .AppendLine("if (obj.tagName=='SELECT'){")
                .AppendLine("strBtn.disabled = false;")
                .AppendLine("strHid.value = false;")
                .AppendLine("}else{")
                .AppendLine("if (obj.value.replace(/,/g, '')!=obj.defaultValue.replace(/,/g,'')){")
                .AppendLine("strBtn.disabled = false;")
                .AppendLine("strHid.value = false;")
                .AppendLine("}")
                .AppendLine("}")
            End If

            .AppendLine("}")
            .AppendLine("function  fncBtnChange(strBtn)")
            .AppendLine("{")
            If blnBtn Then
                .AppendLine("strBtn.focus()")

            End If

            .AppendLine("}")
            .AppendLine("function  fncSetRowData(strBtn,strNo,strKoj,strUPDTime,strRow)")
            .AppendLine("{")
            .AppendLine("if (strBtn=='ìoò^'){")
            .AppendLine("document.getElementById ('" & hidTop.ClientID & "').value=strRow*60;")
            .AppendLine("}")
            .AppendLine("document.getElementById ('" & hidBtn.ClientID & "').value=strBtn;")
            .AppendLine("document.getElementById ('" & hidNo.ClientID & "').value=eval('document.all.'+strNo).value;")
            .AppendLine("document.getElementById ('" & hidKoj.ClientID & "').value=eval('document.all.'+strKoj).value;")

            .AppendLine("document.getElementById ('" & hidUPDTime.ClientID & "').value=strUPDTime;")
            .AppendLine("document.getElementById ('" & hidRowIndex.ClientID & "').value=strRow;")
            .AppendLine("document.getElementById ('" & btn.ClientID & "').click();")

            .AppendLine("return false;")
            .AppendLine("}")
            .AppendLine("function ShowModal()")
            .AppendLine("{")
            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")

            .AppendLine("   var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("   if(buyDiv.style.display=='none')")
            .AppendLine("   {")
            .AppendLine("    buyDiv.style.display='';")
            .AppendLine("    disable.style.display='';")
            .AppendLine("    disable.focus();")
            .AppendLine("   }else{")
            .AppendLine("    buyDiv.style.display='none';")
            .AppendLine("    disable.style.display='none';")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("function itemclear()")
            .AppendLine("{")
            .AppendLine(" document.getElementById('" & Me.ddlKoj.ClientID & "').value='';")

            .AppendLine("return false;")
            .AppendLine("}")
            .AppendLine("function closecover()")
            .AppendLine("{")
            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("   var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("    buyDiv.style.display='none';")
            .AppendLine("    disable.style.display='none';")

            .AppendLine("}")
            .AppendLine("function  fncDisable()")
            .AppendLine("{")
            .AppendLine("ShowModal();")

            .AppendLine("}")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

    Protected Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim commoncheck As New CommonCheck
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.KairyKojSyubetuLogic

        Dim dtHaita As New DataTable
        Dim strErr As String = ""
        Dim strCol As Integer = -1
        Dim dtTable As DataTable = CType(ViewState("dtTable"), DataTable)
        Dim strItem As Integer = 0
        If hidBtn.Value = "ìoò^" Then
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidNo.Value, "â¸ó«çHéñéÌï NO")
                If strErr <> "" Then
                    strCol = 0
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHankaku(hidNo.Value, "â¸ó«çHéñéÌï NO", "1")
                If strErr <> "" Then
                    strCol = 0
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidKoj.Value, "â¸ó«çHéñéÌï ")
                If strErr <> "" Then
                    strCol = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckByte(hidKoj.Value, 20, "â¸ó«çHéñéÌï ", kbn.ZENKAKU)
                If strErr <> "" Then
                    strCol = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckKinsoku(hidKoj.Value, "â¸ó«çHéñéÌï ")
                If strErr <> "" Then
                    strCol = 1
                End If
            End If
            If strErr = "" Then
                If CommonSearchLogic.SelKojSyubetuInfo(hidNo.Value).Rows.Count <> 0 Then
                    strErr = Messages.Instance.MSG2035E
                    strCol = 0
                End If
            End If

            
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"

            Else

                If CommonSearchLogic.InsKojSyubetu(hidNo.Value, hidKoj.Value, ViewState("UserId").ToString) Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "â¸ó«çHéñéÌï É}ÉXÉ^") & "');"
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(0) = hidNo.Value
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(1) = hidKoj.Value

                    dtTable.Rows.Add(dtTable.NewRow)
                    ViewState("dtTable") = dtTable
                    ViewState("UniqueID") = Nothing
                Else
                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "â¸ó«çHéñéÌï É}ÉXÉ^") & "');"

                End If
                strCol = 0
            End If
        ElseIf hidBtn.Value = "èCê≥" Then
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidKoj.Value, "â¸ó«çHéñéÌï ")
                If strErr <> "" Then
                    strCol = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckByte(hidKoj.Value, 20, "â¸ó«çHéñéÌï ", kbn.ZENKAKU)
                If strErr <> "" Then
                    strCol = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckKinsoku(hidKoj.Value, "â¸ó«çHéñéÌï ")
                If strErr <> "" Then
                    strCol = 1
                End If
            End If


            If strErr = "" Then
                dtHaita = CommonSearchLogic.SelHaita(hidNo.Value, hidUPDTime.Value)
                If dtHaita.Rows.Count <> 0 Then
                    strErr = String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "â¸ó«çHéñéÌï É}ÉXÉ^").ToString()

                End If
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"
                hidBool.Value = "False"
            Else
                Dim intRturn As Integer
                intRturn = CommonSearchLogic.UpdKojSyubetu(hidNo.Value, hidKoj.Value, ViewState("UserId").ToString)
                'ê≥èÌèIóπÇµÇΩèÍçá

                If intRturn = 1 Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "â¸ó«çHéñéÌï É}ÉXÉ^") & "');"
                    dtTable.Rows(hidRowIndex.Value).Item(1) = hidKoj.Value
                    dtTable.Rows(hidRowIndex.Value).Item(2) = CommonSearchLogic.SelKojSyubetuInfo(hidNo.Value).Rows(0).Item(2).ToString

                    hidBool.Value = "True"
                ElseIf intRturn = 2 Then
                    strErr = "alert('" & Messages.Instance.MSG2049E & "');"
                    hidBool.Value = "False"

                Else

                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "â¸ó«çHéñéÌï É}ÉXÉ^") & "');"
                    hidBool.Value = "False"

                End If
                strCol = 1
            End If
            Dim strMeisai As String()
            strMeisai = Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(hidRowIndex.Value).ToString, "|")
            'èCê≥ÇÃClientID
            strMeisai(2) = ""
            CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(hidRowIndex.Value) = Join(strMeisai, "|")
        ElseIf hidBtn.Value = "çÌèú" Then
            If strErr = "" Then
                dtHaita = CommonSearchLogic.SelHaita(hidNo.Value, hidUPDTime.Value)
                If dtHaita.Rows.Count <> 0 Then
                    strErr = String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "â¸ó«çHéñéÌï É}ÉXÉ^").ToString()

                End If
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"
            Else
                Dim intRturn As Boolean
                'ê≥èÌèIóπÇµÇΩèÍçá
                intRturn = CommonSearchLogic.DelKojSyubetu(hidNo.Value)
                If intRturn = True Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "â¸ó«çHéñéÌï É}ÉXÉ^") & "');"
                    dtTable.Rows.RemoveAt(hidRowIndex.Value)
                    Dim MeisaiID2 As New Dictionary(Of String, String)
                    Dim MeisaiID As New Dictionary(Of String, String)
                    MeisaiID = CType(ViewState("MeisaiID"), Dictionary(Of String, String))
                    MeisaiID.Remove(hidRowIndex.Value)
                    Dim intC As Integer = 0
                    Dim intC2 As Integer = 0
                    For intC = 0 To MeisaiID.Count - 1
                        If intC >= hidRowIndex.Value Then
                            intC2 = intC + 1
                        Else
                            intC2 = intC
                        End If
                        MeisaiID2.Add(intC, MeisaiID.Item(intC2))
                    Next
                    ViewState("MeisaiID") = MeisaiID2
                    
                Else

                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "â¸ó«çHéñéÌï É}ÉXÉ^") & "');"
                    hidBool.Value = "False"

                End If


            End If
        End If
        SetbodyData(False)
        SetHeadData()
        SetColWidth(intWidth, "body")
        GridViewStyle(intWidth, grdBody)
        SetColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)
        SetKakutyou(ddlKoj, "")
        'UpdatePanelB.Update()
        If strCol <> -1 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strErr & "document.getElementById('" & grdBody.Rows(hidRowIndex.Value).Cells(strCol).Controls(strItem).ClientID & "').focus();", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strErr, True)

        End If

    End Sub
    Protected Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Server.Transfer("MasterMainteMenu.aspx")
    End Sub
End Class