Public Class SiharaiSakiMototyouRecord

#Region "`[j[NNO"
    ''' <summary>
    ''' `[j[NNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intDenpyouUniqueNo As Integer
    ''' <summary>
    ''' `[j[NNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> `[j[NNO</returns>
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

#Region "²¸ïÐR[h"
    ''' <summary>
    ''' ²¸ïÐR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' ²¸ïÐR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸ïÐR[h</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd")> _
    Public Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "²¸ïÐÆR[h"
    ''' <summary>
    ''' ²¸ïÐÆR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaJigyousyoCd As String
    ''' <summary>
    ''' ²¸ïÐÆR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸ïÐÆR[h</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_jigyousyo_cd")> _
    Public Property TysKaisyaJigyousyoCd() As String
        Get
            Return strTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "Nú"
    ''' <summary>
    ''' Nú
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNengappi As DateTime
    ''' <summary>
    ''' Nú
    ''' </summary>
    ''' <value></value>
    ''' <returns> Nú</returns>
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

#Region "ÈÚ"
    ''' <summary>
    ''' ÈÚ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKamoku As String
    ''' <summary>
    ''' ÈÚ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ÈÚ</returns>
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

#Region "¤iR[h"
    ''' <summary>
    ''' ¤iR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' ¤iR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¤iR[h</returns>
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

#Region "i¼"
    ''' <summary>
    ''' i¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strHinmei As String
    ''' <summary>
    ''' i¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> i¼</returns>
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

#Region "¨¼"
    ''' <summary>
    ''' ¨¼
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenMei As String
    ''' <summary>
    ''' ¨¼
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¨¼</returns>
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

#Region "Ê"
    ''' <summary>
    ''' Ê
    ''' </summary>
    ''' <remarks></remarks>
    Private intSuu As Integer = Integer.MinValue
    ''' <summary>
    ''' Ê
    ''' </summary>
    ''' <value></value>
    ''' <returns> Ê</returns>
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

#Region "P¿"
    ''' <summary>
    ''' P¿
    ''' </summary>
    ''' <remarks></remarks>
    Private intTanka As Integer = Integer.MinValue
    ''' <summary>
    ''' P¿
    ''' </summary>
    ''' <value></value>
    ''' <returns> P¿</returns>
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

#Region "Å²àz"
    ''' <summary>
    ''' Å²àz
    ''' </summary>
    ''' <remarks></remarks>
    Private lngZeinukiGaku As Long = Long.MinValue
    ''' <summary>
    ''' Å²àz
    ''' </summary>
    ''' <value></value>
    ''' <returns> Å²àz</returns>
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

#Region "OÅz"
    ''' <summary>
    ''' OÅz
    ''' </summary>
    ''' <remarks></remarks>
    Private intSotozeiGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' OÅz
    ''' </summary>
    ''' <value></value>
    ''' <returns> OÅz</returns>
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

#Region "àz(ÅÝ)"
    ''' <summary>
    ''' àz(ÅÝ)
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKingaku As Long = Long.MinValue
    ''' <summary>
    ''' àz(ÅÝ)
    ''' </summary>
    ''' <value></value>
    ''' <returns> àz(ÅÝ)</returns>
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

#Region "c"
    ''' <summary>
    ''' c
    ''' </summary>
    ''' <remarks></remarks>
    Private lngZandaka As Long = Long.MinValue
    ''' <summary>
    ''' c
    ''' </summary>
    ''' <value></value>
    ''' <returns> c</returns>
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

#Region "`[NO"
    ''' <summary>
    ''' `[NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenpyouNo As String
    ''' <summary>
    ''' `[NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> `[NO</returns>
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