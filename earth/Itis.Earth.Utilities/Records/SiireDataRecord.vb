''' <summary>
''' 仕入データテーブルのレコードクラスです
''' </summary>
''' <remarks></remarks>
<TableClassMap("t_siire_data")> _
Public Class SiireDataRecord

#Region "伝票ユニークNO"
    ''' <summary>
    ''' 伝票ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intDenpyouUniqueNo As Integer
    ''' <summary>
    ''' 伝票ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_unique_no", IsKey:=True, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property DenpyouUniqueNo() As Integer
        Get
            Return intDenpyouUniqueNo
        End Get
        Set(ByVal value As Integer)
            intDenpyouUniqueNo = value
        End Set
    End Property
#End Region

#Region "伝票NO"
    ''' <summary>
    ''' 伝票NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenpyouNo As String
    ''' <summary>
    ''' 伝票NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票NO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_no", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=5)> _
    Public Property DenpyouNo() As String
        Get
            Return strDenpyouNo
        End Get
        Set(ByVal value As String)
            strDenpyouNo = value
        End Set
    End Property
#End Region

#Region "伝票種別"
    ''' <summary>
    ''' 伝票種別
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenpyouSyubetu As String
    ''' <summary>
    ''' 伝票種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票種別</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_syubetu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Property DenpyouSyubetu() As String
        Get
            Return strDenpyouSyubetu
        End Get
        Set(ByVal value As String)
            strDenpyouSyubetu = value
        End Set
    End Property
#End Region

#Region "取消元伝票ユニークNO"
    ''' <summary>
    ''' 取消元伝票ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesiMotoDenpyouUniqueNo As Integer
    ''' <summary>
    ''' 取消元伝票ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取消元伝票ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi_moto_denpyou_unique_no", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property TorikesiMotoDenpyouUniqueNo() As Integer
        Get
            Return intTorikesiMotoDenpyouUniqueNo
        End Get
        Set(ByVal value As Integer)
            intTorikesiMotoDenpyouUniqueNo = value
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
    Private intHimodukeTableType As Integer
    ''' <summary>
    ''' 紐付け元テーブル種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 紐付け元テーブル種別</returns>
    ''' <remarks></remarks>
    <TableMap("himoduke_table_type", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property HimodukeTableType() As Integer
        Get
            Return intHimodukeTableType
        End Get
        Set(ByVal value As Integer)
            intHimodukeTableType = value
        End Set
    End Property
#End Region

#Region "仕入年月日"
    ''' <summary>
    ''' 仕入年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSiireDate As DateTime
    ''' <summary>
    ''' 仕入年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 仕入年月日</returns>
    ''' <remarks></remarks>
    <TableMap("siire_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property SiireDate() As DateTime
        Get
            Return dateSiireDate
        End Get
        Set(ByVal value As DateTime)
            dateSiireDate = value
        End Set
    End Property
#End Region

#Region "伝票仕入年月日"
    ''' <summary>
    ''' 伝票仕入年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDenpyouSiireDate As DateTime
    ''' <summary>
    ''' 伝票仕入年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns>伝票仕入年月日</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_siire_date", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property DenpyouSiireDate() As DateTime
        Get
            Return dateDenpyouSiireDate
        End Get
        Set(ByVal value As DateTime)
            dateDenpyouSiireDate = value
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
    <TableMap("tys_kaisya_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "調査会社事業所コード"
    ''' <summary>
    ''' 調査会社事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaJigyousyoCd As String
    ''' <summary>
    ''' 調査会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社事業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_jigyousyo_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Property TysKaisyaJigyousyoCd() As String
        Get
            Return strTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "調査会社名"
    ''' <summary>
    ''' 調査会社名
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaMei As String
    ''' <summary>
    ''' 調査会社名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社名</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_mei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Property TysKaisyaMei() As String
        Get
            Return strTysKaisyaMei
        End Get
        Set(ByVal value As String)
            strTysKaisyaMei = value
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
    <TableMap("hinmei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
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

#Region "仕入金額"
    ''' <summary>
    ''' 仕入金額
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSiireGaku As Long
    ''' <summary>
    ''' 仕入金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 仕入金額</returns>
    ''' <remarks></remarks>
    <TableMap("siire_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Property SiireGaku() As Long
        Get
            Return lngSiireGaku
        End Get
        Set(ByVal value As Long)
            lngSiireGaku = value
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
    ''' <returns> 登録ログインユーザー名</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_name", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=128)> _
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