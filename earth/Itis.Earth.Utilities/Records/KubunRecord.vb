''' <summary>
''' TBL_M�ް��敪�e�[�u���̃��R�[�h�N���X�ł�
''' </summary>
''' <remarks>TBL_M�ް��敪�e�[�u���̍��ڃv���p�e�B</remarks>
Public Class KubunRecord

#Region "�敪"
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <remarks></remarks>
    Private strKubun As String
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns>�敪</returns>
    ''' <remarks></remarks>
    <TableMap("kbn", IsKey:=True, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Property Kubun() As String
        Get
            Return strKubun
        End Get
        Set(ByVal value As String)
            strKubun = value
        End Set
    End Property
#End Region

#Region "���"
    ''' <summary>
    ''' ���
    ''' </summary>
    ''' <remarks>0�ȊO:������</remarks>
    Private intTorikeshi As Integer = Integer.MinValue
    ''' <summary>
    ''' ���
    ''' </summary>
    ''' <value></value>
    ''' <returns>���</returns>
    ''' <remarks>0�ȊO:������</remarks>
    <TableMap("torikesi", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Torikeshi() As Integer
        Get
            Return intTorikeshi
        End Get
        Set(ByVal value As Integer)
            intTorikeshi = value
        End Set
    End Property
#End Region

#Region "�敪��"
    ''' <summary>
    ''' �敪��
    ''' </summary>
    ''' <remarks></remarks>
    Private strKubunMei As String
    ''' <summary>
    ''' �敪��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�敪��</returns>
    ''' <remarks></remarks>
    <TableMap("kbn_mei", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Property KubunMei() As String
        Get
            Return strKubunMei
        End Get
        Set(ByVal value As String)
            strKubunMei = value
        End Set
    End Property
#End Region

#Region "�����}�X�^��Q�ƃt���O"
    ''' <summary>
    ''' �����}�X�^��Q�ƃt���O
    ''' </summary>
    ''' <remarks></remarks>
    Private intGenkaMasterHisansyouFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' �����}�X�^��Q�ƃt���O
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����}�X�^��Q�ƃt���O</returns>
    ''' <remarks></remarks>
    <TableMap("genka_master_hisansyou_flg", IsKey:=False, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property GenkaMasterHisansyouFlg() As Integer
        Get
            Return intGenkaMasterHisansyouFlg
        End Get
        Set(ByVal value As Integer)
            intGenkaMasterHisansyouFlg = value
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
