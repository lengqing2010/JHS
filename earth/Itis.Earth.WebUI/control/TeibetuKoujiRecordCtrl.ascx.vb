Partial Public Class TeibetuKoujiRecordCtrl
    Inherits System.Web.UI.UserControl

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'UtilitiesのMessegeLogicクラス
    Private mLogic As New MessageLogic

    Private jSM As New JibanSessionManager

    'マスターページのAjaxスクリプトマネージャへのアクセス用
    Private masterAjaxSM As New ScriptManager

    Private cbLogic As New CommonBizLogic
    Private cLogic As New CommonLogic

#Region "表示モード"
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DisplayMode
        ''' <summary>
        ''' 改良工事
        ''' </summary>
        ''' <remarks></remarks>
        KAIRYOU = 130
        ''' <summary>
        ''' 追加工事
        ''' </summary>
        ''' <remarks></remarks>
        TUIKA = 140
    End Enum
#End Region

#Region "プロパティ"
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <remarks></remarks>
    Private mode As DisplayMode
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <value></value>
    ''' <returns>コントロールの表示モード</returns>
    ''' <remarks>商品の種類により画面の表示を変更します</remarks>
    Public Property DispMode() As DisplayMode
        Get
            If Not ViewState("DisplayMode") Is Nothing Then
                mode = ViewState("DisplayMode")
            End If
            Return mode
        End Get
        Set(ByVal value As DisplayMode)
            mode = value
            ViewState("DisplayMode") = value
        End Set
    End Property

    ''' <summary>
    ''' 邸別請求レコード
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _teibetuSeikyuuRec As TeibetuSeikyuuRecord
    ''' <summary>
    ''' 邸別請求レコード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property TeibetuSeikyuuRec() As TeibetuSeikyuuRecord
        Get
            Return GetCtrlData()
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            _teibetuSeikyuuRec = value

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            Dim helper As New DropDownHelper
            ' ドロップダウンリスト設定
            If DispMode = DisplayMode.KAIRYOU Then
                helper.SetDropDownList(SelectSyouhinCd, DropDownHelper.DropDownType.SyouhinKouji, True, True, 0, False)
            ElseIf DispMode = DisplayMode.TUIKA Then
                helper.SetDropDownList(SelectSyouhinCd, DropDownHelper.DropDownType.SyouhinTuika, True, True, 0, False)
            End If

            If Not _teibetuSeikyuuRec Is Nothing Then
                ' コントロールにデータをセット
                SetCtrlData(_teibetuSeikyuuRec)
            End If

        End Set
    End Property

#Region "邸別データ共通設定情報"
    ''' <summary>
    ''' 邸別データ共通設定情報
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingInfo() As TeibetuSettingInfoRecord
        Get
            Dim info As New TeibetuSettingInfoRecord
            info.Kubun = HiddenKubun.Value                                                  ' 区分
            info.Bangou = HiddenBangou.Value                                                ' 番号（保証書NO）
            info.UpdLoginUserId = HiddenLoginUserId.Value                                   ' ログインユーザーID
            info.KeiriGyoumuKengen = Integer.Parse(HiddenKeiriGyoumuKengen.Value)           ' 経理業務権限
            info.HattyuusyoKanriKengen = Integer.Parse(HiddenHattyuusyoKanriKengen.Value)   ' 発注書管理権限
            Return info
        End Get
        Set(ByVal value As TeibetuSettingInfoRecord)
            HiddenKubun.Value = value.Kubun                                 ' 区分
            HiddenBangou.Value = value.Bangou                               ' 番号（保証書NO）
            HiddenLoginUserId.Value = value.UpdLoginUserId                  ' ログインユーザーID
            ' 経理業務権限
            HiddenKeiriGyoumuKengen.Value = value.KeiriGyoumuKengen.ToString()
            ' 発注書管理権限
            HiddenHattyuusyoKanriKengen.Value = value.HattyuusyoKanriKengen.ToString()
        End Set
    End Property
#End Region

    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private _kameitenCd As String
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KameitenCd() As String
        Get
            Return _kameitenCd
        End Get
        Set(ByVal value As String)
            _kameitenCd = value

            If Not _kameitenCd Is Nothing Then
                HiddenKameitenCd.Value = _kameitenCd
            End If
        End Set
    End Property

    ''' <summary>
    ''' 工事会社コード
    ''' </summary>
    ''' <remarks></remarks>
    Private _koujiKaisyaCd As String
    ''' <summary>
    ''' 工事会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KoujiKaisyaCd() As String
        Get
            Return IIf(TextKoujigaisyaCd.Text.Trim() = "", "", Mid(TextKoujigaisyaCd.Text & "     ", 1, 4))
        End Get
        Set(ByVal value As String)
            _koujiKaisyaCd = value

            If Not _koujiKaisyaCd Is Nothing And _
               Not _koujiKaisyaJigyousyoCd Is Nothing Then
                TextKoujigaisyaCd.Text = _koujiKaisyaCd.Trim() + _koujiKaisyaJigyousyoCd.Trim()
            End If
        End Set
    End Property

    ''' <summary>
    ''' 工事会社事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private _koujiKaisyaJigyousyoCd As String
    ''' <summary>
    ''' 工事会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KoujiKaisyaJigyousyoCd() As String
        Get
            Return IIf(TextKoujigaisyaCd.Text.Trim() = "", "", Mid(TextKoujigaisyaCd.Text & "       ", 5, 2))
        End Get
        Set(ByVal value As String)
            _koujiKaisyaJigyousyoCd = value

            If Not _koujiKaisyaCd Is Nothing And _
               Not _koujiKaisyaJigyousyoCd Is Nothing Then
                TextKoujigaisyaCd.Text = _koujiKaisyaCd.Trim() + _koujiKaisyaJigyousyoCd.Trim()
            End If
        End Set
    End Property

    ''' <summary>
    ''' 工事会社名
    ''' </summary>
    ''' <remarks></remarks>
    Private _koujiKaisyaMei As String
    ''' <summary>
    ''' 工事会社名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KoujiKaisyaMei() As String
        Get
            Return _koujiKaisyaMei
        End Get
        Set(ByVal value As String)
            _koujiKaisyaMei = value

            If Not _koujiKaisyaMei Is Nothing Then
                TextKoujigaisyaMei.Text = _koujiKaisyaMei
            End If
        End Set
    End Property

    ''' <summary>
    ''' 工事会社請求有無
    ''' </summary>
    ''' <remarks></remarks>
    Private _koujiKaisyaSeikyuuUmu As Integer
    ''' <summary>
    ''' 工事会社請求有無
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KoujiKaisyaSeikyuuUmu() As Integer
        Get
            Return IIf(CheckKoujigaisyaSeikyuu.Checked, 1, Integer.MinValue)
        End Get
        Set(ByVal value As Integer)
            _koujiKaisyaSeikyuuUmu = value
            CheckKoujigaisyaSeikyuu.Checked = (_koujiKaisyaSeikyuuUmu = 1)
        End Set
    End Property

    ''' <summary>
    ''' 入金額
    ''' </summary>
    ''' <remarks></remarks>
    Private _nyuukinGaku As Integer
    ''' <summary>
    ''' 入金額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NyuukinGaku() As Integer
        Get
            Return _nyuukinGaku
        End Get
        Set(ByVal value As Integer)
            _nyuukinGaku = value
            NyuukinZangakuCtrlKouji.NyuukinGaku = _nyuukinGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        End Set
    End Property

    ''' <summary>
    ''' 残額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ZanGaku() As NyuukinZangakuCtrl
        Get
            Return NyuukinZangakuCtrlKouji
        End Get
        Set(ByVal value As NyuukinZangakuCtrl)
            NyuukinZangakuCtrlKouji = value
        End Set
    End Property

#Region "税込金額"
    ''' <summary>
    ''' 税込金額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ZeikomiKingaku() As Integer
        Get
            Dim strZeikomi As String = IIf(TextUriageZeikomiKingaku.Text.Replace(",", "").Trim() = "", _
                                           "0", _
                                           TextUriageZeikomiKingaku.Text.Replace(",", "").Trim())
            Return Integer.Parse(strZeikomi)
        End Get
    End Property
#End Region

#End Region

#Region "イベント"
    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント（請求先・仕入先情報設定用）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">画面ID</param>
    ''' <remarks></remarks>
    Public Event SetSeikyuuSiireSakiAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)

    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント（工事価格マスタ取得アクション用）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">画面ID</param>
    ''' <remarks></remarks>
    Public Event GetKojMInfoAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)

    ''' <summary>
    ''' ページロード時のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        If IsPostBack = False Then '初期読込時

            ' 画面表示設定
            Select Case Me.DispMode
                Case DisplayMode.KAIRYOU
                    ' 改良工事の場合
                    CtrlTitle.InnerText = EarthConst.CTRL_TITLE_KAIRYOU
                    KoujigaisyaTitle.InnerText = EarthConst.CTRL_KOUJI_KAIRYOU
                    HiddenBunruiCd.Value = EarthConst.SOUKO_CD_KAIRYOU_KOUJI   ' 分類コード
                Case DisplayMode.TUIKA
                    ' 追加改良工事の場合
                    CtrlTitle.InnerText = EarthConst.CTRL_TITLE_TUIKA
                    KoujigaisyaTitle.InnerText = EarthConst.CTRL_KOUJI_TUIKA
                    HiddenBunruiCd.Value = EarthConst.SOUKO_CD_TUIKA_KOUJI   ' 分類コード
            End Select

            If _teibetuSeikyuuRec Is Nothing Then
                ' コントロールの非活性化
                EnabledCtrl(False)
            End If

            If TextKoujigaisyaCd.Text <> "" Then
                ' 工事会社検索
                ButtonKoujigaisyaKensaku_ServerClick(sender, e)
            End If

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            Me.SetDispAction()

            '画面表示時点の値を、Hiddenに保持(売上 変更チェック用)
            If HiddenOpenValuesUriage.Value = String.Empty Then
                HiddenOpenValuesUriage.Value = getCtrlValuesStringUriage()
            End If

            '画面表示時点の値を、Hiddenに保持(仕入 変更チェック用)
            If HiddenOpenValuesSiire.Value = String.Empty Then
                HiddenOpenValuesSiire.Value = getCtrlValuesStringSiire()
            End If

            '画面表示時点の値を、Hiddenに保持(仕入 変更チェック用)
            If Me.HiddenOpenValue.Value = String.Empty Then
                Me.HiddenOpenValue.Value = Me.getCtrlValuesStringAll()
            End If

        End If
    End Sub

    ''' <summary>
    ''' ページ表示直前のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        '活性化制御
        Me.EnabledCtrlKengen()

    End Sub

    ''' <summary>
    ''' 工事会社検索ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKoujigaisyaKensaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        ' 初回起動時のみ
        If IsPostBack = False Then
            blnTorikesi = False
        End If

        If TextKoujigaisyaCd.Text <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetKoujikaishaSearchResult(TextKoujigaisyaCd.Text, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi, _
                                                                            HiddenKameitenCd.Value)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            HiddenKoujigaisyaCdOld.Value = recData.TysKaisyaCd & recData.JigyousyoCd
            TextKoujigaisyaCd.Text = recData.TysKaisyaCd & recData.JigyousyoCd
            TextKoujigaisyaMei.Text = recData.TysKaisyaMei
            ' 工事会社NG設定
            If recData.KahiKbn = 9 Then
                HiddenNg.Value = EarthConst.KOUJI_KAISYA_NG
                TextKoujigaisyaCd.Style("color") = "red"
                TextKoujigaisyaMei.Style("color") = "red"
            Else
                HiddenNg.Value = String.Empty
                TextKoujigaisyaCd.Style("color") = "blue"
                TextKoujigaisyaMei.Style("color") = "blue"
            End If

            '********************************************************************
            '★初期読み以外、工事価格マスタから価格取得
            If IsPostBack = True Then
                '親画面へイベント通知(工事商品価格設定)
                RaiseEvent GetKojMInfoAction(sender, e, Me.ClientID)

                ' 商品・売上金額・請求有無の自動設定（商品M or 工事価格M）
                If SetSyouhinInfoFromKojM(EarthEnum.emKojKkkActionType.KojKaisyaCd) = False Then '工事価格M
                    '直工事以外の場合は価格取得を行なわない
                End If
            End If

            'フォーカスセット
            SetFocus(ButtonKoujigaisyaKensaku)
        Else
            HiddenKoujigaisyaCdOld.Value = String.Empty
            TextKoujigaisyaMei.Text = String.Empty

            If blnTorikesi = False Then
                ' 初期起動時は検索しない
                Return
            End If

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpScript = "callSearch('" & TextKoujigaisyaCd.ClientID & EarthConst.SEP_STRING & _
                                             HiddenKameitenCd.ClientID & "','" & _
                                             UrlConst.SEARCH_KOUJIKAISYA & "','" & _
                                             TextKoujigaisyaCd.ClientID & EarthConst.SEP_STRING & _
                                             TextKoujigaisyaMei.ClientID & EarthConst.SEP_STRING & _
                                             HiddenNg.ClientID & "','" & ButtonKoujigaisyaKensaku.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)

        End If

        '請求仕入用カスタムイベント
        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

    End Sub

    ''' <summary>
    ''' 商品コンボ変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectSyouhinCd_SelectedIndexChanged(ByVal sender As System.Object, _
                                                       ByVal e As System.EventArgs) _
                                                       Handles SelectSyouhinCd.SelectedIndexChanged

        Dim logic As New TeibetuSyuuseiLogic
        Dim syouhinRec As New SyouhinMeisaiRecord
        'メッセージ用
        Dim tmpErrMsg As String = String.Empty
        Dim tmpScriptErr As String = String.Empty

        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        If SelectSyouhinCd.SelectedValue = "" Then

            Dim hattyuuKingaku As String = IIf(TextHattyuusyoKingaku.Text = "", "0", TextHattyuusyoKingaku.Text)

            If Not hattyuuKingaku = "0" Then
                ' 空白選択で発注書金額が０以外の場合、元に戻す
                SelectSyouhinCd.SelectedValue = HiddenSyouhinCd.Value

                ' メッセージを表示
                Dim tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                ScriptManager.RegisterStartupScript(sender, sender.GetType(), "err", tmpScript, True)

                ' 商品コードにフォーカスセット
                SetFocus(SelectSyouhinCd)

                ' 商品コードを変更するので商品コードのUpdateパネルを更新
                UpdatePanelSyouhinCd.Update()

                Return
            Else
                ' データのクリア
                ' 売上
                TextUriageZeinukiKingaku.Text = ""          ' 売上税抜金額
                HiddenZeiritu.Value = ""                    ' 税率(Hidden)
                HiddenBunruiCd.Value = ""                   ' 分類コード（Hidden）
                TextUriageZeikomiKingaku.Text = ""          ' 売上税込金額
                TextUriageSyouhizeiGaku.Text = ""           ' 売上消費税額
                TextSeikyuusyoHakkoubi.Text = ""            ' 請求書発行日
                TextUriageNengappi.Text = ""                ' 売上年月日
                TextDenpyouUriageNengappi.Text = ""         ' 伝票売上年月日
                TextDenpyouSiireNengappi.Text = ""          ' 伝票仕入年月日
                SelectSeikyuuUmu.SelectedValue = "1"        ' 請求有無
                SelectUriageSyori.SelectedValue = "0"       ' 売上処理
                SelectHattyuusyoKakutei.SelectedValue = "0" ' 発注書確定
                ' Old値に現在の値をセット
                HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue
                TextHattyuusyoKakuninbi.Text = ""           ' 発注書確認日
                TextHattyuusyoKingaku.Text = ""             ' 発注書金額
                TextUriagebi.Text = ""                      ' 売上年月日
                ' 仕入
                TextSiireZeinukiKingaku.Text = ""           ' 仕入税抜金額
                TextSiireSyouhizeiGaku.Text = ""            ' 仕入消費税額
                TextSiireZeikomiKingaku.Text = ""           ' 仕入税込金額

                '請求仕入用カスタムイベント
                RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

                EnabledCtrl(False)

                Return
            End If
        End If

        '********************************************************************
        '★商品マスタと工事価格マスタからの取得を切り替える
        '親画面へイベント通知(工事商品価格設定)
        RaiseEvent GetKojMInfoAction(sender, e, Me.ClientID)

        ' 商品・売上金額・請求有無の自動設定（商品M or 工事価格M）
        If SetSyouhinInfoFromKojM(EarthEnum.emKojKkkActionType.SyouhinCd) = True Then '工事価格M

            ' コントロールの活性化
            EnabledCtrl(True)

            '商品コード(Hidden)
            Me.HiddenSyouhinCd.Value = SelectSyouhinCd.SelectedValue

            '分類コード(Hidden)
            Select Case Me.DispMode
                Case DisplayMode.KAIRYOU
                    ' 改良工事の場合
                    HiddenBunruiCd.Value = EarthConst.SOUKO_CD_KAIRYOU_KOUJI   ' 分類コード
                Case DisplayMode.TUIKA
                    ' 追加改良工事の場合
                    HiddenBunruiCd.Value = EarthConst.SOUKO_CD_TUIKA_KOUJI   ' 分類コード
            End Select

            ' 承諾（仕入）の金額再設定
            SetSiireZeigaku()
            '請求仕入用カスタムイベント
            RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

        Else ' 商品M
            If Me.DispMode = DisplayMode.KAIRYOU Then
                syouhinRec = logic.GetSyouhinInfo(SelectSyouhinCd.SelectedValue, _
                                      EarthEnum.EnumSyouhinKubun.KairyouKouji)
            Else
                syouhinRec = logic.GetSyouhinInfo(SelectSyouhinCd.SelectedValue, _
                                                  EarthEnum.EnumSyouhinKubun.TuikaKouji)
            End If

            ' 商品が取得できた場合、画面項目に設定する
            If syouhinRec Is Nothing Then
                ' コントロールの非活性化
                EnabledCtrl(False)
                ' データのクリア
                Me.SelectSyouhinCd.SelectedValue = String.Empty ' 商品コード
                Me.HiddenZeiritu.Value = String.Empty '税率
                Me.HiddenZeiKbn.Value = String.Empty '税区分
                Me.HiddenBunruiCd.Value = String.Empty '分類コード

                ' 売上
                TextUriageZeinukiKingaku.Text = ""          ' 売上税抜金額
                HiddenZeiritu.Value = ""                    ' 税率(Hidden)
                HiddenBunruiCd.Value = ""                   ' 分類コード（Hidden）
                TextUriageZeikomiKingaku.Text = ""          ' 売上税込金額
                TextUriageSyouhizeiGaku.Text = ""           ' 売上消費税額
                TextSeikyuusyoHakkoubi.Text = ""            ' 請求書発行日
                TextUriageNengappi.Text = ""                ' 売上年月日
                TextDenpyouUriageNengappi.Text = ""         ' 伝票売上年月日
                TextDenpyouSiireNengappi.Text = ""          ' 伝票仕入年月日
                SelectSeikyuuUmu.SelectedValue = "1"        ' 請求有無
                SelectUriageSyori.SelectedValue = "0"       ' 売上処理
                SelectHattyuusyoKakutei.SelectedValue = "0" ' 発注書確定
                ' Old値に現在の値をセット
                HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue
                TextHattyuusyoKakuninbi.Text = ""           ' 発注書確認日
                TextHattyuusyoKingaku.Text = ""             ' 発注書金額
                TextUriagebi.Text = ""                      ' 売上処理日

                ' 仕入
                TextSiireZeinukiKingaku.Text = ""           ' 仕入税抜金額
                TextSiireSyouhizeiGaku.Text = ""            ' 仕入消費税額
                TextSiireZeikomiKingaku.Text = ""           ' 仕入税込金額

                '●エラーメッセージ表示
                tmpErrMsg = Messages.MSG163E.Replace("@PARAM1", "商品マスタ")
                tmpScriptErr = "alert('" & tmpErrMsg & "');"
                ScriptManager.RegisterStartupScript(sender, sender.GetType(), "SetSyouhinInfo", tmpScriptErr, True)

            Else
                ' コントロールの活性化
                EnabledCtrl(True)

                '売上年月日で判断して、正しい税率を取得する
                strSyouhinCd = Me.SelectSyouhinCd.SelectedValue
                If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                    '取得した税区分・税率をセット
                    syouhinRec.Zeiritu = strZeiritu
                    syouhinRec.ZeiKbn = strZeiKbn
                End If

                '税率
                Dim zeiritu As Decimal = IIf(syouhinRec.Zeiritu = Decimal.MinValue, 0, syouhinRec.Zeiritu)
                HiddenZeiritu.Value = zeiritu.ToString()

                '税区分
                Dim zeikbn As String = IIf(syouhinRec.ZeiKbn = Nothing, "", syouhinRec.ZeiKbn)
                HiddenZeiKbn.Value = zeikbn

                HiddenBunruiCd.Value = syouhinRec.SoukoCd
                HiddenSyouhinCd.Value = syouhinRec.SyouhinCd

                ' データの自動設定(請求有りの場合のみ)
                If SelectSeikyuuUmu.SelectedValue = "1" Then
                    '実請求金額 
                    Dim uriage As Integer = IIf(syouhinRec.HyoujunKkk = Integer.MinValue, 0, syouhinRec.HyoujunKkk)
                    TextUriageZeinukiKingaku.Text = uriage.ToString(EarthConst.FORMAT_KINGAKU_1)

                    '金額設定
                    SetKingaku()
                End If

                ' 承諾（仕入）の金額再設定
                SetSiireZeigaku()
                '請求仕入用カスタムイベント
                RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)
            End If
        End If

        ' 税込金額変更を親コントロールに通知する
        RaiseEvent ChangeZeikomiGaku(Me, e, CInt(IIf(TextUriageZeikomiKingaku.Text = "", "0", TextUriageZeikomiKingaku.Text)))
        Me.UpdatePanelSeikyuuSiireLink.Update()

        SetFocus(SelectSyouhinCd)

    End Sub

    ''' <summary>
    ''' 請求有無コンボ変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectSeikyuuUmu_SelectedIndexChanged(ByVal sender As System.Object, _
                                                        ByVal e As System.EventArgs) _
                                                        Handles SelectSeikyuuUmu.SelectedIndexChanged

        'メッセージ用
        Dim tmpErrMsg As String = String.Empty
        Dim tmpScript As String = String.Empty

        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        If SelectSeikyuuUmu.SelectedValue = "0" Then
            ' 税額、税込金額を0にする
            TextUriageZeinukiKingaku.Text = "0"
            TextUriageSyouhizeiGaku.Text = "0"
            TextUriageZeikomiKingaku.Text = "0"
        ElseIf TextUriageZeinukiKingaku.Text.Trim() = "" Or _
               TextUriageZeinukiKingaku.Text.Trim() = "0" Then

            ' 請求有りで税抜金額未入力の場合は自動設定
            Dim logic As New TeibetuSyuuseiLogic
            Dim syouhinRec As New SyouhinMeisaiRecord

            '********************************************************************
            '★商品マスタと工事価格マスタからの取得を切り替える
            '親画面へイベント通知(工事商品価格設定)
            RaiseEvent GetKojMInfoAction(sender, e, Me.ClientID)

            ' 商品・売上金額・請求有無の自動設定（商品M or 工事価格M）
            If SetSyouhinInfoFromKojM(EarthEnum.emKojKkkActionType.SeikyuuUmu) = False Then

                '商品M
                If Me.DispMode = DisplayMode.KAIRYOU Then
                    syouhinRec = logic.GetSyouhinInfo(SelectSyouhinCd.SelectedValue, _
                                          EarthEnum.EnumSyouhinKubun.KairyouKouji)
                Else
                    syouhinRec = logic.GetSyouhinInfo(SelectSyouhinCd.SelectedValue, _
                                                      EarthEnum.EnumSyouhinKubun.TuikaKouji)
                End If

                ' 商品が取得できた場合、価格情報を設定する
                If syouhinRec Is Nothing Then
                    '●エラーメッセージ表示
                    tmpErrMsg = Messages.MSG163E.Replace("@PARAM1", "商品マスタ")
                    tmpScript = "alert('" & tmpErrMsg & "');"
                    ScriptManager.RegisterStartupScript(sender, sender.GetType(), "SetSyouhinInfo", tmpScript, True)

                Else
                    '売上年月日で判断して、正しい税率を取得する
                    strSyouhinCd = Me.SelectSyouhinCd.SelectedValue
                    If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                        '取得した税区分・税率をセット
                        syouhinRec.Zeiritu = strZeiritu
                        syouhinRec.ZeiKbn = strZeiKbn
                    End If

                    'データの自動設定
                    '実請求金額
                    Dim uriage As Integer = IIf(syouhinRec.HyoujunKkk = Integer.MinValue, 0, syouhinRec.HyoujunKkk)
                    '税率
                    Dim zeiritu As Decimal = IIf(syouhinRec.Zeiritu = Decimal.MinValue, 0, syouhinRec.Zeiritu)
                    '税区分
                    Dim zeikbn As String = IIf(syouhinRec.ZeiKbn = Nothing, "", syouhinRec.ZeiKbn)

                    TextUriageZeinukiKingaku.Text = uriage.ToString(EarthConst.FORMAT_KINGAKU_1)
                    HiddenZeiritu.Value = zeiritu.ToString()
                    HiddenZeiKbn.Value = zeikbn

                    '金額設定
                    SetKingaku()
                    End If
            End If
        End If
        SetFocus(SelectSeikyuuUmu)

    End Sub

    ''' <summary>
    ''' 仕入税抜金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSiireZeinukiKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' 仕入税抜金額に入力有りの場合、税額、税込金額を設定する
        If TextSiireZeinukiKingaku.Text.Trim() <> "" Then

            ' 税額、税込金額を計算
            Dim siire As Integer = Integer.Parse(TextSiireZeinukiKingaku.Text.Replace(",", ""))
            Dim zeiritu As Decimal = Decimal.Parse(HiddenZeiritu.Value)
            Dim zeigaku As Integer = Fix(siire * zeiritu)
            Dim zeikomi As Integer = siire + zeigaku

            ' 税額、税込金額を設定
            TextSiireSyouhizeiGaku.Text = zeigaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            TextSiireZeikomiKingaku.Text = zeikomi.ToString(EarthConst.FORMAT_KINGAKU_1)
        Else
            ' 税額、税込金額を空白にする
            TextSiireZeinukiKingaku.Text = ""
            TextSiireSyouhizeiGaku.Text = ""
            TextSiireZeikomiKingaku.Text = ""
        End If

        SetFocus(TextSiireSyouhizeiGaku)

    End Sub

    ''' <summary>
    ''' 仕入消費税額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSiireSyouhizeiGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If sender.Text.Trim() = "" Then
            sender.Text = "0"
        End If

        If TextSiireZeinukiKingaku.Text.Trim() <> "" Then
            '仕入税込金額を計算
            Dim zeikomi As Integer = Integer.Parse(TextSiireZeinukiKingaku.Text.Replace(",", "")) + Integer.Parse(sender.Text.Replace(",", ""))
            '仕入税込額更新
            TextSiireZeikomiKingaku.Text = zeikomi.ToString(EarthConst.FORMAT_KINGAKU_1)
        Else
            ' 税額、税込金額を空白にする
            TextSiireZeinukiKingaku.Text = ""
            TextSiireSyouhizeiGaku.Text = ""
            TextSiireZeikomiKingaku.Text = ""
        End If

        'フォーカスセット
        SetFocus(TextDenpyouSiireNengappi)

    End Sub

    ''' <summary>
    ''' 売上税抜金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextUriageZeinukiKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' 売上税抜金額に入力有りの場合、税額、税込金額を設定する
        If TextUriageZeinukiKingaku.Text.Trim() <> "" Then

            '金額設定
            SetKingaku()

            ' 税込金額変更を親コントロールに通知する
            RaiseEvent ChangeZeikomiGaku(Me, e, Integer.Parse(TextUriageZeikomiKingaku.Text.Replace(",", "")))
        Else
            ' 税額、税込金額を空白にする
            TextUriageSyouhizeiGaku.Text = ""
            TextUriageZeikomiKingaku.Text = ""
            ' 税込金額変更を親コントロールに通知する
            RaiseEvent ChangeZeikomiGaku(Me, e, 0)
        End If

        SetFocus(TextUriageSyouhizeiGaku)

    End Sub

    ''' <summary>
    ''' 売上消費税額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextUriageSyouhizeiGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim strTmpUriGaku As String = Me.TextUriageZeinukiKingaku.Text.Trim

        ' 売上税抜金額に入力有りの場合、税額、税込金額を設定する
        If strTmpUriGaku <> String.Empty And strTmpUriGaku <> "0" Then

            If TextUriageSyouhizeiGaku.Text.Trim() = "" Then
                TextUriageSyouhizeiGaku.Text = "0"
            End If

            '金額設定
            SetKingaku(True)

            ' 税込金額変更を親コントロールに通知する
            RaiseEvent ChangeZeikomiGaku(Me, e, Integer.Parse(TextUriageZeikomiKingaku.Text.Replace(",", "")))

        ElseIf strTmpUriGaku = "0" Then
            ' 税額、税込金額を空白にする
            TextUriageSyouhizeiGaku.Text = "0"
            TextUriageZeikomiKingaku.Text = "0"
            ' 税込金額変更を親コントロールに通知する
            RaiseEvent ChangeZeikomiGaku(Me, e, 0)

        Else
            ' 税額、税込金額を空白にする
            TextUriageSyouhizeiGaku.Text = ""
            TextUriageZeikomiKingaku.Text = ""
            ' 税込金額変更を親コントロールに通知する
            RaiseEvent ChangeZeikomiGaku(Me, e, 0)
        End If

        SetFocus(TextDenpyouUriageNengappi)

    End Sub

    ''' <summary>
    ''' 売上処理変更時のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectUriageSyori_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If SelectUriageSyori.SelectedValue = "0" Then
            ' 売上処理日をクリア
            TextUriagebi.Text = ""
        Else
            ' システム日付をセット
            TextUriagebi.Text = Date.Now.ToString("yyyy/MM/dd")
        End If
        SetFocus(SelectUriageSyori)
    End Sub

    ''' <summary>
    ''' 発注書金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextHattyuusyoKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' 発注書自動確定処理
        HattyuuAfterUpdate(sender)
        SetFocus(SelectHattyuusyoKakutei)

    End Sub

    ''' <summary>
    ''' 発注書確定変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectHattyuusyoKakutei_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' 発注書自動確定処理
        FunCheckHKakutei(1, sender)

        ' Old値に現在の値をセット
        HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue

        SetFocus(SelectHattyuusyoKakutei)
    End Sub

    ''' <summary>
    ''' 税込金額の変更を親コントロールに通知する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ChangeZeikomiGaku(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs, _
                                   ByVal zeikomigaku As Integer)

    ''' <summary>
    ''' 工事会社請求チェックボックス変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub CheckKoujigaisyaSeikyuu_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '請求仕入用カスタムイベント
        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

    End Sub

    ''' <summary>
    ''' 売上年月日変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextUriageNengappi_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        '売上年月日
        If Me.TextUriageNengappi.Text <> String.Empty Then '未入力
            '伝票売上年月日
            If Me.TextDenpyouUriageNengappi.Text = String.Empty Then
                '売上年月日をセット
                Me.TextDenpyouUriageNengappi.Text = Me.TextUriageNengappi.Text
                TextDenpyouUriageNengappi_TextChanged(sender, e)
            End If
            '伝票仕入年月日
            If Me.TextDenpyouSiireNengappi.Text = String.Empty Then
                '売上年月日をセット
                Me.TextDenpyouSiireNengappi.Text = Me.TextUriageNengappi.Text
            End If

            ' 税区分・税率を再取得
            strSyouhinCd = Me.SelectSyouhinCd.SelectedValue

            If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                ' 取得した税区分をセット
                Me.HiddenZeiKbn.Value = strZeiKbn
                Me.HiddenZeiritu.Value = strZeiritu

                ' 承諾（仕入）の税額再設定
                SetSiireZeigaku()
                ' 実請求の金額再設定
                SetKingaku()
                ' 税込金額変更を親コントロールに通知する
                RaiseEvent ChangeZeikomiGaku(Me, e, CInt(IIf(TextUriageZeikomiKingaku.Text = "", "0", TextUriageZeikomiKingaku.Text)))
            End If

        Else
            '売上年月日未入力の場合

            ' 税区分・税率を再取得
            strSyouhinCd = Me.SelectSyouhinCd.SelectedValue

            If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                ' 取得した税区分をセット
                Me.HiddenZeiKbn.Value = strZeiKbn
                Me.HiddenZeiritu.Value = strZeiritu

                ' 承諾（仕入）の税額再設定
                SetSiireZeigaku()
                ' 実請求の金額再設定
                SetKingaku()
                ' 税込金額変更を親コントロールに通知する
                RaiseEvent ChangeZeikomiGaku(Me, e, CInt(IIf(TextUriageZeikomiKingaku.Text = "", "0", TextUriageZeikomiKingaku.Text)))
            End If
        End If

        Me.UpdatePanelDenpyouUriageNengappi.Update()

        SetFocus(TextUriageNengappi)

    End Sub

    ''' <summary>
    ''' 伝票売上年月日変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextDenpyouUriageNengappi_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        '請求仕入用カスタムイベント
        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

        '伝票売上年月日
        If Me.TextDenpyouUriageNengappi.Text <> String.Empty Then
            '請求締め日の設定
            Me.SeikyuuSiireLinkCtrl.SetSeikyuuSimeDate(Me.SelectSyouhinCd.SelectedValue, Me.CheckKoujigaisyaSeikyuu.Checked, Me.TextKoujigaisyaCd.Text)

            ' 請求書発行日取得
            Dim strHakDate As String = Me.SeikyuuSiireLinkCtrl.GetSeikyuusyoHakkouDate(Me.TextDenpyouUriageNengappi.Text)
            Me.TextSeikyuusyoHakkoubi.Text = strHakDate
        Else
            Me.TextSeikyuusyoHakkoubi.Text = String.Empty
        End If

        '請求有無にフォーカス
        SetFocus(TextSeikyuusyoHakkoubi)
    End Sub

#End Region

#Region "プライベートメソッド"

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispAction()
        'イベントハンドラを設定
        Dim onFocusPostBackScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScript As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this))__doPostBack(this.id,'');}else{checkNumberAddFig(this);}"
        Dim onFocusScript As String = "removeFig(this);"
        Dim onBlurScript As String = "checkNumberAddFig(this);"
        Dim checkDate As String = "checkDate(this);"
        Dim onFocusPostBackScriptDate As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScriptDate As String = "if(checkTempValueForOnBlur(this)){if(checkDate(this))__doPostBack(this.id,'');}else{checkDate(this);}"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '売上税抜金額
        TextUriageZeinukiKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextUriageZeinukiKingaku.Attributes("onblur") = onBlurPostBackScript
        TextUriageZeinukiKingaku.Attributes("onkeydown") = disabledOnkeydown
        '売上消費税額
        TextUriageSyouhizeiGaku.Attributes("onfocus") = onFocusPostBackScript
        TextUriageSyouhizeiGaku.Attributes("onblur") = onBlurPostBackScript
        TextUriageSyouhizeiGaku.Attributes("onkeydown") = disabledOnkeydown
        '発注書金額
        TextHattyuusyoKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextHattyuusyoKingaku.Attributes("onblur") = onBlurPostBackScript
        TextHattyuusyoKingaku.Attributes("onkeydown") = disabledOnkeydown
        '仕入税抜金額
        TextSiireZeinukiKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextSiireZeinukiKingaku.Attributes("onblur") = onBlurPostBackScript
        TextSiireZeinukiKingaku.Attributes("onkeydown") = disabledOnkeydown
        '仕入消費税額
        TextSiireSyouhizeiGaku.Attributes("onfocus") = onFocusPostBackScript
        TextSiireSyouhizeiGaku.Attributes("onblur") = onBlurPostBackScript
        TextSiireSyouhizeiGaku.Attributes("onkeydown") = disabledOnkeydown
        '請求書発行日
        TextSeikyuusyoHakkoubi.Attributes("onblur") = checkDate
        TextSeikyuusyoHakkoubi.Attributes("onkeydown") = disabledOnkeydown
        '売上年月日
        TextUriageNengappi.Attributes("onfocus") = onFocusPostBackScriptDate
        TextUriageNengappi.Attributes("onblur") = onBlurPostBackScriptDate
        TextUriageNengappi.Attributes("onkeydown") = disabledOnkeydown
        '伝票売上年月日
        TextDenpyouUriageNengappi.Attributes("onfocus") = onFocusPostBackScriptDate
        TextDenpyouUriageNengappi.Attributes("onblur") = onBlurPostBackScriptDate
        TextDenpyouUriageNengappi.Attributes("onkeydown") = disabledOnkeydown
        '伝票仕入年月日
        TextDenpyouSiireNengappi.Attributes("onblur") = checkDate
        TextDenpyouSiireNengappi.Attributes("onkeydown") = disabledOnkeydown
        '発注書確認日
        TextHattyuusyoKakuninbi.Attributes("onblur") = checkDate
        TextHattyuusyoKakuninbi.Attributes("onkeydown") = disabledOnkeydown
        '工事会社コード
        TextKoujigaisyaCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){checkNumber(this);}"
        TextKoujigaisyaCd.Attributes("onkeydown") = disabledOnkeydown

    End Sub

#Region "邸別請求レコード編集"
    ''' <summary>
    ''' 邸別請求レコードデータをコントロールにセットします
    ''' </summary>
    ''' <param name="data"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlData(ByVal data As TeibetuSeikyuuRecord)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetCtrlData", _
                                            data)

        ' 区分（Hidden）
        HiddenKubun.Value = data.Kbn
        ' 番号（保証書NO）（Hidden）
        HiddenBangou.Value = data.HosyousyoNo
        ' 商品コード（Hidden）
        HiddenSyouhinCd.Value = data.SyouhinCd
        '商品コードの存在チェック
        If cLogic.ChkDropDownList(SelectSyouhinCd, data.SyouhinCd) Then
            SelectSyouhinCd.SelectedValue = cLogic.GetDispStr(data.SyouhinCd) '商品コード
        Else '未存在の場合、項目追加
            SelectSyouhinCd.Items.Add(New ListItem(data.SyouhinCd & ":" & data.SyouhinMei, data.SyouhinCd)) '商品コード
            SelectSyouhinCd.SelectedValue = data.SyouhinCd  '選択状態
        End If

        ' 税抜売上金額
        TextUriageZeinukiKingaku.Text = IIf(data.UriGaku = Integer.MinValue, _
                                            0, _
                                            data.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' 売上消費税額
        TextUriageSyouhizeiGaku.Text = IIf(data.UriageSyouhiZeiGaku = Integer.MinValue, 0, Format(data.UriageSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        ' 税込売上金額
        TextUriageZeikomiKingaku.Text = IIf(data.ZeikomiUriGaku = Integer.MinValue, _
                                            0, _
                                            data.ZeikomiUriGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' 請求書発行日
        TextSeikyuusyoHakkoubi.Text = IIf(data.SeikyuusyoHakDate = Date.MinValue, _
                                          "", _
                                          data.SeikyuusyoHakDate.ToString("yyyy/MM/dd"))
        ' 売上年月日
        TextUriageNengappi.Text = IIf(data.UriDate = Date.MinValue, _
                                      "", _
                                      data.UriDate.ToString("yyyy/MM/dd"))
        ' 伝票売上年月日(参照用)
        TextDenpyouUriageNengappiDisplay.Text = IIf(data.DenpyouUriDate = Date.MinValue, _
                                                    "", _
                                                    data.DenpyouUriDate.ToString("yyyy/MM/dd"))
        ' 伝票売上年月日(修正用)
        TextDenpyouUriageNengappi.Text = IIf(data.DenpyouUriDate = Date.MinValue, _
                                             "", _
                                             data.DenpyouUriDate.ToString("yyyy/MM/dd"))
        ' 伝票仕入年月日(参照用)
        TextDenpyouSiireNengappiDisplay.Text = IIf(data.DenpyouSiireDate = Date.MinValue, _
                                                   "", _
                                                   data.DenpyouSiireDate.ToString("yyyy/MM/dd"))
        ' 伝票仕入年月日(修正用)
        TextDenpyouSiireNengappi.Text = IIf(data.DenpyouSiireDate = Date.MinValue, _
                                            "", _
                                            data.DenpyouSiireDate.ToString("yyyy/MM/dd"))
        ' 請求有無ドロップダウン
        SelectSeikyuuUmu.SelectedValue = IIf(data.SeikyuuUmu = 1, "1", "0")
        ' 売上処理ドロップダウン
        SelectUriageSyori.SelectedValue = IIf(data.UriKeijyouFlg = 1, "1", "0")
        ' 売上日（編集不可）
        TextUriagebi.Text = IIf(data.UriKeijyouDate = Date.MinValue, _
                                "", _
                                data.UriKeijyouDate.ToString("yyyy/MM/dd"))
        ' 発注書金額
        TextHattyuusyoKingaku.Text = IIf(data.HattyuusyoGaku = Integer.MinValue, _
                                         "", _
                                         data.HattyuusyoGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' 発注書金額(画面起動時の値)
        HiddenHattyuusyoKingakuOld.Value = TextHattyuusyoKingaku.Text

        ' 発注書確定ドロップダウン
        SelectHattyuusyoKakutei.SelectedValue = IIf(data.HattyuusyoKakuteiFlg = 1, "1", "0")
        ' Old値に現在の値をセット
        HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue

        '発注書確定済みの場合、発注書金額を編集不可に設定
        If SelectHattyuusyoKakutei.SelectedValue = "1" Then
            EnableTextBox(TextHattyuusyoKingaku, False)

            ' 発注書確定済みの場合、経理権限が無い場合、発注書確定も非活性化
            If HiddenKeiriGyoumuKengen.Value = "0" Then
                EnableDropDownList(SelectHattyuusyoKakutei, False)
            End If
        End If

        ' 発注書確認日
        TextHattyuusyoKakuninbi.Text = IIf(data.HattyuusyoKakuninDate = Date.MinValue, _
                                           "", _
                                           data.HattyuusyoKakuninDate.ToString("yyyy/MM/dd"))
        ' 税抜仕入金額
        TextSiireZeinukiKingaku.Text = IIf(data.SiireGaku = Integer.MinValue, _
                                           0, _
                                           data.SiireGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' 仕入消費税額
        If TextSiireZeinukiKingaku.Text <> "" Then
            TextSiireSyouhizeiGaku.Text = IIf(data.SiireSyouhiZeiGaku = Integer.MinValue, _
                                              0, _
                                              data.SiireSyouhiZeiGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        End If
        ' 仕入税込金額
        TextSiireZeikomiKingaku.Text = (data.SiireGaku + data.SiireSyouhiZeiGaku).ToString(EarthConst.FORMAT_KINGAKU_1)
        ' 税率（Hidden）
        HiddenZeiritu.Value = data.Zeiritu.ToString()
        ' 税区分（Hidden）
        HiddenZeiKbn.Value = IIf(data.ZeiKbn Is Nothing, "", data.ZeiKbn)
        ' 更新日時（Hidden）
        If data.UpdDatetime = DateTime.MinValue Then
            HiddenUpdDatetime.Value = data.AddDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)
        Else
            HiddenUpdDatetime.Value = data.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)
        End If

        '請求先/仕入先リンクへ邸別請求レコードの値をセット
        Me.SeikyuuSiireLinkCtrl.SetSeikyuuSiireLinkFromTeibetuRec(data)

    End Sub

    ''' <summary>
    ''' 邸別請求レコードデータにコントロールの内容をセットします
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCtrlData() As TeibetuSeikyuuRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCtrlData")

        ' 商品コード未設定時、何もセットしない
        If SelectSyouhinCd.SelectedValue = "" Then
            Return Nothing
        End If

        ' 邸別請求レコード
        Dim record As New TeibetuSeikyuuRecord

        ' 区分
        If ViewState("Kbn") Is Nothing Then
            record.Kbn = HiddenKubun.Value
        Else
            record.Kbn = ViewState("Kbn")
        End If

        ' 保証書NO
        If ViewState("no") Is Nothing Then
            record.HosyousyoNo = HiddenBangou.Value
        Else
            record.HosyousyoNo = ViewState("no")
        End If

        ' 商品コード
        record.SyouhinCd = SelectSyouhinCd.SelectedValue
        ' 商品名
        record.SyouhinMei = SelectSyouhinCd.Text
        ' 確定区分(固定)
        record.KakuteiKbn = Integer.MinValue
        ' 工務店請求額
        record.KoumutenSeikyuuGaku = 0
        ' 実請求金額
        Dim strUriGaku As String = TextUriageZeinukiKingaku.Text.Replace(",", "").Trim()
        If strUriGaku.Trim() = "" Then
            record.UriGaku = Integer.MinValue
        Else
            record.UriGaku = Integer.Parse(strUriGaku)
        End If
        ' 税率
        record.Zeiritu = Decimal.Parse(HiddenZeiritu.Value)
        ' 税区分
        record.ZeiKbn = HiddenZeiKbn.Value
        ' 消費税額
        Dim strSyouhizeiGaku As String = TextUriageSyouhizeiGaku.Text.Replace(",", "").Trim()
        If strSyouhizeiGaku.Trim() = "" Then
            record.UriageSyouhiZeiGaku = Integer.MinValue
        Else
            record.UriageSyouhiZeiGaku = Integer.Parse(strSyouhizeiGaku)
        End If
        ' 仕入消費税額
        Dim strSiireSyouhizeiGaku As String = TextSiireSyouhizeiGaku.Text.Replace(",", "").Trim()
        If strSiireSyouhizeiGaku.Trim() = "" Then
            record.SiireSyouhiZeiGaku = Integer.MinValue
        Else
            record.SiireSyouhiZeiGaku = Integer.Parse(strSiireSyouhizeiGaku)
        End If
        ' 承諾書金額
        Dim strSiireGaku As String = TextSiireZeinukiKingaku.Text.Replace(",", "").Trim()
        If strSiireGaku.Trim() = "" Then
            record.SiireGaku = Integer.MinValue
        Else
            record.SiireGaku = Integer.Parse(strSiireGaku)
        End If
        ' 請求書発行日
        If Not TextSeikyuusyoHakkoubi.Text.Trim() = "" Then
            record.SeikyuusyoHakDate = Date.Parse(TextSeikyuusyoHakkoubi.Text)
        End If
        ' 売上年月日
        If Not TextUriageNengappi.Text.Trim() = "" Then
            record.UriDate = Date.Parse(TextUriageNengappi.Text)
        End If
        ' 伝票売上年月日(修正用)
        If Not TextDenpyouUriageNengappi.Text.Trim() = "" Then
            record.DenpyouUriDate = Date.Parse(TextDenpyouUriageNengappi.Text)
        End If
        ' 伝票仕入年月日(修正用)
        If Not TextDenpyouSiireNengappi.Text.Trim() = "" Then
            record.DenpyouSiireDate = Date.Parse(TextDenpyouSiireNengappi.Text)
        End If
        ' 請求有無
        record.SeikyuuUmu = IIf(SelectSeikyuuUmu.SelectedValue = "1", 1, 0)
        ' 売上処理
        record.UriKeijyouFlg = IIf(SelectUriageSyori.SelectedValue = "1", 1, 0)
        ' 売上計上日
        If Not TextUriagebi.Text.Trim() = "" Then
            record.UriKeijyouDate = Date.Parse(TextUriagebi.Text)
        End If
        ' 発注書金額
        Dim strHattyuusyoGaku As String = TextHattyuusyoKingaku.Text.Replace(",", "").Trim()
        If strHattyuusyoGaku.Trim() = "" Then
            record.HattyuusyoGaku = Integer.MinValue
        Else
            record.HattyuusyoGaku = Integer.Parse(strHattyuusyoGaku)
        End If
        ' 発注書確定
        record.HattyuusyoKakuteiFlg = IIf(SelectHattyuusyoKakutei.SelectedValue = "1", 1, 0)
        ' 発注書確認日
        If Not TextHattyuusyoKakuninbi.Text.Trim() = "" Then
            record.HattyuusyoKakuninDate = Date.Parse(TextHattyuusyoKakuninbi.Text)
        End If
        ' 画面表示NO
        record.GamenHyoujiNo = 1
        ' 分類コード
        record.BunruiCd = HiddenBunruiCd.Value
        ' 更新日時（Hidden）
        If HiddenUpdDatetime.Value = "" Then
            record.UpdDatetime = DateTime.MinValue
        Else
            record.UpdDatetime = HiddenUpdDatetime.Value
        End If
        ' 更新者ＩＤ
        record.UpdLoginUserId = HiddenLoginUserId.Value

        '請求先/仕入先リンクの情報を邸別請求レコードへセット
        Me.SeikyuuSiireLinkCtrl.SetTeibetuRecFromSeikyuuSiireLink(record)

        Return record

    End Function

#End Region

    ''' <summary>
    ''' Ajax動作時のフォーカスセット
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub SetFocus(ByVal ctrl As Control)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetFocus", _
                                                    ctrl)
        'フォーカスセット
        masterAjaxSM.SetFocus(ctrl)
    End Sub

    ''' <summary>
    ''' コントロールの活性制御
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnabledCtrl(ByVal enabled As Boolean)

        SelectSeikyuuUmu.Enabled = enabled             ' 請求有無
        SelectUriageSyori.Enabled = enabled            ' 売上処理
        SelectHattyuusyoKakutei.Enabled = enabled      ' 発注書確定

        TextUriageZeinukiKingaku.Enabled = enabled     ' 売上税抜金額
        TextUriageSyouhizeiGaku.Enabled = enabled      ' 売上消費税額
        TextUriageZeikomiKingaku.Enabled = enabled     ' 売上税込金額
        TextSeikyuusyoHakkoubi.Enabled = enabled       ' 請求書発行日
        TextUriageNengappi.Enabled = enabled           ' 売上年月日
        TextDenpyouUriageNengappi.Enabled = enabled    ' 伝票売上年月日
        TextDenpyouSiireNengappi.Enabled = enabled     ' 伝票仕入年月日
        TextHattyuusyoKakuninbi.Enabled = enabled      ' 発注書確認日
        TextHattyuusyoKingaku.Enabled = enabled        ' 発注書金額
        TextSiireZeinukiKingaku.Enabled = enabled      ' 仕入税抜金額
        TextSiireSyouhizeiGaku.Enabled = enabled       ' 仕入消費税額

    End Sub

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

    ''' <summary>
    ''' ドロップダウン単体の活性制御
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableDropDownList(ByRef ctrl As DropDownList, _
                              ByVal enabled As Boolean)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnableDropDownList", _
                                                    ctrl, enabled)

        ctrl.Enabled = enabled

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

    ''' <summary>
    ''' チェックボックス単体の活性制御
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableCheckBox(ByRef ctrl As CheckBox, _
                              ByVal enabled As Boolean)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnableDropDownList", _
                                                    ctrl, enabled)

        ctrl.Enabled = enabled

        If enabled Then
            ctrl.TabIndex = 0
        Else
            ctrl.TabIndex = -1
        End If

    End Sub

    ''' <summary>
    ''' コントロールの有効無効を切替える[権限別]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EnabledCtrlKengen()

        ' 経理権限が無い場合、発注書以外非活性
        If HiddenKeiriGyoumuKengen.Value = "0" Then
            ButtonKoujigaisyaKensaku.Visible = False '工事会社検索ボタン
            CheckKoujigaisyaSeikyuu.Visible = True '工事会社請求
            EnableTextBox(TextKoujigaisyaCd, False) '工事会社コード
            EnableDropDownList(SelectSyouhinCd, False) '商品コード
            EnableTextBox(TextUriageZeinukiKingaku, False) '売上税抜金額
            EnableTextBox(TextUriageSyouhizeiGaku, False) '売上消費税額
            EnableTextBox(TextSiireZeinukiKingaku, False) '仕入税抜金額
            EnableTextBox(TextSiireSyouhizeiGaku, False) '仕入消費税額
            EnableTextBox(TextSeikyuusyoHakkoubi, False) '請求書発行日
            EnableTextBox(TextUriageNengappi, False) '売上年月日
            EnableTextBox(TextDenpyouUriageNengappi, False) '伝票売上年月日
            EnableTextBox(TextDenpyouSiireNengappi, False) '伝票仕入年月日
            EnableTextBox(TextHattyuusyoKakuninbi, False) '発注書確認日
            EnableDropDownList(SelectSeikyuuUmu, False) '請求有無
            EnableDropDownList(SelectUriageSyori, False) '売上処理
            EnableCheckBox(CheckKoujigaisyaSeikyuu, False) '工事会社請求

        End If

        ' 経理権限及び発注書管理権限が無い場合、非活性化
        If HiddenHattyuusyoKanriKengen.Value = "0" And _
           HiddenKeiriGyoumuKengen.Value = "0" Then
            EnableTextBox(TextHattyuusyoKingaku, False) '発注書金額
            EnableDropDownList(SelectHattyuusyoKakutei, False) '発注書確定
        End If

    End Sub

#Region "画面コントロールの値を結合し、文字列化する"

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringUriage() As String

        Dim sb As New StringBuilder

        sb.Append(SelectSyouhinCd.SelectedValue & EarthConst.SEP_STRING)    '商品コード
        sb.Append(TextUriageZeinukiKingaku.Text & EarthConst.SEP_STRING)    '売上金額(改良売上金額_税抜金額)
        sb.Append(TextUriageSyouhizeiGaku.Text & EarthConst.SEP_STRING)     '消費税額
        sb.Append(HiddenZeiKbn.Value & EarthConst.SEP_STRING)               '税区分(非表示項目)
        sb.Append(TextDenpyouUriageNengappi.Text & EarthConst.SEP_STRING)   '伝票売上年月日
        sb.Append(TextUriageNengappi.Text & EarthConst.SEP_STRING)          '売上年月日
        sb.Append(SelectUriageSyori.SelectedValue & EarthConst.SEP_STRING)  '売上計上FLG
        sb.Append(TextSeikyuusyoHakkoubi.Text & EarthConst.SEP_STRING)      '請求書発行日

        sb.Append(SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value & EarthConst.SEP_STRING)          '請求先コード
        sb.Append(SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc.Value & EarthConst.SEP_STRING)         '請求先枝番
        sb.Append(SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn.Value & EarthConst.SEP_STRING)         '請求先区分

        Return (sb.ToString)

    End Function

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringSiire() As String

        Dim sb As New StringBuilder

        sb.Append(SelectSyouhinCd.SelectedValue & EarthConst.SEP_STRING)    '商品コード
        sb.Append(TextSiireZeinukiKingaku.Text & EarthConst.SEP_STRING)     '仕入金額(改良仕入金額_税抜金額)
        sb.Append(TextSiireSyouhizeiGaku.Text & EarthConst.SEP_STRING)      '仕入消費税額
        sb.Append(TextDenpyouSiireNengappi.Text & EarthConst.SEP_STRING)    '伝票仕入年月日
        sb.Append(HiddenZeiKbn.Value & EarthConst.SEP_STRING)               '税区分(非表示項目)
        sb.Append(SelectUriageSyori.SelectedValue & EarthConst.SEP_STRING)  '売上計上FLG

        sb.Append(SeikyuuSiireLinkCtrl.AccTysKaisyaCd.Value & EarthConst.SEP_STRING)            '調査会社コード
        sb.Append(SeikyuuSiireLinkCtrl.AccTysKaisyaJigyousyoCd.Value & EarthConst.SEP_STRING)   '調査会社事業所コード

        Return (sb.ToString)

    End Function

#Region "画面コントロールの変更箇所対応"

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する(全項目)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringAll() As String

        Dim sb As New StringBuilder

        '●非表示項目１
        sb.Append(Me.HiddenKubun.Value & EarthConst.SEP_STRING)   '区分
        sb.Append(Me.HiddenBangou.Value & EarthConst.SEP_STRING)   '保証書NO
        sb.Append(Me.HiddenBunruiCd.Value & EarthConst.SEP_STRING)   '分類コード

        '●表示項目
        sb.Append(Me.TextKoujigaisyaCd.Text & EarthConst.SEP_STRING) '工事会社
        sb.Append(CStr(Me.CheckKoujigaisyaSeikyuu.Checked) & EarthConst.SEP_STRING) '工事会社請求有無
        sb.Append(Me.SelectSyouhinCd.SelectedValue & EarthConst.SEP_STRING)   '商品コード

        sb.Append(Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value & EarthConst.SEP_STRING)   '請求先コード
        sb.Append(Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc.Value & EarthConst.SEP_STRING)   '請求先枝番
        sb.Append(Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn.Value & EarthConst.SEP_STRING)   '請求先区分
        sb.Append(Me.SeikyuuSiireLinkCtrl.AccTysKaisyaCd.Value & EarthConst.SEP_STRING)   '調査会社コード
        sb.Append(Me.SeikyuuSiireLinkCtrl.AccTysKaisyaJigyousyoCd.Value & EarthConst.SEP_STRING)   '調査会社事業所コード

        sb.Append(Me.TextSiireZeinukiKingaku.Text & EarthConst.SEP_STRING)   '承諾書金額(仕入)
        sb.Append(Me.TextSiireSyouhizeiGaku.Text & EarthConst.SEP_STRING)   '仕入消費税額
        sb.Append(Me.TextDenpyouSiireNengappiDisplay.Text & EarthConst.SEP_STRING)   '伝票仕入年月日
        sb.Append(Me.TextDenpyouSiireNengappi.Text & EarthConst.SEP_STRING)   '伝票仕入年月日修正

        sb.Append(Me.TextUriageZeinukiKingaku.Text & EarthConst.SEP_STRING)   '実請求税抜金額
        sb.Append(Me.TextUriageSyouhizeiGaku.Text & EarthConst.SEP_STRING)   '消費税額(売上)
        sb.Append(Me.TextUriageZeikomiKingaku.Text & EarthConst.SEP_STRING)   '税込金額(売上)
        sb.Append(Me.TextDenpyouUriageNengappiDisplay.Text & EarthConst.SEP_STRING)   '伝票売上年月日
        sb.Append(Me.TextDenpyouUriageNengappi.Text & EarthConst.SEP_STRING)   '伝票売上年月日修正
        sb.Append(Me.TextUriageNengappi.Text & EarthConst.SEP_STRING)   '売上年月日
        sb.Append(Me.SelectUriageSyori.SelectedValue & EarthConst.SEP_STRING)   '売上処理FLG(売上計上FLG)
        sb.Append(Me.TextUriagebi.Text & EarthConst.SEP_STRING)   '売上処理日(売上計上日)

        sb.Append(Me.TextSeikyuusyoHakkoubi.Text & EarthConst.SEP_STRING)   '請求書発行日
        sb.Append(Me.SelectSeikyuuUmu.SelectedValue & EarthConst.SEP_STRING)   '請求有無

        sb.Append(Me.TextHattyuusyoKingaku.Text & EarthConst.SEP_STRING)   '発注書金額
        sb.Append(Me.SelectHattyuusyoKakutei.SelectedValue & EarthConst.SEP_STRING)   '発注書確定FLG
        sb.Append(Me.TextHattyuusyoKakuninbi.Text & EarthConst.SEP_STRING)   '発注書確認日

        '●非表示項目２
        sb.Append(Me.HiddenZeiKbn.Value & EarthConst.SEP_STRING)   '税区分(税率)

        'KEY情報の取得
        Me.getCtrlValuesStringAllKey()

        Return (sb.ToString)
    End Function

    ''' <summary>
    ''' 邸別請求テーブルの全項目情報を結合し、文字列化する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getCtrlValuesStringAllKey()
        Dim dic As New Dictionary(Of String, String)
        Dim strRecString As String = String.Empty
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic

        '画面表示時のDB値の連結値を取得
        If Me.HiddenKeyValue.Value = String.Empty Then

            With dic
                .Add("0", "区分")
                .Add("1", "保証書NO")
                .Add("2", "分類コード")
                .Add("3", "工事会社コード")
                .Add("4", "工事会社請求")
                .Add("5", "商品コード")
                .Add("6", "請求先コード")
                .Add("7", "請求先枝番")
                .Add("8", "請求先区分")
                .Add("9", "調査会社コード")
                .Add("10", "調査会社事業所コード")
                .Add("11", "承諾書金額(仕入)")
                .Add("12", "仕入消費税額")
                .Add("13", "伝票仕入年月日")
                .Add("14", "伝票仕入年月日修正")
                .Add("15", "実請求税抜金額")
                .Add("16", "消費税額(売上)")
                .Add("17", "税込金額(売上)")
                .Add("18", "伝票売上年月日")
                .Add("19", "伝票売上年月日修正")
                .Add("20", "売上年月日")
                .Add("21", "売上処理FLG(売上計上FLG)")
                .Add("22", "売上処理日(売上計上日)")
                .Add("23", "請求書発行日")
                .Add("24", "請求有無")
                .Add("25", "発注書金額")
                .Add("26", "発注書確定FLG")
                .Add("27", "発注書確認日")
                .Add("28", "税区分(税率)")
            End With

            strRecString = iLogic.getJoinString(dic.Values.GetEnumerator)
            Me.HiddenKeyValue.Value = strRecString
        End If

    End Sub

    ''' <summary>
    ''' 邸別請求テーブルの全項目情報を管理し、対象の項目が存在した場合、背景色を赤色に変更する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getCtrlValuesStringAllKeyCtrlId(ByVal strKey As String)
        Dim objRet As New Object
        Dim dic As New Dictionary(Of String, Object)
        Dim strRecString As String = String.Empty
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic

        'Key毎にオブジェクトをセット
        With dic
            .Add("0", Me.HiddenKubun)
            .Add("1", Me.HiddenBangou)
            .Add("2", Me.HiddenBunruiCd)
            .Add("3", Me.TextKoujigaisyaCd)
            .Add("4", Me.CheckKoujigaisyaSeikyuu)
            .Add("5", Me.SelectSyouhinCd)
            .Add("6", Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd)
            .Add("7", Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc)
            .Add("8", Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn)
            .Add("9", Me.SeikyuuSiireLinkCtrl.AccTysKaisyaCd)
            .Add("10", Me.SeikyuuSiireLinkCtrl.AccTysKaisyaJigyousyoCd)
            .Add("11", Me.TextSiireZeinukiKingaku)
            .Add("12", Me.TextSiireSyouhizeiGaku)
            .Add("13", Me.TextDenpyouSiireNengappiDisplay)
            .Add("14", Me.TextDenpyouSiireNengappi)
            .Add("15", Me.TextUriageZeinukiKingaku)
            .Add("16", Me.TextUriageSyouhizeiGaku)
            .Add("17", Me.TextUriageZeikomiKingaku)
            .Add("18", Me.TextDenpyouUriageNengappiDisplay)
            .Add("19", Me.TextDenpyouUriageNengappi)
            .Add("20", Me.TextUriageNengappi)
            .Add("21", Me.SelectUriageSyori)
            .Add("22", Me.TextUriagebi)
            .Add("23", Me.TextSeikyuusyoHakkoubi)
            .Add("24", Me.SelectSeikyuuUmu)
            .Add("25", Me.TextHattyuusyoKingaku)
            .Add("26", Me.SelectHattyuusyoKakutei)
            .Add("27", Me.TextHattyuusyoKakuninbi)
            .Add("28", Me.HiddenZeiKbn)
        End With

        '背景色変更処理
        Call cLogic.ChgHenkouCtrlBgColor(dic, strKey)

    End Sub

    ''' <summary>
    ''' 邸別請求テーブルの全項目情報を結合し、文字列化する。
    ''' 変更箇所の背景色を変更する
    ''' </summary>
    ''' <param name="strKey">KEY値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringAllKeyName(ByVal strKey As String, ByVal strCtrlNameKey As String) As String
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic
        Dim MyLogic As New TeibetuSyuuseiLogic

        Dim strKeyValues() As String
        Dim strHiddenKeyValues() As String
        Dim strRet As String = String.Empty

        Dim intCnt As Integer = 0
        Dim strTmp1 As String = String.Empty
        Dim strTmp2 As String = String.Empty
        Dim dicItem1 As Dictionary(Of String, String)
        Dim strColorId As String = String.Empty

        If strKey = String.Empty Then
            Return strRet
            Exit Function
        End If

        'DB値
        strKeyValues = iLogic.getArrayFromDollarSep(strKey)

        '項目名を取得
        strHiddenKeyValues = iLogic.getArrayFromDollarSep(strCtrlNameKey)
        dicItem1 = MyLogic.getDicItem(strHiddenKeyValues)

        For intCnt = 0 To strHiddenKeyValues.Length - 1

            If strKeyValues.Length <= intCnt Then Exit For

            strTmp1 = strKeyValues(intCnt)
            If strTmp1 <> String.Empty Then
                If dicItem1.ContainsKey(strTmp1) Then
                    If intCnt <> 0 Then '最初の項目に","は付けない
                        strRet &= ","
                    End If
                    '変更箇所の項目名称を取得
                    strRet &= dicItem1(strTmp1)
                    '背景色の変更
                    Me.getCtrlValuesStringAllKeyCtrlId(strTmp1)
                End If
            End If
        Next

        Return strRet
    End Function

    ''' <summary>
    ''' 変更のあったコントロール名称を文字列結合し、返却する
    ''' </summary>
    ''' <param name="strDbVal">DB値</param>
    ''' <param name="strChgVal">変更値</param>
    ''' <param name="strCtrlNm">コントロール名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkChgCtrlName(ByVal strDbVal As String, ByVal strChgVal As String, ByVal strCtrlNm As String) As String
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic
        Dim strDbValues() As String
        Dim strChgValues() As String
        Dim strRet As String = String.Empty
        Dim strKey As String = String.Empty

        Dim intCnt As Integer = 0
        Dim strTmp1 As String = String.Empty
        Dim strTmp2 As String = String.Empty

        'DB値あるいは変更値が未入力の場合
        If strDbVal = String.Empty OrElse strChgVal = String.Empty Then
            Return strRet
            Exit Function
        End If

        'DB値
        strDbValues = iLogic.getArrayFromDollarSep(strDbVal)
        '画面の値
        strChgValues = iLogic.getArrayFromDollarSep(strChgVal)

        '項目数が同じ場合
        If strDbValues.Length = strChgValues.Length Then
            For intCnt = 0 To strDbValues.Length - 1
                strTmp1 = strDbValues(intCnt)
                strTmp2 = strChgValues(intCnt)
                '変更箇所があればindexを退避
                If strTmp1 <> strTmp2 Then
                    strKey &= CStr(intCnt) & EarthConst.SEP_STRING
                End If
            Next
        End If

        'indexを元に、変更箇所の名称と背景色変更を行なう
        strRet = Me.getCtrlValuesStringAllKeyName(strKey, strCtrlNm)

        Return strRet
    End Function

#End Region

#End Region

#Region "発注書金額関連の処理"
    ''' <summary>
    ''' 発注金額変更時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub HattyuuAfterUpdate(ByVal sender As Object)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".HattyuuAfterUpdate", _
                                                    sender)

        Dim hattyuuKingaku As Integer = Integer.MinValue

        CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, hattyuuKingaku)

        TextHattyuusyoKakuninbi.Text = PfunZ010SetHatyuuYMD(hattyuuKingaku)

        '発注書確定チェック
        Select Case FunCheckHKakutei(2, sender)
            Case 1
                '
            Case 2
                SelectHattyuusyoKakutei.SelectedValue = "1"

                ' Old値に現在の値をセット
                HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue

                '発注書金額を編集不可に設定
                EnableTextBox(TextHattyuusyoKingaku, False)
        End Select

        '更新後発注書金額をoldに設定
        HiddenHattyuusyoKingakuOld.Value = TextHattyuusyoKingaku.Text
        TextHattyuusyoKingaku.Text = IIf(hattyuuKingaku = Integer.MinValue, "", hattyuuKingaku.ToString(EarthConst.FORMAT_KINGAKU_1))


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
           TextHattyuusyoKingaku.Text IsNot TextUriageZeinukiKingaku.Text Then

            Dim hattyuuKingaku As Integer = Integer.MinValue
            CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, hattyuuKingaku)
            ' 発注書確認日を設定
            TextHattyuusyoKakuninbi.Text = PfunZ010SetHatyuuYMD(hattyuuKingaku)

        End If

        ' 比較する金額を数値変換する
        Dim chkVal1 As Integer = 0
        Dim chkVal2 As Integer = 0
        CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, chkVal1)
        CommonLogic.Instance.SetDisplayString(TextUriageZeinukiKingaku.Text, chkVal2)

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
            EnableTextBox(TextHattyuusyoKingaku, False)
        Else
            If HiddenHattyuusyoKanriKengen.Value = "0" And _
               HiddenKeiriGyoumuKengen.Value = "0" Then
            Else
                ' 経理権限か、発注書管理権限有る場合、活性化
                EnableTextBox(TextHattyuusyoKingaku, True)
            End If
        End If

    End Function
#End Region
#End Region

#Region "パブリックメソッド"
    ''' <summary>
    ''' エラーチェック
    ''' </summary>
    ''' <param name="errMess"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <param name="typeWord"></param>
    ''' <param name="denpyouNgList"></param>
    ''' <param name="denpyouErrMess"></param>
    ''' <param name="seikyuuUmuErrMess"></param>
    ''' <param name="strChgPartMess"></param>
    ''' <remarks></remarks>
    Public Sub CheckInput(ByRef errMess As String, _
                          ByRef arrFocusTargetCtrl As List(Of Control), _
                          ByVal typeWord As String, _
                          ByVal denpyouNgList As String, _
                          ByRef denpyouErrMess As String, _
                          ByRef seikyuuUmuErrMess As String, _
                          ByRef strChgPartMess As String _
                          )

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckKinsoku", _
                                                    errMess, _
                                                    arrFocusTargetCtrl, _
                                                    typeWord, _
                                                    denpyouNgList, _
                                                    denpyouErrMess, _
                                                    seikyuuUmuErrMess, _
                                                    strChgPartMess _
                                                    )

        '地盤画面共通クラス
        Dim jBn As New Jiban
        '月次予約確定月
        Dim dtGetujiKakuteiLastSyoriDate As DateTime = DateTime.MinValue

        Dim setuzoku As String = typeWord
        If typeWord <> "" Then
            setuzoku = typeWord & "："
        End If

        'DB読み込み時点の値を、現在画面の値と比較(変更有無チェック)
        If HiddenKubun.Value <> String.Empty AndAlso (HiddenOpenValuesUriage.Value <> String.Empty Or HiddenOpenValuesSiire.Value <> String.Empty) Then
            'DB読み込み時点の値が空の場合は比較対象外
            '比較実施(売上)
            If HiddenOpenValuesUriage.Value <> getCtrlValuesStringUriage() Then
                '変更有りの場合
                If denpyouNgList.IndexOf(HiddenKubun.Value & EarthConst.SEP_STRING & _
                                         HiddenBangou.Value & EarthConst.SEP_STRING & _
                                         HiddenBunruiCd.Value & EarthConst.SEP_STRING & _
                                         "1") >= 0 Then
                    denpyouErrMess += typeWord & ","
                End If

                '月次確定予約済みの処理年月「以前」の日付で伝票売上年月日を設定するのはエラー
                If cLogic.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouUriageNengappi.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                    errMess += String.Format(Messages.MSG161E, dtGetujiKakuteiLastSyoriDate.Month, typeWord, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                    arrFocusTargetCtrl.Add(Me.TextDenpyouUriageNengappi)
                End If
            End If

            '比較実施(仕入)
            If HiddenOpenValuesSiire.Value <> getCtrlValuesStringSiire() Then
                '月次確定予約済みの処理年月「以前」の日付で伝票仕入年月日を設定するのはエラー
                If cLogic.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouSiireNengappi.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                    errMess += String.Format(Messages.MSG161E, dtGetujiKakuteiLastSyoriDate.Month, typeWord, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month).Replace("伝票売上", "伝票仕入")
                    arrFocusTargetCtrl.Add(Me.TextDenpyouSiireNengappi)
                End If
            End If

        End If

        '比較実施(変更チェック)
        Dim strChgVal As String = Me.getCtrlValuesStringAll()
        If Me.HiddenOpenValue.Value <> strChgVal Then
            Dim strCtrlNm As String = String.Empty
            strCtrlNm = Me.ChkChgCtrlName(Me.HiddenOpenValue.Value, strChgVal, Me.HiddenKeyValue.Value) '変更箇所名の取得
            strChgPartMess += "[" & typeWord & "]\r\n" & strCtrlNm & "\r\n"
        End If

        'コード入力値変更チェック
        If TextKoujigaisyaCd.Text <> HiddenKoujigaisyaCdOld.Value Then
            Dim koujigaisya As String = "工事会社コード"
            If HiddenBunruiCd.Value = EarthConst.SOUKO_CD_TUIKA_KOUJI Then
                koujigaisya = "追加工事会社コード"
            End If
            errMess += Messages.MSG030E.Replace("@PARAM1", koujigaisya)
            arrFocusTargetCtrl.Add(ButtonKoujigaisyaKensaku)
        End If

        '必須チェック
        '売上年月日と伝票売上年月日
        If Me.TextUriageNengappi.Text = String.Empty And Me.TextDenpyouUriageNengappi.Text <> String.Empty Then
            errMess += Messages.MSG153E.Replace("@PARAM1", setuzoku & "伝票売上年月日").Replace("@PARAM2", setuzoku & "売上年月日")
            arrFocusTargetCtrl.Add(TextUriageNengappi)
        End If
        '未売上の場合でかつ、売上年月日と伝票売上年月日、伝票仕入年月日が異なる場合
        If Me.SelectUriageSyori.SelectedValue = "0" Then
            '伝票売上年月日と比較
            '伝票仕入年月日と比較
            If Me.TextUriageNengappi.Text <> Me.TextDenpyouUriageNengappi.Text _
                Or Me.TextUriageNengappi.Text <> Me.TextDenpyouSiireNengappi.Text Then
                errMess += Messages.MSG144E.Replace("@PARAM1", setuzoku & "伝票売上年月日あるいは伝票仕入年月日").Replace("@PARAM2", setuzoku & "売上年月日").Replace("@PARAM3", "更新")
                arrFocusTargetCtrl.Add(Me.TextUriageNengappi)
            End If
        End If

        '入力値チェック
        If TextSeikyuusyoHakkoubi.Text <> "" Then
            If DateTime.Parse(TextSeikyuusyoHakkoubi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextSeikyuusyoHakkoubi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "請求書発行日")
                arrFocusTargetCtrl.Add(TextSeikyuusyoHakkoubi)
            End If
        End If

        If TextUriageNengappi.Text <> "" Then
            If DateTime.Parse(TextUriageNengappi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextUriageNengappi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "売上年月日")
                arrFocusTargetCtrl.Add(TextUriageNengappi)
            End If
        End If

        If TextDenpyouUriageNengappi.Text <> "" Then
            If DateTime.Parse(TextDenpyouUriageNengappi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextDenpyouUriageNengappi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "伝票売上年月日")
                arrFocusTargetCtrl.Add(TextDenpyouUriageNengappi)
            End If
        End If

        If TextDenpyouSiireNengappi.Text <> "" Then
            If DateTime.Parse(TextDenpyouSiireNengappi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextDenpyouSiireNengappi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "伝票仕入年月日")
                arrFocusTargetCtrl.Add(TextDenpyouSiireNengappi)
            End If
        End If

        If TextHattyuusyoKakuninbi.Text <> "" Then
            If DateTime.Parse(TextHattyuusyoKakuninbi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextHattyuusyoKakuninbi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "発注書確認日")
                arrFocusTargetCtrl.Add(TextHattyuusyoKakuninbi)
            End If
        End If

        Dim dtLogic As New DataLogic
        Dim intJituGaku As Integer = dtLogic.str2Int(TextUriageZeinukiKingaku.Text.Replace(",", ""))

        '商品の請求有無と売上金額との関連チェック(請求無し・0円以外：NG / 請求あり・0円: 警告)
        If HiddenOpenValuesUriage.Value <> String.Empty Then
            If SelectSyouhinCd.SelectedValue <> String.Empty Then
                If SelectSeikyuuUmu.SelectedValue = "0" And intJituGaku <> 0 Then
                    '請求無し・0円以外：NG
                    errMess += String.Format(Messages.MSG157E, typeWord)
                    arrFocusTargetCtrl.Add(SelectSeikyuuUmu)
                ElseIf SelectSeikyuuUmu.SelectedValue = "1" And intJituGaku = 0 Then
                    '請求あり・0円: 警告
                    seikyuuUmuErrMess += typeWord & "、"
                    arrFocusTargetCtrl.Add(TextUriageZeinukiKingaku)
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' 基本請求先・仕入先情報の設定
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strTysKaisyaCd">調査会社コード</param>
    ''' <remarks></remarks>
    Public Sub SetDefaultSeikyuuSiireSakiInfo(ByVal strKameitenCd As String, ByVal strTysKaisyaCd As String)
        Dim strUriageZumi As String = String.Empty  '売上処理済み判断フラグ用

        '売上処理済チェック
        If Me.SelectUriageSyori.SelectedValue = "1" Then
            strUriageZumi = Me.SelectUriageSyori.SelectedValue
        End If

        Me.SeikyuuSiireLinkCtrl.SetVariableValueCtrlFromParent(strKameitenCd _
                                                                , Me.SelectSyouhinCd.SelectedValue _
                                                                , Me.TextKoujigaisyaCd.Text _
                                                                , strUriageZumi _
                                                                , Me.CheckKoujigaisyaSeikyuu.Checked _
                                                                , Me.TextKoujigaisyaCd.Text _
                                                                , Me.TextDenpyouUriageNengappi.Text)
    End Sub

#End Region

#Region "金額設定"
    ''' <summary>
    ''' 工事価格マスタのKEY情報をレコードクラスより設定
    ''' </summary>
    ''' <remarks>
    ''' [設定対象]
    ''' 依頼内容CTRL.加盟店コード
    ''' 依頼内容CTRL.系列コード
    ''' 依頼内容CTRL.営業所コード
    ''' [未設定対象] ※画面上に項目として存在するため
    ''' 工事商品CTRL.商品コード
    ''' 工事商品CTRL.工事会社コード
    ''' </remarks>
    Public Sub SetKojKkkMstInfo(ByVal keyRec As KoujiKakakuKeyRecord)
        With keyRec
            '加盟店コード
            Me.HiddenKameitenCd.Value = .KameitenCd
            '系列コード
            Me.HiddenKeiretuCd.Value = .KeiretuCd
            '営業所コード
            Me.HiddenEigyousyoCd.Value = .EigyousyoCd
        End With
    End Sub

    ''' <summary>
    ''' 商品情報(工事価格マスタから金額・請求有無等設定)
    ''' </summary>
    '''<param name="emActionType">実行元アクションタイプ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSyouhinInfoFromKojM(ByVal emActionType As EarthEnum.emKojKkkActionType) As Boolean

        Dim keyRec As New KoujiKakakuKeyRecord          '検索キー用レコード
        Dim resultRec As New KoujiKakakuRecord          '結果取得用レコード
        Dim lgcKouji As New KairyouKoujiLogic           '改良工事ロジック
        Dim intResult As Integer = Integer.MinValue

        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        '取得に必要な画面項目のセット
        keyRec = cbLogic.GetKojKkkMstKeyRec(Me.HiddenKameitenCd.Value _
                                            , Me.HiddenEigyousyoCd.Value _
                                            , Me.HiddenKeiretuCd.Value _
                                            , Me.SelectSyouhinCd.SelectedValue _
                                            , Me.TextKoujigaisyaCd.Text)

        '工事会社価格ロジックより取得
        intResult = lgcKouji.GetKoujiKakakuRecord(keyRec, resultRec)

        '条件一致の取得は画面にセット
        If intResult < EarthEnum.emKoujiKakaku.Syouhin Then
            '工事会社請求有無
            If resultRec.KojGaisyaSeikyuuUmu = 1 Then
                Me.CheckKoujigaisyaSeikyuu.Checked = True
            Else
                Me.CheckKoujigaisyaSeikyuu.Checked = False
            End If
            Me.UpdatePanelKoujigaisyaSeikyuu.Update()

            '請求有無
            If emActionType <> EarthEnum.emKojKkkActionType.SeikyuuUmu Then '請求有無変更時は設定しない
                If resultRec.SeikyuuUmu = 0 OrElse resultRec.SeikyuuUmu = 1 Then
                    Me.SelectSeikyuuUmu.SelectedValue = resultRec.SeikyuuUmu
                End If
            End If

            '売上年月日で判断して、正しい税率を取得する
            strSyouhinCd = Me.SelectSyouhinCd.SelectedValue
            If cbLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                '取得した税区分・税率をセット
                resultRec.Zeiritu = strZeiritu
                resultRec.ZeiKbn = strZeiKbn
            End If

            '税率
            Me.HiddenZeiritu.Value = IIf(resultRec.Zeiritu = Decimal.MinValue, 0, cLogic.GetDispStr(resultRec.Zeiritu))
            '税区分
            Me.HiddenZeiKbn.Value = IIf(resultRec.ZeiKbn = String.Empty, "", resultRec.ZeiKbn)

            '請求有無=無しの場合
            If Me.SelectSeikyuuUmu.SelectedValue = "0" Then
                '実請求金額
                Me.TextUriageZeinukiKingaku.Text = "0"
            Else
                '実請求金額
                Me.TextUriageZeinukiKingaku.Text = IIf(resultRec.UriGaku = Integer.MinValue, 0, Format(resultRec.UriGaku, EarthConst.FORMAT_KINGAKU_1))
            End If

            '金額設定
            SetKingaku()

            Me.UpdatePanelKoujiSyouhin.Update()
        Else
            Return False
            Exit Function
        End If

        Return True
    End Function

    ''' <summary>
    ''' 金額設定(売上金額)
    ''' </summary>
    ''' <param name="blnZeigaku"></param>
    ''' <remarks></remarks>
    Private Sub SetKingaku(Optional ByVal blnZeigaku As Boolean = False)
        ' 税抜価格（実請求金額）
        Dim zeinuki_ctrl As TextBox
        ' 消費税率
        Dim zeiritu_ctrl As HiddenField
        ' 消費税額
        Dim zeigaku_ctrl As TextBox
        ' 税込金額
        Dim zeikomi_gaku_ctrl As TextBox

        zeinuki_ctrl = TextUriageZeinukiKingaku
        zeiritu_ctrl = HiddenZeiritu
        zeigaku_ctrl = TextUriageSyouhizeiGaku
        zeikomi_gaku_ctrl = TextUriageZeikomiKingaku

        Dim zeinuki As Integer = 0
        Dim zeiritu As Decimal = 0
        Dim zeigaku As Integer = 0
        Dim zeikomi_gaku As Integer = 0

        If zeinuki_ctrl.Text = "" Then '未入力
            zeinuki_ctrl.Text = ""
            zeigaku_ctrl.Text = ""
            zeikomi_gaku_ctrl.Text = ""

        Else '入力あり
            cLogic.SetDisplayString(CInt(zeinuki_ctrl.Text), zeinuki)
            cLogic.SetDisplayString(zeiritu_ctrl.Value, zeiritu)

            zeinuki = IIf(zeinuki = Integer.MinValue, 0, zeinuki)
            zeiritu = IIf(zeiritu = Decimal.MinValue, 0, zeiritu)

            If blnZeigaku Then '消費税額の値で計算
                cLogic.SetDisplayString(CInt(zeigaku_ctrl.Text), zeigaku)
            Else
                zeigaku = Fix(zeinuki * zeiritu)
            End If
            zeikomi_gaku = zeinuki + zeigaku

            zeigaku_ctrl.Text = Format(zeigaku, EarthConst.FORMAT_KINGAKU_1) '消費税
            zeikomi_gaku_ctrl.Text = Format(zeikomi_gaku, EarthConst.FORMAT_KINGAKU_1) '税込金額

        End If
    End Sub


    ''' <summary>
    ''' 仕入消費税額設定(承諾書金額)
    ''' </summary>
    ''' <param name="blnZeigaku"></param>
    ''' <remarks></remarks>
    Private Sub SetSiireZeigaku(Optional ByVal blnZeigaku As Boolean = False)
        ' 税抜価格（承諾書金額）
        Dim zeinuki_ctrl As TextBox
        ' 消費税率
        Dim zeiritu_ctrl As HiddenField
        ' 消費税額
        Dim zeigaku_ctrl As TextBox
        ' 税込金額
        Dim zeikomi_gaku_ctrl As TextBox

        zeinuki_ctrl = TextSiireZeinukiKingaku ' 承諾書金額
        zeiritu_ctrl = HiddenZeiritu
        zeigaku_ctrl = TextSiireSyouhizeiGaku
        zeikomi_gaku_ctrl = TextSiireZeikomiKingaku

        Dim zeinuki As Integer = 0
        Dim zeiritu As Decimal = 0
        Dim zeigaku As Integer = 0
        Dim zeikomi_gaku As Integer = 0

        If zeinuki_ctrl.Text = "" Then '未入力
            zeinuki_ctrl.Text = ""
            zeigaku_ctrl.Text = ""
            zeikomi_gaku_ctrl.Text = ""

        Else '入力あり
            cLogic.SetDisplayString(CInt(zeinuki_ctrl.Text), zeinuki)
            cLogic.SetDisplayString(zeiritu_ctrl.Value, zeiritu)

            zeinuki = IIf(zeinuki = Integer.MinValue, 0, zeinuki)
            zeiritu = IIf(zeiritu = Decimal.MinValue, 0, zeiritu)

            If blnZeigaku Then '消費税額の値で計算
                cLogic.SetDisplayString(CInt(zeigaku_ctrl.Text), zeigaku)
            Else
                zeigaku = Fix(zeinuki * zeiritu)
            End If
            zeikomi_gaku = zeinuki + zeigaku

            zeigaku_ctrl.Text = Format(zeigaku, EarthConst.FORMAT_KINGAKU_1) '消費税
            zeikomi_gaku_ctrl.Text = Format(zeikomi_gaku, EarthConst.FORMAT_KINGAKU_1) '税込金額

        End If
    End Sub
#End Region

End Class