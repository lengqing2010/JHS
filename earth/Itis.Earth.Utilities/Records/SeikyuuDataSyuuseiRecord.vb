''' <summary>
''' �������R�[�h�N���X/�������󎚓��e�ҏW���
''' </summary>
''' <remarks>�����f�[�^�̊i�[���Ɏg�p���܂�</remarks>
<TableClassMap("t_seikyuu_kagami")> _
Public Class SeikyuuDataSyuuseiRecord
    Inherits SeikyuuDataRecord

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
    Public Overrides Property SeikyuusyoNo() As String
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
    <TableMap("torikesi", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
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
    ''' <returns> �����於1</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_mei", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overrides Property SeikyuuSakiMei() As String
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
    <TableMap("seikyuu_saki_mei2", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overrides Property SeikyuuSakiMei2() As String
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
    <TableMap("yuubin_no", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Overrides Property YuubinNo() As String
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
    <TableMap("jyuusyo1", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overrides Property Jyuusyo1() As String
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
    <TableMap("jyuusyo2", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overrides Property Jyuusyo2() As String
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
    <TableMap("tel_no", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=16)> _
    Public Overrides Property TelNo() As String
        Get
            Return strTelNo
        End Get
        Set(ByVal value As String)
            strTelNo = value
        End Set
    End Property
#End Region

#Region "�����\���(�������\���)"
    ''' <summary>
    ''' �����\���
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKonkaiKaisyuuYoteiDate As DateTime
    ''' <summary>
    ''' �����\���
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("konkai_kaisyuu_yotei_date", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KonkaiKaisyuuYoteiDate() As DateTime
        Get
            Return dateKonkaiKaisyuuYoteiDate
        End Get
        Set(ByVal value As DateTime)
            dateKonkaiKaisyuuYoteiDate = value
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
    <TableMap("tantousya_mei", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overrides Property TantousyaMei() As String
        Get
            Return strTantousyaMei
        End Get
        Set(ByVal value As String)
            strTantousyaMei = value
        End Set
    End Property
#End Region

End Class
