Partial Public Class NyuukinSyori
    Inherits System.Web.UI.Page

#Region "変数"
    Private user_info As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
#End Region

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス
        Dim clsLogic As New NyuukinSyoriLogic

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ユーザー基本認証
        jBn.UserAuth(user_info)
        If user_info Is Nothing Then
            'ログイン情報が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If
        '権限制御
        If user_info.KeiriGyoumuKengen <> -1 Then
            '権限が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If IsPostBack = False Then
            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            ' 系列コードコンボにデータをバインドする
            Dim helper As New DropDownHelper
            helper.SetDropDownList(Me.SelectKeiretuCode, DropDownHelper.DropDownType.KeiretuCd)
            '請求書発行日のクリア
            Me.TextSeikyuusyoHakkoubiFrom.Value = ""
            Me.TextSeikyuusyoHakkoubiTo.Value = ""
        End If
    End Sub

    ''' <summary>
    ''' ページロードコンプリート
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim clsLogic As New NyuukinSyoriLogic
        Dim zenkaiTorikomiTable As DataTable
        Dim dtZenkaiTorikomiDate As Date
        Dim strJs As String

        '前回入金データ取込情報の取得
        zenkaiTorikomiTable = clsLogic.GetZenkaiTorikomiData
        If zenkaiTorikomiTable.Rows.Count > 0 Then
            With zenkaiTorikomiTable.Rows(0)
                '取得データを画面へセット
                '前回取込日時
                dtZenkaiTorikomiDate = .Item("syori_datetime")
                Me.TextZenkaiTorikomiNitiji.Value = dtZenkaiTorikomiDate.Year.ToString() & _
                                                    "/" & _
                                                    dtZenkaiTorikomiDate.Month.ToString("00") & _
                                                    "/" & _
                                                    dtZenkaiTorikomiDate.Day.ToString("00")
                Me.TextZenkaiTorikomiNitiji.Value = .Item("syori_datetime")
                '前回取込ファイル名
                Me.TextZenkaiTorikomiFileMei.Value = .Item("nyuuryoku_file_mei")
                '前回エラー有無
                If .Item("error_umu") = 0 Then
                    Me.LinkZenkaiErrorUmu.NavigateUrl = ""
                    Me.LinkZenkaiErrorUmu.Text = "無し"
                    Me.LinkZenkaiErrorUmu.Attributes.Add("onclick", "")
                    Me.LinkZenkaiErrorUmu.Style("color") = "black"
                Else
                    Me.LinkZenkaiErrorUmu.NavigateUrl = "javascript:void(0);"
                    Me.LinkZenkaiErrorUmu.Text = "有り"
                    strJs = "window.open('" & UrlConst.NYUUKIN_ERROR & _
                            "?nkn_kbn=0" & _
                            "&edi=" & HttpUtility.UrlEncode(.Item("edi_jouhou_sakusei_date")) & _
                            "&file=" & HttpUtility.UrlEncode(Me.TextZenkaiTorikomiFileMei.Value) & _
                            "','errorWindow','menubar=no,toolbar=no,location=no,status=no,resizable=yes,scrollbars=yes')"
                    Me.LinkZenkaiErrorUmu.Attributes.Add("onclick", strJs)
                End If
            End With
        Else
            Me.TextZenkaiTorikomiNitiji.Value = ""
            Me.TextZenkaiTorikomiFileMei.Value = ""
            Me.LinkZenkaiErrorUmu.NavigateUrl = ""
            Me.LinkZenkaiErrorUmu.Text = ""
        End If

        '前回JHS入金データ取込情報の取得
        Dim clsJhsLogic As New JhsNyuukinTorikomiLogic
        Dim recUpload As UploadKanriRecord

        recUpload = clsJhsLogic.GetZenkaiTorikomiData
        If recUpload IsNot Nothing Then

            Me.TextJhsZenkaiTorikomiNitiji.Value = recUpload.SyoriDatetime      '前回取込日時
            Me.TextJhsZenkaiTorikomiFileMei.Value = recUpload.NyuuryokuFileMei  '前回取込ファイル名
            If recUpload.ErrorUmu = 0 Then
            Else
                'Me.LinkJhsZenkaiErrorUmu.NavigateUrl = "javascript:void(0);"
                'Me.LinkJhsZenkaiErrorUmu.Text = "有り"
                'strJs = "window.open('" & UrlConst.NYUUKIN_ERROR & _
                '        "?nkn_kbn=1" & _
                '        "&edi=" & HttpUtility.UrlEncode(recUpload.EdiJouhouSakuseiDate) & _
                '        "&file=" & HttpUtility.UrlEncode(Me.TextJhsZenkaiTorikomiFileMei.Value) & _
                '        "','errorWindow','menubar=no,toolbar=no,location=no,status=no,resizable=yes,scrollbars=yes')"
                'Me.LinkJhsZenkaiErrorUmu.Attributes.Add("onclick", strJs)
            End If
        Else
            Me.TextJhsZenkaiTorikomiNitiji.Value = ""
            Me.TextJhsZenkaiTorikomiFileMei.Value = ""
            'Me.LinkJhsZenkaiErrorUmu.NavigateUrl = ""
            'Me.LinkJhsZenkaiErrorUmu.Text = ""
        End If
    End Sub

    ''' <summary>
    ''' 一括入金処理ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonIkkatuNyuukinSyori_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonIkkatuNyuukinSyori.ServerClick
        Dim clsLogic As New NyuukinSyoriLogic
        Dim teibetuSeikyuuTable As DataTable
        Dim strInfoMsg As String = ""
        Dim strZeikomiUriageGaku As String
        Dim tmpScript As String
        Dim MLogic As New MessageLogic

        '1.入力チェック
        If Not CheckInput() Then
            Exit Sub
        End If

        '2.邸別請求テーブルの抽出
        With clsLogic
            'ログインユーザー情報をクラスへセット
            .LoginUserId = user_info.LoginUserId
            '画面表示内容をクラスへセット
            .AccSeikyuuFrom = Me.TextSeikyuusyoHakkoubiFrom.Value
            .AccSeikyuuTo = Me.TextSeikyuusyoHakkoubiTo.Value
            .AccKeiretuCd = Me.SelectKeiretuCode.Value
            teibetuSeikyuuTable = .GetTeibetuSeikyuuData()
        End With

        '例外処理
        If teibetuSeikyuuTable Is Nothing Then
            '処理対象データが存在しなかった場合
            strInfoMsg = Messages.MSG020E
            tmpScript = "alert('" & strInfoMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Exit Sub
        End If

        '3.実行確認メッセージの表示
        '請求総額の取得
        strZeikomiUriageGaku = clsLogic.CalcZeikomiUriageGaku(teibetuSeikyuuTable)
        If Not IsNumeric(strZeikomiUriageGaku) Then
            MLogic.AlertMessage(sender, strZeikomiUriageGaku)
            Exit Sub
        End If
        Me.HiddenChkUriageGaku.Value = strZeikomiUriageGaku

        strInfoMsg = Messages.MSG048C.Replace("@PARAM1", strZeikomiUriageGaku)
        tmpScript = "if(confirm('" & strInfoMsg & "')){" & vbCrLf
        tmpScript &= "    _d = window.document;"
        tmpScript &= "    setWindowOverlay(this);"
        tmpScript &= "    objEBI(""" & Me.ButtonIkkatuNyuukinSyoriNext.ClientID & """).click();" & vbCrLf
        tmpScript &= "}" & vbCrLf
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "confirm", tmpScript, True)

    End Sub

    ''' <summary>
    ''' 一括入金処理ボタン押下時の処理の続き
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonIkkatuNyuukinSyoriNext_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonIkkatuNyuukinSyoriNext.ServerClick
        Dim clsLogic As New NyuukinSyoriLogic
        Dim strResultMsg As String
        Dim strInfoMsg As String
        Dim tmpScript As String

        '一括入金処理
        With clsLogic
            'ログインユーザー情報をクラスへセット
            .LoginUserId = user_info.LoginUserId
            '画面表示内容をクラスへセット
            .AccSeikyuuFrom = Me.TextSeikyuusyoHakkoubiFrom.Value
            .AccSeikyuuTo = Me.TextSeikyuusyoHakkoubiTo.Value
            .AccKeiretuCd = Me.SelectKeiretuCode.Value
            strResultMsg = .IkkatuNyuukinSyori(Me.HiddenChkUriageGaku.Value)
        End With

        '8.完了メッセージの表示
        If strResultMsg.Length > 0 Then
            tmpScript = "alert('" & strResultMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
        Else
            strInfoMsg = Messages.MSG050S
            tmpScript = "alert('" & strInfoMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
        End If
    End Sub

    ''' <summary>
    ''' 入金データ取り込みボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonNyuukinDataTorikomiNext_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNyuukinDataTorikomiNext.ServerClick
        Dim clsLogic As New NyuukinSyoriLogic
        Dim strErrMsg As String
        Dim strResultMsg As String
        Dim tmpScript As String

        'ログインユーザー情報をクラスへセット
        clsLogic.LoginUserId = user_info.LoginUserId

        '1.参照ボタン押下により指定したファイルのアップロードを行う
        'ファイルチェック
        strErrMsg = clsLogic.ChkFile(Me.FileNyuukinDataTorikomi)
        If strErrMsg.Length > 0 Then
            tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
            Exit Sub
        End If

        strResultMsg = clsLogic.NyuukinDataTorikomi(Me.FileNyuukinDataTorikomi)

        '4.完了メッセージの表示
        If strResultMsg.Length > 0 Then
            tmpScript = "alert('" & strResultMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
        Else
            tmpScript = "alert('" & Messages.MSG060S & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
        End If
    End Sub

    ''' <summary>
    ''' JHS入金データ取り込みボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonJhsNyuukinDataTorikomiNext_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonJhsNyuukinDataTorikomiNext.ServerClick
        Dim clsLogic As New NyuukinSyoriLogic
        Dim clsJhsLogic As New JhsNyuukinTorikomiLogic
        Dim strErrMsg As String
        Dim strResultMsg As String
        Dim tmpScript As String

        'ログインユーザー情報をクラスへセット
        clsJhsLogic.LoginUserId = user_info.LoginUserId

        'ファイルチェック
        strErrMsg = clsLogic.ChkFile(Me.FileJhsNyuukinDataTorikomi)
        If strErrMsg.Length > 0 Then
            tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
            Exit Sub
        End If

        strResultMsg = clsJhsLogic.NyuukinDataTorikomi(Me.FileJhsNyuukinDataTorikomi)

        '完了メッセージの表示()
        tmpScript = "alert('" & strResultMsg & "');" & vbCrLf
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)

    End Sub

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckInput() As Boolean
        'エラーメッセージ初期化
        Dim strErrMsg As String = ""
        Dim arrFocusTargetCtrl As New ArrayList
        Dim strSetFocus As String = ""

        '必須チェック
        If Me.TextSeikyuusyoHakkoubiFrom.Value = "" Then
            strErrMsg += Messages.MSG013E.Replace("@PARAM1", "請求書発行日FROM")
            arrFocusTargetCtrl.Add(Me.TextSeikyuusyoHakkoubiFrom)
        End If
        If Me.SelectKeiretuCode.Value = "" Then
            strErrMsg += Messages.MSG013E.Replace("@PARAM1", "系列コード")
            arrFocusTargetCtrl.Add(Me.SelectKeiretuCode)
        End If
        'TO項目未入力時入力補完
        If Me.TextSeikyuusyoHakkoubiTo.Value = "" Then
            Me.TextSeikyuusyoHakkoubiTo.Value = Me.TextSeikyuusyoHakkoubiFrom.Value
        End If
        '日付チェック
        If TextSeikyuusyoHakkoubiFrom.Value <> "" Then
            If DateTime.Parse(TextSeikyuusyoHakkoubiFrom.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(TextSeikyuusyoHakkoubiFrom.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG088E.Replace("@PARAM1", "請求書発行日FROM")
                arrFocusTargetCtrl.Add(TextSeikyuusyoHakkoubiFrom)
            End If
        End If
        If TextSeikyuusyoHakkoubiTo.Value <> "" Then
            If DateTime.Parse(TextSeikyuusyoHakkoubiTo.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(TextSeikyuusyoHakkoubiTo.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG088E.Replace("@PARAM1", "請求書発行日TO")
                arrFocusTargetCtrl.Add(TextSeikyuusyoHakkoubiTo)
            End If
        End If
        '日付の大小チェック
        If Me.TextSeikyuusyoHakkoubiFrom.Value <> "" And Me.TextSeikyuusyoHakkoubiTo.Value <> "" Then
            If DateTime.Parse(Me.TextSeikyuusyoHakkoubiFrom.Value) > DateTime.Parse(Me.TextSeikyuusyoHakkoubiTo.Value) Then
                strErrMsg += Messages.MSG041E
                arrFocusTargetCtrl.Add(Me.TextSeikyuusyoHakkoubiFrom)
            End If
        End If
        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If strErrMsg <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If
        'エラー無しの場合、Trueを返す
        Return True
    End Function

End Class