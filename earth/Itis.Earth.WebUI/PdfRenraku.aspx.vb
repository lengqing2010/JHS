
Partial Public Class PdfRenraku

    Inherits System.Web.UI.Page

    'メッセージロジック
    Dim mLogic As New MessageLogic

    ''' <summary>
    ''' Page_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '文字列受け取り
        If Request("kbn") IsNot Nothing Then
            kbn.Value = Request("kbn")
        End If
        If Request("hosyouno") IsNot Nothing Then
            hosyouno.Value = Request("hosyouno")
        End If
        If Request("accountno") IsNot Nothing Then
            accountno.Value = Request("accountno")
        End If
        If Request("stdate") IsNot Nothing Then
            stdate.Value = Request("stdate")
        End If


        'dat作成
        '帳票データファイル
        Dim nitiji As String = Replace(System.DateTime.Now, "/", "")
        nitiji = Replace(nitiji, ":", "")
        Dim dataFileName As String = kbn.Value & hosyouno.Value & Replace(nitiji, " ", "") & ".dat"

        '帳票定義ファイル(フォームデータ)
        Dim fcpFileName As String = "jiban.fcp"
        '帳票出力データファイル作成
        Dim status As FcwManager.FileWriteStatus = FcwManager.Instance.CreateFcwDataFile(sender, _
                                                                                        dataFileName, _
                                                                                        kbn.Value, _
                                                                                        hosyouno.Value, _
                                                                                        accountno.Value, _
                                                                                        stdate.Value)

        If status.Equals(Itis.Earth.BizLogic.FcwManager.FileWriteStatus.OK) Then
            '帳票出力データファイルが正常に作成された場合、帳票サーバへ出力指示するためのURLを生成
            Dim redirectUrl As String = FcwManager.Instance.CreateResponseRedirectString(FcwManager.FcwDriverType.CSM, _
                                                                                         fcpFileName, _
                                                                                         dataFileName)
            '帳票出力サーバへリダイレクト
            ScriptManager.RegisterClientScriptBlock(sender, _
                                                    sender.GetType(), _
                                                    "pdfRedirect", _
                                                    "location.replace('" & redirectUrl & "');", _
                                                    True)

        Else
            'データファイル作成で問題が発生した場合、アラートを表示
            mLogic.AlertMessage(sender, String.Format(Messages.MSG121E, status.ToString), 1, "SqlException")

        End If

    End Sub

End Class