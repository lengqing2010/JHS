
Partial Public Class SearchTyousakaisya
    Inherits System.Web.UI.Page

    Dim earthEnum As New EarthEnum
    Dim cl As New CommonLogic

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            ' 調査会社コードが空の場合は名称にフォーカス
            tyousakaisyaKanaNm.Focus()

            If Request("sendSearchTerms") <> "" Then
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
                tyousakaisyaCd.Value = arrSearchTerm(0) '親画面からPOSTされた情報1 ：調査会社コード
                KameitenCd.Value = arrSearchTerm(1)     '親画面からPOSTされた情報2 ：加盟店コード
                If arrSearchTerm.Length >= 3 Then
                    HidGamenType.Value = arrSearchTerm(2) '親画面からPOSTされた情報3(任意) ：画面タイプ(1=調査会社(デフォルト)、2=仕入先、3=支払先)
                End If
                If HidGamenType.Value = String.Empty Then
                    '画面タイプ未設定の場合、デフォルト画面タイプをセット(1=調査会社(デフォルト))
                    HidGamenType.Value = CStr(earthEnum.EnumTyousakaisyaKensakuType.TYOUSAKAISYA)
                End If
                returnTargetIds.Value = Request("returnTargetIds")  '親画面からPOSTされた戻り値セット先ID郡
                afterEventBtnId.Value = Request("afterEventBtnId")  '値セット後に押下する、親画面のボタンID

                If tyousakaisyaCd.Value.Trim() <> "" Then
                    ' 調査会社コードにフォーカス
                    tyousakaisyaCd.Focus()
                End If

                '画面タイトル、表題を変更
                Dim strTitle As String = Me.Title   'タイトルベース文字列
                Dim strHyoudai As String = ThGamenTitle.InnerText   '表題ベース文字列
                Dim strTypeTitle As String = String.Empty   'タイプ別文字列格納用
                Select Case CInt(HidGamenType.Value)
                    Case earthEnum.EnumTyousakaisyaKensakuType.TYOUSAKAISYA

                    Case earthEnum.EnumTyousakaisyaKensakuType.SIIRESAKI
                        strTypeTitle = "(仕入先)"
                    Case earthEnum.EnumTyousakaisyaKensakuType.SIHARAISAKI
                        strTypeTitle = "(支払先)"
                    Case earthEnum.EnumTyousakaisyaKensakuType.KOUJIKAISYA
                        strTypeTitle = "(工事会社)"
                End Select
                ThGamenTitle.InnerText = String.Format(strHyoudai, strTypeTitle)
                Me.Title = "EARTH " & ThGamenTitle.InnerText


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

        '調査会社マスタ検索実行
        dataArray = searchLogic.GetTyousakaisyaSearchResult(tyousakaisyaCd.Value, _
                                                            "", _
                                                            "", _
                                                            tyousakaisyaKanaNm.Value, _
                                                            True, _
                                                            KameitenCd.Value, _
                                                            CInt(HidGamenType.Value))

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

        '結果一覧出力
        Dim rowCount As Integer = 0
        For Each recData As TyousakaisyaSearchRecord In dataArray
            rowCount += 1

            Dim tmpKaisyaMei As String = String.Empty
            Dim tmpKaisyaMeiKana As String = String.Empty
            Dim objTr As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdTysKaiCd As New HtmlTableCell
            Dim objTdTysKaiNm As New HtmlTableCell
            Dim objTdTysKaiKana As New HtmlTableCell

            Dim ret_msg As String = IIf(recData.KahiKbn = 9, EarthConst.TYOUSA_KAISYA_NG, "")

            '検索タイプ別に、適した会社名をテンポラリにセット
            Select Case CInt(HidGamenType.Value)
                Case earthEnum.EnumTyousakaisyaKensakuType.SIHARAISAKI
                    '支払先検索の場合、請求先支払先名をセット
                    tmpKaisyaMei = recData.SeikyuuSakiSiharaiMei
                    tmpKaisyaMeiKana = recData.SeikyuuSakiSiharaiMeiKana
                Case Else
                    '上記以外の場合、調査会社名をセット
                    tmpKaisyaMei = recData.TysKaisyaMei
                    tmpKaisyaMeiKana = recData.TysKaisyaMeiKana
            End Select

            objIptRtnHiddn.ID = "returnHidden" & rowCount
            objIptRtnHiddn.Value = recData.TysKaisyaCd & recData.JigyousyoCd & EarthConst.SEP_STRING & _
                                   tmpKaisyaMei & EarthConst.SEP_STRING & ret_msg
            objTdReturn.Controls.Add(objIptRtnHiddn)
            objTdReturn.Attributes("class") = "searchReturnValues"

            objTdTysKaiCd.InnerHtml = cl.GetDisplayString(recData.TysKaisyaCd & recData.JigyousyoCd, EarthConst.HANKAKU_SPACE)
            objTdTysKaiNm.InnerHtml = cl.GetDisplayString(tmpKaisyaMei, EarthConst.HANKAKU_SPACE)
            objTdTysKaiKana.InnerHtml = cl.GetDisplayString(tmpKaisyaMeiKana, EarthConst.HANKAKU_SPACE)

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
        tyousakaisyaKanaNm.Focus()
    End Sub

End Class