
Partial Public Class SearchKouji
    Inherits System.Web.UI.Page

    Dim cl As New CommonLogic

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            ' 工事会社コードが空の場合は名称にフォーカス
            TextName.Focus()

            If Request("sendSearchTerms") <> "" Then
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
                TextCode.Value = arrSearchTerm(0) '親画面からPOSTされた情報1 ：工事会社コード
                KameitenCd.Value = arrSearchTerm(1)     '親画面からPOSTされた情報2 ：加盟店コード
                returnTargetIds.Value = Request("returnTargetIds")  '親画面からPOSTされた戻り値セット先ID郡
                afterEventBtnId.Value = Request("afterEventBtnId")  '値セット後に押下する、親画面のボタンID

                If TextCode.Value.Trim() <> "" Then
                    ' 工事会社コードにフォーカス
                    TextCode.Focus()
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

        setTable()

    End Sub

    ''' <summary>
    ''' 検索を行い、結果を画面表示する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setTable()

        ' 区分コンボにデータをバインドする
        Dim searchLogic As New TyousakaisyaSearchLogic
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        '検索結果1件のみの場合の列ID格納用hiddenをクリア
        firstSend.Value = ""

        dataArray = searchLogic.GetKoujikaishaSearchResult(TextCode.Value, "", "", TextName.Value, True, KameitenCd.Value)

        ' 検索結果ゼロ件の場合、メッセージを表示
        If dataArray.Count = 0 Then
            Dim tmpScript As String = "alert('" & Messages.MSG020E & "');"
            ScriptManager.RegisterClientScriptBlock(searchGrid, searchGrid.GetType(), "err", tmpScript, True)
        End If

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

        Dim rowCount As Integer = 0
        For Each recData As TyousakaisyaSearchRecord In dataArray
            rowCount += 1

            Dim objTr As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdTysKaiCd As New HtmlTableCell
            Dim objTdTysKaiNm As New HtmlTableCell
            Dim objTdTysKaiKana As New HtmlTableCell

            Dim ret_msg As String = IIf(recData.KahiKbn = 9, EarthConst.TYOUSA_KAISYA_NG, "")

            objIptRtnHiddn.ID = "returnHidden" & rowCount
            objIptRtnHiddn.Value = recData.TysKaisyaCd & recData.JigyousyoCd & EarthConst.SEP_STRING & _
                                   recData.TysKaisyaMei & EarthConst.SEP_STRING & ret_msg
            objTdReturn.Controls.Add(objIptRtnHiddn)
            objTdReturn.Attributes("class") = "searchReturnValues"

            objTdTysKaiCd.InnerHtml = cl.GetDisplayString(recData.TysKaisyaCd & recData.JigyousyoCd, EarthConst.HANKAKU_SPACE)
            objTdTysKaiNm.InnerHtml = cl.GetDisplayString(recData.TysKaisyaMei, EarthConst.HANKAKU_SPACE)
            objTdTysKaiKana.InnerHtml = cl.GetDisplayString(recData.TysKaisyaMeiKana, EarthConst.HANKAKU_SPACE)

            objTr.ID = "resultTr_" & rowCount
            If rowCount = 1 Then
                objTr.Attributes("tabindex") = 0
                objTr.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                objTr.Attributes("tabindex") = -1
            End If
            objTr.Controls.Add(objTdReturn)
            objTr.Controls.Add(objTdTysKaiCd)
            objTr.Controls.Add(objTdTysKaiNm)
            objTr.Controls.Add(objTdTysKaiKana)

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
        TextName.Focus()
    End Sub

End Class