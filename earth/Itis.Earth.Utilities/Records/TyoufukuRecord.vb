''' <summary>
''' �����d�����̃��R�[�h�N���X�ł�
''' </summary>
''' <remarks>�����d���f�[�^���R�[�h�̃v���p�e�B</remarks>
Public Class TyoufukuRecord

#Region "�j�����"
    ''' <summary>
    ''' �j�����
    ''' </summary>
    ''' <remarks></remarks>
    Private strHakiSyubetu As String
    ''' <summary>
    ''' �j�����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�j�����</returns>
    ''' <remarks></remarks>
    Public Property HakiSyubetu() As String
        Get
            Return strHakiSyubetu
        End Get
        Set(ByVal value As String)
            strHakiSyubetu = value
        End Set
    End Property
#End Region

#Region "�敪"
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <remarks></remarks>
    Private strKubun As String
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns>�敪</returns>
    ''' <remarks></remarks>
    Public Property Kubun() As String
        Get
            Return strKubun
        End Get
        Set(ByVal value As String)
            strKubun = value
        End Set
    End Property
#End Region

#Region "�ۏ؏�NO"
    ''' <summary>
    ''' �ۏ؏�NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String
    ''' <summary>
    ''' �ۏ؏�NO
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ۏ؏�NO</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "�{�喼"
    ''' <summary>
    ''' �{�喼
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyumei As String
    ''' <summary>
    ''' �{�喼
    ''' </summary>
    ''' <value></value>
    ''' <returns>�{�喼</returns>
    ''' <remarks></remarks>
    Public Property Sesyumei() As String
        Get
            Return strSesyumei
        End Get
        Set(ByVal value As String)
            strSesyumei = value
        End Set
    End Property
#End Region

#Region "�����Z��1"
    ''' <summary>
    ''' �����Z��1
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyuusyo1 As String
    ''' <summary>
    ''' �����Z��1
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����Z��1</returns>
    ''' <remarks></remarks>
    Public Property Jyuusyo1() As String
        Get
            Return strJyuusyo1
        End Get
        Set(ByVal value As String)
            strJyuusyo1 = value
        End Set
    End Property
#End Region

#Region "�����Z��2"
    ''' <summary>
    ''' �����Z��2
    ''' </summary>
    ''' <remarks></remarks>
    Private strJyuusyo2 As String
    ''' <summary>
    ''' �����Z��2
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����Z��2</returns>
    ''' <remarks></remarks>
    Public Property Jyuusyo2() As String
        Get
            Return strJyuusyo2
        End Get
        Set(ByVal value As String)
            strJyuusyo2 = value
        End Set
    End Property
#End Region

#Region "�����X��1"
    ''' <summary>
    ''' �����X��1
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenNm As String
    ''' <summary>
    ''' �����X��1
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����X��1</returns>
    ''' <remarks></remarks>
    Public Property KameitenNm() As String
        Get
            Return strKameitenNm
        End Get
        Set(ByVal value As String)
            strKameitenNm = value
        End Set
    End Property
#End Region

#Region "���l"
    ''' <summary>
    ''' ���l
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou As String
    ''' <summary>
    ''' ���l
    ''' </summary>
    ''' <value></value>
    ''' <returns>���l</returns>
    ''' <remarks></remarks>
    Public Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region


End Class
