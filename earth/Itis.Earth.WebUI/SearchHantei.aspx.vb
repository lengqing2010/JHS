
Partial Public Class SearchHantei
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

            ' コードが空の場合は名称にフォーカス
            TextName.Focus()

            If Request("sendSearchTerms") <> "" Then
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
                TextCode.Value = arrSearchTerm(0) '親画面からPOSTされた情報1 ：コード
                HiddenKameitenCode.Value = arrSearchTerm(1) '親画面からPOSTされた情報1 ：加盟店コード
                returnTargetIds.Value = Request("returnTargetIds")  '親画面からPOSTされた戻り値セット先ID郡
                afterEventBtnId.Value = Request("afterEventBtnId")  '値セット後に押下する、親画面のボタンID

                If HiddenKameitenCode.Value.Trim() <> "" Then
                    ' コードにフォーカス
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

        'If HiddenKameitenCode.Value = String.Empty And TextName.Value = String.Empty Then
        '    Dim tmpScript As String = "alert('" & Messages.MSG026E & "');"
        '    ScriptManager.RegisterClientScriptBlock(searchGrid, searchGrid.GetType(), "err", tmpScript, True)
        '    HiddenKameitenCode.Focus()
        'Else
        '    setTable()

        'End If

        setTable()

    End Sub

    ''' <summary>
    ''' 検索を行い、結果を画面表示する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setTable()

        ' 区分コンボにデータをバインドする
        Dim KisoSiyouSearchLogic As New KisoSiyouLogic
        Dim dataArray As New List(Of KisoSiyouRecord)

        ' 検索結果総件数
        Dim total_count As Integer

        '検索結果1件のみの場合の列ID格納用hiddenをクリア
        firstSend.Value = ""

        ' 取得件数を絞り込む場合、引数を追加してください
        dataArray = KisoSiyouSearchLogic.GetKisoSiyouSearchResult(TextCode.Value, _
                                                                TextName.Value, _
                                                                HiddenKameitenCode.Value, _
                                                                False, _
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
        For Each recData As KisoSiyouRecord In dataArray
            rowCount += 1
            Dim objTr As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdKisoSiyouCd As New HtmlTableCell
            Dim objTdKisoSiyouNm As New HtmlTableCell
            Dim objTdTodouhukenMei As New HtmlTableCell
            Dim objTdKisoSiyouKana As New HtmlTableCell

            objIptRtnHiddn.ID = "returnHidden" & rowCount
            objIptRtnHiddn.Value = recData.KsSiyouNo & EarthConst.SEP_STRING & recData.KsSiyou
            objTdReturn.Controls.Add(objIptRtnHiddn)
            objTdReturn.Attributes("class") = "searchReturnValues"

            objTdKisoSiyouCd.InnerHtml = cl.GetDisplayString(recData.KsSiyouNo, EarthConst.HANKAKU_SPACE)
            objTdKisoSiyouNm.InnerHtml = cl.GetDisplayString(recData.KsSiyou, EarthConst.HANKAKU_SPACE)
            'objTdTodouhukenMei.InnerHtml = recData.KameitenCd
            'objTdKisoSiyouKana.InnerHtml = recData.TenmeiKana1

            objTr.ID = "resultTr_" & rowCount
            If rowCount = 1 Then
                objTr.Attributes("tabindex") = 0
                objTr.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                objTr.Attributes("tabindex") = -1
            End If
            objTr.Controls.Add(objTdReturn)
            objTr.Controls.Add(objTdKisoSiyouCd)
            objTr.Controls.Add(objTdKisoSiyouNm)
            'objTr.Controls.Add(objTdTodouhukenMei)
            'objTr.Controls.Add(objTdKisoSiyouKana)

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