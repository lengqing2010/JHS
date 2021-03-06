''' <summary>
''' @ΚΏe[u/XVpR[hNX[ΑΚΞ]
''' ¦ΊLvpeBΪΕXVΞΫπέθΟX·ι
''' </summary>
''' <remarks>
''' </remarks>
<TableClassMap("t_teibetu_seikyuu")> _
Public Class TeibetuSeikyuuRecordTokubetuTaiou
    Inherits TeibetuSeikyuuRecord

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
    <TableMap("bunrui_cd", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
    Public Overrides Property BunruiCd() As String
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
    <TableMap("gamen_hyouji_no", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property GamenHyoujiNo() As Integer
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
    <TableMap("syouhin_cd", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Overrides Property SyouhinCd() As String
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
    <TableMap("uri_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property UriGaku() As Integer
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
    <TableMap("siire_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SiireGaku() As Integer
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
    <TableMap("zei_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=1)> _
    Public Overrides Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
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
    Public Overrides Property Zeiritu() As Decimal
        Get
            Return decZeiritu
        End Get
        Set(ByVal value As Decimal)
            decZeiritu = value
        End Set
    End Property
#End Region

#Region "γΑοΕz"
    ''' <summary>
    ''' γΑοΕz
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriageSyouhiZeiGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' γΑοΕz
    ''' </summary>
    ''' <value></value>
    ''' <returns>γΑοΕz</returns>
    ''' <remarks></remarks>
    <TableMap("syouhizei_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property UriageSyouhiZeiGaku() As Integer
        Get
            Dim intTmpUriGaku As Integer = IIf(UriGaku = Integer.MinValue, 0, UriGaku) 'γΰz
            Dim decTmpZeiritu As Decimal = IIf(Zeiritu = Decimal.MinValue, 0, Zeiritu) 'Ε¦

            'ΑοΕz
            If intUriageSyouhiZeiGaku = Integer.MinValue Then 'NULLΜκAγΰzΖΕ¦©η·Z
                intUriageSyouhiZeiGaku = Fix(intTmpUriGaku * decTmpZeiritu) 'ΑοΕz = γΰz * Ε¦
            End If
            Return intUriageSyouhiZeiGaku
        End Get
        Set(ByVal value As Integer)
            intUriageSyouhiZeiGaku = value
        End Set
    End Property
#End Region

#Region "dόΑοΕz"
    ''' <summary>
    ''' dόΑοΕz
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireSyouhiZeiGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' dόΑοΕz
    ''' </summary>
    ''' <value></value>
    ''' <returns>dόΑοΕz</returns>
    ''' <remarks></remarks>
    <TableMap("siire_syouhizei_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.Int, SqlLength:=1)> _
    Public Overrides Property SiireSyouhiZeiGaku() As Integer
        Get
            Dim intTmpSiireGaku As Integer = IIf(SiireGaku = Integer.MinValue, 0, SiireGaku) 'dόΰz
            Dim decTmpZeiritu As Decimal = IIf(Zeiritu = Decimal.MinValue, 0, Zeiritu) 'Ε¦

            'ΑοΕz
            If intSiireSyouhiZeiGaku = Integer.MinValue Then 'NULLΜκAγΰzΖΕ¦©η·Z
                intSiireSyouhiZeiGaku = Fix(intTmpSiireGaku * decTmpZeiritu) 'ΑοΕz = dόΰz * Ε¦
            End If
            Return intSiireSyouhiZeiGaku
        End Get
        Set(ByVal value As Integer)
            intSiireSyouhiZeiGaku = value
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
    <TableMap("seikyuusyo_hak_date", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property SeikyuusyoHakDate() As DateTime
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
    Private dateUriDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' γNϊ
    ''' </summary>
    ''' <value></value>
    ''' <returns> γNϊ</returns>
    ''' <remarks></remarks>
    <TableMap("uri_date", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UriDate() As DateTime
        Get
            Return dateUriDate
        End Get
        Set(ByVal value As DateTime)
            dateUriDate = value
        End Set
    End Property
#End Region

#Region "`[γNϊ"
    ''' <summary>
    ''' `[γNϊ
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDenpyouUriDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' `[γNϊ
    ''' </summary>
    ''' <value></value>
    ''' <returns> `[γNϊ</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_uri_date", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property DenpyouUriDate() As DateTime
        Get
            Return dateDenpyouUriDate
        End Get
        Set(ByVal value As DateTime)
            dateDenpyouUriDate = value
        End Set
    End Property
#End Region

#Region "`[dόNϊ"
    ''' <summary>
    ''' `[dόNϊ
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDenpyouSiireDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' `[dόNϊ
    ''' </summary>
    ''' <value></value>
    ''' <returns> `[dόNϊ</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_siire_date", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=1)> _
    Public Overrides Property DenpyouSiireDate() As DateTime
        Get
            Return dateDenpyouSiireDate
        End Get
        Set(ByVal value As DateTime)
            dateDenpyouSiireDate = value
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
    <TableMap("seikyuu_umu", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SeikyuuUmu() As Integer
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
    <TableMap("kakutei_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KakuteiKbn() As Integer
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
    <TableMap("uri_keijyou_flg", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property UriKeijyouFlg() As Integer
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
    <TableMap("uri_keijyou_date", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UriKeijyouDate() As DateTime
        Get
            Return dateUriKeijyouDate
        End Get
        Set(ByVal value As DateTime)
            dateUriKeijyouDate = value
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
    <TableMap("koumuten_seikyuu_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KoumutenSeikyuuGaku() As Integer
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
    <TableMap("hattyuusyo_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HattyuusyoGaku() As Integer
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
    <TableMap("hattyuusyo_kakunin_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HattyuusyoKakuninDate() As DateTime
        Get
            Return dateHattyuusyoKakuninDate
        End Get
        Set(ByVal value As DateTime)
            dateHattyuusyoKakuninDate = value
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
    <TableMap("tys_mitsyo_sakusei_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysMitsyoSakuseiDate() As DateTime
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
    <TableMap("hattyuusyo_kakutei_flg", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HattyuusyoKakuteiFlg() As Integer
        Get
            Return intHattyuusyoKakuteiFlg
        End Get
        Set(ByVal value As Integer)
            intHattyuusyoKakuteiFlg = value
        End Set
    End Property
#End Region

#Region "Ώζ/dόζ"

#Region "ΏζR[h"
    ''' <summary>
    ''' ΏζR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String = Nothing
    ''' <summary>
    ''' ΏζR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ΏζR[h</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overrides Property SeikyuuSakiCd() As String
        Get
            Return strSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "Ώζ}Τ"
    ''' <summary>
    ''' Ώζ}Τ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrc As String = Nothing
    ''' <summary>
    ''' Ώζ}Τ
    ''' </summary>
    ''' <value></value>
    ''' <returns> Ώζ}Τ</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_brc", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overrides Property SeikyuuSakiBrc() As String
        Get
            Return strSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "Ώζζͺ"
    ''' <summary>
    ''' Ώζζͺ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String = Nothing
    ''' <summary>
    ''' Ώζζͺ
    ''' </summary>
    ''' <value></value>
    ''' <returns> Ώζζͺ</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Overrides Property SeikyuuSakiKbn() As String
        Get
            Return strSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "²ΈοΠR[h"
    ''' <summary>
    ''' ²ΈοΠR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String = Nothing
    ''' <summary>
    ''' ²ΈοΠR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²ΈοΠR[h</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overrides Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "²ΈοΠΖR[h"
    ''' <summary>
    ''' ²ΈοΠΖR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaJigyousyoCd As String = Nothing
    ''' <summary>
    ''' ²ΈοΠΖR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²ΈοΠΖR[h</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_jigyousyo_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overrides Property TysKaisyaJigyousyoCd() As String
        Get
            Return strTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#End Region

End Class