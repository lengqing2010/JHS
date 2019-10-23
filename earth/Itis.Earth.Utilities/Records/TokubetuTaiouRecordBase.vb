''' <summary>
''' 特別対応汎用レコードクラス
''' </summary>
''' <remarks>特別対応画面表示用のデータ格納時に使用します</remarks>
Public Class TokubetuTaiouRecordBase
#Region "特別対応データの項目"

#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String = String.Empty
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 区分</returns>
    ''' <remarks></remarks>
    <TableMap("kbn", IsKey:=True, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Overridable Property Kbn() As String
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
    Private strHosyousyoNo As String = String.Empty
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no", IsKey:=True, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
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
    <TableMap("tokubetu_taiou_cd", IsKey:=True, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property TokubetuTaiouCd() As Integer
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
    <TableMap("torikesi", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "分類コード"
    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strBunruiCd As String = String.Empty
    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 分類コード</returns>
    ''' <remarks></remarks>
    <TableMap("bunrui_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
    Public Overridable Property BunruiCd() As String
        Get
            Return strBunruiCd
        End Get
        Set(ByVal value As String)
            strBunruiCd = value
        End Set
    End Property
#End Region

#Region "画面表示NO"
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intGamenHyoujiNo As Integer = 0
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 画面表示NO</returns>
    ''' <remarks></remarks>
    <TableMap("gamen_hyouji_no", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property GamenHyoujiNo() As Integer
        Get
            Return intGamenHyoujiNo
        End Get
        Set(ByVal value As Integer)
            intGamenHyoujiNo = value
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
    <TableMap("kasan_syouhin_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Overridable Property KasanSyouhinCd() As String
        Get
            Return strKasanSyouhinCd
        End Get
        Set(ByVal value As String)
            strKasanSyouhinCd = value
        End Set
    End Property
#End Region

#Region "工務店請求加算金額"
    ''' <summary>
    ''' 工務店請求加算金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoumutenKasanGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' 工務店請求加算金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工務店請求加算金額</returns>
    ''' <remarks></remarks>
    <TableMap("koumuten_kasan_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KoumutenKasanGaku() As Integer
        Get
            Return intKoumutenKasanGaku
        End Get
        Set(ByVal value As Integer)
            intKoumutenKasanGaku = value
        End Set
    End Property
#End Region

#Region "実請求加算金額"
    ''' <summary>
    ''' 実請求加算金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriKasanGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' 実請求加算金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 実請求加算金額</returns>
    ''' <remarks></remarks>
    <TableMap("uri_kasan_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property UriKasanGaku() As Integer
        Get
            Return intUriKasanGaku
        End Get
        Set(ByVal value As Integer)
            intUriKasanGaku = value
        End Set
    End Property
#End Region

#Region "金額加算商品コードOld"
    ''' <summary>
    ''' 金額加算商品コードOld
    ''' </summary>
    ''' <remarks></remarks>
    Private strKasanSyouhinCdOld As String = String.Empty
    ''' <summary>
    ''' 金額加算商品コードOld
    ''' </summary>
    ''' <value></value>
    ''' <returns> 金額加算商品コードOld</returns>
    ''' <remarks></remarks>
    <TableMap("kasan_syouhin_cd_old", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Overridable Property KasanSyouhinCdOld() As String
        Get
            Return strKasanSyouhinCdOld
        End Get
        Set(ByVal value As String)
            strKasanSyouhinCdOld = value
        End Set
    End Property
#End Region

#Region "工務店請求加算金額Old"
    ''' <summary>
    ''' 工務店請求加算金額Old
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoumutenKasanGakuOld As Integer = Integer.MinValue
    ''' <summary>
    ''' 工務店請求加算金額Old
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工務店請求加算金額Old</returns>
    ''' <remarks></remarks>
    <TableMap("koumuten_kasan_gaku_old", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KoumutenKasanGakuOld() As Integer
        Get
            Return intKoumutenKasanGakuOld
        End Get
        Set(ByVal value As Integer)
            intKoumutenKasanGakuOld = value
        End Set
    End Property
#End Region

#Region "実請求加算金額Old"
    ''' <summary>
    ''' 実請求加算金額Old
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriKasanGakuOld As Integer = Integer.MinValue
    ''' <summary>
    ''' 実請求加算金額Old
    ''' </summary>
    ''' <value></value>
    ''' <returns> 実請求加算金額Old</returns>
    ''' <remarks></remarks>
    <TableMap("uri_kasan_gaku_old", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property UriKasanGakuOld() As Integer
        Get
            Return intUriKasanGakuOld
        End Get
        Set(ByVal value As Integer)
            intUriKasanGakuOld = value
        End Set
    End Property
#End Region

#Region "価格処理フラグ"
    ''' <summary>
    ''' 価格処理フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intKkkSyoriFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 価格処理フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 価格処理フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("kkk_syori_flg", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KkkSyoriFlg() As Integer
        Get
            Return intKkkSyoriFlg
        End Get
        Set(ByVal value As Integer)
            intKkkSyoriFlg = value
        End Set
    End Property
#End Region

#Region "更新フラグ"
    ''' <summary>
    ''' 更新フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private intUpdFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 更新フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新フラグ</returns>
    ''' <remarks></remarks>
    <TableMap("upd_flg", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property UpdFlg() As Integer
        Get
            Return intUpdFlg
        End Get
        Set(ByVal value As Integer)
            intUpdFlg = value
        End Set
    End Property
#End Region

#Region "登録ログインユーザーID"
    ''' <summary>
    ''' 登録ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String = String.Empty
    ''' <summary>
    ''' 登録ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overridable Property AddLoginUserId() As String
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
    Private dateAddDatetime As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property AddDatetime() As DateTime
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
    Private strUpdLoginUserId As String = String.Empty
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overridable Property UpdLoginUserId() As String
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
    Private dateUpdDatetime As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
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
    <TableMap("m_tokubetu_taiou_meisyou")> _
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

#Region "加盟店商品調査方法特別対応マスタの項目"

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
    <TableMap("k_aitesaki_syubetu")> _
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
    <TableMap("k_aitesaki_cd")> _
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
    <TableMap("k_syouhin_cd")> _
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
    <TableMap("k_tys_houhou_no")> _
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
    Private kIntTokubetuTaiouCd As Integer = Integer.MinValue
    ''' <summary>
    ''' 特別対応コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 特別対応コード</returns>
    ''' <remarks></remarks>
    <TableMap("k_tokubetu_taiou_cd")> _
    Public Property kTokubetuTaiouCd() As Integer
        Get
            Return kIntTokubetuTaiouCd
        End Get
        Set(ByVal value As Integer)
            kIntTokubetuTaiouCd = value
        End Set
    End Property
#End Region

#Region "金額加算商品コード"
    ''' <summary>
    ''' 金額加算商品コード
    ''' </summary>
    ''' <remarks></remarks>
    Private kStrKasanSyouhinCd As String = String.Empty
    ''' <summary>
    ''' 金額加算商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 金額加算商品コード</returns>
    ''' <remarks></remarks>
    <TableMap("k_kasan_syouhin_cd")> _
    Public Property kKasanSyouhinCd() As String
        Get
            Return kStrKasanSyouhinCd
        End Get
        Set(ByVal value As String)
            kStrKasanSyouhinCd = value
        End Set
    End Property
#End Region

#Region "初期値"
    ''' <summary>
    ''' 初期値
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyokiti As Integer = Integer.MinValue
    ''' <summary>
    ''' 初期値
    ''' </summary>
    ''' <value></value>
    ''' <returns> 初期値</returns>
    ''' <remarks></remarks>
    <TableMap("k_syokiti")> _
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
    Private kIntUriKasanGaku As Integer = 0
    ''' <summary>
    ''' 実請求加算金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 実請求加算金額</returns>
    ''' <remarks></remarks>
    <TableMap("k_uri_kasan_gaku")> _
    Public Property kUriKasanGaku() As Integer
        Get
            Return kIntUriKasanGaku
        End Get
        Set(ByVal value As Integer)
            kIntUriKasanGaku = value
        End Set
    End Property
#End Region

#Region "工務店請求加算金額"
    ''' <summary>
    ''' 工務店請求加算金額
    ''' </summary>
    ''' <remarks></remarks>
    Private kIntKoumutenKasanGaku As Integer = 0
    ''' <summary>
    ''' 工務店請求加算金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工務店請求加算金額</returns>
    ''' <remarks></remarks>
    <TableMap("k_koumuten_kasan_gaku")> _
    Public Property kKoumutenKasanGaku() As Integer
        Get
            Return kIntKoumutenKasanGaku
        End Get
        Set(ByVal value As Integer)
            kIntKoumutenKasanGaku = value
        End Set
    End Property
#End Region

#End Region

#Region "商品マスタの項目"

#Region "金額加算商品名(特別対応データ)"
    ''' <summary>
    ''' 金額加算商品名
    ''' </summary>
    ''' <remarks></remarks>
    Private strKasanSyouhinMei As String = String.Empty
    ''' <summary>
    ''' 金額加算商品名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 金額加算商品名</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_mei")> _
    Public Property kasanSyouhinMei() As String
        Get
            Return strKasanSyouhinMei
        End Get
        Set(ByVal value As String)
            strKasanSyouhinMei = value
        End Set
    End Property

    ''' <summary>
    ''' 倉庫コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSoukoCd As String = String.Empty
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

    ''' <summary>
    ''' 金額加算商品名
    ''' </summary>
    ''' <remarks></remarks>
    Private strKasanSyouhinMeiold As String = String.Empty
    ''' <summary>
    ''' 金額加算商品名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 金額加算商品名</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_mei_old")> _
    Public Property kasanSyouhinMeiOld() As String
        Get
            Return strKasanSyouhinMeiold
        End Get
        Set(ByVal value As String)
            strKasanSyouhinMeiold = value
        End Set
    End Property

    ''' <summary>
    ''' 倉庫コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSoukoCdOld As String = String.Empty
    ''' <summary>
    ''' 倉庫コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 倉庫コード</returns>
    ''' <remarks></remarks>
    <TableMap("souko_cd_old")> _
    Public Property SoukoCdOld() As String
        Get
            Return strSoukoCdOld
        End Get
        Set(ByVal value As String)
            strSoukoCdOld = value
        End Set
    End Property
#End Region

#Region "金額加算商品名(加盟店商品調査方法特別対応マスタ)"
    ''' <summary>
    ''' 金額加算商品名
    ''' </summary>
    ''' <remarks></remarks>
    Private kStrKasanSyouhinMei As String = String.Empty
    ''' <summary>
    ''' 金額加算商品名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 金額加算商品名</returns>
    ''' <remarks></remarks>
    <TableMap("k_syouhin_mei")> _
    Public Property kKasanSyouhinMei() As String
        Get
            Return kStrKasanSyouhinMei
        End Get
        Set(ByVal value As String)
            kStrKasanSyouhinMei = value
        End Set
    End Property

    ''' <summary>
    ''' 倉庫コード
    ''' </summary>
    ''' <remarks></remarks>
    Private kStrSoukoCd As String = String.Empty
    ''' <summary>
    ''' 倉庫コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 倉庫コード</returns>
    ''' <remarks></remarks>
    <TableMap("k_souko_cd")> _
    Public Property kSoukoCd() As String
        Get
            Return kStrSoukoCd
        End Get
        Set(ByVal value As String)
            kStrSoukoCd = value
        End Set
    End Property
#End Region

#End Region

#Region "各種メソッド"
#Region "チェック状況の判定"
    ''' <summary>
    ''' チェック状況の判定
    ''' </summary>
    ''' <remarks></remarks>
    Private blnHanteiChk As Boolean = False
    ''' <summary>
    ''' チェック状況の判定
    ''' </summary>
    ''' <value></value>
    ''' <returns>チェック状況の判定</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property HanteiCheck() As Boolean
        Get
            If Kbn <> String.Empty _
                AndAlso HosyousyoNo <> String.Empty _
                    AndAlso TokubetuTaiouCd <> Integer.MinValue _
                        AndAlso (Torikesi = Integer.MinValue OrElse Torikesi = 0) Then
                blnHanteiChk = True
            Else
                blnHanteiChk = False
            End If
            Return blnHanteiChk
        End Get
    End Property

    ''' <summary>
    ''' チェック状況の判定(マスタ再取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private blnChkM As Boolean = False
    ''' <summary>
    ''' チェック状況の判定(マスタ再取得)
    ''' </summary>
    ''' <value></value>
    ''' <returns>チェック状況の判定(マスタ再取得)</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property kHanteiCheck() As Boolean
        Get
            If Syokiti = 1 Then
                blnChkM = True
            Else
                blnChkM = False
            End If
            Return blnChkM
        End Get
    End Property

    ''' <summary>
    ''' チェック状況(画面)
    ''' </summary>
    ''' <remarks></remarks>
    Private blnChk As Boolean = False
    ''' <summary>
    ''' チェック状況(画面)
    ''' </summary>
    ''' <value></value>
    ''' <returns>チェック状況(画面)</returns>
    ''' <remarks></remarks>
    Public Overridable Property CheckJyky() As Boolean
        Get
            Return blnChk
        End Get
        Set(ByVal value As Boolean)
            blnChk = value
        End Set
    End Property

    ''' <summary>
    ''' チェック状況(Old値)
    ''' </summary>
    ''' <remarks></remarks>
    Private blnChkOld As Boolean = False
    ''' <summary>
    ''' チェック状況(Old値)
    ''' </summary>
    ''' <value></value>
    ''' <returns>チェック状況(Old値)</returns>
    ''' <remarks></remarks>
    Public Overridable Property CheckJykyOld() As Boolean
        Get
            Return blnChkOld
        End Get
        Set(ByVal value As Boolean)
            blnChkOld = value
        End Set
    End Property

    ''' <summary>
    ''' チェック状況の判定
    ''' </summary>
    ''' <remarks></remarks>
    Private blnHenkouChk As Boolean = False
    ''' <summary>
    ''' チェック状況の判定
    ''' </summary>
    ''' <value></value>
    ''' <returns>チェック状況の判定</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property HenkouCheck() As Boolean
        Get
            HenkouCheck = False

            '更新フラグ=1の場合
            If UpdFlg = 1 Then
                blnHenkouChk = True
            Else 'チェック状況に変更がある場合
                If CheckJyky <> CheckJykyOld Then
                    blnHenkouChk = True
                End If
            End If

            Return blnHenkouChk
        End Get
    End Property
#End Region

#Region "表示の判定"
    ''' <summary>
    ''' 表示の判定
    ''' </summary>
    ''' <remarks></remarks>
    Private blnDisp As Boolean = True
    ''' <summary>
    ''' 表示の判定
    ''' </summary>
    ''' <value></value>
    ''' <returns>表示の判定</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DispHantei() As Boolean
        Get
            blnDisp = True

            '特別対応データが取消でない場合
            If Torikesi = 0 Then
            ElseIf BunruiCd <> String.Empty AndAlso GamenHyoujiNo > 0 Then '特別対応データが取消の場合でも設定先有りの場合
            ElseIf mTorikesi = 0 Then '特別対応マスタが取消でない場合
            Else
                blnDisp = False
            End If
            Return blnDisp
        End Get
    End Property

    ''' <summary>
    ''' 設定先のスタイル
    ''' </summary>
    ''' <remarks></remarks>
    Private strSetteiSakiStyle As String = String.Empty
    ''' <summary>
    ''' 設定先のスタイル
    ''' </summary>
    ''' <value></value>
    ''' <returns> 設定先のスタイル</returns>
    ''' <remarks></remarks>
    Public Property SetteiSakiStyle() As String
        Get
            Return strSetteiSakiStyle
        End Get
        Set(ByVal value As String)
            strSetteiSakiStyle = value
        End Set
    End Property

    ''' <summary>
    ''' 画面表示の優先順を判定(True:トラン,False:マスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private blnPrimaryDisplay As Boolean = False
    ''' <summary>
    ''' 画面表示の優先順を判定
    ''' </summary>
    ''' <value></value>
    ''' <returns>画面表示の優先順を判定</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PrimaryDisplay() As Boolean
        Get
            'トランデータ存在する
            If Kbn <> String.Empty _
                AndAlso HosyousyoNo <> String.Empty _
                    AndAlso TokubetuTaiouCd <> Integer.MinValue Then

                '取消でない
                If Torikesi = 0 Then
                    blnPrimaryDisplay = True
                Else '取消の場合
                    '価格処理フラグがたっている場合、トラン優先
                    If KkkSyoriFlg = 1 Then
                        blnPrimaryDisplay = True
                    Else
                        '取消で設定先がセットされている場合
                        If BunruiCd <> String.Empty AndAlso GamenHyoujiNo > 0 Then
                            blnPrimaryDisplay = True
                        Else
                            blnPrimaryDisplay = False
                        End If
                    End If
                End If
            Else 'トランデータが存在しない
                blnPrimaryDisplay = False
            End If
            Return blnPrimaryDisplay
        End Get
    End Property

#End Region

#Region "更新の設定"
    ''' <summary>
    ''' 価格反映対象確認フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private blnChgVal As Boolean = False
    ''' <summary>
    ''' 価格反映対象確認フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ChgVal() As Boolean
        Get
            Return blnChgVal
        End Get
        Set(ByVal value As Boolean)
            blnChgVal = value
        End Set
    End Property

    ''' <summary>
    ''' 削除判断フラグ(受注画面用)
    ''' </summary>
    ''' <remarks></remarks>
    Private blnDeleteFlg As Boolean = False
    ''' <summary>
    ''' 削除判断フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeleteFlg() As Boolean
        Get
            Return blnDeleteFlg
        End Get
        Set(ByVal value As Boolean)
            blnDeleteFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 価格処理セットフラグ(邸別データ修正画面用)
    ''' </summary>
    ''' <remarks></remarks>
    Private blnKkkSetFlg As Boolean = False
    ''' <summary>
    ''' 価格処理セットフラグ(邸別データ修正画面用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KkkSetFlg() As Boolean
        Get
            Return blnKkkSetFlg
        End Get
        Set(ByVal value As Boolean)
            blnKkkSetFlg = value
        End Set
    End Property

#End Region

#End Region

End Class