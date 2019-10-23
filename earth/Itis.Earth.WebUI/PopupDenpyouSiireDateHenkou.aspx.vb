Public Partial Class PopupDenpyouSiireDateHenkou
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    Private MLogic As New MessageLogic
    Private CLogic As New CommonLogic
    Private SLogic As New SiireDataSearchLogic
    Private recSiire As New SiireDataKeyRecord

    ''' <summary>
    ''' ページ初期処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim jBn As New Jiban
        Dim jSM As New JibanSessionManager
        Dim strDenNo As String = String.Empty
        Dim strDenSiireDate As String = String.Empty

        'マスターページ情報を取得(ScriptManager用)
        masterAjaxSM = AjaxScriptManager

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合
            CLogic.CloseWindow(Me)
            Exit Sub
        End If

        '●権限のチェック
        '経理業務権限
        If userinfo.KeiriGyoumuKengen = 0 Then
            CLogic.CloseWindow(Me)
            Exit Sub
        End If

        '初回起動時
        If IsPostBack = False Then

            'パラメータの取得
            strDenNo = CLogic.GetDisplayString(Request("DenUnqNo"))             '伝票ユニークNO（一覧画面）
            strDenSiireDate = CLogic.GetDisplayString(Request("DenSiireDate"))      '伝票仕入年月日（一覧画面）

            'パラメータ不足時は画面を閉じる
            If String.IsNullOrEmpty(strDenNo) OrElse String.IsNullOrEmpty(strDenSiireDate) Then
                Me.TextDenSiireDate.Disabled = True
                Me.ButtonSubMitDisp.Disabled = True
                CLogic.CloseWindow(Me)
                Exit Sub
            End If

            'パラメータの設定
            Me.HiddenDenpyouUnqNo.Value = strDenNo
            Me.HiddenDefaultDenSiireDate.Value = strDenSiireDate

            'イベントハンドラの設定
            setBtnEvent()

            '売上データの取得と表示
            SetCtrlFromSiireRec(sender, e, strDenNo)

        End If

    End Sub

    ''' <summary>
    ''' 仕入データレコードから画面表示項目への値セットを行う
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strDenNo"></param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromSiireRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal strDenNo As String)

        Dim end_count As Integer = EarthConst.MAX_RESULT_COUNT
        Dim total_count As Integer = 0
        Dim recResult As New SiireSearchResultRecord
        Dim lgcSiire As New SiireDataSearchLogic
        Dim resultArray As New List(Of SiireSearchResultRecord)
        Dim tmpScript As String = String.Empty

        '取得パラメータの設定
        recSiire.DenUnqNo = strDenNo
        recSiire.NewDenpyouDisp = 1
        recSiire.KeijyouZumiDisp = 1

        'データの取得
        resultArray = lgcSiire.GetSiireDataInfo(sender, recSiire, 1, end_count, total_count)

        ' 検索結果ゼロ件の場合、メッセージを表示
        If total_count = 0 Then
            ' 検索結果ゼロ件の場合、メッセージを表示
            MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
        ElseIf total_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            Exit Sub
        End If

        'データの表示
        If total_count = 1 Then
            recResult = resultArray(0)
            '伝票仕入年月日
            Me.TextDenSiireDate.Value = CLogic.GetDisplayString(recResult.DenpyouSiireDate)
            '更新日時
            Me.HiddenRegUpdDate.Value = recResult.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)
        End If

        '一覧と変更画面で差異がある場合（既に更新がある場合）のみメッセージを表示
        If Me.HiddenDefaultDenSiireDate.Value <> Me.TextDenSiireDate.Value Then
            tmpScript = "alert('" & Messages.MSG174W & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Page_Load", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ボタンイベントの設定
    ''' </summary>
    ''' <remarks>
    ''' DB更新中、画面のグレイアウトを行なう。<br/>
    ''' </remarks>
    Private Sub setBtnEvent()

        'イベントハンドラ登録
        Dim tmpScript As String = "setWindowOverlay(this,null,1);"

        '登録許可MSG確認後、OKの場合更新処理を行う
        Me.ButtonSubmit.Attributes("onclick") = tmpScript

    End Sub

    ''' <summary>
    ''' 伝票仕入年月日変更ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSubmit_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSubmit.ServerClick
        Dim tmpScript As String = String.Empty

        '入力チェック
        If checkInput() = False Then Exit Sub

        ' 画面の内容をDBに反映する
        If SaveData() Then '登録成功
            '親画面リロード処理
            tmpScript = "funcSubmit();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "BtnSubmit_ServerClick", tmpScript, True)
            Exit Sub
        Else '登録失敗
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "伝票仕入年月日変更") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "BtnSubmit_ServerClick", tmpScript, True)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function checkInput() As Boolean

        'エラーメッセージ初期化
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList
        '月次処理確定年月
        Dim dtGetujiKakuteiLastSyoriDate As DateTime = DateTime.MinValue

        '伝票仕入年月日
        If Me.TextDenSiireDate.Value <> String.Empty Then
            If CLogic.checkDateHanni(Me.TextDenSiireDate.Value) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "伝票仕入年月日")
                arrFocusTargetCtrl.Add(Me.TextDenSiireDate)
            End If
        Else
            errMess += Messages.MSG013E.Replace("@PARAM1", "伝票仕入年月日")
            arrFocusTargetCtrl.Add(Me.TextDenSiireDate)
        End If

        '月次確定予約済みの処理年月「以前」の日付で伝票仕入年月日を設定するのはエラー
        If CLogic.chkGetujiKakuteiYoyakuzumi(Me.TextDenSiireDate.Value, dtGetujiKakuteiLastSyoriDate) = False Then
            errMess += String.Format(Messages.MSG176E, dtGetujiKakuteiLastSyoriDate.Month, "伝票仕入年月日", dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
            arrFocusTargetCtrl.Add(Me.TextDenSiireDate)
        End If

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If errMess <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            MLogic.AlertMessage(Me, errMess)
            Return False
        End If

        'エラー無しの場合、Trueを返す
        Return True

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function SaveData() As Boolean
        Dim recData As New SiireDataRecord

        '画面からレコードクラスにセット
        recData = GetCtrlToRec()

        'データの更新を行います
        If SLogic.SaveSiireData(Me, recData) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 画面の各情報をレコードクラスに取得し、DB更新用レコードクラスを返却する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCtrlToRec() As SiireDataRecord
        Dim dtRec As New SiireDataRecord

        '伝票ユニークNO
        CLogic.SetDisplayString(Me.HiddenDenpyouUnqNo.Value, dtRec.DenpyouUniqueNo)
        '伝票番号(伝票仕入年月日が変更されるので、採番し直すため伝票番号はクリア)
        dtRec.DenpyouNo = Nothing
        '伝票仕入年月日
        CLogic.SetDisplayString(Me.TextDenSiireDate.Value, dtRec.DenpyouSiireDate)
        '更新者ユーザID
        CLogic.SetDisplayString(userinfo.LoginUserId, dtRec.UpdLoginUserId)
        '登録・更新日時
        dtRec.UpdDatetime = Me.HiddenRegUpdDate.Value

        Return dtRec
    End Function

End Class