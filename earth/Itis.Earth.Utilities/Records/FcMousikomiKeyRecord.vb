''' <summary>
''' FC申込データの検索KEYレコードクラス
''' 検索条件に必要な情報のみ設定
''' </summary>
''' <remarks></remarks>
Public Class FcMousikomiKeyRecord

#Region "申込NO FROM"
    ''' <summary>
    ''' 申込NO FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private lngMousikomiNoFrom As Long = Long.MinValue
    ''' <summary>
    ''' 申込NO FROM
    ''' </summary>
    ''' <value></value>
    ''' <returns> 申込NO FROM</returns>
    ''' <remarks></remarks>
    Public Property MousikomiNoFrom() As Long
        Get
            Return lngMousikomiNoFrom
        End Get
        Set(ByVal value As Long)
            lngMousikomiNoFrom = value
        End Set
    End Property
#End Region

#Region "申込NO TO"
    ''' <summary>
    ''' 申込NO TO
    ''' </summary>
    ''' <remarks></remarks>
    Private lngMousikomiNoTo As Long = Long.MinValue
    ''' <summary>
    ''' 申込NO TO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 申込NO TO</returns>
    ''' <remarks></remarks>
    Public Property MousikomiNoTo() As Long
        Get
            Return lngMousikomiNoTo
        End Get
        Set(ByVal value As Long)
            lngMousikomiNoTo = value
        End Set
    End Property
#End Region

#Region "ステータス"
    ''' <summary>
    ''' ステータス
    ''' </summary>
    ''' <remarks></remarks>
    Private strStatus As String = String.Empty
    ''' <summary>
    ''' ステータス
    ''' </summary>
    ''' <value></value>
    ''' <returns> ステータス</returns>
    ''' <remarks></remarks>
    <TableMap("status")> _
    Public Property Status() As String
        Get
            Return strStatus
        End Get
        Set(ByVal value As String)
            strStatus = value
        End Set
    End Property
#End Region

#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String = String.Empty
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

#Region "保証書NO FROM"
    ''' <summary>
    ''' 保証書NO FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNoFrom As String = String.Empty
    ''' <summary>
    ''' 保証書NO FROM
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO FROM</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNoFrom() As String
        Get
            Return strHosyousyoNoFrom
        End Get
        Set(ByVal value As String)
            strHosyousyoNoFrom = value
        End Set
    End Property
#End Region

#Region "保証書NO TO"
    ''' <summary>
    ''' 保証書NO TO
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNoTo As String = String.Empty
    ''' <summary>
    ''' 保証書NO TO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO TO</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNoTo() As String
        Get
            Return strHosyousyoNoTo
        End Get
        Set(ByVal value As String)
            strHosyousyoNoTo = value
        End Set
    End Property
#End Region

#Region "加盟店コード"
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String = String.Empty
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店コード</returns>
    ''' <remarks></remarks>
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "依頼日FROM"
    ''' <summary>
    ''' 依頼日FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private dateIraiDateFrom As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 依頼日FROM
    ''' </summary>
    ''' <value></value>
    ''' <returns> 依頼日FROM</returns>
    ''' <remarks></remarks>
    Public Property IraiDateFrom() As DateTime
        Get
            Return dateIraiDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateIraiDateFrom = value
        End Set
    End Property
#End Region

#Region "依頼日TO"
    ''' <summary>
    ''' 依頼日TO
    ''' </summary>
    ''' <remarks></remarks>
    Private dateIraiDateTo As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 依頼日TO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 依頼日TO</returns>
    ''' <remarks></remarks>
    Public Property IraiDateTo() As DateTime
        Get
            Return dateIraiDateTo
        End Get
        Set(ByVal value As DateTime)
            dateIraiDateTo = value
        End Set
    End Property
#End Region

#Region "施主名"
    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String = String.Empty
    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 施主名</returns>
    ''' <remarks></remarks>
    Public Property SesyuMei() As String
        Get
            Return strSesyuMei
        End Get
        Set(ByVal value As String)
            strSesyuMei = value
        End Set
    End Property
#End Region

#Region "同時依頼棟数FROM"
    ''' <summary>
    ''' 同時依頼棟数FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private intDoujiIraiTousuuFrom As Integer = Integer.MinValue
    ''' <summary>
    ''' 同時依頼棟数FROM
    ''' </summary>
    ''' <value></value>
    ''' <returns> 同時依頼棟数FROM</returns>
    ''' <remarks></remarks>
    Public Overridable Property DoujiIraiTousuuFrom() As Integer
        Get
            Return intDoujiIraiTousuuFrom
        End Get
        Set(ByVal value As Integer)
            intDoujiIraiTousuuFrom = value
        End Set
    End Property
#End Region

#Region "同時依頼棟数TO"
    ''' <summary>
    ''' 同時依頼棟数TO
    ''' </summary>
    ''' <remarks></remarks>
    Private intDoujiIraiTousuuTo As Integer = Integer.MinValue
    ''' <summary>
    ''' 同時依頼棟数TO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 同時依頼棟数TO</returns>
    ''' <remarks></remarks>
    Public Overridable Property DoujiIraiTousuuTo() As Integer
        Get
            Return intDoujiIraiTousuuTo
        End Get
        Set(ByVal value As Integer)
            intDoujiIraiTousuuTo = value
        End Set
    End Property
#End Region

#Region "登録日時FROM"
    ''' <summary>
    ''' 登録日時FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetimeFrom As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 登録日時FROM
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時FROM</returns>
    ''' <remarks></remarks>
    Public Property AddDatetimeFrom() As DateTime
        Get
            Return dateAddDatetimeFrom
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetimeFrom = value
        End Set
    End Property
#End Region

#Region "登録日時TO"
    ''' <summary>
    ''' 登録日時TO
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetimeTo As DateTime = DateTime.MinValue
    ''' <summary>
    ''' 登録日時TO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時TO</returns>
    ''' <remarks></remarks>
    Public Property AddDatetimeTo() As DateTime
        Get
            Return dateAddDatetimeTo
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetimeTo = value
        End Set
    End Property
#End Region

#Region "調査会社コード＋調査会社事業所コード"
    ''' <summary>
    ''' 調査会社コード＋調査会社事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' 調査会社コード＋調査会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社コード＋調査会社事業所コード</returns>
    ''' <remarks></remarks>
    Public Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "要注意有無"
    ''' <summary>
    ''' 要注意有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intYouTyuuiUmuSearchTaisyou As Integer = Integer.MinValue
    ''' <summary>
    ''' 要注意有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 要注意有無</returns>
    ''' <remarks></remarks>
    Public Property YouTyuuiUmuSearchTaisyou() As String
        Get
            Return intYouTyuuiUmuSearchTaisyou
        End Get
        Set(ByVal value As String)
            intYouTyuuiUmuSearchTaisyou = value
        End Set
    End Property
#End Region

End Class
