Public Class KakakuSetteiRecord

#Region "���i�敪"
    ''' <summary>
    ''' ���i�敪
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouhinKbn As Integer
    ''' <summary>
    ''' ���i�敪
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i�敪</returns>
    ''' <remarks>100:���i�敪1 110,115:���i�敪2 120:���i�敪3</remarks>
    Public Property SyouhinKbn() As Integer
        Get
            Return intSyouhinKbn
        End Get
        Set(ByVal value As Integer)
            intSyouhinKbn = value
        End Set
    End Property
#End Region
#Region "�������@NO"
    ''' <summary>
    ''' �������@NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyousaHouhouNo As Integer
    ''' <summary>
    ''' �������@NO
    ''' </summary>
    ''' <value></value>
    ''' <returns>�������@NO</returns>
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
#Region "�����T�v"
    ''' <summary>
    ''' �����T�v
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyousaGaiyou As Integer
    ''' <summary>
    ''' �����T�v
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����T�v</returns>
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
#Region "���i����"
    ''' <summary>
    ''' ���i����
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' ���i����
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i����</returns>
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
#Region "���i�ݒ�ꏊ"
    ''' <summary>
    ''' ���i�ݒ�ꏊ
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakakuSettei As Integer
    ''' <summary>
    ''' ���i�ݒ�ꏊ
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i�ݒ�ꏊ</returns>
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
