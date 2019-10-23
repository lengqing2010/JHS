<TableClassMap("t_teibetu_seikyuu")> _
Public Class TeibetuSeikyuuRecord

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
    <TableMap("kbn", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.Char, SqlLength:=1)> _
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
    Private strHosyousyoNo As String
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "分類ｺｰﾄﾞ"
    ''' <summary>
    ''' 分類ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strBunruiCd As String
    ''' <summary>
    ''' 分類ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 分類ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("bunrui_cd", IsKey:=True, IsUpdate:=True, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
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
    Private intGamenHyoujiNo As Integer
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 画面表示NO</returns>
    ''' <remarks></remarks>
    <TableMap("gamen_hyouji_no", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property GamenHyoujiNo() As Integer
        Get
            Return intGamenHyoujiNo
        End Get
        Set(ByVal value As Integer)
            intGamenHyoujiNo = value
        End Set
    End Property
#End Region

#Region "商品ｺｰﾄﾞ"
    ''' <summary>
    ''' 商品ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' 商品ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Overridable Property SyouhinCd() As String
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
    Private strSyouhinMei As String
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品名</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_mei")> _
    Public Overridable Property SyouhinMei() As String
        Get
            Return strSyouhinMei
        End Get
        Set(ByVal value As String)
            strSyouhinMei = value
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
    Public Overridable Property UriGaku() As Integer
        Get
            Return intUriGaku
        End Get
        Set(ByVal value As Integer)
            intUriGaku = value
        End Set
    End Property
#End Region

#Region "仕入金額"
    ''' <summary>
    ''' 仕入金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireGaku As Integer
    ''' <summary>
    ''' 仕入金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 仕入金額</returns>
    ''' <remarks></remarks>
    <TableMap("siire_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SiireGaku() As Integer
        Get
            Return intSiireGaku
        End Get
        Set(ByVal value As Integer)
            intSiireGaku = value
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
    Public Overridable Property ZeiKbn() As String
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
    Private decZeiritu As Decimal
    ''' <summary>
    ''' 税率
    ''' </summary>
    ''' <value></value>
    ''' <returns> 税率</returns>
    ''' <remarks></remarks>
    <TableMap("zeiritu")> _
    Public Overridable Property Zeiritu() As Decimal
        Get
            Return decZeiritu
        End Get
        Set(ByVal value As Decimal)
            decZeiritu = value
        End Set
    End Property
#End Region

#Region "売上消費税額"
    ''' <summary>
    ''' 売上消費税額
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriageSyouhiZeiGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' 売上消費税額
    ''' </summary>
    ''' <value></value>
    ''' <returns>売上消費税額</returns>
    ''' <remarks></remarks>
    <TableMap("syouhizei_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property UriageSyouhiZeiGaku() As Integer
        Get
            Dim intTmpUriGaku As Integer = IIf(UriGaku = Integer.MinValue, 0, UriGaku) '売上金額
            Dim decTmpZeiritu As Decimal = IIf(Zeiritu = Decimal.MinValue, 0, Zeiritu) '税率

            '消費税額
            If intUriageSyouhiZeiGaku = Integer.MinValue Then 'NULLの場合、売上金額と税率から換算
                intUriageSyouhiZeiGaku = Fix(intTmpUriGaku * decTmpZeiritu) '消費税額 = 売上金額 * 税率
            End If
            Return intUriageSyouhiZeiGaku
        End Get
        Set(ByVal value As Integer)
            intUriageSyouhiZeiGaku = value
        End Set
    End Property
#End Region

#Region "仕入消費税額"
    ''' <summary>
    ''' 仕入消費税額
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireSyouhiZeiGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' 仕入消費税額
    ''' </summary>
    ''' <value></value>
    ''' <returns>仕入消費税額</returns>
    ''' <remarks></remarks>
    <TableMap("siire_syouhizei_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=1)> _
    Public Overridable Property SiireSyouhiZeiGaku() As Integer
        Get
            Dim intTmpSiireGaku As Integer = IIf(SiireGaku = Integer.MinValue, 0, SiireGaku) '仕入金額
            Dim decTmpZeiritu As Decimal = IIf(Zeiritu = Decimal.MinValue, 0, Zeiritu) '税率

            '消費税額
            If intSiireSyouhiZeiGaku = Integer.MinValue Then 'NULLの場合、売上金額と税率から換算
                intSiireSyouhiZeiGaku = Fix(intTmpSiireGaku * decTmpZeiritu) '消費税額 = 仕入金額 * 税率
            End If
            Return intSiireSyouhiZeiGaku
        End Get
        Set(ByVal value As Integer)
            intSiireSyouhiZeiGaku = value
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
    Public Overridable Property SeikyuusyoHakDate() As DateTime
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
    Private dateUriDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 売上年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上年月日</returns>
    ''' <remarks></remarks>
    <TableMap("uri_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property UriDate() As DateTime
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
    Private dateDenpyouUriDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 伝票売上年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票売上年月日</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_uri_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property DenpyouUriDate() As DateTime
        Get
            Return dateDenpyouUriDate
        End Get
        Set(ByVal value As DateTime)
            dateDenpyouUriDate = value
        End Set
    End Property
#End Region

#Region "伝票仕入年月日"
    ''' <summary>
    ''' 伝票仕入年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDenpyouSiireDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 伝票仕入年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票仕入年月日</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_siire_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=1)> _
    Public Overridable Property DenpyouSiireDate() As DateTime
        Get
            Return dateDenpyouSiireDate
        End Get
        Set(ByVal value As DateTime)
            dateDenpyouSiireDate = value
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
    Public Overridable Property SeikyuuUmu() As Integer
        Get
            Return intSeikyuuUmu
        End Get
        Set(ByVal value As Integer)
            intSeikyuuUmu = value
        End Set
    End Property
#End Region

#Region "確定区分"
    ''' <summary>
    ''' 確定区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakuteiKbn As Integer
    ''' <summary>
    ''' 確定区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 確定区分</returns>
    ''' <remarks></remarks>
    <TableMap("kakutei_kbn", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KakuteiKbn() As Integer
        Get
            Return intKakuteiKbn
        End Get
        Set(ByVal value As Integer)
            intKakuteiKbn = value
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
    Public Overridable Property UriKeijyouFlg() As Integer
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
    <TableMap("uri_keijyou_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property UriKeijyouDate() As DateTime
        Get
            Return dateUriKeijyouDate
        End Get
        Set(ByVal value As DateTime)
            dateUriKeijyouDate = value
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
    <TableMap("bikou", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overridable Property Bikou() As String
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
    Public Overridable Property KoumutenSeikyuuGaku() As Integer
        Get
            Return intKoumutenSeikyuuGaku
        End Get
        Set(ByVal value As Integer)
            intKoumutenSeikyuuGaku = value
        End Set
    End Property
#End Region

#Region "発注書金額"
    ''' <summary>
    ''' 発注書金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intHattyuusyoGaku As Integer
    ''' <summary>
    ''' 発注書金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発注書金額</returns>
    ''' <remarks></remarks>
    <TableMap("hattyuusyo_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HattyuusyoGaku() As Integer
        Get
            If intHattyuusyoGaku = 0 Then
                Return Integer.MinValue
            End If

            Return intHattyuusyoGaku
        End Get
        Set(ByVal value As Integer)
            intHattyuusyoGaku = value
        End Set
    End Property
#End Region

#Region "発注書確認日"
    ''' <summary>
    ''' 発注書確認日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHattyuusyoKakuninDate As DateTime
    ''' <summary>
    ''' 発注書確認日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発注書確認日</returns>
    ''' <remarks></remarks>
    <TableMap("hattyuusyo_kakunin_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HattyuusyoKakuninDate() As DateTime
        Get
            Return dateHattyuusyoKakuninDate
        End Get
        Set(ByVal value As DateTime)
            dateHattyuusyoKakuninDate = value
        End Set
    End Property
#End Region

#Region "一括入金FLG"
    ''' <summary>
    ''' 一括入金FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intIkkatuNyuukinFlg As Integer
    ''' <summary>
    ''' 一括入金FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 一括入金FLG</returns>
    ''' <remarks></remarks>
    <TableMap("ikkatu_nyuukin_flg", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property IkkatuNyuukinFlg() As Integer
        Get
            Return intIkkatuNyuukinFlg
        End Get
        Set(ByVal value As Integer)
            intIkkatuNyuukinFlg = value
        End Set
    End Property
#End Region

#Region "調査見積書作成日"
    ''' <summary>
    ''' 調査見積書作成日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysMitsyoSakuseiDate As DateTime
    ''' <summary>
    ''' 調査見積書作成日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査見積書作成日</returns>
    ''' <remarks></remarks>
    <TableMap("tys_mitsyo_sakusei_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property TysMitsyoSakuseiDate() As DateTime
        Get
            Return dateTysMitsyoSakuseiDate
        End Get
        Set(ByVal value As DateTime)
            dateTysMitsyoSakuseiDate = value
        End Set
    End Property
#End Region

#Region "発注書確定FLG"
    ''' <summary>
    ''' 発注書確定FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intHattyuusyoKakuteiFlg As Integer
    ''' <summary>
    ''' 発注書確定FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発注書確定FLG</returns>
    ''' <remarks></remarks>
    <TableMap("hattyuusyo_kakutei_flg", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HattyuusyoKakuteiFlg() As Integer
        Get
            Return intHattyuusyoKakuteiFlg
        End Get
        Set(ByVal value As Integer)
            intHattyuusyoKakuteiFlg = value
        End Set
    End Property
#End Region

#Region "請求先/仕入先"

#Region "請求先コード"
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String = Nothing
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先コード</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overridable Property SeikyuuSakiCd() As String
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
    Private strSeikyuuSakiBrc As String = Nothing
    ''' <summary>
    ''' 請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先枝番</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_brc", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property SeikyuuSakiBrc() As String
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
    Private strSeikyuuSakiKbn As String = Nothing
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先区分</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Overridable Property SeikyuuSakiKbn() As String
        Get
            Return strSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "調査会社コード"
    ''' <summary>
    ''' 調査会社コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String = Nothing
    ''' <summary>
    ''' 調査会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社コード</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overridable Property TysKaisyaCd() As String
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
    Private strTysKaisyaJigyousyoCd As String = Nothing
    ''' <summary>
    ''' 調査会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社事業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_jigyousyo_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property TysKaisyaJigyousyoCd() As String
        Get
            Return strTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

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
    Private dateAddDatetime As DateTime
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "税込売上金額"
    ''' <summary>
    ''' 税込売上金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 税込売上金額</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ZeikomiUriGaku() As Integer
        Get
            Dim intTmpUriGaku As Integer = IIf(UriGaku = Integer.MinValue, 0, UriGaku)
            Dim decTmpZeiritu As Decimal = IIf(Zeiritu = Decimal.MinValue, 0, Zeiritu)
            If UriageSyouhiZeiGaku = Integer.MinValue Then '消費税額=NULLの場合、税率から算出
                intTmpUriGaku = intTmpUriGaku + Fix(intTmpUriGaku * decTmpZeiritu) '税込売上金額 = 売上金額 + (売上金額 * 消費税率)
            Else
                intTmpUriGaku = intTmpUriGaku + UriageSyouhiZeiGaku     '税込売上金額 = 売上金額 + 消費税額
            End If
            Return intTmpUriGaku
        End Get
    End Property
#End Region

#Region "税込仕入金額"
    ''' <summary>
    ''' 税込仕入金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>税込仕入金額</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ZeikomiSiireGaku() As Integer
        Get
            Dim intTmpSirGaku As Integer = IIf(SiireGaku = Integer.MinValue, 0, SiireGaku)
            Dim decTmpZeiritu As Decimal = IIf(Zeiritu = Decimal.MinValue, 0, Zeiritu)
            If SiireSyouhiZeiGaku = Integer.MinValue Then
                '税率から算出
                intTmpSirGaku = intTmpSirGaku + Fix(intTmpSirGaku * decTmpZeiritu) '税込仕入金額 = 仕入金額 + (仕入金額 * 消費税率)
            Else

            End If
            '税率から算出
            intTmpSirGaku = intTmpSirGaku + SiireSyouhiZeiGaku      '税込仕入金額 = 仕入金額 + 消費税額
            Return intTmpSirGaku
        End Get
    End Property
#End Region

#Region "承諾書金額変更可否FLG"
    ''' <summary>
    ''' 承諾書金額変更可否FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private blnSyoudakuHenkouKahi As Boolean
    ''' <summary>
    ''' 承諾書金額変更可否FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>承諾書金額変更可否FLG</returns>
    ''' <remarks></remarks>
    Public Property SyoudakuHenkouKahi() As Boolean
        Get
            Return blnSyoudakuHenkouKahi
        End Get
        Set(ByVal value As Boolean)
            blnSyoudakuHenkouKahi = value
        End Set
    End Property
#End Region

#Region "工務店請求金額変更可否FLG"
    ''' <summary>
    ''' 工務店請求金額変更可否FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private blnKoumutenHenkouKahi As Boolean
    ''' <summary>
    ''' 工務店請求金額変更可否FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>工務店請求金額変更可否FLG</returns>
    ''' <remarks></remarks>
    Public Property KoumutenHenkouKahi() As Boolean
        Get
            Return blnKoumutenHenkouKahi
        End Get
        Set(ByVal value As Boolean)
            blnKoumutenHenkouKahi = value
        End Set
    End Property
#End Region

#Region "実請求金額変更可否FLG"
    ''' <summary>
    ''' 実請求金額変更可否FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private blnJituseikyuuHenkouKahi As Boolean
    ''' <summary>
    ''' 実請求金額変更可否FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>実請求金額変更可否FLG</returns>
    ''' <remarks></remarks>
    Public Property JituseikyuuHenkouKahi() As Boolean
        Get
            Return blnJituseikyuuHenkouKahi
        End Get
        Set(ByVal value As Boolean)
            blnJituseikyuuHenkouKahi = value
        End Set
    End Property
#End Region

#Region "各種メソッド"
#Region "特別対応価格対応"
    ''' <summary>
    ''' 邸別請求の変更状況の判定
    ''' </summary>
    ''' <remarks></remarks>
    Private emKkkJyky As EarthEnum.emTeibetuSeikyuuKkkJyky = EarthEnum.emTeibetuSeikyuuKkkJyky.NONE
    ''' <summary>
    ''' 邸別請求の変更状況の判定
    ''' </summary>
    ''' <value></value>
    ''' <returns>邸別請求の変更状況の判定</returns>
    ''' <remarks></remarks>
    Public Property KkkHenkouCheck() As EarthEnum.emTeibetuSeikyuuKkkJyky
        Get
            Return emKkkJyky
        End Get
        Set(ByVal value As EarthEnum.emTeibetuSeikyuuKkkJyky)
            emKkkJyky = value
        End Set
    End Property

#End Region

#End Region

End Class