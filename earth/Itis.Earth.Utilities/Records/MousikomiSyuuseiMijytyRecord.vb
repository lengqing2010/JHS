''' <summary>
''' \C³–¢ó’ƒŒƒR[ƒhƒNƒ‰ƒX/\C³‰æ–Ê
''' </summary>
''' <remarks>\ƒf[ƒ^‚ÌC³‚Ég—p‚µ‚Ü‚·(–¢ó’—p)</remarks>
<TableClassMap("MousikomiIF")> _
Public Class MousikomiSyuuseiMijytyRecord
    Inherits MousikomiRecord

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

#Region "•¨Œ–¼ÌƒtƒŠƒKƒi"
    ''' <summary>
    ''' •¨Œ–¼ÌƒtƒŠƒKƒi
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenMeiKana As String = String.Empty
    ''' <summary>
    ''' •¨Œ–¼ÌƒtƒŠƒKƒi
    ''' </summary>
    ''' <value></value>
    ''' <returns> •¨Œ–¼ÌƒtƒŠƒKƒi</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_mei_kana", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=100)> _
    Public Overrides Property BukkenMeiKana() As String
        Get
            Return strBukkenMeiKana
        End Get
        Set(ByVal value As String)
            strBukkenMeiKana = value
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

#Region "’²¸êŠ(—X•Ö”Ô†)‚P"
    ''' <summary>
    ''' ’²¸êŠ(—X•Ö”Ô†)‚P
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysBasyoYuubin1 As String = String.Empty
    ''' <summary>
    ''' ’²¸êŠ(—X•Ö”Ô†)‚P
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸êŠ(—X•Ö”Ô†)‚P</returns>
    ''' <remarks></remarks>
    <TableMap("tys_basyo_yuubin_1", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
    Public Overrides Property TysBasyoYuubin1() As String
        Get
            Return strTysBasyoYuubin1
        End Get
        Set(ByVal value As String)
            strTysBasyoYuubin1 = value
        End Set
    End Property
#End Region

#Region "’²¸êŠ(—X•Ö”Ô†)‚Q"
    ''' <summary>
    ''' ’²¸êŠ(—X•Ö”Ô†)‚Q
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysBasyoYuubin2 As String = String.Empty
    ''' <summary>
    ''' ’²¸êŠ(—X•Ö”Ô†)‚Q
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸êŠ(—X•Ö”Ô†)‚Q</returns>
    ''' <remarks></remarks>
    <TableMap("tys_basyo_yuubin_2", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=4)> _
    Public Overrides Property TysBasyoYuubin2() As String
        Get
            Return strTysBasyoYuubin2
        End Get
        Set(ByVal value As String)
            strTysBasyoYuubin2 = value
        End Set
    End Property
#End Region

#Region "’²¸êŠ(“s“¹•{Œ§)"
    ''' <summary>
    ''' ’²¸êŠ(“s“¹•{Œ§)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysBasyoTodoufuken As String = String.Empty
    ''' <summary>
    ''' ’²¸êŠ(“s“¹•{Œ§)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’²¸êŠ(“s“¹•{Œ§)</returns>
    ''' <remarks></remarks>
    <TableMap("tys_basyo_todoufuken", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overrides Property TysBasyoTodoufuken() As String
        Get
            Return strTysBasyoTodoufuken
        End Get
        Set(ByVal value As String)
            strTysBasyoTodoufuken = value
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

#Region "SDSŠó–]"
    ''' <summary>
    ''' SDSŠó–]
    ''' </summary>
    ''' <remarks></remarks>
    Private intSdsKibou As Integer = Integer.MinValue
    ''' <summary>
    ''' SDSŠó–]
    ''' </summary>
    ''' <value></value>
    ''' <returns> SDSŠó–]</returns>
    ''' <remarks></remarks>
    <TableMap("sds_kibou", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SdsKibou() As Integer
        Get
            Return intSdsKibou
        End Get
        Set(ByVal value As Integer)
            intSdsKibou = value
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

#Region "‰„‚×°–ÊÏ"
    ''' <summary>
    ''' ‰„‚×°–ÊÏ
    ''' </summary>
    ''' <remarks></remarks>
    Private intNobeyukaMenseki As Integer = Integer.MinValue
    ''' <summary>
    ''' ‰„‚×°–ÊÏ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‰„‚×°–ÊÏ</returns>
    ''' <remarks></remarks>
    <TableMap("nobeyuka_menseki", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property NobeyukaMenseki() As Integer
        Get
            Return intNobeyukaMenseki
        End Get
        Set(ByVal value As Integer)
            intNobeyukaMenseki = value
        End Set
    End Property
#End Region

#Region "Œš’z–ÊÏ"
    ''' <summary>
    ''' Œš’z–ÊÏ
    ''' </summary>
    ''' <remarks></remarks>
    Private intKentikuMenseki As Integer = Integer.MinValue
    ''' <summary>
    ''' Œš’z–ÊÏ
    ''' </summary>
    ''' <value></value>
    ''' <returns> Œš’z–ÊÏ</returns>
    ''' <remarks></remarks>
    <TableMap("kentiku_menseki", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KentikuMenseki() As Integer
        Get
            Return intKentikuMenseki
        End Get
        Set(ByVal value As Integer)
            intKentikuMenseki = value
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

#Region "ŠK‘w(’n‰º)"
    ''' <summary>
    ''' ŠK‘w(’n‰º)
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisouTika As Integer = Integer.MinValue
    ''' <summary>
    ''' ŠK‘w(’n‰º)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ŠK‘w(’n‰º)</returns>
    ''' <remarks></remarks>
    <TableMap("kaisou_tika", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KaisouTika() As Integer
        Get
            Return intKaisouTika
        End Get
        Set(ByVal value As Integer)
            intKaisouTika = value
        End Set
    End Property
#End Region

#Region "ÔŒÉ(’n‰ºÔŒÉŒv‰æ)"
    ''' <summary>
    ''' ÔŒÉ(’n‰ºÔŒÉŒv‰æ)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyako As Integer = Integer.MinValue
    ''' <summary>
    ''' ÔŒÉ(’n‰ºÔŒÉŒv‰æ)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ÔŒÉ(’n‰ºÔŒÉŒv‰æ)</returns>
    ''' <remarks></remarks>
    <TableMap("syako", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Syako() As Integer
        Get
            Return intSyako
        End Get
        Set(ByVal value As Integer)
            intSyako = value
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

#Region "Œš•¨—p“r(“X•Ü—p“r)"
    ''' <summary>
    ''' Œš•¨—p“r(“X•Ü—p“r)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTmytNoTenpoYouto As String = String.Empty
    ''' <summary>
    ''' Œš•¨—p“r(“X•Ü—p“r)
    ''' </summary>
    ''' <value></value>
    ''' <returns> Œš•¨—p“r(“X•Ü—p“r)</returns>
    ''' <remarks></remarks>
    <TableMap("tmyt_no_tenpo_youto", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TmytNoTenpoYouto() As String
        Get
            Return strTmytNoTenpoYouto
        End Get
        Set(ByVal value As String)
            strTmytNoTenpoYouto = value
        End Set
    End Property
#End Region

#Region "Œš•¨—p“r(‚»‚Ì‘¼—p“r)"
    ''' <summary>
    ''' Œš•¨—p“r(‚»‚Ì‘¼—p“r)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTmytNoSonotaYouto As String = String.Empty
    ''' <summary>
    ''' Œš•¨—p“r(‚»‚Ì‘¼—p“r)
    ''' </summary>
    ''' <value></value>
    ''' <returns> Œš•¨—p“r(‚»‚Ì‘¼—p“r)</returns>
    ''' <remarks></remarks>
    <TableMap("tmyt_no_sonota_youto", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TmytNoSonotaYouto() As String
        Get
            Return strTmytNoSonotaYouto
        End Get
        Set(ByVal value As String)
            strTmytNoSonotaYouto = value
        End Set
    End Property
#End Region

#Region "’nˆæ“Á«"
    ''' <summary>
    ''' ’nˆæ“Á«
    ''' </summary>
    ''' <remarks></remarks>
    Private strTiikiTokusei As String = String.Empty
    ''' <summary>
    ''' ’nˆæ“Á«
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’nˆæ“Á«</returns>
    ''' <remarks></remarks>
    <TableMap("tiiki_tokusei", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TiikiTokusei() As String
        Get
            Return strTiikiTokusei
        End Get
        Set(ByVal value As String)
            strTiikiTokusei = value
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

#Region "•zŠî‘bƒx[ƒXW"
    ''' <summary>
    ''' •zŠî‘bƒx[ƒXW
    ''' </summary>
    ''' <remarks></remarks>
    Private intNunoKsBaseW As Integer = Integer.MinValue
    ''' <summary>
    ''' •zŠî‘bƒx[ƒXW
    ''' </summary>
    ''' <value></value>
    ''' <returns> •zŠî‘bƒx[ƒXW</returns>
    ''' <remarks></remarks>
    <TableMap("nuno_ks_base_w", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property NunoKsBaseW() As Integer
        Get
            Return intNunoKsBaseW
        End Get
        Set(ByVal value As Integer)
            intNunoKsBaseW = value
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

#Region "—\’èŠî‘b—§‚¿ã‚ª‚è‚‚³"
    ''' <summary>
    ''' —\’èŠî‘b—§‚¿ã‚ª‚è‚‚³
    ''' </summary>
    ''' <remarks></remarks>
    Private intYoteiKsTatiagariTakasa As Integer = Integer.MinValue
    ''' <summary>
    ''' —\’èŠî‘b—§‚¿ã‚ª‚è‚‚³
    ''' </summary>
    ''' <value></value>
    ''' <returns> —\’èŠî‘b—§‚¿ã‚ª‚è‚‚³</returns>
    ''' <remarks></remarks>
    <TableMap("yotei_ks_tatiagari_takasa", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YoteiKsTatiagariTakasa() As Integer
        Get
            Return intYoteiKsTatiagariTakasa
        End Get
        Set(ByVal value As Integer)
            intYoteiKsTatiagariTakasa = value
        End Set
    End Property
#End Region

#Region "•~’n“¹˜H•"
    ''' <summary>
    ''' •~’n“¹˜H•
    ''' </summary>
    ''' <remarks></remarks>
    Private decSktDouroHaba As Decimal = Decimal.MinValue
    ''' <summary>
    ''' •~’n“¹˜H•
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n“¹˜H•</returns>
    ''' <remarks></remarks>
    <TableMap("skt_douro_haba", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Decimal, SqlLength:=0)> _
    Public Overrides Property SktDouroHaba() As Decimal
        Get
            Return decSktDouroHaba
        End Get
        Set(ByVal value As Decimal)
            decSktDouroHaba = value
        End Set
    End Property
#End Region

#Region "’Ês•s‰ÂÔ—¼ƒtƒ‰ƒO"
    ''' <summary>
    ''' ’Ês•s‰ÂÔ—¼ƒtƒ‰ƒO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTuukouFukaSyaryouFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' ’Ês•s‰ÂÔ—¼ƒtƒ‰ƒO
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’Ês•s‰ÂÔ—¼ƒtƒ‰ƒO</returns>
    ''' <remarks></remarks>
    <TableMap("tuukou_fuka_syaryou_flg", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TuukouFukaSyaryouFlg() As Integer
        Get
            Return intTuukouFukaSyaryouFlg
        End Get
        Set(ByVal value As Integer)
            intTuukouFukaSyaryouFlg = value
        End Set
    End Property
#End Region

#Region "“¹˜H‹K§—L–³"
    ''' <summary>
    ''' “¹˜H‹K§—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intDouroKiseiUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' “¹˜H‹K§—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> “¹˜H‹K§—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("douro_kisei_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property DouroKiseiUmu() As Integer
        Get
            Return intDouroKiseiUmu
        End Get
        Set(ByVal value As Integer)
            intDouroKiseiUmu = value
        End Set
    End Property
#End Region

#Region "‚‚³áŠQ—L–³"
    ''' <summary>
    ''' ‚‚³áŠQ—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intTakasaSyougaiUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' ‚‚³áŠQ—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‚‚³áŠQ—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("takasa_syougai_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TakasaSyougaiUmu() As Integer
        Get
            Return intTakasaSyougaiUmu
        End Get
        Set(ByVal value As Integer)
            intTakasaSyougaiUmu = value
        End Set
    End Property
#End Region

#Region "“dü—L–³"
    ''' <summary>
    ''' “dü—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intDensenUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' “dü—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> “dü—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("densen_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property DensenUmu() As Integer
        Get
            Return intDensenUmu
        End Get
        Set(ByVal value As Integer)
            intDensenUmu = value
        End Set
    End Property
#End Region

#Region "ƒgƒ“ƒlƒ‹—L–³"
    ''' <summary>
    ''' ƒgƒ“ƒlƒ‹—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intTonneruUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' ƒgƒ“ƒlƒ‹—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> ƒgƒ“ƒlƒ‹—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("tonneru_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TonneruUmu() As Integer
        Get
            Return intTonneruUmu
        End Get
        Set(ByVal value As Integer)
            intTonneruUmu = value
        End Set
    End Property
#End Region

#Region "•~’n“à‚’á·"
    ''' <summary>
    ''' •~’n“à‚’á·
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktnKouteisa As Integer = Integer.MinValue
    ''' <summary>
    ''' •~’n“à‚’á·
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n“à‚’á·</returns>
    ''' <remarks></remarks>
    <TableMap("sktn_kouteisa", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktnKouteisa() As Integer
        Get
            Return intSktnKouteisa
        End Get
        Set(ByVal value As Integer)
            intSktnKouteisa = value
        End Set
    End Property
#End Region

#Region "•~’n“à‚’á·(•â‘«)"
    ''' <summary>
    ''' •~’n“à‚’á·(•â‘«)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSktnKouteisaHosoku As String = String.Empty
    ''' <summary>
    ''' •~’n“à‚’á·(•â‘«)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n“à‚’á·(•â‘«)</returns>
    ''' <remarks></remarks>
    <TableMap("sktn_kouteisa_hosoku", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property SktnKouteisaHosoku() As String
        Get
            Return strSktnKouteisaHosoku
        End Get
        Set(ByVal value As String)
            strSktnKouteisaHosoku = value
        End Set
    End Property
#End Region

#Region "ƒXƒ[ƒv—L–³"
    ''' <summary>
    ''' ƒXƒ[ƒv—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intSlopeUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' ƒXƒ[ƒv—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> ƒXƒ[ƒv—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("slope_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SlopeUmu() As Integer
        Get
            Return intSlopeUmu
        End Get
        Set(ByVal value As Integer)
            intSlopeUmu = value
        End Set
    End Property
#End Region

#Region "ƒXƒ[ƒv(•â‘«)"
    ''' <summary>
    ''' ƒXƒ[ƒv(•â‘«)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSlopeHosoku As String = String.Empty
    ''' <summary>
    ''' ƒXƒ[ƒv(•â‘«)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ƒXƒ[ƒv(•â‘«)</returns>
    ''' <remarks></remarks>
    <TableMap("slope_hosoku", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property SlopeHosoku() As String
        Get
            Return strSlopeHosoku
        End Get
        Set(ByVal value As String)
            strSlopeHosoku = value
        End Set
    End Property
#End Region

#Region "”À“üğŒ(‚»‚Ì‘¼)"
    ''' <summary>
    ''' ”À“üğŒ(‚»‚Ì‘¼)
    ''' </summary>
    ''' <remarks></remarks>
    Private strHannyuuJyknSonota As String = String.Empty
    ''' <summary>
    ''' ”À“üğŒ(‚»‚Ì‘¼)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ”À“üğŒ(‚»‚Ì‘¼)</returns>
    ''' <remarks></remarks>
    <TableMap("hannyuu_jykn_sonota", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property HannyuuJyknSonota() As String
        Get
            Return strHannyuuJyknSonota
        End Get
        Set(ByVal value As String)
            strHannyuuJyknSonota = value
        End Set
    End Property
#End Region

#Region "•~’n‘O—ğ(‘î’n)"
    ''' <summary>
    ''' •~’n‘O—ğ(‘î’n)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiTakuti As Integer = Integer.MinValue
    ''' <summary>
    ''' •~’n‘O—ğ(‘î’n)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n‘O—ğ(‘î’n)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_takuti", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiTakuti() As Integer
        Get
            Return intSktZenrekiTakuti
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiTakuti = value
        End Set
    End Property
#End Region

#Region "•~’n‘O—ğ(“c)"
    ''' <summary>
    ''' •~’n‘O—ğ(“c)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiTa As Integer = Integer.MinValue
    ''' <summary>
    ''' •~’n‘O—ğ(“c)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n‘O—ğ(“c)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_ta", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiTa() As Integer
        Get
            Return intSktZenrekiTa
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiTa = value
        End Set
    End Property
#End Region

#Region "•~’n‘O—ğ(”¨)"
    ''' <summary>
    ''' •~’n‘O—ğ(”¨)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiHatake As Integer = Integer.MinValue
    ''' <summary>
    ''' •~’n‘O—ğ(”¨)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n‘O—ğ(”¨)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_hatake", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiHatake() As Integer
        Get
            Return intSktZenrekiHatake
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiHatake = value
        End Set
    End Property
#End Region

#Region "•~’n‘O—ğ(A÷”¨)"
    ''' <summary>
    ''' •~’n‘O—ğ(A÷”¨)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiSyokuju As Integer = Integer.MinValue
    ''' <summary>
    ''' •~’n‘O—ğ(A÷”¨)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n‘O—ğ(A÷”¨)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_syokuju", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiSyokuju() As Integer
        Get
            Return intSktZenrekiSyokuju
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiSyokuju = value
        End Set
    End Property
#End Region

#Region "•~’n‘O—ğ(G–Ø—Ñ)"
    ''' <summary>
    ''' •~’n‘O—ğ(G–Ø—Ñ)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiZouki As Integer = Integer.MinValue
    ''' <summary>
    ''' •~’n‘O—ğ(G–Ø—Ñ)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n‘O—ğ(G–Ø—Ñ)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_zouki", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiZouki() As Integer
        Get
            Return intSktZenrekiZouki
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiZouki = value
        End Set
    End Property
#End Region

#Region "•~’n‘O—ğ(’“Ôê)"
    ''' <summary>
    ''' •~’n‘O—ğ(’“Ôê)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiTyuusyajyou As Integer = Integer.MinValue
    ''' <summary>
    ''' •~’n‘O—ğ(’“Ôê)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n‘O—ğ(’“Ôê)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_tyuusyajyou", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiTyuusyajyou() As Integer
        Get
            Return intSktZenrekiTyuusyajyou
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiTyuusyajyou = value
        End Set
    End Property
#End Region

#Region "•~’n‘O—ğ(Š±‘ñ’n)"
    ''' <summary>
    ''' •~’n‘O—ğ(Š±‘ñ’n)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiKantakuti As Integer = Integer.MinValue
    ''' <summary>
    ''' •~’n‘O—ğ(Š±‘ñ’n)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n‘O—ğ(Š±‘ñ’n)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_kantakuti", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiKantakuti() As Integer
        Get
            Return intSktZenrekiKantakuti
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiKantakuti = value
        End Set
    End Property
#End Region

#Region "•~’n‘O—ğ(HêÕ)"
    ''' <summary>
    ''' •~’n‘O—ğ(HêÕ)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiKoujyouato As Integer = Integer.MinValue
    ''' <summary>
    ''' •~’n‘O—ğ(HêÕ)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n‘O—ğ(HêÕ)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_koujyouato", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiKoujyouato() As Integer
        Get
            Return intSktZenrekiKoujyouato
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiKoujyouato = value
        End Set
    End Property
#End Region

#Region "•~’n‘O—ğ(‚»‚Ì‘¼)"
    ''' <summary>
    ''' •~’n‘O—ğ(‚»‚Ì‘¼)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktZenrekiSonota As Integer = Integer.MinValue
    ''' <summary>
    ''' •~’n‘O—ğ(‚»‚Ì‘¼)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n‘O—ğ(‚»‚Ì‘¼)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_sonota", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktZenrekiSonota() As Integer
        Get
            Return intSktZenrekiSonota
        End Get
        Set(ByVal value As Integer)
            intSktZenrekiSonota = value
        End Set
    End Property
#End Region

#Region "•~’n‘O—ğ(‚»‚Ì‘¼•â‘«)"
    ''' <summary>
    ''' •~’n‘O—ğ(‚»‚Ì‘¼•â‘«)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSktZenrekiSonotaHosoku As String = String.Empty
    ''' <summary>
    ''' •~’n‘O—ğ(‚»‚Ì‘¼•â‘«)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n‘O—ğ(‚»‚Ì‘¼•â‘«)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_zenreki_sonota_hosoku", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property SktZenrekiSonotaHosoku() As String
        Get
            Return strSktZenrekiSonotaHosoku
        End Get
        Set(ByVal value As String)
            strSktZenrekiSonotaHosoku = value
        End Set
    End Property
#End Region

#Region "‘î’n‘¢¬‹@ŠÖ"
    ''' <summary>
    ''' ‘î’n‘¢¬‹@ŠÖ
    ''' </summary>
    ''' <remarks></remarks>
    Private intTakutiZouseiKikan As Integer = Integer.MinValue
    ''' <summary>
    ''' ‘î’n‘¢¬‹@ŠÖ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‘î’n‘¢¬‹@ŠÖ</returns>
    ''' <remarks></remarks>
    <TableMap("takuti_zousei_kikan", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property TakutiZouseiKikan() As Integer
        Get
            Return intTakutiZouseiKikan
        End Get
        Set(ByVal value As Integer)
            intTakutiZouseiKikan = value
        End Set
    End Property
#End Region

#Region "‘¢¬Œ”"
    ''' <summary>
    ''' ‘¢¬Œ”
    ''' </summary>
    ''' <remarks></remarks>
    Private intZouseiGessuu As Integer = Integer.MinValue
    ''' <summary>
    ''' ‘¢¬Œ”
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‘¢¬Œ”</returns>
    ''' <remarks></remarks>
    <TableMap("zousei_gessuu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property ZouseiGessuu() As Integer
        Get
            Return intZouseiGessuu
        End Get
        Set(ByVal value As Integer)
            intZouseiGessuu = value
        End Set
    End Property
#End Region

#Region "Ø“y·“y‹æ•ª"
    ''' <summary>
    ''' Ø“y·“y‹æ•ª
    ''' </summary>
    ''' <remarks></remarks>
    Private intKiriMoriKbn As Integer = Integer.MinValue
    ''' <summary>
    ''' Ø“y·“y‹æ•ª
    ''' </summary>
    ''' <value></value>
    ''' <returns> Ø“y·“y‹æ•ª</returns>
    ''' <remarks></remarks>
    <TableMap("kiri_mori_kbn", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KiriMoriKbn() As Integer
        Get
            Return intKiriMoriKbn
        End Get
        Set(ByVal value As Integer)
            intKiriMoriKbn = value
        End Set
    End Property
#End Region

#Region "Šù‘¶Œš•¨—L–³"
    ''' <summary>
    ''' Šù‘¶Œš•¨—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intKisonTatemonoUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' Šù‘¶Œš•¨—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> Šù‘¶Œš•¨—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("kison_tatemono_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KisonTatemonoUmu() As Integer
        Get
            Return intKisonTatemonoUmu
        End Get
        Set(ByVal value As Integer)
            intKisonTatemonoUmu = value
        End Set
    End Property
#End Region

#Region "ˆäŒË—L–³"
    ''' <summary>
    ''' ˆäŒË—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intIdoUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' ˆäŒË—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> ˆäŒË—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("ido_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property IdoUmu() As Integer
        Get
            Return intIdoUmu
        End Get
        Set(ByVal value As Integer)
            intIdoUmu = value
        End Set
    End Property
#End Region

#Region "ò‰»‘…Œ»‹µ—L–³"
    ''' <summary>
    ''' ò‰»‘…Œ»‹µ—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intJyoukaGenkyouUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' ò‰»‘…Œ»‹µ—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> ò‰»‘…Œ»‹µ—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("jyouka_genkyou_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property JyoukaGenkyouUmu() As Integer
        Get
            Return intJyoukaGenkyouUmu
        End Get
        Set(ByVal value As Integer)
            intJyoukaGenkyouUmu = value
        End Set
    End Property
#End Region

#Region "ò‰»‘…—\’è—L–³"
    ''' <summary>
    ''' ò‰»‘…—\’è—L–³
    ''' </summary>
    ''' <remarks></remarks>
    Private intJyoukaYoteiUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' ò‰»‘…—\’è—L–³
    ''' </summary>
    ''' <value></value>
    ''' <returns> ò‰»‘…—\’è—L–³</returns>
    ''' <remarks></remarks>
    <TableMap("jyouka_yotei_umu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property JyoukaYoteiUmu() As Integer
        Get
            Return intJyoukaYoteiUmu
        End Get
        Set(ByVal value As Integer)
            intJyoukaYoteiUmu = value
        End Set
    End Property
#End Region

#Region "’n“ê"
    ''' <summary>
    ''' ’n“ê
    ''' </summary>
    ''' <remarks></remarks>
    Private intJinawa As Integer = Integer.MinValue
    ''' <summary>
    ''' ’n“ê
    ''' </summary>
    ''' <value></value>
    ''' <returns> ’n“ê</returns>
    ''' <remarks></remarks>
    <TableMap("jinawa", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property Jinawa() As Integer
        Get
            Return intJinawa
        End Get
        Set(ByVal value As Integer)
            intJinawa = value
        End Set
    End Property
#End Region

#Region "‹«ŠEY"
    ''' <summary>
    ''' ‹«ŠEY
    ''' </summary>
    ''' <remarks></remarks>
    Private intKyoukaiKui As Integer = Integer.MinValue
    ''' <summary>
    ''' ‹«ŠEY
    ''' </summary>
    ''' <value></value>
    ''' <returns> ‹«ŠEY</returns>
    ''' <remarks></remarks>
    <TableMap("kyoukai_kui", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KyoukaiKui() As Integer
        Get
            Return intKyoukaiKui
        End Get
        Set(ByVal value As Integer)
            intKyoukaiKui = value
        End Set
    End Property
#End Region

#Region "•~’n‚ÌŒ»‹µ(X’n)"
    ''' <summary>
    ''' •~’n‚ÌŒ»‹µ(X’n)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktGenkyouSarati As Integer = Integer.MinValue
    ''' <summary>
    ''' •~’n‚ÌŒ»‹µ(X’n)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n‚ÌŒ»‹µ(X’n)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_genkyou_sarati", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktGenkyouSarati() As Integer
        Get
            Return intSktGenkyouSarati
        End Get
        Set(ByVal value As Integer)
            intSktGenkyouSarati = value
        End Set
    End Property
#End Region

#Region "•~’n‚ÌŒ»‹µ(’“Ôê)"
    ''' <summary>
    ''' •~’n‚ÌŒ»‹µ(’“Ôê)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktGenkyouTyuusyajyou As Integer = Integer.MinValue
    ''' <summary>
    ''' •~’n‚ÌŒ»‹µ(’“Ôê)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n‚ÌŒ»‹µ(’“Ôê)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_genkyou_tyuusyajyou", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktGenkyouTyuusyajyou() As Integer
        Get
            Return intSktGenkyouTyuusyajyou
        End Get
        Set(ByVal value As Integer)
            intSktGenkyouTyuusyajyou = value
        End Set
    End Property
#End Region

#Region "•~’n‚ÌŒ»‹µ(”_k’n)"
    ''' <summary>
    ''' •~’n‚ÌŒ»‹µ(”_k’n)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktGenkyouNoukouti As Integer = Integer.MinValue
    ''' <summary>
    ''' •~’n‚ÌŒ»‹µ(”_k’n)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n‚ÌŒ»‹µ(”_k’n)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_genkyou_noukouti", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktGenkyouNoukouti() As Integer
        Get
            Return intSktGenkyouNoukouti
        End Get
        Set(ByVal value As Integer)
            intSktGenkyouNoukouti = value
        End Set
    End Property
#End Region

#Region "•~’n‚ÌŒ»‹µ(‚»‚Ì‘¼)"
    ''' <summary>
    ''' •~’n‚ÌŒ»‹µ(‚»‚Ì‘¼)
    ''' </summary>
    ''' <remarks></remarks>
    Private intSktGenkyouSonota As Integer = Integer.MinValue
    ''' <summary>
    ''' •~’n‚ÌŒ»‹µ(‚»‚Ì‘¼)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n‚ÌŒ»‹µ(‚»‚Ì‘¼)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_genkyou_sonota", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SktGenkyouSonota() As Integer
        Get
            Return intSktGenkyouSonota
        End Get
        Set(ByVal value As Integer)
            intSktGenkyouSonota = value
        End Set
    End Property
#End Region

#Region "•~’n‚ÌŒ»‹µ(‚»‚Ì‘¼•â‘«)"
    ''' <summary>
    ''' •~’n‚ÌŒ»‹µ(‚»‚Ì‘¼•â‘«)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSktGenkyouSonotaHosoku As String = String.Empty
    ''' <summary>
    ''' •~’n‚ÌŒ»‹µ(‚»‚Ì‘¼•â‘«)
    ''' </summary>
    ''' <value></value>
    ''' <returns> •~’n‚ÌŒ»‹µ(‚»‚Ì‘¼•â‘«)</returns>
    ''' <remarks></remarks>
    <TableMap("skt_genkyou_sonota_hosoku", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property SktGenkyouSonotaHosoku() As String
        Get
            Return strSktGenkyouSonotaHosoku
        End Get
        Set(ByVal value As String)
            strSktGenkyouSonotaHosoku = value
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

#Region "·“y‚ÉŠÖ‚µ‚Ä(’²¸‘OÀ{Ï·“yŒú)"
    ''' <summary>
    ''' ·“y‚ÉŠÖ‚µ‚Ä(’²¸‘OÀ{Ï·“yŒú)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysMaeJissiZumiMoritutiAtusa As String = String.Empty
    ''' <summary>
    ''' ·“y‚ÉŠÖ‚µ‚Ä(’²¸‘OÀ{Ï·“yŒú)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ·“y‚ÉŠÖ‚µ‚Ä(’²¸‘OÀ{Ï·“yŒú)</returns>
    ''' <remarks></remarks>
    <TableMap("mrtt_tys_mae_jissi_zumi_morituti_atusa", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TysMaeJissiZumiMoritutiAtusa() As String
        Get
            Return strTysMaeJissiZumiMoritutiAtusa
        End Get
        Set(ByVal value As String)
            strTysMaeJissiZumiMoritutiAtusa = value
        End Set
    End Property
#End Region

#Region "·“y‚ÉŠÖ‚µ‚Ä(’²¸Œã—\’è·“yŒú)"
    ''' <summary>
    ''' ·“y‚ÉŠÖ‚µ‚Ä(’²¸Œã—\’è·“yŒú)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysAtoYoteiMoritutiAtusa As String = String.Empty
    ''' <summary>
    ''' ·“y‚ÉŠÖ‚µ‚Ä(’²¸Œã—\’è·“yŒú)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ·“y‚ÉŠÖ‚µ‚Ä(’²¸Œã—\’è·“yŒú)</returns>
    ''' <remarks></remarks>
    <TableMap("mrtt_tys_ato_yotei_morituti_atusa", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TysAtoYoteiMoritutiAtusa() As String
        Get
            Return strTysAtoYoteiMoritutiAtusa
        End Get
        Set(ByVal value As String)
            strTysAtoYoteiMoritutiAtusa = value
        End Set
    End Property
#End Region

#Region "—i•Ç(ƒvƒŒƒLƒƒƒXƒg)"
    ''' <summary>
    ''' —i•Ç(ƒvƒŒƒLƒƒƒXƒg)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkPreCast As Integer = Integer.MinValue
    ''' <summary>
    ''' —i•Ç(ƒvƒŒƒLƒƒƒXƒg)
    ''' </summary>
    ''' <value></value>
    ''' <returns> —i•Ç(ƒvƒŒƒLƒƒƒXƒg)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_pre_cast", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkPreCast() As Integer
        Get
            Return intYhkPreCast
        End Get
        Set(ByVal value As Integer)
            intYhkPreCast = value
        End Set
    End Property
#End Region

#Region "—i•Ç(Œ»ê‘Å‚¿)"
    ''' <summary>
    ''' —i•Ç(Œ»ê‘Å‚¿)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkGenbaUti As Integer = Integer.MinValue
    ''' <summary>
    ''' —i•Ç(Œ»ê‘Å‚¿)
    ''' </summary>
    ''' <value></value>
    ''' <returns> —i•Ç(Œ»ê‘Å‚¿)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_genba_uti", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkGenbaUti() As Integer
        Get
            Return intYhkGenbaUti
        End Get
        Set(ByVal value As Integer)
            intYhkGenbaUti = value
        End Set
    End Property
#End Region

#Region "—i•Ç(ŠÔ’mƒuƒƒbƒN)"
    ''' <summary>
    ''' —i•Ç(ŠÔ’mƒuƒƒbƒN)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkKantiBlock As Integer = Integer.MinValue
    ''' <summary>
    ''' —i•Ç(ŠÔ’mƒuƒƒbƒN)
    ''' </summary>
    ''' <value></value>
    ''' <returns> —i•Ç(ŠÔ’mƒuƒƒbƒN)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_kanti_block", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkKantiBlock() As Integer
        Get
            Return intYhkKantiBlock
        End Get
        Set(ByVal value As Integer)
            intYhkKantiBlock = value
        End Set
    End Property
#End Region

#Region "—i•Ç(CB)"
    ''' <summary>
    ''' —i•Ç(CB)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkCb As Integer = Integer.MinValue
    ''' <summary>
    ''' —i•Ç(CB)
    ''' </summary>
    ''' <value></value>
    ''' <returns> —i•Ç(CB)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_cb", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkCb() As Integer
        Get
            Return intYhkCb
        End Get
        Set(ByVal value As Integer)
            intYhkCb = value
        End Set
    End Property
#End Region

#Region "—i•Ç(ŠùİÏ‚İ)"
    ''' <summary>
    ''' —i•Ç(ŠùİÏ‚İ)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkKisetuZumi As Integer = Integer.MinValue
    ''' <summary>
    ''' —i•Ç(ŠùİÏ‚İ)
    ''' </summary>
    ''' <value></value>
    ''' <returns> —i•Ç(ŠùİÏ‚İ)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_kisetu_zumi", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkKisetuZumi() As Integer
        Get
            Return intYhkKisetuZumi
        End Get
        Set(ByVal value As Integer)
            intYhkKisetuZumi = value
        End Set
    End Property
#End Region

#Region "—i•Ç(Vİ—\’è)"
    ''' <summary>
    ''' —i•Ç(Vİ—\’è)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkSinsetuYotei As Integer = Integer.MinValue
    ''' <summary>
    ''' —i•Ç(Vİ—\’è)
    ''' </summary>
    ''' <value></value>
    ''' <returns> —i•Ç(Vİ—\’è)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_sinsetu_yotei", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkSinsetuYotei() As Integer
        Get
            Return intYhkSinsetuYotei
        End Get
        Set(ByVal value As Integer)
            intYhkSinsetuYotei = value
        End Set
    End Property
#End Region

#Region "—i•Ç(İ’uŒo‰ß”N”)"
    ''' <summary>
    ''' —i•Ç(İ’uŒo‰ß”N”)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkSettiKeikaNensuu As Integer = Integer.MinValue
    ''' <summary>
    ''' —i•Ç(İ’uŒo‰ß”N”)
    ''' </summary>
    ''' <value></value>
    ''' <returns> —i•Ç(İ’uŒo‰ß”N”)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_setti_keika_nensuu", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkSettiKeikaNensuu() As Integer
        Get
            Return intYhkSettiKeikaNensuu
        End Get
        Set(ByVal value As Integer)
            intYhkSettiKeikaNensuu = value
        End Set
    End Property
#End Region

#Region "—i•Ç(‚‚³)"
    ''' <summary>
    ''' —i•Ç(‚‚³)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkTakasa As Integer = Integer.MinValue
    ''' <summary>
    ''' —i•Ç(‚‚³)
    ''' </summary>
    ''' <value></value>
    ''' <returns> —i•Ç(‚‚³)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_takasa", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkTakasa() As Integer
        Get
            Return intYhkTakasa
        End Get
        Set(ByVal value As Integer)
            intYhkTakasa = value
        End Set
    End Property
#End Region

#Region "—i•Ç(Œv‰æ‚‚³)"
    ''' <summary>
    ''' —i•Ç(Œv‰æ‚‚³)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkKeikakuTakasa As Integer = Integer.MinValue
    ''' <summary>
    ''' —i•Ç(Œv‰æ‚‚³)
    ''' </summary>
    ''' <value></value>
    ''' <returns> —i•Ç(Œv‰æ‚‚³)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_keikaku_takasa", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkKeikakuTakasa() As Integer
        Get
            Return intYhkKeikakuTakasa
        End Get
        Set(ByVal value As Integer)
            intYhkKeikakuTakasa = value
        End Set
    End Property
#End Region

#Region "—i•Ç(–ğŠŠm”F)"
    ''' <summary>
    ''' —i•Ç(–ğŠŠm”F)
    ''' </summary>
    ''' <remarks></remarks>
    Private intYhkYakusyoKakunin As Integer = Integer.MinValue
    ''' <summary>
    ''' —i•Ç(–ğŠŠm”F)
    ''' </summary>
    ''' <value></value>
    ''' <returns> —i•Ç(–ğŠŠm”F)</returns>
    ''' <remarks></remarks>
    <TableMap("yhk_yakusyo_kakunin", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property YhkYakusyoKakunin() As Integer
        Get
            Return intYhkYakusyoKakunin
        End Get
        Set(ByVal value As Integer)
            intYhkYakusyoKakunin = value
        End Set
    End Property
#End Region

#Region "ƒnƒcƒŠ‚Ì—v”Û"
    ''' <summary>
    ''' ƒnƒcƒŠ‚Ì—v”Û
    ''' </summary>
    ''' <remarks></remarks>
    Private intHaturiYouhi As Integer = Integer.MinValue
    ''' <summary>
    ''' ƒnƒcƒŠ‚Ì—v”Û
    ''' </summary>
    ''' <value></value>
    ''' <returns> ƒnƒcƒŠ‚Ì—v”Û</returns>
    ''' <remarks></remarks>
    <TableMap("haturi_youhi", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HaturiYouhi() As Integer
        Get
            Return intHaturiYouhi
        End Get
        Set(ByVal value As Integer)
            intHaturiYouhi = value
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
    <TableMap("annaizu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    <TableMap("haitizu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    <TableMap("kakukai_heimenzu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    <TableMap("ks_husezu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    <TableMap("ks_danmenzu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    <TableMap("zousei_keikakuzu", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property ZouseiKeikakuZu() As Integer
        Get
            Return intZouseiKeikakuZu
        End Get
        Set(ByVal value As Integer)
            intZouseiKeikakuZu = value
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
