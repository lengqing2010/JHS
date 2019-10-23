Public Class HannyouUriageDataKeyRecord

#Region "取消"
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <value></value>
    ''' <returns> 取消</returns>
    ''' <remarks></remarks>
    Public Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "売上年月日 From"
    ''' <summary>
    ''' 売上年月日 From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriDateFrom As DateTime
    ''' <summary>
    ''' 売上年月日 From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上年月日 From</returns>
    ''' <remarks></remarks>
    Public Property UriDateFrom() As DateTime
        Get
            Return dateUriDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateUriDateFrom = value
        End Set
    End Property
#End Region

#Region "売上年月日 To"
    ''' <summary>
    ''' 売上年月日 To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriDateTo As DateTime
    ''' <summary>
    ''' 売上年月日 To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上年月日 To</returns>
    ''' <remarks></remarks>
    Public Property UriDateTo() As DateTime
        Get
            Return dateUriDateTo
        End Get
        Set(ByVal value As DateTime)
            dateUriDateTo = value
        End Set
    End Property
#End Region

#Region "伝票売上年月日 From"
    ''' <summary>
    ''' 伝票売上年月日 From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDenpyouUriDateFrom As DateTime
    ''' <summary>
    ''' 伝票売上年月日 From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票売上年月日 From</returns>
    ''' <remarks></remarks>
    Public Property DenpyouUriDateFrom() As DateTime
        Get
            Return dateDenpyouUriDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateDenpyouUriDateFrom = value
        End Set
    End Property
#End Region

#Region "伝票売上年月日 To"
    ''' <summary>
    ''' 伝票売上年月日 To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDenpyouUriDateTo As DateTime
    ''' <summary>
    ''' 伝票売上年月日 To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票売上年月日 To</returns>
    ''' <remarks></remarks>
    Public Property DenpyouUriDateTo() As DateTime
        Get
            Return dateDenpyouUriDateTo
        End Get
        Set(ByVal value As DateTime)
            dateDenpyouUriDateTo = value
        End Set
    End Property
#End Region

#Region "請求年月日 From"
    ''' <summary>
    ''' 請求年月日 From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuuDateFrom As DateTime
    ''' <summary>
    ''' 請求年月日 From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求年月日 From</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuDateFrom() As DateTime
        Get
            Return dateSeikyuuDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuuDateFrom = value
        End Set
    End Property
#End Region

#Region "請求年月日 To"
    ''' <summary>
    ''' 請求年月日 To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuuDateTo As DateTime
    ''' <summary>
    ''' 請求年月日 To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求年月日 To</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuDateTo() As DateTime
        Get
            Return dateSeikyuuDateTo
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuuDateTo = value
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

#Region "商品コード"
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品コード</returns>
    ''' <remarks></remarks>
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

#Region "登録日時 From"
    ''' <summary>
    ''' 登録日時 From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetimeFrom As DateTime
    ''' <summary>
    ''' 登録日時 From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時 From</returns>
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

#Region "登録日時 To"
    ''' <summary>
    ''' 登録日時 To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetimeTo As DateTime
    ''' <summary>
    ''' 登録日時 To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時 To</returns>
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

#Region "番号"
    ''' <summary>
    ''' 番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strBangou As String
    ''' <summary>
    ''' 番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 番号</returns>
    ''' <remarks></remarks>
    Public Property Bangou() As String
        Get
            Return strBangou
        End Get
        Set(ByVal value As String)
            strBangou = value
        End Set
    End Property
#End Region

#Region "施主名"
    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String
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

End Class