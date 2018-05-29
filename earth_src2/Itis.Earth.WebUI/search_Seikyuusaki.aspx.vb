Imports Itis.Earth.BizLogic

Partial Public Class search_Seikyuusaki
    Inherits System.Web.UI.Page

    ''' <summary>請求先マスタ検索</summary>
    ''' <history>
    ''' <para>2010/05/20　馬艶軍(大連情報システム部)　新規作成</para>
    ''' </history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strKbn As String = ""
        Dim strFlg As String = ""
        Dim intCols As Integer = 0
        Dim intWidth(0) As String

        If Not IsPostBack Then

            '請求先区分
            SetKakutyou(search_Cd, "1")

            '引継FORMID
            ViewState("strKbn") = Request.QueryString("Kbn")
            ViewState("strFormName") = Request.QueryString("FormName")
            ViewState("objKbn") = Request.QueryString("objKbn")
            ViewState("objCd") = Request.QueryString("objCd")
            ViewState("objBrc") = Request.QueryString("objBrc")
            ViewState("objMei") = Request.QueryString("objMei")
            ViewState("objHidKbn") = Request.QueryString("objHidKbn")
            ViewState("objHidCd") = Request.QueryString("objHidCd")
            ViewState("objHidBrc") = Request.QueryString("objHidBrc")
            ViewState("objDate") = Request.QueryString("objDate")
            ViewState("hidConfirm2") = Request.QueryString("hidConfirm2")

            ViewState("show") = Request.QueryString("show")
            'ViewState("show") = "True"

            ViewState("blnDelete") = Request.QueryString("blnDelete")

            ViewState("objBtn") = Request.QueryString("objBtn")

            ViewState("strFlg") = Request.QueryString("strFlg")


            '引継値
            SetDropSelect(search_Cd, Request.QueryString("strKbn"))
            search_Mei.Text = Request.QueryString("strCd")
            search_Mei2.Text = Request.QueryString("strBrc")

            intCols = setColWidth(intWidth, "head")

            'データ
            grdHead.DataSource = CreateHeadDataSource(intCols)
            grdHead.DataBind()

            '初期表示
            If ViewState("show") = "True" Then
                grdViewStyle(intWidth, grdHead, GetMeisai(True), True)
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
                End If
            End If
            grdViewStyle(intWidth, grdHead, , False)
        End If

        '請求先
        strKbn = ViewState("strKbn")

        lblKensaku.Text = "検索条件"
        Me.Title = "請求先マスタ検索"
        lblTitle.Text = "請求先マスタ検索"

        lblCd.Text = "請求先"
        'lblMei.Text = "請求先カナ名"
        'lblMei2.Text = "請求先枝番"
        lblMei3.Text = "請求先カナ名"
        divMeisai.Attributes.Add("style", "overflow-y:auto ; height:182px; width:758px;")

        clearWin.Attributes.Add("onclick", "return fncClear('" & search_Cd.ClientID & "','" & search_Mei.ClientID & "','" & search_Mei2.ClientID & "','" & maxSearchCount.ClientID & "','" & search_Mei3.ClientID & "');")
        MakeJavaScript()

        search_Mei3.Attributes.Add("onblur", "fncTokomozi(this)")
    End Sub

    ''' <summary>検索データを設定</summary>
    Public Function CreateBodyDataSource(ByVal intRow As String) As DataTable
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic

        Dim DT As New DataTable
        Dim dtKameitenSearchTable As New Itis.Earth.DataAccess.CommonSearchDataSet.SeikyuuSakiTableDataTable
        dtKameitenSearchTable = CommonSearchLogic.GetSeikyuuSakiInfo(intRow, _
                                                                    search_Cd.SelectedValue, _
                                                                    search_Mei.Text, _
                                                                    search_Mei2.Text, search_Mei3.Text, ViewState("blnDelete"), True)



        Dim drTemp As DataRow
        DT.Columns.Add(New DataColumn("seikyuu_saki_kbn", GetType(String)))
        DT.Columns.Add(New DataColumn("seikyuu_saki_cd", GetType(String)))
        DT.Columns.Add(New DataColumn("seikyuu_saki_brc", GetType(String)))
        DT.Columns.Add(New DataColumn("seikyuu_saki_mei", GetType(String)))
        DT.Columns.Add(New DataColumn("seikyuu_sime_date", GetType(String)))
        For i As Integer = 0 To dtKameitenSearchTable.Rows.Count - 1
            drTemp = DT.NewRow
            With drTemp
                .Item(0) = TrimNull(dtKameitenSearchTable.Rows(i).Item("seikyuu_saki_kbn"))
                .Item(1) = TrimNull(dtKameitenSearchTable.Rows(i).Item("seikyuu_saki_cd"))
                .Item(2) = TrimNull(dtKameitenSearchTable.Rows(i).Item("seikyuu_saki_brc"))
                .Item(3) = TrimNull(dtKameitenSearchTable.Rows(i).Item("seikyuu_saki_mei"))
                .Item(4) = TrimNull(dtKameitenSearchTable.Rows(i).Item("seikyuu_sime_date"))
            End With

            DT.Rows.Add(drTemp)
        Next

        Return DT


    End Function

    ''' <summary>GridViewデータ列の設定</summary>
    Function setColWidth(ByRef intwidth() As String, ByVal strName As String) As Integer
        setColWidth = 3
        ReDim intwidth(setColWidth)
        If strName = "head" Then
            intwidth(0) = "120px"
            intwidth(1) = "159px"
            intwidth(2) = "140px"
            intwidth(3) = "290px"
        Else
            intwidth(0) = "122px"
            intwidth(1) = "161px"
            intwidth(2) = "142px"
            intwidth(3) = "292px"
        End If
    End Function
    ''' <summary> GridView内容、フォーマットをセット</summary>
    Sub grdViewStyle(ByVal intwidth() As String, ByVal grd As GridView, Optional ByVal dt As DataTable = Nothing, Optional ByVal blnSort As Boolean = False)
        Dim intCol As Integer = 0
        Dim intRow As Integer = 0
        If IsArray(intwidth) AndAlso grd.Rows.Count > 0 Then
            For intRow = 0 To grd.Rows.Count - 1
                For intCol = 0 To grd.Rows(intRow).Cells.Count - 1
                    If grd.ID = "grdBody" Then
                        If intCol < grd.Rows(intRow).Cells.Count - 1 Then
                            grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")

                        Else
                            grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "width:0px;")

                        End If

                    Else
                        grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")

                    End If
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

            .Item(0) = "請求先区分"
            .Item(1) = "請求先コード"
            .Item(2) = "請求先枝番"
            .Item(3) = "請求先名"

        End With
        dtHeader.Rows.Add(drTemp)

        Return dtHeader
    End Function
    ''' <summary>GridView行の実装</summary>
    Private Sub grdBody_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBody.RowDataBound
        e.Row.Attributes.Add("onclick", "selectedLineColor(this);")

        e.Row.Attributes.Add("ondblclick", "fncSetItem('" & ViewState("objKbn") & _
                                                    "','" & ViewState("objHidKbn") & _
                                                    "','" & e.Row.Cells(0).ClientID & _
                                                    "','" & ViewState("objCd") & _
                                                    "','" & ViewState("objHidCd") & _
                                                    "','" & e.Row.Cells(1).ClientID & _
                                                    "','" & ViewState("objBrc") & _
                                                    "','" & ViewState("objHidBrc") & _
                                                    "','" & e.Row.Cells(2).ClientID & _
                                                    "','" & ViewState("objMei") & _
                                                    "','" & e.Row.Cells(3).ClientID & _
                                                     "','" & ViewState("objDate") & _
                                                    "','" & Replace(e.Row.Cells(4).Text, "&nbsp;", "").Trim & _
                                                    "','" & ViewState("strFormName") & "');")
        e.Row.Cells(4).Visible = False

    End Sub
    ''' <summary>Javascript作成</summary>
    Protected Sub MakeJavaScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .Append("<script language='javascript' type='text/javascript'>  " & vbCrLf)

            .Append("function fncSetItem(objKbn,objHidKbn,strobjKbn,objCd,objHidCd,strobjCd,objBrc,objHidBrc,strobjBrc,objMei,strObjMei,objDate,strObjDate,FormName)" & vbCrLf)
            .Append("{" & vbCrLf)
            .Append("if(window.opener == null || window.opener.closed){" & vbCrLf)
            .Append("    alert('呼び出し元画面が閉じられた為、値をセットできません。');" & vbCrLf)
            .Append("   return false;" & vbCrLf)
            .Append(" }else{" & vbCrLf)
            .Append(" }" & vbCrLf)
            '.Append("eval('window.opener.document.all.'+objCd).innerText=eval('document.all.'+strObjCd).innerText+eval('document.all.'+strObjCd2).innerText;" & vbCrLf)
            '.Append("eval('window.opener.document.all.'+objCd).select();" & vbCrLf)
            'ViewState("hidConfirm2")
            If ViewState("hidConfirm2") <> "" Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("hidConfirm2") & "').value='';" & vbCrLf)
            End If

            '20100712　仕様変更　削除　馬艶軍↓↓↓
            If ViewState("strFlg") = "1" Then
                .AppendLine("   if((eval('document.all.'+strobjKbn).innerText)!='1'){")
                .AppendLine("       alert('請求先区分は調査会社しか選択できません。');")
                .AppendLine("       return false;")
                .Append("}" & vbCrLf)
            End If
            '20100712　仕様変更　削除　馬艶軍↑↑↑

            .Append("if (objKbn!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objKbn).value=eval('document.all.'+strobjKbn).innerText;" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("if (objHidKbn!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objHidKbn).value=eval('document.all.'+strobjKbn).innerText;" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("if (objCd!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objCd).innerText=eval('document.all.'+strobjCd).innerText;" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("if (objHidCd!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objHidCd).innerText=eval('document.all.'+strobjCd).innerText;" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("if (objBrc!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objBrc).innerText=eval('document.all.'+strobjBrc).innerText;" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("if (objHidBrc!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objHidBrc).innerText=eval('document.all.'+strobjBrc).innerText;" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("if (objMei!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objMei).innerText=eval('document.all.'+strObjMei).innerText;" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("if (objDate!=''){" & vbCrLf)
            .Append("if (strObjDate!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objDate).innerText='締め日：'+strObjDate+'日';" & vbCrLf)

            .Append("}else{" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objDate).innerText='';" & vbCrLf)

            .Append("}" & vbCrLf)
            .Append("}" & vbCrLf)

            If Not IsNothing(ViewState("objBtn")) Then
                .Append("   eval('window.opener.document.all.'+'" & ViewState("objBtn") & "').click();" & vbCrLf)
            End If

            .Append("window.close();" & vbCrLf)
            .Append("}" & vbCrLf)

            .Append("function fncClear(objcd,objmei,objmei2,objddl,objmei3){" & vbCrLf)
            .Append("eval('document.all.'+objcd).selectedIndex=0;" & vbCrLf)
            .Append("eval('document.all.'+objmei).innerText='';" & vbCrLf)
            .Append("eval('document.all.'+objmei2).innerText='';" & vbCrLf)
            .Append("eval('document.all.'+objddl).selectedIndex=0;" & vbCrLf)
            .Append("eval('document.all.'+objmei3).innerText='';" & vbCrLf)
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

        intRowCount = CommonSearchLogic.GetSeikyuuSakiCount(search_Cd.SelectedValue, _
                                                                    search_Mei.Text, _
                                                                    search_Mei2.Text, search_Mei3.Text, ViewState("blnDelete"), True)


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
    Sub SetValueScript(Optional ByVal dt As DataTable = Nothing)
        Dim csScript As New StringBuilder

        With csScript
            .Append("<script language='javascript' type='text/javascript'>  " & vbCrLf)
            .Append("if(window.opener == null || window.opener.closed){" & vbCrLf)
            .Append("    alert('呼び出し元画面が閉じられた為、値をセットできません。');" & vbCrLf)
            .Append("  }else{" & vbCrLf)

            If ViewState("hidConfirm2") <> "" Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("hidConfirm2") & "').value='';" & vbCrLf)
            End If

            '20100712　仕様変更　追加　馬艶軍↓↓↓
            If ViewState("strFlg") = "1" Then
                .AppendLine("   if((eval('document.all.'+'" & grdBody.Rows(0).Cells(0).ClientID & "').innerText)!='1'){")
                .AppendLine("       alert('請求先区分は調査会社しか選択できません。');")
                .Append("  }else{" & vbCrLf)
                If ViewState("objKbn") <> "" Then
                    .Append("eval('window.opener.document.all.'+'" & ViewState("objKbn") & "').value=eval('document.all.'+'" & grdBody.Rows(0).Cells(0).ClientID & "').innerText;" & vbCrLf)
                End If
                If ViewState("objHidKbn") <> "" Then
                    .Append("eval('window.opener.document.all.'+'" & ViewState("objHidKbn") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(0).ClientID & "').innerText;" & vbCrLf)
                End If

                If ViewState("objCd") <> "" Then
                    .Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(1).ClientID & "').innerText;" & vbCrLf)
                End If
                If ViewState("objHidCd") <> "" Then
                    .Append("eval('window.opener.document.all.'+'" & ViewState("objHidCd") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(1).ClientID & "').innerText;" & vbCrLf)
                End If

                If ViewState("objBrc") <> "" Then
                    .Append("eval('window.opener.document.all.'+'" & ViewState("objBrc") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(2).ClientID & "').innerText;" & vbCrLf)
                End If
                If ViewState("objHidBrc") <> "" Then
                    .Append("eval('window.opener.document.all.'+'" & ViewState("objHidBrc") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(2).ClientID & "').innerText;" & vbCrLf)
                End If

                If ViewState("objDate") <> "" Then
                    If Replace(dt.Rows(0).Item(4), "&nbsp;", "") = "" Then
                        .Append("eval('window.opener.document.all.'+'" & ViewState("objDate") & "').innerText='';" & vbCrLf)

                    Else
                        .Append("eval('window.opener.document.all.'+'" & ViewState("objDate") & "').innerText='締め日：'+'" & Replace(dt.Rows(0).Item(4), "&nbsp;", "") & "'+'日';" & vbCrLf)

                    End If
                End If
                If ViewState("objMei") <> "" Then
                    .Append("eval('window.opener.document.all.'+'" & ViewState("objMei") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(3).ClientID & "').innerText;" & vbCrLf)
                End If
                .Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').select();" & vbCrLf)

                If Not IsNothing(ViewState("objBtn")) Then
                    .Append("   eval('window.opener.document.all.'+'" & ViewState("objBtn") & "').click();" & vbCrLf)
                End If
                .Append("window.close();" & vbCrLf)
                .Append("}" & vbCrLf)
            Else
                If ViewState("objKbn") <> "" Then
                    .Append("eval('window.opener.document.all.'+'" & ViewState("objKbn") & "').value=eval('document.all.'+'" & grdBody.Rows(0).Cells(0).ClientID & "').innerText;" & vbCrLf)
                End If
                If ViewState("objHidKbn") <> "" Then
                    .Append("eval('window.opener.document.all.'+'" & ViewState("objHidKbn") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(0).ClientID & "').innerText;" & vbCrLf)
                End If

                If ViewState("objCd") <> "" Then
                    .Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(1).ClientID & "').innerText;" & vbCrLf)
                End If
                If ViewState("objHidCd") <> "" Then
                    .Append("eval('window.opener.document.all.'+'" & ViewState("objHidCd") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(1).ClientID & "').innerText;" & vbCrLf)
                End If

                If ViewState("objBrc") <> "" Then
                    .Append("eval('window.opener.document.all.'+'" & ViewState("objBrc") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(2).ClientID & "').innerText;" & vbCrLf)
                End If
                If ViewState("objHidBrc") <> "" Then
                    .Append("eval('window.opener.document.all.'+'" & ViewState("objHidBrc") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(2).ClientID & "').innerText;" & vbCrLf)
                End If

                If ViewState("objDate") <> "" Then
                    If Replace(dt.Rows(0).Item(4), "&nbsp;", "") = "" Then
                        .Append("eval('window.opener.document.all.'+'" & ViewState("objDate") & "').innerText='';" & vbCrLf)

                    Else
                        .Append("eval('window.opener.document.all.'+'" & ViewState("objDate") & "').innerText='締め日：'+'" & Replace(dt.Rows(0).Item(4), "&nbsp;", "") & "'+'日';" & vbCrLf)

                    End If
                End If
                If ViewState("objMei") <> "" Then
                    .Append("eval('window.opener.document.all.'+'" & ViewState("objMei") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(3).ClientID & "').innerText;" & vbCrLf)
                End If
                .Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').select();" & vbCrLf)

                If Not IsNothing(ViewState("objBtn")) Then
                    .Append("   eval('window.opener.document.all.'+'" & ViewState("objBtn") & "').click();" & vbCrLf)
                End If

                .Append("window.close();" & vbCrLf)
            End If
            '20100712　仕様変更　追加　馬艶軍↑↑↑

            '20100712　仕様変更　削除　馬艶軍↓↓↓
            'If ViewState("objKbn") <> "" Then
            '    .Append("eval('window.opener.document.all.'+'" & ViewState("objKbn") & "').value=eval('document.all.'+'" & grdBody.Rows(0).Cells(0).ClientID & "').innerText;" & vbCrLf)
            'End If
            'If ViewState("objHidKbn") <> "" Then
            '    .Append("eval('window.opener.document.all.'+'" & ViewState("objHidKbn") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(0).ClientID & "').innerText;" & vbCrLf)
            'End If

            'If ViewState("objCd") <> "" Then
            '    .Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(1).ClientID & "').innerText;" & vbCrLf)
            'End If
            'If ViewState("objHidCd") <> "" Then
            '    .Append("eval('window.opener.document.all.'+'" & ViewState("objHidCd") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(1).ClientID & "').innerText;" & vbCrLf)
            'End If

            'If ViewState("objBrc") <> "" Then
            '    .Append("eval('window.opener.document.all.'+'" & ViewState("objBrc") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(2).ClientID & "').innerText;" & vbCrLf)
            'End If
            'If ViewState("objHidBrc") <> "" Then
            '    .Append("eval('window.opener.document.all.'+'" & ViewState("objHidBrc") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(2).ClientID & "').innerText;" & vbCrLf)
            'End If

            'If ViewState("objDate") <> "" Then
            '    If Replace(dt.Rows(0).Item(4), "&nbsp;", "") = "" Then
            '        .Append("eval('window.opener.document.all.'+'" & ViewState("objDate") & "').innerText='';" & vbCrLf)

            '    Else
            '        .Append("eval('window.opener.document.all.'+'" & ViewState("objDate") & "').innerText='締め日：'+'" & Replace(dt.Rows(0).Item(4), "&nbsp;", "") & "'+'日';" & vbCrLf)

            '    End If
            'End If
            'If ViewState("objMei") <> "" Then
            '    .Append("eval('window.opener.document.all.'+'" & ViewState("objMei") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(3).ClientID & "').innerText;" & vbCrLf)
            'End If
            '.Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').select();" & vbCrLf)

            'If Not IsNothing(ViewState("objBtn")) Then
            '    .Append("   eval('window.opener.document.all.'+'" & ViewState("objBtn") & "').click();" & vbCrLf)
            'End If

            '.Append("window.close();" & vbCrLf)
            '20100712　仕様変更　削除　馬艶軍↑↑↑

            .Append(" }" & vbCrLf)
            .Append("</script>" & vbCrLf)
        End With
        ClientScript.RegisterStartupScript(Me.GetType, "", csScript.ToString)
    End Sub

    Private Sub search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles search.Click
        Dim dt As DataTable = GetMeisai(False)
        Dim csScript As New StringBuilder
        If grdBody.Rows.Count = 1 Then
            SetValueScript(dt)
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

    ''' <summary>
    ''' 請求先区分ドロップダウンリスト設定
    ''' </summary>
    ''' <param name="ddl">ドロップダウンリスト</param>
    ''' <param name="strSyubetu">名称種別</param>
    ''' <remarks></remarks>
    Sub SetKakutyou(ByVal ddl As DropDownList, ByVal strSyubetu As String)
        Dim dtTable As New DataTable
        Dim intCount As Integer = 0
        Dim TyousaKaisyaMasterBL As New Itis.Earth.BizLogic.TyousaKaisyaMasterLogic
        dtTable = TyousaKaisyaMasterBL.SelKakutyouInfo(strSyubetu)

        Dim ddlist As New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddl.Items.Add(ddlist)
        For intCount = 0 To dtTable.Rows.Count - 1
            ddlist = New ListItem
            ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("meisyou"))
            ddlist.Value = TrimNull(dtTable.Rows(intCount).Item("code"))
            ddl.Items.Add(ddlist)
        Next
    End Sub

    ''' <summary>空白を削除</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function

    ''' <summary>DDL設定</summary>
    Sub SetDropSelect(ByVal ddl As DropDownList, ByVal strValue As String)

        If ddl.Items.FindByValue(strValue) IsNot Nothing Then
            ddl.SelectedValue = strValue
        Else
            ddl.SelectedIndex = 0
        End If
    End Sub

End Class