''' <summary>
''' SQL�p�����[�^�ݒ�p���R�[�h�N���X
''' </summary>
''' <remarks></remarks>
Public Class ParamRecord

#Region "�p�����[�^"
    ''' <summary>
    ''' �p�����[�^
    ''' </summary>
    ''' <remarks></remarks>
    Private strParam As String
    ''' <summary>
    ''' �p�����[�^
    ''' </summary>
    ''' <value></value>
    ''' <returns> �p�����[�^</returns>
    ''' <remarks></remarks>
    Public Property Param() As String
        Get
            Return strParam
        End Get
        Set(ByVal value As String)
            strParam = value
        End Set
    End Property
#End Region

#Region "DB�^�C�v"
    ''' <summary>
    ''' DB�^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Private strDbType As SqlDbType
    ''' <summary>
    ''' DB�^�C�v
    ''' </summary>
    ''' <value></value>
    ''' <returns> DB�^�C�v</returns>
    ''' <remarks></remarks>
    Public Property DbType() As SqlDbType
        Get
            Return strDbType
        End Get
        Set(ByVal value As SqlDbType)
            strDbType = value
        End Set
    End Property
#End Region

#Region "�����O�X"
    ''' <summary>
    ''' �����O�X
    ''' </summary>
    ''' <remarks></remarks>
    Private intParamLength As Integer
    ''' <summary>
    ''' �����O�X
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����O�X</returns>
    ''' <remarks></remarks>
    Public Property ParamLength() As Integer
        Get
            Return intParamLength
        End Get
        Set(ByVal value As Integer)
            intParamLength = value
        End Set
    End Property
#End Region

#Region "�ݒ肷��f�[�^"
    ''' <summary>
    ''' �ݒ肷��f�[�^
    ''' </summary>
    ''' <remarks></remarks>
    Private objSetData As Object
    ''' <summary>
    ''' �ݒ肷��f�[�^
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ݒ肷��f�[�^</returns>
    ''' <remarks></remarks>
    Public Property SetData() As Object
        Get
            Return objSetData
        End Get
        Set(ByVal value As Object)
            objSetData = value
        End Set
    End Property
#End Region

End Class
