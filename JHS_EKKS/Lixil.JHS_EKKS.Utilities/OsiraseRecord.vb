''' <summary>
''' お知らせテーブルのレコードクラスです
''' </summary>
''' <remarks>お知らせテーブルの項目プロパティ</remarks>
Public Class OsiraseRecord

#Region "年月日"
    ''' <summary>
    ''' 年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNengappi As DateTime
    ''' <summary>
    ''' 年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns>年月日</returns>
    ''' <remarks></remarks>
    Public Property Nengappi() As DateTime
        Get
            Return dateNengappi
        End Get
        Set(ByVal value As DateTime)
            dateNengappi = value
        End Set
    End Property
#End Region

#Region "入力部署"
    ''' <summary>
    ''' 入力部署
    ''' </summary>
    ''' <remarks></remarks>
    Private strNyuuryokuBusyo As String
    ''' <summary>
    ''' 入力部署
    ''' </summary>
    ''' <value></value>
    ''' <returns>入力部署</returns>
    ''' <remarks></remarks>
    Public Property NyuuryokuBusyo() As String
        Get
            Return strNyuuryokuBusyo
        End Get
        Set(ByVal value As String)
            strNyuuryokuBusyo = value
        End Set
    End Property
#End Region

#Region "入力者"
    ''' <summary>
    ''' 入力者
    ''' </summary>
    ''' <remarks></remarks>
    Private strNyuuryokuMei As String
    ''' <summary>
    ''' 入力者
    ''' </summary>
    ''' <value></value>
    ''' <returns>入力者</returns>
    ''' <remarks></remarks>
    Public Property NyuuryokuMei() As String
        Get
            Return strNyuuryokuMei
        End Get
        Set(ByVal value As String)
            strNyuuryokuMei = value
        End Set
    End Property
#End Region

#Region "表示内容"
    ''' <summary>
    ''' 表示内容
    ''' </summary>
    ''' <remarks></remarks>
    Private strHyoujiNaiyou As String
    ''' <summary>
    ''' 表示内容
    ''' </summary>
    ''' <value></value>
    ''' <returns>表示内容</returns>
    ''' <remarks></remarks>
    Public Property HyoujiNaiyou() As String
        Get
            Return strHyoujiNaiyou
        End Get
        Set(ByVal value As String)
            strHyoujiNaiyou = value
        End Set
    End Property
#End Region

#Region "リンク先"
    ''' <summary>
    ''' リンク先
    ''' </summary>
    ''' <remarks></remarks>
    Private strLinkSaki As String
    ''' <summary>
    ''' リンク先
    ''' </summary>
    ''' <value></value>
    ''' <returns>リンク先</returns>
    ''' <remarks></remarks>
    Public Property LinkSaki() As String
        Get
            Return strLinkSaki
        End Get
        Set(ByVal value As String)
            strLinkSaki = value
        End Set
    End Property
#End Region

End Class
