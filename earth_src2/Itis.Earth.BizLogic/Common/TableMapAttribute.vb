''' <summary>
''' �e�[�u�����ږ��̑����N���X
''' </summary>
''' <remarks></remarks>
<AttributeUsage(AttributeTargets.Property)> _
Public Class TableMapAttribute
    Inherits Attribute

    Private item_name As String
    Private is_key As Boolean
    Private is_update As Boolean
    Private sql_type As System.Data.SqlDbType
    Private sql_length As Integer

    ''' <summary>
    ''' �R���X�g���N�^
    ''' </summary>
    ''' <param name="name"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal name As String)
        item_name = name
        is_key = False
        is_update = False
        sql_type = SqlDbType.Char
        sql_length = 0
    End Sub

    ''' <summary>
    ''' �e�[�u�����ږ�
    ''' </summary>
    ''' <value>�e�[�u�����ږ�</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ItemName() As String
        Get
            Return item_name
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
            is_key = value
        End Set
        Get
            Return is_key
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
            is_update = value
        End Set
        Get
            Return is_update
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
            sql_type = value
        End Set
        Get
            Return sql_type
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
            sql_length = value
        End Set
        Get
            Return sql_length
        End Get
    End Property

End Class
