Public Class SeikyuuSiireLinkCtrl
    Inherits System.Web.UI.UserControl

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private cl As New CommonLogic
    Private cbl As New CommonBizLogic
    Private Const CHK_TRUE As String = "1"
    Private Const CHK_False As String = "0"

#Region "�v���p�e�B"
    ''' <summary>
    ''' �����X�R�[�hHidden�ւ̊O���A�N�Z�X�p�v���p�e�B
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccKameitenCd() As HtmlInputHidden
        Get
            Return HiddenKameitenCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenKameitenCd = value
        End Set
    End Property

    ''' <summary>
    ''' ������R�[�hHidden�ւ̊O���A�N�Z�X�p�v���p�e�B
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSeikyuuSakiCd() As HtmlInputHidden
        Get
            Return HiddenSeikyuuSakiCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenSeikyuuSakiCd = value
        End Set
    End Property

    ''' <summary>
    ''' ������}��Hidden�ւ̊O���A�N�Z�X�p�v���p�e�B
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSeikyuuSakiBrc() As HtmlInputHidden
        Get
            Return HiddenSeikyuuSakiBrc
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenSeikyuuSakiBrc = value
        End Set
    End Property

    ''' <summary>
    ''' ������敪Hidden�ւ̊O���A�N�Z�X�p�v���p�e�B
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSeikyuuSakiKbn() As HtmlInputHidden
        Get
            Return HiddenSeikyuuSakiKbn
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenSeikyuuSakiKbn = value
        End Set
    End Property

    ''' <summary>
    ''' ������ЃR�[�hHidden�ւ̊O���A�N�Z�X�p�v���p�e�B
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTysKaisyaCd() As HtmlInputHidden
        Get
            Return HiddenTysKaisyaCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenTysKaisyaCd = value
        End Set
    End Property

    ''' <summary>
    ''' ������Ў��Ə��R�[�hHidden�ւ̊O���A�N�Z�X�p�v���p�e�B
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTysKaisyaJigyousyoCd() As HtmlInputHidden
        Get
            Return HiddenTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenTysKaisyaJigyousyoCd = value
        End Set
    End Property

    ''' <summary>
    ''' �������ߓ�Hidden�ւ̊O���A�N�Z�X�p�v���p�e�B
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSeikyuuSimeDate() As HtmlInputHidden
        Get
            Return HiddenSimeDate
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenSimeDate = value
        End Set
    End Property

    ''' <summary>
    ''' ������/�d���惊���N�ւ̊O���A�N�Z�X�p�v���p�e�B
    ''' </summary>
    ''' <value>LinkSeikyuuSiireHenkou</value>
    ''' <returns>LinkSeikyuuSiireHenkou</returns>
    ''' <remarks></remarks>
    Public Property AccLinkSeikyuuSiireHenkou() As HtmlAnchor
        Get
            Return LinkSeikyuuSiireHenkou
        End Get
        Set(ByVal value As HtmlAnchor)
            LinkSeikyuuSiireHenkou = value
        End Set
    End Property

    ''' <summary>
    ''' ������ύX���`�F�b�N�t���O
    ''' </summary>
    ''' <value>HiddenChkSeikyuuSakiChg</value>
    ''' <returns>HiddenChkSeikyuuSakiChg</returns>
    ''' <remarks></remarks>
    Public Property AccHiddenChkSeikyuuSakiChg() As HtmlInputHidden
        Get
            Return HiddenChkSeikyuuSakiChg
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenChkSeikyuuSakiChg = value
        End Set
    End Property

    ''' <summary>
    ''' ������^�C�v������i���ڐ���/�������j
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiTypeStr As String = string.Empty
    ''' <summary>
    ''' ������^�C�v������i���ڐ���/�������j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SeikyuuSakiTypeStr() As String
        Get
            Return strSeikyuuSakiTypeStr
        End Get
    End Property

#End Region

    ''' <summary>
    ''' �y�[�W�`��O����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        '������E�d����̃����N�ݒ�
        SetLinkStyle()

        '�����N�̕\��On/Off�ݒ�
        If Me.HiddenSyouhinCd.Value <> String.Empty Then
            Me.LinkSeikyuuSiireHenkou.Style("display") = "inline"
        Else
            Me.LinkSeikyuuSiireHenkou.Style("display") = "none"
            ClearSeikyuuSiireInfo()
        End If

        '�c�[���`�b�v�̐ݒ�
        Dim strToolTip As String = GetLinkTip()
        'Html�R���g���[���Ƀc�[���`�b�v��ݒ肷��
        cl.SetToolTipForCtrl(Me.LinkSeikyuuSiireHenkou, strToolTip)

        '�C�x���g�n���h���̕t�^
        Me.LinkSeikyuuSiireHenkou.Attributes("onclick") = "callSeikyuuSiireSakiModal('" _
                                                                & UrlConst.SEIKYUU_SIIRE_HENKOU & "','" _
                                                                & Me.HiddenSeikyuuSakiCd.ClientID & "','" _
                                                                & Me.HiddenSeikyuuSakiBrc.ClientID & "','" _
                                                                & Me.HiddenSeikyuuSakiKbn.ClientID & "','" _
                                                                & Me.HiddenTysKaisyaCd.ClientID & "','" _
                                                                & Me.HiddenTysKaisyaJigyousyoCd.ClientID & "','" _
                                                                & Me.HiddenKameitenCd.ClientID & "','" _
                                                                & Me.HiddenDefaultSiireSaki.ClientID & "','" _
                                                                & Me.HiddenSyouhinCd.ClientID & "','" _
                                                                & Me.HiddenKojKaisyaSeikyuu.ClientID & "','" _
                                                                & Me.HiddenKojKaisyaCd.ClientID & "','" _
                                                                & Me.HiddenUriageSyorizumi.ClientID & "','" _
                                                                & Me.HiddenViewMode.ClientID & "','" _
                                                                & Me.UpdatePanelSeikyuuSiireLink.ClientID & "','" _
                                                                & Me.HiddenChkSeikyuuSakiChg.ClientID & "')"
        '�A�b�v�f�[�g�p�l��(�����N��)�̍X�V
        Me.UpdatePanelSeikyuuSiireLink.Update()

    End Sub

    ''' <summary>
    ''' �@�ʐ������R�[�h���琿����/�d���惊���N�ɒl���Z�b�g
    ''' </summary>
    ''' <param name="recData">�@�ʐ������R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetSeikyuuSiireLinkFromTeibetuRec(ByVal recData As TeibetuSeikyuuRecord)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetSeikyuuSiireLinkFromTeibetuRec", _
                                                    recData)
        With recData
            ' ������R�[�h
            Me.HiddenSeikyuuSakiCd.Value = cl.GetDispStr(.SeikyuuSakiCd)
            ' ������}��
            Me.HiddenSeikyuuSakiBrc.Value = cl.GetDispStr(.SeikyuuSakiBrc)
            ' ������敪
            Me.HiddenSeikyuuSakiKbn.Value = cl.GetDispStr(.SeikyuuSakiKbn)
            ' ������ЃR�[�h
            Me.HiddenTysKaisyaCd.Value = cl.GetDispStr(.TysKaisyaCd)
            ' ������Ў��Ə��R�[�h
            Me.HiddenTysKaisyaJigyousyoCd.Value = cl.GetDispStr(.TysKaisyaJigyousyoCd)
        End With
    End Sub

    ''' <summary>
    ''' ������/�d���惊���N����@�ʐ������R�[�h�ɒl���Z�b�g
    ''' </summary>
    ''' <param name="redData">�@�ʐ������R�[�h(�Q�Ɠn��)</param>
    ''' <remarks></remarks>
    Public Sub SetTeibetuRecFromSeikyuuSiireLink(ByRef redData As TeibetuSeikyuuRecord)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetTeibetuRecFromSeikyuuSiireLink", _
                                                    redData)
        With redData
            '������R�[�h
            If Me.HiddenSeikyuuSakiCd.Value <> String.Empty Then
                .SeikyuuSakiCd = Me.HiddenSeikyuuSakiCd.Value
            Else
                redData.SeikyuuSakiCd = Nothing
            End If
            '������}��
            If Me.HiddenSeikyuuSakiBrc.Value <> String.Empty Then
                redData.SeikyuuSakiBrc = Me.HiddenSeikyuuSakiBrc.Value
            Else
                redData.SeikyuuSakiBrc = Nothing
            End If
            '������敪
            If Me.HiddenSeikyuuSakiKbn.Value <> String.Empty Then
                redData.SeikyuuSakiKbn = Me.HiddenSeikyuuSakiKbn.Value
            Else
                redData.SeikyuuSakiKbn = Nothing
            End If
            '������ЃR�[�h
            If Me.HiddenTysKaisyaCd.Value <> String.Empty Then
                redData.TysKaisyaCd = Me.HiddenTysKaisyaCd.Value
            Else
                redData.TysKaisyaCd = Nothing
            End If
            '������Ў��Ə��R�[�h
            If Me.HiddenTysKaisyaJigyousyoCd.Value <> String.Empty Then
                redData.TysKaisyaJigyousyoCd = Me.HiddenTysKaisyaJigyousyoCd.Value
            Else
                redData.TysKaisyaJigyousyoCd = Nothing
            End If
        End With
    End Sub

    ''' <summary>
    ''' ��ʂɂ���Ēl���ύX���꓾��R���g���[�����琿����/�d���惊���N�ɒl���Z�b�g
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="strSiireSakiCd">�d����R�[�h(�n�Ղ̒�����ЃR�[�h)</param>
    ''' <param name="strKeijyouZumi">�v��ςݔ��f(1�F�v��ς݁^1�ȊO�F���v��)</param>
    ''' <param name="blnKojKaisyaSeikyuu">�H����А������f</param>
    ''' <param name="strKojKaisyaCd">�H����ЃR�[�h</param>
    ''' <param name="strDenUriDate">�`�[����N����</param>
    ''' <param name="strViewMode">�\�����[�h</param>
    ''' <returns>����������i�[����Dictionary</returns>
    ''' <remarks>������E�d����̊�{�����Z�b�g����ׂ̏���e��ʂ���擾����</remarks>
    Public Function SetVariableValueCtrlFromParent(ByVal strKameitenCd As String _
                                            , ByVal strSyouhinCd As String _
                                            , ByVal strSiireSakiCd As String _
                                            , ByVal strKeijyouZumi As String _
                                            , Optional ByVal blnKojKaisyaSeikyuu As Boolean = False _
                                            , Optional ByVal strKojKaisyaCd As String = "" _
                                            , Optional ByVal strDenUriDate As String = "" _
                                            , Optional ByVal strViewMode As String = EarthConst.MODE_EDIT) As Dictionary(Of String, String)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetVariableValueCtrlFromParent" _
                                                    , strKameitenCd _
                                                    , strSyouhinCd _
                                                    , strSiireSakiCd _
                                                    , strKojKaisyaCd _
                                                    , strDenUriDate _
                                                    , strViewMode)
        '��{������i�[Dictionary
        Dim dicSeikyuuSakiInfo As New Dictionary(Of String, String)

        '���i�R�[�h
        Me.HiddenSyouhinCd.Value = strSyouhinCd

        '�����X�R�[�h
        Me.HiddenKameitenCd.Value = strKameitenCd
        '�H����А���
        If blnKojKaisyaSeikyuu Then
            Me.HiddenKojKaisyaSeikyuu.Value = CHK_TRUE
        Else
            Me.HiddenKojKaisyaSeikyuu.Value = CHK_False
        End If
        '�H����ЃR�[�h
        Me.HiddenKojKaisyaCd.Value = strKojKaisyaCd

        '��{�d����R�[�h�̐ݒ�
        Me.HiddenDefaultSiireSaki.Value = strSiireSakiCd

        '����v��ς̃Z�b�g
        Me.HiddenUriageSyorizumi.Value = strKeijyouZumi

        '�`�[����N�����̃Z�b�g
        Me.HiddenDenUriDate.Value = strDenUriDate

        '�\�����[�h�̃Z�b�g
        Me.HiddenViewMode.Value = strViewMode

        '���i�R�[�h���Ȃ��ꍇ�̓����N���\���Ȃ̂ŁA�����擾����ׂ�DB�A�N�Z�X�͂��Ȃ�
        If Me.HiddenSyouhinCd.Value = String.Empty Then

            '�A�b�v�f�[�g�p�l��(��{��񓙂�Hidden)�̍X�V
            Me.UpdatePanelSeikyuuSiireInfo.Update()

            Return Nothing
            Exit Function
        End If

        '��{������̎擾
        If Me.HiddenKojKaisyaSeikyuu.Value = CHK_TRUE Then
            '�H����А���
            dicSeikyuuSakiInfo = cbl.getDefaultSeikyuuSaki(strKojKaisyaCd)
        Else
            '�����X����
            dicSeikyuuSakiInfo = cbl.getDefaultSeikyuuSaki(strKameitenCd, strSyouhinCd)
        End If

        '������^�C�v������̃Z�b�g
        SetSeikyuuSakiTypeStr(dicSeikyuuSakiInfo)

        '��{������R�[�h�̐ݒ�
        Me.HiddenDefaultSeikyuuSaki.Value = dicSeikyuuSakiInfo(cbl.dicKeySeikyuuSakiCd) & _
                                            dicSeikyuuSakiInfo(cbl.dicKeySeikyuuSakiBrc) & _
                                            dicSeikyuuSakiInfo(cbl.dicKeySeikyuuSakiKbn)

        '��{�����於�̐ݒ�
        HiddenDefaultSeikyuuSakiMei.Value = dicSeikyuuSakiInfo(cbl.dicKeySeikyuuSakiMei)

        '�A�b�v�f�[�g�p�l��(��{��񓙂�Hidden)�̍X�V
        Me.UpdatePanelSeikyuuSiireInfo.Update()

        Return dicSeikyuuSakiInfo

    End Function

    ''' <summary>
    ''' �������ߓ��̐ݒ�
    ''' </summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="blnKojKaisyaSeikyuu">�H����А���</param>
    ''' <param name="strKojKaisyaCd">�H����ЃR�[�h</param>
    ''' <remarks>�����ݒ�p</remarks>
    Public Sub SetSeikyuuSimeDate(ByVal strSyouhinCd As String, Optional ByVal blnKojKaisyaSeikyuu As Boolean = False, Optional ByVal strKojKaisyaCd As String = "")
        '���i�R�[�h
        Me.HiddenSyouhinCd.Value = strSyouhinCd

        '�H����А���
        If blnKojKaisyaSeikyuu Then
            Me.HiddenKojKaisyaSeikyuu.Value = CHK_TRUE
        Else
            Me.HiddenKojKaisyaSeikyuu.Value = CHK_False
        End If

        '�H����ЃR�[�h
        Me.HiddenKojKaisyaCd.Value = strKojKaisyaCd

        '�������ߓ��̃Z�b�g
        SetSeikyuuSimeDate()

    End Sub

    ''' <summary>
    ''' �������ߓ��̐ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetSeikyuuSimeDate()
        '�������ߓ��̐ݒ�
        If Me.HiddenKojKaisyaSeikyuu.Value = CHK_TRUE Then
            '�H����А���
            Me.HiddenSimeDate.Value = cl.GetDisplayString(cbl.GetSeikyuuSimeDateFromTyousa(Me.HiddenSeikyuuSakiCd.Value _
                                                                                    , Me.HiddenSeikyuuSakiBrc.Value _
                                                                                    , Me.HiddenSeikyuuSakiKbn.Value _
                                                                                    , Me.HiddenKojKaisyaCd.Value))
        Else
            '�����X����
            Me.HiddenSimeDate.Value = cl.GetDisplayString(cbl.GetSeikyuuSimeDateFromKameiten(Me.HiddenSeikyuuSakiCd.Value _
                                                                                    , Me.HiddenSeikyuuSakiBrc.Value _
                                                                                    , Me.HiddenSeikyuuSakiKbn.Value _
                                                                                    , Me.HiddenKameitenCd.Value _
                                                                                    , Me.HiddenSyouhinCd.Value))
        End If

    End Sub

    ''' <summary>
    ''' ���������s���̎擾
    ''' </summary>
    ''' <returns>���������s��</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuusyoHakkouDate(Optional ByVal strDenUriDate As String = "") As String

        If strDenUriDate = String.Empty Then
            strDenUriDate = Me.HiddenDenUriDate.Value
        End If
        Dim SeikyuusyoHakkouDate As String = cl.GetDisplayString(cbl.CalcSeikyuusyoHakkouDate(Me.HiddenSimeDate.Value, strDenUriDate))

        Return SeikyuusyoHakkouDate
    End Function

#Region "�v���C�x�[�g���\�b�h"
    ''' <summary>
    ''' ������/�d����ύX�����N�X�^�C���̐ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetLinkStyle()
        Const LINK_STYLE As String = "text-decoration:underline;cursor:pointer;"
        Const RED_BOLD_STYLE As String = "color:Red;font-weight:bold;"

        '��{������R�[�h
        Dim strDefSeikyuuSaki As String = Me.HiddenDefaultSeikyuuSaki.Value
        '��{�d����R�[�h
        Dim strDefSiireSaki As String = Me.HiddenDefaultSiireSaki.Value
        '�o�^������R�[�h
        Dim strChgSeikyuuSaki As String = Me.HiddenSeikyuuSakiCd.Value _
                                        & Me.HiddenSeikyuuSakiBrc.Value _
                                        & Me.HiddenSeikyuuSakiKbn.Value
        '�o�^�d����R�[�h
        Dim strChgSiireSaki As String = Me.HiddenTysKaisyaCd.Value _
                                        & Me.HiddenTysKaisyaJigyousyoCd.Value

        '�����N�ɃX�^�C���̓K�p(�|�C���^�̕ύX�ƃA���_�[�o�[�̕t�^)
        Me.LinkSeikyuuSiireHenkou.Attributes("style") = LINK_STYLE
        Me.lblLinkSeikyuuStr.Attributes("style") = LINK_STYLE
        Me.lblLinkSiireStr.Attributes("style") = LINK_STYLE

        '�����N�̐F��ݒ�
        If strChgSeikyuuSaki <> String.Empty And strChgSeikyuuSaki <> strDefSeikyuuSaki Then
            lblLinkSeikyuuStr.Attributes("style") = RED_BOLD_STYLE
        End If

        If strChgSiireSaki <> String.Empty And strChgSiireSaki <> strDefSiireSaki Then
            lblLinkSiireStr.Attributes("style") = RED_BOLD_STYLE
        End If

    End Sub

    ''' <summary>
    ''' ������E�d����̃c�[���`�b�v�ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetLinkTip() As String
        Dim strSeikyuuSakiCd As String = Me.HiddenSeikyuuSakiCd.Value
        Dim strSeikyuuSakiBrc As String = Me.HiddenSeikyuuSakiBrc.Value
        Dim strSeikyuuSakiKbn As String = Me.HiddenSeikyuuSakiKbn.Value
        Dim strKameitenCd As String = Me.HiddenKameitenCd.Value
        Dim strSyouhinCd As String = Me.HiddenSyouhinCd.Value
        Dim strDefaultSiireSaki As String = Me.HiddenDefaultSiireSaki.Value
        Dim strChangeSeikyuuSaki As String = strSeikyuuSakiCd & strSeikyuuSakiBrc & strSeikyuuSakiKbn
        Dim strChangeSiireSaki As String = Me.HiddenTysKaisyaCd.Value & Me.HiddenTysKaisyaJigyousyoCd.Value

        Dim strTipSeikyuuSaki As String
        Dim strTipSiireSaki As String
        Dim uriageLogic As New UriageDataSearchLogic
        Dim seikyuuSakiList As List(Of SeikyuuSakiInfoRecord)
        Dim tysKaisyaLogic As New TyousakaisyaSearchLogic
        Dim strTysKaisyaCd As String

        '�����於�̎擾
        If strChangeSeikyuuSaki = String.Empty Then
            '�ύX�������ꍇ�̓f�t�H���g�̐����於
            strTipSeikyuuSaki = Me.HiddenDefaultSeikyuuSakiMei.Value
        Else
            '�ύX������ꍇ�ɂ͐V���������於
            seikyuuSakiList = uriageLogic.GetSeikyuuSakiInfo(strSeikyuuSakiCd, strSeikyuuSakiBrc, strSeikyuuSakiKbn)
            If seikyuuSakiList.Count > 0 Then
                strTipSeikyuuSaki = seikyuuSakiList(0).SeikyuuSakiMei
            Else
                strTipSeikyuuSaki = String.Empty
            End If
        End If

        '�d����R�[�h�̐ݒ�
        If strChangeSiireSaki = String.Empty Then
            '�ύX�������ꍇ�ɂ̓f�t�H���g�̎d���於
            strTysKaisyaCd = strDefaultSiireSaki
        Else
            '�ύX������ꍇ�ɂ͐V�����d���於
            strTysKaisyaCd = strChangeSiireSaki
        End If
        '�d���於�̎擾
        strTipSiireSaki = tysKaisyaLogic.GetTyousaKaisyaMei(strTysKaisyaCd, String.Empty, False)

        '�����N�̃c�[���`�b�v�ݒ�
        Dim cLogic As New CommonLogic
        Dim strLinkTip As String = String.Format(EarthConst.LINK_TIP_STRING, strTipSeikyuuSaki, strTipSiireSaki)

        Return strLinkTip
    End Function

    ''' <summary>
    ''' ������E�d������̃N���A
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearSeikyuuSiireInfo()
        Me.HiddenSeikyuuSakiCd.Value = String.Empty
        Me.HiddenSeikyuuSakiBrc.Value = String.Empty
        Me.HiddenSeikyuuSakiKbn.Value = String.Empty
        Me.HiddenTysKaisyaCd.Value = String.Empty
        Me.HiddenTysKaisyaJigyousyoCd.Value = String.Empty
    End Sub

    ''' <summary>
    ''' ������^�C�v������̃Z�b�g�i���ڐ���/�������j
    ''' </summary>
    ''' <param name="dicSeikyuuSakiInfo">��������Dictionary</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuSakiTypeStr(ByVal dicSeikyuuSakiInfo As Dictionary(Of String, String))
        Dim strSeikyuuSakiCd As String = Me.HiddenSeikyuuSakiCd.Value
        Dim strSeikyuuSakiBrc As String = Me.HiddenSeikyuuSakiBrc.Value
        Dim strSeikyuuSakikbn As String = Me.HiddenSeikyuuSakiKbn.Value
        Dim strKameitenCd As String = Me.HiddenKameitenCd.Value
        Dim strKihonSeikyuuSakiCd As String = dicSeikyuuSakiInfo(cbl.dicKeySeikyuuSakiCd)

        '������^�C�v�̔��f
        If strSeikyuuSakiCd = String.Empty _
        And strSeikyuuSakiBrc = String.Empty _
        And strSeikyuuSakikbn = String.Empty Then
            If strKameitenCd = strKihonSeikyuuSakiCd Then
                '���ڐ���
                strSeikyuuSakiTypeStr = EarthConst.SEIKYU_TYOKUSETU
            Else
                '������
                strSeikyuuSakiTypeStr = EarthConst.SEIKYU_TASETU
            End If
        Else
            If strKameitenCd = strSeikyuuSakiCd Then
                '���ڐ���
                strSeikyuuSakiTypeStr = EarthConst.SEIKYU_TYOKUSETU
            Else
                '������
                strSeikyuuSakiTypeStr = EarthConst.SEIKYU_TASETU
            End If
        End If

    End Sub
#End Region

End Class