''' <summary>
''' 支払伝票照会の検索結果格納用レコード
''' </summary>
''' <remarks></remarks>
Public Class SiharaiSearchResultRecord
    Inherits SiharaiDataRecord

#Region "調査会社コード"
    ''' <summary>
    ''' 調査会社コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' 調査会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社コード</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd")> _
    Public Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "調査会社事業所コード"
    ''' <summary>
    ''' 調査会社事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strJigyousyoCd As String
    ''' <summary>
    ''' 調査会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社事業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("jigyousyo_cd")> _
    Public Property TysJigyousyoCd() As String
        Get
            Return strJigyousyoCd
        End Get
        Set(ByVal value As String)
            strJigyousyoCd = value
        End Set
    End Property
#End Region

End Class
