
Partial Public Class TeibetuIraiNaiyouCtrl
    Inherits System.Web.UI.UserControl

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim masterAjaxSM As New ScriptManager

    ''' <summary>
    ''' ���b�Z�[�W�N���X
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic
    Dim cLogic As New CommonLogic
    Dim jLogic As New JibanLogic
    Dim cbLogic As New CommonBizLogic

#Region "�v���p�e�B"

#Region "�˗����"
    ''' <summary>
    ''' �˗����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �˗���񃊃��NID</returns>
    ''' <remarks></remarks>
    Public Property IraiTBody() As HtmlGenericControl
        Get
            Return TBodyIrai
        End Get
        Set(ByVal value As HtmlGenericControl)
            TBodyIrai = value
        End Set
    End Property
#End Region

#Region "�˗����^�C�g�������ID"
    ''' <summary>
    ''' �˗����^�C�g�������ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> �˗����^�C�g�������ID</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IraiTitleInfobarID() As String
        Get
            Return IraiTitleInfobar.ClientID
        End Get
    End Property
#End Region

#Region "�敪"
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> �敪</returns>
    ''' <remarks></remarks>
    Public Property Kbn() As String
        Get
            Return HiddenKbn.Value
        End Get
        Set(ByVal value As String)
            HiddenKbn.Value = value
        End Set
    End Property
#End Region

#Region "�����X����"
    ''' <summary>
    ''' �����X����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����X����</returns>
    ''' <remarks></remarks>
    Public Property KameitenCd() As String
        Get
            Return TextKameitenCd.Value
        End Get
        Set(ByVal value As String)
            TextKameitenCd.Value = value
        End Set
    End Property
#End Region

#Region "�����X�R�[�h"
    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����X�R�[�h</returns>
    ''' <remarks></remarks>
    Public Property KameitenCdBox() As HtmlInputText
        Get
            Return TextKameitenCd
        End Get
        Set(ByVal value As HtmlInputText)
            TextKameitenCd = value
        End Set
    End Property
#End Region

#Region "�c�Ə��R�[�h"
    ''' <summary>
    ''' �c�Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �c�Ə��R�[�h</returns>
    ''' <remarks></remarks>
    Public Property EigyousyoCd() As String
        Get
            Return HiddenEigyousyoCd.Value
        End Get
        Set(ByVal value As String)
            HiddenEigyousyoCd.Value = value
        End Set
    End Property
#End Region

#Region "�n��R�[�h"
    ''' <summary>
    ''' �n��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �n��R�[�h</returns>
    ''' <remarks></remarks>
    Public Property KeiretuCd() As String
        Get
            Return HiddenKeiretuCd.Value
        End Get
        Set(ByVal value As String)
            HiddenKeiretuCd.Value = value
        End Set
    End Property
#End Region

#Region "������ЃR�[�h"
    ''' <summary>
    ''' ������ЃR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������ЃR�[�h</returns>
    ''' <remarks></remarks>
    Public Property TyousaKaishaCdBox() As HtmlInputText
        Get
            Return TextTyousaKaishaCd
        End Get
        Set(ByVal value As HtmlInputText)
            TextTyousaKaishaCd = value
        End Set
    End Property
#End Region

#Region "������к���"
    ''' <summary>
    ''' ������к���
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������к���</returns>
    ''' <remarks></remarks>
    Public Property TysKaisyaCd() As String
        Get
            Return HiddenTysKaisyaCd.Value
        End Get
        Set(ByVal value As String)
            HiddenTysKaisyaCd.Value = value
            TextTyousaKaishaCd.Value = HiddenTysKaisyaCd.Value.Trim() & HiddenTysKaisyaJigyousyoCd.Value.Trim()
        End Set
    End Property
#End Region

#Region "������Ў��Ə�����"
    Private strTysKaisyaJigyousyoCd As String
    ''' <summary>
    ''' ������Ў��Ə�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������Ў��Ə�����</returns>
    ''' <remarks></remarks>
    Public Property TysKaisyaJigyousyoCd() As String
        Get
            Return HiddenTysKaisyaJigyousyoCd.Value
        End Get
        Set(ByVal value As String)
            HiddenTysKaisyaJigyousyoCd.Value = value
            TextTyousaKaishaCd.Value = HiddenTysKaisyaCd.Value.Trim() & HiddenTysKaisyaJigyousyoCd.Value.Trim()
        End Set
    End Property
#End Region

#Region "ReportIF�i���X�e�[�^�X����"
    ''' <summary>
    ''' ReportIF�i���X�e�[�^�X����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF�i���X�e�[�^�X����</returns>
    ''' <remarks></remarks>
    Public Property StatusIfName() As String
        Get
            Return TextStatusIf.Value
        End Get
        Set(ByVal value As String)
            TextStatusIf.Value = value
        End Set
    End Property
#End Region

#Region "ReportIF�i���X�e�[�^�X�R�[�h"
    ''' <summary>
    ''' ReportIF�i���X�e�[�^�X�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF�i���X�e�[�^�X�R�[�h </returns>
    ''' <remarks></remarks>
    Public Property StatusIfCd() As String
        Get
            Return HiddenStatusIf.Value
        End Get
        Set(ByVal value As String)
            HiddenStatusIf.Value = value
        End Set
    End Property
#End Region

#Region "�����˗�����"
    ''' <summary>
    ''' �����˗�����
    ''' </summary>
    ''' <remarks></remarks>
    Private _doujiIraiTousuu As Integer
    ''' <summary>
    ''' �����˗�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����˗�����</returns>
    ''' <remarks></remarks>
    Public Property DoujiIraiTousuu() As Integer
        Get
            If TextDoujiIraiTousuu.Value = String.Empty Then
                Return 1
            Else
                Return Integer.Parse(TextDoujiIraiTousuu.Value.Replace(",", ""))
            End If
        End Get
        Set(ByVal value As Integer)
            _doujiIraiTousuu = value
        End Set
    End Property
    Public Property DoujiIraiTousuuData() As Integer
        Get
            Return _doujiIraiTousuu
        End Get
        Set(ByVal value As Integer)
            value = _doujiIraiTousuu
        End Set
    End Property
#End Region

#Region "�����p�rNO"
    ''' <summary>
    ''' �����p�rNO
    ''' </summary>
    ''' <remarks></remarks>
    Private _tatemonoYoutoNo As Integer
    ''' <summary>
    ''' �����p�rNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����p�rNO</returns>
    ''' <remarks></remarks>
    Public Property TatemonoYoutoNo() As Integer
        Get
            Return IIf(SelectTatemonoYouto.SelectedValue = "", 99, Integer.Parse(SelectTatemonoYouto.SelectedValue))
        End Get
        Set(ByVal value As Integer)
            _tatemonoYoutoNo = value
        End Set
    End Property
#End Region

#Region "�������@"
    ''' <summary>
    ''' �������@
    ''' </summary>
    ''' <remarks></remarks>
    Private _tysHouhou As Integer
    ''' <summary>
    ''' �������@
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������@</returns>
    ''' <remarks></remarks>
    Public Property TysHouhou() As Integer
        Get
            ' �������@�͕K�{
            If SelectTyousaHouhou.SelectedValue = "" Then
                Return 99
            Else
                Return Integer.Parse(SelectTyousaHouhou.SelectedValue)
            End If
        End Get
        Set(ByVal value As Integer)
            _tysHouhou = value
        End Set
    End Property
    Public Property TysHouhouData() As Integer
        Get
            Return _tysHouhou
        End Get
        Set(ByVal value As Integer)
            _tysHouhou = value
        End Set
    End Property
#End Region

#Region "�������@(�O������̃A�N�Z�X�p)"
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for SelectTyousaHouhou
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTysHouhou() As DropDownList
        Get
            Return SelectTyousaHouhou
        End Get
        Set(ByVal value As DropDownList)
            SelectTyousaHouhou = value
        End Set
    End Property
#End Region

#Region "�����T�v"
    ''' <summary>
    ''' �����T�v
    ''' </summary>
    ''' <remarks></remarks>
    Private _tysGaiyou As Integer
    ''' <summary>
    ''' �����T�v
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����T�v</returns>
    ''' <remarks></remarks>
    Public Property TysGaiyou() As Integer
        Get
            ' �����T�v���I����9�Ȃ̂�Null��9�ɂ��Ă���
            If SelectTyousaGaiyou.Value = "" Then
                Return 9
            Else
                Return Integer.Parse(SelectTyousaGaiyou.Value)
            End If
        End Get
        Set(ByVal value As Integer)
            _tysGaiyou = value
        End Set
    End Property
    Public Property TysGaiyouData() As Integer
        Get
            Return _tysGaiyou
        End Get
        Set(ByVal value As Integer)
            _tysGaiyou = value
        End Set
    End Property
#End Region

#Region "���i1�R�[�h"
    ''' <summary>
    ''' ���i1�R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private _syouhin1 As String
    ''' <summary>
    ''' ���i1�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i1�R�[�h</returns>
    ''' <remarks></remarks>
    Public Property Syouhin1() As String
        Get
            Return SelectSyouhin1.SelectedValue
        End Get
        Set(ByVal value As String)
            _syouhin1 = value
        End Set
    End Property
    Public Property Syouhin1Data() As String
        Get
            Return _syouhin1
        End Get
        Set(ByVal value As String)
            _syouhin1 = value
        End Set
    End Property
#End Region

#Region "���i1�R�[�h(�O������̃A�N�Z�X�p)"
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for SelectSyouhin1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSelectSyouhin1() As DropDownList
        Get
            Return SelectSyouhin1
        End Get
        Set(ByVal value As DropDownList)
            SelectSyouhin1 = value
        End Set
    End Property
#End Region

#Region "���i1����"
    ''' <summary>
    ''' ���i1����
    ''' </summary>
    ''' <remarks></remarks>
    Private _syouhin1Mei As String
    ''' <summary>
    ''' ���i1����
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i1����</returns>
    ''' <remarks></remarks>
    Public Property Syouhin1Mei() As String
        Get
            Return SelectSyouhin1.Text
        End Get
        Set(ByVal value As String)
            _syouhin1Mei = value
        End Set
    End Property
#End Region

#Region "���i�敪"
    ''' <summary>
    ''' ���i�敪
    ''' </summary>
    ''' <remarks></remarks>
    Private _syouhinKbn As Integer
    ''' <summary>
    ''' ���i�敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���i�敪</returns>
    ''' <remarks></remarks>
    Public Property SyouhinKbn() As Integer
        Get
            If RadioSyouhinKbn1.Checked Then
                Return 1
            ElseIf RadioSyouhinKbn2.Checked Then
                Return 2
            ElseIf RadioSyouhinKbn3.Checked Then
                Return 3
            Else
                Return 9
            End If
        End Get
        Set(ByVal value As Integer)
            _syouhinKbn = value

            RadioSyouhinKbn1.Checked = False
            RadioSyouhinKbn2.Checked = False
            RadioSyouhinKbn3.Checked = False
            RadioSyouhinKbn9.Checked = False

            If value = 1 Then
                RadioSyouhinKbn1.Checked = True
            ElseIf value = 2 Then
                RadioSyouhinKbn2.Checked = True
            ElseIf value = 3 Then
                RadioSyouhinKbn3.Checked = True
            ElseIf value = 9 Then
                RadioSyouhinKbn9.Checked = True
            End If
        End Set
    End Property
#End Region

#Region "���i�P�����z��UpdatePanel"
    ''' <summary>
    ''' ���i�P�����z��UpdatePanel
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i�P�����z��UpdatePanel</returns>
    ''' <remarks></remarks>
    Public Property UpdateSyouhin1Seikyuu() As UpdatePanel
        Get
            Return UpdatePanelSyouhin1Seikyuu
        End Get
        Set(ByVal value As UpdatePanel)
            UpdatePanelSyouhin1Seikyuu = value
        End Set
    End Property
#End Region

#Region "���i�P�������z"
    ''' <summary>
    ''' ���i�P�������z
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i�P�������z</returns>
    ''' <remarks></remarks>
    Public Property Syouhin1JituSeikyuuGaku() As String
        Get
            Return HiddenJituSeikyuuGaku.Value
        End Get
        Set(ByVal value As String)
            HiddenJituSeikyuuGaku.Value = value
        End Set
    End Property
#End Region

#Region "���i�P�H���X�����z"
    ''' <summary>
    ''' ���i�P�H���X�����z
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i�P�H���X�����z</returns>
    ''' <remarks></remarks>
    Public Property Syouhin1KoumutenSeikyuuGaku() As String
        Get
            Return HiddenKoumutenSeikyuugaku.Value
        End Get
        Set(ByVal value As String)
            HiddenKoumutenSeikyuugaku.Value = value
        End Set
    End Property
#End Region

#Region "�o���Ɩ�����"
    ''' <summary>
    ''' �o���Ɩ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KeiriGyoumuKengen() As Integer
        Get
            Return HiddenKeiriGyoumuKengen.Value
        End Get
        Set(ByVal value As Integer)
            HiddenKeiriGyoumuKengen.Value = value
        End Set
    End Property
#End Region

#Region "���i�P�������z"
    ''' <summary>
    ''' ���i�P�������z
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Syouhin1HattyuuKingaku() As String
        Get
            Return HiddenHattyuuKingaku.Value
        End Get
        Set(ByVal value As String)
            HiddenHattyuuKingaku.Value = value
        End Set
    End Property
#End Region

#Region "���i�P�̐�������"
    '���i1�̐�����R�[�h
    Private strSyouhin1SeikyuuSakiCd As String
    ''' <summary>
    ''' ���i1�̐�����R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Syouhin1SeikyuuSakiCd() As String
        Get
            Return strSyouhin1SeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strSyouhin1SeikyuuSakiCd = value
        End Set
    End Property

    '���i1�̐�����}��
    Private strSyouhin1SeikyuuSakiBrc As String
    ''' <summary>
    ''' ���i1�̐�����}��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Syouhin1SeikyuuSakiBrc() As String
        Get
            Return strSyouhin1SeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strSyouhin1SeikyuuSakiBrc = value
        End Set
    End Property

    '���i1�̐�����敪
    Private strSyouhin1SeikyuuSakiKbn As String
    ''' <summary>
    ''' ���i1�̐�����敪
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Syouhin1SeikyuuSakiKbn() As String
        Get
            Return strSyouhin1SeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSyouhin1SeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#End Region

#Region "�J�X�^���C�x���g"
    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g�i���i�P�ݒ�p�j
    ''' </summary>
    ''' <remarks>
    ''' �����T���v���P
    ''' <example>
    ''' ���{�N���X�Ɏ�������R�[�h�̗�
    ''' <code>
    ''' ' �e��ʂ̃��\�b�h���s<br/>
    ''' <b>RaiseEvent Syouhin1SetAction(Me, e, syouhin1Rec)</b>
    ''' </code>
    ''' </example><br/>
    ''' �����T���v���Q
    ''' <example>
    ''' ���e��ʂɎ������郁�\�b�h�̗�ł�
    ''' <code>
    ''' ''' <summary><br/>
    ''' ''' �˗��R���g���[���Ŏ擾�������i�P�����ݒ�������i�P�R���g���[���ɔ��f����<br/>
    ''' ''' </summary><br/>
    ''' ''' <param name="sender"></param><br/>
    ''' ''' <param name="e"></param><br/>
    ''' ''' <param name="syouhinRec"></param><br/>
    ''' ''' <remarks></remarks><br/>
    ''' Private Sub SetSyouhin1Data(ByVal sender As System.Object, _<br/>
    '''                             ByVal e As System.EventArgs, _<br/>
    '''                             ByVal syouhinRec As Syouhin1AutoSetRecord, _<br/>
    '''                             ByVal dicIraiInfo As Dictionary(Of String, String)) Handles IraiNaiyou.Syouhin1SetAction<br/><br/><br/>
    '''     ' �������������܂�<br/><br/>
    ''' End Sub<br/>
    ''' </code>
    ''' </example>
    ''' </remarks>
    Public Event Syouhin1SetAction(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs, _
                                ByVal syouhinRec As Syouhin1AutoSetRecord)

    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g�i�����X�ύX���̌n����j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="keiretuCd">�n��R�[�h</param>
    ''' <param name="kameitenCd">�����X�R�[�h</param>
    ''' <remarks></remarks>
    Public Event KeiretuSetAction(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal keiretuCd As String, _
                                      ByVal kameitenCd As String)

    ''' <summary>
    ''' ���i�P�̐���������擾
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event GetSyouhin1SeikyuuSakiInfo(ByVal sender As System.Object _
                                            , ByVal e As System.EventArgs)
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
    ''' ���i�P�̔��������z�v���p�J�X�^���C�x���g
    ''' </summary>
    ''' <remarks></remarks>
    Public Event GetHattyuuKingaku(ByVal sender As System.Object _
                                    , ByVal e As System.EventArgs)

    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g�i������E�d������ݒ�p�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Public Event SetSeikyuuSiireSakiAction(ByVal sender As System.Object _
                                            , ByVal e As System.EventArgs _
                                            , ByVal strId As String)

    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g�i�H�����i�}�X�^�擾�A�N�V�����p�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Public Event GetKojMInfoAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)

    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g�i�H�����i�ݒ�p�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Public Event SetKojMInfoAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)

    ''' <summary>
    ''' �e��ʂ̏������s�p�C�x���g�i�����E�̔����i�}�X�^���݃`�F�b�N�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId"></param>
    ''' <remarks></remarks>
    Public Event CheckGenkaHanbaiKkkMasterAction(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs, _
                                  ByVal strId As String)

#End Region

#Region "�C�x���g�n���h��"
    ''' <summary>
    ''' �y�[�W���[�h���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim MyLogic As New JibanLogic

        '�}�X�^�[�y�[�W�����擾�iScriptManager�p�j
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        If Not IsPostBack Then

            ' �h���b�v�_�E�����X�g�ݒ�
            SetDropDownData()

            ' �v���_�E���A���R�[�h�Ăяo��
            SetPullCdScript()

            ' �����f�[�^�ݒ�
            ' �����˗�����
            TextDoujiIraiTousuu.Value = IIf(_doujiIraiTousuu = 0, 1, _doujiIraiTousuu)
            ' �����p�r
            TextTatemonoYoutoCd.Value = IIf(_tatemonoYoutoNo < 1, 1, _tatemonoYoutoNo)
            SelectTatemonoYouto.SelectedValue = TextTatemonoYoutoCd.Value
            '�������@��DDL�\������
            cLogic.ps_SetSelectTextBoxTysHouhou(_tysHouhou, Me.SelectTyousaHouhou, False)
            TextTyousaHouhouCd.Value = Me.SelectTyousaHouhou.SelectedValue
            HiddenTyousaHouhou.Value = TextTyousaHouhouCd.Value

            ' �����T�v
            SelectTyousaGaiyou.Value = _tysGaiyou
            HiddenTyousaGaiyou.Value = SelectTyousaGaiyou.Value
            ' ���i�敪
            RadioSyouhinKbn1.Checked = (RadioSyouhinKbn1.Value = _syouhinKbn.ToString())
            RadioSyouhinKbn2.Checked = (RadioSyouhinKbn2.Value = _syouhinKbn.ToString())
            RadioSyouhinKbn3.Checked = (RadioSyouhinKbn3.Value = _syouhinKbn.ToString())
            RadioSyouhinKbn9.Checked = (RadioSyouhinKbn9.Value = _syouhinKbn.ToString())
            HiddenSyouhinKbn.Value = SyouhinKbn.ToString()
            ' ���i1
            HiddenSyouhin1Pre.Value = _syouhin1
            If cLogic.ChkDropDownList(SelectSyouhin1, cLogic.GetDispNum(_syouhin1)) Then
                'DDL�ɂ���΁A�I������
                SelectSyouhin1.SelectedValue = cLogic.GetDispNum(_syouhin1)
            Else
                'DDL�ɂȂ���΁A�A�C�e����ǉ�
                SelectSyouhin1.Items.Add(New ListItem(_syouhin1 & ":" & _syouhin1Mei, _syouhin1))
                SelectSyouhin1.SelectedValue = cLogic.GetDispNum(_syouhin1)     '�I�����
            End If

            '****************************************************************************
            ' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
            '****************************************************************************
            SetDispAction(sender, e)

            '���i�敪�̒l�ɂ���āA�\����؂�ւ���
            CheckSyouhinkubun()

            ' �����X���̐ݒ�
            ButtonKameitenKensaku_ServerClick(sender, e)

            ' ������Аݒ�
            ButtonTyousaKaishaKensaku_ServerClick(sender, e)

            ' �˗����e�^�C�g�����A���ݒ�
            SetIraiTitleInfo()

            IraiDispLink.HRef = "javascript:changeDisplayIrai('" & _
                                 TBodyIrai.ClientID & _
                                 "');changeDisplay('" & _
                                 IraiTitleInfobar.ClientID & _
                                 "');"

            ' �o�������������ꍇ�A�񊈐�
            If HiddenKeiriGyoumuKengen.Value = "0" Then
                EnableHtmlInput(TextKameitenCd, False)
                EnableHtmlInput(TextDoujiIraiTousuu, False)
                EnableHtmlInput(TextTatemonoYoutoCd, False)
                EnableHtmlInput(TextTyousaKaishaCd, False)
                EnableHtmlInput(TextTyousaHouhouCd, False)
                EnableDropDownList(SelectSyouhin1, False)
                EnableDropDownList(SelectTatemonoYouto, False)
                EnableDropDownList(SelectTyousaHouhou, False)
                EnableHtmlSelect(SelectTyousaGaiyou, False)
                EnableRadio(RadioSyouhinKbn1, False)
                EnableRadio(RadioSyouhinKbn2, False)
                EnableRadio(RadioSyouhinKbn3, False)
                EnableRadio(RadioSyouhinKbn9, False)

                ButtonKameitenKensaku.Visible = False
                ButtonKameitenTyuuijouhou.Visible = False
                ButtonTyousaKaishaKensaku.Visible = False

            End If

            '��ʕ\�����_�̒l���AHidden�ɕێ�(�d�� �ύX�`�F�b�N�p)
            If Me.HiddenOpenValue.Value = String.Empty Then
                Me.HiddenOpenValue.Value = Me.getCtrlValuesStringAll()
            End If

        End If

    End Sub

    ''' <summary>
    ''' �����X�����{�^���������̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKameitenKensaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^��1���̂ݎ擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�́A������ʂ�\������
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of KameitenSearchRecord)

        Dim recData As New KameitenSearchRecord

        ' ����N�����̂�
        If IsPostBack = False Then
            blnTorikesi = False
        End If

        ' �擾�������i�荞�ޏꍇ�A������ǉ����Ă�������
        If TextKameitenCd.Value <> "" Then '���͗L
            recData = kameitenSearchLogic.GetKameitenSearchResult(HiddenKbn.Value, _
                                                                  TextKameitenCd.Value, _
                                                                  "", _
                                                                  blnTorikesi)
        End If

        '�Y���L
        If Not recData.KameitenCd Is Nothing Then

            '���r���_�[���ӎ����`�F�b�N
            If kameitenSearchLogic.ChkBuilderData13(recData.KameitenCd) Then
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.TrueString
            Else
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.FalseString
            End If

            '���r���_�[���ӎ����`�F�b�N
            Dim strErrMsg As String = String.Empty
            If cbLogic.ChkErrBuilderData(Me.SelectTyousaGaiyou.Value _
                                                    , Me.TextKameitenCd.Value _
                                                    , Me.HiddenKameitenTyuuiJikou.Value _
                                                    , strErrMsg) = False Then
                '���i�P���擾�ł��Ă��Ă��A�r���_�[���ӎ����`�F�b�N��NG�̏ꍇ�A�����I��(�֘A�����X�V����O�ɏI������)
                MLogic.AlertMessage(sender, strErrMsg, 0, "Syouhin1SetError")
                Me.TextKameitenCd.Value = Me.HiddenkameitenCdOld.Value

                Me.UpdatePanelKameiten.Update()
                Exit Sub
            End If

            HiddenkameitenCdOld.Value = recData.KameitenCd
            TextKameitenMei.Value = recData.KameitenMei1
            TextKeiretuNm.Value = recData.KeiretuMei
            TextEigyousyoMei.Value = recData.EigyousyoMei
            Me.HiddenEigyousyoCd.Value = recData.EigyousyoCd
            HiddenKeiretuCd.Value = recData.KeiretuCd
            HiddenTysSeikyuuSaki.Value = recData.TysSeikyuuSaki
            HiddenHansokuSeikyuuSaki.Value = recData.HansokuhinSeikyuuSaki
            SpanTyousaMitumoriFlg.InnerText = recData.TysMitsyoMsg
            SpanHattyuusyoFlg.InnerText = recData.HattyuusyoMsg
            TextJioSakiFlg.Value = IIf(recData.JioSakiFLG = 1, EarthConst.JIO_SAKI, String.Empty)

            '********************************************************************
            '�������ǂ݈ȊO�A�H�����i�}�X�^���牿�i�擾
            If IsPostBack = True Then

                '�e��ʂփC�x���g�ʒm(�H�����i���i�ݒ�)
                RaiseEvent GetKojMInfoAction(sender, e, Me.ClientID)
                RaiseEvent SetKojMInfoAction(sender, e, Me.ClientID)
            End If

            '�����X������R�ݒ�
            setTorikesiRiyuu(recData.Kbn, recData.KameitenCd)

            '�t�H�[�J�X�Z�b�g
            SetFocus(ButtonKameitenKensaku)

        Else '�Y����

            '���r���_�[���ӎ����t���O������
            Me.HiddenKameitenTyuuiJikou.Value = String.Empty

            If blnTorikesi = False Then
                ' �����N�����͌������Ȃ�
                Return
            End If

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            Dim tmpScript As String = "callSearch('" & HiddenKbn.ClientID & _
                                                       EarthConst.SEP_STRING & _
                                                       TextKameitenCd.ClientID & _
                                                       "','" & _
                                                       UrlConst.SEARCH_KAMEITEN & _
                                                       "','" & _
                                                       TextKameitenCd.ClientID & _
                                                       EarthConst.SEP_STRING & _
                                                       TextKameitenMei.ClientID & _
                                                       "','" & _
                                                       ButtonKameitenKensaku.ClientID & "');"

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)

            '�������ʂ��Č��������s����
            If TextKameitenCd.Value <> "" Then '���͗L
                recData = kameitenSearchLogic.GetKameitenSearchResult(HiddenKbn.Value, _
                                                                      TextKameitenCd.Value, _
                                                                      "", _
                                                                      blnTorikesi)
                '�Y���L
                If Not recData.KameitenCd Is Nothing Then

                    '���r���_�[���ӎ����`�F�b�N
                    If kameitenSearchLogic.ChkBuilderData13(recData.KameitenCd) Then
                        Me.HiddenKameitenTyuuiJikou.Value = Boolean.TrueString
                    Else
                        Me.HiddenKameitenTyuuiJikou.Value = Boolean.FalseString
                    End If

                    '���r���_�[���ӎ����`�F�b�N
                    Dim strErrMsg As String = String.Empty
                    If cbLogic.ChkErrBuilderData(Me.SelectTyousaGaiyou.Value, Me.TextKameitenCd.Value, Me.HiddenKameitenTyuuiJikou.Value, strErrMsg) = False Then
                        '���i�P���擾�ł��Ă��Ă��A�r���_�[���ӎ����`�F�b�N��NG�̏ꍇ�A�����I��(�֘A�����X�V����O�ɏI������)
                        MLogic.AlertMessage(sender, strErrMsg, 0, "Syouhin1SetError")
                        Me.TextKameitenCd.Value = Me.HiddenkameitenCdOld.Value

                        Me.UpdatePanelKameiten.Update()
                        Exit Sub
                    End If

                    HiddenkameitenCdOld.Value = recData.KameitenCd
                    TextKameitenMei.Value = recData.KameitenMei1
                    TextKeiretuNm.Value = recData.KeiretuMei
                    HiddenKeiretuCd.Value = recData.KeiretuCd
                    HiddenTysSeikyuuSaki.Value = recData.TysSeikyuuSaki
                    HiddenHansokuSeikyuuSaki.Value = recData.HansokuhinSeikyuuSaki
                    TextEigyousyoMei.Value = recData.EigyousyoMei
                    Me.HiddenEigyousyoCd.Value = recData.EigyousyoCd
                    SpanTyousaMitumoriFlg.InnerText = recData.TysMitsyoMsg
                    SpanHattyuusyoFlg.InnerText = recData.HattyuusyoMsg
                    TextJioSakiFlg.Value = IIf(recData.JioSakiFLG = 1, EarthConst.JIO_SAKI, String.Empty)

                Else '�Y����
                    HiddenkameitenCdOld.Value = ""
                    TextKameitenMei.Value = ""
                    TextKeiretuNm.Value = ""
                    HiddenKeiretuCd.Value = ""
                    HiddenTysSeikyuuSaki.Value = ""
                    HiddenHansokuSeikyuuSaki.Value = ""
                    TextEigyousyoMei.Value = ""
                    Me.HiddenEigyousyoCd.Value = ""
                    SpanTyousaMitumoriFlg.InnerText = ""
                    SpanHattyuusyoFlg.InnerText = ""
                    TextJioSakiFlg.Value = ""
                End If

            Else '���͖�
                HiddenkameitenCdOld.Value = ""
                TextKameitenMei.Value = ""
                TextKeiretuNm.Value = ""
                TextEigyousyoMei.Value = ""
                Me.HiddenEigyousyoCd.Value = ""
                SpanTyousaMitumoriFlg.InnerText = ""
                SpanHattyuusyoFlg.InnerText = ""
                TextJioSakiFlg.Value = ""
            End If
        End If

        '����N�����ȊO
        If IsPostBack = True Then
            '���i�P�Čv�Z
            ButtonSetSyouhin1_ServerClick(sender, e)
        End If

    End Sub

    ''' <summary>
    ''' ������Ќ����{�^���������̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTyousaKaishaKensaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' ���͂���Ă���R�[�h���L�[�ɁA�}�X�^���������A�f�[�^���擾�ł����ꍇ��
        ' ��ʏ����X�V����B�f�[�^�������ꍇ�A������ʂ�\������
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        ' ����N�����̂�
        If IsPostBack = False Then
            blnTorikesi = False
        End If

        If TextTyousaKaishaCd.Value <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetTyousakaisyaSearchResult(TextTyousaKaishaCd.Value, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi, _
                                                                            TextKameitenCd.Value)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            'HiddenTysKaisyaCdOld.Value = recData.TysKaisyaCd & recData.JigyousyoCd
            'HiddenTysKaisyaNmOld.Value = recData.TysKaisyaMei
            TextTyousaKaishaNm.Value = recData.TysKaisyaMei
            TysKaisyaCd = recData.TysKaisyaCd
            TysKaisyaJigyousyoCd = recData.JigyousyoCd
            ' �������NG�ݒ�
            If recData.KahiKbn = 9 Then
                HiddenTysKaisyaNg.Value = EarthConst.TYOUSA_KAISYA_NG
                TextTyousaKaishaCd.Style("color") = "red"
                TextTyousaKaishaNm.Style("color") = "red"
            Else
                HiddenTysKaisyaNg.Value = String.Empty
                TextTyousaKaishaCd.Style("color") = "blue"
                TextTyousaKaishaNm.Style("color") = "blue"
            End If

            '�t�H�[�J�X�Z�b�g
            SetFocus(ButtonTyousaKaishaKensaku)
        Else
            '�\���F��������
            TextTyousaKaishaCd.Style.Remove("color")
            TextTyousaKaishaNm.Style.Remove("color")

            '������ЃR�[�hOld�A������Ж����N���A
            'HiddenTysKaisyaCdOld.Value = String.Empty
            TextTyousaKaishaNm.Value = String.Empty

            If blnTorikesi = False Then
                ' �����N�����͌������Ȃ�
                Return
            End If

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            Dim tmpScript = "callSearch('" & TextTyousaKaishaCd.ClientID & _
                                             EarthConst.SEP_STRING & _
                                             TextKameitenCd.ClientID & _
                                             "','" & _
                                             UrlConst.SEARCH_TYOUSAKAISYA & _
                                             "','" & _
                                             TextTyousaKaishaCd.ClientID & _
                                             EarthConst.SEP_STRING & _
                                             TextTyousaKaishaNm.ClientID & _
                                             EarthConst.SEP_STRING & _
                                             HiddenTysKaisyaNg.ClientID & _
                                             "','" & _
                                             ButtonTyousaKaishaKensaku.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)

        End If

        '�����E�̔����i�}�X�^�ւ̑��݃`�F�b�N
        RaiseEvent CheckGenkaHanbaiKkkMasterAction(sender, e, Me.ClientID)

        If TextTyousaKaishaNm.Value <> String.Empty Then
            HiddenTysKaisyaCdOld.Value = TextTyousaKaishaCd.Value
            HiddenTysKaisyaNmOld.Value = TextTyousaKaishaNm.Value
        End If

        '�e��ʂ̐�����E�d�������ʗp���̃Z�b�g���\�b�h�����s
        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

    End Sub

    ''' <summary>
    ''' ��ʍ��ڂ̓������Z�b�e�B���O�i�����l�A�C�x���g�n���h�����j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetDispAction(ByVal sender As Object, ByVal e As System.EventArgs)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ���V�X�e���ւ̃����N�{�^���ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '�����X���ӎ���
        cLogic.getKameitenTyuuijouhouPath(TextKameitenCd.ClientID, ButtonKameitenTyuuijouhou)

        '���i�P�ݒ�֘A�̂�onchange�C�x���g�n���h����ݒ�
        SetSyouhin1Script()

        '�C�x���g�n���h����ݒ�
        TextKameitenCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){checkNumber(this);}"
        TextTatemonoYoutoCd.Attributes("onblur") += "if(checkTempValueForOnBlur(this)){checkNumber(this);}"
        TextTyousaKaishaCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){checkNumber(this);}"
        TextTyousaHouhouCd.Attributes("onblur") += "if(checkTempValueForOnBlur(this)){checkNumber(this);}"

        TextKameitenCd.Attributes("onkeydown") = "if (event.keyCode == 13){return false;};"
        TextDoujiIraiTousuu.Attributes("onkeydown") = "if (event.keyCode == 13){return false;};"
        TextTatemonoYoutoCd.Attributes("onkeydown") = "if (event.keyCode == 13){return false;};"
        TextTyousaKaishaCd.Attributes("onkeydown") = "if (event.keyCode == 13){return false;};"
        TextTyousaHouhouCd.Attributes("onkeydown") = "if (event.keyCode == 13){return false;};"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' �i���X�e�[�^�X�ɂ���āA������Ђ̕ҏW�ۂ�؂�ւ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        If StatusIfCd > EarthConst.IF_STATUS_TOUROKU_ZUMI Then
            Dim jSM As New JibanSessionManager '�Z�b�V�����Ǘ��N���X
            Dim ht As New Hashtable
            jSM.Hash2Ctrl(UpdatePanelTysKaisya, EarthConst.MODE_VIEW, ht)
            ButtonTyousaKaishaKensaku.Disabled = True
            ButtonTyousaKaishaKensaku.Visible = False
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ������R�e�L�X�g�{�b�N�X�̃X�^�C����ݒ�
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cLogic.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing)

    End Sub

    ''' <summary>
    ''' ���i�P�̎����ݒ�{�^���������C�x���g<br/>
    ''' �����X�R�[�h�A�����p�r�̕ύX���ɉ�������܂�<br/>
    ''' �{�{�^��������aspx����JavaScript�Ŏ��{ [ function callSetSyouhin1(objThis) ]<br/>
    ''' ��L�X�N���v�g�ւ̐ݒ�͂��̃t�@�C���� [ SetSyouhin1Script() ] �Ŏ��{���Ă��܂�<br/>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSetSyouhin1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Syouhin1Set(sender, e)

    End Sub

    ''' <summary>
    ''' ���i�P�̎����ݒ�{�^���������C�x���g(�����T�v�Z�b�g�������s�Ȃ�)<br/>
    ''' ���i�敪�A�������@�̕ύX���ɉ�������܂�<br/>
    ''' �{�{�^��������aspx����JavaScript�Ŏ��{ [ function callSetSyouhin1TysGaiyou(objThis) ]<br/>
    ''' ��L�X�N���v�g�ւ̐ݒ�͂��̃t�@�C���� [ SetSyouhin1Script() ] �Ŏ��{���Ă��܂�<br/>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSetSyouhin1TysGaiyou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Syouhin1Set(sender, e, True)

    End Sub

#End Region

#Region "�v���C�x�[�g���\�b�h"
    ''' <summary>
    ''' �h���b�v�_�E�����X�g�ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDropDownData()

        Dim helper As New DropDownHelper
        'Dim kbn_logic As New KubunLogic
        ' �������@�R���{�Ƀf�[�^���o�C���h����
        helper.SetDropDownList(SelectTyousaHouhou, DropDownHelper.DropDownType.TyousaHouhou, True, False)
        ' ���i1�R���{�Ƀf�[�^���o�C���h����
        helper.SetDropDownList(SelectSyouhin1, DropDownHelper.DropDownType.Syouhin1, True, True, 0, False)
        ' �����T�v�R���{�Ƀf�[�^���o�C���h����
        helper.SetDropDownList(SelectTyousaGaiyou, DropDownHelper.DropDownType.TyousaGaiyou, True)
        ' �����p�r�R���{�Ƀf�[�^���o�C���h����
        helper.SetDropDownList(SelectTatemonoYouto, DropDownHelper.DropDownType.TatemonoYouto, True, False)

    End Sub

    ''' <summary>
    ''' �v���_�E���A���R�[�h���͍��ڃX�N���v�g�ݒ�Ăяo��
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetPullCdScript()

        Dim jBn As New Jiban '�n�Չ�ʃN���X
        jBn.SetPullCdScriptSrc(TextTatemonoYoutoCd, SelectTatemonoYouto)    ' �����p�r
        jBn.SetPullCdScriptSrc(TextTyousaHouhouCd, SelectTyousaHouhou)      ' �������@

    End Sub

    ''' <summary>
    ''' ���i�敪�̒l�ɂ���āA�\����؂�ւ���
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckSyouhinkubun()

        '���W�I�{�^���͔�\���ŏ��i�敪�͑S�ă��x����
        If RadioSyouhinKbn1.Checked Then
            ' 60�N�ۏ�
            SpanSyouhinKbn1.Style("display") = "inline"
            SpanSyouhinKbn2.Style("display") = "none"
            SpanSyouhinKbn3.Style("display") = "none"
            SpanSyouhinKbn9.Style("display") = "none"
        ElseIf RadioSyouhinKbn2.Checked Then
            ' �y�n�̔�
            SpanSyouhinKbn1.Style("display") = "none"
            SpanSyouhinKbn2.Style("display") = "inline"
            SpanSyouhinKbn3.Style("display") = "none"
            SpanSyouhinKbn9.Style("display") = "none"
        ElseIf RadioSyouhinKbn3.Checked Then
            ' ���t�H�[��
            SpanSyouhinKbn1.Style("display") = "none"
            SpanSyouhinKbn2.Style("display") = "none"
            SpanSyouhinKbn3.Style("display") = "inline"
            SpanSyouhinKbn9.Style("display") = "none"
        Else
            ' ���ݒ�͏��i�敪 9 
            SpanSyouhinKbn1.Style("display") = "none"
            SpanSyouhinKbn2.Style("display") = "none"
            SpanSyouhinKbn3.Style("display") = "none"
            SpanSyouhinKbn9.Style("display") = "inline"
        End If

    End Sub

    ''' <summary>
    ''' ���i�P�ݒ�֘A�̂�onchange�C�x���g�n���h����ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSyouhin1Script()

        '���i�P�ݒ�֘A�̂�onchange�C�x���g�n���h����ݒ�
        Dim tmpScript As String = "callSetSyouhin1(this);"
        Dim tmpScript_TysGaiyou As String = "callSetSyouhin1TysGaiyou(this);"
        Dim ChkTysGaiyouScript As String = "callChkTysGaiyou(this)"
        Dim ChkBuilderScript As String = "callChkBuilder(this)"

        SelectTyousaHouhou.Attributes("onchange") += tmpScript_TysGaiyou      ' �������@
        SelectTatemonoYouto.Attributes("onchange") += tmpScript     ' �����p�r
        SelectTyousaGaiyou.Attributes("onchange") = "if(" & ChkBuilderScript & ")" & "if(" & ChkTysGaiyouScript & ");"  ' �����T�v
        SelectSyouhin1.Attributes("onchange") = tmpScript_TysGaiyou '���i1

        '�����˗�����
        Me.TextDoujiIraiTousuu.Attributes("onblur") = "if(checkTempValueForOnBlur_DoujiIrai(this)){if(checkNumberAddFig(this)) " & ChkTysGaiyouScript & "}else{checkNumberAddFig(this);}"
        Me.TextDoujiIraiTousuu.Attributes("onfocus") = "removeFig(this);setTempValueForOnBlur_DoujiIrai(this);"

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

    ''' <summary>
    ''' �˗����^�C�g�����̏��ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetIraiTitleInfo()

        IraiTitleInfobar.InnerHtml = "&nbsp;&nbsp;�y" & _
                                     TextKameitenCd.Value & "�z �y" & _
                                     TextKameitenMei.Value & "�z �y" & _
                                     TextTyousaKaishaCd.Value & "�z �y" & _
                                     TextTyousaKaishaNm.Value & "�z"
    End Sub

    ''' <summary>
    ''' ���i�R�[�h�P�̐ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Public Sub Syouhin1Set(ByVal sender As System.Object, _
                            ByVal e As System.EventArgs, _
                            Optional ByVal blnTysGaiyou As Boolean = False)

        ' ���i�P�ݒ���擾
        Dim syouhin1Rec As Syouhin1AutoSetRecord = Nothing

        ' �f�[�^�擾�p���W�b�N�N���X
        Dim logic As New JibanLogic
        ' �f�[�^�擾�p�p�����[�^�N���X
        Dim param_rec As New Syouhin1InfoRecord
        ' �擾���R�[�h�i�[�N���X
        Dim syouhin_rec As New Syouhin1AutoSetRecord
        ' ���i1�擾�X�e�[�^�X
        Dim intSetSts As Integer = 0
        ' �G���[���b�Z�[�W
        Dim strErrMsg As String = String.Empty

        ' �f�[�^�擾�p�̃p�����[�^�Z�b�g
        param_rec.Kubun = HiddenKbn.Value    ' �敪

        If RadioSyouhinKbn1.Checked = True Then
            ' 60�N�ۏ�(EARTH�ł͔�\��)
            param_rec.SyouhinKbn = 1        ' ���i�敪
        ElseIf RadioSyouhinKbn2.Checked = True Then
            ' �y�n�̔�
            param_rec.SyouhinKbn = 2        ' ���i�敪
        ElseIf RadioSyouhinKbn3.Checked = True Then
            ' ���t�H�[��
            param_rec.SyouhinKbn = 3        ' ���i�敪
        Else
            ' ���ݒ�͏��i�敪 9 
            param_rec.SyouhinKbn = 9        ' ���i�敪
        End If

        ' �����p�r
        CommonLogic.Instance.SetDisplayString(TextTatemonoYoutoCd.Value, param_rec.TatemonoYouto)
        ' ���i�R�[�h1
        CommonLogic.Instance.SetDisplayString(SelectSyouhin1.SelectedValue, param_rec.SyouhinCd)
        ' �������@
        CommonLogic.Instance.SetDisplayString(SelectTyousaHouhou.SelectedValue, param_rec.TyousaHouhouNo)
        ' �������CD+���Ə�CD
        CommonLogic.Instance.SetDisplayString(TextTyousaKaishaCd.Value, param_rec.TysKaisyaCd)
        ' �����˗�����
        CommonLogic.Instance.SetDisplayString(TextDoujiIraiTousuu.Value, param_rec.DoujiIraiTousuu)
        ' �����X�R�[�h
        param_rec.KameitenCd = TextKameitenCd.Value
        ' �n��R�[�h
        param_rec.KeiretuCd = HiddenKeiretuCd.Value
        ' �c�Ə��R�[�h
        param_rec.EigyousyoCd = Me.HiddenEigyousyoCd.Value
        ' �n��t���O
        param_rec.KeiretuFlg = logic.GetKeiretuFlg(HiddenKeiretuCd.Value)

        ' ���i���R�[�h�̐Ŕ����z�ƍH���X�����z���Z�b�g����
        CommonLogic.Instance.SetDisplayString(HiddenJituSeikyuuGaku.Value, _
                                              param_rec.ZeinukiKingaku1)        ' ���������z
        CommonLogic.Instance.SetDisplayString(HiddenKoumutenSeikyuugaku.Value, _
                                              param_rec.KoumutenKingaku1)       ' �H���X�����z
        '���i�P�̐���������擾
        RaiseEvent GetSyouhin1SeikyuuSakiInfo(Me, e)

        '��������̎擾
        syouhin_rec.SeikyuuSakiCd = strSyouhin1SeikyuuSakiCd
        syouhin_rec.SeikyuuSakiBrc = strSyouhin1SeikyuuSakiBrc
        syouhin_rec.SeikyuuSakiKbn = strSyouhin1SeikyuuSakiKbn

        ' ���i�P�����擾����ʂɃZ�b�g
        If logic.GetSyouhin1Info(sender, param_rec, syouhin_rec, intSetSts) = True Then
            syouhin1Rec = syouhin_rec
            '���i1�擾�X�e�[�^�X���Z�b�g
            syouhin1Rec.SetSts = intSetSts
        End If

        ' ���i�R�[�h���擾�ł����A�������z���́��O�̏ꍇ�A���ɖ߂�
        If syouhin_rec.SyouhinCd Is Nothing Then

            RaiseEvent GetHattyuuKingaku(Me, e)

            If HiddenHattyuuKingaku.Value <> "0" Then
                TextTyousaHouhouCd.Value = HiddenTyousaHouhou.Value
                SelectTyousaHouhou.SelectedValue = HiddenTyousaHouhou.Value
                SelectTyousaGaiyou.Value = HiddenTyousaGaiyou.Value
                HiddenSyouhinKbn.Value = IIf(HiddenSyouhinKbn.Value = "", "9", HiddenSyouhinKbn.Value)
                SyouhinKbn = Integer.Parse(HiddenSyouhinKbn.Value)
                Me.SelectSyouhin1.SelectedValue = Me.HiddenSyouhin1Pre.Value

                ' �N���A�ł��Ȃ����b�Z�[�W
                ScriptManager.RegisterClientScriptBlock(sender, _
                                                        sender.GetType(), _
                                                        "alert", _
                                                        "alert('" & _
                                                        Messages.MSG010E & _
                                                        "')", True)

                HiddenTyousaHouhou.Value = TextTyousaHouhouCd.Value
                HiddenTyousaGaiyou.Value = SelectTyousaGaiyou.Value
                HiddenSyouhinKbn.Value = SyouhinKbn.ToString()
                Me.HiddenSyouhin1Pre.Value = Me.SelectSyouhin1.SelectedValue

                UpdatePanelSyouhinKbn.Update()
                UpdatePanelTyousaHouhou.Update()
                UpdatePanelTyousaGaiyou.Update()
                Me.UpdatePanelSyouhin1.Update()

                ' �������f
                Exit Sub
            End If

        End If

        '�����}�X�^�擾�s�̏ꍇ�́A�G���[���b�Z�[�W�̂ݕ\��
        If intSetSts = EarthEnum.emSyouhin1Error.GetGenka Then
            strErrMsg += cbLogic.GetSyouhin1ErrMsg(intSetSts)
        End If

        '�̔����i�}�X�^�擾�s�̏ꍇ�́A�G���[���b�Z�[�W�̂ݕ\��
        If intSetSts = EarthEnum.emSyouhin1Error.GetHanbai Then
            strErrMsg += cbLogic.GetSyouhin1ErrMsg(intSetSts)
        End If

        '�����E�̔����i�}�X�^���ɂȂ��ꍇ�́A�����̃��b�Z�[�W��\��
        If intSetSts = EarthEnum.emSyouhin1Error.GetGenkaHanbai Then
            strErrMsg += cbLogic.GetSyouhin1ErrMsg(EarthEnum.emSyouhin1Error.GetGenka)
            strErrMsg += cbLogic.GetSyouhin1ErrMsg(EarthEnum.emSyouhin1Error.GetHanbai)
        End If

        '���b�Z�[�W������ꍇ�̂ݕ\��
        If strErrMsg <> "" Then
            ScriptManager.RegisterClientScriptBlock(sender, _
                                                    sender.GetType(), _
                                                    "alert", _
                                                    "alert('" & _
                                                    strErrMsg & _
                                                    "')", True)
        End If

        ' �e��ʂ̃��\�b�h���s
        RaiseEvent Syouhin1SetAction(Me, e, syouhin1Rec)

        If syouhin_rec.SyouhinCd IsNot Nothing Then

            '�����^�C�v�̐ݒ�
            RaiseEvent SetSeikyuuTypeAction(Me _
                                            , e _
                                            , Me.ClientID _
                                            , syouhin1Rec.SeikyuuSakiType _
                                            , param_rec.KeiretuCd _
                                            , param_rec.KameitenCd)

        End If

        If blnTysGaiyou Then
            If syouhin_rec.SyouhinCd IsNot Nothing Then
                '�����T�v�̐ݒ�
                Me.SelectTyousaGaiyou.Value = cLogic.GetDispNum(syouhin_rec.TyousaGaiyou, "")
            Else
                '�����T�v�̐ݒ�
                Me.SelectTyousaGaiyou.Value = "9"
            End If
            Me.HiddenTyousaGaiyou.Value = Me.SelectTyousaGaiyou.Value
        End If

        HiddenTyousaHouhou.Value = TextTyousaHouhouCd.Value
        HiddenSyouhinKbn.Value = SyouhinKbn.ToString()
        Me.HiddenSyouhin1Pre.Value = Me.SelectSyouhin1.SelectedValue

        Me.UpdatePanelSyouhinKbn.Update()
        Me.UpdatePanelTyousaHouhou.Update()
        Me.UpdatePanelTyousaGaiyou.Update()
        Me.UpdatePanelSyouhin1.Update()

        '�e��ʂ̐�����E�d�������ʗp���̃Z�b�g���\�b�h�����s
        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

    End Sub

#Region "�R���g���[���̊�������"
    ''' <summary>
    ''' �e�L�X�g�{�b�N�X�P�̂̊�������
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableHtmlInput(ByRef ctrl As HtmlInputText, _
                               ByVal enabled As Boolean, _
                               Optional ByVal isHissu As Boolean = False)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnableHtmlInput", _
                                                    ctrl, enabled, isHissu)

        If enabled Then
            ctrl.Attributes("ReadOnly") = ""
            If isHissu Then
                ctrl.Attributes("class") = "codeNumber hissu"
            Else
                ctrl.Attributes("class") = "codeNumber"
            End If
        Else
            ctrl.Attributes("ReadOnly") = "readonly"
            ctrl.Attributes("class") = "readOnlyStyle"
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
        Else
            ctrl.BackColor = Drawing.Color.Transparent
            ctrl.BorderStyle = BorderStyle.None
        End If

    End Sub

    ''' <summary>
    ''' �h���b�v�_�E���P�̂̊�������
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableHtmlSelect(ByRef ctrl As HtmlSelect, _
                                 ByVal enabled As Boolean)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnableHtmlSelect", _
                                                    ctrl, enabled)

        If enabled Then
            ctrl.Disabled = False
        Else
            ctrl.Disabled = True
        End If

    End Sub

    ''' <summary>
    ''' ���W�I�{�^���P�̂̊�������
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableRadio(ByRef ctrl As HtmlInputRadioButton, _
                            ByVal enabled As Boolean)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnableHtmlSelect", _
                                                    ctrl, enabled)

        If enabled Then
            ctrl.Disabled = False
        Else
            ctrl.Disabled = True
        End If

    End Sub


#End Region

#Region "��ʃR���g���[���̕ύX�ӏ��Ή�"

    ''' <summary>
    ''' ��ʃR���g���[���̒l���������A�����񉻂���(�S����)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringAll() As String

        Dim sb As New StringBuilder

        '���\������
        '�����X�R�[�h
        sb.Append(Me.TextKameitenCd.Value & EarthConst.SEP_STRING)

        '�����˗�����
        sb.Append(Me.TextDoujiIraiTousuu.Value & EarthConst.SEP_STRING)
        '�����p�rTEXT
        sb.Append(Me.TextTatemonoYoutoCd.Value & EarthConst.SEP_STRING)
        '�����p�rDDL
        sb.Append(Me.SelectTatemonoYouto.SelectedValue & EarthConst.SEP_STRING)

        '������ЃR�[�h
        sb.Append(Me.TextTyousaKaishaCd.Value & EarthConst.SEP_STRING)

        '�������@TEXT
        sb.Append(Me.TextTyousaHouhouCd.Value & EarthConst.SEP_STRING)
        '�������@DDL
        sb.Append(Me.SelectTyousaHouhou.SelectedValue & EarthConst.SEP_STRING)

        '�����T�vDDL
        sb.Append(Me.SelectTyousaGaiyou.Value & EarthConst.SEP_STRING)

        '���i1DDL
        sb.Append(Me.SelectSyouhin1.SelectedValue & EarthConst.SEP_STRING)

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
                .Add("0", "�����X�R�[�h")
                .Add("1", "�����˗�����")
                .Add("2", "�����p�rTEXT")
                .Add("3", "�����p�rDDL")
                .Add("4", "������ЃR�[�h")
                .Add("5", "�������@TEXT")
                .Add("6", "�������@DDL")
                .Add("7", "�����T�v")
                .Add("8", "���i1")
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
            .Add("0", Me.TextKameitenCd)
            .Add("1", Me.TextDoujiIraiTousuu)
            .Add("2", Me.TextTatemonoYoutoCd)
            .Add("3", Me.SelectTatemonoYouto)
            .Add("4", Me.TextTyousaKaishaCd)
            .Add("5", Me.TextTyousaHouhouCd)
            .Add("6", Me.SelectTyousaHouhou)
            .Add("7", Me.SelectTyousaGaiyou)
            .Add("8", Me.SelectSyouhin1)
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

    ''' <summary>
    ''' ������R�̐ݒ�
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <remarks></remarks>
    Private Sub setTorikesiRiyuu(ByVal strKbn As String, ByVal strKameitenCd As String)

        '�F�ւ������Ώۂ̃R���g���[����z��Ɋi�[(��������R�e�L�X�g�{�b�N�X�ȊO)
        Dim objArray() As Object = New Object() {Me.TextKameitenCd, Me.TextKameitenMei}

        '������R�Ɖ����X���̕����F�ݒ�
        cLogic.GetKameitenTorikesiRiyuu(strKbn _
                                        , strKameitenCd _
                                        , Me.TextTorikesiRiyuu _
                                        , True _
                                        , False _
                                        , objArray)

    End Sub

#End Region

#Region "�p�u���b�N���\�b�h"
    ''' <summary>
    ''' �G���[�`�F�b�N
    ''' </summary>
    ''' <param name="errMess"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <param name="typeWord"></param>
    ''' <param name="strChgPartMess"></param>
    ''' <remarks></remarks>
    Public Sub CheckInput(ByRef errMess As String, _
                          ByRef arrFocusTargetCtrl As List(Of Control), _
                          ByVal typeWord As String, _
                          ByRef strChgPartMess As String, _
                          ByVal strLavelSyouhin1Cd As String)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckInput", _
                                                    errMess, _
                                                    arrFocusTargetCtrl, _
                                                    typeWord, _
                                                    strChgPartMess, _
                                                    strLavelSyouhin1Cd)

        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban
        Dim strErrMsg As String = String.Empty '��Ɨp
        Dim blnKamentenFlg As Boolean = False
        Dim kameitenSearchLogic As New KameitenSearchLogic

        Dim setuzoku As String = typeWord
        If typeWord <> "" Then
            setuzoku = typeWord & "�F"
        End If

        '��r���{(�ύX�`�F�b�N)
        Dim strChgVal As String = Me.getCtrlValuesStringAll()
        If Me.HiddenOpenValue.Value <> strChgVal Then
            Dim strCtrlNm As String = String.Empty
            strCtrlNm = Me.ChkChgCtrlName(Me.HiddenOpenValue.Value, strChgVal, Me.HiddenKeyValue.Value) '�ύX�ӏ����̎擾
            strChgPartMess += "[" & typeWord & "]\r\n" & strCtrlNm & "\r\n"
        End If

        '�K�{�`�F�b�N
        If TextKameitenCd.Value = "" Or TextKameitenMei.Value = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", setuzoku & "�����X")
            arrFocusTargetCtrl.Add(TextKameitenCd)
            blnKamentenFlg = True '�t���O�𗧂Ă�
        End If
        If TextTatemonoYoutoCd.Value = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", setuzoku & "�����p�r")
            arrFocusTargetCtrl.Add(TextTatemonoYoutoCd)
        End If
        If TextTyousaKaishaCd.Value = "" Or TextTyousaKaishaNm.Value = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", setuzoku & "�������")
            arrFocusTargetCtrl.Add(TextTyousaKaishaCd)
        End If
        If SelectSyouhin1.SelectedValue = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", setuzoku & "���i1")
            arrFocusTargetCtrl.Add(SelectSyouhin1)
        End If
        If TextTyousaHouhouCd.Value = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", setuzoku & "�������@")
            arrFocusTargetCtrl.Add(TextTyousaHouhouCd)
        End If

        '�R�[�h���͒l�ύX�`�F�b�N
        If TextKameitenCd.Value <> HiddenkameitenCdOld.Value Then
            errMess += Messages.MSG030E.Replace("@PARAM1", setuzoku & "�����X�R�[�h")
            arrFocusTargetCtrl.Add(ButtonKameitenKensaku)
            blnKamentenFlg = True '�t���O�𗧂Ă�
        End If
        If TextTyousaKaishaCd.Value <> HiddenTysKaisyaCdOld.Value Then
            errMess += Messages.MSG030E.Replace("@PARAM1", setuzoku & "������ЃR�[�h")
            arrFocusTargetCtrl.Add(ButtonTyousaKaishaKensaku)
        End If

        '�����`�F�b�N
        If jBn.SuutiStrCheck(TextDoujiIraiTousuu.Value, 4, 0) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", setuzoku & _
                                                "�����˗�����").Replace("@PARAM2", "4").Replace("@PARAM3", "0")
            arrFocusTargetCtrl.Add(TextDoujiIraiTousuu)
        End If

        '���i1�R�[�h���ك`�F�b�N
        If SelectSyouhin1.SelectedValue <> strLavelSyouhin1Cd Then
            '�G���[���b�Z�[�W�\��
            errMess += Messages.MSG041E.Replace("�͈͎w��", "���i1�̑I����e�ƕ\�����x��")
            arrFocusTargetCtrl.Add(SelectSyouhin1)
        End If

        '���̑��`�F�b�N
        '�������T�v/�����˗������`�F�b�N
        strErrMsg = String.Empty
        If cbLogic.ChkErrTysGaiyou(Me.SelectTyousaGaiyou.Value, Me.TextDoujiIraiTousuu.Value, strErrMsg) = False Then
            errMess += strErrMsg
            arrFocusTargetCtrl.Add(Me.TextDoujiIraiTousuu)
        End If
        '���r���_�[���ӎ����`�F�b�N(�����X�֘A�̃G���[���Ȃ��ꍇ�`�F�b�N����)
        strErrMsg = String.Empty
        If blnKamentenFlg = False Then
            If kameitenSearchLogic.ChkBuilderData13(TextKameitenCd.Value) Then
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.TrueString
            Else
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.FalseString
            End If
            If cbLogic.ChkErrBuilderData(Me.SelectTyousaGaiyou.Value, Me.TextKameitenCd.Value, Me.HiddenKameitenTyuuiJikou.Value, strErrMsg) = False Then
                errMess += strErrMsg
                arrFocusTargetCtrl.Add(Me.TextKameitenCd)
            End If
        End If

    End Sub

    ''' <summary>
    ''' ������ЃR�[�h�Ɩ��̂�ύX�O�̒l�ɖ߂�
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReturnTyousakaisyaCdNm(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.TextTyousaKaishaCd.Value = Me.HiddenTysKaisyaCdOld.Value
        Me.TextTyousaKaishaNm.Value = Me.HiddenTysKaisyaNmOld.Value
    End Sub

#End Region

End Class
