
Partial Public Class PopupBukkenRirekiSyuusei
    Inherits System.Web.UI.Page

#Region "物件履歴・行コントロールID接頭語"
    Private Const BUKKEN_RIREKI_CTRL_NAME As String = "CtrlBukkenRireki_"
    Private Const SELECT_SYUBETU_CTRL_NAME As String = "SelectSyubetu_"
#End Region

#Region "物件履歴・コントロール値"
    'タイトル
    Private Const CTRL_VALUE_TITLE As String = "物件履歴"
    Private Const CTRL_VALUE_TITLE_KOUSIN As String = CTRL_VALUE_TITLE & EarthConst.GAMEN_MODE_EDIT
    Private Const CTRL_VALUE_TITLE_TOUROKU As String = CTRL_VALUE_TITLE & EarthConst.GAMEN_MODE_NEW
    Private Const CTRL_VALUE_TITLE_KAKUNIN As String = CTRL_VALUE_TITLE & EarthConst.GAMEN_MODE_VIEW
#End Region

    ''' <summary>
    ''' 物件履歴明細行情報/行ユーザーコントロール格納リスト
    ''' </summary>
    ''' <remarks></remarks>
    Private pListCtrl As New List(Of BukkenRirekiRecordCtrl)

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim iraiSession As New IraiSession
    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    ''' <summary>
    ''' メッセージクラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic

#Region "プロパティ"

#Region "パラメータ/物件履歴検索画面"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _kbn As String
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrKbn() As String
        Get
            Return _kbn
        End Get
        Set(ByVal value As String)
            _kbn = value
        End Set
    End Property

    ''' <summary>
    ''' 番号(保証書No)
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _no As String
    ''' <summary>
    ''' 番号(保証書No)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrBangou() As String
        Get
            Return _no
        End Get
        Set(ByVal value As String)
            _no = value
        End Set
    End Property

    ''' <summary>
    ''' 入力NO
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _NyuuryokuNo As String
    ''' <summary>
    ''' 入力NO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrNyuuryokuNo() As String
        Get
            Return _NyuuryokuNo
        End Get
        Set(ByVal value As String)
            _NyuuryokuNo = value
        End Set
    End Property

    ''' <summary>
    ''' 画面モード
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _GamenMode As String
    ''' <summary>
    ''' 入力NO
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
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

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
            ' Key情報を保持
            Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
            If arrSearchTerm.Length = 3 Then
                pStrKbn = arrSearchTerm(0)     '親画面からPOSTされた情報1 ：区分
                pStrBangou = arrSearchTerm(1)     '親画面からPOSTされた情報2 ：保証書NO
                pStrNyuuryokuNo = arrSearchTerm(2)     '親画面からPOSTされた情報3 ：入力NO
            End If

            ' パラメータ不足時は画面を表示しない
            If pStrKbn Is Nothing Or pStrBangou Is Nothing Or pStrNyuuryokuNo Is Nothing Then
                cl.CloseWindow(Me)
                Me.ButtonUpdate.Style("display") = "none"
                Exit Sub
            End If


            '●権限のチェック
            '以下のいずれかの権限がない場合、画面参照のみ
            '依頼業務権限
            '結果業務権限
            '保証業務権限
            '報告書業務権限
            '工事業務権限
            '経理業務権限
            If userinfo.IraiGyoumuKengen = 0 _
                And userinfo.KekkaGyoumuKengen = 0 _
                And userinfo.HosyouGyoumuKengen = 0 _
                And userinfo.HoukokusyoGyoumuKengen = 0 _
                And userinfo.KoujiGyoumuKengen = 0 _
                And userinfo.KeiriGyoumuKengen = 0 Then

                pStrGamenMode = CStr(EarthEnum.emGamenMode.KAKUNIN)
            Else
                pStrGamenMode = String.Empty
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            Dim helper As New DropDownHelper

            '●ダミーコンボにセット
            helper.SetMeisyouDropDownList(Me.SelectTmpCode, EarthConst.emMeisyouType.BUKKEN_RIREKI_SYUBETU, False, True)

            'ダミードロップダウンリストの生成
            Me.CreateDropDownList(Me.SelectTmpCode)

            '****************************************************************************
            ' 地盤データ取得
            '****************************************************************************
            Dim logic As New JibanLogic
            Dim jibanRec As New JibanRecordBase
            jibanRec = logic.GetJibanData(pStrKbn, pStrBangou)

            '地盤読み込みデータがセッションに存在する場合、画面に表示させる
            If logic.ExistsJibanData(pStrKbn, pStrBangou) AndAlso jibanRec IsNot Nothing Then
                Me.SetCtrlFromJibanRec(sender, e, jibanRec) '地盤データをコントロールにセット
            Else
                cl.CloseWindow(Me)
                Me.ButtonUpdate.Style("display") = "none"
                Exit Sub
            End If

            '****************************************************************************
            ' 物件履歴データ取得
            '****************************************************************************
            Me.SetCtrlFromDataRec(sender, e)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            Me.SetDispAction()

            'ボタン押下イベントの設定
            Me.setBtnEvent()

            Me.ButtonClose.Focus() 'フォーカス

        Else
            'ダミードロップダウンリストの生成
            Me.CreateDropDownList(Me.SelectTmpCode)

            '画面設定
            Me.SetDispControl()

        End If

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDispAction()
        Dim jBn As New Jiban '地盤画面クラス

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' プルダウン/コード入力連携設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'なし

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript関連セット
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim checkDate As String = "checkDate(this);"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ***コードおよび検索ポップアップボタン
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'なし

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 金額系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'なし

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 日付系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '日付
        Me.TextHizuke.Attributes("onblur") = checkDate
        Me.TextHizuke.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ドロップダウンリスト
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '種別
        Me.SelectSyubetu.Attributes("onchange") = "SelectSyubetuOnChg('" & Me.SelectSyubetu.ClientID & "')"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 数値系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'なし

    End Sub

    ''' <summary>
    ''' 登録/修正ボタンの設定
    ''' </summary>
    ''' <remarks>
    ''' DB更新中、画面のグレイアウトを行なう。<br/>
    ''' </remarks>
    Private Sub setBtnEvent()
        'イベントハンドラ登録
        Dim tmpScriptOverLay As String = "setWindowOverlay(this,null,1);"

        Me.ButtonUpdate.Attributes("onclick") = tmpScriptOverLay

    End Sub

    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行う（参照処理用）
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)

        ' コンボ設定ヘルパークラスを生成
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        '画面コントロールに設定
        '●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Kubun, False, True)
        objDrpTmp.SelectedValue = jr.Kbn
        Me.TextKbn.Text = objDrpTmp.SelectedItem.Text '区分
        Me.HiddenKbn.Value = jr.Kbn '隠しフィールド

        Me.TextBangou.Text = cl.GetDispStr(jr.HosyousyoNo) '番号
        Me.TextSesyuMei.Text = cl.GetDispStr(jr.SesyuMei) '施主名

    End Sub

    ''' <summary>
    ''' 物件履歴レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object _
                                    , ByVal e As System.EventArgs _
                                    )

        '画面再描画処理を行う
        Dim dataRec As New BukkenRirekiRecord
        Dim logic As New BukkenRirekiLogic

        '該当行情報を再取得
        dataRec = logic.getBukkenRirekiRecord(pStrKbn, pStrBangou, pStrNyuuryokuNo)

        Dim blnFlg As Boolean = True
        Dim strSpace As String = ""

        Dim helper As New DropDownHelper

        ' 種別コンボにデータをバインドする
        helper.SetMeisyouDropDownList(Me.SelectSyubetu, EarthConst.emMeisyouType.BUKKEN_RIREKI_SYUBETU)

        '******************************************
        '* 画面コントロールに設定
        '******************************************
        '(履歴)種別
        '存在チェック
        If cl.ChkDropDownList(Me.SelectSyubetu, cl.GetDispNum(dataRec.RirekiSyubetu)) Then
            Me.SelectSyubetu.SelectedValue = cl.GetDisplayString(dataRec.RirekiSyubetu, "")

            'コード(履歴NO)
            '存在チェック
            If cl.ChkDropDownList(Me.SelectSyubetu, cl.GetDispNum(dataRec.RirekiSyubetu)) Then
                Me.HiddenBunrui.Value = cl.GetDisplayString(dataRec.RirekiNo, "")
            End If
        End If
        '(汎用)日付
        Me.TextHizuke.Text = cl.GetDisplayString(dataRec.HanyouDate)
        '汎用コード
        Me.TextHanyouCode.Text = cl.GetDisplayString(dataRec.HanyouCd)
        '内容
        Me.TextAreaNaiyou.Value = cl.GetDisplayString(dataRec.Naiyou)
        '登録者
        Me.TextTourokuSya.Text = cl.SetDispUserNM(dataRec.AddLoginUserId)
        '登録日時
        Me.TextTourokuDate.Text = IIf(dataRec.AddDatetime = Date.MinValue, "", Format(dataRec.AddDatetime, EarthConst.FORMAT_DATE_TIME_5))
        '更新者
        Me.TextKousinSya.Text = cl.SetDispUserNM(dataRec.UpdLoginUserId)
        '更新日時
        Me.TextKousinDate.Text = IIf(dataRec.UpdDatetime = Date.MinValue, "", Format(dataRec.UpdDatetime, EarthConst.FORMAT_DATE_TIME_5))

        '****************************
        '* Hidden項目
        '****************************
        '入力NO
        If pStrNyuuryokuNo = "0" Then
            Me.HiddenNyuuryokuNo.Value = Integer.MinValue.ToString
        Else
            Me.HiddenNyuuryokuNo.Value = cl.GetDisplayString(dataRec.NyuuryokuNo)
        End If

        '更新日時
        Me.HiddenUpdDatetime.Value = IIf(dataRec.UpdDatetime = Date.MinValue, "", Format(dataRec.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))

        '画面モード別設定
        Me.SetGamenMode(dataRec)

    End Sub

#Region "プライベートメソッド"

    ''' <summary>
    ''' ダミードロップダウンリストの生成
    ''' </summary>
    ''' <param name="SelectTarget">対象元ドロップダウンリスト</param>
    ''' <remarks>名称M.名称種別=16に紐付く名称M.コード=名称M.名称種別のダミードロップダウンリストを生成する</remarks>
    Private Sub CreateDropDownList(ByRef SelectTarget As DropDownList)

        Dim helper As New DropDownHelper

        '●ダミーコンボにセット
        Dim objDrpTmp As DropDownList

        Dim intCnt As Integer = 0
        Dim intValue As Integer

        If SelectTarget.Items.Count <= 0 Then
            Dim strMsg As String = Messages.MSG113E.Replace("@PARAM1", "種別")
            Dim tmpScript As String = "alert('" & strMsg & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CreateDropDownList", tmpScript, True)

            cl.CloseWindow(Me)
            Me.ButtonUpdate.Style("display") = "none"
            Exit Sub
        End If

        For intCnt = 0 To SelectTarget.Items.Count - 1
            intValue = SelectTarget.Items(intCnt).Value 'Value値取得

            objDrpTmp = New DropDownList
            objDrpTmp.ID = SELECT_SYUBETU_CTRL_NAME & intValue.ToString 'ID付与
            objDrpTmp.Style("display") = "none" '非表示
            helper.SetMeisyouDropDownList(objDrpTmp, intValue) '値セット

            Me.divSelect.Controls.Add(objDrpTmp) 'コントロール追加
        Next

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
    Public Function checkInput(ByVal objBtn As HtmlInputButton) As Boolean

        Dim tmpFocusScript As String = String.Empty
        Dim tmpScript As String = String.Empty

        '地盤画面共通クラス
        Dim jBn As New Jiban

        '全てのコントロールを有効化(破棄種別チェック用)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        'エラーメッセージ初期化
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New ArrayList

        '●コード入力値変更チェック
        'なし

        '●必須チェック
        '種別
        If Me.SelectSyubetu.SelectedValue = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "種別")
            arrFocusTargetCtrl.Add(Me.SelectSyubetu)
        End If
        'コード
        If Me.HiddenBunrui.Value = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "コード")
            arrFocusTargetCtrl.Add(Me.HiddenBunrui)
        End If

        '●日付チェック
        '(汎用)日付
        If Me.TextHizuke.Text <> String.Empty Then
            If cl.checkDateHanni(Me.TextHizuke.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "日付")
                arrFocusTargetCtrl.Add(Me.TextHizuke)
            End If
        End If

        '●桁数チェック
        'なし

        '●禁則文字チェック(文字列入力フィールドが対象)
        '汎用コード
        If jBn.KinsiStrCheck(Me.TextHanyouCode.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "汎用コード")
            arrFocusTargetCtrl.Add(Me.TextHanyouCode)
        End If
        '内容
        If jBn.KinsiStrCheck(Me.TextAreaNaiyou.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "内容")
            arrFocusTargetCtrl.Add(Me.TextAreaNaiyou)
        End If

        '●バイト数チェック(文字列入力フィールドが対象)
        '汎用コード
        If jBn.ByteCheckSJIS(Me.TextHanyouCode.Text, Me.TextHanyouCode.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "汎用コード")
            arrFocusTargetCtrl.Add(Me.TextHanyouCode)
        End If

        '改行変換
        If Me.TextAreaNaiyou.Value <> "" Then
            Me.TextAreaNaiyou.Value = Me.TextAreaNaiyou.Value.Replace(vbCrLf, " ")
        End If
        '内容
        If jBn.ByteCheckSJIS(Me.TextAreaNaiyou.Value, 512) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "内容")
            arrFocusTargetCtrl.Add(Me.TextAreaNaiyou)
        End If

        '●その他チェック
        'なし

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If errMess <> "" Then

            If arrFocusTargetCtrl.Count > 0 Then
                tmpFocusScript = "gVarFocus = '" & arrFocusTargetCtrl(0).ClientID & "';"
            End If

            'エラーメッセージ表示
            tmpScript = "alert('" & errMess & "');" & tmpFocusScript
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        'エラー無しの場合、Trueを返す
        Return True
    End Function

    ''' <summary>
    ''' 画面の内容をDBに反映する
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function SaveData(ByVal objBtn As HtmlInputButton) As Boolean
        '*************************
        '物件履歴データを更新する
        '*************************
        Dim logic As New BukkenRirekiLogic
        Dim blnExe As Boolean = False

        '各行ごとに画面からレコードクラスに入れ込み
        Dim dataRec As BukkenRirekiRecord = Nothing

        dataRec = Me.GetRowCtrlToDataRec()

        ' データの更新を行います
        If logic.saveData(Me, dataRec) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 画面の各明細行情報をレコードクラスに取得し、地盤レコードクラスのリストを返却する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetRowCtrlToDataRec() As BukkenRirekiRecord

        Dim intCnt As Integer = 0
        Dim ctrl As New BukkenRirekiRecordCtrl
        ' 画面内容より地盤レコードを生成する
        Dim dataRec As New BukkenRirekiRecord

        With ctrl
            '***************************************
            ' 物件履歴データ
            '***************************************
            ' 区分
            cl.SetDisplayString(Me.HiddenKbn.Value, dataRec.Kbn)
            ' 番号
            cl.SetDisplayString(Me.TextBangou.Text, dataRec.HosyousyoNo)
            ' 履歴種別
            cl.SetDisplayString(Me.SelectSyubetu.SelectedValue, dataRec.RirekiSyubetu)
            ' 履歴NO
            cl.SetDisplayString(Me.HiddenBunrui.Value, dataRec.RirekiNo)
            ' 入力NO
            cl.SetDisplayString(Me.HiddenNyuuryokuNo.Value, dataRec.NyuuryokuNo)
            ' 内容
            cl.SetDisplayString(Me.TextAreaNaiyou.Value, dataRec.Naiyou)
            ' (汎用)日付
            cl.SetDisplayString(Me.TextHizuke.Text, dataRec.HanyouDate)
            ' 汎用コード
            cl.SetDisplayString(Me.TextHanyouCode.Text, dataRec.HanyouCd)
            ' 更新者ユーザーID
            dataRec.UpdLoginUserId = userinfo.LoginUserId
            ' 更新日時 読み込み時のタイムスタンプ
            If Me.HiddenUpdDatetime.Value = "" Then
                dataRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
            Else
                dataRec.UpdDatetime = DateTime.ParseExact(Me.HiddenUpdDatetime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
            End If

        End With

        Return dataRec
    End Function

    ''' <summary>
    ''' 画面モードを設定する
    ''' </summary>
    ''' <param name="dataRec">物件履歴レコード</param>
    ''' <remarks></remarks>
    Private Sub SetGamenMode(ByVal dataRec As BukkenRirekiRecord)
        '登録日付
        Dim strTmpAddDate As String = IIf(dataRec.AddDatetime = Date.MinValue, "", Format(dataRec.AddDatetime, EarthConst.FORMAT_DATE_TIME_3))
        'システム日付
        Dim strNowDate As String = Format(Now.Date, EarthConst.FORMAT_DATE_TIME_3)

        '権限チェックでセット済の場合
        If pStrGamenMode = CStr(EarthEnum.emGamenMode.KAKUNIN) Then
        Else
            '新規登録時
            If strTmpAddDate = String.Empty Then
                pStrGamenMode = CStr(EarthEnum.emGamenMode.SINKI)

            ElseIf strTmpAddDate = strNowDate Then '更新時：登録日付=システム日付
                '画面モード判定
                If cl.GetDisplayString(dataRec.Torikesi) <> 0 Or cl.GetDisplayString(dataRec.HenkouKahiFlg) <> 0 Then
                    pStrGamenMode = CStr(EarthEnum.emGamenMode.KAKUNIN)
                Else
                    If dataRec.Kbn <> String.Empty And dataRec.HosyousyoNo <> String.Empty And dataRec.NyuuryokuNo > 0 Then
                        pStrGamenMode = CStr(EarthEnum.emGamenMode.KOUSIN)
                    Else
                        pStrGamenMode = CStr(EarthEnum.emGamenMode.SINKI)
                    End If
                End If

            Else
                pStrGamenMode = CStr(EarthEnum.emGamenMode.KAKUNIN)
            End If
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
            '入金取込NO
            pStrNyuuryokuNo = String.Empty '値クリア
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

#Region "ボタンイベント"

    ''' <summary>
    ''' 登録/更新 ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonUpdate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUpdate.ServerClick

        Dim strScript As String = String.Empty
        Dim strFocusScript As String = String.Empty
        Dim strErrMsg As String = String.Empty

        Dim objBtn As HtmlInputButton = CType(sender, HtmlInputButton)

        ' 入力チェック
        If Me.checkInput(objBtn) = False Then Exit Sub

        ' 画面の内容をDBに反映する
        If Me.SaveData(objBtn) Then '登録成功
            '登録完了後、画面をリロードするために、キー情報を引き渡す
            Context.Items("sendSearchTerms") = Me.HiddenKbn.Value & EarthConst.SEP_STRING & Me.TextBangou.Text
            '親画面リロード処理
            strScript = "OyaReload();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonUpdate_ServerClick1", strScript, True)
            Exit Sub

        Else '更新失敗

            'MSG内容およびフォーカス
            strErrMsg = Me.ButtonUpdate.Value
            strFocusScript = "gVarFocus = '" & Me.ButtonUpdate.ClientID & "';"

            strScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", strErrMsg) & "');" & strFocusScript
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonUpdate_ServerClick2", strScript, True)
            Exit Sub
        End If

    End Sub

#End Region

End Class