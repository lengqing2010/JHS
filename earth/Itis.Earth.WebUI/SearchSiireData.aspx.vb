
Partial Public Class SearchSiireData
    Inherits System.Web.UI.Page

    '共通ロジック
    Private CLogic As CommonLogic = CommonLogic.Instance
    'メッセージクラス
    Private MLogic As New MessageLogic
    'ログインユーザレコード
    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

    '売上データテーブルレコードクラス
    Private rec As New SiireDataKeyRecord

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
            ' 区分コンボにデータをバインドする
            Dim helper As New DropDownHelper
            Dim kbn_logic As New KubunLogic
            helper.SetDropDownList(selectKbn, DropDownHelper.DropDownType.Kubun)


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
    ''' 検索実行ボタン押下時処理
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

        '商品検索を実行
        If Not String.IsNullOrEmpty(strSyouhinCd) Then
            dataArray = lgcSyouhinSearch.GetSyouhinInfo(strSyouhinCd, String.Empty, total_count)
        End If

        If dataArray.Count = 1 Then
            Dim recData As SyouhinMeisaiRecord = dataArray(0)
            Me.TextSyouhinCd.Value = recData.SyouhinCd
            Me.TextHinmei.Value = recData.SyouhinMei
            'フォーカスセット
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
    ''' 仕入先検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSiireSakiSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSiireSakiSearch.ServerClick
        Dim lgcSiireSearch As New TyousakaisyaSearchLogic
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)
        Dim total_count As Integer = 0
        Dim tmpScript As String = String.Empty
        Dim strSiireCd As String = String.Empty
        Dim strSiireBrc As String = String.Empty

        '画面からコードを取得(ポップアップ戻り値用)
        If Me.HiddenSiireSakiCdNew.Value = String.Empty Then
            strSiireCd = IIf(Me.TextSiireSakiCd.Value <> "", Me.TextSiireSakiCd.Value, String.Empty)
            strSiireBrc = IIf(Me.TextSiireSakiBrc.Value <> "", Me.TextSiireSakiBrc.Value, String.Empty)
            Me.HiddenSiireSakiCdNew.Value = strSiireCd & strSiireBrc
        End If

        '仕入先検索を実行
        If Me.HiddenSiireSakiCdNew.Value <> String.Empty Then
            dataArray = lgcSiireSearch.GetTyousakaisyaSearchResult(Me.HiddenSiireSakiCdNew.Value, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        True, _
                                                                        Me.HiddenKameitenCd.Value, _
                                                                        CInt(Me.HiddenTysKensakuType.Value))
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            Me.TextSiireSakiCd.Value = recData.TysKaisyaCd
            Me.TextSiireSakiBrc.Value = recData.JigyousyoCd
            Me.TextSiireSakiMei.Value = recData.TysKaisyaMei

            'フォーカスセット
            masterAjaxSM.SetFocus(Me.btnSiireSakiSearch)

            Me.HiddenSiireSakiCdNew.Value = String.Empty
        Else
            '調査会社名をクリア
            Me.TextSiireSakiMei.Value = String.Empty

            Dim tmpFocusScript = "objEBI('" & btnSiireSakiSearch.ClientID & "').focus();"
            '検索画面表示用JavaScript『callSearch』を実行
            tmpScript = "callSearch('" & Me.HiddenSiireSakiCdNew.ClientID & EarthConst.SEP_STRING & _
                                         Me.HiddenKameitenCd.ClientID & EarthConst.SEP_STRING & _
                                         Me.HiddenTysKensakuType.ClientID & "','" _
                                       & UrlConst.SEARCH_TYOUSAKAISYA & "','" _
                                       & Me.HiddenSiireSakiCdNew.ClientID & EarthConst.SEP_STRING & _
                                         Me.HiddenKakushuNG.ClientID & "','" _
                                       & Me.btnSiireSakiSearch.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

        '画面からコードを取得(検索結果退避用)
        If Me.HiddenSiireSakiCdNew.Value <> String.Empty Then
            strSiireCd = IIf(Me.TextSiireSakiCd.Value <> "", Me.TextSiireSakiCd.Value, String.Empty)
            strSiireBrc = IIf(Me.TextSiireSakiBrc.Value <> "", Me.TextSiireSakiBrc.Value, String.Empty)
            Me.HiddenSiireSakiCdNew.Value = strSiireCd & strSiireBrc
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
        Dim myLogic As New SiireDataSearchLogic

        '検索条件の設定
        SetSearchKeyFromCtrl(rec)

        '件数
        Dim total_count As Integer = 0

        '検索実行
        dtCsv = myLogic.GetSiireDataCsv(sender, rec, total_count)

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
        If CLogic.OutPutFileFromDtTable(EarthConst.FILE_NAME_CSV_SIIRE_DATA, dtCsv) = False Then
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
        Me.TextSiireSakiCd.Attributes("onblur") = "clrName(this,'" & Me.TextSiireSakiMei.ClientID & "');"
        Me.TextSiireSakiBrc.Attributes("onblur") = "clrName(this,'" & Me.TextSiireSakiMei.ClientID & "');"

        '調査会社検索画面呼び出し用の検索タイプをセット
        Me.HiddenTysKensakuType.Value = CStr(EarthEnum.EnumTyousakaisyaKensakuType.SIIRESAKI)

    End Sub

    ''' <summary>
    ''' 画面項目から検索キーレコードへの値セットを行う。
    ''' </summary>
    ''' <param name="recKey">抽出レコードクラスのキー</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromCtrl(ByRef recKey As SiireDataKeyRecord)
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
        '伝票作成日(登録年月日) From
        recKey.AddDatetimeFrom = IIf(Me.TextAddDateFrom.Value <> "", Me.TextAddDateFrom.Value, DateTime.MinValue)
        '伝票作成日(登録年月日) To
        recKey.AddDatetimeTo = IIf(Me.TextAddDateTo.Value <> "", Me.TextAddDateTo.Value, DateTime.MinValue)
        '商品コード
        recKey.SyouhinCd = IIf(Me.TextSyouhinCd.Value <> "", Me.TextSyouhinCd.Value, String.Empty)
        '仕入先コード
        recKey.SiireSakiCd = IIf(Me.TextSiireSakiCd.Value <> "", Me.TextSiireSakiCd.Value, String.Empty)
        '仕入先枝番
        recKey.SiireSakiBrc = IIf(Me.TextSiireSakiBrc.Value <> "", Me.TextSiireSakiBrc.Value, String.Empty)
        '仕入先名カナ
        recKey.SiireSakiMeiKana = IIf(Me.TextSiireSakiMeiKana.Value <> "", Me.TextSiireSakiMeiKana.Value, String.Empty)
        '仕入年月日 From
        recKey.SiireDateFrom = IIf(Me.TextSiireDateFrom.Value <> "", Me.TextSiireDateFrom.Value, DateTime.MinValue)
        '仕入年月日 To
        recKey.SiireDateTo = IIf(Me.TextSiireDateTo.Value <> "", Me.TextSiireDateTo.Value, DateTime.MinValue)
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
        '表示最大件数
        Dim end_count As Integer = maxSearchCount.Value
        Dim total_count As Integer = 0
        Dim intCsvMaxCnt As Integer = Integer.Parse(EarthConst.MAX_CSV_OUTPUT)

        'ロジッククラスの生成
        Dim logic As New SiireDataSearchLogic

        '検索実行
        Dim resultArray As List(Of SiireSearchResultRecord) = logic.GetSiireDataInfo(sender, rec, 1, end_count, total_count)

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
            resultCount.Style("color") = "red"
            displayCount = maxSearchCount.Value & " / " & CommonLogic.Instance.GetDisplayString(total_count)
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
        For Each data As SiireSearchResultRecord In resultArray

            rowCnt += 1

            Dim objTr1 As New HtmlTableRow
            Dim objTr2 As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdDenUnqNo As New HtmlTableCell              '伝票ユニークNO
            Dim objTdDenpyouNo As New HtmlTableCell             '伝票番号
            Dim objTdSiireSakiCd As New HtmlTableCell           '仕入先コード
            Dim objTdSiireSakiMei As New HtmlTableCell          '仕入先名
            Dim objTdKbn As New HtmlTableCell                   '区分
            Dim objTdBangou As New HtmlTableCell                '番号
            Dim objTdSesyuMei As New HtmlTableCell              '施主名
            Dim objTdSyouhinCd As New HtmlTableCell             '商品コード
            Dim objTdHinmei As New HtmlTableCell                '品名
            Dim objTdSuu As New HtmlTableCell                   '数量
            Dim objTdSiireGaku As New HtmlTableCell             '売上金額
            Dim objTdSiireDate As New HtmlTableCell             '仕入年月日
            Dim objTdDenpyouType As New HtmlTableCell           '伝票種別
            Dim objTdDenSiireDate As New HtmlTableCell          '伝票仕入年月日
            Dim objTdDenSiireDateLink As New HyperLink          '伝票仕入年月日リンク
            Dim objTdSiireKeijyouFlg As New HtmlTableCell       '仕入処理(仕入計上フラグ)

            Dim strDenpyouType As String

            '検索結果配列からセルに格納
            objTdDenUnqNo.InnerHtml = CLogic.GetDisplayString(data.DenpyouUniqueNo, EarthConst.HANKAKU_SPACE)
            objTdDenpyouNo.InnerHtml = CLogic.GetDisplayString(data.DenpyouNo, EarthConst.HANKAKU_SPACE)
            objTdSiireSakiCd.InnerHtml = CLogic.GetDispSeikyuuSakiCd(String.Empty, data.TysKaisyaCd, data.TysKaisyaJigyousyoCd, False)
            objTdSiireSakiMei.InnerHtml = CLogic.GetDisplayString(data.TysKaisyaMei, EarthConst.HANKAKU_SPACE)
            objTdKbn.InnerHtml = CLogic.GetDisplayString(data.Kbn, EarthConst.HANKAKU_SPACE)
            objTdBangou.InnerHtml = CLogic.GetDisplayString(data.Bangou, EarthConst.HANKAKU_SPACE)
            objTdSesyuMei.InnerHtml = CLogic.GetDisplayString(data.SesyuMei, EarthConst.HANKAKU_SPACE)
            objTdSyouhinCd.InnerHtml = CLogic.GetDisplayString(data.SyouhinCd, EarthConst.HANKAKU_SPACE)
            objTdHinmei.InnerHtml = CLogic.GetDisplayString(data.Hinmei, EarthConst.HANKAKU_SPACE)
            objTdSuu.InnerHtml = CLogic.GetDisplayString(Format(data.Suu, "#,0"), EarthConst.HANKAKU_SPACE)
            objTdSiireGaku.InnerHtml = CLogic.GetDisplayString(Format(data.SiireGaku, "#,0"), EarthConst.HANKAKU_SPACE)
            objTdSiireDate.InnerHtml = CLogic.GetDisplayString(data.SiireDate, EarthConst.HANKAKU_SPACE)
            strDenpyouType = CLogic.GetDisplayString(data.DenpyouSyubetu, EarthConst.HANKAKU_SPACE)
            objTdDenpyouType.InnerHtml = strDenpyouType
            objTdSiireKeijyouFlg.InnerHtml = IIf(data.SiireKeijyouFlg = 1, EarthConst.SIIRE_KEI_ZUMI, EarthConst.HANKAKU_SPACE)

            'マイナス伝票のみの設定
            If strDenpyouType = EarthConst.SR Then
                Dim strDenSiireJs As String = String.Empty
                objTdDenSiireDateLink.Text = CLogic.GetDisplayString(data.DenpyouSiireDate, EarthConst.HANKAKU_SPACE)
                objTdDenSiireDateLink.NavigateUrl = "javascript:void(0);"
                strDenSiireJs = "openModalDenSiireDate('" & UrlConst.POPUP_DENPYOU_SIIRE_DATE_HENKOU & "','" & CLogic.GetDisplayString(data.DenpyouUniqueNo) & "','" & CLogic.GetDisplayString(data.DenpyouSiireDate) & "')"
                objTdDenSiireDateLink.Attributes.Add("onclick", strDenSiireJs)
                objTdDenSiireDate.Controls.Add(objTdDenSiireDateLink)
            Else
                objTdDenSiireDate.InnerHtml = CLogic.GetDisplayString(data.DenpyouSiireDate, EarthConst.HANKAKU_SPACE)
            End If


            '各セルの幅設定
            If rowCnt = 1 Then
                objTdDenUnqNo.Style("width") = widthList1(0)
                objTdDenpyouType.Style("width") = widthList1(1)
                objTdDenpyouNo.Style("width") = widthList1(2)
                objTdSiireSakiCd.Style("width") = widthList1(3)
                objTdSiireSakiMei.Style("width") = widthList1(4)
                objTdKbn.Style("width") = widthList1(5)
                objTdBangou.Style("width") = widthList1(6)
                objTdSesyuMei.Style("width") = widthList1(7)

                objTdSyouhinCd.Style("width") = widthList2(0)
                objTdHinmei.Style("width") = widthList2(1)
                objTdSuu.Style("width") = widthList2(2)
                objTdSiireGaku.Style("width") = widthList2(3)
                objTdSiireDate.Style("width") = widthList2(4)
                objTdDenSiireDate.Style("width") = widthList2(5)
                objTdSiireKeijyouFlg.Style("width") = widthList2(6)
            End If

            'スタイル、クラス設定
            objTdDenUnqNo.Attributes("class") = "textCenter"
            objTdDenpyouNo.Attributes("class") = "textCenter"
            objTdKbn.Attributes("class") = "textCenter"
            objTdBangou.Attributes("class") = "textCenter"
            objTdSyouhinCd.Attributes("class") = "textCenter"
            objTdSyouhinCd.Attributes("class") = "textCenter"
            objTdSiireGaku.Attributes("class") = "kingaku"
            objTdSuu.Attributes("class") = "kingaku"
            objTdSiireDate.Attributes("class") = "date textCenter"
            objTdDenSiireDate.Attributes("class") = "date textCenter"
            objTdSiireSakiCd.Attributes("class") = "textCenter"
            objTdDenpyouType.Attributes("class") = "textCenter"
            If strDenpyouType = EarthConst.SR Then
                objTdDenpyouType.Style("color") = "red"
            End If
            objTdSiireKeijyouFlg.Attributes("class") = "textCenter"

            '一覧左側行のID付与と格納
            objTr1.ID = "DataTable_resultTr1_" & rowCnt
            objTr1.Attributes("tabindex") = -1

            With objTr1.Controls
                .Add(objTdDenUnqNo)
                .Add(objTdDenpyouType)
                .Add(objTdDenpyouNo)
                .Add(objTdSiireSakiCd)
                .Add(objTdSiireSakiMei)
                .Add(objTdKbn)
                .Add(objTdBangou)
                .Add(objTdSesyuMei)
            End With

            '一覧右側行のID付与と格納
            objTr2.ID = "DataTable_resultTr2_" & rowCnt

            If rowCnt = 1 Then
                objTr2.Attributes("tabindex") = Integer.Parse(CheckSaisinDenpyou.Attributes("tabindex")) + 10
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                '2行目以降はタブ移動なし
                objTr2.Attributes("tabindex") = -1
            End If

            With objTr2.Controls
                .Add(objTdSyouhinCd)
                .Add(objTdHinmei)
                .Add(objTdSuu)
                .Add(objTdSiireGaku)
                .Add(objTdSiireDate)
                .Add(objTdDenSiireDate)
                .Add(objTdSiireKeijyouFlg)
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