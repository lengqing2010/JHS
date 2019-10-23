''' <summary>
''' 売上データ照会の検索結果格納用レコード
''' </summary>
''' <remarks></remarks>
Public Class UriageSearchResultRecord
    Inherits UriageDataRecord

#Region "地盤T.施主名"
    ''' <summary>
    ''' 地盤T.施主名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String
    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 施主名</returns>
    ''' <remarks></remarks>
    <TableMap("sesyu_mei")> _
    Public Property SesyuMei() As String
        Get
            Return strSesyuMei
        End Get
        Set(ByVal value As String)
            strSesyuMei = value
        End Set
    End Property
#End Region

#Region "売上処理FLG"
    ''' <summary>
    ''' 売上処理FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriKeijyouFlg As String
    ''' <summary>
    ''' 売上処理FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上処理FLG</returns>
    ''' <remarks></remarks>
    <TableMap("uri_keijyou_flg")> _
    Public Property UriKeijyouFlg() As Integer
        Get
            Return intUriKeijyouFlg
        End Get
        Set(ByVal value As Integer)
            intUriKeijyouFlg = value
        End Set
    End Property
#End Region

#Region "加盟店コード"
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店コード</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd")> _
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "加盟店名"
    ''' <summary>
    ''' 加盟店名
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMei As String
    ''' <summary>
    ''' 加盟店名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店名</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_mei1")> _
    Public Property KameitenMei() As String
        Get
            Return strKameitenMei
        End Get
        Set(ByVal value As String)
            strKameitenMei = value
        End Set
    End Property

#End Region

End Class
