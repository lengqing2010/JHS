Imports Itis.ApplicationBlocks.ExceptionManagement
''' <summary>
''' �ۏ؉�ʗp�̒n�Ճf�[�^���R�[�h�N���X�ł�
''' </summary>
''' <remarks>�n�Ճe�[�u���̑S���R�[�h�J�����ɉ����A�@�ʃf�[�^��ێ����Ă܂�<br/>
'''          ���i�R�[�h�P�̓@�ʐ������R�[�h�FTeibetuSeikyuuRecord<br/>
'''          ���i�R�[�h�Q�̓@�ʐ������R�[�h�FDictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
'''          ���i�R�[�h�R�̓@�ʐ������R�[�h�FDictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
'''          �ǉ��H���̓@�ʐ������R�[�h�@�@�FTeibetuSeikyuuRecord<br/>
'''          ���ǍH���̓@�ʐ������R�[�h�@�@�FTeibetuSeikyuuRecord<br/>
'''          �����񍐏��̓@�ʐ������R�[�h�@�FTeibetuSeikyuuRecord<br/>
'''          �H���񍐏��̓@�ʐ������R�[�h�@�FTeibetuSeikyuuRecord<br/>
'''          �ۏ؏��̓@�ʐ������R�[�h�@�@�@�FTeibetuSeikyuuRecord<br/>
'''          ��񕥖߂̓@�ʐ������R�[�h�@�@�FTeibetuSeikyuuRecord<br/>
'''          ��L�ȊO�̓@�ʐ������R�[�h    �FList(TeibetuSeikyuuRecord)<br/>
'''          �@�ʓ������R�[�h              �FDictionary(Of String, TeibetuNyuukinRecord)<br/>
''' </remarks>
<TableClassMap("t_jiban")> _
Public Class JibanRecordHosyou
    Inherits JibanRecordBase

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

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
    <TableMap("kbn", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Overrides Property Kbn() As String
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
    <TableMap("hosyousyo_no", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overrides Property HosyousyoNo() As String
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
    Private intDataHakiSyubetu As Integer
    ''' <summary>
    ''' �ް��j�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ް��j�����</returns>
    ''' <remarks></remarks>
    <TableMap("data_haki_syubetu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property DataHakiSyubetu() As Integer
        Get
            Return intDataHakiSyubetu
        End Get
        Set(ByVal value As Integer)
            intDataHakiSyubetu = value
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
    <TableMap("data_haki_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property DataHakiDate() As DateTime
        Get
            Return dateDataHakiDate
        End Get
        Set(ByVal value As DateTime)
            dateDataHakiDate = value
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
    <TableMap("sesyu_mei", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=50)> _
    Public Overrides Property SesyuMei() As String
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
    <TableMap("bukken_jyuusyo1", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Overrides Property BukkenJyuusyo1() As String
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
    <TableMap("bukken_jyuusyo2", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Overrides Property BukkenJyuusyo2() As String
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
    <TableMap("bukken_jyuusyo3", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=54)> _
    Public Overrides Property BukkenJyuusyo3() As String
        Get
            Return strBukkenJyuusyo3
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo3 = value
        End Set
    End Property
#End Region

#Region "�����X����"
    ''' <summary>
    ''' �����X����
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' �����X����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����X����</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overrides Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
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
    <TableMap("bikou", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
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
    ''' <returns> ���l</returns>
    ''' <remarks></remarks>
    <TableMap("bikou2", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=512)> _
    Public Overrides Property Bikou2() As String
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
    <TableMap("keiyaku_no", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property KeiyakuNo() As String
        Get
            Return strKeiyakuNo
        End Get
        Set(ByVal value As String)
            strKeiyakuNo = value
        End Set
    End Property
#End Region

#Region "��b�񍐏��L��"
    ''' <summary>
    ''' ��b�񍐏��L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intKsHkksUmu As Integer
    ''' <summary>
    ''' ��b�񍐏��L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��b�񍐏��L��</returns>
    ''' <remarks></remarks>
    <TableMap("ks_hkks_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KsHkksUmu() As Integer
        Get
            Return intKsHkksUmu
        End Get
        Set(ByVal value As Integer)
            intKsHkksUmu = value
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
    <TableMap("ks_koj_kanry_hkks_tyk_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KsKojKanryHkksTykDate() As DateTime
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
    <TableMap("hosyousyo_hak_jyky", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HosyousyoHakJyky() As Integer
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
    <TableMap("hosyousyo_hak_jyky_settei_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoHakJykySetteiDate() As DateTime
        Get
            Return dateHosyousyoHakJykySetteiDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakJykySetteiDate = value
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
    <TableMap("hosyousyo_hak_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoHakDate() As DateTime
        Get
            Return dateHosyousyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakDate = value
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
    <TableMap("hosyou_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HosyouUmu() As Integer
        Get
            Return intHosyouUmu
        End Get
        Set(ByVal value As Integer)
            intHosyouUmu = value
        End Set
    End Property
#End Region

#Region "�ۏ؊J�n��"
    ''' <summary>
    ''' �ۏ؊J�n��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyouKaisiDate As DateTime
    ''' <summary>
    ''' �ۏ؊J�n��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؊J�n��</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kaisi_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyouKaisiDate() As DateTime
        Get
            Return dateHosyouKaisiDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyouKaisiDate = value
        End Set
    End Property
#End Region

#Region "�ۏ؊���"
    ''' <summary>
    ''' �ۏ؊���
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouKikan As Integer
    ''' <summary>
    ''' �ۏ؊���
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؊���</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kikan", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HosyouKikan() As Integer
        Get
            Return intHosyouKikan
        End Get
        Set(ByVal value As Integer)
            intHosyouKikan = value
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
    <TableMap("hosyou_nasi_riyuu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overrides Property HosyouNasiRiyuu() As String
        Get
            Return strHosyouNasiRiyuu
        End Get
        Set(ByVal value As String)
            strHosyouNasiRiyuu = value
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
    <TableMap("hosyou_syouhin_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HosyouSyouhinUmu() As Integer
        Get
            Return intHosyouSyouhinUmu
        End Get
        Set(ByVal value As Integer)
            intHosyouSyouhinUmu = value
        End Set
    End Property
#End Region

#Region "�ی����"
    ''' <summary>
    ''' �ی����
    ''' </summary>
    ''' <remarks></remarks>
    Private intHokenKaisya As Integer
    ''' <summary>
    ''' �ی����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ی����</returns>
    ''' <remarks></remarks>
    <TableMap("hoken_kaisya", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HokenKaisya() As Integer
        Get
            Return intHokenKaisya
        End Get
        Set(ByVal value As Integer)
            intHokenKaisya = value
        End Set
    End Property
#End Region

#Region "�ی��\����"
    ''' <summary>
    ''' �ی��\����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHokenSinseiTuki As DateTime
    ''' <summary>
    ''' �ی��\����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ی��\����</returns>
    ''' <remarks></remarks>
    <TableMap("hoken_sinsei_tuki", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HokenSinseiTuki() As DateTime
        Get
            Return dateHokenSinseiTuki
        End Get
        Set(ByVal value As DateTime)
            dateHokenSinseiTuki = value
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
    <TableMap("hosyousyo_saihak_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoSaihakDate() As DateTime
        Get
            Return dateHosyousyoSaihakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoSaihakDate = value
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
    <TableMap("hosyousyo_hak_iraisyo_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HosyousyoHakIraisyoUmu() As Integer
        Get
            Return intHosyousyoHakIraisyoUmu
        End Get
        Set(ByVal value As Integer)
            intHosyousyoHakIraisyoUmu = value
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
    <TableMap("hosyousyo_hak_iraisyo_tyk_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoHakIraisyoTykDate() As DateTime
        Get
            Return dateHosyousyoHakIraisyoTykDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakIraisyoTykDate = value
        End Set
    End Property
#End Region

#Region "�ی��\���敪"
    ''' <summary>
    ''' �ی��\���敪
    ''' </summary>
    ''' <remarks></remarks>
    Private intHokenSinseiKbn As Integer
    ''' <summary>
    ''' �ی��\���敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ی��\���敪</returns>
    ''' <remarks></remarks>
    <TableMap("hoken_sinsei_kbn", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HokenSinseiKbn() As Integer
        Get
            Return intHokenSinseiKbn
        End Get
        Set(ByVal value As Integer)
            intHokenSinseiKbn = value
        End Set
    End Property
#End Region

#Region "�X�V۸޲�հ�ްID"
    ''' <summary>
    ''' �X�V۸޲�հ�ްID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' �X�V۸޲�հ�ްID
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V۸޲�հ�ްID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
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
    ''' <remarks>�r��������s���ׁA�������̍X�V���t��ݒ肵�Ă�������<br/>
    '''          �X�V���̓��t�̓V�X�e�����t���ݒ肳��܂�</remarks>
    <TableMap("upd_datetime", IsKey:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "���������"
    ''' <summary>
    ''' ���������
    ''' </summary>
    ''' <remarks></remarks>
    Private strChousaTachiai As String
    ''' <summary>
    ''' ���������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���������</returns>
    ''' <remarks></remarks>
    Public Property ChousaTachiai() As String
        Get
            Return strChousaTachiai
        End Get
        Set(ByVal value As String)
            strChousaTachiai = value
        End Set
    End Property
#End Region

#Region "�X�V��"
    ''' <summary>
    ''' �X�V��
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinsya As String
    ''' <summary>
    ''' �X�V��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V��</returns>
    ''' <remarks></remarks>
    <TableMap("kousinsya", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property Kousinsya() As String
        Get
            Return strKousinsya
        End Get
        Set(ByVal value As String)
            strKousinsya = value
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
    <TableMap("fuho_syoumeisyo_flg", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property FuhoSyoumeisyoFlg() As Integer
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
    <TableMap("henkou_yotei_kameiten_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overrides Property HenkouYoteiKameitenCd() As String
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
    <TableMap("hosyou_nasi_riyuu_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overrides Property HosyouNasiRiyuuCd() As String
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
    <TableMap("hak_irai_uke_datetime", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HakIraiUkeDatetime() As DateTime
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
    <TableMap("hak_irai_can_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HakIraiCanDatetime() As DateTime
        Get
            Return dateHakIraiCanDatetime
        End Get
        Set(ByVal value As DateTime)
            dateHakIraiCanDatetime = value
        End Set
    End Property
#End Region


#Region "���s�˗����̑����"
    ''' <summary>
    ''' ���s�˗����̑����
    ''' </summary>
    ''' <remarks></remarks>
    Private strIraiSonota As String
    ''' <summary>
    ''' ���s�˗����̑����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���s�˗����̑����</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_sonota", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=255)> _
    Public Property IraiSonota() As String
        Get
            Return strIraiSonota
        End Get
        Set(ByVal value As String)
            strIraiSonota = value
        End Set
    End Property
#End Region
End Class