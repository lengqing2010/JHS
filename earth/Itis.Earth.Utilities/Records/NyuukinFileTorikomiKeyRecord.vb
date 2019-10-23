''' <summary>
''' 入金ファイル取込データテーブルのレコードクラス
''' 検索条件に必要な情報のみ設定
''' </summary>
''' <remarks></remarks>
Public Class NyuukinFileTorikomiKeyRecord

#Region "取消"
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer = Integer.MinValue
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取消</returns>
    ''' <remarks></remarks>
    Public Property Torikesi() As String
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As String)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "入金取込ユニーク From"
    ''' <summary>
    ''' 入金取込ユニーク From
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuukinTorikomiNoFrom As Integer = Integer.MinValue
    ''' <summary>
    ''' 入金取込ユニーク From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金取込ユニーク From</returns>
    ''' <remarks></remarks>
    Public Property NyuukinTorikomiNoFrom() As Integer
        Get
            Return intNyuukinTorikomiNoFrom
        End Get
        Set(ByVal value As Integer)
            intNyuukinTorikomiNoFrom = value
        End Set
    End Property
#End Region

#Region "入金取込ユニーク To"
    ''' <summary>
    ''' 入金取込ユニーク To
    ''' </summary>
    ''' <remarks></remarks>
    Private intNyuukinTorikomiNoTo As Integer = Integer.MinValue
    ''' <summary>
    ''' 入金取込ユニーク To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 入金取込ユニーク To</returns>
    ''' <remarks></remarks>
    Public Property NyuukinTorikomiNoTo() As Integer
        Get
            Return intNyuukinTorikomiNoTo
        End Get
        Set(ByVal value As Integer)
            intNyuukinTorikomiNoTo = value
        End Set
    End Property
#End Region

#Region "取込伝票番号 From"
    ''' <summary>
    ''' 取込伝票番号 From
    ''' </summary>
    ''' <remarks></remarks>
    Private strTorikomiDenpyouNoFrom As String
    ''' <summary>
    ''' 取込伝票番号 From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取込伝票番号 From</returns>
    ''' <remarks></remarks>
    Public Property TorikomiDenpyouNoFrom() As String
        Get
            Return strTorikomiDenpyouNoFrom
        End Get
        Set(ByVal value As String)
            strTorikomiDenpyouNoFrom = value
        End Set
    End Property
#End Region

#Region "取込伝票番号 To"
    ''' <summary>
    ''' 取込伝票番号 To
    ''' </summary>
    ''' <remarks></remarks>
    Private strTorikomiDenpyouNoTo As String
    ''' <summary>
    ''' 取込伝票番号 To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取込伝票番号 To</returns>
    ''' <remarks></remarks>
    Public Property TorikomiDenpyouNoTo() As String
        Get
            Return strTorikomiDenpyouNoTo
        End Get
        Set(ByVal value As String)
            strTorikomiDenpyouNoTo = value
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

#Region "EDI情報作成日"
    ''' <summary>
    ''' EDI情報作成日
    ''' </summary>
    ''' <remarks></remarks>
    Private strEdiJouhouSakuseiDate As String = String.Empty
    ''' <summary>
    ''' EDI情報作成日
    ''' </summary>
    ''' <value></value>
    ''' <returns>EDI情報作成日</returns>
    ''' <remarks></remarks>
    Public Property EdiJouhouSakuseiDate() As String
        Get
            Return strEdiJouhouSakuseiDate
        End Get
        Set(ByVal value As String)
            strEdiJouhouSakuseiDate = value
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

#Region "請求先名"
    ''' <summary>
    ''' 請求先名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMei As String
    ''' <summary>
    ''' 請求先名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先名</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSakiMei() As String
        Get
            Return strSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei = value
        End Set
    End Property
#End Region


End Class
