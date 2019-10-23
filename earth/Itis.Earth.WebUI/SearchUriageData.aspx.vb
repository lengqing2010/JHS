
Partial Public Class SearchUriageData
    Inherits System.Web.UI.Page

    '画面表示の文字列変換用
    Private CLogic As CommonLogic = CommonLogic.Instance
    'メッセージクラス
    Private MLogic As New MessageLogic
    'ログインユーザレコード
    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    '売上データテーブルレコードクラス
    Private rec As New UriageDataKeyRecord

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
            Dim kbn_logic As New KubunLogic

            ' 区分コンボにデータをバインドする
            helper.SetDropDownList(selectKbn, DropDownHelper.DropDownType.Kubun)
            ' 請求先区分コンボにデータをバインドする
            helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuKbn, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)


            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            setDispAction()

            'フォーカス設定
            kubun_all.Focus()

        End If

    End Sub

#Region "ボタンイベント"
    ''' <summary>
    ''' 検索実行ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick
        '検索ボタンにフォーカス
        Me.btnSearch.Focus()

        '検索条件を設定
        SetSearchKeyFromCtrl(rec)

        '検索結果を画面に表示
        SetSearchResult(sender, e)

    End Sub

    ''' <summary>
    ''' 商品検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSyouhinSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyouhinSearch.ServerClick

        Dim lgcSyouhinSearch As New UriageDataSearchLogic
        Dim dataArray As New List(Of SyouhinMeisaiRecord)
        Dim total_count As Integer = 0
        Dim tmpScript As String = String.Empty
        Dim strSyouhinCd As String = String.Empty

        '画面からコードを取得
        strSyouhinCd = IIf(Me.TextSyouhinCd.Value <> "", Me.TextSyouhinCd.Value, String.Empty)

        If strSyouhinCd <> String.Empty Then
            '商品検索を実行
            dataArray = lgcSyouhinSearch.GetSyouhinInfo(strSyouhinCd, String.Empty, total_count)
        End If

        If dataArray.Count = 1 Then
            Dim recData As SyouhinMeisaiRecord = dataArray(0)
            Me.TextSyouhinCd.Value = recData.SyouhinCd
            Me.TextHinmei.Value = recData.SyouhinMei
            masterAjaxSM.SetFocus(Me.btnSyouhinSearch)
        Else
            Dim tmpFocusScript = "objEBI('" & btnSyouhinSearch.ClientID & "').focus();"
            '検索画面表示用JavaScript『callSearch』を実行
            tmpScript = "callSearch('" & Me.TextSyouhinCd.ClientID & EarthConst.SEP_STRING & Me.hdnSyouhinType.ClientID & "','" _
                                       & UrlConst.SEARCH_SYOUHIN & "','" _
                                       & Me.TextSyouhinCd.ClientID & "','" _
                                       & Me.btnSyouhinSearch.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
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
        blnResult = CLogic.CallSeikyuuSakiSearchWindow(sender, e, Me _
                                                        , Me.SelectSeikyuuKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc _
                                                        , Me.TextSeikyuuSakiMei, Me.btnSeikyuuSakiSearch)

        '赤色文字変更対応を配列に格納
        Dim objChgColor As Object() = New Object() {Me.SelectSeikyuuKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakiMei, Me.TextSeikyuuKameiTorikesiRiyuu}
        '取消理由取得設定と色替処理
        CLogic.GetKameitenTorikesiRiyuuMain(Me.SelectSeikyuuKbn.SelectedValue, Me.TextSeikyuuSakiCd.Value, TextSeikyuuKameiTorikesiRiyuu, True, False, objChgColor)

        If blnResult Then
            'フォーカスセット
            setFocusAJ(Me.btnSeikyuuSakiSearch)
        End If

    End Sub

    ''' <summary>
    ''' 加盟店検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnKameitenSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim blnResult As Boolean

        '加盟店検索画面呼出
        blnResult = CLogic.CallKameitenSearchWindow(sender _
                                                , e _
                                                , Me _
                                                , Me.selectKbn.ClientID _
                                                , Me.selectKbn.SelectedValue _
                                                , Me.TextKameitenCd _
                                                , Me.TextKameitenMei _
                                                , Me.btnKameitenSearch _
                                                , False _
                                                , Me.TextKameiTorikesiRiyuu _
                                                )

        If blnResult Then
            'フォーカスセット
            setFocusAJ(Me.btnKameitenSearch)
        End If

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

        Dim strFileNm As String = String.Empty  '出力ファイル名
        Dim dtCsv As DataTable
        Dim myLogic As New UriageDataSearchLogic

        '検索条件の設定
        Me.SetSearchKeyFromCtrl(rec)

        '件数
        Dim total_count As Integer = 0

        '検索実行
        dtCsv = myLogic.GetUriageDataCsv(sender, rec, total_count)

        ' 検索結果ゼロ件の場合、メッセージを表示
        If total_count = 0 Then
            ' 検索結果ゼロ件の場合、メッセージを表示
            MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
            Exit Sub
        ElseIf total_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV出力"), 0, "kensakuErr")
            Exit Sub
        End If

        '出力用データテーブルを基に、CSV出力を行なう
        If CLogic.OutPutFileFromDtTable(EarthConst.FILE_NAME_CSV_URIAGE_DATA, dtCsv) = False Then
            ' 出力用文字列がないので、処理終了
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV出力"), 0, "OutputErr")
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 請求年月日変更後処理起動ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonModalRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonModalRefresh.Click
        Dim tmpFocusScript = "exeSearch();"
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript, True)
    End Sub

#End Region

#Region "プライベートメソッド"
    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 検索実行ボタン関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '検索実行ボタンのイベントハンドラを設定
        btnSearch.Attributes("onclick") = "checkJikkou('0');"
        'CSV出力ボタンのイベントハンドラを設定
        Me.ButtonCsv.Attributes("onclick") = "checkJikkou('1');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 区分、全区分関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '区分プルダウン、全区分チェックボックスのイベントハンドラを設定
        selectKbn.Attributes("onchange") = "setKubunVal();"
        kubun_all.Attributes("onclick") = "setKubunVal();"
        '画面起動時デフォルトは「全区分」にチェック
        If IsPostBack = False Then
            kubun_all.Checked = True
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 商品マスタポップアップ画面の分類指定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.hdnSyouhinType.Value = EarthEnum.EnumSyouhinKubun.AllSyouhin

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '伝票番号イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextDenpyouBangouFrom.Attributes("onblur") = "this.value = paddingStr(this.value, 5, '0');"
        Me.TextDenpyouBangouTo.Attributes("onblur") = "this.value = paddingStr(this.value, 5, '0');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'マスタ検索系コード入力項目イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSyouhinCd.Attributes("onblur") = "clrName(this,'" & Me.TextHinmei.ClientID & "');"
        Me.TextSeikyuuSakiCd.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.TextSeikyuuSakiBrc.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.SelectSeikyuuKbn.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.TextKameitenCd.Attributes("onblur") = "clrKameitenInfo(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 加盟店取消理由
        CLogic.chgVeiwMode(Me.TextKameiTorikesiRiyuu, Nothing, True)
        ' 請求先取消理由
        CLogic.chgVeiwMode(Me.TextSeikyuuKameiTorikesiRiyuu, Nothing, True)

    End Sub

    ''' <summary>
    ''' 画面項目から検索キーレコードへの値セットを行う。
    ''' </summary>
    ''' <param name="recKey">抽出レコードクラスのキー</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromCtrl(ByRef recKey As UriageDataKeyRecord)
        '区分が条件に入るのは"全て以外"のとき
        If kubun_all.Checked = False Then
            ' 区分
            recKey.Kbn = IIf(selectKbn.SelectedValue <> "", selectKbn.SelectedValue, String.Empty)
        End If

        '番号 From
        recKey.BangouFrom = IIf(Me.TextBangouFrom.Value <> "", Me.TextBangouFrom.Value, String.Empty)
        '番号 To
        recKey.BangouTo = IIf(Me.TextBangouTo.Value <> "", Me.TextBangouTo.Value, String.Empty)
        '伝票番号 From
        recKey.DenNoFrom = IIf(Me.TextDenpyouBangouFrom.Value <> "", Me.TextDenpyouBangouFrom.Value, String.Empty)
        '伝票番号 To 
        recKey.DenNoTo = IIf(Me.TextDenpyouBangouTo.Value <> "", Me.TextDenpyouBangouTo.Value, String.Empty)
        '伝票作成日 From
        recKey.AddDatetimeFrom = IIf(Me.TextAddDateFrom.Value <> "", Me.TextAddDateFrom.Value, DateTime.MinValue)
        '伝票作成日 To
        recKey.AddDatetimeTo = IIf(Me.TextAddDateTo.Value <> "", Me.TextAddDateTo.Value, DateTime.MinValue)
        '商品コード
        recKey.SyouhinCd = IIf(Me.TextSyouhinCd.Value <> "", Me.TextSyouhinCd.Value, String.Empty)
        '請求先コード
        recKey.SeikyuuSakiCd = IIf(Me.TextSeikyuuSakiCd.Value <> "", Me.TextSeikyuuSakiCd.Value, String.Empty)
        '加盟店コード
        recKey.KameitenCd = IIf(Me.TextKameitenCd.Value <> "", Me.TextKameitenCd.Value, String.Empty)
        '請求先枝番
        recKey.SeikyuuSakiBrc = IIf(Me.TextSeikyuuSakiBrc.Value <> "", Me.TextSeikyuuSakiBrc.Value, String.Empty)
        '請求先区分
        recKey.SeikyuuSakiKbn = IIf(Me.SelectSeikyuuKbn.SelectedValue <> "", Me.SelectSeikyuuKbn.SelectedValue, String.Empty)
        '請求先カナ名
        recKey.SeikyuuSakiMeiKana = IIf(Me.TextSeikyuuSakiMeiKana.Value <> "", Me.TextSeikyuuSakiMeiKana.Value, String.Empty)
        '請求年月日 From
        recKey.SeikyuuDateFrom = IIf(Me.TextSeikyuuDateFrom.Value <> "", Me.TextSeikyuuDateFrom.Value, DateTime.MinValue)
        '請求年月日 To
        recKey.SeikyuuDateTo = IIf(Me.TextSeikyuuDateTo.Value <> "", Me.TextSeikyuuDateTo.Value, DateTime.MinValue)
        '売上年月日 From
        recKey.UriDateFrom = IIf(Me.TextUriageDateFrom.Value <> "", Me.TextUriageDateFrom.Value, DateTime.MinValue)
        '売上年月日 To
        recKey.UriDateTo = IIf(Me.TextUriageDateTo.Value <> "", Me.TextUriageDateTo.Value, DateTime.MinValue)
        '伝票売上年月日 From
        recKey.DenUriDateFrom = IIf(Me.TextDenpyouUriageDateFrom.Value <> "", Me.TextDenpyouUriageDateFrom.Value, DateTime.MinValue)
        '伝票売上年月日 To
        recKey.DenUriDateTo = IIf(Me.TextDenpyouUriageDateTo.Value <> "", Me.TextDenpyouUriageDateTo.Value, DateTime.MinValue)
        '最新伝票のみ表示
        recKey.NewDenpyouDisp = IIf(CheckSaisinDenpyou.Checked, CheckSaisinDenpyou.Value, Integer.MinValue)
        'マイナス伝票のみ表示
        recKey.MinusDenpyouDisp = IIf(CheckMinusDenpyou.Checked, CheckMinusDenpyou.Value, Integer.MinValue)
        '計上済み伝票のみ表示
        recKey.KeijyouZumiDisp = IIf(CheckKeijyouFlg.Checked, CheckKeijyouFlg.Value, Integer.MinValue)

    End Sub

    ''' <summary>
    ''' 検索条件で検索した内容を検索結果テーブルに表示する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Const CELL_COLOR As String = "red"
        Const CELL_BOLD As String = "bold"
        Const CSS_TEXT_CENTER = "textCenter"
        Const CSS_KINGAKU = "kingaku"
        Const CSS_NUMBER = "number"
        Const CSS_DATE = "date"

        '表示最大件数
        Dim end_count As Integer = maxSearchCount.Value
        Dim total_count As Integer = 0
        Dim intCsvMaxCnt As Integer = Integer.Parse(EarthConst.MAX_CSV_OUTPUT)

        'ロジッククラスの生成
        Dim logic As New UriageDataSearchLogic

        '検索実行
        Dim resultArray As List(Of UriageSearchResultRecord) = logic.GetUriageDataInfo(sender, rec, 1, end_count, total_count)

        ' 検索結果ゼロ件の場合、メッセージを表示
        If total_count = 0 Then
            ' 検索結果ゼロ件の場合、メッセージを表示
            MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
            Me.HiddenCsvOutPut.Value = ""
        ElseIf total_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            Exit Sub
        End If

        ' CSV出力上限以上の場合、確認メッセージを付与
        If total_count > intCsvMaxCnt Then
            Me.HiddenCsvMaxCnt.Value = "1"
        Else
            Me.HiddenCsvMaxCnt.Value = String.Empty
        End If

        '表示件数制限処理
        Dim displayCount As String = ""
        displayCount = total_count
        resultCount.Style.Remove("color")

        If Val(maxSearchCount.Value) < total_count Then
            resultCount.Style("color") = CELL_COLOR
            displayCount = maxSearchCount.Value & " / " & CLogic.GetDisplayString(total_count)
        End If


        '検索結果件数を設定
        resultCount.InnerHtml = displayCount

        '行カウンタ
        Dim rowCnt As Integer = 0

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


        '取得した売上データを画面に表示
        For Each data As UriageSearchResultRecord In resultArray

            rowCnt += 1

            Dim objTr1 As New HtmlTableRow
            Dim objTr2 As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdDenUnqNo As New HtmlTableCell              '伝票ユニークNO
            Dim objTdDenpyouType As New HtmlTableCell           '伝票種別
            Dim objTdDenpyouNo As New HtmlTableCell             '伝票番号
            Dim objTdSeikyuuSakiCd As New HtmlTableCell         '請求先コード
            Dim objTdSeikyuuSakiMei As New HtmlTableCell        '請求先名
            Dim objTdKbn As New HtmlTableCell                   '区分
            Dim objTdBangou As New HtmlTableCell                '番号
            Dim objTdSesyuMei As New HtmlTableCell              '施主名
            Dim objTdSyouhinCd As New HtmlTableCell             '商品コード
            Dim objTdHinmei As New HtmlTableCell                '品名
            Dim objTdUriGaku As New HtmlTableCell               '売上金額
            Dim objTdSuuRyou As New HtmlTableCell               '数量
            Dim objTdUriDate As New HtmlTableCell               '売上年月日
            Dim objTdDenUriDate As New HtmlTableCell            '伝票売上年月日
            Dim objTdDenUriDateLink As New HyperLink            '伝票売上年月日リンク
            Dim objTdUriKeijyouFlg As New HtmlTableCell         '売上処理(売上計上フラグ)
            Dim objTdSeikyuuDate As New HtmlTableCell           '請求年月日
            Dim objTdSeikyuuDateLink As New HyperLink           '請求年月日リンク
            Dim objTdKameitenCd As New HtmlTableCell            '加盟店コード
            Dim objTdKameitenMei As New HtmlTableCell           '加盟店名
            Dim strDenpyouType As String                        '伝票種別

            '検索結果配列からセルに格納
            objTdDenUnqNo.InnerHtml = CLogic.GetDisplayString(data.DenUnqNo, EarthConst.HANKAKU_SPACE)
            strDenpyouType = CLogic.GetDisplayString(data.DenSyubetu, EarthConst.HANKAKU_SPACE)
            objTdDenpyouType.InnerHtml = strDenpyouType
            objTdDenpyouNo.InnerHtml = CLogic.GetDisplayString(data.DenNo, EarthConst.HANKAKU_SPACE)
            objTdSeikyuuSakiCd.InnerHtml = CLogic.GetDispSeikyuuSakiCd(data.SeikyuuSakiKbn, data.SeikyuuSakiCd, data.SeikyuuSakiBrc, False)
            objTdSeikyuuSakiMei.InnerHtml = CLogic.GetDisplayString(data.SeikyuuSakiMei, EarthConst.HANKAKU_SPACE)
            objTdKbn.InnerHtml = CLogic.GetDisplayString(data.Kbn, EarthConst.HANKAKU_SPACE)
            objTdBangou.InnerHtml = CLogic.GetDisplayString(data.Bangou, EarthConst.HANKAKU_SPACE)
            objTdSesyuMei.InnerHtml = CLogic.GetDisplayString(data.SesyuMei, EarthConst.HANKAKU_SPACE)
            objTdSyouhinCd.InnerHtml = CLogic.GetDisplayString(data.SyouhinCd, EarthConst.HANKAKU_SPACE)
            objTdHinmei.InnerHtml = CLogic.GetDisplayString(data.Hinmei, EarthConst.HANKAKU_SPACE)
            objTdUriGaku.InnerHtml = CLogic.GetDisplayString(Format(data.UriGaku, EarthConst.FORMAT_KINGAKU_1), EarthConst.HANKAKU_SPACE)
            objTdSuuRyou.InnerHtml = CLogic.GetDisplayString(data.Suu, EarthConst.HANKAKU_SPACE)
            objTdUriDate.InnerHtml = CLogic.GetDisplayString(data.UriDate, EarthConst.HANKAKU_SPACE)

            objTdUriKeijyouFlg.InnerHtml = IIf(data.UriKeijyouFlg = 1, EarthConst.URIAGE_KEI_ZUMI, EarthConst.HANKAKU_SPACE)

            'マイナス伝票のみの設定
            If strDenpyouType = EarthConst.UR Then

                Dim strSeikyuuJs As String = String.Empty
                Dim strDenUriJs As String = String.Empty

                '伝票売上年月日
                objTdDenUriDateLink.Text = CLogic.GetDisplayString(data.DenUriDate, EarthConst.HANKAKU_SPACE)
                objTdDenUriDateLink.NavigateUrl = "javascript:void(0);"
                strDenUriJs = "openModalDenUriDate('" & UrlConst.POPUP_DENPYOU_URIAGE_DATE_HENKOU & "','" & CLogic.GetDisplayString(data.DenUnqNo) & "','" & CLogic.GetDisplayString(data.DenUriDate) & "')"
                objTdDenUriDateLink.Attributes.Add("onclick", strDenUriJs)
                objTdDenUriDate.Controls.Add(objTdDenUriDateLink)

                '請求年月日の値設定とリンクの設定
                objTdSeikyuuDateLink.Text = CLogic.GetDisplayString(data.SeikyuuDate, EarthConst.HANKAKU_SPACE)
                objTdSeikyuuDateLink.NavigateUrl = "javascript:void(0);"
                strSeikyuuJs &= "openModalSeikyuuDate('" & UrlConst.POPUP_SEIKYUU_DATE_HENKOU & "','" & CLogic.GetDisplayString(data.DenUnqNo) & "','" & CLogic.GetDisplayString(data.SeikyuuDate) & "')"
                objTdSeikyuuDateLink.Attributes.Add("onclick", strSeikyuuJs)
                objTdSeikyuuDate.Controls.Add(objTdSeikyuuDateLink)
            Else
                '伝票売上年月日(リンクなし)
                objTdDenUriDate.InnerHtml = CLogic.GetDisplayString(data.DenUriDate, EarthConst.HANKAKU_SPACE)
                '請求年月日(リンクなし)
                objTdSeikyuuDate.InnerHtml = CLogic.GetDisplayString(data.SeikyuuDate, EarthConst.HANKAKU_SPACE)
            End If

            objTdKameitenCd.InnerHtml = CLogic.GetDisplayString(data.KameitenCd, EarthConst.HANKAKU_SPACE)
            objTdKameitenMei.InnerHtml = CLogic.GetDisplayString(data.KameitenMei, EarthConst.HANKAKU_SPACE)

            '各セルの幅設定
            If rowCnt = 1 Then
                objTdDenUnqNo.Style("width") = widthList1(0)
                objTdDenpyouType.Style("width") = widthList1(1)
                objTdDenpyouNo.Style("width") = widthList1(2)
                objTdSeikyuuSakiCd.Style("width") = widthList1(3)
                objTdSeikyuuSakiMei.Style("width") = widthList1(4)
                objTdKbn.Style("width") = widthList1(5)
                objTdBangou.Style("width") = widthList1(6)
                objTdSesyuMei.Style("width") = widthList1(7)

                objTdSyouhinCd.Style("width") = widthList2(0)
                objTdHinmei.Style("width") = widthList2(1)
                objTdUriGaku.Style("width") = widthList2(2)
                objTdSuuRyou.Style("width") = widthList2(3)
                objTdUriDate.Style("width") = widthList2(4)
                objTdDenUriDate.Style("width") = widthList2(5)
                objTdUriKeijyouFlg.Style("width") = widthList2(6)
                objTdSeikyuuDate.Style("width") = widthList2(7)
                objTdKameitenCd.Style("width") = widthList2(8)
                objTdKameitenMei.Style("width") = widthList2(9)
            End If

            'スタイル、クラス設定
            objTdDenUnqNo.Attributes("class") = CSS_TEXT_CENTER
            objTdDenpyouType.Attributes("class") = CSS_TEXT_CENTER
            If strDenpyouType = EarthConst.UR Then
                objTdDenpyouType.Style("color") = CELL_COLOR
            End If
            objTdDenpyouNo.Attributes("class") = CSS_TEXT_CENTER
            objTdKbn.Attributes("class") = CSS_TEXT_CENTER
            objTdBangou.Attributes("class") = CSS_TEXT_CENTER
            objTdSyouhinCd.Attributes("class") = CSS_TEXT_CENTER
            objTdUriGaku.Attributes("class") = CSS_KINGAKU
            objTdSuuRyou.Attributes("class") = CSS_NUMBER
            objTdUriDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            '伝票売上年月日は赤伝・黒伝で比較を分ける
            If strDenpyouType = EarthConst.UR Then
                '赤伝の場合はリンクと比較
                If objTdUriDate.InnerText <> objTdDenUriDateLink.Text Then
                    objTdDenUriDateLink.Style("color") = CELL_COLOR
                    objTdDenUriDate.Style("font-weight") = CELL_BOLD
                End If
            Else
                '黒伝の場合はセル内の文字と比較
                If objTdUriDate.InnerText <> objTdDenUriDate.InnerText Then
                    objTdDenUriDate.Style("color") = CELL_COLOR
                    objTdDenUriDate.Style("font-weight") = CELL_BOLD
                End If
            End If
            objTdDenUriDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdUriKeijyouFlg.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuuSakiCd.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuuDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdKameitenCd.Attributes("class") = CSS_TEXT_CENTER

            objTr1.ID = "DataTable_resultTr1_" & rowCnt
            objTr1.Attributes("tabindex") = -1

            '行にセルをセット1
            With objTr1.Controls
                .Add(objTdDenUnqNo)
                .Add(objTdDenpyouType)
                .Add(objTdDenpyouNo)
                .Add(objTdSeikyuuSakiCd)
                .Add(objTdSeikyuuSakiMei)
                .Add(objTdKbn)
                .Add(objTdBangou)
                .Add(objTdSesyuMei)
            End With

            objTr2.ID = "DataTable_resultTr2_" & rowCnt
            If rowCnt = 1 Then
                objTr2.Attributes("tabindex") = Integer.Parse(CheckSaisinDenpyou.Attributes("tabindex")) + 10
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop(" & DivLeftData.ClientID & ");"
            Else
                '2行目以降はタブ移動なし
                objTr2.Attributes("tabindex") = -1
            End If

            '行にセルとセット2
            With objTr2.Controls
                .Add(objTdSyouhinCd)
                .Add(objTdHinmei)
                .Add(objTdUriGaku)
                .Add(objTdSuuRyou)
                .Add(objTdUriDate)
                .Add(objTdDenUriDate)
                .Add(objTdUriKeijyouFlg)
                .Add(objTdSeikyuuDate)
                .Add(objTdKameitenCd)
                .Add(objTdKameitenMei)
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
#End Region

End Class