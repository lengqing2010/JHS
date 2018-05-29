Imports Itis.Earth.BizLogic

Partial Public Class TokubetuTaiouMasterSearchList
    Inherits System.Web.UI.Page

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^�Ɖ�</summary>
    ''' <remarks>�����X���i�������@���ʑΉ��}�X�^�Ɖ�p�@�\��񋟂���</remarks>
    ''' <history>
    ''' <para>2011/03/03�@�W���o�t(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private tokubetuTaiouMasterLogic As New TokubetuTaiouMasterLogic
    Private commonCheck As New CommonCheck
    Private commonSearchLogic As New CommonSearchLogic
    Protected scrollHeight As Integer = 0

    ''' <summary>�y�[�W���b�h</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '�����`�F�b�N
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnEigyouKengen As Boolean
        blnEigyouKengen = commonChk.CommonNinnsyou(strUserID, "eigyou_master_kanri_kengen,kaiseki_master_kanri_kengen")

        'JavaScript���쐬
        MakeJavaScript()

        If Not IsPostBack Then

            '�Q�Ɨ����Ǘ��e�[�u����o�^����B
            commonCheck.SetURL(Me, strUserID)

            If Not Request.QueryString("sendSearchTerms") Is Nothing Then
                '������
                Call Syokika(CStr(Request.QueryString("sendSearchTerms")))
            Else
                '������
                Call Syokika(String.Empty)
            End If
        Else
            '��������(�����R�[�h)��ݒ肷��
            If Me.ddlAitesakiSyubetu.SelectedValue <> "0" AndAlso Me.ddlAitesakiSyubetu.SelectedValue <> String.Empty Then
                Me.divAitesakiSyubetu.Attributes.Add("style", "display:block;")
            Else
                Me.divAitesakiSyubetu.Attributes.Add("style", "display:none;")
            End If

            If Me.ddlAitesakiSyubetu.SelectedValue = "1" Then
                '����悪�����X�̏ꍇ

                '���ݒ聁�`�F�b�N
                If Not Me.chkMisetteimo.Checked Then

                    '�u�n��E�c�Ə��E�w�薳���������Ώہv�`�F�b�N�{�b�N�X�\��
                    Me.divSitei.Style.Add("display", "block")

                    '�u�n��E�c�Ə��E�w�薳���������Ώہv�`�F�b�N�{�b�N�X���`�F�b�N
                    If Me.chkSiteiNasiTaisyou.Checked Then

                        '�����R�[�hTO�e�L�X�g�{�b�N�X�{�����{�^��+�uCSV�o�́v�{�^����\��
                        Me.divAitesakiTo.Style.Add("display", "none")
                        Me.tbxKameitenCdTo.Text = String.Empty
                        Me.tbxKameitenMeiTo.Text = String.Empty
                        Me.divCsvOutput.Style.Add("display", "none")

                    Else
                        '�����R�[�hTO�e�L�X�g�{�b�N�X�{�����{�^��+�uCSV�o�́v�{�^���\��
                        Me.divAitesakiTo.Style.Add("display", "block")
                        Me.divCsvOutput.Style.Add("display", "block")
                    End If

                Else
                    '�u�n��E�c�Ə��E�w�薳���������Ώہv�`�F�b�N�{�b�N�X��\��
                    Me.divSitei.Style.Add("display", "none")
                    Me.chkSiteiNasiTaisyou.Checked = False

                    '�����R�[�hTO�e�L�X�g�{�b�N�X�{�����{�^��+�uCSV�o�́v�{�^���\��
                    Me.divAitesakiTo.Style.Add("display", "block")
                    Me.divCsvOutput.Style.Add("display", "block")
                End If

            Else
                '����悪�����X�ȊO�̏ꍇ

                '�u�n��E�c�Ə��E�w�薳���������Ώہv�`�F�b�N�{�b�N�X��\��
                Me.divSitei.Style.Add("display", "none")
                Me.chkSiteiNasiTaisyou.Checked = False

                '�����R�[�hTO�e�L�X�g�{�b�N�X�{�����{�^��+�uCSV�o�́v�{�^���\��
                Me.divAitesakiTo.Style.Add("display", "block")
                Me.divCsvOutput.Style.Add("display", "block")

            End If

            '�����{�^���̏���
            Me.btnKensaku.Enabled = True
            If Me.chkMisetteimo.Checked = True Then
                Me.btnKensaku.Enabled = False
            End If

            'CSV�t�@�C���_�E�����[�h
            If Me.hidCSVFlg.Value = "1" Then
                MakeCSVFile()
            End If

            'DIV��\��
            CloseCover()
        End If

        'CSV�捞�{�^����ݒ肷��
        If blnEigyouKengen Then
            Me.btnCSVInput.Enabled = True
        Else
            Me.btnCSVInput.Enabled = False
        End If

        '������Ԑݒu
        If ddlAitesakiSyubetu.SelectedValue.Trim.Equals("0") OrElse String.IsNullOrEmpty(ddlAitesakiSyubetu.SelectedValue.Trim) Then
            Me.tbxKameitenCdFrom.Text = String.Empty
            Me.tbxKameitenMeiFrom.Text = String.Empty
            Me.tbxKameitenCdTo.Text = String.Empty
            Me.tbxKameitenMeiTo.Text = String.Empty
            Me.divAitesakiSyubetu.Attributes.Add("style", "display:none;")
        Else
            Me.divAitesakiSyubetu.Attributes.Add("style", "display:block;")
        End If

        '���ʑΉ����͕̂\���̂�
        Me.tbxTokubetuTaiouMei.Attributes.Add("readonly", "true")
        '�����X���͕\���̂�
        Me.tbxKameitenMeiFrom.Attributes.Add("readonly", "true")
        '�����X���͕\���̂�
        Me.tbxKameitenMeiTo.Attributes.Add("readonly", "true")
        '����飃{�^��
        Me.btnClose.Attributes.Add("onclick", "fncClose();return false;")
        '�u�����i���ʑΉ��j�v�{�^��
        Me.btnSearch.Attributes.Add("onclick", "fncSetTokubetuKaiou();return false;")
        ''�u�����i�����X�R�[�hFrom�j�{�^���v
        'Me.btnKameitenSearchFrom.Attributes.Add("onClick", "fncSetKameiten('" & Me.tbxKameitenCdFrom.ClientID & "',1,'" & Me.tbxKameitenMeiFrom.ClientID & "');return false;")
        ''�u�����i�����X�R�[�hTo�j�{�^���v
        'Me.btnKameitenSearchTo.Attributes.Add("onClick", "fncSetKameiten('" & Me.tbxKameitenCdTo.ClientID & "',2,'" & Me.tbxKameitenMeiTo.ClientID & "');return false;")
        '�����FROM�̢������{�^��
        Me.btnKameitenSearchFrom.Attributes.Add("onClick", "fncAiteSakiSearch('1');return false;")
        '�����TO�̢������{�^��
        Me.btnKameitenSearchTo.Attributes.Add("onClick", "fncAiteSakiSearch('2');return false;")

        '�u�N���A�v�{�^��
        Me.btnClear.Attributes.Add("onClick", "fncClear();return false;")
        '�����X�R�[�h���͂��ǂ����`�F�b�N
        Me.btnKensaku.Attributes.Add("onClick", "fncSetHidCSV();if(! fncKameitenCdChk('kensaku')){return false;}else{fncShowModal();}")
        Me.btnCSVOutput.Attributes.Add("onClick", "fncSetHidCSV();if(! fncKameitenCdChk('csv')){return false;}else{fncShowModal();}")
        'CSV�捞��ʂ��|�b�v�A�b�v����
        Me.btnCSVInput.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV();fncCSVTorikomi();fncClosecover();return false;")
        '�u���ݒ���܂ށv�`�F�b�N�{�b�N�X
        Me.chkMisetteimo.Attributes.Add("onClick", "fncSetbtnKensaku()")
        '�u�n��E�c�Ə��E�w�薳���������Ώہv�`�F�b�N�{�b�N�X��ύX����ꍇ
        Me.chkSiteiNasiTaisyou.Attributes.Add("onClick", "return fncChangeAite();")
        Call SetButton()
        '������ʂ��ύX����ꍇ�A�����R�[�h����������ݒ肷��
        Me.ddlAitesakiSyubetu.Attributes.Add("onChange", "return fncSetAitesaki();")

        '��ʏc�X�N���[����ݒ肷��
        scrollHeight = ViewState("scrollHeight")

    End Sub

    ''' <summary>������</summary> 
    Private Sub Syokika(ByVal sendSearchTerms As String)

        If Not sendSearchTerms.Equals(String.Empty) Then
            Dim arrSearchTerm() As String = Split(sendSearchTerms, "$$$")
            '������ʂ�ݒ�
            Call setAitesakiSyubetu(arrSearchTerm(0))

            '============2012/05/23 �ԗ� 407553�̑Ή� �ǉ���=========================
            If Me.ddlAitesakiSyubetu.SelectedValue.Trim.Equals("1") Then
                '�u�n��E�c�Ə��E�w�薳���������Ώہv�`�F�b�N�{�b�N�X�\��
                Me.divSitei.Style.Add("display", "block")
            End If
            '============2012/05/23 �ԗ� 407553�̑Ή� �ǉ���=========================


            '-------------------From 2013.03.09���F�ǉ�------------------------
            '�u�n��E�c�Ə��E�w�薳���������Ώہv�`�F�b�N = �`�F�b�N
            If Me.chkSiteiNasiTaisyou.Checked Then
                '�����R�[�hTO�e�L�X�g�{�b�N�X�{�����{�^��+�uCSV�o�́v�{�^����\��
                Me.divAitesakiTo.Style.Add("display", "none")
                Me.tbxKameitenCdTo.Text = String.Empty
                Me.tbxKameitenMeiTo.Text = String.Empty
                Me.divCsvOutput.Style.Add("display", "none")
            End If
            '-------------------To   2013.03.09���F�ǉ�------------------------

            '�����X�R�[�hFrom
            Me.tbxKameitenCdFrom.Text = arrSearchTerm(1)

            '���i�R�[�h��ݒ�
            If arrSearchTerm.Length > 2 Then
                Call SetSyouhinCd(arrSearchTerm(2))
            Else
                Call SetSyouhinCd(String.Empty)
            End If

            '�������@
            If arrSearchTerm.Length > 3 Then
                Call SetTyousaHouhou(arrSearchTerm(3))
            Else
                Call SetTyousaHouhou(String.Empty)
            End If

            '���ʑΉ��R�[�h
            If arrSearchTerm.Length > 4 Then
                Me.tbxTokubetuTaiouCd.Text = arrSearchTerm(4)
            Else
                Me.tbxTokubetuTaiouCd.Text = String.Empty
            End If

            '�����X���Ɠ��ʑΉ�����ݒ肷��
            'Call Me.SetGamenData()
            Call Me.SetGamenData(Me.ddlAitesakiSyubetu.SelectedValue.Trim, Me.tbxKameitenCdFrom.Text.Trim, Me.tbxKameitenCdTo.Text.Trim, IIf(Me.chkTorikesi.Checked, "1", String.Empty))
        Else
            '������ʂ�ݒ�
            Call setAitesakiSyubetu(String.Empty)
            '�����X�R�[�hFrom
            Me.tbxKameitenCdFrom.Text = String.Empty
            '�����X��From
            Me.tbxKameitenMeiFrom.Text = String.Empty
            '�����X�R�[�hTo
            Me.tbxKameitenCdTo.Text = String.Empty
            '�����X��To
            Me.tbxKameitenMeiTo.Text = String.Empty
            '���i�R�[�h��ݒ�
            Call SetSyouhinCd(String.Empty)
            '�������@
            Call SetTyousaHouhou(String.Empty)
            '���ʑΉ��R�[�h
            Me.tbxTokubetuTaiouCd.Text = String.Empty
            '���ʑΉ���
            Me.tbxTokubetuTaiouMei.Text = String.Empty
        End If

        '����͌����ΏۊO
        Me.chkTorikesi.Checked = True
        '���ݒ���܂�
        Me.chkMisetteimo.Checked = False

        '�����N�{�^���̕\����ݒ�
        Call Me.SetUpDownHyouji(False)

        '��������
        Me.grdMeisaiLeft.DataSource = Nothing
        Me.grdMeisaiLeft.DataBind()
        Me.grdMeisaiRight.DataSource = Nothing
        Me.grdMeisaiRight.DataBind()
    End Sub

    ''' <summary>������ʂ�ݒ�</summary>
    Private Sub setAitesakiSyubetu(ByVal strAitesakiCd As String)
        Dim dtAitesakiSyubetu As New Data.DataTable
        dtAitesakiSyubetu = tokubetuTaiouMasterLogic.GetAitesakiSyubetuList()
        Me.ddlAitesakiSyubetu.DataValueField = "value"
        Me.ddlAitesakiSyubetu.DataTextField = "name"
        Me.ddlAitesakiSyubetu.DataSource = dtAitesakiSyubetu
        Me.ddlAitesakiSyubetu.DataBind()

        '���i�R�[�h�̐擪�s�͋󗓂��Z�b�g����
        Me.ddlAitesakiSyubetu.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        If String.IsNullOrEmpty(strAitesakiCd) Then
            Me.ddlAitesakiSyubetu.SelectedValue = String.Empty
        Else
            Try
                Me.ddlAitesakiSyubetu.SelectedValue = strAitesakiCd
            Catch ex As Exception
                Me.ddlAitesakiSyubetu.SelectedValue = String.Empty
            End Try
        End If
    End Sub

    ''' <summary>���i�R�[�h��ݒ�</summary>
    Private Sub SetSyouhinCd(ByVal strSyouhinCd As String)
        '���i�R�[�h�f�[�^���擾
        Dim dtSyouhinCd As New Data.DataTable
        dtSyouhinCd = tokubetuTaiouMasterLogic.GetSyouhinCd()

        '���i�R�[�h
        Me.ddlSyouhinCd.DataValueField = "syouhin_cd"
        Me.ddlSyouhinCd.DataTextField = "syouhin_mei"
        Me.ddlSyouhinCd.DataSource = dtSyouhinCd
        Me.ddlSyouhinCd.DataBind()

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
        dtTyousaHouhou = tokubetuTaiouMasterLogic.GetTyousaHouhou()

        '�������@
        Me.ddlTyousaHouhou.DataValueField = "tys_houhou_no"
        Me.ddlTyousaHouhou.DataTextField = "tys_houhou_mei"
        Me.ddlTyousaHouhou.DataSource = dtTyousaHouhou
        Me.ddlTyousaHouhou.DataBind()

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

    ''' <summary>�����N�{�^���̐F��ݒ�</summary>
    Public Sub setUpDownColor()
        Me.btnSortAitesakiSyubetuUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortAitesakiSyubetuDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortAitesakiCdUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortAitesakiCdDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortAitesakiMeiUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortAitesakiMeiDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortSyouhinCdUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortSyouhinCdDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortSyouhinMeiUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortSyouhinMeiDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortTyousaUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortTyousaDown.ForeColor = Drawing.Color.SkyBlue
        'Me.btnSortTokubetuCdUp.ForeColor = Drawing.Color.SkyBlue
        'Me.btnSortTokubetuCdDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortTokubetuMeiUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortTokubetuMeiDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortTorikesiUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortTorikesiDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortKingakuAddScdUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortKingakuAddScdDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortSyokiTiUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortSyokiTiDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortRequestAddKingakuUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortRequestAddKingakuDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortKoumuAddKingakuUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortKoumuAddKingakuDown.ForeColor = Drawing.Color.SkyBlue
    End Sub

    ''' <summary>�����N�{�^���̕\����ݒ�</summary>
    Public Sub SetUpDownHyouji(ByVal blnHyoujiFlg As Boolean)
        If blnHyoujiFlg = True Then
            Me.btnSortAitesakiSyubetuUp.Visible = True
            Me.btnSortAitesakiSyubetuDown.Visible = True
            Me.btnSortAitesakiCdUp.Visible = True
            Me.btnSortAitesakiCdDown.Visible = True
            Me.btnSortAitesakiMeiUp.Visible = True
            Me.btnSortAitesakiMeiDown.Visible = True
            Me.btnSortSyouhinCdUp.Visible = True
            Me.btnSortSyouhinCdDown.Visible = True
            Me.btnSortSyouhinMeiUp.Visible = True
            Me.btnSortSyouhinMeiDown.Visible = True
            Me.btnSortTyousaUp.Visible = True
            Me.btnSortTyousaDown.Visible = True
            'Me.btnSortTokubetuCdUp.Visible = True
            'Me.btnSortTokubetuCdDown.Visible = True
            Me.btnSortTokubetuMeiUp.Visible = True
            Me.btnSortTokubetuMeiDown.Visible = True
            Me.btnSortTorikesiUp.Visible = True
            Me.btnSortTorikesiDown.Visible = True
            Me.btnSortKingakuAddScdUp.Visible = True
            Me.btnSortKingakuAddScdDown.Visible = True
            Me.btnSortSyokiTiUp.Visible = True
            Me.btnSortSyokiTiDown.Visible = True
            Me.btnSortRequestAddKingakuUp.Visible = True
            Me.btnSortRequestAddKingakuDown.Visible = True
            Me.btnSortKoumuAddKingakuUp.Visible = True
            Me.btnSortKoumuAddKingakuDown.Visible = True
        Else
            Me.btnSortAitesakiSyubetuUp.Visible = False
            Me.btnSortAitesakiSyubetuDown.Visible = False
            Me.btnSortAitesakiCdUp.Visible = False
            Me.btnSortAitesakiCdDown.Visible = False
            Me.btnSortAitesakiMeiUp.Visible = False
            Me.btnSortAitesakiMeiDown.Visible = False
            Me.btnSortSyouhinCdUp.Visible = False
            Me.btnSortSyouhinCdDown.Visible = False
            Me.btnSortSyouhinMeiUp.Visible = False
            Me.btnSortSyouhinMeiDown.Visible = False
            Me.btnSortTyousaUp.Visible = False
            Me.btnSortTyousaDown.Visible = False
            'Me.btnSortTokubetuCdUp.Visible = False
            'Me.btnSortTokubetuCdDown.Visible = False
            Me.btnSortTokubetuMeiUp.Visible = False
            Me.btnSortTokubetuMeiDown.Visible = False
            Me.btnSortTorikesiUp.Visible = False
            Me.btnSortTorikesiDown.Visible = False
            Me.btnSortKingakuAddScdUp.Visible = False
            Me.btnSortKingakuAddScdDown.Visible = False
            Me.btnSortSyokiTiUp.Visible = False
            Me.btnSortSyokiTiDown.Visible = False
            Me.btnSortRequestAddKingakuUp.Visible = False
            Me.btnSortRequestAddKingakuDown.Visible = False
            Me.btnSortKoumuAddKingakuUp.Visible = False
            Me.btnSortKoumuAddKingakuDown.Visible = False
        End If
    End Sub

    ''' <summary>�������׍��ڂ̃\�[�g����</summary> 
    Private Sub btnSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSortAitesakiSyubetuUp.Click _
                                                                                            , btnSortAitesakiSyubetuDown.Click _
                                                                                            , btnSortAitesakiCdUp.Click _
                                                                                            , btnSortAitesakiCdDown.Click _
                                                                                            , btnSortAitesakiMeiUp.Click _
                                                                                            , btnSortAitesakiMeiDown.Click _
                                                                                            , btnSortSyouhinCdUp.Click _
                                                                                            , btnSortSyouhinCdDown.Click _
                                                                                            , btnSortSyouhinMeiUp.Click _
                                                                                            , btnSortSyouhinMeiDown.Click _
                                                                                            , btnSortTyousaUp.Click _
                                                                                            , btnSortTyousaDown.Click _
                                                                                            , btnSortTokubetuMeiUp.Click _
                                                                                            , btnSortTokubetuMeiDown.Click _
                                                                                            , btnSortTorikesiUp.Click _
                                                                                            , btnSortTorikesiDown.Click _
                                                                                            , btnSortKingakuAddScdUp.Click _
                                                                                            , btnSortKingakuAddScdDown.Click _
                                                                                            , btnSortSyokiTiUp.Click _
                                                                                            , btnSortSyokiTiDown.Click _
                                                                                            , btnSortRequestAddKingakuUp.Click _
                                                                                            , btnSortRequestAddKingakuDown.Click _
                                                                                            , btnSortKoumuAddKingakuUp.Click _
                                                                                            , btnSortKoumuAddKingakuDown.Click
        Dim strSort As String = String.Empty
        Dim strUpDown As String = String.Empty

        '�����N�{�^���̐F��ݒ�
        Call Me.setUpDownColor()

        '��ʂɃ\�[�g�����N���b�N��
        Select Case CType(sender, LinkButton).ID
            Case btnSortAitesakiSyubetuUp.ID     '--������ʂŏ����\�[�g
                strSort = "aitesaki_syubetu"
                strUpDown = "ASC"
                btnSortAitesakiSyubetuUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortAitesakiSyubetuDown.ID   '--������ʂō~���\�[�g
                strSort = "aitesaki_syubetu"
                strUpDown = "DESC"
                btnSortAitesakiSyubetuDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortAitesakiCdUp.ID    '--�����R�[�h�ŏ����\�[�g
                strSort = "aitesaki_cd"
                strUpDown = "ASC"
                btnSortAitesakiCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortAitesakiCdDown.ID  '--�����R�[�h�ō~���\�[�g
                strSort = "aitesaki_cd"
                strUpDown = "DESC"
                btnSortAitesakiCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortAitesakiMeiUp.ID    '--����於�ŏ����\�[�g
                strSort = "aitesaki_name"
                strUpDown = "ASC"
                btnSortAitesakiMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortAitesakiMeiDown.ID  '--����於�ō~���\�[�g
                strSort = "aitesaki_name"
                strUpDown = "DESC"
                btnSortAitesakiMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortSyouhinCdUp.ID  '--���i�R�[�h�ŏ����\�[�g
                strSort = "syouhin_cd"
                strUpDown = "ASC"
                btnSortSyouhinCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortSyouhinCdDown.ID    '--���i�R�[�h�ō~���\�[�g
                strSort = "syouhin_cd"
                strUpDown = "DESC"
                btnSortSyouhinCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortSyouhinMeiUp.ID '--���i���̂ŏ����\�[�g
                strSort = "syouhin_mei"
                strUpDown = "ASC"
                btnSortSyouhinMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortSyouhinMeiDown.ID   '--���i���̂ō~���\�[�g
                strSort = "syouhin_mei"
                strUpDown = "DESC"
                btnSortSyouhinMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortTyousaUp.ID '--�����R�[�h�ŏ����\�[�g
                strSort = "tys_houhou_no"
                strUpDown = "ASC"
                btnSortTyousaUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortTyousaDown.ID   '--�����R�[�h�ō~���\�[�g
                strSort = "tys_houhou_no"
                strUpDown = "DESC"
                btnSortTyousaDown.ForeColor = Drawing.Color.IndianRed
                'Case btnSortTokubetuCdUp.ID '--���ʑΉ��R�[�h�ŏ����\�[�g
                '    strSort = "tokubetu_taiou_cd"
                '    strUpDown = "ASC"
                '    btnSortTokubetuCdUp.ForeColor = Drawing.Color.IndianRed
                'Case btnSortTokubetuCdDown.ID   '--���ʑΉ��R�[�h�ō~���\�[�g
                '    strSort = "tokubetu_taiou_cd"
                '    strUpDown = "DESC"
                '    btnSortTokubetuCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortTokubetuMeiUp.ID    '--���ʑΉ����̂ŏ����\�[�g
                strSort = "tokubetu_taiou_meisyou"
                strUpDown = "ASC"
                btnSortTokubetuMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortTokubetuMeiDown.ID  '--���ʑΉ����̂ō~���\�[�g
                strSort = "tokubetu_taiou_meisyou"
                strUpDown = "DESC"
                btnSortTokubetuMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortTorikesiUp.ID   '--����ŏ����\�[�g
                strSort = "torikesi"
                strUpDown = "ASC"
                btnSortTorikesiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortTorikesiDown.ID '--����ō~���\�[�g
                strSort = "torikesi"
                strUpDown = "DESC"
                btnSortTorikesiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortKingakuAddScdUp.ID
                strSort = "kasan_syouhin_cd"
                strUpDown = "ASC"
                btnSortKingakuAddScdUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortKingakuAddScdDown.ID
                strSort = "kasan_syouhin_cd"
                strUpDown = "DESC"
                btnSortKingakuAddScdDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortSyokiTiUp.ID
                strSort = "syokiti"
                strUpDown = "ASC"
                btnSortSyokiTiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortSyokiTiDown.ID
                strSort = "syokiti"
                strUpDown = "DESC"
                btnSortSyokiTiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortRequestAddKingakuUp.ID
                strSort = "uri_kasan_gaku"
                strUpDown = "ASC"
                btnSortRequestAddKingakuUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortRequestAddKingakuDown.ID
                strSort = "uri_kasan_gaku"
                strUpDown = "DESC"
                btnSortRequestAddKingakuDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortKoumuAddKingakuUp.ID
                strSort = "koumuten_kasan_gaku"
                strUpDown = "ASC"
                btnSortKoumuAddKingakuUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortKoumuAddKingakuDown.ID
                strSort = "koumuten_kasan_gaku"
                strUpDown = "DESC"
                btnSortKoumuAddKingakuDown.ForeColor = Drawing.Color.IndianRed

        End Select

        '��ʏc�X�N���[����ݒ肷��
        scrollHeight = ViewState("scrollHeight")

        '��ʃf�[�^�̃\�[�g����ݒ肷��
        Dim dvKameitenInfo As Data.DataView = CType(ViewState("dtTokubetuTaiou"), Data.DataTable).DefaultView
        dvKameitenInfo.Sort = strSort & " " & strUpDown

        Me.grdMeisaiLeft.DataSource = dvKameitenInfo
        Me.grdMeisaiLeft.DataBind()
        Me.grdMeisaiRight.DataSource = dvKameitenInfo
        Me.grdMeisaiRight.DataBind()

        '���ׂ̔w�i�F��ݒ肷��
        SetMeisaiBackColor()

    End Sub

    ''' <summary>�u�������s�v�{�^����������</summary>
    Protected Sub benKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click

        '���̓`�F�b�N
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        Dim dtTokubetuTaiou As Data.DataTable '�f�[�^����
        Dim intCount As Integer    '��������
        '����������ݒ肷��
        Dim dtParam As Dictionary(Of String, String) = SetKensakuJyouken()

        If Me.chkSiteiNasiTaisyou.Checked Then
            '�u�n��E�c�Ə��E�w�薳�����Ώۃ`�F�b�N�{�b�N�X�v=���`�F�b�N�̏ꍇ

            '�����f�[�^���擾����
            dtTokubetuTaiou = tokubetuTaiouMasterLogic.GetTokubetuTaiouNasiInfo(dtParam)
            '�����������擾����
            intCount = tokubetuTaiouMasterLogic.GetTokubetuTaiouNasiCount(dtParam)
        Else
            '�u�n��E�c�Ə��E�w�薳�����Ώۃ`�F�b�N�{�b�N�X�v=�`�F�b�N�̏ꍇ�i�w������X�̌n��E�c�Ə��Ǝw��Ȃ����Ώہj

            '�����f�[�^���擾����
            dtTokubetuTaiou = tokubetuTaiouMasterLogic.GetTokubetuTaiouJyouhou(dtParam)
            '�����������擾����
            intCount = tokubetuTaiouMasterLogic.GetTokubetuTaiouCount(dtParam)
        End If

        '�������ʂ�ݒ肷��
        Me.grdMeisaiLeft.DataSource = dtTokubetuTaiou
        Me.grdMeisaiLeft.DataBind()
        Me.grdMeisaiRight.DataSource = dtTokubetuTaiou
        Me.grdMeisaiRight.DataBind()

        '���ׂ̔w�i�F��ݒ肷��
        SetMeisaiBackColor()

        '��ʃf�[�^��ݒ肷��
        'Call SetGamenData()
        Call Me.SetGamenData(Me.ddlAitesakiSyubetu.SelectedValue.Trim, Me.tbxKameitenCdFrom.Text.Trim, Me.tbxKameitenCdTo.Text.Trim, IIf(Me.chkTorikesi.Checked, "1", String.Empty))
        '�������ʌ�����ݒ肷��
        Call SetKensakuKekka(intCount)

        '�񍐏��l����ݒ肷��
        Call SetHoukokusyo(dtTokubetuTaiou)

        If intCount = 0 Then
            '�\�[�g���{�^����ݒ肷��
            Call SetUpDownHyouji(False)
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
        Else
            '�\�[�g���{�^����ݒ肷��
            Call SetUpDownHyouji(True)
            '�\�[�g���{�^���F��ݒ肷��
            Call setUpDownColor()
            ViewState("dtTokubetuTaiou") = dtTokubetuTaiou

        End If
        ViewState("scrollHeight") = scrollHeight
    End Sub

    '''<summary>�uCSV�o�́v�{�^������������</summary>
    Protected Sub btnCSVOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCSVOutput.Click

        '���̓`�F�b�N
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '��������
        Dim intCount As Long
        '    '����������ݒ肷��
        Dim dtParam As Dictionary(Of String, String) = SetKensakuJyouken()

        '�ő�CSV�o�͐�
        Dim intCsvMax As Integer = CInt(System.Configuration.ConfigurationManager.AppSettings("CsvDownMax"))

        '��ʃf�[�^��ݒ肷��
        'Call SetGamenData()
        Call Me.SetGamenData(Me.ddlAitesakiSyubetu.SelectedValue.Trim, Me.tbxKameitenCdFrom.Text.Trim, Me.tbxKameitenCdTo.Text.Trim, IIf(Me.chkTorikesi.Checked, "1", String.Empty))

        If Me.chkMisetteimo.Checked Then
            '�������ʂ��N���A����
            Me.grdMeisaiLeft.DataSource = Nothing
            Me.grdMeisaiLeft.DataBind()
            Me.grdMeisaiRight.DataSource = Nothing
            Me.grdMeisaiRight.DataBind()

            '�������ʌ�����ݒ肷��
            Call SetKensakuKekka(0)

            '�����N�{�^���̕\����ݒ�
            Call Me.SetUpDownHyouji(False)

            '���ݒ���܂މ����X���i�������@���ʑΉ�CSV�f�[�^������ݒ肷��
            intCount = tokubetuTaiouMasterLogic.GetTokubetuTaiouCSVCount(dtParam)
        Else
            '�����f�[�^���擾����
            Dim dtTokubetuTaiou As Data.DataTable = tokubetuTaiouMasterLogic.GetTokubetuTaiouJyouhou(dtParam)

            '�����f�[�^���擾����
            Me.grdMeisaiLeft.DataSource = dtTokubetuTaiou
            Me.grdMeisaiLeft.DataBind()
            Me.grdMeisaiRight.DataSource = dtTokubetuTaiou
            Me.grdMeisaiRight.DataBind()

            '���ׂ̔w�i�F��ݒ肷��
            SetMeisaiBackColor()

            '�����������擾����
            intCount = tokubetuTaiouMasterLogic.GetTokubetuTaiouCount(dtParam)

            '�������ʌ�����ݒ肷��
            Call SetKensakuKekka(intCount)

            If intCount = 0 Then
                '�\�[�g���{�^����ݒ肷��
                Call SetUpDownHyouji(False)
            Else
                '�\�[�g���{�^����ݒ肷��
                Call SetUpDownHyouji(True)
                '�\�[�g���{�^���F��ݒ肷��
                Call setUpDownColor()
                ViewState("dtTokubetuTaiou") = dtTokubetuTaiou
            End If
        End If

        ViewState("scrollHeight") = scrollHeight

        If intCount > intCsvMax Then
            ShowMessage(Messages.Instance.MSG051E.Replace("@PARAM1", CStr(intCsvMax)), String.Empty)
        Else
            Me.hidCSVFlg.Value = "1"
            ClientScript.RegisterStartupScript(Me.GetType, "", "<script language=javascript>document.forms[0].submit();</script>")
        End If
    End Sub

    '''' <summary>��ʃf�[�^��ݒ肷��</summary>
    'Private Sub SetGamenData()
    '    '�����X�R�[�hFrom�ɂ���āA����ݒ�
    '    If Me.tbxKameitenCdFrom.Text.ToString.Trim <> String.Empty Then

    '        Dim dtKameitenMeiFrom As New Data.DataTable
    '        dtKameitenMeiFrom = commonSearchLogic.GetKameitenKensakuInfo("1", "", "" & Me.tbxKameitenCdFrom.Text.Trim & "", "", False)

    '        If dtKameitenMeiFrom.Rows.Count > 0 Then
    '            Me.tbxKameitenMeiFrom.Text = dtKameitenMeiFrom.Rows(0).Item("kameiten_mei1")
    '        Else
    '            Me.tbxKameitenMeiFrom.Text = String.Empty
    '        End If
    '    Else
    '        Me.tbxKameitenMeiFrom.Text = String.Empty
    '    End If
    '    '�����X�R�[�hTo�ɂ���āA����ݒ�
    '    If Me.tbxKameitenCdTo.Text.ToString.Trim <> String.Empty Then

    '        Dim dtKameitenMeiTo As New Data.DataTable
    '        dtKameitenMeiTo = commonSearchLogic.GetKameitenKensakuInfo("1", "", "" & Me.tbxKameitenCdTo.Text.Trim & "", "", False)

    '        If dtKameitenMeiTo.Rows.Count > 0 Then
    '            Me.tbxKameitenMeiTo.Text = dtKameitenMeiTo.Rows(0).Item("kameiten_mei1")
    '        Else
    '            Me.tbxKameitenMeiTo.Text = String.Empty
    '        End If
    '    Else
    '        Me.tbxKameitenMeiTo.Text = String.Empty
    '    End If
    '    '���ʑΉ��R�[�h�ɂ���āA���̂�ݒ�
    '    If Me.tbxTokubetuTaiouCd.Text.ToString.Trim <> String.Empty Then

    '        Dim dtTokubetuMei As New Data.DataTable
    '        dtTokubetuMei = commonSearchLogic.GetTokubetuKaiouInfo("1", "" & Me.tbxTokubetuTaiouCd.Text.Trim & "", "", False)

    '        If dtTokubetuMei.Rows.Count > 0 Then
    '            Me.tbxTokubetuTaiouMei.Text = dtTokubetuMei.Rows(0).Item("tokubetu_taiou_meisyou")
    '        Else
    '            Me.tbxTokubetuTaiouMei.Text = String.Empty
    '        End If
    '    Else
    '        Me.tbxTokubetuTaiouMei.Text = String.Empty
    '    End If
    'End Sub

    ''' <summary>��ʃf�[�^��ݒ肷��</summary>
    Private Sub SetGamenData(ByVal strAitesakiSyobetu As String, ByVal strAitesakiFromCd As String, ByVal strAitesakiToCd As String, ByVal strTorikesiAitesaki As String)

        If Me.ddlAitesakiSyubetu.SelectedValue.Trim.Equals(String.Empty) OrElse Me.ddlAitesakiSyubetu.SelectedValue.Trim.Equals("0") OrElse Me.ddlAitesakiSyubetu.SelectedValue.Trim.Equals("3") Then
            '�����R�[�hFROM
            Me.tbxKameitenCdFrom.Text = String.Empty
            '����於FROM
            Me.tbxKameitenMeiFrom.Text = String.Empty
            '�����R�[�hTO
            Me.tbxKameitenCdTo.Text = String.Empty
            '����於TO
            Me.tbxKameitenMeiTo.Text = String.Empty

            Me.divAitesakiSyubetu.Attributes.Add("style", "display:none;")
        Else

            If Not strAitesakiFromCd.Trim.Equals(String.Empty) Then
                '�����R�[�hFROM
                Me.tbxKameitenCdFrom.Text = strAitesakiFromCd

                '����於���擾
                Dim dtAitesakiMei As New Data.DataTable
                dtAitesakiMei = tokubetuTaiouMasterLogic.GetAitesakiMei(CInt(strAitesakiSyobetu), strAitesakiFromCd, strTorikesiAitesaki)

                '����於FROM
                If dtAitesakiMei.Rows.Count > 0 Then
                    Me.tbxKameitenMeiFrom.Text = dtAitesakiMei.Rows(0).Item("aitesaki_mei").ToString.Trim
                Else
                    'Me.tbxAiteSakiCdFrom.Text = String.Empty
                    Me.tbxKameitenMeiFrom.Text = String.Empty
                End If
            Else
                Me.tbxKameitenMeiFrom.Text = String.Empty
            End If


            If Not strAitesakiToCd.Trim.Equals(String.Empty) Then
                '�����R�[�hTO
                Me.tbxKameitenCdTo.Text = strAitesakiToCd

                '����於���擾
                Dim dtAitesakiMei As New Data.DataTable
                dtAitesakiMei = tokubetuTaiouMasterLogic.GetAitesakiMei(CInt(strAitesakiSyobetu), strAitesakiToCd, strTorikesiAitesaki)

                '����於TO
                If dtAitesakiMei.Rows.Count > 0 Then
                    Me.tbxKameitenMeiTo.Text = dtAitesakiMei.Rows(0).Item("aitesaki_mei").ToString.Trim
                Else
                    'Me.tbxAiteSakiCdTo.Text = String.Empty
                    Me.tbxKameitenMeiTo.Text = String.Empty
                End If
            Else
                Me.tbxKameitenMeiTo.Text = String.Empty

            End If

            Me.divAitesakiSyubetu.Attributes.Add("style", "display:block;")
        End If

        '���ʑΉ��R�[�h�ɂ���āA���̂�ݒ�
        If Me.tbxTokubetuTaiouCd.Text.ToString.Trim <> String.Empty Then

            Dim dtTokubetuMei As New Data.DataTable
            dtTokubetuMei = commonSearchLogic.GetTokubetuKaiouInfo("1", "" & Me.tbxTokubetuTaiouCd.Text.Trim & "", "", False)

            If dtTokubetuMei.Rows.Count > 0 Then
                Me.tbxTokubetuTaiouMei.Text = dtTokubetuMei.Rows(0).Item("tokubetu_taiou_meisyou")
            Else
                Me.tbxTokubetuTaiouMei.Text = String.Empty
            End If
        Else
            Me.tbxTokubetuTaiouMei.Text = String.Empty
        End If

    End Sub

    '''<summary>CSV�t�@�C�����쐬</summary>
    Private Sub MakeCSVFile()

        '����������ݒ肷��
        Dim dtParamList As Dictionary(Of String, String) = SetKensakuJyouken()
        Dim dtTokubetuTaiouCSV As New Data.DataTable

        If Me.chkMisetteimo.Checked = True Then

            dtTokubetuTaiouCSV = tokubetuTaiouMasterLogic.GetTokubetuTaiouCSV(dtParamList)

        Else

            dtTokubetuTaiouCSV = tokubetuTaiouMasterLogic.GetTokubetuTaiouJyouhouCSV(dtParamList)

        End If

        'CSV�t�@�C�����ݒ�
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("TokubetuTaiouMasterCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSV�t�@�C���w�b�_�ݒ�
        writer.WriteLine(EarthConst.conTokubetuTaiouCsvHeader)

        'CSV�t�@�C�����e�ݒ�
        For Each row As Data.DataRow In dtTokubetuTaiouCSV.Rows
            writer.WriteLine(row.Item(0), row.Item(1), row.Item(2), row.Item(3), row.Item(4), row.Item(5), _
            row.Item(6), row.Item(7), row.Item(8), row.Item(9), row.Item(10), row.Item(11), row.Item(12), row.Item(13), row.Item(14))
        Next

        'CSV�t�@�C���_�E�����[�h
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()

    End Sub

    '''<summary>��ʂɌ���������ݒ�</summary>
    Public Function SetKensakuJyouken() As Dictionary(Of String, String)
        Dim dtParamlist As New Dictionary(Of String, String)
        dtParamlist.Add("kensuu", CStr(Me.ddlKensuu.SelectedValue))                             '�����������
        dtParamlist.Add("aitesakiSyubetu", CStr(Me.ddlAitesakiSyubetu.SelectedValue.Trim))      '�������
        dtParamlist.Add("aitesakiCdFrom", CStr(Me.tbxKameitenCdFrom.Text.Trim))                 '�����R�[�hFrom
        dtParamlist.Add("aitesakiCdTo", CStr(Me.tbxKameitenCdTo.Text.Trim))                     '�����R�[�hTo
        dtParamlist.Add("syouhin_cd", CStr(Me.ddlSyouhinCd.SelectedValue))                      '���i�R�[�h
        dtParamlist.Add("tys_houhou_no", CStr(Me.ddlTyousaHouhou.SelectedValue))                '�������@No
        dtParamlist.Add("tokubetu_taiou_cd", CStr(Me.tbxTokubetuTaiouCd.Text.Trim))             '���ʑΉ��R�[�h
        If Me.chkTorikesi.Checked = True Then
            dtParamlist.Add("torikesiFlg", 1)   '����͌����ΏۊO
        Else
            dtParamlist.Add("torikesiFlg", 0)   '������܂�
        End If

        dtParamlist.Add("aitesakiTorikesiFlg", IIf(Me.chkAitesakiTaisyouGai.Checked, "1", "0")) '��������͑ΏۊO
        dtParamlist.Add("kingaku0TorikesiFlg", IIf(Me.chk0TaisyouGai.Checked, "1", "0"))        '\0�͑ΏۊO

        '------------------From 2013.03.09    ���F�ǉ�����-----------------
        dtParamlist.Add("Syokiti1Nomi", IIf(Me.chkSyokiti.Checked, "1", "0"))        '�����l1�̂�
        '------------------To   2013.03.09    ���F�ǉ�����-----------------

        Return dtParamlist
    End Function

    ''' <summary>���̓`�F�b�N</summary>
    ''' <param name="strObjId">�N���C�A���gID</param>
    ''' <returns>�G���[���b�Z�[�W</returns>
    Public Function CheckInput(ByRef strObjId As String) As String
        Dim csScript As New StringBuilder
        With csScript
            '�����X�R�[�h(From)
            If Me.tbxKameitenCdFrom.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCdFrom.Text, "�����X�R�[�h(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenCdFrom.ClientID
                End If
            End If
            '�����X�R�[�h(To)
            If Me.tbxKameitenCdTo.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCdTo.Text, "�����X�R�[�h(To)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenCdTo.ClientID
                End If
            End If
            '�����X�R�[�h�͈�
            If Me.tbxKameitenCdFrom.Text <> String.Empty And Me.tbxKameitenCdTo.Text <> String.Empty Then
                If commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCdFrom.Text, "�����X�R�[�h(From)") = String.Empty _
                   And commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCdTo.Text, "�����X�R�[�h(To)") = String.Empty Then
                    If Me.tbxKameitenCdFrom.Text > Me.tbxKameitenCdTo.Text Then
                        .Append(String.Format(Messages.Instance.MSG2012E, "�����X�R�[�h", "�����X�R�[�h").ToString)
                        If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                            strObjId = Me.tbxKameitenCdFrom.ClientID
                        End If
                    End If
                End If
            End If
            '���ʑΉ��R�[�h
            If Me.tbxTokubetuTaiouCd.Text <> String.Empty Then
                .Append(commonCheck.CheckHankaku(Me.tbxTokubetuTaiouCd.Text, "���ʑΉ��R�[�h"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxTokubetuTaiouCd.ClientID
                End If
            End If
        End With

        Return csScript.ToString

    End Function

    ''' <summary>�������ʂ�ݒ�</summary>
    Private Sub SetKensakuKekka(ByVal intCount As Integer)
        If Me.ddlKensuu.SelectedValue = "max" Then
            Me.lblCount.Text = CStr(intCount)
            Me.lblCount.ForeColor = Drawing.Color.Black
            scrollHeight = intCount * 43 + 1
        Else
            If intCount > Convert.ToInt64(Me.ddlKensuu.SelectedValue) Then
                Me.lblCount.Text = CStr(Me.ddlKensuu.SelectedValue) & "/" & CStr(intCount)
                Me.lblCount.ForeColor = Drawing.Color.Red
                scrollHeight = Me.ddlKensuu.SelectedValue * 43 + 1
            Else
                Me.lblCount.Text = CStr(intCount)
                Me.lblCount.ForeColor = Drawing.Color.Black
                scrollHeight = intCount * 43 + 1
            End If
        End If
    End Sub

    ''' <summary>JavaScript���쐬</summary>
    Protected Sub MakeJavaScript()
        Dim sbScript As New StringBuilder
        With sbScript
            .AppendLine("<script type='text/javascript' language='javascript'>")
            '���ʑΉ�������ʂ��|�b�v�A�b�v����
            .AppendLine("   function fncSetTokubetuKaiou(){")
            .AppendLine("       var objCd = '" & Me.tbxTokubetuTaiouCd.ClientID & "';")
            .AppendLine("       var objMei = '" & Me.tbxTokubetuTaiouMei.ClientID & "';")
            .AppendLine("       var strCd = document.getElementById('" & Me.tbxTokubetuTaiouCd.ClientID & "').value;")
            .AppendLine("       var FormName = '" & Me.Form.Name & "';")
            .AppendLine("       window.open('search_tokubetu_taiou.aspx?blnDelete=False&FormName='+FormName+'&objCd='+objCd+'&objMei='+objMei+'&strCd='+strCd,'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("   }")
            'CSV�捞��ʂ��|�b�v�A�b�v����
            .AppendLine("   function fncCSVTorikomi(){")
            .AppendLine("       window.open('TokubetuTaiouMasterInput.aspx', 'CSVTorikomi','menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '�����X������ʂ��|�b�v�A�b�v����
            '.AppendLine("   function fncSetKameiten(objCd,strCdKbn,objMei){")
            '.AppendLine("       var strCdFrom = document.getElementById('" & Me.tbxKameitenCdFrom.ClientID & "').value;")
            '.AppendLine("       var strCdTo = document.getElementById('" & Me.tbxKameitenCdTo.ClientID & "').value;")
            '.AppendLine("       var FormName = '" & Me.Form.Name & "';")
            '.AppendLine("       if(strCdKbn == 1){")
            '.AppendLine("           window.open('search_common.aspx?blnDelete=False&FormName='+FormName+'&objCd='+objCd+'&strCd='+strCdFrom+'&objMei='+objMei+'&Kbn='+escape('�����X'),'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            '.AppendLine("       }")
            '.AppendLine("       else{")
            '.AppendLine("           window.open('search_common.aspx?blnDelete=False&FormName='+FormName+'&objCd='+objCd+'&strCd='+strCdTo+'&objMei='+objMei+'&Kbn='+escape('�����X'),'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            '.AppendLine("       }")
            '.AppendLine("   }")

            '����挟������������ꍇ�A�|�b�v�A�b�v���N������
            .AppendLine("function fncAiteSakiSearch(strAiteSakiKbn){")
            '������ʂ��u1:�����X�v�̏ꍇ�A�����X�|�b�v�A�b�v���N������
            .AppendLine("   if(document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value == '1'){")
            .AppendLine("       var strkbn='�����X';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       if(strAiteSakiKbn == '1'){")
            .AppendLine("           strClientCdID = '" & Me.tbxKameitenCdFrom.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxKameitenMeiFrom.ClientID & "';")
            .AppendLine("       }else{")
            .AppendLine("           strClientCdID = '" & Me.tbxKameitenCdTo.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxKameitenMeiTo.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       if(document.all." & Me.chkTorikesi.ClientID & ".checked){")
            .AppendLine("           blnTorikesi = 'True';")
            .AppendLine("       }else{")
            .AppendLine("           blnTorikesi = 'False';")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }else if(document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value == '5'){")
            '������ʂ��u5:�c�Ə��v�̏ꍇ�A�c�Ə��|�b�v�A�b�v���N������
            .AppendLine("       var strkbn='�c�Ə�';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       if(strAiteSakiKbn == '1'){")
            .AppendLine("           strClientCdID = '" & Me.tbxKameitenCdFrom.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxKameitenMeiFrom.ClientID & "';")
            .AppendLine("       }else{")
            .AppendLine("           strClientCdID = '" & Me.tbxKameitenCdTo.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxKameitenMeiTo.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       if(document.all." & Me.chkTorikesi.ClientID & ".checked){")
            .AppendLine("           blnTorikesi = 'True';")
            .AppendLine("       }else{")
            .AppendLine("           blnTorikesi = 'False';")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }else if(document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value == '7'){")
            '������ʂ��u7:�n��v�̏ꍇ�A�n��|�b�v�A�b�v���N������
            .AppendLine("       var strkbn='�n��';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       if(strAiteSakiKbn == '1'){")
            .AppendLine("           strClientCdID = '" & Me.tbxKameitenCdFrom.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxKameitenMeiFrom.ClientID & "';")
            .AppendLine("       }else{")
            .AppendLine("           strClientCdID = '" & Me.tbxKameitenCdTo.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxKameitenMeiTo.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       if(document.all." & Me.chkTorikesi.ClientID & ".checked){")
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

            '�u�N���A�v�{�^���̏���
            .AppendLine("   function fncClear(){")
            .AppendLine("       document.getElementById('" & Me.tbxKameitenCdFrom.ClientID & "').value='';")
            .AppendLine("       document.getElementById('" & Me.tbxKameitenMeiFrom.ClientID & "').value='';")
            .AppendLine("       document.getElementById('" & Me.tbxKameitenCdTo.ClientID & "').value='';")
            .AppendLine("       document.getElementById('" & Me.tbxKameitenMeiTo.ClientID & "').value='';")
            .AppendLine("       document.getElementById('" & Me.ddlSyouhinCd.ClientID & "').selectedIndex=0;")
            .AppendLine("       document.getElementById('" & Me.ddlTyousaHouhou.ClientID & "').selectedIndex=0;")
            .AppendLine("       document.getElementById('" & Me.tbxTokubetuTaiouCd.ClientID & "').value='';")
            .AppendLine("       document.getElementById('" & Me.tbxTokubetuTaiouMei.ClientID & "').value='';")
            .AppendLine("       document.getElementById('" & Me.ddlKensuu.ClientID & "').selectedIndex=1;")
            .AppendLine("       document.getElementById('" & Me.chkTorikesi.ClientID & "').checked=true;")
            .AppendLine("       document.all." & Me.chkAitesakiTaisyouGai.ClientID & ".checked = true;")
            .AppendLine("       document.getElementById('" & Me.chkMisetteimo.ClientID & "').checked=false;")
            .AppendLine("       document.all." & Me.divSitei.ClientID & ".style.display = 'none';")
            .AppendLine("       document.getElementById('" & Me.btnKensaku.ClientID & "').disabled=false;")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'block';")

            .AppendLine("       document.all." & Me.chk0TaisyouGai.ClientID & ".checked = false;")            '�������
            .AppendLine("       document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value = '';")
            '�����R�[�h��\�����Ȃ�
            .AppendLine("       document.all." & Me.divAitesakiSyubetu.ClientID & ".style.display = 'none';")
            .AppendLine("   }")
            '�����X�R�[�h���͂��ǂ����`�F�b�N
            .AppendLine("   function fncKameitenCdChk(strKbn){")
            .AppendLine("       var KameitenCdFrom = document.getElementById('" & Me.tbxKameitenCdFrom.ClientID & "');")
            .AppendLine("       var KameitenCdTo = document.getElementById('" & Me.tbxKameitenCdTo.ClientID & "');")
            .AppendLine("       var chkMisettei = document.getElementById('" & Me.chkMisetteimo.ClientID & "');")
            .AppendLine("       if(document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value != '' && document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value != '0' && KameitenCdFrom.value == '' && KameitenCdTo.value == ''){")
            .AppendLine("           alert('" & Messages.Instance.MSG041E.Replace("@PARAM1", "�����X�R�[�hFROM").Replace("@PARAM2", "�����X�R�[�hTO") & "');")
            .AppendLine("           KameitenCdFrom.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '�m�F���b�Z�[�W�\��
            .AppendLine("       if (strKbn == 'kensaku'){")
            .AppendLine("           if (document.all." & Me.ddlKensuu.ClientID & ".value=='max'){")
            .AppendLine("               if(!confirm('" & Messages.Instance.MSG007C & "')){")
            .AppendLine("                   return false; ")
            .AppendLine("               }")
            .AppendLine("           }")
            .AppendLine("       }")
            '������ʂ��K�{���̓`�F�b�N
            .AppendLine("   if (document.all." & Me.ddlAitesakiSyubetu.ClientID & ".selectedIndex=='0'){")
            .AppendLine("       alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "�������") & "');")
            .AppendLine("       document.all." & Me.ddlAitesakiSyubetu.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            .AppendLine("       return true;")
            .AppendLine("   }")
            '�u���ݒ���܂ށv�`�F�b�N�{�b�N�X
            .AppendLine("   function fncSetbtnKensaku(){")
            .AppendLine("   if(document.all." & Me.chkMisetteimo.ClientID & ".checked){")
            .AppendLine("       document.all." & Me.btnKensaku.ClientID & ".disabled = true;")
            .AppendLine("       document.all." & Me.divSitei.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.chkSiteiNasiTaisyou.ClientID & ".checked = false;")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'block';")
            .AppendLine("   }else{")
            .AppendLine("       document.all." & Me.btnKensaku.ClientID & ".disabled = false;")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'block';")
            .AppendLine("       if(document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value == '1'){")
            .AppendLine("           document.all." & Me.divSitei.ClientID & ".style.display = 'block';")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("    }")
            '�u�n��E�c�Ə��E�w�薳���������Ώہv�`�F�b�N�{�b�N�X����
            .AppendLine("function fncChangeAite(){")
            .AppendLine("   if(document.all." & Me.chkSiteiNasiTaisyou.ClientID & ".checked){")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.tbxKameitenCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenMeiTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'none';")
            .AppendLine("   }else{")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.btnCSVOutput.ClientID & ".display = true;")
            .AppendLine("   }")
            .AppendLine("}")
            '�uClose�v�{�^���̏���
            .AppendLine("   function fncClose(){")
            .AppendLine("       window.close();")
            .AppendLine("       return false;")
            .AppendLine("   }")
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
            '.AppendLine("function fncScrollV(){")
            '.AppendLine("   var divbody=" & Me.divMeisai.ClientID & ";")
            '.AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            '.AppendLine("   divbody.scrollTop = divVscroll.scrollTop;")
            '.AppendLine("}")
            .AppendLine("function fncScrollV(){")
            .AppendLine("   var divbodyleft=" & Me.divMeisaiLeft.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divMeisaiRight.ClientID & ";")
            .AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   divbodyleft.scrollTop = divVscroll.scrollTop;")
            .AppendLine("   divbodyright.scrollTop = divVscroll.scrollTop;")
            .AppendLine("}")
            .AppendLine("function fncScrollH(){")
            .AppendLine("   var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divMeisaiRight.ClientID & ";")
            .AppendLine("   var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("   divheadright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("   divbodyright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("}")
            .AppendLine("   function fncSetHidCSV(){")
            .AppendLine("       document.getElementById('" & Me.hidCSVFlg.ClientID & "').value='';")
            .AppendLine("   }")
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

            '����挟���\����ݒ肷��
            .AppendLine("function fncSetAitesaki(){")
            .AppendLine("   if(document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value == '0'||document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value == ''){")
            .AppendLine("       document.all." & Me.tbxKameitenCdFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenMeiFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenMeiTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.divAitesakiSyubetu.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.divSitei.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.chkSiteiNasiTaisyou.ClientID & ".checked = false;")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'block';")
            .AppendLine("   }else if(document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value == '1'){")
            .AppendLine("       document.all." & Me.divAitesakiSyubetu.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.tbxKameitenCdFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenMeiFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenMeiTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'block';")
            .AppendLine("       if(!document.all." & Me.chkMisetteimo.ClientID & ".checked){")
            .AppendLine("           document.all." & Me.divSitei.ClientID & ".style.display = 'block';")
            .AppendLine("           document.all." & Me.chkSiteiNasiTaisyou.ClientID & ".checked = 'true';")
            .AppendLine("           document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'none';")
            .AppendLine("           document.all." & Me.divCsvOutput.ClientID & ".style.display = 'none';")
            .AppendLine("       }")
            .AppendLine("   }else{")
            .AppendLine("       document.all." & Me.divAitesakiSyubetu.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.tbxKameitenCdFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenMeiFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenMeiTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.divSitei.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.chkSiteiNasiTaisyou.ClientID & ".checked = false;")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'block';")
            .AppendLine("   }")
            .AppendLine("}")

            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "JS_" & Me.ClientID, sbScript.ToString)
    End Sub

    ''' <summary>���b�Z�[�W�\��</summary>
    ''' <param name="strMessage">���b�Z�[�W</param>
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

    ''' <summary>DIV��\��</summary>
    Public Sub CloseCover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat("fncClosecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseCover", csScript.ToString, True)
    End Sub

    'hidden�̒l��ݒ肷��
    Protected Sub SetButton()
        Me.btnSortAitesakiSyubetuDown.Attributes.Add("onClick", "fncShowModal();fncSetHidCSV()")        '--������ʁ�
        Me.btnSortAitesakiSyubetuUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")          '--������ʁ�
        Me.btnSortAitesakiCdDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")             '--�����R�[�h��
        Me.btnSortAitesakiCdUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")               '--�����R�[�h��
        Me.btnSortAitesakiMeiDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")            '--����於�́�
        Me.btnSortAitesakiMeiUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")              '--����於�́�
        Me.btnSortSyouhinCdUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")                '--���i�R�[�h��
        Me.btnSortSyouhinCdDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")              '--���i�R�[�h��
        Me.btnSortSyouhinMeiUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")               '--���i����
        Me.btnSortSyouhinMeiDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")             '--���i����
        Me.btnSortTyousaUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")                   '--�������@��
        Me.btnSortTyousaDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")                 '--�������@��
        'Me.btnSortTokubetuCdUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")               '--���ʑΉ��R�[�h��
        'Me.btnSortTokubetuCdDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")             '--���ʑΉ��R�[�h��
        Me.btnSortTokubetuMeiUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")              '--���ʑΉ����́�
        Me.btnSortTokubetuMeiDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")            '--���ʑΉ����́�
        Me.btnSortTorikesiUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")                 '--�����
        Me.btnSortTorikesiDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")               '--�����
        Me.btnSortKingakuAddScdDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")          '--���z���Z���i�R�[�h��
        Me.btnSortKingakuAddScdUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")            '--���z���Z���i�R�[�h��
        Me.btnSortSyokiTiDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")                '--�����l��
        Me.btnSortSyokiTiUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")                  '--�����l��
        Me.btnSortRequestAddKingakuDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")      '--���������Z���z��
        Me.btnSortRequestAddKingakuUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")        '--���������Z���z��
        Me.btnSortKoumuAddKingakuDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")        '--�H���X�������Z���z��
        Me.btnSortKoumuAddKingakuUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")          '--�H���X�������Z���z��
    End Sub

    Private Sub grdBodyRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMeisaiRight.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim str1 As String = CType(e.Row.FindControl("uri_kasan_gaku"), Label).Text
            Dim str2 As String = CType(e.Row.FindControl("koumuten_kasan_gaku"), Label).Text

            If String.IsNullOrEmpty(str1) Then
                CType(e.Row.FindControl("uri_kasan_gaku"), Label).Text = String.Empty
            Else
                CType(e.Row.FindControl("uri_kasan_gaku"), Label).Text = FormatNumber(str1.Trim, 0)
            End If

            If String.IsNullOrEmpty(str2) Then
                CType(e.Row.FindControl("koumuten_kasan_gaku"), Label).Text = String.Empty
            Else
                CType(e.Row.FindControl("koumuten_kasan_gaku"), Label).Text = FormatNumber(str2.Trim, 0)
            End If
        End If
    End Sub

    ''' <summary>
    ''' ��ʂ̕񍐏��l����ݒ肷��
    ''' </summary>
    ''' <param name="dtTokubetuTaiou"></param>
    ''' <history>
    '''     2013.05.20 �k�o �V�K�쐬
    ''' </history>
    Private Sub SetHoukokusyo(ByVal dtTokubetuTaiou As Data.DataTable)

        '�u�n��E�c�Ə��E�w�薳�����Ώۃ`�F�b�N�{�b�N�X�v=�`�F�b�N�̏ꍇ
        If chkSiteiNasiTaisyou.Checked = True AndAlso dtTokubetuTaiou.Rows.Count > 0 Then

            '�񍐏��l����\������
            Me.tbHoukokusyo.Style.Add("display", "block")

            Dim strHoukokusyo As String = String.Empty  '�񍐏��l���̕\�����e
            Dim strTokubetuMei As String = String.Empty '���ʑΉ��̖���
            '���l��
            Dim strKyuuStyle As String = tokubetuTaiouMasterLogic.GetStyleMeisyou()
            For i As Integer = 0 To dtTokubetuTaiou.Rows.Count - 1
                '�����l�P�̌�����T��
                If dtTokubetuTaiou.Rows(i).Item("syokiti").ToString.Equals("1") Then
                    If dtTokubetuTaiou.Rows(i).Item("tokubetu_taiou_cd").ToString <> String.Empty AndAlso _
                       dtTokubetuTaiou.Rows(i).Item("tokubetu_taiou_cd").ToString < 10 Then
                        strKyuuStyle = String.Empty
                        Exit For
                    End If
                End If
            Next

            '���l���̃Z�b�g
            If strKyuuStyle <> String.Empty Then
                '�񍐏��l���̕\�����e
                strHoukokusyo = strKyuuStyle
            End If

            For i As Integer = 0 To dtTokubetuTaiou.Rows.Count - 1
                '�����l�P�̌�����T��
                If dtTokubetuTaiou.Rows(i).Item("syokiti").ToString.Equals("1") Then

                    If strKyuuStyle = String.Empty OrElse _
                       strKyuuStyle <> String.Empty AndAlso _
                       dtTokubetuTaiou.Rows(i).Item("tokubetu_taiou_cd").ToString <> String.Empty AndAlso _
                       dtTokubetuTaiou.Rows(i).Item("tokubetu_taiou_cd").ToString >= 10 Then
                        '���ʑΉ��̖���
                        strTokubetuMei = dtTokubetuTaiou.Rows(i).Item("tokubetu_taiou_meisyou").ToString
                    Else
                        '���ʑΉ��̖���
                        strTokubetuMei = String.Empty
                    End If

                    '���ʑΉ��̖��̂̑��݂��ǂ����A���f����
                    If strHoukokusyo.IndexOf(strTokubetuMei) >= 0 Then

                        '�񍐏��l���̕\�����e�A���ɑ��݂̎�
                        strHoukokusyo = strHoukokusyo
                    ElseIf strHoukokusyo.IndexOf(strTokubetuMei) = -1 Then

                        '�񍐏��l���̕\�����e�ɒǉ�����
                        If strHoukokusyo = String.Empty Then
                            strHoukokusyo = strTokubetuMei
                        Else
                            strHoukokusyo = strHoukokusyo & "�A" & strTokubetuMei
                        End If
                    End If
                End If
            Next

            '�񍐏��l���̕\��
            Me.lblHoukokusyo.Text = strHoukokusyo
            Me.lblHoukokusyo.ToolTip = strHoukokusyo

        Else
            '�񍐏��l����\�����Ȃ�
            Me.tbHoukokusyo.Style.Add("display", "none")
            Me.lblHoukokusyo.Text = String.Empty

        End If

    End Sub

    ''' <summary>
    ''' ���ׂ̔w�i�F��ݒ肷��
    ''' </summary>
    ''' <history>
    '''     2013.06.07 �k�o �V�K�쐬
    ''' </history>
    Private Sub SetMeisaiBackColor()

        Dim currentRow As Integer
        Dim oldKey As String = String.Empty
        Dim currentKey As String = String.Empty
        Dim color As String = String.Empty

        For currentRow = 0 To grdMeisaiLeft.Rows.Count - 1
            '�Y���s�̃L�[
            currentKey = CType(grdMeisaiLeft.Rows(currentRow).FindControl("aitesaki_syubetu_layout"), Label).Text.ToString & _
                        CType(grdMeisaiLeft.Rows(currentRow).FindControl("aitesaki_cd"), Label).Text.ToString & _
                        CType(grdMeisaiLeft.Rows(currentRow).FindControl("syouhin_cd"), Label).Text.ToString & _
                        CType(grdMeisaiLeft.Rows(currentRow).FindControl("tys_houhou"), Label).Text.ToString
            If currentRow = 0 Then
                '�����l�̃Z�b�g
                oldKey = currentKey
            End If

            '�L�[���ڕύX�̏ꍇ
            If oldKey <> currentKey Then
                '�w�i�F
                If color = "#CCFFFF" Then
                    color = String.Empty
                Else
                    color = "#CCFFFF"
                End If
            End If
            grdMeisaiLeft.Rows(currentRow).Style.Add("background-color", color)
            grdMeisaiRight.Rows(currentRow).Style.Add("background-color", color)
            oldKey = currentKey
        Next

    End Sub

End Class