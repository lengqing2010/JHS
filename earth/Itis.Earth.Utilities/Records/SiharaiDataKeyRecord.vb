''' <summary>
''' 支払データテーブルのレコードクラス
''' 検索条件に必要な情報のみ設定
''' </summary>
''' <remarks></remarks>
Public Class SiharaiDataKeyRecord

#Region "伝票ユニークNO"
    ''' <summary>
    ''' 伝票ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intDenUnqNo As Integer
    ''' <summary>
    ''' 伝票ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_unique_no")> _
    Public Property DenUnqNo() As Integer
        Get
            Return intDenUnqNo
        End Get
        Set(ByVal value As Integer)
            intDenUnqNo = value
        End Set
    End Property
#End Region

#Region "支払年月日 From"
    ''' <summary>
    ''' 支払年月日 From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateShriDateFrom As DateTime
    ''' <summary>
    ''' 支払年月日 From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 支払年月日 From</returns>
    ''' <remarks></remarks>
    Public Property ShriDateFrom() As DateTime
        Get
            Return dateShriDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateShriDateFrom = value
        End Set
    End Property
#End Region

#Region "支払年月日 To"
    ''' <summary>
    ''' 支払年月日 To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateShriDateTo As DateTime
    ''' <summary>
    ''' 支払年月日 To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 支払年月日 To</returns>
    ''' <remarks></remarks>
    Public Property ShriDateTo() As DateTime
        Get
            Return dateShriDateTo
        End Get
        Set(ByVal value As DateTime)
            dateShriDateTo = value
        End Set
    End Property
#End Region

#Region "伝票NO From"
    ''' <summary>
    ''' 伝票NO From
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenNoFrom As String
    ''' <summary>
    ''' 伝票NO From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票NO From</returns>
    ''' <remarks></remarks>
    Public Property DenNoFrom() As String
        Get
            Return strDenNoFrom
        End Get
        Set(ByVal value As String)
            strDenNoFrom = value
        End Set
    End Property
#End Region

#Region "伝票NO To"
    ''' <summary>
    ''' 伝票NO To
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenNoTo As String
    ''' <summary>
    ''' 伝票NO To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票NO To</returns>
    ''' <remarks></remarks>
    Public Property DenNoTo() As String
        Get
            Return strDenNoTo
        End Get
        Set(ByVal value As String)
            strDenNoTo = value
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

#Region "新会計事業所コード"
    ''' <summary>
    ''' 新会計事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkkJigyouCd As String
    ''' <summary>
    ''' 新会計事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 新会計事業所コード</returns>
    ''' <remarks></remarks>
    Public Property SkkJigyouCd() As String
        Get
            Return strSkkJigyouCd
        End Get
        Set(ByVal value As String)
            strSkkJigyouCd = value
        End Set
    End Property
#End Region

#Region "新会計支払先コード"
    ''' <summary>
    ''' 新会計支払先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkkShriSakiCd As String
    ''' <summary>
    ''' 新会計支払先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 新会計支払先コード</returns>
    ''' <remarks></remarks>
    Public Property SkkShriSakiCd() As String
        Get
            Return strSkkShriSakiCd
        End Get
        Set(ByVal value As String)
            strSkkShriSakiCd = value
        End Set
    End Property
#End Region

#Region "最新伝票表示"
    ''' <summary>
    ''' 最新伝票表示
    ''' </summary>
    ''' <remarks></remarks>
    Private intNewDenDisp As Integer
    ''' <summary>
    ''' 最新伝票表示
    ''' </summary>
    ''' <value></value>
    ''' <returns> 最新伝票表示</returns>
    ''' <remarks></remarks>
    Public Property NewDenDisp() As Integer
        Get
            Return intNewDenDisp
        End Get
        Set(ByVal value As Integer)
            intNewDenDisp = value
        End Set
    End Property
#End Region

End Class
