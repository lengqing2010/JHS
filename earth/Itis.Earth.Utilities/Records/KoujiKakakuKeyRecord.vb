''' <summary>
''' 工事価格マスタの検索KEYレコードクラス
''' 検索条件に必要な情報のみ設定
''' </summary>
''' <remarks></remarks>
Public Class KoujiKakakuKeyRecord

#Region "加盟店コード"
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String = String.Empty
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>加盟店コード</returns>
    ''' <remarks></remarks>
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "営業所コード"
    ''' <summary>
    ''' 営業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyousyoCd As String = String.Empty
    ''' <summary>
    ''' 営業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 営業所コード</returns>
    ''' <remarks></remarks>
    Public Property EigyousyoCd() As String
        Get
            Return strEigyousyoCd
        End Get
        Set(ByVal value As String)
            strEigyousyoCd = value
        End Set
    End Property
#End Region

#Region "系列コード"
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuCd As String = String.Empty
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>系列コード</returns>
    ''' <remarks></remarks>
    Public Property KeiretuCd() As String
        Get
            Return strKeiretuCd
        End Get
        Set(ByVal value As String)
            strKeiretuCd = value
        End Set
    End Property
#End Region

#Region "商品コード"
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String = String.Empty
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品コード</returns>
    ''' <remarks></remarks>
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

#Region "工事会社コード＋事業所コード"
    ''' <summary>
    ''' 工事会社コード＋事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojGaisyaCd As String = String.Empty
    ''' <summary>
    ''' 工事会社コード＋事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事会社コード＋事業所コード</returns>
    ''' <remarks></remarks>
    Public Overridable Property KojGaisyaCd() As String
        Get
            Return strKojGaisyaCd
        End Get
        Set(ByVal value As String)
            strKojGaisyaCd = value
        End Set
    End Property
#End Region

End Class
