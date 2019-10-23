Imports System.Text
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Web.UI.WebControls
Imports Itis.ApplicationBlocks.ExceptionManagement

''' <summary>
''' メール送信のヘルパークラスです
''' </summary>
''' <remarks></remarks>
Public Class SendMailHelper

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' メールを送信します
    ''' </summary>
    ''' <param name="smtpHost">smtpサーバー名</param>
    ''' <param name="from">送信者アドレス</param>
    ''' <param name="toStr">宛先</param>
    ''' <param name="cc">CC</param>
    ''' <param name="bcc">BCC</param>
    ''' <param name="subject">件名</param>
    ''' <param name="body">本文</param>
    ''' <param name="file">添付ファイル</param>
    ''' <returns>空白:正常終了 空白以外:エラーメッセージを返却します</returns>
    ''' <remarks>
    ''' iso-2022-jp形式で送信します。
    ''' 送信者アドレス・宛先・件名が空白だと例外が発生します。
    ''' </remarks>
    Public Function SendMail(ByVal smtpHost As String, _
                        ByVal from As String, _
                        ByVal toStr As String, _
                        ByVal cc As String, _
                        ByVal bcc As String, _
                        ByVal subject As String, _
                        ByVal body As String, _
                        ByVal file As FileUpload) As String

        'メソッド名、引数の情報の退避
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

        ' メール送信の例外はメッセージ返却でスルーする
        Try
            ' 送信元
            msg.From = New System.Net.Mail.MailAddress(from)
            '↓表示名を設定する時
            'msg.From = New System.Net.Mail.MailAddress(from, Encode("使用する表示名", myEnc))

            ' 送信先
            Dim toArray As Array = toStr.Split(",")
            For Each toStrTmp As String In toArray
                toStrTmp = toStrTmp.Trim
                If toStrTmp <> String.Empty Then
                    msg.To.Add(New System.Net.Mail.MailAddress(toStrTmp))
                    '↓表示名を設定する時
                    'msg.[To].Add= New System.Net.Mail.MailAddress(toStr, Encode("使用する表示名", myEnc))
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

            ' 件名
            msg.Subject = Encode(subject, myEnc)
            ' 本文
            Dim altView As AlternateView = AlternateView.CreateAlternateViewFromString( _
                body, myEnc, System.Net.Mime.MediaTypeNames.Text.Plain) 'プレーン・テキストを指定

            'Content-Transfer-Encoding: Base64
            altView.TransferEncoding = System.Net.Mime.TransferEncoding.Base64
            msg.AlternateViews.Add(altView)

            ' 添付ファイル情報有りの場合
            If Not file Is Nothing Then
                '添付ファイル
                If file.FileName <> "" Then
                    Dim attachfile As New Attachment(file.FileContent, file.FileName, MediaTypeNames.Text.Plain)
                    msg.Attachments.Add(attachfile)
                End If
            End If

            smtp.Host = smtpHost ' SMTPサーバ
            smtp.Send(msg) ' メッセージを送信

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
    ''' 指定したエンコードに変換します
    ''' </summary>
    ''' <param name="str">変換する文字列</param>
    ''' <param name="enc">エンコード形式</param>
    ''' <returns>変換後文字列</returns>
    ''' <remarks></remarks>
    Private Function Encode(ByVal str As String, ByVal enc As System.Text.Encoding) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".Encode", _
                                            str, _
                                            enc)

        Dim base64str As String = Convert.ToBase64String(enc.GetBytes(str))
        Return String.Format("=?{0}?B?{1}?=", enc.BodyName, base64str)
    End Function

End Class
