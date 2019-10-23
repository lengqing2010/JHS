
Partial Public Class SeikyuuSakiMototyou
    Inherits System.Web.UI.Page

    '画面表示の文字列変換用
    Private CLogic As CommonLogic = CommonLogic.Instance
    'メッセージクラス
    Private MLogic As New MessageLogic
    'ログインユーザレコード
    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

    'CSV用デリミタ指定
    Dim strCsvDelimiter As String = EarthConst.CSV_DELIMITER
    'CSV用括り文字指定
    Dim strCsvQuote As String = String.Empty

    Dim cl As New CommonLogic

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

        '表示項目の初期化
        TdSaisinKurikosiDate.InnerHtml = String.Empty
        TdTourokuZandaka.InnerHtml = String.Empty

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

            ' 請求先区分コンボにデータをバインドする
            helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuKbn, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            setDispAction()

            'フォーカス設定
            SelectSeikyuuKbn.Focus()

            '****************************************************************************
            'Requestデータ取得
            '****************************************************************************
            SelectSeikyuuKbn.SelectedValue = Request("seiKbn")
            TextSeikyuuSakiCd.Value = Request("seiCd")
            TextSeikyuuSakiBrc.Value = Request("seiBrc")
            TextNengappiFrom.Value = Request("fromDate")
            TextNengappiTo.Value = Request("toDate")

            If SelectSeikyuuKbn.SelectedValue <> String.Empty And _
               TextSeikyuuSakiCd.Value <> String.Empty And _
               TextSeikyuuSakiBrc.Value <> String.Empty And _
               TextNengappiFrom.Value <> String.Empty And _
               TextNengappiTo.Value <> String.Empty Then
                '全ての条件がリクエストデータから取得出来た場合、データ取得処理を自動実行
                ButtonHiddenDisplay_ServerClick(sender, e)
                '赤色文字変更対応を配列に格納
                Dim objChgColor As Object() = New Object() {Me.SelectSeikyuuKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakiMei, Me.TextTorikesiRiyuu}
                '取消理由取得設定と色替処理
                cl.GetKameitenTorikesiRiyuuMain(Me.SelectSeikyuuKbn.SelectedValue, Me.TextSeikyuuSakiCd.Value, TextTorikesiRiyuu, True, False, objChgColor)
            Else
                Dim dteNow As DateTime = DateTime.Now
                Dim dteTermFirst As DateTime

                '年度始め日付
                dteTermFirst = cl.GetTermFirstDate(dteNow)
                TextNengappiFrom.Value = dteTermFirst.ToString(EarthConst.FORMAT_DATE_TIME_9)
                'システム日付
                TextNengappiTo.Value = dteNow.ToString(EarthConst.FORMAT_DATE_TIME_9)
            End If
        End If

    End Sub

#Region "ボタンイベント"
    ''' <summary>
    ''' 請求先検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSeikyuuSakiSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeikyuuSakiSearch.ServerClick

        '請求先検索画面呼出
        Dim ret As Boolean = CLogic.CallSeikyuuSakiSearchWindow(sender, e, Me _
                                                                , Me.SelectSeikyuuKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc _
                                                                , Me.TextSeikyuuSakiMei, Me.btnSeikyuuSakiSearch)

        '赤色文字変更対応を配列に格納
        Dim objChgColor As Object() = New Object() {Me.SelectSeikyuuKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakiMei, Me.TextTorikesiRiyuu}
        '取消理由取得設定と色替処理
        cl.GetKameitenTorikesiRiyuuMain(Me.SelectSeikyuuKbn.SelectedValue, Me.TextSeikyuuSakiCd.Value, TextTorikesiRiyuu, True, False, objChgColor)

        'フォーカスセット
        If ret Then
            masterAjaxSM.SetFocus(Me.btnSeikyuuSakiSearch)
        End If

    End Sub

    ''' <summary>
    ''' 検索実行ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHiddenDisplay_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenDisplay.ServerClick
        Try
            'ボタンにフォーカス
            If HiddenCsvOutPut.Value = String.Empty Then
                Me.ButtonDisplay.Focus()
            Else
                Me.ButtonCsv.Focus()
            End If

            '入力値チェック
            If Not checkInput(sender) Then
                HiddenCsvOutPut.Value = String.Empty
                Exit Sub
            End If

            '検索結果を画面に表示
            SetSearchResult(sender, e)

        Catch ex As Exception
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "表示") & "\r\n" & ex.Message, 0, "kensakuErr")

        End Try

    End Sub

    ''' <summary>
    ''' CSV出力ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHiddenCsv_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenCsv.ServerClick

        Try
            'ボタンにフォーカス
            Me.ButtonCsv.Focus()

            '入力値チェック
            If Not checkInput(sender) Then
                Exit Sub
            End If

            '検索結果を画面に表示
            Dim dtList As List(Of SeikyuuSakiMototyouRecord) = SetSearchResult(sender, e)

            If dtList.Count = 0 Then
                ' 検索結果ゼロ件の場合、メッセージを表示
                MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
                Exit Sub
            ElseIf dtList.Count = -1 Then
                ' 検索結果件数が-1の場合、エラーなので、処理終了
                MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV出力"), 0, "kensakuErr")
                Exit Sub
            End If

            'データレコードのListの内容を、CSV出力用に文字列化
            Dim csvString As String = recListToString(dtList)

            '出力ファイル名
            Dim strFileNm As String = "請求先元帳データ.csv"

            'HTTPレスポンスオブジェクト(カレント)
            Dim httpRes As HttpResponse = HttpContext.Current.Response

            'ファイルの出力を行う
            With httpRes
                .Clear()
                .AddHeader("Content-Disposition", "attachment;filename=" & HttpUtility.UrlEncode(strFileNm))
                .ContentType = "text/plain"
                .BinaryWrite(System.Text.Encoding.GetEncoding("Shift-JIS").GetBytes(csvString))
                .End()
            End With

        Catch ex As Exception
            HiddenCsvOutPut.Value = String.Empty
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV出力") & "\r\n" & ex.Message, 0, "kensakuErr")

        End Try


    End Sub

    ''' <summary>
    ''' 元帳印刷ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHiddenPrint_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenPrint.ServerClick
        Try

            '入力値チェック
            If Not checkInput(sender) Then
                Me.ButtonPrint.Focus()
                Exit Sub
            End If

            '検索結果を画面に表示
            Dim dtList As List(Of SeikyuuSakiMototyouRecord) = SetSearchResult(sender, e)

            If dtList.Count = 0 Then
                ' 検索結果ゼロ件の場合、メッセージを表示
                MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
                Me.ButtonPrint.Focus()
                Exit Sub
            ElseIf dtList.Count = -1 Then
                ' 検索結果件数が-1の場合、エラーなので、処理終了
                MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "印刷"), 0, "kensakuErr")
                Me.ButtonPrint.Focus()
                Exit Sub
            End If

            'PDFプレビュー呼び出し用GETパラメータ設定
            Dim tmpParam As String = "{0}&{1}&{2}&{3}&{4}&{5}"
            tmpParam = String.Format(tmpParam, "seiKbn=" & HttpUtility.UrlEncode(SelectSeikyuuKbn.SelectedValue), _
                                               "seiCd=" & HttpUtility.UrlEncode(TextSeikyuuSakiCd.Value), _
                                               "seiBrc=" & HttpUtility.UrlEncode(TextSeikyuuSakiBrc.Value), _
                                               "seiNm=" & HttpUtility.UrlEncode(TextSeikyuuSakiMei.Value), _
                                               "fromDate=" & HttpUtility.UrlEncode(TextNengappiFrom.Value), _
                                               "toDate=" & HttpUtility.UrlEncode(TextNengappiTo.Value))
            'PDFプレビュー呼び出し用スクリプト
            Dim tmpScript As String = "document.getElementById('" & ButtonPrint.ClientID & "').focus();window.open('" & UrlConst.EARTH2_SEIKYUUSAKI_MOTOTYOU_OUTPUT & "?" & tmpParam & "');"
            ScriptManager.RegisterStartupScript(sender, sender.GetType(), "callprint", tmpScript, True)


        Catch ex As Exception
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "印刷") & "\r\n" & ex.Message, 0, "kensakuErr")
            Me.ButtonPrint.Focus()

        End Try
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
        ButtonDisplay.Attributes("onclick") = "checkJikkou('0');"
        'CSV出力ボタンのイベントハンドラを設定
        Me.ButtonCsv.Attributes("onclick") = "checkJikkou('1');"
        '印刷ボタンのイベントハンドラを設定
        Me.ButtonPrint.Attributes("onclick") = "checkJikkou('2');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'マスタ検索系コード入力項目イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSeikyuuSakiCd.Attributes("onchange") = "clrSeikyuuInfo(this);"
        Me.TextSeikyuuSakiBrc.Attributes("onchange") = "clrSeikyuuInfo(this);"
        Me.SelectSeikyuuKbn.Attributes("onchange") = "clrSeikyuuInfo(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)

    End Sub

    ''' <summary>
    ''' 入力値チェック
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function checkInput(ByVal sender As System.Object)

        'エラーメッセージ初期化
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New ArrayList

        '請求先名を取得
        Dim uriageLogic As New UriageDataSearchLogic
        Dim seikyuusakiRec As New SeikyuuSakiInfoRecord
        Dim intAllCnt As Integer = uriageLogic.GetSeikyuuSakiCnt(TextSeikyuuSakiCd.Value, TextSeikyuuSakiBrc.Value, SelectSeikyuuKbn.SelectedValue, String.Empty, String.Empty, False)
        If intAllCnt = 1 Then
            '現在の画面状態で、請求先が１件のみ取得できる場合、自動で名称を画面にセット
            seikyuusakiRec = uriageLogic.GetSeikyuuSakiInfo(TextSeikyuuSakiCd.Value, TextSeikyuuSakiBrc.Value, SelectSeikyuuKbn.SelectedValue, String.Empty, String.Empty, intAllCnt, 1, 10, False)(0)
            '取得した情報を画面項目にセット
            SelectSeikyuuKbn.SelectedValue = seikyuusakiRec.SeikyuuSakiKbn
            TextSeikyuuSakiCd.Value = seikyuusakiRec.SeikyuuSakiCd
            TextSeikyuuSakiBrc.Value = seikyuusakiRec.SeikyuuSakiBrc
            TextSeikyuuSakiMei.Value = seikyuusakiRec.SeikyuuSakiMei
        End If

        '必須チェック
        If String.IsNullOrEmpty(SelectSeikyuuKbn.SelectedValue) Or String.IsNullOrEmpty(TextSeikyuuSakiCd.Value) Or String.IsNullOrEmpty(TextSeikyuuSakiBrc.Value) Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "請求先")
            arrFocusTargetCtrl.Add(SelectSeikyuuKbn)
        End If
        If String.IsNullOrEmpty(TextNengappiFrom.Value) OrElse String.IsNullOrEmpty(TextNengappiTo.Value) Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "年月日")
            arrFocusTargetCtrl.Add(TextNengappiFrom)
        End If

        '日付チェック
        '日付From
        If TextNengappiFrom.Value <> String.Empty Then
            If Not CLogic.checkDateHanni(TextNengappiFrom.Value) Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "年月日(FROM)")
                arrFocusTargetCtrl.Add(TextNengappiFrom)
            End If
        End If
        '日付To
        If TextNengappiTo.Value <> String.Empty Then
            If Not CLogic.checkDateHanni(TextNengappiTo.Value) Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "年月日(TO)")
                arrFocusTargetCtrl.Add(TextNengappiTo)
            End If
        End If
        '日付From・過去データチェック
        If TextNengappiFrom.Value <> String.Empty Then
            If TextNengappiFrom.Value < EarthConst.KEIRI_DATA_MIN_DATE Then
                errMess += Messages.MSG179W
                arrFocusTargetCtrl.Add(TextNengappiFrom)
            End If
        End If
        '日付From-Toチェック
        If TextNengappiFrom.Value <> String.Empty And TextNengappiTo.Value <> String.Empty Then
            If TextNengappiFrom.Value > TextNengappiTo.Value Then
                errMess += Messages.MSG022E.Replace("@PARAM1", "年月日")
                arrFocusTargetCtrl.Add(TextNengappiFrom)
            End If
        End If

        'エラー発生時はメッセージ表示
        If errMess <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            MLogic.AlertMessage(sender, errMess, 0, "inputerror")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索条件で検索した内容を検索結果テーブルに表示する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Function SetSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs) As List(Of SeikyuuSakiMototyouRecord)
        Const CSS_TEXT_CENTER = "textCenter"
        Const CSS_KINGAKU = "kingaku"
        'Const CELL_COLOR As String = "red"
        'Const CELL_BOLD As String = "bold"
        'Const CSS_NUMBER = "number"
        'Const CSS_DATE = "date"

        Dim listCount As Integer = 0

        'ロジッククラスの生成
        Dim logic As New MototyouLogic

        '売掛金データテーブルの取得
        Dim urikakeData As UrikakeDataRecord = logic.GetUrikakeDataNewest(TextSeikyuuSakiCd.Value, _
                                                                          TextSeikyuuSakiBrc.Value, _
                                                                          SelectSeikyuuKbn.SelectedValue)
        '画面項目に値をセット
        setTdStr(TdSaisinKurikosiDate, urikakeData.TaisyouNengetu)
        setTdStr(TdTourokuZandaka, urikakeData.TougetuKurikosiZan, EarthConst.FORMAT_KINGAKU_1)

        '繰越残高を取得
        Dim kurikosiZan As Long = logic.GetSeikyuuSakiMototyouKurikosiZan(TextSeikyuuSakiCd.Value, _
                                                                          TextSeikyuuSakiBrc.Value, _
                                                                          SelectSeikyuuKbn.SelectedValue, _
                                                                          TextNengappiFrom.Value)
        Dim kurikosiRec As New SeikyuuSakiMototyouRecord
        kurikosiRec.Kamoku = "繰越残高"
        kurikosiRec.Zandaka = kurikosiZan

        'データ行取得実行
        Dim dataList As List(Of SeikyuuSakiMototyouRecord) = _
                                    logic.GetSeikyuuSakiMototyouDenpyouData(TextSeikyuuSakiCd.Value, _
                                                                            TextSeikyuuSakiBrc.Value, _
                                                                            SelectSeikyuuKbn.SelectedValue, _
                                                                            TextNengappiFrom.Value, _
                                                                            TextNengappiTo.Value)

        '結果件数セット
        listCount = dataList.Count

        '取得結果ゼロ件の場合、メッセージを表示
        If listCount = 0 Then
            '取得結果ゼロ件の場合、メッセージを表示
            MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
            HiddenCsvOutPut.Value = String.Empty
            Return dataList
        ElseIf listCount = -1 Then
            '取得結果件数が-1の場合、エラーなので、処理終了
            HiddenCsvOutPut.Value = String.Empty
            Return dataList
        End If

        '合計行を作成
        Dim goukeiRec As New SeikyuuSakiMototyouRecord
        goukeiRec.Kamoku = "合計"
        goukeiRec.Hinmei = "期間合計"

        '先頭行、最終行にそれぞれ繰越、合計行レコードをセット
        dataList.Insert(0, kurikosiRec)
        dataList.Insert(dataList.Count, goukeiRec)

        '検索結果件数を設定
        TdResultCount.InnerHtml = listCount

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
        Dim tmpGoukei As Long = 0               '合計行用の合計金額を初期化
        Dim tmpZandaka As Long = kurikosiZan    '残高項目を初期化(繰越残高をセット)
        Dim tmpDenpyouNo As String = String.Empty
        For Each rec As SeikyuuSakiMototyouRecord In dataList

            rowCnt += 1

            Dim objTr1 As New HtmlTableRow
            Dim objTr2 As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden

            Dim objTdNengappi As New HtmlTableCell              '年月日
            Dim objTdKamoku As New HtmlTableCell                '科目
            Dim objTdSyouhinCd As New HtmlTableCell             '商品コード
            Dim objTdHinmei As New HtmlTableCell                '商品名/入金種別など
            Dim objTdKokyakuNo As New HtmlTableCell             '顧客番号
            Dim objTdBukkenMei As New HtmlTableCell             '物件名/摘要など
            Dim objTdSuu As New HtmlTableCell                   '数量
            Dim objTdTanka As New HtmlTableCell                 '単価
            Dim objTdZeinukiGaku As New HtmlTableCell           '税抜金額
            Dim objTdSotozeiGaku As New HtmlTableCell           '消費税
            Dim objTdKingaku As New HtmlTableCell               '金額
            Dim objTdZandaka As New HtmlTableCell               '残高
            Dim objTdSeikyuusyoHakkouDate As New HtmlTableCell  '請求書発行日
            Dim objTdKaisyuuYoteiDate As New HtmlTableCell      '回収予定日
            Dim objTdDenpyouNo As New HtmlTableCell             '伝票番号

            '残高、合計額計算の蓄積と、入金伝票番号の一時保持
            If rec.Kamoku = "売上" Then
                '売上時、残高を加算
                tmpZandaka += IIf(rec.Kingaku = Long.MinValue, 0, rec.Kingaku)
                '売上時、合計額を加算
                tmpGoukei += IIf(rec.Kingaku = Long.MinValue, 0, rec.Kingaku)
                '一時保持伝票番号の更新
                tmpDenpyouNo = String.Empty
            ElseIf rec.Kamoku = "入金" Then
                '売上以外(入金)時、残高を減算
                tmpZandaka -= IIf(rec.Kingaku = Long.MinValue, 0, rec.Kingaku)
                '一時保持伝票番号の更新
                If tmpDenpyouNo = rec.DenpyouNo Then
                    '直前行と同値の場合、画面表示には伝票番号を表示しない
                    rec.DenpyouNo = String.Empty
                Else
                    '伝票番号が変わっていた場合、一時保持値も変更
                    tmpDenpyouNo = rec.DenpyouNo
                End If
            ElseIf rec.Kamoku = "合計" Then
                '合計行の場合、それまでの残高、合計額をセット
                rec.Kingaku = tmpGoukei
            End If
            '残高をレコードにセット
            rec.Zandaka = tmpZandaka

            '検索結果配列からセルに格納
            setTdStr(objTdNengappi, rec.Nengappi)
            setTdStr(objTdKamoku, rec.Kamoku)
            setTdStr(objTdSyouhinCd, rec.SyouhinCd)
            setTdStr(objTdHinmei, rec.Hinmei)
            setTdStr(objTdKokyakuNo, rec.KokyakuNo)
            setTdStr(objTdBukkenMei, rec.BukkenMei)
            setTdStr(objTdSuu, rec.Suu, EarthConst.FORMAT_KINGAKU_1)
            setTdStr(objTdTanka, rec.Tanka, EarthConst.FORMAT_KINGAKU_1)
            setTdStr(objTdZeinukiGaku, rec.ZeinukiGaku, EarthConst.FORMAT_KINGAKU_1)
            setTdStr(objTdSotozeiGaku, rec.SotozeiGaku, EarthConst.FORMAT_KINGAKU_1)
            setTdStr(objTdKingaku, rec.Kingaku, EarthConst.FORMAT_KINGAKU_1)
            setTdStr(objTdZandaka, rec.Zandaka, EarthConst.FORMAT_KINGAKU_1)
            setTdStr(objTdSeikyuusyoHakkouDate, rec.SeikyuuDate)
            setTdStr(objTdKaisyuuYoteiDate, rec.KaisyuuYoteiDate)
            setTdStr(objTdDenpyouNo, rec.DenpyouNo)

            '各セルの幅設定()
            If rowCnt = 1 Then
                objTdNengappi.Style("width") = widthList1(0)
                objTdKamoku.Style("width") = widthList1(1)
                objTdSyouhinCd.Style("width") = widthList1(2)
                objTdHinmei.Style("width") = widthList1(3)
                objTdKokyakuNo.Style("width") = widthList1(4)
                objTdBukkenMei.Style("width") = widthList1(5)
                objTdSuu.Style("width") = widthList1(6)

                objTdTanka.Style("width") = widthList2(0)
                objTdZeinukiGaku.Style("width") = widthList2(1)
                objTdSotozeiGaku.Style("width") = widthList2(2)
                objTdKingaku.Style("width") = widthList2(3)
                objTdZandaka.Style("width") = widthList2(4)
                objTdSeikyuusyoHakkouDate.Style("width") = widthList2(5)
                objTdKaisyuuYoteiDate.Style("width") = widthList2(6)
                objTdDenpyouNo.Style("width") = widthList2(7)
            End If

            'スタイル、クラス設定
            objTdNengappi.Attributes("class") = CSS_TEXT_CENTER
            objTdKamoku.Attributes("class") = CSS_TEXT_CENTER
            objTdSyouhinCd.Attributes("class") = CSS_TEXT_CENTER
            objTdKokyakuNo.Attributes("class") = CSS_TEXT_CENTER
            objTdSuu.Attributes("class") = CSS_KINGAKU
            objTdTanka.Attributes("class") = CSS_KINGAKU
            objTdZeinukiGaku.Attributes("class") = CSS_KINGAKU
            objTdSotozeiGaku.Attributes("class") = CSS_KINGAKU
            objTdKingaku.Attributes("class") = CSS_KINGAKU
            objTdZandaka.Attributes("class") = CSS_KINGAKU
            objTdSeikyuusyoHakkouDate.Attributes("class") = CSS_TEXT_CENTER
            objTdKaisyuuYoteiDate.Attributes("class") = CSS_TEXT_CENTER
            objTdDenpyouNo.Attributes("class") = CSS_TEXT_CENTER

            objTr1.ID = "DataTable_resultTr1_" & rowCnt
            objTr1.Attributes("tabindex") = -1

            '行にセルをセット1
            With objTr1.Controls
                .Add(objTdNengappi)
                .Add(objTdKamoku)
                .Add(objTdSyouhinCd)
                .Add(objTdHinmei)
                .Add(objTdKokyakuNo)
                .Add(objTdBukkenMei)
                .Add(objTdSuu)
            End With

            objTr2.ID = "DataTable_resultTr2_" & rowCnt
            If rowCnt = 1 Then
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop(" & DivLeftData.ClientID & ");"
            Else
                '2行目以降はタブ移動なし
                objTr2.Attributes("tabindex") = -1
            End If

            '行にセルとセット2
            With objTr2.Controls
                .Add(objTdTanka)
                .Add(objTdZeinukiGaku)
                .Add(objTdSotozeiGaku)
                .Add(objTdKingaku)
                .Add(objTdZandaka)
                .Add(objTdSeikyuusyoHakkouDate)
                .Add(objTdKaisyuuYoteiDate)
                .Add(objTdDenpyouNo)
            End With

            'テーブルに行をセット
            TableDataTable1.Controls.Add(objTr1)
            TableDataTable2.Controls.Add(objTr2)

        Next

        'リストを戻す
        Return dataList

    End Function

#Region "レコードの内容を出力用にテキスト化"
    Private Function recListToString(ByVal DataList As List(Of SeikyuuSakiMototyouRecord)) As String
        Dim sb As New StringBuilder
        Dim seikyuuSakiKbn As String = SelectSeikyuuKbn.SelectedValue
        Dim seikyuuSakiCd As String = TextSeikyuuSakiCd.Value
        Dim seikyuuSakiBrc As String = TextSeikyuuSakiBrc.Value
        Dim seikyuuSakiMei As String = TextSeikyuuSakiMei.Value

        'タイトル行をセット
        setCsvString(sb, "請求先区分")
        setCsvString(sb, "請求先コード")
        setCsvString(sb, "請求先枝番")
        setCsvString(sb, "請求先名")
        setCsvString(sb, "年月日")
        setCsvString(sb, "科目")
        setCsvString(sb, "商品コード")
        setCsvString(sb, "商品名/入金種別など")
        setCsvString(sb, "顧客番号")
        setCsvString(sb, "物件名/摘要など")
        setCsvString(sb, "数量")
        setCsvString(sb, "単価")
        setCsvString(sb, "税抜金額")
        setCsvString(sb, "消費税")
        setCsvString(sb, "金額")
        setCsvString(sb, "残高")
        setCsvString(sb, "請求書発行日")
        setCsvString(sb, "回収予定日")
        setCsvString(sb, "伝票番号", True)

        'Listをループ
        For Each rec As SeikyuuSakiMototyouRecord In DataList

            'CSV出力用のStringBuilderに値をセットする
            setCsvString(sb, seikyuuSakiKbn)
            setCsvString(sb, seikyuuSakiCd)
            setCsvString(sb, seikyuuSakiBrc)
            setCsvString(sb, seikyuuSakiMei)
            setCsvString(sb, rec.Nengappi)
            setCsvString(sb, rec.Kamoku)
            setCsvString(sb, rec.SyouhinCd)
            setCsvString(sb, rec.Hinmei)
            setCsvString(sb, rec.KokyakuNo)
            setCsvString(sb, rec.BukkenMei)
            setCsvString(sb, rec.Suu)
            setCsvString(sb, rec.Tanka)
            setCsvString(sb, rec.ZeinukiGaku)
            setCsvString(sb, rec.SotozeiGaku)
            setCsvString(sb, rec.Kingaku)
            setCsvString(sb, rec.Zandaka)
            setCsvString(sb, rec.SeikyuuDate)
            setCsvString(sb, rec.KaisyuuYoteiDate)
            setCsvString(sb, rec.DenpyouNo, True)

        Next

        Return sb.ToString
    End Function


#End Region

#End Region


#Region "ローカル関数"

    ''' <summary>
    ''' TDに適した文字列値をセット
    ''' </summary>
    ''' <param name="td"></param>
    ''' <param name="val"></param>
    ''' <param name="formatStr"></param>
    ''' <param name="defStr"></param>
    ''' <remarks></remarks>
    Private Sub setTdStr(ByRef td As HtmlTableCell, _
                         ByVal val As Object, _
                         Optional ByVal formatStr As String = "", _
                         Optional ByVal defStr As String = EarthConst.HANKAKU_SPACE)

        Dim retStr As String = String.Empty

        'GetDisplayStringで表示文字列化
        retStr = CLogic.GetDisplayString(val, defStr)

        'フォーマットスタイルが指定されており、GetDisplayStringの結果が空値でない場合、
        '元の値から文字列フォーマットを行う
        If Not String.IsNullOrEmpty(formatStr) AndAlso retStr <> defStr Then
            retStr = Format(val, formatStr)
        End If

        'TDにセット
        td.InnerHtml = retStr

    End Sub

    ''' <summary>
    ''' CSV出力用のStringBuilderに値をセットする
    ''' </summary>
    ''' <param name="sb"></param>
    ''' <param name="valObj"></param>
    ''' <param name="flgEndCol"></param>
    ''' <remarks></remarks>
    Private Sub setCsvString(ByRef sb As StringBuilder, ByVal valObj As Object, Optional ByVal flgEndCol As Boolean = False)

        sb.AppendFormat(strCsvQuote & "{0}" & strCsvQuote, CLogic.GetDisplayString(valObj, String.Empty))

        '最終カラムの場合、デリミタではなく改行をセットする
        If flgEndCol Then
            sb.Append(vbCrLf)
        Else
            sb.Append(strCsvDelimiter)
        End If

    End Sub


#End Region

End Class