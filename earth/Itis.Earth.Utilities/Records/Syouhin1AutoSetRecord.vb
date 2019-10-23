''' <summary>
''' 商品コード1の自動設定レコード
''' </summary>
''' <remarks></remarks>
Public Class Syouhin1AutoSetRecord

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
    ''' <returns>商品コード</returns>
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

#Region "商品名"
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinNm As String
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品名</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_mei")> _
    Public Property SyouhinNm() As String
        Get
            Return strSyouhinNm
        End Get
        Set(ByVal value As String)
            strSyouhinNm = value
        End Set
    End Property
#End Region

#Region "調査概要"
    ''' <summary>
    ''' 調査概要
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyousaGaiyou As Integer
    ''' <summary>
    ''' 調査概要(未指定時は9を設定してください)
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査概要</returns>
    ''' <remarks></remarks>
    Public Property TyousaGaiyou() As Integer
        Get
            Return intTyousaGaiyou
        End Get
        Set(ByVal value As Integer)
            intTyousaGaiyou = value
        End Set
    End Property
#End Region

#Region "税抜価格"
    ''' <summary>
    ''' 税抜価格
    ''' </summary>
    ''' <remarks></remarks>
    Private intZeinuki As Integer
    ''' <summary>
    ''' 税抜価格
    ''' </summary>
    ''' <value></value>
    ''' <returns>税抜価格</returns>
    ''' <remarks></remarks>
    Public Property Zeinuki() As Integer
        Get
            Return intZeinuki
        End Get
        Set(ByVal value As Integer)
            intZeinuki = value
        End Set
    End Property
#End Region

#Region "税区分"
    ''' <summary>
    ''' 税区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strZeiKbn As String
    ''' <summary>
    ''' 税区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>税区分</returns>
    ''' <remarks></remarks>
    <TableMap("zei_kbn")> _
    Public Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
        End Set
    End Property
#End Region

#Region "税率"
    ''' <summary>
    ''' 税率
    ''' </summary>
    ''' <remarks></remarks>
    Private decZeiritu As Decimal
    ''' <summary>
    ''' 税率
    ''' </summary>
    ''' <value></value>
    ''' <returns>税率</returns>
    ''' <remarks></remarks>
    <TableMap("zeiritu")> _
    Public Property Zeiritu() As Decimal
        Get
            Return decZeiritu
        End Get
        Set(ByVal value As Decimal)
            decZeiritu = value
        End Set
    End Property
#End Region

#Region "消費税（売上）"
    ''' <summary>
    ''' 消費税（売上）
    ''' </summary>
    ''' <remarks></remarks>
    Private decTaxUri As Decimal
    ''' <summary>
    ''' 消費税（売上）
    ''' </summary>
    ''' <value></value>
    ''' <returns>消費税（売上）</returns>
    ''' <remarks></remarks>
    Public Property TaxUri() As Decimal
        Get
            Return decTaxUri
        End Get
        Set(ByVal value As Decimal)
            decTaxUri = value
        End Set
    End Property
#End Region

#Region "価格設定場所"
    ''' <summary>
    ''' 価格設定場所
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakakuSettei As Integer
    ''' <summary>
    ''' 価格設定場所
    ''' </summary>
    ''' <value></value>
    ''' <returns>価格設定場所</returns>
    ''' <remarks>価格設定場所(0:未設定,1:加盟店M,2:商品M)</remarks>
    Public Property KakakuSettei() As Integer
        Get
            Return intKakakuSettei
        End Get
        Set(ByVal value As Integer)
            intKakakuSettei = value
        End Set
    End Property
#End Region

#Region "工務店請求金額"
    ''' <summary>
    ''' 工務店請求金額
    ''' </summary>
    ''' <remarks></remarks>
    Private decKoumutenGaku As Decimal
    ''' <summary>
    ''' 工務店請求金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>工務店請求金額</returns>
    ''' <remarks></remarks>
    Public Property KoumutenGaku() As Decimal
        Get
            Return decKoumutenGaku
        End Get
        Set(ByVal value As Decimal)
            decKoumutenGaku = value
        End Set
    End Property
#End Region

#Region "実請求金額"
    ''' <summary>
    ''' 実請求金額
    ''' </summary>
    ''' <remarks></remarks>
    Private decJituGaku As Decimal
    ''' <summary>
    ''' 実請求金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>実請求金額</returns>
    ''' <remarks></remarks>
    Public Property JituGaku() As Decimal
        Get
            Return decJituGaku
        End Get
        Set(ByVal value As Decimal)
            decJituGaku = value
        End Set
    End Property
#End Region

#Region "倉庫コード"
    ''' <summary>
    ''' 倉庫コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSoukoCd As String
    ''' <summary>
    ''' 倉庫コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>倉庫コード</returns>
    ''' <remarks></remarks>
    <TableMap("souko_cd")> _
    Public Property SoukoCd() As String
        Get
            Return strSoukoCd
        End Get
        Set(ByVal value As String)
            strSoukoCd = value
        End Set
    End Property
#End Region

#Region "標準価格"
    ''' <summary>
    ''' 標準価格
    ''' </summary>
    ''' <remarks></remarks>
    Private decHyoujunKakaku As Integer
    ''' <summary>
    ''' 標準価格
    ''' </summary>
    ''' <value></value>
    ''' <returns>標準価格</returns>
    ''' <remarks></remarks>
    <TableMap("hyoujun_kkk")> _
    Public Property HyoujunKakaku() As Integer
        Get
            Return decHyoujunKakaku
        End Get
        Set(ByVal value As Integer)
            decHyoujunKakaku = value
        End Set
    End Property
#End Region

#Region "加盟店コード"
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店コード</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd")> _
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "請求先コード(基本)"
    ''' <summary>
    ''' 請求先コード(基本)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String
    ''' <summary>
    ''' 請求先コード(基本)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先コード(基本)</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_cd")> _
    Public Property SeikyuuSakiCd() As String
        Get
            Return strSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "請求先枝番(基本)"
    ''' <summary>
    ''' 請求先枝番(基本)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrc As String
    ''' <summary>
    ''' 請求先枝番(基本)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先枝番(基本)</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_brc")> _
    Public Property SeikyuuSakiBrc() As String
        Get
            Return strSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "請求先区分(基本)"
    ''' <summary>
    ''' 請求先区分(基本)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String
    ''' <summary>
    ''' 請求先区分(基本)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先区分(基本)</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn")> _
    Public Property SeikyuuSakiKbn() As String
        Get
            Return strSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "請求先タイプ(直接or他請求)"
    ''' <summary>
    ''' 請求先タイプ(直接or他請求)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiType As String
    ''' <summary>
    ''' 請求先タイプ(直接or他請求)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先タイプ(直接or他請求)</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn")> _
    Public Property SeikyuuSakiType() As String
        Get
            If KameitenCd <> String.Empty AndAlso _
               SeikyuuSakiCd <> String.Empty AndAlso _
               KameitenCd = SeikyuuSakiCd Then
                Return EarthConst.SEIKYU_TYOKUSETU
            Else
                Return EarthConst.SEIKYU_TASETU
            End If
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiType = value
        End Set
    End Property
#End Region

#Region "工務店請求金額変更FLG"
    ''' <summary>
    ''' 工務店請求金額変更FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoumutenGakuHenkouFlg As Boolean = True
    ''' <summary>
    ''' 工務店請求金額変更FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>工務店請求金額変更FLG </returns>
    ''' <remarks></remarks>
    <TableMap("koumuten_seikyuu_gaku_henkou_flg")> _
    Public Property KoumutenGakuHenkouFlg() As Boolean
        Get
            Return intKoumutenGakuHenkouFlg
        End Get
        Set(ByVal value As Boolean)
            intKoumutenGakuHenkouFlg = value
        End Set
    End Property
#End Region

#Region "実請求金額変更FLG"
    ''' <summary>
    ''' 実請求金額変更FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intJituGakuHenkouFlg As Boolean = True
    ''' <summary>
    ''' 実請求金額変更FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>実請求金額変更FLG </returns>
    ''' <remarks></remarks>
    <TableMap("jitu_seikyuu_gaku_henkou_flg")> _
    Public Property JituGakuHenkouFlg() As Boolean
        Get
            Return intJituGakuHenkouFlg
        End Get
        Set(ByVal value As Boolean)
            intJituGakuHenkouFlg = value
        End Set
    End Property
#End Region

#Region "商品1取得ステータス"
    ''' <summary>
    ''' 商品1取得ステータス
    ''' </summary>
    ''' <remarks></remarks>
    Private intSetSts As Integer = 0
    ''' <summary>
    ''' 商品1取得ステータス
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品1取得ステータス</returns>
    ''' <remarks></remarks>
    Public Property SetSts() As Integer
        Get
            Return intSetSts
        End Get
        Set(ByVal value As Integer)
            intSetSts = value
        End Set
    End Property
#End Region

End Class
