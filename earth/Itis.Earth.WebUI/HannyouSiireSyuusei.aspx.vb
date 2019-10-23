Partial Public Class HannyouSiireSyuusei
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    ''' <summary>
    ''' メッセージクラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic
    ''' <summary>
    ''' ビジネスロジック共通クラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim cbLogic As New CommonBizLogic
    Private sl As New StringLogic
    '地盤セッション管理クラス
    Private jSM As New JibanSessionManager

#Region "画面固有コントロール値"
    'タイトル
    Private Const CTRL_VALUE_TITLE As String = "汎用仕入データ"
    Private Const CTRL_VALUE_TITLE_KOUSIN As String = CTRL_VALUE_TITLE & EarthConst.GAMEN_MODE_EDIT
    Private Const CTRL_VALUE_TITLE_TOUROKU As String = CTRL_VALUE_TITLE & EarthConst.GAMEN_MODE_NEW
    Private Const CTRL_VALUE_TITLE_KAKUNIN As String = CTRL_VALUE_TITLE & EarthConst.GAMEN_MODE_VIEW
#End Region

#Region "プロパティ"

#Region "パラメータ/汎用仕入データ照会画面"

    ''' <summary>
    ''' 汎用仕入ユニークNO
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _HanSiireUnqNo As String = String.Empty
    ''' <summary>
    ''' 汎用仕入ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pStrHanSiireNo() As String
        Get
            Return _HanSiireUnqNo
        End Get
        Set(ByVal value As String)
            _HanSiireUnqNo = value
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

        'スクリプトマネージャーを取得（ScriptManager用）
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
            pStrHanSiireNo = arrSearchTerm(0)

            ' パラメータ不足時は閉じる
            If pStrHanSiireNo Is Nothing OrElse pStrHanSiireNo = String.Empty Then
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

            ' 税区分コンボにデータをバインドする
            helper.SetDropDownList(Me.SelectZeiKbn, DropDownHelper.DropDownType.Syouhizei, True, True)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            Me.SetDispAction()

            'ボタン押下イベントの設定
            Me.setBtnEvent()

            '****************************************************************************
            ' 汎用仕入データ取得
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
        Dim dataRec As New HannyouSiireRecord
        Dim logic As New HannyouSiireLogic

        Dim strDenpyouSiireDate As String = String.Empty

        dataRec = logic.getSearchDataRec(sender, pStrHanSiireNo)

        '汎用仕入NO
        Me.TextHanSiireNo.Text = cl.GetDispNum(dataRec.HanSiireUnqNo, "")
        '取消
        If cl.GetDisplayString(dataRec.Torikesi) = "0" Then
            Me.CheckTorikesi.Checked = False
        ElseIf cl.GetDisplayString(dataRec.Torikesi) = "1" Then
            Me.CheckTorikesi.Checked = True
        Else
            Me.CheckTorikesi.Checked = False
        End If
        '商品コード
        Me.TextSyouhinCd.Text = cl.GetDisplayString(dataRec.SyouhinCd)
        '商品名
        Me.TextSyouhinMei.Text = cl.GetDisplayString(dataRec.Hinmei)
        '加盟店コード
        Me.TextkameitenCd.Text = cl.GetDisplayString(dataRec.KameitenCd)
        '加盟店名
        Me.TextKameitenMei.Text = cl.GetDisplayString(dataRec.KameitenMei)
        '調査会社コード
        Me.TextTysKaisyaCd.Text = cl.GetDisplayString(dataRec.TysKaisyaCd + dataRec.TysKaisyaJigyousyoCd)
        '調査会社名
        Me.TextTysKaisyaMei.Text = cl.GetDisplayString(dataRec.TysKaisyaMei)
        '仕入処理済
        If cl.GetDisplayString(dataRec.SiireKeijyouDate) = String.Empty Then
            Me.SpanSiireSyoriZumi.InnerHtml = String.Empty
        Else
            Me.SpanSiireSyoriZumi.InnerHtml = EarthConst.SIIRE_ZUMI
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
        '税込仕入金額
        Me.TextUriGaku.Text = IIf(dataRec.SiireGaku = 0, _
                                            "", _
                                            dataRec.SiireGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '仕入年月日
        Me.TextSiireDate.Text = cl.GetDisplayString(dataRec.SiireDate, Date.Now.ToString("yyyy/MM/dd"))
        '伝票仕入年月日
        strDenpyouSiireDate = cl.GetDisplayString(dataRec.DenpyouSiireDate)
        Me.TextDenpyouSiireDate.Text = strDenpyouSiireDate
        Me.TextDenpyouSiireDateDisplay.Text = strDenpyouSiireDate
        '区分
        Me.TextKbn.Text = cl.GetDisplayString(dataRec.Kbn)
        '番号
        Me.TextHosyousyoNo.Text = cl.GetDisplayString(dataRec.Bangou)
        '施主名
        Me.TextSesyumei.Text = cl.GetDisplayString(dataRec.SesyuMei)
        '摘要
        Me.TextAreaTekiyou.Value = cl.GetDisplayString(dataRec.Tekiyou)

        '************
        '* Hidden項目
        '************
        '更新日時
        Me.HiddenUpdDatetime.Value = IIf(dataRec.UpdDatetime = Date.MinValue, "", Format(dataRec.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
        '加盟店コード
        Me.HiddenKamentenCdOld.Value = Me.TextKameitenCd.Text
        '調査会社コード
        Me.HiddenTysKaisyaCdOld.Value = Me.TextTysKaisyaCd.Text
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

        '伝票仕入年月日の自動設定(新規モード、初期読込時のみ)
        If pStrGamenMode = CStr(EarthEnum.emGamenMode.SINKI) Then
            If Me.TextSiireDate.Text <> String.Empty And Me.TextDenpyouSiireDate.Text = String.Empty Then
                Me.TextDenpyouSiireDate.Text = Me.TextSiireDate.Text
            End If
        End If

        '加盟店取消理由設定
        setTorikesiRiyuu(Me.TextKbn.Text, Me.TextKameitenCd.Text)

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
        '+ 加盟店、調査会社検索
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '加盟店
        Me.TextKameitenCd.Attributes("onfocus") = onFocusPostBackScript
        Me.TextKameitenCd.Attributes("onblur") = onBlurPostBackScript

        '調査会社
        Me.TextTysKaisyaCd.Attributes("onfocus") = onFocusPostBackScript
        Me.TextTysKaisyaCd.Attributes("onblur") = onBlurPostBackScript

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 商品検索
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSyouhinCd.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSyouhinCd.Attributes("onblur") = onBlurPostBackScript

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
        '仕入年月日
        Me.TextSiireDate.Attributes("onfocus") = onFocusPostBackScriptDate
        Me.TextSiireDate.Attributes("onblur") = onBlurPostBackScriptDate
        Me.TextSiireDate.Attributes("onkeydown") = disabledOnkeydown

        '伝票仕入年月日
        Me.TextDenpyouSiireDate.Attributes("onfocus") = onFocusPostBackScriptDate
        Me.TextDenpyouSiireDate.Attributes("onblur") = onBlurPostBackScriptDate
        Me.TextDenpyouSiireDate.Attributes("onkeydown") = disabledOnkeydown

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
    Private Sub SetGamenMode(ByVal dataRec As HannyouSiireRecord)

        '仕入計上日
        If cl.GetDisplayString(dataRec.SiireKeijyouDate) <> String.Empty AndAlso userinfo.KeiriGyoumuKengen = 0 Then
            '仕入処理済でかつ、経理業務権限が無い場合、確認モードで開く
            pStrGamenMode = CStr(EarthEnum.emGamenMode.KAKUNIN)
        ElseIf userinfo.KeiriGyoumuKengen = -1 Then
            '経理業務権限を保持している場合、更新モードか新規モードで開く判断を行う
            If cl.GetDisplayString(dataRec.HanSiireUnqNo) <> String.Empty AndAlso dataRec.HanSiireUnqNo > 0 Then
                '汎用仕入ユニークNOが設定済みの場合、更新モードで開く
                pStrGamenMode = CStr(EarthEnum.emGamenMode.KOUSIN)
            Else
                '汎用仕入ユニークNOが未設定の場合、新規モードで開く
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
            '汎用仕入NO
            pStrHanSiireNo = String.Empty '値クリア
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

        ' 取消理由テキストボックスのスタイルを設定
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)

    End Sub

    ''' <summary>
    ''' 取消理由の設定
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <remarks></remarks>
    Private Sub setTorikesiRiyuu(ByVal strKbn As String, ByVal strKameitenCd As String)
        Dim strKameitenColor As String = String.Empty

        '色替え処理対象のコントロールを配列に格納(※取消理由テキストボックス以外)
        Dim objArray() As Object = New Object() {Me.TextKameitenCd, Me.TextKameitenMei}

        '取消理由と加盟店情報の文字色設定
        cl.GetKameitenTorikesiRiyuu(strKbn _
                                        , strKameitenCd _
                                        , Me.TextTorikesiRiyuu _
                                        , True _
                                        , False _
                                        , objArray)
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

        'DB読み込み時点の値を、現在画面の値と比較(変更有無チェック)
        If Me.TextHanSiireNo.Text <> String.Empty AndAlso Me.HiddenOpenValues.Value <> String.Empty Then
            'DB読み込み時点の値が空の場合は比較対象外
            '比較実施
            If Me.HiddenOpenValues.Value <> Me.getCtrlValuesString() Then
                '月次確定予約済みの処理年月「以前」の日付で伝票仕入年月日を設定するのはエラー
                If cl.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouSiireDate.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                    errMess += String.Format(Messages.MSG176E, dtGetujiKakuteiLastSyoriDate.Month, "伝票仕入年月日", dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                    arrFocusTargetCtrl.Add(Me.TextDenpyouSiireDate)
                End If
            End If
        End If

        '新規登録時のチェック
        If Me.TextHanSiireNo.Text = String.Empty Then
            '月次確定予約済みの処理年月「以前」の日付で伝票仕入年月日を設定するのはエラー
            If cl.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouSiireDate.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                errMess += String.Format(Messages.MSG176E, dtGetujiKakuteiLastSyoriDate.Month, "伝票仕入年月日", dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                arrFocusTargetCtrl.Add(Me.TextDenpyouSiireDate)
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

        Dim blnMasterChkSyn As Boolean = True '商品
        Dim blnMasterChkKt As Boolean = True '加盟店
        Dim blnMasterChkTys As Boolean = True '調査会社

        '編集状況をチェック
        Me.checkInputEditJyky(errMess, arrFocusTargetCtrl)

        '●コード入力値変更チェック
        '加盟店コード
        If Me.TextKameitenCd.Text <> String.Empty AndAlso Me.TextKameitenCd.Text <> Me.HiddenKamentenCdOld.Value Then
            errMess += Messages.MSG030E.Replace("@PARAM1", "加盟店コード")
            arrFocusTargetCtrl.Add(Me.ButtonSearchKameiten)
            blnMasterChkKt = False
        End If
        '調査会社コード
        If Me.TextTysKaisyaCd.Text <> Me.HiddenTysKaisyaCdOld.Value Then
            errMess += Messages.MSG030E.Replace("@PARAM1", "調査会社コード")
            arrFocusTargetCtrl.Add(Me.ButtonSearchTysKaisya)
            blnMasterChkTys = False
        End If
        '商品コード
        If Me.TextSyouhinCd.Text <> Me.HiddenSyouhinCdOld.Value Then
            errMess += Messages.MSG030E.Replace("@PARAM1", "商品コード")
            arrFocusTargetCtrl.Add(Me.ButtonSearchSyouhin)
            blnMasterChkSyn = False
        End If

        '●必須チェック
        '調査会社コード
        If Me.TextTysKaisyaCd.Text = String.Empty Then
            If blnMasterChkTys Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "調査会社コード")
                arrFocusTargetCtrl.Add(Me.TextTysKaisyaCd)
            End If
        End If
        '商品コード
        If Me.TextSyouhinCd.Text = String.Empty Then
            If blnMasterChkSyn Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "商品コード")
                arrFocusTargetCtrl.Add(Me.TextSyouhinCd)
            End If
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
        '税区分
        If Me.SelectZeiKbn.SelectedValue = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "税区分")
            arrFocusTargetCtrl.Add(Me.SelectZeiKbn)
        End If
        '消費税額
        If Me.TextSyouhiZeiGaku.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "消費税額")
            arrFocusTargetCtrl.Add(Me.TextSyouhiZeiGaku)
        End If

        '●日付チェック
        '仕入年月日
        If Me.TextSiireDate.Text <> String.Empty Then
            If cl.checkDateHanni(Me.TextSiireDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "仕入年月日")
                arrFocusTargetCtrl.Add(Me.TextSiireDate)
            End If
        End If
        '伝票仕入年月日
        If Me.TextDenpyouSiireDate.Text <> String.Empty Then
            If cl.checkDateHanni(Me.TextDenpyouSiireDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "伝票仕入年月日")
                arrFocusTargetCtrl.Add(Me.TextDenpyouSiireDate)
            End If
        End If

        '●コード桁数チェック
        'なし

        '●禁則文字チェック(文字列入力フィールドが対象)
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

        '未仕入の場合でかつ、仕入年月日と伝票仕入年月日が異なる場合
        If Me.SpanSiireSyoriZumi.InnerHtml <> EarthConst.SIIRE_ZUMI Then
            '伝票仕入年月日と比較
            If Me.TextSiireDate.Text <> Me.TextDenpyouSiireDate.Text Then
                errMess += Messages.MSG144E.Replace("@PARAM1", "伝票仕入年月日").Replace("@PARAM2", "仕入年月日").Replace("@PARAM3", "更新")
                arrFocusTargetCtrl.Add(Me.TextSiireDate)
            End If
        End If

        '仕入年月日と伝票仕入年月日
        If Me.TextSiireDate.Text = String.Empty And Me.TextDenpyouSiireDate.Text <> String.Empty Then
            errMess += Messages.MSG153E.Replace("@PARAM1", "伝票仕入年月日").Replace("@PARAM2", "仕入年月日")
            arrFocusTargetCtrl.Add(TextSiireDate)
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
        Dim logic As New HannyouSiireLogic
        Dim dataRec As New HannyouSiireRecord

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
    Public Function GetCtrlToDataRec() As HannyouSiireRecord
        Dim dataRec As New HannyouSiireRecord

        '***************************************
        ' 汎用仕入データ
        '***************************************
        ' 汎用仕入ユニークNO
        cl.SetDisplayString(Me.TextHanSiireNo.Text, dataRec.HanSiireUnqNo)
        ' 取消
        If Me.CheckTorikesi.Checked Then
            dataRec.Torikesi = 1
        Else
            dataRec.Torikesi = 0
        End If
        ' 摘要名
        cl.SetDisplayString(Me.TextAreaTekiyou.Value, dataRec.Tekiyou)
        ' 仕入年月日
        cl.SetDisplayString(Me.TextSiireDate.Text, dataRec.SiireDate)
        ' 伝票仕入年月日
        cl.SetDisplayString(Me.TextDenpyouSiireDate.Text, dataRec.DenpyouSiireDate)
        '調査会社等
        Dim tmpTys As String = Me.TextTysKaisyaCd.Text
        If tmpTys.Length >= 6 Then   '長さ6以上必須
            dataRec.TysKaisyaCd = tmpTys.Substring(0, tmpTys.Length - 2) '調査会社コード
            dataRec.TysKaisyaJigyousyoCd = tmpTys.Substring(tmpTys.Length - 2, 2) '調査会社事業所コード
        End If
        ' 加盟店コード
        cl.SetDisplayString(Me.TextKameitenCd.Text, dataRec.KameitenCd)
        ' 商品コード
        cl.SetDisplayString(Me.TextSyouhinCd.Text, dataRec.SyouhinCd)
        ' 商品名
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
        ' 仕入計上FLG●
        dataRec.SiireKeijyouFlg = 1
        ' 仕入計上日●
        dataRec.SiireKeijyouDate = Date.Now.ToString("yyyy/MM/dd")
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
        ' 仕入金額
        Dim siiregaku_ctrl As TextBox
        ' 消費税額
        Dim syouhizeigaku_ctrl As TextBox

        tanka_ctrl = Me.TextTanka
        suu_ctrl = Me.TextSuu
        zeiritu_ctrl = Me.HiddenZeiritu
        siiregaku_ctrl = Me.TextUriGaku
        syouhizeigaku_ctrl = Me.TextSyouhiZeiGaku

        Dim tanka As Long = 0
        Dim suu As Long = 0
        Dim zeiritu As Decimal = 0
        Dim siiregaku As Long = 0
        Dim syouhizeigaku As Long = 0

        '単価・数量・税率が空白の場合、仕入金額・消費税額を空白にする
        If tanka_ctrl.Text.Trim = String.Empty _
            OrElse suu_ctrl.Text.Trim = String.Empty _
                OrElse zeiritu_ctrl.Value = String.Empty Then
            siiregaku_ctrl.Text = String.Empty
            syouhizeigaku_ctrl.Text = String.Empty
        Else
            tanka = cl.Str2Long(tanka_ctrl.Text) '単価
            suu = cl.Str2Long(suu_ctrl.Text) '数量
            cl.SetDisplayString(zeiritu_ctrl.Value, zeiritu) '税率
            zeiritu = IIf(zeiritu = Decimal.MinValue, 0, zeiritu)

            siiregaku = suu * tanka '仕入金額(税計算前) = 数量 * 単価

            If blnZeigaku Then
                '消費税額計算の場合
                If syouhizeigaku_ctrl.Text.Trim <> String.Empty Then
                    syouhizeigaku = cl.Str2Long(syouhizeigaku_ctrl.Text)    '画面.消費税額
                End If
                siiregaku = siiregaku + syouhizeigaku
            Else
                syouhizeigaku = Fix(siiregaku * zeiritu)    '消費税額 = 仕入金額(税抜) * 税率
                siiregaku = Fix(siiregaku * (1 + zeiritu))  '仕入金額(税込) = 仕入金額(税抜) * (1+税率)
            End If

            '画面表示用にフォーマット
            siiregaku_ctrl.Text = Format(siiregaku, EarthConst.FORMAT_KINGAKU_1)            '仕入金額
            syouhizeigaku_ctrl.Text = Format(syouhizeigaku, EarthConst.FORMAT_KINGAKU_1)    '消費税額

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
        Me.setFocusAJ(Me.TextSiireDate)
    End Sub

#End Region

#Region "画面制御"
    ''' <summary>
    ''' コントロールの画面制御
    ''' </summary>
    ''' <remarks>コントロールの画面制御を仕様書の優先順に記載する</remarks>
    Public Sub SetEnableControl()

        '仕入処理済
        If Me.SpanSiireSyoriZumi.InnerHtml = EarthConst.SIIRE_ZUMI AndAlso userinfo.KeiriGyoumuKengen = 0 Then
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
            '活性(テキストボックス化)
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
        '仕入年月日
        sb.Append(Me.TextSiireDate.Text & EarthConst.SEP_STRING)
        '伝票仕入年月日
        sb.Append(Me.TextDenpyouSiireDate.Text & EarthConst.SEP_STRING)
        '調査会社コード+調査会社事業所コード
        sb.Append(Me.TextTysKaisyaCd.Text & EarthConst.SEP_STRING)
        '商品コード
        sb.Append(Me.TextSyouhinCd.Text & EarthConst.SEP_STRING)
        '数量
        sb.Append(Me.TextSuu.Text & EarthConst.SEP_STRING)
        '仕入金額
        sb.Append(Me.TextUriGaku.Text & EarthConst.SEP_STRING)
        '税区分
        sb.Append(Me.SelectZeiKbn.SelectedValue & EarthConst.SEP_STRING)
        '消費税額
        sb.Append(Me.TextSyouhiZeiGaku.Text & EarthConst.SEP_STRING)

        Return (sb.ToString)
    End Function
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

#Region "仕入_日付関連"
    ''' <summary>
    ''' 仕入年月日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSiireDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strZeiKbn As String = String.Empty   '税区分
        Dim strZeiritu As String = String.Empty   '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        '仕入年月日
        If Me.TextSiireDate.Text <> String.Empty Then
            '伝票仕入年月日
            If Me.TextDenpyouSiireDate.Text.Trim() = String.Empty Then
                '仕入年月日をセット
                Me.TextDenpyouSiireDate.Text = Me.TextSiireDate.Text
            End If

            '税区分・税率を再取得
            strSyouhinCd = Me.TextSyouhinCd.Text
            If cbLogic.getSyouhiZeirituUriage(Me.TextSiireDate.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                Me.SelectZeiKbn.SelectedValue = strZeiKbn
                Me.HiddenZeiritu.Value = strZeiritu
                '金額計算
                Me.SetKingaku()
            End If
        End If

        'フォーカスの設定
        Me.setFocusAJ(Me.TextDenpyouSiireDate)
    End Sub
#End Region

#Region "加盟店関連"

    ''' <summary>
    ''' 加盟店検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSearchKameiten_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '加盟店検索・変更・色変(取消時)処理
        If cl.CallKameitenSearchWindow(sender _
                                        , e _
                                        , Me _
                                        , Me.TextKbn.ClientID _
                                        , Me.TextKbn.Text _
                                        , Me.TextKameitenCd _
                                        , Me.TextKameitenMei _
                                        , Me.ButtonSearchKameiten _
                                        , True _
                                        , Me.TextTorikesiRiyuu) Then

            '加盟店コードを退避
            Me.HiddenKamentenCdOld.Value = Me.TextKameitenCd.Text
        End If

        'フォーカスの設定
        Me.setFocusAJ(Me.ButtonSearchKameiten)

    End Sub

    ''' <summary>
    ''' 加盟店コード変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKameitenCd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '加盟店情報初期化
        clrKameitenInfo()

        'フォーカスの設定
        Me.setFocusAJ(Me.ButtonSearchKameiten)

    End Sub

    ''' <summary>
    ''' 加盟店関連情報をクリアし、文字スタイルを標準(黒)に戻す
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub clrKameitenInfo()

        '加盟店名
        Me.TextKameitenMei.Text = String.Empty
        '加盟店取消理由
        Me.TextTorikesiRiyuu.Text = String.Empty
        '加盟店コード/名称/取消理由の文字色スタイル
        cl.setStyleFontColor(Me.TextKameitenCd.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextKameitenMei.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextTorikesiRiyuu.Style, cl.getKameitenFontColor(0))

    End Sub

#End Region

#Region "調査会社関連"
    ''' <summary>
    ''' 調査会社検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSearchTysKaisya_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.HiddenTysKaisyaSearchType.Value <> "1" Then
            Me.TextTysKaisya_ChangeSub(sender, e)
        Else
            'コードonchangeで呼ばれた場合
            Me.TextTysKaisya_ChangeSub(sender, e, False)
            Me.HiddenTysKaisyaSearchType.Value = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' 調査会社コード変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextTysKaisyaCd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '調査会社名
        Me.TextTysKaisyaMei.Text = String.Empty

        'フォーカスの設定
        Me.setFocusAJ(Me.ButtonSearchTysKaisya)
    End Sub

    ''' <summary>
    ''' 調査会社変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextTysKaisya_ChangeSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal ProcType As Boolean = True)

        '調査会社
        Me.tyousakaisyaSearchSub(sender, e)

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

        '調査会社コード=入力の場合
        If Me.TextTysKaisyaCd.Text <> String.Empty Then
            dataArray = tysLogic.GetTyousakaisyaSearchResult(Me.TextTysKaisyaCd.Text, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            '調査会社コード
            Me.TextTysKaisyaCd.Text = recData.TysKaisyaCd & recData.JigyousyoCd '調査会社コード + 調査会社事業所コード
            '調査会社名
            Me.TextTysKaisyaMei.Text = recData.TysKaisyaMei
            '調査会社コードを退避
            Me.HiddenTysKaisyaCdOld.Value = Me.TextTysKaisyaCd.Text

            'フォーカスセット
            Me.setFocusAJ(Me.ButtonSearchTysKaisya)

        ElseIf ProcType = True Then
            '調査会社名
            Me.TextTysKaisyaMei.Text = String.Empty
            'ダミー
            Me.HiddenKamentenCd.Value = String.Empty

            Dim tmpFocusScript = "objEBI('" & ButtonSearchTysKaisya.ClientID & "').focus();"

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpScript = "callSearch('" & Me.TextTysKaisyaCd.ClientID & EarthConst.SEP_STRING & Me.HiddenKamentenCd.ClientID & _
                                    "','" & UrlConst.SEARCH_TYOUSAKAISYA & _
                                    "','" & Me.TextTysKaisyaCd.ClientID & EarthConst.SEP_STRING & Me.TextTysKaisyaMei.ClientID & _
                                    "','" & ButtonSearchTysKaisya.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSearch", tmpScript, True)
            Exit Sub
        End If

    End Sub

#End Region

#Region "商品情報系"

    ''' <summary>
    ''' 商品コード変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSyouhinCd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '商品名
        Me.TextSyouhinMei.Text = String.Empty

        'フォーカスの設定
        Me.setFocusAJ(Me.ButtonSearchSyouhin)
    End Sub

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
        Dim dataArray As New List(Of SyouhinMeisaiRecord)

        If Me.TextSyouhinCd.Text <> String.Empty Then
            '商品検索を実行
            dataArray = sLogic.GetSyouhinInfo(TextSyouhinCd.Text, _
                                                "", _
                                                EarthEnum.EnumSyouhinKubun.AllSyouhin, _
                                                total_row)
        End If

        If total_row = 1 Then
            '商品情報を画面にセット
            Dim recData As SyouhinMeisaiRecord = dataArray(0)

            Me.TextSyouhinCd.Text = cl.GetDisplayString(recData.SyouhinCd) '商品コード
            Me.TextSyouhinMei.Text = cl.GetDisplayString(recData.SyouhinMei)      '商品名

            ' 商品コードを退避
            Me.HiddenSyouhinCdOld.Value = Me.TextSyouhinCd.Text

            'フォーカスセット
            Me.setFocusAJ(Me.ButtonSearchSyouhin)

        ElseIf ProcType = True Then

            '商品名
            Me.TextSyouhinMei.Text = String.Empty

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

        Me.UpdatePanelSyouhinInfo.Update()


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