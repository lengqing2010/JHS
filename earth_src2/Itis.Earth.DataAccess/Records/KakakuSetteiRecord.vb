Public Class KakakuSetteiRecord

#Region "€iζͺ"
    ''' <summary>
    ''' €iζͺ
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouhinKbn As Integer
    ''' <summary>
    ''' €iζͺ
    ''' </summary>
    ''' <value></value>
    ''' <returns>€iζͺ</returns>
    ''' <remarks>100:€iζͺ1 110,115:€iζͺ2 120:€iζͺ3</remarks>
    Public Property SyouhinKbn() As Integer
        Get
            Return intSyouhinKbn
        End Get
        Set(ByVal value As Integer)
            intSyouhinKbn = value
        End Set
    End Property
#End Region
#Region "²Έϋ@NO"
    ''' <summary>
    ''' ²Έϋ@NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyousaHouhouNo As Integer
    ''' <summary>
    ''' ²Έϋ@NO
    ''' </summary>
    ''' <value></value>
    ''' <returns>²Έϋ@NO</returns>
    ''' <remarks></remarks>
    Public Property TyousaHouhouNo() As Integer
        Get
            Return intTyousaHouhouNo
        End Get
        Set(ByVal value As Integer)
            intTyousaHouhouNo = value
        End Set
    End Property
#End Region
#Region "²ΈTv"
    ''' <summary>
    ''' ²ΈTv
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyousaGaiyou As Integer
    ''' <summary>
    ''' ²ΈTv
    ''' </summary>
    ''' <value></value>
    ''' <returns>²ΈTv</returns>
    ''' <remarks></remarks>
    Public Property TyousaGaiyou() As Integer
        Get
            Return intTyousaGaiyou
        End Get
        Set(ByVal value As Integer)
            intTyousaGaiyou = value
        End Set
    End Property
#End Region
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
#Region "Ώiέθκ"
    ''' <summary>
    ''' Ώiέθκ
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakakuSettei As Integer
    ''' <summary>
    ''' Ώiέθκ
    ''' </summary>
    ''' <value></value>
    ''' <returns>Ώiέθκ</returns>
    ''' <remarks></remarks>
    Public Property KakakuSettei() As Integer
        Get
            Return intKakakuSettei
        End Get
        Set(ByVal value As Integer)
            intKakakuSettei = value
        End Set
    End Property
#End Region

End Class
