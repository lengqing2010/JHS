
Partial Public Class SearchTyoufuku
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            If Request("sendSearchTerms") <> "" Then
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)

                parmKubun.Value = IIf(arrSearchTerm(0) <> String.Empty, Left(arrSearchTerm(0), 1), arrSearchTerm(0))      '親画面からPOSTされた情報1 ：区分
                parmHosyousyoNo.Value = arrSearchTerm(1) '親画面からPOSTされた情報2 ：保証書NO (空白の場合もあり)
                parmSeshuNm.Value = arrSearchTerm(2)    '親画面からPOSTされた情報3 ：施主名
                parmJyuusho1.Value = arrSearchTerm(3)   '親画面からPOSTされた情報4 ：物件住所１
                parmJyuusho2.Value = arrSearchTerm(4)   '親画面からPOSTされた情報5 ：物件住所２

                If parmSeshuNm.Value = "" And parmJyuusho1.Value = "" And parmJyuusho2.Value = "" Then
                    '重複チェックに必要な情報が何も渡っていない
                Else
                    '重複結果表示処理
                    createResultTable()
                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' 重複結果表示処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub createResultTable()
        Dim logic As New JibanLogic

        Dim list As List(Of TyoufukuRecord)
        list = logic.GetTyouhukuRecords(parmKubun.Value, parmHosyousyoNo.Value, parmSeshuNm.Value, parmJyuusho1.Value, parmJyuusho2.Value)

        ' 取得データを確認する
        Dim rowCount As Integer = 0
        For Each recData As TyoufukuRecord In list

            rowCount += 1
            Dim objTr As New HtmlTableRow
            Dim objTdOtherWin As New HtmlTableCell
            Dim objImgOtherWin As New HtmlImage
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdHaki As New HtmlTableCell
            Dim objTdKubun As New HtmlTableCell
            Dim objTdHoshoushoNo As New HtmlTableCell
            Dim objTdSeshuNm As New HtmlTableCell
            Dim objTdJyuusho1 As New HtmlTableCell
            Dim objTdJyuusho2 As New HtmlTableCell

            objImgOtherWin.Src = "images/otherWin.gif"
            objImgOtherWin.Alt = "別ウィンドウで開く"

            objIptRtnHiddn.ID = "returnHidden" & rowCount
            objIptRtnHiddn.Value = recData.Kubun & EarthConst.SEP_STRING & recData.HosyousyoNo
            objTdOtherWin.Attributes("onclick") = "returnSelectValueOtherWin(this);"
            objTdOtherWin.Style("cursor") = "hand;"
            objTdOtherWin.Controls.Add(objIptRtnHiddn)
            objTdOtherWin.Controls.Add(objImgOtherWin)

            objTdHaki.InnerHtml = recData.HakiSyubetu & EarthConst.HANKAKU_SPACE
            objTdKubun.InnerHtml = recData.Kubun & EarthConst.HANKAKU_SPACE
            objTdHoshoushoNo.InnerHtml = recData.HosyousyoNo & EarthConst.HANKAKU_SPACE
            objTdSeshuNm.InnerHtml = recData.Sesyumei & EarthConst.HANKAKU_SPACE
            objTdJyuusho1.InnerHtml = recData.Jyuusyo1 & EarthConst.HANKAKU_SPACE
            objTdJyuusho2.InnerHtml = recData.Jyuusyo2 & EarthConst.HANKAKU_SPACE

            objTr.ID = "resultTr_" & rowCount
            If rowCount = 1 Then
                objTr.Attributes("tabindex") = 0
                objTr.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                objTr.Attributes("tabindex") = -1
            End If
            objTr.Controls.Add(objTdOtherWin)
            objTr.Controls.Add(objTdHaki)
            objTr.Controls.Add(objTdKubun)
            objTr.Controls.Add(objTdHoshoushoNo)
            objTr.Controls.Add(objTdSeshuNm)
            objTr.Controls.Add(objTdJyuusho1)
            objTr.Controls.Add(objTdJyuusho2)

            searchGrid.Controls.Add(objTr)

        Next

    End Sub

    Protected Sub returnBtn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles returnBtn.ServerClick

    End Sub
End Class