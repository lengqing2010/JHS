Imports Itis.Earth.BizLogic
Partial Public Class search_common
    Inherits System.Web.UI.Page
    ''' <summary>���ʏ�񌟍�</summary>
    ''' <history>
    ''' <para>2009/07/15�@����(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strKbn As String = ""
        Dim strFlg As String = ""
        Dim intCols As Integer = 0
        Dim intWidth(0) As String
        If Not IsPostBack Then
            '��ʂ�Form

            ViewState("strKbn") = Request.QueryString("Kbn")
            If ViewState("strKbn") = "�r���_ށ|" Then
                ViewState("strKbn") = "�r���_�["
            End If
            ViewState("strFlg") = Request.QueryString("Flag")
            ViewState("strFormName") = Request.QueryString("FormName")
            ViewState("objCd") = Request.QueryString("objCd")
            ViewState("objMei") = Request.QueryString("objMei")
            ViewState("objCd2") = Request.QueryString("objCd2")
            ViewState("objMei2") = Request.QueryString("objMei2")
            ViewState("objMei3") = Request.QueryString("objMei3")
            ViewState("soukoCd") = Request.QueryString("soukoCd")
            ViewState("objBtnKbn") = Request.QueryString("objBtnKbn")
            ViewState("show") = Request.QueryString("show")
            search_Cd.Text = Request.QueryString("strCd")
            'search_Cd.Attributes.Add("onblur", "fncToUpper(this);")
            If ViewState("soukoCd") = String.Empty Then
                ViewState("soukoCd") = 115
            End If

            '�����X
            ViewState("strKensakuKubun") = Request.QueryString("KensakuKubun")
            ViewState("blnDelete") = Request.QueryString("blnDelete")
            ViewState("submit") = Request.QueryString("submit")
            '==================2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���==========================
            '���
            ViewState("HidTorikesiCd") = Request.QueryString("HidTorikesiCd")
            If ViewState("HidTorikesiCd") Is Nothing Then
                ViewState("HidTorikesiCd") = String.Empty
            End If
            ViewState("TxtdTorikesiCd") = Request.QueryString("TxtdTorikesiCd")
            If ViewState("TxtdTorikesiCd") Is Nothing Then
                ViewState("TxtdTorikesiCd") = String.Empty
            End If
            ViewState("btnChangeColorCd") = Request.QueryString("btnChangeColorCd")
            If ViewState("btnChangeColorCd") Is Nothing Then
                ViewState("btnChangeColorCd") = String.Empty
            End If
            '==================2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���==========================


            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
            '�W�������i
            ViewState("objHyoujunkaKakaku") = Request.QueryString("objHyoujunkaKakaku")
            If ViewState("objHyoujunkaKakaku") Is Nothing Then
                ViewState("objHyoujunkaKakaku") = String.Empty
            End If
            '�߂�Ώ�
            ViewState("objRetrun") = Request.QueryString("objRetrun")
            If ViewState("objRetrun") Is Nothing Then
                ViewState("objRetrun") = String.Empty
            End If
            'hidden
            ViewState("hdnId") = Request.QueryString("hdnId")
            If ViewState("hdnId") Is Nothing Then
                ViewState("hdnId") = String.Empty
            End If
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================

            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
            ViewState("btnFcId") = Request.QueryString("btnFcId")
            If ViewState("btnFcId") Is Nothing Then
                ViewState("btnFcId") = String.Empty
            End If
            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================

            strKbn = ViewState("strKbn")

            strFlg = ViewState("strFlg")

            intCols = setColWidth(strKbn, intWidth, "head")

            '==================2011/05/11 �ԗ� �����������\���ύX �C�� �J�n��==========================
            'grdHead.DataSource = CreateHeadDataSource(strKbn, intCols)
            If strKbn = "���i���i" Then
                grdHead.DataSource = CreateHeadDataSource(strKbn, intCols - 1)
            ElseIf strKbn = "�����X" Then
                grdHead.DataSource = CreateHeadDataSource(strKbn, intCols - 2)
            Else
                grdHead.DataSource = CreateHeadDataSource(strKbn, intCols)
            End If
            '==================2011/05/11 �ԗ� �����������\���ύX �C�� �I����==========================

            grdHead.DataBind()



            If ViewState("show") = "True" And (strKbn = "���[�U�[" Or strKbn = "����") And search_Cd.Text.Trim <> "" Then
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

            Else
                grdViewStyle(intWidth, grdHead, , False)
            End If

        End If


        strKbn = ViewState("strKbn")
        If strKbn = "�X��" Then
            Me.Title = "�Z������"
            lblTitle.Text = "�Z������"
            lblCd.Text = "�X�֔ԍ�"
        ElseIf strKbn = "����" Then
            Me.Title = strKbn & "����"
            lblTitle.Text = strKbn & "����"
            lblCd.Text = "��b�d�lNO"
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
        ElseIf strKbn = "���i���i" Then
            Me.Title = "���i����"
            lblTitle.Text = "���i����"
            lblCd.Text = "���i�R�[�h"
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================
        ElseIf strKbn = "�H�����" Then
            Me.Title = "�H����Ќ���"
            lblTitle.Text = "�H����Ќ���"
            lblCd.Text = "�H����ЃR�[�h"
        Else
            Me.Title = strKbn & "����"
            lblTitle.Text = strKbn & "����"
            lblCd.Text = strKbn & "�R�[�h"
        End If

        lblKensaku.Text = "��������"
        Select Case strKbn
            Case "�����X"
                search_Cd.MaxLength = 5
                search_Mei.MaxLength = 20

                lblMei.Text = strKbn & "�J�i��"
                divMeisai.Attributes.Add("style", "overflow-y:auto ; height:182px; width:758px;")
            Case "�n��"
                lblMei.Text = strKbn & "��"
                divMeisai.Attributes.Add("style", "overflow-y:auto ; height:182px; width:592px;")
            Case "�H�����"
                search_Cd.MaxLength = 7
                lblMei.Text = strKbn & "��"
                divMeisai.Attributes.Add("style", "overflow-y:auto ; height:182px; width:752px;")
            Case "���i"
                lblMei.Text = strKbn & "��"
                divMeisai.Attributes.Add("style", "overflow-y:auto ; height:182px; width:590px;")
            Case "�c�Ə�"
                lblMei.Text = strKbn & "��"
                divMeisai.Attributes.Add("style", "overflow-y:auto ; height:182px; width:598px;")
            Case "�r���_�["
                lblMei.Text = strKbn & "��"
                divMeisai.Attributes.Add("style", "overflow-y:auto ; height:182px;width:608px;")
            Case "���[�U�["
                lblMei.Text = "����"
                divMeisai.Attributes.Add("style", "overflow-y:auto ; height:182px;width:608px;")
            Case "���"
                lblMei.Text = "��ʖ�"
                divMeisai.Attributes.Add("style", "overflow-y:auto ; height:182px;width:608px;")
            Case "����"
                lblMei.Text = "��b�d�l"
                divMeisai.Attributes.Add("style", "overflow-y:auto ; height:182px;width:608px;")
            Case "�X��"
                lblMei.Text = "�Z��"
                divMeisai.Attributes.Add("style", "overflow-y:auto ; height:182px;width:635px;")
                GetMeisai(False)

                Dim csScript1 As New StringBuilder
                If grdBody.Rows.Count = 0 Then
                    With csScript1
                        .Append("<script language='javascript' type='text/javascript'>  " & vbCrLf)

                        .Append("alert('" & Messages.Instance.MSG020E & "');" & vbCrLf)
                        .Append("</script>" & vbCrLf)
                    End With
                    ClientScript.RegisterStartupScript(Me.GetType, "", csScript1.ToString)
                End If
                '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
            Case "���i���i"
                lblMei.Text = "���i��"
                divMeisai.Attributes.Add("style", "overflow-y:auto ; height:182px; width:591px;")
                '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================

        End Select
        clearWin.Attributes.Add("onclick", "return fncClear('" & search_Cd.ClientID & "','" & search_Mei.ClientID & "','" & maxSearchCount.ClientID & "');")
        MakeJavaScript()
    End Sub
    ''' <summary>�����f�[�^��ݒ�</summary>
    Public Function CreateBodyDataSource(ByVal strKbn As String, ByVal intRow As String) As DataTable
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic

        Select Case strKbn
            Case "�c�Ə�"

                Dim dtEigyousyoTable As New Itis.Earth.DataAccess.CommonSearchDataSet.EigyousyoTableDataTable
                dtEigyousyoTable = CommonSearchLogic.GetEigyousyoInfo(intRow, search_Cd.Text, search_Mei.Text, ViewState("blnDelete"))
                Return dtEigyousyoTable
            Case "�����X"
                Dim dtKameitenSearchTable As New Itis.Earth.DataAccess.CommonSearchDataSet.KameitenSearchTableDataTable
                dtKameitenSearchTable = CommonSearchLogic.GetKameitenKensakuInfo(intRow, _
                                                                            ViewState("strKensakuKubun"), _
                                                                            search_Cd.Text, _
                                                                            search_Mei.Text, _
                                                                            ViewState("blnDelete"))
                Return dtKameitenSearchTable
            Case "�H�����"
                Dim dtKojKaisyaTable As New DataTable
                dtKojKaisyaTable = CommonSearchLogic.GetKojKaisyaKensakuInfo(intRow, search_Cd.Text, search_Mei.Text, ViewState("blnDelete"))
                Return dtKojKaisyaTable
            Case "�n��"
                Dim dtKeiretuTable As New Itis.Earth.DataAccess.CommonSearchDataSet.KeiretuTableDataTable
                dtKeiretuTable = CommonSearchLogic.GetKeiretuKensakuInfo(intRow, _
                                                                            ViewState("strKensakuKubun"), _
                                                                            search_Cd.Text, _
                                                                            search_Mei.Text, _
                                                                            ViewState("blnDelete"))
                Return dtKeiretuTable
            Case "���i"
                Dim dtSyouhinTable As New Itis.Earth.DataAccess.CommonSearchDataSet.SyouhinTableDataTable
                dtSyouhinTable = CommonSearchLogic.GetSyouhinInfo(intRow, _
                                                                            search_Cd.Text, _
                                                                            search_Mei.Text, _
                                                                            ViewState("soukoCd"), _
                                                                            ViewState("blnDelete"))
                Return dtSyouhinTable


            Case "�r���_�["
                Dim dtBirudaTable As New Itis.Earth.DataAccess.CommonSearchDataSet.BirudaTableDataTable
                dtBirudaTable = CommonSearchLogic.GetBirudaInfo(intRow, _
                                                                            search_Cd.Text, _
                                                                            search_Mei.Text, _
                                                                            ViewState("blnDelete"))

                Return dtBirudaTable
            Case "���[�U�["
                Dim dtBirudaTable As New Itis.Earth.DataAccess.CommonSearchDataSet.BirudaTableDataTable
                dtBirudaTable = CommonSearchLogic.GetUserInfo(intRow, _
                                                                            search_Cd.Text, _
                                                                            search_Mei.Text, _
                                                                            ViewState("blnDelete"))



                Return dtBirudaTable

            Case "���"
                Dim dtBirudaTable As New Itis.Earth.DataAccess.CommonSearchDataSet.meisyouTableDataTable
                dtBirudaTable = CommonSearchLogic.Selkameitensyubetu(intRow, _
                                                                            search_Cd.Text, _
                                                                            search_Mei.Text)

                Return dtBirudaTable
            Case "����"
                Dim dtBirudaTable As New Itis.Earth.DataAccess.CommonSearchDataSet.IntTableDataTable
                dtBirudaTable = CommonSearchLogic.SelSiyouInfo(intRow, _
                                                                            search_Cd.Text, _
                                                                            search_Mei.Text)

                Return dtBirudaTable
                '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
            Case "���i���i"
                Dim dtSyouhinKakakuTable As New Data.DataTable
                dtSyouhinKakakuTable = CommonSearchLogic.GetSyouhinKakakuInfo(intRow, _
                                                                            search_Cd.Text, _
                                                                            search_Mei.Text, _
                                                                            ViewState("soukoCd"), _
                                                                            ViewState("blnDelete"))
                Return dtSyouhinKakakuTable
                '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================
            Case Else ' "�X��"
                Dim dtBirudaTable As New Itis.Earth.DataAccess.CommonSearchDataSet.BirudaTableDataTable
                dtBirudaTable = CommonSearchLogic.SelYuubinInfo(intRow, _
                                                                            search_Cd.Text, _
                                                                            search_Mei.Text)



                Dim dtBody As New DataTable
                Dim drTemp As DataRow
                Dim intRowCount As Integer = 0
                Dim strTmp As String = ""
                dtBody.Columns.Add(New DataColumn("col1", GetType(String)))
                dtBody.Columns.Add(New DataColumn("col2", GetType(String)))
                dtBody.Columns.Add(New DataColumn("col3", GetType(String)))
                dtBody.Columns.Add(New DataColumn("col4", GetType(String)))
                If dtBirudaTable.Rows.Count <> 0 Then
                    For intRowCount = 0 To dtBirudaTable.Rows.Count - 1
                        drTemp = dtBody.NewRow
                        drTemp.Item(0) = dtBirudaTable.Rows(intRowCount).Item(0)
                        strTmp = GetJyusho(Split(dtBirudaTable.Rows(intRowCount).Item(1), ",")(0))
                        drTemp.Item(1) = Split(strTmp, ",")(0)

                        drTemp.Item(2) = Split(strTmp, ",")(1)
                        drTemp.Item(3) = Split(dtBirudaTable.Rows(intRowCount).Item(1), ",")(1)
                        dtBody.Rows.Add(drTemp)
                    Next
                End If
                Return dtBody

        End Select

    End Function
    Private Function GetJyusho(ByVal value As String) As String
        Dim i As Integer
        If value.Length > 20 Then
            For i = 20 To value.Length
                If System.Text.Encoding.Default.GetBytes(Left(value, i)).Length >= 39 Then
                    Return value.Substring(0, i) & "," & value.Substring(i, value.Length - i)
                End If
            Next

        End If
        Return value & ","
    End Function
    ''' <summary>GridView�f�[�^��̐ݒ�</summary>
    Function setColWidth(ByVal strKbn As String, ByRef intwidth() As String, ByVal strName As String) As Integer
        Select Case strKbn
            Case "�����X"
                '================2012/03/28 �ԗ� 405721�Č��̑Ή� �C����=========================
                'setColWidth = 3
                setColWidth = 5
                '================2012/03/28 �ԗ� 405721�Č��̑Ή� �C����=========================
                ReDim intwidth(setColWidth)
                If strName = "head" Then
                    intwidth(0) = "122px"
                    intwidth(1) = "279px"
                    intwidth(2) = "130px"
                    intwidth(3) = "178px"
                    '================2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
                    intwidth(4) = "1px"
                    intwidth(5) = "1px"
                    '================2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
                Else
                    intwidth(0) = "124px"
                    intwidth(1) = "281"
                    intwidth(2) = "132px"
                    intwidth(3) = "180px"
                    '================2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
                    intwidth(4) = "1px"
                    intwidth(5) = "1px"
                    '================2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
                End If
            Case "�H�����"
                setColWidth = 2
                ReDim intwidth(setColWidth)
                If strName = "head" Then
                    intwidth(0) = "154px"
                    intwidth(1) = "279px"
                    intwidth(2) = "278px"
                Else
                    intwidth(0) = "156px"
                    intwidth(1) = "281px"
                    intwidth(2) = "280px"
                End If
            Case "�n��"
                setColWidth = 2
                ReDim intwidth(setColWidth)
                If strName = "head" Then
                    intwidth(0) = "70px"
                    intwidth(1) = "112px"
                    intwidth(2) = "369px"
                Else
                    intwidth(0) = "72px"
                    intwidth(1) = "114px"
                    intwidth(2) = "371px"
                End If
            Case "���i"
                setColWidth = 1
                ReDim intwidth(setColWidth)
                If strName = "head" Then
                    intwidth(0) = "113px"
                    intwidth(1) = "445px"
                Else
                    intwidth(0) = "115px"
                    intwidth(1) = "447px"
                End If
            Case "�r���_�[", "���[�U�[", "���", "����"
                setColWidth = 1
                ReDim intwidth(setColWidth)
                If strName = "head" Then
                    intwidth(0) = "140px"
                    intwidth(1) = "435px"
                Else
                    intwidth(0) = "142px"
                    intwidth(1) = "437px"
                End If
            Case "�X��"
                setColWidth = 2
                ReDim intwidth(setColWidth)
                If strName = "head" Then
                    intwidth(0) = "110px"
                    intwidth(1) = "180px"
                    intwidth(2) = "305px"
                    'intwidth(1) = "20px"
                Else
                    intwidth(0) = "112px"
                    intwidth(1) = "182px"
                    intwidth(2) = "307px"
                    'intwidth(3) = "20px"
                End If
                '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
            Case "���i���i"
                setColWidth = 2
                ReDim intwidth(setColWidth)
                If strName = "head" Then
                    intwidth(0) = "113px"
                    intwidth(1) = "445px"
                    intwidth(2) = "1px"
                Else
                    intwidth(0) = "115px"
                    intwidth(1) = "447px"
                    intwidth(2) = "1px"
                End If
                '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================

            Case Else
                setColWidth = 1
                ReDim intwidth(setColWidth)
                If strName = "head" Then
                    intwidth(0) = "120px"
                    intwidth(1) = "445px"
                Else
                    intwidth(0) = "122px"
                    intwidth(1) = "447px"
                End If
        End Select
    End Function
    ''' <summary> GridView���e�A�t�H�[�}�b�g���Z�b�g</summary>
    Sub grdViewStyle(ByVal intwidth() As String, ByVal grd As GridView, Optional ByVal dt As DataTable = Nothing, Optional ByVal blnSort As Boolean = False)
        Dim intCol As Integer = 0
        Dim intRow As Integer = 0
        If IsArray(intwidth) AndAlso grd.Rows.Count > 0 Then
            For intRow = 0 To grd.Rows.Count - 1
                For intCol = 0 To grd.Rows(intRow).Cells.Count - 1
                    If ViewState("strKbn") = "�X��" Then
                        If grd.ID = "grdBody" Then
                            If intCol < grd.Rows(intRow).Cells.Count - 1 Then

                                grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")

                            Else
                                grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "width:0px;")
                            End If
                        Else
                            grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")
                        End If
                    Else
                        '==================2011/05/11 �ԗ� �����������\���ύX �C�� �J�n��==========================
                        'grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")
                        If ((ViewState("strKbn") = "���i���i") OrElse (ViewState("strKbn") = "�����X")) AndAlso grd.ID = "grdBody" Then
                            grd.Rows(intRow).Cells(intCol).Style.Add("border-width", "2px")
                            grd.Rows(intRow).Cells(intCol).Style.Add("border-style", "solid")
                            grd.Rows(intRow).Cells(intCol).Style.Add("width", intwidth(intCol))
                        Else
                            grd.Rows(intRow).Cells(intCol).Attributes.Add("style", "border-width:2px;border-style:solid;width:" & intwidth(intCol) & ";")
                        End If
                        '==================2011/05/11 �ԗ� �����������\���ύX �C�� �I����==========================
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
    Public Function CreateHeadDataSource(ByVal strKbn As String, ByVal intCols As Integer) As DataTable
        Dim intColCount As Integer = 0
        Dim intRowCount As Integer = 0
        Dim dtHeader As New DataTable
        Dim drTemp As DataRow
        For intColCount = 0 To intCols
            dtHeader.Columns.Add(New DataColumn("col" & intColCount.ToString, GetType(String)))
        Next
        drTemp = dtHeader.NewRow
        With drTemp
            Select Case strKbn

                Case "�����X"
                    .Item(0) = strKbn & "�R�[�h"
                    .Item(1) = strKbn & "��"
                    .Item(2) = "�s���{����"
                    .Item(3) = strKbn & "�J�i�� "
                Case "�n��"
                    .Item(0) = "�敪"
                    .Item(1) = strKbn & "�R�[�h"
                    .Item(2) = strKbn & "��"
                Case "�H�����"
                    .Item(0) = strKbn & "�R�[�h"
                    .Item(1) = strKbn & "��"
                    .Item(2) = strKbn & "�J�i��"
                Case "���[�U�["
                    .Item(0) = "���[�U�[�h�c"
                    .Item(1) = "����"
                Case "�X��"
                    .Item(0) = "�X�֔ԍ�"
                    .Item(1) = "�Z��1"
                    .Item(2) = "�Z��2"
                Case "����"
                    .Item(0) = "��b�d�lNO"
                    .Item(1) = "��b�d�l"
                    '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
                Case "���i���i"
                    .Item(0) = "���i�R�[�h"
                    .Item(1) = "���i��"
                    '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================
                Case Else
                    .Item(0) = strKbn & "�R�[�h"
                    .Item(1) = strKbn & "��"
            End Select
        End With
        dtHeader.Rows.Add(drTemp)

        Return dtHeader
    End Function
    ''' <summary>GridView�s�̎���</summary>
    Private Sub grdBody_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBody.RowDataBound
        e.Row.Attributes.Add("onclick", "selectedLineColor(this);")
        If ViewState("strKbn") = "�n��" Then
            e.Row.Attributes.Add("ondblclick", "fncSetItem('" & ViewState("objCd") & _
                                                                   "','" & e.Row.Cells(1).ClientID & _
                                                                   "','" & ViewState("objMei") & _
                                                                   "','" & e.Row.Cells(2).ClientID & _
                                                                   "','" & ViewState("strFormName") & "');")
        ElseIf ViewState("strKbn") = "�H�����" Then
            e.Row.Attributes.Add("ondblclick", "fncSetItem('" & ViewState("objCd") & _
                                                                              "','" & e.Row.Cells(0).ClientID & _
                                                                              "','" & ViewState("objMei") & _
                                                                              "','" & e.Row.Cells(1).ClientID & _
                                                                              "','" & ViewState("strFormName") & "');")
        ElseIf ViewState("strKbn") = "�X��" Then
            e.Row.Attributes.Add("ondblclick", "fncSetItem2('" & ViewState("objCd") & _
                                                                              "','" & e.Row.Cells(0).ClientID & _
                                                                              "','" & ViewState("objMei") & _
                                                                              "','" & e.Row.Cells(1).ClientID & _
                                                                              "','" & ViewState("objMei2") & _
                                                                              "','" & e.Row.Cells(2).ClientID & _
                                                                               "','" & ViewState("objMei3") & _
                                                                               "','" & e.Row.Cells(3).Text & _
                                                                              "','" & ViewState("strFormName") & "');")
            e.Row.Cells(3).Visible = False
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
        ElseIf ViewState("strKbn") = "���i���i" Then
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim strhyoujunkakaku As String = e.Row.Cells(2).Text
                If (strhyoujunkakaku.Trim.Equals("&nbsp;")) OrElse (strhyoujunkakaku.Trim.Equals(String.Empty)) Then
                    e.Row.Cells(2).Text = String.Empty
                Else
                    e.Row.Cells(2).Text = FormatNumber(strhyoujunkakaku, 0) & "�~"
                End If
            End If
            e.Row.Attributes.Add("ondblclick", "fncSetItemSyouhinKakaku('" & ViewState("objCd") & _
                                                        "','" & e.Row.Cells(0).ClientID & _
                                                        "','" & ViewState("objMei") & _
                                                        "','" & e.Row.Cells(1).ClientID & _
                                                        "','" & ViewState("objHyoujunkaKakaku") & _
                                                        "','" & e.Row.Cells(2).ClientID & _
                                                        "','" & ViewState("strFormName") & "');")
            e.Row.Cells(2).Style.Add("display", "none")
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================
            '================2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
        ElseIf ViewState("strKbn") = "�����X" Then
            e.Row.Attributes.Add("ondblclick", "fncSetItemKameiten('" & ViewState("objCd") & _
                                                                   "','" & e.Row.Cells(0).ClientID & _
                                                                   "','" & ViewState("objMei") & _
                                                                   "','" & e.Row.Cells(1).ClientID & _
                                                                   "','" & e.Row.Cells(4).ClientID & _
                                                                   "','" & e.Row.Cells(5).ClientID & _
                                                                   "','" & ViewState("strFormName") & "');")
            e.Row.Cells(4).Style.Add("display", "none")
            e.Row.Cells(5).Style.Add("display", "none")
            '================2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
        Else
            e.Row.Attributes.Add("ondblclick", "fncSetItem('" & ViewState("objCd") & _
                                                                   "','" & e.Row.Cells(0).ClientID & _
                                                                   "','" & ViewState("objMei") & _
                                                                   "','" & e.Row.Cells(1).ClientID & _
                                                                   "','" & ViewState("strFormName") & "');")
        End If

    End Sub
    ''' <summary>Javascript�쐬</summary>
    Protected Sub MakeJavaScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .Append("<script language='javascript' type='text/javascript'>  " & vbCrLf)
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
            .Append("function fncSetItemSyouhinKakaku(objCd,strObjCd,objMei,strObjMei,objHyoujunkaKakaku,strHyoujunkaKakaku,FormName)" & vbCrLf)
            .Append("{" & vbCrLf)
            .Append("if(window.opener == null || window.opener.closed){" & vbCrLf)
            .Append("    alert('�Ăяo������ʂ�����ꂽ�ׁA�l���Z�b�g�ł��܂���B');" & vbCrLf)
            .Append("   return false;" & vbCrLf)
            .Append(" }else{" & vbCrLf)
            .Append(" }" & vbCrLf)
            '���i�R�[�h
            .Append("eval('window.opener.document.all.'+objCd).innerText=eval('document.all.'+strObjCd).innerText;" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objCd).select();" & vbCrLf)
            '���i��
            .Append("if (objMei!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objMei).innerText=eval('document.all.'+strObjMei).innerText;" & vbCrLf)
            .Append("}" & vbCrLf)
            '�W�������i
            .Append("if (objHyoujunkaKakaku!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objHyoujunkaKakaku).innerText=eval('document.all.'+strHyoujunkaKakaku).innerText;" & vbCrLf)
            .Append("}" & vbCrLf)
            If ViewState("submit") = True Then
                .Append("eval('window.opener.'+'" & ViewState("strFormName") & "'+'.'+'" & ViewState("objBtnKbn") & "').value='0';" & vbCrLf)
                .Append("window.opener.document.forms[0].submit();" & vbCrLf)
            End If
            .Append("window.close();" & vbCrLf)
            .Append("}" & vbCrLf)
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================

            '==================2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���==========================
            .Append("function fncSetItemKameiten(objCd,strObjCd,objMei,strObjMei,strTorikesi,strTorikesiTxt,FormName)" & vbCrLf)
            .Append("{" & vbCrLf)
            .Append("if(window.opener == null || window.opener.closed){" & vbCrLf)
            .Append("    alert('�Ăяo������ʂ�����ꂽ�ׁA�l���Z�b�g�ł��܂���B');" & vbCrLf)
            .Append("   return false;" & vbCrLf)
            .Append(" }else{" & vbCrLf)
            .Append(" }" & vbCrLf)
            '�����X�R�[�h
            .Append("eval('window.opener.document.all.'+objCd).innerText=eval('document.all.'+strObjCd).innerText;" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objCd).select();" & vbCrLf)
            '�����X��
            .Append("if (objMei!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objMei).innerText=eval('document.all.'+strObjMei).innerText;" & vbCrLf)
            .Append("}" & vbCrLf)
            '�����X ���
            If Not ViewState("HidTorikesiCd").ToString.Trim.Equals(String.Empty) Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("HidTorikesiCd") & "').value=eval('document.all.'+strTorikesi).innerText;" & vbCrLf)
            End If
            If Not ViewState("TxtdTorikesiCd").ToString.Trim.Equals(String.Empty) Then
                .Append("if (eval('document.all.'+strTorikesi).innerText=='0'){" & vbCrLf)
                .Append("   eval('window.opener.document.all.'+'" & ViewState("TxtdTorikesiCd") & "').value='';" & vbCrLf)
                .Append("}" & vbCrLf)
                .Append("else{" & vbCrLf)
                .Append("   eval('window.opener.document.all.'+'" & ViewState("TxtdTorikesiCd") & "').value=eval('document.all.'+strTorikesi).innerText + ':' + eval('document.all.'+strTorikesiTxt).innerText;" & vbCrLf)
                .Append("}" & vbCrLf)
            End If
            If Not ViewState("btnChangeColorCd").ToString.Trim.Equals(String.Empty) Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("btnChangeColorCd") & "').click();" & vbCrLf)
            End If

            If ViewState("submit") = True Then
                .Append("eval('window.opener.'+'" & ViewState("strFormName") & "'+'.'+'" & ViewState("objBtnKbn") & "').value='0';" & vbCrLf)
                .Append("window.opener.document.forms[0].submit();" & vbCrLf)
            End If
            If Not ViewState("hdnId").ToString.Trim.Equals(String.Empty) Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("hdnId") & "').value=eval('document.all.'+strObjCd).innerText;" & vbCrLf)
            End If
            If Not ViewState("objRetrun").ToString.Trim.Equals(String.Empty) Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("objRetrun") & "').value='1';" & vbCrLf)
            End If
            .Append("window.close();" & vbCrLf)
            .Append("}" & vbCrLf)
            '==================2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���==========================

            .Append("function fncSetItem(objCd,strObjCd,objMei,strObjMei,FormName)" & vbCrLf)
            .Append("{" & vbCrLf)
            .Append("if(window.opener == null || window.opener.closed){" & vbCrLf)
            .Append("    alert('�Ăяo������ʂ�����ꂽ�ׁA�l���Z�b�g�ł��܂���B');" & vbCrLf)
            .Append("   return false;" & vbCrLf)
            .Append(" }else{" & vbCrLf)
            .Append(" }" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objCd).innerText=eval('document.all.'+strObjCd).innerText;" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objCd).select();" & vbCrLf)
            .Append("if (objMei!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objMei).innerText=eval('document.all.'+strObjMei).innerText;" & vbCrLf)
            .Append("}" & vbCrLf)
            If ViewState("submit") = True Then
                .Append("eval('window.opener.'+'" & ViewState("strFormName") & "'+'.'+'" & ViewState("objBtnKbn") & "').value='0';" & vbCrLf)
                .Append("window.opener.document.forms[0].submit();" & vbCrLf)
            End If
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
            If Not ViewState("hdnId").ToString.Trim.Equals(String.Empty) Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("hdnId") & "').value=eval('document.all.'+strObjCd).innerText;" & vbCrLf)
            End If
            If Not ViewState("objRetrun").ToString.Trim.Equals(String.Empty) Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("objRetrun") & "').value='1';" & vbCrLf)
            End If
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================

            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
            If Not ViewState("btnFcId").ToString.Trim.Equals(String.Empty) Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("btnFcId") & "').click();" & vbCrLf)
            End If
            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================

            .Append("window.close();" & vbCrLf)
            .Append("}" & vbCrLf)

            .Append("function fncSetItem2(objCd,strObjCd,objMei,strObjMei,objMei2,strObjMei2,objMei3,strObjMei3,FormName)" & vbCrLf)
            .Append("{" & vbCrLf)
            .Append("if(window.opener == null || window.opener.closed){" & vbCrLf)
            .Append("    alert('�Ăяo������ʂ�����ꂽ�ׁA�l���Z�b�g�ł��܂���B');" & vbCrLf)
            .Append("   return false;" & vbCrLf)
            .Append(" }else{" & vbCrLf)
            .Append(" }" & vbCrLf)


            '.Append("eval('window.opener.document.all.'+objCd).innerText=eval('document.all.'+strObjCd).innerText;" & vbCrLf)
            '.Append("eval('window.opener.document.all.'+objCd).select();" & vbCrLf)

            'If ViewState("objMei2") = String.Empty Then

            '    .Append("if (objMei!=''){" & vbCrLf)
            '    .Append("eval('window.opener.document.all.'+objMei).innerText=eval('document.all.'+strObjMei).innerText;" & vbCrLf)
            '    .Append("}" & vbCrLf)

            'Else
            '    .Append("if (eval('window.opener.document.all.'+objMei).value.Trim()==''){" & vbCrLf)
            '    .Append("if (objMei!=''){" & vbCrLf)
            '    .Append("eval('window.opener.document.all.'+objMei).innerText=eval('document.all.'+strObjMei).innerText;" & vbCrLf)
            '    .Append("}" & vbCrLf)
            '    .Append("if (objMei2!=''){" & vbCrLf)
            '    .Append("eval('window.opener.document.all.'+objMei2).innerText=eval('document.all.'+strObjMei2).innerText;" & vbCrLf)
            '    .Append("}" & vbCrLf)
            '    .Append("}" & vbCrLf)
            'End If
            If Not IsNothing(ViewState("objMei3")) Then
                .Append("if(eval('window.opener.document.all.'+'" & ViewState("objMei") & "').value!='' || eval('window.opener.document.all.'+'" & ViewState("objMei2") & "').value!='' || eval('window.opener.document.all.'+'" & ViewState("objMei3") & "').value!='')    {" & vbCrLf)

            Else
                .Append("if(eval('window.opener.document.all.'+'" & ViewState("objMei") & "').value!='' || eval('window.opener.document.all.'+'" & ViewState("objMei2") & "').value!='' )    {" & vbCrLf)

            End If
            .Append("if (!confirm('�����f�[�^������܂����㏑�����Ă�낵���ł����B')){window.close();return false;}}" & vbCrLf)

            .Append("eval('window.opener.document.all.'+objCd).innerText=eval('document.all.'+strObjCd).innerText;" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objCd).select();" & vbCrLf)

            .Append("if ('" & ViewState("objMei") & "'!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objMei).innerText=eval('document.all.'+strObjMei).innerText.Trim();" & vbCrLf)
            .Append(" }" & vbCrLf)
            .Append("if ('" & ViewState("objMei2") & "'!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objMei2).innerText=eval('document.all.'+strObjMei2).innerText.Trim();" & vbCrLf)
            .Append(" }" & vbCrLf)
            .Append("if ('" & ViewState("objMei3") & "'!=''){" & vbCrLf)
            .Append("eval('window.opener.document.all.'+objMei3).value=strObjMei3.Trim();" & vbCrLf)
            .Append(" }" & vbCrLf)

            If ViewState("submit") = True Then
                .Append("window.opener.document.forms[0].submit();" & vbCrLf)
            End If
            .Append("window.close();" & vbCrLf)
            .Append("}" & vbCrLf)


            .Append("function fncClear(objcd,objmei,objddl){" & vbCrLf)
            .Append("eval('document.all.'+objcd).innerText='';" & vbCrLf)
            .Append("eval('document.all.'+objmei).innerText='';" & vbCrLf)

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
        setColWidth(strKbn, intWidth, "body")
        dt = CreateBodyDataSource(strKbn, maxSearchCount.Value)
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
        Select Case strKbn
            Case "�c�Ə�"
                intRowCount = CommonSearchLogic.GetEigyousyoCount(search_Cd.Text, search_Mei.Text, ViewState("blnDelete"))
            Case "�H�����"
                intRowCount = CommonSearchLogic.GetKojKaisyaKensakuCount(search_Cd.Text, search_Mei.Text, ViewState("blnDelete"))

            Case "�����X"
                intRowCount = CommonSearchLogic.GetKameitenKensakuCount(ViewState("strKensakuKubun"), _
                                                                        search_Cd.Text, _
                                                                        search_Mei.Text, _
                                                                        ViewState("blnDelete"))
            Case "�n��"
                intRowCount = CommonSearchLogic.GetKeiretuKensakuCount(ViewState("strKensakuKubun"), _
                                                                        search_Cd.Text, _
                                                                        search_Mei.Text, _
                                                                        ViewState("blnDelete"))
            Case "���i"
                intRowCount = CommonSearchLogic.GetSyouhinCount(search_Cd.Text, search_Mei.Text, ViewState("soukoCd"), ViewState("blnDelete"))
            Case "�r���_�["
                intRowCount = CommonSearchLogic.GetBirudaCount(search_Cd.Text, search_Mei.Text, _
                                                                        ViewState("blnDelete"))
            Case "���[�U�["
                intRowCount = CommonSearchLogic.GetUserCount(search_Cd.Text, search_Mei.Text, _
                                                                        ViewState("blnDelete"))
            Case "���"
                intRowCount = CommonSearchLogic.SelkameitensyubetuCount(search_Cd.Text, search_Mei.Text)

            Case "�X��"
                intRowCount = CommonSearchLogic.SelYuubinCount(search_Cd.Text, search_Mei.Text)
            Case "����"
                intRowCount = CommonSearchLogic.SelSiyouCount(search_Cd.Text, search_Mei.Text)
                '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
            Case "���i���i"
                intRowCount = CommonSearchLogic.GetSyouhinCount(search_Cd.Text, search_Mei.Text, ViewState("soukoCd"), ViewState("blnDelete"))
                '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================
        End Select

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
            .Append("    alert('�Ăяo������ʂ�����ꂽ�ׁA�l���Z�b�g�ł��܂���B');" & vbCrLf)
            .Append("  }else{" & vbCrLf)
            If ViewState("strKbn") = "�n��" Then
                .Append("eval('window.opener.'+'" & ViewState("strFormName") & "'+'.'+'" & ViewState("objCd") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(1).ClientID & "').innerText;" & vbCrLf)
                .Append("if ('" & ViewState("objMei") & "'!=''){" & vbCrLf)
                .Append("eval('window.opener.'+'" & ViewState("strFormName") & "'+'.'+'" & ViewState("objMei") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(2).ClientID & "').innerText;" & vbCrLf)
                .Append(" }" & vbCrLf)
            ElseIf ViewState("strKbn") = "�X��" Then
                .Append("var flg = true;")
                If Not IsNothing(ViewState("objMei3")) Then
                    .Append("if(eval('window.opener.document.all.'+'" & ViewState("objMei") & "').value !='' || eval('window.opener.document.all.'+'" & ViewState("objMei2") & "').value !='' || eval('window.opener.document.all.'+'" & ViewState("objMei3") & "').value !='')    {" & vbCrLf)

                Else
                    .Append("if(eval('window.opener.document.all.'+'" & ViewState("objMei") & "').value !='' || eval('window.opener.document.all.'+'" & ViewState("objMei2") & "').value !='')    {" & vbCrLf)

                End If
                .Append("if (!confirm('�����f�[�^������܂����㏑�����Ă�낵���ł����B')){flg = false;} }" & vbCrLf)


                .Append("if( flg){")
                .Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(0).ClientID & "').innerText;" & vbCrLf)
                .Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').select();" & vbCrLf)
                .Append("if ('" & ViewState("objMei") & "'!=''){" & vbCrLf)
                .Append("eval('window.opener.document.all.'+'" & ViewState("objMei") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(1).ClientID & "').innerText;" & vbCrLf)
                .Append(" }" & vbCrLf)
                .Append("if ('" & ViewState("objMei2") & "'!=''){" & vbCrLf)
                .Append("eval('window.opener.document.all.'+'" & ViewState("objMei2") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(2).ClientID & "').innerText;" & vbCrLf)
                .Append(" }" & vbCrLf)
                .Append("if ('" & ViewState("objMei3") & "'!=''){" & vbCrLf)
                .Append("eval('window.opener.document.all.'+'" & ViewState("objMei3") & "').value='" & dt.Rows(0).Item(3) & "';" & vbCrLf)
                .Append(" }" & vbCrLf)
                .Append(" }" & vbCrLf)
                '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
            ElseIf ViewState("strKbn") = "���i���i" Then
                '���i�R�[�h
                .Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(0).ClientID & "').innerText;" & vbCrLf)
                .Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').select();" & vbCrLf)
                '���i��
                .Append("if ('" & ViewState("objMei") & "'!=''){" & vbCrLf)
                .Append("eval('window.opener.document.all.'+'" & ViewState("objMei") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(1).ClientID & "').innerText;" & vbCrLf)
                .Append(" }" & vbCrLf)
                '�W�������i
                .Append("if ('" & ViewState("objHyoujunkaKakaku") & "'!=''){" & vbCrLf)
                .Append("eval('window.opener.document.all.'+'" & ViewState("objHyoujunkaKakaku") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(2).ClientID & "').innerText;" & vbCrLf)
                .Append(" }" & vbCrLf)
                '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================

                '==================2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���==========================
            ElseIf ViewState("strKbn") = "�����X" Then
                '�����X�R�[�h
                .Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(0).ClientID & "').innerText;" & vbCrLf)
                .Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').select();" & vbCrLf)
                '�����X��
                .Append("if ('" & ViewState("objMei") & "'!=''){" & vbCrLf)
                .Append("eval('window.opener.document.all.'+'" & ViewState("objMei") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(1).ClientID & "').innerText;" & vbCrLf)
                .Append(" }" & vbCrLf)
                '�����X ���
                .Append("if ('" & ViewState("HidTorikesiCd") & "'!=''){" & vbCrLf)
                .Append("eval('window.opener.document.all.'+'" & ViewState("HidTorikesiCd") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(4).ClientID & "').innerText;" & vbCrLf)
                .Append(" }" & vbCrLf)
                .Append("if ('" & ViewState("TxtdTorikesiCd") & "'!=''){" & vbCrLf)
                .Append("   if (eval('document.all.'+'" & grdBody.Rows(0).Cells(4).ClientID & "').innerText=='0'){" & vbCrLf)
                .Append("       eval('window.opener.document.all.'+'" & ViewState("TxtdTorikesiCd") & "').innerText='';" & vbCrLf)
                .Append("   }" & vbCrLf)
                .Append("   else{" & vbCrLf)
                .Append("       eval('window.opener.document.all.'+'" & ViewState("TxtdTorikesiCd") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(4).ClientID & "').innerText + ':' + eval('document.all.'+'" & grdBody.Rows(0).Cells(5).ClientID & "').innerText;" & vbCrLf)
                .Append("   }" & vbCrLf)
                .Append(" }" & vbCrLf)
                .Append("if ('" & ViewState("btnChangeColorCd") & "'!=''){" & vbCrLf)
                .Append("eval('window.opener.document.all.'+'" & ViewState("btnChangeColorCd") & "').click();" & vbCrLf)
                .Append(" }" & vbCrLf)
                '==================2012/03/29 �ԗ� 405721�Č��̑Ή� �ǉ���==========================

            Else
                .Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(0).ClientID & "').innerText;" & vbCrLf)
                .Append("eval('window.opener.document.all.'+'" & ViewState("objCd") & "').select();" & vbCrLf)
                .Append("if ('" & ViewState("objMei") & "'!=''){" & vbCrLf)
                .Append("eval('window.opener.document.all.'+'" & ViewState("objMei") & "').innerText=eval('document.all.'+'" & grdBody.Rows(0).Cells(1).ClientID & "').innerText;" & vbCrLf)
                .Append(" }" & vbCrLf)
            End If
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
            If ViewState("submit") = True Then
                .Append("eval('window.opener.'+'" & ViewState("strFormName") & "'+'.'+'" & ViewState("objBtnKbn") & "').value='0';" & vbCrLf)
                .Append("window.opener.document.forms[0].submit();" & vbCrLf)
            End If
            If Not ViewState("hdnId").ToString.Trim.Equals(String.Empty) Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("hdnId") & "').value=eval('document.all.'+'" & grdBody.Rows(0).Cells(0).ClientID & "').innerText;" & vbCrLf)
            End If
            If Not ViewState("objRetrun").ToString.Trim.Equals(String.Empty) Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("objRetrun") & "').value='1';" & vbCrLf)
            End If
            '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================

            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================
            If Not ViewState("btnFcId").ToString.Trim.Equals(String.Empty) Then
                .Append("eval('window.opener.document.all.'+'" & ViewState("btnFcId") & "').click();" & vbCrLf)
            End If
            '==========2012/04/13 �ԗ� 405738�Č��̑Ή� �ǉ���==================

            .Append("window.close();" & vbCrLf)
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
            setColWidth(ViewState("strKbn"), intWidth, "head")
            grdViewStyle(intWidth, grdHead, dt, True)
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button.Click
        Dim intWidth(0) As String
        setColWidth(ViewState("strKbn"), intWidth, "head")
        grdViewStyle(intWidth, grdHead, GetMeisai(True), True)
    End Sub
End Class