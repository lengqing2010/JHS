Imports Itis.Earth.BizLogic
Partial Public Class KeiretuMaster
    Inherits System.Web.UI.Page
    Private blnBtn As Boolean
    Private intWidth(4) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strNinsyou As String = ""
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        'å†å¿É`ÉFÉbÉNÇ®ÇÊÇ—ê›íË
        blnBtn = commonChk.CommonNinnsyou(strUserID, "eigyou_master_kanri_kengen")
        ViewState("UserId") = strUserID
        If Not IsPostBack Then
            Dim ddlist As New ListItem
            ddlist = New ListItem
            ddlist.Text = ""
            ddlist.Value = ""
            ddlTorikesi.Items.Add(ddlist)

            ddlist = New ListItem
            ddlist.Text = "éÊè¡à»äO"
            ddlist.Value = "0"
            ddlTorikesi.Items.Add(ddlist)
            ddlist = New ListItem
            ddlist.Text = "éÊè¡"
            ddlist.Value = "1"
            ddlTorikesi.Items.Add(ddlist)

            ViewState("kbn") = ""
            ViewState("Torikesi") = 0

            SetHeadData()
            ViewState("dtTable") = SetbodyData(True)

            SetColWidth(intWidth, "body")
            GridViewStyle(intWidth, grdBody)

            SetColWidth(intWidth, "head")
            GridViewStyle(intWidth, grdHead)

            Common_drop1.SelectedValue = "S"
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "txtfocus", " window.setTimeout('objEBI(\'" & grdBody.Rows(grdBody.Rows.Count - 1).Cells(0).Controls(0).ClientID & "\').focus()',50); ", True)

        End If
        divMeisai.Attributes.Add("style", "overflow-y:auto ; height:339px;width:685px;")
        MakeScript()
        btnSearch.Attributes.Add("onclick", "document.getElementById ('" & hidTop.ClientID & "').value =0;")
        Me.divMeisai.Attributes.Add("onscroll", "document.getElementById ('" & hidTop.ClientID & "').value =" & Me.divMeisai.ClientID & ".scrollTop;")
    End Sub

    ''' <summary>çiçûï“èWÉ{É^Éìâüâ∫éûèàóù</summary>
    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim commoncheck As New CommonCheck
        Dim strErr As String = ""
        strErr = commoncheck.CheckHissuNyuuryoku(Common_drop1.SelectedValue, "ãÊï™")

        If strErr <> "" Then
            ViewState("kbn") = ""
            ViewState("Torikesi") = 0
            strErr = "alert('" & strErr & "');document.getElementById('" & Common_drop1.Controls(0).ClientID & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
            Common_drop1.Focus()
            SetHeadData()
            SetbodyData(False, Messages.Instance.MSG2034E)
        Else
            ViewState("kbn") = Common_drop1.SelectedValue
            ViewState("Torikesi") = ddlTorikesi.SelectedValue
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
        'End If
    End Sub
    ''' <summary>GridViewï¿Ç◊ÇÈïùÇÃê›íË</summary>
    Function SetColWidth(ByRef intwidth() As String, ByVal strName As String) As Integer
        If strName = "head" Then
            intwidth(0) = "200px"
            intwidth(1) = "80px"
            intwidth(2) = "260px"
            intwidth(3) = "50px"
        Else
            intwidth(0) = "206px"
            intwidth(1) = "86px"
            intwidth(2) = "266px"
            intwidth(3) = "56px"
        End If


    End Function
    ''' <summary> GridViewÉwÉbÉ_Å[ïîÇÉZÉbÉg</summary>
    Sub SetHeadData()
        Dim CommonLG As New CommonLogic()
        grdHead.DataSource = CommonLG.CreateHeadDataSource("ãÊï™,ånóÒÉRÅ[Éh,ånóÒñº,éÊè¡,èàóù")
        grdHead.DataBind()
    End Sub
    ''' <summary> GridViewBODYïîÇÉZÉbÉg</summary>
    Function SetbodyData(ByVal blnKousin As Boolean, Optional ByVal msg As String = "") As DataTable

        Dim CommonSearchLogic As New Itis.Earth.BizLogic.KeiretuMasterLogic
        Dim dtTable As New DataTable
        Dim CommonLG As New CommonLogic()
        Dim intRow As Integer = 0
        Dim intColCount As Integer = 0
        If blnKousin Then
            ViewState("MeisaiID") = Nothing
            ViewState("UniqueID") = Nothing
            dtTable = CommonSearchLogic.SelKeiretuInfo(ViewState("kbn").ToString, ViewState("Torikesi").ToString)
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
            Dim arrRowId(5) As String

            If intRow = grdBody.Rows.Count - 1 Then

                'ãÊï™
                Dim dropControl As New DropDownList
                Dim CommonDropLogic As New CommonDropLogic
                Dim dtTodouhuken As Data.DataTable = CommonDropLogic.GetCommonDropInfo(5)
                Dim intRow2 As Integer = 0
                For intRow2 = 0 To dtTodouhuken.Rows.Count - 1
                    Dim ddlist As New ListItem

                    ddlist = New ListItem
                    If dtTodouhuken.Rows(intRow2).Item(1).ToString.Trim = "" Then
                        ddlist.Text = dtTodouhuken.Rows(intRow2).Item(0).ToString
                    Else
                        ddlist.Text = dtTodouhuken.Rows(intRow2).Item(0).ToString & "ÅF" & dtTodouhuken.Rows(intRow2).Item(1).ToString

                    End If
                    ddlist.Value = dtTodouhuken.Rows(intRow2).Item(0).ToString
                    dropControl.Items.Add(ddlist)
                Next
                dropControl.Items.Insert(0, New ListItem(String.Empty, String.Empty))

                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    If dropControl.Items.FindByValue(Request.Form(Split(ViewState("UniqueID").ToString, ",")(0))) IsNot Nothing Then
                        dropControl.Items.FindByValue(Request.Form(Split(ViewState("UniqueID").ToString, ",")(0))).Selected = True
                    Else
                        dropControl.SelectedIndex = 0
                    End If
                End If
                dropControl.CssClass = "hissu"
                grdBody.Rows(intRow).Cells(0).Controls.Add(dropControl)
                arrRowId(0) = dropControl.ClientID
                strUniqueID = strUniqueID & dropControl.UniqueID & ","

                'ånóÒÉRÅ[Éh
                Dim txtControl As New TextBox
                txtControl.Width = Unit.Pixel(60)
                txtControl.CssClass = "hissu"
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(1))
                Else
                    txtControl.Text = ""
                End If
                txtControl.Style.Add("ime-mode", "disabled")
                txtControl.Attributes.Add("maxlength", "5")
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtControl)
                arrRowId(1) = txtControl.ClientID
                strUniqueID = strUniqueID & txtControl.UniqueID & ","

                'ånóÒñº
                Dim txtControlM As New TextBox
                txtControlM.Width = Unit.Pixel(240)
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControlM.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(2))
                Else
                    txtControlM.Text = ""
                End If
                txtControlM.Attributes.Add("maxlength", "40")
                grdBody.Rows(intRow).Cells(2).Controls.Add(txtControlM)
                arrRowId(2) = txtControlM.ClientID
                strUniqueID = strUniqueID & txtControlM.UniqueID & ","

                'éÊè¡
                Dim chkControl As New CheckBox
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    chkControl.Checked = IIf(Request.Form(Split(ViewState("UniqueID").ToString, ",")(3)) = "on", True, False)
                Else
                    If ddlTorikesi.SelectedValue <> "" Then
                        chkControl.Checked = ddlTorikesi.SelectedValue
                    End If
                End If

                grdBody.Rows(intRow).Cells(3).Controls.Add(chkControl)
                arrRowId(3) = chkControl.ClientID
                strUniqueID = strUniqueID & chkControl.UniqueID
                arrRowId(4) = ""
                arrRowId(5) = intRow

                'ìoò^
                Dim btnControl As New Button
                btnControl.Text = "ìoò^"
                btnControl.Attributes.Add("onclick", "fncDisable();return fncSetRowData('ìoò^','" & _
                                                        arrRowId(0) & "','" & _
                                                        arrRowId(1) & "','" & _
                                                        arrRowId(2) & "','" & _
                                                        arrRowId(3) & "','" & _
                                                        arrRowId(4) & "','" & _
                                                        arrRowId(5) & "');")
                If Not blnBtn Then
                    btnControl.Enabled = False
                End If
                grdBody.Rows(intRow).Cells(4).Controls.Add(btnControl)
                If blnBtn Then
                    dropControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControlM.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    chkControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")


                End If
            Else
                'ånóÒñº
                Dim txtControl As New TextBox
                txtControl.Width = Unit.Pixel(240)
                If ViewState("MeisaiID") Is Nothing Then
                    txtControl.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item("keiretu_mei"))
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtControl.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(0))
                    Else
                        txtControl.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item("keiretu_mei"))
                    End If
                End If
                txtControl.Attributes.Add("maxlength", "40")
                grdBody.Rows(intRow).Cells(2).Controls.Add(txtControl)

                'éÊè¡
                Dim chkControl As New CheckBox
                If ViewState("MeisaiID") Is Nothing Then
                    chkControl.Checked = CBool(dtTable.Rows(intRow).Item("torikesi"))
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        chkControl.Checked = IIf(Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(1)) = "on", True, False)
                    Else
                        chkControl.Checked = CBool(dtTable.Rows(intRow).Item("torikesi"))
                    End If

                End If
                grdBody.Rows(intRow).Cells(3).Controls.Add(chkControl)

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
                hidTime.Value = dtTable.Rows(intRow).Item("upd_datetime").ToString
                grdBody.Rows(intRow).Cells(4).Controls.Add(btnControl)
                grdBody.Rows(intRow).Cells(4).Controls.Add(hidControl)
                grdBody.Rows(intRow).Cells(4).Controls.Add(hidTime)
                MeisaiID.Add(intRow, txtControl.UniqueID & "|" & chkControl.UniqueID & "|" & hidControl.UniqueID & "|" & hidTime.UniqueID)


                arrRowId(0) = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item("kbn_mei")), ":")(0)
                arrRowId(1) = CommonLG.getDisplayString(dtTable.Rows(intRow).Item("keiretu_cd"))
                arrRowId(2) = txtControl.ClientID
                arrRowId(3) = chkControl.ClientID

                arrRowId(4) = hidTime.Value


                arrRowId(5) = intRow
                btnControl.Attributes.Add("onclick", hidControl.ClientID & ".value=" & btnControl.ClientID & ".disabled;fncDisable();return fncSetRowData('èCê≥','" & _
                                                        arrRowId(0) & "','" & _
                                                        arrRowId(1) & "','" & _
                                                        arrRowId(2) & "','" & _
                                                        arrRowId(3) & "','" & _
                                                        arrRowId(4) & "','" & _
                                                        arrRowId(5) & "');")
                If blnBtn Then
                    txtControl.Attributes.Add("onPropertyChange", "fncButtonChange(" & btnControl.ClientID & "," & hidControl.ClientID & ");")
                    chkControl.Attributes.Add("onclick", "fncButtonChange(" & btnControl.ClientID & "," & hidControl.ClientID & ");if(this.checked){this.value='on'}else{this.value='off'};")
                    txtControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    chkControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")

                Else

                    chkControl.Attributes.Add("onclick", "if(this.checked){this.value='on'}else{this.value='off'};")

                End If


            End If


        Next

        ViewState("UniqueID") = strUniqueID
        ViewState("MeisaiID") = MeisaiID
    End Function
    ''' <summary>JavascriptçÏê¨</summary>
    Private Sub MakeScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "GetScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")

            .AppendLine("function  fncSetRowData(strBtn,strKbn,strKeiretuCd,strKeiretuMei,strTorikesi,strUPDTime,strRow)")
            .AppendLine("{")

            .AppendLine("document.getElementById ('" & hidBtn.ClientID & "').value=strBtn;")

            .AppendLine("if (strBtn=='ìoò^'){")
            .AppendLine("document.getElementById ('" & hidKBN.ClientID & "').value=eval('document.all.'+strKbn).options(eval('document.all.'+strKbn).selectedIndex).text;")

            .AppendLine("document.getElementById ('" & hidKeiretuCd.ClientID & "').value=eval('document.all.'+strKeiretuCd).value;")
            .AppendLine("document.getElementById ('" & hidTop.ClientID & "').value=strRow*60;")
            .AppendLine("}else{")
            .AppendLine("document.getElementById ('" & hidKBN.ClientID & "').value=strKbn;")
            .AppendLine("document.getElementById ('" & hidKeiretuCd.ClientID & "').value=strKeiretuCd;")

            .AppendLine("}")

            .AppendLine("document.getElementById ('" & hidKeiretuMei.ClientID & "').value=eval('document.all.'+strKeiretuMei).value;")
            .AppendLine("document.getElementById ('" & hidTorikesi.ClientID & "').value=eval('document.all.'+strTorikesi).checked;")
            .AppendLine("document.getElementById ('" & hidUPDTime.ClientID & "').value=strUPDTime;")
            .AppendLine("document.getElementById ('" & hidRowIndex.ClientID & "').value=strRow;")
            .AppendLine("document.getElementById ('" & btn.ClientID & "').click();")

            .AppendLine("return false;")
            .AppendLine("}")
            .AppendLine("function  fncButtonChange(strBtn,strHid)")
            .AppendLine("{")
            If blnBtn Then
                .AppendLine("strBtn.disabled = false;")
                .AppendLine("strHid.value = false;")
            End If

            .AppendLine("}")

            .AppendLine("function  fncDisable()")
            .AppendLine("{")
            .AppendLine("var i;")
            .AppendLine("if (document.forms[0].elements.length) ")
            .AppendLine("	{")
            .AppendLine("	for (i=1;i < document.forms[0].elements.length;i++) ")
            .AppendLine("if (document.forms[0].elements[i].type=='submit') ")
            .AppendLine("	{")
            .AppendLine("if (document.forms[0].elements[i].value=='ìoò^' || document.forms[0].elements[i].value=='èCê≥') ")
            .AppendLine("	{")
            .AppendLine("	document.forms[0].elements[i].disabled = true;")
            .AppendLine("	}	")
            .AppendLine("	}	")
            .AppendLine("	}	")
            .AppendLine("}")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub
    ''' <summary> GridViewì‡óeÅAÉtÉHÅ[É}ÉbÉgÇÉZÉbÉg</summary>
    Sub GridViewStyle(ByVal intwidth() As String, ByVal grd As GridView)
        Dim intCol As Integer = 0
        Dim intRow As Integer = 0

        grd.Attributes.Add("Width", "670px")
        If IsArray(intwidth) AndAlso grd.Rows.Count > 0 Then
            For intRow = 0 To grd.Rows.Count - 1
                For intCol = 0 To grd.Rows(intRow).Cells.Count - 1
                    grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")
                    grd.Rows(intRow).Cells(intCol).Attributes.Add("align", "center")
                Next
            Next
        End If
    End Sub
    ''' <summary>ìoò^Ç∆èCê≥É{É^ÉìÇÃèàóù</summary>
    Private Sub btn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.Click

        Dim commoncheck As New CommonCheck
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.KeiretuMasterLogic
        Dim dtHaita As New DataTable
        Dim strErr As String = ""
        Dim strCol As Integer = -1
        Dim dtTable As DataTable = CType(ViewState("dtTable"), DataTable)

        If hidBtn.Value = "ìoò^" Then
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidKBN.Value, "ãÊï™")
                If strErr <> "" Then
                    strCol = 0
                End If
            End If

            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidKeiretuCd.Value, "ånóÒÉRÅ[Éh")
                If strErr <> "" Then
                    strCol = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.ChkHankakuEisuuji(hidKeiretuCd.Value, "ånóÒÉRÅ[Éh")
                If strErr <> "" Then
                    strCol = 1
                End If
            End If

            If strErr = "" And hidKeiretuMei.Value <> "" Then
                strErr = commoncheck.CheckByte(hidKeiretuMei.Value, 40, "ånóÒñº", kbn.ZENKAKU)
                If strErr <> "" Then
                    strCol = 2
                End If
            End If
            If strErr = "" And hidKeiretuMei.Value <> "" Then
                strErr = commoncheck.CheckKinsoku(hidKeiretuMei.Value, "ånóÒñº")
                If strErr <> "" Then
                    strCol = 2
                End If
            End If
            If strErr = "" Then
                If Not CommonSearchLogic.SelJyuufuku(Split(hidKBN.Value, "ÅF")(0), hidKeiretuCd.Value) Then
                    strErr = Messages.Instance.MSG2035E
                    strCol = 0
                End If
            End If
            If hidTorikesi.Value = "true" Then
                hidTorikesi.Value = "1"
            Else
                hidTorikesi.Value = "0"
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"

            Else
                If CommonSearchLogic.InsKeiretu(Split(hidKBN.Value, "ÅF")(0), hidTorikesi.Value, hidKeiretuCd.Value, hidKeiretuMei.Value, ViewState("UserId").ToString) Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "ånóÒÉ}ÉXÉ^") & "');"
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(0) = hidKBN.Value
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(1) = hidKeiretuCd.Value
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(2) = hidKeiretuMei.Value
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(3) = hidTorikesi.Value
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(4) = CommonSearchLogic.SelDate(Split(hidKBN.Value, "ÅF")(0), hidKeiretuCd.Value)
                    dtTable.Rows.Add(dtTable.NewRow)
                    ViewState("dtTable") = dtTable
                    ViewState("UniqueID") = Nothing

                Else
                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "ånóÒÉ}ÉXÉ^") & "');"
                    strCol = 0

                End If

            End If

        Else

            If strErr = "" Then
                strErr = commoncheck.CheckByte(hidKeiretuMei.Value, 40, "ånóÒñº", kbn.ZENKAKU)
                If strErr <> "" Then
                    strCol = 2
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckKinsoku(hidKeiretuMei.Value, "ånóÒñº")
                If strErr <> "" Then
                    strCol = 2
                End If
            End If
            If strErr = "" Then
                dtHaita = CommonSearchLogic.SelHaita(Split(hidKBN.Value, "ÅF")(0), hidKeiretuCd.Value, hidUPDTime.Value)
                If dtHaita.Rows.Count <> 0 Then
                    strErr = String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "ånóÒÉ}ÉXÉ^").ToString()
                    strCol = 2
                End If
            End If
            If hidTorikesi.Value = "true" Then
                hidTorikesi.Value = "1"
            Else
                hidTorikesi.Value = "0"
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"
                hidBool.Value = "False"
            Else

                'ê≥èÌèIóπÇµÇΩèÍçá
                If CommonSearchLogic.UpdKeiretu(Split(hidKBN.Value, "ÅF")(0), hidTorikesi.Value, hidKeiretuCd.Value, hidKeiretuMei.Value, ViewState("UserId").ToString) Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "ånóÒÉ}ÉXÉ^") & "');"
                    dtTable.Rows(hidRowIndex.Value).Item(4) = CommonSearchLogic.SelDate(Split(hidKBN.Value, "ÅF")(0), hidKeiretuCd.Value)
                    hidBool.Value = "True"
                Else
                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "ånóÒÉ}ÉXÉ^") & "');"
                    hidBool.Value = "False"

                End If
                strCol = 2
            End If
            Dim strMeisai As String()
            strMeisai = Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(hidRowIndex.Value).ToString, "|")
            'èCê≥ÇÃClientID
            strMeisai(2) = ""
            CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(hidRowIndex.Value) = Join(strMeisai, "|")


        End If
        SetbodyData(False)
        SetHeadData()


        SetColWidth(intWidth, "body")
        GridViewStyle(intWidth, grdBody)
        SetColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)

        If strCol <> -1 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strErr & "document.getElementById('" & grdBody.Rows(hidRowIndex.Value).Cells(strCol).Controls(0).ClientID & "').focus();", True)
        Else

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strErr, True)

        End If

    End Sub
    ''' <summary>ÉNÉäÉAÉ{É^ÉìÇÃèàóù</summary>
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ddlTorikesi.SelectedIndex = 0
        Common_drop1.SelectedValue = ""

        SetHeadData()
        SetbodyData(False)

        SetColWidth(intWidth, "body")
        GridViewStyle(intWidth, grdBody)

        SetColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;", True)


    End Sub

    Protected Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Server.Transfer("MasterMainteMenu.aspx")
    End Sub
End Class