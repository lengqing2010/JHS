Partial Public Class Houkokusyo
    Inherits System.Web.UI.Page

    Dim iraiSession As New IraiSession
    Dim userInfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    ''' <summary>
    ''' メッセージクラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic
    Dim JibanLogic As New JibanLogic
    Dim kameitenlogic As New KameitenSearchLogic
    Dim MyLogic As New HoukokusyoLogic
    Dim kisoSiyouLogic As New KisoSiyouLogic
    Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
    Dim cbLogic As New CommonBizLogic
    Dim strLogic As New StringLogic

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
        If userInfo Is Nothing Then
            'ログイン情報が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If Context.Items("irai") IsNot Nothing Then
            iraiSession = Context.Items("irai")
        End If

        '各テーブルの表示状態を切り替える
        Me.TBodyHoukokushoInfo.Style("display") = Me.HiddenHoukokusyoInfoStyle.Value

        If IsPostBack = False Then '初期起動時

            ' Key情報を保持
            _kbn = Request("sendPage_kubun")
            _no = Request("sendPage_hosyoushoNo")

            ' パラメータ不足時は画面を表示しない
            If _kbn Is Nothing Or _
               _no Is Nothing Then
                Response.Redirect(UrlConst.MAIN)
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定(調査報告書情報内)
            '****************************************************************************
            Dim helper As New DropDownHelper

            ' 受理コンボにデータをバインドする
            helper.SetDropDownList(SelectJuri, DropDownHelper.DropDownType.HkksJuri)

            ' 写真受理コンボにデータをバインドする
            helper.SetMeisyouDropDownList(Me.SelectSyasinJuri, EarthConst.emMeisyouType.SYASIN_JURI)

            ' 解析担当者コンボにデータをバインドする
            helper.SetDropDownList(SelectKaisekiTantousya, DropDownHelper.DropDownType.Tantousya)

            ' 基礎仕様接続詞コンボにデータをバインドする
            helper.SetDropDownList(SelectHanteiSetuzokuMoji, DropDownHelper.DropDownType.KsSiyouSetuzokusi)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            SetDispAction()

            'ボタン押下イベントの設定
            setBtnEvent()

            '****************************************************************************
            ' 地盤データ取得
            '****************************************************************************
            Dim jibanRec As New JibanRecordBase
            jibanRec = JibanLogic.GetJibanData(pStrKbn, pStrBangou)
            iraiSession.JibanData = jibanRec

            '地盤読み込みデータがセッションに存在する場合、画面に表示させる
            If iraiSession.JibanData IsNot Nothing Then
                Dim jr As JibanRecordBase = iraiSession.JibanData
                SetCtrlFromJibanRec(sender, e, jr) '地盤データをコントロールにセット

            End If

            If ButtonTouroku1.Disabled = False Then
                ButtonTouroku1.Focus() '登録/修正ボタンにフォーカス
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
        Dim strUriageZumi As String = String.Empty  '売上処理済み判断フラグ用
        Dim strViewMode As String = String.Empty

        '売上処理済判断フラグの取得
        strUriageZumi = cl.GetUriageSyoriZumiFlg(Me.SpanUriageSyorizumi.InnerHtml)
        strViewMode = cl.GetViewMode(strUriageZumi, userInfo.KeiriGyoumuKengen)

        If Me.SelectSeikyuuUmu.Style("display") = "none" Then
            strViewMode = EarthConst.MODE_VIEW
        End If

        '請求先/仕入先情報のセット
        Me.ucSeikyuuSiireLink.SetVariableValueCtrlFromParent(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                                    , Me.TextItemCd.Text _
                                                                    , Me.HiddenDefaultSiireSakiCdForLink.Value _
                                                                    , strUriageZumi _
                                                                    , _
                                                                    , _
                                                                    , _
                                                                    , strViewMode)


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
        'イベントハンドラ登録
        Dim tmpTouroku As String = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this);}else{return false;}"
        Dim tmpScript As String = "if(CheckTouroku()){" & tmpTouroku & "}else{return false;}"

        ButtonTouroku1.Attributes("onclick") = tmpScript
        ButtonTouroku2.Attributes("onclick") = tmpScript

    End Sub

    ''' <summary>
    ''' 検索ボタンの表示切替
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetButtonSearchDisp()

        ButtonHantei1.Style("display") = "none"
        ButtonHantei2.Style("display") = "none"

    End Sub

    ''' <summary>
    ''' 業務共通[ユーザーコントロール]ロード時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ucGyoumuKyoutuu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ucGyoumuKyoutuu.Load

        '最終更新者、最終更新日時をセット
        TextSaisyuuKousinSya.Text = ucGyoumuKyoutuu.AccLastupdateusernm.Value
        TextSaisyuuKousinDate.Text = ucGyoumuKyoutuu.AccLastupdatedatetime.Value

        '****************************
        '* 活性化制御
        '****************************

        '初期起動時のみ
        If IsPostBack = False Then
            '画面制御
            SetEnableControl()
        End If

        '報告書業務権限あるいは結果業務権限
        If userInfo.HoukokusyoGyoumuKengen = 0 And userInfo.KekkaGyoumuKengen = 0 Then
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable
            htNotTarget.Add(ucGyoumuKyoutuu.AccDataHakiSyubetu.ID, ucGyoumuKyoutuu.AccDataHakiSyubetu) 'データ破棄種別
            htNotTarget.Add(ucGyoumuKyoutuu.AccDataHakiDate.ID, ucGyoumuKyoutuu.AccDataHakiDate) 'データ破棄日
            jSM.Hash2Ctrl(UpdatePanelHoll, EarthConst.MODE_VIEW, ht, htNotTarget)

            SetButtonSearchDisp()

            '登録ボタン
            ButtonTouroku1.Disabled = True
            ButtonTouroku2.Disabled = True
        End If

        '破棄権限
        If userInfo.DataHakiKengen = 0 Then
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable
            jSM.Hash2Ctrl(ucGyoumuKyoutuu.AccTdDataHakiSyubetu, EarthConst.MODE_VIEW, ht) 'データ破棄種別
            jSM.Hash2Ctrl(ucGyoumuKyoutuu.AccTdDataHakiData, EarthConst.MODE_VIEW, ht) 'データ破棄日

        End If

        '共通情報の入力チェック
        If ucGyoumuKyoutuu.AccKameitenCd.Value = "" Then '加盟店コード
            Dim tmpScript As String = ""

            '登録ボタンの非活性化
            ButtonTouroku1.Disabled = True
            ButtonTouroku2.Disabled = True

            tmpScript = "alert('" & Messages.MSG065W & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ucGyoumuKyoutuu_Load", tmpScript, True)

            '地盤画面共通クラス
            Dim noTarget As New Hashtable
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable

            '全てのコントロールを無効化
            jSM.Hash2Ctrl(UpdatePanelHoll, EarthConst.MODE_VIEW, ht, htNotTarget)
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

    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByRef jr As JibanRecordBase)
        Dim jSM As New JibanSessionManager 'セッション管理クラス
        Dim jBn As New Jiban '地盤画面クラス
        Dim blnTorikesi As Boolean = False '取消フラグ(=False)

        ' コンボ設定ヘルパークラスを生成
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        '******************************************
        '* 画面コントロールに設定【調査報告書情報】
        '******************************************
        '加盟店コード
        Me.HiddenKameitenCd.Value = cl.GetDisplayString(jr.KameitenCd)

        TextIraiTantousya.Text = cl.GetDispStr(jr.IraiTantousyaMei) '依頼担当者
        If jr.TysKaisyaCd <> "" And jr.TysKaisyaJigyousyoCd <> "" Then
            TextTyousaKaisyaMei.Text = tyousakaisyaSearchLogic.GetTyousaKaisyaMei(jr.TysKaisyaCd, jr.TysKaisyaJigyousyoCd, False) '調査会社
            HiddenDefaultSiireSakiCdForLink.Value = (jr.TysKaisyaCd + jr.TysKaisyaJigyousyoCd)
        End If

        '●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.TyousaHouhou, True, False)
        '調査方法のDDL表示処理
        cl.ps_SetSelectTextBoxTysHouhou(jr.TysHouhou, objDrpTmp, True, Me.TextTyousaHouhou)

        TextTyousaJissiDate.Text = cl.GetDispStr(jr.TysJissiDate) '調査実施日
        TextKeikakusyoSakuseiDate.Text = cl.GetDispStr(jr.KeikakusyoSakuseiDate) '計画書作成日
        SelectJuri.SelectedValue = cl.GetDispNum(jr.TysHkksUmu, "") '受理
        TextJuriSyousai.Text = cl.GetDispStr(jr.TysHkksJyuriSyousai) '受理詳細
        TextJuriDate.Text = cl.GetDispStr(jr.TysHkksJyuriDate) '受理日
        TextHassouDate.Text = cl.GetDispStr(jr.TysHkksHakDate) '発送日
        SelectSyasinJuri.SelectedValue = cl.ChgStrToInt(jr.TikanKoujiSyasinJuri) '置換工事写真受理
        TextSyasinComment.Text = cl.GetDispStr(jr.TikanKoujiSyasinComment) '置換工事写真コメント
        TextSaihakkouDate.Text = cl.GetDispStr(jr.TysHkksSaihakDate) '再発行日

        '調査報告書情報がある場合
        If Not jr.TyousaHoukokusyoRecord Is Nothing Then

            '邸別請求情報をコントロールにセット
            SetCtrlTeibetuSeikyuuData(jr.TyousaHoukokusyoRecord)

            '邸別入金情報をコントロールにセット
            If jr.TeibetuNyuukinRecords IsNot Nothing Then
                ' 入金額/残額をセット
                CalcZangaku(jr.getZeikomiGaku(New String() {"150"}), jr.getNyuukinGaku("150"))
            Else
                ' 入金額/残額をセット
                SetKingaku(True)
            End If

        End If

        '【判定結果】
        '存在チェック
        If ChkTantousya(SelectKaisekiTantousya, cl.GetDispNum(jr.TantousyaCd)) Then
            TextKaisekiTantousyaCd.Text = cl.GetDispNum(jr.TantousyaCd, "") '解析担当者コード
            SelectKaisekiTantousya.SelectedValue = TextKaisekiTantousyaCd.Text '解析担当者
        ElseIf jr.TantousyaCd > 0 Then
            TextKaisekiTantousyaCd.Text = cl.GetDispNum(jr.TantousyaCd, "") '解析担当者コード
            SelectKaisekiTantousya.Items.Add(New ListItem(TextKaisekiTantousyaCd.Text & ":" & jr.TantousyaMei, TextKaisekiTantousyaCd.Text)) '解析担当者
            SelectKaisekiTantousya.SelectedValue = TextKaisekiTantousyaCd.Text  '選択状態
        End If
        TextKaisekiSyouninsya.Text = cl.GetDispStr(jr.TyousaHoukokusyoSyouninsyaIf) '解析承認者
        TextKoujiTantousyaCd.Text = cl.GetDispNum(jr.SyouninsyaCd, "") '工事担当者コード
        TextKoujiTantousya.Text = cl.GetDispStr(jr.SyouninsyaMei) '工事担当者
        TextHantei1Cd.Text = cl.GetDispNum(jr.HanteiCd1, "") '判定１コード
        SpanHantei1.InnerHtml = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(jr.HanteiCd1)) '判定１名
        SelectHanteiSetuzokuMoji.SelectedValue = jr.HanteiSetuzokuMoji '判定接続文字
        TextHantei2Cd.Text = cl.GetDispNum(jr.HanteiCd2, "") '判定２コード
        SpanHantei2.InnerHtml = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(jr.HanteiCd2)) '判定２名

        '****************************
        '* Hidden項目
        '****************************
        HiddenHosyousyoHakJykyOld.Value = cl.GetDispNum(jr.HosyousyoHakJyky, "") '保証書発行状況Old

        HiddenHosyousyoHakJyky.Value = cl.GetDispNum(jr.HosyousyoHakJyky, "") '保証書発行状況(DB更新用)
        HiddenHosyousyoHakJykySetteiDate.Value = cl.GetDispStr(jr.HosyousyoHakJykySetteiDate) '保証書発行状況設定日(DB更新用)

        HiddenHosyousyoHakkouDate.Value = cl.GetDispStr(jr.HosyousyoHakDate) '保証書発行日(入力チェック用)

        HiddenHantei1CdOld.Value = cl.GetDispNum(jr.HanteiCd1, "") '判定1Old
        HiddenHantei2CdOld.Value = cl.GetDispNum(jr.HanteiCd2, "") '判定2Old
        HiddenHanteiSetuzokuMojiOld.Value = cl.GetDispNum(jr.HanteiSetuzokuMoji, "") '判定接続文字Old
        HiddenHantei1Old.Value = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(jr.HanteiCd1)) '判定１名
        HiddenHantei1CdMae.Value = cl.GetDispNum(jr.HanteiCd1, "") '判定1前
        HiddenHantei2CdMae.Value = cl.GetDispNum(jr.HanteiCd2, "") '判定2前

        HiddenJituseikyuu1Flg.Value = "" '実請求税抜金額・自動設定用フラグ

        HiddenTyousaKekkaTourokuDate.Value = IIf(jr.TysKekkaAddDatetime = DateTime.MinValue, "", Format(jr.TysKekkaAddDatetime, EarthConst.FORMAT_DATE_TIME_1)) '調査結果登録日時
        HiddenTyousaKekkaUpdateDate.Value = IIf(jr.TysKekkaUpdDatetime = DateTime.MinValue, "", Format(jr.TysKekkaUpdDatetime, EarthConst.FORMAT_DATE_TIME_1)) '調査結果更新日時

        Me.HiddenKojHanteiKekkaFlgOld.Value = cl.GetDispNum(jr.KojHanteiKekkaFlg, "0") '工事判定結果FLGOLD

        '****************************
        '* セッションに画面情報を格納
        '****************************
        jSM.Ctrl2Hash(Me, jBn.IraiData)
        iraiSession.Irai1Data = jBn.IraiData

    End Sub

#Region "入金額/残額"

    ''' <summary>
    ''' 入金額・残額を表示します
    ''' </summary>
    ''' <param name="uriageGoukeiGaku">税込売上金額合計</param>
    ''' <param name="nyuukinGaku">入金額</param>
    ''' <remarks></remarks>
    Public Sub CalcZangaku(ByVal uriageGoukeiGaku As Integer, _
                           ByVal nyuukinGaku As Integer)

        ' NULLは０にする
        uriageGoukeiGaku = IIf(uriageGoukeiGaku = Integer.MinValue, 0, uriageGoukeiGaku)
        nyuukinGaku = IIf(nyuukinGaku = Integer.MinValue, 0, nyuukinGaku)

        ' 入金額
        TextNyuukingaku.Text = nyuukinGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        ' 残額
        TextZangaku.Text = (uriageGoukeiGaku - nyuukinGaku).ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub

    ''' <summary>
    ''' 入金額・残額を表示します<br/>
    ''' 税込売上金額合計のみ変更し、再計算します
    ''' </summary>
    ''' <param name="strUriageGoukeiGaku">税込売上金額合計</param>
    ''' <remarks></remarks>
    Public Sub CalcZangaku(ByVal strUriageGoukeiGaku As String)

        If strUriageGoukeiGaku = "" Then
            ' 残額
            TextZangaku.Text = "0"

        Else
            Dim uriageGoukeiGaku As Integer = Integer.MinValue

            uriageGoukeiGaku = CInt(strUriageGoukeiGaku)

            Dim strNyuukinGaku As String = IIf(TextNyuukingaku.Text.Replace(",", "").Trim() = "", _
                                            "0", _
                                            TextNyuukingaku.Text.Replace(",", "").Trim())

            Dim nyuukinGaku As Integer = Integer.Parse(strNyuukinGaku)

            ' NULLは０にする
            uriageGoukeiGaku = IIf(uriageGoukeiGaku = Integer.MinValue, 0, uriageGoukeiGaku)

            ' 残額
            TextZangaku.Text = (uriageGoukeiGaku - nyuukinGaku).ToString(EarthConst.FORMAT_KINGAKU_1)

        End If

    End Sub

#End Region

#Region "邸別請求レコード編集"
    ''' <summary>
    ''' 邸別請求レコードデータをコントロールにセットします
    ''' </summary>
    ''' <param name="TeibetuRec"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlTeibetuSeikyuuData(ByVal TeibetuRec As TeibetuSeikyuuRecord)

        ' 商品コード（Hidden）
        TextItemCd.Text = TeibetuRec.SyouhinCd
        '商品名
        SpanItemMei.InnerHtml = TeibetuRec.SyouhinMei
        '工務店請求税抜金額
        TextKoumutenSeikyuuKingaku.Text = IIf(TeibetuRec.KoumutenSeikyuuGaku = Integer.MinValue, _
                                            0, _
                                            TeibetuRec.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' 税率（Hidden）
        HiddenZeiritu.Value = TeibetuRec.Zeiritu
        ' 税区分（Hidden）
        HiddenZeiKbn.Value = IIf(TeibetuRec.ZeiKbn = "", "", TeibetuRec.ZeiKbn)
        If TeibetuRec.Zeiritu = 0 And TeibetuRec.ZeiKbn = "" Then
            SetZeiInfo(TeibetuRec.SyouhinCd)
        End If

        '*****************
        '* 【売上金額】
        '*****************
        '●売上消費税額(消費税)
        TextSyouhizei.Text = IIf(TeibetuRec.UriageSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.UriageSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税抜売上金額(実請求税抜金額)
        TextJituseikyuuKingaku.Text = IIf(TeibetuRec.UriGaku = Integer.MinValue, 0, Format(TeibetuRec.UriGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税込売上金額(税込額)
        TextZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiUriGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiUriGaku, EarthConst.FORMAT_KINGAKU_1))

        '*****************
        '* 【仕入金額】
        '*****************
        '仕入金額
        Me.HiddenSiireGaku.Value = IIf(TeibetuRec.SiireGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireGaku, EarthConst.FORMAT_KINGAKU_1))
        '●仕入消費税額
        Me.HiddenSiireSyouhiZei.Value = IIf(TeibetuRec.SiireSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))

        ' 請求書発行日
        TextSeikyuusyoHakkouDate.Text = IIf(TeibetuRec.SeikyuusyoHakDate = Date.MinValue, _
                                          "", _
                                          TeibetuRec.SeikyuusyoHakDate.ToString(EarthConst.FORMAT_DATE_TIME_9))
        ' 売上年月日
        TextUriageNengappi.Text = IIf(TeibetuRec.UriDate = Date.MinValue, _
                                      "", _
                                      TeibetuRec.UriDate.ToString(EarthConst.FORMAT_DATE_TIME_9))
        ' 請求有無ドロップダウン
        SelectSeikyuuUmu.SelectedValue = IIf(TeibetuRec.SeikyuuUmu = 1, "1", "0")
        ' 売上処理済
        SpanUriageSyorizumi.InnerHtml = IIf(TeibetuRec.UriKeijyouDate = Date.MinValue, "", EarthConst.URIAGE_ZUMI)
        ' 売上計上日
        Me.HiddenUriageKeijyouDate.Value = cl.GetDisplayString(TeibetuRec.UriKeijyouDate)
        ' 発注書金額
        TextHattyuusyoKingaku.Text = IIf(TeibetuRec.HattyuusyoGaku = Integer.MinValue, _
                                         "", _
                                         TeibetuRec.HattyuusyoGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' 発注書確定
        TextHattyuusyoKakutei.Text = IIf(TeibetuRec.HattyuusyoKakuteiFlg = 1, EarthConst.KAKUTEI, EarthConst.MIKAKUTEI)
        ' 発注書確認日
        TextHattyuusyoKakuninDate.Text = IIf(TeibetuRec.HattyuusyoKakuninDate = Date.MinValue, _
                                           "", _
                                           TeibetuRec.HattyuusyoKakuninDate.ToString(EarthConst.FORMAT_DATE_TIME_9))
        '再発行理由
        TextSaihakkouRiyuu.Text = TeibetuRec.Bikou

        '請求先/仕入先リンクへセット
        Me.ucSeikyuuSiireLink.SetSeikyuuSiireLinkFromTeibetuRec(TeibetuRec)

        '請求先タイプの取得設定
        Me.TextSeikyuusaki.Text = cl.GetSeikyuuSakiTypeStr( _
                                                        TeibetuRec.SyouhinCd _
                                                        , EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo _
                                                        , Me.HiddenKameitenCd.Value _
                                                        , TeibetuRec.SeikyuuSakiCd _
                                                        , TeibetuRec.SeikyuuSakiBrc _
                                                        , TeibetuRec.SeikyuuSakiKbn)

        '更新日時(排他制御用)
        cl.setDispHaitaUpdTime(TeibetuRec.AddDatetime, TeibetuRec.UpdDatetime, Me.HiddenUpdateTeibetuSeikyuuDatetime)

    End Sub

    ''' <summary>
    ''' 邸別請求レコードデータにコントロールの内容をセットします
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSeikyuuCtrlData() As TeibetuSeikyuuRecord

        ' 商品コード未設定時、何もセットしない
        If TextItemCd.Text = "" Then
            Return Nothing
        End If

        ' 邸別請求レコード
        Dim record As New TeibetuSeikyuuRecord

        ' 区分
        record.Kbn = ucGyoumuKyoutuu.Kubun
        ' 保証書NO
        record.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        '分類コード(150)
        record.BunruiCd = EarthConst.SOUKO_CD_TYOUSA_HOUKOKUSYO
        '画面表示NO
        record.GamenHyoujiNo = 1
        ' 商品コード
        record.SyouhinCd = TextItemCd.Text
        '売上金額
        cl.SetDisplayString(TextJituseikyuuKingaku.Text, record.UriGaku)
        '仕入金額
        cl.SetDisplayString(Me.HiddenSiireGaku.Value, record.SiireGaku)
        ' 税区分
        record.ZeiKbn = HiddenZeiKbn.Value
        ' 税率
        cl.SetDisplayString(HiddenZeiritu.Value, record.Zeiritu)
        ' 消費税額
        cl.SetDisplayString(TextSyouhizei.Text, record.UriageSyouhiZeiGaku)
        ' 仕入消費税額
        cl.SetDisplayString(Me.HiddenSiireSyouhiZei.Value, record.SiireSyouhiZeiGaku)
        ' 請求書発行日
        cl.SetDisplayString(TextSeikyuusyoHakkouDate.Text, record.SeikyuusyoHakDate)
        ' 売上年月日
        cl.SetDisplayString(TextUriageNengappi.Text, record.UriDate)
        ' 伝票売上年月日(ロジッククラスで自動セット)
        record.DenpyouUriDate = DateTime.MinValue
        ' 請求有無
        record.SeikyuuUmu = IIf(SelectSeikyuuUmu.SelectedValue = "1", 1, 0)
        '売上計上FLG▲
        record.UriKeijyouFlg = 0
        '売上計上日▲
        record.UriKeijyouDate = DateTime.MinValue
        '確定区分
        record.KakuteiKbn = Integer.MinValue
        '備考
        record.Bikou = TextSaihakkouRiyuu.Text
        '工務店請求税抜金額
        cl.SetDisplayString(TextKoumutenSeikyuuKingaku.Text, record.KoumutenSeikyuuGaku)
        ' 発注書金額
        cl.SetDisplayString(TextHattyuusyoKingaku.Text, record.HattyuusyoGaku)
        ' 発注書確認日
        cl.SetDisplayString(TextHattyuusyoKakuninDate.Text, record.HattyuusyoKakuninDate)
        '一括入金FLG▲
        record.IkkatuNyuukinFlg = Integer.MinValue
        '調査見積書作成日
        record.TysMitsyoSakuseiDate = DateTime.MinValue
        ' 発注書確定FLG
        record.HattyuusyoKakuteiFlg = IIf(TextHattyuusyoKakutei.Text = EarthConst.KAKUTEI, 1, 0)

        '請求先/仕入先リンクから取得
        Me.ucSeikyuuSiireLink.SetTeibetuRecFromSeikyuuSiireLink(record)

        '更新ログインユーザID
        record.UpdLoginUserId = userInfo.LoginUserId
        ' 更新日時 読み込み時のタイムスタンプ
        record.UpdDatetime = cl.getDispHaitaUpdTime(Me.HiddenUpdateTeibetuSeikyuuDatetime)

        Return record
    End Function

#End Region

    ''' <summary>
    ''' 登録/修正 実行ボタン１,２押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTouroku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTouroku1.ServerClick _
                                                                                                                , ButtonTouroku2.ServerClick
        Dim tmpScript As String = ""

        '判定変更理由チェック
        If Me.checkInputBukkenRireki(Me.HiddenHanteiHenkouRiyuu.Value, False) = False Then Exit Sub

        '共通情報
        If ucGyoumuKyoutuu.checkInput() = False Then Exit Sub

        '調査報告書情報
        If checkInput() = False Then Exit Sub

        ' 画面の内容をDBに反映する
        If SaveData() Then '登録成功
            tmpScript = "window.close();" '画面を閉じる
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonTouroku_ServerClick1", tmpScript, True)

        Else '登録失敗
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "登録/修正") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonTouroku_ServerClick2", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' 判定変更理由チェック
    ''' </summary>
    ''' <param name="strHenkouRiyuu">判定変更理由</param>
    ''' <param name="blnNaiyou">True:「内容」チェック,False:「判定変更理由」チェック</param>
    ''' <returns></returns>
    ''' <remarks>判定関連が変更されている場合、チェックを行なう</remarks>
    Public Function checkInputBukkenRireki(ByRef strHenkouRiyuu As String, ByVal blnNaiyou As Boolean) As Boolean
        Dim jBn As New Jiban '地盤画面クラス
        Dim strHanteiHenkouRiyuu As String = String.Empty
        Dim errMess As String = String.Empty
        Dim strCtrlName As String = "判定変更理由"
        Dim blnHanteiFlg As Boolean = True

        '判定1=入力済でかつ判定関連が変更されている場合、以下のチェックを行なう
        '判定1
        If Me.HiddenHantei1CdOld.Value <> String.Empty Then '入力済
            '判定1
            If Me.HiddenHantei1CdOld.Value <> Me.TextHantei1Cd.Text Then
                blnHanteiFlg = False
            End If
            '判定2
            If Me.HiddenHantei2CdOld.Value <> Me.TextHantei2Cd.Text Then
                blnHanteiFlg = False
            End If
            '判定接続詞
            If Me.HiddenHanteiSetuzokuMojiOld.Value <> Me.SelectHanteiSetuzokuMoji.SelectedValue Then
                blnHanteiFlg = False
            End If
        End If

        '判定関連が変更されている場合、以下のチェックを行なう。
        If blnHanteiFlg = False Then
            ' 登録前チェック
            strHenkouRiyuu = strHenkouRiyuu.Trim '空白除去

            '●必須チェック
            If strHenkouRiyuu = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", strCtrlName)
            End If
            If blnNaiyou Then '内容チェック時
                '禁則文字を置換
                strLogic.KinsiStrClear(strHenkouRiyuu)
            End If
            '●禁則文字チェック(文字列入力フィールドが対象)
            If jBn.KinsiStrCheck(strHenkouRiyuu) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", strCtrlName)
            End If
            '改行変換(物件履歴T.内容)
            If strHenkouRiyuu <> "" Then
                strHenkouRiyuu = strHenkouRiyuu.Replace(vbCrLf, " ")
            End If
            '●バイト数チェック(文字列入力フィールドが対象)
            If blnNaiyou Then '内容チェック時
                If jBn.ByteCheckSJIS(strHenkouRiyuu, 512) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", strCtrlName)
                End If
            Else '判定変更理由チェック時
                If jBn.ByteCheckSJIS(strHenkouRiyuu, 256) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", strCtrlName)
                End If
            End If

            'エラー発生時処理
            If errMess <> "" Then
                'Me.HiddenHanteiHenkouRiyuu.Value = "" '初期化
                strHenkouRiyuu = String.Empty

                'エラーメッセージ表示
                MLogic.AlertMessage(Me, errMess)
                'フォーカスセット
                setFocusAJ(Me.ButtonTouroku1)
                Return False
                Exit Function
                'Else
                '    Me.HiddenHanteiHenkouRiyuu.Value = strHenkouRiyuu
            End If
        End If

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
    Public Function checkInput() As Boolean
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

            '●コード入力値変更チェック
            '判定1コード
            If TextHantei1Cd.Text <> HiddenHantei1CdMae.Value Or (TextHantei1Cd.Text <> "" And SpanHantei1.InnerHtml = "") Then
                errMess += Messages.MSG030E.Replace("@PARAM1", "判定1コード")
                arrFocusTargetCtrl.Add(ButtonHantei1)
            End If
            '判定2コード
            If TextHantei2Cd.Text <> HiddenHantei2CdMae.Value Or (TextHantei2Cd.Text <> "" And SpanHantei2.InnerHtml = "") Then
                errMess += Messages.MSG030E.Replace("@PARAM1", "判定2コード")
                arrFocusTargetCtrl.Add(ButtonHantei2)
            End If

            '●必須チェック
            '<調査報告書再発行>

            '(Chk20:<報告書画面>調査報告書再発行日＝入力、かつ、<報告書画面><工事報告書再発行>請求有無＝有りの場合、チェックを行う。)
            '再発行日
            If TextSaihakkouDate.Text.Length <> 0 Then
                '請求有無
                If SelectSeikyuuUmu.SelectedValue = "1" Then
                    '実請求税抜金額
                    If TextJituseikyuuKingaku.Text.Length = 0 Then
                        errMess += Messages.MSG013E.Replace("@PARAM1", "実請求税抜金額")
                        arrFocusTargetCtrl.Add(TextJituseikyuuKingaku)
                    End If
                    '請求書発行日
                    If TextSeikyuusyoHakkouDate.Text.Length = 0 Then
                        errMess += Messages.MSG013E.Replace("@PARAM1", "請求書発行日")
                        arrFocusTargetCtrl.Add(TextSeikyuusyoHakkouDate)
                    End If
                End If

                '(Chk19:<報告書画面>調査報告書再発行日＝入力の場合、チェックを行う。)
                '請求
                If SelectSeikyuuUmu.SelectedValue = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "請求")
                    arrFocusTargetCtrl.Add(SelectSeikyuuUmu)
                End If
                '再発行理由
                If TextSaihakkouRiyuu.Text.Length = 0 Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "再発行理由")
                    arrFocusTargetCtrl.Add(TextSaihakkouRiyuu)
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
        '受理日
        If TextJuriDate.Text <> "" Then
            If cl.checkDateHanni(TextJuriDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "受理日")
                arrFocusTargetCtrl.Add(TextJuriDate)
            End If
        End If
        '発送日
        If TextHassouDate.Text <> "" Then
            If cl.checkDateHanni(TextHassouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "発送日")
                arrFocusTargetCtrl.Add(TextHassouDate)
            End If
        End If
        '再発行日
        If TextSaihakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextSaihakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "再発行日")
                arrFocusTargetCtrl.Add(TextSaihakkouDate)
            End If
        End If
        '****************
        '* 調査報告書再発行
        '****************
        '請求書発行日
        If TextSeikyuusyoHakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextSeikyuusyoHakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "請求書発行日")
                arrFocusTargetCtrl.Add(TextSeikyuusyoHakkouDate)
            End If
        End If


        '●禁則文字チェック(文字列入力フィールドが対象)
        '受理詳細
        If jBn.KinsiStrCheck(TextJuriSyousai.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "受理詳細")
            arrFocusTargetCtrl.Add(TextJuriSyousai)
        End If
        '写真コメント
        If jBn.KinsiStrCheck(TextSyasinComment.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "写真コメント")
            arrFocusTargetCtrl.Add(TextSyasinComment)
        End If
        '再発行理由
        If jBn.KinsiStrCheck(TextSaihakkouRiyuu.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "再発行理由")
            arrFocusTargetCtrl.Add(TextSaihakkouRiyuu)
        End If

        '●バイト数チェック(文字列入力フィールドが対象)
        '受理詳細
        If jBn.ByteCheckSJIS(TextJuriSyousai.Text, TextJuriSyousai.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "受理詳細")
            arrFocusTargetCtrl.Add(TextJuriSyousai)
        End If
        '写真コメント
        If jBn.ByteCheckSJIS(TextSyasinComment.Text, TextSyasinComment.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "写真コメント")
            arrFocusTargetCtrl.Add(TextSyasinComment)
        End If
        '再発行理由
        If jBn.ByteCheckSJIS(TextSaihakkouRiyuu.Text, TextSaihakkouRiyuu.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "再発行理由")
            arrFocusTargetCtrl.Add(TextSaihakkouRiyuu)
        End If

        '●その他チェック
        '(Chk01:[地盤テーブル]調査実施日＝入力、判定1＝未入力の場合、エラーとする。)
        If TextTyousaJissiDate.Text <> "" And TextHantei1Cd.Text = "" Then
            errMess += Messages.MSG062E.Replace("@PARAM1", "判定コード1")
            arrFocusTargetCtrl.Add(TextHantei1Cd)
        End If

        '(Chk02:調査実施日＝未入力の時、判定1＝入力はエラー)
        If TextTyousaJissiDate.Text = "" And TextHantei1Cd.Text <> "" Then
            errMess += Messages.MSG100E.Replace("@PARAM1", "調査実施日").Replace("@PARAM2", "判定1コード")
            arrFocusTargetCtrl.Add(TextHantei1Cd)
        End If

        '(Chk03:判定1＝未入力かつ（判定2＝入力 or 接続詞＝入力）はエラー)
        If TextHantei1Cd.Text = "" Then
            If SelectHanteiSetuzokuMoji.SelectedValue <> "" Or TextHantei2Cd.Text <> "" Then
                errMess += Messages.MSG061E.Replace("@PARAM1", "判定コード1")
                arrFocusTargetCtrl.Add(TextHantei1Cd)
            End If
        End If

        '(Chk04:判定1＝入力かつ解析担当者＝未入力はエラー)
        If TextHantei1Cd.Text <> "" And SelectKaisekiTantousya.SelectedValue = "" Then
            errMess += Messages.MSG061E.Replace("@PARAM1", "解析担当者")
            arrFocusTargetCtrl.Add(TextKaisekiTantousyaCd)
        End If

        '(Chk05:判定1＝入力かつ計画書作成日＝未入力の場合、エラーにする。)
        If TextHantei1Cd.Text <> "" And TextKeikakusyoSakuseiDate.Text = "" Then
            errMess += Messages.MSG061E.Replace("@PARAM1", "計画書作成日")
            arrFocusTargetCtrl.Add(ButtonHantei1)
        End If

        '(Chk12:<保証画面>保証書発行日＝入力、かつ、判定1≠地盤テーブル（更新前）.判定1の場合、確認メッセージを表示する。
        '確認OKの場合、登録許可する。)
        '=>判定コード1変更時処理にて確認済み

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
        '*************************
        '地盤データは更新対象のみ
        '邸別請求データは全て更新
        '*************************

        Dim jrOld As New JibanRecord
        ' 現在の地盤データをDBから取得する
        jrOld = JibanLogic.GetJibanData(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou)

        ' 画面内容より地盤レコードを生成する
        Dim jibanRec As New JibanRecordHoukokusyo
        Dim brRecHantei As New BukkenRirekiRecord

        '進捗T更新用に、DB上の値をセットする
        JibanLogic.SetSintyokuJibanData(jrOld, jibanRec)

        '商品1～3のコピー
        JibanLogic.ps_CopyTeibetuSyouhinData(jrOld, jibanRec)

        '***************************************
        ' 地盤データ(共通情報)
        '***************************************
        ' 区分
        jibanRec.Kbn = ucGyoumuKyoutuu.Kubun
        ' 番号（保証書NO）
        jibanRec.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        ' データ破棄種別
        cl.SetDisplayString(ucGyoumuKyoutuu.DataHakiSyubetu, jibanRec.DataHakiSyubetu)
        ' データ破棄日
        If ucGyoumuKyoutuu.DataHakiSyubetu = "0" Then
            jibanRec.DataHakiDate = DateTime.MinValue
        Else
            cl.SetDisplayString(ucGyoumuKyoutuu.DataHakiDate, jibanRec.DataHakiDate)
        End If
        ' 施主名
        jibanRec.SesyuMei = ucGyoumuKyoutuu.SesyuMei
        ' 物件住所1
        jibanRec.BukkenJyuusyo1 = ucGyoumuKyoutuu.Jyuusyo1
        ' 物件住所2
        jibanRec.BukkenJyuusyo2 = ucGyoumuKyoutuu.Jyuusyo2
        ' 物件住所3
        jibanRec.BukkenJyuusyo3 = ucGyoumuKyoutuu.Jyuusyo3
        ' 備考
        jibanRec.Bikou = ucGyoumuKyoutuu.Bikou
        ' 備考2
        jibanRec.Bikou2 = ucGyoumuKyoutuu.Bikou2
        ' 更新者ユーザーID
        jibanRec.UpdLoginUserId = userInfo.LoginUserId
        ' 更新日時 読み込み時のタイムスタンプ
        If ucGyoumuKyoutuu.AccupdateDateTime.Value = "" Then
            jibanRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
        Else
            jibanRec.UpdDatetime = DateTime.ParseExact(ucGyoumuKyoutuu.AccupdateDateTime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
        End If
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

        '***************************************
        ' 邸別請求データ
        '***************************************
        ' 「調査報告書情報」の邸別請求データをセットします
        jibanRec.TyousaHoukokusyoRecord = GetSeikyuuCtrlData()

        '***************************************
        ' 地盤データ(調査報告書情報)
        '***************************************
        '受理
        cl.SetDisplayString(SelectJuri.SelectedValue, jibanRec.TysHkksUmu)
        '受理詳細
        jibanRec.TysHkksJyuriSyousai = TextJuriSyousai.Text
        '受理日
        cl.SetDisplayString(TextJuriDate.Text, jibanRec.TysHkksJyuriDate)
        '発送日
        cl.SetDisplayString(TextHassouDate.Text, jibanRec.TysHkksHakDate)
        '置換工事写真受理
        jibanRec.TikanKoujiSyasinJuri = SelectSyasinJuri.SelectedValue
        '置換工事写真コメント
        jibanRec.TikanKoujiSyasinComment = TextSyasinComment.Text
        '再発行日
        cl.SetDisplayString(TextSaihakkouDate.Text, jibanRec.TysHkksSaihakDate)
        '解析担当者コード
        cl.SetDisplayString(TextKaisekiTantousyaCd.Text, jibanRec.TantousyaCd)
        '判定コード1
        cl.SetDisplayString(TextHantei1Cd.Text, jibanRec.HanteiCd1)
        '判定コード2
        cl.SetDisplayString(TextHantei2Cd.Text, jibanRec.HanteiCd2)
        '判定接続文字
        cl.SetDisplayString(SelectHanteiSetuzokuMoji.SelectedValue, jibanRec.HanteiSetuzokuMoji)

        '***************************************
        ' 画面入力項目以外
        '***************************************
        cl.SetDisplayString(TextKeikakusyoSakuseiDate.Text, jibanRec.KeikakusyoSakuseiDate) '計画書作成日

        cl.SetDisplayString(HiddenHosyousyoHakJyky.Value, jibanRec.HosyousyoHakJyky) '保証書発行状況
        cl.SetDisplayString(HiddenHosyousyoHakJykySetteiDate.Value, jibanRec.HosyousyoHakJykySetteiDate) '保証書発行状況設定日

        '調査結果登録日時
        If HiddenTyousaKekkaTourokuDate.Value = "" Then '未入力
            '判定1コード
            If TextHantei1Cd.Text <> "" Then '入力
                jibanRec.TysKekkaAddDatetime = DateTime.Now
            End If
        Else '入力→DB値をそのまま更新
            jibanRec.TysKekkaAddDatetime = DateTime.ParseExact(HiddenTyousaKekkaTourokuDate.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
        End If

        '調査結果更新日時
        If HiddenTyousaKekkaTourokuDate.Value <> "" Then '入力

            '判定1コード、判定2コード、判定接続文字いずれかに変更があった場合
            If HiddenHantei1CdOld.Value <> TextHantei1Cd.Text _
                Or HiddenHantei2CdOld.Value <> TextHantei2Cd.Text _
                    Or HiddenHanteiSetuzokuMojiOld.Value <> SelectHanteiSetuzokuMoji.SelectedValue Then

                jibanRec.TysKekkaUpdDatetime = DateTime.Now 'システム日付をセット

            Else '更新をしない

                If HiddenTyousaKekkaUpdateDate.Value <> "" Then 'DBの値そのまま
                    jibanRec.TysKekkaUpdDatetime = DateTime.ParseExact(HiddenTyousaKekkaUpdateDate.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
                Else
                    jibanRec.TysKekkaUpdDatetime = DateTime.MinValue
                End If
            End If
        End If

        '更新者
        jibanRec.Kousinsya = cbLogic.GetKousinsya(userInfo.LoginUserId, DateTime.Now)

        '工事判定結果FLG
        Dim intKojHanteiKekkaFlg As Integer = Integer.MinValue
        intKojHanteiKekkaFlg = MyLogic.GetKojHanteiKekkaFlg(Me.TextHantei1Cd.Text, Me.TextHantei2Cd.Text, Me.SelectHanteiSetuzokuMoji.SelectedValue)
        cl.SetDisplayString(intKojHanteiKekkaFlg, jibanRec.KojHanteiKekkaFlg)

        '物件履歴データのセット
        brRecHantei = Me.GetBukkenCtrlData()

        If Not brRecHantei Is Nothing Then
            '判定変更理由チェック
            If Me.checkInputBukkenRireki(brRecHantei.Naiyou, True) = False Then Return False

            '履歴NOが以下でない場合、エラー
            If brRecHantei.RirekiNo = 1 Or brRecHantei.RirekiNo = 2 Or brRecHantei.RirekiNo = 3 Or brRecHantei.RirekiNo = 4 Then
            Else
                Dim strMsg As String = Messages.MSG147E.Replace("@PARAM1", "物件履歴データの自動設定")
                MLogic.AlertMessage(Me, strMsg, 0, "Err_BukkenRireki1")
                Return False
            End If
        End If

        '************************************************
        ' 保証書発行状況、保証書発行状況設定日、保証商品有無の自動設定
        '************************************************
        '商品の自動設定後に行なう
        cbLogic.ps_AutoSetHosyousyoHakJyjy(EarthEnum.emGamenInfo.Houkokusyo, jibanRec)

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
        '*********************************************************
        '===========================================================

        ' データの更新を行います
        If MyLogic.SaveJibanData(Me, jibanRec, brRec, brRecHantei) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispAction()
        Dim jBn As New Jiban '地盤画面クラス

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' プルダウン/コード入力連携設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        jBn.SetPullCdScriptSrc(TextKaisekiTantousyaCd, SelectKaisekiTantousya) '解析担当者

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
        '* 判定コード１、２およびボタン
        '*****************************
        TextHantei1Cd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callHantei1Search(this);}else{checkNumber(this);}"
        TextHantei1Cd.Attributes("onfocus") = "setTempValueForOnBlur(this);"
        TextHantei2Cd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callHantei2Search(this);}else{checkNumber(this);}"
        TextHantei2Cd.Attributes("onfocus") = "setTempValueForOnBlur(this);"

        ButtonHantei1.Attributes("onclick") = "SetChangeMaeValue('" & HiddenHantei1CdMae.ClientID & "','" & TextHantei1Cd.ClientID & "');"
        ButtonHantei2.Attributes("onclick") = "SetChangeMaeValue('" & HiddenHantei2CdMae.ClientID & "','" & TextHantei2Cd.ClientID & "');"

        '*****************************
        '* 金額系
        '*****************************
        '工務店請求税抜金額
        TextKoumutenSeikyuuKingaku.Attributes("onfocus") = onFocusPostBackScript & ";SetChangeMaeValue('" & HiddenKoumutenSeikyuuKingakuMae.ClientID & "','" & TextKoumutenSeikyuuKingaku.ClientID & "');"
        TextKoumutenSeikyuuKingaku.Attributes("onblur") = onBlurPostBackScript
        TextKoumutenSeikyuuKingaku.Attributes("onkeydown") = disabledOnkeydown
        '実請求税抜金額
        TextJituseikyuuKingaku.Attributes("onfocus") = onFocusPostBackScript & ";SetChangeMaeValue('" & HiddenJituseikyuuKingakuMae.ClientID & "','" & TextJituseikyuuKingaku.ClientID & "');"
        TextJituseikyuuKingaku.Attributes("onblur") = onBlurPostBackScript
        TextJituseikyuuKingaku.Attributes("onkeydown") = disabledOnkeydown

        '*****************************
        '* 日付系
        '*****************************
        '受理日
        TextJuriDate.Attributes("onblur") = checkDate
        TextJuriDate.Attributes("onkeydown") = disabledOnkeydown
        '発送日
        TextHassouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextHassouDate.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenHassouDateMae.ClientID & "','" & TextHassouDate.ClientID & "');" & onFocusPostBackScriptDate
        TextHassouDate.Attributes("onkeydown") = disabledOnkeydown
        '再発行日
        TextSaihakkouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextSaihakkouDate.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenSaihakkouDateMae.ClientID & "','" & TextSaihakkouDate.ClientID & "');" & onFocusPostBackScriptDate
        TextSaihakkouDate.Attributes("onkeydown") = disabledOnkeydown
        '請求書発行日
        TextSeikyuusyoHakkouDate.Attributes("onblur") = checkDate
        TextSeikyuusyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown

        '*****************************
        '* ドロップダウンリスト
        '*****************************
        '請求有無
        SelectSeikyuuUmu.Attributes("onfocus") = "checkTempValueForOnBlur(this);" & "SetChangeMaeValue('" & HiddenSeikyuuUmuMae.ClientID & "','" & SelectSeikyuuUmu.ClientID & "');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 数値系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '解析担当者
        TextKaisekiTantousyaCd.Attributes("onblur") += checkNumber
        TextKaisekiTantousyaCd.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 機能別テーブルの表示切替
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '調査報告書
        Me.AncTysHoukokusyo.HRef = "JavaScript:changeDisplay('" & Me.TBodyHoukokushoInfo.ClientID & "');SetDisplayStyle('" & Me.HiddenHoukokusyoInfoStyle.ClientID & "','" & Me.TBodyHoukokushoInfo.ClientID & "');"

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
        noTarget.Add(divTyousaHoukokusyo, True)
        noTarget.Add(ButtonTouroku1.ID, True) '登録ボタン1
        noTarget.Add(ButtonTouroku2.ID, True) '登録ボタン2

        If blnFlg Then
            '全てのコントロールを無効化
            jBn.ChangeDesabledAll(divTyousaHoukokusyo, True, noTarget)
        Else
            '全てのコントロールを有効化
            jBn.ChangeDesabledAll(divTyousaHoukokusyo, False, noTarget)
        End If

    End Sub


#Region "金額計算関連"

    ''' <summary>
    ''' 金額設定(消費税/税込金額)
    ''' </summary>
    ''' <param name="objJituSeikyuu">実請求税抜金額</param>
    ''' <param name="objZeiritu">消費税マスタ.税率</param>
    ''' <remarks>消費税および税込金額計算をし、出力する</remarks>
    Private Sub SetZeiKingaku(ByVal objJituSeikyuu As Object, ByVal objZeiritu As Object)
        Dim lngSyouhizei As Long = 0 '消費税
        Dim lngZeikomiKingaku As Long = 0 '税込金額

        '【消費税】=【実請求税抜金額】＊消費税マスタ.税率（KEY：店別初期テーブル.税区分）
        lngSyouhizei = CLng(objJituSeikyuu * objZeiritu)

        '【税込金額】=【実請求税抜金額】＋【消費税】
        lngZeikomiKingaku = CLng(objJituSeikyuu) + lngSyouhizei

        TextSyouhizei.Text = lngSyouhizei.ToString(EarthConst.FORMAT_KINGAKU_1) '消費税
        TextZeikomiKingaku.Text = lngZeikomiKingaku.ToString(EarthConst.FORMAT_KINGAKU_1) '税込金額

    End Sub

    ''' <summary>
    ''' 金額設定(入金額/残額)
    ''' </summary>
    ''' <param name="strZeikomiNyuukingaku">邸別入金テーブル.税込入金金額</param>
    ''' <param name="intZeikomiHenkingaku">税込返金金額（KEY:邸別請求テーブル.区分、保証書NO、分類コード="150"）</param>
    ''' <param name="strZeikomiKingaku">入金額(税込)</param>
    ''' <remarks>入金額（税込）および残額計算をし、出力する</remarks>
    Private Sub SetNyuukingaku( _
                                    ByVal strZeikomiNyuukingaku As String _
                                    , ByVal intZeikomiHenkingaku As Integer _
                                    , ByVal strZeikomiKingaku As String _
                                )
        Dim intZeikomiNyuukingaku As Integer = Integer.Parse(strZeikomiNyuukingaku)
        Dim intNyuukingaku As Integer = 0
        Dim intZangaku As Integer = 0

        '【入金額（税込）】=邸別入金テーブル.税込入金金額－税込返金金額（KEY:邸別請求テーブル.区分、保証書NO、分類コード="150"）
        intNyuukingaku = intZeikomiNyuukingaku - intZeikomiHenkingaku

        '【残額】=【税込金額】－【入金額(税込)】
        intZangaku = Integer.Parse(strZeikomiKingaku) - intNyuukingaku

        TextNyuukingaku.Text = intNyuukingaku.ToString(EarthConst.FORMAT_KINGAKU_1) '入金額（税込）
        TextZangaku.Text = intZangaku.ToString(EarthConst.FORMAT_KINGAKU_1) '残額

        '【hidden項目】
        HiddenZeikomiNyuukingaku.Value = intZeikomiNyuukingaku  '税込入金金額
        HiddenZeikomiHenkinkingaku.Value = intZeikomiNyuukingaku  '税込返金金額

    End Sub

    ''' <summary>
    ''' 金額設定(残額)
    ''' </summary>
    ''' <param name="strZeikomiKingaku">税込金額</param>
    ''' <param name="strNyuukingaku">入金額(税込)</param>
    ''' <remarks>残額を計算し、出力する</remarks>
    Protected Function SetZangaku(ByVal strZeikomiKingaku As String, ByVal strNyuukingaku As String) As String
        Dim strReturn As String = ""
        Dim lngZeikomi As Long = 0
        Dim lngNyuukin As Long = 0

        If strZeikomiKingaku.Length = 0 Or strNyuukingaku.Length = 0 Then
            Return strReturn
        End If

        lngZeikomi = CLng(strZeikomiKingaku)
        lngNyuukin = CLng(strNyuukingaku)
        strReturn = (lngZeikomi - lngNyuukin).ToString(EarthConst.FORMAT_KINGAKU_1)
        Return strReturn
    End Function

    ''' <summary>
    ''' 工務店請求税抜金額変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKoumutenSeikyuuKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim syouhinRec As New Syouhin23Record

        setFocusAJ(TextJituseikyuuKingaku)

        '請求
        If SelectSeikyuuUmu.SelectedValue = "1" Then '有
            '商品コード
            If TextItemCd.Text.Length <> 0 Then '設定済

                '税情報設定
                SetZeiInfo(TextItemCd.Text)

                '請求先
                If TextSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '直接請求

                    '実請求税抜金額＝画面.工務店請求税抜金額
                    TextJituseikyuuKingaku.Text = TextKoumutenSeikyuuKingaku.Text

                    SetKingaku() '金額再計算

                ElseIf TextSeikyuusaki.Text = EarthConst.SEIKYU_TASETU Then '他請求

                    Dim kameitenlogic As New KameitenSearchLogic
                    Dim blnTorikesi As Boolean = False
                    Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                    If record.Torikesi <> 0 Then
                        record.KeiretuCd = ""
                    End If

                    If cl.getKeiretuFlg(record.KeiretuCd) Then '3系列

                        '<表2>実請求税抜金額（掛率）の設定▲

                        Dim logic As New JibanLogic
                        Dim koumuten_gaku As Integer = 0
                        Dim zeinuki_gaku As Integer = 0

                        cl.SetDisplayString(TextKoumutenSeikyuuKingaku.Text, koumuten_gaku)
                        koumuten_gaku = IIf(koumuten_gaku = Integer.MinValue, 0, koumuten_gaku)

                        If logic.GetSeikyuuGaku(sender, _
                                                5, _
                                                record.KeiretuCd, _
                                                TextItemCd.Text, _
                                                koumuten_gaku, _
                                                zeinuki_gaku) Then


                            '商品情報を取得(キー:商品コード)
                            syouhinRec = JibanLogic.GetSyouhinInfo(TextItemCd.Text, EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                            If Not syouhinRec Is Nothing Then
                                '(*3)請求有無変更時に、自動設定された工務店請求金額が0（商品マスタ.標準価格＝0）の場合、1回のみ実請求金額の自動設定を行う。
                                If syouhinRec.HyoujunKkk = 0 Then
                                    If HiddenJituseikyuu1Flg.Value = "" Then
                                        HiddenJituseikyuu1Flg.Value = "1" 'フラグをたてる

                                        ' 税抜金額（実請求金額）へセット
                                        TextJituseikyuuKingaku.Text = zeinuki_gaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                                    End If

                                    '*****************
                                    '* 地盤システムの処理に合わせるため、コメントアウト
                                    '*****************
                                    'Else
                                    '    ' 税抜金額（実請求金額）へセット
                                    '    TextJituseikyuuKingaku.Text = zeinuki_gaku.ToString(EarthConst.FORMAT_KINGAKU1)

                                    SetKingaku() '金額再計算

                                End If

                            End If

                        End If

                    End If

                End If
            End If

        End If

        '画面制御
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' 実請求税抜金額変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextJituSeikyuuKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(TextSeikyuusyoHakkouDate)

        '実請求税抜金額
        If TextJituseikyuuKingaku.Text.Length = 0 Then '入力なし
            TextSyouhizei.Text = ""
            TextZeikomiKingaku.Text = ""
            'TextZangaku.Text = "0"

            SetKingaku() '金額再計算

        Else '入力あり

            '請求有無
            If SelectSeikyuuUmu.SelectedValue = "1" Then '有
                '商品コード
                If TextItemCd.Text.Length <> 0 Then '設定済

                    '税情報設定
                    SetZeiInfo(TextItemCd.Text)

                    '請求先
                    If TextSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '直接請求

                        '工務店請求税抜金額＝画面.実請求税抜金額
                        TextKoumutenSeikyuuKingaku.Text = TextJituseikyuuKingaku.Text

                        SetKingaku() '金額再計算

                    Else '直接請求以外
                        SetKingaku() '金額再計算
                    End If

                End If

            End If

        End If

        '画面制御
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' 税率/税区分をHiddenにセットする
    ''' </summary>
    ''' <param name="strItemCd"></param>
    ''' <remarks></remarks>
    Protected Sub SetZeiInfo(ByVal strItemCd As String)
        Dim houkokusyoLogic As New HoukokusyoLogic
        Dim syouhinRec As New Syouhin23Record

        '商品情報を取得(キー:商品コード)
        syouhinRec = JibanLogic.GetSyouhinInfo(strItemCd, EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If Not syouhinRec Is Nothing Then
            HiddenZeiritu.Value = syouhinRec.Zeiritu '税率
            HiddenZeiKbn.Value = syouhinRec.ZeiKbn '税区分
        End If
    End Sub

    ''' <summary>
    ''' 金額設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKingaku(Optional ByVal blnZeigaku As Boolean = False)

        ' 税抜価格（実請求金額）
        Dim zeinuki_ctrl As TextBox = TextJituseikyuuKingaku
        ' 消費税率
        Dim zeiritu_ctrl As HtmlInputHidden = HiddenZeiritu
        ' 消費税額
        Dim zeigaku_ctrl As TextBox = TextSyouhizei
        ' 税込金額
        Dim zeikomi_gaku_ctrl As TextBox = TextZeikomiKingaku

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

            TextSyouhizei.Text = Format(zeigaku, EarthConst.FORMAT_KINGAKU_1) '消費税
            TextZeikomiKingaku.Text = Format(zeikomi_gaku, EarthConst.FORMAT_KINGAKU_1) '税込金額

        End If

        ' 入金額/残額をセット
        CalcZangaku(TextZeikomiKingaku.Text)

    End Sub

    ''' <summary>
    ''' 残額を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZangaku()
        Dim lngNyuukingaku As Long = 0
        Dim lngZangaku As Long = 0

        If HiddenNyuukingakuOld.Value <> "" Then
            lngNyuukingaku = CLng(HiddenNyuukingakuOld.Value.Replace(",", ""))
        End If

        '残額＝税込金額-入金額(税込)
        lngZangaku = CLng(TextZeikomiKingaku.Text.Replace(",", "")) - CLng(TextNyuukingaku.Text.Replace(",", ""))
        TextZangaku.Text = lngZangaku.ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub

#End Region

    ''' <summary>
    ''' (2) 調査報告書受理変更時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectJuri_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strJuri As String = SelectJuri.SelectedValue '受理
        Dim strJuriDate As String = TextJuriDate.Text '受理日
        Dim strHassouDate As String = TextHassouDate.Text '発送日
        Dim strHizukeHassouDate As String = "" '日付マスタ.報告書発送日

        'セットフォーカス
        setFocusAJ(SelectJuri)

        If strJuri = "1" Then '調査報告書受理＝1（有り）の場合

            If strJuriDate = String.Empty Then '受理日＝未入力の場合
                ' システム日付をセット
                TextJuriDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)

                If strHassouDate = String.Empty Then '発送日＝未入力の場合

                    Dim houkokusyoLogic As New HoukokusyoLogic
                    Dim strRet As String = String.Empty

                    '請求書発行日の自動設定
                    '※調査報告書受理日＞上記の日付マスタ.報告書発送日の場合は、日付マスタ.報告書発送日＋1ヶ月を編集する
                    strRet = houkokusyoLogic.GetHoukokusyoHassoudate( _
                                                             ucGyoumuKyoutuu.Kubun _
                                                            , TextJuriDate.Text)
                    If strRet <> String.Empty Then
                        TextHassouDate.Text = strRet
                    End If
                End If

            End If

        ElseIf strJuri = "2" Or strJuri = "3" Then '調査報告書受理＝2,3（保留、又は、送付不要）の場合

            If strJuriDate = String.Empty Then '受理日＝未入力の場合
                ' システム日付をセット
                TextJuriDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
            End If

        ElseIf strJuri = "4" Then '調査報告書受理＝4（再発送）の場合
            If strJuriDate = String.Empty Then '受理日＝未入力の場合
                ' システム日付をセット
                TextJuriDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
            End If

        End If

        '活性化制御
        SetEnableControl()

    End Sub

#Region "判定種別関連"

    ''' <summary>
    ''' 判定コード1ボタン押下時/判定種別検索画面参照
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHantei1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If HiddenHanteiSearchType1.Value <> "1" Then
            TextHantei1_ChangeSub(sender, e)
        Else
            'コードonchangeで呼ばれた場合
            TextHantei1_ChangeSub(sender, e, False)
            HiddenHanteiSearchType1.Value = String.Empty
        End If

        ucGyoumuKyoutuu.SetHanteiNG(TextHantei1Cd.Text, TextHantei2Cd.Text)

    End Sub

    ''' <summary>
    ''' 判定コード2ボタン押下時/判定種別検索画面参照
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHantei2_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If HiddenHanteiSearchType2.Value <> "1" Then
            TextHantei2_ChangeSub(sender, e)
        Else
            'コードonchangeで呼ばれた場合
            TextHantei2_ChangeSub(sender, e, False)
            HiddenHanteiSearchType2.Value = String.Empty
        End If

        ucGyoumuKyoutuu.SetHanteiNG(TextHantei1Cd.Text, TextHantei2Cd.Text)

    End Sub

    ''' <summary>
    ''' (12) 判定1変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextHantei1_ChangeSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim kisosiyouSearchLogic As New KisoSiyouLogic
        Dim blnTorikesi As Boolean = False
        Dim dataArray As New List(Of KisoSiyouRecord)
        Dim intCount As Integer = 0

        '1.画面の設定
        '判定1名の表示色を設定する
        If TextHantei1Cd.Text <> "" Then
            dataArray = kisosiyouSearchLogic.GetKisoSiyouSearchResult( _
                                    TextHantei1Cd.Text _
                                    , "" _
                                    , ucGyoumuKyoutuu.AccKameitenCd.Value _
                                    , True _
                                    , blnTorikesi _
                                    , intCount _
                                    )
        End If

        If intCount = 1 Then
            Dim recData As KisoSiyouRecord = dataArray(0)
            TextHantei1Cd.Text = recData.KsSiyouNo
            SpanHantei1.InnerHtml = recData.KsSiyou
            Me.HiddenHantei1CdMae.Value = TextHantei1Cd.Text

            ' 調査会社NG設定
            If recData.KahiKbn = 9 Then
                SpanHantei1.Style("color") = "red"
            Else
                SpanHantei1.Style("color") = "blue"
            End If

            '判定コード1検索ボタン実行後処理
            ButtonHantei1SearchAfter(sender, e)

            'セットフォーカス
            setFocusAJ(ButtonHantei1)
        Else

            SpanHantei1.Style("color") = "black"

            '判定コード未設定のため、クリア
            SpanHantei1.InnerHtml = ""

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpFocusScript = "objEBI('" & ButtonHantei1.ClientID & "').focus();"
            Dim tmpScript = "callSearch('" & TextHantei1Cd.ClientID & EarthConst.SEP_STRING & ucGyoumuKyoutuu.AccKameitenCd.ClientID & _
                                        "','" & UrlConst.SEARCH_HANTEI & "','" & _
                                        TextHantei1Cd.ClientID & EarthConst.SEP_STRING & SpanHantei1.ClientID & _
                                        "','" & ButtonHantei1.ClientID & "');"
            If callWindow Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & tmpScript, True)
            ElseIf HiddenHanteiSearchType1.Value = "1" Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript, True)
            End If
        End If

    End Sub

    ''' <summary>
    ''' 判定コード1検索ボタン実行後処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHantei1SearchAfter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""

        '(1)解析担当者＝未入力の場合
        If SelectKaisekiTantousya.SelectedValue = "" Then
            '該当者がいるかチェック
            If ChkTantousya(SelectKaisekiTantousya, cl.GetDispNum(userInfo.AccountNo)) Then
                TextKaisekiTantousyaCd.Text = cl.GetDispNum(userInfo.AccountNo)
                SelectKaisekiTantousya.SelectedValue = cl.GetDispNum(userInfo.AccountNo)
            End If
        End If

        '(2)判定1＝入力の場合
        If TextHantei1Cd.Text.Length <> 0 Then
            '計画書作成日
            If TextKeikakusyoSakuseiDate.Text.Length = 0 Then
                TextKeikakusyoSakuseiDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
            End If
        End If

        '2.判定コード1変更確認
        '(1)保証書発行日=入力済、かつ判定1≠地盤テーブル（更新前）.判定1の場合
        If HiddenHosyousyoHakkouDate.Value <> "" And TextHantei1Cd.Text <> HiddenHantei1CdOld.Value Then
            '判定コード1の確認メッセージを表示する。
            Dim strMsg As String = Messages.MSG063C.Replace("@PARAM1", HiddenHantei1CdOld.Value)

            tmpScript = "callHantei1Cancel('" & strMsg & "','" & ButtonHantei1.ClientID & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHantei1SearchAfter1", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' (13) 判定2変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextHantei2_ChangeSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim kisosiyouSearchLogic As New KisoSiyouLogic
        Dim blnTorikesi As Boolean = False
        Dim dataArray As New List(Of KisoSiyouRecord)
        Dim intCount As Integer = 0

        '1.画面の設定
        '判定1名の表示色を設定する
        If TextHantei2Cd.Text <> "" Then
            dataArray = kisosiyouSearchLogic.GetKisoSiyouSearchResult( _
                                    TextHantei2Cd.Text _
                                    , "" _
                                    , ucGyoumuKyoutuu.AccKameitenCd.Value _
                                    , True _
                                    , blnTorikesi _
                                    , intCount _
                                    )
        End If

        If intCount = 1 Then
            Dim recData As KisoSiyouRecord = dataArray(0)
            TextHantei2Cd.Text = recData.KsSiyouNo
            SpanHantei2.InnerHtml = recData.KsSiyou
            Me.HiddenHantei2CdMae.Value = TextHantei2Cd.Text

            ' 調査会社NG設定
            If recData.KahiKbn = 9 Then
                SpanHantei2.Style("color") = "red"
            Else
                SpanHantei2.Style("color") = "blue"
            End If

            'セットフォーカス
            setFocusAJ(ButtonHantei2)
        Else

            SpanHantei2.Style("color") = "black"

            '判定コード未設定のため、クリア
            SpanHantei2.InnerHtml = ""

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpFocusScript = "objEBI('" & ButtonHantei2.ClientID & "').focus();"
            Dim tmpScript = "callSearch('" & TextHantei2Cd.ClientID & EarthConst.SEP_STRING & ucGyoumuKyoutuu.AccKameitenCd.ClientID & _
                                        "','" & UrlConst.SEARCH_HANTEI & "','" & _
                                        TextHantei2Cd.ClientID & EarthConst.SEP_STRING & SpanHantei2.ClientID & _
                                        "','" & ButtonHantei2.ClientID & "');"
            If callWindow Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & tmpScript, True)
            ElseIf HiddenHanteiSearchType2.Value = "1" Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript, True)
            End If
        End If

    End Sub

    ''' <summary>
    ''' 解析担当者の存在チェック
    ''' </summary>
    ''' <param name="drpArg">チェック対象ドロップダウンリスト</param>
    ''' <param name="strSearchCd">[String]コード値</param>
    ''' <returns>[Boolean]True or False</returns>
    ''' <remarks>解析担当者が存在するかどうかを判断する</remarks>
    Protected Function ChkTantousya(ByVal drpArg As DropDownList, ByVal strSearchCd As String) As Boolean
        Dim intItemCnt As Integer = drpArg.Items.Count 'アイテム数
        Dim intCnt As Integer 'カウンタ

        For intCnt = 0 To intItemCnt - 1
            If strSearchCd = drpArg.Items(intCnt).Value Then
                Return True
            End If
        Next

        Return False
    End Function

#Region "物件履歴データの入力"
    ''' <summary>
    ''' 物件履歴登録用レコードにコントロールの内容をセットします
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBukkenCtrlData() As BukkenRirekiRecord

        ' 判定変更理由=未設定時、物件履歴への書込は行わない
        If Me.HiddenHanteiHenkouRiyuu.Value = String.Empty Then
            Return Nothing
        End If

        '物件履歴登録用レコード
        Dim record As New BukkenRirekiRecord
        Dim logic As New HoukokusyoLogic

        '工事判定結果FLG
        Dim intKojHanteiKekkaFlgOld As Integer = 0 'OLD
        Dim intKojHanteiKekkaFlg As Integer = 0

        If CStr(Me.HiddenKojHanteiKekkaFlgOld.Value) <> String.Empty Then
            intKojHanteiKekkaFlgOld = CInt(Me.HiddenKojHanteiKekkaFlgOld.Value)
        End If
        intKojHanteiKekkaFlg = logic.GetKojHanteiKekkaFlg(Me.TextHantei1Cd.Text, Me.TextHantei2Cd.Text, Me.SelectHanteiSetuzokuMoji.SelectedValue)

        ' 区分
        record.Kbn = ucGyoumuKyoutuu.Kubun
        ' 保証書NO
        record.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        '履歴種別
        record.RirekiSyubetu = EarthConst.BUKKEN_RIREKI_RIREKI_SYUBETU_HANTEI
        '履歴NO
        record.RirekiNo = Me.SetRirekiNo(intKojHanteiKekkaFlgOld, intKojHanteiKekkaFlg)
        '入力NO
        record.NyuuryokuNo = Integer.MinValue
        '内容
        record.Naiyou = Me.SetNaiyou()
        '汎用日付
        cl.SetDisplayString("", record.HanyouDate)
        '汎用コード
        cl.SetDisplayString("", record.HanyouCd)
        '管理日付
        cl.SetDisplayString("", record.KanriDate)
        '管理コード
        record.KanriCd = Me.SetKanriCd(intKojHanteiKekkaFlgOld, intKojHanteiKekkaFlg)
        '変更可否フラグ
        record.HenkouKahiFlg = 1
        '取消
        record.Torikesi = 0
        '登録(更新)ログインユーザID
        record.UpdLoginUserId = userInfo.LoginUserId
        '登録(更新)日時
        record.UpdDatetime = DateTime.Now

        Return record
    End Function

    ''' <summary>
    ''' 条件毎に履歴NOをセットする
    ''' ※変更前と変更後.工事FLGの値で判定
    ''' </summary>
    ''' <param name="intKojHanteiKekkaFlgOld">変更前.工事判定FLG</param>
    ''' <param name="intKojHanteiKekkaFlg">変更後.工事判定FLG</param>
    ''' <returns>
    ''' 変更工事無し→工事無し（変更前の地盤Tの工事FLG=0、変更後の工事FLG=0）：1
    ''' 変更工事あり→工事あり（変更前の地盤Tの工事FLG=1、変更後の工事FLG=1）：2
    ''' 変更工事無し→工事あり（変更前の地盤Tの工事FLG=0、変更後の工事FLG=1）：3
    ''' 変更工事あり→工事無し（変更前の地盤Tの工事FLG=1、変更後の工事FLG=0）：4
    ''' </returns>
    ''' <remarks></remarks>
    Private Function SetRirekiNo(ByVal intKojHanteiKekkaFlgOld As Integer, ByVal intKojHanteiKekkaFlg As Integer) As Integer
        Dim intRet As Integer = Integer.MinValue  '戻り値

        If intKojHanteiKekkaFlgOld = 0 Then

            If intKojHanteiKekkaFlg = 0 Then
                intRet = 1
            ElseIf intKojHanteiKekkaFlg = 1 Then
                intRet = 3
            End If

        ElseIf intKojHanteiKekkaFlgOld = 1 Then

            If intKojHanteiKekkaFlg = 0 Then
                intRet = 4
            ElseIf intKojHanteiKekkaFlg = 1 Then
                intRet = 2
            End If

        End If

        Return intRet
    End Function

    ''' <summary>
    ''' 条件毎に内容をセットする
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetNaiyou() As String
        Dim strRetVal As String = String.Empty '戻り値

        Dim kisoSiyouLogic As New KisoSiyouLogic

        '判定1
        Dim intHantei1Cd As Integer
        '判定2
        Dim intHantei2Cd As Integer
        '判定接続詞
        Dim intHanteiSetuzokusi As Integer
        '判定1名
        Dim strHantei1Mei As String = String.Empty
        '判定2名
        Dim strHantei2Mei As String = String.Empty
        '判定接続詞名
        Dim strHanteiSetuzokusiMei As String = String.Empty

        '判定1
        If CStr(Me.HiddenHantei1CdOld.Value) <> String.Empty Then
            intHantei1Cd = CInt(Me.HiddenHantei1CdOld.Value)
        Else
            intHantei1Cd = Integer.MinValue
        End If
        '判定2
        If CStr(Me.HiddenHantei2CdOld.Value) <> String.Empty Then
            intHantei2Cd = CInt(Me.HiddenHantei2CdOld.Value)
        Else
            intHantei2Cd = Integer.MinValue
        End If
        '判定接続詞
        If CStr(Me.HiddenHanteiSetuzokuMojiOld.Value) <> String.Empty Then
            intHanteiSetuzokusi = CInt(Me.HiddenHanteiSetuzokuMojiOld.Value)
        Else
            intHanteiSetuzokusi = Integer.MinValue
        End If

        strHantei1Mei = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(intHantei1Cd)) '判定１名
        strHantei2Mei = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(intHantei2Cd)) '判定２名

        ' コンボ設定ヘルパークラスを生成
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList
        '●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.KsSiyouSetuzokusi, True, False)

        '**************************
        '「内容」の自動設定
        '**************************
        '**************
        '*変更前
        '**************      
        objDrpTmp.SelectedValue = intHanteiSetuzokusi '判定接続詞

        strRetVal = EarthConst.BUKKEN_RIREKI_HENKOU_MAE
        If Me.HiddenHantei1CdOld.Value <> String.Empty Then
            '判定1
            strRetVal &= "[" & Me.HiddenHantei1CdOld.Value & "]" & strHantei1Mei
        End If
        If Me.HiddenHanteiSetuzokuMojiOld.Value <> String.Empty Then
            '判定接続詞
            strRetVal &= "[" & Me.HiddenHanteiSetuzokuMojiOld.Value & "]" & objDrpTmp.SelectedItem.Text
        End If
        If Me.HiddenHantei2CdOld.Value <> String.Empty Then
            '判定2
            strRetVal &= "[" & Me.HiddenHantei2CdOld.Value & "]" & strHantei2Mei
        End If

        '**************
        '*変更後
        '**************
        objDrpTmp.SelectedValue = Me.SelectHanteiSetuzokuMoji.SelectedValue

        strRetVal &= EarthConst.BUKKEN_RIREKI_HENKOU_ATO
        If Me.TextHantei1Cd.Text <> String.Empty Then
            '判定1
            strRetVal &= "[" & Me.TextHantei1Cd.Text & "]" & Me.SpanHantei1.InnerHtml
        End If
        If Me.SelectHanteiSetuzokuMoji.SelectedValue <> String.Empty Then
            '判定接続詞
            strRetVal &= "[" & Me.SelectHanteiSetuzokuMoji.SelectedValue & "]" & objDrpTmp.SelectedItem.Text
        End If
        If Me.TextHantei2Cd.Text <> String.Empty Then
            '判定2
            strRetVal &= "[" & Me.TextHantei2Cd.Text & "]" & Me.SpanHantei2.InnerHtml
        End If

        '**************
        '* 判定変更理由
        '**************
        strRetVal &= EarthConst.BUKKEN_RIREKI_HENKOU_RIYUU & Me.HiddenHanteiHenkouRiyuu.Value

        Return strRetVal
    End Function

    ''' <summary>
    ''' 条件毎に管理コードをセットする
    ''' </summary>
    ''' <param name="intKojHanteiKekkaFlgOld">変更前.工事判定FLG</param>
    ''' <param name="intKojHanteiKekkaFlg">変更後.工事判定FLG</param>
    ''' <returns>第一引数と第二引数を文字列連結して返す</returns>
    ''' <remarks></remarks>
    Private Function SetKanriCd(ByVal intKojHanteiKekkaFlgOld As Integer, ByVal intKojHanteiKekkaFlg As Integer) As String
        Dim strRet As String = String.Empty

        strRet = CStr(intKojHanteiKekkaFlgOld) & CStr(intKojHanteiKekkaFlg)

        Return strRet
    End Function

#End Region

#End Region

#Region "自動設定"

    ''' <summary>
    ''' 調査報告書発送日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextHassouDate_ServerChange(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""

        'セットフォーカス
        setFocusAJ(SelectSyasinJuri)

        '発送日
        If TextHassouDate.Text.Length <> 0 Then '入力あり
            '自動設定はなし、画面制御のみ＝＞特に処理なし

        ElseIf TextHassouDate.Text.Length = 0 Then '入力なし

            '(*4)発注書金額≠0,かつ,≠Nullの場合、エラーメッセージ（037）を表示し、自動設定（クリア）を行わずに、未入力に変更した項目(発送日/再発行日/請求有無)は元の値に戻す。
            If TextHattyuusyoKingaku.Text <> "0" And TextHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                TextHassouDate.Text = HiddenHassouDateMae.Value '変更前の値に戻す ※注 Javascriptでの設定ではChangeイベントが発生しない
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextHassouDate_ServerChange1", tmpScript, True)
                Exit Sub
            End If

            '●自動設定
            Me.TextSaihakkouDate.Text = String.Empty '再発行日
            Me.TextSaihakkouRiyuu.Text = String.Empty '再発行理由

            ClearControl()

        End If

        '画面制御
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' 再発行日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSaihakkouDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strDtTmp As String
        Dim tmpScript As String = ""

        'セットフォーカス
        setFocusAJ(TextSaihakkouDate)

        '再発行日
        If TextSaihakkouDate.Text.Length <> 0 Then '入力あり

            setFocusAJ(SelectSeikyuuUmu)

            '請求有無
            If SelectSeikyuuUmu.SelectedValue = "1" Then '有
                '商品コード
                If TextItemCd.Text.Length <> 0 Then '設定済
                    '請求書発行日
                    If TextSeikyuusyoHakkouDate.Text.Length = 0 Then '未入力
                        '請求締め日のセット
                        Me.ucSeikyuuSiireLink.SetSeikyuuSimeDate(Me.TextItemCd.Text)
                        '請求書発行日の自動設定
                        strDtTmp = Me.ucSeikyuuSiireLink.GetSeikyuusyoHakkouDate()
                        TextSeikyuusyoHakkouDate.Text = strDtTmp
                    End If

                    '売上年月日
                    If TextUriageNengappi.Text.Length = 0 Then '未入力
                        '売上年月日の自動設定
                        TextUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If

                End If

            ElseIf SelectSeikyuuUmu.SelectedValue = "0" Then '無
                '商品コード
                If TextItemCd.Text.Length <> 0 Then '設定済
                    '売上年月日
                    If TextUriageNengappi.Text.Length = 0 Then '未入力
                        '売上年月日の自動設定
                        TextUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If
                End If
            End If

        Else '入力なし

            '(*4)発注書金額≠0,かつ,≠Nullの場合、エラーメッセージ（037）を表示し、自動設定（クリア）を行わずに、未入力に変更した項目(発送日/再発行日/請求有無)は元の値に戻す。
            If TextHattyuusyoKingaku.Text <> "0" And TextHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                TextSaihakkouDate.Text = HiddenSaihakkouDateMae.Value '変更前の値に戻す ※注 Javascriptでの設定ではChangeイベントが発生しない
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextSaihakkouDate_TextChanged1", tmpScript, True)
                Exit Sub
            End If

            '●自動設定
            Me.TextSaihakkouRiyuu.Text = String.Empty '再発行理由

            ClearControl()

        End If

        '画面制御
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' 請求有無変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectSeikyuuUmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""
        Dim blnTorikesi As Boolean = False

        Dim strDtTmp As String

        Dim syouhinRec As New Syouhin23Record

        'セットフォーカス
        setFocusAJ(SelectSeikyuuUmu)

        '仕入額は常に0円
        Me.HiddenSiireGaku.Value = "0"
        Me.HiddenSiireSyouhiZei.Value = "0"

        '請求有無
        If SelectSeikyuuUmu.SelectedValue = "" Then '空白

            '(*4)発注書金額≠0,かつ,≠Nullの場合、エラーメッセージ（037）を表示し、自動設定（クリア）を行わずに、未入力に変更した項目(発送日/再発行日/請求有無)は元の値に戻す。
            If TextHattyuusyoKingaku.Text <> "0" And TextHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                SelectSeikyuuUmu.SelectedValue = HiddenSeikyuuUmuMae.Value '変更前の値に戻す ※注 Javascriptでの設定ではChangeイベントが発生しない
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextSaihakkouDate_TextChanged1", tmpScript, True)
                Exit Sub
            End If

            '●自動設定
            ClearControl() '空白クリア

        ElseIf SelectSeikyuuUmu.SelectedValue = "0" Then '無

            '金額の0クリア
            Clear0SyouhinTable()

            '商品コード/商品名の自動設定▲
            SetSyouhinInfo()

            SetKingaku() '金額の再計算

        ElseIf SelectSeikyuuUmu.SelectedValue = "1" Then '有

            '商品コード/商品名の自動設定▲
            SetSyouhinInfo()

            '商品コード
            If TextItemCd.Text = String.Empty Then '設定なし
                '請求有無
                SelectSeikyuuUmu.SelectedValue = "" '空白

                SetEnableControl() '画面制御
                Exit Sub
            Else
                '請求書発行日
                If TextSeikyuusyoHakkouDate.Text = String.Empty Then '未入力
                    '請求締め日のセット
                    Me.ucSeikyuuSiireLink.SetSeikyuuSimeDate(Me.TextItemCd.Text)
                    '請求書発行日の自動設定
                    strDtTmp = Me.ucSeikyuuSiireLink.GetSeikyuusyoHakkouDate()
                    TextSeikyuusyoHakkouDate.Text = strDtTmp
                End If

                TextUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) '売上年月日

                '発注書確定
                If TextHattyuusyoKakutei.Text.Length = 0 Then '(*5)発注書確定が空白の場合は、「0：未確定」を設定する
                    TextHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
                End If

            End If

            '商品コード
            If TextItemCd.Text <> String.Empty Then '設定済

                '*************************
                '* 以下、自動設定処理
                '*************************
                If TextSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '直接請求

                    '(*6)加盟店が取消の場合、他請求3系列以外の扱いとする
                    Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                    If record Is Nothing Then 'データ取得できなかった場合
                        SelectSeikyuuUmu.SelectedValue = "" '取得NG(請求有無のクリア)

                        ClearControl() '空白クリア
                    End If

                    If record.Torikesi <> 0 Then '取消フラグがたっている場合
                        '**************************************************
                        ' 他請求（系列以外）
                        '**************************************************
                        ' 工務店請求額は０
                        TextKoumutenSeikyuuKingaku.Text = "0"

                        '実請求税抜金額の自動設定
                        syouhinRec = JibanLogic.GetSyouhinInfo(TextItemCd.Text, EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                        If Not syouhinRec Is Nothing Then
                            HiddenZeiritu.Value = syouhinRec.Zeiritu '税率
                            HiddenZeiKbn.Value = syouhinRec.ZeiKbn '税区分

                            '実請求税抜金額＝商品マスタ.標準価格
                            TextJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)
                        End If

                    Else
                        '**************************************************
                        ' 直接請求
                        '**************************************************
                        '工務店(A)
                        '実請求(A)

                        syouhinRec = JibanLogic.GetSyouhinInfo(TextItemCd.Text, EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                        If Not syouhinRec Is Nothing Then
                            '工務店請求税抜金額＝商品マスタ.標準価格
                            TextKoumutenSeikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)
                            '実請求税抜金額＝画面.工務店請求税抜金額
                            TextJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                            HiddenZeiritu.Value = syouhinRec.Zeiritu '税率
                            HiddenZeiKbn.Value = syouhinRec.ZeiKbn '税区分
                        End If

                    End If

                ElseIf TextSeikyuusaki.Text = EarthConst.SEIKYU_TASETU Then '他請求

                    Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                    If record Is Nothing Then 'データ取得できなかった場合
                        SelectSeikyuuUmu.SelectedValue = "" '取得NG(請求有無のクリア)

                        ClearControl() '空白クリア
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

                        syouhinRec = JibanLogic.GetSyouhinInfo(TextItemCd.Text, EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                        If Not syouhinRec Is Nothing Then
                            HiddenZeiritu.Value = syouhinRec.Zeiritu '税率
                            HiddenZeiKbn.Value = syouhinRec.ZeiKbn '税区分

                            '工務店請求税抜金額＝商品マスタ.標準価格
                            TextKoumutenSeikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                            '※画面.工務店請求税抜金額＝0 の場合は 0 固定
                            If TextKoumutenSeikyuuKingaku.Text = "0" Then
                                TextJituseikyuuKingaku.Text = "0" '実請求税抜金額

                            Else
                                '**************************************************
                                ' 他請求（3系列）
                                '**************************************************
                                Dim zeinukiGaku As Integer = 0

                                If JibanLogic.GetSeikyuuGaku(sender, _
                                                              3, _
                                                              record.KeiretuCd, _
                                                              TextItemCd.Text, _
                                                              syouhinRec.HyoujunKkk, _
                                                              zeinukiGaku) Then
                                    ' 実請求金額へセット
                                    TextJituseikyuuKingaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                                End If

                            End If

                        End If

                    Else '3系列以外

                        '工務店(B)
                        '実請求(C)

                        '**************************************************
                        ' 他請求（3系列以外）
                        '**************************************************
                        ' 工務店請求額は０
                        TextKoumutenSeikyuuKingaku.Text = "0"

                        '実請求税抜金額の自動設定
                        syouhinRec = JibanLogic.GetSyouhinInfo(TextItemCd.Text, EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                        If Not syouhinRec Is Nothing Then
                            HiddenZeiritu.Value = syouhinRec.Zeiritu '税率
                            HiddenZeiKbn.Value = syouhinRec.ZeiKbn '税区分

                            '実請求税抜金額＝商品マスタ.標準価格
                            TextJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                        End If

                    End If

                End If

                SetKingaku() '金額の再計算

            End If

        End If

        '画面制御
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' 調査報告書情報内・商品テーブル金額エリアの0クリア
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub Clear0SyouhinTable()
        '金額の0クリア
        '●自動設定
        TextKoumutenSeikyuuKingaku.Text = "0" '工務店請求金額
        TextJituseikyuuKingaku.Text = "0" '実請求金額
        TextSyouhizei.Text = "0" '消費税
        TextZeikomiKingaku.Text = "0" '税込金額
        TextSeikyuusyoHakkouDate.Text = "" '請求書発行日
        TextUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) '売上年月日
        '発注書確定
        If TextHattyuusyoKakutei.Text.Length = 0 Then '(*5)発注書確定が空白の場合は、「0：未確定」を設定する
            TextHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
        End If

    End Sub

    ''' <summary>
    ''' 調査報告書情報内・商品テーブル金額エリアの空白クリア
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearControl()
        '●自動設定(空白クリア)
        SelectSeikyuuUmu.SelectedValue = "" '請求有無
        SpanSeikyuUmu.Style.Add("display", "none") 'SPAN請求有無
        SpanSeikyuUmu.InnerHtml = "" 'SPAN請求有無

        TextItemCd.Text = "" '商品コード
        SpanItemMei.InnerHtml = "" '商品名
        TextKoumutenSeikyuuKingaku.Text = "" '工務店請求金額
        TextJituseikyuuKingaku.Text = "" '実請求金額
        HiddenSiireGaku.Value = ""        '仕入れ額
        TextSyouhizei.Text = "" '消費税
        HiddenSiireSyouhiZei.Value = ""   '仕入消費税
        TextZeikomiKingaku.Text = "" '税込金額
        TextSeikyuusyoHakkouDate.Text = "" '請求書発行日
        TextUriageNengappi.Text = "" '売上年月日
        TextHattyuusyoKakutei.Text = "" '発注書確定

        SetKingaku() '金額の再計算

    End Sub

    ''' <summary>
    '''商品毎の請求・仕入先が変更されていないかをチェックし、
    '''変更されている場合情報の再取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setSeikyuuSiireHenkou(ByVal sender As Object, ByVal e As System.EventArgs)
        '請求先、仕入先が変更された行をチェックし、存在した場合は
        '各行の請求有無変更時の処理を実行する

        '**************************
        ' 調査報告書
        '**************************
        '請求仕入変更リンクUC内のチェックフラグHiddenを参照し、フラグが立っている場合は処理実施
        If Me.ucSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then

            '請求有無変更時処理
            Me.SelectSeikyuuUmu_SelectedIndexChanged(sender, e)

            'フラグ初期化
            Me.ucSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty

            'フォーカスは請求仕入変更リンク
            setFocusAJ(Me.ucSeikyuuSiireLink.AccLinkSeikyuuSiireHenkou)

            '変更された商品が有った場合、UpdatePanelをUpdate
            Me.UpdatePanelSyouhinInfo.Update()

        End If

    End Sub

    ''' <summary>
    ''' 商品の基本情報をセットする(調査報告書)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSyouhinInfo()
        Dim syouhinRec As Syouhin23Record

        '商品コード/商品名の自動設定▲
        syouhinRec = JibanLogic.GetSyouhinInfo("", EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If syouhinRec Is Nothing Then
            TextItemCd.Text = "" '商品コード
            SpanItemMei.InnerHtml = "" '商品名
        Else
            TextItemCd.Text = cl.GetDispStr(syouhinRec.SyouhinCd) '商品コード
            SpanItemMei.InnerHtml = cl.GetDispStr(syouhinRec.SyouhinMei) '商品名
            HiddenZeiritu.Value = syouhinRec.Zeiritu '税率
            HiddenZeiKbn.Value = syouhinRec.ZeiKbn '税区分

            '画面上で請求先が指定されている場合、レコードの請求先を上書き
            If Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value <> String.Empty Then
                '請求先をレコードにセット
                syouhinRec.SeikyuuSakiCd = Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value
                syouhinRec.SeikyuuSakiBrc = Me.ucSeikyuuSiireLink.AccSeikyuuSakiBrc.Value
                syouhinRec.SeikyuuSakiKbn = Me.ucSeikyuuSiireLink.AccSeikyuuSakiKbn.Value
            End If
            Me.TextSeikyuusaki.Text = syouhinRec.SeikyuuSakiType '●請求先

        End If

    End Sub

#End Region

#Region "画面制御"

    ''' <summary>
    ''' コントロールの活性化制御
    ''' </summary>
    ''' <remarks>コントロールの活性化制御を仕様書の優先順に記載する</remarks>
    Public Sub SetEnableControl()

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable
        htNotTarget.Add(TextSeikyuusaki.ID, TextSeikyuusaki) '請求先
        htNotTarget.Add(TextHattyuusyoKingaku.ID, TextHattyuusyoKingaku) '発注書金額
        htNotTarget.Add(TextHattyuusyoKakutei.ID, TextHattyuusyoKakutei) '発注書確定
        jSM.Hash2Ctrl(TdHassouDate, EarthConst.MODE_VIEW, ht) '発送日
        jSM.Hash2Ctrl(TdSaihakkouDate, EarthConst.MODE_VIEW, ht) '再発行日
        jSM.Hash2Ctrl(TrSyouhin, EarthConst.MODE_VIEW, ht, htNotTarget) '商品テーブル
        jSM.Hash2Ctrl(TdSaihakkouRiyuu, EarthConst.MODE_VIEW, ht) '再発行理由

        '●優先順1
        '売上処理済(邸別請求テーブル.売上計上FLG＝1)
        If SpanUriageSyorizumi.InnerHtml = EarthConst.URIAGE_ZUMI Then '変更箇所なしのため、画面から取得
            cl.chgDispSyouhinText(TextSaihakkouRiyuu) '再発行理由

            '●優先順2
        ElseIf TextHassouDate.Text.Length = 0 Then '調査報告書発送日＝未入力
            cl.chgDispSyouhinText(TextHassouDate) '発送日
        Else
            cl.chgDispSyouhinText(TextHassouDate) '発送日

            '●優先順3
            If TextSaihakkouDate.Text.Length = 0 Then '再発行日＝未入力
                cl.chgDispSyouhinText(TextSaihakkouDate) '再発行日

                '●優先順4,5
            ElseIf SelectSeikyuuUmu.SelectedValue = "0" Or SelectSeikyuuUmu.SelectedValue = "" Then '請求 無or空白
                cl.chgDispSyouhinText(TextSaihakkouDate) '再発行日
                cl.chgDispSyouhinPull(SelectSeikyuuUmu, SpanSeikyuUmu) '請求有無
                cl.chgDispSyouhinText(TextSaihakkouRiyuu) '再発行理由

                '●優先順6
            Else
                cl.chgDispSyouhinText(TextSaihakkouDate) '再発行日
                cl.chgDispSyouhinPull(SelectSeikyuuUmu, SpanSeikyuUmu) '請求有無
                cl.chgDispSyouhinText(TextSaihakkouRiyuu) '再発行理由

                Dim kameitenlogic As New KameitenSearchLogic
                Dim blnTorikesi As Boolean = False
                Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                '(*6)加盟店が取消の場合、他請求3系列以外の扱いとする
                If record.Torikesi <> 0 Then
                    record.KeiretuCd = ""
                End If

                If TextSeikyuusaki.Text = EarthConst.SEIKYU_TASETU And cl.getKeiretuFlg(record.KeiretuCd) = False Then

                    cl.chgDispSyouhinText(TextJituseikyuuKingaku) '実請求税抜金額
                    cl.chgDispSyouhinText(TextSeikyuusyoHakkouDate) '請求書発行日

                    '●優先順7
                Else
                    cl.chgDispSyouhinText(TextJituseikyuuKingaku) '実請求税抜金額
                    cl.chgDispSyouhinText(TextSeikyuusyoHakkouDate) '請求書発行日
                    cl.chgDispSyouhinText(TextKoumutenSeikyuuKingaku) '工務店請求税抜金額
                End If

            End If

        End If

        '受理変更時処理
        If SelectJuri.SelectedValue = "2" Or SelectJuri.SelectedValue = "3" Then
            jSM.Hash2Ctrl(TdHassouDate, EarthConst.MODE_VIEW, ht) '発送日
            jSM.Hash2Ctrl(TdSaihakkouDate, EarthConst.MODE_VIEW, ht) '再発行日
        End If

        '調査実施日未入力時処理
        If TextTyousaJissiDate.Text = "" Then
            jSM.Hash2Ctrl(TdHantei, EarthConst.MODE_VIEW, ht) '判定関連
            SetButtonSearchDisp()

        End If


    End Sub

#End Region

End Class