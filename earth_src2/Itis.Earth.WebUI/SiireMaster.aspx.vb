Imports Itis.Earth.BizLogic
Partial Public Class SiireMaster
    Inherits System.Web.UI.Page
    Private intWidth(6) As String
    Private blnBtn As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strNinsyou As String = ""
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        'å†å¿É`ÉFÉbÉNÇ®ÇÊÇ—ê›íË
        blnBtn = commonChk.CommonNinnsyou(strUserID, "kaiseki_master_kanri_kengen")
        ViewState("UserId") = strUserID

        If Not IsPostBack Then
            ViewState("Kameiten") = ""
            ViewState("Kaisya") = ""
            ViewState("UniqueID") = Nothing
            ViewState("MeisaiID") = Nothing

            SetHeadData()
            ViewState("dtTable") = SetbodyData(True)

            SetColWidth(intWidth, "body")

            GridViewStyle(intWidth, grdBody)

            SetColWidth(intWidth, "head")
            GridViewStyle(intWidth, grdHead)
            'UpdatePanelA.Update()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "txtfocus", " window.setTimeout('objEBI(\'" & grdBody.Rows(grdBody.Rows.Count - 1).Cells(0).Controls(0).ClientID & "\').focus()',1); ", True)

        Else
            closecover()
        End If
        divMeisai.Attributes.Add("style", "overflow-y:auto ; height:339px;width:970px;")

        tbxKaisya_mei.Attributes.Add("readonly", "true")
        tbxKameiten_mei.Attributes.Add("readonly", "true")
        btnSearch.Attributes.Add("onclick", "document.getElementById ('" & hidTop.ClientID & "').value =0;")
        Me.divMeisai.Attributes.Add("onscroll", "document.getElementById ('" & hidTop.ClientID & "').value =" & Me.divMeisai.ClientID & ".scrollTop;")
        btnClear.Attributes.Add("onclick", "return itemclear();")
        MakeScript()
    End Sub
    Protected Sub btnCommonSearch(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchKameiten.Click, btnSearchTysKaisya.Click, btnOpen.Click
        Dim strScript As String = ""
        Dim dtSyouhinTable As New DataTable
        Dim objCd As New TextBox
        Dim objCd2 As New TextBox
        Dim objMei As New TextBox
        Dim kbn As String = ""


        SetHeadData()
        SetbodyData(False)

        SetColWidth(intWidth, "body")
        GridViewStyle(intWidth, grdBody)

        SetColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)

        Select Case CType(sender, Button).ID
            Case "btnSearchTysKaisya"
                objCd = tbxKaisya_cd
                objMei = tbxKaisya_mei
                kbn = "TysKaisya"
            Case "btnSearchKameiten"
                objCd = tbxKameiten_cd
                objMei = tbxKameiten_mei
                kbn = "Kameiten"
            Case "btnOpen"
                If hidBtn.Value = "K" Then
                    objCd = grdBody.Rows(hidRowIndex.Value).Cells(1).Controls(0)
                    objMei = grdBody.Rows(hidRowIndex.Value).Cells(1).Controls(2)
                    kbn = "Kameiten"
                Else
                    objCd = grdBody.Rows(hidRowIndex.Value).Cells(0).Controls(0)
                    objCd2 = grdBody.Rows(hidRowIndex.Value).Cells(0).Controls(2)
                    objMei = grdBody.Rows(hidRowIndex.Value).Cells(0).Controls(4)
                    kbn = "TysKaisya"
                End If


        End Select

        objMei.Text = ""
        If CType(sender, Button).ID = "btnOpen" And hidBtn.Value = "T" Then
            dtSyouhinTable = SelSearch(objCd.Text & objCd2.Text, kbn, CType(sender, Button).ID)
        Else
            dtSyouhinTable = SelSearch(objCd.Text, kbn, CType(sender, Button).ID)
        End If

        If dtSyouhinTable.Rows.Count = 1 Then
            If CType(sender, Button).ID = "btnOpen" And hidBtn.Value = "T" Then
                objCd.Text = dtSyouhinTable.Rows(0).Item(0).ToString
                objCd2.Text = dtSyouhinTable.Rows(0).Item(1).ToString
                objMei.Text = dtSyouhinTable.Rows(0).Item(2).ToString
            ElseIf CType(sender, Button).ID = "btnSearchTysKaisya" Then
                objCd.Text = dtSyouhinTable.Rows(0).Item(0).ToString & dtSyouhinTable.Rows(0).Item(1).ToString
                objMei.Text = dtSyouhinTable.Rows(0).Item(2).ToString
            Else

                objCd.Text = dtSyouhinTable.Rows(0).Item(0).ToString
                objMei.Text = dtSyouhinTable.Rows(0).Item(1).ToString
            End If
            Dim Scriptmangaer1 As Web.UI.ScriptManager = Web.UI.ScriptManager.GetCurrent(Page)

            If kbn = "TysKaisya" Then
                If CType(sender, Button).ID = "btnOpen" Then
                    Scriptmangaer1.SetFocus(grdBody.Rows(grdBody.Rows.Count - 1).Cells(1).Controls(0))
                End If
            Else
                If CType(sender, Button).ID = "btnOpen" Then
                    Scriptmangaer1.SetFocus(grdBody.Rows(grdBody.Rows.Count - 1).Cells(2).Controls(0))
                End If
            End If
            If CType(sender, Button).ID = "btnSearchTysKaisya" Then
                Scriptmangaer1.SetFocus(tbxKameiten_cd)
            End If
            If CType(sender, Button).ID = "btnSearchKameiten" Then
                Scriptmangaer1.SetFocus(btnSearch)
            End If
        Else
            If kbn = "Kameiten" Then
                If CType(sender, Button).ID = "btnSearchKameiten" Then
                    strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('â¡ñøìX')+'&blnDelete=False&FormName=" & _
                                Me.Page.Form.Name & "&objCd=" & _
                                objCd.ClientID & _
                                 "&objMei=" & objMei.ClientID & _
                                 "&strCd='+escape(eval('document.all.'+'" & _
                                 objCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                                 objMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
                Else
                    strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('â¡ñøìX')+'&blnDelete=True&FormName=" & _
                                Me.Page.Form.Name & "&objCd=" & _
                                objCd.ClientID & _
                                 "&objMei=" & objMei.ClientID & _
                                 "&strCd='+escape(eval('document.all.'+'" & _
                                 objCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                                 objMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
                End If
            Else
                If CType(sender, Button).ID = "btnSearchTysKaisya" Then
                    strScript = "objSrchWin = window.open('search_tyousa.aspx?blnDelete=False&FormName=" & _
                                Me.Page.Form.Name & "&objCd=" & _
                                objCd.ClientID & _
                                "&objMei=" & objMei.ClientID & _
                                "&strCd='+escape(eval('document.all.'+'" & _
                                objCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                                objMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
                Else
                    strScript = "objSrchWin = window.open('search_tyousa.aspx?blnDelete=True&FormName=" & _
                                Me.Page.Form.Name & "&objCd=" & _
                                objCd.ClientID & _
                                "&objMei=" & objMei.ClientID & _
                                "&objCd2=" & objCd2.ClientID & _
                                "&strCd='+escape(eval('document.all.'+'" & _
                                objCd.ClientID & "').value)+escape(eval('document.all.'+'" & _
                                objCd2.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                                objMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
                End If
            End If

        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strScript, True)

    End Sub
    Function SelSearch(ByVal objCd As String, ByVal kbn As String, ByVal btnId As String) As DataTable
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.Syouhin2MasterLogic
        Dim CommonSearchLogic2 As New Itis.Earth.BizLogic.SiireMasterLogic
        If kbn = "Kameiten" Then
            If btnId = "btnSearchKameiten" Then
                SelSearch = CommonSearchLogic.GetKameitenKensakuInfo(objCd, "")
            Else
                SelSearch = CommonSearchLogic.GetKameitenKensakuInfo(objCd, "0")
            End If

        Else
            If btnId = "btnSearchTysKaisya" Then
                SelSearch = CommonSearchLogic2.SelTyousaInfo(objCd, False)
            Else
                SelSearch = CommonSearchLogic2.SelTyousaInfo(objCd, True)
            End If

        End If

    End Function

    ''' <summary>GridViewï¿Ç◊ÇÈïùÇÃê›íË</summary>
    Function SetColWidth(ByRef intwidth() As String, ByVal strName As String) As Integer
        If strName = "head" Then

            'If CType(ViewState("dtTable"), DataTable).Rows.Count <> 1 Then
            '    intwidth(0) = "352px"
            '    intwidth(1) = "373px"
            '    intwidth(2) = "55px"
            'Else
            '    intwidth(0) = "353px"
            '    intwidth(1) = "373px"
            '    intwidth(2) = "55px"
            'End If
            intwidth(0) = "263px"
            intwidth(1) = "276px"
            intwidth(2) = "74px"
            intwidth(3) = "74px"
            intwidth(4) = "74px"
            intwidth(5) = "65px"
            'intwidth(6) = "89px"
        Else

            'intwidth(0) = "300px"
            'intwidth(1) = "300px"
            'intwidth(2) = "80px"
            'intwidth(3) = "80px"
            'intwidth(4) = "80px"
            'intwidth(5) = "80px"
            'intwidth(6) = "100px"
        End If
    End Function

    ''' <summary> GridViewÉwÉbÉ_Å[ïîÇÉZÉbÉg</summary>
    Sub SetHeadData()
        Dim CommonLG As New CommonLogic()
        grdHead.DataSource = CommonLG.CreateHeadDataSource("í≤ç∏âÔé–,â¡ñøìX,édì¸âøäiÇP" & vbCrLf & "(1Å`3ìè),édì¸âøäiÇQ" & vbCrLf & "(4Å`9ìè),édì¸âøäiÇR" & vbCrLf & "(10Å`19ìè),äoèëóLñ≥,èàóù")
        grdHead.DataBind()
    End Sub
    ''' <summary> GridViewì‡óeÅAÉtÉHÅ[É}ÉbÉgÇÉZÉbÉg</summary>
    Sub GridViewStyle(ByVal intwidth() As String, ByVal grd As GridView)
        Dim intCol As Integer = 0
        Dim intRow As Integer = 0

        grd.Attributes.Add("Width", "960px")
        If IsArray(intwidth) AndAlso grd.Rows.Count > 0 Then
            For intRow = 0 To grd.Rows.Count - 1
                For intCol = 0 To grd.Rows(intRow).Cells.Count - 1
                    grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")
                    grd.Rows(intRow).Cells(intCol).Attributes.Add("align", "left")
                Next
            Next
        End If
    End Sub
    Sub SetKakutyou(ByVal ddl As DropDownList)


        Dim ddlist As New ListItem
        ddlist = New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddl.Items.Add(ddlist)

        ddlist = New ListItem
        ddlist.Text = "ñ≥"
        ddlist.Value = "0"
        ddl.Items.Add(ddlist)

        ddlist = New ListItem
        ddlist.Text = "óL"
        ddlist.Value = "1"
        ddl.Items.Add(ddlist)

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

        Dim CommonSearchLogic As New Itis.Earth.BizLogic.SiireMasterLogic
        Dim dtTable As New DataTable
        Dim CommonLG As New CommonLogic()
        Dim intRow As Integer = 0
        Dim intColCount As Integer = 0
        If blnKousin Then
            ViewState("MeisaiID") = Nothing
            ViewState("UniqueID") = Nothing
            dtTable = CommonSearchLogic.SelSiireInfo(ViewState("Kaisya").ToString, ViewState("Kameiten"))
            If dtTable.Rows.Count = 0 Then
                If msg <> "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & msg & "');", True)
                End If
            End If
            dtTable.Rows.Add(dtTable.NewRow)
        Else
            dtTable = ViewState("dtTable")
        End If

        'Dim DT2 As New DataTable
        'DT2.Columns.Add(New DataColumn("col1".ToString, GetType(String)))
        'DT2.Columns.Add(New DataColumn("col2".ToString, GetType(String)))
        'DT2.Columns.Add(New DataColumn("col3".ToString, GetType(String)))
        'DT2.Columns.Add(New DataColumn("col4".ToString, GetType(String)))
        'DT2.Columns.Add(New DataColumn("col5".ToString, GetType(String)))
        'DT2.Columns.Add(New DataColumn("col6".ToString, GetType(String)))
        'For intRow = 0 To dtTable.Rows.Count - 1
        '    DT2.Rows.Add(DT2.NewRow)

        'Next
        grdBody.DataSource = dtTable
        grdBody.DataBind()
        SetbodyData = dtTable

        Dim strUniqueID As String = ""
        Dim MeisaiID As New Dictionary(Of String, String)
        For intRow = 0 To grdBody.Rows.Count - 1
            Dim arrRowId(8) As String
            strUniqueID = ""
            If intRow = grdBody.Rows.Count - 1 Then

                'í≤ç∏âÔé–
                Dim txtControlS As New TextBox
                Dim lblControl As New Label
                'í≤ç∏âÔé–ÉRÅ[Éh
                txtControlS.Width = Unit.Pixel(33)
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControlS.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(0))
                Else
                    txtControlS.Text = ""
                End If
                txtControlS.CssClass = "hissu"

                txtControlS.Style.Add("ime-mode", "disabled;")
                txtControlS.Attributes.Add("maxlength", "4")
                txtControlS.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControlS)
                arrRowId(0) = txtControlS.ClientID
                strUniqueID = strUniqueID & txtControlS.UniqueID & ","

                'Lable
                ' lblControl = New Label
                lblControl.Text = "-"
                lblControl.Width = Unit.Pixel(5)
                grdBody.Rows(intRow).Cells(0).Controls.Add(lblControl)


                'éñã∆èäÉRÅ[Éh
                Dim txtControlJ As New TextBox
                txtControlJ.Width = Unit.Pixel(14)
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControlJ.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(1))
                Else
                    txtControlJ.Text = ""
                End If
                txtControlJ.CssClass = "hissu"
                txtControlJ.Style.Add("ime-mode", "disabled")
                txtControlJ.Attributes.Add("maxlength", "2")
                txtControlJ.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControlJ)
                arrRowId(1) = txtControlJ.ClientID
                strUniqueID = strUniqueID & txtControlJ.UniqueID & ","

                'Lable
                ' lblControl = New Label
                'lblControl.Width = Unit.Pixel(5)
                'grdBody.Rows(intRow).Cells(0).Controls.Add(lblControl)



                Dim btnControl As New Button
                btnControl.Text = "åüçı"
                btnControl.Attributes.Add("value2", "åüçı")
                btnControl.Attributes.Add("onclick", "fncDisable();return fncOpenPop('T','" & intRow & "');")
                btnControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(btnControl)

                'Lable
                ' lblControl = New Label
                ' lblControl.Width = Unit.Pixel(5)
                'grdBody.Rows(intRow).Cells(0).Controls.Add(lblControl)
                'í≤ç∏âÔé–ñº
                Dim txtControl2 As New TextBox
                txtControl2.Width = Unit.Pixel(153)
                txtControl2.CssClass = "readOnlyStyle"
                txtControl2.Attributes.Add("readonly", "true")
                txtControl2.Attributes.Add("TabIndex", "-1")

                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl2.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(2))
                Else
                    txtControl2.Text = ""
                End If
                txtControl2.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl2)

                strUniqueID = strUniqueID & txtControl2.UniqueID & ","


                'â¡ñøìXÉRÅ[Éh
                Dim txtControlK As New TextBox
                txtControlK.Width = Unit.Pixel(38)
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControlK.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(3))
                Else
                    txtControlK.Text = ""
                End If
                txtControlK.CssClass = "hissu"
                txtControlK.Attributes.Add("maxlength", "5")
                txtControlK.Style.Add("ime-mode", "disabled")
                txtControlK.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtControlK)
                arrRowId(2) = txtControlK.ClientID
                strUniqueID = strUniqueID & txtControlK.UniqueID & ","
 
                'åüçıÉ{É^Éì
                btnControl = New Button
                btnControl.Text = "åüçı"
                btnControl.Attributes.Add("value2", "åüçı")
                btnControl.Attributes.Add("onclick", "fncDisable();return fncOpenPop('K','" & intRow & "');")
                btnControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(1).Controls.Add(btnControl)

                'Lable
                ' lblControl = New Label
                ' lblControl.Width = Unit.Pixel(5)
                'grdBody.Rows(intRow).Cells(1).Controls.Add(lblControl)
                'â¡ñøìXñº
                Dim txtControl3 As New TextBox
                txtControl3.Width = Unit.Pixel(189)
                txtControl3.CssClass = "readOnlyStyle"
                txtControl3.Attributes.Add("readonly", "true")
                txtControl3.Attributes.Add("TabIndex", "-1")
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl3.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(4))
                Else
                    txtControl3.Text = ""
                End If
                txtControl3.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtControl3)

                strUniqueID = strUniqueID & txtControl3.UniqueID & ","


                'édì¸âøäi1
                Dim txtControlK1 As New TextBox
                txtControlK1.Width = Unit.Pixel(72)
                txtControlK1.CssClass = "kingaku & hissu"
                txtControlK1.Attributes.Add("maxlength", "7")
                txtControlK1.Style.Add("ime-mode", "disabled")
                txtControlK1.Attributes.Add("onblur", "checkNumberAddFig(this);")
                txtControlK1.Attributes.Add("onfocus", "removeFig(this);")
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControlK1.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(5))
                Else
                    txtControlK1.Text = "0"
                End If
                grdBody.Rows(intRow).Cells(2).Controls.Add(txtControlK1)

                strUniqueID = strUniqueID & txtControlK1.UniqueID & ","
                arrRowId(3) = txtControlK1.ClientID

                'édì¸âøäi2
                Dim txtControlK2 As New TextBox
                txtControlK2.Width = Unit.Pixel(72)
                txtControlK2.CssClass = "kingaku & hissu"
                txtControlK2.Attributes.Add("maxlength", "7")
                txtControlK2.Style.Add("ime-mode", "disabled")
                txtControlK2.Attributes.Add("onblur", "checkNumberAddFig(this);")
                txtControlK2.Attributes.Add("onfocus", "removeFig(this);")

                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControlK2.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(6))
                Else
                    txtControlK2.Text = "0"
                End If
                grdBody.Rows(intRow).Cells(3).Controls.Add(txtControlK2)

                strUniqueID = strUniqueID & txtControlK2.UniqueID & ","
                arrRowId(4) = txtControlK2.ClientID

                'édì¸âøäi3
                Dim txtControlK3 As New TextBox
                txtControlK3.Width = Unit.Pixel(72)
                txtControlK3.CssClass = "kingaku & hissu"
                txtControlK3.Attributes.Add("maxlength", "7")
                txtControlK3.Style.Add("ime-mode", "disabled")
                txtControlK3.Attributes.Add("onblur", "checkNumberAddFig(this);")
                txtControlK3.Attributes.Add("onfocus", "removeFig(this);")

                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControlK3.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(7))
                Else
                    txtControlK3.Text = "0"
                End If
                grdBody.Rows(intRow).Cells(4).Controls.Add(txtControlK3)

                strUniqueID = strUniqueID & txtControlK3.UniqueID & ","
                arrRowId(5) = txtControlK3.ClientID

                'óLñ≥
                Dim ddlControl As New DropDownList
                SetKakutyou(ddlControl)
                ddlControl.Width = Unit.Pixel(68)

                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    SetDropSelect(ddlControl, Request.Form(Split(ViewState("UniqueID").ToString, ",")(8)))
                Else
                    ddlControl.SelectedIndex = 0
                End If
                ddlControl.Style.Add("margin-left", "1px")

                grdBody.Rows(intRow).Cells(5).Controls.Add(ddlControl)

                strUniqueID = strUniqueID & ddlControl.UniqueID & ","
                arrRowId(6) = ddlControl.ClientID


                arrRowId(7) = ""
                arrRowId(8) = intRow

                'ìoò^
                btnControl = New Button
                btnControl.Text = "ìoò^"
                btnControl.Attributes.Add("onclick", "fncDisable();return fncSetRowData('ìoò^','" & _
                                                        arrRowId(0) & "','" & _
                                                        arrRowId(1) & "','" & _
                                                        arrRowId(2) & "','" & _
                                                        arrRowId(3) & "','" & _
                                                        arrRowId(4) & "','" & _
                                                        arrRowId(5) & "','" & _
                                                        arrRowId(6) & "','" & _
                                                        arrRowId(7) & "','" & _
                                                        arrRowId(8) & "');")
                If Not blnBtn Then
                    btnControl.Enabled = False
                End If
                ' lblControl = New Label
                '  lblControl.Width = Unit.Pixel(4)
                '  grdBody.Rows(intRow).Cells(5).Controls.Add(lblControl)
                btnControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(6).Controls.Add(btnControl)
                If blnBtn Then
                    txtControlS.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControlJ.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControlK.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControlK1.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControlK2.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControlK3.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")

                End If
                'Lable
                Dim txt As New TextBox
                txt.Style.Add("border-style", "none;")
                txt.Style.Add("background-color", "transparent;")
                txt.Attributes.Add("TabIndex", "-1")
                txt.Width = Unit.Pixel(40)
                grdBody.Rows(intRow).Cells(6).Controls.Add(txt)

                ViewState("UniqueID") = strUniqueID
            Else
                'í≤ç∏âÔé–
                'í≤ç∏âÔé–ÉRÅ[Éh
                'Dim lblControl As New Label
                'Lable
                'lblControl.Width = Unit.Pixel(5)
                'grdBody.Rows(intRow).Cells(0).Controls.Add(lblControl)

                Dim txtControl As New TextBox
                txtControl.Width = Unit.Pixel(45)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Style.Add("border-bottom", "none;")
                txtControl.Attributes.Add("readonly", "true")
                txtControl.Attributes.Add("TabIndex", "-1")
                If ViewState("MeisaiID") Is Nothing Then
                    txtControl.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(0)), ",")(0)
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtControl.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(0))
                    Else
                        txtControl.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(0)), ",")(0)
                    End If
                End If
                txtControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl)
                arrRowId(0) = txtControl.Text
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"


                'label
                ' lblControl = New Label
                ' lblControl.Width = Unit.Pixel(20)
                'grdBody.Rows(intRow).Cells(0).Controls.Add(lblControl)
                'í≤ç∏âÔé–ñº
                txtControl = New TextBox
                txtControl.Width = Unit.Pixel(200)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Attributes.Add("readonly", "true")
                txtControl.Attributes.Add("TabIndex", "-1")
                If ViewState("MeisaiID") Is Nothing Then
                    txtControl.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(0)), ",")(1)
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtControl.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(1))
                    Else
                        txtControl.Text = CommonLG.getDisplayString(Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(0)), ",")(1))
                    End If
                End If
                txtControl.Style.Add("margin-left", "10px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl)
                arrRowId(1) = ""
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"
                'â¡ñøìX
                'â¡ñøìXÉRÅ[Éh
                ' lblControl = New Label
                ' lblControl.Width = Unit.Pixel(5)
                ' grdBody.Rows(intRow).Cells(1).Controls.Add(lblControl)
                txtControl = New TextBox
                txtControl.Width = Unit.Pixel(40)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Style.Add("border-bottom", "none;")
                txtControl.Attributes.Add("readonly", "true")
                txtControl.Attributes.Add("TabIndex", "-1")
                If ViewState("MeisaiID") Is Nothing Then
                    txtControl.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(1)), ",")(0)
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtControl.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(2))
                    Else
                        txtControl.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(1)), ",")(0)
                    End If
                End If
                txtControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtControl)
                arrRowId(2) = txtControl.Text
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"
                'label
                '  lblControl = New Label
                '  lblControl.Width = Unit.Pixel(20)
                '  grdBody.Rows(intRow).Cells(1).Controls.Add(lblControl)
                'â¡ñøìXñº
                txtControl = New TextBox
                txtControl.Width = Unit.Pixel(220)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Attributes.Add("readonly", "true")
                txtControl.Attributes.Add("TabIndex", "-1")
                If ViewState("MeisaiID") Is Nothing Then
                    txtControl.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(1)), ",")(1)
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtControl.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(3))
                    Else
                        txtControl.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(1)), ",")(1)
                    End If
                End If
                txtControl.Style.Add("margin-left", "10px")
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtControl)
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"
                'édì¸âøäi1
                Dim txtK1 As New TextBox
                txtK1.Width = Unit.Pixel(72)
                txtK1.CssClass = "kingaku & hissu"
                txtK1.Attributes.Add("maxlength", "7")
                txtK1.Style.Add("ime-mode", "disabled")
                txtK1.Attributes.Add("onblur", "checkNumberAddFig(this);")
                txtK1.Attributes.Add("onfocus", "removeFig(this);")
                If ViewState("MeisaiID") Is Nothing Then
                    txtK1.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(2))
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtK1.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(4))
                    Else
                        txtK1.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(2))
                    End If
                End If
                txtK1.Text = AddComa(txtK1.Text)
                grdBody.Rows(intRow).Cells(2).Controls.Add(txtK1)

                arrRowId(3) = txtK1.ClientID
                strUniqueID = strUniqueID & txtK1.UniqueID & "|"

                'édì¸âøäi2
                Dim txtK2 As New TextBox
                txtK2.Width = Unit.Pixel(72)
                txtK2.CssClass = "kingaku & hissu"
                txtK2.Attributes.Add("maxlength", "7")
                txtK2.Style.Add("ime-mode", "disabled")
                txtK2.Attributes.Add("onblur", "checkNumberAddFig(this);")
                txtK2.Attributes.Add("onfocus", "removeFig(this);")

                If ViewState("MeisaiID") Is Nothing Then
                    txtK2.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(3))
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtK2.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(5))
                    Else
                        txtK2.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(3))
                    End If
                End If
                txtK2.Text = AddComa(txtK2.Text)
                grdBody.Rows(intRow).Cells(3).Controls.Add(txtK2)

                arrRowId(4) = txtK2.ClientID
                strUniqueID = strUniqueID & txtK2.UniqueID & "|"

                'édì¸âøäi3
                Dim txtK3 As New TextBox
                txtK3.Width = Unit.Pixel(72)
                txtK3.CssClass = "kingaku & hissu"
                txtK3.Attributes.Add("maxlength", "7")
                txtK3.Style.Add("ime-mode", "disabled")
                txtK3.Attributes.Add("onblur", "checkNumberAddFig(this);")
                txtK3.Attributes.Add("onfocus", "removeFig(this);")
                If ViewState("MeisaiID") Is Nothing Then
                    txtK3.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(4))
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtK3.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(6))
                    Else
                        txtK3.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(4))
                    End If
                End If
                txtK3.Text = AddComa(txtK3.Text)
                grdBody.Rows(intRow).Cells(4).Controls.Add(txtK3)
                arrRowId(5) = txtK3.ClientID
                strUniqueID = strUniqueID & txtK3.UniqueID & "|"

                'édì¸âøäi3
                Dim ddlControl As New DropDownList
                SetKakutyou(ddlControl)
                ddlControl.Width = Unit.Pixel(68)

                If ViewState("MeisaiID") Is Nothing Then
                    SetDropSelect(ddlControl, CommonLG.getDisplayString(dtTable.Rows(intRow).Item(5)))

                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then

                        SetDropSelect(ddlControl, Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(7)))
                    Else
                        SetDropSelect(ddlControl, CommonLG.getDisplayString(dtTable.Rows(intRow).Item(5)))
                    End If
                End If
                ddlControl.Style.Add("margin-left", "1px")

                grdBody.Rows(intRow).Cells(5).Controls.Add(ddlControl)
                strUniqueID = strUniqueID & ddlControl.UniqueID & "|"

                arrRowId(6) = ddlControl.ClientID



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
                        If Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(8) = "" Then
                            btnControl.Enabled = Not CBool(hidBool.Value)
                        Else
                            btnControl.Enabled = Not CBool(Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(8)))
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
                '   lblControl = New Label
                ' lblControl.Width = Unit.Pixel(4)
                ' grdBody.Rows(intRow).Cells(5).Controls.Add(lblControl)
                btnControl.Style.Add("margin-left", "1px")
                hidTime.Value = dtTable.Rows(intRow).Item("upd_datetime").ToString
                grdBody.Rows(intRow).Cells(6).Controls.Add(btnControl)
                grdBody.Rows(intRow).Cells(6).Controls.Add(hidControl)
                grdBody.Rows(intRow).Cells(6).Controls.Add(hidTime)

                arrRowId(7) = hidTime.Value


                arrRowId(8) = intRow
                btnControl.Attributes.Add("onclick", "fncDisable();" & hidControl.ClientID & ".value=" & btnControl.ClientID & ".disabled;return fncSetRowData('èCê≥','" & _
                                                                        arrRowId(0) & "','" & _
                                                                        arrRowId(1) & "','" & _
                                                                        arrRowId(2) & "','" & _
                                                                        arrRowId(3) & "','" & _
                                                                        arrRowId(4) & "','" & _
                                                                        arrRowId(5) & "','" & _
                                                                        arrRowId(6) & "','" & _
                                                                        arrRowId(7) & "','" & _
                                                                        arrRowId(8) & "');")
                If blnBtn Then
                    txtK1.Attributes.Add("onPropertyChange", "fncButtonChange(this," & btnControl.ClientID & "," & hidControl.ClientID & ");")
                    txtK2.Attributes.Add("onPropertyChange", "fncButtonChange(this," & btnControl.ClientID & "," & hidControl.ClientID & ");")
                    txtK3.Attributes.Add("onPropertyChange", "fncButtonChange(this," & btnControl.ClientID & "," & hidControl.ClientID & ");")
                    ddlControl.Attributes.Add("onPropertyChange", "fncButtonChange(this," & btnControl.ClientID & "," & hidControl.ClientID & ");")

                    txtK1.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtK2.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtK3.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    ddlControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")

                End If

                'çÌèú

                btnControl = New Button
                btnControl.Text = "çÌèú"
                btnControl.Enabled = True

                If Not blnBtn Then
                    btnControl.Enabled = False
                End If

                ' lblControl = New Label
                '  lblControl.Width = Unit.Pixel(3)
                ' grdBody.Rows(intRow).Cells(5).Controls.Add(lblControl)
                btnControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(6).Controls.Add(btnControl)

                MeisaiID.Add(intRow, strUniqueID & hidControl.UniqueID & "|" & hidTime.UniqueID)

                btnControl.Attributes.Add("onclick", "if (confirm('ÉfÅ[É^ÇçÌèúÇµÇ‹Ç∑ÅBÇÊÇÎÇµÇ¢Ç≈Ç∑Ç©ÅH')){fncDisable();return fncSetRowData('çÌèú','" & _
                                                        arrRowId(0) & "','" & _
                                                        arrRowId(1) & "','" & _
                                                        arrRowId(2) & "','" & _
                                                        arrRowId(3) & "','" & _
                                                        arrRowId(4) & "','" & _
                                                        arrRowId(5) & "','" & _
                                                        arrRowId(6) & "','" & _
                                                        arrRowId(7) & "','" & _
                                                        arrRowId(8) & "');}else{return false;}")
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
            .AppendLine("function  fncOpenPop(strBtn,strRow)")
            .AppendLine("{")
            .AppendLine("document.getElementById ('" & hidBtn.ClientID & "').value=strBtn;")
            .AppendLine("document.getElementById ('" & hidRowIndex.ClientID & "').value=strRow;")
            .AppendLine("document.getElementById ('" & hidTop.ClientID & "').value=strRow*60;")
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
            .AppendLine("function  fncSetRowData(strBtn,strKaisya,strJigyousyo,strKameiten,strkkk1,strkkk2,strkkk3,strumu,strUPDTime,strRow)")
            .AppendLine("{")

            .AppendLine("document.getElementById ('" & hidBtn.ClientID & "').value=strBtn;")

            .AppendLine("if (strBtn=='ìoò^'){")
            .AppendLine("document.getElementById ('" & hidKaisya.ClientID & "').value=eval('document.all.'+strKaisya).value;")
            .AppendLine("document.getElementById ('" & hidJigyousyo.ClientID & "').value=eval('document.all.'+strJigyousyo).value;")
            .AppendLine("document.getElementById ('" & hidKameiten.ClientID & "').value=eval('document.all.'+strKameiten).value;")

            .AppendLine("document.getElementById ('" & hidTop.ClientID & "').value=strRow*60;")
            .AppendLine("}else{")
            .AppendLine("document.getElementById ('" & hidKaisya.ClientID & "').value=strKaisya;")
            .AppendLine("document.getElementById ('" & hidKameiten.ClientID & "').value=strKameiten;")

            .AppendLine("}")
            .AppendLine("document.getElementById ('" & hidkkk1.ClientID & "').value=eval('document.all.'+strkkk1).value;")
            .AppendLine("document.getElementById ('" & hidkkk2.ClientID & "').value=eval('document.all.'+strkkk2).value;")
            .AppendLine("document.getElementById ('" & hidkkk3.ClientID & "').value=eval('document.all.'+strkkk3).value;")
            .AppendLine("document.getElementById ('" & HidUmu.ClientID & "').value=eval('document.all.'+strumu).value;")
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

            .AppendLine(" document.getElementById('" & Me.tbxKaisya_cd.ClientID & "').value='';")
            .AppendLine(" document.getElementById('" & Me.tbxKaisya_mei.ClientID & "').value='';")
            .AppendLine(" document.getElementById('" & Me.tbxKameiten_cd.ClientID & "').value='';")
            .AppendLine(" document.getElementById('" & Me.tbxKameiten_mei.ClientID & "').value='';")
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
            '.AppendLine("var i;")
            '.AppendLine("if (document.forms[0].elements.length) ")
            '.AppendLine("	{")
            '.AppendLine("	for (i=1;i < document.forms[0].elements.length;i++) ")
            '.AppendLine("if (document.forms[0].elements[i].type=='submit') ")
            '.AppendLine("	{")
            '.AppendLine("if (document.forms[0].elements[i].value=='ìoò^' || document.forms[0].elements[i].value=='çÌèú' || document.forms[0].elements[i].value2=='åüçı' ) ")
            '.AppendLine("	{")
            '.AppendLine("	document.forms[0].elements[i].disabled = true;")
            '.AppendLine("	}	")
            '.AppendLine("	}	")
            '.AppendLine("	}	")
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
    Protected Function AddComa(ByVal kekka As String) As String
        If TrimNull(kekka) = "" Then
            Return ""
        Else
            Return CDbl(kekka).ToString("###,###,##0.#")
        End If

    End Function
    ''' <summary>ãÛîíÇçÌèú</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function
    Protected Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim commoncheck As New CommonCheck
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.SiireMasterLogic

        Dim dtHaita As New DataTable
        Dim strErr As String = ""
        Dim strCol As Integer = -1
        Dim dtTable As DataTable = CType(ViewState("dtTable"), DataTable)
        Dim strItem As Integer = 0
        If hidBtn.Value = "ìoò^" Then

            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidKaisya.Value, "í≤ç∏âÔé–ÉRÅ[Éh")
                If strErr <> "" Then
                    strCol = 0
                    strItem = 0
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.ChkHankakuEisuuji(hidKaisya.Value, "í≤ç∏âÔé–ÉRÅ[Éh")
                If strErr <> "" Then
                    strCol = 0
                    strItem = 0
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidJigyousyo.Value, "éñã∆èäÉRÅ[Éh")
                If strErr <> "" Then
                    strCol = 0
                    strItem = 2
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.ChkHankakuEisuuji(hidJigyousyo.Value, "éñã∆èäÉRÅ[Éh")
                If strErr <> "" Then
                    strCol = 0
                    strItem = 2
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidKameiten.Value, "â¡ñøìXÉRÅ[Éh")
                If strErr <> "" Then
                    strCol = 1
                    strItem = 0
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.ChkHankakuEisuuji(hidKameiten.Value, "â¡ñøìXÉRÅ[Éh")
                If strErr <> "" Then
                    strCol = 1
                    strItem = 0
                End If
            End If
            'édì¸âøäi1
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidkkk1.Value, "édì¸âøäiÇP")
                If strErr <> "" Then
                    strCol = 2
                End If
            End If
            If strErr = "" Then
                hidkkk1.Value = Replace(hidkkk1.Value, ",", "")
                strErr = commoncheck.CheckNum(hidkkk1.Value, "édì¸âøäiÇP", "1")
                If strErr <> "" Then
                    strCol = 2
                End If
            End If
            'édì¸âøäiÇQ
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidkkk2.Value, "édì¸âøäiÇQ")
                If strErr <> "" Then
                    strCol = 3
                End If
            End If
            If strErr = "" Then
                hidkkk2.Value = Replace(hidkkk2.Value, ",", "")
                strErr = commoncheck.CheckNum(hidkkk2.Value, "édì¸âøäiÇQ", "1")
                If strErr <> "" Then
                    strCol = 3
                End If
            End If
            'édì¸âøäiÇR
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidkkk3.Value, "édì¸âøäiÇR")
                If strErr <> "" Then
                    strCol = 4
                End If
            End If
            If strErr = "" Then
                hidkkk3.Value = Replace(hidkkk3.Value, ",", "")
                strErr = commoncheck.CheckNum(hidkkk3.Value, "édì¸âøäiÇR", "1")
                If strErr <> "" Then
                    strCol = 4
                End If
            End If

            If strErr = "" Then
                If SelSearch(hidKaisya.Value & hidJigyousyo.Value, "TysKaisya", "").Rows.Count = 0 Then
                    strErr = String.Format(Messages.Instance.MSG2036E, "í≤ç∏âÔé–É}ÉXÉ^").ToString
                    strCol = 0
                    strItem = 0
                End If
            End If
            If strErr = "" Then
                If SelSearch(hidKameiten.Value, "Kameiten", "").Rows.Count = 0 Then
                    strErr = String.Format(Messages.Instance.MSG2036E, "â¡ñøìXÉ}ÉXÉ^").ToString
                    strCol = 1
                    strItem = 0
                End If
            End If
            If strErr = "" Then
                If Not CommonSearchLogic.SelJyuufuku(hidKaisya.Value & hidJigyousyo.Value, hidKameiten.Value) Then
                    strErr = Messages.Instance.MSG2035E
                    strCol = 0
                    strItem = 0
                End If
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"

            Else
                Dim Syouhin2MasterLogic As New Itis.Earth.BizLogic.Syouhin2MasterLogic
                If CommonSearchLogic.InsSiire(hidKaisya.Value, hidJigyousyo.Value, hidKameiten.Value, hidkkk1.Value, hidkkk2.Value, hidkkk3.Value, HidUmu.Value, ViewState("UserId").ToString) Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "édì¸âøäiÉ}ÉXÉ^") & "');"
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(0) = hidKaisya.Value & "-" & hidJigyousyo.Value & "," & TrimNull(CommonSearchLogic.SelTyousaInfo(hidKaisya.Value & hidJigyousyo.Value, False).Rows(0).Item(2))
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(1) = hidKameiten.Value & "," & TrimNull(Syouhin2MasterLogic.GetKameitenKensakuInfo(hidKameiten.Value, "").Rows(0).Item(1))
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(2) = IIf(hidkkk1.Value = "", DBNull.Value, hidkkk1.Value)
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(3) = IIf(hidkkk2.Value = "", DBNull.Value, hidkkk2.Value)
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(4) = IIf(hidkkk3.Value = "", DBNull.Value, hidkkk3.Value)
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(5) = IIf(HidUmu.Value = "", DBNull.Value, HidUmu.Value)
                    dtTable.Rows.Add(dtTable.NewRow)
                    ViewState("dtTable") = dtTable
                    ViewState("UniqueID") = Nothing
                Else
                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "édì¸âøäiÉ}ÉXÉ^") & "');"
                    strCol = 0
                End If
            End If
        ElseIf hidBtn.Value = "èCê≥" Then
            'édì¸âøäi1
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidkkk1.Value, "édì¸âøäiÇP")
                If strErr <> "" Then
                    strCol = 2
                End If
            End If
            If strErr = "" Then
                hidkkk1.Value = Replace(hidkkk1.Value, ",", "")
                strErr = commoncheck.CheckNum(hidkkk1.Value, "édì¸âøäiÇP", "1")
                If strErr <> "" Then
                    strCol = 2
                End If
            End If
            'édì¸âøäiÇQ
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidkkk2.Value, "édì¸âøäiÇQ")
                If strErr <> "" Then
                    strCol = 3
                End If
            End If
            If strErr = "" Then
                hidkkk2.Value = Replace(hidkkk2.Value, ",", "")
                strErr = commoncheck.CheckNum(hidkkk2.Value, "édì¸âøäiÇQ", "1")
                If strErr <> "" Then
                    strCol = 3
                End If
            End If
            'édì¸âøäiÇR
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidkkk3.Value, "édì¸âøäiÇR")
                If strErr <> "" Then
                    strCol = 4
                End If
            End If
            If strErr = "" Then
                hidkkk3.Value = Replace(hidkkk3.Value, ",", "")
                strErr = commoncheck.CheckNum(hidkkk3.Value, "édì¸âøäiÇR", "1")
                If strErr <> "" Then
                    strCol = 4
                End If
            End If

            If strErr = "" Then
                dtHaita = CommonSearchLogic.SelHaita(Replace(hidKaisya.Value, "-", ""), hidKameiten.Value, hidUPDTime.Value)
                If dtHaita.Rows.Count <> 0 Then
                    strErr = String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "édì¸âøäiÉ}ÉXÉ^").ToString()

                End If
            End If

            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"
                hidBool.Value = "False"
            Else
                Dim intRturn As Integer
                intRturn = CommonSearchLogic.UpdSiire(Split(hidKaisya.Value, "-")(0), Split(hidKaisya.Value, "-")(1), hidKameiten.Value, hidkkk1.Value, hidkkk2.Value, hidkkk3.Value, HidUmu.Value, ViewState("UserId").ToString)
                'ê≥èÌèIóπÇµÇΩèÍçá
                If intRturn = 1 Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "édì¸âøäiÉ}ÉXÉ^") & "');"
                    dtTable.Rows(hidRowIndex.Value).Item(2) = IIf(hidkkk1.Value = "", DBNull.Value, hidkkk1.Value)
                    dtTable.Rows(hidRowIndex.Value).Item(3) = IIf(hidkkk2.Value = "", DBNull.Value, hidkkk2.Value)
                    dtTable.Rows(hidRowIndex.Value).Item(4) = IIf(hidkkk3.Value = "", DBNull.Value, hidkkk3.Value)
                    dtTable.Rows(hidRowIndex.Value).Item(5) = IIf(HidUmu.Value = "", DBNull.Value, HidUmu.Value)
                    dtTable.Rows(hidRowIndex.Value).Item(6) = CommonSearchLogic.SelSiireInfo(Replace(hidKaisya.Value, "-", ""), hidKameiten.Value).Rows(0).Item(6).ToString
                    hidBool.Value = "True"

                ElseIf intRturn = 2 Then
                    strErr = "alert('" & Messages.Instance.MSG2049E & "');"
                    hidBool.Value = "False"

                Else

                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "édì¸âøäiÉ}ÉXÉ^") & "');"
                    hidBool.Value = "False"

                End If
                strCol = 2
            End If
            Dim strMeisai As String()
            strMeisai = Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(hidRowIndex.Value).ToString, "|")
            'èCê≥ÇÃClientID
            strMeisai(8) = ""
            CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(hidRowIndex.Value) = Join(strMeisai, "|")


        ElseIf hidBtn.Value = "çÌèú" Then
            If strErr = "" Then
                dtHaita = CommonSearchLogic.SelHaita(Replace(hidKaisya.Value, "-", ""), hidKameiten.Value, hidUPDTime.Value)
                If dtHaita.Rows.Count <> 0 Then
                    strErr = String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "édì¸âøäiÉ}ÉXÉ^").ToString()

                End If
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"
            Else
                'ê≥èÌèIóπÇµÇΩèÍçá
                If CommonSearchLogic.DelKeiretu(Replace(hidKaisya.Value, "-", ""), hidKameiten.Value) Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "édì¸âøäiÉ}ÉXÉ^") & "');"
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
                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "édì¸âøäiÉ}ÉXÉ^") & "');"

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

    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim commoncheck As New CommonCheck
        Dim strErr As String = ""
        Dim txtObj As New TextBox
        Dim dt As New DataTable
        If strErr = "" Then
            If (tbxKaisya_cd.Text & tbxKameiten_cd.Text).Trim = "" Then
                strErr = Messages.Instance.MSG2037E
                txtObj = tbxKaisya_cd
            End If
        End If

        If strErr = "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxKaisya_cd.Text, "í≤ç∏âÔé–ÉRÅ[Éh")
            txtObj = tbxKaisya_cd
        End If
        If strErr = "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxKameiten_cd.Text, "â¡ñøìXÉRÅ[Éh")
            txtObj = tbxKameiten_cd
        End If

        If strErr <> "" Then
            strErr = "alert('" & strErr & "');document.getElementById('" & txtObj.ClientID & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
            SetHeadData()
            SetbodyData(False, Messages.Instance.MSG2034E)
        Else
            tbxKaisya_mei.Text = ""
            tbxKameiten_mei.Text = ""
            If tbxKameiten_cd.Text.Trim <> "" Then
                dt = SelSearch(tbxKameiten_cd.Text, "Kameiten", "btnSearchKameiten")
                If dt.Rows.Count = 1 Then
                    tbxKameiten_mei.Text = dt.Rows(0).Item(1)
                End If
            End If
            If tbxKaisya_cd.Text.Trim <> "" Then
                dt = SelSearch(tbxKaisya_cd.Text, "TysKaisya", "btnSearchTysKaisya")
                If dt.Rows.Count = 1 Then
                    tbxKaisya_cd.Text = dt.Rows(0).Item(0).ToString & dt.Rows(0).Item(1).ToString
                    tbxKaisya_mei.Text = dt.Rows(0).Item(2)
                End If
            End If
            ViewState("Kameiten") = tbxKameiten_cd.Text
            ViewState("Kaisya") = tbxKaisya_cd.Text
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
    Protected Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Server.Transfer("MasterMainteMenu.aspx")
    End Sub
End Class