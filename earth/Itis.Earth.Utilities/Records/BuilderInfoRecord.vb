Public Class BuilderInfoRecord

#Region "�����X����"
    ''' <summary>
    ''' �����X����
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' �����X����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����X����</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd")> _
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "����NO"
    ''' <summary>
    ''' ����NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuuryokuNo As Integer
    ''' <summary>
    ''' ����NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����NO</returns>
    ''' <remarks></remarks>
    <TableMap("nyuuryoku_no")> _
    Public Property NyuuryokuNo() As Integer
        Get
            Return intNyuuryokuNo
        End Get
        Set(ByVal value As Integer)
            intNyuuryokuNo = value
        End Set
    End Property
#End Region

#Region "���ӎ������"
    ''' <summary>
    ''' ���ӎ������
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyuuijikouSyubetu As String
    ''' <summary>
    ''' ���ӎ������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���ӎ������</returns>
    ''' <remarks></remarks>
    <TableMap("tyuuijikou_syubetu")> _
    Public Property TyuuijikouSyubetu() As String
        Get
            Return strTyuuijikouSyubetu
        End Get
        Set(ByVal value As String)
            strTyuuijikouSyubetu = value
        End Set
    End Property
#End Region

#Region "���͓�"
    ''' <summary>
    ''' ���͓�
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNyuuryokuDate As DateTime
    ''' <summary>
    ''' ���͓�
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���͓�</returns>
    ''' <remarks></remarks>
    <TableMap("nyuuryoku_date")> _
    Public Property NyuuryokuDate() As DateTime
        Get
            Return dateNyuuryokuDate
        End Get
        Set(ByVal value As DateTime)
            dateNyuuryokuDate = value
        End Set
    End Property
#End Region

#Region "��t��"
    ''' <summary>
    ''' ��t��
    ''' </summary>
    ''' <remarks></remarks>
    Private strUketukesyaMei As String
    ''' <summary>
    ''' ��t��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��t��</returns>
    ''' <remarks></remarks>
    <TableMap("uketukesya_mei")> _
    Public Property UketukesyaMei() As String
        Get
            Return strUketukesyaMei
        End Get
        Set(ByVal value As String)
            strUketukesyaMei = value
        End Set
    End Property
#End Region

#Region "���e"
    ''' <summary>
    ''' ���e
    ''' </summary>
    ''' <remarks></remarks>
    Private strNaiyou As String
    ''' <summary>
    ''' ���e
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���e</returns>
    ''' <remarks></remarks>
    <TableMap("naiyou")> _
    Public Property Naiyou() As String
        Get
            Return strNaiyou
        End Get
        Set(ByVal value As String)
            strNaiyou = value
        End Set
    End Property
#End Region

End Class