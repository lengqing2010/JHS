''' <summary>
''' x¥f[^e[uÌR[hNX
''' õðÉKvÈîñÌÝÝè
''' </summary>
''' <remarks></remarks>
Public Class SiharaiDataKeyRecord

#Region "`[j[NNO"
    ''' <summary>
    ''' `[j[NNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intDenUnqNo As Integer
    ''' <summary>
    ''' `[j[NNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> `[j[NNO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_unique_no")> _
    Public Property DenUnqNo() As Integer
        Get
            Return intDenUnqNo
        End Get
        Set(ByVal value As Integer)
            intDenUnqNo = value
        End Set
    End Property
#End Region

#Region "x¥Nú From"
    ''' <summary>
    ''' x¥Nú From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateShriDateFrom As DateTime
    ''' <summary>
    ''' x¥Nú From
    ''' </summary>
    ''' <value></value>
    ''' <returns> x¥Nú From</returns>
    ''' <remarks></remarks>
    Public Property ShriDateFrom() As DateTime
        Get
            Return dateShriDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateShriDateFrom = value
        End Set
    End Property
#End Region

#Region "x¥Nú To"
    ''' <summary>
    ''' x¥Nú To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateShriDateTo As DateTime
    ''' <summary>
    ''' x¥Nú To
    ''' </summary>
    ''' <value></value>
    ''' <returns> x¥Nú To</returns>
    ''' <remarks></remarks>
    Public Property ShriDateTo() As DateTime
        Get
            Return dateShriDateTo
        End Get
        Set(ByVal value As DateTime)
            dateShriDateTo = value
        End Set
    End Property
#End Region

#Region "`[NO From"
    ''' <summary>
    ''' `[NO From
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenNoFrom As String
    ''' <summary>
    ''' `[NO From
    ''' </summary>
    ''' <value></value>
    ''' <returns> `[NO From</returns>
    ''' <remarks></remarks>
    Public Property DenNoFrom() As String
        Get
            Return strDenNoFrom
        End Get
        Set(ByVal value As String)
            strDenNoFrom = value
        End Set
    End Property
#End Region

#Region "`[NO To"
    ''' <summary>
    ''' `[NO To
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenNoTo As String
    ''' <summary>
    ''' `[NO To
    ''' </summary>
    ''' <value></value>
    ''' <returns> `[NO To</returns>
    ''' <remarks></remarks>
    Public Property DenNoTo() As String
        Get
            Return strDenNoTo
        End Get
        Set(ByVal value As String)
            strDenNoTo = value
        End Set
    End Property
#End Region

#Region "²¸ïÐR[h{²¸ïÐÆR[h"
    ''' <summary>
    ''' ²¸ïÐR[h{²¸ïÐÆR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' ²¸ïÐR[h{²¸ïÐÆR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ²¸ïÐR[h{²¸ïÐÆR[h</returns>
    ''' <remarks></remarks>
    Public Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "VïvÆR[h"
    ''' <summary>
    ''' VïvÆR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkkJigyouCd As String
    ''' <summary>
    ''' VïvÆR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> VïvÆR[h</returns>
    ''' <remarks></remarks>
    Public Property SkkJigyouCd() As String
        Get
            Return strSkkJigyouCd
        End Get
        Set(ByVal value As String)
            strSkkJigyouCd = value
        End Set
    End Property
#End Region

#Region "Vïvx¥æR[h"
    ''' <summary>
    ''' Vïvx¥æR[h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkkShriSakiCd As String
    ''' <summary>
    ''' Vïvx¥æR[h
    ''' </summary>
    ''' <value></value>
    ''' <returns> Vïvx¥æR[h</returns>
    ''' <remarks></remarks>
    Public Property SkkShriSakiCd() As String
        Get
            Return strSkkShriSakiCd
        End Get
        Set(ByVal value As String)
            strSkkShriSakiCd = value
        End Set
    End Property
#End Region

#Region "ÅV`[\¦"
    ''' <summary>
    ''' ÅV`[\¦
    ''' </summary>
    ''' <remarks></remarks>
    Private intNewDenDisp As Integer
    ''' <summary>
    ''' ÅV`[\¦
    ''' </summary>
    ''' <value></value>
    ''' <returns> ÅV`[\¦</returns>
    ''' <remarks></remarks>
    Public Property NewDenDisp() As Integer
        Get
            Return intNewDenDisp
        End Get
        Set(ByVal value As Integer)
            intNewDenDisp = value
        End Set
    End Property
#End Region

End Class
