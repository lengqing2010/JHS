''' <summary>
''' �\���C���󒍍σ��R�[�h�N���X/�\���C�����
''' </summary>
''' <remarks>�\���f�[�^�̏C�����Ɏg�p���܂�(�󒍍ώ��p)</remarks>
<TableClassMap("MousikomiIF")> _
Public Class MousikomiSyuuseiJytyzumiRecord
    Inherits MousikomiRecord

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�\��NO"
    ''' <summary>
    ''' �\��NO
    ''' </summary>
    ''' <remarks></remarks>
    Private lngMousikomiNo As Long = Long.MinValue
    ''' <summary>
    ''' �\��NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �\��NO</returns>
    ''' <remarks></remarks>
    <TableMap("mousikomi_no", IsKey:=True, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Overrides Property MousikomiNo() As Long
        Get
            Return lngMousikomiNo
        End Get
        Set(ByVal value As Long)
            lngMousikomiNo = value
        End Set
    End Property
#End Region

#Region "�����A����_�S����"
    ''' <summary>
    ''' �����A����_�S����
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTantouMei As String = String.Empty
    ''' <summary>
    ''' �����A����_�S����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����A����_�S����</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_tantou_mei", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TysRenrakusakiTantouMei() As String
        Get
            Return strTysRenrakusakiTantouMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTantouMei = value
        End Set
    End Property
#End Region

#Region "�S���ҘA����TEL"
    ''' <summary>
    ''' �S���ҘA����TEL
    ''' </summary>
    ''' <remarks></remarks>
    Private intTantousyaRenrakusakiTel As Integer = Integer.MinValue
    ''' <summary>
    ''' �S���ҘA����TEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> �S���ҘA����TEL</returns>
    ''' <remarks></remarks>
    <TableMap("tantousya_renrakusaki_tel", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TantousyaRenrakusakiTel() As Integer
        Get
            Return intTantousyaRenrakusakiTel
        End Get
        Set(ByVal value As Integer)
            intTantousyaRenrakusakiTel = value
        End Set
    End Property
#End Region

#Region "�������̃t���K�i"
    ''' <summary>
    ''' �������̃t���K�i
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenMeiKana As String = String.Empty
    ''' <summary>
    ''' �������̃t���K�i
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������̃t���K�i</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_mei_kana", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=100)> _
    Public Overrides Property BukkenMeiKana() As String
        Get
            Return strBukkenMeiKana
        End Get
        Set(ByVal value As String)
            strBukkenMeiKana = value
        End Set
    End Property
#End Region

#Region "�����ꏊ(�X�֔ԍ�)�P"
    ''' <summary>
    ''' �����ꏊ(�X�֔ԍ�)�P
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysBasyoYuubin1 As String = String.Empty
    ''' <summary>
    ''' �����ꏊ(�X�֔ԍ�)�P
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����ꏊ(�X�֔ԍ�)�P</returns>
    ''' <remarks></remarks>
    <TableMap("tys_basyo_yuubin_1", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
    Public Overrides Property TysBasyoYuubin1() As String
        Get
            Return strTysBasyoYuubin1
        End Get
        Set(ByVal value As String)
            strTysBasyoYuubin1 = value
        End Set
    End Property
#End Region

#Region "�����ꏊ(�X�֔ԍ�)�Q"
    ''' <summary>
    ''' �����ꏊ(�X�֔ԍ�)�Q
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysBasyoYuubin2 As String = String.Empty
    ''' <summary>
    ''' �����ꏊ(�X�֔ԍ�)�Q
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����ꏊ(�X�֔ԍ�)�Q</returns>
    ''' <remarks></remarks>
    <TableMap("tys_basyo_yuubin_2", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=4)> _
    Public Overrides Property TysBasyoYuubin2() As String
        Get
            Return strTysBasyoYuubin2
        End Get
        Set(ByVal value As String)
            strTysBasyoYuubin2 = value
        End Set
    End Property
#End Region

#Region "�����ꏊ(�s���{��)"
    ''' <summary>
    ''' �����ꏊ(�s���{��)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysBasyoTodoufuken As String = String.Empty
    ''' <summary>
    ''' �����ꏊ(�s���{��)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����ꏊ(�s���{��)</returns>
    ''' <remarks></remarks>
    <TableMap("tys_basyo_todoufuken", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overrides Property TysBasyoTodoufuken() As String
        Get
            Return strTysBasyoTodoufuken
        End Get
        Set(ByVal value As String)
            strTysBasyoTodoufuken = value
        End Set
    End Property
#End Region

#Region "������]�敪"
    ''' <summary>
    ''' ������]�敪
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKibouKbn As String = String.Empty
    ''' <summary>
    ''' ������]�敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������]�敪</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kibou_kbn", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=4)> _
    Public Overrides Property TysKibouKbn() As String
        Get
            Return strTysKibouKbn
        End Get
        Set(ByVal value As String)
            strTysKibouKbn = value
        End Set
    End Property
#End Region

#Region "�����(���̑��⑫)"
    ''' <summary>
    ''' �����(���̑��⑫)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTtsySonotaHosoku As String = String.Empty
    ''' <summary>
    ''' �����(���̑��⑫)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����(���̑��⑫)</returns>
    ''' <remarks></remarks>
    <TableMap("tatiaisya_sonota_hosoku", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TtsySonotaHosoku() As String
        Get
            Return strTtsySonotaHosoku
        End Get
        Set(ByVal value As String)
            strTtsySonotaHosoku = value
        End Set
    End Property
#End Region

#Region "SDS��]"
    ''' <summary>
    ''' SDS��]
    ''' </summary>
    ''' <remarks></remarks>
    Private intSdsKibou As Integer = Integer.MinValue
    ''' <summary>
    ''' SDS��]
    ''' </summary>
    ''' <value></value>
    ''' <returns> SDS��]</returns>
    ''' <remarks></remarks>
    <TableMap("sds_kibou", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SdsKibou() As Integer
        Get
            Return intSdsKibou
        End Get
        Set(ByVal value As Integer)
            intSdsKibou = value
        End Set
    End Property
#End Region

#Region "���׏��ʐ�"
    ''' <summary>
    ''' ���׏��ʐ�
    ''' </summary>
    ''' <remarks></remarks>
    Private intNobeyukaMenseki As Integer = Integer.MinValue
    ''' <summary>
    ''' ���׏��ʐ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���׏��ʐ�</returns>
    ''' <remarks></remarks>
    <TableMap("nobeyuka_menseki", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property NobeyukaMenseki() As Integer
        Get
            Return intNobeyukaMenseki
        End Get
        Set(ByVal value As Integer)
            intNobeyukaMenseki = value
        End Set
    End Property
#End Region

#Region "���z�ʐ�"
    ''' <summary>
    ''' ���z�ʐ�
    ''' </summary>
    ''' <remarks></remarks>
    Private intKentikuMenseki As Integer = Integer.MinValue
    ''' <summary>
    ''' ���z�ʐ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���z�ʐ�</returns>
    ''' <remarks></remarks>
    <TableMap("kentiku_menseki", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KentikuMenseki() As Integer
        Get
            Return intKentikuMenseki
        End Get
        Set(ByVal value As Integer)
            intKentikuMenseki = value
        End Set
    End Property
#End Region

#Region "�K�w(�n��)"
    ''' <summary>
    ''' �K�w(�n��)
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisouTika As Integer = Integer.MinValue
    ''' <summary>
    ''' �K�w(�n��)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �K�w(�n��)</returns>
    ''' <remarks></remarks>
    <TableMap("kaisou_tika", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KaisouTika() As Integer
        Get
            Return intKaisouTika
        End Get
        Set(ByVal value As Integer)
            intKaisouTika = value
        End Set
    End Property
#End Region

#Region "�����p�r(�X�ܗp�r)"
    ''' <summary>
    ''' �����p�r(�X�ܗp�r)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTmytNoTenpoYouto As String = String.Empty
    ''' <summary>
    ''' �����p�r(�X�ܗp�r)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����p�r(�X�ܗp�r)</returns>
    ''' <remarks></remarks>
    <TableMap("tmyt_no_tenpo_youto", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TmytNoTenpoYouto() As String
        Get
            Return strTmytNoTenpoYouto
        End Get
        Set(ByVal value As String)
            strTmytNoTenpoYouto = value
        End Set
    End Property
#End Region

#Region "�����p�r(���̑��p�r)"
    ''' <summary>
    ''' �����p�r(���̑��p�r)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTmytNoSonotaYouto As String = String.Empty
    ''' <summary>
    ''' �����p�r(���̑��p�r)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����p�r(���̑��p�r)</returns>
    ''' <remarks></remarks>
    <TableMap("tmyt_no_sonota_youto", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TmytNoSonotaYouto() As String
        Get
            Return strTmytNoSonotaYouto
        End Get
        Set(ByVal value As String)
            strTmytNoSonotaYouto = value
        End Set
    End Property
#End Region

#Region "�n�����"
    ''' <summary>
    ''' �n�����
    ''' </summary>
    ''' <remarks></remarks>
    Private strTiikiTokusei As String = String.Empty
    ''' <summary>
    ''' �n�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �n�����</returns>
    ''' <remarks></remarks>
    <TableMap("tiiki_tokusei", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TiikiTokusei() As String
        Get
            Return strTiikiTokusei
        End Get
        Set(ByVal value As String)
            strTiikiTokusei = value
        End Set
    End Property
#End Region

#Region "�z��b�x�[�XW"
    ''' <summary>
    ''' �z��b�x�[�XW
    ''' </summary>
    ''' <remarks></remarks>
    Private intNunoKsBaseW As Integer = Integer.MinValue
    ''' <summary>
    ''' �z��b�x�[�XW
    ''' </summary>
    ''' <value></value>
    ''' <returns> �z��b�x�[�XW</returns>
    ''' <remarks></remarks>
    <TableMap("nuno_ks_base_w", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property NunoKsBaseW() As Integer
        Get
            Return intNunoKsBaseW
        End Get
        Set(ByVal value As Integer)
            intNunoKsBaseW = value
        End Set
    End Property
#End Region

#Region "�\���b�����オ�荂��"
    ''' <summary>
    ''' �\���b�����オ�荂��
    ''' </summary>
    ''' <remarks></remarks>
    Private intYoteiKsTatiagariTakasa As Integer = Integer.MinValue
    ''' <summary>
    ''' �\���b�����オ�荂��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �\���b�����オ�荂��</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_ks_tatiagari_takasa", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YoteiKsTatiagariTakasa() As Integer
        Get
            Return intYoteiKsTatiagariTakasa
        End Get
        Set(ByVal value As Integer)
            intYoteiKsTatiagariTakasa = value
        End Set
    End Property
#End Region

#Region "�~�n���H��"
    ''' <summary>
    ''' �~�n���H��
    ''' </summary>
    ''' <remarks></remarks>
    Private decSktDouroHaba As Decimal = Decimal.MinValue
    ''' <summary>
    ''' �~�n���H��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n���H��</returns>
    ''' <remarks></remarks>
    <TableMap("skt_douro_haba", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overrides Property SktDouroHaba() As Decimal
        Get
            Return decSktDouroHaba
        End Get
        Set(ByVal value As Decimal)
            decSktDouroHaba = value
        End Set
    End Property
#End Region

#Region "�ʍs�s�ԗ��t���O"
    ''' <summary>
    ''' �ʍs�s�ԗ��t���O
    ''' </summary>
    ''' <remarks></remarks>
    Private intTuukouFukaSyaryouFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' �ʍs�s�ԗ��t���O
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ʍs�s�ԗ��t���O</returns>
    ''' <remarks></remarks>
    <TableMap("tuukou_fuka_syaryou_flg", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TuukouFukaSyaryouFlg() As Integer
        Get
            Return intTuukouFukaSyaryouFlg
        End Get
        Set(ByVal value As Integer)
            intTuukouFukaSyaryouFlg = value
        End Set
    End Property
#End Region

#Region "���H�K���L��"
    ''' <summary>
    ''' ���H�K���L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intDouroKiseiUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' ���H�K���L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���H�K���L��</returns>
    ''' <remarks></remarks>
    <TableMap("douro_kisei_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property DouroKiseiUmu() As Integer
        Get
            Return intDouroKiseiUmu
        End Get
        Set(ByVal value As Integer)
            intDouroKiseiUmu = value
        End Set
    End Property
#End Region

#Region "������Q�L��"
    ''' <summary>
    ''' ������Q�L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intTakasaSyougaiUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' ������Q�L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������Q�L��</returns>
    ''' <remarks></remarks>
    <TableMap("takasa_syougai_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TakasaSyougaiUmu() As Integer
        Get
            Return intTakasaSyougaiUmu
        End Get
        Set(ByVal value As Integer)
            intTakasaSyougaiUmu = value
        End Set
    End Property
#End Region

#Region "�d���L��"
    ''' <summary>
    ''' �d���L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intDensenUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' �d���L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �d���L��</returns>
    ''' <remarks></remarks>
    <TableMap("densen_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property DensenUmu() As Integer
        Get
            Return intDensenUmu
        End Get
        Set(ByVal value As Integer)
            intDensenUmu = value
        End Set
    End Property
#End Region

#Region "�g���l���L��"
    ''' <summary>
    ''' �g���l���L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intTonneruUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' �g���l���L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �g���l���L��</returns>
    ''' <remarks></remarks>
    <TableMap("tonneru_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TonneruUmu() As Integer
        Get
            Return intTonneruUmu
        End Get
        Set(ByVal value As Integer)
            intTonneruUmu = value
        End Set
    End Property
#End Region

#Region "�~�n�����፷"
    ''' <summary>
    ''' �~�n�����፷
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktnKouteisa As Integer = Integer.MinValue
    ''' <summary>
    ''' �~�n�����፷
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�����፷</returns>
    ''' <remarks></remarks>
    <TableMap("sktn_kouteisa", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktnKouteisa() As Integer
        Get
            Return intSktnKouteisa
        End Get
        Set(ByVal value As Integer)
            intSktnKouteisa = value
        End Set
    End Property
#End Region

#Region "�~�n�����፷(�⑫)"
    ''' <summary>
    ''' �~�n�����፷(�⑫)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSktnKouteisaHosoku As String = String.Empty
    ''' <summary>
    ''' �~�n�����፷(�⑫)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�����፷(�⑫)</returns>
    ''' <remarks></remarks>
    <TableMap("sktn_kouteisa_hosoku", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property SktnKouteisaHosoku() As String
        Get
            Return strSktnKouteisaHosoku
        End Get
        Set(ByVal value As String)
            strSktnKouteisaHosoku = value
        End Set
    End Property
#End Region

#Region "�X���[�v�L��"
    ''' <summary>
    ''' �X���[�v�L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intSlopeUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' �X���[�v�L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X���[�v�L��</returns>
    ''' <remarks></remarks>
    <TableMap("slope_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SlopeUmu() As Integer
        Get
            Return intSlopeUmu
        End Get
        Set(ByVal value As Integer)
            intSlopeUmu = value
        End Set
    End Property
#End Region

#Region "�X���[�v(�⑫)"
    ''' <summary>
    ''' �X���[�v(�⑫)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSlopeHosoku As String = String.Empty
    ''' <summary>
    ''' �X���[�v(�⑫)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X���[�v(�⑫)</returns>
    ''' <remarks></remarks>
    <TableMap("slope_hosoku", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property SlopeHosoku() As String
        Get
            Return strSlopeHosoku
        End Get
        Set(ByVal value As String)
            strSlopeHosoku = value
        End Set
    End Property
#End Region

#Region "��������(���̑�)"
    ''' <summary>
    ''' ��������(���̑�)
    ''' </summary>
    ''' <remarks></remarks>
    Private strHannyuuJyknSonota As String = String.Empty
    ''' <summary>
    ''' ��������(���̑�)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��������(���̑�)</returns>
    ''' <remarks></remarks>
    <TableMap("hannyuu_jykn_sonota", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property HannyuuJyknSonota() As String
        Get
            Return strHannyuuJyknSonota
        End Get
        Set(ByVal value As String)
            strHannyuuJyknSonota = value
        End Set
    End Property
#End Region

#Region "�~�n�O��(��n)"
    ''' <summary>
    ''' �~�n�O��(��n)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiTakuti As Integer = Integer.MinValue
    ''' <summary>
    ''' �~�n�O��(��n)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�O��(��n)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_takuti", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiTakuti() As Integer
        Get
            Return intSktZenrekiTakuti
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiTakuti = value
        End Set
    End Property
#End Region

#Region "�~�n�O��(�c)"
    ''' <summary>
    ''' �~�n�O��(�c)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiTa As Integer = Integer.MinValue
    ''' <summary>
    ''' �~�n�O��(�c)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�O��(�c)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_ta", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiTa() As Integer
        Get
            Return intSktZenrekiTa
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiTa = value
        End Set
    End Property
#End Region

#Region "�~�n�O��(��)"
    ''' <summary>
    ''' �~�n�O��(��)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiHatake As Integer = Integer.MinValue
    ''' <summary>
    ''' �~�n�O��(��)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�O��(��)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_hatake", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiHatake() As Integer
        Get
            Return intSktZenrekiHatake
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiHatake = value
        End Set
    End Property
#End Region

#Region "�~�n�O��(�A����)"
    ''' <summary>
    ''' �~�n�O��(�A����)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiSyokuju As Integer = Integer.MinValue
    ''' <summary>
    ''' �~�n�O��(�A����)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�O��(�A����)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_syokuju", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiSyokuju() As Integer
        Get
            Return intSktZenrekiSyokuju
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiSyokuju = value
        End Set
    End Property
#End Region

#Region "�~�n�O��(�G�ؗ�)"
    ''' <summary>
    ''' �~�n�O��(�G�ؗ�)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiZouki As Integer = Integer.MinValue
    ''' <summary>
    ''' �~�n�O��(�G�ؗ�)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�O��(�G�ؗ�)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_zouki", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiZouki() As Integer
        Get
            Return intSktZenrekiZouki
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiZouki = value
        End Set
    End Property
#End Region

#Region "�~�n�O��(���ԏ�)"
    ''' <summary>
    ''' �~�n�O��(���ԏ�)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiTyuusyajyou As Integer = Integer.MinValue
    ''' <summary>
    ''' �~�n�O��(���ԏ�)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�O��(���ԏ�)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_tyuusyajyou", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiTyuusyajyou() As Integer
        Get
            Return intSktZenrekiTyuusyajyou
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiTyuusyajyou = value
        End Set
    End Property
#End Region

#Region "�~�n�O��(����n)"
    ''' <summary>
    ''' �~�n�O��(����n)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiKantakuti As Integer = Integer.MinValue
    ''' <summary>
    ''' �~�n�O��(����n)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�O��(����n)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_kantakuti", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiKantakuti() As Integer
        Get
            Return intSktZenrekiKantakuti
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiKantakuti = value
        End Set
    End Property
#End Region

#Region "�~�n�O��(�H���)"
    ''' <summary>
    ''' �~�n�O��(�H���)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiKoujyouato As Integer = Integer.MinValue
    ''' <summary>
    ''' �~�n�O��(�H���)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�O��(�H���)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_koujyouato", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiKoujyouato() As Integer
        Get
            Return intSktZenrekiKoujyouato
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiKoujyouato = value
        End Set
    End Property
#End Region

#Region "�~�n�O��(���̑�)"
    ''' <summary>
    ''' �~�n�O��(���̑�)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiSonota As Integer = Integer.MinValue
    ''' <summary>
    ''' �~�n�O��(���̑�)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�O��(���̑�)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_sonota", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiSonota() As Integer
        Get
            Return intSktZenrekiSonota
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiSonota = value
        End Set
    End Property
#End Region

#Region "�~�n�O��(���̑��⑫)"
    ''' <summary>
    ''' �~�n�O��(���̑��⑫)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSktZenrekiSonotaHosoku As String = String.Empty
    ''' <summary>
    ''' �~�n�O��(���̑��⑫)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�O��(���̑��⑫)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_sonota_hosoku", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property SktZenrekiSonotaHosoku() As String
        Get
            Return strSktZenrekiSonotaHosoku
        End Get
        Set(ByVal value As String)
            strSktZenrekiSonotaHosoku = value
        End Set
    End Property
#End Region

#Region "��n�����@��"
    ''' <summary>
    ''' ��n�����@��
    ''' </summary>
    ''' <remarks></remarks>
    Private intTakutiZouseiKikan As Integer = Integer.MinValue
    ''' <summary>
    ''' ��n�����@��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��n�����@��</returns>
    ''' <remarks></remarks>
    <TableMap("takuti_zousei_kikan", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TakutiZouseiKikan() As Integer
        Get
            Return intTakutiZouseiKikan
        End Get
        Set(ByVal value As Integer)
            intTakutiZouseiKikan = value
        End Set
    End Property
#End Region

#Region "��������"
    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <remarks></remarks>
    Private intZouseiGessuu As Integer = Integer.MinValue
    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��������</returns>
    ''' <remarks></remarks>
    <TableMap("zousei_gessuu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property ZouseiGessuu() As Integer
        Get
            Return intZouseiGessuu
        End Get
        Set(ByVal value As Integer)
            intZouseiGessuu = value
        End Set
    End Property
#End Region

#Region "�ؓy���y�敪"
    ''' <summary>
    ''' �ؓy���y�敪
    ''' </summary>
    ''' <remarks></remarks>
    Private intKiriMoriKbn As Integer = Integer.MinValue
    ''' <summary>
    ''' �ؓy���y�敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ؓy���y�敪</returns>
    ''' <remarks></remarks>
    <TableMap("kiri_mori_kbn", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KiriMoriKbn() As Integer
        Get
            Return intKiriMoriKbn
        End Get
        Set(ByVal value As Integer)
            intKiriMoriKbn = value
        End Set
    End Property
#End Region

#Region "���������L��"
    ''' <summary>
    ''' ���������L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intKisonTatemonoUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' ���������L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���������L��</returns>
    ''' <remarks></remarks>
    <TableMap("kison_tatemono_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KisonTatemonoUmu() As Integer
        Get
            Return intKisonTatemonoUmu
        End Get
        Set(ByVal value As Integer)
            intKisonTatemonoUmu = value
        End Set
    End Property
#End Region

#Region "��˗L��"
    ''' <summary>
    ''' ��˗L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intIdoUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' ��˗L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��˗L��</returns>
    ''' <remarks></remarks>
    <TableMap("ido_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property IdoUmu() As Integer
        Get
            Return intIdoUmu
        End Get
        Set(ByVal value As Integer)
            intIdoUmu = value
        End Set
    End Property
#End Region

#Region "�򉻑������L��"
    ''' <summary>
    ''' �򉻑������L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intJyoukaGenkyouUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' �򉻑������L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �򉻑������L��</returns>
    ''' <remarks></remarks>
    <TableMap("jyouka_genkyou_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property JyoukaGenkyouUmu() As Integer
        Get
            Return intJyoukaGenkyouUmu
        End Get
        Set(ByVal value As Integer)
            intJyoukaGenkyouUmu = value
        End Set
    End Property
#End Region

#Region "�򉻑��\��L��"
    ''' <summary>
    ''' �򉻑��\��L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intJyoukaYoteiUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' �򉻑��\��L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �򉻑��\��L��</returns>
    ''' <remarks></remarks>
    <TableMap("jyouka_yotei_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property JyoukaYoteiUmu() As Integer
        Get
            Return intJyoukaYoteiUmu
        End Get
        Set(ByVal value As Integer)
            intJyoukaYoteiUmu = value
        End Set
    End Property
#End Region

#Region "�n��"
    ''' <summary>
    ''' �n��
    ''' </summary>
    ''' <remarks></remarks>
    Private intJinawa As Integer = Integer.MinValue
    ''' <summary>
    ''' �n��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �n��</returns>
    ''' <remarks></remarks>
    <TableMap("jinawa", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Jinawa() As Integer
        Get
            Return intJinawa
        End Get
        Set(ByVal value As Integer)
            intJinawa = value
        End Set
    End Property
#End Region

#Region "���E�Y"
    ''' <summary>
    ''' ���E�Y
    ''' </summary>
    ''' <remarks></remarks>
    Private intKyoukaiKui As Integer = Integer.MinValue
    ''' <summary>
    ''' ���E�Y
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���E�Y</returns>
    ''' <remarks></remarks>
    <TableMap("kyoukai_kui", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KyoukaiKui() As Integer
        Get
            Return intKyoukaiKui
        End Get
        Set(ByVal value As Integer)
            intKyoukaiKui = value
        End Set
    End Property
#End Region

#Region "�~�n�̌���(�X�n)"
    ''' <summary>
    ''' �~�n�̌���(�X�n)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktGenkyouSarati As Integer = Integer.MinValue
    ''' <summary>
    ''' �~�n�̌���(�X�n)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�̌���(�X�n)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_genkyou_sarati", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktGenkyouSarati() As Integer
        Get
            Return intSktGenkyouSarati
        End Get
        Set(ByVal value As Integer)
            intSktGenkyouSarati = value
        End Set
    End Property
#End Region

#Region "�~�n�̌���(���ԏ�)"
    ''' <summary>
    ''' �~�n�̌���(���ԏ�)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktGenkyouTyuusyajyou As Integer = Integer.MinValue
    ''' <summary>
    ''' �~�n�̌���(���ԏ�)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�̌���(���ԏ�)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_genkyou_tyuusyajyou", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktGenkyouTyuusyajyou() As Integer
        Get
            Return intSktGenkyouTyuusyajyou
        End Get
        Set(ByVal value As Integer)
            intSktGenkyouTyuusyajyou = value
        End Set
    End Property
#End Region

#Region "�~�n�̌���(�_�k�n)"
    ''' <summary>
    ''' �~�n�̌���(�_�k�n)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktGenkyouNoukouti As Integer = Integer.MinValue
    ''' <summary>
    ''' �~�n�̌���(�_�k�n)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�̌���(�_�k�n)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_genkyou_noukouti", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktGenkyouNoukouti() As Integer
        Get
            Return intSktGenkyouNoukouti
        End Get
        Set(ByVal value As Integer)
            intSktGenkyouNoukouti = value
        End Set
    End Property
#End Region

#Region "�~�n�̌���(���̑�)"
    ''' <summary>
    ''' �~�n�̌���(���̑�)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktGenkyouSonota As Integer = Integer.MinValue
    ''' <summary>
    ''' �~�n�̌���(���̑�)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�̌���(���̑�)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_genkyou_sonota", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktGenkyouSonota() As Integer
        Get
            Return intSktGenkyouSonota
        End Get
        Set(ByVal value As Integer)
            intSktGenkyouSonota = value
        End Set
    End Property
#End Region

#Region "�~�n�̌���(���̑��⑫)"
    ''' <summary>
    ''' �~�n�̌���(���̑��⑫)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSktGenkyouSonotaHosoku As String = String.Empty
    ''' <summary>
    ''' �~�n�̌���(���̑��⑫)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �~�n�̌���(���̑��⑫)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_genkyou_sonota_hosoku", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property SktGenkyouSonotaHosoku() As String
        Get
            Return strSktGenkyouSonotaHosoku
        End Get
        Set(ByVal value As String)
            strSktGenkyouSonotaHosoku = value
        End Set
    End Property
#End Region

#Region "���y�Ɋւ���(�����O���{�ϐ��y��)"
    ''' <summary>
    ''' ���y�Ɋւ���(�����O���{�ϐ��y��)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysMaeJissiZumiMoritutiAtusa As String = String.Empty
    ''' <summary>
    ''' ���y�Ɋւ���(�����O���{�ϐ��y��)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���y�Ɋւ���(�����O���{�ϐ��y��)</returns>
    ''' <remarks></remarks>
    <TableMap("mrtt_tys_mae_jissi_zumi_morituti_atusa", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TysMaeJissiZumiMoritutiAtusa() As String
        Get
            Return strTysMaeJissiZumiMoritutiAtusa
        End Get
        Set(ByVal value As String)
            strTysMaeJissiZumiMoritutiAtusa = value
        End Set
    End Property
#End Region

#Region "���y�Ɋւ���(������\�萷�y��)"
    ''' <summary>
    ''' ���y�Ɋւ���(������\�萷�y��)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysAtoYoteiMoritutiAtusa As String = String.Empty
    ''' <summary>
    ''' ���y�Ɋւ���(������\�萷�y��)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���y�Ɋւ���(������\�萷�y��)</returns>
    ''' <remarks></remarks>
    <TableMap("mrtt_tys_ato_yotei_morituti_atusa", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TysAtoYoteiMoritutiAtusa() As String
        Get
            Return strTysAtoYoteiMoritutiAtusa
        End Get
        Set(ByVal value As String)
            strTysAtoYoteiMoritutiAtusa = value
        End Set
    End Property
#End Region

#Region "�i��(�v���L���X�g)"
    ''' <summary>
    ''' �i��(�v���L���X�g)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkPreCast As Integer = Integer.MinValue
    ''' <summary>
    ''' �i��(�v���L���X�g)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �i��(�v���L���X�g)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_pre_cast", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkPreCast() As Integer
        Get
            Return intYhkPreCast
        End Get
        Set(ByVal value As Integer)
            intYhkPreCast = value
        End Set
    End Property
#End Region

#Region "�i��(����ł�)"
    ''' <summary>
    ''' �i��(����ł�)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkGenbaUti As Integer = Integer.MinValue
    ''' <summary>
    ''' �i��(����ł�)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �i��(����ł�)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_genba_uti", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkGenbaUti() As Integer
        Get
            Return intYhkGenbaUti
        End Get
        Set(ByVal value As Integer)
            intYhkGenbaUti = value
        End Set
    End Property
#End Region

#Region "�i��(�Ԓm�u���b�N)"
    ''' <summary>
    ''' �i��(�Ԓm�u���b�N)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkKantiBlock As Integer = Integer.MinValue
    ''' <summary>
    ''' �i��(�Ԓm�u���b�N)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �i��(�Ԓm�u���b�N)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_kanti_block", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkKantiBlock() As Integer
        Get
            Return intYhkKantiBlock
        End Get
        Set(ByVal value As Integer)
            intYhkKantiBlock = value
        End Set
    End Property
#End Region

#Region "�i��(CB)"
    ''' <summary>
    ''' �i��(CB)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkCb As Integer = Integer.MinValue
    ''' <summary>
    ''' �i��(CB)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �i��(CB)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_cb", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkCb() As Integer
        Get
            Return intYhkCb
        End Get
        Set(ByVal value As Integer)
            intYhkCb = value
        End Set
    End Property
#End Region

#Region "�i��(���ݍς�)"
    ''' <summary>
    ''' �i��(���ݍς�)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkKisetuZumi As Integer = Integer.MinValue
    ''' <summary>
    ''' �i��(���ݍς�)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �i��(���ݍς�)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_kisetu_zumi", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkKisetuZumi() As Integer
        Get
            Return intYhkKisetuZumi
        End Get
        Set(ByVal value As Integer)
            intYhkKisetuZumi = value
        End Set
    End Property
#End Region

#Region "�i��(�V�ݗ\��)"
    ''' <summary>
    ''' �i��(�V�ݗ\��)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkSinsetuYotei As Integer = Integer.MinValue
    ''' <summary>
    ''' �i��(�V�ݗ\��)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �i��(�V�ݗ\��)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_sinsetu_yotei", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkSinsetuYotei() As Integer
        Get
            Return intYhkSinsetuYotei
        End Get
        Set(ByVal value As Integer)
            intYhkSinsetuYotei = value
        End Set
    End Property
#End Region

#Region "�i��(�ݒu�o�ߔN��)"
    ''' <summary>
    ''' �i��(�ݒu�o�ߔN��)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkSettiKeikaNensuu As Integer = Integer.MinValue
    ''' <summary>
    ''' �i��(�ݒu�o�ߔN��)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �i��(�ݒu�o�ߔN��)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_setti_keika_nensuu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkSettiKeikaNensuu() As Integer
        Get
            Return intYhkSettiKeikaNensuu
        End Get
        Set(ByVal value As Integer)
            intYhkSettiKeikaNensuu = value
        End Set
    End Property
#End Region

#Region "�i��(����)"
    ''' <summary>
    ''' �i��(����)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkTakasa As Integer = Integer.MinValue
    ''' <summary>
    ''' �i��(����)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �i��(����)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_takasa", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkTakasa() As Integer
        Get
            Return intYhkTakasa
        End Get
        Set(ByVal value As Integer)
            intYhkTakasa = value
        End Set
    End Property
#End Region

#Region "�i��(�v�捂��)"
    ''' <summary>
    ''' �i��(�v�捂��)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkKeikakuTakasa As Integer = Integer.MinValue
    ''' <summary>
    ''' �i��(�v�捂��)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �i��(�v�捂��)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_keikaku_takasa", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkKeikakuTakasa() As Integer
        Get
            Return intYhkKeikakuTakasa
        End Get
        Set(ByVal value As Integer)
            intYhkKeikakuTakasa = value
        End Set
    End Property
#End Region

#Region "�i��(�����m�F)"
    ''' <summary>
    ''' �i��(�����m�F)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkYakusyoKakunin As Integer = Integer.MinValue
    ''' <summary>
    ''' �i��(�����m�F)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �i��(�����m�F)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_yakusyo_kakunin", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkYakusyoKakunin() As Integer
        Get
            Return intYhkYakusyoKakunin
        End Get
        Set(ByVal value As Integer)
            intYhkYakusyoKakunin = value
        End Set
    End Property
#End Region

#Region "�n�c���̗v��"
    ''' <summary>
    ''' �n�c���̗v��
    ''' </summary>
    ''' <remarks></remarks>
    Private intHaturiYouhi As Integer = Integer.MinValue
    ''' <summary>
    ''' �n�c���̗v��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �n�c���̗v��</returns>
    ''' <remarks></remarks>
    <TableMap("haturi_youhi", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HaturiYouhi() As Integer
        Get
            Return intHaturiYouhi
        End Get
        Set(ByVal value As Integer)
            intHaturiYouhi = value
        End Set
    End Property
#End Region

#Region "�X�V��"
    ''' <summary>
    ''' �X�V��
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinsya As String = String.Empty
    ''' <summary>
    ''' �X�V��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V��</returns>
    ''' <remarks></remarks>
    <TableMap("kousinsya", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property Kousinsya() As String
        Get
            Return strKousinsya
        End Get
        Set(ByVal value As String)
            strKousinsya = value
        End Set
    End Property
#End Region

#Region "�X�V����"
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime = DateTime.MinValue
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V����</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "�X�V���O�C�����[�U�[ID"
    ''' <summary>
    ''' �X�V���O�C�����[�U�[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String = String.Empty
    ''' <summary>
    ''' �X�V���O�C�����[�U�[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V���O�C�����[�U�[ID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

End Class
