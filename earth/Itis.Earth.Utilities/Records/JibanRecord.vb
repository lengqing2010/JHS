Imports Itis.ApplicationBlocks.ExceptionManagement
''' <summary>
''' �n�Ճf�[�^�̃��R�[�h�N���X�ł�
''' </summary>
''' <remarks>�n�Ճe�[�u���̑S���R�[�h�J�����ɉ����A�@�ʃf�[�^��ێ����Ă܂�<br/>
'''          ���i�R�[�h�P�̓@�ʐ������R�[�h�FTeibetuSeikyuuRecord<br/>
'''          ���i�R�[�h�Q�̓@�ʐ������R�[�h�FDictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
'''          ���i�R�[�h�R�̓@�ʐ������R�[�h�FDictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
'''          ���i�R�[�h�S�̓@�ʐ������R�[�h�FDictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
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
Public Class JibanRecord
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

#Region "�󒍕�����"
    ''' <summary>
    ''' �󒍕�����
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyutyuuBukkenMei As String
    ''' <summary>
    ''' �󒍕�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �󒍕�����</returns>
    ''' <remarks></remarks>
    <TableMap("jutyuu_bukken_mei", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=50)> _
    Public Overrides Property JyutyuuBukkenMei() As String
        Get
            Return strJyutyuuBukkenMei
        End Get
        Set(ByVal value As String)
            strJyutyuuBukkenMei = value
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
    ''' <returns> �����R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("bunjou_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property BunjouCd() As Integer
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
    Private strBukkenNayoseCd As String = String.Empty
    ''' <summary>
    ''' ��������R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��������R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_nayose_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=11)> _
    Public Overrides Property BukkenNayoseCd() As String
        Get
            Return strBukkenNayoseCd
        End Get
        Set(ByVal value As String)
            strBukkenNayoseCd = value
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

#Region "���i�敪"
    ''' <summary>
    ''' ���i�敪
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouhinKbn As Integer
    ''' <summary>
    ''' ���i�敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���i�敪</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_kbn", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SyouhinKbn() As Integer
        Get
            Return intSyouhinKbn
        End Get
        Set(ByVal value As Integer)
            intSyouhinKbn = value
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
    <TableMap("irai_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property IraiDate() As DateTime
        Get
            Return dateIraiDate
        End Get
        Set(ByVal value As DateTime)
            dateIraiDate = value
        End Set
    End Property
#End Region

#Region "�˗��S����"
    ''' <summary>
    ''' �˗��S����
    ''' </summary>
    ''' <remarks></remarks>
    Private strIraiTantousyaMei As String
    ''' <summary>
    ''' �˗��S����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �˗��S����</returns>
    ''' <remarks></remarks>
    <TableMap("irai_tantousya_mei", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overrides Property IraiTantousyaMei() As String
        Get
            Return strIraiTantousyaMei
        End Get
        Set(ByVal value As String)
            strIraiTantousyaMei = value
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

#Region "TH���r�L��"
    ''' <summary>
    ''' TH���r�L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intThKasiUmu As Integer
    ''' <summary>
    ''' TH���r�L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> TH���r�L��</returns>
    ''' <remarks></remarks>
    <TableMap("th_kasi_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property ThKasiUmu() As Integer
        Get
            Return intThKasiUmu
        End Get
        Set(ByVal value As Integer)
            intThKasiUmu = value
        End Set
    End Property
#End Region

#Region "�K�w"
    ''' <summary>
    ''' �K�w
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisou As Integer = Integer.MinValue
    ''' <summary>
    ''' �K�w
    ''' </summary>
    ''' <value></value>
    ''' <returns> �K�w</returns>
    ''' <remarks></remarks>
    <TableMap("kaisou", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Kaisou() As Integer
        Get
            Return intKaisou
        End Get
        Set(ByVal value As Integer)
            intKaisou = value
        End Set
    End Property
#End Region

#Region "�V�z����"
    ''' <summary>
    ''' �V�z����
    ''' </summary>
    ''' <remarks></remarks>
    Private intSintikuTatekae As Integer
    ''' <summary>
    ''' �V�z����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �V�z����</returns>
    ''' <remarks></remarks>
    <TableMap("sintiku_tatekae", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SintikuTatekae() As Integer
        Get
            Return intSintikuTatekae
        End Get
        Set(ByVal value As Integer)
            intSintikuTatekae = value
        End Set
    End Property
#End Region

#Region "�\��"
    ''' <summary>
    ''' �\��
    ''' </summary>
    ''' <remarks></remarks>
    Private intKouzou As Integer
    ''' <summary>
    ''' �\��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �\��</returns>
    ''' <remarks></remarks>
    <TableMap("kouzou", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Kouzou() As Integer
        Get
            Return intKouzou
        End Get
        Set(ByVal value As Integer)
            intKouzou = value
        End Set
    End Property
#End Region

#Region "�\��MEMO"
    ''' <summary>
    ''' �\��MEMO
    ''' </summary>
    ''' <remarks></remarks>
    Private strKouzouMemo As String
    ''' <summary>
    ''' �\��MEMO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �\��MEMO</returns>
    ''' <remarks></remarks>
    <TableMap("kouzou_memo", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overrides Property KouzouMemo() As String
        Get
            Return strKouzouMemo
        End Get
        Set(ByVal value As String)
            strKouzouMemo = value
        End Set
    End Property
#End Region

#Region "�Ԍ�"
    ''' <summary>
    ''' �Ԍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyako As Integer
    ''' <summary>
    ''' �Ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �Ԍ�</returns>
    ''' <remarks></remarks>
    <TableMap("syako", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Syako() As Integer
        Get
            Return intSyako
        End Get
        Set(ByVal value As Integer)
            intSyako = value
        End Set
    End Property
#End Region

#Region "���؂�[��"
    ''' <summary>
    ''' ���؂�[��
    ''' </summary>
    ''' <remarks></remarks>
    Private decNegiriHukasa As Decimal
    ''' <summary>
    ''' ���؂�[��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���؂�[��</returns>
    ''' <remarks></remarks>
    <TableMap("negiri_hukasa", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overrides Property NegiriHukasa() As Decimal
        Get
            Return decNegiriHukasa
        End Get
        Set(ByVal value As Decimal)
            decNegiriHukasa = value
        End Set
    End Property
#End Region

#Region "�\�萷�y����"
    ''' <summary>
    ''' �\�萷�y����
    ''' </summary>
    ''' <remarks></remarks>
    Private decYoteiMoritutiAtusa As Decimal
    ''' <summary>
    ''' �\�萷�y����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �\�萷�y����</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_morituti_atusa", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overrides Property YoteiMoritutiAtusa() As Decimal
        Get
            Return decYoteiMoritutiAtusa
        End Get
        Set(ByVal value As Decimal)
            decYoteiMoritutiAtusa = value
        End Set
    End Property
#End Region

#Region "�\���b"
    ''' <summary>
    ''' �\���b
    ''' </summary>
    ''' <remarks></remarks>
    Private intYoteiKs As Integer
    ''' <summary>
    ''' �\���b
    ''' </summary>
    ''' <value></value>
    ''' <returns> �\���b</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_ks", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YoteiKs() As Integer
        Get
            Return intYoteiKs
        End Get
        Set(ByVal value As Integer)
            intYoteiKs = value
        End Set
    End Property
#End Region

#Region "�\���bMEMO"
    ''' <summary>
    ''' �\���bMEMO
    ''' </summary>
    ''' <remarks></remarks>
    Private strYoteiKsMemo As String
    ''' <summary>
    ''' �\���bMEMO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �\���bMEMO</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_ks_memo", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overrides Property YoteiKsMemo() As String
        Get
            Return strYoteiKsMemo
        End Get
        Set(ByVal value As String)
            strYoteiKsMemo = value
        End Set
    End Property
#End Region

#Region "������ЃR�[�h"
    ''' <summary>
    ''' ������ЃR�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' ������ЃR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������ЃR�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overrides Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "������Ў��Ə��R�[�h"
    ''' <summary>
    ''' ������Ў��Ə��R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaJigyousyoCd As String
    ''' <summary>
    ''' ������Ў��Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������Ў��Ə��R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_jigyousyo_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overrides Property TysKaisyaJigyousyoCd() As String
        Get
            Return strTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "�������@"
    ''' <summary>
    ''' �������@
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysHouhou As Integer = Integer.MinValue
    ''' <summary>
    ''' �������@
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������@</returns>
    ''' <remarks></remarks>
    <TableMap("tys_houhou", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TysHouhou() As Integer
        Get
            Return intTysHouhou
        End Get
        Set(ByVal value As Integer)
            intTysHouhou = value
        End Set
    End Property
#End Region

#Region "�����T�v"
    ''' <summary>
    ''' �����T�v
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysGaiyou As Integer
    ''' <summary>
    ''' �����T�v
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����T�v</returns>
    ''' <remarks></remarks>
    <TableMap("tys_gaiyou", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TysGaiyou() As Integer
        Get
            Return intTysGaiyou
        End Get
        Set(ByVal value As Integer)
            intTysGaiyou = value
        End Set
    End Property
#End Region

#Region "FC����ް�̔����z"
    ''' <summary>
    ''' FC����ް�̔����z
    ''' </summary>
    ''' <remarks></remarks>
    Private intFcBuilderHanbaiGaku As Integer
    ''' <summary>
    ''' FC����ް�̔����z
    ''' </summary>
    ''' <value></value>
    ''' <returns> FC����ް�̔����z</returns>
    ''' <remarks></remarks>
    <TableMap("fc_builder_hanbai_gaku", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property FcBuilderHanbaiGaku() As Integer
        Get
            Return intFcBuilderHanbaiGaku
        End Get
        Set(ByVal value As Integer)
            intFcBuilderHanbaiGaku = value
        End Set
    End Property
#End Region

#Region "������]��"
    ''' <summary>
    ''' ������]��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysKibouDate As DateTime
    ''' <summary>
    ''' ������]��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������]��</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kibou_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysKibouDate() As DateTime
        Get
            Return dateTysKibouDate
        End Get
        Set(ByVal value As DateTime)
            dateTysKibouDate = value
        End Set
    End Property
#End Region

#Region "������]����"
    ''' <summary>
    ''' ������]����
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKibouJikan As String
    ''' <summary>
    ''' ������]����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������]����</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kibou_jikan", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=26)> _
    Public Overrides Property TysKibouJikan() As String
        Get
            Return strTysKibouJikan
        End Get
        Set(ByVal value As String)
            strTysKibouJikan = value
        End Set
    End Property
#End Region

#Region "����L��"
    ''' <summary>
    ''' ����L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intTatiaiUmu As Integer
    ''' <summary>
    ''' ����L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����L��</returns>
    ''' <remarks></remarks>
    <TableMap("tatiai_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TatiaiUmu() As Integer
        Get
            Return intTatiaiUmu
        End Get
        Set(ByVal value As Integer)
            intTatiaiUmu = value
        End Set
    End Property
#End Region

#Region "����҃R�[�h"
    ''' <summary>
    ''' ����҃R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private intTatiaisyaCd As Integer
    ''' <summary>
    ''' ����҃R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����҃R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("tatiaisya_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TatiaisyaCd() As Integer
        Get
            Return intTatiaisyaCd
        End Get
        Set(ByVal value As Integer)
            intTatiaisyaCd = value
        End Set
    End Property
#End Region

#Region "�Y�t_���ʐ}"
    ''' <summary>
    ''' �Y�t_���ʐ}
    ''' </summary>
    ''' <remarks></remarks>
    Private intTenpuHeimenzu As Integer
    ''' <summary>
    ''' �Y�t_���ʐ}
    ''' </summary>
    ''' <value></value>
    ''' <returns> �Y�t_���ʐ}</returns>
    ''' <remarks></remarks>
    <TableMap("tenpu_heimenzu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TenpuHeimenzu() As Integer
        Get
            Return intTenpuHeimenzu
        End Get
        Set(ByVal value As Integer)
            intTenpuHeimenzu = value
        End Set
    End Property
#End Region

#Region "�Y�t_���ʐ}"
    ''' <summary>
    ''' �Y�t_���ʐ}
    ''' </summary>
    ''' <remarks></remarks>
    Private intTenpuRitumenzu As Integer
    ''' <summary>
    ''' �Y�t_���ʐ}
    ''' </summary>
    ''' <value></value>
    ''' <returns> �Y�t_���ʐ}</returns>
    ''' <remarks></remarks>
    <TableMap("tenpu_ritumenzu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TenpuRitumenzu() As Integer
        Get
            Return intTenpuRitumenzu
        End Get
        Set(ByVal value As Integer)
            intTenpuRitumenzu = value
        End Set
    End Property
#End Region

#Region "�Y�t_��b���}"
    ''' <summary>
    ''' �Y�t_��b���}
    ''' </summary>
    ''' <remarks></remarks>
    Private intTenpuKsHusezu As Integer
    ''' <summary>
    ''' �Y�t_��b���}
    ''' </summary>
    ''' <value></value>
    ''' <returns> �Y�t_��b���}</returns>
    ''' <remarks></remarks>
    <TableMap("tenpu_ks_husezu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TenpuKsHusezu() As Integer
        Get
            Return intTenpuKsHusezu
        End Get
        Set(ByVal value As Integer)
            intTenpuKsHusezu = value
        End Set
    End Property
#End Region

#Region "�Y�t_�f�ʐ}"
    ''' <summary>
    ''' �Y�t_�f�ʐ}
    ''' </summary>
    ''' <remarks></remarks>
    Private intTenpuDanmenzu As Integer
    ''' <summary>
    ''' �Y�t_�f�ʐ}
    ''' </summary>
    ''' <value></value>
    ''' <returns> �Y�t_�f�ʐ}</returns>
    ''' <remarks></remarks>
    <TableMap("tenpu_danmenzu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TenpuDanmenzu() As Integer
        Get
            Return intTenpuDanmenzu
        End Get
        Set(ByVal value As Integer)
            intTenpuDanmenzu = value
        End Set
    End Property
#End Region

#Region "�Y�t_��v�}"
    ''' <summary>
    ''' �Y�t_��v�}
    ''' </summary>
    ''' <remarks></remarks>
    Private intTenpuKukeizu As Integer
    ''' <summary>
    ''' �Y�t_��v�}
    ''' </summary>
    ''' <value></value>
    ''' <returns> �Y�t_��v�}</returns>
    ''' <remarks></remarks>
    <TableMap("tenpu_kukeizu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TenpuKukeizu() As Integer
        Get
            Return intTenpuKukeizu
        End Get
        Set(ByVal value As Integer)
            intTenpuKukeizu = value
        End Set
    End Property
#End Region

#Region "������������"
    ''' <summary>
    ''' ������������
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSyoudakusyoTysDate As DateTime
    ''' <summary>
    ''' ������������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������������</returns>
    ''' <remarks></remarks>
    <TableMap("syoudakusyo_tys_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property SyoudakusyoTysDate() As DateTime
        Get
            Return dateSyoudakusyoTysDate
        End Get
        Set(ByVal value As DateTime)
            dateSyoudakusyoTysDate = value
        End Set
    End Property
#End Region

#Region "�������{��"
    ''' <summary>
    ''' �������{��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysJissiDate As DateTime
    ''' <summary>
    ''' �������{��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������{��</returns>
    ''' <remarks></remarks>
    <TableMap("tys_jissi_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysJissiDate() As DateTime
        Get
            Return dateTysJissiDate
        End Get
        Set(ByVal value As DateTime)
            dateTysJissiDate = value
        End Set
    End Property
#End Region

#Region "�y��"
    ''' <summary>
    ''' �y��
    ''' </summary>
    ''' <remarks></remarks>
    Private strDositu As String
    ''' <summary>
    ''' �y��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �y��</returns>
    ''' <remarks></remarks>
    <TableMap("dositu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overrides Property Dositu() As String
        Get
            Return strDositu
        End Get
        Set(ByVal value As String)
            strDositu = value
        End Set
    End Property
#End Region

#Region "���e�x����"
    ''' <summary>
    ''' ���e�x����
    ''' </summary>
    ''' <remarks></remarks>
    Private strKyoyouSijiryoku As String
    ''' <summary>
    ''' ���e�x����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���e�x����</returns>
    ''' <remarks></remarks>
    <TableMap("kyoyou_sijiryoku", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Overrides Property KyoyouSijiryoku() As String
        Get
            Return strKyoyouSijiryoku
        End Get
        Set(ByVal value As String)
            strKyoyouSijiryoku = value
        End Set
    End Property
#End Region

#Region "����R�[�h1"
    ''' <summary>
    ''' ����R�[�h1
    ''' </summary>
    ''' <remarks></remarks>
    Private intHanteiCd1 As Integer
    ''' <summary>
    ''' ����R�[�h1
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����R�[�h1</returns>
    ''' <remarks></remarks>
    <TableMap("hantei_cd1", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HanteiCd1() As Integer
        Get
            Return intHanteiCd1
        End Get
        Set(ByVal value As Integer)
            intHanteiCd1 = value
        End Set
    End Property
#End Region

#Region "����R�[�h2"
    ''' <summary>
    ''' ����R�[�h2
    ''' </summary>
    ''' <remarks></remarks>
    Private intHanteiCd2 As Integer
    ''' <summary>
    ''' ����R�[�h2
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����R�[�h2</returns>
    ''' <remarks></remarks>
    <TableMap("hantei_cd2", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HanteiCd2() As Integer
        Get
            Return intHanteiCd2
        End Get
        Set(ByVal value As Integer)
            intHanteiCd2 = value
        End Set
    End Property
#End Region

#Region "����ڑ�����"
    ''' <summary>
    ''' ����ڑ�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intHanteiSetuzokuMoji As Integer
    ''' <summary>
    ''' ����ڑ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����ڑ�����</returns>
    ''' <remarks></remarks>
    <TableMap("hantei_setuzoku_moji", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HanteiSetuzokuMoji() As Integer
        Get
            Return intHanteiSetuzokuMoji
        End Get
        Set(ByVal value As Integer)
            intHanteiSetuzokuMoji = value
        End Set
    End Property
#End Region

#Region "�S���҃R�[�h"
    ''' <summary>
    ''' �S���҃R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private intTantousyaCd As Integer
    ''' <summary>
    ''' �S���҃R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �S���҃R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("tantousya_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TantousyaCd() As Integer
        Get
            Return intTantousyaCd
        End Get
        Set(ByVal value As Integer)
            intTantousyaCd = value
        End Set
    End Property
#End Region

#Region "�S���Җ�"
    ''' <summary>
    ''' �S���Җ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strTantousyaMei As String
    ''' <summary>
    ''' �S���Җ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �S���Җ�</returns>
    ''' <remarks></remarks>
    <TableMap("tantousya_mei")> _
    Public Overrides Property TantousyaMei() As String
        Get
            Return strTantousyaMei
        End Get
        Set(ByVal value As String)
            strTantousyaMei = value
        End Set
    End Property
#End Region

#Region "���F�҃R�[�h"
    ''' <summary>
    ''' ���F�҃R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouninsyaCd As Integer
    ''' <summary>
    ''' ���F�҃R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���F�҃R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("syouninsya_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SyouninsyaCd() As Integer
        Get
            Return intSyouninsyaCd
        End Get
        Set(ByVal value As Integer)
            intSyouninsyaCd = value
        End Set
    End Property
#End Region

#Region "���F�Җ�"
    ''' <summary>
    ''' ���F�Җ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouninsyaMei As String
    ''' <summary>
    ''' ���F�Җ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���F�Җ�</returns>
    ''' <remarks></remarks>
    <TableMap("syouninsya_mei")> _
    Public Overrides Property SyouninsyaMei() As String
        Get
            Return strSyouninsyaMei
        End Get
        Set(ByVal value As String)
            strSyouninsyaMei = value
        End Set
    End Property
#End Region

#Region "�v�揑�쐬��"
    ''' <summary>
    ''' �v�揑�쐬��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKeikakusyoSakuseiDate As DateTime
    ''' <summary>
    ''' �v�揑�쐬��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �v�揑�쐬��</returns>
    ''' <remarks></remarks>
    <TableMap("keikakusyo_sakusei_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KeikakusyoSakuseiDate() As DateTime
        Get
            Return dateKeikakusyoSakuseiDate
        End Get
        Set(ByVal value As DateTime)
            dateKeikakusyoSakuseiDate = value
        End Set
    End Property
#End Region

#Region "��b�f�ʃR�[�h"
    ''' <summary>
    ''' ��b�f�ʃR�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private intKsDanmenCd As Integer
    ''' <summary>
    ''' ��b�f�ʃR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��b�f�ʃR�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("ks_danmen_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KsDanmenCd() As Integer
        Get
            Return intKsDanmenCd
        End Get
        Set(ByVal value As Integer)
            intKsDanmenCd = value
        End Set
    End Property
#End Region

#Region "�f�ʐ}����"
    ''' <summary>
    ''' �f�ʐ}����
    ''' </summary>
    ''' <remarks></remarks>
    Private strDanmenzuSetumei As String
    ''' <summary>
    ''' �f�ʐ}����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �f�ʐ}����</returns>
    ''' <remarks></remarks>
    <TableMap("danmenzu_setumei")> _
    Public Overrides Property DanmenzuSetumei() As String
        Get
            Return strDanmenzuSetumei
        End Get
        Set(ByVal value As String)
            strDanmenzuSetumei = value
        End Set
    End Property
#End Region

#Region "�l�@"
    ''' <summary>
    ''' �l�@
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousatu As String
    ''' <summary>
    ''' �l�@
    ''' </summary>
    ''' <value></value>
    ''' <returns> �l�@</returns>
    ''' <remarks></remarks>
    <TableMap("kousatu")> _
    Public Overrides Property Kousatu() As String
        Get
            Return strKousatu
        End Get
        Set(ByVal value As String)
            strKousatu = value
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
    <TableMap("ks_hkks_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    <TableMap("ks_koj_kanry_hkks_tyk_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("hosyousyo_hak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoHakDate() As DateTime
        Get
            Return dateHosyousyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakDate = value
        End Set
    End Property
#End Region

#Region "����ۏ؏����s��"
    ''' <summary>
    ''' ����ۏ؏����s��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateInsatuHosyousyoHakDate As DateTime
    ''' <summary>
    ''' ����ۏ؏����s��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����ۏ؏����s��</returns>
    ''' <remarks></remarks>
    <TableMap("insatu_hosyousyo_hak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property InsatuHosyousyoHakDate() As DateTime
        Get
            Return dateInsatuHosyousyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateInsatuHosyousyoHakDate = value
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
    <TableMap("hosyou_kaisi_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("hosyou_nasi_riyuu")> _
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
    <TableMap("hosyousyo_saihak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoSaihakDate() As DateTime
        Get
            Return dateHosyousyoSaihakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoSaihakDate = value
        End Set
    End Property
#End Region

#Region "�����񍐏��L��"
    ''' <summary>
    ''' �����񍐏��L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysHkksUmu As Integer
    ''' <summary>
    ''' �����񍐏��L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����񍐏��L��</returns>
    ''' <remarks></remarks>
    <TableMap("tys_hkks_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TysHkksUmu() As Integer
        Get
            Return intTysHkksUmu
        End Get
        Set(ByVal value As Integer)
            intTysHkksUmu = value
        End Set
    End Property
#End Region

#Region "�����񍐏��󗝏ڍ�"
    ''' <summary>
    ''' �����񍐏��󗝏ڍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysHkksJyuriSyousai As String
    ''' <summary>
    ''' �����񍐏��󗝏ڍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����񍐏��󗝏ڍ�</returns>
    ''' <remarks></remarks>
    <TableMap("tys_hkks_jyuri_syousai")> _
    Public Overrides Property TysHkksJyuriSyousai() As String
        Get
            Return strTysHkksJyuriSyousai
        End Get
        Set(ByVal value As String)
            strTysHkksJyuriSyousai = value
        End Set
    End Property
#End Region

#Region "�����񍐏��󗝓�"
    ''' <summary>
    ''' �����񍐏��󗝓�
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysHkksJyuriDate As DateTime
    ''' <summary>
    ''' �����񍐏��󗝓�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����񍐏��󗝓�</returns>
    ''' <remarks></remarks>
    <TableMap("tys_hkks_jyuri_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysHkksJyuriDate() As DateTime
        Get
            Return dateTysHkksJyuriDate
        End Get
        Set(ByVal value As DateTime)
            dateTysHkksJyuriDate = value
        End Set
    End Property
#End Region

#Region "�����񍐏�������"
    ''' <summary>
    ''' �����񍐏�������
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysHkksHakDate As DateTime
    ''' <summary>
    ''' �����񍐏�������
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����񍐏�������</returns>
    ''' <remarks></remarks>
    <TableMap("tys_hkks_hak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysHkksHakDate() As DateTime
        Get
            Return dateTysHkksHakDate
        End Get
        Set(ByVal value As DateTime)
            dateTysHkksHakDate = value
        End Set
    End Property
#End Region

#Region "�����񍐏��Ĕ��s��"
    ''' <summary>
    ''' �����񍐏��Ĕ��s��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysHkksSaihakDate As DateTime
    ''' <summary>
    ''' �����񍐏��Ĕ��s��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����񍐏��Ĕ��s��</returns>
    ''' <remarks></remarks>
    <TableMap("tys_hkks_saihak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysHkksSaihakDate() As DateTime
        Get
            Return dateTysHkksSaihakDate
        End Get
        Set(ByVal value As DateTime)
            dateTysHkksSaihakDate = value
        End Set
    End Property
#End Region

#Region "�H���񍐏��L��"
    ''' <summary>
    ''' �H���񍐏��L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojHkksUmu As Integer
    ''' <summary>
    ''' �H���񍐏��L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H���񍐏��L��</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KojHkksUmu() As Integer
        Get
            Return intKojHkksUmu
        End Get
        Set(ByVal value As Integer)
            intKojHkksUmu = value
        End Set
    End Property
#End Region

#Region "�H���񍐏��󗝏ڍ�"
    ''' <summary>
    ''' �H���񍐏��󗝏ڍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojHkksJuriSyousai As String
    ''' <summary>
    ''' �H���񍐏��󗝏ڍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H���񍐏��󗝏ڍ�</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_juri_syousai")> _
    Public Overrides Property KojHkksJuriSyousai() As String
        Get
            Return strKojHkksJuriSyousai
        End Get
        Set(ByVal value As String)
            strKojHkksJuriSyousai = value
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
    <TableMap("koj_hkks_juri_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KojHkksJuriDate() As DateTime
        Get
            Return dateKojHkksJuriDate
        End Get
        Set(ByVal value As DateTime)
            dateKojHkksJuriDate = value
        End Set
    End Property
#End Region

#Region "�H���񍐏�������"
    ''' <summary>
    ''' �H���񍐏�������
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojHkksHassouDate As DateTime
    ''' <summary>
    ''' �H���񍐏�������
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H���񍐏�������</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_hassou_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KojHkksHassouDate() As DateTime
        Get
            Return dateKojHkksHassouDate
        End Get
        Set(ByVal value As DateTime)
            dateKojHkksHassouDate = value
        End Set
    End Property
#End Region

#Region "�H���񍐏��Ĕ��s��"
    ''' <summary>
    ''' �H���񍐏��Ĕ��s��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojHkksSaihakDate As DateTime
    ''' <summary>
    ''' �H���񍐏��Ĕ��s��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H���񍐏��Ĕ��s��</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_saihak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KojHkksSaihakDate() As DateTime
        Get
            Return dateKojHkksSaihakDate
        End Get
        Set(ByVal value As DateTime)
            dateKojHkksSaihakDate = value
        End Set
    End Property
#End Region

#Region "�H����ЃR�[�h"
    ''' <summary>
    ''' �H����ЃR�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojGaisyaCd As String
    ''' <summary>
    ''' �H����ЃR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H����ЃR�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gaisya_cd")> _
    Public Overrides Property KojGaisyaCd() As String
        Get
            Return strKojGaisyaCd
        End Get
        Set(ByVal value As String)
            strKojGaisyaCd = value
        End Set
    End Property
#End Region

#Region "�H����Ў��Ə��R�[�h"
    ''' <summary>
    ''' �H����Ў��Ə��R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojGaisyaJigyousyoCd As String
    ''' <summary>
    ''' �H����Ў��Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H����Ў��Ə��R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gaisya_jigyousyo_cd")> _
    Public Overrides Property KojGaisyaJigyousyoCd() As String
        Get
            Return strKojGaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strKojGaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "���ǍH�����"
    ''' <summary>
    ''' ���ǍH�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intKairyKojSyubetu As Integer
    ''' <summary>
    ''' ���ǍH�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���ǍH�����</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_syubetu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KairyKojSyubetu() As Integer
        Get
            Return intKairyKojSyubetu
        End Get
        Set(ByVal value As Integer)
            intKairyKojSyubetu = value
        End Set
    End Property
#End Region

#Region "���ǍH�������\���"
    ''' <summary>
    ''' ���ǍH�������\���
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKairyKojKanryYoteiDate As DateTime
    ''' <summary>
    ''' ���ǍH�������\���
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���ǍH�������\���</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_kanry_yotei_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KairyKojKanryYoteiDate() As DateTime
        Get
            Return dateKairyKojKanryYoteiDate
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojKanryYoteiDate = value
        End Set
    End Property
#End Region

#Region "���ǍH����"
    ''' <summary>
    ''' ���ǍH����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKairyKojDate As DateTime
    ''' <summary>
    ''' ���ǍH����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���ǍH����</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KairyKojDate() As DateTime
        Get
            Return dateKairyKojDate
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojDate = value
        End Set
    End Property
#End Region

#Region "���ǍH�����H���񒅓�"
    ''' <summary>
    ''' ���ǍH�����H���񒅓�
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKairyKojSokuhouTykDate As DateTime
    ''' <summary>
    ''' ���ǍH�����H���񒅓�
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���ǍH�����H���񒅓�</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_sokuhou_tyk_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KairyKojSokuhouTykDate() As DateTime
        Get
            Return dateKairyKojSokuhouTykDate
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojSokuhouTykDate = value
        End Set
    End Property
#End Region

#Region "�ǉ��H����ЃR�[�h"
    ''' <summary>
    ''' �ǉ��H����ЃR�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTKojKaisyaCd As String
    ''' <summary>
    ''' �ǉ��H����ЃR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ǉ��H����ЃR�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_kaisya_cd")> _
    Public Overrides Property TKojKaisyaCd() As String
        Get
            Return strTKojKaisyaCd
        End Get
        Set(ByVal value As String)
            strTKojKaisyaCd = value
        End Set
    End Property
#End Region

#Region "�ǉ��H����Ў��Ə��R�[�h"
    ''' <summary>
    ''' �ǉ��H����Ў��Ə��R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTKojKaisyaJigyousyoCd As String
    ''' <summary>
    ''' �ǉ��H����Ў��Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ǉ��H����Ў��Ə��R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_kaisya_jigyousyo_cd")> _
    Public Overrides Property TKojKaisyaJigyousyoCd() As String
        Get
            Return strTKojKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTKojKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "�ǉ��H�����"
    ''' <summary>
    ''' �ǉ��H�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intTKojSyubetu As Integer
    ''' <summary>
    ''' �ǉ��H�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ǉ��H�����</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_syubetu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TKojSyubetu() As Integer
        Get
            Return intTKojSyubetu
        End Get
        Set(ByVal value As Integer)
            intTKojSyubetu = value
        End Set
    End Property
#End Region

#Region "�ǉ��H�������\���"
    ''' <summary>
    ''' �ǉ��H�������\���
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTKojKanryYoteiDate As DateTime
    ''' <summary>
    ''' �ǉ��H�������\���
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ǉ��H�������\���</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_kanry_yotei_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TKojKanryYoteiDate() As DateTime
        Get
            Return dateTKojKanryYoteiDate
        End Get
        Set(ByVal value As DateTime)
            dateTKojKanryYoteiDate = value
        End Set
    End Property
#End Region

#Region "�ǉ��H����"
    ''' <summary>
    ''' �ǉ��H����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTKojDate As DateTime
    ''' <summary>
    ''' �ǉ��H����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ǉ��H����</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TKojDate() As DateTime
        Get
            Return dateTKojDate
        End Get
        Set(ByVal value As DateTime)
            dateTKojDate = value
        End Set
    End Property
#End Region

#Region "�ǉ��H�����H���񒅓�"
    ''' <summary>
    ''' �ǉ��H�����H���񒅓�
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTKojSokuhouTykDate As DateTime
    ''' <summary>
    ''' �ǉ��H�����H���񒅓�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ǉ��H�����H���񒅓�</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_sokuhou_tyk_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TKojSokuhouTykDate() As DateTime
        Get
            Return dateTKojSokuhouTykDate
        End Get
        Set(ByVal value As DateTime)
            dateTKojSokuhouTykDate = value
        End Set
    End Property
#End Region

#Region "�ǉ��H����А����L��"
    ''' <summary>
    ''' �ǉ��H����А����L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intTKojKaisyaSeikyuuUmu As Integer
    ''' <summary>
    ''' �ǉ��H����А����L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ǉ��H����А����L��</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_kaisya_seikyuu_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TKojKaisyaSeikyuuUmu() As Integer
        Get
            Return intTKojKaisyaSeikyuuUmu
        End Get
        Set(ByVal value As Integer)
            intTKojKaisyaSeikyuuUmu = value
        End Set
    End Property
#End Region

#Region "�������ʓo�^����"
    ''' <summary>
    ''' �������ʓo�^����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysKekkaAddDatetime As DateTime
    ''' <summary>
    ''' �������ʓo�^����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������ʓo�^����</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kekka_add_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysKekkaAddDatetime() As DateTime
        Get
            Return dateTysKekkaAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateTysKekkaAddDatetime = value
        End Set
    End Property
#End Region

#Region "�������ʍX�V����"
    ''' <summary>
    ''' �������ʍX�V����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysKekkaUpdDatetime As DateTime
    ''' <summary>
    ''' �������ʍX�V����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������ʍX�V����</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kekka_upd_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysKekkaUpdDatetime() As DateTime
        Get
            Return dateTysKekkaUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateTysKekkaUpdDatetime = value
        End Set
    End Property
#End Region

#Region "�����˗�����"
    ''' <summary>
    ''' �����˗�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intDoujiIraiTousuu As Integer
    ''' <summary>
    ''' �����˗�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����˗�����</returns>
    ''' <remarks></remarks>
    <TableMap("douji_irai_tousuu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property DoujiIraiTousuu() As Integer
        Get
            Return intDoujiIraiTousuu
        End Get
        Set(ByVal value As Integer)
            intDoujiIraiTousuu = value
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

#Region "���r�L��"
    ''' <summary>
    ''' ���r�L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intKasiUmu As Integer
    ''' <summary>
    ''' ���r�L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���r�L��</returns>
    ''' <remarks></remarks>
    <TableMap("kasi_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KasiUmu() As Integer
        Get
            Return intKasiUmu
        End Get
        Set(ByVal value As Integer)
            intKasiUmu = value
        End Set
    End Property
#End Region

#Region "�H����А����L��"
    ''' <summary>
    ''' �H����А����L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojGaisyaSeikyuuUmu As Integer
    ''' <summary>
    ''' �H����А����L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H����А����L��</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gaisya_seikyuu_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KojGaisyaSeikyuuUmu() As Integer
        Get
            Return intKojGaisyaSeikyuuUmu
        End Get
        Set(ByVal value As Integer)
            intKojGaisyaSeikyuuUmu = value
        End Set
    End Property
#End Region

#Region "�ԋ�����FLG"
    ''' <summary>
    ''' �ԋ�����FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intHenkinSyoriFlg As Integer
    ''' <summary>
    ''' �ԋ�����FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ԋ�����FLG</returns>
    ''' <remarks></remarks>
    <TableMap("henkin_syori_flg", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HenkinSyoriFlg() As Integer
        Get
            Return intHenkinSyoriFlg
        End Get
        Set(ByVal value As Integer)
            intHenkinSyoriFlg = value
        End Set
    End Property
#End Region

#Region "�ԋ�������"
    ''' <summary>
    ''' �ԋ�������
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHenkinSyoriDate As DateTime
    ''' <summary>
    ''' �ԋ�������
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ԋ�������</returns>
    ''' <remarks></remarks>
    <TableMap("henkin_syori_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HenkinSyoriDate() As DateTime
        Get
            Return dateHenkinSyoriDate
        End Get
        Set(ByVal value As DateTime)
            dateHenkinSyoriDate = value
        End Set
    End Property
#End Region

#Region "�H���S����"
    ''' <summary>
    ''' �H���S����
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojTantousyaMei As String
    ''' <summary>
    ''' �H���S����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H���S����</returns>
    ''' <remarks></remarks>
    <TableMap("koj_tantousya_mei")> _
    Public Overrides Property KojTantousyaMei() As String
        Get
            Return strKojTantousyaMei
        End Get
        Set(ByVal value As String)
            strKojTantousyaMei = value
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
    <TableMap("keiyu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Keiyu() As Integer
        Get
            Return intKeiyu
        End Get
        Set(ByVal value As Integer)
            intKeiyu = value
        End Set
    End Property
#End Region

#Region "�H���d�l�m�F"
    ''' <summary>
    ''' �H���d�l�m�F
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojSiyouKakunin As Integer
    ''' <summary>
    ''' �H���d�l�m�F
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H���d�l�m�F</returns>
    ''' <remarks></remarks>
    <TableMap("koj_siyou_kakunin", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KojSiyouKakunin() As Integer
        Get
            Return intKojSiyouKakunin
        End Get
        Set(ByVal value As Integer)
            intKojSiyouKakunin = value
        End Set
    End Property
#End Region

#Region "�H���d�l�m�F��"
    ''' <summary>
    ''' �H���d�l�m�F��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojSiyouKakuninDate As DateTime
    ''' <summary>
    ''' �H���d�l�m�F��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H���d�l�m�F��</returns>
    ''' <remarks></remarks>
    <TableMap("koj_siyou_kakunin_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KojSiyouKakuninDate() As DateTime
        Get
            Return dateKojSiyouKakuninDate
        End Get
        Set(ByVal value As DateTime)
            dateKojSiyouKakuninDate = value
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
    <TableMap("hosyousyo_hak_iraisyo_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    <TableMap("hosyousyo_hak_iraisyo_tyk_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoHakIraisyoTykDate() As DateTime
        Get
            Return dateHosyousyoHakIraisyoTykDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakIraisyoTykDate = value
        End Set
    End Property
#End Region

#Region "�݌v���e�x����"
    ''' <summary>
    ''' �݌v���e�x����
    ''' </summary>
    ''' <remarks></remarks>
    Private decSekkeiKyoyouSijiryoku As Decimal
    ''' <summary>
    ''' �݌v���e�x����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �݌v���e�x����</returns>
    ''' <remarks></remarks>
    <TableMap("sekkei_kyoyou_sijiryoku", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overrides Property SekkeiKyoyouSijiryoku() As Decimal
        Get
            Return decSekkeiKyoyouSijiryoku
        End Get
        Set(ByVal value As Decimal)
            decSekkeiKyoyouSijiryoku = value
        End Set
    End Property
#End Region

#Region "�˗��\�蓏��"
    ''' <summary>
    ''' �˗��\�蓏��
    ''' </summary>
    ''' <remarks></remarks>
    Private intIraiYoteiTousuu As Integer
    ''' <summary>
    ''' �˗��\�蓏��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �˗��\�蓏��</returns>
    ''' <remarks></remarks>
    <TableMap("irai_yotei_tousuu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property IraiYoteiTousuu() As Integer
        Get
            Return intIraiYoteiTousuu
        End Get
        Set(ByVal value As Integer)
            intIraiYoteiTousuu = value
        End Set
    End Property
#End Region

#Region "�����p�rNO"
    ''' <summary>
    ''' �����p�rNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTatemonoYoutoNo As Integer
    ''' <summary>
    ''' �����p�rNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����p�rNO</returns>
    ''' <remarks></remarks>
    <TableMap("tatemono_youto_no", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TatemonoYoutoNo() As Integer
        Get
            Return intTatemonoYoutoNo
        End Get
        Set(ByVal value As Integer)
            intTatemonoYoutoNo = value
        End Set
    End Property
#End Region

#Region "�ː�"
    ''' <summary>
    ''' �ː�
    ''' </summary>
    ''' <remarks></remarks>
    Private intKosuu As Integer
    ''' <summary>
    ''' �ː�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ː�</returns>
    ''' <remarks></remarks>
    <TableMap("kosuu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Kosuu() As Integer
        Get
            Return intKosuu
        End Get
        Set(ByVal value As Integer)
            intKosuu = value
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

#Region "�����A����_����"
    ''' <summary>
    ''' �����A����_����
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiAtesakiMei As String
    ''' <summary>
    ''' �����A����_����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����A����_����</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_atesaki_mei", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=60)> _
    Public Overrides Property TysRenrakusakiAtesakiMei() As String
        Get
            Return strTysRenrakusakiAtesakiMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiAtesakiMei = value
        End Set
    End Property
#End Region

#Region "�����A����_TEL"
    ''' <summary>
    ''' �����A����_TEL
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTel As String
    ''' <summary>
    ''' �����A����_TEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����A����_TEL</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_tel", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TysRenrakusakiTel() As String
        Get
            Return strTysRenrakusakiTel
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTel = value
        End Set
    End Property
#End Region

#Region "�����A����_FAX"
    ''' <summary>
    ''' �����A����_FAX
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiFax As String
    ''' <summary>
    ''' �����A����_FAX
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����A����_FAX</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_fax", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TysRenrakusakiFax() As String
        Get
            Return strTysRenrakusakiFax
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiFax = value
        End Set
    End Property
#End Region

#Region "�����A����_MAIL"
    ''' <summary>
    ''' �����A����_MAIL
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiMail As String
    ''' <summary>
    ''' �����A����_MAIL
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����A����_MAIL</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_mail", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=64)> _
    Public Overrides Property TysRenrakusakiMail() As String
        Get
            Return strTysRenrakusakiMail
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiMail = value
        End Set
    End Property
#End Region

#Region "�����A����_�S����"
    ''' <summary>
    ''' �����A����_�S����
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTantouMei As String
    ''' <summary>
    ''' �����A����_�S����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����A����_�S����</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_tantou_mei", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TysRenrakusakiTantouMei() As String
        Get
            Return strTysRenrakusakiTantouMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTantouMei = value
        End Set
    End Property
#End Region

#Region "�o�^۸޲�հ�ްID"
    ''' <summary>
    ''' �o�^۸޲�հ�ްID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String
    ''' <summary>
    ''' �o�^۸޲�հ�ްID
    ''' </summary>
    ''' <value></value>
    ''' <returns> �o�^۸޲�հ�ްID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property AddLoginUserId() As String
        Get
            Return strAddLoginUserId
        End Get
        Set(ByVal value As String)
            strAddLoginUserId = value
        End Set
    End Property
#End Region

#Region "�o�^����"
    ''' <summary>
    ''' �o�^����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetime As DateTime
    ''' <summary>
    ''' �o�^����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �o�^����</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property AddDatetime() As DateTime
        Get
            Return dateAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetime = value
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

#Region "���i�R�[�h�P�̓@�ʐ������R�[�h"
    ''' <summary>
    ''' ���i�R�[�h�P�̓@�ʐ������R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private objSyouhin1Record As TeibetuSeikyuuRecord
    ''' <summary>
    ''' ���i�R�[�h�P�̓@�ʐ������R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���i�R�[�h�P�̓@�ʐ������R�[�h</returns>
    ''' <remarks></remarks>
    Public Overrides Property Syouhin1Record() As TeibetuSeikyuuRecord
        Get
            Return objSyouhin1Record
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objSyouhin1Record = value
        End Set
    End Property
#End Region

#Region "���i�R�[�h�Q�̓@�ʐ������R�[�hDictionary"
    ''' <summary>
    ''' ���i�R�[�h�Q�̓@�ʐ������R�[�hDictionary
    ''' </summary>
    ''' <remarks>TeibetuSeikyuuRecord��Dictionary�ł��鎖</remarks>
    Private htbSyouhin2Records As Dictionary(Of Integer, TeibetuSeikyuuRecord)
    ''' <summary>
    ''' ���i�R�[�h�Q�̓@�ʐ���Dictionary
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���i�R�[�h�Q�̓@�ʐ������R�[�hDictionary</returns>
    ''' <remarks>��ʕ\��NO��Key�Ƃ���TeibetuSeikyuuRecord�̃��X�g�ł��鎖</remarks>
    Public Overrides Property Syouhin2Records() As Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Get
            Return htbSyouhin2Records
        End Get
        Set(ByVal value As Dictionary(Of Integer, TeibetuSeikyuuRecord))
            htbSyouhin2Records = value
        End Set
    End Property
#End Region

#Region "���i�R�[�h�R�̓@�ʐ������R�[�hDictionary"
    ''' <summary>
    ''' ���i�R�[�h�R�̓@�ʐ������R�[�hDictionary
    ''' </summary>
    ''' <remarks>TeibetuSeikyuuRecord��Dictionary�ł��鎖</remarks>
    Private htbSyouhin3Records As Dictionary(Of Integer, TeibetuSeikyuuRecord)
    ''' <summary>
    ''' ���i�R�[�h�R�̓@�ʐ������R�[�hDictionary
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��ʕ\��NO��Key�Ƃ������i�R�[�h�R�̓@�ʐ������R�[�h���X�g</returns>
    ''' <remarks>TeibetuSeikyuuRecord��Dictionary�ł��鎖</remarks>
    Public Overrides Property Syouhin3Records() As Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Get
            Return htbSyouhin3Records
        End Get
        Set(ByVal value As Dictionary(Of Integer, TeibetuSeikyuuRecord))
            htbSyouhin3Records = value
        End Set
    End Property
#End Region

#Region "���i�R�[�h�S�̓@�ʐ������R�[�hDictionary"
    ''' <summary>
    ''' ���i�R�[�h�S�̓@�ʐ������R�[�hDictionary
    ''' </summary>
    ''' <remarks>TeibetuSeikyuuRecord��Dictionary�ł��鎖</remarks>
    Private htbSyouhin4Records As Dictionary(Of Integer, TeibetuSeikyuuRecord)
    ''' <summary>
    ''' ���i�R�[�h�S�̓@�ʐ������R�[�hDictionary
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��ʕ\��NO��Key�Ƃ������i�R�[�h�S�̓@�ʐ������R�[�h���X�g</returns>
    ''' <remarks>TeibetuSeikyuuRecord��Dictionary�ł��鎖</remarks>
    Public Overrides Property Syouhin4Records() As Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Get
            Return htbSyouhin4Records
        End Get
        Set(ByVal value As Dictionary(Of Integer, TeibetuSeikyuuRecord))
            htbSyouhin4Records = value
        End Set
    End Property
#End Region

#Region "�ǉ��H���̓@�ʐ������R�[�h"
    ''' <summary>
    ''' �ǉ��H���̓@�ʐ������R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private objTuikaKoujiRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' �ǉ��H���̓@�ʐ������R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ǉ��H���̓@�ʐ������R�[�h</returns>
    ''' <remarks></remarks>
    Public Overrides Property TuikaKoujiRecord() As TeibetuSeikyuuRecord
        Get
            Return objTuikaKoujiRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objTuikaKoujiRecord = value
        End Set
    End Property
#End Region

#Region "���ǍH���̓@�ʐ������R�[�h"
    ''' <summary>
    ''' ���ǍH���̓@�ʐ������R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private objKairyouKoujiRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' ���ǍH���̓@�ʐ������R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���ǍH���̓@�ʐ������R�[�h</returns>
    ''' <remarks></remarks>
    Public Overrides Property KairyouKoujiRecord() As TeibetuSeikyuuRecord
        Get
            Return objKairyouKoujiRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objKairyouKoujiRecord = value
        End Set
    End Property
#End Region

#Region "�����񍐏��̓@�ʐ������R�[�h(150:�����񍐏��Ĕ��s�萔��)"
    ''' <summary>
    ''' �����񍐏��̓@�ʐ������R�[�h(150:�����񍐏��Ĕ��s�萔��)
    ''' </summary>
    ''' <remarks></remarks>
    Private objTyousaHoukokusyoRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' �����񍐏��̓@�ʐ������R�[�h(150:�����񍐏��Ĕ��s�萔��)
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����񍐏��̓@�ʐ������R�[�h(150:�����񍐏��Ĕ��s�萔��)</returns>
    ''' <remarks></remarks>
    Public Overrides Property TyousaHoukokusyoRecord() As TeibetuSeikyuuRecord
        Get
            Return objTyousaHoukokusyoRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objTyousaHoukokusyoRecord = value
        End Set
    End Property
#End Region

#Region "�H���񍐏��̓@�ʐ������R�[�h(160:�H���񍐏��Ĕ��s�萔��)"
    ''' <summary>
    ''' �H���񍐏��̓@�ʐ������R�[�h(160:�H���񍐏��Ĕ��s�萔��)
    ''' </summary>
    ''' <remarks></remarks>
    Private objKoujiHoukokusyoRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' �H���񍐏��̓@�ʐ������R�[�h(160:�H���񍐏��Ĕ��s�萔��)
    ''' </summary>
    ''' <value></value>
    ''' <returns>�H���񍐏��̓@�ʐ������R�[�h(160:�H���񍐏��Ĕ��s�萔��)</returns>
    ''' <remarks></remarks>
    Public Overrides Property KoujiHoukokusyoRecord() As TeibetuSeikyuuRecord
        Get
            Return objKoujiHoukokusyoRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objKoujiHoukokusyoRecord = value
        End Set
    End Property
#End Region

#Region "�ۏ؏��̓@�ʐ������R�[�h(170:�ۏ؏��Ĕ��s�萔��)"
    ''' <summary>
    ''' �ۏ؏��̓@�ʐ������R�[�h(170:�ۏ؏��Ĕ��s�萔��)
    ''' </summary>
    ''' <remarks></remarks>
    Private objHosyousyoRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' �ۏ؏��̓@�ʐ������R�[�h(170:�ۏ؏��Ĕ��s�萔��)
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ۏ؏��̓@�ʐ������R�[�h(170:�ۏ؏��Ĕ��s�萔��)</returns>
    ''' <remarks></remarks>
    Public Overrides Property HosyousyoRecord() As TeibetuSeikyuuRecord
        Get
            Return objHosyousyoRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objHosyousyoRecord = value
        End Set
    End Property
#End Region

#Region "��񕥖߂̓@�ʐ������R�[�h(180:��񕥖�)"
    ''' <summary>
    ''' ��񕥖߂̓@�ʐ������R�[�h(180:��񕥖�)
    ''' </summary>
    ''' <remarks></remarks>
    Private objKaiyakuHaraimodosiRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' ��񕥖߂̓@�ʐ������R�[�h(180:��񕥖�)
    ''' </summary>
    ''' <value></value>
    ''' <returns>��񕥖߂̓@�ʐ������R�[�h(180:��񕥖�)</returns>
    ''' <remarks></remarks>
    Public Overrides Property KaiyakuHaraimodosiRecord() As TeibetuSeikyuuRecord
        Get
            Return objKaiyakuHaraimodosiRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objKaiyakuHaraimodosiRecord = value
        End Set
    End Property
#End Region

#Region "��L�ȊO�̓@�ʐ������R�[�h���X�g"
    ''' <summary>
    ''' ��L�ȊO�̓@�ʐ������R�[�h���X�g
    ''' </summary>
    ''' <remarks>TeibetuSeikyuuRecord�̃��X�g�ł��鎖</remarks>
    Private arrOtherTeibetuSeikyuuRecords As List(Of TeibetuSeikyuuRecord)
    ''' <summary>
    ''' ��L�ȊO�̓@�ʐ������R�[�h���X�g
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��L�ȊO�̓@�ʐ������R�[�h���X�g</returns>
    ''' <remarks>TeibetuSeikyuuRecord�̃��X�g�ł��鎖</remarks>
    Public Overrides Property OtherTeibetuSeikyuuRecords() As List(Of TeibetuSeikyuuRecord)
        Get
            Return arrOtherTeibetuSeikyuuRecords
        End Get
        Set(ByVal value As List(Of TeibetuSeikyuuRecord))
            arrOtherTeibetuSeikyuuRecords = value
        End Set
    End Property
#End Region

#Region "�@�ʓ������R�[�hDictionary"
    ''' <summary>
    ''' �@�ʓ������R�[�hDictionary
    ''' </summary>
    ''' <remarks></remarks>
    Private htbTeibetuNyuukinRecords As Dictionary(Of String, TeibetuNyuukinRecord)
    ''' <summary>
    ''' �@�ʓ������R�[�hDictionary
    ''' </summary>
    ''' <value></value>
    ''' <returns> �@�ʓ������R�[�hDictionary</returns>
    ''' <remarks>���ރR�[�h��Key��TeibetuNyuukinRecord��ێ����܂�<br/>
    ''' ���ޒP�ʂɓ����z��ێ����Ă܂��B<br/>
    ''' ��ʐݒ�ɂ͈ȉ����g�p���܂�<br/>
    ''' 100:���i�P�C�Q����<br/>
    ''' 120:���i�R<br/>
    ''' 130:�ǉ��H��<br/>
    ''' 140:���ǍH��<br/>
    ''' 150:�����񍐏�<br/>
    ''' 160:�H���񍐏�<br/>
    ''' 170:�ۏ؏�<br/>
    ''' 180:��񕥖�</remarks>
    Public Overrides Property TeibetuNyuukinRecords() As Dictionary(Of String, TeibetuNyuukinRecord)
        Get
            Return htbTeibetuNyuukinRecords
        End Get
        Set(ByVal value As Dictionary(Of String, TeibetuNyuukinRecord))
            htbTeibetuNyuukinRecords = value
        End Set
    End Property
#End Region

#Region "�@�ʓ������R�[�hList"
    ''' <summary>
    ''' �@�ʓ������R�[�hList
    ''' </summary>
    ''' <remarks></remarks>
    Private listTeibetuNyuukinUpdateRecords As List(Of TeibetuNyuukinUpdateRecord)

    ''' <summary>
    ''' �@�ʓ������R�[�hList
    ''' </summary>
    ''' <value>�@�ʓ������R�[�hList</value>
    ''' <returns>�@�ʓ������R�[�hList</returns>
    ''' <remarks></remarks>
    Public Overrides Property TeibetuNyuukinLists() As List(Of TeibetuNyuukinUpdateRecord)
        Get
            Return listTeibetuNyuukinUpdateRecords
        End Get
        Set(ByVal value As List(Of TeibetuNyuukinUpdateRecord))
            listTeibetuNyuukinUpdateRecords = value
        End Set
    End Property
#End Region

#Region "ReportIf �A�g�p����"

#Region "ReportIF �ݒ�p �\����"
    Private strKouzouMeiIf As String = ""
    ''' <summary>
    ''' ReportIF �ݒ�p �\����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF �ݒ�p �\����</returns>
    ''' <remarks></remarks>
    Public Overrides Property KouzouMeiIf() As String
        Get
            Return strKouzouMeiIf
        End Get
        Set(ByVal value As String)
            strKouzouMeiIf = value
        End Set
    End Property
#End Region

#Region "ReportIF �ݒ�p �������@��"
    ''' <summary>
    ''' ReportIF �ݒ�p �������@��
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysHouhouMeiIf As String = ""
    ''' <summary>
    ''' ReportIF �ݒ�p �������@��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF �ݒ�p �������@��</returns>
    ''' <remarks></remarks>
    Public Overrides Property TysHouhouMeiIf() As String
        Get
            Return strTysHouhouMeiIf
        End Get
        Set(ByVal value As String)
            strTysHouhouMeiIf = value
        End Set
    End Property
#End Region

#Region "ReportIF �ݒ�p �����X��"
    ''' <summary>
    ''' ReportIF �ݒ�p �����X��
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMeiIf As String = ""
    ''' <summary>
    ''' ReportIF �ݒ�p �����X��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF �ݒ�p �����X��</returns>
    ''' <remarks></remarks>
    Public Overrides Property KameitenMeiIf() As String
        Get
            Return strKameitenMeiIf
        End Get
        Set(ByVal value As String)
            strKameitenMeiIf = value
        End Set
    End Property
#End Region

#Region "ReportIF �ݒ�p �����XTEL"
    ''' <summary>
    ''' ReportIF �ݒ�p �����XTEL
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenTelIf As String = ""
    ''' <summary>
    ''' ReportIF �ݒ�p �����XTEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF �ݒ�p �����XTEL</returns>
    ''' <remarks></remarks>
    Public Overrides Property KameitenTelIf() As String
        Get
            Return strKameitenTelIf
        End Get
        Set(ByVal value As String)
            strKameitenTelIf = value
        End Set
    End Property
#End Region

#Region "ReportIF �ݒ�p �����XFAX"
    ''' <summary>
    ''' ReportIF �ݒ�p �����XFAX
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenFaxIf As String = ""
    ''' <summary>
    ''' ReportIF �ݒ�p �����XFAX
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF �ݒ�p �����XFAX</returns>
    ''' <remarks></remarks>
    Public Overrides Property KameitenFaxIf() As String
        Get
            Return strKameitenFaxIf
        End Get
        Set(ByVal value As String)
            strKameitenFaxIf = value
        End Set
    End Property
#End Region

#Region "ReportIF �ݒ�p �����XMAIL"
    ''' <summary>
    ''' ReportIF �ݒ�p �����XMAIL
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMailIf As String = ""
    ''' <summary>
    ''' ReportIF �ݒ�p �����XMAIL
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF �ݒ�p �����XMAIL</returns>
    ''' <remarks></remarks>
    Public Overrides Property KameitenMailIf() As String
        Get
            Return strKameitenMailIf
        End Get
        Set(ByVal value As String)
            strKameitenMailIf = value
        End Set
    End Property
#End Region

#Region "ReportIF �ݒ�p �����񍐏����F��"
    ''' <summary>
    ''' ReportIF �ݒ�p �����񍐏����F��
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyousaHoukokusyoSyouninsyaIf As String = ""
    ''' <summary>
    ''' ReportIF �ݒ�p �����񍐏����F��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF �ݒ�p �����񍐏����F��</returns>
    ''' <remarks></remarks>
    <TableMap("t_houkoku_syounin", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TyousaHoukokusyoSyouninsyaIf() As String
        Get
            Return strTyousaHoukokusyoSyouninsyaIf
        End Get
        Set(ByVal value As String)
            strTyousaHoukokusyoSyouninsyaIf = value
        End Set
    End Property
#End Region

#Region "ReportIF �ݒ�p �i���X�e�[�^�X"
    ''' <summary>
    ''' ReportIF �ݒ�p �i���X�e�[�^�X
    ''' </summary>
    ''' <remarks></remarks>
    Private strStatusIf As String = ""
    ''' <summary>
    ''' ReportIF �ݒ�p �i���X�e�[�^�X
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF �ݒ�p �i���X�e�[�^�X</returns>
    ''' <remarks></remarks>
    <TableMap("status", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
    Public Overrides Property StatusIf() As String
        Get
            Return strStatusIf
        End Get
        Set(ByVal value As String)
            strStatusIf = value
        End Set
    End Property
#End Region

#Region "������Ж�"
    ''' <summary>
    ''' ������Ж��
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaMeiIf As String = ""
    ''' <summary>
    ''' ������Ж�
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������Ж�</returns>
    ''' <remarks></remarks>
    Public Overrides Property TysKaisyaMeiIf() As String
        Get
            Return strTysKaisyaMeiIf
        End Get
        Set(ByVal value As String)
            strTysKaisyaMeiIf = value
        End Set
    End Property
#End Region

#End Region

#Region "�u���H�� �ʐ^��/�ʐ^�R�����g"

    ''' <summary>
    ''' �ʐ^��
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyasinJuri As String
    ''' <summary>
    ''' �ʐ^��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ʐ^��</returns>
    ''' <remarks></remarks>
    <TableMap("syasin_jyuri", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overrides Property TikanKoujiSyasinJuri() As String
        Get
            Return strSyasinJuri
        End Get
        Set(ByVal value As String)
            strSyasinJuri = value
        End Set
    End Property

    ''' <summary>
    ''' �ʐ^�R�����g
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyasinComment As String
    ''' <summary>
    ''' �ʐ^�R�����g
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ʐ^�R�����g</returns>
    ''' <remarks></remarks>
    <TableMap("syasin_comment", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=100)> _
    Public Overrides Property TikanKoujiSyasinComment() As String
        Get
            Return strSyasinComment
        End Get
        Set(ByVal value As String)
            strSyasinComment = value
        End Set
    End Property

#End Region

#Region "�A�������Ή�"

#Region "��������"
    ''' <summary>
    ''' ���������
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyoriKensuu As Integer = 0
    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��������</returns>
    ''' <remarks></remarks>
    Public Overrides Property SyoriKensuu() As Integer
        Get
            Return intSyoriKensuu
        End Get
        Set(ByVal value As Integer)
            intSyoriKensuu = value
        End Set
    End Property
#End Region

#Region "�A��������"
    ''' <summary>
    ''' �A���������
    ''' </summary>
    ''' <remarks></remarks>
    Private intRentouBukkenSuu As Integer = 1
    ''' <summary>
    ''' �A��������
    ''' </summary>
    ''' <value></value>
    ''' <returns> �A��������</returns>
    ''' <remarks></remarks>
    Public Overrides Property RentouBukkenSuu() As Integer
        Get
            Return intRentouBukkenSuu
        End Get
        Set(ByVal value As Integer)
            intRentouBukkenSuu = value
        End Set
    End Property
#End Region

#End Region

#Region "�\���FLG"
    ''' <summary>
    ''' �\���FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intYoyakuZumiFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' �\���FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>�\���FLG</returns>
    ''' <remarks></remarks>
    <TableMap("yoyaku_zumi_flg", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YoyakuZumiFlg() As Integer
        Get
            Return intYoyakuZumiFlg
        End Get
        Set(ByVal value As Integer)
            intYoyakuZumiFlg = value
        End Set
    End Property
#End Region

#Region "�ē��}"
    ''' <summary>
    ''' �ē��}
    ''' </summary>
    ''' <remarks></remarks>
    Private intAnnaiZu As Integer = Integer.MinValue
    ''' <summary>
    ''' �ē��}
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ē��}</returns>
    ''' <remarks></remarks>
    <TableMap("annaizu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property AnnaiZu() As Integer
        Get
            Return intAnnaiZu
        End Get
        Set(ByVal value As Integer)
            intAnnaiZu = value
        End Set
    End Property
#End Region

#Region "�z�u�}"
    ''' <summary>
    ''' �z�u�}
    ''' </summary>
    ''' <remarks></remarks>
    Private intHaitiZu As Integer = Integer.MinValue
    ''' <summary>
    ''' �z�u�}
    ''' </summary>
    ''' <value></value>
    ''' <returns>�z�u�}</returns>
    ''' <remarks></remarks>
    <TableMap("haitizu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HaitiZu() As Integer
        Get
            Return intHaitiZu
        End Get
        Set(ByVal value As Integer)
            intHaitiZu = value
        End Set
    End Property
#End Region

#Region "�e�K���ʐ}"
    ''' <summary>
    ''' �e�K���ʐ}
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakukaiHeimenZu As Integer = Integer.MinValue
    ''' <summary>
    ''' �e�K���ʐ}
    ''' </summary>
    ''' <value></value>
    ''' <returns>�e�K���ʐ}</returns>
    ''' <remarks></remarks>
    <TableMap("kakukai_heimenzu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KakukaiHeimenZu() As Integer
        Get
            Return intKakukaiHeimenZu
        End Get
        Set(ByVal value As Integer)
            intKakukaiHeimenZu = value
        End Set
    End Property
#End Region

#Region "��b���}"
    ''' <summary>
    ''' ��b���}
    ''' </summary>
    ''' <remarks></remarks>
    Private intKsHuseZu As Integer = Integer.MinValue
    ''' <summary>
    ''' ��b���}
    ''' </summary>
    ''' <value></value>
    ''' <returns>��b���}</returns>
    ''' <remarks></remarks>
    <TableMap("ks_husezu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KsHuseZu() As Integer
        Get
            Return intKsHuseZu
        End Get
        Set(ByVal value As Integer)
            intKsHuseZu = value
        End Set
    End Property
#End Region

#Region "��b�f�ʐ}"
    ''' <summary>
    ''' ��b�f�ʐ}
    ''' </summary>
    ''' <remarks></remarks>
    Private intKsDanmenZu As Integer = Integer.MinValue
    ''' <summary>
    ''' ��b�f�ʐ}
    ''' </summary>
    ''' <value></value>
    ''' <returns>��b�f�ʐ}</returns>
    ''' <remarks></remarks>
    <TableMap("ks_danmenzu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KsDanmenZu() As Integer
        Get
            Return intKsDanmenZu
        End Get
        Set(ByVal value As Integer)
            intKsDanmenZu = value
        End Set
    End Property
#End Region

#Region "�����v��}"
    ''' <summary>
    ''' �����v��}
    ''' </summary>
    ''' <remarks></remarks>
    Private intZouseiKeikakuZu As Integer = Integer.MinValue
    ''' <summary>
    ''' �����v��}
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����v��}</returns>
    ''' <remarks></remarks>
    <TableMap("zousei_keikakuzu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property ZouseiKeikakuZu() As Integer
        Get
            Return intZouseiKeikakuZu
        End Get
        Set(ByVal value As Integer)
            intZouseiKeikakuZu = value
        End Set
    End Property
#End Region

#Region "��b���H�\���FROM"
    ''' <summary>
    ''' ��b���H�\���FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKsTyakkouYoteiFromDate As DateTime
    ''' <summary>
    ''' ��b���H�\���FROM
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��b���H�\���FROM</returns>
    ''' <remarks></remarks>
    <TableMap("ks_tyakkou_yotei_from_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KsTyakkouYoteiFromDate() As DateTime
        Get
            Return dateKsTyakkouYoteiFromDate
        End Get
        Set(ByVal value As DateTime)
            dateKsTyakkouYoteiFromDate = value
        End Set
    End Property
#End Region

#Region "��b���H�\���TO"
    ''' <summary>
    ''' ��b���H�\���TO
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKsTyakkouYoteiToDate As DateTime
    ''' <summary>
    ''' ��b���H�\���TO
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��b���H�\���TO</returns>
    ''' <remarks></remarks>
    <TableMap("ks_tyakkou_yotei_to_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KsTyakkouYoteiToDate() As DateTime
        Get
            Return dateKsTyakkouYoteiToDate
        End Get
        Set(ByVal value As DateTime)
            dateKsTyakkouYoteiToDate = value
        End Set
    End Property
#End Region

#Region "�V�K�o�^���敪"
    ''' <summary>
    ''' �V�K�o�^���敪
    ''' </summary>
    ''' <remarks></remarks>
    Private intSinkiTourokuMotoKbn As Integer = Integer.MinValue
    ''' <summary>
    ''' �V�K�o�^���敪
    ''' </summary>
    ''' <value></value>
    ''' <returns>�V�K�o�^���敪</returns>
    ''' <remarks></remarks>
    <TableMap("sinki_touroku_moto_kbn", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SinkiTourokuMotoKbn() As Integer
        Get
            Return intSinkiTourokuMotoKbn
        End Get
        Set(ByVal value As Integer)
            intSinkiTourokuMotoKbn = value
        End Set
    End Property
#End Region

#Region "�H�����茋��FLG"
    ''' <summary>
    ''' �H�����茋��FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojHanteiKekkaFlg As Integer = 0
    ''' <summary>
    ''' �H�����茋��FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>�H�����茋��FLG</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hantei_kekka_flg", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KojHanteiKekkaFlg() As Integer
        Get
            Return intKojHanteiKekkaFlg
        End Get
        Set(ByVal value As Integer)
            intKojHanteiKekkaFlg = value
        End Set
    End Property
#End Region

#Region "�{�喼�L��"
    ''' <summary>
    ''' �{�喼�L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intSesyuMeiUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' �{�喼�L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �{�喼�L��</returns>
    ''' <remarks></remarks>
    <TableMap("sesyu_mei_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SesyuMeiUmu() As Integer
        Get
            Return intSesyuMeiUmu
        End Get
        Set(ByVal value As Integer)
            intSesyuMeiUmu = value
        End Set
    End Property
#End Region

End Class