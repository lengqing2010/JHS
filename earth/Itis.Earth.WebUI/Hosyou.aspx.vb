Partial Public Class Hosyou
    Inherits System.Web.UI.Page

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim iraiSession As New IraiSession
    Dim userInfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager

    Dim cl As New CommonLogic
    Dim MLogic As New MessageLogic
    Dim JibanLogic As New JibanLogic
    Dim kameitenlogic As New KameitenSearchLogic
    Dim MyLogic As New HosyouLogic
    Dim cbLogic As New CommonBizLogic
    Dim BJykyLogic As New BukkenSintyokuJykyLogic

#Region "�v���p�e�B"
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _kbn As String
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrKbn() As String
        Get
            Return _kbn
        End Get
        Set(ByVal value As String)
            _kbn = value
        End Set
    End Property

    ''' <summary>
    ''' �ԍ�(�ۏ؏�No)
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _no As String
    ''' <summary>
    ''' �ԍ�(�ۏ؏�No)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrBangou() As String
        Get
            Return _no
        End Get
        Set(ByVal value As String)
            _no = value
        End Set
    End Property
#End Region

#Region "�ۏ؉�ʗp/�R���g���[���ړ����^�C�v"

    ''' <summary>
    ''' ���z�^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Enum EnumKingakuType
        ''' <summary>
        ''' �Ĕ��s
        ''' </summary>
        ''' <remarks></remarks>
        Saihakkou = 0
        ''' <summary>
        ''' ��񕥖�
        ''' </summary>
        ''' <remarks></remarks>
        KaiyakuHaraimodosi = 1
        ''' <summary>
        ''' �w��Ȃ�
        ''' </summary>
        ''' <remarks></remarks>
        None = 2
    End Enum

#End Region

#Region "CSS�N���X��"
    Private Const CSS_TEXT_CENTER = "textCenter"
    Private Const CSS_KINGAKU = "kingaku"
    Private Const CSS_NUMBER = "number"
    Private Const CSS_DATE = "date"

    Private Const CSS_COLOR_RED = "red"
    Private Const CSS_COLOR_BLUE = "blue"
    Public Const CSS_COLOR_GRAY = "#dadada"
#End Region

#Region "�y�[�W���[�h�C�x���g"

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X
        Dim jSM As New JibanSessionManager

        '�}�X�^�[�y�[�W�����擾�iScriptManager�p�j
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ���[�U�[��{�F��
        jBn.UserAuth(userInfo)

        '�F�،��ʂɂ���ĉ�ʕ\����ؑւ���
        If userInfo Is Nothing Then
            '���O�C����񂪖����ꍇ�A���C����ʂɔ�΂�
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If Context.Items("irai") IsNot Nothing Then
            iraiSession = Context.Items("irai")
        End If

        '�e�e�[�u���̕\����Ԃ�؂�ւ���
        ucGyoumuKyoutuu.AccKyoutuuInfo.Style("display") = ucGyoumuKyoutuu.AccHdnKyoutuuInfoStyle.Value
        TbodyHakkouIraiInfo.Style("display") = HiddenHakkouIraiInfoStyle.Value
        TbodyHoshoInfo.Style("display") = HiddenHosyouInfoStyle.Value
        TbodyKairyoKouji.Style("display") = HiddenKairyouKoujiStyle.Value
        TbodySaiHakkou.Style("display") = HiddenSaiHakkouStyle.Value

        If IsPostBack = False Then

            ' Key����ێ�
            _kbn = Request("sendPage_kubun")
            _no = Request("sendPage_hosyoushoNo")

            ' �p�����[�^�s�����͉�ʂ�\�����Ȃ�
            If _kbn Is Nothing Or _
               _no Is Nothing Then
                Response.Redirect(UrlConst.MAIN)
            End If

            '****************************************************************************
            ' �h���b�v�_�E�����X�g�ݒ�
            '****************************************************************************
            Dim helper As New DropDownHelper
            ' �ۏ؏���=================================
            ' �ۏ؏����s�󋵃R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(Me.SelectHosyousyoHakkouJyoukyou, DropDownHelper.DropDownType.HosyousyoHakJyky)
            ' �ۏ؂Ȃ����R�R���{�Ƀf�[�^���o�C���h����
            helper.SetKtMeisyouDropDownList(Me.SelectHosyouNasiRiyuu, EarthConst.emKtMeisyouType.HOSYOU_NASI_RIYUU, True, True)
            ' �ėpNO�R���{�Ƀf�[�^���o�C���h����(�ۏ؂Ȃ����R�e�L�X�g�{�b�N�X����������p)
            helper.SetKtMeisyouHannyouDropDownList(Me.SelectHannyouNo, EarthConst.emKtMeisyouType.HOSYOU_NASI_RIYUU, EarthEnum.emKtMeisyouType.HannyouNo, True, False)

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            SetDispAction()

            '�{�^�������C�x���g�̐ݒ�
            setBtnEvent()

            '****************************************************************************
            ' �n�Ճf�[�^�擾
            '****************************************************************************
            Dim logic As New JibanLogic
            Dim jibanRec As New JibanRecordBase
            jibanRec = logic.GetJibanData(pStrKbn, pStrBangou)

            If Not jibanRec Is Nothing Then
                '�n�Ճf�[�^�̓ǂݍ���
                iraiSession.JibanData = jibanRec
                '�n�Ճf�[�^���R���g���[���ɃZ�b�g
                SetCtrlFromJibanRec(jibanRec)

                '****************************************************************************
                ' �ۏ؏��Ǘ��f�[�^�擾
                '****************************************************************************
                Dim HosyouRec As New HosyousyoKanriRecord
                HosyouRec = BJykyLogic.getSearchKeyDataRec(sender, jibanRec.Kbn, jibanRec.HosyousyoNo)

                If Not HosyouRec Is Nothing Then
                    '�ۏ؏��Ǘ��f�[�^���R���g���[���ɃZ�b�g
                    Me.SetCtrlFromHosyouRec(jibanRec, HosyouRec)
                End If

                '****************************************************************************
                ' �i���f�[�^�擾
                '****************************************************************************
                Dim reportRec As New ReportIfGetRecord
                JibanLogic.GetReportIfData(jibanRec, reportRec)
                Me.SetCtrlFromReportRec(jibanRec, reportRec)
            End If

        Else
            '���i���̐����E�d���悪�ύX����Ă��Ȃ������`�F�b�N���A
            '�ύX����Ă���ꍇ���̍Ď擾
            setSeikyuuSiireHenkou(sender, e)

        End If

        '�R���e�L�X�g�ɒl���i�[
        Context.Items("irai") = iraiSession

    End Sub

    ''' <summary>
    ''' �y�[�W���[�h�R���v���[�g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �C�x���g�n���h���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�����i���󋵃{�^��
        cl.getBukkenJykyMasterPath(ButtonBukkenJyokyou, _
                                 userInfo, _
                                 Me.ucGyoumuKyoutuu.AccHiddenKubun.ClientID, _
                                 Me.ucGyoumuKyoutuu.AccBangou.ClientID)

        '****************************
        '* �ۏ؂Ȃ����R�e�L�X�g�{�b�N�X�̊���������i�ۏ؏��j
        '****************************
        If Me.SelectHosyouNasiRiyuu.Style("display") <> "none" Then
            If Me.SelectHosyouNasiRiyuu.SelectedIndex > -1 Then
                If Me.SelectHannyouNo.SelectedItem.Text = EarthConst.ARI_VAL Then
                    Me.TextHosyouNasiRiyuu.Enabled = True
                    Me.TextHosyouNasiRiyuu.Style(EarthConst.STYLE_BACK_GROUND_COLOR) = EarthConst.STYLE_COLOR_WHITE
                Else
                    Me.TextHosyouNasiRiyuu.Enabled = False
                    Me.TextHosyouNasiRiyuu.Style(EarthConst.STYLE_BACK_GROUND_COLOR) = CSS_COLOR_GRAY
                End If
            End If
        End If

        '****************************
        '* ������/�d����������[�U�R���g���[���ɃZ�b�g�i�񍐏��Ĕ��s�j
        '****************************
        Dim strUriageZumi As String = String.Empty    '���㏈���ςݔ��f�t���O�p
        Dim strViewMode As String = String.Empty

        '���㏈���ϔ��f�t���O�̎擾
        strUriageZumi = cl.GetUriageSyoriZumiFlg(Me.SpanShUriageSyorizumi.InnerHtml)
        strViewMode = cl.GetViewMode(strUriageZumi, userInfo.KeiriGyoumuKengen)

        '�Q�ƃ��[�h�̐ݒ�
        If Me.SelectShSeikyuuUmu.Style("display") = "none" Then
            strViewMode = EarthConst.MODE_VIEW
        End If

        Me.ucSeikyuuSiireLinkSai.SetVariableValueCtrlFromParent(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                                    , Me.TextShSyouhinCd.Text _
                                                                    , Me.HiddenDefaultSiireSakiCdForLink.Value _
                                                                    , strUriageZumi _
                                                                    , _
                                                                    , _
                                                                    , _
                                                                    , strViewMode)

        '****************************
        '* ������/�d����������[�U�R���g���[���ɃZ�b�g�i��񕥖߁j
        '****************************
        '�\�����[�h�̏�����
        strViewMode = String.Empty

        '���㏈���ϔ��f�t���O�̎擾
        strUriageZumi = cl.GetUriageSyoriZumiFlg(Me.SpanShUriageSyorizumi.InnerHtml)
        If Me.SpanKyKaiyakuUriageSyorizumi.InnerText = EarthConst.URIAGE_ZUMI Then
            strUriageZumi = EarthConst.URIAGE_ZUMI_CODE
        Else
            strUriageZumi = String.Empty
        End If
        strViewMode = cl.GetViewMode(strUriageZumi, userInfo.KeiriGyoumuKengen)

        '�Q�ƃ��[�h�̐ݒ�
        If Me.SelectKyKaiyakuHaraimodosiSinseiUmu.Style("display") = "none" Then
            strViewMode = EarthConst.MODE_VIEW
        End If

        Me.ucSeikyuuSiireLinkKai.SetVariableValueCtrlFromParent(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                                    , Me.TextKySyouhinCd.Text _
                                                                    , Me.HiddenDefaultSiireSakiCdForLink.Value _
                                                                    , strUriageZumi _
                                                                    , _
                                                                    , _
                                                                    , _
                                                                    , strViewMode)

    End Sub

    ''' <summary>
    ''' �Ɩ�����[���[�U�[�R���g���[��]���[�h������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ucGyoumuKyoutuu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ucGyoumuKyoutuu.Load

        '�ŏI�X�V�ҁA�ŏI�X�V�������Z�b�g
        TextSaisyuuKousinsya.Text = ucGyoumuKyoutuu.AccLastupdateusernm.Value
        TextSaisyuuKousinDate.Text = ucGyoumuKyoutuu.AccLastupdatedatetime.Value

        '****************************
        '* ����������
        '****************************

        '�����N�����̂�
        If IsPostBack = False Then
            '��ʐ���
            SetEnableControl()
            SetEnableControlHhDate(Me.HiddenBukkenJyky.Value)

        End If

        '�ۏ؋Ɩ�����
        If userInfo.HosyouGyoumuKengen = 0 Then
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable
            htNotTarget.Add(ucGyoumuKyoutuu.AccDataHakiSyubetu.ID, ucGyoumuKyoutuu.AccDataHakiSyubetu) '�f�[�^�j�����
            htNotTarget.Add(ucGyoumuKyoutuu.AccDataHakiDate.ID, ucGyoumuKyoutuu.AccDataHakiDate) '�f�[�^�j����
            htNotTarget.Add(SelectHannyouNo.ID, SelectHannyouNo) '�ėpNO
            jSM.Hash2Ctrl(UpdatePanelHosyou, EarthConst.MODE_VIEW, ht, htNotTarget)

            '�o�^�{�^��
            ButtonTouroku1.Disabled = True
            ButtonTouroku2.Disabled = True
            ButtonHkKousin.Disabled = True
            ButtonHakkouCancel.Disabled = True
            ButtonHakkouSet.Disabled = True
        End If

        '�j������
        If userInfo.DataHakiKengen = 0 Then
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable
            jSM.Hash2Ctrl(ucGyoumuKyoutuu.AccTdDataHakiSyubetu, EarthConst.MODE_VIEW, ht) '�f�[�^�j�����
            jSM.Hash2Ctrl(ucGyoumuKyoutuu.AccTdDataHakiData, EarthConst.MODE_VIEW, ht) '�f�[�^�j����

        End If

        '�����X�R�[�h���ςɂȂ����̂ŁA�`�F�b�N�͏����N�����̂ݍs�Ȃ�
        If IsPostBack = False Then
            '���ʏ��̓��̓`�F�b�N
            If ucGyoumuKyoutuu.AccKameitenCd.Value = "" Then '�����X�R�[�h
                Dim tmpScript As String = ""

                '�o�^�{�^���̔񊈐���
                ButtonTouroku1.Disabled = True
                ButtonTouroku2.Disabled = True
                ButtonHkKousin.Disabled = True
                ButtonHakkouCancel.Disabled = True
                ButtonHakkouSet.Disabled = True

                tmpScript = "alert('" & Messages.MSG065W & "');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ucGyoumuKyoutuu_Load", tmpScript, True)

                '�n�Չ�ʋ��ʃN���X
                Dim noTarget As New Hashtable
                Dim jSM As New JibanSessionManager
                Dim ht As New Hashtable
                Dim htNotTarget As New Hashtable
                htNotTarget.Add(SelectHannyouNo.ID, SelectHannyouNo) '�ėpNO

                '�S�ẴR���g���[���𖳌���
                jSM.Hash2Ctrl(UpdatePanelHosyou, EarthConst.MODE_VIEW, ht, htNotTarget)
            End If
        End If

        '�j����ʃ`�F�b�N
        If ucGyoumuKyoutuu.AccDataHakiSyubetu.SelectedValue <> "0" Then
            '�j����ʂ��ݒ肳��Ă���ꍇ�A���ׂẴR���g���[���𖳌���
            CheckHakiDisable(True)
        End If

        '�f�t�H���g�t�H�[�J�X
        If ButtonTouroku1.Disabled <> True Then
            SetFocus(ButtonTouroku1)
        End If

    End Sub

#End Region

#Region "�C�x���g"
    ''' <summary>
    ''' �o�^/�C�� ���s�{�^���P,�Q�������̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTourokuSyuuseiJikkou_ServerClick(ByVal sender As System.Object, _
                                                         ByVal e As System.EventArgs) _
                                                         Handles ButtonTouroku1.ServerClick, _
                                                                 ButtonTouroku2.ServerClick
        Dim tmpScript As String = ""

        ' ���̓`�F�b�N
        '���ʏ��
        If ucGyoumuKyoutuu.checkInput() = False Then Exit Sub

        '�ۏ�
        If checkInput() = False Then Exit Sub

        If SaveData() Then '�o�^����
            cl.CloseWindow(Me)

        Else '�o�^���s
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "�o�^/�C��") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonTouroku_ServerClick", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ���s�Z�b�g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHakkouSet_ServerClick(ByVal sender As System.Object, _
                                              ByVal e As System.EventArgs) _
                                              Handles ButtonHakkouSet.ServerClick

        ' 1 ���͓��e�̃`�F�b�N
        If Not CheckHakIraiUketuke() Then
            ' �`�F�b�N�G���[�Ȃ�I��
            Exit Sub
        End If

        ' 2 �l�̎����Z�b�g(CheckIraiUketuke() �̌��ʔ��f)
        If HiddenHakkouSetTo.Value = "1" Then
            ' �ۏ؏����s������ => ��ʁD�ۏ؏����s���ɉ�ʁD�Z�b�g���s�����Z�b�g
            TextHosyousyoHakkouDate.Text = TextSetHakkouDate.Text
            TextHosyousyoHakkouDate_TextChanged(sender, e)
        ElseIf HiddenHakkouSetTo.Value = "2" Then
            ' �ۏ؏��Ĕ��s������ => ��ʁD�ۏ؏��Ĕ��s���ɉ�ʁD�Z�b�g���s�����Z�b�g
            TextSaihakkouDate.Text = TextSetHakkouDate.Text
            TextSaihakkouDate_TextChanged(sender, e)
        ElseIf HiddenHakkouSetTo.Value = "3" Then
            ' �Ĕ��s���i�R�[�h����
            ' => ��ʁD�ۏ؏��Ĕ��s���ɉ�ʁD�Z�b�g���s�����Z�b�g
            TextSaihakkouDate.Text = TextSetHakkouDate.Text
            ' => ��ʁD�Ĕ��s���R�ɉ�ʁD�Ĕ��s���R + �g3�ʖځh���Z�b�g
            TextSaihakkouRiyuu.Text = TextSaihakkouRiyuu.Text + " 2�x�ڂ̍Ĕ��s����"
            ' => ��ʁD�ۏ؏��Ĕ��s�������L��̏�ԃZ�b�g
            SelectShSeikyuuUmu.Text = "1"
            ' => �t���O�Z�b�g
            Me.ucSeikyuuSiireLinkSai.AccHiddenChkSeikyuuSakiChg.Value = "1"
            TextSaihakkouDate_TextChanged(sender, e)
        Else
            ' ������ɂ��Y�����Ȃ��ꍇ
            ' => ��ʁD�ۏ؏��Ĕ��s���ɉ�ʁD�Z�b�g���s�����Z�b�g
            TextSaihakkouDate.Text = TextSetHakkouDate.Text
            TextSaihakkouDate_TextChanged(sender, e)
        End If

        ' �n�Ճe�[�u��.���s�˗���t�����ɃV�X�e���������Z�b�g
        HiddenHakIraiUketukeFlg.Value = "1"

        ' 3 ��t�Z�b�g�{�^���ƃL�����Z���{�^����񊈐�
        ButtonHakkouSet.Disabled = True
        ButtonHakkouCancel.Disabled = True
        ButtonBukkenTenki.Disabled = True
        ButtonJyuusyoTenki.Disabled = True
        ButtonHosyouKaisiDateTenki.Disabled = True

        ' 4 �m�F���b�Z�[�W��\������
        Dim tmpScript As String = ""
        tmpScript = "alert('" & Messages.MSG207S & "');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ucGyoumuKyoutuu_Load", tmpScript, True)

        Me.UpdatePanelHosyou.Update()
        '���i���̐����E�d���悪�ύX����Ă��Ȃ������`�F�b�N���A
        '�ύX����Ă���ꍇ���̍Ď擾
        setSeikyuuSiireHenkou(sender, e)

        ' 
        ' SetFuhoSyoumeisyoFlg()
        ' SetEnableControl()
    End Sub

    ''' <summary>
    ''' �Ɩ�����[���[�U�[�R���g���[��]��ucGyoumuKyoutuu_OyaGamenAction_hensyu�ŌĂ΂�鏈��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ucGyoumuKyoutuu_OyaGamenAction_hensyu(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal blnDisabled As Boolean) Handles ucGyoumuKyoutuu.OyaGamenAction_hensyu
        '�R���g���[���̗L��/����
        CheckHakiDisable(blnDisabled)
    End Sub

    ''' <summary>
    ''' �j����ʂɂ���āA�R���g���[���̗L��/������ؑւ���
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckHakiDisable(Optional ByVal blnFlg As Boolean = False)
        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban

        '�L�����A�������̑ΏۊO�ɂ���R���g���[���S
        Dim noTarget As New Hashtable
        noTarget.Add(divHakkouIrai, True) '���s�˗��^�u
        noTarget.Add(divHosyou, True) '�ۏ؃^�u
        noTarget.Add(divKairyouKouji, True) '���ǍH���^�u
        noTarget.Add(divSaihakkou, True) '�Ĕ��s�^�u
        noTarget.Add(ButtonTouroku1.ID, True) '�o�^�{�^��1
        noTarget.Add(ButtonTouroku2.ID, True) '�o�^�{�^��2
        noTarget.Add(ButtonHkKousin.ID, True) '�X�V�{�^��
        ' noTarget.Add(ButtonBukkenTenki.ID, True) ' �����]�L�{�^��
        ' noTarget.Add(ButtonJyuusyoTenki.ID, True) ' �Z���]�L�{�^��
        ' noTarget.Add(ButtonHosyouKaisiDateTenki.ID, True) ' �J�n���]�L�{�^��()
        ' noTarget.Add(ButtonHakkouCancel.ID, True) ' ���s�L�����Z���{�^��
        ' noTarget.Add(ButtonHakkouSet.ID, True) ' ���s�Z�b�g�{�^��

        If blnFlg Then
            '�S�ẴR���g���[���𖳌���()
            jBn.ChangeDesabledAll(divHakkouIrai, True, noTarget)
            jBn.ChangeDesabledAll(divHosyou, True, noTarget)
            jBn.ChangeDesabledAll(divKairyouKouji, True, noTarget)
            jBn.ChangeDesabledAll(divSaihakkou, True, noTarget)
        Else
            '�S�ẴR���g���[����L����()
            jBn.ChangeDesabledAll(divHakkouIrai, False, noTarget)
            jBn.ChangeDesabledAll(divHosyou, False, noTarget)
            jBn.ChangeDesabledAll(divKairyouKouji, False, noTarget)
            jBn.ChangeDesabledAll(divSaihakkou, False, noTarget)
        End If

    End Sub

    ''' <summary>
    ''' ���s�˗���t�`�F�b�N
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckHakIraiUketuke()
        Dim e As New System.EventArgs
        Dim tmpScript As String = ""

        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban

        '�G���[���b�Z�[�W������
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New ArrayList

        ' �ۏ؏����s���^�Ĕ��s���^���i�R�[�h���ύX����Ă���
        If HiddenHosyousyoHakkouDateMae.Value <> TextHosyousyoHakkouDate.Text Or _
           Me.HiddenSaihakkouDateOld.Value <> Me.TextSaihakkouDate.Text Or _
           Me.HiddenShSyouhinCdOld.Value <> TextShSyouhinCd.Text Then
            errMess += Messages.MSG208E.Trim
            arrFocusTargetCtrl.Add(Me.TextShSyouhinCd)
        End If

        ' �����X���ӎ������55������A����ʁD�ۏ؏����s�����󔒂���ʁD�ۏ؊J�n�����󔒂̏ꍇ
        If kameitenlogic.ChkBuilderData55(Me.ucGyoumuKyoutuu.AccKameitenCd.Value) And _
           TextHosyousyoHakkouDate.Text = "" And _
           TextHosyouKaisiDate.Text = "" Then
            errMess += Messages.MSG209E.Trim
            arrFocusTargetCtrl.Add(Me.TextHosyousyoHakkouDate)
        End If

        ' �Z�b�g���s�����󔒂̏ꍇ
        If TextSetHakkouDate.Text = "" Then
            errMess += Messages.MSG210E.Trim
            arrFocusTargetCtrl.Add(Me.TextSetHakkouDate)
        ElseIf Date.Compare(Date.Parse(TextSetHakkouDate.Text), Date.Today) = -1 Then
            ' �Z�b�g���s�����ߋ����t�̏ꍇ
            errMess += Messages.MSG211E.Trim
            arrFocusTargetCtrl.Add(Me.TextSetHakkouDate)
        End If

        '�G���[�������͉�ʍĕ`��̂��߁A�R���g���[���̗L��������ؑւ���
        If errMess <> "" Then
            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            '�G���[���b�Z�[�W�\��
            tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        '�G���[�����̏ꍇ�ATrue��Ԃ�
        Return True

    End Function


    ''' <summary>
    ''' ���͍��ڃ`�F�b�N
    ''' </summary>
    ''' <remarks>
    ''' �R�[�h���͒l�ύX�`�F�b�N<br/>
    ''' �K�{�`�F�b�N<br/>
    ''' �֑������`�F�b�N(��������̓t�B�[���h���Ώ�)<br/>
    ''' �o�C�g���`�F�b�N(��������̓t�B�[���h���Ώ�)<br/>
    ''' �����`�F�b�N<br/>
    ''' ���t�`�F�b�N<br/>
    ''' ���̑��`�F�b�N<br/>
    ''' </remarks>
    Private Function checkInput() As Boolean
        Dim e As New System.EventArgs

        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban

        '�S�ẴR���g���[����L����(�j����ʃ`�F�b�N�p)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        '�G���[���b�Z�[�W������
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New ArrayList

        If ucGyoumuKyoutuu.AccDataHakiSyubetu.SelectedValue >= "1" Then
            '�j����ʂ��I������Ă���ꍇ�A�X���[
        Else

            '���͗L��̏ꍇ
            If Me.TextYoteiKameitenCd.Value <> String.Empty Then
                '���R�[�h���͒l�ύX�`�F�b�N
                '�ύX�㌟���{�^�������`�F�b�N
                If (Me.TextYoteiKameitenCd.Value <> Me.HiddenYoteiKameitenCdTextMae.Value) Or _
                    (Me.TextYoteiKameitenCd.Value <> String.Empty And Me.TextYoteiKameitenMei.Value = String.Empty) Then
                    errMess += Messages.MSG030E.Replace("@PARAM1", "�ύX�\������X�R�[�h")
                    arrFocusTargetCtrl.Add(Me.TextYoteiKameitenCd)
                End If
            End If

            '���K�{�`�F�b�N
            '******************************
            '* <�ۏ؏��Ĕ��s>
            '******************************
            '(Chk17:<�ۏ؉��><�ۏ؏��Ĕ��s>�ۏ؏��Ĕ��s�������͂̏ꍇ�A�`�F�b�N���s���B)
            If TextSaihakkouDate.Text <> "" Then
                '����
                If Me.HiddenSaihakkouDateOld.Value = String.Empty Then '�Ĕ��s��Old=������(����̏ꍇ)
                ElseIf Me.HiddenSaihakkouDateOld.Value <> String.Empty AndAlso Me.HiddenSaihakkouDateOld.Value = Me.TextSaihakkouDate.Text Then '�Ĕ��s��=����(�����̏ꍇ�A���񈵂�)
                Else '�Ĕ��s�����قȂ�ꍇ�A���I���̓G���[
                    If Me.SelectShSeikyuuUmu.SelectedValue = String.Empty Then
                        errMess += Messages.MSG013E.Replace("@PARAM1", "����")
                        arrFocusTargetCtrl.Add(SelectShSeikyuuUmu)
                    End If
                End If
                '����
                If Me.TextSaihakkouRiyuu.Text <> String.Empty Then '�Ĕ��s���R=����
                    If Me.SelectShSeikyuuUmu.SelectedValue = String.Empty Then
                        errMess += Messages.MSG153E.Replace("@PARAM1", "�Ĕ��s���R").Replace("@PARAM2", "����")
                        arrFocusTargetCtrl.Add(SelectShSeikyuuUmu)
                    End If
                Else
                    '�Ĕ��s���R
                    If Me.SelectShSeikyuuUmu.SelectedValue <> String.Empty Then
                        errMess += Messages.MSG153E.Replace("@PARAM1", "����").Replace("@PARAM2", "�Ĕ��s���R")
                        arrFocusTargetCtrl.Add(TextSaihakkouRiyuu)
                    End If
                End If

                '�����L��
                If SelectShSeikyuuUmu.SelectedValue = "1" Then '�L
                    '�������Ŕ����z
                    If TextShJituseikyuuKingaku.Text.Length = 0 Then
                        errMess += Messages.MSG013E.Replace("@PARAM1", "�������Ŕ����z")
                        arrFocusTargetCtrl.Add(TextShJituseikyuuKingaku)
                    End If
                    '���������s��
                    If TextShSeikyuusyoHakkouDate.Text.Length = 0 Then
                        errMess += Messages.MSG013E.Replace("@PARAM1", "���������s��")
                        arrFocusTargetCtrl.Add(TextShSeikyuusyoHakkouDate)
                    End If

                End If

            End If

        End If

        '�����t�`�F�b�N
        '�f�[�^�j����
        If ucGyoumuKyoutuu.AccDataHakiDate.Value <> "" Then
            If cl.checkDateHanni(ucGyoumuKyoutuu.AccDataHakiDate.Value) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�f�[�^�j����")
                arrFocusTargetCtrl.Add(ucGyoumuKyoutuu.AccDataHakiDate)
            End If
        End If
        '��b�񍐏�����
        If TextKisoHoukokusyoTyakuDate.Text <> "" Then
            If cl.checkDateHanni(TextKisoHoukokusyoTyakuDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "��b�񍐏�����")
                arrFocusTargetCtrl.Add(TextKisoHoukokusyoTyakuDate)
            End If
        End If
        '���s�˗�������
        If TextHakkouIraiTyakuDate.Text <> "" Then
            If cl.checkDateHanni(TextHakkouIraiTyakuDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "���s�˗�������")
                arrFocusTargetCtrl.Add(TextHakkouIraiTyakuDate)
            End If
        End If
        '�ۏ؏����s��
        If TextHosyousyoHakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextHosyousyoHakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�ۏ؏����s��")
                arrFocusTargetCtrl.Add(TextHosyousyoHakkouDate)
            End If
        End If
        '�Ɩ�������
        If TextGyoumuKanryouDate.Text <> "" Then
            If cl.checkDateHanni(TextGyoumuKanryouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�Ɩ�������")
                arrFocusTargetCtrl.Add(TextGyoumuKanryouDate)
            End If
        End If


        '****************
        '* �ۏ؏��Ĕ��s
        '****************
        '�ۏ؊J�n��
        If TextHosyouKaisiDate.Text <> "" Then
            If cl.checkDateHanni(TextHosyouKaisiDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�ۏ؊J�n��")
                arrFocusTargetCtrl.Add(TextHosyouKaisiDate)
            End If
        End If
        '�Ĕ��s��
        If TextSaihakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextSaihakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�Ĕ��s��")
                arrFocusTargetCtrl.Add(TextSaihakkouDate)
            End If
        End If
        '���������s��
        If TextShSeikyuusyoHakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextShSeikyuusyoHakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "���������s��")
                arrFocusTargetCtrl.Add(TextShSeikyuusyoHakkouDate)
            End If
        End If

        '****************
        '* ��񕥖�
        '****************
        '���������s��
        If TextKySeikyuusyoHakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextKySeikyuusyoHakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "���������s��")
                arrFocusTargetCtrl.Add(TextKySeikyuusyoHakkouDate)
            End If
        End If

        '�������`�F�b�N(�Ȃ�)

        '���֑������`�F�b�N(��������̓t�B�[���h���Ώ�)
        '�_��NO
        If jBn.KinsiStrCheck(TextKeiyakuNo.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�_��NO")
            arrFocusTargetCtrl.Add(TextKeiyakuNo)
        End If
        '�ۏ؂Ȃ����R
        If jBn.KinsiStrCheck(TextHosyouNasiRiyuu.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�ۏ؂Ȃ����R")
            arrFocusTargetCtrl.Add(TextHosyouNasiRiyuu)
        End If
        '�Ĕ��s���R
        If jBn.KinsiStrCheck(TextSaihakkouRiyuu.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�Ĕ��s���R")
            arrFocusTargetCtrl.Add(TextSaihakkouRiyuu)
        End If

        '���o�C�g���`�F�b�N(��������̓t�B�[���h���Ώ�)
        '�_��NO
        If jBn.ByteCheckSJIS(TextKeiyakuNo.Text, TextKeiyakuNo.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�_��NO")
            arrFocusTargetCtrl.Add(TextKeiyakuNo)
        End If
        '�ۏ؂Ȃ����R
        If jBn.ByteCheckSJIS(TextHosyouNasiRiyuu.Text, TextHosyouNasiRiyuu.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�ۏ؂Ȃ����R")
            arrFocusTargetCtrl.Add(TextHosyouNasiRiyuu)
        End If
        '�Ĕ��s���R
        If jBn.ByteCheckSJIS(TextSaihakkouRiyuu.Text, TextSaihakkouRiyuu.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�Ĕ��s���R")
            arrFocusTargetCtrl.Add(TextSaihakkouRiyuu)
        End If

        '�����̑��`�F�b�N
        '�n��T.�ۏ؏��i�L��="��"
        If Me.HiddenHosyouSyouhinUmu.Value <> "1" Then
            '�ۏ؏����s��.�ۏ؂���
            If cbLogic.GetHosyousyoHakJykyHosyouUmu(Me.SelectHosyousyoHakkouJyoukyou.SelectedValue) = "1" Then
                errMess += Messages.MSG145E.Replace("@PARAM1", "���i�ݒ��").Replace("@PARAM2", EarthConst.DISP_HOSYOU_NASI_HOSYOU_SYOUHIN_UMU).Replace("@PARAM3", "�ۏ؏����s��")
                arrFocusTargetCtrl.Add(Me.SelectHosyousyoHakkouJyoukyou)
            End If
        End If
        '******************************
        '* <�ۏ؏��>
        '******************************
        '(Chk19:�ύX�\������X�e�L�X�g�{�b�N�X�����͍ς݁A���A�����̉����X�ƕύX�\������X���قȂ�A���A�ۏ؏����s�������͍ς݂̏ꍇ�A�G���[�Ƃ���B)
        If Me.TextYoteiKameitenCd.Value <> String.Empty _
            AndAlso Me.TextYoteiKameitenCd.Value <> ucGyoumuKyoutuu.AccKameitenCd.Value _
            AndAlso Me.TextHosyousyoHakkouDate.Text <> String.Empty Then

            errMess += Messages.MSG203E
            arrFocusTargetCtrl.Add(Me.TextHosyousyoHakkouDate)
        End If

        '******************************
        '* <��񕥖�>
        '******************************
        '(Chk16:�����L�����L��A���A����N�������ݒ�ρA���A���������s���������͂̏ꍇ�A�G���[�Ƃ���B)
        '���������s��
        If SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "1" And TextKyUriageNengappi.Text <> "" And TextKySeikyuusyoHakkouDate.Text = "" Then
            errMess += Messages.MSG062E.Replace("@PARAM1", "���������s��")
            arrFocusTargetCtrl.Add(TextKySeikyuusyoHakkouDate)
        End If

        '******************************
        '* <���ǍH��>
        '******************************
        '(Chk03:<�ۏ؉��>�ۏ؏����s����[�n�Ճe�[�u��]�������{���A���A�ۏ؉�ʂœo�^�����Ă��Ȃ��ꍇ�A�m�F���b�Z�[�W��\������B
        '�m�FOK�̏ꍇ�A�o�^������B�i2�x�ڂ���́A�m�F���b�Z�[�W��\�������ɁA�������œo�^������j
        '�ۏ؏����s��=>JS�ɂă`�F�b�N

        '(Chk04:<�ۏ؉��>�ۏ؏����s�������́A[�n�Ճe�[�u��]�������{���������́A���A�ۏ؉�ʂœo�^�����Ă��Ȃ��ꍇ�A�m�F���b�Z�[�W��\������B
        '�m�FOK�̏ꍇ�A�o�^������B�i2�x�ڂ���́A�m�F���b�Z�[�W��\�������ɁA�������œo�^������j
        '�ۏ؏����s��=>JS�ɂă`�F�b�N

        '�G���[�������͉�ʍĕ`��̂��߁A�R���g���[���̗L��������ؑւ���
        If errMess <> "" Then
            '�t�H�[�J�X�Z�b�g
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            '�G���[���b�Z�[�W�\��
            Dim tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False

        End If

        '�G���[�����̏ꍇ�ATrue��Ԃ�
        Return True

    End Function

    ''' <summary>
    ''' ��ʂ̓��e��DB�ɔ��f����
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SaveData() As Boolean
        '*************************
        '�n�Ճf�[�^�͍X�V�Ώۂ̂�
        '�@�ʐ����f�[�^�͑S�čX�V
        '���������f�[�^�͕ۏ؏��i�L���̕ύX������ꍇ�A�o�^
        '*************************
        Dim JibanLogic As New JibanLogic
        Dim jrOld As New JibanRecord
        ' ���݂̒n�Ճf�[�^��DB����擾����
        jrOld = JibanLogic.GetJibanData(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou)

        ' ��ʓ��e���n�Ճ��R�[�h�𐶐�����
        Dim jibanRec As New JibanRecordHosyou

        jibanRec = Me.GetCtrlDataRecord()

        '���������f�[�^�̎����Z�b�g
        Dim brRec As BukkenRirekiRecord = cbLogic.MakeBukkenRirekiRecHosyouUmu(jibanRec, cl.GetDisplayString(jrOld.HosyouSyouhinUmu), cl.GetDisplayString(jrOld.KeikakusyoSakuseiDate))

        If Not brRec Is Nothing Then
            '�����������R�[�h�̃`�F�b�N
            Dim strErrMsg As String = String.Empty
            If cbLogic.checkInputBukkenRireki(brRec, strErrMsg) = False Then
                MLogic.AlertMessage(Me, strErrMsg, 0, "ErrBukkenRireki")
                Exit Function
            End If
        End If
        '===========================================================
        ' �f�[�^�̍X�V���s���܂�
        If MyLogic.SaveJibanData(Me, jibanRec, brRec) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' �ύX�\������X�����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonYoteiKameitenSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonYoteiKameitenSearch.ServerClick

        If yoteiKameitenSearchType.Value <> "1" Then
            kameitenSearchSub(sender, e)
        Else
            '�R�[�honchange�ŌĂ΂ꂽ�ꍇ
            kameitenSearchSub(sender, e, False)
            yoteiKameitenSearchType.Value = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' �ύX�\������X�����{�^���������̏���(����)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="callWindow">�����|�b�v�A�b�v���N�����邩�ۂ��̎w��</param>
    ''' <remarks></remarks>
    Protected Sub kameitenSearchSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^��1���̂ݎ擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim kLogic As New KameitenSearchLogic
        Dim dataArray As New List(Of KameitenSearchRecord)
        Dim total_count As Integer = 0
        Dim blnTorikesi As Boolean

        '�����X�R�[�h=���͂̏ꍇ
        If Me.TextYoteiKameitenCd.Value <> String.Empty Then

            'DB�l�Ɠ����ꍇ�A����=0��������Ɋ܂߂Ȃ�
            If Me.TextYoteiKameitenCd.Value = Me.HiddenYoteiKameitenCdTextOld.Value Then
                blnTorikesi = False
            Else
                '�Ɩ�����Ctrl�̉����X�R�[�h�Ɠ����ꍇ�A����=0��������Ɋ܂߂Ȃ�
                If Me.TextYoteiKameitenCd.Value = ucGyoumuKyoutuu.AccKameitenCdTextOld.Value Then

                    blnTorikesi = False
                Else
                    blnTorikesi = True
                End If
            End If

            '���������s
            dataArray = kLogic.GetKameitenSearchResult(Me.ucGyoumuKyoutuu.AccHiddenKubun.Value _
                                                        , Me.TextYoteiKameitenCd.Value _
                                                        , blnTorikesi _
                                                        , total_count)
        End If

        If total_count = 1 Then

            '�����X�R�[�h����꒼��
            Dim recData As KameitenSearchRecord = dataArray(0)
            Me.TextYoteiKameitenCd.Value = recData.KameitenCd

            '�t�H�[�J�X�Z�b�g
            setFocusAJ(Me.ButtonYoteiKameitenSearch)
        Else
            If callWindow = True Then
                '�����X��
                Me.TextYoteiKameitenMei.Value = String.Empty

                '������ʕ\���pJavaScript�wcallSearch�x�����s
                Dim tmpFocusScript = "objEBI('" & ButtonYoteiKameitenSearch.ClientID & "').focus();"
                Dim tmpScript As String = "callSearch('" & Me.ucGyoumuKyoutuu.AccHiddenKubun.ClientID & EarthConst.SEP_STRING & Me.TextYoteiKameitenCd.ClientID & _
                                                "','" & UrlConst.SEARCH_KAMEITEN & _
                                                "','" & Me.TextYoteiKameitenCd.ClientID & EarthConst.SEP_STRING & Me.TextYoteiKameitenMei.ClientID & _
                                                "','" & Me.ButtonYoteiKameitenSearch.ClientID & "');"


                tmpScript = tmpFocusScript + tmpScript
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSearch", tmpScript, True)
            End If
        End If

        ' �����X�������s�㏈�����s
        kameitenSearchAfter_ServerClick(sender, e, blnTorikesi)

    End Sub

    ''' <summary>
    ''' �����X�������s�㏈��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="blnTorikesi">����Ώۃt���O</param>
    ''' <remarks></remarks>
    Protected Sub kameitenSearchAfter_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal blnTorikesi As Boolean)

        '�����X�������s�㏈��(�����X�ڍ׏��A�r���_�[���擾)
        Dim kLogic As New KameitenSearchLogic
        Dim recData As KameitenSearchRecord = kLogic.GetKameitenSearchResult(Me.ucGyoumuKyoutuu.AccHiddenKubun.Value, Me.TextYoteiKameitenCd.Value, "", blnTorikesi)
        Dim strErrMsg As String = String.Empty

        If Me.TextYoteiKameitenCd.Value <> String.Empty Then    '����
            If Not recData Is Nothing AndAlso recData.KameitenCd <> String.Empty Then
                '�����X�����Z�b�g
                Me.SetKameitenInfo(recData)
            Else
                '�N���A���s�Ȃ�
                ClearKameitenInfo(False)
            End If
        Else    '������
            ClearKameitenInfo()
        End If

    End Sub

    ''' <summary>
    ''' �����X�����Z�b�g����
    ''' </summary>
    ''' <param name="recData"></param>
    ''' <remarks></remarks>
    Private Sub SetKameitenInfo(ByVal recData As KameitenSearchRecord)

        If Not recData Is Nothing AndAlso recData.KameitenCd <> String.Empty Then
            '��ʂɒl���Z�b�g
            Me.TextYoteiKameitenCd.Value = cl.GetDispStr(recData.KameitenCd)    '�����X�E�R�[�h
            Me.TextYoteiKameitenMei.Value = cl.GetDispStr(recData.KameitenMei1) '�����X�E����

            '�����X�R�[�h��ޔ�
            Me.HiddenYoteiKameitenCdTextMae.Value = Me.TextYoteiKameitenCd.Value
        End If

    End Sub

    ''' <summary>
    ''' �����X���̃N���A���s�Ȃ�
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearKameitenInfo(Optional ByVal blnFlg As Boolean = True)

        '����������
        If blnFlg Then
            Me.TextYoteiKameitenCd.Value = String.Empty
        End If
        Me.TextYoteiKameitenMei.Value = String.Empty

        '�t�H�[�J�X�̐ݒ�
        Me.setFocusAJ(Me.ButtonYoteiKameitenSearch)
    End Sub

    ''' <summary>
    ''' �X�V�{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHkKousin_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHkKousin.ServerClick
        '�ۏ؏��Ǘ��e�[�u���X�V����
        Dim dtHkUpdDatetime As DateTime
        dtHkUpdDatetime = DateTime.ParseExact(HiddenHkUpdDatetime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)

        '�ۏ؏��Ǘ��e�[�u���ǉ�/�X�V����
        If BJykyLogic.setHosyousyoKanriBukken(sender, ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou, dtHkUpdDatetime) Then

            '�ۏ؏��Ǘ��f�[�^�擾
            Dim hr As New HosyousyoKanriRecord
            hr = BJykyLogic.getSearchKeyDataRec(sender, ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou)

            '��ʂɐݒ�
            If Not hr Is Nothing Then
                '������
                SetBukkenJyky(hr.BukkenJyky)
                ' �X�V����
                HiddenHkUpdDatetime.Value = IIf(hr.UpdDateTime = DateTime.MinValue, Format(hr.AddDateTime, EarthConst.FORMAT_DATE_TIME_1), Format(hr.UpdDateTime, EarthConst.FORMAT_DATE_TIME_1))
                ' �Ɩ�������
                TextGyoumuKanryouDate.Text = cl.GetDisplayString(hr.GyoumuKanryDate)
                ' �Ɩ��J�n���e
                TextGyoumuKaisiNaiyou.Text = cl.GetDisplayString(hr.GyoumuKaisiNaiyou)

                MLogic.AlertMessage(Me, Messages.MSG018S.Replace("@PARAM1����", "�����󋵂̍ŐV��"), 0)
            End If
        Else
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1����", "�����󋵂̍ŐV��"), 1)
        End If

        '���������X�R�[�h�̊���������
        ucGyoumuKyoutuu.SetEnableKameiten(Me.HiddenBukkenJyky.Value)

        setFocusAJ(ButtonHkKousin) '�t�H�[�J�X
    End Sub

#End Region

#Region "�����ݒ胁�\�b�h"
    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispAction()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ���V�X�e���ւ̃����N�{�^���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�����X���ӎ���
        cl.getKameitenTyuuijouhouPath(Me.TextYoteiKameitenCd.ClientID, Me.ButtonYoteiKameitenTyuuijouhou)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript�֘A�Z�b�g
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim onFocusPostBackScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScript As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this)){__doPostBack(this.id,'');}}else{checkNumberAddFig(this);}"
        Dim onFocusScript As String = "removeFig(this);"
        Dim onBlurScript As String = "checkNumberAddFig(this);"
        Dim checkDate As String = "checkDate(this);"
        Dim checkKingaku As String = "checkKingaku(this);"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '���t���ڗp
        Dim onFocusPostBackScriptDate As String = "setTempValueForOnBlur(this);"
        Dim onBlurPostBackScriptDate As String = "if(checkTempValueForOnBlur(this)){if(checkDate(this)){__doPostBack(this.id,'');}}else{checkDate(this);}"

        '�ύX�\������X�R�[�h
        Me.TextYoteiKameitenCd.Attributes("onfocus") = onFocusPostBackScript
        Me.TextYoteiKameitenCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callYoteiKameitenSearch(this);}else{checkNumber(this);}"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ���z�n
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '***********************
        '* �Ĕ��s
        '***********************
        '�H���X�����Ŕ����z
        TextShKoumutenSeikyuuKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextShKoumutenSeikyuuKingaku.Attributes("onblur") = onBlurPostBackScript
        TextShKoumutenSeikyuuKingaku.Attributes("onkeydown") = disabledOnkeydown
        '�������Ŕ����z
        TextShJituseikyuuKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextShJituseikyuuKingaku.Attributes("onblur") = onBlurPostBackScript
        TextShJituseikyuuKingaku.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ���t�n
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '***********************
        '* ���s�˗�
        '***********************
        '�Z�b�g���s��
        TextSetHakkouDate.Attributes("onblur") = checkDate
        TextSetHakkouDate.Attributes("onkeydown") = disabledOnkeydown

        '***********************
        '* �ۏ�
        '***********************
        '��b�񍐏�����
        TextKisoHoukokusyoTyakuDate.Attributes("onblur") = checkDate
        TextKisoHoukokusyoTyakuDate.Attributes("onkeydown") = disabledOnkeydown
        '���s�˗�����
        TextHakkouIraiTyakuDate.Attributes("onblur") = checkDate
        TextHakkouIraiTyakuDate.Attributes("onkeydown") = disabledOnkeydown
        '�Ɩ�������
        TextGyoumuKanryouDate.Attributes("onblur") = checkDate
        TextGyoumuKanryouDate.Attributes("onkeydown") = disabledOnkeydown
        '�ۏ؏����s��
        TextHosyousyoHakkouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextHosyousyoHakkouDate.Attributes("onfocus") = onFocusPostBackScriptDate & "SetChangeMaeValue('" & HiddenHosyousyoHakkouDateMae.ClientID & "','" & TextHosyousyoHakkouDate.ClientID & "');"
        TextHosyousyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown
        '�ۏ؊J�n��
        TextHosyouKaisiDate.Attributes("onblur") = checkDate
        TextHosyouKaisiDate.Attributes("onkeydown") = disabledOnkeydown

        '***********************
        '* �Ĕ��s
        '***********************
        '�Ĕ��s��
        TextSaihakkouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextSaihakkouDate.Attributes("onfocus") = onFocusPostBackScriptDate & "SetChangeMaeValue('" & HiddenSaihakkouDateMae.ClientID & "','" & TextSaihakkouDate.ClientID & "');"
        TextSaihakkouDate.Attributes("onkeydown") = disabledOnkeydown
        '���������s��
        TextShSeikyuusyoHakkouDate.Attributes("onblur") = checkDate
        TextShSeikyuusyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown

        '***********************
        '* ��񕥖�
        '***********************
        '���������s��
        TextKySeikyuusyoHakkouDate.Attributes("onblur") = checkDate
        TextKySeikyuusyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ �h���b�v�_�E�����X�g
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�����L��
        SelectShSeikyuuUmu.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenShSeikyuuUmuMae.ClientID & "','" & SelectShSeikyuuUmu.ClientID & "')"
        '��񕥖ߐ\���L��
        SelectKyKaiyakuHaraimodosiSinseiUmu.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenKyKaiyakuHaraimodosiSinseiUmuMae.ClientID & "','" & SelectKyKaiyakuHaraimodosiSinseiUmu.ClientID & "')"
        '�ۏ؏����s��
        Me.SelectHosyousyoHakkouJyoukyou.Attributes("onfocus") = "SetChangeMaeValue('" & Me.HiddenHosyousyoHakJyKyMae.ClientID & "','" & Me.SelectHosyousyoHakkouJyoukyou.ClientID & "')"
        ''�t�ۏؖ���FLG(�ۏ؏��֘A�Ή��ɂăR�����g�A�E�g)
        'SelectFuhoSyoumeisyoFlg.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenFuhoSyoumeisyoFlgMae.ClientID & "','" & SelectFuhoSyoumeisyoFlg.ClientID & "')"
        '�ۏ؂Ȃ����R
        Me.SelectHosyouNasiRiyuu.Attributes("onchange") += "checkPullDown();"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ �@�\�ʃe�[�u���̕\���ؑ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '���s�˗����
        AncHakkouIraiInfo.HRef = "JavaScript:changeDisplay('" & TbodyHakkouIraiInfo.ClientID & "');SetDisplayStyle('" & HiddenHakkouIraiInfoStyle.ClientID & "','" & TbodyHakkouIraiInfo.ClientID & "');"
        '�ۏ؏��
        AncHosyouInfo.HRef = "JavaScript:changeDisplay('" & TbodyHoshoInfo.ClientID & "');SetDisplayStyle('" & HiddenHosyouInfoStyle.ClientID & "','" & TbodyHoshoInfo.ClientID & "');"
        '���ǍH��
        AncKairyouKouji.HRef = "JavaScript:changeDisplay('" & TbodyKairyoKouji.ClientID & "');SetDisplayStyle('" & HiddenKairyouKoujiStyle.ClientID & "','" & TbodyKairyoKouji.ClientID & "');"
        '�Ĕ��s
        AncSaiHakkou.HRef = "JavaScript:changeDisplay('" & TbodySaiHakkou.ClientID & "');SetDisplayStyle('" & HiddenSaiHakkouStyle.ClientID & "','" & TbodySaiHakkou.ClientID & "');"

    End Sub

    ''' <summary>
    ''' �o�^/�C�����s�{�^���̐ݒ�
    ''' </summary>
    ''' <remarks>
    ''' DB�X�V�̊m�F���s�Ȃ��B<br/>
    ''' OK���FDB�X�V���s�Ȃ��B<br/>
    ''' �L�����Z�����FDB�X�V�𒆒f����B<br/>
    ''' </remarks>
    Private Sub setBtnEvent()

        '�C�x���g�n���h���o�^
        Dim tmpTouroku As String = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this);}else{return false;}"
        Dim tmpScript As String = "if(CheckTouroku()){" & tmpTouroku & "}else{return false;}"
        Dim tmpScript2 As String = "if(objEBI('" & HiddenAjaxFlg.ClientID & "').value!=1){" & tmpScript & "}else{alert('" & Messages.MSG104E & "');}"
        Dim tmpScript3 As String = "if(CheckIraiUketuke()==false){return false;}"

        '�o�^����MSG�m�F��AOK�̏ꍇDB�X�V�������s�Ȃ�
        ButtonTouroku1.Attributes("onclick") = tmpScript
        ButtonTouroku2.Attributes("onclick") = tmpScript
        ButtonHkKousin.Attributes("onclick") = "if(confirm('" & Messages.MSG017C & "')){}else{return false;}"
        ButtonHakkouCancel.Attributes("onclick") = "if(confirm('" & Messages.MSG215C & "')){}else{return false;}"
        ButtonHakkouSet.Attributes("onclick") = tmpScript3
        ButtonHosyouKaisiDateTenki.Attributes("onclick") = "if(CheckHosyouKaisiDate()==false){return false;}"

    End Sub

    ''' <summary>
    ''' Ajax���쎞�̃t�H�[�J�X�Z�b�g
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub setFocusAJ(ByVal ctrl As Control)
        '�t�H�[�J�X�Z�b�g
        masterAjaxSM.SetFocus(ctrl)
    End Sub

#End Region

#Region "�n�Ճf�[�^���R���g���[���ɐݒ肷��"
    ''' <summary>
    ''' �n�Ճ��R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="jr">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByRef jr As JibanRecordBase)

        Dim jSM As New JibanSessionManager '�Z�b�V�����Ǘ��N���X
        Dim jBn As New Jiban '�n�Չ�ʃN���X
        Dim logic As New HosyouLogic '�ۏ؃��W�b�N�N���X
        Dim kisoSiyouLogic As New KisoSiyouLogic ' ��b�d�l���W�b�N�N���X
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim kameitenRec As New KameitenSearchRecord

        ' �R���{�ݒ�w���p�[�N���X�𐶐�
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        '******************************************
        '* ��ʃR���g���[���ɐݒ�
        '******************************************
        '�����X�R�[�h
        Me.HiddenKameitenCd.Value = cl.GetDisplayString(jr.KameitenCd)

        '***********************
        '* �ۏ؏��
        '***********************
        ' �_��NO
        TextKeiyakuNo.Text = cl.GetDisplayString(jr.KeiyakuNo)
        ' �������{��
        TextTyousaJissiDate.Text = cl.GetDisplayString(jr.TysJissiDate)
        ' �v�揑�쐬��
        TextKeikakusyoSakuseiDate.Text = cl.GetDisplayString(jr.KeikakusyoSakuseiDate)
        ' �����m�F����
        TextNyuukinKakuninJyouken.Text = cl.GetDisplayString(jr.NyuukinKakuninJyoukenMei)
        ' ��b�񍐏�
        SelectKisoHoukokusyo.SelectedValue = cl.GetDisplayString(jr.KsHkksUmu)
        ' ��b�񍐒���
        TextKisoHoukokusyoTyakuDate.Text = cl.GetDisplayString(jr.KsKojKanryHkksTykDate)

        ' �ύX�\������X�R�[�h
        If cl.GetDisplayString(jr.Kbn) <> "" And cl.GetDisplayString(jr.HenkouYoteiKameitenCd) <> "" Then

            kameitenRec = kameitenSearchLogic.GetKameitenSearchResult(jr.Kbn, _
                                                                      jr.HenkouYoteiKameitenCd, _
                                                                      jr.TysKaisyaCd & jr.TysKaisyaJigyousyoCd, _
                                                                      False)
            '�����X�����Z�b�g
            Me.SetKameitenInfo(kameitenRec)

            '�����X�R�[�h��DB�l��ޔ�
            Me.HiddenYoteiKameitenCdTextOld.Value = Me.TextYoteiKameitenCd.Value
        End If

        ' ���s�˗���
        SelectHakkouIraisyo.SelectedValue = _
            cl.GetDisplayString(IIf(jr.HosyousyoHakIraisyoUmu = 1, "1", ""))
        ' ���s�˗�����
        TextHakkouIraiTyakuDate.Text = cl.GetDisplayString(jr.HosyousyoHakIraisyoTykDate)
        ' �ۏ؏����s��
        SelectHosyousyoHakkouJyoukyou.SelectedValue = _
            cl.GetDisplayString(jr.HosyousyoHakJyky)

        Dim strTmpVal As String = String.Empty
        Dim strTmpMei As String = String.Empty

        '���݃`�F�b�N(�����ݒ蓙�̖����݃f�[�^�͐���)
        If cl.ChkDropDownList(Me.SelectHosyousyoHakkouJyoukyou, cl.GetDispNum(jr.HosyousyoHakJyky)) Then
            Me.SelectHosyousyoHakkouJyoukyou.SelectedValue = cl.GetDispNum(jr.HosyousyoHakJyky, "")
        ElseIf jr.HosyousyoHakJyky = 0 Then

            '�ۏ؂���
            strTmpVal = EarthConst.AUTO_SET_VAL_HOSYOU_ARI
            strTmpMei = cbLogic.GetHosyousyoHakJykyMei(strTmpVal)

            Me.SelectHosyousyoHakkouJyoukyou.Items.Add(New ListItem(strTmpVal & ":" & strTmpMei, strTmpVal))

            '�ۏ؂Ȃ�
            strTmpVal = EarthConst.AUTO_SET_VAL_HOSYOU_NASI
            strTmpMei = cbLogic.GetHosyousyoHakJykyMei(strTmpVal)

            Me.SelectHosyousyoHakkouJyoukyou.Items.Add(New ListItem(strTmpVal & ":" & strTmpMei, strTmpVal))

            Me.SelectHosyousyoHakkouJyoukyou.SelectedValue = EarthConst.AUTO_SET_VAL_HOSYOU_NASI  '�I�����
        ElseIf jr.HosyousyoHakJyky > 0 Then

            strTmpVal = cl.GetDispNum(jr.HosyousyoHakJyky, "")
            strTmpMei = cbLogic.GetHosyousyoHakJykyMei(strTmpVal)

            Me.SelectHosyousyoHakkouJyoukyou.Items.Add(New ListItem(strTmpVal & ":" & strTmpMei, strTmpVal))
            Me.SelectHosyousyoHakkouJyoukyou.SelectedValue = strTmpVal  '�I�����
        End If

        ' �ۏ؏����s�󋵐ݒ��
        TextHosyousyoHakkouJyoukyouSetteiDate.Text = _
            cl.GetDisplayString(jr.HosyousyoHakJykySetteiDate)
        ' �ۏ؏����s��
        TextHosyousyoHakkouDate.Text = cl.GetDisplayString(jr.HosyousyoHakkouDate)
        ' �ۏ؏�������
        TextHosyousyoHassouDate.Text = cl.GetDisplayString(jr.HosyousyoHassouDate)
        ' ���s�˗����@
        Dim strHosyousyoHakHouhou As String = cl.GetDisplayString(jr.HosyousyoHakHouhou)
        If strHosyousyoHakHouhou = "0" Then
            Me.spanHosyousyoHakHouhou.InnerHtml = EarthConst.HINSITU_HOSYOUSYO_JYOUKYOU_HAKKOUTIMING_IRAISYO ' �˗���
        ElseIf strHosyousyoHakHouhou = "1" Then
            Me.spanHosyousyoHakHouhou.InnerHtml = EarthConst.HINSITU_HOSYOUSYO_JYOUKYOU_HAKKOUTIMING_JIDOU ' �������s
        ElseIf strHosyousyoHakHouhou = "2" Then
            Me.spanHosyousyoHakHouhou.InnerHtml = EarthConst.HINSITU_HOSYOUSYO_JYOUKYOU_HAKKOUTIMING_JIBANMALL ' �n�Ճ��[��
        Else
            Me.spanHosyousyoHakHouhou.InnerHtml = ""
        End If

        ' �t�ۏؖ���FLG
        SelectFuhoSyoumeisyoFlg.SelectedValue = cl.ChgStrToInt(jr.FuhoSyoumeisyoFlg)
        ' �t�ۏؖ���������
        TextFuhoSyoumeisyoHassouDate.Text = cl.GetDisplayString(jr.FuhoSyoumeisyoHassouDate)
        ' �ۏ؊J�n��
        TextHosyouKaisiDate.Text = cl.GetDisplayString(jr.HosyouKaisiDate)
        ' ���i�ݒ��
        Dim strHosyouSyouhinUmu As String = cl.GetDisplayString(jr.HosyouSyouhinUmu)
        If strHosyouSyouhinUmu = "1" Then
            Me.SpanHosyouSyouhinUmu.InnerHtml = EarthConst.DISP_HOSYOU_ARI_HOSYOU_SYOUHIN_UMU
            Me.SpanHosyouSyouhinUmu.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_BLUE
        Else
            Me.SpanHosyouSyouhinUmu.InnerHtml = EarthConst.DISP_HOSYOU_NASI_HOSYOU_SYOUHIN_UMU
            Me.SpanHosyouSyouhinUmu.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
        End If
        ' �ۏؗL��
        SelectHosyouUmu.SelectedValue = cl.GetDisplayString(IIf(jr.HosyouUmu = 1, "1", ""))
        ' �ۏ؊���
        TextHosyouKikan.Text = cl.GetDisplayString(jr.HosyouKikan)
        ' �ۏ؏����s�� NOT NULL�̏ꍇ�A�ۏ؊��Ԃ͔񊈐�
        If TextHosyousyoHakkouDate.Text <> "" Then
            cl.chgVeiwMode(TextHosyouKikan) ' �񊈐�
        End If

        ' �ۏ؂Ȃ����R�R�[�h
        SelectHosyouNasiRiyuu.SelectedValue = cl.GetDisplayString(jr.HosyouNasiRiyuuCd)
        ' �ėpNO(��\��)
        SelectHannyouNo.SelectedIndex = SelectHosyouNasiRiyuu.SelectedIndex
        ' �ۏ؂Ȃ����R�e�L�X�g�{�b�N�X
        TextHosyouNasiRiyuu.Text = cl.GetDisplayString(jr.HosyouNasiRiyuu)
        ' �ۏ؂Ȃ����R�R�[�h�ύX���C�x���g(JS)�����s
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectHosyouNasiRiyuu_SelectChanged", "checkPullDown();", True)

        If Not jr.Syouhin1Record Is Nothing Then
            ' (����)���������s��
            TextTyousaSeikyuusyoHakkouDate.Text = _
                cl.GetDisplayString(jr.Syouhin1Record.SeikyuusyoHakDate)
        End If

        ' �������������v���z
        TextTyousaHattyuusyoGoukeiKingaku.Text = IIf(jr.GetSyouhinHattyusyoKingaku() = 0, _
                                                                    "0", _
                                                                    cl.GetDisplayString(jr.GetSyouhinHattyusyoKingaku()) _
                                                                    )

        ' �������v�����z(�ō�) 
        TextTyousaGoukeiNyuukingaku.Text = IIf(jr.getNyuukinGaku("100") + jr.getNyuukinGaku("120") = 0, _
                                                    "0", _
                                                    cl.GetDisplayString(jr.getNyuukinGaku("100") + jr.getNyuukinGaku("120"), "0") _
                                                    )

        ' �c�z
        Dim zangaku As Integer = ( _
                                  jr.getZeikomiGaku(New String() {"100", "110", "180"}) - _
                                  jr.getNyuukinGaku("100")) + (jr.getZeikomiGaku(New String() {"120"}) - _
                                  jr.getNyuukinGaku("120") _
                                 )

        TextTyousaZangaku.Text = zangaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        '���_�~�[�R���{�ɃZ�b�g
        objDrpTmp = New DropDownList
        helper.SetMeisyouDropDownList(objDrpTmp, EarthConst.emMeisyouType.SYASIN_JURI)
        objDrpTmp.SelectedValue = cl.ChgStrToInt(jr.TikanKoujiSyasinJuri)
        TextSyasinJuri.Text = objDrpTmp.SelectedItem.Text '�u���H���ʐ^��
        TextSyasinComment.Text = cl.GetDispStr(jr.TikanKoujiSyasinComment) '�u���H���ʐ^�R�����g

        ' ���i�P�̓��R�[�h�̗L���Ɋւ�炸�\�����Ă����i�����K�{�Ȃ̂Łj
        If Not jr.Syouhin1Record Is Nothing Then
            TextSyouhin1A.Text = cl.GetDisplayString(jr.Syouhin1Record.SyouhinCd)           ' ���i�P�D�R�[�h
            TextSyouhin1B.Text = cl.GetDisplayString(jr.Syouhin1Record.SyouhinMei)          ' ���i�P�D����
            HiddenSyouhin1SeikyuuHakkouDate.Value = cl.GetDisplayString(jr.Syouhin1Record.SeikyuusyoHakDate) ' ���i�P�D���������s��
        End If

        ' ���i�Q
        SetSyouhin2Ctrl(logic, jr.Syouhin2Records)

        ' ���i�R
        SetSyouhin3Ctrl(logic, jr.Syouhin3Records)

        '***********************
        '* ���ǍH��
        '***********************
        ' ����� (�S���Җ�)
        TextHanteisya.Text = cl.GetDisplayString(jr.TantousyaMei)
        ' ������
        TextHanteiSyubetu.Text = cl.GetDisplayString(kisoSiyouLogic.GetHanteiSyunetuDisp(jr.HanteiCd1, jr.HanteiCd2))

        ' ����̐ݒ�
        SetHanteiData(logic, jr.HanteiCd1, jr.HanteiSetuzokuMoji, jr.HanteiCd2)

        If jr.KojGaisyaCd <> "" And jr.KojGaisyaJigyousyoCd <> "" Then '�H����ЃR�[�h�{�H����Ў��Ə��R�[�h
            '�H����Ж�
            TextKoujiGaisya.Text = tyousakaisyaSearchLogic.GetTyousaKaisyaMei(jr.KojGaisyaCd, jr.KojGaisyaJigyousyoCd, False)
        End If

        ' ���ǍH�����
        TextKairyouKoujiSyubetu.Text = cl.GetDisplayString(jr.KairyKojSyubetuMei)
        ' ���ǍH����
        TextKairyouKoujiDate.Text = cl.GetDisplayString(jr.KairyKojDate)

        If Not jr.KairyouKoujiRecord Is Nothing Then
            ' (�H��)���������s��
            TextKoujiSeikyusyoHakkouDate.Text = _
               cl.GetDisplayString(jr.KairyouKoujiRecord.SeikyuusyoHakDate)
        End If
        ' �H���񍐏���
        TextKoujiHoukokusyoJuri.Text = cl.GetDisplayString(jr.HkksJyuriJyky)
        ' �H���񍐏��󗝓�
        TextKoujiHoukokusyoJuriDate.Text = cl.GetDisplayString(jr.KojHkksJuriDate)
        ' �H���񍐏�������
        TextKoujiHoukokusyoHassouDate.Text = cl.GetDisplayString(jr.KojHkksHassouDate)
        ' �H�����������v���z 
        TextKoujiHattyuusyoGoukeiKingaku.Text = IIf(jr.GetKoujiHattyusyoKingaku() = 0, _
                                                        "0", _
                                                        cl.GetDisplayString(jr.GetKoujiHattyusyoKingaku(), "0") _
                                                        )

        ' �H�����v�����z(�ō�)
        TextKoujiGoukeiNyuukingaku.Text = IIf(jr.getNyuukinGaku("130") + jr.getNyuukinGaku("140") = 0, _
                                                    "0", _
                                                    cl.GetDisplayString(jr.getNyuukinGaku("130") + jr.getNyuukinGaku("140"), "0") _
                                                    )

        ' �c�z
        Dim KoujiZangaku As Integer
        Dim kojZangaku As Integer
        Dim tKojZangaku As Integer

        ' �H����А����̏ꍇ�A�H������c�z�i�ō�������z - �����z�j�͂˂�0�~
        If jr.KojGaisyaSeikyuuUmu = 1 Then
            kojZangaku = 0
        Else
            kojZangaku = jr.getZeikomiGaku(New String() {"130"}) - jr.getNyuukinGaku("130")
        End If
        ' �ǉ��H����А����̏ꍇ�A�H������c�z�i�ō�������z - �����z�j�͂˂�0�~
        If jr.TKojKaisyaSeikyuuUmu = 1 Then
            tKojZangaku = 0
        Else
            tKojZangaku = jr.getZeikomiGaku(New String() {"140"}) - jr.getNyuukinGaku("140")
        End If
        KoujiZangaku = kojZangaku + tKojZangaku
        TextKoujiZangaku.Text = KoujiZangaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        If Not jr.KairyouKoujiRecord Is Nothing Then
            ' �H�����i�D�R�[�h
            TextKoujiSyouhinCd.Text = cl.GetDisplayString(jr.KairyouKoujiRecord.SyouhinCd)
            ' �H�����i�D����
            TextKoujiSyouhinMei.Text = cl.GetDisplayString(jr.KairyouKoujiRecord.SyouhinMei)
        End If

        If jr.KojGaisyaSeikyuuUmu = 1 Then
            ' �H�����i�D�H����А���
            SpanKoujiGaisyaSeikyuu.InnerHtml = _
                cl.GetDisplayString(EarthConst.KOUJIGAISYA_SEIKYUU)
        End If

        If Not jr.TuikaKoujiRecord Is Nothing Then
            ' �ǉ��H�����i.�R�[�h
            TextTuikaKoujiSyouhinCd.Text = cl.GetDisplayString(jr.TuikaKoujiRecord.SyouhinCd)
            ' �ǉ��H�����i.����
            TextTuikaKoujiSyouhinMei.Text = cl.GetDisplayString(jr.TuikaKoujiRecord.SyouhinMei)
        End If

        If jr.TKojKaisyaSeikyuuUmu = 1 Then
            ' �ǉ��H�����i�D�H����А���
            SpanTuikaKoujiKaisyaSeikyuu.InnerHtml = _
                cl.GetDisplayString(EarthConst.KOUJIGAISYA_SEIKYUU)
        End If

        '***********************
        '* �Ĕ��s(�n�Ճe�[�u��)
        '***********************
        ' �Ĕ��s��
        TextSaihakkouDate.Text = cl.GetDisplayString(jr.HosyousyoSaihakDate)

        '***********************
        '* �Ĕ��s(�@�ʐ����e�[�u��)
        '***********************
        If Not jr.HosyousyoRecord Is Nothing Then

            '�@�ʐ��������R���g���[���ɃZ�b�g
            SetCtrlTeibetuSeikyuuDataSh(jr.HosyousyoRecord)

            '�@�ʓ��������R���g���[���ɃZ�b�g
            If jr.TeibetuNyuukinRecords IsNot Nothing Then
                ' �����z/�c�z���Z�b�g
                CalcZangakuSh(jr.getZeikomiGaku(New String() {"170"}), jr.getNyuukinGaku("170"))
            Else
                ' �����z/�c�z���Z�b�g
                SetKingaku(EnumKingakuType.Saihakkou, True)
            End If

        End If

        '***********************
        '* ��񕥖�(�n�Ճe�[�u��)
        '***********************
        ' ��񕥖߁D�ԋ�������
        SpanKyHenkinSyorizumi.InnerHtml = _
            cl.GetDisplayString(IIf(jr.HenkinSyoriFlg = 1, EarthConst.HENKIN_SYORI_ZUMI, ""))
        ' ��񕥖߁D�ԋ�������
        SpanKyHenkinSyoriDate.InnerHtml = _
            cl.GetDisplayString(jr.HenkinSyoriDate)

        '***********************
        '* ��񕥖�(�@�ʐ����e�[�u��)
        '***********************
        If Not jr.KaiyakuHaraimodosiRecord Is Nothing Then

            '�@�ʐ��������R���g���[���ɃZ�b�g
            SetCtrlTeibetuSeikyuuDataKy(jr.KaiyakuHaraimodosiRecord)

        End If

        '****************************
        '* Hidden����
        '****************************
        '�ۏ؏����s���̉�ʐ���p
        '�����������m�F���`�F�b�N
        SetTyousaHattyuusyoKakuninDateFlg(jr)

        '�H���������m�F���`�F�b�N
        SetKoujiHattyuusyoKakuninDateFlg(jr)

        '�ۏ؏��i�L��
        Me.HiddenHosyouSyouhinUmu.Value = cl.GetDisplayString(jr.HosyouSyouhinUmu)

        '****************************
        '* Hidden����(�R���g���[���̒l�ύX�O)
        '****************************
        '�ۏ؏����s��
        HiddenHosyousyoHakkouDateMae.Value = TextHosyousyoHakkouDate.Text
        '�Ĕ��s��
        HiddenSaihakkouDateMae.Value = TextSaihakkouDate.Text
        '�Ĕ��s��Old
        Me.HiddenSaihakkouDateOld.Value = Me.TextSaihakkouDate.Text
        '�����L��
        HiddenShSeikyuuUmuMae.Value = SelectShSeikyuuUmu.SelectedValue
        '��񕥖ߐ\���L��
        HiddenKyKaiyakuHaraimodosiSinseiUmuMae.Value = SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue
        '�ۏ؏����s��
        HiddenHosyousyoHakJyKyMae.Value = Me.SelectHosyousyoHakkouJyoukyou.SelectedValue
        '�ۏ؊���Old
        Me.HiddenHosyouKikanOld.Value = Me.TextHosyouKikan.Text
        ' ���s�˗���t����(�n��)
        Me.HiddenHakIraiUkeDatetimeOld.Value = IIf(cl.GetDisplayString(jr.HakIraiUkeDatetime) = "", "", Format(jr.HakIraiUkeDatetime, EarthConst.FORMAT_DATE_TIME_1))
        ' ���s�˗��L�����Z������(�n��)
        Me.HiddenHakIraiCanDatetimeOld.Value = IIf(cl.GetDisplayString(jr.HakIraiCanDatetime) = "", "", Format(jr.HakIraiCanDatetime, EarthConst.FORMAT_DATE_TIME_1))

        '****************************
        '* Hidden����(�o�^���m�F)
        '****************************
        '�������{��
        HiddenTyousaJissiDateOld.Value = TextTyousaJissiDate.Text
        '������ЃR�[�h�E������Ў��Ə��R�[�h
        HiddenDefaultSiireSakiCdForLink.Value = (jr.TysKaisyaCd + jr.TysKaisyaJigyousyoCd)

        '****************************
        '* �Z�b�V�����ɉ�ʏ����i�[
        '****************************
        jSM.Ctrl2Hash(Me, jBn.IraiData)
        iraiSession.Irai1Data = jBn.IraiData

        '���ۏ؏����s�󋵂ɂ��ۏ؏��i�L���̕\���ؑ�
        Me.ChgDispHosyouUmu()

    End Sub

#Region "�ۏ؏��Ǘ��f�[�^���R���g���[���ɐݒ肷��"
    ''' <summary>
    ''' �n�Ճ��R�[�h�ƕۏ؏��Ǘ����R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="jr">�n�Ճ��R�[�h</param>
    ''' <param name="hr">�ۏ؏��Ǘ����R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromHosyouRec(ByVal jr As JibanRecordBase, ByVal hr As HosyousyoKanriRecord)
        ' �R���{�ݒ�w���p�[�N���X�𐶐�
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        Dim strBukkenJyky As String = cl.ChkHosyousyoBukkenJyky(jr)

        ' ������
        SetBukkenJyky(strBukkenJyky)

        ' �ۏ؏��^�C�v
        ' ���_�~�[�R���{�ɃZ�b�g
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.HOSYOUSYO_TYPE, True, False)
        objDrpTmp.SelectedValue = cl.ChgStrToInt(hr.HosyousyoType)
        TextHosyousyoType.Text = objDrpTmp.SelectedItem.Text

        '�ی����
        ' ���_�~�[�R���{�ɃZ�b�g
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.HokenKaisya, True, False)
        objDrpTmp.SelectedValue = cl.ChgStrToInt(hr.HokenKaisya)
        SpanHkHokenGaisya.InnerText = objDrpTmp.SelectedItem.Text

        ' ���n���O�ی�
        ' ���_�~�[�R���{�ɃZ�b�g
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.HW_HKN, True, False)
        objDrpTmp.SelectedValue = cl.ChgStrToInt(hr.HwMaeHkn)
        TextHwMaeHkn.Text = objDrpTmp.SelectedItem.Text

        ' ���n����ی�
        ' ���_�~�[�R���{�ɃZ�b�g
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.HW_HKN, True, False)
        objDrpTmp.SelectedValue = cl.ChgStrToInt(hr.HwAtoHkn)
        TextHwAtohkn.Text = objDrpTmp.SelectedItem.Text

        ' �X�V����
        HiddenHkUpdDatetime.Value = IIf(hr.UpdDateTime = DateTime.MinValue, Format(hr.AddDateTime, EarthConst.FORMAT_DATE_TIME_1), Format(hr.UpdDateTime, EarthConst.FORMAT_DATE_TIME_1))
        ' �Ɩ�������
        TextGyoumuKanryouDate.Text = cl.GetDisplayString(hr.GyoumuKanryDate)
        ' �Ɩ��J�n���e
        TextGyoumuKaisiNaiyou.Text = cl.GetDisplayString(hr.GyoumuKaisiNaiyou)

    End Sub

    ''' <summary>
    ''' �����󋵂�ݒ�
    ''' </summary>
    ''' <param name="strBukkenJyky">������</param>
    ''' <remarks></remarks>
    Public Sub SetBukkenJyky(ByVal strBukkenJyky As String)
        ' �R���{�ݒ�w���p�[�N���X�𐶐�
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        ' ������
        ' ���_�~�[�R���{�ɃZ�b�g
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.BUKKEN_JYKY, True, False)
        objDrpTmp.SelectedValue = strBukkenJyky
        SpanBukkenJyky.InnerText = objDrpTmp.SelectedItem.Text

        '�X�^�C���ύX
        If strBukkenJyky = "0" OrElse strBukkenJyky = "2" Then
            Me.SpanBukkenJyky.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
            Me.SpanBukkenJyky.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
        Else '������
            Me.SpanBukkenJyky.Style(EarthConst.STYLE_FONT_COLOR) = String.Empty
            Me.SpanBukkenJyky.Style(EarthConst.STYLE_FONT_WEIGHT) = String.Empty
        End If
        Me.HiddenBukkenJyky.Value = strBukkenJyky

        ' �ۏؔ��s���̉�ʐ���
        SetEnableControlHhDate(strBukkenJyky)

    End Sub

#End Region

#Region "�@�ʐ����f�[�^���R���g���[���ɃZ�b�g����"

    ''' <summary>
    ''' �ۏ؏��Ĕ��s/�@�ʐ������R�[�h
    ''' </summary>
    ''' <param name="TeibetuRec"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlTeibetuSeikyuuDataSh( _
                                    ByVal TeibetuRec As TeibetuSeikyuuRecord _
                                    )

        ' �Ĕ��s���R
        TextSaihakkouRiyuu.Text = cl.GetDisplayString(TeibetuRec.Bikou)

        ' ���㏈����
        SpanShUriageSyorizumi.InnerHtml = _
            cl.GetDisplayString(IIf(TeibetuRec.UriKeijyouDate = DateTime.MinValue, _
                                   "", _
                                   EarthConst.URIAGE_ZUMI))
        ' ����v���
        Me.HiddenShUriageKeijyouDate.Value = cl.GetDisplayString(TeibetuRec.UriKeijyouDate)
        ' �����L��
        SelectShSeikyuuUmu.SelectedValue = cl.GetDisplayString(TeibetuRec.SeikyuuUmu)
        ' ���i�R�[�h
        TextShSyouhinCd.Text = cl.GetDisplayString(TeibetuRec.SyouhinCd)
        Me.HiddenShSyouhinCdOld.Value = TextShSyouhinCd.Text ' �ύX�O
        ' ���i��
        SpanShSyouhinMei.InnerHtml = cl.GetDisplayString(TeibetuRec.SyouhinMei)
        ' �H���X�����Ŕ����z
        TextShKoumutenSeikyuuKingaku.Text = IIf(TeibetuRec.KoumutenSeikyuuGaku = Integer.MinValue, 0, Format(TeibetuRec.KoumutenSeikyuuGaku, EarthConst.FORMAT_KINGAKU_1))
        ' �ŗ��iHidden�j
        HiddenShZeiritu.Value = TeibetuRec.Zeiritu
        ' �ŋ敪�iHidden�j
        HiddenShZeiKbn.Value = IIf(TeibetuRec.ZeiKbn = "", "", TeibetuRec.ZeiKbn)
        If TeibetuRec.Zeiritu = 0 And TeibetuRec.ZeiKbn = "" Then
            SetShZeiInfo(TeibetuRec.SyouhinCd)
        End If

        '*****************
        '* �y������z�z
        '*****************
        '���������Ŋz(�����)
        TextShSyouhizei.Text = IIf(TeibetuRec.UriageSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.UriageSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '���Ŕ�������z(�������Ŕ����z)
        TextShJituseikyuuKingaku.Text = IIf(TeibetuRec.UriGaku = Integer.MinValue, 0, Format(TeibetuRec.UriGaku, EarthConst.FORMAT_KINGAKU_1))
        '���ō�������z(�ō��z)
        TextShZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiUriGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiUriGaku, EarthConst.FORMAT_KINGAKU_1))

        '*****************
        '* �y�d�����z�z
        '*****************
        '�d�����z
        Me.HiddenShSiireGaku.Value = IIf(TeibetuRec.SiireGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireGaku, EarthConst.FORMAT_KINGAKU_1))
        '���d������Ŋz
        Me.HiddenShSiireSyouhiZei.Value = IIf(TeibetuRec.SiireSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))

        ' ���������s��
        TextShSeikyuusyoHakkouDate.Text = _
            cl.GetDisplayString(TeibetuRec.SeikyuusyoHakDate)
        ' ����N����
        TextShUriageNengappi.Text = cl.GetDisplayString(TeibetuRec.UriDate)
        ' �������m��
        If TeibetuRec.HattyuusyoKakuteiFlg = 1 Then
            TextShHattyuusyoKakutei.Text = EarthConst.KAKUTEI
        ElseIf TeibetuRec.HattyuusyoKakuteiFlg = 0 Then
            TextShHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
        End If
        ' ���������z 
        TextShHattyuusyoKingaku.Text = _
            cl.GetDisplayString(TeibetuRec.HattyuusyoGaku)
        ' �������m�F��
        TextShHattyuusyoKakuninDate.Text = _
            cl.GetDisplayString(TeibetuRec.HattyuusyoKakuninDate)

        '������/�d���惊���N�փZ�b�g
        Me.ucSeikyuuSiireLinkSai.SetSeikyuuSiireLinkFromTeibetuRec(TeibetuRec)

        '������^�C�v�̎擾�ݒ�
        Me.TextShSeikyuusaki.Text = cl.GetSeikyuuSakiTypeStr( _
                                                        TeibetuRec.SyouhinCd _
                                                        , EarthEnum.EnumSyouhinKubun.Hosyousyo _
                                                        , Me.HiddenKameitenCd.Value _
                                                        , TeibetuRec.SeikyuuSakiCd _
                                                        , TeibetuRec.SeikyuuSakiBrc _
                                                        , TeibetuRec.SeikyuuSakiKbn)

        '�X�V���� �ǂݍ��ݎ��̃^�C���X�^���v(�r������p)
        cl.setDispHaitaUpdTime(TeibetuRec.AddDatetime, TeibetuRec.UpdDatetime, Me.HiddenShUpdDateTime)

    End Sub

    ''' <summary>
    ''' ��񕥖�/�@�ʐ������R�[�h
    ''' </summary>
    ''' <param name="TeibetuRec"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlTeibetuSeikyuuDataKy( _
                                    ByVal TeibetuRec As TeibetuSeikyuuRecord _
                                    )

        ' ���㏈����
        SpanKyKaiyakuUriageSyorizumi.InnerHtml = _
            cl.GetDisplayString(IIf(TeibetuRec.UriKeijyouDate = DateTime.MinValue, _
                                   "", _
                                   EarthConst.URIAGE_ZUMI))
        ' ����v���
        Me.HiddenKyKaiyakuUriageKeijyouDate.Value = cl.GetDisplayString(TeibetuRec.UriKeijyouDate)

        ' ��񕥖ߐ\���L��
        SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = _
            cl.GetDisplayString(TeibetuRec.SeikyuuUmu)
        ' ���i�R�[�h
        TextKySyouhinCd.Text = _
            cl.GetDisplayString(TeibetuRec.SyouhinCd)
        ' �H���X�����Ŕ����z
        TextKyKoumutenSeikyuuKingaku.Text = IIf(TeibetuRec.KoumutenSeikyuuGaku = Integer.MinValue, 0, Format(TeibetuRec.KoumutenSeikyuuGaku, EarthConst.FORMAT_KINGAKU_1))
        ' �ŗ��iHidden�j
        HiddenKyZeiritu.Value = TeibetuRec.Zeiritu
        ' �ŋ敪�iHidden�j
        HiddenKyZeiKbn.Value = IIf(TeibetuRec.ZeiKbn = "", "", TeibetuRec.ZeiKbn)
        If TeibetuRec.Zeiritu = 0 And TeibetuRec.ZeiKbn = "" Then
            SetKyZeiInfo(TeibetuRec.SyouhinCd)
        End If

        '*****************
        '* �y������z�z
        '*****************
        '���������Ŋz(�����)
        TextKySyouhizei.Text = IIf(TeibetuRec.UriageSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.UriageSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '���Ŕ�������z(�������Ŕ����z)
        TextKyJituseikyuuKingaku.Text = IIf(TeibetuRec.UriGaku = Integer.MinValue, 0, Format(TeibetuRec.UriGaku, EarthConst.FORMAT_KINGAKU_1))
        '���ō�������z(�ō��z)
        TextKyZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiUriGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiUriGaku, EarthConst.FORMAT_KINGAKU_1))

        '*****************
        '* �y�d�����z�z
        '*****************
        '�d�����z
        Me.HiddenKySiireGaku.Value = IIf(TeibetuRec.SiireGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireGaku, EarthConst.FORMAT_KINGAKU_1))
        '���d������Ŋz
        Me.HiddenKySiireSyouhiZei.Value = IIf(TeibetuRec.SiireSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))

        ' ���������s��
        TextKySeikyuusyoHakkouDate.Text = _
            cl.GetDisplayString(TeibetuRec.SeikyuusyoHakDate)
        ' ����N����
        TextKyUriageNengappi.Text = _
            cl.GetDisplayString(TeibetuRec.UriDate)
        ' �������m��
        If TeibetuRec.HattyuusyoKakuteiFlg = 1 Then
            TextKyHattyuusyoKakutei.Text = EarthConst.KAKUTEI
        ElseIf TeibetuRec.HattyuusyoKakuteiFlg = 0 Then
            TextKyHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
        End If
        ' ���������z 
        TextKyHattyuusyoKingaku.Text = _
            cl.GetDisplayString(TeibetuRec.HattyuusyoGaku)
        ' �������m�F��
        TextKyHattyuusyoKakuninDate.Text = _
            cl.GetDisplayString(TeibetuRec.HattyuusyoKakuninDate)

        '������/�d���惊���N�փZ�b�g
        Me.ucSeikyuuSiireLinkKai.SetSeikyuuSiireLinkFromTeibetuRec(TeibetuRec)

        '������^�C�v�̎擾�ݒ�
        Me.TextKySeikyuusaki.Text = cl.GetSeikyuuSakiTypeStr( _
                                                        TeibetuRec.SyouhinCd _
                                                        , EarthEnum.EnumSyouhinKubun.Kaiyaku _
                                                        , Me.HiddenKameitenCd.Value _
                                                        , TeibetuRec.SeikyuuSakiCd _
                                                        , TeibetuRec.SeikyuuSakiBrc _
                                                        , TeibetuRec.SeikyuuSakiKbn)

        '�X�V���� �ǂݍ��ݎ��̃^�C���X�^���v(�r������p)
        cl.setDispHaitaUpdTime(TeibetuRec.AddDatetime, TeibetuRec.UpdDatetime, Me.HiddenKyUpdDateTime)

    End Sub

#End Region

#Region "���i�Q�ݒ�"
    ''' <summary>
    ''' ���i�Q���R�[�h�̏����R���g���[���ɐݒ肷��
    ''' </summary>
    ''' <param name="logic">�ۏ؃��W�b�N�N���X</param>
    ''' <param name="records">���i�Q���R�[�h</param>
    ''' <remarks></remarks>
    Private Sub SetSyouhin2Ctrl(ByVal logic As HosyouLogic, _
                                ByVal records As Dictionary(Of Integer, TeibetuSeikyuuRecord))

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetSyouhin2Ctrl", _
                                            logic, _
                                            records)

        If records Is Nothing Then
            TrSyouhin21.Visible = False ' ���i�Q_�P
            TrSyouhin22.Visible = False ' ���i�Q_�Q
            TrSyouhin23.Visible = False ' ���i�Q_�R
            TrSyouhin24.Visible = False ' ���i�Q_�S
        Else
            If Not records.ContainsKey(1) Then
                TrSyouhin21.Visible = False ' ���i�Q_�P
            Else
                TextSyouhin21A.Text = cl.GetDisplayString(records.Item(1).SyouhinCd)     ' ���i�Q_�P�D�R�[�h
                TextSyouhin21B.Text = cl.GetDisplayString(records.Item(1).SyouhinMei)    ' ���i�Q_�P�D����
            End If
            If Not records.ContainsKey(2) Then
                TrSyouhin22.Visible = False ' ���i�Q_�Q
            Else
                TextSyouhin22A.Text = cl.GetDisplayString(records.Item(2).SyouhinCd)     ' ���i�Q_�Q�D�R�[�h
                TextSyouhin22B.Text = cl.GetDisplayString(records.Item(2).SyouhinMei)    ' ���i�Q_�Q�D����
            End If
            If Not records.ContainsKey(3) Then
                TrSyouhin23.Visible = False ' ���i�Q_�R
            Else
                TextSyouhin23A.Text = cl.GetDisplayString(records.Item(3).SyouhinCd)     ' ���i�Q_�R�D�R�[�h
                TextSyouhin23B.Text = cl.GetDisplayString(records.Item(3).SyouhinMei)    ' ���i�Q_�R�D����
            End If
            If Not records.ContainsKey(4) Then
                TrSyouhin24.Visible = False ' ���i�Q_�S
            Else
                TextSyouhin24A.Text = cl.GetDisplayString(records.Item(4).SyouhinCd)     ' ���i�Q_�S�D�R�[�h
                TextSyouhin24B.Text = cl.GetDisplayString(records.Item(4).SyouhinMei)    ' ���i�Q_�S�D����
            End If
        End If
    End Sub
#End Region

#Region "���i�R�ݒ�"
    ''' <summary>
    ''' ���i�R���R�[�h�̏����R���g���[���ɐݒ肷��
    ''' </summary>
    ''' <param name="logic">�ۏ؃��W�b�N�N���X</param>
    ''' <param name="records">���i�R���R�[�h</param>
    ''' <remarks></remarks>
    Private Sub SetSyouhin3Ctrl(ByVal logic As HosyouLogic, _
                                ByVal records As Dictionary(Of Integer, TeibetuSeikyuuRecord))

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetSyouhin3Ctrl", _
                                            logic, _
                                            records)

        If records Is Nothing Then
            TrSyouhin31.Visible = False ' ���i�R_�P
            TrSyouhin32.Visible = False ' ���i�R_�Q
            TrSyouhin33.Visible = False ' ���i�R_�R
            TrSyouhin34.Visible = False ' ���i�R_�S
            TrSyouhin35.Visible = False ' ���i�R_�T
            TrSyouhin36.Visible = False ' ���i�R_�U
            TrSyouhin37.Visible = False ' ���i�R_�V
            TrSyouhin38.Visible = False ' ���i�R_�W
            TrSyouhin39.Visible = False ' ���i�R_�X
        Else
            If Not records.ContainsKey(1) Then
                TrSyouhin31.Visible = False ' ���i�R_�P
            Else
                TextSyouhin31A.Text = cl.GetDisplayString(records.Item(1).SyouhinCd)     ' ���i�R_�P�D�R�[�h
                TextSyouhin31B.Text = cl.GetDisplayString(records.Item(1).SyouhinMei)    ' ���i�R_�P�D����
            End If
            If Not records.ContainsKey(2) Then
                TrSyouhin32.Visible = False ' ���i�R_�Q
            Else
                TextSyouhin32A.Text = cl.GetDisplayString(records.Item(2).SyouhinCd)     ' ���i�R_�Q�D�R�[�h
                TextSyouhin32B.Text = cl.GetDisplayString(records.Item(2).SyouhinMei)    ' ���i�R_�Q�D����
            End If
            If Not records.ContainsKey(3) Then
                TrSyouhin33.Visible = False ' ���i�R_�R
            Else
                TextSyouhin33A.Text = cl.GetDisplayString(records.Item(3).SyouhinCd)     ' ���i�R_�R�D�R�[�h
                TextSyouhin33B.Text = cl.GetDisplayString(records.Item(3).SyouhinMei)    ' ���i�R_�R�D����
            End If
            If Not records.ContainsKey(4) Then
                TrSyouhin34.Visible = False ' ���i�R_�S
            Else
                TextSyouhin34A.Text = cl.GetDisplayString(records.Item(4).SyouhinCd)     ' ���i�R_�S�D�R�[�h
                TextSyouhin34B.Text = cl.GetDisplayString(records.Item(4).SyouhinMei)    ' ���i�R_�S�D����
            End If
            If Not records.ContainsKey(5) Then
                TrSyouhin35.Visible = False ' ���i�R_�T
            Else
                TextSyouhin35A.Text = cl.GetDisplayString(records.Item(5).SyouhinCd)     ' ���i�R_�T�D�R�[�h
                TextSyouhin35B.Text = cl.GetDisplayString(records.Item(5).SyouhinMei)    ' ���i�R_�T�D����
            End If
            If Not records.ContainsKey(6) Then
                TrSyouhin36.Visible = False ' ���i�R_�U
            Else
                TextSyouhin36A.Text = cl.GetDisplayString(records.Item(6).SyouhinCd)     ' ���i�R_�U�D�R�[�h
                TextSyouhin36B.Text = cl.GetDisplayString(records.Item(6).SyouhinMei)    ' ���i�R_�U�D����
            End If
            If Not records.ContainsKey(7) Then
                TrSyouhin37.Visible = False ' ���i�R_�V
            Else
                TextSyouhin37A.Text = cl.GetDisplayString(records.Item(7).SyouhinCd)     ' ���i�R_�V�D�R�[�h
                TextSyouhin37B.Text = cl.GetDisplayString(records.Item(7).SyouhinMei)    ' ���i�R_�V�D����
            End If
            If Not records.ContainsKey(8) Then
                TrSyouhin38.Visible = False ' ���i�R_�W
            Else
                TextSyouhin38A.Text = cl.GetDisplayString(records.Item(8).SyouhinCd)     ' ���i�R_�W�D�R�[�h
                TextSyouhin38B.Text = cl.GetDisplayString(records.Item(8).SyouhinMei)    ' ���i�R_�W�D����
            End If
            If Not records.ContainsKey(9) Then
                TrSyouhin39.Visible = False ' ���i�R_�X
            Else
                TextSyouhin39A.Text = cl.GetDisplayString(records.Item(9).SyouhinCd)     ' ���i�R_�X�D�R�[�h
                TextSyouhin39B.Text = cl.GetDisplayString(records.Item(9).SyouhinMei)    ' ���i�R_�X�D����
            End If
        End If
    End Sub
#End Region

#Region "�ۏ؏����s���̉�ʐ���"

    ''' <summary>
    ''' �ۏ؏����s���̉�ʐ���/�����������m�F��
    ''' </summary>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <remarks>�����i1�`3���R�[�h���`�F�b�N</remarks>
    Private Sub SetTyousaHattyuusyoKakuninDateFlg(ByVal jibanRec As JibanRecordBase)
        Dim strHattyuusyoKakuninDate As String = "" '�������m�F��

        If jibanRec Is Nothing Then Exit Sub

        Dim blnFlg As Boolean = True

        '�����������m�F���t���O
        HiddenTyousaHattyuusyoKakuninDateFlg.Value = "" '������

        With jibanRec

            '���i�P
            If Not .Syouhin1Record Is Nothing Then
                If .Syouhin1Record.SeikyuuUmu = 1 Then
                    '�������m�F��
                    strHattyuusyoKakuninDate = cl.GetDisplayString(.Syouhin1Record.HattyuusyoKakuninDate)
                    If strHattyuusyoKakuninDate = String.Empty Then '������
                        blnFlg = False
                        Exit Sub '�����𔲂���
                    End If
                End If
            End If

            '���i�Q
            If Not .Syouhin2Records Is Nothing Then
                For Each de As KeyValuePair(Of Integer, TeibetuSeikyuuRecord) In jibanRec.Syouhin2Records
                    If Not .Syouhin2Records(de.Key) Is Nothing Then
                        If .Syouhin2Records(de.Key).SeikyuuUmu = 1 Then
                            '�������m�F��
                            strHattyuusyoKakuninDate = cl.GetDisplayString(.Syouhin2Records(de.Key).HattyuusyoKakuninDate)
                            If strHattyuusyoKakuninDate = String.Empty Then '������
                                blnFlg = False
                                Exit Sub '�����𔲂���
                            End If
                        End If
                    End If
                Next
            End If

            '���i�R
            If Not .Syouhin3Records Is Nothing Then
                For Each de As KeyValuePair(Of Integer, TeibetuSeikyuuRecord) In jibanRec.Syouhin3Records
                    If Not .Syouhin3Records(de.Key) Is Nothing Then
                        If .Syouhin3Records(de.Key).SeikyuuUmu = 1 Then
                            '�������m�F��
                            strHattyuusyoKakuninDate = cl.GetDisplayString(.Syouhin3Records(de.Key).HattyuusyoKakuninDate)
                            If strHattyuusyoKakuninDate = String.Empty Then '������
                                blnFlg = False
                                Exit Sub '�����𔲂���
                            End If
                        End If
                    End If
                Next
            End If

            If blnFlg Then
                HiddenTyousaHattyuusyoKakuninDateFlg.Value = "1" '�����������m�F���t���O
            End If

        End With

    End Sub

    ''' <summary>
    ''' �ۏ؏����s���̉�ʐ���/�H���������m�F��
    ''' </summary>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <remarks>�����ǍH�����R�[�h�ƒǉ��H�����R�[�h���`�F�b�N</remarks>
    Private Sub SetKoujiHattyuusyoKakuninDateFlg(ByVal jibanRec As JibanRecordBase)
        Dim strHattyuusyoKakuninDate As String = "" '�������m�F��

        If jibanRec Is Nothing Then Exit Sub

        Dim blnFlg As Boolean = True

        '�H���������m�F���t���O
        HiddenKoujiHattyuusyoKakuninDateFlg.Value = "" '������

        With jibanRec

            '���ǍH��
            If Not .KairyouKoujiRecord Is Nothing Then
                If .KairyouKoujiRecord.SeikyuuUmu = 1 Then
                    '�������m�F��
                    strHattyuusyoKakuninDate = cl.GetDisplayString(.KairyouKoujiRecord.HattyuusyoKakuninDate)
                    If strHattyuusyoKakuninDate = String.Empty Then '������
                        blnFlg = False
                        Exit Sub '�����𔲂���
                    End If
                End If
            End If

            '�ǉ��H��
            If Not .TuikaKoujiRecord Is Nothing Then
                If .TuikaKoujiRecord.SeikyuuUmu = 1 Then
                    '�������m�F��
                    strHattyuusyoKakuninDate = cl.GetDisplayString(.TuikaKoujiRecord.HattyuusyoKakuninDate)
                    If strHattyuusyoKakuninDate = String.Empty Then '������
                        blnFlg = False
                        Exit Sub '�����𔲂���
                    End If
                End If
            End If

            If blnFlg Then
                HiddenKoujiHattyuusyoKakuninDateFlg.Value = "1" '�H���������m�F���t���O
            End If

        End With

    End Sub

#End Region

#Region "������e�̐ݒ�"
    ''' <summary>
    ''' ������e�̐ݒ�
    ''' </summary>
    ''' <param name="logic"></param>
    ''' <param name="intHantei1Cd">����P</param>
    ''' <param name="intHanteiSetuzokuMoji">����ڑ���</param>
    ''' <param name="intHantei2Cd">����Q</param>
    ''' <remarks></remarks>
    Private Sub SetHanteiData(ByVal logic As HosyouLogic, _
                              ByVal intHantei1Cd As Integer, _
                              ByVal intHanteiSetuzokuMoji As Integer, _
                              ByVal intHantei2Cd As Integer)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetHanteiData", _
                                            logic, _
                                            intHantei1Cd, _
                                            intHanteiSetuzokuMoji, _
                                            intHantei2Cd)

        ' ��b�d�l���W�b�N�N���X
        Dim kisoSiyouLogic As New KisoSiyouLogic()

        SpanHantei1.InnerHtml = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(intHantei1Cd)) '����P��

        SpanHanteiSetuzokuMoji.InnerHtml = cl.GetDisplayString(kisoSiyouLogic.GetKisoSiyouSetuzokusiMei(intHanteiSetuzokuMoji)) ' ����ڑ���

        SpanHantei2.InnerHtml = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(intHantei2Cd)) '����Q��

        '*********************
        '* Hidden����
        '*********************
        HiddenHantei1CdOld.Value = intHantei1Cd

    End Sub
#End Region

#End Region

#Region "�i���f�[�^���R���g���[���ɐݒ肷��"
    ''' <summary>
    ''' �i���f�[�^�����ʕ\�����ڂւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="jr">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromReportRec(ByRef jr As JibanRecordBase, ByRef rr As ReportIfGetRecord)

        '***********************
        '* �t���O
        '***********************
        Me.HiddenHakIraiUketukeFlg.Value = ""
        Me.HiddenHakIraiCancelFlg.Value = ""

        '***********************
        '* ���s�˗����
        '***********************
        TextHakIraiTime.Text = IIf(cl.GetDisplayString(rr.HakIraiTime) = "", "", Format(rr.HakIraiTime, EarthConst.FORMAT_DATE_TIME_7) + " �˗�")
        ' ��������
        TextbukkenuMei.Text = rr.HakIraiBknName
        ' �������ݒn�P/�Q/�R
        TextBukkenuSyozai1.Text = rr.HakIraiBknAdr1
        TextBukkenuSyozai2.Text = rr.HakIraiBknAdr2
        TextBukkenuSyozai3.Text = rr.HakIraiBknAdr3
        ' �Z�b�g���s��
        TextSetHakkouDate.Text = cl.GetDisplayString(Now.Date)
        ' �����n����
        TextHikiwatasiDate.Text = cl.GetDisplayString(rr.HakIraiHwDate)
        ' �S����
        TextTantouSya.Text = rr.HakIraiTanto
        ' �A����
        TextRenrakuSaki.Text = rr.HakIraiTantoTel
        ' ����ID
        TextNyuuryokuID.Text = rr.HakIraiLogin
        ' ���s�˗����̑����
        TextIraiSonota.Text = rr.HakIraiSonota

        Me.HiddenHakIraiTime.Value = cbLogic.GetDispStrDateTime(rr.HakIraiTime)
        Me.HiddenHakIraiUkeDatetimeR.Value = cbLogic.GetDispStrDateTime(rr.HakIraiUkeDatetime)
        Me.HiddenHakIraiCanDatetimeR.Value = cbLogic.GetDispStrDateTime(rr.HakIraiCanDatetime)

    End Sub

#End Region


#Region "�R���g���[���̓��e��n�Ճ��R�[�h�ɃZ�b�g���擾����"
    ''' <summary>
    ''' ��ʂ̓��e��n�Ճ��R�[�h�ɃZ�b�g���擾����
    ''' </summary>
    ''' <returns>�R���g���[���̓��e���Z�b�g�����n�Ճ��R�[�h</returns>
    ''' <remarks></remarks>
    Protected Function GetCtrlDataRecord() As JibanRecordHosyou
        Dim JibanLogic As New JibanLogic
        Dim jr As New JibanRecord
        ' ���݂̒n�Ճf�[�^��DB����擾����
        jr = JibanLogic.GetJibanData(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou)

        Dim jibanRec As New JibanRecordHosyou

        '�i��T�X�V�p�ɁADB��̒l���Z�b�g����
        JibanLogic.SetSintyokuJibanData(jr, jibanRec)

        '���i1�`3�̃R�s�[
        JibanLogic.ps_CopyTeibetuSyouhinData(jr, jibanRec)

        '***********************************************
        ' �n�Ճe�[�u�� (���ʏ��)
        '***********************************************
        ' �f�[�^�j�����
        cl.SetDisplayString(ucGyoumuKyoutuu.DataHakiSyubetu, jibanRec.DataHakiSyubetu)
        ' �f�[�^�j����
        If ucGyoumuKyoutuu.DataHakiSyubetu = "0" Then
            jibanRec.DataHakiDate = DateTime.MinValue
        Else
            cl.SetDisplayString(ucGyoumuKyoutuu.DataHakiDate, jibanRec.DataHakiDate)
        End If

        ' �敪
        jibanRec.Kbn = ucGyoumuKyoutuu.Kubun
        ' �ԍ��i�ۏ؏�NO�j
        jibanRec.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        ' �{�喼    
        jibanRec.SesyuMei = ucGyoumuKyoutuu.SesyuMei
        ' �����Z��1,2,3
        jibanRec.BukkenJyuusyo1 = ucGyoumuKyoutuu.Jyuusyo1
        jibanRec.BukkenJyuusyo2 = ucGyoumuKyoutuu.Jyuusyo2
        jibanRec.BukkenJyuusyo3 = ucGyoumuKyoutuu.Jyuusyo3
        ' ���l,���l2
        jibanRec.Bikou = ucGyoumuKyoutuu.Bikou
        jibanRec.Bikou2 = ucGyoumuKyoutuu.Bikou2
        '�������@�R�[�h
        jibanRec.TysHouhou = ucGyoumuKyoutuu.TyousaHouhouCd
        '�������@��
        jibanRec.TysHouhouMeiIf = ucGyoumuKyoutuu.TyousaHouhouMei
        '������ЃR�[�h
        jibanRec.TysKaisyaCd = ucGyoumuKyoutuu.TyousaKaishaCd
        '������Ў��Ə��R�[�h
        jibanRec.TysKaisyaJigyousyoCd = ucGyoumuKyoutuu.TyousaKaishaJigyousyoCd
        '������Ж�
        jibanRec.TysKaisyaMeiIf = ucGyoumuKyoutuu.TyousaKaishaMei
        '�����X�R�[�h
        jibanRec.KameitenCd = ucGyoumuKyoutuu.AccKameitenCd.Value
        '�����X��
        jibanRec.KameitenMeiIf = ucGyoumuKyoutuu.KameitenMei
        '�����XTel
        jibanRec.KameitenTelIf = ucGyoumuKyoutuu.KameitenTel
        '�����XFax
        jibanRec.KameitenFaxIf = ucGyoumuKyoutuu.KameitenFax
        '�����XMail
        jibanRec.KameitenMailIf = ucGyoumuKyoutuu.KameitenMail
        '�\����
        jibanRec.KouzouMeiIf = ucGyoumuKyoutuu.KouzouMei

        '***********************************************
        ' �n�Ճe�[�u�� (�ۏ؏��)
        '***********************************************
        ' �_��NO
        cl.SetDisplayString(TextKeiyakuNo.Text, jibanRec.KeiyakuNo)
        ' ��b�񍐏��L��
        cl.SetDisplayString(SelectKisoHoukokusyo.SelectedValue, jibanRec.KsHkksUmu)
        ' ��b�H�������񍐏�����
        cl.SetDisplayString(TextKisoHoukokusyoTyakuDate.Text, jibanRec.KsKojKanryHkksTykDate)
        '�ύX�\������X�R�[�h
        cl.SetDisplayString(TextYoteiKameitenCd.Value, jibanRec.HenkouYoteiKameitenCd)


        '************************************************
        ' �ۏ؏����s�󋵁A�ۏ؏����s�󋵐ݒ���A�ۏ؏��i�L���̎����ݒ�
        '************************************************
        '���i�̎����ݒ��ɍs�Ȃ�
        cbLogic.ps_AutoSetHosyousyoHakJyjy(EarthEnum.emGamenInfo.Hosyou, jibanRec, Me.SelectHosyousyoHakkouJyoukyou.SelectedValue)

        ' �ۏ؏����s��
        cl.SetDisplayString(TextHosyousyoHakkouDate.Text, jibanRec.HosyousyoHakDate)
        ' �t�ۏؖ���FLG
        cl.SetDisplayString(SelectFuhoSyoumeisyoFlg.SelectedValue, jibanRec.FuhoSyoumeisyoFlg)
        ' �ۏؗL��
        cl.SetDisplayString(SelectHosyouUmu.SelectedValue, jibanRec.HosyouUmu)
        ' �ۏ؊J�n��
        cl.SetDisplayString(TextHosyouKaisiDate.Text, jibanRec.HosyouKaisiDate)
        ' �ۏ؊���
        cl.SetDisplayString(TextHosyouKikan.Text, jibanRec.HosyouKikan)
        ' �ۏ؂Ȃ����R�R�[�h
        cl.SetDisplayString(SelectHosyouNasiRiyuu.SelectedValue, jibanRec.HosyouNasiRiyuuCd)
        ' �ۏ؂Ȃ����R
        cl.SetDisplayString(TextHosyouNasiRiyuu.Text, jibanRec.HosyouNasiRiyuu)
        ' �ۏ؏��Ĕ��s��
        cl.SetDisplayString(TextSaihakkouDate.Text, jibanRec.HosyousyoSaihakDate)
        ' �ۏ؏����s�˗����L��
        cl.SetDisplayString(SelectHakkouIraisyo.SelectedValue, jibanRec.HosyousyoHakIraisyoUmu)
        ' �ۏ؏����s�˗�������
        cl.SetDisplayString(TextHakkouIraiTyakuDate.Text, jibanRec.HosyousyoHakIraisyoTykDate)
        ' �Ɩ�������*
        ' �X�V���O�C�����[�U�[ID
        cl.SetDisplayString(userInfo.LoginUserId, jibanRec.UpdLoginUserId)
        ' �X�V����
        If ucGyoumuKyoutuu.AccupdateDateTime.Value = "" Then
            jibanRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
        Else
            jibanRec.UpdDatetime = DateTime.ParseExact(ucGyoumuKyoutuu.AccupdateDateTime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
        End If

        '***********************************************
        ' �@�ʐ����e�[�u�� 
        '***********************************************

        ' �ۏ؏��Ĕ��s
        jibanRec.HosyousyoRecord = GetSaihakkouCtrlData()

        ' ��񕥖�
        jibanRec.KaiyakuHaraimodosiRecord = GetKaiyakuCtrlData()

        '***************************************
        ' ��ʓ��͍��ڈȊO
        '***************************************
        '�X�V��
        jibanRec.Kousinsya = cbLogic.GetKousinsya(userInfo.LoginUserId, DateTime.Now)

        ' ���s�˗���t����
        If HiddenHakIraiUketukeFlg.Value = "1" Then
            jibanRec.HakIraiUkeDatetime = Date.Now
        Else
            If HiddenHakIraiUkeDatetimeOld.Value <> "" Then
                jibanRec.HakIraiUkeDatetime = DateTime.ParseExact(Me.HiddenHakIraiUkeDatetimeOld.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
            End If
        End If

        Return jibanRec

    End Function

#Region "�@�ʐ������R�[�h�쐬"

#Region "�@�ʐ������R�[�h�쐬�i�Ĕ��s�j"
    ''' <summary>
    ''' �Ĕ��s�̓@�ʐ������R�[�h���擾���܂�
    ''' </summary>
    ''' <returns>�Ĕ��s�̓@�ʐ������R�[�h</returns>
    ''' <remarks></remarks>
    Protected Function GetSaihakkouCtrlData() As TeibetuSeikyuuRecord
        Dim record As New TeibetuSeikyuuRecord

        ' ���i���ݒ莞�̓��R�[�h����
        If TextShSyouhinCd.Text.Trim() = "" Then
            Return Nothing
        End If

        '** KEY��� *******************
        ' �敪
        cl.SetDisplayString(ucGyoumuKyoutuu.Kubun, record.Kbn)
        ' �ԍ��i�ۏ؏�NO�j
        cl.SetDisplayString(ucGyoumuKyoutuu.Bangou, record.HosyousyoNo)
        ' ���ރR�[�h
        record.BunruiCd = EarthConst.SOUKO_CD_HOSYOUSYO
        ' ��ʕ\��NO
        record.GamenHyoujiNo = 1

        '** ���׏�� *******************
        ' ���i�R�[�h
        cl.SetDisplayString(TextShSyouhinCd.Text, record.SyouhinCd)
        ' ������z
        cl.SetDisplayString(TextShJituseikyuuKingaku.Text, record.UriGaku)
        ' �d�����z
        cl.SetDisplayString(HiddenShSiireGaku.Value, record.SiireGaku)
        ' �d������Ŋz
        cl.SetDisplayString(HiddenShSiireSyouhiZei.Value, record.SiireSyouhiZeiGaku)
        ' �ŋ敪
        cl.SetDisplayString(HiddenShZeiKbn.Value, record.ZeiKbn)
        ' �ŗ�
        cl.SetDisplayString(HiddenShZeiritu.Value, record.Zeiritu)
        ' ����Ŋz
        cl.SetDisplayString(TextShSyouhizei.Text, record.UriageSyouhiZeiGaku)
        ' ���������s��
        cl.SetDisplayString(TextShSeikyuusyoHakkouDate.Text, record.SeikyuusyoHakDate)
        ' ����N����
        cl.SetDisplayString(TextShUriageNengappi.Text, record.UriDate)
        ' �`�[����N����(���W�b�N�N���X�Ŏ����Z�b�g)
        record.DenpyouUriDate = DateTime.MinValue
        ' �����L��
        cl.SetDisplayString(SelectShSeikyuuUmu.SelectedValue, record.SeikyuuUmu)
        ' ����v��FLG (Insert�̂�)
        record.UriKeijyouFlg = 0
        ' ����v���
        record.UriKeijyouDate = Date.MinValue
        ' �m��敪
        record.KakuteiKbn = Integer.MinValue
        ' ���l(�Ĕ��s���R)
        cl.SetDisplayString(TextSaihakkouRiyuu.Text, record.Bikou)
        ' �H���X�����z
        cl.SetDisplayString(TextShKoumutenSeikyuuKingaku.Text, record.KoumutenSeikyuuGaku)
        ' ���������z
        cl.SetDisplayString(TextShHattyuusyoKingaku.Text, record.HattyuusyoGaku)
        ' �������m�F��
        cl.SetDisplayString(TextShHattyuusyoKakuninDate.Text, record.HattyuusyoKakuninDate)
        ' �ꊇ����FLG
        record.IkkatuNyuukinFlg = Integer.MinValue
        ' �������Ϗ��쐬��
        record.TysMitsyoSakuseiDate = Date.MinValue
        ' �������m��FLG
        record.HattyuusyoKakuteiFlg = IIf(TextShHattyuusyoKakutei.Text = EarthConst.KAKUTEI, 1, 0)

        '������/�d���惊���N����擾
        Me.ucSeikyuuSiireLinkSai.SetTeibetuRecFromSeikyuuSiireLink(record)

        ' �X�V���O�C�����[�U�[ID
        cl.SetDisplayString(userInfo.LoginUserId, record.UpdLoginUserId)
        '�X�V����(�r������p)
        record.UpdDatetime = cl.getDispHaitaUpdTime(Me.HiddenShUpdDateTime)

        Return record

    End Function
#End Region

#Region "�@�ʐ������R�[�h�쐬�i��񕥖߁j"
    ''' <summary>
    ''' ��񕥖߂̓@�ʐ������R�[�h���擾���܂�
    ''' </summary>
    ''' <returns>��񕥖߂̓@�ʐ������R�[�h</returns>
    ''' <remarks></remarks>
    Protected Function GetKaiyakuCtrlData() As TeibetuSeikyuuRecord
        Dim record As New TeibetuSeikyuuRecord

        ' ���i���ݒ莞�̓��R�[�h����
        If TextKySyouhinCd.Text.Trim() = "" Then
            Return Nothing
        End If

        '** KEY��� *******************
        ' �敪
        cl.SetDisplayString(ucGyoumuKyoutuu.Kubun, record.Kbn)
        ' �ԍ��i�ۏ؏�NO�j
        cl.SetDisplayString(ucGyoumuKyoutuu.Bangou, record.HosyousyoNo)
        ' ���ރR�[�h
        record.BunruiCd = EarthConst.SOUKO_CD_KAIYAKU_HARAIMODOSI
        ' ��ʕ\��NO
        record.GamenHyoujiNo = 1

        '** ���׏�� *******************
        ' ���i�R�[�h
        cl.SetDisplayString(TextKySyouhinCd.Text, record.SyouhinCd)
        ' ������z
        cl.SetDisplayString(TextKyJituseikyuuKingaku.Text, record.UriGaku)
        ' �d�����z
        cl.SetDisplayString(HiddenKySiireGaku.Value, record.SiireGaku)
        ' �d������Ŋz
        cl.SetDisplayString(HiddenKySiireSyouhiZei.Value, record.SiireSyouhiZeiGaku)
        ' �ŋ敪
        cl.SetDisplayString(HiddenKyZeiKbn.Value, record.ZeiKbn)
        ' �ŗ�
        cl.SetDisplayString(HiddenKyZeiritu.Value, record.Zeiritu)
        ' ����Ŋz
        cl.SetDisplayString(TextKySyouhizei.Text, record.UriageSyouhiZeiGaku)
        ' ���������s��
        cl.SetDisplayString(TextKySeikyuusyoHakkouDate.Text, record.SeikyuusyoHakDate)
        ' ����N������
        cl.SetDisplayString(TextKyUriageNengappi.Text, record.UriDate)
        ' �`�[����N����(���W�b�N�N���X�Ŏ����Z�b�g)
        record.DenpyouUriDate = DateTime.MinValue
        ' �����L��
        cl.SetDisplayString(SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue, record.SeikyuuUmu)
        ' ����v��FLG (Insert�̂�)
        record.UriKeijyouFlg = 0
        ' ����v���
        record.UriKeijyouDate = Date.MinValue
        ' �m��敪
        record.KakuteiKbn = Integer.MinValue
        '���l
        record.Bikou = Nothing
        ' �H���X�����z
        cl.SetDisplayString(TextKyKoumutenSeikyuuKingaku.Text, record.KoumutenSeikyuuGaku)
        ' ���������z
        cl.SetDisplayString(TextKyHattyuusyoKingaku.Text, record.HattyuusyoGaku)
        ' �������m�F��
        cl.SetDisplayString(TextKyHattyuusyoKakuninDate.Text, record.HattyuusyoKakuninDate)
        ' �ꊇ����FLG
        record.IkkatuNyuukinFlg = Integer.MinValue
        ' �������Ϗ��쐬��
        record.TysMitsyoSakuseiDate = Date.MinValue
        ' �������m��FLG
        record.HattyuusyoKakuteiFlg = IIf(TextKyHattyuusyoKakutei.Text = EarthConst.KAKUTEI, 1, 0)

        '������/�d���惊���N����擾
        Me.ucSeikyuuSiireLinkKai.SetTeibetuRecFromSeikyuuSiireLink(record)

        ' �X�V���O�C�����[�U�[ID
        cl.SetDisplayString(userInfo.LoginUserId, record.UpdLoginUserId)
        '�X�V����(�r������p)
        record.UpdDatetime = cl.getDispHaitaUpdTime(Me.HiddenKyUpdDateTime)

        Return record
    End Function
#End Region

#End Region

#End Region
#Region "�i���ۏ؏����s�˗���t"
    ''' <summary>
    ''' �]�L(1) ����(����) > �{�喼�]�L
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonBukkenTenki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TextbukkenuMei.Text <> "" Then
            ucGyoumuKyoutuu.AccSesyuMei.Value = TextbukkenuMei.Text
            Me.UpdatePanelHosyou.Update()
        End If
    End Sub

    ''' <summary>
    ''' �]�L(2) �Z��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonJyuusyoTenki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TextBukkenuSyozai1.Text & TextBukkenuSyozai2.Text & TextBukkenuSyozai3.Text <> "" Then
            ucGyoumuKyoutuu.Jyuusyo1 = TextBukkenuSyozai1.Text
            ucGyoumuKyoutuu.Jyuusyo2 = TextBukkenuSyozai2.Text
            ucGyoumuKyoutuu.Jyuusyo3 = TextBukkenuSyozai3.Text
            Me.UpdatePanelHosyou.Update()
        End If
    End Sub

    ''' <summary>
    ''' �]�L(3) �ۏ؊J�n��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHosyouKaisiDateTenki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' ���n���� �� �ۏ؊J�n��
        TextHosyouKaisiDate.Text = TextHikiwatasiDate.Text
    End Sub
    ''' <summary>
    ''' ���s�˗��L�����Z��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHakkouCancel_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim JibanLogic As New JibanLogic
        Dim jr As New JibanRecord
        Dim jibanRec As New JibanRecordHosyou

        ' ���݂̒n�Ճf�[�^��DB����擾����
        jr = JibanLogic.GetJibanData(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou)
        ' ��ʓ��e���n�Ճ��R�[�h�𐶐�����
        jibanRec = Me.GetCtrlDataRecord()

        ' �i��T�X�V�p�ɁADB��̒l���Z�b�g����
        JibanLogic.SetSintyokuJibanData(jr, jibanRec)

        ' 2. �n�Ճe�[�u��.���s�˗��L�����Z�������ɃV�X�e���������Z�b�g
        jibanRec.HakIraiCanDatetime = Date.Now

        ' 3. �o�^���������i�n�Ճe�[�u��.���s�˗��L�����Z�������̂ݍX�V�j
        If MyLogic.UpdateJibanIraiCancel(Me, jibanRec) = True Then
            cl.CloseWindow(Me)
        Else
            Dim tmpScript As String = ""
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "���s�˗��L�����Z��") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonTouroku_ServerClick", tmpScript, True)
        End If

    End Sub


#End Region

#Region "�ۏ؏��ύX������"

    ''' <summary>
    ''' (2) [�ۏ؏��]��b�񍐏��ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKisoHoukokusyo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(SelectKisoHoukokusyo) '�t�H�[�J�X

        '1.��ʂ̐ݒ�
        '��b�񍐏�
        Select Case SelectKisoHoukokusyo.SelectedValue
            Case "1" '�L

                '��b�񍐏�����
                If TextKisoHoukokusyoTyakuDate.Text = "" Then
                    TextKisoHoukokusyoTyakuDate.Text = Date.Now.ToString("yyyy/MM/dd")
                End If

                '�@<���ʏ��>�敪��"E"�̏ꍇ
                If ucGyoumuKyoutuu.Kubun = "E" Then

                    '�E�ۏ؏����s���������́A���A�ۏ؏����s�󋵁������͂̏ꍇ
                    If TextHosyousyoHakkouDate.Text = "" And SelectHosyousyoHakkouJyoukyou.SelectedValue = "" Then

                        '�E�ۏ؏����s����<���ʏ��>�敪�ɊY��������t�}�X�^.�ۏ؏����s��
                        TextHosyousyoHakkouDate.Text = MyLogic.GetHosyousyoHakkouDate(ucGyoumuKyoutuu.Kubun)

                        '�ۏ؏����s���ύX���������s�Ȃ���
                        SetKyoutuuTyousaJissiDate(sender, e)

                    End If

                End If

                '�A<���ʏ��>�敪��"E"�A���� "W"�ŁA�ۏ؊J�n���������͂̏ꍇ
                If ucGyoumuKyoutuu.Kubun = "E" Or ucGyoumuKyoutuu.Kubun = "W" Then

                    '�ۏ؊J�n��
                    If TextHosyouKaisiDate.Text = "" Then

                        '����1�R�[�h
                        Select Case HiddenHantei1CdOld.Value

                            Case "1", "2", "3" '1,2,3�̏ꍇ
                                '�E�ۏ؊J�n�����������{��
                                TextHosyouKaisiDate.Text = TextTyousaJissiDate.Text

                            Case Else '��L�ȊO

                                '�E���ǍH���������͂̏ꍇ
                                If TextKairyouKoujiDate.Text <> "" Then

                                    '�E�ۏ؊J�n����<���ǍH��>���ǍH����
                                    TextHosyouKaisiDate.Text = TextKairyouKoujiDate.Text

                                Else '������
                                    '�E�ۏ؊J�n�����������{��
                                    TextHosyouKaisiDate.Text = TextTyousaJissiDate.Text

                                End If

                        End Select

                    End If

                End If

            Case "0" '��

            Case Else

        End Select

        SetEnableControl() '��ʐ���

    End Sub

    ''' <summary>
    ''' (4) [�ۏ؏��]���s���˗����ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectHakkouIraisyo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(SelectHakkouIraisyo) '�t�H�[�J�X

        '1.��ʂ̐ݒ�
        '(1)���s�˗�����1(�L)�̏ꍇ
        Select Case SelectHakkouIraisyo.SelectedValue
            Case "1" '�L
                '�E���s�˗��������������͂̏ꍇ
                If TextHakkouIraiTyakuDate.Text = "" Then
                    TextHakkouIraiTyakuDate.Text = Date.Now.ToString("yyyy/MM/dd")
                End If

            Case Else '��L�ȊO

        End Select

        '��ʐ���
        SetEnableControl()
        SetEnableControlHhDate(Me.HiddenBukkenJyky.Value)

    End Sub

    ''' <summary>
    ''' (6) [�ۏ؏��]�ۏ؏����s�󋵕ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectHosyousyoHakkouJyoukyou_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        setFocusAJ(SelectHosyousyoHakkouJyoukyou) '�t�H�[�J�X

        Dim strMsg As String = String.Empty

        '1.��ʂ̐ݒ�	
        '(1)�ۏ؏����s�󋵁������͂̏ꍇ
        If SelectHosyousyoHakkouJyoukyou.SelectedValue = String.Empty Then
            '�ۏ؏����s�󋵐ݒ��
            TextHosyousyoHakkouJyoukyouSetteiDate.Text = String.Empty  '��

        Else
            '�n��T.�ۏ؏��i�L��="0"
            If Me.HiddenHosyouSyouhinUmu.Value <> "1" Then
                '�ۏ؏����s��.�ۏ؂���
                If cbLogic.GetHosyousyoHakJykyHosyouUmu(Me.SelectHosyousyoHakkouJyoukyou.SelectedValue) = "1" Then
                    '�ύX�O�̒l�ɖ߂�
                    Me.SelectHosyousyoHakkouJyoukyou.SelectedValue = Me.HiddenHosyousyoHakJyKyMae.Value
                    strMsg = Messages.MSG145E.Replace("@PARAM1", "���i�ݒ��").Replace("@PARAM2", EarthConst.DISP_HOSYOU_NASI_HOSYOU_SYOUHIN_UMU).Replace("@PARAM3", "�ۏ؏����s��")
                    MLogic.AlertMessage(sender, strMsg, 0)
                    Exit Sub
                End If
            End If

            '�ۏ؏����s�󋵐ݒ��
            TextHosyousyoHakkouJyoukyouSetteiDate.Text = Date.Now.ToString("yyyy/MM/dd")

        End If

        '���ۏ؏����s�󋵂ɂ��ۏ؂���/�Ȃ��̕\���ؑ�
        Me.ChgDispHosyouUmu()

    End Sub

    ''' <summary>
    ''' (7) [�ۏ؏��]�ۏ؏����s���ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextHosyousyoHakkouDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""

        '2.�ۏ؏����s�������͂̏ꍇ
        If TextHosyousyoHakkouDate.Text <> "" Then
            SetKyoutuuTyousaJissiDate(sender, e) '�y���ʁz�������{���Ó����m�F�������s���B
        End If

        '3.<�ۏ؏��Ĕ��s>�A<��񕥖�>�̉�ʐݒ�
        '�ۏ؏����s��
        Select Case TextHosyousyoHakkouDate.Text
            Case Is <> "" '����

                SetFuhoSyoumeisyoFlg() '�t�ۏؖ���FLG

                '<��񕥖�>���N���A
                SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" '��񕥖ߐ\���L��
                TextKySyouhinCd.Text = "" '���i�R�[�h
                TextKyKoumutenSeikyuuKingaku.Text = "" '�H���X�����Ŕ����z
                TextKyJituseikyuuKingaku.Text = "" '�������Ŕ����z
                TextKySyouhizei.Text = "" '�����
                TextKyZeikomiKingaku.Text = "" '�ō����z
                TextKySeikyuusyoHakkouDate.Text = "" '���������s��
                TextKyUriageNengappi.Text = "" '����N����
                TextKyHattyuusyoKakutei.Text = "" '�������m��


            Case "" '������

                '(*4)���������z��0,����,��Null�̏ꍇ�A�G���[���b�Z�[�W�i037�j��\�����A�����ݒ�i�N���A�j���s�킸�ɁA�����͂ɕύX��������(������/�Ĕ��s��/�����L��)�͌��̒l�ɖ߂��B
                If TextShHattyuusyoKingaku.Text <> "0" And TextShHattyuusyoKingaku.Text <> "" Then
                    tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                    TextHosyousyoHakkouDate.Text = HiddenHosyousyoHakkouDateMae.Value '�ύX�O�̒l�ɖ߂� ���� Javascript�ł̐ݒ�ł�Change�C�x���g���������Ȃ�
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextHosyousyoHakkouDate_TextChanged", tmpScript, True)
                    Exit Sub
                End If

                SetFuhoSyoumeisyoFlg() '�t�ۏؖ���FLG�̎����ݒ�

                '�������ݒ�
                Me.TextSaihakkouDate.Text = "" '�Ĕ��s��
                Me.TextSaihakkouRiyuu.Text = String.Empty '�Ĕ��s���R

                ClearBlnkSyouhinTableSh() '�󔒃N���A

        End Select

        setFocusAJ(TextHosyouKaisiDate) '�t�H�[�J�X

        '��ʐ���
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' �t�ۏؖ���FLG�̏����l�Z�b�g���s�Ȃ�
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetFuhoSyoumeisyoFlg()

        '�t�ۏؖ���FLG�����ݒ�t���O��"1"�ȊO�̏ꍇ
        If HiddenSetFuhoSyoumeisyoFlg.Value <> EarthConst.ARI_VAL Then Exit Sub '�����ݒ���s�Ȃ�Ȃ�

        '�ۏ؏����s��
        If TextHosyousyoHakkouDate.Text = "" Then '������
            SelectFuhoSyoumeisyoFlg.SelectedValue = "" '��

        Else '����

            Dim KameitenSearchLogic As New KameitenSearchLogic
            Dim recData1 As New KameitenSearchRecord
            Dim tmpScript As String = ""

            setFocusAJ(SelectFuhoSyoumeisyoFlg) '�t�H�[�J�X

            recData1 = KameitenSearchLogic.GetFuhoSyoumeisyoInfo( _
                                                ucGyoumuKyoutuu.Kubun _
                                                , ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                , False _
                                                )

            If Not recData1 Is Nothing Then

                ' �����X.�t�ۏؖ���FLG
                If recData1.FuhoSyoumeisyoFlg <> 1 Then '<>�L��
                    SelectFuhoSyoumeisyoFlg.SelectedValue = "0" '�Ȃ�
                End If

                '�t�ۏؖ����J�n�N��
                If cl.GetDisplayString(recData1.FuhoSyoumeiKaisiNengetu) = "" Then '������
                    SelectFuhoSyoumeisyoFlg.SelectedValue = "0" '�Ȃ�

                Else '����

                    Dim dtHosyousyoHakkouDate As New DateTime '�ۏ؏����s��
                    Dim dtFuhoSyoumeiKaisiDate As New DateTime '�����X.�t�ۏؖ����J�n�N��

                    dtHosyousyoHakkouDate = DateTime.Parse(TextHosyousyoHakkouDate.Text)
                    dtFuhoSyoumeiKaisiDate = recData1.FuhoSyoumeiKaisiNengetu

                    '�ۏ؏����s�� < �����X.�t�ۏؖ����J�n�N��
                    If dtHosyousyoHakkouDate < dtFuhoSyoumeiKaisiDate Then
                        SelectFuhoSyoumeisyoFlg.SelectedValue = "0" '�Ȃ�

                    Else '�ۏ؏����s�� >= �����X.�t�ۏؖ����J�n�N��

                        ' �����X.�t�ۏؖ���FLG
                        If recData1.FuhoSyoumeisyoFlg = 1 Then '�L��
                            SelectFuhoSyoumeisyoFlg.SelectedValue = "1" '�L��
                        End If

                    End If
                End If

            End If

        End If
    End Sub

    ''' <summary>
    ''' �y���ʁz�������{���Ó����m�F�������s���B��(17) �y���ʁz�������{���Ó����m�F�����ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SetKyoutuuTyousaJissiDate(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""

        '****************************
        '* �������{���̓o�^�m�F
        '****************************
        '�������{��
        '(1)�������{���������͂̏ꍇ
        If TextTyousaJissiDate.Text = "" Then '������
            '�o�^����OK���Ă��Ȃ��ꍇ
            If HiddenHosyousyoHakkouDateMsg04.Value <> "1" Then
                '�o�^�m�F���b�Z�[�W��\������B
                tmpScript = "if(confirm('" & Messages.MSG098C & "')){" & vbCrLf
                tmpScript &= "  objEBI('" & HiddenHosyousyoHakkouDateMsg04.ClientID & "').value = '1';" & vbCrLf
                tmpScript &= "}" & vbCrLf
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetKyoutuuTyousaJissiDate1", tmpScript, True)
            End If

        Else '(2)��L�ȊO�̏ꍇ

            '�������{���A���ۏ؏����s���ɓ��͂�����ꍇ
            If TextTyousaJissiDate.Text <> "" And TextHosyousyoHakkouDate.Text <> "" Then

                Dim dtTyousa As Date = Date.Parse(TextTyousaJissiDate.Text)
                Dim dtHosyousyo As Date = Date.Parse(TextHosyousyoHakkouDate.Text)

                '�E�������{�����ۏ؏����s���̏ꍇ
                If dtTyousa > dtHosyousyo Then

                    '�o�^����OK���Ă��Ȃ��ꍇ
                    If HiddenHosyousyoHakkouDateMsg03.Value <> "1" Then
                        '�o�^�m�F���b�Z�[�W��\������B
                        tmpScript = "if(confirm('" & Messages.MSG099C & "')){" & vbCrLf
                        tmpScript &= "  objEBI('" & HiddenHosyousyoHakkouDateMsg03.ClientID & "').value = '1';" & vbCrLf
                        tmpScript &= "}" & vbCrLf
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetKyoutuuTyousaJissiDate2", tmpScript, True)
                    End If

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' (19)�t�ۏؖ���FLG�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectFuhoSyoumeisyoFlg_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim recData1 As New KameitenSearchRecord
        Dim tmpScript As String = ""

        setFocusAJ(SelectFuhoSyoumeisyoFlg) '�t�H�[�J�X

        recData1 = kameitenlogic.GetFuhoSyoumeisyoInfo( _
                                            ucGyoumuKyoutuu.Kubun _
                                            , ucGyoumuKyoutuu.AccKameitenCd.Value _
                                            , True _
                                            )

        If Not recData1 Is Nothing Then

            ' �����X.�t�ۏؖ���FLG
            If recData1.FuhoSyoumeisyoFlg <> 1 Then '<>�L��
                '�t�ۏؖ���FLG
                If SelectFuhoSyoumeisyoFlg.SelectedValue = "1" Then '�L��
                    tmpScript = "callFuhoSyoumeisyoFlgCancel('" & Messages.MSG108E & "','" & ButtonFuhoSyoumeisyoFlg.ClientID & "');" & vbCrLf
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectFuhoSyoumeisyoFlg_SelectedIndexChanged1", tmpScript, True)
                    Exit Sub
                End If

            Else '�L��

                '�t�ۏؖ���FLG
                If HiddenFuhoSyoumeisyoFlgMae.Value = "1" And SelectFuhoSyoumeisyoFlg.SelectedValue <> "1" Then '�L�聨<>�L��

                    Dim dtHosyousyoHakkouDate As New DateTime '�ۏ؏����s��
                    Dim dtFuhoSyoumeiKaisiDate As New DateTime '�����X.�t�ۏؖ����J�n�N��

                    '�ۏ؏����s��
                    If TextHosyousyoHakkouDate.Text = "" Then Exit Sub '�����͎��A�����𔲂���

                    '�t�ۏؖ����J�n�N��
                    If cl.GetDisplayString(recData1.FuhoSyoumeiKaisiNengetu) = "" Then Exit Sub '�����͎��A�����𔲂���

                    dtHosyousyoHakkouDate = DateTime.Parse(TextHosyousyoHakkouDate.Text)
                    dtFuhoSyoumeiKaisiDate = recData1.FuhoSyoumeiKaisiNengetu

                    '�ۏ؏����s�� < �����X.�t�ۏؖ����J�n�N��
                    If dtHosyousyoHakkouDate < dtFuhoSyoumeiKaisiDate Then
                        '�Ȃ�

                    Else '�ۏ؏����s�� >= �����X.�t�ۏؖ����J�n�N��

                        tmpScript = "callFuhoSyoumeisyoFlgCancel('" & Messages.MSG109E & "','" & ButtonFuhoSyoumeisyoFlg.ClientID & "');" & vbCrLf
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectFuhoSyoumeisyoFlg_SelectedIndexChanged2", tmpScript, True)
                        Exit Sub

                    End If

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' �t�ۏؖ���FLG�ύX�E�㏈��(JS�ɂăL�����Z����)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonFuhoSyoumeisyoFlgCancel_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        setFocusAJ(SelectFuhoSyoumeisyoFlg) '�t�H�[�J�X

        '�t�ۏؖ���FLG
        SelectFuhoSyoumeisyoFlg.SelectedValue = HiddenFuhoSyoumeisyoFlgMae.Value '�ύX�O�̒l�ɖ߂�

    End Sub

#End Region

#Region "�Ĕ��s�ύX������"

    ''' <summary>
    ''' (10) [�Ĕ��s]�Ĕ��s���ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSaihakkouDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strDtTmp As String
        Dim tmpScript As String = ""

        setFocusAJ(TextSaihakkouDate) '�t�H�[�J�X

        '�Ĕ��s��
        If TextSaihakkouDate.Text <> "" Then '����

            setFocusAJ(TextFocusBounderSaihakkouRiyuu) '�t�H�[�J�X

            '�����L��
            Select Case SelectShSeikyuuUmu.SelectedValue
                Case "1" '�L
                    '���i�R�[�h
                    If TextShSyouhinCd.Text <> "" Then '�ݒ��
                        '���������s��
                        If TextShSeikyuusyoHakkouDate.Text.Length = 0 Then
                            '�������ߓ��̃Z�b�g
                            Me.ucSeikyuuSiireLinkSai.SetSeikyuuSimeDate(Me.TextShSyouhinCd.Text)
                            '���������s���̎����ݒ�
                            strDtTmp = Me.ucSeikyuuSiireLinkSai.GetSeikyuusyoHakkouDate()
                            TextShSeikyuusyoHakkouDate.Text = strDtTmp
                        End If

                        '����N����
                        If TextShUriageNengappi.Text.Length = 0 Then '������
                            '����N�����̎����ݒ�
                            TextShUriageNengappi.Text = Date.Now.ToString("yyyy/MM/dd")
                        End If

                    End If

                Case "" '��
                    '���i�R�[�h
                    If TextShSyouhinCd.Text <> "" Then '�ݒ��

                        '����N����
                        If TextShUriageNengappi.Text.Length = 0 Then '������
                            '����N�����̎����ݒ�
                            TextShUriageNengappi.Text = Date.Now.ToString("yyyy/MM/dd")
                        End If

                    End If
            End Select

        Else '������

            '(*4)���������z��0,����,��Null�̏ꍇ�A�G���[���b�Z�[�W�i037�j��\�����A�����ݒ�i�N���A�j���s�킸�ɁA�����͂ɕύX��������(������/�Ĕ��s��/�����L��)�͌��̒l�ɖ߂��B
            If TextShHattyuusyoKingaku.Text <> "0" And TextShHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                TextSaihakkouDate.Text = HiddenSaihakkouDateMae.Value '�ύX�O�̒l�ɖ߂� ���� Javascript�ł̐ݒ�ł�Change�C�x���g���������Ȃ�
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextSaihakkouDate_TextChanged", tmpScript, True)
                Exit Sub
            End If

            '�������ݒ�
            Me.TextSaihakkouRiyuu.Text = String.Empty '�Ĕ��s���R

            ClearBlnkSyouhinTableSh() '�󔒃N���A

        End If

        SetEnableControl() '��ʐ���

    End Sub

    ''' <summary>
    ''' (11) [�Ĕ��s]�����L���ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectShSeikyuu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""
        Dim blnTorikesi As Boolean = False
        Dim strDtTmp As String

        Dim syouhinRec As New Syouhin23Record

        setFocusAJ(SelectShSeikyuuUmu) '�t�H�[�J�X

        '�d���z�͏��0�~
        Me.HiddenShSiireGaku.Value = "0"
        Me.HiddenShSiireSyouhiZei.Value = "0"

        '�����L��
        If SelectShSeikyuuUmu.SelectedValue = "" Then '��

            '(*4)���������z��0,����,��Null�̏ꍇ�A�G���[���b�Z�[�W�i037�j��\�����A�����ݒ�i�N���A�j���s�킸�ɁA�����͂ɕύX��������(������/�Ĕ��s��/�����L��)�͌��̒l�ɖ߂��B
            If TextShHattyuusyoKingaku.Text <> "0" And TextShHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                SelectShSeikyuuUmu.SelectedValue = HiddenShSeikyuuUmuMae.Value '�ύX�O�̒l�ɖ߂� ���� Javascript�ł̐ݒ�ł�Change�C�x���g���������Ȃ�
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectShSeikyuu_SelectedIndexChanged", tmpScript, True)
                Exit Sub
            End If

            '�������ݒ�
            ClearBlnkSyouhinTableSh() '�󔒃N���A

        ElseIf SelectShSeikyuuUmu.SelectedValue = "0" Then '��
            '���z��0�N���A
            Clear0SyouhinTableSh()

            '���i�R�[�h/���i���̎����ݒ聣
            SetSyouhinInfoSh()

            '���z�̍Čv�Z
            SetKingaku(EnumKingakuType.Saihakkou)

        ElseIf SelectShSeikyuuUmu.SelectedValue = "1" Then '�L

            '���i�R�[�h/���i���̎����ݒ聣
            SetSyouhinInfoSh()

            '���i�R�[�h
            If TextShSyouhinCd.Text.Length = 0 Then '�ݒ�Ȃ�
                '�����L��
                SelectShSeikyuuUmu.SelectedValue = "" '��

                SetEnableControl() '��ʐ���
                Exit Sub
            Else
                '���������s��
                If TextShSeikyuusyoHakkouDate.Text.Length = 0 Then
                    '�������ߓ��̃Z�b�g
                    Me.ucSeikyuuSiireLinkSai.SetSeikyuuSimeDate(Me.TextShSyouhinCd.Text)
                    '���������s���̎����ݒ�
                    strDtTmp = Me.ucSeikyuuSiireLinkSai.GetSeikyuusyoHakkouDate()
                    TextShSeikyuusyoHakkouDate.Text = strDtTmp
                End If

                TextShUriageNengappi.Text = Date.Now.ToString("yyyy/MM/dd") '����N����
                '�������m��
                If TextShHattyuusyoKakutei.Text.Length = 0 Then '(*5)�������m�肪�󔒂̏ꍇ�́A�u0�F���m��v��ݒ肷��
                    TextShHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
                End If

            End If

            '*************************
            '* �ȉ��A�����ݒ菈��
            '*************************
            If TextShSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '���ڐ���

                '(*6)�����X������̏ꍇ�A������3�n��ȊO�̈����Ƃ���
                Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                If record Is Nothing Then '�f�[�^�擾�ł��Ȃ������ꍇ
                    SelectShSeikyuuUmu.SelectedValue = "" '�擾NG(�����L���̃N���A)

                    ClearBlnkSyouhinTableSh() '�󔒃N���A
                End If

                If record.Torikesi <> 0 Then '����t���O�������Ă���ꍇ
                    '**************************************************
                    ' �������i�n��ȊO�j
                    '**************************************************
                    ' �H���X�����z�͂O
                    TextShKoumutenSeikyuuKingaku.Text = "0"

                    '�������Ŕ����z�̎����ݒ�
                    syouhinRec = JibanLogic.GetSyouhinInfo(TextShSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.Hosyousyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        HiddenShZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
                        HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪

                        '�������Ŕ����z�����i�}�X�^.�W�����i
                        TextShJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                    End If

                Else
                    '**************************************************
                    ' ���ڐ���
                    '**************************************************
                    '�H���X(A)
                    '������(A)

                    syouhinRec = JibanLogic.GetSyouhinInfo(TextShSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.Hosyousyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        '�H���X�����Ŕ����z�����i�}�X�^.�W�����i
                        TextShKoumutenSeikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)
                        '�������Ŕ����z�����.�H���X�����Ŕ����z
                        TextShJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                        HiddenShZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
                        HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪
                    End If

                End If

            ElseIf TextShSeikyuusaki.Text = EarthConst.SEIKYU_TASETU Then '������

                Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                If record Is Nothing Then '�f�[�^�擾�ł��Ȃ������ꍇ
                    SelectShSeikyuuUmu.SelectedValue = "" '�擾NG(�����L���̃N���A)

                    ClearBlnkSyouhinTableSh() '�󔒃N���A
                End If

                '(*6)�����X������̏ꍇ�A������3�n��ȊO�̈����Ƃ���
                If record.Torikesi <> 0 Then
                    record.KeiretuCd = ""
                End If

                If cl.getKeiretuFlg(record.KeiretuCd) Then '3�n��
                    '**************************************************
                    ' �������i3�n��j
                    '**************************************************
                    '�H���X(A)
                    '������(B)

                    syouhinRec = JibanLogic.GetSyouhinInfo(TextShSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.Hosyousyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        HiddenShZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
                        HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪

                        '�H���X�����Ŕ����z�����i�}�X�^.�W�����i
                        TextShKoumutenSeikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                        '�����.�H���X�����Ŕ����z��0 �̏ꍇ�� 0 �Œ�
                        If TextShKoumutenSeikyuuKingaku.Text = "0" Then
                            TextShJituseikyuuKingaku.Text = "0" '�������Ŕ����z

                        Else

                            Dim zeinukiGaku As Integer = 0

                            If JibanLogic.GetSeikyuuGaku(sender, _
                                                          3, _
                                                          record.KeiretuCd, _
                                                          TextShSyouhinCd.Text, _
                                                          syouhinRec.HyoujunKkk, _
                                                          zeinukiGaku) Then
                                ' ���������z�փZ�b�g
                                TextShJituseikyuuKingaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                            End If

                        End If

                    End If

                Else '3�n��ȊO

                    '**************************************************
                    ' �������i3�n��ȊO�j
                    '**************************************************
                    '�H���X(B)
                    '������(C)

                    ' �H���X�����z�͂O
                    TextShKoumutenSeikyuuKingaku.Text = "0"

                    '�������Ŕ����z�̎����ݒ�
                    syouhinRec = JibanLogic.GetSyouhinInfo(TextShSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.Hosyousyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        HiddenShZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
                        HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪

                        '�������Ŕ����z�����i�}�X�^.�W�����i
                        TextShJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                    End If

                End If

            End If

            '���z�̍Čv�Z
            SetKingaku(EnumKingakuType.Saihakkou)

        End If

        '��ʐ���
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' (12) [�Ĕ��s]�H���X�����Ŕ����z�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextShKoumutenSeikyuuKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim syouhinRec As New Syouhin23Record

        setFocusAJ(TextShJituseikyuuKingaku) '�t�H�[�J�X

        '����
        If SelectShSeikyuuUmu.SelectedValue = "1" Then '�L
            '���i�R�[�h
            If TextShSyouhinCd.Text.Length <> 0 Then '�ݒ��

                '�ŏ��ݒ�
                SetShZeiInfo(TextShSyouhinCd.Text)

                '������
                If TextShSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '���ڐ���
                    '**************************************************
                    ' ���ڐ���
                    '**************************************************

                    '�������Ŕ����z�����.�H���X�����Ŕ����z
                    TextShJituseikyuuKingaku.Text = TextShKoumutenSeikyuuKingaku.Text

                    SetKingaku(EnumKingakuType.Saihakkou) '���z�Čv�Z

                ElseIf TextShSeikyuusaki.Text = EarthConst.SEIKYU_TASETU Then '������

                    Dim kameitenlogic As New KameitenSearchLogic
                    Dim blnTorikesi As Boolean = False
                    Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                    '(*6)�����X������̏ꍇ�A������3�n��ȊO�̈����Ƃ���
                    If record.Torikesi <> 0 Then
                        record.KeiretuCd = ""
                    End If

                    If cl.getKeiretuFlg(record.KeiretuCd) Then '3�n��
                        '**************************************************
                        ' �������i3�n��j
                        '**************************************************

                        '<�\2>�������Ŕ����z�i�|���j�̐ݒ聣

                        Dim logic As New JibanLogic
                        Dim koumuten_gaku As Integer = 0
                        Dim zeinuki_gaku As Integer = 0

                        cl.SetDisplayString(TextShKoumutenSeikyuuKingaku.Text, koumuten_gaku)
                        koumuten_gaku = IIf(koumuten_gaku = Integer.MinValue, 0, koumuten_gaku)

                        If logic.GetSeikyuuGaku(sender, _
                                                5, _
                                                record.KeiretuCd, _
                                                TextShSyouhinCd.Text, _
                                                koumuten_gaku, _
                                                zeinuki_gaku) Then


                            '���i�����擾(�L�[:���i�R�[�h)
                            syouhinRec = JibanLogic.GetSyouhinInfo(TextShSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.Hosyousyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                            If Not syouhinRec Is Nothing Then
                                '(*3)�����L���ύX���ɁA�����ݒ肳�ꂽ�H���X�������z��0�i���i�}�X�^.�W�����i��0�j�̏ꍇ�A1��̂ݎ��������z�̎����ݒ���s���B
                                If syouhinRec.HyoujunKkk = 0 Then
                                    If HiddenShJituseikyuu1Flg.Value = "" Then
                                        HiddenShJituseikyuu1Flg.Value = "1" '�t���O�����Ă�

                                        ' �Ŕ����z�i���������z�j�փZ�b�g
                                        TextShJituseikyuuKingaku.Text = zeinuki_gaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                                    End If
                                    '*****************
                                    '* �n�ՃV�X�e���̏����ɍ��킹�邽�߁A�R�����g�A�E�g
                                    '*****************
                                    'Else
                                    '    ' �Ŕ����z�i���������z�j�փZ�b�g
                                    '    TextShJituseikyuuKingaku.Text = zeinuki_gaku.ToString(EarthConst.FORMAT_KINGAKU1)

                                End If

                            End If
                        End If

                        SetKingaku(EnumKingakuType.Saihakkou) '���z�Čv�Z

                    End If

                End If
            End If

        End If

        '��ʐ���
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' (14) [�Ĕ��s]�������Ŕ����z�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextShJituseikyuuKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(TextShSeikyuusyoHakkouDate) '�t�H�[�J�X

        '�������Ŕ����z
        If TextShJituseikyuuKingaku.Text.Length = 0 Then '���͂Ȃ�
            TextShSyouhizei.Text = "" '�����
            TextShZeikomiKingaku.Text = "" '�ō����z

            SetKingaku(EnumKingakuType.Saihakkou) '���z�̍Čv�Z

        Else '���͂���

            '�����L��
            If SelectShSeikyuuUmu.SelectedValue = "1" Then '�L
                '���i�R�[�h
                If TextShSyouhinCd.Text.Length <> 0 Then '�ݒ��

                    '�ŏ��ݒ�
                    SetShZeiInfo(TextShSyouhinCd.Text)

                    '������
                    If TextShSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '���ڐ���

                        '�H���X�����Ŕ����z�����.�������Ŕ����z
                        TextShKoumutenSeikyuuKingaku.Text = TextShJituseikyuuKingaku.Text

                        SetKingaku(EnumKingakuType.Saihakkou) '���z�Čv�Z

                    Else '���ڐ����ȊO
                        SetKingaku(EnumKingakuType.Saihakkou) '���z�Čv�Z
                    End If

                End If

            End If

        End If

        '��ʐ���
        SetEnableControl()
    End Sub

#Region "(16) [�Ĕ��s][��񕥖�]��񕥖ߐ\���L���ύX�������֘A"

    ''' <summary>
    ''' (16) [�Ĕ��s][��񕥖�]��񕥖ߐ\���L���ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKyKaiyakuHaraimodosiSinseiUmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpscript As String = ""

        setFocusAJ(SelectKyKaiyakuHaraimodosiSinseiUmu) '�t�H�[�J�X

        '1.��񕥖߃`�F�b�N�A����щ�ʂ̐ݒ�A��ʐ���	

        '(1)<��񕥖�>��񕥖ߐ\���L����1(�L)�̏ꍇ
        Select Case SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue
            Case "1" '�L
                '�@<hidden><���i�R�[�h1>���������s���������͂̏ꍇ
                If HiddenSyouhin1SeikyuuHakkouDate.Value = "" Then
                    '��񕥖ߐ\���L��
                    SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" '��

                    SetEnableControl() '��ʐ���

                    MLogic.AlertMessage(sender, Messages.MSG096E, 0)
                    Exit Sub

                ElseIf MyLogic.GetKaiyakuKakaku(ucGyoumuKyoutuu.AccKameitenCd.Value, ucGyoumuKyoutuu.Kubun) = 0 Then '�A��񕥖߉��i�iKEY�F���ʉ��.�����X�R�[�h�j��0�̏ꍇ

                    '��񕥖ߐ\���L��
                    SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" '��

                    SetFocus(SelectKyKaiyakuHaraimodosiSinseiUmu) '�t�H�[�J�X

                    SetEnableControl() '��ʐ���

                    MLogic.AlertMessage(sender, Messages.MSG053E, 0)
                    Exit Sub

                Else '�B�@�A�A�ȊO�̏ꍇ


                    '��񕥖ߐ\���L���ύX������
                    SelectKyKaiyakuHaraimodosiSinseiUmu_ChangeSub(sender, e)

                    'b.��ʂ̕ҏW�Ɠ��͐���
                    SetEnableControl() '��ʐ���

                End If

            Case Else '�L �ȊO

                '��񕥖ߐ\���L���ύX������
                SelectKyKaiyakuHaraimodosiSinseiUmu_ChangeSub(sender, e)

                SetEnableControl() '��ʐ���

        End Select

        SetEnableControlHhDate(Me.HiddenBukkenJyky.Value) '��ʐ���

    End Sub

    ''' <summary>
    ''' �����ݒ�/��񕥖ߐ\���L���ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKyKaiyakuHaraimodosiSinseiUmu_ChangeSub(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim syouhinRec As New Syouhin23Record
        Dim tmpScript As String = ""
        Dim strDtTmp As String
        Dim blnTorikesi As Boolean = False

        '�d���z�͏��0�~
        Me.HiddenKySiireGaku.Value = "0"
        Me.HiddenKySiireSyouhiZei.Value = "0"

        '��񕥖ߗL��
        Select Case SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue

            Case "1" '�L

                If MyLogic.GetKaiyakuKakaku(ucGyoumuKyoutuu.AccKameitenCd.Value, ucGyoumuKyoutuu.Kubun) <> 0 Then

                    '���i�R�[�h�̎����ݒ聣
                    SetSyouhinInfoKy()

                    '���i�R�[�h
                    If TextKySyouhinCd.Text = String.Empty Then '�ݒ�Ȃ�
                        '�����L��
                        SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" '��

                        SetEnableControlKy() '��ʐ���
                        Exit Sub
                    Else
                        '���������s��
                        If TextKySeikyuusyoHakkouDate.Text.Length = 0 Then
                            '�������ߓ��̃Z�b�g
                            Me.ucSeikyuuSiireLinkKai.SetSeikyuuSimeDate(Me.TextKySyouhinCd.Text)
                            '���������s���̎����ݒ�
                            strDtTmp = Me.ucSeikyuuSiireLinkKai.GetSeikyuusyoHakkouDate()
                            TextKySeikyuusyoHakkouDate.Text = strDtTmp
                        End If

                        '����N����
                        TextKyUriageNengappi.Text = Date.Now.ToString("yyyy/MM/dd")

                        '�������m��
                        If TextKyHattyuusyoKakutei.Text = "" Then
                            TextKyHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI '���m��
                        End If

                    End If

                    '*************************
                    '* �ȉ��A�����ݒ菈��
                    '*************************
                    '������
                    Select Case TextKySeikyuusaki.Text

                        Case EarthConst.SEIKYU_TYOKUSETU '���ڐ���

                            '(*6)�����X������̏ꍇ�A������3�n��ȊO�̈����Ƃ���
                            Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                            If record Is Nothing Then '�f�[�^�擾�ł��Ȃ������ꍇ
                                SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" '�擾NG(�����L���̃N���A)

                                ClearBlnkSyouhinTableKy() '�󔒃N���A
                            End If

                            If record.Torikesi <> 0 Then '����t���O�������Ă���ꍇ
                                '�H���X(B)
                                '**************************************************
                                ' �������i3�n��ȊO�j
                                '**************************************************
                                ' �H���X�����z�͂O
                                TextKyKoumutenSeikyuuKingaku.Text = "0"

                                '�������Ŕ����z�������X�}�X�^.��񕥖߉��i * -1
                                TextKyJituseikyuuKingaku.Text = MyLogic.GetKaiyakuKakaku(ucGyoumuKyoutuu.AccKameitenCd.Value, ucGyoumuKyoutuu.Kubun) * -1

                            Else
                                '**************************************************
                                ' ���ڐ���
                                '**************************************************

                                '�������Ŕ����z�������X�}�X�^.��񕥖߉��i * -1
                                TextKyJituseikyuuKingaku.Text = MyLogic.GetKaiyakuKakaku(ucGyoumuKyoutuu.AccKameitenCd.Value, ucGyoumuKyoutuu.Kubun) * -1

                                '�H���X(A)
                                '�H���X�����Ŕ����z�����.�������Ŕ����z
                                TextKyKoumutenSeikyuuKingaku.Text = TextKyJituseikyuuKingaku.Text

                                SetKingaku(EnumKingakuType.KaiyakuHaraimodosi) '���z�̍Čv�Z

                            End If

                            SetKingaku(EnumKingakuType.KaiyakuHaraimodosi) '���z�̍Čv�Z

                        Case EarthConst.SEIKYU_TASETU '������

                            Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                            If record Is Nothing Then '�f�[�^�擾�ł��Ȃ������ꍇ
                                SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" '�擾NG(�����L���̃N���A)

                                ClearBlnkSyouhinTableKy() '�󔒃N���A
                            End If

                            '(*6)�����X������̏ꍇ�A������3�n��ȊO�̈����Ƃ���
                            If record.Torikesi <> 0 Then
                                record.KeiretuCd = ""
                            End If

                            If cl.getKeiretuFlg(record.KeiretuCd) Then '3�n��
                                '**************************************************
                                ' �������i3�n��j
                                '**************************************************
                                Dim zeinukiGaku As Integer = 0

                                '�������z�̎擾
                                If JibanLogic.GetSeikyuuGaku(sender, _
                                                              2, _
                                                              record.KeiretuCd, _
                                                              TextKySyouhinCd.Text, _
                                                              syouhinRec.HyoujunKkk, _
                                                              zeinukiGaku) Then
                                    ' �H���X�����Ŕ����z�փZ�b�g
                                    TextKyKoumutenSeikyuuKingaku.Text = (Math.Abs(zeinukiGaku) * -1).ToString(EarthConst.FORMAT_KINGAKU_1)

                                    '�������Ŕ����z�������X�}�X�^.��񕥖߉��i * -1
                                    TextKyJituseikyuuKingaku.Text = MyLogic.GetKaiyakuKakaku(ucGyoumuKyoutuu.AccKameitenCd.Value, ucGyoumuKyoutuu.Kubun) * -1

                                End If

                            Else '3�n��ȊO

                                '�H���X(B)
                                '**************************************************
                                ' �������i3�n��ȊO�j
                                '**************************************************
                                ' �H���X�����z�͂O
                                TextKyKoumutenSeikyuuKingaku.Text = "0"

                                '�������Ŕ����z�������X�}�X�^.��񕥖߉��i * -1
                                TextKyJituseikyuuKingaku.Text = MyLogic.GetKaiyakuKakaku(ucGyoumuKyoutuu.AccKameitenCd.Value, ucGyoumuKyoutuu.Kubun) * -1

                            End If

                            SetKingaku(EnumKingakuType.KaiyakuHaraimodosi) '���z�̍Čv�Z

                    End Select

                End If

            Case "" '��

                '(*4)���������z��0,����,��Null�̏ꍇ�A�G���[���b�Z�[�W�i037�j��\�����A�����ݒ�i�N���A�j���s�킸�ɁA�����͂ɕύX��������(������/�Ĕ��s��/�����L��)�͌��̒l�ɖ߂��B
                If TextKyHattyuusyoKingaku.Text <> "0" And TextKyHattyuusyoKingaku.Text <> "" Then
                    MLogic.AlertMessage(sender, Messages.MSG010E, 0)
                    SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = HiddenKyKaiyakuHaraimodosiSinseiUmuMae.Value '�ύX�O�̒l�ɖ߂� ���� Javascript�ł̐ݒ�ł�Change�C�x���g���������Ȃ�
                    Exit Sub
                End If

                TextKySyouhinCd.Text = "" '���i�R�[�h
                TextKyKoumutenSeikyuuKingaku.Text = "" '�H���X�����Ŕ����z
                TextKyJituseikyuuKingaku.Text = "" '�������Ŕ����z
                TextKySyouhizei.Text = "" '�����
                TextKyZeikomiKingaku.Text = "" '�ō����z
                TextKySeikyuusyoHakkouDate.Text = "" '���������s��
                TextKyUriageNengappi.Text = "" '����N����
                TextKyHattyuusyoKakutei.Text = "" '�������m��

        End Select

    End Sub

#End Region

#End Region

#Region "��ʐ���"

    ''' <summary>
    ''' �R���g���[���̉�ʐ���/�ۏ؉��
    ''' </summary>
    ''' <remarks>�R���g���[���̉�ʐ�����d�l���̗D�揇�ɋL�ڂ���</remarks>
    Public Sub SetEnableControl()
        '���ʏ��
        ucGyoumuKyoutuu.SetEnableKameiten(Me.HiddenBukkenJyky.Value)
        '�ۏ؏��
        SetEnableControlHosyou()
        '�Ĕ��s
        SetEnableControlSh()
        '��񕥖�
        SetEnableControlKy()
        '���s�˗�
        SetEnableControlHakkouIrai()
    End Sub

    ''' <summary>
    ''' �R���g���[���̉�ʐ���/���s�˗����
    ''' </summary>
    ''' <remarks>�R���g���[���̉�ʐ�����d�l���̗D�揇�ɋL�ڂ���</remarks>
    Public Sub SetEnableControlHakkouIrai()

        '******************
        '* �񊈐���
        '******************

        ' ���L�����ɊY������ꍇ�A�{�^�����\���ɐݒ肵�A�G���A�\��������ԂƂ���
        ' �E�i���f�[�^.���s�˗���������(��������)
        ' �E�i���f�[�^.���s�˗����� < �i���f�[�^.���s�˗���t�����@��������
        ' �E�i���f�[�^.���s�˗����� < �i���f�[�^.���s�˗��L�����Z������

        If Me.HiddenHakIraiTime.Value = "" Or _
           Me.HiddenHakIraiTime.Value < Me.HiddenHakIraiUkeDatetimeR.Value Or _
           Me.HiddenHakIraiTime.Value < Me.HiddenHakIraiCanDatetimeR.Value _
        Then
            ButtonHakkouCancel.Disabled = True ' ���s�L�����Z���{�^��
            ButtonHakkouSet.Disabled = True ' ���s�Z�b�g�{�^��
            ButtonBukkenTenki.Disabled = True
            ButtonHakkouSet.Disabled = True
            ButtonJyuusyoTenki.Disabled = True
            ButtonHosyouKaisiDateTenki.Disabled = True

            Dim jBn As New Jiban
            Dim noTarget As New Hashtable
            noTarget.Add(divHakkouIrai, True) '���s�˗��^�u
            jBn.ChangeDesabledAll(divHakkouIrai, True, noTarget)

            Me.HiddenChkKaisiDate.Value = "1"
        Else
            Me.HiddenChkKaisiDate.Value = "0"
        End If


    End Sub

    ''' <summary>
    ''' �R���g���[���̉�ʐ���/�ۏ؏��
    ''' </summary>
    ''' <remarks>�R���g���[���̉�ʐ�����d�l���̗D�揇�ɋL�ڂ���</remarks>
    Public Sub SetEnableControlHosyou()
        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban
        '�L�����A�������̑ΏۊO�ɂ���R���g���[���S
        Dim noTarget As New Hashtable
        noTarget.Add(divHosyou, True) '�ۏ؏��

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable

        Dim hLogic As New HosyouLogic

        '******************
        '* �񊈐���
        '******************

        '��񕥖ߐ\���L�����L
        If SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue <> "" Then
            '��b�񍐏�
            jSM.Hash2Ctrl(TdKisoHoukokusyo, EarthConst.MODE_VIEW, ht)
            '�ۏ؏����s��
            jSM.Hash2Ctrl(TdHosyousyoHakkouJyoukyou, EarthConst.MODE_VIEW, ht)
            '�ۏ؊J�n��
            jSM.Hash2Ctrl(TdHosyouKaisiDate, EarthConst.MODE_VIEW, ht)
            '�ۏؗL��
            jSM.Hash2Ctrl(TdHosyouUmu, EarthConst.MODE_VIEW, ht)
        End If

        '��b�񍐏���������
        If SelectKisoHoukokusyo.SelectedValue = "" Then
            '��b�񍐏�����
            jSM.Hash2Ctrl(TdKisoHoukokusyoTyakuDate, EarthConst.MODE_VIEW, ht)
        Else
            cl.chgDispSyouhinText(TextKisoHoukokusyoTyakuDate)
        End If

        '���s�˗�����������
        If SelectHakkouIraisyo.SelectedValue = "" Then
            '���s�˗�������
            jSM.Hash2Ctrl(TdHakkouIraiTyakuDate, EarthConst.MODE_VIEW, ht)
        Else
            cl.chgDispSyouhinText(TextHakkouIraiTyakuDate)
        End If

        '�t�ۏؖ���FLG�͏�ɔ񊈐�
        jSM.Hash2Ctrl(TdFuhoSyoumeisyoFlg, EarthConst.MODE_VIEW, ht)

        '�t�ۏؖ��������������͂���Ă���ꍇ�́A�t�ۏؖ���FLG�̎����ݒ���s�Ȃ�Ȃ�
        '�����X���l�ݒ�}�X�^.���l���(=42)�̃`�F�b�N
        If TextFuhoSyoumeisyoHassouDate.Text = "" And hLogic.ExistBikouSyubetu(ucGyoumuKyoutuu.AccKameitenCd.Value) Then
            '�t�ۏؖ���FLG�����ݒ�t���O
            HiddenSetFuhoSyoumeisyoFlg.Value = EarthConst.ARI_VAL   '�����ݒ�L��

        End If

    End Sub

    ''' <summary>
    ''' �R���g���[���̉�ʐ���/�ۏ؏��Ĕ��s
    ''' </summary>
    ''' <remarks>�R���g���[���̉�ʐ�����d�l���̗D�揇�ɋL�ڂ���</remarks>
    Public Sub SetEnableControlSh()

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable

        htNotTarget.Add(TextShUriageNengappi.ID, TextShUriageNengappi) '����N����
        htNotTarget.Add(TextShHattyuusyoKakutei.ID, TextShHattyuusyoKakutei) '�������m��
        htNotTarget.Add(TextShHattyuusyoKingaku.ID, TextShHattyuusyoKingaku) '���������z
        htNotTarget.Add(TextShHattyuusyoKakuninDate.ID, TextShHattyuusyoKakuninDate) '�������m�F��

        jSM.Hash2Ctrl(TrShSaihakkou, EarthConst.MODE_VIEW, ht) '�Ĕ��sTR
        jSM.Hash2Ctrl(TrShSyouhin, EarthConst.MODE_VIEW, ht, htNotTarget) '���iTR


        '���D�揇1
        '���㏈����(�@�ʐ����e�[�u��.����v��FLG��1)
        If SpanShUriageSyorizumi.InnerHtml = EarthConst.URIAGE_ZUMI Then '�ύX�ӏ��Ȃ��̂��߁A��ʂ���擾
            cl.chgDispSyouhinText(TextSaihakkouRiyuu) '�Ĕ��s���R

            '���D�揇2
        ElseIf TextHosyousyoHakkouDate.Text.Length = 0 Then '�ۏ؏����s����������

            '���D�揇3
        ElseIf SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "1" Then '��񕥖ߐ\���L�����L

        Else
            '���D�揇4
            If TextSaihakkouDate.Text.Length = 0 Then '�Ĕ��s����������
                cl.chgDispSyouhinText(TextSaihakkouDate) '�Ĕ��s��

            Else

                '���D�揇5,6
                If SelectShSeikyuuUmu.SelectedValue = "0" Or SelectShSeikyuuUmu.SelectedValue = "" Then '���� ��or��
                    cl.chgDispSyouhinText(TextSaihakkouDate) '�Ĕ��s��
                    cl.chgDispSyouhinPull(SelectShSeikyuuUmu, SpanShSeikyuuUmu) '�����L��
                    cl.chgDispSyouhinText(TextSaihakkouRiyuu) '�Ĕ��s���R

                    '���D�揇7
                ElseIf SelectShSeikyuuUmu.SelectedValue = "1" Then '���� �L

                    cl.chgDispSyouhinText(TextSaihakkouDate) '�Ĕ��s��
                    cl.chgDispSyouhinPull(SelectShSeikyuuUmu, SpanShSeikyuuUmu) '�����L��
                    cl.chgDispSyouhinText(TextSaihakkouRiyuu) '�Ĕ��s���R

                    Dim kameitenlogic As New KameitenSearchLogic
                    Dim blnTorikesi As Boolean = False
                    Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                    '(*6)�����X������̏ꍇ�A������3�n��ȊO�̈����Ƃ���
                    If record.Torikesi <> 0 Then
                        record.KeiretuCd = ""
                    End If

                    If TextShSeikyuusaki.Text = EarthConst.SEIKYU_TASETU And cl.getKeiretuFlg(record.KeiretuCd) = False Then

                        cl.chgDispSyouhinText(TextShJituseikyuuKingaku) '�������Ŕ����z
                        cl.chgDispSyouhinText(TextShSeikyuusyoHakkouDate) '���������s��

                        '���D�揇8
                    Else
                        cl.chgDispSyouhinText(TextShJituseikyuuKingaku) '�������Ŕ����z
                        cl.chgDispSyouhinText(TextShSeikyuusyoHakkouDate) '���������s��
                        cl.chgDispSyouhinText(TextShKoumutenSeikyuuKingaku) '�H���X�����Ŕ����z
                    End If

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' �R���g���[���̉�ʐ���/��񕥖�
    ''' </summary>
    ''' <remarks>�R���g���[���̉�ʐ�����d�l���̗D�揇�ɋL�ڂ���</remarks>
    Public Sub SetEnableControlKy()

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable

        htNotTarget.Add(TextShUriageNengappi.ID, TextShUriageNengappi) '����N����
        htNotTarget.Add(TextShHattyuusyoKakutei.ID, TextShHattyuusyoKakutei) '�������m��
        htNotTarget.Add(TextShHattyuusyoKingaku.ID, TextShHattyuusyoKingaku) '���������z
        htNotTarget.Add(TextShHattyuusyoKakuninDate.ID, TextShHattyuusyoKakuninDate) '�������m�F��

        jSM.Hash2Ctrl(TdKyKaiyakuHaraimodosiSinseiUmu, EarthConst.MODE_VIEW, ht) '���.��񕥖ߐ\���L��
        jSM.Hash2Ctrl(TdKySeikyuusyoHakkouDate, EarthConst.MODE_VIEW, ht) '���.���������s��


        '���D�揇1
        '���㏈����(�@�ʐ����e�[�u��.����v��FLG��1)
        If SpanKyKaiyakuUriageSyorizumi.InnerHtml = EarthConst.URIAGE_ZUMI Then '�ύX�ӏ��Ȃ��̂��߁A��ʂ���擾

            '���D�揇2
        ElseIf TextHosyousyoHakkouDate.Text <> "" Then '�ۏ؏����s��������

        Else '���D�揇3

            '��񕥖ߐ\���L��
            If SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "1" Then '�L

                cl.chgDispSyouhinPull(SelectKyKaiyakuHaraimodosiSinseiUmu, SpanKyKaiyakuHaraimodosiSinseiUmu) '��񕥖ߐ\���L��
                cl.chgDispSyouhinText(TextKySeikyuusyoHakkouDate) '���������s��


                '���ۏ؍��ځE�񊈐�����3

                '��b�񍐏�
                jSM.Hash2Ctrl(TdKisoHoukokusyo, EarthConst.MODE_VIEW, ht)
                '�ۏ؏����s��
                jSM.Hash2Ctrl(TdHosyousyoHakkouJyoukyou, EarthConst.MODE_VIEW, ht)
                '�ۏ؏����s��
                jSM.Hash2Ctrl(TdHosyousyoHakkouDate, EarthConst.MODE_VIEW, ht)
                '�ۏ؊J�n��
                jSM.Hash2Ctrl(TdHosyouKaisiDate, EarthConst.MODE_VIEW, ht)
                '�ۏؗL��
                jSM.Hash2Ctrl(TdHosyouUmu, EarthConst.MODE_VIEW, ht)
                '�ۏ؊���
                jSM.Hash2Ctrl(TdHosyouKikan, EarthConst.MODE_VIEW, ht)
                '�ۏ؏��Ĕ��s�E�񊈐�����
                jSM.Hash2Ctrl(TrShSyouhin, EarthConst.MODE_VIEW, ht, htNotTarget) '�Ĕ��s.���iTR

                '���D�揇4
            ElseIf SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" Then '��

                cl.chgDispSyouhinPull(SelectKyKaiyakuHaraimodosiSinseiUmu, SpanKyKaiyakuHaraimodosiSinseiUmu) '��񕥖ߐ\���L��

                '���ۏ؍��ځ�3

                '��b�񍐏�
                cl.chgDispSyouhinPull(SelectKisoHoukokusyo, SpanKisoHoukokusyo)
                '�ۏ؏����s��
                cl.chgDispSyouhinPull(SelectHosyousyoHakkouJyoukyou, SpanHosyousyoHakkouJyoukyou)
                '�ۏ؏����s��
                cl.chgDispSyouhinText(TextHosyousyoHakkouDate)
                '�ۏ؊J�n��
                cl.chgDispSyouhinText(TextHosyouKaisiDate)
                '�ۏؗL��
                cl.chgDispSyouhinPull(SelectHosyouUmu, SpanHosyouUmu)
                '�ۏ؊���
                If TextHosyousyoHakkouDate.Text <> "" Then
                    cl.chgVeiwMode(TextHosyouKikan) ' �񊈐�
                Else
                    cl.chgDispSyouhinText(Me.TextHosyouKikan)
                End If

            End If
        End If


    End Sub

    ''' <summary>
    ''' �R���g���[���̉�ʐ���/�ۏ؏����s��
    ''' </summary>
    ''' <remarks>�R���g���[���̉�ʐ�����d�l���̗D�揇�ɋL�ڂ���</remarks>
    Public Sub SetEnableControlHhDate(Optional ByVal strBukkenJyky As String = "")

        Dim blnFlg As Boolean = False
        Dim vntNyuukinNo As Integer = 0 '�����m�F����NO
        Dim intKoujiFlg As Integer = 0 '�H������FLG
        Dim KisoSiyouLogic As New KisoSiyouLogic
        Dim hLogic As New HosyouLogic

        ' (1) ���i�ݒ��=�ۏ؏��i�Ȃ�
        If Me.SpanHosyouSyouhinUmu.InnerHtml = EarthConst.DISP_HOSYOU_NASI_HOSYOU_SYOUHIN_UMU Then
            ' (1)-(3) �� FALSE �Ȃ̂Ŗ����I�ɃZ�b�g�s�v 
        Else
            ' (3) ���=�����́@���@�ۏ؏��Ĕ��s=������i����ςłȂ��j
            If SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" And _
                        SpanShUriageSyorizumi.InnerHtml <> EarthConst.URIAGE_ZUMI Then

                ' (12) ���s�˗����L��=���ݒ�
                If SelectHakkouIraisyo.SelectedValue = "" Then
                    blnFlg = True '���͉�

                Else
                    ' �����m�F����NO�̎擾
                    vntNyuukinNo = hLogic.GetNyuukinKakuninJoukenNo(ucGyoumuKyoutuu.AccKameitenCd.Value, ucGyoumuKyoutuu.Kubun)

                    intKoujiFlg = 0 '�H������FLG

                    Dim record As KisoSiyouRecord = KisoSiyouLogic.GetKisoSiyouRec(HiddenHantei1CdOld.Value)
                    If record Is Nothing Then
                        intKoujiFlg = 0
                    Else
                        intKoujiFlg = record.KojHanteiFlg
                    End If

                    If intKoujiFlg <> 1 Then
                        ' (4)-(6)
                        '�H������<>1
                        Select Case vntNyuukinNo
                            Case 0, 4, 5
                                blnFlg = funChkPtnHosyousyoHakkouYMD("B")
                            Case 1, 3, 6
                                blnFlg = funChkPtnHosyousyoHakkouYMD("A")
                            Case 2
                                blnFlg = funChkPtnHosyousyoHakkouYMD("E")
                        End Select

                    Else '�H������FLG��1
                        ' (7)-(11)
                        '�H������=1 ���� ���ǍH����=���� ���� �H���񍐏��󗝓�=����
                        If TextKairyouKoujiDate.Text <> "" And TextKoujiHoukokusyoJuriDate.Text <> "" Then

                            Select Case vntNyuukinNo
                                Case 0, 4
                                    blnFlg = funChkPtnHosyousyoHakkouYMD("D")
                                Case 1, 6
                                    blnFlg = funChkPtnHosyousyoHakkouYMD("A")
                                Case 2
                                    blnFlg = funChkPtnHosyousyoHakkouYMD("F")
                                Case 3
                                    blnFlg = funChkPtnHosyousyoHakkouYMD("C")
                                Case 5
                                    blnFlg = funChkPtnHosyousyoHakkouYMD("B")
                            End Select

                        End If
                    End If

                End If
                ' (13) ������ = 1
                If strBukkenJyky = "1" Then
                    blnFlg = True
                End If
            End If
        End If

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        jSM.Hash2Ctrl(TdHosyousyoHakkouDate, EarthConst.MODE_VIEW, ht) '�ۏ؏����s��

        If blnFlg = True Then '���͉�
            '�ۏ؏����s��
            cl.chgDispSyouhinText(TextHosyousyoHakkouDate) '������
        End If

    End Sub

    ''' <summary>
    ''' �ۏ؏����s�����͐���p�`�F�b�N�p�^�[��
    ''' </summary>
    ''' <param name="strPat">�`�F�b�N�p�^�[��</param>
    ''' <returns>True/False (���͉�/���͕s��)</returns>
    ''' <remarks></remarks>
    Public Function funChkPtnHosyousyoHakkouYMD(ByVal strPat As String) As Boolean

        Select Case strPat
            Case "A"
                '�`�F�b�N�Ȃ�
                Return True

            Case "B"
                '��������c�z�`�F�b�N
                If TextTyousaZangaku.Text = "0" Then
                    '��������c�z=0
                    Return True
                End If

            Case "C"
                '�H������c�z�`�F�b�N
                If TextKoujiZangaku.Text = "0" Then
                    '�H������c�z=0
                    Return True
                End If

            Case "D"
                '��������c�z�`�F�b�N+�H������c�z�`�F�b�N
                If TextTyousaZangaku.Text = "0" And TextKoujiZangaku.Text = "0" Then
                    '��������c�z=0
                    Return True
                End If

            Case "E"
                '�����������m�F���`�F�b�N
                If HiddenTyousaHattyuusyoKakuninDateFlg.Value = "1" Then
                    Return True
                End If

            Case "F"
                '�����E�H���������m�F���`�F�b�N
                If HiddenTyousaHattyuusyoKakuninDateFlg.Value = "1" _
                    And HiddenKoujiHattyuusyoKakuninDateFlg.Value = "1" Then

                    Return True
                End If

            Case Else
                Return False
        End Select

        Return False

    End Function

#End Region

#Region "���z�ݒ�"

    ''' <summary>
    ''' ���z�ݒ�(�ۏ؏��Ĕ��s)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKingaku(ByVal emType As EnumKingakuType, Optional ByVal blnZeigaku As Boolean = False)
        ' �Ŕ����i�i���������z�j
        Dim zeinuki_ctrl As TextBox
        ' ����ŗ�
        Dim zeiritu_ctrl As HtmlInputHidden
        ' ����Ŋz
        Dim zeigaku_ctrl As TextBox
        ' �ō����z
        Dim zeikomi_gaku_ctrl As TextBox

        Select Case emType
            Case EnumKingakuType.Saihakkou '�Ĕ��s
                zeinuki_ctrl = TextShJituseikyuuKingaku
                zeiritu_ctrl = HiddenShZeiritu
                zeigaku_ctrl = TextShSyouhizei
                zeikomi_gaku_ctrl = TextShZeikomiKingaku

            Case EnumKingakuType.KaiyakuHaraimodosi '��񕥖�
                zeinuki_ctrl = TextKyJituseikyuuKingaku
                zeiritu_ctrl = HiddenKyZeiritu
                zeigaku_ctrl = TextKySyouhizei
                zeikomi_gaku_ctrl = TextKyZeikomiKingaku

            Case EnumKingakuType.None '�w��Ȃ�
                Exit Sub
            Case Else
                Exit Sub
        End Select

        Dim zeinuki As Integer = 0
        Dim zeiritu As Decimal = 0
        Dim zeigaku As Integer = 0
        Dim zeikomi_gaku As Integer = 0

        If zeinuki_ctrl.Text = "" Then '������
            zeinuki_ctrl.Text = ""
            zeigaku_ctrl.Text = ""
            zeikomi_gaku_ctrl.Text = ""

        Else '���͂���
            cl.SetDisplayString(CInt(zeinuki_ctrl.Text), zeinuki)
            cl.SetDisplayString(zeiritu_ctrl.Value, zeiritu)

            zeinuki = IIf(zeinuki = Integer.MinValue, 0, zeinuki)
            zeiritu = IIf(zeiritu = Decimal.MinValue, 0, zeiritu)

            If blnZeigaku Then '����Ŋz�̒l�Ōv�Z
                cl.SetDisplayString(CInt(zeigaku_ctrl.Text), zeigaku)
            Else
                zeigaku = Fix(zeinuki * zeiritu)
            End If
            zeikomi_gaku = zeinuki + zeigaku

            zeigaku_ctrl.Text = Format(zeigaku, EarthConst.FORMAT_KINGAKU_1) '�����
            zeikomi_gaku_ctrl.Text = Format(zeikomi_gaku, EarthConst.FORMAT_KINGAKU_1) '�ō����z

        End If

        Select Case emType
            Case EnumKingakuType.Saihakkou '�Ĕ��s
                ' �����z/�c�z���Z�b�g
                CalcZangakuSh(zeikomi_gaku_ctrl.Text)

            Case EnumKingakuType.KaiyakuHaraimodosi '��񕥖�
                Exit Sub
            Case EnumKingakuType.None '�w��Ȃ�
                Exit Sub
            Case Else
                Exit Sub
        End Select

    End Sub

    ''' <summary>
    ''' ���i�e�[�u�����z�G���A��0�N���A/�Ĕ��s
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub Clear0SyouhinTableSh()
        '���z��0�N���A
        '�������ݒ�
        TextShKoumutenSeikyuuKingaku.Text = "0" '�H���X�������z
        TextShJituseikyuuKingaku.Text = "0" '���������z
        TextShSyouhizei.Text = "0" '�����
        TextShZeikomiKingaku.Text = "0" '�ō����z
        TextShSeikyuusyoHakkouDate.Text = "" '���������s��
        TextShUriageNengappi.Text = Date.Now.ToString("yyyy/MM/dd") '����N����
        '�������m��
        If TextShHattyuusyoKakutei.Text.Length = 0 Then '(*5)�������m�肪�󔒂̏ꍇ�́A�u0�F���m��v��ݒ肷��
            TextShHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
        End If
    End Sub

    ''' <summary>
    ''' ���i�e�[�u�����z�G���A�̋󔒃N���A/�Ĕ��s
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearBlnkSyouhinTableSh()
        '�󔒃N���A
        '�������ݒ�
        SelectShSeikyuuUmu.SelectedValue = "" '�����L��
        SpanShSeikyuuUmu.Style.Add("display", "none") 'SPAN�����L��
        SpanShSeikyuuUmu.InnerHtml = "" 'SPAN�����L��

        TextShSyouhinCd.Text = "" '���i�R�[�h
        SpanShSyouhinMei.InnerHtml = "" '���i��
        TextShKoumutenSeikyuuKingaku.Text = "" '�H���X�������z
        TextShJituseikyuuKingaku.Text = "" '���������z
        TextShSyouhizei.Text = "" '�����
        TextShZeikomiKingaku.Text = "" '�ō����z
        TextShSeikyuusyoHakkouDate.Text = "" '���������s��
        TextShUriageNengappi.Text = "" '����N����
        TextShHattyuusyoKakutei.Text = "" '�������m��
        Me.HiddenShSiireSyouhiZei.Value = "" '�d������Ŋz
        Me.HiddenShSiireGaku.Value = ""     '�d�����z
        Me.HiddenShSyouhinCdOld.Value = "" ' ���i�R�[�h�ύX�O

        '���z�̍Čv�Z
        SetKingaku(EnumKingakuType.Saihakkou)

    End Sub

    ''' <summary>
    ''' ���i�e�[�u�����z�G���A�̋󔒃N���A/��񕥖�
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearBlnkSyouhinTableKy()
        '�󔒃N���A
        '�������ݒ�
        TextKySyouhinCd.Text = "" '���i�R�[�h
        TextKyKoumutenSeikyuuKingaku.Text = "" '�H���X�������z
        TextKyJituseikyuuKingaku.Text = "" '���������z
        TextKySeikyuusyoHakkouDate.Text = "" '���������s��
        TextKyUriageNengappi.Text = "" '����N����
        TextKyHattyuusyoKingaku.Text = "" '���������z
        TextKyHattyuusyoKakutei.Text = "" '�������m��
        TextKyHattyuusyoKakuninDate.Text = "" '�������m�F��

        SetKingaku(EnumKingakuType.KaiyakuHaraimodosi) '���z�̍Čv�Z

        '�󔒂ŃN���A
        TextKySyouhizei.Text = "" '�����
        TextKyZeikomiKingaku.Text = "" '�ō����z
        Me.HiddenKySiireSyouhiZei.Value = "" '�d������Ŋz
        Me.HiddenKySiireGaku.Value = ""     '�d�����z

    End Sub
#End Region

#Region "�ŗ�/�ŋ敪"

    ''' <summary>
    ''' �ŗ�/�ŋ敪��Hidden�ɃZ�b�g����/�ۏ؏��Ĕ��s
    ''' </summary>
    ''' <param name="strItemCd"></param>
    ''' <remarks></remarks>
    Protected Sub SetShZeiInfo(ByVal strItemCd As String)
        Dim syouhinRec As New Syouhin23Record

        '���i�����擾(�L�[:���i�R�[�h)
        syouhinRec = JibanLogic.GetSyouhinInfo(strItemCd, EarthEnum.EnumSyouhinKubun.Hosyousyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If Not syouhinRec Is Nothing Then
            HiddenShZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
            HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪
        End If
    End Sub

    ''' <summary>
    ''' �ŗ�/�ŋ敪��Hidden�ɃZ�b�g����/��񕥖�
    ''' </summary>
    ''' <param name="strItemCd"></param>
    ''' <remarks></remarks>
    Protected Sub SetKyZeiInfo(ByVal strItemCd As String)
        Dim syouhinRec As New Syouhin23Record

        '���i�����擾(�L�[:���i�R�[�h)
        syouhinRec = JibanLogic.GetSyouhinInfo(strItemCd, EarthEnum.EnumSyouhinKubun.Kaiyaku, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If Not syouhinRec Is Nothing Then
            HiddenKyZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
            HiddenKyZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪
        End If
    End Sub

#End Region

#Region "�����z/�c�z"

    ''' <summary>
    ''' �����z�E�c�z��\�����܂�<br/>
    ''' �ō�������z���v�̂ݕύX���A�Čv�Z���܂�<br/>
    ''' </summary>
    ''' <param name="strUriageGoukeiGaku">�ō�������z���v</param>
    ''' <remarks></remarks>
    Public Sub CalcZangakuSh(ByVal strUriageGoukeiGaku As String)

        ' �����z�i�ō��j
        Dim nyuukingaku_ctrl As TextBox
        ' �c�z
        Dim zangakuu_ctrl As TextBox

        nyuukingaku_ctrl = TextShNyuukingaku '�Ĕ��s.�����z
        zangakuu_ctrl = TextShZangaku '�Ĕ��s.�c�z

        If strUriageGoukeiGaku = "" Then
            ' �c�z
            zangakuu_ctrl.Text = "0"

        Else
            Dim uriageGoukeiGaku As Integer = Integer.MinValue

            uriageGoukeiGaku = CInt(strUriageGoukeiGaku)

            Dim strNyuukinGaku As String = IIf(nyuukingaku_ctrl.Text.Replace(",", "").Trim() = "", _
                                            "0", _
                                            nyuukingaku_ctrl.Text.Replace(",", "").Trim())

            Dim nyuukinGaku As Integer = Integer.Parse(strNyuukinGaku)

            ' NULL�͂O�ɂ���
            uriageGoukeiGaku = IIf(uriageGoukeiGaku = Integer.MinValue, 0, uriageGoukeiGaku)

            ' �c�z
            zangakuu_ctrl.Text = (uriageGoukeiGaku - nyuukinGaku).ToString(EarthConst.FORMAT_KINGAKU_1)

        End If

    End Sub

    ''' <summary>
    ''' �����z�E�c�z��\�����܂�
    ''' </summary>
    ''' <param name="uriageGoukeiGaku">�ō�������z���v</param>
    ''' <param name="nyuukinGaku">�����z</param>
    ''' <remarks></remarks>
    Public Sub CalcZangakuSh( _
                             ByVal uriageGoukeiGaku As Integer _
                            , ByVal nyuukinGaku As Integer _
                            )

        ' �����z�i�ō��j
        Dim nyuukingaku_ctrl As TextBox
        ' �c�z
        Dim zangakuu_ctrl As TextBox

        nyuukingaku_ctrl = TextShNyuukingaku '�Ĕ��s.�����z
        zangakuu_ctrl = TextShZangaku '�Ĕ��s.�c�z

        ' NULL�͂O�ɂ���
        uriageGoukeiGaku = IIf(uriageGoukeiGaku = Integer.MinValue, 0, uriageGoukeiGaku)
        nyuukinGaku = IIf(nyuukinGaku = Integer.MinValue, 0, nyuukinGaku)

        ' �����z
        nyuukingaku_ctrl.Text = nyuukinGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        ' �c�z
        zangakuu_ctrl.Text = (uriageGoukeiGaku - nyuukinGaku).ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub

#End Region

    ''' <summary>
    '''���i���̐����E�d���悪�ύX����Ă��Ȃ������`�F�b�N���A
    '''�ύX����Ă���ꍇ���̍Ď擾
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setSeikyuuSiireHenkou(ByVal sender As Object, ByVal e As System.EventArgs)
        '������A�d���悪�ύX���ꂽ�s���`�F�b�N���A���݂����ꍇ��
        '�e�s�̐����L���ύX���̏��������s����

        '**************************
        ' �ۏ؏��Ĕ��s
        '**************************
        '�����d���ύX�����NUC���̃`�F�b�N�t���OHidden���Q�Ƃ��A�t���O�������Ă���ꍇ�͏������{
        If Me.ucSeikyuuSiireLinkSai.AccHiddenChkSeikyuuSakiChg.Value = "1" Then

            '�����L���ύX������
            Me.SelectShSeikyuu_SelectedIndexChanged(sender, e)

            '�t���O������
            Me.ucSeikyuuSiireLinkSai.AccHiddenChkSeikyuuSakiChg.Value = String.Empty

            '�t�H�[�J�X�͐����d���ύX�����N
            setFocusAJ(Me.ucSeikyuuSiireLinkSai.AccLinkSeikyuuSiireHenkou)

            '�ύX���ꂽ���i���L�����ꍇ�AUpdatePanel��Update
            Me.UpdatePanelHall.Update()

        End If

        '**************************
        ' ��񕥖�
        '**************************
        '�����d���ύX�����NUC���̃`�F�b�N�t���OHidden���Q�Ƃ��A�t���O�������Ă���ꍇ�͏������{
        If Me.ucSeikyuuSiireLinkKai.AccHiddenChkSeikyuuSakiChg.Value = "1" Then

            '��񕥖ߕύX������
            Me.SelectKyKaiyakuHaraimodosiSinseiUmu_ChangeSub(sender, e)

            '�t���O������
            Me.ucSeikyuuSiireLinkKai.AccHiddenChkSeikyuuSakiChg.Value = String.Empty

            '�t�H�[�J�X�͐����d���ύX�����N
            setFocusAJ(Me.ucSeikyuuSiireLinkKai.AccLinkSeikyuuSiireHenkou)

            '�ύX���ꂽ���i���L�����ꍇ�AUpdatePanel��Update
            Me.UpdatePanelHall.Update()

        End If

    End Sub

#Region "���i�̎����ݒ�"

    ''' <summary>
    ''' ���i�̊�{�����Z�b�g����(�ۏ؏��Ĕ��s)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSyouhinInfoSh()
        '���ڐ����A�������̏����擾
        Dim syouhinRec As Syouhin23Record

        '���i�R�[�h/���i���̎����ݒ�
        syouhinRec = JibanLogic.GetSyouhinInfo("", EarthEnum.EnumSyouhinKubun.Hosyousyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If syouhinRec Is Nothing Then
            TextShSyouhinCd.Text = "" '���i�R�[�h
            SpanShSyouhinMei.InnerHtml = "" '���i��
            Me.HiddenShSyouhinCdOld.Value = ""
        Else
            TextShSyouhinCd.Text = cl.GetDispStr(syouhinRec.SyouhinCd) '���i�R�[�h
            SpanShSyouhinMei.InnerHtml = cl.GetDispStr(syouhinRec.SyouhinMei) '���i��
            HiddenShZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
            HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪

            '��ʏ�Ő����悪�w�肳��Ă���ꍇ�A���R�[�h�̐�������㏑��
            If Me.ucSeikyuuSiireLinkSai.AccSeikyuuSakiCd.Value <> String.Empty Then
                '����������R�[�h�ɃZ�b�g
                syouhinRec.SeikyuuSakiCd = Me.ucSeikyuuSiireLinkSai.AccSeikyuuSakiCd.Value
                syouhinRec.SeikyuuSakiBrc = Me.ucSeikyuuSiireLinkSai.AccSeikyuuSakiBrc.Value
                syouhinRec.SeikyuuSakiKbn = Me.ucSeikyuuSiireLinkSai.AccSeikyuuSakiKbn.Value
            End If
            Me.TextShSeikyuusaki.Text = syouhinRec.SeikyuuSakiType '��������

        End If

    End Sub

    ''' <summary>
    ''' ���i�̊�{�����Z�b�g����(��񕥖�)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSyouhinInfoKy()
        '���ڐ����A�������̏����擾
        Dim syouhinRec As Syouhin23Record

        '���i�R�[�h/���i���̎����ݒ�
        syouhinRec = JibanLogic.GetSyouhinInfo("", EarthEnum.EnumSyouhinKubun.Kaiyaku, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If syouhinRec Is Nothing Then
            TextKySyouhinCd.Text = "" '���i�R�[�h
        Else
            TextKySyouhinCd.Text = cl.GetDispStr(syouhinRec.SyouhinCd) '���i�R�[�h
            HiddenKyZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
            HiddenKyZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪

            '��ʏ�Ő����悪�w�肳��Ă���ꍇ�A���R�[�h�̐�������㏑��
            If Me.ucSeikyuuSiireLinkKai.AccSeikyuuSakiCd.Value <> String.Empty Then
                '����������R�[�h�ɃZ�b�g
                syouhinRec.SeikyuuSakiCd = Me.ucSeikyuuSiireLinkKai.AccSeikyuuSakiCd.Value
                syouhinRec.SeikyuuSakiBrc = Me.ucSeikyuuSiireLinkKai.AccSeikyuuSakiBrc.Value
                syouhinRec.SeikyuuSakiKbn = Me.ucSeikyuuSiireLinkKai.AccSeikyuuSakiKbn.Value
            End If
            Me.TextKySeikyuusaki.Text = syouhinRec.SeikyuuSakiType '��������
        End If

    End Sub

#End Region

#Region "�ۏ؏����s�󋵂ɂ��ۏؗL��"

    ''' <summary>
    ''' �ۏ؏����s�󋵂ɂ��ۏؗL���̕\���ؑ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChgDispHosyouUmu()
        Dim strHosyousyoHakJyky As String = Me.SelectHosyousyoHakkouJyoukyou.SelectedValue

        '���.�ۏ؏����s��=���͂̏ꍇ
        If strHosyousyoHakJyky <> String.Empty Then

            '���ۏ؏����s�󋵂ɂ��ۏؗL���̕\���ؑ�
            If cbLogic.GetHosyousyoHakJykyHosyouUmu(strHosyousyoHakJyky) = "0" Then
                Me.SpanHosyousyoHakJykyHosyouUmu.InnerHtml = EarthConst.DISP_HOSYOU_NASI_HOSYOUSYO_HAK_JYKY
                Me.SpanHosyousyoHakJykyHosyouUmu.Style("color") = CSS_COLOR_RED

            ElseIf cbLogic.GetHosyousyoHakJykyHosyouUmu(strHosyousyoHakJyky) = "1" Then
                Me.SpanHosyousyoHakJykyHosyouUmu.InnerHtml = EarthConst.DISP_HOSYOU_ARI_HOSYOUSYO_HAK_JYKY
                Me.SpanHosyousyoHakJykyHosyouUmu.Style("color") = CSS_COLOR_BLUE

            Else
                Me.SpanHosyousyoHakJykyHosyouUmu.InnerHtml = String.Empty

            End If

        Else '�����͂̏ꍇ

            '���ۏ؏��i�L���ɂ��ۏؗL���̕\���ؑ�
            If Me.SpanHosyouSyouhinUmu.InnerHtml = EarthConst.DISP_HOSYOU_NASI_HOSYOU_SYOUHIN_UMU Then
                Me.SpanHosyousyoHakJykyHosyouUmu.InnerHtml = EarthConst.DISP_HOSYOU_NASI_HOSYOUSYO_HAK_JYKY
                Me.SpanHosyousyoHakJykyHosyouUmu.Style("color") = CSS_COLOR_RED
            Else
                Me.SpanHosyousyoHakJykyHosyouUmu.InnerHtml = EarthConst.DISP_HOSYOU_ARI_HOSYOUSYO_HAK_JYKY
                Me.SpanHosyousyoHakJykyHosyouUmu.Style("color") = CSS_COLOR_BLUE
            End If
        End If


    End Sub
#End Region

End Class