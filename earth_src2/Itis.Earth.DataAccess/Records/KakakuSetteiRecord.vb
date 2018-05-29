Public Class KakakuSetteiRecord

#Region "商品区分"
    ''' <summary>
    ''' 商品区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouhinKbn As Integer
    ''' <summary>
    ''' 商品区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品区分</returns>
    ''' <remarks>100:商品区分1 110,115:商品区分2 120:商品区分3</remarks>
    Public Property SyouhinKbn() As Integer
        Get
            Return intSyouhinKbn
        End Get
        Set(ByVal value As Integer)
            intSyouhinKbn = value
        End Set
    End Property
#End Region
#Region "調査方法NO"
    ''' <summary>
    ''' 調査方法NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyousaHouhouNo As Integer
    ''' <summary>
    ''' 調査方法NO
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査方法NO</returns>
    ''' <remarks></remarks>
    Public Property TyousaHouhouNo() As Integer
        Get
            Return intTyousaHouhouNo
        End Get
        Set(ByVal value As Integer)
            intTyousaHouhouNo = value
        End Set
    End Property
#End Region
#Region "調査概要"
    ''' <summary>
    ''' 調査概要
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyousaGaiyou As Integer
    ''' <summary>
    ''' 調査概要
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査概要</returns>
    ''' <remarks></remarks>
    Public Property TyousaGaiyou() As Integer
        Get
            Return intTyousaGaiyou
        End Get
        Set(ByVal value As Integer)
            intTyousaGaiyou = value
        End Set
    End Property
#End Region
#Region "商品ｺｰﾄﾞ"
    ''' <summary>
    ''' 商品ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' 商品ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品ｺｰﾄﾞ</returns>
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
#Region "価格設定場所"
    ''' <summary>
    ''' 価格設定場所
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakakuSettei As Integer
    ''' <summary>
    ''' 価格設定場所
    ''' </summary>
    ''' <value></value>
    ''' <returns>価格設定場所</returns>
    ''' <remarks></remarks>
    Public Property KakakuSettei() As Integer
        Get
            Return intKakakuSettei
        End Get
        Set(ByVal value As Integer)
            intKakakuSettei = value
        End Set
    End Property
#End Region

End Class
