''' <summary>
''' �����f�[�^���R�[�h�N���X/�������ꗗ��ʁA�ߋ��������ꗗ���
''' </summary>
''' <remarks>�����f�[�^�̊i�[���Ɏg�p���܂�</remarks>
<TableClassMap("t_seikyuu_kagami")> _
Public Class SeikyuuDataRecord

#Region "������NO"
    ''' <summary>
    ''' ������NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusyoNo As String
    ''' <summary>
    ''' ������NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_no", IsKey:=True, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=15)> _
    Public Overridable Property SeikyuusyoNo() As String
        Get
            Return strSeikyuusyoNo
        End Get
        Set(ByVal value As String)
            strSeikyuusyoNo = value
        End Set
    End Property
#End Region

#Region "���"
    ''' <summary>
    ''' ���
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer
    ''' <summary>
    ''' ���
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "������R�[�h"
    ''' <summary>
    ''' ������R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String
    ''' <summary>
    ''' ������R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overridable Property SeikyuuSakiCd() As String
        Get
            Return strSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "������}��"
    ''' <summary>
    ''' ������}��
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrc As String
    ''' <summary>
    ''' ������}��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������}��</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_brc", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property SeikyuuSakiBrc() As String
        Get
            Return strSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "������敪"
    ''' <summary>
    ''' ������敪
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String
    ''' <summary>
    ''' ������敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������敪</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Overridable Property SeikyuuSakiKbn() As String
        Get
            Return strSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "�����於"
    ''' <summary>
    ''' �����於
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMei As String
    ''' <summary>
    ''' �����於
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����於</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_mei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overridable Property SeikyuuSakiMei() As String
        Get
            Return strSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei = value
        End Set
    End Property
#End Region

#Region "�����於2"
    ''' <summary>
    ''' �����於2
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMei2 As String
    ''' <summary>
    ''' �����於2
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_mei2", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overridable Property SeikyuuSakiMei2() As String
        Get
            Return strSeikyuuSakiMei2
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei2 = value
        End Set
    End Property
#End Region

#Region "�X�֔ԍ�"
    ''' <summary>
    ''' �X�֔ԍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strYuubinNo As String
    ''' <summary>
    ''' �X�֔ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�֔ԍ�</returns>
    ''' <remarks></remarks>
    <TableMap("yuubin_no", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Overridable Property YuubinNo() As String
        Get
            Return strYuubinNo
        End Get
        Set(ByVal value As String)
            strYuubinNo = value
        End Set
    End Property
#End Region

#Region "�Z��1"
    ''' <summary>
    ''' �Z��1
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyuusyo1 As String
    ''' <summary>
    ''' �Z��1
    ''' </summary>
    ''' <value></value>
    ''' <returns> �Z��1</returns>
    ''' <remarks></remarks>
    <TableMap("jyuusyo1", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overridable Property Jyuusyo1() As String
        Get
            Return strJyuusyo1
        End Get
        Set(ByVal value As String)
            strJyuusyo1 = value
        End Set
    End Property
#End Region

#Region "�Z��2"
    ''' <summary>
    ''' �Z��2
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyuusyo2 As String
    ''' <summary>
    ''' �Z��2
    ''' </summary>
    ''' <value></value>
    ''' <returns> �Z��2</returns>
    ''' <remarks></remarks>
    <TableMap("jyuusyo2", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overridable Property Jyuusyo2() As String
        Get
            Return strJyuusyo2
        End Get
        Set(ByVal value As String)
            strJyuusyo2 = value
        End Set
    End Property
#End Region

#Region "�d�b�ԍ�"
    ''' <summary>
    ''' �d�b�ԍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strTelNo As String
    ''' <summary>
    ''' �d�b�ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �d�b�ԍ�</returns>
    ''' <remarks></remarks>
    <TableMap("tel_no", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=16)> _
    Public Overridable Property TelNo() As String
        Get
            Return strTelNo
        End Get
        Set(ByVal value As String)
            strTelNo = value
        End Set
    End Property
#End Region

#Region "�O��䐿���z"
    ''' <summary>
    ''' �O��䐿���z
    ''' </summary>
    ''' <remarks></remarks>
    Private intZenkaiGoseikyuuGaku As Integer
    ''' <summary>
    ''' �O��䐿���z
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("zenkai_goseikyuu_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property ZenkaiGoseikyuuGaku() As Integer
        Get
            Return intZenkaiGoseikyuuGaku
        End Get
        Set(ByVal value As Integer)
            intZenkaiGoseikyuuGaku = value
        End Set
    End Property
#End Region

#Region "������z"
    ''' <summary>
    ''' ������z
    ''' </summary>
    ''' <remarks></remarks>
    Private intGonyuukinGaku As Integer
    ''' <summary>
    ''' ������z
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("gonyuukin_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property GonyuukinGaku() As Integer
        Get
            Return intGonyuukinGaku
        End Get
        Set(ByVal value As Integer)
            intGonyuukinGaku = value
        End Set
    End Property
#End Region

#Region "���E�z"
    ''' <summary>
    ''' ���E�z
    ''' </summary>
    ''' <remarks></remarks>
    Private intSousaiGaku As Integer
    ''' <summary>
    ''' ���E�z
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("sousai_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SousaiGaku() As Integer
        Get
            Return intSousaiGaku
        End Get
        Set(ByVal value As Integer)
            intSousaiGaku = value
        End Set
    End Property
#End Region

#Region "�����z"
    ''' <summary>
    ''' �����z
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyouseiGaku As Integer
    ''' <summary>
    ''' �����z
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("tyousei_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TyouseiGaku() As Integer
        Get
            Return intTyouseiGaku
        End Get
        Set(ByVal value As Integer)
            intTyouseiGaku = value
        End Set
    End Property
#End Region

#Region "�J�z���z"
    ''' <summary>
    ''' �J�z���z
    ''' </summary>
    ''' <remarks></remarks>
    Private intKurikosiGaku As Integer
    ''' <summary>
    ''' �J�z���z
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kurikosi_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KurikosiGaku() As Integer
        Get
            Return intKurikosiGaku
        End Get
        Set(ByVal value As Integer)
            intKurikosiGaku = value
        End Set
    End Property
#End Region

#Region "����䐿�����z"
    ''' <summary>
    ''' ����䐿�����z
    ''' </summary>
    ''' <remarks></remarks>
    Private intKonkaiGoseikyuuGaku As Integer
    ''' <summary>
    ''' ����䐿�����z
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("konkai_goseikyuu_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KonkaiGoseikyuuGaku() As Integer
        Get
            Return intKonkaiGoseikyuuGaku
        End Get
        Set(ByVal value As Integer)
            intKonkaiGoseikyuuGaku = value
        End Set
    End Property
#End Region

#Region "����J�z���z"
    ''' <summary>
    ''' ����J�z���z
    ''' </summary>
    ''' <remarks></remarks>
    Private intKonkaiKurikosiGaku As Integer
    ''' <summary>
    ''' ����J�z���z
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("konkai_kurikosi_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KonkaiKurikosiGaku() As Integer
        Get
            Return intKonkaiKurikosiGaku
        End Get
        Set(ByVal value As Integer)
            intKonkaiKurikosiGaku = value
        End Set
    End Property
#End Region

#Region "�������\���"
    ''' <summary>
    ''' �������\���
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKonkaiKaisyuuYoteiDate As DateTime
    ''' <summary>
    ''' �������\���
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("konkai_kaisyuu_yotei_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property KonkaiKaisyuuYoteiDate() As DateTime
        Get
            Return dateKonkaiKaisyuuYoteiDate
        End Get
        Set(ByVal value As DateTime)
            dateKonkaiKaisyuuYoteiDate = value
        End Set
    End Property
#End Region

#Region "�����������"
    ''' <summary>
    ''' �����������
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuusyoInsatuDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' �����������
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_insatu_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property SeikyuusyoInsatuDate() As DateTime
        Get
            Return dateSeikyuusyoInsatuDate
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuusyoInsatuDate = value
        End Set
    End Property
#End Region

#Region "���������s��"
    ''' <summary>
    ''' ���������s��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuusyoHakDate As DateTime
    ''' <summary>
    ''' ���������s��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���������s��</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_hak_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property SeikyuusyoHakDate() As DateTime
        Get
            Return dateSeikyuusyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuusyoHakDate = value
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
    <TableMap("tantousya_mei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overridable Property TantousyaMei() As String
        Get
            Return strTantousyaMei
        End Get
        Set(ByVal value As String)
            strTantousyaMei = value
        End Set
    End Property
#End Region

#Region "�������󎚕������t���O"
    ''' <summary>
    ''' �������󎚕������t���O
    ''' </summary>
    ''' <remarks></remarks>
    Private intSeikyuusyoInjiBukkenMeiFlg As Integer
    ''' <summary>
    ''' �������󎚕������t���O
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_inji_bukken_mei_flg", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SeikyuusyoInjiBukkenMeiFlg() As Integer
        Get
            Return intSeikyuusyoInjiBukkenMeiFlg
        End Get
        Set(ByVal value As Integer)
            intSeikyuusyoInjiBukkenMeiFlg = value
        End Set
    End Property
#End Region

#Region "���������ԍ�"
    ''' <summary>
    ''' ���������ԍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strNyuukinKouzaNo As String
    ''' <summary>
    ''' ���������ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_kouza_no", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property NyuukinKouzaNo() As String
        Get
            Return strNyuukinKouzaNo
        End Get
        Set(ByVal value As String)
            strNyuukinKouzaNo = value
        End Set
    End Property
#End Region

#Region "�������ߓ�"
    ''' <summary>
    ''' �������ߓ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSimeDate As String
    ''' <summary>
    ''' �������ߓ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������ߓ�</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_sime_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property SeikyuuSimeDate() As String
        Get
            Return strSeikyuuSimeDate
        End Get
        Set(ByVal value As String)
            strSeikyuuSimeDate = value
        End Set
    End Property
#End Region

#Region "����������ߓ�"
    ''' <summary>
    ''' ����������ߓ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strSenpouSeikyuuSimeDate As String
    ''' <summary>
    ''' ����������ߓ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("senpou_seikyuu_sime_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property SenpouSeikyuuSimeDate() As String
        Get
            Return strSenpouSeikyuuSimeDate
        End Get
        Set(ByVal value As String)
            strSenpouSeikyuuSimeDate = value
        End Set
    End Property
#End Region

#Region "���E�t���O"
    ''' <summary>
    ''' ���E�t���O
    ''' </summary>
    ''' <remarks></remarks>
    Private intSousaiFlg As Integer
    ''' <summary>
    ''' ���E�t���O
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("sousai_flg", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SousaiFlg() As Integer
        Get
            Return intSousaiFlg
        End Get
        Set(ByVal value As Integer)
            intSousaiFlg = value
        End Set
    End Property
#End Region

#Region "����\�茎��"
    ''' <summary>
    ''' ����\�茎��
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisyuuYoteiGessuu As Integer
    ''' <summary>
    ''' ����\�茎��
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_yotei_gessuu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KaisyuuYoteiGessuu() As Integer
        Get
            Return intKaisyuuYoteiGessuu
        End Get
        Set(ByVal value As Integer)
            intKaisyuuYoteiGessuu = value
        End Set
    End Property
#End Region

#Region "����\���"
    ''' <summary>
    ''' ����\���
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuYoteiDate As String
    ''' <summary>
    ''' ����\���
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_yotei_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property KaisyuuYoteiDate() As String
        Get
            Return strKaisyuuYoteiDate
        End Get
        Set(ByVal value As String)
            strKaisyuuYoteiDate = value
        End Set
    End Property
#End Region

#Region "�������K����"
    ''' <summary>
    ''' �������K����
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusyoHittykDate As String
    ''' <summary>
    ''' �������K����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������K����</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_hittyk_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property SeikyuusyoHittykDate() As String
        Get
            Return strSeikyuusyoHittykDate
        End Get
        Set(ByVal value As String)
            strSeikyuusyoHittykDate = value
        End Set
    End Property
#End Region

#Region "���1"
    ''' <summary>
    ''' ���1
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuSyubetu1 As String
    ''' <summary>
    ''' ���1
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_syubetu1", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property KaisyuuSyubetu1() As String
        Get
            Return strKaisyuuSyubetu1
        End Get
        Set(ByVal value As String)
            strKaisyuuSyubetu1 = value
        End Set
    End Property
#End Region

#Region "����1"
    ''' <summary>
    ''' ����1
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisyuuWariai1 As Integer
    ''' <summary>
    ''' ����1
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_wariai1", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KaisyuuWariai1() As Integer
        Get
            Return intKaisyuuWariai1
        End Get
        Set(ByVal value As Integer)
            intKaisyuuWariai1 = value
        End Set
    End Property
#End Region

#Region "��`�T�C�g����"
    ''' <summary>
    ''' ��`�T�C�g����
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisyuuTegataSiteGessuu As Integer
    ''' <summary>
    ''' ��`�T�C�g����
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_tegata_site_gessuu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KaisyuuTegataSiteGessuu() As Integer
        Get
            Return intKaisyuuTegataSiteGessuu
        End Get
        Set(ByVal value As Integer)
            intKaisyuuTegataSiteGessuu = value
        End Set
    End Property
#End Region

#Region "��`�T�C�g��"
    ''' <summary>
    ''' ��`�T�C�g��
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuTegataSiteDate As String
    ''' <summary>
    ''' ��`�T�C�g��
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_tegata_site_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property KaisyuuTegataSiteDate() As String
        Get
            Return strKaisyuuTegataSiteDate
        End Get
        Set(ByVal value As String)
            strKaisyuuTegataSiteDate = value
        End Set
    End Property
#End Region

#Region "�������p��"
    ''' <summary>
    ''' �������p��
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuSeikyuusyoYousi As String
    ''' <summary>
    ''' �������p��
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_seikyuusyo_yousi", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property KaisyuuSeikyuusyoYousi() As String
        Get
            Return strKaisyuuSeikyuusyoYousi
        End Get
        Set(ByVal value As String)
            strKaisyuuSeikyuusyoYousi = value
        End Set
    End Property
#End Region

#Region "�������p���ėp�R�[�h"
    ''' <summary>
    ''' �������p���ėp�R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuSeikyuusyoYousiHannyouCd As String
    ''' <summary>
    ''' �������p���ėp�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_seikyuusyo_yousi_hannyou_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overridable Property KaisyuuSeikyuusyoYousiHannyouCd() As String
        Get
            Return strKaisyuuSeikyuusyoYousiHannyouCd
        End Get
        Set(ByVal value As String)
            strKaisyuuSeikyuusyoYousiHannyouCd = value
        End Set
    End Property
#End Region

#Region "���2"
    ''' <summary>
    ''' ���2
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuSyubetu2 As String
    ''' <summary>
    ''' ���2
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_syubetu2", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property KaisyuuSyubetu2() As String
        Get
            Return strKaisyuuSyubetu2
        End Get
        Set(ByVal value As String)
            strKaisyuuSyubetu2 = value
        End Set
    End Property
#End Region

#Region "����2"
    ''' <summary>
    ''' ����2
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisyuuWariai2 As Integer
    ''' <summary>
    ''' ����2
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_wariai2", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KaisyuuWariai2() As Integer
        Get
            Return intKaisyuuWariai2
        End Get
        Set(ByVal value As Integer)
            intKaisyuuWariai2 = value
        End Set
    End Property
#End Region

#Region "���3"
    ''' <summary>
    ''' ���3
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuSyubetu3 As String
    ''' <summary>
    ''' ���3
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_syubetu3", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property KaisyuuSyubetu3() As String
        Get
            Return strKaisyuuSyubetu3
        End Get
        Set(ByVal value As String)
            strKaisyuuSyubetu3 = value
        End Set
    End Property
#End Region

#Region "����3"
    ''' <summary>
    ''' ����3
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisyuuWariai3 As Integer
    ''' <summary>
    ''' ����3
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_wariai3", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KaisyuuWariai3() As Integer
        Get
            Return intKaisyuuWariai3
        End Get
        Set(ByVal value As Integer)
            intKaisyuuWariai3 = value
        End Set
    End Property
#End Region

#Region "�o�^���O�C�����[�U�[ID"
    ''' <summary>
    ''' �o�^���O�C�����[�U�[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String
    ''' <summary>
    ''' �o�^���O�C�����[�U�[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> �o�^���O�C�����[�U�[ID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overridable Property AddLoginUserId() As String
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
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property AddDatetime() As DateTime
        Get
            Return dateAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetime = value
        End Set
    End Property
#End Region

#Region "�X�V���O�C�����[�U�[ID"
    ''' <summary>
    ''' �X�V���O�C�����[�U�[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' �X�V���O�C�����[�U�[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V���O�C�����[�U�[ID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overridable Property UpdLoginUserId() As String
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
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "������M.�������ߓ�"
    ''' <summary>
    ''' ������M.�������ߓ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSimeDateMst As String
    ''' <summary>
    ''' ������M.�������ߓ�
    ''' </summary>
    ''' <remarks></remarks>
    <TableMap("mst_seikyuu_sime_date")> _
    Public Overridable Property SeikyuuSimeDateMst() As String
        Get
            Return strSeikyuuSimeDateMst
        End Get
        Set(ByVal value As String)
            strSeikyuuSimeDateMst = value
        End Set
    End Property
#End Region

#Region "VIEW �����於"
    ''' <summary>
    ''' VIEW �����於
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMeiView As String
    ''' <summary>
    ''' VIEW �����於
    ''' </summary>
    ''' <value></value>
    ''' <returns> VIEW �����於</returns>
    ''' <remarks></remarks>
    <TableMap("view_seikyuu_saki_mei")> _
    Public Property SeikyuuSakiMeiView() As String
        Get
            Return strSeikyuuSakiMeiView
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMeiView = value
        End Set
    End Property
#End Region

#Region "������T.���������s���Ɣ���f�[�^T.�����N�����Ƃ̍��كt���O"
    ''' <summary>
    ''' ������T.���������s���Ɣ���f�[�^T.�����N�����Ƃ̍��كt���O
    ''' </summary>
    ''' <remarks></remarks>
    Private intSeikyuuDateSaiFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' ������T.���������s���Ɣ���f�[�^T.�����N�����Ƃ̍��كt���O
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������T.���������s���Ɣ���f�[�^T.�����N�����̂����ꂩ���قȂ�ꍇ�A1��Ԃ�
    ''' ���󎚑Ώۃt���O=1</returns>
    ''' <remarks>1:���ق���,0:���قȂ�</remarks>
    <TableMap("seikyuu_date_sai_flg")> _
    Public Property SeikyuuDateSaiFlg() As Integer
        Get
            Return intSeikyuuDateSaiFlg
        End Get
        Set(ByVal value As Integer)
            intSeikyuuDateSaiFlg = value
        End Set
    End Property
#End Region

#Region "����ΏۊO�t���O"
    ''' <summary>
    ''' ����ΏۊO�t���O(�������p���ėp�R�[�h��'9�`'���܂܂�Ă���ꍇ1���Z�b�g����B�ȊO��NULL�̏ꍇ��0)
    ''' </summary>
    ''' <remarks></remarks>
    Private intPrintTaigyougaiFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' ����ΏۊO�t���O
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("print_taigyougai_flg")> _
    Public Overridable Property PrintTaisyougaiFlg() As Integer
        Get
            Return intPrintTaigyougaiFlg
        End Get
        Set(ByVal value As Integer)
            intPrintTaigyougaiFlg = value
        End Set
    End Property
#End Region

#Region "���׌���"
    ''' <summary>
    ''' ���׌���
    ''' </summary>
    ''' <remarks></remarks>
    Private intMeisaiKensuu As Integer = 0
    ''' <summary>
    ''' ���׌���
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���׌���</returns>
    ''' <remarks></remarks>
    <TableMap("meisai_kensuu")> _
    Public Property MeisaiKensuu() As Integer
        Get
            Return intMeisaiKensuu
        End Get
        Set(ByVal value As Integer)
            intMeisaiKensuu = value
        End Set
    End Property
#End Region

#Region "�g������M.�������p������"
    ''' <summary>
    ''' �������p������
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisyuuSeikyuusyoYousiMei As String
    ''' <summary>
    ''' �������p������
    ''' </summary>
    ''' <remarks></remarks>
    <TableMap("mst_meisyou")> _
    Public Overridable Property KaisyuuSeikyuusyoYousiMei() As String
        Get
            Return strKaisyuuSeikyuusyoYousiMei
        End Get
        Set(ByVal value As String)
            strKaisyuuSeikyuusyoYousiMei = value
        End Set
    End Property
#End Region

End Class
