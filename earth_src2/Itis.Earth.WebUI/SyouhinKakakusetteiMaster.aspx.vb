Imports Itis.Earth.BizLogic
Partial Public Class SyouhinKakakusetteiMaster
    Inherits System.Web.UI.Page
    Private blnBtn As Boolean
    '========================2011/04/26 �ԗ� �C�� �J�n��============================
    Private intWidth(5) As Integer
    '========================2011/04/26 �ԗ� �C�� �I����============================

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strNinsyou As String = ""
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        '�����`�F�b�N����ѐݒ�
        blnBtn = commonChk.CommonNinnsyou(strUserID, "kkk_master_kanri_kengen")
        ViewState("UserId") = strUserID
        If Not IsPostBack Then

            ViewState("Kbn") = ""
            ViewState("Houhou") = ""
            ViewState("Gaiyou") = ""
            '========================2011/04/26 �ԗ� �ǉ� �J�n��============================
            '����i�R�[�h�
            ViewState("Syouhin") = ""
            '========================2011/04/26 �ԗ� �ǉ� �I����============================

            ViewState("UniqueID") = Nothing
            ViewState("MeisaiID") = Nothing

            SetHeadData()
            ViewState("dtTable") = SetbodyData(True)
            '========================2011/04/26 �ԗ� �C�� �J�n��============================
            'SetColWidth(intWidth, "body")
            'GridViewStyle(intWidth, grdBody)

            'SetColWidth(intWidth, "head")
            'GridViewStyle(intWidth, grdHead)

            SetColWidth(intWidth, "head")
            GridViewStyle(intWidth, grdHead)
            GridViewStyle(intWidth, grdBody)
            '========================2011/04/26 �ԗ� �C�� �I����============================
            UpdatePanelA.Update()

            SetKakutyou(ddlSyouhinKBN, "01", False)
            SetKakutyou(ddlTysHouhou, "", , "m_tyousahouhou", "meisyou2")
            SetKakutyou(ddlTysGaiyou, "02", True)
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "txtfocus", " window.setTimeout('objEBI(\'" & grdBody.Rows(grdBody.Rows.Count - 1).Cells(0).Controls(0).ClientID & "\').focus()',500); ", True)

        Else
            closecover()
        End If
        '========================2011/04/26 �ԗ� �C�� �J�n��============================
        divMeisai.Attributes.Add("style", "overflow-y:auto ; height:297px;width:927px;")
        '========================2011/04/26 �ԗ� �C�� �I����============================
        MakeScript()

        btnSearch.Attributes.Add("onclick", "document.getElementById ('" & hidTop.ClientID & "').value =0;")
        Me.divMeisai.Attributes.Add("onscroll", "document.getElementById ('" & hidTop.ClientID & "').value =" & Me.divMeisai.ClientID & ".scrollTop;")
        btnClear.Attributes.Add("onclick", "return itemclear();")
    End Sub
    ''' <summary>Javascript�쐬</summary>
    Private Sub MakeScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "GetScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("function  fncOpenPop(strBtn,strRow)")
            .AppendLine("{")
            .AppendLine("document.getElementById ('" & hidRowIndex.ClientID & "').value=strRow;")
            .AppendLine("if (strBtn=='�o�^'){")
            .AppendLine("document.getElementById ('" & hidTop.ClientID & "').value=strRow*60;")
            .AppendLine("}")
            .AppendLine("document.getElementById ('" & btnOpen.ClientID & "').click();")
            .AppendLine("return false;")
            .AppendLine("}")
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
            .AppendLine("function  fncSetRowData(strBtn,strSyouhinKBN,strTysHouhou,strTysGaiyou,strSyouhinCd,strSetteiBasyo,strUPDTime,strRow)")
            .AppendLine("{")

            .AppendLine("document.getElementById ('" & hidBtn.ClientID & "').value=strBtn;")

            .AppendLine("if (strBtn=='�o�^'){")
            .AppendLine("document.getElementById ('" & hidSyouhinKBN.ClientID & "').value=eval('document.all.'+strSyouhinKBN).value +','+eval('document.all.'+strSyouhinKBN).options(eval('document.all.'+strSyouhinKBN).selectedIndex).text;")
            .AppendLine("document.getElementById ('" & hidTysHouhou.ClientID & "').value=eval('document.all.'+strTysHouhou).options(eval('document.all.'+strTysHouhou).selectedIndex).text;")
            .AppendLine("document.getElementById ('" & hidTysGaiyou.ClientID & "').value=eval('document.all.'+strTysGaiyou).value +','+eval('document.all.'+strTysGaiyou).options(eval('document.all.'+strTysGaiyou).selectedIndex).text;")
            .AppendLine("document.getElementById ('" & hidTop.ClientID & "').value=strRow*60;")
            .AppendLine("}else{")
            .AppendLine("document.getElementById ('" & hidSyouhinKBN.ClientID & "').value=strSyouhinKBN;")
            .AppendLine("document.getElementById ('" & hidTysHouhou.ClientID & "').value=strTysHouhou;")
            .AppendLine("document.getElementById ('" & hidTysGaiyou.ClientID & "').value=strTysGaiyou;")
            .AppendLine("}")
            .AppendLine("document.getElementById ('" & hidSyouhinCd.ClientID & "').value=eval('document.all.'+strSyouhinCd).value;")
            .AppendLine("document.getElementById ('" & hidSetteiBasyo.ClientID & "').value=eval('document.all.'+strSetteiBasyo).value;")
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
            .AppendLine(" document.getElementById('" & Me.ddlSyouhinKBN.ClientID & "').value='';")
            .AppendLine(" document.getElementById('" & Me.ddlTysHouhou.ClientID & "').value='';")
            .AppendLine(" document.getElementById('" & Me.ddlTysGaiyou.ClientID & "').value='';")
            '========================2011/04/26 �ԗ� �ǉ� �J�n��============================
            '����i�R�[�h�
            .AppendLine(" document.getElementById('" & Me.tbxSyouhinCd.ClientID & "').value='';")
            '����i���
            .AppendLine(" document.getElementById('" & Me.tbxSyouhinMei.ClientID & "').value='';")
            '========================2011/04/26 �ԗ� �ǉ� �I����============================
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
    Sub SetKakutyou(ByVal ddl As DropDownList, ByVal strSyubetu As String, Optional ByVal strShowCd As Boolean = True, Optional ByVal strTable As String = "", Optional ByVal strItem As String = "")
        Dim SyouhinSearchLogic As New Itis.Earth.BizLogic.KakakusetteiLogic
        Dim dtTable As New DataTable
        Dim intCount As Integer = 0
        dtTable = SyouhinSearchLogic.SelKakutyouInfo(strSyubetu, strTable)

        Dim ddlist As New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddl.Items.Add(ddlist)

        For intCount = 0 To dtTable.Rows.Count - 1
            ddlist = New ListItem
            If strShowCd Then
                If strItem <> "" Then
                    ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("code")) & ":" & TrimNull(dtTable.Rows(intCount).Item(strItem))
                Else
                    ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("code")) & ":" & TrimNull(dtTable.Rows(intCount).Item("meisyou"))
                End If
            Else
                If strItem <> "" Then
                    ddlist.Text = TrimNull(dtTable.Rows(intCount).Item(strItem))
                Else
                    ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("meisyou"))
                End If

            End If
            ddlist.Value = TrimNull(dtTable.Rows(intCount).Item("code"))
            ddl.Items.Add(ddlist)
        Next


    End Sub
    ''' <summary>�󔒂��폜</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function
    Protected Sub btnCommonSearch(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        Dim strScript As String = ""
        Dim dtSyouhinTable As New DataTable
        Dim objCd As New TextBox
        Dim objCd2 As New TextBox
        Dim objCd3 As New DropDownList
        Dim objMei As New TextBox
        Dim kbn As String = ""

        '========================2011/04/26 �ԗ� �ǉ� �J�n��============================
        '���i����ݒ肷��
        SetSyouhinMei()
        '========================2011/04/26 �ԗ� �ǉ� �I����============================

        SetHeadData()
        SetbodyData(False)

        '========================2011/04/26 �ԗ� �C�� �J�n��============================
        'SetColWidth(intWidth, "body")
        'GridViewStyle(intWidth, grdBody)

        'SetColWidth(intWidth, "head")
        'GridViewStyle(intWidth, grdHead)

        SetColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)
        GridViewStyle(intWidth, grdBody)
        '========================2011/04/26 �ԗ� �C�� �I����============================


        objCd = grdBody.Rows(hidRowIndex.Value).Cells(3).Controls(0)
        objMei = grdBody.Rows(hidRowIndex.Value).Cells(3).Controls(2)


        objMei.Text = ""
        dtSyouhinTable = SelSearch(objCd.Text)
        If dtSyouhinTable.Rows.Count = 1 Then
            objCd.Text = dtSyouhinTable.Rows(0).Item(0).ToString.Trim
            objMei.Text = dtSyouhinTable.Rows(0).Item(1).ToString.Trim
            Dim Scriptmangaer1 As Web.UI.ScriptManager = Web.UI.ScriptManager.GetCurrent(Page)
            Scriptmangaer1.SetFocus(grdBody.Rows(hidRowIndex.Value).Cells(4).Controls(0))
        Else
            strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('���i')+'&blnDelete=True&soukoCd='+escape('100')+'&FormName=" & _
                                Me.Page.Form.Name & "&objCd=" & _
                                objCd.ClientID & _
                                "&objMei=" & objMei.ClientID & _
                                "&strCd='+escape(eval('document.all.'+'" & _
                                objCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                                objMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"

        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strScript, True)

    End Sub
    Function SelSearch(ByVal objCd As String) As DataTable
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.Syouhin2MasterLogic
        SelSearch = CommonSearchLogic.SelSyouhin(objCd, "0", "100")

    End Function
    ''' <summary>GridView���ׂ镝�̐ݒ�</summary>
    Function SetColWidth(ByRef intwidth() As Integer, ByVal strName As String) As Integer
        If strName = "head" Then
            '========================2011/04/26 �ԗ� �C�� �J�n��============================
            'intwidth(0) = "92px"
            'intwidth(1) = "95px"
            'intwidth(2) = "279px"
            'intwidth(3) = "465px"
            'intwidth(4) = "112px"
            'intwidth(5) = "95px"

            intwidth(0) = 80
            intwidth(1) = 80
            intwidth(2) = 235
            intwidth(3) = 390
            intwidth(4) = 95
            intwidth(5) = 80
            '========================2011/04/26 �ԗ� �C�� �I����============================
        Else
            'intwidth(0) = "80px"
            'intwidth(1) = "120px"
            'intwidth(2) = "120px"
            'intwidth(3) = "450px"
            'intwidth(4) = "110px"
            'intwidth(5) = "95px"
        End If
    End Function
    Sub SetHeadData()
        Dim CommonLG As New CommonLogic()
        grdHead.DataSource = CommonLG.CreateHeadDataSource("���i�敪,�������@,�����T�v,���i,���i�ݒ�ꏊ,����")
        grdHead.DataBind()
    End Sub
    Sub SetDropSelect(ByVal ddl As DropDownList, ByVal strValue As String)
        If ddl.Items.FindByValue(strValue) IsNot Nothing Then
            ddl.SelectedValue = strValue
        Else
            ddl.SelectedIndex = 0
        End If
    End Sub
    ''' <summary> GridView���e�A�t�H�[�}�b�g���Z�b�g</summary>
    Sub GridViewStyle(ByVal intwidth() As Integer, ByVal grd As GridView)
        Dim intCol As Integer = 0
        Dim intRow As Integer = 0
        '========================2011/04/26 �ԗ� �C�� �J�n��============================
        'grd.Attributes.Add("Width", "950px")
        grd.Attributes.Add("Width", "910px")
        '========================2011/04/26 �ԗ� �C�� �I����============================
        If IsArray(intwidth) AndAlso grd.Rows.Count > 0 Then
            For intRow = 0 To grd.Rows.Count - 1
                For intCol = 0 To grd.Rows(intRow).Cells.Count - 1
                    '========================2011/04/26 �ԗ� �C�� �J�n��============================
                    'grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")
                    grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;")
                    If intCol < 4 Then
                        If grd.ID = Me.grdHead.ID Then
                            grd.Rows(intRow).Cells(intCol).Style.Add("width", CStr(intwidth(intCol)) & "px")
                        Else
                            grd.Rows(intRow).Cells(intCol).Style.Add("width", CStr(intwidth(intCol) + 6) & "px")
                        End If

                    End If
                    '========================2011/04/26 �ԗ� �C�� �I����============================
                    If intCol = 0 Then
                        grd.Rows(intRow).Cells(intCol).Attributes.Add("align", "center")
                    Else
                        grd.Rows(intRow).Cells(intCol).Attributes.Add("align", "left")
                    End If

                Next
                '========================2011/04/26 �ԗ� �ǉ� �J�n��============================
                grd.Rows(intRow).Cells(4).Style.Add("display", "none")
                '========================2011/04/26 �ԗ� �ǉ� �I����============================
            Next
        End If




    End Sub

    ''' <summary> GridViewBODY�����Z�b�g</summary>
    Function SetbodyData(ByVal blnKousin As Boolean, Optional ByVal msg As String = "") As DataTable

        Dim CommonSearchLogic As New Itis.Earth.BizLogic.KakakusetteiLogic
        Dim dtTable As New DataTable
        Dim CommonLG As New CommonLogic()
        Dim intRow As Integer = 0
        Dim intColCount As Integer = 0
        If blnKousin Then
            ViewState("MeisaiID") = Nothing
            ViewState("UniqueID") = Nothing

            '============================2011/04/26 �ԗ� �C�� �J�n��=====================================
            'dtTable = CommonSearchLogic.SelKakakuInfo(ViewState("Kbn").ToString, ViewState("Houhou").ToString, ViewState("Gaiyou").ToString)
            dtTable = CommonSearchLogic.SelKakakuInfo(ViewState("Kbn").ToString, ViewState("Houhou").ToString, ViewState("Gaiyou").ToString, ViewState("Syouhin").ToString)
            '========================2011/04/26 �ԗ� �C�� �I����=====================================

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

                '���i�敪
                Dim ddlControl3 As New DropDownList
                SetKakutyou(ddlControl3, "01", False)
                ddlControl3.Width = Unit.Pixel(79)
                ddlControl3.CssClass = "hissu"
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    SetDropSelect(ddlControl3, Request.Form(Split(ViewState("UniqueID").ToString, ",")(0)))
                Else
                    ddlControl3.SelectedIndex = 0
                End If
                ddlControl3.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(ddlControl3)
                strUniqueID = strUniqueID & ddlControl3.UniqueID & ","
                arrRowId(0) = ddlControl3.ClientID

                '�������@
                Dim ddlControl2 As New DropDownList
                ddlControl2.Width = Unit.Pixel(82)
                SetKakutyou(ddlControl2, "", , "m_tyousahouhou")
                ddlControl2.CssClass = "hissu"
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    SetDropSelect(ddlControl2, Request.Form(Split(ViewState("UniqueID").ToString, ",")(1)))
                Else
                    ddlControl2.SelectedIndex = 0
                End If
                ddlControl2.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(1).Controls.Add(ddlControl2)
                strUniqueID = strUniqueID & ddlControl2.UniqueID & ","
                arrRowId(1) = ddlControl2.ClientID

                '�����T�v
                Dim ddlControl4 As New DropDownList
                SetKakutyou(ddlControl4, "02", True)
                ddlControl4.Width = Unit.Pixel(225)
                ddlControl4.CssClass = "hissu"
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    SetDropSelect(ddlControl4, Request.Form(Split(ViewState("UniqueID").ToString, ",")(2)))
                Else
                    ddlControl4.SelectedIndex = 0
                End If
                ddlControl4.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(2).Controls.Add(ddlControl4)
                strUniqueID = strUniqueID & ddlControl4.UniqueID & ","
                arrRowId(2) = ddlControl4.ClientID


                '���i�R�[�h
                Dim txtControl As New TextBox
                txtControl.Width = Unit.Pixel(38)
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(3))
                Else
                    txtControl.Text = ""
                End If
                txtControl.Text = txtControl.Text.ToUpper
                txtControl.CssClass = "hissu"
                txtControl.Attributes.Add("maxlength", "8")
                txtControl.Style.Add("ime-mode", "disabled")
                txtControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(txtControl)
                arrRowId(3) = txtControl.ClientID
                strUniqueID = strUniqueID & txtControl.UniqueID & ","


                '�����{�^��
                Dim btnControl As New Button
                btnControl.Text = "����"
                btnControl.Attributes.Add("value2", "����")
                btnControl.Attributes.Add("onclick", "fncDisable();return fncOpenPop('�o�^','" & intRow & "');")
                btnControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(btnControl)


                '���i��
                Dim txtControl3 As New TextBox
                txtControl3.Width = Unit.Pixel(280)
                txtControl3.CssClass = "readOnlyStyle"
                txtControl3.Attributes.Add("readonly", "true")
                txtControl3.Attributes.Add("TabIndex", "-1")
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl3.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(4))
                Else
                    txtControl3.Text = ""
                End If
                txtControl3.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(txtControl3)
                strUniqueID = strUniqueID & txtControl3.UniqueID & ","

                '���i�ݒ�ꏊ
                Dim ddlControl As New DropDownList
                SetKakutyou(ddlControl, "03", False)
                ddlControl.Width = Unit.Pixel(95)
                ddlControl.CssClass = "hissu"
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    SetDropSelect(ddlControl, Request.Form(Split(ViewState("UniqueID").ToString, ",")(5)))
                Else
                    ddlControl.SelectedIndex = 0
                End If
                ddlControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(4).Controls.Add(ddlControl)
                strUniqueID = strUniqueID & ddlControl.UniqueID & ","
                arrRowId(4) = ddlControl.ClientID

                arrRowId(5) = ""
                arrRowId(6) = intRow

                '�o�^
                btnControl = New Button
                btnControl.Text = "�o�^"
                btnControl.Attributes.Add("onclick", "fncDisable();return fncSetRowData('�o�^','" & _
                                                        arrRowId(0) & "','" & _
                                                        arrRowId(1) & "','" & _
                                                        arrRowId(2) & "','" & _
                                                        arrRowId(3) & "','" & _
                                                        arrRowId(4) & "','" & _
                                                        arrRowId(5) & "','" & _
                                                        arrRowId(6) & "');")
                If Not blnBtn Then
                    btnControl.Enabled = False
                End If

                btnControl.Style.Add("margin-left", "2px")
                grdBody.Rows(intRow).Cells(5).Controls.Add(btnControl)

                If blnBtn Then
                    ddlControl3.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    ddlControl2.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    ddlControl4.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControl3.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    ddlControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")

                End If
                'Lable
                Dim txt As New TextBox
                txt.Style.Add("border-style", "none;")
                txt.Style.Add("background-color", "transparent;")
                txt.Attributes.Add("TabIndex", "-1")
                txt.ReadOnly = True
                txt.Width = Unit.Pixel(40)
                grdBody.Rows(intRow).Cells(5).Controls.Add(txt)

                ViewState("UniqueID") = strUniqueID
            Else

                '���i�敪
                Dim txtControl As New TextBox
                txtControl.Width = Unit.Pixel(77)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Style.Add("border-bottom", "none;")
                txtControl.Attributes.Add("readonly", "true")
                txtControl.Attributes.Add("TabIndex", "-1")
                If ViewState("MeisaiID") Is Nothing Then
                    txtControl.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(0)), ":")(1)
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtControl.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(0))
                    Else
                        txtControl.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(0)), ":")(1)
                    End If
                End If
                txtControl.Style.Add("margin-left", "1px;")
                txtControl.Style.Add("text-align", "center;")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl)
                arrRowId(0) = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(0)), ":")(0)
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"

                '�������@
                txtControl = New TextBox
                txtControl.Width = Unit.Pixel(80)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Style.Add("border-bottom", "none;")
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
                txtControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtControl)
                arrRowId(1) = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(1)), ":")(0)
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"

                '�����T�v
                txtControl = New TextBox
                txtControl.Width = Unit.Pixel(223)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Style.Add("border-bottom", "none;")
                txtControl.Attributes.Add("readonly", "true")
                txtControl.Attributes.Add("TabIndex", "-1")
                If ViewState("MeisaiID") Is Nothing Then
                    txtControl.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(2))
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtControl.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(2))
                    Else
                        txtControl.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(2))
                    End If
                End If
                txtControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(2).Controls.Add(txtControl)
                arrRowId(2) = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(2)), ":")(0)
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"


                '���i�R�[�h
                Dim txtControl3 As New TextBox
                txtControl3.Width = Unit.Pixel(38)
                If ViewState("MeisaiID") Is Nothing Then
                    txtControl3.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(3)), ":")(0)
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtControl3.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(3))
                    Else
                        txtControl3.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(3)), ":")(0)
                    End If
                End If
                txtControl3.Text = txtControl3.Text.ToUpper
                txtControl3.CssClass = "hissu"
                txtControl3.Style.Add("ime-mode", "disabled;")
                txtControl3.Attributes.Add("maxlength", "8")
                txtControl3.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(txtControl3)
                arrRowId(3) = txtControl3.ClientID
                strUniqueID = strUniqueID & txtControl3.UniqueID & "|"

                '����
                Dim btnControl As New Button
                btnControl.Text = "����"
                btnControl.Attributes.Add("value2", "����")
                btnControl.Attributes.Add("onclick", "fncDisable();return fncOpenPop('�C��','" & intRow & "');")
                btnControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(btnControl)

                '���i��
                txtControl = New TextBox
                txtControl.Width = Unit.Pixel(280)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Attributes.Add("readonly", "true")
                txtControl.Attributes.Add("TabIndex", "-1")
                If ViewState("MeisaiID") Is Nothing Then
                    txtControl.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(3)), ":")(1)
                Else
                    txtControl.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(3)), ":")(1)
                End If
                txtControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(txtControl)
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"

                '���i�ݒ�ꏊ
                Dim ddlControl As New DropDownList
                ddlControl.Width = Unit.Pixel(95)
                SetKakutyou(ddlControl, "03", False)
                ddlControl.CssClass = "hissu"
                If ViewState("MeisaiID") Is Nothing Then
                    SetDropSelect(ddlControl, CommonLG.getDisplayString(dtTable.Rows(intRow).Item(4)))

                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then

                        SetDropSelect(ddlControl, Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(5)))
                    Else
                        SetDropSelect(ddlControl, CommonLG.getDisplayString(dtTable.Rows(intRow).Item(4)))
                    End If
                End If
                ddlControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(4).Controls.Add(ddlControl)
                strUniqueID = strUniqueID & ddlControl.UniqueID & "|"
                arrRowId(4) = ddlControl.ClientID


                '�C��
                Dim hidControl As New HiddenField
                Dim hidTime As New HiddenField
                btnControl = New Button
                btnControl.Text = "�C��"

                If ViewState("MeisaiID") Is Nothing Then
                    btnControl.Attributes.Add("disabled", "true")
                    hidControl.Value = "true"
                Else

                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        If Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(6) = "" Then
                            btnControl.Enabled = Not CBool(hidBool.Value)
                        Else
                            btnControl.Enabled = Not CBool(Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(6)))
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
                grdBody.Rows(intRow).Cells(5).Controls.Add(btnControl)
                grdBody.Rows(intRow).Cells(5).Controls.Add(hidControl)
                grdBody.Rows(intRow).Cells(5).Controls.Add(hidTime)

                arrRowId(5) = hidTime.Value


                arrRowId(6) = intRow
                btnControl.Attributes.Add("onclick", "fncDisable();" & hidControl.ClientID & ".value=" & btnControl.ClientID & ".disabled;return fncSetRowData('�C��','" & _
                                                                        arrRowId(0) & "','" & _
                                                                        arrRowId(1) & "','" & _
                                                                        arrRowId(2) & "','" & _
                                                                        arrRowId(3) & "','" & _
                                                                        arrRowId(4) & "','" & _
                                                                        arrRowId(5) & "','" & _
                                                                        arrRowId(6) & "');")

                If blnBtn Then
                    ddlControl.Attributes.Add("onPropertyChange", "fncButtonChange(this," & btnControl.ClientID & "," & hidControl.ClientID & ");")
                    txtControl3.Attributes.Add("onPropertyChange", "fncButtonChange(this," & btnControl.ClientID & "," & hidControl.ClientID & ");")
                    ddlControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControl3.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")

                End If
                '�폜

                btnControl = New Button
                btnControl.Text = "�폜"
                btnControl.Enabled = True

                If Not blnBtn Then
                    btnControl.Enabled = False
                End If

                btnControl.Style.Add("margin-left", "3px")
                grdBody.Rows(intRow).Cells(5).Controls.Add(btnControl)

                MeisaiID.Add(intRow, strUniqueID & hidControl.UniqueID & "|" & hidTime.UniqueID)

                btnControl.Attributes.Add("onclick", "if (confirm('�f�[�^���폜���܂��B��낵���ł����H')){fncDisable();return fncSetRowData('�폜','" & _
                                                        arrRowId(0) & "','" & _
                                                        arrRowId(1) & "','" & _
                                                        arrRowId(2) & "','" & _
                                                        arrRowId(3) & "','" & _
                                                        arrRowId(4) & "','" & _
                                                        arrRowId(5) & "','" & _
                                                        arrRowId(6) & "');}else{return false;}")
            End If

        Next
        ViewState("MeisaiID") = MeisaiID
    End Function

    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim commoncheck As New CommonCheck
        Dim strErr As String = ""
        Dim txtObj As New DropDownList

        '========================2011/04/26 �ԗ� �ǉ� �J�n��============================
        '���i����ݒ肷��
        SetSyouhinMei()
        '========================2011/04/26 �ԗ� �ǉ� �I����============================

        If strErr = "" Then
            '========================2011/04/26 �ԗ� �C�� �J�n��============================
            'strErr = commoncheck.CheckHissuNyuuryoku(ddlSyouhinKBN.SelectedValue & ddlTysHouhou.SelectedValue & ddlTysGaiyou.SelectedValue, "")
            '�������@�������͒����T�v�������͏��i�R�[�h�̉��ꂩ���K�{
            strErr = commoncheck.CheckHissuNyuuryoku(ddlSyouhinKBN.SelectedValue & ddlTysHouhou.SelectedValue & ddlTysGaiyou.SelectedValue & Me.tbxSyouhinCd.Text.Trim, "")
            '========================2011/04/26 �ԗ� �C�� �I����============================
            If strErr <> "" Then
                txtObj = ddlTysHouhou
                '========================2011/04/26 �ԗ� �C�� �J�n��============================
                'strErr = Messages.Instance.MSG2050E
                '��������@�E�����T�v�E���i�R�[�h�͉��ꂩ�K�{�ł��B���\������
                strErr = Messages.Instance.MSG2053E
                '========================2011/04/26 �ԗ� �C�� �I����============================
            End If
        End If
        If strErr = "" Then
            '========================2011/04/26 �ԗ� �C�� �J�n��============================
            'strErr = commoncheck.CheckHissuNyuuryoku(ddlTysHouhou.SelectedValue & ddlTysGaiyou.SelectedValue, "")
            strErr = commoncheck.CheckHissuNyuuryoku(ddlTysHouhou.SelectedValue & ddlTysGaiyou.SelectedValue & Me.tbxSyouhinCd.Text.Trim, "")
            '========================2011/04/26 �ԗ� �C�� �I����============================
            If strErr <> "" And ddlSyouhinKBN.SelectedValue <> "" Then
                strErr = String.Format(Messages.Instance.MSG2051E, "���i�敪").ToString
                txtObj = ddlSyouhinKBN
            End If
        End If

        '========================2011/04/26 �ԗ� �ǉ� �J�n��============================
        Dim tbxObj As New TextBox

        If Not Me.tbxSyouhinCd.Text.Trim.Equals(String.Empty) Then

            '����i�R�[�h��̔��p�p�����`�F�b�N
            If strErr = String.Empty Then
                strErr = commoncheck.ChkHankakuEisuuji(Me.tbxSyouhinCd.Text.Trim, "���i�R�[�h")
                If strErr <> String.Empty Then
                    tbxObj = Me.tbxSyouhinCd
                End If
            End If

            '����i�R�[�h��̌����`�F�b�N(8��)
            If strErr = String.Empty Then
                If Me.tbxSyouhinCd.Text.Trim.Length > 8 Then
                    strErr = String.Format(Messages.Instance.MSG2004E, "���i�R�[�h", "8").ToString
                End If
                If strErr <> String.Empty Then
                    tbxObj = Me.tbxSyouhinCd
                End If
            End If
        End If
        '========================2011/04/26 �ԗ� �ǉ� �I����============================

        If strErr <> "" Then
            '========================2011/04/26 �ԗ� �C�� �J�n��============================
            'strErr = "alert('" & strErr & "');document.getElementById('" & txtObj.ClientID & "').focus();"
            If Not txtObj.ClientID Is Nothing Then
                strErr = "alert('" & strErr & "');document.getElementById('" & txtObj.ClientID & "').focus();"
            Else
                If Not tbxObj.ClientID Is Nothing Then
                    '����i�R�[�h�
                    strErr = "alert('" & strErr & "');document.getElementById('" & tbxObj.ClientID & "').focus();"
                End If
            End If
                '========================2011/04/26 �ԗ� �C�� �I����============================
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
                SetHeadData()
                SetbodyData(False, Messages.Instance.MSG2034E)
        Else

                ViewState("Kbn") = ddlSyouhinKBN.SelectedValue
                ViewState("Houhou") = ddlTysHouhou.SelectedValue
                ViewState("Gaiyou") = ddlTysGaiyou.SelectedValue
                '========================2011/04/26 �ԗ� �ǉ� �J�n��============================
                '����i�R�[�h�
                ViewState("Syouhin") = Me.tbxSyouhinCd.Text.Trim
                '========================2011/04/26 �ԗ� �ǉ� �I����============================
                SetHeadData()
                ViewState("dtTable") = SetbodyData(True, Messages.Instance.MSG2034E)

        End If
        '========================2011/04/26 �ԗ� �C�� �J�n��============================
        'SetColWidth(intWidth, "body")
        'GridViewStyle(intWidth, grdBody)

        'SetColWidth(intWidth, "head")
        'GridViewStyle(intWidth, grdHead)

        SetColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)
        GridViewStyle(intWidth, grdBody)
        '========================2011/04/26 �ԗ� �C�� �I����============================

        'If strErr = "" Then
        '    Dim Scriptmangaer1 As Web.UI.ScriptManager = Web.UI.ScriptManager.GetCurrent(Page)
        '    Scriptmangaer1.SetFocus(grdBody.Rows(grdBody.Rows.Count - 1).Cells(0).Controls(0))
        '    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "txtfocus", " window.setTimeout('objEBI(\'" & grdBody.Rows(grdBody.Rows.Count - 1).Cells(0).Controls(0).ClientID & "\').focus()',1); ", True)
        'End If

    End Sub
    Protected Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim commoncheck As New CommonCheck
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.KakakusetteiLogic
        Dim Syouhin2MasterLogic As New Itis.Earth.BizLogic.Syouhin2MasterLogic
        Dim dtHaita As New DataTable
        Dim strErr As String = ""
        Dim strCol As Integer = -1
        Dim dtTable As DataTable = CType(ViewState("dtTable"), DataTable)
        Dim strItem As Integer = 0

        '========================2011/04/26 �ԗ� �ǉ� �J�n��============================
        '���i����ݒ肷��
        SetSyouhinMei()
        '========================2011/04/26 �ԗ� �ǉ� �I����============================

        If hidBtn.Value = "�o�^" Then
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(Split(hidSyouhinKBN.Value, ",")(0), "���i�敪")
                If strErr <> "" Then
                    strCol = 0
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(Split(hidTysHouhou.Value, ",")(0), "�������@")
                If strErr <> "" Then
                    strCol = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(Split(hidTysGaiyou.Value, ",")(0), "�����T�v")
                If strErr <> "" Then
                    strCol = 2
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidSyouhinCd.Value, "���i�R�[�h")
                If strErr <> "" Then
                    strCol = 3
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.ChkHankakuEisuuji(hidSyouhinCd.Value, "���i�R�[�h")
                If strErr <> "" Then
                    strCol = 3
                End If
            End If

            '============================2011/04/26 �ԗ� �폜 �J�n��=====================================
            'If strErr = "" Then
            '    strErr = commoncheck.CheckHissuNyuuryoku(hidSetteiBasyo.Value, "���i�ݒ�ꏊ")
            '    If strErr <> "" Then
            '        strCol = 4
            '    End If
            'End If
            '============================2011/04/26 �ԗ� �폜 �I����=====================================

            If strErr = "" Then
                If SelSearch(hidSyouhinCd.Value).Rows.Count = 0 Then
                    strErr = String.Format(Messages.Instance.MSG2036E, "���i�}�X�^").ToString
                    strCol = 3
                Else
                    hidSyouhinCd.Value = SelSearch(hidSyouhinCd.Value).Rows(0).Item(0)
                End If
            End If

            '�d���`�F�b�N(���i�敪�A�������@�A�����T�v)
            If strErr = "" Then
                If Not CommonSearchLogic.SelJyuufuku(Split(hidSyouhinKBN.Value, ",")(0), Split(hidTysHouhou.Value, ":")(0), Split(hidTysGaiyou.Value, ",")(0)) Then
                    strErr = Messages.Instance.MSG2035E
                    strCol = 0
                End If
            End If

            '========================2011/04/26 �ԗ� �ǉ� �J�n��============================
            '�d���`�F�b�N(���i�敪�A�������@�A���i����)
            If strErr = "" Then
                If Not CommonSearchLogic.CheckJyuufukuSyouhin(Split(hidSyouhinKBN.Value, ",")(0), Split(hidTysHouhou.Value, ":")(0), Me.hidSyouhinCd.Value.Trim) Then
                    strErr = Messages.Instance.MSG2035E
                    strCol = 0
                End If
            End If
            '========================2011/04/26 �ԗ� �ǉ� �I����============================

            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"

            Else
                '========================2011/04/26 �ԗ� �ǉ� �J�n��============================
                '����i���i�ݒ�}�X�^.���i�ݒ�ꏊ���"2"�ɃZ�b�g
                Me.hidSetteiBasyo.Value = "2"
                '========================2011/04/26 �ԗ� �ǉ� �I����============================
                If CommonSearchLogic.InsKakakusettei(Split(hidSyouhinKBN.Value, ",")(0), Split(hidTysHouhou.Value, ":")(0), Split(hidTysGaiyou.Value, ",")(0), hidSyouhinCd.Value, hidSetteiBasyo.Value, ViewState("UserId").ToString) Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "���i���i�ݒ�}�X�^") & "');"
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(0) = Split(hidSyouhinKBN.Value, ",")(0) & ":" & Split(hidSyouhinKBN.Value, ",")(1)
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(1) = hidTysHouhou.Value
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(2) = Split(hidTysGaiyou.Value, ",")(0) & ":" & Split(hidTysGaiyou.Value, ",")(1)
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(3) = hidSyouhinCd.Value & ":" & Syouhin2MasterLogic.SelSyouhin(hidSyouhinCd.Value, "", "100").Rows(0).Item(1).ToString
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(4) = hidSetteiBasyo.Value

                    dtTable.Rows.Add(dtTable.NewRow)
                    ViewState("dtTable") = dtTable
                    ViewState("UniqueID") = Nothing
                Else
                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "���i���i�ݒ�}�X�^") & "');"

                End If
            End If
        ElseIf hidBtn.Value = "�C��" Then
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidSyouhinCd.Value, "���i�R�[�h")
                If strErr <> "" Then
                    strCol = 3
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.ChkHankakuEisuuji(hidSyouhinCd.Value, "���i�R�[�h")
                If strErr <> "" Then
                    strCol = 3
                End If
            End If

            '============================2011/04/26 �ԗ� �폜 �J�n��=====================================
            'If strErr = "" Then
            '    strErr = commoncheck.CheckHissuNyuuryoku(hidSetteiBasyo.Value, "���i�ݒ�ꏊ")
            '    If strErr <> "" Then
            '        strCol = 4
            '    End If
            'End If
            '============================2011/04/26 �ԗ� �폜 �I����=====================================

            If strErr = "" Then
                If SelSearch(hidSyouhinCd.Value).Rows.Count = 0 Then
                    strErr = String.Format(Messages.Instance.MSG2036E, "���i�}�X�^").ToString
                    strCol = 3
                Else
                    hidSyouhinCd.Value = SelSearch(hidSyouhinCd.Value).Rows(0).Item(0)
                End If
            End If


            If strErr = "" Then
                dtHaita = CommonSearchLogic.SelHaita(hidSyouhinKBN.Value, hidTysHouhou.Value, hidTysGaiyou.Value, hidUPDTime.Value)
                If dtHaita.Rows.Count <> 0 Then
                    strErr = String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "���i���i�ݒ�}�X�^").ToString()

                End If
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"
                hidBool.Value = "False"
            Else
                Dim intRturn As Integer
                intRturn = CommonSearchLogic.UpdKakakusettei(hidSyouhinKBN.Value, hidTysHouhou.Value, hidTysGaiyou.Value, hidSyouhinCd.Value, hidSetteiBasyo.Value, ViewState("UserId").ToString)
                '����I�������ꍇ

                If intRturn = 1 Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "���i���i�ݒ�}�X�^") & "');"
                    dtTable.Rows(hidRowIndex.Value).Item(3) = hidSyouhinCd.Value & ":" & Syouhin2MasterLogic.SelSyouhin(hidSyouhinCd.Value, "", "100").Rows(0).Item(1).ToString
                    dtTable.Rows(hidRowIndex.Value).Item(4) = hidSetteiBasyo.Value
                    dtTable.Rows(hidRowIndex.Value).Item(5) = CommonSearchLogic.SelKakakuInfo(hidSyouhinKBN.Value, hidTysHouhou.Value, hidTysGaiyou.Value).Rows(0).Item(5).ToString

                    hidBool.Value = "True"
                ElseIf intRturn = 2 Then
                    strErr = "alert('" & Messages.Instance.MSG2049E & "');"
                    hidBool.Value = "False"

                Else

                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "���i���i�ݒ�}�X�^") & "');"
                    hidBool.Value = "False"

                End If
                strCol = 3
            End If
            Dim strMeisai As String()
            strMeisai = Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(hidRowIndex.Value).ToString, "|")
            '�C����ClientID
            strMeisai(6) = ""
            CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(hidRowIndex.Value) = Join(strMeisai, "|")
        ElseIf hidBtn.Value = "�폜" Then
            If strErr = "" Then
                dtHaita = CommonSearchLogic.SelHaita(hidSyouhinKBN.Value, hidTysHouhou.Value, hidTysGaiyou.Value, hidUPDTime.Value)
                If dtHaita.Rows.Count <> 0 Then
                    strErr = String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "���i���i�ݒ�}�X�^").ToString()

                End If
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"
            Else
                Dim intRturn As Boolean
                '����I�������ꍇ
                intRturn = CommonSearchLogic.DelKakakusettei(hidSyouhinKBN.Value, hidTysHouhou.Value, hidTysGaiyou.Value)
                If intRturn = True Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "���i���i�ݒ�}�X�^") & "');"
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

                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "���i���i�ݒ�}�X�^") & "');"
                    hidBool.Value = "False"

                End If


            End If
        End If
        SetbodyData(False)
        SetHeadData()
        '========================2011/04/26 �ԗ� �C�� �J�n��============================
        'SetColWidth(intWidth, "body")
        'GridViewStyle(intWidth, grdBody)

        'SetColWidth(intWidth, "head")
        'GridViewStyle(intWidth, grdHead)

        SetColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)
        GridViewStyle(intWidth, grdBody)
        '========================2011/04/26 �ԗ� �C�� �I����============================

        If strCol <> -1 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strErr & "document.getElementById('" & grdBody.Rows(hidRowIndex.Value).Cells(strCol).Controls(strItem).ClientID & "').focus();", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strErr, True)

        End If

    End Sub
    Protected Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Server.Transfer("MasterMainteMenu.aspx")
    End Sub

    ''' <summary>
    ''' ���i�R�[�h.�����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>���i�����擾����</remarks>
    ''' <history>2011/04/26�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSyouhinCd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSyouhinCd.Click

        Dim strScript As String = ""
        Dim dtSyouhinTable As New DataTable
        Dim objCd As New TextBox
        Dim objCd2 As New TextBox
        Dim objCd3 As New DropDownList
        Dim objMei As New TextBox
        Dim kbn As String = ""


        SetHeadData()
        SetbodyData(False)

        SetColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)
        GridViewStyle(intWidth, grdBody)

        '����i�R�[�h�÷���ޯ��
        objCd = Me.tbxSyouhinCd
        '����i���÷���ޯ��
        objMei = Me.tbxSyouhinMei

        objMei.Text = ""
        dtSyouhinTable = SelSearch(objCd.Text)
        If dtSyouhinTable.Rows.Count = 1 Then
            objCd.Text = dtSyouhinTable.Rows(0).Item(0).ToString.Trim
            objMei.Text = dtSyouhinTable.Rows(0).Item(1).ToString.Trim
            Dim Scriptmangaer1 As Web.UI.ScriptManager = Web.UI.ScriptManager.GetCurrent(Page)
            Scriptmangaer1.SetFocus(objCd)
        Else
            strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('���i')+'&blnDelete=True&soukoCd='+escape('100')+'&FormName=" & _
                                Me.Page.Form.Name & "&objCd=" & _
                                objCd.ClientID & _
                                "&objMei=" & objMei.ClientID & _
                                "&strCd='+escape(eval('document.all.'+'" & _
                                objCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                                objMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"

        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strScript, True)

    End Sub

    ''' <summary>
    ''' ���i�����擾����
    ''' </summary>
    ''' <remarks>���i�����擾����</remarks>
    ''' <history>2011/04/26�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub SetSyouhinMei()

        If Not Me.tbxSyouhinCd.Text.Trim.Equals(String.Empty) Then
            '�f�[�^���擾����
            Dim dtSyouhinTable As New DataTable
            dtSyouhinTable = SelSearch(Me.tbxSyouhinCd.Text.Trim)
            If dtSyouhinTable.Rows.Count > 0 Then
                '����i�R�[�h�
                Me.tbxSyouhinMei.Text = dtSyouhinTable.Rows(0).Item(1).ToString.Trim
            Else
                '����i�R�[�h�
                Me.tbxSyouhinMei.Text = String.Empty
            End If
        Else
            '����i�R�[�h�
            Me.tbxSyouhinMei.Text = String.Empty
        End If

    End Sub

End Class