''' <summary>
''' €iξρR[hΕ·
''' </summary>
''' <remarks></remarks>
Public Class SyouhinRecord

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
    ''' <returns>€iΊ°Δή</returns>
    ''' <remarks></remarks>
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region
#Region "€iΌ"
    ''' <summary>
    ''' €iΌ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinNm As String
    ''' <summary>
    ''' €iΌ
    ''' </summary>
    ''' <value></value>
    ''' <returns>€iΌ</returns>
    ''' <remarks></remarks>
    Public Property SyouhinNm() As String
        Get
            Return strSyouhinNm
        End Get
        Set(ByVal value As String)
            strSyouhinNm = value
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
    ''' <returns>Εζͺ</returns>
    ''' <remarks></remarks>
    Public Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
        End Set
    End Property
#End Region
#Region "WΏi"
    ''' <summary>
    ''' WΏi
    ''' </summary>
    ''' <remarks></remarks>
    Private decHyoujunKakaku As Decimal
    ''' <summary>
    ''' WΏi
    ''' </summary>
    ''' <value></value>
    ''' <returns>WΏi</returns>
    ''' <remarks></remarks>
    Public Property HyoujunKakaku() As Decimal
        Get
            Return decHyoujunKakaku
        End Get
        Set(ByVal value As Decimal)
            decHyoujunKakaku = value
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
    ''' <returns>Ε¦</returns>
    ''' <remarks></remarks>
    Public Property Zeiritu() As Decimal
        Get
            Return decZeiritu
        End Get
        Set(ByVal value As Decimal)
            decZeiritu = value
        End Set
    End Property
#End Region
#Region "qΙΊ°Δή"
    ''' <summary>
    ''' qΙΊ°Δή
    ''' </summary>
    ''' <remarks></remarks>
    Private strSoukoCd As String
    ''' <summary>
    ''' qΙΊ°Δή
    ''' </summary>
    ''' <value></value>
    ''' <returns>qΙΊ°Δή</returns>
    ''' <remarks></remarks>
    Public Property SoukoCd() As String
        Get
            Return strSoukoCd
        End Get
        Set(ByVal value As String)
            strSoukoCd = value
        End Set
    End Property
#End Region
End Class
