''' <summary>
''' 店別請求連携管理レコード
''' </summary>
''' <remarks></remarks>
Public Class TenbetuSeikyuuRenkeiRecord

#Region "店コード"
    ''' <summary>
    ''' 店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strMiseCd As String
    ''' <summary>
    ''' 店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>店コード</returns>
    ''' <remarks></remarks>
    Public Property MiseCd() As String
        Get
            Return strMiseCd
        End Get
        Set(ByVal value As String)
            strMiseCd = value
        End Set
    End Property
#End Region

#Region "分類ｺｰﾄﾞ"
    ''' <summary>
    ''' 分類ｺｰﾄﾞ
    ''' </summary>
    ''' <remarks></remarks>
    Private strBunruiCd As String
    ''' <summary>
    ''' 分類ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 分類ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    Public Property BunruiCd() As String
        Get
            Return strBunruiCd
        End Get
        Set(ByVal value As String)
            strBunruiCd = value
        End Set
    End Property
#End Region

#Region "入力日"
    ''' <summary>
    ''' 入力日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNyuuryokuDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 入力日
    ''' </summary>
    ''' <value></value>
    ''' <returns>入力日</returns>
    ''' <remarks></remarks>
    Public Property NyuuryokuDate() As DateTime
        Get
            Return dateNyuuryokuDate
        End Get
        Set(ByVal value As DateTime)
            dateNyuuryokuDate = value
        End Set
    End Property
#End Region

#Region "入力日NO"
    ''' <summary>
    ''' 入力日NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuuryokuDateNo As Integer
    ''' <summary>
    ''' 入力日NO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NyuuryokuDateNo() As Integer
        Get
            Return intNyuuryokuDateNo
        End Get
        Set(ByVal value As Integer)
            intNyuuryokuDateNo = value
        End Set
    End Property
#End Region

#Region "連携指示コード"
    ''' <summary>
    ''' 連携指示コード
    ''' </summary>
    ''' <remarks></remarks>
    Private intRenkeiSijiCd As Integer
    ''' <summary>
    ''' 連携指示コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 連携指示コード</returns>
    ''' <remarks></remarks>
    Public Property RenkeiSijiCd() As Integer
        Get
            Return intRenkeiSijiCd
        End Get
        Set(ByVal value As Integer)
            intRenkeiSijiCd = value
        End Set
    End Property
#End Region

#Region "送信状況コード"
    ''' <summary>
    ''' 送信状況コード
    ''' </summary>
    ''' <remarks></remarks>
    Private intSousinJykyCd As Integer
    ''' <summary>
    ''' 送信状況コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 送信状況コード</returns>
    ''' <remarks></remarks>
    Public Property SousinJykyCd() As Integer
        Get
            Return intSousinJykyCd
        End Get
        Set(ByVal value As Integer)
            intSousinJykyCd = value
        End Set
    End Property
#End Region

#Region "送信完了日時"
    ''' <summary>
    ''' 送信完了日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSousinKanryDatetime As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 送信完了日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 送信完了日時</returns>
    ''' <remarks></remarks>
    Public Property SousinKanryDatetime() As DateTime
        Get
            Return dateSousinKanryDatetime
        End Get
        Set(ByVal value As DateTime)
            dateSousinKanryDatetime = value
        End Set
    End Property
#End Region

#Region "更新ﾛｸﾞｲﾝﾕｰｻﾞｰID"
    ''' <summary>
    ''' 更新ﾛｸﾞｲﾝﾕｰｻﾞｰID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' 更新ﾛｸﾞｲﾝﾕｰｻﾞｰID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新ﾛｸﾞｲﾝﾕｰｻﾞｰID</returns>
    ''' <remarks></remarks>
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
    Private dateUpdDatetime1 As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    Public Property UpdDatetime1() As DateTime
        Get
            Return dateUpdDatetime1
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime1 = value
        End Set
    End Property
#End Region

#Region "更新状況フラグ"
    ''' <summary>
    ''' 更新状況フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private blnIsUpdate As Boolean
    ''' <summary>
    ''' 更新状況フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns>更新状況フラグ</returns>
    ''' <remarks>地盤には存在する場合True</remarks>
    Public Property IsUpdate() As Boolean
        Get
            Return blnIsUpdate
        End Get
        Set(ByVal value As Boolean)
            blnIsUpdate = value
        End Set
    End Property
#End Region

End Class
