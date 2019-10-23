Imports System.Configuration
Imports System.Web.Configuration

Partial Public Class EditConfigForm1
    Inherits System.Web.UI.Page

    Private userInfo As New LoginUserInfo

    ''' <summary>
    ''' �y�[�W���[�h�C�x���g�n���h��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            ' ���[�U�[��{�F��
            Dim ninsyou As New Ninsyou()
            If (Not ninsyou.IsUserLogon()) Then
                '�F�؎��s
                ninsyou.EndResponseWithAccessDeny()
            End If

            '���[�U�[�����ɂ�胊���N��Ԃ�؂�ւ�
            Dim loginLogic As New LoginUserLogic

            If loginLogic.MakeUserInfo(ninsyou.GetUserID(), userInfo) Then

            Else
                ' ���[�U�[�A�J�E���g���擾�s�̏ꍇNothing���Z�b�g
                userInfo = Nothing
                Debug.WriteLine("���O�C�����[�U�[���̎擾�Ɏ��s���܂���")
            End If

            If userInfo Is Nothing Then
                '���O�C����񂪖����ꍇ�A���C����ʂɔ�΂�
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            End If

            '�V�X�e���Ǘ������������ꍇ�A�Q�Ƃ݂̂Ƃ���
            If userInfo.SystemKanrisyaKengen <> "-1" Then
                TextSmtpAddress.ReadOnly = True
                TextSmtpAddress.BorderStyle = BorderStyle.None
                TextMailFromAddress.ReadOnly = True
                TextMailFromAddress.BorderStyle = BorderStyle.None
                LabelMessage.ForeColor = Drawing.Color.Blue
                LabelMessage.Text = "�V�X�e���Ǘ��Ҍ����������[�U�[�̂ݕҏW�\�ł��D"
                ButtonReload.Visible = False
                ButtonUpdate.Visible = False
            Else
                LabelMessage.ForeColor = Drawing.Color.Blue
                LabelMessage.Text = "�����Ӂ� WebConfig.xml�t�@�C����ҏW���܂��D�X�V��͕ҏW�O�̏�Ԃɖ߂��܂���D"
            End If

            ReloadAppSettings()
        End If
    End Sub

    ''' <summary>
    ''' �ēǂݍ���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonReload_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReload.ServerClick
        ReloadAppSettings()
    End Sub

    ''' <summary>
    ''' �X�V
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
    ''' �ݒ�t�@�C���̍ēǂݍ��݂��s��
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReloadAppSettings()

        Dim config As Configuration = WebConfigurationManager.OpenWebConfiguration("/jhs_earth")

        TextSmtpAddress.Text = config.AppSettings.Settings("MailServerName").Value
        TextMailFromAddress.Text = config.AppSettings.Settings("MailFromAddress").Value

    End Sub

End Class