Partial Public Class Hosyou
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim iraiSession As New IraiSession
    Dim userInfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager

    Dim cl As New CommonLogic
    Dim MLogic As New MessageLogic
    Dim JibanLogic As New JibanLogic
    Dim kameitenlogic As New KameitenSearchLogic
    Dim MyLogic As New HosyouLogic
    Dim cbLogic As New CommonBizLogic
    Dim BJykyLogic As New BukkenSintyokuJykyLogic

#Region "プロパティ"
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
#End Region

#Region "保証画面用/コントロール接頭辞タイプ"

    ''' <summary>
    ''' 金額タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Enum EnumKingakuType
        ''' <summary>
        ''' 再発行
        ''' </summary>
        ''' <remarks></remarks>
        Saihakkou = 0
        ''' <summary>
        ''' 解約払戻
        ''' </summary>
        ''' <remarks></remarks>
        KaiyakuHaraimodosi = 1
        ''' <summary>
        ''' 指定なし
        ''' </summary>
        ''' <remarks></remarks>
        None = 2
    End Enum

#End Region

#Region "CSSクラス名"
    Private Const CSS_TEXT_CENTER = "textCenter"
    Private Const CSS_KINGAKU = "kingaku"
    Private Const CSS_NUMBER = "number"
    Private Const CSS_DATE = "date"

    Private Const CSS_COLOR_RED = "red"
    Private Const CSS_COLOR_BLUE = "blue"
    Public Const CSS_COLOR_GRAY = "#dadada"
#End Region

#Region "ページロードイベント"

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
        jBn.UserAuth(userInfo)

        '認証結果によって画面表示を切替える
        If userInfo Is Nothing Then
            'ログイン情報が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If Context.Items("irai") IsNot Nothing Then
            iraiSession = Context.Items("irai")
        End If

        '各テーブルの表示状態を切り替える
        ucGyoumuKyoutuu.AccKyoutuuInfo.Style("display") = ucGyoumuKyoutuu.AccHdnKyoutuuInfoStyle.Value
        TbodyHakkouIraiInfo.Style("display") = HiddenHakkouIraiInfoStyle.Value
        TbodyHoshoInfo.Style("display") = HiddenHosyouInfoStyle.Value
        TbodyKairyoKouji.Style("display") = HiddenKairyouKoujiStyle.Value
        TbodySaiHakkou.Style("display") = HiddenSaiHakkouStyle.Value

        If IsPostBack = False Then

            ' Key情報を保持
            _kbn = Request("sendPage_kubun")
            _no = Request("sendPage_hosyoushoNo")

            ' パラメータ不足時は画面を表示しない
            If _kbn Is Nothing Or _
               _no Is Nothing Then
                Response.Redirect(UrlConst.MAIN)
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            Dim helper As New DropDownHelper
            ' 保証情報内=================================
            ' 保証書発行状況コンボにデータをバインドする
            helper.SetDropDownList(Me.SelectHosyousyoHakkouJyoukyou, DropDownHelper.DropDownType.HosyousyoHakJyky)
            ' 保証なし理由コンボにデータをバインドする
            helper.SetKtMeisyouDropDownList(Me.SelectHosyouNasiRiyuu, EarthConst.emKtMeisyouType.HOSYOU_NASI_RIYUU, True, True)
            ' 汎用NOコンボにデータをバインドする(保証なし理由テキストボックス活性化制御用)
            helper.SetKtMeisyouHannyouDropDownList(Me.SelectHannyouNo, EarthConst.emKtMeisyouType.HOSYOU_NASI_RIYUU, EarthEnum.emKtMeisyouType.HannyouNo, True, False)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            SetDispAction()

            'ボタン押下イベントの設定
            setBtnEvent()

            '****************************************************************************
            ' 地盤データ取得
            '****************************************************************************
            Dim logic As New JibanLogic
            Dim jibanRec As New JibanRecordBase
            jibanRec = logic.GetJibanData(pStrKbn, pStrBangou)

            If Not jibanRec Is Nothing Then
                '地盤データの読み込み
                iraiSession.JibanData = jibanRec
                '地盤データをコントロールにセット
                SetCtrlFromJibanRec(jibanRec)

                '****************************************************************************
                ' 保証書管理データ取得
                '****************************************************************************
                Dim HosyouRec As New HosyousyoKanriRecord
                HosyouRec = BJykyLogic.getSearchKeyDataRec(sender, jibanRec.Kbn, jibanRec.HosyousyoNo)

                If Not HosyouRec Is Nothing Then
                    '保証書管理データをコントロールにセット
                    Me.SetCtrlFromHosyouRec(jibanRec, HosyouRec)
                End If

                '****************************************************************************
                ' 進捗データ取得
                '****************************************************************************
                Dim reportRec As New ReportIfGetRecord
                JibanLogic.GetReportIfData(jibanRec, reportRec)
                Me.SetCtrlFromReportRec(jibanRec, reportRec)
            End If

        Else
            '商品毎の請求・仕入先が変更されていないかをチェックし、
            '変更されている場合情報の再取得
            setSeikyuuSiireHenkou(sender, e)

        End If

        'コンテキストに値を格納
        Context.Items("irai") = iraiSession

    End Sub

    ''' <summary>
    ''' ページロードコンプリート
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '物件進捗状況ボタン
        cl.getBukkenJykyMasterPath(ButtonBukkenJyokyou, _
                                 userInfo, _
                                 Me.ucGyoumuKyoutuu.AccHiddenKubun.ClientID, _
                                 Me.ucGyoumuKyoutuu.AccBangou.ClientID)

        '****************************
        '* 保証なし理由テキストボックスの活性化制御（保証情報）
        '****************************
        If Me.SelectHosyouNasiRiyuu.Style("display") <> "none" Then
            If Me.SelectHosyouNasiRiyuu.SelectedIndex > -1 Then
                If Me.SelectHannyouNo.SelectedItem.Text = EarthConst.ARI_VAL Then
                    Me.TextHosyouNasiRiyuu.Enabled = True
                    Me.TextHosyouNasiRiyuu.Style(EarthConst.STYLE_BACK_GROUND_COLOR) = EarthConst.STYLE_COLOR_WHITE
                Else
                    Me.TextHosyouNasiRiyuu.Enabled = False
                    Me.TextHosyouNasiRiyuu.Style(EarthConst.STYLE_BACK_GROUND_COLOR) = CSS_COLOR_GRAY
                End If
            End If
        End If

        '****************************
        '* 請求先/仕入先情報をユーザコントロールにセット（報告書再発行）
        '****************************
        Dim strUriageZumi As String = String.Empty    '売上処理済み判断フラグ用
        Dim strViewMode As String = String.Empty

        '売上処理済判断フラグの取得
        strUriageZumi = cl.GetUriageSyoriZumiFlg(Me.SpanShUriageSyorizumi.InnerHtml)
        strViewMode = cl.GetViewMode(strUriageZumi, userInfo.KeiriGyoumuKengen)

        '参照モードの設定
        If Me.SelectShSeikyuuUmu.Style("display") = "none" Then
            strViewMode = EarthConst.MODE_VIEW
        End If

        Me.ucSeikyuuSiireLinkSai.SetVariableValueCtrlFromParent(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                                    , Me.TextShSyouhinCd.Text _
                                                                    , Me.HiddenDefaultSiireSakiCdForLink.Value _
                                                                    , strUriageZumi _
                                                                    , _
                                                                    , _
                                                                    , _
                                                                    , strViewMode)

        '****************************
        '* 請求先/仕入先情報をユーザコントロールにセット（解約払戻）
        '****************************
        '表示モードの初期化
        strViewMode = String.Empty

        '売上処理済判断フラグの取得
        strUriageZumi = cl.GetUriageSyoriZumiFlg(Me.SpanShUriageSyorizumi.InnerHtml)
        If Me.SpanKyKaiyakuUriageSyorizumi.InnerText = EarthConst.URIAGE_ZUMI Then
            strUriageZumi = EarthConst.URIAGE_ZUMI_CODE
        Else
            strUriageZumi = String.Empty
        End If
        strViewMode = cl.GetViewMode(strUriageZumi, userInfo.KeiriGyoumuKengen)

        '参照モードの設定
        If Me.SelectKyKaiyakuHaraimodosiSinseiUmu.Style("display") = "none" Then
            strViewMode = EarthConst.MODE_VIEW
        End If

        Me.ucSeikyuuSiireLinkKai.SetVariableValueCtrlFromParent(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                                    , Me.TextKySyouhinCd.Text _
                                                                    , Me.HiddenDefaultSiireSakiCdForLink.Value _
                                                                    , strUriageZumi _
                                                                    , _
                                                                    , _
                                                                    , _
                                                                    , strViewMode)

    End Sub

    ''' <summary>
    ''' 業務共通[ユーザーコントロール]ロード時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ucGyoumuKyoutuu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ucGyoumuKyoutuu.Load

        '最終更新者、最終更新日時をセット
        TextSaisyuuKousinsya.Text = ucGyoumuKyoutuu.AccLastupdateusernm.Value
        TextSaisyuuKousinDate.Text = ucGyoumuKyoutuu.AccLastupdatedatetime.Value

        '****************************
        '* 活性化制御
        '****************************

        '初期起動時のみ
        If IsPostBack = False Then
            '画面制御
            SetEnableControl()
            SetEnableControlHhDate(Me.HiddenBukkenJyky.Value)

        End If

        '保証業務権限
        If userInfo.HosyouGyoumuKengen = 0 Then
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable
            htNotTarget.Add(ucGyoumuKyoutuu.AccDataHakiSyubetu.ID, ucGyoumuKyoutuu.AccDataHakiSyubetu) 'データ破棄種別
            htNotTarget.Add(ucGyoumuKyoutuu.AccDataHakiDate.ID, ucGyoumuKyoutuu.AccDataHakiDate) 'データ破棄日
            htNotTarget.Add(SelectHannyouNo.ID, SelectHannyouNo) '汎用NO
            jSM.Hash2Ctrl(UpdatePanelHosyou, EarthConst.MODE_VIEW, ht, htNotTarget)

            '登録ボタン
            ButtonTouroku1.Disabled = True
            ButtonTouroku2.Disabled = True
            ButtonHkKousin.Disabled = True
            ButtonHakkouCancel.Disabled = True
            ButtonHakkouSet.Disabled = True
        End If

        '破棄権限
        If userInfo.DataHakiKengen = 0 Then
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable
            jSM.Hash2Ctrl(ucGyoumuKyoutuu.AccTdDataHakiSyubetu, EarthConst.MODE_VIEW, ht) 'データ破棄種別
            jSM.Hash2Ctrl(ucGyoumuKyoutuu.AccTdDataHakiData, EarthConst.MODE_VIEW, ht) 'データ破棄日

        End If

        '加盟店コードが可変になったので、チェックは初期起動時のみ行なう
        If IsPostBack = False Then
            '共通情報の入力チェック
            If ucGyoumuKyoutuu.AccKameitenCd.Value = "" Then '加盟店コード
                Dim tmpScript As String = ""

                '登録ボタンの非活性化
                ButtonTouroku1.Disabled = True
                ButtonTouroku2.Disabled = True
                ButtonHkKousin.Disabled = True
                ButtonHakkouCancel.Disabled = True
                ButtonHakkouSet.Disabled = True

                tmpScript = "alert('" & Messages.MSG065W & "');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ucGyoumuKyoutuu_Load", tmpScript, True)

                '地盤画面共通クラス
                Dim noTarget As New Hashtable
                Dim jSM As New JibanSessionManager
                Dim ht As New Hashtable
                Dim htNotTarget As New Hashtable
                htNotTarget.Add(SelectHannyouNo.ID, SelectHannyouNo) '汎用NO

                '全てのコントロールを無効化
                jSM.Hash2Ctrl(UpdatePanelHosyou, EarthConst.MODE_VIEW, ht, htNotTarget)
            End If
        End If

        '破棄種別チェック
        If ucGyoumuKyoutuu.AccDataHakiSyubetu.SelectedValue <> "0" Then
            '破棄種別が設定されている場合、すべてのコントロールを無効化
            CheckHakiDisable(True)
        End If

        'デフォルトフォーカス
        If ButtonTouroku1.Disabled <> True Then
            SetFocus(ButtonTouroku1)
        End If

    End Sub

#End Region

#Region "イベント"
    ''' <summary>
    ''' 登録/修正 実行ボタン１,２押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTourokuSyuuseiJikkou_ServerClick(ByVal sender As System.Object, _
                                                         ByVal e As System.EventArgs) _
                                                         Handles ButtonTouroku1.ServerClick, _
                                                                 ButtonTouroku2.ServerClick
        Dim tmpScript As String = ""

        ' 入力チェック
        '共通情報
        If ucGyoumuKyoutuu.checkInput() = False Then Exit Sub

        '保証
        If checkInput() = False Then Exit Sub

        If SaveData() Then '登録成功
            cl.CloseWindow(Me)

        Else '登録失敗
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "登録/修正") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonTouroku_ServerClick", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' 発行セット
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHakkouSet_ServerClick(ByVal sender As System.Object, _
                                              ByVal e As System.EventArgs) _
                                              Handles ButtonHakkouSet.ServerClick

        ' 1 入力内容のチェック
        If Not CheckHakIraiUketuke() Then
            ' チェックエラーなら終了
            Exit Sub
        End If

        ' 2 値の自動セット(CheckIraiUketuke() の結果反映)
        If HiddenHakkouSetTo.Value = "1" Then
            ' 保証書発行日が空白 => 画面．保証書発行日に画面．セット発行日をセット
            TextHosyousyoHakkouDate.Text = TextSetHakkouDate.Text
            TextHosyousyoHakkouDate_TextChanged(sender, e)
        ElseIf HiddenHakkouSetTo.Value = "2" Then
            ' 保証書再発行日が空白 => 画面．保証書再発行日に画面．セット発行日をセット
            TextSaihakkouDate.Text = TextSetHakkouDate.Text
            TextSaihakkouDate_TextChanged(sender, e)
        ElseIf HiddenHakkouSetTo.Value = "3" Then
            ' 再発行商品コードが空白
            ' => 画面．保証書再発行日に画面．セット発行日をセット
            TextSaihakkouDate.Text = TextSetHakkouDate.Text
            ' => 画面．再発行理由に画面．再発行理由 + “3通目”をセット
            TextSaihakkouRiyuu.Text = TextSaihakkouRiyuu.Text + " 2度目の再発行あり"
            ' => 画面．保証書再発行料請求有りの状態セット
            SelectShSeikyuuUmu.Text = "1"
            ' => フラグセット
            Me.ucSeikyuuSiireLinkSai.AccHiddenChkSeikyuuSakiChg.Value = "1"
            TextSaihakkouDate_TextChanged(sender, e)
        Else
            ' いずれにも該当しない場合
            ' => 画面．保証書再発行日に画面．セット発行日をセット
            TextSaihakkouDate.Text = TextSetHakkouDate.Text
            TextSaihakkouDate_TextChanged(sender, e)
        End If

        ' 地盤テーブル.発行依頼受付日時にシステム日時をセット
        HiddenHakIraiUketukeFlg.Value = "1"

        ' 3 受付セットボタンとキャンセルボタンを非活性
        ButtonHakkouSet.Disabled = True
        ButtonHakkouCancel.Disabled = True
        ButtonBukkenTenki.Disabled = True
        ButtonJyuusyoTenki.Disabled = True
        ButtonHosyouKaisiDateTenki.Disabled = True

        ' 4 確認メッセージを表示する
        Dim tmpScript As String = ""
        tmpScript = "alert('" & Messages.MSG207S & "');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ucGyoumuKyoutuu_Load", tmpScript, True)

        Me.UpdatePanelHosyou.Update()
        '商品毎の請求・仕入先が変更されていないかをチェックし、
        '変更されている場合情報の再取得
        setSeikyuuSiireHenkou(sender, e)

        ' 
        ' SetFuhoSyoumeisyoFlg()
        ' SetEnableControl()
    End Sub

    ''' <summary>
    ''' 業務共通[ユーザーコントロール]のucGyoumuKyoutuu_OyaGamenAction_hensyuで呼ばれる処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ucGyoumuKyoutuu_OyaGamenAction_hensyu(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal blnDisabled As Boolean) Handles ucGyoumuKyoutuu.OyaGamenAction_hensyu
        'コントロールの有効/無効
        CheckHakiDisable(blnDisabled)
    End Sub

    ''' <summary>
    ''' 破棄種別によって、コントロールの有効/無効を切替える
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckHakiDisable(Optional ByVal blnFlg As Boolean = False)
        '地盤画面共通クラス
        Dim jBn As New Jiban

        '有効化、無効化の対象外にするコントロール郡
        Dim noTarget As New Hashtable
        noTarget.Add(divHakkouIrai, True) '発行依頼タブ
        noTarget.Add(divHosyou, True) '保証タブ
        noTarget.Add(divKairyouKouji, True) '改良工事タブ
        noTarget.Add(divSaihakkou, True) '再発行タブ
        noTarget.Add(ButtonTouroku1.ID, True) '登録ボタン1
        noTarget.Add(ButtonTouroku2.ID, True) '登録ボタン2
        noTarget.Add(ButtonHkKousin.ID, True) '更新ボタン
        ' noTarget.Add(ButtonBukkenTenki.ID, True) ' 物件転記ボタン
        ' noTarget.Add(ButtonJyuusyoTenki.ID, True) ' 住所転記ボタン
        ' noTarget.Add(ButtonHosyouKaisiDateTenki.ID, True) ' 開始日転記ボタン()
        ' noTarget.Add(ButtonHakkouCancel.ID, True) ' 発行キャンセルボタン
        ' noTarget.Add(ButtonHakkouSet.ID, True) ' 発行セットボタン

        If blnFlg Then
            '全てのコントロールを無効化()
            jBn.ChangeDesabledAll(divHakkouIrai, True, noTarget)
            jBn.ChangeDesabledAll(divHosyou, True, noTarget)
            jBn.ChangeDesabledAll(divKairyouKouji, True, noTarget)
            jBn.ChangeDesabledAll(divSaihakkou, True, noTarget)
        Else
            '全てのコントロールを有効化()
            jBn.ChangeDesabledAll(divHakkouIrai, False, noTarget)
            jBn.ChangeDesabledAll(divHosyou, False, noTarget)
            jBn.ChangeDesabledAll(divKairyouKouji, False, noTarget)
            jBn.ChangeDesabledAll(divSaihakkou, False, noTarget)
        End If

    End Sub

    ''' <summary>
    ''' 発行依頼受付チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckHakIraiUketuke()
        Dim e As New System.EventArgs
        Dim tmpScript As String = ""

        '地盤画面共通クラス
        Dim jBn As New Jiban

        'エラーメッセージ初期化
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New ArrayList

        ' 保証書発行日／再発行日／商品コードが変更されている
        If HiddenHosyousyoHakkouDateMae.Value <> TextHosyousyoHakkouDate.Text Or _
           Me.HiddenSaihakkouDateOld.Value <> Me.TextSaihakkouDate.Text Or _
           Me.HiddenShSyouhinCdOld.Value <> TextShSyouhinCd.Text Then
            errMess += Messages.MSG208E.Trim
            arrFocusTargetCtrl.Add(Me.TextShSyouhinCd)
        End If

        ' 加盟店注意事項種別55があり、かつ画面．保証書発行日が空白かつ画面．保証開始日が空白の場合
        If kameitenlogic.ChkBuilderData55(Me.ucGyoumuKyoutuu.AccKameitenCd.Value) And _
           TextHosyousyoHakkouDate.Text = "" And _
           TextHosyouKaisiDate.Text = "" Then
            errMess += Messages.MSG209E.Trim
            arrFocusTargetCtrl.Add(Me.TextHosyousyoHakkouDate)
        End If

        ' セット発行日が空白の場合
        If TextSetHakkouDate.Text = "" Then
            errMess += Messages.MSG210E.Trim
            arrFocusTargetCtrl.Add(Me.TextSetHakkouDate)
        ElseIf Date.Compare(Date.Parse(TextSetHakkouDate.Text), Date.Today) = -1 Then
            ' セット発行日が過去日付の場合
            errMess += Messages.MSG211E.Trim
            arrFocusTargetCtrl.Add(Me.TextSetHakkouDate)
        End If

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If errMess <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        'エラー無しの場合、Trueを返す
        Return True

    End Function


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
    Private Function checkInput() As Boolean
        Dim e As New System.EventArgs

        '地盤画面共通クラス
        Dim jBn As New Jiban

        '全てのコントロールを有効化(破棄種別チェック用)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        'エラーメッセージ初期化
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New ArrayList

        If ucGyoumuKyoutuu.AccDataHakiSyubetu.SelectedValue >= "1" Then
            '破棄種別が選択されている場合、スルー
        Else

            '入力有りの場合
            If Me.TextYoteiKameitenCd.Value <> String.Empty Then
                '●コード入力値変更チェック
                '変更後検索ボタン押下チェック
                If (Me.TextYoteiKameitenCd.Value <> Me.HiddenYoteiKameitenCdTextMae.Value) Or _
                    (Me.TextYoteiKameitenCd.Value <> String.Empty And Me.TextYoteiKameitenMei.Value = String.Empty) Then
                    errMess += Messages.MSG030E.Replace("@PARAM1", "変更予定加盟店コード")
                    arrFocusTargetCtrl.Add(Me.TextYoteiKameitenCd)
                End If
            End If

            '●必須チェック
            '******************************
            '* <保証書再発行>
            '******************************
            '(Chk17:<保証画面><保証書再発行>保証書再発行日＝入力の場合、チェックを行う。)
            If TextSaihakkouDate.Text <> "" Then
                '請求
                If Me.HiddenSaihakkouDateOld.Value = String.Empty Then '再発行日Old=未入力(初回の場合)
                ElseIf Me.HiddenSaihakkouDateOld.Value <> String.Empty AndAlso Me.HiddenSaihakkouDateOld.Value = Me.TextSaihakkouDate.Text Then '再発行日=入力(同日の場合、初回扱い)
                Else '再発行日が異なる場合、未選択はエラー
                    If Me.SelectShSeikyuuUmu.SelectedValue = String.Empty Then
                        errMess += Messages.MSG013E.Replace("@PARAM1", "請求")
                        arrFocusTargetCtrl.Add(SelectShSeikyuuUmu)
                    End If
                End If
                '請求
                If Me.TextSaihakkouRiyuu.Text <> String.Empty Then '再発行理由=入力
                    If Me.SelectShSeikyuuUmu.SelectedValue = String.Empty Then
                        errMess += Messages.MSG153E.Replace("@PARAM1", "再発行理由").Replace("@PARAM2", "請求")
                        arrFocusTargetCtrl.Add(SelectShSeikyuuUmu)
                    End If
                Else
                    '再発行理由
                    If Me.SelectShSeikyuuUmu.SelectedValue <> String.Empty Then
                        errMess += Messages.MSG153E.Replace("@PARAM1", "請求").Replace("@PARAM2", "再発行理由")
                        arrFocusTargetCtrl.Add(TextSaihakkouRiyuu)
                    End If
                End If

                '請求有無
                If SelectShSeikyuuUmu.SelectedValue = "1" Then '有
                    '実請求税抜金額
                    If TextShJituseikyuuKingaku.Text.Length = 0 Then
                        errMess += Messages.MSG013E.Replace("@PARAM1", "実請求税抜金額")
                        arrFocusTargetCtrl.Add(TextShJituseikyuuKingaku)
                    End If
                    '請求書発行日
                    If TextShSeikyuusyoHakkouDate.Text.Length = 0 Then
                        errMess += Messages.MSG013E.Replace("@PARAM1", "請求書発行日")
                        arrFocusTargetCtrl.Add(TextShSeikyuusyoHakkouDate)
                    End If

                End If

            End If

        End If

        '●日付チェック
        'データ破棄日
        If ucGyoumuKyoutuu.AccDataHakiDate.Value <> "" Then
            If cl.checkDateHanni(ucGyoumuKyoutuu.AccDataHakiDate.Value) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "データ破棄日")
                arrFocusTargetCtrl.Add(ucGyoumuKyoutuu.AccDataHakiDate)
            End If
        End If
        '基礎報告書着日
        If TextKisoHoukokusyoTyakuDate.Text <> "" Then
            If cl.checkDateHanni(TextKisoHoukokusyoTyakuDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "基礎報告書着日")
                arrFocusTargetCtrl.Add(TextKisoHoukokusyoTyakuDate)
            End If
        End If
        '発行依頼書着日
        If TextHakkouIraiTyakuDate.Text <> "" Then
            If cl.checkDateHanni(TextHakkouIraiTyakuDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "発行依頼書着日")
                arrFocusTargetCtrl.Add(TextHakkouIraiTyakuDate)
            End If
        End If
        '保証書発行日
        If TextHosyousyoHakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextHosyousyoHakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "保証書発行日")
                arrFocusTargetCtrl.Add(TextHosyousyoHakkouDate)
            End If
        End If
        '業務完了日
        If TextGyoumuKanryouDate.Text <> "" Then
            If cl.checkDateHanni(TextGyoumuKanryouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "業務完了日")
                arrFocusTargetCtrl.Add(TextGyoumuKanryouDate)
            End If
        End If


        '****************
        '* 保証書再発行
        '****************
        '保証開始日
        If TextHosyouKaisiDate.Text <> "" Then
            If cl.checkDateHanni(TextHosyouKaisiDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "保証開始日")
                arrFocusTargetCtrl.Add(TextHosyouKaisiDate)
            End If
        End If
        '再発行日
        If TextSaihakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextSaihakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "再発行日")
                arrFocusTargetCtrl.Add(TextSaihakkouDate)
            End If
        End If
        '請求書発行日
        If TextShSeikyuusyoHakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextShSeikyuusyoHakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "請求書発行日")
                arrFocusTargetCtrl.Add(TextShSeikyuusyoHakkouDate)
            End If
        End If

        '****************
        '* 解約払戻
        '****************
        '請求書発行日
        If TextKySeikyuusyoHakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextKySeikyuusyoHakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "請求書発行日")
                arrFocusTargetCtrl.Add(TextKySeikyuusyoHakkouDate)
            End If
        End If

        '●桁数チェック(なし)

        '●禁則文字チェック(文字列入力フィールドが対象)
        '契約NO
        If jBn.KinsiStrCheck(TextKeiyakuNo.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "契約NO")
            arrFocusTargetCtrl.Add(TextKeiyakuNo)
        End If
        '保証なし理由
        If jBn.KinsiStrCheck(TextHosyouNasiRiyuu.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "保証なし理由")
            arrFocusTargetCtrl.Add(TextHosyouNasiRiyuu)
        End If
        '再発行理由
        If jBn.KinsiStrCheck(TextSaihakkouRiyuu.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "再発行理由")
            arrFocusTargetCtrl.Add(TextSaihakkouRiyuu)
        End If

        '●バイト数チェック(文字列入力フィールドが対象)
        '契約NO
        If jBn.ByteCheckSJIS(TextKeiyakuNo.Text, TextKeiyakuNo.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "契約NO")
            arrFocusTargetCtrl.Add(TextKeiyakuNo)
        End If
        '保証なし理由
        If jBn.ByteCheckSJIS(TextHosyouNasiRiyuu.Text, TextHosyouNasiRiyuu.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "保証なし理由")
            arrFocusTargetCtrl.Add(TextHosyouNasiRiyuu)
        End If
        '再発行理由
        If jBn.ByteCheckSJIS(TextSaihakkouRiyuu.Text, TextSaihakkouRiyuu.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "再発行理由")
            arrFocusTargetCtrl.Add(TextSaihakkouRiyuu)
        End If

        '●その他チェック
        '地盤T.保証商品有無="無"
        If Me.HiddenHosyouSyouhinUmu.Value <> "1" Then
            '保証書発行状況.保証あり
            If cbLogic.GetHosyousyoHakJykyHosyouUmu(Me.SelectHosyousyoHakkouJyoukyou.SelectedValue) = "1" Then
                errMess += Messages.MSG145E.Replace("@PARAM1", "商品設定状況").Replace("@PARAM2", EarthConst.DISP_HOSYOU_NASI_HOSYOU_SYOUHIN_UMU).Replace("@PARAM3", "保証書発行状況")
                arrFocusTargetCtrl.Add(Me.SelectHosyousyoHakkouJyoukyou)
            End If
        End If
        '******************************
        '* <保証情報>
        '******************************
        '(Chk19:変更予定加盟店テキストボックスが入力済み、かつ、物件の加盟店と変更予定加盟店が異なる、かつ、保証書発行日が入力済みの場合、エラーとする。)
        If Me.TextYoteiKameitenCd.Value <> String.Empty _
            AndAlso Me.TextYoteiKameitenCd.Value <> ucGyoumuKyoutuu.AccKameitenCd.Value _
            AndAlso Me.TextHosyousyoHakkouDate.Text <> String.Empty Then

            errMess += Messages.MSG203E
            arrFocusTargetCtrl.Add(Me.TextHosyousyoHakkouDate)
        End If

        '******************************
        '* <解約払戻>
        '******************************
        '(Chk16:請求有無＝有り、かつ、売上年月日＝設定済、かつ、請求書発行日＝未入力の場合、エラーとする。)
        '請求書発行日
        If SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "1" And TextKyUriageNengappi.Text <> "" And TextKySeikyuusyoHakkouDate.Text = "" Then
            errMess += Messages.MSG062E.Replace("@PARAM1", "請求書発行日")
            arrFocusTargetCtrl.Add(TextKySeikyuusyoHakkouDate)
        End If

        '******************************
        '* <改良工事>
        '******************************
        '(Chk03:<保証画面>保証書発行日＜[地盤テーブル]調査実施日、かつ、保証画面で登録許可していない場合、確認メッセージを表示する。
        '確認OKの場合、登録許可する。（2度目からは、確認メッセージを表示せずに、無条件で登録許可する）
        '保証書発行日=>JSにてチェック

        '(Chk04:<保証画面>保証書発行日＝入力、[地盤テーブル]調査実施日＝未入力、かつ、保証画面で登録許可していない場合、確認メッセージを表示する。
        '確認OKの場合、登録許可する。（2度目からは、確認メッセージを表示せずに、無条件で登録許可する）
        '保証書発行日=>JSにてチェック

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If errMess <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & errMess & "');"
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
    Private Function SaveData() As Boolean
        '*************************
        '地盤データは更新対象のみ
        '邸別請求データは全て更新
        '物件履歴データは保証商品有無の変更がある場合、登録
        '*************************
        Dim JibanLogic As New JibanLogic
        Dim jrOld As New JibanRecord
        ' 現在の地盤データをDBから取得する
        jrOld = JibanLogic.GetJibanData(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou)

        ' 画面内容より地盤レコードを生成する
        Dim jibanRec As New JibanRecordHosyou

        jibanRec = Me.GetCtrlDataRecord()

        '物件履歴データの自動セット
        Dim brRec As BukkenRirekiRecord = cbLogic.MakeBukkenRirekiRecHosyouUmu(jibanRec, cl.GetDisplayString(jrOld.HosyouSyouhinUmu), cl.GetDisplayString(jrOld.KeikakusyoSakuseiDate))

        If Not brRec Is Nothing Then
            '物件履歴レコードのチェック
            Dim strErrMsg As String = String.Empty
            If cbLogic.checkInputBukkenRireki(brRec, strErrMsg) = False Then
                MLogic.AlertMessage(Me, strErrMsg, 0, "ErrBukkenRireki")
                Exit Function
            End If
        End If
        '===========================================================
        ' データの更新を行います
        If MyLogic.SaveJibanData(Me, jibanRec, brRec) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 変更予定加盟店検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonYoteiKameitenSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonYoteiKameitenSearch.ServerClick

        If yoteiKameitenSearchType.Value <> "1" Then
            kameitenSearchSub(sender, e)
        Else
            'コードonchangeで呼ばれた場合
            kameitenSearchSub(sender, e, False)
            yoteiKameitenSearchType.Value = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' 変更予定加盟店検索ボタン押下時の処理(実体)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="callWindow">検索ポップアップを起動するか否かの指定</param>
    ''' <remarks></remarks>
    Protected Sub kameitenSearchSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        ' 入力されているコードをキーに、マスタを検索し、データを1件のみ取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim kLogic As New KameitenSearchLogic
        Dim dataArray As New List(Of KameitenSearchRecord)
        Dim total_count As Integer = 0
        Dim blnTorikesi As Boolean

        '加盟店コード=入力の場合
        If Me.TextYoteiKameitenCd.Value <> String.Empty Then

            'DB値と同じ場合、｢取消=0｣を条件に含めない
            If Me.TextYoteiKameitenCd.Value = Me.HiddenYoteiKameitenCdTextOld.Value Then
                blnTorikesi = False
            Else
                '業務共通Ctrlの加盟店コードと同じ場合、｢取消=0｣を条件に含めない
                If Me.TextYoteiKameitenCd.Value = ucGyoumuKyoutuu.AccKameitenCdTextOld.Value Then

                    blnTorikesi = False
                Else
                    blnTorikesi = True
                End If
            End If

            '検索を実行
            dataArray = kLogic.GetKameitenSearchResult(Me.ucGyoumuKyoutuu.AccHiddenKubun.Value _
                                                        , Me.TextYoteiKameitenCd.Value _
                                                        , blnTorikesi _
                                                        , total_count)
        End If

        If total_count = 1 Then

            '加盟店コードを入れ直す
            Dim recData As KameitenSearchRecord = dataArray(0)
            Me.TextYoteiKameitenCd.Value = recData.KameitenCd

            'フォーカスセット
            setFocusAJ(Me.ButtonYoteiKameitenSearch)
        Else
            If callWindow = True Then
                '加盟店名
                Me.TextYoteiKameitenMei.Value = String.Empty

                '検索画面表示用JavaScript『callSearch』を実行
                Dim tmpFocusScript = "objEBI('" & ButtonYoteiKameitenSearch.ClientID & "').focus();"
                Dim tmpScript As String = "callSearch('" & Me.ucGyoumuKyoutuu.AccHiddenKubun.ClientID & EarthConst.SEP_STRING & Me.TextYoteiKameitenCd.ClientID & _
                                                "','" & UrlConst.SEARCH_KAMEITEN & _
                                                "','" & Me.TextYoteiKameitenCd.ClientID & EarthConst.SEP_STRING & Me.TextYoteiKameitenMei.ClientID & _
                                                "','" & Me.ButtonYoteiKameitenSearch.ClientID & "');"


                tmpScript = tmpFocusScript + tmpScript
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSearch", tmpScript, True)
            End If
        End If

        ' 加盟店検索実行後処理実行
        kameitenSearchAfter_ServerClick(sender, e, blnTorikesi)

    End Sub

    ''' <summary>
    ''' 加盟店検索実行後処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="blnTorikesi">取消対象フラグ</param>
    ''' <remarks></remarks>
    Protected Sub kameitenSearchAfter_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal blnTorikesi As Boolean)

        '加盟店検索実行後処理(加盟店詳細情報、ビルダー情報取得)
        Dim kLogic As New KameitenSearchLogic
        Dim recData As KameitenSearchRecord = kLogic.GetKameitenSearchResult(Me.ucGyoumuKyoutuu.AccHiddenKubun.Value, Me.TextYoteiKameitenCd.Value, "", blnTorikesi)
        Dim strErrMsg As String = String.Empty

        If Me.TextYoteiKameitenCd.Value <> String.Empty Then    '入力
            If Not recData Is Nothing AndAlso recData.KameitenCd <> String.Empty Then
                '加盟店情報をセット
                Me.SetKameitenInfo(recData)
            Else
                'クリアを行なう
                ClearKameitenInfo(False)
            End If
        Else    '未入力
            ClearKameitenInfo()
        End If

    End Sub

    ''' <summary>
    ''' 加盟店情報をセットする
    ''' </summary>
    ''' <param name="recData"></param>
    ''' <remarks></remarks>
    Private Sub SetKameitenInfo(ByVal recData As KameitenSearchRecord)

        If Not recData Is Nothing AndAlso recData.KameitenCd <> String.Empty Then
            '画面に値をセット
            Me.TextYoteiKameitenCd.Value = cl.GetDispStr(recData.KameitenCd)    '加盟店・コード
            Me.TextYoteiKameitenMei.Value = cl.GetDispStr(recData.KameitenMei1) '加盟店・名称

            '加盟店コードを退避
            Me.HiddenYoteiKameitenCdTextMae.Value = Me.TextYoteiKameitenCd.Value
        End If

    End Sub

    ''' <summary>
    ''' 加盟店情報のクリアを行なう
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearKameitenInfo(Optional ByVal blnFlg As Boolean = True)

        '初期化処理
        If blnFlg Then
            Me.TextYoteiKameitenCd.Value = String.Empty
        End If
        Me.TextYoteiKameitenMei.Value = String.Empty

        'フォーカスの設定
        Me.setFocusAJ(Me.ButtonYoteiKameitenSearch)
    End Sub

    ''' <summary>
    ''' 更新ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHkKousin_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHkKousin.ServerClick
        '保証書管理テーブル更新日時
        Dim dtHkUpdDatetime As DateTime
        dtHkUpdDatetime = DateTime.ParseExact(HiddenHkUpdDatetime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)

        '保証書管理テーブル追加/更新処理
        If BJykyLogic.setHosyousyoKanriBukken(sender, ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou, dtHkUpdDatetime) Then

            '保証書管理データ取得
            Dim hr As New HosyousyoKanriRecord
            hr = BJykyLogic.getSearchKeyDataRec(sender, ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou)

            '画面に設定
            If Not hr Is Nothing Then
                '物件状況
                SetBukkenJyky(hr.BukkenJyky)
                ' 更新日時
                HiddenHkUpdDatetime.Value = IIf(hr.UpdDateTime = DateTime.MinValue, Format(hr.AddDateTime, EarthConst.FORMAT_DATE_TIME_1), Format(hr.UpdDateTime, EarthConst.FORMAT_DATE_TIME_1))
                ' 業務完了日
                TextGyoumuKanryouDate.Text = cl.GetDisplayString(hr.GyoumuKanryDate)
                ' 業務開始内容
                TextGyoumuKaisiNaiyou.Text = cl.GetDisplayString(hr.GyoumuKaisiNaiyou)

                MLogic.AlertMessage(Me, Messages.MSG018S.Replace("@PARAM1処理", "物件状況の最新化"), 0)
            End If
        Else
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1処理", "物件状況の最新化"), 1)
        End If

        '物件加盟店コードの活性化制御
        ucGyoumuKyoutuu.SetEnableKameiten(Me.HiddenBukkenJyky.Value)

        setFocusAJ(ButtonHkKousin) 'フォーカス
    End Sub

#End Region

#Region "初期設定メソッド"
    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispAction()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 他システムへのリンクボタン設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '加盟店注意事項
        cl.getKameitenTyuuijouhouPath(Me.TextYoteiKameitenCd.ClientID, Me.ButtonYoteiKameitenTyuuijouhou)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript関連セット
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim onFocusPostBackScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScript As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this)){__doPostBack(this.id,'');}}else{checkNumberAddFig(this);}"
        Dim onFocusScript As String = "removeFig(this);"
        Dim onBlurScript As String = "checkNumberAddFig(this);"
        Dim checkDate As String = "checkDate(this);"
        Dim checkKingaku As String = "checkKingaku(this);"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '日付項目用
        Dim onFocusPostBackScriptDate As String = "setTempValueForOnBlur(this);"
        Dim onBlurPostBackScriptDate As String = "if(checkTempValueForOnBlur(this)){if(checkDate(this)){__doPostBack(this.id,'');}}else{checkDate(this);}"

        '変更予定加盟店コード
        Me.TextYoteiKameitenCd.Attributes("onfocus") = onFocusPostBackScript
        Me.TextYoteiKameitenCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callYoteiKameitenSearch(this);}else{checkNumber(this);}"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 金額系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '***********************
        '* 再発行
        '***********************
        '工務店請求税抜金額
        TextShKoumutenSeikyuuKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextShKoumutenSeikyuuKingaku.Attributes("onblur") = onBlurPostBackScript
        TextShKoumutenSeikyuuKingaku.Attributes("onkeydown") = disabledOnkeydown
        '実請求税抜金額
        TextShJituseikyuuKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextShJituseikyuuKingaku.Attributes("onblur") = onBlurPostBackScript
        TextShJituseikyuuKingaku.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 日付系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '***********************
        '* 発行依頼
        '***********************
        'セット発行日
        TextSetHakkouDate.Attributes("onblur") = checkDate
        TextSetHakkouDate.Attributes("onkeydown") = disabledOnkeydown

        '***********************
        '* 保証
        '***********************
        '基礎報告書着日
        TextKisoHoukokusyoTyakuDate.Attributes("onblur") = checkDate
        TextKisoHoukokusyoTyakuDate.Attributes("onkeydown") = disabledOnkeydown
        '発行依頼着日
        TextHakkouIraiTyakuDate.Attributes("onblur") = checkDate
        TextHakkouIraiTyakuDate.Attributes("onkeydown") = disabledOnkeydown
        '業務完了日
        TextGyoumuKanryouDate.Attributes("onblur") = checkDate
        TextGyoumuKanryouDate.Attributes("onkeydown") = disabledOnkeydown
        '保証書発行日
        TextHosyousyoHakkouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextHosyousyoHakkouDate.Attributes("onfocus") = onFocusPostBackScriptDate & "SetChangeMaeValue('" & HiddenHosyousyoHakkouDateMae.ClientID & "','" & TextHosyousyoHakkouDate.ClientID & "');"
        TextHosyousyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown
        '保証開始日
        TextHosyouKaisiDate.Attributes("onblur") = checkDate
        TextHosyouKaisiDate.Attributes("onkeydown") = disabledOnkeydown

        '***********************
        '* 再発行
        '***********************
        '再発行日
        TextSaihakkouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextSaihakkouDate.Attributes("onfocus") = onFocusPostBackScriptDate & "SetChangeMaeValue('" & HiddenSaihakkouDateMae.ClientID & "','" & TextSaihakkouDate.ClientID & "');"
        TextSaihakkouDate.Attributes("onkeydown") = disabledOnkeydown
        '請求書発行日
        TextShSeikyuusyoHakkouDate.Attributes("onblur") = checkDate
        TextShSeikyuusyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown

        '***********************
        '* 解約払戻
        '***********************
        '請求書発行日
        TextKySeikyuusyoHakkouDate.Attributes("onblur") = checkDate
        TextKySeikyuusyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ドロップダウンリスト
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '請求有無
        SelectShSeikyuuUmu.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenShSeikyuuUmuMae.ClientID & "','" & SelectShSeikyuuUmu.ClientID & "')"
        '解約払戻申請有無
        SelectKyKaiyakuHaraimodosiSinseiUmu.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenKyKaiyakuHaraimodosiSinseiUmuMae.ClientID & "','" & SelectKyKaiyakuHaraimodosiSinseiUmu.ClientID & "')"
        '保証書発行状況
        Me.SelectHosyousyoHakkouJyoukyou.Attributes("onfocus") = "SetChangeMaeValue('" & Me.HiddenHosyousyoHakJyKyMae.ClientID & "','" & Me.SelectHosyousyoHakkouJyoukyou.ClientID & "')"
        ''付保証明書FLG(保証書関連対応にてコメントアウト)
        'SelectFuhoSyoumeisyoFlg.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenFuhoSyoumeisyoFlgMae.ClientID & "','" & SelectFuhoSyoumeisyoFlg.ClientID & "')"
        '保証なし理由
        Me.SelectHosyouNasiRiyuu.Attributes("onchange") += "checkPullDown();"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 機能別テーブルの表示切替
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '発行依頼情報
        AncHakkouIraiInfo.HRef = "JavaScript:changeDisplay('" & TbodyHakkouIraiInfo.ClientID & "');SetDisplayStyle('" & HiddenHakkouIraiInfoStyle.ClientID & "','" & TbodyHakkouIraiInfo.ClientID & "');"
        '保証情報
        AncHosyouInfo.HRef = "JavaScript:changeDisplay('" & TbodyHoshoInfo.ClientID & "');SetDisplayStyle('" & HiddenHosyouInfoStyle.ClientID & "','" & TbodyHoshoInfo.ClientID & "');"
        '改良工事
        AncKairyouKouji.HRef = "JavaScript:changeDisplay('" & TbodyKairyoKouji.ClientID & "');SetDisplayStyle('" & HiddenKairyouKoujiStyle.ClientID & "','" & TbodyKairyoKouji.ClientID & "');"
        '再発行
        AncSaiHakkou.HRef = "JavaScript:changeDisplay('" & TbodySaiHakkou.ClientID & "');SetDisplayStyle('" & HiddenSaiHakkouStyle.ClientID & "','" & TbodySaiHakkou.ClientID & "');"

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
        Dim tmpTouroku As String = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this);}else{return false;}"
        Dim tmpScript As String = "if(CheckTouroku()){" & tmpTouroku & "}else{return false;}"
        Dim tmpScript2 As String = "if(objEBI('" & HiddenAjaxFlg.ClientID & "').value!=1){" & tmpScript & "}else{alert('" & Messages.MSG104E & "');}"
        Dim tmpScript3 As String = "if(CheckIraiUketuke()==false){return false;}"

        '登録許可MSG確認後、OKの場合DB更新処理を行なう
        ButtonTouroku1.Attributes("onclick") = tmpScript
        ButtonTouroku2.Attributes("onclick") = tmpScript
        ButtonHkKousin.Attributes("onclick") = "if(confirm('" & Messages.MSG017C & "')){}else{return false;}"
        ButtonHakkouCancel.Attributes("onclick") = "if(confirm('" & Messages.MSG215C & "')){}else{return false;}"
        ButtonHakkouSet.Attributes("onclick") = tmpScript3
        ButtonHosyouKaisiDateTenki.Attributes("onclick") = "if(CheckHosyouKaisiDate()==false){return false;}"

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

#End Region

#Region "地盤データをコントロールに設定する"
    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByRef jr As JibanRecordBase)

        Dim jSM As New JibanSessionManager 'セッション管理クラス
        Dim jBn As New Jiban '地盤画面クラス
        Dim logic As New HosyouLogic '保証ロジッククラス
        Dim kisoSiyouLogic As New KisoSiyouLogic ' 基礎仕様ロジッククラス
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim kameitenRec As New KameitenSearchRecord

        ' コンボ設定ヘルパークラスを生成
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        '******************************************
        '* 画面コントロールに設定
        '******************************************
        '加盟店コード
        Me.HiddenKameitenCd.Value = cl.GetDisplayString(jr.KameitenCd)

        '***********************
        '* 保証情報
        '***********************
        ' 契約NO
        TextKeiyakuNo.Text = cl.GetDisplayString(jr.KeiyakuNo)
        ' 調査実施日
        TextTyousaJissiDate.Text = cl.GetDisplayString(jr.TysJissiDate)
        ' 計画書作成日
        TextKeikakusyoSakuseiDate.Text = cl.GetDisplayString(jr.KeikakusyoSakuseiDate)
        ' 入金確認条件
        TextNyuukinKakuninJyouken.Text = cl.GetDisplayString(jr.NyuukinKakuninJyoukenMei)
        ' 基礎報告書
        SelectKisoHoukokusyo.SelectedValue = cl.GetDisplayString(jr.KsHkksUmu)
        ' 基礎報告着日
        TextKisoHoukokusyoTyakuDate.Text = cl.GetDisplayString(jr.KsKojKanryHkksTykDate)

        ' 変更予定加盟店コード
        If cl.GetDisplayString(jr.Kbn) <> "" And cl.GetDisplayString(jr.HenkouYoteiKameitenCd) <> "" Then

            kameitenRec = kameitenSearchLogic.GetKameitenSearchResult(jr.Kbn, _
                                                                      jr.HenkouYoteiKameitenCd, _
                                                                      jr.TysKaisyaCd & jr.TysKaisyaJigyousyoCd, _
                                                                      False)
            '加盟店情報をセット
            Me.SetKameitenInfo(kameitenRec)

            '加盟店コードのDB値を退避
            Me.HiddenYoteiKameitenCdTextOld.Value = Me.TextYoteiKameitenCd.Value
        End If

        ' 発行依頼書
        SelectHakkouIraisyo.SelectedValue = _
            cl.GetDisplayString(IIf(jr.HosyousyoHakIraisyoUmu = 1, "1", ""))
        ' 発行依頼着日
        TextHakkouIraiTyakuDate.Text = cl.GetDisplayString(jr.HosyousyoHakIraisyoTykDate)
        ' 保証書発行状況
        SelectHosyousyoHakkouJyoukyou.SelectedValue = _
            cl.GetDisplayString(jr.HosyousyoHakJyky)

        Dim strTmpVal As String = String.Empty
        Dim strTmpMei As String = String.Empty

        '存在チェック(自動設定等の未存在データは生成)
        If cl.ChkDropDownList(Me.SelectHosyousyoHakkouJyoukyou, cl.GetDispNum(jr.HosyousyoHakJyky)) Then
            Me.SelectHosyousyoHakkouJyoukyou.SelectedValue = cl.GetDispNum(jr.HosyousyoHakJyky, "")
        ElseIf jr.HosyousyoHakJyky = 0 Then

            '保証あり
            strTmpVal = EarthConst.AUTO_SET_VAL_HOSYOU_ARI
            strTmpMei = cbLogic.GetHosyousyoHakJykyMei(strTmpVal)

            Me.SelectHosyousyoHakkouJyoukyou.Items.Add(New ListItem(strTmpVal & ":" & strTmpMei, strTmpVal))

            '保証なし
            strTmpVal = EarthConst.AUTO_SET_VAL_HOSYOU_NASI
            strTmpMei = cbLogic.GetHosyousyoHakJykyMei(strTmpVal)

            Me.SelectHosyousyoHakkouJyoukyou.Items.Add(New ListItem(strTmpVal & ":" & strTmpMei, strTmpVal))

            Me.SelectHosyousyoHakkouJyoukyou.SelectedValue = EarthConst.AUTO_SET_VAL_HOSYOU_NASI  '選択状態
        ElseIf jr.HosyousyoHakJyky > 0 Then

            strTmpVal = cl.GetDispNum(jr.HosyousyoHakJyky, "")
            strTmpMei = cbLogic.GetHosyousyoHakJykyMei(strTmpVal)

            Me.SelectHosyousyoHakkouJyoukyou.Items.Add(New ListItem(strTmpVal & ":" & strTmpMei, strTmpVal))
            Me.SelectHosyousyoHakkouJyoukyou.SelectedValue = strTmpVal  '選択状態
        End If

        ' 保証書発行状況設定日
        TextHosyousyoHakkouJyoukyouSetteiDate.Text = _
            cl.GetDisplayString(jr.HosyousyoHakJykySetteiDate)
        ' 保証書発行日
        TextHosyousyoHakkouDate.Text = cl.GetDisplayString(jr.HosyousyoHakkouDate)
        ' 保証書発送日
        TextHosyousyoHassouDate.Text = cl.GetDisplayString(jr.HosyousyoHassouDate)
        ' 発行依頼方法
        Dim strHosyousyoHakHouhou As String = cl.GetDisplayString(jr.HosyousyoHakHouhou)
        If strHosyousyoHakHouhou = "0" Then
            Me.spanHosyousyoHakHouhou.InnerHtml = EarthConst.HINSITU_HOSYOUSYO_JYOUKYOU_HAKKOUTIMING_IRAISYO ' 依頼書
        ElseIf strHosyousyoHakHouhou = "1" Then
            Me.spanHosyousyoHakHouhou.InnerHtml = EarthConst.HINSITU_HOSYOUSYO_JYOUKYOU_HAKKOUTIMING_JIDOU ' 自動発行
        ElseIf strHosyousyoHakHouhou = "2" Then
            Me.spanHosyousyoHakHouhou.InnerHtml = EarthConst.HINSITU_HOSYOUSYO_JYOUKYOU_HAKKOUTIMING_JIBANMALL ' 地盤モール
        Else
            Me.spanHosyousyoHakHouhou.InnerHtml = ""
        End If

        ' 付保証明書FLG
        SelectFuhoSyoumeisyoFlg.SelectedValue = cl.ChgStrToInt(jr.FuhoSyoumeisyoFlg)
        ' 付保証明書発送日
        TextFuhoSyoumeisyoHassouDate.Text = cl.GetDisplayString(jr.FuhoSyoumeisyoHassouDate)
        ' 保証開始日
        TextHosyouKaisiDate.Text = cl.GetDisplayString(jr.HosyouKaisiDate)
        ' 商品設定状況
        Dim strHosyouSyouhinUmu As String = cl.GetDisplayString(jr.HosyouSyouhinUmu)
        If strHosyouSyouhinUmu = "1" Then
            Me.SpanHosyouSyouhinUmu.InnerHtml = EarthConst.DISP_HOSYOU_ARI_HOSYOU_SYOUHIN_UMU
            Me.SpanHosyouSyouhinUmu.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_BLUE
        Else
            Me.SpanHosyouSyouhinUmu.InnerHtml = EarthConst.DISP_HOSYOU_NASI_HOSYOU_SYOUHIN_UMU
            Me.SpanHosyouSyouhinUmu.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
        End If
        ' 保証有無
        SelectHosyouUmu.SelectedValue = cl.GetDisplayString(IIf(jr.HosyouUmu = 1, "1", ""))
        ' 保証期間
        TextHosyouKikan.Text = cl.GetDisplayString(jr.HosyouKikan)
        ' 保証書発行日 NOT NULLの場合、保証期間は非活性
        If TextHosyousyoHakkouDate.Text <> "" Then
            cl.chgVeiwMode(TextHosyouKikan) ' 非活性
        End If

        ' 保証なし理由コード
        SelectHosyouNasiRiyuu.SelectedValue = cl.GetDisplayString(jr.HosyouNasiRiyuuCd)
        ' 汎用NO(非表示)
        SelectHannyouNo.SelectedIndex = SelectHosyouNasiRiyuu.SelectedIndex
        ' 保証なし理由テキストボックス
        TextHosyouNasiRiyuu.Text = cl.GetDisplayString(jr.HosyouNasiRiyuu)
        ' 保証なし理由コード変更時イベント(JS)を実行
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectHosyouNasiRiyuu_SelectChanged", "checkPullDown();", True)

        If Not jr.Syouhin1Record Is Nothing Then
            ' (調査)請求書発行日
            TextTyousaSeikyuusyoHakkouDate.Text = _
                cl.GetDisplayString(jr.Syouhin1Record.SeikyuusyoHakDate)
        End If

        ' 調査発注書合計金額
        TextTyousaHattyuusyoGoukeiKingaku.Text = IIf(jr.GetSyouhinHattyusyoKingaku() = 0, _
                                                                    "0", _
                                                                    cl.GetDisplayString(jr.GetSyouhinHattyusyoKingaku()) _
                                                                    )

        ' 調査合計入金額(税込) 
        TextTyousaGoukeiNyuukingaku.Text = IIf(jr.getNyuukinGaku("100") + jr.getNyuukinGaku("120") = 0, _
                                                    "0", _
                                                    cl.GetDisplayString(jr.getNyuukinGaku("100") + jr.getNyuukinGaku("120"), "0") _
                                                    )

        ' 残額
        Dim zangaku As Integer = ( _
                                  jr.getZeikomiGaku(New String() {"100", "110", "180"}) - _
                                  jr.getNyuukinGaku("100")) + (jr.getZeikomiGaku(New String() {"120"}) - _
                                  jr.getNyuukinGaku("120") _
                                 )

        TextTyousaZangaku.Text = zangaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        '●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetMeisyouDropDownList(objDrpTmp, EarthConst.emMeisyouType.SYASIN_JURI)
        objDrpTmp.SelectedValue = cl.ChgStrToInt(jr.TikanKoujiSyasinJuri)
        TextSyasinJuri.Text = objDrpTmp.SelectedItem.Text '置換工事写真受理
        TextSyasinComment.Text = cl.GetDispStr(jr.TikanKoujiSyasinComment) '置換工事写真コメント

        ' 商品１はレコードの有無に関わらず表示しておく（原則必須なので）
        If Not jr.Syouhin1Record Is Nothing Then
            TextSyouhin1A.Text = cl.GetDisplayString(jr.Syouhin1Record.SyouhinCd)           ' 商品１．コード
            TextSyouhin1B.Text = cl.GetDisplayString(jr.Syouhin1Record.SyouhinMei)          ' 商品１．名称
            HiddenSyouhin1SeikyuuHakkouDate.Value = cl.GetDisplayString(jr.Syouhin1Record.SeikyuusyoHakDate) ' 商品１．請求書発行日
        End If

        ' 商品２
        SetSyouhin2Ctrl(logic, jr.Syouhin2Records)

        ' 商品３
        SetSyouhin3Ctrl(logic, jr.Syouhin3Records)

        '***********************
        '* 改良工事
        '***********************
        ' 判定者 (担当者名)
        TextHanteisya.Text = cl.GetDisplayString(jr.TantousyaMei)
        ' 判定種別
        TextHanteiSyubetu.Text = cl.GetDisplayString(kisoSiyouLogic.GetHanteiSyunetuDisp(jr.HanteiCd1, jr.HanteiCd2))

        ' 判定の設定
        SetHanteiData(logic, jr.HanteiCd1, jr.HanteiSetuzokuMoji, jr.HanteiCd2)

        If jr.KojGaisyaCd <> "" And jr.KojGaisyaJigyousyoCd <> "" Then '工事会社コード＋工事会社事業所コード
            '工事会社名
            TextKoujiGaisya.Text = tyousakaisyaSearchLogic.GetTyousaKaisyaMei(jr.KojGaisyaCd, jr.KojGaisyaJigyousyoCd, False)
        End If

        ' 改良工事種別
        TextKairyouKoujiSyubetu.Text = cl.GetDisplayString(jr.KairyKojSyubetuMei)
        ' 改良工事日
        TextKairyouKoujiDate.Text = cl.GetDisplayString(jr.KairyKojDate)

        If Not jr.KairyouKoujiRecord Is Nothing Then
            ' (工事)請求書発行日
            TextKoujiSeikyusyoHakkouDate.Text = _
               cl.GetDisplayString(jr.KairyouKoujiRecord.SeikyuusyoHakDate)
        End If
        ' 工事報告書受理
        TextKoujiHoukokusyoJuri.Text = cl.GetDisplayString(jr.HkksJyuriJyky)
        ' 工事報告書受理日
        TextKoujiHoukokusyoJuriDate.Text = cl.GetDisplayString(jr.KojHkksJuriDate)
        ' 工事報告書発送日
        TextKoujiHoukokusyoHassouDate.Text = cl.GetDisplayString(jr.KojHkksHassouDate)
        ' 工事発注書合計金額 
        TextKoujiHattyuusyoGoukeiKingaku.Text = IIf(jr.GetKoujiHattyusyoKingaku() = 0, _
                                                        "0", _
                                                        cl.GetDisplayString(jr.GetKoujiHattyusyoKingaku(), "0") _
                                                        )

        ' 工事合計入金額(税込)
        TextKoujiGoukeiNyuukingaku.Text = IIf(jr.getNyuukinGaku("130") + jr.getNyuukinGaku("140") = 0, _
                                                    "0", _
                                                    cl.GetDisplayString(jr.getNyuukinGaku("130") + jr.getNyuukinGaku("140"), "0") _
                                                    )

        ' 残額
        Dim KoujiZangaku As Integer
        Dim kojZangaku As Integer
        Dim tKojZangaku As Integer

        ' 工事会社請求の場合、工事代金残額（税込売上金額 - 入金額）はつねに0円
        If jr.KojGaisyaSeikyuuUmu = 1 Then
            kojZangaku = 0
        Else
            kojZangaku = jr.getZeikomiGaku(New String() {"130"}) - jr.getNyuukinGaku("130")
        End If
        ' 追加工事会社請求の場合、工事代金残額（税込売上金額 - 入金額）はつねに0円
        If jr.TKojKaisyaSeikyuuUmu = 1 Then
            tKojZangaku = 0
        Else
            tKojZangaku = jr.getZeikomiGaku(New String() {"140"}) - jr.getNyuukinGaku("140")
        End If
        KoujiZangaku = kojZangaku + tKojZangaku
        TextKoujiZangaku.Text = KoujiZangaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        If Not jr.KairyouKoujiRecord Is Nothing Then
            ' 工事商品．コード
            TextKoujiSyouhinCd.Text = cl.GetDisplayString(jr.KairyouKoujiRecord.SyouhinCd)
            ' 工事商品．名称
            TextKoujiSyouhinMei.Text = cl.GetDisplayString(jr.KairyouKoujiRecord.SyouhinMei)
        End If

        If jr.KojGaisyaSeikyuuUmu = 1 Then
            ' 工事商品．工事会社請求
            SpanKoujiGaisyaSeikyuu.InnerHtml = _
                cl.GetDisplayString(EarthConst.KOUJIGAISYA_SEIKYUU)
        End If

        If Not jr.TuikaKoujiRecord Is Nothing Then
            ' 追加工事商品.コード
            TextTuikaKoujiSyouhinCd.Text = cl.GetDisplayString(jr.TuikaKoujiRecord.SyouhinCd)
            ' 追加工事商品.名称
            TextTuikaKoujiSyouhinMei.Text = cl.GetDisplayString(jr.TuikaKoujiRecord.SyouhinMei)
        End If

        If jr.TKojKaisyaSeikyuuUmu = 1 Then
            ' 追加工事商品．工事会社請求
            SpanTuikaKoujiKaisyaSeikyuu.InnerHtml = _
                cl.GetDisplayString(EarthConst.KOUJIGAISYA_SEIKYUU)
        End If

        '***********************
        '* 再発行(地盤テーブル)
        '***********************
        ' 再発行日
        TextSaihakkouDate.Text = cl.GetDisplayString(jr.HosyousyoSaihakDate)

        '***********************
        '* 再発行(邸別請求テーブル)
        '***********************
        If Not jr.HosyousyoRecord Is Nothing Then

            '邸別請求情報をコントロールにセット
            SetCtrlTeibetuSeikyuuDataSh(jr.HosyousyoRecord)

            '邸別入金情報をコントロールにセット
            If jr.TeibetuNyuukinRecords IsNot Nothing Then
                ' 入金額/残額をセット
                CalcZangakuSh(jr.getZeikomiGaku(New String() {"170"}), jr.getNyuukinGaku("170"))
            Else
                ' 入金額/残額をセット
                SetKingaku(EnumKingakuType.Saihakkou, True)
            End If

        End If

        '***********************
        '* 解約払戻(地盤テーブル)
        '***********************
        ' 解約払戻．返金処理済
        SpanKyHenkinSyorizumi.InnerHtml = _
            cl.GetDisplayString(IIf(jr.HenkinSyoriFlg = 1, EarthConst.HENKIN_SYORI_ZUMI, ""))
        ' 解約払戻．返金処理日
        SpanKyHenkinSyoriDate.InnerHtml = _
            cl.GetDisplayString(jr.HenkinSyoriDate)

        '***********************
        '* 解約払戻(邸別請求テーブル)
        '***********************
        If Not jr.KaiyakuHaraimodosiRecord Is Nothing Then

            '邸別請求情報をコントロールにセット
            SetCtrlTeibetuSeikyuuDataKy(jr.KaiyakuHaraimodosiRecord)

        End If

        '****************************
        '* Hidden項目
        '****************************
        '保証書発行日の画面制御用
        '調査発注書確認日チェック
        SetTyousaHattyuusyoKakuninDateFlg(jr)

        '工事発注書確認日チェック
        SetKoujiHattyuusyoKakuninDateFlg(jr)

        '保証商品有無
        Me.HiddenHosyouSyouhinUmu.Value = cl.GetDisplayString(jr.HosyouSyouhinUmu)

        '****************************
        '* Hidden項目(コントロールの値変更前)
        '****************************
        '保証書発行日
        HiddenHosyousyoHakkouDateMae.Value = TextHosyousyoHakkouDate.Text
        '再発行日
        HiddenSaihakkouDateMae.Value = TextSaihakkouDate.Text
        '再発行日Old
        Me.HiddenSaihakkouDateOld.Value = Me.TextSaihakkouDate.Text
        '請求有無
        HiddenShSeikyuuUmuMae.Value = SelectShSeikyuuUmu.SelectedValue
        '解約払戻申請有無
        HiddenKyKaiyakuHaraimodosiSinseiUmuMae.Value = SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue
        '保証書発行状況
        HiddenHosyousyoHakJyKyMae.Value = Me.SelectHosyousyoHakkouJyoukyou.SelectedValue
        '保証期間Old
        Me.HiddenHosyouKikanOld.Value = Me.TextHosyouKikan.Text
        ' 発行依頼受付日時(地盤)
        Me.HiddenHakIraiUkeDatetimeOld.Value = IIf(cl.GetDisplayString(jr.HakIraiUkeDatetime) = "", "", Format(jr.HakIraiUkeDatetime, EarthConst.FORMAT_DATE_TIME_1))
        ' 発行依頼キャンセル日時(地盤)
        Me.HiddenHakIraiCanDatetimeOld.Value = IIf(cl.GetDisplayString(jr.HakIraiCanDatetime) = "", "", Format(jr.HakIraiCanDatetime, EarthConst.FORMAT_DATE_TIME_1))

        '****************************
        '* Hidden項目(登録許可確認)
        '****************************
        '調査実施日
        HiddenTyousaJissiDateOld.Value = TextTyousaJissiDate.Text
        '調査会社コード・調査会社事業所コード
        HiddenDefaultSiireSakiCdForLink.Value = (jr.TysKaisyaCd + jr.TysKaisyaJigyousyoCd)

        '****************************
        '* セッションに画面情報を格納
        '****************************
        jSM.Ctrl2Hash(Me, jBn.IraiData)
        iraiSession.Irai1Data = jBn.IraiData

        '●保証書発行状況による保証商品有無の表示切替
        Me.ChgDispHosyouUmu()

    End Sub

#Region "保証書管理データをコントロールに設定する"
    ''' <summary>
    ''' 地盤レコードと保証書管理レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <param name="hr">保証書管理レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromHosyouRec(ByVal jr As JibanRecordBase, ByVal hr As HosyousyoKanriRecord)
        ' コンボ設定ヘルパークラスを生成
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        Dim strBukkenJyky As String = cl.ChkHosyousyoBukkenJyky(jr)

        ' 物件状況
        SetBukkenJyky(strBukkenJyky)

        ' 保証書タイプ
        ' ●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.HOSYOUSYO_TYPE, True, False)
        objDrpTmp.SelectedValue = cl.ChgStrToInt(hr.HosyousyoType)
        TextHosyousyoType.Text = objDrpTmp.SelectedItem.Text

        '保険会社
        ' ●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.HokenKaisya, True, False)
        objDrpTmp.SelectedValue = cl.ChgStrToInt(hr.HokenKaisya)
        SpanHkHokenGaisya.InnerText = objDrpTmp.SelectedItem.Text

        ' 引渡し前保険
        ' ●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.HW_HKN, True, False)
        objDrpTmp.SelectedValue = cl.ChgStrToInt(hr.HwMaeHkn)
        TextHwMaeHkn.Text = objDrpTmp.SelectedItem.Text

        ' 引渡し後保険
        ' ●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.HW_HKN, True, False)
        objDrpTmp.SelectedValue = cl.ChgStrToInt(hr.HwAtoHkn)
        TextHwAtohkn.Text = objDrpTmp.SelectedItem.Text

        ' 更新日時
        HiddenHkUpdDatetime.Value = IIf(hr.UpdDateTime = DateTime.MinValue, Format(hr.AddDateTime, EarthConst.FORMAT_DATE_TIME_1), Format(hr.UpdDateTime, EarthConst.FORMAT_DATE_TIME_1))
        ' 業務完了日
        TextGyoumuKanryouDate.Text = cl.GetDisplayString(hr.GyoumuKanryDate)
        ' 業務開始内容
        TextGyoumuKaisiNaiyou.Text = cl.GetDisplayString(hr.GyoumuKaisiNaiyou)

    End Sub

    ''' <summary>
    ''' 物件状況を設定
    ''' </summary>
    ''' <param name="strBukkenJyky">物件状況</param>
    ''' <remarks></remarks>
    Public Sub SetBukkenJyky(ByVal strBukkenJyky As String)
        ' コンボ設定ヘルパークラスを生成
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        ' 物件状況
        ' ●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.BUKKEN_JYKY, True, False)
        objDrpTmp.SelectedValue = strBukkenJyky
        SpanBukkenJyky.InnerText = objDrpTmp.SelectedItem.Text

        'スタイル変更
        If strBukkenJyky = "0" OrElse strBukkenJyky = "2" Then
            Me.SpanBukkenJyky.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
            Me.SpanBukkenJyky.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
        Else '初期化
            Me.SpanBukkenJyky.Style(EarthConst.STYLE_FONT_COLOR) = String.Empty
            Me.SpanBukkenJyky.Style(EarthConst.STYLE_FONT_WEIGHT) = String.Empty
        End If
        Me.HiddenBukkenJyky.Value = strBukkenJyky

        ' 保証発行日の画面制御
        SetEnableControlHhDate(strBukkenJyky)

    End Sub

#End Region

#Region "邸別請求データをコントロールにセットする"

    ''' <summary>
    ''' 保証書再発行/邸別請求レコード
    ''' </summary>
    ''' <param name="TeibetuRec"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlTeibetuSeikyuuDataSh( _
                                    ByVal TeibetuRec As TeibetuSeikyuuRecord _
                                    )

        ' 再発行理由
        TextSaihakkouRiyuu.Text = cl.GetDisplayString(TeibetuRec.Bikou)

        ' 売上処理済
        SpanShUriageSyorizumi.InnerHtml = _
            cl.GetDisplayString(IIf(TeibetuRec.UriKeijyouDate = DateTime.MinValue, _
                                   "", _
                                   EarthConst.URIAGE_ZUMI))
        ' 売上計上日
        Me.HiddenShUriageKeijyouDate.Value = cl.GetDisplayString(TeibetuRec.UriKeijyouDate)
        ' 請求有無
        SelectShSeikyuuUmu.SelectedValue = cl.GetDisplayString(TeibetuRec.SeikyuuUmu)
        ' 商品コード
        TextShSyouhinCd.Text = cl.GetDisplayString(TeibetuRec.SyouhinCd)
        Me.HiddenShSyouhinCdOld.Value = TextShSyouhinCd.Text ' 変更前
        ' 商品名
        SpanShSyouhinMei.InnerHtml = cl.GetDisplayString(TeibetuRec.SyouhinMei)
        ' 工務店請求税抜金額
        TextShKoumutenSeikyuuKingaku.Text = IIf(TeibetuRec.KoumutenSeikyuuGaku = Integer.MinValue, 0, Format(TeibetuRec.KoumutenSeikyuuGaku, EarthConst.FORMAT_KINGAKU_1))
        ' 税率（Hidden）
        HiddenShZeiritu.Value = TeibetuRec.Zeiritu
        ' 税区分（Hidden）
        HiddenShZeiKbn.Value = IIf(TeibetuRec.ZeiKbn = "", "", TeibetuRec.ZeiKbn)
        If TeibetuRec.Zeiritu = 0 And TeibetuRec.ZeiKbn = "" Then
            SetShZeiInfo(TeibetuRec.SyouhinCd)
        End If

        '*****************
        '* 【売上金額】
        '*****************
        '●売上消費税額(消費税)
        TextShSyouhizei.Text = IIf(TeibetuRec.UriageSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.UriageSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税抜売上金額(実請求税抜金額)
        TextShJituseikyuuKingaku.Text = IIf(TeibetuRec.UriGaku = Integer.MinValue, 0, Format(TeibetuRec.UriGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税込売上金額(税込額)
        TextShZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiUriGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiUriGaku, EarthConst.FORMAT_KINGAKU_1))

        '*****************
        '* 【仕入金額】
        '*****************
        '仕入金額
        Me.HiddenShSiireGaku.Value = IIf(TeibetuRec.SiireGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireGaku, EarthConst.FORMAT_KINGAKU_1))
        '●仕入消費税額
        Me.HiddenShSiireSyouhiZei.Value = IIf(TeibetuRec.SiireSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))

        ' 請求書発行日
        TextShSeikyuusyoHakkouDate.Text = _
            cl.GetDisplayString(TeibetuRec.SeikyuusyoHakDate)
        ' 売上年月日
        TextShUriageNengappi.Text = cl.GetDisplayString(TeibetuRec.UriDate)
        ' 発注書確定
        If TeibetuRec.HattyuusyoKakuteiFlg = 1 Then
            TextShHattyuusyoKakutei.Text = EarthConst.KAKUTEI
        ElseIf TeibetuRec.HattyuusyoKakuteiFlg = 0 Then
            TextShHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
        End If
        ' 発注書金額 
        TextShHattyuusyoKingaku.Text = _
            cl.GetDisplayString(TeibetuRec.HattyuusyoGaku)
        ' 発注書確認日
        TextShHattyuusyoKakuninDate.Text = _
            cl.GetDisplayString(TeibetuRec.HattyuusyoKakuninDate)

        '請求先/仕入先リンクへセット
        Me.ucSeikyuuSiireLinkSai.SetSeikyuuSiireLinkFromTeibetuRec(TeibetuRec)

        '請求先タイプの取得設定
        Me.TextShSeikyuusaki.Text = cl.GetSeikyuuSakiTypeStr( _
                                                        TeibetuRec.SyouhinCd _
                                                        , EarthEnum.EnumSyouhinKubun.Hosyousyo _
                                                        , Me.HiddenKameitenCd.Value _
                                                        , TeibetuRec.SeikyuuSakiCd _
                                                        , TeibetuRec.SeikyuuSakiBrc _
                                                        , TeibetuRec.SeikyuuSakiKbn)

        '更新日時 読み込み時のタイムスタンプ(排他制御用)
        cl.setDispHaitaUpdTime(TeibetuRec.AddDatetime, TeibetuRec.UpdDatetime, Me.HiddenShUpdDateTime)

    End Sub

    ''' <summary>
    ''' 解約払戻/邸別請求レコード
    ''' </summary>
    ''' <param name="TeibetuRec"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlTeibetuSeikyuuDataKy( _
                                    ByVal TeibetuRec As TeibetuSeikyuuRecord _
                                    )

        ' 売上処理済
        SpanKyKaiyakuUriageSyorizumi.InnerHtml = _
            cl.GetDisplayString(IIf(TeibetuRec.UriKeijyouDate = DateTime.MinValue, _
                                   "", _
                                   EarthConst.URIAGE_ZUMI))
        ' 売上計上日
        Me.HiddenKyKaiyakuUriageKeijyouDate.Value = cl.GetDisplayString(TeibetuRec.UriKeijyouDate)

        ' 解約払戻申請有無
        SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = _
            cl.GetDisplayString(TeibetuRec.SeikyuuUmu)
        ' 商品コード
        TextKySyouhinCd.Text = _
            cl.GetDisplayString(TeibetuRec.SyouhinCd)
        ' 工務店請求税抜金額
        TextKyKoumutenSeikyuuKingaku.Text = IIf(TeibetuRec.KoumutenSeikyuuGaku = Integer.MinValue, 0, Format(TeibetuRec.KoumutenSeikyuuGaku, EarthConst.FORMAT_KINGAKU_1))
        ' 税率（Hidden）
        HiddenKyZeiritu.Value = TeibetuRec.Zeiritu
        ' 税区分（Hidden）
        HiddenKyZeiKbn.Value = IIf(TeibetuRec.ZeiKbn = "", "", TeibetuRec.ZeiKbn)
        If TeibetuRec.Zeiritu = 0 And TeibetuRec.ZeiKbn = "" Then
            SetKyZeiInfo(TeibetuRec.SyouhinCd)
        End If

        '*****************
        '* 【売上金額】
        '*****************
        '●売上消費税額(消費税)
        TextKySyouhizei.Text = IIf(TeibetuRec.UriageSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.UriageSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税抜売上金額(実請求税抜金額)
        TextKyJituseikyuuKingaku.Text = IIf(TeibetuRec.UriGaku = Integer.MinValue, 0, Format(TeibetuRec.UriGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税込売上金額(税込額)
        TextKyZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiUriGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiUriGaku, EarthConst.FORMAT_KINGAKU_1))

        '*****************
        '* 【仕入金額】
        '*****************
        '仕入金額
        Me.HiddenKySiireGaku.Value = IIf(TeibetuRec.SiireGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireGaku, EarthConst.FORMAT_KINGAKU_1))
        '●仕入消費税額
        Me.HiddenKySiireSyouhiZei.Value = IIf(TeibetuRec.SiireSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))

        ' 請求書発行日
        TextKySeikyuusyoHakkouDate.Text = _
            cl.GetDisplayString(TeibetuRec.SeikyuusyoHakDate)
        ' 売上年月日
        TextKyUriageNengappi.Text = _
            cl.GetDisplayString(TeibetuRec.UriDate)
        ' 発注書確定
        If TeibetuRec.HattyuusyoKakuteiFlg = 1 Then
            TextKyHattyuusyoKakutei.Text = EarthConst.KAKUTEI
        ElseIf TeibetuRec.HattyuusyoKakuteiFlg = 0 Then
            TextKyHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
        End If
        ' 発注書金額 
        TextKyHattyuusyoKingaku.Text = _
            cl.GetDisplayString(TeibetuRec.HattyuusyoGaku)
        ' 発注書確認日
        TextKyHattyuusyoKakuninDate.Text = _
            cl.GetDisplayString(TeibetuRec.HattyuusyoKakuninDate)

        '請求先/仕入先リンクへセット
        Me.ucSeikyuuSiireLinkKai.SetSeikyuuSiireLinkFromTeibetuRec(TeibetuRec)

        '請求先タイプの取得設定
        Me.TextKySeikyuusaki.Text = cl.GetSeikyuuSakiTypeStr( _
                                                        TeibetuRec.SyouhinCd _
                                                        , EarthEnum.EnumSyouhinKubun.Kaiyaku _
                                                        , Me.HiddenKameitenCd.Value _
                                                        , TeibetuRec.SeikyuuSakiCd _
                                                        , TeibetuRec.SeikyuuSakiBrc _
                                                        , TeibetuRec.SeikyuuSakiKbn)

        '更新日時 読み込み時のタイムスタンプ(排他制御用)
        cl.setDispHaitaUpdTime(TeibetuRec.AddDatetime, TeibetuRec.UpdDatetime, Me.HiddenKyUpdDateTime)

    End Sub

#End Region

#Region "商品２設定"
    ''' <summary>
    ''' 商品２レコードの情報をコントロールに設定する
    ''' </summary>
    ''' <param name="logic">保証ロジッククラス</param>
    ''' <param name="records">商品２レコード</param>
    ''' <remarks></remarks>
    Private Sub SetSyouhin2Ctrl(ByVal logic As HosyouLogic, _
                                ByVal records As Dictionary(Of Integer, TeibetuSeikyuuRecord))

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetSyouhin2Ctrl", _
                                            logic, _
                                            records)

        If records Is Nothing Then
            TrSyouhin21.Visible = False ' 商品２_１
            TrSyouhin22.Visible = False ' 商品２_２
            TrSyouhin23.Visible = False ' 商品２_３
            TrSyouhin24.Visible = False ' 商品２_４
        Else
            If Not records.ContainsKey(1) Then
                TrSyouhin21.Visible = False ' 商品２_１
            Else
                TextSyouhin21A.Text = cl.GetDisplayString(records.Item(1).SyouhinCd)     ' 商品２_１．コード
                TextSyouhin21B.Text = cl.GetDisplayString(records.Item(1).SyouhinMei)    ' 商品２_１．名称
            End If
            If Not records.ContainsKey(2) Then
                TrSyouhin22.Visible = False ' 商品２_２
            Else
                TextSyouhin22A.Text = cl.GetDisplayString(records.Item(2).SyouhinCd)     ' 商品２_２．コード
                TextSyouhin22B.Text = cl.GetDisplayString(records.Item(2).SyouhinMei)    ' 商品２_２．名称
            End If
            If Not records.ContainsKey(3) Then
                TrSyouhin23.Visible = False ' 商品２_３
            Else
                TextSyouhin23A.Text = cl.GetDisplayString(records.Item(3).SyouhinCd)     ' 商品２_３．コード
                TextSyouhin23B.Text = cl.GetDisplayString(records.Item(3).SyouhinMei)    ' 商品２_３．名称
            End If
            If Not records.ContainsKey(4) Then
                TrSyouhin24.Visible = False ' 商品２_４
            Else
                TextSyouhin24A.Text = cl.GetDisplayString(records.Item(4).SyouhinCd)     ' 商品２_４．コード
                TextSyouhin24B.Text = cl.GetDisplayString(records.Item(4).SyouhinMei)    ' 商品２_４．名称
            End If
        End If
    End Sub
#End Region

#Region "商品３設定"
    ''' <summary>
    ''' 商品３レコードの情報をコントロールに設定する
    ''' </summary>
    ''' <param name="logic">保証ロジッククラス</param>
    ''' <param name="records">商品３レコード</param>
    ''' <remarks></remarks>
    Private Sub SetSyouhin3Ctrl(ByVal logic As HosyouLogic, _
                                ByVal records As Dictionary(Of Integer, TeibetuSeikyuuRecord))

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetSyouhin3Ctrl", _
                                            logic, _
                                            records)

        If records Is Nothing Then
            TrSyouhin31.Visible = False ' 商品３_１
            TrSyouhin32.Visible = False ' 商品３_２
            TrSyouhin33.Visible = False ' 商品３_３
            TrSyouhin34.Visible = False ' 商品３_４
            TrSyouhin35.Visible = False ' 商品３_５
            TrSyouhin36.Visible = False ' 商品３_６
            TrSyouhin37.Visible = False ' 商品３_７
            TrSyouhin38.Visible = False ' 商品３_８
            TrSyouhin39.Visible = False ' 商品３_９
        Else
            If Not records.ContainsKey(1) Then
                TrSyouhin31.Visible = False ' 商品３_１
            Else
                TextSyouhin31A.Text = cl.GetDisplayString(records.Item(1).SyouhinCd)     ' 商品３_１．コード
                TextSyouhin31B.Text = cl.GetDisplayString(records.Item(1).SyouhinMei)    ' 商品３_１．名称
            End If
            If Not records.ContainsKey(2) Then
                TrSyouhin32.Visible = False ' 商品３_２
            Else
                TextSyouhin32A.Text = cl.GetDisplayString(records.Item(2).SyouhinCd)     ' 商品３_２．コード
                TextSyouhin32B.Text = cl.GetDisplayString(records.Item(2).SyouhinMei)    ' 商品３_２．名称
            End If
            If Not records.ContainsKey(3) Then
                TrSyouhin33.Visible = False ' 商品３_３
            Else
                TextSyouhin33A.Text = cl.GetDisplayString(records.Item(3).SyouhinCd)     ' 商品３_３．コード
                TextSyouhin33B.Text = cl.GetDisplayString(records.Item(3).SyouhinMei)    ' 商品３_３．名称
            End If
            If Not records.ContainsKey(4) Then
                TrSyouhin34.Visible = False ' 商品３_４
            Else
                TextSyouhin34A.Text = cl.GetDisplayString(records.Item(4).SyouhinCd)     ' 商品３_４．コード
                TextSyouhin34B.Text = cl.GetDisplayString(records.Item(4).SyouhinMei)    ' 商品３_４．名称
            End If
            If Not records.ContainsKey(5) Then
                TrSyouhin35.Visible = False ' 商品３_５
            Else
                TextSyouhin35A.Text = cl.GetDisplayString(records.Item(5).SyouhinCd)     ' 商品３_５．コード
                TextSyouhin35B.Text = cl.GetDisplayString(records.Item(5).SyouhinMei)    ' 商品３_５．名称
            End If
            If Not records.ContainsKey(6) Then
                TrSyouhin36.Visible = False ' 商品３_６
            Else
                TextSyouhin36A.Text = cl.GetDisplayString(records.Item(6).SyouhinCd)     ' 商品３_６．コード
                TextSyouhin36B.Text = cl.GetDisplayString(records.Item(6).SyouhinMei)    ' 商品３_６．名称
            End If
            If Not records.ContainsKey(7) Then
                TrSyouhin37.Visible = False ' 商品３_７
            Else
                TextSyouhin37A.Text = cl.GetDisplayString(records.Item(7).SyouhinCd)     ' 商品３_７．コード
                TextSyouhin37B.Text = cl.GetDisplayString(records.Item(7).SyouhinMei)    ' 商品３_７．名称
            End If
            If Not records.ContainsKey(8) Then
                TrSyouhin38.Visible = False ' 商品３_８
            Else
                TextSyouhin38A.Text = cl.GetDisplayString(records.Item(8).SyouhinCd)     ' 商品３_８．コード
                TextSyouhin38B.Text = cl.GetDisplayString(records.Item(8).SyouhinMei)    ' 商品３_８．名称
            End If
            If Not records.ContainsKey(9) Then
                TrSyouhin39.Visible = False ' 商品３_９
            Else
                TextSyouhin39A.Text = cl.GetDisplayString(records.Item(9).SyouhinCd)     ' 商品３_９．コード
                TextSyouhin39B.Text = cl.GetDisplayString(records.Item(9).SyouhinMei)    ' 商品３_９．名称
            End If
        End If
    End Sub
#End Region

#Region "保証書発行日の画面制御"

    ''' <summary>
    ''' 保証書発行日の画面制御/調査発注書確認日
    ''' </summary>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <remarks>※商品1～3レコードをチェック</remarks>
    Private Sub SetTyousaHattyuusyoKakuninDateFlg(ByVal jibanRec As JibanRecordBase)
        Dim strHattyuusyoKakuninDate As String = "" '発注書確認日

        If jibanRec Is Nothing Then Exit Sub

        Dim blnFlg As Boolean = True

        '調査発注書確認日フラグ
        HiddenTyousaHattyuusyoKakuninDateFlg.Value = "" '初期化

        With jibanRec

            '商品１
            If Not .Syouhin1Record Is Nothing Then
                If .Syouhin1Record.SeikyuuUmu = 1 Then
                    '発注書確認日
                    strHattyuusyoKakuninDate = cl.GetDisplayString(.Syouhin1Record.HattyuusyoKakuninDate)
                    If strHattyuusyoKakuninDate = String.Empty Then '未入力
                        blnFlg = False
                        Exit Sub '処理を抜ける
                    End If
                End If
            End If

            '商品２
            If Not .Syouhin2Records Is Nothing Then
                For Each de As KeyValuePair(Of Integer, TeibetuSeikyuuRecord) In jibanRec.Syouhin2Records
                    If Not .Syouhin2Records(de.Key) Is Nothing Then
                        If .Syouhin2Records(de.Key).SeikyuuUmu = 1 Then
                            '発注書確認日
                            strHattyuusyoKakuninDate = cl.GetDisplayString(.Syouhin2Records(de.Key).HattyuusyoKakuninDate)
                            If strHattyuusyoKakuninDate = String.Empty Then '未入力
                                blnFlg = False
                                Exit Sub '処理を抜ける
                            End If
                        End If
                    End If
                Next
            End If

            '商品３
            If Not .Syouhin3Records Is Nothing Then
                For Each de As KeyValuePair(Of Integer, TeibetuSeikyuuRecord) In jibanRec.Syouhin3Records
                    If Not .Syouhin3Records(de.Key) Is Nothing Then
                        If .Syouhin3Records(de.Key).SeikyuuUmu = 1 Then
                            '発注書確認日
                            strHattyuusyoKakuninDate = cl.GetDisplayString(.Syouhin3Records(de.Key).HattyuusyoKakuninDate)
                            If strHattyuusyoKakuninDate = String.Empty Then '未入力
                                blnFlg = False
                                Exit Sub '処理を抜ける
                            End If
                        End If
                    End If
                Next
            End If

            If blnFlg Then
                HiddenTyousaHattyuusyoKakuninDateFlg.Value = "1" '調査発注書確認日フラグ
            End If

        End With

    End Sub

    ''' <summary>
    ''' 保証書発行日の画面制御/工事発注書確認日
    ''' </summary>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <remarks>※改良工事レコードと追加工事レコードをチェック</remarks>
    Private Sub SetKoujiHattyuusyoKakuninDateFlg(ByVal jibanRec As JibanRecordBase)
        Dim strHattyuusyoKakuninDate As String = "" '発注書確認日

        If jibanRec Is Nothing Then Exit Sub

        Dim blnFlg As Boolean = True

        '工事発注書確認日フラグ
        HiddenKoujiHattyuusyoKakuninDateFlg.Value = "" '初期化

        With jibanRec

            '改良工事
            If Not .KairyouKoujiRecord Is Nothing Then
                If .KairyouKoujiRecord.SeikyuuUmu = 1 Then
                    '発注書確認日
                    strHattyuusyoKakuninDate = cl.GetDisplayString(.KairyouKoujiRecord.HattyuusyoKakuninDate)
                    If strHattyuusyoKakuninDate = String.Empty Then '未入力
                        blnFlg = False
                        Exit Sub '処理を抜ける
                    End If
                End If
            End If

            '追加工事
            If Not .TuikaKoujiRecord Is Nothing Then
                If .TuikaKoujiRecord.SeikyuuUmu = 1 Then
                    '発注書確認日
                    strHattyuusyoKakuninDate = cl.GetDisplayString(.TuikaKoujiRecord.HattyuusyoKakuninDate)
                    If strHattyuusyoKakuninDate = String.Empty Then '未入力
                        blnFlg = False
                        Exit Sub '処理を抜ける
                    End If
                End If
            End If

            If blnFlg Then
                HiddenKoujiHattyuusyoKakuninDateFlg.Value = "1" '工事発注書確認日フラグ
            End If

        End With

    End Sub

#End Region

#Region "判定内容の設定"
    ''' <summary>
    ''' 判定内容の設定
    ''' </summary>
    ''' <param name="logic"></param>
    ''' <param name="intHantei1Cd">判定１</param>
    ''' <param name="intHanteiSetuzokuMoji">判定接続詞</param>
    ''' <param name="intHantei2Cd">判定２</param>
    ''' <remarks></remarks>
    Private Sub SetHanteiData(ByVal logic As HosyouLogic, _
                              ByVal intHantei1Cd As Integer, _
                              ByVal intHanteiSetuzokuMoji As Integer, _
                              ByVal intHantei2Cd As Integer)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetHanteiData", _
                                            logic, _
                                            intHantei1Cd, _
                                            intHanteiSetuzokuMoji, _
                                            intHantei2Cd)

        ' 基礎仕様ロジッククラス
        Dim kisoSiyouLogic As New KisoSiyouLogic()

        SpanHantei1.InnerHtml = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(intHantei1Cd)) '判定１名

        SpanHanteiSetuzokuMoji.InnerHtml = cl.GetDisplayString(kisoSiyouLogic.GetKisoSiyouSetuzokusiMei(intHanteiSetuzokuMoji)) ' 判定接続詞

        SpanHantei2.InnerHtml = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(intHantei2Cd)) '判定２名

        '*********************
        '* Hidden項目
        '*********************
        HiddenHantei1CdOld.Value = intHantei1Cd

    End Sub
#End Region

#End Region

#Region "進捗データをコントロールに設定する"
    ''' <summary>
    ''' 進捗データから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromReportRec(ByRef jr As JibanRecordBase, ByRef rr As ReportIfGetRecord)

        '***********************
        '* フラグ
        '***********************
        Me.HiddenHakIraiUketukeFlg.Value = ""
        Me.HiddenHakIraiCancelFlg.Value = ""

        '***********************
        '* 発行依頼情報
        '***********************
        TextHakIraiTime.Text = IIf(cl.GetDisplayString(rr.HakIraiTime) = "", "", Format(rr.HakIraiTime, EarthConst.FORMAT_DATE_TIME_7) + " 依頼")
        ' 物件名称
        TextbukkenuMei.Text = rr.HakIraiBknName
        ' 物件所在地１/２/３
        TextBukkenuSyozai1.Text = rr.HakIraiBknAdr1
        TextBukkenuSyozai2.Text = rr.HakIraiBknAdr2
        TextBukkenuSyozai3.Text = rr.HakIraiBknAdr3
        ' セット発行日
        TextSetHakkouDate.Text = cl.GetDisplayString(Now.Date)
        ' お引渡し日
        TextHikiwatasiDate.Text = cl.GetDisplayString(rr.HakIraiHwDate)
        ' 担当者
        TextTantouSya.Text = rr.HakIraiTanto
        ' 連絡先
        TextRenrakuSaki.Text = rr.HakIraiTantoTel
        ' 入力ID
        TextNyuuryokuID.Text = rr.HakIraiLogin
        ' 発行依頼その他情報
        TextIraiSonota.Text = rr.HakIraiSonota

        Me.HiddenHakIraiTime.Value = cbLogic.GetDispStrDateTime(rr.HakIraiTime)
        Me.HiddenHakIraiUkeDatetimeR.Value = cbLogic.GetDispStrDateTime(rr.HakIraiUkeDatetime)
        Me.HiddenHakIraiCanDatetimeR.Value = cbLogic.GetDispStrDateTime(rr.HakIraiCanDatetime)

    End Sub

#End Region


#Region "コントロールの内容を地盤レコードにセットし取得する"
    ''' <summary>
    ''' 画面の内容を地盤レコードにセットし取得する
    ''' </summary>
    ''' <returns>コントロールの内容をセットした地盤レコード</returns>
    ''' <remarks></remarks>
    Protected Function GetCtrlDataRecord() As JibanRecordHosyou
        Dim JibanLogic As New JibanLogic
        Dim jr As New JibanRecord
        ' 現在の地盤データをDBから取得する
        jr = JibanLogic.GetJibanData(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou)

        Dim jibanRec As New JibanRecordHosyou

        '進捗T更新用に、DB上の値をセットする
        JibanLogic.SetSintyokuJibanData(jr, jibanRec)

        '商品1～3のコピー
        JibanLogic.ps_CopyTeibetuSyouhinData(jr, jibanRec)

        '***********************************************
        ' 地盤テーブル (共通情報)
        '***********************************************
        ' データ破棄種別
        cl.SetDisplayString(ucGyoumuKyoutuu.DataHakiSyubetu, jibanRec.DataHakiSyubetu)
        ' データ破棄日
        If ucGyoumuKyoutuu.DataHakiSyubetu = "0" Then
            jibanRec.DataHakiDate = DateTime.MinValue
        Else
            cl.SetDisplayString(ucGyoumuKyoutuu.DataHakiDate, jibanRec.DataHakiDate)
        End If

        ' 区分
        jibanRec.Kbn = ucGyoumuKyoutuu.Kubun
        ' 番号（保証書NO）
        jibanRec.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        ' 施主名    
        jibanRec.SesyuMei = ucGyoumuKyoutuu.SesyuMei
        ' 物件住所1,2,3
        jibanRec.BukkenJyuusyo1 = ucGyoumuKyoutuu.Jyuusyo1
        jibanRec.BukkenJyuusyo2 = ucGyoumuKyoutuu.Jyuusyo2
        jibanRec.BukkenJyuusyo3 = ucGyoumuKyoutuu.Jyuusyo3
        ' 備考,備考2
        jibanRec.Bikou = ucGyoumuKyoutuu.Bikou
        jibanRec.Bikou2 = ucGyoumuKyoutuu.Bikou2
        '調査方法コード
        jibanRec.TysHouhou = ucGyoumuKyoutuu.TyousaHouhouCd
        '調査方法名
        jibanRec.TysHouhouMeiIf = ucGyoumuKyoutuu.TyousaHouhouMei
        '調査会社コード
        jibanRec.TysKaisyaCd = ucGyoumuKyoutuu.TyousaKaishaCd
        '調査会社事業所コード
        jibanRec.TysKaisyaJigyousyoCd = ucGyoumuKyoutuu.TyousaKaishaJigyousyoCd
        '調査会社名
        jibanRec.TysKaisyaMeiIf = ucGyoumuKyoutuu.TyousaKaishaMei
        '加盟店コード
        jibanRec.KameitenCd = ucGyoumuKyoutuu.AccKameitenCd.Value
        '加盟店名
        jibanRec.KameitenMeiIf = ucGyoumuKyoutuu.KameitenMei
        '加盟店Tel
        jibanRec.KameitenTelIf = ucGyoumuKyoutuu.KameitenTel
        '加盟店Fax
        jibanRec.KameitenFaxIf = ucGyoumuKyoutuu.KameitenFax
        '加盟店Mail
        jibanRec.KameitenMailIf = ucGyoumuKyoutuu.KameitenMail
        '構造名
        jibanRec.KouzouMeiIf = ucGyoumuKyoutuu.KouzouMei

        '***********************************************
        ' 地盤テーブル (保証情報)
        '***********************************************
        ' 契約NO
        cl.SetDisplayString(TextKeiyakuNo.Text, jibanRec.KeiyakuNo)
        ' 基礎報告書有無
        cl.SetDisplayString(SelectKisoHoukokusyo.SelectedValue, jibanRec.KsHkksUmu)
        ' 基礎工事完了報告書着日
        cl.SetDisplayString(TextKisoHoukokusyoTyakuDate.Text, jibanRec.KsKojKanryHkksTykDate)
        '変更予定加盟店コード
        cl.SetDisplayString(TextYoteiKameitenCd.Value, jibanRec.HenkouYoteiKameitenCd)


        '************************************************
        ' 保証書発行状況、保証書発行状況設定日、保証商品有無の自動設定
        '************************************************
        '商品の自動設定後に行なう
        cbLogic.ps_AutoSetHosyousyoHakJyjy(EarthEnum.emGamenInfo.Hosyou, jibanRec, Me.SelectHosyousyoHakkouJyoukyou.SelectedValue)

        ' 保証書発行日
        cl.SetDisplayString(TextHosyousyoHakkouDate.Text, jibanRec.HosyousyoHakDate)
        ' 付保証明書FLG
        cl.SetDisplayString(SelectFuhoSyoumeisyoFlg.SelectedValue, jibanRec.FuhoSyoumeisyoFlg)
        ' 保証有無
        cl.SetDisplayString(SelectHosyouUmu.SelectedValue, jibanRec.HosyouUmu)
        ' 保証開始日
        cl.SetDisplayString(TextHosyouKaisiDate.Text, jibanRec.HosyouKaisiDate)
        ' 保証期間
        cl.SetDisplayString(TextHosyouKikan.Text, jibanRec.HosyouKikan)
        ' 保証なし理由コード
        cl.SetDisplayString(SelectHosyouNasiRiyuu.SelectedValue, jibanRec.HosyouNasiRiyuuCd)
        ' 保証なし理由
        cl.SetDisplayString(TextHosyouNasiRiyuu.Text, jibanRec.HosyouNasiRiyuu)
        ' 保証書再発行日
        cl.SetDisplayString(TextSaihakkouDate.Text, jibanRec.HosyousyoSaihakDate)
        ' 保証書発行依頼書有無
        cl.SetDisplayString(SelectHakkouIraisyo.SelectedValue, jibanRec.HosyousyoHakIraisyoUmu)
        ' 保証書発行依頼書着日
        cl.SetDisplayString(TextHakkouIraiTyakuDate.Text, jibanRec.HosyousyoHakIraisyoTykDate)
        ' 業務完了日*
        ' 更新ログインユーザーID
        cl.SetDisplayString(userInfo.LoginUserId, jibanRec.UpdLoginUserId)
        ' 更新日時
        If ucGyoumuKyoutuu.AccupdateDateTime.Value = "" Then
            jibanRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
        Else
            jibanRec.UpdDatetime = DateTime.ParseExact(ucGyoumuKyoutuu.AccupdateDateTime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
        End If

        '***********************************************
        ' 邸別請求テーブル 
        '***********************************************

        ' 保証書再発行
        jibanRec.HosyousyoRecord = GetSaihakkouCtrlData()

        ' 解約払戻
        jibanRec.KaiyakuHaraimodosiRecord = GetKaiyakuCtrlData()

        '***************************************
        ' 画面入力項目以外
        '***************************************
        '更新者
        jibanRec.Kousinsya = cbLogic.GetKousinsya(userInfo.LoginUserId, DateTime.Now)

        ' 発行依頼受付日時
        If HiddenHakIraiUketukeFlg.Value = "1" Then
            jibanRec.HakIraiUkeDatetime = Date.Now
        Else
            If HiddenHakIraiUkeDatetimeOld.Value <> "" Then
                jibanRec.HakIraiUkeDatetime = DateTime.ParseExact(Me.HiddenHakIraiUkeDatetimeOld.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
            End If
        End If

        Return jibanRec

    End Function

#Region "邸別請求レコード作成"

#Region "邸別請求レコード作成（再発行）"
    ''' <summary>
    ''' 再発行の邸別請求レコードを取得します
    ''' </summary>
    ''' <returns>再発行の邸別請求レコード</returns>
    ''' <remarks></remarks>
    Protected Function GetSaihakkouCtrlData() As TeibetuSeikyuuRecord
        Dim record As New TeibetuSeikyuuRecord

        ' 商品未設定時はレコード無し
        If TextShSyouhinCd.Text.Trim() = "" Then
            Return Nothing
        End If

        '** KEY情報 *******************
        ' 区分
        cl.SetDisplayString(ucGyoumuKyoutuu.Kubun, record.Kbn)
        ' 番号（保証書NO）
        cl.SetDisplayString(ucGyoumuKyoutuu.Bangou, record.HosyousyoNo)
        ' 分類コード
        record.BunruiCd = EarthConst.SOUKO_CD_HOSYOUSYO
        ' 画面表示NO
        record.GamenHyoujiNo = 1

        '** 明細情報 *******************
        ' 商品コード
        cl.SetDisplayString(TextShSyouhinCd.Text, record.SyouhinCd)
        ' 売上金額
        cl.SetDisplayString(TextShJituseikyuuKingaku.Text, record.UriGaku)
        ' 仕入金額
        cl.SetDisplayString(HiddenShSiireGaku.Value, record.SiireGaku)
        ' 仕入消費税額
        cl.SetDisplayString(HiddenShSiireSyouhiZei.Value, record.SiireSyouhiZeiGaku)
        ' 税区分
        cl.SetDisplayString(HiddenShZeiKbn.Value, record.ZeiKbn)
        ' 税率
        cl.SetDisplayString(HiddenShZeiritu.Value, record.Zeiritu)
        ' 消費税額
        cl.SetDisplayString(TextShSyouhizei.Text, record.UriageSyouhiZeiGaku)
        ' 請求書発行日
        cl.SetDisplayString(TextShSeikyuusyoHakkouDate.Text, record.SeikyuusyoHakDate)
        ' 売上年月日
        cl.SetDisplayString(TextShUriageNengappi.Text, record.UriDate)
        ' 伝票売上年月日(ロジッククラスで自動セット)
        record.DenpyouUriDate = DateTime.MinValue
        ' 請求有無
        cl.SetDisplayString(SelectShSeikyuuUmu.SelectedValue, record.SeikyuuUmu)
        ' 売上計上FLG (Insertのみ)
        record.UriKeijyouFlg = 0
        ' 売上計上日
        record.UriKeijyouDate = Date.MinValue
        ' 確定区分
        record.KakuteiKbn = Integer.MinValue
        ' 備考(再発行理由)
        cl.SetDisplayString(TextSaihakkouRiyuu.Text, record.Bikou)
        ' 工務店請求額
        cl.SetDisplayString(TextShKoumutenSeikyuuKingaku.Text, record.KoumutenSeikyuuGaku)
        ' 発注書金額
        cl.SetDisplayString(TextShHattyuusyoKingaku.Text, record.HattyuusyoGaku)
        ' 発注書確認日
        cl.SetDisplayString(TextShHattyuusyoKakuninDate.Text, record.HattyuusyoKakuninDate)
        ' 一括入金FLG
        record.IkkatuNyuukinFlg = Integer.MinValue
        ' 調査見積書作成日
        record.TysMitsyoSakuseiDate = Date.MinValue
        ' 発注書確定FLG
        record.HattyuusyoKakuteiFlg = IIf(TextShHattyuusyoKakutei.Text = EarthConst.KAKUTEI, 1, 0)

        '請求先/仕入先リンクから取得
        Me.ucSeikyuuSiireLinkSai.SetTeibetuRecFromSeikyuuSiireLink(record)

        ' 更新ログインユーザーID
        cl.SetDisplayString(userInfo.LoginUserId, record.UpdLoginUserId)
        '更新日時(排他制御用)
        record.UpdDatetime = cl.getDispHaitaUpdTime(Me.HiddenShUpdDateTime)

        Return record

    End Function
#End Region

#Region "邸別請求レコード作成（解約払戻）"
    ''' <summary>
    ''' 解約払戻の邸別請求レコードを取得します
    ''' </summary>
    ''' <returns>解約払戻の邸別請求レコード</returns>
    ''' <remarks></remarks>
    Protected Function GetKaiyakuCtrlData() As TeibetuSeikyuuRecord
        Dim record As New TeibetuSeikyuuRecord

        ' 商品未設定時はレコード無し
        If TextKySyouhinCd.Text.Trim() = "" Then
            Return Nothing
        End If

        '** KEY情報 *******************
        ' 区分
        cl.SetDisplayString(ucGyoumuKyoutuu.Kubun, record.Kbn)
        ' 番号（保証書NO）
        cl.SetDisplayString(ucGyoumuKyoutuu.Bangou, record.HosyousyoNo)
        ' 分類コード
        record.BunruiCd = EarthConst.SOUKO_CD_KAIYAKU_HARAIMODOSI
        ' 画面表示NO
        record.GamenHyoujiNo = 1

        '** 明細情報 *******************
        ' 商品コード
        cl.SetDisplayString(TextKySyouhinCd.Text, record.SyouhinCd)
        ' 売上金額
        cl.SetDisplayString(TextKyJituseikyuuKingaku.Text, record.UriGaku)
        ' 仕入金額
        cl.SetDisplayString(HiddenKySiireGaku.Value, record.SiireGaku)
        ' 仕入消費税額
        cl.SetDisplayString(HiddenKySiireSyouhiZei.Value, record.SiireSyouhiZeiGaku)
        ' 税区分
        cl.SetDisplayString(HiddenKyZeiKbn.Value, record.ZeiKbn)
        ' 税率
        cl.SetDisplayString(HiddenKyZeiritu.Value, record.Zeiritu)
        ' 消費税額
        cl.SetDisplayString(TextKySyouhizei.Text, record.UriageSyouhiZeiGaku)
        ' 請求書発行日
        cl.SetDisplayString(TextKySeikyuusyoHakkouDate.Text, record.SeikyuusyoHakDate)
        ' 売上年月日日
        cl.SetDisplayString(TextKyUriageNengappi.Text, record.UriDate)
        ' 伝票売上年月日(ロジッククラスで自動セット)
        record.DenpyouUriDate = DateTime.MinValue
        ' 請求有無
        cl.SetDisplayString(SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue, record.SeikyuuUmu)
        ' 売上計上FLG (Insertのみ)
        record.UriKeijyouFlg = 0
        ' 売上計上日
        record.UriKeijyouDate = Date.MinValue
        ' 確定区分
        record.KakuteiKbn = Integer.MinValue
        '備考
        record.Bikou = Nothing
        ' 工務店請求額
        cl.SetDisplayString(TextKyKoumutenSeikyuuKingaku.Text, record.KoumutenSeikyuuGaku)
        ' 発注書金額
        cl.SetDisplayString(TextKyHattyuusyoKingaku.Text, record.HattyuusyoGaku)
        ' 発注書確認日
        cl.SetDisplayString(TextKyHattyuusyoKakuninDate.Text, record.HattyuusyoKakuninDate)
        ' 一括入金FLG
        record.IkkatuNyuukinFlg = Integer.MinValue
        ' 調査見積書作成日
        record.TysMitsyoSakuseiDate = Date.MinValue
        ' 発注書確定FLG
        record.HattyuusyoKakuteiFlg = IIf(TextKyHattyuusyoKakutei.Text = EarthConst.KAKUTEI, 1, 0)

        '請求先/仕入先リンクから取得
        Me.ucSeikyuuSiireLinkKai.SetTeibetuRecFromSeikyuuSiireLink(record)

        ' 更新ログインユーザーID
        cl.SetDisplayString(userInfo.LoginUserId, record.UpdLoginUserId)
        '更新日時(排他制御用)
        record.UpdDatetime = cl.getDispHaitaUpdTime(Me.HiddenKyUpdDateTime)

        Return record
    End Function
#End Region

#End Region

#End Region
#Region "品質保証書発行依頼受付"
    ''' <summary>
    ''' 転記(1) 物件(名称) > 施主名転記
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonBukkenTenki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TextbukkenuMei.Text <> "" Then
            ucGyoumuKyoutuu.AccSesyuMei.Value = TextbukkenuMei.Text
            Me.UpdatePanelHosyou.Update()
        End If
    End Sub

    ''' <summary>
    ''' 転記(2) 住所
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonJyuusyoTenki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TextBukkenuSyozai1.Text & TextBukkenuSyozai2.Text & TextBukkenuSyozai3.Text <> "" Then
            ucGyoumuKyoutuu.Jyuusyo1 = TextBukkenuSyozai1.Text
            ucGyoumuKyoutuu.Jyuusyo2 = TextBukkenuSyozai2.Text
            ucGyoumuKyoutuu.Jyuusyo3 = TextBukkenuSyozai3.Text
            Me.UpdatePanelHosyou.Update()
        End If
    End Sub

    ''' <summary>
    ''' 転記(3) 保証開始日
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHosyouKaisiDateTenki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' 引渡し日 → 保証開始日
        TextHosyouKaisiDate.Text = TextHikiwatasiDate.Text
    End Sub
    ''' <summary>
    ''' 発行依頼キャンセル
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHakkouCancel_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim JibanLogic As New JibanLogic
        Dim jr As New JibanRecord
        Dim jibanRec As New JibanRecordHosyou

        ' 現在の地盤データをDBから取得する
        jr = JibanLogic.GetJibanData(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou)
        ' 画面内容より地盤レコードを生成する
        jibanRec = Me.GetCtrlDataRecord()

        ' 進捗T更新用に、DB上の値をセットする
        JibanLogic.SetSintyokuJibanData(jr, jibanRec)

        ' 2. 地盤テーブル.発行依頼キャンセル日時にシステム日時をセット
        jibanRec.HakIraiCanDatetime = Date.Now

        ' 3. 登録完了処理（地盤テーブル.発行依頼キャンセル日時のみ更新）
        If MyLogic.UpdateJibanIraiCancel(Me, jibanRec) = True Then
            cl.CloseWindow(Me)
        Else
            Dim tmpScript As String = ""
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "発行依頼キャンセル") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonTouroku_ServerClick", tmpScript, True)
        End If

    End Sub


#End Region

#Region "保証情報変更時処理"

    ''' <summary>
    ''' (2) [保証情報]基礎報告書変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKisoHoukokusyo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(SelectKisoHoukokusyo) 'フォーカス

        '1.画面の設定
        '基礎報告書
        Select Case SelectKisoHoukokusyo.SelectedValue
            Case "1" '有

                '基礎報告書着日
                If TextKisoHoukokusyoTyakuDate.Text = "" Then
                    TextKisoHoukokusyoTyakuDate.Text = Date.Now.ToString("yyyy/MM/dd")
                End If

                '①<共通情報>区分＝"E"の場合
                If ucGyoumuKyoutuu.Kubun = "E" Then

                    '・保証書発行日＝未入力、かつ、保証書発行状況＝未入力の場合
                    If TextHosyousyoHakkouDate.Text = "" And SelectHosyousyoHakkouJyoukyou.SelectedValue = "" Then

                        '・保証書発行日←<共通情報>区分に該当する日付マスタ.保証書発行日
                        TextHosyousyoHakkouDate.Text = MyLogic.GetHosyousyoHakkouDate(ucGyoumuKyoutuu.Kubun)

                        '保証書発行日変更時処理を行なう▲
                        SetKyoutuuTyousaJissiDate(sender, e)

                    End If

                End If

                '②<共通情報>区分＝"E"、又は "W"で、保証開始日＝未入力の場合
                If ucGyoumuKyoutuu.Kubun = "E" Or ucGyoumuKyoutuu.Kubun = "W" Then

                    '保証開始日
                    If TextHosyouKaisiDate.Text = "" Then

                        '判定1コード
                        Select Case HiddenHantei1CdOld.Value

                            Case "1", "2", "3" '1,2,3の場合
                                '・保証開始日←調査実施日
                                TextHosyouKaisiDate.Text = TextTyousaJissiDate.Text

                            Case Else '上記以外

                                '・改良工事日＝入力の場合
                                If TextKairyouKoujiDate.Text <> "" Then

                                    '・保証開始日←<改良工事>改良工事日
                                    TextHosyouKaisiDate.Text = TextKairyouKoujiDate.Text

                                Else '未入力
                                    '・保証開始日←調査実施日
                                    TextHosyouKaisiDate.Text = TextTyousaJissiDate.Text

                                End If

                        End Select

                    End If

                End If

            Case "0" '無

            Case Else

        End Select

        SetEnableControl() '画面制御

    End Sub

    ''' <summary>
    ''' (4) [保証情報]発行書依頼書変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectHakkouIraisyo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(SelectHakkouIraisyo) 'フォーカス

        '1.画面の設定
        '(1)発行依頼書＝1(有)の場合
        Select Case SelectHakkouIraisyo.SelectedValue
            Case "1" '有
                '・発行依頼書着日＝未入力の場合
                If TextHakkouIraiTyakuDate.Text = "" Then
                    TextHakkouIraiTyakuDate.Text = Date.Now.ToString("yyyy/MM/dd")
                End If

            Case Else '上記以外

        End Select

        '画面制御
        SetEnableControl()
        SetEnableControlHhDate(Me.HiddenBukkenJyky.Value)

    End Sub

    ''' <summary>
    ''' (6) [保証情報]保証書発行状況変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectHosyousyoHakkouJyoukyou_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        setFocusAJ(SelectHosyousyoHakkouJyoukyou) 'フォーカス

        Dim strMsg As String = String.Empty

        '1.画面の設定	
        '(1)保証書発行状況＝未入力の場合
        If SelectHosyousyoHakkouJyoukyou.SelectedValue = String.Empty Then
            '保証書発行状況設定日
            TextHosyousyoHakkouJyoukyouSetteiDate.Text = String.Empty  '空白

        Else
            '地盤T.保証商品有無="0"
            If Me.HiddenHosyouSyouhinUmu.Value <> "1" Then
                '保証書発行状況.保証あり
                If cbLogic.GetHosyousyoHakJykyHosyouUmu(Me.SelectHosyousyoHakkouJyoukyou.SelectedValue) = "1" Then
                    '変更前の値に戻す
                    Me.SelectHosyousyoHakkouJyoukyou.SelectedValue = Me.HiddenHosyousyoHakJyKyMae.Value
                    strMsg = Messages.MSG145E.Replace("@PARAM1", "商品設定状況").Replace("@PARAM2", EarthConst.DISP_HOSYOU_NASI_HOSYOU_SYOUHIN_UMU).Replace("@PARAM3", "保証書発行状況")
                    MLogic.AlertMessage(sender, strMsg, 0)
                    Exit Sub
                End If
            End If

            '保証書発行状況設定日
            TextHosyousyoHakkouJyoukyouSetteiDate.Text = Date.Now.ToString("yyyy/MM/dd")

        End If

        '●保証書発行状況による保証あり/なしの表示切替
        Me.ChgDispHosyouUmu()

    End Sub

    ''' <summary>
    ''' (7) [保証情報]保証書発行日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextHosyousyoHakkouDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""

        '2.保証書発行日＝入力の場合
        If TextHosyousyoHakkouDate.Text <> "" Then
            SetKyoutuuTyousaJissiDate(sender, e) '【共通】調査実施日妥当性確認処理を行う。
        End If

        '3.<保証書再発行>、<解約払戻>の画面設定
        '保証書発行日
        Select Case TextHosyousyoHakkouDate.Text
            Case Is <> "" '入力

                SetFuhoSyoumeisyoFlg() '付保証明書FLG

                '<解約払戻>をクリア
                SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" '解約払戻申請有無
                TextKySyouhinCd.Text = "" '商品コード
                TextKyKoumutenSeikyuuKingaku.Text = "" '工務店請求税抜金額
                TextKyJituseikyuuKingaku.Text = "" '実請求税抜金額
                TextKySyouhizei.Text = "" '消費税
                TextKyZeikomiKingaku.Text = "" '税込金額
                TextKySeikyuusyoHakkouDate.Text = "" '請求書発行日
                TextKyUriageNengappi.Text = "" '売上年月日
                TextKyHattyuusyoKakutei.Text = "" '発注書確定


            Case "" '未入力

                '(*4)発注書金額≠0,かつ,≠Nullの場合、エラーメッセージ（037）を表示し、自動設定（クリア）を行わずに、未入力に変更した項目(発送日/再発行日/請求有無)は元の値に戻す。
                If TextShHattyuusyoKingaku.Text <> "0" And TextShHattyuusyoKingaku.Text <> "" Then
                    tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                    TextHosyousyoHakkouDate.Text = HiddenHosyousyoHakkouDateMae.Value '変更前の値に戻す ※注 Javascriptでの設定ではChangeイベントが発生しない
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextHosyousyoHakkouDate_TextChanged", tmpScript, True)
                    Exit Sub
                End If

                SetFuhoSyoumeisyoFlg() '付保証明書FLGの自動設定

                '●自動設定
                Me.TextSaihakkouDate.Text = "" '再発行日
                Me.TextSaihakkouRiyuu.Text = String.Empty '再発行理由

                ClearBlnkSyouhinTableSh() '空白クリア

        End Select

        setFocusAJ(TextHosyouKaisiDate) 'フォーカス

        '画面制御
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' 付保証明書FLGの初期値セットを行なう
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetFuhoSyoumeisyoFlg()

        '付保証明書FLG自動設定フラグが"1"以外の場合
        If HiddenSetFuhoSyoumeisyoFlg.Value <> EarthConst.ARI_VAL Then Exit Sub '自動設定を行なわない

        '保証書発行日
        If TextHosyousyoHakkouDate.Text = "" Then '未入力
            SelectFuhoSyoumeisyoFlg.SelectedValue = "" '空白

        Else '入力

            Dim KameitenSearchLogic As New KameitenSearchLogic
            Dim recData1 As New KameitenSearchRecord
            Dim tmpScript As String = ""

            setFocusAJ(SelectFuhoSyoumeisyoFlg) 'フォーカス

            recData1 = KameitenSearchLogic.GetFuhoSyoumeisyoInfo( _
                                                ucGyoumuKyoutuu.Kubun _
                                                , ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                , False _
                                                )

            If Not recData1 Is Nothing Then

                ' 加盟店.付保証明書FLG
                If recData1.FuhoSyoumeisyoFlg <> 1 Then '<>有り
                    SelectFuhoSyoumeisyoFlg.SelectedValue = "0" 'なし
                End If

                '付保証明書開始年月
                If cl.GetDisplayString(recData1.FuhoSyoumeiKaisiNengetu) = "" Then '未入力
                    SelectFuhoSyoumeisyoFlg.SelectedValue = "0" 'なし

                Else '入力

                    Dim dtHosyousyoHakkouDate As New DateTime '保証書発行日
                    Dim dtFuhoSyoumeiKaisiDate As New DateTime '加盟店.付保証明書開始年月

                    dtHosyousyoHakkouDate = DateTime.Parse(TextHosyousyoHakkouDate.Text)
                    dtFuhoSyoumeiKaisiDate = recData1.FuhoSyoumeiKaisiNengetu

                    '保証書発行日 < 加盟店.付保証明書開始年月
                    If dtHosyousyoHakkouDate < dtFuhoSyoumeiKaisiDate Then
                        SelectFuhoSyoumeisyoFlg.SelectedValue = "0" 'なし

                    Else '保証書発行日 >= 加盟店.付保証明書開始年月

                        ' 加盟店.付保証明書FLG
                        If recData1.FuhoSyoumeisyoFlg = 1 Then '有り
                            SelectFuhoSyoumeisyoFlg.SelectedValue = "1" '有り
                        End If

                    End If
                End If

            End If

        End If
    End Sub

    ''' <summary>
    ''' 【共通】調査実施日妥当性確認処理を行う。※(17) 【共通】調査実施日妥当性確認処理変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SetKyoutuuTyousaJissiDate(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""

        '****************************
        '* 調査実施日の登録確認
        '****************************
        '調査実施日
        '(1)調査実施日＝未入力の場合
        If TextTyousaJissiDate.Text = "" Then '未入力
            '登録許可にOKしていない場合
            If HiddenHosyousyoHakkouDateMsg04.Value <> "1" Then
                '登録確認メッセージを表示する。
                tmpScript = "if(confirm('" & Messages.MSG098C & "')){" & vbCrLf
                tmpScript &= "  objEBI('" & HiddenHosyousyoHakkouDateMsg04.ClientID & "').value = '1';" & vbCrLf
                tmpScript &= "}" & vbCrLf
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetKyoutuuTyousaJissiDate1", tmpScript, True)
            End If

        Else '(2)上記以外の場合

            '調査実施日、且つ保証書発行日に入力がある場合
            If TextTyousaJissiDate.Text <> "" And TextHosyousyoHakkouDate.Text <> "" Then

                Dim dtTyousa As Date = Date.Parse(TextTyousaJissiDate.Text)
                Dim dtHosyousyo As Date = Date.Parse(TextHosyousyoHakkouDate.Text)

                '・調査実施日＞保証書発行日の場合
                If dtTyousa > dtHosyousyo Then

                    '登録許可にOKしていない場合
                    If HiddenHosyousyoHakkouDateMsg03.Value <> "1" Then
                        '登録確認メッセージを表示する。
                        tmpScript = "if(confirm('" & Messages.MSG099C & "')){" & vbCrLf
                        tmpScript &= "  objEBI('" & HiddenHosyousyoHakkouDateMsg03.ClientID & "').value = '1';" & vbCrLf
                        tmpScript &= "}" & vbCrLf
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetKyoutuuTyousaJissiDate2", tmpScript, True)
                    End If

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' (19)付保証明書FLG変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectFuhoSyoumeisyoFlg_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim recData1 As New KameitenSearchRecord
        Dim tmpScript As String = ""

        setFocusAJ(SelectFuhoSyoumeisyoFlg) 'フォーカス

        recData1 = kameitenlogic.GetFuhoSyoumeisyoInfo( _
                                            ucGyoumuKyoutuu.Kubun _
                                            , ucGyoumuKyoutuu.AccKameitenCd.Value _
                                            , True _
                                            )

        If Not recData1 Is Nothing Then

            ' 加盟店.付保証明書FLG
            If recData1.FuhoSyoumeisyoFlg <> 1 Then '<>有り
                '付保証明書FLG
                If SelectFuhoSyoumeisyoFlg.SelectedValue = "1" Then '有り
                    tmpScript = "callFuhoSyoumeisyoFlgCancel('" & Messages.MSG108E & "','" & ButtonFuhoSyoumeisyoFlg.ClientID & "');" & vbCrLf
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectFuhoSyoumeisyoFlg_SelectedIndexChanged1", tmpScript, True)
                    Exit Sub
                End If

            Else '有り

                '付保証明書FLG
                If HiddenFuhoSyoumeisyoFlgMae.Value = "1" And SelectFuhoSyoumeisyoFlg.SelectedValue <> "1" Then '有り→<>有り

                    Dim dtHosyousyoHakkouDate As New DateTime '保証書発行日
                    Dim dtFuhoSyoumeiKaisiDate As New DateTime '加盟店.付保証明書開始年月

                    '保証書発行日
                    If TextHosyousyoHakkouDate.Text = "" Then Exit Sub '未入力時、処理を抜ける

                    '付保証明書開始年月
                    If cl.GetDisplayString(recData1.FuhoSyoumeiKaisiNengetu) = "" Then Exit Sub '未入力時、処理を抜ける

                    dtHosyousyoHakkouDate = DateTime.Parse(TextHosyousyoHakkouDate.Text)
                    dtFuhoSyoumeiKaisiDate = recData1.FuhoSyoumeiKaisiNengetu

                    '保証書発行日 < 加盟店.付保証明書開始年月
                    If dtHosyousyoHakkouDate < dtFuhoSyoumeiKaisiDate Then
                        'なし

                    Else '保証書発行日 >= 加盟店.付保証明書開始年月

                        tmpScript = "callFuhoSyoumeisyoFlgCancel('" & Messages.MSG109E & "','" & ButtonFuhoSyoumeisyoFlg.ClientID & "');" & vbCrLf
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectFuhoSyoumeisyoFlg_SelectedIndexChanged2", tmpScript, True)
                        Exit Sub

                    End If

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' 付保証明書FLG変更・後処理(JSにてキャンセル時)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonFuhoSyoumeisyoFlgCancel_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        setFocusAJ(SelectFuhoSyoumeisyoFlg) 'フォーカス

        '付保証明書FLG
        SelectFuhoSyoumeisyoFlg.SelectedValue = HiddenFuhoSyoumeisyoFlgMae.Value '変更前の値に戻す

    End Sub

#End Region

#Region "再発行変更時処理"

    ''' <summary>
    ''' (10) [再発行]再発行日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSaihakkouDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strDtTmp As String
        Dim tmpScript As String = ""

        setFocusAJ(TextSaihakkouDate) 'フォーカス

        '再発行日
        If TextSaihakkouDate.Text <> "" Then '入力

            setFocusAJ(TextFocusBounderSaihakkouRiyuu) 'フォーカス

            '請求有無
            Select Case SelectShSeikyuuUmu.SelectedValue
                Case "1" '有
                    '商品コード
                    If TextShSyouhinCd.Text <> "" Then '設定済
                        '請求書発行日
                        If TextShSeikyuusyoHakkouDate.Text.Length = 0 Then
                            '請求締め日のセット
                            Me.ucSeikyuuSiireLinkSai.SetSeikyuuSimeDate(Me.TextShSyouhinCd.Text)
                            '請求書発行日の自動設定
                            strDtTmp = Me.ucSeikyuuSiireLinkSai.GetSeikyuusyoHakkouDate()
                            TextShSeikyuusyoHakkouDate.Text = strDtTmp
                        End If

                        '売上年月日
                        If TextShUriageNengappi.Text.Length = 0 Then '未入力
                            '売上年月日の自動設定
                            TextShUriageNengappi.Text = Date.Now.ToString("yyyy/MM/dd")
                        End If

                    End If

                Case "" '無
                    '商品コード
                    If TextShSyouhinCd.Text <> "" Then '設定済

                        '売上年月日
                        If TextShUriageNengappi.Text.Length = 0 Then '未入力
                            '売上年月日の自動設定
                            TextShUriageNengappi.Text = Date.Now.ToString("yyyy/MM/dd")
                        End If

                    End If
            End Select

        Else '未入力

            '(*4)発注書金額≠0,かつ,≠Nullの場合、エラーメッセージ（037）を表示し、自動設定（クリア）を行わずに、未入力に変更した項目(発送日/再発行日/請求有無)は元の値に戻す。
            If TextShHattyuusyoKingaku.Text <> "0" And TextShHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                TextSaihakkouDate.Text = HiddenSaihakkouDateMae.Value '変更前の値に戻す ※注 Javascriptでの設定ではChangeイベントが発生しない
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextSaihakkouDate_TextChanged", tmpScript, True)
                Exit Sub
            End If

            '●自動設定
            Me.TextSaihakkouRiyuu.Text = String.Empty '再発行理由

            ClearBlnkSyouhinTableSh() '空白クリア

        End If

        SetEnableControl() '画面制御

    End Sub

    ''' <summary>
    ''' (11) [再発行]請求有無変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectShSeikyuu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""
        Dim blnTorikesi As Boolean = False
        Dim strDtTmp As String

        Dim syouhinRec As New Syouhin23Record

        setFocusAJ(SelectShSeikyuuUmu) 'フォーカス

        '仕入額は常に0円
        Me.HiddenShSiireGaku.Value = "0"
        Me.HiddenShSiireSyouhiZei.Value = "0"

        '請求有無
        If SelectShSeikyuuUmu.SelectedValue = "" Then '空白

            '(*4)発注書金額≠0,かつ,≠Nullの場合、エラーメッセージ（037）を表示し、自動設定（クリア）を行わずに、未入力に変更した項目(発送日/再発行日/請求有無)は元の値に戻す。
            If TextShHattyuusyoKingaku.Text <> "0" And TextShHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                SelectShSeikyuuUmu.SelectedValue = HiddenShSeikyuuUmuMae.Value '変更前の値に戻す ※注 Javascriptでの設定ではChangeイベントが発生しない
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectShSeikyuu_SelectedIndexChanged", tmpScript, True)
                Exit Sub
            End If

            '●自動設定
            ClearBlnkSyouhinTableSh() '空白クリア

        ElseIf SelectShSeikyuuUmu.SelectedValue = "0" Then '無
            '金額の0クリア
            Clear0SyouhinTableSh()

            '商品コード/商品名の自動設定▲
            SetSyouhinInfoSh()

            '金額の再計算
            SetKingaku(EnumKingakuType.Saihakkou)

        ElseIf SelectShSeikyuuUmu.SelectedValue = "1" Then '有

            '商品コード/商品名の自動設定▲
            SetSyouhinInfoSh()

            '商品コード
            If TextShSyouhinCd.Text.Length = 0 Then '設定なし
                '請求有無
                SelectShSeikyuuUmu.SelectedValue = "" '空白

                SetEnableControl() '画面制御
                Exit Sub
            Else
                '請求書発行日
                If TextShSeikyuusyoHakkouDate.Text.Length = 0 Then
                    '請求締め日のセット
                    Me.ucSeikyuuSiireLinkSai.SetSeikyuuSimeDate(Me.TextShSyouhinCd.Text)
                    '請求書発行日の自動設定
                    strDtTmp = Me.ucSeikyuuSiireLinkSai.GetSeikyuusyoHakkouDate()
                    TextShSeikyuusyoHakkouDate.Text = strDtTmp
                End If

                TextShUriageNengappi.Text = Date.Now.ToString("yyyy/MM/dd") '売上年月日
                '発注書確定
                If TextShHattyuusyoKakutei.Text.Length = 0 Then '(*5)発注書確定が空白の場合は、「0：未確定」を設定する
                    TextShHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
                End If

            End If

            '*************************
            '* 以下、自動設定処理
            '*************************
            If TextShSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '直接請求

                '(*6)加盟店が取消の場合、他請求3系列以外の扱いとする
                Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                If record Is Nothing Then 'データ取得できなかった場合
                    SelectShSeikyuuUmu.SelectedValue = "" '取得NG(請求有無のクリア)

                    ClearBlnkSyouhinTableSh() '空白クリア
                End If

                If record.Torikesi <> 0 Then '取消フラグがたっている場合
                    '**************************************************
                    ' 他請求（系列以外）
                    '**************************************************
                    ' 工務店請求額は０
                    TextShKoumutenSeikyuuKingaku.Text = "0"

                    '実請求税抜金額の自動設定
                    syouhinRec = JibanLogic.GetSyouhinInfo(TextShSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.Hosyousyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        HiddenShZeiritu.Value = syouhinRec.Zeiritu '税率
                        HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '税区分

                        '実請求税抜金額＝商品マスタ.標準価格
                        TextShJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                    End If

                Else
                    '**************************************************
                    ' 直接請求
                    '**************************************************
                    '工務店(A)
                    '実請求(A)

                    syouhinRec = JibanLogic.GetSyouhinInfo(TextShSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.Hosyousyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        '工務店請求税抜金額＝商品マスタ.標準価格
                        TextShKoumutenSeikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)
                        '実請求税抜金額＝画面.工務店請求税抜金額
                        TextShJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                        HiddenShZeiritu.Value = syouhinRec.Zeiritu '税率
                        HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '税区分
                    End If

                End If

            ElseIf TextShSeikyuusaki.Text = EarthConst.SEIKYU_TASETU Then '他請求

                Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                If record Is Nothing Then 'データ取得できなかった場合
                    SelectShSeikyuuUmu.SelectedValue = "" '取得NG(請求有無のクリア)

                    ClearBlnkSyouhinTableSh() '空白クリア
                End If

                '(*6)加盟店が取消の場合、他請求3系列以外の扱いとする
                If record.Torikesi <> 0 Then
                    record.KeiretuCd = ""
                End If

                If cl.getKeiretuFlg(record.KeiretuCd) Then '3系列
                    '**************************************************
                    ' 他請求（3系列）
                    '**************************************************
                    '工務店(A)
                    '実請求(B)

                    syouhinRec = JibanLogic.GetSyouhinInfo(TextShSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.Hosyousyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        HiddenShZeiritu.Value = syouhinRec.Zeiritu '税率
                        HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '税区分

                        '工務店請求税抜金額＝商品マスタ.標準価格
                        TextShKoumutenSeikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                        '※画面.工務店請求税抜金額＝0 の場合は 0 固定
                        If TextShKoumutenSeikyuuKingaku.Text = "0" Then
                            TextShJituseikyuuKingaku.Text = "0" '実請求税抜金額

                        Else

                            Dim zeinukiGaku As Integer = 0

                            If JibanLogic.GetSeikyuuGaku(sender, _
                                                          3, _
                                                          record.KeiretuCd, _
                                                          TextShSyouhinCd.Text, _
                                                          syouhinRec.HyoujunKkk, _
                                                          zeinukiGaku) Then
                                ' 実請求金額へセット
                                TextShJituseikyuuKingaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                            End If

                        End If

                    End If

                Else '3系列以外

                    '**************************************************
                    ' 他請求（3系列以外）
                    '**************************************************
                    '工務店(B)
                    '実請求(C)

                    ' 工務店請求額は０
                    TextShKoumutenSeikyuuKingaku.Text = "0"

                    '実請求税抜金額の自動設定
                    syouhinRec = JibanLogic.GetSyouhinInfo(TextShSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.Hosyousyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        HiddenShZeiritu.Value = syouhinRec.Zeiritu '税率
                        HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '税区分

                        '実請求税抜金額＝商品マスタ.標準価格
                        TextShJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                    End If

                End If

            End If

            '金額の再計算
            SetKingaku(EnumKingakuType.Saihakkou)

        End If

        '画面制御
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' (12) [再発行]工務店請求税抜金額変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextShKoumutenSeikyuuKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim syouhinRec As New Syouhin23Record

        setFocusAJ(TextShJituseikyuuKingaku) 'フォーカス

        '請求
        If SelectShSeikyuuUmu.SelectedValue = "1" Then '有
            '商品コード
            If TextShSyouhinCd.Text.Length <> 0 Then '設定済

                '税情報設定
                SetShZeiInfo(TextShSyouhinCd.Text)

                '請求先
                If TextShSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '直接請求
                    '**************************************************
                    ' 直接請求
                    '**************************************************

                    '実請求税抜金額＝画面.工務店請求税抜金額
                    TextShJituseikyuuKingaku.Text = TextShKoumutenSeikyuuKingaku.Text

                    SetKingaku(EnumKingakuType.Saihakkou) '金額再計算

                ElseIf TextShSeikyuusaki.Text = EarthConst.SEIKYU_TASETU Then '他請求

                    Dim kameitenlogic As New KameitenSearchLogic
                    Dim blnTorikesi As Boolean = False
                    Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                    '(*6)加盟店が取消の場合、他請求3系列以外の扱いとする
                    If record.Torikesi <> 0 Then
                        record.KeiretuCd = ""
                    End If

                    If cl.getKeiretuFlg(record.KeiretuCd) Then '3系列
                        '**************************************************
                        ' 他請求（3系列）
                        '**************************************************

                        '<表2>実請求税抜金額（掛率）の設定▲

                        Dim logic As New JibanLogic
                        Dim koumuten_gaku As Integer = 0
                        Dim zeinuki_gaku As Integer = 0

                        cl.SetDisplayString(TextShKoumutenSeikyuuKingaku.Text, koumuten_gaku)
                        koumuten_gaku = IIf(koumuten_gaku = Integer.MinValue, 0, koumuten_gaku)

                        If logic.GetSeikyuuGaku(sender, _
                                                5, _
                                                record.KeiretuCd, _
                                                TextShSyouhinCd.Text, _
                                                koumuten_gaku, _
                                                zeinuki_gaku) Then


                            '商品情報を取得(キー:商品コード)
                            syouhinRec = JibanLogic.GetSyouhinInfo(TextShSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.Hosyousyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                            If Not syouhinRec Is Nothing Then
                                '(*3)請求有無変更時に、自動設定された工務店請求金額が0（商品マスタ.標準価格＝0）の場合、1回のみ実請求金額の自動設定を行う。
                                If syouhinRec.HyoujunKkk = 0 Then
                                    If HiddenShJituseikyuu1Flg.Value = "" Then
                                        HiddenShJituseikyuu1Flg.Value = "1" 'フラグをたてる

                                        ' 税抜金額（実請求金額）へセット
                                        TextShJituseikyuuKingaku.Text = zeinuki_gaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                                    End If
                                    '*****************
                                    '* 地盤システムの処理に合わせるため、コメントアウト
                                    '*****************
                                    'Else
                                    '    ' 税抜金額（実請求金額）へセット
                                    '    TextShJituseikyuuKingaku.Text = zeinuki_gaku.ToString(EarthConst.FORMAT_KINGAKU1)

                                End If

                            End If
                        End If

                        SetKingaku(EnumKingakuType.Saihakkou) '金額再計算

                    End If

                End If
            End If

        End If

        '画面制御
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' (14) [再発行]実請求税抜金額変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextShJituseikyuuKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(TextShSeikyuusyoHakkouDate) 'フォーカス

        '実請求税抜金額
        If TextShJituseikyuuKingaku.Text.Length = 0 Then '入力なし
            TextShSyouhizei.Text = "" '消費税
            TextShZeikomiKingaku.Text = "" '税込金額

            SetKingaku(EnumKingakuType.Saihakkou) '金額の再計算

        Else '入力あり

            '請求有無
            If SelectShSeikyuuUmu.SelectedValue = "1" Then '有
                '商品コード
                If TextShSyouhinCd.Text.Length <> 0 Then '設定済

                    '税情報設定
                    SetShZeiInfo(TextShSyouhinCd.Text)

                    '請求先
                    If TextShSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '直接請求

                        '工務店請求税抜金額＝画面.実請求税抜金額
                        TextShKoumutenSeikyuuKingaku.Text = TextShJituseikyuuKingaku.Text

                        SetKingaku(EnumKingakuType.Saihakkou) '金額再計算

                    Else '直接請求以外
                        SetKingaku(EnumKingakuType.Saihakkou) '金額再計算
                    End If

                End If

            End If

        End If

        '画面制御
        SetEnableControl()
    End Sub

#Region "(16) [再発行][解約払戻]解約払戻申請有無変更時処理関連"

    ''' <summary>
    ''' (16) [再発行][解約払戻]解約払戻申請有無変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKyKaiyakuHaraimodosiSinseiUmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpscript As String = ""

        setFocusAJ(SelectKyKaiyakuHaraimodosiSinseiUmu) 'フォーカス

        '1.解約払戻チェック、および画面の設定、画面制御	

        '(1)<解約払戻>解約払戻申請有無＝1(有)の場合
        Select Case SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue
            Case "1" '有
                '①<hidden><商品コード1>請求書発行日＝未入力の場合
                If HiddenSyouhin1SeikyuuHakkouDate.Value = "" Then
                    '解約払戻申請有無
                    SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" '空白

                    SetEnableControl() '画面制御

                    MLogic.AlertMessage(sender, Messages.MSG096E, 0)
                    Exit Sub

                ElseIf MyLogic.GetKaiyakuKakaku(ucGyoumuKyoutuu.AccKameitenCd.Value, ucGyoumuKyoutuu.Kubun) = 0 Then '②解約払戻価格（KEY：共通画面.加盟店コード）＝0の場合

                    '解約払戻申請有無
                    SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" '空白

                    SetFocus(SelectKyKaiyakuHaraimodosiSinseiUmu) 'フォーカス

                    SetEnableControl() '画面制御

                    MLogic.AlertMessage(sender, Messages.MSG053E, 0)
                    Exit Sub

                Else '③①、②以外の場合


                    '解約払戻申請有無変更時処理
                    SelectKyKaiyakuHaraimodosiSinseiUmu_ChangeSub(sender, e)

                    'b.画面の編集と入力制御
                    SetEnableControl() '画面制御

                End If

            Case Else '有 以外

                '解約払戻申請有無変更時処理
                SelectKyKaiyakuHaraimodosiSinseiUmu_ChangeSub(sender, e)

                SetEnableControl() '画面制御

        End Select

        SetEnableControlHhDate(Me.HiddenBukkenJyky.Value) '画面制御

    End Sub

    ''' <summary>
    ''' 自動設定/解約払戻申請有無変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKyKaiyakuHaraimodosiSinseiUmu_ChangeSub(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim syouhinRec As New Syouhin23Record
        Dim tmpScript As String = ""
        Dim strDtTmp As String
        Dim blnTorikesi As Boolean = False

        '仕入額は常に0円
        Me.HiddenKySiireGaku.Value = "0"
        Me.HiddenKySiireSyouhiZei.Value = "0"

        '解約払戻有無
        Select Case SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue

            Case "1" '有

                If MyLogic.GetKaiyakuKakaku(ucGyoumuKyoutuu.AccKameitenCd.Value, ucGyoumuKyoutuu.Kubun) <> 0 Then

                    '商品コードの自動設定▲
                    SetSyouhinInfoKy()

                    '商品コード
                    If TextKySyouhinCd.Text = String.Empty Then '設定なし
                        '請求有無
                        SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" '空白

                        SetEnableControlKy() '画面制御
                        Exit Sub
                    Else
                        '請求書発行日
                        If TextKySeikyuusyoHakkouDate.Text.Length = 0 Then
                            '請求締め日のセット
                            Me.ucSeikyuuSiireLinkKai.SetSeikyuuSimeDate(Me.TextKySyouhinCd.Text)
                            '請求書発行日の自動設定
                            strDtTmp = Me.ucSeikyuuSiireLinkKai.GetSeikyuusyoHakkouDate()
                            TextKySeikyuusyoHakkouDate.Text = strDtTmp
                        End If

                        '売上年月日
                        TextKyUriageNengappi.Text = Date.Now.ToString("yyyy/MM/dd")

                        '発注書確定
                        If TextKyHattyuusyoKakutei.Text = "" Then
                            TextKyHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI '未確定
                        End If

                    End If

                    '*************************
                    '* 以下、自動設定処理
                    '*************************
                    '請求先
                    Select Case TextKySeikyuusaki.Text

                        Case EarthConst.SEIKYU_TYOKUSETU '直接請求

                            '(*6)加盟店が取消の場合、他請求3系列以外の扱いとする
                            Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                            If record Is Nothing Then 'データ取得できなかった場合
                                SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" '取得NG(請求有無のクリア)

                                ClearBlnkSyouhinTableKy() '空白クリア
                            End If

                            If record.Torikesi <> 0 Then '取消フラグがたっている場合
                                '工務店(B)
                                '**************************************************
                                ' 他請求（3系列以外）
                                '**************************************************
                                ' 工務店請求額は０
                                TextKyKoumutenSeikyuuKingaku.Text = "0"

                                '実請求税抜金額＝加盟店マスタ.解約払戻価格 * -1
                                TextKyJituseikyuuKingaku.Text = MyLogic.GetKaiyakuKakaku(ucGyoumuKyoutuu.AccKameitenCd.Value, ucGyoumuKyoutuu.Kubun) * -1

                            Else
                                '**************************************************
                                ' 直接請求
                                '**************************************************

                                '実請求税抜金額＝加盟店マスタ.解約払戻価格 * -1
                                TextKyJituseikyuuKingaku.Text = MyLogic.GetKaiyakuKakaku(ucGyoumuKyoutuu.AccKameitenCd.Value, ucGyoumuKyoutuu.Kubun) * -1

                                '工務店(A)
                                '工務店請求税抜金額＝画面.実請求税抜金額
                                TextKyKoumutenSeikyuuKingaku.Text = TextKyJituseikyuuKingaku.Text

                                SetKingaku(EnumKingakuType.KaiyakuHaraimodosi) '金額の再計算

                            End If

                            SetKingaku(EnumKingakuType.KaiyakuHaraimodosi) '金額の再計算

                        Case EarthConst.SEIKYU_TASETU '他請求

                            Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                            If record Is Nothing Then 'データ取得できなかった場合
                                SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" '取得NG(請求有無のクリア)

                                ClearBlnkSyouhinTableKy() '空白クリア
                            End If

                            '(*6)加盟店が取消の場合、他請求3系列以外の扱いとする
                            If record.Torikesi <> 0 Then
                                record.KeiretuCd = ""
                            End If

                            If cl.getKeiretuFlg(record.KeiretuCd) Then '3系列
                                '**************************************************
                                ' 他請求（3系列）
                                '**************************************************
                                Dim zeinukiGaku As Integer = 0

                                '請求金額の取得
                                If JibanLogic.GetSeikyuuGaku(sender, _
                                                              2, _
                                                              record.KeiretuCd, _
                                                              TextKySyouhinCd.Text, _
                                                              syouhinRec.HyoujunKkk, _
                                                              zeinukiGaku) Then
                                    ' 工務店請求税抜金額へセット
                                    TextKyKoumutenSeikyuuKingaku.Text = (Math.Abs(zeinukiGaku) * -1).ToString(EarthConst.FORMAT_KINGAKU_1)

                                    '実請求税抜金額＝加盟店マスタ.解約払戻価格 * -1
                                    TextKyJituseikyuuKingaku.Text = MyLogic.GetKaiyakuKakaku(ucGyoumuKyoutuu.AccKameitenCd.Value, ucGyoumuKyoutuu.Kubun) * -1

                                End If

                            Else '3系列以外

                                '工務店(B)
                                '**************************************************
                                ' 他請求（3系列以外）
                                '**************************************************
                                ' 工務店請求額は０
                                TextKyKoumutenSeikyuuKingaku.Text = "0"

                                '実請求税抜金額＝加盟店マスタ.解約払戻価格 * -1
                                TextKyJituseikyuuKingaku.Text = MyLogic.GetKaiyakuKakaku(ucGyoumuKyoutuu.AccKameitenCd.Value, ucGyoumuKyoutuu.Kubun) * -1

                            End If

                            SetKingaku(EnumKingakuType.KaiyakuHaraimodosi) '金額の再計算

                    End Select

                End If

            Case "" '空白

                '(*4)発注書金額≠0,かつ,≠Nullの場合、エラーメッセージ（037）を表示し、自動設定（クリア）を行わずに、未入力に変更した項目(発送日/再発行日/請求有無)は元の値に戻す。
                If TextKyHattyuusyoKingaku.Text <> "0" And TextKyHattyuusyoKingaku.Text <> "" Then
                    MLogic.AlertMessage(sender, Messages.MSG010E, 0)
                    SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = HiddenKyKaiyakuHaraimodosiSinseiUmuMae.Value '変更前の値に戻す ※注 Javascriptでの設定ではChangeイベントが発生しない
                    Exit Sub
                End If

                TextKySyouhinCd.Text = "" '商品コード
                TextKyKoumutenSeikyuuKingaku.Text = "" '工務店請求税抜金額
                TextKyJituseikyuuKingaku.Text = "" '実請求税抜金額
                TextKySyouhizei.Text = "" '消費税
                TextKyZeikomiKingaku.Text = "" '税込金額
                TextKySeikyuusyoHakkouDate.Text = "" '請求書発行日
                TextKyUriageNengappi.Text = "" '売上年月日
                TextKyHattyuusyoKakutei.Text = "" '発注書確定

        End Select

    End Sub

#End Region

#End Region

#Region "画面制御"

    ''' <summary>
    ''' コントロールの画面制御/保証画面
    ''' </summary>
    ''' <remarks>コントロールの画面制御を仕様書の優先順に記載する</remarks>
    Public Sub SetEnableControl()
        '共通情報
        ucGyoumuKyoutuu.SetEnableKameiten(Me.HiddenBukkenJyky.Value)
        '保証情報
        SetEnableControlHosyou()
        '再発行
        SetEnableControlSh()
        '解約払戻
        SetEnableControlKy()
        '発行依頼
        SetEnableControlHakkouIrai()
    End Sub

    ''' <summary>
    ''' コントロールの画面制御/発行依頼情報
    ''' </summary>
    ''' <remarks>コントロールの画面制御を仕様書の優先順に記載する</remarks>
    Public Sub SetEnableControlHakkouIrai()

        '******************
        '* 非活性化
        '******************

        ' 下記条件に該当する場合、ボタンを非表示に設定し、エリア表示を閉じる状態とする
        ' ・進捗データ.発行依頼日時が空白(もしくは)
        ' ・進捗データ.発行依頼日時 < 進捗データ.発行依頼受付日時　もしくは
        ' ・進捗データ.発行依頼日時 < 進捗データ.発行依頼キャンセル日時

        If Me.HiddenHakIraiTime.Value = "" Or _
           Me.HiddenHakIraiTime.Value < Me.HiddenHakIraiUkeDatetimeR.Value Or _
           Me.HiddenHakIraiTime.Value < Me.HiddenHakIraiCanDatetimeR.Value _
        Then
            ButtonHakkouCancel.Disabled = True ' 発行キャンセルボタン
            ButtonHakkouSet.Disabled = True ' 発行セットボタン
            ButtonBukkenTenki.Disabled = True
            ButtonHakkouSet.Disabled = True
            ButtonJyuusyoTenki.Disabled = True
            ButtonHosyouKaisiDateTenki.Disabled = True

            Dim jBn As New Jiban
            Dim noTarget As New Hashtable
            noTarget.Add(divHakkouIrai, True) '発行依頼タブ
            jBn.ChangeDesabledAll(divHakkouIrai, True, noTarget)

            Me.HiddenChkKaisiDate.Value = "1"
        Else
            Me.HiddenChkKaisiDate.Value = "0"
        End If


    End Sub

    ''' <summary>
    ''' コントロールの画面制御/保証情報
    ''' </summary>
    ''' <remarks>コントロールの画面制御を仕様書の優先順に記載する</remarks>
    Public Sub SetEnableControlHosyou()
        '地盤画面共通クラス
        Dim jBn As New Jiban
        '有効化、無効化の対象外にするコントロール郡
        Dim noTarget As New Hashtable
        noTarget.Add(divHosyou, True) '保証情報

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable

        Dim hLogic As New HosyouLogic

        '******************
        '* 非活性化
        '******************

        '解約払戻申請有無＝有
        If SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue <> "" Then
            '基礎報告書
            jSM.Hash2Ctrl(TdKisoHoukokusyo, EarthConst.MODE_VIEW, ht)
            '保証書発行状況
            jSM.Hash2Ctrl(TdHosyousyoHakkouJyoukyou, EarthConst.MODE_VIEW, ht)
            '保証開始日
            jSM.Hash2Ctrl(TdHosyouKaisiDate, EarthConst.MODE_VIEW, ht)
            '保証有無
            jSM.Hash2Ctrl(TdHosyouUmu, EarthConst.MODE_VIEW, ht)
        End If

        '基礎報告書＝未入力
        If SelectKisoHoukokusyo.SelectedValue = "" Then
            '基礎報告書着日
            jSM.Hash2Ctrl(TdKisoHoukokusyoTyakuDate, EarthConst.MODE_VIEW, ht)
        Else
            cl.chgDispSyouhinText(TextKisoHoukokusyoTyakuDate)
        End If

        '発行依頼書＝未入力
        If SelectHakkouIraisyo.SelectedValue = "" Then
            '発行依頼書着日
            jSM.Hash2Ctrl(TdHakkouIraiTyakuDate, EarthConst.MODE_VIEW, ht)
        Else
            cl.chgDispSyouhinText(TextHakkouIraiTyakuDate)
        End If

        '付保証明書FLGは常に非活性
        jSM.Hash2Ctrl(TdFuhoSyoumeisyoFlg, EarthConst.MODE_VIEW, ht)

        '付保証明書発送日が入力されている場合は、付保証明書FLGの自動設定を行なわない
        '加盟店備考設定マスタ.備考種別(=42)のチェック
        If TextFuhoSyoumeisyoHassouDate.Text = "" And hLogic.ExistBikouSyubetu(ucGyoumuKyoutuu.AccKameitenCd.Value) Then
            '付保証明書FLG自動設定フラグ
            HiddenSetFuhoSyoumeisyoFlg.Value = EarthConst.ARI_VAL   '自動設定有り

        End If

    End Sub

    ''' <summary>
    ''' コントロールの画面制御/保証書再発行
    ''' </summary>
    ''' <remarks>コントロールの画面制御を仕様書の優先順に記載する</remarks>
    Public Sub SetEnableControlSh()

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable

        htNotTarget.Add(TextShUriageNengappi.ID, TextShUriageNengappi) '売上年月日
        htNotTarget.Add(TextShHattyuusyoKakutei.ID, TextShHattyuusyoKakutei) '発注書確定
        htNotTarget.Add(TextShHattyuusyoKingaku.ID, TextShHattyuusyoKingaku) '発注書金額
        htNotTarget.Add(TextShHattyuusyoKakuninDate.ID, TextShHattyuusyoKakuninDate) '発注書確認日

        jSM.Hash2Ctrl(TrShSaihakkou, EarthConst.MODE_VIEW, ht) '再発行TR
        jSM.Hash2Ctrl(TrShSyouhin, EarthConst.MODE_VIEW, ht, htNotTarget) '商品TR


        '●優先順1
        '売上処理済(邸別請求テーブル.売上計上FLG＝1)
        If SpanShUriageSyorizumi.InnerHtml = EarthConst.URIAGE_ZUMI Then '変更箇所なしのため、画面から取得
            cl.chgDispSyouhinText(TextSaihakkouRiyuu) '再発行理由

            '●優先順2
        ElseIf TextHosyousyoHakkouDate.Text.Length = 0 Then '保証書発行日＝未入力

            '●優先順3
        ElseIf SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "1" Then '解約払戻申請有無＝有

        Else
            '●優先順4
            If TextSaihakkouDate.Text.Length = 0 Then '再発行日＝未入力
                cl.chgDispSyouhinText(TextSaihakkouDate) '再発行日

            Else

                '●優先順5,6
                If SelectShSeikyuuUmu.SelectedValue = "0" Or SelectShSeikyuuUmu.SelectedValue = "" Then '請求 無or空白
                    cl.chgDispSyouhinText(TextSaihakkouDate) '再発行日
                    cl.chgDispSyouhinPull(SelectShSeikyuuUmu, SpanShSeikyuuUmu) '請求有無
                    cl.chgDispSyouhinText(TextSaihakkouRiyuu) '再発行理由

                    '●優先順7
                ElseIf SelectShSeikyuuUmu.SelectedValue = "1" Then '請求 有

                    cl.chgDispSyouhinText(TextSaihakkouDate) '再発行日
                    cl.chgDispSyouhinPull(SelectShSeikyuuUmu, SpanShSeikyuuUmu) '請求有無
                    cl.chgDispSyouhinText(TextSaihakkouRiyuu) '再発行理由

                    Dim kameitenlogic As New KameitenSearchLogic
                    Dim blnTorikesi As Boolean = False
                    Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                    '(*6)加盟店が取消の場合、他請求3系列以外の扱いとする
                    If record.Torikesi <> 0 Then
                        record.KeiretuCd = ""
                    End If

                    If TextShSeikyuusaki.Text = EarthConst.SEIKYU_TASETU And cl.getKeiretuFlg(record.KeiretuCd) = False Then

                        cl.chgDispSyouhinText(TextShJituseikyuuKingaku) '実請求税抜金額
                        cl.chgDispSyouhinText(TextShSeikyuusyoHakkouDate) '請求書発行日

                        '●優先順8
                    Else
                        cl.chgDispSyouhinText(TextShJituseikyuuKingaku) '実請求税抜金額
                        cl.chgDispSyouhinText(TextShSeikyuusyoHakkouDate) '請求書発行日
                        cl.chgDispSyouhinText(TextShKoumutenSeikyuuKingaku) '工務店請求税抜金額
                    End If

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' コントロールの画面制御/解約払戻
    ''' </summary>
    ''' <remarks>コントロールの画面制御を仕様書の優先順に記載する</remarks>
    Public Sub SetEnableControlKy()

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable

        htNotTarget.Add(TextShUriageNengappi.ID, TextShUriageNengappi) '売上年月日
        htNotTarget.Add(TextShHattyuusyoKakutei.ID, TextShHattyuusyoKakutei) '発注書確定
        htNotTarget.Add(TextShHattyuusyoKingaku.ID, TextShHattyuusyoKingaku) '発注書金額
        htNotTarget.Add(TextShHattyuusyoKakuninDate.ID, TextShHattyuusyoKakuninDate) '発注書確認日

        jSM.Hash2Ctrl(TdKyKaiyakuHaraimodosiSinseiUmu, EarthConst.MODE_VIEW, ht) '解約.解約払戻申請有無
        jSM.Hash2Ctrl(TdKySeikyuusyoHakkouDate, EarthConst.MODE_VIEW, ht) '解約.請求書発行日


        '●優先順1
        '売上処理済(邸別請求テーブル.売上計上FLG＝1)
        If SpanKyKaiyakuUriageSyorizumi.InnerHtml = EarthConst.URIAGE_ZUMI Then '変更箇所なしのため、画面から取得

            '●優先順2
        ElseIf TextHosyousyoHakkouDate.Text <> "" Then '保証書発行日＝入力

        Else '●優先順3

            '解約払戻申請有無
            If SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "1" Then '有

                cl.chgDispSyouhinPull(SelectKyKaiyakuHaraimodosiSinseiUmu, SpanKyKaiyakuHaraimodosiSinseiUmu) '解約払戻申請有無
                cl.chgDispSyouhinText(TextKySeikyuusyoHakkouDate) '請求書発行日


                '■保証項目・非活性化※3

                '基礎報告書
                jSM.Hash2Ctrl(TdKisoHoukokusyo, EarthConst.MODE_VIEW, ht)
                '保証書発行状況
                jSM.Hash2Ctrl(TdHosyousyoHakkouJyoukyou, EarthConst.MODE_VIEW, ht)
                '保証書発行日
                jSM.Hash2Ctrl(TdHosyousyoHakkouDate, EarthConst.MODE_VIEW, ht)
                '保証開始日
                jSM.Hash2Ctrl(TdHosyouKaisiDate, EarthConst.MODE_VIEW, ht)
                '保証有無
                jSM.Hash2Ctrl(TdHosyouUmu, EarthConst.MODE_VIEW, ht)
                '保証期間
                jSM.Hash2Ctrl(TdHosyouKikan, EarthConst.MODE_VIEW, ht)
                '保証書再発行・非活性化▲
                jSM.Hash2Ctrl(TrShSyouhin, EarthConst.MODE_VIEW, ht, htNotTarget) '再発行.商品TR

                '●優先順4
            ElseIf SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" Then '空白

                cl.chgDispSyouhinPull(SelectKyKaiyakuHaraimodosiSinseiUmu, SpanKyKaiyakuHaraimodosiSinseiUmu) '解約払戻申請有無

                '■保証項目※3

                '基礎報告書
                cl.chgDispSyouhinPull(SelectKisoHoukokusyo, SpanKisoHoukokusyo)
                '保証書発行状況
                cl.chgDispSyouhinPull(SelectHosyousyoHakkouJyoukyou, SpanHosyousyoHakkouJyoukyou)
                '保証書発行日
                cl.chgDispSyouhinText(TextHosyousyoHakkouDate)
                '保証開始日
                cl.chgDispSyouhinText(TextHosyouKaisiDate)
                '保証有無
                cl.chgDispSyouhinPull(SelectHosyouUmu, SpanHosyouUmu)
                '保証期間
                If TextHosyousyoHakkouDate.Text <> "" Then
                    cl.chgVeiwMode(TextHosyouKikan) ' 非活性
                Else
                    cl.chgDispSyouhinText(Me.TextHosyouKikan)
                End If

            End If
        End If


    End Sub

    ''' <summary>
    ''' コントロールの画面制御/保証書発行日
    ''' </summary>
    ''' <remarks>コントロールの画面制御を仕様書の優先順に記載する</remarks>
    Public Sub SetEnableControlHhDate(Optional ByVal strBukkenJyky As String = "")

        Dim blnFlg As Boolean = False
        Dim vntNyuukinNo As Integer = 0 '入金確認条件NO
        Dim intKoujiFlg As Integer = 0 '工事判定FLG
        Dim KisoSiyouLogic As New KisoSiyouLogic
        Dim hLogic As New HosyouLogic

        ' (1) 商品設定状況=保証商品なし
        If Me.SpanHosyouSyouhinUmu.InnerHtml = EarthConst.DISP_HOSYOU_NASI_HOSYOU_SYOUHIN_UMU Then
            ' (1)-(3) は FALSE なので明示的にセット不要 
        Else
            ' (3) 解約=未入力　かつ　保証書再発行=未売上（売上済でない）
            If SelectKyKaiyakuHaraimodosiSinseiUmu.SelectedValue = "" And _
                        SpanShUriageSyorizumi.InnerHtml <> EarthConst.URIAGE_ZUMI Then

                ' (12) 発行依頼書有無=未設定
                If SelectHakkouIraisyo.SelectedValue = "" Then
                    blnFlg = True '入力可

                Else
                    ' 入金確認条件NOの取得
                    vntNyuukinNo = hLogic.GetNyuukinKakuninJoukenNo(ucGyoumuKyoutuu.AccKameitenCd.Value, ucGyoumuKyoutuu.Kubun)

                    intKoujiFlg = 0 '工事判定FLG

                    Dim record As KisoSiyouRecord = KisoSiyouLogic.GetKisoSiyouRec(HiddenHantei1CdOld.Value)
                    If record Is Nothing Then
                        intKoujiFlg = 0
                    Else
                        intKoujiFlg = record.KojHanteiFlg
                    End If

                    If intKoujiFlg <> 1 Then
                        ' (4)-(6)
                        '工事判定<>1
                        Select Case vntNyuukinNo
                            Case 0, 4, 5
                                blnFlg = funChkPtnHosyousyoHakkouYMD("B")
                            Case 1, 3, 6
                                blnFlg = funChkPtnHosyousyoHakkouYMD("A")
                            Case 2
                                blnFlg = funChkPtnHosyousyoHakkouYMD("E")
                        End Select

                    Else '工事判定FLG＝1
                        ' (7)-(11)
                        '工事判定=1 かつ 改良工事日=入力 かつ 工事報告書受理日=入力
                        If TextKairyouKoujiDate.Text <> "" And TextKoujiHoukokusyoJuriDate.Text <> "" Then

                            Select Case vntNyuukinNo
                                Case 0, 4
                                    blnFlg = funChkPtnHosyousyoHakkouYMD("D")
                                Case 1, 6
                                    blnFlg = funChkPtnHosyousyoHakkouYMD("A")
                                Case 2
                                    blnFlg = funChkPtnHosyousyoHakkouYMD("F")
                                Case 3
                                    blnFlg = funChkPtnHosyousyoHakkouYMD("C")
                                Case 5
                                    blnFlg = funChkPtnHosyousyoHakkouYMD("B")
                            End Select

                        End If
                    End If

                End If
                ' (13) 物件状況 = 1
                If strBukkenJyky = "1" Then
                    blnFlg = True
                End If
            End If
        End If

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        jSM.Hash2Ctrl(TdHosyousyoHakkouDate, EarthConst.MODE_VIEW, ht) '保証書発行日

        If blnFlg = True Then '入力可
            '保証書発行日
            cl.chgDispSyouhinText(TextHosyousyoHakkouDate) '活性化
        End If

    End Sub

    ''' <summary>
    ''' 保証書発行日入力制御用チェックパターン
    ''' </summary>
    ''' <param name="strPat">チェックパターン</param>
    ''' <returns>True/False (入力可/入力不可)</returns>
    ''' <remarks></remarks>
    Public Function funChkPtnHosyousyoHakkouYMD(ByVal strPat As String) As Boolean

        Select Case strPat
            Case "A"
                'チェックなし
                Return True

            Case "B"
                '調査代金残額チェック
                If TextTyousaZangaku.Text = "0" Then
                    '調査代金残額=0
                    Return True
                End If

            Case "C"
                '工事代金残額チェック
                If TextKoujiZangaku.Text = "0" Then
                    '工事代金残額=0
                    Return True
                End If

            Case "D"
                '調査代金残額チェック+工事代金残額チェック
                If TextTyousaZangaku.Text = "0" And TextKoujiZangaku.Text = "0" Then
                    '調査代金残額=0
                    Return True
                End If

            Case "E"
                '調査発注書確認日チェック
                If HiddenTyousaHattyuusyoKakuninDateFlg.Value = "1" Then
                    Return True
                End If

            Case "F"
                '調査・工事発注書確認日チェック
                If HiddenTyousaHattyuusyoKakuninDateFlg.Value = "1" _
                    And HiddenKoujiHattyuusyoKakuninDateFlg.Value = "1" Then

                    Return True
                End If

            Case Else
                Return False
        End Select

        Return False

    End Function

#End Region

#Region "金額設定"

    ''' <summary>
    ''' 金額設定(保証書再発行)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKingaku(ByVal emType As EnumKingakuType, Optional ByVal blnZeigaku As Boolean = False)
        ' 税抜価格（実請求金額）
        Dim zeinuki_ctrl As TextBox
        ' 消費税率
        Dim zeiritu_ctrl As HtmlInputHidden
        ' 消費税額
        Dim zeigaku_ctrl As TextBox
        ' 税込金額
        Dim zeikomi_gaku_ctrl As TextBox

        Select Case emType
            Case EnumKingakuType.Saihakkou '再発行
                zeinuki_ctrl = TextShJituseikyuuKingaku
                zeiritu_ctrl = HiddenShZeiritu
                zeigaku_ctrl = TextShSyouhizei
                zeikomi_gaku_ctrl = TextShZeikomiKingaku

            Case EnumKingakuType.KaiyakuHaraimodosi '解約払戻
                zeinuki_ctrl = TextKyJituseikyuuKingaku
                zeiritu_ctrl = HiddenKyZeiritu
                zeigaku_ctrl = TextKySyouhizei
                zeikomi_gaku_ctrl = TextKyZeikomiKingaku

            Case EnumKingakuType.None '指定なし
                Exit Sub
            Case Else
                Exit Sub
        End Select

        Dim zeinuki As Integer = 0
        Dim zeiritu As Decimal = 0
        Dim zeigaku As Integer = 0
        Dim zeikomi_gaku As Integer = 0

        If zeinuki_ctrl.Text = "" Then '未入力
            zeinuki_ctrl.Text = ""
            zeigaku_ctrl.Text = ""
            zeikomi_gaku_ctrl.Text = ""

        Else '入力あり
            cl.SetDisplayString(CInt(zeinuki_ctrl.Text), zeinuki)
            cl.SetDisplayString(zeiritu_ctrl.Value, zeiritu)

            zeinuki = IIf(zeinuki = Integer.MinValue, 0, zeinuki)
            zeiritu = IIf(zeiritu = Decimal.MinValue, 0, zeiritu)

            If blnZeigaku Then '消費税額の値で計算
                cl.SetDisplayString(CInt(zeigaku_ctrl.Text), zeigaku)
            Else
                zeigaku = Fix(zeinuki * zeiritu)
            End If
            zeikomi_gaku = zeinuki + zeigaku

            zeigaku_ctrl.Text = Format(zeigaku, EarthConst.FORMAT_KINGAKU_1) '消費税
            zeikomi_gaku_ctrl.Text = Format(zeikomi_gaku, EarthConst.FORMAT_KINGAKU_1) '税込金額

        End If

        Select Case emType
            Case EnumKingakuType.Saihakkou '再発行
                ' 入金額/残額をセット
                CalcZangakuSh(zeikomi_gaku_ctrl.Text)

            Case EnumKingakuType.KaiyakuHaraimodosi '解約払戻
                Exit Sub
            Case EnumKingakuType.None '指定なし
                Exit Sub
            Case Else
                Exit Sub
        End Select

    End Sub

    ''' <summary>
    ''' 商品テーブル金額エリアの0クリア/再発行
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub Clear0SyouhinTableSh()
        '金額の0クリア
        '●自動設定
        TextShKoumutenSeikyuuKingaku.Text = "0" '工務店請求金額
        TextShJituseikyuuKingaku.Text = "0" '実請求金額
        TextShSyouhizei.Text = "0" '消費税
        TextShZeikomiKingaku.Text = "0" '税込金額
        TextShSeikyuusyoHakkouDate.Text = "" '請求書発行日
        TextShUriageNengappi.Text = Date.Now.ToString("yyyy/MM/dd") '売上年月日
        '発注書確定
        If TextShHattyuusyoKakutei.Text.Length = 0 Then '(*5)発注書確定が空白の場合は、「0：未確定」を設定する
            TextShHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
        End If
    End Sub

    ''' <summary>
    ''' 商品テーブル金額エリアの空白クリア/再発行
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearBlnkSyouhinTableSh()
        '空白クリア
        '●自動設定
        SelectShSeikyuuUmu.SelectedValue = "" '請求有無
        SpanShSeikyuuUmu.Style.Add("display", "none") 'SPAN請求有無
        SpanShSeikyuuUmu.InnerHtml = "" 'SPAN請求有無

        TextShSyouhinCd.Text = "" '商品コード
        SpanShSyouhinMei.InnerHtml = "" '商品名
        TextShKoumutenSeikyuuKingaku.Text = "" '工務店請求金額
        TextShJituseikyuuKingaku.Text = "" '実請求金額
        TextShSyouhizei.Text = "" '消費税
        TextShZeikomiKingaku.Text = "" '税込金額
        TextShSeikyuusyoHakkouDate.Text = "" '請求書発行日
        TextShUriageNengappi.Text = "" '売上年月日
        TextShHattyuusyoKakutei.Text = "" '発注書確定
        Me.HiddenShSiireSyouhiZei.Value = "" '仕入消費税額
        Me.HiddenShSiireGaku.Value = ""     '仕入金額
        Me.HiddenShSyouhinCdOld.Value = "" ' 商品コード変更前

        '金額の再計算
        SetKingaku(EnumKingakuType.Saihakkou)

    End Sub

    ''' <summary>
    ''' 商品テーブル金額エリアの空白クリア/解約払戻
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearBlnkSyouhinTableKy()
        '空白クリア
        '●自動設定
        TextKySyouhinCd.Text = "" '商品コード
        TextKyKoumutenSeikyuuKingaku.Text = "" '工務店請求金額
        TextKyJituseikyuuKingaku.Text = "" '実請求金額
        TextKySeikyuusyoHakkouDate.Text = "" '請求書発行日
        TextKyUriageNengappi.Text = "" '売上年月日
        TextKyHattyuusyoKingaku.Text = "" '発注書金額
        TextKyHattyuusyoKakutei.Text = "" '発注書確定
        TextKyHattyuusyoKakuninDate.Text = "" '発注書確認日

        SetKingaku(EnumKingakuType.KaiyakuHaraimodosi) '金額の再計算

        '空白でクリア
        TextKySyouhizei.Text = "" '消費税
        TextKyZeikomiKingaku.Text = "" '税込金額
        Me.HiddenKySiireSyouhiZei.Value = "" '仕入消費税額
        Me.HiddenKySiireGaku.Value = ""     '仕入金額

    End Sub
#End Region

#Region "税率/税区分"

    ''' <summary>
    ''' 税率/税区分をHiddenにセットする/保証書再発行
    ''' </summary>
    ''' <param name="strItemCd"></param>
    ''' <remarks></remarks>
    Protected Sub SetShZeiInfo(ByVal strItemCd As String)
        Dim syouhinRec As New Syouhin23Record

        '商品情報を取得(キー:商品コード)
        syouhinRec = JibanLogic.GetSyouhinInfo(strItemCd, EarthEnum.EnumSyouhinKubun.Hosyousyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If Not syouhinRec Is Nothing Then
            HiddenShZeiritu.Value = syouhinRec.Zeiritu '税率
            HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '税区分
        End If
    End Sub

    ''' <summary>
    ''' 税率/税区分をHiddenにセットする/解約払戻
    ''' </summary>
    ''' <param name="strItemCd"></param>
    ''' <remarks></remarks>
    Protected Sub SetKyZeiInfo(ByVal strItemCd As String)
        Dim syouhinRec As New Syouhin23Record

        '商品情報を取得(キー:商品コード)
        syouhinRec = JibanLogic.GetSyouhinInfo(strItemCd, EarthEnum.EnumSyouhinKubun.Kaiyaku, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If Not syouhinRec Is Nothing Then
            HiddenKyZeiritu.Value = syouhinRec.Zeiritu '税率
            HiddenKyZeiKbn.Value = syouhinRec.ZeiKbn '税区分
        End If
    End Sub

#End Region

#Region "入金額/残額"

    ''' <summary>
    ''' 入金額・残額を表示します<br/>
    ''' 税込売上金額合計のみ変更し、再計算します<br/>
    ''' </summary>
    ''' <param name="strUriageGoukeiGaku">税込売上金額合計</param>
    ''' <remarks></remarks>
    Public Sub CalcZangakuSh(ByVal strUriageGoukeiGaku As String)

        ' 入金額（税込）
        Dim nyuukingaku_ctrl As TextBox
        ' 残額
        Dim zangakuu_ctrl As TextBox

        nyuukingaku_ctrl = TextShNyuukingaku '再発行.入金額
        zangakuu_ctrl = TextShZangaku '再発行.残額

        If strUriageGoukeiGaku = "" Then
            ' 残額
            zangakuu_ctrl.Text = "0"

        Else
            Dim uriageGoukeiGaku As Integer = Integer.MinValue

            uriageGoukeiGaku = CInt(strUriageGoukeiGaku)

            Dim strNyuukinGaku As String = IIf(nyuukingaku_ctrl.Text.Replace(",", "").Trim() = "", _
                                            "0", _
                                            nyuukingaku_ctrl.Text.Replace(",", "").Trim())

            Dim nyuukinGaku As Integer = Integer.Parse(strNyuukinGaku)

            ' NULLは０にする
            uriageGoukeiGaku = IIf(uriageGoukeiGaku = Integer.MinValue, 0, uriageGoukeiGaku)

            ' 残額
            zangakuu_ctrl.Text = (uriageGoukeiGaku - nyuukinGaku).ToString(EarthConst.FORMAT_KINGAKU_1)

        End If

    End Sub

    ''' <summary>
    ''' 入金額・残額を表示します
    ''' </summary>
    ''' <param name="uriageGoukeiGaku">税込売上金額合計</param>
    ''' <param name="nyuukinGaku">入金額</param>
    ''' <remarks></remarks>
    Public Sub CalcZangakuSh( _
                             ByVal uriageGoukeiGaku As Integer _
                            , ByVal nyuukinGaku As Integer _
                            )

        ' 入金額（税込）
        Dim nyuukingaku_ctrl As TextBox
        ' 残額
        Dim zangakuu_ctrl As TextBox

        nyuukingaku_ctrl = TextShNyuukingaku '再発行.入金額
        zangakuu_ctrl = TextShZangaku '再発行.残額

        ' NULLは０にする
        uriageGoukeiGaku = IIf(uriageGoukeiGaku = Integer.MinValue, 0, uriageGoukeiGaku)
        nyuukinGaku = IIf(nyuukinGaku = Integer.MinValue, 0, nyuukinGaku)

        ' 入金額
        nyuukingaku_ctrl.Text = nyuukinGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        ' 残額
        zangakuu_ctrl.Text = (uriageGoukeiGaku - nyuukinGaku).ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub

#End Region

    ''' <summary>
    '''商品毎の請求・仕入先が変更されていないかをチェックし、
    '''変更されている場合情報の再取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setSeikyuuSiireHenkou(ByVal sender As Object, ByVal e As System.EventArgs)
        '請求先、仕入先が変更された行をチェックし、存在した場合は
        '各行の請求有無変更時の処理を実行する

        '**************************
        ' 保証書再発行
        '**************************
        '請求仕入変更リンクUC内のチェックフラグHiddenを参照し、フラグが立っている場合は処理実施
        If Me.ucSeikyuuSiireLinkSai.AccHiddenChkSeikyuuSakiChg.Value = "1" Then

            '請求有無変更時処理
            Me.SelectShSeikyuu_SelectedIndexChanged(sender, e)

            'フラグ初期化
            Me.ucSeikyuuSiireLinkSai.AccHiddenChkSeikyuuSakiChg.Value = String.Empty

            'フォーカスは請求仕入変更リンク
            setFocusAJ(Me.ucSeikyuuSiireLinkSai.AccLinkSeikyuuSiireHenkou)

            '変更された商品が有った場合、UpdatePanelをUpdate
            Me.UpdatePanelHall.Update()

        End If

        '**************************
        ' 解約払戻
        '**************************
        '請求仕入変更リンクUC内のチェックフラグHiddenを参照し、フラグが立っている場合は処理実施
        If Me.ucSeikyuuSiireLinkKai.AccHiddenChkSeikyuuSakiChg.Value = "1" Then

            '解約払戻変更時処理
            Me.SelectKyKaiyakuHaraimodosiSinseiUmu_ChangeSub(sender, e)

            'フラグ初期化
            Me.ucSeikyuuSiireLinkKai.AccHiddenChkSeikyuuSakiChg.Value = String.Empty

            'フォーカスは請求仕入変更リンク
            setFocusAJ(Me.ucSeikyuuSiireLinkKai.AccLinkSeikyuuSiireHenkou)

            '変更された商品が有った場合、UpdatePanelをUpdate
            Me.UpdatePanelHall.Update()

        End If

    End Sub

#Region "商品の自動設定"

    ''' <summary>
    ''' 商品の基本情報をセットする(保証書再発行)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSyouhinInfoSh()
        '直接請求、他請求の情報を取得
        Dim syouhinRec As Syouhin23Record

        '商品コード/商品名の自動設定
        syouhinRec = JibanLogic.GetSyouhinInfo("", EarthEnum.EnumSyouhinKubun.Hosyousyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If syouhinRec Is Nothing Then
            TextShSyouhinCd.Text = "" '商品コード
            SpanShSyouhinMei.InnerHtml = "" '商品名
            Me.HiddenShSyouhinCdOld.Value = ""
        Else
            TextShSyouhinCd.Text = cl.GetDispStr(syouhinRec.SyouhinCd) '商品コード
            SpanShSyouhinMei.InnerHtml = cl.GetDispStr(syouhinRec.SyouhinMei) '商品名
            HiddenShZeiritu.Value = syouhinRec.Zeiritu '税率
            HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '税区分

            '画面上で請求先が指定されている場合、レコードの請求先を上書き
            If Me.ucSeikyuuSiireLinkSai.AccSeikyuuSakiCd.Value <> String.Empty Then
                '請求先をレコードにセット
                syouhinRec.SeikyuuSakiCd = Me.ucSeikyuuSiireLinkSai.AccSeikyuuSakiCd.Value
                syouhinRec.SeikyuuSakiBrc = Me.ucSeikyuuSiireLinkSai.AccSeikyuuSakiBrc.Value
                syouhinRec.SeikyuuSakiKbn = Me.ucSeikyuuSiireLinkSai.AccSeikyuuSakiKbn.Value
            End If
            Me.TextShSeikyuusaki.Text = syouhinRec.SeikyuuSakiType '●請求先

        End If

    End Sub

    ''' <summary>
    ''' 商品の基本情報をセットする(解約払戻)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSyouhinInfoKy()
        '直接請求、他請求の情報を取得
        Dim syouhinRec As Syouhin23Record

        '商品コード/商品名の自動設定
        syouhinRec = JibanLogic.GetSyouhinInfo("", EarthEnum.EnumSyouhinKubun.Kaiyaku, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If syouhinRec Is Nothing Then
            TextKySyouhinCd.Text = "" '商品コード
        Else
            TextKySyouhinCd.Text = cl.GetDispStr(syouhinRec.SyouhinCd) '商品コード
            HiddenKyZeiritu.Value = syouhinRec.Zeiritu '税率
            HiddenKyZeiKbn.Value = syouhinRec.ZeiKbn '税区分

            '画面上で請求先が指定されている場合、レコードの請求先を上書き
            If Me.ucSeikyuuSiireLinkKai.AccSeikyuuSakiCd.Value <> String.Empty Then
                '請求先をレコードにセット
                syouhinRec.SeikyuuSakiCd = Me.ucSeikyuuSiireLinkKai.AccSeikyuuSakiCd.Value
                syouhinRec.SeikyuuSakiBrc = Me.ucSeikyuuSiireLinkKai.AccSeikyuuSakiBrc.Value
                syouhinRec.SeikyuuSakiKbn = Me.ucSeikyuuSiireLinkKai.AccSeikyuuSakiKbn.Value
            End If
            Me.TextKySeikyuusaki.Text = syouhinRec.SeikyuuSakiType '●請求先
        End If

    End Sub

#End Region

#Region "保証書発行状況による保証有無"

    ''' <summary>
    ''' 保証書発行状況による保証有無の表示切替
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChgDispHosyouUmu()
        Dim strHosyousyoHakJyky As String = Me.SelectHosyousyoHakkouJyoukyou.SelectedValue

        '画面.保証書発行状況=入力の場合
        If strHosyousyoHakJyky <> String.Empty Then

            '●保証書発行状況による保証有無の表示切替
            If cbLogic.GetHosyousyoHakJykyHosyouUmu(strHosyousyoHakJyky) = "0" Then
                Me.SpanHosyousyoHakJykyHosyouUmu.InnerHtml = EarthConst.DISP_HOSYOU_NASI_HOSYOUSYO_HAK_JYKY
                Me.SpanHosyousyoHakJykyHosyouUmu.Style("color") = CSS_COLOR_RED

            ElseIf cbLogic.GetHosyousyoHakJykyHosyouUmu(strHosyousyoHakJyky) = "1" Then
                Me.SpanHosyousyoHakJykyHosyouUmu.InnerHtml = EarthConst.DISP_HOSYOU_ARI_HOSYOUSYO_HAK_JYKY
                Me.SpanHosyousyoHakJykyHosyouUmu.Style("color") = CSS_COLOR_BLUE

            Else
                Me.SpanHosyousyoHakJykyHosyouUmu.InnerHtml = String.Empty

            End If

        Else '未入力の場合

            '●保証商品有無による保証有無の表示切替
            If Me.SpanHosyouSyouhinUmu.InnerHtml = EarthConst.DISP_HOSYOU_NASI_HOSYOU_SYOUHIN_UMU Then
                Me.SpanHosyousyoHakJykyHosyouUmu.InnerHtml = EarthConst.DISP_HOSYOU_NASI_HOSYOUSYO_HAK_JYKY
                Me.SpanHosyousyoHakJykyHosyouUmu.Style("color") = CSS_COLOR_RED
            Else
                Me.SpanHosyousyoHakJykyHosyouUmu.InnerHtml = EarthConst.DISP_HOSYOU_ARI_HOSYOUSYO_HAK_JYKY
                Me.SpanHosyousyoHakJykyHosyouUmu.Style("color") = CSS_COLOR_BLUE
            End If
        End If


    End Sub
#End Region

End Class