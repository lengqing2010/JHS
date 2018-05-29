Imports Itis.Earth.BizLogic
Partial Public Class HosyousyoBukkenItiran
    Inherits System.Web.UI.Page
    Dim intWidth(0) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim intCols As Integer = 0
        Dim CommonSearchLogic As New KyoutuuJyouhouLogic
        Dim dtTable As New DataTable
        Dim strUserID As String = ""
        Dim jBn As New Jiban '地盤画面共通クラス
        Dim Context As HttpContext = HttpContext.Current
        Dim Ninsyou As New BizLogic.Ninsyou
        Dim user_info As New LoginUserInfo

        jBn.userAuth(user_info)
        ' ユーザー基本認証
        Context.Items("strFailureMsg") = Messages.Instance.MSG2024E '（"該当ユーザーがありません。"）
        If Ninsyou.GetUserID() = "" Then
            Context.Server.Transfer("./CommonErr.aspx")
        Else
            ViewState("UserId") = Ninsyou.GetUserID()
        End If

        If Not IsPostBack Then
            Dim strGetu As String
            'ViewState("KBN") = "S"
            'ViewState("Kameiten") = "60824"
            'ViewState("Kameiten") = "05668"
            'ViewState("Flg") = "0"
            'ViewState("Getu") = "2008/10"
            'ViewState("Getu") = "2006/03"
            ViewState("Kameiten") = Request.QueryString("Kameiten")
            ViewState("Flg") = Request.QueryString("Flg")
            ViewState("Getu") = Request.QueryString("Getu")
            ViewState("KBN") = Request.QueryString("KBN")
            If ViewState("Flg") = "1" Then
                btnTougetu.Enabled = False
                btnZengetu.Enabled = False
                strGetu = ViewState("Getu") & "/01"
                strGetu = DateAdd(DateInterval.Month, -1, CDate(strGetu)).ToString("yyyy/MM")
                Label1.Visible = True
                Label2.Visible = True
            Else
                btnTougetu.Enabled = False

                ViewState("tou") = False
                dtTable = CommonSearchLogic.SelHosyousyo(ViewState("Kameiten").ToString, DateAdd(DateInterval.Month, -1, CDate(ViewState("Getu") & "/01")).ToString("yyyy/MM"), ViewState("KBN").ToString)
                If dtTable.Rows.Count = 0 Then
                    btnZengetu.Enabled = False
                    ViewState("zen") = False
                Else
                    btnZengetu.Enabled = True
                    ViewState("zen") = True
                End If
                strGetu = ViewState("Getu")
                Label1.Visible = False
                Label2.Visible = False
            End If
            ViewState("Month") = strGetu
            SetHeadData()
            SetbodyData(Messages.Instance.MSG2034E)
            setColWidth(intWidth, "body")
            GridViewStyle(intWidth, grdBody)

            setColWidth(intWidth, "head")
            GridViewStyle(intWidth, grdHead)
        Else
            closecover()
        End If
        btnAll.Attributes.Add("onclick", "fncDisable();")

        btnTougetu.Attributes.Add("onclick", "return funcHennkou('" & grdBody.ClientID & "');")
        btnZengetu.Attributes.Add("onclick", "return funcHennkou('" & grdBody.ClientID & "');")
        btnKousin.Attributes.Add("onclick", "if (GetFlg('" & grdBody.ClientID & "')==false){alert('" & "変更されている行はありません。" & "');return false;}else{fncDisable();}")
        btnClose.Attributes.Add("onclick", "if (funcHennkou('" & grdBody.ClientID & "')==false){return false;}else{window.close();}")
        divMeisai.Attributes.Add("style", "overflow-y:auto ; height:313px;width:803px;")
        divHead.Attributes.Add("style", "overflow-y:auto ; width:810px;")
        Me.divMeisai.Attributes.Add("onscroll", "document.getElementById ('" & hidTop.ClientID & "').value =" & Me.divMeisai.ClientID & ".scrollTop;")
        MakeScript()
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
    ''' <summary> GridViewBODY部をセット</summary>
    Sub SetbodyData(Optional ByVal msg As String = "", Optional ByVal strKbn As String = "", Optional ByVal bln As Boolean = True)

        Dim CommonSearchLogic As New KyoutuuJyouhouLogic
        Dim dtTable As New DataTable
        Dim CommonLG As New CommonLogic()
        Dim intRow As Integer = 0
        Dim intColCount As Integer = 0

        Dim blnDrop As Boolean = False
        dtTable = CommonSearchLogic.SelHosyousyo(ViewState("Kameiten").ToString, ViewState("Month").ToString, ViewState("KBN").ToString)
        If dtTable.Rows.Count = 0 Then
            If msg <> "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & msg & "');", True)
            End If
        End If
        grdBody.DataSource = dtTable
        grdBody.DataBind()
        Dim MeisaiID As New Dictionary(Of String, String)
        Dim MeisaiID2 As New Dictionary(Of String, String)
        For intRow = 0 To grdBody.Rows.Count - 1
            If IsDBNull(dtTable.Rows(intRow).Item(7)) Then
                blnDrop = True
                Exit For
            End If
        Next
        For intRow = 0 To grdBody.Rows.Count - 1


            ''顧客番号
            'Dim txtControl As New Label
            'txtControl.Width = Unit.Pixel(80)
            'txtControl.CssClass = "readOnlyStyle"
            'txtControl.Attributes.Add("readonly", "true")
            'txtControl.Attributes.Add("TabIndex", "-1")
            'txtControl.Style.Add("border-bottom", "none;")
            'txtControl.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(0))
            ''txtControl.Style.Add("margin-left", "1px;")
            'txtControl.Style.Add("text-align", "left;")
            'grdBody.Rows(intRow).Cells(0).Controls.Add(txtControl)

            ''施主名
            'txtControl = New Label
            'txtControl.Width = Unit.Pixel(250)
            'txtControl.CssClass = "readOnlyStyle"
            'txtControl.Attributes.Add("readonly", "true")
            'txtControl.Attributes.Add("TabIndex", "-1")
            'txtControl.Style.Add("border-bottom", "none;")
            'txtControl.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(1))
            ''txtControl.Style.Add("margin-left", "1px")
            'grdBody.Rows(intRow).Cells(1).Controls.Add(txtControl)


            ''データ破棄
            'txtControl = New Label
            'txtControl.Width = Unit.Pixel(80)
            'txtControl.CssClass = "readOnlyStyle"
            'txtControl.Attributes.Add("readonly", "true")
            'txtControl.Attributes.Add("TabIndex", "-1")
            'txtControl.Style.Add("border-bottom", "none;")
            'txtControl.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(2))
            'txtControl.Style.Add("margin-left", "1px")
            'grdBody.Rows(intRow).Cells(2).Controls.Add(txtControl)

            ''保証書発行日
            'txtControl = New Label
            'txtControl.Width = Unit.Pixel(80)
            'txtControl.CssClass = "readOnlyStyle"
            'txtControl.Attributes.Add("readonly", "true")
            'txtControl.Attributes.Add("TabIndex", "-1")
            'txtControl.Style.Add("border-bottom", "none;")
            'txtControl.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(3))
            ''txtControl.Style.Add("margin-left", "1px")
            'grdBody.Rows(intRow).Cells(3).Controls.Add(txtControl)

            ''保証開始日
            'txtControl = New Label
            'txtControl.Width = Unit.Pixel(80)
            'txtControl.CssClass = "readOnlyStyle"
            'txtControl.Attributes.Add("readonly", "true")
            'txtControl.Attributes.Add("TabIndex", "-1")
            'txtControl.Style.Add("border-bottom", "none;")
            'txtControl.Text = CommonLG.getDisplayString(dtTable.Rows(intRow).Item(4))
            ''txtControl.Style.Add("margin-left", "1px")
            'grdBody.Rows(intRow).Cells(4).Controls.Add(txtControl)

            '付保証明
            Dim ddlControl As New DropDownList
            ddlControl.Width = Unit.Pixel(50)
            SetKakutyou(ddlControl)
            If blnDrop Then
                ddlControl.Enabled = False
            Else

                If Not IsDBNull(dtTable.Rows(intRow).Item(6)) Then
                    ddlControl.Enabled = False
                Else
                    ddlControl.Enabled = True
                End If
            End If

            If bln Then
                If strKbn = "1" And ddlControl.Enabled Then
                    SetDropSelect(ddlControl, "1")
                Else
                    SetDropSelect(ddlControl, CommonLG.getDisplayString(dtTable.Rows(intRow).Item(5)))
                End If
            Else
                If ViewState("MeisaiID2") Is Nothing Then
                    SetDropSelect(ddlControl, CommonLG.getDisplayString(dtTable.Rows(intRow).Item(5)))
                Else
                    If CType(ViewState("MeisaiID2"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        SetDropSelect(ddlControl, Request.Form(CType(ViewState("MeisaiID2"), Dictionary(Of String, String)).Item(intRow).ToString))

                    Else
                        SetDropSelect(ddlControl, CommonLG.getDisplayString(dtTable.Rows(intRow).Item(5)))
                    End If
                End If
            End If
            
            grdBody.Rows(intRow).Cells(5).Controls.Add(ddlControl)
            Dim hidFlg As New HiddenField
            If bln Then
                If strKbn = "1" And ddlControl.Enabled Then
                    If CommonLG.getDisplayString(dtTable.Rows(intRow).Item(5)) <> "1" Then
                        hidFlg.Value = "1"
                    Else
                        hidFlg.Value = "0"
                    End If
                Else
                    hidFlg.Value = "0"
                End If
            Else
                If ViewState("MeisaiID") Is Nothing Then
                    hidFlg.Value = "0"
                Else
                    If CType(ViewState("MeisaiID"), Dictionary(Of String, String)).ContainsKey(intRow) Then
                        hidFlg.Value = Request.Form(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(intRow).ToString)

                    Else
                        hidFlg.Value = "0"
                    End If
                End If
            End If

            grdBody.Rows(intRow).Cells(5).Controls.Add(hidFlg)
            ddlControl.Attributes.Add("onchange", "fncSetFlg('" & ddlControl.ClientID & "','" & hidFlg.ClientID & "','" & IIf(CommonLG.getDisplayString(dtTable.Rows(intRow).Item(5)) <> "1", 0, 1) & "');")
            'If strKbn = "1" Or Not bln Then
            '    ddlControl.Attributes.Add("onchange", "fncSetFlg('" & ddlControl.ClientID & "','" & hidFlg.ClientID & "','" & CommonLG.getDisplayString(dtTable.Rows(intRow).Item(5)) & "');")
            'Else
            '    ddlControl.Attributes.Add("onchange", "fncSetFlg('" & ddlControl.ClientID & "','" & hidFlg.ClientID & "','" & ddlControl.SelectedValue & "');")
            'End If
           

            MeisaiID.Add(intRow, hidFlg.UniqueID)
            MeisaiID2.Add(intRow, ddlControl.UniqueID)
            grdBody.Rows(intRow).Cells(6).Visible = False
            grdBody.Rows(intRow).Cells(7).Visible = False
        Next
        ViewState("MeisaiID") = MeisaiID
        ViewState("MeisaiID2") = MeisaiID2
        MeisaiID = Nothing
        MeisaiID2 = Nothing
    End Sub
    Sub SetDropSelect(ByVal ddl As DropDownList, ByVal strValue As String)
        If strValue = "1" Then
            ddl.SelectedIndex = 0
        Else
            ddl.SelectedIndex = 1
        End If
       
    End Sub
    Sub SetKakutyou(ByVal ddl As DropDownList)


        Dim ddlist As New ListItem
  

        ddlist = New ListItem
        ddlist.Text = "有り"
        ddlist.Value = "1"
        ddl.Items.Add(ddlist)

        ddlist = New ListItem
        ddlist.Text = "無し"
        ddlist.Value = "0"
        ddl.Items.Add(ddlist)

    End Sub

    ''' <summary>Javascript作成</summary>
    Private Sub MakeScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "GetScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")

            .AppendLine("function  fncSetFlg(objddl,objhid,strvalue)")
            .AppendLine("{")
            .AppendLine("if (eval('document.all.'+objddl).value!=strvalue){")
            .AppendLine("eval('document.all.'+objhid).value='1';")
            .AppendLine("}else{ eval('document.all.'+objhid).value='0';}")
            .AppendLine("}")
            .AppendLine("function  funcHennkou(objTbl)")
            .AppendLine("{")
            .AppendLine("if (GetFlg(objTbl)==true)")
            .AppendLine("   {")
            .AppendLine("       if (confirm('変更内容が破棄されますがよろしいですか？')==true){")
            .AppendLine("          fncDisable(); return true;")

            .AppendLine("       }else{ return false;}")
            .AppendLine("   }else{fncDisable();}")
            .AppendLine("}")

            .AppendLine("function  GetFlg(strTbl)")
            .AppendLine("{")
            .AppendLine(" var i; ")
            If grdBody.Rows.Count > 0 Then
                .AppendLine(" var objTbl=eval('document.all.'+strTbl); ")
                .AppendLine("if (objTbl!=undefined){")
                .AppendLine("for (i=0;i<objTbl.childNodes[0].childNodes.length;i++){")
                .AppendLine("       if (objTbl.childNodes[0].childNodes[i].childNodes[5].childNodes[1].value=='1'){")
                .AppendLine("           return true;")
                .AppendLine("       }")
                .AppendLine("   }")
                .AppendLine("   }")
            End If

            .AppendLine("           return false;")
            .AppendLine("}")
            .AppendLine("function closecover()")
            .AppendLine("{")
            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("   var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("    buyDiv.style.display='none';")
            .AppendLine("    disable.style.display='none';")

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
            .AppendLine("function  fncDisable()")
            .AppendLine("{")
            .AppendLine("ShowModal();")

            .AppendLine("}")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub
    Sub GridViewStyle(ByVal intwidth() As String, ByVal grd As GridView)
        Dim intCol As Integer = 0
        Dim intRow As Integer = 0

        'grd.Attributes.Add("Width", "765px")
        If IsArray(intwidth) AndAlso grd.Rows.Count > 0 Then
            For intRow = 0 To grd.Rows.Count - 1
                For intCol = 0 To grd.Rows(intRow).Cells.Count - 1
                    If intCol < intwidth.Length Then
                        grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")
                        If intCol = 1 Then
                            grd.Rows(intRow).Cells(intCol).Attributes.Add("align", "left")

                        Else
                            grd.Rows(intRow).Cells(intCol).Attributes.Add("align", "center")

                        End If

                    End If
                Next
            Next
        End If
    End Sub
    ''' <summary>GridViewデータ列の設定</summary>
    Function setColWidth(ByRef intwidth() As String, ByVal strName As String) As Integer

        setColWidth = 5
        ReDim intwidth(setColWidth)
        If strName = "head" Then
            intwidth(0) = "98px"
            intwidth(1) = "279px"
            intwidth(2) = "98px"
            intwidth(3) = "103px"
            intwidth(4) = "103px"
            intwidth(5) = "58px"
        Else
            intwidth(0) = "100px"
            intwidth(1) = "281px"
            intwidth(2) = "100px"
            intwidth(3) = "105px"
            intwidth(4) = "105px"
            intwidth(5) = "60px"
        End If

    End Function
    Sub SetHeadData()
        Dim CommonLG As New CommonLogic()
        grdHead.DataSource = CommonLG.CreateHeadDataSource("顧客番号,施主名,データ破棄,保証書発行日,保証開始日,付保証明")
        grdHead.DataBind()
    End Sub

    Protected Sub btnTougetu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTougetu.Click
        ViewState("Month") = ViewState("Getu")
        SetHeadData()
        SetbodyData(Messages.Instance.MSG2034E)
        setColWidth(intWidth, "body")
        GridViewStyle(intWidth, grdBody)

        setColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "document.all." & Me.btnZengetu.ClientID & ".disabled = false;" & "document.all." & Me.btnTougetu.ClientID & ".disabled = true;", True)
        'btnZengetu.Enabled = True
        'btnTougetu.Enabled = False
        ViewState("zen") = True
        ViewState("tou") = False
        Label1.Visible = False
        Label2.Visible = False
    End Sub

    Protected Sub btnZengetu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZengetu.Click
        Dim CommonSearchLogic As New KyoutuuJyouhouLogic

        ViewState("Month") = DateAdd(DateInterval.Month, -1, CDate(ViewState("Getu") & "/01")).ToString("yyyy/MM")
        SetHeadData()
        SetbodyData(Messages.Instance.MSG2034E)
        setColWidth(intWidth, "body")
        GridViewStyle(intWidth, grdBody)

        setColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "document.all." & Me.btnZengetu.ClientID & ".disabled = true;" & "document.all." & Me.btnTougetu.ClientID & ".disabled = false;", True)
        ViewState("zen") = False
        ViewState("tou") = True
        Label1.Visible = True
        Label2.Visible = True
        'btnZengetu.Enabled = False
        'btnTougetu.Enabled = True
    End Sub
    Function GetFlg() As String
        Dim strReturn As String = ""
        For I As Integer = 0 To CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Count - 1
            If Request(CType(ViewState("MeisaiID"), Dictionary(Of String, String)).Item(I)) = "1" Then
                strReturn = strReturn & grdBody.Rows(I).Cells(0).Text & "|" & Request.Form(CType(ViewState("MeisaiID2"), Dictionary(Of String, String)).Item(I).ToString) & ","
            End If
        Next
        Return Left(strReturn, Len(strReturn) - 1)
    End Function

    Protected Sub btnAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAll.Click

        SetHeadData()
        SetbodyData(Messages.Instance.MSG2034E, "1")
        setColWidth(intWidth, "body")
        GridViewStyle(intWidth, grdBody)

        setColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)
        If ViewState("zen") Then
            Me.btnZengetu.Enabled = True
        Else
            btnZengetu.Enabled = False
        End If
        If ViewState("tou") Then
            Me.btnTougetu.Enabled = True
        Else
            Me.btnTougetu.Enabled = False
        End If
    End Sub

    Protected Sub btnKousin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKousin.Click
        Dim strParam As String = GetFlg()
        Dim strKbn As String = ""
        Dim blnReturn As Boolean = False
        If strParam <> "" Then
            Dim CommonSearchLogic As New KyoutuuJyouhouLogic

            blnReturn = CommonSearchLogic.SetUPDJiban(strParam, ViewState("UserId").ToString)
        Else
            blnReturn = True
        End If
        If blnReturn = False Then
            SetHeadData()
            SetbodyData(Messages.Instance.MSG2034E, , False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = document.getElementById ('" & hidTop.ClientID & "').value;" & "alert('一部または全部の更新に失敗しました。');", True)
        Else
            SetHeadData()
            SetbodyData(Messages.Instance.MSG2034E)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", Me.divMeisai.ClientID & ".scrollTop = 0; document.getElementById ('" & hidTop.ClientID & "').value=0;" & "alert('更新が完了しました。');", True)

        End If
        setColWidth(intWidth, "body")
        GridViewStyle(intWidth, grdBody)

        setColWidth(intWidth, "head")
        GridViewStyle(intWidth, grdHead)
        If ViewState("zen") Then
            Me.btnZengetu.Enabled = True
        Else
            btnZengetu.Enabled = False
        End If
        If ViewState("tou") Then
            Me.btnTougetu.Enabled = True
        Else
            Me.btnTougetu.Enabled = False
        End If
    End Sub
End Class