''' <summary>
''' ¤iR[h2 ©®Ýèpp[^R[h
''' </summary>
''' <remarks></remarks>
Public Class Syouhin23InfoRecord

#Region "¤iQR[h"
    ''' <summary>
    ''' ¤iQR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private objSyouhin2Rec As Syouhin23Record
    ''' <summary>
    ''' ¤iQR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns>¤iQR[h</returns>
    ''' <remarks></remarks>
    Public Property Syouhin2Rec() As Syouhin23Record
        Get
            Return objSyouhin2Rec
        End Get
        Set(ByVal value As Syouhin23Record)
            objSyouhin2Rec = value
        End Set
    End Property
#End Region
#Region "¿æ"
    ''' <summary>
    ''' ¿æ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusaki As String
    ''' <summary>
    ''' ¿æ
    ''' </summary>
    ''' <value></value>
    ''' <returns>¿æ</returns>
    ''' <remarks></remarks>
    Public Property Seikyuusaki() As String
        Get
            Return strSeikyuusaki
        End Get
        Set(ByVal value As String)
            strSeikyuusaki = value
        End Set
    End Property
#End Region
#Region "¿L³"
    ''' <summary>
    ''' ¿L³
    ''' </summary>
    ''' <remarks></remarks>
    Private intSeikyuuUmu As Integer
    ''' <summary>
    ''' ¿L³
    ''' </summary>
    ''' <value></value>
    ''' <returns> ¿L³</returns>
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
#Region "­mèFLG"
    ''' <summary>
    ''' ­mèFLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intHattyuusyoKakuteiFlg As Integer
    ''' <summary>
    ''' ­mèFLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> ­mèFLG</returns>
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
#Region "nñFLG"
    ''' <summary>
    ''' nñFLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeiretuFlg As Integer
    ''' <summary>
    ''' nñFLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>nñFLG</returns>
    ''' <remarks></remarks>
    Public Property KeiretuFlg() As Integer
        Get
            Return intKeiretuFlg
        End Get
        Set(ByVal value As Integer)
            intKeiretuFlg = value
        End Set
    End Property
#End Region
#Region "nñº°ÄÞ"
    ''' <summary>
    ''' nñº°ÄÞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuCd As String
    ''' <summary>
    ''' nñº°ÄÞ
    ''' </summary>
    ''' <value></value>
    ''' <returns>nñº°ÄÞ</returns>
    ''' <remarks></remarks>
    Public Property KeiretuCd() As String
        Get
            Return strKeiretuCd
        End Get
        Set(ByVal value As String)
            strKeiretuCd = value
        End Set
    End Property
#End Region
End Class
