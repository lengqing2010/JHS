Public Class SinkaikeiSiharaiSakiRecord

#Region "VïvÆR[h"
    ''' <summary>
    ''' VïvÆR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkkJigyouCd As String
    ''' <summary>
    ''' VïvÆR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns>VïvÆR[h </returns>
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

#Region "Vïvx¥æR[h"
    ''' <summary>
    ''' Vïvx¥æR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkkShriSakiCd As String
    ''' <summary>
    ''' Vïvx¥æR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> Vïvx¥æR[h</returns>
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

#Region "¬¼ñR[h"
    ''' <summary>
    ''' ¬¼ñR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strKoNayoseCd As String
    ''' <summary>
    ''' ¬¼ñR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns>¬¼ñR[h </returns>
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

#Region "å¼ñR[h"
    ''' <summary>
    ''' å¼ñR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strOoNayoseCd As String
    ''' <summary>
    ''' å¼ñR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns>å¼ñR[h </returns>
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

#Region "KEYx¥æ¼"
    ''' <summary>
    ''' KEYx¥æ¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriSakiMei As String
    ''' <summary>
    ''' KEYx¥æ¼
    ''' </summary>
    ''' <value></value>
    ''' <returns>KEYx¥æ¼ </returns>
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

#Region "XVú"
    ''' <summary>
    ''' XVú
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinBi As String
    ''' <summary>
    ''' XVú
    ''' </summary>
    ''' <value></value>
    ''' <returns>XVú </returns>
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

#Region "x¥ú"
    ''' <summary>
    ''' x¥ú
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriBi As String
    ''' <summary>
    ''' x¥ú
    ''' </summary>
    ''' <value></value>
    ''' <returns>x¥ú </returns>
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

#Region "x¥û@"
    ''' <summary>
    ''' x¥û@
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriHouhou As String
    ''' <summary>
    ''' x¥û@
    ''' </summary>
    ''' <value></value>
    ''' <returns>x¥û@ </returns>
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

#Region "è`ä¦"
    ''' <summary>
    ''' è`ä¦
    ''' </summary>
    ''' <remarks></remarks>
    Private decTegataHiritu As Decimal
    ''' <summary>
    ''' è`ä¦
    ''' </summary>
    ''' <value></value>
    ''' <returns> è`ä¦</returns>
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

#Region "è`TCg"
    ''' <summary>
    ''' è`TCg
    ''' </summary>
    ''' <remarks></remarks>
    Private intTegataSite As Integer
    ''' <summary>
    ''' è`TCg
    ''' </summary>
    ''' <value></value>
    ''' <returns>è`TCg </returns>
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

#Region "UæâsR[h"
    ''' <summary>
    ''' UæâsR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strFrkmSakiGinkouCd As String
    ''' <summary>
    ''' UæâsR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns>UæâsR[h </returns>
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

#Region "UæxXR[h"
    ''' <summary>
    ''' UæxXR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strFrkmSakiSitenCd As String
    ''' <summary>
    ''' UæxXR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns>UæxXR[h </returns>
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

#Region "UæaàíÊ"
    ''' <summary>
    ''' UæaàíÊ
    ''' </summary>
    ''' <remarks></remarks>
    Private strFrkmSakiYokinSyubetu As String
    ''' <summary>
    ''' UæaàíÊ
    ''' </summary>
    ''' <value></value>
    ''' <returns>UæaàíÊ </returns>
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

#Region "UæûÀÔ"
    ''' <summary>
    ''' UæûÀÔ
    ''' </summary>
    ''' <remarks></remarks>
    Private strFrkmSakiKouzaNo As String
    ''' <summary>
    ''' UæûÀÔ
    ''' </summary>
    ''' <value></value>
    ''' <returns>UæûÀÔ </returns>
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

#Region "o^æª"
    ''' <summary>
    ''' o^æª
    ''' </summary>
    ''' <remarks></remarks>
    Private strTourokuKbn As String
    ''' <summary>
    ''' o^æª
    ''' </summary>
    ''' <value></value>
    ''' <returns>o^æª </returns>
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

#Region "o^ú"
    ''' <summary>
    ''' o^ú
    ''' </summary>
    ''' <remarks></remarks>
    Private strTourokuBi As String
    ''' <summary>
    ''' o^ú
    ''' </summary>
    ''' <value></value>
    ''' <returns>o^ú </returns>
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

#Region "x¥æ¼_Ji"
    ''' <summary>
    ''' x¥æ¼_Ji
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriSakiMeiKana As String
    ''' <summary>
    ''' x¥æ¼_Ji
    ''' </summary>
    ''' <value></value>
    ''' <returns>x¥æ¼_Ji </returns>
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

#Region "x¥æ¼_¿"
    ''' <summary>
    ''' x¥æ¼_¿
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriSakiMeiKanji As String
    ''' <summary>
    ''' x¥æ¼_¿
    ''' </summary>
    ''' <value></value>
    ''' <returns>x¥æ¼_¿ </returns>
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

#Region "x¥Êmæª"
    ''' <summary>
    ''' x¥Êmæª
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriTuutiKbn As String
    ''' <summary>
    ''' x¥Êmæª
    ''' </summary>
    ''' <value></value>
    ''' <returns>x¥Êmæª </returns>
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

#Region "Uè¿æª"
    ''' <summary>
    ''' Uè¿æª
    ''' </summary>
    ''' <remarks></remarks>
    Private strFurikomiTesuuryouKbn As String
    ''' <summary>
    ''' Uè¿æª
    ''' </summary>
    ''' <value></value>
    ''' <returns>Uè¿æª </returns>
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

#Region "ûÀ¼`_Ji"
    ''' <summary>
    ''' ûÀ¼`_Ji
    ''' </summary>
    ''' <remarks></remarks>
    Private strKouzaMeigiKana As String
    ''' <summary>
    ''' ûÀ¼`_Ji
    ''' </summary>
    ''' <value></value>
    ''' <returns>ûÀ¼`_Ji </returns>
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

#Region "ÁïÅæª"
    ''' <summary>
    ''' ÁïÅæª
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhizeiKbn As String
    ''' <summary>
    ''' ÁïÅæª
    ''' </summary>
    ''' <value></value>
    ''' <returns>ÁïÅæª </returns>
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

#Region "¹òx¥àeæª"
    ''' <summary>
    ''' ¹òx¥àeæª
    ''' </summary>
    ''' <remarks></remarks>
    Private strGensenShriNaiyouKbn As String
    ''' <summary>
    ''' ¹òx¥àeæª
    ''' </summary>
    ''' <value></value>
    ''' <returns>¹òx¥àeæª </returns>
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

#Region "[tæª"
    ''' <summary>
    ''' [tæª
    ''' </summary>
    ''' <remarks></remarks>
    Private strNoufuKbn As String
    ''' <summary>
    ''' [tæª
    ''' </summary>
    ''' <value></value>
    ''' <returns>[tæª </returns>
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

#Region "EDææª"
    ''' <summary>
    ''' EDææª
    ''' </summary>
    ''' <remarks></remarks>
    Private strSousaiYuusenKbn As String
    ''' <summary>
    ''' EDææª
    ''' </summary>
    ''' <value></value>
    ''' <returns>EDææª </returns>
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

#Region "COàæª"
    ''' <summary>
    ''' COàæª
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaigaiSoukinKbn As String
    ''' <summary>
    ''' COàæª
    ''' </summary>
    ''' <value></value>
    ''' <returns>COàæª </returns>
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

#Region "º¿ÆÒæª"
    ''' <summary>
    ''' º¿ÆÒæª
    ''' </summary>
    ''' <remarks></remarks>
    Private strSitaukeGyousyaKbn As String
    ''' <summary>
    ''' º¿ÆÒæª
    ''' </summary>
    ''' <value></value>
    ''' <returns>º¿ÆÒæª </returns>
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

#Region "XÖÔ"
    ''' <summary>
    ''' XÖÔ
    ''' </summary>
    ''' <remarks></remarks>
    Private strYuubinNo As String
    ''' <summary>
    ''' XÖÔ
    ''' </summary>
    ''' <value></value>
    ''' <returns> XÖÔ</returns>
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

#Region "s¬ºR[h"
    ''' <summary>
    ''' s¬ºR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSityousonCd As String
    ''' <summary>
    ''' s¬ºR[h
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

#Region "Z_Ji"
    ''' <summary>
    ''' Z_Ji
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyuusyoKana As String
    ''' <summary>
    ''' Z_Ji
    ''' </summary>
    ''' <value></value>
    ''' <returns>Z_Ji </returns>
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

#Region "Z_¿"
    ''' <summary>
    ''' Z_¿
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyuusyoKanji As String
    ''' <summary>
    ''' Z_¿
    ''' </summary>
    ''' <value></value>
    ''' <returns>Z_¿ </returns>
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

#Region "dbÔ"
    ''' <summary>
    ''' dbÔ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTelNo As String
    ''' <summary>
    ''' dbÔ
    ''' </summary>
    ''' <value></value>
    ''' <returns> dbÔ</returns>
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

#Region "x¥¨io["
    ''' <summary>
    ''' x¥¨io[
    ''' </summary>
    ''' <remarks></remarks>
    Private intShriBukkenNo As Integer
    ''' <summary>
    ''' x¥¨io[
    ''' </summary>
    ''' <value></value>
    ''' <returns>x¥¨io[ </returns>
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

#Region "x¥æ}X^[¢gpÚ"
    ''' <summary>
    ''' x¥æ}X^[¢gpÚ
    ''' </summary>
    ''' <remarks></remarks>
    Private strMisiyouKoumoku As String
    ''' <summary>
    ''' x¥æ}X^[¢gpÚ
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

#Region "Uwèàz"
    ''' <summary>
    ''' Uwèàz
    ''' </summary>
    ''' <remarks></remarks>
    Private lngFurikomiSiteiKingaku As Long
    ''' <summary>
    ''' Uwèàz
    ''' </summary>
    ''' <value></value>
    ''' <returns>Uwèàz </returns>
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

#Region "æÁú"
    ''' <summary>
    ''' æÁú
    ''' </summary>
    ''' <remarks></remarks>
    Private strTorikesiBi As String
    ''' <summary>
    ''' æÁú
    ''' </summary>
    ''' <value></value>
    ''' <returns>æÁú </returns>
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

#Region "å¼ñR[h_"
    ''' <summary>
    ''' å¼ñR[h_
    ''' </summary>
    ''' <remarks></remarks>
    Private strOoNayoseCdOld As String
    ''' <summary>
    ''' å¼ñR[h_
    ''' </summary>
    ''' <value></value>
    ''' <returns>å¼ñR[h_ </returns>
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

#Region "ÆÒæª"
    ''' <summary>
    ''' ÆÒæª
    ''' </summary>
    ''' <remarks></remarks>
    Private strGyousyaKbn As String
    ''' <summary>
    ''' ÆÒæª
    ''' </summary>
    ''' <value></value>
    ''' <returns>ÆÒæª </returns>
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

#Region "{àæª"
    ''' <summary>
    ''' {àæª
    ''' </summary>
    ''' <remarks></remarks>
    Private strSihonkinKbn As String
    ''' <summary>
    ''' {àæª
    ''' </summary>
    ''' <value></value>
    ''' <returns>{àæª </returns>
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

#Region "{àz"
    ''' <summary>
    ''' {àz
    ''' </summary>
    ''' <remarks></remarks>
    Private intSihonkinGaku As Integer
    ''' <summary>
    ''' {àz
    ''' </summary>
    ''' <value></value>
    ''' <returns>{àz </returns>
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

#Region "æû¿÷Øú"
    ''' <summary>
    ''' æû¿÷Øú
    ''' </summary>
    ''' <remarks></remarks>
    Private strSenpouSkysySimekiriBi As String
    ''' <summary>
    ''' æû¿÷Øú
    ''' </summary>
    ''' <value></value>
    ''' <returns>æû¿÷Øú </returns>
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

#Region "x¥æ¼_¿_44"
    ''' <summary>
    ''' x¥æ¼_¿_44
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriSakiMeiKanji44 As String
    ''' <summary>
    ''' x¥æ¼_¿_44
    ''' </summary>
    ''' <value></value>
    ''' <returns>x¥æ¼_¿_44 </returns>
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

#Region "pâ~ú"
    ''' <summary>
    ''' pâ~ú
    ''' </summary>
    ''' <remarks></remarks>
    Private strRiyouTeisiBi As String
    ''' <summary>
    ''' pâ~ú
    ''' </summary>
    ''' <value></value>
    ''' <returns>pâ~ú </returns>
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

#Region "ÅIXVú"
    ''' <summary>
    ''' ÅIXVú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSaisyuuKousinNitiji As DateTime
    ''' <summary>
    ''' ÅIXVú
    ''' </summary>
    ''' <value></value>
    ''' <returns>ÅIXVú </returns>
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

#Region "o^OC[U[ID"
    ''' <summary>
    ''' o^OC[U[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String
    ''' <summary>
    ''' o^OC[U[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> o^OC[U[ID</returns>
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

#Region "o^ú"
    ''' <summary>
    ''' o^ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetime As DateTime
    ''' <summary>
    ''' o^ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> o^ú</returns>
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

#Region "XVOC[U[ID"
    ''' <summary>
    ''' XVOC[U[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' XVOC[U[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> XVOC[U[ID</returns>
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

#Region "XVú"
    ''' <summary>
    ''' XVú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' XVú
    ''' </summary>
    ''' <value></value>
    ''' <returns> XVú</returns>
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