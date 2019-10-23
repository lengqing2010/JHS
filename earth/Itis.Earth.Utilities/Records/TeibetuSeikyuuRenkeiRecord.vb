''' <summary>
''' 邸別請求連携管理レコード
''' </summary>
''' <remarks></remarks>
Public Class TeibetuSeikyuuRenkeiRecord

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
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "保証書NO"
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO</returns>
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

#Region "画面表示NO"
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intGamenHyoujiNo As Integer
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 画面表示NO</returns>
    ''' <remarks></remarks>
    Public Property GamenHyoujiNo() As Integer
        Get
            Return intGamenHyoujiNo
        End Get
        Set(ByVal value As Integer)
            intGamenHyoujiNo = value
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