''' <summary>
''' ReportIf�擾�f�[�^�ݒ�p�̃��R�[�h�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class ReportIfGetRecord

#Region "�ڋq�ԍ�"
    ''' <summary>
    ''' �ڋq�ԍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strKokyakuNo As String
    ''' <summary>
    ''' �ڋq�ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ڋq�ԍ�</returns>
    ''' <remarks></remarks>
    <TableMap("kokyaku_no", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=20)> _
    Public Overridable Property KokyakuNo() As String
        Get
            Return strKokyakuNo
        End Get
        Set(ByVal value As String)
            strKokyakuNo = value
        End Set
    End Property
#End Region

#Region "��͊������e"
    ''' <summary>
    ''' ��͊������e
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisekiKanryNaiyou As String = String.Empty
    ''' <summary>
    ''' ��͊������e
    ''' </summary>
    ''' <value></value>
    ''' <returns>��͊������e</returns>
    ''' <remarks></remarks>
    <TableMap("kaiseki_kanry_naiyou", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=100)> _
    Public Overridable Property KaisekiKanryNaiyou() As String
        Get
            Return strKaisekiKanryNaiyou
        End Get
        Set(ByVal value As String)
            strKaisekiKanryNaiyou = value
        End Set
    End Property
#End Region

#Region "�H���������f"
    ''' <summary>
    ''' �H���������f
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojKanryHandan As String = String.Empty
    ''' <summary>
    ''' �H���������f
    ''' </summary>
    ''' <value></value>
    ''' <returns>�H���������f</returns>
    ''' <remarks></remarks>
    <TableMap("koj_kanry_handan", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property KojKanryHandan() As String
        Get
            Return strKojKanryHandan
        End Get
        Set(ByVal value As String)
            strKojKanryHandan = value
        End Set
    End Property
#End Region

#Region "�����󋵔��f"
    ''' <summary>
    ''' �����󋵔��f
    ''' </summary>
    ''' <remarks></remarks>
    Private strNyuukinJykyHandan As String = String.Empty
    ''' <summary>
    ''' �����󋵔��f
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����󋵔��f</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_jyky_handan", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property NyuukinJykyHandan() As String
        Get
            Return strNyuukinJykyHandan
        End Get
        Set(ByVal value As String)
            strNyuukinJykyHandan = value
        End Set
    End Property
#End Region

#Region "�H���������e"
    ''' <summary>
    ''' �H���������e
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojKanryNaiyou As String = String.Empty
    ''' <summary>
    ''' �H���������e
    ''' </summary>
    ''' <value></value>
    ''' <returns>�H���������e</returns>
    ''' <remarks></remarks>
    <TableMap("koj_kanry_naiyou", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=100)> _
    Public Overridable Property KojKanryNaiyou() As String
        Get
            Return strKojKanryNaiyou
        End Get
        Set(ByVal value As String)
            strKojKanryNaiyou = value
        End Set
    End Property
#End Region

#Region "�����󋵓��e"
    ''' <summary>
    ''' �����󋵓��e
    ''' </summary>
    ''' <remarks></remarks>
    Private strNyuukinJykyNaiyou As String = String.Empty
    ''' <summary>
    ''' �����󋵓��e
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����󋵓��e</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_jyky_naiyou", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=100)> _
    Public Overridable Property NyuukinJykyNaiyou() As String
        Get
            Return strNyuukinJykyNaiyou
        End Get
        Set(ByVal value As String)
            strNyuukinJykyNaiyou = value
        End Set
    End Property
#End Region

#Region "���s�˗�����"
    ''' <summary>
    ''' ���s�˗�����
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiTime As DateTime
    ''' <summary>
    ''' ���s�˗�����
    ''' </summary>
    ''' <value></value>
    ''' <returns>���s�˗�����</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_time", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property HakIraiTime() As DateTime
        Get
            Return strHakIraiTime
        End Get
        Set(ByVal value As DateTime)
            strHakIraiTime = value
        End Set
    End Property
#End Region

#Region "���s�˗���t����"
    ''' <summary>
    ''' ���s�˗���t����
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiUkeDatetime As DateTime
    ''' <summary>
    ''' ���s�˗���t����
    ''' </summary>
    ''' <value></value>
    ''' <returns>���s�˗���t����</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_uke_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property HakIraiUkeDatetime() As DateTime
        Get
            Return strHakIraiUkeDatetime
        End Get
        Set(ByVal value As DateTime)
            strHakIraiUkeDatetime = value
        End Set
    End Property
#End Region

#Region "���s�˗��L�����Z������"
    ''' <summary>
    ''' ���s�˗��L�����Z������
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiCanDatetime As DateTime
    ''' <summary>
    ''' ���s�˗��L�����Z������
    ''' </summary>
    ''' <value></value>
    ''' <returns>���s�˗��L�����Z������</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_can_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property HakIraiCanDatetime() As DateTime
        Get
            Return strHakIraiCanDatetime
        End Get
        Set(ByVal value As DateTime)
            strHakIraiCanDatetime = value
        End Set
    End Property
#End Region

#Region "�ۏ؏��Ĕ��s��"
    ''' <summary>
    ''' �ۏ؏��Ĕ��s��
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoSaihakDate As DateTime
    ''' <summary>
    ''' �ۏ؏��Ĕ��s��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ۏ؏��Ĕ��s��</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_time", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property HosyousyoSaihakDate() As DateTime
        Get
            Return strHosyousyoSaihakDate
        End Get
        Set(ByVal value As DateTime)
            strHosyousyoSaihakDate = value
        End Set
    End Property
#End Region


#Region "���s�˗���������"
    ''' <summary>
    ''' ���s�˗���������
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknName As String
    ''' <summary>
    ''' ���s�˗���������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���s�˗���������</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_name", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=50)> _
    Public Overridable Property HakIraiBknName() As String
        Get
            Return strHakIraiBknName
        End Get
        Set(ByVal value As String)
            strHakIraiBknName = value
        End Set
    End Property
#End Region


#Region "���s�˗��������ݒn�P"
    ''' <summary>
    ''' ���s�˗��������ݒn�P
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknAdr1 As String = String.Empty
    ''' <summary>
    ''' ���s�˗��������ݒn�P
    ''' </summary>
    ''' <value></value>
    ''' <returns>���s�˗��������ݒn�P</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_adr1", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Overridable Property HakIraiBknAdr1() As String
        Get
            Return strHakIraiBknAdr1
        End Get
        Set(ByVal value As String)
            strHakIraiBknAdr1 = value
        End Set
    End Property
#End Region


#Region "���s�˗��������ݒn�Q"
    ''' <summary>
    ''' ���s�˗��������ݒn�Q
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknAdr2 As String = String.Empty
    ''' <summary>
    ''' ���s�˗��������ݒn�Q
    ''' </summary>
    ''' <value></value>
    ''' <returns>���s�˗��������ݒn�Q</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_adr2", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Overridable Property HakIraiBknAdr2() As String
        Get
            Return strHakIraiBknAdr2
        End Get
        Set(ByVal value As String)
            strHakIraiBknAdr2 = value
        End Set
    End Property
#End Region


#Region "���s�˗��������ݒn�R"
    ''' <summary>
    ''' ���s�˗��������ݒn�R
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiBknAdr3 As String = String.Empty
    ''' <summary>
    ''' ���s�˗��������ݒn�R
    ''' </summary>
    ''' <value></value>
    ''' <returns>���s�˗��������ݒn�R</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_bkn_adr3", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=54)> _
    Public Overridable Property HakIraiBknAdr3() As String
        Get
            Return strHakIraiBknAdr3
        End Get
        Set(ByVal value As String)
            strHakIraiBknAdr3 = value
        End Set
    End Property
#End Region

#Region "���s�˗����n��"
    ''' <summary>
    ''' ���s�˗����n��
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiHwDate As DateTime
    ''' <summary>
    ''' ���s�˗����n��
    ''' </summary>
    ''' <value></value>
    ''' <returns>���s�˗����n��</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_hw_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property HakIraiHwDate() As DateTime
        Get
            Return strHakIraiHwDate
        End Get
        Set(ByVal value As DateTime)
            strHakIraiHwDate = value
        End Set
    End Property
#End Region


#Region "���s�˗��S����"
    ''' <summary>
    ''' ���s�˗��S����
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiTanto As String
    ''' <summary>
    ''' ���s�˗��S����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���s�˗��S����</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_tanto", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=50)> _
    Public Overridable Property HakIraiTanto() As String
        Get
            Return strHakIraiTanto
        End Get
        Set(ByVal value As String)
            strHakIraiTanto = value
        End Set
    End Property
#End Region


#Region "���s�˗��S���ҘA����"
    ''' <summary>
    ''' ���s�˗��S���ҘA����
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiTantoTel As String
    ''' <summary>
    ''' ���s�˗��S���ҘA����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���s�˗��S���ҘA����</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_tanto_tel", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=255)> _
    Public Overridable Property HakIraiTantoTel() As String
        Get
            Return strHakIraiTantoTel
        End Get
        Set(ByVal value As String)
            strHakIraiTantoTel = value
        End Set
    End Property
#End Region


#Region "���s�˗����̓��O�C��ID"
    ''' <summary>
    ''' ���s�˗����̓��O�C��ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiLogin As String
    ''' <summary>
    ''' ���s�˗����̓��O�C��ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���s�˗����̓��O�C��ID</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_login", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=50)> _
    Public Overridable Property HakIraiLogin() As String
        Get
            Return strHakIraiLogin
        End Get
        Set(ByVal value As String)
            strHakIraiLogin = value
        End Set
    End Property
#End Region

#Region "���s�˗����̑����"
    ''' <summary>
    ''' ���s�˗����̑����
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakIraiSonota As String
    ''' <summary>
    ''' ���s�˗����̑����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���s�˗����̑����</returns>
    ''' <remarks></remarks>
    <TableMap("hak_irai_sonota", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=255)> _
    Public Overridable Property HakIraiSonota() As String
        Get
            Return strHakIraiSonota
        End Get
        Set(ByVal value As String)
            strHakIraiSonota = value
        End Set
    End Property
#End Region


End Class