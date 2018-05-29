Imports Itis.Earth.BizLogic
Partial Public Class HanteiKojiSyubetuMaster
    Inherits System.Web.UI.Page
    Private blnBtn As Boolean
    Private intWidth(2) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strNinsyou As String = ""
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        'å†å¿É`ÉFÉbÉNÇ®ÇÊÇ—ê›íË
        blnBtn = commonChk.CommonNinnsyou(strUserID, "koj_gyoumu_kengen")
        ViewState("UserId") = strUserID
        If Not IsPostBack Then

            ViewState("SiyouNo") = ""
            ViewState("KojSyubetu") = ""

            ViewState("UniqueID") = Nothing
            ViewState("MeisaiID") = Nothing

            SetHeadData()
            ViewState("dtTable") = SetbodyData(True)

            SetColWidth(intWidth, "body")
            GridViewStyle(intWidth, grdBody)

            SetColWidth(intWidth, "head")
            GridViewStyle(intWidth, grdHead)
            'UpdatePanelA.Update()

            SetKakutyou(ddlKojSyubetu, "")
            '  ScriptManager.RegisterStartupScript(Me, Me.GetType(), "txtfocus", " window.setTimeout('objEBI(\'" & grdBody.Rows(grdBody.Rows.Count - 1).Cells(0).Controls(0).ClientID & "\').focus()',1); ", True)

        Else
            closecover()
        End If
        divMeisai.Attributes.Add("style", "overflow-y:auto ; height:365px;width:639px;")
        MakeScript()

        'btnSearch.Attributes.Add("onclick", "document.getElementById ('" & hidTop.ClientID & "').value =0;")
        Me.divMeisai.Attributes.Add("onscroll", "document.getElementById ('" & hidTop.ClientID & "').value =" & Me.divMeisai.ClientID & ".scrollTop;")
        btnClear.Attributes.Add("onclick", "return itemclear();")
        tbxSiyou.Attributes.Add("readonly", "true")
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

    Function SelSearch(ByVal strObjCd As String) As DataTable
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.TyuiJyouhouInquiryLogic

        SelSearch = CommonSearchLogic.GetKisoSiyouInfo(strObjCd)
    End Function
    Protected Sub btnCommonSearch(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchSiyou.Click, btnOpen.Click
        Dim strScript As String = ""
        Dim dtSyouhinTable As New DataTable
        Dim objCd As New TextBox
        Dim objMei As New TextBox


        SetHeadData()
        SetbodyData(False)

        SetColWidth(intWidth, "body")
        GridViewStyle(intWidth, grdBody)

        SetColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)

        Select Case CType(sender, Button).ID

            Case "btnSearchSiyou"
                objCd = tbxSiyouNo
                objMei = tbxSiyou
            Case "btnOpen"

                objCd = grdBody.Rows(hidRowIndex.Value).Cells(0).Controls(0)
                objMei = grdBody.Rows(hidRowIndex.Value).Cells(0).Controls(2)

        End Select
        objMei.Text = ""
        dtSyouhinTable = SelSearch(objCd.Text)

        If dtSyouhinTable.Rows.Count = 1 Then
            objCd.Text = dtSyouhinTable.Rows(0).Item(1).ToString.Trim
            objMei.Text = dtSyouhinTable.Rows(0).Item(3).ToString.Trim
            Dim Scriptmangaer1 As Web.UI.ScriptManager = Web.UI.ScriptManager.GetCurrent(Page)

            If CType(sender, Button).ID = "btnSearchSiyou" Then
                Scriptmangaer1.SetFocus(ddlKojSyubetu)
            Else
                Scriptmangaer1.SetFocus(grdBody.Rows(hidRowIndex.Value).Cells(1).Controls(0))
            End If
        Else
            If CType(sender, Button).ID = "btnSearchSiyou" Then
                strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('îªíË')+'&blnDelete=False&FormName=" & _
                            Me.Page.Form.Name & "&objCd=" & _
                            objCd.ClientID & _
                             "&objMei=" & objMei.ClientID & _
                             "&strCd='+escape(eval('document.all.'+'" & _
                             objCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                             objMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            Else
                strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('îªíË')+'&blnDelete=True&FormName=" & _
                            Me.Page.Form.Name & "&objCd=" & _
                            objCd.ClientID & _
                             "&objMei=" & objMei.ClientID & _
                             "&strCd='+escape(eval('document.all.'+'" & _
                             objCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                             objMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            End If
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strScript, True)

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

            intwidth(0) = "294px"
            intwidth(1) = "166px"
            intwidth(2) = "41px"
        Else
            'intwidth(0) = "115px"
            'intwidth(1) = "215px"
            'intwidth(2) = "91px"
        End If
    End Function
    Sub SetHeadData()
        Dim CommonLG As New CommonLogic()
        grdHead.DataSource = CommonLG.CreateHeadDataSource("äÓëbédólNO,â¸ó«çHéñéÌï NO,èàóù")
        grdHead.DataBind()
    End Sub

    Sub GridViewStyle(ByVal intwidth() As String, ByVal grd As GridView)
        Dim intCol As Integer = 0
        Dim intRow As Integer = 0

        grd.Attributes.Add("Width", "622px")
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
        Dim commoncheck As New CommonCheck
        Dim strErr As String = ""
        Dim txtObj As New TextBox
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(tbxSiyouNo.Text, "äÓëbédólNO")
            If strErr <> "" Then
                txtObj = tbxSiyouNo
            End If
        End If
        If strErr = "" Then
            strErr = commoncheck.CheckHankaku(tbxSiyouNo.Text, "äÓëbédólNO", "1")
            If strErr <> "" Then
                txtObj = tbxSiyouNo
            End If
        End If


        If strErr <> "" Then
            strErr = "alert('" & strErr & "');document.getElementById('" & txtObj.ClientID & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strErr, True)
            SetHeadData()
            SetbodyData(False, Messages.Instance.MSG2034E)
        Else
            tbxSiyou.Text = ""
            If tbxSiyouNo.Text.Trim <> "" Then
                Dim dt As New DataTable
                dt = SelSearch(tbxSiyouNo.Text)
                If dt.Rows.Count = 1 Then
                    tbxSiyou.Text = dt.Rows(0).Item(3).ToString.Trim
                End If

            End If
            ViewState("SiyouNo") = Me.tbxSiyouNo.Text
            ViewState("KojSyubetu") = Me.ddlKojSyubetu.SelectedValue
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
        '    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "txtfocus", " window.setTimeout('objEBI(\'" & grdBody.Rows(grdBody.Rows.Count - 1).Cells(0).Controls(0).ClientID & "\').focus()',1); ", True)
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

        Dim CommonSearchLogic As New Itis.Earth.BizLogic.HanteiKojiSyubetuLogic
        Dim dtTable As New DataTable
        Dim CommonLG As New CommonLogic()
        Dim intRow As Integer = 0
        Dim intColCount As Integer = 0
        If blnKousin Then
            ViewState("MeisaiID") = Nothing
            ViewState("UniqueID") = Nothing

            dtTable = CommonSearchLogic.SelHanteiKojiSyubetuInfo(ViewState("SiyouNo").ToString, ViewState("KojSyubetu").ToString)
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

                'äÓëbédólNO
                Dim txtControl1 As New TextBox
                txtControl1.Width = Unit.Pixel(80)
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl1.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(0))
                Else
                    txtControl1.Text = ""
                End If
                txtControl1.CssClass = "hissu"
                txtControl1.Attributes.Add("maxlength", "7")
                txtControl1.Style.Add("ime-mode", "disabled")
                txtControl1.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl1)
                arrRowId(0) = txtControl1.ClientID
                strUniqueID = strUniqueID & txtControl1.UniqueID & ","
                'åüçıÉ{É^Éì
                Dim btnControl As New Button
                btnControl.Text = "åüçı"
                btnControl.Attributes.Add("value2", "åüçı")
                btnControl.Attributes.Add("onclick", "fncDisable();return fncOpenPop('K','" & intRow & "','ìoò^');")
                btnControl.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(btnControl)

                'äÓëbédól
                Dim txtControl As New TextBox
                txtControl.Width = Unit.Pixel(208)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Attributes.Add("readonly", "true")

                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(1))
                Else
                    txtControl.Text = ""
                End If
                txtControl.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl)
                strUniqueID = strUniqueID & txtControl.UniqueID & ","

                'â¸ó«çHéñéÌï NO
                Dim ddlControl As New DropDownList
                SetKakutyou(ddlControl, "")
                ddlControl.Width = Unit.Pixel(196)
                ddlControl.CssClass = "hissu"
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    SetDropSelect(ddlControl, Request.Form(Split(ViewState("UniqueID").ToString, ",")(2)))
                Else
                    ddlControl.SelectedIndex = 0
                End If
                ddlControl.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(1).Controls.Add(ddlControl)
                strUniqueID = strUniqueID & ddlControl.UniqueID & ","
                arrRowId(1) = ddlControl.ClientID


                
                arrRowId(2) = ""
                arrRowId(3) = intRow
                'ìoò^
                btnControl = New Button
                btnControl.Text = "ìoò^"
                btnControl.Attributes.Add("onclick", "fncDisable();return fncSetRowData('ìoò^','" & _
                                                        arrRowId(0) & "','" & _
                                                        arrRowId(1) & "','" & _
                                                        arrRowId(2) & "','" & _
                                                        arrRowId(3) & "');")
                If Not blnBtn Then
                    btnControl.Enabled = False
                End If

                btnControl.Style.Add("margin-left", "8px")
                btnControl.Style.Add("margin-right", "8px")
                grdBody.Rows(intRow).Cells(2).Controls.Add(btnControl)
                If blnBtn Then
                    txtControl1.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    ddlControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                End If
                ViewState("UniqueID") = strUniqueID
            Else

                'â¸ó«çHéñéÌï NO
                Dim txtControl As New TextBox
                txtControl.Width = Unit.Pixel(341)
                txtControl.CssClass = "readOnlyStyle"
                'txtControl.Style.Add("border-bottom", "none;")
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
                arrRowId(0) = Split(txtControl.Text, ":")(0)
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"

                'â¸ó«çHéñéÌï NO
                txtControl = New TextBox
                txtControl.Width = Unit.Pixel(189)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Attributes.Add("readonly", "true")
                txtControl.Attributes.Add("TabIndex", "-1")
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
                txtControl.Style.Add("margin-left", "4px")
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtControl)
                arrRowId(1) = Split(txtControl.Text, ":")(0)
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"


                'èCê≥
                Dim hidControl As New HiddenField
                Dim hidTime As New HiddenField
                hidTime.Value = dtTable.Rows(intRow).Item("upd_datetime").ToString
                grdBody.Rows(intRow).Cells(2).Controls.Add(hidControl)
                grdBody.Rows(intRow).Cells(2).Controls.Add(hidTime)

                arrRowId(2) = hidTime.Value


                arrRowId(3) = intRow
               
                'çÌèú

                Dim btnControl As New Button
                btnControl.Text = "çÌèú"
                btnControl.Enabled = True

                If Not blnBtn Then
                    btnControl.Enabled = False
                End If

                btnControl.Style.Add("margin-left", "8px")
                btnControl.Style.Add("margin-right", "8px")
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

            .AppendLine("function  fncOpenPop(strBtn,strRow,strKBN)")
            .AppendLine("{")
            .AppendLine("document.getElementById ('" & hidBtn.ClientID & "').value=strBtn;")
            .AppendLine("document.getElementById ('" & hidRowIndex.ClientID & "').value=strRow;")
            .AppendLine("if (strKBN=='ìoò^'){")
            .AppendLine("document.getElementById ('" & hidTop.ClientID & "').value=strRow*60;")
            .AppendLine("}")
            .AppendLine("document.getElementById ('" & btnOpen.ClientID & "').click();")
            .AppendLine("return false;")
            .AppendLine("}")
            .AppendLine("function  fncSetRowData(strBtn,strSiyouNo,strSyubetuNo,strUPDTime,strRow)")
            .AppendLine("{")
            .AppendLine("if (strBtn=='ìoò^'){")
            .AppendLine("document.getElementById ('" & hidTop.ClientID & "').value=strRow*60;")
            .AppendLine("document.getElementById ('" & hidSiyouNo.ClientID & "').value=eval('document.all.'+strSiyouNo).value;")
            .AppendLine("document.getElementById ('" & hidSyubetuNo.ClientID & "').value=eval('document.all.'+strSyubetuNo).value;")
            .AppendLine("}else{")
            .AppendLine("document.getElementById ('" & hidSiyouNo.ClientID & "').value=strSiyouNo;")
            .AppendLine("document.getElementById ('" & hidSyubetuNo.ClientID & "').value=strSyubetuNo;")
            .AppendLine("}")
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
            .AppendLine(" document.getElementById('" & Me.ddlKojSyubetu.ClientID & "').value='';")
            .AppendLine(" document.getElementById('" & Me.tbxSiyouNo.ClientID & "').value='';")
            .AppendLine(" document.getElementById('" & Me.tbxSiyou.ClientID & "').value='';")
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
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.HanteiKojiSyubetuLogic
        Dim CommonSearchLogic2 As New Itis.Earth.BizLogic.KairyKojSyubetuLogic
        Dim dtHaita As New DataTable
        Dim strErr As String = ""
        Dim strCol As Integer = -1
        Dim dtTable As DataTable = CType(ViewState("dtTable"), DataTable)
        Dim strItem As Integer = 0
        If hidBtn.Value = "ìoò^" Then
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidSiyouNo.Value, "äÓëbédólNO")
                If strErr <> "" Then
                    strCol = 0
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHankaku(hidSiyouNo.Value, "äÓëbédólNO", "1")
                If strErr <> "" Then
                    strCol = 0
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidSyubetuNo.Value, "â¸ó«çHéñéÌï NO")
                If strErr <> "" Then
                    strCol = 1
                End If
            End If
            
            If strErr = "" Then
                If SelSearch(hidSiyouNo.Value).Rows.Count = 0 Then
                    strErr = String.Format(Messages.Instance.MSG2036E, "äÓëbédólÉ}ÉXÉ^").ToString
                    strCol = 0
                End If
            End If
            If strErr = "" Then
                If CommonSearchLogic2.SelKojSyubetuInfo(hidSyubetuNo.Value).Rows.Count = 0 Then
                    strErr = String.Format(Messages.Instance.MSG2036E, "â¸ó«çHéñéÌï É}ÉXÉ^").ToString
                    strCol = 0
                End If
            End If
            If strErr = "" Then
                If Not CommonSearchLogic.SelJyuufuku(hidSiyouNo.Value, hidSyubetuNo.Value) Then
                    strErr = Messages.Instance.MSG2035E
                    strCol = 0
                    strItem = 0
                End If
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"

            Else

                If CommonSearchLogic.InsSeikyuuSaki(hidSiyouNo.Value, hidSyubetuNo.Value, ViewState("UserId").ToString) Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "îªíËçHéñéÌï ê›íËÉ}ÉXÉ^") & "');"
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(0) = hidSiyouNo.Value & ":" & TrimNull(SelSearch(hidSiyouNo.Value).Rows(0).Item(3).ToString)
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(1) = hidSyubetuNo.Value & ":" & TrimNull(CommonSearchLogic2.SelKojSyubetuInfo(hidSyubetuNo.Value).Rows(0).Item(1).ToString)

                    dtTable.Rows.Add(dtTable.NewRow)
                    ViewState("dtTable") = dtTable
                    ViewState("UniqueID") = Nothing
                Else
                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "îªíËçHéñéÌï ê›íËÉ}ÉXÉ^") & "');"

                End If
            End If
        ElseIf hidBtn.Value = "çÌèú" Then
            If strErr = "" Then
                dtHaita = CommonSearchLogic.SelHaita(hidSiyouNo.Value, hidSyubetuNo.Value, hidUPDTime.Value)
                If dtHaita.Rows.Count <> 0 Then
                    strErr = String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "îªíËçHéñéÌï ê›íËÉ}ÉXÉ^").ToString()

                End If
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"
            Else
                Dim intRturn As Boolean
                'ê≥èÌèIóπÇµÇΩèÍçá
                intRturn = CommonSearchLogic.DelHanteiKojiSyubetu(hidSiyouNo.Value, hidSyubetuNo.Value)
                If intRturn = True Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "îªíËçHéñéÌï ê›íËÉ}ÉXÉ^") & "');"
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

                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "îªíËçHéñéÌï ê›íËÉ}ÉXÉ^") & "');"
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