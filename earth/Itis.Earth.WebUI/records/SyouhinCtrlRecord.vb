Imports System.Web.UI.HtmlControls
''' <summary>
''' ���i���̃R���g���[���Q�Ɨp���R�[�h�N���X
''' </summary>
''' <remarks></remarks>
Public Class SyouhinCtrlRecord

#Region "���i�s(TR)"
    ''' <summary>
    ''' ���i�s(TR)
    ''' </summary>
    ''' <remarks></remarks>
    Private trSyouhinLine As HtmlTableRow
    ''' <summary>
    ''' ���i�s(TR)
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i�s(TR)</returns>
    ''' <remarks></remarks>
    Public Property SyouhinLine() As HtmlTableRow
        Get
            Return trSyouhinLine
        End Get
        Set(ByVal value As HtmlTableRow)
            trSyouhinLine = value
        End Set
    End Property
#End Region
#Region "���ރR�[�h"
    ''' <summary>
    ''' ���ރR�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private txtBunruiCd As HtmlInputHidden
    ''' <summary>
    ''' ���ރR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns>���ރR�[�h</returns>
    ''' <remarks></remarks>
    Public Property BunruiCd() As HtmlInputHidden
        Get
            Return txtBunruiCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtBunruiCd = value
        End Set
    End Property
#End Region
#Region "���i�R�[�h"
    ''' <summary>
    ''' ���i�R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSyouhinCd As HtmlInputText
    ''' <summary>
    ''' ���i�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i�R�[�h</returns>
    ''' <remarks></remarks>
    Public Property SyouhinCd() As HtmlInputText
        Get
            Return txtSyouhinCd
        End Get
        Set(ByVal value As HtmlInputText)
            txtSyouhinCd = value
        End Set
    End Property
#End Region
#Region "���i�R�[�hOld"
    ''' <summary>
    ''' ���i�R�[�hOld
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSyouhinCdOld As HtmlInputHidden
    ''' <summary>
    ''' ���i�R�[�hOld
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i�R�[�hOld</returns>
    ''' <remarks></remarks>
    Public Property SyouhinCdOld() As HtmlInputHidden
        Get
            Return txtSyouhinCdOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtSyouhinCdOld = value
        End Set
    End Property
#End Region
#Region "���i�����{�^��"
    ''' <summary>
    ''' ���i�����{�^��
    ''' </summary>
    ''' <remarks></remarks>
    Private btnShouhinSearch As HtmlInputButton
    ''' <summary>
    ''' ���i�����{�^��
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i�����{�^��</returns>
    ''' <remarks></remarks>
    Public Property ShouhinSearchBtn() As HtmlInputButton
        Get
            Return btnShouhinSearch
        End Get
        Set(ByVal value As HtmlInputButton)
            btnShouhinSearch = value
        End Set
    End Property
#End Region
#Region "���i��"
    ''' <summary>
    ''' ���i��
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSyouhinNm As HtmlInputText
    ''' <summary>
    ''' ���i��
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i��</returns>
    ''' <remarks></remarks>
    Public Property SyouhinNm() As HtmlInputText
        Get
            Return txtSyouhinNm
        End Get
        Set(ByVal value As HtmlInputText)
            txtSyouhinNm = value
        End Set
    End Property
#End Region
#Region "���i���i�\���p�j"
    ''' <summary>
    ''' ���i���i�\���p�j
    ''' </summary>
    ''' <remarks></remarks>
    Private txtDispSyouhinNm As HtmlGenericControl
    ''' <summary>
    ''' ���i���i�\���p�j
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i���i�\���p�j</returns>
    ''' <remarks></remarks>
    Public Property DispSyouhinNm() As HtmlGenericControl
        Get
            Return txtDispSyouhinNm
        End Get
        Set(ByVal value As HtmlGenericControl)
            txtDispSyouhinNm = value
        End Set
    End Property
#End Region
#Region "�m��敪�i���i�R�̂݁j"
    ''' <summary>
    ''' �m��敪�i���i�R�̂݁j
    ''' </summary>
    ''' <remarks></remarks>
    Private selKakuteiKbn As HtmlSelect
    ''' <summary>
    ''' �m��敪�i���i�R�̂݁j
    ''' </summary>
    ''' <value></value>
    ''' <returns>�m��敪�i���i�R�̂݁j</returns>
    ''' <remarks></remarks>
    Public Property KakuteiKbn() As HtmlSelect
        Get
            Return selKakuteiKbn
        End Get
        Set(ByVal value As HtmlSelect)
            selKakuteiKbn = value
        End Set
    End Property
#End Region
#Region "�m��敪SPAN"
    ''' <summary>
    ''' �m��敪SPAN
    ''' </summary>
    ''' <remarks></remarks>
    Private gcKakuteiKbnSpan As HtmlGenericControl
    ''' <summary>
    ''' �m��敪SPAN
    ''' </summary>
    ''' <value></value>
    ''' <returns>�m��敪SPAN</returns>
    ''' <remarks></remarks>
    Public Property KakuteiKbnSpan() As HtmlGenericControl
        Get
            Return gcKakuteiKbnSpan
        End Get
        Set(ByVal value As HtmlGenericControl)
            gcKakuteiKbnSpan = value
        End Set
    End Property
#End Region
#Region "�m��敪Old�i���i�R�̂݁j"
    ''' <summary>
    ''' �m��敪Old�i���i�R�̂݁j
    ''' </summary>
    ''' <remarks></remarks>
    Private hidKakuteiOld As HtmlInputHidden
    ''' <summary>
    ''' �m��敪Old�i���i�R�̂݁j
    ''' </summary>
    ''' <value></value>
    ''' <returns>�m��敪Old�i���i�R�̂݁j</returns>
    ''' <remarks></remarks>
    Public Property KakuteiKbnOld() As HtmlInputHidden
        Get
            Return hidKakuteiOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            hidKakuteiOld = value
        End Set
    End Property
#End Region
#Region "�H���X�����Ŕ����z"
    ''' <summary>
    ''' �H���X�����Ŕ����z
    ''' </summary>
    ''' <remarks></remarks>
    Private txtKoumutenSeikyuuGaku As HtmlInputText
    ''' <summary>
    ''' �H���X�����Ŕ����z
    ''' </summary>
    ''' <value></value>
    ''' <returns>�H���X�����Ŕ����z</returns>
    ''' <remarks></remarks>
    Public Property KoumutenSeikyuuGaku() As HtmlInputText
        Get
            Return txtKoumutenSeikyuuGaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtKoumutenSeikyuuGaku = value
        End Set
    End Property
#End Region
#Region "�H���X�����Ŕ����zOld"
    ''' <summary>
    ''' �H���X�����Ŕ����zOld
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnKoumutenSeikyuuGakuOld As HtmlInputHidden
    ''' <summary>
    ''' �H���X�����Ŕ����zOld
    ''' </summary>
    ''' <value></value>
    ''' <returns>�H���X�����Ŕ����zOld</returns>
    ''' <remarks></remarks>
    Public Property KoumutenSeikyuuGakuOld() As HtmlInputHidden
        Get
            Return hdnKoumutenSeikyuuGakuOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            hdnKoumutenSeikyuuGakuOld = value
        End Set
    End Property
#End Region
#Region "���������z"
    ''' <summary>
    ''' ���������z
    ''' </summary>
    ''' <remarks></remarks>
    Private txtJituSeikyuuGaku As HtmlInputText
    ''' <summary>
    ''' ���������z
    ''' </summary>
    ''' <value></value>
    ''' <returns>���������z</returns>
    ''' <remarks></remarks>
    Public Property JituSeikyuuGaku() As HtmlInputText
        Get
            Return txtJituSeikyuuGaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtJituSeikyuuGaku = value
        End Set
    End Property
#End Region
#Region "���������zOld"
    ''' <summary>
    ''' ���������zOld
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnJituSeikyuuGakuOld As HtmlInputHidden
    ''' <summary>
    ''' ���������zOld
    ''' </summary>
    ''' <value></value>
    ''' <returns>���������zOld</returns>
    ''' <remarks></remarks>
    Public Property JituSeikyuuGakuOld() As HtmlInputHidden
        Get
            Return hdnJituSeikyuuGakuOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            hdnJituSeikyuuGakuOld = value
        End Set
    End Property
#End Region
#Region "����Ŋz"
    ''' <summary>
    ''' ����Ŋz
    ''' </summary>
    ''' <remarks></remarks>
    Private txtZeiGaku As HtmlInputText
    ''' <summary>
    ''' ����Ŋz
    ''' </summary>
    ''' <value></value>
    ''' <returns>����Ŋz</returns>
    ''' <remarks></remarks>
    Public Property ZeiGaku() As HtmlInputText
        Get
            Return txtZeiGaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtZeiGaku = value
        End Set
    End Property
#End Region
#Region "�ŋ敪"
    ''' <summary>
    ''' �ŋ敪
    ''' </summary>
    ''' <remarks></remarks>
    Private txtZeikubun As HtmlInputHidden
    ''' <summary>
    ''' �ŋ敪
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ŋ敪</returns>
    ''' <remarks></remarks>
    Public Property ZeiKubun() As HtmlInputHidden
        Get
            Return txtZeikubun
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtZeikubun = value
        End Set
    End Property
#End Region
#Region "�ŗ�"
    ''' <summary>
    ''' ����Ŋz
    ''' </summary>
    ''' <remarks></remarks>
    Private txtZeiRitu As HtmlInputHidden
    ''' <summary>
    ''' ����Ŋz
    ''' </summary>
    ''' <value></value>
    ''' <returns>����Ŋz</returns>
    ''' <remarks></remarks>
    Public Property ZeiRitu() As HtmlInputHidden
        Get
            Return txtZeiRitu
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtZeiRitu = value
        End Set
    End Property
#End Region
#Region "�ō����z"
    ''' <summary>
    ''' �ō����z
    ''' </summary>
    ''' <remarks></remarks>
    Private txtZeiKomiGaku As HtmlInputText
    ''' <summary>
    ''' �ō����z
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ō����z</returns>
    ''' <remarks></remarks>
    Public Property ZeiKomiGaku() As HtmlInputText
        Get
            Return txtZeiKomiGaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtZeiKomiGaku = value
        End Set
    End Property
#End Region
#Region "���������z"
    ''' <summary>
    ''' ���������z
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSyoudakusyoKingaku As HtmlInputText
    ''' <summary>
    ''' ���������z
    ''' </summary>
    ''' <value></value>
    ''' <returns>���������z</returns>
    ''' <remarks></remarks>
    Public Property SyoudakusyoKingaku() As HtmlInputText
        Get
            Return txtSyoudakusyoKingaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtSyoudakusyoKingaku = value
        End Set
    End Property
#End Region
#Region "���������zOld"
    ''' <summary>
    ''' ���������zOld
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnSyoudakusyoKingakuOld As HtmlInputHidden
    ''' <summary>
    ''' ���������zOld
    ''' </summary>
    ''' <value></value>
    ''' <returns>���������zOld</returns>
    ''' <remarks></remarks>
    Public Property SyoudakusyoKingakuOld() As HtmlInputHidden
        Get
            Return hdnSyoudakusyoKingakuOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            hdnSyoudakusyoKingakuOld = value
        End Set
    End Property
#End Region
#Region "�d�������"
    ''' <summary>
    ''' �d�������
    ''' </summary>
    ''' <remarks></remarks>
    Private hidSiireSyouhizei As HtmlInputHidden
    ''' <summary>
    ''' �d�������
    ''' </summary>
    ''' <value></value>
    ''' <returns>�d�������</returns>
    ''' <remarks></remarks>
    Public Property SiireSyouhizei() As HtmlInputHidden
        Get
            Return hidSiireSyouhizei
        End Get
        Set(ByVal value As HtmlInputHidden)
            hidSiireSyouhizei = value
        End Set
    End Property
#End Region
#Region "���������s��"
    ''' <summary>
    ''' ���������s��
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSeikyuusyoHakkouDate As HtmlInputText
    ''' <summary>
    ''' ���������s��
    ''' </summary>
    ''' <value></value>
    ''' <returns>���������s��</returns>
    ''' <remarks></remarks>
    Public Property SeikyuusyoHakkouDate() As HtmlInputText
        Get
            Return txtSeikyuusyoHakkouDate
        End Get
        Set(ByVal value As HtmlInputText)
            txtSeikyuusyoHakkouDate = value
        End Set
    End Property
#End Region
#Region "����N����"
    ''' <summary>
    ''' ����N����
    ''' </summary>
    ''' <remarks></remarks>
    Private txtUriageDate As HtmlInputText
    ''' <summary>
    ''' ����N����
    ''' </summary>
    ''' <value></value>
    ''' <returns>����N����</returns>
    ''' <remarks></remarks>
    Public Property UriageDate() As HtmlInputText
        Get
            Return txtUriageDate
        End Get
        Set(ByVal value As HtmlInputText)
            txtUriageDate = value
        End Set
    End Property
#End Region
#Region "�����L��"
    ''' <summary>
    ''' �����L��
    ''' </summary>
    ''' <remarks></remarks>
    Private selSeikyuuUmu As HtmlSelect
    ''' <summary>
    ''' �����L��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����L��</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuUmu() As HtmlSelect
        Get
            Return selSeikyuuUmu
        End Get
        Set(ByVal value As HtmlSelect)
            selSeikyuuUmu = value
        End Set
    End Property
#End Region
#Region "�����L��SAPN"
    ''' <summary>
    ''' �����L��SPAN
    ''' </summary>
    ''' <remarks></remarks>
    Private gcSeikyuuUmuSpan As HtmlGenericControl
    ''' <summary>
    ''' �����L��SPAN
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����L��SPAN</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuUmuSpan() As HtmlGenericControl
        Get
            Return gcSeikyuuUmuSpan
        End Get
        Set(ByVal value As HtmlGenericControl)
            gcSeikyuuUmuSpan = value
        End Set
    End Property
#End Region
#Region "���ύ쐬��"
    ''' <summary>
    ''' ���ύ쐬��
    ''' </summary>
    ''' <remarks></remarks>
    Private txtMitumoriDate As HtmlInputText
    ''' <summary>
    ''' ���ύ쐬��
    ''' </summary>
    ''' <value></value>
    ''' <returns>���ύ쐬��</returns>
    ''' <remarks></remarks>
    Public Property MitumoriDate() As HtmlInputText
        Get
            Return txtMitumoriDate
        End Get
        Set(ByVal value As HtmlInputText)
            txtMitumoriDate = value
        End Set
    End Property
#End Region
#Region "�������m��FLG"
    ''' <summary>
    ''' �������m��FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private selHattyuusyoKakutei As HtmlSelect
    ''' <summary>
    ''' �������m��FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>�������m��FLG</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakutei() As HtmlSelect
        Get
            Return selHattyuusyoKakutei
        End Get
        Set(ByVal value As HtmlSelect)
            selHattyuusyoKakutei = value
        End Set
    End Property
#End Region
#Region "�������m��SPAN"
    ''' <summary>
    ''' �������m��SPAN
    ''' </summary>
    ''' <remarks></remarks>
    Private gcHattyuusyoKakuteiSpan As HtmlGenericControl
    ''' <summary>
    ''' �������m��SPAN
    ''' </summary>
    ''' <value></value>
    ''' <returns>�������m��SPAN</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakuteiSpan() As HtmlGenericControl
        Get
            Return gcHattyuusyoKakuteiSpan
        End Get
        Set(ByVal value As HtmlGenericControl)
            gcHattyuusyoKakuteiSpan = value
        End Set
    End Property
#End Region
#Region "�������m��FLGOld"
    ''' <summary>
    ''' �������m��FLGOld
    ''' </summary>
    ''' <remarks></remarks>
    Private selHattyuusyoKakuteiOld As HtmlInputHidden
    ''' <summary>
    ''' �������m��FLGOld
    ''' </summary>
    ''' <value></value>
    ''' <returns>�������m��FLGOld</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakuteiOld() As HtmlInputHidden
        Get
            Return selHattyuusyoKakuteiOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            selHattyuusyoKakuteiOld = value
        End Set
    End Property
#End Region
#Region "���������z"
    ''' <summary>
    ''' ���������z
    ''' </summary>
    ''' <remarks></remarks>
    Private txtHattyuusyoKingaku As HtmlInputText
    ''' <summary>
    ''' ���������z
    ''' </summary>
    ''' <value></value>
    ''' <returns>���������z</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKingaku() As HtmlInputText
        Get
            Return txtHattyuusyoKingaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtHattyuusyoKingaku = value
        End Set
    End Property
#End Region
#Region "���������zOld"
    ''' <summary>
    ''' ���������zOld
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnHattyuusyoKingakuOld As HtmlInputHidden
    ''' <summary>
    ''' ���������zOld
    ''' </summary>
    ''' <value></value>
    ''' <returns>���������zOld</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKingakuOld() As HtmlInputHidden
        Get
            Return hdnHattyuusyoKingakuOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            hdnHattyuusyoKingakuOld = value
        End Set
    End Property
#End Region
#Region "���������zOld(�������m��)"
    ''' <summary>
    ''' ���������zOld
    ''' </summary>
    ''' <remarks></remarks>
    Private txtHattyuusyoKingakuOld As HtmlInputHidden
    ''' <summary>
    ''' ���������zOld
    ''' </summary>
    ''' <value></value>
    ''' <returns>���������zOld</returns>
    ''' <remarks></remarks>
    Public Property HidHattyuusyoKingakuOld() As HtmlInputHidden
        Get
            Return txtHattyuusyoKingakuOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtHattyuusyoKingakuOld = value
        End Set
    End Property
#End Region
#Region "�������m�F��"
    ''' <summary>
    ''' �������m�F��
    ''' </summary>
    ''' <remarks></remarks>
    Private txtHattyuusyoKakuninbi As HtmlInputText
    ''' <summary>
    ''' �������m�F��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�������m�F��</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakuninbi() As HtmlInputText
        Get
            Return txtHattyuusyoKakuninbi
        End Get
        Set(ByVal value As HtmlInputText)
            txtHattyuusyoKakuninbi = value
        End Set
    End Property
#End Region
#Region "���z�ύX�t���O"
    ''' <summary>
    ''' ���z�ύX�t���O
    ''' </summary>
    ''' <remarks></remarks>
    Private txtKingakuFlg As HtmlInputHidden
    ''' <summary>
    ''' ���z�ύX�t���O
    ''' </summary>
    ''' <value></value>
    ''' <returns>���z�ύX�t���O</returns>
    ''' <remarks></remarks>
    Public Property KingakuFlg() As HtmlInputHidden
        Get
            Return txtKingakuFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtKingakuFlg = value
        End Set
    End Property
#End Region
#Region "����󋵁i���i�R�̂݁j"
    ''' <summary>
    ''' ����� uriageJyoukyou
    ''' </summary>
    ''' <remarks></remarks>
    Private txtUriageJyoukyou As HtmlInputHidden
    ''' <summary>
    ''' ����󋵁i���i�R�̂݁j
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����</returns>
    ''' <remarks></remarks>
    Public Property UriageJyoukyou() As HtmlInputHidden
        Get
            Return txtUriageJyoukyou
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtUriageJyoukyou = value
        End Set
    End Property
#End Region
#Region "����v��t���O"
    ''' <summary>
    ''' ����v��t���O uriageKeijyouFlg
    ''' </summary>
    ''' <remarks></remarks>
    Private txtUriageKeijyouFlg As HtmlInputHidden
    ''' <summary>
    ''' ����v��t���O
    ''' </summary>
    ''' <value></value>
    ''' <returns>����v��t���O</returns>
    ''' <remarks></remarks>
    Public Property UriageKeijyouFlg() As HtmlInputHidden
        Get
            Return txtUriageKeijyouFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtUriageKeijyouFlg = value
        End Set
    End Property
#End Region
#Region "����v���"
    ''' <summary>
    ''' ����v��� uriageKeijyouBi
    ''' </summary>
    ''' <remarks></remarks>
    Private txtUriageKeijyouBi As HtmlInputHidden
    ''' <summary>
    ''' ����v���
    ''' </summary>
    ''' <value></value>
    ''' <returns>����v���</returns>
    ''' <remarks></remarks>
    Public Property UriageKeijyouBi() As HtmlInputHidden
        Get
            Return txtUriageKeijyouBi
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtUriageKeijyouBi = value
        End Set
    End Property
#End Region
#Region "���l"
    ''' <summary>
    ''' ���l bikou
    ''' </summary>
    ''' <remarks></remarks>
    Private txtBikou As HtmlInputHidden
    ''' <summary>
    ''' ���l
    ''' </summary>
    ''' <value></value>
    ''' <returns>���l</returns>
    ''' <remarks></remarks>
    Public Property Bikou() As HtmlInputHidden
        Get
            Return txtBikou
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtBikou = value
        End Set
    End Property
#End Region
#Region "�ꊇ����FLG"
    ''' <summary>
    ''' �ꊇ����FLG ikkatuNyuukinFlg
    ''' </summary>
    ''' <remarks></remarks>
    Private txtIkkatuNyuukinFlg As HtmlInputHidden
    ''' <summary>
    ''' �ꊇ����FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ꊇ����FLG</returns>
    ''' <remarks></remarks>
    Public Property IkkatuNyuukinFlg() As HtmlInputHidden
        Get
            Return txtIkkatuNyuukinFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtIkkatuNyuukinFlg = value
        End Set
    End Property
#End Region

#Region "������d���惊���N"
    ''' <summary>
    ''' ������d���惊���N
    ''' </summary>
    ''' <remarks></remarks>
    Private SeikyuuSiireLinkCtrl As SeikyuuSiireLinkCtrl
    ''' <summary>
    ''' ������d���惊���N
    ''' </summary>
    ''' <value></value>
    ''' <returns>������d���惊���N</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSiireLink() As SeikyuuSiireLinkCtrl
        Get
            Return SeikyuuSiireLinkCtrl
        End Get
        Set(ByVal value As SeikyuuSiireLinkCtrl)
            SeikyuuSiireLinkCtrl = value
        End Set
    End Property
#End Region
#Region "���ʑΉ��c�[���`�b�v"
    ''' <summary>
    ''' ���ʑΉ��c�[���`�b�v
    ''' </summary>
    ''' <remarks></remarks>
    Private TokubetuTaiouToolTipCtrl As TokubetuTaiouToolTipCtrl
    ''' <summary>
    ''' ���ʑΉ��c�[���`�b�v
    ''' </summary>
    ''' <value></value>
    ''' <returns>���ʑΉ��c�[���`�b�v</returns>
    ''' <remarks></remarks>
    Public Property TokubetuTaiouToolTip() As TokubetuTaiouToolTipCtrl
        Get
            Return TokubetuTaiouToolTipCtrl
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            TokubetuTaiouToolTipCtrl = value
        End Set
    End Property
#End Region
#Region "�X�V����"
    ''' <summary>
    ''' �X�V���� UpdDatetime
    ''' </summary>
    ''' <remarks></remarks>
    Private txtUpdDatetime As HtmlInputHidden
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Property UpdDatetime() As HtmlInputHidden
        Get
            Return txtUpdDatetime
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtUpdDatetime = value
        End Set
    End Property
#End Region
#Region "���i������R�[�h"
    ''' <summary>
    ''' ���i������R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private hidSyouhinSeikyuuSakiCd As HtmlInputHidden
    ''' <summary>
    ''' ���i������R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i������R�[�h</returns>
    ''' <remarks></remarks>
    Public Property SyouhinSeikyuuSakiCd() As HtmlInputHidden
        Get
            Return hidSyouhinSeikyuuSakiCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            hidSyouhinSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "���������z�ύX��FLG"
    ''' <summary>
    ''' ���������z�ύX��FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private hidSyoudakuHenkouKahi As HtmlInputHidden
    ''' <summary>
    ''' ���������z�ύX��FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>���������z�ύX��FLG</returns>
    ''' <remarks></remarks>
    Public Property SyoudakuHenkouKahi() As HtmlInputHidden
        Get
            Return hidSyoudakuHenkouKahi
        End Get
        Set(ByVal value As HtmlInputHidden)
            hidSyoudakuHenkouKahi = value
        End Set
    End Property
#End Region

#Region "�H���X�����Ŕ����z�ύX��FLG"
    ''' <summary>
    ''' �H���X�����Ŕ����z�ύX��FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private hidKoumutenHenkouKahi As HtmlInputHidden
    ''' <summary>
    ''' �H���X�����Ŕ����z�ύX��FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>�H���X�����Ŕ����z�ύX��FLG</returns>
    ''' <remarks></remarks>
    Public Property KoumutenHenkouKahi() As HtmlInputHidden
        Get
            Return hidKoumutenHenkouKahi
        End Get
        Set(ByVal value As HtmlInputHidden)
            hidKoumutenHenkouKahi = value
        End Set
    End Property
#End Region

#Region "�������Ŕ����z�ύX��FLG"
    ''' <summary>
    ''' �������Ŕ����z�ύX��FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private hidJituGakuHenkouKahi As HtmlInputHidden
    ''' <summary>
    ''' �������Ŕ����z�ύX��FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>�������Ŕ����z�ύX��FLG</returns>
    ''' <remarks></remarks>
    Public Property JituGakuHenkouKahi() As HtmlInputHidden
        Get
            Return hidJituGakuHenkouKahi
        End Get
        Set(ByVal value As HtmlInputHidden)
            hidJituGakuHenkouKahi = value
        End Set
    End Property
#End Region

End Class
