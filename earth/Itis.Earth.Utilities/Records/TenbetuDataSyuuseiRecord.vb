Public Class TenbetuDataSyuuseiRecord

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

#Region "区分名"
    ''' <summary>
    ''' 区分名
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbnName As String
    ''' <summary>
    ''' 区分名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 区分名</returns>
    ''' <remarks></remarks>
    <TableMap("kbn_mei")> _
    Public Property KbnName() As String
        Get
            Return strKbnName
        End Get
        Set(ByVal value As String)
            strKbnName = value
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
    ''' <returns> 加盟店名1</returns>
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

#Region "系列コード"
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuCd As String
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 系列コード</returns>
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

#Region "調査請求先"
    ''' <summary>
    ''' 調査請求先
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysSeikyuuSaki As String
    ''' <summary>
    ''' 調査請求先
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査請求先</returns>
    ''' <remarks></remarks>
    <TableMap("tys_seikyuu_saki")> _
    Public Property TysSeikyuuSaki() As String
        Get
            Return strTysSeikyuuSaki
        End Get
        Set(ByVal value As String)
            strTysSeikyuuSaki = value
        End Set
    End Property
#End Region

#Region "販促品請求先"
    ''' <summary>
    ''' 販促品請求先
    ''' </summary>
    ''' <remarks></remarks>
    Private strHansokuhinSeikyuusaki As String
    ''' <summary>
    ''' 販促品請求先
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品請求先</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuusaki")> _
    Public Property HansokuhinSeikyuusaki() As String
        Get
            Return strHansokuhinSeikyuusaki
        End Get
        Set(ByVal value As String)
            strHansokuhinSeikyuusaki = value
        End Set
    End Property
#End Region

#Region "登録料情報レコード"
    ''' <summary>
    ''' 登録料情報レコード
    ''' </summary>
    ''' <remarks></remarks>
    Private recTourokuRyouRecord As TenbetuTourokuRyouRecord
    ''' <summary>
    ''' 登録料情報レコード
    ''' </summary>
    ''' <value></value>
    ''' <returns>登録料情報レコード</returns>
    ''' <remarks></remarks>
    Public Property TourokuRyouRecord() As TenbetuTourokuRyouRecord
        Get
            Return recTourokuRyouRecord
        End Get
        Set(ByVal value As TenbetuTourokuRyouRecord)
            recTourokuRyouRecord = value
        End Set
    End Property
#End Region

#Region "販促品初期ツール料情報レコード"
    ''' <summary>
    ''' 販促品初期ツール料情報レコード
    ''' </summary>
    ''' <remarks></remarks>
    Private recToolRyouRecord As TenbetuToolRyouRecord
    ''' <summary>
    ''' 販促品初期ツール料情報レコード
    ''' </summary>
    ''' <value></value>
    ''' <returns>販促品初期ツール料情報レコード</returns>
    ''' <remarks></remarks>
    Public Property ToolRyouRecord() As TenbetuToolRyouRecord
        Get
            Return recToolRyouRecord
        End Get
        Set(ByVal value As TenbetuToolRyouRecord)
            recToolRyouRecord = value
        End Set
    End Property
#End Region

#Region "販促品情報レコードリスト"
    ''' <summary>
    ''' 販促品情報レコードリスト
    ''' </summary>
    ''' <remarks></remarks>
    Private recHansokuHinRecords As List(Of TenbetuHansokuHinRecord)
    ''' <summary>
    ''' 販促品情報レコードリスト
    ''' </summary>
    ''' <value></value>
    ''' <returns>販促品情報レコードリスト</returns>
    ''' <remarks></remarks>
    Public Property HansokuHinRecords() As List(Of TenbetuHansokuHinRecord)
        Get
            Return recHansokuHinRecords
        End Get
        Set(ByVal value As List(Of TenbetuHansokuHinRecord))
            recHansokuHinRecords = value
        End Set
    End Property
#End Region

End Class