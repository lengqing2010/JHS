Partial Public Class KairyouKouji
    Inherits System.Web.UI.Page

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim iraiSession As New IraiSession
    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager

    Dim cl As New CommonLogic
    Dim cbLogic As New CommonBizLogic
    Dim JibanLogic As New JibanLogic
    Dim kameitenlogic As New KameitenSearchLogic
    ''' <summary>
    ''' ���b�Z�[�W�N���X
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic

#Region "�v���p�e�B"

#Region "�p�����[�^/��������"
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

#Region "�p�����[�^/�����w��"
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _kbnCp As String
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrKbnCp() As String
        Get
            Return _kbnCp
        End Get
        Set(ByVal value As String)
            _kbnCp = value
        End Set
    End Property

    ''' <summary>
    ''' �ԍ�(�ۏ؏�No)
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _noCp As String
    ''' <summary>
    ''' �ԍ�(�ۏ؏�No)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrBangouCp() As String
        Get
            Return _noCp
        End Get
        Set(ByVal value As String)
            _noCp = value
        End Set
    End Property

    ''' <summary>
    ''' �R�s�[�t���O
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _copy As String
    ''' <summary>
    ''' �R�s�[�t���O
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrCopy() As String
        Get
            Return _copy
        End Get
        Set(ByVal value As String)
            _copy = value
        End Set
    End Property

    ''' <summary>
    ''' ���敪
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _motoKubun As String
    ''' <summary>
    ''' ���敪
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrMotoKubun() As String
        Get
            Return _motoKubun
        End Get
        Set(ByVal value As String)
            _motoKubun = value
        End Set
    End Property

    ''' <summary>
    ''' ���ԍ�
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _motoNo As String
    ''' <summary>
    ''' ���ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrMotoNo() As String
        Get
            Return _motoNo
        End Get
        Set(ByVal value As String)
            _motoNo = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' �����i�R�[�h
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _motoSyouhinCd As String
    ''' <summary>
    ''' �����i�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrMotoSyouhinCd() As String
        Get
            Return _motoSyouhinCd
        End Get
        Set(ByVal value As String)
            _motoSyouhinCd = value
        End Set
    End Property

    ''' <summary>
    ''' �揤�i�R�[�h
    ''' </summary>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Private _sakiSyouhinCd As String
    ''' <summary>
    ''' �揤�i�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property pStrSakiSyouhinCd() As String
        Get
            Return _sakiSyouhinCd
        End Get
        Set(ByVal value As String)
            _sakiSyouhinCd = value
        End Set
    End Property
#End Region

#Region "���ǍH����ʗp/�Œ�l"

    Private Const pStrKairyouKouji = "Kj"
    Private Const pStrTuikaKouji = "Tj"
    Private Const pStrHoukokusyo = "Kh"

    ''' <summary>
    ''' �H���^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Enum EnumKoujiType
        ''' <summary>
        ''' ���ǍH��
        ''' </summary>
        ''' <remarks></remarks>
        KairyouKouji = 0
        ''' <summary>
        ''' �ǉ��H��
        ''' </summary>
        ''' <remarks></remarks>
        TuikaKouji = 1
        ''' <summary>
        ''' ���ǍH���񍐏�
        ''' </summary>
        ''' <remarks></remarks>
        KairyouKoujiHoukokusyo = 2
    End Enum

    ''' <summary>
    ''' ���z�^�C�v
    ''' </summary>
    ''' <remarks>������zor�d�����z</remarks>
    Enum EnumKingakuType
        ''' <summary>
        ''' ������z
        ''' </summary>
        ''' <remarks></remarks>
        UriageKingaku = 0
        ''' <summary>
        ''' �d�����z
        ''' </summary>
        ''' <remarks></remarks>
        SiireKingaku = 1
        ''' <summary>
        ''' �w��Ȃ�
        ''' </summary>
        ''' <remarks></remarks>
        None = 2
    End Enum

#End Region

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X

        '�}�X�^�[�y�[�W�����擾�iScriptManager�p�j
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ���[�U�[��{�F��
        jBn.UserAuth(userinfo)

        '�F�،��ʂɂ���ĉ�ʕ\����ؑւ���
        If userinfo Is Nothing Then
            '���O�C����񂪖����ꍇ�A���C����ʂɔ�΂�
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If Context.Items("irai") IsNot Nothing Then
            iraiSession = Context.Items("irai")
        End If

        '�e�e�[�u���̕\����Ԃ�؂�ւ���
        Me.TBodyKairyouKoujiInfo.Style("display") = Me.HiddenKairyouKoujiInfoStyle.Value
        Me.TBodyKoujiHoukokusyoInfo.Style("display") = Me.HiddenKoujiHoukokusyoInfoStyle.Value

        If IsPostBack = False Then '�����N����

            ' Key����ێ�
            If Context.Items("kbn") IsNot Nothing Then
                '�o�^���s���ʍĕ`��p
                pStrKbn = Context.Items("kbn")
                pStrBangou = Context.Items("no")
                callModalFlg.Value = Context.Items("modal")
            Else
                '���������ق�
                pStrKbn = Request("sendPage_kubun")
                pStrBangou = Request("sendPage_hosyoushoNo")
            End If

            ' �p�����[�^�s�����͉�ʂ�\�����Ȃ�
            If pStrKbn Is Nothing Or pStrBangou Is Nothing Then
                Response.Redirect(UrlConst.MAIN)
            End If

            '****************************************************************************
            ' �h���b�v�_�E�����X�g�ݒ�
            '****************************************************************************
            Dim helper As New DropDownHelper

            '**************************
            ' ���ǍH������
            '**************************
            ' �H���S���҃R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectKoujiTantousya, DropDownHelper.DropDownType.Tantousya)

            '****************
            ' ���ǍH��
            '****************
            '���ǍH����ʃR���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectKjKairyouKoujiSyubetu, DropDownHelper.DropDownType.KairyouKoujiSyubetu)
            '���i�R�[�h�R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectKjSyouhinCd, DropDownHelper.DropDownType.SyouhinKouji)

            '****************
            ' �ǉ��H��
            '****************
            ' ���ǍH����ʃR���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectTjKairyouKoujiSyubetu, DropDownHelper.DropDownType.KairyouKoujiSyubetu)
            '���i�R�[�h�R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectTjSyouhinCd, DropDownHelper.DropDownType.SyouhinTuika)

            '**************************
            ' ���ǍH���񍐏�����
            '**************************
            ' �󗝃R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectKhJuri, DropDownHelper.DropDownType.HkksJuri)

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            SetDispAction()

            '�{�^�������C�x���g�̐ݒ�
            setBtnEvent()

            '****************************************************************************
            ' �n�Ճf�[�^�擾
            '****************************************************************************
            Dim jibanRec As New JibanRecordBase
            Dim jibanMoto As New JibanRecordBase
            jibanRec = JibanLogic.GetJibanData(pStrKbn, pStrBangou) '�n�Ճf�[�^�̎擾

            ' Key����ێ�(�����w��)
            pStrCopy = Request("copy")

            '�����w�肩��̃R�s�[��
            If Not pStrCopy Is Nothing AndAlso pStrCopy = "1" Then
                ' Key����ێ�(�����w��)
                pStrKbnCp = Request("kbn")
                pStrBangouCp = Request("no")
                pStrMotoKubun = Request("motokubun")
                pStrMotoNo = Request("motono")

                SetCopyFromJibanRec(jibanRec, jibanMoto) '�n�Ճf�[�^�̊Y���f�[�^���R�s�[
            End If

            If Not jibanRec Is Nothing Then
                '�n�Ճf�[�^�̓ǂݍ���
                iraiSession.JibanData = jibanRec

                SetCtrlFromJibanRec(sender, e, jibanRec) '�n�Ճf�[�^���R���g���[���ɃZ�b�g
            End If

            If ButtonTouroku1.Disabled = False Then
                ButtonTouroku1.Focus() '�o�^/�C���{�^���Ƀt�H�[�J�X
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
        '���i4
        cl.getSyouhin4MasterPath(ButtonSyouhin4, _
                                 userinfo, _
                                 Me.ucGyoumuKyoutuu.AccHiddenKubun.ClientID, _
                                 Me.ucGyoumuKyoutuu.AccBangou.ClientID, _
                                 Me.ucGyoumuKyoutuu.AccKameitenCd.ClientID, _
                                 Me.HiddenDefaultSiireSakiCdForLink.ClientID)

        '******************************
        '* ������/�d������̃Z�b�g
        '******************************
        Dim strUriageZumi As String = String.Empty    '���㏈���ςݔ��f�t���O�p
        Dim strViewMode As String = String.Empty

        '********************
        '* ���ǍH��
        '********************
        '���㏈���ϔ��f�t���O�̎擾
        strUriageZumi = cl.GetUriageSyoriZumiFlg(Me.SpanKjUriageSyoriZumi.InnerHtml)
        strViewMode = cl.GetViewMode(strUriageZumi, userinfo.KeiriGyoumuKengen)

        Me.ucSeikyuuSiireLinkKai.SetVariableValueCtrlFromParent(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                                    , Me.SelectKjSyouhinCd.SelectedValue _
                                                                    , Me.TextKjKoujiKaisyaCd.Text _
                                                                    , strUriageZumi _
                                                                    , Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked _
                                                                    , Me.TextKjKoujiKaisyaCd.Text _
                                                                    , _
                                                                    , strViewMode)

        '������^�C�v
        Me.TextKjSeikyuusaki.Text = Me.ucSeikyuuSiireLinkKai.SeikyuuSakiTypeStr

        '********************
        '* �ǉ��H��
        '********************
        '�\�����[�h�̏�����
        strViewMode = String.Empty

        '���㏈���ϔ��f�t���O�̎擾
        strUriageZumi = cl.GetUriageSyoriZumiFlg(Me.SpanTjUriageSyorizumi.InnerHtml)
        strViewMode = cl.GetViewMode(strUriageZumi, userinfo.KeiriGyoumuKengen)

        If Me.SelectTjSyouhinCd.Style("display") = "none" Then
            strViewMode = EarthConst.MODE_VIEW
        Else
            strViewMode = String.Empty
        End If

        Me.ucSeikyuuSiireLinkTui.SetVariableValueCtrlFromParent(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                                    , Me.SelectTjSyouhinCd.SelectedValue _
                                                                    , Me.TextTjKoujiKaisyaCd.Text _
                                                                    , strUriageZumi _
                                                                    , Me.CheckBoxTjKoujiKaisyaSeikyuu.Checked _
                                                                    , Me.TextTjKoujiKaisyaCd.Text _
                                                                    , _
                                                                    , strViewMode)

        '������^�C�v
        Me.TextTjSeikyuusaki.Text = Me.ucSeikyuuSiireLinkTui.SeikyuuSakiTypeStr

        Me.UpdatePanelKairyouKoujiInfo.Update()

        '********************
        '* �H���񍐏��Ĕ��s
        '********************
        '�\�����[�h�̏�����
        strViewMode = String.Empty

        '���㏈���ϔ��f�t���O�̎擾
        strUriageZumi = cl.GetUriageSyoriZumiFlg(Me.SpanKhUriageSyorizumi.InnerHtml)
        strViewMode = cl.GetViewMode(strUriageZumi, userinfo.KeiriGyoumuKengen)

        If Me.SelectKhSeikyuuUmu.Style("display") = "none" Then
            strViewMode = EarthConst.MODE_VIEW
        Else
            strViewMode = String.Empty
        End If

        '�H���񍐏�
        Me.ucSeikyuuSiireLink.SetVariableValueCtrlFromParent(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                                    , Me.TextKhSyouhinCd.Text _
                                                                    , Me.HiddenDefaultSiireSakiCdForLink.Value _
                                                                    , strUriageZumi _
                                                                    , _
                                                                    , _
                                                                    , _
                                                                    , strViewMode)

        '�����悪�w�肳��Ă����ꍇ�A���ǍH����А����̃`�F�b�N���g�p�s�Ƃ���
        If Me.ucSeikyuuSiireLinkKai.AccSeikyuuSakiCd.Value <> String.Empty _
        Or Me.ucSeikyuuSiireLinkKai.AccSeikyuuSakiBrc.Value <> String.Empty _
        Or Me.ucSeikyuuSiireLinkKai.AccSeikyuuSakiKbn.Value <> String.Empty Then
            Me.CheckBoxKjKoujiKaisyaSeikyuu.Enabled = False
        Else
            Me.CheckBoxKjKoujiKaisyaSeikyuu.Enabled = True
        End If

        '�����悪�w�肳��Ă����ꍇ�A�ǉ��H����А����̃`�F�b�N���g�p�s�Ƃ���
        If Me.ucSeikyuuSiireLinkTui.AccSeikyuuSakiCd.Value <> String.Empty _
        Or Me.ucSeikyuuSiireLinkTui.AccSeikyuuSakiBrc.Value <> String.Empty _
        Or Me.ucSeikyuuSiireLinkTui.AccSeikyuuSakiKbn.Value <> String.Empty Then
            Me.CheckBoxTjKoujiKaisyaSeikyuu.Enabled = False
        Else
            Me.CheckBoxTjKoujiKaisyaSeikyuu.Enabled = True
        End If

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

        '�o�^����MSG�m�F��AOK�̏ꍇDB�X�V�������s�Ȃ�
        ButtonTouroku1.Attributes("onclick") = tmpScript
        ButtonTouroku2.Attributes("onclick") = tmpScript

    End Sub

    ''' <summary>
    ''' �Ɩ�����[���[�U�[�R���g���[��]���[�h������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ucGyoumuKyoutuu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ucGyoumuKyoutuu.Load

        '�ŏI�X�V�ҁA�ŏI�X�V�������Z�b�g
        TextSaisyuuKousinSya.Text = ucGyoumuKyoutuu.AccLastupdateusernm.Value
        TextSaisyuuKousinDate.Text = ucGyoumuKyoutuu.AccLastupdatedatetime.Value

        '�����N�����̂�
        If IsPostBack = False Then
            '��ʐ���
            SetEnableControlInitKj() '���ǍH���E�����N��
            SetEnableControlKj() '���ǍH��
            SetEnableControlInitTj() '�ǉ��H���E�����N��
            SetEnableControlTj() '�ǉ��H��
            SetEnableControlKh() '���ǍH���񍐏�

        End If

        '�H���Ɩ�����
        If userinfo.KoujiGyoumuKengen = 0 Then
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable
            htNotTarget.Add(ucGyoumuKyoutuu.AccDataHakiSyubetu.ID, ucGyoumuKyoutuu.AccDataHakiSyubetu) '�f�[�^�j�����
            htNotTarget.Add(ucGyoumuKyoutuu.AccDataHakiDate.ID, ucGyoumuKyoutuu.AccDataHakiDate) '�f�[�^�j����
            jSM.Hash2Ctrl(UpdatePanelKairyouKouji, EarthConst.MODE_VIEW, ht, htNotTarget)

            ButtonKjKoujiKaisyaSearch.Style("display") = "none"
            ButtonTjKoujiKaisyaSearch.Style("display") = "none"

            '�o�^�{�^��
            ButtonTouroku1.Disabled = True
            ButtonTouroku2.Disabled = True
        End If

        '�j������
        If userinfo.DataHakiKengen = 0 Then
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable
            jSM.Hash2Ctrl(ucGyoumuKyoutuu.AccTdDataHakiSyubetu, EarthConst.MODE_VIEW, ht) '�f�[�^�j�����
            jSM.Hash2Ctrl(ucGyoumuKyoutuu.AccTdDataHakiData, EarthConst.MODE_VIEW, ht) '�f�[�^�j����

        End If

        '���ʏ��̓��̓`�F�b�N
        If ucGyoumuKyoutuu.AccKameitenCd.Value = "" Then '�����X�R�[�h
            Dim tmpScript As String = ""

            '�o�^�{�^���̔񊈐���
            ButtonTouroku1.Disabled = True
            ButtonTouroku2.Disabled = True

            tmpScript = "alert('" & Messages.MSG065W & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ucGyoumuKyoutuu_Load", tmpScript, True)

            '�n�Չ�ʋ��ʃN���X
            Dim noTarget As New Hashtable
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable

            '�S�ẴR���g���[���𖳌���
            jSM.Hash2Ctrl(UpdatePanelKairyouKouji, EarthConst.MODE_VIEW, ht, htNotTarget)
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

    ''' <summary>
    ''' �n�Ճ��R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="jr">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)
        Dim jSM As New JibanSessionManager '�Z�b�V�����Ǘ��N���X
        Dim jBn As New Jiban '�n�Չ�ʃN���X

        Dim blnTorikesi As Boolean = False '����t���O(=False)
        Dim kisoSiyouLogic As New KisoSiyouLogic
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic

        ' �R���{�ݒ�w���p�[�N���X�𐶐�
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        '******************************************
        '* ��ʃR���g���[���ɐݒ�y���ǍH�����z
        '******************************************
        '�����X�R�[�h
        Me.HiddenKameitenCd.Value = cl.GetDisplayString(jr.KameitenCd)

        If jr.TysKaisyaCd <> "" And jr.TysKaisyaJigyousyoCd <> "" Then
            TextTyousaKaisyaMei.Text = tyousakaisyaSearchLogic.GetTyousaKaisyaMei(jr.TysKaisyaCd, jr.TysKaisyaJigyousyoCd, False) '�������
        End If
        TextTyousaJissiDate.Text = cl.GetDispStr(jr.TysJissiDate) '�������{��
        TextKeikakusyoSakuseiDate.Text = cl.GetDispStr(jr.KeikakusyoSakuseiDate) '�v�揑�쐬��

        '���_�~�[�R���{�ɃZ�b�g
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Tantousya, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(jr.TantousyaCd, "")
        TextHanteisya.Text = objDrpTmp.SelectedItem.Text '�����

        '���݃`�F�b�N
        If cl.ChkDropDownList(SelectKoujiTantousya, cl.GetDispNum(jr.SyouninsyaCd)) Then
            TextKoujiTantousyaCd.Text = cl.GetDispNum(jr.SyouninsyaCd, "") '�H���S���҃R�[�h
            SelectKoujiTantousya.SelectedValue = TextKoujiTantousyaCd.Text '�H���S����
        ElseIf jr.SyouninsyaCd > 0 Then
            TextKoujiTantousyaCd.Text = cl.GetDispNum(jr.SyouninsyaCd, "") '�H���S���҃R�[�h
            SelectKoujiTantousya.Items.Add(New ListItem(TextKoujiTantousyaCd.Text & ":" & jr.SyouninsyaMei, TextKoujiTantousyaCd.Text)) '�H���S����
            SelectKoujiTantousya.SelectedValue = TextKoujiTantousyaCd.Text  '�I�����
        End If

        SpanHantei1.InnerHtml = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(jr.HanteiCd1)) '����P��

        '���_�~�[�R���{�ɃZ�b�g
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.KsSiyouSetuzokusi, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(jr.HanteiSetuzokuMoji, "")
        SpanHanteiSetuzokuMoji.InnerHtml = objDrpTmp.SelectedItem.Text '����ڑ�����

        SpanHantei2.InnerHtml = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(jr.HanteiCd2)) '����Q��

        '******************************************
        '* ��ʃR���g���[���ɐݒ�y���ǍH���z
        '******************************************
        SelectKjKoujiSiyouKakunin.SelectedValue = cl.GetDispNum(jr.KojSiyouKakunin, "") '�H���d�l�m�F
        TextKjKakuninDate.Text = cl.GetDispStr(jr.KojSiyouKakuninDate) '�m�F��
        TextKjKoujiKaisyaCd.Text = cl.GetDispStr(jr.KojGaisyaCd & jr.KojGaisyaJigyousyoCd) '�H����ЃR�[�h
        HiddenKjKojKaisyaCd.Value = cl.GetDispStr(jr.KojGaisyaCd & jr.KojGaisyaJigyousyoCd) '�H����ЃR�[�h(Hidden)
        If jr.KojGaisyaCd <> "" And jr.KojGaisyaJigyousyoCd <> "" Then
            TextKjKoujiKaisyaMei.Text = tyousakaisyaSearchLogic.GetTyousaKaisyaMei(jr.KojGaisyaCd, jr.KojGaisyaJigyousyoCd, False) '�H����Ж�
        End If
        CheckBoxKjKoujiKaisyaSeikyuu.Checked = IIf(jr.KojGaisyaSeikyuuUmu = 1, True, False) '�H����А���
        SelectKjKairyouKoujiSyubetu.SelectedValue = cl.GetDispNum(jr.KairyKojSyubetu, "") '���ǍH�����
        TextKjKanryouYoteiDate.Text = cl.GetDispStr(jr.KairyKojKanryYoteiDate) '�����\���

        TextKjKoujiDate.Text = cl.GetDispStr(jr.KairyKojDate) '�H����
        TextKjKankouSokuhouTyakuDate.Text = cl.GetDispStr(jr.KairyKojSokuhouTykDate) '���H���񒅓�

        '���ǍH���̓@�ʐ������R�[�h������ꍇ
        If Not jr.KairyouKoujiRecord Is Nothing Then

            '�@�ʐ��������R���g���[���ɃZ�b�g
            SetCtrlTeibetuSeikyuuDataKj(jr.KairyouKoujiRecord)

            '�@�ʓ��������R���g���[���ɃZ�b�g
            If jr.TeibetuNyuukinRecords IsNot Nothing Then
                ' �����z/�c�z���Z�b�g
                CalcZangaku(EnumKoujiType.KairyouKouji, jr.getZeikomiGaku(New String() {"130"}), jr.getNyuukinGaku("130"))
            Else
                ' �����z/�c�z���Z�b�g
                SetKingakuUriage(EnumKoujiType.KairyouKouji, True)
            End If

        End If

        '�R�s�[���̎����ݒ�(���������s���A����N����)
        Me.ChkOnCopyAutoSetting(jr.KameitenCd, jr.Kbn)

        '******************************************
        '* ��ʃR���g���[���ɐݒ�y�ǉ��H���z
        '******************************************
        TextTjKoujiKaisyaCd.Text = cl.GetDispStr(jr.TKojKaisyaCd & jr.TKojKaisyaJigyousyoCd) '�H����ЃR�[�h
        HiddenTjKojKaisyaCd.Value = cl.GetDispStr(jr.TKojKaisyaCd & jr.TKojKaisyaJigyousyoCd) '�H����ЃR�[�h(Hidden)
        If jr.TKojKaisyaCd <> "" And jr.TKojKaisyaJigyousyoCd <> "" Then
            TextTjKoujiKaisyaMei.Text = tyousakaisyaSearchLogic.GetTyousaKaisyaMei(jr.TKojKaisyaCd, jr.TKojKaisyaJigyousyoCd, False) '�H����Ж�
        End If
        CheckBoxTjKoujiKaisyaSeikyuu.Checked = IIf(jr.TKojKaisyaSeikyuuUmu = 1, True, False) '�H����А���

        SelectTjKairyouKoujiSyubetu.SelectedValue = cl.GetDispNum(jr.TKojSyubetu, "") '���ǍH�����

        TextTjKanryouYoteiDate.Text = cl.GetDispStr(jr.TKojKanryYoteiDate) '�����\���

        TextTjKoujiDate.Text = cl.GetDispStr(jr.TKojDate) '�H����
        TextTjKankouSokuhouTyakuDate.Text = cl.GetDispStr(jr.TKojSokuhouTykDate) '���H���񒅓�

        '�ǉ��H���̓@�ʐ������R�[�h������ꍇ
        If Not jr.TuikaKoujiRecord Is Nothing Then

            '�@�ʐ��������R���g���[���ɃZ�b�g
            SetCtrlTeibetuSeikyuuDataTj(jr.TuikaKoujiRecord)

            '�@�ʓ��������R���g���[���ɃZ�b�g
            If jr.TeibetuNyuukinRecords IsNot Nothing Then
                ' �����z/�c�z���Z�b�g
                CalcZangaku(EnumKoujiType.TuikaKouji, jr.getZeikomiGaku(New String() {"140"}), jr.getNyuukinGaku("140"))
            Else
                ' �����z/�c�z���Z�b�g
                SetKingakuUriage(EnumKoujiType.TuikaKouji, True)
            End If

        End If

        '******************************************
        '* ��ʃR���g���[���ɐݒ�y���ǍH���񍐏����z
        '******************************************
        SelectKhJuri.SelectedValue = cl.GetDispNum(jr.KojHkksUmu, "") '��
        TextKhJuriSyousai.Text = cl.GetDispStr(jr.KojHkksJuriSyousai) '�󗝏ڍ�
        TextKhJuriDate.Text = cl.GetDispStr(jr.KojHkksJuriDate) '�󗝓�
        TextKhHassouDate.Text = cl.GetDispStr(jr.KojHkksHassouDate) '������
        TextKhSaihakkouDate.Text = cl.GetDispStr(jr.KojHkksSaihakDate) '�Ĕ��s��

        '�H���񍐏���񂪂���ꍇ
        If Not jr.KoujiHoukokusyoRecord Is Nothing Then

            '�@�ʐ��������R���g���[���ɃZ�b�g
            SetCtrlTeibetuSeikyuuDataKh(jr.KoujiHoukokusyoRecord)

            '�@�ʓ��������R���g���[���ɃZ�b�g
            If jr.TeibetuNyuukinRecords IsNot Nothing Then
                ' �����z/�c�z���Z�b�g
                CalcZangaku(EnumKoujiType.KairyouKoujiHoukokusyo, jr.getZeikomiGaku(New String() {"160"}), jr.getNyuukinGaku("160"))
            Else
                ' �����z/�c�z���Z�b�g
                SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo, True)
            End If

        End If

        '****************************
        '* Hidden����
        '****************************
        HiddenHantei1Cd.Value = cl.GetDispNum(jr.HanteiCd1, "") '����1�R�[�h
        HiddenHanteiSetuzokuMoji.Value = cl.GetDispNum(jr.HanteiSetuzokuMoji, "") '����ڑ�����
        HiddenHantei2Cd.Value = cl.GetDispNum(jr.HanteiCd2, "") '����2�R�[�h

        If jr.TysKaisyaCd <> "" And jr.TysKaisyaJigyousyoCd <> "" Then
            HiddenDefaultSiireSakiCdForLink.Value = jr.TysKaisyaCd & jr.TysKaisyaJigyousyoCd '������ЃR�[�h
        End If

        HiddenKjSyouhinCdOld.Value = SelectKjSyouhinCd.SelectedValue '���ǍH��.���i�R�[�hOld
        HiddenKjUriageNengappiOld.Value = TextKjUriageNengappi.Text '���ǍH��.����N����Old

        HiddenTyousaGaiyou.Value = cl.GetDispNum(jr.TysGaiyou, "") '�����T�v

        Me.HiddenHosyouSyouhinUmuOld.Value = cl.GetDisplayString(jr.HosyouSyouhinUmu) '�ۏ؏��i�L��

        Me.HiddenKjKoujiDateOld.Value = Me.TextKjKoujiDate.Text '���ǍH���E�H����Old
        Me.HiddenKjKankouSokuhouTyakuDateOld.Value = Me.TextKjKankouSokuhouTyakuDate.Text '���ǍH���E���H���񒅓�Old
        Me.HiddenTjKoujiDateOld.Value = Me.TextTjKoujiDate.Text '�ǉ��H���E�H����Old
        Me.HiddenTjKankouSokuhouTyakuDateOld.Value = Me.TextTjKankouSokuhouTyakuDate.Text '�ǉ��H���E���H���񒅓�Old

        '****************************
        '* Hidden����(�R���g���[���̒l�ύX�O)
        '****************************
        HiddenKjKoujiKaisyaCdMae.Value = TextKjKoujiKaisyaCd.Text '���ǍH���E�H����ЃR�[�h�ύX�O
        HiddenTjKoujiKaisyaCdMae.Value = TextTjKoujiKaisyaCd.Text '�ǉ��H���E�H����ЃR�[�h�ύX�O
        HiddenKjSyouhinCdMae.Value = SelectKjSyouhinCd.SelectedValue '���ǍH���E���i�R�[�h�ύX�O
        HiddenTjSyouhinCdMae.Value = SelectTjSyouhinCd.SelectedValue '�ǉ��H���E���i�R�[�h�ύX�O
        HiddenKjKairyouKoujiSyubetuMae.Value = SelectKjKairyouKoujiSyubetu.SelectedValue '���ǍH���E���ǎ�ʕύX�O
        HiddenTjKairyouKoujiSyubetuMae.Value = SelectTjKairyouKoujiSyubetu.SelectedValue '�ǉ��H���E���ǎ�ʕύX�O

        HiddenKhHassouDateMae.Value = TextKhHassouDate.Text '���ǍH���񍐏��E�������O
        HiddenKhSaihakkouDateMae.Value = TextKhSaihakkouDate.Text '���ǍH���񍐏��E�Ĕ��s���O
        HiddenKhSeikyuuUmuMae.Value = SelectKhSeikyuuUmu.SelectedValue '���ǍH���񍐏��E�����L���O

        '****************************
        '* Hidden����(�o�^���m�F)
        '****************************
        '���������s���ύX������
        HiddenKjSeikyuusyoHakkouDateMsg1.Value = "" '���ǍH��1
        HiddenKjSeikyuusyoHakkouDateMsg2.Value = "" '���ǍH��2
        HiddenTjSeikyuusyoHakkouDateMsg1.Value = "" '�ǉ��H��1
        HiddenTjSeikyuusyoHakkouDateMsg2.Value = "" '�ǉ��H��2

        'Chk05
        HiddenKjChk05.Value = "" '���ǍH��
        HiddenTjChk05.Value = "" '�ǉ��H��

        'Chk14
        '���㏈���ςł͂Ȃ��A���H���񒅓��������͂̏ꍇ�A�V�K�o�^�t���O�����Ă�
        If SpanKjUriageSyoriZumi.InnerHtml <> EarthConst.URIAGE_ZUMI And TextKjKankouSokuhouTyakuDate.Text = "" Then
            HiddenKjChk14.Value = "0"
        Else
            HiddenKjChk14.Value = "1"
        End If
        'Chk15
        '���㏈���ςł͂Ȃ��A���H���񒅓��������͂̏ꍇ�A�V�K�o�^�t���O�����Ă�
        If SpanTjUriageSyorizumi.InnerHtml <> EarthConst.URIAGE_ZUMI And TextTjKankouSokuhouTyakuDate.Text = "" Then
            HiddenTjChk15.Value = "0"
        Else
            HiddenTjChk15.Value = "1"
        End If

        'Chk08
        HiddenKjChk08.Value = ""
        'Chk09
        HiddenKjChk09.Value = ""
        'Chk10
        HiddenTjChk10.Value = ""
        'Chk11
        HiddenTjChk11.Value = ""

        '****************************
        '* �Z�b�V�����ɉ�ʏ����i�[
        '****************************
        jSM.Ctrl2Hash(Me, jBn.IraiData)
        iraiSession.Irai1Data = jBn.IraiData

    End Sub

    ''' <summary>
    ''' �n�Ճf�[�^�̃R�s�[���s�Ȃ�
    ''' </summary>
    ''' <param name="jibanRec">�n�Ճf�[�^[�R�s�[��]</param>
    ''' <param name="jibanMoto">�n�Ճf�[�^[�R�s�[��]</param>
    ''' <remarks></remarks>
    Private Sub SetCopyFromJibanRec(ByRef jibanRec As JibanRecordBase, ByRef jibanMoto As JibanRecordBase)
        Dim logic As New JibanLogic

        jibanMoto = logic.GetJibanData(pStrMotoKubun, pStrMotoNo) '�n�Ճf�[�^�̎擾(�R�s�[��)

        '�C���X�^���X��
        If jibanRec.KairyouKoujiRecord Is Nothing Then '��
            jibanRec.KairyouKoujiRecord = New TeibetuSeikyuuRecord
        End If
        If jibanMoto.KairyouKoujiRecord Is Nothing Then '��
            jibanMoto.KairyouKoujiRecord = New TeibetuSeikyuuRecord
        End If

        If jibanRec.KairyouKoujiRecord.HattyuusyoGaku = 0 Or jibanRec.KairyouKoujiRecord.HattyuusyoGaku = Integer.MinValue Then
        Else
            '�R�s�[��.���������z=���͂���A���A�R�s�[��.���i�R�[�h=������
            If jibanMoto.KairyouKoujiRecord.SyouhinCd = String.Empty Then
                Exit Sub
            End If
        End If

        '****************************
        '* �n�Ճf�[�^
        '****************************
        jibanRec.SyouninsyaCd = jibanMoto.SyouninsyaCd '�H���S����.�R�[�h
        jibanRec.SyouninsyaMei = jibanMoto.SyouninsyaMei '�H���S����.�S���Җ�
        jibanRec.KojSiyouKakunin = jibanMoto.KojSiyouKakunin '<���ǍH��>�H���d�l�m�F
        jibanRec.KojSiyouKakuninDate = jibanMoto.KojSiyouKakuninDate '<���ǍH��>�m�F��
        jibanRec.KojGaisyaCd = jibanMoto.KojGaisyaCd '<���ǍH��>�H����ЃR�[�h
        jibanRec.KojGaisyaJigyousyoCd = jibanMoto.KojGaisyaJigyousyoCd '<���ǍH��>�H����Ў��Ə��R�[�h
        jibanRec.KojGaisyaSeikyuuUmu = jibanMoto.KojGaisyaSeikyuuUmu '<���ǍH��>�H����А���
        jibanRec.KairyKojSyubetu = jibanMoto.KairyKojSyubetu '<���ǍH��>���ǍH�����
        jibanRec.KairyKojKanryYoteiDate = jibanMoto.KairyKojKanryYoteiDate '<���ǍH��>�����\���

        '****************************
        '* �@�ʐ����f�[�^(���ǍH��)
        '****************************
        '���i�R�[�h�̑ޔ�
        pStrMotoSyouhinCd = jibanMoto.KairyouKoujiRecord.SyouhinCd
        pStrSakiSyouhinCd = jibanRec.KairyouKoujiRecord.SyouhinCd

        jibanRec.KairyouKoujiRecord.SyouhinCd = jibanMoto.KairyouKoujiRecord.SyouhinCd                      '<���ǍH��>���i�R�[�h
        jibanRec.KairyouKoujiRecord.SeikyuuUmu = jibanMoto.KairyouKoujiRecord.SeikyuuUmu                    '<���ǍH��>����
        jibanRec.KairyouKoujiRecord.UriGaku = jibanMoto.KairyouKoujiRecord.UriGaku                          '<���ǍH��><������z>�Ŕ����z
        jibanRec.KairyouKoujiRecord.ZeiKbn = jibanMoto.KairyouKoujiRecord.ZeiKbn                            '�ŋ敪
        jibanRec.KairyouKoujiRecord.Zeiritu = jibanMoto.KairyouKoujiRecord.Zeiritu                          '�ŗ�
        jibanRec.KairyouKoujiRecord.UriageSyouhiZeiGaku = jibanMoto.KairyouKoujiRecord.UriageSyouhiZeiGaku  '����Ŋz
        jibanRec.KairyouKoujiRecord.SiireGaku = jibanMoto.KairyouKoujiRecord.SiireGaku                      '<���ǍH��><�d�����z>�Ŕ����z
        jibanRec.KairyouKoujiRecord.SiireSyouhiZeiGaku = jibanMoto.KairyouKoujiRecord.SiireSyouhiZeiGaku    '�d������Ŋz

    End Sub

#Region "���i�ݒ�"

    ''' <summary>
    ''' �����ݒ�/���i���(���i�R�[�h���W�����i�A�ŋ敪�A�ŗ���ݒ肷��)
    ''' </summary>
    ''' <param name="emType"></param>
    ''' <remarks></remarks>
    Private Sub SetSyouhinInfo(ByVal emType As EnumKoujiType)
        Dim syouhinRec As New Syouhin23Record
        Dim tmpErrMsg As String = String.Empty
        Dim tmpScript As String = String.Empty

        '�H���^�C�v
        Select Case emType
            Case EnumKoujiType.KairyouKouji '���ǍH��

                '���i�R�[�h/���i���̎����ݒ�
                syouhinRec = JibanLogic.GetSyouhinInfo(SelectKjSyouhinCd.SelectedValue, EarthEnum.EnumSyouhinKubun.KairyouKouji, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                If syouhinRec Is Nothing Then
                    SelectKjSyouhinCd.SelectedValue = "" '���i�R�[�h
                    HiddenKjZeiritu.Value = "" '�ŗ�
                    HiddenKjZeiKbn.Value = "" '�ŋ敪

                    SelectKjSeikyuuUmu.SelectedValue = "" '����
                    TextKjUriageNengappi.Text = "" '����N����
                    TextKjUriageZeinukiKingaku.Text = "" '������z/�Ŕ����z
                    TextKjUriageSyouhizei.Text = "" '������z/�����
                    TextKjUriageZeikomiKingaku.Text = "" '������z/�ō����z
                    TextKjSiireZeinukiKingaku.Text = "" '�d�����z/�Ŕ����z
                    TextKjSiireSyouhizei.Text = "" '�d�����z/�����
                    TextKjSiireZeikomiKingaku.Text = "" '�d�����z/�ō����z
                    TextKjZangaku.Text = "0" '�c�z
                    TextKjSeikyuusyoHakkouDate.Text = "" '���������s��
                    TextKjHattyuusyoKakutei.Text = "" '�������m��

                    '���G���[���b�Z�[�W�\��
                    tmpErrMsg = Messages.MSG163E.Replace("@PARAM1", "���i�}�X�^")
                    tmpScript = "alert('" & tmpErrMsg & "');"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetSyouhinInfo", tmpScript, True)

                Else
                    HiddenKjZeiritu.Value = cl.GetDispStr(syouhinRec.Zeiritu) '�ŗ�
                    HiddenKjZeiKbn.Value = cl.GetDispStr(syouhinRec.ZeiKbn) '�ŋ敪

                    '�����L��
                    If SelectKjSeikyuuUmu.SelectedValue = "1" Then '�L
                        TextKjUriageZeinukiKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1) '������z/�Ŕ����z
                    Else '��
                        TextKjUriageZeinukiKingaku.Text = "0" '������z/�Ŕ����z
                    End If

                End If

            Case EnumKoujiType.TuikaKouji '�ǉ��H��

                '���i�R�[�h/���i���̎����ݒ�
                syouhinRec = JibanLogic.GetSyouhinInfo(SelectTjSyouhinCd.SelectedValue, EarthEnum.EnumSyouhinKubun.TuikaKouji, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                If syouhinRec Is Nothing Then
                    SelectTjSyouhinCd.SelectedValue = "" '���i�R�[�h
                    HiddenTjZeiritu.Value = "" '�ŗ�
                    HiddenTjZeiKbn.Value = "" '�ŋ敪

                    SelectTjSeikyuuUmu.SelectedValue = "" '����
                    TextTjUriageNengappi.Text = "" '����N����
                    TextTjUriageZeinukiKingaku.Text = "" '������z/�Ŕ����z
                    TextTjUriageSyouhizei.Text = "" '������z/�����
                    TextTjUriageZeikomiKingaku.Text = "" '������z/�ō����z
                    TextTjSiireZeinukiKingaku.Text = "" '�d�����z/�Ŕ����z
                    TextTjSiireSyouhizei.Text = "" '�d�����z/�����
                    TextTjSiireZeikomiKingaku.Text = "" '�d�����z/�ō����z
                    TextTjZangaku.Text = "0" '�c�z
                    TextTjSeikyuusyoHakkouDate.Text = "" '���������s��
                    TextTjHattyuusyoKakutei.Text = "" '�������m��

                    '���G���[���b�Z�[�W�\��
                    tmpErrMsg = Messages.MSG163E.Replace("@PARAM1", "���i�}�X�^")
                    tmpScript = "alert('" & tmpErrMsg & "');"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetSyouhinInfo", tmpScript, True)

                Else
                    HiddenTjZeiritu.Value = cl.GetDispStr(syouhinRec.Zeiritu) '�ŗ�
                    HiddenTjZeiKbn.Value = cl.GetDispStr(syouhinRec.ZeiKbn) '�ŋ敪

                    '�����L��
                    If SelectTjSeikyuuUmu.SelectedValue = "1" Then '�L
                        TextTjUriageZeinukiKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1) '������z/�Ŕ����z
                    Else '��
                        TextTjUriageZeinukiKingaku.Text = "0" '������z/�Ŕ����z
                    End If

                End If

            Case EnumKoujiType.KairyouKoujiHoukokusyo '���ǍH���񍐏�
                Exit Sub
            Case Else
                Exit Sub
        End Select

    End Sub

    ''' <summary>
    ''' �����ݒ�/���i���(�H�����i�}�X�^������z�E�����L�����ݒ�)
    ''' </summary>
    ''' <param name="emType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSyouhinInfoFromKojM(ByVal emType As EnumKoujiType, ByVal emAcType As EarthEnum.emKojKkkActionType) As Boolean

        Dim syouhinRec As New Syouhin23Record
        Dim keyRec As New KoujiKakakuKeyRecord          '�����L�[�p���R�[�h
        Dim resultRec As New KoujiKakakuRecord          '���ʎ擾�p���R�[�h
        Dim lgcKouji As New KairyouKoujiLogic           '���ǍH�����W�b�N
        Dim intResult As Integer = Integer.MinValue

        '�H���^�C�v
        Select Case emType
            Case EnumKoujiType.KairyouKouji '���ǍH��

                '�擾�ɕK�v�ȉ�ʍ��ڂ̃Z�b�g
                keyRec = cbLogic.GetKojKkkMstKeyRec(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                , Me.ucGyoumuKyoutuu.AccHdnEigyousyoCd.Value _
                                                , Me.ucGyoumuKyoutuu.AccHdnKeiretuCd.Value _
                                                , Me.SelectKjSyouhinCd.SelectedValue _
                                                , Me.TextKjKoujiKaisyaCd.Text)

                '�H����Љ��i���W�b�N���擾
                intResult = lgcKouji.GetKoujiKakakuRecord(keyRec, resultRec)

                '������v�̎擾�͉�ʂɃZ�b�g
                If intResult < EarthEnum.emKoujiKakaku.Syouhin Then
                    '�H����А���
                    If resultRec.KojGaisyaSeikyuuUmu = 1 Then
                        Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked = True
                    Else
                        Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked = False
                    End If
                    '�����L���̃Z�b�g�͐����L���ύX���ȊO
                    If emAcType <> EarthEnum.emKojKkkActionType.SeikyuuUmu Then
                        '�����L��(�Z�b�g����̂͗L��^�������̂݁j
                        If resultRec.SeikyuuUmu = 0 OrElse resultRec.SeikyuuUmu = 1 Then
                            Me.SelectKjSeikyuuUmu.SelectedValue = resultRec.SeikyuuUmu
                        Else
                            Me.SelectKjSeikyuuUmu.SelectedValue = String.Empty
                        End If
                    End If
                    '����Ŕ����z
                    If SelectKjSeikyuuUmu.SelectedValue = "1" Then  '�����L���u�L�v
                        Me.TextKjUriageZeinukiKingaku.Text = cl.ChgStrToInt(cl.GetDispNum(resultRec.UriGaku)).ToString(EarthConst.FORMAT_KINGAKU_1)
                    Else  '�����L���u���v
                        Me.TextKjUriageZeinukiKingaku.Text = "0"
                    End If
                    '�ŗ�
                    Me.HiddenKjZeiritu.Value = cl.GetDispStr(resultRec.Zeiritu)
                    '�ŋ敪
                    Me.HiddenKjZeiKbn.Value = cl.GetDispStr(resultRec.ZeiKbn)
                Else
                    Return False
                    Exit Function
                End If

            Case EnumKoujiType.TuikaKouji   '�ǉ��H��

                '�擾�ɕK�v�ȉ�ʍ��ڂ̃Z�b�g
                keyRec = cbLogic.GetKojKkkMstKeyRec(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                , Me.ucGyoumuKyoutuu.AccHdnEigyousyoCd.Value _
                                                , Me.ucGyoumuKyoutuu.AccHdnKeiretuCd.Value _
                                                , Me.SelectTjSyouhinCd.SelectedValue _
                                                , Me.TextTjKoujiKaisyaCd.Text)

                '�H����Љ��i���W�b�N���擾
                intResult = lgcKouji.GetKoujiKakakuRecord(keyRec, resultRec)

                '������v�̎擾�͉�ʂɃZ�b�g
                If intResult < EarthEnum.emKoujiKakaku.Syouhin Then
                    '�H����А���
                    If resultRec.KojGaisyaSeikyuuUmu = 1 Then
                        Me.CheckBoxTjKoujiKaisyaSeikyuu.Checked = True
                    Else
                        Me.CheckBoxTjKoujiKaisyaSeikyuu.Checked = False
                    End If
                    '�����L���̃Z�b�g�͐����L���ύX���ȊO
                    If emAcType <> EarthEnum.emKojKkkActionType.SeikyuuUmu Then
                        '�����L���i�Z�b�g����̂͗L��^�������̂݁j
                        If resultRec.SeikyuuUmu = 0 OrElse resultRec.SeikyuuUmu = 1 Then
                            Me.SelectTjSeikyuuUmu.SelectedValue = resultRec.SeikyuuUmu
                        Else
                            Me.SelectTjSeikyuuUmu.SelectedValue = String.Empty
                        End If
                    End If
                    '����Ŕ����z
                    If SelectTjSeikyuuUmu.SelectedValue = "1" Then '�L
                        Me.TextTjUriageZeinukiKingaku.Text = cl.ChgStrToInt(cl.GetDispNum(resultRec.UriGaku)).ToString(EarthConst.FORMAT_KINGAKU_1)
                    Else
                        Me.TextTjUriageZeinukiKingaku.Text = "0"
                    End If
                    '�ŗ�
                    Me.HiddenTjZeiritu.Value = cl.GetDispStr(resultRec.Zeiritu)
                    '�ŋ敪
                    Me.HiddenTjZeiKbn.Value = cl.GetDispStr(resultRec.ZeiKbn)
                Else
                    Return False
                    Exit Function
                End If

            Case Else
                Exit Function
        End Select

        Return True

    End Function

#End Region

#Region "���z�ݒ�"

    ''' <summary>
    ''' ���z�ݒ�(������z)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKingakuUriage(ByVal emType As EnumKoujiType, Optional ByVal blnZeigaku As Boolean = False)
        ' �Ŕ����i�i���������z�j
        Dim zeinuki_ctrl As TextBox
        ' ����ŗ�
        Dim zeiritu_ctrl As HtmlInputHidden
        ' ����Ŋz
        Dim zeigaku_ctrl As TextBox
        ' �ō����z
        Dim zeikomi_gaku_ctrl As TextBox

        Select Case emType
            Case EnumKoujiType.KairyouKouji '���ǍH��
                zeinuki_ctrl = TextKjUriageZeinukiKingaku
                zeiritu_ctrl = HiddenKjZeiritu
                zeigaku_ctrl = TextKjUriageSyouhizei
                zeikomi_gaku_ctrl = TextKjUriageZeikomiKingaku

            Case EnumKoujiType.TuikaKouji '�ǉ��H��
                zeinuki_ctrl = TextTjUriageZeinukiKingaku
                zeiritu_ctrl = HiddenTjZeiritu
                zeigaku_ctrl = TextTjUriageSyouhizei
                zeikomi_gaku_ctrl = TextTjUriageZeikomiKingaku

            Case EnumKoujiType.KairyouKoujiHoukokusyo '���ǍH���񍐏�
                zeinuki_ctrl = TextKhJituseikyuuKingaku
                zeiritu_ctrl = HiddenKhZeiritu
                zeigaku_ctrl = TextKhSyouhizei
                zeikomi_gaku_ctrl = TextKhZeikomiKingaku

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

        ' �����z/�c�z���Z�b�g
        CalcZangaku(emType, zeikomi_gaku_ctrl.Text)

    End Sub

    ''' <summary>
    ''' ���z�ݒ�(�d�����z)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKingakuSiire(ByVal emType As EnumKoujiType)
        ' �Ŕ����i�i���������z�j
        Dim zeinuki_ctrl As TextBox
        ' ����ŗ�
        Dim zeiritu_ctrl As HtmlInputHidden
        ' ����Ŋz
        Dim zeigaku_ctrl As TextBox
        ' �ō����z
        Dim zeikomi_gaku_ctrl As TextBox

        Select Case emType
            Case EnumKoujiType.KairyouKouji '���ǍH��
                zeinuki_ctrl = TextKjSiireZeinukiKingaku
                zeiritu_ctrl = HiddenKjZeiritu
                zeigaku_ctrl = TextKjSiireSyouhizei
                zeikomi_gaku_ctrl = TextKjSiireZeikomiKingaku

            Case EnumKoujiType.TuikaKouji '�ǉ��H��
                zeinuki_ctrl = TextTjSiireZeinukiKingaku
                zeiritu_ctrl = HiddenTjZeiritu
                zeigaku_ctrl = TextTjSiireSyouhizei
                zeikomi_gaku_ctrl = TextTjSiireZeikomiKingaku

            Case EnumKoujiType.KairyouKoujiHoukokusyo '���ǍH���񍐏�
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

            zeigaku = Fix(zeinuki * zeiritu)
            zeikomi_gaku = zeinuki + zeigaku

            zeigaku_ctrl.Text = Format(zeigaku, EarthConst.FORMAT_KINGAKU_1) '�����
            zeikomi_gaku_ctrl.Text = Format(zeikomi_gaku, EarthConst.FORMAT_KINGAKU_1) '�ō����z
        End If

    End Sub

    ''' <summary>
    ''' ���i�e�[�u�����z�G���A��0�N���A/�H���񍐏��Ĕ��s
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub Clear0SyouhinTableKh()
        '���z��0�N���A
        '�������ݒ�
        TextKhKoumutenSeikyuuKingaku.Text = "0" '�H���X�������z
        TextKhJituseikyuuKingaku.Text = "0" '���������z
        TextKhSyouhizei.Text = "0" '�����
        TextKhZeikomiKingaku.Text = "0" '�ō����z
        TextKhSeikyuusyoHakkouDate.Text = "" '���������s��
        TextKhUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) '����N����
        '�������m��
        If TextKhHattyuusyoKakutei.Text.Length = 0 Then '(*5)�������m�肪�󔒂̏ꍇ�́A�u0�F���m��v��ݒ肷��
            TextKhHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
        End If

    End Sub

    ''' <summary>
    ''' ���i�e�[�u�����z�G���A�̋󔒃N���A
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearBlnkSyouhinTable()
        '�󔒃N���A
        '�������ݒ�
        TextKhSyouhinCd.Text = "" '���i�R�[�h
        SpanKhSyouhinMei.InnerHtml = "" '���i��
        TextKhKoumutenSeikyuuKingaku.Text = "" '�H���X�������z
        TextKhJituseikyuuKingaku.Text = "" '���������z
        TextKhSeikyuusyoHakkouDate.Text = "" '���������s��
        TextKhUriageNengappi.Text = "" '����N����

        SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo) '���z�̍Čv�Z

    End Sub

#Region "�ŗ�/�ŋ敪"

    ''' <summary>
    ''' �ŗ�/�ŋ敪��Hidden�ɃZ�b�g����
    ''' </summary>
    ''' <param name="strItemCd"></param>
    ''' <remarks></remarks>
    Protected Sub SetKjZeiInfo(ByVal strItemCd As String)
        Dim syouhinRec As New Syouhin23Record

        '���i�����擾(�L�[:���i�R�[�h)
        syouhinRec = JibanLogic.GetSyouhinInfo(strItemCd, EarthEnum.EnumSyouhinKubun.KairyouKouji, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If Not syouhinRec Is Nothing Then
            HiddenKjZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
            HiddenKjZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪
        End If
    End Sub

    ''' <summary>
    ''' �ŗ�/�ŋ敪��Hidden�ɃZ�b�g����
    ''' </summary>
    ''' <param name="strItemCd"></param>
    ''' <remarks></remarks>
    Protected Sub SetTjZeiInfo(ByVal strItemCd As String)
        Dim syouhinRec As New Syouhin23Record

        '���i�����擾(�L�[:���i�R�[�h)
        syouhinRec = JibanLogic.GetSyouhinInfo(strItemCd, EarthEnum.EnumSyouhinKubun.TuikaKouji, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If Not syouhinRec Is Nothing Then
            HiddenTjZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
            HiddenTjZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪
        End If
    End Sub

    ''' <summary>
    ''' �ŗ�/�ŋ敪��Hidden�ɃZ�b�g����
    ''' </summary>
    ''' <param name="strItemCd"></param>
    ''' <remarks></remarks>
    Protected Sub SetKhZeiInfo(ByVal strItemCd As String)
        Dim syouhin23Rec As New Syouhin23Record

        '���i�����擾(�L�[:���i�R�[�h)
        syouhin23Rec = JibanLogic.GetSyouhinInfo(strItemCd, EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If Not syouhin23Rec Is Nothing Then
            HiddenKhZeiritu.Value = syouhin23Rec.Zeiritu '�ŗ�
            HiddenKhZeiKbn.Value = syouhin23Rec.ZeiKbn '�ŋ敪
        End If
    End Sub


#End Region

#End Region

#Region "�����z/�c�z"

    ''' <summary>
    ''' �����z�E�c�z��\�����܂�<br/>
    ''' �ō�������z���v�̂ݕύX���A�Čv�Z���܂�<br/>
    ''' </summary>
    ''' <param name="emType">�H���^�C�v</param>
    ''' <param name="strUriageGoukeiGaku">�ō�������z���v</param>
    ''' <remarks></remarks>
    Public Sub CalcZangaku(ByVal emType As EnumKoujiType, ByVal strUriageGoukeiGaku As String)

        ' �����z�i�ō��j
        Dim nyuukingaku_ctrl As TextBox
        ' �c�z
        Dim zangakuu_ctrl As TextBox

        Select Case emType
            Case EnumKoujiType.KairyouKouji '���ǍH��
                nyuukingaku_ctrl = TextKjNyuukingaku
                zangakuu_ctrl = TextKjZangaku

            Case EnumKoujiType.TuikaKouji '�ǉ��H��
                nyuukingaku_ctrl = TextTjNyuuKingaku
                zangakuu_ctrl = TextTjZangaku

            Case EnumKoujiType.KairyouKoujiHoukokusyo '���ǍH���񍐏�
                nyuukingaku_ctrl = TextKhNyuuKingaku
                zangakuu_ctrl = TextKhZangaku

            Case Else
                Exit Sub
        End Select

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
    ''' <param name="emType">�H���^�C�v</param>
    ''' <param name="uriageGoukeiGaku">�ō�������z���v</param>
    ''' <param name="nyuukinGaku">�����z</param>
    ''' <remarks></remarks>
    Public Sub CalcZangaku( _
                            ByVal emType As EnumKoujiType _
                            , ByVal uriageGoukeiGaku As Integer _
                            , ByVal nyuukinGaku As Integer _
                            )

        ' �����z�i�ō��j
        Dim nyuukingaku_ctrl As TextBox
        ' �c�z
        Dim zangakuu_ctrl As TextBox

        Select Case emType
            Case EnumKoujiType.KairyouKouji '���ǍH��
                nyuukingaku_ctrl = TextKjNyuukingaku
                zangakuu_ctrl = TextKjZangaku

            Case EnumKoujiType.TuikaKouji '�ǉ��H��
                nyuukingaku_ctrl = TextTjNyuuKingaku
                zangakuu_ctrl = TextTjZangaku

            Case EnumKoujiType.KairyouKoujiHoukokusyo '���ǍH���񍐏�
                nyuukingaku_ctrl = TextKhNyuuKingaku
                zangakuu_ctrl = TextKhZangaku

            Case Else
                Exit Sub
        End Select

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
    ''' �R���g���[���̐ړ�����t�^���ĕԂ��B
    ''' </summary>
    ''' <param name="intKoujiType">�H���^�C�v</param>
    ''' <param name="intKingakuType">���z�^�C�v</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChgStrKoujiType( _
                                    ByVal intKoujiType As EnumKoujiType _
                                    , Optional ByVal intKingakuType As EnumKingakuType = EnumKingakuType.UriageKingaku _
                                    ) As String

        Dim strKingakuType As String = ""
        '���z�^�C�v
        Select Case intKingakuType
            Case EnumKingakuType.UriageKingaku
                strKingakuType = "Uriage"
            Case EnumKingakuType.SiireKingaku
                strKingakuType = "Siire"
            Case EnumKingakuType.None
                strKingakuType = ""
        End Select

        '�H���^�C�v
        Select Case intKoujiType
            Case EnumKoujiType.KairyouKouji
                Return pStrKairyouKouji & strKingakuType
            Case EnumKoujiType.TuikaKouji
                Return pStrTuikaKouji & strKingakuType
            Case EnumKoujiType.KairyouKoujiHoukokusyo
                Return pStrHoukokusyo
            Case Else
                Return ""
        End Select
    End Function

#Region "�@�ʐ������R�[�h�ҏW"

#Region "��ʃR���g���[���֏o��"
    ''' <summary>
    ''' ���ǍH��/�@�ʐ������R�[�h
    ''' </summary>
    ''' <param name="TeibetuRec"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlTeibetuSeikyuuDataKj( _
                                    ByVal TeibetuRec As TeibetuSeikyuuRecord _
                                    )

        '���i�R�[�h�̑��݃`�F�b�N
        If cl.ChkDropDownList(SelectKjSyouhinCd, TeibetuRec.SyouhinCd) Then
            SelectKjSyouhinCd.SelectedValue = cl.GetDispStr(TeibetuRec.SyouhinCd) '���i�R�[�h
        Else '�����݂̏ꍇ�A���ڒǉ�
            SelectKjSyouhinCd.Items.Add(New ListItem(TeibetuRec.SyouhinCd & ":" & TeibetuRec.SyouhinMei, TeibetuRec.SyouhinCd)) '���i�R�[�h
            SelectKjSyouhinCd.SelectedValue = TeibetuRec.SyouhinCd  '�I�����
        End If

        ' ���㏈����
        SpanKjUriageSyoriZumi.InnerHtml = IIf(TeibetuRec.UriKeijyouDate = Date.MinValue, "", EarthConst.URIAGE_ZUMI)
        ' ����v���
        Me.HiddenKjUriageKeijyouDate.Value = cl.GetDisplayString(TeibetuRec.UriKeijyouDate)

        ' �����L��
        SelectKjSeikyuuUmu.SelectedValue = IIf(TeibetuRec.SeikyuuUmu = 1, "1", "0")

        ' �ŗ��iHidden�j
        HiddenKjZeiritu.Value = TeibetuRec.Zeiritu
        ' �ŋ敪�iHidden�j
        HiddenKjZeiKbn.Value = IIf(TeibetuRec.ZeiKbn = "", "", TeibetuRec.ZeiKbn)
        If TeibetuRec.Zeiritu = 0 And TeibetuRec.ZeiKbn = "" Then
            SetKjZeiInfo(TeibetuRec.SyouhinCd)
        End If

        '*****************
        '* �y������z�z
        '*****************
        '���������Ŋz
        TextKjUriageSyouhizei.Text = IIf(TeibetuRec.UriageSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.UriageSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '���Ŕ�������z
        TextKjUriageZeinukiKingaku.Text = IIf(TeibetuRec.UriGaku = Integer.MinValue, 0, Format(TeibetuRec.UriGaku, EarthConst.FORMAT_KINGAKU_1))
        '���ō�������z
        TextKjUriageZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiUriGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiUriGaku, EarthConst.FORMAT_KINGAKU_1))

        '*****************
        '* �y�d�����z�z
        '*****************
        '���d������Ŋz
        TextKjSiireSyouhizei.Text = IIf(TeibetuRec.SiireSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '���Ŕ��d�����z
        TextKjSiireZeinukiKingaku.Text = IIf(TeibetuRec.SiireGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireGaku, EarthConst.FORMAT_KINGAKU_1))
        '���ō��d�����z
        TextKjSiireZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiSiireGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiSiireGaku, EarthConst.FORMAT_KINGAKU_1))

        ' ���������s��
        TextKjSeikyuusyoHakkouDate.Text = IIf(TeibetuRec.SeikyuusyoHakDate = Date.MinValue, _
                                          "", _
                                          TeibetuRec.SeikyuusyoHakDate.ToString(EarthConst.FORMAT_DATE_TIME_9))
        ' ����N����
        TextKjUriageNengappi.Text = IIf(TeibetuRec.UriDate = Date.MinValue, _
                                      "", _
                                      TeibetuRec.UriDate.ToString(EarthConst.FORMAT_DATE_TIME_9))

        ' �������m��
        TextKjHattyuusyoKakutei.Text = IIf(TeibetuRec.HattyuusyoKakuteiFlg = 1, EarthConst.KAKUTEI, EarthConst.MIKAKUTEI)
        ' ���������z
        TextKjHattyuusyoKingaku.Text = IIf(TeibetuRec.HattyuusyoGaku = Integer.MinValue, _
                                         "", _
                                         TeibetuRec.HattyuusyoGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' �������m�F��
        TextKjHattyuusyoKakuninDate.Text = IIf(TeibetuRec.HattyuusyoKakuninDate = Date.MinValue, _
                                           "", _
                                           TeibetuRec.HattyuusyoKakuninDate.ToString(EarthConst.FORMAT_DATE_TIME_9))

        '������/�d���惊���N�փZ�b�g
        Me.ucSeikyuuSiireLinkKai.SetSeikyuuSiireLinkFromTeibetuRec(TeibetuRec)

        ' �X�V���� �ǂݍ��ݎ��̃^�C���X�^���v(�r������p)
        cl.setDispHaitaUpdTime(TeibetuRec.AddDatetime, TeibetuRec.UpdDatetime, Me.HiddenUpdateTeibetuSeikyuuDatetimeKj)

    End Sub

    ''' <summary>
    ''' �ǉ��H��/�@�ʐ������R�[�h
    ''' </summary>
    ''' <param name="TeibetuRec"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlTeibetuSeikyuuDataTj( _
                                    ByVal TeibetuRec As TeibetuSeikyuuRecord _
                                    )

        '���i�R�[�h�̑��݃`�F�b�N
        If cl.ChkDropDownList(SelectTjSyouhinCd, TeibetuRec.SyouhinCd) Then
            SelectTjSyouhinCd.SelectedValue = cl.GetDispStr(TeibetuRec.SyouhinCd) '���i�R�[�h
        Else '�����݂̏ꍇ�A���ڒǉ�
            SelectTjSyouhinCd.Items.Add(New ListItem(TeibetuRec.SyouhinCd & ":" & TeibetuRec.SyouhinMei, TeibetuRec.SyouhinCd)) '���i�R�[�h
            SelectTjSyouhinCd.SelectedValue = TeibetuRec.SyouhinCd  '�I�����
        End If
        ' ���㏈����
        SpanTjUriageSyorizumi.InnerHtml = IIf(TeibetuRec.UriKeijyouDate = Date.MinValue, "", EarthConst.URIAGE_ZUMI)
        ' ����v���
        Me.HiddenTjUriageKeijyouDate.Value = cl.GetDisplayString(TeibetuRec.UriKeijyouDate)
        ' �����L��
        SelectTjSeikyuuUmu.SelectedValue = IIf(TeibetuRec.SeikyuuUmu = 1, "1", "0")

        ' �ŗ��iHidden�j
        HiddenTjZeiritu.Value = TeibetuRec.Zeiritu
        ' �ŋ敪�iHidden�j
        HiddenTjZeiKbn.Value = IIf(TeibetuRec.ZeiKbn = "", "", TeibetuRec.ZeiKbn)
        If TeibetuRec.Zeiritu = 0 And TeibetuRec.ZeiKbn = "" Then
            SetTjZeiInfo(TeibetuRec.SyouhinCd)
        End If

        '*****************
        '* �y������z�z
        '*****************
        '���������Ŋz
        TextTjUriageSyouhizei.Text = IIf(TeibetuRec.UriageSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.UriageSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '���Ŕ�������z
        TextTjUriageZeinukiKingaku.Text = IIf(TeibetuRec.UriGaku = Integer.MinValue, 0, Format(TeibetuRec.UriGaku, EarthConst.FORMAT_KINGAKU_1))
        '���ō�������z
        TextTjUriageZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiUriGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiUriGaku, EarthConst.FORMAT_KINGAKU_1))

        '*****************
        '* �y�d�����z�z
        '*****************
        '���d������Ŋz
        TextTjSiireSyouhizei.Text = IIf(TeibetuRec.SiireSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '���Ŕ��d�����z
        TextTjSiireZeinukiKingaku.Text = IIf(TeibetuRec.SiireGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireGaku, EarthConst.FORMAT_KINGAKU_1))
        '���ō��d�����z
        TextTjSiireZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiSiireGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiSiireGaku, EarthConst.FORMAT_KINGAKU_1))

        ' ���������s��
        TextTjSeikyuusyoHakkouDate.Text = IIf(TeibetuRec.SeikyuusyoHakDate = Date.MinValue, _
                                          "", _
                                          TeibetuRec.SeikyuusyoHakDate.ToString(EarthConst.FORMAT_DATE_TIME_9))
        ' ����N����
        TextTjUriageNengappi.Text = IIf(TeibetuRec.UriDate = Date.MinValue, _
                                      "", _
                                      TeibetuRec.UriDate.ToString(EarthConst.FORMAT_DATE_TIME_9))

        ' �������m��
        TextTjHattyuusyoKakutei.Text = IIf(TeibetuRec.HattyuusyoKakuteiFlg = 1, EarthConst.KAKUTEI, EarthConst.MIKAKUTEI)
        ' ���������z
        TextTjHattyuusyoKingaku.Text = IIf(TeibetuRec.HattyuusyoGaku = Integer.MinValue, _
                                         "", _
                                         TeibetuRec.HattyuusyoGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' �������m�F��
        TextTjHattyuusyoKakuninDate.Text = IIf(TeibetuRec.HattyuusyoKakuninDate = Date.MinValue, _
                                           "", _
                                           TeibetuRec.HattyuusyoKakuninDate.ToString(EarthConst.FORMAT_DATE_TIME_9))

        '������/�d���惊���N�փZ�b�g
        Me.ucSeikyuuSiireLinkTui.SetSeikyuuSiireLinkFromTeibetuRec(TeibetuRec)

        ' �X�V���� �ǂݍ��ݎ��̃^�C���X�^���v(�r������p)
        cl.setDispHaitaUpdTime(TeibetuRec.AddDatetime, TeibetuRec.UpdDatetime, Me.HiddenUpdateTeibetuSeikyuuDatetimeTj)

    End Sub

    ''' <summary>
    ''' ���ǍH���񍐏�/�@�ʐ������R�[�h
    ''' </summary>
    ''' <param name="TeibetuRec"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlTeibetuSeikyuuDataKh( _
                                    ByVal TeibetuRec As TeibetuSeikyuuRecord _
                                    )

        ' ���㏈����
        SpanKhUriageSyorizumi.InnerHtml = IIf(TeibetuRec.UriKeijyouDate = Date.MinValue, "", EarthConst.URIAGE_ZUMI)
        ' ����v���
        Me.HiddenKhUriageKeijyouDate.Value = cl.GetDisplayString(TeibetuRec.UriKeijyouDate)
        '���i�R�[�h
        TextKhSyouhinCd.Text = cl.GetDispStr(TeibetuRec.SyouhinCd)
        '���i��
        SpanKhSyouhinMei.InnerHtml = cl.GetDispStr(TeibetuRec.SyouhinMei)

        ' �����L��
        SelectKhSeikyuuUmu.SelectedValue = IIf(TeibetuRec.SeikyuuUmu = 1, "1", "0")

        '�H���X�����Ŕ����z
        TextKhKoumutenSeikyuuKingaku.Text = IIf(TeibetuRec.KoumutenSeikyuuGaku = Integer.MinValue, _
                                            0, _
                                            TeibetuRec.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1))

        ' �ŗ��iHidden�j
        HiddenKhZeiritu.Value = TeibetuRec.Zeiritu
        ' �ŋ敪�iHidden�j
        HiddenKhZeiKbn.Value = IIf(TeibetuRec.ZeiKbn = "", "", TeibetuRec.ZeiKbn)
        If TeibetuRec.Zeiritu = 0 And TeibetuRec.ZeiKbn = "" Then
            SetKhZeiInfo(TeibetuRec.SyouhinCd)
        End If

        '*****************
        '* �y������z�z
        '*****************
        '���������Ŋz(�����)
        TextKhSyouhizei.Text = IIf(TeibetuRec.UriageSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.UriageSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '���Ŕ�������z(�������Ŕ����z)
        TextKhJituseikyuuKingaku.Text = IIf(TeibetuRec.UriGaku = Integer.MinValue, 0, Format(TeibetuRec.UriGaku, EarthConst.FORMAT_KINGAKU_1))
        '���ō�������z(�ō��z)
        TextKhZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiUriGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiUriGaku, EarthConst.FORMAT_KINGAKU_1))

        '*****************
        '* �y�d�����z�z
        '*****************
        '���d������Ŋz(�����)
        Me.HiddenKhSiireSyouhiZei.Value = IIf(TeibetuRec.SiireSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '���d��������z(�Ŕ����z)
        Me.HiddenKhSiireGaku.Value = IIf(TeibetuRec.SiireGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireGaku, EarthConst.FORMAT_KINGAKU_1))

        ' ���������s��
        TextKhSeikyuusyoHakkouDate.Text = IIf(TeibetuRec.SeikyuusyoHakDate = Date.MinValue, _
                                          "", _
                                          TeibetuRec.SeikyuusyoHakDate.ToString(EarthConst.FORMAT_DATE_TIME_9))
        ' ����N����
        TextKhUriageNengappi.Text = IIf(TeibetuRec.UriDate = Date.MinValue, _
                                      "", _
                                      TeibetuRec.UriDate.ToString(EarthConst.FORMAT_DATE_TIME_9))

        ' �������m��
        TextKhHattyuusyoKakutei.Text = IIf(TeibetuRec.HattyuusyoKakuteiFlg = 1, EarthConst.KAKUTEI, EarthConst.MIKAKUTEI)
        ' ���������z
        TextKhHattyuusyoKingaku.Text = IIf(TeibetuRec.HattyuusyoGaku = Integer.MinValue, _
                                         "", _
                                         TeibetuRec.HattyuusyoGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' �������m�F��
        TextKhHattyuusyoKakuninDate.Text = IIf(TeibetuRec.HattyuusyoKakuninDate = Date.MinValue, _
                                           "", _
                                           TeibetuRec.HattyuusyoKakuninDate.ToString(EarthConst.FORMAT_DATE_TIME_9))

        '�Ĕ��s���R
        TextKhSaihakkouRiyuu.Text = cl.GetDispStr(TeibetuRec.Bikou)

        '������/�d���惊���N�փZ�b�g
        Me.ucSeikyuuSiireLink.SetSeikyuuSiireLinkFromTeibetuRec(TeibetuRec)

        '������^�C�v�̎擾�ݒ�
        Me.TextKhSeikyuusaki.Text = cl.GetSeikyuuSakiTypeStr( _
                                                        TeibetuRec.SyouhinCd _
                                                        , EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo _
                                                        , Me.HiddenKameitenCd.Value _
                                                        , TeibetuRec.SeikyuuSakiCd _
                                                        , TeibetuRec.SeikyuuSakiBrc _
                                                        , TeibetuRec.SeikyuuSakiKbn)

        ' �X�V���� �ǂݍ��ݎ��̃^�C���X�^���v(�r������p)
        cl.setDispHaitaUpdTime(TeibetuRec.AddDatetime, TeibetuRec.UpdDatetime, Me.HiddenUpdateTeibetuSeikyuuDatetimeKh)

    End Sub
#End Region

#Region "��ʃR���g���[���������"

    ''' <summary>
    ''' ���ǍH��/�@�ʐ������R�[�h�f�[�^�ɃR���g���[���̓��e���Z�b�g���܂�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSeikyuuCtrlDataKj() As TeibetuSeikyuuRecord
        ' ���i�R�[�h���ݒ莞�A�����Z�b�g���Ȃ�
        If SelectKjSyouhinCd.SelectedValue = "" Then
            Return Nothing
        End If

        '�@�ʐ������R�[�h
        Dim record As New TeibetuSeikyuuRecord

        ' �敪
        record.Kbn = ucGyoumuKyoutuu.Kubun
        ' �ԍ�
        record.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        ' ���ރR�[�h(130)
        record.BunruiCd = EarthConst.SOUKO_CD_KAIRYOU_KOUJI
        ' ��ʕ\��NO
        record.GamenHyoujiNo = 1
        ' ���i�R�[�h
        record.SyouhinCd = SelectKjSyouhinCd.SelectedValue
        ' ������z
        cl.SetDisplayString(TextKjUriageZeinukiKingaku.Text, record.UriGaku)
        ' �d�����z
        cl.SetDisplayString(TextKjSiireZeinukiKingaku.Text, record.SiireGaku)
        ' �ŋ敪
        record.ZeiKbn = HiddenKjZeiKbn.Value
        ' �ŗ�
        cl.SetDisplayString(HiddenKjZeiritu.Value, record.Zeiritu)
        ' ����Ŋz
        cl.SetDisplayString(TextKjUriageSyouhizei.Text, record.UriageSyouhiZeiGaku)
        ' �d������Ŋz
        cl.SetDisplayString(TextKjSiireSyouhizei.Text, record.SiireSyouhiZeiGaku)
        ' ���������s��
        cl.SetDisplayString(TextKjSeikyuusyoHakkouDate.Text, record.SeikyuusyoHakDate)
        ' ����N����
        cl.SetDisplayString(TextKjUriageNengappi.Text, record.UriDate)
        ' �`�[����N����(���W�b�N�N���X�Ŏ����Z�b�g)
        record.DenpyouUriDate = DateTime.MinValue
        ' �����L��
        record.SeikyuuUmu = IIf(SelectKjSeikyuuUmu.SelectedValue = "1", 1, 0)
        ' ����v��FLG
        record.UriKeijyouFlg = 0
        ' ����v���
        record.UriKeijyouDate = DateTime.MinValue
        ' �m��敪
        record.KakuteiKbn = Integer.MinValue
        ' ���l
        record.Bikou = Nothing
        ' �H���X�����Ŕ����z
        record.KoumutenSeikyuuGaku = 0
        ' ���������z
        cl.SetDisplayString(TextKjHattyuusyoKingaku.Text, record.HattyuusyoGaku)
        ' �������m�F��
        cl.SetDisplayString(TextKjHattyuusyoKakuninDate.Text, record.HattyuusyoKakuninDate)
        ' �ꊇ����FLG
        record.IkkatuNyuukinFlg = Integer.MinValue
        ' �������Ϗ��쐬��
        record.TysMitsyoSakuseiDate = DateTime.MinValue
        ' �������m��FLG
        record.HattyuusyoKakuteiFlg = IIf(TextKjHattyuusyoKakutei.Text = EarthConst.KAKUTEI, 1, 0)

        '������/�d���惊���N����擾
        Me.ucSeikyuuSiireLinkKai.SetTeibetuRecFromSeikyuuSiireLink(record)

        ' �X�V���O�C�����[�UID
        record.UpdLoginUserId = userinfo.LoginUserId

        ' �X�V���� �ǂݍ��ݎ��̃^�C���X�^���v(�r������p)
        record.UpdDatetime = cl.getDispHaitaUpdTime(Me.HiddenUpdateTeibetuSeikyuuDatetimeKj)

        Return record
    End Function

    ''' <summary>
    ''' �ǉ��H��(�����ݒ�ΏۊO)/�@�ʐ������R�[�h�f�[�^�ɃR���g���[���̓��e���Z�b�g���܂�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSeikyuuCtrlDataTj() As TeibetuSeikyuuRecord
        ' ���i�R�[�h���ݒ莞�A�����Z�b�g���Ȃ�
        If SelectTjSyouhinCd.SelectedValue = "" Then
            Return Nothing
        End If

        '�@�ʐ������R�[�h
        Dim record As New TeibetuSeikyuuRecord

        ' �敪
        record.Kbn = ucGyoumuKyoutuu.Kubun
        ' �ۏ؏�NO
        record.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        ' ���ރR�[�h(140)
        record.BunruiCd = EarthConst.SOUKO_CD_TUIKA_KOUJI
        ' ��ʕ\��NO
        record.GamenHyoujiNo = 1
        ' ���i�R�[�h
        record.SyouhinCd = SelectTjSyouhinCd.SelectedValue
        ' ������z
        cl.SetDisplayString(TextTjUriageZeinukiKingaku.Text, record.UriGaku)
        ' �d�����z
        cl.SetDisplayString(TextTjSiireZeinukiKingaku.Text, record.SiireGaku)
        ' �ŋ敪
        record.ZeiKbn = HiddenTjZeiKbn.Value
        ' �ŗ�
        cl.SetDisplayString(HiddenTjZeiritu.Value, record.Zeiritu)
        ' ����Ŋz
        cl.SetDisplayString(TextTjUriageSyouhizei.Text, record.UriageSyouhiZeiGaku)
        ' �d������Ŋz
        cl.SetDisplayString(TextTjSiireSyouhizei.Text, record.SiireSyouhiZeiGaku)
        ' ���������s��
        cl.SetDisplayString(TextTjSeikyuusyoHakkouDate.Text, record.SeikyuusyoHakDate)
        ' ����N����
        cl.SetDisplayString(TextTjUriageNengappi.Text, record.UriDate)
        ' �`�[����N����(���W�b�N�N���X�Ŏ����Z�b�g)
        record.DenpyouUriDate = DateTime.MinValue
        ' �����L��
        record.SeikyuuUmu = IIf(SelectTjSeikyuuUmu.SelectedValue = "1", 1, 0)
        ' ����v��FLG
        record.UriKeijyouFlg = 0
        ' ����v���
        record.UriKeijyouDate = Date.MinValue
        ' �m��敪
        record.KakuteiKbn = Integer.MinValue
        ' ���l
        record.Bikou = Nothing
        ' �H���X�����Ŕ����z
        record.KoumutenSeikyuuGaku = 0
        ' ���������z
        cl.SetDisplayString(TextTjHattyuusyoKingaku.Text, record.HattyuusyoGaku)
        ' �������m�F��
        cl.SetDisplayString(TextTjHattyuusyoKakuninDate.Text, record.HattyuusyoKakuninDate)
        ' �ꊇ����FLG
        record.IkkatuNyuukinFlg = Integer.MinValue
        ' �������Ϗ��쐬��
        record.TysMitsyoSakuseiDate = Date.MinValue
        ' �������m��FLG
        record.HattyuusyoKakuteiFlg = IIf(TextTjHattyuusyoKakutei.Text = EarthConst.KAKUTEI, 1, 0)

        '������/�d���惊���N����擾
        Me.ucSeikyuuSiireLinkTui.SetTeibetuRecFromSeikyuuSiireLink(record)

        ' �X�V���O�C�����[�UID
        record.UpdLoginUserId = userinfo.LoginUserId
        ' �X�V���� �ǂݍ��ݎ��̃^�C���X�^���v(�r������p)
        record.UpdDatetime = cl.getDispHaitaUpdTime(Me.HiddenUpdateTeibetuSeikyuuDatetimeTj)

        Return record
    End Function

    ''' <summary>
    ''' �ǉ��H��(�����ݒ�Ώ�)/�@�ʐ������R�[�h�f�[�^�ɃR���g���[���̓��e���Z�b�g���܂�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSeikyuuCtrlDataTjAuto(ByVal recKoujiKkk As KoujiKakakuRecord) As TeibetuSeikyuuRecord

        '�@�ʐ������R�[�h
        Dim record As New TeibetuSeikyuuRecord

        ' �敪
        record.Kbn = ucGyoumuKyoutuu.Kubun
        ' �ۏ؏�NO
        record.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        ' ���ރR�[�h(140)
        record.BunruiCd = EarthConst.SOUKO_CD_TUIKA_KOUJI
        ' ��ʕ\��NO
        record.GamenHyoujiNo = 1
        ' ���i�R�[�h(B2009)
        record.SyouhinCd = EarthConst.SH_CD_JIO2
        ' ��������z
        If Not recKoujiKkk Is Nothing Then
            '�H�����i�}�X�^�ɂ���ꍇ
            record.UriGaku = recKoujiKkk.UriGaku
        Else
            '�H�����i�}�X�^�ɂȂ��ꍇ
            record.UriGaku = 20000
        End If
        ' �d�����z
        record.SiireGaku = 0
        ' �ŋ敪
        SetTjZeiInfo(EarthConst.SH_CD_JIO2)
        record.ZeiKbn = HiddenTjZeiKbn.Value
        ' �ŗ�
        cl.SetDisplayString(HiddenTjZeiritu.Value, record.Zeiritu)
        ' ����Ŋz(���R�[�h�N���X�Ŏ����Z�o�̂��ߏȗ�)

        ' ���������s����
        '���������s���̎����ݒ�
        '������Ѓ}�X�^.�������ߓ�
        Dim dtTmp As Date
        dtTmp = cbLogic.GetSeikyuusyoHakkouDateFromTyousa(TextKjKoujiKaisyaCd.Text)
        If dtTmp <> DateTime.MinValue Then
            TextTjSeikyuusyoHakkouDate.Text = dtTmp.ToString(EarthConst.FORMAT_DATE_TIME_9)
        End If
        cl.SetDisplayString(TextTjSeikyuusyoHakkouDate.Text, record.SeikyuusyoHakDate)
        ' ����N����
        cl.SetDisplayString(Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9), record.UriDate)
        ' �`�[����N����(���W�b�N�N���X�Ŏ����Z�b�g)
        record.DenpyouUriDate = DateTime.MinValue
        ' �������L��
        If Not recKoujiKkk Is Nothing Then
            '�H�����i�}�X�^�ɂ���ꍇ
            record.SeikyuuUmu = recKoujiKkk.SeikyuuUmu
        Else
            '�H�����i�}�X�^�ɂȂ��ꍇ
            record.SeikyuuUmu = 1
        End If
        '����v��FLG
        record.UriKeijyouFlg = 0
        '����v���
        record.UriKeijyouDate = Date.MinValue
        '�m��敪
        record.KakuteiKbn = Integer.MinValue
        '���l
        record.Bikou = Nothing
        '�H���X�����Ŕ����z
        record.KoumutenSeikyuuGaku = 0
        ' ���������z
        record.HattyuusyoGaku = Integer.MinValue
        ' �������m�F��
        record.HattyuusyoKakuninDate = DateTime.MinValue
        '�ꊇ����FLG
        record.IkkatuNyuukinFlg = Integer.MinValue
        '�������Ϗ��쐬��
        record.TysMitsyoSakuseiDate = Date.MinValue
        ' �������m��FLG
        record.HattyuusyoKakuteiFlg = 0
        '������R�[�h
        record.SeikyuuSakiCd = Nothing
        '������}��
        record.SeikyuuSakiBrc = Nothing
        '������敪
        record.SeikyuuSakiKbn = Nothing
        '������ЃR�[�h
        record.TysKaisyaCd = Nothing
        '������Ў��Ə��R�[�h
        record.TysKaisyaJigyousyoCd = Nothing
        '�X�V���O�C�����[�UID
        record.UpdLoginUserId = userinfo.LoginUserId
        ' �X�V���� �ǂݍ��ݎ��̃^�C���X�^���v(�r������p)
        record.UpdDatetime = cl.getDispHaitaUpdTime(Me.HiddenUpdateTeibetuSeikyuuDatetimeTj)

        Return record
    End Function

    ''' <summary>
    ''' �H���񍐏��Ĕ��s/�@�ʐ������R�[�h�f�[�^�ɃR���g���[���̓��e���Z�b�g���܂�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSeikyuuCtrlDataKh() As TeibetuSeikyuuRecord
        ' ���i�R�[�h���ݒ莞�A�����Z�b�g���Ȃ�
        If TextKhSyouhinCd.Text = "" Then
            Return Nothing
        End If

        '�@�ʐ������R�[�h
        Dim record As New TeibetuSeikyuuRecord

        ' �敪
        record.Kbn = ucGyoumuKyoutuu.Kubun
        ' �ۏ؏�NO
        record.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        ' ���ރR�[�h(160)
        record.BunruiCd = EarthConst.SOUKO_CD_KOUJI_HOUKOKUSYO
        ' ��ʕ\��NO
        record.GamenHyoujiNo = 1
        ' ���i�R�[�h
        record.SyouhinCd = TextKhSyouhinCd.Text
        ' ������z
        cl.SetDisplayString(TextKhJituseikyuuKingaku.Text, record.UriGaku)
        ' �d�����z
        cl.SetDisplayString(HiddenKhSiireGaku.Value, record.SiireGaku)
        ' �ŋ敪
        record.ZeiKbn = HiddenKhZeiKbn.Value
        ' �ŗ�
        cl.SetDisplayString(HiddenKhZeiritu.Value, record.Zeiritu)
        ' ����Ŋz
        cl.SetDisplayString(TextKhSyouhizei.Text, record.UriageSyouhiZeiGaku)
        ' �d������Ŋz
        cl.SetDisplayString(HiddenKhSiireSyouhiZei.Value, record.SiireSyouhiZeiGaku)
        ' ���������s��
        cl.SetDisplayString(TextKhSeikyuusyoHakkouDate.Text, record.SeikyuusyoHakDate)
        ' ����N����
        cl.SetDisplayString(TextKhUriageNengappi.Text, record.UriDate)
        ' �`�[����N����(���W�b�N�N���X�Ŏ����Z�b�g)
        record.DenpyouUriDate = DateTime.MinValue
        ' �����L��
        record.SeikyuuUmu = IIf(SelectKhSeikyuuUmu.SelectedValue = "1", 1, 0)
        ' ����v��FLG
        record.UriKeijyouFlg = 0
        ' ����v���
        record.UriKeijyouDate = Date.MinValue
        ' �m��敪
        record.KakuteiKbn = Integer.MinValue
        ' ���l
        record.Bikou = TextKhSaihakkouRiyuu.Text
        ' �H���X�����Ŕ����z
        cl.SetDisplayString(TextKhKoumutenSeikyuuKingaku.Text, record.KoumutenSeikyuuGaku)
        ' ���������z
        cl.SetDisplayString(TextKhHattyuusyoKingaku.Text, record.HattyuusyoGaku)
        ' �������m�F��
        cl.SetDisplayString(TextKhHattyuusyoKakuninDate.Text, record.HattyuusyoKakuninDate)
        ' �ꊇ����FLG
        record.IkkatuNyuukinFlg = Integer.MinValue
        ' �������Ϗ��쐬��
        record.TysMitsyoSakuseiDate = Date.MinValue
        ' �������m��FLG
        record.HattyuusyoKakuteiFlg = IIf(TextKhHattyuusyoKakutei.Text = EarthConst.KAKUTEI, 1, 0)

        '������/�d���惊���N����擾
        Me.ucSeikyuuSiireLink.SetTeibetuRecFromSeikyuuSiireLink(record)

        ' �X�V���O�C�����[�UID
        record.UpdLoginUserId = userinfo.LoginUserId
        ' �X�V���� �ǂݍ��ݎ��̃^�C���X�^���v(�r������p)
        record.UpdDatetime = cl.getDispHaitaUpdTime(Me.HiddenUpdateTeibetuSeikyuuDatetimeKh)

        Return record
    End Function

    ''' <summary>
    ''' �ǉ��H��/�����ݒ莞�ɂ�����n�Ճf�[�^�R�s�[
    ''' ���ǍH���^�u�����ǉ��H���^�u
    ''' </summary>
    ''' <param name="jibanRec"></param>
    ''' <remarks></remarks>
    Private Sub SetJibanDataTjAuto(ByRef jibanRec As JibanRecordKairyouKouji, ByVal recKoujiKkk As KoujiKakakuRecord)

        jibanRec.TKojKaisyaCd = jibanRec.KojGaisyaCd '�H����ЃR�[�h
        jibanRec.TKojKaisyaJigyousyoCd = jibanRec.KojGaisyaJigyousyoCd '�H����Ў��Ə��R�[�h
        jibanRec.TKojSyubetu = jibanRec.KairyKojSyubetu        '�H�����
        jibanRec.TKojKanryYoteiDate = jibanRec.KairyKojKanryYoteiDate '�����\���
        jibanRec.TKojDate = jibanRec.KairyKojDate '�H����
        jibanRec.TKojSokuhouTykDate = jibanRec.KairyKojSokuhouTykDate '���H���񒅓�

        '�H����А���
        If Not recKoujiKkk Is Nothing Then
            '�H�����i�}�X�^�ɂ���ꍇ
            If recKoujiKkk.KojGaisyaSeikyuuUmu = 1 Then
                Me.CheckBoxTjKoujiKaisyaSeikyuu.Checked = True
            Else
                Me.CheckBoxTjKoujiKaisyaSeikyuu.Checked = False
            End If
        Else
            '�H�����i�}�X�^�ɂȂ��ꍇ
            CheckBoxTjKoujiKaisyaSeikyuu.Checked = True '�H����А���(�����ݒ莞�̓`�F�b�N) ���㑱�����ŃZ�b�g
        End If


    End Sub

    ''' <summary>
    ''' �H�����i���R�[�h�擾
    ''' </summary>
    ''' <param name="strKameiCd">�����X�R�[�h</param>
    ''' <param name="strEigyousyoCd">�c�Ə��R�[�h</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="strKojGaisyaCd">�H����ЃR�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKoujiKakakuRec(ByVal strKameiCd As String, _
                                       ByVal strEigyousyoCd As String, _
                                       ByVal strKeiretuCd As String, _
                                       ByVal strSyouhinCd As String, _
                                       ByVal strKojGaisyaCd As String) As KoujiKakakuRecord

        Dim keyRec As New KoujiKakakuKeyRecord          '�����L�[�p���R�[�h
        Dim resultRec As New KoujiKakakuRecord          '���ʎ擾�p���R�[�h
        Dim lgcKouji As New KairyouKoujiLogic           '���ǍH�����W�b�N
        Dim intResult As Integer = Integer.MinValue     '�擾���ʃX�e�[�^�X�p

        '�擾�ɕK�v�ȉ�ʍ��ڂ̃Z�b�g
        keyRec = cbLogic.GetKojKkkMstKeyRec(strKameiCd, strEigyousyoCd, strKeiretuCd, strSyouhinCd, strKojGaisyaCd)

        '�H����Љ��i���W�b�N���擾
        intResult = lgcKouji.GetKoujiKakakuRecord(keyRec, resultRec)

        '�擾�X�e�[�^�X�`�F�b�N(�H�����i�}�X�^�ɂȂ��ꍇ�A���i�}�X�^�͑ΏۊO)
        If intResult <= EarthEnum.emKoujiKakaku.SiteiNasi Then
        Else
            Return Nothing
            Exit Function
        End If

        Return resultRec

    End Function

#End Region

#End Region

    ''' <summary>
    ''' �o�^/�C�� ���s�{�^���P,�Q�������̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTouroku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTouroku1.ServerClick _
                                                                                                                , ButtonTouroku2.ServerClick
        Dim tmpScript As String = ""

        ' ���̓`�F�b�N
        '���ʏ��
        If ucGyoumuKyoutuu.checkInput() = False Then Exit Sub

        '�����񍐏����
        If checkInput() = False Then Exit Sub

        ' ��ʂ̓��e��DB�ɔ��f����
        If SaveData() Then '�o�^����
            '�L�[���̎擾
            _kbn = ucGyoumuKyoutuu.Kubun
            _no = ucGyoumuKyoutuu.Bangou

            ' �p�����[�^�s�����͉�ʂ�\�����Ȃ�
            If _kbn Is Nothing Or _no Is Nothing Then
                Response.Redirect(UrlConst.MAIN)
            End If

            '�������b�Z�[�W���A�������w��|�b�v�A�b�v�\���̂��߂Ƀt���O���Z�b�g
            '�o�^������A��ʂ������[�h���邽�߂ɁA�L�[���������n��
            Context.Items("kbn") = ucGyoumuKyoutuu.Kubun
            Context.Items("no") = ucGyoumuKyoutuu.Bangou
            Context.Items("modal") = Boolean.TrueString

            '��ʑJ�ځi�����[�h�j
            Server.Transfer(UrlConst.KAIRYOU_KOUJI)

        Else '�o�^���s
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "�o�^/�C��") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonTouroku_ServerClick", tmpScript, True)

        End If

    End Sub

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
    Public Function checkInput(Optional ByVal flgNextGamen As Boolean = False) As Boolean
        Dim e As New System.EventArgs
        Dim KjLogic As New KairyouKoujiLogic

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
            '���R�[�h���͒l�ύX�`�F�b�N
            '�H����ЃR�[�h
            If TextKjKoujiKaisyaCd.Text <> HiddenKjKoujiKaisyaCdMae.Value Or (TextKjKoujiKaisyaCd.Text <> "" And Me.TextKjKoujiKaisyaMei.Text = "") Then
                errMess += Messages.MSG030E.Replace("@PARAM1", "�H����ЃR�[�h")
                arrFocusTargetCtrl.Add(ButtonKjKoujiKaisyaSearch)
            End If
            '�ǉ��H����ЃR�[�h
            If TextTjKoujiKaisyaCd.Text <> HiddenTjKoujiKaisyaCdMae.Value Or (TextTjKoujiKaisyaCd.Text <> "" And Me.TextTjKoujiKaisyaMei.Text = "") Then
                errMess += Messages.MSG030E.Replace("@PARAM1", "�ǉ��H����ЃR�[�h")
                arrFocusTargetCtrl.Add(ButtonTjKoujiKaisyaSearch)
            End If

            '���K�{�`�F�b�N
            '******************************
            '* <���ǍH��>
            '******************************
            '(Chk27:<���ǍH�����><���ǍH��>���i�R�[�h��"B2000�ԑ�"�A���A<���ǍH�����><���ǍH��>����N���������́A���A<���ǍH�����><���ǍH��>���㏈����<>"���㏈����"�̏ꍇ�A�`�F�b�N���s���B)
            If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) _
                And TextKjUriageNengappi.Text <> "" _
                    And SpanKjUriageSyoriZumi.InnerHtml <> EarthConst.URIAGE_ZUMI _
                        Then
                '�H���d�l�m�F
                If SelectKjKoujiSiyouKakunin.SelectedValue = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "�H���d�l�m�F")
                    arrFocusTargetCtrl.Add(SelectKjKoujiSiyouKakunin)
                End If
            End If

            '(Chk23:<���ǍH�����><���ǍH��>���i�R�[�h��"B2008"�iJIO�Œ躰��1�j�A���A<���ǍH�����><���ǍH��>����N���������͂̏ꍇ�A�`�F�b�N���s���B
            If SelectKjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO And TextKjUriageNengappi.Text <> "" Then
                '�H����ЃR�[�h
                If TextKjKoujiKaisyaCd.Text = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "�H����ЃR�[�h")
                    arrFocusTargetCtrl.Add(TextKjKoujiKaisyaCd)
                End If
                '���ǍH�����
                If SelectKjKairyouKoujiSyubetu.SelectedValue = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "���ǍH�����")
                    arrFocusTargetCtrl.Add(SelectKjKairyouKoujiSyubetu)
                End If
            End If

            '******************************
            '* <�ǉ��H��>
            '******************************
            '(Chk24:<���ǍH�����><�ǉ��H��>���i�R�[�h��"B2009"�iJIO�Œ躰��2�j�̏ꍇ�A�`�F�b�N���s���B
            If SelectTjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO2 Then
                '�ǉ��H����ЃR�[�h
                If TextTjKoujiKaisyaCd.Text = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "�ǉ��H����ЃR�[�h")
                    arrFocusTargetCtrl.Add(TextTjKoujiKaisyaCd)
                End If
                '�ǉ����ǍH�����
                If SelectTjKairyouKoujiSyubetu.SelectedValue = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "�ǉ����ǍH�����")
                    arrFocusTargetCtrl.Add(SelectTjKairyouKoujiSyubetu)
                End If
            End If

            '******************************
            '* <���ǍH���񍐏����>
            '******************************
            '(Chk21:�񍐏����.�H���񍐏��Ĕ��s�������͂̏ꍇ�A�`�F�b�N���s���B)
            If TextKhSaihakkouDate.Text <> "" Then
                '����
                If SelectKhSeikyuuUmu.SelectedValue = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "����")
                    arrFocusTargetCtrl.Add(SelectKhSeikyuuUmu)
                End If
                '�Ĕ��s���R
                If TextKhSaihakkouRiyuu.Text.Length = 0 Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "�Ĕ��s���R")
                    arrFocusTargetCtrl.Add(TextKhSaihakkouRiyuu)
                End If

                '(Chk22:�񍐏����.�H���񍐏��Ĕ��s�������́A���A�񍐏����.<�H���񍐏��Ĕ��s>�����L�����L��̏ꍇ�A�`�F�b�N���s���B)
                '�����L��
                If SelectKhSeikyuuUmu.SelectedValue = "1" Then '�L
                    '�������Ŕ����z
                    If TextKhJituseikyuuKingaku.Text.Length = 0 Then
                        errMess += Messages.MSG013E.Replace("@PARAM1", "�������Ŕ����z")
                        arrFocusTargetCtrl.Add(TextKhJituseikyuuKingaku)
                    End If
                    '���������s��
                    If TextKhSeikyuusyoHakkouDate.Text.Length = 0 Then
                        errMess += Messages.MSG013E.Replace("@PARAM1", "���������s��")
                        arrFocusTargetCtrl.Add(TextKhSeikyuusyoHakkouDate)
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
        '****************
        '* ���ǍH��
        '****************
        '�m�F��
        If TextKjKakuninDate.Text <> "" Then
            If cl.checkDateHanni(TextKjKakuninDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�m�F��")
                arrFocusTargetCtrl.Add(TextKjKakuninDate)
            End If
        End If
        '�����\���
        If TextKjKanryouYoteiDate.Text <> "" Then
            If cl.checkDateHanni(TextKjKanryouYoteiDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�����\���")
                arrFocusTargetCtrl.Add(TextKjKanryouYoteiDate)
            End If
        End If
        '�H����
        If TextKjKoujiDate.Text <> "" Then
            If cl.checkDateHanni(TextKjKoujiDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�H����")
                arrFocusTargetCtrl.Add(TextKjKoujiDate)
            End If
            If Me.TextKjKoujiDate.Text <> Me.HiddenKjKoujiDateOld.Value Then '�������t�͓��͋֎~
                If Me.TextKjKoujiDate.Text > Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) Then
                    errMess += Messages.MSG204E.Replace("@PARAM1", "�H����")
                    arrFocusTargetCtrl.Add(Me.TextKjKoujiDate)
                End If
            End If
        End If
        '���H���񒅓�
        If TextKjKankouSokuhouTyakuDate.Text <> "" Then
            If cl.checkDateHanni(TextKjKankouSokuhouTyakuDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "���H���񒅓�")
                arrFocusTargetCtrl.Add(TextKjKankouSokuhouTyakuDate)
            End If
            If Me.TextKjKankouSokuhouTyakuDate.Text <> Me.HiddenKjKankouSokuhouTyakuDateOld.Value Then '�������t�͓��͋֎~
                If Me.TextKjKankouSokuhouTyakuDate.Text > Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) Then
                    errMess += Messages.MSG204E.Replace("@PARAM1", "���H���񒅓�")
                    arrFocusTargetCtrl.Add(Me.TextKjKankouSokuhouTyakuDate)
                End If
            End If
        End If
        '���������s��
        If TextKjSeikyuusyoHakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextKjSeikyuusyoHakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "���������s��")
                arrFocusTargetCtrl.Add(TextKjSeikyuusyoHakkouDate)
            End If
        End If
        '****************
        '* �ǉ��H��
        '****************
        '�����\���
        If TextTjKanryouYoteiDate.Text <> "" Then
            If cl.checkDateHanni(TextTjKanryouYoteiDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�����\���")
                arrFocusTargetCtrl.Add(TextTjKanryouYoteiDate)
            End If
        End If
        '�H����
        If TextTjKoujiDate.Text <> "" Then
            If cl.checkDateHanni(TextTjKoujiDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "�H����")
                arrFocusTargetCtrl.Add(TextTjKoujiDate)
            End If
            If Me.TextTjKoujiDate.Text <> Me.HiddenTjKoujiDateOld.Value Then '�������t�͓��͋֎~
                If Me.TextTjKoujiDate.Text > Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) Then
                    errMess += Messages.MSG204E.Replace("@PARAM1", "�H����")
                    arrFocusTargetCtrl.Add(Me.TextTjKoujiDate)
                End If
            End If
        End If
        '���H���񒅓�
        If TextTjKankouSokuhouTyakuDate.Text <> "" Then
            If cl.checkDateHanni(TextTjKankouSokuhouTyakuDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "���H���񒅓�")
                arrFocusTargetCtrl.Add(TextTjKankouSokuhouTyakuDate)
            End If
            If Me.TextTjKankouSokuhouTyakuDate.Text <> Me.HiddenTjKankouSokuhouTyakuDateOld.Value Then '�������t�͓��͋֎~
                If Me.TextTjKankouSokuhouTyakuDate.Text > Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) Then
                    errMess += Messages.MSG204E.Replace("@PARAM1", "���H���񒅓�")
                    arrFocusTargetCtrl.Add(Me.TextTjKankouSokuhouTyakuDate)
                End If
            End If
        End If
        '���������s��
        If TextTjSeikyuusyoHakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextTjSeikyuusyoHakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "���������s��")
                arrFocusTargetCtrl.Add(TextTjSeikyuusyoHakkouDate)
            End If
        End If
        '****************
        '* �H���񍐏��Ĕ��s
        '****************
        '���������s��
        If TextKhSeikyuusyoHakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextKhSeikyuusyoHakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "���������s��")
                arrFocusTargetCtrl.Add(TextKhSeikyuusyoHakkouDate)
            End If
        End If

        '�������`�F�b�N
        '�H�����
        If jBn.SuutiStrCheck(TextKjKoujiKaisyaCd.Text, 7, 0) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "�H�����").Replace("@PARAM2", "7").Replace("@PARAM3", "0")
            arrFocusTargetCtrl.Add(TextKjKoujiKaisyaCd)
        End If
        '�ǉ��H�����
        If jBn.SuutiStrCheck(TextTjKoujiKaisyaCd.Text, 7, 0) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "�ǉ��H�����").Replace("@PARAM2", "7").Replace("@PARAM3", "0")
            arrFocusTargetCtrl.Add(TextTjKoujiKaisyaCd)
        End If

        '���֑������`�F�b�N(��������̓t�B�[���h���Ώ�)
        '******************************
        '* <���ǍH���񍐏����>
        '******************************
        '�󗝏ڍ�
        If jBn.KinsiStrCheck(TextKhJuriSyousai.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�󗝏ڍ�")
            arrFocusTargetCtrl.Add(TextKhJuriSyousai)
        End If
        '�Ĕ��s���R
        If jBn.KinsiStrCheck(TextKhSaihakkouRiyuu.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�Ĕ��s���R")
            arrFocusTargetCtrl.Add(TextKhSaihakkouRiyuu)
        End If

        '���o�C�g���`�F�b�N(��������̓t�B�[���h���Ώ�)
        '******************************
        '* <���ǍH���񍐏����>
        '******************************
        '�󗝏ڍ�
        If jBn.ByteCheckSJIS(TextKhJuriSyousai.Text, TextKhJuriSyousai.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�󗝏ڍ�")
            arrFocusTargetCtrl.Add(TextKhJuriSyousai)
        End If
        '�Ĕ��s���R
        If jBn.ByteCheckSJIS(TextKhSaihakkouRiyuu.Text, TextKhSaihakkouRiyuu.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�Ĕ��s���R")
            arrFocusTargetCtrl.Add(TextKhSaihakkouRiyuu)
        End If

        '�����̑��`�F�b�N
        '******************************
        '* <���ǍH��,�ǉ��H��>
        '******************************
        '(Chk25:<���ǍH�����><���ǍH��>���i�R�[�h�����́A���A<���ǍH�����><�ǉ��H��>���i�R�[�h�����́A���A�ȉ��̏����̂����ꂩ�ɓ��Ă͂܂�ꍇ�A�G���[�Ƃ���B)
        If SelectKjSyouhinCd.SelectedValue <> "" And SelectTjSyouhinCd.SelectedValue <> "" Then
            '�E<���ǍH�����><���ǍH��>���i�R�[�h��"B2008"�iJIO�Œ躰��1�j�A���A<���ǍH�����><�ǉ��H��>���i�R�[�h��"B2009"�iJIO�Œ躰��2�j
            If SelectKjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO And SelectTjSyouhinCd.SelectedValue <> EarthConst.SH_CD_JIO2 Then
                errMess += Messages.MSG077W
                arrFocusTargetCtrl.Add(SelectTjSyouhinCd)
            End If
            '�E<���ǍH�����><���ǍH��>���i�R�[�h��"B2008"�iJIO�Œ躰��1�j�A���A<���ǍH�����><�ǉ��H��>���i�R�[�h��"B2009"�iJIO�Œ躰��2�j
            If SelectKjSyouhinCd.SelectedValue <> EarthConst.SH_CD_JIO And SelectTjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO2 Then
                errMess += Messages.MSG077W
                arrFocusTargetCtrl.Add(SelectKjSyouhinCd)
            End If
        End If

        '(Chk26:�H����ʂ�B2008�̎��͍H����Ђ�999800�ɑI��������o�^���G���[�ɂ���)
        '���ǍH��.���i�R�[�h
        If SelectKjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO Then
            '���ǍH��.�H����ЃR�[�h
            If TextKjKoujiKaisyaCd.Text = EarthConst.KOJ_K_CD_JIO Then '�H���d�l(999800)
                errMess += Messages.MSG101E
                arrFocusTargetCtrl.Add(TextKjKoujiKaisyaCd)
            End If
        End If

        '(Chk28:<���ǍH�����><���ǍH��>���i�R�[�h��"B2008"�iJIO�Œ躰��1�j�A���A���ǍH��.�H����Ђɓ��͂�����ꍇ�ɁA�ȉ��̏����̃`�F�b�N���s�Ȃ��B
        '<���ǍH�����><���ʏ��>�����X�R�[�h�{<���ǍH�����><���ǍH��>�H�����(������ЃR�[�h)�������X������Аݒ�}�X�^�Ɏw��H����Ђ̓o�^���Ȃ��ꍇ�A�G���[�Ƃ���B)
        '���ǍH��.���i�R�[�h
        If SelectKjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO Then
            '���ǍH��.�H�����=���͂��A���ǍH��.���H���񒅓�=����
            If TextKjKoujiKaisyaCd.Text <> String.Empty And Me.TextKjKankouSokuhouTyakuDate.Text <> String.Empty Then
                '���ǍH��.�H����ЃR�[�h
                If KjLogic.ChkExistSiteiKoujiGaisya(ucGyoumuKyoutuu.AccKameitenCd.Value, TextKjKoujiKaisyaCd.Text) = False Then '�w�蒲����Ђ̃`�F�b�N
                    errMess += Messages.MSG114E
                    arrFocusTargetCtrl.Add(TextKjKoujiKaisyaCd)
                End If
            End If

        End If

        '******************************
        '* <���ǍH��>
        '******************************
        '(Chk05:<���ǍH�����>���ǔ�����z(�Ŕ�)��<���ǍH�����>���ǎd�����z(�Ŕ�)�A���A�o�^�����Ă��Ȃ��ꍇ�A�m�F���b�Z�[�W��\������B)
        '�i���͓��e���ύX����Ă���ꍇ�́A���𖳌��ɂ��A�m�F���b�Z�[�W��\������j	
        '�m�FOK�̏ꍇ�A�o�^������B	
        '<���ǍH�����>�ǉ����ǔ�����z�A�ǉ����ǎd�����z�����l�̃`�F�b�N���s���B	
        '������z/�Ŕ����z=>JS�ɂă`�F�b�N

        '(Chk14:<���ǍH�����><���ǍH��>���H���񒅓������́A���A�V�K�o�^�̏ꍇ�A�m�F���b�Z�[�W��\������B)
        '�m�FOK�̏ꍇ�A�o�^������B�i2�x�ڂ���́A�m�F���b�Z�[�W��\�������ɁA�������œo�^������j
        '���H���񒅓�=>JS�ɂă`�F�b�N

        '(Chk08:�H�����.<���ǍH��>���������s�������́A���A���ǍH�����������͂̏ꍇ�A�m�F���b�Z�[�W��\������B)
        '�m�FOK�̏ꍇ�A�o�^������B�i2�x�ڂ���́A�m�F���b�Z�[�W��\�������ɁA�������œo�^������j	
        '���������s��=>JS�ɂă`�F�b�N

        '(Chk09:�H�����.<���ǍH��>���������s�������ǍH�����̏ꍇ�A�m�F���b�Z�[�W��\������B)
        '�m�FOK�̏ꍇ�A�o�^������B�i2�x�ڂ���́A�m�F���b�Z�[�W��\�������ɁA�������œo�^������j	
        '���������s��=>JS�ɂă`�F�b�N

        '(Chk16:�����L�����L��A���A����N�������ݒ�ρA���A���������s���������͂̏ꍇ�A�G���[�Ƃ���B)
        '���������s��
        If SelectKjSeikyuuUmu.SelectedValue = "1" And TextKjUriageNengappi.Text <> "" And TextKjSeikyuusyoHakkouDate.Text = "" Then
            errMess += Messages.MSG062E.Replace("@PARAM1", "���������s��")
            arrFocusTargetCtrl.Add(TextKjSeikyuusyoHakkouDate)
        End If

        '******************************
        '* <�ǉ��H��>
        '******************************
        '(Chk05:<���ǍH�����>���ǔ�����z(�Ŕ�)��<���ǍH�����>���ǎd�����z(�Ŕ�)�A���A�o�^�����Ă��Ȃ��ꍇ�A�m�F���b�Z�[�W��\������B)
        '�i���͓��e���ύX����Ă���ꍇ�́A���𖳌��ɂ��A�m�F���b�Z�[�W��\������j	
        '�m�FOK�̏ꍇ�A�o�^������B	
        '<���ǍH�����>�ǉ����ǔ�����z�A�ǉ����ǎd�����z�����l�̃`�F�b�N���s���B	
        '������z/�Ŕ����z=>JS�ɂă`�F�b�N

        '(Chk15:<���ǍH�����><�ǉ��H��>�ǉ����H���񒅓������́A���A�V�K�o�^�̏ꍇ�A�m�F���b�Z�[�W��\������B)
        '�m�FOK�̏ꍇ�A�o�^������B�i2�x�ڂ���́A�m�F���b�Z�[�W��\�������ɁA�������œo�^������j
        '���H���񒅓�=>JS�ɂă`�F�b�N

        '(Chk10:<���ǍH�����><�ǉ��H��>���������s�������́A���A�ǉ����ǍH�����������͂̏ꍇ�A�m�F���b�Z�[�W��\������B)
        '�m�FOK�̏ꍇ�A�o�^������B�i2�x�ڂ���́A�m�F���b�Z�[�W��\�������ɁA�������œo�^������j	
        '���������s��=>JS�ɂă`�F�b�N

        '(Chk11:<���ǍH�����><�ǉ��H��>���������s�����ǉ����ǍH�����̏ꍇ�A�m�F���b�Z�[�W��\������B)
        '�m�FOK�̏ꍇ�A�o�^������B�i2�x�ڂ���́A�m�F���b�Z�[�W��\�������ɁA�������œo�^������j	
        '���������s��=>JS�ɂă`�F�b�N

        '(Chk16:�����L�����L��A���A����N�������ݒ�ρA���A���������s���������͂̏ꍇ�A�G���[�Ƃ���B)
        '���������s��
        If SelectTjSeikyuuUmu.SelectedValue = "1" And TextTjUriageNengappi.Text <> "" And TextTjSeikyuusyoHakkouDate.Text = "" Then
            errMess += Messages.MSG062E.Replace("@PARAM1", "���������s��")
            arrFocusTargetCtrl.Add(TextTjSeikyuusyoHakkouDate)
        End If


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
    Protected Function SaveData() As Boolean
        '*************************
        '�n�Ճf�[�^�͍X�V�Ώۂ̂�
        '�@�ʐ����f�[�^�͑S�čX�V
        '*************************

        Dim JibanLogic As New JibanLogic
        Dim jrOld As New JibanRecord
        ' ���݂̒n�Ճf�[�^��DB����擾����
        jrOld = JibanLogic.GetJibanData(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou)

        ' ��ʓ��e���n�Ճ��R�[�h�𐶐�����
        Dim jibanRec As New JibanRecordKairyouKouji

        ' �@�ʃf�[�^�C���p�̃��W�b�N�N���X
        Dim logic As New KairyouKoujiLogic

        '�i��T�X�V�p�ɁADB��̒l���Z�b�g����
        JibanLogic.SetSintyokuJibanData(jrOld, jibanRec)

        '���i1�`3�̃R�s�[
        JibanLogic.ps_CopyTeibetuSyouhinData(jrOld, jibanRec)

        '***************************************
        ' �n�Ճf�[�^(���ʏ��)
        '***************************************
        ' �敪
        jibanRec.Kbn = ucGyoumuKyoutuu.Kubun
        ' �ԍ��i�ۏ؏�NO�j
        jibanRec.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        ' �f�[�^�j�����
        cl.SetDisplayString(ucGyoumuKyoutuu.DataHakiSyubetu, jibanRec.DataHakiSyubetu)
        ' �f�[�^�j����
        If ucGyoumuKyoutuu.DataHakiSyubetu = "0" Then
            jibanRec.DataHakiDate = DateTime.MinValue
        Else
            cl.SetDisplayString(ucGyoumuKyoutuu.DataHakiDate, jibanRec.DataHakiDate)
        End If
        ' �{�喼
        jibanRec.SesyuMei = ucGyoumuKyoutuu.SesyuMei
        ' �����Z��1
        jibanRec.BukkenJyuusyo1 = ucGyoumuKyoutuu.Jyuusyo1
        ' �����Z��2
        jibanRec.BukkenJyuusyo2 = ucGyoumuKyoutuu.Jyuusyo2
        ' �����Z��3
        jibanRec.BukkenJyuusyo3 = ucGyoumuKyoutuu.Jyuusyo3
        ' ���l
        jibanRec.Bikou = ucGyoumuKyoutuu.Bikou
        ' ���l2
        jibanRec.Bikou2 = ucGyoumuKyoutuu.Bikou2
        ' �X�V�҃��[�U�[ID
        jibanRec.UpdLoginUserId = userinfo.LoginUserId
        ' �X�V���� �ǂݍ��ݎ��̃^�C���X�^���v
        If ucGyoumuKyoutuu.AccupdateDateTime.Value = "" Then
            jibanRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
        Else
            jibanRec.UpdDatetime = DateTime.ParseExact(ucGyoumuKyoutuu.AccupdateDateTime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
        End If
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

        '***************************************
        ' �n�Ճf�[�^(���ǍH�����)
        '***************************************
        '�H���S���҃R�[�h
        cl.SetDisplayString(TextKoujiTantousyaCd.Text, jibanRec.SyouninsyaCd)

        '**********************
        ' �n�Ճf�[�^(���ǍH��)
        '**********************
        '�H���d�l�m�F
        cl.SetDisplayString(SelectKjKoujiSiyouKakunin.SelectedValue, jibanRec.KojSiyouKakunin)
        '�m�F��
        cl.SetDisplayString(TextKjKakuninDate.Text, jibanRec.KojSiyouKakuninDate)
        '�H����Г�
        Dim tmpTys As String = TextKjKoujiKaisyaCd.Text
        If tmpTys.Length >= 6 Then   '����6�ȏ�K�{
            jibanRec.KojGaisyaCd = tmpTys.Substring(0, tmpTys.Length - 2) '�H����ЃR�[�h
            jibanRec.KojGaisyaJigyousyoCd = tmpTys.Substring(tmpTys.Length - 2, 2) '�H����Ў��Ə��R�[�h
        End If
        '�H����А���
        Dim intTmp1 As Integer = IIf(CheckBoxKjKoujiKaisyaSeikyuu.Checked = True, 1, Integer.MinValue)
        cl.SetDisplayString(intTmp1, jibanRec.KojGaisyaSeikyuuUmu)
        '���ǍH�����
        cl.SetDisplayString(SelectKjKairyouKoujiSyubetu.SelectedValue, jibanRec.KairyKojSyubetu)
        '�����\���
        cl.SetDisplayString(TextKjKanryouYoteiDate.Text, jibanRec.KairyKojKanryYoteiDate)
        '�H����
        cl.SetDisplayString(TextKjKoujiDate.Text, jibanRec.KairyKojDate)
        '���H���񒅓�
        cl.SetDisplayString(TextKjKankouSokuhouTyakuDate.Text, jibanRec.KairyKojSokuhouTykDate)


        '***************************************
        ' �@�ʐ����f�[�^(���ǍH��)
        '***************************************
        jibanRec.KairyouKoujiRecord = GetSeikyuuCtrlDataKj()

        '**********************
        ' �n�Ճf�[�^(�ǉ��H��)
        '**********************
        '�H����Г�
        Dim tmpTysTuika As String = TextTjKoujiKaisyaCd.Text
        If tmpTysTuika.Length >= 6 Then   '����6�ȏ�K�{
            jibanRec.TKojKaisyaCd = tmpTysTuika.Substring(0, tmpTysTuika.Length - 2) '�H����ЃR�[�h
            jibanRec.TKojKaisyaJigyousyoCd = tmpTysTuika.Substring(tmpTysTuika.Length - 2, 2) '�H����Ў��Ə��R�[�h
        End If
        '���ǍH�����
        cl.SetDisplayString(SelectTjKairyouKoujiSyubetu.SelectedValue, jibanRec.TKojSyubetu)
        '�����\���
        cl.SetDisplayString(TextTjKanryouYoteiDate.Text, jibanRec.TKojKanryYoteiDate)
        '�H����
        cl.SetDisplayString(TextTjKoujiDate.Text, jibanRec.TKojDate)
        '���H���񒅓�
        cl.SetDisplayString(TextTjKankouSokuhouTyakuDate.Text, jibanRec.TKojSokuhouTykDate)

        '***************************************
        ' �@�ʐ����f�[�^(�ǉ��H��)
        '***************************************
        '�ǉ��H���f�[�^�����ݒ�`�F�b�N
        Dim recKoujiKkk As New KoujiKakakuRecord
        '�H�����.<���ǍH��>���i�R�[�h��"B2008"�iJIO�Œ躰��1�j�A���H�����.<���ǍH��>����N���������͂̏ꍇ
        If SelectKjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO And TextKjUriageNengappi.Text <> String.Empty Then

            '�@�ʐ������R�[�h(���ރR�[�h=140)�̑��݃`�F�b�N
            If logic.ChkTjDataAutoSetting(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou) = False Then
                jibanRec.TuikaKoujiRecord = GetSeikyuuCtrlDataTj() '�����ݒ�ΏۊO

            Else '��L�ȊO�̏ꍇ

                '���H���񒅓�
                If Me.TextKjKankouSokuhouTyakuDate.Text = String.Empty Then '������
                    jibanRec.TuikaKoujiRecord = GetSeikyuuCtrlDataTj() '�����ݒ�ΏۊO

                Else '����
                    recKoujiKkk = GetKoujiKakakuRec(jrOld.KameitenCd, _
                                                    jrOld.EigyousyoCd, _
                                                    jrOld.KeiretuCd, _
                                                    EarthConst.SH_CD_JIO2, _
                                                    jibanRec.KojGaisyaCd + jibanRec.KojGaisyaJigyousyoCd)
                    SetJibanDataTjAuto(jibanRec, recKoujiKkk) '�����ݒ莞�ɂ�����n�Ճf�[�^�R�s�[
                    jibanRec.TuikaKoujiRecord = GetSeikyuuCtrlDataTjAuto(recKoujiKkk) '�����ݒ�Ώ�
                End If

            End If

        Else '��L�ȊO�̏ꍇ
            jibanRec.TuikaKoujiRecord = GetSeikyuuCtrlDataTj() '�����ݒ�ΏۊO

        End If

        '�H����А���
        Dim intTmp2 As Integer = IIf(CheckBoxTjKoujiKaisyaSeikyuu.Checked = True, 1, Integer.MinValue)
        cl.SetDisplayString(intTmp2, jibanRec.TKojKaisyaSeikyuuUmu)

        '***************************************
        ' �n�Ճf�[�^(���ǍH���񍐏����)
        '***************************************
        '��
        cl.SetDisplayString(SelectKhJuri.SelectedValue, jibanRec.KojHkksUmu)
        '�󗝏ڍ�
        jibanRec.KojHkksJuriSyousai = TextKhJuriSyousai.Text
        '�󗝓�
        cl.SetDisplayString(TextKhJuriDate.Text, jibanRec.KojHkksJuriDate)
        '������
        cl.SetDisplayString(TextKhHassouDate.Text, jibanRec.KojHkksHassouDate)
        '�Ĕ��s��
        cl.SetDisplayString(TextKhSaihakkouDate.Text, jibanRec.KojHkksSaihakDate)

        '***************************************
        ' �@�ʐ����f�[�^(���ǍH���񍐏�)
        '***************************************
        jibanRec.KoujiHoukokusyoRecord = GetSeikyuuCtrlDataKh()

        '***************************************
        ' ��ʓ��͍��ڈȊO
        '***************************************
        '�X�V��
        jibanRec.Kousinsya = cbLogic.GetKousinsya(userinfo.LoginUserId, DateTime.Now)

        '************************************************
        ' �ۏ؏����s�󋵁A�ۏ؏����s�󋵐ݒ���A�ۏ؏��i�L���̎����ݒ�
        '************************************************
        '���i�̎����ݒ��ɍs�Ȃ�
        cbLogic.ps_AutoSetHosyousyoHakJyjy(EarthEnum.emGamenInfo.KairyouKouji, jibanRec)

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
        '*********************************************************
        '===========================================================

        ' �f�[�^�̍X�V���s���܂�
        If logic.SaveJibanData(Me, jibanRec, brRec) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispAction()
        Dim jBn As New Jiban '�n�Չ�ʃN���X

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �v���_�E��/�R�[�h���͘A�g�ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        jBn.SetPullCdScriptSrc(TextKoujiTantousyaCd, SelectKoujiTantousya) '�H���S����

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript�֘A�Z�b�g
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim onFocusPostBackScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScript As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this)){__doPostBack(this.id,'');}}else{checkNumberAddFig(this);}"
        Dim onFocusScript As String = "removeFig(this);"
        Dim onBlurScript As String = "checkNumberAddFig(this);"
        Dim checkDate As String = "checkDate(this);"
        Dim checkKingaku As String = "checkKingaku(this);"
        Dim checkNumber As String = "checkNumber(this);"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '���z�p(�o�^����)
        Dim onBlurPostBackScriptKingakuKjChk05 As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this)){document.getElementById('" & HiddenKjChk05.ClientID & "').value='';__doPostBack(this.id,'');}}else{checkNumberAddFig(this);}"
        Dim onBlurPostBackScriptKingakuTjChk05 As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this)){document.getElementById('" & HiddenTjChk05.ClientID & "').value='';__doPostBack(this.id,'');}}else{checkNumberAddFig(this);}"

        '���t���ڗp
        Dim onFocusPostBackScriptDate As String = "setTempValueForOnBlur(this);"
        Dim onBlurPostBackScriptDate As String = "if(checkTempValueForOnBlur(this)){if(checkDate(this)){__doPostBack(this.id,'');}}else{checkDate(this);}"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ �H����ЃR�[�h�A�ǉ��H����ЃR�[�h����у{�^��
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        TextKjKoujiKaisyaCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callKjKoujiKaisyaSearch(this);};copyToKjHidden(this);"
        TextKjKoujiKaisyaCd.Attributes("onfocus") = "setTempValueForOnBlur(this);"
        TextTjKoujiKaisyaCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callTjKoujiKaisyaSearch(this);};copyToTjHidden(this);"
        TextTjKoujiKaisyaCd.Attributes("onfocus") = "setTempValueForOnBlur(this);"

        ButtonKjKoujiKaisyaSearch.Attributes("onclick") = "SetChangeMaeValue('" & HiddenKjKoujiKaisyaCdMae.ClientID & "','" & TextKjKoujiKaisyaCd.ClientID & "');"
        ButtonKjKoujiKaisyaSearch.Attributes("onmousedown") = "JSkoujiKaisyaSearchTypeKj=9;"
        ButtonKjKoujiKaisyaSearch.Attributes("onkeydown") = "if(event.keyCode==13||event.keyCode==32)JSkoujiKaisyaSearchTypeKj=9;"
        ButtonTjKoujiKaisyaSearch.Attributes("onclick") = "SetChangeMaeValue('" & HiddenTjKoujiKaisyaCdMae.ClientID & "','" & TextTjKoujiKaisyaCd.ClientID & "');"
        ButtonTjKoujiKaisyaSearch.Attributes("onmousedown") = "JSkoujiKaisyaSearchTypeTj=9;"
        ButtonTjKoujiKaisyaSearch.Attributes("onkeydown") = "if(event.keyCode==13||event.keyCode==32)JSkoujiKaisyaSearchTypeTj=9;"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ���z�n
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '***********************
        '* ���ǍH��
        '***********************
        '������z/�Ŕ����z
        TextKjUriageZeinukiKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextKjUriageZeinukiKingaku.Attributes("onblur") = onBlurPostBackScriptKingakuKjChk05
        TextKjUriageZeinukiKingaku.Attributes("onkeydown") = disabledOnkeydown
        '�d�����z/�Ŕ����z
        TextKjSiireZeinukiKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextKjSiireZeinukiKingaku.Attributes("onblur") = onBlurPostBackScriptKingakuKjChk05
        TextKjSiireZeinukiKingaku.Attributes("onkeydown") = disabledOnkeydown
        '***********************
        '* �ǉ��H��
        '***********************
        '������z/�Ŕ����z
        TextTjUriageZeinukiKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextTjUriageZeinukiKingaku.Attributes("onblur") = onBlurPostBackScriptKingakuTjChk05
        TextTjUriageZeinukiKingaku.Attributes("onkeydown") = disabledOnkeydown
        '�d�����z/�Ŕ����z
        TextTjSiireZeinukiKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextTjSiireZeinukiKingaku.Attributes("onblur") = onBlurPostBackScriptKingakuTjChk05
        TextTjSiireZeinukiKingaku.Attributes("onkeydown") = disabledOnkeydown
        '***********************
        '* ���ǍH���񍐏�
        '***********************
        '�H���X�����Ŕ����z
        TextKhKoumutenSeikyuuKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextKhKoumutenSeikyuuKingaku.Attributes("onblur") = onBlurPostBackScript
        TextKhKoumutenSeikyuuKingaku.Attributes("onkeydown") = disabledOnkeydown
        '�������Ŕ����z
        TextKhJituseikyuuKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextKhJituseikyuuKingaku.Attributes("onblur") = onBlurPostBackScript
        TextKhJituseikyuuKingaku.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ���t�n
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '***********************
        '* ���ǍH��
        '***********************
        '�m�F��
        TextKjKakuninDate.Attributes("onblur") = checkDate
        TextKjKakuninDate.Attributes("onkeydown") = disabledOnkeydown
        '�����\���
        TextKjKanryouYoteiDate.Attributes("onblur") = checkDate
        TextKjKanryouYoteiDate.Attributes("onkeydown") = disabledOnkeydown
        '�H����
        TextKjKoujiDate.Attributes("onblur") = checkDate
        TextKjKoujiDate.Attributes("onkeydown") = disabledOnkeydown
        '���H���񒅓���
        TextKjKankouSokuhouTyakuDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextKjKankouSokuhouTyakuDate.Attributes("onfocus") = onFocusPostBackScriptDate
        TextKjKankouSokuhouTyakuDate.Attributes("onkeydown") = disabledOnkeydown
        '���������s��
        TextKjSeikyuusyoHakkouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextKjSeikyuusyoHakkouDate.Attributes("onfocus") = onFocusPostBackScriptDate
        TextKjSeikyuusyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown

        '***********************
        '* �ǉ��H��
        '***********************
        '�����\���
        TextTjKanryouYoteiDate.Attributes("onblur") = checkDate
        TextTjKanryouYoteiDate.Attributes("onkeydown") = disabledOnkeydown
        '�H����
        TextTjKoujiDate.Attributes("onblur") = checkDate
        TextTjKoujiDate.Attributes("onkeydown") = disabledOnkeydown
        '���H���񒅓���
        TextTjKankouSokuhouTyakuDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextTjKankouSokuhouTyakuDate.Attributes("onfocus") = onFocusPostBackScriptDate
        TextTjKankouSokuhouTyakuDate.Attributes("onkeydown") = disabledOnkeydown
        '���������s��
        TextTjSeikyuusyoHakkouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextTjSeikyuusyoHakkouDate.Attributes("onfocus") = onFocusPostBackScriptDate
        TextTjSeikyuusyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown

        '***********************
        '* ���ǍH���񍐏����
        '***********************
        '�󗝓�
        TextKhJuriDate.Attributes("onblur") = checkDate
        TextKhJuriDate.Attributes("onkeydown") = disabledOnkeydown
        '������
        TextKhHassouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextKhHassouDate.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenKhHassouDateMae.ClientID & "','" & TextKhHassouDate.ClientID & "');" & onFocusPostBackScriptDate
        TextKhHassouDate.Attributes("onkeydown") = disabledOnkeydown
        '�Ĕ��s��
        TextKhSaihakkouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextKhSaihakkouDate.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenKhSaihakkouDateMae.ClientID & "','" & TextKhSaihakkouDate.ClientID & "');" & onFocusPostBackScriptDate
        TextKhSaihakkouDate.Attributes("onkeydown") = disabledOnkeydown
        '���������s��
        TextKhSeikyuusyoHakkouDate.Attributes("onblur") = checkDate
        TextKhSeikyuusyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ �h���b�v�_�E�����X�g
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�����L��
        SelectKhSeikyuuUmu.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenKhSeikyuuUmuMae.ClientID & "','" & SelectKhSeikyuuUmu.ClientID & "')"
        '���ǍH��/���i�R�[�h
        SelectKjSyouhinCd.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenKjSyouhinCdMae.ClientID & "','" & SelectKjSyouhinCd.ClientID & "')"
        '�ǉ��H��/���i�R�[�h
        SelectTjSyouhinCd.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenTjSyouhinCdMae.ClientID & "','" & SelectTjSyouhinCd.ClientID & "')"
        '���ǍH��/���ǍH�����
        SelectKjKairyouKoujiSyubetu.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenKjKairyouKoujiSyubetuMae.ClientID & "','" & SelectKjKairyouKoujiSyubetu.ClientID & "')"
        '�ǉ��H��/���ǍH�����
        SelectTjKairyouKoujiSyubetu.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenTjKairyouKoujiSyubetuMae.ClientID & "','" & SelectTjKairyouKoujiSyubetu.ClientID & "')"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ���l�n
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�H���S����
        TextKoujiTantousyaCd.Attributes("onblur") += checkNumber
        TextKoujiTantousyaCd.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ �@�\�ʃe�[�u���̕\���ؑ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '���ǍH�����
        Me.AncKairyouKouji.HRef = "JavaScript:changeDisplay('" & Me.TBodyKairyouKoujiInfo.ClientID & "');SetDisplayStyle('" & Me.HiddenKairyouKoujiInfoStyle.ClientID & "','" & Me.TBodyKairyouKoujiInfo.ClientID & "');"
        '�H���񍐏����
        Me.AncKoujiHoukokusyo.HRef = "JavaScript:changeDisplay('" & Me.TBodyKoujiHoukokusyoInfo.ClientID & "');SetDisplayStyle('" & Me.HiddenKoujiHoukokusyoInfoStyle.ClientID & "','" & Me.TBodyKoujiHoukokusyoInfo.ClientID & "');"


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
        noTarget.Add(divKairyouKouji, True) '���ǍH�����
        noTarget.Add(divKairyouKoujiHoukokusyo, True) '���ǍH���񍐏����
        noTarget.Add(ButtonTouroku1.ID, True) '�o�^�{�^��1
        noTarget.Add(ButtonTouroku2.ID, True) '�o�^�{�^��2

        If blnFlg Then
            '�S�ẴR���g���[���𖳌���()
            jBn.ChangeDesabledAll(divKairyouKouji, True, noTarget)
            jBn.ChangeDesabledAll(divKairyouKoujiHoukokusyo, True, noTarget)
        Else
            '�S�ẴR���g���[����L����()
            jBn.ChangeDesabledAll(divKairyouKouji, False, noTarget)
            jBn.ChangeDesabledAll(divKairyouKoujiHoukokusyo, False, noTarget)
        End If

    End Sub

#Region "��ʐ���"

    ''' <summary>
    ''' �R���g���[���̏����N�����̉�ʐ���/���ǍH��
    ''' </summary>
    ''' <remarks>�R���g���[���̏����N�����̉�ʐ�����d�l���̗D�揇�ɋL�ڂ���</remarks>
    Public Sub SetEnableControlInitKj()

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable
        jSM.Hash2Ctrl(TdKjKoujiSiyouKakunin, EarthConst.MODE_VIEW, ht) '�H���d�l�m�F
        jSM.Hash2Ctrl(TdKjKakuninDate, EarthConst.MODE_VIEW, ht) '�m�F��
        jSM.Hash2Ctrl(TdKjKoujiKaisyaCd, EarthConst.MODE_VIEW, ht) '�H�����
        jSM.Hash2Ctrl(TdKjKoujiKaisyaSeikyuu, EarthConst.MODE_VIEW, ht) '�H����А���

        ButtonKjKoujiKaisyaSearch.Style("display") = "none"

        '���㏈����(�@�ʐ����e�[�u��.����v��FLG��1)
        If SpanKjUriageSyoriZumi.InnerHtml = EarthConst.URIAGE_ZUMI Then '���㏈���ς̏ꍇ
        Else
            cl.chgDispSyouhinPull(SelectKjKoujiSiyouKakunin, SpanKjKoujiSiyouKakunin) '�H���d�l�m�F
            cl.chgDispSyouhinText(TextKjKoujiKaisyaCd) '�H�����
            ButtonKjKoujiKaisyaSearch.Style("display") = "inline"
            cl.chgDispCheckBox(CheckBoxKjKoujiKaisyaSeikyuu, SpanKjKoujiKaisyaSeikyuu) '�H����А���

            If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '�H���d�l�m�F���L�̏ꍇ
                cl.chgDispSyouhinText(TextKjKakuninDate) '�m�F��

            End If

        End If

    End Sub

    ''' <summary>
    ''' �R���g���[���̉�ʐ���/���ǍH��
    ''' </summary>
    ''' <remarks>�R���g���[���̉�ʐ�����d�l���̗D�揇�ɋL�ڂ���</remarks>
    Public Sub SetEnableControlKj()

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable
        jSM.Hash2Ctrl(TdKjKakuninDate, EarthConst.MODE_VIEW, ht) '�m�F��
        jSM.Hash2Ctrl(TdKjSyouhinCd, EarthConst.MODE_VIEW, ht) '���i�R�[�h
        jSM.Hash2Ctrl(TdKjSeikyuuUmu, EarthConst.MODE_VIEW, ht) '�����L��
        jSM.Hash2Ctrl(TdKjUriageKingaku, EarthConst.MODE_VIEW, ht) '������z/�Ŕ����z
        jSM.Hash2Ctrl(TdKjSiireKingaku, EarthConst.MODE_VIEW, ht) '�d�����z/�Ŕ����z
        jSM.Hash2Ctrl(TdKjSeikyuusyoHakkouDate, EarthConst.MODE_VIEW, ht) '���������s��

        '���D�揇1
        '�������m��(�@�ʐ����e�[�u��.�������m��FLG��1)�����Ȃ�

        '���D�揇2
        '���㏈����(�@�ʐ����e�[�u��.����v��FLG��1)
        If SpanKjUriageSyoriZumi.InnerHtml = EarthConst.URIAGE_ZUMI Then
        Else
            '���D�揇3
            '���i�R�[�h����
            If SelectKjSyouhinCd.SelectedValue = "" Then
                cl.chgDispSyouhinPull(SelectKjSyouhinCd, SpanKjSyouhinCd) '���i�R�[�h

            Else
                cl.chgDispSyouhinPull(SelectKjSyouhinCd, SpanKjSyouhinCd) '���i�R�[�h

                '���D�揇4

                If SelectKjSeikyuuUmu.SelectedValue = "0" Or SelectKjSeikyuuUmu.SelectedValue = "" Then '�����L�� ��or��
                    cl.chgDispSyouhinPull(SelectKjSeikyuuUmu, SpanKjSeikyuuUmu) '�����L��
                    cl.chgDispSyouhinText(TextKjSiireZeinukiKingaku) '�d�����z/�Ŕ����z

                    '���D�揇5
                ElseIf SelectKjSeikyuuUmu.SelectedValue = "1" Then '�L
                    cl.chgDispSyouhinPull(SelectKjSeikyuuUmu, SpanKjSeikyuuUmu) '�����L��
                    cl.chgDispSyouhinText(TextKjUriageZeinukiKingaku) '������z/�Ŕ����z
                    cl.chgDispSyouhinText(TextKjSiireZeinukiKingaku) '�d�����z/�Ŕ����z
                    cl.chgDispSyouhinText(TextKjSeikyuusyoHakkouDate) '���������s��

                End If
            End If

        End If
        '���D�揇6(���.�������m�聁�m��)�����Ȃ�
        '���D�揇7(�D�揇6�ȊO)�����Ȃ�
        '�������m�聂�m��

        '**********************
        '* �H���d�l�m�F�ύX��
        '**********************
        '�H���d�l�m�F
        If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '�L
            cl.chgDispSyouhinText(TextKjKakuninDate) '�m�F��

        End If

    End Sub

    ''' <summary>
    ''' �R���g���[���̏����N�����̉�ʐ���/�ǉ��H��
    ''' </summary>
    ''' <remarks>�R���g���[���̏����N�����̉�ʐ�����d�l���̗D�揇�ɋL�ڂ���</remarks>
    Public Sub SetEnableControlInitTj()
        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban
        '�L�����A�������̑ΏۊO�ɂ���R���g���[���S
        Dim noTarget As New Hashtable
        noTarget.Add(divKairyouKouji, True) '���ǍH�����

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable

        ButtonTjKoujiKaisyaSearch.Style("display") = "inline"

        '���㏈����(�@�ʐ����e�[�u��.����v��FLG��1)
        If SpanKjUriageSyoriZumi.InnerHtml <> EarthConst.URIAGE_ZUMI Then '���ǍH��/�����㏈��

            '���i�R�[�h��"B2008"�A������N����������
            If SelectKjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO And TextKjUriageNengappi.Text <> "" Then
                If SpanTjUriageSyorizumi.InnerHtml = EarthConst.URIAGE_ZUMI Then
                    jSM.Hash2Ctrl(TdTjKoujiKaisyaCd, EarthConst.MODE_VIEW, ht) '�H�����
                    ButtonTjKoujiKaisyaSearch.Style("display") = "none"
                End If
            Else
                jSM.Hash2Ctrl(TdTjKoujiKaisyaCd, EarthConst.MODE_VIEW, ht) '�H�����
                ButtonTjKoujiKaisyaSearch.Style("display") = "none"
                jSM.Hash2Ctrl(TdTjKairyouKoujiSyubetu, EarthConst.MODE_VIEW, ht) '���ǍH�����
                jSM.Hash2Ctrl(TdTjKanryouYoteiDate, EarthConst.MODE_VIEW, ht) '�����\���
                jSM.Hash2Ctrl(TdTjKoujiDate, EarthConst.MODE_VIEW, ht) '�H����
                jSM.Hash2Ctrl(TdTjKankouSokuhouTyakuDate, EarthConst.MODE_VIEW, ht) '���H���񒅓�
            End If

        Else
            If SpanTjUriageSyorizumi.InnerHtml = EarthConst.URIAGE_ZUMI Then
                jSM.Hash2Ctrl(TdTjKoujiKaisyaCd, EarthConst.MODE_VIEW, ht) '�H�����
                ButtonTjKoujiKaisyaSearch.Style("display") = "none"
                jSM.Hash2Ctrl(TdTjKoujiKaisyaSeikyuu, EarthConst.MODE_VIEW, ht) '�H����А���
            End If

        End If
        '���i�R�[�h��"B2008"�A������N����������
        If (SelectKjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO And TextKjUriageNengappi.Text <> "") = False Then
            jSM.Hash2Ctrl(TdTjKoujiKaisyaSeikyuu, EarthConst.MODE_VIEW, ht) '�H����А���
        End If


    End Sub

    ''' <summary>
    ''' �R���g���[���̉�ʐ���/�ǉ��H��
    ''' </summary>
    ''' <remarks>�R���g���[���̉�ʐ�����d�l���̗D�揇�ɋL�ڂ���</remarks>
    Public Sub SetEnableControlTj()

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable
        jSM.Hash2Ctrl(TdTjSyouhinCd, EarthConst.MODE_VIEW, ht) '���i�R�[�h
        jSM.Hash2Ctrl(TdTjSeikyuuUmu, EarthConst.MODE_VIEW, ht) '�����L��
        jSM.Hash2Ctrl(TdTjUriageKingaku, EarthConst.MODE_VIEW, ht) '������z/�Ŕ����z
        jSM.Hash2Ctrl(TdTjSiireKingaku, EarthConst.MODE_VIEW, ht) '�d�����z/�Ŕ����z
        jSM.Hash2Ctrl(TdTjSeikyuusyoHakkouDate, EarthConst.MODE_VIEW, ht) '���������s��

        '���D�揇1
        '�������m��(�@�ʐ����e�[�u��.�������m��FLG��1)�����Ȃ�

        '���D�揇2
        '���㏈����(�@�ʐ����e�[�u��.����v��FLG��1)�����Ȃ�
        If SpanTjUriageSyorizumi.InnerHtml = EarthConst.URIAGE_ZUMI Then
        Else

            '���D�揇3
            '���ǍH��.���㏈���ρA���邢�͖����㏈���ł���JIO�����i�Ŕ���N���������͂ł͂Ȃ��ꍇ
            If SpanKjUriageSyoriZumi.InnerHtml = EarthConst.URIAGE_ZUMI Or _
                (SpanKjUriageSyoriZumi.InnerHtml <> EarthConst.URIAGE_ZUMI And _
                (HiddenKjSyouhinCdOld.Value = EarthConst.SH_CD_JIO And HiddenKjUriageNengappiOld.Value <> "")) Then

                '���D�揇4
                '���i�R�[�h����
                If SelectTjSyouhinCd.SelectedValue = "" Then
                    cl.chgDispSyouhinPull(SelectTjSyouhinCd, SpanTjSyouhinCd) '���i�R�[�h

                Else

                    cl.chgDispSyouhinPull(SelectTjSyouhinCd, SpanTjSyouhinCd) '���i�R�[�h

                    '���D�揇4

                    If SelectTjSeikyuuUmu.SelectedValue = "0" Or SelectTjSeikyuuUmu.SelectedValue = "" Then '�����L�� ��or��
                        cl.chgDispSyouhinPull(SelectTjSeikyuuUmu, SpanTjSeikyuuUmu) '�����L��
                        cl.chgDispSyouhinText(TextTjSiireZeinukiKingaku) '�d�����z/�Ŕ����z

                        '���D�揇5
                    ElseIf SelectTjSeikyuuUmu.SelectedValue = "1" Then '�L
                        cl.chgDispSyouhinPull(SelectTjSeikyuuUmu, SpanTjSeikyuuUmu) '�����L��
                        cl.chgDispSyouhinText(TextTjUriageZeinukiKingaku) '������z/�Ŕ����z
                        cl.chgDispSyouhinText(TextTjSiireZeinukiKingaku) '�d�����z/�Ŕ����z
                        cl.chgDispSyouhinText(TextTjSeikyuusyoHakkouDate) '���������s��

                    End If
                End If

                '���D�揇6(���.�������m�聁�m��)�����Ȃ�
                '���D�揇7(�D�揇6�ȊO)�����Ȃ�
                '�������m�聂�m��

            End If

        End If

    End Sub

    ''' <summary>
    ''' �R���g���[���̉�ʐ���/�H���񍐏��Ĕ��s
    ''' </summary>
    ''' <remarks>�R���g���[���̉�ʐ�����d�l���̗D�揇�ɋL�ڂ���</remarks>
    Public Sub SetEnableControlKh()

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable
        htNotTarget.Add(TextKhSeikyuusaki.ID, TextKhSeikyuusaki) '������
        htNotTarget.Add(TextKhUriageNengappi.ID, TextKhUriageNengappi) '����N����
        htNotTarget.Add(TextKhHattyuusyoKakutei.ID, TextKhHattyuusyoKakutei) '�������m��
        htNotTarget.Add(TextKhHattyuusyoKingaku.ID, TextKhHattyuusyoKingaku) '���������z
        htNotTarget.Add(TextKhHattyuusyoKakuninDate.ID, TextKhHattyuusyoKakuninDate) '�������m�F��
        jSM.Hash2Ctrl(TdKhHassouDate, EarthConst.MODE_VIEW, ht) '������
        jSM.Hash2Ctrl(TdKhSaihakkouDate, EarthConst.MODE_VIEW, ht) '�Ĕ��s��
        jSM.Hash2Ctrl(TrKhSyouhin, EarthConst.MODE_VIEW, ht, htNotTarget) '���i�e�[�u��
        jSM.Hash2Ctrl(TdKhSaihakkouRiyuu, EarthConst.MODE_VIEW, ht) '�Ĕ��s���R

        '���D�揇1
        '�������m��(�@�ʐ����e�[�u��.�������m��FLG��1)�����Ȃ�

        '���D�揇2
        '���㏈����(�@�ʐ����e�[�u��.����v��FLG��1)
        If SpanKhUriageSyorizumi.InnerHtml = EarthConst.URIAGE_ZUMI Then '�ύX�ӏ��Ȃ��̂��߁A��ʂ���擾
            cl.chgDispSyouhinText(TextKhSaihakkouRiyuu) '�Ĕ��s���R

            '���D�揇3
        ElseIf TextKhHassouDate.Text.Length = 0 Then '�����񍐏���������������
            cl.chgDispSyouhinText(TextKhHassouDate) '������
        Else
            cl.chgDispSyouhinText(TextKhHassouDate) '������

            '���D�揇4
            If TextKhSaihakkouDate.Text.Length = 0 Then '�Ĕ��s����������
                cl.chgDispSyouhinText(TextKhSaihakkouDate) '�Ĕ��s��

                '���D�揇5,6
            ElseIf SelectKhSeikyuuUmu.SelectedValue = "0" Or SelectKhSeikyuuUmu.SelectedValue = "" Then '���� ��or��
                cl.chgDispSyouhinText(TextKhSaihakkouDate) '�Ĕ��s��
                cl.chgDispSyouhinPull(SelectKhSeikyuuUmu, SpanKhSeikyuuUmu) '�����L��
                cl.chgDispSyouhinText(TextKhSaihakkouRiyuu) '�Ĕ��s���R

                '���D�揇7
            Else
                cl.chgDispSyouhinText(TextKhSaihakkouDate) '�Ĕ��s��
                cl.chgDispSyouhinPull(SelectKhSeikyuuUmu, SpanKhSeikyuuUmu) '�����L��
                cl.chgDispSyouhinText(TextKhSaihakkouRiyuu) '�Ĕ��s���R

                Dim kameitenlogic As New KameitenSearchLogic
                Dim blnTorikesi As Boolean = False
                Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                '(*6)�����X������̏ꍇ�A������3�n��ȊO�̈����Ƃ���
                If record.Torikesi <> 0 Then
                    record.KeiretuCd = ""
                End If

                If TextKhSeikyuusaki.Text = EarthConst.SEIKYU_TASETU And cl.getKeiretuFlg(record.KeiretuCd) = False Then

                    cl.chgDispSyouhinText(TextKhJituseikyuuKingaku) '�������Ŕ����z
                    cl.chgDispSyouhinText(TextKhSeikyuusyoHakkouDate) '���������s��

                    '���D�揇8
                Else
                    cl.chgDispSyouhinText(TextKhJituseikyuuKingaku) '�������Ŕ����z
                    cl.chgDispSyouhinText(TextKhSeikyuusyoHakkouDate) '���������s��
                    cl.chgDispSyouhinText(TextKhKoumutenSeikyuuKingaku) '�H���X�����Ŕ����z
                End If

            End If

        End If


        '���D�揇9(���.�������m�聁�m��)�����Ȃ�

        '���D�揇10(�D�揇9�ȊO)�����Ȃ�
        ''�������m�聂�m��

        '�󗝕ύX
        If SelectKhJuri.SelectedValue = "2" Or SelectKhJuri.SelectedValue = "3" Then
            jSM.Hash2Ctrl(TdKhHassouDate, EarthConst.MODE_VIEW, ht) '������
            jSM.Hash2Ctrl(TdKhSaihakkouDate, EarthConst.MODE_VIEW, ht) '�Ĕ��s��
        End If

    End Sub

#End Region

#Region "�H����Њ֘A"

    ''' <summary>
    ''' ���ǍH��/�H����ЕύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKjKoujiKaisyaCd_ChangeSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^���擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        Dim tmpScript As String = ""

        '�R�[�h�̓���
        If TextKjKoujiKaisyaCd.Text <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetKoujikaishaSearchResult(TextKjKoujiKaisyaCd.Text, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi, _
                                                                            ucGyoumuKyoutuu.AccKameitenCd.Value)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            TextKjKoujiKaisyaCd.Text = recData.TysKaisyaCd & recData.JigyousyoCd
            TextKjKoujiKaisyaMei.Text = recData.TysKaisyaMei
            If Me.TextKjKoujiKaisyaMei.Text = String.Empty Then '���ݒ莞�A���p�X�y�[�X��\��
                Me.TextKjKoujiKaisyaMei.Text = " "
            End If
            Me.HiddenKjKoujiKaisyaCdMae.Value = Me.TextKjKoujiKaisyaCd.Text

            ' �H�����NG�ݒ�
            If recData.KahiKbn = 9 Then
                TextKjKoujiKaisyaCd.Style("color") = "red"
                TextKjKoujiKaisyaMei.Style("color") = "red"

                tmpScript = "alert('" & Messages.MSG103W & "');" & vbCrLf
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextKjKoujiKaisyaCd_ChangeSub", tmpScript, True)

            Else
                TextKjKoujiKaisyaCd.Style("color") = "blue"
                TextKjKoujiKaisyaMei.Style("color") = "blue"
            End If

            '�H�����i�Z�b�g����
            If SetSyouhinInfoFromKojM(EnumKoujiType.KairyouKouji, EarthEnum.emKojKkkActionType.KojKaisyaCd) = True Then
                '���z�̍Čv�Z
                SetKingakuUriage(EnumKoujiType.KairyouKouji)
            End If

            '�Z�b�g�t�H�[�J�X
            setFocusAJ(ButtonKjKoujiKaisyaSearch)

        Else

            TextKjKoujiKaisyaCd.Style("color") = "black"
            TextKjKoujiKaisyaMei.Style("color") = "black"

            '�H����ЃR�[�h�����m��Ȃ̂ŃN���A
            TextKjKoujiKaisyaMei.Text = ""

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            Dim tmpFocusScript = "objEBI('" & ButtonKjKoujiKaisyaSearch.ClientID & "').focus();"
            tmpScript = "callSearch('" & TextKjKoujiKaisyaCd.ClientID & EarthConst.SEP_STRING & _
                                             ucGyoumuKyoutuu.AccKameitenCd.ClientID & "','" & _
                                             UrlConst.SEARCH_KOUJIKAISYA & "','" & _
                                             TextKjKoujiKaisyaCd.ClientID & EarthConst.SEP_STRING & _
                                             TextKjKoujiKaisyaMei.ClientID & _
                                             "','" & ButtonKjKoujiKaisyaSearch.ClientID & "');"
            If callWindow Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & tmpScript, True)
            ElseIf HiddenKoujiKaisyaSearchTypeKj.Value = "1" Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & "if(JSkoujiKaisyaSearchTypeKj==9)" & tmpScript, True)
            End If

        End If

        tmpScript = "JSkoujiKaisyaSearchTypeKj=0;"
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "resetSearchType", tmpScript, True)

    End Sub

    ''' <summary>
    ''' ���ǍH��/�H����Ќ����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKjKoujiKaisyaSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        '1.��ʂ̐ݒ�
        If HiddenKoujiKaisyaSearchTypeKj.Value <> "1" Then
            TextKjKoujiKaisyaCd_ChangeSub(sender, e)
        Else
            '�R�[�honchange�ŌĂ΂ꂽ�ꍇ
            TextKjKoujiKaisyaCd_ChangeSub(sender, e, False)
            HiddenKoujiKaisyaSearchTypeKj.Value = String.Empty
        End If

        '�����L���ɂ�鐿�������s�������ݒ�
        If Me.SelectKjSeikyuuUmu.SelectedValue = "0" Or Me.SelectKjSeikyuuUmu.SelectedValue = "" Then
            Me.TextKjSeikyuusyoHakkouDate.Text = ""
        End If

        '�H�����NG�ݒ�
        ucGyoumuKyoutuu.SetKoujiKaisyaNG(TextKjKoujiKaisyaCd.Text, TextKjKoujiKaisyaCd.Text)

        '�H����А����ύX������(����)
        Me.ChgKjKaisyaSeikyuuUmu()

        '��ʐ���
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' �ǉ��H��/�H����ЕύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextTjKoujiKaisyaCd_ChangeSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^���擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        Dim tmpScript As String = ""

        '�R�[�h�̓���
        If TextTjKoujiKaisyaCd.Text <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetKoujikaishaSearchResult(TextTjKoujiKaisyaCd.Text, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi, _
                                                                            ucGyoumuKyoutuu.AccKameitenCd.Value)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            TextTjKoujiKaisyaCd.Text = recData.TysKaisyaCd & recData.JigyousyoCd
            TextTjKoujiKaisyaMei.Text = recData.TysKaisyaMei
            If Me.TextTjKoujiKaisyaMei.Text = String.Empty Then '���ݒ莞�A���p�X�y�[�X��\��
                Me.TextTjKoujiKaisyaMei.Text = " "
            End If
            Me.HiddenTjKoujiKaisyaCdMae.Value = Me.TextTjKoujiKaisyaCd.Text

            ' �H�����NG�ݒ�
            If recData.KahiKbn = 9 Then
                TextTjKoujiKaisyaCd.Style("color") = "red"
                TextTjKoujiKaisyaMei.Style("color") = "red"

                tmpScript = "alert('" & Messages.MSG103W & "');" & vbCrLf
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextTjKoujiKaisyaCd_ChangeSub", tmpScript, True)
            Else
                TextTjKoujiKaisyaCd.Style("color") = "blue"
                TextTjKoujiKaisyaMei.Style("color") = "blue"
            End If

            '�H�����i�Z�b�g����
            If SetSyouhinInfoFromKojM(EnumKoujiType.TuikaKouji, EarthEnum.emKojKkkActionType.KojKaisyaCd) = True Then
                '���z�̍Čv�Z(�H�����i����擾�o�����ꍇ�̂ݍČv�Z)
                SetKingakuUriage(EnumKoujiType.TuikaKouji)
            End If

            '�Z�b�g�t�H�[�J�X
            setFocusAJ(ButtonTjKoujiKaisyaSearch)
        Else

            TextTjKoujiKaisyaCd.Style("color") = "black"
            TextTjKoujiKaisyaMei.Style("color") = "black"

            '�ǉ��H����ЃR�[�h�����m��Ȃ̂ŃN���A
            TextTjKoujiKaisyaMei.Text = ""

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            Dim tmpFocusScript = "objEBI('" & ButtonTjKoujiKaisyaSearch.ClientID & "').focus();"
            tmpScript = "callSearch('" & TextTjKoujiKaisyaCd.ClientID & EarthConst.SEP_STRING & _
                                             ucGyoumuKyoutuu.AccKameitenCd.ClientID & "','" & _
                                             UrlConst.SEARCH_KOUJIKAISYA & "','" & _
                                             TextTjKoujiKaisyaCd.ClientID & EarthConst.SEP_STRING & _
                                             TextTjKoujiKaisyaMei.ClientID & _
                                             "','" & ButtonTjKoujiKaisyaSearch.ClientID & "');"
            If callWindow Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & tmpScript, True)
            ElseIf HiddenKoujiKaisyaSearchTypeTj.Value = "1" Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & "if(JSkoujiKaisyaSearchTypeTj==9)" & tmpScript, True)
            End If

        End If

        tmpScript = "JSkoujiKaisyaSearchTypeTj=0;"
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "resetSearchType", tmpScript, True)

    End Sub

    ''' <summary>
    ''' �ǉ��H��/�H����Ќ����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTjKoujiKaisyaSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If HiddenKoujiKaisyaSearchTypeTj.Value <> "1" Then
            TextTjKoujiKaisyaCd_ChangeSub(sender, e)
        Else
            '�R�[�honchange�ŌĂ΂ꂽ�ꍇ
            TextTjKoujiKaisyaCd_ChangeSub(sender, e, False)
            HiddenKoujiKaisyaSearchTypeTj.Value = String.Empty
        End If

        '�����L���ɂ�鐿�������s�������ݒ�
        If Me.SelectTjSeikyuuUmu.SelectedValue = "0" Or Me.SelectTjSeikyuuUmu.SelectedValue = "" Then
            Me.TextTjSeikyuusyoHakkouDate.Text = ""
        End If

        '�H�����NG�ݒ�
        ucGyoumuKyoutuu.SetKoujiKaisyaNG(TextTjKoujiKaisyaCd.Text, TextTjKoujiKaisyaCd.Text)

        '<���ʏ��>�敪=E���邢�́A<���ǍH��>�H����А���=�`�F�b�N�̏ꍇ
        If ucGyoumuKyoutuu.Kubun = "E" Or Me.CheckBoxTjKoujiKaisyaSeikyuu.Checked Then
            '�H����А����L���ύX������[����]
            Me.ChgTjKoujiKaisyaSeikyuuUmu()

            '��ʐ���
            SetEnableControlTj()
        End If

    End Sub

#End Region

#Region "�����ݒ�/���������s��"

    ''' <summary>
    ''' [���ǍH��]���������s���̐ݒ�
    ''' </summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <remarks></remarks>
    Private Sub SetKjAutoSeikyuusyoHakkouDate(ByVal strSyouhinCd As String)
        Dim strDtTmp As String

        '�������ߓ��̃Z�b�g
        Me.ucSeikyuuSiireLinkKai.SetSeikyuuSimeDate(strSyouhinCd, CheckBoxKjKoujiKaisyaSeikyuu.Checked, TextKjKoujiKaisyaCd.Text)

        '���������s���̎����ݒ�
        strDtTmp = Me.ucSeikyuuSiireLinkKai.GetSeikyuusyoHakkouDate()

        TextKjSeikyuusyoHakkouDate.Text = strDtTmp

    End Sub

    ''' <summary>
    ''' [�ǉ��H��]���������s���̐ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTjAutoSeikyuusyoHakkouDate(ByVal strSyouhinCd As String)
        Dim strDtTmp As String

        '�������ߓ��̃Z�b�g
        Me.ucSeikyuuSiireLinkTui.SetSeikyuuSimeDate(strSyouhinCd, CheckBoxTjKoujiKaisyaSeikyuu.Checked, TextTjKoujiKaisyaCd.Text)

        '���������s���̎����ݒ�
        strDtTmp = Me.ucSeikyuuSiireLinkTui.GetSeikyuusyoHakkouDate()

        TextTjSeikyuusyoHakkouDate.Text = strDtTmp

    End Sub

    ''' <summary>
    ''' ���������s�����擾
    ''' </summary>
    ''' <param name="ctrlLink">������/�d���惊���N���[�U�[�R���g���[��</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <returns>���������s��</returns>
    ''' <remarks></remarks>
    Private Function GetSeikyuusyoHakkouDate(ByVal ctrlLink As SeikyuuSiireLinkCtrl, ByVal strSyouhinCd As String) As String
        Dim strDtTmp As String

        '���������̃Z�b�g
        ctrlLink.SetSeikyuuSimeDate(strSyouhinCd)

        '���������s���̎����ݒ�
        strDtTmp = ctrlLink.GetSeikyuusyoHakkouDate()

        Return strDtTmp

    End Function

#End Region

#Region "���ǍH���񍐏��ύX������"

    ''' <summary>
    ''' (25) [���ǍH���񍐏����]�󗝕ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKhJuri_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strJuri As String = SelectKhJuri.SelectedValue '��
        Dim strJuriDate As String = TextKhJuriDate.Text '�󗝓�
        Dim strHassouDate As String = TextKhHassouDate.Text '������
        Dim strHizukeHassouDate As String = "" '���t�}�X�^.�񍐏�������

        setFocusAJ(SelectKhJuri)

        If strJuri = "1" Then '�󗝁�1�i�L��j�̏ꍇ

            If strJuriDate = String.Empty Then '�󗝓��������͂̏ꍇ
                ' �V�X�e�����t���Z�b�g
                TextKhJuriDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)

                If strHassouDate = String.Empty Then '�������������͂̏ꍇ

                    Dim houkokusyoLogic As New HoukokusyoLogic
                    Dim strRet As String = String.Empty

                    '�񍐏��������̎����ݒ�
                    '���󗝓�����L�̓��t�}�X�^.�񍐏��������̏ꍇ�́A���t�}�X�^.�񍐏��������{1������ҏW����
                    strRet = houkokusyoLogic.GetHoukokusyoHassoudate(ucGyoumuKyoutuu.Kubun _
                                                            , TextKhJuriDate.Text)
                    If strRet <> String.Empty Then
                        TextKhHassouDate.Text = strRet
                    End If

                End If

            End If

        ElseIf strJuri = "2" Or strJuri = "3" Then '�����񍐏��󗝁�2,3�i�ۗ��A���́A���t�s�v�j�̏ꍇ

            If strJuriDate = String.Empty Then '�󗝓��������͂̏ꍇ
                ' �V�X�e�����t���Z�b�g
                TextKhJuriDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
            End If

        ElseIf strJuri = "4" Then '�����񍐏��󗝁�4�i�Ĕ����j�̏ꍇ
            If strJuriDate = String.Empty Then '�󗝓��������͂̏ꍇ
                ' �V�X�e�����t���Z�b�g
                TextKhJuriDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
            End If

        End If

        '��ʐ���
        SetEnableControlKh()
    End Sub

    ''' <summary>
    ''' (27) [���ǍH���񍐏����]�������ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKhHassouDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""

        '�Z�b�g�t�H�[�J�X
        setFocusAJ(TextKhHassouDate)

        '������
        If TextKhHassouDate.Text.Length <> 0 Then '���͂���
            '�����ݒ�͂Ȃ��A��ʐ���̂݁������ɏ����Ȃ�

            '�Z�b�g�t�H�[�J�X
            setFocusAJ(TextKhSaihakkouDate)

        ElseIf TextKhHassouDate.Text.Length = 0 Then '���͂Ȃ�

            '(*4)���������z��0,����,��Null�̏ꍇ�A�G���[���b�Z�[�W�i037�j��\�����A�����ݒ�i�N���A�j���s�킸�ɁA�����͂ɕύX��������(������/�Ĕ��s��/�����L��)�͌��̒l�ɖ߂��B
            If TextKhHattyuusyoKingaku.Text <> "0" And TextKhHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                TextKhHassouDate.Text = HiddenKhHassouDateMae.Value '�ύX�O�̒l�ɖ߂� ���� Javascript�ł̐ݒ�ł�Change�C�x���g���������Ȃ�
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextKhHassouDate_TextChanged", tmpScript, True)
                Exit Sub
            End If

            '�N���A
            TextKhSaihakkouDate.Text = "" '�Ĕ��s��
            TextKhSaihakkouRiyuu.Text = "" '�Ĕ��s���R

            Me.ClearControlKh()

        End If

        '��ʐ���
        SetEnableControlKh()
    End Sub

    ''' <summary>
    ''' (28) [���ǍH���񍐏����]�Ĕ��s���ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKhSaihakkouDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""
        Dim strSyouhinCd As String = TextKhSyouhinCd.Text

        '�Z�b�g�t�H�[�J�X
        setFocusAJ(TextKhSaihakkouDate)

        '�Ĕ��s��
        If TextKhSaihakkouDate.Text.Length <> 0 Then '���͂���

            '�Z�b�g�t�H�[�J�X
            setFocusAJ(SelectKhSeikyuuUmu)

            '�����L��
            If SelectKhSeikyuuUmu.SelectedValue = "1" Then '�L
                '���i�R�[�h
                If TextKhSyouhinCd.Text.Length <> 0 Then '�ݒ��
                    '���������s��
                    If TextKhSeikyuusyoHakkouDate.Text.Length = 0 Then '������
                        '���������s���̎����ݒ�
                        Dim strDtTmp As String = GetSeikyuusyoHakkouDate(Me.ucSeikyuuSiireLink, strSyouhinCd)
                        TextKhSeikyuusyoHakkouDate.Text = strDtTmp
                    End If

                    '����N����
                    If TextKhUriageNengappi.Text.Length = 0 Then '������
                        '����N�����̎����ݒ�
                        TextKhUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If

                End If

            ElseIf SelectKhSeikyuuUmu.SelectedValue = "0" Then '��
                '���i�R�[�h
                If TextKhSyouhinCd.Text.Length <> 0 Then '�ݒ��
                    '����N����
                    If TextKhUriageNengappi.Text.Length = 0 Then '������
                        '����N�����̎����ݒ�
                        TextKhUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If
                End If
            End If

        Else '���͂Ȃ�

            '(*4)���������z��0,����,��Null�̏ꍇ�A�G���[���b�Z�[�W�i037�j��\�����A�����ݒ�i�N���A�j���s�킸�ɁA�����͂ɕύX��������(������/�Ĕ��s��/�����L��)�͌��̒l�ɖ߂��B
            If TextKhHattyuusyoKingaku.Text <> "0" And TextKhHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                TextKhSaihakkouDate.Text = HiddenKhSaihakkouDateMae.Value '�ύX�O�̒l�ɖ߂� ���� Javascript�ł̐ݒ�ł�Change�C�x���g���������Ȃ�
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextKhSaihakkouDate_TextChanged", tmpScript, True)
                Exit Sub
            End If

            '���N���A
            TextKhSaihakkouRiyuu.Text = "" '�Ĕ��s���R

            Me.ClearControlKh()

        End If

        '��ʐ���
        SetEnableControlKh()

    End Sub

    ''' <summary>
    ''' (29) [���ǍH���񍐏����]�����L���ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKhSeikyuuUmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""
        Dim blnTorikesi As Boolean = False
        '���ڐ����A�������̏����擾
        Dim syouhinRec As Syouhin23Record

        '�Z�b�g�t�H�[�J�X
        setFocusAJ(SelectKhSeikyuuUmu)

        '�d���z�͏��0�~
        Me.HiddenKhSiireGaku.Value = "0"
        Me.HiddenKhSiireSyouhiZei.Value = "0"

        '�����L��
        If SelectKhSeikyuuUmu.SelectedValue = "" Then '��

            '(*4)���������z��0,����,��Null�̏ꍇ�A�G���[���b�Z�[�W�i037�j��\�����A�����ݒ�i�N���A�j���s�킸�ɁA�����͂ɕύX��������(������/�Ĕ��s��/�����L��)�͌��̒l�ɖ߂��B
            If TextKhHattyuusyoKingaku.Text <> "0" And TextKhHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                SelectKhSeikyuuUmu.SelectedValue = HiddenKhSeikyuuUmuMae.Value '�ύX�O�̒l�ɖ߂� ���� Javascript�ł̐ݒ�ł�Change�C�x���g���������Ȃ�
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectKhSeikyuuUmu_SelectedIndexChanged", tmpScript, True)
                Exit Sub
            End If

            '���N���A
            Me.ClearControlKh()

        ElseIf SelectKhSeikyuuUmu.SelectedValue = "0" Then '��
            '���z��0�N���A
            Clear0SyouhinTableKh()

            '���i�R�[�h/���i���̎����ݒ聣
            Me.SetSyouhinInfoKh()

            '���z�̍Čv�Z
            SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo)

        ElseIf SelectKhSeikyuuUmu.SelectedValue = "1" Then '�L

            '���i�R�[�h/���i���̎����ݒ聣
            Me.SetSyouhinInfoKh()

            '���i�R�[�h
            If TextKhSyouhinCd.Text = String.Empty Then '�ݒ�Ȃ�
                '�����L��
                SelectKhSeikyuuUmu.SelectedValue = "" '��

                SetEnableControlKh() '��ʐ���
                Exit Sub
            Else
                '���������s��
                If TextKhSeikyuusyoHakkouDate.Text.Length = 0 Then
                    '���������s���̎����ݒ�
                    Dim strDttmp As String = GetSeikyuusyoHakkouDate(Me.ucSeikyuuSiireLink, Me.TextKhSyouhinCd.Text)
                    TextKhSeikyuusyoHakkouDate.Text = strDttmp
                End If

                TextKhUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) '����N����

                '�������m��
                If TextKhHattyuusyoKakutei.Text.Length = 0 Then '(*5)�������m�肪�󔒂̏ꍇ�́A�u0�F���m��v��ݒ肷��
                    TextKhHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
                End If
            End If

            '*************************
            '* �ȉ��A�����ݒ菈��
            '*************************
            If TextKhSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '���ڐ���

                '(*6)�����X������̏ꍇ�A������3�n��ȊO�̈����Ƃ���
                Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                If record Is Nothing Then '�f�[�^�擾�ł��Ȃ������ꍇ
                    SelectKhSeikyuuUmu.SelectedValue = "" '�擾NG(�����L���̃N���A)

                    ClearBlnkSyouhinTable() '�󔒃N���A
                End If

                If record.Torikesi <> 0 Then '����t���O�������Ă���ꍇ
                    '**************************************************
                    ' �������i�n��ȊO�j
                    '**************************************************
                    ' �H���X�����z�͂O
                    TextKhKoumutenSeikyuuKingaku.Text = "0"

                    '�������Ŕ����z�̎����ݒ�
                    syouhinRec = JibanLogic.GetSyouhinInfo(TextKhSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo, ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        HiddenKhZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
                        HiddenKhZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪

                        '�������Ŕ����z�����i�}�X�^.�W�����i
                        TextKhJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                    End If

                Else
                    '**************************************************
                    ' ���ڐ���
                    '**************************************************
                    '�H���X(A)
                    '������(A)
                    syouhinRec = JibanLogic.GetSyouhinInfo(TextKhSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo, ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        '�H���X�����Ŕ����z�����i�}�X�^.�W�����i
                        TextKhKoumutenSeikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)
                        '�������Ŕ����z�����.�H���X�����Ŕ����z
                        TextKhJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                        HiddenKhZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
                        HiddenKhZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪
                    End If

                End If

            ElseIf TextKhSeikyuusaki.Text = EarthConst.SEIKYU_TASETU Then '������
                Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                If record Is Nothing Then '�f�[�^�擾�ł��Ȃ������ꍇ
                    SelectKhSeikyuuUmu.SelectedValue = "" '�擾NG(�����L���̃N���A)

                    ClearBlnkSyouhinTable() '�󔒃N���A
                End If

                '(*6)�����X������̏ꍇ�A������3�n��ȊO�̈����Ƃ���
                If record.Torikesi <> 0 Then
                    record.KeiretuCd = ""
                End If

                If cl.getKeiretuFlg(record.KeiretuCd) Then '3�n��
                    '�H���X(A)
                    '������(B)
                    syouhinRec = JibanLogic.GetSyouhinInfo(TextKhSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo, ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        HiddenKhZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
                        HiddenKhZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪

                        '�H���X�����Ŕ����z�����i�}�X�^.�W�����i
                        TextKhKoumutenSeikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                        '�����.�H���X�����Ŕ����z��0 �̏ꍇ�� 0 �Œ�
                        If TextKhKoumutenSeikyuuKingaku.Text = "0" Then
                            TextKhJituseikyuuKingaku.Text = "0" '�������Ŕ����z

                        Else
                            '**************************************************
                            ' �������i3�n��j
                            '**************************************************
                            Dim zeinukiGaku As Integer = 0

                            If JibanLogic.GetSeikyuuGaku(sender, _
                                                          3, _
                                                          record.KeiretuCd, _
                                                          TextKhSyouhinCd.Text, _
                                                          syouhinRec.HyoujunKkk, _
                                                          zeinukiGaku) Then
                                ' ���������z�փZ�b�g
                                TextKhJituseikyuuKingaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                            End If

                        End If

                    End If

                Else '3�n��ȊO

                    '�H���X(B)
                    '������(C)
                    '**************************************************
                    ' �������i�n��ȊO�j
                    '**************************************************
                    ' �H���X�����z�͂O
                    TextKhKoumutenSeikyuuKingaku.Text = "0"

                    '�������Ŕ����z�̎����ݒ�
                    syouhinRec = JibanLogic.GetSyouhinInfo(TextKhSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo, ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        HiddenKhZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
                        HiddenKhZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪

                        '�������Ŕ����z�����i�}�X�^.�W�����i
                        TextKhJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                    End If

                End If

            End If

            '���z�̍Čv�Z
            SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo)

        End If

        '��ʐ���
        SetEnableControlKh()

    End Sub

    ''' <summary>
    ''' (30) [���ǍH���񍐏����]�H���X�����Ŕ����z�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKhKoumutenSeikyuuKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim syouhinRec As New Syouhin23Record

        '�Z�b�g�t�H�[�J�X
        setFocusAJ(TextKhJituseikyuuKingaku)

        '����
        If SelectKhSeikyuuUmu.SelectedValue = "1" Then '�L
            '���i�R�[�h
            If TextKhSyouhinCd.Text.Length <> 0 Then '�ݒ��

                '�ŏ��ݒ�
                SetKhZeiInfo(TextKhSyouhinCd.Text)

                '������
                If TextKhSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '���ڐ���

                    '�������Ŕ����z�����.�H���X�����Ŕ����z
                    TextKhJituseikyuuKingaku.Text = TextKhKoumutenSeikyuuKingaku.Text

                    SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo) '���z�Čv�Z

                ElseIf TextKhSeikyuusaki.Text = EarthConst.SEIKYU_TASETU Then '������

                    Dim kameitenlogic As New KameitenSearchLogic
                    Dim blnTorikesi As Boolean = False
                    Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                    '(*6)�����X������̏ꍇ�A������3�n��ȊO�̈����Ƃ���
                    If record.Torikesi <> 0 Then
                        record.KeiretuCd = ""
                    End If

                    If cl.getKeiretuFlg(record.KeiretuCd) Then '3�n��

                        '<�\2>�������Ŕ����z�i�|���j�̐ݒ聣

                        Dim logic As New JibanLogic
                        Dim koumuten_gaku As Integer = 0
                        Dim zeinuki_gaku As Integer = 0

                        cl.SetDisplayString(TextKhKoumutenSeikyuuKingaku.Text, koumuten_gaku)
                        koumuten_gaku = IIf(koumuten_gaku = Integer.MinValue, 0, koumuten_gaku)

                        If logic.GetSeikyuuGaku(sender, _
                                                5, _
                                                record.KeiretuCd, _
                                                TextKhSyouhinCd.Text, _
                                                koumuten_gaku, _
                                                zeinuki_gaku) Then


                            '���i�����擾(�L�[:���i�R�[�h)
                            syouhinRec = JibanLogic.GetSyouhinInfo(TextKhSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo, ucGyoumuKyoutuu.AccKameitenCd.Value)
                            If Not syouhinRec Is Nothing Then
                                '(*3)�����L���ύX���ɁA�����ݒ肳�ꂽ�H���X�������z��0�i���i�}�X�^.�W�����i��0�j�̏ꍇ�A1��̂ݎ��������z�̎����ݒ���s���B
                                If syouhinRec.HyoujunKkk = 0 Then
                                    If HiddenKhJituseikyuu1Flg.Value = "" Then
                                        HiddenKhJituseikyuu1Flg.Value = "1" '�t���O�����Ă�

                                        ' �Ŕ����z�i���������z�j�փZ�b�g
                                        TextKhJituseikyuuKingaku.Text = zeinuki_gaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                                    End If
                                    '*****************
                                    '* �n�ՃV�X�e���̏����ɍ��킹�邽�߁A�R�����g�A�E�g
                                    '*****************
                                    'Else
                                    '    ' �Ŕ����z�i���������z�j�փZ�b�g
                                    '    TextKhJituseikyuuKingaku.Text = zeinuki_gaku.ToString(EarthConst.FORMAT_KINGAKU1)

                                End If

                            End If
                        End If

                        SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo) '���z�Čv�Z

                    End If

                End If
            End If

        End If

        '��ʐ���
        SetEnableControlKh()
    End Sub

    ''' <summary>
    ''' (31) [���ǍH���񍐏����]�������Ŕ����z�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKhJituseikyuuKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '�Z�b�g�t�H�[�J�X
        setFocusAJ(TextKhSeikyuusyoHakkouDate)

        '�������Ŕ����z
        If TextKhJituseikyuuKingaku.Text.Length = 0 Then '���͂Ȃ�
            TextKhSyouhizei.Text = "" '�����
            TextKhZeikomiKingaku.Text = "" '�ō����z

            SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo) '���z�̍Čv�Z

        Else '���͂���

            '�����L��
            If SelectKhSeikyuuUmu.SelectedValue = "1" Then '�L
                '���i�R�[�h
                If TextKhSyouhinCd.Text.Length <> 0 Then '�ݒ��

                    '�ŏ��ݒ�
                    SetKhZeiInfo(TextKhSyouhinCd.Text)

                    '������
                    If TextKhSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '���ڐ���

                        '�H���X�����Ŕ����z�����.�������Ŕ����z
                        TextKhKoumutenSeikyuuKingaku.Text = TextKhJituseikyuuKingaku.Text

                        SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo) '���z�Čv�Z

                    Else '���ڐ����ȊO
                        SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo) '���z�Čv�Z
                    End If

                End If

            End If

        End If

        '��ʐ���
        SetEnableControlKh()

    End Sub

    ''' <summary>
    ''' ���i�̊�{�����Z�b�g����(�H���񍐏��Ĕ��s)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSyouhinInfoKh()

        '���ڐ����A�������̏����擾
        Dim syouhinRec As Syouhin23Record

        '���i�R�[�h/���i���̎����ݒ�
        syouhinRec = JibanLogic.GetSyouhinInfo("", EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If syouhinRec Is Nothing Then
            TextKhSyouhinCd.Text = "" '���i�R�[�h
            SpanKhSyouhinMei.InnerHtml = "" '���i��
        Else
            TextKhSyouhinCd.Text = cl.GetDispStr(syouhinRec.SyouhinCd) '���i�R�[�h
            SpanKhSyouhinMei.InnerHtml = cl.GetDispStr(syouhinRec.SyouhinMei) '���i��
            HiddenKhZeiritu.Value = syouhinRec.Zeiritu '�ŗ�
            HiddenKhZeiKbn.Value = syouhinRec.ZeiKbn '�ŋ敪

            '��ʏ�Ő����悪�w�肳��Ă���ꍇ�A���R�[�h�̐�������㏑��
            If Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value <> String.Empty Then
                '����������R�[�h�ɃZ�b�g
                syouhinRec.SeikyuuSakiCd = Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value
                syouhinRec.SeikyuuSakiBrc = Me.ucSeikyuuSiireLink.AccSeikyuuSakiBrc.Value
                syouhinRec.SeikyuuSakiKbn = Me.ucSeikyuuSiireLink.AccSeikyuuSakiKbn.Value
            End If
            Me.TextKhSeikyuusaki.Text = syouhinRec.SeikyuuSakiType '��������
        End If

    End Sub

    ''' <summary>
    ''' �N���A/�H���񍐏��Ĕ��s
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearControlKh()

        SelectKhSeikyuuUmu.SelectedValue = "" '�����L��
        SpanKhSeikyuuUmu.Style.Add("display", "none") 'SPAN�����L��
        SpanKhSeikyuuUmu.InnerHtml = "" 'SPAN�����L��

        TextKhSyouhinCd.Text = "" '���i�R�[�h
        SpanKhSyouhinMei.InnerHtml = "" '���i��
        TextKhKoumutenSeikyuuKingaku.Text = "" '�H���X�������z
        TextKhJituseikyuuKingaku.Text = "" '���������z
        TextKhSyouhizei.Text = "" '�����
        HiddenKhSiireGaku.Value = ""    '�d����z
        HiddenKhSiireSyouhiZei.Value = ""   '�d������Ŋz
        TextKhZeikomiKingaku.Text = "" '�ō����z
        TextKhSeikyuusyoHakkouDate.Text = "" '���������s��
        TextKhUriageNengappi.Text = "" '����N����
        TextKhHattyuusyoKakutei.Text = "" '�������m��

        '���z�̍Čv�Z
        SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo)

    End Sub

#End Region

#Region "���ǍH���ύX������"

    ''' <summary>
    ''' (2) [���ǍH�����][���ǍH��]�H���d�l�m�F�ύX���̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKjKoujiSiyouKakunin_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim TyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)
        Dim strKameitenCd As String = Me.ucGyoumuKyoutuu.AccKameitenCd.Value
        Dim strSyouhinCd As String = Me.SelectKjSyouhinCd.SelectedValue

        setFocusAJ(SelectKjKoujiSiyouKakunin)

        '1.�m�F���̓��͐���
        '�H���d�l�m�F
        If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '�L

            If TextKjKakuninDate.Text = "" Then '������
                TextKjKakuninDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
            End If

        ElseIf SelectKjKoujiSiyouKakunin.SelectedValue = "" Then '��

            TextKjKakuninDate.Text = ""

        End If

        '2.<���ǍH��>�̉�ʐݒ�
        '(1)<���ǍH��>���������s���̐ݒ�
        If ucGyoumuKyoutuu.Kubun = "E" Then
            '���i�R�[�h
            If Me.SelectKjSyouhinCd.SelectedValue <> String.Empty Then '���͍�
                '�����L��
                If Me.SelectKjSeikyuuUmu.SelectedValue = "1" Then '�L
                    '���H���񒅓�
                    If Me.TextKjKankouSokuhouTyakuDate.Text <> String.Empty Then '���͍�
                        '���������s���̎����ݒ�
                        Me.SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                    End If
                End If

            End If
        End If

        '���H�����i�`�F�b�N
        If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) Then
            '�����L��
            If SelectKjSeikyuuUmu.SelectedValue = "1" Then '�L
                '�H���d�l�m�F
                If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '�L
                    '�H����А���
                    If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '���`�F�b�N
                        '���������s��
                        If TextKjSeikyuusyoHakkouDate.Text = "" Then '��
                            '���������s���̎����ݒ�
                            SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                        End If

                        '����N����
                        If TextKjUriageNengappi.Text = "" Then '��
                            TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                        End If
                    End If
                End If

            ElseIf SelectKjSeikyuuUmu.SelectedValue = "0" Then '��
                '�H���d�l�m�F
                If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '�L
                    '�H����А���
                    If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '���`�F�b�N
                        '����N����
                        If TextKjUriageNengappi.Text = "" Then '��
                            TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                        End If
                    End If
                End If
            End If

            '���H���񒅓�
            If TextKjKankouSokuhouTyakuDate.Text = "" Then '������
                '�H���d�l�m�F
                If SelectKjKoujiSiyouKakunin.SelectedValue = "" Then '��
                    '���������s��
                    TextKjSeikyuusyoHakkouDate.Text = ""
                    '����N����
                    TextKjUriageNengappi.Text = ""
                End If
            End If

            '�����L��=�Lor��
            If Me.SelectKjSeikyuuUmu.SelectedValue = "1" Or Me.SelectKjSeikyuuUmu.SelectedValue = "0" Then
                '���H���񒅓�
                If TextKjKankouSokuhouTyakuDate.Text = "" Then
                    '�H���d�l�m�F=�L�ł��A�H����А����L��=�`�F�b�N���
                    If Me.SelectKjKoujiSiyouKakunin.SelectedValue = "1" And Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked Then
                        '���������s��
                        TextKjSeikyuusyoHakkouDate.Text = ""
                        '����N����
                        TextKjUriageNengappi.Text = ""
                    End If
                End If
            End If

        End If

        '��ʐ���
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (5) [���ǍH�����][���ǍH��]�H����А����ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub CheckBoxKjKoujiKaisyaSeikyuu_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strSyouhinCd As String = Me.SelectKjSyouhinCd.SelectedValue
        Dim strKoujiCd As String = Me.TextKjKoujiKaisyaCd.Text.Trim()
        Dim strKameitenCd As String = Me.ucGyoumuKyoutuu.AccKameitenCd.Value
        Dim dicSeikyuu As New Dictionary(Of String, String)

        setFocusAJ(CheckBoxKjKoujiKaisyaSeikyuu)

        '1.<���ǍH��>�̎����ݒ�
        Me.ChgKjKaisyaSeikyuuUmu()

        '��ʐ���
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' �����ݒ�(���������s���A����N����)/�H����А����L���ύX������[����]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChgKjKaisyaSeikyuuUmu()
        Dim strKameitenCd As String = Me.ucGyoumuKyoutuu.AccKameitenCd.Value
        Dim strSyouhinCd As String = Me.SelectKjSyouhinCd.SelectedValue
        '���i�R�[�h
        If SelectKjSyouhinCd.SelectedValue <> "" Then '����
            '�����L��
            If SelectKjSeikyuuUmu.SelectedValue = "1" Then '�L
                '���H���񒅓�
                If TextKjKankouSokuhouTyakuDate.Text <> "" Then '����
                    '���������s���̎����ݒ�
                    SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                End If
            End If

            '���H�����i�`�F�b�N
            If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) Then
                '���H���񒅓�
                If TextKjKankouSokuhouTyakuDate.Text = "" Then '������
                    '�H����А���
                    If CheckBoxKjKoujiKaisyaSeikyuu.Checked Then '�`�F�b�N
                        '���������s��
                        TextKjSeikyuusyoHakkouDate.Text = ""
                        '����N����
                        TextKjUriageNengappi.Text = ""
                    End If
                End If

                '�����L��
                If SelectKjSeikyuuUmu.SelectedValue = "1" Then '�L
                    '�H���d�l�m�F
                    If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '�L
                        '�H����А���
                        If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '���`�F�b�N

                            '���������s���̎����ݒ�
                            SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)

                            '����N����
                            If TextKjUriageNengappi.Text = "" Then '��
                                TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                            End If
                        End If
                    End If

                ElseIf SelectKjSeikyuuUmu.SelectedValue = "0" Then '��
                    '�H���d�l�m�F
                    If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '�L
                        '�H����А���
                        If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '���`�F�b�N
                            '���������s��
                            TextKjSeikyuusyoHakkouDate.Text = ""
                            '����N����
                            If TextKjUriageNengappi.Text = "" Then '��
                                TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                            End If
                        End If
                    End If

                End If

                '�����L��=�Lor��
                If Me.SelectKjSeikyuuUmu.SelectedValue = "1" Or Me.SelectKjSeikyuuUmu.SelectedValue = "0" Then
                    '���H���񒅓�
                    If TextKjKankouSokuhouTyakuDate.Text = "" Then
                        '�H���d�l�m�F=�L�ł��A�H����А����L��=�`�F�b�N���
                        If Me.SelectKjKoujiSiyouKakunin.SelectedValue = "1" And Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked Then
                            '���������s��
                            TextKjSeikyuusyoHakkouDate.Text = ""
                            '����N����
                            TextKjUriageNengappi.Text = ""
                        End If
                    End If
                End If

            End If

        End If

        '��ʐ���
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (6) [���ǍH�����][���ǍH��]���ǍH����ʕύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKjKairyouKoujiSyubetu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim tmpScript As String = ""
        Dim KjLogic As New KairyouKoujiLogic
        Dim blnRet As Boolean = False

        setFocusAJ(SelectKjKairyouKoujiSyubetu)

        If SelectKjKairyouKoujiSyubetu.SelectedValue <> String.Empty Then
            '1.������ǍH����ʂ̃`�F�b�N

            '�@����H����ʐݒ�}�X�^���A�ȉ��̏����ɊY������f�[�^�𒊏o����B
            blnRet = KjLogic.GetKairyouKoujiSyubetu( _
                            HiddenHanteiSetuzokuMoji.Value _
                            , SelectKjKairyouKoujiSyubetu.SelectedValue _
                            , HiddenHantei1Cd.Value _
                            , HiddenHantei2Cd.Value _
                            )

            '�A�Y���f�[�^�����݂��Ȃ��ꍇ�A�������́A<���ǍH�����>����ڑ���2�i���́j�̏ꍇ�A�ȉ��̏������s���B��
            If blnRet = False Or HiddenHanteiSetuzokuMoji.Value = "2" Then '����
                '����H����ʊm�F���b�Z�[�W��\������B
                If SelectKjKairyouKoujiSyubetu.SelectedValue <> HiddenKjKairyouKoujiSyubetuMae.Value Then
                    tmpScript = "checkKoujiSyubetu('" & Messages.MSG079C & "','" & Messages.MSG080C & "','" & SelectKjKairyouKoujiSyubetu.ClientID & "','" & HiddenKjKairyouKoujiSyubetuMae.ClientID & "');"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectKjKairyouKoujiSyubetu_SelectedIndexChanged", tmpScript, True)
                End If
            End If
        End If

        '��ʐ���
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (8) [���ǍH�����][���ǍH��]���i�R�[�h�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKjSyouhinCd_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim KjLogic As New KairyouKoujiLogic
        Dim SyouhinMeisaiRecord As New SyouhinMeisaiRecord
        Dim tmpScript As String = ""

        setFocusAJ(SelectKjSyouhinCd)

        Dim strKameitenCd As String = Me.ucGyoumuKyoutuu.AccKameitenCd.Value
        Dim strSyouhinCd As String = Me.SelectKjSyouhinCd.SelectedValue

        '1.<���ǍH��>�̎����ݒ�
        '���i�R�[�h
        If SelectKjSyouhinCd.SelectedValue <> "" Then '�ݒ��

            '�ۏ؏��i�L��<>"1"���A�ύX�O.���i�R�[�h=������
            If Me.HiddenHosyouSyouhinUmuOld.Value <> "1" And HiddenKjSyouhinCdMae.Value = "" Then

                '���i�R�[�h�m�F���b�Z�[�W��\������B
                If SelectKjSyouhinCd.SelectedValue <> HiddenKjSyouhinCdMae.Value Then
                    tmpScript = "ChkSyohinCd('" & Messages.MSG115C & "','" & SelectKjSyouhinCd.ClientID & "','" & HiddenKjSyouhinCdMae.ClientID & "');"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectKjSyouhinCd_SelectedIndexChanged", tmpScript, True)
                End If

            End If

            '�����L�����󔒂̏ꍇ
            If SelectKjSeikyuuUmu.SelectedValue = "" Then
                SelectKjSeikyuuUmu.SelectedValue = "1" '�L
            End If

            '********************************************************************
            '�����i�}�X�^�ƍH�����i�}�X�^����̎擾��؂�ւ���

            '���i�E������z�E�����L���̎����ݒ�i���iM or �H�����iM�j
            If SetSyouhinInfoFromKojM(EnumKoujiType.KairyouKouji, EarthEnum.emKojKkkActionType.SyouhinCd) = False Then
                SetSyouhinInfo(EnumKoujiType.KairyouKouji)
            End If

            '���z�̍Čv�Z
            SetKingakuUriage(EnumKoujiType.KairyouKouji)

            '�������m�肪�󔒂̏ꍇ
            If TextKjHattyuusyoKakutei.Text = "" Then
                TextKjHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI '���m��
            End If

            '�����L��
            If SelectKjSeikyuuUmu.SelectedValue = "1" Then '�L

                '���H���񒅓�
                If TextKjKankouSokuhouTyakuDate.Text <> "" Then '�ݒ��

                    '���������s��
                    If TextKjSeikyuusyoHakkouDate.Text = "" Then '��
                        '���������s���̎����ݒ�
                        SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                    End If

                    '����N����
                    If TextKjUriageNengappi.Text = "" Then '��
                        TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If

                End If

            ElseIf Me.SelectKjSeikyuuUmu.SelectedValue = "0" Or Me.SelectKjSeikyuuUmu.SelectedValue = "" Then   '����
                '���������s���̎����ݒ�
                Me.TextKjSeikyuusyoHakkouDate.Text = ""
            End If

            '**********************************************************************

            '���H�����i�`�F�b�N
            '�ύX�O=B2000�ԑ�ł��A�ύX��=B2000�ԑ�ȊO
            If cl.ChkSyouhinCdB2000(HiddenKjSyouhinCdMae.Value) AndAlso cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) = False Then
                '�@�ʐ����e�[�u��.����v��FLG<>1�A���A���H���񒅓�=������
                If Me.SpanKjUriageSyoriZumi.InnerHtml <> EarthConst.URIAGE_ZUMI And Me.TextKjKankouSokuhouTyakuDate.Text = String.Empty Then '������
                    '���������s��
                    Me.TextKjSeikyuusyoHakkouDate.Text = String.Empty
                    '����N����
                    Me.TextKjUriageNengappi.Text = String.Empty
                End If
            End If

            '���H�����i�`�F�b�N
            If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) Then
                '�����L��
                If SelectKjSeikyuuUmu.SelectedValue = "1" Then '�L
                    '�H���d�l�m�F
                    If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '�L
                        '�H����А���
                        If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '���`�F�b�N
                            '���������s��
                            If TextKjSeikyuusyoHakkouDate.Text = "" Then '��
                                '���������s���̎����ݒ�
                                SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                            End If

                            '����N����
                            If TextKjUriageNengappi.Text = "" Then '��
                                TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                            End If
                        End If
                    End If

                Else '��
                    '�H���d�l�m�F
                    If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '�L
                        '�H����А���
                        If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '���`�F�b�N
                            '����N����
                            If TextKjUriageNengappi.Text = "" Then '��
                                TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                            End If
                        End If
                    End If

                End If

                '�����L��=�Lor��
                If Me.SelectKjSeikyuuUmu.SelectedValue = "1" Or Me.SelectKjSeikyuuUmu.SelectedValue = "0" Then
                    '���H���񒅓�
                    If TextKjKankouSokuhouTyakuDate.Text = "" Then
                        '�H���d�l�m�F=�L�ł��A�H����А����L��=�`�F�b�N���
                        If Me.SelectKjKoujiSiyouKakunin.SelectedValue = "1" And Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked Then
                            '���������s��
                            TextKjSeikyuusyoHakkouDate.Text = ""
                            '����N����
                            TextKjUriageNengappi.Text = ""
                        End If
                    End If
                End If

            End If

        Else '������

            '(*1)���������z��0,����,��Null�̏ꍇ�A�G���[���b�Z�[�W�i037�j��\�����A�����ݒ�i�N���A�j���s�킸�ɁA�����͂ɕύX��������(������/�Ĕ��s��/�����L��)�͌��̒l�ɖ߂��B
            If TextKjHattyuusyoKingaku.Text <> "0" And TextKjHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                SelectKjSyouhinCd.SelectedValue = HiddenKjSyouhinCdMae.Value '�ύX�O�̒l�ɖ߂� ���� Javascript�ł̐ݒ�ł�Change�C�x���g���������Ȃ�
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectKjSyouhinCd_SelectedIndexChanged", tmpScript, True)
                Exit Sub
            End If

            SelectKjSeikyuuUmu.SelectedValue = "" '����
            TextKjUriageNengappi.Text = "" '����N����
            TextKjUriageZeinukiKingaku.Text = "" '������z/�Ŕ����z
            TextKjUriageSyouhizei.Text = "" '������z/�����
            TextKjUriageZeikomiKingaku.Text = "" '������z/�ō����z
            TextKjSiireZeinukiKingaku.Text = "" '�d�����z/�Ŕ����z
            TextKjSiireSyouhizei.Text = "" '�d�����z/�����
            TextKjSiireZeikomiKingaku.Text = "" '�d�����z/�ō����z
            TextKjZangaku.Text = "0" '�c�z
            TextKjSeikyuusyoHakkouDate.Text = "" '���������s��
            TextKjHattyuusyoKakutei.Text = "" '�������m��

        End If

        '��ʐ���
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (9) [���ǍH�����][���ǍH��]�����L���ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKjSeikyuuUmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strKameitenCd As String = Me.ucGyoumuKyoutuu.AccKameitenCd.Value
        Dim strSyouhinCd As String = Me.SelectKjSyouhinCd.SelectedValue

        setFocusAJ(SelectKjSeikyuuUmu)

        '���i�R�[�h
        If SelectKjSyouhinCd.SelectedValue <> "" Then '�ݒ��

            '�����L��
            If SelectKjSeikyuuUmu.SelectedValue = "1" Then '�L

                '������z/�Ŕ����z���󔒂̏ꍇ
                If TextKjUriageZeinukiKingaku.Text = "" Or TextKjUriageZeinukiKingaku.Text = "0" Then

                    '���i�E������z�E�����L���̎����ݒ�i���iM or �H�����iM�j
                    If SetSyouhinInfoFromKojM(EnumKoujiType.KairyouKouji, EarthEnum.emKojKkkActionType.SeikyuuUmu) = False Then
                        SetSyouhinInfo(EnumKoujiType.KairyouKouji)
                    End If

                End If

                SetKingakuUriage(EnumKoujiType.KairyouKouji) '���z�̍Čv�Z

                '���H�����i�`�F�b�N
                If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) Then
                    '�H���d�l�m�F
                    If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '�L
                        '�H����А���
                        If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '���`�F�b�N
                            '���������s��
                            If TextKjSeikyuusyoHakkouDate.Text = "" Then '��
                                '���������s���̎����ݒ�
                                SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                            End If
                            '����N����
                            TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)

                        End If
                    End If

                    '�����L��=�Lor��
                    If Me.SelectKjSeikyuuUmu.SelectedValue = "1" Or Me.SelectKjSeikyuuUmu.SelectedValue = "0" Then
                        '���H���񒅓�
                        If TextKjKankouSokuhouTyakuDate.Text = "" Then
                            '�H���d�l�m�F=�L�ł��A�H����А����L��=�`�F�b�N���
                            If Me.SelectKjKoujiSiyouKakunin.SelectedValue = "1" And Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked Then
                                '���������s��
                                TextKjSeikyuusyoHakkouDate.Text = ""
                                '����N����
                                TextKjUriageNengappi.Text = ""
                            End If
                        End If
                    End If

                End If

                '���H���񒅓�
                If TextKjKankouSokuhouTyakuDate.Text <> "" Then '�ݒ��

                    '���������s���̎����ݒ聣
                    If TextKjSeikyuusyoHakkouDate.Text = "" Then '��
                        '���������s���̎����ݒ�
                        SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)

                    End If

                    '����N����
                    TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)

                End If


            ElseIf SelectKjSeikyuuUmu.SelectedValue = "0" Or SelectKjSeikyuuUmu.SelectedValue = "" Then '��or��

                '������z�n��0�N���A
                TextKjUriageZeinukiKingaku.Text = "0"
                TextKjUriageSyouhizei.Text = "0"
                TextKjUriageZeikomiKingaku.Text = "0"

                SetKingakuUriage(EnumKoujiType.KairyouKouji) '���z�̍Čv�Z

                TextKjSeikyuusyoHakkouDate.Text = "" '���������s��

                '���H�����i�`�F�b�N
                If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) Then
                    '�H���d�l�m�F
                    If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '�L
                        '�H����А���
                        If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '���`�F�b�N
                            '����N����
                            TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                        End If
                    End If

                    '�����L��=�Lor��
                    If Me.SelectKjSeikyuuUmu.SelectedValue = "1" Or Me.SelectKjSeikyuuUmu.SelectedValue = "0" Then
                        '���H���񒅓�
                        If TextKjKankouSokuhouTyakuDate.Text = "" Then
                            '�H���d�l�m�F=�L�ł��A�H����А����L��=�`�F�b�N���
                            If Me.SelectKjKoujiSiyouKakunin.SelectedValue = "1" And Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked Then
                                '���������s��
                                TextKjSeikyuusyoHakkouDate.Text = ""
                                '����N����
                                TextKjUriageNengappi.Text = ""
                            End If
                        End If
                    End If
                End If

                '���H���񒅓�
                If TextKjKankouSokuhouTyakuDate.Text <> "" Then '�ݒ��
                    '����N����
                    TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                End If

            End If

        End If


        '��ʐ���
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (10) [���ǍH�����][���ǍH��]���ǔ�����z�i�Ŕ��j�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKjUriageZeinukiKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(TextKjSiireZeinukiKingaku)

        SetKingakuUriage(EnumKoujiType.KairyouKouji) '���z�̍Čv�Z

        '��ʐ���
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (11) [���ǍH�����][���ǍH��]���ǎd�����z�i�Ŕ��j�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKjSiireZeinukiKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(TextKjKoujiDate)

        SetKingakuSiire(EnumKoujiType.KairyouKouji) '���z�̍Čv�Z

        '��ʐ���
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (13) [���ǍH�����][���ǍH��]���H���񒅓��ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKjKankouSokuhouTyakuDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strKameitenCd As String = Me.ucGyoumuKyoutuu.AccKameitenCd.Value
        Dim strSyouhinCd As String = Me.SelectKjSyouhinCd.SelectedValue

        setFocusAJ(TextKjKankouSokuhouTyakuDate)

        '���i�R�[�h
        If SelectKjSyouhinCd.SelectedValue <> "" Then '�ݒ��

            '�����L��
            If SelectKjSeikyuuUmu.SelectedValue = "1" Then '�L

                setFocusAJ(TextKjSeikyuusyoHakkouDate)

                '���H���񒅓�
                If TextKjKankouSokuhouTyakuDate.Text <> "" Then '�ݒ��

                    '���������s���̎����ݒ聣
                    If TextKjSeikyuusyoHakkouDate.Text = "" Then '��
                        '���������s���̎����ݒ�
                        SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)

                    End If

                    '����N����
                    If TextKjUriageNengappi.Text = "" Then '��
                        TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If
                End If

            ElseIf SelectKjSeikyuuUmu.SelectedValue = "0" Then '��

                '���H���񒅓�
                If TextKjKankouSokuhouTyakuDate.Text <> "" Then '�ݒ��
                    '����N����
                    If TextKjUriageNengappi.Text = "" Then '��
                        TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If
                End If

            End If

            '���H�����i�`�F�b�N
            If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) = False Then '<>���H��
                '�@�ʐ����e�[�u��.����v��FLG<>1�A���A���H���񒅓�=������
                If Me.SpanKjUriageSyoriZumi.InnerHtml <> EarthConst.URIAGE_ZUMI And Me.TextKjKankouSokuhouTyakuDate.Text = String.Empty Then '������
                    '���������s��
                    Me.TextKjSeikyuusyoHakkouDate.Text = String.Empty
                    '����N����
                    Me.TextKjUriageNengappi.Text = String.Empty
                End If

            Else '���H��

                '�@�ʐ����e�[�u��.����v��FLG<>1�A���A���H���񒅓�=������
                If Me.SpanKjUriageSyoriZumi.InnerHtml <> EarthConst.URIAGE_ZUMI And TextKjKankouSokuhouTyakuDate.Text = String.Empty Then '������

                    '�H���d�l�m�F
                    If Me.SelectKjKoujiSiyouKakunin.SelectedValue = String.Empty Then '��
                        '���������s��
                        Me.TextKjSeikyuusyoHakkouDate.Text = String.Empty
                        '����N����
                        Me.TextKjUriageNengappi.Text = String.Empty
                    End If

                    '�H����А���
                    If CheckBoxKjKoujiKaisyaSeikyuu.Checked Then '�`�F�b�N
                        '���������s��
                        Me.TextKjSeikyuusyoHakkouDate.Text = String.Empty
                        '����N����
                        Me.TextKjUriageNengappi.Text = String.Empty
                    End If

                End If

            End If

        End If

        '��ʐ���
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (14) [���ǍH�����][���ǍH��]���������s���ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKjSeikyuusyoHakkouDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""

        setFocusAJ(TextKjSeikyuusyoHakkouDate)

        '���������s��
        If TextKjSeikyuusyoHakkouDate.Text = "" Then Exit Sub '�����͂̏ꍇ�A�����𔲂���

        '�H����
        '(1)�H�����������͂̏ꍇ
        If TextKjKoujiDate.Text = "" Then '������
            '�o�^����OK���Ă��Ȃ��ꍇ
            If HiddenKjSeikyuusyoHakkouDateMsg1.Value <> "1" Then
                '�o�^�m�F���b�Z�[�W��\������B
                tmpScript = "if(confirm('" & Messages.MSG078W & "')){" & vbCrLf
                tmpScript &= "  objEBI('" & HiddenKjSeikyuusyoHakkouDateMsg1.ClientID & "').value = '1';" & vbCrLf
                tmpScript &= "}" & vbCrLf
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextKjSeikyuusyoHakkouDate_TextChanged1", tmpScript, True)
            End If
        End If

        '(2)�H������<���ǍH��>���������s���̏ꍇ
        If TextKjKoujiDate.Text.Length <> 0 And TextKjSeikyuusyoHakkouDate.Text.Length <> 0 Then

            Dim dtKouji As Date = Date.Parse(TextKjKoujiDate.Text)
            Dim dtSeikyuu As Date = Date.Parse(TextKjSeikyuusyoHakkouDate.Text)

            If dtKouji > dtSeikyuu Then

                '�o�^����OK���Ă��Ȃ��ꍇ
                If HiddenKjSeikyuusyoHakkouDateMsg2.Value <> "1" Then

                    '�o�^�m�F���b�Z�[�W��\������B
                    '���������s�������ǍH�������Â����t�ł����A<���s>���������s����o�^�ł���悤�ɂ��܂����H
                    Dim strMsg As String = Messages.MSG066C.Replace("@PARAM1", "�H����")

                    '�m�F���b�Z�[�W�\��
                    tmpScript = "if(confirm('" & strMsg & "')){" & vbCrLf
                    tmpScript &= "  objEBI('" & HiddenKjSeikyuusyoHakkouDateMsg2.ClientID & "').value = '1';" & vbCrLf
                    tmpScript &= "}" & vbCrLf
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextKjSeikyuusyoHakkouDate_TextChanged2", tmpScript, True)
                End If

            End If

        End If

    End Sub

#End Region

#Region "�ǉ��H���ύX������"

    ''' <summary>
    ''' (16) [���ǍH�����][�ǉ��H��]���ǍH����ʕύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectTjKairyouKoujiSyubetu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim tmpScript As String = ""
        Dim KjLogic As New KairyouKoujiLogic
        Dim blnRet As Boolean = False

        setFocusAJ(SelectTjKairyouKoujiSyubetu)

        '1.������ǍH����ʂ̃`�F�b�N

        '�@����H����ʐݒ�}�X�^���A�ȉ��̏����ɊY������f�[�^�𒊏o����B
        blnRet = KjLogic.GetKairyouKoujiSyubetu( _
                        HiddenHanteiSetuzokuMoji.Value _
                        , SelectTjKairyouKoujiSyubetu.SelectedValue _
                        , HiddenHantei1Cd.Value _
                        , HiddenHantei2Cd.Value _
                        )

        '�A�Y���f�[�^�����݂��Ȃ��ꍇ�A�������́A<���ǍH�����>����ڑ���2�i���́j�̏ꍇ�A�ȉ��̏������s���B��
        If blnRet = False Or HiddenHanteiSetuzokuMoji.Value = "2" Then '����
            '����H����ʊm�F���b�Z�[�W��\������B
            If SelectTjKairyouKoujiSyubetu.SelectedValue <> HiddenTjKairyouKoujiSyubetuMae.Value Then
                tmpScript = "checkKoujiSyubetu('" & Messages.MSG079C & "','" & Messages.MSG080C & "','" & SelectTjKairyouKoujiSyubetu.ClientID & "','" & HiddenTjKairyouKoujiSyubetuMae.ClientID & "');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectTjKairyouKoujiSyubetu_SelectedIndexChanged", tmpScript, True)
            End If
        End If

        '��ʐ���
        SetEnableControlTj()

    End Sub

    ''' <summary>
    ''' (19) [���ǍH�����][�ǉ��H��]���H���񒅓��ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextTjKankouSokuhouTyakuDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strSyouhinCd As String = SelectTjSyouhinCd.SelectedValue

        setFocusAJ(TextTjKankouSokuhouTyakuDate)

        '���i�R�[�h
        If SelectTjSyouhinCd.SelectedValue <> "" Then '�ݒ��

            '�����L��
            If SelectTjSeikyuuUmu.SelectedValue = "1" Then '�L

                setFocusAJ(TextTjSeikyuusyoHakkouDate)

                '���H���񒅓�
                If TextTjKankouSokuhouTyakuDate.Text <> "" Then '�ݒ��

                    '���������s���̎����ݒ聣
                    If TextTjSeikyuusyoHakkouDate.Text = "" Then '��
                        '���������s���̎����ݒ�
                        SetTjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                    End If

                    '����N����
                    If TextTjUriageNengappi.Text = "" Then '��
                        TextTjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If

                Else '������
                    '������
                    If SpanTjUriageSyorizumi.InnerHtml <> EarthConst.URIAGE_ZUMI Then
                        '��ʋN�����Ɋ��H���񒅓�����ŁA�������Ԃ������ꍇ
                        '���������s��,����N�����̂��N���A
                        TextTjSeikyuusyoHakkouDate.Text = String.Empty
                        TextTjUriageNengappi.Text = String.Empty
                    End If
                End If

            ElseIf SelectTjSeikyuuUmu.SelectedValue = "0" Then '��

                '���H���񒅓�
                If TextTjKankouSokuhouTyakuDate.Text <> "" Then '�ݒ��
                    '����N����
                    TextTjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)

                Else '������

                    '������
                    If SpanTjUriageSyorizumi.InnerHtml <> EarthConst.URIAGE_ZUMI Then
                        '���������s��,����N�����̂��N���A
                        TextTjSeikyuusyoHakkouDate.Text = String.Empty
                        TextTjUriageNengappi.Text = String.Empty
                    End If
                End If

            End If
        End If

        '��ʐ���
        SetEnableControlTj()

    End Sub

    ''' <summary>
    ''' (20) [���ǍH�����][�ǉ��H��]���i�R�[�h�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectTjSyouhinCd_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strSyouhinCd As String = SelectTjSyouhinCd.SelectedValue

        setFocusAJ(SelectTjSyouhinCd)

        '1.<�ǉ��H��>�̎����ݒ�
        '���i�R�[�h
        If SelectTjSyouhinCd.SelectedValue <> "" Then '�ݒ��

            '�����L�����󔒂̏ꍇ
            If SelectTjSeikyuuUmu.SelectedValue = "" Then
                SelectTjSeikyuuUmu.SelectedValue = "1" '�L
            End If

            '���i�E������z�E�����L���̎����ݒ�i���iM or �H�����iM�j
            If SetSyouhinInfoFromKojM(EnumKoujiType.TuikaKouji, EarthEnum.emKojKkkActionType.SyouhinCd) = False Then
                SetSyouhinInfo(EnumKoujiType.TuikaKouji)
            End If

            '���z�̍Čv�Z
            SetKingakuUriage(EnumKoujiType.TuikaKouji)

            '�������m�肪�󔒂̏ꍇ
            If TextTjHattyuusyoKakutei.Text = "" Then
                TextTjHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI '���m��
            End If

            '�����L��
            If SelectTjSeikyuuUmu.SelectedValue = "1" Then '�L

                '���H���񒅓�
                If TextTjKankouSokuhouTyakuDate.Text <> "" Then '�ݒ��

                    '���������s��
                    If TextTjSeikyuusyoHakkouDate.Text = "" Then '��
                        '���������s���̎����ݒ�
                        SetTjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                    End If

                    '����N����
                    If TextTjUriageNengappi.Text = "" Then '��
                        TextTjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If

                End If

            ElseIf Me.SelectTjSeikyuuUmu.SelectedValue = "0" Or Me.SelectTjSeikyuuUmu.SelectedValue = "" Then   '����
                '���������s���̎����ݒ�
                Me.TextTjSeikyuusyoHakkouDate.Text = ""
            End If


        Else '������

            '(*1)���������z��0,����,��Null�̏ꍇ�A�G���[���b�Z�[�W�i037�j��\�����A�����ݒ�i�N���A�j���s�킸�ɁA�����͂ɕύX��������(������/�Ĕ��s��/�����L��)�͌��̒l�ɖ߂��B
            If TextTjHattyuusyoKingaku.Text <> "0" And TextTjHattyuusyoKingaku.Text <> "" Then
                Dim tmpScript As String = "alert('" & Messages.MSG010E & "');" & vbCrLf
                SelectTjSyouhinCd.SelectedValue = HiddenTjSyouhinCdMae.Value '�ύX�O�̒l�ɖ߂� ���� Javascript�ł̐ݒ�ł�Change�C�x���g���������Ȃ�
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectTjSyouhinCd_SelectedIndexChanged", tmpScript, True)
                Exit Sub
            End If

            SelectTjSeikyuuUmu.SelectedValue = "" '����
            TextTjUriageNengappi.Text = "" '����N����
            TextTjUriageZeinukiKingaku.Text = "" '������z/�Ŕ����z
            TextTjUriageSyouhizei.Text = "" '������z/�����
            TextTjUriageZeikomiKingaku.Text = "" '������z/�ō����z
            TextTjSiireZeinukiKingaku.Text = "" '�d�����z/�Ŕ����z
            TextTjSiireSyouhizei.Text = "" '�d�����z/�����
            TextTjSiireZeikomiKingaku.Text = "" '�d�����z/�ō����z
            TextTjZangaku.Text = "0" '�c�z
            TextTjSeikyuusyoHakkouDate.Text = "" '���������s��
            TextTjHattyuusyoKakutei.Text = "" '�������m��

        End If

        '��ʐ���
        SetEnableControlTj()

    End Sub

    ''' <summary>
    ''' (21) [���ǍH�����][�ǉ��H��]�����L���ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectTjSeikyuuUmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strSyouhinCd As String = SelectTjSyouhinCd.SelectedValue

        setFocusAJ(SelectTjSeikyuuUmu)

        '���i�R�[�h
        If SelectTjSyouhinCd.SelectedValue <> "" Then '�ݒ��

            '�����L��
            If SelectTjSeikyuuUmu.SelectedValue = "1" Then '�L

                '������z/�Ŕ����z���󔒂̏ꍇ
                If TextTjUriageZeinukiKingaku.Text = "" Or TextTjUriageZeinukiKingaku.Text = "0" Then

                    '���i�E������z�E�����L���̎����ݒ�i���iM or �H�����iM�j
                    If SetSyouhinInfoFromKojM(EnumKoujiType.TuikaKouji, EarthEnum.emKojKkkActionType.SeikyuuUmu) = False Then
                        SetSyouhinInfo(EnumKoujiType.TuikaKouji)
                    End If

                End If

                SetKingakuUriage(EnumKoujiType.TuikaKouji) '���z�̍Čv�Z

                '���H���񒅓�
                If TextTjKankouSokuhouTyakuDate.Text <> "" Then '�ݒ��

                    '���������s���̎����ݒ聣
                    If TextTjSeikyuusyoHakkouDate.Text = "" Then '��
                        '���������s���̎����ݒ�
                        SetTjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                    End If

                    '����N����
                    TextTjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)

                End If


            ElseIf SelectTjSeikyuuUmu.SelectedValue = "0" Or SelectTjSeikyuuUmu.SelectedValue = "" Then '�� or ��

                '������z�n��0�N���A
                TextTjUriageZeinukiKingaku.Text = "0"
                TextTjUriageSyouhizei.Text = "0"
                TextTjUriageZeikomiKingaku.Text = "0"

                SetKingakuUriage(EnumKoujiType.TuikaKouji) '���z�̍Čv�Z

                TextTjSeikyuusyoHakkouDate.Text = "" '���������s��

                '���H���񒅓�
                If TextTjKankouSokuhouTyakuDate.Text <> "" Then '�ݒ��
                    '����N����
                    TextTjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                End If

            End If

        End If

        '��ʐ���
        SetEnableControlTj()
    End Sub

    ''' <summary>
    ''' (22) [���ǍH�����][�ǉ��H��]�ǉ����ǔ�����z�i�Ŕ��j�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextTjUriageZeinukiKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(TextTjSiireZeinukiKingaku)

        SetKingakuUriage(EnumKoujiType.TuikaKouji) '���z�̍Čv�Z

        '��ʐ���
        SetEnableControlTj()

    End Sub

    ''' <summary>
    ''' (23) [���ǍH�����][�ǉ��H��]�ǉ����ǎd�����z�i�Ŕ��j�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextTjSiireZeinukiKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(TextTjKoujiDate)

        SetKingakuSiire(EnumKoujiType.TuikaKouji) '���z�̍Čv�Z

        '��ʐ���
        SetEnableControlTj()

    End Sub

    ''' <summary>
    ''' (24) [���ǍH�����][�ǉ��H��]���������s���ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextTjSeikyuusyoHakkouDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""

        setFocusAJ(TextTjSeikyuusyoHakkouDate)

        '���������s��
        If TextTjSeikyuusyoHakkouDate.Text = "" Then Exit Sub '�����͂̏ꍇ�A�����𔲂���

        '�H����
        '(1)�H�����������͂̏ꍇ
        If TextTjKoujiDate.Text = "" Then '������
            '�o�^����OK���Ă��Ȃ��ꍇ
            If HiddenTjSeikyuusyoHakkouDateMsg1.Value <> "1" Then
                '�o�^�m�F���b�Z�[�W��\������B
                tmpScript = "if(confirm('" & Messages.MSG078W & "')){" & vbCrLf
                tmpScript &= "  objEBI('" & HiddenTjSeikyuusyoHakkouDateMsg1.ClientID & "').value = '1';" & vbCrLf
                tmpScript &= "}" & vbCrLf
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextTjSeikyuusyoHakkouDate_TextChanged1", tmpScript, True)
            End If
        End If

        '(2)�H������<���ǍH��>���������s���̏ꍇ
        If TextTjKoujiDate.Text.Length <> 0 And TextTjSeikyuusyoHakkouDate.Text.Length <> 0 Then

            Dim dtKouji As Date = Date.Parse(TextTjKoujiDate.Text)
            Dim dtSeikyuu As Date = Date.Parse(TextTjSeikyuusyoHakkouDate.Text)

            If dtKouji > dtSeikyuu Then

                '�o�^����OK���Ă��Ȃ��ꍇ
                If HiddenTjSeikyuusyoHakkouDateMsg2.Value <> "1" Then

                    '�o�^�m�F���b�Z�[�W��\������B
                    '���������s�������ǍH�������Â����t�ł����A<���s>���������s����o�^�ł���悤�ɂ��܂����H
                    Dim strMsg As String = Messages.MSG066C.Replace("@PARAM1", "�H����")

                    setFocusAJ(TextTjSeikyuusyoHakkouDate)

                    '�m�F���b�Z�[�W�\��
                    tmpScript = "if(confirm('" & strMsg & "')){" & vbCrLf
                    tmpScript &= "  objEBI('" & HiddenTjSeikyuusyoHakkouDateMsg2.ClientID & "').value = '1';" & vbCrLf
                    tmpScript &= "}" & vbCrLf
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextTjSeikyuusyoHakkouDate_TextChanged2", tmpScript, True)
                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' (5) [���ǍH�����][�ǉ��H��]�H����А����ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub CheckBoxTjKoujiKaisyaSeikyuu_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(CheckBoxTjKoujiKaisyaSeikyuu)

        '�H����А����L���ύX������[����]
        Me.ChgTjKoujiKaisyaSeikyuuUmu()

        '��ʐ���
        SetEnableControlTj()

    End Sub

    ''' <summary>
    ''' �ǉ��H��.�����ݒ�(���������s���A����N����)/�H����А����L���ύX������[����]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChgTjKoujiKaisyaSeikyuuUmu()
        Dim strSyouhinCd As String = Me.SelectTjSyouhinCd.SelectedValue
        Dim strKoujiCd As String = Me.TextTjKoujiKaisyaCd.Text.Trim()
        Dim strKameitenCd As String = Me.ucGyoumuKyoutuu.AccKameitenCd.Value

        '2.<�ǉ��H��>�̎����ݒ�
        '���i�R�[�h
        If SelectTjSyouhinCd.SelectedValue <> "" Then '����
            '�����L��
            If SelectTjSeikyuuUmu.SelectedValue = "1" Then '�L
                '���H���񒅓�
                If TextTjKankouSokuhouTyakuDate.Text <> "" Then '����
                    '���������s���̎����ݒ�
                    SetTjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                End If
            End If
        End If

    End Sub

#End Region

    ''' <summary>
    ''' �_�~�[�{�^�������ɂ�PostBack�������s�Ȃ�(���i�R�[�h�ύX�������̃L�����Z���̂���)
    ''' �������Ǎ����̂�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonDummy_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.ButtonDummy.Value = "1"

        If ButtonTouroku1.Disabled = False Then
            setFocusAJ(ButtonTouroku1)
        End If

        Me.UpdatePanelKairyouKouji.Update()

    End Sub

    ''' <summary>
    ''' �R�s�[�{�^���������̐��������s���A����N�����̃`�F�b�N����ю����ݒ�
    ''' �������Ǎ����Ȃ̂ŁA���������s���ŕK�v�ȉ����X�R�[�h�A�敪�͕ʓr�w��(���[�U�[�R���g���[���w��̂���)
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ChkOnCopyAutoSetting(ByVal strKameitenCd As String, ByVal strKbn As String)
        Dim strSyouhinCd As String = SelectKjSyouhinCd.SelectedValue

        '���㏈���ϔ��f�t���O�̎擾
        Dim strUriageZumi As String = cl.GetUriageSyoriZumiFlg(Me.SpanKjUriageSyoriZumi.InnerHtml)
        '�\�����[�h�̎擾
        Dim strViewMode As String = cl.GetViewMode(strUriageZumi, userinfo.KeiriGyoumuKengen)
        '���������s���̎����ݒ�p�ɁA������E�d����ύX�����N�ɉ����X���̏����Z�b�g
        Me.ucSeikyuuSiireLinkKai.SetVariableValueCtrlFromParent(strKameitenCd _
                                                                , Me.SelectKjSyouhinCd.SelectedValue _
                                                                , Me.TextKjKoujiKaisyaCd.Text _
                                                                , strUriageZumi _
                                                                , Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked _
                                                                , Me.TextKjKoujiKaisyaCd.Text _
                                                                , _
                                                                , strViewMode)

        '�����w��ɂăR�s�[�������s�Ȃ����ꍇ�A���������s��/����N�����̎����ݒ���s�Ȃ�
        If Not pStrCopy Is Nothing AndAlso pStrCopy = "1" Then

            '���i�R�[�h
            If SelectKjSyouhinCd.SelectedValue <> "" Then '����
                '�����L��
                If SelectKjSeikyuuUmu.SelectedValue = "1" Then '�L
                    '1)���H���񒅓�
                    If TextKjKankouSokuhouTyakuDate.Text <> "" Then '����
                        '���������s���̎����ݒ�
                        SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)

                        '����N����
                        If TextKjUriageNengappi.Text = "" Then '��
                            TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                        End If

                    End If

                ElseIf SelectKjSeikyuuUmu.SelectedValue = "0" Then '��

                    '2)���H���񒅓�
                    If TextKjKankouSokuhouTyakuDate.Text <> String.Empty Then '����
                        '���������s��
                        TextKjSeikyuusyoHakkouDate.Text = String.Empty

                        '����N����
                        If TextKjUriageNengappi.Text = "" Then '��
                            TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                        End If
                    End If

                End If

                '���H�����i�`�F�b�N
                If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) Then

                    '�����L��
                    If SelectKjSeikyuuUmu.SelectedValue = "1" Then '�L
                        '�H���d�l�m�F
                        If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '�L
                            '3)�H����А���
                            If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '���`�F�b�N

                                '���������s���̎����ݒ�
                                SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)

                                '����N����
                                If TextKjUriageNengappi.Text = "" Then '��
                                    TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                                End If
                            End If
                        End If

                    ElseIf SelectKjSeikyuuUmu.SelectedValue = "0" Then '��
                        '�H���d�l�m�F
                        If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '�L
                            '4)�H����А���
                            If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '���`�F�b�N
                                '���������s��
                                TextKjSeikyuusyoHakkouDate.Text = String.Empty

                                '����N����
                                If TextKjUriageNengappi.Text = "" Then '��
                                    TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                                End If
                            End If
                        End If

                    End If

                    '���H���񒅓�
                    If Me.TextKjKankouSokuhouTyakuDate.Text = String.Empty Then
                        '6)�H���d�l�m�F
                        If SelectKjKoujiSiyouKakunin.SelectedValue = "" Then '��
                            '���������s��
                            TextKjSeikyuusyoHakkouDate.Text = String.Empty

                            '����N����
                            TextKjUriageNengappi.Text = String.Empty

                        End If

                        '7)�H����А���
                        If Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked Then
                            '���������s��
                            TextKjSeikyuusyoHakkouDate.Text = String.Empty

                            '����N����
                            TextKjUriageNengappi.Text = String.Empty

                        End If
                    End If

                Else '2000�ԑ�ȊO

                    '���H�����i�`�F�b�N
                    If cl.ChkSyouhinCdB2000(Me.SelectKjSyouhinCd.SelectedValue) = False Then
                        '5)���H���񒅓�=������
                        If TextKjKankouSokuhouTyakuDate.Text = String.Empty Then '������
                            TextKjSeikyuusyoHakkouDate.Text = "" '���������s��
                            TextKjUriageNengappi.Text = "" '����N����
                        End If
                    End If

                End If

            Else '������

                SelectKjSeikyuuUmu.SelectedValue = "" '����
                TextKjUriageNengappi.Text = "" '����N����
                TextKjUriageZeinukiKingaku.Text = "" '������z/�Ŕ����z
                TextKjUriageSyouhizei.Text = "" '������z/�����
                TextKjUriageZeikomiKingaku.Text = "" '������z/�ō����z
                TextKjSiireZeinukiKingaku.Text = "" '�d�����z/�Ŕ����z
                TextKjSiireSyouhizei.Text = "" '�d�����z/�����
                TextKjSiireZeikomiKingaku.Text = "" '�d�����z/�ō����z
                TextKjZangaku.Text = "0" '�c�z
                TextKjSeikyuusyoHakkouDate.Text = "" '���������s��
                TextKjHattyuusyoKakutei.Text = "" '�������m��
            End If

        End If

    End Sub

    ''' <summary>
    '''���i���̐����E�d���悪�ύX����Ă��Ȃ������`�F�b�N���A
    '''�ύX����Ă���ꍇ���̍Ď擾
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setSeikyuuSiireHenkou(ByVal sender As Object, ByVal e As System.EventArgs)
        '������A�d���悪�ύX���ꂽ�s���`�F�b�N���A���݂����ꍇ��
        '�e�s�̐����L���ύX���̏��������s����

        '****************
        ' ���ǍH��
        '****************
        '�����d���ύX�����NUC���̃`�F�b�N�t���OHidden���Q�Ƃ��A�t���O�������Ă���ꍇ�͏������{
        If Me.ucSeikyuuSiireLinkKai.AccHiddenChkSeikyuuSakiChg.Value = "1" Then

            '���i�R�[�h�ύX������
            Me.SelectKjSyouhinCd_SelectedIndexChanged(sender, e)

            '�t���O������
            Me.ucSeikyuuSiireLinkKai.AccHiddenChkSeikyuuSakiChg.Value = String.Empty

            '�t�H�[�J�X�͐����d���ύX�����N
            setFocusAJ(Me.ucSeikyuuSiireLinkKai.AccLinkSeikyuuSiireHenkou)

            '�ύX���ꂽ���i���L�����ꍇ�AUpdatePanel��Update
            Me.UpdatePanelKairyouKoujiInfo.Update()

        End If

        '****************
        ' �ǉ��H��
        '****************
        '�����d���ύX�����NUC���̃`�F�b�N�t���OHidden���Q�Ƃ��A�t���O�������Ă���ꍇ�͏������{
        If Me.ucSeikyuuSiireLinkTui.AccHiddenChkSeikyuuSakiChg.Value = "1" Then

            '���i�R�[�h�ύX������
            Me.SelectTjSyouhinCd_SelectedIndexChanged(sender, e)

            '�t���O������
            Me.ucSeikyuuSiireLinkTui.AccHiddenChkSeikyuuSakiChg.Value = String.Empty

            '�t�H�[�J�X�͐����d���ύX�����N
            setFocusAJ(Me.ucSeikyuuSiireLinkTui.AccLinkSeikyuuSiireHenkou)

            '�ύX���ꂽ���i���L�����ꍇ�AUpdatePanel��Update
            Me.UpdatePanelKairyouKoujiInfo.Update()

        End If

        '**************************
        ' ���ǍH���񍐏�
        '**************************
        '�����d���ύX�����NUC���̃`�F�b�N�t���OHidden���Q�Ƃ��A�t���O�������Ă���ꍇ�͏������{
        If Me.ucSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then

            '�����L���ύX������
            Me.SelectKhSeikyuuUmu_SelectedIndexChanged(sender, e)

            '�t���O������
            Me.ucSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty

            '�t�H�[�J�X�͐����d���ύX�����N
            setFocusAJ(Me.ucSeikyuuSiireLink.AccLinkSeikyuuSiireHenkou)

            '�ύX���ꂽ���i���L�����ꍇ�AUpdatePanel��Update
            Me.UpdatePanelKairyouKoujiHoukokusyoInfo.Update()

        End If

    End Sub
End Class