Partial Public Class GetujiIkkatuSyuusei
    Inherits System.Web.UI.Page
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "変数"
    Private user_info As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    '地盤クラス
    Private jBn As New Jiban
    '月次一括更新ロジック
    Dim clsUpdLogic As New GetujiIkkatuUpdateLogic
    'メッセージロジック
    Dim mLogic As New MessageLogic
#End Region

    ''' <summary>
    ''' ページロード時のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

            Dim today As Date = Date.Now

            ' 初期値設定
            TextUriageFrom.Value = today.Year.ToString() & _
                                   "/" & _
                                   today.Month.ToString("00") & _
                                   "/01"

            TextUriageTo.Value = today.Year.ToString() & _
                                 "/" & _
                                 today.Month.ToString("00") & _
                                 "/" & _
                                 today.Day.ToString("00")

            TextSeikyuuFrom.Value = today.Year.ToString() & _
                                    "/" & _
                                    today.Month.ToString("00") & _
                                    "/" & _
                                    DateTime.DaysInMonth(today.Year, today.Month).ToString("00")

            TextSeikyuuTo.Value = today.Year.ToString() & _
                                  "/" & _
                                  today.Month.ToString("00") & _
                                  "/" & _
                                  DateTime.DaysInMonth(today.Year, today.Month).ToString("00")

            ' 決算処理可能月のチェック
            If Now.Month = 3 Or _
               Now.Month = 4 Or _
               Now.Month = 9 Or _
               Now.Month = 10 Then
                ' 決算月処理ボタンは3,4,9,10月のみ押下可能
                ButtonKessanSyori.Disabled = False
                BtnKessanSyoriCall.Disabled = False
            Else
                ' 押下不可
                ButtonKessanSyori.Disabled = True
                BtnKessanSyoriCall.Disabled = True
            End If

            '月次確定処理に関する画面部品の状態設定
            GetujiKakuteiGamenSetting()

        End If
    End Sub

    ''' <summary>
    ''' 「月次処理」ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonGetujiSyori_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGetujiSyori.ServerClick
        Dim strInfoMsg As String = ""
        Dim intResult As Integer = 0

        '入力チェック
        If Not CheckInput() Then
            Exit Sub
        End If

        With clsUpdLogic
            'ログインユーザー情報をクラスへセット
            .LoginUserId = user_info.LoginUserId
            '画面表示内容をクラスへセット
            .AccUriageFrom = Me.TextUriageFrom.Value
            .AccUriageTo = Me.TextUriageTo.Value
            .AccSeikyuuFrom = Me.TextSeikyuuFrom.Value
            .AccSeikyuuTo = Me.TextSeikyuuTo.Value

            '(2) 月次処理ボタン押下時の処理                     
            intResult = .GetujiSyori(sender)
        End With

        '処理終了メッセージ表示
        If intResult <> 0 Then
            If intResult = Integer.MinValue Then
                strInfoMsg += Messages.MSG019E.Replace("@PARAM1", "月次処理")
            Else
                strInfoMsg += Messages.MSG043S.Replace("@PARAM1", "月次処理")
            End If
        Else
            strInfoMsg += Messages.MSG044S
        End If

        mLogic.AlertMessage(Me, strInfoMsg, 0, "GetujiSyoriError")

    End Sub

    ''' <summary>
    ''' 「決算月処理」ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKessanSyori_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKessanSyori.ServerClick
        Dim strInfoMsg As String = ""
        Dim intResult As Integer = 0

        With clsUpdLogic
            'ログインユーザー情報をクラスへセット
            .LoginUserId = user_info.LoginUserId

            '(3) 決算月処理押下時の処理    
            intResult = .KessanSyori(sender)
        End With

        '処理終了メッセージ表示
        If intResult <> 0 Then
            If intResult = Integer.MinValue Then
                strInfoMsg += Messages.MSG019E.Replace("@PARAM1", "決算月処理")
            Else
                strInfoMsg += Messages.MSG043S.Replace("@PARAM1", "決算月処理")
            End If
        Else
            strInfoMsg += Messages.MSG044S
        End If

        mLogic.AlertMessage(Me, strInfoMsg, 0, "KessanSyoriError")

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
        If TextUriageFrom.Value = "" Then
            strErrMsg += Messages.MSG013E.Replace("@PARAM1", "売上年月日FROM")
            arrFocusTargetCtrl.Add(TextUriageFrom)
        End If
        If TextSeikyuuFrom.Value = "" Then
            strErrMsg += Messages.MSG013E.Replace("@PARAM1", "請求書発行日FROM")
            arrFocusTargetCtrl.Add(TextSeikyuuFrom)
        End If

        'TO項目未入力時入力補完
        If TextUriageTo.Value = "" Then
            TextUriageTo.Value = TextUriageFrom.Value
        End If
        If TextSeikyuuTo.Value = "" Then
            Me.TextSeikyuuTo.Value = Me.TextSeikyuuFrom.Value
        End If

        '日付チェック
        If TextUriageFrom.Value <> "" Then
            If DateTime.Parse(TextUriageFrom.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(TextUriageFrom.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG040E.Replace("@PARAM1", "売上年月日FROM")
                arrFocusTargetCtrl.Add(TextUriageFrom)
            End If
        End If
        If TextUriageTo.Value <> "" Then
            If DateTime.Parse(TextUriageTo.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(TextUriageTo.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG040E.Replace("@PARAM1", "売上年月日TO")
                arrFocusTargetCtrl.Add(TextUriageTo)
            End If
        End If
        If TextSeikyuuFrom.Value <> "" Then
            If DateTime.Parse(TextSeikyuuFrom.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(TextSeikyuuFrom.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG040E.Replace("@PARAM1", "請求書発行日FROM")
                arrFocusTargetCtrl.Add(TextSeikyuuFrom)
            End If
        End If
        If TextSeikyuuTo.Value <> "" Then
            If DateTime.Parse(TextSeikyuuTo.Value) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(TextSeikyuuTo.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG040E.Replace("@PARAM1", "請求書発行日TO")
                arrFocusTargetCtrl.Add(TextSeikyuuTo)
            End If
        End If
        '日付の大小チェック
        If TextUriageFrom.Value <> "" And TextUriageTo.Value <> "" Then
            If DateTime.Parse(TextUriageFrom.Value) > DateTime.Parse(TextUriageTo.Value) Then
                strErrMsg += Messages.MSG041E
                arrFocusTargetCtrl.Add(TextUriageFrom)
            End If
        End If
        If TextSeikyuuFrom.Value <> "" And TextSeikyuuTo.Value <> "" Then
            If DateTime.Parse(TextSeikyuuFrom.Value) > DateTime.Parse(TextSeikyuuTo.Value) Then
                strErrMsg += Messages.MSG041E
                arrFocusTargetCtrl.Add(TextSeikyuuFrom)
            End If
        End If

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If strErrMsg <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            mLogic.AlertMessage(Me, strErrMsg, 0, "CheckInputError")
            Return False
        End If
        'エラー無しの場合、Trueを返す
        Return True
    End Function

    ''' <summary>
    ''' 月次確定処理に関する画面部品の状態設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetujiKakuteiGamenSetting()

        '月次確定処理関連初期設定
        TextKakuteiYM.Value = Today.AddMonths(-1).Year.ToString() & _
                              "/" & _
                              Today.AddMonths(-1).Month.ToString("00")

        Dim targetYM As Date = TextKakuteiYM.Value & "/01"

        '現在の処理状況によりボタンの有効/無効を切替
        Dim syoriJoukyou As Object = clsUpdLogic.GetGetujiKakuteiYoyakuData(targetYM.AddMonths(1).AddDays(-1))
        ButtonKakuteiYoyaku.Disabled = True
        ButtonKakuteiYoyakuExe.Disabled = True
        ButtonKakuteiYoyakuKaijo.Disabled = True
        ButtonKakuteiYoyakuKaijoExe.Disabled = True
        If syoriJoukyou Is Nothing OrElse syoriJoukyou = 0 Then
            ButtonKakuteiYoyaku.Disabled = False
            ButtonKakuteiYoyakuExe.Disabled = False
        ElseIf syoriJoukyou = 1 Then
            ButtonKakuteiYoyakuKaijo.Disabled = False
            ButtonKakuteiYoyakuKaijoExe.Disabled = False
        End If

        '現在の処理状況を画面にセット
        Dim strSyoriJoukyou As String = String.Empty
        TextKakuteiSyoriJoukyou.Style.Remove("color")
        If syoriJoukyou Is Nothing Then
            strSyoriJoukyou = "未予約"
        Else
            Select Case CInt(syoriJoukyou)
                Case 0
                    strSyoriJoukyou = "予約取消"
                Case 1
                    strSyoriJoukyou = "予約中"
                Case 2
                    strSyoriJoukyou = "処理中"
                Case 9
                    strSyoriJoukyou = "処理済み"
                    TextKakuteiSyoriJoukyou.Style("color") = "red"
                Case Else
                    strSyoriJoukyou = syoriJoukyou.ToString
            End Select
        End If
        TextKakuteiSyoriJoukyou.Value = strSyoriJoukyou

    End Sub

    ''' <summary>
    ''' 月次確定処理予約ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKakuteiYoyakuExe_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKakuteiYoyakuExe.ServerClick
        ExeGetujiKakuteiYoyaku(sender, 1)

    End Sub

    ''' <summary>
    ''' 月次確定処理予約解除ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKakuteiYoyakuKaijoExe_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKakuteiYoyakuKaijoExe.ServerClick
        ExeGetujiKakuteiYoyaku(sender, 2)

    End Sub

    ''' <summary>
    ''' 月次確定処理予約状態更新処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="exeType"></param>
    ''' <remarks></remarks>
    Private Sub ExeGetujiKakuteiYoyaku(ByVal sender As System.Object, ByVal exeType As Integer)

        Dim intResult As Integer
        Dim strInfoMsg As String = String.Empty
        Dim targetYM As Date
        Dim extTypeMess As String = "月次確定処理の予約"

        'メッセージに表示する処理タイプの文字列を変更(解除処理の場合)
        If exeType = 2 Then
            extTypeMess &= "解除"
        End If

        '処理年月を設定
        If TextKakuteiYM.Value <> String.Empty Then
            targetYM = TextKakuteiYM.Value & "/01"
            targetYM = targetYM.AddMonths(1).AddDays(-1)
        Else
            mLogic.AlertMessage(sender, Messages.MSG013E.Replace("@PARAM1", "確定処理対象年月"), 0, "CheckInputError")
            Exit Sub
        End If

        'ロジッククラスのプロパティにログインユーザーIDをセット
        clsUpdLogic.LoginUserId = user_info.LoginUserId

        '処理実行
        intResult = clsUpdLogic.EditGetujiKakuteiYoyaku(sender, exeType, targetYM)

        '処理終了メッセージ表示
        If intResult <> 0 Then
            If intResult = Integer.MinValue Then
                strInfoMsg += Messages.MSG155E.Replace("@PARAM1", extTypeMess)
            Else
                strInfoMsg += Messages.MSG043S.Replace("@PARAM1", extTypeMess & "処理")
            End If
        Else
            strInfoMsg += Messages.MSG044S
        End If
        mLogic.AlertMessage(Me, strInfoMsg, 0, "KakuteiYoyakuError")

        '月次確定処理に関する画面部品の状態設定
        GetujiKakuteiGamenSetting()

    End Sub

End Class