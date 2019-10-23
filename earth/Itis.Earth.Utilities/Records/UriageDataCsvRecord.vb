''' <summary>
''' 売上データCSVのレコードクラスです
''' </summary>
''' <remarks></remarks>
Public Class UriageDataCsvRecord

#Region "伝区"
    ''' <summary>
    ''' 伝区
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenku As String
    ''' <summary>
    ''' 伝区
    ''' </summary>
    ''' <value></value>
    ''' <returns>伝区</returns>
    ''' <remarks></remarks>
    <TableMap("denku")> _
    Public Property Denku() As String
        Get
            Return strDenku
        End Get
        Set(ByVal value As String)
            strDenku = value
        End Set
    End Property
#End Region

#Region "売上年月日"
    ''' <summary>
    ''' 売上年月日
    ''' </summary>
    ''' <remarks>邸別請求.売上年月日</remarks>
    Private dateUriDate As DateTime
    ''' <summary>
    ''' 売上年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上年月日</returns>
    ''' <remarks>邸別請求.売上年月日</remarks>
    <TableMap("uri_date")> _
    Public Property UriDate() As DateTime
        Get
            Return dateUriDate
        End Get
        Set(ByVal value As DateTime)
            dateUriDate = value
        End Set
    End Property
#End Region

#Region "請求書発行日"
    ''' <summary>
    ''' 請求書発行日
    ''' </summary>
    ''' <remarks>邸別請求.請求書発行日</remarks>
    Private dateSeikyuusyoHakDate As DateTime
    ''' <summary>
    ''' 請求書発行日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書発行日</returns>
    ''' <remarks>邸別請求.請求書発行日</remarks>
    <TableMap("seikyuusyo_hak_date")> _
    Public Property SeikyuusyoHakDate() As DateTime
        Get
            Return dateSeikyuusyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuusyoHakDate = value
        End Set
    End Property
#End Region

#Region "得意先コード"
    ''' <summary>
    ''' 得意先コード
    ''' </summary>
    ''' <remarks>加盟店.調査請求先</remarks>
    Private strTokuiSakiCd As String
    ''' <summary>
    ''' 得意先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>得意先コード</returns>
    ''' <remarks>加盟店.調査請求先</remarks>
    <TableMap("tys_seikyuu_saki")> _
    Public Property TokuiSakiCd() As String
        Get
            Return strTokuiSakiCd
        End Get
        Set(ByVal value As String)
            strTokuiSakiCd = value
        End Set
    End Property
#End Region

#Region "得意先名"
    ''' <summary>
    ''' 得意先名
    ''' </summary>
    ''' <remarks>加盟店.加盟店名1</remarks>
    Private strTokuiSakiName As String
    ''' <summary>
    ''' 得意先名
    ''' </summary>
    ''' <value></value>
    ''' <returns>得意先名</returns>
    ''' <remarks>加盟店.加盟店名1</remarks>
    <TableMap("kameiten_mei1")> _
    Public Property TokuiSakiName() As String
        Get
            Return strTokuiSakiName
        End Get
        Set(ByVal value As String)
            strTokuiSakiName = value
        End Set
    End Property
#End Region

#Region "直送先コード"
    ''' <summary>
    ''' 直送先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyokusouSakiCd As String
    ''' <summary>
    ''' 直送先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>直送先コード</returns>
    ''' <remarks></remarks>
    <TableMap("tyokusou_saki_cd")> _
    Public Property TyokusouSakiCd() As String
        Get
            Return strTyokusouSakiCd
        End Get
        Set(ByVal value As String)
            strTyokusouSakiCd = value
        End Set
    End Property
#End Region

#Region "先方担当者名"
    ''' <summary>
    ''' 先方担当者名
    ''' </summary>
    ''' <remarks>加盟店.系列コード</remarks>
    Private strSenpouTantousyaName As String
    ''' <summary>
    ''' 先方担当者名
    ''' </summary>
    ''' <value></value>
    ''' <returns>先方担当者名</returns>
    ''' <remarks>加盟店.系列コード</remarks>
    <TableMap("keiretu_cd")> _
    Public Property SenpouTantousyaName() As String
        Get
            Return strSenpouTantousyaName
        End Get
        Set(ByVal value As String)
            strSenpouTantousyaName = value
        End Set
    End Property
#End Region

#Region "部門コード"
    ''' <summary>
    ''' 部門コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strBumonCd As String
    ''' <summary>
    ''' 部門コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>部門コード</returns>
    ''' <remarks></remarks>
    <TableMap("bumon_cd")> _
    Public Property BumonCd() As String
        Get
            Return strBumonCd
        End Get
        Set(ByVal value As String)
            strBumonCd = value
        End Set
    End Property
#End Region

#Region "担当者コード"
    ''' <summary>
    ''' 担当者コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTantousyaCd As String
    ''' <summary>
    ''' 担当者コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>担当者コード</returns>
    ''' <remarks></remarks>
    <TableMap("tantousya_cd")> _
    Public Property TantousyaCd() As String
        Get
            Return strTantousyaCd
        End Get
        Set(ByVal value As String)
            strTantousyaCd = value
        End Set
    End Property
#End Region

#Region "摘要コード"
    ''' <summary>
    ''' 摘要コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTekiyouCd As String
    ''' <summary>
    ''' 摘要コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>摘要コード</returns>
    ''' <remarks></remarks>
    <TableMap("tekiyou_cd")> _
    Public Property TekiyouCd() As String
        Get
            Return strTekiyouCd
        End Get
        Set(ByVal value As String)
            strTekiyouCd = value
        End Set
    End Property
#End Region

#Region "摘要名"
    ''' <summary>
    ''' 摘要名
    ''' </summary>
    ''' <remarks>地盤.施主名</remarks>
    Private strTekiyouName As String
    ''' <summary>
    ''' 摘要名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 摘要名</returns>
    ''' <remarks>地盤.施主名</remarks>
    <TableMap("sesyu_mei")> _
    Public Property TekiyouName() As String
        Get
            Return strTekiyouName
        End Get
        Set(ByVal value As String)
            strTekiyouName = value
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
    ''' <returns>分類コード</returns>
    ''' <remarks></remarks>
    <TableMap("bunrui_cd")> _
    Public Property BunruiCd() As String
        Get
            Return strBunruiCd
        End Get
        Set(ByVal value As String)
            strBunruiCd = value
        End Set
    End Property
#End Region

#Region "伝票区分"
    ''' <summary>
    ''' 伝票区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenpyouKbn As String
    ''' <summary>
    ''' 伝票区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>伝票区分</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_kbn")> _
    Public Property DenpyouKbn() As String
        Get
            Return strDenpyouKbn
        End Get
        Set(ByVal value As String)
            strDenpyouKbn = value
        End Set
    End Property
#End Region

#Region "商品コード"
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <remarks>商品.商品コード</remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品コード</returns>
    ''' <remarks>商品.商品コード</remarks>
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

#Region "マスタ区分"
    ''' <summary>
    ''' マスタ区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strMastaKbn As String
    ''' <summary>
    ''' マスタ区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>マスタ区分</returns>
    ''' <remarks></remarks>
    <TableMap("masta_kbn")> _
    Public Property MastaKbn() As String
        Get
            Return strMastaKbn
        End Get
        Set(ByVal value As String)
            strMastaKbn = value
        End Set
    End Property
#End Region

#Region "品名"
    ''' <summary>
    ''' 品名
    ''' </summary>
    ''' <remarks>商品.商品名</remarks>
    Private strHinMei As String
    ''' <summary>
    ''' 品名
    ''' </summary>
    ''' <value></value>
    ''' <returns>品名</returns>
    ''' <remarks>商品.商品名</remarks>
    <TableMap("syouhin_mei")> _
    Public Property HinMei() As String
        Get
            Return strHinMei
        End Get
        Set(ByVal value As String)
            strHinMei = value
        End Set
    End Property
#End Region

#Region "区"
    ''' <summary>
    ''' 区
    ''' </summary>
    ''' <remarks></remarks>
    Private strKu As String
    ''' <summary>
    ''' 区
    ''' </summary>
    ''' <value></value>
    ''' <returns>区</returns>
    ''' <remarks></remarks>
    <TableMap("ku")> _
    Public Property Ku() As String
        Get
            Return strKu
        End Get
        Set(ByVal value As String)
            strKu = value
        End Set
    End Property
#End Region

#Region "倉庫コード"
    ''' <summary>
    ''' 倉庫コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSoukoCd As String
    ''' <summary>
    ''' 倉庫コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>倉庫コード</returns>
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
#End Region

#Region "入数"
    ''' <summary>
    ''' 入数
    ''' </summary>
    ''' <remarks></remarks>
    Private intIriSuu As Integer
    ''' <summary>
    ''' 入数
    ''' </summary>
    ''' <value></value>
    ''' <returns>入数</returns>
    ''' <remarks></remarks>
    <TableMap("iri_suu")> _
    Public Property IriSuu() As Integer
        Get
            Return intIriSuu
        End Get
        Set(ByVal value As Integer)
            intIriSuu = value
        End Set
    End Property
#End Region

#Region "箱数"
    ''' <summary>
    ''' 箱数
    ''' </summary>
    ''' <remarks></remarks>
    Private intHakoSuu As Integer
    ''' <summary>
    ''' 箱数
    ''' </summary>
    ''' <value></value>
    ''' <returns>箱数</returns>
    ''' <remarks></remarks>
    <TableMap("hako_suu")> _
    Public Property HakoSuu() As Integer
        Get
            Return intHakoSuu
        End Get
        Set(ByVal value As Integer)
            intHakoSuu = value
        End Set
    End Property
#End Region

#Region "数量"
    ''' <summary>
    ''' 数量
    ''' </summary>
    ''' <remarks>邸別請求.数量</remarks>
    Private intSuuryou As Integer
    ''' <summary>
    ''' 数量
    ''' </summary>
    ''' <value></value>
    ''' <returns>数量</returns>
    ''' <remarks>邸別請求.数量</remarks>
    <TableMap("suuryou")> _
    Public Property Suuryou() As Integer
        Get
            Return intSuuryou
        End Get
        Set(ByVal value As Integer)
            intSuuryou = value
        End Set
    End Property
#End Region

#Region "単位"
    ''' <summary>
    ''' 単位
    ''' </summary>
    ''' <remarks>商品.単位</remarks>
    Private strTani As String
    ''' <summary>
    ''' 単位
    ''' </summary>
    ''' <value></value>
    ''' <returns> 単位</returns>
    ''' <remarks>商品.単位</remarks>
    <TableMap("tani")> _
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
    ''' <remarks>邸別請求.単価</remarks>
    Private intTanka As Integer
    ''' <summary>
    ''' 単価
    ''' </summary>
    ''' <value></value>
    ''' <returns> 単価</returns>
    ''' <remarks>邸別請求.単価</remarks>
    <TableMap("tanka")> _
    Public Property Tanka() As Integer
        Get
            Return intTanka
        End Get
        Set(ByVal value As Integer)
            intTanka = value
        End Set
    End Property
#End Region

#Region "原単価"
    ''' <summary>
    ''' 原単価
    ''' </summary>
    ''' <remarks></remarks>
    Private intGenTanka As Integer
    ''' <summary>
    ''' 原単価
    ''' </summary>
    ''' <value></value>
    ''' <returns>原単価</returns>
    ''' <remarks></remarks>
    <TableMap("gen_tanka")> _
    Public Property GenTanka() As Integer
        Get
            Return intGenTanka
        End Get
        Set(ByVal value As Integer)
            intGenTanka = value
        End Set
    End Property
#End Region

#Region "原価額"
    ''' <summary>
    ''' 原価額
    ''' </summary>
    ''' <remarks></remarks>
    Private intGenkaGaku As Integer
    ''' <summary>
    ''' 原価額
    ''' </summary>
    ''' <value></value>
    ''' <returns>原価額</returns>
    ''' <remarks></remarks>
    <TableMap("genka_gaku")> _
    Public Property GenkaGaku() As Integer
        Get
            Return intGenkaGaku
        End Get
        Set(ByVal value As Integer)
            intGenkaGaku = value
        End Set
    End Property
#End Region

#Region "粗利益"
    ''' <summary>
    ''' 粗利益
    ''' </summary>
    ''' <remarks></remarks>
    Private intAraRieki As Integer
    ''' <summary>
    ''' 粗利益
    ''' </summary>
    ''' <value></value>
    ''' <returns>粗利益</returns>
    ''' <remarks></remarks>
    <TableMap("ara_rieki")> _
    Public Property AraRieki() As Integer
        Get
            Return intAraRieki
        End Get
        Set(ByVal value As Integer)
            intAraRieki = value
        End Set
    End Property
#End Region

#Region "外税額"
    ''' <summary>
    ''' 外税額
    ''' </summary>
    ''' <remarks></remarks>
    Private intSotoZeiGaku As Integer
    ''' <summary>
    ''' 外税額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("sotozei_gaku")> _
    Public Property SotoZeiGaku() As Integer
        Get
            Return intSotoZeiGaku
        End Get
        Set(ByVal value As Integer)
            intSotoZeiGaku = value
        End Set
    End Property
#End Region

#Region "内税額"
    ''' <summary>
    ''' 内税額
    ''' </summary>
    ''' <remarks></remarks>
    Private intUtiZeiGaku As Integer
    ''' <summary>
    ''' 内税額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("utizei_gaku")> _
    Public Property UtiZeiGaku() As Integer
        Get
            Return intUtiZeiGaku
        End Get
        Set(ByVal value As Integer)
            intUtiZeiGaku = value
        End Set
    End Property
#End Region

#Region "税区分"
    ''' <summary>
    ''' 税区分
    ''' </summary>
    ''' <remarks>邸別請求.税区分</remarks>
    Private strZeiKbn As String
    ''' <summary>
    ''' 税区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 税区分</returns>
    ''' <remarks>邸別請求.税区分</remarks>
    <TableMap("zei_kbn")> _
    Public Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
        End Set
    End Property
#End Region

#Region "税込区分"
    ''' <summary>
    ''' 税込区分
    ''' </summary>
    ''' <remarks>商品マスタ.税込区分</remarks>
    Private strZeikomiKbn As String
    ''' <summary>
    ''' 税込区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 税込区分</returns>
    ''' <remarks>商品マスタ.税込区分</remarks>
    <TableMap("zeikomi_kbn")> _
    Public Property ZeikomiKbn() As String
        Get
            Return strZeikomiKbn
        End Get
        Set(ByVal value As String)
            strZeikomiKbn = value
        End Set
    End Property
#End Region

#Region "備考"
    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <remarks>邸別請求.区分 + 邸別請求.保証書NO</remarks>
    Private strBikou As String
    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <value></value>
    ''' <returns> 備考</returns>
    ''' <remarks>邸別請求.区分 + 邸別請求.保証書NO</remarks>
    <TableMap("bikou")> _
    Public Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "標準価格"
    ''' <summary>
    ''' 標準価格
    ''' </summary>
    ''' <remarks></remarks>
    Private intHyoujunKakaku As Integer
    ''' <summary>
    ''' 標準価格
    ''' </summary>
    ''' <value></value>
    ''' <returns>標準価格</returns>
    ''' <remarks></remarks>
    <TableMap("hyoujun_kakaku")> _
    Public Property HyoujunKakaku()
        Get
            Return intHyoujunKakaku
        End Get
        Set(ByVal value)
            intHyoujunKakaku = value
        End Set
    End Property
#End Region

#Region "同時入荷区分"
    ''' <summary>
    ''' 同時入荷区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strDoujiNyuukaKbn As String
    ''' <summary>
    ''' 同時入荷区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>同時入荷区分</returns>
    ''' <remarks></remarks>
    <TableMap("douji_nyuuka_kbn")> _
    Public Property DoujiNyuukaKbn() As String
        Get
            Return strDoujiNyuukaKbn
        End Get
        Set(ByVal value As String)
            strDoujiNyuukaKbn = value
        End Set
    End Property
#End Region

#Region "売単価"
    ''' <summary>
    ''' 売単価
    ''' </summary>
    ''' <remarks></remarks>
    Private intBaiTanka As Integer
    ''' <summary>
    ''' 売単価
    ''' </summary>
    ''' <value></value>
    ''' <returns>売単価</returns>
    ''' <remarks></remarks>
    <TableMap("bai_tanka")> _
    Public Property BaiTanka() As Integer
        Get
            Return intBaiTanka
        End Get
        Set(ByVal value As Integer)
            intBaiTanka = value
        End Set
    End Property
#End Region

#Region "売価金額"
    ''' <summary>
    ''' 売価金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intBaikaKingaku As Integer
    ''' <summary>
    ''' 売価金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>売価金額</returns>
    ''' <remarks></remarks>
    <TableMap("baika_kingaku")> _
    Public Property BaikaKingaku() As Integer
        Get
            Return intBaikaKingaku
        End Get
        Set(ByVal value As Integer)
            intBaikaKingaku = value
        End Set
    End Property
#End Region

#Region "規格･型番"
    ''' <summary>
    ''' 規格･型番
    ''' </summary>
    ''' <remarks></remarks>
    Private strKikakuKataBan As String
    ''' <summary>
    ''' 規格･型番
    ''' </summary>
    ''' <value></value>
    ''' <returns>規格･型番</returns>
    ''' <remarks></remarks>
    <TableMap("kikaku_kataban")> _
    Public Property KikakuKataBan() As String
        Get
            Return strKikakuKataBan
        End Get
        Set(ByVal value As String)
            strKikakuKataBan = value
        End Set
    End Property
#End Region

#Region "色"
    ''' <summary>
    ''' 色
    ''' </summary>
    ''' <remarks></remarks>
    Private strColor As String
    ''' <summary>
    ''' 色
    ''' </summary>
    ''' <value></value>
    ''' <returns>色</returns>
    ''' <remarks></remarks>
    <TableMap("color")> _
    Public Property Color() As String
        Get
            Return strColor
        End Get
        Set(ByVal value As String)
            strColor = value
        End Set
    End Property
#End Region

#Region "サイズ"
    ''' <summary>
    ''' サイズ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSize As String
    ''' <summary>
    ''' サイズ
    ''' </summary>
    ''' <value></value>
    ''' <returns>サイズ</returns>
    ''' <remarks></remarks>
    <TableMap("size")> _
    Public Property Size() As String
        Get
            Return strSize
        End Get
        Set(ByVal value As String)
            strSize = value
        End Set
    End Property
#End Region

#Region "売上金額"
    ''' <summary>
    ''' 売上金額
    ''' </summary>
    ''' <remarks>邸別請求.数量 * 邸別請求.単価</remarks>
    Private intUriageKingaku As Integer
    ''' <summary>
    ''' 売上金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>売上金額</returns>
    ''' <remarks>邸別請求.数量 * 邸別請求.単価</remarks>
    <TableMap("uriage_kingaku")> _
    Public Property UriageKingaku() As Integer
        Get
            Return intUriageKingaku
        End Get
        Set(ByVal value As Integer)
            intUriageKingaku = value
        End Set
    End Property
#End Region

#Region "伝票NO"
    ''' <summary>
    ''' 伝票NO
    ''' </summary>
    ''' <remarks>伝票.最終伝票Noをカウントアップ</remarks>
    Private intDenpyouNo As Integer
    ''' <summary>
    ''' 伝票NO
    ''' </summary>
    ''' <value></value>
    ''' <returns>伝票NO</returns>
    ''' <remarks>伝票.最終伝票Noをカウントアップ</remarks>
    <TableMap("denpyou_no")> _
    Public Property DenpyouNo() As Integer
        Get
            Return intDenpyouNo
        End Get
        Set(ByVal value As Integer)
            intDenpyouNo = value
        End Set
    End Property
#End Region

#Region "更新KEY1"
    ''' <summary>
    ''' 更新KEY1
    ''' </summary>
    ''' <remarks>各テーブルの更新用KEY</remarks>
    Private strUpdateKey1 As String
    ''' <summary>
    ''' 更新KEY1
    ''' </summary>
    ''' <value></value>
    ''' <returns>更新KEY1</returns>
    ''' <remarks>各テーブルの更新用KEY</remarks>
    <TableMap("update_key1")> _
    Public Property UpdateKey1() As String
        Get
            Return strUpdateKey1
        End Get
        Set(ByVal value As String)
            strUpdateKey1 = value
        End Set
    End Property
#End Region

#Region "更新KEY2"
    ''' <summary>
    ''' 更新KEY1
    ''' </summary>
    ''' <remarks>各テーブルの更新用KEY</remarks>
    Private strUpdateKey2 As String
    ''' <summary>
    ''' 更新KEY1
    ''' </summary>
    ''' <value></value>
    ''' <returns>更新KEY1</returns>
    ''' <remarks>各テーブルの更新用KEY</remarks>
    <TableMap("update_key2")> _
    Public Property UpdateKey2() As String
        Get
            Return strUpdateKey2
        End Get
        Set(ByVal value As String)
            strUpdateKey2 = value
        End Set
    End Property
#End Region

#Region "更新KEY3"
    ''' <summary>
    ''' 更新KEY3
    ''' </summary>
    ''' <remarks>各テーブルの更新用KEY</remarks>
    Private strUpdateKey3 As String
    ''' <summary>
    ''' 更新KEY3
    ''' </summary>
    ''' <value></value>
    ''' <returns>更新KEY3</returns>
    ''' <remarks>各テーブルの更新用KEY</remarks>
    <TableMap("update_key3")> _
    Public Property UpdateKey3() As String
        Get
            Return strUpdateKey3
        End Get
        Set(ByVal value As String)
            strUpdateKey3 = value
        End Set
    End Property
#End Region

#Region "更新KEY4"
    ''' <summary>
    ''' 更新KEY4
    ''' </summary>
    ''' <remarks>各テーブルの更新用KEY</remarks>
    Private strUpdateKey4 As String
    ''' <summary>
    ''' 更新KEY4
    ''' </summary>
    ''' <value></value>
    ''' <returns>更新KEY4</returns>
    ''' <remarks>各テーブルの更新用KEY</remarks>
    <TableMap("update_key4")> _
    Public Property UpdateKey4() As String
        Get
            Return strUpdateKey4
        End Get
        Set(ByVal value As String)
            strUpdateKey4 = value
        End Set
    End Property
#End Region

End Class
