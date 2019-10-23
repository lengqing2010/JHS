
Partial Public Class GyoumuKyoutuuCtrl
    Inherits System.Web.UI.UserControl

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�����o�ϐ�"
    ''' <summary>
    ''' �˗���ʃZ�b�V�������N���X
    ''' </summary>
    ''' <remarks></remarks>
    Dim iraiSession As New IraiSession
    ''' <summary>
    ''' ���[�U�[���
    ''' </summary>
    ''' <remarks></remarks>
    Dim user_info As New LoginUserInfo
    ''' <summary>
    ''' �}�X�^�[�y�[�W��AjaxScriptManager
    ''' </summary>
    ''' <remarks></remarks>
    Dim masterAjaxSM As New ScriptManager
    ''' <summary>
    ''' �n�Չ�ʃZ�b�V�����Ǘ��N���X
    ''' </summary>
    ''' <remarks></remarks>
    Dim jSM As New JibanSessionManager
    ''' <summary>
    ''' �n�Չ�ʋ��ʃN���X
    ''' </summary>
    ''' <remarks></remarks>
    Dim jBn As New Jiban
    ''' <summary>
    ''' ���ʏ����N���X
    ''' </summary>
    ''' <remarks></remarks>
    Dim cl As New CommonLogic
    ''' <summary>
    ''' ���ʃ��W�b�N�N���X
    ''' </summary>
    ''' <remarks></remarks>
    Dim cbLogic As New CommonBizLogic
    ''' <summary>
    ''' ���b�Z�[�W�N���X
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic
#End Region

#Region "�����o�萔"

#Region "��ʕ\�����[�h"
    ''' <summary>
    ''' �R���g���[���̉�ʕ\�����[�h
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DisplayMode
        ''' <summary>
        ''' �񍐏����
        ''' </summary>
        ''' <remarks></remarks>
        HOUKOKUSYO = 1
        ''' <summary>
        ''' ���ǍH�����
        ''' </summary>
        ''' <remarks></remarks>
        KAIRYOU = 2
        ''' <summary>
        ''' �ۏ؉��
        ''' </summary>
        ''' <remarks></remarks>
        HOSYOU = 3
    End Enum
#End Region

#End Region

#Region "�J�X�^���C�x���g�錾"
    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OyaGamenAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal irai1Mode As String, ByVal actMode As String)

    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OyaGamenAction_hensyu(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal blnDisabled As Boolean)

    ''' <summary>
    ''' �o�^�������̐e��ʂ̏������s�p�J�X�^���C�x���g
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OyaActAtAfterExe(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal irai1Mode As String, ByVal actMode As String, ByVal exeMode As String, ByVal result As Boolean)
#End Region

#Region "���[�U�[�R���g���[���ւ̊O������̃A�N�Z�X�pGetter/Setter"

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for HtmlInputHidden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenKubun() As HtmlInputHidden
        Get
            Return HiddenKubun
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenKubun = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextBangou
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccBangou() As HtmlInputText
        Get
            Return TextBangou
        End Get
        Set(ByVal value As HtmlInputText)
            TextBangou = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextKameitenCd
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccKameitenCd() As HtmlInputText
        Get
            Return TextKameitenCd
        End Get
        Set(ByVal value As HtmlInputText)
            TextKameitenCd = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for HiddenKameitenCdTextOld
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccKameitenCdTextOld() As HtmlInputHidden
        Get
            Return HiddenKameitenCdTextOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenKameitenCdTextOld = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for SelectDataHaki
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccDataHakiSyubetu() As DropDownList
        Get
            Return SelectDataHaki
        End Get
        Set(ByVal value As DropDownList)
            SelectDataHaki = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextDataHakiDate
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccDataHakiDate() As HtmlInputText
        Get
            Return TextDataHakiDate
        End Get
        Set(ByVal value As HtmlInputText)
            TextDataHakiDate = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for lastUpdateDateTime
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccLastupdatedatetime() As HtmlInputHidden
        Get
            Return lastUpdateDateTime
        End Get
        Set(ByVal value As HtmlInputHidden)
            lastUpdateDateTime = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for lastUpdateDateTime
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccDateTimeDetail() As HtmlInputHidden
        Get
            Return HiddenUpdDatetime
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenUpdDatetime = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for lastUpdateUserNm
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccLastupdateusernm() As HtmlInputHidden
        Get
            Return lastUpdateUserNm
        End Get
        Set(ByVal value As HtmlInputHidden)
            lastUpdateUserNm = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for �X�V���t[DB�X�V�p]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccupdateDateTime() As HtmlInputHidden
        Get
            Return updateDateTime
        End Get
        Set(ByVal value As HtmlInputHidden)
            updateDateTime = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for ������ЃR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>������ЃR�[�h+���Ə��R�[�h</remarks>
    Public Property AccTyousaKaisyaCd() As HtmlInputHidden
        Get
            Return HiddenTyousaKaisyaCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenTyousaKaisyaCd = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for �H����ЃR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>�H����ЃR�[�h+���Ə��R�[�h</remarks>
    Public Property AccKoujiKaisyaCd() As HtmlInputHidden
        Get
            Return HiddenKoujiKaisyaCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenKoujiKaisyaCd = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TD�j�����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>�j�����</remarks>
    Public Property AccTdDataHakiSyubetu() As HtmlTableCell
        Get
            Return TdDataHakiSyubetu
        End Get
        Set(ByVal value As HtmlTableCell)
            TdDataHakiSyubetu = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TD�j����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>�f�[�^�j����</remarks>
    Public Property AccTdDataHakiData() As HtmlTableCell
        Get
            Return TdDataHakiDate
        End Get
        Set(ByVal value As HtmlTableCell)
            TdDataHakiDate = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for �n��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnKeiretuCd() As HtmlInputHidden
        Get
            Return HiddenKeiretuCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenKeiretuCd = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for �c�Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnEigyousyoCd() As HtmlInputHidden
        Get
            Return HiddenEigyousyoCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenEigyousyoCd = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for �����T�v
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnTyousaGaiyou() As HtmlInputHidden
        Get
            Return HiddenTyousaGaiyou
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenTyousaGaiyou = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for ���ʏ��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccKyoutuuInfo() As HtmlGenericControl
        Get
            Return kyoutuuInfo
        End Get
        Set(ByVal value As HtmlGenericControl)
            kyoutuuInfo = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for ���ʏ��X�^�C��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnKyoutuuInfoStyle() As HtmlInputHidden
        Get
            Return HiddenKyoutuuInfoStyle
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenKyoutuuInfoStyle = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for �{�喼
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSesyuMei() As HtmlInputText
        Get
            Return TextSesyuMei
        End Get
        Set(ByVal value As HtmlInputText)
            TextSesyuMei = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for �����Z��1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccBukkenJyuusyo1() As HtmlInputText
        Get
            Return TextBukkenJyuusyo1
        End Get
        Set(ByVal value As HtmlInputText)
            TextBukkenJyuusyo1 = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for �����Z��2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccBukkenJyuusyo2() As HtmlInputText
        Get
            Return TextBukkenJyuusyo2
        End Get
        Set(ByVal value As HtmlInputText)
            TextBukkenJyuusyo2 = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for �����Z��3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccBukkenJyuusyo3() As HtmlInputText
        Get
            Return TextBukkenJyuusyo3
        End Get
        Set(ByVal value As HtmlInputText)
            TextBukkenJyuusyo3 = value
        End Set
    End Property

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
            Return mode
        End Get
        Set(ByVal value As DisplayMode)
            mode = value
        End Set
    End Property

    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>�B���t�B�[���h���擾</remarks>
    Public Property Kubun() As String
        Get
            Return HiddenKubun.Value
        End Get
        Set(ByVal value As String)
            HiddenKubun.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �ԍ��i���ۏ؏�NO�j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Bangou() As String
        Get
            Return TextBangou.Value
        End Get
        Set(ByVal value As String)
            TextBangou.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �f�[�^�j�����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataHakiSyubetu() As String
        Get
            Return SelectDataHaki.SelectedValue
        End Get
        Set(ByVal value As String)
            SelectDataHaki.SelectedValue = value
        End Set
    End Property

    ''' <summary>
    ''' �f�[�^�j����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataHakiDate() As String
        Get
            Return TextDataHakiDate.Value
        End Get
        Set(ByVal value As String)
            TextDataHakiDate.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �{�喼
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SesyuMei() As String
        Get
            Return TextSesyuMei.Value
        End Get
        Set(ByVal value As String)
            TextSesyuMei.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �����Z���P
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Jyuusyo1() As String
        Get
            Return TextBukkenJyuusyo1.Value
        End Get
        Set(ByVal value As String)
            TextBukkenJyuusyo1.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �����Z���Q
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Jyuusyo2() As String
        Get
            Return TextBukkenJyuusyo2.Value
        End Get
        Set(ByVal value As String)
            TextBukkenJyuusyo2.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �����Z���R
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Jyuusyo3() As String
        Get
            Return TextBukkenJyuusyo3.Value
        End Get
        Set(ByVal value As String)
            TextBukkenJyuusyo3.Value = value
        End Set
    End Property

    ''' <summary>
    ''' ���l
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Bikou() As String
        Get
            Return TextAreaBikou.Value
        End Get
        Set(ByVal value As String)
            TextAreaBikou.Value = value
        End Set
    End Property

    ''' <summary>
    ''' ���l�Q
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Bikou2() As String
        Get
            Return TextAreaBikou2.Value
        End Get
        Set(ByVal value As String)
            TextAreaBikou2.Value = value
        End Set
    End Property

    ''' <summary>
    ''' ������ЃR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TyousaKaishaCd() As String
        Get
            Return HiddenTyousaKaisyaCd1.Value
        End Get
        Set(ByVal value As String)
            HiddenTyousaKaisyaCd1.Value = value
        End Set
    End Property

    ''' <summary>
    ''' ������Ў��Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TyousaKaishaJigyousyoCd() As String
        Get
            Return HiddenTyousaKaisyaCd2.Value
        End Get
        Set(ByVal value As String)
            HiddenTyousaKaisyaCd2.Value = value
        End Set
    End Property

    ''' <summary>
    ''' ������Ж�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TyousaKaishaMei() As String
        Get
            Return HiddenTyousaKaisyaMei.Value
        End Get
        Set(ByVal value As String)
            HiddenTyousaKaisyaMei.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �������@�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TyousaHouhouCd() As String
        Get
            Return HiddenTyousaHouhouCd.Value
        End Get
        Set(ByVal value As String)
            HiddenTyousaHouhouCd.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �������@��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TyousaHouhouMei() As String
        Get
            Return HiddenTyousaHouhouMei.Value
        End Get
        Set(ByVal value As String)
            HiddenTyousaHouhouMei.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �����X��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KameitenMei() As String
        Get
            Return TextKameitenMei.Value
        End Get
        Set(ByVal value As String)
            TextKameitenMei.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �����X�d�b�ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KameitenTel() As String
        Get
            Return TextKameitenTel.Value
        End Get
        Set(ByVal value As String)
            TextKameitenTel.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �����XFAX�ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KameitenFax() As String
        Get
            Return TextKameitenFax.Value
        End Get
        Set(ByVal value As String)
            TextKameitenFax.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �����X���[���A�h���X
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KameitenMail() As String
        Get
            Return HiddenKameitenMail.Value
        End Get
        Set(ByVal value As String)
            HiddenKameitenMail.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �\���R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KouzouCd() As String
        Get
            Return HiddenKouzou.Value
        End Get
        Set(ByVal value As String)
            HiddenKouzou.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �\����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KouzouMei() As String
        Get
            Return HiddenKouzouMei.Value
        End Get
        Set(ByVal value As String)
            HiddenKouzouMei.Value = value
        End Set
    End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '�}�X�^�[�y�[�W�����擾�iScriptManager�p�j
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        iraiSession = Context.Items("irai")

        ' ���[�U�[��{�F��
        jBn.UserAuth(user_info)
        If user_info Is Nothing Then
            '���O�C����񂪖����ꍇ�A���C����ʂɔ�΂�
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If IsPostBack = False Then

            '****************************************************************************
            ' �h���b�v�_�E�����X�g�ݒ�
            '****************************************************************************
            ' �R���{�ݒ�w���p�[�N���X�𐶐�
            Dim helper As New DropDownHelper
            ' �j���R���{�Ƀf�[�^���o�C���h����
            helper.SetDropDownList(SelectDataHaki, DropDownHelper.DropDownType.DataHaki, True)

            '�n�Փǂݍ��݃f�[�^���Z�b�V�����ɑ��݂���ꍇ�A��ʂɕ\��������
            If iraiSession.JibanData IsNot Nothing Then
                Dim jr As JibanRecordBase = iraiSession.JibanData
                SetCtrlFromJibanRec(sender, e, jr)
            End If

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            setDispAction()

            '��ʐ���(�����N��)
            Me.SetEnableControlInit()
        End If

    End Sub

    ''' <summary>
    ''' �n�Ճ��R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s���i�Q�Ə����p�j
    ''' </summary>
    ''' <param name="jr">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)

        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^��1���̂ݎ擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�́A������ʂ�\������
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim blnTorikesi As Boolean = False
        Dim dataArray As New List(Of KameitenSearchRecord)
        Dim JibanLogic As New JibanLogic

        Dim recData As New KameitenSearchRecord

        ' �R���{�ݒ�w���p�[�N���X�𐶐�
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        '��ʃR���g���[���ɐݒ�
        '���_�~�[�R���{�ɃZ�b�g
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Kubun, True, True)
        objDrpTmp.SelectedValue = jr.Kbn
        TextKubun.Value = objDrpTmp.SelectedItem.Text '�敪
        HiddenKubun.Value = jr.Kbn '�B���t�B�[���h

        TextBangou.Value = cl.GetDispStr(jr.HosyousyoNo) '�ԍ�
        SelectDataHaki.SelectedValue = cl.GetDispStr(jr.DataHakiSyubetu) '�f�[�^�j�����
        TextDataHakiDate.Value = cl.GetDispStr(cl.GetDispStr(jr.DataHakiDate)) '�f�[�^�j����
        TextSesyuMei.Value = cl.GetDispStr(jr.SesyuMei) '�{�喼
        TextBukkenJyuusyo1.Value = cl.GetDispStr(jr.BukkenJyuusyo1) '�����Z��1
        TextBukkenJyuusyo2.Value = cl.GetDispStr(jr.BukkenJyuusyo2) '�����Z��2
        TextBukkenJyuusyo3.Value = cl.GetDispStr(jr.BukkenJyuusyo3) '�����Z��3
        TextAreaBikou.Value = cl.GetDispStr(jr.Bikou) '���l
        TextAreaBikou2.Value = cl.GetDispStr(jr.Bikou2) '���l2

        '���_�~�[�R���{�ɃZ�b�g
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Keiyu)
        objDrpTmp.SelectedValue = jr.Keiyu
        TextKeiyu.Value = objDrpTmp.SelectedItem.Text '�o�R

        TextTatemonoKensa.Value = cl.GetDispUmuStr(jr.KasiUmu, False, True) '�������������r�L��

        TextTyousaRenrakusaki.Value = cl.GetDispStr(jr.TysRenrakusakiAtesakiMei) '�����A����E�A����
        TextTyousaTel.Value = cl.GetDispStr(jr.TysRenrakusakiTel) '�����A����ETEL
        TextTyousaFax.Value = cl.GetDispStr(jr.TysRenrakusakiFax) '�����A����EFAX
        TextTyousaMiseTantousya.Value = cl.GetDispStr(jr.IraiTantousyaMei) '�����A����E�X�S����
        TextTyousaMail.Value = cl.GetDispStr(jr.TysRenrakusakiMail) '�����A����EMAIL

        If cl.GetDispStr(jr.Kbn) <> "" And cl.GetDispStr(jr.KameitenCd) <> "" Then

            recData = kameitenSearchLogic.GetKameitenSearchResult(jr.Kbn, _
                                                                  jr.KameitenCd, _
                                                                  jr.TysKaisyaCd & jr.TysKaisyaJigyousyoCd, _
                                                                  blnTorikesi)

            '�����X�����Z�b�g
            Me.SetKameitenInfo(recData)

            '�����X�R�[�h��DB�l��ޔ�
            Me.HiddenKameitenCdTextOld.Value = Me.TextKameitenCd.Value
        End If

        '���_�~�[�R���{�ɃZ�b�g
        objDrpTmp = New DropDownList
        helper.SetMeisyouDropDownList(objDrpTmp, EarthConst.emMeisyouType.SYOUHIN_KBN, False, False)
        objDrpTmp.SelectedValue = jr.SyouhinKbn
        If jr.SyouhinKbn = 9 Then
            TextSyouhinKbn.Value = " " '���i�敪
        Else
            TextSyouhinKbn.Value = objDrpTmp.SelectedItem.Text '���i�敪
        End If


        TextDoujiIraiTousuu.Value = cl.GetDispStr(jr.DoujiIraiTousuu) '�����˗�����

        '���_�~�[�R���{�ɃZ�b�g
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.TatemonoYouto, True, True)
        objDrpTmp.SelectedValue = jr.TatemonoYoutoNo
        TextTatemonoYouto.Value = objDrpTmp.SelectedItem.Text '�����p�r

        TextKosuu.Value = cl.GetDispStr(jr.Kosuu) '�ː�

        '�X�V���t
        updateDateTime.Value = IIf(jr.UpdDatetime = Date.MinValue, "", Format(jr.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
        HiddenUpdDatetime.Value = IIf(jr.UpdDatetime = Date.MinValue, "", jr.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2))

        '��ʕ\���p �ŏI�X�V�ҁA�ŏI�X�V���������X�V�҂���擾�ɕύX
        cl.SetKousinsya(jr.Kousinsya, lastUpdateUserNm.Value, lastUpdateDateTime.Value)

        'Hidden����
        HiddenTyousaKaisyaCd.Value = jr.TysKaisyaCd & jr.TysKaisyaJigyousyoCd '������ЃR�[�h+������Ў��Ə��R�[�h
        HiddenTyousaKaisyaCd1.Value = jr.TysKaisyaCd '������ЃR�[�h
        HiddenTyousaKaisyaCd2.Value = jr.TysKaisyaJigyousyoCd '������Ў��Ə��R�[�h
        HiddenKoujiKaisyaCd.Value = jr.KojGaisyaCd & jr.KojGaisyaJigyousyoCd '�H����ЃR�[�h+�H����Ў��Ə��R�[�h
        If jr.TysKaisyaCd <> "" And jr.TysKaisyaJigyousyoCd <> "" Then
            Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
            HiddenTyousaKaisyaMei.Value = tyousakaisyaSearchLogic.GetTyousaKaisyaMei(jr.TysKaisyaCd, jr.TysKaisyaJigyousyoCd, False) '�������
        End If
        '���_�~�[�R���{�ɃZ�b�g�i�������@�j
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.TyousaHouhou, True, False)
        '�������@��DDL�\������
        cl.ps_SetSelectTextBoxTysHouhou(jr.TysHouhou, objDrpTmp, False)
        HiddenTyousaHouhouCd.Value = objDrpTmp.SelectedValue '�������@�R�[�h
        HiddenTyousaHouhouMei.Value = objDrpTmp.SelectedItem.Text '�������@��

        '���_�~�[�R���{�ɃZ�b�g�i�\���j
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Kouzou, True, False)
        objDrpTmp.SelectedValue = IIf(jr.Kouzou = Integer.MinValue, "", jr.Kouzou)
        HiddenKouzou.Value = objDrpTmp.SelectedValue '�\���R�[�h
        HiddenKouzouMei.Value = objDrpTmp.SelectedItem.Text '�\����

        '�����T�v�R�[�h
        HiddenTyousaGaiyou.Value = IIf(jr.TysGaiyou = Integer.MinValue, "", jr.TysGaiyou)

        '****************************
        '(3) NG���ݒ苤�ʏ���
        '****************************
        '����R�[�h1
        HiddenHanteiCd1.Value = cl.GetDispNum(jr.HanteiCd1, "")
        '����R�[�h2
        HiddenHanteiCd2.Value = cl.GetDispNum(jr.HanteiCd2, "")
        '�H����ЃR�[�h+�H����Ў��Ə��R�[�h
        HiddenKojGaisyaCd.Value = cl.GetDispStr(jr.KojGaisyaCd & jr.KojGaisyaJigyousyoCd)
        '�ǉ��H����ЃR�[�h+�ǉ��H����Ў��Ə��R�[�h
        HiddenTKojKaisyaCd.Value = cl.GetDispStr(jr.TKojKaisyaCd & jr.TKojKaisyaJigyousyoCd)

        'NG���ݒ苤�ʏ���
        SetNGInfo()

        '�Z�b�V�����ɉ�ʏ����i�[
        jSM.Ctrl2Hash(Me, jBn.IraiData)
        iraiSession.Irai1Data = jBn.IraiData

    End Sub

    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        '�^�C�g���o�[�ɋ敪�A�ۏ؏�NO�A�{�喼�A�Z���P��\��
        TitleInfobar.InnerHtml = "�y" & HiddenKubun.Value & "�z �y" & TextBangou.Value & "�z �y" & TextSesyuMei.Value & _
                                        "�z �y" & TextBukkenJyuusyo1.Value & "�z"

        LinkKyoutuuInfo.HRef &= "changeDisplay('" & TitleInfobar.ClientID & "');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ���V�X�e���ւ̃����N�{�^���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '��������
        cl.getTyosaMitumoriFilePath(Kubun, Bangou, ButtonTyosaMitumori)

        '�ۏ؏�DB
        cl.getHosyousyoDbFilePath(Kubun, Bangou, ButtonHosyousyoDB)

        'ReportJHS
        cl.getReportJHSPath(Kubun, Bangou, ButtonRJHS)

        '�����X���ӎ���
        cl.getKameitenTyuuijouhouPath(TextKameitenCd.ClientID, ButtonKameitenTyuuijouhou)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �C�x���g�n���h���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�r���_�[���\���{�^��
        ButtonBuilderCheck.Attributes("onclick") = "callSearch('" & TextKameitenCd.ClientID & "','" & UrlConst.BUILDER_INFO & "','','');"

        '��������\���{�^��
        ButtonBukkenRireki.Attributes("onclick") = "callSearch('" & HiddenKubun.ClientID & EarthConst.SEP_STRING & TextBangou.ClientID & "','" & UrlConst.POPUP_BUKKEN_RIREKI & "','','');"

        '�X�V����\���{�^��
        ButtonKousinRireki.Attributes("onclick") = "callSearch('" & TextKubun.ClientID & EarthConst.SEP_STRING & TextBangou.ClientID & "','" & UrlConst.SEARCH_KOUSIN_RIREKI & "','','');"

        '���ʑΉ�
        cl.getTokubetuTaiouLinkPath(Me.ButtonTokubetuTaiou, _
                                     user_info, _
                                     TextKubun.ClientID, _
                                     TextBangou.ClientID, _
                                     "", _
                                     "", _
                                     "")

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �f�[�^�j����ʂɂ���āA��ʕ\����ؑւ��鏈��
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �f�[�^�j�����͖��w�莞�̂݊������i���̍ۂ̓f�[�^�j���֌W�ȊO�S�Ĕ񊈐��ƂȂ�j
        SelectDataHaki.Attributes("onchange") = "changeHaki(this);"
        CheckHakiDisable()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ �����X�R�[�h
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextKameitenCd.Attributes("onfocus") = "removeFig(this);setTempValueForOnBlur(this);"
        Me.TextKameitenCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callKameitenSearch(this);}else{checkNumber(this);}"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ �e�[�u���̕\���ؑ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '���ʏ��
        LinkKyoutuuInfo.HRef = "JavaScript:changeDisplay('" & kyoutuuInfo.ClientID & "');SetDisplayStyle('" & HiddenKyoutuuInfoStyle.ClientID & "','" & kyoutuuInfo.ClientID & "');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ������R�e�L�X�g�{�b�N�X�̃X�^�C����ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)

    End Sub

#Region "��ʐ���"

    ''' <summary>
    ''' �R���g���[���̏����N�����̉�ʐ���
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetEnableControlInit()
        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable
        jSM.Hash2Ctrl(Me.TdKameiten, EarthConst.MODE_VIEW, ht)

        If Me.DispMode <> DisplayMode.HOSYOU Then
            Me.ButtonKameitenSearch.Style("display") = "none"
        End If
    End Sub

    ''' <summary>
    ''' �����X�R�[�h�̉�ʐ���
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetEnableKameiten(ByVal strBukkenJyky As String)

        '�����󋵂��u1:�ۏ؏��˗��v�������́u3:�ς݁v�̎�
        If Me.DispMode = DisplayMode.HOSYOU Then
            If strBukkenJyky = "1" OrElse strBukkenJyky = "3" Then
                cl.chgDispSyouhinText(Me.TextKameitenCd) '�����X�R�[�h
                Me.ButtonKameitenSearch.Style("display") = "inline"
            Else
                Me.SetEnableControlInit()
                Me.ButtonKameitenSearch.Style("display") = "none"
            End If

            '�Ɩ�����Ctrl���ŐV��
            If IsPostBack = True Then
                Me.irai1MainUpdatePanel.Update()
            End If
        End If
    End Sub
#End Region

    ''' <summary>
    ''' �����X���̃N���A���s�Ȃ�
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearKameitenInfo(Optional ByVal blnFlg As Boolean = True)

        '����������
        If blnFlg Then
            Me.TextKameitenCd.Value = String.Empty
        End If
        '�����X������R
        Me.TextTorikesiRiyuu.Text = cl.getTorikesiRiyuu(0, String.Empty)
        Me.TextKameitenMei.Value = String.Empty
        Me.TextKameitenTel.Value = String.Empty
        Me.TextKameitenFax.Value = String.Empty
        Me.HiddenKameitenMail.Value = String.Empty
        Me.TextBuilderNo.Value = String.Empty
        Me.HiddenKeiretuCd.Value = String.Empty
        Me.TextKeiretu.Value = String.Empty
        Me.HiddenEigyousyoCd.Value = String.Empty
        Me.TextEigyousyoMei.Value = String.Empty
        Me.TextMitsumoriHituyou.Value = String.Empty
        Me.TextHattyuusyoHituyou.Value = String.Empty
        Me.TextTyousaKaisyaNG.Value = String.Empty
        Me.TextJioSakiFlg.Value = String.Empty
        Me.TextFcTenMei.Value = String.Empty

        '�����X�R�[�h/����/������R�̕����F�X�^�C��
        cl.setStyleFontColor(Me.TextKameitenCd.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextKameitenMei.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextTorikesiRiyuu.Style, cl.getKameitenFontColor(0))

        '�t�H�[�J�X�̐ݒ�
        Me.setFocusAJ(Me.ButtonKameitenSearch)
    End Sub

    ''' <summary>
    ''' �����X�����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKameitenSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKameitenSearch.ServerClick

        If kameitenSearchType.Value <> "1" Then
            kameitenSearchSub(sender, e)
        Else
            '�R�[�honchange�ŌĂ΂ꂽ�ꍇ
            kameitenSearchSub(sender, e, False)
            kameitenSearchType.Value = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' �����X�����{�^���������̏���(����)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub kameitenSearchSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^��1���̂ݎ擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim kLogic As New KameitenSearchLogic
        Dim total_row As Integer = 0
        Dim dataArray As New List(Of KameitenSearchRecord)
        Dim blnTorikesi As Boolean  '����Ώۃt���O

        '�����X�R�[�h=���͂̏ꍇ
        If Me.TextKameitenCd.Value <> String.Empty Then

            'DB�l�Ɠ����ꍇ�A����=0��������Ɋ܂߂Ȃ�
            If Me.TextKameitenCd.Value = Me.HiddenKameitenCdTextOld.Value Then
                blnTorikesi = False
            Else
                blnTorikesi = True
            End If

            '���������s
            dataArray = kLogic.GetKameitenSearchResult(Me.HiddenKubun.Value _
                                                        , Me.TextKameitenCd.Value _
                                                        , blnTorikesi _
                                                        , total_row)
        End If

        If total_row = 1 Then
            '���i������ʂɃZ�b�g
            Dim recData As KameitenSearchRecord = dataArray(0)
            '�����X�R�[�h����꒼��
            Me.TextKameitenCd.Value = dataArray(0).KameitenCd

            '�t�H�[�J�X�Z�b�g
            setFocusAJ(Me.ButtonKameitenSearch)
        Else
            If callWindow = True Then
                '�����X��
                Me.TextKameitenMei.Value = String.Empty

                '������ʕ\���pJavaScript�wcallSearch�x�����s
                Dim tmpFocusScript = "objEBI('" & ButtonKameitenSearch.ClientID & "').focus();"
                Dim tmpScript As String = "callSearch('" & Me.HiddenKubun.ClientID & EarthConst.SEP_STRING & Me.TextKameitenCd.ClientID & _
                                                "','" & UrlConst.SEARCH_KAMEITEN & _
                                                "','" & Me.TextKameitenCd.ClientID & EarthConst.SEP_STRING & Me.TextKameitenMei.ClientID & _
                                                "','" & Me.ButtonKameitenSearch.ClientID & "');"


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
        Dim recData As KameitenSearchRecord = kLogic.GetKameitenSearchResult(Me.HiddenKubun.Value, Me.TextKameitenCd.Value, "", blnTorikesi)
        Dim strErrMsg As String = String.Empty

        If Me.TextKameitenCd.Value <> String.Empty Then    '����
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

        'NG���ݒ苤�ʏ���
        SetNGInfo()

    End Sub

    ''' <summary>
    ''' �����X�����Z�b�g����
    ''' </summary>
    ''' <param name="recData"></param>
    ''' <remarks></remarks>
    Private Sub SetKameitenInfo(ByVal recData As KameitenSearchRecord)

        If Not recData Is Nothing AndAlso recData.KameitenCd <> String.Empty Then
            '��ʂɒl���Z�b�g
            TextKameitenCd.Value = cl.GetDispStr(recData.KameitenCd) '�����X�E�R�[�h
            TextKameitenMei.Value = cl.GetDispStr(recData.KameitenMei1) '�����X�E����
            Me.TextTorikesiRiyuu.Text = cl.getTorikesiRiyuu(recData.Torikesi, recData.KtTorikesiRiyuu) '�����X�E������R
            TextKameitenTel.Value = cl.GetDispStr(recData.TelNo) '�����X�ETEL
            TextKameitenFax.Value = cl.GetDispStr(recData.FaxNo) '�����X�EFAX
            HiddenKameitenMail.Value = cl.GetDispStr(recData.MailAddress) '�����X�EMail
            TextBuilderNo.Value = cl.GetDispStr(recData.BuilderNo) '�����X�E�r���_�[NO
            HiddenKeiretuCd.Value = cl.GetDispStr(recData.KeiretuCd) '�����X�E�n��R�[�h
            TextKeiretu.Value = cl.GetDispStr(recData.KeiretuMei) '�����X�E�n��
            HiddenEigyousyoCd.Value = cl.GetDispStr(recData.EigyousyoCd) '�����X�E�c�Ə��R�[�h
            TextEigyousyoMei.Value = cl.GetDispStr(recData.EigyousyoMei) '�����X�E�c�Ə�/�@�l��
            TextMitsumoriHituyou.Value = cl.GetDispStr(recData.TysMitsyoMsg) '�����X�E���Ϗ��K�{���b�Z�[�W
            TextHattyuusyoHituyou.Value = cl.GetDispStr(recData.HattyuusyoMsg) '�����X�E�������K�{���b�Z�[�W
            TextTyousaKaisyaNG.Value = cl.GetDispKahiStr(recData.KahiKbn) '�����X�ENG��񃁃b�Z�[�W
            TextJioSakiFlg.Value = cl.GetDisplayString(IIf(recData.JioSakiFLG = 1, EarthConst.JIO_SAKI, String.Empty)) 'JIO���t���O
            Me.TextFcTenMei.Value = cl.GetDispStr(recData.FcTenMei) 'FC�X��

            '�����X�R�[�h��ޔ�
            Me.HiddenKameitenCdTextMae.Value = TextKameitenCd.Value

            '�����X�R�[�h/����/������R�̕����F�X�^�C��
            cl.setStyleFontColor(Me.TextKameitenCd.Style, cl.getKameitenFontColor(recData.Torikesi))
            cl.setStyleFontColor(Me.TextKameitenMei.Style, cl.getKameitenFontColor(recData.Torikesi))
            cl.setStyleFontColor(Me.TextTorikesiRiyuu.Style, cl.getKameitenFontColor(recData.Torikesi))

        Else
            '�N���A���s�Ȃ�
            ClearKameitenInfo(False)

        End If

    End Sub

    ''' <summary>
    ''' �j����ʂɂ���āA�R���g���[���̗L��������ؑւ���
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckHakiDisable()
        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban

        '�L�����A�������̑ΏۊO�ɂ���R���g���[���S
        Dim noTarget As New Hashtable
        noTarget.Add(ButtonTyosaMitumori.ID, True) '�������σ{�^��
        noTarget.Add(ButtonHosyousyoDB.ID, True) '�ۏ؏�DB�{�^��
        noTarget.Add(ButtonRJHS.ID, True) 'R-JHS�{�^��
        noTarget.Add(SelectDataHaki.ID, True) '�f�[�^�j�����
        noTarget.Add(changeHaki.ID, True) '�f�[�^�j����ʗp�C�x���g�_�~�[�{�^��
        noTarget.Add(ButtonKameitenTyuuijouhou.ID, True) '���ӏ��{�^��
        noTarget.Add(ButtonBuilderCheck.ID, True) '�r���_�[���{�^��
        noTarget.Add(ButtonBukkenRireki.ID, True) '���������{�^��

        If SelectDataHaki.SelectedValue >= "1" Then
            noTarget.Add(TextDataHakiDate.ID, TextDataHakiDate) '�f�[�^�j����

            '�S�ẴR���g���[���𖳌���()
            jBn.ChangeDesabledAll(Me, True, noTarget)
            '�f�[�^�j�����̕\����ؑւ���
            TextDataHakiDate.Style.Remove("display")
        Else
            '�S�ẴR���g���[����L����()
            jBn.ChangeDesabledAll(Me, False, noTarget)
            '�f�[�^�j�����̕\����ؑւ���
            TextDataHakiDate.Style("display") = "none"
        End If

        setFocusAJ(SelectDataHaki) '�t�H�[�J�X

    End Sub

    ''' <summary>
    ''' �f�[�^�j���R���{�ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub changeHaki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TextDataHakiDate.Value = String.Empty Then
            TextDataHakiDate.Value = Format(Today, "yyyy/MM/dd")
        End If
        '��ʕ\����Ԃ��Đݒ�
        setDispAction()

        If SelectDataHaki.SelectedValue >= "1" Then
            RaiseEvent OyaGamenAction_hensyu(sender, e, True)
        Else
            RaiseEvent OyaGamenAction_hensyu(sender, e, False)
        End If

    End Sub

    ''' <summary>
    ''' ���͍��ڃ`�F�b�N
    ''' </summary>
    ''' <remarks></remarks>
    Public Function checkInput(Optional ByVal flgNextGamen As Boolean = False) As Boolean
        Dim e As New System.EventArgs

        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban

        '�S�ẴR���g���[����L����(�j����ʃ`�F�b�N�p)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        '�G���[���b�Z�[�W������
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New ArrayList

        If SelectDataHaki.SelectedValue >= "1" Then
            '�j����ʂ��I������Ă���ꍇ�A�X���[

        Else

            '�R�[�h�l�`�F�b�N
            '�ύX�㌟���{�^�������`�F�b�N
            If (Me.TextKameitenCd.Value <> Me.HiddenKameitenCdTextMae.Value) Or _
                (Me.TextKameitenCd.Value <> String.Empty And Me.TextKameitenMei.Value = String.Empty) Then
                errMess += Messages.MSG030E.Replace("@PARAM1", "�����X�R�[�h")
                arrFocusTargetCtrl.Add(TextKameitenCd)
            End If

            '���K�{�`�F�b�N
            '<���ʏ��>
            '�{�喼
            If TextSesyuMei.Value.Length = 0 Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "�{�喼")
                arrFocusTargetCtrl.Add(TextSesyuMei)
            End If
            '�����Z��1
            If TextBukkenJyuusyo1.Value.Length = 0 Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "�����Z��1")
                arrFocusTargetCtrl.Add(TextBukkenJyuusyo1)
            End If
            '�����X
            If String.IsNullOrEmpty(Me.TextKameitenCd.Value) Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "�����X�R�[�h")
                arrFocusTargetCtrl.Add(TextKameitenCd)
            End If

        End If

        '���֑������`�F�b�N(��������̓t�B�[���h���Ώ�)
        '�󗝏ڍ�
        If jBn.KinsiStrCheck(TextSesyuMei.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�{�喼")
            arrFocusTargetCtrl.Add(TextSesyuMei)
        End If
        '�����Z��1
        If jBn.KinsiStrCheck(TextBukkenJyuusyo1.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�����Z��1")
            arrFocusTargetCtrl.Add(TextBukkenJyuusyo1)
        End If
        '�����Z��2
        If jBn.KinsiStrCheck(TextBukkenJyuusyo2.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�����Z��2")
            arrFocusTargetCtrl.Add(TextBukkenJyuusyo2)
        End If
        '�����Z��3
        If jBn.KinsiStrCheck(TextBukkenJyuusyo3.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "�����Z��3")
            arrFocusTargetCtrl.Add(TextBukkenJyuusyo3)
        End If
        '���l
        If jBn.KinsiStrCheck(TextAreaBikou.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "���l")
            arrFocusTargetCtrl.Add(TextAreaBikou)
        End If
        '���l2
        If jBn.KinsiStrCheck(TextAreaBikou2.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "���l2")
            arrFocusTargetCtrl.Add(TextAreaBikou2)
        End If

        '���s�ϊ�(���l)
        If TextAreaBikou.Value <> "" Then
            TextAreaBikou.Value = TextAreaBikou.Value.Replace(vbCrLf, " ")
        End If
        If TextAreaBikou2.Value <> "" Then
            TextAreaBikou2.Value = TextAreaBikou2.Value.Replace(vbCrLf, " ")
        End If

        '���o�C�g���`�F�b�N(��������̓t�B�[���h���Ώ�)
        '�{�喼
        If jBn.ByteCheckSJIS(TextSesyuMei.Value, TextSesyuMei.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�{�喼")
            arrFocusTargetCtrl.Add(TextSesyuMei)
        End If
        '�����Z��1
        If jBn.ByteCheckSJIS(TextBukkenJyuusyo1.Value, TextBukkenJyuusyo1.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�����Z��1")
            arrFocusTargetCtrl.Add(TextBukkenJyuusyo1)
        End If
        '�����Z��2
        If jBn.ByteCheckSJIS(TextBukkenJyuusyo2.Value, TextBukkenJyuusyo2.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�����Z��2")
            arrFocusTargetCtrl.Add(TextBukkenJyuusyo2)
        End If
        '�����Z��3
        If jBn.ByteCheckSJIS(TextBukkenJyuusyo3.Value, TextBukkenJyuusyo3.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "�����Z��3")
            arrFocusTargetCtrl.Add(TextBukkenJyuusyo3)
        End If
        '���l
        If jBn.ByteCheckSJIS(TextAreaBikou.Value, 256) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "���l")
            arrFocusTargetCtrl.Add(TextAreaBikou)
        End If
        '���l2
        If jBn.ByteCheckSJIS(TextAreaBikou2.Value, 512) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "���l2")
            arrFocusTargetCtrl.Add(TextAreaBikou2)
        End If

        '�������`�F�b�N
        '�ΏۂȂ�

        '�����t�`�F�b�N
        '�ΏۂȂ�

        '�����̑��`�F�b�N
        '�ΏۂȂ�

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

#Region "(3) NG���ݒ苤�ʏ���"
    ''' <summary>
    ''' NG���ݒ苤�ʏ���
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetNGInfo()
        '�������NG
        SetTyousaKaisyaNG()
        '����NG
        SetHanteiNG(HiddenHanteiCd1.Value, HiddenHanteiCd2.Value)
        '�H�����NG
        SetKoujiKaisyaNG(HiddenKojGaisyaCd.Value, HiddenTKojKaisyaCd.Value)

    End Sub

    ''' <summary>
    ''' �������NG���(�\���ؑւ̂�)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetTyousaKaisyaNG()
        ' �������NG�ݒ�
        If TextTyousaKaisyaNG.Value <> "" Then
            '�\��
            TextTyousaKaisyaNG.Style.Remove("display")
        Else
            '��\��
            TextTyousaKaisyaNG.Style("display") = "none"
        End If
    End Sub

    ''' <summary>
    ''' ����NG���
    ''' </summary>
    ''' <param name="strHanteiCd1">����R�[�h1</param>
    ''' <param name="strHanteiCd2">����R�[�h2</param>
    ''' <remarks></remarks>
    Public Sub SetHanteiNG(ByVal strHanteiCd1 As String, ByVal strHanteiCd2 As String)

        If strHanteiCd1 = "" And strHanteiCd2 = "" Then
            TextHanteiNG.Value = ""
            '��\��
            TextHanteiNG.Style("display") = "none"
            Exit Sub
        End If

        Dim kisosiyouSearchLogic As New KisoSiyouLogic
        Dim blnTorikesi As Boolean = False
        Dim dataArray1 As New List(Of KisoSiyouRecord)
        Dim dataArray2 As New List(Of KisoSiyouRecord)
        Dim intCount1 As Integer = 0
        Dim intCount2 As Integer = 0
        Dim intFlg As Integer = 0

        '1.��ʂ̐ݒ�
        '����1
        If strHanteiCd1 <> "" Then
            dataArray1 = kisosiyouSearchLogic.GetKisoSiyouSearchResult( _
                                    strHanteiCd1 _
                                    , "" _
                                    , TextKameitenCd.Value _
                                    , True _
                                    , blnTorikesi _
                                    , intCount1 _
                                    )
        End If
        If intCount1 = 1 Then
            Dim recData1 As KisoSiyouRecord = dataArray1(0)
            ' �������NG�ݒ�
            If recData1.KahiKbn = 9 Then
                TextHanteiNG.Value = EarthConst.HANTEI_NG
                '�\��
                TextHanteiNG.Style.Remove("display")

                intFlg = 1
            Else
                TextHanteiNG.Value = ""
                '��\��
                TextHanteiNG.Style("display") = "none"
            End If
        End If

        '����2
        If strHanteiCd2 <> "" Then
            dataArray2 = kisosiyouSearchLogic.GetKisoSiyouSearchResult( _
                                    strHanteiCd2 _
                                    , "" _
                                    , TextKameitenCd.Value _
                                    , True _
                                    , blnTorikesi _
                                    , intCount2 _
                                    )
        End If

        If intCount2 = 1 Then
            Dim recData2 As KisoSiyouRecord = dataArray2(0)
            ' �������NG�ݒ�
            If recData2.KahiKbn = 9 Then
                TextHanteiNG.Value = EarthConst.HANTEI_NG
                '�\��
                TextHanteiNG.Style.Remove("display")
            ElseIf intFlg = 1 Then
            Else
                TextHanteiNG.Value = ""
                '��\��
                TextHanteiNG.Style("display") = "none"
            End If
        End If
    End Sub

    ''' <summary>
    ''' �H�����NG���
    ''' </summary>
    ''' <param name="strKoujiKaisyaCd">�H����ЃR�[�h</param>
    ''' <param name="strTuikaKoujiKaisyaCd">�ǉ��H����ЃR�[�h</param>
    ''' <remarks>
    ''' �H�����(������к���,���Ə�����)
    ''' �ǉ��H�����(������к���,���Ə�����)
    ''' </remarks>
    Public Sub SetKoujiKaisyaNG(ByVal strKoujiKaisyaCd As String, ByVal strTuikaKoujiKaisyaCd As String)
        If strKoujiKaisyaCd = "" Then
            TextKoujiKaisyaNG.Value = ""
            '��\��
            TextKoujiKaisyaNG.Style("display") = "none"
            Exit Sub
        End If

        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim blnTorikesi As Boolean = False
        Dim recData1 As New KameitenSearchRecord
        Dim recData2 As New KameitenSearchRecord
        Dim intFlg As Integer = 0

        '***********
        '* ���ǍH��
        '***********
        recData1 = kameitenSearchLogic.GetKoujiKaisyaNGResult( _
                                    HiddenKubun.Value _
                                    , TextKameitenCd.Value _
                                    , strKoujiKaisyaCd _
                                    , True _
                                    )

        If Not recData1 Is Nothing Then

            ' �������NG�ݒ�
            If recData1.KahiKbn = 9 Then
                TextKoujiKaisyaNG.Value = EarthConst.KOUJI_KAISYA_NG
                '�\��
                TextKoujiKaisyaNG.Style.Remove("display")

                intFlg = 1
            Else
                TextKoujiKaisyaNG.Value = ""
                '��\��
                TextKoujiKaisyaNG.Style("display") = "none"
            End If
        Else
            TextKoujiKaisyaNG.Value = ""
        End If


        '***********
        '* �ǉ��H��
        '***********
        recData2 = kameitenSearchLogic.GetKoujiKaisyaNGResult( _
                                    HiddenKubun.Value _
                                    , TextKameitenCd.Value _
                                    , strTuikaKoujiKaisyaCd _
                                    , True _
                                    )

        If Not recData2 Is Nothing Then

            ' �������NG�ݒ�
            If recData2.KahiKbn = 9 Then
                TextKoujiKaisyaNG.Value = EarthConst.KOUJI_KAISYA_NG
                '�\��
                TextKoujiKaisyaNG.Style.Remove("display")

            ElseIf intFlg = 1 Then
            Else
                TextKoujiKaisyaNG.Value = ""
                '��\��
                TextKoujiKaisyaNG.Style("display") = "none"
            End If
        Else
            TextKoujiKaisyaNG.Value = ""
        End If

    End Sub

#End Region

    ''' <summary>
    ''' Ajax���쎞�̃t�H�[�J�X�Z�b�g
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub setFocusAJ(ByVal ctrl As Control)
        '�t�H�[�J�X�Z�b�g
        masterAjaxSM.SetFocus(ctrl)
    End Sub

End Class