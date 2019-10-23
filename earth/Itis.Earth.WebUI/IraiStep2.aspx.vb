
Partial Public Class IraiStep2
    Inherits System.Web.UI.Page

    Dim iraiSession As New IraiSession
    Dim sk As String = String.Empty
    Dim user_info As New LoginUserInfo
    Dim flgKengen As Boolean

    ''' <summary>
    ''' Page_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス
        Dim jSM As New JibanSessionManager

        ' ユーザー基本認証
        jBn.UserAuth(user_info)
        If user_info Is Nothing Then
            'ログイン情報が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        '何の権限もない場合、ダイレクト登録ボタンを隠す
        If (user_info.IraiGyoumuKengen + user_info.KekkaGyoumuKengen + user_info.HosyouGyoumuKengen + _
            user_info.HoukokusyoGyoumuKengen + user_info.KoujiGyoumuKengen + user_info.SinkiNyuuryokuKengen + _
            user_info.DataHakiKengen) = 0 Then
            flgKengen = False
            irai2_exeDirectTouroku.Disabled = True
            irai2_exeDirectTouroku_exe.Disabled = True
            irai2_exeDirectTouroku.Visible = False
            irai2_exeDirectTouroku_exe.Visible = False
        Else

        End If

        If Context.Items("irai") IsNot Nothing Then
            iraiSession = Context.Items("irai")
        End If

        If IsPostBack = False Then

            'ダイレクト登録ボタンのイベントハンドラ登録
            Dim tmpScript As String = "if(objEBI('" & HiddenAjaxFlg.ClientID & "').value!=1){if(confirm('" & Messages.MSG017C & "')){objEBI('" & irai2_exeDirectTouroku_exe.ClientID & "').click();setWindowOverlay(this);}else{return false;}}else{alert('" & Messages.MSG104E & "');}"
            irai2_exeDirectTouroku.Attributes("onclick") = tmpScript

        Else

        End If

        '画面モードをセッションに格納
        iraiSession.Irai1Mode = EarthConst.MODE_VIEW
        iraiSession.Irai2Mode = EarthConst.MODE_EDIT

        '登録ボタン制御用に商品1変更FLGを格納
        If Me.IraiCtrl2_1.AccHiddenTokutaiKkkHaneiFlg.Value <> String.Empty Then
            iraiSession.HiddenTokutaiKkkHaneiFlg = Me.IraiCtrl2_1.AccHiddenTokutaiKkkHaneiFlg.Value
        End If

        'コンテキストに値を格納
        Context.Items("irai") = iraiSession

    End Sub

    ''' <summary>
    ''' ページ描画前処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        '特別対応価格算出前は登録ボタンを非活性→商品1変更時
        If Me.IraiCtrl2_1.AccHiddenTokutaiKkkHaneiFlg.Value <> String.Empty Then
            irai2_exeDirectTouroku.Disabled = True
            irai2_exeDirectTouroku_exe.Disabled = True
        End If

        '子コントロールからの起点があるので、Updateする
        Me.UpdatePanelStep2RegBtn.Update()

    End Sub

    ''' <summary>
    ''' 確認へボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub irai2_next_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles irai2_next.ServerClick

        '入力チェック
        If IraiCtrl1_1.CheckInput() And IraiCtrl2_1.checkInput(True) Then

            'チェックOKの場合、画面遷移
            Dim stVal As String = iraiSession.IraiST
            If iraiSession.IraiST = EarthConst.MODE_NEW Or iraiSession.IraiST = EarthConst.MODE_NEW_EDIT Then
                stVal = EarthConst.MODE_NEW_EDIT
            End If

            iraiSession.IraiST = IIf(stVal Is Nothing, String.Empty, stVal)
            Context.Items("irai") = iraiSession
            Server.Transfer(UrlConst.IRAI_KAKUNIN)

        End If

    End Sub

    ''' <summary>
    ''' irai1ロード時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub IraiCtrl1_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IraiCtrl1_1.Load

        If IraiCtrl1_1.AccIraist.Value = EarthConst.MODE_NEW Then

            iraiSession.IraiST = EarthConst.MODE_NEW

        End If

        '新規登録で、確認画面からの戻り以外の場合、ダイレクト登録ボタンを非表示
        If (IraiCtrl1_1.AccIraist.Value = EarthConst.MODE_NEW Or IraiCtrl1_1.AccIraist.Value = EarthConst.MODE_NEW_EDIT) And _
            IraiCtrl1_1.AccKakuninopenflg.Value <> "true" Then
            irai2_exeDirectTouroku.Disabled = True
            irai2_exeDirectTouroku_exe.Disabled = True
            irai2_exeDirectTouroku.Visible = False
            irai2_exeDirectTouroku_exe.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' irai1のOyaGamenAction_hensyuで呼ばれる処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IraiCtrl1_1_OyaGamenAction_hensyu(ByVal sender As System.Object, ByVal e As System.EventArgs, ByRef flg As Boolean) Handles IraiCtrl1_1.OyaGamenAction_hensyu

        flg = IraiCtrl2_1.checkInput(True)

    End Sub

    ''' <summary>
    ''' ダイレクト登録処理ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub irai2_exeDirectTouroku_exe_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles irai2_exeDirectTouroku_exe.ServerClick

        '入力チェック
        If IraiCtrl1_1.CheckInput() And IraiCtrl2_1.checkInput(True) Then

            'チェックOKの場合、画面遷移
            Dim stVal As String = iraiSession.IraiST
            If iraiSession.IraiST = EarthConst.MODE_NEW Or iraiSession.IraiST = EarthConst.MODE_NEW_EDIT Then
                stVal = EarthConst.MODE_NEW_EDIT
            End If

            iraiSession.IraiST = IIf(stVal Is Nothing, String.Empty, stVal)
            iraiSession.ExeMode = EarthConst.MODE_EXE_DIRECT_TOUROKU
            Context.Items("irai") = iraiSession
            Server.Transfer(UrlConst.IRAI_KAKUNIN)

        End If

    End Sub
End Class