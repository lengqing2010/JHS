Partial Public Class HannyouUriageSyuusei
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Private cl As New CommonLogic
    ''' <summary>
    ''' メッセージクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private MLogic As New MessageLogic
    ''' <summary>
    ''' ビジネスロジック共通クラス
    ''' </summary>
    ''' <remarks></remarks>
    Private cbLogic As New CommonBizLogic
    Private sl As New StringLogic
    '地盤セッション管理クラス
    Private jSM As New JibanSessionManager

#Region "画面固有コントロール値"
    'タイトル
    Private Const CTRL_VALUE_TITLE As String = "汎用売上データ"
    Private Const CTRL_VALUE_TITLE_KOUSIN As String = CTRL_VALUE_TITLE & EarthConst.GAMEN_MODE_EDIT
    Private Const CTRL_VALUE_TITLE_TOUROKU As String = CTRL_VALUE_TITLE & EarthConst.GAMEN_MODE_NEW
    Private Const CTRL_VALUE_TITLE_KAKUNIN As String = CTRL_VALUE_TITLE & EarthConst.GAMEN_MODE_VIEW
#End Region

#Region "プロパティ"

#Region "パラメータ/汎用売上データ照会画面"

    ''' <summary>
    ''' 汎用売上ユニークNO
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _HanUriUnqNo As String = String.Empty
    ''' <summary>
    ''' 汎用売上ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pStrHanUriNo() As String
        Get
            Return _HanUriUnqNo
        End Get
        Set(ByVal value As String)
            _HanUriUnqNo = value
        End Set
    End Property

    ''' <summary>
    ''' 画面モード
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _GamenMode As String = String.Empty
    ''' <summary>
    ''' 画面モード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrGamenMode() As String
        Get
            Return _GamenMode
        End Get
        Set(ByVal value As String)
            _GamenMode = value
        End Set
    End Property

#End Region

#End Region

#Region "プライベートメソッド"

#Region "初期読込時処理系"

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス
        Dim jSM As New JibanSessionManager

        'マスターページ情報を取得（ScriptManager用）
        masterAjaxSM = Me.SM1

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合
            cl.CloseWindow(Me)
            Me.ButtonUpdate.Style("display") = "none"
            Exit Sub
        End If

        If IsPostBack = False Then '初期起動時

            '●パラメータのチェック
            Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
            ' Key情報を保持
            pStrHanUriNo = arrSearchTerm(0)

            ' パラメータ不足時は閉じる
            If pStrHanUriNo Is Nothing OrElse pStrHanUriNo = String.Empty Then
                cl.CloseWindow(Me)
                Me.ButtonUpdate.Style("display") = "none"
                Exit Sub
            End If

            '●権限のチェック
            '経理業務権限
            If userinfo.KeiriGyoumuKengen = 0 Then
                cl.CloseWindow(Me)
                Me.ButtonUpdate.Style("display") = "none"
                Exit Sub
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            Dim helper As New DropDownHelper

            ' 売上店区分コンボにデータをバインドする
            helper.SetKtMeisyouDropDownList(Me.SelectUriageTenKbn, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)

            ' 請求先区分コンボにデータをバインドする
            helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)

            ' 税区分コンボにデータをバインドする
            helper.SetDropDownList(Me.SelectZeiKbn, DropDownHelper.DropDownType.Syouhizei, True, True)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            Me.SetDispAction()

            'ボタン押下イベントの設定
            Me.setBtnEvent()

            '****************************************************************************
            ' 汎用売上データ取得
            '****************************************************************************
            Me.SetCtrlFromDataRec(sender, e)

            '画面制御(施主名)
            Me.setSesyumeiControl(sender, e)

            Me.setFocusAJ(Me.ButtonClose) 'フォーカス

        Else
            '画面設定
            Me.SetDispControl()

        End If

    End Sub

    ''' <summary>
    ''' 抽出レコードクラスから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dataRec As New HannyouUriageRecord
        Dim logic As New HannyouUriageLogic

        Dim strDenpyouUriageDate As String = String.Empty

        dataRec = logic.getSearchDataRec(sender, pStrHanUriNo)

        '汎用売上NO
        Me.TextHanUriNo.Text = cl.GetDispNum(dataRec.HanUriUnqNo, "")
        '取消
        If cl.GetDisplayString(dataRec.Torikesi) = "0" Then
            Me.CheckTorikesi.Checked = False
        ElseIf cl.GetDisplayString(dataRec.Torikesi) = "1" Then
            Me.CheckTorikesi.Checked = True
        Else
            Me.CheckTorikesi.Checked = False
        End If
        '********
        '売上店
        '********
        '売上店区分
        Me.SelectUriageTenKbn.SelectedValue = String.Empty
        '売上店コード
        Me.TextUriageTenCd.Text = String.Empty
        '売上店名
        Me.TextUriageTenMei.Text = String.Empty
        '********
        '請求先
        '********
        '請求先区分
        Me.SelectSeikyuuSakiKbn.SelectedValue = cl.GetDisplayString(dataRec.SeikyuuSakiKbn)
        '請求先コード
        Me.TextSeikyuuSakiCd.Text = cl.GetDisplayString(dataRec.SeikyuuSakiCd)
        '請求先枝番
        Me.TextSeikyuuSakiBrc.Text = cl.GetDisplayString(dataRec.SeikyuuSakiBrc)
        '請求先名
        Me.MakeLinkSeikyuuSakiMei(dataRec.SeikyuuSakiMei)
        '赤色文字変更対応を配列に格納
        Dim objChgColor As Object() = New Object() {Me.SelectSeikyuuSakiKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc, Me.TextTorikesiRiyuu}
        '加盟店取消情報を表示
        cl.GetKameitenTorikesiRiyuuMain(Me.SelectSeikyuuSakiKbn.SelectedValue, Me.TextSeikyuuSakiCd.Text, Me.TextTorikesiRiyuu, True, False, objChgColor)
        '商品コード
        Me.TextSyouhinCd.Text = cl.GetDisplayString(dataRec.SyouhinCd)
        '商品名
        Me.TextSyouhinMei.Text = cl.GetDisplayString(dataRec.Hinmei)
        '売上処理済
        If cl.GetDisplayString(dataRec.UriKeijyouDate) = String.Empty Then
            Me.SpanUriageSyoriZumi.InnerHtml = String.Empty
        Else
            Me.SpanUriageSyoriZumi.InnerHtml = EarthConst.URIAGE_ZUMI
        End If
        '単価
        Me.TextTanka.Text = IIf(dataRec.Tanka = Integer.MinValue, _
                                            "", _
                                            dataRec.Tanka.ToString(EarthConst.FORMAT_KINGAKU_1))
        '数量
        Me.TextSuu.Text = IIf(dataRec.Suu = Integer.MinValue, _
                                            "1", _
                                            dataRec.Suu.ToString(EarthConst.FORMAT_KINGAKU_1))
        '税区分
        Me.SelectZeiKbn.SelectedValue = cl.GetDisplayString(dataRec.ZeiKbn)
       
        '消費税額
        Me.TextSyouhiZeiGaku.Text = IIf(dataRec.SyouhiZeiGaku = Integer.MinValue, _
                                            "", _
                                            dataRec.SyouhiZeiGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '売上金額
        Me.TextUriGaku.Text = IIf(dataRec.UriGaku = 0, _
                                            "", _
                                            dataRec.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1))

        '売上年月日
        Me.TextUriDate.Text = cl.GetDisplayString(dataRec.UriDate, Date.Now.ToString("yyyy/MM/dd"))
        '伝票売上年月日
        strDenpyouUriageDate = cl.GetDisplayString(dataRec.DenpyouUriDate)
        Me.TextDenpyouUriDate.Text = strDenpyouUriageDate
        Me.TextDenpyouUriDateDisplay.Text = strDenpyouUriageDate
        '請求年月日
        Me.TextSeikyuuDate.Text = cl.GetDisplayString(dataRec.SeikyuuDate)
        '区分
        Me.TextKbn.Text = cl.GetDisplayString(dataRec.Kbn)
        '番号
        Me.TextHosyousyoNo.Text = cl.GetDisplayString(dataRec.Bangou)
        '施主名
        Me.TextSesyumei.Text = cl.GetDisplayString(dataRec.SesyuMei)
        '摘要
        Me.TextAreaTekiyou.Value = cl.GetDisplayString(dataRec.Tekiyou)

        '********
        '売上店区分、コード、店名
        '********
        '売上店区分
        Me.SelectUriageTenKbn.SelectedValue = cl.GetDisplayString(dataRec.UriageTenKbn)
        '売上店コード
        Me.TextUriageTenCd.Text = cl.GetDisplayString(dataRec.UriageTenCd)
        '売上店名
        TextUriageTen_ChangeSub(sender, e)

        '************
        '* Hidden項目
        '************
        '更新日時
        Me.HiddenUpdDatetime.Value = IIf(dataRec.UpdDatetime = Date.MinValue, "", Format(dataRec.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
        '請求先コード
        Me.HiddenSeikyuuSakiCdOld.Value = Me.TextSeikyuuSakiCd.Text
        '請求先枝番
        Me.HiddenSeikyuuSakiBrcOld.Value = Me.TextSeikyuuSakiBrc.Text
        '請求先区分
        Me.HiddenSeikyuuSakiKbnOld.Value = Me.SelectSeikyuuSakiKbn.SelectedValue
        '商品コード
        Me.HiddenSyouhinCdOld.Value = Me.TextSyouhinCd.Text
        '税率
        Me.HiddenZeiritu.Value = cbLogic.getSyouhiZeiritu(Me.SelectZeiKbn.SelectedValue, False)
        '区分
        Me.HiddenKubun.Value = Me.TextKbn.Text
        '番号
        Me.HiddenBangou.Value = Me.TextHosyousyoNo.Text

        '画面表示時点の値を、Hiddenに保持(変更チェック用)
        If Me.HiddenOpenValues.Value = String.Empty Then
            Me.HiddenOpenValues.Value = Me.getCtrlValuesString()
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 商品マスタポップアップ画面の分類指定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.HiddenTargetId.Value = EarthEnum.EnumSyouhinKubun.AllSyouhin '全商品

        '画面モード別設定
        Me.SetGamenMode(dataRec)

        '画面制御
        Me.SetEnableControl()

        '伝票売上年月日の自動設定(新規モード、初期読込時のみ)
        If pStrGamenMode = CStr(EarthEnum.emGamenMode.SINKI) Then
            If Me.TextUriDate.Text <> String.Empty And Me.TextDenpyouUriDate.Text = String.Empty Then
                Me.TextDenpyouUriDate.Text = Me.TextUriDate.Text
            End If
        End If

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispAction()
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript関連セット
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim onFocusPostBackScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScript As String = "if(checkTempValueForOnBlur(this)){__doPostBack(this.id,'');}"
        Dim onFocusPostBackScriptKingaku As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScriptKingaku As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this)){__doPostBack(this.id,'');}}else{checkNumberAddFig(this);}"
        Dim onFocusPostBackScriptDate As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScriptDate As String = "if(checkTempValueForOnBlur(this)){if(checkDate(this)){__doPostBack(this.id,'');}}else{checkDate(this);}"
        Dim onFocusScript As String = "removeFig(this);"
        Dim onBlurScript As String = "checkNumberAddFig(this);"
        Dim checkDate As String = "checkDate(this);"
        Dim checkKingaku As String = "checkKingaku(this);"
        Dim checkNumber As String = "checkNumber(this);"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"
        Dim onBlurHankakuEisuu As String = "hankakuEisuu(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 請求先コード、請求先枝番および請求先区分
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '売上店
        Me.TextUriageTenCd.Attributes("onfocus") = onFocusPostBackScript
        Me.TextUriageTenCd.Attributes("onblur") = onBlurPostBackScript

        '請求先
        Me.TextSeikyuuSakiCd.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc.Attributes("onblur") = onBlurPostBackScript

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 金額系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '単価
        Me.TextTanka.Attributes("onfocus") = onFocusPostBackScriptKingaku
        Me.TextTanka.Attributes("onblur") = onBlurPostBackScriptKingaku
        Me.TextTanka.Attributes("onkeydown") = disabledOnkeydown

        '消費税額
        Me.TextSyouhiZeiGaku.Attributes("onfocus") = onFocusPostBackScriptKingaku
        Me.TextSyouhiZeiGaku.Attributes("onblur") = onBlurPostBackScriptKingaku
        Me.TextSyouhiZeiGaku.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 日付系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '売上年月日
        Me.TextUriDate.Attributes("onfocus") = onFocusPostBackScriptDate
        Me.TextUriDate.Attributes("onblur") = onBlurPostBackScriptDate
        Me.TextUriDate.Attributes("onkeydown") = disabledOnkeydown

        '伝票売上年月日
        Me.TextDenpyouUriDate.Attributes("onfocus") = onFocusPostBackScriptDate
        Me.TextDenpyouUriDate.Attributes("onblur") = onBlurPostBackScriptDate
        Me.TextDenpyouUriDate.Attributes("onkeydown") = disabledOnkeydown

        '請求年月日
        Me.TextSeikyuuDate.Attributes("onblur") = checkDate

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 数値系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '数量
        Me.TextSuu.Attributes("onfocus") = onFocusPostBackScriptKingaku
        Me.TextSuu.Attributes("onblur") = onBlurPostBackScriptKingaku
        Me.TextSuu.Attributes("onkeydown") = disabledOnkeydown

        '区分
        Me.TextKbn.Attributes("onblur") = onBlurHankakuEisuu
        '番号
        Me.TextHosyousyoNo.Attributes("onblur") = onBlurHankakuEisuu

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing)

    End Sub

    ''' <summary>
    ''' 登録/修正実行ボタンの設定
    ''' </summary>
    ''' <remarks>
    ''' DB更新の確認を行なう。<br/>
    ''' OK時：DB更新を行なう。<br/>
    ''' キャンセル時：DB更新を中断する。<br/>
    ''' </remarks>
    Private Sub setBtnEvent()

        'イベントハンドラ登録
        Dim tmpScriptOverLay As String = "setWindowOverlay(this,null,1);"
        Dim tmpTouroku As String = "if(confirm('" & Messages.MSG017C & "')){" & tmpScriptOverLay & "}else{return false;}"

        '登録許可MSG確認後、OKの場合DB更新処理を行なう
        Me.ButtonUpdate.Attributes("onclick") = tmpTouroku

    End Sub

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
    ''' 画面モードを設定する
    ''' </summary>
    ''' <param name="dataRec">物件履歴レコード</param>
    ''' <remarks></remarks>
    Private Sub SetGamenMode(ByVal dataRec As HannyouUriageRecord)

        '売上計上日
        If cl.GetDisplayString(dataRec.UriKeijyouDate) <> String.Empty AndAlso userinfo.KeiriGyoumuKengen = 0 Then
            '売上処理済でかつ、経理業務権限が無い場合、確認モードで開く
            pStrGamenMode = CStr(EarthEnum.emGamenMode.KAKUNIN)
        ElseIf userinfo.KeiriGyoumuKengen = -1 Then
            '経理業務権限を保持している場合、更新モードか新規モードで開く判断を行う
            If cl.GetDisplayString(dataRec.HanUriUnqNo) <> String.Empty AndAlso dataRec.HanUriUnqNo > 0 Then
                '汎用売上ユニークNOが設定済みの場合、更新モードで開く
                pStrGamenMode = CStr(EarthEnum.emGamenMode.KOUSIN)
            Else
                '汎用売上ユニークNOが未設定の場合、新規モードで開く
                pStrGamenMode = CStr(EarthEnum.emGamenMode.SINKI)
            End If
        Else
            '経理業務権限が無い場合、確認モードで開く
            '(経理業務権限を持たない場合には画面自体を開けないが、保険として判断を行う)
            pStrGamenMode = CStr(EarthEnum.emGamenMode.KAKUNIN)
        End If

        'Hiddenに退避
        Me.HiddenGamenMode.Value = pStrGamenMode

        '画面設定
        Me.SetDispControl()

    End Sub

    ''' <summary>
    ''' 画面モード別の画面設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispControl()
        'Hiddenより再設定
        pStrGamenMode = Me.HiddenGamenMode.Value

        If pStrGamenMode = CStr(EarthEnum.emGamenMode.KAKUNIN) Then '確認
            'タイトル
            Me.SpanTitle.InnerHtml = CTRL_VALUE_TITLE_KAKUNIN
            'ウィンドウタイトルバー
            Me.Title = EarthConst.SYS_NAME & EarthConst.BRANK_STRING & CTRL_VALUE_TITLE_KAKUNIN
            '更新ボタン
            Me.ButtonUpdate.Style("display") = "none"

        ElseIf pStrGamenMode = CStr(EarthEnum.emGamenMode.SINKI) Then '新規登録
            'タイトル
            Me.SpanTitle.InnerHtml = CTRL_VALUE_TITLE_TOUROKU
            'ウィンドウタイトルバー
            Me.Title = EarthConst.SYS_NAME & EarthConst.BRANK_STRING & CTRL_VALUE_TITLE_TOUROKU
            '汎用売上NO
            pStrHanUriNo = String.Empty '値クリア
            '更新ボタン
            Me.ButtonUpdate.Style("display") = "inline"
            Me.ButtonUpdate.Value = EarthConst.GAMEN_MODE_NEW

        ElseIf pStrGamenMode = CStr(EarthEnum.emGamenMode.KOUSIN) Then '修正実行
            'タイトル
            Me.SpanTitle.InnerHtml = CTRL_VALUE_TITLE_KOUSIN
            'ウィンドウタイトルバー
            Me.Title = EarthConst.SYS_NAME & EarthConst.BRANK_STRING & CTRL_VALUE_TITLE_KOUSIN
            '更新ボタン
            Me.ButtonUpdate.Style("display") = "inline"
            Me.ButtonUpdate.Value = EarthConst.GAMEN_MODE_EDIT
        Else
            cl.CloseWindow(Me)
            Me.ButtonUpdate.Style("display") = "none"
            Exit Sub
        End If
    End Sub

#End Region

#Region "DB更新処理系"

    ''' <summary>
    ''' 画面の編集状況を確認
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkInputEditJyky(ByRef errMess As String, ByRef arrFocusTargetCtrl As ArrayList)

        '月次処理確定年月
        Dim dtGetujiKakuteiLastSyoriDate As DateTime = DateTime.MinValue

        'DB読み込み時点の値を、現在画面の値と比較(更新時の変更有無チェック)
        If Me.TextHanUriNo.Text <> String.Empty AndAlso Me.HiddenOpenValues.Value <> String.Empty Then
            'DB読み込み時点の値が空の場合は比較対象外
            '比較実施
            If Me.HiddenOpenValues.Value <> Me.getCtrlValuesString() Then
                '月次確定予約済みの処理年月「以前」の日付で伝票売上年月日を設定するのはエラー
                If cl.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouUriDate.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                    errMess += String.Format(Messages.MSG176E, dtGetujiKakuteiLastSyoriDate.Month, "伝票売上年月日", dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                    arrFocusTargetCtrl.Add(Me.TextDenpyouUriDate)
                End If
            End If
        End If

        '新規登録時のチェック
        If Me.TextHanUriNo.Text = String.Empty Then
            '月次確定予約済みの処理年月「以前」の日付で伝票売上年月日を設定するのはエラー
            If cl.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouUriDate.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                errMess += String.Format(Messages.MSG176E, dtGetujiKakuteiLastSyoriDate.Month, "伝票売上年月日", dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                arrFocusTargetCtrl.Add(Me.TextDenpyouUriDate)
            End If
        End If



    End Sub

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <remarks>
    ''' コード入力値変更チェック<br/>
    ''' 必須チェック<br/>
    ''' 禁則文字チェック(文字列入力フィールドが対象)<br/>
    ''' バイト数チェック(文字列入力フィールドが対象)<br/>
    ''' 桁数チェック<br/>
    ''' 日付チェック<br/>
    ''' その他チェック<br/>
    ''' </remarks>
    Public Function checkInput() As Boolean
        '地盤画面共通クラス
        Dim jBn As New Jiban

        '全てのコントロールを有効化(破棄種別チェック用)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        'エラーメッセージ初期化
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList

        Dim blnMasterChk As Boolean = True

        '編集状況をチェック
        Me.checkInputEditJyky(errMess, arrFocusTargetCtrl)

        '●コード入力値変更チェック
        '請求先コード、請求先枝番、請求先区分
        If Me.SelectSeikyuuSakiKbn.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld.Value _
            Or Me.TextSeikyuuSakiCd.Text <> Me.HiddenSeikyuuSakiCdOld.Value _
            Or Me.TextSeikyuuSakiBrc.Text <> Me.HiddenSeikyuuSakiBrcOld.Value Then

            errMess += Messages.MSG030E.Replace("@PARAM1", "請求先")
            arrFocusTargetCtrl.Add(Me.ButtonSearchSeikyuuSaki)

            blnMasterChk = False 'フラグをたてる
        End If
        '商品コード
        If Me.TextSyouhinCd.Text <> Me.HiddenSyouhinCdOld.Value Then
            errMess += Messages.MSG030E.Replace("@PARAM1", "商品コード")
            arrFocusTargetCtrl.Add(Me.ButtonSearchSyouhin)
        End If

        '●必須チェック
        '商品コード
        If Me.TextSyouhinCd.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "商品コード")
            arrFocusTargetCtrl.Add(Me.TextSyouhinCd)
        End If
        '商品名
        If Me.TextSyouhinMei.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "商品名")
            arrFocusTargetCtrl.Add(Me.TextSyouhinMei)
        End If
        '請求先区分
        If Me.SelectSeikyuuSakiKbn.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "請求先区分")
            arrFocusTargetCtrl.Add(Me.SelectSeikyuuSakiKbn)
        End If
        '請求先コード
        If Me.TextSeikyuuSakiCd.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "請求先コード")
            arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiCd)
        End If
        '請求先枝番
        If Me.TextSeikyuuSakiBrc.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "請求先枝番")
            arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiBrc)
        End If
        '単価
        If Me.TextTanka.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "単価")
            arrFocusTargetCtrl.Add(Me.TextTanka)
        End If
        '数量
        If Me.TextSuu.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "数量")
            arrFocusTargetCtrl.Add(Me.TextSuu)
        End If
        '消費税額
        If Me.TextSyouhiZeiGaku.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "消費税額")
            arrFocusTargetCtrl.Add(Me.TextSyouhiZeiGaku)
        End If
        '税区分
        If Me.SelectZeiKbn.SelectedValue = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "税区分")
            arrFocusTargetCtrl.Add(Me.SelectZeiKbn)
        End If
        '売上年月日
        If Me.TextUriDate.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "売上年月日")
            arrFocusTargetCtrl.Add(Me.TextUriDate)
        End If
        '請求年月日
        If Me.TextSeikyuuDate.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "請求年月日")
            arrFocusTargetCtrl.Add(Me.TextSeikyuuDate)
        End If

        '●日付チェック
        '売上年月日
        If Me.TextUriDate.Text <> String.Empty Then
            If cl.checkDateHanni(Me.TextUriDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "売上年月日")
                arrFocusTargetCtrl.Add(Me.TextUriDate)
            End If
        End If
        '伝票売上年月日
        If Me.TextDenpyouUriDate.Text <> String.Empty Then
            If cl.checkDateHanni(Me.TextDenpyouUriDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "伝票売上年月日")
                arrFocusTargetCtrl.Add(Me.TextDenpyouUriDate)
            End If
        End If
        '請求年月日
        If Me.TextSeikyuuDate.Text <> String.Empty Then
            If cl.checkDateHanni(Me.TextSeikyuuDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "請求年月日")
                arrFocusTargetCtrl.Add(Me.TextSeikyuuDate)
            End If
        End If

        '●コード桁数チェック
        'なし

        '●禁則文字チェック(文字列入力フィールドが対象)
        '品名
        If jBn.KinsiStrCheck(Me.TextSyouhinMei.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "品名")
            arrFocusTargetCtrl.Add(Me.TextSyouhinMei)
        End If
        '区分
        If jBn.KinsiStrCheck(Me.TextKbn.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "区分")
            arrFocusTargetCtrl.Add(Me.TextKbn)
        End If
        '番号
        If jBn.KinsiStrCheck(Me.TextHosyousyoNo.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "番号")
            arrFocusTargetCtrl.Add(Me.TextHosyousyoNo)
        End If
        '施主名
        If jBn.KinsiStrCheck(Me.TextSesyumei.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "施主名")
            arrFocusTargetCtrl.Add(Me.TextSesyumei)
        End If
        '摘要
        If jBn.KinsiStrCheck(Me.TextAreaTekiyou.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "摘要")
            arrFocusTargetCtrl.Add(Me.TextAreaTekiyou)
        End If

        '●バイト数チェック(文字列入力フィールドが対象)
        '品名
        If jBn.ByteCheckSJIS(Me.TextSyouhinMei.Text, Me.TextSyouhinMei.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "品名")
            arrFocusTargetCtrl.Add(Me.TextSyouhinMei)
        End If
        '改行変換
        If Me.TextAreaTekiyou.Value <> "" Then
            Me.TextAreaTekiyou.Value = Me.TextAreaTekiyou.Value.Replace(vbCrLf, " ")
        End If
        '区分
        If jBn.ByteCheckSJIS(Me.TextKbn.Text, 1) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "区分")
            arrFocusTargetCtrl.Add(Me.TextKbn)
        End If
        '番号
        If jBn.ByteCheckSJIS(Me.TextHosyousyoNo.Text, 10) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "番号")
            arrFocusTargetCtrl.Add(Me.TextHosyousyoNo)
        End If
        '施主名
        If jBn.ByteCheckSJIS(Me.TextSesyumei.Text, 50) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "施主名")
            arrFocusTargetCtrl.Add(Me.TextSesyumei)
        End If
        '摘要
        If jBn.ByteCheckSJIS(Me.TextAreaTekiyou.Value, 512) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "摘要")
            arrFocusTargetCtrl.Add(Me.TextAreaTekiyou)
        End If

        '●その他チェック
        '数値チェック
        If Me.TextSyouhiZeiGaku.Text <> String.Empty Then
            Dim lngTmp As Long = Long.MinValue
            Dim lngVal As Long = Long.Parse(Integer.MaxValue)

            cl.SetDisplayString(Me.TextSyouhiZeiGaku.Text, lngTmp)
            If lngTmp > lngVal Then
                errMess += Messages.MSG154E
                arrFocusTargetCtrl.Add(Me.TextSyouhiZeiGaku)
            End If
        End If

        '※請求先マスタの存在チェック
        '請求先区分、請求先コード、請求先枝番が入力されている場合
        If Me.SelectSeikyuuSakiKbn.SelectedValue <> String.Empty _
            And Me.TextSeikyuuSakiCd.Text <> String.Empty _
            And Me.TextSeikyuuSakiBrc.Text <> String.Empty Then

            'コード入力値エラー時は以下のチェックは行わない
            If blnMasterChk Then
                Dim udsLogic As New UriageDataSearchLogic
                '検索条件に沿った請求先のレコードをすべて取得します
                Dim list As List(Of SeikyuuSakiInfoRecord)
                '実検索件数格納用
                list = udsLogic.GetSeikyuuSakiInfo(Me.TextSeikyuuSakiCd.Text, _
                                                   Me.TextSeikyuuSakiBrc.Text, _
                                                   Me.SelectSeikyuuSakiKbn.SelectedValue, _
                                                   True _
                                                   )

                ' 検索結果1件の場合
                If list.Count = 1 Then
                Else
                    errMess += Messages.MSG139E _
                            & "[請求先区分]" & Me.SelectSeikyuuSakiKbn.SelectedItem.Text & "\r\n" _
                            & "[請求先コード]" & Me.TextSeikyuuSakiCd.Text & "\r\n" _
                            & "[請求先枝番]" & Me.TextSeikyuuSakiBrc.Text
                    arrFocusTargetCtrl.Add(Me.SelectSeikyuuSakiKbn)
                End If

            End If
        End If

        '未売上の場合でかつ、売上年月日と伝票売上年月日が異なる場合
        If Me.SpanUriageSyoriZumi.InnerHtml <> EarthConst.URIAGE_ZUMI Then
            '伝票売上年月日と比較
            If Me.TextUriDate.Text <> Me.TextDenpyouUriDate.Text Then
                errMess += Messages.MSG144E.Replace("@PARAM1", "伝票売上年月日").Replace("@PARAM2", "売上年月日").Replace("@PARAM3", "更新")
                arrFocusTargetCtrl.Add(Me.TextUriDate)
            End If
        End If

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If errMess <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            MLogic.AlertMessage(Me, errMess)
            Return False
        End If

        'エラー無しの場合、Trueを返す
        Return True
    End Function

    ''' <summary>
    ''' 画面の内容をDBに反映する
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function SaveData() As Boolean
        Dim logic As New HannyouUriageLogic
        Dim dataRec As New HannyouUriageRecord

        '画面からレコードクラスにセット
        dataRec = Me.GetCtrlToDataRec()

        ' データの更新を行います
        If logic.saveData(Me, dataRec) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 画面の各情報をレコードクラスに取得し、DB更新用レコードクラスを返却する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCtrlToDataRec() As HannyouUriageRecord
        Dim dataRec As New HannyouUriageRecord

        '***************************************
        ' 汎用売上データ
        '***************************************
        ' 汎用売上ユニークNO
        cl.SetDisplayString(Me.TextHanUriNo.Text, dataRec.HanUriUnqNo)
        ' 取消
        If Me.CheckTorikesi.Checked Then
            dataRec.Torikesi = 1
        Else
            dataRec.Torikesi = 0
        End If
        ' 摘要名
        cl.SetDisplayString(Me.TextAreaTekiyou.Value, dataRec.Tekiyou)
        ' 売上年月日
        cl.SetDisplayString(Me.TextUriDate.Text, dataRec.UriDate)
        ' 伝票売上年月日
        cl.SetDisplayString(Me.TextDenpyouUriDate.Text, dataRec.DenpyouUriDate)
        ' 請求年月日
        cl.SetDisplayString(Me.TextSeikyuuDate.Text, dataRec.SeikyuuDate)
        ' 請求先区分
        cl.SetDisplayString(Me.SelectSeikyuuSakiKbn.SelectedValue, dataRec.SeikyuuSakiKbn)
        ' 請求先コード
        cl.SetDisplayString(Me.TextSeikyuuSakiCd.Text, dataRec.SeikyuuSakiCd)
        ' 請求先枝番
        cl.SetDisplayString(Me.TextSeikyuuSakiBrc.Text, dataRec.SeikyuuSakiBrc)
        ' 商品コード
        cl.SetDisplayString(Me.TextSyouhinCd.Text, dataRec.SyouhinCd)
        ' 品名
        cl.SetDisplayString(Me.TextSyouhinMei.Text, dataRec.Hinmei)
        ' 数量
        cl.SetDisplayString(Me.TextSuu.Text, dataRec.Suu)
        ' 単価
        cl.SetDisplayString(Me.TextTanka.Text, dataRec.Tanka)
        ' 税区分
        cl.SetDisplayString(Me.SelectZeiKbn.SelectedValue, dataRec.ZeiKbn)
        ' 消費税額
        cl.SetDisplayString(Me.TextSyouhiZeiGaku.Text, dataRec.SyouhiZeiGaku)
        '区分
        Dim strKbn As String = Trim(Me.TextKbn.Text)
        If strKbn <> String.Empty Then
            cl.SetDisplayString(strKbn, dataRec.Kbn)
        Else
            dataRec.Kbn = Nothing
        End If
        '番号
        cl.SetDisplayString(Me.TextHosyousyoNo.Text, dataRec.Bangou)
        '施主名
        cl.SetDisplayString(Me.TextSesyumei.Text, dataRec.SesyuMei)
        ' 売上店区分
        cl.SetDisplayString(Me.SelectUriageTenKbn.SelectedValue, dataRec.UriageTenKbn)
        ' 売上店コード
        cl.SetDisplayString(Me.TextUriageTenCd.Text, dataRec.UriageTenCd)
        ' 売上計上FLG●
        dataRec.UriKeijyouFlg = 1
        ' 売上計上日●
        dataRec.UriKeijyouDate = Date.Now.ToString("yyyy/MM/dd")
        ' 更新者ユーザーID
        dataRec.UpdLoginUserId = userinfo.LoginUserId
        ' 更新者ユーザー名
        dataRec.UpdLoginUserName = cl.SetDispUserNM(userinfo.LoginUserId)
        ' 更新日時 読み込み時のタイムスタンプ
        If Me.HiddenUpdDatetime.Value = "" Then
            dataRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
        Else
            dataRec.UpdDatetime = DateTime.ParseExact(Me.HiddenUpdDatetime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
        End If

        Return dataRec
    End Function

#Region "画面コントロールの値を結合し、文字列化する"
    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesString() As String

        Dim sb As New StringBuilder

        '取消
        sb.Append(Me.CheckTorikesi.Checked.ToString & EarthConst.SEP_STRING)
        '区分
        sb.Append(Me.TextKbn.Text & EarthConst.SEP_STRING)
        '番号
        sb.Append(Me.TextHosyousyoNo.Text & EarthConst.SEP_STRING)
        '売上年月日
        sb.Append(Me.TextUriDate.Text & EarthConst.SEP_STRING)
        '伝票売上年月日
        sb.Append(Me.TextDenpyouUriDate.Text & EarthConst.SEP_STRING)
        '売上処理FLG(売上計上FLG)
        sb.Append(Me.SpanUriageSyoriZumi.InnerHtml & EarthConst.SEP_STRING)
        '請求年月日
        sb.Append(Me.TextSeikyuuDate.Text & EarthConst.SEP_STRING)
        '請求先コード
        sb.Append(Me.TextSeikyuuSakiCd.Text & EarthConst.SEP_STRING)
        '請求先枝番
        sb.Append(Me.TextSeikyuuSakiBrc.Text & EarthConst.SEP_STRING)
        '請求先区分
        sb.Append(Me.SelectSeikyuuSakiKbn.SelectedValue & EarthConst.SEP_STRING)
        '商品コード
        sb.Append(Me.TextSyouhinCd.Text & EarthConst.SEP_STRING)
        '品名
        sb.Append(Me.TextSyouhinMei.Text & EarthConst.SEP_STRING)
        '数量
        sb.Append(Me.TextSuu.Text & EarthConst.SEP_STRING)
        '売上金額
        sb.Append(Me.TextUriGaku.Text & EarthConst.SEP_STRING)
        '消費税額
        sb.Append(Me.TextSyouhiZeiGaku.Text & EarthConst.SEP_STRING)

        Return (sb.ToString)
    End Function
#End Region

#End Region

#Region "金額計算系"

    ''' <summary>
    ''' 金額設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKingaku(Optional ByVal blnZeigaku As Boolean = False)
        ' 単価
        Dim tanka_ctrl As TextBox
        ' 数量
        Dim suu_ctrl As TextBox
        ' 税率
        Dim zeiritu_ctrl As HtmlInputHidden
        ' 売上金額
        Dim urigaku_ctrl As TextBox
        ' 消費税額
        Dim syouhizeigaku_ctrl As TextBox

        tanka_ctrl = Me.TextTanka
        suu_ctrl = Me.TextSuu
        zeiritu_ctrl = Me.HiddenZeiritu
        urigaku_ctrl = Me.TextUriGaku
        syouhizeigaku_ctrl = Me.TextSyouhiZeiGaku

        Dim tanka As Long = 0
        Dim suu As Long = 0
        Dim zeiritu As Decimal = 0
        Dim urigaku As Long = 0
        Dim syouhizeigaku As Long = 0

        '単価、数量、税率が空白の場合、売上金額・消費税額を空白にする
        If tanka_ctrl.Text.Trim = String.Empty _
            OrElse suu_ctrl.Text.Trim = String.Empty _
                OrElse zeiritu_ctrl.Value = String.Empty Then

            urigaku_ctrl.Text = String.Empty            '売上金額
            syouhizeigaku_ctrl.Text = String.Empty      '消費税額

        Else '上記以外の場合

            tanka = cl.Str2Long(tanka_ctrl.Text) '単価
            suu = cl.Str2Long(suu_ctrl.Text) '数量
            cl.SetDisplayString(zeiritu_ctrl.Value, zeiritu) '税率
            zeiritu = IIf(zeiritu = Decimal.MinValue, 0, zeiritu)

            urigaku = suu * tanka '売上金額 = 数量 * 単価

            If blnZeigaku Then '消費税額計算の場合
                If syouhizeigaku_ctrl.Text.Trim <> String.Empty Then
                    syouhizeigaku = cl.Str2Long(syouhizeigaku_ctrl.Text) '画面.消費税額
                End If
                urigaku = urigaku + syouhizeigaku '売上金額 = 売上金額 + 消費税額
            Else
                syouhizeigaku = Fix(urigaku * zeiritu) '消費税額 = 売上金額 * 税率
                urigaku = Fix(urigaku * (1 + zeiritu)) '1+税率
            End If

            '画面表示用にフォーマット
            urigaku_ctrl.Text = Format(urigaku, EarthConst.FORMAT_KINGAKU_1) '売上金額
            syouhizeigaku_ctrl.Text = Format(syouhizeigaku, EarthConst.FORMAT_KINGAKU_1) '消費税額

        End If

    End Sub

    ''' <summary>
    ''' 単価変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextTanka_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '金額計算
        Me.SetKingaku()

        'フォーカスの設定
        Me.setFocusAJ(Me.TextSuu)
    End Sub

    ''' <summary>
    ''' 数量変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSuu_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '金額計算
        Me.SetKingaku()

        'フォーカスの設定
        Me.setFocusAJ(Me.SelectZeiKbn)
    End Sub

    ''' <summary>
    ''' 税区分変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectZeiKbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '税率を取得
        Me.HiddenZeiritu.Value = cbLogic.getSyouhiZeiritu(Me.SelectZeiKbn.SelectedValue, False)

        '金額計算
        Me.SetKingaku()

        'フォーカスの設定
        Me.setFocusAJ(Me.SelectZeiKbn)
    End Sub

    ''' <summary>
    ''' 消費税額変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSyouhiZeiGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '金額計算
        Me.SetKingaku(True)

        'フォーカスの設定
        Me.setFocusAJ(Me.TextUriDate)
    End Sub

#End Region

#Region "画面制御"
    ''' <summary>
    ''' コントロールの画面制御
    ''' </summary>
    ''' <remarks>コントロールの画面制御を仕様書の優先順に記載する</remarks>
    Public Sub SetEnableControl()

        '売上処理済(邸別請求テーブル.売上計上FLG＝1)
        If Me.SpanUriageSyoriZumi.InnerHtml = EarthConst.URIAGE_ZUMI AndAlso userinfo.KeiriGyoumuKengen = 0 Then
            '計上済みでかつ、経理業務権限が無い場合、実行ボタンを非表示にする
            Me.ButtonUpdate.Style("display") = "none"
        Else
            Me.ButtonUpdate.Style("display") = "inline"
        End If

    End Sub

    ''' <summary>
    ''' 施主名の活性化制御
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>区分･番号が共に入力済みの場合、施主名は非活性とする</remarks>
    Public Sub setSesyumeiControl(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable '対象外

        '非活性(ラベル化)
        jSM.Hash2Ctrl(UpdatePanelSesyumei, EarthConst.MODE_VIEW, ht, htNotTarget)

        If Me.TextKbn.Text = String.Empty OrElse Me.TextHosyousyoNo.Text = String.Empty Then
            '活性(テキストボックス)
            cl.chgDispSyouhinText(Me.TextSesyumei)
        End If

        If Me.HiddenSesyuFlg.Value <> String.Empty Then
            '活性化制御後のフォーカス位置を指定
            If Me.HiddenKubun.Value <> Me.TextKbn.Text Then
                '区分変更時 → 番号にフォーカス
                Me.HiddenKubun.Value = Me.TextKbn.Text
                Me.setFocusAJ(Me.TextHosyousyoNo)
            ElseIf Me.HiddenBangou.Value <> Me.TextHosyousyoNo.Text Then
                '番号変更時 → 施主名取得ボタンにフォーカス
                Me.HiddenBangou.Value = Me.TextHosyousyoNo.Text
                Me.setFocusAJ(Me.ButtonSesyumeiSyutoku)
            End If
        End If

        '施主名制御用コントロールフラグ
        Me.HiddenSesyuFlg.Value = "1"

    End Sub

#End Region

#End Region

#Region "ボタンイベント"

    ''' <summary>
    ''' 修正実行/新規登録ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonUpdate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUpdate.ServerClick
        Dim tmpScript As String = ""

        ' 入力チェック
        If Me.checkInput() = False Then Exit Sub

        ' 画面の内容をDBに反映する
        If Me.SaveData() Then '登録成功
            '画面を閉じる
            cl.CloseWindow(Me)

        Else '登録失敗
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", Me.ButtonUpdate.Value) & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonUpdate_ServerClick", tmpScript, True)
            Exit Sub
        End If

    End Sub

#Region "請求先関連"

    ''' <summary>
    ''' 請求先検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSearchSeikyuuSaki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearchSeikyuuSaki.ServerClick
        Dim hdnOldObj() As HtmlInputHidden = {Me.HiddenSeikyuuSakiKbnOld _
                                            , Me.HiddenSeikyuuSakiCdOld _
                                            , Me.HiddenSeikyuuSakiBrcOld}
        Dim blnResult As Boolean

        '請求先検索画面呼出
        blnResult = cl.CallSeikyuuSakiSearchWindow(sender, e, Me _
                                            , Me.SelectSeikyuuSakiKbn, Me.TextSeikyuuSakiCd _
                                            , Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakimeiHidden _
                                            , Me.ButtonSearchSeikyuuSaki _
                                            , hdnOldObj)

        '赤色文字変更対応を配列に格納
        Dim objChgColor As Object() = New Object() {Me.SelectSeikyuuSakiKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc, Me.TextTorikesiRiyuu}
        '取消理由取得設定と色替処理
        cl.GetKameitenTorikesiRiyuuMain(Me.SelectSeikyuuSakiKbn.SelectedValue, Me.TextSeikyuuSakiCd.Text, TextTorikesiRiyuu, True, False, objChgColor)

        If blnResult Then
            '請求先名
            Me.MakeLinkSeikyuuSakiMei(Me.TextSeikyuuSakimeiHidden.Text)
            '請求書発行日
            Me.TextSeikyuuDate.Text = Me.GetSeikyuusyoHakkouDate(Me.TextDenpyouUriDate.Text)
            'フォーカスセット
            Me.setFocusAJ(Me.ButtonSearchSeikyuuSaki)
        Else
            Me.MakeLinkSeikyuuSakiMei("")
        End If

    End Sub

    ''' <summary>
    ''' 請求先区分変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectSeikyuuSakiKbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '請求先名・取消理由クリア処理
        clrSeikyuuSakiInfo()

        'フォーカスの設定
        Me.setFocusAJ(Me.SelectSeikyuuSakiKbn)
    End Sub

    ''' <summary>
    ''' 請求先コード変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSeikyuuSakiCd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '請求先名・取消理由クリア処理
        clrSeikyuuSakiInfo()

        'フォーカスの設定
        Me.setFocusAJ(Me.TextSeikyuuSakiBrc)
    End Sub

    ''' <summary>
    ''' 請求先枝番変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSeikyuuSakiBrc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '請求先名・取消理由クリア処理
        clrSeikyuuSakiInfo()

        'フォーカスの設定
        Me.setFocusAJ(Me.ButtonSearchSeikyuuSaki)
    End Sub

    ''' <summary>
    ''' 請求先関連情報をクリアし、文字スタイルを標準(黒)に戻す
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub clrSeikyuuSakiInfo()

        '請求先名
        Me.MakeLinkSeikyuuSakiMei("")
        '加盟店取消理由
        Me.TextTorikesiRiyuu.Text = String.Empty
        '請求先コード/名称/取消理由の文字色スタイル
        cl.setStyleFontColor(Me.SelectSeikyuuSakiKbn.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextSeikyuuSakiCd.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextSeikyuuSakiBrc.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.SelectSeikyuuSakiKbn.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextTorikesiRiyuu.Style, cl.getKameitenFontColor(0))

    End Sub

    ''' <summary>
    ''' 請求先名をセットし、リンクを作成する
    ''' </summary>
    ''' <param name="strSeikyuuSakiMei"></param>
    ''' <remarks></remarks>
    Protected Sub MakeLinkSeikyuuSakiMei(ByVal strSeikyuuSakiMei As String)
        Dim strTmp As String = cl.GetDisplayString(strSeikyuuSakiMei)

        '請求先名
        If strTmp = String.Empty Then '未入力
            Me.LinkSeikyuuSakiMei.InnerHtml = String.Empty
            'リンク作成
            Me.LinkSeikyuuSakiMei.HRef = String.Empty
        Else '入力
            Me.LinkSeikyuuSakiMei.InnerHtml = strTmp
            'リンク作成 ※請求先Mを呼出
            cl.getSeikyuuSakiMasterPath( _
                                        Me.TextSeikyuuSakiCd.Text _
                                        , Me.TextSeikyuuSakiBrc.Text _
                                        , Me.SelectSeikyuuSakiKbn.Text _
                                        , Me.LinkSeikyuuSakiMei)

        End If


    End Sub

    ''' <summary>
    ''' 請求書発行日の取得
    ''' </summary>
    ''' <returns>請求書発行日</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuusyoHakkouDate(Optional ByVal strDenUriDate As String = "") As String
        Dim strSimeDate As String = String.Empty

        If strDenUriDate = String.Empty Then
            Return String.Empty
        End If

        '請求締日の取得
        strSimeDate = cbLogic.GetSeikyuuSimeDate( _
                                                Me.TextSeikyuuSakiCd.Text _
                                                , Me.TextSeikyuuSakiBrc.Text _
                                                , Me.SelectSeikyuuSakiKbn.SelectedValue _
                                               )

        Dim SeikyuusyoHakkouDate As String = cl.GetDisplayString(cbLogic.CalcSeikyuusyoHakkouDate(strSimeDate, strDenUriDate))

        Return SeikyuusyoHakkouDate
    End Function

    ''' <summary>
    ''' 売上年月日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextUriDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        '売上年月日
        If Me.TextUriDate.Text <> String.Empty Then
            '伝票売上年月日
            If Me.TextDenpyouUriDate.Text.Trim() = String.Empty Then
                '売上年月日をセット
                Me.TextDenpyouUriDate.Text = Me.TextUriDate.Text
                TextDenpyouUriDate_TextChanged(sender, e)
            End If

            '税区分・税率を再取得
            strSyouhinCd = Me.TextSyouhinCd.Text
            If cbLogic.getSyouhiZeirituUriage(Me.TextUriDate.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                Me.SelectZeiKbn.SelectedValue = strZeiKbn
                Me.HiddenZeiritu.Value = strZeiritu
                '金額計算
                Me.SetKingaku()
            End If
        End If

        'フォーカスの設定
        Me.setFocusAJ(Me.TextDenpyouUriDate)
    End Sub

    ''' <summary>
    ''' 伝票売上年月日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextDenpyouUriDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '伝票売上年月日
        If Me.TextDenpyouUriDate.Text <> String.Empty Then
            ' 請求書発行日取得
            Dim strHakDate As String = GetSeikyuusyoHakkouDate(Me.TextDenpyouUriDate.Text)
            Me.TextSeikyuuDate.Text = strHakDate
        Else
            Me.TextSeikyuuDate.Text = String.Empty
        End If

        '請求有無にフォーカス
        Me.setFocusAJ(Me.TextSeikyuuDate)
    End Sub

#End Region

#Region "売上店関連"

#Region "初期化処理"
    ''' <summary>
    ''' 初期化処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearUriageTenInfo()
        '売上店名
        Me.TextUriageTenMei.Text = String.Empty

        '請求先区分ごとにMaxLengthを設定
        If Me.SelectUriageTenKbn.SelectedValue = "0" Then '加盟店
            Me.TextUriageTenCd.MaxLength = 5
        ElseIf Me.SelectUriageTenKbn.SelectedValue = "1" Then '調査会社
            Me.TextUriageTenCd.MaxLength = 7
        ElseIf Me.SelectUriageTenKbn.SelectedValue = "2" Then '営業所
            Me.TextUriageTenCd.MaxLength = 5
        End If

        '画面制御
        Me.SetEnableControl()
    End Sub

#End Region

    ''' <summary>
    ''' 売上店検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSearchUriageTen_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.HiddenUriageSearchType.Value <> "1" Then
            Me.TextUriageTen_ChangeSub(sender, e)
        Else
            'コードonchangeで呼ばれた場合
            Me.TextUriageTen_ChangeSub(sender, e, False)
            Me.HiddenUriageSearchType.Value = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' 請求先の自動設定
    ''' </summary>
    ''' <param name="strSeikyuuSakiKbn">請求先区分</param>
    ''' <param name="strSeikyuuSakiCd">請求先コード</param>
    ''' <param name="strSeikyuuSakiBrc">請求先枝番</param>
    ''' <remarks></remarks>
    Private Sub SetAutoSeikyuuSakiInfo(ByVal strSeikyuuSakiKbn As String, ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String)

        '請求先区分、請求先コード、請求先枝番
        If strSeikyuuSakiKbn <> String.Empty _
            And strSeikyuuSakiCd <> String.Empty _
                And strSeikyuuSakiBrc <> String.Empty Then

            '請求先区分
            Me.SelectSeikyuuSakiKbn.SelectedValue = strSeikyuuSakiKbn
            '請求先コード
            Me.TextSeikyuuSakiCd.Text = strSeikyuuSakiCd
            '請求先枝番
            Me.TextSeikyuuSakiBrc.Text = strSeikyuuSakiBrc

            '請求先情報の設定
            Dim e As New EventArgs
            Me.ButtonSearchSeikyuuSaki_ServerClick(Me, e)

        Else
            Dim errMess As String = Messages.MSG149W.Replace("@PARAM1", "請求先情報")
            'エラーメッセージ表示
            MLogic.AlertMessage(Me, errMess)
        End If

    End Sub

    ''' <summary>
    ''' 売上店区分変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectUriageTenKbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '売上店コード
        Me.TextUriageTenCd.Text = String.Empty

        '初期化処理
        Me.ClearUriageTenInfo()

        'フォーカスの設定
        Me.setFocusAJ(Me.SelectUriageTenKbn)
    End Sub

    ''' <summary>
    ''' 売上店コード変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextUriageTenCd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '初期化処理
        Me.ClearUriageTenInfo()

        'フォーカスの設定
        Me.setFocusAJ(Me.ButtonSearchUriageTen)
    End Sub

    ''' <summary>
    ''' 売上店変更時処理
    ''' ※請求先区分ごとに起動するマスタ検索対象を判定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextUriageTen_ChangeSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal ProcType As Boolean = True)

        Dim errMess As String = String.Empty

        '売上店区分
        If Me.SelectUriageTenKbn.SelectedValue = "0" Then
            '加盟店
            Me.SetKameitenInfoSub(sender, e)

        ElseIf Me.SelectUriageTenKbn.SelectedValue = "1" Then
            '調査会社
            Me.tyousakaisyaSearchSub(sender, e)

        ElseIf Me.SelectUriageTenKbn.SelectedValue = "2" Then
            '営業所(コメント対応)
            'Me.SetEigyousyoInfoSub(sender, e)

        End If

    End Sub

    ''' <summary>
    ''' 加盟店コード検索ボタン押下時の処理(実体)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SetKameitenInfoSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal ProcType As Boolean = True)
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim kLogic As New KameitenSearchLogic
        Dim total_row As Integer = 0
        Dim dataArray As New List(Of KameitenSearchRecord)

        '売上店コード=入力の場合
        If Me.TextUriageTenCd.Text <> String.Empty Then
            '検索を実行
            dataArray = kLogic.GetKameitenSearchResult("" _
                                                        , Me.TextUriageTenCd.Text _
                                                        , True _
                                                        , total_row)

        End If

        If total_row = 1 Then
            '商品情報を画面にセット
            Dim recData As KameitenSearchRecord = dataArray(0)
            '売上店コード
            Me.TextUriageTenCd.Text = cl.GetDisplayString(recData.KameitenCd) '加盟店コード

            '加盟店情報を画面に設定
            Me.SetKameitenDetail(sender, e)

            'フォーカスセット
            Me.setFocusAJ(Me.ButtonSearchUriageTen)

        ElseIf ProcType = True Then
            '売上店名
            Me.TextUriageTenMei.Text = String.Empty
            'ダミー
            Me.HiddenKbn.Value = String.Empty
            Dim tmpFocusScript = "objEBI('" & ButtonSearchUriageTen.ClientID & "').focus();"
            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpScript As String = "callSearch('" & Me.HiddenKbn.ClientID & EarthConst.SEP_STRING & Me.TextUriageTenCd.ClientID & _
                                            "','" & UrlConst.SEARCH_KAMEITEN & _
                                            "','" & Me.TextUriageTenCd.ClientID & EarthConst.SEP_STRING & Me.TextUriageTenMei.ClientID & _
                                            "','" & Me.ButtonSearchUriageTen.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSearch", tmpScript, True)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 加盟店情報の設定（加盟店が確定している状態でCallする）
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub SetKameitenDetail(ByVal sender As Object, ByVal e As System.EventArgs)

        '加盟店検索実行後処理(加盟店詳細情報、ビルダー情報取得)
        Dim logic As New KameitenSearchLogic
        Dim blnTorikesi As Boolean = True '取消
        Dim record As KameitenSearchRecord = logic.GetKameitenSearchResult("", Me.TextUriageTenCd.Text, "", blnTorikesi)
        Dim errMess As String = String.Empty

        If Not record Is Nothing OrElse record.KameitenCd <> String.Empty Then
            Me.TextUriageTenMei.Text = record.KameitenMei1  '売上店名

            '商品コード
            If Me.TextSyouhinCd.Text <> String.Empty Then '入力
                Dim uLogic As New UriageDataSearchLogic
                Dim recKsInfo As List(Of KameitenSeikyuuSakiInfoRecord)
                Dim allCount As Integer = 0

                recKsInfo = uLogic.GetKameitenSeikyuuSakiKey(record.KameitenCd, Me.TextSyouhinCd.Text, blnTorikesi)
                If recKsInfo.Count = 1 Then '該当データが存在する場合、請求先情報をセット
                    '請求先の自動設定
                    Me.SetAutoSeikyuuSakiInfo(recKsInfo(0).SeikyuuSakiKbn, recKsInfo(0).SeikyuuSakiCd, recKsInfo(0).SeikyuuSakiBrc)

                Else
                    errMess = Messages.MSG149W.Replace("@PARAM1", "請求先情報")
                    'エラーメッセージ表示
                    MLogic.AlertMessage(Me, errMess)
                End If

            Else
                errMess = "商品コードが未入力のため、" & Messages.MSG149W.Replace("@PARAM1", "請求先情報")
                'エラーメッセージ表示
                MLogic.AlertMessage(Me, errMess)
            End If

        End If

    End Sub

    ''' <summary>
    ''' 調査会社検索ボタン押下時の処理(実体)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tyousakaisyaSearchSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal ProcType As Boolean = True)
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim tysLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        '売上店コード=入力の場合
        If Me.TextUriageTenCd.Text <> String.Empty Then
            dataArray = tysLogic.GetTyousakaisyaSearchResult(Me.TextUriageTenCd.Text, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            '売上店コード
            Me.TextUriageTenCd.Text = recData.TysKaisyaCd & recData.JigyousyoCd '調査会社コード + 調査会社事業所コード
            '売上店名
            Me.TextUriageTenMei.Text = recData.TysKaisyaMei  '調査会社名

            '請求先の自動設定
            Me.SetAutoSeikyuuSakiInfo(recData.SeikyuuSakiKbn, recData.SeikyuuSakiCd, recData.SeikyuuSakiBrc)

            'フォーカスセット
            setFocusAJ(Me.ButtonSearchUriageTen)

        ElseIf ProcType = True Then
            '売上店名
            Me.TextUriageTenMei.Text = String.Empty
            'ダミー
            Me.HiddenKamentenCd.Value = String.Empty
            Dim tmpFocusScript = "objEBI('" & ButtonSearchUriageTen.ClientID & "').focus();"

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpScript = "callSearch('" & Me.TextUriageTenCd.ClientID & EarthConst.SEP_STRING & Me.HiddenKamentenCd.ClientID & _
                                    "','" & UrlConst.SEARCH_TYOUSAKAISYA & _
                                    "','" & Me.TextUriageTenCd.ClientID & EarthConst.SEP_STRING & Me.TextUriageTenMei.ClientID & _
                                    "','" & ButtonSearchUriageTen.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSearch", tmpScript, True)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 営業所検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SetEigyousyoInfoSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal ProcType As Boolean = True)
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim eigyousyoSearchLogic As New EigyousyoSearchLogic
        Dim dataArray As New List(Of EigyousyoSearchRecord)
        Dim total_count As Integer = 0

        '売上店コード=入力の場合
        If Me.TextUriageTenCd.Text <> String.Empty Then
            dataArray = eigyousyoSearchLogic.GetEigyousyoSearchResult(Me.TextUriageTenCd.Text, _
                                                                        String.Empty, _
                                                                        True, _
                                                                        total_count)
        End If

        If dataArray.Count = 1 Then
            Dim recData As EigyousyoSearchRecord = dataArray(0)
            '売上店コード
            Me.TextUriageTenCd.Text = recData.EigyousyoCd  '営業所コード
            '売上店名
            Me.TextUriageTenMei.Text = recData.EigyousyoMei  '営業所名

            '請求先の自動設定
            Me.SetAutoSeikyuuSakiInfo(recData.SeikyuuSakiKbn, recData.SeikyuuSakiCd, recData.SeikyuuSakiBrc)

            'フォーカスセット
            Me.setFocusAJ(Me.ButtonSearchUriageTen)

        ElseIf ProcType = True Then
            '売上店名
            Me.TextUriageTenMei.Text = String.Empty

            Dim tmpFocusScript = "objEBI('" & ButtonSearchUriageTen.ClientID & "').focus();"

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpScript = "callSearch('" & Me.TextUriageTenCd.ClientID & _
                                    "','" & UrlConst.SEARCH_EIGYOUSYO & _
                                    "','" & Me.TextUriageTenCd.ClientID & EarthConst.SEP_STRING & Me.TextUriageTenMei.ClientID & _
                                    "','" & Me.ButtonSearchUriageTen.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSearch", tmpScript, True)
            Exit Sub
        End If

    End Sub

#End Region

#Region "商品情報系"

    ''' <summary>
    ''' 商品検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSearchSyouhin_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.HiddenSyouhinSearchType.Value <> "1" Then
            Me.TextSyouhinCd_ChangeSub(sender, e)
        Else
            'コードonchangeで呼ばれた場合
            Me.TextSyouhinCd_ChangeSub(sender, e, False)
            Me.HiddenSyouhinSearchType.Value = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' 商品検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSyouhinCd_ChangeSub(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs, _
                                Optional ByVal ProcType As Boolean = True)

        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim sLogic As New SyouhinSearchLogic
        Dim total_row As Integer = 0

        '商品検索を実行
        Dim dataArray As List(Of SyouhinMeisaiRecord) = sLogic.GetSyouhinInfo(TextSyouhinCd.Text, _
                                                                                      "", _
                                                                                      EarthEnum.EnumSyouhinKubun.AllSyouhin, _
                                                                                      total_row)
        If total_row = 1 Then
            '商品情報を画面にセット
            Dim recData As SyouhinMeisaiRecord = dataArray(0)

            Me.TextSyouhinCd.Text = cl.GetDisplayString(recData.SyouhinCd) '商品コード

            'フォーカスセット
            Me.setFocusAJ(Me.ButtonSearchSyouhin)

        ElseIf ProcType = True Then
            '検索ポップアップを起動
            Dim tmpFocusScript = "objEBI('" & ButtonSearchSyouhin.ClientID & "').focus();"
            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpScript = "callSearch('" & Me.TextSyouhinCd.ClientID & EarthConst.SEP_STRING & _
                                        Me.HiddenTargetId.ClientID & _
                                        "','" & UrlConst.SEARCH_SYOUHIN & "','" & _
                                        Me.TextSyouhinCd.ClientID & "','" & _
                                        Me.ButtonSearchSyouhin.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSearch", tmpScript, True)
            Exit Sub

        End If

        '商品情報を画面に設定
        Me.SetSyouhin(sender, e)

    End Sub

    ''' <summary>
    ''' 商品情報の設定（商品コードが確定している状態でCallする）
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub SetSyouhin(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lstSyouhin As List(Of SyouhinMeisaiRecord)
        Dim intCntTotal As Integer = 0

        '商品情報の取得
        Dim slogic As New SyouhinSearchLogic
        Dim recSyouhin As New SyouhinMeisaiRecord

        lstSyouhin = slogic.GetSyouhinInfo(TextSyouhinCd.Text, "", EarthEnum.EnumSyouhinKubun.AllSyouhin, intCntTotal)
        If lstSyouhin.Count > 0 Then
            recSyouhin = lstSyouhin(0)
        Else
            Exit Sub
        End If

        '商品情報をセット
        Me.TextSyouhinCd.Text = cl.GetDisplayString(recSyouhin.SyouhinCd)        '商品コード
        Me.TextSyouhinMei.Text = cl.GetDisplayString(recSyouhin.SyouhinMei)      '商品名(上書き)

        '存在チェック
        If cl.ChkDropDownList(SelectZeiKbn, cl.GetDispNum(recSyouhin.ZeiKbn)) Then
            Me.SelectZeiKbn.SelectedValue = cl.GetDispNum(recSyouhin.ZeiKbn)     '税区分(商品に対応した税区分を自動設定)
            Me.HiddenZeiritu.Value = cbLogic.getSyouhiZeiritu(Me.SelectZeiKbn.SelectedValue, False) '税率
        Else
            Me.SelectZeiKbn.SelectedValue = String.Empty
            Me.HiddenZeiritu.Value = cbLogic.getSyouhiZeiritu(Me.SelectZeiKbn.SelectedValue, False) '税率
        End If

        Me.SetKingaku()

        ' 商品コードを退避
        Me.HiddenSyouhinCdOld.Value = Me.TextSyouhinCd.Text

    End Sub

#End Region

#Region "施主名取得系"

    ''' <summary>
    ''' 施主名取得ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSearchSesyumei_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim jLogic As New JibanLogic
        Dim jRecord As New JibanRecord
        Dim errMess As String = String.Empty

        Dim blnChkResult As Boolean

        '地盤データの存在チェック
        blnChkResult = jLogic.ExistsJibanData(Me.TextKbn.Text, Me.TextHosyousyoNo.Text)

        '地盤データが存在する場合→地盤データ取得、地盤データが存在しない場合→メッセージ表示
        If blnChkResult Then
            jRecord = jLogic.GetJibanData(Me.TextKbn.Text, Me.TextHosyousyoNo.Text)
            Me.TextSesyumei.Text = jRecord.SesyuMei
        Else
            errMess = Messages.MSG020E
            'エラーメッセージ表示
            MLogic.AlertMessage(Me, errMess)
        End If

    End Sub
#End Region

#End Region

End Class