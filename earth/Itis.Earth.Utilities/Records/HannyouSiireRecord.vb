''' <summary>
''' 汎用仕入データテーブルのレコードクラスです
''' </summary>
''' <remarks></remarks>
<TableClassMap("t_hannyou_siire")> _
Public Class HannyouSiireRecord

#Region "汎用仕入ユニークNO"
    ''' <summary>
    ''' 汎用仕入ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intHanSiireUnqNo As Integer = 0
    ''' <summary>
    ''' 汎用仕入ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns>汎用仕入ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("han_siire_unique_no", IsKey:=True, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property HanSiireUnqNo() As Integer
        Get
            Return intHanSiireUnqNo
        End Get
        Set(ByVal value As Integer)
            intHanSiireUnqNo = value
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
    ''' <returns>仕入年月日</returns>
    ''' <remarks></remarks>
    <TableMap("siire_date", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("denpyou_siire_date", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("tys_kaisya_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
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
    <TableMap("tys_kaisya_jigyousyo_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
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
    <TableMap("tys_kaisya_mei")> _
    Public Property TysKaisyaMei() As String
        Get
            Return strTysKaisyaMei
        End Get
        Set(ByVal value As String)
            strTysKaisyaMei = value
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
    <TableMap("kameiten_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "加盟店名"
    ''' <summary>
    ''' 加盟店名
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMei As String
    ''' <summary>
    ''' 加盟店名
    ''' </summary>
    ''' <value></value>
    ''' <returns>加盟店名</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_mei1")> _
    Public Property KameitenMei() As String
        Get
            Return strKameitenMei
        End Get
        Set(ByVal value As String)
            strKameitenMei = value
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

#Region "商品名"
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <remarks></remarks>
    Private strHinmei As String
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品名</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_mei")> _
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

#Region "仕入金額[単価*数量*税率]"
    ''' <summary>
    ''' 仕入金額[単価*数量*税率]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSiireGaku As Long = 0
    ''' <summary>
    ''' 仕入金額[単価*数量*税率]
    ''' </summary>
    ''' <value></value>
    ''' <returns>売上金額[単価*数量*税率]</returns>
    ''' <remarks></remarks>
    Public Property SiireGaku() As Long
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
            lngSiireGaku = value
        End Set
    End Property
#End Region

#Region "仕入計上FLG"
    ''' <summary>
    ''' 仕入計上FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireKeijyouFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' 仕入計上FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>仕入計上FLG</returns>
    ''' <remarks></remarks>
    <TableMap("siire_keijyou_flg", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property SiireKeijyouFlg() As Integer
        Get
            Return intSiireKeijyouFlg
        End Get
        Set(ByVal value As Integer)
            intSiireKeijyouFlg = value
        End Set
    End Property
#End Region

#Region "仕入計上日"
    ''' <summary>
    ''' 仕入計上日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSiireKeijyouDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 仕入計上日
    ''' </summary>
    ''' <value></value>
    ''' <returns>仕入計上日</returns>
    ''' <remarks></remarks>
    <TableMap("siire_keijyou_date", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property SiireKeijyouDate() As DateTime
        Get
            Return dateSiireKeijyouDate
        End Get
        Set(ByVal value As DateTime)
            dateSiireKeijyouDate = value
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

End Class