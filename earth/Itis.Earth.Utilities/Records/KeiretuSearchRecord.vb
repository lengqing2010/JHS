Public Class KeiretuSearchRecord

#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 区分</returns>
    ''' <remarks></remarks>
    <TableMap("kbn")> _
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "取消"
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取消</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi")> _
    Public Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "系列ｺｰﾄﾞ"
    ''' <summary>
    ''' 系列ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuCd As String
    ''' <summary>
    ''' 系列ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 系列ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("keiretu_cd")> _
    Public Property KeiretuCd() As String
        Get
            Return strKeiretuCd
        End Get
        Set(ByVal value As String)
            strKeiretuCd = value
        End Set
    End Property
#End Region

#Region "系列名"
    ''' <summary>
    ''' 系列名
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuMei As String
    ''' <summary>
    ''' 系列名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 系列名</returns>
    ''' <remarks></remarks>
    <TableMap("keiretu_mei")> _
    Public Property KeiretuMei() As String
        Get
            Return strKeiretuMei
        End Get
        Set(ByVal value As String)
            strKeiretuMei = value
        End Set
    End Property
#End Region

End Class