Public Partial Class SeikyuuDateIkkatuHenkou
    Inherits System.Web.UI.Page

    '画面表示の文字列変換用
    Private CLogic As CommonLogic = CommonLogic.Instance
    'メッセージクラス
    Private MLogic As New MessageLogic
    'ログインユーザレコード
    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    '請求年月日一括変更ロジック
    Dim logic As New SeikyuuDateIkkatuHenkouLogic

#Region "プロパティ"

#Region "コントロール値"
    '一覧ヘッダー行
    Private Const HEADER_TEIBETU As String = "邸別請求テーブル"
    Private Const HEADER_TENBETU As String = "店別請求テーブル"
    Private Const HEADER_TENBETU_SYOKI As String = "店別初期請求テーブル"
    Private Const HEADER_HANNYOU_URIAGE As String = "汎用売上テーブル"
    Private Const HEADER_URIAGE_DATA As String = "売上データテーブル(元データ削除済)"

    '処理結果
    Private Const RESULT_STR_KEN As String = "件"

    Private Const CTRL_NAME_TR1 As String = "DataTable_resultTr1_"
    Private Const CTRL_NAME_TR2 As String = "DataTable_resultTr2_"

    '検索ボタン押下有無判断用
    Public Const BTN_SEARCH_FLG_ARI As String = "1"
    Public Const BTN_SEARCH_FLG_NASI As String = "0"

#End Region

#Region "CSSクラス名"
    Private Const CSS_TEXT_CENTER = "textCenter"
    Private Const CSS_KINGAKU = "kingaku"
    Private Const CSS_NUMBER = "number"
    Private Const CSS_DATE = "date"
#End Region

#End Region

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

            ' 請求先区分コンボにデータをバインドする
            helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuKbn, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            setDispAction()

            'フォーカス設定
            SelectSeikyuuKbn.Focus()

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
        Dim blnResult As Boolean
        Dim comBizLogic As New CommonBizLogic

        '請求先検索画面呼出
        blnResult = CLogic.CallSeikyuuSakiSearchWindow(sender, e, Me _
                                                        , Me.SelectSeikyuuKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc _
                                                        , Me.TextSeikyuuSakiMei, Me.btnSeikyuuSakiSearch)

        '赤色文字変更対応を配列に格納
        Dim objChgColor As Object() = New Object() {Me.SelectSeikyuuKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakiMei, Me.TextTorikesiRiyuu}
        '取消理由取得設定と色替処理
        CLogic.GetKameitenTorikesiRiyuuMain(Me.SelectSeikyuuKbn.SelectedValue, Me.TextSeikyuuSakiCd.Value, TextTorikesiRiyuu, True, False, objChgColor)

        '請求締日セット
        If Me.TextSeikyuuSakiCd.Value <> String.Empty Then
            TextSeikyuuSimebi.Value = comBizLogic.GetSeikyuuSimeDate(Me.TextSeikyuuSakiCd.Value, _
                                                                     Me.TextSeikyuuSakiBrc.Value, _
                                                                     Me.SelectSeikyuuKbn.SelectedValue)
            If TextSeikyuuSimebi.Value <> String.Empty Then
                TextSeikyuuSimebi.Value &= "日"
            End If
        End If

        If blnResult Then
            'hiddenに退避
            Me.HiddenSeikyuuSakiKbnOld.Value = Me.SelectSeikyuuKbn.SelectedValue
            Me.HiddenSeikyuuSakiCdOld.Value = Me.TextSeikyuuSakiCd.Value
            Me.HiddenSeikyuuSakiBrcOld.Value = Me.TextSeikyuuSakiBrc.Value
            'フォーカスセット
            setFocusAJ(Me.btnSearch)
        End If

        '請求先を変更した場合は再度検索ボタン押下が必要
        Me.HiddenSearch.Value = BTN_SEARCH_FLG_NASI

    End Sub

    ''' <summary>
    ''' 請求年月日一括変更処理実行ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSeikyuuDateIkkatuHenkouExe_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSeikyuuDateIkkatuHenkouExe.ServerClick

        '入力チェック
        If Not CheckInput() Then
            Exit Sub
        Else
        End If

        Try
            Dim listResult As New List(Of Integer)

            '処理実行
            listResult = logic.SeikyuuDateIkkatuHenkou(sender, _
                                                       TextSeikyuuSakiCd.Value, _
                                                       TextSeikyuuSakiBrc.Value, _
                                                       SelectSeikyuuKbn.SelectedValue, _
                                                       TextSeikyuuDate.Value, _
                                                       DateTime.Now, _
                                                       userinfo.LoginUserId)

            If listResult(0) < 0 Then
                TdResult1.InnerHtml = "エラー"
                MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "請求年月日一括変更") & "\r\n" & listResult(0), 0, "err")
            Else
                '結果表示
                Me.SetTableHenkouKekka(listResult)
            End If

        Catch ex As Exception
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "請求年月日一括変更") & "\r\n" & ex.Message, 0, "err")

        End Try

        'DB値の最新を画面描画
        Me.SetCmnSearchResult(sender, e, True)

    End Sub

    ''' <summary>
    ''' 検索実行ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick
        setFocusAJ(Me.btnSearch) 'フォーカス

        '入力チェック
        If Not CheckInput() Then
            Exit Sub
        Else
        End If

        Me.SetCmnSearchResult(sender, e)

    End Sub

#End Region

#Region "プライベートメソッド"

    Private Sub setDispAction()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ボタンのイベントハンドラを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '検索実行ボタン
        Me.btnSearch.Attributes("onclick") = "checkJikkou();"

        '検索ボタン押下有無判断(無)
        Me.HiddenSearch.Value = BTN_SEARCH_FLG_NASI

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'マスタ検索系コード入力項目イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSeikyuuSakiCd.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.TextSeikyuuSakiBrc.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.SelectSeikyuuKbn.Attributes("onblur") = "clrSeikyuuInfo(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        CLogic.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)

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
    ''' 検索実行時処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCmnSearchResult(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs, _
                                    Optional ByVal UpdateFlg As Boolean = False)

        Dim total_count As Integer = 0  '取得件数
        Dim row_count As Integer = 0    '各テーブルの取得件数

        Dim listResult As New List(Of Integer)
        Dim intResult As Integer = 0
        listResult.Add(intResult)

        '************
        '* 検索実行
        '************
        '1.検索実行＠邸別請求テーブル
        Dim TeibetuArray As New List(Of SeikyuuDateIkkatuHenkouRecord)
        TeibetuArray = Me.GetCmnSearchResult(sender, _
                                                row_count, _
                                                total_count, _
                                                listResult, _
                                                EarthEnum.emIkkatuHenkouDataSearchType.TeibetuSeikyuu)

        If row_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            Exit Sub
        End If

        '2.検索実行＠店別請求テーブル
        Dim TenbetuArray As New List(Of SeikyuuDateIkkatuHenkouRecord)
        TenbetuArray = Me.GetCmnSearchResult(sender, _
                                                row_count, _
                                                total_count, _
                                                listResult, _
                                                EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSeikyuu)
        If row_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            Exit Sub
        End If

        '3.検索実行＠店別初期請求テーブル
        Dim TenbetuSyokiArray As New List(Of SeikyuuDateIkkatuHenkouRecord)
        TenbetuSyokiArray = Me.GetCmnSearchResult(sender, _
                                                     row_count, _
                                                     total_count, _
                                                     listResult, _
                                                     EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSyoki)
        If row_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            Exit Sub
        End If

        '4.検索実行＠汎用売上テーブル
        Dim HannyouUriageArray As New List(Of SeikyuuDateIkkatuHenkouRecord)
        HannyouUriageArray = Me.GetCmnSearchResult(sender, _
                                                      row_count, _
                                                      total_count, _
                                                      listResult, _
                                                      EarthEnum.emIkkatuHenkouDataSearchType.HannyouUriage)
        If row_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            Exit Sub
        End If

        '5.検索実行＠売上データテーブル
        Dim UriageDataArray As New List(Of SeikyuuDateIkkatuHenkouRecord)
        UriageDataArray = Me.GetCmnSearchResult(sender, _
                                                   row_count, _
                                                   total_count, _
                                                   listResult, _
                                                   EarthEnum.emIkkatuHenkouDataSearchType.UriageData)
        If row_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            Exit Sub
        End If

        ' 検索結果ゼロ件の場合、メッセージを表示
        If total_count = 0 Then
            ' 検索結果ゼロ件の場合、メッセージを表示
            Dim tmpScript As String = "if(gNoDataMsgFlg != '1'){alert('" & Messages.MSG020E & "')}gNoDataMsgFlg = null;"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "kensakuZero", tmpScript, True)
        End If

        '検索結果件数を設定
        resultCount.InnerHtml = total_count

        '************************
        '* 画面テーブルへ出力
        '************************
        '行カウンタ
        Dim rowCnt As Integer = 0

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
        '* 結果表示
        '************
        '1.結果表示＠邸別請求テーブル
        Me.SetSearchResult(sender, _
                            e, _
                            TeibetuArray, _
                            EarthEnum.emIkkatuHenkouDataSearchType.TeibetuSeikyuu, _
                            widthList1, _
                            widthList2, _
                            rowCnt)

        '2.結果表示＠店別請求テーブル
        Me.SetSearchResult(sender, _
                            e, _
                            TenbetuArray, _
                            EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSeikyuu, _
                            widthList1, _
                            widthList2, _
                            rowCnt)

        '3.結果表示＠店別初期請求テーブル
        Me.SetSearchResult(sender, _
                            e, _
                            TenbetuSyokiArray, _
                            EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSyoki, _
                            widthList1, _
                            widthList2, _
                            rowCnt)

        '4.結果表示＠汎用売上テーブル
        Me.SetSearchResult(sender, _
                            e, _
                            HannyouUriageArray, _
                            EarthEnum.emIkkatuHenkouDataSearchType.HannyouUriage, _
                            widthList1, _
                            widthList2, _
                            rowCnt)

        '5.結果表示＠売上データテーブル
        Me.SetSearchResult(sender, _
                            e, _
                            UriageDataArray, _
                            EarthEnum.emIkkatuHenkouDataSearchType.UriageData, _
                            widthList1, _
                            widthList2, _
                            rowCnt)

        ''一括変更ボタン押下時は、更新結果件数を表示する
        'If UpdateFlg = False Then
        '    '結果表示
        '    Me.SetTableHenkouKekka(listResult)
        'End If

        '検索ボタン押下有無判断(有)
        Me.HiddenSearch.Value = BTN_SEARCH_FLG_ARI

    End Sub

    ''' <summary>
    ''' 対象テーブルを検索する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="row_count">最終行</param>
    ''' <param name="total_count">全件数</param>
    ''' <param name="listResult">処理結果件数表示用リスト</param>
    ''' <param name="emType">請求年月日一括変更検索タイプ</param>
    ''' <returns>請求年月日一括変更検索用レコードのList(Of SeikyuuDateIkkatuHenkouRecord)</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSearchResult(ByVal sender As System.Object, _
                                        ByRef row_count As Integer, _
                                        ByRef total_count As Integer, _
                                        ByRef listResult As List(Of Integer), _
                                        ByVal emType As EarthEnum.emIkkatuHenkouDataSearchType _
                                        ) As List(Of SeikyuuDateIkkatuHenkouRecord)

        '表示最大件数
        Dim end_count As Integer = EarthConst.MAX_RESULT_COUNT

        'ヘッダー行を作成
        Dim teibetuRec As New SeikyuuDateIkkatuHenkouRecord
        Dim tenbetuRec As New SeikyuuDateIkkatuHenkouRecord
        Dim tenbetuSyokiRec As New SeikyuuDateIkkatuHenkouRecord
        Dim HannyouUriageRec As New SeikyuuDateIkkatuHenkouRecord
        Dim UriageDataRec As New SeikyuuDateIkkatuHenkouRecord
        Dim UriageDataSeikyuuDateRec As New SeikyuuDateIkkatuHenkouRecord

        'ヘッダー行に値をセット
        teibetuRec.SesyuMei = HEADER_TEIBETU
        tenbetuRec.SesyuMei = HEADER_TENBETU
        tenbetuSyokiRec.SesyuMei = HEADER_TENBETU_SYOKI
        HannyouUriageRec.SesyuMei = HEADER_HANNYOU_URIAGE
        UriageDataRec.SesyuMei = HEADER_URIAGE_DATA

        Dim resultArray As New List(Of SeikyuuDateIkkatuHenkouRecord)
        resultArray = logic.GetSeikyuuDateIkkatuHenkou(sender, _
                                                       TextSeikyuuSakiCd.Value, _
                                                       TextSeikyuuSakiBrc.Value, _
                                                       SelectSeikyuuKbn.SelectedValue, _
                                                       1, _
                                                       end_count, _
                                                       row_count, _
                                                       emType)
        If row_count >= 0 Then
            '検索結果件数
            listResult(0) = row_count
            listResult.Add(row_count)

            If row_count <> 0 Then

                '先頭行ヘッダーをセット
                Select Case emType
                    Case EarthEnum.emIkkatuHenkouDataSearchType.TeibetuSeikyuu
                        resultArray.Insert(0, teibetuRec)

                    Case EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSeikyuu
                        resultArray.Insert(0, tenbetuRec)

                    Case EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSyoki
                        resultArray.Insert(0, tenbetuSyokiRec)

                    Case EarthEnum.emIkkatuHenkouDataSearchType.HannyouUriage
                        resultArray.Insert(0, HannyouUriageRec)

                    Case EarthEnum.emIkkatuHenkouDataSearchType.UriageData
                        resultArray.Insert(0, UriageDataRec)

                    Case EarthEnum.emIkkatuHenkouDataSearchType.UriageDataTorikesiSeikyuuDate
                        resultArray.Insert(0, UriageDataSeikyuuDateRec)
                End Select

                '総件数にセット
                total_count += row_count
            End If
        End If

        Return resultArray

    End Function

    ''' <summary>
    ''' 検索条件で検索した内容を検索結果テーブルに表示する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="resultArray">請求年月日一括変更検索用レコードのList(Of SeikyuuDateIkkatuHenkouRecord)</param>
    ''' <param name="emType">請求年月日一括変更検索タイプ</param>
    ''' <param name="widthList1">各セルの幅設定用のリスト1</param>
    ''' <param name="widthlist2">各セルの幅設定用のリスト2</param>
    ''' <param name="rowCnt">行カウンタ</param>
    ''' <remarks></remarks>
    Private Sub SetSearchResult(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs, _
                                ByVal resultArray As List(Of SeikyuuDateIkkatuHenkouRecord), _
                                ByVal emType As EarthEnum.emIkkatuHenkouDataSearchType, _
                                ByVal widthList1 As List(Of String), _
                                ByVal widthlist2 As List(Of String), _
                                ByRef rowCnt As Integer)

        '************
        '* 変数宣言
        '************
        Dim blnFirstRow As Boolean = True
        Dim strSpace As String = EarthConst.HANKAKU_SPACE

        'TabIndex
        Dim intTrTabIndex As Integer = 5
        'Tr
        Dim objTr1 As HtmlTableRow
        Dim objTr2 As HtmlTableRow

        'Table Cell
        Dim objTdKbn As HtmlTableCell               '区分
        Dim objTdBangou As HtmlTableCell            '番号
        Dim objTdSesyuMei As HtmlTableCell          '施主名
        Dim objTdKameitenCd As HtmlTableCell        '加盟店コード
        Dim objTdSyouhinCd As HtmlTableCell         '商品コード
        Dim objTdSyouhinMei As HtmlTableCell        '商品名

        Dim objTdSuu As HtmlTableCell               '数量
        Dim objTdUriGaku As HtmlTableCell           '売上金額
        Dim objTdSeikyuusyoHakDate As HtmlTableCell '請求書発行日
        Dim objTdUriDate As HtmlTableCell           '売上年月日
        Dim objTdDennpyouUriDate As HtmlTableCell   '伝票売上年月日

        '取得したデータを画面に表示
        For Each data As SeikyuuDateIkkatuHenkouRecord In resultArray
            rowCnt += 1

            'Tr
            objTr1 = New HtmlTableRow
            objTr2 = New HtmlTableRow

            'Table Cell
            objTdKbn = New HtmlTableCell                '区分
            objTdBangou = New HtmlTableCell             '番号
            objTdSesyuMei = New HtmlTableCell           '施主名
            objTdKameitenCd = New HtmlTableCell         '加盟店コード
            objTdSyouhinCd = New HtmlTableCell          '商品コード
            objTdSyouhinMei = New HtmlTableCell         '商品名

            objTdSuu = New HtmlTableCell                '数量
            objTdUriGaku = New HtmlTableCell            '売上金額
            objTdSeikyuusyoHakDate = New HtmlTableCell  '請求書発行日
            objTdUriDate = New HtmlTableCell            '売上年月日
            objTdDennpyouUriDate = New HtmlTableCell    '伝票売上年月日

            '検索結果配列からセルに格納
            objTdKbn.InnerHtml = CLogic.GetDisplayString(data.Kbn, strSpace)
            objTdBangou.InnerHtml = CLogic.GetDisplayString(data.Bangou, strSpace)
            objTdSesyuMei.InnerHtml = CLogic.GetDisplayString(data.SesyuMei, strSpace)
            objTdKameitenCd.InnerHtml = CLogic.GetDisplayString(data.KameitenCd, strSpace)
            objTdSyouhinCd.InnerHtml = CLogic.GetDisplayString(data.SyouhinCd, strSpace)
            objTdSyouhinMei.InnerHtml = CLogic.GetDisplayString(data.SyouhinMei, strSpace)

            objTdSuu.InnerHtml = CLogic.GetDisplayString(data.Suu, strSpace)

            If blnFirstRow Then
                objTdUriGaku.InnerHtml = CLogic.GetDisplayString(data.UriGaku, strSpace)
                blnFirstRow = False
            Else
                If emType = EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSeikyuu _
                    OrElse emType = EarthEnum.emIkkatuHenkouDataSearchType.HannyouUriage Then

                    If data.Tanka = Integer.MinValue OrElse data.Suu = Integer.MinValue Then
                        objTdUriGaku.InnerHtml = CLogic.GetDisplayString(data.UriGaku, strSpace)
                    Else
                        objTdUriGaku.InnerHtml = CLogic.GetDisplayString(data.Tanka * data.Suu, strSpace)
                    End If
                Else
                    objTdUriGaku.InnerHtml = CLogic.GetDisplayString(data.UriGaku, strSpace)
                End If
            End If

            objTdSeikyuusyoHakDate.InnerHtml = CLogic.GetDisplayString(data.SeikyuusyoHakDate, strSpace)
            objTdUriDate.InnerHtml = CLogic.GetDisplayString(data.UriDate, strSpace)
            objTdDennpyouUriDate.InnerHtml = CLogic.GetDisplayString(data.DenpyouUriDate, strSpace)


            '各セルの幅設定
            If rowCnt = 1 Then
                objTdKbn.Style("width") = widthList1(0)
                objTdBangou.Style("width") = widthList1(1)
                objTdSesyuMei.Style("width") = widthList1(2)
                objTdKameitenCd.Style("width") = widthList1(3)
                objTdSyouhinCd.Style("width") = widthList1(4)
                objTdSyouhinMei.Style("width") = widthList1(5)

                objTdSuu.Style("width") = widthlist2(0)
                objTdUriGaku.Style("width") = widthlist2(1)
                objTdSeikyuusyoHakDate.Style("width") = widthlist2(2)
                objTdUriDate.Style("width") = widthlist2(3)
                objTdDennpyouUriDate.Style("width") = widthlist2(4)
            End If

            'スタイル、クラス設定
            objTdKbn.Attributes("class") = CSS_TEXT_CENTER
            objTdBangou.Attributes("class") = CSS_TEXT_CENTER
            objTdSesyuMei.Attributes("class") = ""
            objTdKameitenCd.Attributes("class") = CSS_TEXT_CENTER
            objTdSyouhinCd.Attributes("class") = CSS_TEXT_CENTER
            objTdSyouhinMei.Attributes("class") = ""

            objTdSuu.Attributes("class") = CSS_NUMBER
            objTdUriGaku.Attributes("class") = CSS_KINGAKU
            objTdSeikyuusyoHakDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdUriDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdDennpyouUriDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER


            '行IDとJSイベントの付与
            objTr1.ID = CTRL_NAME_TR1 & rowCnt
            objTr1.Attributes("tabindex") = -1

            '行にセルをセット1
            With objTr1.Controls
                .Add(objTdKbn)
                .Add(objTdBangou)
                .Add(objTdSesyuMei)
                .Add(objTdKameitenCd)
                .Add(objTdSyouhinCd)
                .Add(objTdSyouhinMei)
            End With

            '行IDとJSイベントの付与
            objTr2.ID = CTRL_NAME_TR2 & rowCnt
            If rowCnt = 1 Then
                objTr2.Attributes("tabindex") = Integer.Parse(Me.btnSearch.Attributes("tabindex")) + intTrTabIndex
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop(" & DivLeftData.ClientID & ");"
            Else
                '2行目以降はタブ移動なし
                objTr2.Attributes("tabindex") = -1
            End If

            '行にセルとセット2
            With objTr2.Controls
                .Add(objTdSuu)
                .Add(objTdUriGaku)
                .Add(objTdSeikyuusyoHakDate)
                .Add(objTdUriDate)
                .Add(objTdDennpyouUriDate)
            End With

            'テーブルに行をセット
            TableDataTable1.Controls.Add(objTr1)
            TableDataTable2.Controls.Add(objTr2)
        Next
    End Sub

    ''' <summary>
    ''' 処理結果表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTableHenkouKekka(ByVal listResult As List(Of Integer))

        TdResult1.InnerHtml = listResult(1) & RESULT_STR_KEN
        TdResult2.InnerHtml = listResult(2) & RESULT_STR_KEN
        TdResult3.InnerHtml = listResult(3) & RESULT_STR_KEN
        TdResult4.InnerHtml = listResult(4) & RESULT_STR_KEN
        TdResult5.InnerHtml = listResult(5) & RESULT_STR_KEN

    End Sub

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckInput() As Boolean
        'エラーメッセージ初期化
        Dim strErrMsg As String = ""
        Dim arrFocusTargetCtrl As New ArrayList

        '●コード入力値変更チェック
        CheckSeikyuuChg(strErrMsg)

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If strErrMsg <> "" Then
            'フォーカスセット
            btnSeikyuuSakiSearch.Focus()
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        'エラー無しの場合、Trueを返す
        Return True

    End Function

    ''' <summary>
    ''' 請求先入力値変更チェック
    ''' </summary>
    ''' <param name="strErrMsg"></param>
    ''' <remarks></remarks>
    Private Sub CheckSeikyuuChg(ByRef strErrMsg As String)

        '請求先コード・枝番・区分のどれかに入力がある場合のみチェック
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd.Value) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc.Value) Or _
           Me.SelectSeikyuuKbn.SelectedIndex <> 0 Then

            '検索ボタン押下時と現在で差異があるのかチェック
            If Me.TextSeikyuuSakiCd.Value <> Me.HiddenSeikyuuSakiCdOld.Value _
                Or Me.TextSeikyuuSakiBrc.Value <> Me.HiddenSeikyuuSakiBrcOld.Value _
                Or Me.SelectSeikyuuKbn.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "請求先")
            End If
        End If
    End Sub

#End Region
End Class