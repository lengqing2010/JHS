
Partial Public Class IraiKakunin
    Inherits System.Web.UI.Page

#Region "メンバ変数"
    Dim iraiSession As New IraiSession
    Dim sk As String = String.Empty
    Dim user_info As New LoginUserInfo
    Dim flgKengen As Boolean
    Dim cl As New CommonLogic
    Dim cbLogic As New CommonBizLogic
    'メッセージロジック
    Dim mLogic As New MessageLogic
    Dim jBn As New Jiban '地盤画面共通クラス
    Dim jSM As New JibanSessionManager
#End Region

    ''' <summary>
    ''' Page_Load
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
        flgKengen = True

        If Context.Items("irai") IsNot Nothing Then
            iraiSession = Context.Items("irai")
        End If

        If Request("st") IsNot Nothing Then
            st.Value = Request("st")
        End If

        '削除権限が無い場合、削除ボタンを非表示
        If user_info.SystemKanrisyaKengen = 0 Then
            btn_sakujo.Visible = False
            btn_sakujo2.Visible = False
            btn_sakujo.Disabled = True
            btn_sakujo2.Disabled = True
        End If
        '何の権限もない場合、登録ボタンを隠す
        If (user_info.IraiGyoumuKengen + user_info.KekkaGyoumuKengen + user_info.HosyouGyoumuKengen + _
            user_info.HoukokusyoGyoumuKengen + user_info.KoujiGyoumuKengen + user_info.SinkiNyuuryokuKengen + _
            user_info.DataHakiKengen) = 0 Then
            flgKengen = False
            btn_shinkiTouroku.Disabled = True
            btn_shinkiTouroku2.Disabled = True
            btn_pdf.Disabled = True
            btn_pdf2.Disabled = True
            btn_tyousaMitsumorisyoSakusei.Disabled = True
            btn_shinkiTouroku.Visible = False
            btn_shinkiTouroku2.Visible = False
            btn_pdf.Visible = False
            btn_pdf2.Visible = False
            btn_tyousaMitsumorisyoSakusei.Visible = False

            Me.ChgDispKyoutuuCopy(False)
        Else
            Me.ChgDispKyoutuuCopy(True)
        End If

        If IsPostBack = False Then

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            ' コンボ設定ヘルパークラスを生成
            Dim helper As New DropDownHelper
            'ダミードロップダウンリストの生成
            helper.SetDropDownList(Me.SelectKubunDummy, DropDownHelper.DropDownType.Kubun2, False)
            ' 区分コンボにデータをバインドする
            Me.CreateDropDownList(Me.SelectKubunDummy, Me.SelectKubunCopy)

            'ボタン押下イベントの設定
            setBtnEvent()

            If Request.Url.PathAndQuery.IndexOf(UrlConst.IRAI_STEP_1) = -1 And _
               Request.Url.PathAndQuery.IndexOf(UrlConst.IRAI_STEP_2) = -1 And _
               st.Value = EarthConst.MODE_VIEW Then
                '参照モードの場合、地盤レコードを取得
                'セッションクリア
                Dim logic As New JibanLogic
                Dim jibanRec As New JibanRecord
                jibanRec = logic.GetJibanData(Request("sendPage_kubun"), Request("sendPage_hosyoushoNo"))
                iraiSession.JibanData = jibanRec

                '地盤データが存在しない場合
                If logic.ExistsJibanData(Request("sendPage_kubun"), Request("sendPage_hosyoushoNo")) = False Then
                    cl.CloseWindow(Me)
                    Me.btn_shinkiTouroku.Visible = False
                    Me.btn_shinkiHikitugi.Visible = False
                    Me.btn_shinkiRenzoku.Visible = False
                    Me.btn_sakujo.Visible = False
                    Me.btn_pdf.Visible = False
                    Me.ButtonKyoutuuInfoCopy.Visible = False
                    Me.btn_tyousaMitsumorisyoSakusei.Visible = False
                End If

                '特別対応価格反映状況確認
                Dim strRet As String = cbLogic.GetTokubetuTaiouKkkNoneSetUp(jibanRec, EarthEnum.emGetDispType.MEISYOU)
                If strRet <> String.Empty Then
                    strRet = strRet.Replace(EarthConst.SEP_STRING, EarthConst.CRLF_CODE)
                    Dim strTmpScript As String = "alert('" & Messages.MSG201W.Replace("@PARAM1", strRet) & "');"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GetTokubetuTaiouKkkNoneSetUp", strTmpScript, True)
                End If

            End If

            If btn_shinkiTouroku.Disabled = False Then
                '登録/修正ボタンにフォーカス
                btn_shinkiTouroku.Focus()
            End If

            '画面モードをセッションに格納
            iraiSession.Irai1Mode = EarthConst.MODE_KAKUNIN
            iraiSession.Irai2Mode = EarthConst.MODE_KAKUNIN

            'セッションからexeModeKeyを削除（ダイレクト登録の場合は除く）
            If iraiSession.ExeMode <> EarthConst.MODE_EXE_DIRECT_TOUROKU Then
                iraiSession.ExeMode = Nothing
            End If

        Else

            'セッションからexeModeKeyを削除
            iraiSession.ExeMode = Nothing

            '押下元ボタンによって、処理を実行
            Select Case actBtnId.Value
                Case btn_shinkiTouroku.ClientID, btn_shinkiTouroku2.ClientID
                    exeTouroku()
                Case btn_shinkiHikitugi.ClientID, btn_shinkiHikitugi2.ClientID
                    exeHikitugi()
                Case btn_shinkiRenzoku.ClientID, btn_shinkiRenzoku2.ClientID
                    exeRenzoku()
                Case Me.ButtonKyoutuuInfoCopy.ClientID
                    exeKyoutuuCopy()
                Case btn_sakujo.ClientID, btn_sakujo2.ClientID
                    exeSakujo()
                Case btn_pdf.ClientID, btn_pdf2.ClientID
                    exePDFRenraku()
                Case btn_tyousaMitsumorisyoSakusei.ClientID
                    exeTyousaMitsumorisyoSakusei()
                Case Else
                    exeOther()
            End Select

        End If

        '画面モードをセッションに格納
        iraiSession.Irai1Mode = EarthConst.MODE_KAKUNIN
        iraiSession.Irai2Mode = EarthConst.MODE_KAKUNIN

        'コンテキストに値を格納
        Context.Items("irai") = iraiSession

    End Sub

    ''' <summary>
    ''' Page_LoadComplete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    ''' <summary>
    ''' ページ描画前処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        '特別対応ボタンの色替え処理
        ChgDispTokubetuTaiou()
    End Sub


    ''' <summary>
    ''' ボタン設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setBtnEvent()

        '表示設定
        btn_shinkiHikitugi.Visible = False
        btn_shinkiHikitugi2.Visible = False
        btn_shinkiHikitugi.Disabled = True
        btn_shinkiHikitugi2.Disabled = True

        btn_shinkiRenzoku.Visible = False
        btn_shinkiRenzoku2.Visible = False
        btn_shinkiRenzoku.Disabled = True
        btn_shinkiRenzoku2.Disabled = True

        btn_pdf.Disabled = True
        btn_pdf2.Disabled = True
        btn_tyousaMitsumorisyoSakusei.Disabled = True

        If st.Value = EarthConst.MODE_NEW Then
            '新規の場合、新規(引継)ボタンを表示
            btn_shinkiHikitugi.Visible = True
            btn_shinkiHikitugi2.Visible = True
            btn_shinkiRenzoku.Visible = True
            btn_shinkiRenzoku2.Visible = True

            Me.ChgDispKyoutuuCopy(False)

        ElseIf st.Value = EarthConst.MODE_VIEW Then
            '参照モードの場合、PDF出力ボタンを有効化
            btn_pdf.Disabled = False
            btn_pdf2.Disabled = False
            btn_tyousaMitsumorisyoSakusei.Disabled = False

        End If

        'イベントハンドラ登録
        Dim tmpScript As String = "actClickButton(this);"
        Dim tmpScript_sinki As String = "actSinki(this);"

        btn_shinkiTouroku.Attributes("onclick") = tmpScript
        btn_shinkiTouroku2.Attributes("onclick") = tmpScript
        btn_shinkiHikitugi.Attributes("onclick") = tmpScript_sinki
        btn_shinkiHikitugi2.Attributes("onclick") = tmpScript_sinki
        btn_shinkiRenzoku.Attributes("onclick") = tmpScript_sinki
        btn_shinkiRenzoku2.Attributes("onclick") = tmpScript_sinki
        btn_sakujo.Attributes("onclick") = tmpScript
        btn_sakujo2.Attributes("onclick") = tmpScript
        btn_pdf.Attributes("onclick") = tmpScript
        btn_pdf2.Attributes("onclick") = tmpScript
        btn_tyousaMitsumorisyoSakusei.Attributes("onclick") = tmpScript
        Me.ButtonHiddenSyouhinReloadPre.Attributes("onclick") = "callTokutaiKkk(this);"

        '特別対応
        cl.getTokubetuTaiouLinkPathJT(Me.ButtonTokubetuTaiou, _
                                     user_info, _
                                     Me.IraiCtrl1_1.AccCbokubun.ClientID, _
                                     Me.IraiCtrl1_1.AccHoshouno.ClientID, _
                                     Me.IraiCtrl2_1.AccTxtKameitenCd.ClientID, _
                                     Me.IraiCtrl2_1.AcCboTysHouhou.ClientID, _
                                     Me.IraiCtrl2_1.AccCboSyouhin1.ClientID, _
                                     Me.IraiCtrl2_1.AccHiddenKakuteiValuesTokubetu.ClientID, _
                                     Me.ButtonTokubetuTaiou.ClientID, _
                                     EarthEnum.emTokubetuTaiouSearchType.IraiKakunin, _
                                     Me.IraiCtrl2_1.AccHiddenTokutaiKkkHaneiFlgPu.ClientID, _
                                     Me.IraiCtrl2_1.AccHiddenChgTokuCd.ClientID, _
                                     Me.IraiCtrl2_1.AccHiddenRentouBukkenSuu.ClientID, _
                                     Me.ButtonHiddenSyouhinReloadPre.ClientID)

    End Sub

    ''' <summary>
    ''' 登録/修正実行ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btn_shinkiTouroku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_shinkiTouroku.ServerClick
        sender.focus()
    End Sub

    Protected Sub btn_shinkiTouroku2_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_shinkiTouroku2.ServerClick
        sender.focus()
    End Sub

    Private Sub exeTouroku()

        '入力チェック
        If IraiCtrl1_1.CheckInput() And IraiCtrl2_1.checkInput(True) Then
            iraiSession.ExeMode = EarthConst.MODE_EXE_TOUROKU
        Else
            actBtnId.Value = ""
        End If

    End Sub

    ''' <summary>
    ''' 新規（引継）登録ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btn_shinkiHikitugi_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_shinkiHikitugi.ServerClick
        sender.focus()
    End Sub

    Protected Sub btn_shinkiHikitugi2_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_shinkiHikitugi2.ServerClick
        sender.focus()
    End Sub

    ''' <summary>
    ''' 共通情報コピーボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKyoutuuInfoCopy_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKyoutuuInfoCopy.ServerClick
        sender.focus()
    End Sub

    Private Sub exeKyoutuuCopy()
        iraiSession.ExeMode = EarthConst.MODE_EXE_COPY
        iraiSession.CopyKbn = Me.SelectKubunCopy.SelectedValue
    End Sub

    Private Sub exeHikitugi()

        '新規引継モード(確認画面遷移時に使用)
        iraiSession.ExeMode = EarthConst.MODE_EXE_HIKITUGI

        '特別対応価格反映前には、登録ボタンを使用不可（非活性化）にする
        iraiSession.HiddenTokutaiKkkHaneiFlg = "0"      'Step1用
        Me.IraiCtrl2_1.AccHiddenTokutaiKkkHaneiFlg.Value = "0"
        Me.IraiCtrl2_1.AccHiddenTokutaiPreMode.Value = EarthConst.MODE_EXE_HIKITUGI

        '特別対応ツールチップ 新規引継用
        jSM.Ctrl2Hash(Me.IraiCtrl2_1, jBn.IraiData)             '最新の画面値をHashtableの格納(確認画面で変更がある為)
        Me.IraiCtrl1_1.AccIrai2DataStr.Value = String.Empty     '初期化
        '確認画面で変更があるので、最新のCtrl値をStringに再退避
        jSM.CtrlHash2Str(jBn.IraiData, Me.IraiCtrl1_1.AccIrai2DataStr.Value)

    End Sub

    ''' <summary>
    ''' 新規（連続）登録ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btn_shinkiRenzoku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_shinkiRenzoku.ServerClick
        sender.focus()
    End Sub

    Protected Sub btn_shinkiRenzoku2_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_shinkiRenzoku2.ServerClick
        sender.focus()
    End Sub

    Private Sub exeRenzoku()
        iraiSession.ExeMode = EarthConst.MODE_EXE_RENZOKU

    End Sub

    ''' <summary>
    ''' 削除ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btn_sakujo_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_sakujo.ServerClick
        sender.focus()
    End Sub

    Protected Sub btn_sakujo2_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_sakujo2.ServerClick
        sender.focus()
    End Sub

    Private Sub exeSakujo()
        iraiSession.ExeMode = EarthConst.MODE_EXE_SAKUJO

    End Sub

    ''' <summary>
    ''' 調査予定連絡書表示ボタン押下時の処理[ボタン１](ポストバック発生用)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btn_pdf_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_pdf.ServerClick
        sender.focus()
    End Sub

    ''' <summary>
    ''' 調査予定連絡書表示ボタン押下時の処理[ボタン２](ポストバック発生用)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btn_pdf2_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_pdf2.ServerClick
        sender.focus()
    End Sub

    ''' <summary>
    ''' 調査予定連絡書表示ボタン押下時の処理(実態)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub exePDFRenraku()
        '入力チェック
        If IraiCtrl1_1.CheckInput() And IraiCtrl2_1.checkInput(True) Then
            iraiSession.ExeMode = EarthConst.MODE_EXE_PDF_RENRAKU
        Else
            actBtnId.Value = ""
        End If

    End Sub

    ''' <summary>
    ''' 調査予定連絡票プレビューウィンドウ起動(帳票サーバへのリダイレクト)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub pdfRenrakuOpen(ByVal sender As System.Object)

        Dim kbn As String = IraiCtrl1_1.AccCbokubun.SelectedValue
        Dim hosyouno As String = IraiCtrl1_1.AccHoshouno.Value
        Dim accountno As String = user_info.AccountNo
        Dim stdate As String = IraiCtrl1_1.AccShoudakuChousaDate.Value

        'dat作成
        '帳票データファイル
        Dim nitiji As String = Replace(System.DateTime.Now, "/", "")
        nitiji = Replace(nitiji, ":", "")
        Dim dataFileName As String = kbn & hosyouno & Replace(nitiji, " ", "") & ".dat"

        '帳票定義ファイル(フォームデータ)
        Dim fcpFileName As String = "jiban.fcp"
        '帳票出力データファイル作成
        Dim status As FcwManager.FileWriteStatus = FcwManager.Instance.CreateFcwDataFile(sender, _
                                                                                         dataFileName, _
                                                                                         kbn, _
                                                                                         hosyouno, _
                                                                                         accountno, _
                                                                                         stdate)

        If status.Equals(Itis.Earth.BizLogic.FcwManager.FileWriteStatus.OK) Then
            '帳票出力データファイルが正常に作成された場合、帳票サーバへ出力指示するためのURLを生成
            Dim redirectUrl As String = FcwManager.Instance.CreateResponseRedirectString(FcwManager.FcwDriverType.CSM, _
                                                                                         fcpFileName, _
                                                                                         dataFileName)

            '帳票サーバへリダイレクトするためのURLを、JSのグローバル変数「redirectUrl」にセットする
            '(受注画面再描画後にリダイレクトをかけるために、JSでリダイレクトを行う準備)
            Dim tmpPdfScript As String = "redirectUrl = """ & redirectUrl & """;"
            ScriptManager.RegisterStartupScript(sender, sender.GetType(), "pdfRenraku", tmpPdfScript, True)

        Else
            'データファイル作成で問題が発生した場合、アラートを表示
            mLogic.AlertMessage(sender, String.Format(Messages.MSG121E, status.ToString), 1, "PDFException")

        End If

    End Sub

    ''' <summary>
    ''' 登録＆調査見積書作成ボタン押下時の処理(ポストバック発生用)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btn_tyousaMitsumorisyoSakusei_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tyousaMitsumorisyoSakusei.ServerClick
        'sender.focus()
    End Sub

    ''' <summary>
    ''' 登録＆調査見積書作成ボタン押下時の処理(実態)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub exeTyousaMitsumorisyoSakusei()
        '入力チェック
        If IraiCtrl1_1.CheckInput() And IraiCtrl2_1.checkInput(True) Then
            iraiSession.ExeMode = EarthConst.MODE_EXE_TYOUSAMITSUMORISYO_SAKUSEI
        Else
            actBtnId.Value = ""
        End If

    End Sub

    ''' <summary>
    ''' 調査見積書作成指示画面の呼び出し(パラメータ渡し)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub tyousaMitsumorisyoSakuseiOpen(ByVal sender As System.Object)

        Dim tmpScript As String = String.Empty
        'パラメータの取得
        Dim kbn As String = IraiCtrl1_1.AccCbokubun.SelectedValue
        Dim hosyouno As String = IraiCtrl1_1.AccHoshouno.Value

        tmpScript = "var newWin = window.open('" & UrlConst.EARTH2_TYOUSA_MITSUMORISYO_SAKUSEI & "?sendSearchTerms=" & kbn & "$$$" & hosyouno & _
            "','searchWindow','menubar=no,toolbar=no,location=no,status=no,resizable=no,scrollbars=no,width=630px,height=450px,left=0px,top=0px');"
        tmpScript += "newWin.focus();"
        ScriptManager.RegisterStartupScript(sender, sender.GetType(), "tyousaMitsumorisyoSakusei", tmpScript, True)

    End Sub

    ''' <summary>
    ''' その他実行時処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub exeOther()
        iraiSession.ExeMode = "other"

    End Sub

    ''' <summary>
    ''' irai1ロード時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub IraiCtrl1_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IraiCtrl1_1.Load


        'irai1画面の確認画面を一度でも開いたかフラグに値をセット
        IraiCtrl1_1.AccKakuninopenflg.Value = "true"

        '最終更新者、最終更新日時をセット
        saishuuKousinSha.Value = IraiCtrl1_1.AccLastupdateusernm.Value
        saishuuKousinDate.Value = IraiCtrl1_1.AccLastupdatedatetime.Value

        '区分Eの場合、調査予定連絡書ボタンを無効化
        If IraiCtrl1_1.AccCboKubun.SelectedValue = "E" Then
            btn_pdf.Disabled = True
            btn_pdf2.Disabled = True
        Else
            btn_pdf.Disabled = False
            btn_pdf2.Disabled = False
        End If

        '何の権限もない場合、編集ボタンを隠す
        If flgKengen = False Then
            IraiCtrl1_1.AccBtn_Irai1.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' irai2ロード時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub IraiCtrl2_1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IraiCtrl2_1.Load

        '登録＆調査見積書作成ボタンの活性化制御
        btn_tyousaMitsumorisyoSakusei.Disabled = False
        '価格が原価M、販売価格設定Mに設定されていない場合、または、工務店請求0円＆実請求0円時、非活性化
        If ((IraiCtrl2_1.AcckoumutenSeikyuZeinukiGaku_1_1.Value = String.Empty) AndAlso (IraiCtrl2_1.AccjituSeikyuZeinukiGaku_1_1.Value = String.Empty)) _
            OrElse (IraiCtrl2_1.AcckoumutenSeikyuZeinukiGaku_1_1.Value = 0) AndAlso (IraiCtrl2_1.AccjituSeikyuZeinukiGaku_1_1.Value = 0) Then
            btn_tyousaMitsumorisyoSakusei.Disabled = True
        End If

        '何の権限もない場合か、依頼業務権限が無い場合は、編集ボタンを隠す
        If flgKengen = False Or user_info.IraiGyoumuKengen <> "-1" Then
            IraiCtrl2_1.AccBtn_Irai2.Disabled = True
            IraiCtrl2_1.AccBtn_Irai2.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' irai2のOyaGamenActionで呼ばれる処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IraiCtrl2_1_OyaActAtAfterExe(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs, _
                                            ByVal irai2Mode As String, _
                                            ByVal actMode As String, _
                                            ByVal exeMode As String, _
                                            ByVal result As Boolean, _
                                            ByVal jibanRecAfterExe As JibanRecord) Handles IraiCtrl2_1.OyaActAtAfterExe
        Dim jSM As New JibanSessionManager
        'メッセージ出力用スクリプト
        Dim tmpScript As String = ""

        '処理
        If irai2Mode = EarthConst.MODE_KAKUNIN Then

            If actMode = EarthConst.MODE_EDIT Then
            End If

            '登録ボタン押下時の終了処理
            If exeMode = EarthConst.MODE_EXE_TOUROKU Or exeMode = EarthConst.MODE_EXE_PDF_RENRAKU Or _
               exeMode = EarthConst.MODE_EXE_TYOUSAMITSUMORISYO_SAKUSEI Then

                '正常時
                If result Then

                    '処理件数 >= 連棟物件数(全処理終了)
                    If jibanRecAfterExe.SyoriKensuu >= CInt(IraiCtrl1_1.AccRentouBukkenSuu.Value) Then

                        '登録成功の場合、ボタン表示を切替える
                        If st.Value = EarthConst.MODE_NEW Then
                            '新規の場合、新規引継ボタンを有効化
                            btn_shinkiHikitugi.Disabled = False
                            btn_shinkiHikitugi2.Disabled = False
                            btn_shinkiRenzoku.Disabled = False
                            btn_shinkiRenzoku2.Disabled = False

                            Me.ChgDispKyoutuuCopy(False)
                        End If

                        tourokuKanryouFlg.Value = Boolean.TrueString

                        '地盤更新後に再取得しておいた地盤データを、画面に表示しなおす
                        IraiCtrl1_1.setIrai1FromJibanRec(sender, e, jibanRecAfterExe)
                        IraiCtrl2_1.setIrai2FromJibanRec(sender, e, jibanRecAfterExe)
                        'Irai1画面コントロールに値をセット
                        jSM.Hash2Ctrl(IraiCtrl1_1, iraiSession.Irai1Mode, iraiSession.Irai1Data)
                        'Irai2画面コントロールに値をセット
                        jSM.Hash2Ctrl(IraiCtrl2_1, iraiSession.Irai2Mode, iraiSession.Irai2Data)
                        '最終更新者、最終更新日時をセット
                        saishuuKousinSha.Value = IraiCtrl1_1.AccLastupdateusernm.Value
                        saishuuKousinDate.Value = IraiCtrl1_1.AccLastupdatedatetime.Value

                        '調査予定連絡書表示実行時
                        If exeMode = EarthConst.MODE_EXE_PDF_RENRAKU Then
                            '帳票出力のためのデータ作成と、帳票サーバへのリダイレクト準備
                            '(リダイレクト先URLをJSグローバル変数にセット)を行う
                            pdfRenrakuOpen(sender)
                            '登録＆調査見積書作成時
                        ElseIf exeMode = EarthConst.MODE_EXE_TYOUSAMITSUMORISYO_SAKUSEI Then
                            '調査見積書作成指示画面の呼び出し(パラメータ渡し)
                            tyousaMitsumorisyoSakuseiOpen(sender)
                        Else
                            If st.Value <> EarthConst.MODE_NEW Then
                                '登録・更新成功時には、ウィンドウを閉じる
                                tmpScript = "if(window.name != '" & EarthConst.MAIN_WINDOW_NAME & "')window.close();"
                                ScriptManager.RegisterStartupScript(sender, sender.GetType(), "close", tmpScript, True)
                            ElseIf IraiCtrl1_1.AccRentouBukkenSuu.Value > "1" Then
                                '連棟物件登録完了時には、メッセージを表示
                                tmpScript = "alert('" & Messages.MSG018S.Replace("@PARAM1", "登録/修正") & "');"
                                ScriptManager.RegisterStartupScript(sender, sender.GetType(), "err", tmpScript, True)
                            End If
                        End If

                        IraiCtrl1_1.AccSyoriKensuu.Value = "0" '処理件数を初期化
                        IraiCtrl1_1.AccRentouBukkenSuu.Value = "1" '連棟物件数を初期化
                        IraiCtrl2_1.AccHiddenTokutaiKkkHaneiFlgPu.Value = String.Empty    '計算処理フラグを初期化
                        IraiCtrl2_1.AccHiddenChgTokuCd.Value = String.Empty             '特別対応ツールチップ変更フラグを初期化
                        IraiCtrl2_1.AccHiddenChgTokuUpdDatetime.Value = String.Empty    '特別対応ツールチップ更新日時を初期化

                        'モードを設定
                        IraiCtrl1_1.AccIraist.Value = EarthConst.MODE_VIEW
                        iraiSession.IraiST = EarthConst.MODE_VIEW

                    Else '処理件数 < 連棟物件数(未処理データあり)
                        If actBtnId.Value = String.Empty Then
                            actBtnId.Value = btn_shinkiTouroku.ClientID
                        End If
                        IraiCtrl1_1.AccSyoriKensuu.Value = CStr(jibanRecAfterExe.SyoriKensuu) '処理件数をセット
                        '連続登録用フラグをセット
                        HiddenCallRentouNextFlg.Value = Boolean.TrueString
                        Exit Sub
                    End If

                Else '異常時
                    btn_shinkiHikitugi.Disabled = True
                    btn_shinkiHikitugi2.Disabled = True
                    btn_shinkiRenzoku.Disabled = True
                    btn_shinkiRenzoku2.Disabled = True
                    tourokuKanryouFlg.Value = Boolean.FalseString

                    tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "登録/修正") & "');"
                    ScriptManager.RegisterStartupScript(sender, sender.GetType(), "err", tmpScript, True)

                End If

            End If

            '削除ボタン押下時の終了処理
            If exeMode = EarthConst.MODE_EXE_SAKUJO Then
                If result Then
                    tourokuKanryouFlg.Value = Boolean.TrueString
                    tmpScript = "alert('" & Messages.MSG018S.Replace("@PARAM1", "削除") & "');"
                    ScriptManager.RegisterStartupScript(sender, sender.GetType(), "err", tmpScript, True)

                    '各種ボタンを無効化
                    btn_shinkiTouroku.Disabled = True
                    btn_shinkiTouroku2.Disabled = True
                    btn_shinkiHikitugi.Disabled = True
                    btn_shinkiHikitugi2.Disabled = True
                    btn_shinkiRenzoku.Disabled = True
                    btn_shinkiRenzoku2.Disabled = True
                    btn_sakujo.Disabled = True
                    btn_sakujo2.Disabled = True
                    btn_pdf.Disabled = True
                    btn_pdf2.Disabled = True
                    btn_tyousaMitsumorisyoSakusei.Disabled = True

                    Me.ChgDispKyoutuuCopy(False)

                    Me.SelectKubunCopy.Visible = False
                    Me.ButtonKyoutuuInfoCopy.Visible = False

                    '全てのコントロールを無効化()
                    Dim jBn As New Jiban
                    Dim delh As New Hashtable
                    jBn.ChangeDesabledAll(IraiCtrl1_1, True, delh)
                    jBn.ChangeDesabledAll(IraiCtrl2_1, True, delh)
                Else
                    tourokuKanryouFlg.Value = Boolean.FalseString
                    tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "削除") & "');"
                    ScriptManager.RegisterStartupScript(sender, sender.GetType(), "err", tmpScript, True)
                End If

            End If


            '確認画面での実行ボタン押下状況をクリア
            actBtnId.Value = ""

        End If

    End Sub

    ''' <summary>
    ''' ダミードロップダウンリストの生成
    ''' </summary>
    ''' <param name="SelectMoto">対象元ドロップダウンリスト</param>
    ''' <param name="SelectSaki">対象先ドロップダウンリスト</param>
    ''' <remarks></remarks>
    Private Sub CreateDropDownList(ByRef SelectMoto As DropDownList, ByRef SelectSaki As DropDownList)
        Dim helper As New DropDownHelper
        Dim intCnt As Integer = 0
        Dim strValue As String

        If SelectMoto.Items.Count <= 0 Then
            Dim strMsg As String = Messages.MSG113E.Replace("@PARAM1", "区分")
            Dim tmpScript As String = "alert('" & strMsg & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CreateDropDownList", tmpScript, True)
            Exit Sub
        End If

        For intCnt = 0 To SelectMoto.Items.Count - 1
            strValue = SelectMoto.Items(intCnt).Value 'Value値取得
            SelectSaki.Items.Add(strValue)
        Next

    End Sub

    ''' <summary>
    ''' 共通情報コピーボタンまわりの表示切替処理
    ''' </summary>
    ''' <param name="blnVisible">表示有無</param>
    ''' <remarks></remarks>
    Private Sub ChgDispKyoutuuCopy(ByVal blnVisible As Boolean)
        'コピー用区分
        Me.SelectKubunCopy.Visible = blnVisible
        '共通情報コピーボタン
        Me.ButtonKyoutuuInfoCopy.Visible = blnVisible
    End Sub

    ''' <summary>
    ''' 特別対応ボタンの色変え処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChgDispTokubetuTaiou()

        Dim strCtrlValuesTokubetu As String
        strCtrlValuesTokubetu = Me.IraiCtrl2_1.getCtrlValuesStringTokubetu()

        If strCtrlValuesTokubetu <> Me.IraiCtrl2_1.AccHiddenKakuteiValuesTokubetu.Value Then
            '赤背景
            Me.ButtonTokubetuTaiou.Style(EarthConst.STYLE_BACK_GROUND_COLOR) = EarthConst.STYLE_COLOR_RED
            '太字
            Me.ButtonTokubetuTaiou.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
        Else
            '背景色を初期化
            Me.ButtonTokubetuTaiou.Style(EarthConst.STYLE_BACK_GROUND_COLOR) = ""
            'ノーマル
            Me.ButtonTokubetuTaiou.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_NORMAL
        End If
    End Sub

    ''' <summary>
    ''' 画面再描画ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHiddenSyouhinReload_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenSyouhinReload.ServerClick

        '特別対応価格反映計算処理を実行
        IraiCtrl2_1.calcTeibetuTokubetuKkk(sender, e, False)

        '商品群パネルアップデート
        Me.IraiCtrl2_1.AccUpdPanelSyouhin.Update()

    End Sub

End Class