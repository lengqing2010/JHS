Partial Public Class tenbetu_syuusei
    Inherits System.Web.UI.Page
#Region "EMAB障害対応情報格納処理"
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
#End Region

#Region "変数"
    Private user_info As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    ''' <summary>
    ''' 販促品行ユーザーコントロール格納リスト
    ''' </summary>
    ''' <remarks></remarks>
    Private listHsRecCtrl As New List(Of HansokuRecordCtrl)
    Private dtLogic As New DataLogic
    Private cLogic As New CommonLogic
    ''' <summary>
    ''' ビジネスロジック共通クラス
    ''' </summary>
    ''' <remarks></remarks>
    Private cbLogic As New CommonBizLogic
    Private clsLogic As New TenbetuSyuuseiLogic
#End Region
#Region "定数"
#Region "自動設定用フラグ列挙体"
    ''' <summary>
    ''' 請求有無
    ''' </summary>
    ''' <remarks></remarks>
    Enum enSeikyuuUmu
        ''' <summary>
        ''' 無
        ''' </summary>
        ''' <remarks></remarks>
        Nasi = 0
        ''' <summary>
        ''' 有
        ''' </summary>
        ''' <remarks></remarks>
        Ari = 1
    End Enum
    Private eUmu As enSeikyuuUmu
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <remarks></remarks>
    Enum enSyouhinCd
        ''' <summary>
        ''' 未入力
        ''' </summary>
        ''' <remarks></remarks>
        Mi = 0
        ''' <summary>
        ''' 入力有
        ''' </summary>
        ''' <remarks></remarks>
        Zumi = 1
    End Enum
    Private eSCd As enSyouhinCd
    ''' <summary>
    ''' 請求先
    ''' </summary>
    ''' <remarks></remarks>
    Enum enSeikyuuSaki
        ''' <summary>
        ''' 直接
        ''' </summary>
        ''' <remarks></remarks>
        Tyoku = 0
        ''' <summary>
        ''' 他
        ''' </summary>
        ''' <remarks></remarks>
        Hoka = 1
    End Enum
    Private eSaki As enSeikyuuSaki
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <remarks></remarks>
    Enum enKeiretu
        ''' <summary>
        ''' 3系列
        ''' </summary>
        ''' <remarks></remarks>
        SanK = 0
        ''' <summary>
        ''' 3系列以外
        ''' </summary>
        ''' <remarks></remarks>
        NotSanK = 1
    End Enum
    Private eKretu As enKeiretu
    ''' <summary>
    ''' 金額変更イベント制御（登録）
    ''' </summary>
    ''' <remarks></remarks>
    Enum enTxtChgCtrlTouroku
        JituGaku = 1
        KoumuGaku = 2
        Reset = -1
    End Enum

#End Region
#Region "３系列"
    Private Const KEIRETU_TH As String = EarthConst.KEIRETU_TH
    Private Const KEIRETU_01 As String = EarthConst.KEIRETU_AIFURU
    Private Const KEIRETU_NF As String = EarthConst.KEIRETU_WANDA
#End Region
#Region "倉庫コード"
    Private SOUKO_CD_TOUROKU As String = EarthConst.SOUKO_CD_TOUROKU_TESUURYOU
    Private SOUKO_CD_TOOL As String = EarthConst.SOUKO_CD_SYOKI_TOOL_RYOU
    Private SOUKO_CD_NOT_FC As String = EarthConst.SOUKO_CD_FC_GAI_HANSOKUHIN
    Private SOUKO_CD_FC As String = EarthConst.SOUKO_CD_FC_HANSOKUHIN
#End Region
#Region "区分"
    Private Const JHSFC = "A"
#End Region
#Region "請求先"
    Private Const SEIKYUU_TYOKU As String = EarthConst.SEIKYU_TYOKUSETU
    Private Const SEIKYUU_HOKA As String = EarthConst.SEIKYU_TASETU
    Private Const SEIKYUU_FC As String = EarthConst.SEIKYU_FCSETU
#End Region
#Region "タイトル"
    Private Const TITLE_TENBETU As String = "EARTH 店別データ修正"
    Private Const PAGE_TITLE_TENBETU As String = "店別データ修正"
    Private Const TITLE_HANSOKU As String = "EARTH 販促品請求"
    Private Const PAGE_TITLE_HANSOKU As String = "販促品請求"
    Private Const TITLE_SANSYOU As String = "EARTH 売上処理済販促品参照"
    Private Const PAGE_TITLE_SANSYOU As String = "売上処理済販促品参照"
#End Region

#Region "販促品商品行コントロールID接頭語"
    Private Const HANSOKU_SHOUHIN_CTRL_NAME As String = "CtrlHansokuSyouhin"
#End Region

#Region "エラーメッセージ"
    ''' <summary>
    ''' エラーメッセージパラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PARAM1 = "@PARAM1"
    ''' <summary>
    ''' エラー対象
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ERR_STRING As String = "の伝票売上年月日"

#End Region
#End Region

#Region "工務店税抜き金額活性制御用"
    Private clsCL As New CommonLogic
    Private clsJSM As New JibanSessionManager
#End Region
#Region "モード"
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DisplayMode
        ''' <summary>
        ''' 店別データ修正
        ''' </summary>
        ''' <remarks></remarks>
        TENBETU = 1
        ''' <summary>
        ''' 販促品請求
        ''' </summary>
        ''' <remarks></remarks>
        HANSOKU = 2
        ''' <summary>
        ''' 売上処理済販促品参照
        ''' </summary>
        ''' <remarks></remarks>
        SANSYOU = 3
    End Enum
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <remarks></remarks>
    Private enMode As DisplayMode
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <value></value>
    ''' <returns>コントロールの表示モード</returns>
    ''' <remarks>商品の種類により画面の表示を変更します</remarks>
    Public Property DispMode() As DisplayMode
        Get
            Return enMode
        End Get
        Set(ByVal value As DisplayMode)
            enMode = value
        End Set
    End Property
    ''' <summary>
    ''' 加盟店モード
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum IsFcMode
        ''' <summary>
        ''' FC加盟店
        ''' </summary>
        ''' <remarks></remarks>
        FC = 1
        ''' <summary>
        ''' FC以外加盟店
        ''' </summary>
        ''' <remarks></remarks>
        NOT_FC = 0
    End Enum
    ''' <summary>
    ''' 加盟店モード
    ''' </summary>
    ''' <remarks></remarks>
    Private enIsFc As IsFcMode
    ''' <summary>
    ''' 加盟店モード
    ''' </summary>
    ''' <value></value>
    ''' <returns>FC加盟店:1 FC以外:0</returns>
    ''' <remarks></remarks>
    Public Property IsFC() As IsFcMode
        Get
            Return enIsFc
        End Get
        Set(ByVal value As IsFcMode)
            enIsFc = value
        End Set
    End Property
    ''' <summary>
    ''' 売上計上フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum UriFlg
        ''' <summary>
        ''' 未計上
        ''' </summary>
        ''' <remarks></remarks>
        MI = 0
        ''' <summary>
        ''' 計上済
        ''' </summary>
        ''' <remarks></remarks>
        ZUMI = 1
        ''' <summary>
        ''' 未設定
        ''' </summary>
        ''' <remarks></remarks>
        NONE = -1
    End Enum
    ''' <summary>
    ''' 売上計上フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private enUriFlg As UriFlg
#End Region

#Region "ページ処理"
    ''' <summary>
    ''' ページの初期設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Me.SelectSeikyuu.Attributes.Add("onchange", "objEBI('" & Me.buttonChgTourokuSeikyuu.ClientID & "').click();")
        Me.SelectSyouhinTourokuRyou.Attributes.Add("onchange", "objEBI('" & Me.buttonChgTourokuSyouhin.ClientID & "').click();")
        Me.SelectSeikyuuTool.Attributes.Add("onchange", "objEBI('" & Me.buttonChgToolSeikyuu.ClientID & "').click();")
        Me.SelectSyouhinToolRyou.Attributes.Add("onchange", "objEBI('" & Me.buttonChgToolSyouhin.ClientID & "').click();")

        Me.TextJituseikyuuZeinukikingaku.Attributes.Add("onfocus", "removeFig(this);setTempValueForOnBlur(this);")
        Me.TextKoumutenZeinukiKingaku.Attributes.Add("onfocus", "removeFig(this);setTempValueForOnBlur(this);")
        Me.TextJituseikyuuZeinukikingakuTool.Attributes.Add("onfocus", "removeFig(this);setTempValueForOnBlur(this);")
        Me.TextSyouhizei.Attributes.Add("onfocus", "removeFig(this);setTempValueForOnBlur(this);")
        Me.TextSyouhizeiTool.Attributes.Add("onfocus", "removeFig(this);setTempValueForOnBlur(this);")
        Me.TextUriageNengappi.Attributes.Add("onfocus", "setTempValueForOnBlur(this);")
        Me.TextUriageNengappiTool.Attributes.Add("onfocus", "setTempValueForOnBlur(this);")
        '伝票売上年月日（登録料）
        Me.TextDenpyouUriDate.Attributes.Add("onfocus", "setTempValueForOnBlur(this);")
        '伝票売上年月日（初期ツール料）
        Me.TextDenpyouUriDateTool.Attributes.Add("onfocus", "setTempValueForOnBlur(this);")

        Me.TextJituseikyuuZeinukikingaku.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkKingaku(this,false)){objEBI('" & Me.buttonChgTourokuJituGaku.ClientID & "').click();}}else{if(checkKingaku(this,false));}")
        Me.TextKoumutenZeinukiKingaku.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkKingaku(this,false)){objEBI('" & Me.buttonChgTourokuKoumu.ClientID & "').click();}}else{if(checkKingaku(this,false));}")
        Me.TextJituseikyuuZeinukikingakuTool.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkKingaku(this,false)){objEBI('" & Me.buttonChgToolJituGaku.ClientID & "').click();}}else{if(checkKingaku(this,false));}")
        Me.TextSyouhizei.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkKingaku(this,false)){objEBI('" & Me.buttonChgSyouhiZei.ClientID & "').click();}}else{if(checkKingaku(this,false));}")
        Me.TextSyouhizeiTool.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkKingaku(this,false)){objEBI('" & Me.ButtonChgSyouhizeiTool.ClientID & "').click();}}else{if(checkKingaku(this,false));}")
        Me.TextUriageNengappi.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkDate(this)){objEBI('" & Me.buttonChgUriDate.ClientID & "').click();}}else{if(checkDate(this));}")
        Me.TextUriageNengappiTool.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkDate(this)){objEBI('" & Me.buttonChgUriDateTool.ClientID & "').click();}}else{if(checkDate(this));}")
        '伝票売上年月日（登録料）
        Me.TextDenpyouUriDate.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkDate(this)){objEBI('" & Me.buttonChgDenUriDate.ClientID & "').click();}}else{if(checkDate(this));}")
        '伝票売上年月日（初期ツール料）
        Me.TextDenpyouUriDateTool.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkDate(this)){objEBI('" & Me.buttonChgDenUriDateTool.ClientID & "').click();}}else{if(checkDate(this));}")

        '金額項目のMaxLengthを設定
        Me.TextHsSyouhizei.MaxLength = 7
        Me.TextHsZeikomiKingaku.MaxLength = 7
        Me.TextHsZeinukiGoukei.MaxLength = 7
        Me.TextJituseikyuuZeinukikingaku.MaxLength = 7
        Me.TextJituseikyuuZeinukikingakuTool.MaxLength = 7
        Me.TextKoumutenZeinukiKingaku.MaxLength = 7
        Me.TextSyouhizei.MaxLength = 7
        Me.TextSyouhizeiTool.MaxLength = 7
        Me.TextZeikomiKingaku.MaxLength = 7
        Me.TextZeikomiKingakuTool.MaxLength = 7
        '日付項目のMaxLengthを設定
        Me.TextHttourokuDate.MaxLength = 10
        Me.TextSeikyuusyoHakkouDate.MaxLength = 10
        Me.TextSeikyuusyoHakkouDateTool.MaxLength = 10
        Me.TextTourokuDate.MaxLength = 10
        Me.TextUriageNengappi.MaxLength = 10
        Me.TextUriageNengappiTool.MaxLength = 10
        Me.TextDenpyouUriDate.MaxLength = 10
        Me.TextDenpyouUriDateTool.MaxLength = 10

    End Sub

    ''' <summary>
    ''' ページロード時のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス
        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ユーザー基本認証
        jBn.UserAuth(user_info)
        If user_info Is Nothing Then
            'ログイン情報が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If
        '権限制御
        If user_info.EigyouMasterKanriKengen = 0 And user_info.HansokuUriageKengen = 0 Then
            '権限が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If IsPostBack Then
            '画面表示モードを復元
            enIsFc = Me.hiddenIsFc.Value
            enMode = Me.hiddenDispMode.Value

            '画面項目設定処理(ポストバック用)
            setDisplayPostBack()

            '画面共通情報をクラスへ引き渡す
            sendClassDispInfo(clsLogic)

        Else
            '画面表示モードを保持
            enMode = Str2Int(Request("tenmd"))
            enIsFc = Str2Int(Request("isfc"))
            Me.TextKameitenCd.Text = Request("kameicd")
            Me.hiddenMiseCd.Value = Request("kameicd")
            If Me.TextKameitenCd.Text = "" And Request("no") IsNot Nothing Then
                enMode = DisplayMode.SANSYOU
                enIsFc = Request("sendPageHidden1")
                Me.TextKameitenCd.Text = Request("no")
                Me.hiddenMiseCd.Value = Request("no")
            End If
            Me.hiddenIsFc.Value = enIsFc
            Me.hiddenDispMode.Value = enMode

            '画面項目設定処理
            setDisplay(sender)

            '表示切替リンク設定
            ChangeDisplay(Me.seikyuuSakiDispLink, Me.seikyuuSakiTbody)
            ChangeDisplay(Me.tourokuRyouDispLink, Me.tourokuRyouTbody)
            ChangeDisplay(Me.hansokuhinSyokiDispLink, Me.hansokuhinSyokiTbody)
            ChangeDisplay(Me.hansokuhinDispLink, Me.hansokuhinTbody)

            ' 取消理由テキストボックスのスタイルを設定
            cLogic.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, False)

            '画面モード設定
            Select Case enMode
                Case DisplayMode.TENBETU
                    '店別データ修正モード
                    Title = TITLE_TENBETU
                    PageTitleTh.InnerText = PAGE_TITLE_TENBETU
                    TourokubiHead.Visible = False
                    tourokubiData.Visible = False
                    tourokuBikou.Visible = False
                    HaisoubiHead.Visible = False
                    HaisoubiData.Visible = False

                    ZeinukiHead.Visible = False
                    SeikyuusakiRow.Visible = False

                    ' FCの場合
                    If enIsFc = IsFcMode.FC Then
                    End If

                    hansokuSyokiBikou.Visible = False
                    hansokuHead.Visible = False
                    UriageZumiRow.Visible = False
                    tdClose.Visible = False
                    tdTouroku1.Style("text-align") = "right"
                    tdTouroku2.Style("text-align") = "left"

                    'FC設定
                    If enIsFc = IsFcMode.FC Then
                        divTourokuryou.Visible = False
                        divHansokuSyoki.Visible = False
                        ' 区分、加盟店、系列、請求先を非表示化
                        KubunRow.Visible = False
                        KameitenRow.Visible = False
                        SeikyuusakiRow.Visible = False
                        TdKeiretuHead.Visible = False
                        TdKeiretu.Visible = False
                        ' 工務店請求額を非表示化
                        fcKoumutenHead.Visible = False

                        'リンクの活性化制御
                        Me.tourokuRyouTbody.Style("display") = "none"
                        Me.tourokuRyouDispLink.Disabled = True
                        Me.tourokuRyouDispLink.Attributes.Add("onclick", "")
                        Me.divTourokuryou.Style("display") = "none"
                        Me.hansokuhinSyokiTbody.Style("display") = "none"
                        Me.hansokuhinSyokiDispLink.Disabled = True
                        Me.hansokuhinSyokiDispLink.Attributes.Add("onclick", "")
                        Me.divHansokuSyoki.Style("display") = "none"
                        Me.buttonTouroku.Disabled = True
                        Me.buttonTourokuCall.Disabled = True
                    Else
                        If Me.hiddenKubun.Value = JHSFC Then
                            'リンクの活性化制御
                            Me.hansokuhinTbody.Style("display") = "none"
                            Me.hansokuhinDispLink.Disabled = True
                            Me.hansokuhinDispLink.Attributes.Add("onclick", "")
                            Me.divHansoku.Style("display") = "none"
                            Me.buttonTourokuHansoku.Disabled = True
                            Me.buttonTourokuHansokuCall.Disabled = True
                        Else
                            'リンクの活性化制御
                            Me.hansokuhinSyokiTbody.Style("display") = "none"
                            Me.hansokuhinSyokiDispLink.Disabled = True
                            Me.divHansokuSyoki.Style("display") = "none"
                            Me.hansokuhinSyokiDispLink.Attributes.Add("onclick", "")
                        End If
                    End If
                    '販促品の新規行追加機能を非活性化
                    Me.selectHansokuSyouhin.Enabled = False
                    Me.buttonAddRow.Disabled = True
                    Me.selectHansokuSyouhin.Visible = False
                    Me.buttonAddRow.Visible = False

                Case DisplayMode.HANSOKU
                    '販促品請求モード
                    Title = TITLE_HANSOKU
                    PageTitleTh.InnerText = PAGE_TITLE_HANSOKU
                    divTourokuryou.Visible = False
                    divHansokuSyoki.Visible = False
                    tdTouroku1.Visible = False
                    tdClose.Visible = False
                    KubunRow.Visible = False

                    divTourokuryou.Visible = False
                    divHansokuSyoki.Visible = False

                    TdKeiretuHead.Visible = False
                    TdKeiretu.Visible = False
                    TdEigyousyo.Attributes("colspan") = "3"

                    SeikyuusakiRow.Visible = True
                    ' FCの場合
                    If enIsFc = IsFcMode.FC Then
                        ' 加盟店を非表示化
                        KameitenRow.Visible = False
                        fcKoumutenHead.Visible = False
                    End If

                    '伝票売上年月日修正ヘッダ(コントロール内)
                    Me.thDenpyouUriDate.Visible = False

                Case DisplayMode.SANSYOU, 0
                    '売上処理済販促品参照モード
                    Title = TITLE_SANSYOU
                    PageTitleTh.InnerText = PAGE_TITLE_SANSYOU
                    divTourokuryou.Visible = False
                    divHansokuSyoki.Visible = False
                    buttonAddRow.Visible = False
                    lineActHead.Visible = False
                    UriageZumiRow.Visible = False
                    tdTouroku1.Visible = False
                    tdTouroku2.Visible = False
                    tdClose.Visible = True
                    KubunRow.Visible = False
                    keiretuTitle.Visible = False
                    TextKeiretuMei.Visible = False

                    divTourokuryou.Visible = False
                    divHansokuSyoki.Visible = False

                    TdKeiretuHead.Visible = False
                    TdKeiretu.Visible = False
                    TdEigyousyo.Attributes("colspan") = "3"

                    Me.seikyuuSakiDispLink.HRef = ""
                    Me.tourokuRyouDispLink.HRef = ""
                    Me.hansokuhinSyokiDispLink.HRef = ""
                    Me.hansokuhinDispLink.HRef = ""

                    Me.seikyuuSakiDispLink.Attributes.Add("onclick", "")
                    Me.tourokuRyouDispLink.Attributes.Add("onclick", "")
                    Me.hansokuhinSyokiDispLink.Attributes.Add("onclick", "")
                    Me.hansokuhinDispLink.Attributes.Add("onclick", "")

                    '伝票売上年月日修正ヘッダ(コントロール内)
                    Me.thDenpyouUriDate.Visible = False

                    SeikyuusakiRow.Visible = True
                    ' FCの場合
                    If enIsFc = IsFcMode.FC Then
                        ' 加盟店を非表示化
                        KameitenRow.Visible = False
                        ' 工務店請求額を非表示化
                        fcKoumutenHead.Visible = False
                    End If

                    Dim ht As New Hashtable
                    Dim MLogic As New MessageLogic
                    clsJSM.Hash2Ctrl(Me.divDisplayReadOnly, EarthConst.MODE_VIEW, ht)
                    Me.Span5.InnerText = ""
            End Select

            '画面読込み時の値をHidden項目に退避
            setOpenValues()
        End If
    End Sub

    ''' <summary>
    ''' ページロードコンプリート
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim lngJituGaku As Long
        Dim intSuuryou As Integer
        Dim lngZeinukiGaku As Long
        Dim lngZeiGaku As Long
        Dim lngZeikomiGaku As Long
        Dim lngKoumuZeiKomiGaku As Long
        Dim lngKoumuZeiNukiGaku As Long
        Dim decZeiritu As Decimal

        For Each ctrl As HansokuRecordCtrl In listHsRecCtrl
            If ctrl.AccSqlTypeFlg.Value <> HansokuRecordCtrl.enSqlTypeFlg.DELETE Then
                With ctrl
                    If .AccTextJituSeikyuuTanka.Value <> "" Then
                        lngJituGaku = Integer.Parse(.AccTextJituSeikyuuTanka.Value.Replace(",", ""))
                    End If
                    If .AccTextSuuRyou.Value <> "" Then
                        intSuuryou = Integer.Parse(.AccTextSuuRyou.Value.Replace(",", ""))
                    End If
                    If .AccTextSyouhizeiGaku.Value <> "" Then
                        lngZeiGaku += Long.Parse(.AccTextSyouhizeiGaku.Value.Replace(",", ""))
                    End If
                    If .AccTextZeikomiKingaku.Value <> "" Then
                        lngZeikomiGaku += Long.Parse(.AccTextZeikomiKingaku.Value.Replace(",", ""))
                    End If
                    If .AccTextKoumutenSeikyuuTanka.Value <> "" Then
                        lngKoumuZeiNukiGaku = Integer.Parse(.AccTextKoumutenSeikyuuTanka.Value.Replace(",", ""))
                        decZeiritu = .AccHiddenZeiritu.Value
                        lngKoumuZeiKomiGaku += Decimal.ToInt64(lngKoumuZeiNukiGaku * intSuuryou + Fix(lngKoumuZeiNukiGaku * intSuuryou * decZeiritu))
                    End If
                End With
                lngZeinukiGaku += lngJituGaku * intSuuryou
            End If
            Me.TextHsZeinukiGoukei.Text = lngZeinukiGaku.ToString
            Me.TextHsSyouhizei.Text = lngZeiGaku.ToString
            Me.TextHsZeikomiKingaku.Text = lngZeikomiGaku.ToString
            Me.hiddenKoumuTenSumAfter.Value = lngKoumuZeiKomiGaku.ToString
        Next

        '販促品の行数取得
        Me.hiddenHansokuLineCount.Value = listHsRecCtrl.Count.ToString

        '出力テキストのフォーマット
        Me.TextHsSyouhizei.Text = StrNum2Str(Me.TextHsSyouhizei.Text)
        Me.TextHsZeikomiKingaku.Text = StrNum2Str(Me.TextHsZeikomiKingaku.Text)
        Me.TextHsZeinukiGoukei.Text = StrNum2Str(Me.TextHsZeinukiGoukei.Text)
        Me.TextJituseikyuuZeinukikingaku.Text = StrNum2Str(Me.TextJituseikyuuZeinukikingaku.Text)
        Me.TextJituseikyuuZeinukikingakuTool.Text = StrNum2Str(Me.TextJituseikyuuZeinukikingakuTool.Text)
        Me.TextKoumutenZeinukiKingaku.Text = StrNum2Str(Me.TextKoumutenZeinukiKingaku.Text)
        Me.TextSyouhizei.Text = StrNum2Str(Me.TextSyouhizei.Text)
        Me.TextSyouhizeiTool.Text = StrNum2Str(Me.TextSyouhizeiTool.Text)
        Me.TextZeikomiKingaku.Text = StrNum2Str(Me.TextZeikomiKingaku.Text)
        Me.TextZeikomiKingakuTool.Text = StrNum2Str(Me.TextZeikomiKingakuTool.Text)

        '直接請求/他請求の表示
        If Me.hiddenHansokuHinSeikyuuSaki.Value = Me.TextKameitenCd.Text Then
            Me.TextSeikyuusaki.Text = SEIKYUU_TYOKU
        Else
            Me.TextSeikyuusaki.Text = SEIKYUU_HOKA
        End If
        '営業所の場合は常にFC請求
        If enIsFc = IsFcMode.FC Then
            Me.TextSeikyuusaki.Text = SEIKYUU_FC
        End If
    End Sub
#End Region

#Region "画面表示制御"
    ''' <summary>
    ''' 画面項目設定処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDisplay(ByVal sender As System.Object)
        Dim recInfo As New TenbetuDataSyuuseiRecord
        Dim recZumiInfo As New TenbetuDataSyuuseiRecord
        Dim helper As New DropDownHelper
        Dim MLogic As New MessageLogic
        Dim recSyouhin As New SyouhinMeisaiRecord
        Dim jLogic As New JibanLogic

        ' コンボ設定ヘルパークラスを生成
        Dim objDrpTmp As New DropDownList

        '画面共通情報をクラスへ引き渡す
        sendClassDispInfo(clsLogic)

        '画面項目取得
        Select Case enMode
            Case DisplayMode.TENBETU
                enUriFlg = UriFlg.NONE

                '各コンボの設定
                ' 商品コードコンボにデータをバインドする
                helper.SetDropDownList(Me.SelectSyouhinTourokuRyou, DropDownHelper.DropDownType.TourokuRyouSyouhin, True, True, 0, False)
                helper.SetDropDownList(Me.SelectSyouhinToolRyou, DropDownHelper.DropDownType.ToolRYouSyouhin, True, True, 0, False)
                If enIsFc = IsFcMode.NOT_FC Then
                    helper.SetDropDownList(Me.selectHansokuSyouhin, DropDownHelper.DropDownType.NotFcSyouhin, True, True, 0, False)
                Else
                    helper.SetDropDownList(Me.selectHansokuSyouhin, DropDownHelper.DropDownType.FcSyouhin, True, True, 0, False)
                End If

            Case DisplayMode.HANSOKU
                enUriFlg = UriFlg.MI

                '各コンボの設定
                ' 商品コードコンボにデータをバインドする
                helper.SetDropDownList(Me.SelectSyouhinTourokuRyou, DropDownHelper.DropDownType.TourokuRyouSyouhin)
                helper.SetDropDownList(Me.SelectSyouhinToolRyou, DropDownHelper.DropDownType.ToolRYouSyouhin)
                If enIsFc = IsFcMode.NOT_FC Then
                    helper.SetDropDownList(Me.selectHansokuSyouhin, DropDownHelper.DropDownType.NotFcSyouhin)
                Else
                    helper.SetDropDownList(Me.selectHansokuSyouhin, DropDownHelper.DropDownType.FcSyouhin)
                End If

            Case DisplayMode.SANSYOU
                enUriFlg = UriFlg.ZUMI

                '各コンボの設定
                ' 商品コードコンボにデータをバインドする
                helper.SetDropDownList(Me.SelectSyouhinTourokuRyou, DropDownHelper.DropDownType.TourokuRyouSyouhin)
                helper.SetDropDownList(Me.SelectSyouhinToolRyou, DropDownHelper.DropDownType.ToolRYouSyouhin)
                If enIsFc = IsFcMode.NOT_FC Then
                    helper.SetDropDownList(Me.selectHansokuSyouhin, DropDownHelper.DropDownType.NotFcSyouhin)
                Else
                    helper.SetDropDownList(Me.selectHansokuSyouhin, DropDownHelper.DropDownType.FcSyouhin)
                End If
        End Select
        recInfo = clsLogic.GetDisplayInfo(enUriFlg)
        If enMode = DisplayMode.HANSOKU Then
            recZumiInfo = clsLogic.GetDisplayInfo(UriFlg.ZUMI)
            If recZumiInfo.HansokuHinRecords Is Nothing OrElse recZumiInfo.HansokuHinRecords.Count = 0 Then
                Me.buttonUriageSyorizumi.Disabled = True
            Else
                Me.buttonUriageSyorizumi.Disabled = False
            End If
        End If

        With recInfo
            '請求先情報
            If .Kbn = "" AndAlso .KbnName = "" Then
                Me.TextKubun.Text = ""
                Me.hiddenKubun.Value = ""
            Else
                Me.TextKubun.Text = .Kbn & ":" & .KbnName
                Me.hiddenKubun.Value = .Kbn
            End If
            Me.TextKameitenCd.Text = .KameitenCd
            Me.TextKameitenMei.Text = .KameitenMei1
            Me.TextEigyousyoCd.Text = .EigyousyoCd
            Me.TextEigyousyoMei.Text = .EigyousyoMei
            Me.TextKeiretuCd.Text = .KeiretuCd
            Me.TextKeiretuMei.Text = .KeiretuMei

            Me.hiddenTyousaSeikyuuSaki.Value = .TysSeikyuuSaki
            Me.hiddenHansokuHinSeikyuuSaki.Value = .HansokuhinSeikyuusaki
            If .TourokuRyouRecord IsNot Nothing Then
                '登録料情報
                With .TourokuRyouRecord
                    If .UriKeijyouFlg = 1 Then
                        Me.SpanUriageSyorizumi.Style("display") = "inline"
                    Else
                        Me.SpanUriageSyorizumi.Style("display") = "none"
                    End If
                    Me.SelectSeikyuu.Value = .SeikyuuUmu

                    '存在チェック
                    If cLogic.ChkDropDownList(Me.SelectSyouhinTourokuRyou, cLogic.GetDisplayString(.SyouhinCd)) Then
                        Me.SelectSyouhinTourokuRyou.SelectedValue = .SyouhinCd
                    Else
                        recSyouhin = jLogic.GetSyouhinRecord(.SyouhinCd)

                        Me.SelectSyouhinTourokuRyou.Items.Add(New ListItem(.SyouhinCd & ":" & recSyouhin.SyouhinMei, .SyouhinCd))
                        Me.SelectSyouhinTourokuRyou.SelectedValue = .SyouhinCd  '選択状態
                    End If

                    Me.TextJituseikyuuZeinukikingaku.Text = .UriGaku
                    Me.hiddenTourokuUriGaku.Value = .UriGaku + .SyouhiZei
                    Me.TextSyouhizei.Text = .SyouhiZei
                    Me.TextZeikomiKingaku.Text = .UriGaku + .SyouhiZei
                    Me.TextKoumutenZeinukiKingaku.Text = .KoumutenSeikyuuGaku
                    Me.TextSeikyuusyoHakkouDate.Text = DtTime2Str(.SeikyuusyoHakDate)
                    Me.TextUriageNengappi.Text = DtTime2Str(.UriDate)
                    Me.TextDenpyouUriDate.Text = DtTime2Str(.DenpyouUriDate)
                    Me.TextDenpyouUriDateDisplay.Text = DtTime2Str(.DenpyouUriDate)
                    Me.hiddenZeiKbn.Value = .ZeiKbn
                    Me.hiddenTourokuUpdateTime.Value = DtTime2Str(.UpdDatetime, True)
                End With
            Else
                Me.SpanUriageSyorizumi.Style("display") = "none"
                Me.SelectSeikyuu.Value = 1
                Me.SelectSyouhinTourokuRyou.SelectedValue = ""
                Me.TextJituseikyuuZeinukikingaku.Text = ""
                Me.hiddenTourokuUriGaku.Value = ""
                Me.TextSyouhizei.Text = ""
                Me.TextZeikomiKingaku.Text = ""
                Me.TextKoumutenZeinukiKingaku.Text = ""
                Me.TextSeikyuusyoHakkouDate.Text = ""
                Me.TextUriageNengappi.Text = ""
                Me.TextDenpyouUriDate.Text = String.Empty
                Me.TextDenpyouUriDateDisplay.Text = String.Empty
                Me.hiddenZeiKbn.Value = ""
                Me.hiddenTourokuUpdateTime.Value = ""
            End If
            '画面自動設定判断
            judgeTourokuAutoSetting(Me.SelectSyouhinTourokuRyou.SelectedValue)
            '工務店税抜き金額活性制御
            activeControlKoumuten()

            If .ToolRyouRecord IsNot Nothing Then
                '販促品初期ツール料
                With .ToolRyouRecord
                    If .UriKeijyouFlg = 1 Then
                        Me.SpanUriagesyorizumiTool.Style("display") = "inline"
                    Else
                        Me.SpanUriagesyorizumiTool.Style("display") = "none"
                    End If
                    Me.SelectSeikyuuTool.Value = .SeikyuuUmu

                    '存在チェック
                    If cLogic.ChkDropDownList(Me.SelectSyouhinToolRyou, cLogic.GetDisplayString(.SyouhinCd)) Then
                        Me.SelectSyouhinToolRyou.SelectedValue = .SyouhinCd
                    Else
                        recSyouhin = jLogic.GetSyouhinRecord(.SyouhinCd)

                        Me.SelectSyouhinToolRyou.Items.Add(New ListItem(.SyouhinCd & ":" & recSyouhin.SyouhinMei, .SyouhinCd))
                        Me.SelectSyouhinToolRyou.SelectedValue = .SyouhinCd  '選択状態
                    End If
                    Me.TextJituseikyuuZeinukikingakuTool.Text = .UriGaku
                    Me.hiddenToolUriGaku.Value = .UriGaku + .SyouhiZei
                    Me.TextSyouhizeiTool.Text = .SyouhiZei
                    Me.TextZeikomiKingakuTool.Text = .UriGaku + .SyouhiZei
                    Me.TextSeikyuusyoHakkouDateTool.Text = DtTime2Str(.SeikyuusyoHakDate)
                    Me.TextUriageNengappiTool.Text = DtTime2Str(.UriDate)
                    Me.TextDenpyouUriDateTool.Text = DtTime2Str(.DenpyouUriDate)
                    Me.TextDenpyouUriDateDisplayTool.Text = DtTime2Str(.DenpyouUriDate)
                    Me.hiddenZeiKbnTool.Value = .ZeiKbn
                    Me.hiddenToolUpdateTime.Value = DtTime2Str(.UpdDatetime, True)
                End With
            Else
                Me.SpanUriagesyorizumiTool.Style("display") = "none"
                Me.SelectSeikyuuTool.Value = 1
                Me.SelectSyouhinToolRyou.SelectedValue = ""
                Me.TextJituseikyuuZeinukikingakuTool.Text = ""
                Me.hiddenToolUriGaku.Value = ""
                Me.TextSyouhizeiTool.Text = ""
                Me.TextZeikomiKingakuTool.Text = ""
                Me.TextSeikyuusyoHakkouDateTool.Text = ""
                Me.TextUriageNengappiTool.Text = ""
                Me.TextDenpyouUriDateTool.Text = String.Empty
                Me.TextDenpyouUriDateDisplayTool.Text = String.Empty
                Me.hiddenZeiKbnTool.Value = ""
                Me.hiddenToolUpdateTime.Value = ""
            End If
            If sender.id <> buttonTouroku.ID Then
                '登録（登録料・販促初期ツール料）押下時以外の場合のみ、販促品商品表示テーブルをDBデータから再表示
                '販促品商品表示テーブルをクリア
                Dim tmpHead As LiteralControl = searchGrid.Controls.Item(0)
                searchGrid.Controls.Clear()
                searchGrid.Controls.Add(tmpHead)
                listHsRecCtrl = New List(Of HansokuRecordCtrl)

                If .HansokuHinRecords IsNot Nothing Then
                    '販促品再設定
                    With .HansokuHinRecords(0)
                        Me.TextHsZeinukiGoukei.Text = .SumTanka
                        'Me.TextHsSyouhizei.Text = .SumSyouhiZei
                        'Me.TextHsZeikomiKingaku.Text = .SumZeikomiGaku
                        'Me.hiddenHsZeikomiGoukeiBefor.Value = .SumZeikomiGaku
                        'Me.hiddenKoumuTenSumBefor.Value = .SumKoumuGaku
                    End With
                    '販促品行作成
                    createHansokuRow(.HansokuHinRecords.Count)
                    '販促品行に値をセット
                    setValueHansokuRow(.HansokuHinRecords)
                Else
                    Me.TextHsZeinukiGoukei.Text = ""
                    Me.TextHsSyouhizei.Text = ""
                    Me.TextHsZeikomiKingaku.Text = ""
                    If enMode = DisplayMode.SANSYOU Then
                        TableResizeBeforAlert(sender)
                        MLogic.AlertMessage(sender, Messages.MSG020E)
                    End If
                End If
            End If
        End With

        If enIsFc = IsFcMode.NOT_FC Then

        End If
        '加盟店取消理由設定
        setTorikesiRiyuu(recInfo.Kbn, recInfo.KameitenCd)

        '画面読込み時の値をHidden項目に退避
        setOpenValues()

    End Sub

    ''' <summary>
    ''' 画面項目設定処理(ポストバック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDisplayPostBack()
        Dim intMakeCnt As Integer
        '販促行の取得
        intMakeCnt = Integer.Parse(Me.hiddenHansokuLineCount.Value)
        '販促品行作成
        createHansokuRow(intMakeCnt)
    End Sub

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckInputTouroku() As Boolean
        'エラーメッセージ初期化
        Dim strErrMsg As String = ""
        Dim arrFocusTargetCtrl As New ArrayList
        Dim strSetFocus As String = ""
        '月次処理確定年月
        Dim dtGetujiKakuteiLastSyoriDate As DateTime = DateTime.MinValue

        'DB読み込み時点の値を、現在画面の値と比較(変更有無チェック)
        If Me.HiddenOpenTourokuRyouValues.Value <> String.Empty Then
            'DB読み込み時点の値が空の場合は比較対象外
            '比較実施
            If HiddenOpenTourokuRyouValues.Value <> getTourokuCtrlValuesString() Then
                '月次確定予約済みの処理年月「以前」の日付で伝票売上年月日を設定するのはエラー
                If cLogic.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouUriDate.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                    strErrMsg += String.Format(Messages.MSG176E, dtGetujiKakuteiLastSyoriDate.Month, Me.tourokuRyouDispLink.InnerText & ERR_STRING, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                    arrFocusTargetCtrl.Add(Me.TextDenpyouUriDate)
                End If
            End If
        End If
        If Me.hiddenKubun.Value = "A" AndAlso Me.HiddenOpenToolRyouValues.Value <> String.Empty Then
            'DB読み込み時点の値が空の場合は比較対象外
            '比較実施
            If HiddenOpenToolRyouValues.Value <> getToolCtrlValuesString() Then

                '月次確定予約済みの処理年月「以前」の日付で伝票売上年月日を設定するのはエラー
                If cLogic.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouUriDateTool.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                    strErrMsg += String.Format(Messages.MSG176E, dtGetujiKakuteiLastSyoriDate.Month, Me.hansokuhinSyokiDispLink.InnerText & ERR_STRING, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                    arrFocusTargetCtrl.Add(Me.TextDenpyouUriDateTool)
                End If
            End If
        End If

        '●入力時必須チェック（売上日ないのに伝票日がある）
        '伝票売上年月日（登録料）
        If Me.TextUriageNengappi.Text = String.Empty And Me.TextDenpyouUriDate.Text <> String.Empty Then
            strErrMsg += Messages.MSG153E.Replace("@PARAM1", "伝票売上年月日").Replace("@PARAM2", "売上年月日")
            arrFocusTargetCtrl.Add(Me.TextUriageNengappi)
        End If
        '伝票売上年月日（初期ツール料）
        If Me.TextUriageNengappiTool.Text = String.Empty And Me.TextDenpyouUriDateTool.Text <> String.Empty Then
            strErrMsg += Messages.MSG153E.Replace("@PARAM1", "伝票売上年月日").Replace("@PARAM2", "売上年月日")
            arrFocusTargetCtrl.Add(Me.TextUriageNengappiTool)
        End If

        '●未計上時チェック（売上日と伝票日に差異がある）
        '伝票売上年月日（登録料）
        If Me.SpanUriageSyorizumi.Style("display") = "none" Then
            If Me.TextUriageNengappi.Text <> Me.TextDenpyouUriDate.Text Then
                strErrMsg += Messages.MSG144E.Replace("@PARAM1", "伝票売上年月日").Replace("@PARAM2", "売上年月日").Replace("@PARAM3", "更新")
                arrFocusTargetCtrl.Add(Me.TextUriageNengappi)
            End If
        End If
        '伝票売上年月日（初期ツール料）
        If Me.SpanUriagesyorizumiTool.Style("display") = "none" Then
            If Me.TextUriageNengappiTool.Text <> Me.TextDenpyouUriDateTool.Text Then
                strErrMsg += Messages.MSG144E.Replace("@PARAM1", "伝票売上年月日").Replace("@PARAM2", "売上年月日").Replace("@PARAM3", "更新")
                arrFocusTargetCtrl.Add(Me.TextUriageNengappiTool)
            End If
        End If

        '必須チェック
        If Me.SelectSyouhinTourokuRyou.SelectedIndex = 0 Then
            strErrMsg += Messages.MSG013E.Replace(PARAM1, "商品コード")
            arrFocusTargetCtrl.Add(Me.SelectSyouhinTourokuRyou)
        End If
        If Me.hiddenKubun.Value = "A" AndAlso Me.SelectSyouhinToolRyou.SelectedIndex = 0 Then
            strErrMsg += Messages.MSG013E.Replace(PARAM1, "商品コード")
            arrFocusTargetCtrl.Add(Me.SelectSyouhinToolRyou)
        End If

        '日付チェック
        If Me.TextSeikyuusyoHakkouDate.Text <> "" Then
            If DateTime.Parse(Me.TextSeikyuusyoHakkouDate.Text) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(Me.TextSeikyuusyoHakkouDate.Text) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG088E.Replace(PARAM1, "請求書発行日")
                arrFocusTargetCtrl.Add(Me.TextSeikyuusyoHakkouDate)
            End If
        End If
        If Me.TextUriageNengappi.Text <> "" Then
            If DateTime.Parse(Me.TextUriageNengappi.Text) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(Me.TextUriageNengappi.Text) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG040E.Replace(PARAM1, "売上年月日")
                arrFocusTargetCtrl.Add(Me.TextUriageNengappi)
            End If
        End If
        If Me.TextDenpyouUriDate.Text <> String.Empty Then
            If DateTime.Parse(Me.TextDenpyouUriDate.Text) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(Me.TextDenpyouUriDate.Text) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG040E.Replace(PARAM1, "伝票売上年月日")
            End If
        End If
        If Me.TextKubun.Text = "A" AndAlso Me.TextSeikyuusyoHakkouDateTool.Text <> "" Then
            If DateTime.Parse(Me.TextSeikyuusyoHakkouDateTool.Text) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(Me.TextSeikyuusyoHakkouDateTool.Text) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG088E.Replace(PARAM1, "請求書発行日")
                arrFocusTargetCtrl.Add(Me.TextSeikyuusyoHakkouDateTool)
            End If
        End If
        If Me.TextKubun.Text = "A" AndAlso Me.TextUriageNengappiTool.Text <> "" Then
            If DateTime.Parse(Me.TextUriageNengappiTool.Text) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(Me.TextUriageNengappiTool.Text) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG040E.Replace(PARAM1, "売上年月日")
                arrFocusTargetCtrl.Add(Me.TextUriageNengappiTool)
            End If
        End If
        If Me.TextKubun.Text = "A" AndAlso Me.TextDenpyouUriDateTool.Text <> "" Then
            If DateTime.Parse(Me.TextDenpyouUriDateTool.Text) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(Me.TextDenpyouUriDateTool.Text) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG040E.Replace(PARAM1, "伝票売上年月日")
                arrFocusTargetCtrl.Add(Me.TextUriageNengappiTool)
            End If
        End If
        'マイナス値チェック
        If Str2Int(Me.TextJituseikyuuZeinukikingaku.Text.Replace(",", "")) < 0 Then
            strErrMsg += String.Format(Messages.MSG091E, "実請求単価", "金額")
        End If
        If Str2Int(Me.TextJituseikyuuZeinukikingakuTool.Text.Replace(",", "")) < 0 Then
            strErrMsg += String.Format(Messages.MSG091E, "実請求単価", "金額")
        End If
        If Str2Int(Me.TextKoumutenZeinukiKingaku.Text.Replace(",", "")) < 0 Then
            strErrMsg += String.Format(Messages.MSG091E, "工務店請求金額", "金額")
        End If
        '桁数チェック
        If Str2Int(Me.TextJituseikyuuZeinukikingaku.Text.Replace(",", "")) > 99999999 Then
            strErrMsg += String.Format(Messages.MSG092E, "実請求単価", "8")
        End If
        If Str2Int(Me.TextJituseikyuuZeinukikingakuTool.Text.Replace(",", "")) > 99999999 Then
            strErrMsg += String.Format(Messages.MSG092E, "実請求単価", "8")
        End If
        If Str2Int(Me.TextKoumutenZeinukiKingaku.Text.Replace(",", "")) > 99999999 Then
            strErrMsg += String.Format(Messages.MSG092E, "工務店請求金額", "8")
        End If

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If strErrMsg <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'テーブルサイズ再設定
            TableResizeBeforAlert(Me)
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If
        'エラー無しの場合、Trueを返す
        Return True
    End Function

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckInputHansoku(ByVal ctrlHansoku As HansokuRecordCtrl) As Boolean
        'エラーメッセージ初期化
        Dim strErrMsg As String = ""
        Dim arrFocusTargetCtrl As New ArrayList
        Dim strSetFocus As String = ""
        '月次処理確定年月
        Dim dtGetujiKakuteiLastSyoriDate As DateTime = DateTime.MinValue

        With ctrlHansoku
            If .AccTextSyouhinCd.Value <> String.Empty AndAlso .AccHiddenOpenValues.Value <> String.Empty Then
                'DB読み込み時点の値が空の場合は比較対象外
                '比較実施
                If .AccHiddenOpenValues.Value <> .getCtrlValuesString() Then

                    '月次確定予約済みの処理年月「以前」の日付で伝票売上年月日を設定するのはエラー
                    If cLogic.chkGetujiKakuteiYoyakuzumi(.AccTextDenpyouUriDate.Value, dtGetujiKakuteiLastSyoriDate) = False Then
                        strErrMsg += String.Format(Messages.MSG176E, dtGetujiKakuteiLastSyoriDate.Month, Me.hansokuhinDispLink.InnerText & ERR_STRING, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                        arrFocusTargetCtrl.Add(.AccTextDenpyouUriDate)
                    End If
                End If
            End If

            '店別データ修正の場合のみチェック
            If enMode = DisplayMode.TENBETU Then
                '●入力時必須チェック(売上日ないのに伝票日がある)
                If .AccTextUriageNengappi.Value = String.Empty And .AccTextDenpyouUriDate.Value <> String.Empty Then
                    strErrMsg += Messages.MSG153E.Replace("@PARAM1", "伝票売上年月日").Replace("@PARAM2", "売上年月日")
                    arrFocusTargetCtrl.Add(.AccTextUriageNengappi)
                End If
                '●未計上時チェック(売上日と伝票日に差異がある)
                If Not String.IsNullOrEmpty(.AccHiddenUriKeijyouFlg.Value) AndAlso .AccHiddenUriKeijyouFlg.Value = 0 Then
                    If .AccTextUriageNengappi.Value <> .AccTextDenpyouUriDate.Value Then
                        strErrMsg += Messages.MSG144E.Replace("@PARAM1", "伝票売上年月日").Replace("@PARAM2", "売上年月日").Replace("@PARAM3", "更新")
                        arrFocusTargetCtrl.Add(.AccTextUriageNengappi)
                    End If
                End If
            End If

            If .AccSqlTypeFlg.Value <> 9 Then
                '削除以外の場合のみ、チェック
                '必須チェック
                If .AccTextSyouhinCd.Value = "" Then
                    strErrMsg += Messages.MSG013E.Replace(PARAM1, "商品コード")
                    arrFocusTargetCtrl.Add(.AccTextSyouhinCd)
                End If
                If .AccTextHassouDate.Value = "" Then
                    strErrMsg += Messages.MSG013E.Replace(PARAM1, "発送日")
                    arrFocusTargetCtrl.Add(.AccTextHassouDate)
                End If
                If .AccTextSeikyuusyoHakkouBi.Value = "" Then
                    strErrMsg += Messages.MSG013E.Replace(PARAM1, "請求書発行日")
                    arrFocusTargetCtrl.Add(.AccTextSeikyuusyoHakkouBi)
                End If
                'マイナス値チェック
                If Str2Int(.AccTextJituSeikyuuTanka.Value.Replace(",", "")) < 0 Then
                    strErrMsg += String.Format(Messages.MSG091E, "実請求単価", "金額")
                    arrFocusTargetCtrl.Add(.AccTextJituSeikyuuTanka)
                End If
                If Str2Int(.AccTextKoumutenSeikyuuTanka.Value.Replace(",", "")) < 0 Then
                    strErrMsg += String.Format(Messages.MSG091E, "工務店請求単価", "金額")
                    arrFocusTargetCtrl.Add(.AccTextKoumutenSeikyuuTanka)
                End If
                If Str2Int(.AccTextSuuRyou.Value.Replace(",", "")) < 0 Then
                    strErrMsg += String.Format(Messages.MSG091E, "数量", "値")
                    arrFocusTargetCtrl.Add(.AccTextSuuRyou)
                End If
                '桁数チェック
                If Str2Int(.AccTextJituSeikyuuTanka.Value.Replace(",", "")) > 99999999 Then
                    strErrMsg += String.Format(Messages.MSG092E, "実請求単価", "8")
                    arrFocusTargetCtrl.Add(.AccTextJituSeikyuuTanka)
                End If
                If Str2Int(.AccTextKoumutenSeikyuuTanka.Value.Replace(",", "")) > 99999999 Then
                    strErrMsg += String.Format(Messages.MSG092E, "工務店請求単価", "8")
                    arrFocusTargetCtrl.Add(.AccTextKoumutenSeikyuuTanka)
                End If
                If Str2Int(.AccTextSuuRyou.Value.Replace(",", "")) > 999999 Then
                    strErrMsg += String.Format(Messages.MSG092E, "数量", "6")
                    arrFocusTargetCtrl.Add(.AccTextSuuRyou)
                End If
                '日付チェック
                If .AccTextHassouDate.Value <> "" Then
                    If DateTime.Parse(.AccTextHassouDate.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(.AccTextHassouDate.Value) < EarthConst.Instance.MIN_DATE Then
                        strErrMsg += Messages.MSG040E.Replace(PARAM1, "発送日")
                        arrFocusTargetCtrl.Add(.AccTextHassouDate)
                    End If
                End If
                If .AccTextUriageNengappi.Value <> "" Then
                    If DateTime.Parse(.AccTextUriageNengappi.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(.AccTextUriageNengappi.Value) < EarthConst.Instance.MIN_DATE Then
                        strErrMsg += Messages.MSG040E.Replace(PARAM1, "売上年月日")
                        arrFocusTargetCtrl.Add(.AccTextUriageNengappi)
                    End If
                End If
                If .AccTextSeikyuusyoHakkouBi.Value <> "" Then
                    If DateTime.Parse(.AccTextSeikyuusyoHakkouBi.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(.AccTextSeikyuusyoHakkouBi.Value) < EarthConst.Instance.MIN_DATE Then
                        strErrMsg += Messages.MSG040E.Replace(PARAM1, "請求書発行日")
                        arrFocusTargetCtrl.Add(.AccTextSeikyuusyoHakkouBi)
                    End If
                End If
                If .AccTextDenpyouUriDate.Value <> "" Then
                    If DateTime.Parse(.AccTextDenpyouUriDate.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(.AccTextDenpyouUriDate.Value) < EarthConst.Instance.MIN_DATE Then
                        strErrMsg += Messages.MSG040E.Replace(PARAM1, "伝票売上年月日")
                        arrFocusTargetCtrl.Add(.AccTextDenpyouUriDate)
                    End If
                End If
            End If
        End With
        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If strErrMsg <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                SetFocusAjax(arrFocusTargetCtrl(0))
            End If
            'テーブルサイズ再設定
            TableResizeBeforAlert(Me)
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If
        'エラー無しの場合、Trueを返す
        Return True
    End Function

    ''' <summary>
    ''' 取消理由の設定
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <remarks></remarks>
    Private Sub setTorikesiRiyuu(ByVal strKbn As String, ByVal strKameitenCd As String)

        '色替え処理対象のコントロールを配列に格納(※取消理由テキストボックス以外)
        Dim objArray() As Object = New Object() {Me.TextKubun, Me.TextKameitenCd, Me.TextKameitenMei, _
                                                 Me.TextEigyousyoCd, Me.TextEigyousyoMei, Me.TextKeiretuCd, Me.TextKeiretuMei, Me.TextSeikyuusaki}

        '取消理由と加盟店情報の文字色設定
        cLogic.GetKameitenTorikesiRiyuu(strKbn _
                                        , strKameitenCd _
                                        , Me.TextTorikesiRiyuu _
                                        , True _
                                        , False _
                                        , objArray)

    End Sub
#End Region

#Region "画面項目変更時制御"
    ''' <summary>
    ''' 登録料－請求有無変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgTourokuSeikyuu_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgTourokuSeikyuu.ServerClick
        Dim clsJibanL As New JibanLogic
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        Dim intJituGaku As Integer
        Dim intZei As Integer
        Dim intZeiKomiGaku As Integer
        Dim intKoumuGaku As Integer
        Dim blnFlg As Boolean
        Dim MLogic As New MessageLogic
        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        'フォーカスの設定
        SetFocusAjax(Me.SelectSeikyuu)

        '金額変更イベント制御（登録）設定
        Me.hiddenTxtChgCtrlTouroku.Value = enTxtChgCtrlTouroku.Reset.ToString

        '金額取得
        If Me.TextJituseikyuuZeinukikingaku.Text <> "" Then
            intJituGaku = CInt(Me.TextJituseikyuuZeinukikingaku.Text)
        End If
        If Me.TextKoumutenZeinukiKingaku.Text <> "" Then
            intKoumuGaku = CInt(Me.TextKoumutenZeinukiKingaku.Text)
        End If

        '画面自動設定判断
        judgeTourokuAutoSetting(Me.SelectSyouhinTourokuRyou.SelectedValue)

        If eUmu = enSeikyuuUmu.Nasi Then
            If Me.TextKubun.Text = "A" Then
                Me.SelectSyouhinTourokuRyou.SelectedValue = "J0099"
            Else
                Me.SelectSyouhinTourokuRyou.SelectedValue = "C0099"
            End If
        End If

        If Me.SelectSyouhinTourokuRyou.SelectedIndex = 0 Then
            Exit Sub
        End If

        '商品明細の取得
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.SelectSyouhinTourokuRyou.SelectedValue, SOUKO_CD_TOUROKU)

        '売上年月日で判断して、正しい税率を取得する
        strSyouhinCd = Me.SelectSyouhinTourokuRyou.SelectedValue
        If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '取得した税区分・税率をセット
            recSyouhinMeisai.Zeiritu = strZeiritu
            recSyouhinMeisai.ZeiKbn = strZeiKbn
        End If

        '自動設定パターン判断
        Select Case True
            Case recSyouhinMeisai Is Nothing
                'テーブルサイズ再設定
                TableResizeBeforAlert(Me)
                MLogic.AlertMessage(sender, Messages.MSG001E)
                Exit Sub

            Case eUmu = enSeikyuuUmu.Ari _
             And eSCd = enSyouhinCd.Zumi _
             And eSaki = enSeikyuuSaki.Tyoku
                intKoumuGaku = recSyouhinMeisai.HyoujunKkk
                intJituGaku = intKoumuGaku
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case eUmu = enSeikyuuUmu.Ari _
             And eSCd = enSyouhinCd.Zumi _
             And eSaki = enSeikyuuSaki.Hoka _
             And eKretu = enKeiretu.SanK
                intKoumuGaku = recSyouhinMeisai.HyoujunKkk
                If intKoumuGaku = 0 Then
                    intJituGaku = 0
                Else
                    blnFlg = clsJibanL.GetSeikyuuGaku(sender _
                                                    , 3 _
                                                    , Me.TextKeiretuCd.Text _
                                                    , Me.SelectSyouhinTourokuRyou.SelectedValue _
                                                    , -1 _
                                                    , intJituGaku)
                End If
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case eUmu = enSeikyuuUmu.Ari _
             And eSCd = enSyouhinCd.Zumi _
             And eSaki = enSeikyuuSaki.Hoka _
             And eKretu = enKeiretu.NotSanK
                intKoumuGaku = 0
                intJituGaku = recSyouhinMeisai.HyoujunKkk
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case eUmu = enSeikyuuUmu.Nasi
                If Me.TextKubun.Text = "A" Then
                    Me.SelectSyouhinTourokuRyou.SelectedValue = "J0099"
                Else
                    Me.SelectSyouhinTourokuRyou.SelectedValue = "C0099"
                End If

                '画面自動設定判断
                judgeTourokuAutoSetting(Me.SelectSyouhinTourokuRyou.SelectedValue)

                '商品コードが変わるので商品明細の再取得
                recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.SelectSyouhinTourokuRyou.SelectedValue, SOUKO_CD_TOUROKU)
                intKoumuGaku = recSyouhinMeisai.HyoujunKkk
                intJituGaku = intKoumuGaku
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case Else
                Exit Sub

        End Select

        Me.TextJituseikyuuZeinukikingaku.Text = intJituGaku
        Me.TextSyouhizei.Text = intZei
        Me.TextZeikomiKingaku.Text = intZeiKomiGaku
        Me.TextKoumutenZeinukiKingaku.Text = intKoumuGaku

        '税区分の設定
        Me.hiddenZeiKbn.Value = recSyouhinMeisai.ZeiKbn

        '工務店税抜き金額活性制御
        activeControlKoumuten()

    End Sub

    ''' <summary>
    ''' 登録料－商品CD変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgTourokuSyouhin_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgTourokuSyouhin.ServerClick
        Dim intJituGaku As Integer
        Dim intZei As Integer
        Dim intZeiKomiGaku As Integer
        Dim intKoumuGaku As Integer
        Dim clsJibanL As New JibanLogic
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        Dim blnFlg As Boolean
        Dim MLogic As New MessageLogic
        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        'フォーカスの設定
        SetFocusAjax(Me.SelectSyouhinTourokuRyou)

        '金額変更イベント制御（登録）設定
        Me.hiddenTxtChgCtrlTouroku.Value = enTxtChgCtrlTouroku.Reset.ToString

        If Me.SelectSyouhinTourokuRyou.SelectedIndex = 0 Then
            Me.TextJituseikyuuZeinukikingaku.Text = ""
            Me.TextSyouhizei.Text = ""
            Me.TextZeikomiKingaku.Text = ""
            Me.TextKoumutenZeinukiKingaku.Text = ""
            Me.hiddenZeiKbn.Value = ""
            Exit Sub
        End If

        '金額取得
        If Me.TextJituseikyuuZeinukikingaku.Text <> "" Then
            intJituGaku = CInt(Me.TextJituseikyuuZeinukikingaku.Text)
        End If
        If Me.TextKoumutenZeinukiKingaku.Text <> "" Then
            intKoumuGaku = CInt(Me.TextKoumutenZeinukiKingaku.Text)
        End If

        '画面自動設定判断
        judgeTourokuAutoSetting(Me.SelectSyouhinTourokuRyou.SelectedValue)

        '商品明細の取得
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.SelectSyouhinTourokuRyou.SelectedValue, SOUKO_CD_TOUROKU)

        '売上年月日で判断して、正しい税率を取得する
        strSyouhinCd = Me.SelectSyouhinTourokuRyou.SelectedValue
        If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '取得した税区分・税率をセット
            recSyouhinMeisai.Zeiritu = strZeiritu
            recSyouhinMeisai.ZeiKbn = strZeiKbn
        End If

        '自動設定パターン判断
        Select Case True
            Case recSyouhinMeisai Is Nothing
                'テーブルサイズ再設定
                TableResizeBeforAlert(Me)
                MLogic.AlertMessage(sender, Messages.MSG001E)
                Exit Sub

            Case eUmu = enSeikyuuUmu.Ari _
             And eSCd = enSyouhinCd.Zumi _
             And eSaki = enSeikyuuSaki.Tyoku
                intKoumuGaku = recSyouhinMeisai.HyoujunKkk
                intJituGaku = intKoumuGaku
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case eUmu = enSeikyuuUmu.Ari _
             And eSCd = enSyouhinCd.Zumi _
             And eSaki = enSeikyuuSaki.Hoka _
             And eKretu = enKeiretu.SanK
                intKoumuGaku = recSyouhinMeisai.HyoujunKkk
                If intKoumuGaku = 0 Then
                    intJituGaku = 0
                Else
                    blnFlg = clsJibanL.GetSeikyuuGaku(sender _
                                                    , 3 _
                                                    , Me.TextKeiretuCd.Text _
                                                    , Me.SelectSyouhinTourokuRyou.SelectedValue _
                                                    , -1 _
                                                    , intJituGaku)
                End If
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case eUmu = enSeikyuuUmu.Ari _
             And eSCd = enSyouhinCd.Zumi _
             And eSaki = enSeikyuuSaki.Hoka _
             And eKretu = enKeiretu.NotSanK
                intKoumuGaku = 0
                intJituGaku = recSyouhinMeisai.HyoujunKkk
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei
            Case Else
                '税区分の設定
                Me.hiddenZeiKbn.Value = recSyouhinMeisai.ZeiKbn
                Exit Sub

        End Select

        Me.TextJituseikyuuZeinukikingaku.Text = intJituGaku
        Me.TextSyouhizei.Text = intZei
        Me.TextZeikomiKingaku.Text = intZeiKomiGaku
        Me.TextKoumutenZeinukiKingaku.Text = intKoumuGaku

        '税区分の設定
        Me.hiddenZeiKbn.Value = recSyouhinMeisai.ZeiKbn

        '工務店税抜き金額活性制御
        activeControlKoumuten()

    End Sub

    ''' <summary>
    ''' 登録料－実請求税抜金額変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgTourokuJituGaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgTourokuJituGaku.ServerClick
        Dim intJituGaku As Integer
        Dim intZei As Integer
        Dim intZeiKomiGaku As Integer
        Dim intKoumuGaku As Integer
        Dim clsJibanL As New JibanLogic
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        Dim blnFlg As Boolean
        Dim MLogic As New MessageLogic
        Dim blnActCtrlKoumu As Boolean
        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        '画面自動設定判断
        judgeTourokuAutoSetting(Me.SelectSyouhinTourokuRyou.SelectedValue)
        '工務店税抜き金額活性制御
        blnActCtrlKoumu = activeControlKoumuten()

        'フォーカスの設定
        SetFocusAjax(Me.TextSyouhizei)

        '金額変更イベント制御（登録）設定
        If Me.hiddenTxtChgCtrlTouroku.Value = enTxtChgCtrlTouroku.KoumuGaku.ToString Then
            Exit Sub
        Else
            Me.hiddenTxtChgCtrlTouroku.Value = enTxtChgCtrlTouroku.JituGaku.ToString
        End If

        '金額取得
        If Me.TextJituseikyuuZeinukikingaku.Text <> "" Then
            intJituGaku = CInt(Me.TextJituseikyuuZeinukikingaku.Text)
        End If
        If Me.TextKoumutenZeinukiKingaku.Text <> "" Then
            intKoumuGaku = CInt(Me.TextKoumutenZeinukiKingaku.Text)
        End If

        If Me.SelectSyouhinTourokuRyou.SelectedIndex = 0 Then
            Exit Sub
        End If

        '商品明細の取得
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.SelectSyouhinTourokuRyou.SelectedValue, SOUKO_CD_TOUROKU)

        '売上年月日で判断して、正しい税率を取得する
        strSyouhinCd = Me.SelectSyouhinTourokuRyou.SelectedValue
        If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '取得した税区分・税率をセット
            recSyouhinMeisai.Zeiritu = strZeiritu
            recSyouhinMeisai.ZeiKbn = strZeiKbn
        End If

        '自動設定パターン判断
        Select Case True
            Case recSyouhinMeisai Is Nothing
                'テーブルサイズ再設定
                TableResizeBeforAlert(Me)
                MLogic.AlertMessage(sender, Messages.MSG001E)
                Exit Sub

            Case eUmu = enSeikyuuUmu.Ari _
             And eSCd = enSyouhinCd.Zumi _
             And eSaki = enSeikyuuSaki.Tyoku
                intKoumuGaku = CInt(IIf(Me.TextJituseikyuuZeinukikingaku.Text = String.Empty, 0, Me.TextJituseikyuuZeinukikingaku.Text))
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case eUmu = enSeikyuuUmu.Ari _
             And eSCd = enSyouhinCd.Zumi _
             And eSaki = enSeikyuuSaki.Hoka _
             And eKretu = enKeiretu.SanK
                blnFlg = clsJibanL.GetSeikyuuGaku(sender _
                                                , 4 _
                                                , Me.TextKeiretuCd.Text _
                                                , Me.SelectSyouhinTourokuRyou.SelectedValue _
                                                , intJituGaku _
                                                , intKoumuGaku)
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case eUmu = enSeikyuuUmu.Ari _
             And eSCd = enSyouhinCd.Zumi _
             And eSaki = enSeikyuuSaki.Hoka _
             And eKretu = enKeiretu.NotSanK
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case eUmu = enSeikyuuUmu.Nasi
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case Else
                Exit Sub
        End Select

        Me.TextJituseikyuuZeinukikingaku.Text = intJituGaku
        Me.TextSyouhizei.Text = intZei
        Me.TextZeikomiKingaku.Text = intZeiKomiGaku
        Me.TextKoumutenZeinukiKingaku.Text = intKoumuGaku

        '税区分の設定
        Me.hiddenZeiKbn.Value = recSyouhinMeisai.ZeiKbn

    End Sub

    ''' <summary>
    ''' 登録料－工務店税抜金額変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgTourokuKoumu_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgTourokuKoumu.ServerClick
        Dim intJituGaku As Integer
        Dim intZei As Integer
        Dim intZeiKomiGaku As Integer
        Dim intKoumuGaku As Integer
        Dim clsJibanL As New JibanLogic
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        Dim blnFlg As Boolean
        Dim MLogic As New MessageLogic

        'フォーカスの設定
        SetFocusAjax(Me.TextSeikyuusyoHakkouDate)

        '金額変更イベント制御（登録）設定
        If Me.hiddenTxtChgCtrlTouroku.Value = enTxtChgCtrlTouroku.JituGaku.ToString Then
            Exit Sub
        Else
            Me.hiddenTxtChgCtrlTouroku.Value = enTxtChgCtrlTouroku.KoumuGaku.ToString
        End If

        If Me.SelectSyouhinTourokuRyou.SelectedIndex = 0 Then
            Exit Sub
        End If

        '金額取得
        If Me.TextJituseikyuuZeinukikingaku.Text <> "" Then
            intJituGaku = CInt(Me.TextJituseikyuuZeinukikingaku.Text)
        End If
        If Me.TextKoumutenZeinukiKingaku.Text <> "" Then
            intKoumuGaku = CInt(Me.TextKoumutenZeinukiKingaku.Text)
        End If

        '画面自動設定判断
        judgeTourokuAutoSetting(Me.SelectSyouhinTourokuRyou.SelectedValue)

        '商品明細の取得
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.SelectSyouhinTourokuRyou.SelectedValue, SOUKO_CD_TOUROKU)

        '自動設定パターン判断
        Select Case True
            Case recSyouhinMeisai Is Nothing
                'テーブルサイズ再設定
                TableResizeBeforAlert(Me)
                MLogic.AlertMessage(sender, Messages.MSG001E)
                Exit Sub

            Case eUmu = enSeikyuuUmu.Ari _
             And eSCd = enSyouhinCd.Zumi _
             And eSaki = enSeikyuuSaki.Tyoku
                intJituGaku = intKoumuGaku
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case eUmu = enSeikyuuUmu.Ari _
             And eSCd = enSyouhinCd.Zumi _
             And eSaki = enSeikyuuSaki.Hoka _
             And eKretu = enKeiretu.SanK
                blnFlg = clsJibanL.GetSeikyuuGaku(sender _
                                                , 5 _
                                                , Me.TextKeiretuCd.Text _
                                                , Me.SelectSyouhinTourokuRyou.SelectedValue _
                                                , intKoumuGaku _
                                                , intJituGaku)
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case Else
                Exit Sub

        End Select

        Me.TextJituseikyuuZeinukikingaku.Text = intJituGaku
        Me.TextSyouhizei.Text = intZei
        Me.TextZeikomiKingaku.Text = intZeiKomiGaku
        Me.TextKoumutenZeinukiKingaku.Text = intKoumuGaku

        '税区分の設定
        Me.hiddenZeiKbn.Value = recSyouhinMeisai.ZeiKbn

    End Sub

    ''' <summary>
    ''' 登録料－消費税額
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgSyouhiZei_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim intJituGaku As Integer = dtLogic.str2Int(Me.TextJituseikyuuZeinukikingaku.Text, System.Globalization.NumberStyles.AllowThousands)
        Dim intZeiGaku As Integer = dtLogic.str2Int(Me.TextSyouhizei.Text, System.Globalization.NumberStyles.AllowThousands)
        Dim blnActCtrlKoumu As Boolean

        '画面自動設定判断
        judgeTourokuAutoSetting(Me.SelectSyouhinTourokuRyou.SelectedValue)
        '工務店税抜き金額活性制御
        blnActCtrlKoumu = activeControlKoumuten()

        If blnActCtrlKoumu Then
            'フォーカスの設定
            SetFocusAjax(Me.TextKoumutenZeinukiKingaku)
        Else
            'フォーカスの設定
            SetFocusAjax(Me.TextSeikyuusyoHakkouDate)
        End If

        If Me.SelectSyouhinTourokuRyou.SelectedIndex = 0 Then
            Exit Sub
        End If

        If intJituGaku = 0 Then
            intZeiGaku = 0
        End If

        Me.TextJituseikyuuZeinukikingaku.Text = intJituGaku
        Me.TextSyouhizei.Text = intZeiGaku
        Me.TextZeikomiKingaku.Text = intJituGaku + intZeiGaku

    End Sub

    ''' <summary>
    ''' 登録料－売上年月日
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgUriDate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        '伝票売上年月日の自動設定
        If Me.TextUriageNengappi.Text <> String.Empty Then
            '売上年月日が入力済みの場合のみ
            If Me.TextDenpyouUriDate.Text = String.Empty Then
                '伝票売上年月日が空だったら、売上年月日をセット
                Me.TextDenpyouUriDate.Text = Me.TextUriageNengappi.Text
                '伝票売上年月日変更時処理を実行
                buttonChgDenUriDate_ServerClick(sender, e)
            End If

            '税区分・税率を再取得
            strSyouhinCd = Me.SelectSyouhinTourokuRyou.SelectedValue
            If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                '取得した税区分をセット
                Me.hiddenZeiKbn.Value = strZeiKbn
                '金額計算
                SetKingaku(strZeiritu,SOUKO_CD_TOUROKU)
            End If
        Else
            '売上年月日未入力の場合

            '商品コード未入力以外
            If Me.SelectSyouhinTourokuRyou.SelectedValue <> String.Empty Then
                '税区分・税率を再取得
                strSyouhinCd = Me.SelectSyouhinTourokuRyou.SelectedValue
                If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                    '取得した税区分をセット
                    Me.hiddenZeiKbn.Value = strZeiKbn
                    '金額計算
                    SetKingaku(strZeiritu, SOUKO_CD_TOUROKU)
                End If
            End If
        End If

        'フォーカスの設定
        SetFocusAjax(Me.TextDenpyouUriDate)
    End Sub

    ''' <summary>
    ''' 登録料－伝票売上年月日
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgDenUriDate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sendClassDispInfo(clsLogic)

        '請求書発行日の自動設定
        If Me.TextDenpyouUriDate.Text <> String.Empty Then
            Me.TextSeikyuusyoHakkouDate.Text = clsLogic.GetHansokuhinSeikyuusyoHakkoudate(Me.hiddenMiseCd.Value _
                                                                                        , Me.SelectSyouhinTourokuRyou.SelectedValue _
                                                                                        , Me.TextDenpyouUriDate.Text)

        Else
            Me.TextSeikyuusyoHakkouDate.Text = String.Empty
        End If

        'フォーカスの設定
        SetFocusAjax(Me.hansokuhinSyokiDispLink)

    End Sub

    ''' <summary>
    ''' 販促品初期ツール料－請求有無変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgToolSeikyuu_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgToolSeikyuu.ServerClick
        Dim recSyouhinMeisai As New SyouhinMeisaiRecord
        Dim intJituGaku As Integer
        Dim intZei As Integer
        Dim intZeiKomiGaku As Integer
        Dim MLogic As New MessageLogic
        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        'フォーカスの設定
        SetFocusAjax(Me.SelectSeikyuuTool)

        '金額取得
        If Me.TextJituseikyuuZeinukikingakuTool.Text <> "" Then
            intJituGaku = CInt(Me.TextJituseikyuuZeinukikingakuTool.Text)
        End If

        '画面自動設定判断
        judgeToolAutoSetting()

        If eUmu = enSeikyuuUmu.Nasi Then
            Me.SelectSyouhinToolRyou.SelectedValue = "K0099"
        End If

        If Me.SelectSyouhinToolRyou.SelectedIndex = 0 Then
            Exit Sub
        End If

        '商品明細の取得
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.SelectSyouhinToolRyou.SelectedValue, SOUKO_CD_TOOL)

        '売上年月日で判断して、正しい税率を取得する
        strSyouhinCd = Me.SelectSyouhinToolRyou.SelectedValue
        If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappiTool.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '取得した税区分・税率をセット
            recSyouhinMeisai.Zeiritu = strZeiritu
            recSyouhinMeisai.ZeiKbn = strZeiKbn
        End If

        '自動設定パターン判断
        Select Case True
            Case recSyouhinMeisai Is Nothing
                'テーブルサイズ再設定
                TableResizeBeforAlert(Me)
                MLogic.AlertMessage(sender, Messages.MSG001E)
                Exit Sub

            Case eUmu = enSeikyuuUmu.Ari _
             And eSCd = enSyouhinCd.Zumi
                intJituGaku = recSyouhinMeisai.HyoujunKkk
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case eUmu = enSeikyuuUmu.Nasi
                Me.SelectSyouhinToolRyou.SelectedValue = "K0099"
                '商品コードが変わるので商品明細の再取得
                recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.SelectSyouhinToolRyou.SelectedValue, SOUKO_CD_TOOL)
                intJituGaku = recSyouhinMeisai.HyoujunKkk
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case Else
                Exit Sub

        End Select

        Me.TextJituseikyuuZeinukikingakuTool.Text = intJituGaku
        Me.TextSyouhizeiTool.Text = intZei
        Me.TextZeikomiKingakuTool.Text = intZeiKomiGaku

        '税区分の設定
        Me.hiddenZeiKbnTool.Value = recSyouhinMeisai.ZeiKbn

    End Sub

    ''' <summary>
    ''' 販促品初期ツール料－商品CD変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgToolSyouhin_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgToolSyouhin.ServerClick
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        Dim intJituGaku As Integer
        Dim intZei As Integer
        Dim intZeiKomiGaku As Integer
        Dim MLogic As New MessageLogic
        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        'フォーカスの設定
        SetFocusAjax(Me.SelectSyouhinToolRyou)

        '金額取得
        If Me.TextJituseikyuuZeinukikingakuTool.Text <> "" Then
            intJituGaku = CInt(Me.TextJituseikyuuZeinukikingakuTool.Text)
        End If

        If Me.SelectSyouhinToolRyou.SelectedIndex = 0 Then
            Me.TextJituseikyuuZeinukikingakuTool.Text = ""
            Me.TextSyouhizeiTool.Text = ""
            Me.TextZeikomiKingakuTool.Text = ""
            Me.hiddenZeiKbn.Value = ""
            Exit Sub
        End If
        '画面自動設定判断
        judgeToolAutoSetting()

        '商品明細の取得
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.SelectSyouhinToolRyou.SelectedValue, SOUKO_CD_TOOL)

        '売上年月日で判断して、正しい税率を取得する
        strSyouhinCd = Me.SelectSyouhinToolRyou.SelectedValue
        If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappiTool.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '取得した税区分・税率をセット
            recSyouhinMeisai.Zeiritu = strZeiritu
            recSyouhinMeisai.ZeiKbn = strZeiKbn
        End If

        '自動設定パターン判断
        Select Case True
            Case recSyouhinMeisai Is Nothing
                'テーブルサイズ再設定
                TableResizeBeforAlert(Me)
                MLogic.AlertMessage(sender, Messages.MSG001E)
                Exit Sub

            Case eUmu = enSeikyuuUmu.Ari _
             And eSCd = enSyouhinCd.Zumi
                intJituGaku = recSyouhinMeisai.HyoujunKkk
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case Else
                '税区分の設定
                Me.hiddenZeiKbnTool.Value = recSyouhinMeisai.ZeiKbn
                Exit Sub

        End Select

        Me.TextJituseikyuuZeinukikingakuTool.Text = intJituGaku
        Me.TextSyouhizeiTool.Text = intZei
        Me.TextZeikomiKingakuTool.Text = intZeiKomiGaku

        '税区分の設定
        Me.hiddenZeiKbnTool.Value = recSyouhinMeisai.ZeiKbn

    End Sub

    ''' <summary>
    ''' 販促品初期ツール料－実請求金額変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgToolJituGaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        Dim intJituGaku As Integer
        Dim intZei As Integer
        Dim intZeiKomiGaku As Integer
        Dim MLogic As New MessageLogic
        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        'フォーカスの設定
        SetFocusAjax(Me.TextSyouhizeiTool)

        '金額取得
        If Me.TextJituseikyuuZeinukikingakuTool.Text <> "" Then
            intJituGaku = CInt(Me.TextJituseikyuuZeinukikingakuTool.Text)
        End If

        If Me.SelectSyouhinToolRyou.SelectedIndex = 0 Then
            Exit Sub
        End If

        '画面自動設定判断
        judgeToolAutoSetting()

        '商品明細の取得
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.SelectSyouhinToolRyou.SelectedValue, SOUKO_CD_TOOL)

        '売上年月日で判断して、正しい税率を取得する
        strSyouhinCd = Me.SelectSyouhinToolRyou.SelectedValue
        If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappiTool.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '取得した税区分・税率をセット
            recSyouhinMeisai.Zeiritu = strZeiritu
            recSyouhinMeisai.ZeiKbn = strZeiKbn
        End If

        '自動設定パターン判断
        Select Case True
            Case recSyouhinMeisai Is Nothing
                'テーブルサイズ再設定
                TableResizeBeforAlert(Me)
                MLogic.AlertMessage(sender, Messages.MSG001E)
                Exit Sub

            Case eUmu = enSeikyuuUmu.Ari _
             And eSCd = enSyouhinCd.Zumi
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case eUmu = enSeikyuuUmu.Nasi
                intZei = Integer.Parse(Fix(intJituGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intJituGaku + intZei

            Case Else
                Exit Sub

        End Select

        Me.TextJituseikyuuZeinukikingakuTool.Text = intJituGaku
        Me.TextSyouhizeiTool.Text = intZei
        Me.TextZeikomiKingakuTool.Text = intZeiKomiGaku

        '税区分の設定
        Me.hiddenZeiKbnTool.Value = recSyouhinMeisai.ZeiKbn

    End Sub

    ''' <summary>
    ''' 販促品初期ツール料－消費税額
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonChgSyouhizeiTool_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim intJituGaku As Integer = dtLogic.str2Int(Me.TextJituseikyuuZeinukikingakuTool.Text, System.Globalization.NumberStyles.AllowThousands)
        Dim intZeiGaku As Integer = dtLogic.str2Int(Me.TextSyouhizeiTool.Text, System.Globalization.NumberStyles.AllowThousands)

        If Me.SelectSyouhinToolRyou.SelectedIndex = 0 Then
            Exit Sub
        End If

        'フォーカスの設定
        SetFocusAjax(Me.TextSeikyuusyoHakkouDateTool)

        If intJituGaku = 0 Then
            intZeiGaku = 0
        End If

        Me.TextJituseikyuuZeinukikingakuTool.Text = intJituGaku
        Me.TextSyouhizeiTool.Text = intZeiGaku
        Me.TextZeikomiKingakuTool.Text = intJituGaku + intZeiGaku

    End Sub

    ''' <summary>
    ''' 販促品初期ツール料－売上年月日
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgUriDateTool_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        '伝票売上年月日の自動設定
        If Me.TextUriageNengappiTool.Text <> String.Empty Then
            If Me.TextDenpyouUriDateTool.Text = String.Empty Then
                Me.TextDenpyouUriDateTool.Text = Me.TextUriageNengappiTool.Text
                buttonChgDenUriDateTool_ServerClick(sender, e)
            End If

            '税区分・税率を再取得
            strSyouhinCd = Me.SelectSyouhinToolRyou.SelectedValue
            If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappiTool.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                '取得した税区分をセット
                Me.hiddenZeiKbnTool.Value = strZeiKbn
                '金額計算
                SetKingaku(strZeiritu, SOUKO_CD_TOOL)
            End If
        Else
            '売上年月日未入力の場合

            '商品コード未入力以外
            If Me.SelectSyouhinTourokuRyou.SelectedValue <> String.Empty Then
                '税区分・税率を再取得
                strSyouhinCd = Me.SelectSyouhinToolRyou.SelectedValue
                If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappiTool.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                    '取得した税区分をセット
                    Me.hiddenZeiKbnTool.Value = strZeiKbn
                    '金額計算
                    SetKingaku(strZeiritu, SOUKO_CD_TOOL)
                End If
            End If
        End If

        'フォーカス設定
        SetFocusAjax(Me.TextDenpyouUriDateTool)

    End Sub

    ''' <summary>
    ''' 販促品初期ツール料－伝票売上年月日
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgDenUriDateTool_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sendClassDispInfo(clsLogic)

        '請求書発行日の自動設定
        If Me.TextDenpyouUriDateTool.Text <> String.Empty Then
            Me.TextSeikyuusyoHakkouDateTool.Text = clsLogic.GetHansokuhinSeikyuusyoHakkoudate(Me.hiddenMiseCd.Value _
                                                                                        , Me.SelectSyouhinToolRyou.SelectedValue _
                                                                                        , Me.TextDenpyouUriDate.Text)
        Else
            Me.TextSeikyuusyoHakkouDateTool.Text = String.Empty
        End If

        'フォーカス設定
        SetFocusAjax(Me.buttonTourokuCall)

    End Sub

    ''' <summary>
    ''' 画面自動設定判断（登録料）
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <remarks></remarks>
    Private Sub judgeTourokuAutoSetting(ByVal strSyouhinCd As String)
        Dim jLogic As New JibanLogic
        Dim itemRec As Syouhin23Record

        '請求有無判断
        Select Case Me.SelectSeikyuu.Value
            Case 0
                eUmu = enSeikyuuUmu.Nasi
            Case 1
                eUmu = enSeikyuuUmu.Ari
            Case Else
                eUmu = -1
        End Select

        '商品コード判断
        Select Case Me.SelectSyouhinTourokuRyou.SelectedIndex
            Case 0
                eSCd = enSyouhinCd.Mi
            Case Is > 0
                eSCd = enSyouhinCd.Zumi
            Case Else
                eSCd = -1
        End Select

        '請求先判断
        itemRec = jLogic.GetSyouhinInfo(strSyouhinCd, EarthEnum.EnumSyouhinKubun.TourokuRyou, Me.TextKameitenCd.Text)
        If itemRec IsNot Nothing AndAlso itemRec.SeikyuuSakiType = EarthConst.SEIKYU_TYOKUSETU Then
            eSaki = enSeikyuuSaki.Tyoku
        Else
            eSaki = enSeikyuuSaki.Hoka
        End If

        '系列判断
        Select Case Me.TextKeiretuCd.Text
            Case KEIRETU_TH
                eKretu = enKeiretu.SanK
            Case KEIRETU_01
                eKretu = enKeiretu.SanK
            Case KEIRETU_NF
                eKretu = enKeiretu.SanK
            Case Else
                eKretu = enKeiretu.NotSanK
        End Select
    End Sub

    ''' <summary>
    ''' 画面自動設定判断（販促品初期ツール料）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub judgeToolAutoSetting()
        '請求有無判断
        Select Case Me.SelectSeikyuuTool.Value
            Case 0
                eUmu = enSeikyuuUmu.Nasi
            Case 1
                eUmu = enSeikyuuUmu.Ari
            Case Else
                eUmu = -1
        End Select
        '商品コード判断
        Select Case Me.SelectSyouhinToolRyou.SelectedIndex
            Case 0
                eSCd = enSyouhinCd.Mi
            Case Is > 0
                eSCd = enSyouhinCd.Zumi
            Case Else
                eSCd = -1
        End Select
    End Sub

    ''' <summary>
    ''' 工務店税抜き金額活性制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Function activeControlKoumuten() As Boolean
        Dim ht As New Hashtable

        If eSaki = enSeikyuuSaki.Hoka And eKretu = enKeiretu.NotSanK Then
            clsJSM.Hash2Ctrl(TdKoumutenZeinukiKingaku, EarthConst.MODE_VIEW, ht)
            Return False
        Else
            clsCL.chgDispSyouhinText(TextKoumutenZeinukiKingaku)
            Return True
        End If
    End Function
#End Region

#Region "ボタン押下処理"
    ''' <summary>
    ''' 新規行追加押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub addRow_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim recHansoku As New TenbetuHansokuHinRecord
        Dim ctrlHansokuRecord As HansokuRecordCtrl
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        Dim strSoukoCd As String
        Dim MLogic As New MessageLogic
        Dim intMaxNyuuryokuDateNo As Integer

        If Me.selectHansokuSyouhin.SelectedValue = "" Then
            'テーブルサイズ再設定
            TableResizeBeforAlert(Me)
            MLogic.AlertMessage(sender, Messages.MSG095E)
            SetFocusAjax(Me.selectHansokuSyouhin)
            Exit Sub
        End If

        '現在の画面情報を設定
        recHansoku.IsFc = enIsFc
        If enIsFc = IsFcMode.NOT_FC Then
            recHansoku.MiseCd = Me.TextKameitenCd.Text
            strSoukoCd = SOUKO_CD_NOT_FC
        Else
            recHansoku.MiseCd = Me.TextEigyousyoCd.Text
            strSoukoCd = SOUKO_CD_FC
        End If
        recHansoku.KeiretuCd = Me.TextKeiretuCd.Text
        recHansoku.HansokuhinSeikyuusaki = Me.hiddenHansokuHinSeikyuuSaki.Value
        recHansoku.TysSeikyuuSaki = Me.hiddenTyousaSeikyuuSaki.Value

        '当日の入力日Noの最大値を取得
        intMaxNyuuryokuDateNo = clsLogic.GetMaxNyuuryokuDateNo(Me.hiddenMiseCd.Value, strSoukoCd, DateTime.Today)
        '99件以上の登録を制御
        If intMaxNyuuryokuDateNo >= 99 Then
            MLogic.AlertMessage(sender, String.Format(Messages.MSG105E, "99"))
            Exit Sub
        End If

        '選択した商品の明細を取得
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.selectHansokuSyouhin.SelectedValue, strSoukoCd)
        '選択した商品の情報を設定
        If recSyouhinMeisai IsNot Nothing Then
            recHansoku.SyouhinCd = recSyouhinMeisai.SyouhinCd
            recHansoku.SyouhinMei = recSyouhinMeisai.SyouhinMei
            recHansoku.Suu = 1
            If enIsFc = IsFcMode.NOT_FC Then
                recHansoku.HassouDate = DateTime.Today
            Else
                recHansoku.HassouDate = DateTime.MinValue
            End If
        Else
            recHansoku.SyouhinCd = Me.selectHansokuSyouhin.SelectedValue
            'テーブルサイズ再設定
            TableResizeBeforAlert(Me)
            MLogic.AlertMessage(sender, Messages.MSG001E)
        End If

        With searchGrid.Controls
            ctrlHansokuRecord = Me.LoadControl("control/HansokuRecordCtrl.ascx")
            ctrlHansokuRecord.ID = HANSOKU_SHOUHIN_CTRL_NAME & (.Count).ToString()

            ctrlHansokuRecord.SetIdIndex(listHsRecCtrl.Count + 1, enMode, enIsFc)
            ctrlHansokuRecord.SetValue(recHansoku)

            ' グリッドに行コントロールをセット
            .Add(ctrlHansokuRecord)
        End With
        '商品コード変更時イベントの呼び出し
        ctrlHansokuRecord.NewButtonClick(sender, e, strSoukoCd, enIsFc)

        listHsRecCtrl.Add(ctrlHansokuRecord)

    End Sub

    ''' <summary>
    ''' 販促品の行を作成します
    ''' </summary>
    ''' <param name="intRowCnt">作成する行数</param>
    ''' <remarks></remarks>
    Private Sub createHansokuRow(ByVal intRowCnt As Integer)
        Dim ctrlHansokuRecord As HansokuRecordCtrl

        For intCnt As Integer = 0 To intRowCnt - 1
            ' ユーザーコントロールのロード
            ctrlHansokuRecord = Me.LoadControl("control/HansokuRecordCtrl.ascx")

            With searchGrid.Controls
                ' ユーザーコントロールにインデックス値を設定
                ctrlHansokuRecord.ID = HANSOKU_SHOUHIN_CTRL_NAME & (.Count).ToString()
                ctrlHansokuRecord.SetIdIndex(intCnt + 1, enMode, enIsFc)

                ' グリッドに行コントロールをセット
                .Add(ctrlHansokuRecord)
            End With

            listHsRecCtrl.Add(ctrlHansokuRecord)
        Next
    End Sub

    ''' <summary>
    ''' 販促品の行に値を設定します
    ''' </summary>
    ''' <param name="listHansoku">設定する値のレコードを格納しているリスト</param>
    ''' <remarks></remarks>
    Private Sub setValueHansokuRow(ByVal listHansoku As List(Of TenbetuHansokuHinRecord))
        Dim ctrlHansokuRecord As HansokuRecordCtrl

        For intCnt As Integer = 0 To listHansoku.Count - 1
            ctrlHansokuRecord = searchGrid.Controls.Item(intCnt + 1)
            'FC判断のセット
            listHansoku(intCnt).IsFc = enIsFc
            listHansoku(intCnt).enMode = enMode
            ctrlHansokuRecord.SetValue(listHansoku(intCnt))
            ctrlHansokuRecord.setOpenValues()
        Next

    End Sub

    ''' <summary>
    ''' 登録（登録料・販促初期ツール料）押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonTouroku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonTouroku.ServerClick
        Dim recDisplayInfo As TenbetuSyokiSeikyuuRecord
        Dim listDispInfo As New List(Of TenbetuSyokiSeikyuuRecord)
        Dim strMsg As String
        Dim MLogic As New MessageLogic
        Dim clsYosin As New YosinTenbetuRecord

        '入力チェック
        If Not CheckInputTouroku() Then
            Exit Sub
        End If

        '登録料の画面情報を店別請求レコードへ格納
        recDisplayInfo = New TenbetuSyokiSeikyuuRecord
        With recDisplayInfo
            .MiseCd = Me.TextKameitenCd.Text
            .BunruiCd = SOUKO_CD_TOUROKU
            .AddDate = DateTime.Today
            .SeikyuusyoHakDate = Str2DtTime(Me.TextSeikyuusyoHakkouDate.Text)
            .UriDate = Str2DtTime(Me.TextUriageNengappi.Text)
            .DenpyouUriDate = Str2DtTime(Me.TextDenpyouUriDate.Text)
            .SeikyuuUmu = Me.SelectSeikyuu.Value
            If Me.SpanUriageSyorizumi.Style("display") = "inline" Then
                .UriKeijyouFlg = 1
            Else
                .UriKeijyouFlg = 0
            End If
            .SyouhinCd = Me.SelectSyouhinTourokuRyou.SelectedValue
            .UriGaku = Str2Int(Me.TextJituseikyuuZeinukikingaku.Text, System.Globalization.NumberStyles.AllowThousands)
            .ZeiKbn = Me.hiddenZeiKbn.Value
            .SyouhiZeiGaku = Str2Int(Me.TextSyouhizei.Text, System.Globalization.NumberStyles.AllowThousands)
            .KoumutenSeikyuuGaku = Str2Int(Me.TextKoumutenZeinukiKingaku.Text, System.Globalization.NumberStyles.AllowThousands)
            .AddLoginUserId = user_info.LoginUserId
            .UpdLoginUserId = user_info.LoginUserId
            .AddDatetime = DateTime.Now
            .UpdDatetime = Str2DtTime(Me.hiddenTourokuUpdateTime.Value)
            '与信用
            .ZeikomiGaku = Str2Int(Me.TextZeikomiKingaku.Text, System.Globalization.NumberStyles.AllowThousands)
        End With
        '登録料の更新用レコードをリストに追加
        listDispInfo.Add(recDisplayInfo)

        '系列CDが"A"の場合、販促品初期ツール料の更新を行う
        If Me.hiddenKubun.Value = JHSFC Then
            '販促品初期ツール料を更新
            recDisplayInfo = New TenbetuSyokiSeikyuuRecord
            With recDisplayInfo
                .MiseCd = Me.TextKameitenCd.Text
                .BunruiCd = SOUKO_CD_TOOL
                .AddDate = DateTime.Today
                .SeikyuusyoHakDate = Str2DtTime(Me.TextSeikyuusyoHakkouDateTool.Text)
                .UriDate = Str2DtTime(Me.TextUriageNengappiTool.Text)
                .DenpyouUriDate = Str2DtTime(Me.TextDenpyouUriDateTool.Text)
                .SeikyuuUmu = Me.SelectSeikyuuTool.Value
                If Me.SpanUriageSyorizumi.Style("display") = "inline" Then
                    .UriKeijyouFlg = 1
                Else
                    .UriKeijyouFlg = 0
                End If
                .SyouhinCd = Me.SelectSyouhinToolRyou.SelectedValue

                .UriGaku = Str2Int(Me.TextJituseikyuuZeinukikingakuTool.Text, System.Globalization.NumberStyles.AllowThousands)
                .ZeiKbn = Me.hiddenZeiKbnTool.Value
                .SyouhiZeiGaku = Str2Int(Me.TextSyouhizeiTool.Text, System.Globalization.NumberStyles.AllowThousands)
                .KoumutenSeikyuuGaku = 0
                .AddLoginUserId = user_info.LoginUserId
                .UpdLoginUserId = user_info.LoginUserId
                .AddDatetime = DateTime.Now
                .UpdDatetime = Str2DtTime(Me.hiddenToolUpdateTime.Value)
                '与信用
                .ZeikomiGaku = Str2Int(Me.TextZeikomiKingakuTool.Text, System.Globalization.NumberStyles.AllowThousands)

                '販促品初期ツール料の更新用レコードをリストに追加
                listDispInfo.Add(recDisplayInfo)
            End With
        End If

        '与信チェック
        With clsYosin
            clsYosin.KameitenCd = Me.hiddenMiseCd.Value
            clsYosin.TourokuTesuuryouUriGaku = Str2Int(Me.hiddenTourokuUriGaku.Value)
            clsYosin.SyokiToolRyouUriGaku = Str2Int(Me.hiddenToolUriGaku.Value)
            clsYosin.HansokuGoukei = Str2Long(Me.hiddenHsZeikomiGoukeiBefor.Value)
            clsYosin.HansokuGoukeiKoumuten = Str2Long(Me.hiddenKoumuTenSumBefor.Value)
        End With

        '与信チェック用に画面の情報をセット
        clsLogic.LoginUserId = user_info.LoginUserId

        '画面共通情報をロジッククラスにセット
        sendClassDispInfo(clsLogic)

        '店別初期請求テーブルを更新
        strMsg = clsLogic.UpdateTenbetuSyokiSeikyuu(listDispInfo, clsYosin)
        If strMsg.Length > 0 Then
            'テーブルサイズ再設定
            TableResizeBeforAlert(Me)
            MLogic.AlertMessage(sender, strMsg)
            MLogic.AlertMessage(sender, Messages.MSG019E.Replace(PARAM1, "登録/修正"), , "ButtonTouroku_ServerClick")
            Exit Sub
        End If

        '画面再描画
        setDisplay(sender)

        '店指定ポップアップ表示のためにフラグをセット
        callModalFlg.Value = Boolean.TrueString

    End Sub

    ''' <summary>
    ''' 登録（販促品）押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonTourokuHansoku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonTourokuHansoku.ServerClick
        Dim recDisplayInfo As TenbetuSeikyuuRecord
        Dim listDisplayInfo As New List(Of TenbetuSeikyuuRecord)
        Dim strString As String = ""
        Dim MLogic As New MessageLogic
        Dim clsYosin As New YosinTenbetuRecord

        'ユーザーコントロールの値を取得
        For Each ctrl As HansokuRecordCtrl In listHsRecCtrl
            recDisplayInfo = New TenbetuSeikyuuRecord
            '入力チェック
            If Not CheckInputHansoku(ctrl) Then
                Exit Sub
            End If
            With recDisplayInfo
                If ctrl.AccTextSyouhinCd.Value <> ctrl.AccHiddenSyouhinCdOld.Value Then
                    'テーブルサイズ再設定
                    TableResizeBeforAlert(Me)
                    MLogic.AlertMessage(sender, Messages.MSG030E.Replace(PARAM1, "商品コード" & ctrl.AccTextSyouhinCd.ID.Replace("TextSyouhinCd_", "")))
                    Exit Sub
                End If
                .MiseCd = ctrl.AccHiddenMiseCd.Value
                .BunruiCd = ctrl.AccHiddenSoukoCd.Value
                .NyuuryokuDate = Str2DtTime(ctrl.AccHiddenNyuuryokuDate.Value)
                .NyuuryokuDateNo = Str2Int(ctrl.AccHiddenNyuuryokuDateNo.Value, System.Globalization.NumberStyles.AllowThousands)
                .HassouDate = Str2DtTime(ctrl.AccTextHassouDate.Value)
                .SeikyuusyoHakDate = Str2DtTime(ctrl.AccTextSeikyuusyoHakkouBi.Value)
                .UriDate = Str2DtTime(ctrl.AccTextUriageNengappi.Value)

                '●伝票売上年月日の補正
                If enMode = DisplayMode.HANSOKU Then
                    '販促品画面
                    If Not String.IsNullOrEmpty(ctrl.AccHiddenUriKeijyouFlg.Value) AndAlso ctrl.AccHiddenUriKeijyouFlg.Value = 0 Then
                        '未計上の場合 売上年月日と同期
                        .DenpyouUriDate = Str2DtTime(ctrl.AccTextUriageNengappi.Value)
                    Else
                        '計上済みの場合 伝票売上年月日と同様(DB値)
                        .DenpyouUriDate = Str2DtTime(ctrl.AccTextDenpyouUriDateDisplay.Value)
                    End If
                Else
                    '店別データ修正画面
                    .DenpyouUriDate = Str2DtTime(ctrl.AccTextDenpyouUriDate.Value)
                End If

                If ctrl.AccTextUriageNengappi.Value.Length = 0 Then
                    .UriKeijyouFlg = 0
                    .UriKeijyouDate = DateTime.MinValue
                End If
                .SyouhinCd = ctrl.AccTextSyouhinCd.Value
                .Tanka = Str2Int(ctrl.AccTextJituSeikyuuTanka.Value, System.Globalization.NumberStyles.AllowThousands)
                .Suu = Str2Int(ctrl.AccTextSuuRyou.Value, System.Globalization.NumberStyles.AllowThousands)
                .ZeiKbn = Str2Int(ctrl.AccHiddenZeiKbn.Value, System.Globalization.NumberStyles.AllowThousands)
                .KoumutenSeikyuuTanka = Str2Int(ctrl.AccTextKoumutenSeikyuuTanka.Value, System.Globalization.NumberStyles.AllowThousands)
                .SyouhiZeiGaku = Str2Int(ctrl.AccTextSyouhizeiGaku.Value, System.Globalization.NumberStyles.AllowThousands)
                .AddLoginUserId = user_info.LoginUserId
                .UpdLoginUserId = user_info.LoginUserId
                .AddDatetime = DateTime.Now
                .UpdDatetime = Str2DtTime(ctrl.AccHiddenUpdDateTime.Value)
                .SqlTypeFlg = ctrl.AccSqlTypeFlg.Value
            End With
            '取得した画面情報をリストに格納
            listDisplayInfo.Add(recDisplayInfo)
        Next

        '与信チェック用に画面の情報をセット
        clsLogic.SeikyuuGakuSum = Str2Long(Me.TextHsZeikomiKingaku.Text, System.Globalization.NumberStyles.AllowThousands)
        clsLogic.KoumuGakuSum = Str2Long(Me.hiddenKoumuTenSumAfter.Value, System.Globalization.NumberStyles.AllowThousands)
        clsLogic.LoginUserId = user_info.LoginUserId

        '与信チェック
        With clsYosin
            clsYosin.KameitenCd = Me.hiddenMiseCd.Value
            clsYosin.TourokuTesuuryouUriGaku = Str2Int(Me.hiddenTourokuUriGaku.Value)
            clsYosin.SyokiToolRyouUriGaku = Str2Int(Me.hiddenToolUriGaku.Value)
            clsYosin.HansokuGoukei = Str2Long(Me.hiddenHsZeikomiGoukeiBefor.Value)
            clsYosin.HansokuGoukeiKoumuten = Str2Long(Me.hiddenKoumuTenSumBefor.Value)
        End With

        '画面共通情報をロジッククラスにセット
        sendClassDispInfo(clsLogic)

        '販促品一覧を更新
        strString = clsLogic.UpdateTenbetuSeikyuu(listDisplayInfo, clsYosin)
        If strString.Length > 0 Then
            'テーブルサイズ再設定
            TableResizeBeforAlert(Me)
            MLogic.AlertMessage(sender, strString)
            MLogic.AlertMessage(sender, Messages.MSG019E.Replace(PARAM1, "登録/修正"), , "ButtonTouroku_ServerClick")
            Exit Sub
        End If

        '画面再描画
        setDisplay(sender)

        '店指定ポップアップ表示のためにフラグをセット
        callModalFlg.Value = Boolean.TrueString
    End Sub
#End Region

#Region "関数"
    ''' <summary>
    ''' アラート表示時にテーブルサイズを再設定します
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub TableResizeBeforAlert(ByVal sender)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "TableResizeBeforAlert" _
                                            , sender)
        ScriptManager.RegisterStartupScript(sender, _
                                            sender.GetType(), _
                                            "TableReSize", "_d = window.document;changeTableSize(""dataGridContent"",200,100); ", True)
    End Sub

    ''' <summary>
    ''' 文字列を日付型に変換します
    ''' </summary>
    ''' <param name="strValue">変換したい文字列</param>
    ''' <returns>変換後の日付</returns>
    ''' <remarks></remarks>
    Private Function Str2DtTime(ByVal strValue As String) As DateTime
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "Str2DtTime" _
                                            , strValue)
        Return dtLogic.str2DtTime(strValue)
    End Function

    ''' <summary>
    ''' 日付型を文字列に変換します
    ''' </summary>
    ''' <param name="dtValue">変換したい日付</param>
    ''' <param name="blnMillisecond">ミリ秒表示判断フラグ</param>
    ''' <returns>変換後の文字列</returns>
    ''' <remarks></remarks>
    Private Function DtTime2Str(ByVal dtValue As Date, Optional ByVal blnMillisecond As Boolean = False) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "Str2DtTime" _
                                            , dtValue _
                                            , blnMillisecond)
        Dim strRet As String

        If blnMillisecond = True Then
            strRet = dtLogic.dtTime2Str(dtValue, EarthConst.FORMAT_DATE_TIME_2)
        Else
            strRet = dtLogic.dtTime2Str(dtValue)
        End If

        Return strRet
    End Function

    ''' <summary>
    ''' 文字列を数値型に変換します(NumberStyles 列挙体を使用)
    ''' </summary>
    ''' <param name="strValue">変換したい文字列</param>
    ''' <returns>変換後の数値</returns>
    ''' <param name="NumStyle">NumberStyles 列挙体</param>
    ''' <remarks></remarks>
    Private Function Str2Int(ByVal strValue As String, ByVal NumStyle As System.Globalization.NumberStyles) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "Str2Int" _
                                            , strValue _
                                            , NumStyle)
        Return dtLogic.str2Int(strValue, NumStyle)
    End Function

    ''' <summary>
    ''' 文字列を数値型に変換します
    ''' </summary>
    ''' <param name="strValue">変換したい文字列</param>
    ''' <returns>変換後の数値</returns>
    ''' <remarks></remarks>
    Private Function Str2Int(ByVal strValue As String) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "Str2Int" _
                                            , strValue)
        Return dtLogic.str2Int(strValue)
    End Function

    ''' <summary>
    ''' 文字列を数値型に変換します(NumberStyles 列挙体を使用)
    ''' </summary>
    ''' <param name="strValue">変換したい文字列</param>
    ''' <returns>変換後の数値</returns>
    ''' <param name="NumStyle">NumberStyles 列挙体</param>
    ''' <remarks></remarks>
    Private Function Str2Long(ByVal strValue As String, ByVal NumStyle As System.Globalization.NumberStyles) As Long
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "Str2Long" _
                                                    , strValue _
                                                    , NumStyle)
        Return dtLogic.str2Long(strValue, NumStyle)
    End Function

    ''' <summary>
    ''' 文字列を数値型に変換します
    ''' </summary>
    ''' <param name="strValue">変換したい文字列</param>
    ''' <returns>変換後の数値</returns>
    ''' <remarks></remarks>
    Private Function Str2Long(ByVal strValue As String) As Long
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "Str2Long" _
                                                    , strValue)
        Return dtLogic.str2Long(strValue)
    End Function

    ''' <summary>
    ''' 数値を文字列に変換します
    ''' </summary>
    ''' <param name="intValue">変換したい数値</param>
    ''' <returns>変換後の文字列</returns>
    ''' <param name="blnThousandsFormat"></param>
    ''' <remarks></remarks>
    Private Function Int2Str(ByVal intValue As String, Optional ByVal blnThousandsFormat As Boolean = True) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "Int2Str" _
                                                    , intValue _
                                                    , blnThousandsFormat)
        Dim strRet As String

        If blnThousandsFormat Then
            strRet = dtLogic.int2Str(intValue, EarthConst.FORMAT_KINGAKU_1)
        Else
            strRet = dtLogic.int2Str(intValue)
        End If

        Return strRet
    End Function

    ''' <summary>
    ''' 数字を3桁区切りの文字列に変更します
    ''' </summary>
    ''' <param name="strNumber">対象数値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function StrNum2Str(ByVal strNumber As String) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "StrNum2Str" _
                                                , strNumber)

        Return dtLogic.strNum2Str(strNumber)
    End Function

    ''' <summary>
    ''' 表示切替リンク設定<br/>
    ''' JavaScript:ChangeDisplay設定
    ''' </summary>
    ''' <param name="anc">アンカーコントロールのID</param>
    ''' <param name="ctrl">表示制御対象のコントロールID</param>
    ''' <remarks></remarks>
    Private Sub ChangeDisplay(ByVal anc As HtmlAnchor, _
                              ByVal ctrl As HtmlGenericControl)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "ChangeDisplay" _
                                                    , anc _
                                                    , ctrl)
        ' スクリプトを設定する
        anc.Attributes("onclick") = String.Format("changeTableSize('dataGridContent',200,100);" & EarthConst.SCRIPT_JS_CHANGE_DISPLAY, ctrl.ClientID)
    End Sub

    ''' <summary>
    ''' 画面共通情報をロジッククラスへ引き渡す
    ''' </summary>
    ''' <param name="clsLogic">店別修正ロジッククラス</param>
    ''' <remarks></remarks>
    Public Sub sendClassDispInfo(ByRef clsLogic As TenbetuSyuuseiLogic)

        '画面共通情報の設定
        With clsLogic
            .MiseCd = Me.hiddenMiseCd.Value
            .Kbn = Me.hiddenKubun.Value
            .IsFC = enIsFc
            .DispMode = enMode
            .LoginUserId = user_info.LoginUserId
        End With
    End Sub

    ''' <summary>
    ''' Ajax動作時のフォーカスセット
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub SetFocusAjax(ByVal ctrl As Control)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetFocusAjax", _
                                                    ctrl)
        masterAjaxSM.SetFocus(ctrl)
    End Sub

    ''' <summary>
    ''' 画面読み込み時の値をHidden項目に退避
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setOpenValues()
        Me.HiddenOpenTourokuRyouValues.Value = getTourokuCtrlValuesString()
        Me.HiddenOpenToolRyouValues.Value = getToolCtrlValuesString()
    End Sub

    ''' <summary>
    ''' 登録料の画面コントロールの値を結合し、文字列化する"
    ''' </summary>
    ''' <returns>登録料の画面コントロールの値を結合した文字列</returns>
    ''' <remarks></remarks>
    Private Function getTourokuCtrlValuesString() As String
        Dim sb As New StringBuilder

        sb.Append(Me.SelectSyouhinTourokuRyou.SelectedValue & EarthConst.SEP_STRING)
        sb.Append(Me.TextJituseikyuuZeinukikingaku.Text.Replace(",", "") & EarthConst.SEP_STRING)
        sb.Append(Me.TextSyouhizei.Text.Replace(",", "") & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuusyoHakkouDate.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextUriageNengappi.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextDenpyouUriDate.Text & EarthConst.SEP_STRING)

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 販促品初期ツール料の画面コントロールの値を結合し、文字列化する"
    ''' </summary>
    ''' <returns>販促品初期ツール料の画面コントロールの値を結合した文字列</returns>
    ''' <remarks></remarks>
    Private Function getToolCtrlValuesString() As String
        Dim sb As New StringBuilder

        sb.Append(Me.SelectSyouhinToolRyou.SelectedValue & EarthConst.SEP_STRING)
        sb.Append(Me.TextJituseikyuuZeinukikingakuTool.Text.Replace(",", "") & EarthConst.SEP_STRING)
        sb.Append(Me.TextSyouhizeiTool.Text.Replace(",", "") & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuusyoHakkouDateTool.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextUriageNengappiTool.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextDenpyouUriDateTool.Text & EarthConst.SEP_STRING)

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 金額設定
    ''' </summary>
    ''' <param name="strZeiritu">税率</param>
    ''' <param name="strType">タイプ(登録料/販促品初期ツール料)</param>
    ''' <remarks></remarks>
    Private Sub SetKingaku(ByVal strZeiritu As String, ByVal strType As String)
        Dim intJituGaku As Integer      '実請求税抜金額
        Dim intZei As Integer           '消費税
        Dim intZeiKomiGaku As Integer   '税込金額

        '画面に計算した値をセットする
        Select Case strType
            '登録料
            Case SOUKO_CD_TOUROKU
                '実請求税抜金額、税率が空白の場合、税込金額、消費税を空白にする
                If Me.TextJituseikyuuZeinukikingaku.Text = String.Empty _
                    OrElse strZeiritu = String.Empty Then
                    Me.TextZeikomiKingaku.Text = String.Empty
                    Me.TextSyouhizei.Text = String.Empty
                Else
                    '上記以外の場合
                    intJituGaku = Me.TextJituseikyuuZeinukikingaku.Text     '実請求税抜金額
                    intZei = Integer.Parse(Fix(intJituGaku * strZeiritu))   '消費税を計算(実請求税抜金額 * 取得した税率)
                    intZeiKomiGaku = intJituGaku + intZei                   '税込金額を計算(実請求税抜金額 + 消費税)

                    Me.TextJituseikyuuZeinukikingaku.Text = intJituGaku     '実請求税抜金額
                    Me.TextSyouhizei.Text = intZei                          '消費税
                    Me.TextZeikomiKingaku.Text = intZeiKomiGaku             '税込金額
                End If

                '販促品初期ツール料
            Case SOUKO_CD_TOOL
                '実請求税抜金額、税率が空白の場合、税込金額、消費税を空白にする
                If Me.TextJituseikyuuZeinukikingakuTool.Text = String.Empty _
                    OrElse strZeiritu = String.Empty Then
                    Me.TextZeikomiKingakuTool.Text = String.Empty
                    Me.TextSyouhizeiTool.Text = String.Empty
                Else
                    '上記以外の場合
                    intJituGaku = Me.TextJituseikyuuZeinukikingakuTool.Text     '実請求税抜金額
                    intZei = Integer.Parse(Fix(intJituGaku * strZeiritu))       '消費税を計算(実請求税抜金額 * 取得した税率)
                    intZeiKomiGaku = intJituGaku + intZei                       '税込金額を計算(実請求税抜金額 + 消費税)

                    Me.TextJituseikyuuZeinukikingakuTool.Text = intJituGaku     '実請求税抜金額
                    Me.TextSyouhizeiTool.Text = intZei                          '消費税
                    Me.TextZeikomiKingakuTool.Text = intZeiKomiGaku             '税込金額
                End If
        End Select

    End Sub

#End Region

End Class