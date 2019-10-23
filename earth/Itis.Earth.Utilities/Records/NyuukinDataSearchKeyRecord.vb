''' <summary>
''' 入金データテーブルのレコードクラスです
''' 検索条件に必要な情報のみ設定
''' </summary>
''' <remarks></remarks>
Public Class NyuukinDataSearchKeyRecord

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

#Region "入金日 From"
    ''' <summary>
    ''' 入金日 From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNyuukinDateFrom As DateTime
    ''' <summary>
    ''' 入金日 From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金日 From</returns>
    ''' <remarks></remarks>
    Public Property NyuukinDateFrom() As DateTime
        Get
            Return dateNyuukinDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateNyuukinDateFrom = value
        End Set
    End Property
#End Region

#Region "入金日 To"
    ''' <summary>
    ''' 入金日 To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNyuukinDateTo As DateTime
    ''' <summary>
    ''' 入金日 To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金日 To</returns>
    ''' <remarks></remarks>
    Public Property NyuukinDateTo() As DateTime
        Get
            Return dateNyuukinDateTo
        End Get
        Set(ByVal value As DateTime)
            dateNyuukinDateTo = value
        End Set
    End Property
#End Region

#Region "請求先コード"
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先コード</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSakiCd() As String
        Get
            Return strSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "請求先枝番"
    ''' <summary>
    ''' 請求先枝番
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrc As String
    ''' <summary>
    ''' 請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先枝番</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSakiBrc() As String
        Get
            Return strSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "請求先区分"
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先区分</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSakiKbn() As String
        Get
            Return strSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "請求先名カナ"
    ''' <summary>
    ''' 請求先名カナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMeiKana As String
    ''' <summary>
    ''' 請求先名カナ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先名カナ</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSakiMeiKana() As String
        Get
            Return strSeikyuuSakiMeiKana
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMeiKana = value
        End Set
    End Property
#End Region

#Region "登録日時(伝票作成日) From"
    ''' <summary>
    ''' 登録日時(伝票作成日) From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetimeFrom As DateTime
    ''' <summary>
    ''' 登録日時(伝票作成日) From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時(伝票作成日) From</returns>
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

#Region "登録日時(伝票作成日) To"
    ''' <summary>
    ''' 登録日時(伝票作成日) To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetimeTo As DateTime
    ''' <summary>
    ''' 登録日時(伝票作成日)To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時(伝票作成日) To</returns>
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

#Region "最新伝票表示"
    ''' <summary>
    ''' 最新伝票表示
    ''' </summary>
    ''' <remarks></remarks>
    Private intNewDenpyouDisp As Integer
    ''' <summary>
    ''' 最新伝票表示
    ''' </summary>
    ''' <value></value>
    ''' <returns> 最新伝票表示</returns>
    ''' <remarks></remarks>
    Public Property NewDenpyouDisp() As Integer
        Get
            Return intNewDenpyouDisp
        End Get
        Set(ByVal value As Integer)
            intNewDenpyouDisp = value
        End Set
    End Property
#End Region

End Class