''' <summary>
''' �e�[�u�����ږ��̑����N���X
''' </summary>
''' <remarks></remarks>
<AttributeUsage(AttributeTargets.Property)> _
Public Class TableMapAttribute
    Inherits Attribute

    Private _itemName As String
    Private _isKey As Boolean
    Private _isUpdate As Boolean
    Private _isInsert As Boolean
    Private _deleteKey As Boolean
    Private _sqlType As System.Data.SqlDbType
    Private _sqlLength As Integer

    ''' <summary>
    ''' �R���X�g���N�^
    ''' </summary>
    ''' <param name="name"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal name As String)
        _itemName = name
        _isKey = False
        _isUpdate = False
        _sqlType = SqlDbType.Char
        _sqlLength = 0
    End Sub

    ''' <summary>
    ''' �e�[�u�����ږ�
    ''' </summary>
    ''' <value>�e�[�u�����ږ�</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ItemName() As String
        Get
            Return _itemName
        End Get
    End Property

    ''' <summary>
    ''' �v���C�}��Key�̔��f
    ''' </summary>
    ''' <value>�X�V�Ώۂ̏ꍇ�Ftrue</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsKey() As Boolean
        Set(ByVal value As Boolean)
            _isKey = value
        End Set
        Get
            Return _isKey
        End Get
    End Property

    ''' <summary>
    ''' �X�V�Ώ�
    ''' </summary>
    ''' <value>�X�V�Ώۂ̏ꍇ�Ftrue</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsUpdate() As Boolean
        Set(ByVal value As Boolean)
            _isUpdate = value
        End Set
        Get
            Return _isUpdate
        End Get
    End Property

    ''' <summary>
    ''' �o�^�Ώ�
    ''' </summary>
    ''' <value>�o�^�Ώۂ̏ꍇ�Ftrue</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsInsert() As Boolean
        Set(ByVal value As Boolean)
            _isInsert = value
        End Set
        Get
            Return _isInsert
        End Get
    End Property

    ''' <summary>
    ''' �폜�L�[
    ''' </summary>
    ''' <value>�폜�L�[�̏ꍇ�Ftrue</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeleteKey() As Boolean
        Set(ByVal value As Boolean)
            _deleteKey = value
        End Set
        Get
            Return _deleteKey
        End Get
    End Property

    ''' <summary>
    ''' SQL���ڃp�����[�^����
    ''' </summary>
    ''' <value>���ڂ̑���</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SqlType() As System.Data.SqlDbType
        Set(ByVal value As System.Data.SqlDbType)
            _sqlType = value
        End Set
        Get
            Return _sqlType
        End Get
    End Property

    ''' <summary>
    ''' SQL���ڌ����i�����񎞕K�{�j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SqlLength() As Integer
        Set(ByVal value As Integer)
            _sqlLength = value
        End Set
        Get
            Return _sqlLength
        End Get
    End Property

End Class
