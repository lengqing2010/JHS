''' <summary>
''' ���m�点�e�[�u���̃��R�[�h�N���X�ł�
''' </summary>
''' <remarks>���m�点�e�[�u���̍��ڃv���p�e�B</remarks>
Public Class OsiraseRecord

#Region "�N����"
    ''' <summary>
    ''' �N����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNengappi As DateTime
    ''' <summary>
    ''' �N����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�N����</returns>
    ''' <remarks></remarks>
    Public Property Nengappi() As DateTime
        Get
            Return dateNengappi
        End Get
        Set(ByVal value As DateTime)
            dateNengappi = value
        End Set
    End Property
#End Region

#Region "���͕���"
    ''' <summary>
    ''' ���͕���
    ''' </summary>
    ''' <remarks></remarks>
    Private strNyuuryokuBusyo As String
    ''' <summary>
    ''' ���͕���
    ''' </summary>
    ''' <value></value>
    ''' <returns>���͕���</returns>
    ''' <remarks></remarks>
    Public Property NyuuryokuBusyo() As String
        Get
            Return strNyuuryokuBusyo
        End Get
        Set(ByVal value As String)
            strNyuuryokuBusyo = value
        End Set
    End Property
#End Region

#Region "���͎�"
    ''' <summary>
    ''' ���͎�
    ''' </summary>
    ''' <remarks></remarks>
    Private strNyuuryokuMei As String
    ''' <summary>
    ''' ���͎�
    ''' </summary>
    ''' <value></value>
    ''' <returns>���͎�</returns>
    ''' <remarks></remarks>
    Public Property NyuuryokuMei() As String
        Get
            Return strNyuuryokuMei
        End Get
        Set(ByVal value As String)
            strNyuuryokuMei = value
        End Set
    End Property
#End Region

#Region "�\�����e"
    ''' <summary>
    ''' �\�����e
    ''' </summary>
    ''' <remarks></remarks>
    Private strHyoujiNaiyou As String
    ''' <summary>
    ''' �\�����e
    ''' </summary>
    ''' <value></value>
    ''' <returns>�\�����e</returns>
    ''' <remarks></remarks>
    Public Property HyoujiNaiyou() As String
        Get
            Return strHyoujiNaiyou
        End Get
        Set(ByVal value As String)
            strHyoujiNaiyou = value
        End Set
    End Property
#End Region

#Region "�����N��"
    ''' <summary>
    ''' �����N��
    ''' </summary>
    ''' <remarks></remarks>
    Private strLinkSaki As String
    ''' <summary>
    ''' �����N��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����N��</returns>
    ''' <remarks></remarks>
    Public Property LinkSaki() As String
        Get
            Return strLinkSaki
        End Get
        Set(ByVal value As String)
            strLinkSaki = value
        End Set
    End Property
#End Region

End Class
