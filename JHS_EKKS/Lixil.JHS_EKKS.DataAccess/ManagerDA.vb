''' <summary>
''' DB�A�N�Z�X�Ɋւ���Ǘ��N���X
''' </summary>
''' <remarks>NotInheritable:�C���X�^���X�𐶐������Ȃ��悤�ɂ���
''' ����A�N�Z�X���ɂ̂݋N�����ADB�ڑ��������ݒ肷��B
''' </remarks>
Public NotInheritable Class ManagerDA
    ''' <summary>
    ''' DB�ڑ�������̐ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared connStr As String = System.Configuration.ConfigurationManager. _
        ConnectionStrings("ConnectionString").ConnectionString
    ''' <summary>
    ''' DB�ڑ�������̎擾
    ''' </summary>
    ''' <value>DB�ڑ�������</value>
    ''' <returns>DB�ڑ�������</returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Connection() As String
        Get
            Return connStr
        End Get
    End Property

End Class
