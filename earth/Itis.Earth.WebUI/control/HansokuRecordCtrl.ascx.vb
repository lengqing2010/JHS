
Partial Public Class HansokuRecordCtrl
    Inherits System.Web.UI.UserControl
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "自動設定用フラグ列挙体"
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
    ''' 金額変更イベント制御（販促品）
    ''' </summary>
    ''' <remarks></remarks>
    Enum enTxtChgCtrlHansoku
        JituGaku = 1
        KoumuGaku = 2
        Reset = -1
    End Enum

    'コントロール表示モード
    Public Enum pDisplayMode
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
#End Region
#Region "工務店税抜き金額活性制御用"
    Private clsCL As New CommonLogic
    Private clsJSM As New JibanSessionManager
#End Region
#Region "加盟店モード"
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
#End Region
    ''' <summary>
    ''' ビジネスロジック共通クラス
    ''' </summary>
    ''' <remarks></remarks>
    Private cbLogic As New CommonBizLogic

    ''' <summary>
    ''' マスターページのAjaxScriptManager
    ''' </summary>
    ''' <remarks></remarks>
    Private masterAjaxSM As New ScriptManager

    ''' <summary>
    ''' コントロールの表示モード（店別モード）
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
    ''' コントロールの表示モード（店別モード）
    ''' </summary>
    ''' <remarks></remarks>
    Private enMode As DisplayMode

    Dim user_info As New LoginUserInfo

    Private clsLogic As New TenbetuSyuuseiLogic

#Region "ユーザーコントロールへの外部からのアクセス用Getter/Setter"
    ''' <summary>
    ''' 外部からのアクセス用 for hiddenMiseCd（加盟店コード）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenMiseCd() As HiddenField
        Get
            Return Me.hiddenMiseCd
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenMiseCd = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenSoukoCd（倉庫コード）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenSoukoCd() As HiddenField
        Get
            Return Me.hiddenSoukoCd
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenSoukoCd = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenNyuuryokuDate（入力日）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenNyuuryokuDate() As HiddenField
        Get
            Return Me.hiddenNyuuryokuDate
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenNyuuryokuDate = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenNyuuryokuDateNo（入力日NO）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Property AccHiddenNyuuryokuDateNo() As HiddenField
        Get
            Return Me.hiddenNyuuryokuDateNo
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenNyuuryokuDateNo = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextHassouDate（発送日）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextHassouDate() As HtmlInputText
        Get
            Return Me.TextHassouDate
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextHassouDate = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextSeikyuusyoHakkouBi（請求書発行日）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSeikyuusyoHakkouBi() As HtmlInputText
        Get
            Return Me.TextSeikyuusyoHakkouBi
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextSeikyuusyoHakkouBi = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextUriageNengappi（売上年月日）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextUriageNengappi() As HtmlInputText
        Get
            Return Me.TextUriageNengappi
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextUriageNengappi = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextDenpyouUriDateDisplay（伝票売上年月日ラベル）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextDenpyouUriDateDisplay() As HtmlInputText
        Get
            Return TextDenpyouUriDateDisplay
        End Get
        Set(ByVal value As HtmlInputText)
            TextDenpyouUriDateDisplay = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextDenpyouUriDate (伝票売上年月日)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextDenpyouUriDate() As HtmlInputText
        Get
            Return TextDenpyouUriDate
        End Get
        Set(ByVal value As HtmlInputText)
            TextDenpyouUriDate = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextSyouhinCd（商品コード）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSyouhinCd() As HtmlInputText
        Get
            Return Me.TextSyouhinCd
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextSyouhinCd = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenSyouhinCdOld（商品コード退避用）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenSyouhinCdOld() As HiddenField
        Get
            Return Me.hiddenOldSyouhinCd
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenOldSyouhinCd = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextJituSeikyuuTanka（実請求単価）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextJituSeikyuuTanka() As HtmlInputText
        Get
            Return Me.TextJituSeikyuuTanka
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextJituSeikyuuTanka = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextSuuryou（数量）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSuuRyou() As HtmlInputText
        Get
            Return Me.TextSuuryou
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextSuuryou = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextSyouhizeiGaku（消費税）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSyouhizeiGaku() As HtmlInputText
        Get
            Return Me.TextSyouhizeiGaku
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextSyouhizeiGaku = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextZeikomiKingaku（税込金額）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextZeikomiKingaku() As HtmlInputText
        Get
            Return Me.TextZeikomiKingaku
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextZeikomiKingaku = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenZeiKbn（税区分）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenZeiKbn() As HiddenField
        Get
            Return Me.hiddenZeiKbn
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenZeiKbn = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for TextKoumutenSeikyuuTanka（工務店請求単価）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextKoumutenSeikyuuTanka() As HtmlInputText
        Get
            Return Me.TextKoumutenSeikyuuTanka
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextKoumutenSeikyuuTanka = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenZeiritu（税率）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenZeiritu() As HiddenField
        Get
            Return Me.hiddenZeiritu
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenZeiritu = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenUpdDateTime（更新日時）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenUpdDateTime() As HiddenField
        Get
            Return Me.hiddenUpdateTime
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenUpdateTime = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for hiddenIsFc（店別モード）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenIsFc() As HiddenField
        Get
            Return Me.hiddenIsFc
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenIsFc = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for HiddenOpenValues(画面読込み時の値)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenOpenValues() As HiddenField
        Get
            Return Me.HiddenOpenValues
        End Get
        Set(ByVal value As HiddenField)
            Me.HiddenOpenValues = value
        End Set
    End Property
    ''' <summary>
    ''' 外部からのアクセス用 for HiddenOpenValues(売上計上FLG)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenUriKeijyouFlg() As HiddenField
        Get
            Return Me.HiddenUriKeijyouFlg
        End Get
        Set(ByVal value As HiddenField)
            Me.HiddenUriKeijyouFlg = value
        End Set
    End Property

#Region "SQL種別判断フラグ"
    Public Property AccSqlTypeFlg() As HiddenField
        Get
            Return Me.hiddenSqlTypeFlg
        End Get
        Set(ByVal value As HiddenField)
            Me.hiddenSqlTypeFlg = value
        End Set
    End Property
    ''' <summary>
    ''' SQL種別判断フラグ列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Enum enSqlTypeFlg
        ''' <summary>
        ''' 更新
        ''' </summary>
        ''' <remarks></remarks>
        UPDATE = EarthConst.enSqlTypeFlg.UPDATE
        ''' <summary>
        ''' 登録
        ''' </summary>
        ''' <remarks></remarks>
        INSERT = EarthConst.enSqlTypeFlg.INSERT
        ''' <summary>
        ''' 削除
        ''' </summary>
        ''' <remarks></remarks>
        DELETE = EarthConst.enSqlTypeFlg.DELETE
    End Enum
#End Region
#End Region

#Region "ページ処理"
    ''' <summary>
    ''' ページの初期処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Me.TextHassouDate.Attributes.Add("onfocus", "setTempValueForOnBlur(this);")
        Me.TextSyouhinCd.Attributes.Add("onfocus", "setTempValueForOnBlur(this);")
        Me.TextKoumutenSeikyuuTanka.Attributes.Add("onfocus", "removeFig(this);setTempValueForOnBlur(this);")
        Me.TextJituSeikyuuTanka.Attributes.Add("onfocus", "removeFig(this);setTempValueForOnBlur(this);")
        Me.TextSuuryou.Attributes.Add("onfocus", "removeFig(this);setTempValueForOnBlur(this);")
        Me.TextSyouhizeiGaku.Attributes.Add("onfocus", "removeFig(this);setTempValueForOnBlur(this);")
        Me.TextUriageNengappi.Attributes.Add("onfocus", "setTempValueForOnBlur(this);")
        Me.TextDenpyouUriDate.Attributes.Add("onfocus", "setTempValueForOnBlur(this);")

        Me.TextHassouDate.Attributes.Add("onblur", "if(checkDate(this)){if(checkTempValueForOnBlur(this)){objEBI('" & Me.buttonChgHassouDate.ClientID & "').click();}}")
        Me.TextSyouhinCd.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){objEBI('" & Me.buttonChgSyouhinCd.ClientID & "').click();}")
        Me.TextKoumutenSeikyuuTanka.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkKingaku(this,false)){objEBI('" & Me.buttonChgKoumu.ClientID & "').click();}}else{if(checkKingaku(this,false));}")
        Me.TextJituSeikyuuTanka.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkKingaku(this,false)){objEBI('" & Me.buttonChgJitu.ClientID & "').click();}}else{if(checkKingaku(this,false));}")
        Me.TextSuuryou.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkKingaku(this,false)){objEBI('" & Me.buttonChgSuuryou.ClientID & "').click();}}else{if(checkKingaku(this,false));}")
        Me.TextSyouhizeiGaku.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkKingaku(this,false)){objEBI('" & Me.buttonChgSyouhiZei.ClientID & "').click();}}else{if(checkKingaku(this,false));}")
        Me.TextUriageNengappi.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkDate(this)){objEBI('" & Me.buttonChgUriDate.ClientID & "').click();}}else{if(checkDate(this));}")
        Me.TextDenpyouUriDate.Attributes.Add("onblur", "if(checkTempValueForOnBlur(this)){if(checkDate(this)){objEBI('" & Me.buttonChgDenUriDate.ClientID & "').click();}}else{if(checkDate(this));}")

        Me.buttonGyouSakujoCall.Attributes.Add("onclick", "deleteConfirm(" & Me.buttonGyouSakujo.ClientID & "," & Me.TextSyouhinCd.ClientID & ");")

        '金額項目のMaxLengthを設定
        Me.TextZeinukiKingaku.MaxLength = 7
        Me.TextZeikomiKingaku.MaxLength = 7
        Me.TextSyouhizeiGaku.MaxLength = 7
        Me.TextKoumutenSeikyuuTanka.MaxLength = 7
        Me.TextJituSeikyuuTanka.MaxLength = 7
        '数量のMaxLengthを設定
        Me.TextSuuryou.MaxLength = 3
        '日付項目のMaxLengthを設定
        Me.TextUriageNengappi.MaxLength = 10
        Me.TextSeikyuusyoHakkouBi.MaxLength = 10
        Me.TextHassouDate.MaxLength = 10
        Me.TextDenpyouUriDate.MaxLength = 10
        '商品コードのMaxLengthを設定
        Me.TextSyouhinCd.MaxLength = 8

        'SQL種別判断フラグを更新にセット
        Me.hiddenSqlTypeFlg.Value = enSqlTypeFlg.UPDATE
    End Sub

    ''' <summary>
    ''' ページロード時のイベント
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

        Dim intKeiriKengen As Integer = user_info.KeiriGyoumuKengen
        If intKeiriKengen <> "-1" Then
            Me.TextSyouhizeiGaku.Attributes.Add("class", "kingaku readOnlyStyle")
            Me.TextSyouhizeiGaku.Attributes.Add("readonly", "true")
        End If

        If Not IsPostBack Then
            '画面読込み時の値をHidden項目に退避
            setOpenValues()
        End If

    End Sub

    ''' <summary>
    ''' ページレンダー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Me.TextKoumutenSeikyuuTanka.Value = StrNum2Str(Me.TextKoumutenSeikyuuTanka.Value)
        Me.TextJituSeikyuuTanka.Value = StrNum2Str(Me.TextJituSeikyuuTanka.Value)
        Me.TextSuuryou.Value = StrNum2Str(Me.TextSuuryou.Value)
        Me.TextZeinukiKingaku.Value = StrNum2Str(Me.TextZeinukiKingaku.Value)
        Me.TextSyouhizeiGaku.Value = StrNum2Str(Me.TextSyouhizeiGaku.Value)
        Me.TextZeikomiKingaku.Value = StrNum2Str(Me.TextZeikomiKingaku.Value)
    End Sub
#End Region

#Region "画面表示制御"
    ''' <summary>
    ''' コントロールにインデックス値を割り当てる
    ''' </summary>
    ''' <param name="index"></param>
    ''' <remarks></remarks>
    Public Sub SetIdIndex(ByVal index As Integer, _
                          ByVal tenbetuMode As Integer, _
                          ByVal isFC As Integer)
        ' 行(trタグ)
        TrHansokuRecord.ID = TrHansokuRecord.ID & "_" & index.ToString()
        ' 各項目

        If tenbetuMode <> DisplayMode.TENBETU Then
            TdHassouDate.ID = TdHassouDate.ID & "_" & index.ToString()
            TextHassouDate.ID = TextHassouDate.ID & "_" & index.ToString()
        Else
            TdHassouDate.Visible = False
            TextHassouDate.Visible = False
            ' 税抜金額は販促品請求のみ
            TdZeinukiKingaku.Visible = False
        End If
        enMode = tenbetuMode

        '店別請求モードによってコントロール表示を切り替える
        If enMode = DisplayMode.HANSOKU Or enMode = DisplayMode.SANSYOU Then
            Me.tdDenUriDate.Visible = False
        End If

        ' FCの場合
        If isFC = 1 Then
            TdKoumutenSeikyuu.Visible = False
        End If

        TextSyouhinCd.ID = TextSyouhinCd.ID & "_" & index.ToString()
        buttonKensaku.ID = buttonKensaku.ID & "_" & index.ToString()
        TextSyouhinMei.ID = TextSyouhinMei.ID & "_" & index.ToString() & "_" & index.ToString()
        TextKoumutenSeikyuuTanka.ID = TextKoumutenSeikyuuTanka.ID & "_" & index.ToString()
        TextSuuryou.ID = TextSuuryou.ID & "_" & index.ToString()
        TextJituSeikyuuTanka.ID = TextJituSeikyuuTanka.ID & "_" & index.ToString()
        TextZeinukiKingaku.ID = TextZeinukiKingaku.ID & "_" & index.ToString()
        TextSyouhizeiGaku.ID = TextSyouhizeiGaku.ID & "_" & index.ToString()
        TextZeikomiKingaku.ID = TextZeikomiKingaku.ID & "_" & index.ToString()
        TextSeikyuusyoHakkouBi.ID = TextSeikyuusyoHakkouBi.ID & "_" & index.ToString()
        TextUriageNengappi.ID = TextUriageNengappi.ID & "_" & index.ToString()
        TextDenpyouUriDate.ID = TextDenpyouUriDate.ID & "_" & index.ToString()

        buttonChgSyouhinCd.ID = buttonChgSyouhinCd.ID & "_" & index.ToString()
        buttonChgKoumu.ID = buttonChgKoumu.ID & "_" & index.ToString()
        buttonChgJitu.ID = buttonChgJitu.ID & "_" & index.ToString()
        buttonChgSuuryou.ID = buttonChgSuuryou.ID & "_" & index.ToString()

        hiddenMiseCd.ID = hiddenMiseCd.ID & "_" & index.ToString()
        hiddenTyousaSeikyuuSaki.ID = hiddenTyousaSeikyuuSaki.ID & "_" & index.ToString()
        hiddenHansokuHinSeikyuuSaki.ID = hiddenHansokuHinSeikyuuSaki.ID & "_" & index.ToString()
        hiddenKeiretuCd.ID = hiddenKeiretuCd.ID & "_" & index.ToString()

        If tenbetuMode <> DisplayMode.SANSYOU Then
            TdGyouSyori.ID = TdGyouSyori.ID & "_" & index.ToString()
            buttonGyouSakujo.ID = buttonGyouSakujo.ID & "_" & index.ToString()
            If tenbetuMode = DisplayMode.HANSOKU Then
                Me.TextUriageNengappi.Attributes("readOnly") = "readonly"
                Me.TextUriageNengappi.Attributes("class") += " readOnlyStyle"
                Me.TextUriageNengappi.Attributes("tabindex") = -1
                Me.TextDenpyouUriDate.Attributes("readOnly") = "readonly"
                Me.TextDenpyouUriDate.Attributes("class") += " readOnlyStyle"
                Me.TextDenpyouUriDate.Attributes("tabindex") = -1
            End If
        Else
            TextHassouDate.Attributes("readOnly") = "readonly"
            TextHassouDate.Attributes("class") += " readOnlyStyle"
            TextSyouhinCd.Attributes("readOnly") = "readonly"
            TextSyouhinCd.Attributes("class") += " readOnlyStyle"
            buttonKensaku.Visible = False
            TextSyouhinMei.Attributes("readOnly") = "readonly"
            TextKoumutenSeikyuuTanka.Attributes("readOnly") = "readonly"
            TextKoumutenSeikyuuTanka.Attributes("class") += " readOnlyStyle"
            TextSuuryou.Attributes("readOnly") = "readonly"
            TextSuuryou.Attributes("class") += " readOnlyStyle"
            TextJituSeikyuuTanka.Attributes("readOnly") = "readonly"
            TextJituSeikyuuTanka.Attributes("class") += " readOnlyStyle"
            TextZeinukiKingaku.Attributes("readOnly") = "readonly"
            TextSyouhizeiGaku.Attributes("readOnly") = "readonly"
            TextZeikomiKingaku.Attributes("readOnly") = "readonly"
            TextSeikyuusyoHakkouBi.Attributes("readOnly") = "readonly"
            TextSeikyuusyoHakkouBi.Attributes("class") += " readOnlyStyle"
            TextUriageNengappi.Attributes("readOnly") = "readonly"
            TextUriageNengappi.Attributes("class") += " readOnlyStyle"
            Me.TextDenpyouUriDate.Attributes("readOnly") = "readonly"
            Me.TextDenpyouUriDate.Attributes("class") += " readOnlyStyle"
            Me.TextDenpyouUriDate.Attributes("tabindex") = -1
            TdGyouSyori.Visible = False
            buttonGyouSakujo.Visible = False
        End If
        If Me.TrHansokuRecord.Style("display") = "none" Then
            'SQL種別判断フラグを削除にセット
            Me.hiddenSqlTypeFlg.Value = enSqlTypeFlg.DELETE
        Else
            'SQL種別判断フラグを更新にセット
            Me.hiddenSqlTypeFlg.Value = enSqlTypeFlg.UPDATE
        End If
    End Sub

    ''' <summary>
    ''' コントロールに値をセットする
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetValue(ByVal recHansoku As TenbetuHansokuHinRecord)

        '画面情報を取得
        With recHansoku
            Me.hiddenMiseCd.Value = .MiseCd
            Me.hiddenNyuuryokuDate.Value = DtTime2Str(.NyuuryokuDate, True)
            Me.hiddenNyuuryokuDateNo.Value = .NyuuryokuDateNo
            Me.hiddenTyousaSeikyuuSaki.Value = .TysSeikyuuSaki
            Me.hiddenHansokuHinSeikyuuSaki.Value = .HansokuhinSeikyuusaki
            Me.hiddenSoukoCd.Value = .BunruiCd
            Me.TextHassouDate.Value = DtTime2Str(.HassouDate)
            Me.TextSyouhinCd.Value = .SyouhinCd
            Me.hiddenOldSyouhinCd.Value = .SyouhinCd
            Me.TextSyouhinMei.Value = .SyouhinMei
            Me.TextKoumutenSeikyuuTanka.Value = .KoumutenSeikyuuTanka
            Me.hiddenZeiritu.Value = .Zeiritu
            Me.TextJituSeikyuuTanka.Value = .Tanka
            Me.TextSuuryou.Value = .Suu
            Me.TextZeinukiKingaku.Value = Long.Parse(.Tanka) * Long.Parse(.Suu)
            Me.TextSyouhizeiGaku.Value = .SyouhiZei
            Me.TextZeikomiKingaku.Value = .ZeikomiGaku
            Me.TextSeikyuusyoHakkouBi.Value = DtTime2Str(.SeikyuusyoHakDate)
            Me.TextUriageNengappi.Value = DtTime2Str(.UriDate)
            Me.TextDenpyouUriDateDisplay.Value = DtTime2Str(.DenpyouUriDate)
            Me.TextDenpyouUriDate.Value = DtTime2Str(.DenpyouUriDate)
            Me.hiddenUpdateTime.Value = DtTime2Str(.UpdDatetime, True)
            Me.hiddenZeiKbn.Value = .ZeiKbn
            Me.hiddenIsFc.Value = .IsFc
            Me.hiddenKeiretuCd.Value = .KeiretuCd
            Me.HiddenUriKeijyouFlg.Value = .UriKeijyouFlg
            enIsFc = .IsFc
        End With

        '画面自動設定判断（販促品）
        judgeHansokuAutoSetting(Me.TextSyouhinCd.Value)
        If eSaki = enSeikyuuSaki.Hoka And eKretu = enKeiretu.NotSanK Then
            Me.TextKoumutenSeikyuuTanka.Value = "0"
        End If

        '工務店税抜き金額活性制御
        activeControlKoumuten()
    End Sub
#End Region

#Region "画面項目変更時制御"
    ''' <summary>
    ''' 発送日変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgHassouDate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgHassouDate.ServerClick

        sendClassDispInfo(clsLogic)

        If enMode = DisplayMode.HANSOKU Then
            '請求書発行日の設定
            If Me.TextSeikyuusyoHakkouBi.Value = "" Then
                Me.TextSeikyuusyoHakkouBi.Value = clsLogic.GetHansokuhinSeikyuusyoHakkoudate(Me.hiddenMiseCd.Value, Me.TextSyouhinCd.Value)
            End If
            If Me.TextUriageNengappi.Value = "" Then
                Me.TextUriageNengappi.Value = DateTime.Today
            End If
            '伝票売上年月日修正
            If Me.TextDenpyouUriDate.Value = "" Then
                Me.TextDenpyouUriDate.Value = Me.TextUriageNengappi.Value
            End If
        End If

        'フォーカスの設定
        SetFocusAjax(Me.TextSyouhinCd)
    End Sub

    ''' <summary>
    ''' 商品CD変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgSyouhinCd_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgSyouhinCd.ServerClick
        Dim clsJibanL As New JibanLogic
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        Dim intJituGaku As Integer
        Dim intSuuryou As Integer
        Dim intZei As Integer
        Dim intZeiNukiGaku As Integer
        Dim intZeiKomiGaku As Integer
        Dim intKoumuGaku As Integer
        Dim blnFlg As Boolean
        Dim MLogic As New MessageLogic
        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        'フォーカスの設定
        SetFocusAjax(Me.buttonKensaku)

        sendClassDispInfo(clsLogic)

        '金額変更イベント制御（販促品）設定
        Me.hiddenTxtChgCtrlHansoku.Value = enTxtChgCtrlHansoku.Reset.ToString

        If Me.TextSyouhinCd.Value = "" Then
            Me.TextSyouhinMei.Value = ""
            Me.TextKoumutenSeikyuuTanka.Value = ""
            Me.TextJituSeikyuuTanka.Value = ""
            Me.TextSuuryou.Value = ""
            Me.TextSyouhizeiGaku.Value = ""
            Me.TextZeinukiKingaku.Value = ""
            Me.TextZeikomiKingaku.Value = ""
            If enMode <> DisplayMode.TENBETU Then
                Me.TextHassouDate.Value = ""
            End If
            Me.TextUriageNengappi.Value = ""
            Me.TextDenpyouUriDateDisplay.Value = String.Empty
            Me.TextDenpyouUriDate.Value = String.Empty
            Me.TextSeikyuusyoHakkouBi.Value = ""
            Me.hiddenOldSyouhinCd.Value = ""
            Exit Sub
        End If

        '金額・数量の取得
        If Me.TextJituSeikyuuTanka.Value <> "" Then
            intJituGaku = CInt(Me.TextJituSeikyuuTanka.Value)
        End If
        If Me.TextKoumutenSeikyuuTanka.Value <> "" Then
            intKoumuGaku = CInt(Me.TextKoumutenSeikyuuTanka.Value)
        End If
        If Me.TextSuuryou.Value.Length = 0 Then
            Me.TextSuuryou.Value = 1
        End If
        If Me.TextSuuryou.Value <> "" Then
            intSuuryou = CInt(Me.TextSuuryou.Value)
        End If

        '商品明細の取得
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.TextSyouhinCd.Value, Me.hiddenSoukoCd.Value)

        '売上年月日で判断して、正しい税率を取得する
        strSyouhinCd = Me.TextSyouhinCd.Value
        If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Value.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '取得した税区分・税率をセット
            recSyouhinMeisai.Zeiritu = strZeiritu
            recSyouhinMeisai.ZeiKbn = strZeiKbn
        End If

        '画面自動設定判断
        judgeHansokuAutoSetting(Me.TextSyouhinCd.Value)

        'FCの場合
        If Me.hiddenIsFc.Value = IsFcMode.FC Then
            If recSyouhinMeisai IsNot Nothing Then
                intJituGaku = recSyouhinMeisai.HyoujunKkk
                intZeiNukiGaku = intJituGaku * intSuuryou
                intZei = Integer.Parse(Fix(intZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intZeiNukiGaku + intZei
            Else
                'テーブルサイズ再設定
                TableResizeBeforAlert(Me)
                MLogic.AlertMessage(sender, Messages.MSG001E)
                Exit Sub
            End If
        Else
            '自動設定パターン判断
            Select Case True
                Case recSyouhinMeisai Is Nothing
                    Exit Sub
                   
                Case eSaki = enSeikyuuSaki.Tyoku
                    intKoumuGaku = recSyouhinMeisai.HyoujunKkk
                    intJituGaku = intKoumuGaku
                    intZeiNukiGaku = intJituGaku * intSuuryou
                    intZei = Integer.Parse(Fix(intZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                    intZeiKomiGaku = intZeiNukiGaku + intZei

                Case eSaki = enSeikyuuSaki.Hoka _
                 And eKretu = enKeiretu.SanK
                    intKoumuGaku = recSyouhinMeisai.HyoujunKkk
                    If intKoumuGaku = 0 Then
                        intJituGaku = 0
                    Else
                        blnFlg = clsJibanL.GetSeikyuuGaku(sender _
                                                        , 3 _
                                                        , Me.hiddenKeiretuCd.Value _
                                                        , recSyouhinMeisai.SyouhinCd _
                                                        , -1 _
                                                        , intJituGaku)
                    End If
                    intZeiNukiGaku = intJituGaku * intSuuryou
                    intZei = Integer.Parse(Fix(intZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                    intZeiKomiGaku = intZeiNukiGaku + intZei

                Case eSaki = enSeikyuuSaki.Hoka _
                 And eKretu = enKeiretu.NotSanK
                    intJituGaku = recSyouhinMeisai.HyoujunKkk
                    intZeiNukiGaku = intJituGaku * intSuuryou
                    intZei = Integer.Parse(Fix(intZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                    intZeiKomiGaku = intZeiNukiGaku + intZei

                Case Else
                    Exit Sub
            End Select
        End If
        Me.TextKoumutenSeikyuuTanka.Value = intKoumuGaku
        Me.TextJituSeikyuuTanka.Value = intJituGaku
        Me.TextSyouhizeiGaku.Value = intZei
        Me.TextZeikomiKingaku.Value = intZeiKomiGaku
        Me.TextZeinukiKingaku.Value = intZeiNukiGaku
        Me.hiddenOldSyouhinCd.Value = recSyouhinMeisai.SyouhinCd
        Me.hiddenZeiritu.Value = recSyouhinMeisai.Zeiritu

        If recSyouhinMeisai IsNot Nothing Then
            '商品名の設定
            Me.TextSyouhinMei.Value = recSyouhinMeisai.SyouhinMei
        End If

        If Me.hiddenSqlTypeFlg.Value = EarthConst.enSqlTypeFlg.INSERT Then
            '税区分の設定
            Me.hiddenZeiKbn.Value = recSyouhinMeisai.ZeiKbn
        End If

        If enMode = DisplayMode.HANSOKU Then
            '請求書発行日の設定
            Me.TextSeikyuusyoHakkouBi.Value = clsLogic.GetHansokuhinSeikyuusyoHakkoudate(Me.hiddenMiseCd.Value, Me.TextSyouhinCd.Value)
        End If

        '工務店税抜き金額活性制御
        activeControlKoumuten()

    End Sub

    ''' <summary>
    ''' 工務店請求単価変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgKoumu_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgKoumu.ServerClick
        Dim clsJibanL As New JibanLogic
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        Dim intJituGaku As Integer
        Dim intSuuryou As Integer
        Dim intZei As Integer
        Dim intZeiNukiGaku As Integer
        Dim intZeiKomiGaku As Integer
        Dim intKoumuGaku As Integer
        Dim blnFlg As Boolean
        Dim MLogic As New MessageLogic

        'フォーカスの設定
        SetFocusAjax(Me.TextJituSeikyuuTanka)

        '金額・数量の取得
        If Me.TextJituSeikyuuTanka.Value <> "" Then
            intJituGaku = CInt(Me.TextJituSeikyuuTanka.Value)
        End If
        If Me.TextKoumutenSeikyuuTanka.Value <> "" Then
            intKoumuGaku = CInt(Me.TextKoumutenSeikyuuTanka.Value)
        End If
        If Me.TextSuuryou.Value.Length = 0 Then
            Me.TextSuuryou.Value = 1
        End If
        If Me.TextSuuryou.Value <> "" Then
            intSuuryou = CInt(Me.TextSuuryou.Value)
        End If

        '金額変更イベント制御（販促品）設定
        If Me.hiddenTxtChgCtrlHansoku.Value = enTxtChgCtrlHansoku.JituGaku.ToString Then
            Me.TextKoumutenSeikyuuTanka.Value = intKoumuGaku
            'フォーカスの設定
            SetFocusAjax(Me.TextSuuryou)
            Exit Sub
        Else
            '金額変更イベント制御（販促品）設定
            Me.hiddenTxtChgCtrlHansoku.Value = enTxtChgCtrlHansoku.KoumuGaku.ToString
        End If

        If Me.TextSyouhinCd.Value = "" Then
            Exit Sub
        End If

        '商品明細の取得
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.TextSyouhinCd.Value, Me.hiddenSoukoCd.Value)

        '画面自動設定判断
        judgeHansokuAutoSetting(Me.TextSyouhinCd.Value)

        '自動設定パターン判断
        Select Case True
            Case recSyouhinMeisai Is Nothing
                'テーブルサイズ再設定
                TableResizeBeforAlert(Me)
                MLogic.AlertMessage(sender, Messages.MSG001E)
                Exit Sub

            Case eSaki = enSeikyuuSaki.Tyoku
                intJituGaku = intKoumuGaku
                intZeiNukiGaku = intJituGaku * intSuuryou
                intZei = Integer.Parse(Fix(intZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intZeiNukiGaku + intZei

            Case eSaki = enSeikyuuSaki.Hoka _
             And eKretu = enKeiretu.SanK
                blnFlg = clsJibanL.GetSeikyuuGaku(sender _
                                                , 5 _
                                                , Me.hiddenKeiretuCd.Value _
                                                , recSyouhinMeisai.SyouhinCd _
                                                , intKoumuGaku _
                                                , intJituGaku)
                intZeiNukiGaku = intJituGaku * intSuuryou
                intZei = Integer.Parse(Fix(intZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                intZeiKomiGaku = intZeiNukiGaku + intZei

            Case Else
                '何もしない
        End Select

        Me.TextJituSeikyuuTanka.Value = intJituGaku
        Me.TextSyouhizeiGaku.Value = intZei
        Me.TextZeikomiKingaku.Value = intZeiKomiGaku
        Me.TextKoumutenSeikyuuTanka.Value = intKoumuGaku
        Me.TextZeinukiKingaku.Value = intZeiNukiGaku

        '工務店税抜き金額活性制御
        activeControlKoumuten()

    End Sub

    ''' <summary>
    ''' 実請求単価変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgJitu_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgJitu.ServerClick
        Dim clsJibanL As New JibanLogic
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        Dim lngJituGaku As Long
        Dim intSuuryou As Integer
        Dim lngZei As Long
        Dim lngZeiNukiGaku As Long
        Dim lngZeiKomiGaku As Long
        Dim intKoumuGaku As Integer
        Dim blnFlg As Boolean
        Dim MLogic As New MessageLogic
        Dim decZeiRitu As Decimal
        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        'フォーカスの設定
        SetFocusAjax(Me.TextSuuryou)

        '金額・数量の取得
        If Me.TextJituSeikyuuTanka.Value <> "" Then
            lngJituGaku = CLng(Me.TextJituSeikyuuTanka.Value)
        End If
        If Me.TextKoumutenSeikyuuTanka.Value <> "" Then
            intKoumuGaku = CInt(Me.TextKoumutenSeikyuuTanka.Value)
        End If
        If Me.TextSuuryou.Value <> "" Then
            intSuuryou = CInt(Me.TextSuuryou.Value)
        End If
        '数量の設定
        If Me.TextSuuryou.Value.Length = 0 Then
            Me.TextSuuryou.Value = 1
        End If
        '税率の設定
        decZeiRitu = Me.hiddenZeiritu.Value

        '商品コードがブランク時は再計算しない
        If Me.TextSyouhinCd.Value = "" Then
            Exit Sub
        End If

        '金額変更イベント制御（販促品）設定
        If Me.hiddenTxtChgCtrlHansoku.Value = enTxtChgCtrlHansoku.KoumuGaku.ToString Then
            lngZeiNukiGaku = lngJituGaku * intSuuryou
            lngZei = Long.Parse(Fix(lngZeiNukiGaku * decZeiRitu))
            lngZeiKomiGaku = lngZeiNukiGaku + lngZei
            Me.TextJituSeikyuuTanka.Value = lngJituGaku
            Me.TextSyouhizeiGaku.Value = lngZei
            Me.TextZeikomiKingaku.Value = lngZeiKomiGaku
            Me.TextKoumutenSeikyuuTanka.Value = intKoumuGaku
            Me.TextZeinukiKingaku.Value = lngZeiNukiGaku

            Exit Sub
        Else
            '金額変更イベント制御（販促品）設定
            Me.hiddenTxtChgCtrlHansoku.Value = enTxtChgCtrlHansoku.JituGaku.ToString
        End If

        '商品明細の取得
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.TextSyouhinCd.Value, Me.hiddenSoukoCd.Value)

        '売上年月日で判断して、正しい税率を取得する
        strSyouhinCd = Me.TextSyouhinCd.Value
        If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Value.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '取得した税区分・税率をセット
            recSyouhinMeisai.Zeiritu = strZeiritu
            recSyouhinMeisai.ZeiKbn = strZeiKbn
        End If

        '画面自動設定判断
        judgeHansokuAutoSetting(Me.TextSyouhinCd.Value)

        '自動設定パターン判断
        Select Case True
            Case recSyouhinMeisai Is Nothing
                'テーブルサイズ再設定
                TableResizeBeforAlert(Me)
                MLogic.AlertMessage(sender, Messages.MSG001E)
                Exit Sub

            Case eSaki = enSeikyuuSaki.Tyoku
                intKoumuGaku = lngJituGaku
                lngZeiNukiGaku = lngJituGaku * intSuuryou
                lngZei = Long.Parse(Fix(lngZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                lngZeiKomiGaku = lngZeiNukiGaku + lngZei

            Case eSaki = enSeikyuuSaki.Hoka _
             And eKretu = enKeiretu.SanK
                blnFlg = clsJibanL.GetSeikyuuGaku(sender _
                                                , 4 _
                                                , Me.hiddenKeiretuCd.Value _
                                                , recSyouhinMeisai.SyouhinCd _
                                                , lngJituGaku _
                                                , intKoumuGaku)
                lngZeiNukiGaku = lngJituGaku * intSuuryou
                lngZei = Long.Parse(Fix(lngZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                lngZeiKomiGaku = lngZeiNukiGaku + lngZei

            Case eSaki = enSeikyuuSaki.Hoka _
             And eKretu = enKeiretu.NotSanK
                lngZeiNukiGaku = lngJituGaku * intSuuryou
                lngZei = Long.Parse(Fix(lngZeiNukiGaku * recSyouhinMeisai.Zeiritu))
                lngZeiKomiGaku = lngZeiNukiGaku + lngZei

            Case Else
                '何もしない
        End Select

        Me.TextJituSeikyuuTanka.Value = lngJituGaku
        Me.TextSyouhizeiGaku.Value = lngZei
        Me.TextZeikomiKingaku.Value = lngZeiKomiGaku
        Me.TextKoumutenSeikyuuTanka.Value = intKoumuGaku
        Me.TextZeinukiKingaku.Value = lngZeiNukiGaku

        '工務店税抜き金額活性制御
        activeControlKoumuten()

    End Sub

    ''' <summary>
    ''' 数量変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgSuuryou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgSuuryou.ServerClick
        Dim lngJituGaku As Long
        Dim intSuuryou As Integer
        Dim lngZei As Long
        Dim lngZeiNukiGaku As Long
        Dim lngZeiKomiGaku As Long
        Dim intKoumuGaku As Integer
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        Dim MLogic As New MessageLogic
        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        'フォーカスの設定
        SetFocusAjax(Me.TextSyouhizeiGaku)

        '金額・数量の取得
        If Me.TextJituSeikyuuTanka.Value <> "" Then
            lngJituGaku = CLng(Me.TextJituSeikyuuTanka.Value)
        End If
        If Me.TextKoumutenSeikyuuTanka.Value <> "" Then
            intKoumuGaku = CInt(Me.TextKoumutenSeikyuuTanka.Value)
        End If
        If Me.TextSuuryou.Value.Length = 0 Then
            Me.TextSuuryou.Value = 1
        End If
        If Me.TextSuuryou.Value <> "" Then
            intSuuryou = CInt(Me.TextSuuryou.Value)
        End If

        If Me.TextSyouhinCd.Value = "" Then
            Exit Sub
        End If

        '商品明細の取得
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.TextSyouhinCd.Value, Me.hiddenSoukoCd.Value)

        '売上年月日で判断して、正しい税率を取得する
        strSyouhinCd = Me.TextSyouhinCd.Value
        If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Value.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '取得した税区分・税率をセット
            recSyouhinMeisai.Zeiritu = strZeiritu
            recSyouhinMeisai.ZeiKbn = strZeiKbn
        End If

        '画面自動設定判断
        judgeHansokuAutoSetting(Me.TextSyouhinCd.Value)

        If recSyouhinMeisai Is Nothing Then
            'テーブルサイズ再設定
            TableResizeBeforAlert(Me)
            MLogic.AlertMessage(sender, Messages.MSG001E)
            Exit Sub
        End If
        lngZeiNukiGaku = lngJituGaku * intSuuryou
        lngZei = Long.Parse(Fix(lngZeiNukiGaku * recSyouhinMeisai.Zeiritu))
        lngZeiKomiGaku = lngZeiNukiGaku + lngZei

        Me.TextJituSeikyuuTanka.Value = lngJituGaku
        Me.TextSyouhizeiGaku.Value = lngZei
        Me.TextZeikomiKingaku.Value = lngZeiKomiGaku.ToString
        Me.TextKoumutenSeikyuuTanka.Value = intKoumuGaku
        Me.TextZeinukiKingaku.Value = lngZeiNukiGaku

        '工務店税抜き金額活性制御
        activeControlKoumuten()

    End Sub

    ''' <summary>
    ''' 消費税額変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgSyouhiZei_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgSyouhiZei.ServerClick
        Dim dtLogic As New DataLogic
        Dim intTanka As Integer = dtLogic.str2Int(Me.TextJituSeikyuuTanka.Value, System.Globalization.NumberStyles.AllowThousands)
        Dim intSuuRyou As Integer = dtLogic.str2Int(Me.TextSuuryou.Value)
        Dim intZeiGaku As Integer = dtLogic.str2Int(Me.TextSyouhizeiGaku.Value, System.Globalization.NumberStyles.AllowThousands)

        '商品コードがブランク時は再計算しない
        If Me.TextSyouhinCd.Value = "" Then
            Exit Sub
        End If

        'フォーカスの設定
        SetFocusAjax(Me.TextSeikyuusyoHakkouBi)

        If intTanka = 0 Or intSuuRyou = 0 Then
            intZeiGaku = 0
        End If

        Me.TextJituSeikyuuTanka.Value = intTanka
        Me.TextSuuryou.Value = intSuuRyou
        Me.TextSyouhizeiGaku.Value = intZeiGaku

        Me.TextZeikomiKingaku.Value = intTanka * intSuuRyou + intZeiGaku


    End Sub

    ''' <summary>
    ''' 売上年月日変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgUriDate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgUriDate.ServerClick
        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        '伝票売上年月日の自動設定
        If Me.TextUriageNengappi.Value <> String.Empty Then
            If Me.TextDenpyouUriDate.Value = String.Empty Then
                Me.TextDenpyouUriDate.Value = Me.TextUriageNengappi.Value
                buttonChgDenUriDate_ServerClick(sender, e)
            End If

            '税区分・税率を再取得
            strSyouhinCd = Me.TextSyouhinCd.Value
            If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Value.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                '取得した税区分・税率をセット
                Me.hiddenZeiKbn.Value = strZeiKbn
                Me.hiddenZeiritu.Value = strZeiritu

                '金額計算
                SetKingaku(strZeiritu)
            End If
        Else
            '売上年月日未入力の場合

            '商品コード未入力以外
            If Me.TextSyouhinCd.Value <> String.Empty Then

                '税区分・税率を再取得
                strSyouhinCd = Me.TextSyouhinCd.Value
                If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Value.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                    '取得した税区分・税率をセット
                    Me.hiddenZeiKbn.Value = strZeiKbn
                    Me.hiddenZeiritu.Value = strZeiritu

                    '金額計算
                    SetKingaku(strZeiritu)
                End If
            End If
        End If

        'フォーカスの設定
        SetFocusAjax(Me.TextDenpyouUriDate)
    End Sub

    ''' <summary>
    ''' 伝票売上年月日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonChgDenUriDate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChgDenUriDate.ServerClick
        sendClassDispInfo(clsLogic)

        '請求書発行日の自動設定
        If Me.TextDenpyouUriDate.Value <> String.Empty Then
            Me.TextSeikyuusyoHakkouBi.Value = clsLogic.GetHansokuhinSeikyuusyoHakkoudate(Me.hiddenMiseCd.Value _
                                                                                        , Me.TextSyouhinCd.Value _
                                                                                        , Me.TextDenpyouUriDate.Value)
        Else
            Me.TextSeikyuusyoHakkouBi.Value = String.Empty
        End If

        'フォーカスの設定
        SetFocusAjax(Me.buttonGyouSakujoCall)

    End Sub

    ''' <summary>
    ''' 画面自動設定判断（販促品）
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <remarks></remarks>
    Private Sub judgeHansokuAutoSetting(ByVal strSyouhinCd As String)
        Dim jLogic As New JibanLogic
        Dim itemRec As Syouhin23Record

        '請求先判断
        If Me.hiddenIsFc.Value = IsFcMode.FC Then
            'FCの場合
            itemRec = Nothing
        Else
            'FC以外の場合
            itemRec = jLogic.GetSyouhinInfo(strSyouhinCd, EarthEnum.EnumSyouhinKubun.HansokuNotFc, Me.hiddenMiseCd.Value)
        End If

        '請求タイプ判定
        If itemRec IsNot Nothing AndAlso itemRec.SeikyuuSakiType = EarthConst.SEIKYU_TYOKUSETU Then
            eSaki = enSeikyuuSaki.Tyoku
        Else
            eSaki = enSeikyuuSaki.Hoka
        End If

        '請求タイプの設定
        SetSeikyuuType(eSaki)

        '系列判断
        Select Case Me.hiddenKeiretuCd.Value
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
    ''' 工務店税抜き金額活性制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub activeControlKoumuten()
        Dim ht As New Hashtable

        If eSaki = enSeikyuuSaki.Hoka And eKretu = enKeiretu.NotSanK Then
            clsCL.chgVeiwMode(TextKoumutenSeikyuuTanka)
        Else
            clsCL.chgDispSyouhinText(TextKoumutenSeikyuuTanka)
        End If
    End Sub
#End Region

#Region "ボタン押下処理"
    ''' <summary>
    ''' 行削除押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonGyouSakujo_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonGyouSakujo.ServerClick
        Me.TrHansokuRecord.Style("display") = "none"
        'SQL種別判断フラグを削除にセット
        Me.hiddenSqlTypeFlg.Value = enSqlTypeFlg.DELETE
    End Sub

    ''' <summary>
    ''' 親画面の新規行追加押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub NewButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strSoukoCd As String, ByVal intIsFc As IsFcMode)
        '倉庫コードを設定
        Me.hiddenSoukoCd.Value = strSoukoCd
        Me.hiddenIsFc.Value = intIsFc
        'SQL種別判断フラグを登録にセット
        Me.hiddenSqlTypeFlg.Value = enSqlTypeFlg.INSERT

        sendClassDispInfo(clsLogic)

        '新規行を追加の場合、請求書発行日・売上年月日を自動設定
        If hiddenSqlTypeFlg.Value = enSqlTypeFlg.INSERT And enIsFc = IsFcMode.NOT_FC Then
            Me.TextSeikyuusyoHakkouBi.Value = clsLogic.GetHansokuhinSeikyuusyoHakkoudate(Me.hiddenMiseCd.Value, Me.TextSyouhinCd.Value)
            Me.TextUriageNengappi.Value = DateTime.Today
            Me.TextDenpyouUriDate.Value = DateTime.Today
        End If

        '商品コード変更時の処理を実行
        Me.buttonChgSyouhinCd_ServerClick(sender, e)
    End Sub

    ''' <summary>
    ''' 商品マスタ検索押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub buttonKensaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonKensaku.ServerClick
        Dim recSyouhinMeisai As SyouhinMeisaiRecord
        '商品明細の取得
        recSyouhinMeisai = clsLogic.GetSyouhinMeisaiRec(Me.TextSyouhinCd.Value, Me.hiddenSoukoCd.Value)
        If recSyouhinMeisai Is Nothing Then
            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpScript As String = "callSearch('" & Me.TextSyouhinCd.ClientID & EarthConst.SEP_STRING & Me.hiddenSoukoCd.ClientID & "','" & UrlConst.SEARCH_SYOUHIN & "','" & Me.TextSyouhinCd.ClientID & "','" & Me.buttonChgSyouhinCd.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If
    End Sub
#End Region

#Region "関数"
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
    ''' 数字を3桁区切りの文字列に変更します
    ''' </summary>
    ''' <param name="strNumber">対象数値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function StrNum2Str(ByVal strNumber As String, Optional ByVal blnThousandsFormat As Boolean = True) As String
        Dim strRet As String
        If strNumber <> "" AndAlso IsNumeric(strNumber) Then
            strRet = Format(CLng(strNumber), EarthConst.FORMAT_KINGAKU_1)
        Else
            strRet = strNumber
        End If
        Return strRet
    End Function

    ''' <summary>
    ''' 日付型を文字列に変換します
    ''' </summary>
    ''' <param name="dtValue">変換したい日付</param>
    ''' <param name="blnMillisecond">ミリ秒表示判断フラグ</param>
    ''' <returns>変換後の文字列</returns>
    ''' <remarks></remarks>
    Private Function DtTime2Str(ByVal dtValue As Date, Optional ByVal blnMillisecond As Boolean = False) As String
        Dim strRet As String
        If dtValue = DateTime.MinValue Then
            strRet = ""
        Else
            If blnMillisecond = True Then
                strRet = dtValue.ToString(EarthConst.FORMAT_DATE_TIME_2)
            Else
                strRet = dtValue.ToString("yyyy/MM/dd")
            End If
        End If
        Return strRet
    End Function

    ''' <summary>
    ''' 画面共通情報をロジッククラスへ引き渡す
    ''' </summary>
    ''' <param name="clsLogic">店別修正ロジッククラス</param>
    ''' <remarks></remarks>
    Public Sub sendClassDispInfo(ByRef clsLogic As TenbetuSyuuseiLogic)
        '画面共通情報の設定
        With clsLogic
            .MiseCd = Me.hiddenMiseCd.Value
            .IsFC = Me.hiddenIsFc.Value
        End With
    End Sub

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
    ''' 請求タイプ設定処理
    ''' </summary>
    ''' <param name="eSaki">請求タイプ（直接/他）</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuType(ByVal eSaki As enSeikyuuSaki)
        '直接請求/他請求判断
        If eSaki = enSeikyuuSaki.Tyoku Then
            Me.lblSeikyuuType.Text = EarthConst.SEIKYU_TYOKUSETU_SHORT
        Else
            Me.lblSeikyuuType.Text = EarthConst.SEIKYU_TASETU_SHORT
        End If

        '営業所の場合は常にFC請求
        If Me.hiddenIsFc.Value = IsFcMode.FC Then
            Me.lblSeikyuuType.Text = EarthConst.SEIKYU_FCSETU_SHORT
        End If
    End Sub

    ''' <summary>
    ''' 画面読み込み時の値をHidden項目に退避
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub setOpenValues()
        Me.HiddenOpenValues.Value = getCtrlValuesString()
    End Sub

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する"
    ''' </summary>
    ''' <returns>画面コントロールの値を結合した文字列</returns>
    ''' <remarks></remarks>
    Public Function getCtrlValuesString() As String
        Dim sb As New StringBuilder

        sb.Append(Me.TextSyouhinCd.Value & EarthConst.SEP_STRING)
        sb.Append(Me.TextJituSeikyuuTanka.Value.Replace(",", "") & EarthConst.SEP_STRING)
        sb.Append(Me.TextSyouhizeiGaku.Value.Replace(",", "") & EarthConst.SEP_STRING)
        sb.Append(Me.TextZeikomiKingaku.Value.Replace(",", "") & EarthConst.SEP_STRING)
        sb.Append(Me.TextUriageNengappi.Value & EarthConst.SEP_STRING)
        sb.Append(Me.TextDenpyouUriDate.Value & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuusyoHakkouBi.Value & EarthConst.SEP_STRING)

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 金額設定
    ''' </summary>
    ''' <param name="strZeiritu">税率</param>
    ''' <remarks></remarks>
    Private Sub SetKingaku(ByVal strZeiritu As String)
        Dim intJituGaku As Integer      '実請求税抜金額
        Dim intZei As Integer           '消費税
        Dim intZeiKomiGaku As Integer   '税込金額
        Dim intSuuryou As Integer
        Dim lngZeiNukiGaku As Long

        '画面に計算した値をセットする

        '実請求税抜金額、税率が空白の場合、税込金額、消費税を空白にする
        If Me.TextJituSeikyuuTanka.Value = String.Empty _
                    OrElse strZeiritu = String.Empty Then
            Me.TextZeikomiKingaku.Value = String.Empty
            Me.TextSyouhizeiGaku.Value = String.Empty
        Else
            '金額計算
            intJituGaku = Me.TextJituSeikyuuTanka.Value             '実請求税抜金額
            If Me.TextSuuryou.Value <> "" Then
                intSuuryou = CInt(Me.TextSuuryou.Value)
            End If
            lngZeiNukiGaku = intJituGaku * intSuuryou
            intZei = Integer.Parse(Fix(lngZeiNukiGaku * strZeiritu))   '消費税を計算(実請求税抜金額 * 取得した税率)
            intZeiKomiGaku = lngZeiNukiGaku + intZei                   '税込金額を計算(実請求税抜金額 + 消費税)

            Me.TextJituSeikyuuTanka.Value = intJituGaku             '実請求税抜金額
            Me.TextSyouhizeiGaku.Value = intZei                     '消費税
            Me.TextZeikomiKingaku.Value = intZeiKomiGaku            '税込金額
        End If

    End Sub
#End Region

End Class