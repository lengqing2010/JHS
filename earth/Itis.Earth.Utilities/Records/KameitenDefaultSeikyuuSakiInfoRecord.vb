<TableClassMap("m_kameiten")> _
Public Class KameitenDefaultSeikyuuSakiInfoRecord
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
    ''' <returns>加盟店コード</returns>
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

#Region "加盟店名1"
    ''' <summary>
    ''' 加盟店名1
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMei1 As String
    ''' <summary>
    ''' 加盟店名1
    ''' </summary>
    ''' <value></value>
    ''' <returns>加盟店名1</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_mei1")> _
    Public Property KameitenMei1() As String
        Get
            Return strKameitenMei1
        End Get
        Set(ByVal value As String)
            strKameitenMei1 = value
        End Set
    End Property
#End Region

#Region "加盟店カナ1"
    ''' <summary>
    ''' 加盟店カナ1
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenKana1 As String
    ''' <summary>
    ''' 加盟店カナ1
    ''' </summary>
    ''' <value></value>
    ''' <returns>加盟店カナ1</returns>
    ''' <remarks></remarks>
    <TableMap("tenmei_kana1")> _
    Public Property KameitenKana1() As String
        Get
            Return strKameitenKana1
        End Get
        Set(ByVal value As String)
            strKameitenKana1 = value
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
    ''' <returns> 調査請求先名</returns>
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

#Region "調査請求先変更有無"
    ''' <summary>
    ''' 調査請求先変更有無
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysHenkouUmu As String
    ''' <summary>
    ''' 調査請求先変更有無
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("tys_henkou_umu")> _
    Public Property TysHenkouUmu() As String
        Get
            Return strTysHenkouUmu
        End Get
        Set(ByVal value As String)
            strTysHenkouUmu = value
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
    ''' <returns> 工事請求先名</returns>
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

#Region "工事請求先変更有無"
    ''' <summary>
    ''' 工事請求先変更有無
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojHenkouUmu As String
    ''' <summary>
    ''' 工事請求先変更有無
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("koj_henkou_umu")> _
    Public Property KojHenkouUmu() As String
        Get
            Return strKojHenkouUmu
        End Get
        Set(ByVal value As String)
            strKojHenkouUmu = value
        End Set
    End Property
#End Region

#Region "販促品請求先コード"
    ''' <summary>
    ''' 販促品請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strHnskSeikyuuSakiCd As String
    ''' <summary>
    ''' 販促品請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品請求先コード</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuu_saki_cd")> _
    Public Property HnskSeikyuuSakiCd() As String
        Get
            Return strHnskSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strHnskSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "販促品請求先枝番"
    ''' <summary>
    ''' 販促品請求先枝番
    ''' </summary>
    ''' <remarks></remarks>
    Private strHnskSeikyuuSakiBrc As String
    ''' <summary>
    ''' 販促品請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品請求先枝番</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuu_saki_brc")> _
    Public Property HnskSeikyuuSakiBrc() As String
        Get
            Return strHnskSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strHnskSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "販促品請求先区分"
    ''' <summary>
    ''' 販促品請求先区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strHnskSeikyuuSakiKbn As String
    ''' <summary>
    ''' 販促品請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品請求先区分</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuu_saki_kbn")> _
    Public Property HnskSeikyuuSakiKbn() As String
        Get
            Return strHnskSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strHnskSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "販促品請求先名"
    ''' <summary>
    ''' 販促品請求先名
    ''' </summary>
    ''' <remarks></remarks>
    Private strHnskSeikyuuSakiMei As String
    ''' <summary>
    ''' 販促品請求先名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品請求先名</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuu_saki_mei")> _
    Public Property HnskSeikyuuSakiMei() As String
        Get
            Return strHnskSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strHnskSeikyuuSakiMei = value
        End Set
    End Property
#End Region

#Region "販促品請求先変更有無"
    ''' <summary>
    ''' 販促品請求先変更有無
    ''' </summary>
    ''' <remarks></remarks>
    Private strHnskHenkouUmu As String
    ''' <summary>
    ''' 販促品請求先変更有無
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("hansoku_henkou_umu")> _
    Public Property HnskHenkouUmu() As String
        Get
            Return strHnskHenkouUmu
        End Get
        Set(ByVal value As String)
            strHnskHenkouUmu = value
        End Set
    End Property
#End Region

#Region "建物請求先コード"
    ''' <summary>
    ''' 建物請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKasiSeikyuuSakiCd As String
    ''' <summary>
    ''' 建物請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 建物請求先コード</returns>
    ''' <remarks></remarks>
    <TableMap("tatemono_seikyuu_saki_cd")> _
    Public Property KasiSeikyuuSakiCd() As String
        Get
            Return strKasiSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strKasiSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "建物請求先枝番"
    ''' <summary>
    ''' 建物請求先枝番
    ''' </summary>
    ''' <remarks></remarks>
    Private strKasiSeikyuuSakiBrc As String
    ''' <summary>
    ''' 建物請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns> 建物請求先枝番</returns>
    ''' <remarks></remarks>
    <TableMap("tatemono_seikyuu_saki_brc")> _
    Public Property KasiSeikyuuSakiBrc() As String
        Get
            Return strKasiSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strKasiSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "建物請求先区分"
    ''' <summary>
    ''' 建物請求先区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKasiSeikyuuSakiKbn As String
    ''' <summary>
    ''' 建物請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 建物請求先区分</returns>
    ''' <remarks></remarks>
    <TableMap("tatemono_seikyuu_saki_kbn")> _
    Public Property KasiSeikyuuSakiKbn() As String
        Get
            Return strKasiSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strKasiSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "建物請求先名"
    ''' <summary>
    ''' 建物請求先名
    ''' </summary>
    ''' <remarks></remarks>
    Private strKasiSeikyuuSakiMei As String
    ''' <summary>
    ''' 建物請求先名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 建物請求先名</returns>
    ''' <remarks></remarks>
    <TableMap("tatemono_seikyuu_saki_mei")> _
    Public Property KasiSeikyuuSakiMei() As String
        Get
            Return strKasiSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strKasiSeikyuuSakiMei = value
        End Set
    End Property
#End Region

#Region "建物請求先変更有無"
    ''' <summary>
    ''' 建物請求先変更有無
    ''' </summary>
    ''' <remarks></remarks>
    Private strKasiHenkouUmu As String
    ''' <summary>
    ''' 建物請求先変更有無
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("tatemono_henkou_umu")> _
    Public Property KasiHenkouUmu() As String
        Get
            Return strKasiHenkouUmu
        End Get
        Set(ByVal value As String)
            strKasiHenkouUmu = value
        End Set
    End Property
#End Region
End Class
