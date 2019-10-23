''' <summary>
''' 売上データテーブルのレコードクラスです
''' </summary>
''' <remarks></remarks>
<TableClassMap("t_uriage_data")> _
Public Class UriageDataRecord

#Region "伝票ユニークNO"
    ''' <summary>
    ''' 伝票ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intDenUnqNo As Integer
    ''' <summary>
    ''' 伝票ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_unique_no", IsKey:=True, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property DenUnqNo() As Integer
        Get
            Return intDenUnqNo
        End Get
        Set(ByVal value As Integer)
            intDenUnqNo = value
        End Set
    End Property
#End Region

#Region "伝票NO"
    ''' <summary>
    ''' 伝票NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenNo As String
    ''' <summary>
    ''' 伝票NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票NO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_no", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=5)> _
    Public Property DenNo() As String
        Get
            Return strDenNo
        End Get
        Set(ByVal value As String)
            strDenNo = value
        End Set
    End Property
#End Region

#Region "伝票種別"
    ''' <summary>
    ''' 伝票種別
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenSyubetu As String
    ''' <summary>
    ''' 伝票種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票種別</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_syubetu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Property DenSyubetu() As String
        Get
            Return strDenSyubetu
        End Get
        Set(ByVal value As String)
            strDenSyubetu = value
        End Set
    End Property
#End Region

#Region "取消元伝票ユニークNO"
    ''' <summary>
    ''' 取消元伝票ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private strToriMotoDenUnqNo As Integer
    ''' <summary>
    ''' 取消元伝票ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取消元伝票ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi_moto_denpyou_unique_no", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property ToriMotoDenUnqNo() As Integer
        Get
            Return strToriMotoDenUnqNo
        End Get
        Set(ByVal value As Integer)
            strToriMotoDenUnqNo = value
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
    <TableMap("kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
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
    <TableMap("bangou", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Property Bangou() As String
        Get
            Return strBangou
        End Get
        Set(ByVal value As String)
            strBangou = value
        End Set
    End Property
#End Region

#Region "紐付けコード"
    ''' <summary>
    ''' 紐付けコード
    ''' </summary>
    ''' <remarks></remarks>
    Private strHimodukeCd As String
    ''' <summary>
    ''' 紐付けコード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 紐付けコード</returns>
    ''' <remarks></remarks>
    <TableMap("himoduke_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Property HimodukeCd() As String
        Get
            Return strHimodukeCd
        End Get
        Set(ByVal value As String)
            strHimodukeCd = value
        End Set
    End Property
#End Region

#Region "紐付け元テーブル種別"
    ''' <summary>
    ''' 紐付け元テーブル種別
    ''' </summary>
    ''' <remarks></remarks>
    Private strHimodukeMotoTblSyubetu As String
    ''' <summary>
    ''' 紐付け元テーブル種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 紐付け元テーブル種別</returns>
    ''' <remarks></remarks>
    <TableMap("himoduke_table_type", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property HimodukeMotoTblSyubetu() As Integer
        Get
            Return strHimodukeMotoTblSyubetu
        End Get
        Set(ByVal value As Integer)
            strHimodukeMotoTblSyubetu = value
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
    <TableMap("uri_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("denpyou_uri_date", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property DenUriDate() As DateTime
        Get
            Return dateDenUriDate
        End Get
        Set(ByVal value As DateTime)
            dateDenUriDate = value
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
    <TableMap("seikyuu_date", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("seikyuu_saki_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
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
    <TableMap("seikyuu_saki_brc", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
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
    <TableMap("seikyuu_saki_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
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
    <TableMap("seikyuu_saki_mei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
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
    <TableMap("syouhin_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
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
    <TableMap("hinmei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    Private intSuu As Integer
    ''' <summary>
    ''' 数量
    ''' </summary>
    ''' <value></value>
    ''' <returns> 数量</returns>
    ''' <remarks></remarks>
    <TableMap("suu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Suu() As Integer
        Get
            Return intSuu
        End Get
        Set(ByVal value As Integer)
            intSuu = value
        End Set
    End Property
#End Region

#Region "単位"
    ''' <summary>
    ''' 単位
    ''' </summary>
    ''' <remarks></remarks>
    Private strTani As String
    ''' <summary>
    ''' 単位
    ''' </summary>
    ''' <value></value>
    ''' <returns> 単位</returns>
    ''' <remarks></remarks>
    <TableMap("tani", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=4)> _
    Public Property Tani() As String
        Get
            Return strTani
        End Get
        Set(ByVal value As String)
            strTani = value
        End Set
    End Property
#End Region

#Region "単価"
    ''' <summary>
    ''' 単価
    ''' </summary>
    ''' <remarks></remarks>
    Private intTanka As Integer
    ''' <summary>
    ''' 単価
    ''' </summary>
    ''' <value></value>
    ''' <returns> 単価</returns>
    ''' <remarks></remarks>
    <TableMap("tanka", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Tanka() As Integer
        Get
            Return intTanka
        End Get
        Set(ByVal value As Integer)
            intTanka = value
        End Set
    End Property
#End Region

#Region "売上金額"
    ''' <summary>
    ''' 売上金額
    ''' </summary>
    ''' <remarks></remarks>
    Private lngUriGaku As Long
    ''' <summary>
    ''' 売上金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上金額</returns>
    ''' <remarks></remarks>
    <TableMap("uri_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Property UriGaku() As Long
        Get
            Return lngUriGaku
        End Get
        Set(ByVal value As Long)
            lngUriGaku = value
        End Set
    End Property
#End Region

#Region "外税額"
    ''' <summary>
    ''' 外税額
    ''' </summary>
    ''' <remarks></remarks>
    Private intSotozeiGaku As Integer
    ''' <summary>
    ''' 外税額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 外税額</returns>
    ''' <remarks></remarks>
    <TableMap("sotozei_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property SotozeiGaku() As Integer
        Get
            Return intSotozeiGaku
        End Get
        Set(ByVal value As Integer)
            intSotozeiGaku = value
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
    <TableMap("zei_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=1)> _
    Public Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
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
    <TableMap("add_login_user_id", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
