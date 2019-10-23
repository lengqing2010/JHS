
Partial Public Class IraiStep1
    Inherits System.Web.UI.Page

    Dim iraiSession As New IraiSession
    Dim sk As String = String.Empty
    Dim userInfo As New LoginUserInfo
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
        jBn.UserAuth(userInfo)
        If userInfo Is Nothing Then
            'ログイン情報が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        '依頼業務権限が無い場合、Step2への遷移ボタンを無効化
        If userInfo.IraiGyoumuKengen <> "-1" Then
            irai1_next.Visible = False
            irai1_next.Disabled = True
        End If

        '何の権限もない場合、ダイレクト登録ボタンを隠す
        If (userInfo.IraiGyoumuKengen + userInfo.KekkaGyoumuKengen + userInfo.HosyouGyoumuKengen + _
            userInfo.HoukokusyoGyoumuKengen + userInfo.KoujiGyoumuKengen + userInfo.SinkiNyuuryokuKengen + _
            userInfo.DataHakiKengen) = 0 Then
            flgKengen = False
            irai1_exeDirectTouroku.Disabled = True
            irai1_exeDirectTouroku_exe.Disabled = True
            irai1_exeDirectTouroku.Visible = False
            irai1_exeDirectTouroku_exe.Visible = False
        Else

        End If

        If Context.Items("irai") IsNot Nothing Then
            iraiSession = Context.Items("irai")
        End If

        If IsPostBack = False Then

            '画面呼び出しパラメータに「st=EarthConst.modeNew」がセットされている場合、セッションをクリアする
            If Request("st") = EarthConst.MODE_NEW And iraiSession.IraiST Is Nothing Then
                iraiSession.ExeMode = Nothing
                iraiSession.Irai1Data = Nothing
                iraiSession.Irai2Data = Nothing
                iraiSession.IraiST = EarthConst.MODE_NEW
                IraiCtrl1_1.AccIraist.Value = EarthConst.MODE_NEW
            Else
                IraiCtrl1_1.AccIraist.Value = iraiSession.IraiST
            End If

            'ダイレクト登録ボタンのイベントハンドラ登録
            Dim tmpScript As String = "if(confirm('" & Messages.MSG017C & "')){objEBI('" & irai1_exeDirectTouroku_exe.ClientID & "').click();setWindowOverlay(this);}else{return false;}"
            irai1_exeDirectTouroku.Attributes("onclick") = tmpScript

            '特別対応価格算出前は登録ボタンを非活性→商品1変更時
            If iraiSession IsNot Nothing Then
                If iraiSession.HiddenTokutaiKkkHaneiFlg <> String.Empty Then
                    irai1_exeDirectTouroku.Disabled = True
                    irai1_exeDirectTouroku_exe.Disabled = True
                End If
            End If

        Else

        End If

        '画面モードをセッションに格納
        iraiSession.Irai1Mode = EarthConst.MODE_EDIT
        iraiSession.Irai2Mode = EarthConst.MODE_VIEW

        'コンテキストに値を格納
        Context.Items("irai") = iraiSession

    End Sub


    ''' <summary>
    ''' 次へボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub irai1_next_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles irai1_next.ServerClick

        '入力チェック
        If IraiCtrl1_1.CheckInput() Then
            'チェックOKの場合、画面遷移
            Dim stVal As String = IraiCtrl1_1.AccIraist.Value

            If stVal = EarthConst.MODE_NEW Or stVal = EarthConst.MODE_NEW_EDIT Then
                stVal = EarthConst.MODE_NEW
            End If

            iraiSession.IraiST = IIf(stVal Is Nothing, String.Empty, stVal)
            Context.Items("irai") = iraiSession
            Server.Transfer(UrlConst.IRAI_STEP_2)
        End If

    End Sub

    ''' <summary>
    ''' 確認へボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub irai1_kakunin_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles irai1_kakunin.ServerClick

        '入力チェック
        If IraiCtrl1_1.CheckInput() Then

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

        '新規登録で、確認画面からの戻り以外の場合、「確認へ」ボタンを非表示
        If (IraiCtrl1_1.AccIraist.Value = EarthConst.MODE_NEW Or IraiCtrl1_1.AccIraist.Value = EarthConst.MODE_NEW_EDIT) And _
            IraiCtrl1_1.AccKakuninopenflg.Value <> "true" Then
            irai1_kakunin.Visible = False
            'ダイレクト登録ボタンも非表示にする
            irai1_exeDirectTouroku.Disabled = True
            irai1_exeDirectTouroku_exe.Disabled = True
            irai1_exeDirectTouroku.Visible = False
            irai1_exeDirectTouroku_exe.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' irai1のOyaGamenActionで呼ばれる処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IraiCtrl1_1_OyaGamenAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal irai1Mode As String, ByVal actMode As String) Handles IraiCtrl1_1.OyaGamenAction


    End Sub

    ''' <summary>
    ''' ダイレクト登録処理ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub irai1_exeDirectTouroku_exe_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles irai1_exeDirectTouroku_exe.ServerClick

        '入力チェック
        If IraiCtrl1_1.CheckInput() Then

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