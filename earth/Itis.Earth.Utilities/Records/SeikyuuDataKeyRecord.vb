''' <summary>
''' ¿f[^ÌõKEYR[hNX
''' õðÉKvÈîñÌÝÝè
''' </summary>
''' <remarks></remarks>
Public Class SeikyuuDataKeyRecord

#Region "¿NOQ"
    ''' <summary>
    ''' ¿NOQ
    ''' </summary>
    ''' <remarks></remarks>
    Private strArrSeikyuuSakiCd As String
    ''' <summary>
    ''' ¿NOQ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿NOQ</returns>
    ''' <remarks></remarks>
    Public Property ArrSeikyuuSakiNo() As String
        Get
            Return strArrSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strArrSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "æÁ"
    ''' <summary>
    ''' æÁ
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer = Integer.MinValue
    ''' <summary>
    ''' æÁ
    ''' </summary>
    ''' <value></value>
    ''' <returns> æÁ</returns>
    ''' <remarks></remarks>
    Public Property Torikesi() As String
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As String)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "óÎÛOp"
    ''' <summary>
    ''' óÎÛOp
    ''' </summary>
    ''' <remarks></remarks>
    Private intInjiYousi As Integer = Integer.MinValue
    ''' <summary>
    ''' óÎÛOp
    ''' </summary>
    ''' <value></value>
    ''' <returns> óÎÛOp</returns>
    ''' <remarks></remarks>
    Public Property InjiYousi() As Integer
        Get
            Return intInjiYousi
        End Get
        Set(ByVal value As Integer)
            intInjiYousi = value
        End Set
    End Property
#End Region

#Region "¿­sú From"
    ''' <summary>
    ''' ¿­sú From
    ''' </summary>
    ''' <remarks></remarks>
    Private dtSeikyuusyoHakDateFrom As DateTime
    ''' <summary>
    ''' ¿­sú From
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿­sú From</returns>
    ''' <remarks></remarks>
    Public Property SeikyuusyoHakDateFrom() As DateTime
        Get
            Return dtSeikyuusyoHakDateFrom
        End Get
        Set(ByVal value As DateTime)
            dtSeikyuusyoHakDateFrom = value
        End Set
    End Property
#End Region

#Region "¿­sú To"
    ''' <summary>
    ''' ¿­sú To
    ''' </summary>
    ''' <remarks></remarks>
    Private dtSeikyuusyoHakDateTo As DateTime
    ''' <summary>
    ''' ¿­sú To
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿­sú To</returns>
    ''' <remarks></remarks>
    Public Property SeikyuusyoHakDateTo() As DateTime
        Get
            Return dtSeikyuusyoHakDateTo
        End Get
        Set(ByVal value As DateTime)
            dtSeikyuusyoHakDateTo = value
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
    Public Property SeikyuuSakiKbn() As String
        Get
            Return strSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

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
    Public Property SeikyuuSakiBrc() As String
        Get
            Return strSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "¿æ¼Ji"
    ''' <summary>
    ''' ¿æ¼Ji
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMeiKana As String
    ''' <summary>
    ''' ¿æ¼Ji
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿æ¼Ji</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSakiMeiKana() As String
        Get
            Return strSeikyuuSakiMeiKana
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMeiKana = value
        End Set
    End Property
#End Region

#Region "¿÷ú"
    ''' <summary>
    ''' ¿÷ú
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSimeDate As String
    ''' <summary>
    ''' ¿÷ú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿÷ú</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSimeDate() As String
        Get
            Return strSeikyuuSimeDate
        End Get
        Set(ByVal value As String)
            strSeikyuuSimeDate = value
        End Set
    End Property
#End Region

#Region "¿®"
    ''' <summary>
    ''' ¿®
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSyosiki As String
    ''' <summary>
    ''' ¿®
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿®</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSyosiki() As String
        Get
            Return strSeikyuuSyosiki
        End Get
        Set(ByVal value As String)
            strSeikyuuSyosiki = value
        End Set
    End Property
#End Region

#Region "¾× From"
    ''' <summary>
    ''' ¾× From
    ''' </summary>
    ''' <remarks></remarks>
    Private intMeisaiKensuuFrom As Integer = Integer.MinValue
    ''' <summary>
    ''' ¾× From
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¾× From</returns>
    ''' <remarks></remarks>
    Public Property MeisaiKensuuFrom() As Integer
        Get
            Return intMeisaiKensuuFrom
        End Get
        Set(ByVal value As Integer)
            intMeisaiKensuuFrom = value
        End Set
    End Property
#End Region

#Region "¾× To"
    ''' <summary>
    ''' ¾× To
    ''' </summary>
    ''' <remarks></remarks>
    Private intMeisaiKensuuTo As Integer = Integer.MinValue
    ''' <summary>
    ''' ¾× To
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¾× From</returns>
    ''' <remarks></remarks>
    Public Property MeisaiKensuuTo() As Integer
        Get
            Return intMeisaiKensuuTo
        End Get
        Set(ByVal value As Integer)
            intMeisaiKensuuTo = value
        End Set
    End Property
#End Region

#Region "¿óüú"
    ''' <summary>
    ''' ¿óüú
    ''' </summary>
    ''' <remarks></remarks>
    Private dtSeikyuusyoInsatuDate As DateTime
    ''' <summary>
    ''' ¿óüú
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿óüú</returns>
    ''' <remarks></remarks>
    Public Property SeikyuusyoInsatuDate() As DateTime
        Get
            Return dtSeikyuusyoInsatuDate
        End Get
        Set(ByVal value As DateTime)
            dtSeikyuusyoInsatuDate = value
        End Set
    End Property
#End Region

End Class