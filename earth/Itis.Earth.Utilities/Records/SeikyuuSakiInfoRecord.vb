Public Class SeikyuuSakiInfoRecord

#Region "¿æR[h"
    ''' <summary>
    ''' ¿æR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String
    ''' <summary>
    ''' ¿æR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿æR[h</returns>
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

#Region "¿æ}Ô"
    ''' <summary>
    ''' ¿æ}Ô
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrc As String
    ''' <summary>
    ''' ¿æ}Ô
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿æ}Ô</returns>
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

#Region "¿ææª"
    ''' <summary>
    ''' ¿ææª
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String
    ''' <summary>
    ''' ¿ææª
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿ææª</returns>
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

#Region "¿æ¼"
    ''' <summary>
    ''' ¿æ¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMei As String
    ''' <summary>
    ''' ¿æ¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿æ¼</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_mei")> _
    Public Property SeikyuuSakiMei() As String
        Get
            Return strSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei = value
        End Set
    End Property
#End Region

#Region "¿æJi"
    ''' <summary>
    ''' ¿æJi
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKana As String
    ''' <summary>
    ''' ¿æJi
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿æJi</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kana")> _
    Public Property SeikyuuSakiKana() As String
        Get
            Return strSeikyuuSakiKana
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKana = value
        End Set
    End Property
#End Region

#Region "¿tæZ1"
    ''' <summary>
    ''' ¿tæZ1
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuJyuusyo1 As String
    ''' <summary>
    ''' ¿tæZ1
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿tæZ1</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_jyuusyo1")> _
    Public Property SkysySoufuJyuusyo1() As String
        Get
            Return strSkysySoufuJyuusyo1
        End Get
        Set(ByVal value As String)
            strSkysySoufuJyuusyo1 = value
        End Set
    End Property
#End Region

#Region "¿tæZ2"
    ''' <summary>
    ''' ¿tæZ2
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuJyuusyo2 As String
    ''' <summary>
    ''' ¿tæZ2
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿tæZ2</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_jyuusyo2")> _
    Public Property SkysySoufuJyuusyo2() As String
        Get
            Return strSkysySoufuJyuusyo2
        End Get
        Set(ByVal value As String)
            strSkysySoufuJyuusyo2 = value
        End Set
    End Property
#End Region

#Region "¿tæXÖÔ"
    ''' <summary>
    ''' ¿tæXÖÔ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuYuubinNo As String
    ''' <summary>
    ''' ¿tæXÖÔ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿tæXÖÔ</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_yuubin_no")> _
    Public Property SkysySoufuYuubinNo() As String
        Get
            Return strSkysySoufuYuubinNo
        End Get
        Set(ByVal value As String)
            strSkysySoufuYuubinNo = value
        End Set
    End Property
#End Region

#Region "¿tædbÔ"
    ''' <summary>
    ''' ¿tædbÔ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuTelNo As String
    ''' <summary>
    ''' ¿tædbÔ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿tædbÔ</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_tel_no")> _
    Public Property SkysySoufuTelNo() As String
        Get
            Return strSkysySoufuTelNo
        End Get
        Set(ByVal value As String)
            strSkysySoufuTelNo = value
        End Set
    End Property
#End Region

#Region "¿tæFAXÔ"
    ''' <summary>
    ''' ¿tæFAXÔ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuFaxNo As String
    ''' <summary>
    ''' ¿tæFAXÔ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿tæFAXÔ</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_fax_no")> _
    Public Property SkysySoufuFaxNo() As String
        Get
            Return strSkysySoufuFaxNo
        End Get
        Set(ByVal value As String)
            strSkysySoufuFaxNo = value
        End Set
    End Property
#End Region

End Class