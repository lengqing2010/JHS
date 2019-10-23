Partial Public Class SearchSiharaiData
    Inherits System.Web.UI.Page

    '画面表示の文字列変換用
    Private CLogic As CommonLogic = CommonLogic.Instance
    'メッセージクラス
    Private MLogic As New MessageLogic
    'ログインユーザレコード
    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    '売上データテーブルレコードクラス
    Private rec As New SiharaiDataKeyRecord

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
            'なし

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            setDispAction()

            'フォーカス設定
            Me.TextShriDateFrom.Focus()

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
        Me.BtnSearch.Focus()

        '検索条件を設定
        SetSearchKeyFromCtrl(rec)

        '検索結果を画面に表示
        SetSearchResult(sender, e)

    End Sub

    ''' <summary>
    ''' 調査会社検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnTysKaisyaSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTysKaisyaSearch.ServerClick
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)
        Dim blnTorikesi As Boolean = True

        If Me.TextTysKaisyaCd.Value <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetTyousakaisyaSearchResult(Me.TextTysKaisyaCd.Value, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi, _
                                                                            "")
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            Me.TextTysKaisyaCd.Value = recData.TysKaisyaCd & recData.JigyousyoCd
            Me.TextTysKaisyaMei.Value = recData.TysKaisyaMei

            'フォーカスセット
            masterAjaxSM.SetFocus(Me.BtnTysKaisyaSearch)
        Else
            '調査会社名をクリア
            Me.TextTysKaisyaMei.Value = String.Empty
            Me.HiddenTyskaisyaCd.Value = String.Empty
            Dim tmpFocusScript = "objEBI('" & BtnTysKaisyaSearch.ClientID & "').focus();"
            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpScript = "callSearch('" & Me.TextTysKaisyaCd.ClientID & EarthConst.SEP_STRING & Me.HiddenTyskaisyaCd.ClientID & _
                                             "','" & UrlConst.SEARCH_TYOUSAKAISYA & _
                                             "','" & Me.TextTysKaisyaCd.ClientID & EarthConst.SEP_STRING & Me.TextTysKaisyaMei.ClientID & _
                                             "','" & Me.BtnTysKaisyaSearch.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSearch", tmpScript, True)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 新会計支払先検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnSkkShriSakiSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSkkShriSakiSearch.ServerClick
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim lgcTysKaisyaSearch As New TyousakaisyaSearchLogic
        Dim dataArray As New List(Of SinkaikeiSiharaiSakiRecord)

        If Me.TextSkkJigyousyoCd.Value <> String.Empty Or Me.TextSkkShriSakiCd.Value <> String.Empty Then
            dataArray = lgcTysKaisyaSearch.GetSkkSiharaisakiSearchResult(Me.TextSkkJigyousyoCd.Value, _
                                                                         Me.TextSkkShriSakiCd.Value)
        End If

        If dataArray.Count = 1 Then
            Dim recData As SinkaikeiSiharaiSakiRecord = dataArray(0)
            Me.TextSkkJigyousyoCd.Value = recData.SkkJigyouCd
            Me.TextSkkShriSakiCd.Value = recData.SkkShriSakiCd
            Me.TextShriSakiMei.Value = recData.ShriSakiMeiKanji
        Else
            '支払先名をクリア
            Me.TextShriSakiMei.Value = String.Empty
            '新会計支払先マスタを表示
            Dim strScript = "objSrchWin = window.open('" & UrlConst.EARTH2_SEARCH_SINKAIKEI_SIHARAI_SAKI & "?Kbn='+escape('新会計支払先')+'&SiharaiKbn='+escape   ('Siharai')+'&FormName=" & _
                                                      Me.Page.Form.Name & "&objCd=" & _
                                                      Me.TextSkkJigyousyoCd.ClientID & _
                                                      "&objCd2=" & Me.TextSkkShriSakiCd.ClientID & _
                                                      "&objHidCd2=" & Me.HiddenSkkShriSakiCd.ClientID & _
                                                      "&objMei=" & Me.TextShriSakiMei.ClientID & _
                                                      "&strCd='+escape(eval('document.all.'+'" & _
                                                      Me.TextSkkJigyousyoCd.ClientID & "').value)+'&strCd2='+escape(eval('document.all.'+'" & _
                                                      Me.TextSkkShriSakiCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                                                      Me.TextShriSakiMei.ClientID & "').value), 'searchWindow',       'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
            Exit Sub
        End If

    End Sub


    ''' <summary>
    ''' CSV出力ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnHiddenCsv_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHiddenCsv.ServerClick
        setFocusAJ(Me.BtnCsv) 'フォーカス

        Me.HiddenCsvOutPut.Value = String.Empty 'フラグをクリア

        Dim strFileNm As String = String.Empty  '出力ファイル名
        Dim dtCsv As DataTable
        Dim myLogic As New SiharaiDataSearchLogic

        '検索条件の設定
        Me.SetSearchKeyFromCtrl(rec)

        '件数
        Dim total_count As Integer = 0

        '検索実行
        dtCsv = myLogic.GetSiharaiDataCsv(sender, rec, total_count)

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
        If CLogic.OutPutFileFromDtTable(EarthConst.FILE_NAME_CSV_SIHARAI_DATA, dtCsv) = False Then
            ' 出力用文字列がないので、処理終了
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV出力"), 0, "OutputErr")
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

        Dim checkDate As String = "checkDate(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 日付系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '支払年月日From
        Me.TextShriDateFrom.Attributes("onblur") = checkDate
        '支払年月日To
        Me.TextShriDateTo.Attributes("onblur") = checkDate

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 検索実行ボタン関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '検索実行ボタンのイベントハンドラを設定
        BtnSearch.Attributes("onclick") = "checkJikkou('0');"
        'CSV出力ボタンのイベントハンドラを設定
        Me.BtnCsv.Attributes("onclick") = "checkJikkou('1');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '伝票番号イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextDenNoFrom.Attributes("onblur") = "this.value = paddingStr(this.value, 5, '0');"
        Me.TextDenNoTo.Attributes("onblur") = "this.value = paddingStr(this.value, 5, '0');"


        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'マスタ検索系コード入力項目イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextTysKaisyaCd.Attributes("onblur") = "clrName(this,'" & Me.TextTysKaisyaMei.ClientID & "');"
        Me.TextSkkJigyousyoCd.Attributes("onblur") = "clrName(this,'" & Me.TextShriSakiMei.ClientID & "');"
        Me.TextSkkShriSakiCd.Attributes("onblur") = "clrName(this,'" & Me.TextShriSakiMei.ClientID & "');"

    End Sub

    ''' <summary>
    ''' 画面項目から検索キーレコードへの値セットを行う。
    ''' </summary>
    ''' <param name="recKey">抽出レコードクラスのキー</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromCtrl(ByRef recKey As SiharaiDataKeyRecord)

        '支払年月日 From
        recKey.ShriDateFrom = IIf(Me.TextShriDateFrom.Value <> "", Me.TextShriDateFrom.Value, DateTime.MinValue)
        '支払年月日日 To
        recKey.ShriDateTo = IIf(Me.TextShriDateTo.Value <> "", Me.TextShriDateTo.Value, DateTime.MinValue)
        '伝票番号 From
        recKey.DenNoFrom = IIf(Me.TextDenNoFrom.Value <> "", Me.TextDenNoFrom.Value, String.Empty)
        '伝票番号 To 
        recKey.DenNoTo = IIf(Me.TextDenNoTo.Value <> "", Me.TextDenNoTo.Value, String.Empty)
        '調査会社コード＋調査会社事業所コード
        recKey.TysKaisyaCd = IIf(Me.TextTysKaisyaCd.Value <> "", Me.TextTysKaisyaCd.Value, String.Empty)
        '新会計事業所コード
        recKey.SkkJigyouCd = IIf(Me.TextSkkJigyousyoCd.Value <> "", Me.TextSkkJigyousyoCd.Value, String.Empty)
        '新会計支払先コード
        recKey.SkkShriSakiCd = IIf(Me.TextSkkShriSakiCd.Value <> "", Me.TextSkkShriSakiCd.Value, String.Empty)
        '最新伝票のみ表示
        recKey.NewDenDisp = IIf(CheckSaisinDenpyou.Checked, CheckSaisinDenpyou.Value, Integer.MinValue)

    End Sub

    ''' <summary>
    ''' 検索条件で検索した内容を検索結果テーブルに表示する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Const CELL_COLOR As String = "red"
        Const CSS_TEXT_CENTER = "textCenter"
        Const CSS_KINGAKU = "kingaku"
        Const CSS_DATE = "date"

        '表示最大件数
        Dim end_count As Integer = maxSearchCount.Value
        Dim total_count As Integer = 0
        Dim intCsvMaxCnt As Integer = Integer.Parse(EarthConst.MAX_CSV_OUTPUT)

        'ロジッククラスの生成
        Dim logic As New SiharaiDataSearchLogic

        '検索実行
        Dim resultArray As List(Of SiharaiSearchResultRecord) = logic.GetSiharaiDataInfo(sender, rec, 1, end_count, total_count)

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
        '支払合計
        Dim lngTotalGaku As Long = 0

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

        '取得した支払データを画面に表示
        For Each data As SiharaiSearchResultRecord In resultArray

            rowCnt += 1

            Dim objTr1 As New HtmlTableRow
            Dim objTr2 As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdDenUnqNo As New HtmlTableCell              '伝票ユニークNO
            Dim objTdDenNo As New HtmlTableCell                 '伝票番号
            Dim objTdTysKaisyaCd As New HtmlTableCell           '調査会社コード
            Dim objTdSkkCd As New HtmlTableCell                 '新会計コード
            Dim objTdShriSaki As New HtmlTableCell              '支払先
            Dim objTdFurikomigaku As New HtmlTableCell          '振込額
            Dim objTdSousaigaku As New HtmlTableCell            '相殺額
            Dim objTdShriDate As New HtmlTableCell              '支払年月日
            Dim objTdTekiyou As New HtmlTableCell               '摘要

            '検索結果配列からセルに格納
            objTdDenUnqNo.InnerHtml = CLogic.GetDisplayString(data.DenUnqNo, EarthConst.HANKAKU_SPACE)
            objTdDenNo.InnerHtml = CLogic.GetDisplayString(data.DenNo, EarthConst.HANKAKU_SPACE)
            objTdTysKaisyaCd.InnerHtml = CLogic.GetDisplayString(data.TysKaisyaCd, EarthConst.HANKAKU_SPACE)
            objTdSkkCd.InnerHtml = CLogic.GetDisplayString(data.SkkJigyouCd + data.SkkShriSakiCd, EarthConst.HANKAKU_SPACE)
            objTdShriSaki.InnerHtml = CLogic.GetDisplayString(data.ShriSakiMei, EarthConst.HANKAKU_SPACE)
            objTdFurikomigaku.InnerHtml = CLogic.GetDisplayString(Format(data.Furikomi, EarthConst.FORMAT_KINGAKU_1), EarthConst.HANKAKU_SPACE)
            objTdSousaigaku.InnerHtml = CLogic.GetDisplayString(Format(data.Sousai, EarthConst.FORMAT_KINGAKU_1), EarthConst.HANKAKU_SPACE)
            objTdShriDate.InnerHtml = CLogic.GetDisplayString(data.ShriDate, EarthConst.HANKAKU_SPACE)
            objTdTekiyou.InnerHtml = CLogic.GetDisplayString(data.TekiyouMei, EarthConst.HANKAKU_SPACE)

            '総合計金額を加算
            lngTotalGaku += (data.Furikomi + data.Sousai)

            '各セルの幅設定()
            If rowCnt = 1 Then
                objTdDenUnqNo.Style("width") = widthList1(0)
                objTdDenNo.Style("width") = widthList1(1)
                objTdTysKaisyaCd.Style("width") = widthList1(2)
                objTdSkkCd.Style("width") = widthList1(3)
                objTdShriSaki.Style("width") = widthList1(4)

                objTdFurikomigaku.Style("width") = widthList2(0)
                objTdSousaigaku.Style("width") = widthList2(1)
                objTdShriDate.Style("width") = widthList2(2)
                objTdTekiyou.Style("width") = widthList2(3)
            End If

            'スタイル、クラス設定
            objTdDenUnqNo.Attributes("class") = CSS_TEXT_CENTER
            objTdDenNo.Attributes("class") = CSS_TEXT_CENTER
            objTdTysKaisyaCd.Attributes("class") = CSS_TEXT_CENTER
            objTdSkkCd.Attributes("class") = CSS_TEXT_CENTER
            objTdFurikomigaku.Attributes("class") = CSS_KINGAKU
            objTdSousaigaku.Attributes("class") = CSS_KINGAKU
            objTdShriDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER

            objTr1.ID = "DataTable_resultTr1_" & rowCnt
            objTr1.Attributes("tabindex") = -1

            '行にセルをセット1
            With objTr1.Controls
                .Add(objTdDenUnqNo)
                .Add(objTdDenNo)
                .Add(objTdTysKaisyaCd)
                .Add(objTdSkkCd)
                .Add(objTdShriSaki)
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
                .Add(objTdFurikomigaku)
                .Add(objTdSousaigaku)
                .Add(objTdShriDate)
                .Add(objTdTekiyou)
            End With

            'テーブルに行をセット
            TableDataTable1.Controls.Add(objTr1)
            TableDataTable2.Controls.Add(objTr2)
        Next

        '支払合計を設定
        Me.TdTotalKingaku.InnerHtml = Format(lngTotalGaku, EarthConst.FORMAT_KINGAKU_2)

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