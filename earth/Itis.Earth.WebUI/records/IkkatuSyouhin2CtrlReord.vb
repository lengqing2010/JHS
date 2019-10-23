Imports System.Web.UI.Control
Imports System.Web.UI.HtmlControls
''' <summary>
''' 商品２情報のコントロール参照用レコードクラス
''' </summary>
''' <remarks>一括変更画面用</remarks>
Public Class IkkatuSyouhin2CtrlReord

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
#Region "加盟店コード"
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnKameitenCd As HiddenField
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>加盟店コード</returns>
    ''' <remarks></remarks>
    Public Property KameitenCd() As HiddenField
        Get
            Return hdnKameitenCd
        End Get
        Set(ByVal value As HiddenField)
            hdnKameitenCd = value
        End Set
    End Property
#End Region
#Region "調査会社コード"
    ''' <summary>
    ''' 調査会社コード
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnTysKaisyaCd As HiddenField
    ''' <summary>
    ''' 調査会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査会社コード</returns>
    ''' <remarks></remarks>
    Public Property TysKaisyaCd() As HiddenField
        Get
            Return hdnTysKaisyaCd
        End Get
        Set(ByVal value As HiddenField)
            hdnTysKaisyaCd = value
        End Set
    End Property
#End Region
#Region "調査会社事業所コード"
    ''' <summary>
    ''' 調査会社事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnTysKaisyaJigyousyoCd As HiddenField
    ''' <summary>
    ''' 調査会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査会社事業所コード</returns>
    ''' <remarks></remarks>
    Public Property TysKaisyaJigyousyoCd() As HiddenField
        Get
            Return hdnTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As HiddenField)
            hdnTysKaisyaJigyousyoCd = value
        End Set
    End Property

#End Region
#Region "系列コード"
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnKeiretuCd As HiddenField
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>系列コード</returns>
    ''' <remarks></remarks>
    Public Property KeiretuCd() As HiddenField
        Get
            Return hdnKeiretuCd
        End Get
        Set(ByVal value As HiddenField)
            hdnKeiretuCd = value
        End Set
    End Property
#End Region
#Region "調査請求先コード"
    ''' <summary>
    ''' 調査請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnTysSeikyuuSaki As HiddenField
    ''' <summary>
    ''' 調査請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査請求先コード</returns>
    ''' <remarks></remarks>
    Public Property TysSeikyuuSaki() As HiddenField
        Get
            Return hdnTysSeikyuuSaki
        End Get
        Set(ByVal value As HiddenField)
            hdnTysSeikyuuSaki = value
        End Set
    End Property
#End Region
#Region "請求先コード"
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnSeikyuuSakiCd As HiddenField
    ''' <summary>
    ''' 調査請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>調査請求先コード</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuSakiCd() As HiddenField
        Get
            Return hdnSeikyuuSakiCd
        End Get
        Set(ByVal value As HiddenField)
            hdnSeikyuuSakiCd = value
        End Set
    End Property
#End Region
#Region "加盟店取消"
    ''' <summary>
    ''' 加盟店取消
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnTorikeshi As HiddenField
    ''' <summary>
    ''' 加盟店取消
    ''' </summary>
    ''' <value></value>
    ''' <returns>加盟店取消</returns>
    ''' <remarks></remarks>
    Public Property Torikeshi() As HiddenField
        Get
            Return hdnTorikeshi
        End Get
        Set(ByVal value As HiddenField)
            hdnTorikeshi = value
        End Set
    End Property
#End Region
#Region "商品1の売上年月日"
    ''' <summary>
    ''' 商品1の売上年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnUriDateItem1 As HiddenField
    ''' <summary>
    ''' 商品1の売上年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品1の売上年月日</returns>
    ''' <remarks></remarks>
    Public Property UriDateItem1() As HiddenField
        Get
            Return hdnUriDateItem1
        End Get
        Set(ByVal value As HiddenField)
            hdnUriDateItem1 = value
        End Set
    End Property
#End Region
#Region "画面ロード時のDB値"
    ''' <summary>
    ''' 画面ロード時のDB値
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnDbValue As HiddenField
    ''' <summary>
    ''' 画面ロード時のDB値
    ''' </summary>
    ''' <value></value>
    ''' <returns>画面ロード時のDB値</returns>
    ''' <remarks></remarks>
    Public Property DbValue() As HiddenField
        Get
            Return hdnDbValue
        End Get
        Set(ByVal value As HiddenField)
            hdnDbValue = value
        End Set
    End Property
#End Region
#Region "計算処理後の画面値"
    ''' <summary>
    ''' 計算処理後の画面値
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnChgValue As HiddenField
    ''' <summary>
    ''' 計算処理後の画面値
    ''' </summary>
    ''' <value></value>
    ''' <returns>計算処理後の画面値</returns>
    ''' <remarks></remarks>
    Public Property ChgValue() As HiddenField
        Get
            Return hdnChgValue
        End Get
        Set(ByVal value As HiddenField)
            hdnChgValue = value
        End Set
    End Property
#End Region
#Region "画面ロード時のDB金額"
    ''' <summary>
    ''' 画面ロード時のDB値
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnDbKingaku As HiddenField
    ''' <summary>
    ''' 画面ロード時のDB値
    ''' </summary>
    ''' <value></value>
    ''' <returns>画面ロード時のDB値</returns>
    ''' <remarks></remarks>
    Public Property DbKingaku() As HiddenField
        Get
            Return hdnDbKingaku
        End Get
        Set(ByVal value As HiddenField)
            hdnDbKingaku = value
        End Set
    End Property
#End Region
#Region "変更確認用の画面金額"
    ''' <summary>
    ''' 変更確認用の画面金額
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnChgKingaku As HiddenField
    ''' <summary>
    ''' 変更確認用の画面金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>変更確認用の画面金額</returns>
    ''' <remarks></remarks>
    Public Property ChgKingaku() As HiddenField
        Get
            Return hdnChgKingaku
        End Get
        Set(ByVal value As HiddenField)
            hdnChgKingaku = value
        End Set
    End Property
#End Region
#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnKbn As HiddenField
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>区分</returns>
    ''' <remarks></remarks>
    Public Property Kbn() As HiddenField
        Get
            Return hdnKbn
        End Get
        Set(ByVal value As HiddenField)
            hdnKbn = value
        End Set
    End Property
#End Region
#Region "保証書NO"
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnHosyousyoNo As HiddenField
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <value></value>
    ''' <returns>保証書NO</returns>
    ''' <remarks></remarks>
    Public Property HosyousyoNo() As HiddenField
        Get
            Return hdnHosyousyoNo
        End Get
        Set(ByVal value As HiddenField)
            hdnHosyousyoNo = value
        End Set
    End Property
#End Region
#Region "分類コード"
    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnBunruiCd As HiddenField
    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>分類コード</returns>
    ''' <remarks></remarks>
    Public Property BunruiCd() As HiddenField
        Get
            Return hdnBunruiCd
        End Get
        Set(ByVal value As HiddenField)
            hdnBunruiCd = value
        End Set
    End Property
#End Region
#Region "画面表示NO"
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnGamenHyoujiNo As HiddenField
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GamenHyoujiNo() As HiddenField
        Get
            Return hdnGamenHyoujiNo
        End Get
        Set(ByVal value As HiddenField)
            hdnGamenHyoujiNo = value
        End Set
    End Property
#End Region
#Region "税区分"
    ''' <summary>
    ''' 税区分
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnZeiKbn As HiddenField
    ''' <summary>
    ''' 税区分
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ZeiKbn() As HiddenField
        Get
            Return hdnZeiKbn
        End Get
        Set(ByVal value As HiddenField)
            hdnZeiKbn = value
        End Set
    End Property
#End Region
#Region "売上年月日"
    ''' <summary>
    ''' 売上年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnUriDate As HiddenField
    ''' <summary>
    ''' 売上年月日
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UriDate() As HiddenField
        Get
            Return hdnUriDate
        End Get
        Set(ByVal value As HiddenField)
            hdnUriDate = value
        End Set
    End Property
#End Region
#Region "発注書確定フラグ"
    ''' <summary>
    ''' 発注書確定フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnHattyuusyoKakuteiFlg As HiddenField
    ''' <summary>
    ''' 発注書確定フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKakuteiFlg() As HiddenField
        Get
            Return hdnHattyuusyoKakuteiFlg
        End Get
        Set(ByVal value As HiddenField)
            hdnHattyuusyoKakuteiFlg = value
        End Set
    End Property
#End Region
#Region "発注書金額"
    ''' <summary>
    ''' 発注書金額
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnHattyuusyoGaku As HiddenField
    ''' <summary>
    ''' 発注書金額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoGaku() As HiddenField
        Get
            Return hdnHattyuusyoGaku
        End Get
        Set(ByVal value As HiddenField)
            hdnHattyuusyoGaku = value
        End Set
    End Property
#End Region
#Region "発注書金額Old"
    ''' <summary>
    ''' 発注書金額Old
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnHattyuusyoGakuOld As HiddenField
    ''' <summary>
    ''' 発注書金額Old
    ''' </summary>
    ''' <value></value>
    ''' <returns>発注書金額Old</returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoGakuOld() As HiddenField
        Get
            Return hdnHattyuusyoGakuOld
        End Get
        Set(ByVal value As HiddenField)
            hdnHattyuusyoGakuOld = value
        End Set
    End Property
#End Region
#Region "金額の先行変更判断フラグ"
    ''' <summary>
    ''' 金額の先行変更判断フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnAutoKingakuFlg As HiddenField
    ''' <summary>
    ''' 金額の先行変更判断フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AutoKingakuFlg() As HiddenField
        Get
            Return hdnAutoKingakuFlg
        End Get
        Set(ByVal value As HiddenField)
            hdnAutoKingakuFlg = value
        End Set
    End Property
#End Region
#Region "金額の更判断フラグ"
    ''' <summary>
    ''' 金額の更判断フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnManualKingakuFlg As HiddenField
    ''' <summary>
    ''' 金額の更判断フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ManualKingakuFlg() As HiddenField
        Get
            Return hdnManualKingakuFlg
        End Get
        Set(ByVal value As HiddenField)
            hdnManualKingakuFlg = value
        End Set
    End Property
#End Region
#Region "金額が両方変更されたか判断フラグ"
    ''' <summary>
    ''' 金額が両方変更されたか判断フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnBothKingakuChgFlg As HiddenField
    ''' <summary>
    ''' 金額が両方変更されたか判断フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BothKingakuChgFlg() As HiddenField
        Get
            Return hdnBothKingakuChgFlg
        End Get
        Set(ByVal value As HiddenField)
            hdnBothKingakuChgFlg = value
        End Set
    End Property
#End Region
#Region "自動計算チェックボックス"
    ''' <summary>
    ''' 自動計算チェックボックス
    ''' </summary>
    ''' <remarks></remarks>
    Private chkAutoCalc As CheckBox
    ''' <summary>
    ''' 自動計算チェックボックス
    ''' </summary>
    ''' <value></value>
    ''' <returns>自動計算チェックボックス</returns>
    ''' <remarks></remarks>
    Public Property AutoCalc() As CheckBox
        Get
            Return chkAutoCalc
        End Get
        Set(ByVal value As CheckBox)
            chkAutoCalc = value
        End Set
    End Property
#End Region
#Region "自動計算SPAN"
    ''' <summary>
    ''' 自動計算SPAN
    ''' </summary>
    ''' <remarks></remarks>
    Private spanAutoCalc As HtmlGenericControl
    ''' <summary>
    ''' 自動計算SPAN
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AutoCalcSpan() As HtmlGenericControl
        Get
            Return spanAutoCalc
        End Get
        Set(ByVal value As HtmlGenericControl)
            spanAutoCalc = value
        End Set
    End Property
#End Region
#Region "画面表示NO"
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <remarks></remarks>
    Private txtNo As TextBox
    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property No() As TextBox
        Get
            Return txtNo
        End Get
        Set(ByVal value As TextBox)
            txtNo = value
        End Set
    End Property
#End Region
#Region "顧客番号"
    ''' <summary>
    ''' 顧客番号
    ''' </summary>
    ''' <remarks></remarks>
    Private txtKokyakuBangou As TextBox
    ''' <summary>
    ''' 顧客番号
    ''' </summary>
    ''' <value></value>
    ''' <returns>顧客番号</returns>
    ''' <remarks></remarks>
    Public Property KokyakuBangou() As TextBox
        Get
            Return txtKokyakuBangou
        End Get
        Set(ByVal value As TextBox)
            txtKokyakuBangou = value
        End Set
    End Property
#End Region
#Region "商品コンボ"
    ''' <summary>
    ''' 商品コンボ
    ''' </summary>
    ''' <remarks></remarks>
    Private drpSyouhin As DropDownList
    ''' <summary>
    ''' 商品コンボ
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品コンボ</returns>
    ''' <remarks></remarks>
    Public Property Syouhin() As DropDownList
        Get
            Return drpSyouhin
        End Get
        Set(ByVal value As DropDownList)
            drpSyouhin = value
        End Set
    End Property
#End Region
#Region "商品Hidden"
    ''' <summary>
    ''' 商品Hidden
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnSyouhin As HiddenField
    ''' <summary>
    ''' 商品Hidden
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品Hidden</returns>
    ''' <remarks></remarks>
    Public Property SyouhinBK() As HiddenField
        Get
            Return hdnSyouhin
        End Get
        Set(ByVal value As HiddenField)
            hdnSyouhin = value
        End Set
    End Property
#End Region
#Region "商品金額Hidden"
    ''' <summary>
    ''' 商品金額Hidden
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnSyouhinKingaku As HiddenField
    ''' <summary>
    ''' 商品金額Hidden
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品金額Hidden</returns>
    ''' <remarks></remarks>
    Public Property SyouhinKingakuBK() As HiddenField
        Get
            Return hdnSyouhinKingaku
        End Get
        Set(ByVal value As HiddenField)
            hdnSyouhinKingaku = value
        End Set
    End Property
#End Region
#Region "商品SPAN"
    ''' <summary>
    ''' 商品SPAN
    ''' </summary>
    ''' <remarks></remarks>
    Private spanSyouhin As HtmlGenericControl
    ''' <summary>
    ''' 商品SPAN
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SyouhinSpan() As HtmlGenericControl
        Get
            Return spanSyouhin
        End Get
        Set(ByVal value As HtmlGenericControl)
            spanSyouhin = value
        End Set
    End Property
#End Region
#Region "売上処理"
    ''' <summary>
    ''' 売上処理
    ''' </summary>
    ''' <remarks></remarks>
    Private txtUriage As TextBox
    ''' <summary>
    ''' 売上処理
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UriageSyori() As TextBox
        Get
            Return txtUriage
        End Get
        Set(ByVal value As TextBox)
            txtUriage = value
        End Set
    End Property
#End Region
#Region "工務店請求額"
    ''' <summary>
    ''' 工務店請求額
    ''' </summary>
    ''' <remarks></remarks>
    Private txtKoumuten As TextBox
    ''' <summary>
    ''' 工務店請求額
    ''' </summary>
    ''' <value></value>
    ''' <returns>工務店請求額</returns>
    ''' <remarks></remarks>
    Public Property KoumutenKingaku() As TextBox
        Get
            Return txtKoumuten
        End Get
        Set(ByVal value As TextBox)
            txtKoumuten = value
        End Set
    End Property
#End Region
#Region "工務店請求額Old"
    ''' <summary>
    ''' 工務店請求額Old
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnKoumuten As HiddenField
    ''' <summary>
    ''' 工務店請求額Old
    ''' </summary>
    ''' <value></value>
    ''' <returns>工務店請求額Old</returns>
    ''' <remarks></remarks>
    Public Property KoumutenKingakuOld() As HiddenField
        Get
            Return hdnKoumuten
        End Get
        Set(ByVal value As HiddenField)
            hdnKoumuten = value
        End Set
    End Property
#End Region
#Region "実請求金額"
    ''' <summary>
    ''' 実請求金額
    ''' </summary>
    ''' <remarks></remarks>
    Private txtJituSeikyuu As TextBox
    ''' <summary>
    ''' 実請求金額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property JituSeikyuuKingaku() As TextBox
        Get
            Return txtJituSeikyuu
        End Get
        Set(ByVal value As TextBox)
            txtJituSeikyuu = value
        End Set
    End Property
#End Region
#Region "実請求金額Old"
    ''' <summary>
    ''' 実請求金額Old
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnJituSeikyuu As HiddenField
    ''' <summary>
    ''' 実請求金額Old
    ''' </summary>
    ''' <value></value>
    ''' <returns>実請求金額Old</returns>
    ''' <remarks></remarks>
    Public Property JituSeikyuuKingakuOld() As HiddenField
        Get
            Return hdnJituSeikyuu
        End Get
        Set(ByVal value As HiddenField)
            hdnJituSeikyuu = value
        End Set
    End Property
#End Region
#Region "承諾書金額"
    ''' <summary>
    ''' 承諾書金額
    ''' </summary>
    ''' <remarks></remarks>
    Private txtSyoudakusyo As TextBox
    ''' <summary>
    ''' 承諾書金額
    ''' </summary>
    ''' <value></value>
    ''' <returns>承諾書金額</returns>
    ''' <remarks></remarks>
    Public Property SyoudakusyoKingaku() As TextBox
        Get
            Return txtSyoudakusyo
        End Get
        Set(ByVal value As TextBox)
            txtSyoudakusyo = value
        End Set
    End Property
#End Region
#Region "承諾書金額Old"
    ''' <summary>
    ''' 承諾書金額Old
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnSyoudakusyo As HiddenField
    ''' <summary>
    ''' 承諾書金額Old
    ''' </summary>
    ''' <value></value>
    ''' <returns>承諾書金額Old</returns>
    ''' <remarks></remarks>
    Public Property SyoudakusyoKingakuOld() As HiddenField
        Get
            Return hdnSyoudakusyo
        End Get
        Set(ByVal value As HiddenField)
            hdnSyoudakusyo = value
        End Set
    End Property
#End Region
#Region "請求有無"
    ''' <summary>
    ''' 請求有無
    ''' </summary>
    ''' <remarks></remarks>
    Private drpSeikyuu As DropDownList
    ''' <summary>
    ''' 請求有無
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SeikyuuUmu() As DropDownList
        Get
            Return drpSeikyuu
        End Get
        Set(ByVal value As DropDownList)
            drpSeikyuu = value
        End Set
    End Property
#End Region
#Region "請求有無SPAN"
    ''' <summary>
    ''' 請求有無SPAN
    ''' </summary>
    ''' <remarks></remarks>
    Private spanSeikyuu As HtmlGenericControl
    ''' <summary>
    ''' 請求有無SPAN
    ''' </summary>
    ''' <value></value>
    ''' <returns>請求有無SPAN</returns>
    ''' <remarks></remarks>
    Public Property SeikyuuUmuSpan() As HtmlGenericControl
        Get
            Return spanSeikyuu
        End Get
        Set(ByVal value As HtmlGenericControl)
            spanSeikyuu = value
        End Set
    End Property
#End Region
#Region "追加ボタン"
    ''' <summary>
    ''' 追加ボタン
    ''' </summary>
    ''' <remarks></remarks>
    Private btnAdd As HtmlInputButton
    ''' <summary>
    ''' 追加ボタン
    ''' </summary>
    ''' <value></value>
    ''' <returns>追加ボタン</returns>
    ''' <remarks></remarks>
    Public Property AddBtn() As HtmlInputButton
        Get
            Return btnAdd
        End Get
        Set(ByVal value As HtmlInputButton)
            btnAdd = value
        End Set
    End Property
#End Region
#Region "削除ボタン"
    ''' <summary>
    ''' 削除ボタン
    ''' </summary>
    ''' <remarks></remarks>
    Private btnDel As HtmlInputButton
    ''' <summary>
    ''' 削除ボタン
    ''' </summary>
    ''' <value></value>
    ''' <returns>削除ボタン</returns>
    ''' <remarks></remarks>
    Public Property DelBtn() As HtmlInputButton
        Get
            Return btnDel
        End Get
        Set(ByVal value As HtmlInputButton)
            btnDel = value
        End Set
    End Property
#End Region
#Region "登録日時"
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnAddDateTime As HiddenField
    ''' <summary>
    ''' 登録日時
    ''' </summary>
    ''' <value></value>
    ''' <returns>登録日時</returns>
    ''' <remarks></remarks>
    Public Property AddDateTime() As HiddenField
        Get
            Return hdnAddDateTime
        End Get
        Set(ByVal value As HiddenField)
            hdnAddDateTime = value
        End Set
    End Property
#End Region
#Region "更新日時"
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnUpdDateTime As HiddenField
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns>更新日時</returns>
    ''' <remarks></remarks>
    Public Property UpdDateTime() As HiddenField
        Get
            Return hdnUpdDateTime
        End Get
        Set(ByVal value As HiddenField)
            hdnUpdDateTime = value
        End Set
    End Property
#End Region
#Region "行表示状態"
    ''' <summary>
    ''' 行表示状態
    ''' </summary>
    ''' <remarks></remarks>
    Private hdnRowDisplay As HiddenField
    ''' <summary>
    ''' 行表示状態
    ''' </summary>
    ''' <value></value>
    ''' <returns>行表示状態</returns>
    ''' <remarks></remarks>
    Public Property RowDisplay() As HiddenField
        Get
            Return hdnRowDisplay
        End Get
        Set(ByVal value As HiddenField)
            hdnRowDisplay = value
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

End Class
