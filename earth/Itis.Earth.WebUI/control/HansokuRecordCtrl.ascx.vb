
Partial Public Class HansokuRecordCtrl
    Inherits System.Web.UI.UserControl
    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�����ݒ�p�t���O�񋓑�"
    ''' <summary>
    ''' ������
    ''' </summary>
    ''' <remarks></remarks>
    Enum enSeikyuuSaki
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <remarks></remarks>
        Tyoku = 0
        ''' <summary>
        ''' ��
        ''' </summary>
        ''' <remarks></remarks>
        Hoka = 1
    End Enum
    Private eSaki As enSeikyuuSaki
    ''' <summary>
    ''' �n��R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Enum enKeiretu
        ''' <summary>
        ''' 3�n��
        ''' </summary>
        ''' <remarks></remarks>
        SanK = 0
        ''' <summary>
        ''' 3�n��ȊO
        ''' </summary>
        ''' <remarks></remarks>
        NotSanK = 1
    End Enum
    Private eKretu As enKeiretu
    ''' <summary>
    ''' ���z�ύX�C�x���g����i�̑��i�j
    ''' </summary>
    ''' <remarks></remarks>
    Enum enTxtChgCtrlHansoku
        JituGaku = 1
        KoumuGaku = 2
        Reset = -1
    End Enum

    '�R���g���[���\�����[�h
    Public Enum pDisplayMode
        ''' <summary>
        ''' �X�ʃf�[�^�C��
        ''' </summary>
        ''' <remarks></remarks>
        TENBETU = 1
        ''' <summary>
        ''' �̑��i����
        ''' </summary>
        ''' <remarks></remarks>
        HANSOKU = 2
        ''' <summary>
        ''' ���㏈���ϔ̑��i�Q��
        ''' </summary>
        ''' <remarks></remarks>
        SANSYOU = 3
    End Enum
#Region "�R�n��"
    Private Const KEIRETU_TH As String = EarthConst.KEIRETU_TH
    Private Const KEIRETU_01 As String = EarthConst.KEIRETU_AIFURU
    Private Const KEIRETU_NF As String = EarthConst.KEIRETU_WANDA
#End Region
#Region "�q�ɃR�[�h"
    Private SOUKO_CD_TOUROKU As String = EarthConst.SOUKO_CD_TOUROKU_TESUURYOU
    Private SOUKO_CD_TOOL As String = EarthConst.SOUKO_CD_SYOKI_TOOL_RYOU
    Private SOUKO_CD_NOT_FC As String = EarthConst.SOUKO_CD_FC_GAI_HANSOKUHIN
    Private SOUKO_CD_FC As String = EarthConst.SOUKO_CD_FC_HANSOKUHIN
#End Region
#End Region
#Region "�H���X�Ŕ������z��������p"
    Private clsCL As New CommonLogic
    Private clsJSM As New JibanSessionManager
#End Region
#Region "�����X���[�h"
    ''' <summary>
    ''' �����X���[�h
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum IsFcMode
        ''' <summary>
        ''' FC�����X
        ''' </summary>
        ''' <remarks></remarks>
        FC = 1
        ''' <summary>
        ''' FC�ȊO�����X
        ''' </summary>
        ''' <remarks></remarks>
        NOT_FC = 0
    End Enum
    ''' <summary>
    ''' �����X���[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private enIsFc As IsFcMode
#End Region
    ''' <summary>
    ''' �r�W�l�X���W�b�N���ʃN���X
    ''' </summary>
    ''' <remarks></remarks>
    Private cbLogic As New CommonBizLogic

    ''' <summary>
    ''' �}�X�^�[�y�[�W��AjaxScriptManager
    ''' </summary>
    ''' <remarks></remarks>
    Private masterAjaxSM As New ScriptManager

    ''' <summary>
    ''' �R���g���[���̕\�����[�h�i�X�ʃ��[�h�j
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DisplayMode
        ''' <summary>
        ''' �X�ʃf�[�^�C��
        ''' </summary>
        ''' <remarks></remarks>
        TENBETU = 1
        ''' <summary>
        ''' �̑��i����
        ''' </summary>
        ''' <remarks></remarks>
        HANSOKU = 2
        ''' <summary>
        ''' ���㏈���ϔ̑��i�Q��
        ''' </summary>
        ''' <remarks></remarks>
        SANSYOU = 3
    End Enum
    ''' <summary>
    ''' �R���g���[���̕\�����[�h�i�X�ʃ��[�h�j
    ''' </summary>
    ''' <remarks></remarks>
    Private enMode As DisplayMode

    Dim user_info As New LoginUserInfo

    Private clsLogic As New TenbetuSyuuseiLogic

#Region "���[�U�[�R���g���[���ւ̊O������̃A�N�Z�X�pGetter/Setter"
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for hiddenMiseCd�i�����X�R�[�h�j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenMiseCd() As HiddenField
        Get
            Return Me.hiddenMiseCd
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenMiseCd = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for HiddenSoukoCd�i�q�ɃR�[�h�j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenSoukoCd() As HiddenField
        Get
            Return Me.hiddenSoukoCd
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenSoukoCd = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for HiddenNyuuryokuDate�i���͓��j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenNyuuryokuDate() As HiddenField
        Get
            Return Me.hiddenNyuuryokuDate
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenNyuuryokuDate = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for HiddenNyuuryokuDateNo�i���͓�NO�j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Property AccHiddenNyuuryokuDateNo() As HiddenField
        Get
            Return Me.hiddenNyuuryokuDateNo
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenNyuuryokuDateNo = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextHassouDate�i�������j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextHassouDate() As HtmlInputText
        Get
            Return Me.TextHassouDate
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextHassouDate = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextSeikyuusyoHakkouBi�i���������s���j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSeikyuusyoHakkouBi() As HtmlInputText
        Get
            Return Me.TextSeikyuusyoHakkouBi
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextSeikyuusyoHakkouBi = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextUriageNengappi�i����N�����j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextUriageNengappi() As HtmlInputText
        Get
            Return Me.TextUriageNengappi
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextUriageNengappi = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextDenpyouUriDateDisplay�i�`�[����N�������x���j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextDenpyouUriDateDisplay() As HtmlInputText
        Get
            Return TextDenpyouUriDateDisplay
        End Get
        Set(ByVal value As HtmlInputText)
            TextDenpyouUriDateDisplay = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextDenpyouUriDate (�`�[����N����)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextDenpyouUriDate() As HtmlInputText
        Get
            Return TextDenpyouUriDate
        End Get
        Set(ByVal value As HtmlInputText)
            TextDenpyouUriDate = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextSyouhinCd�i���i�R�[�h�j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSyouhinCd() As HtmlInputText
        Get
            Return Me.TextSyouhinCd
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextSyouhinCd = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for HiddenSyouhinCdOld�i���i�R�[�h�ޔ�p�j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenSyouhinCdOld() As HiddenField
        Get
            Return Me.hiddenOldSyouhinCd
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenOldSyouhinCd = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextJituSeikyuuTanka�i�������P���j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextJituSeikyuuTanka() As HtmlInputText
        Get
            Return Me.TextJituSeikyuuTanka
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextJituSeikyuuTanka = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextSuuryou�i���ʁj
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSuuRyou() As HtmlInputText
        Get
            Return Me.TextSuuryou
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextSuuryou = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextSyouhizeiGaku�i����Łj
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSyouhizeiGaku() As HtmlInputText
        Get
            Return Me.TextSyouhizeiGaku
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextSyouhizeiGaku = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextZeikomiKingaku�i�ō����z�j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextZeikomiKingaku() As HtmlInputText
        Get
            Return Me.TextZeikomiKingaku
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextZeikomiKingaku = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for HiddenZeiKbn�i�ŋ敪�j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenZeiKbn() As HiddenField
        Get
            Return Me.hiddenZeiKbn
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenZeiKbn = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextKoumutenSeikyuuTanka�i�H���X�����P���j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextKoumutenSeikyuuTanka() As HtmlInputText
        Get
            Return Me.TextKoumutenSeikyuuTanka
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextKoumutenSeikyuuTanka = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for HiddenZeiritu�i�ŗ��j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenZeiritu() As HiddenField
        Get
            Return Me.hiddenZeiritu
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenZeiritu = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for HiddenUpdDateTime�i�X�V�����j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenUpdDateTime() As HiddenField
        Get
            Return Me.hiddenUpdateTime
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenUpdateTime = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for hiddenIsFc�i�X�ʃ��[�h�j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenIsFc() As HiddenField
        Get
            Return Me.hiddenIsFc
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenIsFc = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for HiddenOpenValues(��ʓǍ��ݎ��̒l)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenOpenValues() As HiddenField
        Get
            Return Me.HiddenOpenValues
        End Get
        Set(ByVal value As HiddenField)
            Me.HiddenOpenValues = value
        End Set
    End Property
    ''' <summary>
    ''' �O������̃A�N�Z�X�p for HiddenOpenValues(����v��FLG)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenUriKeijyouFlg() As HiddenField
        Get
            Return Me.HiddenUriKeijyouFlg
        End Get
        Set(ByVal value As HiddenField)
            Me.HiddenUriKeijyouFlg = value
        End Set
    End Property

#Region "SQL��ʔ��f�t���O"
    Public Property AccSqlTypeFlg() As HiddenField
        Get
            Return Me.hiddenSqlTypeFlg
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenSqlTypeFlg = value
        End Set
    End Property
    ''' <summary>
    ''' SQL��ʔ��f�t���O�񋓑�
    ''' </summary>
    ''' <remarks></remarks>
    Enum enSqlTypeFlg
        ''' <summary>
        ''' �X�V
        ''' </summary>
        ''' <remarks></remarks>
        UPDATE = EarthConst.enSqlTypeFlg.UPDATE
        ''' <summary>
        ''' �o�^
        ''' </summary>
        ''' <remarks></remarks>
        INSERT = EarthConst.enSqlTypeFlg.INSERT
        ''' <summary>
        ''' �폜
        ''' </summary>
        ''' <remarks></remarks>
        DELETE = EarthConst.enSqlTypeFlg.DELETE
    End Enum
#End Region
#End Region

#Region "�y�[�W����"
    ''' <summary>
    ''' �y�[�W�̏�������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Me.TextHassouDate.Attributes.Add("onfocus", "setTempValueForOnBlur(this);")
        Me.TextSyouhinCd.Attributes.Add("onfocus", "setTempValueForOnBlur(this);")
        Me.TextKoumutenSeikyuuTanka.Attributes.Add("onfocus", "removeFig(this);setTempValueForOnBlur(this);")
        Me.TextJituSeikyuuTanka.Attributes.Add("onfocus", "removeFig(this);setTempValueForOnBlur(this);")
        Me.TextSuuryou.Attributes.Add("onfocus", "removeFig(this);setTempValueForOnBlur(this);")
        Me.TextSyouhizeiGaku.Attributes.Add("onfocus", "removeFig(this);setTempValueForOnBlur(this);")
        Me.TextUriageNengappi.Attributes.Add("onfocus", "setTempValueForOnBlur(this);")
        Me.TextDenpyouUriDate.Attributes.Add("onfocus", "setTempValueForOnBlur(this);")

        Me.TextHassouDate.Attributes.Add("onblur", "if(checkDate(this)){if(checkTempValueForOnBlur(this)){objEBI('" & Me.buttonChgHassouDate.ClientID & "').click();}}")
        Me.TextSyouhinCd.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){objEBI('" & Me.buttonChgSyouhinCd.ClientID & "').click();}")
        Me.TextKoumutenSeikyuuTanka.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkKingaku(this,false)){objEBI('" & Me.buttonChgKoumu.ClientID & "').click();}}else{if(checkKingaku(this,false));}")
        Me.TextJituSeikyuuTanka.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkKingaku(this,false)){objEBI('" & Me.buttonChgJitu.ClientID & "').click();}}else{if(checkKingaku(this,false));}")
        Me.TextSuuryou.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkKingaku(this,false)){objEBI('" & Me.buttonChgSuuryou.ClientID & "').click();}}else{if(checkKingaku(this,false));}")
        Me.TextSyouhizeiGaku.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkKingaku(this,false)){objEBI('" & Me.buttonChgSyouhiZei.ClientID & "').click();}}else{if(checkKingaku(this,false));}")
        Me.TextUriageNengappi.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkDate(this)){objEBI('" & Me.buttonChgUriDate.ClientID & "').click();}}else{if(checkDate(this));}")
        Me.TextDenpyouUriDate.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkDate(this)){objEBI('" & Me.buttonChgDenUriDate.ClientID & "').click();}}else{if(checkDate(this));}")

        Me.buttonGyouSakujoCall.Attributes.Add("onclick", "deleteConfirm(" & Me.buttonGyouSakujo.ClientID & "," & Me.TextSyouhinCd.ClientID & ");")

        '���z���ڂ�MaxLength��ݒ�
        Me.TextZeinukiKingaku.MaxLength = 7
        Me.TextZeikomiKingaku.MaxLength = 7
        Me.TextSyouhizeiGaku.MaxLength = 7
        Me.TextKoumutenSeikyuuTanka.MaxLength = 7
        Me.TextJituSeikyuuTanka.MaxLength = 7
        '���ʂ�MaxLength��ݒ�
        Me.TextSuuryou.MaxLength = 3
        '���t���ڂ�MaxLength��ݒ�
        Me.TextUriageNengappi.MaxLength = 10
        Me.TextSeikyuusyoHakkouBi.MaxLength = 10
        Me.TextHassouDate.MaxLength = 10
        Me.TextDenpyouUriDate.MaxLength = 10
        '���i�R�[�h��MaxLength��ݒ�
        Me.TextSyouhinCd.MaxLength = 8

        'SQL��ʔ��f�t���O���X�V�ɃZ�b�g
        Me.hiddenSqlTypeFlg.Value = enSqlTypeFlg.UPDATE
    End Sub

    ''' <summary>
    ''' �y�[�W���[�h���̃C�x���g
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
        jBn.UserAuth(user_info)

        Dim intKeiriKengen As Integer = user_info.KeiriGyoumuKengen
        If intKeiriKengen <> "-1" Then
            Me.TextSyouhizeiGaku.Attributes.Add("class", "kingaku readOnlyStyle")
            Me.TextSyouhizeiGaku.Attributes.Add("readonly", "true")
        End If

        If Not IsPostBack Then
            '��ʓǍ��ݎ��̒l��Hidden���ڂɑޔ�
            setOpenValues()
        End If

    End Sub

    ''' <summary>
    ''' �y�[�W�����_�[
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Me.TextKoumutenSeikyuuTanka.Value = StrNum2Str(Me.TextKoumutenSeikyuuTanka.Value)
        Me.TextJituSeikyuuTanka.Value = StrNum2Str(Me.TextJituSeikyuuTanka.Value)
        Me.TextSuuryou.Value = StrNum2Str(Me.TextSuuryou.Value)
        Me.TextZeinukiKingaku.Value = StrNum2Str(Me.TextZeinukiKingaku.Value)
        Me.TextSyouhizeiGaku.Value = StrNum2Str(Me.TextSyouhizeiGaku.Value)
        Me.TextZeikomiKingaku.Value = StrNum2Str(Me.TextZeikomiKingaku.Value)
    End Sub
#End Region

#Region "��ʕ\������"
    ''' <summary>
    ''' �R���g���[���ɃC���f�b�N�X�l�����蓖�Ă�
    ''' </summary>
    ''' <param name="index"></param>
    ''' <remarks></remarks>
    Public Sub SetIdIndex(ByVal index As Integer, _
                          ByVal tenbetuMode As Integer, _
                          ByVal isFC As Integer)
        ' �s(tr�^�O)
        TrHansokuRecord.ID = TrHansokuRecord.ID & "_" & index.ToString()
        ' �e����

        If tenbetuMode <> DisplayMode.TENBETU Then
            TdHassouDate.ID = TdHassouDate.ID & "_" & index.ToString()
            TextHassouDate.ID = TextHassouDate.ID & "_" & index.ToString()
        Else
            TdHassouDate.Visible = False
            TextHassouDate.Visible = False
            ' �Ŕ����z�͔̑��i�����̂�
            TdZeinukiKingaku.Visible = False
        End If
        enMode = tenbetuMode

        '�X�ʐ������[�h�ɂ���ăR���g���[���\����؂�ւ���
        If enMode = DisplayMode.HANSOKU Or enMode = DisplayMode.SANSYOU Then
            Me.tdDenUriDate.Visible = False
        End If

        ' FC�̏ꍇ
        If isFC = 1 Then
            TdKoumutenSeikyuu.Visible = False
        End If

        TextSyouhinCd.ID = TextSyouhinCd.ID & "_" & index.ToString()
        buttonKensaku.ID = buttonKensaku.ID & "_" & index.ToString()
        TextSyouhinMei.ID = TextSyouhinMei.ID & "_" & index.ToString() & "_" & index.ToString()
        TextKoumutenSeikyuuTanka.ID = TextKoumutenSeikyuuTanka.ID & "_" & index.ToString()
        TextSuuryou.ID = TextSuuryou.ID & "_" & index.ToString()
        TextJituSeikyuuTanka.ID = TextJituSeikyuuTanka.ID & "_" & index.ToString()
        TextZeinukiKingaku.ID = TextZeinukiKingaku.ID & "_" & index.ToString()
        TextSyouhizeiGaku.ID = TextSyouhizeiGaku.ID & "_" & index.ToString()
        TextZeikomiKingaku.ID = TextZeikomiKingaku.ID & "_" & index.ToString()
        TextSeikyuusyoHakkouBi.ID = TextSeikyuusyoHakkouBi.ID & "_" & index.ToString()
        TextUriageNengappi.ID = TextUriageNengappi.ID & "_" & index.ToString()
        TextDenpyouUriDate.ID = TextDenpyouUriDate.ID & "_" & index.ToString()

        buttonChgSyouhinCd.ID = buttonChgSyouhinCd.ID & "_" & index.ToString()
        buttonChgKoumu.ID = buttonChgKoumu.ID & "_" & index.ToString()
        buttonChgJitu.ID = buttonChgJitu.ID & "_" & index.ToString()
        buttonChgSuuryou.ID = buttonChgSuuryou.ID & "_" & index.ToString()

        hiddenMiseCd.ID = hiddenMiseCd.ID & "_" & index.ToString()
        hiddenTyousaSeikyuuSaki.ID = hiddenTyousaSeikyuuSaki.ID & "_" & index.ToString()
        hiddenHansokuHinSeikyuuSaki.ID = hiddenHansokuHinSeikyuuSaki.ID & "_" & index.ToString()
        hiddenKeiretuCd.ID = hiddenKeiretuCd.ID & "_" & index.ToString()

        If tenbetuMode <> DisplayMode.SANSYOU Then
            TdGyouSyori.ID = TdGyouSyori.ID & "_" & index.ToString()
            buttonGyouSakujo.ID = buttonGyouSakujo.ID & "_" & index.ToString()
            If tenbetuMode = DisplayMode.HANSOKU Then
                Me.TextUriageNengappi.Attributes("readOnly") = "readonly"
                Me.TextUriageNengappi.Attributes("class") += " readOnlyStyle"
                Me.TextUriageNengappi.Attributes("tabindex") = -1
                Me.TextDenpyouUriDate.Attributes("readOnly") = "readonly"
                Me.TextDenpyouUriDate.Attributes("class") += " readOnlyStyle"
                Me.TextDenpyouUriDate.Attributes("tabindex") = -1
            End If
        Else
            TextHassouDate.Attributes("readOnly") = "readonly"
            TextHassouDate.Attributes("class") += " readOnlyStyle"
            TextSyouhinCd.Attributes("readOnly") = "readonly"
            TextSyouhinCd.Attributes("class") += " readOnlyStyle"
            buttonKensaku.Visible = False
            TextSyouhinMei.Attributes("readOnly") = "readonly"
            TextKoumutenSeikyuuTanka.Attributes("readOnly") = "readonly"
            TextKoumutenSeikyuuTanka.Attributes("class") += " readOnlyStyle"
            TextSuuryou.Attributes("readOnly") = "readonly"
            TextSuuryou.Attributes("class") += " readOnlyStyle"
            TextJituSeikyuuTanka.Attributes("readOnly") = "readonly"
            TextJituSeikyuuTanka.Attributes("class") += " readOnlyStyle"
            TextZeinukiKingaku.Attributes("readOnly") = "readonly"
            TextSyouhizeiGaku.Attributes("readOnly") = "readonly"
            TextZeikomiKingaku.Attributes("readOnly") = "readonly"
            TextSeikyuusyoHakkouBi.Attributes("readOnly") = "readonly"
            TextSeikyuusyoHakkouBi.Attributes("class") += " readOnlyStyle"
            TextUriageNengappi.Attributes("readOnly") = "readonly"
            TextUriageNengappi.Attributes("class") += " readOnlyStyle"
            Me.TextDenpyouUriDate.Attributes("readOnly") = "readonly"
            Me.TextDenpyouUriDate.Attributes("class") += " readOnlyStyle"
            Me.TextDenpyouUriDate.Attributes("tabindex") = -1
            TdGyouSyori.Visible = False
            buttonGyouSakujo.Visible = False
        End If
        If Me.TrHansokuRecord.Style("display") = "none" Then
            'SQL��ʔ��f�t���O���폜�ɃZ�b�g
            Me.hiddenSqlTypeFlg.Value = enSqlTypeFlg.DELETE
        Else
            'SQL��ʔ��f�t���O���X�V�ɃZ�b�g
            Me.hiddenSqlTypeFlg.Value = enSqlTypeFlg.UPDATE
        End If
    End Sub

    ''' <summary>
    ''' �R���g���[���ɒl���Z�b�g����
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetValue(ByVal recHansoku As TenbetuHansokuHinRecord)

        '��ʏ����擾
        With recHansoku
            Me.hiddenMiseCd.Value = .MiseCd
            Me.hiddenNyuuryokuDate.Value = DtTime2Str(.NyuuryokuDate, True)
            Me.hiddenNyuuryokuDateNo.Value = .NyuuryokuDateNo
            Me.hiddenTyousaSeikyuuSaki.Value = .TysSeikyuuSaki
            Me.hiddenHansokuHinSeikyuuSaki.Value = .HansokuhinSeikyuusaki
            Me.hiddenSoukoCd.Value = .BunruiCd
            Me.TextHassouDate.Value = DtTime2Str(.HassouDate)
            Me.TextSyouhinCd.Value = .SyouhinCd
            Me.hiddenOldSyouhinCd.Value = .SyouhinCd
            Me.TextSyouhinMei.Value = .SyouhinMei
            Me.TextKoumutenSeikyuuTanka.Value = .KoumutenSeikyuuTanka
            Me.hiddenZeiritu.Value = .Zeiritu
            Me.TextJituSeikyuuTanka.Value = .Tanka
            Me.TextSuuryou.Value = .Suu
            Me.TextZeinukiKingaku.Value = Long.Parse(.Tanka) * Long.Parse(.Suu)
            Me.TextSyouhizeiGaku.Value = .SyouhiZei
            Me.TextZeikomiKingaku.Value = .ZeikomiGaku
            Me.TextSeikyuusyoHakkouBi.Value = DtTime2Str(.SeikyuusyoHakDate)
            Me.TextUriageNengappi.Value = DtTime2Str(.UriDate)
            Me.TextDenpyouUriDateDisplay.Value = DtTime2Str(.DenpyouUriDate)
            Me.TextDenpyouUriDate.Value = DtTime2Str(.DenpyouUriDate)
            Me.hiddenUpdateTime.Value = DtTime2Str(.UpdDatetime, True)
            Me.hiddenZeiKbn.Value = .ZeiKbn
            Me.hiddenIsFc.Value = .IsFc
            Me.hiddenKeiretuCd.Value = .KeiretuCd
            Me.HiddenUriKeijyouFlg.Value = .UriKeijyouFlg
            enIsFc = .IsFc
        End With

        '��ʎ����ݒ蔻�f�i�̑��i�j
        judgeHansokuAutoSetting(Me.TextSyouhinCd.Value)
        If eSaki = enSeikyuuSaki.Hoka And eKretu = enKeiretu.NotSanK Then
            Me.TextKoumutenSeikyuuTanka.Value = "0"
        End If

        '�H���X�Ŕ������z��������
        activeControlKoumuten()
    End Sub
#End Region

#Region "��ʍ��ڕύX������"
    ''' <summary>
    ''' �������ύX��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgHassouDate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgHassouDate.ServerClick

        sendClassDispInfo(clsLogic)

        If enMode = DisplayMode.HANSOKU Then
            '���������s���̐ݒ�
            If Me.TextSeikyuusyoHakkouBi.Value = "" Then
                Me.TextSeikyuusyoHakkouBi.Value = clsLogic.GetHansokuhinSeikyuusyoHakkoudate(Me.hiddenMiseCd.Value, Me.TextSyouhinCd.Value)
            End If
            If Me.TextUriageNengappi.Value = "" Then
                Me.TextUriageNengappi.Value = DateTime.Today
            End If
            '�`�[����N�����C��
            If Me.TextDenpyouUriDate.Value = "" Then
                Me.TextDenpyouUriDate.Value = Me.TextUriageNengappi.Value
            End If
        End If

        '�t�H�[�J�X�̐ݒ�
        SetFocusAjax(Me.TextSyouhinCd)
    End Sub

    ''' <summary>
    ''' ���iCD�ύX��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgSyouhinCd_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgSyouhinCd.ServerClick
        Dim clsJibanL As New JibanLogic
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        Dim intJituGaku As Integer
        Dim intSuuryou As Integer
        Dim intZei As Integer
        Dim intZeiNukiGaku As Integer
        Dim intZeiKomiGaku As Integer
        Dim intKoumuGaku As Integer
        Dim blnFlg As Boolean
        Dim MLogic As New MessageLogic
        Dim strZeiKbn As String = String.Empty      '�ŋ敪
        Dim strZeiritu As String = String.Empty     '�ŗ�
        Dim strSyouhinCd As String = String.Empty   '���i�R�[�h

        '�t�H�[�J�X�̐ݒ�
        SetFocusAjax(Me.buttonKensaku)

        sendClassDispInfo(clsLogic)

        '���z�ύX�C�x���g����i�̑��i�j�ݒ�
        Me.hiddenTxtChgCtrlHansoku.Value = enTxtChgCtrlHansoku.Reset.ToString

        If Me.TextSyouhinCd.Value = "" Then
            Me.TextSyouhinMei.Value = ""
            Me.TextKoumutenSeikyuuTanka.Value = ""
            Me.TextJituSeikyuuTanka.Value = ""
            Me.TextSuuryou.Value = ""
            Me.TextSyouhizeiGaku.Value = ""
            Me.TextZeinukiKingaku.Value = ""
            Me.TextZeikomiKingaku.Value = ""
            If enMode <> DisplayMode.TENBETU Then
                Me.TextHassouDate.Value = ""
            End If
            Me.TextUriageNengappi.Value = ""
            Me.TextDenpyouUriDateDisplay.Value = String.Empty
            Me.TextDenpyouUriDate.Value = String.Empty
            Me.TextSeikyuusyoHakkouBi.Value = ""
            Me.hiddenOldSyouhinCd.Value = ""
            Exit Sub
        End If

        '���z�E���ʂ̎擾
        If Me.TextJituSeikyuuTanka.Value <> "" Then
            intJituGaku = CInt(Me.TextJituSeikyuuTanka.Value)
        End If
        If Me.TextKoumutenSeikyuuTanka.Value <> "" Then
            intKoumuGaku = CInt(Me.TextKoumutenSeikyuuTanka.Value)
        End If
        If Me.TextSuuryou.Value.Length = 0 Then
            Me.TextSuuryou.Value = 1
        End If
        If Me.TextSuuryou.Value <> "" Then
            intSuuryou = CInt(Me.TextSuuryou.Value)
        End If

        '���i���ׂ̎擾
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.TextSyouhinCd.Value, Me.hiddenSoukoCd.Value)

        '����N�����Ŕ��f���āA�������ŗ����擾����
        strSyouhinCd = Me.TextSyouhinCd.Value
        If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Value.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '�擾�����ŋ敪�E�ŗ����Z�b�g
            recSyouhinMeisai.Zeiritu = strZeiritu
            recSyouhinMeisai.ZeiKbn = strZeiKbn
        End If

        '��ʎ����ݒ蔻�f
        judgeHansokuAutoSetting(Me.TextSyouhinCd.Value)

        'FC�̏ꍇ
        If Me.hiddenIsFc.Value = IsFcMode.FC Then
            If recSyouhinMeisai IsNot Nothing Then
                intJituGaku = recSyouhinMeisai.HyoujunKkk
                intZeiNukiGaku = intJituGaku * intSuuryou
                intZei = Integer.Parse(Fix(intZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intZeiNukiGaku + intZei
            Else
                '�e�[�u���T�C�Y�Đݒ�
                TableResizeBeforAlert(Me)
                MLogic.AlertMessage(sender, Messages.MSG001E)
                Exit Sub
            End If
        Else
            '�����ݒ�p�^�[�����f
            Select Case True
                Case recSyouhinMeisai Is Nothing
                    Exit Sub
                   
                Case eSaki = enSeikyuuSaki.Tyoku
                    intKoumuGaku = recSyouhinMeisai.HyoujunKkk
                    intJituGaku = intKoumuGaku
                    intZeiNukiGaku = intJituGaku * intSuuryou
                    intZei = Integer.Parse(Fix(intZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                    intZeiKomiGaku = intZeiNukiGaku + intZei

                Case eSaki = enSeikyuuSaki.Hoka _
                 And eKretu = enKeiretu.SanK
                    intKoumuGaku = recSyouhinMeisai.HyoujunKkk
                    If intKoumuGaku = 0 Then
                        intJituGaku = 0
                    Else
                        blnFlg = clsJibanL.GetSeikyuuGaku(sender _
                                                        , 3 _
                                                        , Me.hiddenKeiretuCd.Value _
                                                        , recSyouhinMeisai.SyouhinCd _
                                                        , -1 _
                                                        , intJituGaku)
                    End If
                    intZeiNukiGaku = intJituGaku * intSuuryou
                    intZei = Integer.Parse(Fix(intZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                    intZeiKomiGaku = intZeiNukiGaku + intZei

                Case eSaki = enSeikyuuSaki.Hoka _
                 And eKretu = enKeiretu.NotSanK
                    intJituGaku = recSyouhinMeisai.HyoujunKkk
                    intZeiNukiGaku = intJituGaku * intSuuryou
                    intZei = Integer.Parse(Fix(intZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                    intZeiKomiGaku = intZeiNukiGaku + intZei

                Case Else
                    Exit Sub
            End Select
        End If
        Me.TextKoumutenSeikyuuTanka.Value = intKoumuGaku
        Me.TextJituSeikyuuTanka.Value = intJituGaku
        Me.TextSyouhizeiGaku.Value = intZei
        Me.TextZeikomiKingaku.Value = intZeiKomiGaku
        Me.TextZeinukiKingaku.Value = intZeiNukiGaku
        Me.hiddenOldSyouhinCd.Value = recSyouhinMeisai.SyouhinCd
        Me.hiddenZeiritu.Value = recSyouhinMeisai.Zeiritu

        If recSyouhinMeisai IsNot Nothing Then
            '���i���̐ݒ�
            Me.TextSyouhinMei.Value = recSyouhinMeisai.SyouhinMei
        End If

        If Me.hiddenSqlTypeFlg.Value = EarthConst.enSqlTypeFlg.INSERT Then
            '�ŋ敪�̐ݒ�
            Me.hiddenZeiKbn.Value = recSyouhinMeisai.ZeiKbn
        End If

        If enMode = DisplayMode.HANSOKU Then
            '���������s���̐ݒ�
            Me.TextSeikyuusyoHakkouBi.Value = clsLogic.GetHansokuhinSeikyuusyoHakkoudate(Me.hiddenMiseCd.Value, Me.TextSyouhinCd.Value)
        End If

        '�H���X�Ŕ������z��������
        activeControlKoumuten()

    End Sub

    ''' <summary>
    ''' �H���X�����P���ύX��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgKoumu_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgKoumu.ServerClick
        Dim clsJibanL As New JibanLogic
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        Dim intJituGaku As Integer
        Dim intSuuryou As Integer
        Dim intZei As Integer
        Dim intZeiNukiGaku As Integer
        Dim intZeiKomiGaku As Integer
        Dim intKoumuGaku As Integer
        Dim blnFlg As Boolean
        Dim MLogic As New MessageLogic

        '�t�H�[�J�X�̐ݒ�
        SetFocusAjax(Me.TextJituSeikyuuTanka)

        '���z�E���ʂ̎擾
        If Me.TextJituSeikyuuTanka.Value <> "" Then
            intJituGaku = CInt(Me.TextJituSeikyuuTanka.Value)
        End If
        If Me.TextKoumutenSeikyuuTanka.Value <> "" Then
            intKoumuGaku = CInt(Me.TextKoumutenSeikyuuTanka.Value)
        End If
        If Me.TextSuuryou.Value.Length = 0 Then
            Me.TextSuuryou.Value = 1
        End If
        If Me.TextSuuryou.Value <> "" Then
            intSuuryou = CInt(Me.TextSuuryou.Value)
        End If

        '���z�ύX�C�x���g����i�̑��i�j�ݒ�
        If Me.hiddenTxtChgCtrlHansoku.Value = enTxtChgCtrlHansoku.JituGaku.ToString Then
            Me.TextKoumutenSeikyuuTanka.Value = intKoumuGaku
            '�t�H�[�J�X�̐ݒ�
            SetFocusAjax(Me.TextSuuryou)
            Exit Sub
        Else
            '���z�ύX�C�x���g����i�̑��i�j�ݒ�
            Me.hiddenTxtChgCtrlHansoku.Value = enTxtChgCtrlHansoku.KoumuGaku.ToString
        End If

        If Me.TextSyouhinCd.Value = "" Then
            Exit Sub
        End If

        '���i���ׂ̎擾
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.TextSyouhinCd.Value, Me.hiddenSoukoCd.Value)

        '��ʎ����ݒ蔻�f
        judgeHansokuAutoSetting(Me.TextSyouhinCd.Value)

        '�����ݒ�p�^�[�����f
        Select Case True
            Case recSyouhinMeisai Is Nothing
                '�e�[�u���T�C�Y�Đݒ�
                TableResizeBeforAlert(Me)
                MLogic.AlertMessage(sender, Messages.MSG001E)
                Exit Sub

            Case eSaki = enSeikyuuSaki.Tyoku
                intJituGaku = intKoumuGaku
                intZeiNukiGaku = intJituGaku * intSuuryou
                intZei = Integer.Parse(Fix(intZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intZeiNukiGaku + intZei

            Case eSaki = enSeikyuuSaki.Hoka _
             And eKretu = enKeiretu.SanK
                blnFlg = clsJibanL.GetSeikyuuGaku(sender _
                                                , 5 _
                                                , Me.hiddenKeiretuCd.Value _
                                                , recSyouhinMeisai.SyouhinCd _
                                                , intKoumuGaku _
                                                , intJituGaku)
                intZeiNukiGaku = intJituGaku * intSuuryou
                intZei = Integer.Parse(Fix(intZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intZeiNukiGaku + intZei

            Case Else
                '�������Ȃ�
        End Select

        Me.TextJituSeikyuuTanka.Value = intJituGaku
        Me.TextSyouhizeiGaku.Value = intZei
        Me.TextZeikomiKingaku.Value = intZeiKomiGaku
        Me.TextKoumutenSeikyuuTanka.Value = intKoumuGaku
        Me.TextZeinukiKingaku.Value = intZeiNukiGaku

        '�H���X�Ŕ������z��������
        activeControlKoumuten()

    End Sub

    ''' <summary>
    ''' �������P���ύX��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgJitu_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgJitu.ServerClick
        Dim clsJibanL As New JibanLogic
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        Dim lngJituGaku As Long
        Dim intSuuryou As Integer
        Dim lngZei As Long
        Dim lngZeiNukiGaku As Long
        Dim lngZeiKomiGaku As Long
        Dim intKoumuGaku As Integer
        Dim blnFlg As Boolean
        Dim MLogic As New MessageLogic
        Dim decZeiRitu As Decimal
        Dim strZeiKbn As String = String.Empty      '�ŋ敪
        Dim strZeiritu As String = String.Empty     '�ŗ�
        Dim strSyouhinCd As String = String.Empty   '���i�R�[�h

        '�t�H�[�J�X�̐ݒ�
        SetFocusAjax(Me.TextSuuryou)

        '���z�E���ʂ̎擾
        If Me.TextJituSeikyuuTanka.Value <> "" Then
            lngJituGaku = CLng(Me.TextJituSeikyuuTanka.Value)
        End If
        If Me.TextKoumutenSeikyuuTanka.Value <> "" Then
            intKoumuGaku = CInt(Me.TextKoumutenSeikyuuTanka.Value)
        End If
        If Me.TextSuuryou.Value <> "" Then
            intSuuryou = CInt(Me.TextSuuryou.Value)
        End If
        '���ʂ̐ݒ�
        If Me.TextSuuryou.Value.Length = 0 Then
            Me.TextSuuryou.Value = 1
        End If
        '�ŗ��̐ݒ�
        decZeiRitu = Me.hiddenZeiritu.Value

        '���i�R�[�h���u�����N���͍Čv�Z���Ȃ�
        If Me.TextSyouhinCd.Value = "" Then
            Exit Sub
        End If

        '���z�ύX�C�x���g����i�̑��i�j�ݒ�
        If Me.hiddenTxtChgCtrlHansoku.Value = enTxtChgCtrlHansoku.KoumuGaku.ToString Then
            lngZeiNukiGaku = lngJituGaku * intSuuryou
            lngZei = Long.Parse(Fix(lngZeiNukiGaku * decZeiRitu))
            lngZeiKomiGaku = lngZeiNukiGaku + lngZei
            Me.TextJituSeikyuuTanka.Value = lngJituGaku
            Me.TextSyouhizeiGaku.Value = lngZei
            Me.TextZeikomiKingaku.Value = lngZeiKomiGaku
            Me.TextKoumutenSeikyuuTanka.Value = intKoumuGaku
            Me.TextZeinukiKingaku.Value = lngZeiNukiGaku

            Exit Sub
        Else
            '���z�ύX�C�x���g����i�̑��i�j�ݒ�
            Me.hiddenTxtChgCtrlHansoku.Value = enTxtChgCtrlHansoku.JituGaku.ToString
        End If

        '���i���ׂ̎擾
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.TextSyouhinCd.Value, Me.hiddenSoukoCd.Value)

        '����N�����Ŕ��f���āA�������ŗ����擾����
        strSyouhinCd = Me.TextSyouhinCd.Value
        If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Value.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '�擾�����ŋ敪�E�ŗ����Z�b�g
            recSyouhinMeisai.Zeiritu = strZeiritu
            recSyouhinMeisai.ZeiKbn = strZeiKbn
        End If

        '��ʎ����ݒ蔻�f
        judgeHansokuAutoSetting(Me.TextSyouhinCd.Value)

        '�����ݒ�p�^�[�����f
        Select Case True
            Case recSyouhinMeisai Is Nothing
                '�e�[�u���T�C�Y�Đݒ�
                TableResizeBeforAlert(Me)
                MLogic.AlertMessage(sender, Messages.MSG001E)
                Exit Sub

            Case eSaki = enSeikyuuSaki.Tyoku
                intKoumuGaku = lngJituGaku
                lngZeiNukiGaku = lngJituGaku * intSuuryou
                lngZei = Long.Parse(Fix(lngZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                lngZeiKomiGaku = lngZeiNukiGaku + lngZei

            Case eSaki = enSeikyuuSaki.Hoka _
             And eKretu = enKeiretu.SanK
                blnFlg = clsJibanL.GetSeikyuuGaku(sender _
                                                , 4 _
                                                , Me.hiddenKeiretuCd.Value _
                                                , recSyouhinMeisai.SyouhinCd _
                                                , lngJituGaku _
                                                , intKoumuGaku)
                lngZeiNukiGaku = lngJituGaku * intSuuryou
                lngZei = Long.Parse(Fix(lngZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                lngZeiKomiGaku = lngZeiNukiGaku + lngZei

            Case eSaki = enSeikyuuSaki.Hoka _
             And eKretu = enKeiretu.NotSanK
                lngZeiNukiGaku = lngJituGaku * intSuuryou
                lngZei = Long.Parse(Fix(lngZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                lngZeiKomiGaku = lngZeiNukiGaku + lngZei

            Case Else
                '�������Ȃ�
        End Select

        Me.TextJituSeikyuuTanka.Value = lngJituGaku
        Me.TextSyouhizeiGaku.Value = lngZei
        Me.TextZeikomiKingaku.Value = lngZeiKomiGaku
        Me.TextKoumutenSeikyuuTanka.Value = intKoumuGaku
        Me.TextZeinukiKingaku.Value = lngZeiNukiGaku

        '�H���X�Ŕ������z��������
        activeControlKoumuten()

    End Sub

    ''' <summary>
    ''' ���ʕύX��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgSuuryou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgSuuryou.ServerClick
        Dim lngJituGaku As Long
        Dim intSuuryou As Integer
        Dim lngZei As Long
        Dim lngZeiNukiGaku As Long
        Dim lngZeiKomiGaku As Long
        Dim intKoumuGaku As Integer
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        Dim MLogic As New MessageLogic
        Dim strZeiKbn As String = String.Empty      '�ŋ敪
        Dim strZeiritu As String = String.Empty     '�ŗ�
        Dim strSyouhinCd As String = String.Empty   '���i�R�[�h

        '�t�H�[�J�X�̐ݒ�
        SetFocusAjax(Me.TextSyouhizeiGaku)

        '���z�E���ʂ̎擾
        If Me.TextJituSeikyuuTanka.Value <> "" Then
            lngJituGaku = CLng(Me.TextJituSeikyuuTanka.Value)
        End If
        If Me.TextKoumutenSeikyuuTanka.Value <> "" Then
            intKoumuGaku = CInt(Me.TextKoumutenSeikyuuTanka.Value)
        End If
        If Me.TextSuuryou.Value.Length = 0 Then
            Me.TextSuuryou.Value = 1
        End If
        If Me.TextSuuryou.Value <> "" Then
            intSuuryou = CInt(Me.TextSuuryou.Value)
        End If

        If Me.TextSyouhinCd.Value = "" Then
            Exit Sub
        End If

        '���i���ׂ̎擾
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.TextSyouhinCd.Value, Me.hiddenSoukoCd.Value)

        '����N�����Ŕ��f���āA�������ŗ����擾����
        strSyouhinCd = Me.TextSyouhinCd.Value
        If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Value.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '�擾�����ŋ敪�E�ŗ����Z�b�g
            recSyouhinMeisai.Zeiritu = strZeiritu
            recSyouhinMeisai.ZeiKbn = strZeiKbn
        End If

        '��ʎ����ݒ蔻�f
        judgeHansokuAutoSetting(Me.TextSyouhinCd.Value)

        If recSyouhinMeisai Is Nothing Then
            '�e�[�u���T�C�Y�Đݒ�
            TableResizeBeforAlert(Me)
            MLogic.AlertMessage(sender, Messages.MSG001E)
            Exit Sub
        End If
        lngZeiNukiGaku = lngJituGaku * intSuuryou
        lngZei = Long.Parse(Fix(lngZeiNukiGaku * recSyouhinMeisai.Zeiritu))
        lngZeiKomiGaku = lngZeiNukiGaku + lngZei

        Me.TextJituSeikyuuTanka.Value = lngJituGaku
        Me.TextSyouhizeiGaku.Value = lngZei
        Me.TextZeikomiKingaku.Value = lngZeiKomiGaku.ToString
        Me.TextKoumutenSeikyuuTanka.Value = intKoumuGaku
        Me.TextZeinukiKingaku.Value = lngZeiNukiGaku

        '�H���X�Ŕ������z��������
        activeControlKoumuten()

    End Sub

    ''' <summary>
    ''' ����Ŋz�ύX��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgSyouhiZei_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgSyouhiZei.ServerClick
        Dim dtLogic As New DataLogic
        Dim intTanka As Integer = dtLogic.str2Int(Me.TextJituSeikyuuTanka.Value, System.Globalization.NumberStyles.AllowThousands)
        Dim intSuuRyou As Integer = dtLogic.str2Int(Me.TextSuuryou.Value)
        Dim intZeiGaku As Integer = dtLogic.str2Int(Me.TextSyouhizeiGaku.Value, System.Globalization.NumberStyles.AllowThousands)

        '���i�R�[�h���u�����N���͍Čv�Z���Ȃ�
        If Me.TextSyouhinCd.Value = "" Then
            Exit Sub
        End If

        '�t�H�[�J�X�̐ݒ�
        SetFocusAjax(Me.TextSeikyuusyoHakkouBi)

        If intTanka = 0 Or intSuuRyou = 0 Then
            intZeiGaku = 0
        End If

        Me.TextJituSeikyuuTanka.Value = intTanka
        Me.TextSuuryou.Value = intSuuRyou
        Me.TextSyouhizeiGaku.Value = intZeiGaku

        Me.TextZeikomiKingaku.Value = intTanka * intSuuRyou + intZeiGaku


    End Sub

    ''' <summary>
    ''' ����N�����ύX��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgUriDate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgUriDate.ServerClick
        Dim strZeiKbn As String = String.Empty      '�ŋ敪
        Dim strZeiritu As String = String.Empty     '�ŗ�
        Dim strSyouhinCd As String = String.Empty   '���i�R�[�h

        '�`�[����N�����̎����ݒ�
        If Me.TextUriageNengappi.Value <> String.Empty Then
            If Me.TextDenpyouUriDate.Value = String.Empty Then
                Me.TextDenpyouUriDate.Value = Me.TextUriageNengappi.Value
                buttonChgDenUriDate_ServerClick(sender, e)
            End If

            '�ŋ敪�E�ŗ����Ď擾
            strSyouhinCd = Me.TextSyouhinCd.Value
            If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Value.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                '�擾�����ŋ敪�E�ŗ����Z�b�g
                Me.hiddenZeiKbn.Value = strZeiKbn
                Me.hiddenZeiritu.Value = strZeiritu

                '���z�v�Z
                SetKingaku(strZeiritu)
            End If
        Else
            '����N���������͂̏ꍇ

            '���i�R�[�h�����͈ȊO
            If Me.TextSyouhinCd.Value <> String.Empty Then

                '�ŋ敪�E�ŗ����Ď擾
                strSyouhinCd = Me.TextSyouhinCd.Value
                If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Value.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                    '�擾�����ŋ敪�E�ŗ����Z�b�g
                    Me.hiddenZeiKbn.Value = strZeiKbn
                    Me.hiddenZeiritu.Value = strZeiritu

                    '���z�v�Z
                    SetKingaku(strZeiritu)
                End If
            End If
        End If

        '�t�H�[�J�X�̐ݒ�
        SetFocusAjax(Me.TextDenpyouUriDate)
    End Sub

    ''' <summary>
    ''' �`�[����N�����ύX������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgDenUriDate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgDenUriDate.ServerClick
        sendClassDispInfo(clsLogic)

        '���������s���̎����ݒ�
        If Me.TextDenpyouUriDate.Value <> String.Empty Then
            Me.TextSeikyuusyoHakkouBi.Value = clsLogic.GetHansokuhinSeikyuusyoHakkoudate(Me.hiddenMiseCd.Value _
                                                                                        , Me.TextSyouhinCd.Value _
                                                                                        , Me.TextDenpyouUriDate.Value)
        Else
            Me.TextSeikyuusyoHakkouBi.Value = String.Empty
        End If

        '�t�H�[�J�X�̐ݒ�
        SetFocusAjax(Me.buttonGyouSakujoCall)

    End Sub

    ''' <summary>
    ''' ��ʎ����ݒ蔻�f�i�̑��i�j
    ''' </summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <remarks></remarks>
    Private Sub judgeHansokuAutoSetting(ByVal strSyouhinCd As String)
        Dim jLogic As New JibanLogic
        Dim itemRec As Syouhin23Record

        '�����攻�f
        If Me.hiddenIsFc.Value = IsFcMode.FC Then
            'FC�̏ꍇ
            itemRec = Nothing
        Else
            'FC�ȊO�̏ꍇ
            itemRec = jLogic.GetSyouhinInfo(strSyouhinCd, EarthEnum.EnumSyouhinKubun.HansokuNotFc, Me.hiddenMiseCd.Value)
        End If

        '�����^�C�v����
        If itemRec IsNot Nothing AndAlso itemRec.SeikyuuSakiType = EarthConst.SEIKYU_TYOKUSETU Then
            eSaki = enSeikyuuSaki.Tyoku
        Else
            eSaki = enSeikyuuSaki.Hoka
        End If

        '�����^�C�v�̐ݒ�
        SetSeikyuuType(eSaki)

        '�n�񔻒f
        Select Case Me.hiddenKeiretuCd.Value
            Case KEIRETU_TH
                eKretu = enKeiretu.SanK
            Case KEIRETU_01
                eKretu = enKeiretu.SanK
            Case KEIRETU_NF
                eKretu = enKeiretu.SanK
            Case Else
                eKretu = enKeiretu.NotSanK
        End Select
    End Sub

    ''' <summary>
    ''' �H���X�Ŕ������z��������
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub activeControlKoumuten()
        Dim ht As New Hashtable

        If eSaki = enSeikyuuSaki.Hoka And eKretu = enKeiretu.NotSanK Then
            clsCL.chgVeiwMode(TextKoumutenSeikyuuTanka)
        Else
            clsCL.chgDispSyouhinText(TextKoumutenSeikyuuTanka)
        End If
    End Sub
#End Region

#Region "�{�^����������"
    ''' <summary>
    ''' �s�폜�������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonGyouSakujo_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonGyouSakujo.ServerClick
        Me.TrHansokuRecord.Style("display") = "none"
        'SQL��ʔ��f�t���O���폜�ɃZ�b�g
        Me.hiddenSqlTypeFlg.Value = enSqlTypeFlg.DELETE
    End Sub

    ''' <summary>
    ''' �e��ʂ̐V�K�s�ǉ��������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub NewButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strSoukoCd As String, ByVal intIsFc As IsFcMode)
        '�q�ɃR�[�h��ݒ�
        Me.hiddenSoukoCd.Value = strSoukoCd
        Me.hiddenIsFc.Value = intIsFc
        'SQL��ʔ��f�t���O��o�^�ɃZ�b�g
        Me.hiddenSqlTypeFlg.Value = enSqlTypeFlg.INSERT

        sendClassDispInfo(clsLogic)

        '�V�K�s��ǉ��̏ꍇ�A���������s���E����N�����������ݒ�
        If hiddenSqlTypeFlg.Value = enSqlTypeFlg.INSERT And enIsFc = IsFcMode.NOT_FC Then
            Me.TextSeikyuusyoHakkouBi.Value = clsLogic.GetHansokuhinSeikyuusyoHakkoudate(Me.hiddenMiseCd.Value, Me.TextSyouhinCd.Value)
            Me.TextUriageNengappi.Value = DateTime.Today
            Me.TextDenpyouUriDate.Value = DateTime.Today
        End If

        '���i�R�[�h�ύX���̏��������s
        Me.buttonChgSyouhinCd_ServerClick(sender, e)
    End Sub

    ''' <summary>
    ''' ���i�}�X�^�����������̏���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonKensaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonKensaku.ServerClick
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        '���i���ׂ̎擾
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.TextSyouhinCd.Value, Me.hiddenSoukoCd.Value)
        If recSyouhinMeisai Is Nothing Then
            '������ʕ\���pJavaScript�wcallSearch�x�����s
            Dim tmpScript As String = "callSearch('" & Me.TextSyouhinCd.ClientID & EarthConst.SEP_STRING & Me.hiddenSoukoCd.ClientID & "','" & UrlConst.SEARCH_SYOUHIN & "','" & Me.TextSyouhinCd.ClientID & "','" & Me.buttonChgSyouhinCd.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If
    End Sub
#End Region

#Region "�֐�"
    ''' <summary>
    ''' Ajax���쎞�̃t�H�[�J�X�Z�b�g
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub SetFocusAjax(ByVal ctrl As Control)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetFocusAjax", _
                                                    ctrl)
        masterAjaxSM.SetFocus(ctrl)
    End Sub

    ''' <summary>
    ''' ������3����؂�̕�����ɕύX���܂�
    ''' </summary>
    ''' <param name="strNumber">�Ώې��l</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function StrNum2Str(ByVal strNumber As String, Optional ByVal blnThousandsFormat As Boolean = True) As String
        Dim strRet As String
        If strNumber <> "" AndAlso IsNumeric(strNumber) Then
            strRet = Format(CLng(strNumber), EarthConst.FORMAT_KINGAKU_1)
        Else
            strRet = strNumber
        End If
        Return strRet
    End Function

    ''' <summary>
    ''' ���t�^�𕶎���ɕϊ����܂�
    ''' </summary>
    ''' <param name="dtValue">�ϊ����������t</param>
    ''' <param name="blnMillisecond">�~���b�\�����f�t���O</param>
    ''' <returns>�ϊ���̕�����</returns>
    ''' <remarks></remarks>
    Private Function DtTime2Str(ByVal dtValue As Date, Optional ByVal blnMillisecond As Boolean = False) As String
        Dim strRet As String
        If dtValue = DateTime.MinValue Then
            strRet = ""
        Else
            If blnMillisecond = True Then
                strRet = dtValue.ToString(EarthConst.FORMAT_DATE_TIME_2)
            Else
                strRet = dtValue.ToString("yyyy/MM/dd")
            End If
        End If
        Return strRet
    End Function

    ''' <summary>
    ''' ��ʋ��ʏ������W�b�N�N���X�ֈ����n��
    ''' </summary>
    ''' <param name="clsLogic">�X�ʏC�����W�b�N�N���X</param>
    ''' <remarks></remarks>
    Public Sub sendClassDispInfo(ByRef clsLogic As TenbetuSyuuseiLogic)
        '��ʋ��ʏ��̐ݒ�
        With clsLogic
            .MiseCd = Me.hiddenMiseCd.Value
            .IsFC = Me.hiddenIsFc.Value
        End With
    End Sub

    ''' <summary>
    ''' �A���[�g�\�����Ƀe�[�u���T�C�Y���Đݒ肵�܂�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub TableResizeBeforAlert(ByVal sender)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "TableResizeBeforAlert" _
                                            , sender)
        ScriptManager.RegisterStartupScript(sender, _
                                            sender.GetType(), _
                                            "TableReSize", "_d = window.document;changeTableSize(""dataGridContent"",200,100); ", True)
    End Sub

    ''' <summary>
    ''' �����^�C�v�ݒ菈��
    ''' </summary>
    ''' <param name="eSaki">�����^�C�v�i����/���j</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuType(ByVal eSaki As enSeikyuuSaki)
        '���ڐ���/���������f
        If eSaki = enSeikyuuSaki.Tyoku Then
            Me.lblSeikyuuType.Text = EarthConst.SEIKYU_TYOKUSETU_SHORT
        Else
            Me.lblSeikyuuType.Text = EarthConst.SEIKYU_TASETU_SHORT
        End If

        '�c�Ə��̏ꍇ�͏��FC����
        If Me.hiddenIsFc.Value = IsFcMode.FC Then
            Me.lblSeikyuuType.Text = EarthConst.SEIKYU_FCSETU_SHORT
        End If
    End Sub

    ''' <summary>
    ''' ��ʓǂݍ��ݎ��̒l��Hidden���ڂɑޔ�
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub setOpenValues()
        Me.HiddenOpenValues.Value = getCtrlValuesString()
    End Sub

    ''' <summary>
    ''' ��ʃR���g���[���̒l���������A�����񉻂���"
    ''' </summary>
    ''' <returns>��ʃR���g���[���̒l����������������</returns>
    ''' <remarks></remarks>
    Public Function getCtrlValuesString() As String
        Dim sb As New StringBuilder

        sb.Append(Me.TextSyouhinCd.Value & EarthConst.SEP_STRING)
        sb.Append(Me.TextJituSeikyuuTanka.Value.Replace(",", "") & EarthConst.SEP_STRING)
        sb.Append(Me.TextSyouhizeiGaku.Value.Replace(",", "") & EarthConst.SEP_STRING)
        sb.Append(Me.TextZeikomiKingaku.Value.Replace(",", "") & EarthConst.SEP_STRING)
        sb.Append(Me.TextUriageNengappi.Value & EarthConst.SEP_STRING)
        sb.Append(Me.TextDenpyouUriDate.Value & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuusyoHakkouBi.Value & EarthConst.SEP_STRING)

        Return sb.ToString
    End Function

    ''' <summary>
    ''' ���z�ݒ�
    ''' </summary>
    ''' <param name="strZeiritu">�ŗ�</param>
    ''' <remarks></remarks>
    Private Sub SetKingaku(ByVal strZeiritu As String)
        Dim intJituGaku As Integer      '�������Ŕ����z
        Dim intZei As Integer           '�����
        Dim intZeiKomiGaku As Integer   '�ō����z
        Dim intSuuryou As Integer
        Dim lngZeiNukiGaku As Long

        '��ʂɌv�Z�����l���Z�b�g����

        '�������Ŕ����z�A�ŗ����󔒂̏ꍇ�A�ō����z�A����ł��󔒂ɂ���
        If Me.TextJituSeikyuuTanka.Value = String.Empty _
                    OrElse strZeiritu = String.Empty Then
            Me.TextZeikomiKingaku.Value = String.Empty
            Me.TextSyouhizeiGaku.Value = String.Empty
        Else
            '���z�v�Z
            intJituGaku = Me.TextJituSeikyuuTanka.Value             '�������Ŕ����z
            If Me.TextSuuryou.Value <> "" Then
                intSuuryou = CInt(Me.TextSuuryou.Value)
            End If
            lngZeiNukiGaku = intJituGaku * intSuuryou
            intZei = Integer.Parse(Fix(lngZeiNukiGaku * strZeiritu))   '����ł��v�Z(�������Ŕ����z * �擾�����ŗ�)
            intZeiKomiGaku = lngZeiNukiGaku + intZei                   '�ō����z���v�Z(�������Ŕ����z + �����)

            Me.TextJituSeikyuuTanka.Value = intJituGaku             '�������Ŕ����z
            Me.TextSyouhizeiGaku.Value = intZei                     '�����
            Me.TextZeikomiKingaku.Value = intZeiKomiGaku            '�ō����z
        End If

    End Sub
#End Region

End Class