Partial Public Class FcMousikomiSyuusei
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

    'CommonBiz共通ロジック
    Private cbLogic As New CommonBizLogic
    '共通ロジック
    Private cl As New CommonLogic
    'メッセージクラス
    Private MLogic As New MessageLogic
    'ロジッククラス
    Private MSLogic As New FcMousikomiSearchLogic
    'FC申込レコードクラス
    Private dtRec As New FcMousikomiRecord
    '地盤ロジック
    Dim JLogic As New JibanLogic

#Region "画面固有コントロール値"
    Private Const MI_JUTYUU As String = "未"
    Private Const HORYUU_JUTYUU As String = "保留"
    Private Const ZUMI_JUTYUU As String = "済"
    Private Const ARI_TIKASYAKOKEIKAKU As String = "有"
    Private Const NASI_TIKASYAKOKEIKAKU As String = "無"
#End Region

#Region "プロパティ"

#Region "パラメータ/FC申込修正画面"
    ''' <summary>
    ''' 申込NO
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _MousikomiNo As String = String.Empty
    ''' <summary>
    ''' 申込NO
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrMousikomiNo() As String
        Get
            Return _MousikomiNo
        End Get
        Set(ByVal value As String)
            _MousikomiNo = value
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

        'スクリプトマネージャーを取得（ScriptManager用）
        masterAjaxSM = Me.AjaxScriptManager

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合
            cl.CloseWindow(Me)
            Me.BtnStyle(False)
            Exit Sub
        End If

        If IsPostBack = False Then
            Try
                '●パラメータのチェック
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
                ' Key情報を保持
                pStrMousikomiNo = arrSearchTerm(0)

                ' パラメータ不足時は閉じる
                If pStrMousikomiNo Is Nothing OrElse pStrMousikomiNo = String.Empty Then
                    cl.CloseWindow(Me)
                    Me.BtnStyle(False)
                    Exit Sub
                End If
            Catch ex As Exception
                cl.CloseWindow(Me)
                Me.BtnStyle(False)
                Exit Sub
            End Try

            '●権限のチェック
            '新規入力権限
            If userinfo.SinkiNyuuryokuKengen = 0 Then
                cl.CloseWindow(Me)
                Me.BtnStyle(False)
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            ' コンボ設定ヘルパークラスを生成
            Dim helper As New DropDownHelper

            '建物概要：構造種別コンボにデータをバインドする
            helper.SetDropDownList(SelectTGKouzouSyubetu, DropDownHelper.DropDownType.Kouzou, True, False)
            '建物概要：新築建替コンボにデータをバインドする
            helper.SetDropDownList(SelectTGSintikuTatekae, DropDownHelper.DropDownType.ShintikuTatekae, True, False)
            '建物概要：階層(地上)コンボにデータをバインドする
            helper.SetDropDownList(SelectTGKaisouTijyou, DropDownHelper.DropDownType.Kaisou, True, False)
            '建物概要：建物用途コンボにデータをバインドする
            helper.SetDropDownList(SelectTGTatemonoYouto, DropDownHelper.DropDownType.TatemonoYouto, True, False)
            '地業および予定基礎状況：予定基礎コンボにデータをバインドする
            helper.SetDropDownList(SelectTYYoteiKiso, DropDownHelper.DropDownType.YoteiKiso, True, False)

            '****************************************************************************
            ' FC申込修正データ取得
            '****************************************************************************
            SetCtrlFromDataRec(sender, e)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            'ボタン押下イベントの設定()
            setBtnEvent()

            '未受注・保留のデータの場合
            If Me.SpanJutyuuJyky.InnerHtml = MI_JUTYUU Or _
               Me.SpanJutyuuJyky.InnerHtml = HORYUU_JUTYUU Then
                '重複チェック
                CheckTyoufuku(Nothing)
            End If
            'フォーカス設定
            ButtonClose.Focus()
        End If

        '****************************************************************************
        ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
        '****************************************************************************
        setDispAction()

        '****************************************************************************
        ' ステータスによって、表示状態の切り替えを行う
        '****************************************************************************
        SetEnableControl(sender, e)

        '画面の設定
        SetDispControl()

    End Sub

    ''' <summary>
    ''' ステータスによって、表示状態の切り替えを行う
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SetEnableControl(ByVal sender As Object, ByVal e As System.EventArgs)

        '地盤画面共通クラス
        Dim jBn As New Jiban
        '有効化、無効化の対象外にするコントロール郡
        Dim noTarget As New Hashtable

        'ボタン(画面上部)
        noTarget.Add(Me.ButtonClose.ID, True)                          '閉じる
        noTarget.Add(Me.ButtonSyuusei.ID, True)                        '修正
        noTarget.Add(Me.ButtonHoryuu.ID, True)                         '保留
        noTarget.Add(Me.ButtonSinkiJutyuu.ID, True)                    '新規受注
        noTarget.Add(Me.ButtonFcMousikomi.ID, True)                    'FC申込

        '要注意情報
        noTarget.Add(Me.TextAreaYouTyuuiJouhou.ID, True)
        noTarget.Add(Me.TextMousikomiNo.ID, True)                      '申込NO
        noTarget.Add(Me.SpanJutyuuJyky.ID, True)                       '受注状況
        noTarget.Add(Me.TextKbn.ID, True)                              '区分
        noTarget.Add(Me.TextHosyousyoNo.ID, True)                      '番号
        noTarget.Add(Me.TextKameitenCd.ID, True)                       '加盟店
        noTarget.Add(Me.TextTorikesiRiyuu.ID, True)                    '加盟店取消理由
        noTarget.Add(Me.TextBoxTysHouhou.ID, True)                     '調査方法
        noTarget.Add(Me.TextSyouhin1Cd.ID, True)                       '商品コード1
        noTarget.Add(Me.TextIraiDate.ID, True)                         '依頼日
        noTarget.Add(Me.TextKeiyu.ID, True)                            '経由
        noTarget.Add(Me.TextBukkenNayoseCd.ID, True)                   '物件名寄コード
        '担当調査会社
        noTarget.Add(Me.TextTantouTysKaisya.ID, True)
        '申込調査会社
        noTarget.Add(Me.TextMousikomiTysKaisya.ID, True)
        '調査会社担当者
        noTarget.Add(Me.TextTysKaisyaCdTantousya.ID, True)
        '調査連絡先担当者
        noTarget.Add(Me.TextTantousya.ID, True)
        '調査立会者(立会者(その他補足))
        noTarget.Add(Me.TextAreaTTSonotaHosoku.ID, True)

        'ボタン(画面下部)
        noTarget.Add(Me.ButtonSyuusei2.ID, True)                       '修正

        'ダミー項目
        noTarget.Add(Me.RadioTantuosyaTelDummy.ID, True)               '担当者連絡先TELダブルクリック用ダミー

        '受注済時
        If Me.SpanJutyuuJyky.InnerHtml = ZUMI_JUTYUU Then
            '全てのコントロールを無効化
            jBn.ChangeDesabledAll(Me, True, noTarget)
        End If

    End Sub

    ''' <summary>
    ''' 画面モード別の画面設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispControl()

        If Me.SpanJutyuuJyky.InnerHtml = MI_JUTYUU Then
            'ボタンの設定
            BtnStyle(True, True, True)

        ElseIf Me.SpanJutyuuJyky.InnerHtml = ZUMI_JUTYUU Then
            'ボタンの設定
            BtnStyle(True, False, False)

        ElseIf Me.SpanJutyuuJyky.InnerHtml = HORYUU_JUTYUU Then
            'ボタンの設定
            BtnStyle(True, True, False)
        End If
    End Sub

    ''' <summary>
    ''' 抽出レコードクラスから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object, ByVal e As System.EventArgs)

        If pStrMousikomiNo Is String.Empty Then
            pStrMousikomiNo = Me.HiddenMousikomiNo.Value
        End If

        ' コンボ設定ヘルパークラスを生成
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        dtRec = MSLogic.GetMousikomiDataRec(sender, pStrMousikomiNo)

        '要注意情報
        Me.TextAreaYouTyuuiJouhou.Value = cl.GetDisplayString(dtRec.YouTyuuiJouhou)
        '申込NO
        Me.TextMousikomiNo.Text = pStrMousikomiNo
        '受注状況
        If dtRec.Status = EarthConst.MOUSIKOMI_STATUS_MI_JUTYUU Then
            Me.SpanJutyuuJyky.InnerHtml = MI_JUTYUU
        ElseIf dtRec.Status = EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU Then
            Me.SpanJutyuuJyky.InnerHtml = ZUMI_JUTYUU
        ElseIf dtRec.Status = EarthConst.MOUSIKOMI_STATUS_HORYUU_JUTYUU Then
            Me.SpanJutyuuJyky.InnerHtml = HORYUU_JUTYUU
        End If
        '区分
        '●区分コンボにデータをバインドする(ダミーコンボにセット)
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Kubun, True, True)
        objDrpTmp.SelectedValue = dtRec.Kbn
        Me.TextKbn.Text = objDrpTmp.SelectedItem.Text
        '番号
        Me.TextHosyousyoNo.Text = cl.GetDisplayString(dtRec.HosyousyoNo)
        '担当調査会社
        Me.TextTantouTysKaisya.Text = cl.GetDisplayString(dtRec.TantouTysKaisyaCd & dtRec.TantouTysKaisyaJigyousyoCd + ":" + dtRec.TantouTysKaisyaMei)
        '申込調査会社
        Me.TextMousikomiTysKaisya.Text = cl.GetDisplayString(dtRec.MousikomiTysKaisyaCd & dtRec.MousikomiTysKaisyaJigyousyoCd + ":" + dtRec.MousikomiTysKaisyaMei)
        '調査会社担当者
        Me.TextTysKaisyaCdTantousya.Text = cl.GetDisplayString(dtRec.TysKaisyaTantousya)
        '加盟店コード
        Me.TextKameitenCd.Text = cl.GetDisplayString(dtRec.KameitenCd + ":" + dtRec.KameitenMei)
        '加盟店取消理由表示・色変処理
        Dim objChgColor As Object() = New Object() {Me.TextKameitenCd, Me.TextTorikesiRiyuu}
        cl.GetKameitenTorikesiRiyuu(cl.GetDisplayString(dtRec.Kbn), cl.GetDisplayString(dtRec.KameitenCd), Me.TextTorikesiRiyuu, True, False, objChgColor)
        '調査方法NO
        Me.TextBoxTysHouhou.Text = cl.GetDisplayString(dtRec.TysHouhou & ":" & dtRec.TysHouhouMei)
        '商品コード
        Me.TextSyouhin1Cd.Text = cl.GetDisplayString(dtRec.SyouhinCd & ":" & dtRec.SyouhinNm)
        '依頼日
        Me.TextIraiDate.Text = cbLogic.GetDispStrDateTime(dtRec.IraiDate, "")
        '加盟店様物件管理番号(契約NO)
        Me.TextKameiBukkenKanriNo.Text = cl.GetDisplayString(dtRec.KeiyakuNo)
        '経由
        '●経由コンボにデータをバインドする(ダミーコンボにセット)
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Keiyu, False, True)
        objDrpTmp.SelectedValue = dtRec.Keiyu
        Me.TextKeiyu.Text = objDrpTmp.SelectedItem.Text
        '調査連絡先_宛先
        Me.TextTysRenrakusakiAtesakiMei.Text = cl.GetDisplayString(dtRec.TysRenrakusakiAtesakiMei)
        '調査連絡先担当者名
        Me.TextTantousya.Text = cl.GetDisplayString(dtRec.TysRenrakusakiTantouMei)
        '物件名寄コード
        Me.TextBukkenNayoseCd.Text = cl.GetDisplayString(dtRec.BukkenNayoseCd)
        '担当者連絡先TEL
        Me.RadioTantuosyaTel0.Checked = (dtRec.TantousyaRenrakusakiTel = CStr(0))
        Me.RadioTantuosyaTel1.Checked = (dtRec.TantousyaRenrakusakiTel = CStr(1))
        Me.TextTantousyaTel.Text = IIf(Me.RadioTantuosyaTel1.Checked, cl.GetDisplayString(dtRec.TysRenrakusakiTel), "")
        '調査連絡先FAX
        Me.TextTysRenrakusakiFax.Text = cl.GetDisplayString(dtRec.TysRenrakusakiFax)
        '調査連絡先MAIL
        Me.TextTysRenrakusakiMail.Text = cl.GetDisplayString(dtRec.TysRenrakusakiMail)
        '物件名称：施主名
        Me.TextBMBukkenMeisyou.Text = cl.GetDisplayString(dtRec.SesyuMei)
        '施主名有無
        Me.RadioSesyumei1.Checked = (dtRec.SesyuMeiUmu = 1)
        Me.RadioSesyumei0.Checked = (dtRec.SesyuMeiUmu = 0)
        '調査場所_市区町村：物件住所1
        Me.TextTBBukkenJyuusyo1.Text = cl.GetDisplayString(dtRec.BukkenJyuusyo1)
        '調査場所_番地１：物件住所2
        Me.TextTBBukkenJyuusyo2.Text = cl.GetDisplayString(dtRec.BukkenJyuusyo2)
        '調査場所_番地２：物件住所3
        Me.TextTBBukkenJyuusyo3.Text = cl.GetDisplayString(dtRec.BukkenJyuusyo3)
        '同時依頼棟数
        Me.TextDIDoujiIraiTousuu.Text = cl.GetDisplayString(cbLogic.SetAutoDoujiIraiTousuu(dtRec.DoujiIraiTousuu), "")
        '調査希望日
        Me.TextTKTyousaKibouDate.Text = cl.GetDisplayString(dtRec.TysKibouDate)
        '調査希望(区分)
        Me.TextTKTyousaKibouKbn.Text = cl.GetDisplayString(dtRec.TysKibouKbn)
        '調査開始希望時間
        Me.TextTKTyousaKaisiKibouJikan.Text = cl.GetDisplayString(dtRec.TysKaisiKibouJikan)
        '調査立会者
        Me.RadioTTAri.Checked = (dtRec.TatiaiUmu = 1)
        'チェックボックスをセット
        cl.SetTatiaiCd(dtRec.TtsyCd, Me.CheckTTSesyuSama, Me.CheckTTTantousya, Me.CheckTTSonota)
        Me.TextAreaTTSonotaHosoku.Value = cl.GetDisplayString(dtRec.TtsySonotaHosoku)
        Me.RadioTTNasi.Checked = (dtRec.TatiaiUmu = 0)
        '基礎竣工予定日FROM
        Me.TextKYKsTyakkouYoteiDateFrom.Text = cl.GetDisplayString(dtRec.KsTyakkouYoteiFromDate)
        '基礎竣工予定日TO
        Me.TextKYKsTyakkouYoteiDateTo.Text = cl.GetDisplayString(dtRec.KsTyakkouYoteiToDate)
        '建物概要_構造種別
        Me.TextTGKouzouSyubetuCd.Text = cl.GetDisplayString(dtRec.Kouzou)
        If cl.ChkDropDownList(Me.SelectTGKouzouSyubetu, Me.TextTGKouzouSyubetuCd.Text) Then
            Me.SelectTGKouzouSyubetu.SelectedValue = Me.TextTGKouzouSyubetuCd.Text
        End If
        Me.TextTGKouzouSyubetuSonota.Text = IIf(Me.SelectTGKouzouSyubetu.SelectedValue = "9", cl.GetDisplayString(dtRec.KouzouMemo), "")
        '建物概要_新築立替
        Me.TextTGSintikuTatekaeCd.Text = cl.GetDisplayString(dtRec.SintikuTatekae)
        If cl.ChkDropDownList(Me.SelectTGSintikuTatekae, Me.TextTGSintikuTatekaeCd.Text) Then
            Me.SelectTGSintikuTatekae.SelectedValue = Me.TextTGSintikuTatekaeCd.Text
        End If
        '建物概要_階層(地上)
        Me.TextTGKaisouTijyou.Text = cl.GetDisplayString(dtRec.KaisouTijyou)
        If cl.ChkDropDownList(Me.SelectTGKaisouTijyou, Me.TextTGKaisouTijyou.Text) Then
            Me.SelectTGKaisouTijyou.SelectedValue = Me.TextTGKaisouTijyou.Text
        End If
        '建物概要_建物用途
        Me.TextTGTatemonoYouto.Text = cl.GetDisplayString(dtRec.TmytNo)
        If cl.ChkDropDownList(Me.SelectTGTatemonoYouto, Me.TextTGTatemonoYouto.Text) Then
            Me.SelectTGTatemonoYouto.SelectedValue = Me.TextTGTatemonoYouto.Text
        End If
        '建物概要_設計許容支持力
        Me.TextTGSekkeiKyoyouSijiryoku.Text = cl.GetDisplayString(dtRec.SekkeiKyoyouSijiryoku)
        '地業および予定基礎状況_根切り深さ
        Me.TextTYNegiriHukasa.Text = cl.GetDisplayString(dtRec.NegiriHukasa)
        '地業および予定基礎状況_予定盛土厚さ
        Me.TextTYYoteiMoritutiAtusa.Text = cl.GetDisplayString(dtRec.YoteiMoritutiAtusa)
        '地業および予定基礎状況_基礎
        Me.TextTYYoteiKiso.Text = cl.GetDisplayString(dtRec.YoteiKs)
        If cl.ChkDropDownList(Me.SelectTYYoteiKiso, Me.TextTYYoteiKiso.Text) Then
            Me.SelectTYYoteiKiso.SelectedValue = Me.TextTYYoteiKiso.Text
        End If
        Me.TextTYYoteiKisoMemo.Text = IIf(Me.SelectTYYoteiKiso.SelectedValue = "9", cl.GetDisplayString(dtRec.YoteiKsMemo), "")
        '備考
        Me.TextAreaBKBikou.Value = cl.GetDisplayString(dtRec.Bikou)

        '************
        '* Hidden項目
        '************
        '申込NO
        Me.HiddenMousikomiNo.Value = pStrMousikomiNo
        '更新日時
        Me.HiddenUpdDatetime.Value = IIf(dtRec.UpdDatetime = Date.MinValue, "", Format(dtRec.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
        '同時依頼棟数 新規受注用Hidden
        Me.HiddenDoujiIraiTousuu.Value = Me.TextDIDoujiIraiTousuu.Text
        '同時依頼棟数2以上チェック用：Check03
        Me.HiddenTBChk03.Value = String.Empty
        '区分
        Me.HiddenKbn.Value = dtRec.Kbn

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()
        Dim jBn As New Jiban '地盤画面クラス

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 重複チェック関連(※表示設定)
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        If Me.SpanJutyuuJyky.InnerHtml = ZUMI_JUTYUU Then
            Me.ButtonTyoufukuCheck.Style("display") = "none"
            '物件ダイレクト
            LinkBukkenDirect.HRef = UrlConst.POPUP_GAMEN_KIDOU & "?kbn=" & Me.HiddenKbn.Value & "&no=" & Me.TextHosyousyoNo.Text
        Else
            '物件ダイレクト
            LinkBukkenDirect.HRef = String.Empty
        End If

        Me.TextBMBukkenMeisyou.Attributes("onchange") = "ChgTyoufukuBukken(this);"
        Me.TextTBBukkenJyuusyo1.Attributes("onchange") = "ChgTyoufukuBukken(this);"
        Me.TextTBBukkenJyuusyo2.Attributes("onchange") = "ChgTyoufukuBukken(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' プルダウン/コード入力連携設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '構造種別
        jBn.SetPullCdScriptSrc(TextTGKouzouSyubetuCd, SelectTGKouzouSyubetu)
        '新築･建替
        jBn.SetPullCdScriptSrc(TextTGSintikuTatekaeCd, SelectTGSintikuTatekae)
        '階層(地上)
        jBn.SetPullCdScriptSrc(TextTGKaisouTijyou, SelectTGKaisouTijyou)
        '建物用途
        jBn.SetPullCdScriptSrc(TextTGTatemonoYouto, SelectTGTatemonoYouto)
        '予定基礎
        jBn.SetPullCdScriptSrc(TextTYYoteiKiso, SelectTYYoteiKiso)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 「～その他」や「立会有り」の場合のみ表示する項目
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '構造、予定基礎に関しては、「9：その他」の場合のみ「～その他」の入力項目を使用可能にするスクリプトを埋め込む
        '担当者連絡先TEL
        RadioTantuosyaTel1.Attributes("onclick") = "checkSonota(this.checked==true,'" & TextTantousyaTel.ClientID & "');"
        '調査立会者
        CheckTTSonota.Attributes("onclick") = "checkSonota(this.checked==true,'" & TextAreaTTSonotaHosoku.ClientID & "');"
        '構造
        SelectTGKouzouSyubetu.Attributes("onchange") += "checkSonota(this.value==9,'" & TextTGKouzouSyubetuSonota.ClientID & "');"
        '予定基礎(その他補足)
        SelectTYYoteiKiso.Attributes("onchange") += "checkSonota(this.value==9,'" & TextTYYoteiKisoMemo.ClientID & "');"

        '指定値選択時の動き(画面表示時用)
        checkSonota()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript関連セット
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim checkDate As String = "checkDate(this);"
        Dim checkNumber As String = "checkNumber(this);"
        Dim onFocusScript As String = "removeFig(this);"
        Dim onBlurNumScript As String = "checkNumberAddFig(this);"
        Dim onBlurFewNumScript As String = "checkFewNumberAddFig(this);"

        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '*****************************
        '* 日付系
        '*****************************
        '調査希望日FROM
        TextTKTyousaKibouDate.Attributes("onblur") = checkDate
        TextTKTyousaKibouDate.Attributes("onkeydown") = disabledOnkeydown
        '基礎着工予定日FROM
        TextKYKsTyakkouYoteiDateFrom.Attributes("onblur") = checkDate
        TextKYKsTyakkouYoteiDateFrom.Attributes("onkeydown") = disabledOnkeydown
        '基礎着工予定日TO
        TextKYKsTyakkouYoteiDateTo.Attributes("onblur") = checkDate
        TextKYKsTyakkouYoteiDateTo.Attributes("onkeydown") = disabledOnkeydown

        '*****************************
        '* 数値系
        '*****************************
        '同時依頼棟数
        Me.TextDIDoujiIraiTousuu.Attributes("onblur") = checkNumber
        Me.TextDIDoujiIraiTousuu.Attributes("onkeydown") = disabledOnkeydown
        '設計許容支持力
        Me.TextTGSekkeiKyoyouSijiryoku.Attributes("onfocus") = onFocusScript
        Me.TextTGSekkeiKyoyouSijiryoku.Attributes("onblur") = onBlurFewNumScript
        '根切り深さ
        Me.TextTYNegiriHukasa.Attributes("onfocus") = onFocusScript
        Me.TextTYNegiriHukasa.Attributes("onblur") = onBlurFewNumScript
        '予定盛土厚さ
        Me.TextTYYoteiMoritutiAtusa.Attributes("onfocus") = onFocusScript
        Me.TextTYYoteiMoritutiAtusa.Attributes("onblur") = onBlurFewNumScript

        '*****************************
        '* ラジオボタン
        '*****************************
        '担当者連絡先の表示切替
        checkTantousyaRenrakusaki()
        '調査立会者の表示切替
        checkTysTatiaisya()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)

    End Sub

    ''' <summary>
    ''' 各種ボタンの設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setBtnEvent()

        'イベントハンドラ登録
        Dim tmpScriptOverLay As String = "setWindowOverlay(this,null,1);"

        Dim tmpSyuusei As String = "if(confirm('" & Messages.MSG017C & "')){" & tmpScriptOverLay & "}else{return false;}"
        Dim tmpScriptSyuusei As String = "if(CheckJikkou()){" & tmpSyuusei & "}else{return false;}"
        Dim tmpSinkiJyutyuu As String = "if(CheckTouroku()){" & tmpSyuusei & "}else{return false;}"
        Dim tmpHoryuu As String = tmpSyuusei

        '登録許可MSG確認後、OKの場合DB更新処理を行なう
        Me.ButtonSyuusei.Attributes("onclick") = tmpScriptSyuusei       '修正ボタン(画面上部)
        Me.ButtonSyuusei2.Attributes("onclick") = tmpScriptSyuusei      '修正ボタン(画面下部)
        Me.ButtonSinkiJutyuu.Attributes("onclick") = tmpSinkiJyutyuu    '新規受注ボタン(画面上部)
        Me.ButtonSinkiJutyuu2.Attributes("onclick") = tmpSinkiJyutyuu   '新規受注ボタン(画面下部)
        Me.ButtonHoryuu.Attributes("onclick") = tmpHoryuu               '保留ボタン(画面上部)

        'FC申込
        cl.getFcMousikomiPath(Me.HiddenKbn.Value, Me.TextHosyousyoNo.Text, Me.ButtonFcMousikomi)

    End Sub

    ''' <summary>
    ''' ボタン表示処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BtnStyle(ByVal flgDispBase As Boolean, _
                            Optional ByVal flgSinkiJutyuuKahi As Boolean = True, _
                            Optional ByVal flgHoryuuKahi As Boolean = True)

        If flgDispBase Then
            ''修正ボタンを表示
            'Me.ButtonSyuusei.Style("display") = "inline"
            'Me.ButtonSyuusei2.Style("display") = "inline"

            '未受注の場合
            If flgSinkiJutyuuKahi Then
                '新規受注ボタンを表示
                Me.ButtonSinkiJutyuu.Style("display") = "inline"
                Me.ButtonSinkiJutyuu2.Style("display") = "inline"
            Else
                '新規受注ボタンを非表示
                Me.ButtonSinkiJutyuu.Style("display") = "none"
                Me.ButtonSinkiJutyuu2.Style("display") = "none"
            End If

            '保留の場合
            If flgHoryuuKahi Then
                '保留ボタンを表示
                Me.ButtonHoryuu.Style("display") = "inline"
            Else
                '保留ボタンを非表示
                Me.ButtonHoryuu.Style("display") = "none"
            End If
        Else
            '非表示
            Me.ButtonSyuusei.Style("display") = "none"
            Me.ButtonSinkiJutyuu.Style("display") = "none"
            Me.ButtonSyuusei2.Style("display") = "none"
            Me.ButtonSinkiJutyuu2.Style("display") = "none"
            Me.ButtonHoryuu.Style("display") = "none"
        End If
    End Sub

    ''' <summary>
    ''' 担当者連絡先によって、表示を切り替える
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkTantousyaRenrakusaki()
        '担当者連絡先に対する調査連絡先TELテキストボックスの表示切替
        Dim TantousyaRenrakusakiScript As String = "ChgDispTantousyaRenrakusaki();"

        Me.RadioTantuosyaTel0.Attributes("onclick") = TantousyaRenrakusakiScript
        Me.RadioTantuosyaTel1.Attributes("onclick") = TantousyaRenrakusakiScript

        '担当者連絡先のラジオボタンをダブルクリックすると、ダミーのラジオボタンを選択する＝＞チェック解除用
        Dim tmpScript As String = "objEBI('" & Me.RadioTantuosyaTelDummy.ClientID & "').click();"
        Me.RadioTantuosyaTel0.Attributes("ondblclick") = tmpScript & TantousyaRenrakusakiScript
        Me.RadioTantuosyaTel1.Attributes("ondblclick") = tmpScript & TantousyaRenrakusakiScript

        If Me.RadioTantuosyaTel1.Checked = Boolean.TrueString Then '有
            '表示
            Me.TextTantousyaTel.Style("visiblity") = "visible"
        Else '非表示
            Me.TextTantousyaTel.Style("visiblity") = "hidden"
        End If
    End Sub

    ''' <summary>
    ''' 調査立会者の有無によって、表示を切り替える
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkTysTatiaisya()
        '調査立会者に対するチェックボックスの表示切替
        Dim TatiaisyaScript As String = "ChgDispTatiaisya();"

        Me.RadioTTAri.Attributes("onclick") = TatiaisyaScript
        Me.RadioTTNasi.Attributes("onclick") = TatiaisyaScript

        '調査立会者のラジオボタンをダブルクリックすると、ダミーのラジオボタンを選択する＝＞チェック解除用
        Dim tmpScript As String = "objEBI('" & Me.RadioTTTysDummy.ClientID & "').click();"
        Me.RadioTTAri.Attributes("ondblclick") = tmpScript & TatiaisyaScript
        Me.RadioTTNasi.Attributes("ondblclick") = tmpScript & TatiaisyaScript

        '活性化制御
        If Me.RadioTTAri.Checked Then '有
            '活性化
            Me.CheckTTSesyuSama.Disabled = False
            Me.CheckTTTantousya.Disabled = False
            Me.CheckTTSonota.Disabled = False
        Else '非活性化
            Me.CheckTTSesyuSama.Disabled = True
            Me.CheckTTTantousya.Disabled = True
            Me.CheckTTSonota.Disabled = True
        End If

        '表示･非表示
        If Me.CheckTTSonota.Checked = Boolean.TrueString Then '有
            '表示
            Me.TextAreaTTSonotaHosoku.Style("visiblity") = "visible"
        Else '非表示
            Me.TextAreaTTSonotaHosoku.Style("visiblity") = "hidden"
        End If
    End Sub

    ''' <summary>
    ''' 選択値が「その他」の場合、表示を切り替える
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkSonota()
        '指定値選択時の動き(画面表示時用)
        '担当者連絡先TEL
        Dim tmpAL1 As New ArrayList
        tmpAL1.Add(TextTantousyaTel)
        cl.CheckVisible(Boolean.TrueString, RadioTantuosyaTel1, tmpAL1)
        '調査立会者
        Dim tmpAL2 As New ArrayList
        tmpAL2.Add(TextAreaTTSonotaHosoku)
        cl.CheckVisible(Boolean.TrueString, CheckTTSonota, tmpAL2)
        '構造
        Dim tmpAL3 As New ArrayList
        tmpAL3.Add(TextTGKouzouSyubetuSonota)
        cl.CheckVisible("9", SelectTGKouzouSyubetu, tmpAL3)
        '予定基礎(布基礎)
        Dim tmpAL6 As New ArrayList
        cl.CheckVisible("1", SelectTYYoteiKiso, tmpAL6)
        '予定基礎(その他補足)
        Dim tmpAL7 As New ArrayList
        tmpAL7.Add(TextTYYoteiKisoMemo)
        cl.CheckVisible("9", SelectTYYoteiKiso, tmpAL7)

    End Sub

#End Region

#Region "重複チェック処理"
    ''' <summary>
    ''' 重複チェック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub CheckTyoufuku(ByVal sender As System.Object)
        Dim bolResult1 As Boolean = True
        Dim bolResult2 As Boolean = True

        ' 施主名が空白以外の場合、施主名の重複チェックを実施
        If Me.TextBMBukkenMeisyou.Text.Trim <> "" Then
            If JLogic.ChkTyouhuku(Me.TextKbn.Text.Chars(0), _
                                 String.Empty, _
                                 Me.TextBMBukkenMeisyou.Text.Trim()) = True Then
                bolResult1 = False
            End If
        End If

        ' 物件住所が空白以外の場合、物件住所の重複チェックを実施
        If Me.TextTBBukkenJyuusyo1.Text.Trim() <> "" Or Me.TextTBBukkenJyuusyo2.Text.Trim() <> "" Then
            If JLogic.ChkTyouhuku(Me.TextKbn.Text.Chars(0), _
                                 String.Empty, _
                                 Me.TextTBBukkenJyuusyo1.Text.Trim(), _
                                 Me.TextTBBukkenJyuusyo2.Text.Trim()) = True Then
                bolResult2 = False
            End If
        End If

        If sender IsNot Nothing Then
            'チェック処理のトリガーによって、確認フラグをセット
            If Me.HiddenTyoufukuKakuninTargetId.Value = Me.TextBMBukkenMeisyou.ClientID Then
                '施主名変更時に実行された場合
                Me.HiddenTyoufukuKakuninFlg1.Value = bolResult1.ToString
            ElseIf (Me.HiddenTyoufukuKakuninTargetId.Value = Me.TextTBBukkenJyuusyo1.ClientID Or _
                    Me.HiddenTyoufukuKakuninTargetId.Value = Me.TextTBBukkenJyuusyo2.ClientID) Then
                '住所変更時に実行された場合
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
        If jBn.KinsiStrCheck(Me.TextBMBukkenMeisyou.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件名称")
            arrFocusTargetCtrl.Add(Me.TextBMBukkenMeisyou)
        End If
        '物件住所1
        If jBn.KinsiStrCheck(Me.TextTBBukkenJyuusyo1.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "市区町村")
            arrFocusTargetCtrl.Add(Me.TextTBBukkenJyuusyo1)
        End If
        '物件住所2
        If jBn.KinsiStrCheck(Me.TextTBBukkenJyuusyo2.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "番地１")
            arrFocusTargetCtrl.Add(Me.TextTBBukkenJyuusyo2)
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

#End Region

#End Region

#Region "DB更新処理系"

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <remarks>
    ''' 必須チェック<br/>
    ''' 禁則文字チェック(文字列入力フィールドが対象)<br/>
    ''' バイト数チェック(文字列入力フィールドが対象)<br/>
    ''' 桁数チェック<br/>
    ''' 日付チェック<br/>
    ''' その他チェック<br/>
    ''' </remarks>
    Public Function checkInput(ByVal prmActBtn As HtmlInputButton) As Boolean
        '地盤画面共通クラス
        Dim jBn As New Jiban

        'エラーメッセージ初期化
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList

        '各ボタン別チェック
        Select Case prmActBtn.ID
            Case Me.ButtonSinkiJutyuu.ID, Me.ButtonSinkiJutyuu2.ID
                '●必須チェック
                '*************************
                '* 受注状況別チェック処理
                '*************************
                If Me.SpanJutyuuJyky.InnerHtml = ZUMI_JUTYUU Then
                    errMess += Messages.MSG185E.Replace("@PARAM1", "受注").Replace("@PARAM2", "新規受注")
                    arrFocusTargetCtrl.Add(Me.ButtonSinkiJutyuu)
                End If

            Case Me.ButtonSyuusei.ID

            Case Me.ButtonHoryuu.ID
                '保留ボタン機能は、ステータス変更のみ
                Return True

            Case Else
                errMess += "既定のボタン以外が押されました。\r\n"
        End Select

        If errMess = String.Empty Then

            '●必須チェック
            '*************************
            '* 受注状況別チェック処理
            '*************************
            If Me.SpanJutyuuJyky.InnerHtml <> ZUMI_JUTYUU Then '受注済の場合はチェックを行なわない
                '物件名称：施主名
                If Me.TextBMBukkenMeisyou.Text = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "物件名称")
                    arrFocusTargetCtrl.Add(Me.TextBMBukkenMeisyou)
                End If
                '施主名有無
                If Me.RadioSesyumei0.Checked = False AndAlso Me.RadioSesyumei1.Checked = False Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "施主名有無")
                    arrFocusTargetCtrl.Add(Me.RadioSesyumei1)
                End If
                '同時依頼棟数
                If Me.TextDIDoujiIraiTousuu.Text = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "同時依頼棟数")
                    arrFocusTargetCtrl.Add(Me.TextDIDoujiIraiTousuu)
                End If
                '市区町村：物件住所1
                If Me.TextTBBukkenJyuusyo1.Text = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "市区町村")
                    arrFocusTargetCtrl.Add(Me.TextTBBukkenJyuusyo1)
                End If
                '調査希望日FROM
                If Me.TextTKTyousaKibouDate.Text = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "調査希望日FROM")
                    arrFocusTargetCtrl.Add(Me.TextTKTyousaKibouDate)
                End If
            End If

            '●禁則文字チェック(文字列入力フィールドが対象)
            '要注意情報
            If jBn.KinsiStrCheck(Me.TextAreaYouTyuuiJouhou.Value) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "要注意情報")
                arrFocusTargetCtrl.Add(Me.TextAreaYouTyuuiJouhou)
            End If
            '改行変換
            If Me.TextAreaYouTyuuiJouhou.Value <> "" Then
                Me.TextAreaYouTyuuiJouhou.Value = Me.TextAreaYouTyuuiJouhou.Value.Replace(vbCrLf, " ")
            End If
            '調査会社担当者
            If jBn.KinsiStrCheck(Me.TextTysKaisyaCdTantousya.Text) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "調査会社担当者")
                arrFocusTargetCtrl.Add(Me.TextTysKaisyaCdTantousya)
            End If
            '調査立会者(その他補足)
            If jBn.KinsiStrCheck(Me.TextAreaTTSonotaHosoku.Value) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "調査立会者(その他補足)")
                arrFocusTargetCtrl.Add(Me.TextAreaTTSonotaHosoku)
            End If
            '改行変換
            If Me.TextAreaTTSonotaHosoku.Value <> "" Then
                Me.TextAreaTTSonotaHosoku.Value = Me.TextAreaTTSonotaHosoku.Value.Replace(vbCrLf, " ")
            End If
            '*************************
            '* 受注状況別チェック処理
            '*************************
            If Me.SpanJutyuuJyky.InnerHtml <> ZUMI_JUTYUU Then '受注済の場合はチェックを行なわない
                '調査連絡先担当者名
                If jBn.KinsiStrCheck(Me.TextTantousya.Text) = False Then
                    errMess += Messages.MSG015E.Replace("@PARAM1", "調査連絡先担当者名")
                    arrFocusTargetCtrl.Add(Me.TextTantousya)
                End If
                '調査連絡先宛先
                If jBn.KinsiStrCheck(Me.TextTysRenrakusakiAtesakiMei.Text) = False Then
                    errMess += Messages.MSG015E.Replace("@PARAM1", "調査連絡先宛先")
                    arrFocusTargetCtrl.Add(Me.TextTysRenrakusakiAtesakiMei)
                End If
                '加盟店様物件管理番号
                If jBn.KinsiStrCheck(Me.TextKameiBukkenKanriNo.Text) = False Then
                    errMess += Messages.MSG015E.Replace("@PARAM1", "加盟店様物件管理番号")
                    arrFocusTargetCtrl.Add(Me.TextKameiBukkenKanriNo)
                End If
                '調査連絡先TEL
                If jBn.KinsiStrCheck(Me.TextTantousyaTel.Text) = False Then
                    errMess += Messages.MSG015E.Replace("@PARAM1", "調査連絡先TEL")
                    arrFocusTargetCtrl.Add(Me.TextTantousyaTel)
                End If
                '調査連絡先FAX
                If jBn.KinsiStrCheck(Me.TextTysRenrakusakiFax.Text) = False Then
                    errMess += Messages.MSG015E.Replace("@PARAM1", "調査連絡先FAX")
                    arrFocusTargetCtrl.Add(Me.TextTysRenrakusakiFax)
                End If
                '調査連絡先MAIL
                If jBn.KinsiStrCheck(Me.TextTysRenrakusakiMail.Text) = False Then
                    errMess += Messages.MSG015E.Replace("@PARAM1", "調査連絡先MAIL")
                    arrFocusTargetCtrl.Add(Me.TextTysRenrakusakiMail)
                End If
                '物件名称：施主名
                If jBn.KinsiStrCheck(Me.TextBMBukkenMeisyou.Text) = False Then
                    errMess += Messages.MSG015E.Replace("@PARAM1", "物件名称")
                    arrFocusTargetCtrl.Add(Me.TextBMBukkenMeisyou)
                End If
                '市区町村：物件住所1
                If jBn.KinsiStrCheck(Me.TextTBBukkenJyuusyo1.Text) = False Then
                    errMess += Messages.MSG015E.Replace("@PARAM1", "市区町村")
                    arrFocusTargetCtrl.Add(Me.TextTBBukkenJyuusyo1)
                End If
                '番地１：物件住所2
                If jBn.KinsiStrCheck(Me.TextTBBukkenJyuusyo2.Text) = False Then
                    errMess += Messages.MSG015E.Replace("@PARAM1", "番地１")
                    arrFocusTargetCtrl.Add(Me.TextTBBukkenJyuusyo2)
                End If
                '番地２：物件住所3
                If jBn.KinsiStrCheck(Me.TextTBBukkenJyuusyo3.Text) = False Then
                    errMess += Messages.MSG015E.Replace("@PARAM1", "番地２")
                    arrFocusTargetCtrl.Add(Me.TextTBBukkenJyuusyo3)
                End If
                '調査希望(区分)
                If jBn.KinsiStrCheck(Me.TextTKTyousaKibouKbn.Text) = False Then
                    errMess += Messages.MSG015E.Replace("@PARAM1", "調査希望(区分)")
                    arrFocusTargetCtrl.Add(Me.TextTKTyousaKibouKbn)
                End If
                '調査開始希望時間
                If jBn.KinsiStrCheck(Me.TextTKTyousaKaisiKibouJikan.Text) = False Then
                    errMess += Messages.MSG015E.Replace("@PARAM1", "調査開始希望時間")
                    arrFocusTargetCtrl.Add(Me.TextTKTyousaKaisiKibouJikan)
                End If
                '構造種別(その他補足)
                If jBn.KinsiStrCheck(Me.TextTGKouzouSyubetuSonota.Text) = False Then
                    errMess += Messages.MSG015E.Replace("@PARAM1", "構造種別(その他補足)")
                    arrFocusTargetCtrl.Add(Me.TextTGKouzouSyubetuSonota)
                End If
                '予定基礎(その他補足)
                If jBn.KinsiStrCheck(Me.TextTYYoteiKisoMemo.Text) = False Then
                    errMess += Messages.MSG015E.Replace("@PARAM1", "予定基礎(その他補足)")
                    arrFocusTargetCtrl.Add(Me.TextTYYoteiKisoMemo)
                End If
                '備考
                If jBn.KinsiStrCheck(Me.TextAreaBKBikou.Value) = False Then
                    errMess += Messages.MSG015E.Replace("@PARAM1", "備考")
                    arrFocusTargetCtrl.Add(Me.TextAreaBKBikou)
                End If
                '改行変換
                If Me.TextAreaBKBikou.Value <> "" Then
                    Me.TextAreaBKBikou.Value = Me.TextAreaBKBikou.Value.Replace(vbCrLf, " ")
                End If
            End If

            '●バイト数チェック(文字列入力フィールドが対象)
            '要注意情報
            If jBn.ByteCheckSJIS(Me.TextAreaYouTyuuiJouhou.Value, 256) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "要注意情報")
                arrFocusTargetCtrl.Add(Me.TextAreaYouTyuuiJouhou)
            End If
            '調査会社担当者
            If jBn.ByteCheckSJIS(Me.TextTysKaisyaCdTantousya.Text, Me.TextTysKaisyaCdTantousya.MaxLength) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "調査会社担当者")
                arrFocusTargetCtrl.Add(Me.TextTysKaisyaCdTantousya)
            End If
            '*************************
            '* 受注状況別チェック処理
            '*************************
            If Me.SpanJutyuuJyky.InnerHtml <> ZUMI_JUTYUU Then '受注済の場合はチェックを行なわない
                '調査連絡先担当者
                If jBn.ByteCheckSJIS(Me.TextTantousya.Text, Me.TextTantousya.MaxLength) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "調査連絡先担当者")
                    arrFocusTargetCtrl.Add(Me.TextTantousya)
                End If
                '調査立会者(その他補足)
                If jBn.ByteCheckSJIS(Me.TextAreaTTSonotaHosoku.Value, 256) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "調査立会者(その他補足)")
                    arrFocusTargetCtrl.Add(Me.TextAreaTTSonotaHosoku)
                End If
                '加盟店様物件管理番号
                If jBn.ByteCheckSJIS(Me.TextKameiBukkenKanriNo.Text, Me.TextKameiBukkenKanriNo.MaxLength) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "加盟店様物件管理番号")
                    arrFocusTargetCtrl.Add(Me.TextKameiBukkenKanriNo)
                End If
                '調査連絡先宛先
                If jBn.ByteCheckSJIS(Me.TextTysRenrakusakiAtesakiMei.Text, Me.TextTysRenrakusakiAtesakiMei.MaxLength) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "調査連絡先宛先")
                    arrFocusTargetCtrl.Add(Me.TextTysRenrakusakiAtesakiMei)
                End If
                '調査連絡先TEL
                If jBn.ByteCheckSJIS(Me.TextTantousyaTel.Text, Me.TextTantousyaTel.MaxLength) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "調査連絡先TEL")
                    arrFocusTargetCtrl.Add(Me.TextTantousyaTel)
                End If
                '調査連絡先FAX
                If jBn.ByteCheckSJIS(Me.TextTysRenrakusakiFax.Text, Me.TextTysRenrakusakiFax.MaxLength) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "調査連絡先FAX")
                    arrFocusTargetCtrl.Add(Me.TextTysRenrakusakiFax)
                End If
                '調査連絡先MAIL
                If jBn.ByteCheckSJIS(Me.TextTysRenrakusakiMail.Text, Me.TextTysRenrakusakiMail.MaxLength) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "調査連絡先MAIL")
                    arrFocusTargetCtrl.Add(Me.TextTysRenrakusakiMail)
                End If
                '物件名称：施主名
                If jBn.ByteCheckSJIS(Me.TextBMBukkenMeisyou.Text, Me.TextBMBukkenMeisyou.MaxLength) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "物件名称")
                    arrFocusTargetCtrl.Add(Me.TextBMBukkenMeisyou)
                End If
                '市区町村：物件住所1
                If jBn.ByteCheckSJIS(Me.TextTBBukkenJyuusyo1.Text, Me.TextTBBukkenJyuusyo1.MaxLength) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "市区町村")
                    arrFocusTargetCtrl.Add(Me.TextTBBukkenJyuusyo1)
                End If
                '番地１：物件住所2
                If jBn.ByteCheckSJIS(Me.TextTBBukkenJyuusyo2.Text, Me.TextTBBukkenJyuusyo2.MaxLength) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "番地１")
                    arrFocusTargetCtrl.Add(Me.TextTBBukkenJyuusyo2)
                End If
                '番地２：物件住所3
                If jBn.ByteCheckSJIS(Me.TextTBBukkenJyuusyo3.Text, Me.TextTBBukkenJyuusyo3.MaxLength) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "番地２")
                    arrFocusTargetCtrl.Add(Me.TextTBBukkenJyuusyo3)
                End If
                '調査希望(区分)
                If jBn.ByteCheckSJIS(Me.TextTKTyousaKibouKbn.Text, Me.TextTKTyousaKibouKbn.MaxLength) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "調査希望(区分)")
                    arrFocusTargetCtrl.Add(Me.TextTKTyousaKibouKbn)
                End If
                '調査開始希望時間
                If jBn.ByteCheckSJIS(Me.TextTKTyousaKaisiKibouJikan.Text, Me.TextTKTyousaKaisiKibouJikan.MaxLength) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "調査開始希望時間")
                    arrFocusTargetCtrl.Add(Me.TextTKTyousaKaisiKibouJikan)
                End If
                '構造種別(その他補足)
                If jBn.ByteCheckSJIS(Me.TextTGKouzouSyubetuSonota.Text, Me.TextTGKouzouSyubetuSonota.MaxLength) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "構造種別(その他補足)")
                    arrFocusTargetCtrl.Add(Me.TextTGKouzouSyubetuSonota)
                End If
                '予定基礎(その他補足)
                If jBn.ByteCheckSJIS(Me.TextTYYoteiKisoMemo.Text, Me.TextTYYoteiKisoMemo.MaxLength) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "予定基礎(その他補足)")
                    arrFocusTargetCtrl.Add(Me.TextTYYoteiKisoMemo)
                End If
                '備考
                If jBn.ByteCheckSJIS(Me.TextAreaBKBikou.Value, 256) = False Then
                    errMess += Messages.MSG016E.Replace("@PARAM1", "備考")
                    arrFocusTargetCtrl.Add(Me.TextAreaBKBikou)
                End If
            End If

            '●桁数チェック
            '*************************
            '* 受注状況別チェック処理
            '*************************
            If Me.SpanJutyuuJyky.InnerHtml <> ZUMI_JUTYUU Then '受注済の場合はチェックを行なわない
                '設計許容支持力
                If jBn.SuutiStrCheck(Me.TextTGSekkeiKyoyouSijiryoku.Text, 4, 1) = False Then
                    errMess += Messages.MSG027E.Replace("@PARAM1", "設計許容支持力").Replace("@PARAM2", "4").Replace("@PARAM3", "1")
                    arrFocusTargetCtrl.Add(Me.TextTGSekkeiKyoyouSijiryoku)
                End If
                '根切り深さ
                If jBn.SuutiStrCheck(Me.TextTYNegiriHukasa.Text, 8, 4) = False Then
                    errMess += Messages.MSG027E.Replace("@PARAM1", "根切り深さ").Replace("@PARAM2", "8").Replace("@PARAM3", "4")
                    arrFocusTargetCtrl.Add(Me.TextTYNegiriHukasa)
                End If
                '予定盛土厚さ
                If jBn.SuutiStrCheck(Me.TextTYYoteiMoritutiAtusa.Text, 8, 4) = False Then
                    errMess += Messages.MSG027E.Replace("@PARAM1", "予定盛土厚さ").Replace("@PARAM2", "8").Replace("@PARAM3", "4")
                    arrFocusTargetCtrl.Add(Me.TextTYYoteiMoritutiAtusa)
                End If
            End If

            '●日付チェック
            '*************************
            '* 受注状況別チェック処理
            '*************************
            If Me.SpanJutyuuJyky.InnerHtml <> ZUMI_JUTYUU Then '受注済の場合はチェックを行なわない
                '調査希望日FROM
                If Me.TextTKTyousaKibouDate.Text <> "" Then
                    If cl.checkDateHanni(Me.TextTKTyousaKibouDate.Text) = False Then
                        errMess += Messages.MSG014E.Replace("@PARAM1", "調査希望日FROM")
                        arrFocusTargetCtrl.Add(Me.TextTKTyousaKibouDate)
                    End If
                End If
                '基礎着工予定日FROM
                If Me.TextKYKsTyakkouYoteiDateFrom.Text <> "" Then
                    If cl.checkDateHanni(Me.TextKYKsTyakkouYoteiDateFrom.Text) = False Then
                        errMess += Messages.MSG014E.Replace("@PARAM1", "基礎着工予定日FROM")
                        arrFocusTargetCtrl.Add(Me.TextKYKsTyakkouYoteiDateFrom)
                    End If
                End If
                '基礎着工予定日TO
                If Me.TextKYKsTyakkouYoteiDateTo.Text <> "" Then
                    If cl.checkDateHanni(Me.TextKYKsTyakkouYoteiDateTo.Text) = False Then
                        errMess += Messages.MSG014E.Replace("@PARAM1", "基礎着工予定日TO")
                        arrFocusTargetCtrl.Add(Me.TextKYKsTyakkouYoteiDateTo)
                    End If
                End If
            End If

            '●その他チェック
            '*************************
            '* 受注状況別チェック処理
            '*************************
            '(日付項目.TOの値が、日付項目.FROMの値より以前の場合、エラーとする。)
            If Me.SpanJutyuuJyky.InnerHtml <> ZUMI_JUTYUU Then '受注済の場合はチェックを行なわない
                '基礎着工予定日FROM、TOチェック
                If Me.TextKYKsTyakkouYoteiDateFrom.Text <> String.Empty And Me.TextKYKsTyakkouYoteiDateTo.Text <> String.Empty Then
                    If Me.TextKYKsTyakkouYoteiDateFrom.Text > Me.TextKYKsTyakkouYoteiDateTo.Text Then
                        errMess += Messages.MSG022E.Replace("@PARAM1", "基礎着工予定日")
                        arrFocusTargetCtrl.Add(Me.TextKYKsTyakkouYoteiDateFrom)
                    End If
                End If

                '(特定項目選択時の必須チェック)
                '担当者連絡先TEL(その他)→調査連絡先TEL
                If Me.RadioTantuosyaTel1.Checked And Me.TextTantousyaTel.Text = String.Empty Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "担当者連絡先TEL(その他)を選択時、調査連絡先TEL")
                    arrFocusTargetCtrl.Add(Me.TextTantousyaTel)
                End If
                '調査立会者(その他)→立会者(その他補足)
                If Me.CheckTTSonota.Checked And Me.TextAreaTTSonotaHosoku.Value = String.Empty Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "調査立会者(その他)を選択時、立会者(その他補足)")
                    arrFocusTargetCtrl.Add(Me.TextAreaTTSonotaHosoku)
                End If
                '構造(その他)→構造(その他補足)
                If Me.SelectTGKouzouSyubetu.SelectedValue = "9" And Me.TextTGKouzouSyubetuSonota.Text = String.Empty Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "構造(その他)を選択時、構造(その他補足)")
                    arrFocusTargetCtrl.Add(Me.TextTGKouzouSyubetuSonota)
                End If
                '予定基礎(その他)→予定基礎(その他補足)
                If Me.SelectTYYoteiKiso.SelectedValue = "9" And Me.TextTYYoteiKisoMemo.Text = String.Empty Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "予定基礎(その他)を選択時、予定基礎(その他補足)")
                    arrFocusTargetCtrl.Add(Me.TextTYYoteiKisoMemo)
                End If

                '重複物件チェック
                '重複物件あり時、重複物件ポップアップにて確認していない場合
                If Me.HiddenTyoufukuKakuninFlg1.Value <> Boolean.TrueString Or _
                   Me.HiddenTyoufukuKakuninFlg2.Value <> Boolean.TrueString Then
                    errMess += Messages.MSG017E
                    arrFocusTargetCtrl.Add(Me.ButtonTyoufukuCheck)
                End If

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
    Protected Function SaveData(ByVal emType As EarthEnum.emMousikomiUpdType) As Boolean

        Dim MLogic As New FcMousikomiSearchLogic
        Dim listRec As New List(Of FcMousikomiRecord)

        '画面からレコードクラスにセット
        listRec = Me.GetCtrlToDataList(emType)

        Select Case emType
            '保留ボタン
            Case EarthEnum.emMousikomiUpdType.Horyuu
                '保留のみ更新処理
                If MLogic.saveData(Me, listRec) = False Then
                    Return False
                End If

            Case EarthEnum.emMousikomiUpdType.SinkiJutyuu '新規受注ボタン
                '新規受注処理
                If MLogic.saveDataJutyuu(Me, listRec, EarthEnum.emMousikomiSinkiJutyuuType.MousikomiSyuusei) = False Then
                    Return False
                End If

            Case EarthEnum.emMousikomiUpdType.Misettei, EarthEnum.emMousikomiUpdType.Syuusei
                '申込修正処理
                If MLogic.saveData(Me, listRec) = False Then
                    Return False
                End If

        End Select

        Return True
    End Function

    ''' <summary>
    ''' 画面の各情報をレコードクラスに取得し、DB更新用レコードクラスを返却する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCtrlToDataList(Optional ByVal emType As EarthEnum.emMousikomiUpdType = EarthEnum.emMousikomiUpdType.Misettei) As List(Of FcMousikomiRecord)
        Dim listRec As New List(Of FcMousikomiRecord)

        With dtRec
            '***************************************
            ' データ
            '***************************************
            ' 要注意情報
            cl.SetDisplayString(Me.TextAreaYouTyuuiJouhou.Value, .YouTyuuiJouhou)
            ' 調査会社担当者
            cl.SetDisplayString(Me.TextTysKaisyaCdTantousya.Text, .TysKaisyaTantousya)
            ' 申込NO
            cl.SetDisplayString(Me.TextMousikomiNo.Text, .MousikomiNo)
            ' 受注状況
            If Me.SpanJutyuuJyky.InnerHtml = MI_JUTYUU Then '未受注の場合
                If emType = EarthEnum.emMousikomiUpdType.Horyuu Then
                    dtRec.Status = EarthConst.MOUSIKOMI_STATUS_HORYUU_JUTYUU 'ステータス=保留
                Else
                    dtRec.Status = EarthConst.MOUSIKOMI_STATUS_MI_JUTYUU 'ステータス=修正
                End If
            ElseIf Me.SpanJutyuuJyky.InnerHtml = ZUMI_JUTYUU Then '受注済の場合
                dtRec.Status = EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU 'ステータス=受注済
            ElseIf Me.SpanJutyuuJyky.InnerHtml = HORYUU_JUTYUU Then '保留の場合
                If emType = EarthEnum.emMousikomiUpdType.Horyuu Then '保留ボタン押下時
                    dtRec.Status = EarthConst.MOUSIKOMI_STATUS_HORYUU_JUTYUU 'ステータス=保留
                Else
                    dtRec.Status = EarthConst.MOUSIKOMI_STATUS_MI_JUTYUU 'ステータス=未受注
                End If
            End If
            ' 加盟店様物件管理番号(契約NO)
            cl.SetDisplayString(Me.TextKameiBukkenKanriNo.Text, .KeiyakuNo)
            ' 調査連絡先宛先
            cl.SetDisplayString(Me.TextTysRenrakusakiAtesakiMei.Text, .TysRenrakusakiAtesakiMei)
            ' 調査連絡先担当者
            cl.SetDisplayString(Me.TextTantousya.Text, .TysRenrakusakiTantouMei)
            ' 担当者連絡先TEL
            If Me.RadioTantuosyaTel0.Checked Then
                cl.SetDisplayString("0", .TantousyaRenrakusakiTel)
            ElseIf Me.RadioTantuosyaTel1.Checked Then
                cl.SetDisplayString("1", .TantousyaRenrakusakiTel)
            End If
            ' 調査連絡先TEL
            cl.SetDisplayString(Me.TextTantousyaTel.Text, .TysRenrakusakiTel)
            ' 調査連絡先FAX
            cl.SetDisplayString(Me.TextTysRenrakusakiFax.Text, .TysRenrakusakiFax)
            ' 調査連絡先MAIL
            cl.SetDisplayString(Me.TextTysRenrakusakiMail.Text, .TysRenrakusakiMail)
            ' 物件名称：施主名
            cl.SetDisplayString(Me.TextBMBukkenMeisyou.Text, .SesyuMei)
            '施主名有無
            If Me.RadioSesyumei0.Checked Then '無
                cl.SetDisplayString(0, .SesyuMeiUmu)
            ElseIf Me.RadioSesyumei1.Checked Then '有
                cl.SetDisplayString(1, .SesyuMeiUmu)
            Else
                cl.SetDisplayString(Integer.MinValue, .SesyuMeiUmu)
            End If
            ' 市区町村：物件住所1
            cl.SetDisplayString(Me.TextTBBukkenJyuusyo1.Text, .BukkenJyuusyo1)
            ' 番地１：物件住所2
            cl.SetDisplayString(Me.TextTBBukkenJyuusyo2.Text, .BukkenJyuusyo2)
            ' 番地２：物件住所3
            cl.SetDisplayString(Me.TextTBBukkenJyuusyo3.Text, .BukkenJyuusyo3)
            ' 同時依頼棟数
            cl.SetDisplayString(Me.TextDIDoujiIraiTousuu.Text, .DoujiIraiTousuu)
            ' 調査希望日
            cl.SetDisplayString(Me.TextTKTyousaKibouDate.Text, .TysKibouDate)
            ' 調査希望(区分)
            cl.SetDisplayString(Me.TextTKTyousaKibouKbn.Text, .TysKibouKbn)
            ' 調査開始希望時間
            cl.SetDisplayString(Me.TextTKTyousaKaisiKibouJikan.Text, .TysKaisiKibouJikan)
            ' 立会有無
            If Me.RadioTTAri.Checked Then
                cl.SetDisplayString("1", .TatiaiUmu)
            ElseIf Me.RadioTTNasi.Checked Then
                cl.SetDisplayString("0", .TatiaiUmu)
            End If
            ' 立会者コード
            .TtsyCd = cl.GetTatiaiCd(Me.CheckTTSesyuSama, Me.CheckTTTantousya, Me.CheckTTSonota)
            ' 立会者(その他補足)
            cl.SetDisplayString(Me.TextAreaTTSonotaHosoku.Value, .TtsySonotaHosoku)
            ' 基礎着工予定日FROM
            cl.SetDisplayString(Me.TextKYKsTyakkouYoteiDateFrom.Text, .KsTyakkouYoteiFromDate)
            ' 基礎着工予定日TO
            cl.SetDisplayString(Me.TextKYKsTyakkouYoteiDateTo.Text, .KsTyakkouYoteiToDate)
            ' 構造
            cl.SetDisplayString(Me.SelectTGKouzouSyubetu.SelectedValue, .Kouzou)
            ' 構造MEMO
            cl.SetDisplayString(Me.TextTGKouzouSyubetuSonota.Text, .KouzouMemo)
            ' 新築建替
            cl.SetDisplayString(Me.SelectTGSintikuTatekae.SelectedValue, .SintikuTatekae)
            ' 階層(地上)
            cl.SetDisplayString(Me.SelectTGKaisouTijyou.SelectedValue, .KaisouTijyou)
            ' 建物用途NO
            cl.SetDisplayString(Me.SelectTGTatemonoYouto.SelectedValue, .TmytNo)
            ' 設計許容支持力
            cl.SetDisplayString(Me.TextTGSekkeiKyoyouSijiryoku.Text, .SekkeiKyoyouSijiryoku)
            ' 根切り深さ
            cl.SetDisplayString(Me.TextTYNegiriHukasa.Text, .NegiriHukasa)
            ' 予定盛土厚さ
            cl.SetDisplayString(Me.TextTYYoteiMoritutiAtusa.Text, .YoteiMoritutiAtusa)
            ' 予定基礎
            cl.SetDisplayString(Me.SelectTYYoteiKiso.SelectedValue, .YoteiKs)
            ' 予定基礎MEMO
            cl.SetDisplayString(Me.TextTYYoteiKisoMemo.Text, .YoteiKsMemo)
            ' 備考
            cl.SetDisplayString(Me.TextAreaBKBikou.Value, .Bikou)
            ' 更新者
            .Kousinsya = cbLogic.GetKousinsya(userinfo.LoginUserId, DateTime.Now)
            ' 更新者ユーザーID
            .UpdLoginUserId = userinfo.LoginUserId
            ' 更新日時 読み込み時のタイムスタンプ
            If Me.HiddenUpdDatetime.Value = "" Then
                .UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
            Else
                .UpdDatetime = DateTime.ParseExact(Me.HiddenUpdDatetime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
            End If

        End With

        listRec.Add(dtRec)

        Return listRec
    End Function

#End Region

#Region "ボタンイベント"

    ''' <summary>
    ''' 修正ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSyuusei_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSyuusei.ServerClick, ButtonSyuusei2.ServerClick
        ' 修正ボタン押下時処理実行
        ButtonSyuuseiCheck()

    End Sub

    ''' <summary>
    ''' 保留ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHoryuu_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHoryuu.ServerClick
        ' 保留ボタン押下時処理実行
        ButtonHoryuuCheck()

    End Sub

    ''' <summary>
    ''' 新規受注ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSinkiJutyuu_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSinkiJutyuu.ServerClick, ButtonSinkiJutyuu2.ServerClick

        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.ButtonSinkiJutyuu

        '新規受注処理は、まず修正ボタン押下時処理を通す
        If ButtonSyuuseiCheck() = False Then Exit Sub

        Me.SetCtrlFromDataRec(sender, e)

        ' 入力チェック
        If Me.checkInput(objActBtn) = False Then Exit Sub

        ' 画面の内容をDBに反映する
        If Me.SaveData(EarthEnum.emMousikomiUpdType.SinkiJutyuu) Then '登録成功
            '画面を閉じる
            cl.CloseWindow(Me)

        Else '登録失敗
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", Me.ButtonSinkiJutyuu.Value) & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonSinkiJutyuu_ServerClick", tmpScript, True)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 修正ボタン押下時処理(実態)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ButtonSyuuseiCheck() As Boolean

        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.ButtonSyuusei

        ' 入力チェック
        If Me.checkInput(objActBtn) = False Then Return False

        ' 画面の内容をDBに反映する
        If Me.SaveData(EarthEnum.emMousikomiUpdType.Syuusei) Then '登録成功
            '画面を閉じる
            cl.CloseWindow(Me)

        Else '登録失敗
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", Me.ButtonSyuusei.Value) & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonSyuusei_ServerClick", tmpScript, True)
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 保留ボタン押下時処理(実態)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ButtonHoryuuCheck() As Boolean
        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.ButtonHoryuu

        ' 入力チェック
        If Me.checkInput(objActBtn) = False Then Return False

        ' 画面の内容をDBに反映する
        If Me.SaveData(EarthEnum.emMousikomiUpdType.Horyuu) Then '登録成功
            '画面を閉じる
            cl.CloseWindow(Me)

        Else '登録失敗
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", Me.ButtonHoryuu.Value) & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHoryuu_ServerClick", tmpScript, True)
            Return False
        End If

        Return True
    End Function

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
        Dim tmpScript As String = "callSearch('" & Me.TextKbn.ClientID & EarthConst.SEP_STRING & Me.TextHosyousyoNo.ClientID & _
                                                                EarthConst.SEP_STRING & Me.TextBMBukkenMeisyou.ClientID & _
                                                                EarthConst.SEP_STRING & Me.TextTBBukkenJyuusyo1.ClientID & EarthConst.SEP_STRING & _
                                                                Me.TextTBBukkenJyuusyo2.ClientID & "', '" & UrlConst.SEARCH_TYOUFUKU & "');"
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & tmpScript, True)
        Exit Sub

    End Sub

#End Region

End Class