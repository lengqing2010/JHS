''' <summary>
''' 与信チェック用店別データ
''' </summary>
''' <remarks></remarks>
Public Class YosinTenbetuRecord

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
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "加盟店ｺｰﾄﾞ"
    ''' <summary>
    ''' 加盟店ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' 加盟店ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店ｺｰﾄﾞ</returns>
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

#Region "登録手数料売上金額"
    ''' <summary>
    ''' 登録手数料売上金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intTourokuTesuuryouUriGaku As Integer
    ''' <summary>
    ''' 登録手数料売上金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録手数料売上金額</returns>
    ''' <remarks></remarks>
    Public Property TourokuTesuuryouUriGaku() As Integer
        Get
            Return intTourokuTesuuryouUriGaku
        End Get
        Set(ByVal value As Integer)
            intTourokuTesuuryouUriGaku = value
        End Set
    End Property
#End Region

#Region "初期ツール料売上金額"
    ''' <summary>
    ''' 初期ツール料売上金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyokiToolRyouUriGaku As Integer
    ''' <summary>
    ''' 初期ツール料売上金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 初期ツール料売上金額</returns>
    ''' <remarks></remarks>
    Public Property SyokiToolRyouUriGaku() As Integer
        Get
            Return intSyokiToolRyouUriGaku
        End Get
        Set(ByVal value As Integer)
            intSyokiToolRyouUriGaku = value
        End Set
    End Property
#End Region

#Region "販促品合計金額(単価*数量をレコード分)"
    ''' <summary>
    ''' 販促品合計金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intHansokuGoukei As Long
    ''' <summary>
    ''' 販促品合計金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品合計金額</returns>
    ''' <remarks></remarks>
    Public Property HansokuGoukei() As Long
        Get
            Return intHansokuGoukei
        End Get
        Set(ByVal value As Long)
            intHansokuGoukei = value
        End Set
    End Property
#End Region

#Region "販促品合計金額_工務店(工務店請求単価*数量をレコード分)"
    ''' <summary>
    ''' 販促品合計金額_工務店
    ''' </summary>
    ''' <remarks></remarks>
    Private intHansokuGoukeiKoumuten As Long
    ''' <summary>
    ''' 販促品合計金額_工務店
    ''' </summary>
    ''' <value></value>
    ''' <returns> 販促品合計金額_工務店</returns>
    ''' <remarks></remarks>
    Public Property HansokuGoukeiKoumuten() As Long
        Get
            Return intHansokuGoukeiKoumuten
        End Get
        Set(ByVal value As Long)
            intHansokuGoukeiKoumuten = value
        End Set
    End Property
#End Region

#Region "更新ログインユーザーID"
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新ログインユーザーID</returns>
    ''' <remarks></remarks>
    Public Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

#Region "更新日時"
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

End Class
