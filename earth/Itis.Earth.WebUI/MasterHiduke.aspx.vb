
Partial Public Class MasterHiduke
    Inherits System.Web.UI.Page

    Dim user_info As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ユーザー基本認証
        jBn.UserAuth(user_info)

        '認証結果によって画面表示を切替える
        If user_info IsNot Nothing Then

            '保証書発行日編集可:  保証書権限
            If user_info.HosyouGyoumuKengen <> -1 Then
                TextHosyousyoHakkouHenkou.Style("display") = "none"
                ButtonHosyousyoHakkouHenkou.Disabled = True
            Else
                TextHosyousyoHakkouHenkou.Style("display") = "inline"
                ButtonHosyousyoHakkouHenkou.Disabled = False
            End If
            '報告書発送日編集可:  報告書権限
            If user_info.HoukokusyoGyoumuKengen <> -1 Then
                TextHoukokusyoHassouHenkou.Style("display") = "none"
                ButtonHoukokusyoHassouHenkou.Disabled = True
            Else
                TextHoukokusyoHassouHenkou.Style("display") = "inline"
                ButtonHoukokusyoHassouHenkou.Disabled = False
            End If
            '保証書NO年月編集可: 新規入力権限
            If user_info.SinkiNyuuryokuKengen <> -1 Then
                TextHosyousyoNoHenkou.Style("display") = "none"
                ButtonHosyousyoNoHenkou.Disabled = True
            Else
                TextHosyousyoNoHenkou.Style("display") = "inline"
                ButtonHosyousyoNoHenkou.Disabled = False
            End If

        Else
            Response.Redirect(UrlConst.MAIN)
        End If

        If IsPostBack = False Then

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            ' 区分コンボにデータをバインドする
            Dim helper As New DropDownHelper
            Dim kbn_logic As New KubunLogic
            helper.SetDropDownList(Me.SelectKubun, DropDownHelper.DropDownType.Kubun, False)

            ' 初期値A固定
            SelectKubun.SelectedValue = "A"

            SetHidukeData()

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            TextHosyousyoHakkouHenkou.Attributes("onblur") = "checkDate(this);"
            TextHoukokusyoHassouHenkou.Attributes("onblur") = "checkDate(this);"
            TextHosyousyoNoHenkou.Attributes("onblur") = "checkDateYm(this);"

            'フォーカス設定
            Me.SelectKubun.Focus()

        Else

        End If

    End Sub

    ''' <summary>
    ''' 保証書発行日ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHosyousyoHakkouHenkou_ServerClick(ByVal sender As System.Object, _
                                           ByVal e As System.EventArgs) _
                                           Handles ButtonHosyousyoHakkouHenkou.ServerClick
        Dim logic As New HidukeLogic
        Dim record As New HidukeSaveRecord

        If CheckInput() = False Then
            Exit Sub
        End If

        logic.SetDisplayString(SelectKubun.SelectedValue, record.Kbn)
        logic.SetDisplayString(TextHosyousyoHakkouHenkou.Text, record.HosyousyoHakDate)
        logic.SetDisplayString(TextHoukokusyoHassouDate.Text, record.HkksHassouDate)
        logic.SetDisplayString(TextHosyousyoNo.Text & "/01", record.HosyousyoNoNengetu)

        logic.SetDisplayString(user_info.LoginUserId, record.UpdLoginUserId)
        logic.SetDisplayString(HiddenUpdDateTime.Value, record.UpdDatetime)

        If logic.EditHidukeSaveRecord(sender, record) = True Then
            SetHidukeData()
        End If
    End Sub

    ''' <summary>
    ''' 報告書発送日ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHoukokusyoHassouHenkou_ServerClick(ByVal sender As System.Object, _
                                           ByVal e As System.EventArgs) _
                                           Handles ButtonHoukokusyoHassouHenkou.ServerClick

        Dim logic As New HidukeLogic
        Dim record As New HidukeSaveRecord

        If CheckInput() = False Then
            Exit Sub
        End If

        logic.SetDisplayString(SelectKubun.SelectedValue, record.Kbn)
        logic.SetDisplayString(TextHosyousyoHakkouDate.Text, record.HosyousyoHakDate)
        logic.SetDisplayString(TextHoukokusyoHassouHenkou.Text, record.HkksHassouDate)
        logic.SetDisplayString(TextHosyousyoNo.Text & "/01", record.HosyousyoNoNengetu)

        logic.SetDisplayString(user_info.LoginUserId, record.UpdLoginUserId)
        logic.SetDisplayString(HiddenUpdDateTime.Value, record.UpdDatetime)

        If logic.EditHidukeSaveRecord(sender, record) = True Then
            SetHidukeData()
        End If
    End Sub

    ''' <summary>
    ''' 保証書NO年月ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHosyousyoNoHenkou_ServerClick(ByVal sender As System.Object, _
                                           ByVal e As System.EventArgs) _
                                           Handles ButtonHosyousyoNoHenkou.ServerClick

        Dim logic As New HidukeLogic
        Dim record As New HidukeSaveRecord

        If CheckInput() = False Then
            Exit Sub
        End If

        logic.SetDisplayString(SelectKubun.SelectedValue, record.Kbn)
        logic.SetDisplayString(TextHosyousyoHakkouDate.Text, record.HosyousyoHakDate)
        logic.SetDisplayString(TextHoukokusyoHassouDate.Text, record.HkksHassouDate)
        logic.SetDisplayString(TextHosyousyoNoHenkou.Text & "/01", record.HosyousyoNoNengetu)

        logic.SetDisplayString(user_info.LoginUserId, record.UpdLoginUserId)
        logic.SetDisplayString(HiddenUpdDateTime.Value, record.UpdDatetime)

        If logic.EditHidukeSaveRecord(sender, record) = True Then
            SetHidukeData()
        End If

    End Sub


    ''' <summary>
    ''' 区分コンボ変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKubun_SelectedIndexChanged(ByVal sender As System.Object, _
                                                   ByVal e As System.EventArgs) _
                                                   Handles SelectKubun.SelectedIndexChanged
        SetHidukeData()
    End Sub

    ''' <summary>
    ''' DBより日付Saveマスタの情報を取得し設定する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHidukeData()

        Dim logic As New HidukeLogic

        Dim record As HidukeSaveRecord = logic.GetHidukeRecord(SelectKubun.SelectedValue)

        If Not record Is Nothing Then
            '保証書発行日
            TextHosyousyoHakkouDate.Text = logic.GetDisplayString(record.HosyousyoHakDate)
            TextHosyousyoHakkouHenkou.Text = TextHosyousyoHakkouDate.Text
            '報告書発送日
            TextHoukokusyoHassouDate.Text = logic.GetDisplayString(record.HkksHassouDate)
            TextHoukokusyoHassouHenkou.Text = TextHoukokusyoHassouDate.Text
            '保証書NO年月
            TextHosyousyoNo.Text = IIf(record.HosyousyoNoNengetu = DateTime.MinValue, "", record.HosyousyoNoNengetu.ToString("yyyy/MM"))
            TextHosyousyoNoHenkou.Text = TextHosyousyoNo.Text
            '更新日時
            HiddenUpdDateTime.Value = record.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)
        Else
            '保証書発行日
            TextHosyousyoHakkouDate.Text = String.Empty
            TextHosyousyoHakkouHenkou.Text = String.Empty
            '報告書発送日
            TextHoukokusyoHassouDate.Text = String.Empty
            TextHoukokusyoHassouHenkou.Text = String.Empty
            '保証書NO年月
            TextHosyousyoNo.Text = String.Empty
            TextHosyousyoNoHenkou.Text = String.Empty
            '更新日時
            HiddenUpdDateTime.Value = String.Empty
        End If

    End Sub


    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <returns>チェック結果 True:OK False:NG</returns>
    ''' <remarks></remarks>
    Private Function CheckInput() As Boolean

        'エラーメッセージ初期化
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New List(Of Control)

        '入力値チェック
        If TextHosyousyoHakkouHenkou.Text <> "" Then
            If DateTime.Parse(TextHosyousyoHakkouHenkou.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextHosyousyoHakkouHenkou.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "保証書発行日")
                arrFocusTargetCtrl.Add(TextHosyousyoHakkouHenkou)
            End If
        End If

        If TextHoukokusyoHassouHenkou.Text <> "" Then
            If DateTime.Parse(TextHoukokusyoHassouHenkou.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextHoukokusyoHassouHenkou.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "報告書発送日")
                arrFocusTargetCtrl.Add(TextHoukokusyoHassouHenkou)
            End If
        End If

        If TextHosyousyoNoHenkou.Text <> "" Then
            If DateTime.Parse(TextHosyousyoNoHenkou.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextHosyousyoNoHenkou.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "保証書NO年月")
                arrFocusTargetCtrl.Add(TextHosyousyoNoHenkou)
            End If
        End If

        If errMess <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        Return True

    End Function


End Class