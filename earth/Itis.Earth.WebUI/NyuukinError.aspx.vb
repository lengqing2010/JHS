Partial Public Class NyuukinError
    Inherits System.Web.UI.Page

#Region "変数"
    Private user_info As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
#End Region

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス

        ' ユーザー基本認証
        jBn.UserAuth(user_info)
        If user_info Is Nothing Then
            'ログイン情報が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If
        '権限制御
        If user_info.KeiriGyoumuKengen <> -1 Then
            '権限が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        setTable()

    End Sub

    ''' <summary>
    ''' 検索を行い、結果を画面に表示する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setTable()
        Dim clsLogic As New NyuukinErrorLogic
        Dim nyuukinErrTable As DataTable
        Dim objTr As HtmlTableRow
        Dim objTdGyouNo As HtmlTableCell
        Dim objTdGroupCd As HtmlTableCell
        Dim objTdKokyakuCd As HtmlTableCell
        Dim objTdTekiyou As HtmlTableCell
        Dim objTdNyuukinGaku As HtmlTableCell
        Dim objTdSyouhinCd As HtmlTableCell

        '1.エラー情報の取得
        nyuukinErrTable = clsLogic.GetNyuukinErrData(Request("nkn_kbn"), Request("edi"))
        'If Request("nkn_kbn") = 1 Then
        '    Me.lblTitle.Text = "入金重複確認"
        '    Me.Title = "EARTH 入金重複確認"
        'End If
        '表示件数表示処理
        Me.resultCount.InnerText = nyuukinErrTable.Rows.Count
        Me.textTorikomiDate.Value = nyuukinErrTable.Rows(0).Item("syori_datetime")
        Me.textTorikomiFileName.Value = Request("file")

        'グリッドのクリア
        Me.errorGrid.Controls.Clear()
        'エラー情報の出力
        For intCnt As Integer = 0 To nyuukinErrTable.Rows.Count - 1
            'コントロールのインスタンス生成
            objTr = New HtmlTableRow
            objTdGyouNo = New HtmlTableCell
            objTdGroupCd = New HtmlTableCell
            objTdKokyakuCd = New HtmlTableCell
            objTdTekiyou = New HtmlTableCell
            objTdNyuukinGaku = New HtmlTableCell
            objTdSyouhinCd = New HtmlTableCell
            objTdNyuukinGaku.Align = "right"
            'セルへ取得した値のセット
            With nyuukinErrTable.Rows(intCnt)
                objTdGyouNo.InnerText = .Item("gyou_no")
                objTdGroupCd.InnerText = .Item("group_cd")
                objTdKokyakuCd.InnerText = .Item("kokyaku_cd")
                If IsDBNull(.Item("tekiyou")) OrElse .Item("tekiyou").ToString.Trim = String.Empty Then
                    objTdTekiyou.InnerText = "　"
                Else
                    objTdTekiyou.InnerText = .Item("tekiyou")
                End If
                objTdNyuukinGaku.InnerText = Format(.Item("nyuukin_gaku"), EarthConst.FORMAT_KINGAKU_3)
                If IsDBNull(.Item("syouhin_cd")) OrElse .Item("syouhin_cd").ToString.Trim = String.Empty Then
                    objTdSyouhinCd.InnerText = "　"
                Else
                    objTdSyouhinCd.InnerText = .Item("syouhin_cd")
                End If
            End With
            objTr.ID = "resultTr_" & intCnt + 1
            '行にセルの追加
            With objTr.Controls
                .Add(objTdGyouNo)
                .Add(objTdGroupCd)
                .Add(objTdKokyakuCd)
                .Add(objTdTekiyou)
                .Add(objTdNyuukinGaku)
                .Add(objTdSyouhinCd)
            End With
            objTr.BorderColor = "black"

            'グリッドに行の追加
            errorGrid.Controls.Add(objTr)
        Next
    End Sub
End Class