
Partial Public Class TokubetuTaiouRecordCtrl
    Inherits System.Web.UI.UserControl

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim iraiSession As New IraiSession
    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    Dim cbLogic As New CommonBizLogic
    Dim MLogic As New MessageLogic

#Region "�R���g���[���l"
    Private Const KEIJYOU_ZUMI As String = "��"
#End Region

#Region "���[�U�[�R���g���[���ւ̊O������̃A�N�Z�X�pGetter/Setter"

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for CheckBox���ʑΉ�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccCheckBoxTokubetuTaiou() As HtmlInputCheckBox
        Get
            Return Me.CheckBoxTokubetuTaiou
        End Get
        Set(ByVal value As HtmlInputCheckBox)
            Me.CheckBoxTokubetuTaiou = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for Hidden���ʑΉ��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnTokubetuTaiouCd() As HtmlInputHidden
        Get
            Return Me.HiddenTokubetuTaiouCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenTokubetuTaiouCd = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for Text���ʑΉ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSpanTokubetuTaiouMei() As HtmlGenericControl
        Get
            Return Me.SpanTokubetuTaiouMei
        End Get
        Set(ByVal value As HtmlGenericControl)
            Me.SpanTokubetuTaiouMei = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for Text���z���Z���i�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextKasanSyouhinCd() As HtmlInputText
        Get
            Return Me.TextKasanSyouhinCd
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextKasanSyouhinCd = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for Text���z���Z���i��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextKasanSyouhinMei() As HtmlInputText
        Get
            Return Me.TextKasanSyouhinMei
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextKasanSyouhinMei = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for Hidden���ރR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnBunruiCd() As HtmlInputHidden
        Get
            Return Me.HiddenBunruiCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenBunruiCd = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for Hidden��ʕ\��NO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnGamenHyoujiNo() As HtmlInputHidden
        Get
            Return Me.HiddenGamenHyoujiNo
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenGamenHyoujiNo = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for Text�ݒ��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSetteiSaki() As HtmlInputText
        Get
            Return Me.TextSetteiSaki
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextSetteiSaki = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for Hidden�ݒ��Style
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnHiddenSetteiSakiStyle() As HtmlInputHidden
        Get
            Return Me.HiddenSetteiSakiStyle
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenSetteiSakiStyle = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for Text����v��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextUriKeijyou() As HtmlInputText
        Get
            Return Me.TextUriKeijyou
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextUriKeijyou = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for Hidden����v��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnUriKeijyou() As HtmlInputHidden
        Get
            Return Me.HiddenUriKeijyou
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenUriKeijyou = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for Hidden���i�����t���O
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnKkkSyoriFlg() As HtmlInputHidden
        Get
            Return Me.HiddenKkkSyoriFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenKkkSyoriFlg = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for Hidden���������z
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnHattyuuKingaku() As HtmlInputHidden
        Get
            Return Me.HiddenHattyuuKingaku
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenHattyuuKingaku = value
        End Set
    End Property

#End Region

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '���ʑΉ���ʏ����擾�iScriptManager�p�j
        Dim myMaster As PopupTokubetuTaiou = Page.Page
        masterAjaxSM = myMaster.AjaxScriptManager

        '�`�F�b�N�{�b�N�X�̃`�F�b�N�C�x���g���A�_�~�[�{�^������������
        Dim strScript_UriKeijou As String = String.Empty
        Dim strScript_CheckAction As String = String.Empty

        '���i�����t���O�������Ă��炸�A����v��ςł��A�`�F�b�N��Ԃ���������ꍇ�A�m�FMSG�\����Ƀ`�F�b�N�C�x���g���N������
        strScript_UriKeijou = "if(objEBI('" & Me.AccHdnKkkSyoriFlg.ClientID & "').value != '1' && objEBI('" & Me.AccHdnUriKeijyou.ClientID & "').value == '1' && objEBI('" & Me.CheckBoxTokubetuTaiou.ClientID & "').checked == false){if(confirm('" & Messages.MSG199C & "')){objEBI('" & Me.AccHdnKkkSyoriFlg.ClientID & "').value = 1;}else{objEBI('" & Me.CheckBoxTokubetuTaiou.ClientID & "').checked = true;return false;}}"
        '���̏ꍇ�A�ݒ��̕ύX�C�x���g�͋N���s�v
        strScript_CheckAction = "if(objEBI('" & Me.AccHdnHiddenSetteiSakiStyle.ClientID & "').value != 'blue') objEBI('" & Me.ButtonChkTokubetuTaiou.ClientID & "').click();"

        Me.CheckBoxTokubetuTaiou.Attributes("onclick") = strScript_UriKeijou & strScript_CheckAction

    End Sub

#Region "�C�x���g"
    ''' <summary>
    ''' �e��ʂ̓��ʑΉ��`�F�b�N�{�b�N�X�`�F�b�N�������s�p�J�X�^���C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">�`�F�b�N�{�b�N�XID</param>
    ''' <remarks></remarks>
    Public Event SetCheckTokubetuTaiouChangeAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)

    ''' <summary>
    ''' �e��ʂ̓��ʑΉ��`�F�b�N�{�b�N�X�`�F�b�N�����������s�p�J�X�^���C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">�`�F�b�N�{�b�N�XID</param>
    ''' <remarks></remarks>
    Public Event SetCheckOffTokubetuTaiouChangeAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)

#End Region

#Region "�v���C�x�[�g���\�b�h"

    ''' <summary>
    ''' ���ʑΉ����R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s�Ȃ��B
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="dtRec">���ʑΉ����R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal dtRec As TokubetuTaiouRecordBase _
                                    )

        Dim strTokubetuTaiouCd As String = String.Empty '���ʑΉ��R�[�h
        Dim strSpace As String = EarthConst.HANKAKU_SPACE

        '��ʕ\���p
        Dim strKasanSyouhinCdDisp As String = String.Empty
        Dim strSyouhinMeiDisp As String = String.Empty
        Dim intUriKasanGakuDisp As Integer = Integer.MinValue
        Dim intKoumutenKasanGakuDisp As Integer = Integer.MinValue

        '���ʑΉ��f�[�^
        Dim blnChecked As Boolean = False
        Dim strKasanSyouhinCdNew As String = String.Empty
        Dim strSyouhinMeiNew As String = String.Empty
        Dim intUriKasanGakuNew As Integer = Integer.MinValue
        Dim intKoumutenKasanGakuNew As Integer = Integer.MinValue

        '�����X���ʑΉ��}�X�^
        Dim blnCheckedMst As Boolean = False
        Dim strKasanSyouhinCdMst As String = String.Empty
        Dim strSyouhinMeiMst As String = String.Empty
        Dim intUriKasanGakuMst As Integer = Integer.MinValue
        Dim intKoumutenKasanGakuMst As Integer = Integer.MinValue

        '���ʑΉ��f�[�^/Old
        Dim strKasanSyouhinCdOld As String = String.Empty
        Dim strSyouhinMeiOld As String = String.Empty
        Dim intUriKasanGakuOld As Integer = Integer.MinValue
        Dim intKoumutenKasanGakuOld As Integer = Integer.MinValue

        '******************************************
        '* ��ʃR���g���[���ɐݒ�
        '******************************************
        '���ʑΉ��R�[�h
        strTokubetuTaiouCd = cl.GetDisplayString(dtRec.mTokubetuTaiouCd)
        Me.HiddenTokubetuTaiouCd.Value = strTokubetuTaiouCd
        '���ʑΉ�����(���̂̂�)
        Me.HiddenTokubetuTaiouMei.Value = cl.GetDisplayString(dtRec.TokubetuTaiouMeisyou)
        Me.SpanTokubetuTaiouMei.InnerHtml = Me.HiddenTokubetuTaiouMei.Value
        '���i�����t���O
        Me.HiddenKkkSyoriFlg.Value = IIf(cl.GetDispNum(dtRec.KkkSyoriFlg) = EarthConst.ARI_VAL, EarthConst.ARI_VAL, EarthConst.NASI_VAL)
        '�X�V�t���O
        Me.HiddenUpdFlg.Value = IIf(cl.GetDispNum(dtRec.UpdFlg) = EarthConst.ARI_VAL, EarthConst.ARI_VAL, EarthConst.NASI_VAL)
        '���ރR�[�h
        Me.HiddenBunruiCd.Value = cl.GetDisplayString(dtRec.BunruiCd)
        '��ʕ\��NO
        Me.HiddenGamenHyoujiNo.Value = cl.GetDispNum(dtRec.GamenHyoujiNo)

        '�������X���ʑΉ��}�X�^
        blnCheckedMst = dtRec.kHanteiCheck
        strKasanSyouhinCdMst = cl.GetDisplayString(dtRec.kKasanSyouhinCd)
        strSyouhinMeiMst = cl.GetDisplayString(dtRec.kKasanSyouhinMei)
        intUriKasanGakuMst = cl.GetDispNum(dtRec.kUriKasanGaku)
        intKoumutenKasanGakuMst = cl.GetDispNum(dtRec.kKoumutenKasanGaku)

        '�����ʑΉ��f�[�^/�V
        blnChecked = dtRec.HanteiCheck
        strKasanSyouhinCdNew = cl.GetDisplayString(dtRec.KasanSyouhinCd)
        strSyouhinMeiNew = cl.GetDisplayString(dtRec.kasanSyouhinMei)
        intUriKasanGakuNew = cl.GetDispNum(dtRec.UriKasanGaku)
        intKoumutenKasanGakuNew = cl.GetDispNum(dtRec.KoumutenKasanGaku)

        '�����ʑΉ��f�[�^/��
        strKasanSyouhinCdOld = cl.GetDisplayString(dtRec.KasanSyouhinCdOld)
        strSyouhinMeiOld = cl.GetDisplayString(dtRec.kasanSyouhinMeiOld)
        intUriKasanGakuOld = cl.GetDispNum(dtRec.UriKasanGakuOld)
        intKoumutenKasanGakuOld = cl.GetDispNum(dtRec.KoumutenKasanGakuOld)

        '��ʕ\��(�D�捀�ڂ𔻒�)�F�V�A���ō��ق�����ꍇ�A�V�ŕ\��
        strKasanSyouhinCdDisp = IIf(strKasanSyouhinCdOld <> strKasanSyouhinCdNew, strKasanSyouhinCdNew, strKasanSyouhinCdOld)
        strSyouhinMeiDisp = IIf(strSyouhinMeiOld <> strSyouhinMeiNew, strSyouhinMeiNew, strSyouhinMeiOld)
        intUriKasanGakuDisp = IIf(intUriKasanGakuOld <> intUriKasanGakuNew, intUriKasanGakuNew, intUriKasanGakuOld)
        intKoumutenKasanGakuDisp = IIf(intKoumutenKasanGakuOld <> intKoumutenKasanGakuNew, intKoumutenKasanGakuNew, intKoumutenKasanGakuOld)

        '����ʍ��ڂɃZ�b�g
        '���ʑΉ��`�F�b�N�L��
        Me.CheckBoxTokubetuTaiou.Checked = blnChecked

        '���=0�̏ꍇ���邢�́A���<>0�ł����i�����t���O=1�̏ꍇ�A�g�����l��\��
        If dtRec.PrimaryDisplay Then
            '���z���Z���i�R�[�h
            Me.TextKasanSyouhinCd.Value = strKasanSyouhinCdDisp
            '���i��
            Me.TextKasanSyouhinMei.Value = strSyouhinMeiDisp
            '�H���X�������Z���z
            Me.TextKoumutenSeikyuKasanKingaku.Value = Format(intKoumutenKasanGakuDisp, EarthConst.FORMAT_KINGAKU_1)
            '���������Z���z
            Me.TextJituSeikyuKasanKingaku.Value = Format(intUriKasanGakuDisp, EarthConst.FORMAT_KINGAKU_1)

        Else '�}�X�^�l��\��
            '���z���Z���i�R�[�h
            Me.TextKasanSyouhinCd.Value = strKasanSyouhinCdMst
            '���i��
            Me.TextKasanSyouhinMei.Value = strSyouhinMeiMst
            '�H���X�������Z���z
            Me.TextKoumutenSeikyuKasanKingaku.Value = Format(intKoumutenKasanGakuMst, EarthConst.FORMAT_KINGAKU_1)
            '���������Z���z
            Me.TextJituSeikyuKasanKingaku.Value = Format(intUriKasanGakuMst, EarthConst.FORMAT_KINGAKU_1)

        End If

        '���i�����t���O
        If Me.HiddenKkkSyoriFlg.Value = EarthConst.ARI_VAL Then
            Me.TextKkkSyoriFlg.Value = KEIJYOU_ZUMI
        Else
            Me.TextKkkSyoriFlg.Value = String.Empty
        End If

        '****************************
        '* Hidden����
        '****************************
        '�������Ǎ����̒l��ޔ�
        If IsPostBack = False Then
            '���ʑΉ��`�F�b�N�L��(�ύX�`�F�b�N�p�ɑޔ�)
            Me.HiddenChkJykyOld.Value = IIf(blnChecked = True, "1", "0")

            '���z���Z���i�R�[�hDispOld
            Me.HiddenKasanSyouhinCdDispOld.Value = Me.TextKasanSyouhinCd.Value
            '���z���Z���i��DispOld
            Me.HiddenKasanSyouhinMeiDispOld.Value = Me.TextKasanSyouhinMei.Value
            '�H���X�������Z���zDispOld
            Me.HiddenKoumutenSeikyuKasanKingakuDispOld.Value = Me.TextKoumutenSeikyuKasanKingaku.Value
            '���������Z���zDispOld
            Me.HiddenJituSeikyuKasanKingakuDispOld.Value = Me.TextJituSeikyuKasanKingaku.Value

            '���g����(New)�l��ޔ�
            '���z���Z���i�R�[�hNew
            Me.HiddenKasanSyouhinCdNew.Value = strKasanSyouhinCdNew
            '���z���Z���i��Old
            Me.HiddenKasanSyouhinMeiNew.Value = strSyouhinMeiNew
            '�H���X�������Z���zOld
            Me.HiddenKoumutenSeikyuKasanKingakuNew.Value = Format(intKoumutenKasanGakuNew, EarthConst.FORMAT_KINGAKU_1)
            '���������Z���zOld
            Me.HiddenJituSeikyuKasanKingakuNew.Value = Format(intUriKasanGakuNew, EarthConst.FORMAT_KINGAKU_1)

            '���g����(Old)�l��ޔ�
            '���z���Z���i�R�[�hOld
            Me.HiddenKasanSyouhinCdOld.Value = strKasanSyouhinCdOld
            '���z���Z���i��Old
            Me.HiddenKasanSyouhinMeiOld.Value = strSyouhinMeiOld
            '�H���X�������Z���zOld
            Me.HiddenKoumutenSeikyuKasanKingakuOld.Value = Format(intKoumutenKasanGakuOld, EarthConst.FORMAT_KINGAKU_1)
            '���������Z���zOld
            Me.HiddenJituSeikyuKasanKingakuOld.Value = Format(intUriKasanGakuOld, EarthConst.FORMAT_KINGAKU_1)

            '���}�X�^�l��ޔ�
            '���ʑΉ��`�F�b�N�{�b�N�X
            Me.HiddenTokubetuTaiouCheckedMst.Value = IIf(blnCheckedMst = True, EarthConst.ARI_VAL, EarthConst.NASI_VAL)
            '���z���Z���i�R�[�hOld
            Me.HiddenKasanSyouhinCdMst.Value = strKasanSyouhinCdMst
            '���z���Z���i��Old
            Me.HiddenKasanSyouhinMeiMst.Value = strSyouhinMeiMst
            '�H���X�������Z���zOld
            Me.HiddenKoumutenSeikyuKasanKingakuMst.Value = Format(intKoumutenKasanGakuMst, EarthConst.FORMAT_KINGAKU_1)
            '���������Z���zOld
            Me.HiddenJituSeikyuKasanKingakuMst.Value = Format(intUriKasanGakuMst, EarthConst.FORMAT_KINGAKU_1)

            '�\���ؑ֗p
            If dtRec.DispHantei Then
                Me.HiddenDisp.Value = EarthConst.ARI_VAL
            Else
                Me.HiddenDisp.Value = EarthConst.NASI_VAL
            End If

            '�X�V����
            Me.HiddenUpdDatetime.Value = IIf(dtRec.UpdDatetime = Date.MinValue, "", Format(dtRec.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
        End If

        '******************************************
        '* �\���ݒ�
        '******************************************
        Me.SetDispControl()

        Select Case dtRec.PrimaryDisplay
            Case True
                Me.SetDispControlSyouhin(EarthEnum.emTokubetuTaiouActBtn.BtnLoad)
            Case False
                Me.SetDispControlSyouhin(EarthEnum.emTokubetuTaiouActBtn.BtnMaster)
        End Select
    End Sub

    ''' <summary>
    ''' ���ʑΉ����R�[�h�����ʕ\�����ڂւ̒l�Z�b�g���s�Ȃ�(�}�X�^�l�𔽉f)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromHiddenMst(ByVal sender As Object _
                                    , ByVal e As System.EventArgs _
                                    , ByRef dtRec As TokubetuTaiouRecordBase _
                                    )

        '******************************************
        '* �\���ݒ�
        '******************************************
        Me.SetDispControlSyouhin(EarthEnum.emTokubetuTaiouActBtn.BtnMaster)

        Dim blnCheckedMst As Boolean = False
        Dim strKasanSyouhinCdMst As String = String.Empty
        Dim strSyouhinMeiMst As String = String.Empty
        Dim intUriKasanGakuMst As Integer = Integer.MinValue
        Dim intKoumutenKasanGakuMst As Integer = Integer.MinValue

        '�����X�}�X�^���ŏ㏑��
        blnCheckedMst = IIf(Me.HiddenTokubetuTaiouCheckedMst.Value = "1", True, False)
        strKasanSyouhinCdMst = cl.GetDisplayString(Me.HiddenKasanSyouhinCdMst.Value)
        strSyouhinMeiMst = cl.GetDisplayString(Me.HiddenKasanSyouhinMeiMst.Value)
        intUriKasanGakuMst = cl.GetDispNum(Me.HiddenJituSeikyuKasanKingakuMst.Value)
        intKoumutenKasanGakuMst = cl.GetDispNum(Me.HiddenKoumutenSeikyuKasanKingakuMst.Value)

        '���ʑΉ��R�[�h
        Me.HiddenTokubetuTaiouCd.Value = Me.HiddenTokubetuTaiouCd.Value
        '�`�F�b�N���
        Me.CheckBoxTokubetuTaiou.Checked = blnCheckedMst
        '���z���Z���i�R�[�h
        Me.TextKasanSyouhinCd.Value = strKasanSyouhinCdMst
        '���z���Z���i��
        Me.TextKasanSyouhinMei.Value = strSyouhinMeiMst
        '�H���X�������Z���z
        Me.TextKoumutenSeikyuKasanKingaku.Value = Format(intKoumutenKasanGakuMst, EarthConst.FORMAT_KINGAKU_1)
        '���������Z���z
        Me.TextJituSeikyuKasanKingaku.Value = Format(intUriKasanGakuMst, EarthConst.FORMAT_KINGAKU_1)

        '�������̏ꍇ�A�ݒ���ێ����Ă����B�ȊO�̓N���A����B
        If Me.HiddenSetteiSakiStyle.Value <> EarthConst.STYLE_COLOR_BLUE Then
            '���ރR�[�h
            Me.HiddenBunruiCd.Value = String.Empty
            '��ʕ\��NO
            Me.HiddenGamenHyoujiNo.Value = "0"
        End If

        '******************************************
        '* �\���ݒ�
        '******************************************
        Me.SetDispControl()

    End Sub

    ''' <summary>
    ''' ��ʂ̊e���׍s�������R�[�h�N���X�Ɏ擾���A���ʑΉ����R�[�h�N���X��ԋp����
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetRowCtrlToDataRec(ByRef dataRec As TokubetuTaiouRecordBase)
        Dim ctrl As New TokubetuTaiouRecordCtrl

        '***************************************
        ' ���ʑΉ��f�[�^
        '***************************************
        '���ʑΉ��R�[�h
        cl.SetDisplayString(Me.HiddenTokubetuTaiouCd.Value, dataRec.TokubetuTaiouCd)
        '���ʑΉ���
        cl.SetDisplayString(Me.HiddenTokubetuTaiouMei.Value, dataRec.TokubetuTaiouMeisyou)

        '�`�F�b�N�L��
        dataRec.CheckJyky = Me.CheckBoxTokubetuTaiou.Checked
        '�`�F�b�N�L��Old
        dataRec.CheckJykyOld = IIf(Me.HiddenChkJykyOld.Value = "1", True, False)

        '����ʒl���Z�b�g(�}�X�^�Ď擾�{�^�������ɂ��ύX�����\��������̂ŁAOld�l�ޔ��͌㑱�̃��W�b�N�ɂď������s�Ȃ�)
        '���z���Z���i�R�[�h
        cl.SetDisplayString(Me.TextKasanSyouhinCd.Value, dataRec.KasanSyouhinCd)
        '�H���X�������Z���z
        cl.SetDisplayString(Me.TextKoumutenSeikyuKasanKingaku.Value, dataRec.KoumutenKasanGaku)
        '���������Z���z
        cl.SetDisplayString(Me.TextJituSeikyuKasanKingaku.Value, dataRec.UriKasanGaku)
        '���z���Z���i�R�[�hOld
        cl.SetDisplayString(Me.HiddenKasanSyouhinCdOld.Value, dataRec.KasanSyouhinCdOld)
        '�H���X�������Z���zOld
        cl.SetDisplayString(Me.HiddenKoumutenSeikyuKasanKingakuOld.Value, dataRec.KoumutenKasanGakuOld)
        '���������Z���zOld
        cl.SetDisplayString(Me.HiddenJituSeikyuKasanKingakuOld.Value, dataRec.UriKasanGakuOld)

        '���������邢�̓`�F�b�N�{�b�N�X�`�F�b�N��Ԃ̏ꍇ�A�ݒ���ێ����Ă����B�ȊO�̓N���A����B
        If Me.HiddenSetteiSakiStyle.Value <> EarthConst.STYLE_COLOR_BLUE Then
            '���ރR�[�h
            Me.HiddenBunruiCd.Value = String.Empty
            '��ʕ\��NO
            Me.HiddenGamenHyoujiNo.Value = "0"
        End If
        '���ރR�[�h
        cl.SetDisplayString(Me.HiddenBunruiCd.Value, dataRec.BunruiCd)
        '��ʕ\��NO
        cl.SetDisplayString(Me.HiddenGamenHyoujiNo.Value, dataRec.GamenHyoujiNo)

        '���
        dataRec.Torikesi = IIf(dataRec.CheckJyky = True, 0, 1)
        '�X�V�t���O
        cl.SetDisplayString(Me.HiddenUpdFlg.Value, dataRec.UpdFlg)
        dataRec.UpdFlg = IIf(dataRec.HenkouCheck = True, 1, 0)
        '���i�����t���O
        cl.SetDisplayString(Me.HiddenKkkSyoriFlg.Value, dataRec.KkkSyoriFlg)
        '�ݒ��/�X�^�C��
        cl.SetDisplayString(Me.HiddenSetteiSakiStyle.Value, dataRec.SetteiSakiStyle)

        ' �X�V���� �ǂݍ��ݎ��̃^�C���X�^���v
        If Me.HiddenUpdDatetime.Value = "" Then
            dataRec.UpdDatetime = DateTime.MinValue
        Else
            dataRec.UpdDatetime = DateTime.ParseExact(Me.HiddenUpdDatetime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
        End If

        Me.SetDispControl()

    End Sub

    ''' <summary>
    ''' �`�F�b�N���ꂽ�s�̏����擾���A���ʑΉ����R�[�h�N���X��ԋp����
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetChkRowCtrlToDataRec() As TokubetuTaiouRecordBase
        Dim ctrl As New TokubetuTaiouRecordCtrl
        ' ��ʓ��e���n�Ճ��R�[�h�𐶐�����
        Dim dataRec As New TokubetuTaiouRecordBase

        '***************************************
        ' ���ʑΉ��f�[�^
        '***************************************
        '���ʑΉ��R�[�h
        cl.SetDisplayString(Me.HiddenTokubetuTaiouCd.Value, dataRec.TokubetuTaiouCd)
        '���ʑΉ���
        cl.SetDisplayString(Me.HiddenTokubetuTaiouMei.Value, dataRec.TokubetuTaiouMeisyou)

        '���E�����ȊO�͍Đݒ�ׁ̈A�N���A����
        If Me.HiddenSetteiSakiStyle.Value <> EarthConst.STYLE_COLOR_BLUE Then
            '���ރR�[�h
            Me.HiddenBunruiCd.Value = String.Empty
            '��ʕ\��NO
            Me.HiddenGamenHyoujiNo.Value = "0"
        End If
        '���ރR�[�h
        cl.SetDisplayString(Me.HiddenBunruiCd.Value, dataRec.BunruiCd)
        '��ʕ\��NO
        cl.SetDisplayString(Me.HiddenGamenHyoujiNo.Value, dataRec.GamenHyoujiNo)

        '���z���Z���i�R�[�h
        cl.SetDisplayString(Me.TextKasanSyouhinCd.Value, dataRec.KasanSyouhinCd)
        '�H���X�������Z���z
        cl.SetDisplayString(Me.TextKoumutenSeikyuKasanKingaku.Value, dataRec.KoumutenKasanGaku)
        '���������Z���z
        cl.SetDisplayString(Me.TextJituSeikyuKasanKingaku.Value, dataRec.UriKasanGaku)
        '���i�����t���O
        cl.SetDisplayString(Me.HiddenKkkSyoriFlg.Value, dataRec.KkkSyoriFlg)
        '�`�F�b�N��
        dataRec.CheckJyky = Me.CheckBoxTokubetuTaiou.Checked
        '�`�F�b�N��(Old�l)
        dataRec.CheckJykyOld = IIf(Me.HiddenChkJykyOld.Value = "1", True, False)
        '�X�V�t���O
        dataRec.UpdFlg = IIf(Me.HiddenUpdFlg.Value = "1", "1", "0")
        '�ݒ��̃X�^�C��
        cl.SetDisplayString(Me.HiddenSetteiSakiStyle.Value, dataRec.SetteiSakiStyle)

        Return dataRec
    End Function

#Region "��ʐ���"

    ''' <summary>
    ''' ���׍s�̕\���ݒ���s���B
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDispControl()
        Dim intGamenHyoujiNo As Integer
        Dim strSettei As String

        '���\���ؑ�
        If Me.HiddenDisp.Value = EarthConst.ARI_VAL Then
            Me.Tr1.Style(EarthConst.STYLE_DISPLAY) = EarthConst.STYLE_DISPLAY_INLINE
        Else
            Me.Tr1.Style(EarthConst.STYLE_DISPLAY) = EarthConst.STYLE_DISPLAY_NONE
        End If

        '��tabindex�̕t�^
        Me.CheckBoxTokubetuTaiou.Attributes(EarthConst.STYLE_TAB_INDEX) = CInt(Me.HiddenTokubetuTaiouCd.Value) + 1

        '���\���X�^�C���ύX/�ݒ��F�f�t�H���g�Ԏ�����
        Me.HiddenSetteiSakiStyle.Value = EarthConst.STYLE_COLOR_RED
        '�ݒ�悪�Z�b�g����Ă���ꍇ�A������
        If Me.HiddenBunruiCd.Value <> String.Empty AndAlso (Me.HiddenGamenHyoujiNo.Value <> String.Empty AndAlso Me.HiddenGamenHyoujiNo.Value <> "0") Then
            Me.HiddenSetteiSakiStyle.Value = EarthConst.STYLE_COLOR_BLUE '������
        End If

        Select Case Me.HiddenSetteiSakiStyle.Value
            Case EarthConst.STYLE_COLOR_BLUE
                cl.setStyleBlueBold(Me.AccTextSetteiSaki.Style, True)
            Case EarthConst.STYLE_COLOR_RED
                cl.setStyleRedBold(Me.AccTextSetteiSaki.Style, True)
        End Select
        '�ݒ��
        cl.SetDisplayString(Me.HiddenGamenHyoujiNo.Value, intGamenHyoujiNo)
        strSettei = cbLogic.DevideTokubetuCd(Me, Me.HiddenBunruiCd.Value, intGamenHyoujiNo)
        Me.AccTextSetteiSaki.Value = IIf(strSettei <> String.Empty, "���i" & strSettei, String.Empty)

    End Sub

    ''' <summary>
    ''' ���׍s�̕\���ݒ���s���B
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDispControlSyouhin(Optional ByVal intBtnFlg As EarthEnum.emTokubetuTaiouActBtn = EarthEnum.emTokubetuTaiouActBtn.BtnLoad)

        '���\���X�^�C���ύX/���̑��F���ق�����ꍇ�A�Ԏ�����
        Select Case intBtnFlg
            Case EarthEnum.emTokubetuTaiouActBtn.BtnMaster
                '���z���Z���i�R�[�hMst
                If Me.TextKasanSyouhinCd.Value = Me.HiddenKasanSyouhinCdMst.Value Then
                    cl.setStyleRedBold(Me.TextKasanSyouhinCd.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextKasanSyouhinCd.Style, True)
                End If
                '���z���Z���i��Mst
                If Me.TextKasanSyouhinMei.Value = Me.HiddenKasanSyouhinMeiMst.Value Then
                    cl.setStyleRedBold(Me.TextKasanSyouhinMei.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextKasanSyouhinMei.Style, True)
                End If
                '�H���X�������Z���zMst
                If Me.TextKoumutenSeikyuKasanKingaku.Value = Me.HiddenKoumutenSeikyuKasanKingakuMst.Value Then
                    cl.setStyleRedBold(Me.TextKoumutenSeikyuKasanKingaku.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextKoumutenSeikyuKasanKingaku.Style, True)
                End If
                '���������Z���zMst
                If Me.TextJituSeikyuKasanKingaku.Value = Me.HiddenJituSeikyuKasanKingakuMst.Value Then
                    cl.setStyleRedBold(Me.TextJituSeikyuKasanKingaku.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextJituSeikyuKasanKingaku.Style, True)
                End If

            Case EarthEnum.emTokubetuTaiouActBtn.BtnLoad
                '���z���Z���i�R�[�hOld
                If Me.TextKasanSyouhinCd.Value = Me.HiddenKasanSyouhinCdOld.Value Then
                    cl.setStyleRedBold(Me.TextKasanSyouhinCd.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextKasanSyouhinCd.Style, True)
                End If
                '���z���Z���i��Old
                If Me.TextKasanSyouhinMei.Value = Me.HiddenKasanSyouhinMeiOld.Value Then
                    cl.setStyleRedBold(Me.TextKasanSyouhinMei.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextKasanSyouhinMei.Style, True)
                End If
                '�H���X�������Z���zOld
                If Me.TextKoumutenSeikyuKasanKingaku.Value = Me.HiddenKoumutenSeikyuKasanKingakuOld.Value Then
                    cl.setStyleRedBold(Me.TextKoumutenSeikyuKasanKingaku.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextKoumutenSeikyuKasanKingaku.Style, True)
                End If
                '���������Z���zOld
                If Me.TextJituSeikyuKasanKingaku.Value = Me.HiddenJituSeikyuKasanKingakuOld.Value Then
                    cl.setStyleRedBold(Me.TextJituSeikyuKasanKingaku.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextJituSeikyuKasanKingaku.Style, True)
                End If

            Case Else
        End Select

    End Sub

#End Region

#End Region

    ''' <summary>
    ''' ���ʑΉ��`�F�b�N�{�b�N�X/�`�F�b�N�C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonCheckBoxTokubetuTaiou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonChkTokubetuTaiou.ServerClick

        '�e��ʃC�x���g�ďo
        RaiseEvent SetCheckTokubetuTaiouChangeAction(sender, e, Me.CheckBoxTokubetuTaiou.ClientID)

    End Sub
End Class