<TableClassMap("t_tenbetu_syoki_seikyuu")> _
Public Class TenbetuSyokiSeikyuuRecord

#Region "店コード"
    ''' <summary>
    ''' 店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strMiseCd As String
    ''' <summary>
    ''' 店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 店コード</returns>
    ''' <remarks></remarks>
    <TableMap("mise_cd", IsInsert:=True, IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=5)> _
    Public Property MiseCd() As String
        Get
            Return strMiseCd
        End Get
        Set(ByVal value As String)
            strMiseCd = value
        End Set
    End Property
#End Region

#Region "分類コード"
    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strBunruiCd As String
    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 分類コード</returns>
    ''' <remarks></remarks>
    <TableMap("bunrui_cd", IsKey:=True, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
    Public Property BunruiCd() As String
        Get
            Return strBunruiCd
        End Get
        Set(ByVal value As String)
            strBunruiCd = value
        End Set
    End Property
#End Region

#Region "登録日"
    ''' <summary>
    ''' 登録日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDate As DateTime
    ''' <summary>
    ''' 登録日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日</returns>
    ''' <remarks></remarks>
    <TableMap("add_date", IsKey:=False, IsUpdate:=False, IsInsert:=True, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property AddDate() As DateTime
        Get
            Return dateAddDate
        End Get
        Set(ByVal value As DateTime)
            dateAddDate = value
        End Set
    End Property
#End Region

#Region "請求書発行日"
    ''' <summary>
    ''' 請求書発行日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuusyoHakDate As DateTime
    ''' <summary>
    ''' 請求書発行日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書発行日</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_hak_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property SeikyuusyoHakDate() As DateTime
        Get
            Return dateSeikyuusyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuusyoHakDate = value
        End Set
    End Property
#End Region

#Region "売上年月日"
    ''' <summary>
    ''' 売上年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriDate As DateTime
    ''' <summary>
    ''' 売上年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上年月日</returns>
    ''' <remarks></remarks>
    <TableMap("uri_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property UriDate() As DateTime
        Get
            Return dateUriDate
        End Get
        Set(ByVal value As DateTime)
            dateUriDate = value
        End Set
    End Property
#End Region

#Region "伝票売上年月日"
    ''' <summary>
    ''' 伝票売上年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDenUriDate As DateTime
    ''' <summary>
    ''' 伝票売上年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票売上年月日</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_uri_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property DenpyouUriDate() As DateTime
        Get
            Return dateDenUriDate
        End Get
        Set(ByVal value As DateTime)
            dateDenUriDate = value
        End Set
    End Property
#End Region

#Region "請求有無"
    ''' <summary>
    ''' 請求有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intSeikyuuUmu As Integer
    ''' <summary>
    ''' 請求有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求有無</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_umu", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property SeikyuuUmu() As Integer
        Get
            Return intSeikyuuUmu
        End Get
        Set(ByVal value As Integer)
            intSeikyuuUmu = value
        End Set
    End Property
#End Region

#Region "売上計上FLG"
    ''' <summary>
    ''' 売上計上FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriKeijyouFlg As Integer
    ''' <summary>
    ''' 売上計上FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上計上FLG</returns>
    ''' <remarks></remarks>
    <TableMap("uri_keijyou_flg", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property UriKeijyouFlg() As Integer
        Get
            Return intUriKeijyouFlg
        End Get
        Set(ByVal value As Integer)
            intUriKeijyouFlg = value
        End Set
    End Property
#End Region

#Region "売上計上日"
    ''' <summary>
    ''' 売上計上日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriKeijyouDate As DateTime
    ''' <summary>
    ''' 売上計上日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上計上日</returns>
    ''' <remarks></remarks>
    <TableMap("uri_keijyou_date", IsKey:=False, IsUpdate:=True, IsInsert:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property UriKeijyouDate() As DateTime
        Get
            Return dateUriKeijyouDate
        End Get
        Set(ByVal value As DateTime)
            dateUriKeijyouDate = value
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
    <TableMap("syouhin_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

#Region "売上金額"
    ''' <summary>
    ''' 売上金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriGaku As Integer
    ''' <summary>
    ''' 売上金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上金額</returns>
    ''' <remarks></remarks>
    <TableMap("uri_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property UriGaku() As Integer
        Get
            Return intUriGaku
        End Get
        Set(ByVal value As Integer)
            intUriGaku = value
        End Set
    End Property
#End Region

#Region "消費税額"
    ''' <summary>
    ''' 消費税額
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouhiZeiGaku As Integer
    ''' <summary>
    ''' 消費税額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("syouhizei_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property SyouhiZeiGaku() As Integer
        Get
            Return intSyouhiZeiGaku
        End Get
        Set(ByVal value As Integer)
            intSyouhiZeiGaku = value
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
    ''' <returns> 税区分</returns>
    ''' <remarks></remarks>
    <TableMap("zei_kbn", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=1)> _
    Public Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
        End Set
    End Property
#End Region

#Region "備考"
    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou As String
    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <value></value>
    ''' <returns> 備考</returns>
    ''' <remarks></remarks>
    <TableMap("bikou", IsKey:=False, IsUpdate:=True, IsInsert:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "工務店請求金額"
    ''' <summary>
    ''' 工務店請求金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoumutenSeikyuuGaku As Integer
    ''' <summary>
    ''' 工務店請求金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工務店請求金額</returns>
    ''' <remarks></remarks>
    <TableMap("koumuten_seikyuu_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property KoumutenSeikyuuGaku() As Integer
        Get
            Return intKoumutenSeikyuuGaku
        End Get
        Set(ByVal value As Integer)
            intKoumutenSeikyuuGaku = value
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
    <TableMap("add_login_user_id", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, IsInsert:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, IsInsert:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "SQL種別判断フラグ"
    ''' <summary>
    ''' SQL種別判断フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intSqlTypeFlg As Integer
    ''' <summary>
    ''' SQL種別判断フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns>SQL種別判断フラグ</returns>
    ''' <remarks></remarks>
    Public Property SqlTypeFlg() As Integer
        Get
            Return intSqlTypeFlg
        End Get
        Set(ByVal value As Integer)
            intSqlTypeFlg = value
        End Set
    End Property

#End Region

#Region "税込金額（与信チェック用）"
    ''' <summary>
    ''' 税込金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intZeikomiGaku As Integer
    ''' <summary>
    ''' 税込金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>税込金額</returns>
    ''' <remarks></remarks>
    Public Property ZeikomiGaku() As Integer
        Get
            Return intZeikomiGaku
        End Get
        Set(ByVal value As Integer)
            intZeikomiGaku = value
        End Set
    End Property
#End Region

End Class