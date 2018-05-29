Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class GenkaMasterSearchList
    Inherits System.Web.UI.Page

    ''' <summary>�����}�X�^�Ɖ�</summary>
    ''' <remarks>�����}�X�^�Ɖ�p�@�\��񋟂���</remarks>
    ''' <history>
    ''' <para>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private genkaMasterLogic As New GenkaMasterLogic
    Private commonCheck As New CommonCheck
    Protected scrollHeight As Integer = 0

    ''' <summary>�y�[�W���b�h</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '�����`�F�b�N
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnEigyouKengen As Boolean
        blnEigyouKengen = commonChk.CommonNinnsyou(strUserID, "kaiseki_master_kanri_kengen")

        'Javascript�쐬
        Call Me.MakeJavascript()

        If Not IsPostBack Then
            '�Q�Ɨ����Ǘ��e�[�u����o�^����B
            commonCheck.SetURL(Me, strUserID)

            If Not Request("sendSearchTerms") Is Nothing Then
                '������
                Call Me.Syokika(CStr(Request("sendSearchTerms")))
            Else
                '������
                Call Me.Syokika(String.Empty)
            End If
        Else

            '��������(�����R�[�h)��ݒ肷��
            If Me.ddlAiteSakiSyubetu.SelectedValue.Trim.Equals("0") OrElse Me.ddlAiteSakiSyubetu.SelectedValue.Trim.Equals(String.Empty) Then
                '�����R�[�hFROM
                Me.tbxAiteSakiCdFrom.Text = String.Empty
                '����於FROM
                Me.tbxAiteSakiMeiFrom.Text = String.Empty
                '�����R�[�hTO
                Me.tbxAiteSakiCdTo.Text = String.Empty
                '����於TO
                Me.tbxAiteSakiMeiTo.Text = String.Empty

                Me.divAitesaki.Attributes.Add("style", "display:none;")
            Else
                Me.divAitesaki.Attributes.Add("style", "display:block;")
            End If

            '�������s�{�^�����p����ݒ肷��
            If Me.chkMiseltutei.Checked Then
                Me.btnKensakujiltukou.Enabled = False
            Else
                Me.btnKensakujiltukou.Enabled = True
            End If

            'CSV�o�̓{�^������������ꍇ
            If Me.hidCsvOut.Value = "1" Then
                'CSV�o��
                Call CsvOutPut()
            End If

            'DIV��\��
            CloseCover()
        End If

        'CSV�捞�{�^����ݒ肷��
        If blnEigyouKengen Then
            Me.btnCsvInput.Enabled = True
        Else
            Me.btnCsvInput.Enabled = False
        End If

        '����飃{�^��
        Me.btnClose.Attributes.Add("onClick", "fncClose();return false;")
        '�CSV�捞��{�^��
        Me.btnCsvInput.Attributes.Add("onClick", "fncOpenInputPopup();return false;")
        '��N���A��{�^��
        Me.btnClear.Attributes.Add("onClick", "fncClear();return false;")
        '������ʂ��ύX����ꍇ�A�����R�[�h����������ݒ肷��
        Me.ddlAiteSakiSyubetu.Attributes.Add("onChange", "return fncSetAitesaki();")

        '�����R�[�h(FROM)���t�H�[�J�X�������ꍇ�A�啶���ɕϊ�����
        Me.tbxAiteSakiCdFrom.Attributes.Add("onBlur", "fncToUpper(this);")
        '�����R�[�h(TO)���t�H�[�J�X�������ꍇ�A�啶���ɕϊ�����
        Me.tbxAiteSakiCdTo.Attributes.Add("onBlur", "fncToUpper(this);")

        '�u���ݒ���܂ށv�`�F�b�N�{�b�N�X��ύX����ꍇ�A�u�������s�v�{�^�����p����ݒ肷��
        Me.chkMiseltutei.Attributes.Add("onClick", "return fncChange();")

        '�����{�^������������ꍇ�A�K�{�`�F�b�N
        Me.btnKensakujiltukou.Attributes.Add("onClick", "if(!fncNyuuryokuCheck('Kensaku')){return false;}else{fncShowModal();}")
        '�CSV�o�ͣ�{�^������������ꍇ�A�K�{�`�F�b�N
        Me.btnCsvOutput.Attributes.Add("onClick", "if(!fncNyuuryokuCheck('Csv')){return false;}else{fncShowModal();}")

        '�����FROM�̢������{�^��
        Me.btnAiteSakiCdFrom.Attributes.Add("onClick", "fncAiteSakiSearch('1');return false;")
        '�����TO�̢������{�^��
        Me.btnAiteSakiCdTo.Attributes.Add("onClick", "fncAiteSakiSearch('2');return false;")

        '������ЃR�[�h�̢������{�^��
        Me.btnKensakuTyousakaisyaCd.Attributes.Add("onClick", "fncTyousaKaisyaSearch();return false;")

        '��ʏc�X�N���[����ݒ肷��
        scrollHeight = ViewState("scrollHeight")
    End Sub

    ''' <summary>������</summary>
    Private Sub Syokika(ByVal sendSearchTerms As String)

        If Not sendSearchTerms.Trim.Equals(String.Empty) Then
            Dim arrSearchTerm() As String = Split(sendSearchTerms, "$$$")

            If arrSearchTerm.Length > 0 Then
                If arrSearchTerm.Length > 1 Then
                    '������ЃR�[�h
                    Me.tbxTyousaKaisyaCd.Text = arrSearchTerm(0).Trim & arrSearchTerm(1).Trim
                    '������Ж�
                    Call Me.SetTyousaKaisyaCd(arrSearchTerm(0).Trim, arrSearchTerm(1).Trim, String.Empty)
                Else
                    '������ЃR�[�h
                    Me.tbxTyousaKaisyaCd.Text = arrSearchTerm(0).Trim
                    '������Ж�
                    Call Me.SetTyousaKaisyaCd(arrSearchTerm(0).Trim, String.Empty, String.Empty)
                End If
            End If

            If arrSearchTerm.Length > 2 Then
                '������ʂ�ݒ�
                Call SetAitesakiSyobetu(arrSearchTerm(2).Trim)
            Else
                '������ʂ�ݒ�
                Call SetAitesakiSyobetu(String.Empty)
            End If

            If arrSearchTerm.Length > 3 Then
                '�����R�[�h��ݒ�
                Call Me.SetAitesakiCd(Me.ddlAiteSakiSyubetu.SelectedValue, arrSearchTerm(3), String.Empty, "1")
            End If

            If arrSearchTerm.Length > 4 Then
                '���i�R�[�h��ݒ�
                Call SetSyouhinCd(arrSearchTerm(4))
            Else
                '���i�R�[�h��ݒ�
                Call SetSyouhinCd(String.Empty)
            End If

            If arrSearchTerm.Length > 5 Then
                '�������@NO��ݒ�
                Call SetTyousaHouhou(arrSearchTerm(5))
            Else
                '�������@NO��ݒ�
                Call SetTyousaHouhou(String.Empty)
            End If

        Else
            '������ЃR�[�h
            Me.tbxTyousaKaisyaCd.Text = String.Empty
            '������Ж�
            Me.tbxTyousaKaisyaMei.Text = String.Empty
            '������ʂ�ݒ�
            Call SetAitesakiSyobetu(String.Empty)
            '�����R�[�h��ݒ�
            Call Me.SetAitesakiCd(Me.ddlAiteSakiSyubetu.SelectedValue, String.Empty, String.Empty, "1")
            '���i�R�[�h��ݒ�
            Call SetSyouhinCd(String.Empty)
            '�������@NO��ݒ�
            Call SetTyousaHouhou(String.Empty)
        End If



        '�����������
        Me.ddlKensakuJyouken.SelectedValue = "100"
        '����͌����ΏۊO
        Me.chkKensakuTaisyouGai.Checked = True
        '��������͑ΏۊO
        Me.chkAitesakiTaisyouGai.Checked = True
        '���ݒ���܂�
        Me.chkMiseltutei.Checked = False

        '��������
        Me.lblCount.Text = String.Empty

        '�����N�{�^���̕\����ݒ�
        Call Me.SetUpDownHyouji(False)

        '����
        Me.grdBodyLeft.DataSource = Nothing
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = Nothing
        Me.grdBodyRight.DataBind()


        '�����N�{�^���̕\����ݒ�
        Call Me.SetUpDownHyouji(False)

        '�����N�{�^���̐F
        'Call setUpDownColor()

    End Sub

    ''' <summary>������Ж���ݒ�</summary>
    Private Sub SetTyousaKaisyaCd(ByVal strTyousaKaisyaCd As String, ByVal strJigyousyoCd As String, ByVal strKensakuTaisyouGai As String)

        If Not strTyousaKaisyaCd.Trim.Equals(String.Empty) Then
            '������Ж��f�[�^���擾
            Dim dtTyousaKaisyaMei As New Data.DataTable
            dtTyousaKaisyaMei = genkaMasterLogic.GetTyousaKaisyaMei(strTyousaKaisyaCd, strJigyousyoCd, strKensakuTaisyouGai)

            If dtTyousaKaisyaMei.Rows.Count > 0 Then
                '������Ж�
                Me.tbxTyousaKaisyaMei.Text = dtTyousaKaisyaMei.Rows(0).Item(0).ToString.Trim
            Else
                Me.tbxTyousaKaisyaMei.Text = String.Empty
            End If
        Else
            Me.tbxTyousaKaisyaMei.Text = String.Empty
        End If


    End Sub


    ''' <summary>������ʂ�ݒ�</summary>
    Private Sub SetAitesakiSyobetu(ByVal strAitesakiSyobetu As String)
        '������ʃf�[�^���擾
        Dim dtAitesakiSyobetu As New Data.DataTable
        dtAitesakiSyobetu = genkaMasterLogic.GetAiteSakiSyubetu()

        If dtAitesakiSyobetu.Rows.Count > 0 Then
            '�������
            Me.ddlAiteSakiSyubetu.DataValueField = "code"
            Me.ddlAiteSakiSyubetu.DataTextField = "meisyou"
            Me.ddlAiteSakiSyubetu.DataSource = dtAitesakiSyobetu
            Me.ddlAiteSakiSyubetu.DataBind()
        End If

        '������ʂ̐擪�s�͋󗓂��Z�b�g����
        Me.ddlAiteSakiSyubetu.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '�v�����^�̃f�t�H���g�\��
        If strAitesakiSyobetu.Equals(String.Empty) Then
            Me.ddlAiteSakiSyubetu.SelectedValue = String.Empty
        Else
            Try
                Me.ddlAiteSakiSyubetu.SelectedValue = strAitesakiSyobetu
            Catch ex As Exception
                Me.ddlAiteSakiSyubetu.SelectedValue = String.Empty
            End Try
        End If

    End Sub

    ''' <summary>�����R�[�h��ݒ�</summary>
    Private Sub SetAitesakiCd(ByVal strAitesakiSyobetu As String, ByVal strAitesakiFromCd As String, ByVal strAitesakiToCd As String, ByVal strTorikesiAitesaki As String)

        If Me.ddlAiteSakiSyubetu.SelectedValue.Trim.Equals(String.Empty) OrElse Me.ddlAiteSakiSyubetu.SelectedValue.Trim.Equals("0") OrElse Me.ddlAiteSakiSyubetu.SelectedValue.Trim.Equals("3") Then
            '�����R�[�hFROM
            Me.tbxAiteSakiCdFrom.Text = String.Empty
            '����於FROM
            Me.tbxAiteSakiMeiFrom.Text = String.Empty
            '�����R�[�hTO
            Me.tbxAiteSakiCdTo.Text = String.Empty
            '����於TO
            Me.tbxAiteSakiMeiTo.Text = String.Empty

            Me.divAitesaki.Attributes.Add("style", "display:none;")
        Else

            If Not strAitesakiFromCd.Trim.Equals(String.Empty) Then
                '�����R�[�hFROM
                Me.tbxAiteSakiCdFrom.Text = strAitesakiFromCd

                '����於���擾
                Dim dtAitesakiMei As New Data.DataTable
                dtAitesakiMei = genkaMasterLogic.GetAitesakiMei(CInt(strAitesakiSyobetu), strAitesakiFromCd, strTorikesiAitesaki)

                '����於FROM
                If dtAitesakiMei.Rows.Count > 0 Then
                    Me.tbxAiteSakiMeiFrom.Text = dtAitesakiMei.Rows(0).Item("aitesaki_mei").ToString.Trim
                Else
                    'Me.tbxAiteSakiCdFrom.Text = String.Empty
                    Me.tbxAiteSakiMeiFrom.Text = String.Empty
                End If
            Else
                Me.tbxAiteSakiMeiFrom.Text = String.Empty
            End If


            If Not strAitesakiToCd.Trim.Equals(String.Empty) Then
                '�����R�[�hTO
                Me.tbxAiteSakiCdTo.Text = strAitesakiToCd

                '����於���擾
                Dim dtAitesakiMei As New Data.DataTable
                dtAitesakiMei = genkaMasterLogic.GetAitesakiMei(CInt(strAitesakiSyobetu), strAitesakiToCd, strTorikesiAitesaki)

                '����於TO
                If dtAitesakiMei.Rows.Count > 0 Then
                    Me.tbxAiteSakiMeiTo.Text = dtAitesakiMei.Rows(0).Item("aitesaki_mei").ToString.Trim
                Else
                    'Me.tbxAiteSakiCdTo.Text = String.Empty
                    Me.tbxAiteSakiMeiTo.Text = String.Empty
                End If
            Else
                Me.tbxAiteSakiMeiTo.Text = String.Empty

            End If

            Me.divAitesaki.Attributes.Add("style", "display:block;")
        End If

    End Sub

    ''' <summary>���i�R�[�h��ݒ�</summary>
    Private Sub SetSyouhinCd(ByVal strSyouhinCd As String)
        '���i�R�[�h�f�[�^���擾
        Dim dtSyouhinCd As New Data.DataTable
        dtSyouhinCd = genkaMasterLogic.GetSyouhinCd()

        If dtSyouhinCd.Rows.Count > 0 Then
            '���i�R�[�h
            Me.ddlSyouhinCd.DataValueField = "syouhin_cd"
            Me.ddlSyouhinCd.DataTextField = "syouhin_mei"
            Me.ddlSyouhinCd.DataSource = dtSyouhinCd
            Me.ddlSyouhinCd.DataBind()
        End If

        '���i�R�[�h�̐擪�s�͋󗓂��Z�b�g����
        Me.ddlSyouhinCd.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '�v�����^�̃f�t�H���g�\��
        If strSyouhinCd.Equals(String.Empty) Then
            Me.ddlSyouhinCd.SelectedValue = String.Empty
        Else
            Try
                Me.ddlSyouhinCd.SelectedValue = strSyouhinCd
            Catch ex As Exception
                Me.ddlSyouhinCd.SelectedValue = String.Empty
            End Try
        End If

    End Sub

    ''' <summary>�������@��ݒ�</summary>
    Private Sub SetTyousaHouhou(ByVal strTyousaHouhou As String)
        '�������@�f�[�^���擾
        Dim dtTyousaHouhou As New Data.DataTable
        dtTyousaHouhou = genkaMasterLogic.GetTyousaHouhou()

        If dtTyousaHouhou.Rows.Count > 0 Then
            '�������@
            Me.ddlTyousaHouhou.DataValueField = "tys_houhou_no"
            Me.ddlTyousaHouhou.DataTextField = "tys_houhou_mei"
            Me.ddlTyousaHouhou.DataSource = dtTyousaHouhou
            Me.ddlTyousaHouhou.DataBind()
        End If

        '�������@�̐擪�s�͋󗓂��Z�b�g����
        Me.ddlTyousaHouhou.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '�v�����^�̃f�t�H���g�\��
        If strTyousaHouhou.Equals(String.Empty) Then
            Me.ddlTyousaHouhou.SelectedValue = String.Empty
        Else
            Try
                Me.ddlTyousaHouhou.SelectedValue = strTyousaHouhou
            Catch ex As Exception
                Me.ddlTyousaHouhou.SelectedValue = String.Empty
            End Try
        End If

    End Sub


    ''' <summary>��������s��{�^�����N���b�N��</summary>
    Private Sub btnKensakujiltukou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakujiltukou.Click

        '���̓`�F�b�N
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '������Ж���ݒ�
        Call Me.SetTyousaKaisyaCd(Left(Me.tbxTyousaKaisyaCd.Text.Trim, 4), IIf(Me.tbxTyousaKaisyaCd.Text.Trim.Length > 4, Mid(Me.tbxTyousaKaisyaCd.Text.Trim, 5, 6), String.Empty), String.Empty)

        '����於��ݒ�
        Call Me.SetAitesakiCd(Me.ddlAiteSakiSyubetu.SelectedValue.Trim, Me.tbxAiteSakiCdFrom.Text.Trim, Me.tbxAiteSakiCdTo.Text.Trim, IIf(Me.chkAitesakiTaisyouGai.Checked, "1", String.Empty))

        '�������̃f�[�^���擾
        Dim dtGenkaJyouhou As New GenkaMasterDataSet.GenkaInfoTableDataTable
        dtGenkaJyouhou = genkaMasterLogic.GetGenkaJyouhou(Me.ddlKensakuJyouken.SelectedValue.Trim, Me.tbxTyousaKaisyaCd.Text.Trim, Me.ddlAiteSakiSyubetu.SelectedValue.Trim, Me.tbxAiteSakiCdFrom.Text.Trim, Me.tbxAiteSakiCdTo.Text.Trim, Me.ddlSyouhinCd.SelectedValue.Trim, Me.ddlTyousaHouhou.SelectedValue.Trim, Me.chkKensakuTaisyouGai.Checked, Me.chkAitesakiTaisyouGai.Checked)

        If dtGenkaJyouhou.Rows.Count > 0 Then

            ViewState("dtGenkaJyouhou") = dtGenkaJyouhou

            Me.grdBodyLeft.DataSource = CType(ViewState("dtGenkaJyouhou"), GenkaMasterDataSet.GenkaInfoTableDataTable)
            Me.grdBodyLeft.DataBind()

            Me.grdBodyRight.DataSource = CType(ViewState("dtGenkaJyouhou"), GenkaMasterDataSet.GenkaInfoTableDataTable)
            Me.grdBodyRight.DataBind()
            '�����N�{�^���̕\����ݒ�
            Call Me.SetUpDownHyouji(True)

            '�����N�{�^���̐F��ݒ�
            Call Me.setUpDownColor()

            '�������ʂ�ݒ�
            Dim intKenkasaKensuu As Integer = Me.SetKensakuResult(True)

        Else
            ViewState("dtGenkaJyouhou") = Nothing

            Me.grdBodyLeft.DataSource = Nothing
            Me.grdBodyLeft.DataBind()

            Me.grdBodyRight.DataSource = Nothing
            Me.grdBodyRight.DataBind()
            '�����N�{�^���̕\����ݒ�
            Call Me.SetUpDownHyouji(False)

            '�������ʂ�ݒ�
            Dim intKenkasaKensuu As Integer = Me.SetKensakuResult(False)
            '�G���[���b�Z�[�W��\��
            ShowMessage(Messages.Instance.MSG020E, String.Empty)

        End If

        ViewState("scrollHeight") = scrollHeight

    End Sub

    ''' <summary>CSV�o�̓{�^�����N���b�N��</summary>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click

        '���̓`�F�b�N
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '������Ж���ݒ�
        Call Me.SetTyousaKaisyaCd(Left(Me.tbxTyousaKaisyaCd.Text.Trim, 4), IIf(Me.tbxTyousaKaisyaCd.Text.Trim.Length > 4, Mid(Me.tbxTyousaKaisyaCd.Text.Trim, 5, 6), String.Empty), String.Empty)

        '����於��ݒ�
        Call Me.SetAitesakiCd(Me.ddlAiteSakiSyubetu.SelectedValue.Trim, Me.tbxAiteSakiCdFrom.Text.Trim, Me.tbxAiteSakiCdTo.Text.Trim, IIf(Me.chkAitesakiTaisyouGai.Checked, "1", String.Empty))

        '��������
        Dim intCount As Long
        'CSV�o�͏������
        Dim intCsvMax As Integer = CInt(System.Configuration.ConfigurationManager.AppSettings("CsvDownMax"))

        ''������ʂ��u1�F�����X�v�������R�[�hFROM�Ƒ����R�[�hTO�����͂̏ꍇ�A�����X�����`�F�b�N
        'If Me.ddlAiteSakiSyubetu.SelectedValue = "1" _
        '        AndAlso Me.tbxAiteSakiCdFrom.Text.Trim <> String.Empty _
        '        AndAlso Me.tbxAiteSakiCdTo.Text.Trim <> String.Empty Then
        '    '�����X�������擾����
        '    intCount = genkaMasterLogic.GetKameitenCount(Me.tbxAiteSakiCdFrom.Text.Trim, Me.tbxAiteSakiCdTo.Text.Trim, IIf(Me.chkAitesakiTaisyouGai.Checked, "1", String.Empty))
        '    If intCount > intCsvMax Then
        '        strErrMessage = Messages.Instance.MSG046E.Replace("@PARAM1", intCsvMax)
        '        ShowMessage(strErrMessage, Me.tbxAiteSakiCdFrom.ClientID)
        '        Exit Sub
        '    End If
        'End If

        If Me.chkMiseltutei.Checked Then

            '�����N�{�^���̕\����ݒ�
            Call Me.SetUpDownHyouji(False)

            '�������ʂ�ݒ�
            Dim intKenkasaKensuu As Integer = Me.SetKensakuResult(False)

            Me.grdBodyLeft.DataSource = Nothing
            Me.grdBodyLeft.DataBind()
            Me.grdBodyRight.DataSource = Nothing
            Me.grdBodyRight.DataBind()

            '���ݒ���܂ޔ̔����iCSV�f�[�^������ݒ肷��
            intCount = genkaMasterLogic.GetMiSeteiGenkaCSVCount(Me.tbxTyousaKaisyaCd.Text.Trim, Me.ddlAiteSakiSyubetu.SelectedValue.Trim, Me.tbxAiteSakiCdFrom.Text.Trim, Me.tbxAiteSakiCdTo.Text.Trim, Me.ddlSyouhinCd.SelectedValue.Trim, Me.ddlTyousaHouhou.SelectedValue.Trim, Me.chkKensakuTaisyouGai.Checked, Me.chkAitesakiTaisyouGai.Checked)
        Else
            '�������̃f�[�^���擾
            Dim dtGenkaJyouhou As New GenkaMasterDataSet.GenkaInfoTableDataTable
            dtGenkaJyouhou = genkaMasterLogic.GetGenkaJyouhou(Me.ddlKensakuJyouken.SelectedValue.Trim, Me.tbxTyousaKaisyaCd.Text.Trim, Me.ddlAiteSakiSyubetu.SelectedValue.Trim, Me.tbxAiteSakiCdFrom.Text.Trim, Me.tbxAiteSakiCdTo.Text.Trim, Me.ddlSyouhinCd.SelectedValue.Trim, Me.ddlTyousaHouhou.SelectedValue.Trim, Me.chkKensakuTaisyouGai.Checked, Me.chkAitesakiTaisyouGai.Checked)

            If dtGenkaJyouhou.Rows.Count > 0 Then

                ViewState("dtGenkaJyouhou") = dtGenkaJyouhou

                Me.grdBodyLeft.DataSource = CType(ViewState("dtGenkaJyouhou"), GenkaMasterDataSet.GenkaInfoTableDataTable)
                Me.grdBodyLeft.DataBind()

                Me.grdBodyRight.DataSource = CType(ViewState("dtGenkaJyouhou"), GenkaMasterDataSet.GenkaInfoTableDataTable)
                Me.grdBodyRight.DataBind()
                '�����N�{�^���̕\����ݒ�
                Call Me.SetUpDownHyouji(True)

                '�����N�{�^���̐F��ݒ�
                Call Me.setUpDownColor()

                '�������ʂ�ݒ�
                intCount = Me.SetKensakuResult(True)

            Else
                ViewState("dtGenkaJyouhou") = Nothing

                Me.grdBodyLeft.DataSource = Nothing
                Me.grdBodyLeft.DataBind()

                Me.grdBodyRight.DataSource = Nothing
                Me.grdBodyRight.DataBind()
                '�����N�{�^���̕\����ݒ�
                Call Me.SetUpDownHyouji(False)

                '�������ʂ�ݒ�
                intCount = Me.SetKensakuResult(False)
            End If

        End If

        ViewState("scrollHeight") = scrollHeight

        If intCount > intCsvMax Then
            strErrMessage = Messages.Instance.MSG051E.Replace("@PARAM1", intCsvMax)
            ShowMessage(strErrMessage, String.Empty)
        Else
            Me.hidCsvOut.Value = "1"
            ClientScript.RegisterStartupScript(Me.GetType, "", "<script language=javascript>document.forms[0].submit();</script>")
        End If

    End Sub

    '''' <summary>CSV�o��</summary>
    Private Sub CsvOutPut()

        '�f�[�^���擾
        Dim dtGenkaCsvInfo As New Data.DataTable
        If Me.chkMiseltutei.Checked = True Then
            '���ݒ���܂ޔ̔����iCSV�f�[�^���擾����
            dtGenkaCsvInfo = genkaMasterLogic.GetMiSeteiGenkaCSVInfo(Me.tbxTyousaKaisyaCd.Text.Trim, Me.ddlAiteSakiSyubetu.SelectedValue.Trim, Me.tbxAiteSakiCdFrom.Text.Trim, Me.tbxAiteSakiCdTo.Text.Trim, Me.ddlSyouhinCd.SelectedValue.Trim, Me.ddlTyousaHouhou.SelectedValue.Trim, Me.chkKensakuTaisyouGai.Checked, Me.chkAitesakiTaisyouGai.Checked)
        Else
            '�̔����iCSV�f�[�^���擾����
            dtGenkaCsvInfo = genkaMasterLogic.GetGenkaJyouhouCSV(Me.tbxTyousaKaisyaCd.Text.Trim, Me.ddlAiteSakiSyubetu.SelectedValue.Trim, Me.tbxAiteSakiCdFrom.Text.Trim, Me.tbxAiteSakiCdTo.Text.Trim, Me.ddlSyouhinCd.SelectedValue.Trim, Me.ddlTyousaHouhou.SelectedValue.Trim, Me.chkKensakuTaisyouGai.Checked, Me.chkAitesakiTaisyouGai.Checked)
        End If

        'CSV�t�@�C�����ݒ�
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("GenkaMasterCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSV�t�@�C���w�b�_�ݒ�
        writer.WriteLine(EarthConst.conGenkaCsvHeader)

        'CSV�t�@�C�����e�ݒ�
        For i As Integer = 0 To dtGenkaCsvInfo.Rows.Count - 1
            With dtGenkaCsvInfo.Rows(i)
                writer.WriteLine(.Item(0), .Item(1), .Item(2), .Item(3), .Item(4), .Item(5), .Item(6), .Item(7), .Item(8), .Item(9), _
                                 .Item(10), .Item(11), .Item(12), .Item(13), .Item(14), .Item(15), .Item(16), .Item(17), .Item(18), .Item(19), _
                                 .Item(20), .Item(21), .Item(22), .Item(23), .Item(24), .Item(25), .Item(26), .Item(27), .Item(28), .Item(29), _
                                 .Item(30), .Item(31), .Item(32), .Item(33), .Item(34), .Item(35), .Item(36), .Item(37), .Item(38), .Item(39), _
                                 .Item(40), .Item(41))
            End With
        Next


        'CSV�t�@�C���_�E�����[�h
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()

    End Sub



    ''' <summary>�������ʂ�ݒ�</summary>
    Private Function SetKensakuResult(ByVal blnFlg As Boolean) As Integer

        '�������擾
        Dim intGenkaJyouhouCount As Integer = 0

        If blnFlg.Equals(True) Then

            intGenkaJyouhouCount = genkaMasterLogic.GetGenkaJyouhouCount(Me.tbxTyousaKaisyaCd.Text.Trim, Me.ddlAiteSakiSyubetu.SelectedValue.Trim, Me.tbxAiteSakiCdFrom.Text.Trim, Me.tbxAiteSakiCdTo.Text.Trim, Me.ddlSyouhinCd.SelectedValue.Trim, Me.ddlTyousaHouhou.SelectedValue.Trim, Me.chkKensakuTaisyouGai.Checked, Me.chkAitesakiTaisyouGai.Checked)

            If Me.ddlKensakuJyouken.SelectedValue.Trim.Equals("max") Then

                Me.lblCount.Text = CStr(intGenkaJyouhouCount)
                '���F
                Me.lblCount.ForeColor = Drawing.Color.Black

                scrollHeight = intGenkaJyouhouCount * 22 + 1
            Else
                If intGenkaJyouhouCount > CInt(Me.ddlKensakuJyouken.SelectedValue) Then
                    Me.lblCount.Text = Me.ddlKensakuJyouken.SelectedValue.Trim & " / " & CStr(intGenkaJyouhouCount)
                    '�ԐF
                    Me.lblCount.ForeColor = Drawing.Color.Red

                    scrollHeight = Me.ddlKensakuJyouken.SelectedValue * 22 + 1
                Else
                    Me.lblCount.Text = CStr(intGenkaJyouhouCount)
                    '���F
                    Me.lblCount.ForeColor = Drawing.Color.Black

                    scrollHeight = intGenkaJyouhouCount * 22 + 1
                End If
            End If
        Else
            Me.lblCount.Text = "0"
            '���F
            Me.lblCount.ForeColor = Drawing.Color.Black

            scrollHeight = intGenkaJyouhouCount * 22 + 1

        End If

        Return intGenkaJyouhouCount

    End Function

    ''' <summary>�����N�{�^���̐F��ݒ�</summary>
    Public Sub setUpDownColor()

        btnTyousakaisyaCdUp.ForeColor = Drawing.Color.SkyBlue
        btnTyousakaisyaCdDown.ForeColor = Drawing.Color.SkyBlue
        btnTyousakaisyaMeiUp.ForeColor = Drawing.Color.SkyBlue
        btnTyousakaisyaMeiDown.ForeColor = Drawing.Color.SkyBlue
        btnAiteSakiSyubetuUp.ForeColor = Drawing.Color.SkyBlue
        btnAiteSakiSyubetuDown.ForeColor = Drawing.Color.SkyBlue
        btnAiteSakiCdUp.ForeColor = Drawing.Color.SkyBlue
        btnAiteSakiCdDown.ForeColor = Drawing.Color.SkyBlue
        btnAiteSakiMeiUp.ForeColor = Drawing.Color.SkyBlue
        btnAiteSakiMeiDown.ForeColor = Drawing.Color.SkyBlue
        btnSyouhinCdUp.ForeColor = Drawing.Color.SkyBlue
        btnSyouhinCdDown.ForeColor = Drawing.Color.SkyBlue
        btnSyouhinMeiUp.ForeColor = Drawing.Color.SkyBlue
        btnSyouhinMeiDown.ForeColor = Drawing.Color.SkyBlue
        btnTyousaHouhouUp.ForeColor = Drawing.Color.SkyBlue
        btnTyousaHouhouDown.ForeColor = Drawing.Color.SkyBlue
        btnTorikesiUp.ForeColor = Drawing.Color.SkyBlue
        btnTorikesiDown.ForeColor = Drawing.Color.SkyBlue
        btnKakaku1Up.ForeColor = Drawing.Color.SkyBlue
        btnKakaku1Down.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg1Up.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg1Down.ForeColor = Drawing.Color.SkyBlue
        btnKakaku2Up.ForeColor = Drawing.Color.SkyBlue
        btnKakaku2Down.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg2Up.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg2Down.ForeColor = Drawing.Color.SkyBlue
        btnKakaku3Up.ForeColor = Drawing.Color.SkyBlue
        btnKakaku3Down.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg3Up.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg3Down.ForeColor = Drawing.Color.SkyBlue
        btnKakaku4Up.ForeColor = Drawing.Color.SkyBlue
        btnKakaku4Down.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg4Up.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg4Down.ForeColor = Drawing.Color.SkyBlue
        btnKakaku5Up.ForeColor = Drawing.Color.SkyBlue
        btnKakaku5Down.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg5Up.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg5Down.ForeColor = Drawing.Color.SkyBlue
        btnKakaku6Up.ForeColor = Drawing.Color.SkyBlue
        btnKakaku6Down.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg6Up.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg6Down.ForeColor = Drawing.Color.SkyBlue
        btnKakaku7Up.ForeColor = Drawing.Color.SkyBlue
        btnKakaku7Down.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg7Up.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg7Down.ForeColor = Drawing.Color.SkyBlue
        btnKakaku8Up.ForeColor = Drawing.Color.SkyBlue
        btnKakaku8Down.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg8Up.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg8Down.ForeColor = Drawing.Color.SkyBlue
        btnKakaku9Up.ForeColor = Drawing.Color.SkyBlue
        btnKakaku9Down.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg9Up.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg9Down.ForeColor = Drawing.Color.SkyBlue
        btnKakaku10Up.ForeColor = Drawing.Color.SkyBlue
        btnKakaku10Down.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg10Up.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg10Down.ForeColor = Drawing.Color.SkyBlue
        btnKakaku11to19Up.ForeColor = Drawing.Color.SkyBlue
        btnKakaku11to19Down.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg11to19Up.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg11to19Down.ForeColor = Drawing.Color.SkyBlue
        btnKakaku21to29Up.ForeColor = Drawing.Color.SkyBlue
        btnKakaku21to29Down.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg21to29Up.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg21to29Down.ForeColor = Drawing.Color.SkyBlue
        btnKakaku31to39Up.ForeColor = Drawing.Color.SkyBlue
        btnKakaku31to39Down.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg31to39Up.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg31to39Down.ForeColor = Drawing.Color.SkyBlue
        btnKakaku41to49Up.ForeColor = Drawing.Color.SkyBlue
        btnKakaku41to49Down.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg41to49Up.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg41to49Down.ForeColor = Drawing.Color.SkyBlue
        btnKakaku50Up.ForeColor = Drawing.Color.SkyBlue
        btnKakaku50Down.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg50Up.ForeColor = Drawing.Color.SkyBlue
        btnKakakuHenkouFlg50Down.ForeColor = Drawing.Color.SkyBlue

    End Sub

    ''' <summary>�����N�{�^���̕\����ݒ�</summary>
    Public Sub SetUpDownHyouji(ByVal blnHyoujiFlg As Boolean)

        If blnHyoujiFlg = True Then
            btnTyousakaisyaCdUp.Visible = True
            btnTyousakaisyaCdDown.Visible = True
            btnTyousakaisyaMeiUp.Visible = True
            btnTyousakaisyaMeiDown.Visible = True
            btnAiteSakiSyubetuUp.Visible = True
            btnAiteSakiSyubetuDown.Visible = True
            btnAiteSakiCdUp.Visible = True
            btnAiteSakiCdDown.Visible = True
            btnAiteSakiMeiUp.Visible = True
            btnAiteSakiMeiDown.Visible = True
            btnSyouhinCdUp.Visible = True
            btnSyouhinCdDown.Visible = True
            btnSyouhinMeiUp.Visible = True
            btnSyouhinMeiDown.Visible = True
            btnTyousaHouhouUp.Visible = True
            btnTyousaHouhouDown.Visible = True
            btnTorikesiUp.Visible = True
            btnTorikesiDown.Visible = True
            btnKakaku1Up.Visible = True
            btnKakaku1Down.Visible = True
            btnKakakuHenkouFlg1Up.Visible = True
            btnKakakuHenkouFlg1Down.Visible = True
            btnKakaku2Up.Visible = True
            btnKakaku2Down.Visible = True
            btnKakakuHenkouFlg2Up.Visible = True
            btnKakakuHenkouFlg2Down.Visible = True
            btnKakaku3Up.Visible = True
            btnKakaku3Down.Visible = True
            btnKakakuHenkouFlg3Up.Visible = True
            btnKakakuHenkouFlg3Down.Visible = True
            btnKakaku4Up.Visible = True
            btnKakaku4Down.Visible = True
            btnKakakuHenkouFlg4Up.Visible = True
            btnKakakuHenkouFlg4Down.Visible = True
            btnKakaku5Up.Visible = True
            btnKakaku5Down.Visible = True
            btnKakakuHenkouFlg5Up.Visible = True
            btnKakakuHenkouFlg5Down.Visible = True
            btnKakaku6Up.Visible = True
            btnKakaku6Down.Visible = True
            btnKakakuHenkouFlg6Up.Visible = True
            btnKakakuHenkouFlg6Down.Visible = True
            btnKakaku7Up.Visible = True
            btnKakaku7Down.Visible = True
            btnKakakuHenkouFlg7Up.Visible = True
            btnKakakuHenkouFlg7Down.Visible = True
            btnKakaku8Up.Visible = True
            btnKakaku8Down.Visible = True
            btnKakakuHenkouFlg8Up.Visible = True
            btnKakakuHenkouFlg8Down.Visible = True
            btnKakaku9Up.Visible = True
            btnKakaku9Down.Visible = True
            btnKakakuHenkouFlg9Up.Visible = True
            btnKakakuHenkouFlg9Down.Visible = True
            btnKakaku10Up.Visible = True
            btnKakaku10Down.Visible = True
            btnKakakuHenkouFlg10Up.Visible = True
            btnKakakuHenkouFlg10Down.Visible = True
            btnKakaku11to19Up.Visible = True
            btnKakaku11to19Down.Visible = True
            btnKakakuHenkouFlg11to19Up.Visible = True
            btnKakakuHenkouFlg11to19Down.Visible = True
            btnKakaku21to29Up.Visible = True
            btnKakaku21to29Down.Visible = True
            btnKakakuHenkouFlg21to29Up.Visible = True
            btnKakakuHenkouFlg21to29Down.Visible = True
            btnKakaku31to39Up.Visible = True
            btnKakaku31to39Down.Visible = True
            btnKakakuHenkouFlg31to39Up.Visible = True
            btnKakakuHenkouFlg31to39Down.Visible = True
            btnKakaku41to49Up.Visible = True
            btnKakaku41to49Down.Visible = True
            btnKakakuHenkouFlg41to49Up.Visible = True
            btnKakakuHenkouFlg41to49Down.Visible = True
            btnKakaku50Up.Visible = True
            btnKakaku50Down.Visible = True
            btnKakakuHenkouFlg50Up.Visible = True
            btnKakakuHenkouFlg50Down.Visible = True
        Else
            btnTyousakaisyaCdUp.Visible = False
            btnTyousakaisyaCdDown.Visible = False
            btnTyousakaisyaMeiUp.Visible = False
            btnTyousakaisyaMeiDown.Visible = False
            btnAiteSakiSyubetuUp.Visible = False
            btnAiteSakiSyubetuDown.Visible = False
            btnAiteSakiCdUp.Visible = False
            btnAiteSakiCdDown.Visible = False
            btnAiteSakiMeiUp.Visible = False
            btnAiteSakiMeiDown.Visible = False
            btnSyouhinCdUp.Visible = False
            btnSyouhinCdDown.Visible = False
            btnSyouhinMeiUp.Visible = False
            btnSyouhinMeiDown.Visible = False
            btnTyousaHouhouUp.Visible = False
            btnTyousaHouhouDown.Visible = False
            btnTorikesiUp.Visible = False
            btnTorikesiDown.Visible = False
            btnKakaku1Up.Visible = False
            btnKakaku1Down.Visible = False
            btnKakakuHenkouFlg1Up.Visible = False
            btnKakakuHenkouFlg1Down.Visible = False
            btnKakaku2Up.Visible = False
            btnKakaku2Down.Visible = False
            btnKakakuHenkouFlg2Up.Visible = False
            btnKakakuHenkouFlg2Down.Visible = False
            btnKakaku3Up.Visible = False
            btnKakaku3Down.Visible = False
            btnKakakuHenkouFlg3Up.Visible = False
            btnKakakuHenkouFlg3Down.Visible = False
            btnKakaku4Up.Visible = False
            btnKakaku4Down.Visible = False
            btnKakakuHenkouFlg4Up.Visible = False
            btnKakakuHenkouFlg4Down.Visible = False
            btnKakaku5Up.Visible = False
            btnKakaku5Down.Visible = False
            btnKakakuHenkouFlg5Up.Visible = False
            btnKakakuHenkouFlg5Down.Visible = False
            btnKakaku6Up.Visible = False
            btnKakaku6Down.Visible = False
            btnKakakuHenkouFlg6Up.Visible = False
            btnKakakuHenkouFlg6Down.Visible = False
            btnKakaku7Up.Visible = False
            btnKakaku7Down.Visible = False
            btnKakakuHenkouFlg7Up.Visible = False
            btnKakakuHenkouFlg7Down.Visible = False
            btnKakaku8Up.Visible = False
            btnKakaku8Down.Visible = False
            btnKakakuHenkouFlg8Up.Visible = False
            btnKakakuHenkouFlg8Down.Visible = False
            btnKakaku9Up.Visible = False
            btnKakaku9Down.Visible = False
            btnKakakuHenkouFlg9Up.Visible = False
            btnKakakuHenkouFlg9Down.Visible = False
            btnKakaku10Up.Visible = False
            btnKakaku10Down.Visible = False
            btnKakakuHenkouFlg10Up.Visible = False
            btnKakakuHenkouFlg10Down.Visible = False
            btnKakaku11to19Up.Visible = False
            btnKakaku11to19Down.Visible = False
            btnKakakuHenkouFlg11to19Up.Visible = False
            btnKakakuHenkouFlg11to19Down.Visible = False
            btnKakaku21to29Up.Visible = False
            btnKakaku21to29Down.Visible = False
            btnKakakuHenkouFlg21to29Up.Visible = False
            btnKakakuHenkouFlg21to29Down.Visible = False
            btnKakaku31to39Up.Visible = False
            btnKakaku31to39Down.Visible = False
            btnKakakuHenkouFlg31to39Up.Visible = False
            btnKakakuHenkouFlg31to39Down.Visible = False
            btnKakaku41to49Up.Visible = False
            btnKakaku41to49Down.Visible = False
            btnKakakuHenkouFlg41to49Up.Visible = False
            btnKakakuHenkouFlg41to49Down.Visible = False
            btnKakaku50Up.Visible = False
            btnKakaku50Down.Visible = False
            btnKakakuHenkouFlg50Up.Visible = False
            btnKakakuHenkouFlg50Down.Visible = False
        End If

    End Sub

    Private Sub btnSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTyousakaisyaCdUp.Click, _
                                                                                           btnTyousakaisyaCdDown.Click, _
                                                                                           btnTyousakaisyaMeiUp.Click, _
                                                                                           btnTyousakaisyaMeiDown.Click, _
                                                                                           btnAiteSakiSyubetuUp.Click, _
                                                                                           btnAiteSakiSyubetuDown.Click, _
                                                                                           btnAiteSakiCdUp.Click, _
                                                                                           btnAiteSakiCdDown.Click, _
                                                                                           btnAiteSakiMeiUp.Click, _
                                                                                           btnAiteSakiMeiDown.Click, _
                                                                                           btnSyouhinCdUp.Click, _
                                                                                           btnSyouhinCdDown.Click, _
                                                                                           btnSyouhinMeiUp.Click, _
                                                                                           btnSyouhinMeiDown.Click, _
                                                                                           btnTyousaHouhouUp.Click, _
                                                                                           btnTyousaHouhouDown.Click, _
                                                                                           btnTorikesiUp.Click, _
                                                                                           btnTorikesiDown.Click, _
                                                                                           btnKakaku1Up.Click, _
                                                                                           btnKakaku1Down.Click, _
                                                                                           btnKakakuHenkouFlg1Up.Click, _
                                                                                           btnKakakuHenkouFlg1Down.Click, _
                                                                                           btnKakaku2Up.Click, _
                                                                                           btnKakaku2Down.Click, _
                                                                                           btnKakakuHenkouFlg2Up.Click, _
                                                                                           btnKakakuHenkouFlg2Down.Click, _
                                                                                           btnKakaku3Up.Click, _
                                                                                           btnKakaku3Down.Click, _
                                                                                           btnKakakuHenkouFlg3Up.Click, _
                                                                                           btnKakakuHenkouFlg3Down.Click, _
                                                                                           btnKakaku4Up.Click, _
                                                                                           btnKakaku4Down.Click, _
                                                                                           btnKakakuHenkouFlg4Up.Click, _
                                                                                           btnKakakuHenkouFlg4Down.Click, _
                                                                                           btnKakaku5Up.Click, _
                                                                                           btnKakaku5Down.Click, _
                                                                                           btnKakakuHenkouFlg5Up.Click, _
                                                                                           btnKakakuHenkouFlg5Down.Click, _
                                                                                           btnKakaku6Up.Click, _
                                                                                           btnKakaku6Down.Click, _
                                                                                           btnKakakuHenkouFlg6Up.Click, _
                                                                                           btnKakakuHenkouFlg6Down.Click, _
                                                                                           btnKakaku7Up.Click, _
                                                                                           btnKakaku7Down.Click, _
                                                                                           btnKakakuHenkouFlg7Up.Click, _
                                                                                           btnKakakuHenkouFlg7Down.Click, _
                                                                                           btnKakaku8Up.Click, _
                                                                                           btnKakaku8Down.Click, _
                                                                                           btnKakakuHenkouFlg8Up.Click, _
                                                                                           btnKakakuHenkouFlg8Down.Click, _
                                                                                           btnKakaku9Up.Click, _
                                                                                           btnKakaku9Down.Click, _
                                                                                           btnKakakuHenkouFlg9Up.Click, _
                                                                                           btnKakakuHenkouFlg9Down.Click, _
                                                                                           btnKakaku10Up.Click, _
                                                                                           btnKakaku10Down.Click, _
                                                                                           btnKakakuHenkouFlg10Up.Click, _
                                                                                           btnKakakuHenkouFlg10Down.Click, _
                                                                                           btnKakaku11to19Up.Click, _
                                                                                           btnKakaku11to19Down.Click, _
                                                                                           btnKakakuHenkouFlg11to19Up.Click, _
                                                                                           btnKakakuHenkouFlg11to19Down.Click, _
                                                                                           btnKakaku21to29Up.Click, _
                                                                                           btnKakaku21to29Down.Click, _
                                                                                           btnKakakuHenkouFlg21to29Up.Click, _
                                                                                           btnKakakuHenkouFlg21to29Down.Click, _
                                                                                           btnKakaku31to39Up.Click, _
                                                                                           btnKakaku31to39Down.Click, _
                                                                                           btnKakakuHenkouFlg31to39Up.Click, _
                                                                                           btnKakakuHenkouFlg31to39Down.Click, _
                                                                                           btnKakaku41to49Up.Click, _
                                                                                           btnKakaku41to49Down.Click, _
                                                                                           btnKakakuHenkouFlg41to49Up.Click, _
                                                                                           btnKakakuHenkouFlg41to49Down.Click, _
                                                                                           btnKakaku50Up.Click, _
                                                                                           btnKakaku50Down.Click, _
                                                                                           btnKakakuHenkouFlg50Up.Click, _
                                                                                           btnKakakuHenkouFlg50Down.Click

        Dim strSort As String = String.Empty
        Dim strUpDown As String = String.Empty

        '�����N�{�^���̐F��ݒ�
        Call Me.setUpDownColor()

        '��ʂɃ\�[�g�����N���b�N��
        Select Case CType(sender, LinkButton).ID
            Case btnTyousakaisyaCdUp.ID
                strSort = "tys_kaisya_cd"
                strUpDown = "ASC"
                btnTyousakaisyaCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnTyousakaisyaCdDown.ID
                strSort = "tys_kaisya_cd"
                strUpDown = "DESC"
                btnTyousakaisyaCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnTyousakaisyaMeiUp.ID
                strSort = "tys_kaisya_mei"
                strUpDown = "ASC"
                btnTyousakaisyaMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnTyousakaisyaMeiDown.ID
                strSort = "tys_kaisya_mei"
                strUpDown = "DESC"
                btnTyousakaisyaMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnAiteSakiSyubetuUp.ID
                strSort = "aitesaki_syubetu"
                strUpDown = "ASC"
                btnAiteSakiSyubetuUp.ForeColor = Drawing.Color.IndianRed
            Case btnAiteSakiSyubetuDown.ID
                strSort = "aitesaki_syubetu"
                strUpDown = "DESC"
                btnAiteSakiSyubetuDown.ForeColor = Drawing.Color.IndianRed
            Case btnAiteSakiCdUp.ID
                strSort = "aitesaki_cd"
                strUpDown = "ASC"
                btnAiteSakiCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnAiteSakiCdDown.ID
                strSort = "aitesaki_cd"
                strUpDown = "DESC"
                btnAiteSakiCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnAiteSakiMeiUp.ID
                strSort = "aitesaki_mei"
                strUpDown = "ASC"
                btnAiteSakiMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnAiteSakiMeiDown.ID
                strSort = "aitesaki_mei"
                strUpDown = "DESC"
                btnAiteSakiMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSyouhinCdUp.ID
                strSort = "syouhin_cd"
                strUpDown = "ASC"
                btnSyouhinCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnSyouhinCdDown.ID
                strSort = "syouhin_cd"
                strUpDown = "DESC"
                btnSyouhinCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnSyouhinMeiUp.ID
                strSort = "syouhin_mei"
                strUpDown = "ASC"
                btnSyouhinMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSyouhinMeiDown.ID
                strSort = "syouhin_mei"
                strUpDown = "DESC"
                btnSyouhinMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnTyousaHouhouUp.ID
                strSort = "tys_houhou_no"
                strUpDown = "ASC"
                btnTyousaHouhouUp.ForeColor = Drawing.Color.IndianRed
            Case btnTyousaHouhouDown.ID
                strSort = "tys_houhou_no"
                strUpDown = "DESC"
                btnTyousaHouhouDown.ForeColor = Drawing.Color.IndianRed
            Case btnTorikesiUp.ID
                strSort = "torikesi"
                strUpDown = "ASC"
                btnTorikesiUp.ForeColor = Drawing.Color.IndianRed
            Case btnTorikesiDown.ID
                strSort = "torikesi"
                strUpDown = "DESC"
                btnTorikesiDown.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku1Up.ID
                strSort = "tou_kkk1"
                strUpDown = "ASC"
                btnKakaku1Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku1Down.ID
                strSort = "tou_kkk1"
                strUpDown = "DESC"
                btnKakaku1Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg1Up.ID
                strSort = "tou_kkk_henkou_flg1"
                strUpDown = "ASC"
                btnKakakuHenkouFlg1Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg1Down.ID
                strSort = "tou_kkk_henkou_flg1"
                strUpDown = "DESC"
                btnKakakuHenkouFlg1Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku2Up.ID
                strSort = "tou_kkk2"
                strUpDown = "ASC"
                btnKakaku2Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku2Down.ID
                strSort = "tou_kkk2"
                strUpDown = "DESC"
                btnKakaku2Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg2Up.ID
                strSort = "tou_kkk_henkou_flg2"
                strUpDown = "ASC"
                btnKakakuHenkouFlg2Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg2Down.ID
                strSort = "tou_kkk_henkou_flg2"
                strUpDown = "DESC"
                btnKakakuHenkouFlg2Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku3Up.ID
                strSort = "tou_kkk3"
                strUpDown = "ASC"
                btnKakaku3Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku3Down.ID
                strSort = "tou_kkk3"
                strUpDown = "DESC"
                btnKakaku3Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg3Up.ID
                strSort = "tou_kkk_henkou_flg3"
                strUpDown = "ASC"
                btnKakakuHenkouFlg3Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg3Down.ID
                strSort = "tou_kkk_henkou_flg3"
                strUpDown = "DESC"
                btnKakakuHenkouFlg3Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku4Up.ID
                strSort = "tou_kkk4"
                strUpDown = "ASC"
                btnKakaku4Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku4Down.ID
                strSort = "tou_kkk4"
                strUpDown = "DESC"
                btnKakaku4Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg4Up.ID
                strSort = "tou_kkk_henkou_flg4"
                strUpDown = "ASC"
                btnKakakuHenkouFlg4Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg4Down.ID
                strSort = "tou_kkk_henkou_flg4"
                strUpDown = "DESC"
                btnKakakuHenkouFlg4Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku5Up.ID
                strSort = "tou_kkk5"
                strUpDown = "ASC"
                btnKakaku5Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku5Down.ID
                strSort = "tou_kkk5"
                strUpDown = "DESC"
                btnKakaku5Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg5Up.ID
                strSort = "tou_kkk_henkou_flg5"
                strUpDown = "ASC"
                btnKakakuHenkouFlg5Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg5Down.ID
                strSort = "tou_kkk_henkou_flg5"
                strUpDown = "DESC"
                btnKakakuHenkouFlg5Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku6Up.ID
                strSort = "tou_kkk6"
                strUpDown = "ASC"
                btnKakaku6Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku6Down.ID
                strSort = "tou_kkk6"
                strUpDown = "DESC"
                btnKakaku6Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg6Up.ID
                strSort = "tou_kkk_henkou_flg6"
                strUpDown = "ASC"
                btnKakakuHenkouFlg6Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg6Down.ID
                strSort = "tou_kkk_henkou_flg6"
                strUpDown = "DESC"
                btnKakakuHenkouFlg6Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku7Up.ID
                strSort = "tou_kkk7"
                strUpDown = "ASC"
                btnKakaku7Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku7Down.ID
                strSort = "tou_kkk7"
                strUpDown = "DESC"
                btnKakaku7Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg7Up.ID
                strSort = "tou_kkk_henkou_flg7"
                strUpDown = "ASC"
                btnKakakuHenkouFlg7Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg7Down.ID
                strSort = "tou_kkk_henkou_flg7"
                strUpDown = "DESC"
                btnKakakuHenkouFlg7Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku8Up.ID
                strSort = "tou_kkk8"
                strUpDown = "ASC"
                btnKakaku8Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku8Down.ID
                strSort = "tou_kkk8"
                strUpDown = "DESC"
                btnKakaku8Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg8Up.ID
                strSort = "tou_kkk_henkou_flg8"
                strUpDown = "ASC"
                btnKakakuHenkouFlg8Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg8Down.ID
                strSort = "tou_kkk_henkou_flg8"
                strUpDown = "DESC"
                btnKakakuHenkouFlg8Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku9Up.ID
                strSort = "tou_kkk9"
                strUpDown = "ASC"
                btnKakaku9Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku9Down.ID
                strSort = "tou_kkk9"
                strUpDown = "DESC"
                btnKakaku9Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg9Up.ID
                strSort = "tou_kkk_henkou_flg9"
                strUpDown = "ASC"
                btnKakakuHenkouFlg9Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg9Down.ID
                strSort = "tou_kkk_henkou_flg9"
                strUpDown = "DESC"
                btnKakakuHenkouFlg9Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku10Up.ID
                strSort = "tou_kkk10"
                strUpDown = "ASC"
                btnKakaku10Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku10Down.ID
                strSort = "tou_kkk10"
                strUpDown = "DESC"
                btnKakaku10Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg10Up.ID
                strSort = "tou_kkk_henkou_flg10"
                strUpDown = "ASC"
                btnKakakuHenkouFlg10Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg10Down.ID
                strSort = "tou_kkk_henkou_flg10"
                strUpDown = "DESC"
                btnKakakuHenkouFlg10Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku11to19Up.ID
                strSort = "tou_kkk11t19"
                strUpDown = "ASC"
                btnKakaku11to19Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku11to19Down.ID
                strSort = "tou_kkk11t19"
                strUpDown = "DESC"
                btnKakaku11to19Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg11to19Up.ID
                strSort = "tou_kkk_henkou_flg11t19"
                strUpDown = "ASC"
                btnKakakuHenkouFlg11to19Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg11to19Down.ID
                strSort = "tou_kkk_henkou_flg11t19"
                strUpDown = "DESC"
                btnKakakuHenkouFlg11to19Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku21to29Up.ID
                strSort = "tou_kkk20t29"
                strUpDown = "ASC"
                btnKakaku21to29Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku21to29Down.ID
                strSort = "tou_kkk20t29"
                strUpDown = "DESC"
                btnKakaku21to29Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg21to29Up.ID
                strSort = "tou_kkk_henkou_flg20t29"
                strUpDown = "ASC"
                btnKakakuHenkouFlg21to29Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg21to29Down.ID
                strSort = "tou_kkk_henkou_flg20t29"
                strUpDown = "DESC"
                btnKakakuHenkouFlg21to29Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku31to39Up.ID
                strSort = "tou_kkk30t39"
                strUpDown = "ASC"
                btnKakaku31to39Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku31to39Down.ID
                strSort = "tou_kkk30t39"
                strUpDown = "DESC"
                btnKakaku31to39Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg31to39Up.ID
                strSort = "tou_kkk_henkou_flg30t39"
                strUpDown = "ASC"
                btnKakakuHenkouFlg31to39Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg31to39Down.ID
                strSort = "tou_kkk_henkou_flg30t39"
                strUpDown = "DESC"
                btnKakakuHenkouFlg31to39Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku41to49Up.ID
                strSort = "tou_kkk40t49"
                strUpDown = "ASC"
                btnKakaku41to49Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku41to49Down.ID
                strSort = "tou_kkk40t49"
                strUpDown = "DESC"
                btnKakaku41to49Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg41to49Up.ID
                strSort = "tou_kkk_henkou_flg40t49"
                strUpDown = "ASC"
                btnKakakuHenkouFlg41to49Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg41to49Down.ID
                strSort = "tou_kkk_henkou_flg40t49"
                strUpDown = "DESC"
                btnKakakuHenkouFlg41to49Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku50Up.ID
                strSort = "tou_kkk50t"
                strUpDown = "ASC"
                btnKakaku50Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakaku50Down.ID
                strSort = "tou_kkk50t"
                strUpDown = "DESC"
                btnKakaku50Down.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg50Up.ID
                strSort = "tou_kkk_henkou_flg50t"
                strUpDown = "ASC"
                btnKakakuHenkouFlg50Up.ForeColor = Drawing.Color.IndianRed
            Case btnKakakuHenkouFlg50Down.ID
                strSort = "tou_kkk_henkou_flg50t"
                strUpDown = "DESC"
                btnKakakuHenkouFlg50Down.ForeColor = Drawing.Color.IndianRed
        End Select

        '��ʃf�[�^�̃\�[�g����ݒ肷��
        Dim dvKameitenInfo As Data.DataView = CType(ViewState("dtGenkaJyouhou"), GenkaMasterDataSet.GenkaInfoTableDataTable).DefaultView
        dvKameitenInfo.Sort = strSort & " " & strUpDown

        Me.grdBodyLeft.DataSource = dvKameitenInfo
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = dvKameitenInfo
        Me.grdBodyRight.DataBind()

        '��ʏc�X�N���[����ݒ肷��
        scrollHeight = ViewState("scrollHeight")
        '��ʉ��X�N���[���ʒu��ݒ肷��
        SetScroll()

    End Sub




    ''' <summary>Javascript�쐬</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
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
            .AppendLine("	 ")
            .AppendLine("	function LTrim(str)  ")
            .AppendLine("	{  ")
            .AppendLine("		var i; ")
            .AppendLine("		for(i=0;i<str.length;i++)  ")
            .AppendLine("		{  ")
            .AppendLine("			if(str.charAt(i)!=' '&&str.charAt(i)!='�@')break;  ")
            .AppendLine("		}  ")
            .AppendLine("		str=str.substring(i,str.length);  ")
            .AppendLine("		return str;  ")
            .AppendLine("	}  ")
            .AppendLine("	function RTrim(str)  ")
            .AppendLine("	{  ")
            .AppendLine("		var i;  ")
            .AppendLine("		for(i=str.length-1;i>=0;i--)  ")
            .AppendLine("		{  ")
            .AppendLine("			if(str.charAt(i)!=' '&&str.charAt(i)!='�@')break;  ")
            .AppendLine("		}  ")
            .AppendLine("		str=str.substring(0,i+1);  ")
            .AppendLine("		return str;  ")
            .AppendLine("	} ")
            .AppendLine("	function Trim(str)  ")
            .AppendLine("	{  ")
            .AppendLine("		return LTrim(RTrim(str));  ")
            .AppendLine("	}  ")
            .AppendLine("	 ")
            '�N���A�{�^������
            .AppendLine("function fncClear(){")
            '�������
            .AppendLine("   document.all." & Me.tbxTyousaKaisyaCd.ClientID & ".value = ''")
            .AppendLine("   document.all." & Me.tbxTyousaKaisyaMei.ClientID & ".value = ''")
            '�������
            .AppendLine("   document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value = ''")
            '�����R�[�h
            .AppendLine("   document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".value = '';")
            .AppendLine("   document.all." & Me.tbxAiteSakiMeiFrom.ClientID & ".value = '';")
            .AppendLine("   document.all." & Me.tbxAiteSakiCdTo.ClientID & ".value = '';")
            .AppendLine("   document.all." & Me.tbxAiteSakiMeiTo.ClientID & ".value = '';")
            '�����R�[�h��\�����Ȃ�
            .AppendLine("   document.all." & Me.divAitesaki.ClientID & ".style.display = 'none';")
            '���i�R�[�h
            .AppendLine("   document.all." & Me.ddlSyouhinCd.ClientID & ".value = '';")
            '�������@
            .AppendLine("   document.all." & Me.ddlTyousaHouhou.ClientID & ".value = '';")
            '�����������
            .AppendLine("   document.all." & Me.ddlKensakuJyouken.ClientID & ".value = '100';")
            '�������s
            .AppendLine("   document.all." & Me.btnKensakujiltukou.ClientID & ".disabled = false;")
            '����͌����ΏۊO
            .AppendLine("   document.all." & Me.chkKensakuTaisyouGai.ClientID & ".checked = true;")
            '��������͑ΏۊO
            .AppendLine("   document.all." & Me.chkAitesakiTaisyouGai.ClientID & ".checked = true;")
            '���ݒ���܂�
            .AppendLine("   document.all." & Me.chkMiseltutei.ClientID & ".checked = false;")

            .AppendLine("   return false;")
            .AppendLine("}")

            '����挟���\����ݒ肷��
            .AppendLine("function fncSetAitesaki(){")
            .AppendLine("   if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '0'||document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == ''||document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '3'){")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.divAitesaki.ClientID & ".style.display = 'none';")
            .AppendLine("   }else{")
            .AppendLine("       document.all." & Me.divAitesaki.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiTo.ClientID & ".value = '';")
            .AppendLine("   }")
            .AppendLine("}")

            '�u���ݒ���܂ށv�`�F�b�N�{�b�N�X����
            .AppendLine("function fncChange(){")
            .AppendLine("   if(document.all." & Me.chkMiseltutei.ClientID & ".checked){")
            .AppendLine("       document.all." & Me.btnKensakujiltukou.ClientID & ".disabled = true;")
            .AppendLine("   }else{")
            .AppendLine("       document.all." & Me.btnKensakujiltukou.ClientID & ".disabled = false;")
            .AppendLine("   }")
            .AppendLine("}")

            '���̓`�F�b�N
            .AppendLine("   function fncNyuuryokuCheck(strKbn){")
            .AppendLine("       fncSetCsvOut();")
            '������ʂ��K�{���̓`�F�b�N
            .AppendLine("       if (Trim(document.all." & Me.tbxTyousaKaisyaCd.ClientID & ".value) =='')")
            .AppendLine("       {")
            .AppendLine("           if (document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".selectedIndex=='0'){")
            .AppendLine("               alert('" & Messages.Instance.MSG041E.Replace("@PARAM1", "������ЃR�[�h").Replace("@PARAM2", "�������") & "');")
            .AppendLine("               document.all." & Me.tbxTyousaKaisyaCd.ClientID & ".value='';")
            .AppendLine("               document.all." & Me.tbxTyousaKaisyaCd.ClientID & ".focus();")
            .AppendLine("               return false; ")
            .AppendLine("           }")
            .AppendLine("       }")
            '�u�����R�[�hFROM�v�A�u�����R�[�hTO�v�̂��Âꂩ�͓��͕K�{
            .AppendLine("       if ((document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value!='') && (document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value!='0' && document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value!='3')){")
            .AppendLine("           if(fncNyuuryokuHissu(document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".value)==false && fncNyuuryokuHissu(document.all." & Me.tbxAiteSakiCdTo.ClientID & ".value)==false){")
            .AppendLine("               alert('" & Messages.Instance.MSG041E.Replace("@PARAM1", "�����R�[�hFROM").Replace("@PARAM2", "�����R�[�hTO") & "');")
            .AppendLine("               document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".focus();")
            .AppendLine("               return false; ")
            .AppendLine("           }")
            .AppendLine("       }")
            '�m�F���b�Z�[�W�\��
            .AppendLine("       if(strKbn == 'Kensaku'){")
            .AppendLine("           if (document.all." & Me.ddlKensakuJyouken.ClientID & ".value=='max'){")
            .AppendLine("               if(!confirm('" & Messages.Instance.MSG007C & "')){")
            .AppendLine("                   return false; ")
            .AppendLine("               }")
            .AppendLine("           }")
            .AppendLine("       }")
            .AppendLine("   return true;")
            .AppendLine("   }")
            '���͕K�{�`�F�b�N:�����́A�X�y�[�X�݂̂Ȃ�G���[�\��
            .AppendLine("function fncNyuuryokuHissu(strValue) {")
            .AppendLine("    var wkflg = 0;")
            .AppendLine("    var wkdata = strValue;")
            .AppendLine("    for (i = 0; i < wkdata.length; i++) {")
            .AppendLine("        if (wkdata.charAt(i) != " & """" & " " & """" & ") {")
            .AppendLine("           if (wkdata.charAt(i) != " & """" & "  " & """" & ") {")
            .AppendLine("               wkflg = 1;")
            .AppendLine("           }")
            .AppendLine("        }")
            .AppendLine("    }")
            .AppendLine("    if (wkflg == 0) {")
            .AppendLine("        return false;")
            .AppendLine("    }")
            .AppendLine("    return true;")
            .AppendLine("}")

            'CSV�o�͋敪��ݒ肷��
            .AppendLine("function fncSetCsvOut(){")
            .AppendLine("   document.all." & Me.hidCsvOut.ClientID & ".value='';")
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
            .AppendLine("   }else if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '5'){")
            '������ʂ��u5:�c�Ə��v�̏ꍇ�A�c�Ə��|�b�v�A�b�v���N������
            .AppendLine("       var strkbn='�c�Ə�';")
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

            '������ЃR�[�h��������������ꍇ�A�|�b�v�A�b�v���N������
            .AppendLine("function fncTyousaKaisyaSearch(){")
            .AppendLine("       var strkbn='�������';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       strClientCdID = '" & Me.tbxTyousaKaisyaCd.ClientID & "';")
            .AppendLine("       strClientMeiID = '" & Me.tbxTyousaKaisyaMei.ClientID & "';")
            '.AppendLine("       if(document.all." & Me.chkKensakuTaisyouGai.ClientID & ".checked){")
            '.AppendLine("           blnTorikesi = 'True';")
            '.AppendLine("       }else{")
            .AppendLine("           blnTorikesi = 'False';")
            '.AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_tyousa.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&objCd2=&show=False&blnDelete='+blnTorikesi+'&strCd='+escape(document.getElementById(strClientCdID).value),'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("} ")

            'DIV�\��
            .AppendLine("   function fncShowModal(){")
            .AppendLine("      var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("      var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("      if(buyDiv.style.display=='none')")
            .AppendLine("      {")
            .AppendLine("       buyDiv.style.display='';")
            .AppendLine("       disable.style.display='';")
            .AppendLine("       disable.focus();")
            .AppendLine("      }else{")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("      }")
            .AppendLine("   }")
            'DIV��\��
            .AppendLine("   function fncClosecover(){")
            .AppendLine("       var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("       var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("   }")

            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub

    ''' <summary>���̓`�F�b�N</summary>
    ''' <param name="strObjId">�N���C�A���gID</param>
    ''' <returns>�G���[���b�Z�[�W</returns>
    Public Function CheckInput(ByRef strObjId As String) As String
        Dim csScript As New StringBuilder
        With csScript
            '������ЃR�[�h(�p�����`�F�b�N)
            If Me.tbxTyousaKaisyaCd.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxTyousaKaisyaCd.Text, "������ЃR�[�h"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxTyousaKaisyaCd.ClientID
                End If
            End If
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

    ''' <summary>DIV��\��</summary>
    Public Sub CloseCover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat("fncClosecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseCover", csScript.ToString, True)
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


    Private Sub grdBodyLeft_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyLeft.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
        End If


    End Sub

    Private Sub grdBodyRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyRight.RowDataBound
        Dim numIndex() As Integer = {1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29}

        If e.Row.RowType = DataControlRowType.DataRow Then
            For Each i As Integer In numIndex
                If e.Row.Cells(i).Text.Trim.Equals("&nbsp;") Then
                    e.Row.Cells(i).Text = String.Empty
                Else
                    e.Row.Cells(i).Text = FormatNumber(e.Row.Cells(i).Text.Trim, 0)
                End If
            Next
        End If
    End Sub
End Class