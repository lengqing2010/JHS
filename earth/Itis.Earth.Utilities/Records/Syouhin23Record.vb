Public Class Syouhin23Record

#Region "€iR[h"
    ''' <summary>
    ''' €iR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' €iR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> €iR[h</returns>
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

#Region "€iΌ"
    ''' <summary>
    ''' €iΌ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinMei As String
    ''' <summary>
    ''' €iΌ
    ''' </summary>
    ''' <value></value>
    ''' <returns> €iΌ</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_mei")> _
    Public Property SyouhinMei() As String
        Get
            Return strSyouhinMei
        End Get
        Set(ByVal value As String)
            strSyouhinMei = value
        End Set
    End Property
#End Region

#Region "WΏi"
    ''' <summary>
    ''' WΏi
    ''' </summary>
    ''' <remarks></remarks>
    Private intHyoujunKkk As Integer
    ''' <summary>
    ''' WΏi
    ''' </summary>
    ''' <value></value>
    ''' <returns> WΏi</returns>
    ''' <remarks></remarks>
    <TableMap("hyoujun_kkk")> _
    Public Property HyoujunKkk() As Integer
        Get
            Return intHyoujunKkk
        End Get
        Set(ByVal value As Integer)
            intHyoujunKkk = value
        End Set
    End Property
#End Region

#Region "qΙR[h"
    ''' <summary>
    ''' qΙR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSoukoCd As String
    ''' <summary>
    ''' qΙR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> qΙR[h</returns>
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

#Region "dόΏi"
    ''' <summary>
    ''' dόΏi
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireKkk As Integer
    ''' <summary>
    ''' dόΏi
    ''' </summary>
    ''' <value></value>
    ''' <returns> dόΏi</returns>
    ''' <remarks></remarks>
    <TableMap("siire_kkk")> _
    Public Property SiireKkk() As Integer
        Get
            Return intSiireKkk
        End Get
        Set(ByVal value As Integer)
            intSiireKkk = value
        End Set
    End Property
#End Region

#Region "Ε¦"
    ''' <summary>
    ''' Ε¦
    ''' </summary>
    ''' <remarks></remarks>
    Private decZeiritu As Decimal
    ''' <summary>
    ''' Ε¦
    ''' </summary>
    ''' <value></value>
    ''' <returns> Ε¦</returns>
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

#Region "ΑΏXR[h"
    ''' <summary>
    ''' ΑΏXR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' ΑΏXR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ΑΏXR[h</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd")> _
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "ΏζR[h(ξ{)"
    ''' <summary>
    ''' ΏζR[h(ξ{)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String
    ''' <summary>
    ''' ΏζR[h(ξ{)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ΏζR[h(ξ{)</returns>
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

#Region "Ώζ}Τ(ξ{)"
    ''' <summary>
    ''' Ώζ}Τ(ξ{)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrc As String
    ''' <summary>
    ''' Ώζ}Τ(ξ{)
    ''' </summary>
    ''' <value></value>
    ''' <returns> Ώζ}Τ(ξ{)</returns>
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

#Region "Ώζζͺ(ξ{)"
    ''' <summary>
    ''' Ώζζͺ(ξ{)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String
    ''' <summary>
    ''' Ώζζͺ(ξ{)
    ''' </summary>
    ''' <value></value>
    ''' <returns> Ώζζͺ(ξ{)</returns>
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

#Region "Ώζ^Cv(ΌΪorΌΏ)"
    ''' <summary>
    ''' Ώζ^Cv(ΌΪorΌΏ)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiType As String
    ''' <summary>
    ''' Ώζ^Cv(ΌΪorΌΏ)
    ''' </summary>
    ''' <value></value>
    ''' <returns> Ώζ^Cv(ΌΪorΌΏ)</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn")> _
    Public Property SeikyuuSakiType() As String
        Get
            If KameitenCd <> String.Empty AndAlso _
               SeikyuuSakiCd <> String.Empty AndAlso _
               KameitenCd = SeikyuuSakiCd Then
                Return EarthConst.SEIKYU_TYOKUSETU
            Else
                Return EarthConst.SEIKYU_TASETU
            End If
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiType = value
        End Set
    End Property
#End Region

#Region "ΫΨL³"
    ''' <summary>
    ''' ΫΨL³
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouUmu As Integer
    ''' <summary>
    ''' ΫΨL³
    ''' </summary>
    ''' <value></value>
    ''' <returns>ΫΨL³</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_umu")> _
    Public Property HosyouUmu() As Integer
        Get
            Return intHosyouUmu
        End Get
        Set(ByVal value As Integer)
            intHosyouUmu = value
        End Set
    End Property
#End Region

End Class