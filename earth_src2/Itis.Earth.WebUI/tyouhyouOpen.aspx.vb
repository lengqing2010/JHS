Imports Itis.Earth.BizLogic
Partial Public Class tyouhyouOpen
    Inherits System.Web.UI.Page
    Private Const SEP_STRING As String = "$$$"
    Private strUrl As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Request("fcwUrl") <> "" Then
                Dim arrSearchTerm() As String = Split(Request("fcwUrl"), SEP_STRING)
                strUrl = arrSearchTerm(0)
                'モール展開
                ViewState("moru") = arrSearchTerm(1)
                ViewState("moruEnable") = arrSearchTerm(2)
                '区分
                ViewState("kbn") = arrSearchTerm(3)
                '保証書NO
                ViewState("bukkenNo") = arrSearchTerm(4)
                '作成回数
                ViewState("mitumorisyoSakuseiKaisuu") = arrSearchTerm(5)
            End If

            Me.hiddenIframe.Attributes.Add("src", strUrl)

            ClientScript.RegisterClientScriptBlock(Page.GetType(), "setCopy", "<script type =""text/javascript"" > function window.onload(){ document.getElementById('" & Me.btnTest.ClientID & "').click();}</script>")
        End If
    End Sub

    Private Sub btnTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTest.Click

        '調査見積書_命名   [区分]＋[保証書No]＋[-0-]+[調査見積書000]+[‐作成回数]
        '帳票サーバーで帳票の保存PATH
        Dim TyouhyouPath As String = System.Configuration.ConfigurationManager.AppSettings("TyouhyouPath").ToString
        TyouhyouPath = TyouhyouPath & ViewState("kbn") & ViewState("bukkenNo") & "-0-" & "調査見積書000" & "-" & ViewState("mitumorisyoSakuseiKaisuu") & ".pdf"

        '格納サーバーAで帳票の保存PATH
        Dim TyouhyouServerAPath As String = System.Configuration.ConfigurationManager.AppSettings("TyouhyouServerAPath").ToString
        TyouhyouServerAPath = TyouhyouServerAPath & ViewState("kbn") & ViewState("bukkenNo") & "調査見積書.pdf"

        '格納サーバーAにCopy
        System.IO.File.Copy(TyouhyouPath, TyouhyouServerAPath, True)

        'するを選択する＆Enable = True
        'AndAlso ViewState("moruEnable") = True 2013/12/20削除する
        If ViewState("moru") = True Then
            '格納サーバーBで帳票の保存PATH
            Dim TyouhyouServerBPath As String = System.Configuration.ConfigurationManager.AppSettings("TyouhyouServerBPath").ToString
            TyouhyouServerBPath = TyouhyouServerBPath & ViewState("kbn") & ViewState("bukkenNo") & "-0-" & "調査見積書000" & "-" & ViewState("mitumorisyoSakuseiKaisuu") & ".pdf"

            '格納サーバーBにCopy
            System.IO.File.Copy(TyouhyouPath, TyouhyouServerBPath, True)
        End If

        '帳票をOpenする
        Call Me.GetFile(TyouhyouPath)

    End Sub

    Private Sub GetFile(ByVal strFileName As String)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script type =""text/javascript"" >")
            .AppendLine("function OpenFile(){")
            .AppendLine("   document.getElementById('" & Me.file.ClientID & "').href = '" & "file:" & strFileName.Replace("\", "/") & "';")
            .AppendLine("   document.getElementById('" & Me.file.ClientID & "').click();")
            .AppendLine("}")
            .AppendLine("setTimeout('OpenFile()',1000);")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

End Class