
Partial Public Class NyuukinTorikomiSyuusei
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Private cl As New CommonLogic
    Private sl As New StringLogic
    ''' <summary>
    ''' メッセージクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private MLogic As New MessageLogic

#Region "入金ファイル取込・コントロール値"
    'タイトル
    Private Const CTRL_VALUE_TITLE As String = "入金取込データ"
    Private Const CTRL_VALUE_TITLE_KOUSIN As String = CTRL_VALUE_TITLE & EarthConst.GAMEN_MODE_EDIT
    Private Const CTRL_VALUE_TITLE_TOUROKU As String = CTRL_VALUE_TITLE & EarthConst.GAMEN_MODE_NEW
    Private Const CTRL_VALUE_TITLE_KAKUNIN As String = CTRL_VALUE_TITLE & EarthConst.GAMEN_MODE_VIEW
#End Region

#Region "プロパティ"

#Region "パラメータ/入金ファイル取込検索画面"

    ''' <summary>
    ''' 入金取込ユニークNO
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _NyuukinTorikomiNo As String
    ''' <summary>
    ''' 入金取込ユニークNO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrNkTkNo() As String
        Get
            Return _NyuukinTorikomiNo
        End Get
        Set(ByVal value As String)
            _NyuukinTorikomiNo = value
        End Set
    End Property
    ''' <summary>
    ''' 画面モード
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _GamenMode As String
    ''' <summary>
    ''' 入力NO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrGamenMode() As String
        Get
            Return _GamenMode
        End Get
        Set(ByVal value As String)
            _GamenMode = value
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

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合
            cl.CloseWindow(Me)
            Me.ButtonUpdate.Style("display") = "none"
            Exit Sub
        End If

        If IsPostBack = False Then '初期起動時

            '●パラメータのチェック
            Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
            ' Key情報を保持
            pStrNkTkNo = arrSearchTerm(0)

            ' パラメータ不足時は閉じる
            If pStrNkTkNo Is Nothing OrElse pStrNkTkNo = String.Empty Then
                cl.CloseWindow(Me)
                Me.ButtonUpdate.Style("display") = "none"
                Exit Sub
            End If

            '●権限のチェック
            '以下のいずれかの権限がない場合、画面参照のみ
            '経理業務権限
            If userinfo.KeiriGyoumuKengen = 0 Then
                cl.CloseWindow(Me)
                Me.ButtonUpdate.Style("display") = "none"
                Exit Sub
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            Dim helper As New DropDownHelper

            ' 請求先区分コンボにデータをバインドする
            helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            Me.SetDispAction()

            'ボタン押下イベントの設定
            Me.setBtnEvent()

            '****************************************************************************
            ' 入金取込データ取得
            '****************************************************************************
            Me.SetCtrlFromDataRec(sender, e)

            Me.ButtonClose.Focus() 'フォーカス

        End If

    End Sub

    ''' <summary>
    ''' 入金取込レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object, ByVal e As System.EventArgs)
        ' 画面内容より入金取込レコードを生成する
        Dim dataRec As New NyuukinFileTorikomiRecord
        Dim logic As New NyuukinTorikomiLogic

        dataRec = logic.getNyuukinFileTorikomiList(sender, pStrNkTkNo)

        '入金取込NO
        Me.TextNyuukinTorikomiNo.Text = cl.GetDisplayString(dataRec.NyuukinTorikomiUniqueNo)
        'EDI情報作成日
        Me.TextEdiJouhouSakuseiDate.Text = cl.GetDisplayString(dataRec.EdiJouhouSakuseiDate)
        '取消
        If cl.GetDisplayString(dataRec.Torikesi) = "0" Then
            Me.CheckTorikesi.Checked = False
        ElseIf cl.GetDisplayString(dataRec.Torikesi) = "1" Then
            Me.CheckTorikesi.Checked = True
        Else
            Me.CheckTorikesi.Checked = False
        End If
        '入金日
        Me.TextNyuukinDate.Text = cl.GetDisplayString(dataRec.NyuukinDate)
        '取込伝票番号
        Me.TextTorikomiDenpyouNo.Text = cl.GetDisplayString(dataRec.TorikomiDenpyouNo)
        '請求先区分
        Me.SelectSeikyuuSakiKbn.SelectedValue = cl.GetDisplayString(dataRec.SeikyuuSakiKbn)
        '請求先コード
        Me.TextSeikyuuSakiCd.Text = cl.GetDisplayString(dataRec.SeikyuuSakiCd)
        '請求先枝番
        Me.TextSeikyuuSakiBrc.Text = cl.GetDisplayString(dataRec.SeikyuuSakiBrc)
        '請求先名
        Me.TextSeikyuuSakiMei.Text = cl.GetDisplayString(dataRec.SeikyuuSakiMei)
        '照合口座No.
        Me.TextSyougouKouzaNo.Text = cl.GetDisplayString(dataRec.SyougouKouzaNo)

        '**********
        '*入金額
        '**********
        '現金
        Me.TextNkGenkin.Text = IIf(dataRec.Genkin = Long.MinValue, "0", Format(dataRec.Genkin, EarthConst.FORMAT_KINGAKU_2))
        '小切手
        Me.TextNkKogitte.Text = IIf(dataRec.Kogitte = Long.MinValue, "0", Format(dataRec.Kogitte, EarthConst.FORMAT_KINGAKU_2))
        '振込
        Me.TextNkFurikomi.Text = IIf(dataRec.Furikomi = Long.MinValue, "0", Format(dataRec.Furikomi, EarthConst.FORMAT_KINGAKU_2))
        '手形
        Me.TextNkTegata.Text = IIf(dataRec.Tegata = Long.MinValue, "0", Format(dataRec.Tegata, EarthConst.FORMAT_KINGAKU_2))
        '相殺
        Me.TextNkSousai.Text = IIf(dataRec.Sousai = Long.MinValue, "0", Format(dataRec.Sousai, EarthConst.FORMAT_KINGAKU_2))
        '値引
        Me.TextNkNebiki.Text = IIf(dataRec.Nebiki = Long.MinValue, "0", Format(dataRec.Nebiki, EarthConst.FORMAT_KINGAKU_2))
        'その他
        Me.TextNkSonota.Text = IIf(dataRec.Sonota = Long.MinValue, "0", Format(dataRec.Sonota, EarthConst.FORMAT_KINGAKU_2))
        '協力会費
        Me.TextNkKyouryokuKaihi.Text = IIf(dataRec.KyouryokuKaihi = Long.MinValue, "0", Format(dataRec.KyouryokuKaihi, EarthConst.FORMAT_KINGAKU_2))
        '口座振替
        Me.TextNkKouzaFurikae.Text = IIf(dataRec.KouzaFurikae = Long.MinValue, "0", Format(dataRec.KouzaFurikae, EarthConst.FORMAT_KINGAKU_2))
        '振込手数料
        Me.TextNkFurikomiTesuuryou.Text = IIf(dataRec.FurikomiTesuuryou = Long.MinValue, "0", Format(dataRec.FurikomiTesuuryou, EarthConst.FORMAT_KINGAKU_2))

        '手形期日
        Me.TextTegataKijitu.Text = cl.GetDisplayString(dataRec.TegataKijitu)
        '手形No.
        Me.TextTegataNo.Text = cl.GetDisplayString(dataRec.TegataNo)
        '摘要名
        Me.TextAreaTekiyou.Value = cl.GetDisplayString(dataRec.TekiyouMei)

        '取消理由
        '赤色文字変更対応を配列に格納
        Dim objChgColor As Object() = New Object() {Me.SelectSeikyuuSakiKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakiMei, Me.TextTorikesiRiyuu}
        '取消理由取得設定と色替処理
        cl.GetKameitenTorikesiRiyuuMain(Me.SelectSeikyuuSakiKbn.SelectedValue, Me.TextSeikyuuSakiCd.Text, Me.TextTorikesiRiyuu, True, False, objChgColor)

        '************
        '* Hidden項目
        '************
        '更新日時
        Me.HiddenUpdDatetime.Value = IIf(dataRec.UpdDatetime = Date.MinValue, "", Format(dataRec.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
        '請求先コード
        Me.HiddenSeikyuuSakiCdOld.Value = Me.TextSeikyuuSakiCd.Text
        '請求先枝番
        Me.HiddenSeikyuuSakiBrcOld.Value = Me.TextSeikyuuSakiBrc.Text
        '請求先区分
        Me.HiddenSeikyuuSakiKbnOld.Value = Me.SelectSeikyuuSakiKbn.SelectedValue

        '画面表示時点の値を、Hiddenに保持(変更チェック用)
        If Me.HiddenOpenValues.Value = String.Empty Then
            Me.HiddenOpenValues.Value = Me.getCtrlValuesString()
        End If

        '画面モード別設定
        Me.SetGamenMode()

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispAction()
        Dim jBn As New Jiban '地盤画面クラス

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript関連セット
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim onFocusPostBackScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScript As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this)){__doPostBack(this.id,'');}}else{checkNumberAddFig(this);}"
        Dim checkDate As String = "checkDate(this)"
        Dim checkKingaku As String = "checkKingaku(this)"
        Dim checkNumber As String = "checkNumber(this)"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"
        Dim onFocusScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurScript As String = "if(checkTempValueForOnBlur(this)){checkNumberAddFig(this);CalcTotalGaku();}else{checkNumberAddFig(this);}"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 請求先コード、請求先枝番および請求先区分
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.SelectSeikyuuSakiKbn.Attributes("onchange") = "clrSeikyuuSakiInfo();"
        Me.TextSeikyuuSakiCd.Attributes("onchange") = "clrSeikyuuSakiInfo();"
        Me.TextSeikyuuSakiBrc.Attributes("onchange") = "clrSeikyuuSakiInfo();"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 金額系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '***********************
        '* 入金額
        '***********************
        '現金
        Me.TextNkGenkin.Attributes("onfocus") = onFocusScript
        Me.TextNkGenkin.Attributes("onblur") = onBlurScript
        '小切手
        Me.TextNkKogitte.Attributes("onfocus") = onFocusScript
        Me.TextNkKogitte.Attributes("onblur") = onBlurScript
        '振込
        Me.TextNkFurikomi.Attributes("onfocus") = onFocusScript
        Me.TextNkFurikomi.Attributes("onblur") = onBlurScript
        '手形
        Me.TextNkTegata.Attributes("onfocus") = onFocusScript
        Me.TextNkTegata.Attributes("onblur") = onBlurScript
        '相殺
        Me.TextNkSousai.Attributes("onfocus") = onFocusScript
        Me.TextNkSousai.Attributes("onblur") = onBlurScript
        '値引
        Me.TextNkNebiki.Attributes("onfocus") = onFocusScript
        Me.TextNkNebiki.Attributes("onblur") = onBlurScript
        'その他
        Me.TextNkSonota.Attributes("onfocus") = onFocusScript
        Me.TextNkSonota.Attributes("onblur") = onBlurScript
        '協力会費
        Me.TextNkKyouryokuKaihi.Attributes("onfocus") = onFocusScript
        Me.TextNkKyouryokuKaihi.Attributes("onblur") = onBlurScript
        '口座振替
        Me.TextNkKouzaFurikae.Attributes("onfocus") = onFocusScript
        Me.TextNkKouzaFurikae.Attributes("onblur") = onBlurScript
        '振込手数料
        Me.TextNkFurikomiTesuuryou.Attributes("onfocus") = onFocusScript
        Me.TextNkFurikomiTesuuryou.Attributes("onblur") = onBlurScript

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 日付系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '入金日
        Me.TextNyuukinDate.Attributes("onblur") = checkDate
        '手形期日
        Me.TextTegataKijitu.Attributes("onblur") = checkDate

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 数値系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '照合口座No.
        Me.TextSyougouKouzaNo.Attributes("onblur") = checkNumber
        '手形No.
        Me.TextTegataNo.Attributes("onblur") = checkNumber

        '伝票番号
        Me.TextTorikomiDenpyouNo.Attributes("onblur") = "if(checkNumber(this)) this.value = paddingStr(this.value, 6, '0');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)

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
        Dim tmpScriptOverLay As String = "setWindowOverlay(this,null,1);"
        Dim tmpTouroku As String = "if(confirm('" & Messages.MSG017C & "')){" & tmpScriptOverLay & "}else{return false;}"

        '登録許可MSG確認後、OKの場合DB更新処理を行なう
        Me.ButtonUpdate.Attributes("onclick") = tmpTouroku

    End Sub

#Region "プライベートメソッド"

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
    ''' 画面の編集状況を確認
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkInputEditJyky(ByRef errMess As String, ByRef arrFocusTargetCtrl As ArrayList)

        '月次処理確定年月
        Dim dtGetujiKakuteiLastSyoriDate As DateTime = DateTime.MinValue

        'DB読み込み時点の値を、現在画面の値と比較(変更有無チェック)
        If Me.TextNyuukinTorikomiNo.Text <> String.Empty AndAlso Me.HiddenOpenValues.Value <> String.Empty Then
            'DB読み込み時点の値が空の場合は比較対象外
            '比較実施
            If Me.HiddenOpenValues.Value <> Me.getCtrlValuesString() Then
                '月次確定予約済みの処理年月「以前」の日付で入金日を設定するのはエラー
                If cl.chkGetujiKakuteiYoyakuzumi(Me.TextNyuukinDate.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                    errMess += String.Format(Messages.MSG176E, dtGetujiKakuteiLastSyoriDate.Month, "入金日", dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                    arrFocusTargetCtrl.Add(Me.TextNyuukinDate)
                End If
            End If
        End If

        '新規登録時のチェック
        If Me.TextNyuukinTorikomiNo.Text = String.Empty Then
            '月次確定予約済みの処理年月「以前」の日付で入金日を設定するのはエラー
            If cl.chkGetujiKakuteiYoyakuzumi(Me.TextNyuukinDate.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                errMess += String.Format(Messages.MSG176E, dtGetujiKakuteiLastSyoriDate.Month, "入金日", dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                arrFocusTargetCtrl.Add(Me.TextNyuukinDate)
            End If
        End If

    End Sub

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <remarks>
    ''' コード入力値変更チェック<br/>
    ''' 必須チェック<br/>
    ''' 禁則文字チェック(文字列入力フィールドが対象)<br/>
    ''' バイト数チェック(文字列入力フィールドが対象)<br/>
    ''' 桁数チェック<br/>
    ''' 日付チェック<br/>
    ''' その他チェック<br/>
    ''' </remarks>
    Public Function checkInput() As Boolean
        '地盤画面共通クラス
        Dim jBn As New Jiban

        '全てのコントロールを有効化(破棄種別チェック用)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        'エラーメッセージ初期化
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList

        Dim blnMasterChk As Boolean = True

        '編集状況をチェック
        Me.checkInputEditJyky(errMess, arrFocusTargetCtrl)

        '●コード入力値変更チェック
        '請求先コード、請求先枝番、請求先区分
        If Me.SelectSeikyuuSakiKbn.SelectedValue <> Me.HiddenSeikyuuSakiKbnOld.Value _
            Or Me.TextSeikyuuSakiCd.Text <> Me.HiddenSeikyuuSakiCdOld.Value _
            Or Me.TextSeikyuuSakiBrc.Text <> Me.HiddenSeikyuuSakiBrcOld.Value Then

            errMess += Messages.MSG030E.Replace("@PARAM1", "請求先")
            arrFocusTargetCtrl.Add(Me.ButtonSearchSeikyuuSaki)

            blnMasterChk = False 'フラグをたてる
        End If

        '●必須チェック
        '入金日
        If Me.TextNyuukinDate.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "入金日")
            arrFocusTargetCtrl.Add(Me.TextNyuukinDate)
        End If
        '取込伝票番号
        If Me.TextTorikomiDenpyouNo.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "取込伝票番号")
            arrFocusTargetCtrl.Add(Me.TextTorikomiDenpyouNo)
        End If
        '請求先区分
        If Me.SelectSeikyuuSakiKbn.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "請求先区分")
            arrFocusTargetCtrl.Add(Me.SelectSeikyuuSakiKbn)
        End If
        '請求先コード
        If Me.TextSeikyuuSakiCd.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "請求先コード")
            arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiCd)
        End If
        '請求先枝番
        If Me.TextSeikyuuSakiBrc.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "請求先枝番")
            arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiBrc)
        End If
        '照合口座No.
        If Me.TextSyougouKouzaNo.Text = String.Empty Then
            errMess += Messages.MSG013E.Replace("@PARAM1", "照合口座No.")
            arrFocusTargetCtrl.Add(Me.TextSyougouKouzaNo)
        End If

        '●日付チェック
        '入金日
        If Me.TextNyuukinDate.Text <> String.Empty Then
            If cl.checkDateHanni(Me.TextNyuukinDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "入金日")
                arrFocusTargetCtrl.Add(Me.TextNyuukinDate)
            End If
        End If
        '手形期日
        If Me.TextTegataKijitu.Text <> String.Empty Then
            If cl.checkDateHanni(Me.TextTegataKijitu.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "手形期日")
                arrFocusTargetCtrl.Add(Me.TextTegataKijitu)
            End If
        End If

        '●コード桁数チェック
        '取込伝票番号
        If Me.TextTorikomiDenpyouNo.Text <> String.Empty Then
            If jBn.CodeByteCheckSJIS(Me.TextTorikomiDenpyouNo.Text, Me.TextTorikomiDenpyouNo.MaxLength) = False Then
                errMess += Messages.MSG027E.Replace("@PARAM1", "取込伝票番号").Replace("@PARAM2", "6").Replace("@PARAM3", "0")
                arrFocusTargetCtrl.Add(Me.TextTorikomiDenpyouNo)
            End If
        End If

        '●禁則文字チェック(文字列入力フィールドが対象)
        '取込伝票番号
        If jBn.KinsiStrCheck(Me.TextTorikomiDenpyouNo.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "取込伝票番号")
            arrFocusTargetCtrl.Add(Me.TextTorikomiDenpyouNo)
        End If
        '請求先名
        If jBn.KinsiStrCheck(Me.TextSeikyuuSakiMei.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "請求先名")
            arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiMei)
        End If
        '照合口座No.
        If jBn.KinsiStrCheck(Me.TextSyougouKouzaNo.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "照合口座No.")
            arrFocusTargetCtrl.Add(Me.TextSyougouKouzaNo)
        End If
        '摘要名
        If jBn.KinsiStrCheck(Me.TextAreaTekiyou.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "摘要名")
            arrFocusTargetCtrl.Add(Me.TextAreaTekiyou)
        End If

        '●バイト数チェック(文字列入力フィールドが対象)
        '取込伝票番号
        If jBn.ByteCheckSJIS(Me.TextTorikomiDenpyouNo.Text, Me.TextTorikomiDenpyouNo.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "取込伝票番号")
            arrFocusTargetCtrl.Add(Me.TextTorikomiDenpyouNo)
        End If
        '請求先名
        If jBn.ByteCheckSJIS(Me.TextSeikyuuSakiMei.Text, Me.TextSeikyuuSakiMei.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "請求先名")
            arrFocusTargetCtrl.Add(Me.TextSeikyuuSakiMei)
        End If
        '照合口座No.
        If jBn.ByteCheckSJIS(Me.TextSyougouKouzaNo.Text, Me.TextSyougouKouzaNo.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "照合口座No.")
            arrFocusTargetCtrl.Add(Me.TextSyougouKouzaNo)
        End If
        '摘要名
        If jBn.ByteCheckSJIS(Me.TextAreaTekiyou.Value, 255) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "摘要名")
            arrFocusTargetCtrl.Add(Me.TextAreaTekiyou)
        End If

        '適用名の改行をスペースに置換
        sl.ChgVbCrlfToStr(Me.TextAreaTekiyou.Value)

        '●その他チェック
        '※請求先マスタの存在チェック
        '請求先区分、請求先コード、請求先枝番が入力されている場合
        If Me.SelectSeikyuuSakiKbn.SelectedValue <> String.Empty _
            And Me.TextSeikyuuSakiCd.Text <> String.Empty _
            And Me.TextSeikyuuSakiBrc.Text <> String.Empty Then

            'コード入力値エラー時は以下のチェックは行わない
            If blnMasterChk Then
                Dim udsLogic As New UriageDataSearchLogic
                '検索条件に沿った請求先のレコードをすべて取得します
                Dim list As List(Of SeikyuuSakiInfoRecord)
                '実検索件数格納用
                list = udsLogic.GetSeikyuuSakiInfo(Me.TextSeikyuuSakiCd.Text, _
                                                   Me.TextSeikyuuSakiBrc.Text, _
                                                   Me.SelectSeikyuuSakiKbn.SelectedValue, _
                                                   True _
                                                   )

                ' 検索結果1件の場合
                If list.Count = 1 Then
                Else
                    errMess += Messages.MSG139E _
                            & "[請求先区分]" & Me.SelectSeikyuuSakiKbn.SelectedItem.Text & "\r\n" _
                            & "[請求先コード]" & Me.TextSeikyuuSakiCd.Text & "\r\n" _
                            & "[請求先枝番]" & Me.TextSeikyuuSakiBrc.Text
                    arrFocusTargetCtrl.Add(Me.SelectSeikyuuSakiKbn)
                End If

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
            Return False

        End If

        'エラー無しの場合、Trueを返す
        Return True
    End Function

    ''' <summary>
    ''' 画面の内容をDBに反映する
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function SaveData() As Boolean
        '*************************
        '取消時、必須項目と請求先名のみ更新
        '上記以外は全て更新
        '*************************
        Dim logic As New NyuukinTorikomiLogic
        Dim dataRec As New NyuukinFileTorikomiRecord

        '各行ごとに画面からレコードクラスに入れ込み
        dataRec = Me.GetCtrlToDataRec()

        ' データの更新を行います
        If logic.saveData(Me, dataRec) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 画面の各情報をレコードクラスに取得し、入金取込レコードクラスを返却する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCtrlToDataRec() As NyuukinFileTorikomiRecord
        ' 画面内容より入金取込レコードを生成する
        Dim dataRec As New NyuukinFileTorikomiRecord

        '***************************************
        ' 入金取込データ
        '***************************************
        ' 入金取込NO
        cl.SetDisplayString(Me.TextNyuukinTorikomiNo.Text, dataRec.NyuukinTorikomiUniqueNo)
        ' EDI情報作成日
        cl.SetDisplayString(Me.TextEdiJouhouSakuseiDate.Text, dataRec.EdiJouhouSakuseiDate)
        ' 取消
        If Me.CheckTorikesi.Checked Then
            dataRec.Torikesi = 1
        Else
            dataRec.Torikesi = 0
        End If
        ' 入金日
        cl.SetDisplayString(Me.TextNyuukinDate.Text, dataRec.NyuukinDate)
        ' 取込伝票番号
        cl.SetDisplayString(Me.TextTorikomiDenpyouNo.Text, dataRec.TorikomiDenpyouNo)
        ' 請求先区分
        cl.SetDisplayString(Me.SelectSeikyuuSakiKbn.SelectedValue, dataRec.SeikyuuSakiKbn)
        ' 請求先コード
        cl.SetDisplayString(Me.TextSeikyuuSakiCd.Text, dataRec.SeikyuuSakiCd)
        ' 請求先枝番
        cl.SetDisplayString(Me.TextSeikyuuSakiBrc.Text, dataRec.SeikyuuSakiBrc)
        ' 請求先名
        cl.SetDisplayString(Me.TextSeikyuuSakiMei.Text, dataRec.SeikyuuSakiMei)
        ' 照合口座No.
        cl.SetDisplayString(Me.TextSyougouKouzaNo.Text, dataRec.SyougouKouzaNo)

        ' *********
        ' * 入金額
        ' *********
        ' 現金
        cl.SetDisplayString(Me.TextNkGenkin.Text, dataRec.Genkin)
        ' 小切手
        cl.SetDisplayString(Me.TextNkKogitte.Text, dataRec.Kogitte)
        ' 振込
        cl.SetDisplayString(Me.TextNkFurikomi.Text, dataRec.Furikomi)
        ' 手形
        cl.SetDisplayString(Me.TextNkTegata.Text, dataRec.Tegata)
        ' 相殺
        cl.SetDisplayString(Me.TextNkSousai.Text, dataRec.Sousai)
        ' 値引
        cl.SetDisplayString(Me.TextNkNebiki.Text, dataRec.Nebiki)
        ' その他
        cl.SetDisplayString(Me.TextNkSonota.Text, dataRec.Sonota)
        ' 協力会費
        cl.SetDisplayString(Me.TextNkKyouryokuKaihi.Text, dataRec.KyouryokuKaihi)
        ' 口座振替
        cl.SetDisplayString(Me.TextNkKouzaFurikae.Text, dataRec.KouzaFurikae)
        ' 振込手数料
        cl.SetDisplayString(Me.TextNkFurikomiTesuuryou.Text, dataRec.FurikomiTesuuryou)

        ' 手形期日
        cl.SetDisplayString(Me.TextTegataKijitu.Text, dataRec.TegataKijitu)
        ' 手形No.
        cl.SetDisplayString(Me.TextTegataNo.Text, dataRec.TegataNo)
        ' 摘要名
        cl.SetDisplayString(Me.TextAreaTekiyou.Value, dataRec.TekiyouMei)

        ' 更新者ユーザーID
        dataRec.UpdLoginUserId = userinfo.LoginUserId
        ' 更新日時 読み込み時のタイムスタンプ
        If Me.HiddenUpdDatetime.Value = "" Then
            dataRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
        Else
            dataRec.UpdDatetime = DateTime.ParseExact(Me.HiddenUpdDatetime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
        End If

        Return dataRec
    End Function

    ''' <summary>
    ''' 画面モードを設定する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetGamenMode()
        '新規の場合
        If Me.pStrNkTkNo = "0" Then
            pStrGamenMode = CStr(EarthEnum.emGamenMode.SINKI)
        ElseIf IsNumeric(Me.pStrNkTkNo) AndAlso Integer.Parse(Me.pStrNkTkNo) > 0 Then '更新の場合
            pStrGamenMode = CStr(EarthEnum.emGamenMode.KOUSIN)
        Else '上記以外
            pStrGamenMode = CStr(EarthEnum.emGamenMode.KAKUNIN)
        End If

        'Hiddenに退避
        Me.HiddenGamenMode.Value = pStrGamenMode

        '画面設定
        Me.SetDispControl()

    End Sub

    ''' <summary>
    ''' 画面モード別の画面設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispControl()
        'Hiddenより再設定
        pStrGamenMode = Me.HiddenGamenMode.Value

        If pStrGamenMode = CStr(EarthEnum.emGamenMode.KAKUNIN) Then '確認
            cl.CloseWindow(Me)
            Me.ButtonUpdate.Style("display") = "none"
            Exit Sub

        ElseIf pStrGamenMode = CStr(EarthEnum.emGamenMode.SINKI) Then '新規登録
            'タイトル
            Me.SpanTitle.InnerHtml = CTRL_VALUE_TITLE_TOUROKU
            'ウィンドウタイトルバー
            Me.Title = EarthConst.SYS_NAME & EarthConst.BRANK_STRING & CTRL_VALUE_TITLE_TOUROKU
            '入金取込NO
            pStrNkTkNo = String.Empty '値クリア
            '更新ボタン
            Me.ButtonUpdate.Value = EarthConst.GAMEN_MODE_NEW

        ElseIf pStrGamenMode = CStr(EarthEnum.emGamenMode.KOUSIN) Then '修正実行
            'タイトル
            Me.SpanTitle.InnerHtml = CTRL_VALUE_TITLE_KOUSIN
            'ウィンドウタイトルバー
            Me.Title = EarthConst.SYS_NAME & EarthConst.BRANK_STRING & CTRL_VALUE_TITLE_KOUSIN
            '更新ボタン
            Me.ButtonUpdate.Value = EarthConst.GAMEN_MODE_EDIT
        Else
            cl.CloseWindow(Me)
            Me.ButtonUpdate.Style("display") = "none"
            Exit Sub
        End If
    End Sub


#Region "画面コントロールの値を結合し、文字列化する"
    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesString() As String

        Dim sb As New StringBuilder

        '取消
        sb.Append(Me.CheckTorikesi.Checked.ToString & EarthConst.SEP_STRING)
        '請求先コード
        sb.Append(Me.TextSeikyuuSakiCd.Text & EarthConst.SEP_STRING)
        '請求先枝番
        sb.Append(Me.TextSeikyuuSakiBrc.Text & EarthConst.SEP_STRING)
        '請求先区分
        sb.Append(Me.SelectSeikyuuSakiKbn.SelectedValue & EarthConst.SEP_STRING)
        '照合口座No.
        sb.Append(Me.TextSyougouKouzaNo.Text & EarthConst.SEP_STRING)
        '入金日
        sb.Append(Me.TextNyuukinDate.Text & EarthConst.SEP_STRING)
        '入金額 [現金]
        sb.Append(Me.TextNkGenkin.Text & EarthConst.SEP_STRING)
        '入金額 [小切手]
        sb.Append(Me.TextNkKogitte.Text & EarthConst.SEP_STRING)
        '入金額 [振込]
        sb.Append(Me.TextNkFurikomi.Text & EarthConst.SEP_STRING)
        '入金額 [手形]
        sb.Append(Me.TextNkTegata.Text & EarthConst.SEP_STRING)
        '入金額 [相殺]
        sb.Append(Me.TextNkSousai.Text & EarthConst.SEP_STRING)
        '入金額 [値引]
        sb.Append(Me.TextNkNebiki.Text & EarthConst.SEP_STRING)
        '入金額 [その他]
        sb.Append(Me.TextNkSonota.Text & EarthConst.SEP_STRING)
        '入金額 [協力会費]
        sb.Append(Me.TextNkKyouryokuKaihi.Text & EarthConst.SEP_STRING)
        '入金額 [口座振替]
        sb.Append(Me.TextNkKouzaFurikae.Text & EarthConst.SEP_STRING)
        '入金額 [振込手数料]
        sb.Append(Me.TextNkFurikomiTesuuryou.Text & EarthConst.SEP_STRING)
        '手形期日
        sb.Append(Me.TextTegataKijitu.Text & EarthConst.SEP_STRING)
        '手形NO
        sb.Append(Me.TextTegataNo.Text & EarthConst.SEP_STRING)
        '摘要名
        sb.Append(Me.TextAreaTekiyou.Value & EarthConst.SEP_STRING)

        Return (sb.ToString)
    End Function
#End Region

#End Region

#Region "ボタンイベント"

    ''' <summary>
    ''' 修正実行/新規登録ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonUpdate_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUpdate.ServerClick
        Dim tmpScript As String = ""

        ' 入力チェック
        If Me.checkInput() = False Then Exit Sub

        ' 画面の内容をDBに反映する
        If Me.SaveData() Then '登録成功
            '画面を閉じる
            cl.CloseWindow(Me)

        Else '登録失敗
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", Me.ButtonUpdate.Value) & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonUpdate_ServerClick", tmpScript, True)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 請求先検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSeikyuuSakiSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearchSeikyuuSaki.ServerClick
        Dim hdnOldObj() As HtmlInputHidden = {Me.HiddenSeikyuuSakiKbnOld _
                                            , Me.HiddenSeikyuuSakiCdOld _
                                            , Me.HiddenSeikyuuSakiBrcOld}
        Dim blnResult As Boolean


        '請求先検索画面呼出
        blnResult = cl.CallSeikyuuSakiSearchWindow(sender, e, Me _
                                                    , Me.SelectSeikyuuSakiKbn, Me.TextSeikyuuSakiCd _
                                                , Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakiMei _
                                                , Me.ButtonSearchSeikyuuSaki _
                                                , hdnOldObj)

        '赤色文字変更対応を配列に格納
        Dim objChgColor As Object() = New Object() {Me.SelectSeikyuuSakiKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakiMei, Me.TextTorikesiRiyuu}
        '取消理由取得設定と色替処理
        cl.GetKameitenTorikesiRiyuuMain(Me.SelectSeikyuuSakiKbn.SelectedValue, Me.TextSeikyuuSakiCd.Text, Me.TextTorikesiRiyuu, True, False, objChgColor)

        If blnResult Then
            'フォーカスセット
            setFocusAJ(Me.ButtonSearchSeikyuuSaki)
        End If

    End Sub

#End Region

End Class