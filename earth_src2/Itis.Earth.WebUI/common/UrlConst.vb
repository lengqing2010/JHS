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
    Public Shared ReadOnly BUILDER_INFO As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("BuilderInfo")

    ''' <summary>
    ''' "GetujiIkkatuSyuusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly GETUJI_IKKATU_SYUUSEI As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("GetujiIkkatuSyuusei")

    ''' <summary>
    ''' "Hosyou.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly HOSYOU As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("Hosyou")

    ''' <summary>
    ''' "Houkokusyo.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly HOUKOKUSYO As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("Houkokusyo")

    ''' <summary>
    ''' "IraiKakunin.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly IRAI_KAKUNIN As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("IraiKakunin")

    ''' <summary>
    ''' "IraiStep1.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly IRAI_STEP_1 As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("IraiStep1")

    ''' <summary>
    ''' "IraiStep2.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly IRAI_STEP_2 As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("IraiStep2")

    ''' <summary>
    ''' "KairyouKouji.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly KAIRYOU_KOUJI As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("KairyouKouji")

    ''' <summary>
    ''' "main.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly MAIN As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("main")

    ''' <summary>
    ''' "MasterHiduke.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly MASTER_HIDUKE As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("MasterHiduke")

    ''' <summary>
    ''' "NyuukinError.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly NYUUKIN_ERROR As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("NyuukinError")

    ''' <summary>
    ''' "NyuukinSyori.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly NYUUKIN_SYORI As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("NyuukinSyori")

    ''' <summary>
    ''' "PdfRenraku.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly PDF_RENRAKU As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("PdfRenraku")

    ''' <summary>
    ''' "PopupBukkenSitei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_BUKKEN_SITEI As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("PopupBukkenSitei")

    ''' <summary>
    ''' "PopupMiseSitei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_MISE_SITEI As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("PopupMiseSitei")

    ''' <summary>
    ''' "SearchBukken.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_BUKKEN As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("SearchBukken")

    ''' <summary>
    ''' "SearchEigyousyo.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_EIGYOUSYO As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("SearchEigyousyo")

    ''' <summary>
    ''' "SearchHantei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_HANTEI As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("SearchHantei")

    ''' <summary>
    ''' "SearchKameiten.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_KAMEITEN As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("SearchKameiten")

    ''' <summary>
    ''' "SearchKeiretu.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_KEIRETU As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("SearchKeiretu")

    ''' <summary>
    ''' "SearchSyouhin.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_SYOUHIN As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("SearchSyouhin")

    ''' <summary>
    ''' "SearchTyoufuku.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_TYOUFUKU As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("SearchTyoufuku")

    ''' <summary>
    ''' "SearchTyousakaisya.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_TYOUSAKAISYA As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("SearchTyousakaisya")

    ''' <summary>
    ''' "SearchTyousakaisya.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEARCH_KOUJIKAISYA As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("SearchKouji")

    ''' <summary>
    ''' "SeikyuuSiireCheckList.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly SEIKYUU_SIIRE_CHECKLIST As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("SeikyuuSiireCheckList")

    ''' <summary>
    ''' "TeibetuNyuukinSyuusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly TEIBETU_NYUUKIN_SYUUSEI As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("TeibetuNyuukinSyuusei")

    ''' <summary>
    ''' "TeibetuSyuusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly TEIBETU_SYUUSEI As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("TeibetuSyuusei")

    ''' <summary>
    ''' "TenbetuSyuusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly TENBETU_SYUUSEI As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("TenbetuSyuusei")

    ''' <summary>
    ''' "UriageSiireSakusei.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly URIAGE_SIIRE_SAKUSEI As String = ConfigurationManager.AppSettings("EarthPath") & ConfigurationManager.AppSettings("UriageSiireSakusei")
  
#Region "EARTH2画面"
    ''' <summary>
    ''' "earth2 / EigyouJyouhouInquiry.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly KAMEITEN_BUKKEN_SYOUKAI As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("KameitenBukkenSyoukai")

    ''' <summary>
    ''' "earth2 / KensakuSyoukaiInquiry.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly KAMEITEN_SYOUKAI_TOUROKU As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("KameitenSyoukaiTouroku")

    ''' <summary>
    ''' "earth2 / MasterMainteMenu.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly MASTER_MAINTENANCE As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("MasterMaintenance")

    ''' <summary>
    ''' "EigyouJyouhouInquiry.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_EIGYOUJYOUHOU As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("EigyouJyouhou")
    ''' <summary>
    ''' "EigyouJyouhouInquiry.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly POPUP_BUKKENJYOUHOU As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("BukkenJyouhou")

    ''' <summary>
    ''' "HanyouSiireInput.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly HANYOUSIIRE_INPUT As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("HanyouSiireInput")

    ''' <summary>
    ''' "HanyouSiireErrorDetails.aspx"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly HANYOUSIIRE_ERRORDETAILS As String = ConfigurationManager.AppSettings("Earth2Path") & ConfigurationManager.AppSettings("HanyouSiireErrorDetails")

#End Region

End Class

