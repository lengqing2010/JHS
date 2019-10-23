''' <summary>
''' 邸別入金テーブル更新用レコード
''' </summary>
''' <remarks></remarks>
Public Class TeibetuNyuukinUpdateRecord

    ''' <summary>
    ''' 邸別入金レコード
    ''' </summary>
    ''' <remarks></remarks>
    Private teibetuNyuukinRec As TeibetuNyuukinRecord
    ''' <summary>
    ''' 邸別入金レコード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TeibetuNyuukinrecord() As TeibetuNyuukinRecord
        Get
            Return teibetuNyuukinRec
        End Get
        Set(ByVal value As TeibetuNyuukinRecord)
            teibetuNyuukinRec = value
        End Set
    End Property

    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strBunruiCd As String
    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BunruiCd() As String
        Get
            Return strBunruiCd
        End Get
        Set(ByVal value As String)
            strBunruiCd = value
        End Set
    End Property

    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intGamenHyoujiNo As Integer
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GamenHyoujiNo() As Integer
        Get
            Return intGamenHyoujiNo
        End Get
        Set(ByVal value As Integer)
            intGamenHyoujiNo = value
        End Set
    End Property


End Class
