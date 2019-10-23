Partial Public Class SearchFcMousikomi
    Inherits System.Web.UI.Page

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

    '共通ロジック
    Private cl As New CommonLogic
    Private cbLogic As New CommonBizLogic
    'メッセージクラス
    Private MLogic As New MessageLogic

    'FC申込データ検索ロジッククラス
    Dim MyLogic As New FcMousikomiSearchLogic
    'FC申込データ検索KEYレコードクラス
    Private keyRec As New FcMousikomiKeyRecord
    'FC申込データレコードクラス
    Private dtRec As New FcMousikomiRecord
    '地盤ロジック
    Dim jLogic As New JibanLogic

#Region "プロパティ"

#Region "パラメータ/受注画面・物件ダイレクト"

    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _Kubun As String = String.Empty
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrKbn() As String
        Get
            Return _Kubun
        End Get
        Set(ByVal value As String)
            _Kubun = value
        End Set
    End Property

    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _HosyousyoNo As String = String.Empty
    ''' <summary>
    ''' 保証書NO
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrHosyousyoNo() As String
        Get
            Return _HosyousyoNo
        End Get
        Set(ByVal value As String)
            _HosyousyoNo = value
        End Set
    End Property

#End Region

#Region "コントロール値"
    Private Const CTRL_NAME_TR1 As String = "DataTable_resultTr1_"
    Private Const CTRL_NAME_TR2 As String = "DataTable_resultTr2_"
    Private Const CTRL_NAME_TD_SENTOU As String = "DataTable_Sentou_Td_"
    Private Const CTRL_NAME_HIDDEN_MOUSIKOMI_NO As String = "HdnMousikomiNo_"
    Private Const CTRL_NAME_HIDDEN_UPDDATETIME As String = "HdnUpdDatetime_"
    Private Const CTRL_NAME_HIDDEN_JUTYUUZUMI_FLG As String = "HdnJutyuuZumiFlg_"
    Private Const CTRL_NAME_HIDDEN_DOUJI_IRAI_TOUSUU As String = "HdnDoujiIraiTousuu_"
    Private Const CTRL_NAME_HIDDEN_TYOUFUKU_FLG As String = "HdnTyoufukuFlg_"
    Private Const CTRL_NAME_CHECK_TAISYOU As String = "ChkTaisyou_"
#End Region

#Region "画面固有コントロール値"
    '受注状況 固定値
    Private Const BUKKEN_JYKY_MI_JUTYUU As String = "未受注"
    Private Const BUKKEN_JYKY_ZUMI_JUTYUU As String = "受注済"
    Private Const BUKKEN_JYKY_HORYUU_JUTYUU As String = "保留"
    '要注意情報 固定値
    Private Const YOU_TYUUI_ARI As String = "有"
    Private Const YOU_TYUUI_NASI As String = ""
#End Region

#Region "CSSクラス名"
    Private Const CSS_TEXT_CENTER = "textCenter"
    Private Const CSS_DATE = "date"
    Private Const CSS_NUMBER = "number"
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

        ' Key情報を保持
        Dim arrSearchTerm() As String

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If IsPostBack = False Then

            '●パラメータのチェック
            If Context.Items("sendSearchTerms") IsNot Nothing Then '物件履歴修正からの呼出
                arrSearchTerm = Split(Context.Items("sendSearchTerms"), EarthConst.SEP_STRING)
            Else
                '各業務画面からの呼出
                arrSearchTerm = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
                '物件ダイレクト画面からの呼出
                pStrKbn = Request("sendPage_kubun")
                pStrHosyousyoNo = Request("sendPage_hosyoushoNo")
            End If

            If arrSearchTerm.Length >= 2 Then
                pStrKbn = arrSearchTerm(0)              '親画面からPOSTされた情報1 ：区分
                pStrHosyousyoNo = arrSearchTerm(1)      '親画面からPOSTされた情報2 ：保証書NO
            End If

            '●権限のチェック
            '新規入力権限
            If userinfo.SinkiNyuuryokuKengen = 0 Then
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            Dim helper As New DropDownHelper
            Dim kbn_logic As New KubunLogic

            ' 区分コンボにデータをバインドする
            helper.SetDropDownList(SelectKbn, DropDownHelper.DropDownType.Kubun)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            setDispAction()

            'ボタン押下イベントの設定
            setBtnEvent()

            ' パラメータが引き渡されている場合、画面にセットする
            If pStrKbn IsNot Nothing OrElse pStrHosyousyoNo IsNot Nothing Then
                Me.SelectKbn.SelectedValue = pStrKbn
                Me.TextHosyousyoNoFrom.Value = pStrHosyousyoNo
                Me.TextHosyousyoNoTo.Value = pStrHosyousyoNo
                Me.SelectStatus.SelectedIndex = 2

                'フォーカス設定
                Me.btnSearch.Focus()
            Else
                'フォーカス設定
                Me.TextMousikomiNoFrom.Focus()
            End If

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
    ''' 加盟店検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKameitenSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKameitenSearch.ServerClick
        Dim blnResult As Boolean

        '加盟店検索画面呼出
        blnResult = cl.CallKameitenSearchWindow(sender _
                                                , e _
                                                , Me _
                                                , Me.SelectKbn.ClientID _
                                                , Me.SelectKbn.SelectedValue _
                                                , Me.TextKameitenCd _
                                                , Me.TextKameitenMei _
                                                , Me.ButtonKameitenSearch _
                                                , False _
                                                , Me.TextTorikesiRiyuu _
                                                )

        If blnResult Then
            'フォーカスセット
            setFocusAJ(Me.ButtonKameitenSearch)
        End If
    End Sub

    ''' <summary>
    ''' 調査会社検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tyousakaisyaSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTysKaisyaSearch.ServerClick
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        Dim tmpScript As String = String.Empty

        If Me.TextTysKaisyaCd.Value <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetTyousakaisyaSearchResult(TextTysKaisyaCd.Value, "", "", "", False)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            TextTysKaisyaCd.Value = recData.TysKaisyaCd + recData.JigyousyoCd
            TextTysKaisyaMei.Value = recData.TysKaisyaMei

            'フォーカスセット
            masterAjaxSM.SetFocus(ButtonTysKaisyaSearch)
        Else
            '検索画面表示用JavaScript『callSearch』を実行
            tmpScript = "callSearch('" & TextTysKaisyaCd.ClientID & "','" & UrlConst.SEARCH_TYOUSAKAISYA & "','" & _
                            TextTysKaisyaCd.ClientID & EarthConst.SEP_STRING & TextTysKaisyaMei.ClientID & "','" & ButtonTysKaisyaSearch.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If
    End Sub

    ''' <summary>
    ''' 新規受注ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSinkiJutyuu_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSinkiJutyuu.ServerClick
        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.ButtonSinkiJutyuu

        ' 入力チェック
        If Me.checkInput(objActBtn) = False Then Exit Sub

        ' 画面の内容をDBに反映する
        If Me.SaveData(EarthEnum.emSearchMousikomiBtnType.SinkiJutyuu) Then '登録成功

            tmpScript = "gNoDataMsgFlg = '1';"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonSinkiJutyuu_ServerClick", tmpScript, True)

            Me.SetCmnSearchResult(sender, e)
        Else '登録失敗
            Me.SetCmnSearchResult(sender, e)

            setFocusAJ(Me.btnSearch) 'フォーカス

            tmpScript = "alert('" & Messages.MSG147E.Replace("@PARAM1", "新規受注") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonSinkiJutyuu_ServerClick", tmpScript, True)
            Exit Sub
        End If
    End Sub

#End Region

#Region "プライベートメソッド"
    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ボタンのイベントハンドラを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '検索実行ボタン
        Me.btnSearch.Attributes("onclick") = "checkJikkou(0);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '新規受注ボタン設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '新規受注上限物件数セット(Web.configから取得)
        Me.HiddenSinkiJutyuuMax.Value = EarthConst.MAX_SINKI_JUTYUU

        'マスタ検索系コード入力項目イベントハンドラ設定　加盟店名削除
        Me.TextKameitenCd.Attributes("onblur") = "checkNumber(this);clrKameitenInfo(this);"
        'マスタ検索系コード入力項目イベントハンドラ設定　調査会社名削除
        Me.TextTysKaisyaCd.Attributes("onblur") = "checkNumber(this);clrName('" & TextTysKaisyaMei.ClientID & "');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)

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
        Me.ButtonSinkiJutyuu.Attributes("onclick") = tmpScript.Replace("@PARAM1", EarthEnum.emSearchMousikomiBtnType.SinkiJutyuu)

    End Sub

    ''' <summary>
    ''' 画面項目から検索キーレコードへの値セットを行う。
    ''' </summary>
    ''' <param name="recKey">抽出レコードクラスのキー</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromCtrl(ByRef recKey As FcMousikomiKeyRecord)

        '申込NO FROM
        keyRec.MousikomiNoFrom = IIf(Me.TextMousikomiNoFrom.Value <> String.Empty, Me.TextMousikomiNoFrom.Value, Long.MinValue)
        '申込NO TO
        keyRec.MousikomiNoTo = IIf(Me.TextMousikomiNoTo.Value <> String.Empty, Me.TextMousikomiNoTo.Value, Long.MinValue)
        '登録日時FROM
        keyRec.AddDatetimeFrom = IIf(Me.TextMousikomiDateFrom.Value <> String.Empty, Me.TextMousikomiDateFrom.Value, DateTime.MinValue)
        '登録日時TO
        keyRec.AddDatetimeTo = IIf(Me.TextMousikomiDateTo.Value <> String.Empty, Me.TextMousikomiDateTo.Value, DateTime.MinValue)
        '加盟店コード
        keyRec.KameitenCd = IIf(Me.TextKameitenCd.Value <> String.Empty, Me.TextKameitenCd.Value, String.Empty)
        '依頼日FROM
        keyRec.IraiDateFrom = IIf(Me.TextIraiDateFrom.Value <> String.Empty, Me.TextIraiDateFrom.Value, DateTime.MinValue)
        '依頼日TO
        keyRec.IraiDateTo = IIf(Me.TextIraiDateTo.Value <> String.Empty, Me.TextIraiDateTo.Value, DateTime.MinValue)
        '物件名称
        keyRec.SesyuMei = IIf(Me.TextBukkenMeisyou.Value <> String.Empty, Me.TextBukkenMeisyou.Value, String.Empty)
        '同時依頼棟数FROM
        keyRec.DoujiIraiTousuuFrom = IIf(Me.TextDoujiIraiTousuuFrom.Value <> String.Empty, Me.TextDoujiIraiTousuuFrom.Value, Integer.MinValue)
        '同時依頼棟数TO
        keyRec.DoujiIraiTousuuTo = IIf(Me.TextDoujiIraiTousuuTo.Value <> String.Empty, Me.TextDoujiIraiTousuuTo.Value, Integer.MinValue)
        '区分
        keyRec.Kbn = IIf(Me.SelectKbn.SelectedValue <> String.Empty, Me.SelectKbn.SelectedValue, String.Empty)
        '保証書NO FROM
        keyRec.HosyousyoNoFrom = IIf(Me.TextHosyousyoNoFrom.Value <> String.Empty, Me.TextHosyousyoNoFrom.Value, String.Empty)
        '保証書NO TO
        keyRec.HosyousyoNoTo = IIf(Me.TextHosyousyoNoTo.Value <> String.Empty, Me.TextHosyousyoNoTo.Value, String.Empty)
        'ステータス
        keyRec.Status = IIf(Me.SelectStatus.Value <> String.Empty, Me.SelectStatus.Value, String.Empty)
        '調査会社コード・調査会社事業所コード
        keyRec.TysKaisyaCd = IIf(Me.TextTysKaisyaCd.Value <> "", Me.TextTysKaisyaCd.Value, String.Empty)
        '要注意有無
        keyRec.YouTyuuiUmuSearchTaisyou = IIf(Me.CheckYouTyuuiUmu.Checked, 0, Integer.MinValue)

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

    ''' <summary>
    ''' 検索条件で検索した内容を検索結果テーブルに表示する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '表示最大件数
        Dim end_count As Integer = IIf(maxSearchCount.Value <> "max", maxSearchCount.Value, EarthConst.MAX_RESULT_COUNT)
        Dim total_count As Integer = 0 '取得件数

        '検索実行
        Dim resultArray As New List(Of FcMousikomiRecord)

        '対象チェックボックスの初期化
        Me.CheckAll.Checked = False

        resultArray = MyLogic.GetMousikomiDataInfo(sender, keyRec, 1, end_count, total_count)

        ' 検索結果ゼロ件の場合、メッセージを表示
        If total_count = 0 Then
            ' 検索結果ゼロ件の場合、メッセージを表示
            Dim tmpScript As String = "if(gNoDataMsgFlg != '1'){alert('" & Messages.MSG020E & "')}gNoDataMsgFlg = null;"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "kensakuZero", tmpScript, True)
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
        'コンボ設定ヘルパークラスを生成
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        'TabIndex
        Dim intTrTabIndex As Integer = 5
        'Tr
        Dim objTr1 As HtmlTableRow
        Dim objTr2 As HtmlTableRow

        Dim strMousikomiNo As String                    '申込NO

        'Hidden 　
        Dim objHdnMousikomiNo As HtmlInputHidden        '申込NO
        Dim objHdnUpdDatetime As HtmlInputHidden        '更新日時
        Dim objHdnJutyuuZumi As HtmlInputHidden         '受注済判定用
        Dim objHdnDoujiIraiTousuu As HtmlInputHidden    '同時依頼棟数
        Dim objHdnTyoufuku As HtmlInputHidden           '重複判定用
        'CheckBox 
        Dim objChkTaisyou As HtmlInputCheckBox          '対象

        'Table Cell
        Dim objTdTaisyou As HtmlTableCell               '対象
        Dim objTdYoutyuui As HtmlTableCell              '要注意
        Dim objTdJutyuuJyky As HtmlTableCell            '受注状況
        Dim objTdKbn As HtmlTableCell                   '区分
        Dim objTdHosyousyoNo As HtmlTableCell           '保証書NO
        Dim objTdSesyuMei As HtmlTableCell              '施主名

        Dim objTdTantouTysKaisyaCd As HtmlTableCell     '担_調コード
        Dim objTdTantouTysKaisyaMei As HtmlTableCell    '担_調名
        Dim objTdMousikomiTysKaisyaCd As HtmlTableCell  '申_調コード
        Dim objTdMousikomiTysKaisyaMei As HtmlTableCell '申_調名
        Dim objTdJyuusyo1 As HtmlTableCell              '物件住所1
        Dim objTdJyuusyo2 As HtmlTableCell              '物件住所2
        Dim objTdKameitenCd As HtmlTableCell            '加盟店コード
        Dim objTdKameitenMei As HtmlTableCell           '加盟店名
        Dim objTdIraiDate As HtmlTableCell              '依頼日
        Dim objTdSyouhinCd As HtmlTableCell             '商品コード
        Dim objTdSyouhinMei As HtmlTableCell            '商品名
        Dim objTdDoujiIraiTousuu As HtmlTableCell       '同時依頼棟数
        Dim objTdSesyuMeiUmu As HtmlTableCell           '施主名有無
        Dim objTdBukkennNayose As New HtmlTableCell     '物件名寄コード

        '取得したデータを画面に表示
        For Each data As FcMousikomiRecord In resultArray
            rowCnt += 1

            'Tr
            objTr1 = New HtmlTableRow
            objTr2 = New HtmlTableRow

            strMousikomiNo = String.Empty

            '***********
            '* Table1
            '********
            'Hidden 
            objHdnMousikomiNo = New HtmlInputHidden         'Hidden申込NO
            objHdnUpdDatetime = New HtmlInputHidden         'Hidden更新日時
            objHdnJutyuuZumi = New HtmlInputHidden          'Hidden受注済判定用
            objHdnDoujiIraiTousuu = New HtmlInputHidden     'Hidden同時依頼棟数
            objHdnTyoufuku = New HtmlInputHidden            'Hidden重複判定用
            'CheckBox 
            objChkTaisyou = New HtmlInputCheckBox           '対象チェックボックス

            'Table Cell
            objTdTaisyou = New HtmlTableCell                '対象
            objTdYoutyuui = New HtmlTableCell               '要注意
            objTdJutyuuJyky = New HtmlTableCell             '受注状況
            objTdKbn = New HtmlTableCell                    '区分
            objTdHosyousyoNo = New HtmlTableCell            '保証書NO
            objTdSesyuMei = New HtmlTableCell               '施主名

            '***********
            '* Table2
            '********
            objTdTantouTysKaisyaCd = New HtmlTableCell      '担_調コード
            objTdTantouTysKaisyaMei = New HtmlTableCell     '担_調名
            objTdMousikomiTysKaisyaCd = New HtmlTableCell   '申_調コード
            objTdMousikomiTysKaisyaMei = New HtmlTableCell  '申_調名
            objTdJyuusyo1 = New HtmlTableCell               '物件住所1
            objTdJyuusyo2 = New HtmlTableCell               '物件住所2
            objTdKameitenCd = New HtmlTableCell             '加盟店コード
            objTdKameitenMei = New HtmlTableCell            '加盟店名
            objTdIraiDate = New HtmlTableCell               '依頼日
            objTdSyouhinCd = New HtmlTableCell              '商品コード
            objTdSyouhinMei = New HtmlTableCell             '商品名
            objTdDoujiIraiTousuu = New HtmlTableCell        '同時依頼棟数
            objTdSesyuMeiUmu = New HtmlTableCell            '施主名有無
            objTdBukkennNayose = New HtmlTableCell          '物件名寄コード

            '検索結果配列からセルに格納
            objTdTaisyou.ID = CTRL_NAME_TD_SENTOU & rowCnt

            '申込NOを取得
            strMousikomiNo = cl.GetDisplayString(data.MousikomiNo)

            '******************
            '* 先頭列
            '*************
            'Hidden申込NO
            objHdnMousikomiNo.ID = CTRL_NAME_HIDDEN_MOUSIKOMI_NO & rowCnt
            objHdnMousikomiNo.Value = strMousikomiNo
            objTdTaisyou.Controls.Add(objHdnMousikomiNo)

            'Hidden更新日時
            objHdnUpdDatetime.ID = CTRL_NAME_HIDDEN_UPDDATETIME & rowCnt
            objHdnUpdDatetime.Value = IIf(data.UpdDatetime = DateTime.MinValue, Format(data.AddDatetime, EarthConst.FORMAT_DATE_TIME_1), Format(data.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
            objTdTaisyou.Controls.Add(objHdnUpdDatetime)

            'Hidden受注済判定用
            objHdnJutyuuZumi.ID = CTRL_NAME_HIDDEN_JUTYUUZUMI_FLG & rowCnt
            objHdnJutyuuZumi.Value = cl.GetDisplayString(data.Status, EarthConst.ISNULL)
            objTdTaisyou.Controls.Add(objHdnJutyuuZumi)

            'Hidden同時依頼棟数
            objHdnDoujiIraiTousuu.ID = CTRL_NAME_HIDDEN_DOUJI_IRAI_TOUSUU & rowCnt
            objHdnDoujiIraiTousuu.Value = cl.GetDisplayString(data.DoujiIraiTousuu, EarthConst.ISNULL)
            objTdTaisyou.Controls.Add(objHdnDoujiIraiTousuu)

            'Hidden重複判定用
            objHdnTyoufuku.ID = CTRL_NAME_HIDDEN_TYOUFUKU_FLG & rowCnt
            objHdnTyoufuku.Value = IIf(Me.CheckTyoufuku(Me, data.Kbn, data.SesyuMei, data.BukkenJyuusyo1, data.BukkenJyuusyo2), _
                                        EarthConst.TYOUFUKU_NASI, EarthConst.TYOUFUKU_ARI)
            objTdTaisyou.Controls.Add(objHdnTyoufuku)

            '対象チェックボックス(戻り要素(hidden)は、必ず先頭列の先頭のInputタグにセットすること)
            objChkTaisyou.ID = CTRL_NAME_CHECK_TAISYOU & strMousikomiNo
            objChkTaisyou.Attributes("tabindex") = Integer.Parse(Me.SelectStatus.Attributes("tabindex")) + intTrTabIndex + rowCnt
            objTdTaisyou.Controls.Add(objChkTaisyou)

            '******************

            If data.YouTyuuiJouhou Is Nothing Or data.YouTyuuiJouhou = "" Then
                objTdYoutyuui.InnerHtml = cl.GetDisplayString(YOU_TYUUI_NASI, strSpace)
            Else
                objTdYoutyuui.InnerHtml = cl.GetDisplayString(YOU_TYUUI_ARI, strSpace)
            End If

            If data.Status = EarthConst.MOUSIKOMI_STATUS_MI_JUTYUU Then
                objTdJutyuuJyky.InnerHtml = cl.GetDisplayString(BUKKEN_JYKY_MI_JUTYUU, strSpace)
            ElseIf data.Status = EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU Then
                objTdJutyuuJyky.InnerHtml = cl.GetDisplayString(BUKKEN_JYKY_ZUMI_JUTYUU, strSpace)
            ElseIf data.Status = EarthConst.MOUSIKOMI_STATUS_HORYUU_JUTYUU Then
                objTdJutyuuJyky.InnerHtml = cl.GetDisplayString(BUKKEN_JYKY_HORYUU_JUTYUU, strSpace)
            End If

            objTdKbn.InnerHtml = cl.GetDisplayString(data.Kbn, strSpace)
            objTdHosyousyoNo.InnerHtml = cl.GetDisplayString(data.HosyousyoNo, strSpace)
            objTdSesyuMei.InnerHtml = cl.GetDisplayString(data.SesyuMei, strSpace)

            '担_調
            objTdTantouTysKaisyaCd.InnerHtml = cl.GetDisplayString(data.TantouTysKaisyaCd & data.TantouTysKaisyaJigyousyoCd, strSpace)
            objTdTantouTysKaisyaMei.InnerHtml = cl.GetDisplayString(data.TantouTysKaisyaMei, strSpace)
            '申_調
            objTdMousikomiTysKaisyaCd.InnerHtml = cl.GetDisplayString(data.MousikomiTysKaisyaCd & data.MousikomiTysKaisyaJigyousyoCd, strSpace)
            objTdMousikomiTysKaisyaMei.InnerHtml = cl.GetDisplayString(data.MousikomiTysKaisyaMei, strSpace)

            objTdJyuusyo1.InnerHtml = cl.GetDisplayString(data.BukkenJyuusyo1, strSpace)
            objTdJyuusyo2.InnerHtml = cl.GetDisplayString(data.BukkenJyuusyo2, strSpace)
            objTdKameitenCd.InnerHtml = cl.GetDisplayString(data.KameitenCd, strSpace)
            objTdKameitenMei.InnerHtml = cl.GetDisplayString(data.KameitenMei, strSpace)
            objTdIraiDate.InnerHtml = cbLogic.GetDispStrDateTime(data.IraiDate, strSpace)
            objTdSyouhinCd.InnerHtml = cl.GetDisplayString(data.SyouhinCd, strSpace)
            objTdSyouhinMei.InnerHtml = cl.GetDisplayString(data.SyouhinNm, strSpace)
            objTdDoujiIraiTousuu.InnerHtml = cl.GetDisplayString(data.DoujiIraiTousuu, strSpace)
            objTdSesyuMeiUmu.InnerHtml = cl.GetDisplayString(cl.GetDispUmuStr(data.SesyuMeiUmu), strSpace)
            objTdBukkennNayose.InnerHtml = cl.GetDisplayString(data.BukkenNayoseCd, strSpace)

            '各セルの幅設定
            If rowCnt = 1 Then
                objTdTaisyou.Style("width") = widthList1(0)
                objTdYoutyuui.Style("width") = widthList1(1)
                objTdJutyuuJyky.Style("width") = widthList1(2)
                objTdKbn.Style("width") = widthList1(3)
                objTdHosyousyoNo.Style("width") = widthList1(4)
                objTdSesyuMei.Style("width") = widthList1(5)

                objTdTantouTysKaisyaCd.Style("width") = widthList2(0)
                objTdTantouTysKaisyaMei.Style("width") = widthList2(1)
                objTdMousikomiTysKaisyaCd.Style("width") = widthList2(2)
                objTdMousikomiTysKaisyaMei.Style("width") = widthList2(3)
                objTdJyuusyo1.Style("width") = widthList2(4)
                objTdJyuusyo2.Style("width") = widthList2(5)
                objTdKameitenCd.Style("width") = widthList2(6)
                objTdKameitenMei.Style("width") = widthList2(7)
                objTdIraiDate.Style("width") = widthList2(8)
                objTdSyouhinCd.Style("width") = widthList2(9)
                objTdSyouhinMei.Style("width") = widthList2(10)
                objTdDoujiIraiTousuu.Style("width") = widthList2(11)
                objTdSesyuMeiUmu.Style("width") = widthList2(12)
                objTdBukkennNayose.Style("width") = widthList2(13)
            End If

            'スタイル、クラス設定
            objTdTaisyou.Attributes("class") = CSS_TEXT_CENTER
            objTdYoutyuui.Attributes("class") = CSS_TEXT_CENTER
            objTdJutyuuJyky.Attributes("class") = CSS_TEXT_CENTER
            objTdKbn.Attributes("class") = CSS_TEXT_CENTER
            objTdHosyousyoNo.Attributes("class") = CSS_TEXT_CENTER
            objTdSesyuMei.Attributes("class") = ""

            objTdTantouTysKaisyaCd.Attributes("class") = CSS_TEXT_CENTER
            objTdTantouTysKaisyaMei.Attributes("class") = ""
            objTdMousikomiTysKaisyaCd.Attributes("class") = CSS_TEXT_CENTER
            objTdMousikomiTysKaisyaMei.Attributes("class") = ""
            objTdJyuusyo1.Attributes("class") = ""
            objTdJyuusyo2.Attributes("class") = ""
            objTdKameitenCd.Attributes("class") = CSS_TEXT_CENTER
            objTdKameitenMei.Attributes("class") = ""
            objTdIraiDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdSyouhinCd.Attributes("class") = CSS_TEXT_CENTER
            objTdSyouhinMei.Attributes("class") = ""
            objTdDoujiIraiTousuu.Attributes("class") = CSS_NUMBER
            objTdSesyuMeiUmu.Attributes("class") = CSS_TEXT_CENTER
            objTdBukkennNayose.Attributes("class") = CSS_TEXT_CENTER

            '行IDとJSイベントの付与
            objTr1.ID = CTRL_NAME_TR1 & rowCnt
            objTr1.Attributes("tabindex") = -1

            '行にセルをセット1
            With objTr1.Controls
                .Add(objTdTaisyou) '戻り要素(hidden)は、必ず先頭列の先頭のInputタグにセットすること
                .Add(objTdYoutyuui)
                .Add(objTdJutyuuJyky)
                .Add(objTdKbn)
                .Add(objTdHosyousyoNo)
                .Add(objTdSesyuMei)
            End With

            '行IDとJSイベントの付与
            objTr2.ID = CTRL_NAME_TR2 & rowCnt
            If rowCnt = 1 Then
                objTr2.Attributes("tabindex") = Integer.Parse(Me.SelectStatus.Attributes("tabindex")) + intTrTabIndex
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop(" & DivLeftData.ClientID & ");"
            Else
                '2行目以降はタブ移動なし
                objTr2.Attributes("tabindex") = -1
            End If

            '行にセルとセット2
            With objTr2.Controls
                .Add(objTdTantouTysKaisyaCd)
                .Add(objTdTantouTysKaisyaMei)
                .Add(objTdMousikomiTysKaisyaCd)
                .Add(objTdMousikomiTysKaisyaMei)
                .Add(objTdJyuusyo1)
                .Add(objTdJyuusyo2)
                .Add(objTdKameitenCd)
                .Add(objTdKameitenMei)
                .Add(objTdIraiDate)
                .Add(objTdSyouhinCd)
                .Add(objTdSyouhinMei)
                .Add(objTdDoujiIraiTousuu)
                .Add(objTdSesyuMeiUmu)
                .Add(objTdBukkennNayose)
            End With

            'テーブルに行をセット
            TableDataTable1.Controls.Add(objTr1)
            TableDataTable2.Controls.Add(objTr2)

        Next

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
    ''' 重複チェック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Function CheckTyoufuku(ByVal sender As System.Object, _
                                     ByVal kbn As String, _
                                     ByVal Sesyumei As String, _
                                     ByVal BukkenJyuusyo1 As String, _
                                     ByVal BukkenJyuusyo2 As String _
                                     ) As Boolean

        Dim bolResult1 As Boolean = True
        Dim bolResult2 As Boolean = True

        '施主名が空白以外の場合、施主名の重複チェックを実施
        If Sesyumei <> String.Empty Then
            If jLogic.ChkTyouhuku(kbn, _
                                 String.Empty, _
                                 Sesyumei) = True Then
                bolResult1 = False
            End If
        End If

        '物件住所が空白以外の場合、物件住所の重複チェックを実施
        If BukkenJyuusyo1 <> String.Empty Or BukkenJyuusyo2 <> String.Empty Then
            If jLogic.ChkTyouhuku(kbn, _
                                 String.Empty, _
                                 BukkenJyuusyo1, _
                                 BukkenJyuusyo2) = True Then
                bolResult2 = False
            End If
        End If

        If bolResult1 = False OrElse bolResult2 = False Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

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
        If prmActBtn.ID = Me.ButtonSinkiJutyuu.ID Then
            tmpCtrl = Me.ButtonSinkiJutyuu
        Else
            Return False
        End If

        '対象チェックボックスにてデータがセットされているかをチェック
        If Me.HiddenSendValMousikomiNo.Value = String.Empty _
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
    Protected Function SaveData(ByVal emType As EarthEnum.emSearchMousikomiBtnType) As Boolean
        Dim listRec As New List(Of FcMousikomiRecord)

        '画面からレコードクラスにセット
        listRec = Me.GetCtrlToDataList()

        ' データの更新を行います
        If emType = EarthEnum.emSearchMousikomiBtnType.SinkiJutyuu Then
            If MyLogic.saveDataJutyuu(Me, listRec, EarthEnum.emMousikomiSinkiJutyuuType.SearchMousikomi) = False Then
                Return False
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' 画面の各情報をレコードクラスに取得し、DB更新用レコードクラスを返却する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCtrlToDataList() As List(Of FcMousikomiRecord)
        Dim listRec As New List(Of FcMousikomiRecord)
        Dim dtRec As New FcMousikomiRecord
        Dim intCnt As Integer = 0
        Dim arrKeyMousikomiNo() As String = Nothing
        Dim arrKeyUpdDatetime() As String = Nothing

        If Me.HiddenSendValMousikomiNo.Value <> String.Empty Then
            arrKeyMousikomiNo = Split(Me.HiddenSendValMousikomiNo.Value, EarthConst.SEP_STRING)
        End If

        If Me.HiddenSendValUpdDatetime.Value <> String.Empty Then
            arrKeyUpdDatetime = Split(Me.HiddenSendValUpdDatetime.Value, EarthConst.SEP_STRING)
        End If

        '申込NOと更新日時の個数が同一の場合
        If arrKeyMousikomiNo.Length = arrKeyUpdDatetime.Length Then
            If Not arrKeyMousikomiNo Is Nothing And Not arrKeyUpdDatetime Is Nothing Then

                For intCnt = 0 To arrKeyMousikomiNo.Length - 1
                    '請求書NOあるいは更新日時が空白の場合、次の処理へ
                    If arrKeyMousikomiNo(intCnt) = String.Empty _
                        OrElse arrKeyUpdDatetime(intCnt) = String.Empty Then
                        Continue For
                    End If

                    '***************************************
                    ' FC申込データ
                    '***************************************
                    dtRec = New FcMousikomiRecord

                    ' 申込NO
                    cl.SetDisplayString(arrKeyMousikomiNo(intCnt), dtRec.MousikomiNo)
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

End Class