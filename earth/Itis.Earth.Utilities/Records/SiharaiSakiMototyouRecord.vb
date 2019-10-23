Public Class SiharaiSakiMototyouRecord

#Region "伝票ユニークNO"
    ''' <summary>
    ''' 伝票ユニークNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intDenpyouUniqueNo As Integer
    ''' <summary>
    ''' 伝票ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票ユニークNO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_unique_no")> _
    Public Property DenpyouUniqueNo() As Integer
        Get
            Return intDenpyouUniqueNo
        End Get
        Set(ByVal value As Integer)
            intDenpyouUniqueNo = value
        End Set
    End Property
#End Region

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
    Private strTysKaisyaJigyousyoCd As String
    ''' <summary>
    ''' 調査会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社事業所コード</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_jigyousyo_cd")> _
    Public Property TysKaisyaJigyousyoCd() As String
        Get
            Return strTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "年月日"
    ''' <summary>
    ''' 年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateNengappi As DateTime
    ''' <summary>
    ''' 年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 年月日</returns>
    ''' <remarks></remarks>
    <TableMap("nengappi")> _
    Public Property Nengappi() As DateTime
        Get
            Return dateNengappi
        End Get
        Set(ByVal value As DateTime)
            dateNengappi = value
        End Set
    End Property
#End Region

#Region "科目"
    ''' <summary>
    ''' 科目
    ''' </summary>
    ''' <remarks></remarks>
    Private strKamoku As String
    ''' <summary>
    ''' 科目
    ''' </summary>
    ''' <value></value>
    ''' <returns> 科目</returns>
    ''' <remarks></remarks>
    <TableMap("kamoku")> _
    Public Property Kamoku() As String
        Get
            Return strKamoku
        End Get
        Set(ByVal value As String)
            strKamoku = value
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
    <TableMap("syouhin_cd")> _
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

#Region "品名"
    ''' <summary>
    ''' 品名
    ''' </summary>
    ''' <remarks></remarks>
    Private strHinmei As String
    ''' <summary>
    ''' 品名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 品名</returns>
    ''' <remarks></remarks>
    <TableMap("hinmei")> _
    Public Property Hinmei() As String
        Get
            Return strHinmei
        End Get
        Set(ByVal value As String)
            strHinmei = value
        End Set
    End Property
#End Region

#Region "顧客番号"
    ''' <summary>
    ''' 顧客番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strKokyakuNo As String
    ''' <summary>
    ''' 顧客番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 顧客番号</returns>
    ''' <remarks></remarks>
    <TableMap("kokyaku_no")> _
    Public Property KokyakuNo() As String
        Get
            Return strKokyakuNo
        End Get
        Set(ByVal value As String)
            strKokyakuNo = value
        End Set
    End Property
#End Region

#Region "物件名"
    ''' <summary>
    ''' 物件名
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenMei As String
    ''' <summary>
    ''' 物件名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 物件名</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_mei")> _
    Public Property BukkenMei() As String
        Get
            Return strBukkenMei
        End Get
        Set(ByVal value As String)
            strBukkenMei = value
        End Set
    End Property
#End Region

#Region "数量"
    ''' <summary>
    ''' 数量
    ''' </summary>
    ''' <remarks></remarks>
    Private intSuu As Integer = Integer.MinValue
    ''' <summary>
    ''' 数量
    ''' </summary>
    ''' <value></value>
    ''' <returns> 数量</returns>
    ''' <remarks></remarks>
    <TableMap("suu")> _
    Public Property Suu() As Integer
        Get
            Return intSuu
        End Get
        Set(ByVal value As Integer)
            intSuu = value
        End Set
    End Property
#End Region

#Region "単価"
    ''' <summary>
    ''' 単価
    ''' </summary>
    ''' <remarks></remarks>
    Private intTanka As Integer = Integer.MinValue
    ''' <summary>
    ''' 単価
    ''' </summary>
    ''' <value></value>
    ''' <returns> 単価</returns>
    ''' <remarks></remarks>
    <TableMap("tanka")> _
    Public Property Tanka() As Integer
        Get
            Return intTanka
        End Get
        Set(ByVal value As Integer)
            intTanka = value
        End Set
    End Property
#End Region

#Region "税抜金額"
    ''' <summary>
    ''' 税抜金額
    ''' </summary>
    ''' <remarks></remarks>
    Private lngZeinukiGaku As Long = Long.MinValue
    ''' <summary>
    ''' 税抜金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 税抜金額</returns>
    ''' <remarks></remarks>
    <TableMap("zeinuki_gaku")> _
    Public Property ZeinukiGaku() As Long
        Get
            Return lngZeinukiGaku
        End Get
        Set(ByVal value As Long)
            lngZeinukiGaku = value
        End Set
    End Property
#End Region

#Region "外税額"
    ''' <summary>
    ''' 外税額
    ''' </summary>
    ''' <remarks></remarks>
    Private intSotozeiGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' 外税額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 外税額</returns>
    ''' <remarks></remarks>
    <TableMap("sotozei_gaku")> _
    Public Property SotozeiGaku() As Integer
        Get
            Return intSotozeiGaku
        End Get
        Set(ByVal value As Integer)
            intSotozeiGaku = value
        End Set
    End Property
#End Region

#Region "金額(税込み)"
    ''' <summary>
    ''' 金額(税込み)
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKingaku As Long = Long.MinValue
    ''' <summary>
    ''' 金額(税込み)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 金額(税込み)</returns>
    ''' <remarks></remarks>
    <TableMap("kingaku")> _
    Public Property Kingaku() As Long
        Get
            Return lngKingaku
        End Get
        Set(ByVal value As Long)
            lngKingaku = value
        End Set
    End Property
#End Region

#Region "残高"
    ''' <summary>
    ''' 残高
    ''' </summary>
    ''' <remarks></remarks>
    Private lngZandaka As Long = Long.MinValue
    ''' <summary>
    ''' 残高
    ''' </summary>
    ''' <value></value>
    ''' <returns> 残高</returns>
    ''' <remarks></remarks>
    <TableMap("zandaka")> _
    Public Property Zandaka() As Long
        Get
            Return lngZandaka
        End Get
        Set(ByVal value As Long)
            lngZandaka = value
        End Set
    End Property
#End Region

#Region "伝票NO"
    ''' <summary>
    ''' 伝票NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenpyouNo As String
    ''' <summary>
    ''' 伝票NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 伝票NO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_no")> _
    Public Property DenpyouNo() As String
        Get
            Return strDenpyouNo
        End Get
        Set(ByVal value As String)
            strDenpyouNo = value
        End Set
    End Property
#End Region

End Class