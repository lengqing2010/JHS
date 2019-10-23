Public Class SinkaikeiSiharaiSakiRecord

#Region "新会計事業所コード"
    ''' <summary>
    ''' 新会計事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkkJigyouCd As String
    ''' <summary>
    ''' 新会計事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>新会計事業所コード </returns>
    ''' <remarks></remarks>
    <TableMap("skk_jigyou_cd")> _
    Public Property SkkJigyouCd() As String
        Get
            Return strSkkJigyouCd
        End Get
        Set(ByVal value As String)
            strSkkJigyouCd = value
        End Set
    End Property
#End Region

#Region "新会計支払先コード"
    ''' <summary>
    ''' 新会計支払先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkkShriSakiCd As String
    ''' <summary>
    ''' 新会計支払先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 新会計支払先コード</returns>
    ''' <remarks></remarks>
    <TableMap("skk_shri_saki_cd")> _
    Public Property SkkShriSakiCd() As String
        Get
            Return strSkkShriSakiCd
        End Get
        Set(ByVal value As String)
            strSkkShriSakiCd = value
        End Set
    End Property
#End Region

#Region "小名寄コード"
    ''' <summary>
    ''' 小名寄コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKoNayoseCd As String
    ''' <summary>
    ''' 小名寄コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>小名寄コード </returns>
    ''' <remarks></remarks>
    <TableMap("ko_nayose_cd")> _
    Public Property KoNayoseCd() As String
        Get
            Return strKoNayoseCd
        End Get
        Set(ByVal value As String)
            strKoNayoseCd = value
        End Set
    End Property
#End Region

#Region "大名寄コード"
    ''' <summary>
    ''' 大名寄コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strOoNayoseCd As String
    ''' <summary>
    ''' 大名寄コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>大名寄コード </returns>
    ''' <remarks></remarks>
    <TableMap("oo_nayose_cd")> _
    Public Property OoNayoseCd() As String
        Get
            Return strOoNayoseCd
        End Get
        Set(ByVal value As String)
            strOoNayoseCd = value
        End Set
    End Property
#End Region

#Region "KEY支払先名"
    ''' <summary>
    ''' KEY支払先名
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriSakiMei As String
    ''' <summary>
    ''' KEY支払先名
    ''' </summary>
    ''' <value></value>
    ''' <returns>KEY支払先名 </returns>
    ''' <remarks></remarks>
    <TableMap("shri_saki_mei")> _
    Public Property ShriSakiMei() As String
        Get
            Return strShriSakiMei
        End Get
        Set(ByVal value As String)
            strShriSakiMei = value
        End Set
    End Property
#End Region

#Region "更新日"
    ''' <summary>
    ''' 更新日
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinBi As String
    ''' <summary>
    ''' 更新日
    ''' </summary>
    ''' <value></value>
    ''' <returns>更新日 </returns>
    ''' <remarks></remarks>
    <TableMap("kousin_bi")> _
    Public Property KousinBi() As String
        Get
            Return strKousinBi
        End Get
        Set(ByVal value As String)
            strKousinBi = value
        End Set
    End Property
#End Region

#Region "支払日"
    ''' <summary>
    ''' 支払日
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriBi As String
    ''' <summary>
    ''' 支払日
    ''' </summary>
    ''' <value></value>
    ''' <returns>支払日 </returns>
    ''' <remarks></remarks>
    <TableMap("shri_bi")> _
    Public Property ShriBi() As String
        Get
            Return strShriBi
        End Get
        Set(ByVal value As String)
            strShriBi = value
        End Set
    End Property
#End Region

#Region "支払方法"
    ''' <summary>
    ''' 支払方法
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriHouhou As String
    ''' <summary>
    ''' 支払方法
    ''' </summary>
    ''' <value></value>
    ''' <returns>支払方法 </returns>
    ''' <remarks></remarks>
    <TableMap("shri_houhou")> _
    Public Property ShriHouhou() As String
        Get
            Return strShriHouhou
        End Get
        Set(ByVal value As String)
            strShriHouhou = value
        End Set
    End Property
#End Region

#Region "手形比率"
    ''' <summary>
    ''' 手形比率
    ''' </summary>
    ''' <remarks></remarks>
    Private decTegataHiritu As Decimal
    ''' <summary>
    ''' 手形比率
    ''' </summary>
    ''' <value></value>
    ''' <returns> 手形比率</returns>
    ''' <remarks></remarks>
    <TableMap("tegata_hiritu")> _
    Public Property TegataHiritu() As Decimal
        Get
            Return decTegataHiritu
        End Get
        Set(ByVal value As Decimal)
            decTegataHiritu = value
        End Set
    End Property
#End Region

#Region "手形サイト"
    ''' <summary>
    ''' 手形サイト
    ''' </summary>
    ''' <remarks></remarks>
    Private intTegataSite As Integer
    ''' <summary>
    ''' 手形サイト
    ''' </summary>
    ''' <value></value>
    ''' <returns>手形サイト </returns>
    ''' <remarks></remarks>
    <TableMap("tegata_site")> _
    Public Property TegataSite() As Integer
        Get
            Return intTegataSite
        End Get
        Set(ByVal value As Integer)
            intTegataSite = value
        End Set
    End Property
#End Region

#Region "振込先銀行コード"
    ''' <summary>
    ''' 振込先銀行コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strFrkmSakiGinkouCd As String
    ''' <summary>
    ''' 振込先銀行コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>振込先銀行コード </returns>
    ''' <remarks></remarks>
    <TableMap("frkm_saki_ginkou_cd")> _
    Public Property FrkmSakiGinkouCd() As String
        Get
            Return strFrkmSakiGinkouCd
        End Get
        Set(ByVal value As String)
            strFrkmSakiGinkouCd = value
        End Set
    End Property
#End Region

#Region "振込先支店コード"
    ''' <summary>
    ''' 振込先支店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strFrkmSakiSitenCd As String
    ''' <summary>
    ''' 振込先支店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>振込先支店コード </returns>
    ''' <remarks></remarks>
    <TableMap("frkm_saki_siten_cd")> _
    Public Property FrkmSakiSitenCd() As String
        Get
            Return strFrkmSakiSitenCd
        End Get
        Set(ByVal value As String)
            strFrkmSakiSitenCd = value
        End Set
    End Property
#End Region

#Region "振込先預金種別"
    ''' <summary>
    ''' 振込先預金種別
    ''' </summary>
    ''' <remarks></remarks>
    Private strFrkmSakiYokinSyubetu As String
    ''' <summary>
    ''' 振込先預金種別
    ''' </summary>
    ''' <value></value>
    ''' <returns>振込先預金種別 </returns>
    ''' <remarks></remarks>
    <TableMap("frkm_saki_yokin_syubetu")> _
    Public Property FrkmSakiYokinSyubetu() As String
        Get
            Return strFrkmSakiYokinSyubetu
        End Get
        Set(ByVal value As String)
            strFrkmSakiYokinSyubetu = value
        End Set
    End Property
#End Region

#Region "振込先口座番号"
    ''' <summary>
    ''' 振込先口座番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strFrkmSakiKouzaNo As String
    ''' <summary>
    ''' 振込先口座番号
    ''' </summary>
    ''' <value></value>
    ''' <returns>振込先口座番号 </returns>
    ''' <remarks></remarks>
    <TableMap("frkm_saki_kouza_no")> _
    Public Property FrkmSakiKouzaNo() As String
        Get
            Return strFrkmSakiKouzaNo
        End Get
        Set(ByVal value As String)
            strFrkmSakiKouzaNo = value
        End Set
    End Property
#End Region

#Region "登録区分"
    ''' <summary>
    ''' 登録区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strTourokuKbn As String
    ''' <summary>
    ''' 登録区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>登録区分 </returns>
    ''' <remarks></remarks>
    <TableMap("touroku_kbn")> _
    Public Property TourokuKbn() As String
        Get
            Return strTourokuKbn
        End Get
        Set(ByVal value As String)
            strTourokuKbn = value
        End Set
    End Property
#End Region

#Region "登録日"
    ''' <summary>
    ''' 登録日
    ''' </summary>
    ''' <remarks></remarks>
    Private strTourokuBi As String
    ''' <summary>
    ''' 登録日
    ''' </summary>
    ''' <value></value>
    ''' <returns>登録日 </returns>
    ''' <remarks></remarks>
    <TableMap("touroku_bi")> _
    Public Property TourokuBi() As String
        Get
            Return strTourokuBi
        End Get
        Set(ByVal value As String)
            strTourokuBi = value
        End Set
    End Property
#End Region

#Region "支払先名_カナ"
    ''' <summary>
    ''' 支払先名_カナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriSakiMeiKana As String
    ''' <summary>
    ''' 支払先名_カナ
    ''' </summary>
    ''' <value></value>
    ''' <returns>支払先名_カナ </returns>
    ''' <remarks></remarks>
    <TableMap("shri_saki_mei_kana")> _
    Public Property ShriSakiMeiKana() As String
        Get
            Return strShriSakiMeiKana
        End Get
        Set(ByVal value As String)
            strShriSakiMeiKana = value
        End Set
    End Property
#End Region

#Region "支払先名_漢字"
    ''' <summary>
    ''' 支払先名_漢字
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriSakiMeiKanji As String
    ''' <summary>
    ''' 支払先名_漢字
    ''' </summary>
    ''' <value></value>
    ''' <returns>支払先名_漢字 </returns>
    ''' <remarks></remarks>
    <TableMap("shri_saki_mei_kanji")> _
    Public Property ShriSakiMeiKanji() As String
        Get
            Return strShriSakiMeiKanji
        End Get
        Set(ByVal value As String)
            strShriSakiMeiKanji = value
        End Set
    End Property
#End Region

#Region "支払通知区分"
    ''' <summary>
    ''' 支払通知区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriTuutiKbn As String
    ''' <summary>
    ''' 支払通知区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>支払通知区分 </returns>
    ''' <remarks></remarks>
    <TableMap("shri_tuuti_kbn")> _
    Public Property ShriTuutiKbn() As String
        Get
            Return strShriTuutiKbn
        End Get
        Set(ByVal value As String)
            strShriTuutiKbn = value
        End Set
    End Property
#End Region

#Region "振込手数料区分"
    ''' <summary>
    ''' 振込手数料区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strFurikomiTesuuryouKbn As String
    ''' <summary>
    ''' 振込手数料区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>振込手数料区分 </returns>
    ''' <remarks></remarks>
    <TableMap("furikomi_tesuuryou_kbn")> _
    Public Property FurikomiTesuuryouKbn() As String
        Get
            Return strFurikomiTesuuryouKbn
        End Get
        Set(ByVal value As String)
            strFurikomiTesuuryouKbn = value
        End Set
    End Property
#End Region

#Region "口座名義_カナ"
    ''' <summary>
    ''' 口座名義_カナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKouzaMeigiKana As String
    ''' <summary>
    ''' 口座名義_カナ
    ''' </summary>
    ''' <value></value>
    ''' <returns>口座名義_カナ </returns>
    ''' <remarks></remarks>
    <TableMap("kouza_meigi_kana")> _
    Public Property KouzaMeigiKana() As String
        Get
            Return strKouzaMeigiKana
        End Get
        Set(ByVal value As String)
            strKouzaMeigiKana = value
        End Set
    End Property
#End Region

#Region "消費税区分"
    ''' <summary>
    ''' 消費税区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhizeiKbn As String
    ''' <summary>
    ''' 消費税区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>消費税区分 </returns>
    ''' <remarks></remarks>
    <TableMap("syouhizei_kbn")> _
    Public Property SyouhizeiKbn() As String
        Get
            Return strSyouhizeiKbn
        End Get
        Set(ByVal value As String)
            strSyouhizeiKbn = value
        End Set
    End Property
#End Region

#Region "源泉支払内容区分"
    ''' <summary>
    ''' 源泉支払内容区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strGensenShriNaiyouKbn As String
    ''' <summary>
    ''' 源泉支払内容区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>源泉支払内容区分 </returns>
    ''' <remarks></remarks>
    <TableMap("gensen_shri_naiyou_kbn")> _
    Public Property GensenShriNaiyouKbn() As String
        Get
            Return strGensenShriNaiyouKbn
        End Get
        Set(ByVal value As String)
            strGensenShriNaiyouKbn = value
        End Set
    End Property
#End Region

#Region "納付区分"
    ''' <summary>
    ''' 納付区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strNoufuKbn As String
    ''' <summary>
    ''' 納付区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>納付区分 </returns>
    ''' <remarks></remarks>
    <TableMap("noufu_kbn")> _
    Public Property NoufuKbn() As String
        Get
            Return strNoufuKbn
        End Get
        Set(ByVal value As String)
            strNoufuKbn = value
        End Set
    End Property
#End Region

#Region "相殺優先区分"
    ''' <summary>
    ''' 相殺優先区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strSousaiYuusenKbn As String
    ''' <summary>
    ''' 相殺優先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>相殺優先区分 </returns>
    ''' <remarks></remarks>
    <TableMap("sousai_yuusen_kbn")> _
    Public Property SousaiYuusenKbn() As String
        Get
            Return strSousaiYuusenKbn
        End Get
        Set(ByVal value As String)
            strSousaiYuusenKbn = value
        End Set
    End Property
#End Region

#Region "海外送金区分"
    ''' <summary>
    ''' 海外送金区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaigaiSoukinKbn As String
    ''' <summary>
    ''' 海外送金区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>海外送金区分 </returns>
    ''' <remarks></remarks>
    <TableMap("kaigai_soukin_kbn")> _
    Public Property KaigaiSoukinKbn() As String
        Get
            Return strKaigaiSoukinKbn
        End Get
        Set(ByVal value As String)
            strKaigaiSoukinKbn = value
        End Set
    End Property
#End Region

#Region "下請業者区分"
    ''' <summary>
    ''' 下請業者区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strSitaukeGyousyaKbn As String
    ''' <summary>
    ''' 下請業者区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>下請業者区分 </returns>
    ''' <remarks></remarks>
    <TableMap("sitauke_gyousya_kbn")> _
    Public Property SitaukeGyousyaKbn() As String
        Get
            Return strSitaukeGyousyaKbn
        End Get
        Set(ByVal value As String)
            strSitaukeGyousyaKbn = value
        End Set
    End Property
#End Region

#Region "郵便番号"
    ''' <summary>
    ''' 郵便番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strYuubinNo As String
    ''' <summary>
    ''' 郵便番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 郵便番号</returns>
    ''' <remarks></remarks>
    <TableMap("yuubin_no")> _
    Public Property YuubinNo() As String
        Get
            Return strYuubinNo
        End Get
        Set(ByVal value As String)
            strYuubinNo = value
        End Set
    End Property
#End Region

#Region "市町村コード"
    ''' <summary>
    ''' 市町村コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSityousonCd As String
    ''' <summary>
    ''' 市町村コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("sityouson_cd")> _
    Public Property SityousonCd() As String
        Get
            Return strSityousonCd
        End Get
        Set(ByVal value As String)
            strSityousonCd = value
        End Set
    End Property
#End Region

#Region "住所_カナ"
    ''' <summary>
    ''' 住所_カナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyuusyoKana As String
    ''' <summary>
    ''' 住所_カナ
    ''' </summary>
    ''' <value></value>
    ''' <returns>住所_カナ </returns>
    ''' <remarks></remarks>
    <TableMap("jyuusyo_kana")> _
    Public Property JyuusyoKana() As String
        Get
            Return strJyuusyoKana
        End Get
        Set(ByVal value As String)
            strJyuusyoKana = value
        End Set
    End Property
#End Region

#Region "住所_漢字"
    ''' <summary>
    ''' 住所_漢字
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyuusyoKanji As String
    ''' <summary>
    ''' 住所_漢字
    ''' </summary>
    ''' <value></value>
    ''' <returns>住所_漢字 </returns>
    ''' <remarks></remarks>
    <TableMap("jyuusyo_kanji")> _
    Public Property JyuusyoKanji() As String
        Get
            Return strJyuusyoKanji
        End Get
        Set(ByVal value As String)
            strJyuusyoKanji = value
        End Set
    End Property
#End Region

#Region "電話番号"
    ''' <summary>
    ''' 電話番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strTelNo As String
    ''' <summary>
    ''' 電話番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 電話番号</returns>
    ''' <remarks></remarks>
    <TableMap("tel_no")> _
    Public Property TelNo() As String
        Get
            Return strTelNo
        End Get
        Set(ByVal value As String)
            strTelNo = value
        End Set
    End Property
#End Region

#Region "支払物件ナンバー"
    ''' <summary>
    ''' 支払物件ナンバー
    ''' </summary>
    ''' <remarks></remarks>
    Private intShriBukkenNo As Integer
    ''' <summary>
    ''' 支払物件ナンバー
    ''' </summary>
    ''' <value></value>
    ''' <returns>支払物件ナンバー </returns>
    ''' <remarks></remarks>
    <TableMap("shri_bukken_no")> _
    Public Property ShriBukkenNo() As Integer
        Get
            Return intShriBukkenNo
        End Get
        Set(ByVal value As Integer)
            intShriBukkenNo = value
        End Set
    End Property
#End Region

#Region "支払先マスター未使用項目"
    ''' <summary>
    ''' 支払先マスター未使用項目
    ''' </summary>
    ''' <remarks></remarks>
    Private strMisiyouKoumoku As String
    ''' <summary>
    ''' 支払先マスター未使用項目
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    <TableMap("misiyou_koumoku")> _
    Public Property MisiyouKoumoku() As String
        Get
            Return strMisiyouKoumoku
        End Get
        Set(ByVal value As String)
            strMisiyouKoumoku = value
        End Set
    End Property
#End Region

#Region "振込指定金額"
    ''' <summary>
    ''' 振込指定金額
    ''' </summary>
    ''' <remarks></remarks>
    Private lngFurikomiSiteiKingaku As Long
    ''' <summary>
    ''' 振込指定金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>振込指定金額 </returns>
    ''' <remarks></remarks>
    <TableMap("furikomi_sitei_kingaku")> _
    Public Property FurikomiSiteiKingaku() As Long
        Get
            Return lngFurikomiSiteiKingaku
        End Get
        Set(ByVal value As Long)
            lngFurikomiSiteiKingaku = value
        End Set
    End Property
#End Region

#Region "取消日"
    ''' <summary>
    ''' 取消日
    ''' </summary>
    ''' <remarks></remarks>
    Private strTorikesiBi As String
    ''' <summary>
    ''' 取消日
    ''' </summary>
    ''' <value></value>
    ''' <returns>取消日 </returns>
    ''' <remarks></remarks>
    <TableMap("torikesi_bi")> _
    Public Property TorikesiBi() As String
        Get
            Return strTorikesiBi
        End Get
        Set(ByVal value As String)
            strTorikesiBi = value
        End Set
    End Property
#End Region

#Region "大名寄コード_旧"
    ''' <summary>
    ''' 大名寄コード_旧
    ''' </summary>
    ''' <remarks></remarks>
    Private strOoNayoseCdOld As String
    ''' <summary>
    ''' 大名寄コード_旧
    ''' </summary>
    ''' <value></value>
    ''' <returns>大名寄コード_旧 </returns>
    ''' <remarks></remarks>
    <TableMap("oo_nayose_cd_old")> _
    Public Property OoNayoseCdOld() As String
        Get
            Return strOoNayoseCdOld
        End Get
        Set(ByVal value As String)
            strOoNayoseCdOld = value
        End Set
    End Property
#End Region

#Region "業者区分"
    ''' <summary>
    ''' 業者区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strGyousyaKbn As String
    ''' <summary>
    ''' 業者区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>業者区分 </returns>
    ''' <remarks></remarks>
    <TableMap("gyousya_kbn")> _
    Public Property GyousyaKbn() As String
        Get
            Return strGyousyaKbn
        End Get
        Set(ByVal value As String)
            strGyousyaKbn = value
        End Set
    End Property
#End Region

#Region "資本金区分"
    ''' <summary>
    ''' 資本金区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strSihonkinKbn As String
    ''' <summary>
    ''' 資本金区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>資本金区分 </returns>
    ''' <remarks></remarks>
    <TableMap("sihonkin_kbn")> _
    Public Property SihonkinKbn() As String
        Get
            Return strSihonkinKbn
        End Get
        Set(ByVal value As String)
            strSihonkinKbn = value
        End Set
    End Property
#End Region

#Region "資本金額"
    ''' <summary>
    ''' 資本金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intSihonkinGaku As Integer
    ''' <summary>
    ''' 資本金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>資本金額 </returns>
    ''' <remarks></remarks>
    <TableMap("sihonkin_gaku")> _
    Public Property SihonkinGaku() As Integer
        Get
            Return intSihonkinGaku
        End Get
        Set(ByVal value As Integer)
            intSihonkinGaku = value
        End Set
    End Property
#End Region

#Region "先方請求書締切日"
    ''' <summary>
    ''' 先方請求書締切日
    ''' </summary>
    ''' <remarks></remarks>
    Private strSenpouSkysySimekiriBi As String
    ''' <summary>
    ''' 先方請求書締切日
    ''' </summary>
    ''' <value></value>
    ''' <returns>先方請求書締切日 </returns>
    ''' <remarks></remarks>
    <TableMap("senpou_skysy_simekiri_bi")> _
    Public Property SenpouSkysySimekiriBi() As String
        Get
            Return strSenpouSkysySimekiriBi
        End Get
        Set(ByVal value As String)
            strSenpouSkysySimekiriBi = value
        End Set
    End Property
#End Region

#Region "支払先名_漢字_44桁"
    ''' <summary>
    ''' 支払先名_漢字_44桁
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriSakiMeiKanji44 As String
    ''' <summary>
    ''' 支払先名_漢字_44桁
    ''' </summary>
    ''' <value></value>
    ''' <returns>支払先名_漢字_44桁 </returns>
    ''' <remarks></remarks>
    <TableMap("shri_saki_mei_kanji_44")> _
    Public Property ShriSakiMeiKanji44() As String
        Get
            Return strShriSakiMeiKanji44
        End Get
        Set(ByVal value As String)
            strShriSakiMeiKanji44 = value
        End Set
    End Property
#End Region

#Region "利用停止日"
    ''' <summary>
    ''' 利用停止日
    ''' </summary>
    ''' <remarks></remarks>
    Private strRiyouTeisiBi As String
    ''' <summary>
    ''' 利用停止日
    ''' </summary>
    ''' <value></value>
    ''' <returns>利用停止日 </returns>
    ''' <remarks></remarks>
    <TableMap("riyou_teisi_bi")> _
    Public Property RiyouTeisiBi() As String
        Get
            Return strRiyouTeisiBi
        End Get
        Set(ByVal value As String)
            strRiyouTeisiBi = value
        End Set
    End Property
#End Region

#Region "最終更新日時"
    ''' <summary>
    ''' 最終更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSaisyuuKousinNitiji As DateTime
    ''' <summary>
    ''' 最終更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns>最終更新日時 </returns>
    ''' <remarks></remarks>
    <TableMap("saisyuu_kousin_nitiji")> _
    Public Property SaisyuuKousinNitiji() As DateTime
        Get
            Return dateSaisyuuKousinNitiji
        End Get
        Set(ByVal value As DateTime)
            dateSaisyuuKousinNitiji = value
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