
Partial Public Class IkkatuHenkouTysSyouhin1RecordCtrl
    Inherits System.Web.UI.UserControl

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private cl As New CommonLogic
    Private masterAjaxSM As New ScriptManager
    Private dicKey As New IkkatuHenkouTysSyouhinLogic

#Region "ユーザーコントロールへの外部からのアクセス用Getter/Setter"
    ''' <summary>
    ''' 外部からのアクセス用 for TrTysSyouhin_1_1(TR１行目)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTrTysSyouhin_1_1() As HtmlTableRow
        Get
            Return TrTysSyouhin_1_1
        End Get
        Set(ByVal value As HtmlTableRow)
            TrTysSyouhin_1_1 = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TrTysSyouhin_1_2(TR２行目)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTrTysSyouhin_1_2() As HtmlTableRow
        Get
            Return TrTysSyouhin_1_2
        End Get
        Set(ByVal value As HtmlTableRow)
            TrTysSyouhin_1_2 = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for CheckAutoCalc(計算チェックボックス)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccCheckAutoCalc() As CheckBox
        Get
            Return CheckAutoCalc
        End Get
        Set(ByVal value As CheckBox)
            CheckAutoCalc = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenDbValue(DB値保存用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenDbValue() As HiddenField
        Get
            Return HiddenDbValue
        End Get
        Set(ByVal value As HiddenField)
            HiddenDbValue = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenChgValue(計算処理用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenChgValue() As HiddenField
        Get
            Return HiddenChgValue
        End Get
        Set(ByVal value As HiddenField)
            HiddenChgValue = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenDbKingaku(DBの金額値保存用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenDbKingaku() As HiddenField
        Get
            Return HiddenDbKingaku
        End Get
        Set(ByVal value As HiddenField)
            HiddenDbKingaku = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenChgKingaku(金額の変更確認用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenChgKingaku() As HiddenField
        Get
            Return HiddenChgKingaku
        End Get
        Set(ByVal value As HiddenField)
            HiddenChgKingaku = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenKbn(区分)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenKbn() As HiddenField
        Get
            Return HiddenKbn
        End Get
        Set(ByVal value As HiddenField)
            HiddenKbn = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenHosyousyoNo(保証書NO)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenHosyousyoNo() As HiddenField
        Get
            Return HiddenHosyousyoNo
        End Get
        Set(ByVal value As HiddenField)
            HiddenHosyousyoNo = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenBunruiCd(分類コード)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenBunruiCd() As HiddenField
        Get
            Return HiddenBunruiCd
        End Get
        Set(ByVal value As HiddenField)
            HiddenBunruiCd = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenGamenHyoujiNo(画面表示NO)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenGamenHyoujiNo() As HiddenField
        Get
            Return HiddenGamenHyoujiNo
        End Get
        Set(ByVal value As HiddenField)
            HiddenGamenHyoujiNo = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenSyouhinKbn(商品区分)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenSyouhinKbn() As HiddenField
        Get
            Return HiddenSyouhinKbn
        End Get
        Set(ByVal value As HiddenField)
            HiddenSyouhinKbn = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenTorikeshi(加盟店取消)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenTorikeshi() As HiddenField
        Get
            Return HiddenTorikeshi
        End Get
        Set(ByVal value As HiddenField)
            HiddenTorikeshi = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenKeiretuCd(系列コード)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenKeiretuCd() As HiddenField
        Get
            Return HiddenKeiretuCd
        End Get
        Set(ByVal value As HiddenField)
            HiddenKeiretuCd = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenEigyousyoCd(営業所コード)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenEigyousyoCd() As HiddenField
        Get
            Return HiddenEigyousyoCd
        End Get
        Set(ByVal value As HiddenField)
            HiddenEigyousyoCd = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenTysKaisyaCd(調査会社コード)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenTysKaisyaCd() As HiddenField
        Get
            Return HiddenTysKaisyaCd
        End Get
        Set(ByVal value As HiddenField)
            HiddenTysKaisyaCd = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenTysKaisyaJigyousyoCd(調査会社事業所コード)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenTysKaisyaJigyousyoCd() As HiddenField
        Get
            Return HiddenTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As HiddenField)
            HiddenTysKaisyaJigyousyoCd = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenTysSeikyuuSaki(調査請求先)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenTysSeikyuuSaki() As HiddenField
        Get
            Return HiddenTysSeikyuuSaki
        End Get
        Set(ByVal value As HiddenField)
            HiddenTysSeikyuuSaki = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenSeikyuuSakiCd(請求先コード)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenSeikyuuSakiCd() As HiddenField
        Get
            Return HiddenSeikyuuSakiCd
        End Get
        Set(ByVal value As HiddenField)
            HiddenSeikyuuSakiCd = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenTysGaiyou(調査概要)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenTysGaiyou() As HiddenField
        Get
            Return HiddenTysGaiyou
        End Get
        Set(ByVal value As HiddenField)
            HiddenTysGaiyou = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenKakakuSetteiBasyo(価格設定場所)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenKakakuSetteiBasyo() As HiddenField
        Get
            Return HiddenKakakuSetteiBasyo
        End Get
        Set(ByVal value As HiddenField)
            HiddenKakakuSetteiBasyo = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenIraiTousuu(依頼棟数)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenIraiTousuu() As HiddenField
        Get
            Return HiddenIraiTousuu
        End Get
        Set(ByVal value As HiddenField)
            HiddenIraiTousuu = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenTatemonoYoutoNo(建物用途)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenTatemonoYoutoNo() As HiddenField
        Get
            Return HiddenTatemonoYoutoNo
        End Get
        Set(ByVal value As HiddenField)
            HiddenTatemonoYoutoNo = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenStatusIf(進捗ステータス)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenStatusIf() As HiddenField
        Get
            Return HiddenStatusIf
        End Get
        Set(ByVal value As HiddenField)
            HiddenStatusIf = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenHattyuusyoGaku(発注書金額)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenHattyuusyoGaku() As HiddenField
        Get
            Return HiddenHattyuusyoGaku
        End Get
        Set(ByVal value As HiddenField)
            HiddenHattyuusyoGaku = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenAutoKingakuFlg(金額の先行変更判断フラグ格納用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenAutoKingakuFlg() As HiddenField
        Get
            Return HiddenAutoKingakuFlg
        End Get
        Set(ByVal value As HiddenField)
            HiddenAutoKingakuFlg = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenManualKingakuFlg(金額の更判断フラグ格納用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenManualKingakuFlg() As HiddenField
        Get
            Return HiddenManualKingakuFlg
        End Get
        Set(ByVal value As HiddenField)
            HiddenManualKingakuFlg = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenBothChgFlg(金額が両方変更されたか判断フラグ格納用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenBothKingakuChgFlg() As HiddenField
        Get
            Return HiddenBothKingakuChgFlg
        End Get
        Set(ByVal value As HiddenField)
            HiddenBothKingakuChgFlg = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenAddDatetimeJiban(地盤テーブル登録日時)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenAddDatetimeJiban() As HiddenField
        Get
            Return HiddenAddDatetimeJiban
        End Get
        Set(ByVal value As HiddenField)
            HiddenAddDatetimeJiban = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenUpdDatetimeJiban(地盤テーブル更新日時)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenUpdDatetimeJiban() As HiddenField
        Get
            Return HiddenUpdDatetimeJiban
        End Get
        Set(ByVal value As HiddenField)
            HiddenUpdDatetimeJiban = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextKokyakuBangou(顧客番号)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextKokyakuBangou() As TextBox
        Get
            Return TextKokyakuBangou
        End Get
        Set(ByVal value As TextBox)
            TextKokyakuBangou = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextTysHouhouCode(調査方法コード)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextTysHouhouCode() As TextBox
        Get
            Return TextTysHouhouCode
        End Get
        Set(ByVal value As TextBox)
            TextTysHouhouCode = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for SelectTysHouhou(調査方法コンボ)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSelectTysHouhou() As DropDownList
        Get
            Return SelectTysHouhou
        End Get
        Set(ByVal value As DropDownList)
            SelectTysHouhou = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenTysHouhouCode(調査方法Hidden)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenTysHouhouCode() As HiddenField
        Get
            Return HiddenTysHouhouCode
        End Get
        Set(ByVal value As HiddenField)
            HiddenTysHouhouCode = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextTyousakaisyaCode(調査会社コード)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextTyousakaisyaCode() As TextBox
        Get
            Return TextTyousakaisyaCode
        End Get
        Set(ByVal value As TextBox)
            TextTyousakaisyaCode = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextTyousakaisyaName(調査会社名)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextTyousakaisyaName() As TextBox
        Get
            Return TextTyousakaisyaName
        End Get
        Set(ByVal value As TextBox)
            TextTyousakaisyaName = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextKameitenCode(加盟店コード)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextKameitenCode() As TextBox
        Get
            Return TextKameitenCode
        End Get
        Set(ByVal value As TextBox)
            TextKameitenCode = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextKameitenName(加盟店名)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextKameitenName() As TextBox
        Get
            Return TextKameitenName
        End Get
        Set(ByVal value As TextBox)
            TextKameitenName = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextSesyuName(施主名)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSesyuName() As TextBox
        Get
            Return TextSesyuName
        End Get
        Set(ByVal value As TextBox)
            TextSesyuName = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for SelectSyouhin1(商品1名コンボ)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSelectSyouhin1() As DropDownList
        Get
            Return SelectSyouhin1
        End Get
        Set(ByVal value As DropDownList)
            SelectSyouhin1 = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenSyouhin1Code(商品1Hidden)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenSyouhin1Code() As HiddenField
        Get
            Return HiddenSyouhin1Code
        End Get
        Set(ByVal value As HiddenField)
            HiddenSyouhin1Code = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextUriageSyori(売上処理)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextUriageSyori() As TextBox
        Get
            Return TextUriageSyori
        End Get
        Set(ByVal value As TextBox)
            TextUriageSyori = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextKoumutenKingaku(工務店請求額)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextKoumutenKingaku() As TextBox
        Get
            Return TextKoumutenKingaku
        End Get
        Set(ByVal value As TextBox)
            TextKoumutenKingaku = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextJituSeikyuuKingaku(実請求金額)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextJituSeikyuuKingaku() As TextBox
        Get
            Return TextJituSeikyuuKingaku
        End Get
        Set(ByVal value As TextBox)
            TextJituSeikyuuKingaku = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextSyoudakusyoKingaku(承諾書金額)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSyoudakusyoKingaku() As TextBox
        Get
            Return TextSyoudakusyoKingaku
        End Get
        Set(ByVal value As TextBox)
            TextSyoudakusyoKingaku = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for SelectSeikyuuUmu(請求有無コンボ)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSelectSeikyuuUmu() As DropDownList
        Get
            Return SelectSeikyuuUmu
        End Get
        Set(ByVal value As DropDownList)
            SelectSeikyuuUmu = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenSdsHenkouKahi(承諾書金額変更可否フラグ)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSdsHenkouKahi() As HiddenField
        Get
            Return HiddenSdsHenkouKahi
        End Get
        Set(ByVal value As HiddenField)
            HiddenSdsHenkouKahi = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenKmtnHenkouKahi(工務店請求金額変更可否フラグ)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccKmtnHenkouKahi() As HiddenField
        Get
            Return HiddenKmtnHenkouKahi
        End Get
        Set(ByVal value As HiddenField)
            HiddenKmtnHenkouKahi = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenJituGakuHenkouKahi(実請求税抜き金額変更可否フラグ)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccJituGakuHenkouKahi() As HiddenField
        Get
            Return HiddenJituGakuHenkouKahi
        End Get
        Set(ByVal value As HiddenField)
            HiddenJituGakuHenkouKahi = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for ucTokubetuTaiouToolTipCtrl(特別対応ツールチップ)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip() As TokubetuTaiouToolTipCtrl
        Get
            Return ucTokubetuTaiouToolTipCtrl
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            ucTokubetuTaiouToolTipCtrl = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' NG調査会社設定警告イベントハンドラー
    ''' </summary>
    ''' <remarks></remarks>
    Public Event WarningTysKaisya As EventHandler
    ''' <summary>
    ''' NG調査会社設定警告イベント
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overridable Sub OnWarningTysKaisya(ByVal e As EventArgs)
        RaiseEvent WarningTysKaisya(Me, e)
    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        '****************************************************************************
        ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
        '****************************************************************************
        setDispAction()
    End Sub

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        'ポストバック以外のみ
        If Not IsPostBack Then
            '商品の活性制御
            enableItem1()
        End If
    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()
        Dim jBn As New Jiban '地盤画面クラス

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript関連セット
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim onFocusScript As String = "removeFig(this);"
        Dim checkKingaku As String = "checkKingaku(this);"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"
        Dim onChgScript As String = "rowChgItem1('" & Me.ClientID & "');"
        Dim onChgKingaku As String = "setKingaku(this, '" & Me.HiddenAutoKingakuFlg.ClientID & "', '" & Me.HiddenManualKingakuFlg.ClientID & "', '" & Me.HiddenBothKingakuChgFlg.ClientID & "');"
        Dim setSyouhinScript As String = "SetSyouhin1('" & Me.SelectSyouhin1.ClientID & "', '" & Me.HiddenSyouhin1Code.ClientID & "');"
        Dim setSyouhinOldScript As String = "SetSyouhin1('" & Me.HiddenSyouhin1Code.ClientID & "', '" & Me.SelectSyouhin1.ClientID & "');"
        Dim ChkTokubetuTaiouScript As String = "ChkTokubetuTaiou('" & Me.ucTokubetuTaiouToolTipCtrl.AccDisplayCd.ClientID & "', '')"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 金額系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '************
        '* 画面上部
        '************
        '調査方法
        jBn.SetPullCdScriptSrc(Me.TextTysHouhouCode, Me.SelectTysHouhou)

        '調査方法コード
        Me.TextTysHouhouCode.Attributes("onchange") = onChgScript
        '調査方法コンボ
        Me.SelectTysHouhou.Attributes("onchange") &= onChgScript

        '調査会社コード
        Me.TextTyousakaisyaCode.Attributes("onchange") = onChgScript
        '調査会社名
        Me.TextTyousakaisyaName.Attributes("onchange") = onChgScript

        '商品1コンボ
        Me.SelectSyouhin1.Attributes("onchange") &= "if(" & ChkTokubetuTaiouScript & "){" & onChgScript & setSyouhinScript & "}else{" & setSyouhinOldScript & "}"

        '工務店請求金額
        Me.TextKoumutenKingaku.Attributes("onfocus") = onFocusScript
        Me.TextKoumutenKingaku.Attributes("onblur") = checkKingaku
        Me.TextKoumutenKingaku.Attributes("onkeydown") = disabledOnkeydown
        Me.TextKoumutenKingaku.Attributes("onchange") = onChgKingaku

        '実請求金額
        Me.TextJituSeikyuuKingaku.Attributes("onfocus") = onFocusScript
        Me.TextJituSeikyuuKingaku.Attributes("onblur") = checkKingaku
        Me.TextJituSeikyuuKingaku.Attributes("onkeydown") = disabledOnkeydown
        Me.TextJituSeikyuuKingaku.Attributes("onchange") = onChgKingaku

        '承諾書金額
        Me.TextSyoudakusyoKingaku.Attributes("onfocus") = onFocusScript
        Me.TextSyoudakusyoKingaku.Attributes("onblur") = checkKingaku
        Me.TextSyoudakusyoKingaku.Attributes("onkeydown") = disabledOnkeydown

        '請求有無
        Me.SelectSeikyuuUmu.Attributes("onchange") = onChgScript

        '*****************************
        '* コードおよびポップアップボタン
        '*****************************
        'その他情報.調査会社
        Dim scriptSb As New StringBuilder()
        scriptSb.Append("if(checkTempValueForOnBlur(this)){")
        scriptSb.Append("if(objEBI('" & Me.TextTyousakaisyaCode.ClientID & "').value == ''){")
        scriptSb.Append("objEBI('" & Me.TextTyousakaisyaName.ClientID & "').value = '';")
        scriptSb.Append("objEBI('" & Me.HiddenTyousaKaishaCdOld.ClientID & "').value = '';}")
        scriptSb.Append("objEBI('" & Me.TextTyousakaisyaCode.ClientID & "').style.color = 'black';")
        scriptSb.Append("objEBI('" & Me.TextTyousakaisyaName.ClientID & "').style.color = 'black';")
        scriptSb.Append("}else{checkNumber(this);}")

        Me.TextTyousakaisyaCode.Attributes("onblur") = scriptSb.ToString
        Me.TextTyousakaisyaCode.Attributes("onfocus") = "setTempValueForOnBlur(this);"

        scriptSb = New StringBuilder
        scriptSb.Append("objEBI('" & Me.TextTyousakaisyaCode.ClientID & "').style.color = 'black';")
        scriptSb.Append("objEBI('" & Me.TextTyousakaisyaName.ClientID & "').style.color = 'black';")
        scriptSb.Append("if(objEBI('" & Me.TextTyousakaisyaCode.ClientID & "').value == '' || gintCallPopUp == 1){")
        scriptSb.Append("          gintCallPopUp = 0;")
        scriptSb.Append("          callSearch('" & Me.TextTyousakaisyaCode.ClientID & "','" & UrlConst.SEARCH_TYOUSAKAISYA & "','" & _
                                                                Me.TextTyousakaisyaCode.ClientID & EarthConst.SEP_STRING & _
                                                                Me.TextTyousakaisyaName.ClientID & _
                                                                "','" & Me.ButtonSearchTyousakaisya.ClientID & "');")
        scriptSb.Append("}else{")
        scriptSb.Append("getTysKaisyaName(this" & ", " & _
                                            "objEBI('" & Me.TextTyousakaisyaCode.ClientID & "').value" & ", " & _
                                            "objEBI('" & Me.TextTyousakaisyaName.ClientID & "')" & ", " & _
                                            "objEBI('" & Me.HiddenTyousaKaishaCdOld.ClientID & "'));")
        scriptSb.Append("}")

        Me.ButtonSearchTyousakaisya.Attributes("onclick") = scriptSb.ToString

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)
    End Sub

    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <param name="Item1Rec">邸別請求レコード（商品1）</param>
    ''' <remarks></remarks>
    Public Sub setCtrlFromJibanRec(ByVal jr As JibanRecordBase, ByVal e As System.EventArgs, ByVal Item1Rec As TeibetuSeikyuuRecord)
        Dim jibanLogic As New JibanLogic
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim kameitenRec As New KameitenSearchRecord
        Dim blnTorikesi As Boolean = False
        Dim dic As New Dictionary(Of String, String)
        Dim dicKingaku As New Dictionary(Of String, String)
        Dim clsLogic As New IkkatuHenkouTysSyouhinLogic
        Dim strRecString As String
        Dim strKingakuString As String
        Dim itemRec As Syouhin23Record
        Dim wkDtTime As DateTime

        '****************************************************************************
        ' ドロップダウンリスト設定
        '****************************************************************************
        Dim helper As New DropDownHelper
        ' 調査方法コンボにデータをバインドする
        helper.SetDropDownList(Me.SelectTysHouhou, DropDownHelper.DropDownType.TyousaHouhou, True, False)
        ' 商品1コンボにデータをバインドする
        helper.SetDropDownList(Me.SelectSyouhin1, DropDownHelper.DropDownType.Syouhin1, True, True)
        'IkkatuHenkouTysSyouhinLogic.getDicItem1メソッドと同じ順番で登録すること
        With dic
            '地盤レコードから取得
            .Add(dicKey.dkKbn, cl.GetDisplayString(jr.Kbn))                                         '区分 ------------------- (0)
            .Add(dicKey.dkHosyousyoNo, cl.GetDisplayString(jr.HosyousyoNo))                         '保証書NO --------------- (1)
            .Add(dicKey.dkTysHouhou, cl.GetDisplayString(jr.TysHouhou))                             '調査方法 --------------- (2)
            .Add(dicKey.dkTysKaisyaCd, cl.GetDisplayString(jr.TysKaisyaCd))                         '調査会社コード --------- (3)
            .Add(dicKey.dkTysKaisyaJigyousyoCd, cl.GetDisplayString(jr.TysKaisyaJigyousyoCd))       '調査会社事業所コード --- (4)
            .Add(dicKey.dkKameitenCd, cl.GetDisplayString(jr.KameitenCd))                           '加盟店コード ----------- (5)
            .Add(dicKey.dkSesyuMei, cl.GetDisplayString(jr.SesyuMei))                               '施主名 ----------------- (6)
            .Add(dicKey.dkSyouhinKbn, cl.GetDisplayString(jr.SyouhinKbn))                           '商品区分 --------------- (7)
            .Add(dicKey.dkTysGaiyou, cl.GetDisplayString(jr.TysGaiyou))                             '調査概要 --------------- (8)
            .Add(dicKey.dkIraiTousuu, cl.GetDisplayString(Format(jr.DoujiIraiTousuu, "#,0")))       '依頼棟数 --------------- (9)
            .Add(dicKey.dkKakakuSetteiBasyo, cl.GetDisplayString(jibanLogic.GetKakakuSetteiBasyo(jr.SyouhinKbn, jr.TysHouhou, Item1Rec.SyouhinCd)))   '価格設定場所 ---- (10)
            .Add(dicKey.dkTatemonoYoutoNo, cl.GetDisplayString(jr.TatemonoYoutoNo))                                                             '建物用途NO ------ (11)
            .Add(dicKey.dkStatusIf, cl.GetDisplayString(jr.StatusIf))                                                                           '進捗ステータス--- (12)
            .Add(dicKey.dkAddDatetimeJiban, cl.GetDisplayString(jr.AddDatetime.ToString(EarthConst.FORMAT_DATE_TIME_1)))                        '登録日時 -------- (13)
            .Add(dicKey.dkUpdDatetimeJiban, cl.GetDisplayString(jr.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_1)))                        '更新日時 -------- (14)
            '邸別請求レコードから取得
            .Add(dicKey.dkBunruiCd, cl.GetDisplayString(Item1Rec.BunruiCd))                         '分類コード ----------------- (15)
            .Add(dicKey.dkGamenHyoujiNo, cl.GetDisplayString(Item1Rec.GamenHyoujiNo))               '画面表示NO ----------------- (16)
            .Add(dicKey.dkSyouhinCd, cl.GetDisplayString(Item1Rec.SyouhinCd))                       '商品コード ----------------- (17)
            .Add(dicKey.dkZeiKbn, cl.GetDisplayString(Item1Rec.ZeiKbn))                             '税区分 --------------------- (18)
            .Add(dicKey.dkSeikyuusyoHakDate, cl.GetDisplayString(Item1Rec.SeikyuusyoHakDate))       '請求書発行日 --------------- (19)
            .Add(dicKey.dkUriDate, cl.GetDisplayString(Item1Rec.UriDate))                           '売上年月日 ----------------- (20)
            .Add(dicKey.dkSeikyuuUmu, cl.GetDisplayString(Item1Rec.SeikyuuUmu))                     '請求有無 ------------------- (21)
            .Add(dicKey.dkKakuteiKbn, cl.GetDisplayString(Item1Rec.KakuteiKbn))                     '確定区分 ------------------- (22)
            .Add(dicKey.dkUriKeijyouFlg, cl.GetDisplayString(Item1Rec.UriKeijyouFlg))               '売上計上フラグ ------------- (23)
            .Add(dicKey.dkUriKeijouDate, cl.GetDisplayString(Item1Rec.UriKeijyouDate))              '売上計上日 ----------------- (24)
            .Add(dicKey.dkBikou, cl.GetDisplayString(Item1Rec.Bikou))                               '備考 ----------------------- (25)
            .Add(dicKey.dkHattyuusyoGaku, cl.GetDisplayString(Item1Rec.HattyuusyoGaku))                 '発注書金額 --------- (26)
            .Add(dicKey.dkHattyuusyoKakuninDate, cl.GetDisplayString(Item1Rec.HattyuusyoKakuninDate))   '発注書確認日 ------- (27)
            .Add(dicKey.dkIkkatuNyuukinFlg, cl.GetDisplayString(Item1Rec.IkkatuNyuukinFlg))             '一括入金フラグ ----- (28)
            .Add(dicKey.dkTysMitsyoSakuseiDate, cl.GetDisplayString(Item1Rec.TysMitsyoSakuseiDate))     '調査見積書作成日 --- (29)
            .Add(dicKey.dkHattyuusyoKakuteiFlg, cl.GetDisplayString(Item1Rec.HattyuusyoKakuteiFlg))     '発注書確定フラグ --- (30)
            .Add(dicKey.dkAddLoginUserId, cl.GetDisplayString(Item1Rec.AddLoginUserId))                 '登録ログインユーザーID   --- (31)

            wkDtTime = Item1Rec.AddDatetime
            If wkDtTime = DateTime.MinValue Then
                .Add(dicKey.dkAddDatetimeItem, cl.GetDisplayString(Item1Rec.AddDatetime))                                           '登録日時 ------------------- (32)
            Else
                .Add(dicKey.dkAddDatetimeItem, cl.GetDisplayString(Item1Rec.AddDatetime.ToString(EarthConst.FORMAT_DATE_TIME_1)))   '登録日時 ------------------- (32)
            End If
            .Add(dicKey.dkUpdLoginUserId, cl.GetDisplayString(Item1Rec.UpdLoginUserId))                                             '更新ログインユーザーID   --- (33)
            wkDtTime = Item1Rec.UpdDatetime
            If wkDtTime = DateTime.MinValue Then
                .Add(dicKey.dkUpdDatetimeItem, cl.GetDisplayString(Item1Rec.UpdDatetime))                                           '更新日時 ------------------- (34)
            Else
                .Add(dicKey.dkUpdDatetimeItem, cl.GetDisplayString(Item1Rec.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_1)))   '更新日時 ------------------- (34)
            End If
        End With

        'IkkatuHenkouTysSyouhinLogic.getDicItem1Kingakuメソッドと同じ順番で登録すること
        With dicKingaku
            '工務店請求額
            .Add(dicKey.dkKoumutenSeikyuuGaku, cl.GetDisplayString(Item1Rec.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1)))
            '実請求額
            .Add(dicKey.dkUriGaku, cl.GetDisplayString(Item1Rec.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1)))
            '承諾書金額
            .Add(dicKey.dkSiireGaku, cl.GetDisplayString(Item1Rec.SiireGaku.ToString(EarthConst.FORMAT_KINGAKU_1)))
        End With

        '区分
        Me.HiddenKbn.Value = dic(dicKey.dkKbn)
        '保証書NO
        Me.HiddenHosyousyoNo.Value = dic(dicKey.dkHosyousyoNo)
        '顧客番号
        Me.TextKokyakuBangou.Text = dic(dicKey.dkKbn) & dic(dicKey.dkHosyousyoNo)
        '調査方法コード
        Me.TextTysHouhouCode.Text = dic(dicKey.dkTysHouhou)
        '調査方法Hidden
        Me.HiddenTysHouhouCode.Value = dic(dicKey.dkTysHouhou)
        '調査方法のDDL表示処理
        cl.ps_SetSelectTextBoxTysHouhou(dic(dicKey.dkTysHouhou), Me.SelectTysHouhou, False)
        '調査会社コード(DB値)
        Me.HiddenTysKaisyaCd.Value = dic(dicKey.dkTysKaisyaCd)
        '調査会社事業所コード
        Me.HiddenTysKaisyaJigyousyoCd.Value = dic(dicKey.dkTysKaisyaJigyousyoCd)
        '調査会社コード(表示値)
        Me.TextTyousakaisyaCode.Text = dic(dicKey.dkTysKaisyaCd) & dic(dicKey.dkTysKaisyaJigyousyoCd)
        Me.HiddenTyousaKaishaCdOld.Value = dic(dicKey.dkTysKaisyaCd) & dic(dicKey.dkTysKaisyaJigyousyoCd)

        '調査会社名
        If dic(dicKey.dkTysKaisyaCd) <> "" And dic(dicKey.dkTysKaisyaJigyousyoCd) <> "" Then
            getTysGaisyaName(dic(dicKey.dkTysKaisyaCd) & dic(dicKey.dkTysKaisyaJigyousyoCd), dic(dicKey.dkKameitenCd))
        End If

        '加盟店コード
        Me.TextKameitenCode.Text = dic(dicKey.dkKameitenCd)

        '加盟店マスタからデータを取得
        kameitenRec = kameitenSearchLogic.GetKameitenSearchResult(dic(dicKey.dkKbn), _
                                                                      dic(dicKey.dkKameitenCd), _
                                                                      dic(dicKey.dkTysKaisyaCd) & dic(dicKey.dkTysKaisyaJigyousyoCd), _
                                                                      blnTorikesi)

        ' 加盟店マスタからデータが取得できた場合、下記対象項目の値をセット(取消状況に応じて、色替えを行なう処理を呼ぶ)
        If Not kameitenRec Is Nothing Then
            Me.TextKameitenName.Text = cl.GetDisplayString(kameitenRec.KameitenMei1)
            Me.TextTorikesiRiyuu.Text = cl.GetDisplayString(cl.getTorikesiRiyuu(kameitenRec.Torikesi, kameitenRec.KtTorikesiRiyuu, True)) '加盟店取消理由
            Me.HiddenKeiretuCd.Value = cl.GetDisplayString(kameitenRec.KeiretuCd)
            Me.HiddenEigyousyoCd.Value = cl.GetDisplayString(kameitenRec.EigyousyoCd)

            '加盟店取消理由設定
            setTorikesiRiyuu(kameitenRec.Kbn, kameitenRec.KameitenCd)
        Else
            '加盟店情報初期化(色戻し処理)⇒処理なし
        End If

        '直接請求/他請求の取得
        itemRec = jibanLogic.GetSyouhinInfo(dic(dicKey.dkSyouhinCd), EarthEnum.EnumSyouhinKubun.AllSyouhinTorikesi, dic(dicKey.dkKameitenCd))
        If cl.GetDisplayString(Item1Rec.SeikyuuSakiCd) <> String.Empty Then
            itemRec.SeikyuuSakiCd = cl.GetDisplayString(Item1Rec.SeikyuuSakiCd)
            '請求先コード
            Me.HiddenSeikyuuSakiCd.Value = cl.GetDisplayString(Item1Rec.SeikyuuSakiCd)
        End If

        '請求タイプ
        Me.HiddenTysSeikyuuSaki.Value = itemRec.SeikyuuSakiType
        '加盟店取消
        Me.HiddenTorikeshi.Value = cl.GetDisplayString(kameitenRec.Torikesi)

        '施主名
        Me.TextSesyuName.Text = dic(dicKey.dkSesyuMei)

        '商品区分
        Me.HiddenSyouhinKbn.Value = dic(dicKey.dkSyouhinKbn)
        '調査概要
        Me.HiddenTysGaiyou.Value = dic(dicKey.dkTysGaiyou)
        '同時依頼棟数
        Me.HiddenIraiTousuu.Value = dic(dicKey.dkIraiTousuu)
        '価格設定場所
        Me.HiddenKakakuSetteiBasyo.Value = dic(dicKey.dkKakakuSetteiBasyo)
        '建物用途NO
        Me.HiddenTatemonoYoutoNo.Value = dic(dicKey.dkTatemonoYoutoNo)
        '進捗ステータス
        Me.HiddenStatusIf.Value = dic(dicKey.dkStatusIf)
        '発注書金額
        Me.HiddenHattyuusyoGaku.Value = dic(dicKey.dkHattyuusyoGaku)
        '地盤テーブル登録日時
        Me.HiddenAddDatetimeJiban.Value = dic(dicKey.dkAddDatetimeJiban)
        '地盤テーブル更新日時
        Me.HiddenUpdDatetimeJiban.Value = dic(dicKey.dkUpdDatetimeJiban)

        '分類コード 
        Me.HiddenBunruiCd.Value = dic(dicKey.dkBunruiCd)
        '画面表示NO
        Me.HiddenGamenHyoujiNo.Value = dic(dicKey.dkGamenHyoujiNo)

        '商品1名コンボ
        Me.HiddenSyouhin1Code.Value = dic(dicKey.dkSyouhinCd)
        If cl.ChkDropDownList(SelectSyouhin1, cl.GetDispNum(Item1Rec.SyouhinCd)) Then
            'DDLにあれば、選択する
            SelectSyouhin1.SelectedValue = cl.GetDispStr(Item1Rec.SyouhinCd)
        Else
            'DDLになければ、アイテムを追加
            SelectSyouhin1.Items.Add(New ListItem(Item1Rec.SyouhinCd & ":" & Item1Rec.SyouhinMei, Item1Rec.SyouhinCd))
            SelectSyouhin1.SelectedValue = cl.GetDispStr(Item1Rec.SyouhinCd)     '選択状態
        End If

        '売上処理
        If dic(dicKey.dkUriKeijyouFlg) = "0" Then
            Me.TextUriageSyori.Text = ""
        Else
            Me.TextUriageSyori.Text = EarthConst.KEIJOU_ZUMI
        End If

        '承諾書金額変更可否FLG
        If Item1Rec.SyoudakuHenkouKahi = False Then
            Me.HiddenSdsHenkouKahi.Value = EarthConst.HENKOU_FUKA
        Else
            Me.HiddenSdsHenkouKahi.Value = String.Empty
        End If
        '工務店請求金額変更可否FLG
        If Item1Rec.KoumutenHenkouKahi = False Then
            Me.HiddenKmtnHenkouKahi.Value = EarthConst.HENKOU_FUKA
        Else
            Me.HiddenKmtnHenkouKahi.Value = String.Empty
        End If
        '実請求金額変更可否FLG
        If Item1Rec.JituseikyuuHenkouKahi = False Then
            Me.HiddenJituGakuHenkouKahi.Value = EarthConst.HENKOU_FUKA
        Else
            Me.HiddenJituGakuHenkouKahi.Value = String.Empty
        End If

        '工務店請求額
        Me.TextKoumutenKingaku.Text = dicKingaku(dicKey.dkKoumutenSeikyuuGaku)
        '実請求額
        Me.TextJituSeikyuuKingaku.Text = dicKingaku(dicKey.dkUriGaku)
        '承諾書金額
        Me.TextSyoudakusyoKingaku.Text = dicKingaku(dicKey.dkSiireGaku)

        '売上年月日
        Me.HiddenUriDate.Value = dic(dicKey.dkUriDate)

        '請求有無
        If dic(dicKey.dkSeikyuuUmu) = "0" Then
            Me.SelectSeikyuuUmu.SelectedValue = "0"
        Else
            Me.SelectSeikyuuUmu.SelectedValue = "1"
        End If


        '画面表示時のDB値の連結値を取得
        strRecString = clsLogic.getJoinString(dic.Values.GetEnumerator)

        Me.HiddenDbValue.Value = strRecString
        Me.HiddenChgValue.Value = strRecString

        '画面表示金額のDB値の連結値を取得
        strKingakuString = clsLogic.getJoinString(dicKingaku.Values.GetEnumerator)
        Me.HiddenDbKingaku.Value = strKingakuString
        Me.HiddenChgKingaku.Value = strKingakuString

        '画面表示時の項目の連結値を取得(原価)
        Me.HiddenOpenValuesGenka.Value = getCtrlValuesStringGenka()
        '画面表示時の項目の連結値を取得(販売価格)
        Me.HiddenOpenValuesHanbai.Value = getCtrlValuesStringHanbai()
        '画面表示時の項目の連結値を取得(商品価格設定)
        Me.HiddenOpenValuesSyouhinKkk.Value = getCtrlValuesStringSyouhinKkk()

    End Sub

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する(原価)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringGenka() As String
        Dim sb As New StringBuilder

        sb.Append(Me.SelectTysHouhou.SelectedValue & EarthConst.SEP_STRING)     '調査方法NO
        sb.Append(Me.TextTyousakaisyaCode.Text & EarthConst.SEP_STRING)         '調査会社CD
        sb.Append(Me.SelectSyouhin1.SelectedValue & EarthConst.SEP_STRING)      '商品1プルダウン

        Return (sb.ToString)
    End Function

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する(販売価格)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringHanbai() As String
        Dim sb As New StringBuilder

        sb.Append(Me.SelectTysHouhou.SelectedValue & EarthConst.SEP_STRING)     '調査方法NO
        sb.Append(Me.TextTyousakaisyaCode.Text & EarthConst.SEP_STRING)         '調査会社CD
        sb.Append(Me.SelectSyouhin1.SelectedValue & EarthConst.SEP_STRING)      '商品1プルダウン

        Return (sb.ToString)
    End Function

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する(商品価格設定)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringSyouhinKkk() As String
        Dim sb As New StringBuilder

        '商品区分
        sb.Append(Me.HiddenSyouhinKbn.Value & EarthConst.SEP_STRING)
        '調査方法NO
        sb.Append(Me.SelectTysHouhou.SelectedValue & EarthConst.SEP_STRING)
        '商品1プルダウン
        sb.Append(Me.SelectSyouhin1.SelectedValue & EarthConst.SEP_STRING)

        Return (sb.ToString)
    End Function

#Region "調査会社取得処理"
    ''' <summary>
    ''' 調査会社名称取得処理
    ''' </summary>
    ''' <param name="strGaisyaCd">調査会社コード</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <remarks></remarks>
    Private Sub getTysGaisyaName(ByVal strGaisyaCd, ByVal strKameitenCd)
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        dataArray = tyousakaisyaSearchLogic.GetTyousakaisyaSearchResult(strGaisyaCd, "", "", "", True, strKameitenCd)

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            Me.TextTyousakaisyaCode.Text = recData.TysKaisyaCd & recData.JigyousyoCd
            Me.TextTyousakaisyaName.Text = recData.TysKaisyaMei
            ' 調査会社NG設定
            If recData.KahiKbn = 9 Then
                Me.TextTyousakaisyaCode.Style("color") = "red"
                Me.TextTyousakaisyaName.Style("color") = "red"
                'NG調査会社設定警告イベントを発生
                RaiseEvent WarningTysKaisya(Me, New EventArgs())
            Else
                Me.TextTyousakaisyaCode.Style("color") = "blue"
                Me.TextTyousakaisyaName.Style("color") = "blue"
            End If
        Else
            Me.TextTyousakaisyaName.Text = ""
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Ajax動作時のフォーカスセット
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub setFocusAJ(ByVal ctrl As Control)
        'フォーカスセット
        masterAjaxSM.SetFocus(ctrl)
    End Sub

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <param name="errMess">エラーメッセージ</param>
    ''' <param name="arrFocusTargetCtrl">フォーカス対象リスト</param>
    ''' <remarks>
    ''' コード入力値変更チェック<br/>
    ''' 必須チェック<br/>
    ''' 禁則文字チェック(文字列入力フィールドが対象)<br/>
    ''' バイト数チェック(文字列入力フィールドが対象)<br/>
    ''' 桁数チェック<br/>
    ''' 日付チェック<br/>
    ''' その他チェック<br/>
    ''' </remarks>
    Public Sub checkInput( _
                            ByRef errMess As String, _
                            ByRef arrFocusTargetCtrl As List(Of Control), _
                            Optional ByVal flgSyouhinKkkChk As Boolean = False)

        '地盤画面共通クラス
        Dim jBn As New Jiban
        Dim strErrGyouInfo As String = EarthConst.KOKYAKU_BANGOU & Me.TextKokyakuBangou.Text & EarthConst.BRANK_STRING

        '商品データの入力制御処理
        Me.enableItem1()

        '●コード入力値変更チェック
        If Me.TextTyousakaisyaCode.Text <> Me.HiddenTyousaKaishaCdOld.Value Then
            errMess += strErrGyouInfo & Messages.MSG030E.Replace("@PARAM1", "調査会社コード")
            arrFocusTargetCtrl.Add(Me.ButtonSearchTyousakaisya)
        Else
            '調査会社名称の取得
            getTysGaisyaName(Me.TextTyousakaisyaCode.Text, Me.TextKameitenCode.Text)
        End If

        '●必須チェック
        '調査方法
        If Me.TextTysHouhouCode.Text = String.Empty Then
            '発注書金額「0」または空白のときのみエラーとする
            If Me.HiddenHattyuusyoGaku.Value = "0" Or Me.HiddenHattyuusyoGaku.Value = "" Then
                errMess &= strErrGyouInfo & Messages.MSG013E.Replace("@PARAM1", "調査方法")
                arrFocusTargetCtrl.Add(Me.SelectTysHouhou)
            End If
        End If
        '調査会社
        If Me.TextTyousakaisyaCode.Text = String.Empty Or Me.TextTyousakaisyaName.Text = String.Empty Then
            errMess += strErrGyouInfo & Messages.MSG013E.Replace("@PARAM1", "調査会社")
            arrFocusTargetCtrl.Add(Me.TextTyousakaisyaCode)
        End If
        '商品1
        If Me.SelectSyouhin1.SelectedValue = String.Empty Then
            errMess += strErrGyouInfo & Messages.MSG013E.Replace("@PARAM1", "商品1")
            arrFocusTargetCtrl.Add(Me.SelectSyouhin1)
        End If

        '商品価格設定・原価・販売価格マスタへの存在チェック
        If flgSyouhinKkkChk Then
            Dim lgcJiban As New JibanLogic
            Dim intTysHouhou As Integer
            Dim intTysGaiyou As Integer
            Dim intDoujiIraiTousuu As Integer

            '○初期読込時と登録時に商品価格設定関係の値が変更時のみ『商品価格設定マスタ』値をチェックする
            If Me.HiddenOpenValuesSyouhinKkk.Value <> getCtrlValuesStringSyouhinKkk() Then
                '設定・取得用 商品価格設定レコード
                Dim recKakakuSettei As New KakakuSetteiRecord

                '商品区分
                cl.SetDisplayString(Me.HiddenSyouhinKbn.Value, recKakakuSettei.SyouhinKbn)
                '商品コード
                cl.SetDisplayString(Me.SelectSyouhin1.SelectedValue, recKakakuSettei.SyouhinCd)
                '調査方法
                cl.SetDisplayString(Me.TextTysHouhouCode.Text, recKakakuSettei.TyousaHouhouNo)

                '商品価格設定マスタから値の取得
                If lgcJiban.GetTysGaiyou(recKakakuSettei) = False Then
                    errMess += strErrGyouInfo & Messages.MSG183E
                    arrFocusTargetCtrl.Add(Me)
                End If
            End If

            '○初期読込時と登録時に変更がある場合のみ『原価マスタ』チェックを行なう
            If Me.HiddenOpenValuesGenka.Value <> getCtrlValuesStringGenka() Then
                '数値項目の変換
                cl.SetDisplayString(Me.TextTysHouhouCode.Text, intTysHouhou)
                cl.SetDisplayString(Me.HiddenTysGaiyou.Value, intTysGaiyou)
                cl.SetDisplayString(Me.HiddenIraiTousuu.Value, intDoujiIraiTousuu)

                '原価マスタチェック
                If lgcJiban.GetSyoudakusyoKingaku1(Me.SelectSyouhin1.SelectedValue, _
                                                   Me.HiddenKbn.Value, _
                                                   intTysHouhou, _
                                                   intTysGaiyou, _
                                                   intDoujiIraiTousuu, _
                                                   Me.TextTyousakaisyaCode.Text, _
                                                   Me.TextKameitenCode.Text, _
                                                   0, _
                                                   Me.HiddenKeiretuCd.Value, _
                                                   False) = False Then
                    errMess += strErrGyouInfo & Messages.MSG180E
                    arrFocusTargetCtrl.Add(Me)
                End If
            End If

            '○初期読込時と登録時に変更がある場合のみ『販売価格マスタ』チェックを行なう
            If Me.HiddenOpenValuesHanbai.Value <> getCtrlValuesStringHanbai() Then
                '販売価格マスタチェック
                Dim hin1InfoRec As New Syouhin1InfoRecord
                Dim hin1AutoSetRecord As New Syouhin1AutoSetRecord
                hin1InfoRec.SyouhinCd = Me.SelectSyouhin1.SelectedValue
                hin1InfoRec.TysKaisyaCd = Me.TextTyousakaisyaCode.Text
                hin1InfoRec.TyousaHouhouNo = intTysHouhou
                hin1InfoRec.KameitenCd = Me.TextKameitenCode.Text
                hin1InfoRec.EigyousyoCd = Me.HiddenEigyousyoCd.Value
                hin1InfoRec.KeiretuCd = Me.HiddenKeiretuCd.Value

                If lgcJiban.GetHanbaiKingaku1(hin1InfoRec, _
                                           hin1AutoSetRecord) = False Then
                    errMess += strErrGyouInfo & Messages.MSG182E
                    arrFocusTargetCtrl.Add(Me)
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' 取消理由の設定
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <remarks></remarks>
    Private Sub setTorikesiRiyuu(ByVal strKbn As String, ByVal strKameitenCd As String)

        '色替え処理対象のコントロールを配列に格納(※取消理由テキストボックス以外)
        Dim objArray() As Object = New Object() {Me.TextKameitenCode, Me.TextKameitenName, Me.TextTorikesiRiyuu}

        '取消理由と加盟店情報の文字色設定
        cl.GetKameitenTorikesiRiyuu(strKbn _
                                        , strKameitenCd _
                                        , Me.TextTorikesiRiyuu _
                                        , True _
                                        , False _
                                        , objArray)

    End Sub

    ''' <summary>
    ''' 金額の整合性チェック
    ''' </summary>
    ''' <param name="arrFocusTargetCtrl">フォーカス対象リスト</param>
    ''' <returns>エラーメッセージ</returns>
    ''' <remarks>
    ''' 直接請求の場合、工務店請求額と実請求額は等しくなければいけない。
    ''' 請求有・3系列の場合、工務店請求額・実請求額のどちらかのみ変更された際、最低一回は計算処理必須。
    ''' </remarks>
    Public Function checkKingaku(ByRef arrFocusTargetCtrl As List(Of Control)) As String
        Dim errMess As String = ""
        Dim strErrGyouInfo As String = EarthConst.KOKYAKU_BANGOU & Me.TextKokyakuBangou.Text & EarthConst.BRANK_STRING & "商品1" & EarthConst.BRANK_STRING & EarthConst.BRANK_STRING & EarthConst.BRANK_STRING
        Dim jbLogic As New JibanLogic
        Dim intKeiretuFlg As Integer = 0

        '特別対応価格が反映されてる場合は、チェックを行わない
        If Not String.IsNullOrEmpty(Me.ucTokubetuTaiouToolTipCtrl.AccDisplayCd.Value) Then
            Return errMess
            Exit Function
        End If

        '系列フラグ
        intKeiretuFlg = jbLogic.GetKeiretuFlg(Me.HiddenKeiretuCd.Value)

        '直接請求の場合
        If Me.HiddenTysSeikyuuSaki.Value = EarthConst.SEIKYU_TYOKUSETU Then
            If Me.TextKoumutenKingaku.Text <> Me.TextJituSeikyuuKingaku.Text Then
                errMess &= strErrGyouInfo & Messages.MSG132E
                arrFocusTargetCtrl.Add(Me.TextKoumutenKingaku)
            End If
        End If

        '請求有・3系列の場合
        If Me.HiddenAutoKingakuFlg.Value = Me.HiddenManualKingakuFlg.Value Then
            If Me.SelectSeikyuuUmu.SelectedValue = "1" _
                    And intKeiretuFlg = 1 _
                    And Me.HiddenBothKingakuChgFlg.Value <> String.Empty _
                    And Me.HiddenBothKingakuChgFlg.Value <> "1" Then
                errMess &= strErrGyouInfo & Messages.MSG142E
                arrFocusTargetCtrl.Add(Me.TextKoumutenKingaku)
            End If
        End If

        Return errMess
    End Function

    ''' <summary>
    ''' 商品データの入力制御処理
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub enableItem1()
        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim jibanLogic As New JibanLogic

        jSM.Hash2Ctrl(Me.TrTysSyouhin_1_2, EarthConst.MODE_VIEW, ht)

        '商品コード
        cl.chgDispSyouhinPull(Me.SelectSyouhin1, Me.SPAN_Syouhin1)
        '特別対応ツールチップ(非表示設定)
        Me.AccTokubetuTaiouToolTip.AccVisibleFlg.Value = "1"

        '売上状況を判定()
        If Me.TextUriageSyori.Text <> "" Or Me.HiddenTorikeshi.Value = "1" Then
            '売上計上済み、または取消加盟店の場合、参照モードに設定
            jSM.Hash2Ctrl(Me.TrTysSyouhin_1_1, EarthConst.MODE_VIEW, ht)
            jSM.Hash2Ctrl(Me.TrTysSyouhin_1_2, EarthConst.MODE_VIEW, ht)
            '各種検索ボタンを非表示
            Me.ButtonSearchTyousakaisya.Disabled = True
            Me.ButtonSearchTyousakaisya.Visible = False

        ElseIf Me.TextTysHouhouCode.Text = String.Empty _
                Or Me.SelectTysHouhou.SelectedValue = String.Empty _
                Or Me.SelectSyouhin1.SelectedValue = String.Empty Then

            Me.SelectSeikyuuUmu.Style("display") = "none"
            Me.SPAN_Seikyuu.InnerHtml = ""
            '発注書金額＝0または＝NULLの場合、自動設定(クリア処理)を行う
            If Me.HiddenHattyuusyoGaku.Value = "0" Or Me.HiddenHattyuusyoGaku.Value = "" Then
                Me.TextKoumutenKingaku.Text = ""
                Me.TextJituSeikyuuKingaku.Text = ""
                Me.TextSyoudakusyoKingaku.Text = ""
                Me.SelectSeikyuuUmu.SelectedValue = "1"
            End If

        Else
            If Me.HiddenSdsHenkouKahi.Value = "" Then
                '承諾書金額
                cl.chgDispSyouhinText(Me.TextSyoudakusyoKingaku)
            End If

            If Me.HiddenUriDate.Value = "" Then
                '価格反映されている場合
                If Me.ucTokubetuTaiouToolTipCtrl.AccDisplayCd.Value = String.Empty Then
                    '請求有無
                    cl.chgDispSyouhinPull(Me.SelectSeikyuuUmu, Me.SPAN_Seikyuu)
                End If
            End If

            '価格反映されている場合
            If Me.ucTokubetuTaiouToolTipCtrl.AccDisplayCd.Value = String.Empty Then
                '工務店請求金額の変更可否
                If Me.HiddenKmtnHenkouKahi.Value = "" Then
                    cl.chgDispSyouhinText(Me.TextKoumutenKingaku)
                End If
                '実請求税抜金額の変更可否
                If Me.HiddenJituGakuHenkouKahi.Value = "" Then
                    cl.chgDispSyouhinText(Me.TextJituSeikyuuKingaku)
                End If
            End If

            '●工務店・実請求の変更可否既存
            'If Me.SelectSeikyuuUmu.SelectedValue = "1" Then
            '    If Me.HiddenKakakuSetteiBasyo.Value = "1" Then
            '    Else
            '        cl.chgDispSyouhinText(Me.TextJituSeikyuuKingaku)    '実請求金額
            '        If Me.HiddenTysSeikyuuSaki.Value = EarthConst.SEIKYU_TASETU And _
            '            jibanLogic.GetKeiretuFlg(Me.HiddenKeiretuCd.Value) = 0 Then
            '        Else
            '            cl.chgDispSyouhinText(Me.TextKoumutenKingaku)   '工務店請求金額
            '        End If
            '    End If
            'End If

            '+++++++++++++++++++++++++++++++++++++++++++++++++++
            ' 進捗ステータスによって、調査会社の編集可否を切り替え
            '+++++++++++++++++++++++++++++++++++++++++++++++++++
            If HiddenStatusIf.Value > EarthConst.IF_STATUS_TOUROKU_ZUMI Then
                jSM.Hash2Ctrl(Me.TdTyousakaisya, EarthConst.MODE_VIEW, ht)
                Me.ButtonSearchTyousakaisya.Disabled = True
                Me.ButtonSearchTyousakaisya.Visible = False
            End If
        End If

    End Sub

    ''' <summary>
    ''' 画面商品コントロールから邸別請求データに値をセットする（登録/更新処理用）
    ''' </summary>
    ''' <returns>邸別請求レコード</returns>
    ''' <remarks></remarks>
    Public Function setSyouhinToTeibetu() As TeibetuSeikyuuRecord
        Dim tsR As New TeibetuSeikyuuRecord
        Dim clsLogic As New IkkatuHenkouTysSyouhinLogic
        Dim strChgValues() As String
        Dim dicItem1 As Dictionary(Of String, String)

        If String.IsNullOrEmpty(Me.AccHiddenChgValue.Value) Then
            strChgValues = Nothing
        Else
            strChgValues = clsLogic.getArrayFromDollarSep(Me.AccHiddenChgValue.Value)
        End If
        dicItem1 = clsLogic.getDicItem1(strChgValues)

        '区分
        tsR.Kbn = Me.HiddenKbn.Value
        '番号
        tsR.HosyousyoNo = Me.HiddenHosyousyoNo.Value
        '分類コード
        tsR.BunruiCd = Me.HiddenBunruiCd.Value
        '画面表示NO
        tsR.GamenHyoujiNo = CInt(Me.HiddenGamenHyoujiNo.Value)
        '商品コード
        tsR.SyouhinCd = Me.SelectSyouhin1.SelectedValue
        '売上金額
        If Me.TextJituSeikyuuKingaku.Text = "" Then
            Me.TextJituSeikyuuKingaku.Text = "0"
        End If
        cl.SetDisplayString(Me.TextJituSeikyuuKingaku.Text, tsR.UriGaku)
        '仕入金額
        If Me.TextSyoudakusyoKingaku.Text = "" Then
            Me.TextSyoudakusyoKingaku.Text = "0"
        End If
        cl.SetDisplayString(Me.TextSyoudakusyoKingaku.Text, tsR.SiireGaku)
        '税区分
        tsR.ZeiKbn = dicItem1(dicKey.dkZeiKbn)
        '請求書発行日
        cl.SetDisplayString(dicItem1(dicKey.dkSeikyuusyoHakDate), tsR.SeikyuusyoHakDate)
        '売上年月日
        cl.SetDisplayString(dicItem1(dicKey.dkUriDate), tsR.UriDate)
        '請求有無
        cl.SetDisplayString(Me.SelectSeikyuuUmu.SelectedValue, tsR.SeikyuuUmu)
        '売上計上FLG
        cl.SetDisplayString(dicItem1(dicKey.dkUriKeijyouFlg), tsR.UriKeijyouFlg)
        '売上計上日
        cl.SetDisplayString(dicItem1(dicKey.dkUriKeijouDate), tsR.UriKeijyouDate)
        '確定区分
        tsR.KakuteiKbn = Integer.MinValue
        '備考
        cl.SetDisplayString(dicItem1(dicKey.dkBikou), tsR.Bikou)
        '工務店請求金額
        If Me.TextKoumutenKingaku.Text = "" Then
            Me.TextKoumutenKingaku.Text = "0"
        End If
        cl.SetDisplayString(Me.TextKoumutenKingaku.Text, tsR.KoumutenSeikyuuGaku)
        '発注書金額
        cl.SetDisplayString(dicItem1(dicKey.dkHattyuusyoGaku), tsR.HattyuusyoGaku)
        '発注書確認日
        cl.SetDisplayString(dicItem1(dicKey.dkHattyuusyoKakuninDate), tsR.HattyuusyoKakuninDate)
        '一括入金FLG
        cl.SetDisplayString(dicItem1(dicKey.dkIkkatuNyuukinFlg), tsR.IkkatuNyuukinFlg)
        '調査見積書作成日
        cl.SetDisplayString(dicItem1(dicKey.dkTysMitsyoSakuseiDate), tsR.TysMitsyoSakuseiDate)
        '発注書確定FLG
        cl.SetDisplayString(dicItem1(dicKey.dkHattyuusyoKakuteiFlg), tsR.HattyuusyoKakuteiFlg)

        'データ取得時点での邸別請求テーブル.更新日時の保持値（排他チェック用）
        If dicItem1(dicKey.dkUpdDatetimeItem) = "" Then
            If dicItem1(dicKey.dkAddDatetimeItem) = "" Then
                tsR.UpdDatetime = DateTime.MinValue
            Else
                tsR.UpdDatetime = DateTime.ParseExact(dicItem1(dicKey.dkAddDatetimeItem), EarthConst.FORMAT_DATE_TIME_1, Nothing)
            End If
        Else
            tsR.UpdDatetime = DateTime.ParseExact(dicItem1(dicKey.dkUpdDatetimeItem), EarthConst.FORMAT_DATE_TIME_1, Nothing)
        End If

        Return tsR

    End Function

End Class
