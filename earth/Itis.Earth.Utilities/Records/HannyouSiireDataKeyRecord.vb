Public Class HannyouSiireDataKeyRecord

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

#Region "仕入年月日 From"
    ''' <summary>
    ''' 仕入年月日 From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSiireDateFrom As DateTime
    ''' <summary>
    ''' 仕入年月日 From
    ''' </summary>
    ''' <value></value>
    ''' <returns>仕入年月日 From</returns>
    ''' <remarks></remarks>
    Public Property SiireDateFrom() As DateTime
        Get
            Return dateSiireDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateSiireDateFrom = value
        End Set
    End Property
#End Region

#Region "仕入年月日 To"
    ''' <summary>
    ''' 仕入年月日 To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSiireDateTo As DateTime
    ''' <summary>
    ''' 仕入年月日 To
    ''' </summary>
    ''' <value></value>
    ''' <returns>仕入年月日 To</returns>
    ''' <remarks></remarks>
    Public Property SiireDateTo() As DateTime
        Get
            Return dateSiireDateTo
        End Get
        Set(ByVal value As DateTime)
            dateSiireDateTo = value
        End Set
    End Property
#End Region

#Region "伝票仕入年月日 From"
    ''' <summary>
    ''' 伝票仕入年月日 From
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDenpyouSiireDateFrom As DateTime
    ''' <summary>
    ''' 伝票仕入年月日 From
    ''' </summary>
    ''' <value></value>
    ''' <returns>伝票仕入年月日 From</returns>
    ''' <remarks></remarks>
    Public Property DenpyouSiireDateFrom() As DateTime
        Get
            Return dateDenpyouSiireDateFrom
        End Get
        Set(ByVal value As DateTime)
            dateDenpyouSiireDateFrom = value
        End Set
    End Property
#End Region

#Region "伝票仕入年月日 To"
    ''' <summary>
    ''' 伝票仕入年月日 To
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDenpyouSiireDateTo As DateTime
    ''' <summary>
    ''' 伝票仕入年月日 To
    ''' </summary>
    ''' <value></value>
    ''' <returns>伝票仕入年月日 To</returns>
    ''' <remarks></remarks>
    Public Property DenpyouSiireDateTo() As DateTime
        Get
            Return dateDenpyouSiireDateTo
        End Get
        Set(ByVal value As DateTime)
            dateDenpyouSiireDateTo = value
        End Set
    End Property
#End Region

#Region "調査会社コード＋調査会社事業所コード"
    ''' <summary>
    ''' 調査会社コード＋調査会社事業所コード tys_kaisya_cd tys_kaisya_jigyousyo_cd
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

#Region "調査会社名カナ"
    ''' <summary>
    ''' 調査会社名カナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaMeiKana As String
    ''' <summary>
    ''' 調査会社名カナ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社名カナ</returns>
    ''' <remarks></remarks>
    Public Property TysKaisyaMeiKana() As String
        Get
            Return strTysKaisyaMeiKana
        End Get
        Set(ByVal value As String)
            strTysKaisyaMeiKana = value
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