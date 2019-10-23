
Partial Public Class SearchSyouhin
    Inherits System.Web.UI.Page

    Dim cl As New CommonLogic

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            ' 商品コードが空の場合は名称にフォーカス
            search_shouhinKanaNm.Focus()

            If Request("sendSearchTerms") <> "" Then
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
                search_shouhinCd.Value = arrSearchTerm(0)                   '親画面からPOSTされた情報1 ：商品コード
                search_shouhin23.Value = arrSearchTerm(1).Split("_")(0)     '親画面からPOSTされた情報2 ：商品2or3

                returnTargetIds.Value = Request("returnTargetIds")  '親画面からPOSTされた戻り値セット先ID郡
                afterEventBtnId.Value = Request("afterEventBtnId")  '値セット後に押下する、親画面のボタンID
            End If

            If search_shouhinCd.Value.Trim() <> "" Then
                ' 商品コードにフォーカス
                search_shouhinCd.Focus()
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

        Dim total_row As Integer
        Dim intSyouhinKbn As EarthEnum.EnumSyouhinKubun

        '検索結果1件のみの場合の列ID格納用hiddenをクリア
        firstSend.Value = ""
        Select Case search_shouhin23.Value
            Case "1"
                intSyouhinKbn = EarthEnum.EnumSyouhinKubun.Syouhin1
            Case "2"
                intSyouhinKbn = EarthEnum.EnumSyouhinKubun.Syouhin2_110
            Case "3"
                intSyouhinKbn = EarthEnum.EnumSyouhinKubun.Syouhin3
            Case EarthConst.SOUKO_CD_SYOUHIN_4
                intSyouhinKbn = EarthEnum.EnumSyouhinKubun.Syouhin4
            Case EarthConst.SOUKO_CD_FC_GAI_HANSOKUHIN
                intSyouhinKbn = EarthEnum.EnumSyouhinKubun.HansokuNotFc
            Case EarthConst.SOUKO_CD_FC_HANSOKUHIN
                intSyouhinKbn = EarthEnum.EnumSyouhinKubun.HansokuFc
            Case Else
                intSyouhinKbn = EarthEnum.EnumSyouhinKubun.AllSyouhin
        End Select
        Dim list As List(Of Syouhin23Record) = getSyouhinInfo(search_shouhinCd.Value & Chr(37), _
                                                              search_shouhinKanaNm.Value, _
                                                              intSyouhinKbn, _
                                                              total_row)
        ' 検索結果ゼロ件の場合、メッセージを表示
        If total_row = 0 Then
            Dim tmpScript As String = "alert('" & Messages.MSG020E & "');"
            ScriptManager.RegisterClientScriptBlock(searchGrid, searchGrid.GetType(), "err", tmpScript, True)
        End If

        '表示件数制限処理
        Dim displayCount As String = ""
        displayCount = total_row
        resultCount.Style.Remove("color")
        If maxSearchCount.Value <> "max" Then
            If Val(maxSearchCount.Value) < total_row Then
                resultCount.Style("color") = "red"
                displayCount = maxSearchCount.Value & " / " & CommonLogic.Instance.GetDisplayString(total_row)
            End If
        End If

        resultCount.InnerHtml = displayCount

        ' 行カウンタ
        Dim rowCount As Integer = 0
        Dim strTmpHosyouUmu As String = String.Empty

        ' 取得した商品情報を画面に表示
        For Each data As Syouhin23Record In list

            rowCount += 1
            Dim objTr As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdSyouhinCd As New HtmlTableCell
            Dim objTdSyouhinNm As New HtmlTableCell
            Dim objTdHosyouUmu As New HtmlTableCell
            strTmpHosyouUmu = String.Empty

            objIptRtnHiddn.ID = "returnHidden" & rowCount
            objIptRtnHiddn.Value = data.SyouhinCd & EarthConst.SEP_STRING & data.SoukoCd & EarthConst.SEP_STRING & data.SyouhinMei & EarthConst.SEP_STRING & data.SyouhinMei
            objTdReturn.Controls.Add(objIptRtnHiddn)
            objTdReturn.Attributes("class") = "searchReturnValues"

            objTdSyouhinCd.InnerHtml = cl.GetDisplayString(data.SyouhinCd, EarthConst.HANKAKU_SPACE)
            objTdSyouhinNm.InnerHtml = cl.GetDisplayString(data.SyouhinMei, EarthConst.HANKAKU_SPACE)
            strTmpHosyouUmu = cl.GetDisplayString(data.HosyouUmu)
            If strTmpHosyouUmu <> String.Empty Then
                If strTmpHosyouUmu = "1" Then
                    objTdHosyouUmu.InnerHtml = EarthConst.ARI
                ElseIf strTmpHosyouUmu = "0" Then
                    objTdHosyouUmu.InnerHtml = EarthConst.NASI
                Else
                    objTdHosyouUmu.InnerHtml = EarthConst.HANKAKU_SPACE
                End If
            Else
                objTdHosyouUmu.InnerHtml = EarthConst.HANKAKU_SPACE
            End If

            objTr.ID = "resultTr_" & rowCount
            If rowCount = 1 Then
                objTr.Attributes("tabindex") = 0
                objTr.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                objTr.Attributes("tabindex") = -1
            End If
            objTr.Controls.Add(objTdReturn)
            objTr.Controls.Add(objTdSyouhinCd)
            objTr.Controls.Add(objTdSyouhinNm)
            objTr.Controls.Add(objTdHosyouUmu)

            searchGrid.Controls.Add(objTr)

            If total_row = 1 Then
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

        search_shouhinKanaNm.Focus()

    End Sub

    ''' <summary>
    ''' 商品情報を取得します
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strSyouhinNm">商品名</param>
    ''' <param name="intSyouhinKbn">商品区分種類</param>
    ''' <param name="all_row_count">検索結果全件数</param>
    ''' <param name="start_row">（任意）データ抽出時の開始行(1件目は1を指定)Default:1</param>
    ''' <param name="end_row">（任意）データ抽出時の終了行 Default:99999999</param>
    ''' <returns>Syouhin23Recordを格納したArrayList</returns>
    ''' <remarks>
    ''' <example>商品情報を取得して結果を設定するサンプルコード（商品２の場合）
    ''' <code>
    ''' Dim total_row As Integer<br/>
    ''' ' 商品２の情報を１件目から１００件目まで取得する場合
    ''' For Each data As Syouhin23Record IN getSyouhinInfo(True, total_row, 1, 100)<br/>
    '''     [商品コードの設定先] = data.SyouhinCd <br/>
    '''     [商品名の設定先] = data.SyouhinMei <br/>
    ''' Next <br/>
    ''' </code>
    ''' </example>
    ''' </remarks>
    Private Function getSyouhinInfo(ByVal strSyouhinCd As String, _
                                    ByVal strSyouhinNm As String, _
                                    ByVal intSyouhinKbn As EarthEnum.EnumSyouhinKubun, _
                                    ByRef all_row_count As Integer, _
                                    Optional ByVal start_row As Integer = 1, _
                                    Optional ByVal end_row As Integer = 99999999) As List(Of Syouhin23Record)
        Dim logic As New JibanLogic
        Dim list As List(Of Syouhin23Record)

        '商品タイプに沿った商品のレコードをすべて取得します
        list = logic.GetSyouhin23(strSyouhinCd, _
                                  strSyouhinNm, _
                                  intSyouhinKbn, _
                                  all_row_count, _
                                  Integer.MinValue, _
                                  "", _
                                  start_row, _
                                  end_row)
        Return list
    End Function

End Class