Public Class KaikakeDataRecord

#Region "対象年月"
    ''' <summary>
    ''' 対象年月
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTaisyouNengetu As DateTime
    ''' <summary>
    ''' 対象年月
    ''' </summary>
    ''' <value></value>
    ''' <returns> 対象年月</returns>
    ''' <remarks></remarks>
    <TableMap("taisyou_nengetu")> _
    Public Property TaisyouNengetu() As DateTime
        Get
            Return dateTaisyouNengetu
        End Get
        Set(ByVal value As DateTime)
            dateTaisyouNengetu = value
        End Set
    End Property
#End Region

#Region "調査会社コード"
    ''' <summary>
    ''' 調査会社コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' 調査会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社コード</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd")> _
    Public Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "支払集計先事業所コード"
    ''' <summary>
    ''' 支払集計先事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriJigyousyoCd As String
    ''' <summary>
    ''' 支払集計先事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 支払集計先事業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("shri_jigyousyo_cd")> _
    Public Property ShriJigyousyoCd() As String
        Get
            Return strShriJigyousyoCd
        End Get
        Set(ByVal value As String)
            strShriJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "当月繰越残高"
    ''' <summary>
    ''' 当月繰越残高
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTougetuKurikosiZan As Long
    ''' <summary>
    ''' 当月繰越残高
    ''' </summary>
    ''' <value></value>
    ''' <returns> 当月繰越残高</returns>
    ''' <remarks></remarks>
    <TableMap("tougetu_kurikosi_zan")> _
    Public Property TougetuKurikosiZan() As Long
        Get
            Return lngTougetuKurikosiZan
        End Get
        Set(ByVal value As Long)
            lngTougetuKurikosiZan = value
        End Set
    End Property
#End Region

#Region "当月支払額 [振込]"
    ''' <summary>
    ''' 当月支払額 [振込]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngFurikomi As Long
    ''' <summary>
    ''' 当月支払額 [振込]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 当月支払額 [振込]</returns>
    ''' <remarks></remarks>
    <TableMap("furikomi")> _
    Public Property Furikomi() As Long
        Get
            Return lngFurikomi
        End Get
        Set(ByVal value As Long)
            lngFurikomi = value
        End Set
    End Property
#End Region

#Region "当月支払額 [相殺]"
    ''' <summary>
    ''' 当月支払額 [相殺]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSousai As Long
    ''' <summary>
    ''' 当月支払額 [相殺]
    ''' </summary>
    ''' <value></value>
    ''' <returns> 当月支払額 [相殺]</returns>
    ''' <remarks></remarks>
    <TableMap("sousai")> _
    Public Property Sousai() As Long
        Get
            Return lngSousai
        End Get
        Set(ByVal value As Long)
            lngSousai = value
        End Set
    End Property
#End Region

#Region "当月支払合計"
    ''' <summary>
    ''' 当月支払合計
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTougetuShriGoukei As Long
    ''' <summary>
    ''' 当月支払合計
    ''' </summary>
    ''' <value></value>
    ''' <returns> 当月支払合計</returns>
    ''' <remarks></remarks>
    <TableMap("tougetu_shri_goukei")> _
    Public Property TougetuShriGoukei() As Long
        Get
            Return lngTougetuShriGoukei
        End Get
        Set(ByVal value As Long)
            lngTougetuShriGoukei = value
        End Set
    End Property
#End Region

#Region "当月仕入等"
    ''' <summary>
    ''' 当月仕入等
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTougetuSiireNado As Long
    ''' <summary>
    ''' 当月仕入等
    ''' </summary>
    ''' <value></value>
    ''' <returns> 当月仕入等</returns>
    ''' <remarks></remarks>
    <TableMap("tougetu_siire_nado")> _
    Public Property TougetuSiireNado() As Long
        Get
            Return lngTougetuSiireNado
        End Get
        Set(ByVal value As Long)
            lngTougetuSiireNado = value
        End Set
    End Property
#End Region

#Region "当月消費税等"
    ''' <summary>
    ''' 当月消費税等
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTougetuZeiNado As Long
    ''' <summary>
    ''' 当月消費税等
    ''' </summary>
    ''' <value></value>
    ''' <returns> 当月消費税等</returns>
    ''' <remarks></remarks>
    <TableMap("tougetu_zei_nado")> _
    Public Property TougetuZeiNado() As Long
        Get
            Return lngTougetuZeiNado
        End Get
        Set(ByVal value As Long)
            lngTougetuZeiNado = value
        End Set
    End Property
#End Region

#Region "登録ログインユーザーID"
    ''' <summary>
    ''' 登録ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String
    ''' <summary>
    ''' 登録ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id")> _
    Public Property AddLoginUserId() As String
        Get
            Return strAddLoginUserId
        End Get
        Set(ByVal value As String)
            strAddLoginUserId = value
        End Set
    End Property
#End Region

#Region "登録日時"
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetime As DateTime
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime")> _
    Public Property AddDatetime() As DateTime
        Get
            Return dateAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetime = value
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
    <TableMap("upd_login_user_id")> _
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
    <TableMap("upd_datetime")> _
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