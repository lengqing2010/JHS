''' <summary>
''' ReportIfo^EXVf[^ÝèpÌR[hNXÅ·
''' </summary>
''' <remarks></remarks>
Public Class ReportIfRecord

#Region "ÚqÔ"
    ''' <summary>
    ''' ÚqÔ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKokyakuNo As String
    ''' <summary>
    ''' ÚqÔ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ÚqÔ</returns>
    ''' <remarks></remarks>
    Public Property KokyakuNo() As String
        Get
            Return strKokyakuNo
        End Get
        Set(ByVal value As String)
            strKokyakuNo = value
        End Set
    End Property
#End Region

#Region "ÚqÔ-ÇÔ(ñ)"
    ''' <summary>
    ''' ÚqÔ-ÇÔ(ñ)
    ''' </summary>
    ''' <remarks></remarks>
    Private intKokyakuBrc As Integer
    ''' <summary>
    ''' ÚqÔ-ÇÔ(ñ)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ÚqÔ-ÇÔ(ñ)</returns>
    ''' <remarks></remarks>
    Public Property KokyakuBrc() As Integer
        Get
            Return intKokyakuBrc
        End Get
        Set(ByVal value As Integer)
            intKokyakuBrc = value
        End Set
    End Property
#End Region

#Region "T[rXæª"
    ''' <summary>
    ''' T[rXæª
    ''' </summary>
    ''' <remarks></remarks>
    Private intServiceKbn As Integer
    ''' <summary>
    ''' T[rXæª
    ''' </summary>
    ''' <value></value>
    ''' <returns> T[rXæª</returns>
    ''' <remarks></remarks>
    Public Property ServiceKbn() As Integer
        Get
            Return intServiceKbn
        End Get
        Set(ByVal value As Integer)
            intServiceKbn = value
        End Set
    End Property
#End Region

#Region "Û¯ØÔ"
    ''' <summary>
    ''' Û¯ØÔ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyoukenNo As String
    ''' <summary>
    ''' Û¯ØÔ
    ''' </summary>
    ''' <value></value>
    ''' <returns> Û¯ØÔ</returns>
    ''' <remarks></remarks>
    Public Property SyoukenNo() As String
        Get
            Return strSyoukenNo
        End Get
        Set(ByVal value As String)
            strSyoukenNo = value
        End Set
    End Property
#End Region

#Region "²¸û@"
    ''' <summary>
    ''' ²¸û@
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyousa As String
    ''' <summary>
    ''' ²¸û@
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸û@</returns>
    ''' <remarks></remarks>
    Public Property Tyousa() As String
        Get
            Return strTyousa
        End Get
        Set(ByVal value As String)
            strTyousa = value
        End Set
    End Property
#End Region

#Region "væ¨"
    ''' <summary>
    ''' væ¨
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeikaku As String
    ''' <summary>
    ''' væ¨
    ''' </summary>
    ''' <value></value>
    ''' <returns> væ¨</returns>
    ''' <remarks></remarks>
    Public Property Keikaku() As String
        Get
            Return strKeikaku
        End Get
        Set(ByVal value As String)
            strKeikaku = value
        End Set
    End Property
#End Region

#Region "{å¼"
    ''' <summary>
    ''' {å¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuName As String
    ''' <summary>
    ''' {å¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> {å¼</returns>
    ''' <remarks></remarks>
    Public Property SesyuName() As String
        Get
            Return strSesyuName
        End Get
        Set(ByVal value As String)
            strSesyuName = value
        End Set
    End Property
#End Region

#Region "¨Z1(1sÚ)"
    ''' <summary>
    ''' ¨Z1(1sÚ)
    ''' </summary>
    ''' <remarks></remarks>
    Private strBknAdr1 As String
    ''' <summary>
    ''' ¨Z1(1sÚ)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¨Z1(1sÚ)</returns>
    ''' <remarks></remarks>
    Public Property BknAdr1() As String
        Get
            Return strBknAdr1
        End Get
        Set(ByVal value As String)
            strBknAdr1 = value
        End Set
    End Property
#End Region

#Region "¨Z2(2sÚ)"
    ''' <summary>
    ''' ¨Z2(2sÚ)
    ''' </summary>
    ''' <remarks></remarks>
    Private strBknAdr2 As String
    ''' <summary>
    ''' ¨Z2(2sÚ)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¨Z2(2sÚ)</returns>
    ''' <remarks></remarks>
    Public Property BknAdr2() As String
        Get
            Return strBknAdr2
        End Get
        Set(ByVal value As String)
            strBknAdr2 = value
        End Set
    End Property
#End Region

#Region "²¸ó]ú"
    ''' <summary>
    ''' ²¸ó]ú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateChousaHopeDate As DateTime
    ''' <summary>
    ''' ²¸ó]ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸ó]ú</returns>
    ''' <remarks></remarks>
    Public Property ChousaHopeDate() As DateTime
        Get
            Return dateChousaHopeDate
        End Get
        Set(ByVal value As DateTime)
            dateChousaHopeDate = value
        End Set
    End Property
#End Region

#Region "²¸ó]ú(Ô)"
    ''' <summary>
    ''' ²¸ó]ú(Ô)
    ''' </summary>
    ''' <remarks></remarks>
    Private strChousaHopeTime As String
    ''' <summary>
    ''' ²¸ó]ú(Ô)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸ó]ú(Ô)</returns>
    ''' <remarks></remarks>
    Public Property ChousaHopeTime() As String
        Get
            Return strChousaHopeTime
        End Get
        Set(ByVal value As String)
            strChousaHopeTime = value
        End Set
    End Property
#End Region

#Region "²¸ËSÒ"
    ''' <summary>
    ''' ²¸ËSÒ
    ''' </summary>
    ''' <remarks></remarks>
    Private strChousaTanto As String
    ''' <summary>
    ''' ²¸ËSÒ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸ËSÒ</returns>
    ''' <remarks></remarks>
    Public Property ChousaTanto() As String
        Get
            Return strChousaTanto
        End Get
        Set(ByVal value As String)
            strChousaTanto = value
        End Set
    End Property
#End Region

#Region "²¸§ïÒ"
    ''' <summary>
    ''' ²¸§ïÒ
    ''' </summary>
    ''' <remarks></remarks>
    Private strChousaTachiai As String
    ''' <summary>
    ''' ²¸§ïÒ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸§ïÒ</returns>
    ''' <remarks></remarks>
    Public Property ChousaTachiai() As String
        Get
            Return strChousaTachiai
        End Get
        Set(ByVal value As String)
            strChousaTachiai = value
        End Set
    End Property
#End Region

#Region "Á¿XR[h"
    ''' <summary>
    ''' Á¿XR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiCd As String
    ''' <summary>
    ''' Á¿XR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> Á¿XR[h</returns>
    ''' <remarks></remarks>
    Public Property KameiCd() As String
        Get
            Return strKameiCd
        End Get
        Set(ByVal value As String)
            strKameiCd = value
        End Set
    End Property
#End Region

#Region "Á¿X¼"
    ''' <summary>
    ''' Á¿X¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiName As String
    ''' <summary>
    ''' Á¿X¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> Á¿X¼</returns>
    ''' <remarks></remarks>
    Public Property KameiName() As String
        Get
            Return strKameiName
        End Get
        Set(ByVal value As String)
            strKameiName = value
        End Set
    End Property
#End Region

#Region "Á¿XTEL"
    ''' <summary>
    ''' Á¿XTEL
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiTel As String
    ''' <summary>
    ''' Á¿XTEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> Á¿XTEL</returns>
    ''' <remarks></remarks>
    Public Property KameiTel() As String
        Get
            Return strKameiTel
        End Get
        Set(ByVal value As String)
            strKameiTel = value
        End Set
    End Property
#End Region

#Region "Á¿XFAX"
    ''' <summary>
    ''' Á¿XFAX
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiFax As String
    ''' <summary>
    ''' Á¿XFAX
    ''' </summary>
    ''' <value></value>
    ''' <returns> Á¿XFAX</returns>
    ''' <remarks></remarks>
    Public Property KameiFax() As String
        Get
            Return strKameiFax
        End Get
        Set(ByVal value As String)
            strKameiFax = value
        End Set
    End Property
#End Region

#Region "Á¿XMAIL"
    ''' <summary>
    ''' Á¿XMAIL
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiMail As String
    ''' <summary>
    ''' Á¿XMAIL
    ''' </summary>
    ''' <value></value>
    ''' <returns> Á¿XMAIL</returns>
    ''' <remarks></remarks>
    Public Property KameiMail() As String
        Get
            Return strKameiMail
        End Get
        Set(ByVal value As String)
            strKameiMail = value
        End Set
    End Property
#End Region

#Region "²¸ïÐR[h"
    ''' <summary>
    ''' ²¸ïÐR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyousaCd As String
    ''' <summary>
    ''' ²¸ïÐR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸ïÐR[h</returns>
    ''' <remarks></remarks>
    Public Property TyousaCd() As String
        Get
            Return strTyousaCd
        End Get
        Set(ByVal value As String)
            strTyousaCd = value
        End Set
    End Property
#End Region

#Region "²¸ïÐÆR[h"
    ''' <summary>
    ''' ²¸ïÐÆR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyousaBrc As String
    ''' <summary>
    ''' ²¸ïÐÆR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸ïÐÆR[h</returns>
    ''' <remarks></remarks>
    Public Property TyousaBrc() As String
        Get
            Return strTyousaBrc
        End Get
        Set(ByVal value As String)
            strTyousaBrc = value
        End Set
    End Property
#End Region

#Region "²¸ïÐ¼Ì"
    ''' <summary>
    ''' ²¸ïÐ¼Ì
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyousaName As String
    ''' <summary>
    ''' ²¸ïÐ¼Ì
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸ïÐ¼Ì</returns>
    ''' <remarks></remarks>
    Public Property TyousaName() As String
        Get
            Return strTyousaName
        End Get
        Set(ByVal value As String)
            strTyousaName = value
        End Set
    End Property
#End Region

#Region "|[gSSüÍ§äÌR[hXV^CX^v"
    ''' <summary>
    ''' |[gSSüÍ§äÌR[hXV^CX^v
    ''' </summary>
    ''' <remarks></remarks>
    Private dateReportUpdateTime As DateTime
    ''' <summary>
    ''' |[gSSüÍ§äÌR[hXV^CX^v
    ''' </summary>
    ''' <value></value>
    ''' <returns> |[gSSüÍ§äÌR[hXV^CX^v</returns>
    ''' <remarks></remarks>
    Public Property ReportUpdateTime() As DateTime
        Get
            ' ¢ÝèÍVXeútðÔp
            If dateReportUpdateTime = DateTime.MinValue Then
                dateReportUpdateTime = DateTime.Now
            End If

            Return dateReportUpdateTime

        End Get
        Set(ByVal value As DateTime)
            dateReportUpdateTime = value
        End Set
    End Property
#End Region

#Region "i»f[^MXe[^X"
    ''' <summary>
    ''' i»f[^MXe[^X
    ''' </summary>
    ''' <remarks></remarks>
    Private strSendSts As String = "00"
    ''' <summary>
    ''' i»f[^MXe[^X
    ''' </summary>
    ''' <value></value>
    ''' <returns> i»f[^MXe[^X</returns>
    ''' <remarks></remarks>
    Public Property SendSts() As String
        Get
            Return strSendSts
        End Get
        Set(ByVal value As String)
            strSendSts = value
        End Set
    End Property
#End Region

#Region "i»f[^óMXe[^X"
    ''' <summary>
    ''' i»f[^óMXe[^X
    ''' </summary>
    ''' <remarks></remarks>
    Private strRecvSts As String = "00"
    ''' <summary>
    ''' i»f[^óMXe[^X
    ''' </summary>
    ''' <value></value>
    ''' <returns> i»f[^óMXe[^X</returns>
    ''' <remarks></remarks>
    Public Property RecvSts() As String
        Get
            Return strRecvSts
        End Get
        Set(ByVal value As String)
            strRecvSts = value
        End Set
    End Property
#End Region

#Region "PDFóMXe[^X"
    ''' <summary>
    ''' PDFóMXe[^X
    ''' </summary>
    ''' <remarks></remarks>
    Private strPdfSts As String = "00"
    ''' <summary>
    ''' PDFóMXe[^X
    ''' </summary>
    ''' <value></value>
    ''' <returns> PDFóMXe[^X</returns>
    ''' <remarks></remarks>
    Public Property PdfSts() As String
        Get
            Return strPdfSts
        End Get
        Set(ByVal value As String)
            strPdfSts = value
        End Set
    End Property
#End Region

#Region "²¸Aæ_¶æ"
    ''' <summary>
    ''' ²¸Aæ_¶æ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiAtesakiMei As String
    ''' <summary>
    ''' ²¸Aæ_¶æ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸Aæ_¶æ</returns>
    ''' <remarks></remarks>
    Public Property TysRenrakusakiAtesakiMei() As String
        Get
            Return strTysRenrakusakiAtesakiMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiAtesakiMei = value
        End Set
    End Property
#End Region

#Region "²¸Aæ_TEL"
    ''' <summary>
    ''' ²¸Aæ_TEL
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTel As String
    ''' <summary>
    ''' ²¸Aæ_TEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸Aæ_TEL</returns>
    ''' <remarks></remarks>
    Public Property TysRenrakusakiTel() As String
        Get
            Return strTysRenrakusakiTel
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTel = value
        End Set
    End Property
#End Region

#Region "²¸Aæ_FAX"
    ''' <summary>
    ''' ²¸Aæ_FAX
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiFax As String
    ''' <summary>
    ''' ²¸Aæ_FAX
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸Aæ_FAX</returns>
    ''' <remarks></remarks>
    Public Property TysRenrakusakiFax() As String
        Get
            Return strTysRenrakusakiFax
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiFax = value
        End Set
    End Property
#End Region

#Region "²¸Aæ_MAIL"
    ''' <summary>
    ''' ²¸Aæ_MAIL
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiMail As String
    ''' <summary>
    ''' ²¸Aæ_MAIL
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸Aæ_MAIL</returns>
    ''' <remarks></remarks>
    Public Property TysRenrakusakiMail() As String
        Get
            Return strTysRenrakusakiMail
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiMail = value
        End Set
    End Property
#End Region

#Region "²¸Aæ_SÒ"
    ''' <summary>
    ''' ²¸Aæ_SÒ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTantouMei As String
    ''' <summary>
    ''' ²¸Aæ_SÒ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸Aæ_SÒ</returns>
    ''' <remarks></remarks>
    Public Property TysRenrakusakiTantouMei() As String
        Get
            Return strTysRenrakusakiTantouMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTantouMei = value
        End Set
    End Property
#End Region

End Class