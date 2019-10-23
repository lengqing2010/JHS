
Partial Public Class TeibetuSyouhinRecordCtrl
    Inherits System.Web.UI.UserControl

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private masterAjaxSM As New ScriptManager

    Private jSM As New JibanSessionManager

    Private cBizLogic As New CommonBizLogic
    Private cLogic As New CommonLogic


#Region "�R���g���[���̕\�����[�h"
    ''' <summary>
    ''' �R���g���[���̕\�����[�h
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DisplayMode
        ''' <summary>
        ''' ���i�P
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN1 = 1
        ''' <summary>
        ''' ���i�Q
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN2 = 2
        ''' <summary>
        ''' ���i�R
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN3 = 3
        ''' <summary>
        ''' �񍐏�
        ''' </summary>
        ''' <remarks></remarks>
        HOUKOKUSYO = 4
        ''' <summary>
        ''' �ۏ�
        ''' </summary>
        ''' <remarks></remarks>
        HOSYOU = 5
        ''' <summary>
        ''' �ۏ�(��񕥖�)
        ''' </summary>
        ''' <remarks></remarks>
        KAIYAKU = 6
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
            Return mode
        End Get
        Set(ByVal value As DisplayMode)
            mode = value
        End Set
    End Property

    ''' <summary>
    ''' �s��css�N���X��
    ''' </summary>
    ''' <remarks></remarks>
    Private _cssName As String
    ''' <summary>
    ''' �s��css�N���X��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CssName() As String
        Get
            Return _cssName
        End Get
        Set(ByVal value As String)
            _cssName = value
        End Set
    End Property

    ''' <summary>
    ''' �R���g���[���̕\���ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private _enabled As Boolean
    ''' <summary>
    ''' �R���g���[���̕\���ݒ�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Enabled() As Boolean
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            _enabled = value
            ' �e�L�X�g�{�b�N�X
            TextHattyuusyoKakuninbi.Enabled = _enabled
            TextHattyuusyoKingaku.Enabled = _enabled
            TextJituSeikyuuGaku.Enabled = _enabled
            TextSyouhizeiGaku.Enabled = _enabled
            TextSiireSyouhizeiGaku.Enabled = _enabled
            TextKoumutenSeikyuuGaku.Enabled = _enabled
            TextMitumorisyoSakuseibi.Enabled = _enabled
            TextSeikyuusyoHakkoubi.Enabled = _enabled
            TextSyoudakusyoKingaku.Enabled = _enabled
            TextUriageBi.Enabled = _enabled
            TextUriageNengappi.Enabled = _enabled
            TextDenpyouUriageNengappi.Enabled = _enabled
            TextDenpyouSiireNengappi.Enabled = _enabled
            ' �h���b�v�_�E�����X�g
            SelectSeikyuuUmu.Enabled = _enabled
            SelectHattyuusyoKakutei.Enabled = _enabled
            SelectUriageSyori.Enabled = _enabled
        End Set
    End Property

    ''' <summary>
    ''' �����L���̕\���ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private _enableSeikyuuUmu As Boolean
    ''' <summary>
    ''' �����L���̕\���ݒ�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EnableSeikyuuUmu() As Boolean
        Get
            Return _enableSeikyuuUmu
        End Get
        Set(ByVal value As Boolean)
            _enableSeikyuuUmu = value
            ' �h���b�v�_�E�����X�g
            SelectSeikyuuUmu.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' �s�̕\���ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private _rowVisible As Boolean = True
    ''' <summary>
    ''' �s�̕\���ݒ�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RowVisible() As Boolean
        Get
            Return _rowVisible
        End Get
        Set(ByVal value As Boolean)
            _rowVisible = value
        End Set
    End Property

    ''' <summary>
    ''' �󔒍s�̕\���ݒ�R���g���[���s�̉��ɋ󔒍s��ݒ肷�邩
    ''' </summary>
    ''' <remarks></remarks>
    Private _isRowSpacer As Boolean = False
    ''' <summary>
    ''' �󔒍s�̕\���ݒ�R���g���[���s�̉��ɋ󔒍s��ݒ肷�邩
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsRowSpacer() As Boolean
        Get
            Return _isRowSpacer
        End Get
        Set(ByVal value As Boolean)
            _isRowSpacer = value
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
    ''' <value>�c�a���擾�����@�ʐ������R�[�h</value>
    ''' <returns>��ʓ��e���ݒ肵���@�ʐ������R�[�h</returns>
    ''' <remarks>���N�G�X�g�p�����[�^�i�[�p</remarks>
    Public Property TeibetuSeikyuuRec() As TeibetuSeikyuuRecord
        Get
            Return GetCtrlData()
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            _teibetuSeikyuuRec = value

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
    ''' ��ʕ\��NO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GamenHyoujiNo() As String
        Get
            Return HiddenGamenHyoujiNo.Value
        End Get
        Set(ByVal value As String)
            HiddenGamenHyoujiNo.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �����^�C�v
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SeikyuuType() As String
        Get
            Return HiddenSeikyuuType.Value
        End Get
        Set(ByVal value As String)
            HiddenSeikyuuType.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �n��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KeiretuCd() As String
        Get
            Return HiddenKeiretuCd.Value
        End Get
        Set(ByVal value As String)
            HiddenKeiretuCd.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KameitenCd() As String
        Get
            Return HiddenKameitenCd.Value
        End Get
        Set(ByVal value As String)
            HiddenKameitenCd.Value = value
        End Set
    End Property

    ''' <summary>
    ''' UpdatePanel
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UpdateSyouhinPanel() As UpdatePanel
        Get
            Return UpdatePanelSyouhinRec
        End Get
        Set(ByVal value As UpdatePanel)
            UpdatePanelSyouhinRec = value
        End Set
    End Property

    ''' <summary>
    ''' ���i�P�����ݒ�p���R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AutoSetSyouhinRecord() As Syouhin1AutoSetRecord
        Set(ByVal value As Syouhin1AutoSetRecord)

            Dim strZeiKbn As String = String.Empty      '�ŋ敪
            Dim strZeiritu As String = String.Empty     '�ŗ�
            Dim strSyouhinCd As String = String.Empty   '���i�R�[�h

            ' �l���擾�ł����ꍇ�A�R���g���[���ɐݒ肷��B�擾�ł��Ȃ��ꍇ�f�[�^��S�ăN���A����
            If value Is Nothing Then
                EnabledCtrl(False)
            Else

                '����N�����Ŕ��f���āA�������ŗ����擾����
                strSyouhinCd = value.SyouhinCd
                If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                    '�擾�����ŋ敪�E�ŗ����Z�b�g
                    value.ZeiKbn = strZeiKbn
                    value.Zeiritu = strZeiritu
                End If

                TextSyouhinCd.Text = value.SyouhinCd                    ' ���i�R�[�h
                HiddenBunruiCd.Value = EarthConst.SOUKO_CD_SYOUHIN_1    ' ���ރR�[�h
                HiddenSyouhinCdOld.Value = value.SyouhinCd              ' ���i�R�[�h(�ύX�`�F�b�N�p)
                SpanSyouhinName.Text = value.SyouhinNm                  ' ���i��
                HiddenZeiKbn.Value = value.ZeiKbn                       ' �ŋ敪
                HiddenZeiritu.Value = value.Zeiritu                     ' �ŗ�

                '�����L�����L��̏ꍇ�A�������z���Z�b�g
                If SelectSeikyuuUmu.SelectedValue = "1" And _
                    value.SetSts <> EarthEnum.emSyouhin1Error.GetHanbai And _
                    value.SetSts <> EarthEnum.emSyouhin1Error.GetGenkaHanbai Then
                    '�H���X�����z
                    TextKoumutenSeikyuuGaku.Text = value.KoumutenGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                    '�������z
                    TextJituSeikyuuGaku.Text = value.JituGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                    SetZeigaku(Nothing)                                        ' �Ŋz�E�ō��z�̐ݒ�
                End If

                ' �d������Ŋz�ݒ�
                SetSiireZeigaku()
                ' �ō����z�ύX��e�R���g���[���ɒʒm����
                Dim e As New System.EventArgs
                RaiseEvent ChangeZeikomiGaku(Me, e, CInt(IIf(TextZeikomiKingaku.Text = "", "0", TextZeikomiKingaku.Text)))

                '����������
                EnabledCtrl(True)
                End If

        End Set
    End Property

#Region "���i�R�[�h"
    ''' <summary>
    ''' ���i�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���i�R�[�h</returns>
    ''' <remarks></remarks>
    Public Property SyouhinCd() As String
        Get
            Return TextSyouhinCd.Text
        End Get
        Set(ByVal value As String)
            TextSyouhinCd.Text = value
        End Set
    End Property
#End Region

#Region "�ō����z"
    ''' <summary>
    ''' �ō����z
    ''' </summary>
    ''' <value></value>


    Public ReadOnly Property ZeikomiKingaku() As Integer
        Get
            Dim strZeikomi As String = IIf(TextZeikomiKingaku.Text.Replace(",", "").Trim() = "", _
                                           "0", _
                                           TextZeikomiKingaku.Text.Replace(",", "").Trim())
            Return Integer.Parse(strZeikomi)
        End Get
    End Property
#End Region

#Region "���������z"
    ''' <summary>
    ''' ���������z
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property HattyuusyoKingaku() As String
        Get
            Dim strHattyuu As String = IIf(TextHattyuusyoKingaku.Text.Replace(",", "").Trim() = "", _
                                           "0", _
                                           TextHattyuusyoKingaku.Text.Replace(",", "").Trim())
            Return strHattyuu
        End Get
    End Property
#End Region

#Region "���ރR�[�h"
    ''' <summary>
    ''' ���ރR�[�h
    ''' </summary>
    ''' <value>���ރR�[�h</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BunruiCd() As String
        Get
            Return HiddenBunruiCd.Value
        End Get
        Set(ByVal value As String)
            HiddenBunruiCd.Value = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' �������@
    ''' </summary>
    ''' <remarks></remarks>
    Private _tysHouhou As String
    ''' <summary>
    ''' �������@
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tysHouhou() As String
        Get
            Return _tysHouhou
        End Get
        Set(ByVal value As String)
            _tysHouhou = value
        End Set
    End Property

    ''' <summary>
    ''' ������E�d���惊���N�̊O���A�N�Z�X�p
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSeikyuuSiireLink() As SeikyuuSiireLinkCtrl
        Get
            Return SeikyuuSiireLinkCtrl
        End Get
        Set(ByVal value As SeikyuuSiireLinkCtrl)
            SeikyuuSiireLinkCtrl = value
        End Set
    End Property

    ''' <summary>
    ''' ���ʑΉ��c�[���`�b�v�̊O���A�N�Z�X�p
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip() As TokubetuTaiouToolTipCtrl
        Get
            Return TokubetuTaiouToolTipCtrl
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            TokubetuTaiouToolTipCtrl = value
        End Set
    End Property

    ''' <summary>
    ''' ���ʑΉ��f�[�^�X�V�t���O�̊O���A�N�Z�X�p
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouUpdFlg() As HiddenField
        Get
            Return HiddenTokubetuTaiouUpdFlg
        End Get
        Set(ByVal value As HiddenField)
            HiddenTokubetuTaiouUpdFlg = value
        End Set
    End Property

    ''' <summary>
    ''' ���㏈���v���_�E���̊O���A�N�Z�X�p
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccUriageSyori() As DropDownList
        Get
            Return SelectUriageSyori
        End Get
        Set(ByVal value As DropDownList)
            SelectUriageSyori = value
        End Set
    End Property
#End Region

#Region "�C�x���g"

    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g<br/>
    ''' �������z�A�H���X�����z��e��ʂ֓n��
    ''' </summary>
    ''' <remarks></remarks>
    Public Event KingakuSetAction(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs, _
                                ByVal jituSeikyuuGaku As String, _
                                ByVal koumutenSeikyuuGaku As String)

    ''' <summary>
    ''' �˗��R���g���[���̉��i�ݒ胍�W�b�N��e��ʂɎ��s������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ExecKakakuSettei(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs)

    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g�i�����^�C�v�ݒ�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">�J�X�^���C�x���g������ID</param>
    ''' <param name="strSeikyuuSakiTypeStr">������^�C�v�i���ڐ���/�������j</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <remarks></remarks>
    Public Event SetSeikyuuTypeAction(ByVal sender As System.Object _
                                        , ByVal e As System.EventArgs _
                                        , ByVal strId As String _
                                        , ByVal strSeikyuuSakiTypeStr As String _
                                        , ByVal strKeiretuCd As String _
                                        , ByVal strKameitenCd As String)

    ''' <summary>
    ''' �˗��R���g���[���̒������@���v���p�e�B�ɐݒ肷��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="TyousaHouhouNo">�������@No</param>
    ''' <remarks></remarks>
    Public Event SetTysHouhouAction(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs, _
                                 ByRef TyousaHouhouNo As Integer)

    ''' <summary>
    ''' ���i�Q�C�R�̐e�R���g���[���ɐ�������̐ݒ��v������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event GetSeikyuuInfo(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs)

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
    ''' ����N�����̕ύX��e�R���g���[���ɒʒm����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ChangeUriageNengappi(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal uriagenengappi As String)

    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g�i������E�d������ݒ�p�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Public Event SetSeikyuuSiireSakiAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)

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

        If Not IsPostBack Then

            ' �v���p�e�B�̐ݒ�l�𔽉f
            SyouhinRecord.Attributes("class") = _cssName

            If _isRowSpacer Then
                TableSpacer.Style("display") = "inline"
            Else
                TableSpacer.Style("display") = "none"
            End If

            If _rowVisible = False Then
                SyouhinRecord.Style("display") = "none"
            End If

            ' ���[�h��ێ�����
            HiddenDispMode.Value = Me.DispMode

            ' ��\�����ڂɃf�t�H���g�l��ݒ�
            If HiddenSeikyuuType.Value = String.Empty Then
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu
            End If

            ' �h���b�v�_�E�����X�g�ɐݒ肷��A�C�e��
            Dim itemBlank As New ListItem   ' ��
            Dim itemAri As New ListItem     ' �L��
            Dim itemNasi As New ListItem    ' ����

            itemBlank.Text = ""
            itemBlank.Value = ""
            itemAri.Text = "�L"
            itemAri.Value = "1"
            itemNasi.Text = "��"
            itemNasi.Value = "0"

            ' �����L����ۑ�����
            Dim saveSeikyuuUmu As String = SelectSeikyuuUmu.SelectedValue

            ' �\�����[�h�Ő����L���h���b�v�_�E�����\�z����̂ŃA�C�e�����폜����
            SelectSeikyuuUmu.Items.Clear()

            TdSyoudakusyoKingaku.Attributes("class") = "boldBorderLeft"
            ' ��ʕ\���ݒ�
            Select Case Me.DispMode
                Case DisplayMode.SYOUHIN1
                    ' ���i�P�̏ꍇ
                    '��\���ݒ�
                    TextSyouhinCd.ReadOnly = True
                    TextSyouhinCd.BackColor = Drawing.Color.Transparent     ' �w�i�𓧉߂�����
                    TextSyouhinCd.Style("border-style") = "none"            ' ���i�R�[�h��\���X�^�C���ɕύX
                    TextSyouhinCd.Attributes("class") = "readOnlyStyle"     ' ���i�R�[�h��\���X�^�C���ɕύX
                    TextSyouhinCd.TabIndex = -1                             ' �^�u�t�H�[�J�X����
                    ButtonSyouhinKensaku.Style("display") = "none"          ' ���i�����{�^��
                    SelectKakutei.Style("display") = "none"                 ' �m��

                    ' �����L���h���b�v�_�E���A�C�e���ݒ�
                    SelectSeikyuuUmu.Items.Add(itemAri)
                    SelectSeikyuuUmu.Items.Add(itemNasi)
                    SelectSeikyuuUmu.SelectedValue = saveSeikyuuUmu

                Case DisplayMode.SYOUHIN2
                    ' ���i�Q�̏ꍇ
                    SelectKakutei.Style("display") = "none"                 ' �m��
                    HiddenTargetId.Value = "2"                              ' ���i�����p��ID
                    ' �����L���h���b�v�_�E���A�C�e���ݒ�
                    SelectSeikyuuUmu.Items.Add(itemAri)
                    SelectSeikyuuUmu.Items.Add(itemNasi)
                    SelectSeikyuuUmu.SelectedValue = saveSeikyuuUmu

                Case DisplayMode.SYOUHIN3
                    ' ���i�R�̏ꍇ
                    HiddenTargetId.Value = "3"                              ' ���i�����p��ID
                    ' �����L���h���b�v�_�E���A�C�e���ݒ�
                    SelectSeikyuuUmu.Items.Add(itemAri)
                    SelectSeikyuuUmu.Items.Add(itemNasi)
                    SelectSeikyuuUmu.SelectedValue = saveSeikyuuUmu

                Case DisplayMode.HOUKOKUSYO
                    ' �񍐏��̏ꍇ
                    '��\���ݒ�
                    TextSyouhinCd.ReadOnly = True
                    TextSyouhinCd.BackColor = Drawing.Color.Transparent     ' �w�i�𓧉߂�����
                    TextSyouhinCd.Style("border-style") = "none"            ' ���i�R�[�h��\���X�^�C���ɕύX
                    TextSyouhinCd.Attributes("class") = "readOnlyStyle"     ' ���i�R�[�h��\���X�^�C���ɕύX
                    TextSyouhinCd.TabIndex = -1                             ' �^�u�t�H�[�J�X����
                    ButtonSyouhinKensaku.Style("display") = "none"          ' ���i�����{�^��
                    SelectKakutei.Style("display") = "none"                 ' �m��
                    TdSpacer.Style("display") = "inline"                    ' �E�[�X�y�[�T�[

                    ' �����L���h���b�v�_�E���A�C�e���ݒ�
                    SelectSeikyuuUmu.Items.Add(itemBlank)
                    SelectSeikyuuUmu.Items.Add(itemAri)
                    SelectSeikyuuUmu.Items.Add(itemNasi)
                    SelectSeikyuuUmu.SelectedValue = saveSeikyuuUmu

                    ' ���������z�A�`�[�d���N�������\���ɂ���
                    TdSyoudakusyoKingaku.Visible = False
                    TdDenpyouSiireNengappi.Visible = False


                Case DisplayMode.HOSYOU
                    ' �ۏ؂̏ꍇ
                    '��\���ݒ�
                    TextSyouhinCd.ReadOnly = True
                    TextSyouhinCd.BackColor = Drawing.Color.Transparent     ' �w�i�𓧉߂�����
                    TextSyouhinCd.Style("border-style") = "none"            ' ���i�R�[�h��\���X�^�C���ɕύX
                    TextSyouhinCd.Attributes("class") = "readOnlyStyle"     ' ���i�R�[�h��\���X�^�C���ɕύX
                    TextSyouhinCd.TabIndex = -1                             ' �^�u�t�H�[�J�X����
                    ButtonSyouhinKensaku.Style("display") = "none"          ' ���i�����{�^��
                    SelectKakutei.Style("display") = "none"                 ' �m��
                    TdSpacer.Style("display") = "inline"                    ' �E�[�X�y�[�T�[

                    ' �����L���h���b�v�_�E���A�C�e���ݒ�
                    SelectSeikyuuUmu.Items.Add(itemBlank)
                    SelectSeikyuuUmu.Items.Add(itemAri)
                    SelectSeikyuuUmu.Items.Add(itemNasi)
                    SelectSeikyuuUmu.SelectedValue = saveSeikyuuUmu

                    ' ���������z�A�`�[�d���N�������\���ɂ���
                    TdSyoudakusyoKingaku.Visible = False
                    TdDenpyouSiireNengappi.Visible = False

                Case DisplayMode.KAIYAKU
                    ' �ۏ�(��񕥖�)�̏ꍇ
                    '��\���ݒ�
                    TextSyouhinCd.ReadOnly = True
                    TextSyouhinCd.BackColor = Drawing.Color.Transparent     ' �w�i�𓧉߂�����
                    TextSyouhinCd.Style("border-style") = "none"            ' ���i�R�[�h��\���X�^�C���ɕύX
                    TextSyouhinCd.Attributes("class") = "readOnlyStyle"     ' ���i�R�[�h��\���X�^�C���ɕύX
                    TextSyouhinCd.TabIndex = -1                             ' �^�u�t�H�[�J�X����
                    ButtonSyouhinKensaku.Style("display") = "none"          ' ���i�����{�^��
                    SpanSyouhinName.Style("display") = "none"               ' ���i��
                    SelectKakutei.Style("display") = "none"                 ' �m��
                    TdSpacer.Style("display") = "inline"                    ' �E�[�X�y�[�T�[

                    ' �����L���h���b�v�_�E���A�C�e���ݒ�
                    SelectSeikyuuUmu.Items.Add(itemBlank)
                    SelectSeikyuuUmu.Items.Add(itemAri)
                    SelectSeikyuuUmu.SelectedValue = saveSeikyuuUmu

                    ' ���������z�A�`�[�d���N�������\���ɂ���
                    TdSyoudakusyoKingaku.Visible = False
                    TdDenpyouSiireNengappi.Visible = False

                    HiddenHosyouMessage.Value = "0" '��񕥖ߐ\���L���ύX�t���O�̏�����

                Case Else

            End Select

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            Me.SetDispAction()

            RaiseEvent SetSeikyuuTypeAction(sender _
                                            , e _
                                            , Me.ClientID _
                                            , Me.SeikyuuSiireLinkCtrl.SeikyuuSakiTypeStr _
                                            , Me.KeiretuCd _
                                            , Me.KameitenCd)

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
            '��ʕ\�����_�̒l���AHidden�ɕێ�(���ʑΉ����i�Ή� �ύX�`�F�b�N�p)
            If Me.HiddenOpenValuesTokubetuTaiou.Value = String.Empty Then
                Me.HiddenOpenValuesTokubetuTaiou.Value = getCtrlValuesStringTokubetuTaiou()
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
    ''' �H���X�����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKoumutenSeikyuuGaku_TextChanded(ByVal sender As System.Object, _
                                                      ByVal e As System.EventArgs) Handles TextKoumutenSeikyuuGaku.TextChanged

        ' ���[�h�̎擾
        Dim mode As Integer = Integer.Parse(HiddenDispMode.Value)
        Dim strSeikyuuTypeStr As String

        '���ڐ����E���������f
        If Me.HiddenSeikyuuType.Value = "0" Or Me.HiddenSeikyuuType.Value = "2" Then
            strSeikyuuTypeStr = EarthConst.SEIKYU_TYOKUSETU
        Else
            strSeikyuuTypeStr = EarthConst.SEIKYU_TASETU
        End If

        RaiseEvent SetSeikyuuTypeAction(sender, e, Me.ClientID, strSeikyuuTypeStr, Me.HiddenKeiretuCd.Value, Me.HiddenKameitenCd.Value)

        ' ����������擾�i���ݒ莞�͒��ڐ����̌n��ȊO�ɂ��Ă����i�����X���͕K�{�Ȃ̂ōŏI�I�ɂ͎w�肳���j�j
        Dim seikyuuType As Integer = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu
        If HiddenSeikyuuType.Value.ToString.Trim() <> String.Empty Then
            seikyuuType = Integer.Parse(HiddenSeikyuuType.Value)
        End If

        Select Case mode
            Case DisplayMode.SYOUHIN1, _
                 DisplayMode.SYOUHIN2, _
                 DisplayMode.SYOUHIN3, _
                 DisplayMode.HOUKOKUSYO, _
                 DisplayMode.HOSYOU
                '***************************************
                ' ���i�P�E���i�Q�E���i�R �񍐏��E�ۏ؏�
                '***************************************

                ' ��ʂ̍H���X�������z�𐔒l�^�ɕϊ�
                Dim koumutengaku As Integer = 0
                If TextKoumutenSeikyuuGaku.Text.Trim() <> String.Empty Then
                    koumutengaku = Integer.Parse(TextKoumutenSeikyuuGaku.Text.Replace(",", ""))
                End If

                TextKoumutenSeikyuuGaku.Text = koumutengaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                ' ��������
                If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
                   HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then

                    '**************************************************
                    ' ���ڐ���
                    '**************************************************
                    ' ���H���X���z�����������z�ɐݒ�
                    TextKoumutenSeikyuuGaku.Text = koumutengaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                    TextJituSeikyuuGaku.Text = koumutengaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                Else
                    '**************************************************
                    ' �������i�n��j���n��ȊO�͓��͕s�ׁ̈A�֌W�Ȃ�
                    '**************************************************
                    If HiddenKingakuFlg.Value <> "2" Then

                        ' �񍐏��̏ꍇ�A�H���X���z���O�̏ꍇ�A��x�������������z�������ݒ肷��
                        If mode = DisplayMode.HOUKOKUSYO And _
                           koumutengaku = 0 And _
                           HiddenKingakuFlg.Value = "1" Then
                            ' �񍐏���1�x�̂�
                            Exit Sub
                        End If

                        Dim logic As New JibanLogic
                        Dim zeinukiGaku As Integer = 0

                        If logic.GetSeikyuuGaku(sender, _
                                                5, _
                                                HiddenKeiretuCd.Value, _
                                                TextSyouhinCd.Text, _
                                                koumutengaku, _
                                                zeinukiGaku) Then

                            ' ���������z�փZ�b�g
                            TextJituSeikyuuGaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                            ' �H���X�������z�Ǝ������z�̎����ݒ萧�䔻��p�t���O�Z�b�g
                            HiddenKingakuFlg.Value = "1"

                        End If
                    End If
                End If

                ' ���i�P�̏ꍇ�̂ݐ������z��e��ʂ֓�����
                If HiddenDispMode.Value = "1" Then
                    ' �e��ʂɕύX�f�[�^���Z�b�g
                    RaiseEvent KingakuSetAction(Me, e, TextJituSeikyuuGaku.Text, TextKoumutenSeikyuuGaku.Text)
                End If

                SetZeigaku(e)

                '�t�H�[�J�X�Z�b�g
                SetFocus(TextJituSeikyuuGaku)

            Case DisplayMode.KAIYAKU
                '***************************************
                ' ��񕥖�
                '***************************************
                ' ��������
                If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
                   HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then
                    ' ���ڐ����̏ꍇ�A�������z�ɃZ�b�g
                    TextJituSeikyuuGaku.Text = TextKoumutenSeikyuuGaku.Text
                    SetZeigaku(e)
                End If
            Case Else
        End Select

    End Sub

    ''' <summary>
    ''' �������z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextJituSeikyuuGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' ���[�h�̎擾
        Dim mode As Integer = Integer.Parse(HiddenDispMode.Value)

        Select Case mode
            Case DisplayMode.SYOUHIN1, _
                 DisplayMode.SYOUHIN2, _
                 DisplayMode.SYOUHIN3
                '***************************************
                ' ���i�P�E���i�Q�E���i�R 
                '***************************************
                RaiseEvent GetSeikyuuInfo(Me, e)

                ' ����������擾�i���ݒ莞�͒��ڐ����̌n��ȊO�ɂ��Ă����i�����X���͕K�{�Ȃ̂ōŏI�I�ɂ͎w�肳���j�j
                Dim seikyuuType As Integer

                If TextJituSeikyuuGaku.Text.Trim() = "" Then
                    SetZeigaku(e)
                    '�t�H�[�J�X�Z�b�g
                    SetFocus(TextDenpyouUriageNengappi)
                    Exit Sub
                End If

                Dim strSeikyuuTypeStr As String

                '���ڐ����E���������f
                If Me.HiddenSeikyuuType.Value = "0" Or Me.HiddenSeikyuuType.Value = "2" Then
                    strSeikyuuTypeStr = EarthConst.SEIKYU_TYOKUSETU
                Else
                    strSeikyuuTypeStr = EarthConst.SEIKYU_TASETU
                End If

                RaiseEvent SetSeikyuuTypeAction(sender, e, Me.ClientID, strSeikyuuTypeStr, Me.HiddenKeiretuCd.Value, Me.HiddenKameitenCd.Value)

                If HiddenSeikyuuType.Value = "" Then
                    Me.HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu
                    seikyuuType = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu
                Else
                    seikyuuType = Integer.Parse(HiddenSeikyuuType.Value)
                End If

                ' ��ʂ̎��������z�𐔒l�^�ɕϊ�
                Dim jitugaku As Integer = 0
                If TextJituSeikyuuGaku.Text.Trim() <> String.Empty Then
                    jitugaku = Integer.Parse(TextJituSeikyuuGaku.Text.Replace(",", ""))
                End If

                TextJituSeikyuuGaku.Text = jitugaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                ' ��������
                If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
                   HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then

                    '**************************************************
                    ' ���ڐ���
                    '**************************************************
                    ' �����������z���H���X���z�ɐݒ�
                    TextJituSeikyuuGaku.Text = jitugaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                    TextKoumutenSeikyuuGaku.Text = jitugaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                ElseIf HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuKeiretu Then
                    '**************************************************
                    ' �������i�n��j���n��ȊO�͐ݒ�Ȃ�
                    '**************************************************
                    If HiddenKingakuFlg.Value <> "1" Then

                        Dim koumutenGaku As Integer = 0
                        Dim logic As New JibanLogic

                        ' �����z�Z�o���\�b�h�ւ̈����ݒ�i���i�P�̏ꍇ�̂�6,����4�j
                        Dim param As Integer = IIf(mode = DisplayMode.SYOUHIN1, 6, 4)

                        ' �����z���Z�o����
                        If logic.GetSeikyuuGaku(sender, _
                                                param, _
                                                HiddenKeiretuCd.Value, _
                                                TextSyouhinCd.Text, _
                                                jitugaku, _
                                                koumutenGaku) Then

                            ' �H���X�������z�Z�b�g
                            TextKoumutenSeikyuuGaku.Text = koumutenGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                            ' �H���X�������z�Ǝ������z�̎����ݒ萧�䔻��p�t���O�Z�b�g
                            HiddenKingakuFlg.Value = "2"

                        End If
                    End If
                End If

                ' ���i�P�̏ꍇ�̂ݐ������z��e��ʂ֓�����
                If HiddenDispMode.Value = "1" Then
                    ' �e��ʂɕύX�f�[�^���Z�b�g
                    RaiseEvent KingakuSetAction(Me, e, TextJituSeikyuuGaku.Text, TextKoumutenSeikyuuGaku.Text)
                End If

                SetZeigaku(e)

                '�t�H�[�J�X�Z�b�g
                SetFocus(TextSyouhizeiGaku)

            Case DisplayMode.HOUKOKUSYO, _
                 DisplayMode.HOSYOU, _
                 DisplayMode.KAIYAKU
                '***************************************
                ' �񍐏��E�ۏ؏��E��񕥖�
                '***************************************
                ' ��������
                If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
                   HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then
                    '**************************************************
                    ' ���ڐ���
                    '**************************************************
                    ' ���H���X���z�Ɏ��������z��ݒ�
                    TextKoumutenSeikyuuGaku.Text = TextJituSeikyuuGaku.Text

                End If

                SetZeigaku(e)

                '�t�H�[�J�X�Z�b�g
                SetFocus(TextSyouhizeiGaku)

            Case Else
        End Select

    End Sub

    ''' <summary>
    ''' ����Ŋz�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSyouhizeiGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If TextSyouhizeiGaku.Text.Trim() = "" Then
            TextSyouhizeiGaku.Text = "0"
        End If

        SetZeigaku(e, True)

        '�t�H�[�J�X�Z�b�g
        SetFocus(TextDenpyouUriageNengappi)

    End Sub

    ''' <summary>
    ''' ���������z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSyoudakusyoKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If sender.Text.Trim() = "" Then
            sender.Text = "0"
        End If

        Dim jitugaku As Integer = Integer.Parse(sender.Text.Replace(",", ""))
        Dim zeigaku As Integer = 0
        If IsNumeric(Me.HiddenZeiritu.Value) = False Then
            Me.HiddenZeiritu.Value = "0"
        End If
        zeigaku = Fix(jitugaku * Decimal.Parse(HiddenZeiritu.Value))

        TextSiireSyouhizeiGaku.Text = zeigaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        '�t�H�[�J�X�Z�b�g
        SetFocus(TextDenpyouSiireNengappi)

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

        '�t�H�[�J�X�Z�b�g
        SetFocus(TextDenpyouSiireNengappi)

    End Sub

    ''' <summary>
    ''' �����L���ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectSeikyuuUmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' ���[�h�̎擾
        Dim mode As Integer = Integer.Parse(HiddenDispMode.Value)

        Select Case mode
            Case DisplayMode.SYOUHIN1
                '***************************************
                ' ���i�P
                '***************************************
                If SelectSeikyuuUmu.SelectedValue = "1" Then
                    ' �����L��̏ꍇ�A�˗��R���g���[���̉��i�ݒ�����s�i�e��ʂ����s�j
                    RaiseEvent ExecKakakuSettei(Me, e)
                Else
                    ' ���������̏ꍇ
                    TextKoumutenSeikyuuGaku.Text = "0"
                    TextJituSeikyuuGaku.Text = "0"
                    SetZeigaku(e)
                End If


            Case DisplayMode.SYOUHIN2, _
                 DisplayMode.SYOUHIN3
                '***************************************
                ' ���i�Q�E���i�R
                '***************************************
                If SelectSeikyuuUmu.SelectedValue = "1" Then
                    ' �����L��̏ꍇ�A���i�Č��������s
                    ButtonSyouhinKensaku_ServerClick(sender, e)

                Else
                    ' ���������̏ꍇ
                    TextKoumutenSeikyuuGaku.Text = "0"
                    TextJituSeikyuuGaku.Text = "0"
                    SetZeigaku(e)
                End If

            Case DisplayMode.HOUKOKUSYO, _
                     DisplayMode.HOSYOU, _
                     DisplayMode.KAIYAKU
                '***************************************
                ' �񍐏��E�ۏ؏��E��񕥖�
                '***************************************
                SetSyouhinEtc(sender, e)

        End Select

        HiddenSeikyuuUmuOld.Value = SelectSeikyuuUmu.SelectedValue

        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

        '�t�H�[�J�X�Z�b�g
        SetFocus(SelectSeikyuuUmu)

    End Sub

    ''' <summary>
    ''' ���㏈���ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectUriageSyori_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If SelectUriageSyori.SelectedValue = "0" Then
            ' ���㏈�������N���A
            TextUriageBi.Text = ""
        Else
            ' �V�X�e�����t���Z�b�g
            TextUriageBi.Text = Date.Now.ToString("yyyy/MM/dd")
        End If

        '�t�H�[�J�X�Z�b�g
        SetFocus(SelectUriageSyori)

    End Sub

    ''' <summary>
    ''' ���i�����{�^���������̃C�x���g�i���i�Q�E�R�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSyouhinKensaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' �������̏���
        SearchSyouhin23(sender, e)

    End Sub

    ''' <summary>
    ''' ���i�����{�^���i��\���j�������̃C�x���g�i���i�Q�E�R�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHiddenSyouhinKensaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' �������̏���
        SearchSyouhin23(sender, e, False)

    End Sub

    ''' <summary>
    ''' ���������z�ύX���̃C�x���g�n���h��
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
    ''' �������m��h���b�v�_�E���ύX���̃C�x���g�n���h��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectHattyuusyoKakutei_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' �����������m�菈��
        FunCheckHKakutei(1, sender)
        SetFocus(SelectHattyuusyoKakutei)

        ' Old�l�Ɍ��݂̒l���Z�b�g
        HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue
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
            strSyouhinCd = Me.TextSyouhinCd.Text

            ' ���i�R�[�h�ύX���AOld�ɉ��.���i�R�[�h���Z�b�g
            If HiddenSyouhinCdOld.Value <> TextSyouhinCd.Text Then
                HiddenSyouhinCdOld.Value = TextSyouhinCd.Text
            End If

            If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                ' �擾�����ŋ敪���Z�b�g
                Me.HiddenZeiKbn.Value = strZeiKbn
                Me.HiddenZeiritu.Value = strZeiritu

                ' �ŋ敪�E�ŗ����󔒂̏ꍇ�A�������Ŕ����z�Ə��������z�ɋ󔒂��Z�b�g
                If Me.HiddenZeiritu.Value = String.Empty OrElse Me.HiddenZeiritu.Value = String.Empty Then
                    Me.TextJituSeikyuuGaku.Text = ""
                    Me.TextSyoudakusyoKingaku.Text = ""
                End If

                ' �������̋��z�Đݒ�
                SetZeigaku(e)
                ' �d������Ŋz�ݒ�
                SetSiireZeigaku()
                ' �ō����z�ύX��e�R���g���[���ɒʒm����
                RaiseEvent ChangeZeikomiGaku(Me, e, CInt(IIf(TextZeikomiKingaku.Text = "", "0", TextZeikomiKingaku.Text)))
            End If
        Else
            '����N���������͂̏ꍇ

            ' �ŋ敪�E�ŗ����Ď擾

            strSyouhinCd = Me.TextSyouhinCd.Text
            If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                ' �擾�����ŋ敪���Z�b�g
                Me.HiddenZeiKbn.Value = strZeiKbn
                Me.HiddenZeiritu.Value = strZeiritu

                ' �ŋ敪�E�ŗ����󔒂̏ꍇ�A�������Ŕ����z�Ə��������z�ɋ󔒂��Z�b�g
                If Me.HiddenZeiritu.Value = String.Empty OrElse Me.HiddenZeiritu.Value = String.Empty Then
                    Me.TextJituSeikyuuGaku.Text = ""
                    Me.TextSyoudakusyoKingaku.Text = ""
                End If

                ' �������̋��z�Đݒ�
                SetZeigaku(e)
                ' �d������Ŋz�ݒ�
                SetSiireZeigaku()
                ' �ō����z�ύX��e�R���g���[���ɒʒm����
                RaiseEvent ChangeZeikomiGaku(Me, e, CInt(IIf(TextZeikomiKingaku.Text = "", "0", TextZeikomiKingaku.Text)))
            End If
        End If

        SetFocus(TextDenpyouUriageNengappi)

    End Sub

    ''' <summary>
    ''' �`�[����N�����ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextDenpyouUriageNengappi_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        '�`�[����N����
        If Me.TextDenpyouUriageNengappi.Text <> String.Empty Then
            '���ߓ��̐ݒ�
            Me.SeikyuuSiireLinkCtrl.SetSeikyuuSimeDate()
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
    ''' ���i�Q�C�R�����{�^���������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SearchSyouhin23(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs, _
                                Optional ByVal ProcType As Boolean = True)

        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^���擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim SyouhinSearchLogic As New SyouhinSearchLogic
        Dim total_row As Integer

        ' ���[�h�̎擾
        Dim mode As Integer = Integer.Parse(HiddenDispMode.Value)

        ' �������@�̎擾
        Dim TyousaHouhouNo As Integer = Integer.MinValue
        RaiseEvent SetTysHouhouAction(Me, e, TyousaHouhouNo)

        '���i�Q�C�R���������s
        Dim dataArray As List(Of Syouhin23Record) = SyouhinSearchLogic.GetSyouhinInfo(TextSyouhinCd.Text, _
                                                                                      "", _
                                                                                      (mode = DisplayMode.SYOUHIN2), _
                                                                                      total_row, _
                                                                                      TyousaHouhouNo)

        If dataArray.Count = 1 Then
            '���i������ʂɃZ�b�g
            Dim recData As Syouhin23Record = dataArray(0)

            '�t�H�[�J�X�Z�b�g
            SetFocus(ButtonSyouhinKensaku)
        ElseIf ProcType = True Then
            '�����|�b�v�A�b�v���N��

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            Dim tmpScript = "callSearch('" & TextSyouhinCd.ClientID & EarthConst.SEP_STRING & _
                                        HiddenTargetId.ClientID & _
                                        "','" & UrlConst.SEARCH_SYOUHIN & "','" & _
                                        TextSyouhinCd.ClientID & "','" & _
                                        ButtonSyouhinKensaku.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)

            Exit Sub

        End If

        '���i�Q�C�R�ݒ�
        SetSyouhin23(sender, e, ProcType)

        ' ���i�R�[�h��ۑ�
        HiddenSyouhinCdOld.Value = TextSyouhinCd.Text

        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

    End Sub

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

        '�H���X�����Ŕ����z
        TextKoumutenSeikyuuGaku.Attributes("onfocus") = onFocusPostBackScript
        TextKoumutenSeikyuuGaku.Attributes("onblur") = onBlurPostBackScript
        TextKoumutenSeikyuuGaku.Attributes("onkeydown") = disabledOnkeydown
        '�������Ŕ����z
        TextJituSeikyuuGaku.Attributes("onfocus") = onFocusPostBackScript
        TextJituSeikyuuGaku.Attributes("onblur") = onBlurPostBackScript
        TextJituSeikyuuGaku.Attributes("onkeydown") = disabledOnkeydown
        '����Ŋz
        TextSyouhizeiGaku.Attributes("onfocus") = onFocusPostBackScript
        TextSyouhizeiGaku.Attributes("onblur") = onBlurPostBackScript
        TextSyouhizeiGaku.Attributes("onkeydown") = disabledOnkeydown
        '���i�R�[�h
        TextSyouhinCd.Attributes("onfocus") = "setTempValueForOnBlur(this);"
        TextSyouhinCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(this.value=='')objEBI('" & ButtonHiddenSyouhinKensaku.ClientID & "').click();}"
        TextSyouhinCd.Attributes("onkeydown") = disabledOnkeydown
        '���������z
        TextHattyuusyoKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextHattyuusyoKingaku.Attributes("onblur") = onBlurPostBackScript
        TextHattyuusyoKingaku.Attributes("onkeydown") = disabledOnkeydown
        '���������z
        TextSyoudakusyoKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextSyoudakusyoKingaku.Attributes("onblur") = onBlurPostBackScript
        TextSyoudakusyoKingaku.Attributes("onkeydown") = disabledOnkeydown
        '�d������Ŋz
        TextSiireSyouhizeiGaku.Attributes("onfocus") = onFocusPostBackScript
        TextSiireSyouhizeiGaku.Attributes("onblur") = onBlurPostBackScript
        TextSiireSyouhizeiGaku.Attributes("onkeydown") = disabledOnkeydown
        '���Ϗ��쐬��
        TextMitumorisyoSakuseibi.Attributes("onblur") = checkDate
        TextMitumorisyoSakuseibi.Attributes("onkeydown") = disabledOnkeydown
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

        ' ���i�R�[�h���ݒ莞�A�����Z�b�g���Ȃ�
        If data.SyouhinCd Is Nothing Then
            Return
        ElseIf data.SyouhinCd = "" Then
            Return
        End If

        ' �敪
        HiddenKubun.Value = data.Kbn
        ' �ۏ؏�NO
        HiddenBangou.Value = data.HosyousyoNo
        ' ���i�R�[�h
        TextSyouhinCd.Text = data.SyouhinCd
        HiddenSyouhinCdOld.Value = data.SyouhinCd
        ' ���i��
        SpanSyouhinName.Text = data.SyouhinMei
        ' ���i�R�̏ꍇ�̂݊m��敪
        If Me.DispMode = DisplayMode.SYOUHIN3 Then
            SelectKakutei.SelectedValue = data.KakuteiKbn
        End If
        ' �H���X�����z
        TextKoumutenSeikyuuGaku.Text = IIf(data.KoumutenSeikyuuGaku = Integer.MinValue, _
                                           0, _
                                           data.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' ���������z
        TextJituSeikyuuGaku.Text = IIf(data.UriGaku = Integer.MinValue, _
                                       0, _
                                       data.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1))

        If TextJituSeikyuuGaku.Text <> "" Then

            ' ����Ŋz
            TextSyouhizeiGaku.Text = IIf(data.UriageSyouhiZeiGaku = Integer.MinValue, _
                                       0, _
                                       data.UriageSyouhiZeiGaku.ToString(EarthConst.FORMAT_KINGAKU_1))

            ' �ō����z
            TextZeikomiKingaku.Text = Format(data.ZeikomiUriGaku, EarthConst.FORMAT_KINGAKU_1)

        End If

        ' ���������z
        TextSyoudakusyoKingaku.Text = IIf(data.SiireGaku = Integer.MinValue, _
                                          0, _
                                          data.SiireGaku.ToString(EarthConst.FORMAT_KINGAKU_1))

        If TextSyoudakusyoKingaku.Text <> "" Then
            ' �d������Ŋz
            TextSiireSyouhizeiGaku.Text = IIf(data.SiireSyouhiZeiGaku = Integer.MinValue, _
                                       0, _
                                       data.SiireSyouhiZeiGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        End If

        ' ���Ϗ��쐬��
        If Not data.TysMitsyoSakuseiDate = Nothing Then
            TextMitumorisyoSakuseibi.Text = IIf(data.TysMitsyoSakuseiDate = DateTime.MinValue, _
                                                "", _
                                                data.TysMitsyoSakuseiDate.ToString("yyyy/MM/dd"))
        End If

        ' ���������s��
        If Not data.SeikyuusyoHakDate = Nothing Then
            TextSeikyuusyoHakkoubi.Text = IIf(data.SeikyuusyoHakDate = DateTime.MinValue, _
                                              "", _
                                              data.SeikyuusyoHakDate.ToString("yyyy/MM/dd"))
        End If

        ' ����N����
        If Not data.UriDate = Nothing Then
            TextUriageNengappi.Text = IIf(data.UriDate = DateTime.MinValue, _
                                          "", _
                                          data.UriDate.ToString("yyyy/MM/dd"))
        End If

        ' �`�[����N����(�Q�Ɨp)
        If Not data.DenpyouUriDate = Nothing Then
            TextDenpyouUriageNengappiDisplay.Text = IIf(data.DenpyouUriDate = Date.MinValue, _
                                                        "", _
                                                        data.DenpyouUriDate.ToString("yyyy/MM/dd"))
        End If
        ' �`�[����N����(�C���p)
        If Not data.DenpyouUriDate = Nothing Then
            TextDenpyouUriageNengappi.Text = IIf(data.DenpyouUriDate = Date.MinValue, _
                                                 "", _
                                                 data.DenpyouUriDate.ToString("yyyy/MM/dd"))
        End If

        ' �`�[�d���N����(�Q�Ɨp)
        If Not data.DenpyouSiireDate = Nothing Then
            TextDenpyouSiireNengappiDisplay.Text = IIf(data.DenpyouSiireDate = Date.MinValue, _
                                                        "", _
                                                        data.DenpyouSiireDate.ToString("yyyy/MM/dd"))
        End If
        ' �`�[�d���N����(�C���p)
        If Not data.DenpyouSiireDate = Nothing Then
            TextDenpyouSiireNengappi.Text = IIf(data.DenpyouSiireDate = Date.MinValue, _
                                                 "", _
                                                 data.DenpyouSiireDate.ToString("yyyy/MM/dd"))
        End If

        ' �����L��
        SelectSeikyuuUmu.SelectedValue = data.SeikyuuUmu
        HiddenSeikyuuUmuOld.Value = SelectSeikyuuUmu.SelectedValue

        ' ���㏈��
        SelectUriageSyori.SelectedValue = data.UriKeijyouFlg

        ' ����v���
        If Not data.UriKeijyouDate = Nothing Then
            TextUriageBi.Text = IIf(data.UriKeijyouDate = DateTime.MinValue, _
                                    "", _
                                    data.UriKeijyouDate.ToString("yyyy/MM/dd"))
        End If

        ' ���������z
        TextHattyuusyoKingaku.Text = IIf(data.HattyuusyoGaku = Integer.MinValue, _
                                         "", _
                                         data.HattyuusyoGaku.ToString("###,###,###"))
        ' ���������z(��ʋN�����̒l)
        HiddenHattyuusyoKingakuOld.Value = IIf(data.HattyuusyoGaku = Integer.MinValue, _
                                               "", _
                                               data.HattyuusyoGaku.ToString("###,###,###"))

        ' �������m��
        SelectHattyuusyoKakutei.SelectedValue = data.HattyuusyoKakuteiFlg

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
        If Not data.HattyuusyoKakuninDate = Nothing Then
            TextHattyuusyoKakuninbi.Text = IIf(data.HattyuusyoKakuninDate = DateTime.MinValue, _
                                               "", _
                                               data.HattyuusyoKakuninDate.ToString("yyyy/MM/dd"))
        End If

        ' �����z
        HiddenNyuukinGaku.Value = _
            IIf(data.SiireGaku = Integer.MinValue, 0, data.UriGaku.ToString())

        ' �ŗ�
        HiddenZeiritu.Value = _
            IIf(data.Zeiritu = Decimal.MinValue, 0, data.Zeiritu.ToString())

        ' ��ʕ\��NO
        HiddenGamenHyoujiNo.Value = data.GamenHyoujiNo.ToString()

        ' ���ރR�[�h
        HiddenBunruiCd.Value = data.BunruiCd

        ' �ŋ敪
        HiddenZeiKbn.Value = data.ZeiKbn

        ' �X�V����
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
        If TextSyouhinCd.Text = "" Then
            Return Nothing
        End If

        ' �@�ʐ������R�[�h
        Dim record As New TeibetuSeikyuuRecord
        ' �敪
        record.Kbn = HiddenKubun.Value
        ' �ۏ؏�NO
        record.HosyousyoNo = HiddenBangou.Value

        ' ���i�R�[�h
        record.SyouhinCd = TextSyouhinCd.Text
        ' ���i��
        record.SyouhinMei = SpanSyouhinName.Text
        ' ���i�R�̏ꍇ�̂݊m��敪
        If Me.DispMode = DisplayMode.SYOUHIN3 Then
            record.KakuteiKbn = SelectKakutei.SelectedValue
        Else
            record.KakuteiKbn = Integer.MinValue
        End If
        ' �H���X�����z
        Dim strKoumutenGaku As String = TextKoumutenSeikyuuGaku.Text.Replace(",", "").Trim()

        If strKoumutenGaku.Trim() = "" Then
            record.KoumutenSeikyuuGaku = Integer.MinValue
        Else
            record.KoumutenSeikyuuGaku = Integer.Parse(strKoumutenGaku)
        End If
        ' ���������z
        Dim strUriGaku As String = TextJituSeikyuuGaku.Text.Replace(",", "").Trim()
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
        Dim strSyouhizeiGaku As String = TextSyouhizeiGaku.Text.Replace(",", "").Trim()
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
        If Me.DispMode = DisplayMode.SYOUHIN1 Or _
           Me.DispMode = DisplayMode.SYOUHIN2 Or _
           Me.DispMode = DisplayMode.SYOUHIN3 Then
            Dim strSiireGaku As String = TextSyoudakusyoKingaku.Text.Replace(",", "").Trim()
            If strSiireGaku.Trim() = "" Then
                record.SiireGaku = Integer.MinValue
            Else
                record.SiireGaku = Integer.Parse(strSiireGaku)
            End If
        Else
            record.SiireGaku = 0
        End If
        ' ���Ϗ��쐬��
        If Not TextMitumorisyoSakuseibi.Text.Trim() = "" Then
            record.TysMitsyoSakuseiDate = Date.Parse(TextMitumorisyoSakuseibi.Text)
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
        If Not TextUriageBi.Text.Trim() = "" Then
            record.UriKeijyouDate = Date.Parse(TextUriageBi.Text)
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
        HiddenGamenHyoujiNo.Value = IIf(HiddenGamenHyoujiNo.Value = "", "1", HiddenGamenHyoujiNo.Value)
        record.GamenHyoujiNo = Integer.Parse(HiddenGamenHyoujiNo.Value)
        ' ���ރR�[�h
        record.BunruiCd = HiddenBunruiCd.Value

        ' �X�V����
        If HiddenUpdDatetime.Value <> "" Then
            record.UpdDatetime = DateTime.Parse(HiddenUpdDatetime.Value)
        Else
            record.UpdDatetime = DateTime.MinValue
        End If
        ' �X�V�҂h�c
        record.UpdLoginUserId = HiddenLoginUserId.Value

        '������/�d���惊���N�̏���@�ʐ������R�[�h�փZ�b�g
        Me.SeikyuuSiireLinkCtrl.SetTeibetuRecFromSeikyuuSiireLink(record)

        Return record

    End Function
#End Region

#Region "���i�Q�E�R�ݒ�"
    ''' <summary>
    ''' ���i�Q�E�R���̐ݒ�i���i�R�[�h���m�肵�Ă����Ԃ�Call����j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="ProcType"></param>
    ''' <remarks></remarks>
    Public Sub SetSyouhin23(ByVal sender As Object, ByVal e As System.EventArgs, _
                                Optional ByVal ProcType As Boolean = True)

        Dim strZeiKbn As String = String.Empty      '�ŋ敪
        Dim strZeiritu As String = String.Empty     '�ŗ�
        Dim strSyouhinCd As String = String.Empty   '���i�R�[�h

        Dim syouhinCd As String = TextSyouhinCd.Text

        If TextSyouhinCd.Text = String.Empty Then

            Dim hatyuusyoKingaku As String = IIf(TextHattyuusyoKingaku.Text = "", "0", TextHattyuusyoKingaku.Text)

            ' ���������z���O���󔒂̏ꍇ�A���ɖ߂��ď����I��
            If hatyuusyoKingaku <> "0" Then

                TextSyouhinCd.Text = HiddenSyouhinCdOld.Value

                If ProcType = False Then
                    ' �N���A�ł��Ȃ����b�Z�[�W
                    ScriptManager.RegisterClientScriptBlock(sender, _
                                                            sender.GetType(), _
                                                            "alert", _
                                                            "alert('" & _
                                                            Messages.MSG010E & _
                                                            "')", True)
                End If

                SetFocus(TextSyouhinCd)

                Exit Sub
            End If

            '���i�R�[�h����̏ꍇ�A�s���N���A���ď����I��
            EnabledCtrl(False)

            Exit Sub
        Else
            ' �R���g���[����������
            EnabledCtrl(True)
        End If

        '�����X�R�[�h�̎擾
        Me.KameitenCd = Me.SeikyuuSiireLinkCtrl.AccKameitenCd.Value

        ' ���擾�p�̃p�����[�^�N���X
        Dim syouhin23_info As New Syouhin23InfoRecord

        ' ���i�̊�{�����擾
        Dim syouhin23_rec As Syouhin23Record = getSyouhinInfo(IIf(mode = DisplayMode.SYOUHIN2, "2", "3"), TextSyouhinCd.Text)

        If syouhin23_rec Is Nothing Then
            '���i�R�[�h����̏ꍇ�A�s���N���A���ď����I��
            EnabledCtrl(False)
            Exit Sub
        End If

        ' �R�[�h�A���̂��Z�b�g
        TextSyouhinCd.Text = syouhin23_rec.SyouhinCd
        HiddenBunruiCd.Value = syouhin23_rec.SoukoCd
        SpanSyouhinName.Text = syouhin23_rec.SyouhinMei

        '�����悪�ʂɎw�肳��Ă���ꍇ�A�f�t�H���g�̐�������㏑��
        If Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value <> String.Empty Then
            syouhin23_rec.SeikyuuSakiCd = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value
            syouhin23_rec.SeikyuuSakiBrc = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc.Value
            syouhin23_rec.SeikyuuSakiKbn = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn.Value
        End If

        ' �@�ʐ������擾�p�̃��W�b�N�N���X
        Dim logic As New JibanLogic

        ' ���i�R�[�h�y�щ�ʂ̏����Z�b�g
        syouhin23_info.Syouhin2Rec = syouhin23_rec                                                  ' ���i�̊�{���
        syouhin23_info.SeikyuuUmu = SelectSeikyuuUmu.SelectedValue                                  ' �����L��
        syouhin23_info.HattyuusyoKakuteiFlg = Integer.Parse(SelectHattyuusyoKakutei.SelectedValue)  ' �������m��t���O

        syouhin23_info.KeiretuCd = HiddenKeiretuCd.Value                 ' �n��R�[�h
        syouhin23_info.KeiretuFlg = GetKeiretuFlg(HiddenKeiretuCd.Value) ' �n��t���O

        If syouhin23_info.Syouhin2Rec.SyouhinCd IsNot Nothing Then

            '�����^�C�v�̐ݒ�
            RaiseEvent SetSeikyuuTypeAction(Me _
                                            , e _
                                            , Me.ClientID _
                                            , syouhin23_info.Syouhin2Rec.SeikyuuSakiType _
                                            , syouhin23_info.KeiretuCd _
                                            , Me.KameitenCd)
        End If

        '������^�C�v�i���ڐ���/�������j�̐ݒ�
        syouhin23_info.Seikyuusaki = syouhin23_info.Syouhin2Rec.SeikyuuSakiType

        ' �������R�[�h�̎擾(�m���Ɍ��ʂ��L��)
        Dim teibetu_seikyuu_rec As TeibetuSeikyuuRecord = getSyouhin23SeikyuuInfo(sender, syouhin23_info)

        '����N�����Ŕ��f���āA�������ŗ����擾����
        strSyouhinCd = TextSyouhinCd.Text
        If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '�擾�����ŋ敪�E�ŗ����Z�b�g
            teibetu_seikyuu_rec.ZeiKbn = strZeiKbn
            teibetu_seikyuu_rec.Zeiritu = strZeiritu
            syouhin23_info.Syouhin2Rec.Zeiritu = strZeiritu

        End If

        ' �R���g���[����������
        EnabledCtrl(True)

        ' ���i�����Z�b�g
        TextKoumutenSeikyuuGaku.Text = teibetu_seikyuu_rec.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        TextJituSeikyuuGaku.Text = teibetu_seikyuu_rec.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        TextSyouhizeiGaku.Text = (Fix(teibetu_seikyuu_rec.UriGaku * syouhin23_info.Syouhin2Rec.Zeiritu)).ToString(EarthConst.FORMAT_KINGAKU_1)
        TextZeikomiKingaku.Text = teibetu_seikyuu_rec.UriGaku + Fix(teibetu_seikyuu_rec.UriGaku * syouhin23_info.Syouhin2Rec.Zeiritu).ToString(EarthConst.FORMAT_KINGAKU_1)

        ' �@�ʃf�[�^�C���͏��������z�������ݒ肵�Ȃ�
        ' TextSyoudakusyoKingaku.Text = teibetu_seikyuu_rec.SiireGaku.ToString(EarthConst.FORMAT_KINGAKU1)
        HiddenZeiKbn.Value = teibetu_seikyuu_rec.ZeiKbn
        HiddenZeiritu.Value = syouhin23_rec.Zeiritu
        HiddenKingakuFlg.Value = ""
        If HiddenHattyuusyoKingakuOld.Value <> "1" Then
            HiddenHattyuusyoKingakuOld.Value = ""
        End If

        ' ���z�Đݒ�
        SetZeigaku(e)
        ' �����i�d���j�Ŋz�Đݒ�
        SetSiireZeigaku()
    End Sub

    ''' <summary>
    ''' ���i�Q�A�R��ʕ\���p�̏��i�����擾���܂�
    ''' </summary>
    ''' <param name="syouhin_type">���i�Qor�R</param>
    ''' <param name="syouhin_cd">���i�R�[�h</param>
    ''' <returns>�@�ʐ������R�[�h</returns>
    ''' <remarks></remarks>
    Private Function getSyouhinInfo(ByVal syouhin_type As String, _
                                    ByVal syouhin_cd As String) As Syouhin23Record

        Dim syouhin23_rec As Syouhin23Record = Nothing

        ' ���擾�p�̃��W�b�N�N���X
        Dim logic As New JibanLogic
        Dim count As Integer = 0

        ' �������@�̎擾
        Dim TyousaHouhouNo As Integer = Integer.MinValue
        RaiseEvent SetTysHouhouAction(Me, New EventArgs, TyousaHouhouNo)

        ' ���i�����擾����i�R�[�h�w��Ȃ̂łP���̂ݎ擾�����j
        Dim list As List(Of Syouhin23Record) = logic.GetSyouhin23(syouhin_cd, _
                                                                  "", _
                                                                  IIf(syouhin_type = "2", EarthEnum.EnumSyouhinKubun.Syouhin2_110, EarthEnum.EnumSyouhinKubun.Syouhin3), _
                                                                  count, _
                                                                  TyousaHouhouNo, _
                                                                  KameitenCd)

        ' �擾�ł��Ȃ��ꍇ
        If list.Count < 1 Then
            Return syouhin23_rec
        End If

        ' �擾�ł����ꍇ�̂݃Z�b�g
        syouhin23_rec = list(0)

        Return syouhin23_rec

    End Function

    ''' <summary>
    ''' ���i�Q�A�R��ʕ\���p�̓@�ʐ����f�[�^���擾���܂�
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="syouhin23_info">���i�Q�C�R���擾�p�̃p�����[�^�N���X</param>
    ''' <returns>�@�ʐ������R�[�h</returns>
    ''' <remarks></remarks>
    Private Function getSyouhin23SeikyuuInfo(ByVal sender As Object, _
                                             ByVal syouhin23_info As Syouhin23InfoRecord _
                                             ) As TeibetuSeikyuuRecord

        Dim teibetu_rec As TeibetuSeikyuuRecord = Nothing

        ' ���擾�p�̃��W�b�N�N���X
        Dim logic As New JibanLogic

        ' �����f�[�^�̎擾
        teibetu_rec = logic.GetSyouhin23SeikyuuData(sender, syouhin23_info, 1)

        Return teibetu_rec

    End Function

#End Region
    ''' <summary>
    ''' �񍐏��E�ۏ؏��E��񕥖߂̏��i���Z�b�g����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="blnLinkRefresh">�����N�X�V���f�t���O</param>
    ''' <remarks></remarks>
    Public Sub SetSyouhinEtc(ByVal sender As System.Object, ByVal e As System.EventArgs, _
                             Optional ByVal blnLinkRefresh As Boolean = False)
        Dim enabled As Boolean = True

        Dim strZeiKbn As String = String.Empty      '�ŋ敪
        Dim strZeiritu As String = String.Empty     '�ŗ�
        Dim strSyouhinCd As String = String.Empty   '���i�R�[�h

        If SelectSeikyuuUmu.SelectedValue = "" Then

            Dim hatyuusyoKingaku As String = IIf(TextHattyuusyoKingaku.Text = "", "0", TextHattyuusyoKingaku.Text)

            ' ���������z���O���󔒂̏ꍇ�A���ɖ߂��ď����I��
            If hatyuusyoKingaku <> "0" Then

                SelectSeikyuuUmu.SelectedValue = HiddenSeikyuuUmuOld.Value
                ' �N���A�ł��Ȃ����b�Z�[�W
                ScriptManager.RegisterClientScriptBlock(sender, _
                                                        sender.GetType(), _
                                                        "alert", _
                                                        "alert('" & _
                                                        Messages.MSG010E & _
                                                        "')", True)
                Exit Sub
            End If

            enabled = False

            Dim hattyuuKingaku As String = IIf(TextHattyuusyoKingaku.Text = "", _
                                               "0", _
                                               TextHattyuusyoKingaku.Text)

            ' �������z���͍ς݂̏ꍇ�A�N���A���Ȃ�
            If hattyuuKingaku > "0" Then
                ' �����L�ɖ߂�
                SelectSeikyuuUmu.SelectedValue = "1"
                enabled = True
            End If
        ElseIf SelectSeikyuuUmu.SelectedValue = "0" Then

            ' �����͕񍐏��ۏ؏��݂̂ŋ��z��0�N���A����
            TextJituSeikyuuGaku.Text = "0"
            TextKoumutenSeikyuuGaku.Text = "0"
            SetZeigaku(e)
        End If

        Dim logic As New TeibetuSyuuseiLogic
        Dim syouhinRec As New SyouhinMeisaiRecord
        Dim strKameitenCd As String = Me.SeikyuuSiireLinkCtrl.AccKameitenCd.Value

        ' ���i�R�[�h���Đݒ�(�󔒈ȊO)
        If SelectSeikyuuUmu.SelectedValue = "0" Or _
           SelectSeikyuuUmu.SelectedValue = "1" Then

            Select Case mode
                Case DisplayMode.HOUKOKUSYO
                    If HiddenBunruiCd.Value = EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo Then
                        syouhinRec = logic.GetSyouhinInfo("" _
                                                        , EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo _
                                                        , strKameitenCd)
                        '����N�����Ŕ��f���āA�������ŗ����擾����
                        strSyouhinCd = syouhinRec.SyouhinCd
                        If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                            '�擾�����ŋ敪�E�ŗ����Z�b�g
                            syouhinRec.ZeiKbn = strZeiKbn
                            syouhinRec.Zeiritu = strZeiritu
                        End If

                        HiddenZeiritu.Value = syouhinRec.Zeiritu
                        HiddenZeiKbn.Value = syouhinRec.ZeiKbn

                    Else
                        syouhinRec = logic.GetSyouhinInfo("" _
                                                        , EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo _
                                                        , strKameitenCd)
                        '����N�����Ŕ��f���āA�������ŗ����擾����
                        strSyouhinCd = syouhinRec.SyouhinCd
                        If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                            '�擾�����ŋ敪�E�ŗ����Z�b�g
                            syouhinRec.ZeiKbn = strZeiKbn
                            syouhinRec.Zeiritu = strZeiritu
                        End If

                        HiddenZeiritu.Value = syouhinRec.Zeiritu
                        HiddenZeiKbn.Value = syouhinRec.ZeiKbn

                    End If

                Case DisplayMode.HOSYOU
                    syouhinRec = logic.GetSyouhinInfo("" _
                                                    , EarthEnum.EnumSyouhinKubun.Hosyousyo _
                                                    , strKameitenCd)
                    '����N�����Ŕ��f���āA�������ŗ����擾����
                    strSyouhinCd = syouhinRec.SyouhinCd
                    If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                        '�擾�����ŋ敪�E�ŗ����Z�b�g
                        syouhinRec.ZeiKbn = strZeiKbn
                        syouhinRec.Zeiritu = strZeiritu
                    End If

                    HiddenZeiritu.Value = syouhinRec.Zeiritu
                    HiddenZeiKbn.Value = syouhinRec.ZeiKbn

                Case DisplayMode.KAIYAKU
                    syouhinRec = logic.GetSyouhinInfo("" _
                                                    , EarthEnum.EnumSyouhinKubun.Kaiyaku _
                                                    , strKameitenCd)
                    '����N�����Ŕ��f���āA�������ŗ����擾����
                    strSyouhinCd = syouhinRec.SyouhinCd
                    If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                        '�擾�����ŋ敪�E�ŗ����Z�b�g
                        syouhinRec.ZeiKbn = strZeiKbn
                        syouhinRec.Zeiritu = strZeiritu
                    End If

                    HiddenZeiritu.Value = syouhinRec.Zeiritu
                    HiddenZeiKbn.Value = syouhinRec.ZeiKbn

                    ' ���͕W�����i���ɉ�񕥖߂����i��ݒ肷��
                    syouhinRec.HyoujunKkk = _
                        logic.GetKaiyakuKakaku(HiddenKameitenCd.Value, HiddenKubun.Value)
            End Select

            '���i��
            TextSyouhinCd.Text = syouhinRec.SyouhinCd
            SpanSyouhinName.Text = syouhinRec.SyouhinMei

        End If

        '�����X�֘A���̃Z�b�g
        syouhinRec.KameitenCdDisp = strKameitenCd
        syouhinRec.KeiretuCd = Me.KeiretuCd

        '�����悪�ʂɎw�肳��Ă���ꍇ�A�f�t�H���g�̐�������㏑��
        If Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value <> String.Empty Then
            syouhinRec.SeikyuuSakiCdDisp = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value
            syouhinRec.SeikyuuSakiBrcDisp = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc.Value
            syouhinRec.SeikyuuSakiKbnDisp = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn.Value
        End If

        If syouhinRec IsNot Nothing Then
            '�����^�C�v�̐ݒ�
            RaiseEvent SetSeikyuuTypeAction(Me _
                                            , e _
                                            , Me.ClientID _
                                            , syouhinRec.SeikyuuSakiType _
                                            , syouhinRec.KeiretuCd _
                                            , syouhinRec.KameitenCdDisp)
        End If

        ' �R���g���[���̊�������
        EnabledCtrl(enabled)

        ' �������z�̐ݒ�(��񕥖߈ȊO)
        If mode <> DisplayMode.KAIYAKU And _
           SelectSeikyuuUmu.SelectedValue = "1" Then
            ' ��������
            If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
               HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then
                '**************************************************
                ' ���ڐ���
                '**************************************************
                ' ���W�����i�����������z�E�H���X���z�ɐݒ�
                TextKoumutenSeikyuuGaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)
                TextJituSeikyuuGaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                SetZeigaku(e)
            ElseIf HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuKeiretu Then
                '**************************************************
                ' �������i�n��j
                '**************************************************
                ' �W�����i���H���X���z�ɐݒ�
                TextKoumutenSeikyuuGaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                ' �������z���擾����
                If TextKoumutenSeikyuuGaku.Text = "0" Then
                    TextJituSeikyuuGaku.Text = "0"
                Else
                    Dim jibanLogic As New JibanLogic
                    Dim zeinukiGaku As Integer = 0

                    If jibanLogic.GetSeikyuuGaku(sender, _
                                            3, _
                                            HiddenKeiretuCd.Value, _
                                            TextSyouhinCd.Text, _
                                            syouhinRec.HyoujunKkk, _
                                            zeinukiGaku) Then

                        ' ���������z�փZ�b�g
                        TextJituSeikyuuGaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                    End If
                End If

                SetZeigaku(e)

            ElseIf HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuNotKeiretu Then
                '**************************************************
                ' �������i�n��ȊO�j
                '**************************************************
                ' �H���X�����z�͂O
                TextKoumutenSeikyuuGaku.Text = "0"
                ' �������z�͕W�����i
                TextJituSeikyuuGaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                SetZeigaku(e)
            End If

        ElseIf mode = DisplayMode.KAIYAKU And _
               SelectSeikyuuUmu.SelectedValue = "1" Then

            '���i�ݒ胁�\�b�h
            SetKaiyakuSyouhin(sender, e)

        End If

        If blnLinkRefresh Then
            RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)
        End If
    End Sub

    ''' <summary>
    ''' ����Ŋz�A�ō����z�̃Z�b�g
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZeigaku(ByVal e As System.EventArgs, Optional ByVal blnZeigaku As Boolean = False)

        ' �������z���󔒂̏ꍇ�A����ŁA�ō����z���󔒂ɂ���
        If TextJituSeikyuuGaku.Text.Trim() = "" Then
            TextSyouhizeiGaku.Text = ""
            TextZeikomiKingaku.Text = ""
            ' �ō����z�ύX��e�R���g���[���ɒʒm����
            RaiseEvent ChangeZeikomiGaku(Me, e, 0)
            Exit Sub
        ElseIf TextJituSeikyuuGaku.Text.Trim() = "0" Then
            TextSyouhizeiGaku.Text = "0"
            TextZeikomiKingaku.Text = "0"
            ' �ō����z�ύX��e�R���g���[���ɒʒm����
            RaiseEvent ChangeZeikomiGaku(Me, e, 0)
            Exit Sub
        End If

        Dim jitugaku As Integer = IIf(TextJituSeikyuuGaku.Text.Trim() = "", _
                                      0, _
                                      Integer.Parse(TextJituSeikyuuGaku.Text.Replace(",", "")))
        Dim zeigaku As Integer = 0
        If blnZeigaku Then '����Ŋz�v�Z�̏ꍇ
            zeigaku = IIf(TextSyouhizeiGaku.Text.Trim() = "", _
                                      0, _
                                      Integer.Parse(TextSyouhizeiGaku.Text.Replace(",", "")))
        Else
            If IsNumeric(Me.HiddenZeiritu.Value) = False Then
                Me.HiddenZeiritu.Value = "0"
            End If
            zeigaku = Fix(jitugaku * Decimal.Parse(HiddenZeiritu.Value))
        End If

        Dim zeikomi As Integer = jitugaku + zeigaku

        TextSyouhizeiGaku.Text = zeigaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        TextZeikomiKingaku.Text = zeikomi.ToString(EarthConst.FORMAT_KINGAKU_1)

        If Not e Is Nothing Then
            ' �ō����z�ύX��e�R���g���[���ɒʒm����
            RaiseEvent ChangeZeikomiGaku(Me, e, zeikomi)
        End If
    End Sub

    ''' <summary>
    ''' �d������Ŋz�̃Z�b�g
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSiireZeigaku()
        Dim intSyouGaku As Integer = 0
        Dim intSiireZeiGaku As Integer = 0

        '���������z���󔒂̏ꍇ�A�d������Ŋz���󔒂ɂ���
        If Me.TextSyoudakusyoKingaku.Text.Trim() = String.Empty Then
            Me.TextSiireSyouhizeiGaku.Text = String.Empty
            Exit Sub
        ElseIf Me.TextSyoudakusyoKingaku.Text.Trim() = "0" Then
            Me.TextSiireSyouhizeiGaku.Text = "0"
            Exit Sub
        End If

        '�ŗ��̊m�F
        If IsNumeric(Me.HiddenZeiritu.Value) = False Then
            Me.HiddenZeiritu.Value = "0"
        End If

        '���������z�̎擾
        intSyouGaku = Integer.Parse(Me.TextSyoudakusyoKingaku.Text.Replace(",", ""))

        '�d������Ŋz�̌v�Z
        intSiireZeiGaku = Fix(intSyouGaku * Decimal.Parse(Me.HiddenZeiritu.Value))

        '��ʂ֐ݒ�
        Me.TextSiireSyouhizeiGaku.Text = intSiireZeiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub

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

#Region "�n��t���O�擾"
    ''' <summary>
    ''' �n��R�[�h���n��t���O���擾���܂�
    ''' </summary>
    ''' <param name="keiretuCd">�n��R�[�h</param>
    ''' <returns>�n��t���O</returns>
    ''' <remarks>1:�n��</remarks>
    Private Function GetKeiretuFlg(ByVal keiretuCd As String) As Integer

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKeiretuFlg", _
                                            keiretuCd)

        Dim blnKeiretuFlg = cLogic.getKeiretuFlg(keiretuCd)

        If blnKeiretuFlg Then
            Return 1
        Else
            Return 0
        End If

    End Function
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

                ' ���������z����͕s�ɐݒ�
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
           TextHattyuusyoKingaku.Text IsNot TextJituSeikyuuGaku.Text Then

            Dim hattyuuKingaku As Integer = Integer.MinValue
            CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, hattyuuKingaku)
            ' �������m�F����ݒ�
            TextHattyuusyoKakuninbi.Text = PfunZ010SetHatyuuYMD(hattyuuKingaku)

        End If

        ' ��r������z�𐔒l�ϊ�����
        Dim chkVal1 As Integer = 0
        Dim chkVal2 As Integer = 0
        CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, chkVal1)
        CommonLogic.Instance.SetDisplayString(TextJituSeikyuuGaku.Text, chkVal2)

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

#Region "�R���g���[���̊�������"

    ''' <summary>
    ''' �R���g���[���̊�������
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnabledCtrl(ByVal enabled As Boolean)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnabledCtrl", _
                                                    enabled)

        ' ���[�h�̎擾
        Dim mode As Integer = Integer.Parse(HiddenDispMode.Value)

        If Not enabled Then

            ' �f�[�^���N���A
            TextSyouhinCd.Text = ""                     ' ���i�R�[�h
            SpanSyouhinName.Text = ""              ' ���i��
            TextJituSeikyuuGaku.Text = ""               ' ���������z
            TextKoumutenSeikyuuGaku.Text = ""           ' �H���X�����z
            TextSyouhizeiGaku.Text = ""                 ' ����Ŋz
            TextSiireSyouhizeiGaku.Text = ""            ' �d������Ŋz
            TextZeikomiKingaku.Text = ""                ' �ō����z
            TextSyoudakusyoKingaku.Text = ""            ' ���������z
            TextMitumorisyoSakuseibi.Text = ""          ' ���Ϗ��쐬��
            TextSeikyuusyoHakkoubi.Text = ""            ' ���������s��
            TextUriageNengappi.Text = ""                ' ����N����
            TextDenpyouUriageNengappiDisplay.Text = ""  ' �`�[����N����(�Q�Ɨp)
            TextDenpyouUriageNengappi.Text = ""         ' �`�[����N����(�C���p)
            TextDenpyouSiireNengappiDisplay.Text = ""   ' �`�[�d���N����(�Q�Ɨp)
            TextDenpyouSiireNengappi.Text = ""          ' �`�[�d���N����(�C���p)
            If mode = DisplayMode.SYOUHIN1 Or _
               mode = DisplayMode.SYOUHIN2 Or _
               mode = DisplayMode.SYOUHIN3 Then
                SelectSeikyuuUmu.SelectedValue = "1"    ' �����L��
            End If
            SelectKakutei.SelectedValue = "0"           ' �m��
            SelectUriageSyori.SelectedValue = "0"       ' ���㏈��
            TextUriageBi.Text = ""                      ' �����
            TextHattyuusyoKingaku.Text = ""             ' ���������z
            TextHattyuusyoKakuninbi.Text = ""           ' �������m�F��
            SelectHattyuusyoKakutei.SelectedValue = "0" ' �������m��
            HiddenHattyuusyoFlgOld.Value = ""           ' �������t���OOld
            HiddenKingakuFlg.Value = ""                 ' ���z�t���O
            Me.AccTokubetuTaiouToolTip.AccDisplayCd.Value = String.Empty    '���ʑΉ��c�[���`�b�v(�\���p�j

        End If

        ' ��������
        TextKoumutenSeikyuuGaku.Enabled = enabled       ' �H���X�����z
        TextJituSeikyuuGaku.Enabled = enabled           ' ���������z
        TextSyouhizeiGaku.Enabled = enabled             ' ����Ŋz
        TextSiireSyouhizeiGaku.Enabled = enabled        ' �d������Ŋz
        TextSyoudakusyoKingaku.Enabled = enabled        ' ���������z
        TextMitumorisyoSakuseibi.Enabled = enabled      ' ���Ϗ��쐬��
        TextSeikyuusyoHakkoubi.Enabled = enabled        ' ���������s��
        TextUriageNengappi.Enabled = enabled            ' ����N����
        TextDenpyouUriageNengappiDisplay.Enabled = enabled  ' �`�[����N����(�Q�Ɨp)
        TextDenpyouUriageNengappi.Enabled = enabled         ' �`�[����N����(�C���p)
        TextDenpyouSiireNengappiDisplay.Enabled = enabled   ' �`�[�d���N����(�Q�Ɨp)
        TextDenpyouSiireNengappi.Enabled = enabled          ' �`�[�d���N����(�C���p)
        If mode = DisplayMode.SYOUHIN1 Or _
           mode = DisplayMode.SYOUHIN2 Or _
           mode = DisplayMode.SYOUHIN3 Then
            SelectSeikyuuUmu.Enabled = enabled          ' �����L��
        End If
        SelectKakutei.Enabled = enabled                 ' �m��
        SelectUriageSyori.Enabled = enabled             ' ���㏈��
        TextHattyuusyoKingaku.Enabled = enabled         ' ���������z
        TextHattyuusyoKakuninbi.Enabled = enabled       ' �������m�F��
        SelectHattyuusyoKakutei.Enabled = enabled       ' �������m��

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
    ''' �R���g���[���̗L��������ؑւ���[������]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EnabledCtrlKengen()

        ' �o�������������ꍇ�A�������ȊO�񊈐�
        If HiddenKeiriGyoumuKengen.Value = "0" Then
            EnableTextBox(TextSyouhinCd, False) '���i�R�[�h
            EnableTextBox(TextKoumutenSeikyuuGaku, False) '�H���X�����Ŕ����z
            EnableTextBox(TextJituSeikyuuGaku, False) '���������z
            EnableTextBox(TextSyouhizeiGaku, False) '����Ŋz
            EnableTextBox(TextSiireSyouhizeiGaku, False) '�d������Ŋz
            EnableTextBox(TextSyoudakusyoKingaku, False) '���������z
            EnableTextBox(TextMitumorisyoSakuseibi, False) '���Ϗ����z
            EnableTextBox(TextSeikyuusyoHakkoubi, False) '���������s��
            EnableTextBox(TextUriageNengappi, False) '����N����
            EnableTextBox(TextDenpyouUriageNengappi, False) '�`�[����N����
            EnableTextBox(TextDenpyouSiireNengappi, False) '�`�[�d���N����
            EnableTextBox(TextHattyuusyoKakuninbi, False) '�������m�F��
            EnableDropDownList(SelectSeikyuuUmu, False) '�����L��
            EnableDropDownList(SelectUriageSyori, False) '���㏈��
            EnableDropDownList(SelectKakutei, False) '�m�菈��
            ButtonSyouhinKensaku.Visible = False '���i����

        End If

        ' �o�������y�є������Ǘ������������ꍇ�A�񊈐���
        If HiddenHattyuusyoKanriKengen.Value = "0" And _
           HiddenKeiriGyoumuKengen.Value = "0" Then
            EnableTextBox(TextHattyuusyoKingaku, False) '���������z
            EnableDropDownList(SelectHattyuusyoKakutei, False) '�������m��
        End If

    End Sub

#End Region

#Region "��񕥖ߕύX���m�F����"

    ''' <summary>
    ''' ��񕥖ߏ��i�Z�b�g����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetKaiyakuSyouhin(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strKameitenCd As String = Me.SeikyuuSiireLinkCtrl.AccKameitenCd.Value
        Dim logic As New TeibetuSyuuseiLogic
        Dim syouhinRec As New SyouhinMeisaiRecord
        Dim strZeiKbn As String = String.Empty      '�ŋ敪
        Dim strZeiritu As String = String.Empty     '�ŗ�
        Dim strSyouhinCd As String = String.Empty   '���i�R�[�h

        syouhinRec = logic.GetSyouhinInfo("" _
                                        , EarthEnum.EnumSyouhinKubun.Kaiyaku _
                                        , strKameitenCd)


        '�����X�֘A���̃Z�b�g
        syouhinRec.KameitenCdDisp = strKameitenCd
        syouhinRec.KeiretuCd = Me.KeiretuCd

        '�����悪�ʂɎw�肳��Ă���ꍇ�A�f�t�H���g�̐�������㏑��
        If Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value <> String.Empty Then
            syouhinRec.SeikyuuSakiCdDisp = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value
            syouhinRec.SeikyuuSakiBrcDisp = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc.Value
            syouhinRec.SeikyuuSakiKbnDisp = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn.Value
        End If

        If syouhinRec IsNot Nothing Then
            '�����^�C�v�̐ݒ�
            RaiseEvent SetSeikyuuTypeAction(Me _
                                            , e _
                                            , Me.ClientID _
                                            , syouhinRec.SeikyuuSakiType _
                                            , syouhinRec.KeiretuCd _
                                            , syouhinRec.KameitenCdDisp)
        End If

        '���i��
        TextSyouhinCd.Text = syouhinRec.SyouhinCd
        SpanSyouhinName.Text = syouhinRec.SyouhinMei

        '���E�ۏ؊J�n���A�ۏؗL���A�ۏ؊��ԁA�ی���ЁA�ی��\���A�ی��\����
        '��񕥖ߗL�����L�莞�̓f�t�H���g�N���A�ƂȂ邽�߁A�������͂��Ȃ��œo�^�{�^���������������m�F

        '����N�����Ŕ��f���āA�������ŗ����擾����
        strSyouhinCd = syouhinRec.SyouhinCd
        If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '�擾�����ŋ敪�E�ŗ����Z�b�g
            syouhinRec.ZeiKbn = strZeiKbn
            syouhinRec.Zeiritu = strZeiritu
        End If

        HiddenZeiritu.Value = syouhinRec.Zeiritu
        HiddenZeiKbn.Value = syouhinRec.ZeiKbn

        ' ���͕W�����i���ɉ�񕥖߂����i��ݒ肷��
        syouhinRec.HyoujunKkk = _
            logic.GetKaiyakuKakaku(HiddenKameitenCd.Value, HiddenKubun.Value)

        '**************************************************
        ' ��񕥖�
        '**************************************************
        Dim jibanLogic As New JibanLogic
        Dim zeinukiGaku As Integer = Math.Abs(syouhinRec.HyoujunKkk) * -1

        ' ���������z�փZ�b�g
        TextJituSeikyuuGaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
           HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then
            '**************************************************
            ' ���ڐ���
            '**************************************************
            ' �H���X�����z�֎��������z���Z�b�g
            TextKoumutenSeikyuuGaku.Text = TextJituSeikyuuGaku.Text
        ElseIf HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuKeiretu Then
            '**************************************************
            ' �������i�n��j
            '**************************************************
            zeinukiGaku = 0

            If JibanLogic.GetSeikyuuGaku(sender, _
                                    2, _
                                    HiddenKeiretuCd.Value, _
                                    TextSyouhinCd.Text, _
                                    syouhinRec.HyoujunKkk, _
                                    zeinukiGaku) Then

                zeinukiGaku = Math.Abs(zeinukiGaku) * -1

                ' �H���X�����z�փZ�b�g
                TextKoumutenSeikyuuGaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            End If
        ElseIf HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuNotKeiretu Then
            '**************************************************
            ' �������i�n��ȊO�j
            '**************************************************
            ' �H���X�����z�͂O
            TextKoumutenSeikyuuGaku.Text = "0"
        End If

        SetZeigaku(e)

        HiddenSeikyuuUmuOld.Value = SelectSeikyuuUmu.SelectedValue

        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)


    End Sub


    ''' <summary>
    ''' ��񕥖ߐ\���L���ύX�E�㏈��(JS�ɂăL�����Z����)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKaiyakuHaraiModosiCancel_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '��񕥖ߐ����L��
        SelectSeikyuuUmu.SelectedValue = "" '��
        HiddenSeikyuuUmuOld.Value = ""

        Enabled = False

        ' �R���g���[���̊�������
        EnabledCtrl(Enabled)

        SelectSeikyuuUmu.Enabled = True          ' �����L��

        '�t�H�[�J�X�Z�b�g
        SetFocus(SelectSeikyuuUmu)

    End Sub

#End Region

#Region "��ʃR���g���[���̒l���������A�����񉻂���"
    ''' <summary>
    ''' ��ʃR���g���[���̒l���������A�����񉻂���(����)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringUriage() As String

        Dim sb As New StringBuilder

        sb.Append(TextSyouhinCd.Text & EarthConst.SEP_STRING)               '���i�R�[�h
        sb.Append(TextJituSeikyuuGaku.Text & EarthConst.SEP_STRING)         '������z(�������Ŕ����z)
        sb.Append(TextSyouhizeiGaku.Text & EarthConst.SEP_STRING)           '����Ŋz
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
    ''' ��ʃR���g���[���̒l���������A�����񉻂���(�d��)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringSiire() As String

        Dim sb As New StringBuilder

        sb.Append(TextSyouhinCd.Text & EarthConst.SEP_STRING)               '���i�R�[�h
        sb.Append(TextSyoudakusyoKingaku.Text & EarthConst.SEP_STRING)      '�d�����z(���������z)
        sb.Append(TextSiireSyouhizeiGaku.Text & EarthConst.SEP_STRING)      '�d������Ŋz
        sb.Append(TextDenpyouSiireNengappi.Text & EarthConst.SEP_STRING)    '�`�[�d���N����
        sb.Append(HiddenZeiKbn.Value & EarthConst.SEP_STRING)               '�ŋ敪(��\������)
        sb.Append(SelectUriageSyori.SelectedValue & EarthConst.SEP_STRING)  '����v��FLG

        sb.Append(SeikyuuSiireLinkCtrl.AccTysKaisyaCd.Value & EarthConst.SEP_STRING)            '������ЃR�[�h
        sb.Append(SeikyuuSiireLinkCtrl.AccTysKaisyaJigyousyoCd.Value & EarthConst.SEP_STRING)   '������Ў��Ə��R�[�h

        Return (sb.ToString)

    End Function

    ''' <summary>
    ''' ��ʃR���g���[���̒l���������A�����񉻂���(���ʑΉ����i�Ή�)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringTokubetuTaiou() As String

        Dim sb As New StringBuilder

        sb.Append(TextSyouhinCd.Text & EarthConst.SEP_STRING)               '���i�R�[�h
        sb.Append(TextKoumutenSeikyuuGaku.Text & EarthConst.SEP_STRING)     '�H���X�����Ŕ����z
        sb.Append(TextJituSeikyuuGaku.Text & EarthConst.SEP_STRING)         '�������Ŕ����z
        sb.Append(TextSyouhizeiGaku.Text & EarthConst.SEP_STRING)           '����Ŋz
        sb.Append(SelectSeikyuuUmu.SelectedValue & EarthConst.SEP_STRING)   '�����L��

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
        sb.Append(HiddenKubun.Value & EarthConst.SEP_STRING)   '�敪
        sb.Append(HiddenBangou.Value & EarthConst.SEP_STRING)   '�ۏ؏�NO
        sb.Append(HiddenBunruiCd.Value & EarthConst.SEP_STRING)   '���ރR�[�h
        sb.Append(HiddenGamenHyoujiNo.Value & EarthConst.SEP_STRING)   '��ʕ\��NO

        '���\������
        sb.Append(TextSyouhinCd.Text & EarthConst.SEP_STRING)   '���i�R�[�h
        sb.Append(SelectKakutei.SelectedValue & EarthConst.SEP_STRING)   '�m��敪
        sb.Append(SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value & EarthConst.SEP_STRING)   '������R�[�h
        sb.Append(SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc.Value & EarthConst.SEP_STRING)   '������}��
        sb.Append(SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn.Value & EarthConst.SEP_STRING)   '������敪
        sb.Append(SeikyuuSiireLinkCtrl.AccTysKaisyaCd.Value & EarthConst.SEP_STRING)   '������ЃR�[�h
        sb.Append(SeikyuuSiireLinkCtrl.AccTysKaisyaJigyousyoCd.Value & EarthConst.SEP_STRING)   '������Ў��Ə��R�[�h

        sb.Append(TextSyoudakusyoKingaku.Text & EarthConst.SEP_STRING)   '���������z(�d��)
        sb.Append(TextSiireSyouhizeiGaku.Text & EarthConst.SEP_STRING)   '�d������Ŋz
        sb.Append(TextDenpyouSiireNengappiDisplay.Text & EarthConst.SEP_STRING)   '�`�[�d���N����
        sb.Append(TextDenpyouSiireNengappi.Text & EarthConst.SEP_STRING)   '�`�[�d���N�����C��

        sb.Append(TextKoumutenSeikyuuGaku.Text & EarthConst.SEP_STRING)   '�H���X�����Ŕ����z
        sb.Append(TextJituSeikyuuGaku.Text & EarthConst.SEP_STRING)   '�������Ŕ����z
        sb.Append(TextSyouhizeiGaku.Text & EarthConst.SEP_STRING)   '����Ŋz(����)
        sb.Append(TextZeikomiKingaku.Text & EarthConst.SEP_STRING)   '�ō����z(����)
        sb.Append(TextDenpyouUriageNengappiDisplay.Text & EarthConst.SEP_STRING)   '�`�[����N����
        sb.Append(TextDenpyouUriageNengappi.Text & EarthConst.SEP_STRING)   '�`�[����N�����C��
        sb.Append(TextUriageNengappi.Text & EarthConst.SEP_STRING)   '����N����
        sb.Append(SelectUriageSyori.SelectedValue & EarthConst.SEP_STRING)   '���㏈��FLG(����v��FLG)
        sb.Append(TextUriageBi.Text & EarthConst.SEP_STRING)   '���㏈����(����v���)

        sb.Append(TextSeikyuusyoHakkoubi.Text & EarthConst.SEP_STRING)   '���������s��
        sb.Append(SelectSeikyuuUmu.SelectedValue & EarthConst.SEP_STRING)   '�����L��

        sb.Append(TextHattyuusyoKingaku.Text & EarthConst.SEP_STRING)   '���������z
        sb.Append(SelectHattyuusyoKakutei.SelectedValue & EarthConst.SEP_STRING)   '�������m��FLG
        sb.Append(TextHattyuusyoKakuninbi.Text & EarthConst.SEP_STRING)   '�������m�F��

        '����\�����ڂQ
        sb.Append(HiddenZeiKbn.Value & EarthConst.SEP_STRING)   '�ŋ敪(�ŗ�)


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
                .Add("3", "��ʕ\��NO")
                .Add("4", "���i�R�[�h")
                .Add("5", "�m��敪")
                .Add("6", "������R�[�h")
                .Add("7", "������}��")
                .Add("8", "������敪")
                .Add("9", "������ЃR�[�h")
                .Add("10", "������Ў��Ə��R�[�h")
                .Add("11", "���������z(�d��)")
                .Add("12", "�d������Ŋz")
                .Add("13", "�`�[�d���N����")
                .Add("14", "�`�[�d���N�����C��")
                .Add("15", "�H���X�����Ŕ����z")
                .Add("16", "�������Ŕ����z")
                .Add("17", "����Ŋz(����)")
                .Add("18", "�ō����z(����)")
                .Add("19", "�`�[����N����")
                .Add("20", "�`�[����N�����C��")
                .Add("21", "����N����")
                .Add("22", "���㏈��FLG(����v��FLG)")
                .Add("23", "���㏈����(����v���)")
                .Add("24", "���������s��")
                .Add("25", "�����L��")
                .Add("26", "���������z")
                .Add("27", "�������m��FLG")
                .Add("28", "�������m�F��")
                .Add("29", "�ŋ敪(�ŗ�)")
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
            .Add("3", Me.HiddenGamenHyoujiNo)
            .Add("4", Me.TextSyouhinCd)
            .Add("5", Me.SelectKakutei)
            .Add("6", Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd)
            .Add("7", Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc)
            .Add("8", Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn)
            .Add("9", Me.SeikyuuSiireLinkCtrl.AccTysKaisyaCd)
            .Add("10", Me.SeikyuuSiireLinkCtrl.AccTysKaisyaJigyousyoCd)
            .Add("11", Me.TextSyoudakusyoKingaku)
            .Add("12", Me.TextSiireSyouhizeiGaku)
            .Add("13", Me.TextDenpyouSiireNengappiDisplay)
            .Add("14", Me.TextDenpyouSiireNengappi)
            .Add("15", Me.TextKoumutenSeikyuuGaku)
            .Add("16", Me.TextJituSeikyuuGaku)
            .Add("17", Me.TextSyouhizeiGaku)
            .Add("18", Me.TextZeikomiKingaku)
            .Add("19", Me.TextDenpyouUriageNengappiDisplay)
            .Add("20", Me.TextDenpyouUriageNengappi)
            .Add("21", Me.TextUriageNengappi)
            .Add("22", Me.SelectUriageSyori)
            .Add("23", Me.TextUriageBi)
            .Add("24", Me.TextSeikyuusyoHakkoubi)
            .Add("25", Me.SelectSeikyuuUmu)
            .Add("26", Me.TextHattyuusyoKingaku)
            .Add("27", Me.SelectHattyuusyoKakutei)
            .Add("28", Me.TextHattyuusyoKakuninbi)
            .Add("29", Me.HiddenZeiKbn)
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
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckInput", _
                                                    errMess, _
                                                    arrFocusTargetCtrl, _
                                                    typeWord, _
                                                    denpyouNgList, _
                                                    denpyouErrMess, _
                                                    seikyuuUmuErrMess, _
                                                    strChgPartMess _
                                                    )

        ' ���[�h�̎擾
        Dim mode As Integer = Integer.Parse(HiddenDispMode.Value)

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
                If denpyouNgList.Replace("$$$115$$$", "$$$110$$$").IndexOf(HiddenKubun.Value & EarthConst.SEP_STRING & _
                                                                           HiddenBangou.Value & EarthConst.SEP_STRING & _
                                                                           Me.BunruiCd.Replace("115", "110") & EarthConst.SEP_STRING & _
                                                                           Me.GamenHyoujiNo) >= 0 Then
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

            ' ���i�P�`�R
            If mode = DisplayMode.SYOUHIN1 OrElse mode = DisplayMode.SYOUHIN2 OrElse mode = DisplayMode.SYOUHIN3 Then
                '��r���{(���ʑΉ����i�Ή�)
                If Me.HiddenOpenValuesTokubetuTaiou.Value <> getCtrlValuesStringTokubetuTaiou() Then
                    Me.AccTokubetuTaiouUpdFlg.Value = EarthConst.ARI_VAL
                Else
                    Me.AccTokubetuTaiouUpdFlg.Value = EarthConst.NASI_VAL
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

        ' ���i�P�͕K�{
        If mode = DisplayMode.SYOUHIN1 Then
            If TextSyouhinCd.Text = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", setuzoku & "���i�R�[�h")
                arrFocusTargetCtrl.Add(TextSyouhinCd)
            End If
        End If

        ' ���i�Q�E�R�͏��i�R�[�h�̕ύX�`�F�b�N���s��
        If mode = DisplayMode.SYOUHIN2 Or _
           mode = DisplayMode.SYOUHIN3 Then
            If TextSyouhinCd.Text <> HiddenSyouhinCdOld.Value Then
                errMess += Messages.MSG030E.Replace("@PARAM1", setuzoku & "���i�R�[�h")
                arrFocusTargetCtrl.Add(ButtonSyouhinKensaku)
            End If
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
        If TextMitumorisyoSakuseibi.Text <> "" Then
            If DateTime.Parse(TextMitumorisyoSakuseibi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextMitumorisyoSakuseibi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "���Ϗ��쐬��")
                arrFocusTargetCtrl.Add(TextMitumorisyoSakuseibi)
            End If
        End If

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
        Dim intJituGaku As Integer = dtLogic.str2Int(TextJituSeikyuuGaku.Text.Replace(",", ""))

        '���i�̐����L���Ɣ�����z�Ƃ̊֘A�`�F�b�N(���������E0�~�ȊO�FNG / ��������E0�~: �x��)
        If HiddenOpenValuesUriage.Value <> String.Empty Then
            If TextSyouhinCd.Text <> String.Empty Then
                If SelectSeikyuuUmu.SelectedValue = 0 And intJituGaku <> 0 Then
                    '���������E0�~�ȊO�FNG
                    errMess += String.Format(Messages.MSG157E, typeWord)
                    arrFocusTargetCtrl.Add(SelectSeikyuuUmu)
                ElseIf SelectSeikyuuUmu.SelectedValue = 1 And intJituGaku = 0 Then
                    '��������E0�~: �x��
                    seikyuuUmuErrMess += typeWord & "�A"
                    arrFocusTargetCtrl.Add(TextJituSeikyuuGaku)
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' ��{������E�d������̐ݒ�
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strTysKaisyaCd">������ЃR�[�h</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetDefaultSeikyuuSiireSakiInfo(ByVal strKameitenCd As String, ByVal strTysKaisyaCd As String, ByVal strKeiretuCd As String)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetDefaultSeikyuuSiireSakiInfo", _
                                                    strKameitenCd, _
                                                    strTysKaisyaCd)

        Dim strUriageZumi As String = String.Empty  '���㏈���ςݔ��f�t���O�p

        '���㏈���σ`�F�b�N
        If Me.SelectUriageSyori.SelectedValue = "1" Then
            strUriageZumi = Me.SelectUriageSyori.SelectedValue
        End If

        '�n��R�[�h�̐ݒ�
        Me.HiddenKeiretuCd.Value = strKeiretuCd

        Me.SeikyuuSiireLinkCtrl.SetVariableValueCtrlFromParent(strKameitenCd _
                                                                , Me.TextSyouhinCd.Text _
                                                                , strTysKaisyaCd _
                                                                , strUriageZumi _
                                                                , _
                                                                , _
                                                                , Me.TextDenpyouUriageNengappi.Text)
    End Sub

    ''' <summary>
    ''' �����^�C�v�̐ݒ�
    ''' </summary>
    ''' <param name="enSeikyuuType">�����^�C�v</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strSyouhinLine">�J�X�^���C�x���g������ID</param>
    ''' <remarks></remarks>
    Public Sub SetSeikyuuType(ByVal enSeikyuuType As EarthEnum.EnumSeikyuuType _
                            , ByVal strKeiretuCd As String _
                            , ByVal strKameitenCd As String _
                            , Optional ByVal strSyouhinLine As String = "")

        ' ������������i�P�R���g���[���֐ݒ肷��
        ' ���������z�E�H���X�����z�E�����L���̕ύX������Ɏg�p���܂�
        Me.SeikyuuType = cLogic.GetDisplayString(Integer.Parse(enSeikyuuType))
        Me.KeiretuCd = strKeiretuCd
        Me.KameitenCd = strKameitenCd
        Me.UpdateSyouhinPanel.Update()
    End Sub

#End Region

End Class
