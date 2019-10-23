
Partial Public Class SearchKameiten
    Inherits System.Web.UI.Page

    'メッセージロジック
    Dim mLogic As New MessageLogic
    Dim cl As New CommonLogic

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            ' 加盟店コードが空の場合は名称にフォーカス
            kameitenKanaNm.Focus()

            If Request("sendSearchTerms") <> "" Then
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
                kubun.Value = arrSearchTerm(0) '親画面からPOSTされた情報1 ：区分
                kameitenCd.Value = arrSearchTerm(1) '親画面からPOSTされた情報1 ：加盟店コード
                returnTargetIds.Value = Request("returnTargetIds")  '親画面からPOSTされた戻り値セット先ID郡
                afterEventBtnId.Value = Request("afterEventBtnId")  '値セット後に押下する、親画面のボタンID

                If kameitenCd.Value.Trim() <> "" Then
                    ' 加盟店コードにフォーカス
                    kameitenCd.Focus()
                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' 検索実行時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick

        If kameitenCd.Value = String.Empty And _
           kameitenKanaNm.Value = String.Empty And _
           TextKameitenTourokuJyuusyo.Value = String.Empty And _
           TextKameitenTelNo.Value = String.Empty Then

            Dim strMsg As String = Messages.MSG026E.Replace("@PARAM1", "加盟店コード").Replace("@PARAM2", "加盟店カナ名")

            '検索条件の何れかは必須
            mLogic.AlertMessage(sender, strMsg, 1, "hissuError")
            'フォーカスセット
            kameitenCd.Focus()
        Else
            '検索実行＆結果表示
            setTable()

        End If


    End Sub

    ''' <summary>
    ''' 検索を行い、結果を画面表示する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setTable()

        ' 区分コンボにデータをバインドする
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim dataArray As New List(Of KameitenSearchRecord)

        ' 検索結果総件数
        Dim total_count As Integer

        '検索結果1件のみの場合の列ID格納用hiddenをクリア
        firstSend.Value = ""

        ' 加盟店マスタ検索実行
        ' (取得件数を絞り込む場合、引数を追加してください)
        dataArray = kameitenSearchLogic.GetKameitenSearchResult(kubun.Value, _
                                                                kameitenCd.Value, _
                                                                "", _
                                                                kameitenKanaNm.Value, _
                                                                TextKameitenTourokuJyuusyo.Value, _
                                                                TextKameitenTelNo.Value, _
                                                                True, _
                                                                total_count)
        '表示件数制限処理
        Dim displayCount As String = ""
        displayCount = dataArray.Count
        resultCount.Style.Remove("color")
        If maxSearchCount.Value <> "max" Then
            If Val(maxSearchCount.Value) < dataArray.Count Then
                resultCount.Style("color") = "red"
                displayCount = maxSearchCount.Value & " / " & CommonLogic.Instance.GetDisplayString(dataArray.Count)
            End If
        End If

        resultCount.InnerHtml = displayCount

        ' 検索結果ゼロ件の場合、メッセージを表示
        If dataArray.Count = 0 Then
            mLogic.AlertMessage(searchGrid, Messages.MSG020E, 1, "zeroError")
        End If

        Dim rowCount As Integer = 0
        For Each recData As KameitenSearchRecord In dataArray
            rowCount += 1
            Dim objTr As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdKameitenCd As New HtmlTableCell
            Dim objTdKameitenNm As New HtmlTableCell
            Dim objTdTodouhukenMei As New HtmlTableCell
            Dim objTdKameitenKana As New HtmlTableCell

            objIptRtnHiddn.ID = "returnHidden" & rowCount
            objIptRtnHiddn.Value = recData.KameitenCd & EarthConst.SEP_STRING & recData.KameitenMei1
            objTdReturn.Controls.Add(objIptRtnHiddn)
            objTdReturn.Attributes("class") = "searchReturnValues"

            objTdKameitenCd.InnerHtml = cl.GetDisplayString(recData.KameitenCd, EarthConst.HANKAKU_SPACE)
            objTdKameitenNm.InnerHtml = cl.GetDisplayString(recData.KameitenMei1, EarthConst.HANKAKU_SPACE)
            objTdTodouhukenMei.InnerHtml = cl.GetDisplayString(recData.TodouhukenMei, EarthConst.HANKAKU_SPACE)
            objTdKameitenKana.InnerHtml = cl.GetDisplayString(recData.TenmeiKana1, EarthConst.HANKAKU_SPACE)

            objTr.ID = "resultTr_" & rowCount
            If rowCount = 1 Then
                objTr.Attributes("tabindex") = 0
                objTr.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                objTr.Attributes("tabindex") = -1
            End If
            objTr.Controls.Add(objTdReturn)
            objTr.Controls.Add(objTdKameitenCd)
            objTr.Controls.Add(objTdKameitenNm)
            objTr.Controls.Add(objTdTodouhukenMei)
            objTr.Controls.Add(objTdKameitenKana)

            searchGrid.Controls.Add(objTr)

            If dataArray.Count = 1 Then
                '検索結果1件のみの場合の列ID格納用hiddenに値をセット
                firstSend.Value = objTr.ClientID
            End If

            '表示件数制限処理
            If maxSearchCount.Value <> "max" Then
                If rowCount >= Val(maxSearchCount.Value) Then
                    Exit For
                End If
            End If

        Next

        kameitenKanaNm.Focus()

    End Sub

End Class