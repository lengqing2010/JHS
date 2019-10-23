Imports Itis.ApplicationBlocks.ExceptionManagement
''' <summary>
''' ’n”Õƒf[ƒ^‚ÌƒŒƒR[ƒhƒNƒ‰ƒX‚Å‚·
''' FC\ŒŸõ/FC\C³‰æ–Ê—p‚ÌXVƒf[ƒ^\¬‚Å‚·
''' </summary>
''' <remarks>’n”Õƒe[ƒuƒ‹‚Ì‘SƒŒƒR[ƒhƒJƒ‰ƒ€‚É‰Á‚¦A“@•Êƒf[ƒ^‚ğ•Û‚µ‚Ä‚Ü‚·<br/>
'''          ¤•iƒR[ƒh‚P‚Ì“@•Ê¿‹ƒŒƒR[ƒhFTeibetuSeikyuuRecord<br/>
'''          ¤•iƒR[ƒh‚Q‚Ì“@•Ê¿‹ƒŒƒR[ƒhFDictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
'''          ¤•iƒR[ƒh‚R‚Ì“@•Ê¿‹ƒŒƒR[ƒhFDictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
'''          ’Ç‰ÁH–‚Ì“@•Ê¿‹ƒŒƒR[ƒh@@FTeibetuSeikyuuRecord<br/>
'''          ‰ü—ÇH–‚Ì“@•Ê¿‹ƒŒƒR[ƒh@@FTeibetuSeikyuuRecord<br/>
'''          ’²¸•ñ‘‚Ì“@•Ê¿‹ƒŒƒR[ƒh@FTeibetuSeikyuuRecord<br/>
'''          H–•ñ‘‚Ì“@•Ê¿‹ƒŒƒR[ƒh@FTeibetuSeikyuuRecord<br/>
'''          •ÛØ‘‚Ì“@•Ê¿‹ƒŒƒR[ƒh@@@FTeibetuSeikyuuRecord<br/>
'''          ‰ğ–ñ•¥–ß‚Ì“@•Ê¿‹ƒŒƒR[ƒh@@FTeibetuSeikyuuRecord<br/>
'''          ã‹LˆÈŠO‚Ì“@•Ê¿‹ƒŒƒR[ƒh    FList(TeibetuSeikyuuRecord)<br/>
'''          “@•Ê“ü‹àƒŒƒR[ƒh              FDictionary(Of String, TeibetuNyuukinRecord)<br/>
''' </remarks>
<TableClassMap("t_jiban")> _
Public Class JibanRecordFcMousikomiJutyuu
    Inherits JibanRecordBase

    'EMABáŠQ‘Î‰î•ñŠi”[ˆ—
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "‹æ•ª"
    ''' <summary>
    ''' ‹æ•ª
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String
    ''' <summary>
    ''' ‹æ•ª
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‹æ•ª</returns>
    ''' <remarks></remarks>
    <TableMap("kbn", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Overrides Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "•ÛØ‘NO"
    ''' <summary>
    ''' •ÛØ‘NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String
    ''' <summary>
    ''' •ÛØ‘NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛØ‘NO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overrides Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "ÃŞ°À”jŠüí•Ê"
    ''' <summary>
    ''' ÃŞ°À”jŠüí•Ê
    ''' </summary>
    ''' <remarks></remarks>
    Private intDataHakiSyubetu As Integer
    ''' <summary>
    ''' ÃŞ°À”jŠüí•Ê
    ''' </summary>
    ''' <value></value>
    ''' <returns> ÃŞ°À”jŠüí•Ê</returns>
    ''' <remarks></remarks>
    <TableMap("data_haki_syubetu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property DataHakiSyubetu() As Integer
        Get
            Return intDataHakiSyubetu
        End Get
        Set(ByVal value As Integer)
            intDataHakiSyubetu = value
        End Set
    End Property
#End Region

#Region "ÃŞ°À”jŠü“ú"
    ''' <summary>
    ''' ÃŞ°À”jŠü“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDataHakiDate As DateTime
    ''' <summary>
    ''' ÃŞ°À”jŠü“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ÃŞ°À”jŠü“ú</returns>
    ''' <remarks></remarks>
    <TableMap("data_haki_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property DataHakiDate() As DateTime
        Get
            Return dateDataHakiDate
        End Get
        Set(ByVal value As DateTime)
            dateDataHakiDate = value
        End Set
    End Property
#End Region

#Region "{å–¼"
    ''' <summary>
    ''' {å–¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String
    ''' <summary>
    ''' {å–¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> {å–¼</returns>
    ''' <remarks></remarks>
    <TableMap("sesyu_mei", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=50)> _
    Public Overrides Property SesyuMei() As String
        Get
            Return strSesyuMei
        End Get
        Set(ByVal value As String)
            strSesyuMei = value
        End Set
    End Property
#End Region

#Region "ó’•¨Œ–¼"
    ''' <summary>
    ''' ó’•¨Œ–¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyutyuuBukkenMei As String
    ''' <summary>
    ''' ó’•¨Œ–¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> ó’•¨Œ–¼</returns>
    ''' <remarks></remarks>
    <TableMap("jutyuu_bukken_mei", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=50)> _
    Public Overrides Property JyutyuuBukkenMei() As String
        Get
            Return strJyutyuuBukkenMei
        End Get
        Set(ByVal value As String)
            strJyutyuuBukkenMei = value
        End Set
    End Property
#End Region

#Region "•¨ŒZŠ1"
    ''' <summary>
    ''' •¨ŒZŠ1
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo1 As String
    ''' <summary>
    ''' •¨ŒZŠ1
    ''' </summary>
    ''' <value></value>
    ''' <returns> •¨ŒZŠ1</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo1", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Overrides Property BukkenJyuusyo1() As String
        Get
            Return strBukkenJyuusyo1
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo1 = value
        End Set
    End Property
#End Region

#Region "•¨ŒZŠ2"
    ''' <summary>
    ''' •¨ŒZŠ2
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo2 As String
    ''' <summary>
    ''' •¨ŒZŠ2
    ''' </summary>
    ''' <value></value>
    ''' <returns> •¨ŒZŠ2</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo2", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
    Public Overrides Property BukkenJyuusyo2() As String
        Get
            Return strBukkenJyuusyo2
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo2 = value
        End Set
    End Property
#End Region

#Region "•¨ŒZŠ3"
    ''' <summary>
    ''' •¨ŒZŠ3
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo3 As String
    ''' <summary>
    ''' •¨ŒZŠ3
    ''' </summary>
    ''' <value></value>
    ''' <returns> •¨ŒZŠ3</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo3", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=54)> _
    Public Overrides Property BukkenJyuusyo3() As String
        Get
            Return strBukkenJyuusyo3
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo3 = value
        End Set
    End Property
#End Region

#Region "•¨Œ–¼ŠñƒR[ƒh"
    ''' <summary>
    ''' •¨Œ–¼ŠñƒR[ƒh
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenNayoseCd As String = String.Empty
    ''' <summary>
    ''' •¨Œ–¼ŠñƒR[ƒh
    ''' </summary>
    ''' <value></value>
    ''' <returns> •¨Œ–¼ŠñƒR[ƒh</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_nayose_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=11)> _
    Public Overrides Property BukkenNayoseCd() As String
        Get
            Return strBukkenNayoseCd
        End Get
        Set(ByVal value As String)
            strBukkenNayoseCd = value
        End Set
    End Property
#End Region

#Region "‰Á–¿“Xº°ÄŞ"
    ''' <summary>
    ''' ‰Á–¿“Xº°ÄŞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' ‰Á–¿“Xº°ÄŞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‰Á–¿“Xº°ÄŞ</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overrides Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "¤•i‹æ•ª"
    ''' <summary>
    ''' ¤•i‹æ•ª
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouhinKbn As Integer
    ''' <summary>
    ''' ¤•i‹æ•ª
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¤•i‹æ•ª</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_kbn", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SyouhinKbn() As Integer
        Get
            Return intSyouhinKbn
        End Get
        Set(ByVal value As Integer)
            intSyouhinKbn = value
        End Set
    End Property
#End Region

#Region "”õl"
    ''' <summary>
    ''' ”õl
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou As String
    ''' <summary>
    ''' ”õl
    ''' </summary>
    ''' <value></value>
    ''' <returns> ”õl</returns>
    ''' <remarks></remarks>
    <TableMap("bikou", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "”õl2"
    ''' <summary>
    ''' ”õl2
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou2 As String
    ''' <summary>
    ''' ”õl2
    ''' </summary>
    ''' <value></value>
    ''' <returns> ”õl</returns>
    ''' <remarks></remarks>
    <TableMap("bikou2", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=512)> _
    Public Overrides Property Bikou2() As String
        Get
            Return strBikou2
        End Get
        Set(ByVal value As String)
            strBikou2 = value
        End Set
    End Property
#End Region

#Region "ˆË—Š“ú"
    ''' <summary>
    ''' ˆË—Š“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateIraiDate As DateTime
    ''' <summary>
    ''' ˆË—Š“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ˆË—Š“ú</returns>
    ''' <remarks></remarks>
    <TableMap("irai_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property IraiDate() As DateTime
        Get
            Return dateIraiDate
        End Get
        Set(ByVal value As DateTime)
            dateIraiDate = value
        End Set
    End Property
#End Region

#Region "ˆË—Š’S“–Ò"
    ''' <summary>
    ''' ˆË—Š’S“–Ò
    ''' </summary>
    ''' <remarks></remarks>
    Private strIraiTantousyaMei As String
    ''' <summary>
    ''' ˆË—Š’S“–Ò
    ''' </summary>
    ''' <value></value>
    ''' <returns> ˆË—Š’S“–Ò</returns>
    ''' <remarks></remarks>
    <TableMap("irai_tantousya_mei", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overrides Property IraiTantousyaMei() As String
        Get
            Return strIraiTantousyaMei
        End Get
        Set(ByVal value As String)
            strIraiTantousyaMei = value
        End Set
    End Property
#End Region

#Region "Œ_–ñNO"
    ''' <summary>
    ''' Œ_–ñNO
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiyakuNo As String
    ''' <summary>
    ''' Œ_–ñNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> Œ_–ñNO</returns>
    ''' <remarks></remarks>
    <TableMap("keiyaku_no", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property KeiyakuNo() As String
        Get
            Return strKeiyakuNo
        End Get
        Set(ByVal value As String)
            strKeiyakuNo = value
        End Set
    End Property
#End Region

#Region "THàêár—L–³"
    ''' <summary>
    ''' THàêár—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intThKasiUmu As Integer
    ''' <summary>
    ''' THàêár—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> THàêár—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("th_kasi_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property ThKasiUmu() As Integer
        Get
            Return intThKasiUmu
        End Get
        Set(ByVal value As Integer)
            intThKasiUmu = value
        End Set
    End Property
#End Region

#Region "ŠK‘w"
    ''' <summary>
    ''' ŠK‘w
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisou As Integer = Integer.MinValue
    ''' <summary>
    ''' ŠK‘w
    ''' </summary>
    ''' <value></value>
    ''' <returns> ŠK‘w</returns>
    ''' <remarks></remarks>
    <TableMap("kaisou", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Kaisou() As Integer
        Get
            Return intKaisou
        End Get
        Set(ByVal value As Integer)
            intKaisou = value
        End Set
    End Property
#End Region

#Region "V’zŒš‘Ö"
    ''' <summary>
    ''' V’zŒš‘Ö
    ''' </summary>
    ''' <remarks></remarks>
    Private intSintikuTatekae As Integer
    ''' <summary>
    ''' V’zŒš‘Ö
    ''' </summary>
    ''' <value></value>
    ''' <returns> V’zŒš‘Ö</returns>
    ''' <remarks></remarks>
    <TableMap("sintiku_tatekae", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SintikuTatekae() As Integer
        Get
            Return intSintikuTatekae
        End Get
        Set(ByVal value As Integer)
            intSintikuTatekae = value
        End Set
    End Property
#End Region

#Region "\‘¢"
    ''' <summary>
    ''' \‘¢
    ''' </summary>
    ''' <remarks></remarks>
    Private intKouzou As Integer
    ''' <summary>
    ''' \‘¢
    ''' </summary>
    ''' <value></value>
    ''' <returns> \‘¢</returns>
    ''' <remarks></remarks>
    <TableMap("kouzou", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Kouzou() As Integer
        Get
            Return intKouzou
        End Get
        Set(ByVal value As Integer)
            intKouzou = value
        End Set
    End Property
#End Region

#Region "\‘¢MEMO"
    ''' <summary>
    ''' \‘¢MEMO
    ''' </summary>
    ''' <remarks></remarks>
    Private strKouzouMemo As String
    ''' <summary>
    ''' \‘¢MEMO
    ''' </summary>
    ''' <value></value>
    ''' <returns> \‘¢MEMO</returns>
    ''' <remarks></remarks>
    <TableMap("kouzou_memo", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overrides Property KouzouMemo() As String
        Get
            Return strKouzouMemo
        End Get
        Set(ByVal value As String)
            strKouzouMemo = value
        End Set
    End Property
#End Region

#Region "ÔŒÉ"
    ''' <summary>
    ''' ÔŒÉ
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyako As Integer
    ''' <summary>
    ''' ÔŒÉ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ÔŒÉ</returns>
    ''' <remarks></remarks>
    <TableMap("syako", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Syako() As Integer
        Get
            Return intSyako
        End Get
        Set(ByVal value As Integer)
            intSyako = value
        End Set
    End Property
#End Region

#Region "ªØ‚è[‚³"
    ''' <summary>
    ''' ªØ‚è[‚³
    ''' </summary>
    ''' <remarks></remarks>
    Private decNegiriHukasa As Decimal
    ''' <summary>
    ''' ªØ‚è[‚³
    ''' </summary>
    ''' <value></value>
    ''' <returns> ªØ‚è[‚³</returns>
    ''' <remarks></remarks>
    <TableMap("negiri_hukasa", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overrides Property NegiriHukasa() As Decimal
        Get
            Return decNegiriHukasa
        End Get
        Set(ByVal value As Decimal)
            decNegiriHukasa = value
        End Set
    End Property
#End Region

#Region "—\’è·“yŒú‚³"
    ''' <summary>
    ''' —\’è·“yŒú‚³
    ''' </summary>
    ''' <remarks></remarks>
    Private decYoteiMoritutiAtusa As Decimal
    ''' <summary>
    ''' —\’è·“yŒú‚³
    ''' </summary>
    ''' <value></value>
    ''' <returns> —\’è·“yŒú‚³</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_morituti_atusa", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overrides Property YoteiMoritutiAtusa() As Decimal
        Get
            Return decYoteiMoritutiAtusa
        End Get
        Set(ByVal value As Decimal)
            decYoteiMoritutiAtusa = value
        End Set
    End Property
#End Region

#Region "—\’èŠî‘b"
    ''' <summary>
    ''' —\’èŠî‘b
    ''' </summary>
    ''' <remarks></remarks>
    Private intYoteiKs As Integer
    ''' <summary>
    ''' —\’èŠî‘b
    ''' </summary>
    ''' <value></value>
    ''' <returns> —\’èŠî‘b</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_ks", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YoteiKs() As Integer
        Get
            Return intYoteiKs
        End Get
        Set(ByVal value As Integer)
            intYoteiKs = value
        End Set
    End Property
#End Region

#Region "—\’èŠî‘bMEMO"
    ''' <summary>
    ''' —\’èŠî‘bMEMO
    ''' </summary>
    ''' <remarks></remarks>
    Private strYoteiKsMemo As String
    ''' <summary>
    ''' —\’èŠî‘bMEMO
    ''' </summary>
    ''' <value></value>
    ''' <returns> —\’èŠî‘bMEMO</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_ks_memo", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overrides Property YoteiKsMemo() As String
        Get
            Return strYoteiKsMemo
        End Get
        Set(ByVal value As String)
            strYoteiKsMemo = value
        End Set
    End Property
#End Region

#Region "’²¸‰ïĞº°ÄŞ"
    ''' <summary>
    ''' ’²¸‰ïĞº°ÄŞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' ’²¸‰ïĞº°ÄŞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸‰ïĞº°ÄŞ</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overrides Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "’²¸‰ïĞ–‹ÆŠº°ÄŞ"
    ''' <summary>
    ''' ’²¸‰ïĞ–‹ÆŠº°ÄŞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaJigyousyoCd As String
    ''' <summary>
    ''' ’²¸‰ïĞ–‹ÆŠº°ÄŞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸‰ïĞ–‹ÆŠº°ÄŞ</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_jigyousyo_cd", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overrides Property TysKaisyaJigyousyoCd() As String
        Get
            Return strTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "’²¸•û–@"
    ''' <summary>
    ''' ’²¸•û–@
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysHouhou As Integer = Integer.MinValue
    ''' <summary>
    ''' ’²¸•û–@
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸•û–@</returns>
    ''' <remarks></remarks>
    <TableMap("tys_houhou", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TysHouhou() As Integer
        Get
            Return intTysHouhou
        End Get
        Set(ByVal value As Integer)
            intTysHouhou = value
        End Set
    End Property
#End Region

#Region "’²¸ŠT—v"
    ''' <summary>
    ''' ’²¸ŠT—v
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysGaiyou As Integer
    ''' <summary>
    ''' ’²¸ŠT—v
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸ŠT—v</returns>
    ''' <remarks></remarks>
    <TableMap("tys_gaiyou", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TysGaiyou() As Integer
        Get
            Return intTysGaiyou
        End Get
        Set(ByVal value As Integer)
            intTysGaiyou = value
        End Set
    End Property
#End Region

#Region "FCËŞÙÀŞ°”Ì”„‹àŠz"
    ''' <summary>
    ''' FCËŞÙÀŞ°”Ì”„‹àŠz
    ''' </summary>
    ''' <remarks></remarks>
    Private intFcBuilderHanbaiGaku As Integer
    ''' <summary>
    ''' FCËŞÙÀŞ°”Ì”„‹àŠz
    ''' </summary>
    ''' <value></value>
    ''' <returns> FCËŞÙÀŞ°”Ì”„‹àŠz</returns>
    ''' <remarks></remarks>
    <TableMap("fc_builder_hanbai_gaku", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property FcBuilderHanbaiGaku() As Integer
        Get
            Return intFcBuilderHanbaiGaku
        End Get
        Set(ByVal value As Integer)
            intFcBuilderHanbaiGaku = value
        End Set
    End Property
#End Region

#Region "’²¸Šó–]“ú"
    ''' <summary>
    ''' ’²¸Šó–]“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysKibouDate As DateTime
    ''' <summary>
    ''' ’²¸Šó–]“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸Šó–]“ú</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kibou_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysKibouDate() As DateTime
        Get
            Return dateTysKibouDate
        End Get
        Set(ByVal value As DateTime)
            dateTysKibouDate = value
        End Set
    End Property
#End Region

#Region "’²¸Šó–]ŠÔ"
    ''' <summary>
    ''' ’²¸Šó–]ŠÔ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKibouJikan As String
    ''' <summary>
    ''' ’²¸Šó–]ŠÔ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸Šó–]ŠÔ</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kibou_jikan", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=26)> _
    Public Overrides Property TysKibouJikan() As String
        Get
            Return strTysKibouJikan
        End Get
        Set(ByVal value As String)
            strTysKibouJikan = value
        End Set
    End Property
#End Region

#Region "—§‰ï—L–³"
    ''' <summary>
    ''' —§‰ï—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intTatiaiUmu As Integer
    ''' <summary>
    ''' —§‰ï—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> —§‰ï—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("tatiai_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TatiaiUmu() As Integer
        Get
            Return intTatiaiUmu
        End Get
        Set(ByVal value As Integer)
            intTatiaiUmu = value
        End Set
    End Property
#End Region

#Region "—§‰ïÒº°ÄŞ"
    ''' <summary>
    ''' —§‰ïÒº°ÄŞ
    ''' </summary>
    ''' <remarks></remarks>
    Private intTatiaisyaCd As Integer
    ''' <summary>
    ''' —§‰ïÒº°ÄŞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> —§‰ïÒº°ÄŞ</returns>
    ''' <remarks></remarks>
    <TableMap("tatiaisya_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TatiaisyaCd() As Integer
        Get
            Return intTatiaisyaCd
        End Get
        Set(ByVal value As Integer)
            intTatiaisyaCd = value
        End Set
    End Property
#End Region

#Region "“Y•t_•½–Ê}"
    ''' <summary>
    ''' “Y•t_•½–Ê}
    ''' </summary>
    ''' <remarks></remarks>
    Private intTenpuHeimenzu As Integer
    ''' <summary>
    ''' “Y•t_•½–Ê}
    ''' </summary>
    ''' <value></value>
    ''' <returns> “Y•t_•½–Ê}</returns>
    ''' <remarks></remarks>
    <TableMap("tenpu_heimenzu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TenpuHeimenzu() As Integer
        Get
            Return intTenpuHeimenzu
        End Get
        Set(ByVal value As Integer)
            intTenpuHeimenzu = value
        End Set
    End Property
#End Region

#Region "“Y•t_—§–Ê}"
    ''' <summary>
    ''' “Y•t_—§–Ê}
    ''' </summary>
    ''' <remarks></remarks>
    Private intTenpuRitumenzu As Integer
    ''' <summary>
    ''' “Y•t_—§–Ê}
    ''' </summary>
    ''' <value></value>
    ''' <returns> “Y•t_—§–Ê}</returns>
    ''' <remarks></remarks>
    <TableMap("tenpu_ritumenzu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TenpuRitumenzu() As Integer
        Get
            Return intTenpuRitumenzu
        End Get
        Set(ByVal value As Integer)
            intTenpuRitumenzu = value
        End Set
    End Property
#End Region

#Region "“Y•t_Šî‘b•š}"
    ''' <summary>
    ''' “Y•t_Šî‘b•š}
    ''' </summary>
    ''' <remarks></remarks>
    Private intTenpuKsHusezu As Integer
    ''' <summary>
    ''' “Y•t_Šî‘b•š}
    ''' </summary>
    ''' <value></value>
    ''' <returns> “Y•t_Šî‘b•š}</returns>
    ''' <remarks></remarks>
    <TableMap("tenpu_ks_husezu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TenpuKsHusezu() As Integer
        Get
            Return intTenpuKsHusezu
        End Get
        Set(ByVal value As Integer)
            intTenpuKsHusezu = value
        End Set
    End Property
#End Region

#Region "“Y•t_’f–Ê}"
    ''' <summary>
    ''' “Y•t_’f–Ê}
    ''' </summary>
    ''' <remarks></remarks>
    Private intTenpuDanmenzu As Integer
    ''' <summary>
    ''' “Y•t_’f–Ê}
    ''' </summary>
    ''' <value></value>
    ''' <returns> “Y•t_’f–Ê}</returns>
    ''' <remarks></remarks>
    <TableMap("tenpu_danmenzu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TenpuDanmenzu() As Integer
        Get
            Return intTenpuDanmenzu
        End Get
        Set(ByVal value As Integer)
            intTenpuDanmenzu = value
        End Set
    End Property
#End Region

#Region "“Y•t_‹éŒv}"
    ''' <summary>
    ''' “Y•t_‹éŒv}
    ''' </summary>
    ''' <remarks></remarks>
    Private intTenpuKukeizu As Integer
    ''' <summary>
    ''' “Y•t_‹éŒv}
    ''' </summary>
    ''' <value></value>
    ''' <returns> “Y•t_‹éŒv}</returns>
    ''' <remarks></remarks>
    <TableMap("tenpu_kukeizu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TenpuKukeizu() As Integer
        Get
            Return intTenpuKukeizu
        End Get
        Set(ByVal value As Integer)
            intTenpuKukeizu = value
        End Set
    End Property
#End Region

#Region "³‘ø‘’²¸“ú"
    ''' <summary>
    ''' ³‘ø‘’²¸“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSyoudakusyoTysDate As DateTime
    ''' <summary>
    ''' ³‘ø‘’²¸“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ³‘ø‘’²¸“ú</returns>
    ''' <remarks></remarks>
    <TableMap("syoudakusyo_tys_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property SyoudakusyoTysDate() As DateTime
        Get
            Return dateSyoudakusyoTysDate
        End Get
        Set(ByVal value As DateTime)
            dateSyoudakusyoTysDate = value
        End Set
    End Property
#End Region

#Region "’²¸À{“ú"
    ''' <summary>
    ''' ’²¸À{“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysJissiDate As DateTime
    ''' <summary>
    ''' ’²¸À{“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸À{“ú</returns>
    ''' <remarks></remarks>
    <TableMap("tys_jissi_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysJissiDate() As DateTime
        Get
            Return dateTysJissiDate
        End Get
        Set(ByVal value As DateTime)
            dateTysJissiDate = value
        End Set
    End Property
#End Region

#Region "“y¿"
    ''' <summary>
    ''' “y¿
    ''' </summary>
    ''' <remarks></remarks>
    Private strDositu As String
    ''' <summary>
    ''' “y¿
    ''' </summary>
    ''' <value></value>
    ''' <returns> “y¿</returns>
    ''' <remarks></remarks>
    <TableMap("dositu")> _
    Public Overrides Property Dositu() As String
        Get
            Return strDositu
        End Get
        Set(ByVal value As String)
            strDositu = value
        End Set
    End Property
#End Region

#Region "‹–—ex—Í"
    ''' <summary>
    ''' ‹–—ex—Í
    ''' </summary>
    ''' <remarks></remarks>
    Private strKyoyouSijiryoku As String
    ''' <summary>
    ''' ‹–—ex—Í
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‹–—ex—Í</returns>
    ''' <remarks></remarks>
    <TableMap("kyoyou_sijiryoku")> _
    Public Overrides Property KyoyouSijiryoku() As String
        Get
            Return strKyoyouSijiryoku
        End Get
        Set(ByVal value As String)
            strKyoyouSijiryoku = value
        End Set
    End Property
#End Region

#Region "”»’èº°ÄŞ1"
    ''' <summary>
    ''' ”»’èº°ÄŞ1
    ''' </summary>
    ''' <remarks></remarks>
    Private intHanteiCd1 As Integer
    ''' <summary>
    ''' ”»’èº°ÄŞ1
    ''' </summary>
    ''' <value></value>
    ''' <returns> ”»’èº°ÄŞ1</returns>
    ''' <remarks></remarks>
    <TableMap("hantei_cd1", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HanteiCd1() As Integer
        Get
            Return intHanteiCd1
        End Get
        Set(ByVal value As Integer)
            intHanteiCd1 = value
        End Set
    End Property
#End Region

#Region "”»’èº°ÄŞ2"
    ''' <summary>
    ''' ”»’èº°ÄŞ2
    ''' </summary>
    ''' <remarks></remarks>
    Private intHanteiCd2 As Integer
    ''' <summary>
    ''' ”»’èº°ÄŞ2
    ''' </summary>
    ''' <value></value>
    ''' <returns> ”»’èº°ÄŞ2</returns>
    ''' <remarks></remarks>
    <TableMap("hantei_cd2", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HanteiCd2() As Integer
        Get
            Return intHanteiCd2
        End Get
        Set(ByVal value As Integer)
            intHanteiCd2 = value
        End Set
    End Property
#End Region

#Region "”»’èÚ‘±•¶š"
    ''' <summary>
    ''' ”»’èÚ‘±•¶š
    ''' </summary>
    ''' <remarks></remarks>
    Private intHanteiSetuzokuMoji As Integer
    ''' <summary>
    ''' ”»’èÚ‘±•¶š
    ''' </summary>
    ''' <value></value>
    ''' <returns> ”»’èÚ‘±•¶š</returns>
    ''' <remarks></remarks>
    <TableMap("hantei_setuzoku_moji", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HanteiSetuzokuMoji() As Integer
        Get
            Return intHanteiSetuzokuMoji
        End Get
        Set(ByVal value As Integer)
            intHanteiSetuzokuMoji = value
        End Set
    End Property
#End Region

#Region "’S“–Òº°ÄŞ"
    ''' <summary>
    ''' ’S“–Òº°ÄŞ
    ''' </summary>
    ''' <remarks></remarks>
    Private intTantousyaCd As Integer
    ''' <summary>
    ''' ’S“–Òº°ÄŞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’S“–Òº°ÄŞ</returns>
    ''' <remarks></remarks>
    <TableMap("tantousya_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TantousyaCd() As Integer
        Get
            Return intTantousyaCd
        End Get
        Set(ByVal value As Integer)
            intTantousyaCd = value
        End Set
    End Property
#End Region

#Region "’S“–Ò–¼"
    ''' <summary>
    ''' ’S“–Ò–¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strTantousyaMei As String
    ''' <summary>
    ''' ’S“–Ò–¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’S“–Ò–¼</returns>
    ''' <remarks></remarks>
    <TableMap("tantousya_mei")> _
    Public Overrides Property TantousyaMei() As String
        Get
            Return strTantousyaMei
        End Get
        Set(ByVal value As String)
            strTantousyaMei = value
        End Set
    End Property
#End Region

#Region "³”FÒº°ÄŞ"
    ''' <summary>
    ''' ³”FÒº°ÄŞ
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouninsyaCd As Integer
    ''' <summary>
    ''' ³”FÒº°ÄŞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ³”FÒº°ÄŞ</returns>
    ''' <remarks></remarks>
    <TableMap("syouninsya_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SyouninsyaCd() As Integer
        Get
            Return intSyouninsyaCd
        End Get
        Set(ByVal value As Integer)
            intSyouninsyaCd = value
        End Set
    End Property
#End Region

#Region "³”FÒ–¼"
    ''' <summary>
    ''' ³”FÒ–¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouninsyaMei As String
    ''' <summary>
    ''' ³”FÒ–¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> ³”FÒ–¼</returns>
    ''' <remarks></remarks>
    <TableMap("syouninsya_mei")> _
    Public Overrides Property SyouninsyaMei() As String
        Get
            Return strSyouninsyaMei
        End Get
        Set(ByVal value As String)
            strSyouninsyaMei = value
        End Set
    End Property
#End Region

#Region "Œv‰æ‘ì¬“ú"
    ''' <summary>
    ''' Œv‰æ‘ì¬“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKeikakusyoSakuseiDate As DateTime
    ''' <summary>
    ''' Œv‰æ‘ì¬“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> Œv‰æ‘ì¬“ú</returns>
    ''' <remarks></remarks>
    <TableMap("keikakusyo_sakusei_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KeikakusyoSakuseiDate() As DateTime
        Get
            Return dateKeikakusyoSakuseiDate
        End Get
        Set(ByVal value As DateTime)
            dateKeikakusyoSakuseiDate = value
        End Set
    End Property
#End Region

#Region "Šî‘b’f–Êº°ÄŞ"
    ''' <summary>
    ''' Šî‘b’f–Êº°ÄŞ
    ''' </summary>
    ''' <remarks></remarks>
    Private intKsDanmenCd As Integer
    ''' <summary>
    ''' Šî‘b’f–Êº°ÄŞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> Šî‘b’f–Êº°ÄŞ</returns>
    ''' <remarks></remarks>
    <TableMap("ks_danmen_cd", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KsDanmenCd() As Integer
        Get
            Return intKsDanmenCd
        End Get
        Set(ByVal value As Integer)
            intKsDanmenCd = value
        End Set
    End Property
#End Region

#Region "’f–Ê}à–¾"
    ''' <summary>
    ''' ’f–Ê}à–¾
    ''' </summary>
    ''' <remarks></remarks>
    Private strDanmenzuSetumei As String
    ''' <summary>
    ''' ’f–Ê}à–¾
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’f–Ê}à–¾</returns>
    ''' <remarks></remarks>
    <TableMap("danmenzu_setumei")> _
    Public Overrides Property DanmenzuSetumei() As String
        Get
            Return strDanmenzuSetumei
        End Get
        Set(ByVal value As String)
            strDanmenzuSetumei = value
        End Set
    End Property
#End Region

#Region "l@"
    ''' <summary>
    ''' l@
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousatu As String
    ''' <summary>
    ''' l@
    ''' </summary>
    ''' <value></value>
    ''' <returns> l@</returns>
    ''' <remarks></remarks>
    <TableMap("kousatu")> _
    Public Overrides Property Kousatu() As String
        Get
            Return strKousatu
        End Get
        Set(ByVal value As String)
            strKousatu = value
        End Set
    End Property
#End Region

#Region "Šî‘b•ñ‘—L–³"
    ''' <summary>
    ''' Šî‘b•ñ‘—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intKsHkksUmu As Integer
    ''' <summary>
    ''' Šî‘b•ñ‘—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> Šî‘b•ñ‘—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("ks_hkks_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KsHkksUmu() As Integer
        Get
            Return intKsHkksUmu
        End Get
        Set(ByVal value As Integer)
            intKsHkksUmu = value
        End Set
    End Property
#End Region

#Region "Šî‘bH–Š®—¹•ñ‘’…“ú"
    ''' <summary>
    ''' Šî‘bH–Š®—¹•ñ‘’…“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKsKojKanryHkksTykDate As DateTime
    ''' <summary>
    ''' Šî‘bH–Š®—¹•ñ‘’…“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> Šî‘bH–Š®—¹•ñ‘’…“ú</returns>
    ''' <remarks></remarks>
    <TableMap("ks_koj_kanry_hkks_tyk_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KsKojKanryHkksTykDate() As DateTime
        Get
            Return dateKsKojKanryHkksTykDate
        End Get
        Set(ByVal value As DateTime)
            dateKsKojKanryHkksTykDate = value
        End Set
    End Property
#End Region

#Region "•ÛØ‘”­só‹µ"
    ''' <summary>
    ''' •ÛØ‘”­só‹µ
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoHakJyky As Integer
    ''' <summary>
    ''' •ÛØ‘”­só‹µ
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛØ‘”­só‹µ</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_jyky", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HosyousyoHakJyky() As Integer
        Get
            Return intHosyousyoHakJyky
        End Get
        Set(ByVal value As Integer)
            intHosyousyoHakJyky = value
        End Set
    End Property
#End Region

#Region "•ÛØ‘”­só‹µİ’è“ú"
    ''' <summary>
    ''' •ÛØ‘”­só‹µİ’è“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakJykySetteiDate As DateTime
    ''' <summary>
    ''' •ÛØ‘”­só‹µİ’è“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛØ‘”­só‹µİ’è“ú</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_jyky_settei_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoHakJykySetteiDate() As DateTime
        Get
            Return dateHosyousyoHakJykySetteiDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakJykySetteiDate = value
        End Set
    End Property
#End Region

#Region "•ÛØ‘”­s“ú"
    ''' <summary>
    ''' •ÛØ‘”­s“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakDate As DateTime
    ''' <summary>
    ''' •ÛØ‘”­s“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛØ‘”­s“ú</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoHakDate() As DateTime
        Get
            Return dateHosyousyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakDate = value
        End Set
    End Property
#End Region

#Region "ˆóü•ÛØ‘”­s“ú"
    ''' <summary>
    ''' ˆóü•ÛØ‘”­s“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateInsatuHosyousyoHakDate As DateTime
    ''' <summary>
    ''' ˆóü•ÛØ‘”­s“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ˆóü•ÛØ‘”­s“ú</returns>
    ''' <remarks></remarks>
    <TableMap("insatu_hosyousyo_hak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property InsatuHosyousyoHakDate() As DateTime
        Get
            Return dateInsatuHosyousyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateInsatuHosyousyoHakDate = value
        End Set
    End Property
#End Region

#Region "•ÛØ—L–³"
    ''' <summary>
    ''' •ÛØ—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouUmu As Integer
    ''' <summary>
    ''' •ÛØ—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛØ—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HosyouUmu() As Integer
        Get
            Return intHosyouUmu
        End Get
        Set(ByVal value As Integer)
            intHosyouUmu = value
        End Set
    End Property
#End Region

#Region "•ÛØŠJn“ú"
    ''' <summary>
    ''' •ÛØŠJn“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyouKaisiDate As DateTime
    ''' <summary>
    ''' •ÛØŠJn“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛØŠJn“ú</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kaisi_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyouKaisiDate() As DateTime
        Get
            Return dateHosyouKaisiDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyouKaisiDate = value
        End Set
    End Property
#End Region

#Region "•ÛØŠúŠÔ"
    ''' <summary>
    ''' •ÛØŠúŠÔ
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouKikan As Integer
    ''' <summary>
    ''' •ÛØŠúŠÔ
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛØŠúŠÔ</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_kikan", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HosyouKikan() As Integer
        Get
            Return intHosyouKikan
        End Get
        Set(ByVal value As Integer)
            intHosyouKikan = value
        End Set
    End Property
#End Region

#Region "•ÛØ‚È‚µ——R"
    ''' <summary>
    ''' •ÛØ‚È‚µ——R
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyouNasiRiyuu As String
    ''' <summary>
    ''' •ÛØ‚È‚µ——R
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛØ‚È‚µ——R</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_nasi_riyuu")> _
    Public Overrides Property HosyouNasiRiyuu() As String
        Get
            Return strHosyouNasiRiyuu
        End Get
        Set(ByVal value As String)
            strHosyouNasiRiyuu = value
        End Set
    End Property
#End Region

#Region "•ÛØ¤•i—L–³"
    ''' <summary>
    ''' •ÛØ¤•i—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouSyouhinUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' •ÛØ¤•i—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛØ¤•i—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_syouhin_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HosyouSyouhinUmu() As Integer
        Get
            Return intHosyouSyouhinUmu
        End Get
        Set(ByVal value As Integer)
            intHosyouSyouhinUmu = value
        End Set
    End Property
#End Region

#Region "•ÛŒ¯‰ïĞ"
    ''' <summary>
    ''' •ÛŒ¯‰ïĞ
    ''' </summary>
    ''' <remarks></remarks>
    Private intHokenKaisya As Integer
    ''' <summary>
    ''' •ÛŒ¯‰ïĞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛŒ¯‰ïĞ</returns>
    ''' <remarks></remarks>
    <TableMap("hoken_kaisya", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HokenKaisya() As Integer
        Get
            Return intHokenKaisya
        End Get
        Set(ByVal value As Integer)
            intHokenKaisya = value
        End Set
    End Property
#End Region

#Region "•ÛŒ¯\¿Œ"
    ''' <summary>
    ''' •ÛŒ¯\¿Œ
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHokenSinseiTuki As DateTime
    ''' <summary>
    ''' •ÛŒ¯\¿Œ
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛŒ¯\¿Œ</returns>
    ''' <remarks></remarks>
    <TableMap("hoken_sinsei_tuki", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HokenSinseiTuki() As DateTime
        Get
            Return dateHokenSinseiTuki
        End Get
        Set(ByVal value As DateTime)
            dateHokenSinseiTuki = value
        End Set
    End Property
#End Region

#Region "•ÛØ‘Ä”­s“ú"
    ''' <summary>
    ''' •ÛØ‘Ä”­s“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoSaihakDate As DateTime
    ''' <summary>
    ''' •ÛØ‘Ä”­s“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛØ‘Ä”­s“ú</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_saihak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoSaihakDate() As DateTime
        Get
            Return dateHosyousyoSaihakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoSaihakDate = value
        End Set
    End Property
#End Region

#Region "’²¸•ñ‘—L–³"
    ''' <summary>
    ''' ’²¸•ñ‘—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysHkksUmu As Integer
    ''' <summary>
    ''' ’²¸•ñ‘—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸•ñ‘—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("tys_hkks_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TysHkksUmu() As Integer
        Get
            Return intTysHkksUmu
        End Get
        Set(ByVal value As Integer)
            intTysHkksUmu = value
        End Set
    End Property
#End Region

#Region "’²¸•ñ‘ó—Ú×"
    ''' <summary>
    ''' ’²¸•ñ‘ó—Ú×
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysHkksJyuriSyousai As String
    ''' <summary>
    ''' ’²¸•ñ‘ó—Ú×
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸•ñ‘ó—Ú×</returns>
    ''' <remarks></remarks>
    <TableMap("tys_hkks_jyuri_syousai")> _
    Public Overrides Property TysHkksJyuriSyousai() As String
        Get
            Return strTysHkksJyuriSyousai
        End Get
        Set(ByVal value As String)
            strTysHkksJyuriSyousai = value
        End Set
    End Property
#End Region

#Region "’²¸•ñ‘ó—“ú"
    ''' <summary>
    ''' ’²¸•ñ‘ó—“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysHkksJyuriDate As DateTime
    ''' <summary>
    ''' ’²¸•ñ‘ó—“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸•ñ‘ó—“ú</returns>
    ''' <remarks></remarks>
    <TableMap("tys_hkks_jyuri_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysHkksJyuriDate() As DateTime
        Get
            Return dateTysHkksJyuriDate
        End Get
        Set(ByVal value As DateTime)
            dateTysHkksJyuriDate = value
        End Set
    End Property
#End Region

#Region "’²¸•ñ‘”­‘—“ú"
    ''' <summary>
    ''' ’²¸•ñ‘”­‘—“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysHkksHakDate As DateTime
    ''' <summary>
    ''' ’²¸•ñ‘”­‘—“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸•ñ‘”­‘—“ú</returns>
    ''' <remarks></remarks>
    <TableMap("tys_hkks_hak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysHkksHakDate() As DateTime
        Get
            Return dateTysHkksHakDate
        End Get
        Set(ByVal value As DateTime)
            dateTysHkksHakDate = value
        End Set
    End Property
#End Region

#Region "’²¸•ñ‘Ä”­s“ú"
    ''' <summary>
    ''' ’²¸•ñ‘Ä”­s“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysHkksSaihakDate As DateTime
    ''' <summary>
    ''' ’²¸•ñ‘Ä”­s“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸•ñ‘Ä”­s“ú</returns>
    ''' <remarks></remarks>
    <TableMap("tys_hkks_saihak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysHkksSaihakDate() As DateTime
        Get
            Return dateTysHkksSaihakDate
        End Get
        Set(ByVal value As DateTime)
            dateTysHkksSaihakDate = value
        End Set
    End Property
#End Region

#Region "H–•ñ‘—L–³"
    ''' <summary>
    ''' H–•ñ‘—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojHkksUmu As Integer
    ''' <summary>
    ''' H–•ñ‘—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> H–•ñ‘—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KojHkksUmu() As Integer
        Get
            Return intKojHkksUmu
        End Get
        Set(ByVal value As Integer)
            intKojHkksUmu = value
        End Set
    End Property
#End Region

#Region "H–•ñ‘ó—Ú×"
    ''' <summary>
    ''' H–•ñ‘ó—Ú×
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojHkksJuriSyousai As String
    ''' <summary>
    ''' H–•ñ‘ó—Ú×
    ''' </summary>
    ''' <value></value>
    ''' <returns> H–•ñ‘ó—Ú×</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_juri_syousai")> _
    Public Overrides Property KojHkksJuriSyousai() As String
        Get
            Return strKojHkksJuriSyousai
        End Get
        Set(ByVal value As String)
            strKojHkksJuriSyousai = value
        End Set
    End Property
#End Region

#Region "H–•ñ‘ó—“ú"
    ''' <summary>
    ''' H–•ñ‘ó—“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojHkksJuriDate As DateTime
    ''' <summary>
    ''' H–•ñ‘ó—“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> H–•ñ‘ó—“ú</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_juri_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KojHkksJuriDate() As DateTime
        Get
            Return dateKojHkksJuriDate
        End Get
        Set(ByVal value As DateTime)
            dateKojHkksJuriDate = value
        End Set
    End Property
#End Region

#Region "H–•ñ‘”­‘—“ú"
    ''' <summary>
    ''' H–•ñ‘”­‘—“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojHkksHassouDate As DateTime
    ''' <summary>
    ''' H–•ñ‘”­‘—“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> H–•ñ‘”­‘—“ú</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_hassou_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KojHkksHassouDate() As DateTime
        Get
            Return dateKojHkksHassouDate
        End Get
        Set(ByVal value As DateTime)
            dateKojHkksHassouDate = value
        End Set
    End Property
#End Region

#Region "H–•ñ‘Ä”­s“ú"
    ''' <summary>
    ''' H–•ñ‘Ä”­s“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojHkksSaihakDate As DateTime
    ''' <summary>
    ''' H–•ñ‘Ä”­s“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> H–•ñ‘Ä”­s“ú</returns>
    ''' <remarks></remarks>
    <TableMap("koj_hkks_saihak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KojHkksSaihakDate() As DateTime
        Get
            Return dateKojHkksSaihakDate
        End Get
        Set(ByVal value As DateTime)
            dateKojHkksSaihakDate = value
        End Set
    End Property
#End Region

#Region "H–‰ïĞº°ÄŞ"
    ''' <summary>
    ''' H–‰ïĞº°ÄŞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojGaisyaCd As String
    ''' <summary>
    ''' H–‰ïĞº°ÄŞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> H–‰ïĞº°ÄŞ</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gaisya_cd")> _
    Public Overrides Property KojGaisyaCd() As String
        Get
            Return strKojGaisyaCd
        End Get
        Set(ByVal value As String)
            strKojGaisyaCd = value
        End Set
    End Property
#End Region

#Region "H–‰ïĞ–‹ÆŠº°ÄŞ"
    ''' <summary>
    ''' H–‰ïĞ–‹ÆŠº°ÄŞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojGaisyaJigyousyoCd As String
    ''' <summary>
    ''' H–‰ïĞ–‹ÆŠº°ÄŞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> H–‰ïĞ–‹ÆŠº°ÄŞ</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gaisya_jigyousyo_cd")> _
    Public Overrides Property KojGaisyaJigyousyoCd() As String
        Get
            Return strKojGaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strKojGaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "‰ü—ÇH–í•Ê"
    ''' <summary>
    ''' ‰ü—ÇH–í•Ê
    ''' </summary>
    ''' <remarks></remarks>
    Private intKairyKojSyubetu As Integer
    ''' <summary>
    ''' ‰ü—ÇH–í•Ê
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‰ü—ÇH–í•Ê</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_syubetu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KairyKojSyubetu() As Integer
        Get
            Return intKairyKojSyubetu
        End Get
        Set(ByVal value As Integer)
            intKairyKojSyubetu = value
        End Set
    End Property
#End Region

#Region "‰ü—ÇH–Š®—¹—\’è“ú"
    ''' <summary>
    ''' ‰ü—ÇH–Š®—¹—\’è“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKairyKojKanryYoteiDate As DateTime
    ''' <summary>
    ''' ‰ü—ÇH–Š®—¹—\’è“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‰ü—ÇH–Š®—¹—\’è“ú</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_kanry_yotei_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KairyKojKanryYoteiDate() As DateTime
        Get
            Return dateKairyKojKanryYoteiDate
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojKanryYoteiDate = value
        End Set
    End Property
#End Region

#Region "‰ü—ÇH–“ú"
    ''' <summary>
    ''' ‰ü—ÇH–“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKairyKojDate As DateTime
    ''' <summary>
    ''' ‰ü—ÇH–“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‰ü—ÇH–“ú</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KairyKojDate() As DateTime
        Get
            Return dateKairyKojDate
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojDate = value
        End Set
    End Property
#End Region

#Region "‰ü—ÇH–Š®H‘¬•ñ’…“ú"
    ''' <summary>
    ''' ‰ü—ÇH–Š®H‘¬•ñ’…“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKairyKojSokuhouTykDate As DateTime
    ''' <summary>
    ''' ‰ü—ÇH–Š®H‘¬•ñ’…“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‰ü—ÇH–Š®H‘¬•ñ’…“ú</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_sokuhou_tyk_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KairyKojSokuhouTykDate() As DateTime
        Get
            Return dateKairyKojSokuhouTykDate
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojSokuhouTykDate = value
        End Set
    End Property
#End Region

#Region "’Ç‰ÁH–‰ïĞº°ÄŞ"
    ''' <summary>
    ''' ’Ç‰ÁH–‰ïĞº°ÄŞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTKojKaisyaCd As String
    ''' <summary>
    ''' ’Ç‰ÁH–‰ïĞº°ÄŞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’Ç‰ÁH–‰ïĞº°ÄŞ</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_kaisya_cd")> _
    Public Overrides Property TKojKaisyaCd() As String
        Get
            Return strTKojKaisyaCd
        End Get
        Set(ByVal value As String)
            strTKojKaisyaCd = value
        End Set
    End Property
#End Region

#Region "’Ç‰ÁH–‰ïĞ–‹ÆŠº°ÄŞ"
    ''' <summary>
    ''' ’Ç‰ÁH–‰ïĞ–‹ÆŠº°ÄŞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTKojKaisyaJigyousyoCd As String
    ''' <summary>
    ''' ’Ç‰ÁH–‰ïĞ–‹ÆŠº°ÄŞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’Ç‰ÁH–‰ïĞ–‹ÆŠº°ÄŞ</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_kaisya_jigyousyo_cd")> _
    Public Overrides Property TKojKaisyaJigyousyoCd() As String
        Get
            Return strTKojKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTKojKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "’Ç‰ÁH–í•Ê"
    ''' <summary>
    ''' ’Ç‰ÁH–í•Ê
    ''' </summary>
    ''' <remarks></remarks>
    Private intTKojSyubetu As Integer
    ''' <summary>
    ''' ’Ç‰ÁH–í•Ê
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’Ç‰ÁH–í•Ê</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_syubetu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TKojSyubetu() As Integer
        Get
            Return intTKojSyubetu
        End Get
        Set(ByVal value As Integer)
            intTKojSyubetu = value
        End Set
    End Property
#End Region

#Region "’Ç‰ÁH–Š®—¹—\’è“ú"
    ''' <summary>
    ''' ’Ç‰ÁH–Š®—¹—\’è“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTKojKanryYoteiDate As DateTime
    ''' <summary>
    ''' ’Ç‰ÁH–Š®—¹—\’è“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’Ç‰ÁH–Š®—¹—\’è“ú</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_kanry_yotei_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TKojKanryYoteiDate() As DateTime
        Get
            Return dateTKojKanryYoteiDate
        End Get
        Set(ByVal value As DateTime)
            dateTKojKanryYoteiDate = value
        End Set
    End Property
#End Region

#Region "’Ç‰ÁH–“ú"
    ''' <summary>
    ''' ’Ç‰ÁH–“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTKojDate As DateTime
    ''' <summary>
    ''' ’Ç‰ÁH–“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’Ç‰ÁH–“ú</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TKojDate() As DateTime
        Get
            Return dateTKojDate
        End Get
        Set(ByVal value As DateTime)
            dateTKojDate = value
        End Set
    End Property
#End Region

#Region "’Ç‰ÁH–Š®H‘¬•ñ’…“ú"
    ''' <summary>
    ''' ’Ç‰ÁH–Š®H‘¬•ñ’…“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTKojSokuhouTykDate As DateTime
    ''' <summary>
    ''' ’Ç‰ÁH–Š®H‘¬•ñ’…“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’Ç‰ÁH–Š®H‘¬•ñ’…“ú</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_sokuhou_tyk_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TKojSokuhouTykDate() As DateTime
        Get
            Return dateTKojSokuhouTykDate
        End Get
        Set(ByVal value As DateTime)
            dateTKojSokuhouTykDate = value
        End Set
    End Property
#End Region

#Region "’Ç‰ÁH–‰ïĞ¿‹—L–³"
    ''' <summary>
    ''' ’Ç‰ÁH–‰ïĞ¿‹—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intTKojKaisyaSeikyuuUmu As Integer
    ''' <summary>
    ''' ’Ç‰ÁH–‰ïĞ¿‹—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’Ç‰ÁH–‰ïĞ¿‹—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("t_koj_kaisya_seikyuu_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TKojKaisyaSeikyuuUmu() As Integer
        Get
            Return intTKojKaisyaSeikyuuUmu
        End Get
        Set(ByVal value As Integer)
            intTKojKaisyaSeikyuuUmu = value
        End Set
    End Property
#End Region

#Region "’²¸Œ‹‰Ê“o˜^“ú"
    ''' <summary>
    ''' ’²¸Œ‹‰Ê“o˜^“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysKekkaAddDatetime As DateTime
    ''' <summary>
    ''' ’²¸Œ‹‰Ê“o˜^“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸Œ‹‰Ê“o˜^“ú</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kekka_add_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysKekkaAddDatetime() As DateTime
        Get
            Return dateTysKekkaAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateTysKekkaAddDatetime = value
        End Set
    End Property
#End Region

#Region "’²¸Œ‹‰ÊXV“ú"
    ''' <summary>
    ''' ’²¸Œ‹‰ÊXV“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysKekkaUpdDatetime As DateTime
    ''' <summary>
    ''' ’²¸Œ‹‰ÊXV“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸Œ‹‰ÊXV“ú</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kekka_upd_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysKekkaUpdDatetime() As DateTime
        Get
            Return dateTysKekkaUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateTysKekkaUpdDatetime = value
        End Set
    End Property
#End Region

#Region "“¯ˆË—Š“”"
    ''' <summary>
    ''' “¯ˆË—Š“”
    ''' </summary>
    ''' <remarks></remarks>
    Private intDoujiIraiTousuu As Integer = Integer.MinValue
    ''' <summary>
    ''' “¯ˆË—Š“”
    ''' </summary>
    ''' <value></value>
    ''' <returns> “¯ˆË—Š“”</returns>
    ''' <remarks></remarks>
    <TableMap("douji_irai_tousuu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property DoujiIraiTousuu() As Integer
        Get
            Return intDoujiIraiTousuu
        End Get
        Set(ByVal value As Integer)
            intDoujiIraiTousuu = value
        End Set
    End Property
#End Region

#Region "•ÛŒ¯\¿‹æ•ª"
    ''' <summary>
    ''' •ÛŒ¯\¿‹æ•ª
    ''' </summary>
    ''' <remarks></remarks>
    Private intHokenSinseiKbn As Integer
    ''' <summary>
    ''' •ÛŒ¯\¿‹æ•ª
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛŒ¯\¿‹æ•ª</returns>
    ''' <remarks></remarks>
    <TableMap("hoken_sinsei_kbn", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HokenSinseiKbn() As Integer
        Get
            Return intHokenSinseiKbn
        End Get
        Set(ByVal value As Integer)
            intHokenSinseiKbn = value
        End Set
    End Property
#End Region

#Region "àêár—L–³"
    ''' <summary>
    ''' àêár—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intKasiUmu As Integer
    ''' <summary>
    ''' àêár—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> àêár—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("kasi_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KasiUmu() As Integer
        Get
            Return intKasiUmu
        End Get
        Set(ByVal value As Integer)
            intKasiUmu = value
        End Set
    End Property
#End Region

#Region "H–‰ïĞ¿‹—L–³"
    ''' <summary>
    ''' H–‰ïĞ¿‹—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojGaisyaSeikyuuUmu As Integer
    ''' <summary>
    ''' H–‰ïĞ¿‹—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> H–‰ïĞ¿‹—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gaisya_seikyuu_umu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KojGaisyaSeikyuuUmu() As Integer
        Get
            Return intKojGaisyaSeikyuuUmu
        End Get
        Set(ByVal value As Integer)
            intKojGaisyaSeikyuuUmu = value
        End Set
    End Property
#End Region

#Region "•Ô‹àˆ—FLG"
    ''' <summary>
    ''' •Ô‹àˆ—FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intHenkinSyoriFlg As Integer
    ''' <summary>
    ''' •Ô‹àˆ—FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> •Ô‹àˆ—FLG</returns>
    ''' <remarks></remarks>
    <TableMap("henkin_syori_flg", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HenkinSyoriFlg() As Integer
        Get
            Return intHenkinSyoriFlg
        End Get
        Set(ByVal value As Integer)
            intHenkinSyoriFlg = value
        End Set
    End Property
#End Region

#Region "•Ô‹àˆ—“ú"
    ''' <summary>
    ''' •Ô‹àˆ—“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHenkinSyoriDate As DateTime
    ''' <summary>
    ''' •Ô‹àˆ—“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> •Ô‹àˆ—“ú</returns>
    ''' <remarks></remarks>
    <TableMap("henkin_syori_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HenkinSyoriDate() As DateTime
        Get
            Return dateHenkinSyoriDate
        End Get
        Set(ByVal value As DateTime)
            dateHenkinSyoriDate = value
        End Set
    End Property
#End Region

#Region "H–’S“–Ò"
    ''' <summary>
    ''' H–’S“–Ò
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojTantousyaMei As String
    ''' <summary>
    ''' H–’S“–Ò
    ''' </summary>
    ''' <value></value>
    ''' <returns> H–’S“–Ò</returns>
    ''' <remarks></remarks>
    <TableMap("koj_tantousya_mei")> _
    Public Overrides Property KojTantousyaMei() As String
        Get
            Return strKojTantousyaMei
        End Get
        Set(ByVal value As String)
            strKojTantousyaMei = value
        End Set
    End Property
#End Region

#Region "Œo—R"
    ''' <summary>
    ''' Œo—R
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeiyu As Integer
    ''' <summary>
    ''' Œo—R
    ''' </summary>
    ''' <value></value>
    ''' <returns> Œo—R</returns>
    ''' <remarks></remarks>
    <TableMap("keiyu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Keiyu() As Integer
        Get
            Return intKeiyu
        End Get
        Set(ByVal value As Integer)
            intKeiyu = value
        End Set
    End Property
#End Region

#Region "H–d—lŠm”F"
    ''' <summary>
    ''' H–d—lŠm”F
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojSiyouKakunin As Integer
    ''' <summary>
    ''' H–d—lŠm”F
    ''' </summary>
    ''' <value></value>
    ''' <returns> H–d—lŠm”F</returns>
    ''' <remarks></remarks>
    <TableMap("koj_siyou_kakunin", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KojSiyouKakunin() As Integer
        Get
            Return intKojSiyouKakunin
        End Get
        Set(ByVal value As Integer)
            intKojSiyouKakunin = value
        End Set
    End Property
#End Region

#Region "H–d—lŠm”F“ú"
    ''' <summary>
    ''' H–d—lŠm”F“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojSiyouKakuninDate As DateTime
    ''' <summary>
    ''' H–d—lŠm”F“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> H–d—lŠm”F“ú</returns>
    ''' <remarks></remarks>
    <TableMap("koj_siyou_kakunin_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KojSiyouKakuninDate() As DateTime
        Get
            Return dateKojSiyouKakuninDate
        End Get
        Set(ByVal value As DateTime)
            dateKojSiyouKakuninDate = value
        End Set
    End Property
#End Region

#Region "•ÛØ‘”­sˆË—Š‘—L–³"
    ''' <summary>
    ''' •ÛØ‘”­sˆË—Š‘—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyousyoHakIraisyoUmu As Integer
    ''' <summary>
    ''' •ÛØ‘”­sˆË—Š‘—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛØ‘”­sˆË—Š‘—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_iraisyo_umu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HosyousyoHakIraisyoUmu() As Integer
        Get
            Return intHosyousyoHakIraisyoUmu
        End Get
        Set(ByVal value As Integer)
            intHosyousyoHakIraisyoUmu = value
        End Set
    End Property
#End Region

#Region "•ÛØ‘”­sˆË—Š‘’…“ú"
    ''' <summary>
    ''' •ÛØ‘”­sˆË—Š‘’…“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakIraisyoTykDate As DateTime
    ''' <summary>
    ''' •ÛØ‘”­sˆË—Š‘’…“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛØ‘”­sˆË—Š‘’…“ú</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_iraisyo_tyk_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoHakIraisyoTykDate() As DateTime
        Get
            Return dateHosyousyoHakIraisyoTykDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakIraisyoTykDate = value
        End Set
    End Property
#End Region

#Region "İŒv‹–—ex—Í"
    ''' <summary>
    ''' İŒv‹–—ex—Í
    ''' </summary>
    ''' <remarks></remarks>
    Private decSekkeiKyoyouSijiryoku As Decimal
    ''' <summary>
    ''' İŒv‹–—ex—Í
    ''' </summary>
    ''' <value></value>
    ''' <returns> İŒv‹–—ex—Í</returns>
    ''' <remarks></remarks>
    <TableMap("sekkei_kyoyou_sijiryoku", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overrides Property SekkeiKyoyouSijiryoku() As Decimal
        Get
            Return decSekkeiKyoyouSijiryoku
        End Get
        Set(ByVal value As Decimal)
            decSekkeiKyoyouSijiryoku = value
        End Set
    End Property
#End Region

#Region "ˆË—Š—\’è“”"
    ''' <summary>
    ''' ˆË—Š—\’è“”
    ''' </summary>
    ''' <remarks></remarks>
    Private intIraiYoteiTousuu As Integer
    ''' <summary>
    ''' ˆË—Š—\’è“”
    ''' </summary>
    ''' <value></value>
    ''' <returns> ˆË—Š—\’è“”</returns>
    ''' <remarks></remarks>
    <TableMap("irai_yotei_tousuu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property IraiYoteiTousuu() As Integer
        Get
            Return intIraiYoteiTousuu
        End Get
        Set(ByVal value As Integer)
            intIraiYoteiTousuu = value
        End Set
    End Property
#End Region

#Region "Œš•¨—p“rNO"
    ''' <summary>
    ''' Œš•¨—p“rNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTatemonoYoutoNo As Integer
    ''' <summary>
    ''' Œš•¨—p“rNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> Œš•¨—p“rNO</returns>
    ''' <remarks></remarks>
    <TableMap("tatemono_youto_no", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TatemonoYoutoNo() As Integer
        Get
            Return intTatemonoYoutoNo
        End Get
        Set(ByVal value As Integer)
            intTatemonoYoutoNo = value
        End Set
    End Property
#End Region

#Region "ŒË”"
    ''' <summary>
    ''' ŒË”
    ''' </summary>
    ''' <remarks></remarks>
    Private intKosuu As Integer = Integer.MinValue
    ''' <summary>
    ''' ŒË”
    ''' </summary>
    ''' <value></value>
    ''' <returns> ŒË”</returns>
    ''' <remarks></remarks>
    <TableMap("kosuu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Kosuu() As Integer
        Get
            Return intKosuu
        End Get
        Set(ByVal value As Integer)
            intKosuu = value
        End Set
    End Property
#End Region

#Region "XVÒ"
    ''' <summary>
    ''' XVÒ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinsya As String
    ''' <summary>
    ''' XVÒ
    ''' </summary>
    ''' <value></value>
    ''' <returns> XVÒ</returns>
    ''' <remarks></remarks>
    <TableMap("kousinsya", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property Kousinsya() As String
        Get
            Return strKousinsya
        End Get
        Set(ByVal value As String)
            strKousinsya = value
        End Set
    End Property
#End Region

#Region "’²¸˜A—æ_ˆ¶æ"
    ''' <summary>
    ''' ’²¸˜A—æ_ˆ¶æ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiAtesakiMei As String
    ''' <summary>
    ''' ’²¸˜A—æ_ˆ¶æ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸˜A—æ_ˆ¶æ</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_atesaki_mei", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=60)> _
    Public Overrides Property TysRenrakusakiAtesakiMei() As String
        Get
            Return strTysRenrakusakiAtesakiMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiAtesakiMei = value
        End Set
    End Property
#End Region

#Region "’²¸˜A—æ_TEL"
    ''' <summary>
    ''' ’²¸˜A—æ_TEL
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTel As String
    ''' <summary>
    ''' ’²¸˜A—æ_TEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸˜A—æ_TEL</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_tel", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TysRenrakusakiTel() As String
        Get
            Return strTysRenrakusakiTel
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTel = value
        End Set
    End Property
#End Region

#Region "’²¸˜A—æ_FAX"
    ''' <summary>
    ''' ’²¸˜A—æ_FAX
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiFax As String
    ''' <summary>
    ''' ’²¸˜A—æ_FAX
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸˜A—æ_FAX</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_fax", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TysRenrakusakiFax() As String
        Get
            Return strTysRenrakusakiFax
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiFax = value
        End Set
    End Property
#End Region

#Region "’²¸˜A—æ_MAIL"
    ''' <summary>
    ''' ’²¸˜A—æ_MAIL
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiMail As String
    ''' <summary>
    ''' ’²¸˜A—æ_MAIL
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸˜A—æ_MAIL</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_mail", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=64)> _
    Public Overrides Property TysRenrakusakiMail() As String
        Get
            Return strTysRenrakusakiMail
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiMail = value
        End Set
    End Property
#End Region

#Region "’²¸˜A—æ_’S“–Ò"
    ''' <summary>
    ''' ’²¸˜A—æ_’S“–Ò
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTantouMei As String
    ''' <summary>
    ''' ’²¸˜A—æ_’S“–Ò
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸˜A—æ_’S“–Ò</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_tantou_mei", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TysRenrakusakiTantouMei() As String
        Get
            Return strTysRenrakusakiTantouMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTantouMei = value
        End Set
    End Property
#End Region

#Region "“o˜^Û¸Ş²İÕ°»Ş°ID"
    ''' <summary>
    ''' “o˜^Û¸Ş²İÕ°»Ş°ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String
    ''' <summary>
    ''' “o˜^Û¸Ş²İÕ°»Ş°ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> “o˜^Û¸Ş²İÕ°»Ş°ID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property AddLoginUserId() As String
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
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property AddDatetime() As DateTime
        Get
            Return dateAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetime = value
        End Set
    End Property
#End Region

#Region "XVÛ¸Ş²İÕ°»Ş°ID"
    ''' <summary>
    ''' XVÛ¸Ş²İÕ°»Ş°ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' XVÛ¸Ş²İÕ°»Ş°ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> XVÛ¸Ş²İÕ°»Ş°ID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property UpdLoginUserId() As String
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
    ''' <remarks>”r‘¼§Œä‚ğs‚¤ˆ×AŒŸõ‚ÌXV“ú•t‚ğİ’è‚µ‚Ä‚­‚¾‚³‚¢<br/>
    '''          XV‚Ì“ú•t‚ÍƒVƒXƒeƒ€“ú•t‚ªİ’è‚³‚ê‚Ü‚·</remarks>
    <TableMap("upd_datetime", IsKey:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "¤•iƒR[ƒh‚P‚Ì“@•Ê¿‹ƒŒƒR[ƒh"
    ''' <summary>
    ''' ¤•iƒR[ƒh‚P‚Ì“@•Ê¿‹ƒŒƒR[ƒh
    ''' </summary>
    ''' <remarks></remarks>
    Private objSyouhin1Record As TeibetuSeikyuuRecord
    ''' <summary>
    ''' ¤•iƒR[ƒh‚P‚Ì“@•Ê¿‹ƒŒƒR[ƒh
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¤•iƒR[ƒh‚P‚Ì“@•Ê¿‹ƒŒƒR[ƒh</returns>
    ''' <remarks></remarks>
    Public Overrides Property Syouhin1Record() As TeibetuSeikyuuRecord
        Get
            Return objSyouhin1Record
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objSyouhin1Record = value
        End Set
    End Property
#End Region

#Region "¤•iƒR[ƒh‚Q‚Ì“@•Ê¿‹ƒŒƒR[ƒhDictionary"
    ''' <summary>
    ''' ¤•iƒR[ƒh‚Q‚Ì“@•Ê¿‹ƒŒƒR[ƒhDictionary
    ''' </summary>
    ''' <remarks>TeibetuSeikyuuRecord‚ÌDictionary‚Å‚ ‚é–</remarks>
    Private htbSyouhin2Records As Dictionary(Of Integer, TeibetuSeikyuuRecord)
    ''' <summary>
    ''' ¤•iƒR[ƒh‚Q‚Ì“@•Ê¿‹Dictionary
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¤•iƒR[ƒh‚Q‚Ì“@•Ê¿‹ƒŒƒR[ƒhDictionary</returns>
    ''' <remarks>‰æ–Ê•\¦NO‚ğKey‚Æ‚µ‚½TeibetuSeikyuuRecord‚ÌƒŠƒXƒg‚Å‚ ‚é–</remarks>
    Public Overrides Property Syouhin2Records() As Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Get
            Return htbSyouhin2Records
        End Get
        Set(ByVal value As Dictionary(Of Integer, TeibetuSeikyuuRecord))
            htbSyouhin2Records = value
        End Set
    End Property
#End Region

#Region "¤•iƒR[ƒh‚R‚Ì“@•Ê¿‹ƒŒƒR[ƒhDictionary"
    ''' <summary>
    ''' ¤•iƒR[ƒh‚R‚Ì“@•Ê¿‹ƒŒƒR[ƒhDictionary
    ''' </summary>
    ''' <remarks>TeibetuSeikyuuRecord‚ÌDictionary‚Å‚ ‚é–</remarks>
    Private htbSyouhin3Records As Dictionary(Of Integer, TeibetuSeikyuuRecord)
    ''' <summary>
    ''' ¤•iƒR[ƒh‚R‚Ì“@•Ê¿‹ƒŒƒR[ƒhDictionary
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‰æ–Ê•\¦NO‚ğKey‚Æ‚µ‚½¤•iƒR[ƒh‚R‚Ì“@•Ê¿‹ƒŒƒR[ƒhƒŠƒXƒg</returns>
    ''' <remarks>TeibetuSeikyuuRecord‚ÌDictionary‚Å‚ ‚é–</remarks>
    Public Overrides Property Syouhin3Records() As Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Get
            Return htbSyouhin3Records
        End Get
        Set(ByVal value As Dictionary(Of Integer, TeibetuSeikyuuRecord))
            htbSyouhin3Records = value
        End Set
    End Property
#End Region

#Region "’Ç‰ÁH–‚Ì“@•Ê¿‹ƒŒƒR[ƒh"
    ''' <summary>
    ''' ’Ç‰ÁH–‚Ì“@•Ê¿‹ƒŒƒR[ƒh
    ''' </summary>
    ''' <remarks></remarks>
    Private objTuikaKoujiRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' ’Ç‰ÁH–‚Ì“@•Ê¿‹ƒŒƒR[ƒh
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’Ç‰ÁH–‚Ì“@•Ê¿‹ƒŒƒR[ƒh</returns>
    ''' <remarks></remarks>
    Public Overrides Property TuikaKoujiRecord() As TeibetuSeikyuuRecord
        Get
            Return objTuikaKoujiRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objTuikaKoujiRecord = value
        End Set
    End Property
#End Region

#Region "‰ü—ÇH–‚Ì“@•Ê¿‹ƒŒƒR[ƒh"
    ''' <summary>
    ''' ‰ü—ÇH–‚Ì“@•Ê¿‹ƒŒƒR[ƒh
    ''' </summary>
    ''' <remarks></remarks>
    Private objKairyouKoujiRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' ‰ü—ÇH–‚Ì“@•Ê¿‹ƒŒƒR[ƒh
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‰ü—ÇH–‚Ì“@•Ê¿‹ƒŒƒR[ƒh</returns>
    ''' <remarks></remarks>
    Public Overrides Property KairyouKoujiRecord() As TeibetuSeikyuuRecord
        Get
            Return objKairyouKoujiRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objKairyouKoujiRecord = value
        End Set
    End Property
#End Region

#Region "’²¸•ñ‘‚Ì“@•Ê¿‹ƒŒƒR[ƒh"
    ''' <summary>
    ''' ’²¸•ñ‘‚Ì“@•Ê¿‹ƒŒƒR[ƒh
    ''' </summary>
    ''' <remarks></remarks>
    Private objTyousaHoukokusyoRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' ’²¸•ñ‘‚Ì“@•Ê¿‹ƒŒƒR[ƒh
    ''' </summary>
    ''' <value></value>
    ''' <returns>’²¸•ñ‘‚Ì“@•Ê¿‹ƒŒƒR[ƒh</returns>
    ''' <remarks></remarks>
    Public Overrides Property TyousaHoukokusyoRecord() As TeibetuSeikyuuRecord
        Get
            Return objTyousaHoukokusyoRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objTyousaHoukokusyoRecord = value
        End Set
    End Property
#End Region

#Region "H–•ñ‘‚Ì“@•Ê¿‹ƒŒƒR[ƒh"
    ''' <summary>
    ''' H–•ñ‘‚Ì“@•Ê¿‹ƒŒƒR[ƒh
    ''' </summary>
    ''' <remarks></remarks>
    Private objKoujiHoukokusyoRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' H–•ñ‘‚Ì“@•Ê¿‹ƒŒƒR[ƒh
    ''' </summary>
    ''' <value></value>
    ''' <returns>H–•ñ‘‚Ì“@•Ê¿‹ƒŒƒR[ƒh</returns>
    ''' <remarks></remarks>
    Public Overrides Property KoujiHoukokusyoRecord() As TeibetuSeikyuuRecord
        Get
            Return objKoujiHoukokusyoRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objKoujiHoukokusyoRecord = value
        End Set
    End Property
#End Region

#Region "•ÛØ‘‚Ì“@•Ê¿‹ƒŒƒR[ƒh"
    ''' <summary>
    ''' •ÛØ‘‚Ì“@•Ê¿‹ƒŒƒR[ƒh
    ''' </summary>
    ''' <remarks></remarks>
    Private objHosyousyoRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' •ÛØ‘‚Ì“@•Ê¿‹ƒŒƒR[ƒh
    ''' </summary>
    ''' <value></value>
    ''' <returns>•ÛØ‘‚Ì“@•Ê¿‹ƒŒƒR[ƒh</returns>
    ''' <remarks></remarks>
    Public Overrides Property HosyousyoRecord() As TeibetuSeikyuuRecord
        Get
            Return objHosyousyoRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objHosyousyoRecord = value
        End Set
    End Property
#End Region

#Region "‰ğ–ñ•¥–ß‚Ì“@•Ê¿‹ƒŒƒR[ƒh"
    ''' <summary>
    ''' ‰ğ–ñ•¥–ß‚Ì“@•Ê¿‹ƒŒƒR[ƒh
    ''' </summary>
    ''' <remarks></remarks>
    Private objKaiyakuHaraimodosiRecord As TeibetuSeikyuuRecord
    ''' <summary>
    ''' ‰ğ–ñ•¥–ß‚Ì“@•Ê¿‹ƒŒƒR[ƒh
    ''' </summary>
    ''' <value></value>
    ''' <returns>‰ğ–ñ•¥–ß‚Ì“@•Ê¿‹ƒŒƒR[ƒh</returns>
    ''' <remarks></remarks>
    Public Overrides Property KaiyakuHaraimodosiRecord() As TeibetuSeikyuuRecord
        Get
            Return objKaiyakuHaraimodosiRecord
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            objKaiyakuHaraimodosiRecord = value
        End Set
    End Property
#End Region

#Region "ã‹LˆÈŠO‚Ì“@•Ê¿‹ƒŒƒR[ƒhƒŠƒXƒg"
    ''' <summary>
    ''' ã‹LˆÈŠO‚Ì“@•Ê¿‹ƒŒƒR[ƒhƒŠƒXƒg
    ''' </summary>
    ''' <remarks>TeibetuSeikyuuRecord‚ÌƒŠƒXƒg‚Å‚ ‚é–</remarks>
    Private arrOtherTeibetuSeikyuuRecords As List(Of TeibetuSeikyuuRecord)
    ''' <summary>
    ''' ã‹LˆÈŠO‚Ì“@•Ê¿‹ƒŒƒR[ƒhƒŠƒXƒg
    ''' </summary>
    ''' <value></value>
    ''' <returns> ã‹LˆÈŠO‚Ì“@•Ê¿‹ƒŒƒR[ƒhƒŠƒXƒg</returns>
    ''' <remarks>TeibetuSeikyuuRecord‚ÌƒŠƒXƒg‚Å‚ ‚é–</remarks>
    Public Overrides Property OtherTeibetuSeikyuuRecords() As List(Of TeibetuSeikyuuRecord)
        Get
            Return arrOtherTeibetuSeikyuuRecords
        End Get
        Set(ByVal value As List(Of TeibetuSeikyuuRecord))
            arrOtherTeibetuSeikyuuRecords = value
        End Set
    End Property
#End Region

#Region "“@•Ê“ü‹àƒŒƒR[ƒhDictionary"
    ''' <summary>
    ''' “@•Ê“ü‹àƒŒƒR[ƒhDictionary
    ''' </summary>
    ''' <remarks></remarks>
    Private htbTeibetuNyuukinRecords As Dictionary(Of String, TeibetuNyuukinRecord)
    ''' <summary>
    ''' “@•Ê“ü‹àƒŒƒR[ƒhDictionary
    ''' </summary>
    ''' <value></value>
    ''' <returns> “@•Ê“ü‹àƒŒƒR[ƒhDictionary</returns>
    ''' <remarks>•ª—ŞƒR[ƒh‚ğKey‚ÉTeibetuNyuukinRecord‚ğ•Û‚µ‚Ü‚·<br/>
    ''' •ª—Ş’PˆÊ‚É“ü‹àŠz‚ğ•Û‚µ‚Ä‚Ü‚·B<br/>
    ''' ‰æ–Êİ’è‚É‚ÍˆÈ‰º‚ğg—p‚µ‚Ü‚·<br/>
    ''' 100:¤•i‚PC‚Q‹¤’Ê<br/>
    ''' 120:¤•i‚R<br/>
    ''' 130:’Ç‰ÁH–<br/>
    ''' 140:‰ü—ÇH–<br/>
    ''' 150:’²¸•ñ‘<br/>
    ''' 160:H–•ñ‘<br/>
    ''' 170:•ÛØ‘<br/>
    ''' 180:‰ğ–ñ•¥–ß</remarks>
    Public Overrides Property TeibetuNyuukinRecords() As Dictionary(Of String, TeibetuNyuukinRecord)
        Get
            Return htbTeibetuNyuukinRecords
        End Get
        Set(ByVal value As Dictionary(Of String, TeibetuNyuukinRecord))
            htbTeibetuNyuukinRecords = value
        End Set
    End Property
#End Region

#Region "ReportIf ˜AŒg—p€–Ú"

#Region "ReportIF İ’è—p \‘¢–¼"
    ''' <summary>
    ''' ReportIF İ’è—p \‘¢–¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strKouzouMeiIf As String = ""
    ''' <summary>
    ''' ReportIF İ’è—p \‘¢–¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF İ’è—p \‘¢–¼</returns>
    ''' <remarks></remarks>
    Public Overrides Property KouzouMeiIf() As String
        Get
            Return strKouzouMeiIf
        End Get
        Set(ByVal value As String)
            strKouzouMeiIf = value
        End Set
    End Property
#End Region

#Region "ReportIF İ’è—p ’²¸•û–@–¼"
    ''' <summary>
    ''' ReportIF İ’è—p ’²¸•û–@–¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysHouhouMeiIf As String = ""
    ''' <summary>
    ''' ReportIF İ’è—p ’²¸•û–@–¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF İ’è—p ’²¸•û–@–¼</returns>
    ''' <remarks></remarks>
    Public Overrides Property TysHouhouMeiIf() As String
        Get
            Return strTysHouhouMeiIf
        End Get
        Set(ByVal value As String)
            strTysHouhouMeiIf = value
        End Set
    End Property
#End Region

#Region "ReportIF İ’è—p ‰Á–¿“X–¼"
    ''' <summary>
    ''' ReportIF İ’è—p ‰Á–¿“X–¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMeiIf As String = ""
    ''' <summary>
    ''' ReportIF İ’è—p ‰Á–¿“X–¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF İ’è—p ‰Á–¿“X–¼</returns>
    ''' <remarks></remarks>
    Public Overrides Property KameitenMeiIf() As String
        Get
            Return strKameitenMeiIf
        End Get
        Set(ByVal value As String)
            strKameitenMeiIf = value
        End Set
    End Property
#End Region

#Region "ReportIF İ’è—p ‰Á–¿“XTEL"
    ''' <summary>
    ''' ReportIF İ’è—p ‰Á–¿“XTEL
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenTelIf As String = ""
    ''' <summary>
    ''' ReportIF İ’è—p ‰Á–¿“XTEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF İ’è—p ‰Á–¿“XTEL</returns>
    ''' <remarks></remarks>
    Public Overrides Property KameitenTelIf() As String
        Get
            Return strKameitenTelIf
        End Get
        Set(ByVal value As String)
            strKameitenTelIf = value
        End Set
    End Property
#End Region

#Region "ReportIF İ’è—p ‰Á–¿“XFAX"
    ''' <summary>
    ''' ReportIF İ’è—p ‰Á–¿“XFAX
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenFaxIf As String = ""
    ''' <summary>
    ''' ReportIF İ’è—p ‰Á–¿“XFAX
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF İ’è—p ‰Á–¿“XFAX</returns>
    ''' <remarks></remarks>
    Public Overrides Property KameitenFaxIf() As String
        Get
            Return strKameitenFaxIf
        End Get
        Set(ByVal value As String)
            strKameitenFaxIf = value
        End Set
    End Property
#End Region

#Region "ReportIF İ’è—p ‰Á–¿“XMAIL"
    ''' <summary>
    ''' ReportIF İ’è—p ‰Á–¿“XMAIL
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMailIf As String = ""
    ''' <summary>
    ''' ReportIF İ’è—p ‰Á–¿“XMAIL
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF İ’è—p ‰Á–¿“XMAIL</returns>
    ''' <remarks></remarks>
    Public Overrides Property KameitenMailIf() As String
        Get
            Return strKameitenMailIf
        End Get
        Set(ByVal value As String)
            strKameitenMailIf = value
        End Set
    End Property
#End Region

#Region "ReportIF İ’è—p ’²¸•ñ‘³”FÒ"
    ''' <summary>
    ''' ReportIF İ’è—p ’²¸•ñ‘³”FÒ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyousaHoukokusyoSyouninsyaIf As String = ""
    ''' <summary>
    ''' ReportIF İ’è—p ’²¸•ñ‘³”FÒ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF İ’è—p ’²¸•ñ‘³”FÒ</returns>
    ''' <remarks></remarks>
    <TableMap("t_houkoku_syounin", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TyousaHoukokusyoSyouninsyaIf() As String
        Get
            Return strTyousaHoukokusyoSyouninsyaIf
        End Get
        Set(ByVal value As String)
            strTyousaHoukokusyoSyouninsyaIf = value
        End Set
    End Property
#End Region

#Region "ReportIF İ’è—p i’»ƒXƒe[ƒ^ƒX"
    ''' <summary>
    ''' ReportIF İ’è—p i’»ƒXƒe[ƒ^ƒX
    ''' </summary>
    ''' <remarks></remarks>
    Private strStatusIf As String = ""
    ''' <summary>
    ''' ReportIF İ’è—p i’»ƒXƒe[ƒ^ƒX
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF İ’è—p i’»ƒXƒe[ƒ^ƒX</returns>
    ''' <remarks></remarks>
    <TableMap("status", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
    Public Overrides Property StatusIf() As String
        Get
            Return strStatusIf
        End Get
        Set(ByVal value As String)
            strStatusIf = value
        End Set
    End Property
#End Region

#Region "’²¸‰ïĞ–¼"
    ''' <summary>
    ''' ’²¸‰ïĞ–¼Ş
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaMeiIf As String = ""
    ''' <summary>
    ''' ’²¸‰ïĞ–¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸‰ïĞ–¼</returns>
    ''' <remarks></remarks>
    Public Overrides Property TysKaisyaMeiIf() As String
        Get
            Return strTysKaisyaMeiIf
        End Get
        Set(ByVal value As String)
            strTysKaisyaMeiIf = value
        End Set
    End Property
#End Region

#End Region

#Region "“ü‹àŠzæ“¾"
    ''' <summary>
    ''' •ª—ŞƒR[ƒh‚ğƒL[‚É“ü‹àŠz‚ğæ“¾‚µ‚Ü‚·<br/>
    ''' ‘¶İ‚µ‚È‚¢ê‡‚Í0‚ğ•Ô‚µ‚Ü‚·
    ''' </summary>
    ''' <param name="bunruiCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function getNyuukinGaku(ByVal bunruiCd As String) As Integer

        'ƒƒ\ƒbƒh–¼Aˆø”‚Ìî•ñ‚Ì‘Ş”ğ
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getNyuukinGaku", _
                                                    bunruiCd)

        Dim nyuukinGaku As Integer = 0

        If Not Me.TeibetuNyuukinRecords Is Nothing Then
            ' ¤•i‚R‚Ì“ü‹àŠz‚ğæ“¾
            If Me.TeibetuNyuukinRecords.ContainsKey(bunruiCd) = True Then
                Dim record As New TeibetuNyuukinRecord
                record = Me.TeibetuNyuukinRecords.Item(bunruiCd)
                nyuukinGaku = record.NyuukinGaku
            End If
        End If

        Return nyuukinGaku

    End Function

#End Region

#Region "Å‹àŠzæ“¾"
    ''' <summary>
    ''' •ª—ŞƒR[ƒh‚ğƒL[‚ÉÅ‹àŠz‚ğæ“¾‚µ‚Ü‚·<br/>
    ''' •ª—ŞƒR[ƒh‚ğ•¡”w’è‚µ‚½ê‡AÅ‹àŠz‚ğ‰ÁZ‚µ‚Ü‚·<br/>
    ''' ‘¶İ‚µ‚È‚¢ê‡‚Í0‚ğ•Ô‚µ‚Ü‚·
    ''' </summary>
    ''' <param name="bunruiCodes">•ª—ŞƒR[ƒh‚Ì”z—ñ</param>
    ''' <returns>Å‹àŠzi•ª—ŞƒR[ƒh‚Ì”z—ñ•ª‰ÁZj</returns>
    ''' <remarks>
    ''' ¤•i‚Q‚Í•ª—ŞƒR[ƒh"110","115"‚Ì‰½‚ê‚©‚Å‰Â<br/>
    ''' ¦—¼•ûw’è‚·‚é‚Æd•¡‰ÁZ‚³‚ê‚Ü‚·
    ''' </remarks>
    Public Overloads Function getZeikomiGaku(ByVal bunruiCodes() As String) As Integer

        'ƒƒ\ƒbƒh–¼Aˆø”‚Ìî•ñ‚Ì‘Ş”ğ
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getZeikomiGaku", _
                                                    bunruiCodes)

        Dim zeikomi As Integer = 0
        Dim i As Integer

        For Each bunruiCd As String In bunruiCodes
            If bunruiCd = "100" Then
                ' ¤•i‚P
                If Not Me.Syouhin1Record Is Nothing Then
                    zeikomi = zeikomi + Me.Syouhin1Record.ZeikomiUriGaku
                End If

            ElseIf bunruiCd = "110" Or _
                   bunruiCd = "115" Then
                ' ¤•i‚Q
                If Not Me.Syouhin2Records Is Nothing Then
                    For i = 1 To 4
                        If Me.Syouhin2Records.ContainsKey(i) = True Then
                            Dim syouhin2Rec As New TeibetuSeikyuuRecord
                            syouhin2Rec = Me.Syouhin2Records.Item(i)
                            zeikomi = zeikomi + syouhin2Rec.ZeikomiUriGaku
                        End If
                    Next
                End If
            ElseIf bunruiCd = "120" Then
                ' ¤•i‚R
                If Not Me.Syouhin3Records Is Nothing Then
                    For i = 1 To 9
                        If Me.Syouhin3Records.ContainsKey(i) = True Then
                            Dim syouhin3Rec As New TeibetuSeikyuuRecord
                            syouhin3Rec = Me.Syouhin3Records.Item(i)
                            zeikomi = zeikomi + syouhin3Rec.ZeikomiUriGaku
                        End If
                    Next
                End If
            ElseIf bunruiCd = "130" Then
                ' ‰ü—ÇH–
                If Not Me.KairyouKoujiRecord Is Nothing Then
                    zeikomi = zeikomi + Me.KairyouKoujiRecord.ZeikomiUriGaku
                End If
            ElseIf bunruiCd = "140" Then
                ' ’Ç‰ÁH–
                If Not Me.TuikaKoujiRecord Is Nothing Then
                    zeikomi = zeikomi + Me.TuikaKoujiRecord.ZeikomiUriGaku
                End If
            ElseIf bunruiCd = "150" Then
                ' ’²¸•ñ‘
                If Not Me.TyousaHoukokusyoRecord Is Nothing Then
                    zeikomi = zeikomi + Me.TyousaHoukokusyoRecord.ZeikomiUriGaku
                End If
            ElseIf bunruiCd = "160" Then
                ' H–•ñ‘
                If Not Me.KoujiHoukokusyoRecord Is Nothing Then
                    zeikomi = zeikomi + Me.KoujiHoukokusyoRecord.ZeikomiUriGaku
                End If
            ElseIf bunruiCd = "170" Then
                ' •ÛØ‘
                If Not Me.HosyousyoRecord Is Nothing Then
                    zeikomi = zeikomi + Me.HosyousyoRecord.ZeikomiUriGaku
                End If
            ElseIf bunruiCd = "180" Then
                ' ‰ğ–ñ•¥–ß
                If Not Me.KaiyakuHaraimodosiRecord Is Nothing Then
                    zeikomi = zeikomi + Me.KaiyakuHaraimodosiRecord.ZeikomiUriGaku
                End If
            End If
        Next

        Return zeikomi

    End Function

#End Region

#Region "¤•i‚P`‚R”­’‘‹àŠz‡Œvæ“¾"
    ''' <summary>
    ''' ¤•i‚P`‚R‚Ü‚Å‚Ì”­’‘‹àŠz‚ğƒTƒ}ƒŠ[‚µ•Ô‹p‚µ‚Ü‚·
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function GetSyouhinHattyusyoKingaku() As Integer

        'ƒƒ\ƒbƒh–¼Aˆø”‚Ìî•ñ‚Ì‘Ş”ğ
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getHattyusyoKingaku")

        Dim hattyuuGaku As Integer = 0

        If Not Me.Syouhin1Record Is Nothing Then
            '@¤•i‚P‚Ì”­’‘‹àŠz
            Dim work As Decimal = IIf(Me.Syouhin1Record.HattyuusyoGaku = _
                                      Integer.MinValue, 0, Me.Syouhin1Record.HattyuusyoGaku)
            hattyuuGaku = Fix(work * (1 + Me.Syouhin1Record.Zeiritu))
        End If

        If Not Me.Syouhin2Records Is Nothing Then
            Dim i As Integer
            For i = 1 To 4
                If Me.Syouhin2Records.ContainsKey(i) Then
                    '@¤•i‚Q‚Ì”­’‘‹àŠz
                    Dim work As Decimal = IIf(Me.Syouhin2Records.Item(i).HattyuusyoGaku = _
                                              Integer.MinValue, _
                                              0, _
                                              Me.Syouhin2Records.Item(i).HattyuusyoGaku)
                    hattyuuGaku = hattyuuGaku + Fix(work * (1 + Me.Syouhin2Records.Item(i).Zeiritu))
                End If
            Next
        End If

        If Not Me.Syouhin3Records Is Nothing Then
            Dim i As Integer
            For i = 1 To 9
                If Me.Syouhin3Records.ContainsKey(i) Then
                    '@¤•i‚R‚Ì”­’‘‹àŠz
                    Dim work As Decimal = IIf(Me.Syouhin3Records.Item(i).HattyuusyoGaku = _
                                              Integer.MinValue, _
                                              0, _
                                              Me.Syouhin3Records.Item(i).HattyuusyoGaku)
                    hattyuuGaku = hattyuuGaku + Fix(work * (1 + Me.Syouhin3Records.Item(i).Zeiritu))
                End If
            Next
        End If

        Return hattyuuGaku

    End Function

#End Region

#Region "H–”­’‘‹àŠz‡Œvæ“¾"
    ''' <summary>
    ''' H–‚Ì”­’‘‹àŠz‚ğƒTƒ}ƒŠ[‚µ•Ô‹p‚µ‚Ü‚·
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function GetKoujiHattyusyoKingaku() As Integer

        'ƒƒ\ƒbƒh–¼Aˆø”‚Ìî•ñ‚Ì‘Ş”ğ
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getHattyusyoKingaku")

        Dim hattyuuGaku As Integer = 0

        If Not Me.KairyouKoujiRecord Is Nothing Then
            '@‰ü—ÇH–‚Ì”­’‘‹àŠz
            Dim work As Decimal = IIf(Me.KairyouKoujiRecord.HattyuusyoGaku = _
                                      Integer.MinValue, 0, Me.KairyouKoujiRecord.HattyuusyoGaku)
            hattyuuGaku = Fix(work * (1 + Me.KairyouKoujiRecord.Zeiritu))
        End If

        If Not Me.TuikaKoujiRecord Is Nothing Then
            '@’Ç‰ÁH–‚Ì”­’‘‹àŠz
            Dim work As Decimal = IIf(Me.TuikaKoujiRecord.HattyuusyoGaku = _
                                      Integer.MinValue, 0, Me.TuikaKoujiRecord.HattyuusyoGaku)
            hattyuuGaku = hattyuuGaku + Fix(work * (1 + Me.TuikaKoujiRecord.Zeiritu))
        End If

        Return hattyuuGaku

    End Function

#End Region

#Region "’uŠ·H– Ê^ó—/Ê^ƒRƒƒ“ƒg"

    ''' <summary>
    ''' Ê^ó—
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyasinJuri As String
    ''' <summary>
    ''' Ê^ó—
    ''' </summary>
    ''' <value></value>
    ''' <returns>Ê^ó—</returns>
    ''' <remarks></remarks>
    <TableMap("syasin_jyuri", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overrides Property TikanKoujiSyasinJuri() As String
        Get
            Return strSyasinJuri
        End Get
        Set(ByVal value As String)
            strSyasinJuri = value
        End Set
    End Property

    ''' <summary>
    ''' Ê^ƒRƒƒ“ƒg
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyasinComment As String
    ''' <summary>
    ''' Ê^ƒRƒƒ“ƒg
    ''' </summary>
    ''' <value></value>
    ''' <returns>Ê^ƒRƒƒ“ƒg</returns>
    ''' <remarks></remarks>
    <TableMap("syasin_comment", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=100)> _
    Public Overrides Property TikanKoujiSyasinComment() As String
        Get
            Return strSyasinComment
        End Get
        Set(ByVal value As String)
            strSyasinComment = value
        End Set
    End Property

#End Region

#Region "“ú•tƒ}ƒXƒ^"

#Region "•ÛØ‘”­s“ú"
    ''' <summary>
    ''' •ÛØ‘”­s“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakkouDate As DateTime
    ''' <summary>
    ''' •ÛØ‘”­s“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛØ‘”­s“ú</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HosyousyoHakkouDate() As DateTime
        Get
            Return dateHosyousyoHakkouDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakkouDate = value
        End Set
    End Property
#End Region

#Region "•ñ‘”­‘—“ú"
    ''' <summary>
    ''' •ñ‘”­‘—“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHoukokusyoHassouDate As DateTime
    ''' <summary>
    ''' •ñ‘”­‘—“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ñ‘”­‘—“ú</returns>
    ''' <remarks></remarks>
    <TableMap("hkks_hassou_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HoukokusyoHassouDate() As DateTime
        Get
            Return dateHoukokusyoHassouDate
        End Get
        Set(ByVal value As DateTime)
            dateHoukokusyoHassouDate = value
        End Set
    End Property
#End Region

#End Region

#Region "š•ÛØ‰æ–ÊŒÅ—L‚ÌƒvƒƒpƒeƒB"

#Region "“ü‹àŠm”FğŒ–¼"
    ''' <summary>
    ''' “ü‹àŠm”FğŒ–¼
    ''' </summary>
    ''' <remarks></remarks>
    Private _nyuukinKakuninJyoukenMei As String
    ''' <summary>
    ''' “ü‹àŠm”FğŒ–¼
    ''' </summary>
    ''' <value></value>
    ''' <returns>“ü‹àŠm”FğŒ–¼</returns>
    ''' <remarks>‰Á–¿“Xƒ}ƒXƒ^‚Ì“ü‹àŠm”FğŒ‚ğKey‚É–¼Ìƒ}ƒXƒ^‚Ìí•Ê"05"‚æ‚èæ“¾‚µ‚½’l‚ğİ’è‚µ‚Ü‚·</remarks>
    <TableMap("nyuukin_kakunin_jyouken_mei")> _
    Public Overloads Property NyuukinKakuninJyoukenMei() As String
        Get
            Return _nyuukinKakuninJyoukenMei
        End Get
        Set(ByVal value As String)
            _nyuukinKakuninJyoukenMei = value
        End Set
    End Property
#End Region

#Region "•ñ‘ó—ó‹µ"
    ''' <summary>
    ''' •ñ‘ó—ó‹µ
    ''' </summary>
    ''' <remarks></remarks>
    Private _hkksJyuriJyky As String
    ''' <summary>
    ''' •ñ‘ó—ó‹µ
    ''' </summary>
    ''' <value></value>
    ''' <returns>•ñ‘ó—ó‹µ</returns>
    ''' <remarks>’n”Õƒe[ƒuƒ‹DH–•ñ‘—L–³•ñ‘ó—ƒ}ƒXƒ^.•ñ‘ó—NO‚Å•ñ‘ó—ó‹µ‚ğİ’è‚µ‚Ü‚·</remarks>
    <TableMap("hkks_jyuri_jyky")> _
    Public Overloads Property HkksJyuriJyky() As String
        Get
            Return _hkksJyuriJyky
        End Get
        Set(ByVal value As String)
            _hkksJyuriJyky = value
        End Set
    End Property
#End Region

#Region "‰ü—ÇH–í•Ê–¼"
    ''' <summary>
    ''' ‰ü—ÇH–í•Ê–¼
    ''' </summary>
    ''' <remarks></remarks>
    Private _kairyKojSyubetuMei As String
    ''' <summary>
    ''' ‰ü—ÇH–í•Ê–¼
    ''' </summary>
    ''' <value></value>
    ''' <returns>‰ü—ÇH–í•Ê–¼</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_syubetu_mei")> _
    Public Overloads Property KairyKojSyubetuMei() As String
        Get
            Return _kairyKojSyubetuMei
        End Get
        Set(ByVal value As String)
            _kairyKojSyubetuMei = value
        End Set
    End Property
#End Region

#Region "”Ì‘£•i¿‹æ"
    ''' <summary>
    ''' ”Ì‘£•i¿‹æ
    ''' </summary>
    ''' <remarks></remarks>
    Private _hansokuhinSeikyuusaki As String
    ''' <summary>
    ''' ”Ì‘£•i¿‹æ
    ''' </summary>
    ''' <value></value>
    ''' <returns>”Ì‘£•i¿‹æ</returns>
    ''' <remarks></remarks>
    <TableMap("hansokuhin_seikyuusaki")> _
    Public Overloads Property HansokuhinSeikyuusaki() As String
        Get
            Return _hansokuhinSeikyuusaki
        End Get
        Set(ByVal value As String)
            _hansokuhinSeikyuusaki = value
        End Set
    End Property
#End Region

#Region "’²¸¿‹æ"
    ''' <summary>
    ''' ’²¸¿‹æ
    ''' </summary>
    ''' <remarks></remarks>
    Private _tysSeikyuuSaki As String
    ''' <summary>
    ''' ’²¸¿‹æ
    ''' </summary>
    ''' <value></value>
    ''' <returns>’²¸¿‹æ</returns>
    ''' <remarks></remarks>
    <TableMap("tys_seikyuu_saki")> _
    Public Overloads Property TysSeikyuuSaki() As String
        Get
            Return _tysSeikyuuSaki
        End Get
        Set(ByVal value As String)
            _tysSeikyuuSaki = value
        End Set
    End Property
#End Region

#Region "•t•ÛØ–¾‘FLG"
    ''' <summary>
    ''' •t•ÛØ–¾‘FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intFuhoSyoumeisyoFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' •t•ÛØ–¾‘FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>•t•ÛØ–¾‘FLG</returns>
    ''' <remarks></remarks>
    <TableMap("fuho_syoumeisyo_flg", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property FuhoSyoumeisyoFlg() As Integer
        Get
            Return intFuhoSyoumeisyoFlg
        End Get
        Set(ByVal value As Integer)
            intFuhoSyoumeisyoFlg = value
        End Set
    End Property
#End Region

#Region "•t•ÛØ–¾‘”­‘—“ú"
    ''' <summary>
    ''' •t•ÛØ–¾‘”­‘—“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateFuhoSyoumeisyoHassouDate As DateTime
    ''' <summary>
    ''' •t•ÛØ–¾‘”­‘—“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("fuho_syoumeisyo_hassou_date")> _
    Public Overloads Property FuhoSyoumeisyoHassouDate() As DateTime
        Get
            Return dateFuhoSyoumeisyoHassouDate
        End Get
        Set(ByVal value As DateTime)
            dateFuhoSyoumeisyoHassouDate = value
        End Set
    End Property
#End Region

#Region "˜A“•¨Œ‘Î‰"

#Region "ˆ—Œ”"
    ''' <summary>
    ''' ˆ—Œ”Ş
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyoriKensuu As Integer = 0
    ''' <summary>
    ''' ˆ—Œ”
    ''' </summary>
    ''' <value></value>
    ''' <returns> ˆ—Œ”</returns>
    ''' <remarks></remarks>
    Public Overrides Property SyoriKensuu() As Integer
        Get
            Return intSyoriKensuu
        End Get
        Set(ByVal value As Integer)
            intSyoriKensuu = value
        End Set
    End Property
#End Region

#Region "˜A“•¨Œ”"
    ''' <summary>
    ''' ˜A“•¨Œ”Ş
    ''' </summary>
    ''' <remarks></remarks>
    Private intRentouBukkenSuu As Integer = 1
    ''' <summary>
    ''' ˜A“•¨Œ”
    ''' </summary>
    ''' <value></value>
    ''' <returns> ˜A“•¨Œ”</returns>
    ''' <remarks></remarks>
    Public Overrides Property RentouBukkenSuu() As Integer
        Get
            Return intRentouBukkenSuu
        End Get
        Set(ByVal value As Integer)
            intRentouBukkenSuu = value
        End Set
    End Property
#End Region

#End Region

#End Region

#Region "—\–ñÏFLG"
    ''' <summary>
    ''' —\–ñÏFLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intYoyakuZumiFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' —\–ñÏFLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>—\–ñÏFLG</returns>
    ''' <remarks></remarks>
    <TableMap("yoyaku_zumi_flg", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YoyakuZumiFlg() As Integer
        Get
            Return intYoyakuZumiFlg
        End Get
        Set(ByVal value As Integer)
            intYoyakuZumiFlg = value
        End Set
    End Property
#End Region

#Region "ˆÄ“à}"
    ''' <summary>
    ''' ˆÄ“à}
    ''' </summary>
    ''' <remarks></remarks>
    Private intAnnaiZu As Integer = Integer.MinValue
    ''' <summary>
    ''' ˆÄ“à}
    ''' </summary>
    ''' <value></value>
    ''' <returns>ˆÄ“à}</returns>
    ''' <remarks></remarks>
    <TableMap("annaizu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property AnnaiZu() As Integer
        Get
            Return intAnnaiZu
        End Get
        Set(ByVal value As Integer)
            intAnnaiZu = value
        End Set
    End Property
#End Region

#Region "”z’u}"
    ''' <summary>
    ''' ”z’u}
    ''' </summary>
    ''' <remarks></remarks>
    Private intHaitiZu As Integer = Integer.MinValue
    ''' <summary>
    ''' ”z’u}
    ''' </summary>
    ''' <value></value>
    ''' <returns>”z’u}</returns>
    ''' <remarks></remarks>
    <TableMap("haitizu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HaitiZu() As Integer
        Get
            Return intHaitiZu
        End Get
        Set(ByVal value As Integer)
            intHaitiZu = value
        End Set
    End Property
#End Region

#Region "ŠeŠK•½–Ê}"
    ''' <summary>
    ''' ŠeŠK•½–Ê}
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakukaiHeimenZu As Integer = Integer.MinValue
    ''' <summary>
    ''' ŠeŠK•½–Ê}
    ''' </summary>
    ''' <value></value>
    ''' <returns>ŠeŠK•½–Ê}</returns>
    ''' <remarks></remarks>
    <TableMap("kakukai_heimenzu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KakukaiHeimenZu() As Integer
        Get
            Return intKakukaiHeimenZu
        End Get
        Set(ByVal value As Integer)
            intKakukaiHeimenZu = value
        End Set
    End Property
#End Region

#Region "Šî‘b•š}"
    ''' <summary>
    ''' Šî‘b•š}
    ''' </summary>
    ''' <remarks></remarks>
    Private intKsHuseZu As Integer = Integer.MinValue
    ''' <summary>
    ''' Šî‘b•š}
    ''' </summary>
    ''' <value></value>
    ''' <returns>Šî‘b•š}</returns>
    ''' <remarks></remarks>
    <TableMap("ks_husezu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KsHuseZu() As Integer
        Get
            Return intKsHuseZu
        End Get
        Set(ByVal value As Integer)
            intKsHuseZu = value
        End Set
    End Property
#End Region

#Region "Šî‘b’f–Ê}"
    ''' <summary>
    ''' Šî‘b’f–Ê}
    ''' </summary>
    ''' <remarks></remarks>
    Private intKsDanmenZu As Integer = Integer.MinValue
    ''' <summary>
    ''' Šî‘b’f–Ê}
    ''' </summary>
    ''' <value></value>
    ''' <returns>Šî‘b’f–Ê}</returns>
    ''' <remarks></remarks>
    <TableMap("ks_danmenzu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KsDanmenZu() As Integer
        Get
            Return intKsDanmenZu
        End Get
        Set(ByVal value As Integer)
            intKsDanmenZu = value
        End Set
    End Property
#End Region

#Region "‘¢¬Œv‰æ}"
    ''' <summary>
    ''' ‘¢¬Œv‰æ}
    ''' </summary>
    ''' <remarks></remarks>
    Private intZouseiKeikakuZu As Integer = Integer.MinValue
    ''' <summary>
    ''' ‘¢¬Œv‰æ}
    ''' </summary>
    ''' <value></value>
    ''' <returns>‘¢¬Œv‰æ}</returns>
    ''' <remarks></remarks>
    <TableMap("zousei_keikakuzu", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property ZouseiKeikakuZu() As Integer
        Get
            Return intZouseiKeikakuZu
        End Get
        Set(ByVal value As Integer)
            intZouseiKeikakuZu = value
        End Set
    End Property
#End Region

#Region "Šî‘b’…H—\’è“úFROM"
    ''' <summary>
    ''' Šî‘b’…H—\’è“úFROM
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKsTyakkouYoteiFromDate As DateTime
    ''' <summary>
    ''' Šî‘b’…H—\’è“úFROM
    ''' </summary>
    ''' <value></value>
    ''' <returns> Šî‘b’…H—\’è“úFROM</returns>
    ''' <remarks></remarks>
    <TableMap("ks_tyakkou_yotei_from_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KsTyakkouYoteiFromDate() As DateTime
        Get
            Return dateKsTyakkouYoteiFromDate
        End Get
        Set(ByVal value As DateTime)
            dateKsTyakkouYoteiFromDate = value
        End Set
    End Property
#End Region

#Region "Šî‘b’…H—\’è“úTO"
    ''' <summary>
    ''' Šî‘b’…H—\’è“úTO
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKsTyakkouYoteiToDate As DateTime
    ''' <summary>
    ''' Šî‘b’…H—\’è“úTO
    ''' </summary>
    ''' <value></value>
    ''' <returns> Šî‘b’…H—\’è“úTO</returns>
    ''' <remarks></remarks>
    <TableMap("ks_tyakkou_yotei_to_date", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KsTyakkouYoteiToDate() As DateTime
        Get
            Return dateKsTyakkouYoteiToDate
        End Get
        Set(ByVal value As DateTime)
            dateKsTyakkouYoteiToDate = value
        End Set
    End Property
#End Region

#Region "V‹K“o˜^Œ³‹æ•ª"
    ''' <summary>
    ''' V‹K“o˜^Œ³‹æ•ª
    ''' </summary>
    ''' <remarks></remarks>
    Private intSinkiTourokuMotoKbn As Integer = Integer.MinValue
    ''' <summary>
    ''' V‹K“o˜^Œ³‹æ•ª
    ''' </summary>
    ''' <value></value>
    ''' <returns>V‹K“o˜^Œ³‹æ•ª</returns>
    ''' <remarks></remarks>
    <TableMap("sinki_touroku_moto_kbn", IsKey:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SinkiTourokuMotoKbn() As Integer
        Get
            Return intSinkiTourokuMotoKbn
        End Get
        Set(ByVal value As Integer)
            intSinkiTourokuMotoKbn = value
        End Set
    End Property
#End Region

End Class