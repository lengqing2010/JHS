Imports System.Web.SessionState

Partial Public Class EarthError
    Inherits System.Web.UI.Page

#Region "プロパティ"
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <remarks></remarks>
    Private exException As System.Exception
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <value></value>
    ''' <returns>コントロールの表示モード</returns>
    ''' <remarks>商品の種類により画面の表示を変更します</remarks>
    Public Property EarthException() As System.Exception
        Get
            Return exException
        End Get
        Set(ByVal value As System.Exception)
            exException = value
        End Set
    End Property
#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SpanErrTime.InnerText = "エラー発生日時：" & DateTime.Now
        SpanErrPath.InnerText = "エラー発生ページ：" & Request("aspxerrorpath")

        If Request("code") IsNot Nothing Then
            Dim strCode As String = Request("code")
            Dim strCodeMess As String = String.Empty
            Select Case strCode
                Case "403"
                    strCodeMess = "ページを表示できません。アクセスが許可されていない可能性があります。"
                Case "404"
                    strCodeMess = "ページが見つかりません。アドレスを確認してください。"
            End Select
            SpanErrMess.InnerText = strCodeMess
            SpanErrPath.InnerText = "エラーコード：" & Request("code")
        End If

        EarthException = Context.Items("earthLastError")
        If EarthException IsNot Nothing Then
            PreErrMess.InnerHtml = "エラーメッセージ：" & EarthException.ToString()
        ElseIf Server.GetLastError IsNot Nothing Then
            PreErrMess.InnerHtml = "Error Messages：" & Server.GetLastError.ToString()
        End If

    End Sub

End Class