Imports Itis.Earth.BizLogic
Partial Public Class search_SiharaiTyousa
    Inherits System.Web.UI.Page
    ''' <summary>���ʏ�񌟍�</summary>
    ''' <history>
    ''' <para>2010/05/20�@�n���R(��A���)�@�V�K�쐬</para>
    ''' </history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strKbn As String = ""
        Dim strFlg As String = ""
        Dim intCols As Integer = 0
        Dim intWidth(0) As String
        If Not IsPostBack Then
            '��ʂ�Form
            '�x���敪
            ViewState("SiharaiKbn") = Request.QueryString("SiharaiKbn")

            ViewState("strKbn") = Request.QueryString("Kbn")
            ViewState("strFormName") = Request.QueryString("FormName")
            ViewState("objCd2") = Request.QueryString("objCd2")
            ViewState("objHidCd2") = Request.QueryString("objHidCd2")
            ViewState("strCd") = Request.QueryString("strCd")
            If ViewState("SiharaiKbn") = "Siharai" Then
                search_Cd.Text = Request.QueryString("strCd") & Request.QueryString("strCd2")
            End If
            'ViewState("objCd") = Request.QueryString("objCd")

            ViewState("objMei") = Request.QueryString("objMei")
            ViewState("show") = Request.QueryString("show")

            '�����X

            intCols = setColWidth(intWidth, "head")

            grdHead.DataSource = CreateHeadDataSource(intCols)

            grdHead.DataBind()

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


        strKbn = ViewState("strKbn")


        lblKensaku.Text = "��������"
        Me.Title = "������Ќ���"
        lblTitle.Text = "������Ќ���"

        lblCd.Text = "������ЃR�[�h"
        lblMei.Text = "������Ж�"
        lblMei2.Text = "������ЃJ�i"
        divMeisai.Attributes.Add("style", "overflow-y:auto ; height:182px; width:758px;")

        clearWin.Attributes.Add("onclick", "return fncClear('" & search_Cd.ClientID & "','" & search_Mei.ClientID & "','" & search_Mei2.ClientID & "','" & maxSearchCount.ClientID & "');")
        MakeJavaScript()

        search_Mei2.Attributes.Add("onblur", "fncTokomozi(this)")

    End Sub
    ''' <summary>�����f�[�^��ݒ�</summary>
    Public Function CreateBodyDataSource(ByVal intRow As String) As DataTable
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic


        Dim dtKameitenSearchTable As New Itis.Earth.DataAccess.CommonSearchDataSet.tyousakaisyaTableDataTable
        dtKameitenSearchTable = CommonSearchLogic.GetTyousaInfo(intRow, _
                                                                    search_Cd.Text, _
                                                                    search_Mei.Text, _
                                                                    search_Mei2.Text)
        Return dtKameitenSearchTable


    End Function

    ''' <summary>GridView�f�[�^��̐ݒ�</summary>
    Function setColWidth(ByRef intwidth() As String, ByVal strName As String) As Integer
        setColWidth = 3
        ReDim intwidth(setColWidth)
        If strName = "head" Then
            intwidth(0) = "130px"
            intwidth(1) = "179px"
            intwidth(2) = "160px"
            intwidth(3) = "240px"
        Else
            intwidth(0) = "132px"
            intwidth(1) = "181px"
            intwidth(2) = "162px"
            intwidth(3) = "242px"
        End If
    End Function
    ''' <summary> GridView���e�A�t�H�[�}�b�g���Z�b�g</summary>
    Sub grdViewStyle(ByVal intwidth() As String, ByVal grd As GridView, Optional ByVal dt As DataTable = Nothing, Optional ByVal blnSort As Boolean = False)
        Dim intCol As Integer = 0
        Dim intRow As Integer = 0
        If IsArray(intwidth) AndAlso grd.Rows.Count > 0 Then
            For intRow = 0 To grd.Rows.Count - 1
                For intCol = 0 To grd.Rows(intRow).Cells.Count - 1
                    grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")
                    If grd.ID = "grdHead" And blnSort Then
                        Dim strSort As String = ""
                        If Not (dt Is Nothing) Then
                            strSort = dt.Columns(intCol).ColumnName
                        End If
                        Dim lbl As New Label
                        lbl.Text = grd.Rows(intRow).Cells(intCol).Text & " "
                        grd.Rows(intRow).Cells(intCol).Controls.Add(lbl)
                        Dim lnkBtn As New LinkButton
                        lnkBtn.Text = "��"
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
                        lnkBtn2.Text = "��"
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

    ''' <summary>�w�[�_�|���f�[�^��ݒ�</summary>
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

            .Item(0) = "������ЃR�[�h"
            .Item(1) = "������Ў��Ə��R�[�h"
            .Item(2) = "������Ж�"
            .Item(3) = "������ЏZ���P"


        End With
        dtHeader.Rows.Add(drTemp)

        Return dtHeader
    End Function
    ''' <summary>GridView�s�̎���</summary>
    Private Sub grdBody_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBody.RowDataBound
        e.Row.Attributes.Add("onclick", "selectedLineColor(this);")

        e.Row.Attributes.Add("ondblclick", "fncSetItem('" & ViewState("objCd2") & _
                                                               "','" & e.Row.Cells(0).ClientID & _
                                                               "','" & e.Row.Cells(1).ClientID & _
                                                               "','" & ViewState("objMei") & _
                                                               "','" & e.Row.Cells(2).ClientID & _
                                                               "','" & ViewState("strCd") & _
                                                               "','" & ViewState("objHidCd2") & _
                                                               "','" & ViewState("strFormName") & "');")


    End Sub
    ''' <summary>Javascript�쐬</summary>
    Protected Sub MakeJavaScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .Append("<script language='javascript' type='text/javascript'>  " & vbCrLf)

            .Append("function fncSetItem(objCd,strObjCd,strObjCd2,objMei,strObjMei,strCd,objHidCd2,FormName)" & vbCrLf)
            .Append("{" & vbCrLf)
            .Append("if(window.opener == null || window.opener.closed){" & vbCrLf)
            .Append("    alert('�Ăяo������ʂ�����ꂽ�ׁA�l���Z�b�g�ł��܂���B');" & vbCrLf)
            .Append("   return false;" & vbCrLf)
            .Append(" }else{" & vbCrLf)
            .Append(" }" & vbCrLf)
            .AppendLine("   if((eval('document.all.'+strObjCd).innerText)!=strCd){")
            If ViewState("strKbn") = "�x���W�v�掖�Ə�" Then
                .AppendLine("       alert('�x���W�v�掖�Ə��R�[�h�ƒ�����ЃR�[�h���قȂ邽�߁A�ݒ�ł��܂���B');")
            Else
                .AppendLine("       alert('�x�����׏W�v�掖�Ə��R�[�h�ƒ�����ЃR�[�h���قȂ邽�߁A�ݒ�ł��܂���B');")
            End If
            .AppendLine("       window.close();")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .Append("eval('window.opener.document.all.'+objCd).innerText=eval('document.all.'+strObjCd2).innerText;" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objHidCd2).innerText=eval('document.all.'+strObjCd2).innerText;" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objCd).select();" & vbCrLf)
            .Append("if (objMei!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objMei).innerText=eval('document.all.'+strObjMei).innerText;" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("window.close();" & vbCrLf)
            .Append("}" & vbCrLf)

            .Append("function fncClear(objcd,objmei,objmei2,objddl){" & vbCrLf)
            .Append("eval('document.all.'+objcd).innerText='';" & vbCrLf)
            .Append("eval('document.all.'+objmei).innerText='';" & vbCrLf)
            .Append("eval('document.all.'+objmei2).innerText='';" & vbCrLf)
            .Append("eval('document.all.'+objddl).selectedIndex=0;" & vbCrLf)
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

        intRowCount = CommonSearchLogic.GetTyousaCount(search_Cd.Text, _
                                                                    search_Mei.Text, _
                                                                    search_Mei2.Text)


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
    Sub SetValueScript()
        Dim csScript As New StringBuilder

        With csScript
            .Append("<script language='javascript' type='text/javascript'>  " & vbCrLf)
            .Append("if(window.opener == null || window.opener.closed){" & vbCrLf)
            .Append("    alert('�Ăяo������ʂ�����ꂽ�ׁA�l���Z�b�g�ł��܂���B');" & vbCrLf)
            .Append("  }else{" & vbCrLf)

            .AppendLine("   if((eval('document.all.'+'" & grdBody.Rows(0).Cells(0).ClientID & "').innerText)!='" & ViewState("strCd") & "'){")
            If ViewState("strKbn") = "�x���W�v�掖�Ə�" Then
                .AppendLine("       alert('�x���W�v�掖�Ə��R�[�h�ƒ�����ЃR�[�h���قȂ邽�߁A�ݒ�ł��܂���B');")
            Else
                .AppendLine("       alert('�x�����׏W�v�掖�Ə��R�[�h�ƒ�����ЃR�[�h���قȂ邽�߁A�ݒ�ł��܂���B');")
            End If
            .AppendLine("       window.close();")
            .AppendLine("   }else{")
            If ViewState("objCd2") <> "" Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("objCd2") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(1).ClientID & "').innerText;" & vbCrLf)
            End If
            If ViewState("objHidCd2") <> "" Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("objHidCd2") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(1).ClientID & "').innerText;" & vbCrLf)
            End If

            If ViewState("objMei") <> "" Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("objMei") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(2).ClientID & "').innerText;" & vbCrLf)
            End If

            If ViewState("objCd2") <> "" Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("objCd2") & "').select();" & vbCrLf)
            End If
            .Append("window.close();" & vbCrLf)
            .AppendLine("   }")

            .Append(" }" & vbCrLf)
            .Append("</script>" & vbCrLf)
        End With
        ClientScript.RegisterStartupScript(Me.GetType, "", csScript.ToString)
    End Sub

    Private Sub search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles search.Click
        Dim dt As DataTable = GetMeisai(False)
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
End Class