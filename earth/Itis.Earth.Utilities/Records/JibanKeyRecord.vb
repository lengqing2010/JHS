''' <summary>
''' �n�Ոꗗ���Ŏg�p����Key���ڂ�ݒ肷��N���X�ł�<br/>
''' ���������Ƃ��ĕK�v�ȏ��̂ݐݒ肵�ĉ�����
''' </summary>
''' <remarks></remarks>
Public Class JibanKeyRecord

#Region "�敪_1"
    ''' <summary>
    ''' �敪_1 kbn
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn1 As String
    ''' <summary>
    ''' �敪_1 
    ''' </summary>
    ''' <value></value>
    ''' <returns>�敪_1</returns>
    ''' <remarks></remarks>
    Public Property Kbn1() As String
        Get
            Return strKbn1
        End Get
        Set(ByVal value As String)
            strKbn1 = value
        End Set
    End Property
#End Region
#Region "�敪_2"
    ''' <summary>
    ''' �敪_2 kbn
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn2 As String
    ''' <summary>
    ''' �敪_2
    ''' </summary>
    ''' <value></value>
    ''' <returns>�敪_2</returns>
    ''' <remarks></remarks>
    Public Property Kbn2() As String
        Get
            Return strKbn2
        End Get
        Set(ByVal value As String)
            strKbn2 = value
        End Set
    End Property
#End Region
#Region "�敪_3"
    ''' <summary>
    ''' �敪_3  kbn
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn3 As String
    ''' <summary>
    ''' �敪_3
    ''' </summary>
    ''' <value></value>
    ''' <returns>�敪_3</returns>
    ''' <remarks></remarks>
    Public Property Kbn3() As String
        Get
            Return strKbn3
        End Get
        Set(ByVal value As String)
            strKbn3 = value
        End Set
    End Property
#End Region
#Region "�ۏ؏�NO �Ώ۔͈�"
    ''' <summary>
    ''' �ۏ؏�NO �Ώ۔͈� 
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoNoHani As Integer
    ''' <summary>
    ''' �ۏ؏�NO �Ώ۔͈�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏�NO �Ώ۔͈�</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNoHani() As Integer
        Get
            Return intHosyousyoNoHani
        End Get
        Set(ByVal value As Integer)
            intHosyousyoNoHani = value
        End Set
    End Property
#End Region
#Region "�ۏ؏�NO From"
    ''' <summary>
    ''' �ۏ؏�NO From hosyousyo_no
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNoFrom As String
    ''' <summary>
    ''' �ۏ؏�NO From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏�NO From</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNoFrom() As String
        Get
            Return strHosyousyoNoFrom
        End Get
        Set(ByVal value As String)
            strHosyousyoNoFrom = value
        End Set
    End Property
#End Region
#Region "�ۏ؏�NO To"
    ''' <summary>
    ''' �ۏ؏�NO To hosyousyo_no
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNoTo As String
    ''' <summary>
    ''' �ۏ؏�NO To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏�NO To</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNoTo() As String
        Get
            Return strHosyousyoNoTo
        End Get
        Set(ByVal value As String)
            strHosyousyoNoTo = value
        End Set
    End Property
#End Region
#Region "�����X����"
    ''' <summary>
    ''' �����X���� kameiten_cd
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' �����X����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����X����</returns>
    ''' <remarks></remarks>
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region
#Region "�����X�J�i1"
    ''' <summary>
    ''' �����X�J�i1 tenmei_kana1
    ''' </summary>
    ''' <remarks></remarks>
    Private strTenmeiKana1 As String
    ''' <summary>
    ''' �����X�J�i1
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����X�J�i1</returns>
    ''' <remarks></remarks>
    Public Property TenmeiKana1() As String
        Get
            Return strTenmeiKana1
        End Get
        Set(ByVal value As String)
            strTenmeiKana1 = value
        End Set
    End Property
#End Region
#Region "�n����"
    ''' <summary>
    ''' �n���� keiretu_cd 
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuCd As String
    ''' <summary>
    ''' �n����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �n����</returns>
    ''' <remarks></remarks>
    Public Property KeiretuCd() As String
        Get
            Return strKeiretuCd
        End Get
        Set(ByVal value As String)
            strKeiretuCd = value
        End Set
    End Property
#End Region
#Region "�c�Ə�����"
    ''' <summary>
    ''' �c�Ə����� eigyousyo_cd 
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyousyoCd As String
    ''' <summary>
    ''' �c�Ə�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �c�Ə�����</returns>
    ''' <remarks></remarks>
    Public Property EigyousyoCd() As String
        Get
            Return strEigyousyoCd
        End Get
        Set(ByVal value As String)
            strEigyousyoCd = value
        End Set
    End Property
#End Region
#Region "������ЃR�[�h�{������Ў��Ə��R�[�h"
    ''' <summary>
    ''' ������ЃR�[�h�{������Ў��Ə��R�[�h tys_kaisya_cd tys_kaisya_jigyousyo_cd
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' ������ЃR�[�h�{������Ў��Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������ЃR�[�h�{������Ў��Ə��R�[�h</returns>
    ''' <remarks></remarks>
    Public Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region
#Region "�H����ЃR�[�h�{�H����Ў��Ə��R�[�h"
    ''' <summary>
    ''' �H����ЃR�[�h�{�H����Ў��Ə��R�[�h koj_gaisya_cd koj_gaisya_jigyousyo_cd
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojGaisyaCd As String
    ''' <summary>
    ''' �H����ЃR�[�h�{�H����Ў��Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H����ЃR�[�h�{�H����Ў��Ə��R�[�h</returns>
    ''' <remarks></remarks>
    Public Property KojGaisyaCd() As String
        Get
            Return strKojGaisyaCd
        End Get
        Set(ByVal value As String)
            strKojGaisyaCd = value
        End Set
    End Property
#End Region
#Region "�H������N����From"
    ''' <summary>
    ''' �H������N����From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojUriDateFrom As DateTime
    ''' <summary>
    ''' �H������N����From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H������N����From</returns>
    ''' <remarks></remarks>
    Public Property KojUriDateFrom() As DateTime
        Get
            Return dateKojUriDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateKojUriDateFrom = value
        End Set
    End Property
#End Region
#Region "�H������N����To"
    ''' <summary>
    ''' �H������N����To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojUriDateTo As DateTime
    ''' <summary>
    ''' �H������N����To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H������N����To</returns>
    ''' <remarks></remarks>
    Public Property KojUriDateTo() As DateTime
        Get
            Return dateKojUriDateTo
        End Get
        Set(ByVal value As DateTime)
            dateKojUriDateTo = value
        End Set
    End Property
#End Region
#Region "���ǍH�������\���From"
    ''' <summary>
    ''' ���ǍH�������\���From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKairyKojKanryYoteiDateFrom As DateTime
    ''' <summary>
    ''' ���ǍH�������\���From
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���ǍH�������\���From</returns>
    ''' <remarks></remarks>
    Public Property KairyKojKanryYoteiDateFrom() As DateTime
        Get
            Return dateKairyKojKanryYoteiDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojKanryYoteiDateFrom = value
        End Set
    End Property
#End Region
#Region "���ǍH�������\���To"
    ''' <summary>
    ''' ���ǍH�������\���To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKairyKojKanryYoteiDateTo As DateTime
    ''' <summary>
    ''' ���ǍH�������\���To
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���ǍH�������\���To</returns>
    ''' <remarks></remarks>
    Public Property KairyKojKanryYoteiDateTo() As DateTime
        Get
            Return dateKairyKojKanryYoteiDateTo
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojKanryYoteiDateTo = value
        End Set
    End Property
#End Region
#Region "�{�喼"
    ''' <summary>
    ''' �{�喼 sesyu_mei
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String
    ''' <summary>
    ''' �{�喼
    ''' </summary>
    ''' <value></value>
    ''' <returns> �{�喼</returns>
    ''' <remarks></remarks>
    Public Property SesyuMei() As String
        Get
            Return strSesyuMei
        End Get
        Set(ByVal value As String)
            strSesyuMei = value
        End Set
    End Property
#End Region
#Region "�����Z��1+2"
    ''' <summary>
    ''' �����Z��1+2 bukken_jyuusyo12
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo12 As String
    ''' <summary>
    ''' �����Z��1+2
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����Z��1+2</returns>
    ''' <remarks></remarks>
    Public Property BukkenJyuusyo12() As String
        Get
            Return strBukkenJyuusyo12
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo12 = value
        End Set
    End Property
#End Region
#Region "���l"
    ''' <summary>
    ''' ���l bikou
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou As String
    ''' <summary>
    ''' ���l
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���l</returns>
    ''' <remarks></remarks>
    Public Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region
#Region "�˗���From"
    ''' <summary>
    ''' �˗���From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateIraiDateFrom As DateTime
    ''' <summary>
    ''' �˗���From
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���ǍH�������\���From</returns>
    ''' <remarks></remarks>
    Public Property IraiDateFrom() As DateTime
        Get
            Return dateIraiDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateIraiDateFrom = value
        End Set
    End Property
#End Region
#Region "�˗���To"
    ''' <summary>
    ''' �˗���To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateIraiDateTo As DateTime
    ''' <summary>
    ''' �˗���To
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���ǍH�������\���To</returns>
    ''' <remarks></remarks>
    Public Property IraiDateTo() As DateTime
        Get
            Return dateIraiDateTo
        End Get
        Set(ByVal value As DateTime)
            dateIraiDateTo = value
        End Set
    End Property
#End Region
#Region "������]��From"
    ''' <summary>
    ''' ������]��From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTyousaKibouDateFrom As DateTime
    ''' <summary>
    ''' ������]��From
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������]��From</returns>
    ''' <remarks></remarks>
    Public Property TyousaKibouDateFrom() As DateTime
        Get
            Return dateTyousaKibouDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateTyousaKibouDateFrom = value
        End Set
    End Property
#End Region
#Region "������]��To"
    ''' <summary>
    ''' ������]��To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTyousaKibouDateTo As DateTime
    ''' <summary>
    ''' ������]��To
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������]��To</returns>
    ''' <remarks></remarks>
    Public Property TyousaKibouDateTo() As DateTime
        Get
            Return dateTyousaKibouDateTo
        End Get
        Set(ByVal value As DateTime)
            dateTyousaKibouDateTo = value
        End Set
    End Property
#End Region
#Region "�������{��From"
    ''' <summary>
    ''' �������{��From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTyousaJissiDateFrom As DateTime
    ''' <summary>
    ''' �������{��From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������{��From</returns>
    ''' <remarks></remarks>
    Public Property TyousaJissiDateFrom() As DateTime
        Get
            Return dateTyousaJissiDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateTyousaJissiDateFrom = value
        End Set
    End Property
#End Region
#Region "�������{��To"
    ''' <summary>
    ''' �������{��To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTyousaJissiDateTo As DateTime
    ''' <summary>
    ''' �������{��To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������{��To</returns>
    ''' <remarks></remarks>
    Public Property TyousaJissiDateTo() As DateTime
        Get
            Return dateTyousaJissiDateTo
        End Get
        Set(ByVal value As DateTime)
            dateTyousaJissiDateTo = value
        End Set
    End Property
#End Region
#Region "�ۏ؏����s��From"
    ''' <summary>
    ''' �ۏ؏����s��From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakkouDateFrom As DateTime
    ''' <summary>
    ''' �ۏ؏����s��From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏����s��From</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoHakkouDateFrom() As DateTime
        Get
            Return dateHosyousyoHakkouDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakkouDateFrom = value
        End Set
    End Property
#End Region
#Region "�ۏ؏����s��To"
    ''' <summary>
    ''' �ۏ؏����s��To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakkouDateTo As DateTime
    ''' <summary>
    ''' �ۏ؏����s��To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏����s��To</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoHakkouDateTo() As DateTime
        Get
            Return dateHosyousyoHakkouDateTo
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakkouDateTo = value
        End Set
    End Property
#End Region
#Region "������������From"
    ''' <summary>
    ''' ������������From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSyoudakusyoTyousaDateFrom As DateTime
    ''' <summary>
    ''' ������������From
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������������From</returns>
    ''' <remarks></remarks>
    Public Property SyoudakusyoTyousaDateFrom() As DateTime
        Get
            Return dateSyoudakusyoTyousaDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateSyoudakusyoTyousaDateFrom = value
        End Set
    End Property
#End Region
#Region "������������To"
    ''' <summary>
    ''' ������������To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSyoudakusyoTyousaDateTo As DateTime
    ''' <summary>
    ''' ������������To
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������������To</returns>
    ''' <remarks></remarks>
    Public Property SyoudakusyoTyousaDateTo() As DateTime
        Get
            Return dateSyoudakusyoTyousaDateTo
        End Get
        Set(ByVal value As DateTime)
            dateSyoudakusyoTyousaDateTo = value
        End Set
    End Property
#End Region
#Region "�v�揑�쐬��From"
    ''' <summary>
    ''' �v�揑�쐬��From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKeikakusyoSakuseiDateFrom As DateTime
    ''' <summary>
    ''' �v�揑�쐬��From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �v�揑�쐬��From</returns>
    ''' <remarks></remarks>
    Public Property KeikakusyoSakuseiDateFrom() As DateTime
        Get
            Return dateKeikakusyoSakuseiDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateKeikakusyoSakuseiDateFrom = value
        End Set
    End Property
#End Region
#Region "�v�揑�쐬��To"
    ''' <summary>
    ''' �v�揑�쐬��To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKeikakusyoSakuseiDateTo As DateTime
    ''' <summary>
    ''' �v�揑�쐬��To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �v�揑�쐬��To</returns>
    ''' <remarks></remarks>
    Public Property KeikakusyoSakuseiDateTo() As DateTime
        Get
            Return dateKeikakusyoSakuseiDateTo
        End Get
        Set(ByVal value As DateTime)
            dateKeikakusyoSakuseiDateTo = value
        End Set
    End Property
#End Region
#Region "�ۏ؏����s�˗�������From"
    ''' <summary>
    ''' �ۏ؏����s�˗�������From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakkouIraisyoTyakuDateFrom As DateTime
    ''' <summary>
    ''' �ۏ؏����s�˗�������From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏����s�˗�������From</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoHakkouIraisyoTyakuDateFrom() As DateTime
        Get
            Return dateHosyousyoHakkouIraisyoTyakuDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakkouIraisyoTyakuDateFrom = value
        End Set
    End Property
#End Region
#Region "�ۏ؏����s�˗�������To"
    ''' <summary>
    ''' �ۏ؏����s�˗�������To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakkouIraisyoTyakuDateTo As DateTime
    ''' <summary>
    ''' �ۏ؏����s�˗�������To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏����s�˗�������To</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoHakkouIraisyoTyakuDateTo() As DateTime
        Get
            Return dateHosyousyoHakkouIraisyoTyakuDateTo
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakkouIraisyoTyakuDateTo = value
        End Set
    End Property
#End Region
#Region "�f�[�^�j�����"
    ''' <summary>
    ''' �f�[�^�j�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intDataHakiSyubetu As Integer
    ''' <summary>
    ''' �f�[�^�j�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �f�[�^�j�����</returns>
    ''' <remarks></remarks>
    Public Property DataHakiSyubetu() As Integer
        Get
            Return intDataHakiSyubetu
        End Get
        Set(ByVal value As Integer)
            intDataHakiSyubetu = value
        End Set
    End Property
#End Region
#Region "�\���FLG"
    ''' <summary>
    ''' �\���FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intYoyakuZumiFlg As Integer
    ''' <summary>
    ''' �\���FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> �\���FLG</returns>
    ''' <remarks></remarks>
    Public Property YoyakuZumiFlg() As Integer
        Get
            Return intYoyakuZumiFlg
        End Get
        Set(ByVal value As Integer)
            intYoyakuZumiFlg = value
        End Set
    End Property
#End Region

#Region "�����R�[�h"
    ''' <summary>
    ''' �����R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private intBunjouCd As Integer = Integer.MinValue
    ''' <summary>
    ''' �����R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BunjouCd() As Integer
        Get
            Return intBunjouCd
        End Get
        Set(ByVal value As Integer)
            intBunjouCd = value
        End Set
    End Property
#End Region
#Region "��������R�[�h"
    ''' <summary>
    ''' ��������R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenNayoseCd As String
    ''' <summary>
    ''' ��������R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BukkenNayoseCd() As String
        Get
            Return strBukkenNayoseCd
        End Get
        Set(ByVal value As String)
            strBukkenNayoseCd = value
        End Set
    End Property
#End Region

#Region "�_��NO"
    ''' <summary>
    ''' �_��NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiyakuNo As String
    ''' <summary>
    ''' �_��NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �_��NO</returns>
    ''' <remarks></remarks>
    Public Overridable Property KeiyakuNo() As String
        Get
            Return strKeiyakuNo
        End Get
        Set(ByVal value As String)
            strKeiyakuNo = value
        End Set
    End Property
#End Region

#Region "��_�����{�t���O"
    ''' <summary>
    ''' ��_�����{�t���O
    ''' </summary>
    ''' <remarks></remarks>
    Private strTouzaiFlg As String
    ''' <summary>
    ''' ��_�����{�t���O
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��_�����{�t���O</returns>
    ''' <remarks></remarks>
    Public Overridable Property TouzaiFlg() As String
        Get
            Return strTouzaiFlg
        End Get
        Set(ByVal value As String)
            strTouzaiFlg = value
        End Set
    End Property
#End Region


End Class
