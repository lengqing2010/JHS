''' <summary>
''' �i���ۏ؏��󋵌����̃��R�[�h�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class HinsituHosyousyoJyoukyouSearchRecord

#Region "�敪"
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> �敪</returns>
    ''' <remarks></remarks>
    <TableMap("kbn")> _
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "�ۏ؏�NO"
    ''' <summary>
    ''' �ۏ؏�NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String
    ''' <summary>
    ''' �ۏ؏�NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏�NO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no")> _
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "�ް��j�����"
    ''' <summary>
    ''' �ް��j�����
    ''' </summary>
    ''' <remarks></remarks>
    Private strDataHakiSyubetu As String
    ''' <summary>
    ''' �ް��j�����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("data_haki_syubetu")> _
    Public Property DataHakiSyubetu() As String
        Get
            Return strDataHakiSyubetu
        End Get
        Set(ByVal value As String)
            strDataHakiSyubetu = value
        End Set
    End Property
#End Region

#Region "�{�喼"
    ''' <summary>
    ''' �{�喼
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String
    ''' <summary>
    ''' �{�喼
    ''' </summary>
    ''' <value></value>
    ''' <returns> �{�喼</returns>
    ''' <remarks></remarks>
    <TableMap("sesyu_mei")> _
    Public Property SesyuMei() As String
        Get
            Return strSesyuMei
        End Get
        Set(ByVal value As String)
            strSesyuMei = value
        End Set
    End Property
#End Region

#Region "�����Z��1"
    ''' <summary>
    ''' �����Z��1
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo1 As String
    ''' <summary>
    ''' �����Z��1
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����Z��1</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo1")> _
    Public Property BukkenJyuusyo1() As String
        Get
            Return strBukkenJyuusyo1
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo1 = value
        End Set
    End Property
#End Region

#Region "�����Z��2"
    ''' <summary>
    ''' �����Z��2
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo2 As String
    ''' <summary>
    ''' �����Z��2
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����Z��2</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo2")> _
    Public Property BukkenJyuusyo2() As String
        Get
            Return strBukkenJyuusyo2
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo2 = value
        End Set
    End Property
#End Region

#Region "�����Z��3"
    ''' <summary>
    ''' �����Z��3
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo3 As String
    ''' <summary>
    ''' �����Z��3
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����Z��3</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo3")> _
    Public Property BukkenJyuusyo3() As String
        Get
            Return strBukkenJyuusyo3
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo3 = value
        End Set
    End Property
#End Region

#Region "�����X�R�[�h"
    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����X�R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd")> _
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "�����X���"
    ''' <summary>
    ''' �����X���
    ''' </summary>
    ''' <remarks></remarks>
    Private intKtTorikesi As Integer
    ''' <summary>
    ''' �����X���
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����X���</returns>
    ''' <remarks></remarks>
    <TableMap("kt_torikesi")> _
    Public Property KtTorikesi() As Integer
        Get
            Return intKtTorikesi
        End Get
        Set(ByVal value As Integer)
            intKtTorikesi = value
        End Set
    End Property
#End Region

#Region "�����X������R"
    ''' <summary>
    ''' �����X������R
    ''' </summary>
    ''' <remarks></remarks>
    Private strKtTorikesiRiyuu As String
    ''' <summary>
    ''' �����X������R
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����X������R</returns>
    ''' <remarks></remarks>
    <TableMap("kt_torikesi_riyuu")> _
    Public Property KtTorikesiRiyuu() As String
        Get
            Return strKtTorikesiRiyuu
        End Get
        Set(ByVal value As String)
            strKtTorikesiRiyuu = value
        End Set
    End Property
#End Region

#Region "�����X��1"
    ''' <summary>
    ''' �����X��1
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMei1 As String
    ''' <summary>
    ''' �����X��1
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����X��1</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_mei1")> _
    Public Property KameitenMei1() As String
        Get
            Return strKameitenMei1
        End Get
        Set(ByVal value As String)
            strKameitenMei1 = value
        End Set
    End Property
#End Region

#Region "�X���J�i1"
    ''' <summary>
    ''' �X���J�i1
    ''' </summary>
    ''' <remarks></remarks>
    Private strTenmeiKana1 As String
    ''' <summary>
    ''' �X���J�i1
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X���J�i1</returns>
    ''' <remarks></remarks>
    <TableMap("tenmei_kana1")> _
    Public Property TenmeiKana1() As String
        Get
            Return strTenmeiKana1
        End Get
        Set(ByVal value As String)
            strTenmeiKana1 = value
        End Set
    End Property
#End Region

#Region "���s�˗�����"
    ''' <summary>
    ''' ���s�˗�����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHakIraiTime As DateTime
    ''' <summary>
    ''' ���s�˗�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���s�˗�����</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_time")> _
    Public Property HakIraiTime() As DateTime
        Get
            Return dateHakIraiTime
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiTime = value
        End Set
    End Property
#End Region

#Region "�ۏ؏����s�˗�������"
    ''' <summary>
    ''' �ۏ؏����s�˗�������
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakIraisyoTykDate As DateTime
    ''' <summary>
    ''' �ۏ؏����s�˗�������
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏����s�˗�������</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_iraisyo_tyk_date")> _
    Public Property HosyousyoHakIraisyoTykDate() As DateTime
        Get
            Return dateHosyousyoHakIraisyoTykDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakIraisyoTykDate = value
        End Set
    End Property
#End Region

#Region "�ۏ؏����s��"
    ''' <summary>
    ''' �ۏ؏����s��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakDate As DateTime
    ''' <summary>
    ''' �ۏ؏����s��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏����s��</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_date")> _
    Public Property HosyousyoHakDate() As DateTime
        Get
            Return dateHosyousyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakDate = value
        End Set
    End Property
#End Region

#Region "�ۏ؏��J�n��"
    ''' <summary>
    ''' �ۏ؏��J�n��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyouKaisiDate As DateTime
    ''' <summary>
    ''' �ۏ؏��J�n��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏��J�n��</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kaisi_date")> _
    Public Property HosyouKaisiDate() As DateTime
        Get
            Return dateHosyouKaisiDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyouKaisiDate = value
        End Set
    End Property
#End Region

#Region "��b�d�l1"
    ''' <summary>
    ''' ��b�d�l1
    ''' </summary>
    ''' <remarks></remarks>
    Private strKsSiyou1 As String
    ''' <summary>
    ''' ��b�d�l1
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��b�d�l1</returns>
    ''' <remarks></remarks>
    <TableMap("ks_siyou_1")> _
    Public Property KsSiyou1() As String
        Get
            Return strKsSiyou1
        End Get
        Set(ByVal value As String)
            strKsSiyou1 = value
        End Set
    End Property
#End Region

#Region "��b�d�l2"
    ''' <summary>
    ''' ��b�d�l2
    ''' </summary>
    ''' <remarks></remarks>
    Private strKsSiyou2 As String
    ''' <summary>
    ''' ��b�d�l2
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��b�d�l2</returns>
    ''' <remarks></remarks>
    <TableMap("ks_siyou_2")> _
    Public Property KsSiyou2() As String
        Get
            Return strKsSiyou2
        End Get
        Set(ByVal value As String)
            strKsSiyou2 = value
        End Set
    End Property
#End Region

#Region "��b�d�l�ڑ���"
    ''' <summary>
    ''' ��b�d�l�ڑ���
    ''' </summary>
    ''' <remarks></remarks>
    Private strKsSiyouSetuzokusi As String
    ''' <summary>
    ''' ��b�d�l�ڑ���
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("ks_siyou_setuzokusi")> _
    Public Property KsSiyouSetuzokusi() As String
        Get
            Return strKsSiyouSetuzokusi
        End Get
        Set(ByVal value As String)
            strKsSiyouSetuzokusi = value
        End Set
    End Property
#End Region

#Region "�H���񍐏��󗝓�"
    ''' <summary>
    ''' �H���񍐏��󗝓�
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojHkksJuriDate As DateTime
    ''' <summary>
    ''' �H���񍐏��󗝓�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H���񍐏��󗝓�</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_juri_date")> _
    Public Property KojHkksJuriDate() As DateTime
        Get
            Return dateKojHkksJuriDate
        End Get
        Set(ByVal value As DateTime)
            dateKojHkksJuriDate = value
        End Set
    End Property
#End Region

#Region "�ۏ؂Ȃ����R"
    ''' <summary>
    ''' �ۏ؂Ȃ����R
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyouNasiRiyuu As String
    ''' <summary>
    ''' �ۏ؂Ȃ����R
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؂Ȃ����R</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_nasi_riyuu")> _
    Public Property HosyouNasiRiyuu() As String
        Get
            Return strHosyouNasiRiyuu
        End Get
        Set(ByVal value As String)
            strHosyouNasiRiyuu = value
        End Set
    End Property
#End Region

#Region "�\����"
    ''' <summary>
    ''' �\����
    ''' </summary>
    ''' <remarks></remarks>
    Private strDisplayName As String
    ''' <summary>
    ''' �\����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �\����</returns>
    ''' <remarks></remarks>
    <TableMap("DisplayName")> _
    Public Property DisplayName() As String
        Get
            Return strDisplayName
        End Get
        Set(ByVal value As String)
            strDisplayName = value
        End Set
    End Property
#End Region

#Region "�ۏ؏����s�L��"
    ''' <summary>
    ''' �ۏ؏����s�L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoHakUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' �ۏ؏����s�L��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_umu")> _
    Public Property HosyousyoHakUmu() As Integer
        Get
            Return intHosyousyoHakUmu
        End Get
        Set(ByVal value As Integer)
            intHosyousyoHakUmu = value
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
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kikan_MK")> _
    Public Property HosyouKikanMK() As Integer
        Get
            Return intHosyouKikanMK
        End Get
        Set(ByVal value As Integer)
            intHosyouKikanMK = value
        End Set
    End Property
#End Region

#Region "�ۏ؊���(�n��)"
    ''' <summary>
    ''' �ۏ؊���(�n��)
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouKikanTJ As Integer = Integer.MinValue
    ''' <summary>
    ''' �ۏ؊���(�n��)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kikan_TJ")> _
    Public Property HosyouKikanTJ() As Integer
        Get
            Return intHosyouKikanTJ
        End Get
        Set(ByVal value As Integer)
            intHosyouKikanTJ = value
        End Set
    End Property
#End Region

#Region "�˗���"
    ''' <summary>
    ''' �˗���
    ''' </summary>
    ''' <remarks></remarks>
    Private dateIraiDate As DateTime
    ''' <summary>
    ''' �˗���
    ''' </summary>
    ''' <value></value>
    ''' <returns> �˗���</returns>
    ''' <remarks></remarks>
    <TableMap("irai_date")> _
    Public Property IraiDate() As DateTime
        Get
            Return dateIraiDate
        End Get
        Set(ByVal value As DateTime)
            dateIraiDate = value
        End Set
    End Property
#End Region

#Region "���l"
    ''' <summary>
    ''' ���l
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou As String
    ''' <summary>
    ''' ���l
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���l</returns>
    ''' <remarks></remarks>
    <TableMap("bikou")> _
    Public Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "�X�V����"
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V����</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime")> _
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "���ӎ������"
    ''' <summary>
    ''' ���ӎ������
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyuuijikouSyubetu As Integer
    ''' <summary>
    ''' ���ӎ������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���ӎ������</returns>
    ''' <remarks></remarks>
    <TableMap("tyuuijikou_syubetu")> _
    Public Property TyuuijikouSyubetu() As Integer
        Get
            Return intTyuuijikouSyubetu
        End Get
        Set(ByVal value As Integer)
            intTyuuijikouSyubetu = value
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
    <TableMap("hak_irai_hw_date")> _
    Public Property HakIraiHwDate() As DateTime
        Get
            Return dateHakIraiHwDate
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiHwDate = value
        End Set
    End Property
#End Region

#Region "�ۏ؏��Ĕ��s��"
    ''' <summary>
    ''' �ۏ؏��Ĕ��s��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoSaihakDate As DateTime
    ''' <summary>
    ''' �ۏ؏��Ĕ��s��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏��Ĕ��s��</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_saihak_date")> _
    Public Property HosyousyoSaihakDate() As DateTime
        Get
            Return dateHosyousyoSaihakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoSaihakDate = value
        End Set
    End Property
#End Region

#Region "�˗���������"
    ''' <summary>
    ''' �˗���������
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknName As String
    ''' <summary>
    ''' �˗���������
    ''' </summary>
    ''' <value></value>
    ''' <returns> �˗���������</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_name")> _
    Public Property HakIraiBknName() As String
        Get
            Return strHakIraiBknName
        End Get
        Set(ByVal value As String)
            strHakIraiBknName = value
        End Set
    End Property
#End Region

#Region "�˗��������ݒn1"
    ''' <summary>
    ''' �˗��������ݒn1
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknAdr1 As String
    ''' <summary>
    ''' �˗��������ݒn1
    ''' </summary>
    ''' <value></value>
    ''' <returns> �˗��������ݒn1</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_adr1")> _
    Public Property HakIraiBknAdr1() As String
        Get
            Return strHakIraiBknAdr1
        End Get
        Set(ByVal value As String)
            strHakIraiBknAdr1 = value
        End Set
    End Property
#End Region

#Region "�˗��������ݒn2"
    ''' <summary>
    ''' �˗��������ݒn2
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknAdr2 As String
    ''' <summary>
    ''' �˗��������ݒn2
    ''' </summary>
    ''' <value></value>
    ''' <returns> �˗��������ݒn2</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_adr2")> _
    Public Property HakIraiBknAdr2() As String
        Get
            Return strHakIraiBknAdr2
        End Get
        Set(ByVal value As String)
            strHakIraiBknAdr2 = value
        End Set
    End Property
#End Region

#Region "�˗��������ݒn3"
    ''' <summary>
    ''' �˗��������ݒn3
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknAdr3 As String
    ''' <summary>
    ''' �˗��������ݒn2
    ''' </summary>
    ''' <value></value>
    ''' <returns> �˗��������ݒn3</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_adr3")> _
    Public Property HakIraiBknAdr3() As String
        Get
            Return strHakIraiBknAdr3
        End Get
        Set(ByVal value As String)
            strHakIraiBknAdr3 = value
        End Set
    End Property
#End Region

#Region "�o�R"
    ''' <summary>
    ''' �o�R
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeiyu As Integer
    ''' <summary>
    ''' �o�R
    ''' </summary>
    ''' <value></value>
    ''' <returns> �o�R</returns>
    ''' <remarks></remarks>
    <TableMap("keiyu")> _
    Public Property Keiyu() As Integer
        Get
            Return intKeiyu
        End Get
        Set(ByVal value As Integer)
            intKeiyu = value
        End Set
    End Property
#End Region

#Region "�ۏ؏�NO"
    ''' <summary>
    ''' �ۏ؏�NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNoTk As String
    ''' <summary>
    ''' �ۏ؏�NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏�NO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no_TK")> _
    Public Property HosyousyoNoTk() As String
        Get
            Return strHosyousyoNoTk
        End Get
        Set(ByVal value As String)
            strHosyousyoNoTk = value
        End Set
    End Property
#End Region

#Region "������"
    ''' <summary>
    ''' ������
    ''' </summary>
    ''' <remarks></remarks>
    Private intBukkenJyky As Integer = Integer.MinValue
    ''' <summary>
    ''' ������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyky")> _
    Public Property BukkenJyky() As Integer
        Get
            Return intBukkenJyky
        End Get
        Set(ByVal value As Integer)
            intBukkenJyky = value
        End Set
    End Property
#End Region

#Region "���lTK"
    ''' <summary>
    ''' ���lTK
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikouTk As String
    ''' <summary>
    ''' ���lTK
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���lTK</returns>
    ''' <remarks></remarks>
    <TableMap("bikou_TK")> _
    Public Property BikouTk() As String
        Get
            Return strBikouTk
        End Get
        Set(ByVal value As String)
            strBikouTk = value
        End Set
    End Property
#End Region

#Region "�ް��j����"
    ''' <summary>
    ''' �ް��j����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDataHakiDate As DateTime
    ''' <summary>
    ''' �ް��j����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ް��j����</returns>
    ''' <remarks></remarks>
    <TableMap("data_haki_date")> _
    Public Property DataHakiDate() As DateTime
        Get
            Return dateDataHakiDate
        End Get
        Set(ByVal value As DateTime)
            dateDataHakiDate = value
        End Set
    End Property
#End Region

#Region "���l2"
    ''' <summary>
    ''' ���l2
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou2 As String
    ''' <summary>
    ''' ���l2
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���l2</returns>
    ''' <remarks></remarks>
    <TableMap("bikou2")> _
    Public Property Bikou2() As String
        Get
            Return strBikou2
        End Get
        Set(ByVal value As String)
            strBikou2 = value
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
    <TableMap("keiyaku_no")> _
    Public Property KeiyakuNo() As String
        Get
            Return strKeiyakuNo
        End Get
        Set(ByVal value As String)
            strKeiyakuNo = value
        End Set
    End Property
#End Region

#Region "��b�H�������񍐏�����"
    ''' <summary>
    ''' ��b�H�������񍐏�����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKsKojKanryHkksTykDate As DateTime
    ''' <summary>
    ''' ��b�H�������񍐏�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��b�H�������񍐏�����</returns>
    ''' <remarks></remarks>
    <TableMap("ks_koj_kanry_hkks_tyk_date")> _
    Public Property KsKojKanryHkksTykDate() As DateTime
        Get
            Return dateKsKojKanryHkksTykDate
        End Get
        Set(ByVal value As DateTime)
            dateKsKojKanryHkksTykDate = value
        End Set
    End Property
#End Region

#Region "�ۏ؏����s��"
    ''' <summary>
    ''' �ۏ؏����s��
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoHakJyky As Integer
    ''' <summary>
    ''' �ۏ؏����s��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏����s��</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_jyky")> _
    Public Property HosyousyoHakJyky() As Integer
        Get
            Return intHosyousyoHakJyky
        End Get
        Set(ByVal value As Integer)
            intHosyousyoHakJyky = value
        End Set
    End Property
#End Region

#Region "�ۏ؏����s�󋵐ݒ��"
    ''' <summary>
    ''' �ۏ؏����s�󋵐ݒ��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakJykySetteiDate As DateTime
    ''' <summary>
    ''' �ۏ؏����s�󋵐ݒ��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏����s�󋵐ݒ��</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_jyky_settei_date")> _
    Public Property HosyousyoHakJykySetteiDate() As DateTime
        Get
            Return dateHosyousyoHakJykySetteiDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakJykySetteiDate = value
        End Set
    End Property
#End Region

#Region "�ۏؗL��"
    ''' <summary>
    ''' �ۏؗL��
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouUmu As Integer
    ''' <summary>
    ''' �ۏؗL��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏؗL��</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_umu")> _
    Public Property HosyouUmu() As Integer
        Get
            Return intHosyouUmu
        End Get
        Set(ByVal value As Integer)
            intHosyouUmu = value
        End Set
    End Property
#End Region

#Region "�ۏ؏��i�L��"
    ''' <summary>
    ''' �ۏ؏��i�L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouSyouhinUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' �ۏ؏��i�L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏��i�L��</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_syouhin_umu")> _
    Public Property HosyouSyouhinUmu() As Integer
        Get
            Return intHosyouSyouhinUmu
        End Get
        Set(ByVal value As Integer)
            intHosyouSyouhinUmu = value
        End Set
    End Property
#End Region

#Region "�ۏ؏����s�˗����L��"
    ''' <summary>
    ''' �ۏ؏����s�˗����L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoHakIraisyoUmu As Integer
    ''' <summary>
    ''' �ۏ؏����s�˗����L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏����s�˗����L��</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_iraisyo_umu")> _
    Public Property HosyousyoHakIraisyoUmu() As Integer
        Get
            Return intHosyousyoHakIraisyoUmu
        End Get
        Set(ByVal value As Integer)
            intHosyousyoHakIraisyoUmu = value
        End Set
    End Property
#End Region

#Region "�t�ۏؖ���FLG"
    ''' <summary>
    ''' �t�ۏؖ���FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intFuhoSyoumeisyoFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' �t�ۏؖ���FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>�t�ۏؖ���FLG</returns>
    ''' <remarks></remarks>
    <TableMap("fuho_syoumeisyo_flg")> _
    Public Property FuhoSyoumeisyoFlg() As Integer
        Get
            Return intFuhoSyoumeisyoFlg
        End Get
        Set(ByVal value As Integer)
            intFuhoSyoumeisyoFlg = value
        End Set
    End Property
#End Region

#Region "�ύX�\������X�R�[�h"
    ''' <summary>
    ''' �ύX�\������X�R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strHenkouYoteiKameitenCd As String
    ''' <summary>
    ''' �ύX�\������X�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ύX�\������X�R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("henkou_yotei_kameiten_cd")> _
    Public Property HenkouYoteiKameitenCd() As String
        Get
            Return strHenkouYoteiKameitenCd
        End Get
        Set(ByVal value As String)
            strHenkouYoteiKameitenCd = value
        End Set
    End Property
#End Region

#Region "�ۏ؂Ȃ����R�R�[�h"
    ''' <summary>
    ''' �ۏ؂Ȃ����R�R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyouNasiRiyuuCd As String
    ''' <summary>
    ''' �ۏ؂Ȃ����R�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؂Ȃ����R�R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_nasi_riyuu_cd")> _
    Public Property HosyouNasiRiyuuCd() As String
        Get
            Return strHosyouNasiRiyuuCd
        End Get
        Set(ByVal value As String)
            strHosyouNasiRiyuuCd = value
        End Set
    End Property
#End Region

#Region "���s�˗���t����"
    ''' <summary>
    ''' ���s�˗���t����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHakIraiUkeDatetime As DateTime
    ''' <summary>
    ''' ���s�˗���t����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���s�˗���t����</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_uke_datetime")> _
    Public Property HakIraiUkeDatetime() As DateTime
        Get
            Return dateHakIraiUkeDatetime
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiUkeDatetime = value
        End Set
    End Property
#End Region

#Region "���s�˗��L�����Z������"
    ''' <summary>
    ''' ���s�˗��L�����Z������
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHakIraiCanDatetime As DateTime
    ''' <summary>
    ''' ���s�˗��L�����Z������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���s�˗��L�����Z������</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_can_datetime")> _
    Public Property HakIraiCanDatetime() As DateTime
        Get
            Return dateHakIraiCanDatetime
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiCanDatetime = value
        End Set
    End Property
#End Region

End Class

