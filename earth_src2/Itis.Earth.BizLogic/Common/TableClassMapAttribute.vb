''' <summary>
''' ���R�[�h�N���X�Ɋ��蓖�Ă�e�[�u�����̑����N���X
''' </summary>
''' <remarks></remarks>
<AttributeUsage(AttributeTargets.Class)> _
Public Class TableClassMapAttribute
    Inherits Attribute

    Private table_name As String

    ''' <summary>
    ''' �R���X�g���N�^
    ''' </summary>
    ''' <param name="name"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal name As String)
        table_name = name
    End Sub

    ''' <summary>
    ''' �e�[�u�����ږ�
    ''' </summary>
    ''' <value>�e�[�u�����ږ�</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TableName() As String
        Get
            Return table_name
        End Get
    End Property
End Class