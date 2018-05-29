''' <summary>
''' 商品情報レコードです
''' </summary>
''' <remarks></remarks>
Public Class SyouhinRecord

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
#Region "商品名"
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinNm As String
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品名</returns>
    ''' <remarks></remarks>
    Public Property SyouhinNm() As String
        Get
            Return strSyouhinNm
        End Get
        Set(ByVal value As String)
            strSyouhinNm = value
        End Set
    End Property
#End Region
#Region "税区分"
    ''' <summary>
    ''' 税区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strZeiKbn As String
    ''' <summary>
    ''' 税区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>税区分</returns>
    ''' <remarks></remarks>
    Public Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
        End Set
    End Property
#End Region
#Region "標準価格"
    ''' <summary>
    ''' 標準価格
    ''' </summary>
    ''' <remarks></remarks>
    Private decHyoujunKakaku As Decimal
    ''' <summary>
    ''' 標準価格
    ''' </summary>
    ''' <value></value>
    ''' <returns>標準価格</returns>
    ''' <remarks></remarks>
    Public Property HyoujunKakaku() As Decimal
        Get
            Return decHyoujunKakaku
        End Get
        Set(ByVal value As Decimal)
            decHyoujunKakaku = value
        End Set
    End Property
#End Region
#Region "税率"
    ''' <summary>
    ''' 税率
    ''' </summary>
    ''' <remarks></remarks>
    Private decZeiritu As Decimal
    ''' <summary>
    ''' 税率
    ''' </summary>
    ''' <value></value>
    ''' <returns>税率</returns>
    ''' <remarks></remarks>
    Public Property Zeiritu() As Decimal
        Get
            Return decZeiritu
        End Get
        Set(ByVal value As Decimal)
            decZeiritu = value
        End Set
    End Property
#End Region
#Region "倉庫ｺｰﾄﾞ"
    ''' <summary>
    ''' 倉庫ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSoukoCd As String
    ''' <summary>
    ''' 倉庫ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns>倉庫ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    Public Property SoukoCd() As String
        Get
            Return strSoukoCd
        End Get
        Set(ByVal value As String)
            strSoukoCd = value
        End Set
    End Property
#End Region
End Class
