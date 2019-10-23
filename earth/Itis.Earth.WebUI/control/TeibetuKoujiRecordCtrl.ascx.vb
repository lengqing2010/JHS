Partial Public Class TeibetuKoujiRecordCtrl
    Inherits System.Web.UI.UserControl

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'Utilities��MessegeLogic�N���X
    Private mLogic As New MessageLogic

    Private jSM As New JibanSessionManager

    '�}�X�^�[�y�[�W��Ajax�X�N���v�g�}�l�[�W���ւ̃A�N�Z�X�p
    Private masterAjaxSM As New ScriptManager

    Private cbLogic As New CommonBizLogic
    Private cLogic As New CommonLogic

#Region "�\�����[�h"
    ''' <summary>
    ''' �R���g���[���̕\�����[�h
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DisplayMode
        ''' <summary>
        ''' ���ǍH��
        ''' </summary>
        ''' <remarks></remarks>
        KAIRYOU = 130
        ''' <summary>
        ''' �ǉ��H��
        ''' </summary>
        ''' <remarks></remarks>
        TUIKA = 140
    End Enum
#End Region

#Region "�v���p�e�B"
    ''' <summary>
    ''' �R���g���[���̕\�����[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private mode As DisplayMode
    ''' <summary>
    ''' �R���g���[���̕\�����[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns>�R���g���[���̕\�����[�h</returns>
    ''' <remarks>���i�̎�ނɂ���ʂ̕\����ύX���܂�</remarks>
    Public Property DispMode() As DisplayMode
        Get
            If Not ViewState("DisplayMode") Is Nothing Then
                mode = ViewState("DisplayMode")
            End If
            Return mode
        End Get
        Set(ByVal value As DisplayMode)
            mode = value
            ViewState("DisplayMode") = value
        End Set
    End Property

    ''' <summary>
    ''' �@�ʐ������R�[�h
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _teibetuSeikyuuRec As TeibetuSeikyuuRecord
    ''' <summary>
    ''' �@�ʐ������R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property TeibetuSeikyuuRec() As TeibetuSeikyuuRecord
        Get
            Return GetCtrlData()
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            _teibetuSeikyuuRec = value

            '****************************************************************************
            ' �h���b�v�_�E�����X�g�ݒ�
            '****************************************************************************
            Dim helper As New DropDownHelper
            ' �h���b�v�_�E�����X�g�ݒ�
            If DispMode = DisplayMode.KAIRYOU Then
                helper.SetDropDownList(SelectSyouhinCd, DropDownHelper.DropDownType.SyouhinKouji, True, True, 0, False)
            ElseIf DispMode = DisplayMode.TUIKA Then
                helper.SetDropDownList(SelectSyouhinCd, DropDownHelper.DropDownType.SyouhinTuika, True, True, 0, False)
            End If

            If Not _teibetuSeikyuuRec Is Nothing Then
                ' �R���g���[���Ƀf�[�^���Z�b�g
                SetCtrlData(_teibetuSeikyuuRec)
            End If

        End Set
    End Property

#Region "�@�ʃf�[�^���ʐݒ���"
    ''' <summary>
    ''' �@�ʃf�[�^���ʐݒ���
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingInfo() As TeibetuSettingInfoRecord
        Get
            Dim info As New TeibetuSettingInfoRecord
            info.Kubun = HiddenKubun.Value                                                  ' �敪
            info.Bangou = HiddenBangou.Value                                                ' �ԍ��i�ۏ؏�NO�j
            info.UpdLoginUserId = HiddenLoginUserId.Value                                   ' ���O�C�����[�U�[ID
            info.KeiriGyoumuKengen = Integer.Parse(HiddenKeiriGyoumuKengen.Value)           ' �o���Ɩ�����
            info.HattyuusyoKanriKengen = Integer.Parse(HiddenHattyuusyoKanriKengen.Value)   ' �������Ǘ�����
            Return info
        End Get
        Set(ByVal value As TeibetuSettingInfoRecord)
            HiddenKubun.Value = value.Kubun                                 ' �敪
            HiddenBangou.Value = value.Bangou                               ' �ԍ��i�ۏ؏�NO�j
            HiddenLoginUserId.Value = value.UpdLoginUserId                  ' ���O�C�����[�U�[ID
            ' �o���Ɩ�����
            HiddenKeiriGyoumuKengen.Value = value.KeiriGyoumuKengen.ToString()
            ' �������Ǘ�����
            HiddenHattyuusyoKanriKengen.Value = value.HattyuusyoKanriKengen.ToString()
        End Set
    End Property
#End Region

    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private _kameitenCd As String
    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KameitenCd() As String
        Get
            Return _kameitenCd
        End Get
        Set(ByVal value As String)
            _kameitenCd = value

            If Not _kameitenCd Is Nothing Then
                HiddenKameitenCd.Value = _kameitenCd
            End If
        End Set
    End Property

    ''' <summary>
    ''' �H����ЃR�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private _koujiKaisyaCd As String
    ''' <summary>
    ''' �H����ЃR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KoujiKaisyaCd() As String
        Get
            Return IIf(TextKoujigaisyaCd.Text.Trim() = "", "", Mid(TextKoujigaisyaCd.Text & "     ", 1, 4))
        End Get
        Set(ByVal value As String)
            _koujiKaisyaCd = value

            If Not _koujiKaisyaCd Is Nothing And _
               Not _koujiKaisyaJigyousyoCd Is Nothing Then
                TextKoujigaisyaCd.Text = _koujiKaisyaCd.Trim() + _koujiKaisyaJigyousyoCd.Trim()
            End If
        End Set
    End Property

    ''' <summary>
    ''' �H����Ў��Ə��R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private _koujiKaisyaJigyousyoCd As String
    ''' <summary>
    ''' �H����Ў��Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KoujiKaisyaJigyousyoCd() As String
        Get
            Return IIf(TextKoujigaisyaCd.Text.Trim() = "", "", Mid(TextKoujigaisyaCd.Text & "       ", 5, 2))
        End Get
        Set(ByVal value As String)
            _koujiKaisyaJigyousyoCd = value

            If Not _koujiKaisyaCd Is Nothing And _
               Not _koujiKaisyaJigyousyoCd Is Nothing Then
                TextKoujigaisyaCd.Text = _koujiKaisyaCd.Trim() + _koujiKaisyaJigyousyoCd.Trim()
            End If
        End Set
    End Property

    ''' <summary>
    ''' �H����Ж�
    ''' </summary>
    ''' <remarks></remarks>
    Private _koujiKaisyaMei As String
    ''' <summary>
    ''' �H����Ж�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KoujiKaisyaMei() As String
        Get
            Return _koujiKaisyaMei
        End Get
        Set(ByVal value As String)
            _koujiKaisyaMei = value

            If Not _koujiKaisyaMei Is Nothing Then
                TextKoujigaisyaMei.Text = _koujiKaisyaMei
            End If
        End Set
    End Property

    ''' <summary>
    ''' �H����А����L��
    ''' </summary>
    ''' <remarks></remarks>
    Private _koujiKaisyaSeikyuuUmu As Integer
    ''' <summary>
    ''' �H����А����L��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KoujiKaisyaSeikyuuUmu() As Integer
        Get
            Return IIf(CheckKoujigaisyaSeikyuu.Checked, 1, Integer.MinValue)
        End Get
        Set(ByVal value As Integer)
            _koujiKaisyaSeikyuuUmu = value
            CheckKoujigaisyaSeikyuu.Checked = (_koujiKaisyaSeikyuuUmu = 1)
        End Set
    End Property

    ''' <summary>
    ''' �����z
    ''' </summary>
    ''' <remarks></remarks>
    Private _nyuukinGaku As Integer
    ''' <summary>
    ''' �����z
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NyuukinGaku() As Integer
        Get
            Return _nyuukinGaku
        End Get
        Set(ByVal value As Integer)
            _nyuukinGaku = value
            NyuukinZangakuCtrlKouji.NyuukinGaku = _nyuukinGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        End Set
    End Property

    ''' <summary>
    ''' �c�z
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ZanGaku() As NyuukinZangakuCtrl
        Get
            Return NyuukinZangakuCtrlKouji
        End Get
        Set(ByVal value As NyuukinZangakuCtrl)
            NyuukinZangakuCtrlKouji = value
        End Set
    End Property

#Region "�ō����z"
    ''' <summary>
    ''' �ō����z
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ZeikomiKingaku() As Integer
        Get
            Dim strZeikomi As String = IIf(TextUriageZeikomiKingaku.Text.Replace(",", "").Trim() = "", _
                                           "0", _
                                           TextUriageZeikomiKingaku.Text.Replace(",", "").Trim())
            Return Integer.Parse(strZeikomi)
        End Get
    End Property
#End Region

#End Region

#Region "�C�x���g"
    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g�i������E�d������ݒ�p�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Public Event SetSeikyuuSiireSakiAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)

    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g�i�H�����i�}�X�^�擾�A�N�V�����p�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Public Event GetKojMInfoAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)

    ''' <summary>
    ''' �y�[�W���[�h���̃C�x���g�n���h��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '�}�X�^�[�y�[�W�����擾�iScriptManager�p�j
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        If IsPostBack = False Then '�����Ǎ���

            ' ��ʕ\���ݒ�
            Select Case Me.DispMode
                Case DisplayMode.KAIRYOU
                    ' ���ǍH���̏ꍇ
                    CtrlTitle.InnerText = EarthConst.CTRL_TITLE_KAIRYOU
                    KoujigaisyaTitle.InnerText = EarthConst.CTRL_KOUJI_KAIRYOU
                    HiddenBunruiCd.Value = EarthConst.SOUKO_CD_KAIRYOU_KOUJI   ' ���ރR�[�h
                Case DisplayMode.TUIKA
                    ' �ǉ����ǍH���̏ꍇ
                    CtrlTitle.InnerText = EarthConst.CTRL_TITLE_TUIKA
                    KoujigaisyaTitle.InnerText = EarthConst.CTRL_KOUJI_TUIKA
                    HiddenBunruiCd.Value = EarthConst.SOUKO_CD_TUIKA_KOUJI   ' ���ރR�[�h
            End Select

            If _teibetuSeikyuuRec Is Nothing Then
                ' �R���g���[���̔񊈐���
                EnabledCtrl(False)
            End If

            If TextKoujigaisyaCd.Text <> "" Then
                ' �H����Ќ���
                ButtonKoujigaisyaKensaku_ServerClick(sender, e)
            End If

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            Me.SetDispAction()

            '��ʕ\�����_�̒l���AHidden�ɕێ�(���� �ύX�`�F�b�N�p)
            If HiddenOpenValuesUriage.Value = String.Empty Then
                HiddenOpenValuesUriage.Value = getCtrlValuesStringUriage()
            End If

            '��ʕ\�����_�̒l���AHidden�ɕێ�(�d�� �ύX�`�F�b�N�p)
            If HiddenOpenValuesSiire.Value = String.Empty Then
                HiddenOpenValuesSiire.Value = getCtrlValuesStringSiire()
            End If

            '��ʕ\�����_�̒l���AHidden�ɕێ�(�d�� �ύX�`�F�b�N�p)
            If Me.HiddenOpenValue.Value = String.Empty Then
                Me.HiddenOpenValue.Value = Me.getCtrlValuesStringAll()
            End If

        End If
    End Sub

    ''' <summary>
    ''' �y�[�W�\�����O�̃C�x���g�n���h��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        '����������
        Me.EnabledCtrlKengen()

    End Sub

    ''' <summary>
    ''' �H����Ќ����{�^���������̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKoujigaisyaKensaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^���擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        ' ����N�����̂�
        If IsPostBack = False Then
            blnTorikesi = False
        End If

        If TextKoujigaisyaCd.Text <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetKoujikaishaSearchResult(TextKoujigaisyaCd.Text, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi, _
                                                                            HiddenKameitenCd.Value)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            HiddenKoujigaisyaCdOld.Value = recData.TysKaisyaCd & recData.JigyousyoCd
            TextKoujigaisyaCd.Text = recData.TysKaisyaCd & recData.JigyousyoCd
            TextKoujigaisyaMei.Text = recData.TysKaisyaMei
            ' �H�����NG�ݒ�
            If recData.KahiKbn = 9 Then
                HiddenNg.Value = EarthConst.KOUJI_KAISYA_NG
                TextKoujigaisyaCd.Style("color") = "red"
                TextKoujigaisyaMei.Style("color") = "red"
            Else
                HiddenNg.Value = String.Empty
                TextKoujigaisyaCd.Style("color") = "blue"
                TextKoujigaisyaMei.Style("color") = "blue"
            End If

            '********************************************************************
            '�������ǂ݈ȊO�A�H�����i�}�X�^���牿�i�擾
            If IsPostBack = True Then
                '�e��ʂփC�x���g�ʒm(�H�����i���i�ݒ�)
                RaiseEvent GetKojMInfoAction(sender, e, Me.ClientID)

                ' ���i�E������z�E�����L���̎����ݒ�i���iM or �H�����iM�j
                If SetSyouhinInfoFromKojM(EarthEnum.emKojKkkActionType.KojKaisyaCd) = False Then '�H�����iM
                    '���H���ȊO�̏ꍇ�͉��i�擾���s�Ȃ�Ȃ�
                End If
            End If

            '�t�H�[�J�X�Z�b�g
            SetFocus(ButtonKoujigaisyaKensaku)
        Else
            HiddenKoujigaisyaCdOld.Value = String.Empty
            TextKoujigaisyaMei.Text = String.Empty

            If blnTorikesi = False Then
                ' �����N�����͌������Ȃ�
                Return
            End If

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            Dim tmpScript = "callSearch('" & TextKoujigaisyaCd.ClientID & EarthConst.SEP_STRING & _
                                             HiddenKameitenCd.ClientID & "','" & _
                                             UrlConst.SEARCH_KOUJIKAISYA & "','" & _
                                             TextKoujigaisyaCd.ClientID & EarthConst.SEP_STRING & _
                                             TextKoujigaisyaMei.ClientID & EarthConst.SEP_STRING & _
                                             HiddenNg.ClientID & "','" & ButtonKoujigaisyaKensaku.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)

        End If

        '�����d���p�J�X�^���C�x���g
        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

    End Sub

    ''' <summary>
    ''' ���i�R���{�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectSyouhinCd_SelectedIndexChanged(ByVal sender As System.Object, _
                                                       ByVal e As System.EventArgs) _
                                                       Handles SelectSyouhinCd.SelectedIndexChanged

        Dim logic As New TeibetuSyuuseiLogic
        Dim syouhinRec As New SyouhinMeisaiRecord
        '���b�Z�[�W�p
        Dim tmpErrMsg As String = String.Empty
        Dim tmpScriptErr As String = String.Empty

        Dim strZeiKbn As String = String.Empty      '�ŋ敪
        Dim strZeiritu As String = String.Empty     '�ŗ�
        Dim strSyouhinCd As String = String.Empty   '���i�R�[�h

        If SelectSyouhinCd.SelectedValue = "" Then

            Dim hattyuuKingaku As String = IIf(TextHattyuusyoKingaku.Text = "", "0", TextHattyuusyoKingaku.Text)

            If Not hattyuuKingaku = "0" Then
                ' �󔒑I���Ŕ��������z���O�ȊO�̏ꍇ�A���ɖ߂�
                SelectSyouhinCd.SelectedValue = HiddenSyouhinCd.Value

                ' ���b�Z�[�W��\��
                Dim tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                ScriptManager.RegisterStartupScript(sender, sender.GetType(), "err", tmpScript, True)

                ' ���i�R�[�h�Ƀt�H�[�J�X�Z�b�g
                SetFocus(SelectSyouhinCd)

                ' ���i�R�[�h��ύX����̂ŏ��i�R�[�h��Update�p�l�����X�V
                UpdatePanelSyouhinCd.Update()

                Return
            Else
                ' �f�[�^�̃N���A
                ' ����
                TextUriageZeinukiKingaku.Text = ""          ' ����Ŕ����z
                HiddenZeiritu.Value = ""                    ' �ŗ�(Hidden)
                HiddenBunruiCd.Value = ""                   ' ���ރR�[�h�iHidden�j
                TextUriageZeikomiKingaku.Text = ""          ' ����ō����z
                TextUriageSyouhizeiGaku.Text = ""           ' �������Ŋz
                TextSeikyuusyoHakkoubi.Text = ""            ' ���������s��
                TextUriageNengappi.Text = ""                ' ����N����
                TextDenpyouUriageNengappi.Text = ""         ' �`�[����N����
                TextDenpyouSiireNengappi.Text = ""          ' �`�[�d���N����
                SelectSeikyuuUmu.SelectedValue = "1"        ' �����L��
                SelectUriageSyori.SelectedValue = "0"       ' ���㏈��
                SelectHattyuusyoKakutei.SelectedValue = "0" ' �������m��
                ' Old�l�Ɍ��݂̒l���Z�b�g
                HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue
                TextHattyuusyoKakuninbi.Text = ""           ' �������m�F��
                TextHattyuusyoKingaku.Text = ""             ' ���������z
                TextUriagebi.Text = ""                      ' ����N����
                ' �d��
                TextSiireZeinukiKingaku.Text = ""           ' �d���Ŕ����z
                TextSiireSyouhizeiGaku.Text = ""            ' �d������Ŋz
                TextSiireZeikomiKingaku.Text = ""           ' �d���ō����z

                '�����d���p�J�X�^���C�x���g
                RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

                EnabledCtrl(False)

                Return
            End If
        End If

        '********************************************************************
        '�����i�}�X�^�ƍH�����i�}�X�^����̎擾��؂�ւ���
        '�e��ʂփC�x���g�ʒm(�H�����i���i�ݒ�)
        RaiseEvent GetKojMInfoAction(sender, e, Me.ClientID)

        ' ���i�E������z�E�����L���̎����ݒ�i���iM or �H�����iM�j
        If SetSyouhinInfoFromKojM(EarthEnum.emKojKkkActionType.SyouhinCd) = True Then '�H�����iM

            ' �R���g���[���̊�����
            EnabledCtrl(True)

            '���i�R�[�h(Hidden)
            Me.HiddenSyouhinCd.Value = SelectSyouhinCd.SelectedValue

            '���ރR�[�h(Hidden)
            Select Case Me.DispMode
                Case DisplayMode.KAIRYOU
                    ' ���ǍH���̏ꍇ
                    HiddenBunruiCd.Value = EarthConst.SOUKO_CD_KAIRYOU_KOUJI   ' ���ރR�[�h
                Case DisplayMode.TUIKA
                    ' �ǉ����ǍH���̏ꍇ
                    HiddenBunruiCd.Value = EarthConst.SOUKO_CD_TUIKA_KOUJI   ' ���ރR�[�h
            End Select

            ' �����i�d���j�̋��z�Đݒ�
            SetSiireZeigaku()
            '�����d���p�J�X�^���C�x���g
            RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

        Else ' ���iM
            If Me.DispMode = DisplayMode.KAIRYOU Then
                syouhinRec = logic.GetSyouhinInfo(SelectSyouhinCd.SelectedValue, _
                                      EarthEnum.EnumSyouhinKubun.KairyouKouji)
            Else
                syouhinRec = logic.GetSyouhinInfo(SelectSyouhinCd.SelectedValue, _
                                                  EarthEnum.EnumSyouhinKubun.TuikaKouji)
            End If

            ' ���i���擾�ł����ꍇ�A��ʍ��ڂɐݒ肷��
            If syouhinRec Is Nothing Then
                ' �R���g���[���̔񊈐���
                EnabledCtrl(False)
                ' �f�[�^�̃N���A
                Me.SelectSyouhinCd.SelectedValue = String.Empty ' ���i�R�[�h
                Me.HiddenZeiritu.Value = String.Empty '�ŗ�
                Me.HiddenZeiKbn.Value = String.Empty '�ŋ敪
                Me.HiddenBunruiCd.Value = String.Empty '���ރR�[�h

                ' ����
                TextUriageZeinukiKingaku.Text = ""          ' ����Ŕ����z
                HiddenZeiritu.Value = ""                    ' �ŗ�(Hidden)
                HiddenBunruiCd.Value = ""                   ' ���ރR�[�h�iHidden�j
                TextUriageZeikomiKingaku.Text = ""          ' ����ō����z
                TextUriageSyouhizeiGaku.Text = ""           ' �������Ŋz
                TextSeikyuusyoHakkoubi.Text = ""            ' ���������s��
                TextUriageNengappi.Text = ""                ' ����N����
                TextDenpyouUriageNengappi.Text = ""         ' �`�[����N����
                TextDenpyouSiireNengappi.Text = ""          ' �`�[�d���N����
                SelectSeikyuuUmu.SelectedValue = "1"        ' �����L��
                SelectUriageSyori.SelectedValue = "0"       ' ���㏈��
                SelectHattyuusyoKakutei.SelectedValue = "0" ' �������m��
                ' Old�l�Ɍ��݂̒l���Z�b�g
                HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue
                TextHattyuusyoKakuninbi.Text = ""           ' �������m�F��
                TextHattyuusyoKingaku.Text = ""             ' ���������z
                TextUriagebi.Text = ""                      ' ���㏈����

                ' �d��
                TextSiireZeinukiKingaku.Text = ""           ' �d���Ŕ����z
                TextSiireSyouhizeiGaku.Text = ""            ' �d������Ŋz
                TextSiireZeikomiKingaku.Text = ""           ' �d���ō����z

                '���G���[���b�Z�[�W�\��
                tmpErrMsg = Messages.MSG163E.Replace("@PARAM1", "���i�}�X�^")
                tmpScriptErr = "alert('" & tmpErrMsg & "');"
                ScriptManager.RegisterStartupScript(sender, sender.GetType(), "SetSyouhinInfo", tmpScriptErr, True)

            Else
                ' �R���g���[���̊�����
                EnabledCtrl(True)

                '����N�����Ŕ��f���āA�������ŗ����擾����
                strSyouhinCd = Me.SelectSyouhinCd.SelectedValue
                If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                    '�擾�����ŋ敪�E�ŗ����Z�b�g
                    syouhinRec.Zeiritu = strZeiritu
                    syouhinRec.ZeiKbn = strZeiKbn
                End If

                '�ŗ�
                Dim zeiritu As Decimal = IIf(syouhinRec.Zeiritu = Decimal.MinValue, 0, syouhinRec.Zeiritu)
                HiddenZeiritu.Value = zeiritu.ToString()

                '�ŋ敪
                Dim zeikbn As String = IIf(syouhinRec.ZeiKbn = Nothing, "", syouhinRec.ZeiKbn)
                HiddenZeiKbn.Value = zeikbn

                HiddenBunruiCd.Value = syouhinRec.SoukoCd
                HiddenSyouhinCd.Value = syouhinRec.SyouhinCd

                ' �f�[�^�̎����ݒ�(�����L��̏ꍇ�̂�)
                If SelectSeikyuuUmu.SelectedValue = "1" Then
                    '���������z 
                    Dim uriage As Integer = IIf(syouhinRec.HyoujunKkk = Integer.MinValue, 0, syouhinRec.HyoujunKkk)
                    TextUriageZeinukiKingaku.Text = uriage.ToString(EarthConst.FORMAT_KINGAKU_1)

                    '���z�ݒ�
                    SetKingaku()
                End If

                ' �����i�d���j�̋��z�Đݒ�
                SetSiireZeigaku()
                '�����d���p�J�X�^���C�x���g
                RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)
            End If
        End If

        ' �ō����z�ύX��e�R���g���[���ɒʒm����
        RaiseEvent ChangeZeikomiGaku(Me, e, CInt(IIf(TextUriageZeikomiKingaku.Text = "", "0", TextUriageZeikomiKingaku.Text)))
        Me.UpdatePanelSeikyuuSiireLink.Update()

        SetFocus(SelectSyouhinCd)

    End Sub

    ''' <summary>
    ''' �����L���R���{�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectSeikyuuUmu_SelectedIndexChanged(ByVal sender As System.Object, _
                                                        ByVal e As System.EventArgs) _
                                                        Handles SelectSeikyuuUmu.SelectedIndexChanged

        '���b�Z�[�W�p
        Dim tmpErrMsg As String = String.Empty
        Dim tmpScript As String = String.Empty

        Dim strZeiKbn As String = String.Empty      '�ŋ敪
        Dim strZeiritu As String = String.Empty     '�ŗ�
        Dim strSyouhinCd As String = String.Empty   '���i�R�[�h

        If SelectSeikyuuUmu.SelectedValue = "0" Then
            ' �Ŋz�A�ō����z��0�ɂ���
            TextUriageZeinukiKingaku.Text = "0"
            TextUriageSyouhizeiGaku.Text = "0"
            TextUriageZeikomiKingaku.Text = "0"
        ElseIf TextUriageZeinukiKingaku.Text.Trim() = "" Or _
               TextUriageZeinukiKingaku.Text.Trim() = "0" Then

            ' �����L��ŐŔ����z�����͂̏ꍇ�͎����ݒ�
            Dim logic As New TeibetuSyuuseiLogic
            Dim syouhinRec As New SyouhinMeisaiRecord

            '********************************************************************
            '�����i�}�X�^�ƍH�����i�}�X�^����̎擾��؂�ւ���
            '�e��ʂփC�x���g�ʒm(�H�����i���i�ݒ�)
            RaiseEvent GetKojMInfoAction(sender, e, Me.ClientID)

            ' ���i�E������z�E�����L���̎����ݒ�i���iM or �H�����iM�j
            If SetSyouhinInfoFromKojM(EarthEnum.emKojKkkActionType.SeikyuuUmu) = False Then

                '���iM
                If Me.DispMode = DisplayMode.KAIRYOU Then
                    syouhinRec = logic.GetSyouhinInfo(SelectSyouhinCd.SelectedValue, _
                                          EarthEnum.EnumSyouhinKubun.KairyouKouji)
                Else
                    syouhinRec = logic.GetSyouhinInfo(SelectSyouhinCd.SelectedValue, _
                                                      EarthEnum.EnumSyouhinKubun.TuikaKouji)
                End If

                ' ���i���擾�ł����ꍇ�A���i����ݒ肷��
                If syouhinRec Is Nothing Then
                    '���G���[���b�Z�[�W�\��
                    tmpErrMsg = Messages.MSG163E.Replace("@PARAM1", "���i�}�X�^")
                    tmpScript = "alert('" & tmpErrMsg & "');"
                    ScriptManager.RegisterStartupScript(sender, sender.GetType(), "SetSyouhinInfo", tmpScript, True)

                Else
                    '����N�����Ŕ��f���āA�������ŗ����擾����
                    strSyouhinCd = Me.SelectSyouhinCd.SelectedValue
                    If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                        '�擾�����ŋ敪�E�ŗ����Z�b�g
                        syouhinRec.Zeiritu = strZeiritu
                        syouhinRec.ZeiKbn = strZeiKbn
                    End If

                    '�f�[�^�̎����ݒ�
                    '���������z
                    Dim uriage As Integer = IIf(syouhinRec.HyoujunKkk = Integer.MinValue, 0, syouhinRec.HyoujunKkk)
                    '�ŗ�
                    Dim zeiritu As Decimal = IIf(syouhinRec.Zeiritu = Decimal.MinValue, 0, syouhinRec.Zeiritu)
                    '�ŋ敪
                    Dim zeikbn As String = IIf(syouhinRec.ZeiKbn = Nothing, "", syouhinRec.ZeiKbn)

                    TextUriageZeinukiKingaku.Text = uriage.ToString(EarthConst.FORMAT_KINGAKU_1)
                    HiddenZeiritu.Value = zeiritu.ToString()
                    HiddenZeiKbn.Value = zeikbn

                    '���z�ݒ�
                    SetKingaku()
                    End If
            End If
        End If
        SetFocus(SelectSeikyuuUmu)

    End Sub

    ''' <summary>
    ''' �d���Ŕ����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSiireZeinukiKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' �d���Ŕ����z�ɓ��͗L��̏ꍇ�A�Ŋz�A�ō����z��ݒ肷��
        If TextSiireZeinukiKingaku.Text.Trim() <> "" Then

            ' �Ŋz�A�ō����z���v�Z
            Dim siire As Integer = Integer.Parse(TextSiireZeinukiKingaku.Text.Replace(",", ""))
            Dim zeiritu As Decimal = Decimal.Parse(HiddenZeiritu.Value)
            Dim zeigaku As Integer = Fix(siire * zeiritu)
            Dim zeikomi As Integer = siire + zeigaku

            ' �Ŋz�A�ō����z��ݒ�
            TextSiireSyouhizeiGaku.Text = zeigaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            TextSiireZeikomiKingaku.Text = zeikomi.ToString(EarthConst.FORMAT_KINGAKU_1)
        Else
            ' �Ŋz�A�ō����z���󔒂ɂ���
            TextSiireZeinukiKingaku.Text = ""
            TextSiireSyouhizeiGaku.Text = ""
            TextSiireZeikomiKingaku.Text = ""
        End If

        SetFocus(TextSiireSyouhizeiGaku)

    End Sub

    ''' <summary>
    ''' �d������Ŋz�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSiireSyouhizeiGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If sender.Text.Trim() = "" Then
            sender.Text = "0"
        End If

        If TextSiireZeinukiKingaku.Text.Trim() <> "" Then
            '�d���ō����z���v�Z
            Dim zeikomi As Integer = Integer.Parse(TextSiireZeinukiKingaku.Text.Replace(",", "")) + Integer.Parse(sender.Text.Replace(",", ""))
            '�d���ō��z�X�V
            TextSiireZeikomiKingaku.Text = zeikomi.ToString(EarthConst.FORMAT_KINGAKU_1)
        Else
            ' �Ŋz�A�ō����z���󔒂ɂ���
            TextSiireZeinukiKingaku.Text = ""
            TextSiireSyouhizeiGaku.Text = ""
            TextSiireZeikomiKingaku.Text = ""
        End If

        '�t�H�[�J�X�Z�b�g
        SetFocus(TextDenpyouSiireNengappi)

    End Sub

    ''' <summary>
    ''' ����Ŕ����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextUriageZeinukiKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' ����Ŕ����z�ɓ��͗L��̏ꍇ�A�Ŋz�A�ō����z��ݒ肷��
        If TextUriageZeinukiKingaku.Text.Trim() <> "" Then

            '���z�ݒ�
            SetKingaku()

            ' �ō����z�ύX��e�R���g���[���ɒʒm����
            RaiseEvent ChangeZeikomiGaku(Me, e, Integer.Parse(TextUriageZeikomiKingaku.Text.Replace(",", "")))
        Else
            ' �Ŋz�A�ō����z���󔒂ɂ���
            TextUriageSyouhizeiGaku.Text = ""
            TextUriageZeikomiKingaku.Text = ""
            ' �ō����z�ύX��e�R���g���[���ɒʒm����
            RaiseEvent ChangeZeikomiGaku(Me, e, 0)
        End If

        SetFocus(TextUriageSyouhizeiGaku)

    End Sub

    ''' <summary>
    ''' �������Ŋz�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextUriageSyouhizeiGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim strTmpUriGaku As String = Me.TextUriageZeinukiKingaku.Text.Trim

        ' ����Ŕ����z�ɓ��͗L��̏ꍇ�A�Ŋz�A�ō����z��ݒ肷��
        If strTmpUriGaku <> String.Empty And strTmpUriGaku <> "0" Then

            If TextUriageSyouhizeiGaku.Text.Trim() = "" Then
                TextUriageSyouhizeiGaku.Text = "0"
            End If

            '���z�ݒ�
            SetKingaku(True)

            ' �ō����z�ύX��e�R���g���[���ɒʒm����
            RaiseEvent ChangeZeikomiGaku(Me, e, Integer.Parse(TextUriageZeikomiKingaku.Text.Replace(",", "")))

        ElseIf strTmpUriGaku = "0" Then
            ' �Ŋz�A�ō����z���󔒂ɂ���
            TextUriageSyouhizeiGaku.Text = "0"
            TextUriageZeikomiKingaku.Text = "0"
            ' �ō����z�ύX��e�R���g���[���ɒʒm����
            RaiseEvent ChangeZeikomiGaku(Me, e, 0)

        Else
            ' �Ŋz�A�ō����z���󔒂ɂ���
            TextUriageSyouhizeiGaku.Text = ""
            TextUriageZeikomiKingaku.Text = ""
            ' �ō����z�ύX��e�R���g���[���ɒʒm����
            RaiseEvent ChangeZeikomiGaku(Me, e, 0)
        End If

        SetFocus(TextDenpyouUriageNengappi)

    End Sub

    ''' <summary>
    ''' ���㏈���ύX���̃C�x���g�n���h��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectUriageSyori_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If SelectUriageSyori.SelectedValue = "0" Then
            ' ���㏈�������N���A
            TextUriagebi.Text = ""
        Else
            ' �V�X�e�����t���Z�b�g
            TextUriagebi.Text = Date.Now.ToString("yyyy/MM/dd")
        End If
        SetFocus(SelectUriageSyori)
    End Sub

    ''' <summary>
    ''' ���������z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextHattyuusyoKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' �����������m�菈��
        HattyuuAfterUpdate(sender)
        SetFocus(SelectHattyuusyoKakutei)

    End Sub

    ''' <summary>
    ''' �������m��ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectHattyuusyoKakutei_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' �����������m�菈��
        FunCheckHKakutei(1, sender)

        ' Old�l�Ɍ��݂̒l���Z�b�g
        HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue

        SetFocus(SelectHattyuusyoKakutei)
    End Sub

    ''' <summary>
    ''' �ō����z�̕ύX��e�R���g���[���ɒʒm����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ChangeZeikomiGaku(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs, _
                                   ByVal zeikomigaku As Integer)

    ''' <summary>
    ''' �H����А����`�F�b�N�{�b�N�X�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub CheckKoujigaisyaSeikyuu_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '�����d���p�J�X�^���C�x���g
        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

    End Sub

    ''' <summary>
    ''' ����N�����ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextUriageNengappi_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim strZeiKbn As String = String.Empty      '�ŋ敪
        Dim strZeiritu As String = String.Empty     '�ŗ�
        Dim strSyouhinCd As String = String.Empty   '���i�R�[�h

        '����N����
        If Me.TextUriageNengappi.Text <> String.Empty Then '������
            '�`�[����N����
            If Me.TextDenpyouUriageNengappi.Text = String.Empty Then
                '����N�������Z�b�g
                Me.TextDenpyouUriageNengappi.Text = Me.TextUriageNengappi.Text
                TextDenpyouUriageNengappi_TextChanged(sender, e)
            End If
            '�`�[�d���N����
            If Me.TextDenpyouSiireNengappi.Text = String.Empty Then
                '����N�������Z�b�g
                Me.TextDenpyouSiireNengappi.Text = Me.TextUriageNengappi.Text
            End If

            ' �ŋ敪�E�ŗ����Ď擾
            strSyouhinCd = Me.SelectSyouhinCd.SelectedValue

            If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                ' �擾�����ŋ敪���Z�b�g
                Me.HiddenZeiKbn.Value = strZeiKbn
                Me.HiddenZeiritu.Value = strZeiritu

                ' �����i�d���j�̐Ŋz�Đݒ�
                SetSiireZeigaku()
                ' �������̋��z�Đݒ�
                SetKingaku()
                ' �ō����z�ύX��e�R���g���[���ɒʒm����
                RaiseEvent ChangeZeikomiGaku(Me, e, CInt(IIf(TextUriageZeikomiKingaku.Text = "", "0", TextUriageZeikomiKingaku.Text)))
            End If

        Else
            '����N���������͂̏ꍇ

            ' �ŋ敪�E�ŗ����Ď擾
            strSyouhinCd = Me.SelectSyouhinCd.SelectedValue

            If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                ' �擾�����ŋ敪���Z�b�g
                Me.HiddenZeiKbn.Value = strZeiKbn
                Me.HiddenZeiritu.Value = strZeiritu

                ' �����i�d���j�̐Ŋz�Đݒ�
                SetSiireZeigaku()
                ' �������̋��z�Đݒ�
                SetKingaku()
                ' �ō����z�ύX��e�R���g���[���ɒʒm����
                RaiseEvent ChangeZeikomiGaku(Me, e, CInt(IIf(TextUriageZeikomiKingaku.Text = "", "0", TextUriageZeikomiKingaku.Text)))
            End If
        End If

        Me.UpdatePanelDenpyouUriageNengappi.Update()

        SetFocus(TextUriageNengappi)

    End Sub

    ''' <summary>
    ''' �`�[����N�����ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextDenpyouUriageNengappi_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        '�����d���p�J�X�^���C�x���g
        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

        '�`�[����N����
        If Me.TextDenpyouUriageNengappi.Text <> String.Empty Then
            '�������ߓ��̐ݒ�
            Me.SeikyuuSiireLinkCtrl.SetSeikyuuSimeDate(Me.SelectSyouhinCd.SelectedValue, Me.CheckKoujigaisyaSeikyuu.Checked, Me.TextKoujigaisyaCd.Text)

            ' ���������s���擾
            Dim strHakDate As String = Me.SeikyuuSiireLinkCtrl.GetSeikyuusyoHakkouDate(Me.TextDenpyouUriageNengappi.Text)
            Me.TextSeikyuusyoHakkoubi.Text = strHakDate
        Else
            Me.TextSeikyuusyoHakkoubi.Text = String.Empty
        End If

        '�����L���Ƀt�H�[�J�X
        SetFocus(TextSeikyuusyoHakkoubi)
    End Sub

#End Region

#Region "�v���C�x�[�g���\�b�h"

    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispAction()
        '�C�x���g�n���h����ݒ�
        Dim onFocusPostBackScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScript As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this))__doPostBack(this.id,'');}else{checkNumberAddFig(this);}"
        Dim onFocusScript As String = "removeFig(this);"
        Dim onBlurScript As String = "checkNumberAddFig(this);"
        Dim checkDate As String = "checkDate(this);"
        Dim onFocusPostBackScriptDate As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScriptDate As String = "if(checkTempValueForOnBlur(this)){if(checkDate(this))__doPostBack(this.id,'');}else{checkDate(this);}"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '����Ŕ����z
        TextUriageZeinukiKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextUriageZeinukiKingaku.Attributes("onblur") = onBlurPostBackScript
        TextUriageZeinukiKingaku.Attributes("onkeydown") = disabledOnkeydown
        '�������Ŋz
        TextUriageSyouhizeiGaku.Attributes("onfocus") = onFocusPostBackScript
        TextUriageSyouhizeiGaku.Attributes("onblur") = onBlurPostBackScript
        TextUriageSyouhizeiGaku.Attributes("onkeydown") = disabledOnkeydown
        '���������z
        TextHattyuusyoKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextHattyuusyoKingaku.Attributes("onblur") = onBlurPostBackScript
        TextHattyuusyoKingaku.Attributes("onkeydown") = disabledOnkeydown
        '�d���Ŕ����z
        TextSiireZeinukiKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextSiireZeinukiKingaku.Attributes("onblur") = onBlurPostBackScript
        TextSiireZeinukiKingaku.Attributes("onkeydown") = disabledOnkeydown
        '�d������Ŋz
        TextSiireSyouhizeiGaku.Attributes("onfocus") = onFocusPostBackScript
        TextSiireSyouhizeiGaku.Attributes("onblur") = onBlurPostBackScript
        TextSiireSyouhizeiGaku.Attributes("onkeydown") = disabledOnkeydown
        '���������s��
        TextSeikyuusyoHakkoubi.Attributes("onblur") = checkDate
        TextSeikyuusyoHakkoubi.Attributes("onkeydown") = disabledOnkeydown
        '����N����
        TextUriageNengappi.Attributes("onfocus") = onFocusPostBackScriptDate
        TextUriageNengappi.Attributes("onblur") = onBlurPostBackScriptDate
        TextUriageNengappi.Attributes("onkeydown") = disabledOnkeydown
        '�`�[����N����
        TextDenpyouUriageNengappi.Attributes("onfocus") = onFocusPostBackScriptDate
        TextDenpyouUriageNengappi.Attributes("onblur") = onBlurPostBackScriptDate
        TextDenpyouUriageNengappi.Attributes("onkeydown") = disabledOnkeydown
        '�`�[�d���N����
        TextDenpyouSiireNengappi.Attributes("onblur") = checkDate
        TextDenpyouSiireNengappi.Attributes("onkeydown") = disabledOnkeydown
        '�������m�F��
        TextHattyuusyoKakuninbi.Attributes("onblur") = checkDate
        TextHattyuusyoKakuninbi.Attributes("onkeydown") = disabledOnkeydown
        '�H����ЃR�[�h
        TextKoujigaisyaCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){checkNumber(this);}"
        TextKoujigaisyaCd.Attributes("onkeydown") = disabledOnkeydown

    End Sub

#Region "�@�ʐ������R�[�h�ҏW"
    ''' <summary>
    ''' �@�ʐ������R�[�h�f�[�^���R���g���[���ɃZ�b�g���܂�
    ''' </summary>
    ''' <param name="data"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlData(ByVal data As TeibetuSeikyuuRecord)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetCtrlData", _
                                            data)

        ' �敪�iHidden�j
        HiddenKubun.Value = data.Kbn
        ' �ԍ��i�ۏ؏�NO�j�iHidden�j
        HiddenBangou.Value = data.HosyousyoNo
        ' ���i�R�[�h�iHidden�j
        HiddenSyouhinCd.Value = data.SyouhinCd
        '���i�R�[�h�̑��݃`�F�b�N
        If cLogic.ChkDropDownList(SelectSyouhinCd, data.SyouhinCd) Then
            SelectSyouhinCd.SelectedValue = cLogic.GetDispStr(data.SyouhinCd) '���i�R�[�h
        Else '�����݂̏ꍇ�A���ڒǉ�
            SelectSyouhinCd.Items.Add(New ListItem(data.SyouhinCd & ":" & data.SyouhinMei, data.SyouhinCd)) '���i�R�[�h
            SelectSyouhinCd.SelectedValue = data.SyouhinCd  '�I�����
        End If

        ' �Ŕ�������z
        TextUriageZeinukiKingaku.Text = IIf(data.UriGaku = Integer.MinValue, _
                                            0, _
                                            data.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' �������Ŋz
        TextUriageSyouhizeiGaku.Text = IIf(data.UriageSyouhiZeiGaku = Integer.MinValue, 0, Format(data.UriageSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        ' �ō�������z
        TextUriageZeikomiKingaku.Text = IIf(data.ZeikomiUriGaku = Integer.MinValue, _
                                            0, _
                                            data.ZeikomiUriGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' ���������s��
        TextSeikyuusyoHakkoubi.Text = IIf(data.SeikyuusyoHakDate = Date.MinValue, _
                                          "", _
                                          data.SeikyuusyoHakDate.ToString("yyyy/MM/dd"))
        ' ����N����
        TextUriageNengappi.Text = IIf(data.UriDate = Date.MinValue, _
                                      "", _
                                      data.UriDate.ToString("yyyy/MM/dd"))
        ' �`�[����N����(�Q�Ɨp)
        TextDenpyouUriageNengappiDisplay.Text = IIf(data.DenpyouUriDate = Date.MinValue, _
                                                    "", _
                                                    data.DenpyouUriDate.ToString("yyyy/MM/dd"))
        ' �`�[����N����(�C���p)
        TextDenpyouUriageNengappi.Text = IIf(data.DenpyouUriDate = Date.MinValue, _
                                             "", _
                                             data.DenpyouUriDate.ToString("yyyy/MM/dd"))
        ' �`�[�d���N����(�Q�Ɨp)
        TextDenpyouSiireNengappiDisplay.Text = IIf(data.DenpyouSiireDate = Date.MinValue, _
                                                   "", _
                                                   data.DenpyouSiireDate.ToString("yyyy/MM/dd"))
        ' �`�[�d���N����(�C���p)
        TextDenpyouSiireNengappi.Text = IIf(data.DenpyouSiireDate = Date.MinValue, _
                                            "", _
                                            data.DenpyouSiireDate.ToString("yyyy/MM/dd"))
        ' �����L���h���b�v�_�E��
        SelectSeikyuuUmu.SelectedValue = IIf(data.SeikyuuUmu = 1, "1", "0")
        ' ���㏈���h���b�v�_�E��
        SelectUriageSyori.SelectedValue = IIf(data.UriKeijyouFlg = 1, "1", "0")
        ' ������i�ҏW�s�j
        TextUriagebi.Text = IIf(data.UriKeijyouDate = Date.MinValue, _
                                "", _
                                data.UriKeijyouDate.ToString("yyyy/MM/dd"))
        ' ���������z
        TextHattyuusyoKingaku.Text = IIf(data.HattyuusyoGaku = Integer.MinValue, _
                                         "", _
                                         data.HattyuusyoGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' ���������z(��ʋN�����̒l)
        HiddenHattyuusyoKingakuOld.Value = TextHattyuusyoKingaku.Text

        ' �������m��h���b�v�_�E��
        SelectHattyuusyoKakutei.SelectedValue = IIf(data.HattyuusyoKakuteiFlg = 1, "1", "0")
        ' Old�l�Ɍ��݂̒l���Z�b�g
        HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue

        '�������m��ς݂̏ꍇ�A���������z��ҏW�s�ɐݒ�
        If SelectHattyuusyoKakutei.SelectedValue = "1" Then
            EnableTextBox(TextHattyuusyoKingaku, False)

            ' �������m��ς݂̏ꍇ�A�o�������������ꍇ�A�������m����񊈐���
            If HiddenKeiriGyoumuKengen.Value = "0" Then
                EnableDropDownList(SelectHattyuusyoKakutei, False)
            End If
        End If

        ' �������m�F��
        TextHattyuusyoKakuninbi.Text = IIf(data.HattyuusyoKakuninDate = Date.MinValue, _
                                           "", _
                                           data.HattyuusyoKakuninDate.ToString("yyyy/MM/dd"))
        ' �Ŕ��d�����z
        TextSiireZeinukiKingaku.Text = IIf(data.SiireGaku = Integer.MinValue, _
                                           0, _
                                           data.SiireGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' �d������Ŋz
        If TextSiireZeinukiKingaku.Text <> "" Then
            TextSiireSyouhizeiGaku.Text = IIf(data.SiireSyouhiZeiGaku = Integer.MinValue, _
                                              0, _
                                              data.SiireSyouhiZeiGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        End If
        ' �d���ō����z
        TextSiireZeikomiKingaku.Text = (data.SiireGaku + data.SiireSyouhiZeiGaku).ToString(EarthConst.FORMAT_KINGAKU_1)
        ' �ŗ��iHidden�j
        HiddenZeiritu.Value = data.Zeiritu.ToString()
        ' �ŋ敪�iHidden�j
        HiddenZeiKbn.Value = IIf(data.ZeiKbn Is Nothing, "", data.ZeiKbn)
        ' �X�V�����iHidden�j
        If data.UpdDatetime = DateTime.MinValue Then
            HiddenUpdDatetime.Value = data.AddDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)
        Else
            HiddenUpdDatetime.Value = data.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)
        End If

        '������/�d���惊���N�֓@�ʐ������R�[�h�̒l���Z�b�g
        Me.SeikyuuSiireLinkCtrl.SetSeikyuuSiireLinkFromTeibetuRec(data)

    End Sub

    ''' <summary>
    ''' �@�ʐ������R�[�h�f�[�^�ɃR���g���[���̓��e���Z�b�g���܂�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCtrlData() As TeibetuSeikyuuRecord

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCtrlData")

        ' ���i�R�[�h���ݒ莞�A�����Z�b�g���Ȃ�
        If SelectSyouhinCd.SelectedValue = "" Then
            Return Nothing
        End If

        ' �@�ʐ������R�[�h
        Dim record As New TeibetuSeikyuuRecord

        ' �敪
        If ViewState("Kbn") Is Nothing Then
            record.Kbn = HiddenKubun.Value
        Else
            record.Kbn = ViewState("Kbn")
        End If

        ' �ۏ؏�NO
        If ViewState("no") Is Nothing Then
            record.HosyousyoNo = HiddenBangou.Value
        Else
            record.HosyousyoNo = ViewState("no")
        End If

        ' ���i�R�[�h
        record.SyouhinCd = SelectSyouhinCd.SelectedValue
        ' ���i��
        record.SyouhinMei = SelectSyouhinCd.Text
        ' �m��敪(�Œ�)
        record.KakuteiKbn = Integer.MinValue
        ' �H���X�����z
        record.KoumutenSeikyuuGaku = 0
        ' ���������z
        Dim strUriGaku As String = TextUriageZeinukiKingaku.Text.Replace(",", "").Trim()
        If strUriGaku.Trim() = "" Then
            record.UriGaku = Integer.MinValue
        Else
            record.UriGaku = Integer.Parse(strUriGaku)
        End If
        ' �ŗ�
        record.Zeiritu = Decimal.Parse(HiddenZeiritu.Value)
        ' �ŋ敪
        record.ZeiKbn = HiddenZeiKbn.Value
        ' ����Ŋz
        Dim strSyouhizeiGaku As String = TextUriageSyouhizeiGaku.Text.Replace(",", "").Trim()
        If strSyouhizeiGaku.Trim() = "" Then
            record.UriageSyouhiZeiGaku = Integer.MinValue
        Else
            record.UriageSyouhiZeiGaku = Integer.Parse(strSyouhizeiGaku)
        End If
        ' �d������Ŋz
        Dim strSiireSyouhizeiGaku As String = TextSiireSyouhizeiGaku.Text.Replace(",", "").Trim()
        If strSiireSyouhizeiGaku.Trim() = "" Then
            record.SiireSyouhiZeiGaku = Integer.MinValue
        Else
            record.SiireSyouhiZeiGaku = Integer.Parse(strSiireSyouhizeiGaku)
        End If
        ' ���������z
        Dim strSiireGaku As String = TextSiireZeinukiKingaku.Text.Replace(",", "").Trim()
        If strSiireGaku.Trim() = "" Then
            record.SiireGaku = Integer.MinValue
        Else
            record.SiireGaku = Integer.Parse(strSiireGaku)
        End If
        ' ���������s��
        If Not TextSeikyuusyoHakkoubi.Text.Trim() = "" Then
            record.SeikyuusyoHakDate = Date.Parse(TextSeikyuusyoHakkoubi.Text)
        End If
        ' ����N����
        If Not TextUriageNengappi.Text.Trim() = "" Then
            record.UriDate = Date.Parse(TextUriageNengappi.Text)
        End If
        ' �`�[����N����(�C���p)
        If Not TextDenpyouUriageNengappi.Text.Trim() = "" Then
            record.DenpyouUriDate = Date.Parse(TextDenpyouUriageNengappi.Text)
        End If
        ' �`�[�d���N����(�C���p)
        If Not TextDenpyouSiireNengappi.Text.Trim() = "" Then
            record.DenpyouSiireDate = Date.Parse(TextDenpyouSiireNengappi.Text)
        End If
        ' �����L��
        record.SeikyuuUmu = IIf(SelectSeikyuuUmu.SelectedValue = "1", 1, 0)
        ' ���㏈��
        record.UriKeijyouFlg = IIf(SelectUriageSyori.SelectedValue = "1", 1, 0)
        ' ����v���
        If Not TextUriagebi.Text.Trim() = "" Then
            record.UriKeijyouDate = Date.Parse(TextUriagebi.Text)
        End If
        ' ���������z
        Dim strHattyuusyoGaku As String = TextHattyuusyoKingaku.Text.Replace(",", "").Trim()
        If strHattyuusyoGaku.Trim() = "" Then
            record.HattyuusyoGaku = Integer.MinValue
        Else
            record.HattyuusyoGaku = Integer.Parse(strHattyuusyoGaku)
        End If
        ' �������m��
        record.HattyuusyoKakuteiFlg = IIf(SelectHattyuusyoKakutei.SelectedValue = "1", 1, 0)
        ' �������m�F��
        If Not TextHattyuusyoKakuninbi.Text.Trim() = "" Then
            record.HattyuusyoKakuninDate = Date.Parse(TextHattyuusyoKakuninbi.Text)
        End If
        ' ��ʕ\��NO
        record.GamenHyoujiNo = 1
        ' ���ރR�[�h
        record.BunruiCd = HiddenBunruiCd.Value
        ' �X�V�����iHidden�j
        If HiddenUpdDatetime.Value = "" Then
            record.UpdDatetime = DateTime.MinValue
        Else
            record.UpdDatetime = HiddenUpdDatetime.Value
        End If
        ' �X�V�҂h�c
        record.UpdLoginUserId = HiddenLoginUserId.Value

        '������/�d���惊���N�̏���@�ʐ������R�[�h�փZ�b�g
        Me.SeikyuuSiireLinkCtrl.SetTeibetuRecFromSeikyuuSiireLink(record)

        Return record

    End Function

#End Region

    ''' <summary>
    ''' Ajax���쎞�̃t�H�[�J�X�Z�b�g
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub SetFocus(ByVal ctrl As Control)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetFocus", _
                                                    ctrl)
        '�t�H�[�J�X�Z�b�g
        masterAjaxSM.SetFocus(ctrl)
    End Sub

    ''' <summary>
    ''' �R���g���[���̊�������
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnabledCtrl(ByVal enabled As Boolean)

        SelectSeikyuuUmu.Enabled = enabled             ' �����L��
        SelectUriageSyori.Enabled = enabled            ' ���㏈��
        SelectHattyuusyoKakutei.Enabled = enabled      ' �������m��

        TextUriageZeinukiKingaku.Enabled = enabled     ' ����Ŕ����z
        TextUriageSyouhizeiGaku.Enabled = enabled      ' �������Ŋz
        TextUriageZeikomiKingaku.Enabled = enabled     ' ����ō����z
        TextSeikyuusyoHakkoubi.Enabled = enabled       ' ���������s��
        TextUriageNengappi.Enabled = enabled           ' ����N����
        TextDenpyouUriageNengappi.Enabled = enabled    ' �`�[����N����
        TextDenpyouSiireNengappi.Enabled = enabled     ' �`�[�d���N����
        TextHattyuusyoKakuninbi.Enabled = enabled      ' �������m�F��
        TextHattyuusyoKingaku.Enabled = enabled        ' ���������z
        TextSiireZeinukiKingaku.Enabled = enabled      ' �d���Ŕ����z
        TextSiireSyouhizeiGaku.Enabled = enabled       ' �d������Ŋz

    End Sub

    ''' <summary>
    ''' �e�L�X�g�{�b�N�X�P�̂̊�������
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableTextBox(ByRef ctrl As TextBox, _
                              ByVal enabled As Boolean)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnableTextBox", _
                                                    ctrl, enabled)

        ctrl.ReadOnly = Not enabled

        If enabled Then
            ctrl.BackColor = Drawing.Color.White
            ctrl.BorderStyle = BorderStyle.NotSet
            ctrl.TabIndex = 0
        Else
            ctrl.BackColor = Drawing.Color.Transparent
            ctrl.BorderStyle = BorderStyle.None
            ctrl.TabIndex = -1
        End If

    End Sub

    ''' <summary>
    ''' �h���b�v�_�E���P�̂̊�������
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableDropDownList(ByRef ctrl As DropDownList, _
                              ByVal enabled As Boolean)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnableDropDownList", _
                                                    ctrl, enabled)

        ctrl.Enabled = enabled

        If enabled Then
            ctrl.BackColor = Drawing.Color.White
            ctrl.BorderStyle = BorderStyle.NotSet
            ctrl.TabIndex = 0
        Else
            ctrl.BackColor = Drawing.Color.Transparent
            ctrl.BorderStyle = BorderStyle.None
            ctrl.TabIndex = -1
        End If

    End Sub

    ''' <summary>
    ''' �`�F�b�N�{�b�N�X�P�̂̊�������
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableCheckBox(ByRef ctrl As CheckBox, _
                              ByVal enabled As Boolean)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnableDropDownList", _
                                                    ctrl, enabled)

        ctrl.Enabled = enabled

        If enabled Then
            ctrl.TabIndex = 0
        Else
            ctrl.TabIndex = -1
        End If

    End Sub

    ''' <summary>
    ''' �R���g���[���̗L��������ؑւ���[������]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EnabledCtrlKengen()

        ' �o�������������ꍇ�A�������ȊO�񊈐�
        If HiddenKeiriGyoumuKengen.Value = "0" Then
            ButtonKoujigaisyaKensaku.Visible = False '�H����Ќ����{�^��
            CheckKoujigaisyaSeikyuu.Visible = True '�H����А���
            EnableTextBox(TextKoujigaisyaCd, False) '�H����ЃR�[�h
            EnableDropDownList(SelectSyouhinCd, False) '���i�R�[�h
            EnableTextBox(TextUriageZeinukiKingaku, False) '����Ŕ����z
            EnableTextBox(TextUriageSyouhizeiGaku, False) '�������Ŋz
            EnableTextBox(TextSiireZeinukiKingaku, False) '�d���Ŕ����z
            EnableTextBox(TextSiireSyouhizeiGaku, False) '�d������Ŋz
            EnableTextBox(TextSeikyuusyoHakkoubi, False) '���������s��
            EnableTextBox(TextUriageNengappi, False) '����N����
            EnableTextBox(TextDenpyouUriageNengappi, False) '�`�[����N����
            EnableTextBox(TextDenpyouSiireNengappi, False) '�`�[�d���N����
            EnableTextBox(TextHattyuusyoKakuninbi, False) '�������m�F��
            EnableDropDownList(SelectSeikyuuUmu, False) '�����L��
            EnableDropDownList(SelectUriageSyori, False) '���㏈��
            EnableCheckBox(CheckKoujigaisyaSeikyuu, False) '�H����А���

        End If

        ' �o�������y�є������Ǘ������������ꍇ�A�񊈐���
        If HiddenHattyuusyoKanriKengen.Value = "0" And _
           HiddenKeiriGyoumuKengen.Value = "0" Then
            EnableTextBox(TextHattyuusyoKingaku, False) '���������z
            EnableDropDownList(SelectHattyuusyoKakutei, False) '�������m��
        End If

    End Sub

#Region "��ʃR���g���[���̒l���������A�����񉻂���"

    ''' <summary>
    ''' ��ʃR���g���[���̒l���������A�����񉻂���
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringUriage() As String

        Dim sb As New StringBuilder

        sb.Append(SelectSyouhinCd.SelectedValue & EarthConst.SEP_STRING)    '���i�R�[�h
        sb.Append(TextUriageZeinukiKingaku.Text & EarthConst.SEP_STRING)    '������z(���ǔ�����z_�Ŕ����z)
        sb.Append(TextUriageSyouhizeiGaku.Text & EarthConst.SEP_STRING)     '����Ŋz
        sb.Append(HiddenZeiKbn.Value & EarthConst.SEP_STRING)               '�ŋ敪(��\������)
        sb.Append(TextDenpyouUriageNengappi.Text & EarthConst.SEP_STRING)   '�`�[����N����
        sb.Append(TextUriageNengappi.Text & EarthConst.SEP_STRING)          '����N����
        sb.Append(SelectUriageSyori.SelectedValue & EarthConst.SEP_STRING)  '����v��FLG
        sb.Append(TextSeikyuusyoHakkoubi.Text & EarthConst.SEP_STRING)      '���������s��

        sb.Append(SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value & EarthConst.SEP_STRING)          '������R�[�h
        sb.Append(SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc.Value & EarthConst.SEP_STRING)         '������}��
        sb.Append(SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn.Value & EarthConst.SEP_STRING)         '������敪

        Return (sb.ToString)

    End Function

    ''' <summary>
    ''' ��ʃR���g���[���̒l���������A�����񉻂���
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringSiire() As String

        Dim sb As New StringBuilder

        sb.Append(SelectSyouhinCd.SelectedValue & EarthConst.SEP_STRING)    '���i�R�[�h
        sb.Append(TextSiireZeinukiKingaku.Text & EarthConst.SEP_STRING)     '�d�����z(���ǎd�����z_�Ŕ����z)
        sb.Append(TextSiireSyouhizeiGaku.Text & EarthConst.SEP_STRING)      '�d������Ŋz
        sb.Append(TextDenpyouSiireNengappi.Text & EarthConst.SEP_STRING)    '�`�[�d���N����
        sb.Append(HiddenZeiKbn.Value & EarthConst.SEP_STRING)               '�ŋ敪(��\������)
        sb.Append(SelectUriageSyori.SelectedValue & EarthConst.SEP_STRING)  '����v��FLG

        sb.Append(SeikyuuSiireLinkCtrl.AccTysKaisyaCd.Value & EarthConst.SEP_STRING)            '������ЃR�[�h
        sb.Append(SeikyuuSiireLinkCtrl.AccTysKaisyaJigyousyoCd.Value & EarthConst.SEP_STRING)   '������Ў��Ə��R�[�h

        Return (sb.ToString)

    End Function

#Region "��ʃR���g���[���̕ύX�ӏ��Ή�"

    ''' <summary>
    ''' ��ʃR���g���[���̒l���������A�����񉻂���(�S����)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringAll() As String

        Dim sb As New StringBuilder

        '����\�����ڂP
        sb.Append(Me.HiddenKubun.Value & EarthConst.SEP_STRING)   '�敪
        sb.Append(Me.HiddenBangou.Value & EarthConst.SEP_STRING)   '�ۏ؏�NO
        sb.Append(Me.HiddenBunruiCd.Value & EarthConst.SEP_STRING)   '���ރR�[�h

        '���\������
        sb.Append(Me.TextKoujigaisyaCd.Text & EarthConst.SEP_STRING) '�H�����
        sb.Append(CStr(Me.CheckKoujigaisyaSeikyuu.Checked) & EarthConst.SEP_STRING) '�H����А����L��
        sb.Append(Me.SelectSyouhinCd.SelectedValue & EarthConst.SEP_STRING)   '���i�R�[�h

        sb.Append(Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value & EarthConst.SEP_STRING)   '������R�[�h
        sb.Append(Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc.Value & EarthConst.SEP_STRING)   '������}��
        sb.Append(Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn.Value & EarthConst.SEP_STRING)   '������敪
        sb.Append(Me.SeikyuuSiireLinkCtrl.AccTysKaisyaCd.Value & EarthConst.SEP_STRING)   '������ЃR�[�h
        sb.Append(Me.SeikyuuSiireLinkCtrl.AccTysKaisyaJigyousyoCd.Value & EarthConst.SEP_STRING)   '������Ў��Ə��R�[�h

        sb.Append(Me.TextSiireZeinukiKingaku.Text & EarthConst.SEP_STRING)   '���������z(�d��)
        sb.Append(Me.TextSiireSyouhizeiGaku.Text & EarthConst.SEP_STRING)   '�d������Ŋz
        sb.Append(Me.TextDenpyouSiireNengappiDisplay.Text & EarthConst.SEP_STRING)   '�`�[�d���N����
        sb.Append(Me.TextDenpyouSiireNengappi.Text & EarthConst.SEP_STRING)   '�`�[�d���N�����C��

        sb.Append(Me.TextUriageZeinukiKingaku.Text & EarthConst.SEP_STRING)   '�������Ŕ����z
        sb.Append(Me.TextUriageSyouhizeiGaku.Text & EarthConst.SEP_STRING)   '����Ŋz(����)
        sb.Append(Me.TextUriageZeikomiKingaku.Text & EarthConst.SEP_STRING)   '�ō����z(����)
        sb.Append(Me.TextDenpyouUriageNengappiDisplay.Text & EarthConst.SEP_STRING)   '�`�[����N����
        sb.Append(Me.TextDenpyouUriageNengappi.Text & EarthConst.SEP_STRING)   '�`�[����N�����C��
        sb.Append(Me.TextUriageNengappi.Text & EarthConst.SEP_STRING)   '����N����
        sb.Append(Me.SelectUriageSyori.SelectedValue & EarthConst.SEP_STRING)   '���㏈��FLG(����v��FLG)
        sb.Append(Me.TextUriagebi.Text & EarthConst.SEP_STRING)   '���㏈����(����v���)

        sb.Append(Me.TextSeikyuusyoHakkoubi.Text & EarthConst.SEP_STRING)   '���������s��
        sb.Append(Me.SelectSeikyuuUmu.SelectedValue & EarthConst.SEP_STRING)   '�����L��

        sb.Append(Me.TextHattyuusyoKingaku.Text & EarthConst.SEP_STRING)   '���������z
        sb.Append(Me.SelectHattyuusyoKakutei.SelectedValue & EarthConst.SEP_STRING)   '�������m��FLG
        sb.Append(Me.TextHattyuusyoKakuninbi.Text & EarthConst.SEP_STRING)   '�������m�F��

        '����\�����ڂQ
        sb.Append(Me.HiddenZeiKbn.Value & EarthConst.SEP_STRING)   '�ŋ敪(�ŗ�)

        'KEY���̎擾
        Me.getCtrlValuesStringAllKey()

        Return (sb.ToString)
    End Function

    ''' <summary>
    ''' �@�ʐ����e�[�u���̑S���ڏ����������A�����񉻂���
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getCtrlValuesStringAllKey()
        Dim dic As New Dictionary(Of String, String)
        Dim strRecString As String = String.Empty
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic

        '��ʕ\������DB�l�̘A���l���擾
        If Me.HiddenKeyValue.Value = String.Empty Then

            With dic
                .Add("0", "�敪")
                .Add("1", "�ۏ؏�NO")
                .Add("2", "���ރR�[�h")
                .Add("3", "�H����ЃR�[�h")
                .Add("4", "�H����А���")
                .Add("5", "���i�R�[�h")
                .Add("6", "������R�[�h")
                .Add("7", "������}��")
                .Add("8", "������敪")
                .Add("9", "������ЃR�[�h")
                .Add("10", "������Ў��Ə��R�[�h")
                .Add("11", "���������z(�d��)")
                .Add("12", "�d������Ŋz")
                .Add("13", "�`�[�d���N����")
                .Add("14", "�`�[�d���N�����C��")
                .Add("15", "�������Ŕ����z")
                .Add("16", "����Ŋz(����)")
                .Add("17", "�ō����z(����)")
                .Add("18", "�`�[����N����")
                .Add("19", "�`�[����N�����C��")
                .Add("20", "����N����")
                .Add("21", "���㏈��FLG(����v��FLG)")
                .Add("22", "���㏈����(����v���)")
                .Add("23", "���������s��")
                .Add("24", "�����L��")
                .Add("25", "���������z")
                .Add("26", "�������m��FLG")
                .Add("27", "�������m�F��")
                .Add("28", "�ŋ敪(�ŗ�)")
            End With

            strRecString = iLogic.getJoinString(dic.Values.GetEnumerator)
            Me.HiddenKeyValue.Value = strRecString
        End If

    End Sub

    ''' <summary>
    ''' �@�ʐ����e�[�u���̑S���ڏ����Ǘ����A�Ώۂ̍��ڂ����݂����ꍇ�A�w�i�F��ԐF�ɕύX����
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getCtrlValuesStringAllKeyCtrlId(ByVal strKey As String)
        Dim objRet As New Object
        Dim dic As New Dictionary(Of String, Object)
        Dim strRecString As String = String.Empty
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic

        'Key���ɃI�u�W�F�N�g���Z�b�g
        With dic
            .Add("0", Me.HiddenKubun)
            .Add("1", Me.HiddenBangou)
            .Add("2", Me.HiddenBunruiCd)
            .Add("3", Me.TextKoujigaisyaCd)
            .Add("4", Me.CheckKoujigaisyaSeikyuu)
            .Add("5", Me.SelectSyouhinCd)
            .Add("6", Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd)
            .Add("7", Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc)
            .Add("8", Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn)
            .Add("9", Me.SeikyuuSiireLinkCtrl.AccTysKaisyaCd)
            .Add("10", Me.SeikyuuSiireLinkCtrl.AccTysKaisyaJigyousyoCd)
            .Add("11", Me.TextSiireZeinukiKingaku)
            .Add("12", Me.TextSiireSyouhizeiGaku)
            .Add("13", Me.TextDenpyouSiireNengappiDisplay)
            .Add("14", Me.TextDenpyouSiireNengappi)
            .Add("15", Me.TextUriageZeinukiKingaku)
            .Add("16", Me.TextUriageSyouhizeiGaku)
            .Add("17", Me.TextUriageZeikomiKingaku)
            .Add("18", Me.TextDenpyouUriageNengappiDisplay)
            .Add("19", Me.TextDenpyouUriageNengappi)
            .Add("20", Me.TextUriageNengappi)
            .Add("21", Me.SelectUriageSyori)
            .Add("22", Me.TextUriagebi)
            .Add("23", Me.TextSeikyuusyoHakkoubi)
            .Add("24", Me.SelectSeikyuuUmu)
            .Add("25", Me.TextHattyuusyoKingaku)
            .Add("26", Me.SelectHattyuusyoKakutei)
            .Add("27", Me.TextHattyuusyoKakuninbi)
            .Add("28", Me.HiddenZeiKbn)
        End With

        '�w�i�F�ύX����
        Call cLogic.ChgHenkouCtrlBgColor(dic, strKey)

    End Sub

    ''' <summary>
    ''' �@�ʐ����e�[�u���̑S���ڏ����������A�����񉻂���B
    ''' �ύX�ӏ��̔w�i�F��ύX����
    ''' </summary>
    ''' <param name="strKey">KEY�l</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringAllKeyName(ByVal strKey As String, ByVal strCtrlNameKey As String) As String
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic
        Dim MyLogic As New TeibetuSyuuseiLogic

        Dim strKeyValues() As String
        Dim strHiddenKeyValues() As String
        Dim strRet As String = String.Empty

        Dim intCnt As Integer = 0
        Dim strTmp1 As String = String.Empty
        Dim strTmp2 As String = String.Empty
        Dim dicItem1 As Dictionary(Of String, String)
        Dim strColorId As String = String.Empty

        If strKey = String.Empty Then
            Return strRet
            Exit Function
        End If

        'DB�l
        strKeyValues = iLogic.getArrayFromDollarSep(strKey)

        '���ږ����擾
        strHiddenKeyValues = iLogic.getArrayFromDollarSep(strCtrlNameKey)
        dicItem1 = MyLogic.getDicItem(strHiddenKeyValues)

        For intCnt = 0 To strHiddenKeyValues.Length - 1

            If strKeyValues.Length <= intCnt Then Exit For

            strTmp1 = strKeyValues(intCnt)
            If strTmp1 <> String.Empty Then
                If dicItem1.ContainsKey(strTmp1) Then
                    If intCnt <> 0 Then '�ŏ��̍��ڂ�","�͕t���Ȃ�
                        strRet &= ","
                    End If
                    '�ύX�ӏ��̍��ږ��̂��擾
                    strRet &= dicItem1(strTmp1)
                    '�w�i�F�̕ύX
                    Me.getCtrlValuesStringAllKeyCtrlId(strTmp1)
                End If
            End If
        Next

        Return strRet
    End Function

    ''' <summary>
    ''' �ύX�̂������R���g���[�����̂𕶎��񌋍����A�ԋp����
    ''' </summary>
    ''' <param name="strDbVal">DB�l</param>
    ''' <param name="strChgVal">�ύX�l</param>
    ''' <param name="strCtrlNm">�R���g���[������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkChgCtrlName(ByVal strDbVal As String, ByVal strChgVal As String, ByVal strCtrlNm As String) As String
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic
        Dim strDbValues() As String
        Dim strChgValues() As String
        Dim strRet As String = String.Empty
        Dim strKey As String = String.Empty

        Dim intCnt As Integer = 0
        Dim strTmp1 As String = String.Empty
        Dim strTmp2 As String = String.Empty

        'DB�l���邢�͕ύX�l�������͂̏ꍇ
        If strDbVal = String.Empty OrElse strChgVal = String.Empty Then
            Return strRet
            Exit Function
        End If

        'DB�l
        strDbValues = iLogic.getArrayFromDollarSep(strDbVal)
        '��ʂ̒l
        strChgValues = iLogic.getArrayFromDollarSep(strChgVal)

        '���ڐ��������ꍇ
        If strDbValues.Length = strChgValues.Length Then
            For intCnt = 0 To strDbValues.Length - 1
                strTmp1 = strDbValues(intCnt)
                strTmp2 = strChgValues(intCnt)
                '�ύX�ӏ��������index��ޔ�
                If strTmp1 <> strTmp2 Then
                    strKey &= CStr(intCnt) & EarthConst.SEP_STRING
                End If
            Next
        End If

        'index�����ɁA�ύX�ӏ��̖��̂Ɣw�i�F�ύX���s�Ȃ�
        strRet = Me.getCtrlValuesStringAllKeyName(strKey, strCtrlNm)

        Return strRet
    End Function

#End Region

#End Region

#Region "���������z�֘A�̏���"
    ''' <summary>
    ''' �������z�ύX���̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub HattyuuAfterUpdate(ByVal sender As Object)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".HattyuuAfterUpdate", _
                                                    sender)

        Dim hattyuuKingaku As Integer = Integer.MinValue

        CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, hattyuuKingaku)

        TextHattyuusyoKakuninbi.Text = PfunZ010SetHatyuuYMD(hattyuuKingaku)

        '�������m��`�F�b�N
        Select Case FunCheckHKakutei(2, sender)
            Case 1
                '
            Case 2
                SelectHattyuusyoKakutei.SelectedValue = "1"

                ' Old�l�Ɍ��݂̒l���Z�b�g
                HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue

                '���������z��ҏW�s�ɐݒ�
                EnableTextBox(TextHattyuusyoKingaku, False)
        End Select

        '�X�V�㔭�������z��old�ɐݒ�
        HiddenHattyuusyoKingakuOld.Value = TextHattyuusyoKingaku.Text
        TextHattyuusyoKingaku.Text = IIf(hattyuuKingaku = Integer.MinValue, "", hattyuuKingaku.ToString(EarthConst.FORMAT_KINGAKU_1))


    End Sub

    ''' <summary>
    ''' �������m�F���ݒ菈��
    ''' </summary>
    ''' <param name="rvntKingaku">���������z</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PfunZ010SetHatyuuYMD(ByVal rvntKingaku As Integer) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".PfunZ010SetHatyuuYMD", _
                                                    rvntKingaku)

        If rvntKingaku = 0 Or rvntKingaku = Integer.MinValue Then
            Return ""
        Else
            Return Date.Now.ToString("yyyy/MM/dd")
        End If
    End Function

    ''' <summary>
    ''' �������m��`�F�b�N
    ''' </summary>
    ''' <param name="rlngMode">1,�������m��ύX��.2,���������z�ύX��</param>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <returns>0:�����Ȃ��A1:�������m��փt�H�[�J�X�ڍs�A2:�����m��</returns>
    ''' <remarks></remarks>
    Public Function FunCheckHKakutei(ByVal rlngMode As Long, _
                                     ByVal sender As Object) As Long

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".FunCheckHKakutei", _
                                                    rlngMode, sender)

        FunCheckHKakutei = 0

        If SelectHattyuusyoKakutei.SelectedValue = "1" And _
           TextHattyuusyoKingaku.Text IsNot TextUriageZeinukiKingaku.Text Then

            Dim hattyuuKingaku As Integer = Integer.MinValue
            CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, hattyuuKingaku)
            ' �������m�F����ݒ�
            TextHattyuusyoKakuninbi.Text = PfunZ010SetHatyuuYMD(hattyuuKingaku)

        End If

        ' ��r������z�𐔒l�ϊ�����
        Dim chkVal1 As Integer = 0
        Dim chkVal2 As Integer = 0
        CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, chkVal1)
        CommonLogic.Instance.SetDisplayString(TextUriageZeinukiKingaku.Text, chkVal2)

        ' �����
        If rlngMode = 2 Then
            ' ���������z�ύX���̓e�L�X�g�ĕύX�チ�b�Z�[�W�\��
            If SelectUriageSyori.SelectedValue = EarthConst.URIAGE_ZUMI_CODE Then

                ' ��r���ċ��z�̔�r�ɂ�胁�b�Z�[�W�𕪂���
                If chkVal1 = chkVal2 Then
                    FunCheckHKakutei = 2
                    If HiddenHattyuusyoKingakuOld.Value <> "" And _
                       HiddenHattyuusyoFlgOld.Value <> "1" Then
                        ' �����������m��
                        ScriptManager.RegisterClientScriptBlock(sender, _
                                                                sender.GetType(), _
                                                                "alert", _
                                                                "alert('" & _
                                                                Messages.MSG045C & _
                                                                "')", True)
                    End If
                End If
            End If
        Else
            If SelectHattyuusyoKakutei.SelectedValue = "1" Then
                ' �������m��ύX���͋��z����Ń��b�Z�[�W�\��
                FunCheckHKakutei = 2
                If chkVal1 <> chkVal2 Then
                    ' �����������m��
                    ScriptManager.RegisterClientScriptBlock(sender, _
                                                            sender.GetType(), _
                                                            "alert", _
                                                            "alert('" & _
                                                            Messages.MSG046C & _
                                                            "')", True)
                End If
            End If
        End If

        '�������m��ς݂̏ꍇ�A���������z��ҏW�s�ɐݒ�
        If SelectHattyuusyoKakutei.SelectedValue = "1" Then
            EnableTextBox(TextHattyuusyoKingaku, False)
        Else
            If HiddenHattyuusyoKanriKengen.Value = "0" And _
               HiddenKeiriGyoumuKengen.Value = "0" Then
            Else
                ' �o���������A�������Ǘ������L��ꍇ�A������
                EnableTextBox(TextHattyuusyoKingaku, True)
            End If
        End If

    End Function
#End Region
#End Region

#Region "�p�u���b�N���\�b�h"
    ''' <summary>
    ''' �G���[�`�F�b�N
    ''' </summary>
    ''' <param name="errMess"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <param name="typeWord"></param>
    ''' <param name="denpyouNgList"></param>
    ''' <param name="denpyouErrMess"></param>
    ''' <param name="seikyuuUmuErrMess"></param>
    ''' <param name="strChgPartMess"></param>
    ''' <remarks></remarks>
    Public Sub CheckInput(ByRef errMess As String, _
                          ByRef arrFocusTargetCtrl As List(Of Control), _
                          ByVal typeWord As String, _
                          ByVal denpyouNgList As String, _
                          ByRef denpyouErrMess As String, _
                          ByRef seikyuuUmuErrMess As String, _
                          ByRef strChgPartMess As String _
                          )

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckKinsoku", _
                                                    errMess, _
                                                    arrFocusTargetCtrl, _
                                                    typeWord, _
                                                    denpyouNgList, _
                                                    denpyouErrMess, _
                                                    seikyuuUmuErrMess, _
                                                    strChgPartMess _
                                                    )

        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban
        '�����\��m�茎
        Dim dtGetujiKakuteiLastSyoriDate As DateTime = DateTime.MinValue

        Dim setuzoku As String = typeWord
        If typeWord <> "" Then
            setuzoku = typeWord & "�F"
        End If

        'DB�ǂݍ��ݎ��_�̒l���A���݉�ʂ̒l�Ɣ�r(�ύX�L���`�F�b�N)
        If HiddenKubun.Value <> String.Empty AndAlso (HiddenOpenValuesUriage.Value <> String.Empty Or HiddenOpenValuesSiire.Value <> String.Empty) Then
            'DB�ǂݍ��ݎ��_�̒l����̏ꍇ�͔�r�ΏۊO
            '��r���{(����)
            If HiddenOpenValuesUriage.Value <> getCtrlValuesStringUriage() Then
                '�ύX�L��̏ꍇ
                If denpyouNgList.IndexOf(HiddenKubun.Value & EarthConst.SEP_STRING & _
                                         HiddenBangou.Value & EarthConst.SEP_STRING & _
                                         HiddenBunruiCd.Value & EarthConst.SEP_STRING & _
                                         "1") >= 0 Then
                    denpyouErrMess += typeWord & ","
                End If

                '�����m��\��ς݂̏����N���u�ȑO�v�̓��t�œ`�[����N������ݒ肷��̂̓G���[
                If cLogic.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouUriageNengappi.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                    errMess += String.Format(Messages.MSG161E, dtGetujiKakuteiLastSyoriDate.Month, typeWord, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                    arrFocusTargetCtrl.Add(Me.TextDenpyouUriageNengappi)
                End If
            End If

            '��r���{(�d��)
            If HiddenOpenValuesSiire.Value <> getCtrlValuesStringSiire() Then
                '�����m��\��ς݂̏����N���u�ȑO�v�̓��t�œ`�[�d���N������ݒ肷��̂̓G���[
                If cLogic.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouSiireNengappi.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                    errMess += String.Format(Messages.MSG161E, dtGetujiKakuteiLastSyoriDate.Month, typeWord, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month).Replace("�`�[����", "�`�[�d��")
                    arrFocusTargetCtrl.Add(Me.TextDenpyouSiireNengappi)
                End If
            End If

        End If

        '��r���{(�ύX�`�F�b�N)
        Dim strChgVal As String = Me.getCtrlValuesStringAll()
        If Me.HiddenOpenValue.Value <> strChgVal Then
            Dim strCtrlNm As String = String.Empty
            strCtrlNm = Me.ChkChgCtrlName(Me.HiddenOpenValue.Value, strChgVal, Me.HiddenKeyValue.Value) '�ύX�ӏ����̎擾
            strChgPartMess += "[" & typeWord & "]\r\n" & strCtrlNm & "\r\n"
        End If

        '�R�[�h���͒l�ύX�`�F�b�N
        If TextKoujigaisyaCd.Text <> HiddenKoujigaisyaCdOld.Value Then
            Dim koujigaisya As String = "�H����ЃR�[�h"
            If HiddenBunruiCd.Value = EarthConst.SOUKO_CD_TUIKA_KOUJI Then
                koujigaisya = "�ǉ��H����ЃR�[�h"
            End If
            errMess += Messages.MSG030E.Replace("@PARAM1", koujigaisya)
            arrFocusTargetCtrl.Add(ButtonKoujigaisyaKensaku)
        End If

        '�K�{�`�F�b�N
        '����N�����Ɠ`�[����N����
        If Me.TextUriageNengappi.Text = String.Empty And Me.TextDenpyouUriageNengappi.Text <> String.Empty Then
            errMess += Messages.MSG153E.Replace("@PARAM1", setuzoku & "�`�[����N����").Replace("@PARAM2", setuzoku & "����N����")
            arrFocusTargetCtrl.Add(TextUriageNengappi)
        End If
        '������̏ꍇ�ł��A����N�����Ɠ`�[����N�����A�`�[�d���N�������قȂ�ꍇ
        If Me.SelectUriageSyori.SelectedValue = "0" Then
            '�`�[����N�����Ɣ�r
            '�`�[�d���N�����Ɣ�r
            If Me.TextUriageNengappi.Text <> Me.TextDenpyouUriageNengappi.Text _
                Or Me.TextUriageNengappi.Text <> Me.TextDenpyouSiireNengappi.Text Then
                errMess += Messages.MSG144E.Replace("@PARAM1", setuzoku & "�`�[����N�������邢�͓`�[�d���N����").Replace("@PARAM2", setuzoku & "����N����").Replace("@PARAM3", "�X�V")
                arrFocusTargetCtrl.Add(Me.TextUriageNengappi)
            End If
        End If

        '���͒l�`�F�b�N
        If TextSeikyuusyoHakkoubi.Text <> "" Then
            If DateTime.Parse(TextSeikyuusyoHakkoubi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextSeikyuusyoHakkoubi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "���������s��")
                arrFocusTargetCtrl.Add(TextSeikyuusyoHakkoubi)
            End If
        End If

        If TextUriageNengappi.Text <> "" Then
            If DateTime.Parse(TextUriageNengappi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextUriageNengappi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "����N����")
                arrFocusTargetCtrl.Add(TextUriageNengappi)
            End If
        End If

        If TextDenpyouUriageNengappi.Text <> "" Then
            If DateTime.Parse(TextDenpyouUriageNengappi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextDenpyouUriageNengappi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "�`�[����N����")
                arrFocusTargetCtrl.Add(TextDenpyouUriageNengappi)
            End If
        End If

        If TextDenpyouSiireNengappi.Text <> "" Then
            If DateTime.Parse(TextDenpyouSiireNengappi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextDenpyouSiireNengappi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "�`�[�d���N����")
                arrFocusTargetCtrl.Add(TextDenpyouSiireNengappi)
            End If
        End If

        If TextHattyuusyoKakuninbi.Text <> "" Then
            If DateTime.Parse(TextHattyuusyoKakuninbi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextHattyuusyoKakuninbi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "�������m�F��")
                arrFocusTargetCtrl.Add(TextHattyuusyoKakuninbi)
            End If
        End If

        Dim dtLogic As New DataLogic
        Dim intJituGaku As Integer = dtLogic.str2Int(TextUriageZeinukiKingaku.Text.Replace(",", ""))

        '���i�̐����L���Ɣ�����z�Ƃ̊֘A�`�F�b�N(���������E0�~�ȊO�FNG / ��������E0�~: �x��)
        If HiddenOpenValuesUriage.Value <> String.Empty Then
            If SelectSyouhinCd.SelectedValue <> String.Empty Then
                If SelectSeikyuuUmu.SelectedValue = "0" And intJituGaku <> 0 Then
                    '���������E0�~�ȊO�FNG
                    errMess += String.Format(Messages.MSG157E, typeWord)
                    arrFocusTargetCtrl.Add(SelectSeikyuuUmu)
                ElseIf SelectSeikyuuUmu.SelectedValue = "1" And intJituGaku = 0 Then
                    '��������E0�~: �x��
                    seikyuuUmuErrMess += typeWord & "�A"
                    arrFocusTargetCtrl.Add(TextUriageZeinukiKingaku)
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' ��{������E�d������̐ݒ�
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strTysKaisyaCd">������ЃR�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetDefaultSeikyuuSiireSakiInfo(ByVal strKameitenCd As String, ByVal strTysKaisyaCd As String)
        Dim strUriageZumi As String = String.Empty  '���㏈���ςݔ��f�t���O�p

        '���㏈���σ`�F�b�N
        If Me.SelectUriageSyori.SelectedValue = "1" Then
            strUriageZumi = Me.SelectUriageSyori.SelectedValue
        End If

        Me.SeikyuuSiireLinkCtrl.SetVariableValueCtrlFromParent(strKameitenCd _
                                                                , Me.SelectSyouhinCd.SelectedValue _
                                                                , Me.TextKoujigaisyaCd.Text _
                                                                , strUriageZumi _
                                                                , Me.CheckKoujigaisyaSeikyuu.Checked _
                                                                , Me.TextKoujigaisyaCd.Text _
                                                                , Me.TextDenpyouUriageNengappi.Text)
    End Sub

#End Region

#Region "���z�ݒ�"
    ''' <summary>
    ''' �H�����i�}�X�^��KEY�������R�[�h�N���X���ݒ�
    ''' </summary>
    ''' <remarks>
    ''' [�ݒ�Ώ�]
    ''' �˗����eCTRL.�����X�R�[�h
    ''' �˗����eCTRL.�n��R�[�h
    ''' �˗����eCTRL.�c�Ə��R�[�h
    ''' [���ݒ�Ώ�] ����ʏ�ɍ��ڂƂ��đ��݂��邽��
    ''' �H�����iCTRL.���i�R�[�h
    ''' �H�����iCTRL.�H����ЃR�[�h
    ''' </remarks>
    Public Sub SetKojKkkMstInfo(ByVal keyRec As KoujiKakakuKeyRecord)
        With keyRec
            '�����X�R�[�h
            Me.HiddenKameitenCd.Value = .KameitenCd
            '�n��R�[�h
            Me.HiddenKeiretuCd.Value = .KeiretuCd
            '�c�Ə��R�[�h
            Me.HiddenEigyousyoCd.Value = .EigyousyoCd
        End With
    End Sub

    ''' <summary>
    ''' ���i���(�H�����i�}�X�^������z�E�����L�����ݒ�)
    ''' </summary>
    '''<param name="emActionType">���s���A�N�V�����^�C�v</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSyouhinInfoFromKojM(ByVal emActionType As EarthEnum.emKojKkkActionType) As Boolean

        Dim keyRec As New KoujiKakakuKeyRecord          '�����L�[�p���R�[�h
        Dim resultRec As New KoujiKakakuRecord          '���ʎ擾�p���R�[�h
        Dim lgcKouji As New KairyouKoujiLogic           '���ǍH�����W�b�N
        Dim intResult As Integer = Integer.MinValue

        Dim strZeiKbn As String = String.Empty      '�ŋ敪
        Dim strZeiritu As String = String.Empty     '�ŗ�
        Dim strSyouhinCd As String = String.Empty   '���i�R�[�h

        '�擾�ɕK�v�ȉ�ʍ��ڂ̃Z�b�g
        keyRec = cbLogic.GetKojKkkMstKeyRec(Me.HiddenKameitenCd.Value _
                                            , Me.HiddenEigyousyoCd.Value _
                                            , Me.HiddenKeiretuCd.Value _
                                            , Me.SelectSyouhinCd.SelectedValue _
                                            , Me.TextKoujigaisyaCd.Text)

        '�H����Љ��i���W�b�N���擾
        intResult = lgcKouji.GetKoujiKakakuRecord(keyRec, resultRec)

        '������v�̎擾�͉�ʂɃZ�b�g
        If intResult < EarthEnum.emKoujiKakaku.Syouhin Then
            '�H����А����L��
            If resultRec.KojGaisyaSeikyuuUmu = 1 Then
                Me.CheckKoujigaisyaSeikyuu.Checked = True
            Else
                Me.CheckKoujigaisyaSeikyuu.Checked = False
            End If
            Me.UpdatePanelKoujigaisyaSeikyuu.Update()

            '�����L��
            If emActionType <> EarthEnum.emKojKkkActionType.SeikyuuUmu Then '�����L���ύX���͐ݒ肵�Ȃ�
                If resultRec.SeikyuuUmu = 0 OrElse resultRec.SeikyuuUmu = 1 Then
                    Me.SelectSeikyuuUmu.SelectedValue = resultRec.SeikyuuUmu
                End If
            End If

            '����N�����Ŕ��f���āA�������ŗ����擾����
            strSyouhinCd = Me.SelectSyouhinCd.SelectedValue
            If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                '�擾�����ŋ敪�E�ŗ����Z�b�g
                resultRec.Zeiritu = strZeiritu
                resultRec.ZeiKbn = strZeiKbn
            End If

            '�ŗ�
            Me.HiddenZeiritu.Value = IIf(resultRec.Zeiritu = Decimal.MinValue, 0, cLogic.GetDispStr(resultRec.Zeiritu))
            '�ŋ敪
            Me.HiddenZeiKbn.Value = IIf(resultRec.ZeiKbn = String.Empty, "", resultRec.ZeiKbn)

            '�����L��=�����̏ꍇ
            If Me.SelectSeikyuuUmu.SelectedValue = "0" Then
                '���������z
                Me.TextUriageZeinukiKingaku.Text = "0"
            Else
                '���������z
                Me.TextUriageZeinukiKingaku.Text = IIf(resultRec.UriGaku = Integer.MinValue, 0, Format(resultRec.UriGaku, EarthConst.FORMAT_KINGAKU_1))
            End If

            '���z�ݒ�
            SetKingaku()

            Me.UpdatePanelKoujiSyouhin.Update()
        Else
            Return False
            Exit Function
        End If

        Return True
    End Function

    ''' <summary>
    ''' ���z�ݒ�(������z)
    ''' </summary>
    ''' <param name="blnZeigaku"></param>
    ''' <remarks></remarks>
    Private Sub SetKingaku(Optional ByVal blnZeigaku As Boolean = False)
        ' �Ŕ����i�i���������z�j
        Dim zeinuki_ctrl As TextBox
        ' ����ŗ�
        Dim zeiritu_ctrl As HiddenField
        ' ����Ŋz
        Dim zeigaku_ctrl As TextBox
        ' �ō����z
        Dim zeikomi_gaku_ctrl As TextBox

        zeinuki_ctrl = TextUriageZeinukiKingaku
        zeiritu_ctrl = HiddenZeiritu
        zeigaku_ctrl = TextUriageSyouhizeiGaku
        zeikomi_gaku_ctrl = TextUriageZeikomiKingaku

        Dim zeinuki As Integer = 0
        Dim zeiritu As Decimal = 0
        Dim zeigaku As Integer = 0
        Dim zeikomi_gaku As Integer = 0

        If zeinuki_ctrl.Text = "" Then '������
            zeinuki_ctrl.Text = ""
            zeigaku_ctrl.Text = ""
            zeikomi_gaku_ctrl.Text = ""

        Else '���͂���
            cLogic.SetDisplayString(CInt(zeinuki_ctrl.Text), zeinuki)
            cLogic.SetDisplayString(zeiritu_ctrl.Value, zeiritu)

            zeinuki = IIf(zeinuki = Integer.MinValue, 0, zeinuki)
            zeiritu = IIf(zeiritu = Decimal.MinValue, 0, zeiritu)

            If blnZeigaku Then '����Ŋz�̒l�Ōv�Z
                cLogic.SetDisplayString(CInt(zeigaku_ctrl.Text), zeigaku)
            Else
                zeigaku = Fix(zeinuki * zeiritu)
            End If
            zeikomi_gaku = zeinuki + zeigaku

            zeigaku_ctrl.Text = Format(zeigaku, EarthConst.FORMAT_KINGAKU_1) '�����
            zeikomi_gaku_ctrl.Text = Format(zeikomi_gaku, EarthConst.FORMAT_KINGAKU_1) '�ō����z

        End If
    End Sub


    ''' <summary>
    ''' �d������Ŋz�ݒ�(���������z)
    ''' </summary>
    ''' <param name="blnZeigaku"></param>
    ''' <remarks></remarks>
    Private Sub SetSiireZeigaku(Optional ByVal blnZeigaku As Boolean = False)
        ' �Ŕ����i�i���������z�j
        Dim zeinuki_ctrl As TextBox
        ' ����ŗ�
        Dim zeiritu_ctrl As HiddenField
        ' ����Ŋz
        Dim zeigaku_ctrl As TextBox
        ' �ō����z
        Dim zeikomi_gaku_ctrl As TextBox

        zeinuki_ctrl = TextSiireZeinukiKingaku ' ���������z
        zeiritu_ctrl = HiddenZeiritu
        zeigaku_ctrl = TextSiireSyouhizeiGaku
        zeikomi_gaku_ctrl = TextSiireZeikomiKingaku

        Dim zeinuki As Integer = 0
        Dim zeiritu As Decimal = 0
        Dim zeigaku As Integer = 0
        Dim zeikomi_gaku As Integer = 0

        If zeinuki_ctrl.Text = "" Then '������
            zeinuki_ctrl.Text = ""
            zeigaku_ctrl.Text = ""
            zeikomi_gaku_ctrl.Text = ""

        Else '���͂���
            cLogic.SetDisplayString(CInt(zeinuki_ctrl.Text), zeinuki)
            cLogic.SetDisplayString(zeiritu_ctrl.Value, zeiritu)

            zeinuki = IIf(zeinuki = Integer.MinValue, 0, zeinuki)
            zeiritu = IIf(zeiritu = Decimal.MinValue, 0, zeiritu)

            If blnZeigaku Then '����Ŋz�̒l�Ōv�Z
                cLogic.SetDisplayString(CInt(zeigaku_ctrl.Text), zeigaku)
            Else
                zeigaku = Fix(zeinuki * zeiritu)
            End If
            zeikomi_gaku = zeinuki + zeigaku

            zeigaku_ctrl.Text = Format(zeigaku, EarthConst.FORMAT_KINGAKU_1) '�����
            zeikomi_gaku_ctrl.Text = Format(zeikomi_gaku, EarthConst.FORMAT_KINGAKU_1) '�ō����z

        End If
    End Sub
#End Region

End Class