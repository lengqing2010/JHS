''' <summary>
''' 汎用売上データテーブルのレコードクラスです
''' </summary>
''' <remarks></remarks>
<TableClassMap("t_hannyou_uriage")> _
Public Class HannyouUriageRecord

#Region "汎用売上ユニークNO"
    ''' <summary>
    ''' 汎用売上ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intHanUriUnqNo As Integer = 0
    ''' <summary>
    ''' 汎用売上ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns>汎用売上ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("han_uri_unique_no", IsKey:=True, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property HanUriUnqNo() As Integer
        Get
            Return intHanUriUnqNo
        End Get
        Set(ByVal value As Integer)
            intHanUriUnqNo = value
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
    ''' <returns>取消</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "摘要"
    ''' <summary>
    ''' 摘要
    ''' </summary>
    ''' <remarks></remarks>
    Private strTekiyou As String
    ''' <summary>
    ''' 摘要
    ''' </summary>
    ''' <value></value>
    ''' <returns>摘要</returns>
    ''' <remarks></remarks>
    <TableMap("tekiyou", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=512)> _
    Public Property Tekiyou() As String
        Get
            Return strTekiyou
        End Get
        Set(ByVal value As String)
            strTekiyou = value
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
    <TableMap("uri_date", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    Private dateDenpyouUriDate As DateTime
    ''' <summary>
    ''' 伝票売上年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票売上年月日</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_uri_date", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property DenpyouUriDate() As DateTime
        Get
            Return dateDenpyouUriDate
        End Get
        Set(ByVal value As DateTime)
            dateDenpyouUriDate = value
        End Set
    End Property
#End Region

#Region "請求年月日"
    ''' <summary>
    ''' 請求年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuuDate As DateTime
    ''' <summary>
    ''' 請求年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求年月日</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_date", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property SeikyuuDate() As DateTime
        Get
            Return dateSeikyuuDate
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuuDate = value
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
    <TableMap("seikyuu_saki_cd", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
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
    <TableMap("seikyuu_saki_brc", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
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
    <TableMap("seikyuu_saki_kbn", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Char, SqlLength:=1)> _
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
    ''' <returns> 請求先名</returns>
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

#Region "品名"
    ''' <summary>
    ''' 品名
    ''' </summary>
    ''' <remarks></remarks>
    Private strHinmei As String
    ''' <summary>
    ''' 品名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 品名</returns>
    ''' <remarks></remarks>
    <TableMap("hin_mei", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Property Hinmei() As String
        Get
            Return strHinmei
        End Get
        Set(ByVal value As String)
            strHinmei = value
        End Set
    End Property
#End Region

#Region "数量"
    ''' <summary>
    ''' 数量
    ''' </summary>
    ''' <remarks></remarks>
    Private intSuu As Integer = Integer.MinValue
    ''' <summary>
    ''' 数量
    ''' </summary>
    ''' <value></value>
    ''' <returns> 数量</returns>
    ''' <remarks></remarks>
    <TableMap("suu", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Suu() As Integer
        Get
            Return intSuu
        End Get
        Set(ByVal value As Integer)
            intSuu = value
        End Set
    End Property
#End Region

#Region "単価"
    ''' <summary>
    ''' 単価
    ''' </summary>
    ''' <remarks></remarks>
    Private intTanka As Integer = Integer.MinValue
    ''' <summary>
    ''' 単価
    ''' </summary>
    ''' <value></value>
    ''' <returns> 単価</returns>
    ''' <remarks></remarks>
    <TableMap("tanka", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Tanka() As Integer
        Get
            Return intTanka
        End Get
        Set(ByVal value As Integer)
            intTanka = value
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
    <TableMap("zei_kbn", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=1)> _
    Public Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
        End Set
    End Property
#End Region

#Region "税率"
    ''' <summary>
    ''' 税率
    ''' </summary>
    ''' <remarks></remarks>
    Private decZeiritu As Decimal = 0
    ''' <summary>
    ''' 税率
    ''' </summary>
    ''' <value></value>
    ''' <returns> 税率</returns>
    ''' <remarks></remarks>
    <TableMap("zeiritu")> _
    Public Property Zeiritu() As Decimal
        Get
            Return decZeiritu
        End Get
        Set(ByVal value As Decimal)
            decZeiritu = value
        End Set
    End Property
#End Region

#Region "消費税額"
    ''' <summary>
    ''' 消費税額
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouhiZeiGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' 消費税額
    ''' </summary>
    ''' <value></value>
    ''' <returns>消費税額</returns>
    ''' <remarks></remarks>
    <TableMap("syouhizei_gaku", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property SyouhiZeiGaku() As Integer
        Get
            Return intSyouhiZeiGaku
        End Get
        Set(ByVal value As Integer)
            intSyouhiZeiGaku = value
        End Set
    End Property
#End Region

#Region "売上金額[単価*数量*税率]"
    ''' <summary>
    ''' 売上金額[単価*数量*税率]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngUriGaku As Long = 0
    ''' <summary>
    ''' 売上金額[単価*数量*税率]
    ''' </summary>
    ''' <value></value>
    ''' <returns>売上金額[単価*数量*税率]</returns>
    ''' <remarks></remarks>
    Public Property UriGaku() As Long
        Get
            Dim tmpUriGaku As Long
            Dim tmpTanka As Long = Long.Parse(Tanka)
            Dim tmpSuu As Long = Long.Parse(Suu)

            If Suu = Integer.MinValue _
                OrElse Tanka = Integer.MinValue Then
                tmpUriGaku = 0
            Else
                tmpUriGaku = tmpSuu * tmpTanka '数量*単価
            End If
            If SyouhiZeiGaku = Integer.MinValue Then
                tmpUriGaku = Fix(tmpUriGaku * (1 + Zeiritu)) '1+税率
            Else
                tmpUriGaku = Fix(tmpUriGaku + SyouhiZeiGaku) '消費税額
            End If

            Return tmpUriGaku
        End Get
        Set(ByVal value As Long)
            lngUriGaku = value
        End Set
    End Property
#End Region

#Region "売上計上FLG"
    ''' <summary>
    ''' 売上計上FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriKeijyouFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 売上計上FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上計上FLG</returns>
    ''' <remarks></remarks>
    <TableMap("uri_keijyou_flg", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    Private dateUriKeijyouDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 売上計上日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上計上日</returns>
    ''' <remarks></remarks>
    <TableMap("uri_keijyou_date", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property UriKeijyouDate() As DateTime
        Get
            Return dateUriKeijyouDate
        End Get
        Set(ByVal value As DateTime)
            dateUriKeijyouDate = value
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
    <TableMap("add_login_user_id", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Property AddLoginUserId() As String
        Get
            Return strAddLoginUserId
        End Get
        Set(ByVal value As String)
            strAddLoginUserId = value
        End Set
    End Property
#End Region

#Region "登録ログインユーザー名"
    ''' <summary>
    ''' 登録ログインユーザー名
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserName As String
    ''' <summary>
    ''' 登録ログインユーザー名
    ''' </summary>
    ''' <value></value>
    ''' <returns>登録ログインユーザー名</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_name", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=128)> _
    Public Property AddLoginUserName() As String
        Get
            Return strAddLoginUserName
        End Get
        Set(ByVal value As String)
            strAddLoginUserName = value
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
    <TableMap("add_datetime", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("upd_login_user_id", IsKey:=False, IsInsert:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

#Region "更新ログインユーザー名"
    ''' <summary>
    ''' 更新ログインユーザー名
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserName As String
    ''' <summary>
    ''' 更新ログインユーザー名
    ''' </summary>
    ''' <value></value>
    ''' <returns>更新ログインユーザー名</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_name", IsKey:=False, IsInsert:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=128)> _
    Public Property UpdLoginUserName() As String
        Get
            Return strUpdLoginUserName
        End Get
        Set(ByVal value As String)
            strUpdLoginUserName = value
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
    <TableMap("upd_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
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
    <TableMap("kbn", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "番号"
    ''' <summary>
    ''' 番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strBangou As String
    ''' <summary>
    ''' 番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 番号</returns>
    ''' <remarks></remarks>
    <TableMap("bangou", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Property Bangou() As String
        Get
            Return strBangou
        End Get
        Set(ByVal value As String)
            strBangou = value
        End Set
    End Property
#End Region

#Region "施主名"
    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String
    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 施主名</returns>
    ''' <remarks></remarks>
    <TableMap("sesyu_mei", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=50)> _
    Public Property SesyuMei() As String
        Get
            Return strSesyuMei
        End Get
        Set(ByVal value As String)
            strSesyuMei = value
        End Set
    End Property
#End Region

#Region "売上店区分"
    ''' <summary>
    ''' 売上店区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strUriageTenKbn As String
    ''' <summary>
    ''' 売上店区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上店区分</returns>
    ''' <remarks></remarks>
    <TableMap("uriage_ten_kbn", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Property UriageTenKbn() As String
        Get
            Return strUriageTenKbn
        End Get
        Set(ByVal value As String)
            strUriageTenKbn = value
        End Set
    End Property
#End Region

#Region "売上店コード"
    ''' <summary>
    ''' 売上店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strUriageTenCd As String
    ''' <summary>
    ''' 売上店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上店コード</returns>
    ''' <remarks></remarks>
    <TableMap("uriage_ten_cd", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=7)> _
    Public Property UriageTenCd() As String
        Get
            Return strUriageTenCd
        End Get
        Set(ByVal value As String)
            strUriageTenCd = value
        End Set
    End Property
#End Region


End Class