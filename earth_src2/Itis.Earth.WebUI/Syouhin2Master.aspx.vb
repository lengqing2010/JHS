Imports Itis.Earth.BizLogic
Partial Public Class Syouhin2Master
    Inherits System.Web.UI.Page
    Private blnBtn As Boolean
    Private intWidth(3) As String

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
            'UpdatePanelA.Update()

        End If
        divMeisai.Attributes.Add("style", "overflow-y:auto ; height:339px;width:785px;")
        MakeScript()
        tbxSyouhin_mei.Attributes.Add("readonly", "true")
        tbxKameiten_mei.Attributes.Add("readonly", "true")
        btnSearch.Attributes.Add("onclick", "document.getElementById ('" & hidTop.ClientID & "').value =0;")
        Me.divMeisai.Attributes.Add("onscroll", "document.getElementById ('" & hidTop.ClientID & "').value =" & Me.divMeisai.ClientID & ".scrollTop;")

    End Sub

    Protected Sub btnCommonSearch(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchKameiten.Click, btnSearchSyouhin.Click, btnOpen.Click
        Dim strScript As String = ""
        Dim dtSyouhinTable As New DataTable
        Dim objCd As New TextBox
        Dim objMei As New TextBox
        Dim kbn As String = ""


        SetHeadData()
        SetbodyData(False)

        SetColWidth(intWidth, "body")
        GridViewStyle(intWidth, grdBody)

        SetColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)

        Select Case CType(sender, Button).ID
            Case "btnSearchSyouhin"
                objCd = tbxSyouhin_cd
                objMei = tbxSyouhin_mei
                kbn = "Syouhin"
            Case "btnSearchKameiten"
                objCd = tbxKameiten_cd
                objMei = tbxKameiten_mei
                kbn = "Kameiten"
            Case "btnOpen"
                If hidBtn.Value = "K" Then
                    objCd = grdBody.Rows(hidRowIndex.Value).Cells(0).Controls(1)
                    kbn = "Kameiten"
                    objMei = grdBody.Rows(hidRowIndex.Value).Cells(0).Controls(5)
                Else
                    objCd = grdBody.Rows(hidRowIndex.Value).Cells(1).Controls(1)
                    kbn = "Syouhin"
                    objMei = grdBody.Rows(hidRowIndex.Value).Cells(1).Controls(5)
                End If


        End Select
        objMei.Text = ""
        dtSyouhinTable = SelSearch(objCd.Text, kbn, CType(sender, Button).ID)
        If dtSyouhinTable.Rows.Count = 1 Then
            objCd.Text = dtSyouhinTable.Rows(0).Item(0)
            objMei.Text = dtSyouhinTable.Rows(0).Item(1)
            Dim Scriptmangaer1 As Web.UI.ScriptManager = Web.UI.ScriptManager.GetCurrent(Page)

            If kbn = "Kameiten" Then
                If CType(sender, Button).ID = "btnOpen" Then
                    Scriptmangaer1.SetFocus(grdBody.Rows(hidRowIndex.Value).Cells(1).Controls(1))
                End If
            Else
                If CType(sender, Button).ID = "btnOpen" Then
                    Scriptmangaer1.SetFocus(grdBody.Rows(hidRowIndex.Value).Cells(2).Controls(0))
                End If
            End If
            If CType(sender, Button).ID = "btnSearchSyouhin" Then
                Scriptmangaer1.SetFocus(btnSearch)
            End If
            If CType(sender, Button).ID = "btnSearchKameiten" Then
                Scriptmangaer1.SetFocus(tbxSyouhin_cd)
            End If
        Else
        If kbn = "Kameiten" Then
                If CType(sender, Button).ID = "btnSearchKameiten" Then
                    strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('加盟店')+'&blnDelete=False&FormName=" & _
                                Me.Page.Form.Name & "&objCd=" & _
                                objCd.ClientID & _
                                 "&objMei=" & objMei.ClientID & _
                                 "&strCd='+escape(eval('document.all.'+'" & _
                                 objCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                                 objMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
                Else
                    strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('加盟店')+'&blnDelete=True&FormName=" & _
                                Me.Page.Form.Name & "&objCd=" & _
                                objCd.ClientID & _
                                 "&objMei=" & objMei.ClientID & _
                                 "&strCd='+escape(eval('document.all.'+'" & _
                                 objCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                                 objMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
                End If
            Else
                If CType(sender, Button).ID = "btnSearchSyouhin" Then
                    strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('商品')+'&soukoCd='+escape('110,115')+'&FormName=" & _
                                Me.Page.Form.Name & "&objCd=" & _
                                objCd.ClientID & _
                                "&objMei=" & objMei.ClientID & _
                                "&strCd='+escape(eval('document.all.'+'" & _
                                objCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                                objMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
                Else
                    strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('商品')+'&blnDelete=True&soukoCd='+escape('110,115')+'&FormName=" & _
                                Me.Page.Form.Name & "&objCd=" & _
                                objCd.ClientID & _
                                "&objMei=" & objMei.ClientID & _
                                "&strCd='+escape(eval('document.all.'+'" & _
                                objCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                                objMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
                End If
            End If

        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strScript, True)

    End Sub
    Function SelSearch(ByVal objCd As String, ByVal kbn As String, ByVal btnId As String) As DataTable
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.Syouhin2MasterLogic
        If kbn = "Kameiten" Then
            If btnId = "btnSearchKameiten" Then
                SelSearch = CommonSearchLogic.GetKameitenKensakuInfo(objCd, "")
            Else
                SelSearch = CommonSearchLogic.GetKameitenKensakuInfo(objCd, "0")
            End If

        Else
            If btnId = "btnSearchSyouhin" Then
                SelSearch = CommonSearchLogic.SelSyouhin(objCd, "", "110,115")
            Else
                SelSearch = CommonSearchLogic.SelSyouhin(objCd, "0", "110,115")
            End If

        End If

    End Function
    ''' <summary>GridView並べる幅の設定</summary>
    Function SetColWidth(ByRef intwidth() As String, ByVal strName As String) As Integer
        If strName = "head" Then

            If CType(ViewState("dtTable"), DataTable).Rows.Count <> 1 Then
                intwidth(0) = "352px"
                intwidth(1) = "373px"
                intwidth(2) = "55px"
            Else
                intwidth(0) = "353px"
                intwidth(1) = "373px"
                intwidth(2) = "55px"
            End If
        Else
           
            intwidth(0) = "355px"
            intwidth(1) = "374px"
            intwidth(2) = "69px"
        End If
    End Function
    ''' <summary> GridViewヘッダー部をセット</summary>
    Sub SetHeadData()
        Dim CommonLG As New CommonLogic()
        grdHead.DataSource = CommonLG.CreateHeadDataSource("加盟店,商品コード,処理")
        grdHead.DataBind()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim commoncheck As New CommonCheck
        Dim strErr As String = ""
        Dim txtObj As New TextBox
        Dim dt As New DataTable
        If strErr = "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxKameiten_cd.Text, "加盟店コード")
            txtObj = tbxKameiten_cd
        End If
        If strErr = "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxSyouhin_cd.Text, "商品コード")
            txtObj = tbxSyouhin_cd
        End If

        If strErr <> "" Then
            strErr = "alert('" & strErr & "');document.getElementById('" & txtObj.ClientID & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)

        Else

            ViewState("Kameiten") = tbxKameiten_cd.Text
            ViewState("Syouhin") = tbxSyouhin_cd.Text
            tbxKameiten_mei.Text = ""
            tbxSyouhin_mei.Text = ""

            If tbxKameiten_cd.Text.Trim <> "" Then
                dt = SelSearch(tbxKameiten_cd.Text, "Kameiten", "btnSearchKameiten")
                If dt.Rows.Count = 1 Then
                    tbxKameiten_mei.Text = dt.Rows(0).Item(1)
                End If

            End If
            If tbxSyouhin_cd.Text.Trim <> "" Then
                dt = SelSearch(tbxSyouhin_cd.Text, "Syouhin", "btnSearchSyouhin")
                If dt.Rows.Count = 1 Then
                    tbxSyouhin_cd.Text = dt.Rows(0).Item(0)
                    tbxSyouhin_mei.Text = dt.Rows(0).Item(1)

                End If
            End If
            SetHeadData()
            ViewState("dtTable") = SetbodyData(True, Messages.Instance.MSG2034E)
        End If
        SetColWidth(intWidth, "body")
        GridViewStyle(intWidth, grdBody)

        SetColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)
        'If strErr = "" Then
        '    Dim Scriptmangaer1 As Web.UI.ScriptManager = Web.UI.ScriptManager.GetCurrent(Page)
        '    Scriptmangaer1.SetFocus(grdBody.Rows(grdBody.Rows.Count - 1).Cells(0).Controls(1))
        'End If

    End Sub
    ''' <summary> GridView内容、フォーマットをセット</summary>
    Sub GridViewStyle(ByVal intwidth() As String, ByVal grd As GridView)
        Dim intCol As Integer = 0
        Dim intRow As Integer = 0

        grd.Attributes.Add("Width", "780px")
        If IsArray(intwidth) AndAlso grd.Rows.Count > 0 Then
            For intRow = 0 To grd.Rows.Count - 1
                For intCol = 0 To grd.Rows(intRow).Cells.Count - 1
                    grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")
                    If Not (grd.ID = "grdBody" And intRow = grd.Rows.Count - 1 And intCol <> grd.Rows(intRow).Cells.Count - 1) Then
                        grd.Rows(intRow).Cells(intCol).Attributes.Add("align", "center")
                    End If

                Next
            Next
        End If
    End Sub
    ''' <summary> GridViewBODY部をセット</summary>
    Function SetbodyData(ByVal blnKousin As Boolean, Optional ByVal msg As String = "") As DataTable

        Dim CommonSearchLogic As New Itis.Earth.BizLogic.Syouhin2MasterLogic
        Dim dtTable As New DataTable
        Dim CommonLG As New CommonLogic()
        Dim intRow As Integer = 0
        Dim intColCount As Integer = 0
        If blnKousin Then
            ViewState("MeisaiID") = Nothing
            ViewState("UniqueID") = Nothing
            dtTable = CommonSearchLogic.SelSyouhinInfo(ViewState("Kameiten").ToString, ViewState("Syouhin"))
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
            Dim arrRowId(3) As String

            If intRow = grdBody.Rows.Count - 1 Then

                '加盟店

                Dim lblControl As New Label
                lblControl.Width = Unit.Pixel(5)
                grdBody.Rows(intRow).Cells(0).Controls.Add(lblControl)

                Dim txtControlK As New TextBox
                txtControlK.Width = Unit.Pixel(38)
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControlK.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(0))
                Else
                    txtControlK.Text = ""
                End If
                txtControlK.CssClass = "hissu"
                txtControlK.Style.Add("ime-mode", "disabled")
                txtControlK.Attributes.Add("maxlength", "5")
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControlK)
                lblControl = New Label
                lblControl.Width = Unit.Pixel(5)
                grdBody.Rows(intRow).Cells(0).Controls.Add(lblControl)

                arrRowId(0) = txtControlK.ClientID

                Dim btnControl As New Button
                btnControl.Text = "検索"
                btnControl.Attributes.Add("value2", "検索")
                btnControl.Attributes.Add("onclick", "fncDisable();return fncOpenPop('K','" & intRow & "');")
                grdBody.Rows(intRow).Cells(0).Controls.Add(btnControl)


                lblControl = New Label
                lblControl.Width = Unit.Pixel(5)
                grdBody.Rows(intRow).Cells(0).Controls.Add(lblControl)

                Dim txtControl2 As New TextBox
                txtControl2.Width = Unit.Pixel(230)
                txtControl2.CssClass = "readOnlyStyle"
                txtControl2.Attributes.Add("readonly", "true")
                txtControl2.Attributes.Add("TabIndex", "-1")
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl2.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(1))
                Else
                    txtControl2.Text = ""
                End If
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl2)
                strUniqueID = strUniqueID & txtControlK.UniqueID & "," & txtControl2.UniqueID & ","



                '加盟店
                lblControl = New Label
                lblControl.Width = Unit.Pixel(5)
                grdBody.Rows(intRow).Cells(1).Controls.Add(lblControl)

                Dim txtControlS As New TextBox
                txtControlS.Width = Unit.Pixel(60)
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControlS.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(2))
                Else
                    txtControlS.Text = ""
                End If
                txtControlS.CssClass = "hissu"
                txtControlS.Attributes.Add("maxlength", "8")
                txtControlS.Style.Add("ime-mode", "disabled")
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtControlS)
                lblControl = New Label
                lblControl.Width = Unit.Pixel(5)
                grdBody.Rows(intRow).Cells(1).Controls.Add(lblControl)
                arrRowId(1) = txtControlS.ClientID

                btnControl = New Button
                btnControl.Text = "検索"
                btnControl.Attributes.Add("value2", "検索")
                btnControl.Attributes.Add("onclick", "fncDisable();return fncOpenPop('S','" & intRow & "');")

                grdBody.Rows(intRow).Cells(1).Controls.Add(btnControl)


                lblControl = New Label
                lblControl.Width = Unit.Pixel(5)
                grdBody.Rows(intRow).Cells(1).Controls.Add(lblControl)

                Dim txtControl3 As New TextBox
                txtControl3.Width = Unit.Pixel(230)
                txtControl3.CssClass = "readOnlyStyle"
                txtControl3.Attributes.Add("readonly", "true")
                txtControl3.Attributes.Add("TabIndex", "-1")
                If Not blnKousin And Not ViewState("UniqueID") Is Nothing Then
                    txtControl3.Text = Request.Form(Split(ViewState("UniqueID").ToString, ",")(3))
                Else
                    txtControl3.Text = ""
                End If
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtControl3)
                strUniqueID = strUniqueID & txtControlS.UniqueID & "," & txtControl3.UniqueID & ","



                arrRowId(2) = ""
                arrRowId(3) = intRow

                '登録
                btnControl = New Button
                btnControl.Text = "登録"
                btnControl.Attributes.Add("onclick", "fncDisable();return fncSetRowData('登録','" & _
                                                        arrRowId(0) & "','" & _
                                                        arrRowId(1) & "','" & _
                                                        arrRowId(2) & "','" & _
                                                        arrRowId(3) & "');")
                If Not blnBtn Then
                    btnControl.Enabled = False
                End If
                grdBody.Rows(intRow).Cells(2).Controls.Add(btnControl)
                If blnBtn Then
                    txtControlS.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")
                    txtControlK.Attributes.Add("onkeypress", "if ( event.keyCode==13){document.all." & btnControl.ClientID & ".click();return false;}")

                End If

                grdBody.Rows(intRow).Cells(0).Attributes.Remove("align")
                grdBody.Rows(intRow).Cells(1).Attributes.Remove("align")
                grdBody.Rows(intRow).Attributes.Remove("align")
                grdBody.Rows(intRow).Attributes.Add("align", "left")
                grdBody.Rows(intRow).Cells(0).Attributes.Add("align", "left")
                grdBody.Rows(intRow).Cells(1).Attributes.Add("align", "left")

            Else
                '加盟店
                Dim txtControl As New TextBox
                txtControl.Width = Unit.Pixel(40)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Attributes.Add("readonly", "true")
                txtControl.Attributes.Add("TabIndex", "-1")
                txtControl.Text = Split(dtTable.Rows(intRow).Item(0), ",")(0)
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl)
                Dim lblControl As New Label
                lblControl.Width = Unit.Pixel(20)
                grdBody.Rows(intRow).Cells(0).Controls.Add(lblControl)
                txtControl = New TextBox
                txtControl.Width = Unit.Pixel(250)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Attributes.Add("readonly", "true")
                txtControl.Attributes.Add("TabIndex", "-1")
                txtControl.Text = Split(dtTable.Rows(intRow).Item(0), ",")(1)
                grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl)

                '加盟店
                txtControl = New TextBox
                txtControl.Width = Unit.Pixel(60)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Attributes.Add("readonly", "true")
                txtControl.Attributes.Add("TabIndex", "-1")
                txtControl.Text = Split(dtTable.Rows(intRow).Item(1), ",")(0)
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtControl)
                lblControl = New Label
                lblControl.Width = Unit.Pixel(20)
                grdBody.Rows(intRow).Cells(1).Controls.Add(lblControl)
                txtControl = New TextBox
                txtControl.Width = Unit.Pixel(250)
                txtControl.CssClass = "readOnlyStyle"
                txtControl.Attributes.Add("readonly", "true")
                txtControl.Attributes.Add("TabIndex", "-1")
                txtControl.Text = Split(dtTable.Rows(intRow).Item(1), ",")(1)
                grdBody.Rows(intRow).Cells(1).Controls.Add(txtControl)

                '削除
                Dim hidControl As New HiddenField
                Dim hidTime As New HiddenField
                Dim btnControl As New Button
                btnControl.Text = "削除"
                btnControl.Enabled = True
                hidControl.Value = "false"
                If Not blnBtn Then
                    btnControl.Enabled = False
                End If

                hidTime.Value = dtTable.Rows(intRow).Item("upd_datetime")
                grdBody.Rows(intRow).Cells(2).Controls.Add(btnControl)
                grdBody.Rows(intRow).Cells(2).Controls.Add(hidControl)
                grdBody.Rows(intRow).Cells(2).Controls.Add(hidTime)
                MeisaiID.Add(intRow, hidControl.UniqueID & "|" & hidTime.UniqueID)



                arrRowId(0) = Split(dtTable.Rows(intRow).Item(0), ",")(0)
                arrRowId(1) = Split(dtTable.Rows(intRow).Item(1), ",")(0)

                arrRowId(2) = hidTime.Value


                arrRowId(3) = intRow
                btnControl.Attributes.Add("onclick", "if (confirm('データを削除します。よろしいですか？')){" & hidControl.ClientID & ".value=" & btnControl.ClientID & ".disabled;fncDisable();return fncSetRowData('削除','" & _
                                                        arrRowId(0) & "','" & _
                                                        arrRowId(1) & "','" & _
                                                        arrRowId(2) & "','" & _
                                                        arrRowId(3) & "');}else{return false;}")
            End If
        Next

        ViewState("UniqueID") = strUniqueID
        ViewState("MeisaiID") = MeisaiID
    End Function
    ''' <summary>Javascript作成</summary>
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

            .AppendLine("function  fncSetRowData(strBtn,strKameiten,strSyouhin,strUPDTime,strRow)")
            .AppendLine("{")

            .AppendLine("document.getElementById ('" & hidBtn.ClientID & "').value=strBtn;")

            .AppendLine("if (strBtn=='登録'){")
            .AppendLine("document.getElementById ('" & hidKameiten.ClientID & "').value=eval('document.all.'+strKameiten).value;")
            .AppendLine("document.getElementById ('" & hidSyouhin.ClientID & "').value=eval('document.all.'+strSyouhin).value;")
            .AppendLine("document.getElementById ('" & hidTop.ClientID & "').value=strRow*60;")
            .AppendLine("}else{")
            .AppendLine("document.getElementById ('" & hidKameiten.ClientID & "').value=strKameiten;")
            .AppendLine("document.getElementById ('" & hidSyouhin.ClientID & "').value=strSyouhin;")

            .AppendLine("}")

            .AppendLine("document.getElementById ('" & hidUPDTime.ClientID & "').value=strUPDTime;")
            .AppendLine("document.getElementById ('" & hidRowIndex.ClientID & "').value=strRow;")
            .AppendLine("document.getElementById ('" & btn.ClientID & "').click();")

            .AppendLine("return false;")
            .AppendLine("}")

            .AppendLine("function  fncDisable()")
            .AppendLine("{")
            .AppendLine("var i;")
            .AppendLine("if (document.forms[0].elements.length) ")
            .AppendLine("	{")
            .AppendLine("	for (i=1;i < document.forms[0].elements.length;i++) ")
            .AppendLine("if (document.forms[0].elements[i].type=='submit') ")
            .AppendLine("	{")
            .AppendLine("if (document.forms[0].elements[i].value=='登録' || document.forms[0].elements[i].value=='削除' || document.forms[0].elements[i].value2=='検索' ) ")
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

    Protected Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim commoncheck As New CommonCheck
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.Syouhin2MasterLogic
        'Dim ComLogic As New Itis.Earth.BizLogic.CommonSearchLogic
        Dim dtHaita As New DataTable
        Dim strErr As String = ""
        Dim strCol As Integer = -1
        Dim dtTable As DataTable = CType(ViewState("dtTable"), DataTable)
        If hidBtn.Value = "登録" Then

            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidKameiten.Value, "加盟店コード")
                If strErr <> "" Then
                    strCol = 0
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.ChkHankakuEisuuji(hidKameiten.Value, "加盟店コード")
                If strErr <> "" Then
                    strCol = 0

                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.CheckHissuNyuuryoku(hidSyouhin.Value, "商品コード")
                If strErr <> "" Then
                    strCol = 1
                End If
            End If
            If strErr = "" Then
                strErr = commoncheck.ChkHankakuEisuuji(hidSyouhin.Value, "商品コード")
                If strErr <> "" Then
                    strCol = 1

                End If
            End If
            If strErr = "" Then
                If SelSearch(hidKameiten.Value, "Kameiten", "").Rows.Count = 0 Then
                    strErr = String.Format(Messages.Instance.MSG2036E, "加盟店マスタ").ToString
                    strCol = 0
                End If
            End If
            If strErr = "" Then
                If SelSearch(hidSyouhin.Value, "Syouhin", "").Rows.Count = 0 Then
                    strErr = String.Format(Messages.Instance.MSG2036E, "商品マスタ").ToString
                    strCol = 1
                End If
            End If
            If strErr = "" Then
                If Not CommonSearchLogic.SelJyuufuku(hidKameiten.Value, hidSyouhin.Value) Then
                    strErr = Messages.Instance.MSG2035E
                    strCol = 0
                End If
            End If

            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"

            Else

                If CommonSearchLogic.InsKeiretu(hidKameiten.Value, hidSyouhin.Value, ViewState("UserId").ToString) Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "特定店商品２設定マスタ") & "');"
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(0) = hidKameiten.Value & "," & Trim(CommonSearchLogic.GetKameitenKensakuInfo(hidKameiten.Value, "").Item(0).kameiten_mei1)
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(1) = hidSyouhin.Value & "," & Trim(CommonSearchLogic.SelSyouhin(hidSyouhin.Value, "", "110,115").Item(0).syouhin_mei)
                    dtTable.Rows(dtTable.Rows.Count - 1).Item(2) = CommonSearchLogic.SelDate(hidKameiten.Value, hidSyouhin.Value)

                    dtTable.Rows.Add(dtTable.NewRow)
                    ViewState("dtTable") = dtTable
                    ViewState("UniqueID") = Nothing
                Else

                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "特定店商品２設定マスタ") & "');"
                    strCol = 0
                End If
            End If
        Else
            If strErr = "" Then
                dtHaita = CommonSearchLogic.SelHaita(hidKameiten.Value, hidSyouhin.Value, hidUPDTime.Value)
                If dtHaita.Rows.Count <> 0 Then
                    strErr = String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "特定店商品２設定マスタ").ToString()

                End If
            End If
            If strErr <> "" Then
                strErr = "alert('" & strErr & "');"
            Else
                '正常終了した場合
                If CommonSearchLogic.DelKeiretu(hidKameiten.Value, hidSyouhin.Value) Then
                    strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "特定店商品２設定マスタ") & "');"
                    dtTable.Rows.RemoveAt(hidRowIndex.Value)
                Else
                    strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "特定店商品２設定マスタ") & "');"

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
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strErr & "document.getElementById('" & grdBody.Rows(hidRowIndex.Value).Cells(strCol).Controls(1).ClientID & "').focus();", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & strErr, True)

        End If

    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click

        tbxKameiten_cd.Text = ""
        tbxKameiten_mei.Text = ""
        tbxSyouhin_cd.Text = ""
        tbxSyouhin_mei.Text = ""

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