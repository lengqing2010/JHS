''' <summary>
''' �i���ۏ؏��󋵂Ŏg�p����Key���ڂ�ݒ肷��N���X�ł�<br/>
''' ���������Ƃ��ĕK�v�ȏ��̂ݐݒ肵�ĉ�����
''' </summary>
''' <remarks></remarks>
Public Class HinsituHosyousyoJyoukyouRecord

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
    ''' <returns> �˗���From</returns>
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
    ''' <returns> �˗���To</returns>
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

#Region "���s�i����1"
    ''' <summary>
    ''' ���s�i����1
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakkouStatus1 As Integer = Integer.MinValue
    ''' <summary>
    ''' ���s�i����1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakkouStatus1() As Integer
        Get
            Return intHakkouStatus1
        End Get
        Set(ByVal value As Integer)
            intHakkouStatus1 = value
        End Set
    End Property
#End Region
#Region "���s�i����2"
    ''' <summary>
    ''' ���s�i����2
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakkouStatus2 As Integer = Integer.MinValue
    ''' <summary>
    ''' ���s�i����2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakkouStatus2() As Integer
        Get
            Return intHakkouStatus2
        End Get
        Set(ByVal value As Integer)
            intHakkouStatus2 = value
        End Set
    End Property
#End Region
#Region "���s�i����3"
    ''' <summary>
    ''' ���s�i����3
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakkouStatus3 As Integer = Integer.MinValue
    ''' <summary>
    ''' ���s�i����3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakkouStatus3() As Integer
        Get
            Return intHakkouStatus3
        End Get
        Set(ByVal value As Integer)
            intHakkouStatus3 = value
        End Set
    End Property
#End Region
#Region "���s�i����4"
    ''' <summary>
    ''' ���s�i����4
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakkouStatus4 As Integer = Integer.MinValue
    ''' <summary>
    ''' ���s�i����4
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakkouStatus4() As Integer
        Get
            Return intHakkouStatus4
        End Get
        Set(ByVal value As Integer)
            intHakkouStatus4 = value
        End Set
    End Property
#End Region
#Region "���s�i����5"
    ''' <summary>
    ''' ���s�i����5
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakkouStatus5 As Integer = Integer.MinValue
    ''' <summary>
    ''' ���s�i����5
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakkouStatus5() As Integer
        Get
            Return intHakkouStatus5
        End Get
        Set(ByVal value As Integer)
            intHakkouStatus5 = value
        End Set
    End Property
#End Region
#Region "���s�i����6"
    ''' <summary>
    ''' ���s�i����6
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakkouStatus6 As Integer = Integer.MinValue
    ''' <summary>
    ''' ���s�i����6
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakkouStatus6() As Integer
        Get
            Return intHakkouStatus6
        End Get
        Set(ByVal value As Integer)
            intHakkouStatus6 = value
        End Set
    End Property
#End Region

#Region "���s�^�C�~���O"
    ''' <summary>
    ''' ���s�^�C�~���O
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakkouTiming As Integer = Integer.MinValue
    ''' <summary>
    ''' ���s�^�C�~���O
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakkouTiming() As Integer
        Get
            Return intHakkouTiming
        End Get
        Set(ByVal value As Integer)
            intHakkouTiming = value
        End Set
    End Property
#End Region

#Region "�ۏ؏����s�˗������� ��`�F�b�N�{�b�N�X"
    ''' <summary>
    ''' �ۏ؏����s�˗������� ��`�F�b�N�{�b�N�X
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoHakkouIraisyoTyakuDateChk As Integer = Integer.MinValue
    ''' <summary>
    ''' �ۏ؏����s�˗������� ��`�F�b�N�{�b�N�X
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HosyousyoHakkouIraisyoTyakuDateChk() As Integer
        Get
            Return intHosyousyoHakkouIraisyoTyakuDateChk
        End Get
        Set(ByVal value As Integer)
            intHosyousyoHakkouIraisyoTyakuDateChk = value
        End Set
    End Property
#End Region

#Region "�ۏ؏����s�� ��`�F�b�N�{�b�N�X"
    ''' <summary>
    ''' �ۏ؏����s�� ��`�F�b�N�{�b�N�X
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoHakkouDateChk As Integer = Integer.MinValue
    ''' <summary>
    ''' �ۏ؏����s�� ��`�F�b�N�{�b�N�X
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HosyousyoHakkouDateChk() As Integer
        Get
            Return intHosyousyoHakkouDateChk
        End Get
        Set(ByVal value As Integer)
            intHosyousyoHakkouDateChk = value
        End Set
    End Property
#End Region

#Region "�ۏ؏��Ĕ��s��From"
    ''' <summary>
    ''' �ۏ؏��Ĕ��s��From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoSaihakDateFrom As DateTime
    ''' <summary>
    ''' �ۏ؏��Ĕ��s��From
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏��Ĕ��s��From</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoSaihakDateFrom() As DateTime
        Get
            Return dateHosyousyoSaihakDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoSaihakDateFrom = value
        End Set
    End Property
#End Region
#Region "�ۏ؏��Ĕ��s��To"
    ''' <summary>
    ''' �ۏ؏��Ĕ��s��To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoSaihakDateTo As DateTime
    ''' <summary>
    ''' �ۏ؏��Ĕ��s��To
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏��Ĕ��s��To</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoSaihakDateTo() As DateTime
        Get
            Return dateHosyousyoSaihakDateTo
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoSaihakDateTo = value
        End Set
    End Property
#End Region

#Region "���s�˗����� ��`�F�b�N�{�b�N�X"
    ''' <summary>
    ''' ���s�˗����� ��`�F�b�N�{�b�N�X
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakIraiTimeChk As Integer = Integer.MinValue
    ''' <summary>
    ''' ���s�˗����� ��`�F�b�N�{�b�N�X
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HakIraiTimeChk() As Integer
        Get
            Return intHakIraiTimeChk
        End Get
        Set(ByVal value As Integer)
            intHakIraiTimeChk = value
        End Set
    End Property
#End Region
#Region "���s�˗�����From"
    ''' <summary>
    ''' ���s�˗�����From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHakIraiTimeFrom As DateTime
    ''' <summary>
    ''' ���s�˗�����From
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���s�˗�����From</returns>
    ''' <remarks></remarks>
    Public Property HakIraiTimeFrom() As DateTime
        Get
            Return dateHakIraiTimeFrom
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiTimeFrom = value
        End Set
    End Property
#End Region
#Region "���s�˗�����To"
    ''' <summary>
    ''' ���s�˗�����To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHakIraiTimeTo As DateTime
    ''' <summary>
    ''' ���s�˗�����To
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���s�˗�����To</returns>
    ''' <remarks></remarks>
    Public Property HakIraiTimeTo() As DateTime
        Get
            Return dateHakIraiTimeTo
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiTimeTo = value
        End Set
    End Property
#End Region

#Region "�ۏ؊���(�����X)"
    ''' <summary>
    ''' �ۏ؊���(�����X)
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouKikanMK As Integer = Integer.MinValue
    ''' <summary>
    ''' �ۏ؊���(�����X)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؊���(�����X)</returns>
    ''' <remarks></remarks>
    Public Property HosyouKikanMK() As Integer
        Get
            Return intHosyouKikanMK
        End Get
        Set(ByVal value As Integer)
            intHosyouKikanMK = value
        End Set
    End Property
#End Region
#Region "�ۏ؊���(����)"
    ''' <summary>
    ''' �ۏ؊���(����)
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouKikanTJ As Integer = Integer.MinValue
    ''' <summary>
    ''' �ۏ؊���(����)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؊���(����)</returns>
    ''' <remarks></remarks>
    Public Property HosyouKikanTJ() As Integer
        Get
            Return intHosyouKikanTJ
        End Get
        Set(ByVal value As Integer)
            intHosyouKikanTJ = value
        End Set
    End Property
#End Region

#Region "���s�˗����n��"
    ''' <summary>
    ''' ���s�˗����n��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHakIraiHwDate As DateTime
    ''' <summary>
    ''' ���s�˗����n��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���s�˗����n��</returns>
    ''' <remarks></remarks>
    Public Property HakIraiHwDate() As DateTime
        Get
            Return dateHakIraiHwDate
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiHwDate = value
        End Set
    End Property
#End Region

End Class
