''' <summary>
''' 営業所マスタ検索レコードクラス
''' </summary>
''' <remarks></remarks>
Public Class EigyousyoSearchRecord

#Region "営業所コード"
    ''' <summary>
    ''' 営業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyousyoCd As String
    ''' <summary>
    ''' 営業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 営業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("eigyousyo_cd")> _
    Public Property EigyousyoCd() As String
        Get
            Return strEigyousyoCd
        End Get
        Set(ByVal value As String)
            strEigyousyoCd = value
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

#Region "営業所名"
    ''' <summary>
    ''' 営業所名
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyousyoMei As String
    ''' <summary>
    ''' 営業所名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 営業所名</returns>
    ''' <remarks></remarks>
    <TableMap("eigyousyo_mei")> _
    Public Property EigyousyoMei() As String
        Get
            Return strEigyousyoMei
        End Get
        Set(ByVal value As String)
            strEigyousyoMei = value
        End Set
    End Property
#End Region

#Region "営業所カナ"
    ''' <summary>
    ''' 営業所カナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyousyoKana As String
    ''' <summary>
    ''' 営業所カナ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 営業所カナ</returns>
    ''' <remarks></remarks>
    <TableMap("eigyousyo_kana")> _
    Public Property EigyousyoKana() As String
        Get
            Return strEigyousyoKana
        End Get
        Set(ByVal value As String)
            strEigyousyoKana = value
        End Set
    End Property
#End Region

#Region "請求先コード"
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求先コード</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_cd")> _
    Public Property SeikyuuSakiCd() As String
        Get
            Return strSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "請求先枝番"
    ''' <summary>
    ''' 請求先枝番
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrc As String
    ''' <summary>
    ''' 請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求先枝番</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_brc")> _
    Public Property SeikyuuSakiBrc() As String
        Get
            Return strSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "請求先区分"
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求先区分</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn")> _
    Public Property SeikyuuSakiKbn() As String
        Get
            Return strSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "請求先名"
    ''' <summary>
    ''' 請求先名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMei As String
    ''' <summary>
    ''' 請求先名
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求先名</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_mei")> _
    Public Property SeikyuuSakiMei() As String
        Get
            Return strSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei = value
        End Set
    End Property
#End Region

#Region "請求先カナ"
    ''' <summary>
    ''' 請求先カナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKana As String
    ''' <summary>
    ''' 請求先カナ
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求先カナ</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kana")> _
    Public Property SeikyuuSakiKana() As String
        Get
            Return strSeikyuuSakiKana
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKana = value
        End Set
    End Property
#End Region

End Class