''' <summary>
''' ���R�[�h�N���X�Ɋ��蓖�Ă�e�[�u�����̑����N���X
''' </summary>
''' <remarks></remarks>
<AttributeUsage(AttributeTargets.Class)> _
Public Class TableClassMapAttribute
    Inherits Attribute

    Private _tableName As String

    ''' <summary>
    ''' �R���X�g���N�^
    ''' </summary>
    ''' <param name="name"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal name As String)
        _tableName = name
    End Sub

    ''' <summary>
    ''' �e�[�u�����ږ�
    ''' </summary>
    ''' <value>�e�[�u�����ږ�</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TableName() As String
        Get
            Return _tableName
        End Get
    End Property
End Class