
Partial Public Class SearchBukken
    Inherits System.Web.UI.Page

    Dim userInfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cookieKey As String = "earth_kensaku_checked"
    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic
    Dim cl As New CommonLogic

    Const pStrSpace As String = EarthConst.HANKAKU_SPACE

    ''' <summary>
    ''' ページロード
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
        jBn.UserAuth(userInfo)

        '認証結果によって画面表示を切替える
        If userInfo IsNot Nothing Then

        Else
            Response.Redirect(UrlConst.MAIN)
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '一括変更画面起動ボタン設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '一括変更画面起動上限物件数セット(Web.configから取得)
        HiddenIkkatuKidouMax.Value = EarthConst.MAX_IKKATU_KIDOU
        '一括変更画面起動ボタン設定
        ButtonIkkatuTysSyouhin.Disabled = True
        ButtonIkkatuKihon.Disabled = True
        If userInfo.IraiGyoumuKengen = -1 Or _
           userInfo.HoukokusyoGyoumuKengen = -1 Or _
           userInfo.KoujiGyoumuKengen = -1 Or _
           userInfo.HosyouGyoumuKengen = -1 Or _
           userInfo.KekkaGyoumuKengen = -1 Then
            '４業務権限（依頼・報告・工事・保証・結果）の何れかがある場合、ボタンを有効化
            ButtonIkkatuKihon.Disabled = False
        End If
        If userInfo.IraiGyoumuKengen = -1 Then
            '依頼がある場合、ボタンを有効化
            ButtonIkkatuTysSyouhin.Disabled = False
        End If
        '初期状態ではボタンは非表示＆onclickイベントハンドラはクリア
        ButtonIkkatuKihon.Visible = False
        ButtonIkkatuTysSyouhin.Visible = False
        ButtonIkkatuKihon.Attributes.Remove("onclick")
        ButtonIkkatuTysSyouhin.Attributes.Remove("onclick")

        '各テーブルの表示状態を切り替える
        Me.kensakuInfo.Style("display") = Me.HiddenKensakuInfoStyle.Value

        If IsPostBack = False Then

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            ' 区分コンボにデータをバインドする
            Dim helper As New DropDownHelper
            Dim kbn_logic As New KubunLogic
            helper.SetDropDownList(cmbKubun_1, DropDownHelper.DropDownType.Kubun)
            helper.SetDropDownList(cmbKubun_2, DropDownHelper.DropDownType.Kubun)
            helper.SetDropDownList(cmbKubun_3, DropDownHelper.DropDownType.Kubun)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            setDispAction()

            'フォーカス設定
            kubun_all.Focus()

        Else

        End If

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()
        Dim today As Date = DateAndTime.Today   '本日日付オブジェクト

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 検索実行ボタン関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '検索実行ボタンのイベントハンドラを設定
        btnSearch.Attributes("onclick") = "checkJikkou();"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 区分、全区分関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '区分プルダウン、全区分チェックボックスのイベントハンドラを設定
        cmbKubun_1.Attributes("onchange") = "setKubunVal();"
        cmbKubun_2.Attributes("onchange") = "setKubunVal();"
        cmbKubun_3.Attributes("onchange") = "setKubunVal();"
        kubun_all.Attributes("onclick") = "setKubunVal();"
        '画面起動時デフォルトは「全区分」にチェック
        If IsPostBack = False Then
            kubun_all.Checked = True
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 対象範囲関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '対象範囲の年指定を設定(現在年-2)
        haniYear.InnerHtml = today.Year - 2
        '対象範囲のラジオボタンが設定された場合にコード値をhiddenに格納するイベントハンドラを設定
        Dim tmpHanniScr = "actNoHaniRadio(this);"
        rdo_hanni0.Attributes("onclick") = tmpHanniScr
        rdo_hanni1.Attributes("onclick") = tmpHanniScr

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 保証書NO自動設定関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '保証書NO自動設定ラジオボタンのイベントハンドラを設定
        Dim tmpDate As New Date
        Dim val12From As String = DateAdd("m", -11, today).ToString("yyyyMM0001")   '過去12ヶ月
        Dim val6From As String = DateAdd("m", -5, today).ToString("yyyyMM0001")     '過去6ヶ月
        Dim val3From As String = DateAdd("m", -2, today).ToString("yyyyMM0001")     '過去3ヶ月
        Dim val2From As String = DateAdd("m", -1, today).ToString("yyyyMM0001")     '過去2ヶ月
        Dim val0From As String = DateAdd("m", 0, today).ToString("yyyyMM0001")      '当月
        Dim valTo As String = today.ToString("yyyyMM9999")                          'To

        '保証書NO自動設定スクリプトテンポラリ
        Dim tmpHosScr = "actNoAutoRadio(this,'{0}','{1}');"

        '保証書NO自動設定イベントハンドラ設定
        hoshouNoSet_12.Attributes("onclick") = String.Format(tmpHosScr, val12From, valTo)   '過去12ヶ月
        hoshouNoSet_6.Attributes("onclick") = String.Format(tmpHosScr, val6From, valTo)     '過去6ヶ月
        hoshouNoSet_3.Attributes("onclick") = String.Format(tmpHosScr, val3From, valTo)     '過去3ヶ月
        hoshouNoSet_2.Attributes("onclick") = String.Format(tmpHosScr, val2From, valTo)     '過去2ヶ月
        hoshouNoSet_0.Attributes("onclick") = String.Format(tmpHosScr, val0From, valTo)     '当月

        'マスタ検索系コード入力項目イベントハンドラ設定
        kameitenCd.Attributes("onblur") = "checkNumber(this);clrKameitenInfo(this);"
        keiretuCd.Attributes("onblur") = "clrName(this,'" & keiretuNm.ClientID & "');"
        eigyousyoCd.Attributes("onblur") = "clrName(this,'" & eigyousyoNm.ClientID & "');"
        tyousakaisyaCd.Attributes("onblur") = "checkNumber(this);clrName(this,'" & tyousakaisyaNm.ClientID & "');"
        kojiKaishaCd.Attributes("onblur") = "checkNumber(this);clrName(this,'" & kojiKaishaNm.ClientID & "');"

        '分譲コードの入力チェックイベント
        Me.TextBunjouCd.Attributes("onblur") = "if(checkNumber(this)) checkMinus(this);"

        'クッキーからデフォルト起動画面選択状態を取得
        Dim Cookie As HttpCookie
        If Request.Cookies(cookieKey) IsNot Nothing Then
            Cookie = Request.Cookies(cookieKey)
            chkHyoujiGamen1.Checked = Cookie.Values(chkHyoujiGamen1.ID)
            chkHyoujiGamen2.Checked = Cookie.Values(chkHyoujiGamen2.ID)
            chkHyoujiGamen3.Checked = Cookie.Values(chkHyoujiGamen3.ID)
            chkHyoujiGamen4.Checked = Cookie.Values(chkHyoujiGamen4.ID)
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 機能別テーブルの表示切替
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '検索条件
        Me.AKensakuInfo.HRef = "JavaScript:changeDisplay('" & Me.kensakuInfo.ClientID & "');SetDisplayStyle('" & HiddenKensakuInfoStyle.ClientID & "','" & Me.kensakuInfo.ClientID & "');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 東西フラグのチェック解除チェック
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        checkTouzaiFlg()

    End Sub

    ''' <summary>
    ''' ラジオボタンをダブルクリックでダミーのラジオボタンを選択＝＞チェック解除用
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkTouzaiFlg()
        'ラジオボタンをダブルクリックすると、ダミーのラジオボタンを選択する＝＞チェック解除用
        Dim tmpScript As String = "objEBI('" & Me.rdo_TouzaiFlg_dummy.ClientID & "').click();"
        Me.rdo_TouzaiFlg_0.Attributes("ondblclick") = tmpScript
        Me.rdo_TouzaiFlg_1.Attributes("ondblclick") = tmpScript
    End Sub

    ''' <summary>
    ''' 検索実行時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick

        ' 検索ボタンにフォーカス
        btnSearch.Focus()

        Dim rec As New JibanKeyRecord

        'ボタン押下時点でのデフォルト起動画面選択状態をクッキーに保存
        Dim Cookie As HttpCookie = New HttpCookie(cookieKey)
        Cookie.Values.Add(chkHyoujiGamen1.ID, chkHyoujiGamen1.Checked.ToString())
        Cookie.Values.Add(chkHyoujiGamen2.ID, chkHyoujiGamen2.Checked.ToString())
        Cookie.Values.Add(chkHyoujiGamen3.ID, chkHyoujiGamen3.Checked.ToString())
        Cookie.Values.Add(chkHyoujiGamen4.ID, chkHyoujiGamen4.Checked.ToString())
        Cookie.Expires = DateTime.MaxValue ' 永続的保存クッキー
        Response.AppendCookie(Cookie)

        ' 区分が条件に入るのは "全て以外" のとき
        If kubun_all.Checked = False Then
            ' 区分1
            rec.Kbn1 = IIf(cmbKubun_1.SelectedValue <> "", cmbKubun_1.SelectedValue, String.Empty)
            ' 区分2
            rec.Kbn2 = IIf(cmbKubun_2.SelectedValue <> "", cmbKubun_2.SelectedValue, String.Empty)
            ' 区分3
            rec.Kbn3 = IIf(cmbKubun_3.SelectedValue <> "", cmbKubun_3.SelectedValue, String.Empty)
        End If
        ' 東_西日本フラグ
        rec.TouzaiFlg = IIf(Me.rdo_TouzaiFlg_0.Checked = True, rdo_TouzaiFlg_0.Value, IIf(Me.rdo_TouzaiFlg_1.Checked = True, rdo_TouzaiFlg_1.Value, String.Empty))
        ' 保証書NO 範囲指定
        rec.HosyousyoNoHani = IIf(rdo_hanni1.Checked = True, 1, Integer.MinValue)
        ' 保証書NO From
        rec.HosyousyoNoFrom = IIf(hoshouNo_from.Value <> "", hoshouNo_from.Value, String.Empty)
        ' 保証書NO To
        rec.HosyousyoNoTo = IIf(hoshouNo_to.Value <> "", hoshouNo_to.Value, String.Empty)
        ' 加盟店コード
        rec.KameitenCd = IIf(kameitenCd.Value <> "", kameitenCd.Value, String.Empty)
        ' 加盟店カナ
        rec.TenmeiKana1 = IIf(kameitenKana.Value <> "", kameitenKana.Value, String.Empty)
        ' 系列コード
        rec.KeiretuCd = IIf(keiretuCd.Value <> "", keiretuCd.Value, String.Empty)
        ' 営業所コード
        rec.EigyousyoCd = IIf(eigyousyoCd.Value <> String.Empty, eigyousyoCd.Value, String.Empty)
        ' 調査会社コード・調査会社事業所コード
        rec.TysKaisyaCd = IIf(tyousakaisyaCd.Value <> "", tyousakaisyaCd.Value, String.Empty)
        ' 工事会社コード・工事会社事業所コード
        rec.KojGaisyaCd = IIf(kojiKaishaCd.Value <> "", kojiKaishaCd.Value, String.Empty)
        ' 工事売上年月日 From
        rec.KojUriDateFrom = IIf(TextKoujiUriageDateFrom.Value <> "", TextKoujiUriageDateFrom.Value, DateTime.MinValue)
        ' 工事売上年月日 To
        rec.KojUriDateTo = IIf(TextKoujiUriageDateTo.Value <> "", TextKoujiUriageDateTo.Value, DateTime.MinValue)
        ' 工事完了予定日 From
        rec.KairyKojKanryYoteiDateFrom = IIf(TextKoujiKanryouYoteiDateFrom.Value <> "", TextKoujiKanryouYoteiDateFrom.Value, DateTime.MinValue)
        ' 工事完了予定日 To
        rec.KairyKojKanryYoteiDateTo = IIf(TextKoujiKanryouYoteiDateTo.Value <> "", TextKoujiKanryouYoteiDateTo.Value, DateTime.MinValue)
        ' 施主名
        rec.SesyuMei = IIf(seshuName.Value <> "", seshuName.Value, String.Empty)
        ' 備考
        rec.Bikou = IIf(TextBikou.Value <> "", TextBikou.Value, String.Empty)
        ' 物件住所１＋２
        rec.BukkenJyuusyo12 = IIf(bukkenJyuusho12.Value <> "", bukkenJyuusho12.Value.Replace("％"c, "%"c), String.Empty)
        ' 依頼日 From
        rec.IraiDateFrom = IIf(TextIraiDateFrom.Value <> "", TextIraiDateFrom.Value, DateTime.MinValue)
        ' 依頼日 To
        rec.IraiDateTo = IIf(TextIraiDateTo.Value <> "", TextIraiDateTo.Value, DateTime.MinValue)
        ' 調査希望日 From
        rec.TyousaKibouDateFrom = IIf(TextTyousaKibouDateFrom.Value <> "", TextTyousaKibouDateFrom.Value, DateTime.MinValue)
        ' 調査希望日 To
        rec.TyousaKibouDateTo = IIf(TextTyousaKibouDateTo.Value <> "", TextTyousaKibouDateTo.Value, DateTime.MinValue)
        ' 調査実施日 From
        rec.TyousaJissiDateFrom = IIf(TextTyousaJissiDateFrom.Value <> "", TextTyousaJissiDateFrom.Value, DateTime.MinValue)
        ' 調査実施日 To
        rec.TyousaJissiDateTo = IIf(TextTyousaJissiDateTo.Value <> "", TextTyousaJissiDateTo.Value, DateTime.MinValue)
        ' 保証書発行日 From
        rec.HosyousyoHakkouDateFrom = IIf(TextHosyousyoHakkouDateFrom.Value <> "", TextHosyousyoHakkouDateFrom.Value, DateTime.MinValue)
        ' 保証書発行日 To
        rec.HosyousyoHakkouDateTo = IIf(TextHosyousyoHakkouDateTo.Value <> "", TextHosyousyoHakkouDateTo.Value, DateTime.MinValue)
        ' 承諾書調査日 From
        rec.SyoudakusyoTyousaDateFrom = IIf(TextSyoudakusyoTyousaDateFrom.Value <> "", TextSyoudakusyoTyousaDateFrom.Value, DateTime.MinValue)
        ' 承諾書調査日 To
        rec.SyoudakusyoTyousaDateTo = IIf(TextSyoudakusyoTyousaDateTo.Value <> "", TextSyoudakusyoTyousaDateTo.Value, DateTime.MinValue)
        ' 計画書作成日 From
        rec.KeikakusyoSakuseiDateFrom = IIf(TextKeikakusyoSakuseiDateFrom.Value <> "", TextKeikakusyoSakuseiDateFrom.Value, DateTime.MinValue)
        ' 計画書作成日 To
        rec.KeikakusyoSakuseiDateTo = IIf(TextKeikakusyoSakuseiDateTo.Value <> "", TextKeikakusyoSakuseiDateTo.Value, DateTime.MinValue)
        ' 保証書発行依頼書着日 From
        rec.HosyousyoHakkouIraisyoTyakuDateFrom = IIf(TextHosyousyoHakkouIraisyoTyakuDateFrom.Value <> "", TextHosyousyoHakkouIraisyoTyakuDateFrom.Value, DateTime.MinValue)
        ' 保証書発行依頼書着日 To
        rec.HosyousyoHakkouIraisyoTyakuDateTo = IIf(TextHosyousyoHakkouIraisyoTyakuDateTo.Value <> "", TextHosyousyoHakkouIraisyoTyakuDateTo.Value, DateTime.MinValue)
        ' データ破棄種別
        rec.DataHakiSyubetu = IIf(CheckHakiTaisyou.Checked, CheckHakiTaisyou.Value, Integer.MinValue)
        ' 予約済FLG
        rec.YoyakuZumiFlg = IIf(CheckYoyakuZumi.Checked, CheckYoyakuZumi.Value, Integer.MinValue)
        ' 分譲コード
        rec.BunjouCd = IIf(TextBunjouCd.Value <> "", TextBunjouCd.Value, Integer.MinValue)
        ' 物件名寄コード
        rec.BukkenNayoseCd = IIf(TextNayoseCd.Value <> "", TextNayoseCd.Value, String.Empty)
        ' 契約NO
        rec.KeiyakuNo = IIf(Me.TextKeiyakuNo.Value <> "", TextKeiyakuNo.Value, String.Empty)

        '表示最大件数
        Dim end_count As Integer = IIf(maxSearchCount.Value <> "max", maxSearchCount.Value, EarthConst.MAX_RESULT_COUNT)

        Dim logic As New JibanLogic

        Dim total_count As Integer = 0

        ' 検索実行
        Dim resultArray As List(Of JibanSearchRecord) = logic.GetJibanSearchData(sender, rec, 1, end_count, total_count)

        If total_count = 0 Then
            ' 検索結果ゼロ件の場合、メッセージを表示
            mLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
        ElseIf total_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            Exit Sub
        End If

        ' 検索件数を画面に表示
        Dim displayCount As String = ""
        If end_count <> EarthConst.MAX_RESULT_COUNT And end_count < total_count Then
            resultCount.Style("color") = "red"
            displayCount = end_count & " / " & CommonLogic.Instance.GetDisplayString(total_count)
        Else
            resultCount.Style.Remove("color")
            displayCount = CommonLogic.Instance.GetDisplayString(total_count)
        End If
        resultCount.InnerHtml = displayCount

        ' 結果を画面に表示
        createDataTable(resultArray)

    End Sub

    ''' <summary>
    ''' 選択行ダブルクリック時の実行処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSend_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.ServerClick
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        '検索画面表示用JavaScript『callSearch』を実行
        'Dim targetWin As String = IIf(sendTargetWin.Value <> "", sendTargetWin.Value, "earthMainWindow")
        'Dim tmpScript = "window.open('IraiKakunin.aspx', '" & targetWin & "')"
        'ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callIraiView", tmpScript, True)
    End Sub

    ''' <summary>
    ''' 加盟店検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub kameitenSearch_ServerClick1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kameitenSearch.ServerClick
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim dataArray As New List(Of KameitenSearchRecord)

        Dim total_count As Integer

        Dim tmpScript As String = String.Empty

        If kubunVal.Value = String.Empty And kubun_all.Checked = False Then
            '区分未選択の場合、エラー
            mLogic.AlertMessage(sender, Messages.MSG006E, 0, "error")
            masterAjaxSM.SetFocus(cmbKubun_1)
            Exit Sub
        End If

        ' 取得件数を絞り込む場合、引数を追加してください
        If kameitenCd.Value <> "" Then
            dataArray = kameitenSearchLogic.GetKameitenSearchResult(kubunVal.Value, _
                                                                    kameitenCd.Value, _
                                                                    False, _
                                                                    total_count)
        End If

        If dataArray.Count = 1 Then
            Dim recData As KameitenSearchRecord = dataArray(0)
            kameitenCd.Value = recData.KameitenCd
            kameitenNm.Value = recData.KameitenMei1
            Me.TextTorikesiRiyuu.Text = cl.getTorikesiRiyuu(recData.Torikesi, recData.KtTorikesiRiyuu)

            '加盟店コード/名称/取消理由の文字色スタイル
            cl.setStyleFontColor(Me.kameitenCd.Style, cl.getKameitenFontColor(recData.Torikesi))
            cl.setStyleFontColor(Me.kameitenNm.Style, cl.getKameitenFontColor(recData.Torikesi))
            cl.setStyleFontColor(Me.TextTorikesiRiyuu.Style, cl.getKameitenFontColor(recData.Torikesi))

            'フォーカスセット
            masterAjaxSM.SetFocus(kameitenSearch)
        Else
            'フォーカスセット
            Dim tmpFocusScript = "objEBI('" & kameitenSearch.ClientID & "').focus();"
            '検索画面表示用JavaScript『callSearch』を実行
            tmpScript = "callSearch('" & kubunVal.ClientID & EarthConst.SEP_STRING & kameitenCd.ClientID & "','" & UrlConst.SEARCH_KAMEITEN & "','" & _
                            kameitenCd.ClientID & EarthConst.SEP_STRING & kameitenNm.ClientID & "','" & kameitenSearch.ClientID & "');"

            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' 系列ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub keiretuSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles keiretuSearch.ServerClick
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim keiretuSearchLogic As New KeiretuSearchLogic
        Dim dataArray As New List(Of KeiretuSearchRecord)

        Dim tmpScript As String = String.Empty

        If kubunVal.Value = String.Empty And kubun_all.Checked = False Then
            '区分未選択の場合、エラー
            mLogic.AlertMessage(sender, Messages.MSG006E, 0, "error")
            masterAjaxSM.SetFocus(cmbKubun_1)
            Exit Sub
        End If

        If keiretuCd.Value <> "" Then
            dataArray = keiretuSearchLogic.GetKeiretuSearchResult(kubunVal.Value, keiretuCd.Value, "", False)
        End If

        If dataArray.Count = 1 Then
            Dim recData As KeiretuSearchRecord = dataArray(0)
            keiretuCd.Value = recData.KeiretuCd
            keiretuNm.Value = recData.KeiretuMei

            'フォーカスセット
            masterAjaxSM.SetFocus(keiretuSearch)
        Else
            'フォーカスセット
            Dim tmpFocusScript = "objEBI('" & keiretuSearch.ClientID & "').focus();"
            '検索画面表示用JavaScript『callSearch』を実行
            tmpScript = "callSearch('" & kubunVal.ClientID & EarthConst.SEP_STRING & keiretuCd.ClientID & "','" & UrlConst.SEARCH_KEIRETU & "','" & _
                            keiretuCd.ClientID & EarthConst.SEP_STRING & keiretuNm.ClientID & "','" & keiretuSearch.ClientID & "');"

            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If
    End Sub

    ''' <summary>
    ''' 調査会社検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tyousakaisyaSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tyousakaisyaSearch.ServerClick
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        Dim tmpScript As String = String.Empty

        If tyousakaisyaCd.Value <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetTyousakaisyaSearchResult(tyousakaisyaCd.Value, "", "", "", False)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            tyousakaisyaCd.Value = recData.TysKaisyaCd + recData.JigyousyoCd
            tyousakaisyaNm.Value = recData.TysKaisyaMei

            'フォーカスセット
            masterAjaxSM.SetFocus(tyousakaisyaSearch)
        Else
            '検索画面表示用JavaScript『callSearch』を実行
            tmpScript = "callSearch('" & tyousakaisyaCd.ClientID & "','" & UrlConst.SEARCH_TYOUSAKAISYA & "','" & _
                            tyousakaisyaCd.ClientID & EarthConst.SEP_STRING & tyousakaisyaNm.ClientID & "','" & tyousakaisyaSearch.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If
    End Sub

    ''' <summary>
    ''' 工事会社検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub kojiKaishaSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kojiKaishaSearch.ServerClick
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        Dim tmpScript As String = String.Empty

        If kojiKaishaCd.Value <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetKoujikaishaSearchResult(kojiKaishaCd.Value, "", "", "", False, kameitenCd.Value)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            kojiKaishaCd.Value = recData.TysKaisyaCd + recData.JigyousyoCd
            kojiKaishaNm.Value = recData.TysKaisyaMei

            'フォーカスセット
            masterAjaxSM.SetFocus(kojiKaishaSearch)
        Else
            '検索画面表示用JavaScript『callSearch』を実行
            tmpScript = "callSearch('" & kojiKaishaCd.ClientID & EarthConst.SEP_STRING & kameitenCd.ClientID & "','" & UrlConst.SEARCH_KOUJIKAISYA & "','" & _
                            kojiKaishaCd.ClientID & EarthConst.SEP_STRING & kojiKaishaNm.ClientID & "','" & kojiKaishaSearch.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If
    End Sub

    ''' <summary>
    ''' 検索結果行の生成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub createDataTable(ByVal list As List(Of JibanSearchRecord))
        '検索結果1件のみの場合の列ID格納用hiddenをクリア
        firstSend.Value = ""

        Dim lineCounter As Integer = 0
        Dim com As CommonLogic = CommonLogic.Instance

        '各セルの幅設定用のリスト作成（タイトル行の幅をベースにする）
        Dim widthList1 As New List(Of String)
        Dim tableWidth1 As Integer = 0

        For Each cell As HtmlTableCell In TableTitleTable1.Rows(0).Cells
            Dim tmpWidth = cell.Style("width")
            widthList1.Add(tmpWidth)
            tableWidth1 += Integer.Parse(tmpWidth.Replace("px", "")) + 3
        Next
        TableTitleTable1.Style("width") = tableWidth1 & "px"
        TableDataTable1.Style("width") = tableWidth1 & "px"

        Dim widthList2 As New List(Of String)
        Dim tableWidth2 As Integer = 0

        For Each cell As HtmlTableCell In TableTitleTable2.Rows(0).Cells
            Dim tmpWidth = cell.Style("width")
            widthList2.Add(tmpWidth)
            tableWidth2 += Integer.Parse(tmpWidth.Replace("px", "")) + 3
        Next
        TableTitleTable2.Style("width") = tableWidth2 & "px"
        TableDataTable2.Style("width") = tableWidth2 & "px"

        '一括変更画面起動ボタンのDisabled状態を確認
        Dim flgIkkatuOk As Boolean = False
        If ButtonIkkatuKihon.Disabled = False OrElse ButtonIkkatuTysSyouhin.Disabled = False Then
            '一括変更画面起動ボタンの何れかが有効な場合
            If list.Count > 0 Then
                '検索結果がある場合、一括変更画面起動ボタンをセット＆表示
                flgIkkatuOk = True
                ButtonIkkatuKihon.Visible = True
                ButtonIkkatuKihon.Attributes("onclick") = "returnSelectValueOtherWin(this,5)"
                '依頼業務権限がある場合のみ表示
                If userInfo.IraiGyoumuKengen = -1 Then
                    ButtonIkkatuTysSyouhin.Visible = True
                    ButtonIkkatuTysSyouhin.Attributes("onclick") = "returnSelectValueOtherWin(this,6)"
                End If
            End If
        End If

        For Each data As JibanSearchRecord In list

            lineCounter += 1

            Dim objTr1 As New HtmlTableRow
            Dim objTr2 As New HtmlTableRow
            Dim objTdOtherWin As New HtmlTableCell
            Dim objImgOtherWin As New HtmlImage
            Dim objAncOtherWin1 As New HtmlAnchor
            Dim objAncOtherWin2 As New HtmlAnchor
            Dim objAncOtherWin3 As New HtmlAnchor
            Dim objAncOtherWin4 As New HtmlAnchor
            Dim objIptRtnHiddn1 As New HtmlInputHidden
            Dim objIptRtnHiddn2 As New HtmlInputHidden
            Dim objTdIkkatu As New HtmlTableCell
            Dim objCheckIkkatu As New HtmlInputCheckBox
            Dim objTdHaki As New HtmlTableCell
            Dim objTdKubun As New HtmlTableCell
            Dim objTdBangou As New HtmlTableCell
            Dim objTdSeshuNm As New HtmlTableCell
            Dim objTdJyuusho1 As New HtmlTableCell
            Dim objTdKameitenCd As New HtmlTableCell
            Dim objTdKameitenNm As New HtmlTableCell
            Dim objTdKoumutenGaku As New HtmlTableCell
            Dim objTdJitsuGaku As New HtmlTableCell
            Dim objTdShoudakuGaku As New HtmlTableCell
            Dim objTdBikou As New HtmlTableCell
            Dim objTdChousaDate As New HtmlTableCell
            Dim objTdIraiDate As New HtmlTableCell
            Dim objTdTyousaKibouDate As New HtmlTableCell
            Dim objSpace1 As New HtmlGenericControl
            Dim objSpace2 As New HtmlGenericControl
            Dim objSpace3 As New HtmlGenericControl

            Dim objTdIraiTantousya As New HtmlTableCell
            Dim objTdTyousakaisyaCd As New HtmlTableCell
            Dim objTdTyousakaisyaJigyouCd As New HtmlTableCell
            Dim objTdTyousakaishaMei As New HtmlTableCell
            Dim objTdTyousahouhou As New HtmlTableCell
            Dim objTdSyoudakusyoTyousaDate As New HtmlTableCell
            Dim objTdTantousyaMei As New HtmlTableCell
            Dim objTdSyouninsyaMei As New HtmlTableCell
            Dim objTdTyousakekka As New HtmlTableCell
            Dim objTdKeikakusyoSakuseiDate As New HtmlTableCell
            Dim objTdHosyousyoHakkouDate As New HtmlTableCell
            Dim objTdEigyouTantousyaMei As New HtmlTableCell
            Dim objTdKoujiUriageDate As New HtmlTableCell
            Dim objTdYoyakuZumi As New HtmlTableCell

            Dim objTdBunjouCd As New HtmlTableCell
            Dim objTdBukkennNayose As New HtmlTableCell
            Dim objTdKeiyakuNo As New HtmlTableCell
            Dim objTdKtTorikesi As New HtmlTableCell

            Dim strKtInfoColor As String = EarthConst.STYLE_COLOR_BLACK

            objSpace1.InnerHtml = pStrSpace & pStrSpace
            objSpace2.InnerHtml = pStrSpace & pStrSpace
            objSpace3.InnerHtml = pStrSpace & pStrSpace

            objIptRtnHiddn1.ID = "returnHidden" & lineCounter
            objIptRtnHiddn1.Value = data.Kbn & EarthConst.SEP_STRING & data.HosyousyoNo
            objAncOtherWin1.InnerText = "受"
            objAncOtherWin1.HRef = "javascript:void(0)"
            objAncOtherWin1.Attributes("onclick") = "returnSelectValueOtherWin(this,1);"
            objAncOtherWin1.Attributes("tabindex") = "-1"
            objAncOtherWin2.InnerText = "報"
            objAncOtherWin2.HRef = "javascript:void(0)"
            objAncOtherWin2.Attributes("onclick") = "returnSelectValueOtherWin(this,2);"
            objAncOtherWin2.Attributes("tabindex") = "-1"
            objAncOtherWin3.InnerText = "工"
            objAncOtherWin3.HRef = "javascript:void(0)"
            objAncOtherWin3.Attributes("onclick") = "returnSelectValueOtherWin(this,3);"
            objAncOtherWin3.Attributes("tabindex") = "-1"
            objAncOtherWin4.InnerText = "保"
            objAncOtherWin4.HRef = "javascript:void(0)"
            objAncOtherWin4.Attributes("onclick") = "returnSelectValueOtherWin(this,4);"
            objAncOtherWin4.Attributes("tabindex") = "-1"
            objTdOtherWin.Style("text-align") = "center"
            With objTdOtherWin.Controls
                .Add(objIptRtnHiddn1)  '戻り要素(hidden)は、必ず先頭列の先頭のInputタグにセットすること
                .Add(objAncOtherWin1)
                .Add(objSpace1)
                .Add(objAncOtherWin2)
                .Add(objSpace2)
                .Add(objAncOtherWin3)
                .Add(objSpace3)
                .Add(objAncOtherWin4)
            End With

            objTdIkkatu.Attributes("onclick") = "this.firstChild.click();"  'TDクリック時にもチェック操作を行う
            If flgIkkatuOk = False OrElse data.DataHakiSyubetu <> String.Empty Then
                '一括変更画面起動権限が無い場合または、破棄済みの物件はチェックボックス非表示
                objCheckIkkatu.Visible = False
                objTdIkkatu.InnerHtml = pStrSpace
                objTdIkkatu.Attributes.Remove("onclick")
            End If
            objCheckIkkatu.ID = "CheckIkkatu" & lineCounter
            objCheckIkkatu.Value = objIptRtnHiddn1.Value
            objTdIkkatu.Controls.Add(objCheckIkkatu)
            objTdHaki.InnerHtml = com.GetDisplayString(data.DataHakiSyubetu, pStrSpace)
            objTdKubun.InnerHtml = com.GetDisplayString(data.Kbn, pStrSpace)
            objTdBangou.InnerHtml = com.GetDisplayString(data.HosyousyoNo, pStrSpace)
            objTdSeshuNm.InnerHtml = com.GetDisplayString(data.SesyuMei, "　")

            objIptRtnHiddn2.ID = "returnHidden" & lineCounter
            objIptRtnHiddn2.Value = data.Kbn & EarthConst.SEP_STRING & data.HosyousyoNo
            objTdJyuusho1.InnerHtml = com.GetDisplayString(data.BukkenJyuusyo1 & data.BukkenJyuusyo2 & data.BukkenJyuusyo3, "　")
            objTdJyuusho1.Controls.Add(objIptRtnHiddn2)  '戻り要素(hidden)は、必ず先頭列の先頭のInputタグにセットすること
            objTdKameitenCd.InnerHtml = com.GetDisplayString(data.KameitenCd, pStrSpace)
            objTdKameitenNm.InnerHtml = com.GetDisplayString(data.KameitenMei1, pStrSpace)
            objTdKoumutenGaku.InnerHtml = com.GetDisplayString(Format(data.KoumutenSeikyuuGaku, "#,0"), pStrSpace)
            objTdJitsuGaku.InnerHtml = com.GetDisplayString(Format(data.UriGaku, "#,0"), pStrSpace)
            objTdShoudakuGaku.InnerHtml = com.GetDisplayString(Format(data.SiireGaku, "#,0"), pStrSpace)
            objTdBikou.InnerHtml = com.GetDisplayString(data.Bikou, pStrSpace)
            objTdChousaDate.InnerHtml = com.GetDisplayString(data.TysJissiDate, pStrSpace)
            objTdIraiDate.InnerHtml = com.GetDisplayString(data.IraiDate, pStrSpace)
            objTdTyousaKibouDate.InnerHtml = com.GetDisplayString(data.TysKibouDate, pStrSpace)
            objTdYoyakuZumi.InnerHtml = com.GetDisplayString(IIf(data.YoyakuZumiFlg = 1, "予約済", ""), pStrSpace)
            objTdIraiTantousya.InnerHtml = com.GetDisplayString(data.IraiTantousyaMei, pStrSpace)
            objTdTyousakaisyaCd.InnerHtml = com.GetDisplayString(data.TysKaisyaCd, pStrSpace)
            objTdTyousakaisyaJigyouCd.InnerHtml = com.GetDisplayString(data.TysKaisyaJigyousyoCd, pStrSpace)
            objTdTyousakaishaMei.InnerHtml = com.GetDisplayString(data.TysKaisyaMei, pStrSpace)
            objTdTyousahouhou.InnerHtml = com.GetDisplayString(data.TysHouhouMei, pStrSpace)
            objTdSyoudakusyoTyousaDate.InnerHtml = com.GetDisplayString(data.SyoudakusyoTysDate, pStrSpace)
            objTdTantousyaMei.InnerHtml = com.GetDisplayString(data.TantousyaMei, pStrSpace)
            objTdSyouninsyaMei.InnerHtml = com.GetDisplayString(data.SyouninsyaMei, pStrSpace)
            objTdTyousakekka.InnerHtml = com.GetDisplayString(data.Hantei1 & pStrSpace & data.HanteiSetuzokuMoji & pStrSpace & data.Hantei2, pStrSpace)
            objTdKeikakusyoSakuseiDate.InnerHtml = com.GetDisplayString(data.KeikakusyoSakuseiDate, pStrSpace)
            objTdHosyousyoHakkouDate.InnerHtml = com.GetDisplayString(data.HosyousyoHakDate, pStrSpace)
            objTdEigyouTantousyaMei.InnerHtml = com.GetDisplayString(data.EigyouTantousyaMei, pStrSpace)
            objTdKoujiUriageDate.InnerHtml = com.GetDisplayString(data.KojUriDate, pStrSpace)
            objTdBunjouCd.InnerHtml = com.GetDisplayString(data.BunjouCd, pStrSpace)
            objTdBukkennNayose.InnerHtml = com.GetDisplayString(data.BukkenNayoseCd, pStrSpace)
            objTdKeiyakuNo.InnerHtml = com.GetDisplayString(data.KeiyakuNo, pStrSpace)
            objTdKtTorikesi.InnerHtml = com.GetDisplayString(com.getTorikesiRiyuu(data.KtTorikesi, data.KtTorikesiRiyuu, True), pStrSpace) '加盟店取消理由

            'スタイル、Class設定
            objTdIkkatu.Attributes("class") = "textCenter"
            objCheckIkkatu.Style("height") = "15px;"
            objTdKubun.Attributes("class") = "textCenter"
            objTdBangou.Attributes("class") = "textCenter"
            objTdKameitenCd.Attributes("class") = "textCenter"
            objTdKtTorikesi.Attributes("class") = "textCenter"
            objTdYoyakuZumi.Attributes("class") = "textCenter"
            objTdTyousakaisyaCd.Attributes("class") = "textCenter"
            objTdTyousakaisyaJigyouCd.Attributes("class") = "textCenter"
            objTdKoumutenGaku.Attributes("class") = "kingaku"
            objTdJitsuGaku.Attributes("class") = "kingaku"
            objTdShoudakuGaku.Attributes("class") = "kingaku"
            objTdChousaDate.Attributes("class") = "date textCenter"
            objTdIraiDate.Attributes("class") = "date textCenter"
            objTdTyousaKibouDate.Attributes("class") = "date textCenter"
            objTdSyoudakusyoTyousaDate.Attributes("class") = "date textCenter"
            objTdKeikakusyoSakuseiDate.Attributes("class") = "date textCenter"
            objTdHosyousyoHakkouDate.Attributes("class") = "date textCenter"
            objTdKoujiUriageDate.Attributes("class") = "date textCenter"
            objTdBunjouCd.Attributes("class") = "number"
            objTdBukkennNayose.Attributes("class") = "textCenter"
            objTdKeiyakuNo.Attributes("class") = "textCenter"

            '加盟店取消の場合、文字色変更
            strKtInfoColor = com.getKameitenFontColor(data.KtTorikesi)
            objTdKameitenCd.Style(EarthConst.STYLE_FONT_COLOR) = strKtInfoColor
            objTdKtTorikesi.Style(EarthConst.STYLE_FONT_COLOR) = strKtInfoColor
            objTdKameitenNm.Style(EarthConst.STYLE_FONT_COLOR) = strKtInfoColor

            '各セルの幅設定
            If lineCounter = 1 Then
                '左側
                objTdOtherWin.Style("width") = widthList1(0)
                objTdIkkatu.Style("width") = widthList1(1)
                objTdHaki.Style("width") = widthList1(2)
                objTdKubun.Style("width") = widthList1(3)
                objTdBangou.Style("width") = widthList1(4)
                objTdSeshuNm.Style("width") = widthList1(5)
                '右側
                objTdJyuusho1.Style("width") = widthList2(0)
                objTdKameitenCd.Style("width") = widthList2(1)
                objTdKtTorikesi.Style("width") = widthList2(2)
                objTdKameitenNm.Style("width") = widthList2(3)
                objTdIraiTantousya.Style("width") = widthList2(4)
                objTdIraiDate.Style("width") = widthList2(5)
                objTdTyousaKibouDate.Style("width") = widthList2(6)
                objTdYoyakuZumi.Style("width") = widthList2(7)
                objTdTyousakaisyaCd.Style("width") = widthList2(8)
                objTdTyousakaisyaJigyouCd.Style("width") = widthList2(9)
                objTdTyousakaishaMei.Style("width") = widthList2(10)
                objTdTyousahouhou.Style("width") = widthList2(11)
                objTdSyoudakusyoTyousaDate.Style("width") = widthList2(12)
                objTdKoumutenGaku.Style("width") = widthList2(13)
                objTdJitsuGaku.Style("width") = widthList2(14)
                objTdShoudakuGaku.Style("width") = widthList2(15)
                objTdTantousyaMei.Style("width") = widthList2(16)
                objTdSyouninsyaMei.Style("width") = widthList2(17)
                objTdChousaDate.Style("width") = widthList2(18)
                objTdTyousakekka.Style("width") = widthList2(19)
                objTdKeikakusyoSakuseiDate.Style("width") = widthList2(20)
                objTdHosyousyoHakkouDate.Style("width") = widthList2(21)
                objTdBikou.Style("width") = widthList2(22)
                objTdEigyouTantousyaMei.Style("width") = widthList2(23)
                objTdKoujiUriageDate.Style("width") = widthList2(24)
                objTdBunjouCd.Style("width") = widthList2(25)
                objTdBukkennNayose.Style("width") = widthList2(26)
                objTdKeiyakuNo.Style("width") = widthList2(27)
            End If

            objTr1.ID = "DataTable_resultTr1_" & lineCounter
            objTr1.Attributes("tabindex") = -1

            '行にセルをセット(左側)
            With objTr1.Controls
                .Add(objTdOtherWin)
                .Add(objTdIkkatu)
                .Add(objTdHaki)
                .Add(objTdKubun)
                .Add(objTdBangou)
                .Add(objTdSeshuNm)
            End With

            objTr2.ID = "DataTable_resultTr2_" & lineCounter
            If lineCounter = 1 Then
                '1行目にTabindexを設定＆フォーカス時にスクロールを最上段まで戻すJSを設定
                objTr2.Attributes("tabindex") = Integer.Parse(CheckHakiTaisyou.Attributes("tabindex")) + 10
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop(" & DivLeftData.ClientID & ");"
            Else
                '2行目以降はタブ移動なし
                objTr2.Attributes("tabindex") = -1
            End If

            '行にセルをセット(右側)
            With objTr2.Controls
                .Add(objTdJyuusho1)
                .Add(objTdKameitenCd)
                .Add(objTdKtTorikesi)
                .Add(objTdKameitenNm)
                .Add(objTdIraiTantousya)
                .Add(objTdIraiDate)
                .Add(objTdTyousaKibouDate)
                .Add(objTdYoyakuZumi)
                .Add(objTdTyousakaisyaCd)
                .Add(objTdTyousakaisyaJigyouCd)
                .Add(objTdTyousakaishaMei)
                .Add(objTdTyousahouhou)
                .Add(objTdSyoudakusyoTyousaDate)
                .Add(objTdKoumutenGaku)
                .Add(objTdJitsuGaku)
                .Add(objTdShoudakuGaku)
                .Add(objTdTantousyaMei)
                .Add(objTdSyouninsyaMei)
                .Add(objTdChousaDate)
                .Add(objTdTyousakekka)
                .Add(objTdKeikakusyoSakuseiDate)
                .Add(objTdHosyousyoHakkouDate)
                .Add(objTdBikou)
                .Add(objTdEigyouTantousyaMei)
                .Add(objTdKoujiUriageDate)
                .Add(objTdBunjouCd)
                .Add(objTdBukkennNayose)
                .Add(objTdKeiyakuNo)
            End With

            'テーブルに行をセット
            '左側
            TableDataTable1.Controls.Add(objTr1)
            '右側
            TableDataTable2.Controls.Add(objTr2)

            If list.Count = 1 Then
                '検索結果1件のみの場合の列ID格納用hiddenに値をセット
                firstSend.Value = objTr1.ClientID
            End If

        Next

    End Sub

    ''' <summary>
    ''' 営業所検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub eigyousyoSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles eigyousyoSearch.ServerClick
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim blnResult As Boolean

        '営業所マスタを検索
        blnResult = cl.CallEigyousyoSearchWindow(sender _
                                                 , e _
                                                 , Me _
                                                 , Me.eigyousyoCd _
                                                 , Me.eigyousyoNm _
                                                 , Me.eigyousyoSearch _
                                                 , False _
                                                 )

        If blnResult Then
            'フォーカスセット
            masterAjaxSM.SetFocus(Me.eigyousyoSearch)
        End If

    End Sub

End Class