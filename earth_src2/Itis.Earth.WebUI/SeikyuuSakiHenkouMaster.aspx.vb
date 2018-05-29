Imports Itis.Earth.BizLogic
Partial Public Class SeikyuuSakiHenkouMaster
    Inherits System.Web.UI.Page
    Private blnBtn As Boolean
    Private intWidth(4) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strNinsyou As String = ""
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        '権限チェックおよび設定
        blnBtn = commonChk.CommonNinnsyou(strUserID, "keiri_gyoumu_kengen")
        ViewState("UserId") = strUserID
        If Not IsPostBack Then
            ViewState("Kameiten") = ""
            ViewState("Syouhin") = ""
            ViewState("UniqueID") = Nothing
            ViewState("MeisaiID") = Nothing

            SetHeadData()
            ViewState("dtTable") = SetbodyData(True)

            SetColWidth(intWidth, "body")
            GridViewStyle(intWidth, grdBody)

            SetColWidth(intWidth, "head")
            GridViewStyle(intWidth, grdHead)
            UpdatePanelA.Update()

            SetKakutyou(ddlSyouhinKBN, "43")
            '  ScriptManager.RegisterStartupScript(Me, Me.GetType(), "txtfocus", " window.setTimeout('objEBI(\'" & grdBody.Rows(grdBody.Rows.Count - 1).Cells(0).Controls(0).ClientID & "\').focus()',500); ", True)

        Else
            closecover()
        End If
        divMeisai.Attributes.Add("style", "overflow-y:auto ; height:352px;width:1070px;")
        MakeScript()

        tbxKameiten_mei.Attributes.Add("readonly", "true")
        btnSearch.Attributes.Add("onclick", "document.getElementById ('" & hidTop.ClientID & "').value =0;")
        Me.divMeisai.Attributes.Add("onscroll", "document.getElementById ('" & hidTop.ClientID & "').value =" & Me.divMeisai.ClientID & ".scrollTop;")
        btnClear.Attributes.Add("onclick", "return itemclear();")

        Me.btnChangeColor.Attributes.Add("onClick", "fncChangeColor('" & Me.hidTorikesi.ClientID & "','" & Me.tbxKameiten_cd.ClientID & "','" & tbxKameiten_mei.ClientID & "','" & tbxTorikesi.ClientID & "');return false;")

        Call Me.SetColor(Me.hidTorikesi, Me.tbxKameiten_cd, Me.tbxKameiten_mei, Me.tbxTorikesi)

    End Sub

    Protected Sub btnCommonSearch(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchKameiten.Click, btnOpen.Click
        Dim strScript As String = ""
        Dim dtSyouhinTable As New DataTable
        Dim objCd As New TextBox
        Dim objCd2 As New TextBox
        Dim objCd3 As New DropDownList
        Dim objMei As New TextBox
        Dim kbn As String = ""

        Dim objHidTorikesi As HiddenField = Nothing
        Dim objTxtTorikesi As TextBox = Nothing
        Dim objBtnTorikesi As Button = Nothing



        SetHeadData()
        SetbodyData(False)

        SetColWidth(intWidth, "body")
        GridViewStyle(intWidth, grdBody)

        SetColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)

        Select Case CType(sender, Button).ID

            Case "btnSearchKameiten"
                objCd = tbxKameiten_cd
                objMei = tbxKameiten_mei

                objTxtTorikesi = Me.tbxTorikesi
                objHidTorikesi = Me.hidTorikesi
                objBtnTorikesi = Me.btnChangeColor

                kbn = "Kameiten"
            Case "btnOpen"
                If hidBtn.Value = "K" Then
                    objCd = grdBody.Rows(hidRowIndex.Value).Cells(0).Controls(0)
                    objMei = grdBody.Rows(hidRowIndex.Value).Cells(0).Controls(2)

                    objTxtTorikesi = grdBody.Rows(hidRowIndex.Value).Cells(1).Controls(0)
                    objHidTorikesi = grdBody.Rows(hidRowIndex.Value).Cells(1).Controls(1)
                    objBtnTorikesi = grdBody.Rows(hidRowIndex.Value).Cells(1).Controls(2)

                    kbn = "Kameiten"
                End If

                If hidBtn.Value = "S" Then
                    objCd = grdBody.Rows(hidRowIndex.Value).Cells(3).Controls(1)
                    objCd2 = grdBody.Rows(hidRowIndex.Value).Cells(3).Controls(3)
                    objCd3 = grdBody.Rows(hidRowIndex.Value).Cells(3).Controls(0)

                    objMei = grdBody.Rows(hidRowIndex.Value).Cells(3).Controls(5)
                    kbn = "SeikyuuSaki"
                End If
        End Select

        objMei.Text = ""

        If kbn = "Kameiten" Then

            objHidTorikesi.Value = String.Empty
            objTxtTorikesi.Text = String.Empty

            dtSyouhinTable = SelSearch(objCd.Text, kbn, CType(sender, Button).ID)
        Else
            dtSyouhinTable = SelSearch(objCd.Text & "," & objCd2.Text & "," & objCd3.SelectedValue, kbn, CType(sender, Button).ID)
        End If




        If dtSyouhinTable.Rows.Count = 1 Then
            If kbn = "Kameiten" Then
                objCd.Text = dtSyouhinTable.Rows(0).Item(0).ToString.Trim
                objMei.Text = dtSyouhinTable.Rows(0).Item(1).ToString.Trim

                objHidTorikesi.Value = dtSyouhinTable.Rows(0).Item("torikesi").ToString.Trim
                If dtSyouhinTable.Rows(0).Item("torikesi").ToString.Trim.Equals("0") Then
                    objTxtTorikesi.Text = String.Empty
                Else
                    objTxtTorikesi.Text = dtSyouhinTable.Rows(0).Item("torikesi").ToString.Trim & ":" & dtSyouhinTable.Rows(0).Item("torikesi_txt").ToString.Trim
                End If

            Else
                objMei.Text = dtSyouhinTable.Rows(0).Item(0).ToString.Trim
                SetDropSelect(objCd3, dtSyouhinTable.Rows(0).Item(1).ToString.Trim())
                objCd.Text = dtSyouhinTable.Rows(0).Item(2).ToString.Trim
                objCd2.Text = dtSyouhinTable.Rows(0).Item(3).ToString.Trim
            End If

            Dim Scriptmangaer1 As Web.UI.ScriptManager = Web.UI.ScriptManager.GetCurrent(Page)
            If kbn = "Kameiten" Then
                If CType(sender, Button).ID = "btnSearchKameiten" Then
                    Scriptmangaer1.SetFocus(ddlSyouhinKBN)
                Else
                    Scriptmangaer1.SetFocus(grdBody.Rows(hidRowIndex.Value).Cells(2).Controls(0))
                End If

            Else
                Scriptmangaer1.SetFocus(grdBody.Rows(hidRowIndex.Value).Cells(4).Controls(0))
            End If
        Else
            If kbn = "Kameiten" Then
                If CType(sender, Button).ID = "btnSearchKameiten" Then
                    strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('加盟店')+'&blnDelete=False&FormName=" & Me.Page.Form.Name & _
                                 "&objCd=" & objCd.ClientID & _
                                 "&objMei=" & objMei.ClientID & _
                                 "&HidTorikesiCd=" & objHidTorikesi.ClientID & _
                                 "&TxtdTorikesiCd=" & objTxtTorikesi.ClientID & _
                                 "&btnChangeColorCd=" & objBtnTorikesi.ClientID & _
                                 "&strCd='+escape(eval('document.all.'+'" & objCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & objMei.ClientID & "').value)," & _
                                 " 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
                Else
                    strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('加盟店')+'&blnDelete=False&FormName=" & Me.Page.Form.Name & _
                                 "&objCd=" & objCd.ClientID & _
                                 "&objMei=" & objMei.ClientID & _
                                 "&HidTorikesiCd=" & objHidTorikesi.ClientID & _
                                 "&TxtdTorikesiCd=" & objTxtTorikesi.ClientID & _
                                 "&btnChangeColorCd=" & objBtnTorikesi.ClientID & _
                                 "&strCd='+escape(eval('document.all.'+'" & objCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & objMei.ClientID & "').value)," & _
                                 " 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
                End If

            Else
                strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=True&objKbn=" & _
                                               objCd3.ClientID & _
                                                "&objMei=" & objMei.ClientID & _
                                                "&objCd=" & objCd.ClientID & _
                                                "&objBrc=" & objCd2.ClientID & _
                                                "&strKbn='+escape(eval('document.all.'+'" & _
                                                objCd3.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
                                                objCd.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
                                                objCd2.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"

            End If

        End If
        If kbn = "Kameiten" Then
            Call Me.SetColor(objHidTorikesi, objCd, objMei, objTxtTorikesi)
        End If

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strScript, True)

    End Sub
    Function SelSearch(ByVal objCd As String, ByVal kbn As String, ByVal btnId As String) As DataTable
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.Syouhin2MasterLogic
        Dim CommonSearchLogic2 As New Itis.Earth.BizLogic.SeikyuuSakiHenkouLogic
        If kbn = "Kameiten" Then
            'If btnId = "btnSearchKameiten" Then
            '    SelSearch = CommonSearchLogic.GetKameitenKensakuInfo(objCd, "")
            'Else
            '    SelSearch = CommonSearchLogic.GetKameitenKensakuInfo(objCd, "0")
            'End If
            SelSearch = CommonSearchLogic.GetKameitenKensakuInfo(objCd, "")
        Else
            SelSearch = CommonSearchLogic2.SelSeikyuuSakiMei(Split(objCd, ",")(0), Split(objCd, ",")(1), Split(objCd, ",")(2), "0")
        End If

    End Function
    ''' <summary>GridView並べる幅の設定</summary>
    Function SetColWidth(ByRef intwidth() As String, ByVal strName As String) As Integer
        If strName = "head" Then

            intwidth(0) = "254px"
            intwidth(1) = "74px"
            intwidth(2) = "204px"
            intwidth(3) = "422px"
            intwidth(4) = "1px"
        Else
            intwidth(0) = "260px"
            intwidth(1) = "80px"
            intwidth(2) = "210px"
            intwidth(3) = "428px"
            intwidth(4) = "1px"
        End If
    End Function
    ''' <summary> GridViewヘッダー部をセット</summary>
    Sub SetHeadData()
        Dim CommonLG As New CommonLogic()
        grdHead.DataSource = CommonLG.CreateHeadDataSource("加盟店,加盟店取消,商品区分,請求先変更,処理")
        grdHead.DataBind()
    End Sub

    Sub SetKakutyou(ByVal ddl As DropDownList, ByVal strSyubetu As String)
        Dim SyouhinSearchLogic As New Itis.Earth.BizLogic.SyouhinMasterLogic
        Dim dtTable As New DataTable
        Dim intCount As Integer = 0
        dtTable = SyouhinSearchLogic.SelKakutyouInfo(strSyubetu)

        Dim ddlist As New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddl.Items.Add(ddlist)
        If strSyubetu = "43" Then
            For intCount = 0 To dtTable.Rows.Count - 1
                ddlist = New ListItem
                ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("code")) & ":" & TrimNull(dtTable.Rows(intCount).Item("meisyou"))
                ddlist.Value = TrimNull(dtTable.Rows(intCount).Item("code"))
                ddl.Items.Add(ddlist)
            Next
        Else
            ddlist = New ListItem
            ddlist.Text = "加盟店"
            ddlist.Value = "0"
            ddl.Items.Add(ddlist)
            ddlist = New ListItem
            ddlist.Text = "調査会社"
            ddlist.Value = "1"
            ddl.Items.Add(ddlist)
            ddlist = New ListItem
            ddlist.Text = "営業所"
            ddlist.Value = "2"
            ddl.Items.Add(ddlist)

        End If

    End Sub
    Sub SetDropSelect(ByVal ddl As DropDownList, ByVal strValue As String)
        If ddl.Items.FindByValue(strValue) IsNot Nothing Then
            ddl.SelectedValue = strValue
        Else
            ddl.SelectedIndex = 0
        End If
    End Sub
    ''' <summary>空白を削除</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function
    ''' <summary> GridView内容、フォーマットをセット</summary>
    Sub GridViewStyle(ByVal intwidth() As String, ByVal grd As GridView)
        Dim intCol As Integer = 0
        Dim intRow As Integer = 0

        grd.Attributes.Add("Width", "1070px")
        If IsArray(intwidth) AndAlso grd.Rows.Count > 0 Then
            For intRow = 0 To grd.Rows.Count - 1
                For intCol = 0 To grd.Rows(intRow).Cells.Count - 1
                    grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;")
                    If intCol <> grd.Rows(intRow).Cells.Count - 1 Then
                        grd.Rows(intRow).Cells(intCol).Style.Add("width", intwidth(intCol))
                    End If
                    grd.Rows(intRow).Cells(intCol).Attributes.Add("align", "left")
                Next
            Next
        End If
    End Sub

    ''' <summary> GridViewBODY部をセット</summary>
    Function SetbodyData(ByVal blnKousin As Boolean, Optional ByVal msg As String = "") As DataTable

        Dim CommonSearchLogic As New Itis.Earth.BizLogic.SeikyuuSakiHenkouLogic
        Dim dtTable As New DataTable
        Dim CommonLG As New CommonLogic()
        Dim intRow As Integer = 0
        Dim intColCount As Integer = 0
        If blnKousin Then
            ViewState("MeisaiID") = Nothing
            ViewState("UniqueID") = Nothing
            dtTable = CommonSearchLogic.SelSeikyuuInfo(ViewState("Kameiten").ToString, ViewState("Syouhin").ToString)
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
            Dim arrRowId(7) As String
            strUniqueID = ""
            If intRow = grdBody.Rows.Count - 1 Then


                '加盟店コード
                Dim txtControl2 As New TextBox
                txtControl2.Width = Unit.Pixel(38)
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl2.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(0))
                Else
                    txtControl2.Text = ""
                End If
                txtControl2.CssClass = "hissu"
                txtControl2.Attributes.Add("maxlength", "5")
                txtControl2.Style.Add("ime-mode", "disabled")
                'txtControl2.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl2)
                arrRowId(0) = txtControl2.ClientID
                strUniqueID = strUniqueID & txtControl2.UniqueID & ","


                '検索ボタン
                Dim btnControl As New Button
                btnControl.Text = "検索"
                btnControl.Attributes.Add("value2", "検索")
                btnControl.Attributes.Add("onclick", "fncDisable();return fncOpenPop('K','" & intRow & "','登録');")
                'btnControl.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(btnControl)


                '加盟店名
                Dim txtControl3 As New TextBox
                txtControl3.Width = Unit.Pixel(175)
                txtControl3.CssClass = "readOnlyStyle"
                txtControl3.Attributes.Add("readonly", "true")
                txtControl3.Attributes.Add("TabIndex", "-1")
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl3.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(1))
                Else
                    txtControl3.Text = ""
                End If
                'txtControl3.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl3)

                strUniqueID = strUniqueID & txtControl3.UniqueID & ","

                '==============2012/03/30 車龍 405721案件の対応 追加↓=============================
                '取消
                Dim txtTorikesi As New TextBox
                txtTorikesi.Width = Unit.Pixel(77)
                txtTorikesi.CssClass = "readOnlyStyle"
                txtTorikesi.Attributes.Add("readonly", "true")
                txtTorikesi.Attributes.Add("TabIndex", "-1")
                txtTorikesi.Style.Add("border-bottom ", "none")
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtTorikesi.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(8))
                Else
                    txtTorikesi.Text = ""
                End If
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtTorikesi)

                Dim hidTorikesi As New HiddenField
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    hidTorikesi.Value = Request.Form(Split(ViewState("UniqueID").ToString, ",")(7))
                Else
                    hidTorikesi.Value = ""
                End If
                grdBody.Rows(intRow).Cells(1).Controls.Add(hidTorikesi)

                Dim btntorikesi As New Button
                btntorikesi.Style.Add("display", "none")
                btntorikesi.Attributes.Add("onClick", "fncChangeColor('" & hidTorikesi.ClientID & "','" & txtControl2.ClientID & "','" & txtControl3.ClientID & "','" & txtTorikesi.ClientID & "');return false;")
                grdBody.Rows(intRow).Cells(1).Controls.Add(btntorikesi)

                Call Me.SetColor(hidTorikesi, txtControl2, txtControl3, txtTorikesi)
                '==============2012/03/30 車龍 405721案件の対応 追加↑=============================

                '商品区分
                Dim ddlControl2 As New DropDownList
                SetKakutyou(ddlControl2, "43")
                ddlControl2.CssClass = "hissu"

                ddlControl2.Width = Unit.Pixel(210)

                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    SetDropSelect(ddlControl2, Request.Form(Split(ViewState("UniqueID").ToString, ",")(2)))
                Else
                    ddlControl2.SelectedIndex = 0
                End If
                'ddlControl2.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(2).Controls.Add(ddlControl2)
                strUniqueID = strUniqueID & ddlControl2.UniqueID & ","
                arrRowId(1) = ddlControl2.ClientID

                '請求先区分
                Dim ddlControl1 As New DropDownList
                SetKakutyou(ddlControl1, "0")
                ddlControl1.CssClass = "hissu"

                ddlControl1.Width = Unit.Pixel(80)

                'ddlControl1.Style.Add("margin-left", "5px")
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    SetDropSelect(ddlControl1, Request.Form(Split(ViewState("UniqueID").ToString, ",")(3)))
                Else
                    ddlControl1.SelectedIndex = 0
                End If
                grdBody.Rows(intRow).Cells(3).Controls.Add(ddlControl1)
                strUniqueID = strUniqueID & ddlControl1.UniqueID & ","
                arrRowId(2) = ddlControl1.ClientID

                '請求変更先

                Dim txtControl1 As New TextBox
                txtControl1.Width = Unit.Pixel(38)
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl1.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(4))
                Else
                    txtControl1.Text = ""
                End If
                txtControl1.Text = txtControl1.Text.ToUpper
                txtControl1.CssClass = "hissu"
                'txtControl1.Attributes.Add("maxlength", "5")
                txtControl1.Style.Add("ime-mode", "disabled")
                'txtControl1.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(txtControl1)
                strUniqueID = strUniqueID & txtControl1.UniqueID & ","
                arrRowId(3) = txtControl1.ClientID

                'Lable
                Dim lblControl As New Label
                lblControl.Text = "-"
                lblControl.Width = Unit.Pixel(5)
                grdBody.Rows(intRow).Cells(3).Controls.Add(lblControl)

                '枝番
                Dim txtControl4 As New TextBox
                txtControl4.Width = Unit.Pixel(18)
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl4.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(5))
                Else
                    txtControl4.Text = ""
                End If
                txtControl4.CssClass = "hissu"

                txtControl4.Style.Add("ime-mode", "disabled;")
                txtControl4.Attributes.Add("maxlength", "2")
                'txtControl4.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(txtControl4)
                arrRowId(4) = txtControl4.ClientID
                strUniqueID = strUniqueID & txtControl4.UniqueID & ","
                '検索
                btnControl = New Button
                btnControl.Text = "検索"
                btnControl.Attributes.Add("value2", "検索")
                btnControl.Attributes.Add("onclick", "fncDisable();return fncOpenPop('S','" & intRow & "','登録');")
                'btnControl.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(btnControl)

                '請求先名
                Dim txtControl As New TextBox
                txtControl.Width = Unit.Pixel(229)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Attributes.Add("readonly", "true")
                txtControl.Attributes.Add("TabIndex", "-1")
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(6))
                Else
                    txtControl.Text = ""
                End If
                'txtControl.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(txtControl)

                strUniqueID = strUniqueID & txtControl.UniqueID & ","


                arrRowId(5) = ""
                arrRowId(6) = intRow
                arrRowId(7) = ""
                '登録
                btnControl = New Button
                btnControl.Text = "登録"
                btnControl.Attributes.Add("onclick", "fncDisable();return fncSetRowData('登録','" & _
                                                        arrRowId(0) & "','" & _
                                                        arrRowId(1) & "','" & _
                                                        arrRowId(2) & "','" & _
                                                        arrRowId(3) & "','" & _
                                                        arrRowId(4) & "','" & _
                                                        arrRowId(5) & "','" & _
                                                        arrRowId(6) & "','" & _
                                                        arrRowId(7) & "','" & _
                                                        txtTorikesi.ClientID & "');")
                If Not blnBtn Then
                    btnControl.Enabled = False
                End If

                'btnControl.Style.Add("margin-left", "4px")
                grdBody.Rows(intRow).Cells(4).Controls.Add(btnControl)
                If blnBtn Then
                    txtControl2.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControl1.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControl4.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    ddlControl2.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    ddlControl1.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")

                End If
                'Lable
                Dim txt As New TextBox
                txt.Style.Add("border-style", "none;")
                txt.Style.Add("background-color", "transparent;")
                txt.Attributes.Add("TabIndex", "-1")
                txt.ReadOnly = True
                txt.Width = Unit.Pixel(40)
                grdBody.Rows(intRow).Cells(4).Controls.Add(txt)

                strUniqueID = strUniqueID & hidTorikesi.UniqueID & "," & txtTorikesi.UniqueID & ","

                ViewState("UniqueID") = strUniqueID
            Else

                '加盟店
                '加盟店コード
                Dim txtControl As New TextBox
                txtControl.Width = Unit.Pixel(40)
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
                'txtControl.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl)
                arrRowId(0) = txtControl.Text
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"

                '加盟店名
                Dim txtControl1 As New TextBox
                txtControl1.Width = Unit.Pixel(206)
                txtControl1.CssClass = "readOnlyStyle"
                txtControl1.Attributes.Add("readonly", "true")
                txtControl1.Attributes.Add("TabIndex", "-1")
                If ViewState("MeisaiID") Is Nothing Then
                    txtControl1.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(0)), ",")(1)
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtControl1.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(1))
                    Else
                        txtControl1.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(0)), ",")(1)
                    End If
                End If
                txtControl1.Style.Add("margin-left", "10px")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl1)
                strUniqueID = strUniqueID & txtControl1.UniqueID & "|"

                '==============2012/03/30 車龍 405721案件の対応 追加↓=============================
                '取消
                Dim txtTorikesi As New TextBox
                txtTorikesi.Width = Unit.Pixel(77)
                txtTorikesi.CssClass = "readOnlyStyle"
                txtTorikesi.Attributes.Add("readonly", "true")
                txtTorikesi.Attributes.Add("TabIndex", "-1")
                txtTorikesi.Style.Add("border-bottom ", "none")

                Dim strTorikesi() As String
                strTorikesi = CommonLG.getDisplayString(dtTable.Rows(intRow).Item("torikesi")).Split(":")
                If strTorikesi(0).Trim.Equals("0") Then
                    txtTorikesi.Text = String.Empty
                Else
                    txtTorikesi.Text = Join(strTorikesi, ":")
                End If
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtTorikesi)

                Dim hidTorikesi As New HiddenField
                hidTorikesi.Value = strTorikesi(0)
                grdBody.Rows(intRow).Cells(1).Controls.Add(hidTorikesi)

                Call Me.SetColor(hidTorikesi, txtControl, txtControl1, txtTorikesi)
                '==============2012/03/30 車龍 405721案件の対応 追加↑=============================

                '商品区分
                Dim ddlControl As New DropDownList
                SetKakutyou(ddlControl, "43")
                ddlControl.CssClass = "hissu"

                ddlControl.Width = Unit.Pixel(210)

                If ViewState("MeisaiID") Is Nothing Then
                    SetDropSelect(ddlControl, CommonLG.getDisplayString(dtTable.Rows(intRow).Item(2)))

                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then

                        SetDropSelect(ddlControl, Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(2)))
                    Else
                        SetDropSelect(ddlControl, CommonLG.getDisplayString(dtTable.Rows(intRow).Item(2)))
                    End If
                End If
                'ddlControl.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(2).Controls.Add(ddlControl)
                strUniqueID = strUniqueID & ddlControl.UniqueID & "|"
                arrRowId(1) = ddlControl.ClientID
                arrRowId(7) = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(2)).ToString
                '=======================================================

                '請求先区分
                Dim ddlControl2 As New DropDownList
                SetKakutyou(ddlControl2, "0")
                ddlControl2.CssClass = "hissu"

                ddlControl2.Width = Unit.Pixel(80)

                'ddlControl2.Style.Add("margin-left", "5px")
                If ViewState("MeisaiID") Is Nothing Then
                    SetDropSelect(ddlControl2, Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(3)), ",")(0))

                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then

                        SetDropSelect(ddlControl2, Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(3)))
                    Else
                        SetDropSelect(ddlControl2, Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(3)), ",")(0))
                    End If
                End If
                grdBody.Rows(intRow).Cells(3).Controls.Add(ddlControl2)
                strUniqueID = strUniqueID & ddlControl2.UniqueID & "|"
                arrRowId(2) = ddlControl2.ClientID

                '請求変更先

                Dim txtControl2 As New TextBox
                txtControl2.Width = Unit.Pixel(38)
                If ViewState("MeisaiID") Is Nothing Then
                    txtControl2.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(3)), ",")(1)
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtControl2.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(4))
                    Else
                        txtControl2.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(3)), ",")(1)
                    End If
                End If
                txtControl2.Text = txtControl2.Text.ToUpper
                txtControl2.CssClass = "hissu"
                txtControl2.Attributes.Add("maxlength", "5")
                txtControl2.Style.Add("ime-mode", "disabled")
                'txtControl2.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(txtControl2)
                strUniqueID = strUniqueID & txtControl2.UniqueID & "|"
                arrRowId(3) = txtControl2.ClientID

                'Lable
                Dim lblControl As New Label
                lblControl.Text = "-"
                lblControl.Width = Unit.Pixel(5)
                grdBody.Rows(intRow).Cells(3).Controls.Add(lblControl)

                '枝番
                Dim txtControl3 As New TextBox
                txtControl3.Width = Unit.Pixel(18)
                If ViewState("MeisaiID") Is Nothing Then
                    txtControl3.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(3)), ",")(2)
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        txtControl3.Text = Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(5))
                    Else
                        txtControl3.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(3)), ",")(2)
                    End If
                End If
                txtControl3.CssClass = "hissu"
                txtControl3.Style.Add("ime-mode", "disabled;")
                txtControl3.Attributes.Add("maxlength", "2")
                'txtControl3.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(txtControl3)
                arrRowId(4) = txtControl3.ClientID
                strUniqueID = strUniqueID & txtControl3.UniqueID & "|"




                '検索
                Dim btnControl As New Button
                btnControl.Text = "検索"
                btnControl.Attributes.Add("value2", "検索")
                btnControl.Attributes.Add("onclick", "fncDisable();return fncOpenPop('S','" & intRow & "','修正');")
                'btnControl.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(btnControl)

                '請求先名
                txtControl = New TextBox
                txtControl.Width = Unit.Pixel(229)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Attributes.Add("readonly", "true")
                txtControl.Attributes.Add("TabIndex", "-1")
                If ViewState("MeisaiID") Is Nothing Then
                    txtControl.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(3)), ",")(3)
                Else

                    txtControl.Text = Split(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(3)), ",")(3)

                End If
                'txtControl.Style.Add("margin-left", "5px")
                grdBody.Rows(intRow).Cells(3).Controls.Add(txtControl)
                strUniqueID = strUniqueID & txtControl.UniqueID & "|"

                '修正
                Dim hidControl As New HiddenField
                Dim hidTime As New HiddenField
                btnControl = New Button
                btnControl.Text = "修正"

                If ViewState("MeisaiID") Is Nothing Then
                    btnControl.Attributes.Add("disabled", "true")
                    hidControl.Value = "true"
                Else

                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        If Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(7) = "" Then
                            btnControl.Enabled = Not CBool(hidBool.Value)
                        Else
                            btnControl.Enabled = Not CBool(Request.Form(Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString, "|")(7)))
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

                'btnControl.Style.Add("margin-left", "4px")
                hidTime.Value = dtTable.Rows(intRow).Item("upd_datetime").ToString
                grdBody.Rows(intRow).Cells(4).Controls.Add(btnControl)
                grdBody.Rows(intRow).Cells(4).Controls.Add(hidControl)
                grdBody.Rows(intRow).Cells(4).Controls.Add(hidTime)

                arrRowId(5) = hidTime.Value


                arrRowId(6) = intRow
                btnControl.Attributes.Add("onclick", "fncDisable();" & hidControl.ClientID & ".value=" & btnControl.ClientID & ".disabled;return fncSetRowData('修正','" & _
                                                                        arrRowId(0) & "','" & _
                                                                        arrRowId(1) & "','" & _
                                                                        arrRowId(2) & "','" & _
                                                                        arrRowId(3) & "','" & _
                                                                        arrRowId(4) & "','" & _
                                                                        arrRowId(5) & "','" & _
                                                                        arrRowId(6) & "','" & _
                                                                        arrRowId(7) & "','" & _
                                                                        txtTorikesi.ClientID & "');")

                If blnBtn Then
                    ddlControl.Attributes.Add("onPropertyChange", "fncButtonChange(this," & btnControl.ClientID & "," & hidControl.ClientID & ");")
                    ddlControl2.Attributes.Add("onPropertyChange", "fncButtonChange(this," & btnControl.ClientID & "," & hidControl.ClientID & ");")
                    txtControl2.Attributes.Add("onPropertyChange", "fncButtonChange(this," & btnControl.ClientID & "," & hidControl.ClientID & ");")
                    txtControl3.Attributes.Add("onPropertyChange", "fncButtonChange(this," & btnControl.ClientID & "," & hidControl.ClientID & ");")

                    ddlControl.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    ddlControl2.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControl2.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControl3.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")

                End If
                '削除

                btnControl = New Button
                btnControl.Text = "削除"
                btnControl.Enabled = True

                If Not blnBtn Then
                    btnControl.Enabled = False
                End If

                btnControl.Style.Add("margin-left", "1px")
                grdBody.Rows(intRow).Cells(4).Controls.Add(btnControl)

                MeisaiID.Add(intRow, strUniqueID & hidControl.UniqueID & "|" & hidTime.UniqueID)

                btnControl.Attributes.Add("onclick", "if (confirm('データを削除します。よろしいですか？')){fncDisable();return fncSetRowData('削除','" & _
                                                        arrRowId(0) & "','" & _
                                                        arrRowId(1) & "','" & _
                                                        arrRowId(2) & "','" & _
                                                        arrRowId(3) & "','" & _
                                                        arrRowId(4) & "','" & _
                                                        arrRowId(5) & "','" & _
                                                        arrRowId(6) & "','" & _
                                                        arrRowId(7) & "','" & _
                                                        txtTorikesi.ClientID & "');}else{return false;}")
            End If
        Next
        ViewState("MeisaiID") = MeisaiID
    End Function
    ''' <summary>Javascript作成</summary>
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
            .AppendLine("if (strKBN=='登録'){")
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
            .AppendLine("function  fncSetRowData(strBtn,strKameiten,strSyouhinKBN,strSeikyuuKBN,strHenkou,strBrc,strUPDTime,strRow,strSyouhinmae,strTorikesiId)")
            .AppendLine("{")

            .AppendLine("document.getElementById ('" & hidBtn.ClientID & "').value=strBtn;")

            .AppendLine("if (strBtn=='登録'){")
            .AppendLine("document.getElementById ('" & hidKameiten.ClientID & "').value=eval('document.all.'+strKameiten).value;")
            .AppendLine("document.getElementById ('" & hidTop.ClientID & "').value=strRow*60;")
            .AppendLine("}else{")

            .AppendLine("document.getElementById ('" & hidKameiten.ClientID & "').value=strKameiten;")

            .AppendLine("}")
            .AppendLine("document.getElementById ('" & hidSyouhinKBN.ClientID & "').value=eval('document.all.'+strSyouhinKBN).value;")
            .AppendLine("document.getElementById ('" & hidSeikyuuKBN.ClientID & "').value=eval('document.all.'+strSeikyuuKBN).value;")
            .AppendLine("document.getElementById ('" & hidstrHenkou.ClientID & "').value=eval('document.all.'+strHenkou).value;")
            .AppendLine("document.getElementById ('" & hidBrc.ClientID & "').value=eval('document.all.'+strBrc).value;")

            .AppendLine("document.getElementById ('" & hidUPDTime.ClientID & "').value=strUPDTime;")
            .AppendLine("document.getElementById ('" & hidRowIndex.ClientID & "').value=strRow;")
            .AppendLine("document.getElementById ('" & hidSyouhinmae.ClientID & "').value=strSyouhinmae;")

            .AppendLine("document.getElementById ('" & hidTorikesiMeisai.ClientID & "').value=eval('document.all.'+strTorikesiId).value;")

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
            .AppendLine(" document.getElementById('" & Me.tbxKameiten_cd.ClientID & "').value='';")
            .AppendLine(" document.getElementById('" & Me.tbxKameiten_mei.ClientID & "').value='';")

            .AppendLine(" document.getElementById('" & Me.tbxTorikesi.ClientID & "').value='';")
            .AppendLine(" document.getElementById('" & Me.hidTorikesi.ClientID & "').value='';")
            .AppendLine(" document.getElementById('" & Me.tbxKameiten_cd.ClientID & "').style.color = 'black'; ")
            .AppendLine(" document.getElementById('" & Me.tbxKameiten_mei.ClientID & "').style.color = 'black'; ")
            .AppendLine(" document.getElementById('" & Me.tbxTorikesi.ClientID & "').style.color = 'black'; ")

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

            .AppendLine("function fncChangeColor(objHidId,objCdId,objMeiId,objTorikesiId)")
            .AppendLine("{")
            .AppendLine("   var strColor;")
            .AppendLine("   var objHid = document.getElementById(objHidId);")
            .AppendLine("   var objCd = document.getElementById(objCdId);")
            .AppendLine("   var objMei = document.getElementById(objMeiId);")
            .AppendLine("   var objTorikesi = document.getElementById(objTorikesiId);")
            .AppendLine("   if(objHid.value == '0')")
            .AppendLine("   {")
            .AppendLine("       strColor = 'black';")
            .AppendLine("   }")
            .AppendLine("   else")
            .AppendLine("   {")
            .AppendLine("       strColor = 'red';")
            .AppendLine("   }")
            .AppendLine("	objCd.style.color = strColor; ")
            .AppendLine("	objMei.style.color = strColor; ")
            .AppendLine("	objTorikesi.style.color = strColor; ")
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
    Protected Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim commoncheck As New CommonCheck
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.SeikyuuSakiHenkouLogic

        Dim dtHaita As New DataTable
        Dim strErr As String = ""
        Dim strCol As Integer = -1
        Dim dtTable As DataTable = CType(ViewState("dtTable"), DataTable)
        Dim strItem As Integer = 0
        If hidBtn.Value = "登録" Then
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidKameiten.Value, "加盟店コード")
                If strErr <> "" Then
                    strCol = 0
                    strItem = 0
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.ChkHankakuEisuuji(hidKameiten.Value, "加盟店コード")
                If strErr <> "" Then
                    strCol = 0
                    strItem = 0
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidSyouhinKBN.Value, "商品区分")
                If strErr <> "" Then
                    strCol = 2
                    strItem = 0
                End If
            End If

            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidSeikyuuKBN.Value, "請求先区分")
                If strErr <> "" Then
                    strCol = 3
                    strItem = 0
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidstrHenkou.Value, "請求先コード")
                If strErr <> "" Then
                    strCol = 3
                    strItem = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.ChkHankakuEisuuji(hidstrHenkou.Value, "請求先コード")
                If strErr <> "" Then
                    strCol = 3
                    strItem = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidBrc.Value, "請求先枝番")
                If strErr <> "" Then
                    strCol = 3
                    strItem = 3
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.ChkHankakuEisuuji(hidBrc.Value, "請求先枝番")
                If strErr <> "" Then
                    strCol = 3
                    strItem = 3
                End If
            End If
            If strErr = "" Then
                If SelSearch(hidKameiten.Value, "Kameiten", "").Rows.Count = 0 Then
                    strErr = String.Format(Messages.Instance.MSG2036E, "加盟店マスタ").ToString
                    strCol = 0
                    strItem = 0
                End If
            End If

            If strErr = "" Then
                If SelSearch(hidstrHenkou.Value & "," & hidBrc.Value & "," & hidSeikyuuKBN.Value, "SeikyuuSaki", "").Rows.Count = 0 Then
                    strErr = String.Format(Messages.Instance.MSG2036E, "請求先変更マスタ").ToString
                    strCol = 3
                    strItem = 0
                End If
            End If

            If strErr = "" Then
                If Not CommonSearchLogic.SelJyuufuku(hidKameiten.Value, hidSyouhinKBN.Value) Then
                    strErr = Messages.Instance.MSG2035E
                    strCol = 0
                    strItem = 0
                End If
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"

            Else
                Dim Syouhin2MasterLogic As New Itis.Earth.BizLogic.Syouhin2MasterLogic
                If CommonSearchLogic.InsSeikyuuSaki(hidKameiten.Value, hidSyouhinKBN.Value, hidstrHenkou.Value, hidBrc.Value, hidSeikyuuKBN.Value, ViewState("UserId").ToString) Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "請求先変更マスタ") & "');"

                    Dim dt As New Data.DataTable
                    dt = Syouhin2MasterLogic.GetKameitenKensakuInfo(hidKameiten.Value, "")
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(0) = hidKameiten.Value & "," & TrimNull(dt.Rows(0).Item(1))
                    If dt.Rows(0).Item("torikesi").ToString.Trim.Equals("0") Then
                        dtTable.Rows(dtTable.Rows.Count - 1).Item(1) = String.Empty
                    Else
                        dtTable.Rows(dtTable.Rows.Count - 1).Item(1) = dt.Rows(0).Item("torikesi").ToString.Trim & ":" & dt.Rows(0).Item("torikesi_txt").ToString.Trim
                    End If
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(2) = hidSyouhinKBN.Value
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(3) = hidSeikyuuKBN.Value & "," & hidstrHenkou.Value & "," & hidBrc.Value & "," & SelSearch(hidstrHenkou.Value & "," & hidBrc.Value & "," & hidSeikyuuKBN.Value, "SeikyuuSaki", "btnSeikyuuSaki").Rows(0).Item(0).ToString


                    dtTable.Rows.Add(dtTable.NewRow)
                    ViewState("dtTable") = dtTable
                    ViewState("UniqueID") = Nothing
                Else
                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "請求先変更マスタ") & "');"

                End If
            End If
        ElseIf hidBtn.Value = "修正" Then
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidSyouhinKBN.Value, "商品区分")
                If strErr <> "" Then
                    strCol = 2
                    strItem = 0
                End If
            End If

            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidSeikyuuKBN.Value, "請求先区分")
                If strErr <> "" Then
                    strCol = 3
                    strItem = 0
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidstrHenkou.Value, "請求先コード")
                If strErr <> "" Then
                    strCol = 3
                    strItem = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.ChkHankakuEisuuji(hidstrHenkou.Value, "請求先コード")
                If strErr <> "" Then
                    strCol = 3
                    strItem = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidBrc.Value, "請求先枝番")
                If strErr <> "" Then
                    strCol = 3
                    strItem = 3
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.ChkHankakuEisuuji(hidBrc.Value, "請求先枝番")
                If strErr <> "" Then
                    strCol = 3
                    strItem = 3
                End If
            End If
            'If strErr = "" Then
            '    If SelSearch(hidKameiten.Value, "Kameiten", "").Rows.Count = 0 Then
            '        strErr = String.Format(Messages.Instance.MSG2036E, "加盟店マスタ").ToString
            '        strCol = 1
            '        strItem = 0
            '    End If
            'End If

            If strErr = "" Then
                If SelSearch(hidstrHenkou.Value & "," & hidBrc.Value & "," & hidSeikyuuKBN.Value, "SeikyuuSaki", "").Rows.Count = 0 Then
                    strErr = String.Format(Messages.Instance.MSG2036E, "請求先変更マスタ").ToString
                    strCol = 3
                    strItem = 0
                End If
            End If
            If strErr = "" Then
                If Not CommonSearchLogic.SelJyuufuku(hidKameiten.Value, hidSyouhinKBN.Value, hidSyouhinmae.Value) Then
                    strErr = Messages.Instance.MSG2035E
                    strCol = 2
                    strItem = 0
                End If
            End If
            If strErr = "" Then
                dtHaita = CommonSearchLogic.SelHaita(hidKameiten.Value, hidSyouhinmae.Value, hidUPDTime.Value)
                If dtHaita.Rows.Count <> 0 Then
                    strErr = String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "請求先変更マスタ").ToString()

                End If
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"
                hidBool.Value = "False"
            Else
                Dim intRturn As Integer
                intRturn = CommonSearchLogic.UpdSeikyuuSaki(hidKameiten.Value, hidSyouhinKBN.Value, hidstrHenkou.Value, hidBrc.Value, hidSeikyuuKBN.Value, ViewState("UserId").ToString, hidSyouhinmae.Value)
                '正常終了した場合
                dtTable.Rows(dtTable.Rows.Count - 1).Item(1) = hidSyouhinmae.Value
                If intRturn = 1 Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "請求先変更マスタ") & "');"
                    dtTable.Rows(hidRowIndex.Value).Item(1) = hidSyouhinKBN.Value
                    dtTable.Rows(hidRowIndex.Value).Item(2) = hidSeikyuuKBN.Value & "," & hidstrHenkou.Value & "," & hidBrc.Value & "," & SelSearch(hidstrHenkou.Value & "," & hidBrc.Value & "," & hidSeikyuuKBN.Value, "SeikyuuSaki", "btnSeikyuuSaki").Rows(0).Item(0).ToString
                    dtTable.Rows(hidRowIndex.Value).Item(3) = CommonSearchLogic.SelSeikyuuInfo(hidKameiten.Value, hidSyouhinKBN.Value).Rows(0).Item(3).ToString

                    hidBool.Value = "True"
                    strCol = 2
                ElseIf intRturn = 2 Then
                    strErr = "alert('" & Messages.Instance.MSG2049E & "');"
                    hidBool.Value = "False"

                Else

                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "請求先変更マスタ") & "');"
                    hidBool.Value = "False"

                End If
            End If
            Dim strMeisai As String()
            strMeisai = Split(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(hidRowIndex.Value).ToString, "|")
            '修正のClientID
            strMeisai(7) = ""
            CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(hidRowIndex.Value) = Join(strMeisai, "|")
        ElseIf hidBtn.Value = "削除" Then
            If strErr = "" Then
                dtHaita = CommonSearchLogic.SelHaita(hidKameiten.Value, hidSyouhinmae.Value, hidUPDTime.Value)
                If dtHaita.Rows.Count <> 0 Then
                    strErr = String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "請求先変更マスタ").ToString()

                End If
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"
            Else
                Dim intRturn As Boolean
                '正常終了した場合
                intRturn = CommonSearchLogic.DelSeikyuuSaki(hidKameiten.Value, hidSyouhinKBN.Value)
                If intRturn = True Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "請求先変更マスタ") & "');"
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
                    'ElseIf intRturn = 2 Then
                    '    strErr = "alert('" & Messages.Instance.MSG2049E & "');"
                    '    hidBool.Value = "False"

                Else

                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "請求先変更マスタ") & "');"
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

    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim commoncheck As New CommonCheck
        Dim strErr As String = ""
        Dim txtObj As New TextBox

        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(tbxKameiten_cd.Text, "加盟店コード")
            If strErr <> "" Then
                txtObj = tbxKameiten_cd
            End If
        End If
        If strErr = "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxKameiten_cd.Text, "加盟店コード")
            If strErr <> "" Then
                txtObj = tbxKameiten_cd
            End If
        End If

        If strErr <> "" Then
            strErr = "alert('" & strErr & "');document.getElementById('" & txtObj.ClientID & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
            SetHeadData()
            SetbodyData(False, Messages.Instance.MSG2034E)
        Else
            tbxKameiten_mei.Text = ""

            Me.hidTorikesi.Value = String.Empty
            Me.tbxTorikesi.Text = String.Empty

            If tbxKameiten_cd.Text.Trim <> "" Then
                Dim dt As New DataTable
                dt = SelSearch(tbxKameiten_cd.Text, "Kameiten", "btnSearchKameiten")

                If dt.Rows.Count = 1 Then
                    tbxKameiten_mei.Text = dt.Rows(0).Item(1)

                    Me.hidTorikesi.Value = dt.Rows(0).Item("torikesi").ToString.Trim
                    If dt.Rows(0).Item("torikesi").ToString.Trim.Equals("0") Then
                        Me.tbxTorikesi.Text = String.Empty
                    Else
                        Me.tbxTorikesi.Text = dt.Rows(0).Item("torikesi").ToString.Trim & ":" & dt.Rows(0).Item("torikesi_txt").ToString.Trim
                    End If

                    Call Me.SetColor(Me.hidTorikesi, Me.tbxKameiten_cd, Me.tbxKameiten_mei, Me.tbxTorikesi)
                End If
            End If
            ViewState("Kameiten") = tbxKameiten_cd.Text
            ViewState("Syouhin") = ddlSyouhinKBN.SelectedValue
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

    ''' <summary>
    ''' 色を変更する
    ''' </summary>
    ''' <history>2012/03/27 車龍 405721案件の対応 追加</history>
    Private Sub SetColor(ByVal hid As HiddenField, ByVal tbxCd As TextBox, ByVal tbxMei As TextBox, ByVal tbxTorikesi As TextBox)

        Dim strTorikesi As String
        strTorikesi = hid.Value.Trim

        If strTorikesi.Equals("0") OrElse strTorikesi.Equals(String.Empty) Then
            tbxCd.ForeColor = Drawing.Color.Black
            tbxMei.ForeColor = Drawing.Color.Black
            tbxTorikesi.ForeColor = Drawing.Color.Black
        Else
            tbxCd.ForeColor = Drawing.Color.Red
            tbxMei.ForeColor = Drawing.Color.Red
            tbxTorikesi.ForeColor = Drawing.Color.Red
        End If

    End Sub

End Class