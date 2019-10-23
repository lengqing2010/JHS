Public Class YosinCheckTaisyouKanriRecord

#Region "処理ID"
    ''' <summary>
    ''' 処理ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyoriId As String
    ''' <summary>
    ''' 処理ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 処理ID</returns>
    ''' <remarks></remarks>
    <TableMap("syori_id")> _
    Public Property SyoriId() As String
        Get
            Return strSyoriId
        End Get
        Set(ByVal value As String)
            strSyoriId = value
        End Set
    End Property
#End Region

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

#Region "保証書NO"
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no")> _
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
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

#Region "名寄先コード"
    ''' <summary>
    ''' 名寄先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strNayoseSakiCd As String
    ''' <summary>
    ''' 名寄先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 名寄先コード</returns>
    ''' <remarks></remarks>
    <TableMap("nayose_saki_cd")> _
    Public Property NayoseSakiCd() As String
        Get
            Return strNayoseSakiCd
        End Get
        Set(ByVal value As String)
            strNayoseSakiCd = value
        End Set
    End Property
#End Region

#Region "与信管理更新ログインユーザーID"
    ''' <summary>
    ''' 与信管理更新ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strMYosinkanriUpdLoginUserId As String
    ''' <summary>
    ''' 与信管理更新ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 与信管理更新ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("m_yosinkanri_upd_login_user_id")> _
    Public Property MYosinkanriUpdLoginUserId() As String
        Get
            Return strMYosinkanriUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strMYosinkanriUpdLoginUserId = value
        End Set
    End Property
#End Region

#Region "与信管理更新日時"
    ''' <summary>
    ''' 与信管理更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateMYosinkanriUpdDatetime As DateTime
    ''' <summary>
    ''' 与信管理更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 与信管理更新日時</returns>
    ''' <remarks></remarks>
    <TableMap("m_yosinkanri_upd_datetime")> _
    Public Property MYosinkanriUpdDatetime() As DateTime
        Get
            Return dateMYosinkanriUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateMYosinkanriUpdDatetime = value
        End Set
    End Property
#End Region

#Region "調査売上金額合計"
    ''' <summary>
    ''' 調査売上金額合計
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysUriGakuGoukei As String
    ''' <summary>
    ''' 調査売上金額合計
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査売上金額合計</returns>
    ''' <remarks></remarks>
    <TableMap("tys_uri_gaku_goukei")> _
    Public Property TysUriGakuGoukei() As String
        Get
            Return strTysUriGakuGoukei
        End Get
        Set(ByVal value As String)
            strTysUriGakuGoukei = value
        End Set
    End Property
#End Region

#Region "与信チェック処理ステータス"
    ''' <summary>
    ''' 与信チェック処理ステータス
    ''' </summary>
    ''' <remarks></remarks>
    Private intYosinCheckSyoriSts As Integer
    ''' <summary>
    ''' 与信チェック処理ステータス
    ''' </summary>
    ''' <value></value>
    ''' <returns> 与信チェック処理ステータス</returns>
    ''' <remarks></remarks>
    <TableMap("yosin_check_syori_sts")> _
    Public Property YosinCheckSyoriSts() As Integer
        Get
            Return intYosinCheckSyoriSts
        End Get
        Set(ByVal value As Integer)
            intYosinCheckSyoriSts = value
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