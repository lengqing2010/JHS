Public Class BuilderInfoRecord

#Region "加盟店ｺｰﾄﾞ"
    ''' <summary>
    ''' 加盟店ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' 加盟店ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店ｺｰﾄﾞ</returns>
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

#Region "入力NO"
    ''' <summary>
    ''' 入力NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuuryokuNo As Integer
    ''' <summary>
    ''' 入力NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入力NO</returns>
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

#Region "注意事項種別"
    ''' <summary>
    ''' 注意事項種別
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyuuijikouSyubetu As String
    ''' <summary>
    ''' 注意事項種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 注意事項種別</returns>
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

#Region "入力日"
    ''' <summary>
    ''' 入力日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNyuuryokuDate As DateTime
    ''' <summary>
    ''' 入力日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入力日</returns>
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

#Region "受付者"
    ''' <summary>
    ''' 受付者
    ''' </summary>
    ''' <remarks></remarks>
    Private strUketukesyaMei As String
    ''' <summary>
    ''' 受付者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 受付者</returns>
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

#Region "内容"
    ''' <summary>
    ''' 内容
    ''' </summary>
    ''' <remarks></remarks>
    Private strNaiyou As String
    ''' <summary>
    ''' 内容
    ''' </summary>
    ''' <value></value>
    ''' <returns> 内容</returns>
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