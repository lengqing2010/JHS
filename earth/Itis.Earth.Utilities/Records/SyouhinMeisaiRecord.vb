''' <summary>
''' 商品情報のレコードクラスです
''' </summary>
''' <remarks></remarks>
Public Class SyouhinMeisaiRecord

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

#Region "商品名"
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinMei As String
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品名</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_mei")> _
    Public Property SyouhinMei() As String
        Get
            Return strSyouhinMei
        End Get
        Set(ByVal value As String)
            strSyouhinMei = value
        End Set
    End Property
#End Region

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
    <TableMap("torikesi")> _
    Public Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "単位"
    ''' <summary>
    ''' 単位
    ''' </summary>
    ''' <remarks></remarks>
    Private strTani As String
    ''' <summary>
    ''' 単位
    ''' </summary>
    ''' <value></value>
    ''' <returns> 単位</returns>
    ''' <remarks></remarks>
    <TableMap("tani")> _
    Public Property Tani() As String
        Get
            Return strTani
        End Get
        Set(ByVal value As String)
            strTani = value
        End Set
    End Property
#End Region

#Region "請求先区分"
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusakiKbn As String
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先区分</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusaki_kbn")> _
    Public Property SeikyuusakiKbn() As String
        Get
            Return strSeikyuusakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuusakiKbn = value
        End Set
    End Property
#End Region

#Region "支払用商品名"
    ''' <summary>
    ''' 支払用商品名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSiharaiyouSyouhinMei As String
    ''' <summary>
    ''' 支払用商品名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 支払用商品名</returns>
    ''' <remarks></remarks>
    <TableMap("siharaiyou_syouhin_mei")> _
    Public Property SiharaiyouSyouhinMei() As String
        Get
            Return strSiharaiyouSyouhinMei
        End Get
        Set(ByVal value As String)
            strSiharaiyouSyouhinMei = value
        End Set
    End Property
#End Region

#Region "商品区分1"
    ''' <summary>
    ''' 商品区分1
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinKbn1 As String
    ''' <summary>
    ''' 商品区分1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品区分1</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_kbn1")> _
    Public Property SyouhinKbn1() As String
        Get
            Return strSyouhinKbn1
        End Get
        Set(ByVal value As String)
            strSyouhinKbn1 = value
        End Set
    End Property
#End Region

#Region "商品区分2"
    ''' <summary>
    ''' 商品区分2
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinKbn2 As String
    ''' <summary>
    ''' 商品区分2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品区分2</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_kbn2")> _
    Public Property SyouhinKbn2() As String
        Get
            Return strSyouhinKbn2
        End Get
        Set(ByVal value As String)
            strSyouhinKbn2 = value
        End Set
    End Property
#End Region

#Region "商品区分3"
    ''' <summary>
    ''' 商品区分3
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinKbn3 As String
    ''' <summary>
    ''' 商品区分3
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品区分3</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_kbn3")> _
    Public Property SyouhinKbn3() As String
        Get
            Return strSyouhinKbn3
        End Get
        Set(ByVal value As String)
            strSyouhinKbn3 = value
        End Set
    End Property
#End Region

#Region "保証有無"
    ''' <summary>
    ''' 保証有無
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouUmu As Integer
    ''' <summary>
    ''' 保証有無
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保証有無</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_umu")> _
    Public Property HosyouUmu() As Integer
        Get
            Return intHosyouUmu
        End Get
        Set(ByVal value As Integer)
            intHosyouUmu = value
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
    ''' <returns> 税区分</returns>
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

#Region "税込区分"
    ''' <summary>
    ''' 税込区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strZeikomiKbn As String
    ''' <summary>
    ''' 税込区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 税込区分</returns>
    ''' <remarks></remarks>
    <TableMap("zeikomi_kbn")> _
    Public Property ZeikomiKbn() As String
        Get
            Return strZeikomiKbn
        End Get
        Set(ByVal value As String)
            strZeikomiKbn = value
        End Set
    End Property
#End Region

#Region "標準価格"
    ''' <summary>
    ''' 標準価格
    ''' </summary>
    ''' <remarks></remarks>
    Private intHyoujunKkk As Integer
    ''' <summary>
    ''' 標準価格
    ''' </summary>
    ''' <value></value>
    ''' <returns> 標準価格</returns>
    ''' <remarks></remarks>
    <TableMap("hyoujun_kkk")> _
    Public Property HyoujunKkk() As Integer
        Get
            Return intHyoujunKkk
        End Get
        Set(ByVal value As Integer)
            intHyoujunKkk = value
        End Set
    End Property
#End Region

#Region "系列コード"
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiretuCd As String
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 系列コード</returns>
    ''' <remarks></remarks>
    <TableMap("keiretu_cd")> _
    Public Property KeiretuCd() As String
        Get
            Return strKeiretuCd
        End Get
        Set(ByVal value As String)
            strKeiretuCd = value
        End Set
    End Property
#End Region

#Region "ビルダー請求額"
    ''' <summary>
    ''' ビルダー請求額
    ''' </summary>
    ''' <remarks></remarks>
    Private intBuilderSeikyuugaku As Integer
    ''' <summary>
    ''' ビルダー請求額
    ''' </summary>
    ''' <value></value>
    ''' <returns> ビルダー請求額</returns>
    ''' <remarks></remarks>
    <TableMap("builder_seikyuugaku")> _
    Public Property BuilderSeikyuugaku() As Integer
        Get
            Return intBuilderSeikyuugaku
        End Get
        Set(ByVal value As Integer)
            intBuilderSeikyuugaku = value
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
    ''' <returns> 倉庫コード</returns>
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

#Region "仕入価格"
    ''' <summary>
    ''' 仕入価格
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireKkk As Integer
    ''' <summary>
    ''' 仕入価格
    ''' </summary>
    ''' <value></value>
    ''' <returns> 仕入価格</returns>
    ''' <remarks></remarks>
    <TableMap("siire_kkk")> _
    Public Property SiireKkk() As Integer
        Get
            Return intSiireKkk
        End Get
        Set(ByVal value As Integer)
            intSiireKkk = value
        End Set
    End Property
#End Region

#Region "工事タイプ"
    ''' <summary>
    ''' 工事タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKojType As String
    ''' <summary>
    ''' 工事タイプ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 工事タイプ</returns>
    ''' <remarks></remarks>
    <TableMap("koj_type")> _
    Public Property KojType() As String
        Get
            Return strKojType
        End Get
        Set(ByVal value As String)
            strKojType = value
        End Set
    End Property
#End Region

#Region "調査有無区分"
    ''' <summary>
    ''' 調査有無区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intTysUmuKbn As Integer
    ''' <summary>
    ''' 調査有無区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査有無区分 </returns>
    ''' <remarks></remarks>
    <TableMap("tys_umu_kbn")> _
    Public Property TysUmuKbn() As Integer
        Get
            Return intTysUmuKbn
        End Get
        Set(ByVal value As Integer)
            intTysUmuKbn = value
        End Set
    End Property
#End Region

#Region "登録ログインユーザーID"
    ''' <summary>
    ''' 登録ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String
    ''' <summary>
    ''' 登録ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id")> _
    Public Property AddLoginUserId() As String
        Get
            Return strAddLoginUserId
        End Get
        Set(ByVal value As String)
            strAddLoginUserId = value
        End Set
    End Property
#End Region

#Region "登録日時"
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetime As DateTime
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 登録日時</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime")> _
    Public Property AddDatetime() As DateTime
        Get
            Return dateAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetime = value
        End Set
    End Property
#End Region

#Region "更新ログインユーザーID"
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' 更新ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id")> _
    Public Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

#Region "更新日時"
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime")> _
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
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
    ''' <returns> 税率</returns>
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

#Region "加盟店コード"
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCdDisp As String
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店コード</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd")> _
    Public Property KameitenCdDisp() As String
        Get
            Return strKameitenCdDisp
        End Get
        Set(ByVal value As String)
            strKameitenCdDisp = value
        End Set
    End Property
#End Region

#Region "請求先コード(基本)"
    ''' <summary>
    ''' 請求先コード(基本)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCdDisp As String
    ''' <summary>
    ''' 請求先コード(基本)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先コード(基本)</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_cd")> _
    Public Property SeikyuuSakiCdDisp() As String
        Get
            Return strSeikyuuSakiCdDisp
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiCdDisp = value
        End Set
    End Property
#End Region

#Region "請求先枝番(基本)"
    ''' <summary>
    ''' 請求先枝番(基本)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrcDisp As String
    ''' <summary>
    ''' 請求先枝番(基本)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先枝番(基本)</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_brc")> _
    Public Property SeikyuuSakiBrcDisp() As String
        Get
            Return strSeikyuuSakiBrcDisp
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiBrcDisp = value
        End Set
    End Property
#End Region

#Region "請求先区分(基本)"
    ''' <summary>
    ''' 請求先区分(基本)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbnDisp As String
    ''' <summary>
    ''' 請求先区分(基本)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先区分(基本)</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn")> _
    Public Property SeikyuuSakiKbnDisp() As String
        Get
            Return strSeikyuuSakiKbnDisp
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbnDisp = value
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
            If KameitenCdDisp <> String.Empty AndAlso _
               SeikyuuSakiCdDisp <> String.Empty AndAlso _
               KameitenCdDisp = SeikyuuSakiCdDisp Then
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

#Region "SDS自動設定"
    ''' <summary>
    ''' SDS自動設定
    ''' </summary>
    ''' <remarks></remarks>
    Private intSdsJidouSet As Integer
    ''' <summary>
    ''' SDS自動設定
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査有無区分 </returns>
    ''' <remarks></remarks>
    <TableMap("sds_jidou_set")> _
    Public Property SdsJidouSet() As Integer
        Get
            Return intSdsJidouSet
        End Get
        Set(ByVal value As Integer)
            intSdsJidouSet = value
        End Set
    End Property
#End Region

End Class