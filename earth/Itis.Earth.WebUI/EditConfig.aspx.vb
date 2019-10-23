Imports System.Configuration
Imports System.Web.Configuration

Partial Public Class EditConfigForm1
    Inherits System.Web.UI.Page

    Private userInfo As New LoginUserInfo

    ''' <summary>
    ''' ページロードイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            ' ユーザー基本認証
            Dim ninsyou As New Ninsyou()
            If (Not ninsyou.IsUserLogon()) Then
                '認証失敗
                ninsyou.EndResponseWithAccessDeny()
            End If

            'ユーザー権限によりリンク状態を切り替え
            Dim loginLogic As New LoginUserLogic

            If loginLogic.MakeUserInfo(ninsyou.GetUserID(), userInfo) Then

            Else
                ' ユーザーアカウント情報取得不可の場合Nothingをセット
                userInfo = Nothing
                Debug.WriteLine("ログインユーザー情報の取得に失敗しました")
            End If

            If userInfo Is Nothing Then
                'ログイン情報が無い場合、メイン画面に飛ばす
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            End If

            'システム管理権限が無い場合、参照のみとする
            If userInfo.SystemKanrisyaKengen <> "-1" Then
                TextSmtpAddress.ReadOnly = True
                TextSmtpAddress.BorderStyle = BorderStyle.None
                TextMailFromAddress.ReadOnly = True
                TextMailFromAddress.BorderStyle = BorderStyle.None
                LabelMessage.ForeColor = Drawing.Color.Blue
                LabelMessage.Text = "システム管理者権限を持つユーザーのみ編集可能です．"
                ButtonReload.Visible = False
                ButtonUpdate.Visible = False
            Else
                LabelMessage.ForeColor = Drawing.Color.Blue
                LabelMessage.Text = "★注意★ WebConfig.xmlファイルを編集します．更新後は編集前の状態に戻せません．"
            End If

            ReloadAppSettings()
        End If
    End Sub

    ''' <summary>
    ''' 再読み込み
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonReload_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReload.ServerClick
        ReloadAppSettings()
    End Sub

    ''' <summary>
    ''' 更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonUpdate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUpdate.ServerClick

        Dim config As Configuration = WebConfigurationManager.OpenWebConfiguration("/jhs_earth")

        config.AppSettings.Settings.Remove("MailServerName")
        config.AppSettings.Settings.Add("MailServerName", TextSmtpAddress.Text)

        config.AppSettings.Settings.Remove("MailFromAddress")
        config.AppSettings.Settings.Add("MailFromAddress", TextMailFromAddress.Text)

        config.Save()

    End Sub

    ''' <summary>
    ''' 設定ファイルの再読み込みを行う
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReloadAppSettings()

        Dim config As Configuration = WebConfigurationManager.OpenWebConfiguration("/jhs_earth")

        TextSmtpAddress.Text = config.AppSettings.Settings("MailServerName").Value
        TextMailFromAddress.Text = config.AppSettings.Settings("MailFromAddress").Value

    End Sub

End Class