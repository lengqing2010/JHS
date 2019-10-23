Public Class UrikakeDataRecord

#Region "‘ÎÛ”NŒ"
    ''' <summary>
    ''' ‘ÎÛ”NŒ
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTaisyouNengetu As DateTime
    ''' <summary>
    ''' ‘ÎÛ”NŒ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‘ÎÛ”NŒ</returns>
    ''' <remarks></remarks>
    <TableMap("taisyou_nengetu")> _
    Public Property TaisyouNengetu() As DateTime
        Get
            Return dateTaisyouNengetu
        End Get
        Set(ByVal value As DateTime)
            dateTaisyouNengetu = value
        End Set
    End Property
#End Region

#Region "¿‹æƒR[ƒh"
    ''' <summary>
    ''' ¿‹æƒR[ƒh
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String
    ''' <summary>
    ''' ¿‹æƒR[ƒh
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿‹æƒR[ƒh</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_cd")> _
    Public Property SeikyuuSakiCd() As String
        Get
            Return strSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "¿‹æ}”Ô"
    ''' <summary>
    ''' ¿‹æ}”Ô
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrc As String
    ''' <summary>
    ''' ¿‹æ}”Ô
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿‹æ}”Ô</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_brc")> _
    Public Property SeikyuuSakiBrc() As String
        Get
            Return strSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "¿‹æ‹æ•ª"
    ''' <summary>
    ''' ¿‹æ‹æ•ª
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String
    ''' <summary>
    ''' ¿‹æ‹æ•ª
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿‹æ‹æ•ª</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn")> _
    Public Property SeikyuuSakiKbn() As String
        Get
            Return strSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "“–ŒŒJ‰zc‚"
    ''' <summary>
    ''' “–ŒŒJ‰zc‚
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTougetuKurikosiZan As Long
    ''' <summary>
    ''' “–ŒŒJ‰zc‚
    ''' </summary>
    ''' <value></value>
    ''' <returns> “–ŒŒJ‰zc‚</returns>
    ''' <remarks></remarks>
    <TableMap("tougetu_kurikosi_zan")> _
    Public Property TougetuKurikosiZan() As Long
        Get
            Return lngTougetuKurikosiZan
        End Get
        Set(ByVal value As Long)
            lngTougetuKurikosiZan = value
        End Set
    End Property
#End Region

#Region "“ü‹àŠz [Œ»‹à]"
    ''' <summary>
    ''' “ü‹àŠz [Œ»‹à]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngGenkin As Long
    ''' <summary>
    ''' “ü‹àŠz [Œ»‹à]
    ''' </summary>
    ''' <value></value>
    ''' <returns> “ü‹àŠz [Œ»‹à]</returns>
    ''' <remarks></remarks>
    <TableMap("genkin")> _
    Public Property Genkin() As Long
        Get
            Return lngGenkin
        End Get
        Set(ByVal value As Long)
            lngGenkin = value
        End Set
    End Property
#End Region

#Region "“ü‹àŠz [¬Øè]"
    ''' <summary>
    ''' “ü‹àŠz [¬Øè]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKogitte As Long
    ''' <summary>
    ''' “ü‹àŠz [¬Øè]
    ''' </summary>
    ''' <value></value>
    ''' <returns> “ü‹àŠz [¬Øè]</returns>
    ''' <remarks></remarks>
    <TableMap("kogitte")> _
    Public Property Kogitte() As Long
        Get
            Return lngKogitte
        End Get
        Set(ByVal value As Long)
            lngKogitte = value
        End Set
    End Property
#End Region

#Region "“ü‹àŠz [U]"
    ''' <summary>
    ''' “ü‹àŠz [U]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngFurikomi As Long
    ''' <summary>
    ''' “ü‹àŠz [U]
    ''' </summary>
    ''' <value></value>
    ''' <returns> “ü‹àŠz [U]</returns>
    ''' <remarks></remarks>
    <TableMap("furikomi")> _
    Public Property Furikomi() As Long
        Get
            Return lngFurikomi
        End Get
        Set(ByVal value As Long)
            lngFurikomi = value
        End Set
    End Property
#End Region

#Region "“ü‹àŠz [èŒ`]"
    ''' <summary>
    ''' “ü‹àŠz [èŒ`]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTegata As Long
    ''' <summary>
    ''' “ü‹àŠz [èŒ`]
    ''' </summary>
    ''' <value></value>
    ''' <returns> “ü‹àŠz [èŒ`]</returns>
    ''' <remarks></remarks>
    <TableMap("tegata")> _
    Public Property Tegata() As Long
        Get
            Return lngTegata
        End Get
        Set(ByVal value As Long)
            lngTegata = value
        End Set
    End Property
#End Region

#Region "“ü‹àŠz [‘ŠE]"
    ''' <summary>
    ''' “ü‹àŠz [‘ŠE]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSousai As Long
    ''' <summary>
    ''' “ü‹àŠz [‘ŠE]
    ''' </summary>
    ''' <value></value>
    ''' <returns> “ü‹àŠz [‘ŠE]</returns>
    ''' <remarks></remarks>
    <TableMap("sousai")> _
    Public Property Sousai() As Long
        Get
            Return lngSousai
        End Get
        Set(ByVal value As Long)
            lngSousai = value
        End Set
    End Property
#End Region

#Region "“ü‹àŠz [’lˆø]"
    ''' <summary>
    ''' “ü‹àŠz [’lˆø]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngNebiki As Long
    ''' <summary>
    ''' “ü‹àŠz [’lˆø]
    ''' </summary>
    ''' <value></value>
    ''' <returns> “ü‹àŠz [’lˆø]</returns>
    ''' <remarks></remarks>
    <TableMap("nebiki")> _
    Public Property Nebiki() As Long
        Get
            Return lngNebiki
        End Get
        Set(ByVal value As Long)
            lngNebiki = value
        End Set
    End Property
#End Region

#Region "“ü‹àŠz [‚»‚Ì‘¼]"
    ''' <summary>
    ''' “ü‹àŠz [‚»‚Ì‘¼]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSonota As Long
    ''' <summary>
    ''' “ü‹àŠz [‚»‚Ì‘¼]
    ''' </summary>
    ''' <value></value>
    ''' <returns> “ü‹àŠz [‚»‚Ì‘¼]</returns>
    ''' <remarks></remarks>
    <TableMap("sonota")> _
    Public Property Sonota() As Long
        Get
            Return lngSonota
        End Get
        Set(ByVal value As Long)
            lngSonota = value
        End Set
    End Property
#End Region

#Region "“ü‹àŠz [‹¦—Í‰ï”ï]"
    ''' <summary>
    ''' “ü‹àŠz [‹¦—Í‰ï”ï]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKyouryokuKaihi As Long
    ''' <summary>
    ''' “ü‹àŠz [‹¦—Í‰ï”ï]
    ''' </summary>
    ''' <value></value>
    ''' <returns> “ü‹àŠz [‹¦—Í‰ï”ï]</returns>
    ''' <remarks></remarks>
    <TableMap("kyouryoku_kaihi")> _
    Public Property KyouryokuKaihi() As Long
        Get
            Return lngKyouryokuKaihi
        End Get
        Set(ByVal value As Long)
            lngKyouryokuKaihi = value
        End Set
    End Property
#End Region

#Region "“ü‹àŠz [ŒûÀU‘Ö]"
    ''' <summary>
    ''' “ü‹àŠz [ŒûÀU‘Ö]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKouzaFurikae As Long
    ''' <summary>
    ''' “ü‹àŠz [ŒûÀU‘Ö]
    ''' </summary>
    ''' <value></value>
    ''' <returns> “ü‹àŠz [ŒûÀU‘Ö]</returns>
    ''' <remarks></remarks>
    <TableMap("kouza_furikae")> _
    Public Property KouzaFurikae() As Long
        Get
            Return lngKouzaFurikae
        End Get
        Set(ByVal value As Long)
            lngKouzaFurikae = value
        End Set
    End Property
#End Region

#Region "“ü‹àŠz [Uè”—¿]"
    ''' <summary>
    ''' “ü‹àŠz [Uè”—¿]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngFurikomiTesuuryou As Long
    ''' <summary>
    ''' “ü‹àŠz [Uè”—¿]
    ''' </summary>
    ''' <value></value>
    ''' <returns> “ü‹àŠz [Uè”—¿]</returns>
    ''' <remarks></remarks>
    <TableMap("furikomi_tesuuryou")> _
    Public Property FurikomiTesuuryou() As Long
        Get
            Return lngFurikomiTesuuryou
        End Get
        Set(ByVal value As Long)
            lngFurikomiTesuuryou = value
        End Set
    End Property
#End Region

#Region "“–Œ“ü‹à‡Œv"
    ''' <summary>
    ''' “–Œ“ü‹à‡Œv
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTougetuNyuukinGoukei As Long
    ''' <summary>
    ''' “–Œ“ü‹à‡Œv
    ''' </summary>
    ''' <value></value>
    ''' <returns> “–Œ“ü‹à‡Œv</returns>
    ''' <remarks></remarks>
    <TableMap("tougetu_nyuukin_goukei")> _
    Public Property TougetuNyuukinGoukei() As Long
        Get
            Return lngTougetuNyuukinGoukei
        End Get
        Set(ByVal value As Long)
            lngTougetuNyuukinGoukei = value
        End Set
    End Property
#End Region

#Region "“–Œ”„ã‚"
    ''' <summary>
    ''' “–Œ”„ã‚
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTougetuUriageDaka As Long
    ''' <summary>
    ''' “–Œ”„ã‚
    ''' </summary>
    ''' <value></value>
    ''' <returns> “–Œ”„ã‚</returns>
    ''' <remarks></remarks>
    <TableMap("tougetu_uriage_daka")> _
    Public Property TougetuUriageDaka() As Long
        Get
            Return lngTougetuUriageDaka
        End Get
        Set(ByVal value As Long)
            lngTougetuUriageDaka = value
        End Set
    End Property
#End Region

#Region "“–ŒÁ”ïÅ“™"
    ''' <summary>
    ''' “–ŒÁ”ïÅ“™
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTougetuZeiNado As Long
    ''' <summary>
    ''' “–ŒÁ”ïÅ“™
    ''' </summary>
    ''' <value></value>
    ''' <returns> “–ŒÁ”ïÅ“™</returns>
    ''' <remarks></remarks>
    <TableMap("tougetu_zei_nado")> _
    Public Property TougetuZeiNado() As Long
        Get
            Return lngTougetuZeiNado
        End Get
        Set(ByVal value As Long)
            lngTougetuZeiNado = value
        End Set
    End Property
#End Region

#Region "“o˜^ƒƒOƒCƒ“ƒ†[ƒU[ID"
    ''' <summary>
    ''' “o˜^ƒƒOƒCƒ“ƒ†[ƒU[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String
    ''' <summary>
    ''' “o˜^ƒƒOƒCƒ“ƒ†[ƒU[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> “o˜^ƒƒOƒCƒ“ƒ†[ƒU[ID</returns>
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

#Region "“o˜^“ú"
    ''' <summary>
    ''' “o˜^“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetime As DateTime
    ''' <summary>
    ''' “o˜^“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> “o˜^“ú</returns>
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

#Region "XVƒƒOƒCƒ“ƒ†[ƒU[ID"
    ''' <summary>
    ''' XVƒƒOƒCƒ“ƒ†[ƒU[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' XVƒƒOƒCƒ“ƒ†[ƒU[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> XVƒƒOƒCƒ“ƒ†[ƒU[ID</returns>
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

#Region "XV“ú"
    ''' <summary>
    ''' XV“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' XV“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> XV“ú</returns>
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