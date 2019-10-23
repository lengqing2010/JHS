Public Partial Class SeikyuusyoDataSakusei
    Inherits System.Web.UI.Page

#Region "共通変数"

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    Private jSM As New JibanSessionManager
    Private jBn As New Jiban
    Private cl As New CommonLogic
    Private lgcMassage As New MessageLogic
    Private lgcSeiDataSaku As New SeikyuusyoDataSakuseiLogic

    ''' <summary>
    ''' 請求先情報行No
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emSsCtrl
        intCtrl1 = 1        '行1
        intCtrl2 = 2        '行2
        intCtrl3 = 3        '行3
        intCtrl4 = 4        '行4
        intCtrl5 = 5        '行5
        intCtrl6 = 6        '行6
        intCtrl7 = 7        '行7
        intCtrl8 = 8        '行8
        intCtrl9 = 9        '行9
        intCtrl10 = 10      '行10
    End Enum

#End Region

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)
        If userinfo Is Nothing Then
            'ログイン情報が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        ' 権限制御
        If userinfo.KeiriGyoumuKengen <> -1 Then
            '権限が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If


        '各テーブルの表示状態を切り替える
        Me.TBodySeikyuuInfo.Style("display") = Me.HiddenDispStyle.Value

        If IsPostBack = False Then

            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            SetDispAction()

            ' 初期フォーカス
            Me.TextSeikyuusyoHakDate.Focus()

            '画面表示時点の請求先情報をHiddenに保持(変更チェック用)
            If Me.HiddenPushValuesSeikyuu.Value = String.Empty Then
                Me.HiddenPushValuesSeikyuu.Value = getCtrlValuesStringSeikyuu()
            End If

        End If

        Me.HiddenMovePageType.Value = String.Empty

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDispAction()

        'JavaScript関連定義
        Dim onFocusPostBackScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScript As String = "if(checkTempValueForOnBlur(this)){__doPostBack(this.id,'');}"

        '****************************************************************************
        ' ドロップダウンリスト設定
        '****************************************************************************
        Dim helper As New DropDownHelper
        ' 請求先区分コンボにデータをバインドする
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_1, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_2, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_3, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_4, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_5, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_6, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_7, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_8, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_9, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)
        helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn_10, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '請求先検索系コード入力項目イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '請求先コード
        Me.TextSeikyuuSakiCd_1.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_1.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_2.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_2.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_3.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_3.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_4.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_4.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_5.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_5.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_6.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_6.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_7.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_7.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_8.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_8.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_9.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_9.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiCd_10.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiCd_10.Attributes("onblur") = onBlurPostBackScript
        '請求先枝番
        Me.TextSeikyuuSakiBrc_1.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_1.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_2.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_2.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_3.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_3.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_4.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_4.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_5.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_5.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_6.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_6.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_7.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_7.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_8.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_8.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_9.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_9.Attributes("onblur") = onBlurPostBackScript
        Me.TextSeikyuuSakiBrc_10.Attributes("onfocus") = onFocusPostBackScript
        Me.TextSeikyuuSakiBrc_10.Attributes("onblur") = onBlurPostBackScript

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript関連セット
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim checkDate As String = "checkDate(this);"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '請求書発行日
        Me.TextSeikyuusyoHakDate.Attributes("onblur") = checkDate
        Me.TextSeikyuusyoHakDate.Attributes("onkeydown") = disabledOnkeydown

        '請求書一覧ボタン
        Me.ButtonSyuturyokuSeikyuusyo.Attributes("onclick") = "checkJikkou(this);"
        '過去請求書一覧ボタン
        Me.ButtonKakoSeikyuusyo.Attributes("onclick") = "checkJikkou(this);"

        '●請求先情報リンク
        Me.SeikyuuDispLink.HRef = "JavaScript:changeDisplay('" & Me.TBodySeikyuuInfo.ClientID & "');SetDisplayStyle('" & Me.HiddenDispStyle.ClientID & "','" & Me.TBodySeikyuuInfo.ClientID & "');"

        '請求書締め日履歴ボタン
        Me.ButtonSeikyuusyoSimeDateRireki.Attributes("onclick") = "checkJikkou(this);"

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
        Dim blnMstChk As Boolean = True     'マスタへの存在チェックフラグ

        '●コード入力値変更チェック
        CheckSeikyuuChg(strErrMsg, arrFocusTargetCtrl)

        '必須チェック
        If Trim(Me.TextSeikyuusyoHakDate.Value) = String.Empty Then
            strErrMsg += Messages.MSG013E.Replace("@PARAM1", "請求書発行日")
            arrFocusTargetCtrl.Add(Me.TextSeikyuusyoHakDate)
        End If

        '日付チェック
        If Me.TextSeikyuusyoHakDate.Value <> String.Empty Then
            If DateTime.Parse(Me.TextSeikyuusyoHakDate.Value) > EarthConst.Instance.MAX_DATE _
            Or DateTime.Parse(Me.TextSeikyuusyoHakDate.Value) < EarthConst.Instance.MIN_DATE Then
                strErrMsg += Messages.MSG088E.Replace("@PARAM1", "請求書発行日")
                arrFocusTargetCtrl.Add(TextSeikyuusyoHakDate)
            End If
        End If

        '請求先比較チェック(変更チェック)
        If Me.HiddenPushValuesSeikyuu.Value <> getCtrlValuesStringSeikyuu() Then
            strErrMsg += Messages.MSG188E
            arrFocusTargetCtrl.Add(Me.btnGetDateCall)
        End If

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If strErrMsg <> "" Then

            '請求先指定を展開する
            Me.TBodySeikyuuInfo.Style("display") = "inline"
            Me.HiddenDispStyle.Value = "inline"

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

    ''' <summary>
    ''' 請求先入力値変更チェック
    ''' </summary>
    ''' <param name="strErrMsg"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <remarks></remarks>
    Private Sub CheckSeikyuuChg(ByRef strErrMsg As String, ByRef arrFocusTargetCtrl As ArrayList)

        '請求先コード・枝番・区分のどれかに入力がある場合のみチェック
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_1.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_1.Text) Or _
           Me.SelectSeikyuuSakiKbn_1.SelectedIndex <> 0 Then

            '検索ボタン押下時と現在で差異があるのかチェック
            If Me.TextSeikyuuSakiCd_1.Text <> Me.HiddenSeikyuuSakiCdOld_1.Value _
                Or Me.TextSeikyuuSakiBrc_1.Text <> Me.HiddenSeikyuuSakiBrcOld_1.Value _
                Or Me.SelectSeikyuuSakiKbn_1.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_1.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "請求先１")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_1)
            End If

        End If

        '請求先コード・枝番・区分のどれかに入力がある場合のみチェック
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_2.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_2.Text) Or _
           Me.SelectSeikyuuSakiKbn_2.SelectedIndex <> 0 Then

            '検索ボタン押下時と現在で差異があるのかチェック
            If Me.TextSeikyuuSakiCd_2.Text <> Me.HiddenSeikyuuSakiCdOld_2.Value _
                Or Me.TextSeikyuuSakiBrc_2.Text <> Me.HiddenSeikyuuSakiBrcOld_2.Value _
                Or Me.SelectSeikyuuSakiKbn_2.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_2.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "請求先２")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_2)
            End If
        End If

        '請求先コード・枝番・区分のどれかに入力がある場合のみチェック
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_3.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_3.Text) Or _
           Me.SelectSeikyuuSakiKbn_2.SelectedIndex <> 0 Then

            '検索ボタン押下時と現在で差異があるのかチェック
            If Me.TextSeikyuuSakiCd_3.Text <> Me.HiddenSeikyuuSakiCdOld_3.Value _
                Or Me.TextSeikyuuSakiBrc_3.Text <> Me.HiddenSeikyuuSakiBrcOld_3.Value _
                Or Me.SelectSeikyuuSakiKbn_3.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_3.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "請求先３")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_3)
            End If
        End If

        '請求先コード・枝番・区分のどれかに入力がある場合のみチェック
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_4.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_4.Text) Or _
           Me.SelectSeikyuuSakiKbn_4.SelectedIndex <> 0 Then

            '検索ボタン押下時と現在で差異があるのかチェック
            If Me.TextSeikyuuSakiCd_4.Text <> Me.HiddenSeikyuuSakiCdOld_4.Value _
                Or Me.TextSeikyuuSakiBrc_4.Text <> Me.HiddenSeikyuuSakiBrcOld_4.Value _
                Or Me.SelectSeikyuuSakiKbn_4.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_4.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "請求先４")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_4)
            End If
        End If

        '請求先コード・枝番・区分のどれかに入力がある場合のみチェック
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_5.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_5.Text) Or _
           Me.SelectSeikyuuSakiKbn_5.SelectedIndex <> 0 Then

            '検索ボタン押下時と現在で差異があるのかチェック
            If Me.TextSeikyuuSakiCd_5.Text <> Me.HiddenSeikyuuSakiCdOld_5.Value _
                Or Me.TextSeikyuuSakiBrc_5.Text <> Me.HiddenSeikyuuSakiBrcOld_5.Value _
                Or Me.SelectSeikyuuSakiKbn_5.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_5.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "請求先５")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_5)
            End If
        End If

        '請求先コード・枝番・区分のどれかに入力がある場合のみチェック
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_6.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_6.Text) Or _
           Me.SelectSeikyuuSakiKbn_6.SelectedIndex <> 0 Then

            '検索ボタン押下時と現在で差異があるのかチェック
            If Me.TextSeikyuuSakiCd_6.Text <> Me.HiddenSeikyuuSakiCdOld_6.Value _
                Or Me.TextSeikyuuSakiBrc_6.Text <> Me.HiddenSeikyuuSakiBrcOld_6.Value _
                Or Me.SelectSeikyuuSakiKbn_6.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_6.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "請求先６")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_6)
            End If
        End If

        '請求先コード・枝番・区分のどれかに入力がある場合のみチェック
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_7.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_7.Text) Or _
           Me.SelectSeikyuuSakiKbn_7.SelectedIndex <> 0 Then

            '検索ボタン押下時と現在で差異があるのかチェック
            If Me.TextSeikyuuSakiCd_7.Text <> Me.HiddenSeikyuuSakiCdOld_7.Value _
                Or Me.TextSeikyuuSakiBrc_7.Text <> Me.HiddenSeikyuuSakiBrcOld_7.Value _
                Or Me.SelectSeikyuuSakiKbn_7.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_7.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "請求先７")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_7)
            End If
        End If

        '請求先コード・枝番・区分のどれかに入力がある場合のみチェック
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_8.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_8.Text) Or _
           Me.SelectSeikyuuSakiKbn_8.SelectedIndex <> 0 Then

            '検索ボタン押下時と現在で差異があるのかチェック
            If Me.TextSeikyuuSakiCd_8.Text <> Me.HiddenSeikyuuSakiCdOld_8.Value _
                Or Me.TextSeikyuuSakiBrc_8.Text <> Me.HiddenSeikyuuSakiBrcOld_8.Value _
                Or Me.SelectSeikyuuSakiKbn_8.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_8.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "請求先８")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_8)
            End If
        End If

        '請求先コード・枝番・区分のどれかに入力がある場合のみチェック
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_9.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_9.Text) Or _
           Me.SelectSeikyuuSakiKbn_9.SelectedIndex <> 0 Then

            '検索ボタン押下時と現在で差異があるのかチェック
            If Me.TextSeikyuuSakiCd_9.Text <> Me.HiddenSeikyuuSakiCdOld_9.Value _
                Or Me.TextSeikyuuSakiBrc_9.Text <> Me.HiddenSeikyuuSakiBrcOld_9.Value _
                Or Me.SelectSeikyuuSakiKbn_9.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_9.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "請求先９")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_9)
            End If
        End If

        '請求先コード・枝番・区分のどれかに入力がある場合のみチェック
        If Not String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_10.Text) Or _
           Not String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_10.Text) Or _
           Me.SelectSeikyuuSakiKbn_10.SelectedIndex <> 0 Then

            '検索ボタン押下時と現在で差異があるのかチェック
            If Me.TextSeikyuuSakiCd_10.Text <> Me.HiddenSeikyuuSakiCdOld_10.Value _
                Or Me.TextSeikyuuSakiBrc_10.Text <> Me.HiddenSeikyuuSakiBrcOld_10.Value _
                Or Me.SelectSeikyuuSakiKbn_10.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld_10.Value Then

                strErrMsg += Messages.MSG030E.Replace("@PARAM1", "請求先１０")
                arrFocusTargetCtrl.Add(btnSeikyuuSakiSearch_10)
            End If
        End If


    End Sub

    ''' <summary>
    ''' 日付取得ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGetDate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDate.ServerClick

        Dim strErrMsg As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList
        Dim strSetFocus As String = String.Empty
        Dim listSsi As New List(Of SeikyuuSakiInfoRecord)
        Dim strResultDate As String = String.Empty
        Dim flgResult As Integer = Integer.MinValue
        Dim intErrFlg As EarthEnum.emHidukeSyutokuErr = EarthEnum.emHidukeSyutokuErr.OK

        '●コード入力値変更チェック
        CheckSeikyuuChg(strErrMsg, arrFocusTargetCtrl)

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If strErrMsg <> "" Then

            '請求先指定を展開する
            Me.TBodySeikyuuInfo.Style("display") = "inline"
            Me.HiddenDispStyle.Value = "inline"

            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Exit Sub
        End If

        '請求先情報の取得
        SetSeikyuuInfo(listSsi)

        '請求書発行日・全対象FLGの初期値設定(最小締日を取得。取得できなかった場合、当日日付)
        lgcSeiDataSaku.getMinSeikyuuSimeDate(listSsi, strResultDate, flgResult, intErrFlg)

        '日付取得不可の場合はメッセージ表示
        If intErrFlg > EarthEnum.emHidukeSyutokuErr.OK Then
            Select Case intErrFlg
                Case EarthEnum.emHidukeSyutokuErr.SyutokuErr
                    strErrMsg = Messages.MSG196W.Replace("@PARAM1", "日付が取得出来なかった").Replace("@PARAM2", "請求書発行日").Replace("@PARAM3", "現在日時")
                Case EarthEnum.emHidukeSyutokuErr.SqlErr
                    strErrMsg = Messages.MSG196W.Replace("@PARAM1", "SQL発行エラーの").Replace("@PARAM2", "請求書発行日").Replace("@PARAM3", "現在日時")
                Case EarthEnum.emHidukeSyutokuErr.HidukeErr
                    strErrMsg = Messages.MSG196W.Replace("@PARAM1", "日付形式エラーの").Replace("@PARAM2", "請求書発行日").Replace("@PARAM3", "現在日時")
            End Select
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & strErrMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
        End If

        '請求書発行日セット
        Me.TextSeikyuusyoHakDate.Value = strResultDate
        '全締日、全請求先を作成対象チェックボックスセット
        If flgResult = "1" Then
            Me.CheckAllSakusei.Checked = True
        ElseIf flgResult = "0" Then
            Me.CheckAllSakusei.Checked = False
        End If

        '日付取得ボタン押下時に値を退避
        Me.HiddenPushValuesSeikyuu.Value = getCtrlValuesStringSeikyuu()

    End Sub

    ''' <summary>
    ''' 請求書データ作成ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSeikyuusyoDataSakusei_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSeikyuusyoDataSakusei.ServerClick

        Dim intDataCnt As Integer = 0
        Dim tmpScript As String = String.Empty
        Dim strResultMsg As String = String.Empty
        Dim strInfoMsg As String = String.Empty
        Dim strSeikyuusyoHakDate As String = String.Empty
        Dim blnAllSakuseiFlg As Boolean = False
        Dim listSsi As New List(Of SeikyuuSakiInfoRecord)

        '入力チェック
        If Not CheckInput() Then
            Exit Sub
        End If

        '請求先情報の取得
        SetSeikyuuInfo(listSsi)

        'プロパティの設定
        lgcSeiDataSaku.LoginUserId = userinfo.LoginUserId

        '画面項目の取得
        strSeikyuusyoHakDate = Me.TextSeikyuusyoHakDate.Value   '請求書発行日
        blnAllSakuseiFlg = Me.CheckAllSakusei.Checked     '請求書作成全対象フラグ

        '請求書データ作成処理
        strResultMsg = lgcSeiDataSaku.SeikyuusyoDataSakuseiSyori(strSeikyuusyoHakDate, blnAllSakuseiFlg, listSsi)

        '完了メッセージの表示
        If strResultMsg.Length > 0 Then
            tmpScript = "alert('" & strResultMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
        Else
            strInfoMsg = Messages.MSG018S.Replace("@PARAM1", "請求書データ作成")
            tmpScript = "alert('" & strInfoMsg & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' 請求先検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSeikyuuSakiSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim blnResult As Boolean
        Dim strTargetId As String = CType(sender, HtmlInputButton).ID       '実行元ボタンID
        Dim tmpSeikyuuSakicd As New TextBox                 '請求先コード
        Dim tmpSeikyuuSakiBrc As New TextBox                '請求先枝番
        Dim tmpSeikyuuSakiKbn As New DropDownList           '請求先区分
        Dim tmpSeikyuuSakiMei As New HtmlInputText          '請求先名
        Dim tmpSeikyuuSakiMeiHdn As New TextBox             '請求先名（Hiddenテキストボックス）
        Dim tmpSeikyuuSakiBtn As New HtmlInputButton        '請求先ボタン
        Dim hdnOldObj As HtmlInputHidden() = New HtmlInputHidden(2) {}

        'ボタン押下行により呼出対象を切り分け
        Select Case strTargetId
            Case Me.btnSeikyuuSakiSearch_1.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_1
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_1
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_1
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_1
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_1
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_1
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_1         '区分→コード→枝番の順
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_1
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_1
            Case Me.btnSeikyuuSakiSearch_2.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_2
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_2
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_2
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_2
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_2
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_2
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_2
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_2
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_2
            Case Me.btnSeikyuuSakiSearch_3.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_3
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_3
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_3
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_3
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_3
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_3
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_3
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_3
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_3
            Case Me.btnSeikyuuSakiSearch_4.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_4
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_4
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_4
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_4
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_4
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_4
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_4
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_4
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_4
            Case Me.btnSeikyuuSakiSearch_5.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_5
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_5
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_5
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_5
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_5
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_5
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_5
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_5
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_5
            Case Me.btnSeikyuuSakiSearch_6.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_6
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_6
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_6
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_6
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_6
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_6
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_6
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_6
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_6
            Case Me.btnSeikyuuSakiSearch_7.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_7
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_7
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_7
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_7
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_7
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_7
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_7
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_7
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_7
            Case Me.btnSeikyuuSakiSearch_8.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_8
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_8
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_8
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_8
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_8
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_8
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_8
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_8
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_8
            Case Me.btnSeikyuuSakiSearch_9.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_9
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_9
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_9
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_9
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_9
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_9
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_9
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_9
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_9
            Case Me.btnSeikyuuSakiSearch_10.ID
                tmpSeikyuuSakicd = Me.TextSeikyuuSakiCd_10
                tmpSeikyuuSakiBrc = Me.TextSeikyuuSakiBrc_10
                tmpSeikyuuSakiKbn = Me.SelectSeikyuuSakiKbn_10
                tmpSeikyuuSakiMei = Me.TextSeikyuuSakiMei_10
                tmpSeikyuuSakiMeiHdn = Me.TextSeikyuuSakiMeiHdn_10
                tmpSeikyuuSakiBtn = Me.btnSeikyuuSakiSearch_10
                hdnOldObj(0) = Me.HiddenSeikyuuSakiKbnOld_10
                hdnOldObj(1) = Me.HiddenSeikyuuSakiCdOld_10
                hdnOldObj(2) = Me.HiddenSeikyuuSakiBrcOld_10
        End Select


        '請求先検索画面呼出
        blnResult = cl.CallSeikyuuSakiSearchWindow(sender, e, Me _
                                                   , tmpSeikyuuSakiKbn _
                                                   , tmpSeikyuuSakicd _
                                                   , tmpSeikyuuSakiBrc _
                                                   , tmpSeikyuuSakiMeiHdn _
                                                   , tmpSeikyuuSakiBtn _
                                                   , hdnOldObj)
        If blnResult Then
            'フォーカスセット
            masterAjaxSM.SetFocus(tmpSeikyuuSakiBtn)
            tmpSeikyuuSakiMei.Value = tmpSeikyuuSakiMeiHdn.Text
        End If

    End Sub

    ''' <summary>
    ''' 請求先区分変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectSeikyuuSakiKbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim strTargetId As String = CType(sender, DropDownList).ID       '実行元コンボ

        'ボタン押下行より呼出対象を切り分け
        Select Case strTargetId
            Case Me.SelectSeikyuuSakiKbn_1.ID
                '請求先区分変更時に名称をクリアする
                Me.TextSeikyuuSakiMei_1.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_1)
            Case Me.SelectSeikyuuSakiKbn_2.ID
                Me.TextSeikyuuSakiMei_2.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_2)
            Case Me.SelectSeikyuuSakiKbn_3.ID
                Me.TextSeikyuuSakiMei_3.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_3)
            Case Me.SelectSeikyuuSakiKbn_4.ID
                Me.TextSeikyuuSakiMei_4.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_4)
            Case Me.SelectSeikyuuSakiKbn_5.ID
                Me.TextSeikyuuSakiMei_5.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_5)
            Case Me.SelectSeikyuuSakiKbn_6.ID
                Me.TextSeikyuuSakiMei_6.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_6)
            Case Me.SelectSeikyuuSakiKbn_7.ID
                Me.TextSeikyuuSakiMei_7.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_7)
            Case Me.SelectSeikyuuSakiKbn_8.ID
                Me.TextSeikyuuSakiMei_8.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_8)
            Case Me.SelectSeikyuuSakiKbn_9.ID
                Me.TextSeikyuuSakiMei_9.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_9)
            Case Me.SelectSeikyuuSakiKbn_10.ID
                Me.TextSeikyuuSakiMei_10.Value = String.Empty
                masterAjaxSM.SetFocus(SelectSeikyuuSakiKbn_10)
        End Select

    End Sub

    ''' <summary>
    ''' 請求先コード変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSeikyuuSakiCd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim strTargetId As String = CType(sender, TextBox).ID       '実行元テキスト

        'ボタン押下行より呼出対象を切り分け
        Select Case strTargetId
            Case Me.TextSeikyuuSakiCd_1.ID
                '請求先区分変更時に名称をクリアする
                Me.TextSeikyuuSakiMei_1.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_1)
            Case Me.TextSeikyuuSakiCd_2.ID
                Me.TextSeikyuuSakiMei_2.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_2)
            Case Me.TextSeikyuuSakiCd_3.ID
                Me.TextSeikyuuSakiMei_3.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_3)
            Case Me.TextSeikyuuSakiCd_4.ID
                Me.TextSeikyuuSakiMei_4.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_4)
            Case Me.TextSeikyuuSakiCd_5.ID
                Me.TextSeikyuuSakiMei_5.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_5)
            Case Me.TextSeikyuuSakiCd_6.ID
                Me.TextSeikyuuSakiMei_6.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_6)
            Case Me.TextSeikyuuSakiCd_7.ID
                Me.TextSeikyuuSakiMei_7.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_7)
            Case Me.TextSeikyuuSakiCd_8.ID
                Me.TextSeikyuuSakiMei_8.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_8)
            Case Me.TextSeikyuuSakiCd_9.ID
                Me.TextSeikyuuSakiMei_9.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_9)
            Case Me.TextSeikyuuSakiCd_10.ID
                Me.TextSeikyuuSakiMei_10.Value = String.Empty
                masterAjaxSM.SetFocus(TextSeikyuuSakiBrc_10)
        End Select

    End Sub

    ''' <summary>
    ''' 請求先枝番変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSeikyuuSakiBrc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim strTargetId As String = CType(sender, TextBox).ID       '実行元テキスト

        'ボタン押下行より呼出対象を切り分け
        Select Case strTargetId
            Case Me.TextSeikyuuSakiBrc_1.ID
                '請求先区分変更時に名称をクリアする
                Me.TextSeikyuuSakiMei_1.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_1)
            Case Me.TextSeikyuuSakiBrc_2.ID
                Me.TextSeikyuuSakiMei_2.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_2)
            Case Me.TextSeikyuuSakiBrc_3.ID
                Me.TextSeikyuuSakiMei_3.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_3)
            Case Me.TextSeikyuuSakiBrc_4.ID
                Me.TextSeikyuuSakiMei_4.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_4)
            Case Me.TextSeikyuuSakiBrc_5.ID
                Me.TextSeikyuuSakiMei_5.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_5)
            Case Me.TextSeikyuuSakiBrc_6.ID
                Me.TextSeikyuuSakiMei_6.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_6)
            Case Me.TextSeikyuuSakiBrc_7.ID
                Me.TextSeikyuuSakiMei_7.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_7)
            Case Me.TextSeikyuuSakiBrc_8.ID
                Me.TextSeikyuuSakiMei_8.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_8)
            Case Me.TextSeikyuuSakiBrc_9.ID
                Me.TextSeikyuuSakiMei_9.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_9)
            Case Me.TextSeikyuuSakiBrc_10.ID
                Me.TextSeikyuuSakiMei_10.Value = String.Empty
                masterAjaxSM.SetFocus(btnSeikyuuSakiSearch_10)
        End Select

    End Sub

    ''' <summary>
    ''' 画面から請求先情報を習得し、リストにセットする
    ''' </summary>
    ''' <param name="listSsi"></param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuInfo(ByRef listSsi As List(Of SeikyuuSakiInfoRecord))
        Dim recSeikyuu As New SeikyuuSakiInfoRecord

        'レコード単位で習得して、リストにセット
        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl1)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl2)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl3)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl4)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl5)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl6)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl7)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl8)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl9)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

        recSeikyuu = MakeSeikyuuInfoRec(emSsCtrl.intCtrl10)
        If Not recSeikyuu Is Nothing Then
            listSsi.Add(recSeikyuu)
        End If

    End Sub

    ''' <summary>
    ''' 画面から行単位で請求先情報を取得する
    ''' </summary>
    ''' <param name="emSsCtrl"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MakeSeikyuuInfoRec(ByVal emSsCtrl As emSsCtrl) As SeikyuuSakiInfoRecord
        Dim recTemp As New SeikyuuSakiInfoRecord

        Select Case emSsCtrl
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl1
                '1行目
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_1.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_1.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_1.SelectedValue) Then
                    'どれか一つでも無い場合は設定しない
                    Return Nothing
                Else
                    '請求先コード
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_1.Text, recTemp.SeikyuuSakiCd)
                    '請求先枝番
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_1.Text, recTemp.SeikyuuSakiBrc)
                    '請求先区分
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_1.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl2
                '2行目
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_2.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_2.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_2.SelectedValue) Then
                    'どれか一つでも無い場合は設定しない
                    Return Nothing
                Else
                    '請求先コード
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_2.Text, recTemp.SeikyuuSakiCd)
                    '請求先枝番
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_2.Text, recTemp.SeikyuuSakiBrc)
                    '請求先区分
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_2.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl3
                '3行目
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_3.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_3.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_3.SelectedValue) Then
                    'どれか一つでも無い場合は設定しない
                    Return Nothing
                Else
                    '請求先コード
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_3.Text, recTemp.SeikyuuSakiCd)
                    '請求先枝番
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_3.Text, recTemp.SeikyuuSakiBrc)
                    '請求先区分
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_3.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl4
                '4行目
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_4.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_4.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_4.SelectedValue) Then
                    'どれか一つでも無い場合は設定しない
                    Return Nothing
                Else
                    '請求先コード
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_4.Text, recTemp.SeikyuuSakiCd)
                    '請求先枝番
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_4.Text, recTemp.SeikyuuSakiBrc)
                    '請求先区分
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_4.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl5
                '5行目
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_5.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_5.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_5.SelectedValue) Then
                    'どれか一つでも無い場合は設定しない
                    Return Nothing
                Else
                    '請求先コード
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_5.Text, recTemp.SeikyuuSakiCd)
                    '請求先枝番
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_5.Text, recTemp.SeikyuuSakiBrc)
                    '請求先区分
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_5.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl6
                '6行目
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_6.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_6.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_6.SelectedValue) Then
                    'どれか一つでも無い場合は設定しない
                    Return Nothing
                Else
                    '請求先コード
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_6.Text, recTemp.SeikyuuSakiCd)
                    '請求先枝番
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_6.Text, recTemp.SeikyuuSakiBrc)
                    '請求先区分
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_6.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl7
                '7行目
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_7.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_7.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_7.SelectedValue) Then
                    'どれか一つでも無い場合は設定しない
                    Return Nothing
                Else
                    '請求先コード
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_7.Text, recTemp.SeikyuuSakiCd)
                    '請求先枝番
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_7.Text, recTemp.SeikyuuSakiBrc)
                    '請求先区分
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_7.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl8
                '8行目
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_8.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_8.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_8.SelectedValue) Then
                    'どれか一つでも無い場合は設定しない
                    Return Nothing
                Else
                    '請求先コード
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_8.Text, recTemp.SeikyuuSakiCd)
                    '請求先枝番
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_8.Text, recTemp.SeikyuuSakiBrc)
                    '請求先区分
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_8.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl9
                '9行目
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_9.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_9.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_9.SelectedValue) Then
                    'どれか一つでも無い場合は設定しない
                    Return Nothing
                Else
                    '請求先コード
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_9.Text, recTemp.SeikyuuSakiCd)
                    '請求先枝番
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_9.Text, recTemp.SeikyuuSakiBrc)
                    '請求先区分
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_9.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
            Case SeikyuusyoDataSakusei.emSsCtrl.intCtrl10
                '10行目
                If String.IsNullOrEmpty(Me.TextSeikyuuSakiCd_10.Text) Or _
                    String.IsNullOrEmpty(Me.TextSeikyuuSakiBrc_10.Text) Or _
                    String.IsNullOrEmpty(Me.SelectSeikyuuSakiKbn_10.SelectedValue) Then
                    'どれか一つでも無い場合は設定しない
                    Return Nothing
                Else
                    '請求先コード
                    cl.SetDisplayString(Me.TextSeikyuuSakiCd_10.Text, recTemp.SeikyuuSakiCd)
                    '請求先枝番
                    cl.SetDisplayString(Me.TextSeikyuuSakiBrc_10.Text, recTemp.SeikyuuSakiBrc)
                    '請求先区分
                    cl.SetDisplayString(Me.SelectSeikyuuSakiKbn_10.SelectedValue, recTemp.SeikyuuSakiKbn)
                End If
        End Select

        Return recTemp
    End Function

    ''' <summary>
    ''' 画面コントロール(請求先情報)の値を結合し、文字列化する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringSeikyuu() As String

        Dim sb As New StringBuilder

        sb.Append(Me.TextSeikyuuSakiCd_1.Text & EarthConst.SEP_STRING)                  '請求先コード１
        sb.Append(Me.TextSeikyuuSakiBrc_1.Text & EarthConst.SEP_STRING)                 '請求先枝番１
        sb.Append(Me.SelectSeikyuuSakiKbn_1.SelectedIndex & EarthConst.SEP_STRING)      '請求先区分１
        sb.Append(Me.TextSeikyuuSakiCd_2.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_2.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_2.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_3.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_3.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_3.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_4.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_4.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_4.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_5.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_5.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_5.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_6.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_6.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_6.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_7.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_7.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_7.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_8.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_8.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_8.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_9.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_9.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_9.SelectedIndex & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiCd_10.Text & EarthConst.SEP_STRING)
        sb.Append(Me.TextSeikyuuSakiBrc_10.Text & EarthConst.SEP_STRING)
        sb.Append(Me.SelectSeikyuuSakiKbn_10.SelectedIndex & EarthConst.SEP_STRING)

        Return (sb.ToString)

    End Function

End Class