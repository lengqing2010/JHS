Partial Public Class Syouhin4RecordCtrl
    Inherits System.Web.UI.UserControl

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim iraiSession As New IraiSession
    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim jSM As New JibanSessionManager
    Dim cl As New CommonLogic
    Dim MLogic As New MessageLogic

#Region "プロパティ"

    ''' <summary>
    ''' 外部からのアクセス用 for Hidden加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnKameitenCd() As HtmlInputHidden
        Get
            Return Me.HiddenKameitenCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenKameitenCd = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for Hidden区分
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnKbn() As HtmlInputHidden
        Get
            Return Me.HiddenKubun
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenKubun = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for Hidden地盤.調査会社
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnJibanTysKaisyaCd() As HtmlInputHidden
        Get
            Return Me.HiddenJibanTysKaisyaCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            Me.HiddenJibanTysKaisyaCd = value
        End Set
    End Property

    ''' <summary>
    ''' 請求先・仕入先リンクの外部アクセス用
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSeikyuuSiireLink() As SeikyuuSiireLinkCtrl
        Get
            Return ucSeikyuuSiireLink
        End Get
        Set(ByVal value As SeikyuuSiireLinkCtrl)
            ucSeikyuuSiireLink = value
        End Set
    End Property

#Region "権限系"

#Region "依頼業務権限"
    ''' <summary>
    ''' 依頼業務権限
    ''' </summary>
    ''' <value></value>


    Public Property IraiGyoumuKengen() As String
        Get
            Return Integer.Parse(HiddenIraiGyoumuKengen.Value)
        End Get
        Set(ByVal value As String)
            HiddenIraiGyoumuKengen.Value = value.ToString()
        End Set
    End Property
#End Region

#Region "発注書管理権限"
    ''' <summary>
    ''' 発注書管理権限
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKanriKengen() As String
        Get
            Return Integer.Parse(HiddenHattyuusyoKanriKengen.Value)
        End Get
        Set(ByVal value As String)
            HiddenHattyuusyoKanriKengen.Value = value.ToString()
        End Set
    End Property
#End Region

#Region "経理業務権限"
    ''' <summary>
    ''' 経理業務権限
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KeiriGyoumuKengen() As String
        Get
            Return Integer.Parse(HiddenKeiriGyoumuKengen.Value)
        End Get
        Set(ByVal value As String)
            HiddenKeiriGyoumuKengen.Value = value.ToString()
        End Set
    End Property
#End Region

#End Region

#End Region

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '商品4画面情報を取得（ScriptManager用）
        Dim myMaster As PopupSyouhin4 = Page.Page
        masterAjaxSM = myMaster.AjaxScriptManager

        '****************************************************************************
        ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
        '****************************************************************************
        SetDispAction()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 商品マスタポップアップ画面の分類指定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.HiddenTargetId.Value = EarthEnum.EnumSyouhinKubun.Syouhin4

        If Not IsPostBack Then
            '画面読込み時の値をHidden項目に退避
            setOpenValuesUriage()
            setOpenValuesSiire()
        End If

    End Sub

    ''' <summary>
    ''' ページ描画前処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        Dim strViewMode As String       '画面モード

        '経理権限が無い場合は売上・仕入先変更画面は参照モードで開く
        If HiddenKeiriGyoumuKengen.Value <> "-1" Then
            strViewMode = EarthConst.MODE_VIEW
        Else
            strViewMode = EarthConst.MODE_EDIT
        End If

        '請求先/仕入先情報のセット
        Me.ucSeikyuuSiireLink.SetVariableValueCtrlFromParent(Me.AccHdnKameitenCd.Value _
                                                            , Me.TextSyouhinCd.Text _
                                                            , Me.AccHdnJibanTysKaisyaCd.Value _
                                                            , Me.HiddenUriageJyoukyou.Value _
                                                            , _
                                                            , _
                                                            , Me.TextDenpyouUriageNengappi.Text _
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
    ''' 邸別請求レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="recJiban">地盤レコード</param>
    ''' <param name="recTeiSei">邸別請求レコード</param>
    ''' <param name="intKengen">権限(有:1、無:0)</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal recJiban As JibanRecord _
                                    , ByVal recTeiSei As TeibetuSeikyuuRecord _
                                    , Optional ByVal intKengen As Integer = 1 _
                                    )

        '商品コード未設定時、何もセットしない
        If recTeiSei.SyouhinCd Is Nothing Then
            Exit Sub
        ElseIf recTeiSei.SyouhinCd = "" Then
            Exit Sub
        End If

        '******************************************
        '* 邸別請求データを画面コントロールに設定
        '******************************************
        '区分
        Me.HiddenKubun.Value = recTeiSei.Kbn
        '保証書NO
        Me.HiddenBangou.Value = recTeiSei.HosyousyoNo
        '分類コード
        Me.HiddenBunruiCd.Value = recTeiSei.BunruiCd
        '画面表示NO
        Me.HiddenGamenHyoujiNo.Value = recTeiSei.GamenHyoujiNo
        '税区分
        Me.HiddenZeikubun.Value = recTeiSei.ZeiKbn
        '税率
        Me.HiddenZeiritu.Value = recTeiSei.Zeiritu
        '商品コード
        Me.TextSyouhinCd.Text = recTeiSei.SyouhinCd
        Me.HiddenSyouhinCdOld.Value = recTeiSei.SyouhinCd
        '●確定区分
        Me.SelectKakutei.SelectedValue = recTeiSei.KakuteiKbn
        '商品名
        Me.SpanSyouhinMei.InnerHtml = recTeiSei.SyouhinMei
        '工務店請求税抜金額
        Me.TextKoumutenSeikyuuGaku.Text = IIf(recTeiSei.KoumutenSeikyuuGaku = Integer.MinValue, _
                                              0, _
                                              recTeiSei.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1))

        '実請求税抜金額
        Me.TextJituSeikyuuGaku.Text = IIf(recTeiSei.UriGaku = Integer.MinValue, _
                                          0, _
                                          recTeiSei.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1))

        '仕入消費税額
        Me.TextSiireSyouhizeiGaku.Text = recTeiSei.SiireSyouhiZeiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        '消費税額
        Me.TextSyouhizeiGaku.Text = recTeiSei.UriageSyouhiZeiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        '税込金額
        Me.TextZeikomiKingaku.Text = recTeiSei.ZeikomiUriGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        '承諾書金額
        Me.TextSyoudakusyoKingaku.Text = IIf(recTeiSei.SiireGaku = Integer.MinValue, _
                                             0, _
                                            (recTeiSei.SiireGaku).ToString(EarthConst.FORMAT_KINGAKU_1))
        '見積書作成日
        If Not recTeiSei.TysMitsyoSakuseiDate = Nothing Then
            Me.TextMitumorisyoSakuseiDate.Text = IIf(recTeiSei.TysMitsyoSakuseiDate = DateTime.MinValue, _
                                                     "", _
                                                    cl.GetDisplayString(recTeiSei.TysMitsyoSakuseiDate))
        End If

        '売上年月日
        If Not recTeiSei.UriDate = Nothing Then
            Me.TextUriageNengappi.Text = IIf(recTeiSei.UriDate = DateTime.MinValue, _
                                             "", _
                                            cl.GetDisplayString(recTeiSei.UriDate))
        End If

        '伝票売上年月日(修正･参照)
        If Not recTeiSei.DenpyouUriDate = Nothing Then
            Me.TextDenpyouUriageNengappi.Text = IIf(recTeiSei.DenpyouUriDate = DateTime.MinValue, _
                                              "", _
                                              cl.GetDisplayString(recTeiSei.DenpyouUriDate))
            Me.TextDenpyouUriageNengappiDisplay.Text = IIf(recTeiSei.DenpyouUriDate = DateTime.MinValue, _
                                  "", _
                                  cl.GetDisplayString(recTeiSei.DenpyouUriDate))
        End If

        '伝票仕入年月日(修正･参照)
        If Not recTeiSei.DenpyouSiireDate = Nothing Then
            Me.TextDenpyouSiireNengappi.Text = IIf(recTeiSei.DenpyouSiireDate = DateTime.MinValue, _
                                              "", _
                                              cl.GetDisplayString(recTeiSei.DenpyouSiireDate))
            Me.TextDenpyouSiireNengappiDisplay.Text = IIf(recTeiSei.DenpyouSiireDate = DateTime.MinValue, _
                                  "", _
                                  cl.GetDisplayString(recTeiSei.DenpyouSiireDate))
        End If

        '●請求有無
        Me.SelectSeikyuuUmu.SelectedValue = recTeiSei.SeikyuuUmu

        '請求書発行日
        If Not recTeiSei.SeikyuusyoHakDate = Nothing Then
            Me.TextSeikyuusyoHakkouDate.Text = IIf(recTeiSei.SeikyuusyoHakDate = DateTime.MinValue, _
                                                   "", _
                                                   cl.GetDisplayString(recTeiSei.SeikyuusyoHakDate))
        End If

        '●売上処理
        Me.SelectUriageSyori.SelectedValue = recTeiSei.UriKeijyouFlg
        '売上計上FGL
        Me.HiddenUriageJyoukyou.Value = recTeiSei.UriKeijyouFlg
        '売上計上日
        Me.HiddenUriageKeijyouDate.Value = cl.GetDisplayString(recTeiSei.UriKeijyouDate)

        '売上処理日
        If Not recTeiSei.UriKeijyouDate = Nothing Then
            Me.TextUriageDate.Text = IIf(recTeiSei.UriKeijyouDate = DateTime.MinValue, _
                                         "", _
                                         cl.GetDisplayString(recTeiSei.UriKeijyouDate))
        End If

        '発注書金額
        Me.TextHattyuusyoKingaku.Text = IIf(recTeiSei.HattyuusyoGaku = Integer.MinValue, _
                                            "", _
                                            (recTeiSei.HattyuusyoGaku).ToString(EarthConst.FORMAT_KINGAKU_1))
        '●発注書確定
        Me.SelectHattyuusyoKakutei.SelectedValue = recTeiSei.HattyuusyoKakuteiFlg
        '発注書確定FLG
        Me.HiddenHattyuusyoKakuteiOld.Value = recTeiSei.HattyuusyoKakuteiFlg
        Me.HiddenHattyuusyoFlgOld.Value = recTeiSei.HattyuusyoKakuteiFlg

        '発注書確定済みの場合、発注書金額を編集不可に設定
        If SelectHattyuusyoKakutei.SelectedValue = "1" Then
            cl.chgVeiwMode(TextHattyuusyoKingaku)

            ' 発注書確定済みの場合、経理権限が無い場合、発注書確定も非活性化
            If HiddenKeiriGyoumuKengen.Value = "0" Then
                cl.chgVeiwMode(Me.SelectHattyuusyoKakutei, Me.SpanKakutei)
            End If
        End If

        '発注書確認日
        If Not recTeiSei.HattyuusyoKakuninDate = Nothing Then
            Me.TextHattyuusyoKakuninDate.Text = IIf(recTeiSei.HattyuusyoKakuninDate = DateTime.MinValue, _
                                                    "", _
                                                    cl.GetDisplayString(recTeiSei.HattyuusyoKakuninDate))
        End If

        '更新日時
        If recTeiSei.UpdDatetime = DateTime.MinValue Then
            Me.HiddenUpdDatetime.Value = recTeiSei.AddDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)
        Else
            Me.HiddenUpdDatetime.Value = recTeiSei.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)
        End If

        '請求先/仕入先リンクへセット
        Me.ucSeikyuuSiireLink.SetSeikyuuSiireLinkFromTeibetuRec(recTeiSei)

        '******************************************
        '* 商品情報の取得
        '******************************************
        Dim lstSyouhin23 As New List(Of Syouhin23Record)
        Dim recSyouhin23 As New Syouhin23Record
        Dim lgcJiban As New JibanLogic
        Dim intCnt As Integer = 0
        lstSyouhin23 = lgcJiban.GetSyouhin23(recTeiSei.SyouhinCd, _
                                             String.Empty, _
                                             EarthEnum.EnumSyouhinKubun.AllSyouhinTorikesi, _
                                             intCnt, _
                                             Integer.MinValue, _
                                             Me.HiddenKameitenCd.Value)
        If lstSyouhin23.Count > 0 Then
            recSyouhin23 = lstSyouhin23(0)
        End If

        '請求先情報の設定
        GetSeikyuuInfo(recSyouhin23)

        '【権限による活性制御】
        SetEnableControl()

    End Sub

    ''' <summary>
    ''' 画面商品コントロールから邸別請求データに値をセットする（登録/更新処理用）
    ''' </summary>
    ''' <returns>邸別請求レコード</returns>
    ''' <remarks></remarks>
    Public Function setTeibetuToSyouhin() As TeibetuSeikyuuRecord

        '邸別請求レコード
        Dim recTS As New TeibetuSeikyuuRecord

        '画面表示NO
        cl.SetDisplayString(Me.HiddenGamenHyoujiNo.Value, recTS.GamenHyoujiNo)
        '商品コード
        cl.SetDisplayString(Me.TextSyouhinCd.Text, recTS.SyouhinCd)
        '●確定区分
        cl.SetDisplayString(Me.SelectKakutei.SelectedValue, recTS.KakuteiKbn)
        '工務店請求税抜金額
        cl.SetDisplayString(Me.TextKoumutenSeikyuuGaku.Text, recTS.KoumutenSeikyuuGaku)
        '実請求税抜金額（売上金額）
        cl.SetDisplayString(Me.TextJituSeikyuuGaku.Text, recTS.UriGaku)
        '承諾書金額（仕入金額）
        cl.SetDisplayString(Me.TextSyoudakusyoKingaku.Text, recTS.SiireGaku)
        '●税区分
        cl.SetDisplayString(Me.HiddenZeikubun.Value, recTS.ZeiKbn)
        '税率
        cl.SetDisplayString(Me.HiddenZeiritu.Value, recTS.Zeiritu)
        '消費税額
        cl.SetDisplayString(Me.TextSyouhizeiGaku.Text, recTS.UriageSyouhiZeiGaku)
        '仕入消費税額
        cl.SetDisplayString(Me.TextSiireSyouhizeiGaku.Text, recTS.SiireSyouhiZeiGaku)
        '見積書作成日
        cl.SetDisplayString(Me.TextMitumorisyoSakuseiDate.Text, recTS.TysMitsyoSakuseiDate)
        '売上年月日
        cl.SetDisplayString(Me.TextUriageNengappi.Text, recTS.UriDate)
        '伝票売上年月日
        cl.SetDisplayString(Me.TextDenpyouUriageNengappi.Text, recTS.DenpyouUriDate)
        '伝票仕入年月日
        cl.SetDisplayString(Me.TextDenpyouSiireNengappi.Text, recTS.DenpyouSiireDate)
        '●請求有無
        cl.SetDisplayString(Me.SelectSeikyuuUmu.SelectedValue, recTS.SeikyuuUmu)
        '請求書発行日
        cl.SetDisplayString(Me.TextSeikyuusyoHakkouDate.Text, recTS.SeikyuusyoHakDate)
        '●売上処理
        cl.SetDisplayString(Me.SelectUriageSyori.SelectedValue, recTS.UriKeijyouFlg)
        '売上処理日
        cl.SetDisplayString(Me.TextUriageDate.Text, recTS.UriKeijyouDate)
        '発注書金額
        cl.SetDisplayString(Me.TextHattyuusyoKingaku.Text, recTS.HattyuusyoGaku)
        '●発注書確定
        cl.SetDisplayString(Me.SelectHattyuusyoKakutei.SelectedValue, recTS.HattyuusyoKakuteiFlg)
        '発注書確認日
        cl.SetDisplayString(Me.TextHattyuusyoKakuninDate.Text, recTS.HattyuusyoKakuninDate)

        '請求先/仕入先リンクから取得
        Me.ucSeikyuuSiireLink.SetTeibetuRecFromSeikyuuSiireLink(recTS)

        '更新日時
        If Me.HiddenUpdDatetime.Value <> String.Empty Then
            recTS.UpdDatetime = DateTime.Parse(Me.HiddenUpdDatetime.Value)
        Else
            recTS.UpdDatetime = DateTime.MinValue
        End If
        '更新者ID
        recTS.UpdLoginUserId = Me.HiddenLoginUserId.Value

        Return recTS
    End Function

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

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 商品コードおよびボタン
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSyouhinCd.Attributes("onfocus") = "setTempValueForOnBlur(this);"
        Me.TextSyouhinCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)) callSetSyouhin4(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 金額系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '工務店請求税抜金額
        Me.TextKoumutenSeikyuuGaku.Attributes("onfocus") = onFocusPostBackScript
        Me.TextKoumutenSeikyuuGaku.Attributes("onblur") = onBlurPostBackScript
        Me.TextKoumutenSeikyuuGaku.Attributes("onkeydown") = disabledOnkeydown
        '実請求税抜金額
        Me.TextJituSeikyuuGaku.Attributes("onfocus") = onFocusPostBackScript
        Me.TextJituSeikyuuGaku.Attributes("onblur") = onBlurPostBackScript
        Me.TextJituSeikyuuGaku.Attributes("onkeydown") = disabledOnkeydown
        '消費税額
        Me.TextSyouhizeiGaku.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSyouhizeiGaku.Attributes("onblur") = onBlurPostBackScript
        Me.TextSyouhizeiGaku.Attributes("onkeydown") = disabledOnkeydown
        '仕入消費税額
        Me.TextSiireSyouhizeiGaku.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSiireSyouhizeiGaku.Attributes("onblur") = onBlurPostBackScript
        Me.TextSiireSyouhizeiGaku.Attributes("onkeydown") = disabledOnkeydown
        '承諾書金額
        Me.TextSyoudakusyoKingaku.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSyoudakusyoKingaku.Attributes("onblur") = onBlurPostBackScript
        Me.TextSyoudakusyoKingaku.Attributes("onkeydown") = disabledOnkeydown
        '発注書金額
        Me.TextHattyuusyoKingaku.Attributes("onfocus") = onFocusPostBackScript
        Me.TextHattyuusyoKingaku.Attributes("onblur") = onBlurPostBackScript
        Me.TextHattyuusyoKingaku.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 日付系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '見積書作成日
        Me.TextMitumorisyoSakuseiDate.Attributes("onblur") = checkDate
        Me.TextMitumorisyoSakuseiDate.Attributes("onkeydown") = disabledOnkeydown
        '売上年月日
        Me.TextUriageNengappi.Attributes("onblur") = onBlurPostBackScriptDate
        Me.TextUriageNengappi.Attributes("onfocus") = onFocusPostBackScriptDate
        Me.TextUriageNengappi.Attributes("onkeydown") = disabledOnkeydown
        '伝票売上年月日
        Me.TextDenpyouUriageNengappi.Attributes("onblur") = onBlurPostBackScriptDate
        Me.TextDenpyouUriageNengappi.Attributes("onfocus") = onFocusPostBackScriptDate
        Me.TextDenpyouUriageNengappi.Attributes("onkeydown") = disabledOnkeydown
        '伝票仕入年月日
        Me.TextDenpyouSiireNengappi.Attributes("onblur") = checkDate
        Me.TextDenpyouSiireNengappi.Attributes("onkeydown") = disabledOnkeydown
        '請求書発行日
        Me.TextSeikyuusyoHakkouDate.Attributes("onblur") = checkDate
        Me.TextSeikyuusyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown
        '発注書確認日
        Me.TextHattyuusyoKakuninDate.Attributes("onblur") = checkDate
        Me.TextHattyuusyoKakuninDate.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ タブインデックス
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '仕入消費税額
        Me.TextSiireSyouhizeiGaku.TabIndex = "-1"
        '売上年月日
        Me.TextUriageNengappi.TabIndex = "-1"
        '売上処理
        Me.SelectUriageSyori.TabIndex = "-1"
        '請求有無
        Me.SelectSeikyuuUmu.TabIndex = "-1"


    End Sub

    ''' <summary>
    ''' 請求情報の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetSeikyuuInfo(ByVal recSyouhin As Syouhin23Record)

        Dim lgcKameiten As New KameitenSearchLogic
        Dim recKameiten As New KameitenSearchRecord
        Dim intKeiretuFlg As Integer
        Dim strSeikyuuType As String = String.Empty
        Dim lgcJiban As New JibanLogic

        'デフォルト値（他請求・3系列以外）
        HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu

        '加盟店データの取得
        recKameiten = lgcKameiten.GetKameitenSearchResult(Me.HiddenKubun.Value, Me.HiddenKameitenCd.Value, String.Empty)

        '加盟店が取得出来ない場合には、他請求・3系列以外扱い
        If recKameiten.KameitenCd Is Nothing Then
            Exit Sub
        End If

        '請求先が個別に指定されている場合、デフォルトの請求先を上書き
        If Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value <> String.Empty Then
            recSyouhin.SeikyuuSakiCd = Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value
            recSyouhin.SeikyuuSakiBrc = Me.ucSeikyuuSiireLink.AccSeikyuuSakiBrc.Value
            recSyouhin.SeikyuuSakiKbn = Me.ucSeikyuuSiireLink.AccSeikyuuSakiKbn.Value
        End If

        '系列コードの設定
        Me.HiddenKeiretu.Value = recKameiten.KeiretuCd

        '3系列の判定(1:3系列、0:3系列以外)
        intKeiretuFlg = lgcJiban.GetKeiretuFlg(recKameiten.KeiretuCd)

        '商品が無い場合には系列のみ判断（デフォルト他請求）
        If String.IsNullOrEmpty(recSyouhin.SyouhinCd) Then
            If intKeiretuFlg = 1 Then
                '他請求・3系列
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuKeiretu
            Else
                '他請求・3系列以外
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuNotKeiretu
            End If
            Exit Sub
        End If

        '加盟店M・商品Mによる請求タイプの判定
        If recSyouhin.SeikyuuSakiType = EarthConst.SEIKYU_TYOKUSETU Then
            '直接請求
            If intKeiretuFlg = 1 Then
                '3系列
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu
            Else
                '3系列以外
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu
            End If
        ElseIf recSyouhin.SeikyuuSakiType = EarthConst.SEIKYU_TASETU Then
            '他請求
            If intKeiretuFlg = 1 Then
                '3系列
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuKeiretu
            Else
                '3系列以外
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuNotKeiretu
            End If
        End If


    End Sub

#Region "画面制御"

    ''' <summary>
    ''' コントロールの画面制御/商品４（受注から来た時用）
    ''' </summary>
    ''' <remarks>コントロールの画面制御を仕様書の優先順に記載する</remarks>
    Public Sub SetEnableControl()
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable '対象外

        jSM.Hash2Ctrl(TrSyouhin4Record, EarthConst.MODE_VIEW, ht, htNotTarget) 'Tr商品４レコード

        '●優先順2
        '売上処理済(邸別請求テーブル.売上計上FLG＝1)
        If Not String.IsNullOrEmpty(Me.HiddenUriageKeijyouDate.Value) Then
        Else
            '●優先順3
            '商品コード＝空白
            If Me.TextSyouhinCd.Text = String.Empty Then
                cl.chgDispSyouhinText(Me.TextSyouhinCd) '商品コード

                '●優先順4
                '商品コード＝入力
            Else
                cl.chgDispSyouhinText(Me.TextSyouhinCd) '商品コード

                cl.chgDispSyouhinPull(Me.SelectKakutei, Me.SpanKakutei)                     '確定状況
                cl.chgDispSyouhinText(Me.TextSyoudakusyoKingaku)                            '承諾書金額
                cl.chgDispSyouhinText(Me.TextSyouhizeiGaku)                                 '消費税額
                cl.chgDispSyouhinText(Me.TextSiireSyouhizeiGaku)                            '仕入消費税額
                cl.chgDispSyouhinPull(Me.SelectSeikyuuUmu, Me.SpanSeikyuuUmu)               '請求有無
                cl.chgDispSyouhinPull(Me.SelectHattyuusyoKakutei, Me.SpanHattyuusyoKakutei) '発注書確定

                '****************
                cl.chgDispSyouhinText(Me.TextMitumorisyoSakuseiDate)                        '見積書作成日
                cl.chgDispSyouhinText(Me.TextUriageNengappi)                                '売上年月日
                cl.chgDispSyouhinText(Me.TextDenpyouUriageNengappi)                         '伝票売上年月日
                cl.chgDispSyouhinText(Me.TextDenpyouSiireNengappi)                          '伝票仕入年月日
                cl.chgDispSyouhinText(Me.TextSeikyuusyoHakkouDate)                          '請求書発行日
                cl.chgDispSyouhinPull(Me.SelectUriageSyori, Me.SpanUriageSyori)             '売上処理
                cl.chgDispSyouhinText(Me.TextHattyuusyoKakuninDate)                         '発注書確認日
                '****************

                '●優先順5
                '請求有無=有り、かつ、請求先=他請求、かつ、系列<>3系列
                If Me.SelectSeikyuuUmu.SelectedValue = "1" Then '有り
                    '他請求 and 3系列以外
                    If Me.HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuNotKeiretu Then
                        cl.chgDispSyouhinText(Me.TextJituSeikyuuGaku) '実請求金額

                        '●優先順6
                        '請求有無=有り、かつ、優先順5以外
                    Else
                        cl.chgDispSyouhinText(Me.TextKoumutenSeikyuuGaku) '工務店請求金額
                        cl.chgDispSyouhinText(Me.TextJituSeikyuuGaku) '実請求金額
                    End If

                End If

            End If

        End If

        '●優先順1
        '発注書確定(邸別請求テーブル.発注書確定FLG＝1)
        If Me.HiddenHattyuusyoKakuteiOld.Value = "1" Then '確定
            '発注書確定
            cl.chgVeiwMode(SelectHattyuusyoKakutei, SpanHattyuusyoKakutei)
        Else '未確定
            '商品コード
            If Me.TextSyouhinCd.Text = String.Empty Then '未入力
            Else '入力
                cl.chgDispSyouhinPull(Me.SelectHattyuusyoKakutei, Me.SpanHattyuusyoKakutei) '発注書確定

                '●優先順7
                '請求有無
                If Me.SelectSeikyuuUmu.SelectedValue = "0" Then '無し
                Else
                    '●優先順8
                    '発注書確定=1:確定
                    If Me.SelectHattyuusyoKakutei.SelectedValue = "1" Then

                        '●優先順9
                        '優先順8以外
                    Else
                        cl.chgDispSyouhinText(Me.TextHattyuusyoKingaku) '発注書金額

                    End If
                End If
            End If
        End If

        '**********************************
        '権限による表示・ラベル化処理を実行
        '**********************************
        SetEnableControlKengen()


    End Sub

    ''' <summary>
    ''' 権限による画面制御
    ''' </summary>
    ''' <remarks>経理業務・依頼業務・発注書管理権限による制御を行う</remarks>
    Public Sub SetEnableControlKengen()

        '各権限の取得
        Dim Keiri As Integer = Me.HiddenKeiriGyoumuKengen.Value
        Dim Irai As Integer = Me.HiddenIraiGyoumuKengen.Value
        Dim Hattyuu As Integer = Me.HiddenHattyuusyoKanriKengen.Value

        ''経理権限が無い場合は売上処理・伝票売上年月日等がラベル化
        'If HiddenKeiriGyoumuKengen.Value <> "-1" Then
        '    cl.chgVeiwMode(TextUriageNengappi)                              '売上年月日
        '    cl.chgVeiwMode(TextDenpyouUriageNengappi)                       '伝票売上年月日
        '    cl.chgVeiwMode(TextDenpyouSiireNengappi)                        '伝票仕入年月日
        '    cl.chgVeiwMode(SelectUriageSyori, SpanUriageSyori)              '売上処理
        '    cl.chgVeiwMode(TextSyouhizeiGaku)                               '消費税額
        '    cl.chgVeiwMode(TextSiireSyouhizeiGaku)                          '消費税額
        '    cl.chgVeiwMode(TextMitumorisyoSakuseiDate)                      '見積書作成日
        '    cl.chgVeiwMode(TextSeikyuusyoHakkouDate)                        '請求書発行日
        '    cl.chgVeiwMode(TextHattyuusyoKakuninDate)                       '発注書確認日
        'ElseIf HiddenKeiriGyoumuKengen.Value = "-1" Then
        '    '経理権限がある場合には全て使用可
        '    cl.chgDispSyouhinText(TextSyouhinCd)
        '    cl.chgDispSyouhinPull(SelectKakutei, SpanKakutei)
        '    cl.chgDispSyouhinText(TextKoumutenSeikyuuGaku)
        '    cl.chgDispSyouhinText(TextJituSeikyuuGaku)
        '    cl.chgDispSyouhinText(TextSyouhizeiGaku)
        '    cl.chgDispSyouhinText(TextSiireSyouhizeiGaku)
        '    cl.chgDispSyouhinText(TextSyoudakusyoKingaku)
        '    cl.chgDispSyouhinText(TextMitumorisyoSakuseiDate)
        '    cl.chgDispSyouhinText(TextUriageNengappi)
        '    cl.chgDispSyouhinText(TextDenpyouUriageNengappi)
        '    cl.chgDispSyouhinText(TextDenpyouSiireNengappi)
        '    cl.chgDispSyouhinPull(SelectSeikyuuUmu, SpanSeikyuuUmu)
        '    cl.chgDispSyouhinText(TextSeikyuusyoHakkouDate)
        '    cl.chgDispSyouhinPull(SelectUriageSyori, SpanUriageSyori)
        '    '発注書関連も使用可
        '    cl.chgDispSyouhinText(TextHattyuusyoKingaku)
        '    cl.chgDispSyouhinPull(SelectHattyuusyoKakutei, SpanHattyuusyoKakutei)
        '    cl.chgDispSyouhinText(TextHattyuusyoKakuninDate)
        'End If

        ''経理権限及び依頼業務権限が無い場合、発注書以外ラベル化
        'If (HiddenKeiriGyoumuKengen.Value <> "-1") And _
        '    (HiddenIraiGyoumuKengen.Value <> "-1") Then
        '    cl.chgVeiwMode(TextUriageNengappi)                              '売上年月日
        '    cl.chgVeiwMode(TextDenpyouUriageNengappi)                       '伝票売上年月日
        '    cl.chgVeiwMode(TextDenpyouSiireNengappi)                        '伝票仕入年月日
        '    cl.chgVeiwMode(SelectUriageSyori, SpanUriageSyori)              '売上処理
        '    cl.chgVeiwMode(SelectKakutei, SpanKakutei)                      '確定区分
        '    cl.chgVeiwMode(TextKoumutenSeikyuuGaku)                         '工務店請求金額
        '    cl.chgVeiwMode(TextJituSeikyuuGaku)                             '実請求税抜金額
        '    cl.chgVeiwMode(TextSyouhizeiGaku)                               '消費税額
        '    cl.chgVeiwMode(TextSiireSyouhizeiGaku)                          '仕入消費税額
        '    cl.chgVeiwMode(TextSyoudakusyoKingaku)                          '承諾書金額
        '    cl.chgVeiwMode(TextMitumorisyoSakuseiDate)                      '見積書作成日
        '    cl.chgVeiwMode(SelectSeikyuuUmu, SpanSeikyuuUmu)                '請求有無
        '    cl.chgVeiwMode(TextSeikyuusyoHakkouDate)                        '請求書発行日
        '    cl.chgVeiwMode(TextHattyuusyoKakuninDate)                       '発注書確認日
        '    cl.chgVeiwMode(TextSyouhinCd)                                   '商品コード
        '    Me.ButtonSyouhinKensaku.Visible = False                         '商品検索ボタン
        'End If

        ''経理権限・依頼権限・発注書権限が無い場合全てラベル化
        'If (HiddenKeiriGyoumuKengen.Value <> "-1") And _
        '    (HiddenIraiGyoumuKengen.Value <> "-1") And _
        '    (HiddenHattyuusyoKanriKengen.Value <> "-1") Then
        '    cl.chgVeiwMode(TextUriageNengappi)                              '売上年月日
        '    cl.chgVeiwMode(TextDenpyouUriageNengappi)                       '伝票売上年月日
        '    cl.chgVeiwMode(TextDenpyouSiireNengappi)                        '伝票仕入年月日
        '    cl.chgVeiwMode(SelectUriageSyori, SpanUriageSyori)              '売上処理
        '    cl.chgVeiwMode(SelectKakutei, SpanKakutei)                      '確定区分
        '    cl.chgVeiwMode(TextKoumutenSeikyuuGaku)                         '工務店請求金額
        '    cl.chgVeiwMode(TextJituSeikyuuGaku)                             '実請求税抜金額
        '    cl.chgVeiwMode(TextSyouhizeiGaku)                               '消費税額
        '    cl.chgVeiwMode(TextSiireSyouhizeiGaku)                          '仕入消費税額
        '    cl.chgVeiwMode(TextSyoudakusyoKingaku)                          '承諾書金額
        '    cl.chgVeiwMode(TextMitumorisyoSakuseiDate)                      '見積書作成日
        '    cl.chgVeiwMode(SelectSeikyuuUmu, SpanSeikyuuUmu)                '請求有無
        '    cl.chgVeiwMode(TextSeikyuusyoHakkouDate)                        '請求書発行日
        '    cl.chgVeiwMode(TextHattyuusyoKakuninDate)                       '発注書確認日
        '    cl.chgVeiwMode(TextSyouhinCd)                                   '商品コード
        '    Me.ButtonSyouhinKensaku.Visible = False                         '商品検索ボタン
        '    cl.chgVeiwMode(TextHattyuusyoKingaku)                           '発注書金額
        '    cl.chgVeiwMode(SelectHattyuusyoKakutei, SpanHattyuusyoKakutei)  '発注書確定
        'End If

        If HiddenKeiriGyoumuKengen.Value = "-1" Then
            '経理権限がある場合には全て使用可
            cl.chgDispSyouhinText(TextSyouhinCd)
            cl.chgDispSyouhinPull(SelectKakutei, SpanKakutei)
            cl.chgDispSyouhinText(TextKoumutenSeikyuuGaku)
            cl.chgDispSyouhinText(TextJituSeikyuuGaku)
            cl.chgDispSyouhinText(TextSyouhizeiGaku)
            cl.chgDispSyouhinText(TextSiireSyouhizeiGaku)
            cl.chgDispSyouhinText(TextSyoudakusyoKingaku)
            cl.chgDispSyouhinText(TextMitumorisyoSakuseiDate)
            cl.chgDispSyouhinText(TextUriageNengappi)
            cl.chgDispSyouhinText(TextDenpyouUriageNengappi)
            cl.chgDispSyouhinText(TextDenpyouSiireNengappi)
            cl.chgDispSyouhinPull(SelectSeikyuuUmu, SpanSeikyuuUmu)
            cl.chgDispSyouhinText(TextSeikyuusyoHakkouDate)
            cl.chgDispSyouhinPull(SelectUriageSyori, SpanUriageSyori)
            '発注書関連も使用可
            cl.chgDispSyouhinText(TextHattyuusyoKingaku)
            cl.chgDispSyouhinPull(SelectHattyuusyoKakutei, SpanHattyuusyoKakutei)
            cl.chgDispSyouhinText(TextHattyuusyoKakuninDate)
        Else
            '経理権限が無い場合には全てラベル化
            cl.chgVeiwMode(TextUriageNengappi)                              '売上年月日
            cl.chgVeiwMode(TextDenpyouUriageNengappi)                       '伝票売上年月日
            cl.chgVeiwMode(TextDenpyouSiireNengappi)                        '伝票仕入年月日
            cl.chgVeiwMode(SelectUriageSyori, SpanUriageSyori)              '売上処理
            cl.chgVeiwMode(SelectKakutei, SpanKakutei)                      '確定区分
            cl.chgVeiwMode(TextKoumutenSeikyuuGaku)                         '工務店請求金額
            cl.chgVeiwMode(TextJituSeikyuuGaku)                             '実請求税抜金額
            cl.chgVeiwMode(TextSyouhizeiGaku)                               '消費税額
            cl.chgVeiwMode(TextSiireSyouhizeiGaku)                          '仕入消費税額
            cl.chgVeiwMode(TextSyoudakusyoKingaku)                          '承諾書金額
            cl.chgVeiwMode(TextMitumorisyoSakuseiDate)                      '見積書作成日
            cl.chgVeiwMode(SelectSeikyuuUmu, SpanSeikyuuUmu)                '請求有無
            cl.chgVeiwMode(TextSeikyuusyoHakkouDate)                        '請求書発行日
            cl.chgVeiwMode(TextHattyuusyoKakuninDate)                       '発注書確認日
            cl.chgVeiwMode(TextSyouhinCd)                                   '商品コード
            Me.ButtonSyouhinKensaku.Visible = False                         '商品検索ボタン
            cl.chgVeiwMode(TextHattyuusyoKingaku)                           '発注書金額
            cl.chgVeiwMode(SelectHattyuusyoKakutei, SpanHattyuusyoKakutei)  '発注書確定
            cl.chgVeiwMode(TextHattyuusyoKakuninDate)                       '発注書確認日
        End If

    End Sub

#End Region

#Region "コントロールの活性制御"
    ''' <summary>
    ''' コントロールの初期化
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub initCtrl(ByVal enabled As Boolean)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".initCtrl")

        If enabled = False Then
            '[ラベル化]
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable '対象外
            htNotTarget.Add(Me.TextSyouhinCd.ID, Me.TextSyouhinCd)  '商品コード
            jSM.Hash2Ctrl(TrSyouhin4Record, EarthConst.MODE_VIEW, ht, htNotTarget) 'Tr商品４レコード

            '[データのクリア]
            Me.TextSyouhinCd.Text = String.Empty                '商品コード
            Me.SpanKakutei.InnerHtml = String.Empty             '確定区分
            Me.SpanSyouhinMei.InnerText = String.Empty          '商品名
            Me.TextKoumutenSeikyuuGaku.Text = String.Empty      '工務店請求税抜金額
            Me.TextJituSeikyuuGaku.Text = String.Empty          '実請求税抜金額
            Me.TextSyouhizeiGaku.Text = String.Empty            '消費税額
            Me.TextSiireSyouhizeiGaku.Text = String.Empty       '仕入消費税額
            Me.TextZeikomiKingaku.Text = String.Empty           '税込金額
            Me.TextSyoudakusyoKingaku.Text = String.Empty       '承諾書金額
            Me.TextMitumorisyoSakuseiDate.Text = String.Empty   '見積書作成日
            Me.TextUriageNengappi.Text = String.Empty           '売上年月日
            Me.TextDenpyouUriageNengappiDisplay.Text = String.Empty     '伝票売上年月日(参照)
            Me.TextDenpyouUriageNengappi.Text = String.Empty    '伝票売上年月日(修正)
            Me.TextDenpyouSiireNengappiDisplay.Text = String.Empty      '伝票仕入年月日(参照)
            Me.TextDenpyouSiireNengappi.Text = String.Empty     '伝票仕入年月日(修正)
            Me.SpanSeikyuuUmu.InnerHtml = String.Empty          '請求有無
            Me.TextSeikyuusyoHakkouDate.Text = String.Empty     '請求書発効日
            Me.SpanUriageSyori.InnerHtml = String.Empty         '売上処理
            Me.TextUriageDate.Text = String.Empty               '売上処理日
            Me.TextHattyuusyoKingaku.Text = String.Empty        '発注書金額
            Me.SpanHattyuusyoKakutei.InnerHtml = String.Empty   '発注書確定
            Me.TextHattyuusyoKakuninDate.Text = String.Empty    '発注書確認日
            Me.HiddenHattyuusyoKakuteiOld.Value = String.Empty  '発注書確定FLG

        Else
            cl.chgDispSyouhinText(Me.TextKoumutenSeikyuuGaku)                           '工務店請求金額
            cl.chgDispSyouhinText(Me.TextJituSeikyuuGaku)                               '実請求税抜金額
            cl.chgDispSyouhinText(Me.TextSyouhizeiGaku)                                 '消費税額
            cl.chgDispSyouhinText(Me.TextSiireSyouhizeiGaku)                            '仕入消費税額
            cl.chgDispSyouhinText(Me.TextSyoudakusyoKingaku)                            '承諾書金額
            cl.chgDispSyouhinText(Me.TextMitumorisyoSakuseiDate)                        '見積書作成日
            cl.chgDispSyouhinText(Me.TextUriageNengappi)                                '売上年月日
            cl.chgDispSyouhinText(Me.TextDenpyouUriageNengappi)                         '伝票売上年月日
            cl.chgDispSyouhinText(Me.TextDenpyouSiireNengappi)                          '伝票仕入年月日
            cl.chgDispSyouhinText(Me.TextSeikyuusyoHakkouDate)                          '請求書発行日
            cl.chgDispSyouhinText(Me.TextHattyuusyoKingaku)                             '発注書金額
            cl.chgDispSyouhinText(Me.TextHattyuusyoKakuninDate)                         '発注書確認日
            cl.chgDispSyouhinPull(Me.SelectKakutei, Me.SpanKakutei)                     '確定状況
            cl.chgDispSyouhinPull(Me.SelectSeikyuuUmu, Me.SpanSeikyuuUmu)               '請求有無
            cl.chgDispSyouhinPull(Me.SelectUriageSyori, Me.SpanUriageSyori)             '売上処理
            cl.chgDispSyouhinPull(Me.SelectHattyuusyoKakutei, Me.SpanHattyuusyoKakutei) '発注書確定
        End If

    End Sub

#End Region

#Region "商品情報系"

    ''' <summary>
    ''' 商品検索ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSyouhinKensaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSyouhinKensaku.ServerClick

        If HiddenSyouhin4SearchType.Value <> "1" Then
            TextSyouhinCd_ChangeSub(sender, e)
        Else
            'コードonchangeで呼ばれた場合
            TextSyouhinCd_ChangeSub(sender, e, False)
            HiddenSyouhin4SearchType.Value = String.Empty
        End If


    End Sub

    ''' <summary>
    ''' 商品検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TextSyouhinCd_ChangeSub(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs, _
                                Optional ByVal ProcType As Boolean = True)

        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim SyouhinSearchLogic As New SyouhinSearchLogic
        Dim total_row As Integer

        ' モードの取得
        'Dim mode As Integer = Integer.Parse(HiddenDispMode.Value)

        '商品検索を実行
        Dim dataArray As List(Of SyouhinMeisaiRecord) = SyouhinSearchLogic.GetSyouhinInfo(TextSyouhinCd.Text, _
                                                                                      "", _
                                                                                      EarthEnum.EnumSyouhinKubun.Syouhin4, _
                                                                                      total_row)
        If dataArray.Count = 1 Then
            '商品情報を画面にセット
            Dim recData As SyouhinMeisaiRecord = dataArray(0)

            'フォーカスセット
            masterAjaxSM.SetFocus(ButtonSyouhinKensaku)
        ElseIf ProcType = True Then
            '検索ポップアップを起動

            Dim tmpFocusScript = "objEBI('" & ButtonSyouhinKensaku.ClientID & "').focus();"

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpScript = "callSearch('" & TextSyouhinCd.ClientID & EarthConst.SEP_STRING & _
                                        HiddenTargetId.ClientID & _
                                        "','" & UrlConst.SEARCH_SYOUHIN & "','" & _
                                        TextSyouhinCd.ClientID & "','" & _
                                        ButtonSyouhinKensaku.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)

            Exit Sub

        End If

        '商品情報を画面に設定
        SetSyouhin(sender, e, ProcType)

        ' 商品コードを保存
        HiddenSyouhinCdOld.Value = TextSyouhinCd.Text

    End Sub

    ''' <summary>
    ''' 商品情報の設定（商品コードが確定している状態でCallする）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="ProcType"></param>
    ''' <remarks></remarks>
    Public Sub SetSyouhin(ByVal sender As Object, ByVal e As System.EventArgs, _
                                Optional ByVal ProcType As Boolean = True)

        Dim intCntTotal As Integer
        Dim logic As New JibanLogic
        Dim lgcSyouhin As New SyouhinSearchLogic
        Dim lstSyouhin23 As New List(Of Syouhin23Record)
        Dim recSyouhin23 As New Syouhin23Record
        Dim lgcJiban As New JibanLogic
        Dim strZeiKbn As String = String.Empty              '税区分
        Dim strZeiritu As String = String.Empty             '税率
        Dim strSyouhinCd As String = String.Empty           '商品コード
        ' 取得用ロジッククラス
        Dim cBizLogic As New CommonBizLogic

        If Trim(Me.TextSyouhinCd.Text) = String.Empty Then

            '発注書金額の取得
            Dim strHattyuuGaku As String = IIf(Me.TextHattyuusyoKingaku.Text = String.Empty, "0", Me.TextHattyuusyoKingaku.Text)

            If strHattyuuGaku <> "0" Then
                Me.TextSyouhinCd.Text = Me.HiddenSyouhinCdOld.Value
                'クリア出来ないMSG
                ScriptManager.RegisterClientScriptBlock(sender, _
                                        sender.GetType(), _
                                        "alert", _
                                        "alert('" & _
                                        Messages.MSG010E & _
                                        "')", True)
                setFocusAJ(Me.TextSyouhinCd)
                Exit Sub
            End If

            'コントロールの初期化(非活性化）
            initCtrl(False)
            setFocusAJ(Me.ButtonSyouhinKensaku)
            Exit Sub

        End If

        '商品情報の取得
        lstSyouhin23 = logic.GetSyouhin23(Me.TextSyouhinCd.Text, _
                                        String.Empty, _
                                        EarthEnum.EnumSyouhinKubun.Syouhin4, _
                                        intCntTotal, _
                                        Integer.MinValue, _
                                        Me.HiddenKameitenCd.Value)
        If lstSyouhin23.Count > 0 Then
            recSyouhin23 = lstSyouhin23(0)
        Else
            initCtrl(False)
            Exit Sub
        End If

        '請求先情報の設定
        GetSeikyuuInfo(recSyouhin23)

        '売上年月日で判断して、正しい税率を取得する
        strSyouhinCd = Me.TextSyouhinCd.Text
        If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '取得した税区分・税率をセット
            recSyouhin23.Zeiritu = strZeiritu
            recSyouhin23.ZeiKbn = strZeiKbn
        End If

        'コード・名称をセット
        Me.TextSyouhinCd.Text = recSyouhin23.SyouhinCd        '商品コード
        Me.HiddenBunruiCd.Value = recSyouhin23.SoukoCd        '分類コード（商品4）
        Me.SpanSyouhinMei.InnerHtml = recSyouhin23.SyouhinMei '商品名
        Me.HiddenZeiritu.Value = recSyouhin23.Zeiritu         '税率
        Me.HiddenZeikubun.Value = recSyouhin23.ZeiKbn         '税区分

        '邸別請求情報の取得
        ' 情報取得用のパラメータクラス
        Dim syouhin23_info As New Syouhin23InfoRecord
        syouhin23_info.Syouhin2Rec = New Syouhin23Record

        '商品・画面情報をレコードにセット
        syouhin23_info.Syouhin2Rec.SyouhinCd = recSyouhin23.SyouhinCd                                 '商品コード
        syouhin23_info.Syouhin2Rec.ZeiKbn = recSyouhin23.ZeiKbn                                       '税区分
        syouhin23_info.Syouhin2Rec.SoukoCd = recSyouhin23.SoukoCd                                     '倉庫コード
        If Me.HiddenSyouhinCdOld.Value <> recSyouhin23.SyouhinCd Then ' 後続で使用しないので請求先／仕入先変更時はそのまま
            syouhin23_info.Syouhin2Rec.SiireKkk = recSyouhin23.SiireKkk                                   '仕入価格(承諾書金額)
        Else
            recSyouhin23.SiireKkk = TextSyoudakusyoKingaku.Text
            syouhin23_info.Syouhin2Rec.SiireKkk = recSyouhin23.SiireKkk
        End If
        syouhin23_info.Syouhin2Rec.HyoujunKkk = recSyouhin23.HyoujunKkk                               '標準価格
        syouhin23_info.Syouhin2Rec.SiireKkk = recSyouhin23.SiireKkk                                   '仕入価格
        syouhin23_info.SeikyuuUmu = SelectSeikyuuUmu.SelectedValue                                  '請求有無
        syouhin23_info.HattyuusyoKakuteiFlg = Integer.Parse(SelectHattyuusyoKakutei.SelectedValue)  '発注書確定フラグ
        syouhin23_info.KeiretuCd = Me.HiddenKeiretu.Value                                        '系列コード
        syouhin23_info.KeiretuFlg = lgcJiban.GetKeiretuFlg(Me.HiddenKeiretu.Value)                        '系列フラグ(3系列or以外)
        If Me.HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
            Me.HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then
            syouhin23_info.Seikyuusaki = EarthConst.SEIKYU_TYOKUSETU                                '請求タイプ(直接or他)
        Else
            syouhin23_info.Seikyuusaki = EarthConst.SEIKYU_TASETU
        End If

        ' 請求レコードの取得(確実に結果が有る)
        Dim recTeiSei As TeibetuSeikyuuRecord = logic.GetSyouhin23SeikyuuData(sender, syouhin23_info, 1)

        '価格情報をセット
        TextKoumutenSeikyuuGaku.Text = recTeiSei.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        TextJituSeikyuuGaku.Text = recTeiSei.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        TextSyouhizeiGaku.Text = (Fix(recTeiSei.UriGaku * recSyouhin23.Zeiritu)).ToString(EarthConst.FORMAT_KINGAKU_1)
        TextZeikomiKingaku.Text = recTeiSei.UriGaku + Fix(recTeiSei.UriGaku * recSyouhin23.Zeiritu).ToString(EarthConst.FORMAT_KINGAKU_1)
        TextSyoudakusyoKingaku.Text = recSyouhin23.SiireKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

        ' 税金額再設定
        SetZeigaku(e)

        ' 仕入消費税額設定
        SetSiireZeigaku()

        ' 請求書発行日・売上年月日の設定
        If Me.HiddenKeiriGyoumuKengen.Value <> "-1" Then
            setHakkoubi()
        End If

        '【権限による活性制御】
        SetEnableControl()

        '新規行追加時に画面の動きを設定
        SetDispAction()

    End Sub

#End Region

#Region "ドロップダウンリスト系"

    ''' <summary>
    ''' 確定ドロップダウンリスト変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKakutei_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' 請求書発行日、売上年月日を設定します
        If Me.HiddenKeiriGyoumuKengen.Value <> "-1" Then
            setHakkoubi()
        End If

        setFocusAJ(SelectKakutei)

    End Sub

    ''' <summary>
    ''' 請求有無ドロップダウンリスト変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectSeikyuuUmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If SelectSeikyuuUmu.SelectedValue = "1" Then

            '商品情報の取得
            Dim lstSyouhin23 As New List(Of Syouhin23Record)
            Dim recSyouhin23 As New Syouhin23Record
            Dim intCnt As Integer = 0
            Dim logic As New JibanLogic

            lstSyouhin23 = logic.GetSyouhin23(HiddenSyouhinCdOld.Value, _
                                              String.Empty, _
                                              EarthEnum.EnumSyouhinKubun.Syouhin4, _
                                              intCnt, _
                                              Integer.MinValue, _
                                              Me.HiddenKameitenCd.Value)
            If lstSyouhin23.Count > 0 Then
                recSyouhin23 = lstSyouhin23(0)
            Else
                initCtrl(False)
                Exit Sub
            End If

            '請求先情報の設定
            GetSeikyuuInfo(recSyouhin23)

            '邸別請求情報の取得
            ' 情報取得用のパラメータクラス
            Dim syouhin23_info As New Syouhin23InfoRecord
            syouhin23_info.Syouhin2Rec = New Syouhin23Record

            'コントロールの設定（活性化）
            initCtrl(True)

            '商品・画面情報をレコードにセット
            syouhin23_info.Syouhin2Rec.SyouhinCd = recSyouhin23.SyouhinCd                                 '商品コード
            syouhin23_info.Syouhin2Rec.ZeiKbn = recSyouhin23.ZeiKbn                                       '税区分
            syouhin23_info.Syouhin2Rec.SoukoCd = recSyouhin23.SoukoCd                                     '倉庫コード
            syouhin23_info.Syouhin2Rec.HyoujunKkk = recSyouhin23.HyoujunKkk                               '標準価格
            syouhin23_info.Syouhin2Rec.SiireKkk = recSyouhin23.SiireKkk                                   '仕入価格
            syouhin23_info.SeikyuuUmu = SelectSeikyuuUmu.SelectedValue                                    '請求有無
            syouhin23_info.HattyuusyoKakuteiFlg = Integer.Parse(SelectHattyuusyoKakutei.SelectedValue)    '発注書確定フラグ
            syouhin23_info.KeiretuCd = Me.HiddenKeiretu.Value                                             '系列コード
            syouhin23_info.KeiretuFlg = logic.GetKeiretuFlg(Me.HiddenKeiretu.Value)                       '系列フラグ(3系列or以外)
            If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then
                syouhin23_info.Seikyuusaki = EarthConst.SEIKYU_TYOKUSETU                                  '請求タイプ(直接or他)
            Else
                syouhin23_info.Seikyuusaki = EarthConst.SEIKYU_TASETU
            End If

            ' 請求レコードの取得(確実に結果が有る)
            Dim recTeiSei As TeibetuSeikyuuRecord = logic.GetSyouhin23SeikyuuData(sender, syouhin23_info, 1)

            '価格情報をセット
            TextKoumutenSeikyuuGaku.Text = recTeiSei.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            TextJituSeikyuuGaku.Text = recTeiSei.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            TextSyouhizeiGaku.Text = (Fix(recTeiSei.UriGaku * recSyouhin23.Zeiritu)).ToString(EarthConst.FORMAT_KINGAKU_1)
            TextZeikomiKingaku.Text = recTeiSei.UriGaku + Fix(recTeiSei.UriGaku * recSyouhin23.Zeiritu).ToString(EarthConst.FORMAT_KINGAKU_1)

            ' 税金額再設定
            SetZeigaku(e)

        Else
            ' 請求無しの場合
            TextKoumutenSeikyuuGaku.Text = "0"
            TextJituSeikyuuGaku.Text = "0"
            SetZeigaku(e)
            Me.TextSeikyuusyoHakkouDate.Text = String.Empty
        End If

        '【画面制御】
        SetEnableControl()

        '請求書発行日・売上年月日を設定
        If Me.HiddenKeiriGyoumuKengen.Value <> "-1" Then
            setHakkoubi(True)
        End If

        'フォーカスを設定
        setFocusAJ(SelectSeikyuuUmu)

    End Sub

    ''' <summary>
    ''' 売上処理ドロップダウンリスト変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectUriageSyori_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If SelectUriageSyori.SelectedValue = "0" Then
            ' 売上処理日をクリア
            TextUriageDate.Text = String.Empty
        Else
            ' システム日付をセット
            TextUriageDate.Text = Date.Now.ToString("yyyy/MM/dd")
        End If

        'フォーカスセット
        setFocusAJ(SelectUriageSyori)


    End Sub

    ''' <summary>
    ''' 発注書確定ドロップダウンリスト変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectHattyuusyoKakutei_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' 発注書自動確定処理
        FunCheckHKakutei(1, sender)
        setFocusAJ(SelectHattyuusyoKakutei)

        ' Old値に現在の値をセット
        HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue

    End Sub

    ''' <summary>
    ''' 請求年月日，売上年月日を設定します
    ''' </summary>
    ''' <param name="flgUriDateForce">売上年月日無条件で自動設定FLG</param>
    ''' <remarks></remarks>
    Private Sub setHakkoubi(Optional ByVal flgUriDateForce As Boolean = False)

        ' 売上・請求年月日取得用ロジッククラス
        Dim logic As New JibanLogic
        ' 値を取得する為の邸別請求レコード
        Dim teibetu_rec As New TeibetuSeikyuuRecord

        ' 確定区分
        cl.SetDisplayString(Me.SelectKakutei.SelectedIndex, teibetu_rec.KakuteiKbn)
        ' 請求書発行日
        cl.SetDisplayString(Me.TextSeikyuusyoHakkouDate.Text, teibetu_rec.SeikyuusyoHakDate)
        ' 売上年月日
        cl.SetDisplayString(Me.TextUriageNengappi.Text, teibetu_rec.UriDate)
        ' 請求有無
        cl.SetDisplayString(Me.SelectSeikyuuUmu.SelectedValue, teibetu_rec.SeikyuuUmu)
        ' 区分
        teibetu_rec.Kbn = Me.HiddenKubun.Value
        ' 商品コード
        cl.SetDisplayString(Me.TextSyouhinCd.Text, teibetu_rec.SyouhinCd)

        '請求先情報のセット
        teibetu_rec.SeikyuuSakiCd = Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value
        teibetu_rec.SeikyuuSakiBrc = Me.ucSeikyuuSiireLink.AccSeikyuuSakiBrc.Value
        teibetu_rec.SeikyuuSakiKbn = Me.ucSeikyuuSiireLink.AccSeikyuuSakiKbn.Value

        ' 売上・請求年月日取得
        logic.SubChangeKakutei(Me.HiddenKameitenCd.Value, teibetu_rec)

        ' 請求書発行日設定
        TextSeikyuusyoHakkouDate.Text = cl.GetDisplayString(teibetu_rec.SeikyuusyoHakDate)
        ' 売上年月日・伝票売上年月日設定
        If flgUriDateForce And (Me.SelectKakutei.SelectedIndex = 1) Then
            TextUriageNengappi.Text = Date.Now.ToString("yyyy/MM/dd")
            TextDenpyouUriageNengappi.Text = Date.Now.ToString("yyyy/MM/dd")
        ElseIf Not flgUriDateForce Then
            TextUriageNengappi.Text = cl.GetDisplayString(teibetu_rec.UriDate)
            TextDenpyouUriageNengappi.Text = cl.GetDisplayString(teibetu_rec.UriDate)
        End If


    End Sub

#End Region

#Region "金額系"

    ''' <summary>
    ''' 工務店請求税抜金額変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKoumutenSeikyuuGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextKoumutenSeikyuuGaku.TextChanged

        ' 画面の工務店請求金額を数値型に変換
        Dim koumutengaku As Integer = 0

        If Not Me.TextKoumutenSeikyuuGaku.Text.Trim() = String.Empty Then
            koumutengaku = Integer.Parse(TextKoumutenSeikyuuGaku.Text.Replace(",", ""))
        End If

        TextKoumutenSeikyuuGaku.Text = koumutengaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        ' 請求先情報
        If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
           HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then

            '**************************************************
            ' 直接請求
            '**************************************************
            ' ※工務店金額を実請求金額に設定
            TextKoumutenSeikyuuGaku.Text = koumutengaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            TextJituSeikyuuGaku.Text = koumutengaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        Else
            '**************************************************
            ' 他請求（系列）※系列以外は入力不可の為、関係なし
            '**************************************************
            If HiddenKingakuFlg.Value <> "2" Then

                Dim logic As New JibanLogic
                Dim zeinukiGaku As Integer = 0

                If logic.GetSeikyuuGaku(sender, _
                                        5, _
                                        HiddenKeiretu.Value, _
                                        TextSyouhinCd.Text, _
                                        koumutengaku, _
                                        zeinukiGaku) Then

                    ' 実請求金額へセット
                    TextJituSeikyuuGaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                    ' 工務店請求金額と実請求額の自動設定制御判定用フラグセット
                    HiddenKingakuFlg.Value = "1"

                End If
            End If
        End If

        SetZeigaku(e)

        'フォーカスセット
        setFocusAJ(TextJituSeikyuuGaku)

    End Sub

    ''' <summary>
    ''' 実請求税抜金額変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextJituSeikyuuGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextJituSeikyuuGaku.TextChanged

        '実請求金額を数値型に変換、金額フォーマットを指定
        Dim jitugaku As Integer = 0
        If Me.TextJituSeikyuuGaku.Text.Trim() <> String.Empty Then
            jitugaku = Integer.Parse(TextJituSeikyuuGaku.Text.Replace(",", ""))
            TextJituSeikyuuGaku.Text = jitugaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        Else
            SetZeigaku(e)
            setFocusAJ(TextSyouhizeiGaku)
            Exit Sub
        End If

        ' 請求先情報
        If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
           HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then

            '**************************************************
            ' 直接請求
            '**************************************************
            ' ※実請求金額を工務店金額に設定
            TextJituSeikyuuGaku.Text = jitugaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            TextKoumutenSeikyuuGaku.Text = jitugaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        ElseIf HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuKeiretu Then
            '**************************************************
            ' 他請求（系列）※系列以外は設定なし
            '**************************************************
            If HiddenKingakuFlg.Value <> "1" Then

                Dim koumutenGaku As Integer = 0
                Dim logic As New JibanLogic

                ' 請求額算出メソッドへの引数設定（商品１の場合のみ6,他は4）
                Dim param As Integer = 4

                ' 請求額を算出する
                If logic.GetSeikyuuGaku(sender, _
                                        param, _
                                        HiddenKeiretu.Value, _
                                        TextSyouhinCd.Text, _
                                        jitugaku, _
                                        koumutenGaku) Then

                    ' 工務店請求金額セット
                    TextKoumutenSeikyuuGaku.Text = koumutenGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                    ' 工務店請求金額と実請求額の自動設定制御判定用フラグセット
                    HiddenKingakuFlg.Value = "2"

                End If
            End If
        End If

        SetZeigaku(e)

        'フォーカスセット
        setFocusAJ(TextSyouhizeiGaku)

    End Sub

    ''' <summary>
    ''' 承諾書金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSyoudakusyoKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextSyoudakusyoKingaku.TextChanged

        If sender.Text.Trim() = "" Then
            sender.Text = "0"
        End If

        Dim jitugaku As Integer = Integer.Parse(sender.Text.Replace(",", ""))
        Dim zeigaku As Integer = 0
        If IsNumeric(Me.HiddenZeiritu.Value) = False Then
            Me.HiddenZeiritu.Value = "0"
        End If
        zeigaku = Fix(jitugaku * Decimal.Parse(HiddenZeiritu.Value))

        TextSiireSyouhizeiGaku.Text = zeigaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        'フォーカスセット
        setFocusAJ(TextDenpyouSiireNengappi)

    End Sub

    ''' <summary>
    ''' 仕入消費税額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSiireSyouhizeiGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextSiireSyouhizeiGaku.TextChanged

        If sender.Text.Trim() = "" Then
            sender.Text = "0"
        End If

        'フォーカスセット
        setFocusAJ(TextDenpyouSiireNengappi)

    End Sub

    ''' <summary>
    ''' 消費税額額変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSyouhizeiGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextSyouhizeiGaku.TextChanged

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".TextSyouhizeiGaku_TextChanged", sender)

        Dim intJitugaku As Integer = 0
        Dim intZeigaku As Integer = 0

        '実請求税抜金額の取得
        If Me.TextJituSeikyuuGaku.Text.Trim() = String.Empty Or Me.TextJituSeikyuuGaku.Text.Trim() = "0" Then
            Me.TextSyouhizeiGaku.Text = "0"
            Me.TextZeikomiKingaku.Text = "0"
            Exit Sub
        Else
            intJitugaku = Integer.Parse(TextJituSeikyuuGaku.Text.Replace(",", ""))
        End If

        '消費税額の取得
        If Me.TextSyouhizeiGaku.Text.Trim() <> String.Empty Then
            intZeigaku = Integer.Parse(TextSyouhizeiGaku.Text.Replace(",", ""))
        Else
            Me.TextSyouhizeiGaku.Text = "0"
        End If

        '税込金額の再計算
        Me.TextZeikomiKingaku.Text = (intJitugaku + intZeigaku).ToString(EarthConst.FORMAT_KINGAKU_1)

        'フォーカスの設定
        setFocusAJ(TextDenpyouUriageNengappi)

    End Sub

    ''' <summary>
    ''' 発注書金額変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextHattyuusyoKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextHattyuusyoKingaku.TextChanged
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".TextHattyuusyoKingaku_TextChanged", _
                                                    sender)

        Dim hattyuuKingaku As Integer = Integer.MinValue

        CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, hattyuuKingaku)

        TextHattyuusyoKakuninDate.Text = PfunZ010SetHatyuuYMD(hattyuuKingaku)

        '発注書確定チェック
        Select Case FunCheckHKakutei(2, sender)
            Case 1
                '
            Case 2
                SelectHattyuusyoKakutei.SelectedValue = "1"

                ' Old値に現在の値をセット
                HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue

                ' 発注書金額を入力不可に設定
                EnableTextBox(TextHattyuusyoKingaku, False)
        End Select

        '更新後発注書金額をoldに設定
        HiddenHattyuusyoKingakuOld.Value = TextHattyuusyoKingaku.Text
        TextHattyuusyoKingaku.Text = IIf(hattyuuKingaku = Integer.MinValue, "", hattyuuKingaku.ToString(EarthConst.FORMAT_KINGAKU_1))

        setFocusAJ(SelectHattyuusyoKakutei)
    End Sub

    ''' <summary>
    ''' 発注書確認日設定処理
    ''' </summary>
    ''' <param name="rvntKingaku">発注書金額</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PfunZ010SetHatyuuYMD(ByVal rvntKingaku As Integer) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".PfunZ010SetHatyuuYMD", _
                                                    rvntKingaku)

        If rvntKingaku = 0 Or rvntKingaku = Integer.MinValue Then
            Return ""
        Else
            Return Date.Now.ToString("yyyy/MM/dd")
        End If
    End Function

    ''' <summary>
    ''' 発注書確定チェック
    ''' </summary>
    ''' <param name="rlngMode">1,発注書確定変更時.2,発注書金額変更時</param>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <returns>0:処理なし、1:発注書確定へフォーカス移行、2:自動確定</returns>
    ''' <remarks></remarks>
    Public Function FunCheckHKakutei(ByVal rlngMode As Long, _
                                     ByVal sender As Object) As Long

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".FunCheckHKakutei", _
                                                    rlngMode, sender)

        FunCheckHKakutei = 0

        If SelectHattyuusyoKakutei.SelectedValue = "1" And _
           TextHattyuusyoKingaku.Text IsNot TextJituSeikyuuGaku.Text Then

            Dim hattyuuKingaku As Integer = Integer.MinValue
            CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, hattyuuKingaku)
            ' 発注書確認日を設定
            TextHattyuusyoKakuninDate.Text = PfunZ010SetHatyuuYMD(hattyuuKingaku)

        End If

        ' 比較する金額を数値変換する
        Dim chkVal1 As Integer = 0
        Dim chkVal2 As Integer = 0
        CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, chkVal1)
        CommonLogic.Instance.SetDisplayString(TextJituSeikyuuGaku.Text, chkVal2)

        ' 売上状況
        If rlngMode = 2 Then
            ' 発注書金額変更時はテキスト再変更後メッセージ表示
            If SelectUriageSyori.SelectedValue = EarthConst.URIAGE_ZUMI_CODE Then

                ' 比較して金額の比較によりメッセージを分ける
                If chkVal1 = chkVal2 Then
                    FunCheckHKakutei = 2
                    If HiddenHattyuusyoKingakuOld.Value <> "" And _
                       HiddenHattyuusyoFlgOld.Value <> "1" Then
                        ' 発注書自動確定
                        ScriptManager.RegisterClientScriptBlock(sender, _
                                                                sender.GetType(), _
                                                                "alert", _
                                                                "alert('" & _
                                                                Messages.MSG045C & _
                                                                "')", True)
                    End If
                End If
            End If
        Else
            If SelectHattyuusyoKakutei.SelectedValue = "1" Then
                ' 発注書確定変更時は金額相違でメッセージ表示
                FunCheckHKakutei = 2
                If chkVal1 <> chkVal2 Then
                    ' 発注書自動確定
                    ScriptManager.RegisterClientScriptBlock(sender, _
                                                            sender.GetType(), _
                                                            "alert", _
                                                            "alert('" & _
                                                            Messages.MSG046C & _
                                                            "')", True)
                End If
            End If
        End If

        '発注書確定済みの場合、発注書金額を編集不可に設定
        If SelectHattyuusyoKakutei.SelectedValue = "1" Then
            cl.chgVeiwMode(TextHattyuusyoKingaku)
        Else
            If HiddenKeiriGyoumuKengen.Value = "-1" Or _
               HiddenHattyuusyoKanriKengen.Value = "-1" Or _
               HiddenKeiriGyoumuKengen.Value = "-1" Then
                ' 経理権限か、依頼業務か、発注書管理権限有る場合、活性化
                cl.chgDispSyouhinText(TextHattyuusyoKingaku)
            End If
        End If

    End Function

    ''' <summary>
    ''' 消費税額、税込金額のセット
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZeigaku(ByVal e As System.EventArgs)

        ' 実請求税抜金額が空白の場合、消費税額・税込金額を空白にする
        If TextJituSeikyuuGaku.Text.Trim() = String.Empty Then
            TextSyouhizeiGaku.Text = String.Empty
            TextZeikomiKingaku.Text = String.Empty
            Exit Sub
        End If

        Dim jitugaku As Integer = IIf(TextJituSeikyuuGaku.Text.Trim() = "", _
                                      0, _
                                      Integer.Parse(TextJituSeikyuuGaku.Text.Replace(",", "")))
        Dim zeigaku As Integer = Fix(jitugaku * Decimal.Parse(HiddenZeiritu.Value))
        Dim zeikomi As Integer = jitugaku + zeigaku

        '消費税額
        TextSyouhizeiGaku.Text = zeigaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        '税込金額
        TextZeikomiKingaku.Text = zeikomi.ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub

    ''' <summary>
    ''' 仕入消費税額のセット
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSiireZeigaku()
        Dim intSyouGaku As Integer = 0
        Dim intSiireZeiGaku As Integer = 0

        '承諾書金額が空白の場合、仕入消費税額を空白にする
        If Me.TextSyoudakusyoKingaku.Text.Trim() = String.Empty Then
            Me.TextSiireSyouhizeiGaku.Text = String.Empty
            Exit Sub
        ElseIf Me.TextSyoudakusyoKingaku.Text.Trim() = "0" Then
            Me.TextSiireSyouhizeiGaku.Text = "0"
            Exit Sub
        End If

        '税率の確認
        If IsNumeric(Me.HiddenZeiritu.Value) = False Then
            Me.HiddenZeiritu.Value = "0"
        End If

        '承諾書金額の取得
        intSyouGaku = Integer.Parse(Me.TextSyoudakusyoKingaku.Text.Replace(",", ""))

        '仕入消費税額の計算
        intSiireZeiGaku = Fix(intSyouGaku * Decimal.Parse(Me.HiddenZeiritu.Value))

        '画面へ設定
        Me.TextSiireSyouhizeiGaku.Text = intSiireZeiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub

#End Region

#Region "日付系イベント"

    ''' <summary>
    ''' 売上年月日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextUriageNengappi_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextUriageNengappi.TextChanged
        Dim strZeiKbn As String = String.Empty              '税区分
        Dim strZeiritu As String = String.Empty             '税率
        Dim strSyouhinCd As String = String.Empty           '商品コード
        ' 取得用ロジッククラス
        Dim cBizLogic As New CommonBizLogic

        '売上年月日
        If Me.TextUriageNengappi.Text <> String.Empty Then
            '空白の場合のみ伝票売上年月日を設定
            If Me.TextDenpyouUriageNengappi.Text.Trim() = String.Empty Then
                Me.TextDenpyouUriageNengappi.Text = Me.TextUriageNengappi.Text
                TextDenpyouUriageNengappi_TextChanged(sender, e)
            End If
            '空白の場合のみ伝票仕入年月日を設定
            If Me.TextDenpyouSiireNengappi.Text = String.Empty Then
                '売上年月日をセット
                Me.TextDenpyouSiireNengappi.Text = Me.TextUriageNengappi.Text
            End If

            '税区分・税率を再取得
            strSyouhinCd = Me.TextSyouhinCd.Text

            ' 商品コード変更時、Oldに画面.商品コードをセット
            If HiddenSyouhinCdOld.Value <> TextSyouhinCd.Text Then
                HiddenSyouhinCdOld.Value = TextSyouhinCd.Text
            End If

            If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                '取得した税区分・税率をセット
                Me.HiddenZeikubun.Value = strZeiKbn
                Me.HiddenZeiritu.Value = strZeiritu

                '税区分・税率が空白の場合、実請求税抜金額と承諾書金額に空白をセットする
                If Me.HiddenZeiritu.Value = String.Empty OrElse Me.HiddenZeikubun.Value = String.Empty Then
                    Me.TextJituSeikyuuGaku.Text = ""
                    Me.TextSyoudakusyoKingaku.Text = ""
                End If
                '金額計算
                SetZeigaku(e)
                SetSiireZeigaku()
            End If
        Else
            '売上年月日未入力の場合

            '税区分・税率を再取得
            strSyouhinCd = Me.TextSyouhinCd.Text

            If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                '取得した税区分・税率をセット
                Me.HiddenZeikubun.Value = strZeiKbn
                Me.HiddenZeiritu.Value = strZeiritu

                '税区分・税率が空白の場合、実請求税抜金額と承諾書金額に空白をセットする
                If Me.HiddenZeiritu.Value = String.Empty OrElse Me.HiddenZeikubun.Value = String.Empty Then
                    Me.TextJituSeikyuuGaku.Text = ""
                    Me.TextSyoudakusyoKingaku.Text = ""
                End If
                '金額計算
                SetZeigaku(e)
                SetSiireZeigaku()
            End If
        End If

        '伝票売上年月日にフォーカス
        setFocusAJ(TextDenpyouUriageNengappi)

    End Sub

    ''' <summary>
    ''' 伝票売上年月日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextDenpyouUriageNengappi_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextDenpyouUriageNengappi.TextChanged
        ' 取得用ロジッククラス
        Dim cBizLogic As New CommonBizLogic

        '伝票売上年月日
        If Me.TextDenpyouUriageNengappi.Text <> String.Empty Then
            '締め日の設定
            Me.ucSeikyuuSiireLink.SetSeikyuuSimeDate()
            ' 請求書発行日取得
            Dim strHakDate As String = Me.ucSeikyuuSiireLink.GetSeikyuusyoHakkouDate(Me.TextDenpyouUriageNengappi.Text)
            Me.TextSeikyuusyoHakkouDate.Text = strHakDate
        Else
            Me.TextSeikyuusyoHakkouDate.Text = String.Empty
        End If

        '請求書発行日にフォーカス
        setFocusAJ(Me.TextSeikyuusyoHakkouDate)

    End Sub

#End Region

#Region "入力チェック"
    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <param name="errMess"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <remarks></remarks>
    Public Sub CheckInput(ByRef errMess As String, _
                          ByRef arrFocusTargetCtrl As List(Of Control), _
                          ByVal intLineCnt As Integer)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckInput", _
                                                    errMess, _
                                                    arrFocusTargetCtrl)

        '行番号文字列
        Dim strGyouInfo As String = "商品4_" & intLineCnt & "："
        '月次予約確定月
        Dim dtGetujiKakuteiLastSyoriDate As DateTime = DateTime.MinValue

        '商品コード
        If Me.TextSyouhinCd.Text.Trim = String.Empty Then '未入力
            Exit Sub '商品削除のため、対象外
        End If

        'DB読み込み時点の値を、現在画面の値と比較(変更有無チェック)
        If HiddenKubun.Value <> String.Empty AndAlso (HiddenOpenValuesUriage.Value <> String.Empty Or HiddenOpenValuesSiire.Value <> String.Empty) Then
            'DB読み込み時点の値が空の場合は比較対象外
            '比較実施(売上)
            If HiddenOpenValuesUriage.Value <> getCtrlValuesStringUriage() Then
                '月次確定予約済みの処理年月「以前」の日付で伝票売上年月日を設定するのはエラー
                If cl.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouUriageNengappi.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                    errMess += String.Format(Messages.MSG161E, dtGetujiKakuteiLastSyoriDate.Month, strGyouInfo, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                    arrFocusTargetCtrl.Add(Me.TextDenpyouUriageNengappi)
                End If
            End If

            '比較実施(仕入)
            If HiddenOpenValuesSiire.Value <> getCtrlValuesStringSiire() Then
                '月次確定予約済みの処理年月「以前」の日付で伝票仕入年月日を設定するのはエラー
                If cl.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouSiireNengappi.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                    errMess += String.Format(Messages.MSG161E, dtGetujiKakuteiLastSyoriDate.Month, strGyouInfo, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month).Replace("伝票売上", "伝票仕入")
                    arrFocusTargetCtrl.Add(Me.TextDenpyouSiireNengappi)
                End If
            End If

        End If
        '新規登録時のチェック(伝票売上年月日)
        If String.IsNullOrEmpty(HiddenOpenValuesUriage.Value) Then
            '月次確定予約済みの処理年月「以前」の日付で伝票売上年月日を設定するのはエラー
            If cl.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouUriageNengappi.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                errMess += String.Format(Messages.MSG161E, dtGetujiKakuteiLastSyoriDate.Month, strGyouInfo, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                arrFocusTargetCtrl.Add(Me.TextDenpyouUriageNengappi)
            End If
        End If
        '新規登録時のチェック(伝票仕入年月日)
        If String.IsNullOrEmpty(HiddenOpenValuesSiire.Value) Then
            '月次確定予約済みの処理年月「以前」の日付で伝票仕入年月日を設定するのはエラー
            If cl.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouSiireNengappi.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                errMess += String.Format(Messages.MSG161E, dtGetujiKakuteiLastSyoriDate.Month, strGyouInfo, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month).Replace("伝票売上", "伝票仕入")
                arrFocusTargetCtrl.Add(Me.TextDenpyouSiireNengappi)
            End If
        End If

        '●コード入力値変更チェック
        '商品コード
        If Me.TextSyouhinCd.Text <> Me.HiddenSyouhinCdOld.Value Then
            errMess &= Messages.MSG030E.Replace("@PARAM1", "商品コード")
            arrFocusTargetCtrl.Add(ButtonSyouhinKensaku)
        End If

        '●必須チェック
        '請求先情報(請求有無：有りの場合のみ必須)
        If Me.SelectSeikyuuUmu.SelectedValue = "1" Then
            If Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value.Trim() = String.Empty _
                Or Me.ucSeikyuuSiireLink.AccSeikyuuSakiBrc.Value.Trim() = String.Empty _
                Or Me.ucSeikyuuSiireLink.AccSeikyuuSakiKbn.Value.Trim() = String.Empty Then

                errMess &= strGyouInfo & Messages.MSG151E.Replace("@PARAM1", "請求先情報").Replace("@PARAM2", "請求先/仕入先変更画面")
                arrFocusTargetCtrl.Add(ucSeikyuuSiireLink.AccLinkSeikyuuSiireHenkou)
            End If
        End If
        '仕入先情報
        If Me.ucSeikyuuSiireLink.AccTysKaisyaCd.Value.Trim() = String.Empty _
            Or Me.ucSeikyuuSiireLink.AccTysKaisyaJigyousyoCd.Value.Trim() = String.Empty Then

            errMess &= strGyouInfo & Messages.MSG151E.Replace("@PARAM1", "仕入先情報").Replace("@PARAM2", "請求先/仕入先変更画面")
            arrFocusTargetCtrl.Add(ucSeikyuuSiireLink.AccLinkSeikyuuSiireHenkou)
        End If

        '売上年月日と伝票売上年月日
        If Me.TextUriageNengappi.Text = String.Empty And Me.TextDenpyouUriageNengappi.Text <> String.Empty Then
            errMess &= strGyouInfo & Messages.MSG153E.Replace("@PARAM1", "伝票売上年月日").Replace("@PARAM2", "売上年月日")
            arrFocusTargetCtrl.Add(TextUriageNengappi)
        End If

        '●日付チェック
        '見積書作成日
        If Trim(Me.TextMitumorisyoSakuseiDate.Text) <> "" Then
            If cl.checkDateHanni(Me.TextMitumorisyoSakuseiDate.Text) = False Then
                errMess &= strGyouInfo & Messages.MSG014E.Replace("@PARAM1", "見積書作成日")
                arrFocusTargetCtrl.Add(TextMitumorisyoSakuseiDate)
            End If
        End If
        '売上年月日
        If Trim(Me.TextUriageNengappi.Text) <> "" Then
            If cl.checkDateHanni(Me.TextUriageNengappi.Text) = False Then
                errMess &= strGyouInfo & Messages.MSG014E.Replace("@PARAM1", "売上年月日")
                arrFocusTargetCtrl.Add(Me.TextUriageNengappi)
            End If
        End If
        '伝票売上年月日
        If Trim(Me.TextDenpyouUriageNengappi.Text) <> "" Then
            If cl.checkDateHanni(Me.TextDenpyouUriageNengappi.Text) = False Then
                errMess &= strGyouInfo & Messages.MSG014E.Replace("@PARAM1", "伝票売上年月日")
                arrFocusTargetCtrl.Add(Me.TextDenpyouUriageNengappi)
            End If
        End If
        '伝票仕入年月日
        If Trim(Me.TextDenpyouSiireNengappi.Text) <> "" Then
            If cl.checkDateHanni(Me.TextDenpyouSiireNengappi.Text) = False Then
                errMess &= strGyouInfo & Messages.MSG014E.Replace("@PARAM1", "伝票仕入年月日")
                arrFocusTargetCtrl.Add(Me.TextDenpyouSiireNengappi)
            End If
        End If
        '請求書発行日
        If Trim(Me.TextSeikyuusyoHakkouDate.Text) <> "" Then
            If cl.checkDateHanni(Me.TextSeikyuusyoHakkouDate.Text) = False Then
                errMess &= strGyouInfo & Messages.MSG014E.Replace("@PARAM1", "請求書年月日")
                arrFocusTargetCtrl.Add(Me.TextSeikyuusyoHakkouDate)
            End If
        End If
        '発注書確認日
        If Trim(Me.TextHattyuusyoKakuninDate.Text) <> "" Then
            If cl.checkDateHanni(Me.TextHattyuusyoKakuninDate.Text) = False Then
                errMess &= strGyouInfo & Messages.MSG014E.Replace("@PARAM1", "発注書確認日")
                arrFocusTargetCtrl.Add(Me.TextHattyuusyoKakuninDate)
            End If
        End If

    End Sub

    ''' <summary>
    ''' 画面読み込み時の値をHidden項目に退避
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub setOpenValuesUriage()
        Me.HiddenOpenValuesUriage.Value = getCtrlValuesStringUriage()
    End Sub

    ''' <summary>
    ''' 画面読み込み時の値をHidden項目に退避
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub setOpenValuesSiire()
        Me.HiddenOpenValuesSiire.Value = getCtrlValuesStringSiire()
    End Sub

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する
    ''' </summary>
    ''' <returns>画面コントロールの値を結合した文字列</returns>
    ''' <remarks></remarks>
    Public Function getCtrlValuesStringUriage() As String
        Dim sb As New StringBuilder

        sb.Append(TextSyouhinCd.Text & EarthConst.SEP_STRING)               '商品コード
        sb.Append(TextJituSeikyuuGaku.Text & EarthConst.SEP_STRING)         '売上金額(実請求税抜金額)
        sb.Append(TextSyouhizeiGaku.Text & EarthConst.SEP_STRING)           '消費税額
        sb.Append(HiddenZeikubun.Value & EarthConst.SEP_STRING)             '税区分(非表示項目)
        sb.Append(TextDenpyouUriageNengappi.Text & EarthConst.SEP_STRING)   '伝票売上年月日
        sb.Append(TextUriageNengappi.Text & EarthConst.SEP_STRING)          '売上年月日
        sb.Append(SelectUriageSyori.SelectedValue & EarthConst.SEP_STRING)  '売上計上FLG
        sb.Append(TextSeikyuusyoHakkouDate.Text & EarthConst.SEP_STRING)    '請求書発行日

        sb.Append(ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value & EarthConst.SEP_STRING)          '請求先コード
        sb.Append(ucSeikyuuSiireLink.AccSeikyuuSakiBrc.Value & EarthConst.SEP_STRING)         '請求先枝番
        sb.Append(ucSeikyuuSiireLink.AccSeikyuuSakiKbn.Value & EarthConst.SEP_STRING)         '請求先区分

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する
    ''' </summary>
    ''' <returns>画面コントロールの値を結合した文字列</returns>
    ''' <remarks></remarks>
    Public Function getCtrlValuesStringSiire() As String
        Dim sb As New StringBuilder

        sb.Append(TextSyouhinCd.Text & EarthConst.SEP_STRING)               '商品コード
        sb.Append(TextSyoudakusyoKingaku.Text & EarthConst.SEP_STRING)      '仕入金額(承諾書金額)
        sb.Append(TextSiireSyouhizeiGaku.Text & EarthConst.SEP_STRING)      '仕入消費税額
        sb.Append(TextDenpyouSiireNengappi.Text & EarthConst.SEP_STRING)    '伝票仕入年月日
        sb.Append(HiddenZeikubun.Value & EarthConst.SEP_STRING)             '税区分(非表示項目)
        sb.Append(SelectUriageSyori.SelectedValue & EarthConst.SEP_STRING)  '売上計上FLG

        sb.Append(ucSeikyuuSiireLink.AccTysKaisyaCd.Value & EarthConst.SEP_STRING)            '調査会社コード
        sb.Append(ucSeikyuuSiireLink.AccTysKaisyaJigyousyoCd.Value & EarthConst.SEP_STRING)   '調査会社事業所コード

        Return sb.ToString
    End Function

#End Region

#Region "コントロールの活性制御"
    ''' <summary>
    ''' テキストボックス単体の活性制御
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableTextBox(ByRef ctrl As TextBox, _
                              ByVal enabled As Boolean)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnableTextBox", _
                                                    ctrl, enabled)

        ctrl.ReadOnly = Not enabled

        If enabled Then
            ctrl.BackColor = Drawing.Color.White
            ctrl.BorderStyle = BorderStyle.NotSet
            ctrl.TabIndex = 0
        Else
            ctrl.BackColor = Drawing.Color.Transparent
            ctrl.BorderStyle = BorderStyle.None
            ctrl.TabIndex = -1
        End If

    End Sub



#End Region

End Class