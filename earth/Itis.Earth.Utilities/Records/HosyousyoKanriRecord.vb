''' <summary>
''' �ۏ؏��Ǘ��e�[�u���̃��R�[�h�N���X�ł�
''' </summary>
''' <remarks></remarks>
<TableClassMap("t_hosyousyo_kanri")> _
Public Class HosyousyoKanriRecord

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
    <TableMap("kbn", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
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
    <TableMap("hosyousyo_no", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
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
    <TableMap("bukken_jyky", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property BukkenJyky() As Integer
        Get
            Return intBukkenJyky
        End Get
        Set(ByVal value As Integer)
            intBukkenJyky = value
        End Set
    End Property
#End Region

#Region "��͊���"
    ''' <summary>
    ''' ��͊���
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisekiKanry As Integer = Integer.MinValue
    ''' <summary>
    ''' ��͊���
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��͊���</returns>
    ''' <remarks></remarks>
    <TableMap("kaiseki_kanry", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property KaisekiKanry() As Integer
        Get
            Return intKaisekiKanry
        End Get
        Set(ByVal value As Integer)
            intKaisekiKanry = value
        End Set
    End Property
#End Region

#Region "�H���L��"
    ''' <summary>
    ''' �H���L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' �H���L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H���L��</returns>
    ''' <remarks></remarks>
    <TableMap("koj_umu", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property KojUmu() As Integer
        Get
            Return intKojUmu
        End Get
        Set(ByVal value As Integer)
            intKojUmu = value
        End Set
    End Property
#End Region

#Region "�H������"
    ''' <summary>
    ''' �H������
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojKanry As Integer = Integer.MinValue
    ''' <summary>
    ''' �H������
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H������</returns>
    ''' <remarks></remarks>
    <TableMap("koj_kanry", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property KojKanry() As Integer
        Get
            Return intKojKanry
        End Get
        Set(ByVal value As Integer)
            intKojKanry = value
        End Set
    End Property
#End Region

#Region "�����m�F����"
    ''' <summary>
    ''' �����m�F����
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuukinKakuninJyouken As Integer = Integer.MinValue
    ''' <summary>
    ''' �����m�F����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����m�F����</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_kakunin_jyouken", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property NyuukinKakuninJyouken() As Integer
        Get
            Return intNyuukinKakuninJyouken
        End Get
        Set(ByVal value As Integer)
            intNyuukinKakuninJyouken = value
        End Set
    End Property
#End Region

#Region "������"
    ''' <summary>
    ''' ������
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuukinJyky As Integer = Integer.MinValue
    ''' <summary>
    ''' ������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_jyky", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property NyuukinJyky() As Integer
        Get
            Return intNyuukinJyky
        End Get
        Set(ByVal value As Integer)
            intNyuukinJyky = value
        End Set
    End Property
#End Region

#Region "���r"
    ''' <summary>
    ''' ���r
    ''' </summary>
    ''' <remarks></remarks>
    Private intKasi As Integer = Integer.MinValue
    ''' <summary>
    ''' ���r
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���r</returns>
    ''' <remarks></remarks>
    <TableMap("kasi", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property Kasi() As Integer
        Get
            Return intKasi
        End Get
        Set(ByVal value As Integer)
            intKasi = value
        End Set
    End Property
#End Region

#Region "�ی����"
    ''' <summary>
    ''' �ی����
    ''' </summary>
    ''' <remarks></remarks>
    Private intHokenKaisya As Integer = Integer.MinValue
    ''' <summary>
    ''' �ی����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ی����</returns>
    ''' <remarks></remarks>
    <TableMap("hoken_kaisya", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HokenKaisya() As Integer
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
    <TableMap("hoken_sinsei_tuki", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HokenSinseiTuki() As DateTime
        Get
            Return dateHokenSinseiTuki
        End Get
        Set(ByVal value As DateTime)
            dateHokenSinseiTuki = value
        End Set
    End Property
#End Region

#Region "�ی��\���敪"
    ''' <summary>
    ''' �ی��\���敪
    ''' </summary>
    ''' <remarks></remarks>
    Private intHokenSinseiKbn As Integer = Integer.MinValue
    ''' <summary>
    ''' �ی��\���敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ی��\���敪</returns>
    ''' <remarks></remarks>
    <TableMap("hoken_sinsei_kbn", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HokenSinseiKbn() As Integer
        Get
            Return intHokenSinseiKbn
        End Get
        Set(ByVal value As Integer)
            intHokenSinseiKbn = value
        End Set
    End Property
#End Region

#Region "���n���O�ی�"
    ''' <summary>
    ''' ���n���O�ی�
    ''' </summary>
    ''' <remarks></remarks>
    Private intHwMaeHkn As Integer = Integer.MinValue
    ''' <summary>
    ''' ���n���O�ی�
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���n���O�ی�</returns>
    ''' <remarks></remarks>
    <TableMap("hw_mae_hkn", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HwMaeHkn() As Integer
        Get
            Return intHwMaeHkn
        End Get
        Set(ByVal value As Integer)
            intHwMaeHkn = value
        End Set
    End Property
#End Region

#Region "���n���O�ی��N����"
    ''' <summary>
    ''' ���n���O�ی��N����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHwMaeHknDate As DateTime
    ''' <summary>
    ''' ���n���O�ی��N����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���n���O�ی��N����</returns>
    ''' <remarks></remarks>
    <TableMap("hw_mae_hkn_date", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HwMaeHknDate() As DateTime
        Get
            Return dateHwMaeHknDate
        End Get
        Set(ByVal value As DateTime)
            dateHwMaeHknDate = value
        End Set
    End Property
#End Region

#Region "���n���O�ی����{��"
    ''' <summary>
    ''' ���n���O�ی����{��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHwMaeHknJissiDate As DateTime
    ''' <summary>
    ''' ���n���O�ی����{��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���n���O�ی����{��</returns>
    ''' <remarks></remarks>
    <TableMap("hw_mae_hkn_jissi_date", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HwMaeHknJissiDate() As DateTime
        Get
            Return dateHwMaeHknJissiDate
        End Get
        Set(ByVal value As DateTime)
            dateHwMaeHknJissiDate = value
        End Set
    End Property
#End Region

#Region "���n���O�ی��K�p�\����{��"
    ''' <summary>
    ''' ���n���O�ی��K�p�\����{��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHwMaeHknTekiyouYoteiJissiDate As DateTime
    ''' <summary>
    ''' ���n���O�ی��K�p�\����{��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���n���O�ی��K�p�\����{��</returns>
    ''' <remarks></remarks>
    <TableMap("hw_mae_hkn_tekiyou_yotei_jissi_date", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HwMaeHknTekiyouYoteiJissiDate() As DateTime
        Get
            Return dateHwMaeHknTekiyouYoteiJissiDate
        End Get
        Set(ByVal value As DateTime)
            dateHwMaeHknTekiyouYoteiJissiDate = value
        End Set
    End Property
#End Region

#Region "���n����ی�"
    ''' <summary>
    ''' ���n����ی�
    ''' </summary>
    ''' <remarks></remarks>
    Private intHwAtoHkn As Integer = Integer.MinValue
    ''' <summary>
    ''' ���n����ی�
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���n����ی�</returns>
    ''' <remarks></remarks>
    <TableMap("hw_ato_hkn", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HwAtoHkn() As Integer
        Get
            Return intHwAtoHkn
        End Get
        Set(ByVal value As Integer)
            intHwAtoHkn = value
        End Set
    End Property
#End Region

#Region "���n����ی��N����"
    ''' <summary>
    ''' ���n����ی��N����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHwAtoHknDate As DateTime
    ''' <summary>
    ''' ���n����ی��N����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���n����ی��N����</returns>
    ''' <remarks></remarks>
    <TableMap("hw_ato_hkn_date", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HwAtoHknDate() As DateTime
        Get
            Return dateHwAtoHknDate
        End Get
        Set(ByVal value As DateTime)
            dateHwAtoHknDate = value
        End Set
    End Property
#End Region

#Region "���n����ی����{��"
    ''' <summary>
    ''' ���n����ی����{��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHwAtoHknJissiDate As DateTime
    ''' <summary>
    ''' ���n����ی����{��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���n����ی����{��</returns>
    ''' <remarks></remarks>
    <TableMap("hw_ato_hkn_jissi_date", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HwAtoHknJissiDate() As DateTime
        Get
            Return dateHwAtoHknJissiDate
        End Get
        Set(ByVal value As DateTime)
            dateHwAtoHknJissiDate = value
        End Set
    End Property
#End Region

#Region "���n����ی��K�p�\����{��"
    ''' <summary>
    ''' ���n����ی��K�p�\����{��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHwAtoHknTekiyouYoteiJissiDate As DateTime
    ''' <summary>
    ''' ���n����ی��K�p�\����{��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���n����ی��K�p�\����{��</returns>
    ''' <remarks></remarks>
    <TableMap("hw_ato_hkn_tekiyou_yotei_jissi_date", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HwAtoHknTekiyouYoteiJissiDate() As DateTime
        Get
            Return dateHwAtoHknTekiyouYoteiJissiDate
        End Get
        Set(ByVal value As DateTime)
            dateHwAtoHknTekiyouYoteiJissiDate = value
        End Set
    End Property
#End Region

#Region "���n����ی�������"
    ''' <summary>
    ''' ���n����ی�������
    ''' </summary>
    ''' <remarks></remarks>
    Private intHwAtoHknTorikesiSyubetsu As Integer = Integer.MinValue
    ''' <summary>
    ''' ���n����ی�������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���n����ی�������</returns>
    ''' <remarks></remarks>
    <TableMap("hw_ato_hkn_torikesi_syubetsu", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HwAtoHknTorikesiSyubetsu() As Integer
        Get
            Return intHwAtoHknTorikesiSyubetsu
        End Get
        Set(ByVal value As Integer)
            intHwAtoHknTorikesiSyubetsu = value
        End Set
    End Property
#End Region

#Region "�����t���O"
    ''' <summary>
    ''' �����t���O
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyoriFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' �����t���O
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����t���O</returns>
    ''' <remarks></remarks>
    <TableMap("syori_flg", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SyoriFlg() As Integer
        Get
            Return intSyoriFlg
        End Get
        Set(ByVal value As Integer)
            intSyoriFlg = value
        End Set
    End Property
#End Region

#Region "��������"
    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSyoriDateTime As DateTime
    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��������</returns>
    ''' <remarks></remarks>
    <TableMap("syori_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property SyoriDateTime() As DateTime
        Get
            Return dateSyoriDateTime
        End Get
        Set(ByVal value As DateTime)
            dateSyoriDateTime = value
        End Set
    End Property
#End Region

#Region "�ۏ؏��^�C�v"
    ''' <summary>
    ''' �ۏ؏��^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoType As Integer = Integer.MinValue
    ''' <summary>
    ''' �ۏ؏��^�C�v
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏��^�C�v</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_type", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HosyousyoType() As Integer
        Get
            Return intHosyousyoType
        End Get
        Set(ByVal value As Integer)
            intHosyousyoType = value
        End Set
    End Property
#End Region

#Region "�ۏ؊���"
    ''' <summary>
    ''' �ۏ؊���
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouKikan As Integer = Integer.MinValue
    ''' <summary>
    ''' �ۏ؊���
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ۏ؊���</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kikan", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property HosyouKikan() As Integer
        Get
            Return intHosyouKikan
        End Get
        Set(ByVal value As Integer)
            intHosyouKikan = value
        End Set
    End Property
#End Region

#Region "�Ɩ�������"
    ''' <summary>
    ''' �Ɩ�������
    ''' </summary>
    ''' <remarks></remarks>
    Private dateGyoumuKanryDate As DateTime
    ''' <summary>
    ''' �Ɩ�������
    ''' </summary>
    ''' <value></value>
    ''' <returns> �Ɩ�������</returns>
    ''' <remarks></remarks>
    <TableMap("gyoumu_kanry_date", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property GyoumuKanryDate() As DateTime
        Get
            Return dateGyoumuKanryDate
        End Get
        Set(ByVal value As DateTime)
            dateGyoumuKanryDate = value
        End Set
    End Property
#End Region

#Region "�ۏ؊J�n�Ɩ����e"
    ''' <summary>
    ''' �ۏ؊J�n�Ɩ����e
    ''' </summary>
    ''' <remarks></remarks>
    Private strGyoumuKaisiNaiyou As String
    ''' <summary>
    ''' �ۏ؊J�n�Ɩ����e
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؊J�n�Ɩ����e</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kaisi_gyoumu_naiyou", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=18)> _
    Public Property GyoumuKaisiNaiyou() As String
        Get
            Return strGyoumuKaisiNaiyou
        End Get
        Set(ByVal value As String)
            strGyoumuKaisiNaiyou = value
        End Set
    End Property
#End Region


#Region "�o�^���O�C�����[�UID"
    ''' <summary>
    ''' �o�^���O�C�����[�UID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String
    ''' <summary>
    ''' �o�^���O�C�����[�UID
    ''' </summary>
    ''' <value></value>
    ''' <returns> �o�^���O�C�����[�UID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Property AddLoginUserId() As String
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
    Private dateAddDateTime As DateTime
    ''' <summary>
    ''' �o�^����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �o�^����</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property AddDateTime() As DateTime
        Get
            Return dateAddDateTime
        End Get
        Set(ByVal value As DateTime)
            dateAddDateTime = value
        End Set
    End Property
#End Region

#Region "�X�V���O�C�����[�UID"
    ''' <summary>
    ''' �X�V���O�C�����[�UID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' �X�V���O�C�����[�UID
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V���O�C�����[�UID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Property UpdLoginUserId() As String
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
    Private dateUpdDateTime As DateTime
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V����</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property UpdDateTime() As DateTime
        Get
            Return dateUpdDateTime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDateTime = value
        End Set
    End Property
#End Region

End Class