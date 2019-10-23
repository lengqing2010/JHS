''' <summary>
''' FC\C³–¢ó’ƒŒƒR[ƒhƒNƒ‰ƒX/FC\C³‰æ–Ê
''' </summary>
''' <remarks>FC\ƒf[ƒ^‚ÌC³‚Ég—p‚µ‚Ü‚·(–¢ó’—p)</remarks>
<TableClassMap("FcMousikomiIF")> _
Public Class FcMousikomiSyuuseiMijytyRecord
    Inherits FcMousikomiRecord

    'EMABáŠQ‘Î‰î•ñŠi”[ˆ—
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "\NO"
    ''' <summary>
    ''' \NO
    ''' </summary>
    ''' <remarks></remarks>
    Private lngMousikomiNo As Long = Long.MinValue
    ''' <summary>
    ''' \NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> \NO</returns>
    ''' <remarks></remarks>
    <TableMap("mousikomi_no", IsKey:=True, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Overrides Property MousikomiNo() As Long
        Get
            Return lngMousikomiNo
        End Get
        Set(ByVal value As Long)
            lngMousikomiNo = value
        End Set
    End Property
#End Region

#Region "—v’ˆÓî•ñ"
    ''' <summary>
    ''' —v’ˆÓî•ñ
    ''' </summary>
    ''' <remarks></remarks>
    Private strYouTyuuiJouhou As String = String.Empty
    ''' <summary>
    ''' —v’ˆÓî•ñ
    ''' </summary>
    ''' <value></value>
    ''' <returns> —v’ˆÓî•ñ</returns>
    ''' <remarks></remarks>
    <TableMap("you_tyuui_jouhou", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property YouTyuuiJouhou() As String
        Get
            Return strYouTyuuiJouhou
        End Get
        Set(ByVal value As String)
            strYouTyuuiJouhou = value
        End Set
    End Property
#End Region

#Region "’²¸‰ïĞ’S“–Ò"
    ''' <summary>
    ''' ’²¸‰ïĞ’S“–Ò
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCdTantousya As String = String.Empty
    ''' <summary>
    ''' ’²¸‰ïĞ’S“–Ò
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸‰ïĞ’S“–Ò</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd_tantousya", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TysKaisyaTantousya() As String
        Get
            Return strTysKaisyaCdTantousya
        End Get
        Set(ByVal value As String)
            strTysKaisyaCdTantousya = value
        End Set
    End Property
#End Region

#Region "Œ_–ñNO"
    ''' <summary>
    ''' Œ_–ñNO
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiyakuNo As String = String.Empty
    ''' <summary>
    ''' Œ_–ñNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> Œ_–ñNO</returns>
    ''' <remarks></remarks>
    <TableMap("keiyaku_no", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property KeiyakuNo() As String
        Get
            Return strKeiyakuNo
        End Get
        Set(ByVal value As String)
            strKeiyakuNo = value
        End Set
    End Property
#End Region

#Region "’²¸˜A—æ_ˆ¶æ"
    ''' <summary>
    ''' ’²¸˜A—æ_ˆ¶æ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiAtesakiMei As String = String.Empty
    ''' <summary>
    ''' ’²¸˜A—æ_ˆ¶æ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸˜A—æ_ˆ¶æ</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_atesaki_mei", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=60)> _
    Public Overrides Property TysRenrakusakiAtesakiMei() As String
        Get
            Return strTysRenrakusakiAtesakiMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiAtesakiMei = value
        End Set
    End Property
#End Region

#Region "’²¸˜A—æ_’S“–Ò"
    ''' <summary>
    ''' ’²¸˜A—æ_’S“–Ò
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTantouMei As String = String.Empty
    ''' <summary>
    ''' ’²¸˜A—æ_’S“–Ò
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸˜A—æ_’S“–Ò</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_tantou_mei", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TysRenrakusakiTantouMei() As String
        Get
            Return strTysRenrakusakiTantouMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTantouMei = value
        End Set
    End Property
#End Region

#Region "’S“–Ò˜A—æTEL"
    ''' <summary>
    ''' ’S“–Ò˜A—æTEL
    ''' </summary>
    ''' <remarks></remarks>
    Private intTantousyaRenrakusakiTel As Integer = Integer.MinValue
    ''' <summary>
    ''' ’S“–Ò˜A—æTEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’S“–Ò˜A—æTEL</returns>
    ''' <remarks></remarks>
    <TableMap("tantousya_renrakusaki_tel", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TantousyaRenrakusakiTel() As Integer
        Get
            Return intTantousyaRenrakusakiTel
        End Get
        Set(ByVal value As Integer)
            intTantousyaRenrakusakiTel = value
        End Set
    End Property
#End Region

#Region "’²¸˜A—æ_TEL"
    ''' <summary>
    ''' ’²¸˜A—æ_TEL
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTel As String = String.Empty
    ''' <summary>
    ''' ’²¸˜A—æ_TEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸˜A—æ_TEL</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_tel", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
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
    Private strTysRenrakusakiFax As String = String.Empty
    ''' <summary>
    ''' ’²¸˜A—æ_FAX
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸˜A—æ_FAX</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_fax", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
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
    Private strTysRenrakusakiMail As String = String.Empty
    ''' <summary>
    ''' ’²¸˜A—æ_MAIL
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸˜A—æ_MAIL</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_mail", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=64)> _
    Public Overrides Property TysRenrakusakiMail() As String
        Get
            Return strTysRenrakusakiMail
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiMail = value
        End Set
    End Property
#End Region

#Region "{å–¼"
    ''' <summary>
    ''' {å–¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String = String.Empty
    ''' <summary>
    ''' {å–¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> {å–¼</returns>
    ''' <remarks></remarks>
    <TableMap("sesyu_mei", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=50)> _
    Public Overrides Property SesyuMei() As String
        Get
            Return strSesyuMei
        End Get
        Set(ByVal value As String)
            strSesyuMei = value
        End Set
    End Property
#End Region

#Region "•¨ŒZŠ1"
    ''' <summary>
    ''' •¨ŒZŠ1
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo1 As String = String.Empty
    ''' <summary>
    ''' •¨ŒZŠ1
    ''' </summary>
    ''' <value></value>
    ''' <returns> •¨ŒZŠ1</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo1", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
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
    Private strBukkenJyuusyo2 As String = String.Empty
    ''' <summary>
    ''' •¨ŒZŠ2
    ''' </summary>
    ''' <value></value>
    ''' <returns> •¨ŒZŠ2</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo2", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=32)> _
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
    Private strBukkenJyuusyo3 As String = String.Empty
    ''' <summary>
    ''' •¨ŒZŠ3
    ''' </summary>
    ''' <value></value>
    ''' <returns> •¨ŒZŠ3</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo3", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=54)> _
    Public Overrides Property BukkenJyuusyo3() As String
        Get
            Return strBukkenJyuusyo3
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo3 = value
        End Set
    End Property
#End Region

#Region "’²¸Šó–]“ú"
    ''' <summary>
    ''' ’²¸Šó–]“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysKibouDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' ’²¸Šó–]“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸Šó–]“ú</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kibou_date", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysKibouDate() As DateTime
        Get
            Return dateTysKibouDate
        End Get
        Set(ByVal value As DateTime)
            dateTysKibouDate = value
        End Set
    End Property
#End Region

#Region "’²¸Šó–]‹æ•ª"
    ''' <summary>
    ''' ’²¸Šó–]‹æ•ª
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKibouKbn As String = String.Empty
    ''' <summary>
    ''' ’²¸Šó–]‹æ•ª
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸Šó–]‹æ•ª</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kibou_kbn", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=4)> _
    Public Overrides Property TysKibouKbn() As String
        Get
            Return strTysKibouKbn
        End Get
        Set(ByVal value As String)
            strTysKibouKbn = value
        End Set
    End Property
#End Region

#Region "’²¸ŠJnŠó–]ŠÔ"
    ''' <summary>
    ''' ’²¸ŠJnŠó–]ŠÔ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisiKibouJikan As String = String.Empty
    ''' <summary>
    ''' ’²¸ŠJnŠó–]ŠÔ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸ŠJnŠó–]ŠÔ</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisi_kibou_jikan", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Overrides Property TysKaisiKibouJikan() As String
        Get
            Return strTysKaisiKibouJikan
        End Get
        Set(ByVal value As String)
            strTysKaisiKibouJikan = value
        End Set
    End Property
#End Region

#Region "Šî‘b’…H—\’è“úFROM"
    ''' <summary>
    ''' Šî‘b’…H—\’è“úFROM
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKsTyakkouYoteiFromDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' Šî‘b’…H—\’è“úFROM
    ''' </summary>
    ''' <value></value>
    ''' <returns> Šî‘b’…H—\’è“úFROM</returns>
    ''' <remarks></remarks>
    <TableMap("ks_tyakkou_yotei_from_date", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    Private dateKsTyakkouYoteiToDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' Šî‘b’…H—\’è“úTO
    ''' </summary>
    ''' <value></value>
    ''' <returns> Šî‘b’…H—\’è“úTO</returns>
    ''' <remarks></remarks>
    <TableMap("ks_tyakkou_yotei_to_date", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property KsTyakkouYoteiToDate() As DateTime
        Get
            Return dateKsTyakkouYoteiToDate
        End Get
        Set(ByVal value As DateTime)
            dateKsTyakkouYoteiToDate = value
        End Set
    End Property
#End Region

#Region "—§‰ï—L–³"
    ''' <summary>
    ''' —§‰ï—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intTatiaiUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' —§‰ï—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> —§‰ï—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("tatiai_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TatiaiUmu() As Integer
        Get
            Return intTatiaiUmu
        End Get
        Set(ByVal value As Integer)
            intTatiaiUmu = value
        End Set
    End Property
#End Region

#Region "—§‰ïÒƒR[ƒh"
    ''' <summary>
    ''' —§‰ïÒƒR[ƒh
    ''' </summary>
    ''' <remarks></remarks>
    Private intTtsyCd As Integer = Integer.MinValue
    ''' <summary>
    ''' —§‰ïÒƒR[ƒh
    ''' </summary>
    ''' <value></value>
    ''' <returns> —§‰ïÒƒR[ƒh</returns>
    ''' <remarks></remarks>
    <TableMap("tatiaisya_cd", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TtsyCd() As Integer
        Get
            Return intTtsyCd
        End Get
        Set(ByVal value As Integer)
            intTtsyCd = value
        End Set
    End Property
#End Region

#Region "—§‰ïÒ(‚»‚Ì‘¼•â‘«)"
    ''' <summary>
    ''' —§‰ïÒ—§‰ïÒ(‚»‚Ì‘¼•â‘«)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTtsySonotaHosoku As String = String.Empty
    ''' <summary>
    ''' —§‰ïÒ—§‰ïÒ(‚»‚Ì‘¼•â‘«)
    ''' </summary>
    ''' <value></value>
    ''' <returns> —§‰ïÒ(‚»‚Ì‘¼•â‘«)</returns>
    ''' <remarks></remarks>
    <TableMap("tatiaisya_sonota_hosoku", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TtsySonotaHosoku() As String
        Get
            Return strTtsySonotaHosoku
        End Get
        Set(ByVal value As String)
            strTtsySonotaHosoku = value
        End Set
    End Property
#End Region

#Region "\‘¢"
    ''' <summary>
    ''' \‘¢
    ''' </summary>
    ''' <remarks></remarks>
    Private intKouzou As Integer = Integer.MinValue
    ''' <summary>
    ''' \‘¢
    ''' </summary>
    ''' <value></value>
    ''' <returns> \‘¢</returns>
    ''' <remarks></remarks>
    <TableMap("kouzou", IsKey:=False, IsUpdate:=True, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    Private strKouzouMemo As String = String.Empty
    ''' <summary>
    ''' \‘¢MEMO
    ''' </summary>
    ''' <value></value>
    ''' <returns> \‘¢MEMO</returns>
    ''' <remarks></remarks>
    <TableMap("kouzou_memo", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overrides Property KouzouMemo() As String
        Get
            Return strKouzouMemo
        End Get
        Set(ByVal value As String)
            strKouzouMemo = value
        End Set
    End Property
#End Region

#Region "V’zŒš‘Ö"
    ''' <summary>
    ''' V’zŒš‘Ö
    ''' </summary>
    ''' <remarks></remarks>
    Private intSintikuTatekae As Integer = Integer.MinValue
    ''' <summary>
    ''' V’zŒš‘Ö
    ''' </summary>
    ''' <value></value>
    ''' <returns> V’zŒš‘Ö</returns>
    ''' <remarks></remarks>
    <TableMap("sintiku_tatekae", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SintikuTatekae() As Integer
        Get
            Return intSintikuTatekae
        End Get
        Set(ByVal value As Integer)
            intSintikuTatekae = value
        End Set
    End Property
#End Region

#Region "ŠK‘w(’nã)"
    ''' <summary>
    ''' ŠK‘w(’nã)
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisouTijyou As Integer = Integer.MinValue
    ''' <summary>
    ''' ŠK‘w(’nã)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ŠK‘w(’nã)</returns>
    ''' <remarks></remarks>
    <TableMap("kaisou_tijyou", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KaisouTijyou() As Integer
        Get
            Return intKaisouTijyou
        End Get
        Set(ByVal value As Integer)
            intKaisouTijyou = value
        End Set
    End Property
#End Region

#Region "Œš•¨—p“rNO"
    ''' <summary>
    ''' Œš•¨—p“rNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTmytNo As Integer = Integer.MinValue
    ''' <summary>
    ''' Œš•¨—p“rNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> Œš•¨—p“rNO</returns>
    ''' <remarks></remarks>
    <TableMap("tatemono_youto_no", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TmytNo() As Integer
        Get
            Return intTmytNo
        End Get
        Set(ByVal value As Integer)
            intTmytNo = value
        End Set
    End Property
#End Region

#Region "İŒv‹–—ex—Í"
    ''' <summary>
    ''' İŒv‹–—ex—Í
    ''' </summary>
    ''' <remarks></remarks>
    Private decSekkeiKyoyouSijiryoku As Decimal = Decimal.MinValue
    ''' <summary>
    ''' İŒv‹–—ex—Í
    ''' </summary>
    ''' <value></value>
    ''' <returns> İŒv‹–—ex—Í</returns>
    ''' <remarks></remarks>
    <TableMap("sekkei_kyoyou_sijiryoku", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overrides Property SekkeiKyoyouSijiryoku() As Decimal
        Get
            Return decSekkeiKyoyouSijiryoku
        End Get
        Set(ByVal value As Decimal)
            decSekkeiKyoyouSijiryoku = value
        End Set
    End Property
#End Region

#Region "ªØ‚è[‚³"
    ''' <summary>
    ''' ªØ‚è[‚³
    ''' </summary>
    ''' <remarks></remarks>
    Private decNegiriHukasa As Decimal = Decimal.MinValue
    ''' <summary>
    ''' ªØ‚è[‚³
    ''' </summary>
    ''' <value></value>
    ''' <returns> ªØ‚è[‚³</returns>
    ''' <remarks></remarks>
    <TableMap("negiri_hukasa", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
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
    <TableMap("yotei_morituti_atusa", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
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
    Private intYoteiKs As Integer = Integer.MinValue
    ''' <summary>
    ''' —\’èŠî‘b
    ''' </summary>
    ''' <value></value>
    ''' <returns> —\’èŠî‘b</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_ks", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    Private strYoteiKsMemo As String = String.Empty
    ''' <summary>
    ''' —\’èŠî‘bMEMO
    ''' </summary>
    ''' <value></value>
    ''' <returns> —\’èŠî‘bMEMO</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_ks_memo", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Overrides Property YoteiKsMemo() As String
        Get
            Return strYoteiKsMemo
        End Get
        Set(ByVal value As String)
            strYoteiKsMemo = value
        End Set
    End Property
#End Region

#Region "”õl"
    ''' <summary>
    ''' ”õl
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou As String = String.Empty
    ''' <summary>
    ''' ”õl
    ''' </summary>
    ''' <value></value>
    ''' <returns> ”õl</returns>
    ''' <remarks></remarks>
    <TableMap("bikou", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
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
    <TableMap("douji_irai_tousuu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property DoujiIraiTousuu() As Integer
        Get
            Return intDoujiIraiTousuu
        End Get
        Set(ByVal value As Integer)
            intDoujiIraiTousuu = value
        End Set
    End Property
#End Region

#Region "{å–¼—L–³"
    ''' <summary>
    ''' {å–¼—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intSesyuMeiUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' {å–¼—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> {å–¼—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("sesyu_mei_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SesyuMeiUmu() As Integer
        Get
            Return intSesyuMeiUmu
        End Get
        Set(ByVal value As Integer)
            intSesyuMeiUmu = value
        End Set
    End Property
#End Region

#Region "XVÒ"
    ''' <summary>
    ''' XVÒ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinsya As String = String.Empty
    ''' <summary>
    ''' XVÒ
    ''' </summary>
    ''' <value></value>
    ''' <returns> XVÒ</returns>
    ''' <remarks></remarks>
    <TableMap("kousinsya", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property Kousinsya() As String
        Get
            Return strKousinsya
        End Get
        Set(ByVal value As String)
            strKousinsya = value
        End Set
    End Property
#End Region

#Region "“o˜^“ú"
    ''' <summary>
    ''' “o˜^“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetime As DateTime = DateTime.MinValue
    ''' <summary>
    ''' “o˜^“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> “o˜^“ú</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property AddDatetime() As DateTime
        Get
            Return dateAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetime = value
        End Set
    End Property
#End Region

#Region "XV“ú"
    ''' <summary>
    ''' XV“ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime = DateTime.MinValue
    ''' <summary>
    ''' XV“ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> XV“ú</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "XVƒƒOƒCƒ“ƒ†[ƒU[ID"
    ''' <summary>
    ''' XVƒƒOƒCƒ“ƒ†[ƒU[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String = String.Empty
    ''' <summary>
    ''' XVƒƒOƒCƒ“ƒ†[ƒU[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> XVƒƒOƒCƒ“ƒ†[ƒU[ID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

End Class
