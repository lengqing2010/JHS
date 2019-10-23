
Partial Public Class SearchSeikyuuSaki
    Inherits System.Web.UI.Page

    Dim udsLogic As New UriageDataSearchLogic
    Dim mesLogic As New MessageLogic
    Dim cl As New CommonLogic

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            ' 請求先コードが空の場合は名称にフォーカス
            TextKana.Focus()

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            Dim helper As New DropDownHelper

            ' 請求先区分コンボにデータをバインドする
            helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)

            If Request("sendSearchTerms") <> "" Then
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
                TextCd.Value = arrSearchTerm(0)                     '親画面からPOSTされた情報1 ：請求先コード
                TextBrc.Value = arrSearchTerm(1)                     '親画面からPOSTされた情報1 ：請求先コード
                SelectSeikyuuSakiKbn.SelectedValue = arrSearchTerm(2)                     '親画面からPOSTされた情報1 ：請求先コード
                returnTargetIds.Value = Request("returnTargetIds")  '親画面からPOSTされた戻り値セット先ID郡
                afterEventBtnId.Value = Request("afterEventBtnId")  '値セット後に押下する、親画面のボタンID
            End If

            If TextCd.Value.Trim() <> "" Then
                ' 請求先コードにフォーカス
                TextCd.Focus()
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


        'データ取得最大件数をセット
        Dim maxCount As Integer
        Try
            maxCount = Integer.Parse(SelectMaxSearchCount.Value)
        Catch ex As Exception
            maxCount = 100
        End Try

        '検索条件に沿った請求先のレコードをすべて取得します
        Dim list As List(Of SeikyuuSakiInfoRecord)
        '実検索件数格納用
        Dim resultRowCount As Integer
        list = udsLogic.GetSeikyuuSakiInfo(TextCd.Value, _
                                           TextBrc.Value, _
                                           SelectSeikyuuSakiKbn.SelectedValue, _
                                           String.Empty, _
                                           TextKana.Value, _
                                           resultRowCount, _
                                           1, _
                                           maxCount)

        ' 検索結果ゼロ件の場合、メッセージを表示
        If resultRowCount = 0 Then
            mesLogic.AlertMessage(sender, Messages.MSG020E)
        End If

        '表示件数制限処理
        Dim displayCount As String = resultRowCount
        TdResultCount.Style.Remove("color")
        If maxCount <> Integer.MaxValue Then
            If maxCount < resultRowCount Then
                TdResultCount.Style("color") = "red"
                displayCount = maxCount & " / " & CommonLogic.Instance.GetDisplayString(resultRowCount)
            End If
        End If
        TdResultCount.InnerHtml = displayCount

        ' 行カウンタ
        Dim rowCount As Integer = 0

        ' 取得した請求先情報を画面に表示
        For Each data As SeikyuuSakiInfoRecord In list

            rowCount += 1
            Dim objTr As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdCd As New HtmlTableCell
            Dim objTdName As New HtmlTableCell

            objIptRtnHiddn.ID = "returnHidden" & rowCount
            With objIptRtnHiddn
                .Value = String.Empty
                .Value &= data.SeikyuuSakiCd & EarthConst.SEP_STRING
                .Value &= data.SeikyuuSakiBrc & EarthConst.SEP_STRING
                .Value &= data.SeikyuuSakiKbn & EarthConst.SEP_STRING
                .Value &= data.SeikyuuSakiMei & EarthConst.SEP_STRING
            End With
            objTdReturn.Controls.Add(objIptRtnHiddn)
            objTdReturn.Attributes("class") = "searchReturnValues"

            objTdCd.InnerHtml = cl.GetDispSeikyuuSakiCd(data.SeikyuuSakiKbn, data.SeikyuuSakiCd, data.SeikyuuSakiBrc, False)
            objTdName.InnerHtml = cl.GetDisplayString(data.SeikyuuSakiMei, EarthConst.HANKAKU_SPACE)

            objTr.ID = "resultTr_" & rowCount
            If rowCount = 1 Then
                objTr.Attributes("tabindex") = 0
                objTr.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                objTr.Attributes("tabindex") = -1
            End If
            objTr.Controls.Add(objTdReturn)
            objTr.Controls.Add(objTdCd)
            objTr.Controls.Add(objTdName)

            searchGrid.Controls.Add(objTr)

            If resultRowCount = 1 Then
                '検索結果1件のみの場合の列ID格納用hiddenに値をセット
                firstSend.Value = objTr.ClientID
            End If

        Next

        TextKana.Focus()

    End Sub

End Class