
Partial Public Class IkkatuHenkouTysSyouhin
    Inherits System.Web.UI.Page
#Region "一括変更変更対象一覧行コントロールID接頭語"
    Protected ME_CLIENT_ID As String
    Protected Const TYS_SYOUHIN1_CTRL_NAME As String = "Hin1_"
    Protected Const TYS_SYOUHIN2_CTRL_NAME As String = "Hin2_"
#End Region

    ''' <summary>
    ''' 一括変更調査商品１情報/行ユーザーコントロール格納リスト
    ''' </summary>
    ''' <remarks></remarks>
    Private pListItem1Ctrl As New List(Of IkkatuHenkouTysSyouhin1RecordCtrl)
    ''' <summary>
    ''' 一括変更調査商品２情報/行ユーザーコントロール格納リスト
    ''' </summary>
    ''' <remarks></remarks>
    Private pListItem2Ctrl As New List(Of IkkatuHenkouTysSyouhin2RecordCtrl)

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    '文字列定数
    Private Const KKK_NO As String = EarthConst.KOKYAKU_BANGOU
    Private Const BLANK As String = EarthConst.BRANK_STRING
    Private Const ITEM As String = "商品"
    Private Const CRLF As String = "\r\n"

    '共通変数
    Dim iraiSession As New IraiSession
    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    Dim cbLogic As New CommonBizLogic
    Dim sLogic As New SyouhinSearchLogic

    'UtilitiesのMessegeLogicクラス
    Dim MLogic As New MessageLogic
    Dim tLogic As New TokubetuTaiouLogic    '特別対応ロジッククラス

#Region "プロパティ"

#Region "パラメータ/物件検索"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _kbn As String
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrKbn() As String
        Get
            Return _kbn
        End Get
        Set(ByVal value As String)
            _kbn = value
        End Set
    End Property

    ''' <summary>
    ''' 番号(保証書No)
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _no As String
    ''' <summary>
    ''' 番号(保証書No)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrBangou() As String
        Get
            Return _no
        End Get
        Set(ByVal value As String)
            _no = value
        End Set
    End Property

#End Region

#End Region

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス
        Dim jSM As New JibanSessionManager
        Dim logic As New JibanLogic

        'ContentPlaceHolderのIDを取得
        ME_CLIENT_ID = Me.ButtonIkkatuHenkou.Parent.ClientID & Me.ClientIDSeparator.ToString

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合
            Me.ButtonNaiyouChk.Visible = False
            Me.ButtonIkkatuHenkou.Visible = False
            closeWindow()
            Exit Sub
        End If

        'NG調査会社通知メッセージの非表示
        Me.LabelNgTysKaisya.Text = ""
        Me.LabelNgTysKaisya.Visible = False

        If IsPostBack = False Then '初期起動時

            '●パラメータのチェック
            ' Key情報を保持
            _kbn = Request("sendPage_kubun")
            _no = Request("sendPage_hosyoushoNo")

            ' パラメータ不足時は画面を表示しない
            If _kbn Is Nothing Or _no Is Nothing Then
                Me.ButtonNaiyouChk.Visible = False
                Me.ButtonIkkatuHenkou.Visible = False
                closeWindow()
                Exit Sub
            End If

            '●権限のチェック
            '以下のいずれかの権限がない場合、当画面を閉じる

            '依頼業務権限
            If userinfo.IraiGyoumuKengen <> "-1" Then
                Me.ButtonNaiyouChk.Visible = False
                Me.ButtonIkkatuHenkou.Visible = False
                closeWindow()
                Exit Sub
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            Dim helper As New DropDownHelper

            ' 調査会社コンボにデータをバインドする
            helper.SetDropDownList(Me.SelectTysKaisya, DropDownHelper.DropDownType.TyousaKaisya, False, False)

            ' 調査方法コンボにデータをバインドする
            helper.SetDropDownList(Me.SelectTysHouhou, DropDownHelper.DropDownType.TyousaHouhou, True, False)

            ' 商品1コンボにデータをバインドする
            helper.SetDropDownList(Me.SelectSyouhin1, DropDownHelper.DropDownType.Syouhin1, True, True)

            ' 商品コード2コンボにデータをバインドする
            helper.SetDropDownList(Me.SelectSyouhin2, DropDownHelper.DropDownType.Syouhin2Group)
            ' 商品コード2の倉庫コードをHiddenに保持
            Me.setSyouhinSoukoCd()

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            setDispAction()

            'ボタン押下イベントの設定
            setBtnEvent()

            '****************************************************************************
            ' 地盤データ取得
            '****************************************************************************
            If logic.ExistsJibanData(_kbn, _no) Then
                setCtrlFromJibanRec(sender, e)
            Else
                Me.ButtonNaiyouChk.Visible = False
                Me.ButtonIkkatuHenkou.Visible = False
                cl.CloseWindow(Me)
                Exit Sub
            End If

            If Me.ButtonIkkatuHenkou.Disabled = False Then
                Me.ButtonIkkatuHenkou.Focus() '一括変更ボタンにフォーカス
            End If

        Else
            '画面項目設定処理(ポストバック用)
            Me.setDisplayPostBack()
        End If

    End Sub

    ''' <summary>
    ''' 画面項目設定処理(ポストバック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDisplayPostBack()
        Dim intMakeCnt As Integer
        '行数の取得
        intMakeCnt = Integer.Parse(Me.HiddenLineCnt.Value)

        '行作成
        Me.createRow(intMakeCnt)
    End Sub

    ''' <summary>
    ''' ページロードコンプリート
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        '明細行数取得
        Me.HiddenLineCnt.Value = pListItem1Ctrl.Count.ToString
    End Sub

#Region "ボタンイベント"
    ''' <summary>
    ''' 計算ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonNaiyouChk_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNaiyouChk.ServerClick
        Dim clsLogic As New IkkatuHenkouTysSyouhinLogic
        Dim strRecString As String
        Dim syouhin1Rec As Syouhin1AutoSetRecord = Nothing
        Dim syouhin2Rec As TeibetuSeikyuuRecord = Nothing
        'エラーメッセージ初期化
        Dim errMess As String = ""
        Dim errTarget As String = ""
        Dim arrFocusTargetCtrl As New List(Of Control)
        Dim tmpScript As String = ""

        '0. 商品２の行の表示状態を設定
        setDisplayState()

        '1. 変更対象一覧の入力チェック
        '商品1入力チェック(商品の存在チェックは行わない)
        errMess &= checkInput(Me.tblMeisaiSyouhin1, arrFocusTargetCtrl)
        '商品2入力チェック
        errMess &= checkInput(Me.tblMeisaiSyouhin2, arrFocusTargetCtrl)
        '入力チェックエラー
        If arrFocusTargetCtrl.Count > 0 Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Exit Sub
        End If

        '2. 各項目の自動設定
        For intCntI As Integer = 0 To pListItem1Ctrl.Count - 1
            Dim ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl = pListItem1Ctrl.Item(intCntI)
            Dim ctrlItem2Rec As IkkatuHenkouTysSyouhin2RecordCtrl = pListItem2Ctrl.Item(intCntI)

            '**********************
            '* 商品１の自動設定
            '**********************
            With ctrlItem1Rec
                If .AccHiddenTorikeshi.Value <> "1" Then
                    '商品１の設定
                    syouhin1Rec = setItem1(sender, ctrlItem1Rec, .AccCheckAutoCalc.Checked, errMess)

                    '調査方法退避用Hiddenに現在の調査方法を設定
                    .AccHiddenTysHouhouCode.Value = .AccTextTysHouhouCode.Text

                    '商品が取得できない場合エラーメッセージを設定
                    If syouhin1Rec Is Nothing Then
                        errTarget = KKK_NO & .AccTextKokyakuBangou.Text & BLANK
                        If .AccHiddenHattyuusyoGaku.Value <> "0" And .AccHiddenHattyuusyoGaku.Value <> "" Then
                            errMess &= errTarget & Messages.MSG010E & "\r\n"
                        Else
                            errMess &= errTarget & Messages.MSG032E & "\r\n"
                        End If
                        arrFocusTargetCtrl.Add(.AccSelectTysHouhou)
                    End If

                    '商品１承諾書金額の設定
                    setItem1SyoudakuGaku(sender, ctrlItem1Rec, .AccCheckAutoCalc.Checked)

                End If
                '商品１の活性/非活性制御
                .enableItem1()

                '売上処理済みではない、かつ取消加盟店ではない場合は金額の再計算を行う
                If .AccTextUriageSyori.Text = "" And .AccHiddenTorikeshi.Value <> "1" Then
                    '自動計算チェックオフ時の金額再計算
                    setItem1ManualCalc(ctrlItem1Rec, syouhin1Rec)
                End If

                '自動計算チェックボックスのチェックを外す
                .AccCheckAutoCalc.Checked = False

                '金額変更判断フラグをクリア
                .AccHiddenBothKingakuChgFlg.Value = String.Empty

                '画面表示値の連結文字列を取得
                strRecString = getItem1InfoJoinString(ctrlItem1Rec)

                '計算処理用Hiddenに値をセット
                .AccHiddenChgValue.Value = strRecString
            End With

            '**********************
            '* 商品２の自動設定
            '**********************
            With ctrlItem2Rec

                For intCntJ As Integer = 1 To 4
                    Dim strLine As String = "2_" & intCntJ
                    ' 設定するコントロールのインスタンスを取得
                    Dim ctrlRow As IkkatuSyouhin2CtrlReord = ctrlItem2Rec.getItem2RowInfo(strLine)

                    '地盤データを商品２のテーブルにセット
                    setJibanDataToItem2(ctrlRow, ctrlItem1Rec)

                    '商品１が売上処理済みの場合(JavaScriptで追加できないよう制御しているが念の為)
                    If ctrlItem1Rec.AccTextUriageSyori.Text <> "" Then
                        If ctrlRow.UriageSyori.Text = "" Then
                            ctrlRow.Syouhin.SelectedValue = ""
                            ctrlRow.SyouhinLine.Style("display") = "none"
                        End If
                    End If

                    If ctrlRow.Syouhin.SelectedValue <> "" Then
                        '商品２の設定
                        syouhin2Rec = setItem2(sender, ctrlRow, strLine, errMess)
                    End If

                    '商品退避用Hiddenに現在の商品情報を設定
                    ctrlRow.SyouhinBK.Value = ctrlRow.Syouhin.SelectedValue
                    ctrlRow.SyouhinKingakuBK.Value = ctrlRow.KoumutenKingaku.Text & EarthConst.SEP_STRING & _
                                                        ctrlRow.JituSeikyuuKingaku.Text & EarthConst.SEP_STRING & _
                                                        ctrlRow.SyoudakusyoKingaku.Text & EarthConst.SEP_STRING & _
                                                        ctrlRow.SeikyuuUmu.SelectedValue
                    '商品２の活性/非活性制御
                    .enableItem2(ctrlRow)

                    '売上処理済み、若しくは特別対応価格追加済の場合は金額の再計算を行わない
                    If ctrlRow.UriageSyori.Text = "" AndAlso String.IsNullOrEmpty(ctrlRow.TokubetuTaiouToolTip.AccDisplayCd.Value) Then
                        '自動計算チェックオフ時の金額再計算
                        setItem2ManualCalc(ctrlRow, syouhin2Rec)
                    End If

                    '自動計算チェックボックスのチェックを外す
                    ctrlRow.AutoCalc.Checked = False

                    '金額変更判断フラグを初期化
                    ctrlRow.BothKingakuChgFlg.Value = String.Empty

                    '画面表示値の連結値を取得
                    strRecString = getItem2InfoJoinString(ctrlRow)

                    '計算処理用Hiddenに値をセット
                    ctrlRow.ChgValue.Value = strRecString
                Next
            End With
        Next

        'エラー発生時はエラーを表示
        If errMess <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' 登録/修正 実行ボタン１,２押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonIkkatuHenkou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonIkkatuHenkou.ServerClick
        Dim errMess As String = ""
        Dim strErrTarget As String = ""
        Dim arrFocusTargetCtrl As New List(Of Control)
        Dim intErrCnt As Integer = 0
        Dim clsLogic As New IkkatuHenkouTysSyouhinLogic
        Dim tmpScript As String = ""

        '商品２の行の表示状態を設定
        setDisplayState()

        '商品1入力チェック
        errMess &= checkInput(Me.tblMeisaiSyouhin1, arrFocusTargetCtrl, True)
        '商品2入力チェック
        errMess &= checkInput(Me.tblMeisaiSyouhin2, arrFocusTargetCtrl)
        '入力チェックエラー
        If arrFocusTargetCtrl.Count > 0 Then
            showErrDialog(errMess, "ButtonIkkatuHenkou_ServerClick", arrFocusTargetCtrl)
            Exit Sub
        End If

        '計算ボタン押下チェック
        errMess = Messages.MSG127E
        For intCnt As Integer = 0 To pListItem1Ctrl.Count - 1
            strErrTarget &= checkCalc(intCnt, arrFocusTargetCtrl)
        Next
        '計算未処理エラー
        If arrFocusTargetCtrl.Count > 0 Then
            showErrDialog(errMess & strErrTarget, "ButtonIkkatuHenkou_ServerClick", arrFocusTargetCtrl)
            Exit Sub
        End If

        'チェックボックスのチェック判断
        errMess = Messages.MSG129E
        For intCnt As Integer = 0 To pListItem1Ctrl.Count - 1
            strErrTarget &= checkChkBox(intCnt, arrFocusTargetCtrl)
        Next
        'チェック有りエラー
        If arrFocusTargetCtrl.Count > 0 Then
            showErrDialog(errMess & strErrTarget, "ButtonIkkatuHenkou_ServerClick", arrFocusTargetCtrl)
            Exit Sub
        End If

        errMess = ""
        '金額の整合性チェック
        For intCnt As Integer = 0 To pListItem1Ctrl.Count - 1
            errMess &= checkKingaku(intCnt, arrFocusTargetCtrl)
        Next
        'チェック有りエラー
        If arrFocusTargetCtrl.Count > 0 Then
            errMess &= Messages.MSG143E
            showErrDialog(errMess, "ButtonIkkatuHenkou_ServerClick", arrFocusTargetCtrl)
            Exit Sub
        End If

        ' 画面の内容をDBに反映する
        If SaveData() Then '登録成功
            '画面を閉じる
            closeWindow()

        Else '登録失敗
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "一括変更") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonIkkatuHenkou_ServerClick", tmpScript, True)
        End If

    End Sub

#End Region

#Region "商品１設定関連"
    ''' <summary>
    ''' 商品コード１の設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="ctrlItem1Rec">処理対象コントロール行</param>
    ''' <param name="blnAutoCalc">自動計算判断フラグ</param>
    ''' <param name="strErrMsg">エラーメッセージ（参照渡し）</param>
    ''' <remarks></remarks>
    Private Function setItem1(ByVal sender As System.Object, ByVal ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl, ByVal blnAutoCalc As Boolean, ByRef strErrMsg As String) As Syouhin1AutoSetRecord
        ' データ取得用ロジッククラス
        Dim jibanLogic As New JibanLogic
        ' データ取得用パラメータクラス
        Dim paramRec As New Syouhin1InfoRecord
        ' 取得レコード格納クラス
        Dim syouhinRec As New Syouhin1AutoSetRecord
        '加盟店マスタ検索クラス
        Dim kameitenSearchLogic As New KameitenSearchLogic
        '加盟店情報格納クラス
        Dim kameitenRec As New KameitenSearchRecord
        'エラー行情報
        Dim errTarget As String = KKK_NO & ctrlItem1Rec.AccTextKokyakuBangou.Text & BLANK & ITEM & "1" & BLANK
        '関数から帰ってくるエラーメッセージ格納用
        Dim strRetErrMsg As String = String.Empty
        '関数内で出力しているエラーメッセージ取得用
        Dim strSender As String
        'エラー判定フラグ
        Dim intErrJudgeFlg As Integer = 0
        '商品1取得エラーメッセージ
        Dim strSyouhinErr As String = String.Empty

        With ctrlItem1Rec
            ' データ取得用のパラメータセット
            paramRec.Kubun = .AccHiddenKbn.Value    ' 区分
            paramRec.SyouhinKbn = Integer.Parse(.AccHiddenSyouhinKbn.Value) '商品区分

            cl.SetDisplayString(.AccHiddenTatemonoYoutoNo.Value, paramRec.TatemonoYouto)    '建物用途
            'cl.SetDisplayString(.AccHiddenTysGaiyou.Value, paramRec.TyousaGaiyou)           '調査概要
            cl.SetDisplayString(.AccSelectSyouhin1.SelectedValue, paramRec.SyouhinCd)        '商品コード
            cl.SetDisplayString(.AccSelectTysHouhou.SelectedValue, paramRec.TyousaHouhouNo) '調査方法
            cl.SetDisplayString(.AccTextTyousakaisyaCode.Text, paramRec.TysKaisyaCd)        '調査会社CD＋事業所CD
            cl.SetDisplayString(.AccHiddenIraiTousuu.Value, paramRec.DoujiIraiTousuu)       '同時依頼棟数

            '加盟店マスタからデータを取得
            kameitenRec = kameitenSearchLogic.GetKameitenSearchResult(paramRec.Kubun, _
                                                                      .AccTextKameitenCode.Text, _
                                                                      .AccTextTyousakaisyaCode.Text, _
                                                                      True)
            '取得しなおした加盟店情報を設定
            .AccHiddenKeiretuCd.Value = kameitenRec.KeiretuCd
            .AccHiddenEigyousyoCd.Value = kameitenRec.EigyousyoCd

            paramRec.KameitenCd = .AccTextKameitenCode.Text                                 '加盟店コード
            paramRec.KeiretuCd = .AccHiddenKeiretuCd.Value                                  '系列コード
            paramRec.KeiretuFlg = jibanLogic.GetKeiretuFlg(.AccHiddenKeiretuCd.Value)       '系列フラグ
            paramRec.EigyousyoCd = .AccHiddenEigyousyoCd.Value                              '営業所コード

            cl.SetDisplayString(.AccTextJituSeikyuuKingaku.Text, paramRec.ZeinukiKingaku1)  '税抜金額
            cl.SetDisplayString(.AccTextKoumutenKingaku.Text, paramRec.KoumutenKingaku1)    '工務店請求額

            '請求先をセット
            syouhinRec.SeikyuuSakiCd = ctrlItem1Rec.AccHiddenSeikyuuSakiCd.Value

            ' 商品１情報を取得し画面にセット
            strSender = String.Empty
            If jibanLogic.GetSyouhin1Info(strSender, paramRec, syouhinRec, intErrJudgeFlg) = True Then
                If strSender.Length > 0 Then
                    strErrMsg &= errTarget & strSender & CRLF
                End If

                '商品1取得ステータスエラー
                If intErrJudgeFlg > 0 Then
                    '原価・販売両方の場合は2行表示
                    If intErrJudgeFlg = EarthEnum.emSyouhin1Error.GetGenkaHanbai Then
                        strSyouhinErr = cbLogic.GetSyouhin1ErrMsg(EarthEnum.emSyouhin1Error.GetGenka)
                        strErrMsg &= errTarget & strSyouhinErr
                        strSyouhinErr = cbLogic.GetSyouhin1ErrMsg(EarthEnum.emSyouhin1Error.GetHanbai)
                        strErrMsg &= errTarget & strSyouhinErr
                    Else
                        'それ以外はステータスにより1行表示
                        strSyouhinErr = cbLogic.GetSyouhin1ErrMsg(intErrJudgeFlg)
                        strErrMsg &= errTarget & strSyouhinErr & CRLF
                    End If
                End If

                ' 画面にセットする
                .AccHiddenBunruiCd.Value = "100"     ' 分類コード（100固定）
                .AccSelectSyouhin1.SelectedValue = syouhinRec.SyouhinCd     '商品コード1
                .AccHiddenTysSeikyuuSaki.Value = syouhinRec.SeikyuuSakiType '請求先タイプの設定
                .AccHiddenTysGaiyou.Value = cl.GetDispNum(syouhinRec.TyousaGaiyou, String.Empty)        '調査概要の設定

                '●特別対応価格反映(商品1変更時、特別価格を＋)
                '商品取得後かつ売上金額設定前に、特別価格を加算しておく
                calcTeiBetuTokubetuKkk(sender, ctrlItem1Rec, syouhinRec)

                '計算チェック有り無しで条件分岐
                If blnAutoCalc Then
                    ' 有無しか無いが、空白が登場してもいいように既存と同じく空白は無にする
                    If .AccSelectSeikyuuUmu.SelectedValue = "" Then
                        .AccSelectSeikyuuUmu.SelectedValue = "0"
                    End If

                    '********************************************************************
                    '* 販売価格(工務店・実請求の金額／変更可否設定)
                    '********************************************************************
                    If intErrJudgeFlg = EarthEnum.emSyouhin1Error.GetGenkaHanbai Or intErrJudgeFlg = EarthEnum.emSyouhin1Error.GetHanbai Then
                        '●販売価格マスタ取得不可は、金額設定なし・変更可
                        If .AccSelectSeikyuuUmu.SelectedValue = "1" Then
                            .AccKmtnHenkouKahi.Value = String.Empty
                            .AccJituGakuHenkouKahi.Value = String.Empty
                        Else
                            .AccKmtnHenkouKahi.Value = EarthConst.HENKOU_FUKA
                            .AccJituGakuHenkouKahi.Value = EarthConst.HENKOU_FUKA
                        End If
                    Else
                        '●販売価格マスタからの取得可能の場合は設定
                        ' 請求ありの場合、金額をセット
                        If .AccSelectSeikyuuUmu.SelectedValue = "1" Then
                            ' 工務店請求額
                            .AccTextKoumutenKingaku.Text = syouhinRec.KoumutenGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                            ' 工務店請求額変更可否フラグ
                            If syouhinRec.KoumutenGakuHenkouFlg Then
                                .AccKmtnHenkouKahi.Value = String.Empty
                            Else
                                .AccKmtnHenkouKahi.Value = EarthConst.HENKOU_FUKA
                            End If
                            ' 実請求額
                            .AccTextJituSeikyuuKingaku.Text = syouhinRec.JituGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                            ' 実請求額変更可否フラグ
                            If syouhinRec.JituGakuHenkouFlg Then
                                .AccJituGakuHenkouKahi.Value = String.Empty
                            Else
                                .AccJituGakuHenkouKahi.Value = EarthConst.HENKOU_FUKA
                            End If
                        Else
                            ' 工務店請求額
                            .AccTextKoumutenKingaku.Text = "0"
                            .AccKmtnHenkouKahi.Value = EarthConst.HENKOU_FUKA       '変更不可
                            ' 実請求額
                            .AccTextJituSeikyuuKingaku.Text = "0"
                            .AccJituGakuHenkouKahi.Value = EarthConst.HENKOU_FUKA   '変更不可
                        End If
                    End If

                    '金額の先行変更判断フラグをクリア
                    .AccHiddenAutoKingakuFlg.Value = ""
                Else
                    If .AccHiddenManualKingakuFlg.Value = .AccHiddenAutoKingakuFlg.Value Or paramRec.Seikyuusaki = EarthConst.SEIKYU_TYOKUSETU Then
                        '金額の先行変更判断フラグをクリア
                        If .AccHiddenAutoKingakuFlg.Value = "1" Then
                            '工務店請求金額変更時処理
                            strSender = String.Empty
                            .AccTextJituSeikyuuKingaku.Text = getJituGaku(strSender, _
                                                                            paramRec.Seikyuusaki, _
                                                                            paramRec.KeiretuFlg, _
                                                                            paramRec.KeiretuCd, _
                                                                            .AccTextKoumutenKingaku.Text, _
                                                                            .AccTextJituSeikyuuKingaku.Text, _
                                                                            .AccSelectSyouhin1.SelectedValue)
                            strRetErrMsg = errTarget & strSender & CRLF
                            If strSender.Length > 0 And strErrMsg.IndexOf(strRetErrMsg) = -1 Then

                                strErrMsg &= errTarget & strSender & CRLF
                            End If
                        ElseIf .AccHiddenAutoKingakuFlg.Value = "2" Then
                            '実請求金額変更時処理
                            strSender = String.Empty
                            .AccTextKoumutenKingaku.Text = getKoumuGaku(strSender, _
                                                                        paramRec.Seikyuusaki, _
                                                                        paramRec.KeiretuFlg, _
                                                                        paramRec.KeiretuCd, _
                                                                        .AccTextJituSeikyuuKingaku.Text, _
                                                                        .AccTextKoumutenKingaku.Text, _
                                                                        .AccSelectSyouhin1.SelectedValue, _
                                                                        1)
                            strRetErrMsg = errTarget & strSender & CRLF
                            If strSender.Length > 0 And strErrMsg.IndexOf(strRetErrMsg) = -1 Then
                                strErrMsg &= errTarget & strSender & CRLF
                            End If
                        End If
                    End If
                End If

                '金額がブランクの場合"0"をセット
                If .AccTextJituSeikyuuKingaku.Text = String.Empty Then
                    .AccTextJituSeikyuuKingaku.Text = "0"
                End If
                If .AccTextKoumutenKingaku.Text = String.Empty Then
                    .AccTextKoumutenKingaku.Text = "0"
                End If

                '金額の変更判断フラグをクリア
                .AccHiddenManualKingakuFlg.Value = ""
                '金額が両方変更されたか判断フラグを設定（計算済の場合は再計算不要）
                .AccHiddenBothKingakuChgFlg.Value = "1"

                ' 非表示項目
                .AccHiddenKakakuSetteiBasyo.Value = syouhinRec.KakakuSettei
            ElseIf intErrJudgeFlg <> EarthEnum.emSyouhin1Error.GetTysGaiyou Then
                '商品価格設定マスタ取得エラー以外はMsgのみ
                strSyouhinErr = cbLogic.GetSyouhin1ErrMsg(intErrJudgeFlg)
                strErrMsg &= errTarget & strSyouhinErr & CRLF
            Else
                '商品価格設定マスタ取得エラーは商品1クリア
                If strSender.Length > 0 Then
                    strErrMsg &= errTarget & strSender & CRLF
                End If
                '商品情報が取得できなかった場合
                If .AccHiddenHattyuusyoGaku.Value <> "0" And .AccHiddenHattyuusyoGaku.Value <> "" Then
                    '発注書金額≠0かつ≠NULLの場合、自動設定(クリア処理)を行わない
                    .AccTextTysHouhouCode.Text = .AccHiddenTysHouhouCode.Value
                    .AccSelectTysHouhou.SelectedValue = .AccHiddenTysHouhouCode.Value
                Else
                    '商品１をクリア
                    clearItem1Info(ctrlItem1Rec)
                End If

                Return Nothing
            End If

        End With

        Return syouhinRec

    End Function

    ''' <summary>
    ''' 商品１承諾書金額の設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="ctrlItem1Rec">処理対象コントロール行</param>
    ''' <param name="blnAutoCalc">自動計算判断フラグ</param>
    ''' <remarks></remarks>
    Private Sub setItem1SyoudakuGaku(ByVal sender As System.Object, ByVal ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl, ByVal blnAutoCalc As Boolean)

        Dim jibanLogic As New JibanLogic

        ' 取得する承諾書価格
        Dim syoudaku_kakaku As Integer = 0

        Dim chousa_jouhou As Integer
        Dim chousa_gaiyou As Integer
        Dim kakaku_settei As Integer
        Dim irai_tousuu As Integer
        Dim blnSyoudakuHenkouFlg As Boolean

        If blnAutoCalc Then
            With ctrlItem1Rec

                ' 数値項目の変換
                cl.SetDisplayString(.AccTextTysHouhouCode.Text, chousa_jouhou)
                cl.SetDisplayString(.AccHiddenTysGaiyou.Value, chousa_gaiyou)
                cl.SetDisplayString(.AccHiddenKakakuSetteiBasyo.Value, kakaku_settei)
                cl.SetDisplayString(.AccHiddenIraiTousuu.Value, irai_tousuu)

                If jibanLogic.GetSyoudakusyoKingaku1(.AccSelectSyouhin1.SelectedValue, _
                                             .AccHiddenKbn.Value, _
                                             chousa_jouhou, _
                                             chousa_gaiyou, _
                                             irai_tousuu, _
                                             .AccTextTyousakaisyaCode.Text, _
                                             .AccTextKameitenCode.Text, _
                                             syoudaku_kakaku, _
                                             .AccHiddenKeiretuCd.Value, _
                                             blnSyoudakuHenkouFlg) = True Then

                    ' 承諾書金額を画面にセット
                    .AccTextSyoudakusyoKingaku.Text = syoudaku_kakaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                    ' 承諾書金額変更可否の設定
                    If blnSyoudakuHenkouFlg = False Then
                        .AccSdsHenkouKahi.Value = EarthConst.HENKOU_FUKA
                    Else
                        .AccSdsHenkouKahi.Value = String.Empty
                    End If
                Else
                    '●原価マスタに該当の組み合わせが無いかつ商品設定無しは、金額無し変更不可
                    If .AccSelectSyouhin1.SelectedValue = "" Then
                        .AccTextSyoudakusyoKingaku.Text = ""
                        .AccSdsHenkouKahi.Value = EarthConst.HENKOU_FUKA
                    Else
                        '原価マスタに該当の組み合わせ無しのみは変更可
                        .AccSdsHenkouKahi.Value = ""
                    End If
                End If
            End With
        End If
    End Sub

    ''' <summary>
    ''' 指定商品行の値をクリアする(商品１)
    ''' </summary>
    ''' <param name="ctrlItem1Rec">処理対象コントロール行</param>
    ''' <remarks></remarks>
    Private Sub clearItem1Info(ByVal ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl)
        With ctrlItem1Rec
            .AccSelectSyouhin1.SelectedValue = String.Empty
            .AccTextJituSeikyuuKingaku.Text = String.Empty
            .AccTextKoumutenKingaku.Text = String.Empty
            .AccTextSyoudakusyoKingaku.Text = String.Empty
            .AccSelectSeikyuuUmu.SelectedValue = "1"

        End With
    End Sub

    ''' <summary>
    ''' 自動計算チェックオフ時の商品１金額計算
    ''' </summary>
    ''' <param name="ctrlItem1Rec">処理対象コントロール行</param>
    ''' <param name="syouhinRec">商品コード1の自動設定データレコード</param>
    ''' <remarks></remarks>
    Private Sub setItem1ManualCalc(ByVal ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl, ByVal syouhinRec As Syouhin1AutoSetRecord)

        With ctrlItem1Rec
            If .AccCheckAutoCalc.Checked = False And syouhinRec IsNot Nothing Then
                If .AccTextKoumutenKingaku.ReadOnly = True Then
                    If .AccSelectSeikyuuUmu.SelectedValue = "1" Then
                        .AccTextKoumutenKingaku.Text = syouhinRec.KoumutenGaku.ToString(EarthConst.FORMAT_KINGAKU_1)      ' 工務店請求額
                    Else
                        .AccTextKoumutenKingaku.Text = "0"   ' 工務店請求額
                    End If
                End If
                If .AccTextJituSeikyuuKingaku.ReadOnly = True Then
                    If .AccSelectSeikyuuUmu.SelectedValue = "1" Then
                        .AccTextJituSeikyuuKingaku.Text = syouhinRec.JituGaku.ToString(EarthConst.FORMAT_KINGAKU_1)              ' 実請求額
                    Else
                        .AccTextJituSeikyuuKingaku.Text = "0"       ' 実請求額
                    End If
                End If
            End If
        End With

    End Sub

    ''' <summary>
    ''' 画面表示値の連結文字列を取得(商品１)
    ''' </summary>
    ''' <param name="ctrlItem1Rec">処理対象コントロール行</param>
    ''' <returns>画面表示値の連結文字列</returns>
    ''' <remarks></remarks>
    Private Function getItem1InfoJoinString(ByVal ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl) As String
        Dim clsLogic As New IkkatuHenkouTysSyouhinLogic
        Dim strItem1ChgValues() As String
        Dim dicItem1 As New Dictionary(Of String, String)
        Dim strItem1InfoJoinString As String

        '計算処理用Hiddenの値を取得
        strItem1ChgValues = clsLogic.getArrayFromDollarSep(ctrlItem1Rec.AccHiddenChgValue.Value)

        '計算処理用Hiddenの値をDictionaryに登録
        dicItem1 = clsLogic.getDicItem1(strItem1ChgValues)

        '自動設定後の商品１の各コントロール値をDictionaryに格納
        dicItem1 = setDicAfterCalcItem1(ctrlItem1Rec, dicItem1)

        '画面表示値の連結値を取得
        strItem1InfoJoinString = clsLogic.getJoinString(dicItem1.Values.GetEnumerator)

        Return strItem1InfoJoinString

    End Function

    ''' <summary>
    ''' 自動設定後の商品１の各コントロール値をDictionaryに登録
    ''' </summary>
    ''' <param name="ctrlItem1Rec">処理対象コントロール行</param>
    ''' <param name="dic">計算処理用Hidden値のDictionary</param>
    ''' <returns>各コントロール値が設定されたDictionary</returns>
    ''' <remarks>※画面に表示されている金額以外を登録 ⇒ 計算処理後に発生する金額のみの変更は再計算が不要な為</remarks>
    Private Function setDicAfterCalcItem1(ByVal ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl, ByVal dic As Dictionary(Of String, String)) As Dictionary(Of String, String)
        With ctrlItem1Rec
            '地盤テーブル用データのセット
            dic("Kbn") = .AccHiddenKbn.Value                                            '区分 ------------------- (0)
            dic("HosyousyoNo") = .AccHiddenHosyousyoNo.Value                            '保証書NO --------------- (1)
            dic("TysHouhou") = .AccTextTysHouhouCode.Text                               '調査方法 --------------- (2)
            '会社コードを分割
            Dim strDispKaisyaCd As String = .AccTextTyousakaisyaCode.Text
            .AccHiddenTysKaisyaCd.Value = strDispKaisyaCd.Substring(0, strDispKaisyaCd.Length - 2)
            .AccHiddenTysKaisyaJigyousyoCd.Value = strDispKaisyaCd.Substring(strDispKaisyaCd.Length - 2)

            dic("TysKaisyaCd") = .AccHiddenTysKaisyaCd.Value                            '調査会社コード --------- (3)
            dic("TysKaisyaJigyousyoCd") = .AccHiddenTysKaisyaJigyousyoCd.Value          '調査会社事業所コード --- (4)
            dic("KameitenCd") = .AccTextKameitenCode.Text                               '加盟店コード ----------- (5)
            dic("SesyuMei") = .AccTextSesyuName.Text                                    '施主名 ----------------- (6)
            dic("SyouhinKbn") = .AccHiddenSyouhinKbn.Value                              '商品区分 --------------- (7)
            dic("TysGaiyou") = .AccHiddenTysGaiyou.Value                                '調査概要 --------------- (8)
            dic("IraiTousuu") = .AccHiddenIraiTousuu.Value                              '依頼棟数 --------------- (9)
            dic("KakakuSetteiBasyo") = .AccHiddenKakakuSetteiBasyo.Value                '価格設定場所 ----------- (10)
            dic("AddDatetimeJiban") = .AccHiddenAddDatetimeJiban.Value                  '登録日時 --------------- (11)
            dic("UpdDatetimeJiban") = .AccHiddenUpdDatetimeJiban.Value                  '更新日時 --------------- (12)
            '邸別請求テーブル用データのセット
            dic("BunruiCd") = .AccHiddenBunruiCd.Value                                  '分類コード ------------- (13)
            dic("GamenHyoujiNo") = .AccHiddenGamenHyoujiNo.Value                        '画面表示NO ------------- (14)
            dic("SyouhinCd") = .AccSelectSyouhin1.SelectedValue                         '商品コード ------------- (15)
            If .AccTextUriageSyori.Text = "" Then
                dic("UriKeijyouFlg") = "0"                                              '売上計上フラグ[0] ------ (21)
            Else
                dic("UriKeijyouFlg") = "1"                                              '売上計上フラグ[1] ------ (21)
            End If
            dic("SeikyuuUmu") = .AccSelectSeikyuuUmu.SelectedValue                      '請求有無 --------------- (21)
        End With

        Return dic
    End Function

    ''' <summary>
    ''' 画面表示金額の連結文字列を取得(商品１)
    ''' </summary>
    ''' <param name="ctrlitem1rec">処理対象コントロール</param>
    ''' <returns>画面金額の連結文字列</returns>
    ''' <remarks></remarks>
    Private Function getItem1KingakuString(ByVal ctrlitem1rec As IkkatuHenkouTysSyouhin1RecordCtrl) As String
        Dim clsLogic As New IkkatuHenkouTysSyouhinLogic
        Dim strItem1ChgValues() As String
        Dim dicItem1 As New Dictionary(Of String, String)
        Dim stritem1KingakuJoinString As String

        '変更確認用Hiddenの値を取得
        strItem1ChgValues = clsLogic.getArrayFromDollarSep(ctrlitem1rec.AccHiddenChgKingaku.Value)

        '変更確認用Hiddenの値をDictionaryに登録
        dicItem1 = clsLogic.getDicItem1Kingaku(strItem1ChgValues)

        '自動設定後の商品１金額をDictionaryに格納
        dicItem1 = setDicAfterCalcItem1Kingaku(ctrlitem1rec, dicItem1)

        '画面金額の連結値を取得
        stritem1KingakuJoinString = clsLogic.getJoinString(dicItem1.Values.GetEnumerator)

        Return stritem1KingakuJoinString

    End Function

    ''' <summary>
    ''' 自動設定後の商品１の各金額をDictionaryに登録
    ''' </summary>
    ''' <param name="ctrlItem1Rec">処理対象コントロール行</param>
    ''' <param name="dic">変更確認用Hidden値のDictionary</param>
    ''' <returns>各金額が設定されたDictionary</returns>
    ''' <remarks></remarks>
    Private Function setDicAfterCalcItem1Kingaku(ByVal ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl, ByVal dic As Dictionary(Of String, String)) As Dictionary(Of String, String)
        '
        With ctrlItem1Rec
            dic("KoumutenSeikyuuGaku") = .AccTextKoumutenKingaku.Text
            dic("UriGaku") = .AccTextJituSeikyuuKingaku.Text
            dic("SiireGaku") = .AccTextSyoudakusyoKingaku.Text
        End With

        Return dic

    End Function
#End Region

    ''' <summary>
    ''' 商品2コンボの倉庫コードをHiddenに格納
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setSyouhinSoukoCd()
        Dim syouhin2List As New List(Of SyouhinMeisaiRecord)
        Dim recTmp As New SyouhinMeisaiRecord   '作業用
        Dim strSyouhincCd As String = ""        '作業用
        Dim strBunruiCd As String = ""          '作業用

        syouhin2List = sLogic.GetSyouhinSoukoCd(800)

        For intCnt As Integer = 0 To syouhin2List.Count - 1
            recTmp = syouhin2List(intCnt)

            If intCnt = syouhin2List.Count - 1 Then
                strSyouhincCd &= recTmp.SyouhinCd
                strBunruiCd &= recTmp.SoukoCd
            Else
                strSyouhincCd &= recTmp.SyouhinCd & EarthConst.SEP_STRING
                strBunruiCd &= recTmp.SoukoCd & EarthConst.SEP_STRING
            End If
        Next
        'Hiddenに格納
        Me.HiddenArrSyouhinCd.Value = strSyouhincCd
        Me.HiddenArrBunruiCd.Value = strBunruiCd
    End Sub

    ''' <summary>
    ''' 変更設定された商品1に対して、再度特別対応価格を反映する
    ''' </summary>
    ''' <param name="ctrlItem1Rec"></param>
    ''' <remarks></remarks>
    Private Sub calcTeiBetuTokubetuKkk(ByVal sender As System.Object, ByVal ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl, ByRef Syouhin1Rec As Syouhin1AutoSetRecord)

        Dim listRec As New List(Of TokubetuTaiouRecordBase)
        Dim lgcTokuTai As New TokubetuTaiouLogic
        Dim intTokutaiCnt As Integer = Integer.MinValue
        Dim lgcJiban As New JibanLogic
        Dim recJiban As JibanRecord
        Dim intTmpKingakuAction As Integer = Integer.MinValue

        '地盤データの取得
        recJiban = lgcJiban.GetJibanData(ctrlItem1Rec.AccHiddenKbn.Value, ctrlItem1Rec.AccHiddenHosyousyoNo.Value)

        '地盤レコード商品1をDB値から上書き(再計算用)
        UpdateJibanRecFromCtrl(recJiban, ctrlItem1Rec, Syouhin1Rec)

        '特別対応マスタベースの特別対応データをDBから取得
        If Not recJiban Is Nothing AndAlso Not recJiban.Syouhin1Record Is Nothing Then
            listRec = lgcTokuTai.GetTokubetuTaiouDataInfo(sender, _
                                                 ctrlItem1Rec.AccHiddenKbn.Value, _
                                                 ctrlItem1Rec.AccHiddenHosyousyoNo.Value, _
                                                 ctrlItem1Rec.AccTokubetuTaiouToolTip.AccDisplayCd.Value, _
                                                 intTokutaiCnt)
        End If

        '処理件数チェック
        If intTokutaiCnt <= 0 Then
            Exit Sub
        End If

        '特別対応リスト内を走査し、画面情報を追記する
        For intListCnt As Integer = 0 To listRec.Count - 1
            '強制更新フラグ
            listRec(intListCnt).UpdFlg = "1"
            'これから加算するので設定先情報を消去(画面にないからロジックで加算する)
            listRec(intListCnt).GamenHyoujiNo = Nothing
            listRec(intListCnt).BunruiCd = String.Empty
        Next

        '特別対応価格反映処理
        intTmpKingakuAction = cbLogic.pf_ChkTokubetuTaiouKkk(sender, listRec, recJiban, True)
        If intTmpKingakuAction <= EarthEnum.emKingakuAction.KINGAKU_ALERT Then
            MLogic.AlertMessage(sender, Messages.MSG200W.Replace("@PARAM1", cbLogic.AccTokubetuTaiouKkkMsg), 0, "KkkException")
        End If

        '価格算出後の邸別レコードを商品レコードに再セット
        If Not recJiban.Syouhin1Record Is Nothing Then
            '工務店請求金額
            Syouhin1Rec.KoumutenGaku = recJiban.Syouhin1Record.KoumutenSeikyuuGaku
            '売上金額・実請求金額
            Syouhin1Rec.JituGaku = recJiban.Syouhin1Record.UriGaku
        End If

        '特別対応ツールチップ最表示
        DevideTokubetuTaiouCd(sender, ctrlItem1Rec, listRec)
        '特別対応ツールチップのHidden項目を更新（登録時用）
        DevideTokubetuTaiouHidden(sender, ctrlItem1Rec, listRec)

    End Sub

    ''' <summary>
    ''' 特別対応価格算出の為、地盤レコードに商品レコード情報を上書く
    ''' </summary>
    ''' <param name="recJiban"></param>
    ''' <param name="ctrlItem1Rec"></param>
    ''' <param name="Syouhin1Rec"></param>
    ''' <remarks></remarks>
    Private Sub UpdateJibanRecFromCtrl(ByRef recJiban As JibanRecord, ByVal ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl, ByVal Syouhin1Rec As Syouhin1AutoSetRecord)

        '【地盤情報】
        '調査方法
        cl.SetDisplayString(ctrlItem1Rec.AccTextTysHouhouCode.Text, recJiban.TysHouhou)
        '調査会社
        cl.SetDisplayString(ctrlItem1Rec.AccTextTyousakaisyaCode.Text, recJiban.TysKaisyaCd)

        '【商品1情報】（更新項目）
        '商品コード
        recJiban.Syouhin1Record.SyouhinCd = Syouhin1Rec.SyouhinCd
        '売上金額・実請求金額
        recJiban.Syouhin1Record.UriGaku = Syouhin1Rec.JituGaku
        '工務店請求金額
        recJiban.Syouhin1Record.KoumutenSeikyuuGaku = Syouhin1Rec.KoumutenGaku

    End Sub

#Region "商品２設定関連"

    ''' <summary>
    ''' 行の表示状態を設定
    ''' </summary>
    ''' <remarks>追加／削除した行の状態を維持</remarks>
    Private Sub setDisplayState()
        For intCntI As Integer = 0 To pListItem1Ctrl.Count - 1
            Dim ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl = pListItem1Ctrl.Item(intCntI)
            Dim ctrlItem2Rec As IkkatuHenkouTysSyouhin2RecordCtrl = pListItem2Ctrl.Item(intCntI)
            For intCntJ As Integer = 1 To 4
                Dim strLine As String = "2_" & intCntJ
                ' 設定するコントロールのインスタンスを取得
                Dim ctrlRow As IkkatuSyouhin2CtrlReord = ctrlItem2Rec.getItem2RowInfo(strLine)

                '行の表示状態を設定
                ctrlRow.SyouhinLine.Style("display") = ctrlRow.RowDisplay.Value
            Next
        Next
    End Sub

    ''' <summary>
    ''' 地盤データを商品２のHidden項目にセットする
    ''' </summary>
    ''' <param name="ctrlRow">商品２の処理対象行</param>
    ''' <param name="ctrlItem1Rec">商品１の処理対象コントロール行</param>
    ''' <remarks></remarks>
    Private Sub setJibanDataToItem2(ByVal ctrlRow As IkkatuSyouhin2CtrlReord, ByVal ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl)
        '加盟店コード関連を取得
        ctrlRow.KameitenCd.Value = ctrlItem1Rec.AccTextKameitenCode.Text
        ctrlRow.KeiretuCd.Value = ctrlItem1Rec.AccHiddenKeiretuCd.Value

    End Sub

    ''' <summary>
    ''' 商品コード２の設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="ctrlRow">処理対象行</param>
    ''' <param name="syouhinType">インスタンスを取得したいレコードタイプ</param>
    ''' <param name="strErrMsg">エラーメッセージ（参照渡し）</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function setItem2(ByVal sender As System.Object, ByVal ctrlRow As IkkatuSyouhin2CtrlReord, ByVal syouhinType As String, ByRef strErrMsg As String) As TeibetuSeikyuuRecord
        ' データ取得用パラメータクラス
        Dim paramRec As New Syouhin23InfoRecord
        ' 取得レコード格納クラス
        Dim syouhinRec As Syouhin23Record
        '邸別請求レコード
        Dim teibetu_seikyuu_rec As TeibetuSeikyuuRecord
        'エラー行情報
        Dim strErrGyouInfo As String = KKK_NO & ctrlRow.KokyakuBangou.Text & BLANK & ITEM & syouhinType & BLANK
        '関数から帰ってくるエラーメッセージ格納用
        Dim strRetErrMsg As String = String.Empty
        '関数内で出力しているエラーメッセージ取得用
        Dim strSender As String

        With ctrlRow
            ' 商品の基本情報を取得
            syouhinRec = getItem2Info(.Syouhin.SelectedValue, .KameitenCd.Value)
            ' 取得できない場合、エラー
            If syouhinRec Is Nothing Then
                ' 行をクリアして処理終了
                clearItem2Info(ctrlRow)
                Return Nothing
                Exit Function
            End If

            ' 邸別請求情報取得用のロジッククラス
            Dim jibanLogic As New JibanLogic

            ' 商品コード及び画面の情報をセット
            paramRec.Syouhin2Rec = syouhinRec                                       ' 商品の基本情報
            paramRec.SeikyuuUmu = ctrlRow.SeikyuuUmu.SelectedValue                  ' 請求有無
            '発注書確定フラグの初期値
            If ctrlRow.HattyuusyoKakuteiFlg.Value = String.Empty Then
                ctrlRow.HattyuusyoKakuteiFlg.Value = "0"
            End If
            paramRec.HattyuusyoKakuteiFlg = ctrlRow.HattyuusyoKakuteiFlg.Value      ' 発注書確定フラグ
            If ctrlRow.SeikyuuSakiCd.Value <> String.Empty Then                     ' 請求先コード
                syouhinRec.SeikyuuSakiCd = ctrlRow.SeikyuuSakiCd.Value
            End If
            paramRec.Seikyuusaki = syouhinRec.SeikyuuSakiType                       ' 直接請求/他請求
            paramRec.KeiretuCd = .KeiretuCd.Value                                   ' 系列コード
            paramRec.KeiretuFlg = jibanLogic.GetKeiretuFlg(.KeiretuCd.Value)        ' 系列フラグ

            '請求先タイプの設定
            .TysSeikyuuSaki.Value = paramRec.Seikyuusaki

            ' 請求レコードの取得(確実に結果が有る)
            strSender = String.Empty
            teibetu_seikyuu_rec = getItem2SeikyuuInfo(strSender, paramRec, ctrlRow)
            If strSender.Length > 0 Then
                strErrMsg &= strErrGyouInfo & strSender & CRLF
            End If

            '税区分
            .ZeiKbn.Value = syouhinRec.ZeiKbn
            .HattyuusyoKakuteiFlg.Value = teibetu_seikyuu_rec.HattyuusyoKakuteiFlg

            ' コード、名称をセット（多棟割りの場合、設定必須の為）
            .BunruiCd.Value = syouhinRec.SoukoCd
            .Syouhin.SelectedValue = syouhinRec.SyouhinCd

            If .AutoCalc.Checked Then
                '有無しか無いが、空白が登場してもいいように既存と同じく空白は無にする(但し、商品がブランク以外のとき)
                If .SeikyuuUmu.SelectedValue = "" And .Syouhin.SelectedValue <> "" Then
                    .SeikyuuUmu.SelectedValue = "0"
                End If

                ' 請求ありの場合、金額をセット
                If .SeikyuuUmu.SelectedValue = "1" Then
                    ' 価格情報をセット
                    .KoumutenKingaku.Text = teibetu_seikyuu_rec.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                    .JituSeikyuuKingaku.Text = teibetu_seikyuu_rec.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                    .SyoudakusyoKingaku.Text = teibetu_seikyuu_rec.SiireGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                Else
                    .KoumutenKingaku.Text = "0"
                    .JituSeikyuuKingaku.Text = "0"
                    '承諾書金額がブランクだったら0にする
                    If .SyoudakusyoKingaku.Text = "" Then
                        .SyoudakusyoKingaku.Text = "0"
                    End If
                End If
                '金額の先行変更判断フラグをクリア
                .AutoKingakuFlg.Value = ""
            Else
                If .AutoKingakuFlg.Value = .ManualKingakuFlg.Value Or paramRec.Seikyuusaki = EarthConst.SEIKYU_TYOKUSETU Then
                    '金額の先行変更判断フラグをクリア
                    If .AutoKingakuFlg.Value = "1" Then
                        '工務店請求金額変更時処理
                        strSender = String.Empty
                        .JituSeikyuuKingaku.Text = getJituGaku(strSender, _
                                                                paramRec.Seikyuusaki, _
                                                                paramRec.KeiretuFlg, _
                                                                paramRec.KeiretuCd, _
                                                                .KoumutenKingaku.Text, _
                                                                .JituSeikyuuKingaku.Text, _
                                                                .Syouhin.SelectedValue)
                        strRetErrMsg = strErrGyouInfo & strSender & CRLF
                        If strSender.Length > 0 And strErrMsg.IndexOf(strRetErrMsg) = -1 Then
                            strErrMsg &= strErrGyouInfo & strSender & CRLF
                        End If
                    ElseIf .AutoKingakuFlg.Value = "2" Then
                        '実請求金額変更時処理
                        strSender = String.Empty
                        .KoumutenKingaku.Text = getKoumuGaku(strSender, _
                                                                paramRec.Seikyuusaki, _
                                                                paramRec.KeiretuFlg, _
                                                                paramRec.KeiretuCd, _
                                                                .JituSeikyuuKingaku.Text, _
                                                                .KoumutenKingaku.Text, _
                                                                .Syouhin.SelectedValue, _
                                                                2)
                        strRetErrMsg = strErrGyouInfo & strSender & CRLF
                        If strSender.Length > 0 And strErrMsg.IndexOf(strRetErrMsg) = -1 Then
                            strErrMsg &= strErrGyouInfo & strSender & CRLF
                        End If
                    End If
                End If
            End If

            '金額がブランクの場合"0"をセット
            If .JituSeikyuuKingaku.Text = String.Empty Then
                .JituSeikyuuKingaku.Text = "0"
            End If
            If .KoumutenKingaku.Text = String.Empty Then
                .KoumutenKingaku.Text = "0"
            End If

            '金額の変更判断フラグをクリア
            .ManualKingakuFlg.Value = ""
            '金額が両方変更されたか判断フラグをクリア（計算済の場合は再計算不要）
            .BothKingakuChgFlg.Value = "1"

        End With
        Return teibetu_seikyuu_rec
    End Function

    ''' <summary>
    ''' 商品２画面表示用の商品情報を取得します
    ''' </summary>
    ''' <param name="syouhinCd">商品コード</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>邸別請求レコード</returns>
    ''' <remarks></remarks>
    Private Function getItem2Info(ByVal syouhinCd As String, ByVal strKameitenCd As String) As Syouhin23Record
        Dim item2Rec As Syouhin23Record = Nothing
        ' 情報取得用のロジッククラス
        Dim clsLogic As New JibanLogic
        Dim count As Integer = 0

        ' 商品情報を取得する（コード指定なので１件のみ取得される）
        Dim list As List(Of Syouhin23Record) = clsLogic.GetSyouhin23(syouhinCd _
                                                                    , "" _
                                                                    , EarthEnum.EnumSyouhinKubun.Syouhin2_110 _
                                                                    , count _
                                                                    , Integer.MinValue _
                                                                    , strKameitenCd)
        ' 取得できない場合
        If list.Count < 1 Then
            Return item2Rec
        End If

        ' 取得できた場合のみセット
        item2Rec = list(0)

        Return item2Rec

    End Function

    ''' <summary>
    ''' 指定商品行の値をクリアする(商品２)
    ''' </summary>
    ''' <param name="ctrlRow">処理対象コントロール行</param>
    ''' <remarks></remarks>
    Private Sub clearItem2Info(ByVal ctrlRow As IkkatuSyouhin2CtrlReord)
        With ctrlRow
            .Syouhin.SelectedValue = String.Empty
            .JituSeikyuuKingaku.Text = String.Empty
            .KoumutenKingaku.Text = String.Empty
            .SyoudakusyoKingaku.Text = String.Empty
            .SeikyuuUmu.SelectedValue = "1"
        End With
    End Sub

    ''' <summary>
    ''' 商品２画面表示用の邸別請求データを取得します
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="item2Rec">商品２情報取得用のパラメータクラス</param>
    ''' <param name="ctrlRow">処理対象行</param>
    ''' <returns>邸別請求レコード</returns>
    ''' <remarks></remarks>
    Private Function getItem2SeikyuuInfo(ByRef sender As Object, _
                                             ByVal item2Rec As Syouhin23InfoRecord, _
                                             ByVal ctrlRow As IkkatuSyouhin2CtrlReord _
                                             ) As TeibetuSeikyuuRecord

        Dim teibetuRec As TeibetuSeikyuuRecord = Nothing

        ' 情報取得用のロジッククラス
        Dim logic As New JibanLogic

        ' 請求データの取得
        teibetuRec = logic.GetSyouhin23SeikyuuData(sender, item2Rec, 1)

        Return teibetuRec

    End Function

    ''' <summary>
    ''' 自動計算チェックオフ時の商品２金額計算
    ''' </summary>
    ''' <param name="ctrlRow">処理対象行</param>
    ''' <param name="syouhinRec">商品コード1の自動設定データレコード</param>
    ''' <remarks></remarks>
    Private Sub setItem2ManualCalc(ByVal ctrlRow As IkkatuSyouhin2CtrlReord, ByVal syouhinRec As TeibetuSeikyuuRecord)

        With ctrlRow
            If .AutoCalc.Checked = False And syouhinRec IsNot Nothing Then
                If .KoumutenKingaku.ReadOnly = True Then
                    If .SeikyuuUmu.SelectedValue = "1" Then
                        .KoumutenKingaku.Text = syouhinRec.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1)      ' 工務店請求額
                    Else
                        .KoumutenKingaku.Text = "0"   ' 工務店請求額
                    End If
                End If
                If .JituSeikyuuKingaku.ReadOnly = True Then
                    If .SeikyuuUmu.SelectedValue = "1" Then
                        .JituSeikyuuKingaku.Text = syouhinRec.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1)              ' 実請求額
                    Else
                        .JituSeikyuuKingaku.Text = "0"       ' 実請求額
                    End If
                End If
            End If
        End With

    End Sub

    ''' <summary>
    ''' 画面表示値の連結文字列を取得(商品２)
    ''' </summary>
    ''' <param name="ctrlRow">処理対象行</param>
    ''' <returns>画面表示値の連結文字列</returns>
    ''' <remarks></remarks>
    Private Function getItem2InfoJoinString(ByVal ctrlRow As IkkatuSyouhin2CtrlReord) As String
        Dim clsLogic As New IkkatuHenkouTysSyouhinLogic
        Dim strItem2ChgValues() As String
        Dim dicItem2 As New Dictionary(Of String, String)
        Dim strItem2InfoJoinString As String
        Dim blnMakeBlank As Boolean = False

        If String.IsNullOrEmpty(ctrlRow.ChgValue.Value) Then
            If ctrlRow.SyouhinLine.Style("display") = "none" Then
                Return String.Empty
            End If
            blnMakeBlank = True
        End If
        '計算処理用Hiddenの値を取得
        strItem2ChgValues = clsLogic.getArrayFromDollarSep(ctrlRow.ChgValue.Value)

        '計算処理用Hiddenの値をDictionaryに登録
        dicItem2 = clsLogic.getDicItem2(strItem2ChgValues, blnMakeBlank)

        '自動設定後の商品２の各コントロール値をDictionaryに登録
        dicItem2 = setDicAfterCalcItem2(ctrlRow, dicItem2)

        '画面表示値の連結値を取得
        strItem2InfoJoinString = clsLogic.getJoinString(dicItem2.Values.GetEnumerator)

        Return strItem2InfoJoinString

    End Function

    ''' <summary>
    ''' 自動設定後の商品2の各コントロール値をDictionaryに登録
    ''' </summary>
    ''' <param name="ctrlRow">処理対象行</param>
    ''' <returns>各コントロール値が設定されたDictionary</returns>
    ''' <remarks></remarks>
    Private Function setDicAfterCalcItem2(ByVal ctrlRow As IkkatuSyouhin2CtrlReord, ByVal dic As Dictionary(Of String, String)) As Dictionary(Of String, String)
        With ctrlRow
            '地盤テーブル用データの取得
            dic("KameitenCd") = .KameitenCd.Value                       '加盟店コード ----------- (2)
            If .UriageSyori.Text = "" Then
                '邸別請求テーブル用データの取得(商品１)
                dic("UriKeijyouFlgItem1") = "0"                         '売上計上フラグ[0] ------ (3)
            Else
                dic("UriKeijyouFlgItem1") = "1"                         '売上計上フラグ[1] ------ (3)
            End If
            '邸別請求テーブル用データの取得(商品２)
            dic("Kbn") = .Kbn.Value                                     '区分 ------------------- (4)
            dic("HosyousyoNo") = .HosyousyoNo.Value                     '保証書NO --------------- (5)
            dic("BunruiCd") = .BunruiCd.Value                           '分類コード ------------- (6)
            dic("GamenHyoujiNo") = .GamenHyoujiNo.Value                 '画面表示NO ------------- (7)
            dic("SyouhinCd") = .Syouhin.SelectedValue                   '商品コード ------------- (8)
            dic("ZeiKbn") = .ZeiKbn.Value                               '税区分 ----------------- (9)
            dic("HattyuusyoKakuteiFlg") = .HattyuusyoKakuteiFlg.Value   '発注書確定フラグ ------- (21)
            dic("SeikyuuUmu") = .SeikyuuUmu.SelectedValue               '請求有無 --------------- (12)
        End With

        Return dic
    End Function

    ''' <summary>
    ''' 画面表示金額の連結文字列を取得(商品２)
    ''' </summary>
    ''' <param name="ctrlRow">処理対象行</param>
    ''' <returns>画面金額の連結文字列</returns>
    ''' <remarks></remarks>
    Private Function getItem2KingakuString(ByVal ctrlRow As IkkatuSyouhin2CtrlReord) As String
        Dim clsLogic As New IkkatuHenkouTysSyouhinLogic
        Dim strItem2ChgValues() As String
        Dim dicItem2 As New Dictionary(Of String, String)
        Dim stritem2KingakuJoinString As String
        Dim blnMakeBlank As Boolean

        If String.IsNullOrEmpty(ctrlRow.ChgKingaku.Value) Then
            If ctrlRow.SyouhinLine.Style("display") = "none" Then
                Return String.Empty
            End If
            blnMakeBlank = True
        End If
        '変更確認用Hiddenの値を取得
        strItem2ChgValues = clsLogic.getArrayFromDollarSep(ctrlRow.ChgKingaku.Value)

        '変更確認用Hiddenの値をDictionaryに登録
        dicItem2 = clsLogic.getDicItem2Kingaku(strItem2ChgValues, blnMakeBlank)

        '自動設定後の商品１金額をDictionaryに格納
        dicItem2 = setDicAfterCalcItem1Kingaku(ctrlRow, dicItem2)

        '画面金額の連結値を取得
        stritem2KingakuJoinString = clsLogic.getJoinString(dicItem2.Values.GetEnumerator)

        Return stritem2KingakuJoinString

    End Function

    ''' <summary>
    ''' 自動設定後の商品２の各金額をDictionaryに登録
    ''' </summary>
    ''' <param name="ctrlRow">処理対象行</param>
    ''' <param name="dic">変更確認用Hidden値のDictionary</param>
    ''' <returns>各金額が設定されたDictionary</returns>
    ''' <remarks></remarks>
    Private Function setDicAfterCalcItem1Kingaku(ByVal ctrlRow As IkkatuSyouhin2CtrlReord, ByVal dic As Dictionary(Of String, String)) As Dictionary(Of String, String)
        '
        With ctrlRow
            dic("KoumutenSeikyuuGaku") = .KoumutenKingaku.Text
            dic("UriGaku") = .JituSeikyuuKingaku.Text
            dic("SiireGaku") = .SyoudakusyoKingaku.Text
        End With

        Return dic

    End Function

#End Region

#Region "商品１、２共通金額計算"

    ''' <summary>
    ''' 実請求金額設定処理（工務店請求金額変更時に呼ぶ）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strSeikyuuSaki">請求先</param>
    ''' <param name="intKeiretuFlg">系列フラグ</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strKoumutenGaku">工務店金額</param>
    ''' <param name="strZeinukiJituGaku">実請求金額</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <returns>実請求金額(フォーマット済)</returns>
    ''' <remarks></remarks>
    Private Function getJituGaku(ByRef sender As Object, _
                                    ByVal strSeikyuuSaki As String, _
                                    ByVal intKeiretuFlg As Integer, _
                                    ByVal strKeiretuCd As String, _
                                    ByVal strKoumutenGaku As String, _
                                    ByVal strZeinukiJituGaku As String, _
                                    ByVal strSyouhinCd As String) As String
        Dim intZeinukiJituGaku As Integer = 0

        '金額自動設定条件判断
        If strSeikyuuSaki = EarthConst.SEIKYU_TYOKUSETU Then
            '調査請求先が直接請求の場合、工務店請求額を実請求金額へセット
            strZeinukiJituGaku = strKoumutenGaku

        ElseIf intKeiretuFlg = 1 Then
            '他請求・３系列の場合
            Dim clsJibanLogic As New JibanLogic
            Dim intKoumutenGaku As Integer = 0

            If strKoumutenGaku = String.Empty Then
                strKoumutenGaku = "0"
            End If
            cl.SetDisplayString(strKoumutenGaku, intKoumutenGaku)

            If clsJibanLogic.GetSeikyuuGaku(sender, _
                                            5, _
                                            strKeiretuCd, _
                                            strSyouhinCd, _
                                            intKoumutenGaku, _
                                            intZeinukiJituGaku) Then
                '実請求金額の設定
                strZeinukiJituGaku = intZeinukiJituGaku.ToString
            End If
        End If

        '実請求金額のフォーマット設定
        If strZeinukiJituGaku = String.Empty Then
            strZeinukiJituGaku = "0"
        Else
            strZeinukiJituGaku = CInt(strZeinukiJituGaku).ToString(EarthConst.FORMAT_KINGAKU_1)
        End If

        Return strZeinukiJituGaku

    End Function

    ''' <summary>
    ''' 工務店請求金額設定処理（実請求金額変更時に呼ぶ）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strSeikyuuSaki">請求先</param>
    ''' <param name="intKeiretuFlg">系列フラグ</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strZeinukiJituGaku">実請求金額</param>
    ''' <param name="strKoumutenGaku">工務店金額</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="intItemType">商品種別(商品１,２,３のどれか)</param>
    ''' <returns>工務店請求金額(フォーマット済)</returns>
    ''' <remarks></remarks>
    Private Function getKoumuGaku(ByRef sender As Object, _
                                    ByVal strSeikyuuSaki As String, _
                                    ByVal intKeiretuFlg As Integer, _
                                    ByVal strKeiretuCd As String, _
                                    ByVal strZeinukiJituGaku As String, _
                                    ByVal strKoumutenGaku As String, _
                                    ByVal strSyouhinCd As String, _
                                    ByVal intItemType As Integer) As String
        Dim intKoumutenGaku As Integer = 0

        '金額自動設定条件判断
        If strSeikyuuSaki = EarthConst.SEIKYU_TYOKUSETU Then
            '調査請求先が直接請求の場合、実請求金額を工務店請求額へセット
            strKoumutenGaku = strZeinukiJituGaku

        ElseIf intKeiretuFlg = 1 Then
            '他請求・３系列の場合
            Dim clsJibanLogic As New JibanLogic
            Dim intZeinukiJituGaku As Integer = 0
            Dim param As Integer

            If strZeinukiJituGaku = String.Empty Then
                strZeinukiJituGaku = "0"
            End If

            cl.SetDisplayString(strZeinukiJituGaku, intZeinukiJituGaku)

            '金額取得モードの設定
            If intItemType = 1 Then
                '商品1の場合
                param = 6
            Else
                '商品1以外の場合
                param = 4
            End If

            If clsJibanLogic.GetSeikyuuGaku(sender, _
                                            param, _
                                            strKeiretuCd, _
                                            strSyouhinCd, _
                                            intZeinukiJituGaku, _
                                            intKoumutenGaku) Then
                '工務店請求金額の設定
                strKoumutenGaku = intKoumutenGaku.ToString
            End If
        End If

        '工務店請求金額のフォーマット設定
        If strKoumutenGaku = String.Empty Then
            strKoumutenGaku = "0"
        Else
            strKoumutenGaku = CInt(strKoumutenGaku).ToString(EarthConst.FORMAT_KINGAKU_1)
        End If

        Return strKoumutenGaku
    End Function

#End Region

#Region "チェック処理"
    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <param name="tblTarget">チェック対象テーブル</param>
    ''' <param name="arrFocusTargetCtrl">フォーカス対象リスト</param>
    ''' <param name="flgSyouhinKkkChk">商品チェック判断フラグ</param>
    ''' <returns>チェック結果(OK：True ／ NG：False)</returns>
    ''' <remarks>
    ''' コード入力値変更チェック<br/>
    ''' 必須チェック<br/>
    ''' 禁則文字チェック(文字列入力フィールドが対象)<br/>
    ''' バイト数チェック(文字列入力フィールドが対象)<br/>
    ''' 桁数チェック<br/>
    ''' 日付チェック<br/>
    ''' その他チェック<br/>
    ''' </remarks>
    Private Function checkInput(ByRef tblTarget As HtmlGenericControl, _
                                ByRef arrFocusTargetCtrl As List(Of Control), _
                                Optional ByVal flgSyouhinKkkChk As Boolean = False) As String
        Dim e As New System.EventArgs

        '地盤画面共通クラス
        Dim jBn As New Jiban

        '全てのコントロールを有効化(破棄種別チェック用)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        'エラーメッセージ初期化
        Dim errMess As String = ""

        '変更対象一覧のセット数を取得（tblMeisaiの先頭にはLiteralControlが含まれる為、マイナス１）
        Dim intRowCnt As Integer = tblTarget.Controls.Count - 1
        Dim intCnt As Integer = 0
        Dim ctrlBukkenInfoRec1 As IkkatuHenkouTysSyouhin1RecordCtrl
        Dim ctrlBukkenInfoRec2 As IkkatuHenkouTysSyouhin2RecordCtrl

        For intCnt = 0 To intRowCnt - 1
            If tblTarget.ID = Me.tblMeisaiSyouhin1.ID Then
                ctrlBukkenInfoRec1 = tblTarget.Controls(intCnt + 1)
                ctrlBukkenInfoRec1.checkInput(errMess, arrFocusTargetCtrl, flgSyouhinKkkChk)
            ElseIf tblTarget.ID = Me.tblMeisaiSyouhin2.ID Then
                ctrlBukkenInfoRec2 = tblTarget.Controls(intCnt + 1)
                ctrlBukkenInfoRec2.checkInput(errMess, arrFocusTargetCtrl)
            End If
        Next

        Return errMess

    End Function

    ''' <summary>
    ''' チェックボックスのチェック判断
    ''' </summary>
    ''' <param name="intCnt">ユーザーコントロールの行数</param>
    ''' <param name="arrFocusTargetCtrl">フォーカス対象リスト</param>
    ''' <returns>チェック結果(OK：True ／ NG：False)</returns>
    ''' <remarks></remarks>
    Private Function checkChkBox(ByVal intCnt As Integer, ByRef arrFocusTargetCtrl As List(Of Control)) As String
        'エラーメッセージ初期化
        Dim errTarget As String = ""
        Dim ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl = pListItem1Ctrl.Item(intCnt)
        Dim ctrlItem2Rec As IkkatuHenkouTysSyouhin2RecordCtrl = pListItem2Ctrl.Item(intCnt)

        '商品１のチェック
        With ctrlItem1Rec
            If .AccCheckAutoCalc.Checked = True Then
                'チェック有りエラー
                errTarget &= EarthConst.BRANK_STRING & EarthConst.KOKYAKU_BANGOU & .AccTextKokyakuBangou.Text & EarthConst.BRANK_STRING & "商品1\r\n"
                arrFocusTargetCtrl.Add(.AccCheckAutoCalc)
            End If
        End With

        '商品２のチェック
        With ctrlItem2Rec
            For intCntJ As Integer = 1 To 4
                Dim strLine As String = "2_" & intCntJ
                Dim ctrlRow As IkkatuSyouhin2CtrlReord = .getItem2RowInfo(strLine)

                '地盤データを商品２にセット
                setJibanDataToItem2(ctrlRow, ctrlItem1Rec)

                '行が表示状態かつチェックが入っている場合
                If ctrlRow.SyouhinLine.Style("display") <> "none" And ctrlRow.AutoCalc.Checked = True Then
                    'チェック有りエラー
                    errTarget &= EarthConst.BRANK_STRING & EarthConst.KOKYAKU_BANGOU & ctrlRow.KokyakuBangou.Text & EarthConst.BRANK_STRING & "商品" & strLine & "\r\n"
                    arrFocusTargetCtrl.Add(ctrlRow.AutoCalc)
                End If

            Next
        End With

        Return errTarget
    End Function

    ''' <summary>
    ''' 計算処理チェック
    ''' </summary>
    ''' <param name="intCnt">ユーザーコントロールの行数</param>
    ''' <param name="arrFocusTargetCtrl">フォーカス対象リスト</param>
    ''' <returns>チェック結果(OK：True ／ NG：False)</returns>
    ''' <remarks></remarks>
    Private Function checkCalc(ByVal intCnt As Integer, ByRef arrFocusTargetCtrl As List(Of Control)) As String
        Dim strChkString As String
        'エラーメッセージ初期化
        Dim errTarget As String = ""
        Dim ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl = pListItem1Ctrl.Item(intCnt)
        Dim ctrlItem2Rec As IkkatuHenkouTysSyouhin2RecordCtrl = pListItem2Ctrl.Item(intCnt)

        '商品１のチェック
        With ctrlItem1Rec
            '画面表示値の連結文字列を取得(商品１)
            strChkString = getItem1InfoJoinString(ctrlItem1Rec)
            '画面金額の連結文字列を取得(商品１)
            .AccHiddenChgKingaku.Value = getItem1KingakuString(ctrlItem1Rec)

            If .AccHiddenChgValue.Value <> strChkString Then
                '計算未処理エラー
                errTarget &= EarthConst.BRANK_STRING & EarthConst.KOKYAKU_BANGOU & .AccTextKokyakuBangou.Text & EarthConst.BRANK_STRING & "商品1\r\n"
                arrFocusTargetCtrl.Add(.AccSelectTysHouhou)
            End If
        End With

        '商品２のチェック
        With ctrlItem2Rec
            For intCntJ As Integer = 1 To 4
                Dim strLine As String = "2_" & intCntJ
                Dim ctrlRow As IkkatuSyouhin2CtrlReord = .getItem2RowInfo(strLine)

                '地盤データを商品２にセット
                setJibanDataToItem2(ctrlRow, ctrlItem1Rec)

                '画面表示値の連結文字列を取得(商品２)
                strChkString = getItem2InfoJoinString(ctrlRow)
                '画面金額の連結文字列を取得(商品２)
                ctrlRow.ChgKingaku.Value = getItem2KingakuString(ctrlRow)

                '行が表示状態かつ計算処理が行われていない場合
                If ctrlRow.SyouhinLine.Style("display") <> "none" And ctrlRow.ChgValue.Value <> strChkString Then
                    '計算未処理エラー
                    errTarget &= EarthConst.BRANK_STRING & EarthConst.KOKYAKU_BANGOU & ctrlRow.KokyakuBangou.Text & EarthConst.BRANK_STRING & "商品" & strLine & "\r\n"
                    arrFocusTargetCtrl.Add(ctrlRow.Syouhin)
                End If

            Next
        End With

        Return errTarget
    End Function

    ''' <summary>
    ''' 金額の整合性チェック
    ''' </summary>
    ''' <param name="intCnt">ユーザーコントロールの行数</param>
    ''' <param name="arrFocusTargetCtrl">フォーカス対象リスト</param>
    ''' <returns>エラーメッセージ</returns>
    ''' <remarks>直接請求の場合、工務店請求額と実請求額は等しくなければいけない</remarks>
    Private Function checkKingaku(ByVal intCnt As Integer, ByRef arrFocusTargetCtrl As List(Of Control)) As String
        'エラーメッセージ初期化
        Dim errTarget As String = ""
        Dim ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl = pListItem1Ctrl.Item(intCnt)
        Dim ctrlItem2Rec As IkkatuHenkouTysSyouhin2RecordCtrl = pListItem2Ctrl.Item(intCnt)

        'エラーメッセージ初期化
        Dim errMess As String = ""

        '商品１のチェック
        With ctrlItem1Rec
            errMess &= .checkKingaku(arrFocusTargetCtrl)
        End With

        '商品２のチェック
        With ctrlItem2Rec
            errMess &= .checkKingaku(arrFocusTargetCtrl)
        End With

        Return errMess
    End Function
#End Region

#Region "ユーティリティ"
    ''' <summary>
    ''' 当画面を閉じるスクリプトを生成する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub closeWindow()
        Dim tmpScript As String = "window.close();" '画面を閉じる
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", tmpScript, True)
    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()
        Dim jBn As New Jiban '地盤画面クラス

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' プルダウン/コード入力連携設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '************
        '* 画面上部
        '************
        '調査方法
        jBn.SetPullCdScriptSrc(Me.TextTysHouhouCode, Me.SelectTysHouhou)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript関連セット
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim onFocusScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurScript As String = "checkNumberAddFig(this);"
        Dim checkDate As String = "checkDate(this);"
        Dim checkKingaku As String = "if(checkTempValueForOnBlur(this)){if(checkKingaku(this)){@CHECK_PLUS_SCRIPT@}}else{checkKingaku(this);}"
        Dim checkNumber As String = "checkNumber(this);"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"
        Dim onChangeItem2 As String = "clearKingakuText();"
        '金額プラス値不許可チェック
        Dim checkPlusKingaku As String = "if(checkPlusTaisyou()){checkPlusKingaku(" & Me.HiddenBunruiCd.ClientID & ", this);};"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ***コードおよび検索ポップアップボタン
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '調査会社
        Dim scriptSb As New StringBuilder()

        'Me.TextTysGaisyaCode.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callTyousakaisyaSearch(this);}else{checkNumber(this);}"
        scriptSb.Append("if(checkTempValueForOnBlur(this)){")
        scriptSb.Append("if(objEBI('" & Me.TextTysGaisyaCode.ClientID & "').value == ''){")
        scriptSb.Append("objEBI('" & Me.TextTysGaisyaName.ClientID & "').value = '';")
        scriptSb.Append("objEBI('" & Me.HiddenTyousaKaishaCdOld.ClientID & "').value = '';}")
        scriptSb.Append("objEBI('" & Me.TextTysGaisyaCode.ClientID & "').style.color = 'black';")
        scriptSb.Append("objEBI('" & Me.TextTysGaisyaName.ClientID & "').style.color = 'black';")
        scriptSb.Append("}else{checkNumber(this);}")

        Me.TextTysGaisyaCode.Attributes("onblur") = scriptSb.ToString
        Me.TextTysGaisyaCode.Attributes("onfocus") = "setTempValueForOnBlur(this);"

        scriptSb = New StringBuilder
        scriptSb.Append("objEBI('" & Me.TextTysGaisyaCode.ClientID & "').style.color = 'black';")
        scriptSb.Append("objEBI('" & Me.TextTysGaisyaName.ClientID & "').style.color = 'black';")
        scriptSb.Append("if(objEBI('" & Me.TextTysGaisyaCode.ClientID & "').value == '' || gintCallPopUp == 1){")
        scriptSb.Append("          gintCallPopUp = 0;")
        scriptSb.Append("          callSearch('" & Me.TextTysGaisyaCode.ClientID & "','" & UrlConst.SEARCH_TYOUSAKAISYA & "','" & _
                                                                Me.TextTysGaisyaCode.ClientID & EarthConst.SEP_STRING & _
                                                                Me.TextTysGaisyaName.ClientID & _
                                                                "','" & ButtonTysGaisya.ClientID & "');")
        scriptSb.Append("}else{")
        scriptSb.Append("getTysKaisyaName(this" & ", " & _
                                            "objEBI('" & Me.TextTysGaisyaCode.ClientID & "').value" & ", " & _
                                            "objEBI('" & Me.TextTysGaisyaName.ClientID & "')" & ", " & _
                                            "objEBI('" & Me.HiddenTyousaKaishaCdOld.ClientID & "'));")
        scriptSb.Append("}")

        Me.ButtonTysGaisya.Attributes("onclick") = scriptSb.ToString

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 金額系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '工務店請求金額
        Me.TextKoumutenSeikyuuGaku.Attributes("onfocus") = onFocusScript
        Me.TextKoumutenSeikyuuGaku.Attributes("onblur") = checkKingaku.Replace("@CHECK_PLUS_SCRIPT@", checkPlusKingaku)
        Me.TextKoumutenSeikyuuGaku.Attributes("onkeydown") = disabledOnkeydown
        '実請求金額
        Me.TextJituSeikyuuKinGaku.Attributes("onfocus") = onFocusScript
        Me.TextJituSeikyuuKinGaku.Attributes("onblur") = checkKingaku.Replace("@CHECK_PLUS_SCRIPT@", checkPlusKingaku)
        Me.TextJituSeikyuuKinGaku.Attributes("onkeydown") = disabledOnkeydown
        '承諾書金額
        Me.TextSyoudakusyoKingaku.Attributes("onfocus") = onFocusScript
        Me.TextSyoudakusyoKingaku.Attributes("onblur") = checkKingaku.Replace("@CHECK_PLUS_SCRIPT@", checkPlusKingaku)
        Me.TextSyoudakusyoKingaku.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 日付系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'なし

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ドロップダウンリスト
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '商品2
        Me.SelectSyouhin2.Attributes("onchange") = onChangeItem2

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 数値系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '調査方法
        Me.TextTysHouhouCode.Attributes("onkeydown") = disabledOnkeydown

    End Sub

    ''' <summary>
    ''' Ajax動作時のフォーカスセット
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub setFocusAJ(ByVal ctrl As Control)
        'フォーカスセット
        masterAjaxSM.SetFocus(ctrl)
    End Sub

    ''' <summary>
    ''' 登録/修正実行ボタンの設定
    ''' </summary>
    ''' <remarks>
    ''' DB更新の確認を行なう。<br/>
    ''' OK時：DB更新を行なう。<br/>
    ''' キャンセル時：DB更新を中断する。<br/>
    ''' </remarks>
    Private Sub setBtnEvent()

        'イベントハンドラ登録
        Dim tmpScript As String = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this,null,1);}else{return false;}"

        '登録許可MSG確認後、OKの場合DB更新処理を行なう
        Me.ButtonIkkatuHenkou.Attributes("onclick") = tmpScript

    End Sub

    ''' <summary>
    ''' エラーメッセージダイアログ表示処理
    ''' </summary>
    ''' <param name="strMsg">表示メッセージ</param>
    ''' <param name="strAlertName">スクリプト ブロックの一意の識別子</param>
    ''' <param name="arrFocusTargetCtrl">フォーカスをセットするコントロール群</param>
    ''' <remarks></remarks>
    Private Sub showErrDialog(ByVal strMsg As String, ByVal strAlertName As String, ByVal arrFocusTargetCtrl As List(Of Control))
        'フォーカスセット
        If arrFocusTargetCtrl.Count > 0 Then
            arrFocusTargetCtrl(0).Focus()
        End If
        'エラーメッセージ表示
        Dim tmpScript As String = "alert('" & strMsg & "');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), strAlertName, tmpScript, True)
        Exit Sub
    End Sub

    ''' <summary>
    ''' NG調査会社設定警告イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>NG調査会社設定警告ラベルを表示する</remarks>
    Private Sub WarningTyskaisya(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.LabelNgTysKaisya.Visible = True
        Me.LabelNgTysKaisya.Text = Messages.MSG131W

    End Sub

    ''' <summary>
    ''' 明細行を作成します
    ''' </summary>
    ''' <param name="intRowCnt">作成する行数</param>
    ''' <remarks></remarks>
    Private Sub createRow(ByVal intRowCnt As Integer)
        Dim ctrlSyouhin1InfoRec As IkkatuHenkouTysSyouhin1RecordCtrl
        Dim ctrlSyouhin2InfoRec As IkkatuHenkouTysSyouhin2RecordCtrl

        For intCnt As Integer = 1 To intRowCnt

            '商品1
            ctrlSyouhin1InfoRec = createRowItem1(intCnt, True)
            '商品2
            ctrlSyouhin2InfoRec = createRowitem2(intCnt)

        Next
    End Sub

    ''' <summary>
    ''' 商品1の明細行を1行作成します
    ''' </summary>
    ''' <param name="intRowNo">行数</param>
    ''' <param name="blnWarn">NG調査会社警告イベントの実装有無判断フラグ</param>
    ''' <returns>商品1の明細行のユーザーコントロール</returns>
    ''' <remarks></remarks>
    Private Function createRowItem1(ByVal intRowNo As Integer, Optional ByVal blnWarn As Boolean = False) As IkkatuHenkouTysSyouhin1RecordCtrl
        Dim ctrlSyouhin1InfoRec As IkkatuHenkouTysSyouhin1RecordCtrl

        With tblMeisaiSyouhin1.Controls

            ctrlSyouhin1InfoRec = Me.LoadControl("control/IkkatuHenkouTysSyouhin1RecordCtrl.ascx")
            ctrlSyouhin1InfoRec.ID = TYS_SYOUHIN1_CTRL_NAME & intRowNo.ToString

            If blnWarn Then
                'NG調査会社警告イベントの実装
                AddHandler ctrlSyouhin1InfoRec.WarningTysKaisya, AddressOf Me.WarningTyskaisya
            End If

            If (intRowNo Mod 2) = 0 Then
                ctrlSyouhin1InfoRec.AccTrTysSyouhin_1_1.Attributes("class") = "even"
                ctrlSyouhin1InfoRec.AccTrTysSyouhin_1_2.Attributes("class") = "even"
            Else
                ctrlSyouhin1InfoRec.AccTrTysSyouhin_1_1.Attributes("class") = "odd"
                ctrlSyouhin1InfoRec.AccTrTysSyouhin_1_2.Attributes("class") = "odd"
            End If

            .Add(ctrlSyouhin1InfoRec)

            pListItem1Ctrl.Add(ctrlSyouhin1InfoRec)

        End With

        Return ctrlSyouhin1InfoRec
    End Function

    ''' <summary>
    ''' 商品2の明細行を1行作成します
    ''' </summary>
    ''' <param name="intRowNo">行数</param>
    ''' <returns>商品2の明細行のユーザーコントロール</returns>
    ''' <remarks></remarks>
    Private Function createRowitem2(ByVal intRowNo) As IkkatuHenkouTysSyouhin2RecordCtrl
        Dim ctrlSyouhin2InfoRec As IkkatuHenkouTysSyouhin2RecordCtrl

        With tblMeisaiSyouhin2.Controls

            ctrlSyouhin2InfoRec = Me.LoadControl("control/IkkatuHenkouTysSyouhin2RecordCtrl.ascx")
            ctrlSyouhin2InfoRec.ID = TYS_SYOUHIN2_CTRL_NAME & intRowNo.ToString

            .Add(ctrlSyouhin2InfoRec)

            pListItem2Ctrl.Add(ctrlSyouhin2InfoRec)

        End With

        Return ctrlSyouhin2InfoRec

    End Function
#End Region

#Region "DBアクセス関連"
    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="sender">地盤レコード</param>
    ''' <param name="e">地盤レコード</param>
    ''' <remarks></remarks>
    Private Sub setCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim jSM As New JibanSessionManager 'セッション管理クラス
        Dim jBn As New Jiban '地盤画面クラス

        Dim arrKbn() As String = Split(_kbn, EarthConst.SEP_STRING)
        Dim arrNo() As String = Split(_no, EarthConst.SEP_STRING)

        '区分と番号のパラメータ引渡し数が異なる場合
        If arrKbn.Length <> arrNo.Length Then
            closeWindow()
            Exit Sub
        End If

        Dim logic As New JibanLogic
        Dim jibanRec As New JibanRecordBase
        Dim kameitenRec As New KameitenSearchRecord
        Dim teibetuRecItem1 As TeibetuSeikyuuRecord
        Dim teibetuRecItem2 As Dictionary(Of Integer, TeibetuSeikyuuRecord)

        Dim kameitenSearchLogic As New KameitenSearchLogic

        Dim intCnt As Integer  'カウンタ
        Dim intDBCnt As Integer = 1 'DBカウント
        Dim intHissuChk As Integer = 0 '必須入力チェック
        Dim intUriageCnt As Integer = 0 '売上処理済の件数
        Dim intTorikeshi As Integer = 0 '取消加盟店の件数
        Dim blnSyoudakuHenkouFlg As Boolean '承諾書金額変更可否フラグ
        Dim hin1InfoRec As New Syouhin1InfoRecord   '商品1検索用のレコード
        Dim hin1AutoSetRecord As New Syouhin1AutoSetRecord      '商品1自動設定用のレコード
        Dim lgcJiban As New JibanLogic

        Dim ctrlSyouhin1InfoRec As IkkatuHenkouTysSyouhin1RecordCtrl
        Dim ctrlSyouhin2InfoRec As IkkatuHenkouTysSyouhin2RecordCtrl

        For intCnt = 0 To arrKbn.Length - 1

            '地盤データの取得
            jibanRec = logic.GetJibanData(arrKbn(intCnt), arrNo(intCnt), True)
            '邸別請求データの取得
            teibetuRecItem1 = jibanRec.Syouhin1Record
            teibetuRecItem2 = jibanRec.Syouhin2Records

            If Not jibanRec Is Nothing AndAlso jibanRec.Kbn IsNot Nothing Then

                '必須項目(加盟店コード)が未入力の場合
                If jibanRec.KameitenCd Is Nothing Then
                    intHissuChk = 1
                    Exit For
                End If

                '加盟店データの取得
                kameitenRec = kameitenSearchLogic.GetKameitenSearchResult(jibanRec.Kbn, _
                                                                          jibanRec.KameitenCd, _
                                                                          jibanRec.TysKaisyaCd & jibanRec.TysKaisyaJigyousyoCd, _
                                                                          False)

                '●原価マスタより承諾書金額変更FLGをを取得
                If logic.GetSyoudakusyoKingaku1(teibetuRecItem1.SyouhinCd, _
                                                jibanRec.Kbn, _
                                                jibanRec.TysHouhou, _
                                                jibanRec.TysGaiyou, _
                                                jibanRec.DoujiIraiTousuu, _
                                                jibanRec.TysKaisyaCd & jibanRec.TysKaisyaJigyousyoCd, _
                                                jibanRec.KameitenCd, _
                                                0, _
                                                kameitenRec.KeiretuCd, _
                                                blnSyoudakuHenkouFlg) Then
                    teibetuRecItem1.SyoudakuHenkouKahi = blnSyoudakuHenkouFlg
                Else
                    teibetuRecItem1.SyoudakuHenkouKahi = False
                End If

                '●販売価格マスタより工務店・実請求変更FLGを取得
                '検索KEYの設定
                hin1InfoRec.TysKaisyaCd = jibanRec.TysKaisyaCd & jibanRec.TysKaisyaJigyousyoCd      '調査会社コード+事業所コード
                hin1InfoRec.KameitenCd = jibanRec.KameitenCd                                        '加盟店コード
                hin1InfoRec.EigyousyoCd = kameitenRec.EigyousyoCd                                   '営業所コード
                hin1InfoRec.KeiretuCd = kameitenRec.KeiretuCd                                       '系列コード
                hin1InfoRec.TyousaHouhouNo = jibanRec.TysHouhou                                     '調査方法NO
                hin1InfoRec.SyouhinCd = teibetuRecItem1.SyouhinCd                                   '商品コード

                If lgcJiban.GetHanbaiKingaku1(hin1InfoRec, hin1AutoSetRecord) Then
                    '工務店請求・実請求の変更可否設定
                    teibetuRecItem1.KoumutenHenkouKahi = hin1AutoSetRecord.KoumutenGakuHenkouFlg
                    teibetuRecItem1.JituseikyuuHenkouKahi = hin1AutoSetRecord.JituGakuHenkouFlg
                Else
                    '取得出来ない場合、工務店請求・実請求金額変更不可
                    teibetuRecItem1.KoumutenHenkouKahi = False
                    teibetuRecItem1.JituseikyuuHenkouKahi = False
                End If

                '商品1のユーザーコントロールを作成
                ctrlSyouhin1InfoRec = createRowItem1(intDBCnt)

                '地盤データをユーザコントロール（上段）にセット
                ctrlSyouhin1InfoRec.setCtrlFromJibanRec(jibanRec, e, teibetuRecItem1)

                If ctrlSyouhin1InfoRec.AccTextUriageSyori.Text <> "" Then
                    intUriageCnt += 1
                End If
                If ctrlSyouhin1InfoRec.AccHiddenTorikeshi.Value = "1" Then
                    intTorikeshi += 1
                End If

                '商品2のユーザーコントロールを作成
                ctrlSyouhin2InfoRec = createRowitem2(intDBCnt)

                '地盤データをユーザコントロール（下段）にセット
                ctrlSyouhin2InfoRec.setCtrlFromJibanRec(jibanRec, teibetuRecItem1, teibetuRecItem2, Me.trHeadSyouhin2)

                '区分・番号に紐付く特別対応データを取得する
                Me.GetTokubetuTaiouCd(sender, ctrlSyouhin1InfoRec, ctrlSyouhin2InfoRec, jibanRec)

                intDBCnt += 1 'カウントアップ
            End If
        Next

        '該当データが一件もない場合
        If pListItem1Ctrl.Count = 0 Or intHissuChk <> 0 Then
            closeWindow()
            Exit Sub
        End If

        '該当データが全て売上処理済の場合、計算ボタン・一括変更ボタンを非活性化
        If pListItem1Ctrl.Count <= intUriageCnt + intTorikeshi Then
            Me.ButtonNaiyouChk.Disabled = True
            Me.ButtonIkkatuHenkou.Disabled = True
        End If

    End Sub

    ''' <summary>
    ''' 画面の内容をDBに反映する
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SaveData() As Boolean
        '*************************
        '地盤データは更新対象のみ
        '*************************

        Dim logic As New IkkatuHenkouTysSyouhinLogic

        '各行ごとに画面からレコードクラスに入れ込み
        Dim listJibanRec As List(Of JibanRecordIkkatuHenkouTysSyouhin) = Nothing
        Dim listBrRec As List(Of BukkenRirekiRecord) = Nothing
        Dim listTokuRec As List(Of TokubetuTaiouRecordBase) = Nothing

        listJibanRec = Me.GetRowCtrlToJibanRec(listBrRec, listTokuRec)
        If listJibanRec Is Nothing Then
            Return False
        End If

        ' データの更新を行います
        If logic.SaveJibanData(Me, listJibanRec, listBrRec, listTokuRec) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 画面の各明細行情報をレコードクラスに取得し、地盤レコードクラスのリストを返却する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetRowCtrlToJibanRec(ByRef listBrRec As List(Of BukkenRirekiRecord), ByRef listTokuRec As List(Of TokubetuTaiouRecordBase)) As List(Of JibanRecordIkkatuHenkouTysSyouhin)
        '*************************
        '地盤データは更新対象のみ
        '*************************
        Dim listJibanRec As New List(Of JibanRecordIkkatuHenkouTysSyouhin)
        listBrRec = New List(Of BukkenRirekiRecord)
        listTokuRec = New List(Of TokubetuTaiouRecordBase)
        Dim intTokubetuCnt As Integer = 0   '特別対応登録用リストのカウンタ

        '商品1情報の取得
        For intCntCtrl As Integer = 0 To pListItem1Ctrl.Count - 1
            Dim ctrlSyouhin1 As IkkatuHenkouTysSyouhin1RecordCtrl = pListItem1Ctrl(intCntCtrl)
            Dim ctrlSyouhin2 As IkkatuHenkouTysSyouhin2RecordCtrl = pListItem2Ctrl(intCntCtrl)
            Dim JibanLogic As New JibanLogic
            Dim jrOld As New JibanRecord
            ' 画面内容より地盤レコードを生成する
            Dim jibanRec As New JibanRecordIkkatuHenkouTysSyouhin
            Dim lgcTokutai As New TokubetuTaiouLogic
            Dim listRec As New List(Of TokubetuTaiouRecordBase)   'DB返却用特別対応レコードリスト

            '更新判断フラグ
            Dim blnUpdate As Boolean = False

            '商品１テーブルから取得
            With ctrlSyouhin1
                ' 現在の地盤データをDBから取得する
                jrOld = JibanLogic.GetJibanData(.AccHiddenKbn.Value, .AccHiddenHosyousyoNo.Value)

                '進捗T更新用に、DB上の値をセットする
                JibanLogic.SetSintyokuJibanData(jrOld, jibanRec)

                '***************************************
                ' 地盤データ
                '***************************************
                ' 調査会社等
                Dim tmpTys As String = .AccTextTyousakaisyaCode.Text
                If tmpTys.Length >= 6 Then   '長さ6以上必須
                    jibanRec.TysKaisyaCd = tmpTys.Substring(0, tmpTys.Length - 2) '調査会社コード
                    jibanRec.TysKaisyaJigyousyoCd = tmpTys.Substring(tmpTys.Length - 2, 2) '調査会社事業所コード
                End If
                ' 調査方法
                cl.SetDisplayString(.AccTextTysHouhouCode.Text, jibanRec.TysHouhou)

                '***************************************
                ' 画面入力項目以外
                '***************************************
                ' 区分
                jibanRec.Kbn = .AccHiddenKbn.Value
                ' 番号（保証書NO）
                jibanRec.HosyousyoNo = .AccHiddenHosyousyoNo.Value
                ' 更新者ユーザーID
                jibanRec.UpdLoginUserId = userinfo.LoginUserId
                ' 更新日時 読み込み時のタイムスタンプ
                If .AccHiddenUpdDatetimeJiban.Value = "" Then
                    jibanRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
                Else
                    jibanRec.UpdDatetime = DateTime.ParseExact(.AccHiddenUpdDatetimeJiban.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
                End If
                '更新者
                jibanRec.Kousinsya = cbLogic.GetKousinsya(userinfo.LoginUserId, DateTime.Now)
                ' 調査概要
                cl.SetDisplayString(.AccHiddenTysGaiyou.Value, jibanRec.TysGaiyou)

                '***************************************
                ' 邸別請求データ
                '***************************************
                '商品1
                jibanRec.Syouhin1Record = .setSyouhinToTeibetu

                '***************************************
                ' 特別対応データ
                '***************************************
                If ctrlSyouhin1.AccTokubetuTaiouToolTip.AccUpdDatetime.Value <> String.Empty AndAlso _
                    ctrlSyouhin1.AccTokubetuTaiouToolTip.AccTaisyouCd.Value <> String.Empty Then

                    '画面ツールチップから特別対応コードを取得
                    Dim strDisplayCd As String = ctrlSyouhin1.AccTokubetuTaiouToolTip.AccDisplayCd.Value
                    Dim intTokuTaiCnt As Integer = Integer.MinValue

                    '特別対応マスタベースの特別対応データをDBから取得
                    listRec = lgcTokutai.GetTokubetuTaiouDataInfo(Me, _
                                                                 jibanRec.Kbn, _
                                                                 jibanRec.HosyousyoNo, _
                                                                 strDisplayCd, _
                                                                 intTokuTaiCnt)

                    '取得できた場合のみリストセット
                    If intTokuTaiCnt > 0 Then
                        '複数の特別対応コードの場合があるので、切り分ける
                        Dim arrTaisyouCd() As String = Split(ctrlSyouhin1.AccTokubetuTaiouToolTip.AccTaisyouCd.Value, EarthConst.SEP_STRING)
                        Dim arrUpdDateTime() As String = Split(ctrlSyouhin1.AccTokubetuTaiouToolTip.AccUpdDatetime.Value, EarthConst.SEP_STRING)

                        'DB値に画面情報を上書きする
                        For i As Integer = 0 To arrTaisyouCd.Length - 1
                            'DB値リストからコードを検索
                            For j As Integer = 0 To listRec.Count - 1
                                'DBリストからヒットしたら画面情報を上書き
                                If arrTaisyouCd(i) = listRec(j).TokubetuTaiouCd Then
                                    '更新フラグ(ここのタイミングではすべて更新対象となるため)
                                    listRec(j).UpdFlg = 1
                                    '金額加算商品コード(=画面.邸別の商品コード)
                                    listRec(j).KasanSyouhinCd = jibanRec.Syouhin1Record.SyouhinCd
                                    '更新ログインユーザ
                                    listRec(j).UpdLoginUserId = userinfo.LoginUserId
                                    '更新日時 読み込み時のタイムスタンプ
                                    If arrUpdDateTime(i) = String.Empty Then
                                        listRec(j).UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
                                    Else
                                        listRec(j).UpdDatetime = DateTime.ParseExact(arrUpdDateTime(i), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                                    End If
                                End If
                            Next
                        Next
                    End If
                End If

                '***************************************
                ' 更新判断フラグの設定
                '***************************************
                '画面情報の変更チェック(金額以外)
                If .AccHiddenChgValue.Value <> .AccHiddenDbValue.Value Then
                    blnUpdate = True
                End If
                '画面情報の変更チェック(金額)
                If .AccHiddenChgKingaku.Value <> .AccHiddenDbKingaku.Value Then
                    blnUpdate = True
                End If

            End With

            '商品２テーブルから取得
            With ctrlSyouhin2
                '商品2情報の取得(1～4)
                jibanRec.Syouhin2Records = .setSyouhinToTeibetu(blnUpdate, jibanRec)

            End With

            '商品３ 保証商品有無を判別する為、DBのデータを取得
            jibanRec.Syouhin3Records = jrOld.Syouhin3Records

            '*********************************************************
            '●保証関連の自動設定
            cbLogic.ps_AutoSetHosyousyoHakJyjy(EarthEnum.emGamenInfo.IkkatuTysSyouhinInfo, jibanRec)

            '変更状況のチェック
            '保証書発行状況の更新チェック
            If cl.GetDispNum(jrOld.HosyousyoHakJyky, "") <> cl.GetDispNum(jibanRec.HosyousyoHakJyky, "") Then
                blnUpdate = True
            End If
            '保証書発行状況設定日の更新チェック
            If cl.GetDisplayString(jrOld.HosyousyoHakJykySetteiDate) <> cl.GetDisplayString(jibanRec.HosyousyoHakJykySetteiDate) Then
                blnUpdate = True
            End If
            '保証商品有無の更新チェック
            If cl.GetDispNum(jrOld.HosyouSyouhinUmu, "") <> cl.GetDispNum(jibanRec.HosyouSyouhinUmu, "") Then
                blnUpdate = True
            End If

            '●物件履歴データの自動セット
            Dim brRec As BukkenRirekiRecord = cbLogic.MakeBukkenRirekiRecHosyouUmu(jibanRec _
                                                                            , cl.GetDisplayString(jrOld.HosyouSyouhinUmu) _
                                                                            , cl.GetDisplayString(jrOld.KeikakusyoSakuseiDate))
            If Not brRec Is Nothing Then
                '物件履歴レコードのチェック
                Dim strErrMsg As String = String.Empty
                If cbLogic.checkInputBukkenRireki(brRec, strErrMsg) = False Then
                    MLogic.AlertMessage(Me, strErrMsg, 0, "ErrBukkenRireki")
                    Return Nothing
                    Exit Function
                End If
            End If
            '********************************

            '編集された物件のみ更新する
            If blnUpdate Then
                'リストに追加
                listJibanRec.Add(jibanRec)
                listBrRec.Add(brRec)
                '特別対応リストに追加
                If listRec.Count > 0 Then
                    For k As Integer = 0 To listRec.Count - 1
                        listTokuRec.Add(listRec(k))
                    Next
                End If
            End If
        Next

        Return listJibanRec
    End Function



#End Region

#Region "特別対応"
    ''' <summary>
    ''' 特別対応データを取得する(ツールチップ表示用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetTokubetuTaiouCd(ByVal sender As Object, _
                                   ByRef ctrlRec1 As IkkatuHenkouTysSyouhin1RecordCtrl, _
                                   ByRef ctrlRec2 As IkkatuHenkouTysSyouhin2RecordCtrl, _
                                   ByVal jibanRec As JibanRecordBase)

        Dim ttList As New List(Of TokubetuTaiouRecordBase)

        '区分、保証書NOをキーに特別対応データを取得
        ttList = tLogic.GetTokubetuTaiouDataInfo(sender, jibanRec.Kbn, jibanRec.HosyousyoNo, String.Empty, 0)

        '振分け処理
        Me.DevideTokubetuTaiouCd(sender, ttList, ctrlRec1, ctrlRec2)

    End Sub

    ''' <summary>
    ''' 特別対応データのリストを画面の各ツールチップに振り分ける
    ''' </summary>
    ''' <param name="ttList">特別対応レコードのリスト</param>
    ''' <remarks></remarks>
    Private Sub DevideTokubetuTaiouCd(ByVal sender As Object, _
                                      ByVal ttList As List(Of TokubetuTaiouRecordBase), _
                                      ByRef ctrlRec1 As IkkatuHenkouTysSyouhin1RecordCtrl, _
                                      ByRef ctrlRec2 As IkkatuHenkouTysSyouhin2RecordCtrl)

        Dim recTmp As New TokubetuTaiouRecordBase       '作業用
        Dim strTokubetuTaiouCd As String = String.Empty '特別対応コード
        Dim strResult As String                         '振分先

        If Not ttList Is Nothing Then
            For Each recTmp In ttList
                '特別対応コードを作業用に取得
                strTokubetuTaiouCd = cl.GetDisplayString(recTmp.TokubetuTaiouCd)

                '振分け先を取得
                strResult = cbLogic.checkDevideTaisyou(sender, recTmp)

                '振分け先のHiddenに追加
                If strResult <> String.Empty Then
                    Dim search_shouhin As String = strResult.Split(EarthConst.UNDER_SCORE)(0)   '商品判定用

                    If search_shouhin = "1" Then
                        'ツールチップ設定対象かチェック
                        If cbLogic.checkToolTipSetValue(sender, recTmp, ctrlRec1.AccHiddenBunruiCd.Value, ctrlRec1.AccHiddenGamenHyoujiNo.Value) <> EarthEnum.emToolTipType.NASI Then

                            ctrlRec1.AccTokubetuTaiouToolTip.SetDisplayCd(strTokubetuTaiouCd)

                            '更新日時をセット
                            If ctrlRec1.AccTokubetuTaiouToolTip.AccUpdDatetime.Value = String.Empty Then
                                ctrlRec1.AccTokubetuTaiouToolTip.AccUpdDatetime.Value = IIf(recTmp.UpdDatetime = Date.MinValue, "", Format(recTmp.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
                            Else
                                ctrlRec1.AccTokubetuTaiouToolTip.AccUpdDatetime.Value &= EarthConst.SEP_STRING & IIf(recTmp.UpdDatetime = Date.MinValue, "", Format(recTmp.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
                            End If
                        End If
                    ElseIf search_shouhin = "2" Then
                        ctrlRec2.SetTokubetuTaiouToolTip(sender, strTokubetuTaiouCd, strResult, recTmp)
                    End If
                End If
            Next
        End If

    End Sub

    ''' <summary>
    ''' 特別対応データのリストを画面の各ツールチップに振り分ける
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="ctrlItem1Rec">商品1コントロール</param>
    ''' <param name="listRec">特別対応リスト</param>
    ''' <remarks></remarks>
    Private Sub DevideTokubetuTaiouCd(ByVal sender As Object, ByVal ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl, ByVal listRec As List(Of TokubetuTaiouRecordBase))

        Dim recTmp As New TokubetuTaiouRecordBase
        Dim strTokubetuTaiouCd As String = String.Empty

        'ツールチップを初期化
        ctrlItem1Rec.AccTokubetuTaiouToolTip.AccDisplayCd.Value = String.Empty

        If Not listRec Is Nothing Then
            For Each recTmp In listRec
                '特別対応コードを作業用に取得
                strTokubetuTaiouCd = cl.GetDisplayString(recTmp.TokubetuTaiouCd)
                'ツールチップHiddenに特別対応コードを格納
                If ctrlItem1Rec.AccSelectSyouhin1.SelectedValue <> String.Empty Then
                    ctrlItem1Rec.AccTokubetuTaiouToolTip.SetDisplayCd(strTokubetuTaiouCd)
                End If
            Next
        End If

    End Sub

    ''' <summary>
    ''' 特別対応データのリスト（変更情報）を各ツールチップのHiddenに振り分ける
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="ctrlItem1Rec"></param>
    ''' <param name="listRec"></param>
    ''' <remarks></remarks>
    Private Sub DevideTokubetuTaiouHidden(ByVal sender As Object, ByVal ctrlItem1Rec As IkkatuHenkouTysSyouhin1RecordCtrl, ByVal listRec As List(Of TokubetuTaiouRecordBase))

        Dim recTmp As New TokubetuTaiouRecordBase

        '（初期化）
        '更新日時
        ctrlItem1Rec.AccTokubetuTaiouToolTip.AccUpdDatetime.Value = String.Empty
        '変更対象
        ctrlItem1Rec.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty

        If Not listRec Is Nothing Then
            '特別対応リスト分処理を繰り返す
            For Each recTmp In listRec
                If recTmp.UpdFlg <> "1" Then
                    '変更対象ではない場合は、処理を飛ばす
                    Continue For
                End If

                '更新日時
                If ctrlItem1Rec.AccTokubetuTaiouToolTip.AccUpdDatetime.Value = String.Empty Then
                    ctrlItem1Rec.AccTokubetuTaiouToolTip.AccUpdDatetime.Value = IIf(recTmp.UpdDatetime = Date.MinValue, "", Format(recTmp.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
                Else
                    ctrlItem1Rec.AccTokubetuTaiouToolTip.AccUpdDatetime.Value &= EarthConst.SEP_STRING & IIf(recTmp.UpdDatetime = Date.MinValue, "", Format(recTmp.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
                End If
                '変更対象
                If ctrlItem1Rec.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty Then
                    ctrlItem1Rec.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = cl.GetDisplayString(recTmp.TokubetuTaiouCd)
                Else
                    ctrlItem1Rec.AccTokubetuTaiouToolTip.AccTaisyouCd.Value &= EarthConst.SEP_STRING & cl.GetDisplayString(recTmp.TokubetuTaiouCd)
                End If
            Next
        End If

    End Sub

#End Region

End Class