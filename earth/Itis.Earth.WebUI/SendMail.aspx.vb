Imports System.Configuration
Imports System.Web.Configuration

Partial Public Class SendMail
    Inherits System.Web.UI.Page

    ' 定数
    Private Const SEND_MESSAGE = "送信しました"
    Private Const SEND_HISSU_ERROR = "{0}は必須です"
    Private Const SYSTEM_KANRI = "本機能はシステム管理者のみ使用可能です．"

    Private userInfo As New LoginUserInfo

    ''' <summary>
    ''' ページロード時のイベント
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

            'システム管理権限が無い場合、メール送信を不可とする
            If userInfo.SystemKanrisyaKengen <> "-1" Then
                LabelMessage.ForeColor = Drawing.Color.Blue
                LabelMessage.Text = SYSTEM_KANRI
                ButtonSend.Visible = False
            End If


            TextFrom.Text = ConfigurationManager.AppSettings("MailFromAddress")
        End If
    End Sub

    ''' <summary>
    ''' メール送信
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSend.Click

        LabelMessage.Text = ""

        ' 入力チェック
        If HissuCheck() = False Then
            Return
        End If

        Dim mailHelper As New SendMailHelper

        LabelMessage.Text = mailHelper.SendMail(ConfigurationManager.AppSettings("MailServerName"), _
                            TextFrom.Text, _
                            TextTo.Text, _
                            "", _
                            "", _
                            TextSubject.Text, _
                            TextBody.Text, _
                            FileUpload)

        ' エラーがない場合、終了メッセージを表示する
        If LabelMessage.Text = "" Then
            LabelMessage.ForeColor = Drawing.Color.Blue
            LabelMessage.Text = SEND_MESSAGE
        Else
            LabelMessage.ForeColor = Drawing.Color.Red
        End If

    End Sub

    ''' <summary>
    ''' 入力必須チェック
    ''' </summary>
    ''' <returns>True:エラー無し False:エラー有り</returns>
    ''' <remarks></remarks>
    Private Function HissuCheck() As Boolean

        Dim ret As Boolean = True
        LabelMessage.ForeColor = Drawing.Color.Red

        ' 送信者アドレス（編集不可なのでありえない）
        If TextFrom.Text.Trim() = "" Then
            LabelMessage.Text = String.Format(SEND_HISSU_ERROR, TdFrom.InnerText)
            TextFrom.Focus()
            ret = False
        End If
        ' 送信先アドレス
        If TextTo.Text.Trim() = "" Then
            If LabelMessage.Text <> "" Then
                LabelMessage.Text = LabelMessage.Text & "</br>"
            Else
                TextTo.Focus()
            End If
            LabelMessage.Text = LabelMessage.Text & String.Format(SEND_HISSU_ERROR, TdTo.InnerText)
            ret = False
        End If
        ' 件名
        If TextSubject.Text.Trim() = "" Then
            If LabelMessage.Text <> "" Then
                LabelMessage.Text = LabelMessage.Text & "</br>"
            Else
                TextSubject.Focus()
            End If
            LabelMessage.Text = LabelMessage.Text & String.Format(SEND_HISSU_ERROR, TdSubject.InnerText)
            ret = False
        End If
        ' 本文
        If TextBody.Text.Trim() = "" Then
            If LabelMessage.Text <> "" Then
                LabelMessage.Text = LabelMessage.Text & "</br>"
            Else
                TextBody.Focus()
            End If
            LabelMessage.Text = LabelMessage.Text & String.Format(SEND_HISSU_ERROR, TdBody.InnerText)
            ret = False
        End If

        Return ret

    End Function

End Class