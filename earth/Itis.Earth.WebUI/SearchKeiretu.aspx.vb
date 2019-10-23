
Partial Public Class SearchKeiretu
    Inherits System.Web.UI.Page

    Dim cl As New CommonLogic

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            ' 系列コードが空の場合は名称にフォーカス
            keiretuNm.Focus()

            If Request("sendSearchTerms") <> "" Then
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
                kubun.Value = arrSearchTerm(0) '親画面からPOSTされた情報1 ：区分
                keiretuCd.Value = arrSearchTerm(1) '親画面からPOSTされた情報2 ：系列コード
                returnTargetIds.Value = Request("returnTargetIds")  '親画面からPOSTされた戻り値セット先ID郡
                afterEventBtnId.Value = Request("afterEventBtnId")  '値セット後に押下する、親画面のボタンID

                If keiretuCd.Value.Trim() <> "" Then
                    ' 系列コードにフォーカス
                    keiretuCd.Focus()
                End If
            End If

        End If

    End Sub

    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick
        setTable()

    End Sub

    ''' <summary>
    ''' 検索を行い、結果を画面表示する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setTable()

        ' 区分コンボにデータをバインドする
        Dim KeiretuSearchLogic As New KeiretuSearchLogic
        Dim dataArray As New List(Of KeiretuSearchRecord)

        '検索結果1件のみの場合の列ID格納用hiddenをクリア
        firstSend.Value = ""

        dataArray = KeiretuSearchLogic.GetKeiretuSearchResult(kubun.Value, keiretuCd.Value, keiretuNm.Value, True)

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
        For Each recData As KeiretuSearchRecord In dataArray
            rowCount += 1
            Dim objTr As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdKeiretuCd As New HtmlTableCell
            Dim objTdKeiretuNm As New HtmlTableCell

            objIptRtnHiddn.ID = "returnHidden" & rowCount
            objIptRtnHiddn.Value = recData.KeiretuCd & EarthConst.SEP_STRING & recData.KeiretuMei
            objTdReturn.Controls.Add(objIptRtnHiddn)
            objTdReturn.Attributes("class") = "searchReturnValues"

            objTdKeiretuCd.InnerHtml = cl.GetDisplayString(recData.KeiretuCd, EarthConst.HANKAKU_SPACE)
            objTdKeiretuNm.InnerHtml = cl.GetDisplayString(recData.KeiretuMei, EarthConst.HANKAKU_SPACE)

            objTr.ID = "resultTr_" & rowCount
            If rowCount = 1 Then
                objTr.Attributes("tabindex") = 0
                objTr.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                objTr.Attributes("tabindex") = -1
            End If
            objTr.Controls.Add(objTdReturn)
            objTr.Controls.Add(objTdKeiretuCd)
            objTr.Controls.Add(objTdKeiretuNm)

            searchGrid.Controls.Add(objTr)

            '検索結果1件のみの場合の列ID格納用hiddenをクリア
            firstSend.Value = ""
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

        keiretuNm.Focus()
    End Sub

End Class