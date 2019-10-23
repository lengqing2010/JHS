''' <summary>
''' SQLパラメータ設定用レコードクラス
''' </summary>
''' <remarks></remarks>
Public Class ParamRecord

#Region "パラメータ"
    ''' <summary>
    ''' パラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private strParam As String
    ''' <summary>
    ''' パラメータ
    ''' </summary>
    ''' <value></value>
    ''' <returns> パラメータ</returns>
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

#Region "DBタイプ"
    ''' <summary>
    ''' DBタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Private strDbType As SqlDbType
    ''' <summary>
    ''' DBタイプ
    ''' </summary>
    ''' <value></value>
    ''' <returns> DBタイプ</returns>
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

#Region "レングス"
    ''' <summary>
    ''' レングス
    ''' </summary>
    ''' <remarks></remarks>
    Private intParamLength As Integer
    ''' <summary>
    ''' レングス
    ''' </summary>
    ''' <value></value>
    ''' <returns> レングス</returns>
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

#Region "設定するデータ"
    ''' <summary>
    ''' 設定するデータ
    ''' </summary>
    ''' <remarks></remarks>
    Private objSetData As Object
    ''' <summary>
    ''' 設定するデータ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 設定するデータ</returns>
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
