''' <summary>
''' �������@�}�X�^�̃��R�[�h�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class TyousahouhouRecord

#Region "�������@NO"
    ''' <summary>
    ''' �������@NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysHouhouNo As Integer = Integer.MinValue
    ''' <summary>
    ''' �������@NO
    ''' </summary>
    ''' <value></value>
    ''' <returns>�������@NO</returns>
    ''' <remarks></remarks>
    <TableMap("tys_houhou_no", IsKey:=True, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property TysHouhouNo() As Integer
        Get
            Return intTysHouhouNo
        End Get
        Set(ByVal value As Integer)
            intTysHouhouNo = value
        End Set
    End Property
#End Region

#Region "���"
    ''' <summary>
    ''' ���
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer = Integer.MinValue
    ''' <summary>
    ''' ���
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "�������@����"
    ''' <summary>
    ''' �������@����
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysHouhouMeiRyaku As String
    ''' <summary>
    ''' �������@����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�������@����</returns>
    ''' <remarks></remarks>
    <TableMap("tys_houhou_mei_ryaku", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=4)> _
    Public Property TysHouhouMeiRyaku() As String
        Get
            Return strTysHouhouMeiRyaku
        End Get
        Set(ByVal value As String)
            strTysHouhouMeiRyaku = value
        End Set
    End Property
#End Region

#Region "�������@����"
    ''' <summary>
    ''' �������@����
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysHouhouMei As String
    ''' <summary>
    ''' �������@����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�������@����</returns>
    ''' <remarks></remarks>
    <TableMap("tys_houhou_mei", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Property TysHouhouMei() As String
        Get
            Return strTysHouhouMei
        End Get
        Set(ByVal value As String)
            strTysHouhouMei = value
        End Set
    End Property
#End Region

#Region "�����ݒ�s�v�t���O"
    ''' <summary>
    ''' �����ݒ�s�v�t���O
    ''' </summary>
    ''' <remarks></remarks>
    Private intGenkaSetteiFuyouFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' �����ݒ�s�v�t���O
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����ݒ�s�v�t���O</returns>
    ''' <remarks></remarks>
    <TableMap("genka_settei_fuyou_flg", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property GenkaSetteiFuyouFlg() As Integer
        Get
            Return intGenkaSetteiFuyouFlg
        End Get
        Set(ByVal value As Integer)
            intGenkaSetteiFuyouFlg = value
        End Set
    End Property
#End Region

#Region "���i�ݒ�s�v�t���O"
    ''' <summary>
    ''' ���i�ݒ�s�v�t���O
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakakuSetteiFuyouFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' ���i�ݒ�s�v�t���O
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i�ݒ�s�v�t���O</returns>
    ''' <remarks></remarks>
    <TableMap("kakaku_settei_fuyou_flg", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property KakakuSetteiFuyouFlg() As Integer
        Get
            Return intKakakuSetteiFuyouFlg
        End Get
        Set(ByVal value As Integer)
            intKakakuSetteiFuyouFlg = value
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
    Private dateAddDatetime As DateTime
    ''' <summary>
    ''' �o�^����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �o�^����</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property AddDatetime() As DateTime
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
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V����</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

End Class

