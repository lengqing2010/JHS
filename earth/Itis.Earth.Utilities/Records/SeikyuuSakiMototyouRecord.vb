Public Class SeikyuuSakiMototyouRecord

#Region "“`•[ƒ†ƒj[ƒNNO"
    ''' <summary>
    ''' “`•[ƒ†ƒj[ƒNNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intDenpyouUniqueNo As Integer
    ''' <summary>
    ''' “`•[ƒ†ƒj[ƒNNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> “`•[ƒ†ƒj[ƒNNO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_unique_no")> _
    Public Property DenpyouUniqueNo() As Integer
        Get
            Return intDenpyouUniqueNo
        End Get
        Set(ByVal value As Integer)
            intDenpyouUniqueNo = value
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

#Region "”NŒ“ú"
    ''' <summary>
    ''' ”NŒ“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNengappi As DateTime
    ''' <summary>
    ''' ”NŒ“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ”NŒ“ú</returns>
    ''' <remarks></remarks>
    <TableMap("nengappi")> _
    Public Property Nengappi() As DateTime
        Get
            Return dateNengappi
        End Get
        Set(ByVal value As DateTime)
            dateNengappi = value
        End Set
    End Property
#End Region

#Region "‰È–Ú"
    ''' <summary>
    ''' ‰È–Ú
    ''' </summary>
    ''' <remarks></remarks>
    Private strKamoku As String
    ''' <summary>
    ''' ‰È–Ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‰È–Ú</returns>
    ''' <remarks></remarks>
    <TableMap("kamoku")> _
    Public Property Kamoku() As String
        Get
            Return strKamoku
        End Get
        Set(ByVal value As String)
            strKamoku = value
        End Set
    End Property
#End Region

#Region "¤•iƒR[ƒh"
    ''' <summary>
    ''' ¤•iƒR[ƒh
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' ¤•iƒR[ƒh
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¤•iƒR[ƒh</returns>
    ''' <remarks></remarks>
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

#Region "•i–¼"
    ''' <summary>
    ''' •i–¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strHinmei As String
    ''' <summary>
    ''' •i–¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> •i–¼</returns>
    ''' <remarks></remarks>
    <TableMap("hinmei")> _
    Public Property Hinmei() As String
        Get
            Return strHinmei
        End Get
        Set(ByVal value As String)
            strHinmei = value
        End Set
    End Property
#End Region

#Region "ŒÚ‹q”Ô†"
    ''' <summary>
    ''' ŒÚ‹q”Ô†
    ''' </summary>
    ''' <remarks></remarks>
    Private strKokyakuNo As String
    ''' <summary>
    ''' ŒÚ‹q”Ô†
    ''' </summary>
    ''' <value></value>
    ''' <returns> ŒÚ‹q”Ô†</returns>
    ''' <remarks></remarks>
    <TableMap("kokyaku_no")> _
    Public Property KokyakuNo() As String
        Get
            Return strKokyakuNo
        End Get
        Set(ByVal value As String)
            strKokyakuNo = value
        End Set
    End Property
#End Region

#Region "•¨Œ–¼"
    ''' <summary>
    ''' •¨Œ–¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenMei As String
    ''' <summary>
    ''' •¨Œ–¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> •¨Œ–¼</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_mei")> _
    Public Property BukkenMei() As String
        Get
            Return strBukkenMei
        End Get
        Set(ByVal value As String)
            strBukkenMei = value
        End Set
    End Property
#End Region

#Region "”—Ê"
    ''' <summary>
    ''' ”—Ê
    ''' </summary>
    ''' <remarks></remarks>
    Private intSuu As Integer = Integer.MinValue
    ''' <summary>
    ''' ”—Ê
    ''' </summary>
    ''' <value></value>
    ''' <returns> ”—Ê</returns>
    ''' <remarks></remarks>
    <TableMap("suu")> _
    Public Property Suu() As Integer
        Get
            Return intSuu
        End Get
        Set(ByVal value As Integer)
            intSuu = value
        End Set
    End Property
#End Region

#Region "’P‰¿"
    ''' <summary>
    ''' ’P‰¿
    ''' </summary>
    ''' <remarks></remarks>
    Private intTanka As Integer = Integer.MinValue
    ''' <summary>
    ''' ’P‰¿
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’P‰¿</returns>
    ''' <remarks></remarks>
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

#Region "Å”²‹àŠz"
    ''' <summary>
    ''' Å”²‹àŠz
    ''' </summary>
    ''' <remarks></remarks>
    Private lngZeinukiGaku As Long = Long.MinValue
    ''' <summary>
    ''' Å”²‹àŠz
    ''' </summary>
    ''' <value></value>
    ''' <returns> Å”²‹àŠz</returns>
    ''' <remarks></remarks>
    <TableMap("zeinuki_gaku")> _
    Public Property ZeinukiGaku() As Long
        Get
            Return lngZeinukiGaku
        End Get
        Set(ByVal value As Long)
            lngZeinukiGaku = value
        End Set
    End Property
#End Region

#Region "ŠOÅŠz"
    ''' <summary>
    ''' ŠOÅŠz
    ''' </summary>
    ''' <remarks></remarks>
    Private intSotozeiGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' ŠOÅŠz
    ''' </summary>
    ''' <value></value>
    ''' <returns> ŠOÅŠz</returns>
    ''' <remarks></remarks>
    <TableMap("sotozei_gaku")> _
    Public Property SotozeiGaku() As Integer
        Get
            Return intSotozeiGaku
        End Get
        Set(ByVal value As Integer)
            intSotozeiGaku = value
        End Set
    End Property
#End Region

#Region "‹àŠz(Å‚İ)"
    ''' <summary>
    ''' ‹àŠz(Å‚İ)
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKingaku As Long = Long.MinValue
    ''' <summary>
    ''' ‹àŠz(Å‚İ)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‹àŠz(Å‚İ)</returns>
    ''' <remarks></remarks>
    <TableMap("kingaku")> _
    Public Property Kingaku() As Long
        Get
            Return lngKingaku
        End Get
        Set(ByVal value As Long)
            lngKingaku = value
        End Set
    End Property
#End Region

#Region "c‚"
    ''' <summary>
    ''' c‚
    ''' </summary>
    ''' <remarks></remarks>
    Private lngZandaka As Long = Long.MinValue
    ''' <summary>
    ''' c‚
    ''' </summary>
    ''' <value></value>
    ''' <returns> c‚</returns>
    ''' <remarks></remarks>
    <TableMap("zandaka")> _
    Public Property Zandaka() As Long
        Get
            Return lngZandaka
        End Get
        Set(ByVal value As Long)
            lngZandaka = value
        End Set
    End Property
#End Region

#Region "¿‹”NŒ“ú"
    ''' <summary>
    ''' ¿‹”NŒ“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuuDate As DateTime
    ''' <summary>
    ''' ¿‹”NŒ“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿‹”NŒ“ú</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_date")> _
    Public Property SeikyuuDate() As DateTime
        Get
            Return dateSeikyuuDate
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuuDate = value
        End Set
    End Property
#End Region

#Region "‰ñû—\’è“ú"
    ''' <summary>
    ''' ‰ñû—\’è“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKaisyuuYoteiDate As DateTime
    ''' <summary>
    ''' ‰ñû—\’è“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‰ñû—\’è“ú</returns>
    ''' <remarks></remarks>
    <TableMap("kaisyuu_yotei_date")> _
    Public Property KaisyuuYoteiDate() As DateTime
        Get
            Return dateKaisyuuYoteiDate
        End Get
        Set(ByVal value As DateTime)
            dateKaisyuuYoteiDate = value
        End Set
    End Property
#End Region

#Region "“`•[NO"
    ''' <summary>
    ''' “`•[NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenpyouNo As String
    ''' <summary>
    ''' “`•[NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> “`•[NO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_no")> _
    Public Property DenpyouNo() As String
        Get
            Return strDenpyouNo
        End Get
        Set(ByVal value As String)
            strDenpyouNo = value
        End Set
    End Property
#End Region

End Class