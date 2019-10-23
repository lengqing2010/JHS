Imports System.Configuration

''' <summary>
''' EARTHで使用するURLを管理するクラスです
''' 使用する場合は UrlConst.XXXX と指定
''' </summary>
''' <remarks>
''' </remarks>
Public Class UrlConst
    ''' <summary>
    ''' "BuilderInfo.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly BUILDER_INFO As String = ConfigurationManager.AppSettings("BuilderInfo")

    ''' <summary>
    ''' "GetujiIkkatuSyuusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly GETUJI_IKKATU_SYUUSEI As String = ConfigurationManager.AppSettings("GetujiIkkatuSyuusei")

    ''' <summary>
    ''' "Hosyou.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly HOSYOU As String = ConfigurationManager.AppSettings("Hosyou")

    ''' <summary>
    ''' "Houkokusyo.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly HOUKOKUSYO As String = ConfigurationManager.AppSettings("Houkokusyo")

    ''' <summary>
    ''' "IraiKakunin.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly IRAI_KAKUNIN As String = ConfigurationManager.AppSettings("IraiKakunin")

    ''' <summary>
    ''' "IraiStep1.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly IRAI_STEP_1 As String = ConfigurationManager.AppSettings("IraiStep1")

    ''' <summary>
    ''' "IraiStep2.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly IRAI_STEP_2 As String = ConfigurationManager.AppSettings("IraiStep2")

    ''' <summary>
    ''' "KairyouKouji.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly KAIRYOU_KOUJI As String = ConfigurationManager.AppSettings("KairyouKouji")

    ''' <summary>
    ''' "main.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly MAIN As String = ConfigurationManager.AppSettings("main")

    ''' <summary>
    ''' "MasterHiduke.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly MASTER_HIDUKE As String = ConfigurationManager.AppSettings("MasterHiduke")

    ''' <summary>
    ''' "NyuukinError.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly NYUUKIN_ERROR As String = ConfigurationManager.AppSettings("NyuukinError")

    ''' <summary>
    ''' "NyuukinSyori.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly NYUUKIN_SYORI As String = ConfigurationManager.AppSettings("NyuukinSyori")

    ''' <summary>
    ''' "PdfRenraku.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly PDF_RENRAKU As String = ConfigurationManager.AppSettings("PdfRenraku")

    ''' <summary>
    ''' "PopupBukkenSitei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_BUKKEN_SITEI As String = ConfigurationManager.AppSettings("PopupBukkenSitei")

    ''' <summary>
    ''' "PopupMiseSitei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_MISE_SITEI As String = ConfigurationManager.AppSettings("PopupMiseSitei")

    ''' <summary>
    ''' "SearchBukken.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_BUKKEN As String = ConfigurationManager.AppSettings("SearchBukken")

    ''' <summary>
    ''' "SearchHinsituHosyousyoJyoukyou.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_HINSITU_HOSYOUSYO_JYOUKYOU As String = ConfigurationManager.AppSettings("SearchHinsituHosyousyoJyoukyou")

    ''' <summary>
    ''' "SearchEigyousyo.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_EIGYOUSYO As String = ConfigurationManager.AppSettings("SearchEigyousyo")

    ''' <summary>
    ''' "SearchHantei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_HANTEI As String = ConfigurationManager.AppSettings("SearchHantei")

    ''' <summary>
    ''' "SearchKameiten.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_KAMEITEN As String = ConfigurationManager.AppSettings("SearchKameiten")

    ''' <summary>
    ''' "SearchKeiretu.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_KEIRETU As String = ConfigurationManager.AppSettings("SearchKeiretu")

    ''' <summary>
    ''' "SearchSyouhin.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_SYOUHIN As String = ConfigurationManager.AppSettings("SearchSyouhin")

    ''' <summary>
    ''' "SearchTyoufuku.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_TYOUFUKU As String = ConfigurationManager.AppSettings("SearchTyoufuku")

    ''' <summary>
    ''' "SearchTyousakaisya.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_TYOUSAKAISYA As String = ConfigurationManager.AppSettings("SearchTyousakaisya")

    ''' <summary>
    ''' "SearchTyousakaisya.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_KOUJIKAISYA As String = ConfigurationManager.AppSettings("SearchKouji")

    ''' <summary>
    ''' "SeikyuuSiireCheckList.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEIKYUU_SIIRE_CHECKLIST As String = ConfigurationManager.AppSettings("SeikyuuSiireCheckList")

    ''' <summary>
    ''' "TeibetuNyuukinSyuusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly TEIBETU_NYUUKIN_SYUUSEI As String = ConfigurationManager.AppSettings("TeibetuNyuukinSyuusei")

    ''' <summary>
    ''' "TeibetuSyuusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly TEIBETU_SYUUSEI As String = ConfigurationManager.AppSettings("TeibetuSyuusei")

    ''' <summary>
    ''' "TenbetuSyuusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly TENBETU_SYUUSEI As String = ConfigurationManager.AppSettings("TenbetuSyuusei")

    ''' <summary>
    ''' "UriageSiireSakusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly URIAGE_SIIRE_SAKUSEI As String = ConfigurationManager.AppSettings("UriageSiireSakusei")

    ''' <summary>
    ''' "PopupGamenKidou.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_GAMEN_KIDOU As String = ConfigurationManager.AppSettings("PopupGamenKidou")

    ''' <summary>
    ''' "MousikomiInput.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly MOUSIKOMI_INPUT As String = ConfigurationManager.AppSettings("MousikomiInput")

    ''' <summary>
    ''' "IkkatuHenkouKihon.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly IKKATU_HENKOU_KIHON As String = ConfigurationManager.AppSettings("IkkatuHenkouKihon")

    ''' <summary>
    ''' "IkkatuHenkouTysSyouhin.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly IKKATU_HENKOU_TYS_SYOUHIN As String = ConfigurationManager.AppSettings("IkkatuHenkouTysSyouhin")

    ''' <summary>
    ''' "PopupBukkenRireki.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_BUKKEN_RIREKI As String = ConfigurationManager.AppSettings("PopupBukkenRireki")

    ''' <summary>
    ''' "PopupBukkenRirekiSyousai.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_BUKKEN_RIREKI_SYUUSEI As String = ConfigurationManager.AppSettings("PopupBukkenRirekiSyuusei")

    ''' <summary>
    ''' "SearchUriageData.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_URIAGE_DATA As String = ConfigurationManager.AppSettings("SearchUriageData")

    ''' <summary>
    ''' "NyuukinTorikomiSyuusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly NYUUKIN_TORIKOMI_SYUUSEI As String = ConfigurationManager.AppSettings("NyuukinTorikomiSyuusei")

    ''' <summary>
    ''' "SearchNyuukinTorikomi.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_NYUUKIN_TORIKOMI As String = ConfigurationManager.AppSettings("SearchNyuukinTorikomi")

    ''' <summary>
    ''' "SearchSiireData.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_SIIRE_DATA As String = ConfigurationManager.AppSettings("SearchSiireData")

    ''' <summary>
    ''' "SearchNyuukinData.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_NYUUKIN_DATA As String = ConfigurationManager.AppSettings("SearchNyuukinData")

    ''' <summary>
    ''' "SearchSeikyuuSaki.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_SEIKYUU_SAKI As String = ConfigurationManager.AppSettings("SearchSeikyuuSaki")

    ''' <summary>
    ''' "SearchSiireSaki.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_SIIRE_SAKI As String = ConfigurationManager.AppSettings("SearchSiireSaki")

    ''' <summary>
    ''' "PopupSyouhin4.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_SYOUHIN4 As String = ConfigurationManager.AppSettings("PopupSyouhin4")

    ''' <summary>
    ''' "SearchHannyouUriage.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_HANNYOU_URIAGE As String = ConfigurationManager.AppSettings("SearchHannyouUriage")

    ''' <summary>
    ''' "HannyouUriageSyuusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly HANNYOU_URIAGE_SYUUSEI As String = ConfigurationManager.AppSettings("HannyouUriageSyuusei")

    ''' <summary>
    ''' "SearchHannyouSiire.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_HANNYOU_SIIRE As String = ConfigurationManager.AppSettings("SearchHannyouSiire")

    ''' <summary>
    ''' "HannyouSiireSyuusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly HANNYOU_SIIRE_SYUUSEI As String = ConfigurationManager.AppSettings("HannyouSiireSyuusei")

    ''' <summary>
    ''' "SearchSiharaiData.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_SIHARAI_DATA As String = ConfigurationManager.AppSettings("SearchSiharaiData")

    ''' <summary>
    ''' "PopupSeikyuuSiireSakiHenkou.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEIKYUU_SIIRE_HENKOU As String = ConfigurationManager.AppSettings("PopupSeikyuuSiireSakiHenkou")

    ''' <summary>
    ''' "SeikyuusyoDataSakusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEIKYUUSYO_DATA_SAKUSEI As String = ConfigurationManager.AppSettings("SeikyuusyoDataSakusei")

    ''' <summary>
    ''' "SearchSeikyuusyo.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_SEIKYUUSYO As String = ConfigurationManager.AppSettings("SearchSeikyuusyo")

    ''' <summary>
    ''' "SeikyuusyoSyuusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEIKYUUSYO_SYUUSEI As String = ConfigurationManager.AppSettings("SeikyuusyoSyuusei")

    ''' <summary>
    ''' "SeikyuuSakiMototyou.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEIKYUU_SAKI_MOTOTYOU As String = ConfigurationManager.AppSettings("SeikyuuSakiMototyou")

    ''' <summary>
    ''' "SiharaiSakiMototyou.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SIHARAI_SAKI_MOTOTYOU As String = ConfigurationManager.AppSettings("SiharaiSakiMototyou")

    ''' <summary>
    ''' "SeikyuuDateIkkatuHenkou.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEIKYUU_DATE_IKKATU_HENKOU As String = ConfigurationManager.AppSettings("SeikyuuDateIkkatuHenkou")

    ''' <summary>
    ''' "KousinRirekiSyoukai.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_KOUSIN_RIREKI As String = ConfigurationManager.AppSettings("SearchKousinRireki")

    ''' <summary>
    ''' "PopupSeikyuuDateHenkou.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_SEIKYUU_DATE_HENKOU As String = ConfigurationManager.AppSettings("PopupSeikyuuDateHenkou")

    ''' <summary>
    ''' "KeiriMenu.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly KEIRI_MENU As String = ConfigurationManager.AppSettings("KeiriMenu")

    ''' <summary>
    ''' SeikyuuSiireSakiHenkou.aspx
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEIKYUU_SIIRE_SAKI_HENKOU As String = ConfigurationManager.AppSettings("SeikyuuSiireSakiHenkou")

    ''' <summary>
    ''' "PopupDenpyouUriageDateHenkou.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_DENPYOU_URIAGE_DATE_HENKOU As String = ConfigurationManager.AppSettings("PopupDenpyouUriageDateHenkou")

    ''' <summary>
    ''' "PopupDenpyouSiireDateHenkou.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_DENPYOU_SIIRE_DATE_HENKOU As String = ConfigurationManager.AppSettings("PopupDenpyouSiireDateHenkou")

    ''' <summary>
    ''' "SearchSeikyuusyoMiinsatu.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_SEIKYUUSYO_MIINSATU As String = ConfigurationManager.AppSettings("PopupSeikyuusyoMiinsatu")

    ''' <summary>
    ''' "SearchSeikyuusyoMiinsatu.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_Bukken_SINTYOKU_JYKY As String = ConfigurationManager.AppSettings("PopupBukkenSintyokuJyky")

    ''' <summary>
    ''' "PopupTokubetuTaiou.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_TOKUBETU_TAIOU As String = ConfigurationManager.AppSettings("PopupTokubetuTaiou")

    ''' <summary>
    ''' "SearchMousikomi.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_MOUSIKOMI As String = ConfigurationManager.AppSettings("SearchMousikomi")

    ''' <summary>
    ''' "MousikomiSyuusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly MOUSIKOMI_SYUUSEI As String = ConfigurationManager.AppSettings("MousikomiSyuusei")

    ''' <summary>
    ''' "SearchSeikyuusyoSimeDateRireki.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_SEIKYUUSYO_SIME_DATE_RIREKI As String = ConfigurationManager.AppSettings("SearchSeikyuusyoSimeDateRireki")

    ''' <summary>
    ''' "SearchFcMousikomi.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_FC_MOUSIKOMI As String = ConfigurationManager.AppSettings("SearchFcMousikomi")

    ''' <summary>
    ''' "FcMousikomiSyuusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly FC_MOUSIKOMI_SYUUSEI As String = ConfigurationManager.AppSettings("FcMousikomiSyuusei")

#Region "EARTH2画面"

    ''' <summary>
    ''' "earth2 / EigyouJyouhouInquiry.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_KAMEITEN_SYOUKAI As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("KameitenSyoukai")

    ''' <summary>
    ''' "earth2 / BukkenJyouhouSearch.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_BUKKEN_SYOUKAI As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("BukkenSyoukai")

    ''' <summary>
    ''' "earth2 / KensakuSyoukaiInquiry.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_KAMEITEN_SYOUKAI_TOUROKU As String = _
                ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("KameitenSyoukaiTouroku")

    ''' <summary>
    ''' "earth2 / TyuiJyouhouInquiry.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_KAMEITEN_TYUUIJIKOU As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("KameitenTyuuijikou")

    ''' <summary>
    ''' "earth2 / MasterMainteMenu.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_MASTER_MAINTENANCE As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("MasterMaintenance")

    ''' <summary>
    ''' "earth2 / KihonJyouhouInquiry.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_KAMEITEN_KIHONJOUHOU As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("KameitenKihonjouhou")

    ''' <summary>
    ''' "earth2 / TyuiJyouhouDirectInquiry.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_KAMEITEN_TYUUIJOUHOU_DIRECT As String = _
                ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("KameitenTyuuijouhouDirect")

    ''' <summary>
    ''' "earth2 / SeikyuuSakiMaster.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_SEIKYUUSAKI_MASTER As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("SeikyuuSakiMaster")

    ''' <summary>
    ''' "earth2 / KakusyuDataSyuturyokuMenu.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_KAKUSYU_DATA_SYUTURYOKU_MENU As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("KakusyuDataSyuturyokuMenu")

    ''' <summary>
    ''' "earth2 / SeikyuSakiMototyouOutput.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_SEIKYUUSAKI_MOTOTYOU_OUTPUT As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("SeikyuSakiMototyouOutput")

    ''' <summary>
    ''' "earth2 / SiharaisakiMototyouOutput.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_SIHARAISAKI_MOTOTYOU_OUTPUT As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("SiharaisakiMototyouOutput")

    ''' <summary>
    ''' "earth2 / SeikyusyoFcwOutput.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_SEIKYUSYO_FCW_OUTPUT As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("SeikyusyoFcwOutput")

    ''' <summary>
    ''' "earth2 / SeikyusyoFcwOutput.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_SEARCH_SINKAIKEI_SIHARAI_SAKI As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("SearchSinkaikeiSiharaiSaki")

    ''' <summary>
    ''' "earth2 / SeikyusyoExcelOutputTestPage.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_SEIKYUUSYO_EXCEL_OUTPUT_TESTPAGE As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("SeikyusyoExcelOutputTestPage")

    ''' <summary>
    ''' "earth2 / TyousaMitumoriYouDataSyuturyoku.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_TYOUSA_MITUMORI_YOU_DATA_SYUTURYOKU As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("TyousaMitumoriYouDataSyuturyoku")

    ''' <summary>
    ''' "earth2 / HanyouUriageInput.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_HANYOU_URIAGE_INPUT As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("HanyouUriageInput")

    ''' <summary>
    ''' "earth2 / HanyouSiireInput.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_HANYOU_SIIRE_INPUT As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("HanyouSiireInput")

    ''' <summary>
    ''' "earth2 / TyousaMitumorisyoSakuseiInquiry.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly EARTH2_TYOUSA_MITSUMORISYO_SAKUSEI As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("TyousaMitsumorisyoSakusei")

#End Region

End Class

