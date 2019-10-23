
Partial Public Class BuilderInfo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
            kameitenCd.Value = arrSearchTerm(0)     '親画面からPOSTされた情報1 ：加盟店コード

            '検索を行い、結果を画面表示する
            setTable()

        End If
    End Sub

    ''' <summary>
    ''' 検索を行い、結果を画面表示する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setTable()

        ' ビルダー情報検索実行
        Dim jibanLogic As New JibanLogic
        Dim dataArray As New List(Of BuilderInfoRecord)

        dataArray = jibanLogic.GetBuilderInfo(kameitenCd.Value)
        resultCount.InnerHtml = dataArray.Count

        Dim rowCount As Integer = 0
        For Each recData As BuilderInfoRecord In dataArray
            rowCount += 1

            Dim objTr As New HtmlTableRow
            Dim objTdReturn As New HtmlTableCell
            Dim objIptRtnHiddn As New HtmlInputHidden
            Dim objTdTyuuijikouSyubetu As New HtmlTableCell
            Dim objTdNyuuryokuDate As New HtmlTableCell
            Dim objTdUketukesyaMei As New HtmlTableCell
            Dim objTdNaiyou As New HtmlTableCell

            objIptRtnHiddn.ID = "returnHidden" & rowCount
            objIptRtnHiddn.Value = recData.KameitenCd & EarthConst.SEP_STRING & recData.NyuuryokuNo
            objTdReturn.Controls.Add(objIptRtnHiddn)
            objTdReturn.Attributes("class") = "searchReturnValues"

            Dim defStr As String = "&nbsp;"
            objTdTyuuijikouSyubetu.InnerHtml = dispStr(recData.TyuuijikouSyubetu, defStr)
            objTdNyuuryokuDate.InnerHtml = dispStr(recData.NyuuryokuDate, defStr)
            objTdUketukesyaMei.InnerHtml = dispStr(recData.UketukesyaMei, defStr)
            objTdNaiyou.InnerHtml = dispStr(recData.Naiyou, defStr)

            objTdNaiyou.Style("width") = "400px"

            objTr.ID = "resultTr_" & rowCount
            objTr.Controls.Add(objTdReturn)
            objTr.Controls.Add(objTdTyuuijikouSyubetu)
            objTr.Controls.Add(objTdNyuuryokuDate)
            objTr.Controls.Add(objTdUketukesyaMei)
            objTr.Controls.Add(objTdNaiyou)

            searchGrid.Controls.Add(objTr)

        Next

    End Sub

    ''' <summary>
    ''' 画面表示用文字列に変換するファンクション（オーバーライド）
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function dispStr(ByVal obj As Object, Optional ByVal str As String = "") As String

        Return CommonLogic.Instance.GetDisplayString(obj, str)

    End Function

End Class