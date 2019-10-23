''' <summary>
''' 加盟店商品調査方法特別対応マスタレコードクラス
''' </summary>
''' <remarks>加盟店商品調査方法特別対応マスタの格納時に使用します</remarks>
<TableClassMap("m_kamei_syouhin_tys_tokubetu_taiou")> _
Public Class KameiTokubetuTaiouRecord

#Region "相手先種別"
    ''' <summary>
    ''' 相手先種別
    ''' </summary>
    ''' <remarks></remarks>
    Private intAitesakiSyubetu As Integer = Integer.MinValue
    ''' <summary>
    ''' 相手先種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 相手先種別</returns>
    ''' <remarks></remarks>
    <TableMap("aitesaki_syubetu")> _
    Public Property AitesakiSyubetu() As Integer
        Get
            Return intAitesakiSyubetu
        End Get
        Set(ByVal value As Integer)
            intAitesakiSyubetu = value
        End Set
    End Property
#End Region

#Region "相手先コード"
    ''' <summary>
    ''' 相手先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strAitesakiCd As String = String.Empty
    ''' <summary>
    ''' 相手先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 相手先コード</returns>
    ''' <remarks></remarks>
    <TableMap("aitesaki_cd")> _
    Public Property AitesakiCd() As String
        Get
            Return strAitesakiCd
        End Get
        Set(ByVal value As String)
            strAitesakiCd = value
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

#Region "調査方法NO"
    ''' <summary>
    ''' 調査方法NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysHouhouNo As Integer = Integer.MinValue
    ''' <summary>
    ''' 調査方法NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査方法NO</returns>
    ''' <remarks></remarks>
    <TableMap("tys_houhou_no")> _
    Public Property TysHouhouNo() As Integer
        Get
            Return intTysHouhouNo
        End Get
        Set(ByVal value As Integer)
            intTysHouhouNo = value
        End Set
    End Property
#End Region

#Region "特別対応コード"
    ''' <summary>
    ''' 特別対応コード
    ''' </summary>
    ''' <remarks></remarks>
    Private intTokubetuTaiouCd As Integer = Integer.MinValue
    ''' <summary>
    ''' 特別対応コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 特別対応コード</returns>
    ''' <remarks></remarks>
    <TableMap("tokubetu_taiou_cd")> _
    Public Property TokubetuTaiouCd() As Integer
        Get
            Return intTokubetuTaiouCd
        End Get
        Set(ByVal value As Integer)
            intTokubetuTaiouCd = value
        End Set
    End Property
#End Region

#Region "取消"
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer = Integer.MinValue
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

#Region "金額加算商品コード"
    ''' <summary>
    ''' 金額加算商品コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKasanSyouhinCd As String = String.Empty
    ''' <summary>
    ''' 金額加算商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 金額加算商品コード</returns>
    ''' <remarks></remarks>
    <TableMap("kasan_syouhin_cd")> _
    Public Property KasanSyouhinCd() As String
        Get
            Return strKasanSyouhinCd
        End Get
        Set(ByVal value As String)
            strKasanSyouhinCd = value
        End Set
    End Property
#End Region

#Region "初期値"
    ''' <summary>
    ''' 初期値
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyokiti As Integer = 0
    ''' <summary>
    ''' 初期値
    ''' </summary>
    ''' <value></value>
    ''' <returns> 初期値</returns>
    ''' <remarks></remarks>
    <TableMap("syokiti")> _
    Public Property Syokiti() As Integer
        Get
            Return intSyokiti
        End Get
        Set(ByVal value As Integer)
            intSyokiti = value
        End Set
    End Property
#End Region

#Region "実請求加算金額"
    ''' <summary>
    ''' 実請求加算金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriKasanGaku As Integer = 0
    ''' <summary>
    ''' 実請求加算金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 実請求加算金額</returns>
    ''' <remarks></remarks>
    <TableMap("uri_kasan_gaku")> _
    Public Property UriKasanGaku() As Integer
        Get
            Return intUriKasanGaku
        End Get
        Set(ByVal value As Integer)
            intUriKasanGaku = value
        End Set
    End Property
#End Region

#Region "工務店請求加算金額"
    ''' <summary>
    ''' 工務店請求加算金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoumutenKasanGaku As Integer = 0
    ''' <summary>
    ''' 工務店請求加算金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工務店請求加算金額</returns>
    ''' <remarks></remarks>
    <TableMap("koumuten_kasan_gaku")> _
    Public Property KoumutenKasanGaku() As Integer
        Get
            Return intKoumutenKasanGaku
        End Get
        Set(ByVal value As Integer)
            intKoumutenKasanGaku = value
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

#Region "各種メソッド"
#Region "チェック状況の判定"
    ''' <summary>
    ''' チェック状況の判定
    ''' </summary>
    ''' <remarks></remarks>
    Private blnChk As Boolean = False
    ''' <summary>
    ''' チェック状況の判定
    ''' </summary>
    ''' <value></value>
    ''' <returns>チェック状況の判定</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property HanteiCheck() As Boolean
        Get
            If AitesakiSyubetu <> Integer.MinValue _
                AndAlso AitesakiCd <> String.Empty _
                    AndAlso SyouhinCd <> String.Empty _
                        AndAlso TysHouhouNo <> Integer.MinValue Then
                blnChk = True
            End If
            Return blnChk
        End Get
    End Property
#End Region
#End Region

#Region "特別対応マスタの項目"
#Region "特別対応コード"
    ''' <summary>
    ''' 特別対応コード
    ''' </summary>
    ''' <remarks></remarks>
    Private mIntTokubetuTaiouCd As Integer = Integer.MinValue
    ''' <summary>
    ''' 特別対応コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 特別対応コード</returns>
    ''' <remarks></remarks>
    <TableMap("m_tokubetu_taiou_cd")> _
    Public Property mTokubetuTaiouCd() As Integer
        Get
            Return mIntTokubetuTaiouCd
        End Get
        Set(ByVal value As Integer)
            mIntTokubetuTaiouCd = value
        End Set
    End Property
#End Region

#Region "取消"
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <remarks></remarks>
    Private mIntTorikesi As Integer = Integer.MinValue
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取消</returns>
    ''' <remarks></remarks>
    <TableMap("m_torikesi")> _
    Public Property mTorikesi() As Integer
        Get
            Return mIntTorikesi
        End Get
        Set(ByVal value As Integer)
            mIntTorikesi = value
        End Set
    End Property
#End Region

#Region "特別対応名称"
    ''' <summary>
    ''' 特別対応名称
    ''' </summary>
    ''' <remarks></remarks>
    Private strTokubetuTaiouMeisyou As String = String.Empty
    ''' <summary>
    ''' 特別対応名称
    ''' </summary>
    ''' <value></value>
    ''' <returns> 特別対応名称</returns>
    ''' <remarks></remarks>
    <TableMap("tokubetu_taiou_meisyou")> _
    Public Property TokubetuTaiouMeisyou() As String
        Get
            Return strTokubetuTaiouMeisyou
        End Get
        Set(ByVal value As String)
            strTokubetuTaiouMeisyou = value
        End Set
    End Property
#End Region
#End Region

End Class