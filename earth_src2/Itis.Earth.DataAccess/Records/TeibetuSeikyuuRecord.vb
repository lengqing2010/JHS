Public Class TeibetuSeikyuuRecord

#Region "ζͺ"
    ''' <summary>
    ''' ζͺ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String
    ''' <summary>
    ''' ζͺ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ζͺ</returns>
    ''' <remarks></remarks>
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "ΫΨNO"
    ''' <summary>
    ''' ΫΨNO
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String
    ''' <summary>
    ''' ΫΨNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> ΫΨNO</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "ͺήΊ°Δή"
    ''' <summary>
    ''' ͺήΊ°Δή
    ''' </summary>
    ''' <remarks></remarks>
    Private strBunruiCd As String
    ''' <summary>
    ''' ͺήΊ°Δή
    ''' </summary>
    ''' <value></value>
    ''' <returns> ͺήΊ°Δή</returns>
    ''' <remarks></remarks>
    Public Property BunruiCd() As String
        Get
            Return strBunruiCd
        End Get
        Set(ByVal value As String)
            strBunruiCd = value
        End Set
    End Property
#End Region

#Region "ζΚ\¦NO"
    ''' <summary>
    ''' ζΚ\¦NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intGamenHyoujiNo As Integer
    ''' <summary>
    ''' ζΚ\¦NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> ζΚ\¦NO</returns>
    ''' <remarks></remarks>
    Public Property GamenHyoujiNo() As Integer
        Get
            Return intGamenHyoujiNo
        End Get
        Set(ByVal value As Integer)
            intGamenHyoujiNo = value
        End Set
    End Property
#End Region

#Region "€iΊ°Δή"
    ''' <summary>
    ''' €iΊ°Δή
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' €iΊ°Δή
    ''' </summary>
    ''' <value></value>
    ''' <returns> €iΊ°Δή</returns>
    ''' <remarks></remarks>
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

#Region "γΰz"
    ''' <summary>
    ''' γΰz
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriGaku As Integer
    ''' <summary>
    ''' γΰz
    ''' </summary>
    ''' <value></value>
    ''' <returns> γΰz</returns>
    ''' <remarks></remarks>
    Public Property UriGaku() As Integer
        Get
            Return intUriGaku
        End Get
        Set(ByVal value As Integer)
            intUriGaku = value
        End Set
    End Property
#End Region

#Region "dόΰz"
    ''' <summary>
    ''' dόΰz
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireGaku As Integer
    ''' <summary>
    ''' dόΰz
    ''' </summary>
    ''' <value></value>
    ''' <returns> dόΰz</returns>
    ''' <remarks></remarks>
    Public Property SiireGaku() As Integer
        Get
            Return intSiireGaku
        End Get
        Set(ByVal value As Integer)
            intSiireGaku = value
        End Set
    End Property
#End Region

#Region "Εζͺ"
    ''' <summary>
    ''' Εζͺ
    ''' </summary>
    ''' <remarks></remarks>
    Private strZeiKbn As String
    ''' <summary>
    ''' Εζͺ
    ''' </summary>
    ''' <value></value>
    ''' <returns> Εζͺ</returns>
    ''' <remarks></remarks>
    Public Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
        End Set
    End Property
#End Region

#Region "Ώ­sϊ"
    ''' <summary>
    ''' Ώ­sϊ
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuusyoHakDate As DateTime
    ''' <summary>
    ''' Ώ­sϊ
    ''' </summary>
    ''' <value></value>
    ''' <returns> Ώ­sϊ</returns>
    ''' <remarks></remarks>
    Public Property SeikyuusyoHakDate() As DateTime
        Get
            Return dateSeikyuusyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuusyoHakDate = value
        End Set
    End Property
#End Region

#Region "γNϊ"
    ''' <summary>
    ''' γNϊ
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriDate As DateTime
    ''' <summary>
    ''' γNϊ
    ''' </summary>
    ''' <value></value>
    ''' <returns> γNϊ</returns>
    ''' <remarks></remarks>
    Public Property UriDate() As DateTime
        Get
            Return dateUriDate
        End Get
        Set(ByVal value As DateTime)
            dateUriDate = value
        End Set
    End Property
#End Region

#Region "ΏL³"
    ''' <summary>
    ''' ΏL³
    ''' </summary>
    ''' <remarks></remarks>
    Private intSeikyuuUmu As Integer
    ''' <summary>
    ''' ΏL³
    ''' </summary>
    ''' <value></value>
    ''' <returns> ΏL³</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuUmu() As Integer
        Get
            Return intSeikyuuUmu
        End Get
        Set(ByVal value As Integer)
            intSeikyuuUmu = value
        End Set
    End Property
#End Region

#Region "mθζͺ"
    ''' <summary>
    ''' mθζͺ
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakuteiKbn As Integer
    ''' <summary>
    ''' mθζͺ
    ''' </summary>
    ''' <value></value>
    ''' <returns> mθζͺ</returns>
    ''' <remarks></remarks>
    Public Property KakuteiKbn() As Integer
        Get
            Return intKakuteiKbn
        End Get
        Set(ByVal value As Integer)
            intKakuteiKbn = value
        End Set
    End Property
#End Region

#Region "γvγFLG"
    ''' <summary>
    ''' γvγFLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriKeijyouFlg As Integer
    ''' <summary>
    ''' γvγFLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> γvγFLG</returns>
    ''' <remarks></remarks>
    Public Property UriKeijyouFlg() As Integer
        Get
            Return intUriKeijyouFlg
        End Get
        Set(ByVal value As Integer)
            intUriKeijyouFlg = value
        End Set
    End Property
#End Region

#Region "γvγϊ"
    ''' <summary>
    ''' γvγϊ
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriKeijyouDate As DateTime
    ''' <summary>
    ''' γvγϊ
    ''' </summary>
    ''' <value></value>
    ''' <returns> γvγϊ</returns>
    ''' <remarks></remarks>
    Public Property UriKeijyouDate() As DateTime
        Get
            Return dateUriKeijyouDate
        End Get
        Set(ByVal value As DateTime)
            dateUriKeijyouDate = value
        End Set
    End Property
#End Region

#Region "υl"
    ''' <summary>
    ''' υl
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou As String
    ''' <summary>
    ''' υl
    ''' </summary>
    ''' <value></value>
    ''' <returns> υl</returns>
    ''' <remarks></remarks>
    Public Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "H±XΏΰz"
    ''' <summary>
    ''' H±XΏΰz
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoumutenSeikyuuGaku As Integer
    ''' <summary>
    ''' H±XΏΰz
    ''' </summary>
    ''' <value></value>
    ''' <returns> H±XΏΰz</returns>
    ''' <remarks></remarks>
    Public Property KoumutenSeikyuuGaku() As Integer
        Get
            Return intKoumutenSeikyuuGaku
        End Get
        Set(ByVal value As Integer)
            intKoumutenSeikyuuGaku = value
        End Set
    End Property
#End Region

#Region "­ΰz"
    ''' <summary>
    ''' ­ΰz
    ''' </summary>
    ''' <remarks></remarks>
    Private intHattyuusyoGaku As Integer
    ''' <summary>
    ''' ­ΰz
    ''' </summary>
    ''' <value></value>
    ''' <returns> ­ΰz</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoGaku() As Integer
        Get
            Return intHattyuusyoGaku
        End Get
        Set(ByVal value As Integer)
            intHattyuusyoGaku = value
        End Set
    End Property
#End Region

#Region "­mFϊ"
    ''' <summary>
    ''' ­mFϊ
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHattyuusyoKakuninDate As DateTime
    ''' <summary>
    ''' ­mFϊ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ­mFϊ</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakuninDate() As DateTime
        Get
            Return dateHattyuusyoKakuninDate
        End Get
        Set(ByVal value As DateTime)
            dateHattyuusyoKakuninDate = value
        End Set
    End Property
#End Region

#Region "κόΰFLG"
    ''' <summary>
    ''' κόΰFLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intIkkatuNyuukinFlg As Integer
    ''' <summary>
    ''' κόΰFLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> κόΰFLG</returns>
    ''' <remarks></remarks>
    Public Property IkkatuNyuukinFlg() As Integer
        Get
            Return intIkkatuNyuukinFlg
        End Get
        Set(ByVal value As Integer)
            intIkkatuNyuukinFlg = value
        End Set
    End Property
#End Region

#Region "²Έ©Ομ¬ϊ"
    ''' <summary>
    ''' ²Έ©Ομ¬ϊ
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysMitsyoSakuseiDate As DateTime
    ''' <summary>
    ''' ²Έ©Ομ¬ϊ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²Έ©Ομ¬ϊ</returns>
    ''' <remarks></remarks>
    Public Property TysMitsyoSakuseiDate() As DateTime
        Get
            Return dateTysMitsyoSakuseiDate
        End Get
        Set(ByVal value As DateTime)
            dateTysMitsyoSakuseiDate = value
        End Set
    End Property
#End Region

#Region "­mθFLG"
    ''' <summary>
    ''' ­mθFLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intHattyuusyoKakuteiFlg As Integer
    ''' <summary>
    ''' ­mθFLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> ­mθFLG</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakuteiFlg() As Integer
        Get
            Return intHattyuusyoKakuteiFlg
        End Get
        Set(ByVal value As Integer)
            intHattyuusyoKakuteiFlg = value
        End Set
    End Property
#End Region

End Class