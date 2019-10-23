
Partial Public Class SearchEigyousyo
    Inherits System.Web.UI.Page

    Dim cl As New CommonLogic

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            ' 営業所コードが空の場合は名称にフォーカス
            Me.eigyousyoKanaNm.Focus()

            If Request("sendSearchTerms") <> "" Then
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
                Me.eigyousyoCd.Value = arrSearchTerm(0) '親画面からPOSTされた情報1 ：営業所コード
                returnTargetIds.Value = Request("returnTargetIds")  '親画面からPOSTされた戻り値セット先ID郡
                afterEventBtnId.Value = Request("afterEventBtnId")  '値セット後に押下する、親画面のボタンID

                If Me.eigyousyoCd.Value.Trim() <> "" Then
                    ' 営業所コードにフォーカス
                    Me.eigyousyoCd.Focus()
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

        '営業所コードと営業所カナ名は必須項目無し
        'If Me.eigyousyoCd.Value = String.Empty And Me.eigyousyoKanaNm.Value = String.Empty Then
        '    Dim tmpScript As String = "alert('" & Messages.MSG047E & "');"
        '    ScriptManager.RegisterClientScriptBlock(searchGrid, searchGrid.GetType(), "err", tmpScript, True)
        '    Me.eigyousyoCd.Focus()
        'Else
        setTable()
        'End If


    End Sub

    ''' <summary>
    ''' 検索を行い、結果を画面表示する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setTable()

        ' 区分コンボにデータをバインドする
        Dim eigyousyoSearchLogic As New EigyousyoSearchLogic
        Dim dataArray As New List(Of EigyousyoSearchRecord)

        ' 検索結果総件数
        Dim total_count As Integer

        '検索結果1件のみの場合の列ID格納用hiddenをクリア
        firstSend.Value = ""

        ' 取得件数を絞り込む場合、引数を追加してください
        dataArray = eigyousyoSearchLogic.GetEigyousyoSearchResult(eigyousyoCd.Value, _
                                                                eigyousyoKanaNm.Value, _
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
            Dim tmpScript As String = "alert('" & Messages.MSG020E & "');"
            ScriptManager.RegisterClientScriptBlock(searchGrid, searchGrid.GetType(), "err", tmpScript, True)
        End If

        Dim rowCount As Integer = 0
        For Each recData As EigyousyoSearchRecord In dataArray
            rowCount += 1
            Dim objTr As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdEigyousyoCd As New HtmlTableCell
            Dim objTdEigyousyoNm As New HtmlTableCell

            objIptRtnHiddn.ID = "returnHidden" & rowCount
            objIptRtnHiddn.Value = recData.EigyousyoCd & EarthConst.SEP_STRING & recData.EigyousyoMei
            objTdReturn.Controls.Add(objIptRtnHiddn)
            objTdReturn.Attributes("class") = "searchReturnValues"

            objTdEigyousyoCd.InnerHtml = cl.GetDisplayString(recData.EigyousyoCd, EarthConst.HANKAKU_SPACE)
            objTdEigyousyoNm.InnerHtml = cl.GetDisplayString(recData.EigyousyoMei, EarthConst.HANKAKU_SPACE)

            objTr.ID = "resultTr_" & rowCount
            If rowCount = 1 Then
                objTr.Attributes("tabindex") = 0
                objTr.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                objTr.Attributes("tabindex") = -1
            End If

            objTr.Controls.Add(objTdReturn)
            objTr.Controls.Add(objTdEigyousyoCd)
            objTr.Controls.Add(objTdEigyousyoNm)

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

        Me.eigyousyoKanaNm.Focus()

    End Sub

End Class