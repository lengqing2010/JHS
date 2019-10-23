Public Class HosyousyoDbRecord

#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 区分</returns>
    ''' <remarks></remarks>
    <TableMap("kbn")> _
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "年月日From"
    ''' <summary>
    ''' 年月日From
    ''' </summary>
    ''' <remarks></remarks>
    Private strDateFrom As String
    ''' <summary>
    ''' 年月日From
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks>年月日From</remarks>
    <TableMap("date_from")> _
    Public Property DateFrom() As String
        Get
            Return strDateFrom
        End Get
        Set(ByVal value As String)
            strDateFrom = value
        End Set
    End Property
#End Region

#Region "年月日To"
    ''' <summary>
    ''' 年月日To
    ''' </summary>
    ''' <remarks></remarks>
    Private strDateTo As String
    ''' <summary>
    ''' 年月日To
    ''' </summary>
    ''' <value></value>
    ''' <returns>年月日To </returns>
    ''' <remarks></remarks>
    <TableMap("date_to")> _
    Public Property DateTo() As String
        Get
            Return strDateTo
        End Get
        Set(ByVal value As String)
            strDateTo = value
        End Set
    End Property
#End Region

#Region "格納先ファイルパス"
    ''' <summary>
    ''' 格納先ファイルパス
    ''' </summary>
    ''' <remarks></remarks>
    Private strKakunousakiFilePass As String
    ''' <summary>
    ''' 格納先ファイルパス
    ''' </summary>
    ''' <value></value>
    ''' <returns>格納先ファイルパス </returns>
    ''' <remarks></remarks>
    <TableMap("kakunousaki_file_pass")> _
    Public Property KakunousakiFilePass() As String
        Get
            Return strKakunousakiFilePass
        End Get
        Set(ByVal value As String)
            strKakunousakiFilePass = value
        End Set
    End Property
#End Region

#Region "登録ログインユーザーID"
    ''' <summary>
    ''' 登録ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String
    ''' <summary>
    ''' 登録ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id")> _
    Public Property AddLoginUserId() As String
        Get
            Return strAddLoginUserId
        End Get
        Set(ByVal value As String)
            strAddLoginUserId = value
        End Set
    End Property
#End Region

#Region "登録日時"
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetime As DateTime
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime")> _
    Public Property AddDatetime() As DateTime
        Get
            Return dateAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetime = value
        End Set
    End Property
#End Region

#Region "更新ログインユーザーID"
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id")> _
    Public Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

#Region "更新日時"
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime")> _
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

End Class