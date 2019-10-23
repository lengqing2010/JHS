Public Class SeikyuuSiireSakiHenkouRecord

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

#Region "調査請求先コード"
    ''' <summary>
    ''' 調査請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysSeikyuuSakiCd As String
    ''' <summary>
    ''' 調査請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査請求先コード</returns>
    ''' <remarks></remarks>
    <TableMap("tys_seikyuu_saki_cd")> _
    Public Property TysSeikyuuSakiCd() As String
        Get
            Return strTysSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strTysSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "調査請求先枝番"
    ''' <summary>
    ''' 調査請求先枝番
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysSeikyuuSakiBrc As String
    ''' <summary>
    ''' 調査請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査請求先枝番</returns>
    ''' <remarks></remarks>
    <TableMap("tys_seikyuu_saki_brc")> _
    Public Property TysSeikyuuSakiBrc() As String
        Get
            Return strTysSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strTysSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "調査請求先区分"
    ''' <summary>
    ''' 調査請求先区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysSeikyuuSakiKbn As String
    ''' <summary>
    ''' 調査請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査請求先区分</returns>
    ''' <remarks></remarks>
    <TableMap("tys_seikyuu_saki_kbn")> _
    Public Property TysSeikyuuSakiKbn() As String
        Get
            Return strTysSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strTysSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "調査請求先名"
    ''' <summary>
    ''' 調査請求先名
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysSeikyuuSakiMei As String
    ''' <summary>
    ''' 調査請求先名
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査請求先名</returns>
    ''' <remarks></remarks>
    <TableMap("tys_seikyuu_saki_mei")> _
    Public Property TysSeikyuuSakiMei() As String
        Get
            Return strTysSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strTysSeikyuuSakiMei = value
        End Set
    End Property
#End Region

#Region "工事請求先コード"
    ''' <summary>
    ''' 工事請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojSeikyuuSakiCd As String
    ''' <summary>
    ''' 工事請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事請求先コード</returns>
    ''' <remarks></remarks>
    <TableMap("koj_seikyuu_saki_cd")> _
    Public Property KojSeikyuuSakiCd() As String
        Get
            Return strKojSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strKojSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "工事請求先枝番"
    ''' <summary>
    ''' 工事請求先枝番
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojSeikyuuSakiBrc As String
    ''' <summary>
    ''' 工事請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事請求先枝番</returns>
    ''' <remarks></remarks>
    <TableMap("koj_seikyuu_saki_brc")> _
    Public Property KojSeikyuuSakiBrc() As String
        Get
            Return strKojSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strKojSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "工事請求先区分"
    ''' <summary>
    ''' 工事請求先区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojSeikyuuSakiKbn As String
    ''' <summary>
    ''' 工事請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事請求先区分</returns>
    ''' <remarks></remarks>
    <TableMap("koj_seikyuu_saki_kbn")> _
    Public Property KojSeikyuuSakiKbn() As String
        Get
            Return strKojSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strKojSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "工事請求先名"
    ''' <summary>
    ''' 工事請求先名
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojSeikyuuSakiMei As String
    ''' <summary>
    ''' 工事請求先名
    ''' </summary>
    ''' <value></value>
    ''' <returns>工事請求先名</returns>
    ''' <remarks></remarks>
    <TableMap("koj_seikyuu_saki_mei")> _
    Public Property KojSeikyuuSakiMei() As String
        Get
            Return strKojSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strKojSeikyuuSakiMei = value
        End Set
    End Property
#End Region

#Region "販促品請求先コード"
    ''' <summary>
    ''' 販促品請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strHansokuhinSeikyuuSakiCd As String
    ''' <summary>
    ''' 販促品請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品請求先コード</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuu_saki_cd")> _
    Public Property HansokuhinSeikyuuSakiCd() As String
        Get
            Return strHansokuhinSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strHansokuhinSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "販促品請求先枝番"
    ''' <summary>
    ''' 販促品請求先枝番
    ''' </summary>
    ''' <remarks></remarks>
    Private strHansokuhinSeikyuuSakiBrc As String
    ''' <summary>
    ''' 販促品請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品請求先枝番</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuu_saki_brc")> _
    Public Property HansokuhinSeikyuuSakiBrc() As String
        Get
            Return strHansokuhinSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strHansokuhinSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "販促品請求先区分"
    ''' <summary>
    ''' 販促品請求先区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strHansokuhinSeikyuuSakiKbn As String
    ''' <summary>
    ''' 販促品請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品請求先区分</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuu_saki_kbn")> _
    Public Property HansokuhinSeikyuuSakiKbn() As String
        Get
            Return strHansokuhinSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strHansokuhinSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "販促品請求先名"
    ''' <summary>
    ''' 販促品請求先名
    ''' </summary>
    ''' <remarks></remarks>
    Private strHansokuhinSeikyuuSakiMei As String
    ''' <summary>
    ''' 販促品請求先名
    ''' </summary>
    ''' <value></value>
    ''' <returns>販促品請求先名</returns>
    ''' <remarks></remarks>
    <TableMap("hansoku_seikyuu_saki_mei")> _
    Public Property HansokuhinSeikyuuSakiMei() As String
        Get
            Return strHansokuhinSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strHansokuhinSeikyuuSakiMei = value
        End Set
    End Property
#End Region

#Region "商品コード"
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品コード</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_cd")> _
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

#Region "倉庫コード"
    ''' <summary>
    ''' 倉庫コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSoukoCd As String
    ''' <summary>
    ''' 倉庫コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 倉庫コード</returns>
    ''' <remarks></remarks>
    <TableMap("souko_cd")> _
    Public Property SoukoCd() As String
        Get
            Return strSoukoCd
        End Get
        Set(ByVal value As String)
            strSoukoCd = value
        End Set
    End Property
#End Region

#Region "商品区分3"
    ''' <summary>
    ''' 商品区分3
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinKbn3 As String
    ''' <summary>
    ''' 商品区分3
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品区分3</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_kbn3")> _
    Public Property SyouhinKbn3() As String
        Get
            Return strSyouhinKbn3
        End Get
        Set(ByVal value As String)
            strSyouhinKbn3 = value
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
    ''' <returns> 請求先コード</returns>
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
    ''' <returns> 請求先枝番</returns>
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
    ''' <returns> 請求先区分</returns>
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

End Class