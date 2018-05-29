Imports Itis.Earth.BizLogic
Partial Public Class TantouSyaMaster
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

            ViewState("SyainCd") = ""
            ViewState("SyainMei") = ""
            ViewState("KBN") = ""

            ViewState("UniqueID") = Nothing
            ViewState("MeisaiID") = Nothing

            SetHeadData()
            ViewState("dtTable") = SetbodyData(True)

            SetColWidth(intWidth, "body")
            GridViewStyle(intWidth, grdBody)

            SetColWidth(intWidth, "head")
            GridViewStyle(intWidth, grdHead)
            'UpdatePanelA.Update()

            SetKakutyou(ddlKBN, "")
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "txtfocus", " window.setTimeout('objEBI(\'" & grdBody.Rows(grdBody.Rows.Count - 1).Cells(0).Controls(0).ClientID & "\').focus()',1); ", True)

        Else
            closecover()
        End If
        divMeisai.Attributes.Add("style", "overflow-y:auto ; height:365px;width:577px;")
        MakeScript()

        btnSearch.Attributes.Add("onclick", "document.getElementById ('" & hidTop.ClientID & "').value =0;")
        Me.divMeisai.Attributes.Add("onscroll", "document.getElementById ('" & hidTop.ClientID & "').value =" & Me.divMeisai.ClientID & ".scrollTop;")
        btnClear.Attributes.Add("onclick", "return itemclear();")

    End Sub
    Sub SetKakutyou(ByVal ddl As DropDownList, ByVal strSyubetu As String)

        Dim ddlist As New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddl.Items.Add(ddlist)


        ddlist = New ListItem
        ddlist.Text = "ï\é¶"
        ddlist.Value = "0"
        ddl.Items.Add(ddlist)

        ddlist = New ListItem
        ddlist.Text = "îÒï\é¶"
        ddlist.Value = "1"
        ddl.Items.Add(ddlist)

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
    Function SelSearch(ByVal strNO As String) As DataTable
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.TantousyaLogic

        SelSearch = CommonSearchLogic.SelSimeiInfo(strNO)
    End Function
    ''' <summary>GridViewï¿Ç◊ÇÈïùÇÃê›íË</summary>
    Function SetColWidth(ByRef intwidth() As String, ByVal strName As String) As Integer
        If strName = "head" Then
            intwidth(0) = "143px"
            intwidth(1) = "192px"
            intwidth(2) = "103px"
            'intwidth(3) = "41px"
        Else
            'intwidth(0) = "115px"
            'intwidth(1) = "215px"
            'intwidth(2) = "91px"
        End If
    End Function
    Sub SetHeadData()
        Dim CommonLG As New CommonLogic()
        grdHead.DataSource = CommonLG.CreateHeadDataSource("íSìñé“ÉRÅ[Éh,íSìñé“ñº,ï\é¶ãÊï™,èàóù")
        grdHead.DataBind()
    End Sub
    Sub GridViewStyle(ByVal intwidth() As String, ByVal grd As GridView)
        Dim intCol As Integer = 0
        Dim intRow As Integer = 0

        grd.Attributes.Add("Width", "560px")
        If IsArray(intwidth) AndAlso grd.Rows.Count > 0 Then
            For intRow = 0 To grd.Rows.Count - 1
                For intCol = 0 To grd.Rows(intRow).Cells.Count - 1
                    grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")
                    If "grdBody" = grd.ID And intCol = 0 Then
                        grd.Rows(intRow).Cells(intCol).Attributes.Add("align", "left")
                    Else
                        grd.Rows(intRow).Cells(intCol).Attributes.Add("align", "center")

                    End If

                Next
            Next
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim commoncheck As New CommonCheck
        Dim strErr As String = ""
        Dim txtObj As New TextBox
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(Me.tbxSyainCd.Text & Me.tbxSyainMei.Text & ddlKBN.SelectedValue, "")
            If strErr <> "" Then
                txtObj = tbxSyainCd
                strErr = Messages.Instance.MSG2037E
            End If
        End If
        If strErr = "" And tbxSyainCd.Text <> "" Then
            strErr = commoncheck.CheckHankaku(tbxSyainCd.Text, "íSìñé“ÉRÅ[Éh", "1")
            If strErr <> "" Then
                txtObj = tbxSyainCd
            End If
        End If

        If strErr = "" And tbxSyainMei.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxSyainMei.Text, 30, "íSìñé“ñº", kbn.ZENKAKU)
            If strErr <> "" Then
                txtObj = tbxSyainMei
            End If

        End If

        If strErr = "" And tbxSyainMei.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxSyainMei.Text, "íSìñé“ñº")
            If strErr <> "" Then
                txtObj = tbxSyainMei
            End If
        End If

        If strErr <> "" Then
            strErr = "alert('" & strErr & "');document.getElementById('" & txtObj.ClientID & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strErr, True)
            SetHeadData()
            SetbodyData(False, Messages.Instance.MSG2034E)
        Else
            ViewState("SyainCd") = tbxSyainCd.Text
            ViewState("SyainMei") = tbxSyainMei.Text
            ViewState("KBN") = ddlKBN.SelectedValue

            SetHeadData()
            ViewState("dtTable") = SetbodyData(True, Messages.Instance.MSG2034E)
        End If
        SetColWidth(intWidth, "body")
        GridViewStyle(intWidth, grdBody)

        SetColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)


        'If strErr = "" Then
        '    Dim Scriptmangaer1 As Web.UI.ScriptManager = Web.UI.ScriptManager.GetCurrent(Page)
        '    Scriptmangaer1.SetFocus(grdBody.Rows(grdBody.Rows.Count - 1).Cells(0).Controls(0))

        '    '           ScriptManager.RegisterStartupScript(Me, Me.GetType(), "txtfocus", " window.setTimeout('objEBI(\'" & grdBody.Rows(grdBody.Rows.Count - 1).Cells(0).Controls(0).ClientID & "\').focus()',1); ", True)
        'End If


    End Sub
    Sub SetDropSelect(ByVal ddl As DropDownList, ByVal strValue As String)
        If ddl.Items.FindByValue(strValue) IsNot Nothing Then
            ddl.SelectedValue = strValue
        Else
            ddl.SelectedIndex = 0
        End If
    End Sub
    ''' <summary> GridViewBODYïîÇÉZÉbÉg</summary>
    Function SetbodyData(ByVal blnKousin As Boolean, Optional ByVal msg As String = "") As DataTable

        Dim CommonSearchLogic As New Itis.Earth.BizLogic.TantousyaLogic
        Dim dtTable As New DataTable
        Dim CommonLG As New CommonLogic()
        Dim intRow As Integer = 0
        Dim intColCount As Integer = 0
        If blnKousin Then
            ViewState("MeisaiID") = Nothing
            ViewState("UniqueID") = Nothing

            dtTable = CommonSearchLogic.SelTantouInfo(ViewState("SyainCd"), ViewState("SyainMei"), ViewState("KBN"), "like")
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
            Dim arrRowId(4) As String
            strUniqueID = ""
            If intRow = grdBody.Rows.Count - 1 Then

                'íSìñé“ÉRÅ[Éh
                Dim txtControl As New TextBox
                txtControl.Width = Unit.Pixel(50)
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(0))
                Else

                    txtControl.Text = ""
                End If
                txtControl.CssClass = "hissu"
                txtControl.Attributes.Add("maxlength", "5")
                txtControl.Style.Add("ime-mode", "disabled")
                txtControl.Style.Add("margin-left", "5px")
                txtControl.Style.Add("text-align", "left;")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl)
                arrRowId(0) = txtControl.ClientID
                strUniqueID = strUniqueID & txtControl.UniqueID & ","

                'åüçıÉ{É^Éì
                Dim btnControl As New Button
                btnControl.Text = "éÅñºéÊìæ"
                btnControl.Attributes.Add("value2", "åüçı")
                btnControl.Attributes.Add("onclick", "fncDisable();return fncOpenPop('" & intRow & "','ìoò^');")
                btnControl.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(btnControl)

                'íSìñé“ñº
                Dim txtControl1 As New TextBox
                txtControl1.Width = Unit.Pixel(180)
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl1.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(1))
                Else
                    txtControl1.Text = ""
                End If
                txtControl1.CssClass = "hissu"
                txtControl1.Attributes.Add("maxlength", "30")
                txtControl1.Style.Add("margin-left", "3px")
                txtControl1.Style.Add("margin-right", "3px")
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtControl1)
                arrRowId(1) = txtControl1.ClientID
                strUniqueID = strUniqueID & txtControl1.UniqueID & ","

                'ï\é¶ãÊï™
                Dim ddlControl As New DropDownList
                SetKakutyou(ddlControl, "")
                ddlControl.Width = Unit.Pixel(100)
                ddlControl.CssClass = "hissu"
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    SetDropSelect(ddlControl, Request.Form(Split(ViewState("UniqueID").ToString, ",")(2)))
                Else
                    ddlControl.SelectedIndex = 0
                End If
                ddlControl.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(2).Controls.Add(ddlControl)
                strUniqueID = strUniqueID & ddlControl.UniqueID & ","
                arrRowId(2) = ddlControl.ClientID



                arrRowId(3) = ""
                arrRowId(4) = intRow

                'ìoò^
                Dim btnControlT As New Button
                btnControlT.Text = "ìoò^"
                btnControlT.Attributes.Add("onclick", "fncDisable();return fncSetRowData('ìoò^','" & _
                                                        arrRowId(0) & "','" & _
                                                        arrRowId(1) & "','" & _
                                                        arrRowId(2) & "','" & _
                                                        arrRowId(3) & "','" & _
                                                        arrRowId(4) & "');")
                If Not blnBtn Then
                    btnControlT.Enabled = False
                End If

                btnControlT.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(btnControlT)
                If blnBtn Then
                    txtControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControlT.ClientID & ".click();return false;}")
                    txtControl1.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControlT.ClientID & ".click();return false;}")
                    ddlControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControlT.ClientID & ".click();return false;}")

                End If
                'Lable
                Dim txt As New TextBox
                txt.Style.Add("border-style", "none;")
                txt.Style.Add("background-color", "transparent;")
                txt.Attributes.Add("TabIndex", "-1")
                txt.ReadOnly = True
                txt.Width = Unit.Pixel(47)
                grdBody.Rows(intRow).Cells(3).Controls.Add(txt)
                ViewState("UniqueID") = strUniqueID

            Else

                'íSìñé“ÉRÅ[Éh
                Dim txtControl As New TextBox
                txtControl.Width = Unit.Pixel(50)
                txtControl.CssClass = "readOnlyStyle"
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
                txtControl.Style.Add("margin-left", "5px;")
                txtControl.Style.Add("text-align", "left;")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl)
                arrRowId(0) = txtControl.ClientID
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"

                'íSìñé“ñº
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
                txtControl.Attributes.Add("maxlength", "30")
                txtControl.Style.Add("margin-left", "3px")
                txtControl.Style.Add("margin-right", "3px")
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtControl)
                arrRowId(1) = txtControl.ClientID
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"

                'ï\é¶ãÊï™
                Dim ddlControl As New DropDownList
                SetKakutyou(ddlControl, "")
                ddlControl.Width = Unit.Pixel(100)
                ddlControl.CssClass = "hissu"
        
                If ViewState("MeisaiID") Is Nothing Then
                    SetDropSelect(ddlControl, dtTable.Rows(intRow).Item(2))

                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then

                        SetDropSelect(ddlControl, Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(2)))
                    Else
                        SetDropSelect(ddlControl, dtTable.Rows(intRow).Item(2))
                    End If
                End If
                ddlControl.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(2).Controls.Add(ddlControl)
                strUniqueID = strUniqueID & ddlControl.UniqueID & "|"
                arrRowId(2) = ddlControl.ClientID

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
                        If Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(3) = "" Then
                            btnControl.Enabled = Not CBool(hidBool.Value)
                        Else
                            btnControl.Enabled = Not CBool(Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(3)))
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

                btnControl.Style.Add("margin-left", "5px")
                hidTime.Value = dtTable.Rows(intRow).Item("upd_datetime").ToString
                grdBody.Rows(intRow).Cells(3).Controls.Add(btnControl)
                grdBody.Rows(intRow).Cells(3).Controls.Add(hidControl)
                grdBody.Rows(intRow).Cells(3).Controls.Add(hidTime)

                arrRowId(3) = hidTime.Value


                arrRowId(4) = intRow
                btnControl.Attributes.Add("onclick", "fncDisable();" & hidControl.ClientID & ".value=" & btnControl.ClientID & ".disabled;return fncSetRowData('èCê≥','" & _
                                                                        arrRowId(0) & "','" & _
                                                                        arrRowId(1) & "','" & _
                                                                        arrRowId(2) & "','" & _
                                                                        arrRowId(3) & "','" & _
                                                                        arrRowId(4) & "');")

                If blnBtn Then
                    txtControl.Attributes.Add("onPropertyChange", "fncButtonChange(this," & btnControl.ClientID & "," & hidControl.ClientID & ");")
                    ddlControl.Attributes.Add("onPropertyChange", "fncButtonChange(this," & btnControl.ClientID & "," & hidControl.ClientID & ");")

                    txtControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    ddlControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")

                End If
                'çÌèú

                btnControl = New Button
                btnControl.Text = "çÌèú"
                btnControl.Enabled = True

                If Not blnBtn Then
                    btnControl.Enabled = False
                End If

                btnControl.Style.Add("margin-left", "5px")
                btnControl.Style.Add("margin-right", "5px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(btnControl)

                MeisaiID.Add(intRow, strUniqueID & hidControl.UniqueID & "|" & hidTime.UniqueID)

                btnControl.Attributes.Add("onclick", "if (confirm('ÉfÅ[É^ÇçÌèúÇµÇ‹Ç∑ÅBÇÊÇÎÇµÇ¢Ç≈Ç∑Ç©ÅH')){fncDisable();return fncSetRowData('çÌèú','" & _
                                                        arrRowId(0) & "','" & _
                                                        arrRowId(1) & "','" & _
                                                        arrRowId(2) & "','" & _
                                                        arrRowId(3) & "','" & _
                                                        arrRowId(4) & "');}else{return false;}")
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
            .AppendLine("function  fncOpenPop(strRow,strKBN)")
            .AppendLine("{")
            .AppendLine("document.getElementById ('" & hidRowIndex.ClientID & "').value=strRow;")
            .AppendLine("document.getElementById ('" & hidTop.ClientID & "').value=strRow*60;")
            .AppendLine("document.getElementById ('" & btnOpen.ClientID & "').click();")
            .AppendLine("return false;")
            .AppendLine("}")
            .AppendLine("function  fncSetRowData(strBtn,strNo,strMei,strKBN,strUPDTime,strRow)")
            .AppendLine("{")
            .AppendLine("if (strBtn=='ìoò^'){")
            .AppendLine("document.getElementById ('" & hidTop.ClientID & "').value=strRow*60;")
            .AppendLine("}")
            .AppendLine("document.getElementById ('" & hidNo.ClientID & "').value=eval('document.all.'+strNo).value;")
            .AppendLine("document.getElementById ('" & hidMei.ClientID & "').value=eval('document.all.'+strMei).value;")
            .AppendLine("document.getElementById ('" & hidKBN.ClientID & "').value=eval('document.all.'+strKBN).value;")
            .AppendLine("document.getElementById ('" & hidBtn.ClientID & "').value=strBtn;")


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
            .AppendLine(" document.getElementById('" & Me.ddlKBN.ClientID & "').value='';")
            .AppendLine(" document.getElementById('" & Me.tbxSyainCd.ClientID & "').value='';")
            .AppendLine(" document.getElementById('" & Me.tbxSyainMei.ClientID & "').value='';")
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
    Protected Sub btnCommonSearch(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click

        Dim dtTable As New DataTable
        Dim objCd As New TextBox
        Dim objMei As New TextBox

        SetHeadData()
        SetbodyData(False)

        SetColWidth(intWidth, "body")
        GridViewStyle(intWidth, grdBody)

        SetColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)


        objCd = grdBody.Rows(hidRowIndex.Value).Cells(0).Controls(0)
        objMei = grdBody.Rows(hidRowIndex.Value).Cells(1).Controls(0)
        objMei.Text = ""
        dtTable = SelSearch(objCd.Text)
        If dtTable.Rows.Count = 1 Then
            objMei.Text = dtTable.Rows(0).Item(0).ToString.Trim
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;", True)
            Dim Scriptmangaer1 As Web.UI.ScriptManager = Web.UI.ScriptManager.GetCurrent(Page)
            Scriptmangaer1.SetFocus(grdBody.Rows(hidRowIndex.Value).Cells(1).Controls(0))
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;alert('ÉAÉJÉEÉìÉgÉ}ÉXÉ^Å[Ç…" & Messages.Instance.MSG020E & "');" & " window.setTimeout('objEBI(\'" & objCd.ClientID & "\').focus()',1);", True)
        End If

    End Sub


    Protected Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim commoncheck As New CommonCheck
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.TantousyaLogic

        Dim dtHaita As New DataTable
        Dim strErr As String = ""
        Dim strCol As Integer = -1
        Dim dtTable As DataTable = CType(ViewState("dtTable"), DataTable)
        Dim strItem As Integer = 0
        If hidBtn.Value = "ìoò^" Then
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidNo.Value, "íSìñé“ÉRÅ[Éh")
                If strErr <> "" Then
                    strCol = 0
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHankaku(hidNo.Value, "íSìñé“ÉRÅ[Éh", "1")
                If strErr <> "" Then
                    strCol = 0
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidMei.Value, "íSìñé“ñº")
                If strErr <> "" Then
                    strCol = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckByte(hidMei.Value, 30, "íSìñé“ñº", kbn.ZENKAKU)
                If strErr <> "" Then
                    strCol = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckKinsoku(hidMei.Value, "íSìñé“ñº")
                If strErr <> "" Then
                    strCol = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidKBN.Value, "ï\é¶ãÊï™")
                If strErr <> "" Then
                    strCol = 2
                End If
            End If
            If strErr = "" Then
                If SelSearch(hidNo.Value).Rows.Count = 0 Then
                    strErr = String.Format(Messages.Instance.MSG2036E, "ÉAÉJÉEÉìÉgÉ}ÉXÉ^").ToString
                    strCol = 0
                End If
            End If
            
            If strErr = "" Then
                If Not CommonSearchLogic.SelJyuufuku(hidNo.Value) Then
                    strErr = Messages.Instance.MSG2035E
                    strCol = 0
                    strItem = 0
                End If
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"

            Else

                If CommonSearchLogic.InsTantousya(hidNo.Value, hidMei.Value, hidKBN.Value, ViewState("UserId").ToString) Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "íSìñé“É}ÉXÉ^") & "');"
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(0) = hidNo.Value
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(1) = hidMei.Value
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(2) = hidKBN.Value

                    dtTable.Rows.Add(dtTable.NewRow)
                    ViewState("dtTable") = dtTable
                    ViewState("UniqueID") = Nothing
                Else
                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "íSìñé“É}ÉXÉ^") & "');"

                End If
            End If
        ElseIf hidBtn.Value = "èCê≥" Then
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidMei.Value, "íSìñé“ñº")
                If strErr <> "" Then
                    strCol = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckByte(hidMei.Value, 30, "íSìñé“ñº", kbn.ZENKAKU)
                If strErr <> "" Then
                    strCol = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckKinsoku(hidMei.Value, "íSìñé“ñº")
                If strErr <> "" Then
                    strCol = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidKBN.Value, "ï\é¶ãÊï™")
                If strErr <> "" Then
                    strCol = 2
                End If
            End If


            If strErr = "" Then
                dtHaita = CommonSearchLogic.SelHaita(hidNo.Value, hidUPDTime.Value)
                If dtHaita.Rows.Count <> 0 Then
                    strErr = String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "íSìñé“É}ÉXÉ^").ToString()

                End If
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"
                hidBool.Value = "False"
            Else
                Dim intRturn As Integer
                intRturn = CommonSearchLogic.UpdTantou(hidNo.Value, hidMei.Value, hidKBN.Value, ViewState("UserId").ToString)
                'ê≥èÌèIóπÇµÇΩèÍçá

                If intRturn = 1 Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "íSìñé“É}ÉXÉ^") & "');"
                    dtTable.Rows(hidRowIndex.Value).Item(1) = hidMei.Value
                    dtTable.Rows(hidRowIndex.Value).Item(2) = hidKBN.Value
                    dtTable.Rows(hidRowIndex.Value).Item(3) = CommonSearchLogic.SelTantouInfo(hidNo.Value).Rows(0).Item(3).ToString

                    hidBool.Value = "True"
                ElseIf intRturn = 2 Then
                    strErr = "alert('" & Messages.Instance.MSG2049E & "');"
                    hidBool.Value = "False"

                Else

                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "íSìñé“É}ÉXÉ^") & "');"
                    hidBool.Value = "False"

                End If
                strCol = 1
            End If
            Dim strMeisai As String()
            strMeisai = Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(hidRowIndex.Value).ToString, "|")
            'èCê≥ÇÃClientID
            strMeisai(3) = ""
            CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(hidRowIndex.Value) = Join(strMeisai, "|")

        ElseIf hidBtn.Value = "çÌèú" Then
            If strErr = "" Then
                dtHaita = CommonSearchLogic.SelHaita(hidNo.Value, hidUPDTime.Value)
                If dtHaita.Rows.Count <> 0 Then
                    strErr = String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "íSìñé“É}ÉXÉ^").ToString()

                End If
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"
            Else
                Dim intRturn As Boolean
                'ê≥èÌèIóπÇµÇΩèÍçá
                intRturn = CommonSearchLogic.DelTantou(hidNo.Value)
                If intRturn = True Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "íSìñé“É}ÉXÉ^") & "');"
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

                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "íSìñé“É}ÉXÉ^") & "');"
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