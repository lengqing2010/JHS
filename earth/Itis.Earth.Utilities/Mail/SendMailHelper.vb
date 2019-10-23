Imports System.Text
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Web.UI.WebControls
Imports Itis.ApplicationBlocks.ExceptionManagement

''' <summary>
''' ���[�����M�̃w���p�[�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class SendMailHelper

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' ���[���𑗐M���܂�
    ''' </summary>
    ''' <param name="smtpHost">smtp�T�[�o�[��</param>
    ''' <param name="from">���M�҃A�h���X</param>
    ''' <param name="toStr">����</param>
    ''' <param name="cc">CC</param>
    ''' <param name="bcc">BCC</param>
    ''' <param name="subject">����</param>
    ''' <param name="body">�{��</param>
    ''' <param name="file">�Y�t�t�@�C��</param>
    ''' <returns>��:����I�� �󔒈ȊO:�G���[���b�Z�[�W��ԋp���܂�</returns>
    ''' <remarks>
    ''' iso-2022-jp�`���ő��M���܂��B
    ''' ���M�҃A�h���X�E����E�������󔒂��Ɨ�O���������܂��B
    ''' </remarks>
    Public Function SendMail(ByVal smtpHost As String, _
                        ByVal from As String, _
                        ByVal toStr As String, _
                        ByVal cc As String, _
                        ByVal bcc As String, _
                        ByVal subject As String, _
                        ByVal body As String, _
                        ByVal file As FileUpload) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SendMail", _
                                            from, _
                                            toStr, _
                                            cc, _
                                            bcc, _
                                            subject, _
                                            body, _
                                            file)

        Dim smtp As New SmtpClient()
        Dim msg As New MailMessage()
        Dim myEnc As Encoding = Encoding.GetEncoding("iso-2022-jp")

        ' ���[�����M�̗�O�̓��b�Z�[�W�ԋp�ŃX���[����
        Try
            ' ���M��
            msg.From = New System.Net.Mail.MailAddress(from)
            '���\������ݒ肷�鎞
            'msg.From = New System.Net.Mail.MailAddress(from, Encode("�g�p����\����", myEnc))

            ' ���M��
            Dim toArray As Array = toStr.Split(",")
            For Each toStrTmp As String In toArray
                toStrTmp = toStrTmp.Trim
                If toStrTmp <> String.Empty Then
                    msg.To.Add(New System.Net.Mail.MailAddress(toStrTmp))
                    '���\������ݒ肷�鎞
                    'msg.[To].Add= New System.Net.Mail.MailAddress(toStr, Encode("�g�p����\����", myEnc))
                End If
            Next

            'CC
            If cc <> "" Then
                msg.CC.Add(New System.Net.Mail.MailAddress(cc))
            End If

            'BCC
            If bcc <> "" Then
                msg.Bcc.Add(New System.Net.Mail.MailAddress(bcc))
            End If

            ' ����
            msg.Subject = Encode(subject, myEnc)
            ' �{��
            Dim altView As AlternateView = AlternateView.CreateAlternateViewFromString( _
                body, myEnc, System.Net.Mime.MediaTypeNames.Text.Plain) '�v���[���E�e�L�X�g���w��

            'Content-Transfer-Encoding: Base64
            altView.TransferEncoding = System.Net.Mime.TransferEncoding.Base64
            msg.AlternateViews.Add(altView)

            ' �Y�t�t�@�C�����L��̏ꍇ
            If Not file Is Nothing Then
                '�Y�t�t�@�C��
                If file.FileName <> "" Then
                    Dim attachfile As New Attachment(file.FileContent, file.FileName, MediaTypeNames.Text.Plain)
                    msg.Attachments.Add(attachfile)
                End If
            End If

            smtp.Host = smtpHost ' SMTP�T�[�o
            smtp.Send(msg) ' ���b�Z�[�W�𑗐M

        Catch anEx As ArgumentNullException
            Return Messages.MSG033E
        Catch aoEx As ArgumentOutOfRangeException
            Return Messages.MSG034E
        Catch agEx As ArgumentException
            Return agEx.Message
        Catch odEx As ObjectDisposedException
            Return Messages.MSG035E
        Catch ioEx As InvalidOperationException
            Return Messages.MSG036E
        Catch sfEx As SmtpFailedRecipientsException
            Return Messages.MSG037E
        Catch smEx As SmtpException
            Return Messages.MSG038E
        End Try

        Return ""

    End Function

    ''' <summary>
    ''' �w�肵���G���R�[�h�ɕϊ����܂�
    ''' </summary>
    ''' <param name="str">�ϊ����镶����</param>
    ''' <param name="enc">�G���R�[�h�`��</param>
    ''' <returns>�ϊ��㕶����</returns>
    ''' <remarks></remarks>
    Private Function Encode(ByVal str As String, ByVal enc As System.Text.Encoding) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".Encode", _
                                            str, _
                                            enc)

        Dim base64str As String = Convert.ToBase64String(enc.GetBytes(str))
        Return String.Format("=?{0}?B?{1}?=", enc.BodyName, base64str)
    End Function

End Class
