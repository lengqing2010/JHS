Imports System.Configuration
Imports System.Web.Configuration

Partial Public Class SendMail
    Inherits System.Web.UI.Page

    ' �萔
    Private Const SEND_MESSAGE = "���M���܂���"
    Private Const SEND_HISSU_ERROR = "{0}�͕K�{�ł�"
    Private Const SYSTEM_KANRI = "�{�@�\�̓V�X�e���Ǘ��҂̂ݎg�p�\�ł��D"

    Private userInfo As New LoginUserInfo

    ''' <summary>
    ''' �y�[�W���[�h���̃C�x���g
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

            '�V�X�e���Ǘ������������ꍇ�A���[�����M��s�Ƃ���
            If userInfo.SystemKanrisyaKengen <> "-1" Then
                LabelMessage.ForeColor = Drawing.Color.Blue
                LabelMessage.Text = SYSTEM_KANRI
                ButtonSend.Visible = False
            End If


            TextFrom.Text = ConfigurationManager.AppSettings("MailFromAddress")
        End If
    End Sub

    ''' <summary>
    ''' ���[�����M
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSend.Click

        LabelMessage.Text = ""

        ' ���̓`�F�b�N
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

        ' �G���[���Ȃ��ꍇ�A�I�����b�Z�[�W��\������
        If LabelMessage.Text = "" Then
            LabelMessage.ForeColor = Drawing.Color.Blue
            LabelMessage.Text = SEND_MESSAGE
        Else
            LabelMessage.ForeColor = Drawing.Color.Red
        End If

    End Sub

    ''' <summary>
    ''' ���͕K�{�`�F�b�N
    ''' </summary>
    ''' <returns>True:�G���[���� False:�G���[�L��</returns>
    ''' <remarks></remarks>
    Private Function HissuCheck() As Boolean

        Dim ret As Boolean = True
        LabelMessage.ForeColor = Drawing.Color.Red

        ' ���M�҃A�h���X�i�ҏW�s�Ȃ̂ł��肦�Ȃ��j
        If TextFrom.Text.Trim() = "" Then
            LabelMessage.Text = String.Format(SEND_HISSU_ERROR, TdFrom.InnerText)
            TextFrom.Focus()
            ret = False
        End If
        ' ���M��A�h���X
        If TextTo.Text.Trim() = "" Then
            If LabelMessage.Text <> "" Then
                LabelMessage.Text = LabelMessage.Text & "</br>"
            Else
                TextTo.Focus()
            End If
            LabelMessage.Text = LabelMessage.Text & String.Format(SEND_HISSU_ERROR, TdTo.InnerText)
            ret = False
        End If
        ' ����
        If TextSubject.Text.Trim() = "" Then
            If LabelMessage.Text <> "" Then
                LabelMessage.Text = LabelMessage.Text & "</br>"
            Else
                TextSubject.Focus()
            End If
            LabelMessage.Text = LabelMessage.Text & String.Format(SEND_HISSU_ERROR, TdSubject.InnerText)
            ret = False
        End If
        ' �{��
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