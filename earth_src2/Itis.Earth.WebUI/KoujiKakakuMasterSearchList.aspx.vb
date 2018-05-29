Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Partial Public Class KoujiKakakuMaster
    Inherits System.Web.UI.Page
    Private hanbaiKakakuSearchListLogic As New HanbaiKakakuMasterLogic
    Private commonCheck As New CommonCheck
    Protected scrollHeight As Integer = 0
    Protected setFlg As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strUserID As String = ""
        '�����`�F�b�N
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X
        Dim user_info As New LoginUserInfo
        jBn.userAuth(user_info)
        If user_info Is Nothing Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Context.Server.Transfer("./CommonErr.aspx")
        End If
        'javascript�쐬
        MakeJavascript()

        If Not IsPostBack Then
            '�Q�Ɨ����Ǘ��e�[�u����o�^����B
            CommonCheck.SetURL(Me, strUserID)

            If Not Request("sendSearchTerms") Is Nothing Then
                '������
                Call SetInitData(CStr(Request("sendSearchTerms")))
            Else
                '������
                Call SetInitData(String.Empty)
            End If
        Else
            '��������(�����R�[�h)��ݒ肷��
            If Me.ddlAiteSakiSyubetu.SelectedValue <> "0" AndAlso Me.ddlAiteSakiSyubetu.SelectedValue <> String.Empty Then
                Me.divAitesaki.Attributes.Add("style", "display:block;")
            Else
                Me.divAitesaki.Attributes.Add("style", "display:none;")
            End If
            'CSV�o�̓{�^������������ꍇ
            If Me.hidCsvOut.Value = "1" Then
                'CSV�o��
                Call CsvOutPut()
            End If
        End If
        setFlg = GetKojKengen(user_info)
        'CSV�捞�{�^����ݒ肷��
        Me.btnCsvUpload.Enabled = setFlg

        '��V�K�o�^��{�^����ݒ肷��
        Me.btnTouroku.Enabled = setFlg
        '�����R�[�h(FROM)���t�H�[�J�X�������ꍇ�A�啶���ɕϊ�����
        Me.tbxAiteSakiCdFrom.Attributes.Add("onBlur", "fncToUpper(this);")
        '�����R�[�h(TO)���t�H�[�J�X�������ꍇ�A�啶���ɕϊ�����
        Me.tbxAiteSakiCdTo.Attributes.Add("onBlur", "fncToUpper(this);")

        Me.tbxKojKaisyaCd.Attributes.Add("onBlur", "fncToUpper(this);")
        '����於(FROM)�����͕s��ݒ肷��
        Me.tbxAiteSakiMeiFrom.Attributes.Add("ReadOnly", "True")
        '����於(To)�����͕s��ݒ肷��
        Me.tbxAiteSakiMeiTo.Attributes.Add("ReadOnly", "True")
        '�H����Ж������͕s��ݒ肷��
        Me.tbxKojKaisyaMei.Attributes.Add("ReadOnly", "True")
        '�����{�^������������ꍇ�A�K�{�`�F�b�N
        Me.btnKensaku.Attributes.Add("onClick", "if(fncNyuuryokuCheck('1')){fncShowModal();}else{return false;}")
        'Csv�o�̓{�^������������ꍇ�A�K�{�`�F�b�N
        Me.btnCsvOutput.Attributes.Add("onClick", "if(fncNyuuryokuCheck('2')){fncShowModal();}else{return false;}")
        '������ʂ��ύX����ꍇ�A�����R�[�h����������ݒ肷��
        Me.ddlAiteSakiSyubetu.Attributes.Add("onChange", "return fncSetAitesaki();")
        '�N���A�{�^������
        Me.btnClear.Attributes.Add("onClick", "return fncClear();")
        '�u����v�{�^������
        Me.btnClose.Attributes.Add("onClick", "return fncClose();")
        '�uCSV�捞�v�{�^������������ꍇ�A�̔����i�捞�|�b�v�A�b�v��ʂ��N������
        Me.btnCsvUpload.Attributes.Add("onClick", "return fncCsvUpload();")


        '��ʏc�X�N���[����ݒ肷��
        scrollHeight = ViewState("scrollHeight")

        '��V�K�o�^��{�^��
        If setFlg Then
            Me.btnTouroku.Attributes.Add("onClick", "fncPopupKobetuSettei('','','','');return false;")
        End If
    End Sub
    ''' <summary>��ʈꗗ�w�b�_�[�ɕ��я����N���b�N��</summary>
    Private Sub btnSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAitesakiSyubetuUp.Click, _
                                                                                           btnAitesakiSyubetuDown.Click, _
                                                                                           btnAitesakiCdUp.Click, _
                                                                                           btnAitesakiCdDown.Click, _
                                                                                           btnAitesakiMeiUp.Click, _
                                                                                           btnAitesakiMeiDown.Click, _
                                                                                           btnSyouhinCdUp.Click, _
                                                                                           btnSyouhinCdDown.Click, _
                                                                                           btntSyouhinMeiUp.Click, _
                                                                                           btntSyouhinMeiDown.Click, _
                                                                                           btnUriGakuUp.Click, _
                                                                                           btnUriGakuDown.Click, _
                                                                                           btnTorikesiUp.Click, _
                                                                                           btnTorikesiDown.Click, _
                                                                                           btnKojUmuUp.Click, _
                                                                                           btnKojUmuDown.Click, _
                                                                                           btnSeikyuUmuUp.Click, _
                                                                                           btnSeikyuUmuDown.Click, _
                                                                                            btnKojCdUp.Click, _
                                                                                            btnKojCdDown.Click, _
                                                                                            btnKojKaisyaUp.Click, _
                                                                                            btnKojKaisyaDown.Click

        Dim strSort As String = String.Empty
        Dim strUpDown As String = String.Empty

        '�\�[�g���{�^���F��ݒ肷��
        Call SetSortButtonColor()

        '��ʂɃ\�[�g�����N���b�N��
        Select Case CType(sender, LinkButton).ID
            Case btnAitesakiSyubetuUp.ID                '������ʁ�
                strSort = "aitesaki"
                strUpDown = "ASC"
                btnAitesakiSyubetuUp.ForeColor = Drawing.Color.IndianRed
            Case btnAitesakiSyubetuDown.ID              '������ʁ�
                strSort = "aitesaki"
                strUpDown = "DESC"
                btnAitesakiSyubetuDown.ForeColor = Drawing.Color.IndianRed
            Case btnAitesakiCdUp.ID                     '�����R�[�h��
                strSort = "aitesaki_cd"
                strUpDown = "ASC"
                btnAitesakiCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnAitesakiCdDown.ID                   '�����R�[�h��
                strSort = "aitesaki_cd"
                strUpDown = "DESC"
                btnAitesakiCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnAitesakiMeiUp.ID                    '����於��
                strSort = "aitesaki_mei"
                strUpDown = "ASC"
                btnAitesakiMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnAitesakiMeiDown.ID                  '����於��
                strSort = "aitesaki_mei"
                strUpDown = "DESC"
                btnAitesakiMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSyouhinCdUp.ID                      '���i�R�[�h��
                strSort = "syouhin_cd"
                strUpDown = "ASC"
                btnSyouhinCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnSyouhinCdDown.ID                    '���i�R�[�h��
                strSort = "syouhin_cd"
                strUpDown = "DESC"
                btnSyouhinCdDown.ForeColor = Drawing.Color.IndianRed
            Case btntSyouhinMeiUp.ID                    '���i����
                strSort = "syouhin_mei"
                strUpDown = "ASC"
                btntSyouhinMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btntSyouhinMeiDown.ID                  '���i����
                strSort = "syouhin_mei"
                strUpDown = "DESC"
                btntSyouhinMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnKojCdUp.ID                      '�H�R�[�h��
                strSort = "koj_cd"
                strUpDown = "ASC"
                btnKojCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnKojCdDown.ID                    '�H�R�[�h��
                strSort = "koj_cd"
                strUpDown = "DESC"
                btnKojCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnKojKaisyaUp.ID                       '�H����Ж���
                strSort = "tys_kaisya_mei"
                strUpDown = "ASC"
                btnKojKaisyaUp.ForeColor = Drawing.Color.IndianRed
            Case btnKojKaisyaDown.ID                     '�H����Ж���
                strSort = "tys_kaisya_mei"
                strUpDown = "DESC"
                btnKojKaisyaDown.ForeColor = Drawing.Color.IndianRed
            Case btnTorikesiUp.ID                       '�����
                strSort = "torikesi"
                strUpDown = "ASC"
                btnTorikesiUp.ForeColor = Drawing.Color.IndianRed
            Case btnTorikesiDown.ID                     '�����
                strSort = "torikesi"
                strUpDown = "DESC"
                btnTorikesiDown.ForeColor = Drawing.Color.IndianRed
            Case btnUriGakuUp.ID            '������z(�Ŕ�)��
                strSort = "uri_gaku"
                strUpDown = "ASC"
                btnUriGakuUp.ForeColor = Drawing.Color.IndianRed
            Case btnUriGakuDown.ID          '������z(�Ŕ�)��
                strSort = "uri_gaku"
                strUpDown = "DESC"
                btnUriGakuDown.ForeColor = Drawing.Color.IndianRed
            Case btnKojUmuUp.ID   '�H����А����L����
                strSort = "kojumu"
                strUpDown = "ASC"
                btnKojUmuUp.ForeColor = Drawing.Color.IndianRed
            Case btnKojUmuDown.ID '�H����А����L����
                strSort = "kojumu"
                strUpDown = "DESC"
                btnKojUmuDown.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuUmuUp.ID                '�����L����
                strSort = "seikyuumu"
                strUpDown = "ASC"
                btnSeikyuUmuUp.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuUmuDown.ID              '�����L����
                strSort = "seikyuumu"
                strUpDown = "DESC"
                btnSeikyuUmuDown.ForeColor = Drawing.Color.IndianRed

        End Select

        '��ʃf�[�^�̃\�[�g����ݒ肷��
        Dim dt As DataTable = CType(ViewState("dtKojKakakuInfo"), Data.DataTable)

        Dim dvKojKakakuInfo As Data.DataView = dt.DefaultView
        dvKojKakakuInfo.Sort = strSort & " " & strUpDown

        Me.grdBodyLeft.DataSource = dvKojKakakuInfo
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = dvKojKakakuInfo
        Me.grdBodyRight.DataBind()

        '�������ݒ肷��
        Call SetKingaku()

        '��ʏc�X�N���[����ݒ肷��
        scrollHeight = ViewState("scrollHeight")
        '��ʉ��X�N���[���ʒu��ݒ肷��
        SetScroll()

    End Sub
    ''' <summary>���̓`�F�b�N</summary>
    ''' <param name="strObjId">�N���C�A���gID</param>
    ''' <returns>�G���[���b�Z�[�W</returns>
    Public Function CheckInput(ByRef strObjId As String) As String
        Dim csScript As New StringBuilder
        With csScript
            '�����R�[�h(From)(�p�����`�F�b�N)
            If Me.tbxAiteSakiCdFrom.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxAiteSakiCdFrom.Text, "�����R�[�h(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxAiteSakiCdFrom.ClientID
                End If
            End If
            '�����R�[�h(To)(�p�����`�F�b�N)
            If Me.tbxAiteSakiCdTo.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxAiteSakiCdTo.Text, "�����R�[�h(To)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxAiteSakiCdTo.ClientID
                End If
            End If
        End With
        Return csScript.ToString
    End Function
    ''' <summary>�G���[���b�Z�[�W�\��</summary>
    ''' <param name="strMessage">�G���[���b�Z�[�W</param>
    ''' <param name="strObjId">�N���C�A���gID</param>
    Private Sub ShowMessage(ByVal strMessage As String, ByVal strObjId As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("	alert('" & strMessage & "');")
            If strObjId <> String.Empty Then
                .AppendLine("   document.getElementById('" & strObjId & "').focus();")
                .AppendLine("   document.getElementById('" & strObjId & "').select();")
            End If
            .AppendLine("}")
        End With
        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
    End Sub
    ''' <summary>�������ʌ�����ݒ肷��</summary>
    ''' <param name="intCount">�������ʌ���</param>
    Private Sub SetKensakuCount(ByVal intCount As Integer)

        If Me.ddlKensakuCount.SelectedValue = "max" Then
            Me.lblCount.Text = intCount
            Me.lblCount.ForeColor = Drawing.Color.Black
            scrollHeight = intCount * 32 + 1
        Else
            If intCount > Me.ddlKensakuCount.SelectedValue Then
                Me.lblCount.Text = Me.ddlKensakuCount.SelectedValue & " / " & intCount
                Me.lblCount.ForeColor = Drawing.Color.Red
                scrollHeight = Me.ddlKensakuCount.SelectedValue * 32 + 1
            Else
                Me.lblCount.Text = intCount
                Me.lblCount.ForeColor = Drawing.Color.Black
                scrollHeight = intCount * 32 + 1
            End If
        End If

    End Sub
    ''' <summary>�\�[�g���{�^����ݒ肷��</summary>
    ''' <param name="blnFlg">�\���敪</param>
    Private Sub SetSortButton(ByVal blnFlg As Boolean)

        Me.btnAitesakiSyubetuUp.Visible = blnFlg                 '������ʁ�
        Me.btnAitesakiSyubetuDown.Visible = blnFlg               '������ʁ�
        Me.btnAitesakiCdUp.Visible = blnFlg                      '�����R�[�h��
        Me.btnAitesakiCdDown.Visible = blnFlg                    '�����R�[�h��
        Me.btnAitesakiMeiUp.Visible = blnFlg                     '����於��
        Me.btnAitesakiMeiDown.Visible = blnFlg                   '����於��
        Me.btnSyouhinCdUp.Visible = blnFlg                       '���i�R�[�h��
        Me.btnSyouhinCdDown.Visible = blnFlg                     '���i�R�[�h��
        Me.btntSyouhinMeiUp.Visible = blnFlg                     '���i����
        Me.btntSyouhinMeiDown.Visible = blnFlg                   '���i����
        Me.btnTorikesiUp.Visible = blnFlg                        '�����
        Me.btnTorikesiDown.Visible = blnFlg                      '�����
        Me.btnUriGakuUp.Visible = blnFlg                        ' ������z(�Ŕ�)��
        Me.btnUriGakuDown.Visible = blnFlg                      ' ������z(�Ŕ�)��
        Me.btnKojUmuUp.Visible = blnFlg                         '�H����А����L����
        Me.btnKojUmuDown.Visible = blnFlg                       '�H����А����L����
        Me.btnSeikyuUmuUp.Visible = blnFlg                      '�����L����
        Me.btnSeikyuUmuDown.Visible = blnFlg                    '�����L����
        Me.btnKojCdUp.Visible = blnFlg                          '�H�R�[�h��
        Me.btnKojCdDown.Visible = blnFlg                        '�H�R�[�h��
        Me.btnKojKaisyaUp.Visible = blnFlg                       '�H����Ж���
        Me.btnKojKaisyaDown.Visible = blnFlg                     '�H����Ж���

    End Sub
    ''' <summary>�\�[�g���{�^���F��ݒ肷��</summary>
    Private Sub SetSortButtonColor()

        Me.btnAitesakiSyubetuUp.ForeColor = Drawing.Color.SkyBlue                   '������ʁ�
        Me.btnAitesakiSyubetuDown.ForeColor = Drawing.Color.SkyBlue                 '������ʁ�
        Me.btnAitesakiCdUp.ForeColor = Drawing.Color.SkyBlue                        '�����R�[�h��
        Me.btnAitesakiCdDown.ForeColor = Drawing.Color.SkyBlue                      '�����R�[�h��
        Me.btnAitesakiMeiUp.ForeColor = Drawing.Color.SkyBlue                       '����於��
        Me.btnAitesakiMeiDown.ForeColor = Drawing.Color.SkyBlue                     '����於��
        Me.btnSyouhinCdUp.ForeColor = Drawing.Color.SkyBlue                         '���i�R�[�h��
        Me.btnSyouhinCdDown.ForeColor = Drawing.Color.SkyBlue                       '���i�R�[�h��
        Me.btntSyouhinMeiUp.ForeColor = Drawing.Color.SkyBlue                       '���i����
        Me.btntSyouhinMeiDown.ForeColor = Drawing.Color.SkyBlue                     '���i����
        Me.btnTorikesiUp.ForeColor = Drawing.Color.SkyBlue                          '�����
        Me.btnTorikesiDown.ForeColor = Drawing.Color.SkyBlue                        '�����
        Me.btnUriGakuUp.ForeColor = Drawing.Color.SkyBlue                           ' ������z(�Ŕ�)��
        Me.btnUriGakuDown.ForeColor = Drawing.Color.SkyBlue                         ' ������z(�Ŕ�)��
        Me.btnKojUmuUp.ForeColor = Drawing.Color.SkyBlue                            '�H����А����L����
        Me.btnKojUmuDown.ForeColor = Drawing.Color.SkyBlue                          '�H����А����L����
        Me.btnSeikyuUmuUp.ForeColor = Drawing.Color.SkyBlue                         '�����L����
        Me.btnSeikyuUmuDown.ForeColor = Drawing.Color.SkyBlue                       '�����L����
        Me.btnKojCdUp.ForeColor = Drawing.Color.SkyBlue                             '�H�R�[�h��
        Me.btnKojCdDown.ForeColor = Drawing.Color.SkyBlue                           '�H�R�[�h��
        Me.btnKojKaisyaUp.ForeColor = Drawing.Color.SkyBlue                         '�H����Ж���
        Me.btnKojKaisyaDown.ForeColor = Drawing.Color.SkyBlue                       '�H����Ж���

    End Sub
    ''' <summary>������ݒ�</summary>
    Public Sub SetKingaku()

        Dim rowCount As Integer

        For rowCount = 0 To Me.grdBodyRight.Rows.Count - 1
            '������z(�Ŕ�)��ݒ肷��
            If CType(Me.grdBodyRight.Rows(rowCount).Cells(3).Controls(1), Label).Text.ToString.Trim.Equals(String.Empty) Then
                CType(Me.grdBodyRight.Rows(rowCount).Cells(3).Controls(1), Label).Text = ""
            Else
                CType(Me.grdBodyRight.Rows(rowCount).Cells(3).Controls(1), Label).Text = FormatNumber(CType(Me.grdBodyRight.Rows(rowCount).Cells(3).Controls(1), Label).Text, 0)
            End If

        Next

    End Sub
    ''' <summary>�������s�{�^�����N���b�N��</summary>
    Private Sub btnKensaku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensaku.Click

        '���̓`�F�b�N
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        Dim intCount As Integer     '��������
        '����������ݒ肷��
        Dim dtParam As Data.DataTable = SetKojKakaku()
        '�����f�[�^���擾����
        Dim dtKojKakakuInfo As New Data.DataTable

        Dim KojKakakuSearchListLogic As New KojKakakuMasterLogic

        '�����������擾����
        intCount = KojKakakuSearchListLogic.GetKojKakakuInfoCount(dtParam)
        '�����f�[�^���擾����
        dtKojKakakuInfo = KojKakakuSearchListLogic.GetKojKakakuInfo(dtParam)


        '�������ʂ�ݒ肷��
        Me.grdBodyLeft.DataSource = dtKojKakakuInfo
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = dtKojKakakuInfo
        Me.grdBodyRight.DataBind()

        '����於��ݒ肷��
        Call SetAitesakiMei(Me.ddlAiteSakiSyubetu.SelectedValue, _
                    dtParam.Rows(0).Item("aitesaki_cd_from"), _
                    dtParam.Rows(0).Item("aitesaki_cd_to"), _
                    dtParam.Rows(0).Item("torikesi_aitesaki"))
        Call SetKojKaisya(Me.tbxKojKaisyaCd.Text)
        '�������ʌ�����ݒ肷��
        Call SetKensakuCount(intCount)

        If intCount = 0 Then
            '�\�[�g���{�^����ݒ肷��
            Call SetSortButton(False)
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
            Exit Sub
        Else
            '�\�[�g���{�^����ݒ肷��
            Call SetSortButton(True)
            '�\�[�g���{�^���F��ݒ肷��
            Call SetSortButtonColor()
            '�������ݒ肷��
            Call SetKingaku()
            ViewState("dtKojKakakuInfo") = dtKojKakakuInfo
            ViewState("scrollHeight") = scrollHeight
        End If

    End Sub
    ''' <summary>��ʂɓ��͂����l���f�[�^�e�[�u���ɐݒ肷��</summary>
    ''' <returns>��ʃf�[�^����������p�����[�^�f�[�^�e�[�u��</returns>
    Private Function SetKojKakaku() As DataTable

        Dim dtParam As New DataTable

        dtParam.Columns.Add(New DataColumn("aitesaki_syubetu", GetType(String)))
        dtParam.Columns.Add(New DataColumn("aitesaki_cd_from", GetType(String)))
        dtParam.Columns.Add(New DataColumn("aitesaki_cd_to", GetType(String)))
        dtParam.Columns.Add(New DataColumn("syouhin_cd", GetType(String)))
        dtParam.Columns.Add(New DataColumn("kojkaisya_cd", GetType(String)))

        dtParam.Columns.Add(New DataColumn("torikesi", GetType(String)))
        dtParam.Columns.Add(New DataColumn("torikesi_aitesaki", GetType(String)))
        dtParam.Columns.Add(New DataColumn("kensaku_count", GetType(String)))
        Dim row As DataRow = dtParam.NewRow
        '������ʂ�ݒ肷��
        row.Item("aitesaki_syubetu") = Me.ddlAiteSakiSyubetu.SelectedValue
        '�����R�[�hFROM��ݒ肷��
        row.Item("aitesaki_cd_from") = Me.tbxAiteSakiCdFrom.Text
        '�����R�[�hTO��ݒ肷��
        row.Item("aitesaki_cd_to") = Me.tbxAiteSakiCdTo.Text
        '���i�R�[�h��ݒ肷��
        row.Item("syouhin_cd") = Me.ddlSyouhinCd.SelectedValue
        '�H�����
        row.Item("kojkaisya_cd") = Me.tbxKojKaisyaCd.Text
        '�����ݒ肷��
        row.Item("torikesi") = IIf(Me.chkKensakuTaisyouGai.Checked, "1", String.Empty)
        '���������ݒ肷��
        row.Item("torikesi_aitesaki") = IIf(Me.chkAitesakiTaisyouGai.Checked, "1", String.Empty)
        '�������������ݒ肷��
        row.Item("kensaku_count") = Me.ddlKensakuCount.SelectedValue
        dtParam.Rows.Add(row)

        Return dtParam
    End Function
    '''' <summary>CSV�o��</summary>
    Private Sub CsvOutPut()
        '����������ݒ肷��
        Dim dtParam As Data.DataTable = SetKojKakaku()
        Dim dtKojKakakuCsvInfo As New DataTable
        Dim KojKakakuSearchListLogic As New KojKakakuMasterLogic
        '�̔����iCSV�f�[�^���擾����
        dtKojKakakuCsvInfo = KojKakakuSearchListLogic.GetKojKakakuCSVInfo(dtParam)

        'CSV�t�@�C�����ݒ�
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("KoujiKakakuMasterCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSV�t�@�C���w�b�_�ݒ�
        writer.WriteLine(EarthConst.conKojKakakuCsvHeader)

        'CSV�t�@�C�����e�ݒ�
        For Each row As Data.DataRow In dtKojKakakuCsvInfo.Rows
            writer.WriteLine(row.Item("edi_jouhou_sakusei_date"), _
                                row.Item("aitesaki_syubetu"), _
                                row.Item("aitesaki_cd"), _
                                row.Item("aitesaki_mei"), _
                                row.Item("syouhin_cd"), _
                                row.Item("syouhin_mei"), _
                                row.Item("koj_gaisya_cd") & row.Item("koj_gaisya_jigyousyo_cd"), _
                                row.Item("tys_kaisya_mei"), _
                                row.Item("torikesi"), _
                                row.Item("uri_gaku"), _
                                row.Item("koj_gaisya_seikyuu_umu"), _
                                row.Item("seikyuu_umu"))
        Next

        'CSV�t�@�C���_�E�����[�h
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()

    End Sub
    ''' <summary>�����f�[�^���Z�b�g</summary>
    ''' <param name="sendSearchTerms">�p�����[�^</param>
    Protected Sub SetInitData(ByVal sendSearchTerms As String)

        If sendSearchTerms <> String.Empty Then
            Dim arrSearchTerm() As String = Split(sendSearchTerms, "$$$")
            '������ʂ�ݒ肷��
            SetAitesakiSyubetu(arrSearchTerm(0))
            If Me.ddlAiteSakiSyubetu.SelectedValue <> String.Empty AndAlso Me.ddlAiteSakiSyubetu.SelectedValue <> "0" Then
                '�����R�[�hFROM��ݒ肷��
                If arrSearchTerm.Length > 1 Then
                    Me.tbxAiteSakiCdFrom.Text = arrSearchTerm(1)
                    Me.tbxAiteSakiCdTo.Text = arrSearchTerm(1)
                    Call SetAitesakiMei(Me.ddlAiteSakiSyubetu.SelectedValue, arrSearchTerm(1), arrSearchTerm(1), "1")
                Else
                    Me.tbxAiteSakiCdFrom.Text = String.Empty
                    Me.tbxAiteSakiCdTo.Text = String.Empty
                End If
               
                Me.divAitesaki.Attributes.Add("style", "display:block;")
            Else
                '�����R�[�hFROM��ݒ肷��
                Me.tbxAiteSakiCdFrom.Text = String.Empty
                '����於FROM��ݒ肷��
                Me.tbxAiteSakiMeiFrom.Text = String.Empty
                '�����R�[�hTO��ݒ肷��
                Me.tbxAiteSakiCdTo.Text = String.Empty
                '����於TO��ݒ肷��
                Me.tbxAiteSakiMeiTo.Text = String.Empty
                Me.divAitesaki.Attributes.Add("style", "display:none;")
            End If
            '���i�R�[�h��ݒ肷��
            If arrSearchTerm.Length > 2 Then
                Call SetSyouhin(arrSearchTerm(2))
            Else
                Call SetSyouhin(String.Empty)
            End If
            '���i�R�[�h��ݒ肷��
            If arrSearchTerm.Length > 3 Then
                Call SetKojKaisya(arrSearchTerm(3))
            Else
                Call SetKojKaisya(String.Empty)
            End If

        Else
            '������ʂ�ݒ肷��
            Call SetAitesakiSyubetu(String.Empty)
            '�����R�[�hFROM��ݒ肷��
            Me.tbxAiteSakiCdFrom.Text = String.Empty
            '����於FROM��ݒ肷��
            Me.tbxAiteSakiMeiFrom.Text = String.Empty
            '�����R�[�hTO��ݒ肷��
            Me.tbxAiteSakiCdTo.Text = String.Empty
            '����於TO��ݒ肷��
            Me.tbxAiteSakiMeiTo.Text = String.Empty
            Me.divAitesaki.Attributes.Add("style", "display:none;")
            '���i�R�[�h��ݒ肷��
            Call SetSyouhin(String.Empty)
            '�H�����
            Me.tbxKojKaisyaCd.Text = String.Empty
            Me.tbxKojKaisyaMei.Text = String.Empty
        End If
        '�ꗗ�f�[�^��ݒ肷��
        Me.grdBodyLeft.DataSource = Nothing
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = Nothing
        Me.grdBodyRight.DataBind()

    End Sub
    ''' <summary> ������ʃ��X�g��ݒ肷��</summary>
    ''' <param name="strAitesakiSyubetu">�������</param>
    Sub SetAitesakiSyubetu(ByVal strAitesakiSyubetu As String)

        Dim dtAiteSakiSyubetu As Data.DataTable
        '������ʃf�[�^���擾����
        dtAiteSakiSyubetu = hanbaiKakakuSearchListLogic.GetAiteSakiSyubetu()
        '�f�[�^��ݒ肷��
        Me.ddlAiteSakiSyubetu.DataTextField = "aitesaki_syubetu"
        Me.ddlAiteSakiSyubetu.DataValueField = "code"
        Me.ddlAiteSakiSyubetu.DataSource = dtAiteSakiSyubetu
        Me.ddlAiteSakiSyubetu.DataBind()
        Me.ddlAiteSakiSyubetu.Items.Insert(0, New ListItem("", ""))
        If strAitesakiSyubetu = "1" Then
            Me.divSitei.Style.Add("display", "block")
            Me.tbxAiteSakiCdTo.Text = String.Empty
            Me.tbxAiteSakiMeiTo.Text = String.Empty
        End If
        '�I�������l��ݒ肷��
        If strAitesakiSyubetu <> String.Empty Then
            Dim intcount
            For intcount = 0 To Me.ddlAiteSakiSyubetu.Items.Count - 1
                If Me.ddlAiteSakiSyubetu.Items(intcount).Value = strAitesakiSyubetu Then
                    Me.ddlAiteSakiSyubetu.SelectedIndex = intcount
                    Exit For
                End If
            Next
        End If

    End Sub
    ''' <summary> �H����Ђ�ݒ肷��</summary>
    ''' <param name="strSetKojKaisyaCd">�H����ЃR�[�h</param>
    Sub SetKojKaisya(ByVal strSetKojKaisyaCd As String)
        Dim KojKakuLogic As New KojKakakuMasterLogic
        Dim dtSearchTable As New DataTable

        If strSetKojKaisyaCd = "ALLAL" Then
            Me.tbxKojKaisyaMei.Text = "�w�薳��"
        ElseIf strSetKojKaisyaCd = "" Then
            Me.tbxKojKaisyaMei.Text = ""
        Else
            dtSearchTable = KojKakuLogic.GetKojKaisyaKensaku(strSetKojKaisyaCd)
            If dtSearchTable.Rows.Count > 0 Then
                Me.tbxKojKaisyaMei.Text = dtSearchTable.Rows(0).Item("tys_kaisya_mei")
            End If
        End If

        Me.tbxKojKaisyaCd.Text = strSetKojKaisyaCd
        
    End Sub
    ''' <summary> ���i�R�[�h���X�g��ݒ肷��</summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    Sub SetSyouhin(ByVal strSyouhinCd As String)

        Dim dtSyouhin As Data.DataTable
        '������ʃf�[�^���擾����
        Dim KojKakakuSearchListLogic As New KojKakakuMasterLogic
        dtSyouhin = KojKakakuSearchListLogic.GetSyouhin()
        '�f�[�^��ݒ肷��
        Me.ddlSyouhinCd.DataTextField = "syouhin"
        Me.ddlSyouhinCd.DataValueField = "syouhin_cd"
        Me.ddlSyouhinCd.DataSource = dtSyouhin
        Me.ddlSyouhinCd.DataBind()
        Me.ddlSyouhinCd.Items.Insert(0, New ListItem("", ""))
        '�I�������l��ݒ肷��
        If strSyouhinCd <> String.Empty Then
            Dim intcount
            For intcount = 0 To Me.ddlSyouhinCd.Items.Count - 1
                If Me.ddlSyouhinCd.Items(intcount).Value = strSyouhinCd Then
                    Me.ddlSyouhinCd.SelectedIndex = intcount
                    Exit For
                End If
            Next
        End If

    End Sub
    ''' <summary>����於��ݒ肷��</summary>
    ''' <param name="strAitesakiSyubetu">�������</param>
    ''' <param name="strAitesakiCdFrom">�����R�[�hFROM</param>
    ''' <param name="strAitesakiCdTo">�����R�[�hTO</param>
    ''' <param name="strTorikesiAitesaki">��������敪</param>
    Private Sub SetAitesakiMei(ByVal strAitesakiSyubetu As String, _
                               ByVal strAitesakiCdFrom As String, _
                               ByVal strAitesakiCdTo As String, _
                               ByVal strTorikesiAitesaki As String)

        Dim commonSearchLogic As New CommonSearchLogic
        '�����R�[�hFrom��ݒ肷��
        If Me.tbxAiteSakiCdFrom.Text <> String.Empty Then
            Dim dtAiteSakiTable As Data.DataTable = hanbaiKakakuSearchListLogic.GetAiteSaki(strAitesakiSyubetu, _
                                                                                            strTorikesiAitesaki, _
                                                                                            strAitesakiCdFrom)
            If dtAiteSakiTable.Rows.Count > 0 Then
                Me.tbxAiteSakiCdFrom.Text = dtAiteSakiTable.Rows(0).Item("cd").ToString
                Me.tbxAiteSakiMeiFrom.Text = dtAiteSakiTable.Rows(0).Item("mei").ToString
            Else
                Me.tbxAiteSakiMeiFrom.Text = String.Empty
            End If
        Else
            Me.tbxAiteSakiMeiFrom.Text = String.Empty
        End If

        '�����R�[�hTo��ݒ肷��
        If Me.tbxAiteSakiCdTo.Text <> String.Empty Then

            Dim dtAiteSakiTable As Data.DataTable = hanbaiKakakuSearchListLogic.GetAiteSaki(strAitesakiSyubetu, _
                                                                                            strTorikesiAitesaki, _
                                                                                            strAitesakiCdTo)
            If dtAiteSakiTable.Rows.Count > 0 Then
                Me.tbxAiteSakiCdTo.Text = dtAiteSakiTable.Rows(0).Item("cd").ToString
                Me.tbxAiteSakiMeiTo.Text = dtAiteSakiTable.Rows(0).Item("mei").ToString
            Else
                Me.tbxAiteSakiMeiTo.Text = String.Empty
            End If
        Else
            Me.tbxAiteSakiMeiTo.Text = String.Empty
        End If
    End Sub
    ''' <summary>Javascript�쐬</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            '�X�N���[����ݒ肷��
            .AppendLine("function wheel(event){")
            .AppendLine("   var delta = 0;")
            .AppendLine("   if(!event)")
            .AppendLine("       event = window.event;")
            .AppendLine("   if(event.wheelDelta){")
            .AppendLine("       delta = event.wheelDelta/120;")
            .AppendLine("       if(window.opera)")
            .AppendLine("           delta = -delta;")
            .AppendLine("       }else if(event.detail){")
            .AppendLine("           delta = -event.detail/3;")
            .AppendLine("       }")
            .AppendLine("   if(delta)")
            .AppendLine("       handle(delta);")
            .AppendLine("}")
            .AppendLine("function handle(delta){")
            .AppendLine("   var divVscroll=" & divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   if (delta < 0){")
            .AppendLine("       divVscroll.scrollTop = divVscroll.scrollTop + 15;")
            .AppendLine("   }else{")
            .AppendLine("       divVscroll.scrollTop = divVscroll.scrollTop - 15;")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("function fncScrollV(){")
            .AppendLine("   var divbodyleft=" & Me.divBodyLeft.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   divbodyleft.scrollTop = divVscroll.scrollTop;")
            .AppendLine("   divbodyright.scrollTop = divVscroll.scrollTop;")
            .AppendLine("}")
            .AppendLine("function fncScrollH(){")
            .AppendLine("   var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("   var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("   divheadright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("   divbodyright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("}")
            .AppendLine("function fncSetScroll(){")
            .AppendLine("   var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("   document.getElementById('" & Me.hidScroll.ClientID & "').value = divHscroll.scrollLeft;")
            .AppendLine("}")
            '�������s�ACSV�o�̓{�^������������ꍇ�A���̓`�F�b�N
            .AppendLine("function fncNyuuryokuCheck(strKbn){")
            'CSV�o�͋敪��ݒ肷��
            .AppendLine("   fncSetCsvOut();")
            '������ʂ��K�{���̓`�F�b�N
            .AppendLine("   if (document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".selectedIndex=='0'){")
            .AppendLine("       alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "�������") & "');")
            .AppendLine("       document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '�u�����R�[�hFROM�v�A�u�����R�[�hTO�v�̂��Âꂩ�͓��͕K�{
            .AppendLine("   if (document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value!='0'){")
            .AppendLine("       if(fncNyuuryokuHissu(document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".value)==false && fncNyuuryokuHissu(document.all." & Me.tbxAiteSakiCdTo.ClientID & ".value)==false){")
            .AppendLine("           alert('" & Messages.Instance.MSG041E.Replace("@PARAM1", "�����R�[�hFROM").Replace("@PARAM2", "�����R�[�hTO") & "');")
            .AppendLine("           document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".focus();")
            .AppendLine("           return false; ")
            .AppendLine("       }")
            .AppendLine("   }")
            '�m�F���b�Z�[�W�\��
            .AppendLine("   if(strKbn=='1'){")
            .AppendLine("       if (document.all." & Me.ddlKensakuCount.ClientID & ".value=='max'){")
            .AppendLine("          if(confirm('" & Messages.Instance.MSG007C & "')){")
            .AppendLine("               document.forms[0].submit();")
            .AppendLine("           }else{")
            .AppendLine("              return false; ")
            .AppendLine("         }")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("return true;")
            .AppendLine("}")
            '���͕K�{�`�F�b�N:�����́A�X�y�[�X�݂̂Ȃ�G���[�\��
            .AppendLine("function fncNyuuryokuHissu(strValue) {")
            .AppendLine("   var wkflg = 0;")
            .AppendLine("   var wkdata = strValue;")
            .AppendLine("   for (i = 0; i < wkdata.length; i++) {")
            .AppendLine("       if (wkdata.charAt(i) != " & """" & " " & """" & ") {")
            .AppendLine("           if (wkdata.charAt(i) != " & """" & "  " & """" & ") {")
            .AppendLine("               wkflg = 1;")
            .AppendLine("           }")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("   if (wkflg == 0) {")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .AppendLine("   return true;")
            .AppendLine("}")
            'CSV�o�͋敪��ݒ肷��
            .AppendLine("function fncSetCsvOut(){")
            .AppendLine("   document.all." & Me.hidCsvOut.ClientID & ".value='';")
            .AppendLine("}")
            '����挟���\����ݒ肷��
            .AppendLine("function fncSetAitesaki(){")
            .AppendLine("   if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '0'||document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == ''){")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.divAitesaki.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.divSitei.ClientID & ".style.display = 'none';")
            .AppendLine("   }else if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '1'){")
            .AppendLine("       document.all." & Me.divAitesaki.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiTo.ClientID & ".value = '';")
            .AppendLine("   }else{")
            .AppendLine("       document.all." & Me.divAitesaki.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.divSitei.ClientID & ".style.display = 'none';")
            .AppendLine("   }")
            .AppendLine("}")
            '����挟������������ꍇ�A�|�b�v�A�b�v���N������
            .AppendLine("function fncAiteSakiSearch(strAiteSakiKbn){")
            '������ʂ��u1:�����X�v�̏ꍇ�A�����X�|�b�v�A�b�v���N������
            .AppendLine("   if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '1'){")
            .AppendLine("       var strkbn='�����X';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       if(strAiteSakiKbn == '1'){")
            .AppendLine("             strClientCdID = '" & Me.tbxAiteSakiCdFrom.ClientID & "';")
            .AppendLine("             strClientMeiID = '" & Me.tbxAiteSakiMeiFrom.ClientID & "';")
            .AppendLine("       }else{")
            .AppendLine("            strClientCdID = '" & Me.tbxAiteSakiCdTo.ClientID & "';")
            .AppendLine("             strClientMeiID = '" & Me.tbxAiteSakiMeiTo.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       if(document.all." & Me.chkAitesakiTaisyouGai.ClientID & ".checked){")
            .AppendLine("            blnTorikesi = 'True';")
            .AppendLine("       }else{")
            .AppendLine("            blnTorikesi = 'False';")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }else if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '5'){")
            '������ʂ��u5:�c�Ə��v�̏ꍇ�A�c�Ə��|�b�v�A�b�v���N������
            .AppendLine("       var strkbn='�c�Ə�';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       if(strAiteSakiKbn == '1'){")
            .AppendLine("            strClientCdID = '" & Me.tbxAiteSakiCdFrom.ClientID & "';")
            .AppendLine("            strClientMeiID = '" & Me.tbxAiteSakiMeiFrom.ClientID & "';")
            .AppendLine("       }else{")
            .AppendLine("           strClientCdID = '" & Me.tbxAiteSakiCdTo.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxAiteSakiMeiTo.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       if(document.all." & Me.chkAitesakiTaisyouGai.ClientID & ".checked){")
            .AppendLine("           blnTorikesi = 'True';")
            .AppendLine("       }else{")
            .AppendLine("           blnTorikesi = 'False';")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }else if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '7'){")
            '������ʂ��u7:�n��v�̏ꍇ�A�n��|�b�v�A�b�v���N������
            .AppendLine("       var strkbn='�n��';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       if(strAiteSakiKbn == '1'){")
            .AppendLine("           strClientCdID = '" & Me.tbxAiteSakiCdFrom.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxAiteSakiMeiFrom.ClientID & "';")
            .AppendLine("       }else{")
            .AppendLine("           strClientCdID = '" & Me.tbxAiteSakiCdTo.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxAiteSakiMeiTo.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       if(document.all." & Me.chkAitesakiTaisyouGai.ClientID & ".checked){")
            .AppendLine("           blnTorikesi = 'True';")
            .AppendLine("       }else{")
            .AppendLine("           blnTorikesi = 'False';")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("function fncKojKaisyaSearch(){")
            .AppendLine(" if (document.all." & Me.tbxKojKaisyaCd.ClientID & ".value=='ALLAL'){")
            .AppendLine(" document.all." & Me.tbxKojKaisyaMei.ClientID & ".value='�w�薳��'}else{")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?blnDelete=True&Kbn='+escape('�H�����')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
              tbxKojKaisyaCd.ClientID & _
                       "&objMei=" & tbxKojKaisyaMei.ClientID & _
                       "&strCd='+escape(eval('document.all.'+'" & _
                       tbxKojKaisyaCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                       tbxKojKaisyaMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("}")
            .AppendLine("       return false;")
            .AppendLine("}")
            '�N���A�{�^������
            .AppendLine("function fncClear(){")
            .AppendLine("   document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value = ''")
            .AppendLine("   document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".value = '';")
            .AppendLine("   document.all." & Me.tbxAiteSakiMeiFrom.ClientID & ".value = '';")
            .AppendLine("   document.all." & Me.tbxAiteSakiCdTo.ClientID & ".value = '';")
            .AppendLine("   document.all." & Me.tbxAiteSakiMeiTo.ClientID & ".value = '';")
            .AppendLine("   document.all." & Me.divAitesaki.ClientID & ".style.display = 'none';")
            .AppendLine("   document.all." & Me.ddlSyouhinCd.ClientID & ".value = '';")
            .AppendLine("   document.all." & Me.ddlKensakuCount.ClientID & ".value = '100';")
            .AppendLine("   document.all." & Me.btnKensaku.ClientID & ".disabled = false;")
            .AppendLine("   document.all." & Me.chkKensakuTaisyouGai.ClientID & ".checked = true;")
            .AppendLine("   document.all." & Me.chkAitesakiTaisyouGai.ClientID & ".checked = true;")
            .AppendLine("   document.all." & Me.divSitei.ClientID & ".style.display = 'none';")
            .AppendLine("   document.all." & Me.tbxKojKaisyaCd.ClientID & ".value = '';")
            .AppendLine("   document.all." & Me.tbxKojKaisyaMei.ClientID & ".value = '';")
            .AppendLine("   return false;")
            .AppendLine("}")
            '�N���A�{�^������
            .AppendLine("function fncClose(){")
            .AppendLine("   window.close();")
            .AppendLine("   return false;")
            .AppendLine("}")

            'DIV�\��
            .AppendLine("function fncShowModal(){")
            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("   var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("   if(buyDiv.style.display=='none')")
            .AppendLine("   {")
            .AppendLine("       buyDiv.style.display='';")
            .AppendLine("       disable.style.display='';")
            .AppendLine("       disable.focus();")
            .AppendLine("   }else{")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("   }")
            .AppendLine("}")
            '�捞�|�b�v�A�b�v��ʂ��N������
            .AppendLine("function fncCsvUpload(){")
            .AppendLine("   window.open('KoujiKakakuMasterInput.aspx', 'KojKakakuWindow')")
            .AppendLine("   return false;")
            .AppendLine("}")

            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub
    ''' <summary>���X�N���[���ݒ�</summary>
    Public Sub SetScroll()
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("divheadright.scrollLeft = document.getElementById('" & Me.hidScroll.ClientID & "').value;")
            .AppendLine("divbodyright.scrollLeft = document.getElementById('" & Me.hidScroll.ClientID & "').value;")
            .AppendLine("divHscroll.scrollLeft = document.getElementById('" & Me.hidScroll.ClientID & "').value;")
        End With
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetScroll", csScript.ToString, True)
    End Sub
    ''' <summary>�H���Ɩ������`�F�b�N</summary>
    ''' <returns>�H���Ɩ������敪</returns>
    Public Function GetKojKengen(ByVal user_info As LoginUserInfo) As Boolean

        Dim dtAccountTable As CommonSearchDataSet.AccountTableDataTable
        '�c�Ə��}�X�^�����擾
        dtAccountTable = commonCheck.CheckKengen(user_info.AccountNo)
        If dtAccountTable.Rows.Count = 0 Then
            Return False
        ElseIf dtAccountTable.Rows(0).Item("koj_gyoumu_kengen") = -1 Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>CSV�o�̓{�^�����N���b�N��</summary>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click

        '���̓`�F�b�N
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        Dim intCount As Long     '��������
        '����������ݒ肷��
        Dim dtParam As Data.DataTable = SetKojKakaku()
        'CSV�o�͏������
        Dim intCsvMax As Integer = CInt(System.Configuration.ConfigurationManager.AppSettings("CsvDownMax"))

        Dim KojKakakuSearchListLogic As New KojKakakuMasterLogic
        '�����f�[�^���擾����
        Dim dtKojKakakuInfo As Data.DataTable = KojKakakuSearchListLogic.GetKojKakakuInfo(dtParam)
        '�������ʂ�ݒ肷��
        Me.grdBodyLeft.DataSource = dtKojKakakuInfo
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = dtKojKakakuInfo
        Me.grdBodyRight.DataBind()

        '����於��ݒ肷��
        Call SetAitesakiMei(Me.ddlAiteSakiSyubetu.SelectedValue, _
                    dtParam.Rows(0).Item("aitesaki_cd_from"), _
                    dtParam.Rows(0).Item("aitesaki_cd_to"), _
                    dtParam.Rows(0).Item("torikesi_aitesaki"))

        Call SetKojKaisya(Me.tbxKojKaisyaCd.Text)
        '�����������擾����
        intCount = KojKakakuSearchListLogic.GetKojKakakuInfoCount(dtParam)

        '�������ʌ�����ݒ肷��
        Call SetKensakuCount(intCount)



        If intCount = 0 Then
            '�\�[�g���{�^����ݒ肷��
            Call SetSortButton(False)
        Else
            '�\�[�g���{�^����ݒ肷��
            Call SetSortButton(True)
            '�\�[�g���{�^���F��ݒ肷��
            Call SetSortButtonColor()
            '�������ݒ肷��
            Call SetKingaku()
            ViewState("dtKojKakakuInfo") = dtKojKakakuInfo
            ViewState("scrollHeight") = scrollHeight
        End If


        If intCount > intCsvMax Then
            strErrMessage = Messages.Instance.MSG051E.Replace("@PARAM1", intCsvMax)
            ShowMessage(strErrMessage, "")
        Else
            Me.hidCsvOut.Value = "1"
            ClientScript.RegisterStartupScript(Me.GetType, "", "<script language=javascript>document.forms[0].submit();</script>")
        End If

    End Sub
    Private Sub grdBodyLeft_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyLeft.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            If setFlg Then
                '�������
                Dim strAiteSakiSyubetu As String
                strAiteSakiSyubetu = e.Row.Cells(0).Text.Split("�F")(0).Trim
                '�����R�[�h
                Dim strAiteSakiCd As String
                If strAiteSakiSyubetu.Equals("0") Then
                    strAiteSakiCd = "ALL"
                Else
                    strAiteSakiCd = e.Row.Cells(1).Text.Trim
                End If
                '���i�R�[�h
                Dim strSyouhinCd As String
                strSyouhinCd = e.Row.Cells(3).Text.Trim
                ''�H����ЃR�[�h
                Dim strKojKaisyaCd As String
                strKojKaisyaCd = CType(e.Row.Cells(4).Controls(3), HiddenField).Value.Replace("�F", "").Trim
                e.Row.Attributes.Add("ondblclick", "fncPopupKobetuSettei('" & strAiteSakiSyubetu & "','" & strAiteSakiCd & "','" & strSyouhinCd & "','" & strKojKaisyaCd & "');")
            End If
        End If

    End Sub

    Private Sub grdBodyRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyRight.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim RowIndex As Integer = e.Row.RowIndex
            If setFlg Then

                '�������
                Dim strAiteSakiSyubetu As String
                strAiteSakiSyubetu = Me.grdBodyLeft.Rows(RowIndex).Cells(0).Text.Split("�F")(0).Trim
                '�����R�[�h
                Dim strAiteSakiCd As String
                If strAiteSakiSyubetu.Equals("0") Then
                    strAiteSakiCd = "ALL"
                Else
                    strAiteSakiCd = Me.grdBodyLeft.Rows(RowIndex).Cells(1).Text.Trim
                End If
                '���i�R�[�h
                Dim strSyouhinCd As String
                strSyouhinCd = Me.grdBodyLeft.Rows(RowIndex).Cells(3).Text.Trim
                '�H����ЃR�[�h
                Dim strKojKaisyaCd As String
                strKojKaisyaCd = CType(Me.grdBodyLeft.Rows(RowIndex).Cells(4).Controls(3), HiddenField).Value.Replace("�F", "").Trim
                e.Row.Attributes.Add("ondblclick", "fncPopupKobetuSettei('" & strAiteSakiSyubetu & "','" & strAiteSakiCd & "','" & strSyouhinCd & "','" & strKojKaisyaCd & "');")
            End If
        End If
    End Sub
End Class