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
                'ƒ‚[ƒ‹“WŠJ
                ViewState("moru") = arrSearchTerm(1)
                ViewState("moruEnable") = arrSearchTerm(2)
                '‹æ•ª
                ViewState("kbn") = arrSearchTerm(3)
                '•ÛØ‘NO
                ViewState("bukkenNo") = arrSearchTerm(4)
                'ì¬‰ñ”
                ViewState("mitumorisyoSakuseiKaisuu") = arrSearchTerm(5)
            End If

            Me.hiddenIframe.Attributes.Add("src", strUrl)

            ClientScript.RegisterClientScriptBlock(Page.GetType(), "setCopy", "<script type =""text/javascript"" > function window.onload(){ document.getElementById('" & Me.btnTest.ClientID & "').click();}</script>")
        End If
    End Sub

    Private Sub btnTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTest.Click

        '’²¸Œ©Ï‘_–½–¼   [‹æ•ª]{[•ÛØ‘No]{[-0-]+[’²¸Œ©Ï‘000]+[]ì¬‰ñ”]
        '’ •[ƒT[ƒo[‚Å’ •[‚Ì•Û‘¶PATH
        Dim TyouhyouPath As String = System.Configuration.ConfigurationManager.AppSettings("TyouhyouPath").ToString
        TyouhyouPath = TyouhyouPath & ViewState("kbn") & ViewState("bukkenNo") & "-0-" & "’²¸Œ©Ï‘000" & "-" & ViewState("mitumorisyoSakuseiKaisuu") & ".pdf"

        'Ši”[ƒT[ƒo[A‚Å’ •[‚Ì•Û‘¶PATH
        Dim TyouhyouServerAPath As String = System.Configuration.ConfigurationManager.AppSettings("TyouhyouServerAPath").ToString
        TyouhyouServerAPath = TyouhyouServerAPath & ViewState("kbn") & ViewState("bukkenNo") & "’²¸Œ©Ï‘.pdf"

        'Ši”[ƒT[ƒo[A‚ÉCopy
        System.IO.File.Copy(TyouhyouPath, TyouhyouServerAPath, True)

        '‚·‚é‚ğ‘I‘ğ‚·‚é•Enable = True
        'AndAlso ViewState("moruEnable") = True 2013/12/20íœ‚·‚é
        If ViewState("moru") = True Then
            'Ši”[ƒT[ƒo[B‚Å’ •[‚Ì•Û‘¶PATH
            Dim TyouhyouServerBPath As String = System.Configuration.ConfigurationManager.AppSettings("TyouhyouServerBPath").ToString
            TyouhyouServerBPath = TyouhyouServerBPath & ViewState("kbn") & ViewState("bukkenNo") & "-0-" & "’²¸Œ©Ï‘000" & "-" & ViewState("mitumorisyoSakuseiKaisuu") & ".pdf"

            'Ši”[ƒT[ƒo[B‚ÉCopy
            System.IO.File.Copy(TyouhyouPath, TyouhyouServerBPath, True)
        End If

        '’ •[‚ğOpen‚·‚é
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