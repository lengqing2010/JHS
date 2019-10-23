Public Class YosinEigyouTantousyaMailTargetRecord

#Region "加盟店コード"
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店コード</returns>
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

#Region "営業担当者"
    ''' <summary>
    ''' 営業担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyouTantousyaMei As String
    ''' <summary>
    ''' 営業担当者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 営業担当者</returns>
    ''' <remarks></remarks>
    <TableMap("eigyou_tantousya_mei")> _
    Public Property EigyouTantousyaMei() As String
        Get
            Return strEigyouTantousyaMei
        End Get
        Set(ByVal value As String)
            strEigyouTantousyaMei = value
        End Set
    End Property
#End Region

#Region "引継ぎ完了日"
    ''' <summary>
    ''' 引継ぎ完了日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHikitugiKanryouDate As DateTime
    ''' <summary>
    ''' 引継ぎ完了日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 引継ぎ完了日</returns>
    ''' <remarks></remarks>
    <TableMap("hikitugi_kanryou_date")> _
    Public Property HikitugiKanryouDate() As DateTime
        Get
            Return dateHikitugiKanryouDate
        End Get
        Set(ByVal value As DateTime)
            dateHikitugiKanryouDate = value
        End Set
    End Property
#End Region

#Region "旧営業担当者"
    ''' <summary>
    ''' 旧営業担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private strKyuuEigyouTantousyaMei As String
    ''' <summary>
    ''' 旧営業担当者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 旧営業担当者</returns>
    ''' <remarks></remarks>
    <TableMap("kyuu_eigyou_tantousya_mei")> _
    Public Property KyuuEigyouTantousyaMei() As String
        Get
            Return strKyuuEigyouTantousyaMei
        End Get
        Set(ByVal value As String)
            strKyuuEigyouTantousyaMei = value
        End Set
    End Property
#End Region

#Region "E-MAIL-ADDRESS"
    ''' <summary>
    ''' E-MAIL-ADDRESS
    ''' </summary>
    ''' <remarks></remarks>
    Private strEmailAddresses As String
    ''' <summary>
    ''' E-MAIL-ADDRESS
    ''' </summary>
    ''' <value></value>
    ''' <returns> E-MAIL-ADDRESS</returns>
    ''' <remarks></remarks>
    <TableMap("EmailAddresses")> _
    Public Property EmailAddresses() As String
        Get
            Return strEmailAddresses
        End Get
        Set(ByVal value As String)
            strEmailAddresses = value
        End Set
    End Property
#End Region

#Region "表示名"
    ''' <summary>
    ''' 表示名
    ''' </summary>
    ''' <remarks></remarks>
    Private strDisplayName As String
    ''' <summary>
    ''' 表示名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 表示名</returns>
    ''' <remarks></remarks>
    <TableMap("DisplayName")> _
    Public Property DisplayName() As String
        Get
            Return strDisplayName
        End Get
        Set(ByVal value As String)
            strDisplayName = value
        End Set
    End Property
#End Region

End Class