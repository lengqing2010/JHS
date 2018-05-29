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
                '���[���W�J
                ViewState("moru") = arrSearchTerm(1)
                ViewState("moruEnable") = arrSearchTerm(2)
                '�敪
                ViewState("kbn") = arrSearchTerm(3)
                '�ۏ؏�NO
                ViewState("bukkenNo") = arrSearchTerm(4)
                '�쐬��
                ViewState("mitumorisyoSakuseiKaisuu") = arrSearchTerm(5)
            End If

            Me.hiddenIframe.Attributes.Add("src", strUrl)

            ClientScript.RegisterClientScriptBlock(Page.GetType(), "setCopy", "<script type =""text/javascript"" > function window.onload(){ document.getElementById('" & Me.btnTest.ClientID & "').click();}</script>")
        End If
    End Sub

    Private Sub btnTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTest.Click

        '�������Ϗ�_����   [�敪]�{[�ۏ؏�No]�{[-0-]+[�������Ϗ�000]+[�]�쐬��]
        '���[�T�[�o�[�Œ��[�̕ۑ�PATH
        Dim TyouhyouPath As String = System.Configuration.ConfigurationManager.AppSettings("TyouhyouPath").ToString
        TyouhyouPath = TyouhyouPath & ViewState("kbn") & ViewState("bukkenNo") & "-0-" & "�������Ϗ�000" & "-" & ViewState("mitumorisyoSakuseiKaisuu") & ".pdf"

        '�i�[�T�[�o�[A�Œ��[�̕ۑ�PATH
        Dim TyouhyouServerAPath As String = System.Configuration.ConfigurationManager.AppSettings("TyouhyouServerAPath").ToString
        TyouhyouServerAPath = TyouhyouServerAPath & ViewState("kbn") & ViewState("bukkenNo") & "�������Ϗ�.pdf"

        '�i�[�T�[�o�[A��Copy
        System.IO.File.Copy(TyouhyouPath, TyouhyouServerAPath, True)

        '�����I�����違Enable = True
        'AndAlso ViewState("moruEnable") = True 2013/12/20�폜����
        If ViewState("moru") = True Then
            '�i�[�T�[�o�[B�Œ��[�̕ۑ�PATH
            Dim TyouhyouServerBPath As String = System.Configuration.ConfigurationManager.AppSettings("TyouhyouServerBPath").ToString
            TyouhyouServerBPath = TyouhyouServerBPath & ViewState("kbn") & ViewState("bukkenNo") & "-0-" & "�������Ϗ�000" & "-" & ViewState("mitumorisyoSakuseiKaisuu") & ".pdf"

            '�i�[�T�[�o�[B��Copy
            System.IO.File.Copy(TyouhyouPath, TyouhyouServerBPath, True)
        End If

        '���[��Open����
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