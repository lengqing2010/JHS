Partial Public Class SeikyuuSiireSakiHenkou
    Inherits System.Web.UI.Page
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
#Region "フィールド"
    Private cLogic As New CommonLogic
    Private mLogic As New MessageLogic
    Private userInfo As New LoginUserInfo
    Private Const PRM1 As String = "@PARAM1"
    Private Const KBN As String = "KBN"
    Private Const BANGOU As String = "BANGOU"
    Private Const REFRESH As String = "REFRESH"

#End Region

#Region "イベント"
#Region "ページ系"
    ''' <summary>
    ''' 画面初期処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SeikyuuSiireSakiHenkou_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        '画面の初期設定
        setDispAction()
    End Sub

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jbn As New Jiban

        ' ユーザー基本認証
        jbn.UserAuth(userInfo)

        '認証結果によって画面表示を切替える
        If userInfo Is Nothing Then
            Response.Redirect(UrlConst.MAIN)
        End If

        '権限チェック
        If userInfo.KeiriGyoumuKengen <> -1 Then
            Response.Redirect(UrlConst.MAIN)
        End If

        'クリア時の検索条件を復元
        If Context.Items(KBN) IsNot Nothing AndAlso Context.Items(KBN) <> String.Empty Then
            Me.selectKbn.SelectedValue = Context.Items(KBN)
        End If
        If Context.Items(BANGOU) IsNot Nothing AndAlso Context.Items(BANGOU) <> String.Empty Then
            Me.TextBangou.Value = Context.Items(BANGOU)
        End If

        '更新後の再描画
        If Context.Items(REFRESH) IsNot Nothing AndAlso Context.Items(REFRESH) <> String.Empty Then
            Context.Items(REFRESH) = Nothing
            ButtonEdit_Click(sender, e)
            Exit Sub
        End If

        If IsPostBack Then
            If Me.HiddenRemakeInfo.Value <> String.Empty Then
                'ユーザーコントロールの再作成
                remakeTeibetuRecCtrl(Me.HiddenRemakeInfo.Value)
            End If
        End If
    End Sub
#End Region

#Region "ボタン系"
    ''' <summary>
    ''' 編集ボタンクリック(物件を指定)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEdit.Click
        '入力チェック
        If Not checkInputSearch(sender) Then
            Exit Sub
        End If

        Dim strKbn As String = Me.selectKbn.SelectedValue
        Dim strBangou As String = Me.TextBangou.Value

        '地盤データをセット
        setJibanData(sender, strKbn, strBangou)

        '地盤データが存在した場合
        If Me.selectKbnLbl.SelectedValue <> String.Empty And Me.TextBangouLbl.Text <> String.Empty Then
            '登録・クリアボタンの非活性
            Me.ButtonSubmit.Enabled = True
            Me.ButtonClear.Enabled = True
            '検索条件の非活性
            Me.selectKbn.Enabled = False
            Me.TextBangou.Disabled = True

            '編集ボタンの非活性
            Me.ButtonEdit.Enabled = False
        End If

    End Sub

    ''' <summary>
    ''' 修正実行ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSubmit.Click
        Dim recJiban As New JibanRecordBase
        Dim listTeibetuRec As New List(Of TeibetuSeikyuuRecordSeikyuuSiireSakiHenkou)
        Dim strErrMsg As String = ""
        Dim listErrCtrl As New List(Of Control)
        Dim myLogic As New SeikyuuSiireSakiHenkouLogic
        Dim intResult As Integer
        Dim intErr As SeikyuuSiireSakiHenkouLogic.ErrType
        Dim strMsgStr() As String

        '画面からデータの取得＆セット
        getDispData(recJiban, listTeibetuRec, strErrMsg, listErrCtrl)

        '入力チェック
        If Not checkInputSubmit(sender, strErrMsg, listErrCtrl) Then
            Exit Sub
        End If

        '邸別請求データを更新
        strMsgStr = myLogic.saveData(sender, recJiban, listTeibetuRec, intResult, intErr)

        '更新エラーチェック
        Select Case intErr
            Case SeikyuuSiireSakiHenkouLogic.ErrType.SUCCESS
                Exit Select
            Case SeikyuuSiireSakiHenkouLogic.ErrType.HAITA
                '排他エラー
                'saveData内でエラーメッセージの出力が行われているので、
                'ここではスルー
                Exit Sub
            Case SeikyuuSiireSakiHenkouLogic.ErrType.KOUSIN
                '邸別請求レコード更新エラー
                'saveData内のEditTeibetuRecordでエラーメッセージの出力が行われているので、
                'ここではスルー
                Exit Sub
            Case SeikyuuSiireSakiHenkouLogic.ErrType.RENKEI
                '連携更新エラー
                mLogic.DbErrorMessage(sender _
                                    , "連携更新" _
                                    , "邸別請求連携" _
                                    , String.Format(EarthConst.TEIBETU_KEY, strMsgStr))
                Exit Sub
            Case SeikyuuSiireSakiHenkouLogic.ErrType.EXCEPTION
                '例外処理
                'saveData内でエラーメッセージの出力が行われているので、
                'ここではスルー
                Exit Sub
            Case Else
                'その他エラー
                mLogic.DbErrorMessage(sender _
                                    , "データベース更新" _
                                    , "－" _
                                    , String.Format(EarthConst.TEIBETU_KEY, New String() {"－", "－", "－", "－"}))
                Exit Sub
        End Select

        '画面再描画
        Context.Items(REFRESH) = REFRESH
        ButtonClear_Click(sender, e)

    End Sub

    ''' <summary>
    ''' クリアボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClear.Click
        Context.Items(KBN) = Me.selectKbn.SelectedValue
        Context.Items(BANGOU) = Me.TextBangou.Value
        Server.Transfer(UrlConst.SEIKYUU_SIIRE_SAKI_HENKOU)
    End Sub
#End Region
#End Region

#Region "プライベートメソッド"
#Region "初期処理"
    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()
        '****************************************************************************
        ' ドロップダウンリスト設定
        '****************************************************************************
        ' 区分コンボにデータをバインドする
        Dim helper As New DropDownHelper
        Dim kbn_logic As New KubunLogic
        helper.SetDropDownList(selectKbn, DropDownHelper.DropDownType.Kubun)
        helper.SetDropDownList(selectKbnLbl, DropDownHelper.DropDownType.Kubun)

        '使用不可項目のセット
        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        jSM.Hash2Ctrl(Me.tblBukkenInfo, EarthConst.MODE_VIEW, ht)
        jSM.Hash2Ctrl(Me.TBodyDefaultSeikyuuSaki, EarthConst.MODE_VIEW, ht)
        jSM.Hash2Ctrl(Me.TBodyDefaultSiireSaki, EarthConst.MODE_VIEW, ht)

        '入力(数値)チェック
        Me.TextBangou.Attributes("onblur") = "checkNumber(this);"

        '登録・クリアボタンの非活性
        Me.ButtonSubmit.Enabled = False
        Me.ButtonClear.Enabled = False
        '検索条件の活性
        Me.selectKbn.Enabled = True
        Me.TextBangou.Disabled = False
        '編集ボタンの活性
        Me.ButtonEdit.Enabled = True

    End Sub
#End Region

#Region "エラーチェック"
    ''' <summary>
    ''' 検索時入力チェック
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function checkInputSearch(ByVal sender As System.Object)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".checkInputSearch" _
                                                    , sender)

        Dim strTmpMsg As String = String.Empty

        If Me.selectKbn.SelectedValue = String.Empty Then
            strTmpMsg &= Messages.MSG013E.Replace(PRM1, "区分")
        End If

        If Me.TextBangou.Value = String.Empty Then
            strTmpMsg &= Messages.MSG013E.Replace(PRM1, "番号")
        End If

        If strTmpMsg.Length > 0 Then
            mLogic.AlertMessage(sender, strTmpMsg)
            Return False
            Exit Function
        End If

        Return True
    End Function

    ''' <summary>
    ''' 登録時入力チェック
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strErrMsg">エラーメッセージ</param>
    ''' <param name="listErrCtrl">エラーコントロール群</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function checkInputSubmit(ByVal sender As System.Object, ByVal strErrMsg As String, ByVal listErrCtrl As List(Of Control))
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".checkInputSubmit" _
                                                    , sender _
                                                    , strErrMsg _
                                                    , listErrCtrl)
        If listErrCtrl.Count > 0 Then
            mLogic.AlertMessage(sender, strErrMsg)
            Return False
            Exit Function
        End If

        Return True
    End Function
#End Region

#Region "DBからデータの取得＆セット"
    ''' <summary>
    ''' 地盤データを画面に設定する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBangou">番号</param>
    ''' <remarks></remarks>
    Private Sub setJibanData(ByVal sender As System.Object, ByVal strKbn As String, ByVal strBangou As String)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".setJibanData" _
                                                    , sender _
                                                    , strKbn _
                                                    , strBangou)
        Dim jbnLogic As New JibanLogic
        Dim tbsLogic As New TeibetuSyuuseiLogic
        Dim kmtLogic As New KameitenSearchLogic
        Dim tysLogic As New TyousakaisyaSearchLogic
        Dim msgLogic As New MessageLogic
        Dim recJiban As JibanRecord
        Dim recKmTen As KameitenDefaultSeikyuuSakiInfoRecord
        Dim recKaiKoj As TysKaisyaRecord = Nothing
        Dim recTuiKoj As TysKaisyaRecord = Nothing
        Dim recSirTys As TysKaisyaRecord = Nothing

        '地盤データを取得する
        recJiban = jbnLogic.GetJibanData(strKbn, strBangou)

        '地盤データがない場合
        If recJiban.Kbn Is Nothing Or recJiban.HosyousyoNo Is Nothing Then
            Dim strJs As String = ""
            strJs &= "alert('" & Messages.MSG020E & "');" & vbCrLf
            ScriptManager.RegisterStartupScript(sender, sender.GetType(), "NoDataErr", strJs, True)
            Exit Sub
        End If

        '区分
        Me.selectKbnLbl.SelectedValue = cLogic.GetDisplayString(recJiban.Kbn)
        Me.spanKbn.InnerHtml = cLogic.GetDisplayString(recJiban.Kbn)
        '番号
        Me.TextBangouLbl.Text = cLogic.GetDisplayString(recJiban.HosyousyoNo)
        '施主名
        Me.TextSesyuMeiLbl.Text = cLogic.GetDisplayString(recJiban.SesyuMei)
        '加盟店コード
        Me.TextKameitenCdLbl.Text = cLogic.GetDisplayString(recJiban.KameitenCd)
        '加盟店取消理由を表示
        Dim objChgColor As Object() = New Object() {Me.TextKameitenCdLbl, Me.TextKameitenMeiLbl, Me.TextTorikesiRiyuuKihon}
        cLogic.GetKameitenTorikesiRiyuu(Me.selectKbnLbl.SelectedValue, Me.TextKameitenCdLbl.Text, Me.TextTorikesiRiyuuKihon, True, False, objChgColor)

        '加盟店データを取得する
        recKmTen = kmtLogic.GetKameitenDefaultSeikyuuSakiInfo(recJiban.KameitenCd)
        '工事会社データを取得する
        If recJiban.KairyouKoujiRecord IsNot Nothing Then
            '改良工事
            recKaiKoj = tysLogic.getTysKaisyaInfo(recJiban.KojGaisyaCd & _
                                                    recJiban.KojGaisyaJigyousyoCd)
        End If
        If recJiban.TuikaKoujiRecord IsNot Nothing Then
            '追加工事
            recTuiKoj = tysLogic.getTysKaisyaInfo(recJiban.TKojKaisyaCd & _
                                                    recJiban.TKojKaisyaJigyousyoCd)
        End If

        '地盤の更新日時(排他制御用) 
        If recJiban.UpdDatetime = DateTime.MinValue Then
            Me.HiddenJibanUpdDateTime.Value = String.Empty
        Else
            Me.HiddenJibanUpdDateTime.Value = recJiban.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_1)
        End If

        '*******************************
        '* 基本請求先のセット
        '*******************************
        If recKmTen IsNot Nothing Then
            '加盟店名
            Me.TextKameitenMeiLbl.Text = cLogic.GetDisplayString(recKmTen.KameitenMei1)

            '************
            '* 調査 
            '************
            '基本調査請求先コード
            Me.TextDefaultTysSeikyuuCdLbl.Text = cLogic.GetDispSeikyuuSakiCd( _
                                                                              recKmTen.TysSeikyuuSakiKbn _
                                                                            , recKmTen.TysSeikyuuSakiCd _
                                                                            , recKmTen.TysSeikyuuSakiBrc _
                                                                            , True)
            '基本調査請求先名
            Me.TextDefaultTysSeikyuuMeiLbl.Text = cLogic.GetDisplayString(recKmTen.TysSeikyuuSakiMei)
            '基本調査請求先変更有無
            If cLogic.GetDisplayString(recKmTen.TysHenkouUmu) <> String.Empty Then
                Me.TextDefaultTysSeikyuuHenkouLbl.Text = EarthConst.TOUROKU_ARI
                cLogic.setStyleRedBold(Me.TextDefaultTysSeikyuuHenkouLbl.Style, True)
            Else
                Me.TextDefaultTysSeikyuuHenkouLbl.Text = EarthConst.TOUROKU_NASI
            End If

            '************
            '* 改良工事
            '************
            If cLogic.GetDisplayString(recJiban.KojGaisyaSeikyuuUmu) <> 1 OrElse recKaiKoj Is Nothing Then
                '基本改良工事請求先コード
                Me.TextDefaultKaiKojSeikyuuCdLbl.Text = cLogic.GetDispSeikyuuSakiCd( _
                                                                                      recKmTen.KojSeikyuuSakiKbn _
                                                                                    , recKmTen.KojSeikyuuSakiCd _
                                                                                    , recKmTen.KojSeikyuuSakiBrc _
                                                                                    , True)
                '基本改良工事請求先名
                Me.TextDefaultKaiKojSeikyuuMeiLbl.Text = cLogic.GetDisplayString(recKmTen.KojSeikyuuSakiMei)
                '基本改良工事先変更有無
                If cLogic.GetDisplayString(recKmTen.KojHenkouUmu) <> String.Empty Then
                    Me.TextDefaultKaiKojSeikyuuHenkouLbl.Text = EarthConst.TOUROKU_ARI
                    cLogic.setStyleRedBold(Me.TextDefaultKaiKojSeikyuuHenkouLbl.Style, True)
                Else
                    Me.TextDefaultKaiKojSeikyuuHenkouLbl.Text = EarthConst.TOUROKU_NASI
                End If
            Else
                '基本改良工事請求先コード
                Me.TextDefaultKaiKojSeikyuuCdLbl.Text = cLogic.GetDispSeikyuuSakiCd( _
                                                                                      recKaiKoj.SeikyuuSakiKbn _
                                                                                    , recKaiKoj.SeikyuuSakiCd _
                                                                                    , recKaiKoj.SeikyuuSakiBrc _
                                                                                    , True)
                '基本改良工事請求先名
                Me.TextDefaultKaiKojSeikyuuMeiLbl.Text = cLogic.GetDisplayString(recKaiKoj.SeikyuuSakiMei)
                '基本改良工事先変更有無
                Me.TextDefaultKaiKojSeikyuuHenkouLbl.Text = EarthConst.TAISYOU_GAI
            End If

            '************
            '* 追加工事
            '************
            If cLogic.GetDisplayString(recJiban.TKojKaisyaSeikyuuUmu) <> 1 OrElse recTuiKoj Is Nothing Then
                '基本追加工事請求先コード
                Me.TextDefaultTuiKojSeikyuuCdLbl.Text = cLogic.GetDispSeikyuuSakiCd( _
                                                                                      recKmTen.KojSeikyuuSakiKbn _
                                                                                    , recKmTen.KojSeikyuuSakiCd _
                                                                                    , recKmTen.KojSeikyuuSakiBrc _
                                                                                    , True)
                '基本追加工事請求先名
                Me.TextDefaultTuiKojSeikyuuMeiLbl.Text = cLogic.GetDisplayString(recKmTen.KojSeikyuuSakiMei)
                '基本追加工事先変更有無
                If cLogic.GetDisplayString(recKmTen.KojHenkouUmu) <> String.Empty Then
                    Me.TextDefaultTuiKojSeikyuuHenkouLbl.Text = EarthConst.TOUROKU_ARI
                    cLogic.setStyleRedBold(Me.TextDefaultTuiKojSeikyuuHenkouLbl.Style, True)
                Else
                    Me.TextDefaultTuiKojSeikyuuHenkouLbl.Text = EarthConst.TOUROKU_NASI
                End If
            Else
                '基本追加工事請求先コード
                Me.TextDefaultTuiKojSeikyuuCdLbl.Text = cLogic.GetDispSeikyuuSakiCd( _
                                                                                      recTuiKoj.SeikyuuSakiKbn _
                                                                                    , recTuiKoj.SeikyuuSakiCd _
                                                                                    , recTuiKoj.SeikyuuSakiBrc _
                                                                                    , True)
                '基本追加工事請求先名
                Me.TextDefaultTuiKojSeikyuuMeiLbl.Text = cLogic.GetDisplayString(recTuiKoj.SeikyuuSakiMei)
                '基本追加工事先変更有無
                Me.TextDefaultTuiKojSeikyuuHenkouLbl.Text = EarthConst.TAISYOU_GAI
            End If

            '************
            '* 販促品
            '************
            '基本販促品請求先コード
            Me.TextDefaultHnskSeikyuuCdLbl.Text = cLogic.GetDispSeikyuuSakiCd( _
                                                                                  recKmTen.HnskSeikyuuSakiKbn _
                                                                                , recKmTen.HnskSeikyuuSakiCd _
                                                                                , recKmTen.HnskSeikyuuSakiBrc _
                                                                                , True)
            '基本販促品請求先名
            Me.TextDefaultHnskSeikyuuMeiLbl.Text = cLogic.GetDisplayString(recKmTen.HnskSeikyuuSakiMei)
            '基本販促品請求先変更有無
            If cLogic.GetDisplayString(recKmTen.HnskHenkouUmu) <> String.Empty Then
                Me.TextDefaultHnskSeikyuuHenkouLbl.Text = EarthConst.TOUROKU_ARI
                cLogic.setStyleRedBold(Me.TextDefaultHnskSeikyuuHenkouLbl.Style, True)
            Else
                Me.TextDefaultHnskSeikyuuHenkouLbl.Text = EarthConst.TOUROKU_NASI
            End If

            '************
            '* 設計確認
            '************
            '基本設計確認請求先コード
            Me.TextDefaultKasiSeikyuuCdLbl.Text = cLogic.GetDispSeikyuuSakiCd( _
                                                                                  recKmTen.KasiSeikyuuSakiKbn _
                                                                                , recKmTen.KasiSeikyuuSakiCd _
                                                                                , recKmTen.KasiSeikyuuSakiBrc _
                                                                                , True)
            '基本設計確認請求先名
            Me.TextDefaultKasiSeikyuuMeiLbl.Text = cLogic.GetDisplayString(recKmTen.KasiSeikyuuSakiMei)
            '基本設計確認請求先変更有無
            If cLogic.GetDisplayString(recKmTen.KasiHenkouUmu) <> String.Empty Then
                Me.TextDefaultKasiSeikyuuHenkouLbl.Text = EarthConst.TOUROKU_ARI
                cLogic.setStyleRedBold(Me.TextDefaultKasiSeikyuuHenkouLbl.Style, True)
            Else
                Me.TextDefaultKasiSeikyuuHenkouLbl.Text = EarthConst.TOUROKU_NASI
            End If
        End If
        '*******************************
        '* 基本仕入先のセット
        '*******************************
        Dim strJbnTysKaisyaCd As String = String.Empty
        Dim strJbnTysKaisyaMei As String = String.Empty
        Dim strKaiKojKaisyaCd As String = String.Empty
        Dim strKaiKojKaisyaMei As String = String.Empty
        Dim strTuiKojKaisyaCd As String = String.Empty
        Dim strTuiKojKaisyaMei As String = String.Empty

        '基本調査仕入先コード
        strJbnTysKaisyaCd = getTysKaisyaCdDisplay(cLogic.GetDisplayString(recJiban.TysKaisyaCd) _
                                                , cLogic.GetDisplayString(recJiban.TysKaisyaJigyousyoCd))
        '調査会社情報の取得
        recSirTys = tysLogic.getTysKaisyaInfo(recJiban.TysKaisyaCd & _
                                                    recJiban.TysKaisyaJigyousyoCd)
        '基本調査仕入先名
        strJbnTysKaisyaMei = recSirTys.TysKaisyaMei

        '改良工事会社コード
        strKaiKojKaisyaCd = getTysKaisyaCdDisplay(cLogic.GetDisplayString(recJiban.KojGaisyaCd) _
                                                , cLogic.GetDisplayString(recJiban.KojGaisyaJigyousyoCd))
        If recKaiKoj IsNot Nothing Then
            '改良工事会社名
            strKaiKojKaisyaMei = cLogic.GetDisplayString(recKaiKoj.TysKaisyaMei)
        End If

        '追加工事会社コード
        strTuiKojKaisyaCd = getTysKaisyaCdDisplay(cLogic.GetDisplayString(recJiban.TKojKaisyaCd) _
                                                , cLogic.GetDisplayString(recJiban.TKojKaisyaJigyousyoCd))
        If recTuiKoj IsNot Nothing Then
            '追加工事会社名
            strTuiKojKaisyaMei = cLogic.GetDisplayString(recTuiKoj.TysKaisyaMei)
        End If

        '調査
        Me.TextDefaultTysSiireCdLbl.Text = strJbnTysKaisyaCd
        Me.TextDefaultTysSiireMeiLbl.Text = strJbnTysKaisyaMei
        '改良工事
        Me.TextDefaultKaiKojSiireCdLbl.Text = strKaiKojKaisyaCd
        Me.TextDefaultKaiKojSiireMeiLbl.Text = strKaiKojKaisyaMei
        '追加工事
        Me.TextDefaultTuiKojSiireCdLbl.Text = strTuiKojKaisyaCd
        Me.TextDefaultTuiKojSiireMeiLbl.Text = strTuiKojKaisyaMei
        '設計確認
        Me.TextDefaultKasiSiireCdLbl.Text = strJbnTysKaisyaCd
        Me.TextDefaultKasiSiireMeiLbl.Text = strJbnTysKaisyaMei

        '邸別請求データのセット
        setTeibetuData(recJiban)

    End Sub

    ''' <summary>
    ''' 邸別請求データを画面に設定する
    ''' </summary>
    ''' <param name="recJiban">地盤レコード</param>
    ''' <remarks></remarks>
    Private Sub setTeibetuData(ByVal recJiban As JibanRecord)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".setTeibetuData" _
                                                    , recJiban)
        Dim recTeibetu As TeibetuSeikyuuRecord
        Dim dicRecTeibetu As Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Dim intRowCnt As Integer
        Dim strRemakeInfo As String = ""

        '商品1をセット
        recTeibetu = recJiban.Syouhin1Record
        If recTeibetu IsNot Nothing Then
            addTeibetuRecCtrl(EarthConst.USR_CTRL_ID_ITEM1, recJiban, recTeibetu, Me.tdSyouhin1, 1)
            strRemakeInfo &= "1"
        End If

        strRemakeInfo &= EarthConst.SEP_STRING

        '商品2をセット
        dicRecTeibetu = recJiban.Syouhin2Records
        If dicRecTeibetu IsNot Nothing Then
            For Each dic As KeyValuePair(Of Integer, TeibetuSeikyuuRecord) In dicRecTeibetu
                intRowCnt += 1
                recTeibetu = dic.Value
                addTeibetuRecCtrl(EarthConst.USR_CTRL_ID_ITEM2 & intRowCnt, recJiban, recTeibetu, Me.tdSyouhin2, intRowCnt)
            Next
            strRemakeInfo &= intRowCnt.ToString
        End If

        strRemakeInfo &= EarthConst.SEP_STRING

        '行数を初期化
        intRowCnt = 0
        '商品3をセット
        dicRecTeibetu = recJiban.Syouhin3Records
        If dicRecTeibetu IsNot Nothing Then
            For Each dic As KeyValuePair(Of Integer, TeibetuSeikyuuRecord) In dicRecTeibetu
                intRowCnt += 1
                recTeibetu = dic.Value
                addTeibetuRecCtrl(EarthConst.USR_CTRL_ID_ITEM3 & intRowCnt, recJiban, recTeibetu, Me.tdSyouhin3, intRowCnt)
            Next
            strRemakeInfo &= intRowCnt.ToString
        End If

        strRemakeInfo &= EarthConst.SEP_STRING

        '行数を初期化
        intRowCnt = 0
        '商品4をセット
        dicRecTeibetu = recJiban.Syouhin4Records
        If dicRecTeibetu IsNot Nothing Then
            For Each dic As KeyValuePair(Of Integer, TeibetuSeikyuuRecord) In dicRecTeibetu
                intRowCnt += 1
                recTeibetu = dic.Value
                addTeibetuRecCtrl(EarthConst.USR_CTRL_ID_ITEM4 & intRowCnt, recJiban, recTeibetu, Me.tdSyouhin4, intRowCnt)
            Next
            strRemakeInfo &= intRowCnt.ToString
        End If

        strRemakeInfo &= EarthConst.SEP_STRING

        '改良工事をセット
        recTeibetu = recJiban.KairyouKoujiRecord
        If recTeibetu IsNot Nothing Then
            addTeibetuRecCtrl(EarthConst.USR_CTRL_ID_K_KOUJI, recJiban, recTeibetu, Me.tdKaiKoj, 1)
            strRemakeInfo &= "1"
        End If

        strRemakeInfo &= EarthConst.SEP_STRING

        '追加工事をセット
        recTeibetu = recJiban.TuikaKoujiRecord
        If recTeibetu IsNot Nothing Then
            addTeibetuRecCtrl(EarthConst.USR_CTRL_ID_T_KOUJI, recJiban, recTeibetu, Me.TdTuiKoj, 1)
            strRemakeInfo &= "1"
        End If

        strRemakeInfo &= EarthConst.SEP_STRING

        '調査報告書をセット
        recTeibetu = recJiban.TyousaHoukokusyoRecord
        If recTeibetu IsNot Nothing Then
            addTeibetuRecCtrl(EarthConst.USR_CTRL_ID_T_HOUKOKU, recJiban, recTeibetu, Me.TdTysHokoku, 1)
            strRemakeInfo &= "1"
        End If

        strRemakeInfo &= EarthConst.SEP_STRING

        '工事報告書をセット
        recTeibetu = recJiban.KoujiHoukokusyoRecord
        If recTeibetu IsNot Nothing Then
            addTeibetuRecCtrl(EarthConst.USR_CTRL_ID_K_HOUKOKU, recJiban, recTeibetu, Me.TdKojHokoku, 1)
            strRemakeInfo &= "1"
        End If

        strRemakeInfo &= EarthConst.SEP_STRING

        '保証書をセット
        recTeibetu = recJiban.HosyousyoRecord
        If recTeibetu IsNot Nothing Then
            addTeibetuRecCtrl(EarthConst.USR_CTRL_ID_HOSYOUSYO, recJiban, recTeibetu, Me.TdHosyosyo, 1)
            strRemakeInfo &= "1"
        End If

        strRemakeInfo &= EarthConst.SEP_STRING

        '解約払戻をセット
        recTeibetu = recJiban.KaiyakuHaraimodosiRecord
        If recTeibetu IsNot Nothing Then
            addTeibetuRecCtrl(EarthConst.USR_CTRL_ID_KAIYAKU, recJiban, recTeibetu, Me.TdKaiyaku, 1)
            strRemakeInfo &= "1"
        End If

        Me.HiddenRemakeInfo.Value = strRemakeInfo

    End Sub
#End Region

#Region "画面からデータの取得＆セット"
    ''' <summary>
    ''' 画面からデータの取得＆セット
    ''' </summary>
    ''' <param name="recJiban">地盤レコード</param>
    ''' <param name="listTeibetuRec">邸別請求レコードリスト</param>
    ''' <param name="strErrMsg">エラーメッセージ</param>
    ''' <param name="listErrCtrl">エラーコントロール群</param>
    ''' <remarks></remarks>
    Private Sub getDispData(ByRef recJiban As JibanRecordBase _
                            , ByRef listTeibetuRec As List(Of TeibetuSeikyuuRecordSeikyuuSiireSakiHenkou) _
                            , ByRef strErrMsg As String _
                            , ByRef listErrCtrl As List(Of Control))
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getDispData" _
                                                    , listTeibetuRec _
                                                    , strErrMsg _
                                                    , listErrCtrl)

        Dim strArrInfo() As String = Split(Me.HiddenRemakeInfo.Value, EarthConst.SEP_STRING)

        '地盤データのセット(排他制御用)
        With recJiban
            '区分
            .Kbn = Me.selectKbnLbl.SelectedValue
            '保証書NO
            .HosyousyoNo = Me.TextBangouLbl.Text
            '更新ユーザー
            .UpdLoginUserId = userInfo.LoginUserId

            '更新日時(排他制御用)
            .UpdDatetime = cLogic.getDispHaitaUpdTime(Me.HiddenJibanUpdDateTime)

        End With

        '商品1
        getSingleDispData(strArrInfo(0), Me.tdSyouhin1, strErrMsg, listErrCtrl, EarthConst.ITEM_BUNRUI_NAME_ITEM1, listTeibetuRec)
        '商品2
        getMultiDispData(strArrInfo(1), Me.tdSyouhin2, strErrMsg, listErrCtrl, EarthConst.ITEM_BUNRUI_NAME_ITEM2, listTeibetuRec)
        '商品3
        getMultiDispData(strArrInfo(2), Me.tdSyouhin3, strErrMsg, listErrCtrl, EarthConst.ITEM_BUNRUI_NAME_ITEM3, listTeibetuRec)
        '商品4
        getMultiDispData(strArrInfo(3), Me.tdSyouhin4, strErrMsg, listErrCtrl, EarthConst.ITEM_BUNRUI_NAME_ITEM4, listTeibetuRec)
        '改良工事
        getSingleDispData(strArrInfo(4), Me.tdKaiKoj, strErrMsg, listErrCtrl, EarthConst.ITEM_BUNRUI_NAME_K_KOUJI, listTeibetuRec)
        '追加工事
        getSingleDispData(strArrInfo(5), Me.TdTuiKoj, strErrMsg, listErrCtrl, EarthConst.ITEM_BUNRUI_NAME_T_KOUJI, listTeibetuRec)
        '調査報告書
        getSingleDispData(strArrInfo(6), Me.TdTysHokoku, strErrMsg, listErrCtrl, EarthConst.ITEM_BUNRUI_NAME_T_HOUKOKU, listTeibetuRec)
        '工事報告書
        getSingleDispData(strArrInfo(7), Me.TdKojHokoku, strErrMsg, listErrCtrl, EarthConst.ITEM_BUNRUI_NAME_K_HOUKOKU, listTeibetuRec)
        '保証書
        getSingleDispData(strArrInfo(8), Me.TdHosyosyo, strErrMsg, listErrCtrl, EarthConst.ITEM_BUNRUI_NAME_HOSYOUSYO, listTeibetuRec)
        '解約払戻
        getSingleDispData(strArrInfo(9), Me.TdKaiyaku, strErrMsg, listErrCtrl, EarthConst.ITEM_BUNRUI_NAME_KAIYAKU, listTeibetuRec)

    End Sub

    ''' <summary>
    ''' 単一商品のユーザーコントロールからデータを取得
    ''' </summary>
    ''' <param name="strRowCnt">行数(単一商品なので1固定）</param>
    ''' <param name="objTd">TDオブジェクト</param>
    ''' <param name="strErrMsg">エラーメッセージ</param>
    ''' <param name="listErrCtrl">エラーコントロール群</param>
    ''' <param name="strRowIdentify">行識別文字列</param>
    ''' <param name="listTeibetuRec">邸別請求レコードリスト</param>
    ''' <remarks></remarks>
    Private Sub getSingleDispData(ByVal strRowCnt As String _
                                , ByVal objTd As HtmlTableCell _
                                , ByRef strErrMsg As String _
                                , ByRef listErrCtrl As List(Of Control) _
                                , ByVal strRowIdentify As String _
                                , ByRef listTeibetuRec As List(Of TeibetuSeikyuuRecordSeikyuuSiireSakiHenkou))
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSingleDispData" _
                                                    , strRowCnt _
                                                    , objTd _
                                                    , strErrMsg _
                                                    , listErrCtrl _
                                                    , strRowIdentify _
                                                    , listTeibetuRec)
        Dim ctrlRec As SeikyuuSiireSyouhinRecordCtrl
        If strRowCnt <> String.Empty Then
            ctrlRec = objTd.Controls(3)
            ctrlRec.checkInput(strErrMsg, listErrCtrl, strRowIdentify)
            listTeibetuRec.Add(getUpdTeibetuRec(ctrlRec))
        End If
    End Sub

    ''' <summary>
    ''' 複数行商品のユーザーコントロールからデータを取得
    ''' </summary>
    ''' <param name="strRowCnt">行数</param>
    ''' <param name="objTd">TDオブジェクト</param>
    ''' <param name="strErrMsg">エラーメッセージ</param>
    ''' <param name="listErrCtrl">エラーコントロール群</param>
    ''' <param name="strRowIdentify">行識別文字列</param>
    ''' <param name="listTeibetuRec">邸別請求レコードリスト</param>
    ''' <remarks></remarks>
    Private Sub getMultiDispData(ByVal strRowCnt As String _
                                , ByVal objTd As HtmlTableCell _
                                , ByRef strErrMsg As String _
                                , ByRef listErrCtrl As List(Of Control) _
                                , ByVal strRowIdentify As String _
                                , ByRef listTeibetuRec As List(Of TeibetuSeikyuuRecordSeikyuuSiireSakiHenkou))
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getMultiDispData" _
                                                    , strRowCnt _
                                                    , objTd _
                                                    , strErrMsg _
                                                    , listErrCtrl _
                                                    , strRowIdentify _
                                                    , listTeibetuRec)
        Dim ctrlRec As SeikyuuSiireSyouhinRecordCtrl
        If strRowCnt <> String.Empty Then
            For intCnt As Integer = 1 To Integer.Parse(strRowCnt)
                ctrlRec = objTd.Controls(intCnt + 2)
                ctrlRec.checkInput(strErrMsg, listErrCtrl, strRowIdentify & EarthConst.UNDER_SCORE & StrConv(intCnt.ToString, VbStrConv.Wide))
                listTeibetuRec.Add(getUpdTeibetuRec(ctrlRec))
            Next
        End If
    End Sub

    ''' <summary>
    ''' ユーザーコントロールのデータを邸別請求レコードクラスへセット
    ''' </summary>
    ''' <param name="ctrlRec">ユーザーコントロール</param>
    ''' <returns>ユーザーコントロールの値をセットした邸別請求レコード</returns>
    ''' <remarks></remarks>
    Private Function getUpdTeibetuRec(ByVal ctrlRec As SeikyuuSiireSyouhinRecordCtrl) As TeibetuSeikyuuRecordSeikyuuSiireSakiHenkou
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getUpdTeibetuRec" _
                                                    , ctrlRec)

        Dim recData As New TeibetuSeikyuuRecordSeikyuuSiireSakiHenkou

        With recData
            'KEY情報
            .Kbn = Me.selectKbnLbl.SelectedValue
            .HosyousyoNo = Me.TextBangouLbl.Text
            .BunruiCd = ctrlRec.AccBunruiCd
            .GamenHyoujiNo = ctrlRec.AccGamenHyoujiNo
            '請求先
            .SeikyuuSakiCd = ctrlRec.AccSeikyuuSakiCd
            .SeikyuuSakiBrc = ctrlRec.AccSeikyuuSakiBrc
            .SeikyuuSakiKbn = ctrlRec.AccSeikyuuSakiKbn
            '仕入先
            .TysKaisyaCd = ctrlRec.AccSiireSakiCd
            .TysKaisyaJigyousyoCd = ctrlRec.AccSiireSakiBrc
            '更新ユーザー
            .UpdLoginUserId = userInfo.LoginUserId
            '更新日時(排他制御用)
            .UpdDatetime = cLogic.getDispHaitaUpdTime(ctrlRec.AccHdnUpdDateTime)

        End With

        Return recData

    End Function

#End Region

#Region "ユーザーコントロールの動的ロード"
    ''' <summary>
    ''' 邸別請求データユーザーコントロールをテーブルに追加
    ''' </summary>
    ''' <param name="strCtrlRecId">ユーザーコントロールID</param>
    ''' <param name="recJiban">地盤レコード</param>
    ''' <param name="recTeibetu">邸別請求レコード</param>
    ''' <param name="objTd">ユーザーコントロールを追加するTDオブジェクト</param>
    ''' <param name="intRowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub addTeibetuRecCtrl(ByVal strCtrlRecId As String _
                                , ByVal recJiban As JibanRecord _
                                , ByVal recTeibetu As TeibetuSeikyuuRecord _
                                , ByVal objTd As HtmlTableCell _
                                , ByVal intRowNo As Integer)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".addTeibetuRecCtrl" _
                                                    , strCtrlRecId _
                                                    , recJiban _
                                                    , recTeibetu _
                                                    , objTd _
                                                    , intRowNo)

        Dim ctrlRec As SeikyuuSiireSyouhinRecordCtrl

        'ユーザコントロールの読込
        ctrlRec = Me.LoadControl("control/SeikyuuSiireSyouhinRecordCtrl.ascx")
        With ctrlRec
            'IDの付与
            .ID = strCtrlRecId

            'テーブルにユーザーコントロールを追加
            objTd.Controls.Add(ctrlRec)

            '***********************************************
            '* ユーザーコントロールの部品に値をセット
            '***********************************************
            '行色の設定(テーブルに追加前でないと設定できない)
            If intRowNo Mod 2 = 0 Then
                .RowColorStyle = "even"
            Else
                .RowColorStyle = "odd"
            End If

            '加盟店情報
            .AccKameitenCd = recJiban.KameitenCd
            '商品情報
            .AccSyouhinCd = recTeibetu.SyouhinCd
            .AccSyouhinMei = recTeibetu.SyouhinMei
            .AccBunruiCd = recTeibetu.BunruiCd
            .AccGamenHyoujiNo = recTeibetu.GamenHyoujiNo
            Select Case strCtrlRecId
                Case EarthConst.USR_CTRL_ID_K_KOUJI
                    .AccKojKaisyaSeikyuuFlg = recJiban.KojGaisyaSeikyuuUmu
                    .AccKojKaisyaCd = recJiban.KojGaisyaCd & recJiban.KojGaisyaJigyousyoCd
                    '基本仕入先情報
                    .AccDefaultSiireSakiCd = recJiban.KojGaisyaCd
                    .AccDefaultSiireSakiBrc = recJiban.KojGaisyaJigyousyoCd
                Case EarthConst.USR_CTRL_ID_T_KOUJI
                    .AccKojKaisyaSeikyuuFlg = recJiban.TKojKaisyaSeikyuuUmu
                    .AccKojKaisyaCd = recJiban.TKojKaisyaCd & recJiban.TKojKaisyaJigyousyoCd
                    '基本仕入先情報
                    .AccDefaultSiireSakiCd = recJiban.TKojKaisyaCd
                    .AccDefaultSiireSakiBrc = recJiban.TKojKaisyaJigyousyoCd
                Case Else
                    '基本仕入先情報
                    .AccDefaultSiireSakiCd = recJiban.TysKaisyaCd
                    .AccDefaultSiireSakiBrc = recJiban.TysKaisyaJigyousyoCd
            End Select

            '請求先情報
            .AccSeikyuuSakiCd = recTeibetu.SeikyuuSakiCd
            .AccSeikyuuSakiBrc = recTeibetu.SeikyuuSakiBrc
            .AccSeikyuuSakiKbn = recTeibetu.SeikyuuSakiKbn

            '仕入先情報
            .AccSiireSakiCd = recTeibetu.TysKaisyaCd
            .AccSiireSakiBrc = recTeibetu.TysKaisyaJigyousyoCd

            '更新日時(排他制御用)
            cLogic.setDispHaitaUpdTime(recTeibetu.AddDatetime, recTeibetu.UpdDatetime, .AccHdnUpdDateTime)

            .setDispData()
        End With

    End Sub

    ''' <summary>
    ''' 邸別請求データユーザーコントロールをテーブルに再作成
    ''' </summary>
    ''' <param name="strRemakeInfo"></param>
    ''' <remarks></remarks>
    Private Sub remakeTeibetuRecCtrl(ByVal strRemakeInfo As String)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".remakeTeibetuRecCtrl" _
                                                    , strRemakeInfo)
        Dim strArrInfo() As String = Split(strRemakeInfo, EarthConst.SEP_STRING)
        Dim ctrlRec As SeikyuuSiireSyouhinRecordCtrl
        Dim intCnt As Integer

        '商品1
        If strArrInfo(0) <> String.Empty Then
            ctrlRec = Me.LoadControl("control/SeikyuuSiireSyouhinRecordCtrl.ascx")
            ctrlRec.ID = EarthConst.USR_CTRL_ID_ITEM1
            Me.tdSyouhin1.Controls.Add(ctrlRec)
        End If
        '商品2
        If strArrInfo(1) <> String.Empty Then
            For intCnt = 1 To Integer.Parse(strArrInfo(1))
                ctrlRec = Me.LoadControl("control/SeikyuuSiireSyouhinRecordCtrl.ascx")
                ctrlRec.ID = EarthConst.USR_CTRL_ID_ITEM2 & intCnt
                Me.tdSyouhin2.Controls.Add(ctrlRec)
            Next
        End If
        '商品3
        If strArrInfo(2) <> String.Empty Then
            For intCnt = 1 To Integer.Parse(strArrInfo(2))
                ctrlRec = Me.LoadControl("control/SeikyuuSiireSyouhinRecordCtrl.ascx")
                ctrlRec.ID = EarthConst.USR_CTRL_ID_ITEM3 & intCnt
                Me.tdSyouhin3.Controls.Add(ctrlRec)
            Next
        End If
        '商品4
        If strArrInfo(3) <> String.Empty Then
            For intCnt = 1 To Integer.Parse(strArrInfo(3))
                ctrlRec = Me.LoadControl("control/SeikyuuSiireSyouhinRecordCtrl.ascx")
                ctrlRec.ID = EarthConst.USR_CTRL_ID_ITEM4 & intCnt
                Me.tdSyouhin4.Controls.Add(ctrlRec)
            Next
        End If
        '改良工事
        If strArrInfo(4) <> String.Empty Then
            ctrlRec = Me.LoadControl("control/SeikyuuSiireSyouhinRecordCtrl.ascx")
            ctrlRec.ID = EarthConst.USR_CTRL_ID_K_KOUJI
            Me.tdKaiKoj.Controls.Add(ctrlRec)
        End If
        '追加工事
        If strArrInfo(5) <> String.Empty Then
            ctrlRec = Me.LoadControl("control/SeikyuuSiireSyouhinRecordCtrl.ascx")
            ctrlRec.ID = EarthConst.USR_CTRL_ID_T_KOUJI
            Me.TdTuiKoj.Controls.Add(ctrlRec)
        End If
        '調査報告書
        If strArrInfo(6) <> String.Empty Then
            ctrlRec = Me.LoadControl("control/SeikyuuSiireSyouhinRecordCtrl.ascx")
            ctrlRec.ID = EarthConst.USR_CTRL_ID_T_HOUKOKU
            Me.TdTysHokoku.Controls.Add(ctrlRec)
        End If
        '工事報告書
        If strArrInfo(7) <> String.Empty Then
            ctrlRec = Me.LoadControl("control/SeikyuuSiireSyouhinRecordCtrl.ascx")
            ctrlRec.ID = EarthConst.USR_CTRL_ID_K_HOUKOKU
            Me.TdKojHokoku.Controls.Add(ctrlRec)
        End If
        '保証書
        If strArrInfo(8) <> String.Empty Then
            ctrlRec = Me.LoadControl("control/SeikyuuSiireSyouhinRecordCtrl.ascx")
            ctrlRec.ID = EarthConst.USR_CTRL_ID_HOSYOUSYO
            Me.TdHosyosyo.Controls.Add(ctrlRec)
        End If
        '解約払戻
        If strArrInfo(9) <> String.Empty Then
            ctrlRec = Me.LoadControl("control/SeikyuuSiireSyouhinRecordCtrl.ascx")
            ctrlRec.ID = EarthConst.USR_CTRL_ID_KAIYAKU
            Me.TdKaiyaku.Controls.Add(ctrlRec)
        End If

    End Sub
#End Region

#Region "ユーティリティ"
    ''' <summary>
    ''' 画面に表示する調査会社コードを取得する
    ''' </summary>
    ''' <param name="strKaisyaCd">調査会社コード</param>
    ''' <param name="strJigyousyoCd">調査会社事業所コード</param>
    ''' <returns>画面に表示する調査会社コード</returns>
    ''' <remarks></remarks>
    Private Function getTysKaisyaCdDisplay(ByVal strKaisyaCd As String, ByVal strJigyousyoCd As String) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getTysKaisyaCdDisplay" _
                                                    , strKaisyaCd _
                                                    , strJigyousyoCd)

        If strKaisyaCd <> String.Empty And strJigyousyoCd <> String.Empty Then
            Return strKaisyaCd & EarthConst.HYPHEN_FULL_CHAR & strJigyousyoCd
            Exit Function
        Else
            Return String.Empty
        End If
    End Function
#End Region

#End Region

End Class