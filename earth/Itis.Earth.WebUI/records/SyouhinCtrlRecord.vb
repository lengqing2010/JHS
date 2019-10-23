Imports System.Web.UI.HtmlControls
''' <summary>
''' 商品情報のコントロール参照用レコードクラス
''' </summary>
''' <remarks></remarks>
Public Class SyouhinCtrlRecord

#Region "商品行(TR)"
    ''' <summary>
    ''' 商品行(TR)
    ''' </summary>
    ''' <remarks></remarks>
    Private trSyouhinLine As HtmlTableRow
    ''' <summary>
    ''' 商品行(TR)
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品行(TR)</returns>
    ''' <remarks></remarks>
    Public Property SyouhinLine() As HtmlTableRow
        Get
            Return trSyouhinLine
        End Get
        Set(ByVal value As HtmlTableRow)
            trSyouhinLine = value
        End Set
    End Property
#End Region
#Region "分類コード"
    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <remarks></remarks>
    Private txtBunruiCd As HtmlInputHidden
    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>分類コード</returns>
    ''' <remarks></remarks>
    Public Property BunruiCd() As HtmlInputHidden
        Get
            Return txtBunruiCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtBunruiCd = value
        End Set
    End Property
#End Region
#Region "商品コード"
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSyouhinCd As HtmlInputText
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品コード</returns>
    ''' <remarks></remarks>
    Public Property SyouhinCd() As HtmlInputText
        Get
            Return txtSyouhinCd
        End Get
        Set(ByVal value As HtmlInputText)
            txtSyouhinCd = value
        End Set
    End Property
#End Region
#Region "商品コードOld"
    ''' <summary>
    ''' 商品コードOld
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSyouhinCdOld As HtmlInputHidden
    ''' <summary>
    ''' 商品コードOld
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品コードOld</returns>
    ''' <remarks></remarks>
    Public Property SyouhinCdOld() As HtmlInputHidden
        Get
            Return txtSyouhinCdOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtSyouhinCdOld = value
        End Set
    End Property
#End Region
#Region "商品検索ボタン"
    ''' <summary>
    ''' 商品検索ボタン
    ''' </summary>
    ''' <remarks></remarks>
    Private btnShouhinSearch As HtmlInputButton
    ''' <summary>
    ''' 商品検索ボタン
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品検索ボタン</returns>
    ''' <remarks></remarks>
    Public Property ShouhinSearchBtn() As HtmlInputButton
        Get
            Return btnShouhinSearch
        End Get
        Set(ByVal value As HtmlInputButton)
            btnShouhinSearch = value
        End Set
    End Property
#End Region
#Region "商品名"
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSyouhinNm As HtmlInputText
    ''' <summary>
    ''' 商品名
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品名</returns>
    ''' <remarks></remarks>
    Public Property SyouhinNm() As HtmlInputText
        Get
            Return txtSyouhinNm
        End Get
        Set(ByVal value As HtmlInputText)
            txtSyouhinNm = value
        End Set
    End Property
#End Region
#Region "商品名（表示用）"
    ''' <summary>
    ''' 商品名（表示用）
    ''' </summary>
    ''' <remarks></remarks>
    Private txtDispSyouhinNm As HtmlGenericControl
    ''' <summary>
    ''' 商品名（表示用）
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品名（表示用）</returns>
    ''' <remarks></remarks>
    Public Property DispSyouhinNm() As HtmlGenericControl
        Get
            Return txtDispSyouhinNm
        End Get
        Set(ByVal value As HtmlGenericControl)
            txtDispSyouhinNm = value
        End Set
    End Property
#End Region
#Region "確定区分（商品３のみ）"
    ''' <summary>
    ''' 確定区分（商品３のみ）
    ''' </summary>
    ''' <remarks></remarks>
    Private selKakuteiKbn As HtmlSelect
    ''' <summary>
    ''' 確定区分（商品３のみ）
    ''' </summary>
    ''' <value></value>
    ''' <returns>確定区分（商品３のみ）</returns>
    ''' <remarks></remarks>
    Public Property KakuteiKbn() As HtmlSelect
        Get
            Return selKakuteiKbn
        End Get
        Set(ByVal value As HtmlSelect)
            selKakuteiKbn = value
        End Set
    End Property
#End Region
#Region "確定区分SPAN"
    ''' <summary>
    ''' 確定区分SPAN
    ''' </summary>
    ''' <remarks></remarks>
    Private gcKakuteiKbnSpan As HtmlGenericControl
    ''' <summary>
    ''' 確定区分SPAN
    ''' </summary>
    ''' <value></value>
    ''' <returns>確定区分SPAN</returns>
    ''' <remarks></remarks>
    Public Property KakuteiKbnSpan() As HtmlGenericControl
        Get
            Return gcKakuteiKbnSpan
        End Get
        Set(ByVal value As HtmlGenericControl)
            gcKakuteiKbnSpan = value
        End Set
    End Property
#End Region
#Region "確定区分Old（商品３のみ）"
    ''' <summary>
    ''' 確定区分Old（商品３のみ）
    ''' </summary>
    ''' <remarks></remarks>
    Private hidKakuteiOld As HtmlInputHidden
    ''' <summary>
    ''' 確定区分Old（商品３のみ）
    ''' </summary>
    ''' <value></value>
    ''' <returns>確定区分Old（商品３のみ）</returns>
    ''' <remarks></remarks>
    Public Property KakuteiKbnOld() As HtmlInputHidden
        Get
            Return hidKakuteiOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            hidKakuteiOld = value
        End Set
    End Property
#End Region
#Region "工務店請求税抜金額"
    ''' <summary>
    ''' 工務店請求税抜金額
    ''' </summary>
    ''' <remarks></remarks>
    Private txtKoumutenSeikyuuGaku As HtmlInputText
    ''' <summary>
    ''' 工務店請求税抜金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>工務店請求税抜金額</returns>
    ''' <remarks></remarks>
    Public Property KoumutenSeikyuuGaku() As HtmlInputText
        Get
            Return txtKoumutenSeikyuuGaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtKoumutenSeikyuuGaku = value
        End Set
    End Property
#End Region
#Region "工務店請求税抜金額Old"
    ''' <summary>
    ''' 工務店請求税抜金額Old
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnKoumutenSeikyuuGakuOld As HtmlInputHidden
    ''' <summary>
    ''' 工務店請求税抜金額Old
    ''' </summary>
    ''' <value></value>
    ''' <returns>工務店請求税抜金額Old</returns>
    ''' <remarks></remarks>
    Public Property KoumutenSeikyuuGakuOld() As HtmlInputHidden
        Get
            Return hdnKoumutenSeikyuuGakuOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            hdnKoumutenSeikyuuGakuOld = value
        End Set
    End Property
#End Region
#Region "実請求金額"
    ''' <summary>
    ''' 実請求金額
    ''' </summary>
    ''' <remarks></remarks>
    Private txtJituSeikyuuGaku As HtmlInputText
    ''' <summary>
    ''' 実請求金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>実請求金額</returns>
    ''' <remarks></remarks>
    Public Property JituSeikyuuGaku() As HtmlInputText
        Get
            Return txtJituSeikyuuGaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtJituSeikyuuGaku = value
        End Set
    End Property
#End Region
#Region "実請求金額Old"
    ''' <summary>
    ''' 実請求金額Old
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnJituSeikyuuGakuOld As HtmlInputHidden
    ''' <summary>
    ''' 実請求金額Old
    ''' </summary>
    ''' <value></value>
    ''' <returns>実請求金額Old</returns>
    ''' <remarks></remarks>
    Public Property JituSeikyuuGakuOld() As HtmlInputHidden
        Get
            Return hdnJituSeikyuuGakuOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            hdnJituSeikyuuGakuOld = value
        End Set
    End Property
#End Region
#Region "消費税額"
    ''' <summary>
    ''' 消費税額
    ''' </summary>
    ''' <remarks></remarks>
    Private txtZeiGaku As HtmlInputText
    ''' <summary>
    ''' 消費税額
    ''' </summary>
    ''' <value></value>
    ''' <returns>消費税額</returns>
    ''' <remarks></remarks>
    Public Property ZeiGaku() As HtmlInputText
        Get
            Return txtZeiGaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtZeiGaku = value
        End Set
    End Property
#End Region
#Region "税区分"
    ''' <summary>
    ''' 税区分
    ''' </summary>
    ''' <remarks></remarks>
    Private txtZeikubun As HtmlInputHidden
    ''' <summary>
    ''' 税区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>税区分</returns>
    ''' <remarks></remarks>
    Public Property ZeiKubun() As HtmlInputHidden
        Get
            Return txtZeikubun
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtZeikubun = value
        End Set
    End Property
#End Region
#Region "税率"
    ''' <summary>
    ''' 消費税額
    ''' </summary>
    ''' <remarks></remarks>
    Private txtZeiRitu As HtmlInputHidden
    ''' <summary>
    ''' 消費税額
    ''' </summary>
    ''' <value></value>
    ''' <returns>消費税額</returns>
    ''' <remarks></remarks>
    Public Property ZeiRitu() As HtmlInputHidden
        Get
            Return txtZeiRitu
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtZeiRitu = value
        End Set
    End Property
#End Region
#Region "税込金額"
    ''' <summary>
    ''' 税込金額
    ''' </summary>
    ''' <remarks></remarks>
    Private txtZeiKomiGaku As HtmlInputText
    ''' <summary>
    ''' 税込金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>税込金額</returns>
    ''' <remarks></remarks>
    Public Property ZeiKomiGaku() As HtmlInputText
        Get
            Return txtZeiKomiGaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtZeiKomiGaku = value
        End Set
    End Property
#End Region
#Region "承諾書金額"
    ''' <summary>
    ''' 承諾書金額
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSyoudakusyoKingaku As HtmlInputText
    ''' <summary>
    ''' 承諾書金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>承諾書金額</returns>
    ''' <remarks></remarks>
    Public Property SyoudakusyoKingaku() As HtmlInputText
        Get
            Return txtSyoudakusyoKingaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtSyoudakusyoKingaku = value
        End Set
    End Property
#End Region
#Region "承諾書金額Old"
    ''' <summary>
    ''' 承諾書金額Old
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnSyoudakusyoKingakuOld As HtmlInputHidden
    ''' <summary>
    ''' 承諾書金額Old
    ''' </summary>
    ''' <value></value>
    ''' <returns>承諾書金額Old</returns>
    ''' <remarks></remarks>
    Public Property SyoudakusyoKingakuOld() As HtmlInputHidden
        Get
            Return hdnSyoudakusyoKingakuOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            hdnSyoudakusyoKingakuOld = value
        End Set
    End Property
#End Region
#Region "仕入消費税"
    ''' <summary>
    ''' 仕入消費税
    ''' </summary>
    ''' <remarks></remarks>
    Private hidSiireSyouhizei As HtmlInputHidden
    ''' <summary>
    ''' 仕入消費税
    ''' </summary>
    ''' <value></value>
    ''' <returns>仕入消費税</returns>
    ''' <remarks></remarks>
    Public Property SiireSyouhizei() As HtmlInputHidden
        Get
            Return hidSiireSyouhizei
        End Get
        Set(ByVal value As HtmlInputHidden)
            hidSiireSyouhizei = value
        End Set
    End Property
#End Region
#Region "請求書発行日"
    ''' <summary>
    ''' 請求書発行日
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSeikyuusyoHakkouDate As HtmlInputText
    ''' <summary>
    ''' 請求書発行日
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求書発行日</returns>
    ''' <remarks></remarks>
    Public Property SeikyuusyoHakkouDate() As HtmlInputText
        Get
            Return txtSeikyuusyoHakkouDate
        End Get
        Set(ByVal value As HtmlInputText)
            txtSeikyuusyoHakkouDate = value
        End Set
    End Property
#End Region
#Region "売上年月日"
    ''' <summary>
    ''' 売上年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private txtUriageDate As HtmlInputText
    ''' <summary>
    ''' 売上年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns>売上年月日</returns>
    ''' <remarks></remarks>
    Public Property UriageDate() As HtmlInputText
        Get
            Return txtUriageDate
        End Get
        Set(ByVal value As HtmlInputText)
            txtUriageDate = value
        End Set
    End Property
#End Region
#Region "請求有無"
    ''' <summary>
    ''' 請求有無
    ''' </summary>
    ''' <remarks></remarks>
    Private selSeikyuuUmu As HtmlSelect
    ''' <summary>
    ''' 請求有無
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求有無</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuUmu() As HtmlSelect
        Get
            Return selSeikyuuUmu
        End Get
        Set(ByVal value As HtmlSelect)
            selSeikyuuUmu = value
        End Set
    End Property
#End Region
#Region "請求有無SAPN"
    ''' <summary>
    ''' 請求有無SPAN
    ''' </summary>
    ''' <remarks></remarks>
    Private gcSeikyuuUmuSpan As HtmlGenericControl
    ''' <summary>
    ''' 請求有無SPAN
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求有無SPAN</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuUmuSpan() As HtmlGenericControl
        Get
            Return gcSeikyuuUmuSpan
        End Get
        Set(ByVal value As HtmlGenericControl)
            gcSeikyuuUmuSpan = value
        End Set
    End Property
#End Region
#Region "見積作成日"
    ''' <summary>
    ''' 見積作成日
    ''' </summary>
    ''' <remarks></remarks>
    Private txtMitumoriDate As HtmlInputText
    ''' <summary>
    ''' 見積作成日
    ''' </summary>
    ''' <value></value>
    ''' <returns>見積作成日</returns>
    ''' <remarks></remarks>
    Public Property MitumoriDate() As HtmlInputText
        Get
            Return txtMitumoriDate
        End Get
        Set(ByVal value As HtmlInputText)
            txtMitumoriDate = value
        End Set
    End Property
#End Region
#Region "発注書確定FLG"
    ''' <summary>
    ''' 発注書確定FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private selHattyuusyoKakutei As HtmlSelect
    ''' <summary>
    ''' 発注書確定FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>発注書確定FLG</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakutei() As HtmlSelect
        Get
            Return selHattyuusyoKakutei
        End Get
        Set(ByVal value As HtmlSelect)
            selHattyuusyoKakutei = value
        End Set
    End Property
#End Region
#Region "発注書確定SPAN"
    ''' <summary>
    ''' 発注書確定SPAN
    ''' </summary>
    ''' <remarks></remarks>
    Private gcHattyuusyoKakuteiSpan As HtmlGenericControl
    ''' <summary>
    ''' 発注書確定SPAN
    ''' </summary>
    ''' <value></value>
    ''' <returns>発注書確定SPAN</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakuteiSpan() As HtmlGenericControl
        Get
            Return gcHattyuusyoKakuteiSpan
        End Get
        Set(ByVal value As HtmlGenericControl)
            gcHattyuusyoKakuteiSpan = value
        End Set
    End Property
#End Region
#Region "発注書確定FLGOld"
    ''' <summary>
    ''' 発注書確定FLGOld
    ''' </summary>
    ''' <remarks></remarks>
    Private selHattyuusyoKakuteiOld As HtmlInputHidden
    ''' <summary>
    ''' 発注書確定FLGOld
    ''' </summary>
    ''' <value></value>
    ''' <returns>発注書確定FLGOld</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakuteiOld() As HtmlInputHidden
        Get
            Return selHattyuusyoKakuteiOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            selHattyuusyoKakuteiOld = value
        End Set
    End Property
#End Region
#Region "発注書金額"
    ''' <summary>
    ''' 発注書金額
    ''' </summary>
    ''' <remarks></remarks>
    Private txtHattyuusyoKingaku As HtmlInputText
    ''' <summary>
    ''' 発注書金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>発注書金額</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKingaku() As HtmlInputText
        Get
            Return txtHattyuusyoKingaku
        End Get
        Set(ByVal value As HtmlInputText)
            txtHattyuusyoKingaku = value
        End Set
    End Property
#End Region
#Region "発注書金額Old"
    ''' <summary>
    ''' 発注書金額Old
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnHattyuusyoKingakuOld As HtmlInputHidden
    ''' <summary>
    ''' 発注書金額Old
    ''' </summary>
    ''' <value></value>
    ''' <returns>発注書金額Old</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKingakuOld() As HtmlInputHidden
        Get
            Return hdnHattyuusyoKingakuOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            hdnHattyuusyoKingakuOld = value
        End Set
    End Property
#End Region
#Region "発注書金額Old(発注書確定)"
    ''' <summary>
    ''' 発注書金額Old
    ''' </summary>
    ''' <remarks></remarks>
    Private txtHattyuusyoKingakuOld As HtmlInputHidden
    ''' <summary>
    ''' 発注書金額Old
    ''' </summary>
    ''' <value></value>
    ''' <returns>発注書金額Old</returns>
    ''' <remarks></remarks>
    Public Property HidHattyuusyoKingakuOld() As HtmlInputHidden
        Get
            Return txtHattyuusyoKingakuOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtHattyuusyoKingakuOld = value
        End Set
    End Property
#End Region
#Region "発注書確認日"
    ''' <summary>
    ''' 発注書確認日
    ''' </summary>
    ''' <remarks></remarks>
    Private txtHattyuusyoKakuninbi As HtmlInputText
    ''' <summary>
    ''' 発注書確認日
    ''' </summary>
    ''' <value></value>
    ''' <returns>発注書確認日</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakuninbi() As HtmlInputText
        Get
            Return txtHattyuusyoKakuninbi
        End Get
        Set(ByVal value As HtmlInputText)
            txtHattyuusyoKakuninbi = value
        End Set
    End Property
#End Region
#Region "金額変更フラグ"
    ''' <summary>
    ''' 金額変更フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private txtKingakuFlg As HtmlInputHidden
    ''' <summary>
    ''' 金額変更フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns>金額変更フラグ</returns>
    ''' <remarks></remarks>
    Public Property KingakuFlg() As HtmlInputHidden
        Get
            Return txtKingakuFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtKingakuFlg = value
        End Set
    End Property
#End Region
#Region "売上状況（商品３のみ）"
    ''' <summary>
    ''' 売上状況 uriageJyoukyou
    ''' </summary>
    ''' <remarks></remarks>
    Private txtUriageJyoukyou As HtmlInputHidden
    ''' <summary>
    ''' 売上状況（商品３のみ）
    ''' </summary>
    ''' <value></value>
    ''' <returns>売上状況</returns>
    ''' <remarks></remarks>
    Public Property UriageJyoukyou() As HtmlInputHidden
        Get
            Return txtUriageJyoukyou
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtUriageJyoukyou = value
        End Set
    End Property
#End Region
#Region "売上計上フラグ"
    ''' <summary>
    ''' 売上計上フラグ uriageKeijyouFlg
    ''' </summary>
    ''' <remarks></remarks>
    Private txtUriageKeijyouFlg As HtmlInputHidden
    ''' <summary>
    ''' 売上計上フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns>売上計上フラグ</returns>
    ''' <remarks></remarks>
    Public Property UriageKeijyouFlg() As HtmlInputHidden
        Get
            Return txtUriageKeijyouFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtUriageKeijyouFlg = value
        End Set
    End Property
#End Region
#Region "売上計上日"
    ''' <summary>
    ''' 売上計上日 uriageKeijyouBi
    ''' </summary>
    ''' <remarks></remarks>
    Private txtUriageKeijyouBi As HtmlInputHidden
    ''' <summary>
    ''' 売上計上日
    ''' </summary>
    ''' <value></value>
    ''' <returns>売上計上日</returns>
    ''' <remarks></remarks>
    Public Property UriageKeijyouBi() As HtmlInputHidden
        Get
            Return txtUriageKeijyouBi
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtUriageKeijyouBi = value
        End Set
    End Property
#End Region
#Region "備考"
    ''' <summary>
    ''' 備考 bikou
    ''' </summary>
    ''' <remarks></remarks>
    Private txtBikou As HtmlInputHidden
    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <value></value>
    ''' <returns>備考</returns>
    ''' <remarks></remarks>
    Public Property Bikou() As HtmlInputHidden
        Get
            Return txtBikou
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtBikou = value
        End Set
    End Property
#End Region
#Region "一括入金FLG"
    ''' <summary>
    ''' 一括入金FLG ikkatuNyuukinFlg
    ''' </summary>
    ''' <remarks></remarks>
    Private txtIkkatuNyuukinFlg As HtmlInputHidden
    ''' <summary>
    ''' 一括入金FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>一括入金FLG</returns>
    ''' <remarks></remarks>
    Public Property IkkatuNyuukinFlg() As HtmlInputHidden
        Get
            Return txtIkkatuNyuukinFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtIkkatuNyuukinFlg = value
        End Set
    End Property
#End Region

#Region "請求先仕入先リンク"
    ''' <summary>
    ''' 請求先仕入先リンク
    ''' </summary>
    ''' <remarks></remarks>
    Private SeikyuuSiireLinkCtrl As SeikyuuSiireLinkCtrl
    ''' <summary>
    ''' 請求先仕入先リンク
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求先仕入先リンク</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSiireLink() As SeikyuuSiireLinkCtrl
        Get
            Return SeikyuuSiireLinkCtrl
        End Get
        Set(ByVal value As SeikyuuSiireLinkCtrl)
            SeikyuuSiireLinkCtrl = value
        End Set
    End Property
#End Region
#Region "特別対応ツールチップ"
    ''' <summary>
    ''' 特別対応ツールチップ
    ''' </summary>
    ''' <remarks></remarks>
    Private TokubetuTaiouToolTipCtrl As TokubetuTaiouToolTipCtrl
    ''' <summary>
    ''' 特別対応ツールチップ
    ''' </summary>
    ''' <value></value>
    ''' <returns>特別対応ツールチップ</returns>
    ''' <remarks></remarks>
    Public Property TokubetuTaiouToolTip() As TokubetuTaiouToolTipCtrl
        Get
            Return TokubetuTaiouToolTipCtrl
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            TokubetuTaiouToolTipCtrl = value
        End Set
    End Property
#End Region
#Region "更新日時"
    ''' <summary>
    ''' 更新日時 UpdDatetime
    ''' </summary>
    ''' <remarks></remarks>
    Private txtUpdDatetime As HtmlInputHidden
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns>更新日時</returns>
    ''' <remarks></remarks>
    Public Property UpdDatetime() As HtmlInputHidden
        Get
            Return txtUpdDatetime
        End Get
        Set(ByVal value As HtmlInputHidden)
            txtUpdDatetime = value
        End Set
    End Property
#End Region
#Region "商品請求先コード"
    ''' <summary>
    ''' 商品請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private hidSyouhinSeikyuuSakiCd As HtmlInputHidden
    ''' <summary>
    ''' 商品請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品請求先コード</returns>
    ''' <remarks></remarks>
    Public Property SyouhinSeikyuuSakiCd() As HtmlInputHidden
        Get
            Return hidSyouhinSeikyuuSakiCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            hidSyouhinSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "承諾書金額変更可否FLG"
    ''' <summary>
    ''' 承諾書金額変更可否FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private hidSyoudakuHenkouKahi As HtmlInputHidden
    ''' <summary>
    ''' 承諾書金額変更可否FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>承諾書金額変更可否FLG</returns>
    ''' <remarks></remarks>
    Public Property SyoudakuHenkouKahi() As HtmlInputHidden
        Get
            Return hidSyoudakuHenkouKahi
        End Get
        Set(ByVal value As HtmlInputHidden)
            hidSyoudakuHenkouKahi = value
        End Set
    End Property
#End Region

#Region "工務店請求税抜金額変更可否FLG"
    ''' <summary>
    ''' 工務店請求税抜金額変更可否FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private hidKoumutenHenkouKahi As HtmlInputHidden
    ''' <summary>
    ''' 工務店請求税抜金額変更可否FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>工務店請求税抜金額変更可否FLG</returns>
    ''' <remarks></remarks>
    Public Property KoumutenHenkouKahi() As HtmlInputHidden
        Get
            Return hidKoumutenHenkouKahi
        End Get
        Set(ByVal value As HtmlInputHidden)
            hidKoumutenHenkouKahi = value
        End Set
    End Property
#End Region

#Region "実請求税抜金額変更可否FLG"
    ''' <summary>
    ''' 実請求税抜金額変更可否FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private hidJituGakuHenkouKahi As HtmlInputHidden
    ''' <summary>
    ''' 実請求税抜金額変更可否FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>実請求税抜金額変更可否FLG</returns>
    ''' <remarks></remarks>
    Public Property JituGakuHenkouKahi() As HtmlInputHidden
        Get
            Return hidJituGakuHenkouKahi
        End Get
        Set(ByVal value As HtmlInputHidden)
            hidJituGakuHenkouKahi = value
        End Set
    End Property
#End Region

End Class
