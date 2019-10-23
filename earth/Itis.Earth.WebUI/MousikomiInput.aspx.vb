
Partial Public Class MousikomiInput
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    Dim cbLogic As New CommonBizLogic

    ''' <summary>
    ''' メッセージクラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic
    Dim JLogic As New JibanLogic
    Dim sLogic As New StringLogic

    ''' <summary>
    ''' 物件履歴コントロールタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emBrCtrl
        ''' <summary>
        ''' 物件履歴コントロール1
        ''' </summary>
        ''' <remarks></remarks>
        intCtrl1 = 1
        ''' <summary>
        ''' 物件履歴コントロール2
        ''' </summary>
        ''' <remarks></remarks>
        intCtrl2 = 2
        ''' <summary>
        ''' 物件履歴コントロール3
        ''' </summary>
        ''' <remarks></remarks>
        intCtrl3 = 3
    End Enum

#Region "物件履歴・行コントロールID接頭語"
    Private Const BUKKEN_RIREKI_CTRL_NAME As String = "CtrlBukkenRireki_"
    Private Const SELECT_SYUBETU_CTRL_NAME As String = "SelectSyubetu_"
    Private Const BUKKEN_RIREKI_CTRL_CNT As Integer = 1
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

        Dim strKbn As String = ""
        Dim strBangou As String = ""

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing OrElse userinfo.SinkiNyuuryokuKengen = 0 Then
            'ログイン情報が無い場合、新規入力権限がない場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        '各テーブルの表示状態を切り替える
        Me.TBodyBRInfo.Style("display") = Me.HiddenBRInfoStyle.Value

        If IsPostBack = False Then '初期起動時

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            ' コンボ設定ヘルパークラスを生成
            Dim helper As New DropDownHelper
            Dim objDrpTmp As New DropDownList
            ' 区分コンボにデータをバインドする
            helper.SetDropDownList(SelectKIKubun, DropDownHelper.DropDownType.Kubun2, True)
            ' 構造種別コンボにデータをバインドする
            helper.SetDropDownList(SelectKouzouSyubetu, DropDownHelper.DropDownType.Kouzou, True, False)
            ' 階層コンボにデータをバインドする
            helper.SetDropDownList(SelectKaisou, DropDownHelper.DropDownType.Kaisou, True, False)
            ' 新築建替コンボにデータをバインドする
            helper.SetDropDownList(SelectSintikuTatekae, DropDownHelper.DropDownType.ShintikuTatekae, True, False)
            ' 建物用途コンボにデータをバインドする
            helper.SetDropDownList(SelectTatemonoYouto, DropDownHelper.DropDownType.TatemonoYouto, True, False)
            ' 予定基礎コンボにデータをバインドする
            helper.SetDropDownList(SelectYoteiKiso, DropDownHelper.DropDownType.YoteiKiso, True, False)
            ' 地下車庫計画コンボにデータをバインドする
            helper.SetDropDownList(SelectTikaSyakoKeikaku, DropDownHelper.DropDownType.Syako, True)
            ' 経由コンボにデータをバインドする
            helper.SetDropDownList(SelectSIKeiyu, DropDownHelper.DropDownType.Keiyu, False, True)
            ' 商品1コンボにデータをバインドする
            helper.SetDropDownList(choSyouhin1, DropDownHelper.DropDownType.Syouhin1, True, True)
            '調査方法
            helper.SetDropDownList(SelectSITysHouhou, DropDownHelper.DropDownType.TyousaHouhou, True, False)
            '調査概要
            helper.SetDropDownList(SelectSITysGaiyou, DropDownHelper.DropDownType.TyousaGaiyou, True)

            ' 種別コンボにデータをバインドする
            helper.SetMeisyouDropDownList(Me.SelectBRSyubetu1, EarthConst.emMeisyouType.BUKKEN_RIREKI_SYUBETU)
            helper.SetMeisyouDropDownList(Me.SelectBRSyubetu2, EarthConst.emMeisyouType.BUKKEN_RIREKI_SYUBETU)
            helper.SetMeisyouDropDownList(Me.SelectBRSyubetu3, EarthConst.emMeisyouType.BUKKEN_RIREKI_SYUBETU)

            '●ダミーコンボにセット
            helper.SetMeisyouDropDownList(Me.SelectTmpCode, EarthConst.emMeisyouType.BUKKEN_RIREKI_SYUBETU, False, True)

            'ダミードロップダウンリストの生成
            Me.CreateDropDownList(Me.SelectTmpCode)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            SetDispAction()

        Else
            'ダミードロップダウンリストの生成
            Me.CreateDropDownList(Me.SelectTmpCode)

        End If

        'ボタン押下イベントの設定
        setBtnEvent()

        If ButtonTouroku1.Disabled = False Then
            ButtonTouroku1.Focus() '登録/修正ボタンにフォーカス
        End If

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispAction()
        Dim jBn As New Jiban '地盤画面クラス

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 他システムへのリンクボタン設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '加盟店注意事項
        cl.getKameitenTyuuijouhouPath(Me.TextKITourokuBangou.ClientID, Me.ButtonKIKameitenTyuuijouhou)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 区分、保証書NO関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++


        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 重複チェック関連(※表示設定)
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.CheckTyoufuku(Nothing)

        Me.TextBukkenMeisyou.Attributes("onchange") = "ChgTyoufukuBukken(this);"
        Me.TextBukkenJyuusyo1.Attributes("onchange") = "ChgTyoufukuBukken(this);"
        Me.TextBukkenJyuusyo2.Attributes("onchange") = "ChgTyoufukuBukken(this);"

        '住所３を備考に転記
        Me.ButtonJyuusyoTenki.Attributes("onclick") = "juushoTenki_onclick();"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' プルダウン/コード入力連携設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '構造種別
        jBn.SetPullCdScriptSrc(TextKouzouSyubetuCd, SelectKouzouSyubetu)
        '新築建替
        jBn.SetPullCdScriptSrc(TextSintikuTatekaeCd, SelectSintikuTatekae)
        '階層
        jBn.SetPullCdScriptSrc(TextKaisouCd, SelectKaisou)
        '建物用途
        jBn.SetPullCdScriptSrc(TextTatemonoYoutoCd, SelectTatemonoYouto)
        '予定基礎
        jBn.SetPullCdScriptSrc(TextYoteiKisoCd, SelectYoteiKiso)
        '地下車庫計画
        jBn.SetPullCdScriptSrc(TextTikaSyakoKeikakuCd, SelectTikaSyakoKeikaku)
        '調査方法
        jBn.SetPullCdScriptSrc(TextSITysHouhouCd, SelectSITysHouhou)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 「～その他」や「立会有り」の場合のみ表示する項目
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '構造、予定基礎に関しては、「9：その他」の場合のみ「～その他」の入力項目を使用可能にするスクリプトを埋め込む
        SelectKouzouSyubetu.Attributes("onchange") += "checkSonota(this.value==9,'" & TextKouzouSyubetuSonota.ClientID & "');"
        SelectYoteiKiso.Attributes("onchange") += "checkSonota(this.value==9,'" & TextYoteiKisoSonota.ClientID & "');"

        '指定値選択時の動き(画面表示時用)
        checkSonota()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 前日/当日日付自動関連セット
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '前日
        Me.HiddenDateYesterday.Value = Format(Today.AddDays(-1), "yyyy/MM/dd")
        Dim strBefore As String = "if(objEBI('@IRAIDATA').value=='')objEBI('@IRAIDATA').value='@YESTERDAY';"
        strBefore = strBefore.Replace("@IRAIDATA", Me.TextIraiDate.ClientID)
        strBefore = strBefore.Replace("@YESTERDAY", Me.HiddenDateYesterday.Value)
        Me.ButtonIraiDateYestarday.Attributes("onclick") = strBefore

        '当日
        Me.HiddenDateToday.Value = Format(Today, "yyyy/MM/dd")
        Dim strToday As String = "if(objEBI('@IRAIDATA').value=='')objEBI('@IRAIDATA').value='@TODAY';"
        strToday = strToday.Replace("@IRAIDATA", Me.TextIraiDate.ClientID)
        strToday = strToday.Replace("@TODAY", Me.HiddenDateToday.Value)
        Me.ButtonIraiDateToday.Attributes("onclick") = strToday

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 同時依頼棟数、建物用途、戸数関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 同時依頼棟数はデフォルト１
        If TextDoujiIraiTousuu.Text = String.Empty Then
            TextDoujiIraiTousuu.Text = "1"
        End If

        ' 建物用途はデフォルト１
        If SelectTatemonoYouto.SelectedValue = String.Empty Then
            TextTatemonoYoutoCd.Text = "1"
            SelectTatemonoYouto.SelectedValue = "1"
        End If
        ' 戸数はデフォルト１
        If TextSIKosuu.Text = String.Empty Then
            TextSIKosuu.Text = "1"
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript関連セット
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim onFocusPostBackScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScript As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this)){__doPostBack(this.id,'');}}else{checkNumberAddFig(this);}"
        Dim onFocusScript As String = "removeFig(this);"
        Dim onBlurScript As String = "checkNumberAddFig(this);"
        Dim checkDate As String = "checkDate(this);"
        Dim checkKingaku As String = "checkKingaku(this);"
        Dim checkNumber As String = "checkNumber(this);"

        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '日付項目用
        Dim onFocusPostBackScriptDate As String = "setTempValueForOnBlur(this);"
        Dim onBlurPostBackScriptDate As String = "if(checkTempValueForOnBlur(this)){if(checkDate(this)){__doPostBack(this.id,'');}}else{checkDate(this);}"

        '*****************************
        '* 日付系
        '*****************************
        '<画面中央部>
        '依頼日
        TextIraiDate.Attributes("onblur") = checkDate
        TextIraiDate.Attributes("onkeydown") = disabledOnkeydown
        '調査希望日
        TextTyousaKibouDate.Attributes("onblur") = checkDate
        TextTyousaKibouDate.Attributes("onkeydown") = disabledOnkeydown
        '基礎着工予定日FROM
        TextKsTyakkouYoteiDateFrom.Attributes("onblur") = checkDate
        TextKsTyakkouYoteiDateFrom.Attributes("onkeydown") = disabledOnkeydown
        '基礎着工予定日TO
        TextKsTyakkouYoteiDateTo.Attributes("onblur") = checkDate
        TextKsTyakkouYoteiDateTo.Attributes("onkeydown") = disabledOnkeydown
        '<物件履歴情報>
        '日付
        Me.TextBRHizuke1.Attributes("onblur") = checkDate
        Me.TextBRHizuke1.Attributes("onkeydown") = disabledOnkeydown
        '日付
        Me.TextBRHizuke2.Attributes("onblur") = checkDate
        Me.TextBRHizuke2.Attributes("onkeydown") = disabledOnkeydown
        '日付
        Me.TextBRHizuke3.Attributes("onblur") = checkDate
        Me.TextBRHizuke3.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 数値系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '今回同時依頼棟数
        Me.TextDoujiIraiTousuu.Attributes("onfocus") = onFocusScript
        Me.TextDoujiIraiTousuu.Attributes("onblur") = onBlurScript
        '構造種別
        Me.TextKouzouSyubetuCd.Attributes("onblur") += checkNumber
        '新築立替
        Me.TextSintikuTatekaeCd.Attributes("onblur") += checkNumber
        '階層
        Me.TextKaisouCd.Attributes("onblur") += checkNumber
        '建物用途
        Me.TextTatemonoYoutoCd.Attributes("onblur") += checkNumber
        '設計許容支持力
        Me.TextSekkeiKyoyouSijiryoku.Attributes("onfocus") = onFocusScript
        Me.TextSekkeiKyoyouSijiryoku.Attributes("onblur") = onBlurScript
        '依頼予定棟数
        Me.TextIraiYoteiTousuu.Attributes("onfocus") = onFocusScript
        Me.TextIraiYoteiTousuu.Attributes("onblur") = onBlurScript
        '根切り深さ
        Me.TextNegiriHukasa.Attributes("onfocus") = onFocusScript
        Me.TextNegiriHukasa.Attributes("onblur") = onBlurScript
        '予定盛土厚さ
        Me.TextYoteiMoritutiAtusa.Attributes("onfocus") = onFocusScript
        Me.TextYoteiMoritutiAtusa.Attributes("onblur") = onBlurScript
        '予定基礎
        Me.TextYoteiKisoCd.Attributes("onblur") += checkNumber
        '地下車庫計画
        Me.TextTikaSyakoKeikakuCd.Attributes("onblur") += checkNumber
        '戸数
        Me.TextSIKosuu.Attributes("onfocus") = onFocusScript
        Me.TextSIKosuu.Attributes("onblur") = onBlurScript
        '調査方法
        Me.TextSITysHouhouCd.Attributes("onblur") += checkNumber

        '*****************************
        '* コードおよびポップアップボタン
        '*****************************
        '加盟店情報.登録番号
        TextKITourokuBangou.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callKameitenSearch(this);}else{checkNumber(this);}"
        TextKITourokuBangou.Attributes("onfocus") = "setTempValueForOnBlur(this);"
        'その他情報.調査会社
        TextSITysGaisyaCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callTyousakaisyaSearch(this);}else{checkNumber(this);}"
        TextSITysGaisyaCd.Attributes("onfocus") = "setTempValueForOnBlur(this);"

        'ポップアップボタン
        ButtonKITourokuBangou.Attributes("onclick") = "SetChangeMaeValue('" & HiddenKITourokuBangouMae.ClientID & "','" & TextKITourokuBangou.ClientID & "');"
        ButtonSITysGaisya.Attributes("onclick") = "SetChangeMaeValue('" & HiddenSITysGaisyaMae.ClientID & "','" & TextSITysGaisyaCd.ClientID & "');"

        '*****************************
        '* プルダウン
        '*****************************
        '<画面右上部>
        '区分
        SelectKIKubun.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenKIKbnMae.ClientID & "','" & SelectKIKubun.ClientID & "');"
        SelectKIKubun.Attributes("onchange") = "ChgSelectKbn();"
        '<物件履歴情報>
        '種別
        Me.SelectBRSyubetu1.Attributes("onchange") = "SelectSyubetuOnChg('" & Me.SelectBRSyubetu1.ClientID & "',1)"
        '種別
        Me.SelectBRSyubetu2.Attributes("onchange") = "SelectSyubetuOnChg('" & Me.SelectBRSyubetu2.ClientID & "',2)"
        '種別
        Me.SelectBRSyubetu3.Attributes("onchange") = "SelectSyubetuOnChg('" & Me.SelectBRSyubetu3.ClientID & "',3)"

        '*****************************
        '* ラジオボタン
        '*****************************
        '商品区分の表示切替
        checkSyouhinkubun()

        '調査立会者の表示切替
        checkTysTatiaisya()

        '*****************************
        '* 調査概要設定
        '*****************************
        Dim setTysGaiyouScript As String = "callSetTysGaiyou(this);"
        Me.choSyouhin1.Attributes("onchange") = setTysGaiyouScript
        Me.SelectSITysHouhou.Attributes("onchange") += setTysGaiyouScript

        '商品1変更時、SDS自動設定
        Dim setTysHouhouGaisyaScript As String = "callKameitenSearchFromSyouhin1(this);"
        Me.choSyouhin1.Attributes("onchange") += setTysHouhouGaisyaScript

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 機能別テーブルの表示切替
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '<物件履歴情報>
        Me.AncBRInfo.HRef = "JavaScript:changeDisplay('" & Me.TBodyBRInfo.ClientID & "');SetDisplayStyle('" & Me.HiddenBRInfoStyle.ClientID & "','" & Me.TBodyBRInfo.ClientID & "');"

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
    ''' 登録/修正実行ボタンの設定
    ''' </summary>
    ''' <remarks>
    ''' DB更新の確認を行なう。<br/>
    ''' OK時：DB更新を行なう。<br/>
    ''' キャンセル時：DB更新を中断する。<br/>
    ''' </remarks>
    Private Sub setBtnEvent()

        '表示設定
        '番号
        Me.TextBangou.Visible = False

        '新規(引継)申込ボタン
        Me.ButtonSinkiHikitugi.Visible = False
        '新規申込ボタン
        Me.ButtonSinki.Visible = False

        'イベントハンドラ登録
        Dim tmpScript As String = "actClickButton(this)"
        Me.ButtonTouroku1.Attributes("onclick") = tmpScript
        Me.ButtonTouroku2.Attributes("onclick") = tmpScript

    End Sub

    ''' <summary>
    ''' 調査概要設定ボタン(非表示)押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSetTysGaiyou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTysGaiyou.ServerClick
        '設定・取得用 商品価格設定レコード
        Dim recKakakuSettei As New KakakuSetteiRecord

        '実行元のクライアントコントロール
        Dim strActCtrlId As String = actCtrlId.Value.Replace(ClientID & ClientIDSeparator.ToString, "")
        Dim actCtrl As Control = FindControl(strActCtrlId)

        '商品区分
        If RadioSISyouhinKbn1.Checked Then '60年保証
            recKakakuSettei.SyouhinKbn = Me.RadioSISyouhinKbn1.Value
        ElseIf RadioSISyouhinKbn2.Checked Then '土地販売
            recKakakuSettei.SyouhinKbn = Me.RadioSISyouhinKbn2.Value
        ElseIf RadioSISyouhinKbn3.Checked Then 'リフォーム
            recKakakuSettei.SyouhinKbn = Me.RadioSISyouhinKbn3.Value
        Else 'その他
            recKakakuSettei.SyouhinKbn = Me.RadioSISyouhinKbn9.Value
        End If
        '調査方法
        cl.SetDisplayString(Me.TextSITysHouhouCd.Text, recKakakuSettei.TyousaHouhouNo)
        '商品コード
        cl.SetDisplayString(Me.choSyouhin1.SelectedValue, recKakakuSettei.SyouhinCd)

        '商品価格設定マスタから値の取得
        JLogic.GetTysGaiyou(recKakakuSettei)

        '調査概要の設定
        Me.SelectSITysGaiyou.SelectedValue = cl.GetDispNum(recKakakuSettei.TyousaGaiyou, "")

        'フォーカスセット
        If actCtrlId.Value = Me.choSyouhin1.ClientID Then
            masterAjaxSM.SetFocus(Me.choSyouhin1)
        ElseIf actCtrlId.Value = Me.SelectSITysHouhou.ClientID Then
            masterAjaxSM.SetFocus(Me.SelectSITysHouhou)
        End If


    End Sub

#Region "[加盟店情報]処理"

    ''' <summary>
    ''' 加盟店情報.登録番号検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKITourokuBangou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKITourokuBangou.ServerClick
        '処理実行前の区分値を保持
        Dim tmpOldKbn As String = Me.SelectKIKubun.SelectedValue

        If kameitenSearchType.Value <> "1" Then
            kameitenSearchSub(sender, e)
        Else
            'コードonchangeで呼ばれた場合
            kameitenSearchSub(sender, e, False)
            kameitenSearchType.Value = String.Empty
        End If

        If tmpOldKbn <> Me.SelectKIKubun.SelectedValue Then
            '加盟店検索の結果、区分が変更されている場合、重複チェックを行う(チェック実行トリガーは区分とする)
            Me.HiddenTyoufukuKakuninTargetId.Value = Me.SelectKIKubun.ClientID
            Me.CheckTyoufuku(sender)
        End If
    End Sub

    ''' <summary>
    ''' 加盟店検索ボタン押下時の処理(実体)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="callWindow">検索ポップアップを起動するか否かの指定</param>
    ''' <remarks></remarks>
    Private Sub kameitenSearchSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        ' 入力されているコードをキーに、マスタを検索し、データを1件のみ取得できた場合は
        ' 画面情報を更新する。データが無い場合は、検索画面を表示する
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim dataArray As New List(Of KameitenSearchRecord)
        Dim blnTorikesi As Boolean = True
        Dim total_count As Integer
        Dim strTysGaisyaCd As String = String.Empty
        Dim blnRet As Boolean = False

        ' 取得件数を絞り込む場合、引数を追加してください
        If TextKITourokuBangou.Text <> "" Then
            dataArray = kameitenSearchLogic.GetKameitenSearchResult(SelectKIKubun.SelectedValue, _
                                                                    TextKITourokuBangou.Text, _
                                                                    blnTorikesi, _
                                                                    total_count)
        End If

        If dataArray.Count = 1 Then

            '加盟店コードを入れ直す
            Me.TextKITourokuBangou.Text = dataArray(0).KameitenCd
            Me.HiddenKITourokuBangouMae.Value = Me.TextKITourokuBangou.Text

            '●ビルダー注意事項チェック
            If kameitenSearchLogic.ChkBuilderData13(Me.TextKITourokuBangou.Text) Then
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.TrueString
            Else
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.FalseString
            End If

            '●調査方法SDS自動設定チェック
            '画面.商品1が入力済み以外の場合、以降の処理は行わない。
            If (TextKITourokuBangou.Text <> "") AndAlso (Me.choSyouhin1.SelectedValue <> "") Then
                blnRet = cbLogic.ChkTysJidouSet(Me.choSyouhin1.SelectedValue, Me.TextKITourokuBangou.Text, strTysGaisyaCd)
            End If

            '発注停止チェック
            cl.chkOrderStopFlg(sender, dataArray(0).OrderStopFLG, Me.TextKITourokuBangou.Text, Me.saveCdOrderStop.Value)

            ' フォーカスセット > 商品１入力後に画面がスクロールしてしまうので排除
            ' setFocusAJ(ButtonKITourokuBangou)
        Else
            '●ビルダー注意事項フラグ初期化
            Me.HiddenKameitenTyuuiJikou.Value = String.Empty

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpFocusScript = "objEBI('" & ButtonKITourokuBangou.ClientID & "').focus();"
            Dim tmpScript As String = "callSearch('" & SelectKIKubun.ClientID & EarthConst.SEP_STRING & TextKITourokuBangou.ClientID & "','" & UrlConst.SEARCH_KAMEITEN & "','" & _
                            TextKITourokuBangou.ClientID & EarthConst.SEP_STRING & TextKISyamei.ClientID & "','" & ButtonKITourokuBangou.ClientID & "');"

            '加盟店未確定のため名称クリア
            'クリアを行なう
            Me.ClearKameitenInfo(False)

            'ポップアップ表示
            If callWindow Then
                tmpScript = tmpFocusScript & tmpScript
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
                Exit Sub
            ElseIf kameitenSearchType.Value = "1" Then
                tmpScript = tmpFocusScript
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            End If
        End If

        ' 加盟店検索実行後処理実行
        kameitenSearchAfter_ServerClick(sender, e, blnRet, strTysGaisyaCd)

    End Sub

    ''' <summary>
    ''' 加盟店検索実行後処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="blnRet">SDS自動設定チェック結果</param>
    ''' <param name="strTysGaisyaCd">SDS自動設定調査会社</param>
    ''' <remarks></remarks>
    Protected Sub kameitenSearchAfter_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal blnRet As Boolean, ByVal strTysGaisyaCd As String)

        '登録番号
        If Me.TextKITourokuBangou.Text <> "" Then '入力
            '加盟店コードを退避(発注停止処理の場合は再検索)
            Me.saveCdOrderStop.Value = Me.TextKITourokuBangou.Text

            '加盟店関連項目の設定を行なう
            '加盟店検索実行後処理(加盟店詳細情報、ビルダー情報取得)
            Dim logic As New KameitenSearchLogic
            Dim blnTorikesi As Boolean = True
            Dim dataArray As New List(Of KameitenSearchRecord)
            Dim record As KameitenSearchRecord

            record = logic.GetKameitenSearchResult("", TextKITourokuBangou.Text, "", blnTorikesi)

            If Not record Is Nothing AndAlso record.KameitenCd <> String.Empty Then
                '区分
                Me.SelectKIKubun.SelectedValue = cl.GetDisplayString(record.Kbn)
                '社名
                Me.TextKISyamei.Text = cl.GetDisplayString(record.KameitenMei1)
                '住所
                Me.TextKIJyuusyo.Text = cl.GetDisplayString(record.Jyuusyo)
                'TEL
                Me.TextKITel.Text = cl.GetDisplayString(record.TelNo)
                'FAX
                Me.TextKIFax.Text = cl.GetDisplayString(record.FaxNo)

                'JIO先の表示設定
                If record.JioSakiFLG = 1 Then
                    Me.SpanKIJioSaki.InnerHtml = EarthConst.JIO_SAKI
                Else
                    Me.SpanKIJioSaki.InnerHtml = ""
                End If

                '加盟店による自動設定
                '保証書発行有無は０の場合無し、以外１（地盤仕様）
                Me.SelectSIHosyouUmu.SelectedValue = IIf(cl.GetDisplayString(record.HosyousyoHakUmu) = "1", "1", "")
                Me.HiddenHosyouKikan.Value = cl.GetDisplayString(record.HosyouKikan)
                ' 工事会社請求有無設定
                Me.HiddenKjGaisyaSeikyuuUmu.Value = IIf(cl.GetDisplayString(record.KojGaisyaSeikyuuUmu) = "1", "1", "")

                '調査方法SDS自動設定
                If blnRet = True AndAlso strTysGaisyaCd <> String.Empty Then
                    '調査方法
                    Me.TextSITysHouhouCd.Text = EarthConst.TYOUSA_HOUHOU_CD_15
                    Me.SelectSITysHouhou.SelectedValue = EarthConst.TYOUSA_HOUHOU_CD_15
                    '調査会社
                    Me.TextSITysGaisyaCd.Text = strTysGaisyaCd
                    tyousakaisyaSearchSub(sender, e, False)
                    '調査概要
                    btnSetTysGaiyou_ServerClick(sender, e)
                End If
            Else
                'クリアを行なう
                ClearKameitenInfo()

            End If

        Else '未入力
            'クリアを行なう
            ClearKameitenInfo(False)

        End If

    End Sub

    ''' <summary>
    ''' 加盟店.クリアボタン(非表示)押下時処理
    ''' ※加盟店関連情報をクリアする
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKIKameitenClear_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(Me.SelectKIKubun)

        'クリアを行なう
        ClearKameitenInfo()

        '区分
        Me.SelectKIKubun.SelectedValue = CStr(Me.HiddenKIKbnMae.Value)

        '重複チェックを行う(チェック実行トリガーは区分とする)
        Me.HiddenTyoufukuKakuninTargetId.Value = Me.SelectKIKubun.ClientID
        Me.CheckTyoufuku(sender)

    End Sub

    ''' <summary>
    ''' 加盟店情報をクリアする
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearKameitenInfo(Optional ByVal blnFlg As Boolean = True)

        If blnFlg Then
            '区分
            Me.SelectKIKubun.SelectedValue = ""
            '登録番号
            Me.TextKITourokuBangou.Text = ""
            ' 担当者
            Me.TextKITantousya.Text = ""
        End If

        '加盟店コードをクリア(発注停止処理用)
        Me.saveCdOrderStop.Value = ""

        '社名
        Me.TextKISyamei.Text = ""
        '住所
        Me.TextKIJyuusyo.Text = ""
        'TEL
        Me.TextKITel.Text = ""
        'FAX
        Me.TextKIFax.Text = ""

        'JIO先の表示設定
        Me.SpanKIJioSaki.InnerHtml = ""

        '加盟店による自動設定
        '保証書発行有無は０の場合無し、以外１（地盤仕様）
        Me.SelectSIHosyouUmu.SelectedValue = ""
        Me.HiddenHosyouKikan.Value = ""
        ' 工事会社請求有無設定
        Me.HiddenKjGaisyaSeikyuuUmu.Value = ""

    End Sub

#End Region

#Region "[その他情報]処理"

    ''' <summary>
    ''' 調査会社検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSITysGaisya_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If tyousakaisyaSearchType.Value <> "1" Then
            tyousakaisyaSearchSub(sender, e)
        Else
            'コードonchangeで呼ばれた場合
            tyousakaisyaSearchSub(sender, e, False)
            tyousakaisyaSearchType.Value = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' 調査会社検索ボタン押下時の処理(実体)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tyousakaisyaSearchSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        If TextSITysGaisyaCd.Text <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetTyousakaisyaSearchResult(TextSITysGaisyaCd.Text, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi, _
                                                                            TextKITourokuBangou.Text)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            TextSITysGaisyaCd.Text = recData.TysKaisyaCd & recData.JigyousyoCd
            TextSITysGaisyaMei.Text = recData.TysKaisyaMei

            Me.HiddenSITysGaisyaMae.Value = Me.TextSITysGaisyaCd.Text

            ' 調査会社NG設定
            If recData.KahiKbn = 9 Then
                TextSITysGaisyaCd.Style("color") = "red"
                TextSITysGaisyaMei.Style("color") = "red"
            Else
                TextSITysGaisyaCd.Style("color") = "blue"
                TextSITysGaisyaMei.Style("color") = "blue"
            End If

            'フォーカスセット
            setFocusAJ(ButtonSITysGaisya)
        Else
            TextSITysGaisyaCd.Style("color") = "black"
            TextSITysGaisyaMei.Style("color") = "black"

            '調査会社コードが未確定なのでクリア
            Me.TextSITysGaisyaMei.Text = "" '調査会社名

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpFocusScript = "objEBI('" & ButtonSITysGaisya.ClientID & "').focus();"
            Dim tmpScript = "callSearch('" & TextSITysGaisyaCd.ClientID & EarthConst.SEP_STRING & TextKITourokuBangou.ClientID & "','" & UrlConst.SEARCH_TYOUSAKAISYA & "','" & _
                                        TextSITysGaisyaCd.ClientID & EarthConst.SEP_STRING & _
                                        TextSITysGaisyaMei.ClientID & _
                                        "','" & ButtonSITysGaisya.ClientID & "');"
            If callWindow Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & tmpScript, True)
            ElseIf tyousakaisyaSearchType.Value = "1" Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript, True)
            End If
        End If

    End Sub

    ''' <summary>
    ''' 商品区分の値によって、表示を切り替える
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkSyouhinkubun()

        'ラジオボタンは非表示で商品区分は全てラベル化
        If Me.RadioSISyouhinKbn1.Checked Then
            ' 60年保証
            Me.SpanSISyouhinKbn1.Style("display") = "inline"
            Me.SpanSISyouhinKbn2.Style("display") = "none"
            Me.SpanSISyouhinKbn3.Style("display") = "none"
            Me.SpanSISyouhinKbn9.Style("display") = "none"
        ElseIf Me.RadioSISyouhinKbn2.Checked Then
            ' 土地販売
            Me.SpanSISyouhinKbn1.Style("display") = "none"
            Me.SpanSISyouhinKbn2.Style("display") = "inline"
            Me.SpanSISyouhinKbn3.Style("display") = "none"
            Me.SpanSISyouhinKbn9.Style("display") = "none"
        ElseIf Me.RadioSISyouhinKbn3.Checked Then
            ' リフォーム
            Me.SpanSISyouhinKbn1.Style("display") = "none"
            Me.SpanSISyouhinKbn2.Style("display") = "none"
            Me.SpanSISyouhinKbn3.Style("display") = "inline"
            Me.SpanSISyouhinKbn9.Style("display") = "none"
        Else
            ' 未設定は商品区分 9 
            Me.SpanSISyouhinKbn1.Style("display") = "none"
            Me.SpanSISyouhinKbn2.Style("display") = "none"
            Me.SpanSISyouhinKbn3.Style("display") = "none"
            Me.SpanSISyouhinKbn9.Style("display") = "inline"
        End If

    End Sub

#End Region

#Region "[物件履歴情報]処理"

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

            Response.Redirect(UrlConst.MAIN)
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

#End Region

#Region "メイン"

#Region "ボタンイベント"

    ''' <summary>
    ''' 重複物件確認画面呼び出しボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTyoufukuCheck_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTyoufukuCheck.ServerClick

        '重複確認結果フラグをセット
        Me.HiddenTyoufukuKakuninFlg1.Value = Boolean.TrueString
        Me.HiddenTyoufukuKakuninFlg2.Value = Boolean.TrueString

        '重複物件確認画面呼び出し
        Dim tmpFocusScript = "objEBI('" & ButtonTyoufukuCheck.ClientID & "').focus();"
        Dim tmpScript As String = "callSearch('" & Me.SelectKIKubun.ClientID & EarthConst.SEP_STRING & Me.TextKITourokuBangou.ClientID & _
                                                                EarthConst.SEP_STRING & Me.TextBukkenMeisyou.ClientID & _
                                                                EarthConst.SEP_STRING & Me.TextBukkenJyuusyo1.ClientID & EarthConst.SEP_STRING & _
                                                                Me.TextBukkenJyuusyo2.ClientID & "', '" & UrlConst.SEARCH_TYOUFUKU & "');"
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & tmpScript, True)
        Exit Sub

    End Sub

    ''' <summary>
    ''' 地盤T更新ボタン処理(隠しボタン)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>地盤Tの更新処理を連棟物件数分行なう</remarks>
    Protected Sub ButtonHiddenUpdate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenUpdate.ServerClick

        Dim tmpScript As String = ""

        '処理件数 >= 連棟物件数(全処理終了)
        If CInt(Me.HiddenSyoriKensuu.Value) >= CInt(HiddenRentouBukkenSuu.Value) Then

            '連棟物件登録完了時には、メッセージを表示
            tmpScript = "alert('" & Messages.MSG018S.Replace("@PARAM1", "登録") & "');"
            ScriptManager.RegisterStartupScript(sender, sender.GetType(), "ButtonHiddenUpdate_ServerClick1", tmpScript, True)

            'ボタンの表示切替
            Me.ChgDispButton(sender)

        Else '処理件数 < 連棟物件数(未処理データあり)

            ' 画面の内容をDBに反映する
            If SaveData() Then '登録成功
                If actBtnId.Value = String.Empty Then
                    actBtnId.Value = Me.ButtonHiddenUpdate.ClientID
                End If

                '連続登録用フラグをセット
                HiddenCallRentouNextFlg.Value = Boolean.TrueString

            Else '登録失敗
                tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "登録") & "');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHiddenUpdate_ServerClick2", tmpScript, True)
                Exit Sub
            End If

        End If
    End Sub

    ''' <summary>
    ''' 保証書NO自動採番処理(隠しボタン)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>採番Mより最終番号を更新、地盤Tに新規追加を行なう。
    ''' エラーがなければ、続いて地盤Tの更新処理を行なう</remarks>
    Protected Sub ButtonHiddenSaiban_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenSaiban.ServerClick
        Dim kubun As String = Me.SelectKIKubun.SelectedValue
        Dim hosyousyo_no As String = ""
        Dim jBn As New Jiban '地盤画面クラス

        Dim intRentouBukkenSuu As Integer = 1 '連棟物件数
        Dim strRentouBukkenSuu As String = ""
        Dim errMess As String = "" 'JS用

        '区分あるいは連棟物件数が未入力の場合、処理を抜ける
        If kubun = "" Or HiddenRentouBukkenSuu.Value = "" Then
            'エラーMSG
            errMess = Messages.MSG013E.Replace("@PARAM1", "区分、連棟物件数")
            MLogic.AlertMessage(sender, errMess)
            'フォーカスセット
            setFocusAJ(Me.ButtonTouroku1)
            Exit Sub
        End If

        '連棟物件数を取得
        strRentouBukkenSuu = CStr(HiddenRentouBukkenSuu.Value)
        strRentouBukkenSuu = StrConv(strRentouBukkenSuu, VbStrConv.Narrow) '全角→半角
        HiddenRentouBukkenSuu.Value = strRentouBukkenSuu '半角で入れ直し

        '入力チェック(連棟物件数)
        '●バイト数チェック(文字列入力フィールドが対象)
        If jBn.ByteCheckSJIS(strRentouBukkenSuu, "3") = False Then
            errMess += Messages.MSG092E.Replace("{0}", "連棟物件数").Replace("{1}", "3")
        End If
        '●数値チェック
        If IsNumeric(strRentouBukkenSuu) = False Then
            errMess += Messages.MSG040E.Replace("@PARAM1", "連棟物件数")
        Else
            intRentouBukkenSuu = CInt(strRentouBukkenSuu)
            '●数値範囲チェック
            If intRentouBukkenSuu <= 0 Or intRentouBukkenSuu > 999 Then
                errMess += Messages.MSG111E.Replace("@PARAM1", "連棟物件数").Replace("@PARAM2", "1").Replace("@PARAM3", "999")
            End If
        End If

        'エラー発生時処理
        If errMess <> "" Then
            HiddenRentouBukkenSuu.Value = "" '初期化

            'エラーメッセージ表示
            MLogic.AlertMessage(sender, errMess)
            'フォーカスセット
            setFocusAJ(Me.ButtonTouroku1)
            Exit Sub
        End If

        '入力チェック
        If Me.checkInput() = False Then Exit Sub

        '***************************************
        ' 加盟店指定調査会社チェック
        '***************************************
        Dim KameitenCd As String = Me.TextKITourokuBangou.Text '加盟店コード
        Dim TysKaisyaCd As String = String.Empty               '調査会社コード
        Dim TysKaisyaJigyousyoCd As String = String.Empty      '調査会社事業所コード
        ' 調査会社等
        Dim tmpTys As String = TextSITysGaisyaCd.Text
        If tmpTys.Length >= 6 Then   '長さ6以上必須
            TysKaisyaCd = tmpTys.Substring(0, tmpTys.Length - 2) '調査会社コード
            TysKaisyaJigyousyoCd = tmpTys.Substring(tmpTys.Length - 2, 2) '調査会社事業所コード
        End If

        If Me.checkInputTysTehaiCenter(Me, KameitenCd, TysKaisyaCd, TysKaisyaJigyousyoCd) = False Then Exit Sub

        ' 地盤テーブル初期登録を実施
        If JLogic.InsertJibanData( _
                                    sender, _
                                    kubun, _
                                    hosyousyo_no, _
                                    userinfo.LoginUserId, _
                                    intRentouBukkenSuu, _
                                    EarthEnum.EnumSinkiTourokuMotoKbnType.EarthMousikomi _
                                    ) _
                                    = False Then

            '採番失敗
            errMess = Messages.MSG019E.Replace("@PARAM1", "採番")
            MLogic.AlertMessage(sender, errMess)
            'フォーカスセット
            setFocusAJ(Me.ButtonTouroku1)
            Exit Sub
        End If

        '保証書NOを退避
        Me.HiddenSentouBangou.Value = hosyousyo_no

        '地盤T更新処理
        actBtnId.Value = Me.ButtonTouroku1.ClientID

        ButtonHiddenUpdate_ServerClick(sender, e)

    End Sub

    ''' <summary>
    ''' 入力チェック処理(隠しボタン)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHiddenInputChk_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenInputChk.ServerClick

        '入力チェック
        If checkInput() = False Then

            '指定値選択時の動き(画面表示時用)
            checkSonota()

            '調査立会者の表示切替
            checkTysTatiaisya()

            '入力チェックNG
            Me.HiddenInputChk.Value = ""
        Else
            '入力チェックOK
            Me.HiddenInputChk.Value = Boolean.TrueString
        End If

    End Sub

    ''' <summary>
    ''' 新規(引継)申込/新規申込 ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSinki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSinkiHikitugi.ServerClick _
                                                                                                                    , ButtonSinki.ServerClick

        'ボタンの表示切替
        Me.ChgDispButton(sender)

    End Sub

    ''' <summary>
    ''' 調査立会者の有無によって、表示を切り替える
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkTysTatiaisya()

        '調査立会者に対するチェックボックスの表示切替
        Dim TatiaisyaScript As String = "ChgDispTatiaisya();"

        Me.RadioTysTatiaisya1.Attributes("onclick") = TatiaisyaScript
        Me.RadioTysTatiaisya0.Attributes("onclick") = TatiaisyaScript

        '調査立会者のラジオボタンをダブルクリックすると、ダミーのラジオボタンを選択する＝＞チェック解除用
        Dim tmpScript As String = "objEBI('" & Me.RadioTysDummy.ClientID & "').click();"
        Me.RadioTysTatiaisya1.Attributes("ondblclick") = tmpScript & TatiaisyaScript
        Me.RadioTysTatiaisya0.Attributes("ondblclick") = tmpScript & TatiaisyaScript

        '調査立会者
        If Me.RadioTysTatiaisya1.Checked Then '有
            '活性化
            Me.CheckTysTatiaisyaSesyuSama.Disabled = False
            Me.CheckTysTatiaisyaTantousya.Disabled = False
            Me.CheckTysTatiaisyaSonota.Disabled = False
        Else '非活性化
            Me.CheckTysTatiaisyaSesyuSama.Disabled = True
            Me.CheckTysTatiaisyaTantousya.Disabled = True
            Me.CheckTysTatiaisyaSonota.Disabled = True
        End If

    End Sub

    ''' <summary>
    ''' 選択値が「その他」の場合、表示を切り替える
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkSonota()
        '指定値選択時の動き(画面表示時用)
        Dim tmpAL As New ArrayList
        tmpAL.Add(TextKouzouSyubetuSonota)
        cl.CheckVisible("9", SelectKouzouSyubetu, tmpAL)
        Dim tmpAL2 As New ArrayList
        tmpAL2.Add(TextYoteiKisoSonota)
        cl.CheckVisible("9", SelectYoteiKiso, tmpAL2)
    End Sub

#End Region

#Region "プライベートメソッド"

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
        Dim e As New System.EventArgs

        '地盤画面共通クラス
        Dim jBn As New Jiban

        '全てのコントロールを有効化(破棄種別チェック用)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        'エラーメッセージ初期化
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New List(Of Control)

        Dim strErrMsg As String = String.Empty '作業用
        Dim blnKamentenFlg As Boolean = False
        Dim kameitenSearchLogic As New KameitenSearchLogic

        '●コード入力値変更チェック
        '登録番号(加盟店コード)
        If TextKITourokuBangou.Text <> HiddenKITourokuBangouMae.Value Or (TextKITourokuBangou.Text <> "" And Me.TextKISyamei.Text = "") Then
            errMess += Messages.MSG030E.Replace("@PARAM1", "登録番号")
            arrFocusTargetCtrl.Add(ButtonKITourokuBangou)
            blnKamentenFlg = True 'フラグを立てる
        End If
        '調査会社コード
        If TextSITysGaisyaCd.Text <> HiddenSITysGaisyaMae.Value Or (TextSITysGaisyaCd.Text <> "" And Me.TextSITysGaisyaMei.Text = "") Then
            errMess += Messages.MSG030E.Replace("@PARAM1", "調査会社コード")
            arrFocusTargetCtrl.Add(ButtonSITysGaisya)
        End If

        '●必須チェック
        '<画面左上部>
        '依頼日
        If Me.TextIraiDate.Text = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "依頼日")
            arrFocusTargetCtrl.Add(Me.TextIraiDate)
        End If
        '<画面右上部>
        '区分
        If Me.SelectKIKubun.SelectedValue = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "区分")
            arrFocusTargetCtrl.Add(Me.SelectKIKubun)
        End If
        '登録番号(加盟店コード)
        If Me.TextKITourokuBangou.Text = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "登録番号")
            arrFocusTargetCtrl.Add(Me.TextKITourokuBangou)
            blnKamentenFlg = True 'フラグを立てる
        End If
        '<画面中央部>
        '物件名称
        If Me.TextBukkenMeisyou.Text = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "物件名称")
            arrFocusTargetCtrl.Add(Me.TextBukkenMeisyou)
        End If
        '施主名有無
        If Me.RadioSesyumei0.Checked = False AndAlso Me.RadioSesyumei1.Checked = False Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "施主名有無")
            arrFocusTargetCtrl.Add(Me.RadioSesyumei1)
        End If
        '今回同時依頼棟数
        If Me.TextDoujiIraiTousuu.Text = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "今回同時依頼棟数")
            arrFocusTargetCtrl.Add(Me.TextDoujiIraiTousuu)
        End If
        '物件住所1
        If Me.TextBukkenJyuusyo1.Text = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "物件住所1")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo1)
        End If
        '調査希望日
        If Me.TextTyousaKibouDate.Text = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "調査希望日")
            arrFocusTargetCtrl.Add(Me.TextTyousaKibouDate)
        End If
        '<画面下部>
        '商品1
        If Me.choSyouhin1.SelectedValue = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "商品1")
            arrFocusTargetCtrl.Add(Me.choSyouhin1)
        End If

        '●日付チェック
        '<画面中央部>
        '依頼日
        If Me.TextIraiDate.Text <> "" Then
            If cl.checkDateHanni(Me.TextIraiDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "依頼日")
                arrFocusTargetCtrl.Add(Me.TextIraiDate)
            End If
        End If
        '調査希望日
        If Me.TextTyousaKibouDate.Text <> "" Then
            If cl.checkDateHanni(Me.TextTyousaKibouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "調査希望日")
                arrFocusTargetCtrl.Add(Me.TextTyousaKibouDate)
            End If
        End If
        '基礎着工予定日FROM
        If Me.TextKsTyakkouYoteiDateFrom.Text <> "" Then
            If cl.checkDateHanni(Me.TextKsTyakkouYoteiDateFrom.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "基礎着工予定日FROM")
                arrFocusTargetCtrl.Add(Me.TextKsTyakkouYoteiDateFrom)
            End If
        End If
        '基礎着工予定日TO
        If Me.TextKsTyakkouYoteiDateTo.Text <> "" Then
            If cl.checkDateHanni(Me.TextKsTyakkouYoteiDateTo.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "基礎着工予定日TO")
                arrFocusTargetCtrl.Add(Me.TextKsTyakkouYoteiDateTo)
            End If
        End If

        '●禁則文字チェック(文字列入力フィールドが対象)
        '<画面左上部>
        '調査連絡先.住所
        If jBn.KinsiStrCheck(Me.TextTysJyuusyo.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "調査連絡先：住所")
            arrFocusTargetCtrl.Add(Me.TextTysJyuusyo)
        End If
        '調査連絡先.TEL
        If jBn.KinsiStrCheck(Me.TextTysTel.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "調査連絡先：TEL")
            arrFocusTargetCtrl.Add(Me.TextTysTel)
        End If
        '調査連絡先.FAX
        If jBn.KinsiStrCheck(Me.TextTysFax.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "調査連絡先：FAX")
            arrFocusTargetCtrl.Add(Me.TextTysFax)
        End If
        '<画面右上部>
        '契約NO
        If jBn.KinsiStrCheck(Me.TextKeiyakuNo.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "契約NO")
            arrFocusTargetCtrl.Add(Me.TextKeiyakuNo)
        End If
        '加盟店.担当者
        If jBn.KinsiStrCheck(Me.TextKITantousya.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "加盟店：担当者")
            arrFocusTargetCtrl.Add(Me.TextKITantousya)
        End If
        '<画面中央部>
        '物件名称
        If jBn.KinsiStrCheck(Me.TextBukkenMeisyou.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件名称")
            arrFocusTargetCtrl.Add(Me.TextBukkenMeisyou)
        End If
        '物件住所1
        If jBn.KinsiStrCheck(Me.TextBukkenJyuusyo1.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件住所1")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo1)
        End If
        '物件住所2
        If jBn.KinsiStrCheck(Me.TextBukkenJyuusyo2.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件住所2")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo2)
        End If
        '物件住所3
        If jBn.KinsiStrCheck(Me.TextBukkenJyuusyo3.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件住所3")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo3)
        End If
        '調査希望時間
        If jBn.KinsiStrCheck(Me.TextTyousaKibouJikan.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "調査希望時間")
            arrFocusTargetCtrl.Add(Me.TextTyousaKibouJikan)
        End If
        '構造その他
        If jBn.KinsiStrCheck(Me.TextKouzouSyubetuSonota.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "構造その他")
            arrFocusTargetCtrl.Add(Me.TextKouzouSyubetuSonota)
        End If
        '予定基礎その他
        If jBn.KinsiStrCheck(Me.TextYoteiKisoSonota.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "予定基礎その他")
            arrFocusTargetCtrl.Add(Me.TextYoteiKisoSonota)
        End If
        '<その他情報>
        '備考
        If jBn.KinsiStrCheck(Me.TextSIBikou.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "備考")
            arrFocusTargetCtrl.Add(Me.TextSIBikou)
        End If
        '備考2
        If jBn.KinsiStrCheck(Me.TextSIBikou2.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "備考2")
            arrFocusTargetCtrl.Add(Me.TextSIBikou2)
        End If
        '物件名寄コード
        If jBn.KinsiStrCheck(Me.TextBukkenNayoseCd.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件名寄コード")
            arrFocusTargetCtrl.Add(Me.TextBukkenNayoseCd)
        End If

        '改行変換
        '<その他情報>
        If Me.TextSIBikou.Value <> "" Then
            Me.TextSIBikou.Value = Me.TextSIBikou.Value.Replace(vbCrLf, " ")
        End If
        If Me.TextSIBikou2.Value <> "" Then
            Me.TextSIBikou2.Value = Me.TextSIBikou2.Value.Replace(vbCrLf, " ")
        End If

        '●バイト数チェック(文字列入力フィールドが対象)
        '<画面上部>
        '調査連絡先：住所
        If jBn.ByteCheckSJIS(Me.TextTysJyuusyo.Text, Me.TextTysJyuusyo.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "調査連絡先：住所")
            arrFocusTargetCtrl.Add(Me.TextTysJyuusyo)
        End If
        '調査連絡先：TEL
        If jBn.ByteCheckSJIS(Me.TextTysTel.Text, Me.TextTysTel.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "調査連絡先：TEL")
            arrFocusTargetCtrl.Add(Me.TextTysTel)
        End If
        '調査連絡先：FAX
        If jBn.ByteCheckSJIS(Me.TextTysFax.Text, Me.TextTysFax.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "調査連絡先：FAX")
            arrFocusTargetCtrl.Add(Me.TextTysFax)
        End If
        '契約NO
        If jBn.ByteCheckSJIS(Me.TextKeiyakuNo.Text, Me.TextKeiyakuNo.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "契約NO")
            arrFocusTargetCtrl.Add(Me.TextKeiyakuNo)
        End If
        '加盟店：担当者
        If jBn.ByteCheckSJIS(Me.TextKITantousya.Text, Me.TextKITantousya.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "加盟店：担当者")
            arrFocusTargetCtrl.Add(Me.TextKITantousya)
        End If
        '<画面中央部>
        '物件名称
        If jBn.ByteCheckSJIS(Me.TextBukkenMeisyou.Text, Me.TextBukkenMeisyou.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "物件名称")
            arrFocusTargetCtrl.Add(Me.TextBukkenMeisyou)
        End If
        '物件住所1
        If jBn.ByteCheckSJIS(Me.TextBukkenJyuusyo1.Text, Me.TextBukkenJyuusyo1.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "物件住所1")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo1)
        End If
        '物件住所2
        If jBn.ByteCheckSJIS(Me.TextBukkenJyuusyo2.Text, Me.TextBukkenJyuusyo2.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "物件住所2")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo2)
        End If
        '物件住所3
        If jBn.ByteCheckSJIS(Me.TextBukkenJyuusyo3.Text, Me.TextBukkenJyuusyo3.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "物件住所3")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo3)
        End If
        '調査希望時間
        If jBn.ByteCheckSJIS(Me.TextTyousaKibouJikan.Text, Me.TextTyousaKibouJikan.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "調査希望時間")
            arrFocusTargetCtrl.Add(Me.TextTyousaKibouJikan)
        End If
        '構造その他
        If jBn.ByteCheckSJIS(Me.TextKouzouSyubetuSonota.Text, Me.TextKouzouSyubetuSonota.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "構造その他")
            arrFocusTargetCtrl.Add(Me.TextKouzouSyubetuSonota)
        End If
        '予定基礎その他
        If jBn.ByteCheckSJIS(Me.TextYoteiKisoSonota.Text, Me.TextYoteiKisoSonota.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "予定基礎その他")
            arrFocusTargetCtrl.Add(Me.TextYoteiKisoSonota)
        End If
        '<その他情報>
        '備考
        If jBn.ByteCheckSJIS(Me.TextSIBikou.Value, 256) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "備考")
            arrFocusTargetCtrl.Add(Me.TextSIBikou)
        End If
        '備考2
        If jBn.ByteCheckSJIS(Me.TextSIBikou2.Value, 512) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "備考2")
            arrFocusTargetCtrl.Add(Me.TextSIBikou2)
        End If
        '物件名寄コード
        If jBn.ByteCheckSJIS(Me.TextBukkenNayoseCd.Value, Me.TextBukkenNayoseCd.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "物件名寄コード")
            arrFocusTargetCtrl.Add(Me.TextBukkenNayoseCd)
        End If

        '●桁数チェック
        '<画面中央部>
        '今回同時依頼棟数
        If jBn.SuutiStrCheck(Me.TextDoujiIraiTousuu.Text, 4, 0) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "今回同時依頼棟数").Replace("@PARAM2", "4").Replace("@PARAM3", "0")
            arrFocusTargetCtrl.Add(Me.TextDoujiIraiTousuu)
        End If
        '設計許容支持力
        If jBn.SuutiStrCheck(Me.TextSekkeiKyoyouSijiryoku.Text, 4, 1) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "設計許容支持力").Replace("@PARAM2", "4").Replace("@PARAM3", "1")
            arrFocusTargetCtrl.Add(Me.TextSekkeiKyoyouSijiryoku)
        End If
        '依頼予定棟数
        If jBn.SuutiStrCheck(Me.TextIraiYoteiTousuu.Text, 4, 0) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "依頼予定棟数").Replace("@PARAM2", "4").Replace("@PARAM3", "0")
            arrFocusTargetCtrl.Add(Me.TextIraiYoteiTousuu)
        End If
        '根切り深さ
        If jBn.SuutiStrCheck(Me.TextNegiriHukasa.Text, 8, 4) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "根切り深さ").Replace("@PARAM2", "8").Replace("@PARAM3", "4")
            arrFocusTargetCtrl.Add(Me.TextNegiriHukasa)
        End If
        '予定盛土厚さ
        If jBn.SuutiStrCheck(Me.TextYoteiMoritutiAtusa.Text, 9, 3) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "予定盛土厚さ").Replace("@PARAM2", "9").Replace("@PARAM3", "3")
            arrFocusTargetCtrl.Add(Me.TextYoteiMoritutiAtusa)
        End If
        '<画面下部>
        '戸数
        If jBn.SuutiStrCheck(Me.TextSIKosuu.Text, 4, 0) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "戸数").Replace("@PARAM2", "4").Replace("@PARAM3", "0")
            arrFocusTargetCtrl.Add(Me.TextSIKosuu)
        End If

        '●その他チェック
        '(Chk02:日付項目.TOの値が、日付項目.FROMの値より以前の場合、エラーとする。)
        '登録処理実行前に、JSでチェック済み
        '基礎着工予定日FROM、TOチェック
        If Me.TextKsTyakkouYoteiDateFrom.Text <> String.Empty And Me.TextKsTyakkouYoteiDateTo.Text <> String.Empty Then
            If Me.TextKsTyakkouYoteiDateFrom.Text > Me.TextKsTyakkouYoteiDateTo.Text Then
                errMess += Messages.MSG022E.Replace("@PARAM1", "基礎着工予定日")
                arrFocusTargetCtrl.Add(Me.TextKsTyakkouYoteiDateFrom)
            End If
        End If

        '(Chk03:画面.区分と加盟店M.区分が不一致の場合、エラーとする。(キー：画面.区分＝加盟店M.区分 AND 画面.登録番号＝加盟店M.加盟店コード)
        '区分、登録番号(加盟店コード)
        ' 入力されているコードをキーに、マスタを検索し、データを1件のみ取得できた場合は
        ' 画面情報を更新する。データが無い場合は、検索画面を表示する
        Dim dataArray As New List(Of KameitenSearchRecord)
        Dim blnTorikesi As Boolean = False
        Dim total_count As Integer = 0

        ' 取得件数を絞り込む場合、引数を追加してください
        If Me.SelectKIKubun.SelectedValue <> "" And Me.TextKITourokuBangou.Text <> "" Then
            dataArray = kameitenSearchLogic.GetKameitenSearchResult(SelectKIKubun.SelectedValue, _
                                                                    TextKITourokuBangou.Text, _
                                                                    blnTorikesi, _
                                                                    total_count)

            If total_count = 1 Then '正常
            Else '異常
                errMess += Messages.MSG120E.Replace("@PARAM1", "区分").Replace("@PARAM2", "加盟店コード")
                arrFocusTargetCtrl.Add(Me.SelectKIKubun)
            End If

        End If

        '●調査概要/同時依頼棟数チェック
        strErrMsg = String.Empty
        If cbLogic.ChkErrTysGaiyou(Me.SelectSITysGaiyou.SelectedValue, Me.TextDoujiIraiTousuu.Text, strErrMsg) = False Then
            errMess += strErrMsg
            arrFocusTargetCtrl.Add(Me.TextDoujiIraiTousuu)
        End If
        '●ビルダー注意事項チェック(加盟店関連のエラーがない場合チェックする)
        strErrMsg = String.Empty
        If blnKamentenFlg = False Then
            If kameitenSearchLogic.ChkBuilderData13(Me.TextKITourokuBangou.Text) Then
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.TrueString
            Else
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.FalseString
            End If
            If cbLogic.ChkErrBuilderData(Me.SelectSITysGaiyou.SelectedValue, Me.TextKITourokuBangou.Text, Me.HiddenKameitenTyuuiJikou.Value, strErrMsg) = False Then
                errMess += strErrMsg
                arrFocusTargetCtrl.Add(Me.TextKITourokuBangou)
            End If
        End If

        '重複物件チェック
        '重複物件あり時、重複物件ポップアップにて確認していない場合
        If Me.HiddenTyoufukuKakuninFlg1.Value <> Boolean.TrueString Or _
           Me.HiddenTyoufukuKakuninFlg2.Value <> Boolean.TrueString Then
            errMess += Messages.MSG017E
            arrFocusTargetCtrl.Add(Me.ButtonTyoufukuCheck)
        End If

        '●物件履歴情報の入力チェック
        If Me.checkInputBR(errMess, arrFocusTargetCtrl, emBrCtrl.intCtrl1) = False Then
            ' エラーメッセージが追加されたので物件履歴情報を展開する
            Me.TBodyBRInfo.Attributes("style") = "display:inline;"
        End If
        If Me.checkInputBR(errMess, arrFocusTargetCtrl, emBrCtrl.intCtrl2) = False Then
            ' エラーメッセージが追加されたので物件履歴情報を展開する
            Me.TBodyBRInfo.Attributes("style") = "display:inline;"
        End If
        If Me.checkInputBR(errMess, arrFocusTargetCtrl, emBrCtrl.intCtrl3) = False Then
            ' エラーメッセージが追加されたので物件履歴情報を展開する
            Me.TBodyBRInfo.Attributes("style") = "display:inline;"
        End If

        '物件NOを取得
        Dim strBukkenNo As String = String.Empty '物件NOはこの時点で未決定
        Dim strBukkenNayoseCd As String = Me.TextBukkenNayoseCd.Value.ToUpper
        Dim blnBukkenNoFlg As Boolean = True

        '物件名寄コード
        If strBukkenNayoseCd = String.Empty Then '未入力
            strBukkenNayoseCd = strBukkenNo '未入力の場合、当物件NOをセット
        End If

        '物件名寄コード(入力不正チェック)
        If Me.TextBukkenNayoseCd.Value <> String.Empty Then
            '11桁のチェック
            If sLogic.GetStrByteSJIS(Me.TextBukkenNayoseCd.Value) = Me.TextBukkenNayoseCd.MaxLength Then
            Else
                blnBukkenNoFlg = False

                errMess += Messages.MSG040E.Replace("@PARAM1", "物件名寄コード") & "【区分+保証書NO(番号)】\r\n"
                arrFocusTargetCtrl.Add(Me.TextBukkenNayoseCd)
            End If
        End If

        If blnBukkenNoFlg Then
            '物件名寄先を指定している場合のみチェック
            If strBukkenNayoseCd <> String.Empty Then
                '名寄先が親物件かのチェック
                If JLogic.ChkBukkenNayoseOyaBukken(strBukkenNo, strBukkenNayoseCd) = False Then
                    errMess += Messages.MSG167E.Replace("@PARAM1", "名寄先の物件").Replace("@PARAM2", "子物件").Replace("@PARAM3", "物件名寄コード")
                    arrFocusTargetCtrl.Add(Me.TextBukkenNayoseCd)
                End If

                '自物件の名寄状況チェック→当画面では不要
            End If
        End If

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If errMess <> "" Then
            Dim tmpScript As String = ""

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
    ''' 入力項目チェック(物件履歴情報)
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
    Public Function checkInputBR( _
                          ByRef errMess As String _
                          , ByRef arrFocusTargetCtrl As List(Of Control) _
                          , ByVal emBrCtrl As emBrCtrl _
                          ) As Boolean

        '地盤画面共通クラス
        Dim jBn As New Jiban

        'エラーメッセージ初期化
        Dim strErrMsg As String = errMess '比較用
        Dim strSettouji As String = String.Empty

        'チェック作業用
        Dim objSelectBRSyubetu As New DropDownList
        Dim objHiddenBRBunrui As New HtmlInputHidden
        Dim objTextBRHizuke As New TextBox
        Dim objTextBRHanyouCd As New TextBox
        Dim objTextAreaBRNaiyou As New HtmlTextArea

        Select Case emBrCtrl
            Case MousikomiInput.emBrCtrl.intCtrl1
                strSettouji = "物件履歴１："

                objSelectBRSyubetu = Me.SelectBRSyubetu1
                objHiddenBRBunrui = Me.HiddenBRBunrui1
                objTextBRHizuke = Me.TextBRHizuke1
                objTextBRHanyouCd = Me.TextBRHanyouCd1
                objTextAreaBRNaiyou = Me.TextAreaBRNaiyou1

            Case MousikomiInput.emBrCtrl.intCtrl2
                strSettouji = "物件履歴２："

                objSelectBRSyubetu = Me.SelectBRSyubetu2
                objHiddenBRBunrui = Me.HiddenBRBunrui2
                objTextBRHizuke = Me.TextBRHizuke2
                objTextBRHanyouCd = Me.TextBRHanyouCd2
                objTextAreaBRNaiyou = Me.TextAreaBRNaiyou2

            Case MousikomiInput.emBrCtrl.intCtrl3
                strSettouji = "物件履歴３："

                objSelectBRSyubetu = Me.SelectBRSyubetu3
                objHiddenBRBunrui = Me.HiddenBRBunrui3
                objTextBRHizuke = Me.TextBRHizuke3
                objTextBRHanyouCd = Me.TextBRHanyouCd3
                objTextAreaBRNaiyou = Me.TextAreaBRNaiyou3

            Case Else
                'チェック対象外として処理を抜ける
                Return True
        End Select

        '●コード入力値変更チェック
        'なし

        '●必須チェック
        '種別、分類
        If objSelectBRSyubetu.SelectedValue <> String.Empty And objHiddenBRBunrui.Value = String.Empty _
            Or objSelectBRSyubetu.SelectedValue = String.Empty And objHiddenBRBunrui.Value <> String.Empty Then
            errMess += strSettouji & Messages.MSG013E.Replace("@PARAM1", "種別と分類")
            arrFocusTargetCtrl.Add(objSelectBRSyubetu)
        End If
        '種別、分類＝未入力でかつ、以外が入力の場合
        If objSelectBRSyubetu.SelectedValue = String.Empty And objHiddenBRBunrui.Value = String.Empty Then
            If objTextBRHizuke.Text <> String.Empty _
                Or objTextBRHanyouCd.Text <> String.Empty _
                    Or objTextAreaBRNaiyou.Value <> String.Empty Then

                errMess += strSettouji & Messages.MSG013E.Replace("@PARAM1", "種別と分類")
                arrFocusTargetCtrl.Add(objSelectBRSyubetu)
            End If
        End If

        '●日付チェック
        '(汎用)日付
        If objTextBRHizuke.Text <> String.Empty Then
            If cl.checkDateHanni(objTextBRHizuke.Text) = False Then
                errMess += strSettouji & Messages.MSG014E.Replace("@PARAM1", "日付")
                arrFocusTargetCtrl.Add(objTextBRHizuke)
            End If
        End If

        '●禁則文字チェック(文字列入力フィールドが対象)
        '汎用コード
        If jBn.KinsiStrCheck(objTextBRHanyouCd.Text) = False Then
            errMess += strSettouji & Messages.MSG015E.Replace("@PARAM1", "汎用コード")
            arrFocusTargetCtrl.Add(objTextBRHanyouCd)
        End If
        '内容
        If jBn.KinsiStrCheck(objTextAreaBRNaiyou.Value) = False Then
            errMess += strSettouji & Messages.MSG015E.Replace("@PARAM1", "内容")
            arrFocusTargetCtrl.Add(objTextAreaBRNaiyou)
        End If

        '改行変換
        If objTextAreaBRNaiyou.Value <> "" Then
            objTextAreaBRNaiyou.Value = objTextAreaBRNaiyou.Value.Replace(vbCrLf, " ")
        End If

        '●バイト数チェック(文字列入力フィールドが対象)
        '汎用コード
        If jBn.ByteCheckSJIS(objTextBRHanyouCd.Text, objTextBRHanyouCd.MaxLength) = False Then
            errMess += strSettouji & Messages.MSG016E.Replace("@PARAM1", "汎用コード")
            arrFocusTargetCtrl.Add(objTextBRHanyouCd)
        End If
        '内容
        If jBn.ByteCheckSJIS(objTextAreaBRNaiyou.Value, 512) = False Then
            errMess += strSettouji & Messages.MSG016E.Replace("@PARAM1", "内容")
            arrFocusTargetCtrl.Add(objTextAreaBRNaiyou)
        End If

        '●桁数チェック
        'なし

        '●その他チェック
        'なし

        If errMess <> strErrMsg Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' 入力項目チェック(調査手配センター)
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
    Public Function checkInputTysTehaiCenter(ByVal sender As Object, ByVal KameitenCd As String, ByVal TysKaisyaCd As String, ByVal TysKaisyaJigyousyoCd As String) As Boolean
        Dim e As New System.EventArgs

        '地盤画面共通クラス
        Dim jBn As New Jiban
        Dim logic As New MousikomiInputLogic

        '全てのコントロールを有効化(破棄種別チェック用)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        'エラーメッセージ初期化
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New List(Of Control)

        Dim strTysTehai As String = String.Empty

        strTysTehai = cbLogic.ChkExistKameitenTysTehaiCenter(KameitenCd, TysKaisyaCd & TysKaisyaJigyousyoCd)
        If strTysTehai <> String.Empty Then
            errMess += Messages.MSG206E.Replace("@PARAM1", strTysTehai)
            arrFocusTargetCtrl.Add(Me.TextSITysGaisyaCd)
        End If

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If errMess <> "" Then
            Dim tmpScript As String = ""

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
    ''' 画面の内容をDBに反映する
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function SaveData() As Boolean

        ' 画面内容より地盤レコードを生成する
        Dim jibanRec As New JibanRecordMousikomiInput

        ' 邸別データ修正用のロジッククラス
        Dim logic As New MousikomiInputLogic

        Dim dtNow As DateTime = DateTime.Now
        'エラーメッセージ初期化
        Dim errMess As String = ""

        '***************************************
        ' 地盤データ
        '***************************************
        ' 区分
        cl.SetDisplayString(SelectKIKubun.SelectedValue, jibanRec.Kbn)
        ' 番号（保証書NO）
        jibanRec.HosyousyoNo = CStr(Me.HiddenSentouBangou.Value)
        ' 施主名
        cl.SetDisplayString(TextBukkenMeisyou.Text, jibanRec.SesyuMei)
        ' 受注物件名
        cl.SetDisplayString(TextBukkenMeisyou.Text, jibanRec.JyutyuuBukkenMei)
        ' 物件住所1
        cl.SetDisplayString(TextBukkenJyuusyo1.Text, jibanRec.BukkenJyuusyo1)
        ' 物件住所2
        cl.SetDisplayString(TextBukkenJyuusyo2.Text, jibanRec.BukkenJyuusyo2)
        ' 物件住所3
        cl.SetDisplayString(TextBukkenJyuusyo3.Text, jibanRec.BukkenJyuusyo3)
        '物件名寄コード
        If Me.TextBukkenNayoseCd.Value = String.Empty Then
            jibanRec.BukkenNayoseCdFlg = True 'ロジッククラスでセット
        Else
            jibanRec.BukkenNayoseCdFlg = False
            jibanRec.BukkenNayoseCd = Me.TextBukkenNayoseCd.Value.ToUpper  '画面.物件名寄コード
        End If
        ' 加盟店コード
        cl.SetDisplayString(TextKITourokuBangou.Text, jibanRec.KameitenCd)
        ' 商品区分
        If RadioSISyouhinKbn1.Checked Then '60年保証
            jibanRec.SyouhinKbn = RadioSISyouhinKbn1.Value
        ElseIf RadioSISyouhinKbn2.Checked Then '土地販売
            jibanRec.SyouhinKbn = RadioSISyouhinKbn2.Value
        ElseIf RadioSISyouhinKbn3.Checked Then 'リフォーム
            jibanRec.SyouhinKbn = RadioSISyouhinKbn3.Value
        Else 'その他
            jibanRec.SyouhinKbn = RadioSISyouhinKbn9.Value
        End If
        ' 備考 ※改行コードは変換済みのこと
        cl.SetDisplayString(TextSIBikou.Value, jibanRec.Bikou)
        ' 備考2 ※改行コードは変換済みのこと
        cl.SetDisplayString(TextSIBikou2.Value, jibanRec.Bikou2)
        ' 依頼日
        cl.SetDisplayString(TextIraiDate.Text, jibanRec.IraiDate)
        ' 依頼担当者
        cl.SetDisplayString(TextKITantousya.Text, jibanRec.IraiTantousyaMei)
        ' 契約NO
        cl.SetDisplayString(TextKeiyakuNo.Text, jibanRec.KeiyakuNo)
        ' 階層
        cl.SetDisplayString(Me.SelectKaisou.SelectedValue, jibanRec.Kaisou)
        ' 新築立替
        cl.SetDisplayString(Me.SelectSintikuTatekae.SelectedValue, jibanRec.SintikuTatekae)
        ' 構造種別
        cl.SetDisplayString(Me.SelectKouzouSyubetu.SelectedValue, jibanRec.Kouzou)
        ' 構造種別MEMO(その他)
        cl.SetDisplayString(TextKouzouSyubetuSonota.Text, jibanRec.KouzouMemo)
        ' 車庫(地下車庫計画)
        cl.SetDisplayString(Me.SelectTikaSyakoKeikaku.SelectedValue, jibanRec.Syako)
        ' 根切り深さ
        cl.SetDisplayString(TextNegiriHukasa.Text, jibanRec.NegiriHukasa)
        ' 予定盛土厚さ
        If TextYoteiMoritutiAtusa.Text <> String.Empty Then
            cl.SetDisplayString(TextYoteiMoritutiAtusa.Text / 10, jibanRec.YoteiMoritutiAtusa)
        Else
            cl.SetDisplayString(TextYoteiMoritutiAtusa.Text, jibanRec.YoteiMoritutiAtusa)
        End If
        ' 予定基礎
        cl.SetDisplayString(Me.SelectYoteiKiso.SelectedValue, jibanRec.YoteiKs)
        ' 予定基礎MEMO(その他)
        cl.SetDisplayString(TextYoteiKisoSonota.Text, jibanRec.YoteiKsMemo)
        ' 調査会社等
        Dim tmpTys As String = TextSITysGaisyaCd.Text
        If tmpTys.Length >= 6 Then   '長さ6以上必須
            jibanRec.TysKaisyaCd = tmpTys.Substring(0, tmpTys.Length - 2) '調査会社コード
            jibanRec.TysKaisyaJigyousyoCd = tmpTys.Substring(tmpTys.Length - 2, 2) '調査会社事業所コード
        End If
        ' 調査方法
        cl.SetDisplayString(Me.SelectSITysHouhou.SelectedValue, jibanRec.TysHouhou)
        ' 調査概要
        cl.SetDisplayString(Me.SelectSITysGaiyou.SelectedValue, jibanRec.TysGaiyou)
        ' 商品コード
        cl.SetDisplayString(Me.choSyouhin1.SelectedValue, jibanRec.SyouhinCd1)
        ' 調査希望日
        cl.SetDisplayString(TextTyousaKibouDate.Text, jibanRec.TysKibouDate)
        ' 調査希望時間
        cl.SetDisplayString(TextTyousaKibouJikan.Text, jibanRec.TysKibouJikan)
        ' 立会有無
        If RadioTysTatiaisya0.Checked Then '無
            cl.SetDisplayString(0, jibanRec.TatiaiUmu)
        ElseIf RadioTysTatiaisya1.Checked Then '有
            cl.SetDisplayString(1, jibanRec.TatiaiUmu)
        Else
            cl.SetDisplayString(Integer.MinValue, jibanRec.TatiaiUmu)
        End If
        ' 立会者コード
        cl.SetDisplayString(cl.GetTatiaiCd(Me.CheckTysTatiaisyaSesyuSama, Me.CheckTysTatiaisyaTantousya, Me.CheckTysTatiaisyaSonota), jibanRec.TatiaisyaCd)
        '保証書発行状況
        cl.SetDisplayString(Integer.MinValue, jibanRec.HosyousyoHakJyky)
        '保証書発行状況設定日
        cl.SetDisplayString(DateTime.MinValue, jibanRec.HosyousyoHakJykySetteiDate)
        ' 保証有無
        cl.SetDisplayString(SelectSIHosyouUmu.SelectedValue, jibanRec.HosyouUmu)
        ' 保証期間
        cl.SetDisplayString(HiddenHosyouKikan.Value, jibanRec.HosyouKikan)
        ' 更新日時
        cl.SetDisplayString(dtNow, jibanRec.UpdDatetime)
        ' 同時依頼棟数
        cl.SetDisplayString(TextDoujiIraiTousuu.Text, jibanRec.DoujiIraiTousuu)
        ' 瑕疵有無
        cl.SetDisplayString(SelectSITatemonoKensa.SelectedValue, jibanRec.KasiUmu)
        ' 工事会社請求有無
        Dim intTmp As Integer = IIf(HiddenKjGaisyaSeikyuuUmu.Value = "1", HiddenKjGaisyaSeikyuuUmu.Value, Integer.MinValue)
        cl.SetDisplayString(intTmp, jibanRec.KojGaisyaSeikyuuUmu)
        ' 経由
        jibanRec.Keiyu = IIf(SelectSIKeiyu.SelectedValue = "", 0, SelectSIKeiyu.SelectedValue)
        ' 設計許容支持力
        cl.SetDisplayString(TextSekkeiKyoyouSijiryoku.Text, jibanRec.SekkeiKyoyouSijiryoku)
        ' 依頼予定棟数
        cl.SetDisplayString(TextIraiYoteiTousuu.Text, jibanRec.IraiYoteiTousuu)
        ' 建物用途NO
        cl.SetDisplayString(Me.SelectTatemonoYouto.SelectedValue, jibanRec.TatemonoYoutoNo)
        ' 戸数
        cl.SetDisplayString(TextSIKosuu.Text, jibanRec.Kosuu)
        ' 更新者
        jibanRec.Kousinsya = cbLogic.GetKousinsya(userinfo.LoginUserId, dtNow)

        '******************
        '* 調査連絡先
        '******************
        ' 調査連絡先_宛先
        cl.SetDisplayString(TextTysJyuusyo.Text, jibanRec.TysRenrakusakiAtesakiMei)
        ' 調査連絡先_TEL
        cl.SetDisplayString(TextTysTel.Text, jibanRec.TysRenrakusakiTel)
        ' 調査連絡先_FAX
        cl.SetDisplayString(TextTysFax.Text, jibanRec.TysRenrakusakiFax)

        ' 更新ログインユーザーID
        cl.SetDisplayString(userinfo.LoginUserId, jibanRec.UpdLoginUserId)

        ' 予約済FLG
        jibanRec.YoyakuZumiFlg = IIf(CheckYoyakuZumi.Checked, 1, 0)

        '******************
        '* 添付資料
        '******************
        ' 案内図
        jibanRec.AnnaiZu = IIf(CheckTPAnnaiZu.Checked, 1, 0)
        ' 配置図
        jibanRec.HaitiZu = IIf(CheckTPHaitiZu.Checked, 1, 0)
        ' 各階平面図
        jibanRec.KakukaiHeimenZu = IIf(CheckTPKakukaiHeimenZu.Checked, 1, 0)
        ' 基礎伏図
        jibanRec.KsHuseZu = IIf(CheckTPKsFuseZu.Checked, 1, 0)
        ' 基礎断面図
        jibanRec.KsDanmenZu = IIf(CheckTPKsDanmenZu.Checked, 1, 0)
        ' 造成計画図
        jibanRec.ZouseiKeikakuZu = IIf(CheckTPZouseiKeikakuZu.Checked, 1, 0)

        '******************
        '* 基礎着工予定日
        '******************
        ' 基礎着工予定日FROM
        cl.SetDisplayString(TextKsTyakkouYoteiDateFrom.Text, jibanRec.KsTyakkouYoteiFromDate)
        ' 基礎着工予定日TO
        cl.SetDisplayString(TextKsTyakkouYoteiDateTo.Text, jibanRec.KsTyakkouYoteiToDate)

        '******************
        '* 施主名有無
        '******************
        ' 施主名有無
        If RadioSesyumei0.Checked Then '無
            cl.SetDisplayString(0, jibanRec.SesyuMeiUmu)
        ElseIf RadioSesyumei1.Checked Then '有
            cl.SetDisplayString(1, jibanRec.SesyuMeiUmu)
        Else
            cl.SetDisplayString(Integer.MinValue, jibanRec.SesyuMeiUmu)
        End If

        '***************************************
        ' 商品以外の自動設定
        '***************************************
        If Me.ChkOtherAutoSetting(Me, jibanRec) = False Then
            Return False
        End If

        ''***************************************
        '' 加盟店指定調査会社チェック
        ''***************************************
        'If Me.checkInputTysTehaiCenter(Me, jibanRec) = False Then
        '    Return False
        'End If

        '***************************************
        ' 商品の自動設定(商品１～２)
        '***************************************
        '商品1なし時はエラーを返す
        If Me.ChkSyouhin12AutoSetting(Me, jibanRec) = False Then
            Return False
        End If

        '***************************************
        ' 物件履歴情報の設定
        '***************************************
        Dim listBr As New List(Of BukkenRirekiRecord)
        Me.SetBukkenRirekiInfo(Me, jibanRec, listBr)

        '***************************************
        ' その他
        '***************************************
        '連棟物件数
        jibanRec.RentouBukkenSuu = CInt(Me.HiddenRentouBukkenSuu.Value)
        '処理件数
        jibanRec.SyoriKensuu = CInt(Me.HiddenSyoriKensuu.Value)

        Dim intGenzaiSyoriKensuu As Integer = CInt(Me.HiddenSyoriKensuu.Value)
        '===========================================================

        ' データの更新を行います
        If logic.saveData(Me, jibanRec, intGenzaiSyoriKensuu, listBr) = False Then
            Return False
        End If
        '処理件数を格納
        Me.HiddenSyoriKensuu.Value = CStr(intGenzaiSyoriKensuu)

        '登録した地盤データを画面にセット
        SetDispJibanData(jibanRec)

        Return True
    End Function

    ''' <summary>
    ''' 重複チェックボタン押下時の処理（非表示ボタン）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonExeTyoufukuCheck_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '地盤画面共通クラス
        Dim jBn As New Jiban

        '禁則文字列チェック
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList

        '物件名称
        If jBn.KinsiStrCheck(Me.TextBukkenMeisyou.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件名称")
            arrFocusTargetCtrl.Add(Me.TextBukkenMeisyou)
        End If
        '物件住所1
        If jBn.KinsiStrCheck(Me.TextBukkenJyuusyo1.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件住所1")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo1)
        End If
        '物件住所2
        If jBn.KinsiStrCheck(Me.TextBukkenJyuusyo2.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件住所2")
            arrFocusTargetCtrl.Add(Me.TextBukkenJyuusyo2)
        End If

        If errMess <> String.Empty Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                SetFocus(arrFocusTargetCtrl(0))
            End If
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "err", tmpScript, True)
        Else
            '入力チェックOKなら、重複チェック実行
            Me.CheckTyoufuku(sender)
        End If

    End Sub

    ''' <summary>
    ''' 重複チェック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub CheckTyoufuku(ByVal sender As System.Object)
        Dim bolResult1 As Boolean = True
        Dim bolResult2 As Boolean = True

        ' 施主名が空白以外の場合、施主名の重複チェックを実施
        If Me.TextBukkenMeisyou.Text.Trim <> "" Then
            If JLogic.ChkTyouhuku(Me.SelectKIKubun.SelectedValue, _
                                 String.Empty, _
                                 Me.TextBukkenMeisyou.Text.Trim) = True Then
                bolResult1 = False
            End If
        End If

        ' 物件住所が空白以外の場合、物件住所の重複チェックを実施
        If Me.TextBukkenJyuusyo1.Text.Trim() <> "" Or Me.TextBukkenJyuusyo2.Text.Trim() <> "" Then
            If JLogic.ChkTyouhuku(Me.SelectKIKubun.SelectedValue, _
                                 String.Empty, _
                                 Me.TextBukkenJyuusyo1.Text.Trim(), _
                                 Me.TextBukkenJyuusyo2.Text.Trim()) = True Then
                bolResult2 = False
            End If
        End If

        If sender IsNot Nothing Then
            'チェック処理のトリガーによって、確認フラグをセット
            If Me.HiddenTyoufukuKakuninTargetId.Value = Me.TextBukkenMeisyou.ClientID Then
                '施主名変更時に実行された場合
                Me.HiddenTyoufukuKakuninFlg1.Value = bolResult1.ToString
            ElseIf (Me.HiddenTyoufukuKakuninTargetId.Value = Me.TextBukkenJyuusyo1.ClientID Or _
                    Me.HiddenTyoufukuKakuninTargetId.Value = Me.TextBukkenJyuusyo2.ClientID) Then
                '住所変更時に実行された場合
                Me.HiddenTyoufukuKakuninFlg2.Value = bolResult2.ToString
            ElseIf Me.HiddenTyoufukuKakuninTargetId.Value = Me.SelectKIKubun.ClientID Then
                '区分変更時に実行された場合
                Me.HiddenTyoufukuKakuninFlg1.Value = bolResult1.ToString
                Me.HiddenTyoufukuKakuninFlg2.Value = bolResult2.ToString
            End If
        End If

        '重複が存在する場合、「重複物件あり」ボタンを有効化
        If bolResult1 And bolResult2 Then
            Me.ButtonTyoufukuCheck.Disabled = True
            Me.ButtonTyoufukuCheck.Value = "重複物件なし"
            Me.HiddenTyoufukuKakuninFlg1.Value = Boolean.TrueString
            Me.HiddenTyoufukuKakuninFlg2.Value = Boolean.TrueString
        Else
            Me.ButtonTyoufukuCheck.Disabled = False
            Me.ButtonTyoufukuCheck.Value = "重複物件あり"
        End If

        ' チェック処理のトリガーになったIDを格納しているコントロールをクリア
        Me.HiddenTyoufukuKakuninTargetId.Value = String.Empty

        UpdatePanelTyoufukuCheck.Update()

    End Sub

    ''' <summary>
    ''' 先頭番号の表示切替
    ''' </summary>
    ''' <param name="blnDispFlg">表示 or 非表示</param>
    ''' <remarks></remarks>
    Private Sub SetDispBangou(ByVal blnDispFlg As Boolean)

        If blnDispFlg Then '表示
            '番号
            Me.TextBangou.Visible = True
            Me.TextBangou.Text = SetFormatBangou(CStr(Me.HiddenSentouBangou.Value), CStr(Me.HiddenRentouBukkenSuu.Value))

        Else '非表示
            '番号
            Me.TextBangou.Visible = False
            Me.TextBangou.Text = String.Empty

        End If

    End Sub

    ''' <summary>
    ''' 登録処理後、地盤データを画面に表示する
    ''' </summary>
    ''' <param name="recJibanData"></param>
    ''' <remarks></remarks>
    Private Sub SetDispJibanData(ByVal recJibanData As JibanRecordMousikomiInput)
        '表示切替
        Dim e As System.EventArgs = New System.EventArgs

        '処理件数 >= 連棟物件数 の場合、全処理終了
        If recJibanData.SyoriKensuu >= recJibanData.RentouBukkenSuu Then

            '自動採番した番号を画面にセット
            SetDispBangou(True)

            '調査会社
            Me.TextSITysGaisyaCd.Text = cl.GetDispStr(recJibanData.TysKaisyaCd + recJibanData.TysKaisyaJigyousyoCd)
            Me.tyousakaisyaSearchSub(Me, e, False)

            '調査方法のDDL表示処理
            cl.ps_SetSelectTextBoxTysHouhou(recJibanData.TysHouhou, Me.SelectSITysHouhou, False, Me.TextSITysHouhouCd)

            '調査方法
            cl.SetDisplayString(recJibanData.TysHouhou, Me.TextSITysHouhouCd.Text)

            '調査概要
            If recJibanData.TysGaiyou <> 0 Then
                Me.SelectSITysGaiyou.SelectedValue = cl.GetDispNum(recJibanData.TysGaiyou, "")
            End If

            Me.UpdatePanelSonota.Update()

            '保証商品
            cl.ChgDispHosyouSyouhin(recJibanData.HosyouSyouhinUmu, Me.TextHosyouSyouhinUmu)
            Me.UpdatePanelHosyouSyouhinUmu.Update()

        End If
    End Sub

    ''' <summary>
    ''' 先頭番号と連棟物件数を取得し、指定のフォーマット文字列を返す
    ''' ※指定フォーマット
    ''' 【番号：XXXXXXXXXX ～ XXXXXXXXXX】
    ''' ただし、連棟物件数=1時は、【番号：XXXXXXXXXX】
    ''' </summary>
    ''' <param name="strSentouBangou"></param>
    ''' <param name="strRentouSuu"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetFormatBangou(ByVal strSentouBangou As String, ByVal strRentouSuu As String) As String
        Dim strTmp As String = ""
        Dim strFomatL As String = "【物件番号："
        Dim strFomatC As String = " ～ "
        Dim strFomatR As String = "】"
        Dim strKbn As String = Me.SelectKIKubun.SelectedValue

        If strSentouBangou = String.Empty Or strRentouSuu = String.Empty Then
            Return strTmp
            Exit Function
        End If

        If strRentouSuu = "1" Then
            strTmp = strFomatL & strKbn & strSentouBangou & strFomatR
        Else
            ' 番号(保証書NO) 作業用
            Dim intLastBangou As Integer = CInt(strSentouBangou) + (CInt(strRentouSuu) - 1)
            Dim strLastBangou As String = Format(intLastBangou, "0000000000") 'フォーマット

            strTmp = strFomatL & strKbn & strSentouBangou & strFomatC & strKbn & strLastBangou & strFomatR
        End If

        Return strTmp
    End Function

    ''' <summary>
    ''' コントロールの有効無効を切替える
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetEnableControl(ByVal blnFlg As Boolean)
        '地盤画面共通クラス
        Dim jBn As New Jiban

        '有効化、無効化の対象外にするコントロール郡
        Dim noTarget As New Hashtable
        noTarget.Add(Me.ButtonSinkiHikitugi.ID, True) '新規(引継)申込ボタン
        noTarget.Add(Me.ButtonSinki.ID, True) '新規申込ボタン
        noTarget.Add(Me.TextBangou.ID, True) '番号

        If blnFlg Then
            '全てのコントロールを無効化
            jBn.ChangeDesabledAll(Me, True, noTarget)

            setFocusAJ(Me.ButtonSinkiHikitugi) 'フォーカス
        Else
            '全てのコントロールを有効化
            jBn.ChangeDesabledAll(Me, False, noTarget)

            setFocusAJ(Me.ButtonTouroku1) 'フォーカス
        End If

    End Sub

    ''' <summary>
    ''' 登録 実行/新規(引継)申込/新規申込 ボタンの表示切替を行なう
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub ChgDispButton(ByVal sender As System.Object)

        Dim objBtn As HtmlInputButton = CType(sender, HtmlInputButton)

        '画面コントロールの活性化制御を行なう
        Select Case objBtn.ID  '実行ボタンを取得
            Case Me.ButtonTouroku1.ID, Me.ButtonTouroku2.ID, Me.ButtonHiddenUpdate.ID  '登録実行ボタン
                '新規ボタン表示
                Me.ButtonSinkiHikitugi.Visible = True
                Me.ButtonSinki.Visible = True
                '登録ボタン非表示
                Me.ButtonTouroku1.Visible = False
                Me.ButtonTouroku2.Visible = False

                '番号の表示切替
                SetDispBangou(True)

                '指定値選択時の動き(画面表示時用)
                checkSonota()

                '調査立会者の表示切替
                checkTysTatiaisya()

                '画面項目非活性化
                Me.SetEnableControl(True)

            Case Me.ButtonSinkiHikitugi.ID '新規(引継)申込登録ボタン
                '新規ボタン非表示
                Me.ButtonSinkiHikitugi.Visible = False
                Me.ButtonSinki.Visible = False
                '登録ボタン表示
                Me.ButtonTouroku1.Visible = True
                Me.ButtonTouroku2.Visible = True

                '画面項目活性化
                Me.SetEnableControl(False)

                '番号の表示切替
                SetDispBangou(False)

                '指定値選択時の動き(画面表示時用)
                checkSonota()

                '調査立会者の表示切替
                checkTysTatiaisya()

                '重複チェックを行う(チェック実行トリガーは区分とする)
                Me.HiddenTyoufukuKakuninTargetId.Value = Me.SelectKIKubun.ClientID
                Me.CheckTyoufuku(sender)

                '先頭番号
                Me.HiddenSentouBangou.Value = String.Empty
                '連棟物件数
                Me.HiddenRentouBukkenSuu.Value = "1"
                '処理件数
                Me.HiddenSyoriKensuu.Value = "0"
                '連棟続行FLG
                HiddenCallRentouNextFlg.Value = String.Empty
                '入力チェックFLG
                Me.HiddenInputChk.Value = String.Empty

            Case Me.ButtonSinki.ID '新規申込ボタン
                '画面遷移（リロード）
                Server.Transfer(UrlConst.MOUSIKOMI_INPUT)

            Case Else
                'エラー
                Exit Select
        End Select
    End Sub

#End Region

#End Region

#Region "商品以外の自動設定"

    ''' <summary>
    ''' 商品以外の自動設定処理を行なう。
    ''' ※調査会社、調査方法、
    ''' 保証期間、工事会社請求有無
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanrec"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChkOtherAutoSetting(ByVal sender As Object, ByRef jibanRec As JibanRecordMousikomiInput) As Boolean

        Dim logic As New MousikomiInputLogic
        Dim blnSiteiTysGaisya As Boolean = False
        Dim tmpTys As String = ""
        Dim blnTysGaiyou As Boolean = False

        '調査会社の自動設定
        '調査会社
        Dim strTysGaisya As String = Me.TextSITysGaisyaCd.Text

        If strTysGaisya = String.Empty Then '未入力

            '指定調査会社の取得および設定
            blnSiteiTysGaisya = logic.ChkExistSiteiTysGaisya(Me.TextKITourokuBangou.Text, strTysGaisya)
            If blnSiteiTysGaisya Then
                ' 取得した調査会社
                tmpTys = strTysGaisya
            Else
                ' 仮調査会社
                tmpTys = EarthConst.KARI_TYOSA_KAISYA_CD
            End If

            If tmpTys.Length >= 6 Then   '長さ6以上必須
                jibanRec.TysKaisyaCd = tmpTys.Substring(0, tmpTys.Length - 2) '調査会社コード
                jibanRec.TysKaisyaJigyousyoCd = tmpTys.Substring(tmpTys.Length - 2, 2) '調査会社事業所コード
            End If

        End If

        '調査方法の自動設定
        '調査会社=未入力でかつ、調査方法=未入力の場合でかつ、自動設定の調査会社<>仮調査会社の場合
        If Me.TextSITysGaisyaCd.Text = String.Empty _
            AndAlso Me.SelectSITysHouhou.SelectedValue = String.Empty _
                AndAlso tmpTys <> String.Empty _
                    AndAlso tmpTys <> EarthConst.KARI_TYOSA_KAISYA_CD Then

            jibanRec.TysHouhou = 1
            blnTysGaiyou = True
        Else
            If Me.SelectSITysHouhou.SelectedValue = String.Empty Then '未入力
                jibanRec.TysHouhou = 90
                blnTysGaiyou = True
            End If
        End If

        '加盟店による自動設定
        '保証期間
        If Me.HiddenHosyouKikan.Value <> "" Then
            jibanRec.HosyouKikan = CInt(Me.HiddenHosyouKikan.Value)
        End If
        '工事会社請求有無
        If Me.HiddenKjGaisyaSeikyuuUmu.Value <> "" Then
            jibanRec.KojGaisyaSeikyuuUmu = CInt(Me.HiddenKjGaisyaSeikyuuUmu.Value)
        End If

        '調査概要の自動設定(調査方法が自動設定される場合でかつ、調査概要が未設定の場合)
        If blnTysGaiyou Then
            With jibanRec
                '未設定の場合
                If Me.SelectSITysGaiyou.SelectedValue = "9" Then
                    '設定・取得用 商品価格設定レコード
                    Dim recKakakuSettei As New KakakuSetteiRecord

                    '商品区分
                    cbLogic.SetDisplayString(.SyouhinKbn, recKakakuSettei.SyouhinKbn)
                    '調査方法
                    cbLogic.SetDisplayString(.TysHouhou, recKakakuSettei.TyousaHouhouNo)
                    '商品コード
                    cbLogic.SetDisplayString(.SyouhinCd1, recKakakuSettei.SyouhinCd)

                    '商品価格設定マスタから値の取得
                    JLogic.GetTysGaiyou(recKakakuSettei)

                    '調査概要の設定
                    .TysGaiyou = cl.GetDispNum(recKakakuSettei.TyousaGaiyou, Integer.MinValue)
                End If
            End With
        End If

        Return True
    End Function

    ''' <summary>
    ''' 画面からの物件履歴情報を取得し、リストとして返却する
    ''' </summary>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <param name="listBr">物件履歴レコードのリスト(画面の情報)</param>
    ''' <remarks></remarks>
    Private Sub SetBukkenRirekiInfo(ByVal sender As Object, ByVal jibanRec As JibanRecordBase, ByRef listBr As List(Of BukkenRirekiRecord))
        Dim brRec As New BukkenRirekiRecord
        Dim intCnt As Integer = 0

        '画面情報を取得し、物件履歴データをセットする
        brRec = Me.MakeBukkenRirekiRec(jibanRec, emBrCtrl.intCtrl1)
        If Not brRec Is Nothing Then
            listBr.Add(brRec)
        End If

        brRec = Me.MakeBukkenRirekiRec(jibanRec, emBrCtrl.intCtrl2)
        If Not brRec Is Nothing Then
            listBr.Add(brRec)
        End If

        brRec = Me.MakeBukkenRirekiRec(jibanRec, emBrCtrl.intCtrl3)
        If Not brRec Is Nothing Then
            listBr.Add(brRec)
        End If

    End Sub


#Region "特別対応(物件履歴情報)"
    ''' <summary>
    ''' 物件履歴データを作成する[申込入力画面]
    ''' </summary>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <param name="emBrCtrl">物件履歴コントロールタイプ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MakeBukkenRirekiRec( _
                                        ByVal jibanRec As JibanRecordBase _
                                        , ByVal emBrCtrl As emBrCtrl _
                                        ) As BukkenRirekiRecord

        '入力チェック
        Select Case emBrCtrl
            Case MousikomiInput.emBrCtrl.intCtrl1
                '種別および分類
                If Me.SelectBRSyubetu1.SelectedValue <> String.Empty And Me.HiddenBRBunrui1.Value <> String.Empty Then
                Else
                    Return Nothing
                End If

            Case MousikomiInput.emBrCtrl.intCtrl2
                '種別および分類
                If Me.SelectBRSyubetu2.SelectedValue <> String.Empty And Me.HiddenBRBunrui2.Value <> String.Empty Then
                Else
                    Return Nothing
                End If

            Case MousikomiInput.emBrCtrl.intCtrl3
                '種別および分類
                If Me.SelectBRSyubetu3.SelectedValue <> String.Empty And Me.HiddenBRBunrui3.Value <> String.Empty Then
                Else
                    Return Nothing
                End If

            Case Else
                Return Nothing
        End Select

        '●以下、レコードを生成
        '物件履歴登録用レコード
        Dim record As New BukkenRirekiRecord

        '区分
        record.Kbn = jibanRec.Kbn
        '保証書NO
        record.HosyousyoNo = jibanRec.HosyousyoNo

        Select Case emBrCtrl
            Case MousikomiInput.emBrCtrl.intCtrl1
                '履歴種別
                cl.SetDisplayString(Me.SelectBRSyubetu1.SelectedValue, record.RirekiSyubetu)
                '履歴NO
                cl.SetDisplayString(Me.HiddenBRBunrui1.Value, record.RirekiNo)
                '内容
                record.Naiyou = Me.TextAreaBRNaiyou1.Value
                '汎用日付
                cl.SetDisplayString(Me.TextBRHizuke1.Text, record.HanyouDate)
                '汎用コード
                cl.SetDisplayString(Me.TextBRHanyouCd1.Text, record.HanyouCd)

            Case MousikomiInput.emBrCtrl.intCtrl2
                '履歴種別
                cl.SetDisplayString(Me.SelectBRSyubetu2.SelectedValue, record.RirekiSyubetu)
                '履歴NO
                cl.SetDisplayString(Me.HiddenBRBunrui2.Value, record.RirekiNo)
                '内容
                record.Naiyou = Me.TextAreaBRNaiyou2.Value
                '汎用日付
                cl.SetDisplayString(Me.TextBRHizuke2.Text, record.HanyouDate)
                '汎用コード
                cl.SetDisplayString(Me.TextBRHanyouCd2.Text, record.HanyouCd)

            Case MousikomiInput.emBrCtrl.intCtrl3
                '履歴種別
                cl.SetDisplayString(Me.SelectBRSyubetu3.SelectedValue, record.RirekiSyubetu)
                '履歴NO
                cl.SetDisplayString(Me.HiddenBRBunrui3.Value, record.RirekiNo)
                '内容
                record.Naiyou = Me.TextAreaBRNaiyou3.Value
                '汎用日付
                cl.SetDisplayString(Me.TextBRHizuke3.Text, record.HanyouDate)
                '汎用コード
                cl.SetDisplayString(Me.TextBRHanyouCd3.Text, record.HanyouCd)

        End Select

        '入力NO(Logic側で採番)
        record.NyuuryokuNo = Integer.MinValue
        '管理日付
        cl.SetDisplayString(String.Empty, record.KanriDate)
        '管理コード
        cl.SetDisplayString(String.Empty, record.KanriCd)
        '変更可否フラグ
        record.HenkouKahiFlg = 0
        '取消
        record.Torikesi = 0
        '登録(更新)ログインユーザID
        record.UpdLoginUserId = jibanRec.UpdLoginUserId
        '登録(更新)日時
        record.UpdDatetime = jibanRec.UpdDatetime

        Return record
    End Function

#End Region

#End Region

#Region "商品１、２の自動設定"

    ''' <summary>
    ''' 商品１、２の自動設定のチェックとセットを行なう。
    ''' ※正常時は該当商品を該当邸別請求レコードにセット
    ''' ※エラー時は処理中断
    ''' </summary>
    ''' <param name="jibanRec"></param>
    ''' <returns>True or False</returns>
    ''' <remarks></remarks>
    Public Function ChkSyouhin12AutoSetting(ByVal sender As Object, ByRef jibanRec As JibanRecordMousikomiInput) As Boolean

        'エラーメッセージ初期化
        Dim tmpScript As String = ""
        Dim strCrlf As String = "\r\n"
        Dim strErrMsg As String = ""

        '商品コード１の設定実行
        If JLogic.Syouhin1Set(sender, jibanRec) = False Then

        Else '商品1セットOK

            '商品１承諾書金額の設定
            JLogic.Syouhin1SyoudakuGakuSet(sender, jibanRec)

            '商品２レコードオブジェクトの生成
            JLogic.CreateSyouhin23Rec(sender, jibanRec)

            '特定加盟店の商品ｺｰﾄﾞ2自動設定
            JLogic.TokuteitenSyouhin2Set(sender, jibanRec)

            '商品ｺｰﾄﾞ2[多棟数割引]の自動設定
            JLogic.TatouwariSet(sender, jibanRec)

            '請求書発行日、売上年月日の設定実行
            JLogic.Syouhin1UriageSeikyuDateSet(sender, jibanRec, False)

            '商品２レコードオブジェクトの破棄
            JLogic.DeleteSyouhin23Rec(sender, jibanRec)

        End If

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If jibanRec.SyouhinKkk_ErrMsg <> "" Then
            'エラーメッセージ表示
            tmpScript = "alert('" & jibanRec.SyouhinKkk_ErrMsg & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ChkSyouhin12AutoSetting", tmpScript, True)
            Return False
        End If

        Return True
    End Function

#End Region

End Class