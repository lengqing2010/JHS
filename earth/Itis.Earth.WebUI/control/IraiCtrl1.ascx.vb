
Partial Public Class IraiCtrl1
    Inherits System.Web.UI.UserControl

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private MyLogic As New JibanLogic

#Region "メンバ変数"
    ''' <summary>
    ''' 依頼画面セッション情報クラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim iraiSession As New IraiSession
    ''' <summary>
    ''' ユーザー情報
    ''' </summary>
    ''' <remarks></remarks>
    Dim user_info As New LoginUserInfo
    ''' <summary>
    ''' マスターページのAjaxScriptManager
    ''' </summary>
    ''' <remarks></remarks>
    Dim masterAjaxSM As New ScriptManager
    ''' <summary>
    ''' 地盤画面セッション管理クラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim jSM As New JibanSessionManager
    ''' <summary>
    ''' 地盤画面共通クラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim jBn As New Jiban
    ''' <summary>
    ''' 共通処理クラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim cl As New CommonLogic
    ''' <summary>
    ''' メッセージクラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic
    ''' <summary>
    ''' 採番処理完了フラグ（新規引継、新規連続用）
    ''' </summary>
    ''' <remarks></remarks>
    Dim saibanOkFlg As Boolean = False
    Dim sLogic As New StringLogic

#End Region

#Region "カスタムイベント宣言"
    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OyaGamenAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal irai1Mode As String, ByVal actMode As String)

    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OyaGamenAction_hensyu(ByVal sender As System.Object, ByVal e As System.EventArgs, ByRef flg As Boolean)

    ''' <summary>
    ''' 登録完了時の親画面の処理実行用カスタムイベント
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OyaActAtAfterExe(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal irai1Mode As String, ByVal actMode As String, ByVal exeMode As String, ByVal result As Boolean)
#End Region

#Region "ユーザーコントロールへの外部からのアクセス用Getter/Setter"

    ''' <summary>
    ''' 外部からのアクセス用 for btn_irai1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccBtn_Irai1() As HtmlInputButton
        Get
            Return btn_irai1
        End Get
        Set(ByVal value As HtmlInputButton)
            btn_irai1 = value
        End Set
    End Property


    ''' <summary>
    ''' 外部からのアクセス用 for cboKubun
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccCbokubun() As DropDownList
        Get
            Return cboKubun
        End Get
        Set(ByVal value As DropDownList)
            cboKubun = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for hoshouNo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHoshouno() As HtmlInputText
        Get
            Return hoshouNo
        End Get
        Set(ByVal value As HtmlInputText)
            hoshouNo = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for iraiST
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccIraist() As HtmlInputHidden
        Get
            Return iraiST
        End Get
        Set(ByVal value As HtmlInputHidden)
            iraiST = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for kakuninOpenFlg
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccKakuninopenflg() As HtmlInputHidden
        Get
            Return kakuninOpenFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            kakuninOpenFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for lastUpdateDateTime
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccLastupdatedatetime() As HtmlInputHidden
        Get
            Return lastUpdateDateTime
        End Get
        Set(ByVal value As HtmlInputHidden)
            lastUpdateDateTime = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for lastUpdateUserNm
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccLastupdateusernm() As HtmlInputHidden
        Get
            Return lastUpdateUserNm
        End Get
        Set(ByVal value As HtmlInputHidden)
            lastUpdateUserNm = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for shoudakuChousaDate
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccShoudakuChousaDate() As HtmlInputText
        Get
            Return shoudakuChousaDate
        End Get
        Set(ByVal value As HtmlInputText)
            shoudakuChousaDate = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for HiddenRentouBukkenSuu
    ''' </summary>
    ''' <value>連棟物件数</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccRentouBukkenSuu() As HtmlInputHidden
        Get
            Return HiddenRentouBukkenSuu
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenRentouBukkenSuu = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for HiddenSyoriKensuu
    ''' </summary>
    ''' <value>処理件数</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSyoriKensuu() As HtmlInputHidden
        Get
            Return HiddenSyoriKensuu
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenSyoriKensuu = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextBukkenNayoseCd
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccNayoseCd() As HtmlInputText
        Get
            Return Me.TextBukkenNayoseCd
        End Get
        Set(ByVal value As HtmlInputText)
            TextBukkenNayoseCd = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for CheckBunjouCdSaiban
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccChkBunjou() As HtmlInputCheckBox
        Get
            Return Me.CheckBunjouCdSaiban
        End Get
        Set(ByVal value As HtmlInputCheckBox)
            Me.CheckBunjouCdSaiban = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for HiddenInsTokubetuTaiouFlg
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenInsTokubetuTaiouFlg() As HtmlInputHidden
        Get
            Return HiddenInsTokubetuTaiouFlg
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenInsTokubetuTaiouFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for Irai2DataStr
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccIrai2DataStr() As HtmlInputHidden
        Get
            Return Irai2DataStr
        End Get
        Set(ByVal value As HtmlInputHidden)
            Irai2DataStr = value
        End Set
    End Property

#End Region

    ''' <summary>
    ''' Page_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        iraiSession = Context.Items("irai")

        ' ユーザー基本認証
        jBn.UserAuth(user_info)
        If user_info Is Nothing Then
            'ログイン情報が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        '処理ステータスを取得（新規か否か）
        If iraiSession.IraiST = EarthConst.MODE_NEW Or iraiSession.IraiST = EarthConst.MODE_NEW_EDIT Then
            actMode.Value = EarthConst.MODE_NEW
        End If

        If iraiSession.Irai1Mode = EarthConst.MODE_VIEW And iraiSession.Irai2Mode = EarthConst.MODE_EDIT Then
            btn_irai1.Attributes("onclick") = "if(checkAjax()){objEBI('" & btn_irai1_act.ClientID & "').click();}else{return false;}"
        Else
            btn_irai1.Attributes("onclick") = "objEBI('" & btn_irai1_act.ClientID & "').click();"
        End If


        If IsPostBack = False Then

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            ' コンボ設定ヘルパークラスを生成
            Dim helper As New DropDownHelper
            ' 区分コンボにデータをバインドする
            helper.SetDropDownList(cboKubun, DropDownHelper.DropDownType.Kubun2, True)
            ' 構造コンボにデータをバインドする
            helper.SetDropDownList(cboKouzou, DropDownHelper.DropDownType.Kouzou, True, False)
            ' 階層コンボにデータをバインドする
            helper.SetDropDownList(cboKaisou, DropDownHelper.DropDownType.Kaisou, True, False)
            ' 新築建替コンボにデータをバインドする
            helper.SetDropDownList(cboShintikuTatekae, DropDownHelper.DropDownType.ShintikuTatekae, True, False)
            ' 予定基礎コンボにデータをバインドする
            helper.SetDropDownList(cboYoteiKiso, DropDownHelper.DropDownType.YoteiKiso, True, False)
            ' 車庫コンボにデータをバインドする
            helper.SetDropDownList(cboSyako, DropDownHelper.DropDownType.Syako, True)
            ' ﾃﾞｰﾀ破棄コンボにデータをバインドする
            helper.SetDropDownList(cboDataHaki, DropDownHelper.DropDownType.DataHaki, True)
            ' 経由コンボにデータをバインドする
            helper.SetDropDownList(keiyu, DropDownHelper.DropDownType.Keiyu, False, True)

            '地盤読み込みデータがセッションに存在する場合、画面に表示させる
            If iraiSession.JibanData IsNot Nothing And _
               Request("st") = EarthConst.MODE_VIEW And _
               iraiSession.ExeMode <> EarthConst.MODE_EXE_DIRECT_TOUROKU Then
                Dim jr As JibanRecord = iraiSession.JibanData
                setIrai1FromJibanRec(sender, e, jr)
                'actModeに「参照」をセット
                actMode.Value = EarthConst.MODE_VIEW
            ElseIf actMode.Value = "" Then
                'actMode未設定時に「編集」をセット
                actMode.Value = EarthConst.MODE_EDIT
            End If

            '認証結果によって画面表示を切替える
            If iraiSession.Irai1Mode = EarthConst.MODE_EDIT Then
                ' irai1Modeが編集の場合
                If actMode.Value = EarthConst.MODE_NEW Then
                    '新規入力時の権限別設定
                    cboKubun.Focus() '区分にフォーカス
                    If user_info.SinkiNyuuryokuKengen = -1 And user_info.IraiGyoumuKengen = -1 Then
                        '新規入力権限と依頼業務権限がある場合、全ての項目を登録可能
                        jSM.Hash2Ctrl(Me, iraiSession.Irai1Mode, iraiSession.Irai1Data)

                        If hoshouNo.Value = "" Then
                            '保証書NO未採番の場合、保証書NO採番前状態の画面状態に切替える
                            '全ての入力項目を非表示化
                            jBn.SetVisibilityAll(Me, "hidden")
                            '特定コントロールのみを表示
                            cboKubun.Style("visibility") = "visible"
                            btnHoshoushoNoSaiban.Style("visibility") = "visible"
                            hoshoushoNoSaiban.Style("visibility") = "visible"
                        End If

                    Else
                        'それ以外の場合、全て入力不可
                        If user_info.SinkiNyuuryokuKengen = -1 Then
                            '新規入力権限がある場合、保証書NO採番、破棄は使用可能
                            Dim notTargets As New Hashtable
                            notTargets.Add(cboKubun.ID, True)
                            notTargets.Add(btnHoshoushoNoSaiban.ID, True)
                            notTargets.Add(cboDataHaki.ID, True)
                            notTargets.Add(hakiDate.ID, True)
                            jSM.Hash2Ctrl(Me, EarthConst.MODE_VIEW, iraiSession.Irai1Data, notTargets)
                        Else
                            jSM.Hash2Ctrl(Me, EarthConst.MODE_VIEW, iraiSession.Irai1Data)
                            btnHoshoushoNoSaiban.Disabled = True
                        End If
                        juushoTenki.Visible = False
                        setIraiDate.Visible = False
                    End If
                    '破棄権限がない場合、破棄プルダウンは使用不可
                    If user_info.DataHakiKengen <> -1 Then
                        cboDataHaki.Enabled = False
                        hakiDate.Attributes("readonly") = "readonly"
                    End If
                End If

                If actMode.Value = EarthConst.MODE_EDIT And actMode.Value <> EarthConst.MODE_NEW And actMode.Value <> EarthConst.MODE_NEW_EDIT Then
                    Dim notTargets As New Hashtable
                    '編集時の権限別設定（新規時を除く）
                    If user_info.IraiGyoumuKengen = -1 Then
                        '依頼業務権限がある場合、全ての項目を編集可能
                        jSM.Hash2Ctrl(Me, iraiSession.Irai1Mode, iraiSession.Irai1Data)
                    ElseIf (user_info.KekkaGyoumuKengen + user_info.HosyouGyoumuKengen + _
                            user_info.HoukokusyoGyoumuKengen + user_info.KoujiGyoumuKengen) <> 0 Then
                        '依頼、結果、保証、報告書、工事の何れかの権限が有る場合は、
                        '物件名、物件住所、備考、破棄、経由、建物検査のみ編集可能
                        notTargets.Add(seshuName.ID, True)
                        notTargets.Add(TextJyutyuuBukkenMei.ID, True)
                        notTargets.Add(bukkenJyuusho1.ID, True)
                        notTargets.Add(bukkenJyuusho2.ID, True)
                        notTargets.Add(bukkenJyuusho3.ID, True)
                        notTargets.Add(bikou.ID, True)
                        notTargets.Add(cboDataHaki.ID, True)
                        notTargets.Add(hakiDate.ID, True)
                        notTargets.Add(keiyu.ID, True)
                        notTargets.Add(kashi.ID, True)
                        setIraiDate.Visible = False
                        jSM.Hash2Ctrl(Me, EarthConst.MODE_VIEW, iraiSession.Irai1Data, notTargets)
                    Else
                        'それ以外の場合
                        notTargets.Add(cboDataHaki.ID, True)
                        notTargets.Add(hakiDate.ID, True)
                        juushoTenki.Visible = False
                        setIraiDate.Visible = False
                        jSM.Hash2Ctrl(Me, EarthConst.MODE_VIEW, iraiSession.Irai1Data, notTargets)
                    End If
                    '破棄権限がない場合、破棄プルダウンは使用不可
                    If user_info.DataHakiKengen <> -1 Then
                        cboDataHaki.Enabled = False
                        hakiDate.Attributes("readonly") = "readonly"
                    End If
                End If

            Else
                ' irai1Modeが編集以外の場合、照会画面を表示
                jSM.Hash2Ctrl(Me, iraiSession.Irai1Mode, iraiSession.Irai1Data)
            End If


            'Irai2の画面情報をテキスト化して保持
            If iraiSession.Irai2Data IsNot Nothing Then
                Irai2DataStr.Value = String.Empty
                jSM.CtrlHash2Str(iraiSession.Irai2Data, Irai2DataStr.Value)

            End If
            'Irai2のDDL情報をテキスト化して保持
            If iraiSession.DdlDataSyouhin1 IsNot Nothing Then
                Irai2DdlDataStr.Value = String.Empty
                jSM.DdlHash2Str(Irai2DdlDataStr.Value, iraiSession.DdlDataSyouhin1, iraiSession.DdlDataTysHouhou)
            End If


            '画面項目の動きをセッティング（初期値、イベントハンドラ等）
            SetDispAction()

            'タイトルバーに区分、保証書NO、物件名、住所１を表示
            irai1TitleInfobar.InnerHtml = "【" & cboKubun.SelectedValue & "】 【" & hoshouNo.Value & "】 【" & seshuName.Value & _
                                            "】 【" & bukkenJyuusho1.Value & "】"


            '============================================================
            'ダイレクト登録モードが指定されている場合、実行処理を行う
            '============================================================
            If iraiSession.ExeMode IsNot Nothing Then
                If iraiSession.ExeMode = EarthConst.MODE_EXE_DIRECT_TOUROKU Then
                    '処理モードは「登録/修正」を使用
                    exeAtKakunin(sender, e, EarthConst.MODE_EXE_TOUROKU)
                End If
            End If

        Else

            'セッションに画面表示値を格納
            jSM.Ctrl2Hash(Me, jBn.IraiData)
            iraiSession.Irai1Data = jBn.IraiData

            '============================================================
            '受注【確認】での各種処理実行
            '============================================================
            If iraiSession.ExeMode IsNot Nothing Then
                exeAtKakunin(sender, e, iraiSession.ExeMode)
            End If

        End If


        'モード別に項目の表示状態を切替える
        If iraiSession.Irai1Mode = EarthConst.MODE_EDIT Then
            irai1DispLink.HRef = ""
            btn_irai1.Visible = False
        Else
            irai1DispLink.HRef = "javascript:changeDisplay('" & irai1TBody.ClientID & "');changeDisplay('" & irai1TitleInfobar.ClientID & "');"
            btn_irai1.Visible = True
            juushoTenki.Visible = False
            setIraiDate.Visible = False
            hoshoushoNoSaiban.Visible = False
            btnHoshoushoNoSaiban.Visible = False
            If iraiSession.Irai1Mode = EarthConst.MODE_VIEW Then
                irai1TitleInfobar.Style("display") = "inline"
                irai1TBody.Style("display") = "none"
            End If
        End If

        '採番チェックボックス表示切替
        Me.ChgDispSpanBunjouCdSaiban()

        '施主名有無ラジオボタン表示切替
        Me.ChgDispSpanSesyumeiUmu()

        'Irai2の画面情報をテキスト化して保持
        If Irai2DataStr.Value <> String.Empty Then
            iraiSession.Irai2DataStr = Irai2DataStr.Value
        End If
        'Irai2のドロップダウンリスト情報(テキスト)をセッションに保存
        If Irai2DdlDataStr.Value <> String.Empty Then
            iraiSession.DdlDataStr = Irai2DdlDataStr.Value
        End If

        'コンテキストに値を格納
        Context.Items("irai") = iraiSession

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispAction()
        Dim jBn As New Jiban '地盤画面クラス

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 他システムへのリンクボタン設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '調査見積書
        cl.getTyousaMitsumorisyoFilePath(cboKubun.SelectedValue, hoshouNo.Value, ButtonTyousaMitsumorisyo)
        '保証書DB
        cl.getHosyousyoDbFilePath(cboKubun.SelectedValue, hoshouNo.Value, ButtonHosyousyoDB)
        'ReportJHS
        cl.getReportJHSPath(cboKubun.SelectedValue, hoshouNo.Value, ButtonRJHS)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 区分、保証書NO関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '保証書NO発行済みの場合、区分/採番ボタンを無効化
        CheckSaiban()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 重複チェック関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '重複チェック処理実行
        If HiddenSesyumeiOld.Value = String.Empty Or HiddenJuusyo1.Value = String.Empty Then
            ChkChoufuku(Nothing)
        Else
            choufukuCheck.Style("display") = "none"
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' プルダウン/コード入力連携設定(画面側で更にイベントハンドラを埋め込む際には、かき消さないように追記する)
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        jBn.SetPullCdScriptSrc(tatiai_cd, tatiai)
        jBn.SetPullCdScriptSrc(kouzou_cd, cboKouzou)
        jBn.SetPullCdScriptSrc(kaisou_cd, cboKaisou)
        jBn.SetPullCdScriptSrc(sintikuTatekae_cd, cboShintikuTatekae)
        jBn.SetPullCdScriptSrc(yoteiKiso_cd, cboYoteiKiso)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 「～その他」や「立会有り」の場合のみ表示する項目
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '構造、予定基礎に関しては、「9：その他」の場合のみ「～その他」の入力項目を使用可能にするスクリプトを埋め込む
        cboKouzou.Attributes("onchange") += "checkSonota(this.value==9,'" & kouzouSonota.ClientID & "','" & lblKouzouSonota.ClientID & "');"
        cboYoteiKiso.Attributes("onchange") += "checkSonota(this.value==9,'" & yoteiKisoSonota.ClientID & "','" & lblYoteiKisoSonota.ClientID & "');"
        '指定値選択時の動き(画面表示時用)
        Dim tmpAL As New ArrayList
        tmpAL.Add(kouzouSonota)
        tmpAL.Add(lblKouzouSonota)
        cl.CheckVisible("9", cboKouzou, tmpAL)
        Dim tmpAL2 As New ArrayList
        tmpAL2.Add(yoteiKisoSonota)
        tmpAL2.Add(lblYoteiKisoSonota)
        cl.CheckVisible("9", cboYoteiKiso, tmpAL2)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 「立会者有り」の場合のみ表示する項目
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 立会者チェックボックス群
        Dim tatiai_ctrl As String = tatiaisya.ClientID & "," & tatiaiSha_1.ClientID & "," & tatiaiSha_2.ClientID & "," & _
                                    tatiaiSha_3.ClientID & "," & tatiaiDiv.ClientID

        ' 立会者はありの場合のみ設定
        tatiai.Attributes("onchange") += "setVisibility(this.value==0,'" & tatiai_ctrl & "');"
        '指定値選択時の動き(画面表示時用)
        Dim tmpAL3 As New ArrayList
        tmpAL3.Add(tatiaisya)
        tmpAL3.Add(tatiaiSha_1)
        tmpAL3.Add(tatiaiSha_2)
        tmpAL3.Add(tatiaiSha_3)
        tmpAL3.Add(tatiaiDiv)
        cl.CheckVisible("1", tatiai, tmpAL3)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++

        '(FC)申込検索表示ボタン(地盤T.新規登録元区分=3(地盤モール)の場合は活性化、以外は非活性化)
        If HiddenSinkiTourokuMotoKbn.Value = CStr(EarthEnum.EnumSinkiTourokuMotoKbnType.ReportJHS) Then
            '申込検索表示ボタン
            Me.ButtonSearchMousikomi.Disabled = False
            Me.ButtonSearchMousikomi.Attributes("onclick") = "callSearch('" & cboKubun.ClientID & EarthConst.SEP_STRING & hoshouNo.ClientID & "','" & UrlConst.SEARCH_MOUSIKOMI & "','','');"
            'FC申込検索表示ボタン
            Me.ButtonSearchFcMousikomi.Disabled = False
            Me.ButtonSearchFcMousikomi.Attributes("onclick") = "callSearch('" & cboKubun.ClientID & EarthConst.SEP_STRING & hoshouNo.ClientID & "','" & UrlConst.SEARCH_FC_MOUSIKOMI & "','','');"
        Else
            '申込検索表示ボタン
            Me.ButtonSearchMousikomi.Disabled = True
            'FC申込検索表示ボタン
            Me.ButtonSearchFcMousikomi.Disabled = True
        End If

        '物件履歴表示ボタン
        ButtonBukkenRireki.Attributes("onclick") = "callSearch('" & cboKubun.ClientID & EarthConst.SEP_STRING & hoshouNo.ClientID & "','" & UrlConst.POPUP_BUKKEN_RIREKI & "','','');"

        '更新履歴表示ボタン
        ButtonKousinRireki.Attributes("onclick") = "callSearch('" & cboKubun.ClientID & EarthConst.SEP_STRING & hoshouNo.ClientID & "','" & UrlConst.SEARCH_KOUSIN_RIREKI & "','','');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' データ破棄種別によって、画面表示を切替える処理
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' データ破棄日は未指定時のみ活性化（その際はデータ破棄関係以外全て非活性となる）
        cboDataHaki.Attributes("onchange") = "changeHaki(this);"
        CheckHakiDisable()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 前日日付自動関連セット
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        dateYesterday.Value = Format(Today.AddDays(-1), "yyyy/MM/dd")
        Dim tmpYDscr As String = "if(objEBI('@IRAIDATA').value=='')objEBI('@IRAIDATA').value='@YESTERDAY';"
        tmpYDscr = tmpYDscr.Replace("@IRAIDATA", iraiDate.ClientID)
        tmpYDscr = tmpYDscr.Replace("@YESTERDAY", dateYesterday.Value)
        setIraiDate.Attributes("onclick") = tmpYDscr

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 受注物件名を物件名からコピーするイベントハンドラ
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        seshuName.Attributes("onblur") = "setJyutyuuBukkenMei(objEBI('" & TextJyutyuuBukkenMei.ClientID & "'),this);"

        '分譲コード
        Me.TextBunjouCd.Attributes("onblur") = "if(checkNumber(this)) checkMinus(this);"
        CheckBunjouCdSaiban.Attributes("onclick") = "objEBI('" & Me.ButtonBunjouSaiban.ClientID & "').click();"
    End Sub

    ''' <summary>
    ''' 保証書NO発行済みの場合、区分/採番ボタンを無効化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckSaiban()
        If cboKubun.SelectedValue <> "" And hoshouNo.Value <> "" Then
            cboKubun.Style("display") = "none"
            spanKubun.Style("display") = "inline"
            cboKubun.Items(cboKubun.SelectedIndex).Text = cboKubun.Items(cboKubun.SelectedIndex).Text.Split("[")(0)
            spanKubun.InnerHtml = "【 " & cboKubun.SelectedItem.Text & " 】"
            btnHoshoushoNoSaiban.Style("display") = "none"
        End If

        '採番チェックボックス表示切替
        Me.ChgDispSpanBunjouCdSaiban()

        '施主名有無ラジオボタン表示切替
        Me.ChgDispSpanSesyumeiUmu()

    End Sub

    ''' <summary>
    ''' 採番チェックボックスSPAN表示切替
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChgDispSpanBunjouCdSaiban()
        '採番チェックボックス
        If Me.CheckBunjouCdSaiban.Style("visibility") = "hidden" Then
            Me.SpanBunjouCdSaiban.Visible = False
        Else
            Me.SpanBunjouCdSaiban.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' 施主名有無ラジオボタンSPAN表示切替
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChgDispSpanSesyumeiUmu()
        '施主名有無ラジオボタン
        If Me.RadioSesyumei1.Style("visibility") = "hidden" Then    '有
            Me.SpanSesyumei1.Visible = False
        Else
            Me.SpanSesyumei1.Visible = True
        End If

        If Me.RadioSesyumei0.Style("visibility") = "hidden" Then    '無
            Me.SpanSesyumei0.Visible = False
        Else
            Me.SpanSesyumei0.Visible = True
        End If

    End Sub

    ''' <summary>
    ''' Ajax動作時のフォーカスセット
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub SetFocus(ByVal ctrl As Control)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetFocus", _
                                                    ctrl)
        'フォーカスセット
        If iraiSession.Irai1Mode = EarthConst.MODE_EDIT Then
            masterAjaxSM.SetFocus(ctrl)
        End If
    End Sub

    ''' <summary>
    ''' 「編集」ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btn_irai1_act_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_irai1_act.ServerClick

        'Step2の依頼確定状況をチェック
        Dim irai2kakuteiFlg As Boolean = True
        RaiseEvent OyaGamenAction_hensyu(sender, e, irai2kakuteiFlg)
        If irai2kakuteiFlg = False Then
            Exit Sub
        End If

        If iraiSession.ExeMode IsNot Nothing Then
            iraiSession.ExeMode = Nothing
        End If

        '処理ステータスをコンテキストで渡す
        Dim stVal As String = iraiSession.IraiST
        If iraiSession.IraiST = EarthConst.MODE_NEW Or iraiSession.IraiST = EarthConst.MODE_NEW_EDIT Then
            stVal = EarthConst.MODE_NEW_EDIT
        End If

        iraiSession.IraiST = IIf(stVal Is Nothing, String.Empty, stVal)
        Context.Items("irai") = iraiSession
        Server.Transfer(UrlConst.IRAI_STEP_1)

    End Sub

    ''' <summary>
    ''' 保証書NO自動採番処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub hoshoushoNoSaiban_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles hoshoushoNoSaiban.ServerClick

        '採番処理完了フラグを初期化
        saibanOkFlg = False

        Dim kubun As String = String.Empty
        If Not iraiSession Is Nothing AndAlso iraiSession.CopyKbn <> String.Empty Then
            kubun = iraiSession.CopyKbn
        Else
            kubun = cboKubun.SelectedValue
        End If

        Dim hosyousyo_no As String = ""
        Dim jBn As New Jiban '地盤画面クラス

        Dim intRentouBukkenSuu As Integer = 1 '連棟物件数
        Dim strRentouBukkenSuu As String = ""
        Dim errMess As String = "" 'JS用

        '区分あるいは連棟物件数が未入力の場合、処理を抜ける
        If kubun = "" Or HiddenRentouBukkenSuu.Value = "" Then
            'エラーMSG
            errMess = Messages.MSG013E.Replace("@PARAM1", "区分、連棟物件数")
            MLogic.AlertMessage(sender, errMess)
            'フォーカスセット
            SetFocus(btnHoshoushoNoSaiban)
            Exit Sub
        End If

        '連棟物件数を取得
        strRentouBukkenSuu = CStr(HiddenRentouBukkenSuu.Value)
        strRentouBukkenSuu = StrConv(strRentouBukkenSuu, VbStrConv.Narrow) '全角→半角
        HiddenRentouBukkenSuu.Value = strRentouBukkenSuu '半角で入れ直し

        '入力チェック(連棟物件数)
        '●バイト数チェック(文字列入力フィールドが対象)
        If jBn.ByteCheckSJIS(strRentouBukkenSuu, "3") = False Then
            errMess += Messages.MSG092E.Replace("{0}", "連棟物件数").Replace("{1}", "3")
        End If
        '●数値チェック
        If IsNumeric(strRentouBukkenSuu) = False Then
            errMess += Messages.MSG040E.Replace("@PARAM1", "連棟物件数")
        Else
            intRentouBukkenSuu = CInt(strRentouBukkenSuu)
            '●数値範囲チェック
            If intRentouBukkenSuu <= 0 Or intRentouBukkenSuu > 999 Then
                errMess += Messages.MSG111E.Replace("@PARAM1", "連棟物件数").Replace("@PARAM2", "1").Replace("@PARAM3", "999")
            End If
        End If

        'エラー発生時処理
        If errMess <> "" Then
            HiddenRentouBukkenSuu.Value = "" '初期化

            'エラーメッセージ表示
            MLogic.AlertMessage(sender, errMess)
            'フォーカスセット
            SetFocus(btnHoshoushoNoSaiban)
            Exit Sub
        End If

        ' 地盤テーブル初期登録を実施
        If MyLogic.InsertJibanData( _
                                    sender, _
                                    kubun, _
                                    hosyousyo_no, _
                                    user_info.LoginUserId, _
                                    intRentouBukkenSuu, _
                                    EarthEnum.EnumSinkiTourokuMotoKbnType.EarthJyutyuu _
                                    ) _
                                    = False Then

            '採番失敗
            errMess = Messages.MSG019E.Replace("@PARAM1", "採番")
            MLogic.AlertMessage(sender, errMess)
            'フォーカスセット
            SetFocus(btnHoshoushoNoSaiban)
            Exit Sub
        End If

        '特別対応デフォルト登録に初回のみ値を設定　
        AccHiddenInsTokubetuTaiouFlg.Value = "1"

        '特別対応データのデフォルト登録(新規引継時。以外は依頼内容確定ボタン押下時に処理を行なう)
        If Not iraiSession Is Nothing AndAlso _
            iraiSession.ExeMode = EarthConst.MODE_EXE_HIKITUGI Then

            If MyLogic.insertTokubetuTaiouUIHikitugi(Me, _
                                                     kubun, _
                                                     hoshouNo.Value, _
                                                     hosyousyo_no, _
                                                     user_info.LoginUserId, _
                                                     DateTime.Now, _
                                                     Me.HiddenRentouBukkenSuu.Value) = False Then
                MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "特別対応データの登録"), 0, "hoshoushoNoSaiban_ServerClick")
                'フォーカスセット
                SetFocus(btnHoshoushoNoSaiban)
                Exit Sub
            End If

            '特別対応デフォルト登録に初回のみ値を設定　
            AccHiddenInsTokubetuTaiouFlg.Value = "@"

        End If

        '画面表示
        Me.cboKubun.SelectedValue = kubun
        '保証書NO
        hoshouNo.Value = hosyousyo_no
        '更新日付をクリア
        updateDateTime.Value = ""

        '全ての入力項目を活性化(重複チェック、～その他を除く)
        '＠権限：新規入力権限、依頼業務権限を共に所持している場合のみ
        If user_info IsNot Nothing Then
            If actMode.Value = EarthConst.MODE_NEW Then
                If user_info.SinkiNyuuryokuKengen = -1 And user_info.IraiGyoumuKengen = -1 Then

                    '全ての入力項目を表示化
                    jBn.SetVisibilityAll(Me, "visible")

                End If
            End If
        End If


        '画面表示状態を再設定
        SetDispAction()

        '区分、採番ボタンを無効化
        CheckSaiban()

        'フォーカスセット
        SetFocus(cboDataHaki)

        '採番処理完了フラグを立てる
        saibanOkFlg = True

    End Sub

    ''' <summary>
    ''' 重複チェックボタン押下時の処理（非表示ボタン）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnChoufukuCheck_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' 入力チェック
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList
        CheckKinsoku(errMess, arrFocusTargetCtrl)
        If errMess <> String.Empty Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                SetFocus(arrFocusTargetCtrl(0))
            End If
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "err", tmpScript, True)
        Else
            '入力チェックOKなら、重複チェック実行
            ChkChoufuku(sender)
        End If

    End Sub

    ''' <summary>
    ''' 重複チェック処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChkChoufuku(ByVal sender As System.Object)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkChoufuku", _
                                                    sender)

        Dim bolResult1 As Boolean = True
        Dim bolResult2 As Boolean = True

        ' 物件名が空白以外の場合、物件名の重複チェックを実施
        If seshuName.Value.Trim <> "" Then
            If MyLogic.ChkTyouhuku(cboKubun.SelectedValue, _
                                 hoshouNo.Value, _
                                 seshuName.Value.Trim) = True Then
                bolResult1 = False
            End If
        End If

        ' 物件名重複チェックがOKで、物件住所が空白以外の場合、物件住所の重複チェックを実施
        If bukkenJyuusho1.Value.Trim() <> "" Or bukkenJyuusho2.Value.Trim() <> "" Then
            If MyLogic.ChkTyouhuku(cboKubun.SelectedValue, _
                                 hoshouNo.Value, _
                                 bukkenJyuusho1.Value.Trim(), _
                                 bukkenJyuusho2.Value.Trim()) = True Then
                bolResult2 = False
            End If
        End If

        If sender IsNot Nothing Then
            If choufukuKakuninTargetId.Value = seshuName.ClientID Then
                choufukuKakuninFlg1.Value = bolResult1.ToString
            ElseIf (choufukuKakuninTargetId.Value = bukkenJyuusho1.ClientID Or choufukuKakuninTargetId.Value = bukkenJyuusho2.ClientID) Then
                choufukuKakuninFlg2.Value = bolResult2.ToString
            End If
        End If

        '重複が存在する場合、「重複物件あり」ボタンを有効化
        If bolResult1 And bolResult2 Then
            choufukuCheck.Disabled = True
            choufukuCheck.Value = "重複物件なし"
            choufukuKakuninFlg1.Value = Boolean.TrueString
            choufukuKakuninFlg2.Value = Boolean.TrueString
        Else
            choufukuCheck.Disabled = False
            choufukuCheck.Value = "重複物件あり"
        End If

        ' チェック処理のトリガーになったIDを格納しているコントロールをクリア
        choufukuKakuninTargetId.Value = String.Empty

    End Sub

    ''' <summary>
    ''' 重複物件確認画面呼び出しボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub choufukuCheck_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '重複確認フラグをセット
        choufukuKakuninFlg1.Value = Boolean.TrueString
        choufukuKakuninFlg2.Value = Boolean.TrueString

        '重複物件確認画面呼び出し
        Dim tmpFocusScript = "objEBI('" & choufukuCheck.ClientID & "').focus();"
        Dim tmpScript As String = "callSearch('" & cboKubun.ClientID & EarthConst.SEP_STRING & hoshouNo.ClientID & _
                                                                EarthConst.SEP_STRING & seshuName.ClientID & _
                                                                EarthConst.SEP_STRING & bukkenJyuusho1.ClientID & EarthConst.SEP_STRING & _
                                                                bukkenJyuusho2.ClientID & "', '" & UrlConst.SEARCH_TYOUFUKU & "');"
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript + tmpScript, True)

    End Sub

    ''' <summary>
    ''' データ破棄コンボ変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub changeHaki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If hakiDate.Value = String.Empty Then
            hakiDate.Value = Format(Today, "yyyy/MM/dd")
        End If
        '画面表示状態を再設定
        SetDispAction()
    End Sub

    ''' <summary>
    ''' 破棄種別によって、コントロールの有効無効を切替える
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckHakiDisable()
        '地盤画面共通クラス
        Dim jBn As New Jiban

        '有効化、無効化の対象外にするコントロール郡
        Dim noTarget As New Hashtable
        noTarget.Add(btn_irai1_act.ID, True)
        noTarget.Add(btn_irai1.ID, True)
        noTarget.Add(cboDataHaki.ID, True)
        noTarget.Add(changeHaki.ID, True)
        noTarget.Add(spanHakiDate.ID, True)
        noTarget.Add(hakiDate.ID, True)
        noTarget.Add(choufukuCheck.ID, True)
        noTarget.Add(btnHoshoushoNoSaiban.ID, True)
        noTarget.Add(ButtonHosyousyoDB.ID, True) '保証書DB
        noTarget.Add(ButtonTyousaMitsumorisyo.ID, True) '調査見積書
        noTarget.Add(Me.ButtonBukkenRireki.ID, True) '物件履歴
        noTarget.Add(Me.ButtonRJHS.ID, True) 'R-JHS
        noTarget.Add(Me.ButtonSearchMousikomi.ID, True) '申込検索ボタン
        noTarget.Add(Me.ButtonSearchFcMousikomi.ID, True) 'FC申込検索ボタン

        If cboDataHaki.SelectedValue >= "1" Then
            '全てのコントロールを無効化()
            jBn.ChangeDesabledAll(Me, True, noTarget)
            '破棄日の表示を切替える
            hakiDate.Style.Remove("display")
            spanHakiDate.Style.Remove("display")
        Else
            '全てのコントロールを有効化()
            jBn.ChangeDesabledAll(Me, False, noTarget)
            '破棄日の表示を切替える
            hakiDate.Style("display") = "none"
            spanHakiDate.Style("display") = "none"
        End If

    End Sub

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <remarks></remarks>
    Public Function CheckInput() As Boolean

        '地盤画面共通クラス
        Dim jBn As New Jiban

        '全てのコントロールを有効化(破棄種別チェック用)
        Dim noTarget As New Hashtable
        noTarget.Add(Me.ButtonSearchMousikomi.ID, True) '申込検索ボタン
        noTarget.Add(Me.ButtonSearchFcMousikomi.ID, True) 'FC申込検索ボタン

        jBn.ChangeDesabledAll(Me, False, noTarget)

        'エラーメッセージ初期化
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList

        If cboDataHaki.SelectedValue >= "1" Then
            '破棄種別が選択されている場合、スルー

        Else
            '必須チェック
            If cboKubun.SelectedValue = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "区分")
                arrFocusTargetCtrl.Add(cboKubun)
            End If
            If hoshouNo.Value = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "保証書NO")
                arrFocusTargetCtrl.Add(hoshouNo)
            End If
            If seshuName.Value = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "物件名")
                arrFocusTargetCtrl.Add(seshuName)
            End If
            If Me.RadioSesyumei0.Checked = False AndAlso Me.RadioSesyumei1.Checked = False Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "施主名有無")
                arrFocusTargetCtrl.Add(RadioSesyumei1)
            End If
            If TextJyutyuuBukkenMei.Value = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "受注物件名")
                arrFocusTargetCtrl.Add(TextJyutyuuBukkenMei)
            End If
            If bukkenJyuusho1.Value = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "物件住所１")
                arrFocusTargetCtrl.Add(bukkenJyuusho1)
            End If
            If Request("st") = EarthConst.MODE_NEW Then
                If (choufukuKakuninFlg1.Value = Boolean.FalseString Or choufukuKakuninFlg2.Value = Boolean.FalseString) And _
                    iraiSession.Irai1Mode = EarthConst.MODE_EDIT Then
                    errMess += Messages.MSG017E
                    arrFocusTargetCtrl.Add(choufukuCheck)
                End If
            End If
            If iraiDate.Value = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "依頼日")
                arrFocusTargetCtrl.Add(iraiDate)
            End If
            If chousaKibouDate.Value = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "調査希望日")
                arrFocusTargetCtrl.Add(chousaKibouDate)
            End If

        End If

        '日付チェック
        If hakiDate.Value <> String.Empty Then
            If DateTime.Parse(hakiDate.Value) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(hakiDate.Value) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "データ破棄日")
                arrFocusTargetCtrl.Add(hakiDate)
            End If
        End If
        If iraiDate.Value <> String.Empty Then
            If DateTime.Parse(iraiDate.Value) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(iraiDate.Value) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "依頼日")
                arrFocusTargetCtrl.Add(iraiDate)
            End If
        End If
        If chousaKibouDate.Value <> String.Empty Then
            If DateTime.Parse(chousaKibouDate.Value) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(chousaKibouDate.Value) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "調査希望日")
                arrFocusTargetCtrl.Add(chousaKibouDate)
            End If
        End If
        If shoudakuChousaDate.Value <> String.Empty Then
            If DateTime.Parse(shoudakuChousaDate.Value) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(shoudakuChousaDate.Value) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "承諾書調査日")
                arrFocusTargetCtrl.Add(shoudakuChousaDate)
            End If
        End If
        If TextKisoTyakkouYoteiDateFrom.Value <> String.Empty Then
            If DateTime.Parse(TextKisoTyakkouYoteiDateFrom.Value) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextKisoTyakkouYoteiDateFrom.Value) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "基礎着工予定日FROM")
                arrFocusTargetCtrl.Add(TextKisoTyakkouYoteiDateFrom)
            End If
        End If
        If TextKisoTyakkouYoteiDateTo.Value <> String.Empty Then
            If DateTime.Parse(TextKisoTyakkouYoteiDateTo.Value) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextKisoTyakkouYoteiDateTo.Value) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "基礎着工予定日TO")
                arrFocusTargetCtrl.Add(TextKisoTyakkouYoteiDateTo)
            End If
        End If

        '日付From-Toチェック
        If TextKisoTyakkouYoteiDateFrom.Value <> String.Empty And TextKisoTyakkouYoteiDateTo.Value <> String.Empty Then
            If TextKisoTyakkouYoteiDateFrom.Value > TextKisoTyakkouYoteiDateTo.Value Then
                errMess += Messages.MSG022E.Replace("@PARAM1", "基礎着工予定日")
                arrFocusTargetCtrl.Add(TextKisoTyakkouYoteiDateFrom)
            End If
        End If

        '禁則文字数チェック
        CheckKinsoku(errMess, arrFocusTargetCtrl)

        '改行変換
        If bikou.Value <> "" Then
            bikou.Value = bikou.Value.Replace(vbCrLf, " ")
        End If

        'バイト数チェック
        If jBn.ByteCheckSJIS(seshuName.Value, seshuName.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "物件名")
            arrFocusTargetCtrl.Add(seshuName)
        End If
        If jBn.ByteCheckSJIS(TextJyutyuuBukkenMei.Value, TextJyutyuuBukkenMei.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "受注物件名")
            arrFocusTargetCtrl.Add(TextJyutyuuBukkenMei)
        End If
        If jBn.ByteCheckSJIS(bukkenJyuusho1.Value, bukkenJyuusho1.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "物件住所１")
            arrFocusTargetCtrl.Add(bukkenJyuusho1)
        End If
        If jBn.ByteCheckSJIS(bukkenJyuusho2.Value, bukkenJyuusho2.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "物件住所２")
            arrFocusTargetCtrl.Add(bukkenJyuusho2)
        End If
        If jBn.ByteCheckSJIS(bukkenJyuusho3.Value, bukkenJyuusho3.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "物件住所３")
            arrFocusTargetCtrl.Add(bukkenJyuusho3)
        End If
        If jBn.ByteCheckSJIS(bikou.Value, 256) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "備考")
            arrFocusTargetCtrl.Add(bikou)
        End If
        If jBn.ByteCheckSJIS(bikou2.Value, 512) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "備考２")
            arrFocusTargetCtrl.Add(bikou)
        End If
        If jBn.ByteCheckSJIS(Me.TextBunjouCd.Value, Me.TextBunjouCd.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "分譲コード")
            arrFocusTargetCtrl.Add(Me.TextBunjouCd)
        End If
        If jBn.ByteCheckSJIS(Me.TextBukkenNayoseCd.Value, Me.TextBukkenNayoseCd.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "物件名寄コード")
            arrFocusTargetCtrl.Add(Me.TextBukkenNayoseCd)
        End If
        If jBn.ByteCheckSJIS(tyousakaisyaRenraku.Value, tyousakaisyaRenraku.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "調査連絡先：連絡先")
            arrFocusTargetCtrl.Add(tyousakaisyaRenraku)
        End If
        If jBn.ByteCheckSJIS(tyousakaisyaTantou.Value, tyousakaisyaTantou.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "調査連絡先：店担当者")
            arrFocusTargetCtrl.Add(tyousakaisyaTantou)
        End If
        If jBn.ByteCheckSJIS(chousaKibouTime.Value, chousaKibouTime.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "調査希望時間")
            arrFocusTargetCtrl.Add(chousaKibouTime)
        End If
        If jBn.ByteCheckSJIS(kouzouSonota.Value, kouzouSonota.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "構造その他")
            arrFocusTargetCtrl.Add(kouzouSonota)
        End If
        If jBn.ByteCheckSJIS(yoteiKisoSonota.Value, yoteiKisoSonota.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "予定基礎その他")
            arrFocusTargetCtrl.Add(yoteiKisoSonota)
        End If
        If jBn.ByteCheckSJIS(tyousakaisyaTel.Value, tyousakaisyaTel.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "調査連絡先：Tel")
            arrFocusTargetCtrl.Add(tyousakaisyaTel)
        End If
        If jBn.ByteCheckSJIS(tyousakaisyaFax.Value, tyousakaisyaFax.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "調査連絡先：Fax")
            arrFocusTargetCtrl.Add(tyousakaisyaFax)
        End If
        If jBn.ByteCheckSJIS(tyousakaisyaMailAddr.Value, tyousakaisyaMailAddr.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "調査連絡先：mail")
            arrFocusTargetCtrl.Add(tyousakaisyaMailAddr)
        End If
        If jBn.ByteCheckSJIS(keiyakuNo.Value, keiyakuNo.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "契約NO")
            arrFocusTargetCtrl.Add(keiyakuNo)
        End If

        '桁数チェック
        If jBn.SuutiStrCheck(iraiYoteiTousu.Value, 4, 0) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "依頼予定棟数").Replace("@PARAM2", "4").Replace("@PARAM3", "0")
            arrFocusTargetCtrl.Add(iraiYoteiTousu)
        End If
        If jBn.SuutiStrCheck(sijiryoku.Value, 4, 1) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "設計許容支持力").Replace("@PARAM2", "4").Replace("@PARAM3", "1")
            arrFocusTargetCtrl.Add(sijiryoku)
        End If
        If jBn.SuutiStrCheck(negiri.Value, 8, 4) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "根切り深さ").Replace("@PARAM2", "8").Replace("@PARAM3", "4")
            arrFocusTargetCtrl.Add(negiri)
        End If
        If jBn.SuutiStrCheck(morituti.Value, 9, 3) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "予定盛土厚さ").Replace("@PARAM2", "9").Replace("@PARAM3", "3")
            arrFocusTargetCtrl.Add(morituti)
        End If

        '●その他チェック
        '分譲コード(存在チェック)
        If Me.TextBunjouCd.Value <> String.Empty And Me.CheckBunjouCdSaiban.Checked = False Then
            If MyLogic.ChkJibanBunjouCd(Me.TextBunjouCd.Value) = False Then
                errMess += Messages.MSG165E.Replace("@PARAM1", "分譲コード").Replace("@PARAM2", "地盤データ").Replace("@PARAM3", "分譲コード")
                arrFocusTargetCtrl.Add(Me.TextBunjouCd)
            End If
        End If
        If Me.CheckBunjouCdSaiban.Checked Then
            If Me.TextBunjouCd.Value <> Me.HiddenSaibanNo.Value Then
                errMess += Messages.MSG120E.Replace("@PARAM1", "入力した分譲コード").Replace("@PARAM2", "採番した分譲コード")
                arrFocusTargetCtrl.Add(Me.CheckBunjouCdSaiban)
            End If
        End If

        '物件NOを取得
        Dim strBukkenNo As String = Me.cboKubun.SelectedValue.ToUpper & Me.hoshouNo.Value.ToUpper
        Dim strBukkenNayoseCd As String = Me.TextBukkenNayoseCd.Value.ToUpper
        Dim blnBukkenNoFlg As Boolean = True

        '物件名寄コード
        If strBukkenNayoseCd = String.Empty Then '未入力
            strBukkenNayoseCd = strBukkenNo '未入力の場合、当物件NOをセット
        End If

        '物件名寄コード(入力不正チェック)
        If Me.TextBukkenNayoseCd.Value <> String.Empty Then
            '11桁のチェック
            If sLogic.GetStrByteSJIS(Me.TextBukkenNayoseCd.Value) = Me.TextBukkenNayoseCd.MaxLength Then
            Else
                blnBukkenNoFlg = False

                errMess += Messages.MSG040E.Replace("@PARAM1", "物件名寄コード") & "【区分+保証書NO(番号)】\r\n"
                arrFocusTargetCtrl.Add(Me.TextBukkenNayoseCd)
            End If
        End If

        If blnBukkenNoFlg Then
            '名寄先が親物件かのチェック
            If MyLogic.ChkBukkenNayoseOyaBukken(strBukkenNo, strBukkenNayoseCd) = False Then
                errMess += Messages.MSG167E.Replace("@PARAM1", "名寄先の物件").Replace("@PARAM2", "子物件").Replace("@PARAM3", "物件名寄コード")
                arrFocusTargetCtrl.Add(Me.TextBukkenNayoseCd)
            End If

            '自物件の名寄状況チェック
            If MyLogic.ChkBukkenNayoseJyky(strBukkenNo, strBukkenNayoseCd) = False Then
                errMess += Messages.MSG167E.Replace("@PARAM1", "当物件NO").Replace("@PARAM2", "他物件の名寄先").Replace("@PARAM3", "物件名寄コード")
                arrFocusTargetCtrl.Add(Me.TextBukkenNayoseCd)
            End If
        End If

        'エラー発生時は画面再描画のため、コントロールの有効無効を切替える
        If errMess <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            ' 破棄種別チェック
            SetDispAction()
            Return False
        End If

        'エラー無しの場合、Trueを返す
        Return True

    End Function

    ''' <summary>
    ''' 禁則文字数チェック
    ''' </summary>
    ''' <param name="errMess"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <remarks></remarks>
    Private Sub CheckKinsoku(ByRef errMess As String, _
                             ByRef arrFocusTargetCtrl As ArrayList)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckKinsoku", _
                                                    errMess, _
                                                    arrFocusTargetCtrl)

        '地盤画面共通クラス
        Dim jBn As New Jiban

        '禁則文字数チェック
        If jBn.KinsiStrCheck(seshuName.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件名")
            arrFocusTargetCtrl.Add(seshuName)
        End If
        If jBn.KinsiStrCheck(TextJyutyuuBukkenMei.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "受注物件名")
            arrFocusTargetCtrl.Add(TextJyutyuuBukkenMei)
        End If
        If jBn.KinsiStrCheck(bukkenJyuusho1.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件住所１")
            arrFocusTargetCtrl.Add(bukkenJyuusho1)
        End If
        If jBn.KinsiStrCheck(bukkenJyuusho2.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件住所２")
            arrFocusTargetCtrl.Add(bukkenJyuusho2)
        End If
        If jBn.KinsiStrCheck(bukkenJyuusho3.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件住所３")
            arrFocusTargetCtrl.Add(bukkenJyuusho3)
        End If
        If jBn.KinsiStrCheck(bikou.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "備考")
            arrFocusTargetCtrl.Add(bikou)
        End If
        If jBn.KinsiStrCheck(bikou2.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "備考２")
            arrFocusTargetCtrl.Add(bikou)
        End If
        If jBn.KinsiStrCheck(Me.TextBunjouCd.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "分譲コード")
            arrFocusTargetCtrl.Add(Me.TextBunjouCd)
        End If
        If jBn.KinsiStrCheck(Me.TextBukkenNayoseCd.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件名寄コード")
            arrFocusTargetCtrl.Add(Me.TextBukkenNayoseCd)
        End If
        If jBn.KinsiStrCheck(tyousakaisyaRenraku.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "調査連絡先：連絡先")
            arrFocusTargetCtrl.Add(tyousakaisyaRenraku)
        End If
        If jBn.KinsiStrCheck(tyousakaisyaTantou.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "調査連絡先：店担当者")
            arrFocusTargetCtrl.Add(tyousakaisyaTantou)
        End If
        If jBn.KinsiStrCheck(tyousakaisyaTel.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "調査連絡先：Tel")
            arrFocusTargetCtrl.Add(tyousakaisyaTel)
        End If
        If jBn.KinsiStrCheck(tyousakaisyaFax.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "調査連絡先：Fax")
            arrFocusTargetCtrl.Add(tyousakaisyaFax)
        End If
        If jBn.KinsiStrCheck(tyousakaisyaMailAddr.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "調査連絡先：Mail")
            arrFocusTargetCtrl.Add(tyousakaisyaMailAddr)
        End If
        If jBn.KinsiStrCheck(keiyakuNo.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "契約NO")
            arrFocusTargetCtrl.Add(keiyakuNo)
        End If
        If jBn.KinsiStrCheck(chousaKibouTime.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "調査希望時間")
            arrFocusTargetCtrl.Add(chousaKibouTime)
        End If
        If jBn.KinsiStrCheck(kouzouSonota.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "構造その他")
            arrFocusTargetCtrl.Add(kouzouSonota)
        End If
        If jBn.KinsiStrCheck(yoteiKisoSonota.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "予定基礎その他")
            arrFocusTargetCtrl.Add(yoteiKisoSonota)
        End If

    End Sub

    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行う（参照処理用）
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub setIrai1FromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecord)

        'セッションのirai1を削除
        iraiSession.Irai1Data = Nothing

        '画面コントロールに設定
        cboKubun.SelectedValue = jr.Kbn
        hoshouNo.Value = jr.HosyousyoNo
        cboDataHaki.SelectedValue = jr.DataHakiSyubetu
        hakiDate.Value = GetDispStr(jr.DataHakiDate)
        seshuName.Value = jr.SesyuMei
        TextJyutyuuBukkenMei.Value = jr.JyutyuuBukkenMei
        HiddenSesyumeiOld.Value = jr.SesyuMei

        '施主名有無
        RadioSesyumei1.Checked = (jr.SesyuMeiUmu = 1)
        RadioSesyumei0.Checked = (jr.SesyuMeiUmu = 0)

        bukkenJyuusho1.Value = jr.BukkenJyuusyo1
        HiddenJuusyo1.Value = jr.BukkenJyuusyo1
        bukkenJyuusho2.Value = jr.BukkenJyuusyo2
        bukkenJyuusho3.Value = jr.BukkenJyuusyo3
        bikou.Value = jr.Bikou
        bikou2.Value = jr.Bikou2
        Me.TextBunjouCd.Value = cl.GetDisplayString(jr.BunjouCd)
        Me.HiddenBunjouCd.Value = cl.GetDisplayString(jr.BunjouCd)
        Me.TextBukkenNayoseCd.Value = cl.GetDisplayString(jr.BukkenNayoseCd)
        If jr.Keiyu = 0 Or jr.Keiyu = 1 Or jr.Keiyu = 5 Or jr.Keiyu = 9 Then
            keiyu.Value = jr.Keiyu
        Else
            keiyu.Value = 0
        End If
        kashi.Value = jr.KasiUmu
        tyousakaisyaRenraku.Value = jr.TysRenrakusakiAtesakiMei
        tyousakaisyaTel.Value = jr.TysRenrakusakiTel
        tyousakaisyaFax.Value = jr.TysRenrakusakiFax
        tyousakaisyaTantou.Value = jr.IraiTantousyaMei
        tyousakaisyaMailAddr.Value = jr.TysRenrakusakiMail
        iraiDate.Value = GetDispStr(jr.IraiDate)
        keiyakuNo.Value = jr.KeiyakuNo
        chousaKibouDate.Value = GetDispStr(jr.TysKibouDate)
        chousaKibouTime.Value = jr.TysKibouJikan
        If jr.TatiaiUmu = 1 Then
            tatiai.SelectedValue = jr.TatiaiUmu
        Else
            tatiai.SelectedValue = String.Empty
        End If
        cl.SetTatiaiCd(jr.TatiaisyaCd, Me.tatiaiSha_1, Me.tatiaiSha_2, Me.tatiaiSha_3)
        cboKouzou.SelectedValue = IIf(jr.Kouzou = 0, "", jr.Kouzou)
        kouzouSonota.Value = jr.KouzouMemo
        '階層
        If cl.ChkDropDownList(Me.cboKaisou, cl.GetDisplayString(jr.Kaisou)) Then
            cboKaisou.SelectedValue = cl.GetDisplayString(jr.Kaisou)
        Else
            cboKaisou.SelectedIndex = 0
        End If
        cboShintikuTatekae.SelectedValue = IIf(jr.SintikuTatekae = 0, "", jr.SintikuTatekae)
        sijiryoku.Value = Format(jr.SekkeiKyoyouSijiryoku, "#,0.#")
        iraiYoteiTousu.Value = Format(jr.IraiYoteiTousuu, "#,0")
        negiri.Value = Format(jr.NegiriHukasa, "#,0.####")
        morituti.Value = Format(jr.YoteiMoritutiAtusa * 10, "#,0.###")
        cboYoteiKiso.SelectedValue = IIf(jr.YoteiKs = 0, "", jr.YoteiKs)
        yoteiKisoSonota.Value = jr.YoteiKsMemo
        cboSyako.SelectedValue = IIf(jr.Syako = 0, "", jr.Syako)
        shoudakuUmu.Checked = (shoudakuUmu.Value = jr.TenpuHeimenzu)
        shoudakuChousaDate.Value = GetDispStr(jr.SyoudakusyoTysDate)

        CheckYoyakuZumi.Checked = (jr.YoyakuZumiFlg = 1)
        CheckAnnaiZu.Checked = (jr.AnnaiZu = 1)
        CheckHaitiZu.Checked = (jr.HaitiZu = 1)
        CheckKakukaiHeimenZu.Checked = (jr.KakukaiHeimenZu = 1)
        CheckKisoHuseZu.Checked = (jr.KsHuseZu = 1)
        CheckKisoDanmenZu.Checked = (jr.KsDanmenZu = 1)
        CheckZouseiKeikakuZu.Checked = (jr.ZouseiKeikakuZu = 1)

        TextKisoTyakkouYoteiDateFrom.Value = GetDispStr(jr.KsTyakkouYoteiFromDate)
        TextKisoTyakkouYoteiDateTo.Value = GetDispStr(jr.KsTyakkouYoteiToDate)

        HiddenSinkiTourokuMotoKbn.Value = cl.GetDisplayString(jr.SinkiTourokuMotoKbn)

        '処理件数
        HiddenSyoriKensuu.Value = jr.SyoriKensuu

        '更新日付
        updateDateTime.Value = IIf(jr.UpdDatetime = Date.MinValue, "", Format(jr.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))

        Me.HiddenDbUpdDate.Value = IIf(jr.UpdDatetime = Date.MinValue, "", Format(jr.UpdDatetime, EarthConst.FORMAT_DATE_TIME_2))

        '画面表示用 最終更新者、最終更新日時＝＞更新者から取得に変更
        CommonLogic.Instance.SetKousinsya(jr.Kousinsya, lastUpdateUserNm.Value, lastUpdateDateTime.Value)

        '重複チェックボタンの表示設定
        If HiddenSesyumeiOld.Value = String.Empty Or HiddenJuusyo1.Value = String.Empty Then
        Else
            choufukuCheck.Style("display") = "none"
        End If

        'セッションに画面情報を格納
        jSM.Ctrl2Hash(Me, jBn.IraiData)
        iraiSession.Irai1Data = jBn.IraiData

    End Sub

    ''' <summary>
    ''' 受注【確認】での各種処理実行
    ''' <param name="sender">sender</param>
    ''' <param name="exeMode">確認画面からの処理モード</param>
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub exeAtKakunin(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal exeMode As String)
        Dim jR As New JibanRecord

        '登録/編集実行時
        If exeMode = EarthConst.MODE_EXE_TOUROKU Or exeMode = EarthConst.MODE_EXE_PDF_RENRAKU Or _
        exeMode = EarthConst.MODE_EXE_TYOUSAMITSUMORISYO_SAKUSEI Then

            ' 地盤テーブルの初期登録を行う

            jR.Kbn = cboKubun.SelectedValue
            jR.HosyousyoNo = hoshouNo.Value
            SetDispStr(cboDataHaki.SelectedValue, jR.DataHakiSyubetu)
            If cboDataHaki.SelectedValue >= "1" Then
                SetDispStr(hakiDate.Value, jR.DataHakiDate)
            End If
            jR.SesyuMei = seshuName.Value
            jR.JyutyuuBukkenMei = TextJyutyuuBukkenMei.Value
            jR.BukkenJyuusyo1 = bukkenJyuusho1.Value
            jR.BukkenJyuusyo2 = bukkenJyuusho2.Value
            jR.BukkenJyuusyo3 = bukkenJyuusho3.Value
            jR.Bikou = bikou.Value
            jR.Bikou2 = bikou2.Value
            cl.SetDisplayString(Me.TextBunjouCd.Value, jR.BunjouCd)
            If Me.TextBukkenNayoseCd.Value = String.Empty Then
                jR.BukkenNayoseCdFlg = True 'ロジッククラスでセット
            Else
                jR.BukkenNayoseCdFlg = False
                jR.BukkenNayoseCd = Me.TextBukkenNayoseCd.Value.ToUpper  '画面.物件名寄コード
            End If
            jR.Keiyu = IIf(keiyu.Value = " ", 0, keiyu.Value)
            jR.KasiUmu = kashi.Value
            jR.TysRenrakusakiAtesakiMei = tyousakaisyaRenraku.Value
            jR.TysRenrakusakiTel = tyousakaisyaTel.Value
            jR.TysRenrakusakiFax = tyousakaisyaFax.Value
            jR.IraiTantousyaMei = tyousakaisyaTantou.Value
            jR.TysRenrakusakiMail = tyousakaisyaMailAddr.Value
            SetDispStr(iraiDate.Value, jR.IraiDate)
            jR.KeiyakuNo = keiyakuNo.Value
            SetDispStr(chousaKibouDate.Value, jR.TysKibouDate)
            jR.TysKibouJikan = chousaKibouTime.Value
            SetDispStr(tatiai.SelectedValue, jR.TatiaiUmu)
            SetDispStr(cl.GetTatiaiCd(Me.tatiaiSha_1, Me.tatiaiSha_2, Me.tatiaiSha_3), jR.TatiaisyaCd)
            SetDispStr(cboKouzou.SelectedValue, jR.Kouzou)
            jR.KouzouMemo = kouzouSonota.Value
            '更新日時
            If Me.HiddenDbUpdDate.Value <> String.Empty Then
                jR.UpdDatetime = Me.HiddenDbUpdDate.Value
            End If
            SetDispStr(cboKaisou.SelectedValue, jR.Kaisou)
            SetDispStr(cboShintikuTatekae.SelectedValue, jR.SintikuTatekae)
            SetDispStr(sijiryoku.Value, jR.SekkeiKyoyouSijiryoku)
            SetDispStr(iraiYoteiTousu.Value, jR.IraiYoteiTousuu)
            SetDispStr(negiri.Value, jR.NegiriHukasa)
            If morituti.Value <> String.Empty Then
                SetDispStr(morituti.Value / 10, jR.YoteiMoritutiAtusa)
            Else
                SetDispStr(morituti.Value, jR.YoteiMoritutiAtusa)
            End If
            SetDispStr(cboYoteiKiso.SelectedValue, jR.YoteiKs)
            jR.YoteiKsMemo = yoteiKisoSonota.Value
            SetDispStr(cboSyako.SelectedValue, jR.Syako)
            SetDispStr(IIf(shoudakuUmu.Checked, shoudakuUmu.Value, 0), jR.TenpuHeimenzu)
            SetDispStr(shoudakuChousaDate.Value, jR.SyoudakusyoTysDate)

            SetDispStr(IIf(CheckYoyakuZumi.Checked, 1, 0), jR.YoyakuZumiFlg)

            SetDispStr(IIf(CheckAnnaiZu.Checked, 1, 0), jR.AnnaiZu)
            SetDispStr(IIf(CheckHaitiZu.Checked, 1, 0), jR.HaitiZu)
            SetDispStr(IIf(CheckKakukaiHeimenZu.Checked, 1, 0), jR.KakukaiHeimenZu)
            SetDispStr(IIf(CheckKisoHuseZu.Checked, 1, 0), jR.KsHuseZu)
            SetDispStr(IIf(CheckKisoDanmenZu.Checked, 1, 0), jR.KsDanmenZu)
            SetDispStr(IIf(CheckZouseiKeikakuZu.Checked, 1, 0), jR.ZouseiKeikakuZu)

            SetDispStr(TextKisoTyakkouYoteiDateFrom.Value, jR.KsTyakkouYoteiFromDate)
            SetDispStr(TextKisoTyakkouYoteiDateTo.Value, jR.KsTyakkouYoteiToDate)

            '施主名有無
            If RadioSesyumei0.Checked Then '無
                SetDispStr(0, jR.SesyuMeiUmu)
            ElseIf RadioSesyumei1.Checked Then '有
                SetDispStr(1, jR.SesyuMeiUmu)
            Else
                SetDispStr(Integer.MinValue, jR.SesyuMeiUmu)
            End If

            ' ReportIf 設定用
            SetDispStr(cboKouzou.SelectedItem.Text, jR.KouzouMeiIf)

            SetDispStr(IIf(HiddenSyoriKensuu.Value = "", 0, HiddenSyoriKensuu.Value), jR.SyoriKensuu) '処理件数
            SetDispStr(HiddenRentouBukkenSuu.Value, jR.RentouBukkenSuu) '連棟物件数

            iraiSession.JibanData = jR

        End If

        Me.AccHiddenInsTokubetuTaiouFlg.Value = String.Empty

        '新規引継実行時
        If exeMode = EarthConst.MODE_EXE_HIKITUGI Then
            '採番処理完了フラグ初期化
            saibanOkFlg = False
            hoshoushoNoSaiban_ServerClick(sender, e)
            If saibanOkFlg Then
                '採番OKの場合
                HiddenSesyumeiOld.Value = String.Empty
                HiddenJuusyo1.Value = String.Empty
                choufukuKakuninFlg1.Value = Boolean.FalseString
                choufukuKakuninFlg2.Value = Boolean.FalseString
                iraiST.Value = EarthConst.MODE_NEW
                iraiSession.IraiST = EarthConst.MODE_NEW
                Me.TextBunjouCd.Value = String.Empty
                Me.CheckBunjouCdSaiban.Checked = False
                Me.TextBukkenNayoseCd.Value = String.Empty
                btn_irai1_act_ServerClick(sender, e)
            End If
        End If

        '新規連続実行時
        If exeMode = EarthConst.MODE_EXE_RENZOKU Then
            '採番処理完了フラグ初期化
            saibanOkFlg = False
            hoshoushoNoSaiban_ServerClick(sender, e)
            If saibanOkFlg Then
                '採番OKの場合
                iraiST.Value = EarthConst.MODE_NEW
                iraiSession.IraiST = EarthConst.MODE_NEW
                iraiSession.Irai1Data = Nothing
                iraiSession.Irai2Data = Nothing
                'セッションに画面表示値を格納
                iraiSession.Irai1Data = New Hashtable
                iraiSession.Irai1Data.Add(iraiST.ClientID, iraiST)
                iraiSession.Irai1Data.Add(cboKubun.ClientID, cboKubun)
                iraiSession.Irai1Data.Add(hoshouNo.ClientID, hoshouNo)
                iraiSession.Irai1Data.Add(HiddenRentouBukkenSuu.ClientID, HiddenRentouBukkenSuu)
                iraiSession.Irai1Data.Add(Me.HiddenInsTokubetuTaiouFlg.ClientID, Me.HiddenInsTokubetuTaiouFlg)
                btn_irai1_act_ServerClick(sender, e)
            End If
        End If

        '名寄物件作成ボタン実行時
        If exeMode = EarthConst.MODE_EXE_COPY Then
            '採番処理完了フラグ初期化
            saibanOkFlg = False
            hoshoushoNoSaiban_ServerClick(sender, e)
            If saibanOkFlg Then
                '採番OKの場合
                iraiST.Value = EarthConst.MODE_NEW
                iraiSession.IraiST = EarthConst.MODE_NEW
                iraiSession.Irai2Data = Nothing
                Irai2DataStr.Value = String.Empty
                iraiSession.Irai2DataStr = Nothing
                HiddenSesyumeiOld.Value = String.Empty
                HiddenJuusyo1.Value = String.Empty
                choufukuKakuninFlg1.Value = Boolean.FalseString
                choufukuKakuninFlg2.Value = Boolean.FalseString
                iraiST.Value = EarthConst.MODE_NEW
                iraiSession.IraiST = EarthConst.MODE_NEW
                Me.cboDataHaki.SelectedValue = "0"
                Me.hakiDate.Value = String.Empty
                btn_irai1_act_ServerClick(sender, e)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 画面表示用文字列に変換するファンクション（オーバーライド）
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDispStr(ByVal str As Object) As String

        Return cl.GetDisplayString(str)

    End Function

    ''' <summary>
    ''' 画面表示用文字列を特定の型に変換するファンクション（オーバーライド）
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDispStr(ByVal str As Object, ByRef retObj As Object) As Boolean

        If cl.SetDisplayString(str, retObj) Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' 分譲コード採番チェックボックス変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonBunjouSaiban_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim blnChecked As Boolean = Me.CheckBunjouCdSaiban.Checked
        Dim strBunjouCd As String = Me.TextBunjouCd.Value
        Dim intBunjouCd As Integer

        'チェックを外した場合は、データ読込み時の分譲コードに戻す
        If Not blnChecked Then
            cl.chgDispSyouhinText(Me.TextBunjouCd)
            Me.TextBunjouCd.Value = Me.HiddenBunjouCd.Value
            Exit Sub
        End If

        '既に採番済みの場合は、その採番済みの分譲コードを使用する
        If Me.HiddenSaibanNo.Value <> String.Empty Then
            Me.TextBunjouCd.Value = Me.HiddenSaibanNo.Value
            '分譲コードをラベル化
            cl.chgVeiwMode(Me.TextBunjouCd)
            Exit Sub
        End If

        '分譲コードを採番して取得
        intBunjouCd = MyLogic.getCntUpBunjouCd(sender, user_info.LoginUserId)

        '分譲コードをセット
        If intBunjouCd = Integer.MinValue Then
            Me.CheckBunjouCdSaiban.Checked = False
        Else
            Me.TextBunjouCd.Value = intBunjouCd.ToString
            Me.HiddenSaibanNo.Value = intBunjouCd.ToString
            '分譲コードをラベル化
            cl.chgVeiwMode(Me.TextBunjouCd)
        End If
    End Sub
End Class