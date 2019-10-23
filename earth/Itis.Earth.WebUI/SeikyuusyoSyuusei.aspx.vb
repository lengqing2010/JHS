Public Partial Class SeikyuusyoSyuusei
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

    '共通ロジック
    Private cl As New CommonLogic
    'メッセージクラス
    Private MLogic As New MessageLogic
    'ロジッククラス
    Private MyLogic As New SeikyuuDataSearchLogic
    '地盤セッション管理クラス
    Private jSM As New JibanSessionManager
    '請求データレコードクラス
    Private dtRec As New SeikyuuDataRecord

#Region "画面固有コントロール値"

    ''' <summary>
    ''' 取消ボタン「請求書取消」
    ''' </summary>
    Private Const BTN_TORIKESI As String = "請求書取消"

    ''' <summary>
    ''' 取消ボタン「請求書取消解除」
    ''' </summary>
    Private Const BTN_TORIKESI_KAIJO As String = "請求書取消解除"

#End Region

#Region "プロパティ"

#Region "パラメータ/請求書一覧画面"

    ''' <summary>
    ''' 起動画面
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _GamenMode As String = String.Empty
    ''' <summary>
    ''' 起動画面
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrGamenMode() As String
        Get
            Return _GamenMode
        End Get
        Set(ByVal value As String)
            _GamenMode = value
        End Set
    End Property

    ''' <summary>
    ''' 請求書NO
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _SeikyuusyoNo As String = String.Empty
    ''' <summary>
    ''' 請求書NO
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrSeikyuusyoNo() As String
        Get
            Return _SeikyuusyoNo
        End Get
        Set(ByVal value As String)
            _SeikyuusyoNo = value
        End Set
    End Property
#End Region

#End Region

#Region "プライベートメソッド"

#Region "初期読込時処理系"

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス
        Dim jSM As New JibanSessionManager

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master

        'スクリプトマネージャーを取得（ScriptManager用）
        masterAjaxSM = Me.SM1

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合
            cl.CloseWindow(Me)
            Me.BtnStyle(False)
            Exit Sub
        End If

        If IsPostBack = False Then
            Try
                '●パラメータのチェック
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
                ' Key情報を保持
                pStrGamenMode = arrSearchTerm(0)
                pStrSeikyuusyoNo = arrSearchTerm(1)

                ' パラメータ不足時は閉じる
                If pStrSeikyuusyoNo Is Nothing OrElse pStrSeikyuusyoNo = String.Empty Then
                    cl.CloseWindow(Me)
                    Me.BtnStyle(False)
                    Exit Sub
                End If
            Catch ex As Exception
                cl.CloseWindow(Me)
                Me.BtnStyle(False)
                Exit Sub
            End Try

            '●権限のチェック
            '経理業務権限
            If userinfo.KeiriGyoumuKengen = 0 Then
                cl.CloseWindow(Me)
                Me.BtnStyle(False)
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            'なし

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            setDispAction()

            'ボタン押下イベントの設定()
            setBtnEvent()

            'フォーカス設定
            BtnClose.Focus()

            '****************************************************************************
            ' 請求書一覧データ取得
            '****************************************************************************
            SetCtrlFromDataRec(sender, e)

        Else
            '画面設定()
            'Me.SetGamenMode(dtRec)

        End If
    End Sub

    ''' <summary>
    ''' 抽出レコードクラスから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object, ByVal e As System.EventArgs)

        dtRec = MyLogic.GetSeikyuuDataRec(sender, pStrSeikyuusyoNo)

        '請求書NO
        Me.TextSeikyuusyoNo.Text = cl.GetDispNum(dtRec.SeikyuusyoNo, "")

        '請求先
        Me.TextSeikyuuSakiCd.Text = cl.GetDispSeikyuuSakiCd(dtRec.SeikyuuSakiKbn, dtRec.SeikyuuSakiCd, dtRec.SeikyuuSakiBrc, False)
        '請求先M.請求先名
        Me.TextSeikyuuSakiMei.Text = cl.GetDisplayString(dtRec.SeikyuuSakiMeiView)
        '請求先名
        Me.TextSeikyuuSakiMei1.Text = cl.GetDisplayString(dtRec.SeikyuuSakiMei)
        '請求先名２
        Me.TextSeikyuuSakiMei2.Text = cl.GetDisplayString(dtRec.SeikyuuSakiMei2)
        '郵便番号
        Me.TextYuubin.Text = cl.GetDisplayString(dtRec.YuubinNo)
        '電話番号
        Me.TextTellNo.Text = cl.GetDisplayString(dtRec.TelNo)
        '住所１
        Me.TextJuusyo1.Text = cl.GetDisplayString(dtRec.Jyuusyo1)
        '住所２
        Me.TextJuusyo2.Text = cl.GetDisplayString(dtRec.Jyuusyo2)
        '明細件数
        Me.TextMeisai.Text = cl.GetDisplayString(dtRec.MeisaiKensuu)
        '担当者
        Me.TextTantousyaMei.Text = cl.GetDisplayString(dtRec.TantousyaMei)
        '前回御請求額
        Me.TextZenSeikyuuGaku.Text = IIf(dtRec.ZenkaiGoseikyuuGaku = 0, _
                                            "", _
                                            dtRec.ZenkaiGoseikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '今回御請求額
        Me.TextKonSeikyuuGaku.Text = IIf(dtRec.KonkaiGoseikyuuGaku = 0, _
                                            "", _
                                            dtRec.KonkaiGoseikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '御入金額
        Me.TextNyuukinGaku.Text = IIf(dtRec.GonyuukinGaku = 0, _
                                            "", _
                                            dtRec.GonyuukinGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '前回繰越残高
        Me.TextZenZandaka.Text = IIf(dtRec.KurikosiGaku = 0, _
                                            "", _
                                            dtRec.KurikosiGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '繰越残高
        Me.TextKonZandaka.Text = IIf(dtRec.KonkaiKurikosiGaku = 0, _
                                            "", _
                                            dtRec.KonkaiKurikosiGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '相殺額
        Me.TextSousaiGaku.Text = IIf(dtRec.SousaiGaku = 0, _
                                            "", _
                                            dtRec.SousaiGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '調整額
        Me.TextTyouseiGaku.Text = IIf(dtRec.TyouseiGaku = 0, _
                                            "", _
                                            dtRec.TyouseiGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        '入金予定日
        Me.TextNyuukinYoteiDate.Text = cl.GetDisplayString(dtRec.KonkaiKaisyuuYoteiDate)
        'データ作成時締め日
        Me.TextDataSakuseijiSimeDate.Text = cl.GetDisplayString(dtRec.SeikyuuSimeDate)
        '請求書発行日
        Me.TextSeikyuusyoHkDate.Text = cl.GetDisplayString(dtRec.SeikyuusyoHakDate)
        '請求書印刷日
        Me.TextSeikyuusyoInsatuDate.Text = cl.GetDisplayString(dtRec.SeikyuusyoInsatuDate)

        '印刷対象外フラグ(請求書用紙汎用コード)
        Me.HiddenPrintTaisyougaiFlg.Value = dtRec.PrintTaisyougaiFlg

        '************
        '* Hidden項目
        '************
        '更新日時
        Me.HiddenUpdDatetime.Value = IIf(dtRec.UpdDatetime = Date.MinValue, "", Format(dtRec.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))

        '画面モード別設定
        SetGamenMode(dtRec)

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        Dim checkDate As String = "checkDate(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 日付系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '入金予定日
        Me.TextNyuukinYoteiDate.Attributes("onblur") = checkDate

    End Sub

    ''' <summary>
    ''' 各種ボタンの設定
    ''' </summary>
    ''' <remarks>
    ''' DB更新の確認を行なう。<br/>
    ''' OK時：DB更新を行なう。<br/>
    ''' キャンセル時：DB更新を中断する。<br/>
    ''' </remarks>
    Private Sub setBtnEvent()

        'イベントハンドラ登録
        Dim tmpScriptOverLay As String = "setWindowOverlay(this,null,1);"
        Dim tmpTouroku As String = "if(confirm('" & Messages.MSG017C & "')){" & tmpScriptOverLay & "}else{return false;}"
        Dim tmpTourokuKakunin As String = "if(confirm('" & Messages.MSG017C & "\r\n" & Messages.MSG159E & "')){" & tmpScriptOverLay & "}else{return false;}"

        '登録許可MSG確認後、OKの場合DB更新処理を行なう
        Me.BtnSyusei.Attributes("onclick") = tmpTouroku     '修正ボタン
        Me.BtnInsatu.Attributes("onclick") = tmpTourokuKakunin     '印刷ボタン
        Me.BtnTorikesi.Attributes("onclick") = tmpTourokuKakunin   '取消ボタン

    End Sub

    ''' <summary>
    ''' ボタン表示処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BtnStyle(ByVal flgDisp As Boolean)

        If flgDisp Then
            'ボタンを表示
            Me.BtnSyusei.Style("display") = "inline"
            Me.BtnInsatu.Style("display") = "inline"
            Me.BtnTorikesi.Style("display") = "inline"
            Me.BtnSeikyuuSakiCall.Style("display") = "inline"
        Else
            'ボタンを非表示
            Me.BtnSyusei.Style("display") = "none"
            Me.BtnInsatu.Style("display") = "none"
            Me.BtnTorikesi.Style("display") = "none"
            Me.BtnSeikyuuSakiCall.Style("display") = "none"
        End If


    End Sub

    ''' <summary>
    ''' 画面の活性化制御
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub setEnabledCtrl(ByVal enabled As Boolean)

        If enabled = True Then
            cl.chgDispSyouhinText(Me.TextSeikyuuSakiMei1)
            cl.chgDispSyouhinText(Me.TextSeikyuuSakiMei2)
            cl.chgDispSyouhinText(Me.TextTellNo)
            cl.chgDispSyouhinText(Me.TextYuubin)
            cl.chgDispSyouhinText(Me.TextJuusyo1)
            cl.chgDispSyouhinText(Me.TextJuusyo2)
            cl.chgDispSyouhinText(Me.TextTantousyaMei)
            cl.chgDispSyouhinText(Me.TextNyuukinYoteiDate)
        End If

    End Sub

    ''' <summary>
    ''' 画面モード別の画面設定
    ''' </summary>
    ''' <param name="dtRec">請求データレコード</param>
    ''' <remarks></remarks>
    Private Sub SetGamenMode(ByVal dtRec As SeikyuuDataRecord)
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable '対象外
        Dim tmpScriptOverLay As String = "setWindowOverlay(this,null,1);"
        Dim tmpConfirm As String = "if(confirm('" & Messages.MSG017C & "')){" & tmpScriptOverLay & "}else{return false;}"
        Dim tmpTorikesi As String = "if(confirm('" & Messages.MSG017C & "\r\n" & Messages.MSG159E & "')){" & tmpScriptOverLay & "}else{return false;}"

        'Hiddenに退避
        Me.HiddenGamenMode.Value = pStrGamenMode

        '請求書Noの存在チェック
        If cl.GetDisplayString(dtRec.SeikyuusyoNo) = String.Empty Then
            'ボタン非表示
            BtnStyle(False)
        Else
            'ボタン表示
            BtnStyle(True)

            '請求鑑.取消の判断処理
            If cl.GetDisplayString(dtRec.Torikesi) = "0" Then
                '取消ボタンの設定
                Me.BtnTorikesi.Value = BTN_TORIKESI
                Me.BtnTorikesi.Style("background-color") = "fuchsia"
                Me.BtnTorikesi.Attributes("onclick") = tmpTorikesi
            Else
                '取消ボタンの設定
                Me.BtnTorikesi.Value = BTN_TORIKESI_KAIJO
                Me.BtnTorikesi.Style("background-color") = "#ffff69"
                Me.BtnTorikesi.Attributes("onclick") = tmpConfirm
            End If

            '遷移元画面による判断処理
            If pStrGamenMode = CStr(EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo) Then
                '請求書一覧画面
                Me.BtnInsatu.Value = EarthConst.BUTTON_SEIKYUUSYO_PRINT
            ElseIf pStrGamenMode = CStr(EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo) Then
                '過去一覧画面
                Me.BtnInsatu.Value = EarthConst.BUTTON_SEIKYUUSYO_RE_PRINT
            End If

            '【活性制御】
            '印刷済と取消による活性判断
            If dtRec.SeikyuusyoInsatuDate <> Date.MinValue Or cl.GetDisplayString(dtRec.Torikesi) <> "0" Then
                '印刷済みor取消の場合非活性化
                jSM.Hash2Ctrl(Me, EarthConst.MODE_VIEW, ht, htNotTarget)
                '修正ボタン非活性化
                Me.BtnSyusei.Disabled = True
                '請求書印刷の場合のみ非活性化
                If Me.BtnInsatu.Value = EarthConst.BUTTON_SEIKYUUSYO_PRINT Then
                    Me.BtnInsatu.Disabled = True
                End If
            Else
                '未印刷and未取消の場合活性化
                setEnabledCtrl(True)
                Me.BtnSyusei.Disabled = False
                '請求書式が印刷可の場合のみ印刷活性化
                If Me.HiddenPrintTaisyougaiFlg.Value = 0 Then
                    Me.BtnInsatu.Disabled = False
                End If
            End If

            '請求書式が印刷対象外・取得できない場合、常に印刷非活性化
            If Me.HiddenPrintTaisyougaiFlg.Value = 1 Or String.IsNullOrEmpty(dtRec.KaisyuuSeikyuusyoYousi) Then
                Me.BtnInsatu.Disabled = True
            End If

            '請求先マスタボタンにリンクを生成
            cl.getSeikyuuSakiMasterPath( _
                                    dtRec.SeikyuuSakiCd _
                                    , dtRec.SeikyuuSakiBrc _
                                    , dtRec.SeikyuuSakiKbn _
                                    , Me.BtnSeikyuuSaki)

            '文字色反転処理(請求鑑.請求締め日と請求先M.請求締め日)
            If dtRec.SeikyuuSimeDate <> dtRec.SeikyuuSimeDateMst Then
                'スタイル設定
                Me.TextDataSakuseijiSimeDate.Style("color") = "red"
                Me.TextDataSakuseijiSimeDate.Style("font-weight") = "bold"
            End If
        End If

    End Sub


#End Region

#Region "DB更新処理系"

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <remarks>
    ''' 必須チェック<br/>
    ''' 禁則文字チェック(文字列入力フィールドが対象)<br/>
    ''' バイト数チェック(文字列入力フィールドが対象)<br/>
    ''' 桁数チェック<br/>
    ''' 日付チェック<br/>
    ''' その他チェック<br/>
    ''' </remarks>
    Public Function checkInput(ByVal prmActBtn As HtmlInputButton) As Boolean
        '地盤画面共通クラス
        Dim jBn As New Jiban

        '全てのコントロールを有効化
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        'エラーメッセージ初期化
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList

        '入力チェックは修正ボタン押下時のみ
        If prmActBtn.ID = Me.BtnInsatu.ID Then
            Return True
        ElseIf prmActBtn.ID = Me.BtnTorikesi.ID Then
            Return True
        ElseIf prmActBtn.ID = Me.BtnSyusei.ID Then

            '●コード入力値変更チェック
            'なし

            '●必須チェック
            '請求先名
            If Me.TextSeikyuuSakiMei1.Text = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "請求先名")
                arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiMei1)
            End If
            '郵便番号
            If Me.TextYuubin.Text = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "郵便番号")
                arrFocusTargetCtrl.Add(Me.TextYuubin)
            End If
            '住所１
            If Me.TextJuusyo1.Text = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "住所１")
                arrFocusTargetCtrl.Add(Me.TextJuusyo1)
            End If

            '●日付チェック
            '入金予定日
            If Me.TextNyuukinYoteiDate.Text <> String.Empty Then
                If cl.checkDateHanni(Me.TextNyuukinYoteiDate.Text) = False Then
                    errMess += Messages.MSG014E.Replace("@PARAM1", "入金予定日")
                    arrFocusTargetCtrl.Add(Me.TextNyuukinYoteiDate)
                End If
            End If

            '●コード桁数チェック
            'なし

            '●禁則文字チェック(文字列入力フィールドが対象)
            '請求先名
            If jBn.KinsiStrCheck(Me.TextSeikyuuSakiMei1.Text) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "請求先名")
                arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiMei1)
            End If
            '請求先名２
            If jBn.KinsiStrCheck(Me.TextSeikyuuSakiMei2.Text) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "請求先名２")
                arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiMei2)
            End If
            '郵便番号
            If jBn.KinsiStrCheck(Me.TextYuubin.Text) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "郵便番号")
                arrFocusTargetCtrl.Add(Me.TextYuubin)
            End If
            '電話番号
            If jBn.KinsiStrCheck(Me.TextTellNo.Text) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "電話番号")
                arrFocusTargetCtrl.Add(Me.TextTellNo)
            End If
            '住所１
            If jBn.KinsiStrCheck(Me.TextJuusyo1.Text) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "住所１")
                arrFocusTargetCtrl.Add(Me.TextJuusyo1)
            End If
            '住所２
            If jBn.KinsiStrCheck(Me.TextJuusyo2.Text) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "住所２")
                arrFocusTargetCtrl.Add(Me.TextJuusyo2)
            End If
            '担当者名
            If jBn.KinsiStrCheck(Me.TextTantousyaMei.Text) = False Then
                errMess += Messages.MSG015E.Replace("@PARAM1", "担当者名")
                arrFocusTargetCtrl.Add(Me.TextTantousyaMei)
            End If

            '●バイト数チェック(文字列入力フィールドが対象)
            '請求先名
            If jBn.ByteCheckSJIS(Me.TextSeikyuuSakiMei1.Text, Me.TextSeikyuuSakiMei1.MaxLength) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "請求先名")
                arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiMei1)
            End If
            '請求先名２
            If jBn.ByteCheckSJIS(Me.TextSeikyuuSakiMei2.Text, Me.TextSeikyuuSakiMei2.MaxLength) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "請求先名２")
                arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiMei2)
            End If
            '郵便番号
            If jBn.ByteCheckSJIS(Me.TextYuubin.Text, Me.TextYuubin.MaxLength) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "郵便番号")
                arrFocusTargetCtrl.Add(Me.TextYuubin)
            End If
            '電話番号
            If jBn.ByteCheckSJIS(Me.TextTellNo.Text, Me.TextTellNo.MaxLength) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "電話番号")
                arrFocusTargetCtrl.Add(Me.TextTellNo)
            End If
            '住所１
            If jBn.ByteCheckSJIS(Me.TextJuusyo1.Text, Me.TextJuusyo1.MaxLength) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "住所１")
                arrFocusTargetCtrl.Add(Me.TextJuusyo1)
            End If
            '住所２
            If jBn.ByteCheckSJIS(Me.TextJuusyo2.Text, Me.TextJuusyo2.MaxLength) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "住所２")
                arrFocusTargetCtrl.Add(Me.TextJuusyo2)
            End If
            '担当者名
            If jBn.ByteCheckSJIS(Me.TextTantousyaMei.Text, Me.TextTantousyaMei.MaxLength) = False Then
                errMess += Messages.MSG016E.Replace("@PARAM1", "担当者名")
                arrFocusTargetCtrl.Add(Me.TextTantousyaMei)
            End If

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
    ''' 画面の内容をDBに反映する
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function SaveData(ByVal emType As EarthEnum.emSeikyuusyoUpdType) As Boolean
        Dim MyLogic As New SeikyuuDataSearchLogic
        Dim listRec As New List(Of SeikyuuDataRecord)

        '画面からレコードクラスにセット
        listRec = Me.GetCtrlToDataList()

        ' データの更新を行います
        If MyLogic.saveData(Me, listRec, emType) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 画面の各情報をレコードクラスに取得し、DB更新用レコードクラスを返却する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCtrlToDataList() As List(Of SeikyuuDataRecord)
        Dim listRec As New List(Of SeikyuuDataRecord)
        Dim dtRec As New SeikyuuDataRecord

        With dtRec
            '***************************************
            ' データ
            '***************************************
            ' 請求書NO
            cl.SetDisplayString(Me.TextSeikyuusyoNo.Text, .SeikyuusyoNo)
            ' 取消
            If Me.BtnTorikesi.Value = BTN_TORIKESI Then
                .Torikesi = 1
            ElseIf Me.BtnTorikesi.Value = BTN_TORIKESI_KAIJO Then
                .Torikesi = 0
            End If
            ' 請求先名
            cl.SetDisplayString(Me.TextSeikyuuSakiMei1.Text, .SeikyuuSakiMei)
            ' 請求先名２
            cl.SetDisplayString(Me.TextSeikyuuSakiMei2.Text, .SeikyuuSakiMei2)
            ' 郵便番号
            cl.SetDisplayString(Me.TextYuubin.Text, .YuubinNo)
            ' 電話番号
            cl.SetDisplayString(Me.TextTellNo.Text, .TelNo)
            ' 住所１
            cl.SetDisplayString(Me.TextJuusyo1.Text, .Jyuusyo1)
            ' 住所２
            cl.SetDisplayString(Me.TextJuusyo2.Text, .Jyuusyo2)
            ' 担当者名
            cl.SetDisplayString(Me.TextTantousyaMei.Text, .TantousyaMei)
            ' 入金予定日
            cl.SetDisplayString(Me.TextNyuukinYoteiDate.Text, .KonkaiKaisyuuYoteiDate)
            ' 請求書印刷日
            cl.SetDisplayString(DateTime.Now, .SeikyuusyoInsatuDate)
            ' 更新者ユーザーID
            .UpdLoginUserId = userinfo.LoginUserId
            ' 更新日時 読み込み時のタイムスタンプ
            If Me.HiddenUpdDatetime.Value = "" Then
                .UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
            Else
                .UpdDatetime = DateTime.ParseExact(Me.HiddenUpdDatetime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
            End If
        End With

        listRec.Add(dtRec)

        Return listRec
    End Function


#End Region

#End Region

#Region "ボタンイベント"

    ''' <summary>
    ''' 修正ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnSyusei_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSyusei.ServerClick
        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.BtnSyusei

        ' 入力チェック
        If Me.checkInput(objActBtn) = False Then Exit Sub

        ' 画面の内容をDBに反映する
        If Me.SaveData(EarthEnum.emSeikyuusyoUpdType.SeikyuusyoSyuusei) Then '登録成功
            '画面を閉じる
            cl.CloseWindow(Me)

        Else '登録失敗
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", Me.BtnSyusei.Value) & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "BtnSyusei_ServerClick", tmpScript, True)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 印刷ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnInsatu_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnInsatu.ServerClick
        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.BtnInsatu

        '請求書印刷ボタン押下時、DB更新を行なう
        If objActBtn.Value = EarthConst.BUTTON_SEIKYUUSYO_PRINT Then

            ' 入力チェック
            If Me.checkInput(objActBtn) = False Then Exit Sub

            ' 画面の内容をDBに反映する
            If Me.SaveData(EarthEnum.emSeikyuusyoUpdType.SeikyuusyoPrint) Then

                pStrSeikyuusyoNo = Me.TextSeikyuusyoNo.Text
                SetCtrlFromDataRec(sender, e)

            Else '登録失敗
                tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", Me.BtnInsatu.Value) & "');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "BtnInsatu_ServerClick", tmpScript, True)
                Exit Sub
            End If

        End If

        'PDF出力処理
        tmpScript = "window.open('" & UrlConst.EARTH2_SEIKYUSYO_FCW_OUTPUT & "?seino=" & Me.TextSeikyuusyoNo.Text & "');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonHiddenPrint_ServerClick2", tmpScript, True)

    End Sub

    ''' <summary>
    ''' 取消ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnTorikesi_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTorikesi.ServerClick
        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.BtnTorikesi
        Dim intExistCnt As Integer = 0

        ' 入力チェック
        If Me.checkInput(objActBtn) = False Then Exit Sub

        ' 請求書Noの取得
        pStrSeikyuusyoNo = Me.TextSeikyuusyoNo.Text

        ' 取消解除時は明細（伝票ユニークNo）が重複していないかチェックする
        If Me.BtnTorikesi.Value = BTN_TORIKESI_KAIJO Then
            intExistCnt = MyLogic.GetDenpyouExistsCnt(sender, pStrSeikyuusyoNo)
            If intExistCnt > 0 Then
                '明細に重複あり
                tmpScript = "alert('" & Messages.MSG170E & "');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "BtnTorikesi_ServerClick", tmpScript, True)
                Exit Sub
            End If
        End If

        ' 画面の内容をDBに反映する
        If Me.SaveData(EarthEnum.emSeikyuusyoUpdType.SeikyuusyoTorikesi) Then

            SetCtrlFromDataRec(sender, e)

        Else '登録失敗
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", Me.BtnTorikesi.Value) & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "BtnTorikesi_ServerClick", tmpScript, True)
            Exit Sub
        End If

    End Sub

#End Region

End Class