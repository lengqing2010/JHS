''' <summary>
''' 伝票レコード
''' </summary>
''' <remarks>伝票マスタのレコードを格納するクラスです</remarks>
Public Class DenpyouRecord

#Region "伝票種別"
    ''' <summary>
    ''' 伝票種別
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenpyouType As String
    ''' <summary>
    ''' 伝票種別
    ''' </summary>
    ''' <value></value>
    ''' <returns>伝票種別</returns>
    ''' <remarks></remarks>
    ''' 
    <TableMap("denpyou_syubetu")> _
    Public Property DenpyouType() As String
        Get
            Return strDenpyouType
        End Get
        Set(ByVal value As String)
            strDenpyouType = value
        End Set
    End Property
#End Region

#Region "伝票NO"
    ''' <summary>
    ''' 伝票NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intDenpyouNo As Integer
    ''' <summary>
    ''' 伝票NO
    ''' </summary>
    ''' <value></value>
    ''' <returns>伝票NO</returns>
    ''' <remarks></remarks>
    ''' 
    <TableMap("saisyuu_denpyou_no")> _
    Public Property DenpyouNo() As Integer
        Get
            Return intDenpyouNo
        End Get
        Set(ByVal value As Integer)
            intDenpyouNo = value
        End Set
    End Property
#End Region

#Region "最終データ作成日時"
    ''' <summary>
    ''' 最終データ作成日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dtLastSakuseiDateTime As DateTime
    ''' <summary>
    '''  最終データ作成日時
    ''' </summary>
    ''' <value></value>
    ''' <returns>最終データ作成日時</returns>
    ''' <remarks></remarks>
    ''' 
    <TableMap("saisyuu_sakusei_datetime")> _
    Public Property LastSakuseiDateTime() As DateTime
        Get
            Return dtLastSakuseiDateTime
        End Get
        Set(ByVal value As DateTime)
            dtLastSakuseiDateTime = value
        End Set
    End Property
#End Region

End Class
