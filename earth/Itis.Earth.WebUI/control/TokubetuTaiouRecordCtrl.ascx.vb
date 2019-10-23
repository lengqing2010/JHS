
Partial Public Class TokubetuTaiouRecordCtrl
    Inherits System.Web.UI.UserControl

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim iraiSession As New IraiSession
    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    Dim cbLogic As New CommonBizLogic
    Dim MLogic As New MessageLogic

#Region "コントロール値"
    Private Const KEIJYOU_ZUMI As String = "済"
#End Region

#Region "ユーザーコントロールへの外部からのアクセス用Getter/Setter"

    ''' <summary>
    ''' 外部からのアクセス用 for CheckBox特別対応
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccCheckBoxTokubetuTaiou() As HtmlInputCheckBox
        Get
            Return Me.CheckBoxTokubetuTaiou
        End Get
        Set(ByVal value As HtmlInputCheckBox)
            Me.CheckBoxTokubetuTaiou = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for Hidden特別対応コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnTokubetuTaiouCd() As HtmlInputHidden
        Get
            Return Me.HiddenTokubetuTaiouCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenTokubetuTaiouCd = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for Text特別対応名称
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSpanTokubetuTaiouMei() As HtmlGenericControl
        Get
            Return Me.SpanTokubetuTaiouMei
        End Get
        Set(ByVal value As HtmlGenericControl)
            Me.SpanTokubetuTaiouMei = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for Text金額加算商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextKasanSyouhinCd() As HtmlInputText
        Get
            Return Me.TextKasanSyouhinCd
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextKasanSyouhinCd = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for Text金額加算商品名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextKasanSyouhinMei() As HtmlInputText
        Get
            Return Me.TextKasanSyouhinMei
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextKasanSyouhinMei = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for Hidden分類コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnBunruiCd() As HtmlInputHidden
        Get
            Return Me.HiddenBunruiCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenBunruiCd = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for Hidden画面表示NO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnGamenHyoujiNo() As HtmlInputHidden
        Get
            Return Me.HiddenGamenHyoujiNo
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenGamenHyoujiNo = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for Text設定先
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextSetteiSaki() As HtmlInputText
        Get
            Return Me.TextSetteiSaki
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextSetteiSaki = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for Hidden設定先Style
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnHiddenSetteiSakiStyle() As HtmlInputHidden
        Get
            Return Me.HiddenSetteiSakiStyle
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenSetteiSakiStyle = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for Text売上計上
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTextUriKeijyou() As HtmlInputText
        Get
            Return Me.TextUriKeijyou
        End Get
        Set(ByVal value As HtmlInputText)
            Me.TextUriKeijyou = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for Hidden売上計上
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnUriKeijyou() As HtmlInputHidden
        Get
            Return Me.HiddenUriKeijyou
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenUriKeijyou = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for Hidden価格処理フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnKkkSyoriFlg() As HtmlInputHidden
        Get
            Return Me.HiddenKkkSyoriFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenKkkSyoriFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for Hidden発注書金額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnHattyuuKingaku() As HtmlInputHidden
        Get
            Return Me.HiddenHattyuuKingaku
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenHattyuuKingaku = value
        End Set
    End Property

#End Region

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '特別対応画面情報を取得（ScriptManager用）
        Dim myMaster As PopupTokubetuTaiou = Page.Page
        masterAjaxSM = myMaster.AjaxScriptManager

        'チェックボックスのチェックイベント時、ダミーボタンを押下する
        Dim strScript_UriKeijou As String = String.Empty
        Dim strScript_CheckAction As String = String.Empty

        '価格処理フラグがたっておらず、売上計上済でかつ、チェック状態を解除する場合、確認MSG表示後にチェックイベントを起動する
        strScript_UriKeijou = "if(objEBI('" & Me.AccHdnKkkSyoriFlg.ClientID & "').value != '1' && objEBI('" & Me.AccHdnUriKeijyou.ClientID & "').value == '1' && objEBI('" & Me.CheckBoxTokubetuTaiou.ClientID & "').checked == false){if(confirm('" & Messages.MSG199C & "')){objEBI('" & Me.AccHdnKkkSyoriFlg.ClientID & "').value = 1;}else{objEBI('" & Me.CheckBoxTokubetuTaiou.ClientID & "').checked = true;return false;}}"
        '青字の場合、設定先の変更イベントは起動不要
        strScript_CheckAction = "if(objEBI('" & Me.AccHdnHiddenSetteiSakiStyle.ClientID & "').value != 'blue') objEBI('" & Me.ButtonChkTokubetuTaiou.ClientID & "').click();"

        Me.CheckBoxTokubetuTaiou.Attributes("onclick") = strScript_UriKeijou & strScript_CheckAction

    End Sub

#Region "イベント"
    ''' <summary>
    ''' 親画面の特別対応チェックボックスチェック処理実行用カスタムイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">チェックボックスID</param>
    ''' <remarks></remarks>
    Public Event SetCheckTokubetuTaiouChangeAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)

    ''' <summary>
    ''' 親画面の特別対応チェックボックスチェック解除処理実行用カスタムイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">チェックボックスID</param>
    ''' <remarks></remarks>
    Public Event SetCheckOffTokubetuTaiouChangeAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)

#End Region

#Region "プライベートメソッド"

    ''' <summary>
    ''' 特別対応レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="dtRec">特別対応レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal dtRec As TokubetuTaiouRecordBase _
                                    )

        Dim strTokubetuTaiouCd As String = String.Empty '特別対応コード
        Dim strSpace As String = EarthConst.HANKAKU_SPACE

        '画面表示用
        Dim strKasanSyouhinCdDisp As String = String.Empty
        Dim strSyouhinMeiDisp As String = String.Empty
        Dim intUriKasanGakuDisp As Integer = Integer.MinValue
        Dim intKoumutenKasanGakuDisp As Integer = Integer.MinValue

        '特別対応データ
        Dim blnChecked As Boolean = False
        Dim strKasanSyouhinCdNew As String = String.Empty
        Dim strSyouhinMeiNew As String = String.Empty
        Dim intUriKasanGakuNew As Integer = Integer.MinValue
        Dim intKoumutenKasanGakuNew As Integer = Integer.MinValue

        '加盟店特別対応マスタ
        Dim blnCheckedMst As Boolean = False
        Dim strKasanSyouhinCdMst As String = String.Empty
        Dim strSyouhinMeiMst As String = String.Empty
        Dim intUriKasanGakuMst As Integer = Integer.MinValue
        Dim intKoumutenKasanGakuMst As Integer = Integer.MinValue

        '特別対応データ/Old
        Dim strKasanSyouhinCdOld As String = String.Empty
        Dim strSyouhinMeiOld As String = String.Empty
        Dim intUriKasanGakuOld As Integer = Integer.MinValue
        Dim intKoumutenKasanGakuOld As Integer = Integer.MinValue

        '******************************************
        '* 画面コントロールに設定
        '******************************************
        '特別対応コード
        strTokubetuTaiouCd = cl.GetDisplayString(dtRec.mTokubetuTaiouCd)
        Me.HiddenTokubetuTaiouCd.Value = strTokubetuTaiouCd
        '特別対応名称(名称のみ)
        Me.HiddenTokubetuTaiouMei.Value = cl.GetDisplayString(dtRec.TokubetuTaiouMeisyou)
        Me.SpanTokubetuTaiouMei.InnerHtml = Me.HiddenTokubetuTaiouMei.Value
        '価格処理フラグ
        Me.HiddenKkkSyoriFlg.Value = IIf(cl.GetDispNum(dtRec.KkkSyoriFlg) = EarthConst.ARI_VAL, EarthConst.ARI_VAL, EarthConst.NASI_VAL)
        '更新フラグ
        Me.HiddenUpdFlg.Value = IIf(cl.GetDispNum(dtRec.UpdFlg) = EarthConst.ARI_VAL, EarthConst.ARI_VAL, EarthConst.NASI_VAL)
        '分類コード
        Me.HiddenBunruiCd.Value = cl.GetDisplayString(dtRec.BunruiCd)
        '画面表示NO
        Me.HiddenGamenHyoujiNo.Value = cl.GetDispNum(dtRec.GamenHyoujiNo)

        '●加盟店特別対応マスタ
        blnCheckedMst = dtRec.kHanteiCheck
        strKasanSyouhinCdMst = cl.GetDisplayString(dtRec.kKasanSyouhinCd)
        strSyouhinMeiMst = cl.GetDisplayString(dtRec.kKasanSyouhinMei)
        intUriKasanGakuMst = cl.GetDispNum(dtRec.kUriKasanGaku)
        intKoumutenKasanGakuMst = cl.GetDispNum(dtRec.kKoumutenKasanGaku)

        '●特別対応データ/新
        blnChecked = dtRec.HanteiCheck
        strKasanSyouhinCdNew = cl.GetDisplayString(dtRec.KasanSyouhinCd)
        strSyouhinMeiNew = cl.GetDisplayString(dtRec.kasanSyouhinMei)
        intUriKasanGakuNew = cl.GetDispNum(dtRec.UriKasanGaku)
        intKoumutenKasanGakuNew = cl.GetDispNum(dtRec.KoumutenKasanGaku)

        '●特別対応データ/旧
        strKasanSyouhinCdOld = cl.GetDisplayString(dtRec.KasanSyouhinCdOld)
        strSyouhinMeiOld = cl.GetDisplayString(dtRec.kasanSyouhinMeiOld)
        intUriKasanGakuOld = cl.GetDispNum(dtRec.UriKasanGakuOld)
        intKoumutenKasanGakuOld = cl.GetDispNum(dtRec.KoumutenKasanGakuOld)

        '画面表示(優先項目を判定)：新、旧で差異がある場合、新で表示
        strKasanSyouhinCdDisp = IIf(strKasanSyouhinCdOld <> strKasanSyouhinCdNew, strKasanSyouhinCdNew, strKasanSyouhinCdOld)
        strSyouhinMeiDisp = IIf(strSyouhinMeiOld <> strSyouhinMeiNew, strSyouhinMeiNew, strSyouhinMeiOld)
        intUriKasanGakuDisp = IIf(intUriKasanGakuOld <> intUriKasanGakuNew, intUriKasanGakuNew, intUriKasanGakuOld)
        intKoumutenKasanGakuDisp = IIf(intKoumutenKasanGakuOld <> intKoumutenKasanGakuNew, intKoumutenKasanGakuNew, intKoumutenKasanGakuOld)

        '●画面項目にセット
        '特別対応チェック有無
        Me.CheckBoxTokubetuTaiou.Checked = blnChecked

        '取消=0の場合あるいは、取消<>0でも価格処理フラグ=1の場合、トラン値を表示
        If dtRec.PrimaryDisplay Then
            '金額加算商品コード
            Me.TextKasanSyouhinCd.Value = strKasanSyouhinCdDisp
            '商品名
            Me.TextKasanSyouhinMei.Value = strSyouhinMeiDisp
            '工務店請求加算金額
            Me.TextKoumutenSeikyuKasanKingaku.Value = Format(intKoumutenKasanGakuDisp, EarthConst.FORMAT_KINGAKU_1)
            '実請求加算金額
            Me.TextJituSeikyuKasanKingaku.Value = Format(intUriKasanGakuDisp, EarthConst.FORMAT_KINGAKU_1)

        Else 'マスタ値を表示
            '金額加算商品コード
            Me.TextKasanSyouhinCd.Value = strKasanSyouhinCdMst
            '商品名
            Me.TextKasanSyouhinMei.Value = strSyouhinMeiMst
            '工務店請求加算金額
            Me.TextKoumutenSeikyuKasanKingaku.Value = Format(intKoumutenKasanGakuMst, EarthConst.FORMAT_KINGAKU_1)
            '実請求加算金額
            Me.TextJituSeikyuKasanKingaku.Value = Format(intUriKasanGakuMst, EarthConst.FORMAT_KINGAKU_1)

        End If

        '価格処理フラグ
        If Me.HiddenKkkSyoriFlg.Value = EarthConst.ARI_VAL Then
            Me.TextKkkSyoriFlg.Value = KEIJYOU_ZUMI
        Else
            Me.TextKkkSyoriFlg.Value = String.Empty
        End If

        '****************************
        '* Hidden項目
        '****************************
        '●初期読込時の値を退避
        If IsPostBack = False Then
            '特別対応チェック有無(変更チェック用に退避)
            Me.HiddenChkJykyOld.Value = IIf(blnChecked = True, "1", "0")

            '金額加算商品コードDispOld
            Me.HiddenKasanSyouhinCdDispOld.Value = Me.TextKasanSyouhinCd.Value
            '金額加算商品名DispOld
            Me.HiddenKasanSyouhinMeiDispOld.Value = Me.TextKasanSyouhinMei.Value
            '工務店請求加算金額DispOld
            Me.HiddenKoumutenSeikyuKasanKingakuDispOld.Value = Me.TextKoumutenSeikyuKasanKingaku.Value
            '実請求加算金額DispOld
            Me.HiddenJituSeikyuKasanKingakuDispOld.Value = Me.TextJituSeikyuKasanKingaku.Value

            '●トラン(New)値を退避
            '金額加算商品コードNew
            Me.HiddenKasanSyouhinCdNew.Value = strKasanSyouhinCdNew
            '金額加算商品名Old
            Me.HiddenKasanSyouhinMeiNew.Value = strSyouhinMeiNew
            '工務店請求加算金額Old
            Me.HiddenKoumutenSeikyuKasanKingakuNew.Value = Format(intKoumutenKasanGakuNew, EarthConst.FORMAT_KINGAKU_1)
            '実請求加算金額Old
            Me.HiddenJituSeikyuKasanKingakuNew.Value = Format(intUriKasanGakuNew, EarthConst.FORMAT_KINGAKU_1)

            '●トラン(Old)値を退避
            '金額加算商品コードOld
            Me.HiddenKasanSyouhinCdOld.Value = strKasanSyouhinCdOld
            '金額加算商品名Old
            Me.HiddenKasanSyouhinMeiOld.Value = strSyouhinMeiOld
            '工務店請求加算金額Old
            Me.HiddenKoumutenSeikyuKasanKingakuOld.Value = Format(intKoumutenKasanGakuOld, EarthConst.FORMAT_KINGAKU_1)
            '実請求加算金額Old
            Me.HiddenJituSeikyuKasanKingakuOld.Value = Format(intUriKasanGakuOld, EarthConst.FORMAT_KINGAKU_1)

            '●マスタ値を退避
            '特別対応チェックボックス
            Me.HiddenTokubetuTaiouCheckedMst.Value = IIf(blnCheckedMst = True, EarthConst.ARI_VAL, EarthConst.NASI_VAL)
            '金額加算商品コードOld
            Me.HiddenKasanSyouhinCdMst.Value = strKasanSyouhinCdMst
            '金額加算商品名Old
            Me.HiddenKasanSyouhinMeiMst.Value = strSyouhinMeiMst
            '工務店請求加算金額Old
            Me.HiddenKoumutenSeikyuKasanKingakuMst.Value = Format(intKoumutenKasanGakuMst, EarthConst.FORMAT_KINGAKU_1)
            '実請求加算金額Old
            Me.HiddenJituSeikyuKasanKingakuMst.Value = Format(intUriKasanGakuMst, EarthConst.FORMAT_KINGAKU_1)

            '表示切替用
            If dtRec.DispHantei Then
                Me.HiddenDisp.Value = EarthConst.ARI_VAL
            Else
                Me.HiddenDisp.Value = EarthConst.NASI_VAL
            End If

            '更新日時
            Me.HiddenUpdDatetime.Value = IIf(dtRec.UpdDatetime = Date.MinValue, "", Format(dtRec.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
        End If

        '******************************************
        '* 表示設定
        '******************************************
        Me.SetDispControl()

        Select Case dtRec.PrimaryDisplay
            Case True
                Me.SetDispControlSyouhin(EarthEnum.emTokubetuTaiouActBtn.BtnLoad)
            Case False
                Me.SetDispControlSyouhin(EarthEnum.emTokubetuTaiouActBtn.BtnMaster)
        End Select
    End Sub

    ''' <summary>
    ''' 特別対応レコードから画面表示項目への値セットを行なう(マスタ値を反映)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromHiddenMst(ByVal sender As Object _
                                    , ByVal e As System.EventArgs _
                                    , ByRef dtRec As TokubetuTaiouRecordBase _
                                    )

        '******************************************
        '* 表示設定
        '******************************************
        Me.SetDispControlSyouhin(EarthEnum.emTokubetuTaiouActBtn.BtnMaster)

        Dim blnCheckedMst As Boolean = False
        Dim strKasanSyouhinCdMst As String = String.Empty
        Dim strSyouhinMeiMst As String = String.Empty
        Dim intUriKasanGakuMst As Integer = Integer.MinValue
        Dim intKoumutenKasanGakuMst As Integer = Integer.MinValue

        '加盟店マスタ情報で上書き
        blnCheckedMst = IIf(Me.HiddenTokubetuTaiouCheckedMst.Value = "1", True, False)
        strKasanSyouhinCdMst = cl.GetDisplayString(Me.HiddenKasanSyouhinCdMst.Value)
        strSyouhinMeiMst = cl.GetDisplayString(Me.HiddenKasanSyouhinMeiMst.Value)
        intUriKasanGakuMst = cl.GetDispNum(Me.HiddenJituSeikyuKasanKingakuMst.Value)
        intKoumutenKasanGakuMst = cl.GetDispNum(Me.HiddenKoumutenSeikyuKasanKingakuMst.Value)

        '特別対応コード
        Me.HiddenTokubetuTaiouCd.Value = Me.HiddenTokubetuTaiouCd.Value
        'チェック状態
        Me.CheckBoxTokubetuTaiou.Checked = blnCheckedMst
        '金額加算商品コード
        Me.TextKasanSyouhinCd.Value = strKasanSyouhinCdMst
        '金額加算商品名
        Me.TextKasanSyouhinMei.Value = strSyouhinMeiMst
        '工務店請求加算金額
        Me.TextKoumutenSeikyuKasanKingaku.Value = Format(intKoumutenKasanGakuMst, EarthConst.FORMAT_KINGAKU_1)
        '実請求加算金額
        Me.TextJituSeikyuKasanKingaku.Value = Format(intUriKasanGakuMst, EarthConst.FORMAT_KINGAKU_1)

        '青字太字の場合、設定先を保持しておく。以外はクリアする。
        If Me.HiddenSetteiSakiStyle.Value <> EarthConst.STYLE_COLOR_BLUE Then
            '分類コード
            Me.HiddenBunruiCd.Value = String.Empty
            '画面表示NO
            Me.HiddenGamenHyoujiNo.Value = "0"
        End If

        '******************************************
        '* 表示設定
        '******************************************
        Me.SetDispControl()

    End Sub

    ''' <summary>
    ''' 画面の各明細行情報をレコードクラスに取得し、特別対応レコードクラスを返却する
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetRowCtrlToDataRec(ByRef dataRec As TokubetuTaiouRecordBase)
        Dim ctrl As New TokubetuTaiouRecordCtrl

        '***************************************
        ' 特別対応データ
        '***************************************
        '特別対応コード
        cl.SetDisplayString(Me.HiddenTokubetuTaiouCd.Value, dataRec.TokubetuTaiouCd)
        '特別対応名
        cl.SetDisplayString(Me.HiddenTokubetuTaiouMei.Value, dataRec.TokubetuTaiouMeisyou)

        'チェック有無
        dataRec.CheckJyky = Me.CheckBoxTokubetuTaiou.Checked
        'チェック有無Old
        dataRec.CheckJykyOld = IIf(Me.HiddenChkJykyOld.Value = "1", True, False)

        '●画面値をセット(マスタ再取得ボタン押下により変更される可能性があるので、Old値退避は後続のロジックにて処理を行なう)
        '金額加算商品コード
        cl.SetDisplayString(Me.TextKasanSyouhinCd.Value, dataRec.KasanSyouhinCd)
        '工務店請求加算金額
        cl.SetDisplayString(Me.TextKoumutenSeikyuKasanKingaku.Value, dataRec.KoumutenKasanGaku)
        '実請求加算金額
        cl.SetDisplayString(Me.TextJituSeikyuKasanKingaku.Value, dataRec.UriKasanGaku)
        '金額加算商品コードOld
        cl.SetDisplayString(Me.HiddenKasanSyouhinCdOld.Value, dataRec.KasanSyouhinCdOld)
        '工務店請求加算金額Old
        cl.SetDisplayString(Me.HiddenKoumutenSeikyuKasanKingakuOld.Value, dataRec.KoumutenKasanGakuOld)
        '実請求加算金額Old
        cl.SetDisplayString(Me.HiddenJituSeikyuKasanKingakuOld.Value, dataRec.UriKasanGakuOld)

        '青字太字あるいはチェックボックスチェック状態の場合、設定先を保持しておく。以外はクリアする。
        If Me.HiddenSetteiSakiStyle.Value <> EarthConst.STYLE_COLOR_BLUE Then
            '分類コード
            Me.HiddenBunruiCd.Value = String.Empty
            '画面表示NO
            Me.HiddenGamenHyoujiNo.Value = "0"
        End If
        '分類コード
        cl.SetDisplayString(Me.HiddenBunruiCd.Value, dataRec.BunruiCd)
        '画面表示NO
        cl.SetDisplayString(Me.HiddenGamenHyoujiNo.Value, dataRec.GamenHyoujiNo)

        '取消
        dataRec.Torikesi = IIf(dataRec.CheckJyky = True, 0, 1)
        '更新フラグ
        cl.SetDisplayString(Me.HiddenUpdFlg.Value, dataRec.UpdFlg)
        dataRec.UpdFlg = IIf(dataRec.HenkouCheck = True, 1, 0)
        '価格処理フラグ
        cl.SetDisplayString(Me.HiddenKkkSyoriFlg.Value, dataRec.KkkSyoriFlg)
        '設定先/スタイル
        cl.SetDisplayString(Me.HiddenSetteiSakiStyle.Value, dataRec.SetteiSakiStyle)

        ' 更新日時 読み込み時のタイムスタンプ
        If Me.HiddenUpdDatetime.Value = "" Then
            dataRec.UpdDatetime = DateTime.MinValue
        Else
            dataRec.UpdDatetime = DateTime.ParseExact(Me.HiddenUpdDatetime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
        End If

        Me.SetDispControl()

    End Sub

    ''' <summary>
    ''' チェックされた行の情報を取得し、特別対応レコードクラスを返却する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetChkRowCtrlToDataRec() As TokubetuTaiouRecordBase
        Dim ctrl As New TokubetuTaiouRecordCtrl
        ' 画面内容より地盤レコードを生成する
        Dim dataRec As New TokubetuTaiouRecordBase

        '***************************************
        ' 特別対応データ
        '***************************************
        '特別対応コード
        cl.SetDisplayString(Me.HiddenTokubetuTaiouCd.Value, dataRec.TokubetuTaiouCd)
        '特別対応名
        cl.SetDisplayString(Me.HiddenTokubetuTaiouMei.Value, dataRec.TokubetuTaiouMeisyou)

        '青字・太字以外は再設定の為、クリアする
        If Me.HiddenSetteiSakiStyle.Value <> EarthConst.STYLE_COLOR_BLUE Then
            '分類コード
            Me.HiddenBunruiCd.Value = String.Empty
            '画面表示NO
            Me.HiddenGamenHyoujiNo.Value = "0"
        End If
        '分類コード
        cl.SetDisplayString(Me.HiddenBunruiCd.Value, dataRec.BunruiCd)
        '画面表示NO
        cl.SetDisplayString(Me.HiddenGamenHyoujiNo.Value, dataRec.GamenHyoujiNo)

        '金額加算商品コード
        cl.SetDisplayString(Me.TextKasanSyouhinCd.Value, dataRec.KasanSyouhinCd)
        '工務店請求加算金額
        cl.SetDisplayString(Me.TextKoumutenSeikyuKasanKingaku.Value, dataRec.KoumutenKasanGaku)
        '実請求加算金額
        cl.SetDisplayString(Me.TextJituSeikyuKasanKingaku.Value, dataRec.UriKasanGaku)
        '価格処理フラグ
        cl.SetDisplayString(Me.HiddenKkkSyoriFlg.Value, dataRec.KkkSyoriFlg)
        'チェック状況
        dataRec.CheckJyky = Me.CheckBoxTokubetuTaiou.Checked
        'チェック状況(Old値)
        dataRec.CheckJykyOld = IIf(Me.HiddenChkJykyOld.Value = "1", True, False)
        '更新フラグ
        dataRec.UpdFlg = IIf(Me.HiddenUpdFlg.Value = "1", "1", "0")
        '設定先のスタイル
        cl.SetDisplayString(Me.HiddenSetteiSakiStyle.Value, dataRec.SetteiSakiStyle)

        Return dataRec
    End Function

#Region "画面制御"

    ''' <summary>
    ''' 明細行の表示設定を行う。
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDispControl()
        Dim intGamenHyoujiNo As Integer
        Dim strSettei As String

        '●表示切替
        If Me.HiddenDisp.Value = EarthConst.ARI_VAL Then
            Me.Tr1.Style(EarthConst.STYLE_DISPLAY) = EarthConst.STYLE_DISPLAY_INLINE
        Else
            Me.Tr1.Style(EarthConst.STYLE_DISPLAY) = EarthConst.STYLE_DISPLAY_NONE
        End If

        '●tabindexの付与
        Me.CheckBoxTokubetuTaiou.Attributes(EarthConst.STYLE_TAB_INDEX) = CInt(Me.HiddenTokubetuTaiouCd.Value) + 1

        '●表示スタイル変更/設定先：デフォルト赤字太字
        Me.HiddenSetteiSakiStyle.Value = EarthConst.STYLE_COLOR_RED
        '設定先がセットされている場合、青字太字
        If Me.HiddenBunruiCd.Value <> String.Empty AndAlso (Me.HiddenGamenHyoujiNo.Value <> String.Empty AndAlso Me.HiddenGamenHyoujiNo.Value <> "0") Then
            Me.HiddenSetteiSakiStyle.Value = EarthConst.STYLE_COLOR_BLUE '青字太字
        End If

        Select Case Me.HiddenSetteiSakiStyle.Value
            Case EarthConst.STYLE_COLOR_BLUE
                cl.setStyleBlueBold(Me.AccTextSetteiSaki.Style, True)
            Case EarthConst.STYLE_COLOR_RED
                cl.setStyleRedBold(Me.AccTextSetteiSaki.Style, True)
        End Select
        '設定先
        cl.SetDisplayString(Me.HiddenGamenHyoujiNo.Value, intGamenHyoujiNo)
        strSettei = cbLogic.DevideTokubetuCd(Me, Me.HiddenBunruiCd.Value, intGamenHyoujiNo)
        Me.AccTextSetteiSaki.Value = IIf(strSettei <> String.Empty, "商品" & strSettei, String.Empty)

    End Sub

    ''' <summary>
    ''' 明細行の表示設定を行う。
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDispControlSyouhin(Optional ByVal intBtnFlg As EarthEnum.emTokubetuTaiouActBtn = EarthEnum.emTokubetuTaiouActBtn.BtnLoad)

        '●表示スタイル変更/その他：差異がある場合、赤字太字
        Select Case intBtnFlg
            Case EarthEnum.emTokubetuTaiouActBtn.BtnMaster
                '金額加算商品コードMst
                If Me.TextKasanSyouhinCd.Value = Me.HiddenKasanSyouhinCdMst.Value Then
                    cl.setStyleRedBold(Me.TextKasanSyouhinCd.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextKasanSyouhinCd.Style, True)
                End If
                '金額加算商品名Mst
                If Me.TextKasanSyouhinMei.Value = Me.HiddenKasanSyouhinMeiMst.Value Then
                    cl.setStyleRedBold(Me.TextKasanSyouhinMei.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextKasanSyouhinMei.Style, True)
                End If
                '工務店請求加算金額Mst
                If Me.TextKoumutenSeikyuKasanKingaku.Value = Me.HiddenKoumutenSeikyuKasanKingakuMst.Value Then
                    cl.setStyleRedBold(Me.TextKoumutenSeikyuKasanKingaku.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextKoumutenSeikyuKasanKingaku.Style, True)
                End If
                '実請求加算金額Mst
                If Me.TextJituSeikyuKasanKingaku.Value = Me.HiddenJituSeikyuKasanKingakuMst.Value Then
                    cl.setStyleRedBold(Me.TextJituSeikyuKasanKingaku.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextJituSeikyuKasanKingaku.Style, True)
                End If

            Case EarthEnum.emTokubetuTaiouActBtn.BtnLoad
                '金額加算商品コードOld
                If Me.TextKasanSyouhinCd.Value = Me.HiddenKasanSyouhinCdOld.Value Then
                    cl.setStyleRedBold(Me.TextKasanSyouhinCd.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextKasanSyouhinCd.Style, True)
                End If
                '金額加算商品名Old
                If Me.TextKasanSyouhinMei.Value = Me.HiddenKasanSyouhinMeiOld.Value Then
                    cl.setStyleRedBold(Me.TextKasanSyouhinMei.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextKasanSyouhinMei.Style, True)
                End If
                '工務店請求加算金額Old
                If Me.TextKoumutenSeikyuKasanKingaku.Value = Me.HiddenKoumutenSeikyuKasanKingakuOld.Value Then
                    cl.setStyleRedBold(Me.TextKoumutenSeikyuKasanKingaku.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextKoumutenSeikyuKasanKingaku.Style, True)
                End If
                '実請求加算金額Old
                If Me.TextJituSeikyuKasanKingaku.Value = Me.HiddenJituSeikyuKasanKingakuOld.Value Then
                    cl.setStyleRedBold(Me.TextJituSeikyuKasanKingaku.Style, False)
                Else
                    cl.setStyleRedBold(Me.TextJituSeikyuKasanKingaku.Style, True)
                End If

            Case Else
        End Select

    End Sub

#End Region

#End Region

    ''' <summary>
    ''' 特別対応チェックボックス/チェックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonCheckBoxTokubetuTaiou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonChkTokubetuTaiou.ServerClick

        '親画面イベント呼出
        RaiseEvent SetCheckTokubetuTaiouChangeAction(sender, e, Me.CheckBoxTokubetuTaiou.ClientID)

    End Sub
End Class