Partial Public Class PopupBukkenSintyokuJyky
    Inherits System.Web.UI.Page

    '行コントロールID接頭語
    Private Const CTRL_NAME_TR As String = "resultTr_"
    Private Const SELECT_SYUBETU_CTRL_NAME As String = "SelectSyubetu_"

    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    Dim MLogic As New MessageLogic
    Private dtRec As New HosyousyoKanriRecord

    Const CSS_TEXT_CENTER = "textCenter"
    Const CSS_DATE = "date"

#Region "プロパティ"

#Region "パラメータ/各業務画面"
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
    ''' 番号
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _no As String
    ''' <summary>
    ''' 番号
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
            Exit Sub
        End If

        If IsPostBack = False Then '初期起動時

            '●パラメータのチェック
            Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
            ' Key情報を保持
            pStrKbn = arrSearchTerm(0)     '親画面からPOSTされた情報1 ：区分
            pStrBangou = arrSearchTerm(1)     '親画面からPOSTされた情報2 ：保証書NO

            ' パラメータ不足時は画面を表示しない
            If pStrKbn Is Nothing Or pStrBangou Is Nothing Then
                cl.CloseWindow(Me)
                Exit Sub
            End If

            '●アカウントマスタへの登録有無をチェック(無ければメインへ戻る)
            If userinfo.AccountNo = 0 _
                Then
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            End If

            '****************************************************************************
            ' 地盤データ取得
            '****************************************************************************
            Dim logic As New JibanLogic
            Dim jibanRec As New JibanRecordBase
            jibanRec = logic.GetJibanData(pStrKbn, pStrBangou)

            '地盤読み込みデータがセッションに存在する場合、画面に表示させる
            If jibanRec IsNot Nothing Then
                Me.SetCtrlFromJibanRec(sender, e, jibanRec) '地盤データをコントロールにセット
            Else
                cl.CloseWindow(Me)
                Exit Sub
            End If

            '****************************************************************************
            ' 保証書管理レコード取得
            '****************************************************************************
            Dim rec As New HosyousyoKanriRecord '保証書管理レコード
            Me.SetCtrlFromDataRec(sender, e, rec) '保証書管理レコードから画面表示項目への値セット

            '****************************************************************************
            ' 進捗データ取得
            '****************************************************************************
            If rec.HosyousyoNo <> String.Empty Then
                Dim reportRec As New ReportIfGetRecord '進捗データレコード
                logic.GetReportIfData(jibanRec, reportRec) '進捗データ取得
                Me.SetCtrlFromReportIFDataRec(sender, e, jibanRec, rec, reportRec) 'データをコントロールにセット
            End If

            Me.ButtonClose.Focus() 'フォーカス

        End If

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
        Me.SpanKbn.InnerHtml = objDrpTmp.SelectedItem.Text '区分
        Me.SpanBangou.InnerHtml = cl.GetDispStr(jr.HosyousyoNo) '番号

        '付保証明書FLG
        If jr.FuhoSyoumeisyoFlg = "1" Then
            Me.SpanFuhoSyomeisyoFlg.InnerHtml = "有り"
        ElseIf jr.FuhoSyoumeisyoFlg = "0" Then
            Me.SpanFuhoSyomeisyoFlg.InnerHtml = "無し"
        Else
            Me.SpanFuhoSyomeisyoFlg.InnerHtml = ""
        End If

        '付保証明書発送日
        Me.SpanFuhoSyomeisyoHassoDate.InnerHtml = cl.GetDispStr(jr.FuhoSyoumeisyoHassouDate)


    End Sub

    ''' <summary>
    ''' 保証書管理レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="sender">地盤レコード</param>
    ''' <param name="e">地盤レコード</param>
    ''' <param name="rec">保証書管理レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object, ByVal e As System.EventArgs, ByRef rec As HosyousyoKanriRecord)

        Dim logic As New BukkenSintyokuJykyLogic

        ' コンボ設定ヘルパークラスを生成
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        '●該当データをDBから抽出
        rec = logic.getSearchKeyDataRec(sender, pStrKbn, pStrBangou)

        '保証書管理読み込みデータがセッションに存在しない場合
        If rec.Kbn Is Nothing Or rec.HosyousyoNo Is Nothing Then
            MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
            cl.CloseWindow(Me)
            Exit Sub
        End If

        '●ダミーコンボにセット(解析完了)
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.KAISEKI_KANRY, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(rec.KaisekiKanry)
        Me.HiddenKaisekiKanry.Value = objDrpTmp.SelectedValue
        Me.SpanKaisekiKanry.InnerHtml = objDrpTmp.SelectedItem.Text

        '●ダミーコンボにセット(工事有無)
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.KOJ_UMU, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(rec.KojUmu)
        Me.HiddenKojUmu.Value = objDrpTmp.SelectedValue
        Me.SpanKojUmu.InnerHtml = objDrpTmp.SelectedItem.Text

        '●ダミーコンボにセット(工事完了)
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.KOJ_KANRY, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(rec.KojKanry)
        Me.HiddenKojKanry.Value = objDrpTmp.SelectedValue
        Me.SpanKojKanry.InnerHtml = objDrpTmp.SelectedItem.Text

        '●ダミーコンボにセット(入金確認条件)
        objDrpTmp = New DropDownList
        helper.SetMeisyouDropDownList(objDrpTmp, EarthConst.emMeisyouType.NYUUKIN_KAKUNIN, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(rec.NyuukinKakuninJyouken)
        Me.SpanNyuukinKakuninJyouken.InnerHtml = objDrpTmp.SelectedItem.Text

        '●ダミーコンボにセット(入金状況)
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.NYUUKIN_JYKY, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(rec.NyuukinJyky)
        Me.HiddenNyuukinJyky.Value = objDrpTmp.SelectedValue
        Me.SpanNyuukinJyky.InnerHtml = objDrpTmp.SelectedItem.Text

        '　入金状況=7の場合は日付を表示する
        If rec.NyuukinJyky = 7 Then
            '入金予定日を取得
            Dim strNyuukinYoteiDate As String
            strNyuukinYoteiDate = logic.setNyuukinYoteiDate(Me, rec.Kbn, rec.HosyousyoNo, rec.UpdDateTime)
            '取得した入金予定日を表示
            Me.SpanNyuukinJyky.InnerHtml = Me.SpanNyuukinJyky.InnerHtml.Replace("MM/DD", strNyuukinYoteiDate)
        End If

        '●ダミーコンボにセット(瑕疵)
        objDrpTmp = New DropDownList
        helper.SetKtMeisyouDropDownList(objDrpTmp, EarthConst.emKtMeisyouType.KASI, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(rec.Kasi)
        Me.HiddenKasi.Value = objDrpTmp.SelectedValue
        Me.SpanKasi.InnerHtml = objDrpTmp.SelectedItem.Text

        '表示スタイル設定
        Me.SetStringStyle()

        '画面モード別設定
        Me.SetGamenMode(rec)

    End Sub

    ''' <summary>
    ''' 進捗データから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <param name="rec">保証書管理レコード</param>
    ''' <param name="reportRec">進捗データレコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromReportIFDataRec(ByVal sender As Object, ByVal e As System.EventArgs, _
                                          ByVal jibanRec As JibanRecordBase, ByVal rec As HosyousyoKanriRecord, ByVal reportRec As ReportIfGetRecord)

        '解析
        If reportRec.KaisekiKanryNaiyou <> String.Empty Then
            Me.SpanKaiseki.InnerHtml = reportRec.KaisekiKanryNaiyou
        End If

        '改良工事
        If reportRec.KojKanryHandan <> String.Empty Then
            Me.SpanKairyoKoji.InnerHtml = reportRec.KojKanryHandan
        End If

        'ご入金
        If reportRec.NyuukinJykyHandan <> String.Empty Then
            Me.SpanNyuukin.InnerHtml = reportRec.NyuukinJykyHandan
        End If

        '発行依頼
        If rec.BukkenJyky = 3 Then
            '保証書T.物件状況=3の場合、発行済
            Me.SpanHakkouIrai.InnerHtml = EarthConst.BUKKEN_SINTYOKU_JYKY_JIBANMOLE_HAKKOUIRAI_ZUMI
        ElseIf cl.GetDisplayString(jibanRec.HosyousyoHakkouDate) <> Nothing Then
            '地盤T.保証書発効日<>空白の場合、受付完了
            Me.SpanHakkouIrai.InnerHtml = EarthConst.BUKKEN_SINTYOKU_JYKY_JIBANMOLE_HAKKOUIRAI_KANRY
        ElseIf cl.GetDisplayString(IIf(jibanRec.HosyousyoHakIraisyoUmu = 1, "1", "")) = "1" OrElse _
               cl.GetDisplayString(reportRec.HakIraiTime) <> Nothing Then
            '地盤T.保証書発行依頼書有無=1 orelse 進捗T.発行依頼日時<>空白の場合、ご依頼済
            Me.SpanHakkouIrai.InnerHtml = EarthConst.BUKKEN_SINTYOKU_JYKY_JIBANMOLE_HAKKOUIRAI_IRAIZUMI
        Else
            '未
            Me.SpanHakkouIrai.InnerHtml = EarthConst.BUKKEN_SINTYOKU_JYKY_JIBANMOLE_HAKKOUIRAI_MI
        End If

        '工事内容
        If reportRec.KojKanryNaiyou <> String.Empty Then
            Me.SpanKojKanryNaiyou.InnerHtml = reportRec.KojKanryNaiyou
        End If

        '入金状況
        If reportRec.NyuukinJykyNaiyou <> String.Empty Then
            Me.SpanNyuukinJykyNaiyou.InnerHtml = reportRec.NyuukinJykyNaiyou
        End If

    End Sub

#Region "プライベートメソッド"

    ''' <summary>
    ''' 保証書管理テーブルから取得した情報をもとに、
    ''' 表示スタイルを白抜きの赤字太字に設定する
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetStringStyle()
        '解析完了
        If Me.HiddenKaisekiKanry.Value = "0" OrElse Me.HiddenKaisekiKanry.Value = "2" Then
            Me.SpanKaisekiKanry.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
            Me.SpanKaisekiKanry.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
            Me.CellKaisekiKanry.BgColor = EarthConst.STYLE_COLOR_WHITE
        End If
        '工事完了
        If (Me.HiddenKojUmu.Value = "1" AndAlso Me.HiddenKojKanry.Value = "1") _
            OrElse (Me.HiddenKojUmu.Value = "0" AndAlso (Me.HiddenKojKanry.Value = "0" OrElse Me.HiddenKojKanry.Value = "1")) Then
        Else
            Me.SpanKojKanry.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
            Me.SpanKojKanry.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
            Me.CellKojKanry.BgColor = EarthConst.STYLE_COLOR_WHITE
        End If
        '入金状況
        If Me.HiddenNyuukinJyky.Value = "2" OrElse Me.HiddenNyuukinJyky.Value = "4" _
            OrElse Me.HiddenNyuukinJyky.Value = "5" OrElse Me.HiddenNyuukinJyky.Value = "6" Then
            Me.SpanNyuukinJyky.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
            Me.SpanNyuukinJyky.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
            Me.CellNyuukinJyky.BgColor = EarthConst.STYLE_COLOR_WHITE
        End If
        '瑕疵[建物検査]
        If Me.HiddenKasi.Value = "2" OrElse Me.HiddenKasi.Value = "3" OrElse Me.HiddenKasi.Value = "4" Then
            Me.SpanKasi.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
            Me.SpanKasi.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
            Me.CellKasi.BgColor = EarthConst.STYLE_COLOR_WHITE
        End If
    End Sub


    ''' <summary>
    ''' 画面モードを設定する
    ''' </summary>
    ''' <param name="dataRec">保証書管理レコード</param>
    ''' <remarks></remarks>
    Private Sub SetGamenMode(ByVal dataRec As HosyousyoKanriRecord)

        If cl.GetDisplayString(dataRec.SyoriFlg) = "0" Or cl.GetDisplayString(dataRec.SyoriFlg) = "1" Then
            pStrGamenMode = CStr(EarthEnum.emBukkenSintyokuJykyGamenMode.Nitiji)
        ElseIf cl.GetDisplayString(dataRec.SyoriFlg) = "2" Then
            pStrGamenMode = CStr(EarthEnum.emBukkenSintyokuJykyGamenMode.Getuji)
        End If

        'Hiddenに退避
        Me.HiddenGamenMode.Value = pStrGamenMode

        '画面設定
        Me.SetDispControl(dataRec)

    End Sub


    ''' <summary>
    ''' 画面モード別の画面設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispControl(ByVal dataRec As HosyousyoKanriRecord)
        'Hiddenより再設定
        pStrGamenMode = Me.HiddenGamenMode.Value

        If pStrGamenMode = CStr(EarthEnum.emBukkenSintyokuJykyGamenMode.Nitiji) Then '日次
            '最終処理日時(日次)
            Me.SpanSyoriDateType.InnerHtml = "最終処理日時(日次) : "
        ElseIf pStrGamenMode = CStr(EarthEnum.emBukkenSintyokuJykyGamenMode.Getuji) Then '月次
            '最終処理日時(月次)
            Me.SpanSyoriDateType.InnerHtml = "最終処理日時(月次) : "
        End If

        '処理日時を指定
        If cl.GetDisplayString(dataRec.SyoriDateTime) <> "" Then
            Me.TextSyoriDate.Text = cl.GetDisplayString(Format(dataRec.SyoriDateTime, "yyyy/MM/dd HH:mm"))
        Else
            Me.TextSyoriDate.Text = ""
        End If

    End Sub

#End Region

End Class