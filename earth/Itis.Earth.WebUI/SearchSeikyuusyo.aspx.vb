Partial Public Class SearchSeikyuusyo
    Inherits System.Web.UI.Page

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

    '共通ロジック
    Private cl As New CommonLogic
    'メッセージクラス
    Private MLogic As New MessageLogic
    '請求データロジッククラス
    Dim MyLogic As New SeikyuuDataSearchLogic
    '請求未印刷データロジッククラス
    Dim SubLogic As New SeikyuuMiinsatuDataSearchLogic

    '請求データ検索KEYレコードクラス
    Private keyRec As New SeikyuuDataKeyRecord
    '請求データレコードクラス
    Private dtRec As New SeikyuuDataRecord

#Region "プロパティ"

#Region "パラメータ/請求書データ作成画面"

    ''' <summary>
    ''' 起動画面
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _GamenMode As String = String.Empty
    ''' <summary>
    ''' 起動画面
    ''' </summary>
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

#Region "コントロール値"
    'タイトル
    Private Const CTRL_VALUE_TITLE As String = "請求書一覧"
    Private Const CTRL_VALUE_TITLE_KAKO As String = "過去" & CTRL_VALUE_TITLE
    '請求書式ドロップダウンリスト
    Private Const CTRL_VALUE_SEIKYUUSYOSIKI_DDL_ISNULL As String = "請求書式未設定"

    '検索実行時、非表示データが存在する場合メッセージエリアに以下を表示する
    Private Const ALERT_MSG_DISP_NONE As String = "※表示しきれていないデータがあります"

    Private Const CTRL_NAME_TR1 As String = "DataTable_resultTr1_"
    Private Const CTRL_NAME_TR2 As String = "DataTable_resultTr2_"
    Private Const CTRL_NAME_TD_SENTOU As String = "DataTable_Sentou_Td_"
    Private Const CTRL_NAME_HIDDEN_SEIKYUUSYO_NO As String = "HdnSeikyuusyoNo_"
    Private Const CTRL_NAME_HIDDEN_UPDDATETIME As String = "HdnUpdDatetime_"
    Private Const CTRL_NAME_HIDDEN_PRINT_TAISYOUGAI_FLG As String = "HdnPrintFlg_"
    Private Const CTRL_NAME_HIDDEN_TORIKESI_FLG As String = "HdnTorikesiFlg_"
    Private Const CTRL_NAME_HIDDEN_SYOSIKI_FLG As String = "HdnSyosikiFlg_"
    Private Const CTRL_NAME_HIDDEN_MEISAI_CNT As String = "HdnMeisaiCnt_"
    Private Const CTRL_NAME_CHECK_TAISYOU As String = "ChkTaisyou_"
#End Region

#Region "CSSクラス名"
    Private Const CSS_TEXT_CENTER = "textCenter"
    Private Const CSS_KINGAKU = "kingaku"
    Private Const CSS_NUMBER = "number"
    Private Const CSS_DATE = "date"
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
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If IsPostBack = False Then
            Try
                ' 引渡パラメータを保持
                '●パラメータのチェック
                pStrGamenMode = Request("st")

                ' パラメータ不足時は画面を表示しない(画面モード)
                If pStrGamenMode Is Nothing Then
                    Response.Redirect(UrlConst.SEIKYUUSYO_DATA_SAKUSEI)
                    Exit Sub
                Else
                    Me.TextSekyuusyoHakDateTo.Value = Date.Now.ToString("yyyy/MM/dd")
                    'Hiddenに格納
                    Me.HiddenPrmSeikyuusyoHakDateTo.Value = Date.Now.ToString("yyyy/MM/dd")
                    Me.HiddenGamenMode.Value = pStrGamenMode
                End If
            Catch ex As Exception
                Response.Redirect(UrlConst.SEIKYUUSYO_DATA_SAKUSEI)
                Exit Sub
            End Try

            '●権限のチェック
            '経理業務権限
            If userinfo.KeiriGyoumuKengen = 0 Then
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            Dim helper As New DropDownHelper

            ' 請求先区分コンボにデータをバインドする
            helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)

            ' 請求書式コンボにデータをバインドする
            helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSyousiki, EarthConst.emKtMeisyouType.KAISYUU_SEIKYUUSYO_YOUSI, True, False)
            'IS NULL検索用にAdd
            Me.SelectSeikyuuSyousiki.Items.Add(New ListItem(CTRL_VALUE_SEIKYUUSYOSIKI_DDL_ISNULL, EarthConst.ISNULL))

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            setDispAction()

            '画面設定
            Me.SetDispControl(sender, e)

            'ボタン押下イベントの設定
            setBtnEvent()

            'フォーカス設定
            Me.TextSekyuusyoHakDateFrom.Focus()

        Else
            '画面設定
            Me.SetDispControl(sender, e)

        End If

    End Sub

#Region "ボタンイベント"
    ''' <summary>
    ''' 検索実行ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick
        setFocusAJ(Me.btnSearch) 'フォーカス

        Me.SetCmnSearchResult(sender, e)

    End Sub

    ''' <summary>
    ''' CSV出力ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHiddenCsv_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenCsv.ServerClick
        setFocusAJ(Me.ButtonCsv) 'フォーカス

        Me.HiddenCsvOutPut.Value = String.Empty 'フラグをクリア
        Me.HiddenCsvMaxCnt.Value = String.Empty

        'レコードクラス
        Dim dtCsv As New DataTable
        Dim strFileNm As String = String.Empty

        '●検索条件の取得
        Me.SetSearchKeyFromCtrl(keyRec)

        '件数
        Dim total_count As Integer = 0

        '画面モード別処理
        If Me.HiddenGamenMode.Value = EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo Then '請求書一覧画面
            '検索実行
            dtCsv = MyLogic.GetSeikyuusyoDataCsv(sender, keyRec, total_count, EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo)
            strFileNm = EarthConst.FILE_NAME_CSV_SEIKYUUSYO_DATA

        ElseIf Me.HiddenGamenMode.Value = EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo Then '過去請求書一覧画面
            '検索実行
            dtCsv = MyLogic.GetSeikyuusyoDataCsv(sender, keyRec, total_count, EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo)
            strFileNm = EarthConst.FILE_NAME_CSV_KAKO_SEIKYUUSYO_DATA
        End If

        ' 検索結果ゼロ件の場合、メッセージを表示
        If total_count = 0 Then
            ' 検索結果ゼロ件の場合、メッセージを表示
            Dim tmpScript As String = "if(gNoDataMsgFlg != '1'){alert('" & Messages.MSG020E & "')}gNoDataMsgFlg = null;"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "kensakuZero", tmpScript, True)
            Exit Sub
        ElseIf total_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV出力"), 0, "kensakuErr")
            Exit Sub
        End If

        '出力用データテーブルを基に、CSV出力を行なう
        If cl.OutPutFileFromDtTable(strFileNm, dtCsv) = False Then
            ' 出力用文字列がないので、処理終了
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV出力"), 0, "OutputErr")
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 戻るボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonReturn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReturn.ServerClick
        Response.Redirect(UrlConst.SEIKYUUSYO_DATA_SAKUSEI)
    End Sub

    ''' <summary>
    ''' 印刷ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSeikyuusyoPrint_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSeikyuusyoPrint.ServerClick
        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.ButtonSeikyuusyoPrint

        '印刷ボタン押下時、DB更新を行なう
        If objActBtn.Value = EarthConst.BUTTON_SEIKYUUSYO_PRINT Then

            ' 入力チェック
            If Me.checkInput(objActBtn) = False Then Exit Sub

            tmpScript = "gNoDataMsgFlg = '1';"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHiddenPrint_FlgSet", tmpScript, True)

            ' 画面の内容をDBに反映する
            If Me.SaveData(EarthEnum.emSeikyuusyoUpdType.SeikyuusyoPrint) Then '登録成功

                Me.SetCmnSearchResult(sender, e)

            Else '登録失敗
                Me.SetCmnSearchResult(sender, e)

                setFocusAJ(Me.btnSearch) 'フォーカス

                tmpScript = "alert('" & Messages.MSG147E.Replace("@PARAM1", objActBtn.Value) & "');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHiddenPrint_ServerClick", tmpScript, True)
                Exit Sub
            End If

        ElseIf objActBtn.Value = EarthConst.BUTTON_SEIKYUUSYO_RE_PRINT Then '再印刷ボタン押下時

            Me.SetCmnSearchResult(sender, e)

        End If

        'PDF出力処理
        tmpScript = "gVarPdfFlg = '1';"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHiddenPrint_ServerClick2", tmpScript, True)

    End Sub

    ''' <summary>
    ''' 請求書取消ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSeikyuusyoTorikesi_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSeikyuusyoTorikesi.ServerClick
        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.ButtonSeikyuusyoTorikesi

        ' 入力チェック
        If Me.checkInput(objActBtn) = False Then Exit Sub

        ' 画面の内容をDBに反映する
        If Me.SaveData(EarthEnum.emSeikyuusyoUpdType.SeikyuusyoTorikesi) Then '登録成功

            tmpScript = "gNoDataMsgFlg = '1';"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHiddenTorikesi_ServerClick", tmpScript, True)

            Me.SetCmnSearchResult(sender, e)
        Else '登録失敗
            Me.SetCmnSearchResult(sender, e)

            setFocusAJ(Me.btnSearch) 'フォーカス

            tmpScript = "alert('" & Messages.MSG147E.Replace("@PARAM1", "請求書取消") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHiddenTorikesi_ServerClick", tmpScript, True)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 請求先検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSeikyuuSakiSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeikyuuSakiSearch.ServerClick
        Dim blnResult As Boolean

        '請求先検索画面呼出
        blnResult = cl.CallSeikyuuSakiSearchWindow(sender, e, Me _
                                                        , Me.SelectSeikyuuSakiKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc _
                                                        , Me.TextSeikyuuSakiMei, Me.btnSeikyuuSakiSearch)
        If blnResult Then
            'フォーカスセット
            setFocusAJ(Me.btnSeikyuuSakiSearch)
        End If

    End Sub

#End Region

#Region "プライベートメソッド"
    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        Dim strJs As String

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ボタンのイベントハンドラを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '検索実行ボタン
        Me.btnSearch.Attributes("onclick") = "checkJikkou('0');"
        'CSV出力ボタン
        Me.ButtonCsv.Attributes("onclick") = "checkJikkou('1');"

        '未印刷一覧ボタン
        strJs = "window.open('" & UrlConst.POPUP_SEIKYUUSYO_MIINSATU & _
        "','this','menubar=no,toolbar=no,location=no,status=no,resizable=yes,scrollbars=yes')"
        Me.ButtonMiinsatu.Attributes.Add("onclick", strJs)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'マスタ検索系コード入力項目イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSeikyuuSakiCd.Attributes("onblur") = "clrName(this,'" & Me.TextSeikyuuSakiMei.ClientID & "');"
        Me.TextSeikyuuSakiBrc.Attributes("onblur") = "clrName(this,'" & Me.TextSeikyuuSakiMei.ClientID & "');"
        Me.SelectSeikyuuSakiKbn.Attributes("onblur") = "clrName(this,'" & Me.TextSeikyuuSakiMei.ClientID & "');"

    End Sub

    ''' <summary>
    ''' 各種ボタンの設定
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Private Sub setBtnEvent()
        Dim strChkTaisyou As String = "ChkTaisyou(@PARAM1)"
        Dim strChkJikkou As String = "checkJikkou(@PARAM1)"

        'イベントハンドラ登録
        Dim tmpTouroku As String = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this);}else{return false;}"
        Dim tmpScript As String = "if(" & strChkTaisyou & " && " & strChkJikkou & "){" & tmpTouroku & "}else{return false;}"

        '確認MSG後、OKの場合後続処理を行なう
        Me.ButtonSeikyuusyoPrint.Attributes("onclick") = tmpScript.Replace("@PARAM1", EarthEnum.emSearchSeikyuusyoBtnType.Print)
        Me.ButtonSeikyuusyoTorikesi.Attributes("onclick") = tmpScript.Replace("@PARAM1", EarthEnum.emSearchSeikyuusyoBtnType.Torikesi)

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
    ''' 画面項目から検索キーレコードへの値セットを行う。
    ''' </summary>
    ''' <param name="recKey">抽出レコードクラスのキー</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromCtrl(ByRef recKey As SeikyuuDataKeyRecord)

        '請求書NO群
        recKey.ArrSeikyuuSakiNo = IIf(Me.HiddenSendValSeikyuusyoNo.Value <> String.Empty, Me.HiddenSendValSeikyuusyoNo.Value, String.Empty)

        '請求書発行日 From
        recKey.SeikyuusyoHakDateFrom = IIf(Me.TextSekyuusyoHakDateFrom.Value <> String.Empty, Me.TextSekyuusyoHakDateFrom.Value, DateTime.MinValue)
        '請求書発行日 To
        recKey.SeikyuusyoHakDateTo = IIf(Me.TextSekyuusyoHakDateTo.Value <> String.Empty, Me.TextSekyuusyoHakDateTo.Value, DateTime.MinValue)
        '請求先区分
        recKey.SeikyuuSakiKbn = IIf(Me.SelectSeikyuuSakiKbn.SelectedValue <> String.Empty, Me.SelectSeikyuuSakiKbn.SelectedValue, String.Empty)
        '請求先コード
        recKey.SeikyuuSakiCd = IIf(Me.TextSeikyuuSakiCd.Value <> String.Empty, Me.TextSeikyuuSakiCd.Value, String.Empty)
        '請求先枝番
        recKey.SeikyuuSakiBrc = IIf(Me.TextSeikyuuSakiBrc.Value <> String.Empty, Me.TextSeikyuuSakiBrc.Value, String.Empty)
        '請求先カナ名
        recKey.SeikyuuSakiMeiKana = IIf(Me.TextSeikyuuSakiMeiKana.Value <> String.Empty, Me.TextSeikyuuSakiMeiKana.Value, String.Empty)
        '取消
        recKey.Torikesi = IIf(Me.CheckTorikesiTaisyou.Checked, 0, Integer.MinValue)

        '画面別処理
        If Me.HiddenGamenMode.Value = EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo Then '請求書一覧画面
            '請求締日
            recKey.SeikyuuSimeDate = IIf(Me.TextSeikyuuSimeDate.Value <> String.Empty, Me.TextSeikyuuSimeDate.Value, String.Empty)
            '請求書式
            recKey.SeikyuuSyosiki = IIf(Me.SelectSeikyuuSyousiki.SelectedValue <> String.Empty, Me.SelectSeikyuuSyousiki.SelectedValue, String.Empty)
            '明細件数 FROM
            recKey.MeisaiKensuuFrom = IIf(Me.TextMeisaiKensuuFrom.Value <> String.Empty, Me.TextMeisaiKensuuFrom.Value, Integer.MinValue)
            '明細件数 TO
            recKey.MeisaiKensuuTo = IIf(Me.TextMeisaiKensuuTo.Value <> String.Empty, Me.TextMeisaiKensuuTo.Value, Integer.MinValue)
            '印字用紙
            recKey.InjiYousi = Integer.MinValue

        ElseIf Me.HiddenGamenMode.Value = EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo Then '過去請求書一覧画面
            '請求締日
            recKey.SeikyuuSimeDate = IIf(Me.TextSeikyuuSimeDate.Value <> String.Empty, Me.TextSeikyuuSimeDate.Value, String.Empty)
            '請求書式
            recKey.SeikyuuSyosiki = IIf(Me.SelectSeikyuuSyousiki.SelectedValue <> String.Empty, Me.SelectSeikyuuSyousiki.SelectedValue, String.Empty)
            '明細件数 FROM
            recKey.MeisaiKensuuFrom = Integer.MinValue
            '明細件数 TO
            recKey.MeisaiKensuuTo = Integer.MinValue
            '印字用紙
            recKey.InjiYousi = IIf(Me.CheckInjiTaisyou.Checked, 0, Integer.MinValue)
        End If

    End Sub

    ''' <summary>
    ''' 検索条件で検索した内容を検索結果テーブルに表示する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '表示最大件数
        Dim end_count As Integer = maxSearchCount.Value
        Dim total_count As Integer = 0 '取得件数
        Dim intMihakkou As Integer = 0 '未発行件数
        Dim intCsvMaxCnt As Integer = Integer.Parse(EarthConst.MAX_CSV_OUTPUT)

        '検索実行
        Dim resultArray As New List(Of SeikyuuDataRecord)

        '対象チェックボックスの初期化(CSV出力時以外)
        If Me.HiddenCsvOutPut.Value <> "1" Then
            Me.CheckAll.Checked = False
        End If

        '画面モード別処理
        If Me.HiddenGamenMode.Value = EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo Then '請求書一覧画面
            resultArray = MyLogic.GetSeikyuuDataInfo(sender, keyRec, 1, end_count, total_count, EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo)

            '未発行件数を設定
            MyLogic.GetMihakkouCnt(sender, Me.TdMihakkou.InnerHtml)

            'スタイル設定
            If Me.TrMihakkou.Style("display") = "inline" Then
                'スタイル設定
                If Me.TdMihakkou.InnerHtml = String.Empty OrElse Me.TdMihakkou.InnerHtml = "0" Then
                    Me.TdMihakkou.Style("color") = ""
                    Me.TdMihakkou.Style("font-weight") = ""
                Else
                    Me.TdMihakkou.Style("color") = "red"
                    Me.TdMihakkou.Style("font-weight") = "bold"
                End If
            End If

        ElseIf Me.HiddenGamenMode.Value = EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo Then '過去請求書一覧画面
            resultArray = MyLogic.GetSeikyuuDataInfo(sender, keyRec, 1, end_count, total_count, EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo)
        End If

        ' 検索結果ゼロ件の場合、メッセージを表示
        If total_count = 0 Then
            ' 検索結果ゼロ件の場合、メッセージを表示
            Dim tmpScript As String = "if(gNoDataMsgFlg != '1'){alert('" & Messages.MSG020E & "')}gNoDataMsgFlg = null;"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "kensakuZero", tmpScript, True)
            Me.HiddenCsvOutPut.Value = ""
        ElseIf total_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            Exit Sub
        End If

        '表示件数制限処理
        Dim displayCount As String = ""
        displayCount = total_count
        resultCount.Style.Remove("color")

        If end_count < total_count Then
            resultCount.Style("color") = "red"
            displayCount = CommonLogic.Instance.GetDisplayString(end_count) & " / " & CommonLogic.Instance.GetDisplayString(total_count)
            'MSGエリアの表示切替
            Me.TdMsgArea.InnerHtml = ALERT_MSG_DISP_NONE
        Else
            'MSGエリアの表示切替
            Me.TdMsgArea.InnerHtml = String.Empty
        End If

        '検索結果件数を設定
        resultCount.InnerHtml = displayCount

        '************************
        '* 画面テーブルへ出力
        '************************
        '行カウンタ
        Dim rowCnt As Integer = 0
        Dim strSpace As String = EarthConst.HANKAKU_SPACE
        Dim intTorikesi As Integer = 0

        '************
        '* セル幅取得
        '************
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

        '************
        '* 変数宣言
        '************
        'TabIndex
        Dim intTrTabIndex As Integer = 5
        'Tr
        Dim objTr1 As HtmlTableRow
        Dim objTr2 As HtmlTableRow

        Dim strSeikyuusyoNo As String                   '請求書NO

        'Hidden 
        Dim objHdnSeikyuusyoNo As HtmlInputHidden       '請求書NO
        Dim objHdnUpdDatetime As HtmlInputHidden        '更新日時
        Dim objHdnPrintTaisyougai As HtmlInputHidden    '印刷出力対象外フラグ
        Dim objHdnTorikesi As HtmlInputHidden           '取消フラグ
        Dim objHdnSyosiki As HtmlInputHidden            '請求書式フラグ
        Dim objHdnMeisaiCnt As HtmlInputHidden          '請求明細件数
        'CheckBox 
        Dim objChkTaisyou As HtmlInputCheckBox          '対象
        'Table Cell
        Dim objTdTaisyou As HtmlTableCell               '対象
        Dim objTdSeikyuusyoNo As HtmlTableCell          '請求書NO
        Dim objTdTorikesi As HtmlTableCell              '取消
        Dim objTdSeikyuuSakiCd As HtmlTableCell         '請求先コード
        Dim objTdSeikyuuSakiMei As HtmlTableCell        '請求先名
        Dim objTdSeikyuuSakiMei2 As HtmlTableCell       '請求先名2

        Dim objTdSeikyuusyoHakDate As HtmlTableCell     '請求書発行日
        Dim objTdSeikyuuSimeDate As HtmlTableCell       '請求締め日
        Dim objTdKaisyuuYoteiDate As HtmlTableCell      '回収予定日

        Dim objTdZenkaiGoseikyuuGaku As HtmlTableCell   '前回御請求金額
        Dim objTdGonyuukinGaku As HtmlTableCell         '御入金額
        Dim objTdZenkaiKurikosiZandaka As HtmlTableCell '前回繰越残高
        Dim objTdKonkaiGoseikyuuGaku As HtmlTableCell   '今回御請求額
        Dim objTdKurikosiZandaka As HtmlTableCell       '繰越残高

        Dim objTdYuubinNo As HtmlTableCell              '郵便番号
        Dim objTdTelNo As HtmlTableCell                 '電話番号
        Dim objTdJuusyo1 As HtmlTableCell               '住所1
        Dim objTdJuusyo2 As HtmlTableCell               '住所2

        '取得したデータを画面に表示
        For Each data As SeikyuuDataRecord In resultArray
            rowCnt += 1

            'Tr
            objTr1 = New HtmlTableRow
            objTr2 = New HtmlTableRow

            strSeikyuusyoNo = String.Empty

            '***********
            '* Table1
            '********
            'Hidden 
            objHdnSeikyuusyoNo = New HtmlInputHidden       'Hidden請求書NO
            objHdnUpdDatetime = New HtmlInputHidden        'Hidden更新日時
            objHdnPrintTaisyougai = New HtmlInputHidden    'Hidden印刷出力対象外フラグ
            objHdnTorikesi = New HtmlInputHidden           'Hidden取消フラグ
            objHdnSyosiki = New HtmlInputHidden
            objHdnMeisaiCnt = New HtmlInputHidden
            'CheckBox 
            objChkTaisyou = New HtmlInputCheckBox          '対象チェックボックス
            'Table Cell
            objTdTaisyou = New HtmlTableCell               '対象
            objTdSeikyuusyoNo = New HtmlTableCell          '請求書NO
            objTdTorikesi = New HtmlTableCell              '取消
            objTdSeikyuuSakiCd = New HtmlTableCell         '請求先コード
            objTdSeikyuuSakiMei = New HtmlTableCell        '請求先名
            objTdSeikyuuSakiMei2 = New HtmlTableCell       '請求先名2

            '***********
            '* Table2
            '********
            objTdSeikyuusyoHakDate = New HtmlTableCell     '請求書発行日
            objTdSeikyuuSimeDate = New HtmlTableCell       '請求締め日
            objTdKaisyuuYoteiDate = New HtmlTableCell      '回収予定日

            objTdZenkaiGoseikyuuGaku = New HtmlTableCell   '前回御請求額
            objTdGonyuukinGaku = New HtmlTableCell         '御入金額
            objTdZenkaiKurikosiZandaka = New HtmlTableCell '前回繰越残高
            objTdKonkaiGoseikyuuGaku = New HtmlTableCell   '今回御請求額
            objTdKurikosiZandaka = New HtmlTableCell       '繰越残高

            objTdYuubinNo = New HtmlTableCell              '郵便番号
            objTdTelNo = New HtmlTableCell                 '電話番号
            objTdJuusyo1 = New HtmlTableCell               '住所1
            objTdJuusyo2 = New HtmlTableCell               '住所2

            '検索結果配列からセルに格納
            objTdTaisyou.ID = CTRL_NAME_TD_SENTOU & rowCnt

            '請求書NOを取得
            strSeikyuusyoNo = cl.GetDisplayString(data.SeikyuusyoNo)
            '取消を取得
            intTorikesi = IIf(data.Torikesi = 0, data.Torikesi, 1)

            '******************
            '* 先頭列
            '*************
            'Hidden請求書NO
            objHdnSeikyuusyoNo.ID = CTRL_NAME_HIDDEN_SEIKYUUSYO_NO & rowCnt
            objHdnSeikyuusyoNo.Value = strSeikyuusyoNo
            objTdTaisyou.Controls.Add(objHdnSeikyuusyoNo)

            'Hidden更新日時
            objHdnUpdDatetime.ID = CTRL_NAME_HIDDEN_UPDDATETIME & rowCnt
            objHdnUpdDatetime.Value = IIf(data.UpdDatetime = DateTime.MinValue, Format(data.AddDatetime, EarthConst.FORMAT_DATE_TIME_1), Format(data.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
            objTdTaisyou.Controls.Add(objHdnUpdDatetime)

            'HiddenCSV出力対象外フラグ
            objHdnPrintTaisyougai.ID = CTRL_NAME_HIDDEN_PRINT_TAISYOUGAI_FLG & rowCnt
            objHdnPrintTaisyougai.Value = IIf(data.PrintTaisyougaiFlg = 1, data.PrintTaisyougaiFlg, 0)
            objTdTaisyou.Controls.Add(objHdnPrintTaisyougai)

            'Hidden取消フラグ
            objHdnTorikesi.ID = CTRL_NAME_HIDDEN_TORIKESI_FLG & rowCnt
            objHdnTorikesi.Value = intTorikesi.ToString
            objTdTaisyou.Controls.Add(objHdnTorikesi)

            'Hidden書式フラグ
            objHdnSyosiki.ID = CTRL_NAME_HIDDEN_SYOSIKI_FLG & rowCnt
            objHdnSyosiki.Value = cl.GetDisplayString(data.KaisyuuSeikyuusyoYousi, EarthConst.ISNULL)
            objTdTaisyou.Controls.Add(objHdnSyosiki)

            'Hidden明細件数
            objHdnMeisaiCnt.ID = CTRL_NAME_HIDDEN_MEISAI_CNT & rowCnt
            objHdnMeisaiCnt.Value = cl.GetDisplayString(data.MeisaiKensuu, 0)
            objTdTaisyou.Controls.Add(objHdnMeisaiCnt)

            '対象チェックボックス(戻り要素(hidden)は、必ず先頭列の先頭のInputタグにセットすること)
            objChkTaisyou.ID = CTRL_NAME_CHECK_TAISYOU & strSeikyuusyoNo
            objChkTaisyou.Attributes("tabindex") = Integer.Parse(Me.CheckTorikesiTaisyou.Attributes("tabindex")) + intTrTabIndex + rowCnt
            objTdTaisyou.Controls.Add(objChkTaisyou)
            '******************

            objTdSeikyuusyoNo.InnerHtml = cl.GetDisplayString(strSeikyuusyoNo, strSpace)
            If intTorikesi = 0 Then
                objTdTorikesi.InnerHtml = strSpace
            ElseIf intTorikesi = 1 Then
                objTdTorikesi.InnerHtml = EarthConst.TORIKESI
            End If
            objTdSeikyuuSakiCd.InnerHtml = cl.GetDispSeikyuuSakiCd(data.SeikyuuSakiKbn, data.SeikyuuSakiCd, data.SeikyuuSakiBrc, False)
            objTdSeikyuuSakiMei.InnerHtml = cl.GetDisplayString(data.SeikyuuSakiMei, strSpace)
            objTdSeikyuuSakiMei2.InnerHtml = cl.GetDisplayString(data.SeikyuuSakiMei2, strSpace)

            objTdSeikyuusyoHakDate.InnerHtml = cl.GetDisplayString(data.SeikyuusyoHakDate, strSpace)
            '●ToolTip設定
            cl.setSeikyuusyoToolTip(data, objTdSeikyuusyoHakDate, CommonLogic.emToolTipSetType.SeikyuusyoHakDate)
            objTdSeikyuuSimeDate.InnerHtml = cl.GetDisplayString(data.SeikyuuSimeDateMst, strSpace)
            '●ToolTip設定
            cl.setSeikyuusyoToolTip(data, objTdSeikyuuSimeDate, CommonLogic.emToolTipSetType.SeikyuuSimeDate)
            objTdKaisyuuYoteiDate.InnerHtml = cl.GetDisplayString(data.KonkaiKaisyuuYoteiDate, strSpace)

            objTdZenkaiGoseikyuuGaku.InnerHtml = cl.GetDisplayString(Format(data.ZenkaiGoseikyuuGaku, EarthConst.FORMAT_KINGAKU_1), strSpace)
            objTdGonyuukinGaku.InnerHtml = cl.GetDisplayString(Format(data.GonyuukinGaku, EarthConst.FORMAT_KINGAKU_1), strSpace)
            objTdZenkaiKurikosiZandaka.InnerHtml = cl.GetDisplayString(Format(data.KurikosiGaku, EarthConst.FORMAT_KINGAKU_1), strSpace)
            objTdKonkaiGoseikyuuGaku.InnerHtml = cl.GetDisplayString(Format(data.KonkaiGoseikyuuGaku, EarthConst.FORMAT_KINGAKU_1), strSpace)
            objTdKurikosiZandaka.InnerHtml = cl.GetDisplayString(Format(data.KonkaiKurikosiGaku, EarthConst.FORMAT_KINGAKU_1), strSpace)

            objTdYuubinNo.InnerHtml = cl.GetDisplayString(data.YuubinNo, strSpace)
            objTdTelNo.InnerHtml = cl.GetDisplayString(data.TelNo, strSpace)
            objTdJuusyo1.InnerHtml = cl.GetDisplayString(data.Jyuusyo1, strSpace)
            objTdJuusyo2.InnerHtml = cl.GetDisplayString(data.Jyuusyo2, strSpace)

            '各セルの幅設定
            If rowCnt = 1 Then
                objTdTaisyou.Style("width") = widthList1(0)
                objTdSeikyuusyoNo.Style("width") = widthList1(1)
                objTdTorikesi.Style("width") = widthList1(2)
                objTdSeikyuuSakiCd.Style("width") = widthList1(3)
                objTdSeikyuuSakiMei.Style("width") = widthList1(4)
                objTdSeikyuuSakiMei2.Style("width") = widthList1(5)

                objTdSeikyuusyoHakDate.Style("width") = widthList2(0)
                objTdSeikyuuSimeDate.Style("width") = widthList2(1)
                objTdKaisyuuYoteiDate.Style("width") = widthList2(2)

                objTdZenkaiGoseikyuuGaku.Style("width") = widthList2(3)
                objTdGonyuukinGaku.Style("width") = widthList2(4)
                objTdZenkaiKurikosiZandaka.Style("width") = widthList2(5)
                objTdKonkaiGoseikyuuGaku.Style("width") = widthList2(6)
                objTdKurikosiZandaka.Style("width") = widthList2(7)

                objTdYuubinNo.Style("width") = widthList2(8)
                objTdTelNo.Style("width") = widthList2(9)
                objTdJuusyo1.Style("width") = widthList2(10)
                objTdJuusyo2.Style("width") = widthList2(11)
            End If

            'スタイル、クラス設定
            objTdTaisyou.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuusyoNo.Attributes("class") = CSS_TEXT_CENTER
            objTdTorikesi.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuuSakiCd.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuuSakiMei.Attributes("class") = ""
            objTdSeikyuuSakiMei2.Attributes("class") = ""

            objTdSeikyuusyoHakDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdSeikyuuSimeDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdKaisyuuYoteiDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER

            objTdZenkaiGoseikyuuGaku.Attributes("class") = CSS_KINGAKU
            objTdGonyuukinGaku.Attributes("class") = CSS_KINGAKU
            objTdZenkaiKurikosiZandaka.Attributes("class") = CSS_KINGAKU
            objTdKonkaiGoseikyuuGaku.Attributes("class") = CSS_KINGAKU
            objTdKurikosiZandaka.Attributes("class") = CSS_KINGAKU

            objTdYuubinNo.Attributes("class") = CSS_TEXT_CENTER
            objTdTelNo.Attributes("class") = CSS_TEXT_CENTER
            objTdJuusyo1.Attributes("class") = ""
            objTdJuusyo2.Attributes("class") = ""

            '行IDとJSイベントの付与
            objTr1.ID = CTRL_NAME_TR1 & rowCnt
            objTr1.Attributes("tabindex") = -1

            '行にセルをセット1
            With objTr1.Controls
                .Add(objTdTaisyou) '戻り要素(hidden)は、必ず先頭列の先頭のInputタグにセットすること
                .Add(objTdSeikyuusyoNo)
                .Add(objTdTorikesi)
                .Add(objTdSeikyuuSakiCd)
                .Add(objTdSeikyuuSakiMei)
                .Add(objTdSeikyuuSakiMei2)
            End With

            '行IDとJSイベントの付与
            objTr2.ID = CTRL_NAME_TR2 & rowCnt
            If rowCnt = 1 Then
                objTr2.Attributes("tabindex") = Integer.Parse(Me.CheckTorikesiTaisyou.Attributes("tabindex")) + intTrTabIndex
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop(" & DivLeftData.ClientID & ");"
            Else
                '2行目以降はタブ移動なし
                objTr2.Attributes("tabindex") = -1
            End If

            '行にセルとセット2
            With objTr2.Controls
                .Add(objTdSeikyuusyoHakDate)
                .Add(objTdSeikyuuSimeDate)
                .Add(objTdKaisyuuYoteiDate)

                .Add(objTdZenkaiGoseikyuuGaku)
                .Add(objTdGonyuukinGaku)
                .Add(objTdZenkaiKurikosiZandaka)
                .Add(objTdKonkaiGoseikyuuGaku)
                .Add(objTdKurikosiZandaka)

                .Add(objTdYuubinNo)
                .Add(objTdTelNo)
                .Add(objTdJuusyo1)
                .Add(objTdJuusyo2)
            End With

            'テーブルに行をセット
            TableDataTable1.Controls.Add(objTr1)
            TableDataTable2.Controls.Add(objTr2)

        Next



    End Sub

    ''' <summary>
    ''' 画面モード別の画面設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispControl(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Hiddenより再設定
        pStrGamenMode = Me.HiddenGamenMode.Value

        Dim end_count As Integer = 10 '表示最大件数
        Dim total_count As Integer = 0 '取得件数

        '未印刷データを検索
        Dim resultArray As New List(Of SeikyuuDataRecord)
        resultArray = SubLogic.GetSeikyuuMiinsatuData(sender, dtRec, 1, end_count, total_count)

        If pStrGamenMode = CStr(EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo) Then '請求書一覧画面
            'タイトル
            Me.SpanTitle.InnerHtml = CTRL_VALUE_TITLE
            'ウィンドウタイトルバー
            Me.Title = EarthConst.SYS_NAME & EarthConst.BRANK_STRING & CTRL_VALUE_TITLE
            '印刷
            Me.ButtonSeikyuusyoPrint.Value = EarthConst.BUTTON_SEIKYUUSYO_PRINT
            '画面別表示切替行
            Me.TrMihakkou.Style("display") = "inline"
            Me.TrSearchArea.Style("display") = "inline"
            Me.SpanInjiYousi.Style("display") = "none"

            '未印刷一覧(該当データが存在する場合のみボタン表示)
            If total_count = 0 Then
                Me.ButtonMiinsatu.Style("display") = "none"
            ElseIf total_count >= 1 Then
                Me.ButtonMiinsatu.Style("display") = "inline"
            ElseIf total_count = -1 Then
                ' 検索結果件数が-1の場合、エラーなので、処理終了
                Exit Sub
            End If

        ElseIf pStrGamenMode = CStr(EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo) Then '過去請求書一覧画面
            'タイトル
            Me.SpanTitle.InnerHtml = CTRL_VALUE_TITLE_KAKO
            'ウィンドウタイトルバー
            Me.Title = EarthConst.SYS_NAME & EarthConst.BRANK_STRING & CTRL_VALUE_TITLE_KAKO
            '再印刷
            Me.ButtonSeikyuusyoPrint.Value = EarthConst.BUTTON_SEIKYUUSYO_RE_PRINT
            '未印刷一覧
            Me.ButtonMiinsatu.Style("display") = "none"
            '画面別表示切替行
            Me.TrMihakkou.Style("display") = "none"
            Me.TdSelectSeikyuuSyousiki.ColSpan = "3"
            Me.KoumokumeiSearchMeisaiKensuu.Style("display") = "none"
            Me.TdTextMeisaiKensuu.Style("display") = "none"
            Me.SpanInjiYousi.Style("display") = "inline"
        Else
            Response.Redirect(UrlConst.SEIKYUUSYO_DATA_SAKUSEI)
            Exit Sub
        End If

        Me.TextSeikyuuSakiCd.Attributes("class") = "codeNumber"

    End Sub

    ''' <summary>
    ''' 検索実行時処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCmnSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '検索条件を設定
        Me.SetSearchKeyFromCtrl(keyRec)

        '検索結果を画面に表示
        Me.SetSearchResult(sender, e)

    End Sub

#Region "DB更新処理系"

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
    Public Function checkInput(ByVal prmActBtn As HtmlInputButton) As Boolean
        'エラーメッセージ初期化
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList

        Dim tmpCtrl As HtmlControl
        If prmActBtn.ID = Me.ButtonSeikyuusyoTorikesi.ID Then
            tmpCtrl = Me.ButtonSeikyuusyoTorikesi
        ElseIf prmActBtn.ID = Me.ButtonSeikyuusyoPrint.ID Then
            tmpCtrl = Me.ButtonSeikyuusyoPrint
        Else
            Return False
        End If

        '対象チェックボックスにてデータがセットされているかをチェック
        If Me.HiddenSendValSeikyuusyoNo.Value = String.Empty _
            Or Me.HiddenSendValUpdDatetime.Value = String.Empty Then
            errMess += Messages.MSG140E
            arrFocusTargetCtrl.Add(tmpCtrl)
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
    ''' <param name="emType">請求データの更新タイプ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function SaveData(ByVal emType As EarthEnum.emSeikyuusyoUpdType) As Boolean
        Dim listRec As New List(Of SeikyuuDataRecord)

        '画面からレコードクラスにセット
        listRec = Me.GetCtrlToDataRec()

        ' データの更新を行います
        If MyLogic.saveData(Me, listRec, emType) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 画面の各情報をレコードクラスに取得し、DB更新用レコードクラスを返却する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCtrlToDataRec() As List(Of SeikyuuDataRecord)
        Dim listRec As New List(Of SeikyuuDataRecord)
        Dim dtRec As New SeikyuuDataRecord
        Dim intCnt As Integer = 0
        Dim arrKeySeikyuusyoNo() As String = Nothing
        Dim arrKeyUpdDatetime() As String = Nothing

        If Me.HiddenSendValSeikyuusyoNo.Value <> String.Empty Then
            arrKeySeikyuusyoNo = Split(Me.HiddenSendValSeikyuusyoNo.Value, EarthConst.SEP_STRING)
        End If

        If Me.HiddenSendValUpdDatetime.Value <> String.Empty Then
            arrKeyUpdDatetime = Split(Me.HiddenSendValUpdDatetime.Value, EarthConst.SEP_STRING)
        End If

        '請求書NOと更新日時の個数が同一の場合
        If arrKeySeikyuusyoNo.Length = arrKeyUpdDatetime.Length Then

            If Not arrKeySeikyuusyoNo Is Nothing And Not arrKeyUpdDatetime Is Nothing Then

                For intCnt = 0 To arrKeySeikyuusyoNo.Length - 1
                    '請求書NOあるいは更新日時が空白の場合、次の処理へ
                    If arrKeySeikyuusyoNo(intCnt) = String.Empty _
                        OrElse arrKeyUpdDatetime(intCnt) = String.Empty Then
                        Continue For
                    End If

                    '***************************************
                    ' 請求データ
                    '***************************************
                    dtRec = New SeikyuuDataRecord

                    ' 請求書NO
                    cl.SetDisplayString(arrKeySeikyuusyoNo(intCnt), dtRec.SeikyuusyoNo)
                    ' 取消
                    dtRec.Torikesi = 1
                    ' 請求書印刷日
                    dtRec.SeikyuusyoInsatuDate = DateTime.Now
                    ' 更新者ユーザーID
                    dtRec.UpdLoginUserId = userinfo.LoginUserId
                    ' 更新日時 読み込み時のタイムスタンプ
                    If arrKeyUpdDatetime(intCnt) = "" Then
                        dtRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
                    Else
                        dtRec.UpdDatetime = DateTime.ParseExact(arrKeyUpdDatetime(intCnt), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                    End If

                    listRec.Add(dtRec)
                Next
            End If
        End If

        Return listRec
    End Function

#End Region

#End Region

End Class