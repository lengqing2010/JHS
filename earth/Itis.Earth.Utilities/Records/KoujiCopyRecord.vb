Public Class KoujiCopyRecord

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
    <TableMap("kbn")> _
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "保証書NO"
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証書NO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no")> _
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "承認者コード"
    ''' <summary>
    ''' 承認者コード
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouninsyaCd As Integer = Integer.MinValue
    ''' <summary>
    ''' 承認者コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 承認者コード</returns>
    ''' <remarks></remarks>
    <TableMap("syouninsya_cd")> _
    Public Property SyouninsyaCd() As Integer
        Get
            Return intSyouninsyaCd
        End Get
        Set(ByVal value As Integer)
            intSyouninsyaCd = value
        End Set
    End Property
#End Region

#Region "工事仕様確認"
    ''' <summary>
    ''' 工事仕様確認
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojSiyouKakunin As Integer = Integer.MinValue
    ''' <summary>
    ''' 工事仕様確認
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事仕様確認</returns>
    ''' <remarks></remarks>
    <TableMap("koj_siyou_kakunin")> _
    Public Property KojSiyouKakunin() As Integer
        Get
            Return intKojSiyouKakunin
        End Get
        Set(ByVal value As Integer)
            intKojSiyouKakunin = value
        End Set
    End Property
#End Region

#Region "工事仕様確認日"
    ''' <summary>
    ''' 工事仕様確認日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojSiyouKakuninDate As DateTime
    ''' <summary>
    ''' 工事仕様確認日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事仕様確認日</returns>
    ''' <remarks></remarks>
    <TableMap("koj_siyou_kakunin_date")> _
    Public Property KojSiyouKakuninDate() As DateTime
        Get
            Return dateKojSiyouKakuninDate
        End Get
        Set(ByVal value As DateTime)
            dateKojSiyouKakuninDate = value
        End Set
    End Property
#End Region

#Region "工事会社コード"
    ''' <summary>
    ''' 工事会社コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojGaisyaCd As String
    ''' <summary>
    ''' 工事会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事会社コード</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gaisya_cd")> _
    Public Property KojGaisyaCd() As String
        Get
            Return strKojGaisyaCd
        End Get
        Set(ByVal value As String)
            strKojGaisyaCd = value
        End Set
    End Property
#End Region

#Region "工事会社請求有無"
    ''' <summary>
    ''' 工事会社請求有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intKojGaisyaSeikyuuUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' 工事会社請求有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事会社請求有無</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gaisya_seikyuu_umu")> _
    Public Property KojGaisyaSeikyuuUmu() As Integer
        Get
            Return intKojGaisyaSeikyuuUmu
        End Get
        Set(ByVal value As Integer)
            intKojGaisyaSeikyuuUmu = value
        End Set
    End Property
#End Region

#Region "改良工事種別"
    ''' <summary>
    ''' 改良工事種別
    ''' </summary>
    ''' <remarks></remarks>
    Private intKairyKojSyubetu As Integer = Integer.MinValue
    ''' <summary>
    ''' 改良工事種別
    ''' </summary>
    ''' <value></value>
    ''' <returns> 改良工事種別</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_syubetu")> _
    Public Property KairyKojSyubetu() As Integer
        Get
            Return intKairyKojSyubetu
        End Get
        Set(ByVal value As Integer)
            intKairyKojSyubetu = value
        End Set
    End Property
#End Region

#Region "改良工事完了予定日"
    ''' <summary>
    ''' 改良工事完了予定日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKairyKojKanryYoteiDate As DateTime
    ''' <summary>
    ''' 改良工事完了予定日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 改良工事完了予定日</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_kanry_yotei_date")> _
    Public Property KairyKojKanryYoteiDate() As DateTime
        Get
            Return dateKairyKojKanryYoteiDate
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojKanryYoteiDate = value
        End Set
    End Property
#End Region

#Region "改良工事完工速報着日"
    ''' <summary>
    ''' 改良工事完工速報着日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKairyKojSokuhouTykDate As DateTime
    ''' <summary>
    ''' 改良工事完工速報着日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 改良工事完工速報着日</returns>
    ''' <remarks></remarks>
    <TableMap("kairy_koj_sokuhou_tyk_date")> _
    Public Property KairyKojSokuhouTykDate() As DateTime
        Get
            Return dateKairyKojSokuhouTykDate
        End Get
        Set(ByVal value As DateTime)
            dateKairyKojSokuhouTykDate = value
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

#Region "請求有無"
    ''' <summary>
    ''' 請求有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intSeikyuuUmu As Integer = Integer.MinValue
    ''' <summary>
    ''' 請求有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求有無</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_umu")> _
    Public Property SeikyuuUmu() As Integer
        Get
            Return intSeikyuuUmu
        End Get
        Set(ByVal value As Integer)
            intSeikyuuUmu = value
        End Set
    End Property
#End Region

#Region "売上金額"
    ''' <summary>
    ''' 売上金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' 売上金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上金額</returns>
    ''' <remarks></remarks>
    <TableMap("uri_gaku")> _
    Public Property UriGaku() As Integer
        Get
            Return intUriGaku
        End Get
        Set(ByVal value As Integer)
            intUriGaku = value
        End Set
    End Property
#End Region

#Region "仕入金額"
    ''' <summary>
    ''' 仕入金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' 仕入金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 仕入金額</returns>
    ''' <remarks></remarks>
    <TableMap("siire_gaku")> _
    Public Property SiireGaku() As Integer
        Get
            Return intSiireGaku
        End Get
        Set(ByVal value As Integer)
            intSiireGaku = value
        End Set
    End Property
#End Region

#Region "売上計上日"
    ''' <summary>
    ''' 売上計上日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriKeijyouDate As DateTime
    ''' <summary>
    ''' 売上計上日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 売上計上日</returns>
    ''' <remarks></remarks>
    <TableMap("uri_keijyou_date")> _
    Public Property UriKeijyouDate() As DateTime
        Get
            Return dateUriKeijyouDate
        End Get
        Set(ByVal value As DateTime)
            dateUriKeijyouDate = value
        End Set
    End Property
#End Region

#Region "発注書金額"
    ''' <summary>
    ''' 発注書金額
    ''' </summary>
    ''' <remarks></remarks>
    Private intHattyuusyoGaku As Integer
    ''' <summary>
    ''' 発注書金額
    ''' </summary>
    ''' <value></value>
    ''' <returns> 発注書金額</returns>
    ''' <remarks></remarks>
    <TableMap("hattyuusyo_gaku")> _
    Public Property HattyuusyoGaku() As Integer
        Get
            If intHattyuusyoGaku = 0 Then
                Return Integer.MinValue
            End If

            Return intHattyuusyoGaku
        End Get
        Set(ByVal value As Integer)
            intHattyuusyoGaku = value
        End Set
    End Property
#End Region

End Class