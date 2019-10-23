Partial Public Class KairyouKouji
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim iraiSession As New IraiSession
    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager

    Dim cl As New CommonLogic
    Dim cbLogic As New CommonBizLogic
    Dim JibanLogic As New JibanLogic
    Dim kameitenlogic As New KameitenSearchLogic
    ''' <summary>
    ''' メッセージクラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic

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

#Region "パラメータ/物件指定"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _kbnCp As String
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrKbnCp() As String
        Get
            Return _kbnCp
        End Get
        Set(ByVal value As String)
            _kbnCp = value
        End Set
    End Property

    ''' <summary>
    ''' 番号(保証書No)
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _noCp As String
    ''' <summary>
    ''' 番号(保証書No)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrBangouCp() As String
        Get
            Return _noCp
        End Get
        Set(ByVal value As String)
            _noCp = value
        End Set
    End Property

    ''' <summary>
    ''' コピーフラグ
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _copy As String
    ''' <summary>
    ''' コピーフラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrCopy() As String
        Get
            Return _copy
        End Get
        Set(ByVal value As String)
            _copy = value
        End Set
    End Property

    ''' <summary>
    ''' 元区分
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _motoKubun As String
    ''' <summary>
    ''' 元区分
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrMotoKubun() As String
        Get
            Return _motoKubun
        End Get
        Set(ByVal value As String)
            _motoKubun = value
        End Set
    End Property

    ''' <summary>
    ''' 元番号
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _motoNo As String
    ''' <summary>
    ''' 元番号
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrMotoNo() As String
        Get
            Return _motoNo
        End Get
        Set(ByVal value As String)
            _motoNo = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' 元商品コード
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _motoSyouhinCd As String
    ''' <summary>
    ''' 元商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrMotoSyouhinCd() As String
        Get
            Return _motoSyouhinCd
        End Get
        Set(ByVal value As String)
            _motoSyouhinCd = value
        End Set
    End Property

    ''' <summary>
    ''' 先商品コード
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _sakiSyouhinCd As String
    ''' <summary>
    ''' 先商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrSakiSyouhinCd() As String
        Get
            Return _sakiSyouhinCd
        End Get
        Set(ByVal value As String)
            _sakiSyouhinCd = value
        End Set
    End Property
#End Region

#Region "改良工事画面用/固定値"

    Private Const pStrKairyouKouji = "Kj"
    Private Const pStrTuikaKouji = "Tj"
    Private Const pStrHoukokusyo = "Kh"

    ''' <summary>
    ''' 工事タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Enum EnumKoujiType
        ''' <summary>
        ''' 改良工事
        ''' </summary>
        ''' <remarks></remarks>
        KairyouKouji = 0
        ''' <summary>
        ''' 追加工事
        ''' </summary>
        ''' <remarks></remarks>
        TuikaKouji = 1
        ''' <summary>
        ''' 改良工事報告書
        ''' </summary>
        ''' <remarks></remarks>
        KairyouKoujiHoukokusyo = 2
    End Enum

    ''' <summary>
    ''' 金額タイプ
    ''' </summary>
    ''' <remarks>売上金額or仕入金額</remarks>
    Enum EnumKingakuType
        ''' <summary>
        ''' 売上金額
        ''' </summary>
        ''' <remarks></remarks>
        UriageKingaku = 0
        ''' <summary>
        ''' 仕入金額
        ''' </summary>
        ''' <remarks></remarks>
        SiireKingaku = 1
        ''' <summary>
        ''' 指定なし
        ''' </summary>
        ''' <remarks></remarks>
        None = 2
    End Enum

#End Region

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
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合、メイン画面に飛ばす
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If Context.Items("irai") IsNot Nothing Then
            iraiSession = Context.Items("irai")
        End If

        '各テーブルの表示状態を切り替える
        Me.TBodyKairyouKoujiInfo.Style("display") = Me.HiddenKairyouKoujiInfoStyle.Value
        Me.TBodyKoujiHoukokusyoInfo.Style("display") = Me.HiddenKoujiHoukokusyoInfoStyle.Value

        If IsPostBack = False Then '初期起動時

            ' Key情報を保持
            If Context.Items("kbn") IsNot Nothing Then
                '登録実行後画面再描画用
                pStrKbn = Context.Items("kbn")
                pStrBangou = Context.Items("no")
                callModalFlg.Value = Context.Items("modal")
            Else
                '物件検索ほか
                pStrKbn = Request("sendPage_kubun")
                pStrBangou = Request("sendPage_hosyoushoNo")
            End If

            ' パラメータ不足時は画面を表示しない
            If pStrKbn Is Nothing Or pStrBangou Is Nothing Then
                Response.Redirect(UrlConst.MAIN)
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            Dim helper As New DropDownHelper

            '**************************
            ' 改良工事情報内
            '**************************
            ' 工事担当者コンボにデータをバインドする
            helper.SetDropDownList(SelectKoujiTantousya, DropDownHelper.DropDownType.Tantousya)

            '****************
            ' 改良工事
            '****************
            '改良工事種別コンボにデータをバインドする
            helper.SetDropDownList(SelectKjKairyouKoujiSyubetu, DropDownHelper.DropDownType.KairyouKoujiSyubetu)
            '商品コードコンボにデータをバインドする
            helper.SetDropDownList(SelectKjSyouhinCd, DropDownHelper.DropDownType.SyouhinKouji)

            '****************
            ' 追加工事
            '****************
            ' 改良工事種別コンボにデータをバインドする
            helper.SetDropDownList(SelectTjKairyouKoujiSyubetu, DropDownHelper.DropDownType.KairyouKoujiSyubetu)
            '商品コードコンボにデータをバインドする
            helper.SetDropDownList(SelectTjSyouhinCd, DropDownHelper.DropDownType.SyouhinTuika)

            '**************************
            ' 改良工事報告書情報内
            '**************************
            ' 受理コンボにデータをバインドする
            helper.SetDropDownList(SelectKhJuri, DropDownHelper.DropDownType.HkksJuri)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            SetDispAction()

            'ボタン押下イベントの設定
            setBtnEvent()

            '****************************************************************************
            ' 地盤データ取得
            '****************************************************************************
            Dim jibanRec As New JibanRecordBase
            Dim jibanMoto As New JibanRecordBase
            jibanRec = JibanLogic.GetJibanData(pStrKbn, pStrBangou) '地盤データの取得

            ' Key情報を保持(物件指定)
            pStrCopy = Request("copy")

            '物件指定からのコピー時
            If Not pStrCopy Is Nothing AndAlso pStrCopy = "1" Then
                ' Key情報を保持(物件指定)
                pStrKbnCp = Request("kbn")
                pStrBangouCp = Request("no")
                pStrMotoKubun = Request("motokubun")
                pStrMotoNo = Request("motono")

                SetCopyFromJibanRec(jibanRec, jibanMoto) '地盤データの該当データをコピー
            End If

            If Not jibanRec Is Nothing Then
                '地盤データの読み込み
                iraiSession.JibanData = jibanRec

                SetCtrlFromJibanRec(sender, e, jibanRec) '地盤データをコントロールにセット
            End If

            If ButtonTouroku1.Disabled = False Then
                ButtonTouroku1.Focus() '登録/修正ボタンにフォーカス
            End If

        Else
            '商品毎の請求・仕入先が変更されていないかをチェックし、
            '変更されている場合情報の再取得
            setSeikyuuSiireHenkou(sender, e)

        End If

        'コンテキストに値を格納
        Context.Items("irai") = iraiSession

    End Sub

    ''' <summary>
    ''' ページロードコンプリート
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '商品4
        cl.getSyouhin4MasterPath(ButtonSyouhin4, _
                                 userinfo, _
                                 Me.ucGyoumuKyoutuu.AccHiddenKubun.ClientID, _
                                 Me.ucGyoumuKyoutuu.AccBangou.ClientID, _
                                 Me.ucGyoumuKyoutuu.AccKameitenCd.ClientID, _
                                 Me.HiddenDefaultSiireSakiCdForLink.ClientID)

        '******************************
        '* 請求先/仕入先情報のセット
        '******************************
        Dim strUriageZumi As String = String.Empty    '売上処理済み判断フラグ用
        Dim strViewMode As String = String.Empty

        '********************
        '* 改良工事
        '********************
        '売上処理済判断フラグの取得
        strUriageZumi = cl.GetUriageSyoriZumiFlg(Me.SpanKjUriageSyoriZumi.InnerHtml)
        strViewMode = cl.GetViewMode(strUriageZumi, userinfo.KeiriGyoumuKengen)

        Me.ucSeikyuuSiireLinkKai.SetVariableValueCtrlFromParent(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                                    , Me.SelectKjSyouhinCd.SelectedValue _
                                                                    , Me.TextKjKoujiKaisyaCd.Text _
                                                                    , strUriageZumi _
                                                                    , Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked _
                                                                    , Me.TextKjKoujiKaisyaCd.Text _
                                                                    , _
                                                                    , strViewMode)

        '請求先タイプ
        Me.TextKjSeikyuusaki.Text = Me.ucSeikyuuSiireLinkKai.SeikyuuSakiTypeStr

        '********************
        '* 追加工事
        '********************
        '表示モードの初期化
        strViewMode = String.Empty

        '売上処理済判断フラグの取得
        strUriageZumi = cl.GetUriageSyoriZumiFlg(Me.SpanTjUriageSyorizumi.InnerHtml)
        strViewMode = cl.GetViewMode(strUriageZumi, userinfo.KeiriGyoumuKengen)

        If Me.SelectTjSyouhinCd.Style("display") = "none" Then
            strViewMode = EarthConst.MODE_VIEW
        Else
            strViewMode = String.Empty
        End If

        Me.ucSeikyuuSiireLinkTui.SetVariableValueCtrlFromParent(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                                    , Me.SelectTjSyouhinCd.SelectedValue _
                                                                    , Me.TextTjKoujiKaisyaCd.Text _
                                                                    , strUriageZumi _
                                                                    , Me.CheckBoxTjKoujiKaisyaSeikyuu.Checked _
                                                                    , Me.TextTjKoujiKaisyaCd.Text _
                                                                    , _
                                                                    , strViewMode)

        '請求先タイプ
        Me.TextTjSeikyuusaki.Text = Me.ucSeikyuuSiireLinkTui.SeikyuuSakiTypeStr

        Me.UpdatePanelKairyouKoujiInfo.Update()

        '********************
        '* 工事報告書再発行
        '********************
        '表示モードの初期化
        strViewMode = String.Empty

        '売上処理済判断フラグの取得
        strUriageZumi = cl.GetUriageSyoriZumiFlg(Me.SpanKhUriageSyorizumi.InnerHtml)
        strViewMode = cl.GetViewMode(strUriageZumi, userinfo.KeiriGyoumuKengen)

        If Me.SelectKhSeikyuuUmu.Style("display") = "none" Then
            strViewMode = EarthConst.MODE_VIEW
        Else
            strViewMode = String.Empty
        End If

        '工事報告書
        Me.ucSeikyuuSiireLink.SetVariableValueCtrlFromParent(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                                    , Me.TextKhSyouhinCd.Text _
                                                                    , Me.HiddenDefaultSiireSakiCdForLink.Value _
                                                                    , strUriageZumi _
                                                                    , _
                                                                    , _
                                                                    , _
                                                                    , strViewMode)

        '請求先が指定されていた場合、改良工事会社請求のチェックを使用不可とする
        If Me.ucSeikyuuSiireLinkKai.AccSeikyuuSakiCd.Value <> String.Empty _
        Or Me.ucSeikyuuSiireLinkKai.AccSeikyuuSakiBrc.Value <> String.Empty _
        Or Me.ucSeikyuuSiireLinkKai.AccSeikyuuSakiKbn.Value <> String.Empty Then
            Me.CheckBoxKjKoujiKaisyaSeikyuu.Enabled = False
        Else
            Me.CheckBoxKjKoujiKaisyaSeikyuu.Enabled = True
        End If

        '請求先が指定されていた場合、追加工事会社請求のチェックを使用不可とする
        If Me.ucSeikyuuSiireLinkTui.AccSeikyuuSakiCd.Value <> String.Empty _
        Or Me.ucSeikyuuSiireLinkTui.AccSeikyuuSakiBrc.Value <> String.Empty _
        Or Me.ucSeikyuuSiireLinkTui.AccSeikyuuSakiKbn.Value <> String.Empty Then
            Me.CheckBoxTjKoujiKaisyaSeikyuu.Enabled = False
        Else
            Me.CheckBoxTjKoujiKaisyaSeikyuu.Enabled = True
        End If

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
        Dim tmpTouroku As String = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this);}else{return false;}"
        Dim tmpScript As String = "if(CheckTouroku()){" & tmpTouroku & "}else{return false;}"

        '登録許可MSG確認後、OKの場合DB更新処理を行なう
        ButtonTouroku1.Attributes("onclick") = tmpScript
        ButtonTouroku2.Attributes("onclick") = tmpScript

    End Sub

    ''' <summary>
    ''' 業務共通[ユーザーコントロール]ロード時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ucGyoumuKyoutuu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ucGyoumuKyoutuu.Load

        '最終更新者、最終更新日時をセット
        TextSaisyuuKousinSya.Text = ucGyoumuKyoutuu.AccLastupdateusernm.Value
        TextSaisyuuKousinDate.Text = ucGyoumuKyoutuu.AccLastupdatedatetime.Value

        '初期起動時のみ
        If IsPostBack = False Then
            '画面制御
            SetEnableControlInitKj() '改良工事・初期起動
            SetEnableControlKj() '改良工事
            SetEnableControlInitTj() '追加工事・初期起動
            SetEnableControlTj() '追加工事
            SetEnableControlKh() '改良工事報告書

        End If

        '工事業務権限
        If userinfo.KoujiGyoumuKengen = 0 Then
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable
            htNotTarget.Add(ucGyoumuKyoutuu.AccDataHakiSyubetu.ID, ucGyoumuKyoutuu.AccDataHakiSyubetu) 'データ破棄種別
            htNotTarget.Add(ucGyoumuKyoutuu.AccDataHakiDate.ID, ucGyoumuKyoutuu.AccDataHakiDate) 'データ破棄日
            jSM.Hash2Ctrl(UpdatePanelKairyouKouji, EarthConst.MODE_VIEW, ht, htNotTarget)

            ButtonKjKoujiKaisyaSearch.Style("display") = "none"
            ButtonTjKoujiKaisyaSearch.Style("display") = "none"

            '登録ボタン
            ButtonTouroku1.Disabled = True
            ButtonTouroku2.Disabled = True
        End If

        '破棄権限
        If userinfo.DataHakiKengen = 0 Then
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable
            jSM.Hash2Ctrl(ucGyoumuKyoutuu.AccTdDataHakiSyubetu, EarthConst.MODE_VIEW, ht) 'データ破棄種別
            jSM.Hash2Ctrl(ucGyoumuKyoutuu.AccTdDataHakiData, EarthConst.MODE_VIEW, ht) 'データ破棄日

        End If

        '共通情報の入力チェック
        If ucGyoumuKyoutuu.AccKameitenCd.Value = "" Then '加盟店コード
            Dim tmpScript As String = ""

            '登録ボタンの非活性化
            ButtonTouroku1.Disabled = True
            ButtonTouroku2.Disabled = True

            tmpScript = "alert('" & Messages.MSG065W & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ucGyoumuKyoutuu_Load", tmpScript, True)

            '地盤画面共通クラス
            Dim noTarget As New Hashtable
            Dim jSM As New JibanSessionManager
            Dim ht As New Hashtable
            Dim htNotTarget As New Hashtable

            '全てのコントロールを無効化
            jSM.Hash2Ctrl(UpdatePanelKairyouKouji, EarthConst.MODE_VIEW, ht, htNotTarget)
        End If

        '破棄種別チェック
        If ucGyoumuKyoutuu.AccDataHakiSyubetu.SelectedValue <> "0" Then
            '破棄種別が設定されている場合、すべてのコントロールを無効化
            CheckHakiDisable(True)
        End If

        'デフォルトフォーカス
        If ButtonTouroku1.Disabled <> True Then
            SetFocus(ButtonTouroku1)
        End If

    End Sub

    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)
        Dim jSM As New JibanSessionManager 'セッション管理クラス
        Dim jBn As New Jiban '地盤画面クラス

        Dim blnTorikesi As Boolean = False '取消フラグ(=False)
        Dim kisoSiyouLogic As New KisoSiyouLogic
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic

        ' コンボ設定ヘルパークラスを生成
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        '******************************************
        '* 画面コントロールに設定【改良工事情報】
        '******************************************
        '加盟店コード
        Me.HiddenKameitenCd.Value = cl.GetDisplayString(jr.KameitenCd)

        If jr.TysKaisyaCd <> "" And jr.TysKaisyaJigyousyoCd <> "" Then
            TextTyousaKaisyaMei.Text = tyousakaisyaSearchLogic.GetTyousaKaisyaMei(jr.TysKaisyaCd, jr.TysKaisyaJigyousyoCd, False) '調査会社
        End If
        TextTyousaJissiDate.Text = cl.GetDispStr(jr.TysJissiDate) '調査実施日
        TextKeikakusyoSakuseiDate.Text = cl.GetDispStr(jr.KeikakusyoSakuseiDate) '計画書作成日

        '●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Tantousya, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(jr.TantousyaCd, "")
        TextHanteisya.Text = objDrpTmp.SelectedItem.Text '判定者

        '存在チェック
        If cl.ChkDropDownList(SelectKoujiTantousya, cl.GetDispNum(jr.SyouninsyaCd)) Then
            TextKoujiTantousyaCd.Text = cl.GetDispNum(jr.SyouninsyaCd, "") '工事担当者コード
            SelectKoujiTantousya.SelectedValue = TextKoujiTantousyaCd.Text '工事担当者
        ElseIf jr.SyouninsyaCd > 0 Then
            TextKoujiTantousyaCd.Text = cl.GetDispNum(jr.SyouninsyaCd, "") '工事担当者コード
            SelectKoujiTantousya.Items.Add(New ListItem(TextKoujiTantousyaCd.Text & ":" & jr.SyouninsyaMei, TextKoujiTantousyaCd.Text)) '工事担当者
            SelectKoujiTantousya.SelectedValue = TextKoujiTantousyaCd.Text  '選択状態
        End If

        SpanHantei1.InnerHtml = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(jr.HanteiCd1)) '判定１名

        '●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.KsSiyouSetuzokusi, True, False)
        objDrpTmp.SelectedValue = cl.GetDispNum(jr.HanteiSetuzokuMoji, "")
        SpanHanteiSetuzokuMoji.InnerHtml = objDrpTmp.SelectedItem.Text '判定接続文字

        SpanHantei2.InnerHtml = cl.GetDispStr(kisoSiyouLogic.GetKisoSiyouMei(jr.HanteiCd2)) '判定２名

        '******************************************
        '* 画面コントロールに設定【改良工事】
        '******************************************
        SelectKjKoujiSiyouKakunin.SelectedValue = cl.GetDispNum(jr.KojSiyouKakunin, "") '工事仕様確認
        TextKjKakuninDate.Text = cl.GetDispStr(jr.KojSiyouKakuninDate) '確認日
        TextKjKoujiKaisyaCd.Text = cl.GetDispStr(jr.KojGaisyaCd & jr.KojGaisyaJigyousyoCd) '工事会社コード
        HiddenKjKojKaisyaCd.Value = cl.GetDispStr(jr.KojGaisyaCd & jr.KojGaisyaJigyousyoCd) '工事会社コード(Hidden)
        If jr.KojGaisyaCd <> "" And jr.KojGaisyaJigyousyoCd <> "" Then
            TextKjKoujiKaisyaMei.Text = tyousakaisyaSearchLogic.GetTyousaKaisyaMei(jr.KojGaisyaCd, jr.KojGaisyaJigyousyoCd, False) '工事会社名
        End If
        CheckBoxKjKoujiKaisyaSeikyuu.Checked = IIf(jr.KojGaisyaSeikyuuUmu = 1, True, False) '工事会社請求
        SelectKjKairyouKoujiSyubetu.SelectedValue = cl.GetDispNum(jr.KairyKojSyubetu, "") '改良工事種別
        TextKjKanryouYoteiDate.Text = cl.GetDispStr(jr.KairyKojKanryYoteiDate) '完了予定日

        TextKjKoujiDate.Text = cl.GetDispStr(jr.KairyKojDate) '工事日
        TextKjKankouSokuhouTyakuDate.Text = cl.GetDispStr(jr.KairyKojSokuhouTykDate) '完工速報着日

        '改良工事の邸別請求レコードがある場合
        If Not jr.KairyouKoujiRecord Is Nothing Then

            '邸別請求情報をコントロールにセット
            SetCtrlTeibetuSeikyuuDataKj(jr.KairyouKoujiRecord)

            '邸別入金情報をコントロールにセット
            If jr.TeibetuNyuukinRecords IsNot Nothing Then
                ' 入金額/残額をセット
                CalcZangaku(EnumKoujiType.KairyouKouji, jr.getZeikomiGaku(New String() {"130"}), jr.getNyuukinGaku("130"))
            Else
                ' 入金額/残額をセット
                SetKingakuUriage(EnumKoujiType.KairyouKouji, True)
            End If

        End If

        'コピー時の自動設定(請求書発行日、売上年月日)
        Me.ChkOnCopyAutoSetting(jr.KameitenCd, jr.Kbn)

        '******************************************
        '* 画面コントロールに設定【追加工事】
        '******************************************
        TextTjKoujiKaisyaCd.Text = cl.GetDispStr(jr.TKojKaisyaCd & jr.TKojKaisyaJigyousyoCd) '工事会社コード
        HiddenTjKojKaisyaCd.Value = cl.GetDispStr(jr.TKojKaisyaCd & jr.TKojKaisyaJigyousyoCd) '工事会社コード(Hidden)
        If jr.TKojKaisyaCd <> "" And jr.TKojKaisyaJigyousyoCd <> "" Then
            TextTjKoujiKaisyaMei.Text = tyousakaisyaSearchLogic.GetTyousaKaisyaMei(jr.TKojKaisyaCd, jr.TKojKaisyaJigyousyoCd, False) '工事会社名
        End If
        CheckBoxTjKoujiKaisyaSeikyuu.Checked = IIf(jr.TKojKaisyaSeikyuuUmu = 1, True, False) '工事会社請求

        SelectTjKairyouKoujiSyubetu.SelectedValue = cl.GetDispNum(jr.TKojSyubetu, "") '改良工事種別

        TextTjKanryouYoteiDate.Text = cl.GetDispStr(jr.TKojKanryYoteiDate) '完了予定日

        TextTjKoujiDate.Text = cl.GetDispStr(jr.TKojDate) '工事日
        TextTjKankouSokuhouTyakuDate.Text = cl.GetDispStr(jr.TKojSokuhouTykDate) '完工速報着日

        '追加工事の邸別請求レコードがある場合
        If Not jr.TuikaKoujiRecord Is Nothing Then

            '邸別請求情報をコントロールにセット
            SetCtrlTeibetuSeikyuuDataTj(jr.TuikaKoujiRecord)

            '邸別入金情報をコントロールにセット
            If jr.TeibetuNyuukinRecords IsNot Nothing Then
                ' 入金額/残額をセット
                CalcZangaku(EnumKoujiType.TuikaKouji, jr.getZeikomiGaku(New String() {"140"}), jr.getNyuukinGaku("140"))
            Else
                ' 入金額/残額をセット
                SetKingakuUriage(EnumKoujiType.TuikaKouji, True)
            End If

        End If

        '******************************************
        '* 画面コントロールに設定【改良工事報告書情報】
        '******************************************
        SelectKhJuri.SelectedValue = cl.GetDispNum(jr.KojHkksUmu, "") '受理
        TextKhJuriSyousai.Text = cl.GetDispStr(jr.KojHkksJuriSyousai) '受理詳細
        TextKhJuriDate.Text = cl.GetDispStr(jr.KojHkksJuriDate) '受理日
        TextKhHassouDate.Text = cl.GetDispStr(jr.KojHkksHassouDate) '発送日
        TextKhSaihakkouDate.Text = cl.GetDispStr(jr.KojHkksSaihakDate) '再発行日

        '工事報告書情報がある場合
        If Not jr.KoujiHoukokusyoRecord Is Nothing Then

            '邸別請求情報をコントロールにセット
            SetCtrlTeibetuSeikyuuDataKh(jr.KoujiHoukokusyoRecord)

            '邸別入金情報をコントロールにセット
            If jr.TeibetuNyuukinRecords IsNot Nothing Then
                ' 入金額/残額をセット
                CalcZangaku(EnumKoujiType.KairyouKoujiHoukokusyo, jr.getZeikomiGaku(New String() {"160"}), jr.getNyuukinGaku("160"))
            Else
                ' 入金額/残額をセット
                SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo, True)
            End If

        End If

        '****************************
        '* Hidden項目
        '****************************
        HiddenHantei1Cd.Value = cl.GetDispNum(jr.HanteiCd1, "") '判定1コード
        HiddenHanteiSetuzokuMoji.Value = cl.GetDispNum(jr.HanteiSetuzokuMoji, "") '判定接続文字
        HiddenHantei2Cd.Value = cl.GetDispNum(jr.HanteiCd2, "") '判定2コード

        If jr.TysKaisyaCd <> "" And jr.TysKaisyaJigyousyoCd <> "" Then
            HiddenDefaultSiireSakiCdForLink.Value = jr.TysKaisyaCd & jr.TysKaisyaJigyousyoCd '調査会社コード
        End If

        HiddenKjSyouhinCdOld.Value = SelectKjSyouhinCd.SelectedValue '改良工事.商品コードOld
        HiddenKjUriageNengappiOld.Value = TextKjUriageNengappi.Text '改良工事.売上年月日Old

        HiddenTyousaGaiyou.Value = cl.GetDispNum(jr.TysGaiyou, "") '調査概要

        Me.HiddenHosyouSyouhinUmuOld.Value = cl.GetDisplayString(jr.HosyouSyouhinUmu) '保証商品有無

        Me.HiddenKjKoujiDateOld.Value = Me.TextKjKoujiDate.Text '改良工事・工事日Old
        Me.HiddenKjKankouSokuhouTyakuDateOld.Value = Me.TextKjKankouSokuhouTyakuDate.Text '改良工事・完工速報着日Old
        Me.HiddenTjKoujiDateOld.Value = Me.TextTjKoujiDate.Text '追加工事・工事日Old
        Me.HiddenTjKankouSokuhouTyakuDateOld.Value = Me.TextTjKankouSokuhouTyakuDate.Text '追加工事・完工速報着日Old

        '****************************
        '* Hidden項目(コントロールの値変更前)
        '****************************
        HiddenKjKoujiKaisyaCdMae.Value = TextKjKoujiKaisyaCd.Text '改良工事・工事会社コード変更前
        HiddenTjKoujiKaisyaCdMae.Value = TextTjKoujiKaisyaCd.Text '追加工事・工事会社コード変更前
        HiddenKjSyouhinCdMae.Value = SelectKjSyouhinCd.SelectedValue '改良工事・商品コード変更前
        HiddenTjSyouhinCdMae.Value = SelectTjSyouhinCd.SelectedValue '追加工事・商品コード変更前
        HiddenKjKairyouKoujiSyubetuMae.Value = SelectKjKairyouKoujiSyubetu.SelectedValue '改良工事・改良種別変更前
        HiddenTjKairyouKoujiSyubetuMae.Value = SelectTjKairyouKoujiSyubetu.SelectedValue '追加工事・改良種別変更前

        HiddenKhHassouDateMae.Value = TextKhHassouDate.Text '改良工事報告書・発送日前
        HiddenKhSaihakkouDateMae.Value = TextKhSaihakkouDate.Text '改良工事報告書・再発行日前
        HiddenKhSeikyuuUmuMae.Value = SelectKhSeikyuuUmu.SelectedValue '改良工事報告書・請求有無前

        '****************************
        '* Hidden項目(登録許可確認)
        '****************************
        '請求書発行日変更時処理
        HiddenKjSeikyuusyoHakkouDateMsg1.Value = "" '改良工事1
        HiddenKjSeikyuusyoHakkouDateMsg2.Value = "" '改良工事2
        HiddenTjSeikyuusyoHakkouDateMsg1.Value = "" '追加工事1
        HiddenTjSeikyuusyoHakkouDateMsg2.Value = "" '追加工事2

        'Chk05
        HiddenKjChk05.Value = "" '改良工事
        HiddenTjChk05.Value = "" '追加工事

        'Chk14
        '売上処理済ではなく、完工速報着日が未入力の場合、新規登録フラグをたてる
        If SpanKjUriageSyoriZumi.InnerHtml <> EarthConst.URIAGE_ZUMI And TextKjKankouSokuhouTyakuDate.Text = "" Then
            HiddenKjChk14.Value = "0"
        Else
            HiddenKjChk14.Value = "1"
        End If
        'Chk15
        '売上処理済ではなく、完工速報着日が未入力の場合、新規登録フラグをたてる
        If SpanTjUriageSyorizumi.InnerHtml <> EarthConst.URIAGE_ZUMI And TextTjKankouSokuhouTyakuDate.Text = "" Then
            HiddenTjChk15.Value = "0"
        Else
            HiddenTjChk15.Value = "1"
        End If

        'Chk08
        HiddenKjChk08.Value = ""
        'Chk09
        HiddenKjChk09.Value = ""
        'Chk10
        HiddenTjChk10.Value = ""
        'Chk11
        HiddenTjChk11.Value = ""

        '****************************
        '* セッションに画面情報を格納
        '****************************
        jSM.Ctrl2Hash(Me, jBn.IraiData)
        iraiSession.Irai1Data = jBn.IraiData

    End Sub

    ''' <summary>
    ''' 地盤データのコピーを行なう
    ''' </summary>
    ''' <param name="jibanRec">地盤データ[コピー先]</param>
    ''' <param name="jibanMoto">地盤データ[コピー元]</param>
    ''' <remarks></remarks>
    Private Sub SetCopyFromJibanRec(ByRef jibanRec As JibanRecordBase, ByRef jibanMoto As JibanRecordBase)
        Dim logic As New JibanLogic

        jibanMoto = logic.GetJibanData(pStrMotoKubun, pStrMotoNo) '地盤データの取得(コピー元)

        'インスタンス化
        If jibanRec.KairyouKoujiRecord Is Nothing Then '先
            jibanRec.KairyouKoujiRecord = New TeibetuSeikyuuRecord
        End If
        If jibanMoto.KairyouKoujiRecord Is Nothing Then '元
            jibanMoto.KairyouKoujiRecord = New TeibetuSeikyuuRecord
        End If

        If jibanRec.KairyouKoujiRecord.HattyuusyoGaku = 0 Or jibanRec.KairyouKoujiRecord.HattyuusyoGaku = Integer.MinValue Then
        Else
            'コピー先.発注書金額=入力あり、かつ、コピー元.商品コード=未入力
            If jibanMoto.KairyouKoujiRecord.SyouhinCd = String.Empty Then
                Exit Sub
            End If
        End If

        '****************************
        '* 地盤データ
        '****************************
        jibanRec.SyouninsyaCd = jibanMoto.SyouninsyaCd '工事担当者.コード
        jibanRec.SyouninsyaMei = jibanMoto.SyouninsyaMei '工事担当者.担当者名
        jibanRec.KojSiyouKakunin = jibanMoto.KojSiyouKakunin '<改良工事>工事仕様確認
        jibanRec.KojSiyouKakuninDate = jibanMoto.KojSiyouKakuninDate '<改良工事>確認日
        jibanRec.KojGaisyaCd = jibanMoto.KojGaisyaCd '<改良工事>工事会社コード
        jibanRec.KojGaisyaJigyousyoCd = jibanMoto.KojGaisyaJigyousyoCd '<改良工事>工事会社事業所コード
        jibanRec.KojGaisyaSeikyuuUmu = jibanMoto.KojGaisyaSeikyuuUmu '<改良工事>工事会社請求
        jibanRec.KairyKojSyubetu = jibanMoto.KairyKojSyubetu '<改良工事>改良工事種別
        jibanRec.KairyKojKanryYoteiDate = jibanMoto.KairyKojKanryYoteiDate '<改良工事>完了予定日

        '****************************
        '* 邸別請求データ(改良工事)
        '****************************
        '商品コードの退避
        pStrMotoSyouhinCd = jibanMoto.KairyouKoujiRecord.SyouhinCd
        pStrSakiSyouhinCd = jibanRec.KairyouKoujiRecord.SyouhinCd

        jibanRec.KairyouKoujiRecord.SyouhinCd = jibanMoto.KairyouKoujiRecord.SyouhinCd                      '<改良工事>商品コード
        jibanRec.KairyouKoujiRecord.SeikyuuUmu = jibanMoto.KairyouKoujiRecord.SeikyuuUmu                    '<改良工事>請求
        jibanRec.KairyouKoujiRecord.UriGaku = jibanMoto.KairyouKoujiRecord.UriGaku                          '<改良工事><売上金額>税抜金額
        jibanRec.KairyouKoujiRecord.ZeiKbn = jibanMoto.KairyouKoujiRecord.ZeiKbn                            '税区分
        jibanRec.KairyouKoujiRecord.Zeiritu = jibanMoto.KairyouKoujiRecord.Zeiritu                          '税率
        jibanRec.KairyouKoujiRecord.UriageSyouhiZeiGaku = jibanMoto.KairyouKoujiRecord.UriageSyouhiZeiGaku  '消費税額
        jibanRec.KairyouKoujiRecord.SiireGaku = jibanMoto.KairyouKoujiRecord.SiireGaku                      '<改良工事><仕入金額>税抜金額
        jibanRec.KairyouKoujiRecord.SiireSyouhiZeiGaku = jibanMoto.KairyouKoujiRecord.SiireSyouhiZeiGaku    '仕入消費税額

    End Sub

#Region "商品設定"

    ''' <summary>
    ''' 自動設定/商品情報(商品コードより標準価格、税区分、税率を設定する)
    ''' </summary>
    ''' <param name="emType"></param>
    ''' <remarks></remarks>
    Private Sub SetSyouhinInfo(ByVal emType As EnumKoujiType)
        Dim syouhinRec As New Syouhin23Record
        Dim tmpErrMsg As String = String.Empty
        Dim tmpScript As String = String.Empty

        '工事タイプ
        Select Case emType
            Case EnumKoujiType.KairyouKouji '改良工事

                '商品コード/商品名の自動設定
                syouhinRec = JibanLogic.GetSyouhinInfo(SelectKjSyouhinCd.SelectedValue, EarthEnum.EnumSyouhinKubun.KairyouKouji, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                If syouhinRec Is Nothing Then
                    SelectKjSyouhinCd.SelectedValue = "" '商品コード
                    HiddenKjZeiritu.Value = "" '税率
                    HiddenKjZeiKbn.Value = "" '税区分

                    SelectKjSeikyuuUmu.SelectedValue = "" '請求
                    TextKjUriageNengappi.Text = "" '売上年月日
                    TextKjUriageZeinukiKingaku.Text = "" '売上金額/税抜金額
                    TextKjUriageSyouhizei.Text = "" '売上金額/消費税
                    TextKjUriageZeikomiKingaku.Text = "" '売上金額/税込金額
                    TextKjSiireZeinukiKingaku.Text = "" '仕入金額/税抜金額
                    TextKjSiireSyouhizei.Text = "" '仕入金額/消費税
                    TextKjSiireZeikomiKingaku.Text = "" '仕入金額/税込金額
                    TextKjZangaku.Text = "0" '残額
                    TextKjSeikyuusyoHakkouDate.Text = "" '請求書発行日
                    TextKjHattyuusyoKakutei.Text = "" '発注書確定

                    '●エラーメッセージ表示
                    tmpErrMsg = Messages.MSG163E.Replace("@PARAM1", "商品マスタ")
                    tmpScript = "alert('" & tmpErrMsg & "');"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetSyouhinInfo", tmpScript, True)

                Else
                    HiddenKjZeiritu.Value = cl.GetDispStr(syouhinRec.Zeiritu) '税率
                    HiddenKjZeiKbn.Value = cl.GetDispStr(syouhinRec.ZeiKbn) '税区分

                    '請求有無
                    If SelectKjSeikyuuUmu.SelectedValue = "1" Then '有
                        TextKjUriageZeinukiKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1) '売上金額/税抜金額
                    Else '無
                        TextKjUriageZeinukiKingaku.Text = "0" '売上金額/税抜金額
                    End If

                End If

            Case EnumKoujiType.TuikaKouji '追加工事

                '商品コード/商品名の自動設定
                syouhinRec = JibanLogic.GetSyouhinInfo(SelectTjSyouhinCd.SelectedValue, EarthEnum.EnumSyouhinKubun.TuikaKouji, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
                If syouhinRec Is Nothing Then
                    SelectTjSyouhinCd.SelectedValue = "" '商品コード
                    HiddenTjZeiritu.Value = "" '税率
                    HiddenTjZeiKbn.Value = "" '税区分

                    SelectTjSeikyuuUmu.SelectedValue = "" '請求
                    TextTjUriageNengappi.Text = "" '売上年月日
                    TextTjUriageZeinukiKingaku.Text = "" '売上金額/税抜金額
                    TextTjUriageSyouhizei.Text = "" '売上金額/消費税
                    TextTjUriageZeikomiKingaku.Text = "" '売上金額/税込金額
                    TextTjSiireZeinukiKingaku.Text = "" '仕入金額/税抜金額
                    TextTjSiireSyouhizei.Text = "" '仕入金額/消費税
                    TextTjSiireZeikomiKingaku.Text = "" '仕入金額/税込金額
                    TextTjZangaku.Text = "0" '残額
                    TextTjSeikyuusyoHakkouDate.Text = "" '請求書発行日
                    TextTjHattyuusyoKakutei.Text = "" '発注書確定

                    '●エラーメッセージ表示
                    tmpErrMsg = Messages.MSG163E.Replace("@PARAM1", "商品マスタ")
                    tmpScript = "alert('" & tmpErrMsg & "');"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetSyouhinInfo", tmpScript, True)

                Else
                    HiddenTjZeiritu.Value = cl.GetDispStr(syouhinRec.Zeiritu) '税率
                    HiddenTjZeiKbn.Value = cl.GetDispStr(syouhinRec.ZeiKbn) '税区分

                    '請求有無
                    If SelectTjSeikyuuUmu.SelectedValue = "1" Then '有
                        TextTjUriageZeinukiKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1) '売上金額/税抜金額
                    Else '無
                        TextTjUriageZeinukiKingaku.Text = "0" '売上金額/税抜金額
                    End If

                End If

            Case EnumKoujiType.KairyouKoujiHoukokusyo '改良工事報告書
                Exit Sub
            Case Else
                Exit Sub
        End Select

    End Sub

    ''' <summary>
    ''' 自動設定/商品情報(工事価格マスタから金額・請求有無等設定)
    ''' </summary>
    ''' <param name="emType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSyouhinInfoFromKojM(ByVal emType As EnumKoujiType, ByVal emAcType As EarthEnum.emKojKkkActionType) As Boolean

        Dim syouhinRec As New Syouhin23Record
        Dim keyRec As New KoujiKakakuKeyRecord          '検索キー用レコード
        Dim resultRec As New KoujiKakakuRecord          '結果取得用レコード
        Dim lgcKouji As New KairyouKoujiLogic           '改良工事ロジック
        Dim intResult As Integer = Integer.MinValue

        '工事タイプ
        Select Case emType
            Case EnumKoujiType.KairyouKouji '改良工事

                '取得に必要な画面項目のセット
                keyRec = cbLogic.GetKojKkkMstKeyRec(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                , Me.ucGyoumuKyoutuu.AccHdnEigyousyoCd.Value _
                                                , Me.ucGyoumuKyoutuu.AccHdnKeiretuCd.Value _
                                                , Me.SelectKjSyouhinCd.SelectedValue _
                                                , Me.TextKjKoujiKaisyaCd.Text)

                '工事会社価格ロジックより取得
                intResult = lgcKouji.GetKoujiKakakuRecord(keyRec, resultRec)

                '条件一致の取得は画面にセット
                If intResult < EarthEnum.emKoujiKakaku.Syouhin Then
                    '工事会社請求
                    If resultRec.KojGaisyaSeikyuuUmu = 1 Then
                        Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked = True
                    Else
                        Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked = False
                    End If
                    '請求有無のセットは請求有無変更時以外
                    If emAcType <> EarthEnum.emKojKkkActionType.SeikyuuUmu Then
                        '請求有無(セットするのは有り／無し時のみ）
                        If resultRec.SeikyuuUmu = 0 OrElse resultRec.SeikyuuUmu = 1 Then
                            Me.SelectKjSeikyuuUmu.SelectedValue = resultRec.SeikyuuUmu
                        Else
                            Me.SelectKjSeikyuuUmu.SelectedValue = String.Empty
                        End If
                    End If
                    '売上税抜金額
                    If SelectKjSeikyuuUmu.SelectedValue = "1" Then  '請求有無「有」
                        Me.TextKjUriageZeinukiKingaku.Text = cl.ChgStrToInt(cl.GetDispNum(resultRec.UriGaku)).ToString(EarthConst.FORMAT_KINGAKU_1)
                    Else  '請求有無「無」
                        Me.TextKjUriageZeinukiKingaku.Text = "0"
                    End If
                    '税率
                    Me.HiddenKjZeiritu.Value = cl.GetDispStr(resultRec.Zeiritu)
                    '税区分
                    Me.HiddenKjZeiKbn.Value = cl.GetDispStr(resultRec.ZeiKbn)
                Else
                    Return False
                    Exit Function
                End If

            Case EnumKoujiType.TuikaKouji   '追加工事

                '取得に必要な画面項目のセット
                keyRec = cbLogic.GetKojKkkMstKeyRec(Me.ucGyoumuKyoutuu.AccKameitenCd.Value _
                                                , Me.ucGyoumuKyoutuu.AccHdnEigyousyoCd.Value _
                                                , Me.ucGyoumuKyoutuu.AccHdnKeiretuCd.Value _
                                                , Me.SelectTjSyouhinCd.SelectedValue _
                                                , Me.TextTjKoujiKaisyaCd.Text)

                '工事会社価格ロジックより取得
                intResult = lgcKouji.GetKoujiKakakuRecord(keyRec, resultRec)

                '条件一致の取得は画面にセット
                If intResult < EarthEnum.emKoujiKakaku.Syouhin Then
                    '工事会社請求
                    If resultRec.KojGaisyaSeikyuuUmu = 1 Then
                        Me.CheckBoxTjKoujiKaisyaSeikyuu.Checked = True
                    Else
                        Me.CheckBoxTjKoujiKaisyaSeikyuu.Checked = False
                    End If
                    '請求有無のセットは請求有無変更時以外
                    If emAcType <> EarthEnum.emKojKkkActionType.SeikyuuUmu Then
                        '請求有無（セットするのは有り／無し時のみ）
                        If resultRec.SeikyuuUmu = 0 OrElse resultRec.SeikyuuUmu = 1 Then
                            Me.SelectTjSeikyuuUmu.SelectedValue = resultRec.SeikyuuUmu
                        Else
                            Me.SelectTjSeikyuuUmu.SelectedValue = String.Empty
                        End If
                    End If
                    '売上税抜金額
                    If SelectTjSeikyuuUmu.SelectedValue = "1" Then '有
                        Me.TextTjUriageZeinukiKingaku.Text = cl.ChgStrToInt(cl.GetDispNum(resultRec.UriGaku)).ToString(EarthConst.FORMAT_KINGAKU_1)
                    Else
                        Me.TextTjUriageZeinukiKingaku.Text = "0"
                    End If
                    '税率
                    Me.HiddenTjZeiritu.Value = cl.GetDispStr(resultRec.Zeiritu)
                    '税区分
                    Me.HiddenTjZeiKbn.Value = cl.GetDispStr(resultRec.ZeiKbn)
                Else
                    Return False
                    Exit Function
                End If

            Case Else
                Exit Function
        End Select

        Return True

    End Function

#End Region

#Region "金額設定"

    ''' <summary>
    ''' 金額設定(売上金額)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKingakuUriage(ByVal emType As EnumKoujiType, Optional ByVal blnZeigaku As Boolean = False)
        ' 税抜価格（実請求金額）
        Dim zeinuki_ctrl As TextBox
        ' 消費税率
        Dim zeiritu_ctrl As HtmlInputHidden
        ' 消費税額
        Dim zeigaku_ctrl As TextBox
        ' 税込金額
        Dim zeikomi_gaku_ctrl As TextBox

        Select Case emType
            Case EnumKoujiType.KairyouKouji '改良工事
                zeinuki_ctrl = TextKjUriageZeinukiKingaku
                zeiritu_ctrl = HiddenKjZeiritu
                zeigaku_ctrl = TextKjUriageSyouhizei
                zeikomi_gaku_ctrl = TextKjUriageZeikomiKingaku

            Case EnumKoujiType.TuikaKouji '追加工事
                zeinuki_ctrl = TextTjUriageZeinukiKingaku
                zeiritu_ctrl = HiddenTjZeiritu
                zeigaku_ctrl = TextTjUriageSyouhizei
                zeikomi_gaku_ctrl = TextTjUriageZeikomiKingaku

            Case EnumKoujiType.KairyouKoujiHoukokusyo '改良工事報告書
                zeinuki_ctrl = TextKhJituseikyuuKingaku
                zeiritu_ctrl = HiddenKhZeiritu
                zeigaku_ctrl = TextKhSyouhizei
                zeikomi_gaku_ctrl = TextKhZeikomiKingaku

            Case Else
                Exit Sub
        End Select

        Dim zeinuki As Integer = 0
        Dim zeiritu As Decimal = 0
        Dim zeigaku As Integer = 0
        Dim zeikomi_gaku As Integer = 0

        If zeinuki_ctrl.Text = "" Then '未入力
            zeinuki_ctrl.Text = ""
            zeigaku_ctrl.Text = ""
            zeikomi_gaku_ctrl.Text = ""

        Else '入力あり
            cl.SetDisplayString(CInt(zeinuki_ctrl.Text), zeinuki)
            cl.SetDisplayString(zeiritu_ctrl.Value, zeiritu)

            zeinuki = IIf(zeinuki = Integer.MinValue, 0, zeinuki)
            zeiritu = IIf(zeiritu = Decimal.MinValue, 0, zeiritu)

            If blnZeigaku Then '消費税額の値で計算
                cl.SetDisplayString(CInt(zeigaku_ctrl.Text), zeigaku)
            Else
                zeigaku = Fix(zeinuki * zeiritu)
            End If
            zeikomi_gaku = zeinuki + zeigaku

            zeigaku_ctrl.Text = Format(zeigaku, EarthConst.FORMAT_KINGAKU_1) '消費税
            zeikomi_gaku_ctrl.Text = Format(zeikomi_gaku, EarthConst.FORMAT_KINGAKU_1) '税込金額

        End If

        ' 入金額/残額をセット
        CalcZangaku(emType, zeikomi_gaku_ctrl.Text)

    End Sub

    ''' <summary>
    ''' 金額設定(仕入金額)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKingakuSiire(ByVal emType As EnumKoujiType)
        ' 税抜価格（実請求金額）
        Dim zeinuki_ctrl As TextBox
        ' 消費税率
        Dim zeiritu_ctrl As HtmlInputHidden
        ' 消費税額
        Dim zeigaku_ctrl As TextBox
        ' 税込金額
        Dim zeikomi_gaku_ctrl As TextBox

        Select Case emType
            Case EnumKoujiType.KairyouKouji '改良工事
                zeinuki_ctrl = TextKjSiireZeinukiKingaku
                zeiritu_ctrl = HiddenKjZeiritu
                zeigaku_ctrl = TextKjSiireSyouhizei
                zeikomi_gaku_ctrl = TextKjSiireZeikomiKingaku

            Case EnumKoujiType.TuikaKouji '追加工事
                zeinuki_ctrl = TextTjSiireZeinukiKingaku
                zeiritu_ctrl = HiddenTjZeiritu
                zeigaku_ctrl = TextTjSiireSyouhizei
                zeikomi_gaku_ctrl = TextTjSiireZeikomiKingaku

            Case EnumKoujiType.KairyouKoujiHoukokusyo '改良工事報告書
                Exit Sub

            Case Else
                Exit Sub
        End Select

        Dim zeinuki As Integer = 0
        Dim zeiritu As Decimal = 0
        Dim zeigaku As Integer = 0
        Dim zeikomi_gaku As Integer = 0

        If zeinuki_ctrl.Text = "" Then '未入力
            zeinuki_ctrl.Text = ""
            zeigaku_ctrl.Text = ""
            zeikomi_gaku_ctrl.Text = ""

        Else '入力あり

            cl.SetDisplayString(CInt(zeinuki_ctrl.Text), zeinuki)
            cl.SetDisplayString(zeiritu_ctrl.Value, zeiritu)

            zeinuki = IIf(zeinuki = Integer.MinValue, 0, zeinuki)
            zeiritu = IIf(zeiritu = Decimal.MinValue, 0, zeiritu)

            zeigaku = Fix(zeinuki * zeiritu)
            zeikomi_gaku = zeinuki + zeigaku

            zeigaku_ctrl.Text = Format(zeigaku, EarthConst.FORMAT_KINGAKU_1) '消費税
            zeikomi_gaku_ctrl.Text = Format(zeikomi_gaku, EarthConst.FORMAT_KINGAKU_1) '税込金額
        End If

    End Sub

    ''' <summary>
    ''' 商品テーブル金額エリアの0クリア/工事報告書再発行
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub Clear0SyouhinTableKh()
        '金額の0クリア
        '●自動設定
        TextKhKoumutenSeikyuuKingaku.Text = "0" '工務店請求金額
        TextKhJituseikyuuKingaku.Text = "0" '実請求金額
        TextKhSyouhizei.Text = "0" '消費税
        TextKhZeikomiKingaku.Text = "0" '税込金額
        TextKhSeikyuusyoHakkouDate.Text = "" '請求書発行日
        TextKhUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) '売上年月日
        '発注書確定
        If TextKhHattyuusyoKakutei.Text.Length = 0 Then '(*5)発注書確定が空白の場合は、「0：未確定」を設定する
            TextKhHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
        End If

    End Sub

    ''' <summary>
    ''' 商品テーブル金額エリアの空白クリア
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearBlnkSyouhinTable()
        '空白クリア
        '●自動設定
        TextKhSyouhinCd.Text = "" '商品コード
        SpanKhSyouhinMei.InnerHtml = "" '商品名
        TextKhKoumutenSeikyuuKingaku.Text = "" '工務店請求金額
        TextKhJituseikyuuKingaku.Text = "" '実請求金額
        TextKhSeikyuusyoHakkouDate.Text = "" '請求書発行日
        TextKhUriageNengappi.Text = "" '売上年月日

        SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo) '金額の再計算

    End Sub

#Region "税率/税区分"

    ''' <summary>
    ''' 税率/税区分をHiddenにセットする
    ''' </summary>
    ''' <param name="strItemCd"></param>
    ''' <remarks></remarks>
    Protected Sub SetKjZeiInfo(ByVal strItemCd As String)
        Dim syouhinRec As New Syouhin23Record

        '商品情報を取得(キー:商品コード)
        syouhinRec = JibanLogic.GetSyouhinInfo(strItemCd, EarthEnum.EnumSyouhinKubun.KairyouKouji, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If Not syouhinRec Is Nothing Then
            HiddenKjZeiritu.Value = syouhinRec.Zeiritu '税率
            HiddenKjZeiKbn.Value = syouhinRec.ZeiKbn '税区分
        End If
    End Sub

    ''' <summary>
    ''' 税率/税区分をHiddenにセットする
    ''' </summary>
    ''' <param name="strItemCd"></param>
    ''' <remarks></remarks>
    Protected Sub SetTjZeiInfo(ByVal strItemCd As String)
        Dim syouhinRec As New Syouhin23Record

        '商品情報を取得(キー:商品コード)
        syouhinRec = JibanLogic.GetSyouhinInfo(strItemCd, EarthEnum.EnumSyouhinKubun.TuikaKouji, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If Not syouhinRec Is Nothing Then
            HiddenTjZeiritu.Value = syouhinRec.Zeiritu '税率
            HiddenTjZeiKbn.Value = syouhinRec.ZeiKbn '税区分
        End If
    End Sub

    ''' <summary>
    ''' 税率/税区分をHiddenにセットする
    ''' </summary>
    ''' <param name="strItemCd"></param>
    ''' <remarks></remarks>
    Protected Sub SetKhZeiInfo(ByVal strItemCd As String)
        Dim syouhin23Rec As New Syouhin23Record

        '商品情報を取得(キー:商品コード)
        syouhin23Rec = JibanLogic.GetSyouhinInfo(strItemCd, EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If Not syouhin23Rec Is Nothing Then
            HiddenKhZeiritu.Value = syouhin23Rec.Zeiritu '税率
            HiddenKhZeiKbn.Value = syouhin23Rec.ZeiKbn '税区分
        End If
    End Sub


#End Region

#End Region

#Region "入金額/残額"

    ''' <summary>
    ''' 入金額・残額を表示します<br/>
    ''' 税込売上金額合計のみ変更し、再計算します<br/>
    ''' </summary>
    ''' <param name="emType">工事タイプ</param>
    ''' <param name="strUriageGoukeiGaku">税込売上金額合計</param>
    ''' <remarks></remarks>
    Public Sub CalcZangaku(ByVal emType As EnumKoujiType, ByVal strUriageGoukeiGaku As String)

        ' 入金額（税込）
        Dim nyuukingaku_ctrl As TextBox
        ' 残額
        Dim zangakuu_ctrl As TextBox

        Select Case emType
            Case EnumKoujiType.KairyouKouji '改良工事
                nyuukingaku_ctrl = TextKjNyuukingaku
                zangakuu_ctrl = TextKjZangaku

            Case EnumKoujiType.TuikaKouji '追加工事
                nyuukingaku_ctrl = TextTjNyuuKingaku
                zangakuu_ctrl = TextTjZangaku

            Case EnumKoujiType.KairyouKoujiHoukokusyo '改良工事報告書
                nyuukingaku_ctrl = TextKhNyuuKingaku
                zangakuu_ctrl = TextKhZangaku

            Case Else
                Exit Sub
        End Select

        If strUriageGoukeiGaku = "" Then
            ' 残額
            zangakuu_ctrl.Text = "0"

        Else
            Dim uriageGoukeiGaku As Integer = Integer.MinValue

            uriageGoukeiGaku = CInt(strUriageGoukeiGaku)

            Dim strNyuukinGaku As String = IIf(nyuukingaku_ctrl.Text.Replace(",", "").Trim() = "", _
                                            "0", _
                                            nyuukingaku_ctrl.Text.Replace(",", "").Trim())

            Dim nyuukinGaku As Integer = Integer.Parse(strNyuukinGaku)

            ' NULLは０にする
            uriageGoukeiGaku = IIf(uriageGoukeiGaku = Integer.MinValue, 0, uriageGoukeiGaku)

            ' 残額
            zangakuu_ctrl.Text = (uriageGoukeiGaku - nyuukinGaku).ToString(EarthConst.FORMAT_KINGAKU_1)

        End If

    End Sub

    ''' <summary>
    ''' 入金額・残額を表示します
    ''' </summary>
    ''' <param name="emType">工事タイプ</param>
    ''' <param name="uriageGoukeiGaku">税込売上金額合計</param>
    ''' <param name="nyuukinGaku">入金額</param>
    ''' <remarks></remarks>
    Public Sub CalcZangaku( _
                            ByVal emType As EnumKoujiType _
                            , ByVal uriageGoukeiGaku As Integer _
                            , ByVal nyuukinGaku As Integer _
                            )

        ' 入金額（税込）
        Dim nyuukingaku_ctrl As TextBox
        ' 残額
        Dim zangakuu_ctrl As TextBox

        Select Case emType
            Case EnumKoujiType.KairyouKouji '改良工事
                nyuukingaku_ctrl = TextKjNyuukingaku
                zangakuu_ctrl = TextKjZangaku

            Case EnumKoujiType.TuikaKouji '追加工事
                nyuukingaku_ctrl = TextTjNyuuKingaku
                zangakuu_ctrl = TextTjZangaku

            Case EnumKoujiType.KairyouKoujiHoukokusyo '改良工事報告書
                nyuukingaku_ctrl = TextKhNyuuKingaku
                zangakuu_ctrl = TextKhZangaku

            Case Else
                Exit Sub
        End Select

        ' NULLは０にする
        uriageGoukeiGaku = IIf(uriageGoukeiGaku = Integer.MinValue, 0, uriageGoukeiGaku)
        nyuukinGaku = IIf(nyuukinGaku = Integer.MinValue, 0, nyuukinGaku)

        ' 入金額
        nyuukingaku_ctrl.Text = nyuukinGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        ' 残額
        zangakuu_ctrl.Text = (uriageGoukeiGaku - nyuukinGaku).ToString(EarthConst.FORMAT_KINGAKU_1)

    End Sub

#End Region

    ''' <summary>
    ''' コントロールの接頭辞を付与して返す。
    ''' </summary>
    ''' <param name="intKoujiType">工事タイプ</param>
    ''' <param name="intKingakuType">金額タイプ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChgStrKoujiType( _
                                    ByVal intKoujiType As EnumKoujiType _
                                    , Optional ByVal intKingakuType As EnumKingakuType = EnumKingakuType.UriageKingaku _
                                    ) As String

        Dim strKingakuType As String = ""
        '金額タイプ
        Select Case intKingakuType
            Case EnumKingakuType.UriageKingaku
                strKingakuType = "Uriage"
            Case EnumKingakuType.SiireKingaku
                strKingakuType = "Siire"
            Case EnumKingakuType.None
                strKingakuType = ""
        End Select

        '工事タイプ
        Select Case intKoujiType
            Case EnumKoujiType.KairyouKouji
                Return pStrKairyouKouji & strKingakuType
            Case EnumKoujiType.TuikaKouji
                Return pStrTuikaKouji & strKingakuType
            Case EnumKoujiType.KairyouKoujiHoukokusyo
                Return pStrHoukokusyo
            Case Else
                Return ""
        End Select
    End Function

#Region "邸別請求レコード編集"

#Region "画面コントロールへ出力"
    ''' <summary>
    ''' 改良工事/邸別請求レコード
    ''' </summary>
    ''' <param name="TeibetuRec"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlTeibetuSeikyuuDataKj( _
                                    ByVal TeibetuRec As TeibetuSeikyuuRecord _
                                    )

        '商品コードの存在チェック
        If cl.ChkDropDownList(SelectKjSyouhinCd, TeibetuRec.SyouhinCd) Then
            SelectKjSyouhinCd.SelectedValue = cl.GetDispStr(TeibetuRec.SyouhinCd) '商品コード
        Else '未存在の場合、項目追加
            SelectKjSyouhinCd.Items.Add(New ListItem(TeibetuRec.SyouhinCd & ":" & TeibetuRec.SyouhinMei, TeibetuRec.SyouhinCd)) '商品コード
            SelectKjSyouhinCd.SelectedValue = TeibetuRec.SyouhinCd  '選択状態
        End If

        ' 売上処理済
        SpanKjUriageSyoriZumi.InnerHtml = IIf(TeibetuRec.UriKeijyouDate = Date.MinValue, "", EarthConst.URIAGE_ZUMI)
        ' 売上計上日
        Me.HiddenKjUriageKeijyouDate.Value = cl.GetDisplayString(TeibetuRec.UriKeijyouDate)

        ' 請求有無
        SelectKjSeikyuuUmu.SelectedValue = IIf(TeibetuRec.SeikyuuUmu = 1, "1", "0")

        ' 税率（Hidden）
        HiddenKjZeiritu.Value = TeibetuRec.Zeiritu
        ' 税区分（Hidden）
        HiddenKjZeiKbn.Value = IIf(TeibetuRec.ZeiKbn = "", "", TeibetuRec.ZeiKbn)
        If TeibetuRec.Zeiritu = 0 And TeibetuRec.ZeiKbn = "" Then
            SetKjZeiInfo(TeibetuRec.SyouhinCd)
        End If

        '*****************
        '* 【売上金額】
        '*****************
        '●売上消費税額
        TextKjUriageSyouhizei.Text = IIf(TeibetuRec.UriageSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.UriageSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税抜売上金額
        TextKjUriageZeinukiKingaku.Text = IIf(TeibetuRec.UriGaku = Integer.MinValue, 0, Format(TeibetuRec.UriGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税込売上金額
        TextKjUriageZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiUriGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiUriGaku, EarthConst.FORMAT_KINGAKU_1))

        '*****************
        '* 【仕入金額】
        '*****************
        '●仕入消費税額
        TextKjSiireSyouhizei.Text = IIf(TeibetuRec.SiireSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税抜仕入金額
        TextKjSiireZeinukiKingaku.Text = IIf(TeibetuRec.SiireGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税込仕入金額
        TextKjSiireZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiSiireGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiSiireGaku, EarthConst.FORMAT_KINGAKU_1))

        ' 請求書発行日
        TextKjSeikyuusyoHakkouDate.Text = IIf(TeibetuRec.SeikyuusyoHakDate = Date.MinValue, _
                                          "", _
                                          TeibetuRec.SeikyuusyoHakDate.ToString(EarthConst.FORMAT_DATE_TIME_9))
        ' 売上年月日
        TextKjUriageNengappi.Text = IIf(TeibetuRec.UriDate = Date.MinValue, _
                                      "", _
                                      TeibetuRec.UriDate.ToString(EarthConst.FORMAT_DATE_TIME_9))

        ' 発注書確定
        TextKjHattyuusyoKakutei.Text = IIf(TeibetuRec.HattyuusyoKakuteiFlg = 1, EarthConst.KAKUTEI, EarthConst.MIKAKUTEI)
        ' 発注書金額
        TextKjHattyuusyoKingaku.Text = IIf(TeibetuRec.HattyuusyoGaku = Integer.MinValue, _
                                         "", _
                                         TeibetuRec.HattyuusyoGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' 発注書確認日
        TextKjHattyuusyoKakuninDate.Text = IIf(TeibetuRec.HattyuusyoKakuninDate = Date.MinValue, _
                                           "", _
                                           TeibetuRec.HattyuusyoKakuninDate.ToString(EarthConst.FORMAT_DATE_TIME_9))

        '請求先/仕入先リンクへセット
        Me.ucSeikyuuSiireLinkKai.SetSeikyuuSiireLinkFromTeibetuRec(TeibetuRec)

        ' 更新日時 読み込み時のタイムスタンプ(排他制御用)
        cl.setDispHaitaUpdTime(TeibetuRec.AddDatetime, TeibetuRec.UpdDatetime, Me.HiddenUpdateTeibetuSeikyuuDatetimeKj)

    End Sub

    ''' <summary>
    ''' 追加工事/邸別請求レコード
    ''' </summary>
    ''' <param name="TeibetuRec"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlTeibetuSeikyuuDataTj( _
                                    ByVal TeibetuRec As TeibetuSeikyuuRecord _
                                    )

        '商品コードの存在チェック
        If cl.ChkDropDownList(SelectTjSyouhinCd, TeibetuRec.SyouhinCd) Then
            SelectTjSyouhinCd.SelectedValue = cl.GetDispStr(TeibetuRec.SyouhinCd) '商品コード
        Else '未存在の場合、項目追加
            SelectTjSyouhinCd.Items.Add(New ListItem(TeibetuRec.SyouhinCd & ":" & TeibetuRec.SyouhinMei, TeibetuRec.SyouhinCd)) '商品コード
            SelectTjSyouhinCd.SelectedValue = TeibetuRec.SyouhinCd  '選択状態
        End If
        ' 売上処理済
        SpanTjUriageSyorizumi.InnerHtml = IIf(TeibetuRec.UriKeijyouDate = Date.MinValue, "", EarthConst.URIAGE_ZUMI)
        ' 売上計上日
        Me.HiddenTjUriageKeijyouDate.Value = cl.GetDisplayString(TeibetuRec.UriKeijyouDate)
        ' 請求有無
        SelectTjSeikyuuUmu.SelectedValue = IIf(TeibetuRec.SeikyuuUmu = 1, "1", "0")

        ' 税率（Hidden）
        HiddenTjZeiritu.Value = TeibetuRec.Zeiritu
        ' 税区分（Hidden）
        HiddenTjZeiKbn.Value = IIf(TeibetuRec.ZeiKbn = "", "", TeibetuRec.ZeiKbn)
        If TeibetuRec.Zeiritu = 0 And TeibetuRec.ZeiKbn = "" Then
            SetTjZeiInfo(TeibetuRec.SyouhinCd)
        End If

        '*****************
        '* 【売上金額】
        '*****************
        '●売上消費税額
        TextTjUriageSyouhizei.Text = IIf(TeibetuRec.UriageSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.UriageSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税抜売上金額
        TextTjUriageZeinukiKingaku.Text = IIf(TeibetuRec.UriGaku = Integer.MinValue, 0, Format(TeibetuRec.UriGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税込売上金額
        TextTjUriageZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiUriGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiUriGaku, EarthConst.FORMAT_KINGAKU_1))

        '*****************
        '* 【仕入金額】
        '*****************
        '●仕入消費税額
        TextTjSiireSyouhizei.Text = IIf(TeibetuRec.SiireSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税抜仕入金額
        TextTjSiireZeinukiKingaku.Text = IIf(TeibetuRec.SiireGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税込仕入金額
        TextTjSiireZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiSiireGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiSiireGaku, EarthConst.FORMAT_KINGAKU_1))

        ' 請求書発行日
        TextTjSeikyuusyoHakkouDate.Text = IIf(TeibetuRec.SeikyuusyoHakDate = Date.MinValue, _
                                          "", _
                                          TeibetuRec.SeikyuusyoHakDate.ToString(EarthConst.FORMAT_DATE_TIME_9))
        ' 売上年月日
        TextTjUriageNengappi.Text = IIf(TeibetuRec.UriDate = Date.MinValue, _
                                      "", _
                                      TeibetuRec.UriDate.ToString(EarthConst.FORMAT_DATE_TIME_9))

        ' 発注書確定
        TextTjHattyuusyoKakutei.Text = IIf(TeibetuRec.HattyuusyoKakuteiFlg = 1, EarthConst.KAKUTEI, EarthConst.MIKAKUTEI)
        ' 発注書金額
        TextTjHattyuusyoKingaku.Text = IIf(TeibetuRec.HattyuusyoGaku = Integer.MinValue, _
                                         "", _
                                         TeibetuRec.HattyuusyoGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' 発注書確認日
        TextTjHattyuusyoKakuninDate.Text = IIf(TeibetuRec.HattyuusyoKakuninDate = Date.MinValue, _
                                           "", _
                                           TeibetuRec.HattyuusyoKakuninDate.ToString(EarthConst.FORMAT_DATE_TIME_9))

        '請求先/仕入先リンクへセット
        Me.ucSeikyuuSiireLinkTui.SetSeikyuuSiireLinkFromTeibetuRec(TeibetuRec)

        ' 更新日時 読み込み時のタイムスタンプ(排他制御用)
        cl.setDispHaitaUpdTime(TeibetuRec.AddDatetime, TeibetuRec.UpdDatetime, Me.HiddenUpdateTeibetuSeikyuuDatetimeTj)

    End Sub

    ''' <summary>
    ''' 改良工事報告書/邸別請求レコード
    ''' </summary>
    ''' <param name="TeibetuRec"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlTeibetuSeikyuuDataKh( _
                                    ByVal TeibetuRec As TeibetuSeikyuuRecord _
                                    )

        ' 売上処理済
        SpanKhUriageSyorizumi.InnerHtml = IIf(TeibetuRec.UriKeijyouDate = Date.MinValue, "", EarthConst.URIAGE_ZUMI)
        ' 売上計上日
        Me.HiddenKhUriageKeijyouDate.Value = cl.GetDisplayString(TeibetuRec.UriKeijyouDate)
        '商品コード
        TextKhSyouhinCd.Text = cl.GetDispStr(TeibetuRec.SyouhinCd)
        '商品名
        SpanKhSyouhinMei.InnerHtml = cl.GetDispStr(TeibetuRec.SyouhinMei)

        ' 請求有無
        SelectKhSeikyuuUmu.SelectedValue = IIf(TeibetuRec.SeikyuuUmu = 1, "1", "0")

        '工務店請求税抜金額
        TextKhKoumutenSeikyuuKingaku.Text = IIf(TeibetuRec.KoumutenSeikyuuGaku = Integer.MinValue, _
                                            0, _
                                            TeibetuRec.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1))

        ' 税率（Hidden）
        HiddenKhZeiritu.Value = TeibetuRec.Zeiritu
        ' 税区分（Hidden）
        HiddenKhZeiKbn.Value = IIf(TeibetuRec.ZeiKbn = "", "", TeibetuRec.ZeiKbn)
        If TeibetuRec.Zeiritu = 0 And TeibetuRec.ZeiKbn = "" Then
            SetKhZeiInfo(TeibetuRec.SyouhinCd)
        End If

        '*****************
        '* 【売上金額】
        '*****************
        '●売上消費税額(消費税)
        TextKhSyouhizei.Text = IIf(TeibetuRec.UriageSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.UriageSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税抜売上金額(実請求税抜金額)
        TextKhJituseikyuuKingaku.Text = IIf(TeibetuRec.UriGaku = Integer.MinValue, 0, Format(TeibetuRec.UriGaku, EarthConst.FORMAT_KINGAKU_1))
        '●税込売上金額(税込額)
        TextKhZeikomiKingaku.Text = IIf(TeibetuRec.ZeikomiUriGaku = Integer.MinValue, 0, Format(TeibetuRec.ZeikomiUriGaku, EarthConst.FORMAT_KINGAKU_1))

        '*****************
        '* 【仕入金額】
        '*****************
        '●仕入消費税額(消費税)
        Me.HiddenKhSiireSyouhiZei.Value = IIf(TeibetuRec.SiireSyouhiZeiGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireSyouhiZeiGaku, EarthConst.FORMAT_KINGAKU_1))
        '●仕入売上金額(税抜金額)
        Me.HiddenKhSiireGaku.Value = IIf(TeibetuRec.SiireGaku = Integer.MinValue, 0, Format(TeibetuRec.SiireGaku, EarthConst.FORMAT_KINGAKU_1))

        ' 請求書発行日
        TextKhSeikyuusyoHakkouDate.Text = IIf(TeibetuRec.SeikyuusyoHakDate = Date.MinValue, _
                                          "", _
                                          TeibetuRec.SeikyuusyoHakDate.ToString(EarthConst.FORMAT_DATE_TIME_9))
        ' 売上年月日
        TextKhUriageNengappi.Text = IIf(TeibetuRec.UriDate = Date.MinValue, _
                                      "", _
                                      TeibetuRec.UriDate.ToString(EarthConst.FORMAT_DATE_TIME_9))

        ' 発注書確定
        TextKhHattyuusyoKakutei.Text = IIf(TeibetuRec.HattyuusyoKakuteiFlg = 1, EarthConst.KAKUTEI, EarthConst.MIKAKUTEI)
        ' 発注書金額
        TextKhHattyuusyoKingaku.Text = IIf(TeibetuRec.HattyuusyoGaku = Integer.MinValue, _
                                         "", _
                                         TeibetuRec.HattyuusyoGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' 発注書確認日
        TextKhHattyuusyoKakuninDate.Text = IIf(TeibetuRec.HattyuusyoKakuninDate = Date.MinValue, _
                                           "", _
                                           TeibetuRec.HattyuusyoKakuninDate.ToString(EarthConst.FORMAT_DATE_TIME_9))

        '再発行理由
        TextKhSaihakkouRiyuu.Text = cl.GetDispStr(TeibetuRec.Bikou)

        '請求先/仕入先リンクへセット
        Me.ucSeikyuuSiireLink.SetSeikyuuSiireLinkFromTeibetuRec(TeibetuRec)

        '請求先タイプの取得設定
        Me.TextKhSeikyuusaki.Text = cl.GetSeikyuuSakiTypeStr( _
                                                        TeibetuRec.SyouhinCd _
                                                        , EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo _
                                                        , Me.HiddenKameitenCd.Value _
                                                        , TeibetuRec.SeikyuuSakiCd _
                                                        , TeibetuRec.SeikyuuSakiBrc _
                                                        , TeibetuRec.SeikyuuSakiKbn)

        ' 更新日時 読み込み時のタイムスタンプ(排他制御用)
        cl.setDispHaitaUpdTime(TeibetuRec.AddDatetime, TeibetuRec.UpdDatetime, Me.HiddenUpdateTeibetuSeikyuuDatetimeKh)

    End Sub
#End Region

#Region "画面コントロールから入力"

    ''' <summary>
    ''' 改良工事/邸別請求レコードデータにコントロールの内容をセットします
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSeikyuuCtrlDataKj() As TeibetuSeikyuuRecord
        ' 商品コード未設定時、何もセットしない
        If SelectKjSyouhinCd.SelectedValue = "" Then
            Return Nothing
        End If

        '邸別請求レコード
        Dim record As New TeibetuSeikyuuRecord

        ' 区分
        record.Kbn = ucGyoumuKyoutuu.Kubun
        ' 番号
        record.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        ' 分類コード(130)
        record.BunruiCd = EarthConst.SOUKO_CD_KAIRYOU_KOUJI
        ' 画面表示NO
        record.GamenHyoujiNo = 1
        ' 商品コード
        record.SyouhinCd = SelectKjSyouhinCd.SelectedValue
        ' 売上金額
        cl.SetDisplayString(TextKjUriageZeinukiKingaku.Text, record.UriGaku)
        ' 仕入金額
        cl.SetDisplayString(TextKjSiireZeinukiKingaku.Text, record.SiireGaku)
        ' 税区分
        record.ZeiKbn = HiddenKjZeiKbn.Value
        ' 税率
        cl.SetDisplayString(HiddenKjZeiritu.Value, record.Zeiritu)
        ' 消費税額
        cl.SetDisplayString(TextKjUriageSyouhizei.Text, record.UriageSyouhiZeiGaku)
        ' 仕入消費税額
        cl.SetDisplayString(TextKjSiireSyouhizei.Text, record.SiireSyouhiZeiGaku)
        ' 請求書発行日
        cl.SetDisplayString(TextKjSeikyuusyoHakkouDate.Text, record.SeikyuusyoHakDate)
        ' 売上年月日
        cl.SetDisplayString(TextKjUriageNengappi.Text, record.UriDate)
        ' 伝票売上年月日(ロジッククラスで自動セット)
        record.DenpyouUriDate = DateTime.MinValue
        ' 請求有無
        record.SeikyuuUmu = IIf(SelectKjSeikyuuUmu.SelectedValue = "1", 1, 0)
        ' 売上計上FLG
        record.UriKeijyouFlg = 0
        ' 売上計上日
        record.UriKeijyouDate = DateTime.MinValue
        ' 確定区分
        record.KakuteiKbn = Integer.MinValue
        ' 備考
        record.Bikou = Nothing
        ' 工務店請求税抜金額
        record.KoumutenSeikyuuGaku = 0
        ' 発注書金額
        cl.SetDisplayString(TextKjHattyuusyoKingaku.Text, record.HattyuusyoGaku)
        ' 発注書確認日
        cl.SetDisplayString(TextKjHattyuusyoKakuninDate.Text, record.HattyuusyoKakuninDate)
        ' 一括入金FLG
        record.IkkatuNyuukinFlg = Integer.MinValue
        ' 調査見積書作成日
        record.TysMitsyoSakuseiDate = DateTime.MinValue
        ' 発注書確定FLG
        record.HattyuusyoKakuteiFlg = IIf(TextKjHattyuusyoKakutei.Text = EarthConst.KAKUTEI, 1, 0)

        '請求先/仕入先リンクから取得
        Me.ucSeikyuuSiireLinkKai.SetTeibetuRecFromSeikyuuSiireLink(record)

        ' 更新ログインユーザID
        record.UpdLoginUserId = userinfo.LoginUserId

        ' 更新日時 読み込み時のタイムスタンプ(排他制御用)
        record.UpdDatetime = cl.getDispHaitaUpdTime(Me.HiddenUpdateTeibetuSeikyuuDatetimeKj)

        Return record
    End Function

    ''' <summary>
    ''' 追加工事(自動設定対象外)/邸別請求レコードデータにコントロールの内容をセットします
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSeikyuuCtrlDataTj() As TeibetuSeikyuuRecord
        ' 商品コード未設定時、何もセットしない
        If SelectTjSyouhinCd.SelectedValue = "" Then
            Return Nothing
        End If

        '邸別請求レコード
        Dim record As New TeibetuSeikyuuRecord

        ' 区分
        record.Kbn = ucGyoumuKyoutuu.Kubun
        ' 保証書NO
        record.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        ' 分類コード(140)
        record.BunruiCd = EarthConst.SOUKO_CD_TUIKA_KOUJI
        ' 画面表示NO
        record.GamenHyoujiNo = 1
        ' 商品コード
        record.SyouhinCd = SelectTjSyouhinCd.SelectedValue
        ' 売上金額
        cl.SetDisplayString(TextTjUriageZeinukiKingaku.Text, record.UriGaku)
        ' 仕入金額
        cl.SetDisplayString(TextTjSiireZeinukiKingaku.Text, record.SiireGaku)
        ' 税区分
        record.ZeiKbn = HiddenTjZeiKbn.Value
        ' 税率
        cl.SetDisplayString(HiddenTjZeiritu.Value, record.Zeiritu)
        ' 消費税額
        cl.SetDisplayString(TextTjUriageSyouhizei.Text, record.UriageSyouhiZeiGaku)
        ' 仕入消費税額
        cl.SetDisplayString(TextTjSiireSyouhizei.Text, record.SiireSyouhiZeiGaku)
        ' 請求書発行日
        cl.SetDisplayString(TextTjSeikyuusyoHakkouDate.Text, record.SeikyuusyoHakDate)
        ' 売上年月日
        cl.SetDisplayString(TextTjUriageNengappi.Text, record.UriDate)
        ' 伝票売上年月日(ロジッククラスで自動セット)
        record.DenpyouUriDate = DateTime.MinValue
        ' 請求有無
        record.SeikyuuUmu = IIf(SelectTjSeikyuuUmu.SelectedValue = "1", 1, 0)
        ' 売上計上FLG
        record.UriKeijyouFlg = 0
        ' 売上計上日
        record.UriKeijyouDate = Date.MinValue
        ' 確定区分
        record.KakuteiKbn = Integer.MinValue
        ' 備考
        record.Bikou = Nothing
        ' 工務店請求税抜金額
        record.KoumutenSeikyuuGaku = 0
        ' 発注書金額
        cl.SetDisplayString(TextTjHattyuusyoKingaku.Text, record.HattyuusyoGaku)
        ' 発注書確認日
        cl.SetDisplayString(TextTjHattyuusyoKakuninDate.Text, record.HattyuusyoKakuninDate)
        ' 一括入金FLG
        record.IkkatuNyuukinFlg = Integer.MinValue
        ' 調査見積書作成日
        record.TysMitsyoSakuseiDate = Date.MinValue
        ' 発注書確定FLG
        record.HattyuusyoKakuteiFlg = IIf(TextTjHattyuusyoKakutei.Text = EarthConst.KAKUTEI, 1, 0)

        '請求先/仕入先リンクから取得
        Me.ucSeikyuuSiireLinkTui.SetTeibetuRecFromSeikyuuSiireLink(record)

        ' 更新ログインユーザID
        record.UpdLoginUserId = userinfo.LoginUserId
        ' 更新日時 読み込み時のタイムスタンプ(排他制御用)
        record.UpdDatetime = cl.getDispHaitaUpdTime(Me.HiddenUpdateTeibetuSeikyuuDatetimeTj)

        Return record
    End Function

    ''' <summary>
    ''' 追加工事(自動設定対象)/邸別請求レコードデータにコントロールの内容をセットします
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSeikyuuCtrlDataTjAuto(ByVal recKoujiKkk As KoujiKakakuRecord) As TeibetuSeikyuuRecord

        '邸別請求レコード
        Dim record As New TeibetuSeikyuuRecord

        ' 区分
        record.Kbn = ucGyoumuKyoutuu.Kubun
        ' 保証書NO
        record.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        ' 分類コード(140)
        record.BunruiCd = EarthConst.SOUKO_CD_TUIKA_KOUJI
        ' 画面表示NO
        record.GamenHyoujiNo = 1
        ' 商品コード(B2009)
        record.SyouhinCd = EarthConst.SH_CD_JIO2
        ' ●売上金額
        If Not recKoujiKkk Is Nothing Then
            '工事価格マスタにある場合
            record.UriGaku = recKoujiKkk.UriGaku
        Else
            '工事価格マスタにない場合
            record.UriGaku = 20000
        End If
        ' 仕入金額
        record.SiireGaku = 0
        ' 税区分
        SetTjZeiInfo(EarthConst.SH_CD_JIO2)
        record.ZeiKbn = HiddenTjZeiKbn.Value
        ' 税率
        cl.SetDisplayString(HiddenTjZeiritu.Value, record.Zeiritu)
        ' 消費税額(レコードクラスで自動算出のため省略)

        ' 請求書発行日▲
        '請求書発行日の自動設定
        '調査会社マスタ.請求締め日
        Dim dtTmp As Date
        dtTmp = cbLogic.GetSeikyuusyoHakkouDateFromTyousa(TextKjKoujiKaisyaCd.Text)
        If dtTmp <> DateTime.MinValue Then
            TextTjSeikyuusyoHakkouDate.Text = dtTmp.ToString(EarthConst.FORMAT_DATE_TIME_9)
        End If
        cl.SetDisplayString(TextTjSeikyuusyoHakkouDate.Text, record.SeikyuusyoHakDate)
        ' 売上年月日
        cl.SetDisplayString(Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9), record.UriDate)
        ' 伝票売上年月日(ロジッククラスで自動セット)
        record.DenpyouUriDate = DateTime.MinValue
        ' ●請求有無
        If Not recKoujiKkk Is Nothing Then
            '工事価格マスタにある場合
            record.SeikyuuUmu = recKoujiKkk.SeikyuuUmu
        Else
            '工事価格マスタにない場合
            record.SeikyuuUmu = 1
        End If
        '売上計上FLG
        record.UriKeijyouFlg = 0
        '売上計上日
        record.UriKeijyouDate = Date.MinValue
        '確定区分
        record.KakuteiKbn = Integer.MinValue
        '備考
        record.Bikou = Nothing
        '工務店請求税抜金額
        record.KoumutenSeikyuuGaku = 0
        ' 発注書金額
        record.HattyuusyoGaku = Integer.MinValue
        ' 発注書確認日
        record.HattyuusyoKakuninDate = DateTime.MinValue
        '一括入金FLG
        record.IkkatuNyuukinFlg = Integer.MinValue
        '調査見積書作成日
        record.TysMitsyoSakuseiDate = Date.MinValue
        ' 発注書確定FLG
        record.HattyuusyoKakuteiFlg = 0
        '請求先コード
        record.SeikyuuSakiCd = Nothing
        '請求先枝番
        record.SeikyuuSakiBrc = Nothing
        '請求先区分
        record.SeikyuuSakiKbn = Nothing
        '調査会社コード
        record.TysKaisyaCd = Nothing
        '調査会社事業所コード
        record.TysKaisyaJigyousyoCd = Nothing
        '更新ログインユーザID
        record.UpdLoginUserId = userinfo.LoginUserId
        ' 更新日時 読み込み時のタイムスタンプ(排他制御用)
        record.UpdDatetime = cl.getDispHaitaUpdTime(Me.HiddenUpdateTeibetuSeikyuuDatetimeTj)

        Return record
    End Function

    ''' <summary>
    ''' 工事報告書再発行/邸別請求レコードデータにコントロールの内容をセットします
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSeikyuuCtrlDataKh() As TeibetuSeikyuuRecord
        ' 商品コード未設定時、何もセットしない
        If TextKhSyouhinCd.Text = "" Then
            Return Nothing
        End If

        '邸別請求レコード
        Dim record As New TeibetuSeikyuuRecord

        ' 区分
        record.Kbn = ucGyoumuKyoutuu.Kubun
        ' 保証書NO
        record.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        ' 分類コード(160)
        record.BunruiCd = EarthConst.SOUKO_CD_KOUJI_HOUKOKUSYO
        ' 画面表示NO
        record.GamenHyoujiNo = 1
        ' 商品コード
        record.SyouhinCd = TextKhSyouhinCd.Text
        ' 売上金額
        cl.SetDisplayString(TextKhJituseikyuuKingaku.Text, record.UriGaku)
        ' 仕入金額
        cl.SetDisplayString(HiddenKhSiireGaku.Value, record.SiireGaku)
        ' 税区分
        record.ZeiKbn = HiddenKhZeiKbn.Value
        ' 税率
        cl.SetDisplayString(HiddenKhZeiritu.Value, record.Zeiritu)
        ' 消費税額
        cl.SetDisplayString(TextKhSyouhizei.Text, record.UriageSyouhiZeiGaku)
        ' 仕入消費税額
        cl.SetDisplayString(HiddenKhSiireSyouhiZei.Value, record.SiireSyouhiZeiGaku)
        ' 請求書発行日
        cl.SetDisplayString(TextKhSeikyuusyoHakkouDate.Text, record.SeikyuusyoHakDate)
        ' 売上年月日
        cl.SetDisplayString(TextKhUriageNengappi.Text, record.UriDate)
        ' 伝票売上年月日(ロジッククラスで自動セット)
        record.DenpyouUriDate = DateTime.MinValue
        ' 請求有無
        record.SeikyuuUmu = IIf(SelectKhSeikyuuUmu.SelectedValue = "1", 1, 0)
        ' 売上計上FLG
        record.UriKeijyouFlg = 0
        ' 売上計上日
        record.UriKeijyouDate = Date.MinValue
        ' 確定区分
        record.KakuteiKbn = Integer.MinValue
        ' 備考
        record.Bikou = TextKhSaihakkouRiyuu.Text
        ' 工務店請求税抜金額
        cl.SetDisplayString(TextKhKoumutenSeikyuuKingaku.Text, record.KoumutenSeikyuuGaku)
        ' 発注書金額
        cl.SetDisplayString(TextKhHattyuusyoKingaku.Text, record.HattyuusyoGaku)
        ' 発注書確認日
        cl.SetDisplayString(TextKhHattyuusyoKakuninDate.Text, record.HattyuusyoKakuninDate)
        ' 一括入金FLG
        record.IkkatuNyuukinFlg = Integer.MinValue
        ' 調査見積書作成日
        record.TysMitsyoSakuseiDate = Date.MinValue
        ' 発注書確定FLG
        record.HattyuusyoKakuteiFlg = IIf(TextKhHattyuusyoKakutei.Text = EarthConst.KAKUTEI, 1, 0)

        '請求先/仕入先リンクから取得
        Me.ucSeikyuuSiireLink.SetTeibetuRecFromSeikyuuSiireLink(record)

        ' 更新ログインユーザID
        record.UpdLoginUserId = userinfo.LoginUserId
        ' 更新日時 読み込み時のタイムスタンプ(排他制御用)
        record.UpdDatetime = cl.getDispHaitaUpdTime(Me.HiddenUpdateTeibetuSeikyuuDatetimeKh)

        Return record
    End Function

    ''' <summary>
    ''' 追加工事/自動設定時における地盤データコピー
    ''' 改良工事タブ＝＞追加工事タブ
    ''' </summary>
    ''' <param name="jibanRec"></param>
    ''' <remarks></remarks>
    Private Sub SetJibanDataTjAuto(ByRef jibanRec As JibanRecordKairyouKouji, ByVal recKoujiKkk As KoujiKakakuRecord)

        jibanRec.TKojKaisyaCd = jibanRec.KojGaisyaCd '工事会社コード
        jibanRec.TKojKaisyaJigyousyoCd = jibanRec.KojGaisyaJigyousyoCd '工事会社事業所コード
        jibanRec.TKojSyubetu = jibanRec.KairyKojSyubetu        '工事種別
        jibanRec.TKojKanryYoteiDate = jibanRec.KairyKojKanryYoteiDate '完了予定日
        jibanRec.TKojDate = jibanRec.KairyKojDate '工事日
        jibanRec.TKojSokuhouTykDate = jibanRec.KairyKojSokuhouTykDate '完工速報着日

        '工事会社請求
        If Not recKoujiKkk Is Nothing Then
            '工事価格マスタにある場合
            If recKoujiKkk.KojGaisyaSeikyuuUmu = 1 Then
                Me.CheckBoxTjKoujiKaisyaSeikyuu.Checked = True
            Else
                Me.CheckBoxTjKoujiKaisyaSeikyuu.Checked = False
            End If
        Else
            '工事価格マスタにない場合
            CheckBoxTjKoujiKaisyaSeikyuu.Checked = True '工事会社請求(自動設定時はチェック) ※後続処理でセット
        End If


    End Sub

    ''' <summary>
    ''' 工事価格レコード取得
    ''' </summary>
    ''' <param name="strKameiCd">加盟店コード</param>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strKojGaisyaCd">工事会社コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKoujiKakakuRec(ByVal strKameiCd As String, _
                                       ByVal strEigyousyoCd As String, _
                                       ByVal strKeiretuCd As String, _
                                       ByVal strSyouhinCd As String, _
                                       ByVal strKojGaisyaCd As String) As KoujiKakakuRecord

        Dim keyRec As New KoujiKakakuKeyRecord          '検索キー用レコード
        Dim resultRec As New KoujiKakakuRecord          '結果取得用レコード
        Dim lgcKouji As New KairyouKoujiLogic           '改良工事ロジック
        Dim intResult As Integer = Integer.MinValue     '取得結果ステータス用

        '取得に必要な画面項目のセット
        keyRec = cbLogic.GetKojKkkMstKeyRec(strKameiCd, strEigyousyoCd, strKeiretuCd, strSyouhinCd, strKojGaisyaCd)

        '工事会社価格ロジックより取得
        intResult = lgcKouji.GetKoujiKakakuRecord(keyRec, resultRec)

        '取得ステータスチェック(工事価格マスタにない場合、商品マスタは対象外)
        If intResult <= EarthEnum.emKoujiKakaku.SiteiNasi Then
        Else
            Return Nothing
            Exit Function
        End If

        Return resultRec

    End Function

#End Region

#End Region

    ''' <summary>
    ''' 登録/修正 実行ボタン１,２押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTouroku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTouroku1.ServerClick _
                                                                                                                , ButtonTouroku2.ServerClick
        Dim tmpScript As String = ""

        ' 入力チェック
        '共通情報
        If ucGyoumuKyoutuu.checkInput() = False Then Exit Sub

        '調査報告書情報
        If checkInput() = False Then Exit Sub

        ' 画面の内容をDBに反映する
        If SaveData() Then '登録成功
            'キー情報の取得
            _kbn = ucGyoumuKyoutuu.Kubun
            _no = ucGyoumuKyoutuu.Bangou

            ' パラメータ不足時は画面を表示しない
            If _kbn Is Nothing Or _no Is Nothing Then
                Response.Redirect(UrlConst.MAIN)
            End If

            '完了メッセージ兼、次物件指定ポップアップ表示のためにフラグをセット
            '登録完了後、画面をリロードするために、キー情報を引き渡す
            Context.Items("kbn") = ucGyoumuKyoutuu.Kubun
            Context.Items("no") = ucGyoumuKyoutuu.Bangou
            Context.Items("modal") = Boolean.TrueString

            '画面遷移（リロード）
            Server.Transfer(UrlConst.KAIRYOU_KOUJI)

        Else '登録失敗
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "登録/修正") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonTouroku_ServerClick", tmpScript, True)

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
    Public Function checkInput(Optional ByVal flgNextGamen As Boolean = False) As Boolean
        Dim e As New System.EventArgs
        Dim KjLogic As New KairyouKoujiLogic

        '地盤画面共通クラス
        Dim jBn As New Jiban

        '全てのコントロールを有効化(破棄種別チェック用)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        'エラーメッセージ初期化
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New ArrayList

        If ucGyoumuKyoutuu.AccDataHakiSyubetu.SelectedValue >= "1" Then
            '破棄種別が選択されている場合、スルー
        Else
            '●コード入力値変更チェック
            '工事会社コード
            If TextKjKoujiKaisyaCd.Text <> HiddenKjKoujiKaisyaCdMae.Value Or (TextKjKoujiKaisyaCd.Text <> "" And Me.TextKjKoujiKaisyaMei.Text = "") Then
                errMess += Messages.MSG030E.Replace("@PARAM1", "工事会社コード")
                arrFocusTargetCtrl.Add(ButtonKjKoujiKaisyaSearch)
            End If
            '追加工事会社コード
            If TextTjKoujiKaisyaCd.Text <> HiddenTjKoujiKaisyaCdMae.Value Or (TextTjKoujiKaisyaCd.Text <> "" And Me.TextTjKoujiKaisyaMei.Text = "") Then
                errMess += Messages.MSG030E.Replace("@PARAM1", "追加工事会社コード")
                arrFocusTargetCtrl.Add(ButtonTjKoujiKaisyaSearch)
            End If

            '●必須チェック
            '******************************
            '* <改良工事>
            '******************************
            '(Chk27:<改良工事画面><改良工事>商品コード＝"B2000番台"、かつ、<改良工事画面><改良工事>売上年月日＝入力、かつ、<改良工事画面><改良工事>売上処理済<>"売上処理済"の場合、チェックを行う。)
            If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) _
                And TextKjUriageNengappi.Text <> "" _
                    And SpanKjUriageSyoriZumi.InnerHtml <> EarthConst.URIAGE_ZUMI _
                        Then
                '工事仕様確認
                If SelectKjKoujiSiyouKakunin.SelectedValue = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "工事仕様確認")
                    arrFocusTargetCtrl.Add(SelectKjKoujiSiyouKakunin)
                End If
            End If

            '(Chk23:<改良工事画面><改良工事>商品コード＝"B2008"（JIO固定ｺｰﾄﾞ1）、かつ、<改良工事画面><改良工事>売上年月日＝入力の場合、チェックを行う。
            If SelectKjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO And TextKjUriageNengappi.Text <> "" Then
                '工事会社コード
                If TextKjKoujiKaisyaCd.Text = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "工事会社コード")
                    arrFocusTargetCtrl.Add(TextKjKoujiKaisyaCd)
                End If
                '改良工事種別
                If SelectKjKairyouKoujiSyubetu.SelectedValue = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "改良工事種別")
                    arrFocusTargetCtrl.Add(SelectKjKairyouKoujiSyubetu)
                End If
            End If

            '******************************
            '* <追加工事>
            '******************************
            '(Chk24:<改良工事画面><追加工事>商品コード＝"B2009"（JIO固定ｺｰﾄﾞ2）の場合、チェックを行う。
            If SelectTjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO2 Then
                '追加工事会社コード
                If TextTjKoujiKaisyaCd.Text = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "追加工事会社コード")
                    arrFocusTargetCtrl.Add(TextTjKoujiKaisyaCd)
                End If
                '追加改良工事種別
                If SelectTjKairyouKoujiSyubetu.SelectedValue = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "追加改良工事種別")
                    arrFocusTargetCtrl.Add(SelectTjKairyouKoujiSyubetu)
                End If
            End If

            '******************************
            '* <改良工事報告書情報>
            '******************************
            '(Chk21:報告書画面.工事報告書再発行日＝入力の場合、チェックを行う。)
            If TextKhSaihakkouDate.Text <> "" Then
                '請求
                If SelectKhSeikyuuUmu.SelectedValue = "" Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "請求")
                    arrFocusTargetCtrl.Add(SelectKhSeikyuuUmu)
                End If
                '再発行理由
                If TextKhSaihakkouRiyuu.Text.Length = 0 Then
                    errMess += Messages.MSG013E.Replace("@PARAM1", "再発行理由")
                    arrFocusTargetCtrl.Add(TextKhSaihakkouRiyuu)
                End If

                '(Chk22:報告書画面.工事報告書再発行日＝入力、かつ、報告書画面.<工事報告書再発行>請求有無＝有りの場合、チェックを行う。)
                '請求有無
                If SelectKhSeikyuuUmu.SelectedValue = "1" Then '有
                    '実請求税抜金額
                    If TextKhJituseikyuuKingaku.Text.Length = 0 Then
                        errMess += Messages.MSG013E.Replace("@PARAM1", "実請求税抜金額")
                        arrFocusTargetCtrl.Add(TextKhJituseikyuuKingaku)
                    End If
                    '請求書発行日
                    If TextKhSeikyuusyoHakkouDate.Text.Length = 0 Then
                        errMess += Messages.MSG013E.Replace("@PARAM1", "請求書発行日")
                        arrFocusTargetCtrl.Add(TextKhSeikyuusyoHakkouDate)
                    End If

                End If

            End If

        End If

        '●日付チェック
        'データ破棄日
        If ucGyoumuKyoutuu.AccDataHakiDate.Value <> "" Then
            If cl.checkDateHanni(ucGyoumuKyoutuu.AccDataHakiDate.Value) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "データ破棄日")
                arrFocusTargetCtrl.Add(ucGyoumuKyoutuu.AccDataHakiDate)
            End If
        End If
        '****************
        '* 改良工事
        '****************
        '確認日
        If TextKjKakuninDate.Text <> "" Then
            If cl.checkDateHanni(TextKjKakuninDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "確認日")
                arrFocusTargetCtrl.Add(TextKjKakuninDate)
            End If
        End If
        '完了予定日
        If TextKjKanryouYoteiDate.Text <> "" Then
            If cl.checkDateHanni(TextKjKanryouYoteiDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "完了予定日")
                arrFocusTargetCtrl.Add(TextKjKanryouYoteiDate)
            End If
        End If
        '工事日
        If TextKjKoujiDate.Text <> "" Then
            If cl.checkDateHanni(TextKjKoujiDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "工事日")
                arrFocusTargetCtrl.Add(TextKjKoujiDate)
            End If
            If Me.TextKjKoujiDate.Text <> Me.HiddenKjKoujiDateOld.Value Then '未来日付は入力禁止
                If Me.TextKjKoujiDate.Text > Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) Then
                    errMess += Messages.MSG204E.Replace("@PARAM1", "工事日")
                    arrFocusTargetCtrl.Add(Me.TextKjKoujiDate)
                End If
            End If
        End If
        '完工速報着日
        If TextKjKankouSokuhouTyakuDate.Text <> "" Then
            If cl.checkDateHanni(TextKjKankouSokuhouTyakuDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "完工速報着日")
                arrFocusTargetCtrl.Add(TextKjKankouSokuhouTyakuDate)
            End If
            If Me.TextKjKankouSokuhouTyakuDate.Text <> Me.HiddenKjKankouSokuhouTyakuDateOld.Value Then '未来日付は入力禁止
                If Me.TextKjKankouSokuhouTyakuDate.Text > Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) Then
                    errMess += Messages.MSG204E.Replace("@PARAM1", "完工速報着日")
                    arrFocusTargetCtrl.Add(Me.TextKjKankouSokuhouTyakuDate)
                End If
            End If
        End If
        '請求書発行日
        If TextKjSeikyuusyoHakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextKjSeikyuusyoHakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "請求書発行日")
                arrFocusTargetCtrl.Add(TextKjSeikyuusyoHakkouDate)
            End If
        End If
        '****************
        '* 追加工事
        '****************
        '完了予定日
        If TextTjKanryouYoteiDate.Text <> "" Then
            If cl.checkDateHanni(TextTjKanryouYoteiDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "完了予定日")
                arrFocusTargetCtrl.Add(TextTjKanryouYoteiDate)
            End If
        End If
        '工事日
        If TextTjKoujiDate.Text <> "" Then
            If cl.checkDateHanni(TextTjKoujiDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "工事日")
                arrFocusTargetCtrl.Add(TextTjKoujiDate)
            End If
            If Me.TextTjKoujiDate.Text <> Me.HiddenTjKoujiDateOld.Value Then '未来日付は入力禁止
                If Me.TextTjKoujiDate.Text > Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) Then
                    errMess += Messages.MSG204E.Replace("@PARAM1", "工事日")
                    arrFocusTargetCtrl.Add(Me.TextTjKoujiDate)
                End If
            End If
        End If
        '完工速報着日
        If TextTjKankouSokuhouTyakuDate.Text <> "" Then
            If cl.checkDateHanni(TextTjKankouSokuhouTyakuDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "完工速報着日")
                arrFocusTargetCtrl.Add(TextTjKankouSokuhouTyakuDate)
            End If
            If Me.TextTjKankouSokuhouTyakuDate.Text <> Me.HiddenTjKankouSokuhouTyakuDateOld.Value Then '未来日付は入力禁止
                If Me.TextTjKankouSokuhouTyakuDate.Text > Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) Then
                    errMess += Messages.MSG204E.Replace("@PARAM1", "完工速報着日")
                    arrFocusTargetCtrl.Add(Me.TextTjKankouSokuhouTyakuDate)
                End If
            End If
        End If
        '請求書発行日
        If TextTjSeikyuusyoHakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextTjSeikyuusyoHakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "請求書発行日")
                arrFocusTargetCtrl.Add(TextTjSeikyuusyoHakkouDate)
            End If
        End If
        '****************
        '* 工事報告書再発行
        '****************
        '請求書発行日
        If TextKhSeikyuusyoHakkouDate.Text <> "" Then
            If cl.checkDateHanni(TextKhSeikyuusyoHakkouDate.Text) = False Then
                errMess += Messages.MSG014E.Replace("@PARAM1", "請求書発行日")
                arrFocusTargetCtrl.Add(TextKhSeikyuusyoHakkouDate)
            End If
        End If

        '●桁数チェック
        '工事会社
        If jBn.SuutiStrCheck(TextKjKoujiKaisyaCd.Text, 7, 0) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "工事会社").Replace("@PARAM2", "7").Replace("@PARAM3", "0")
            arrFocusTargetCtrl.Add(TextKjKoujiKaisyaCd)
        End If
        '追加工事会社
        If jBn.SuutiStrCheck(TextTjKoujiKaisyaCd.Text, 7, 0) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", "追加工事会社").Replace("@PARAM2", "7").Replace("@PARAM3", "0")
            arrFocusTargetCtrl.Add(TextTjKoujiKaisyaCd)
        End If

        '●禁則文字チェック(文字列入力フィールドが対象)
        '******************************
        '* <改良工事報告書情報>
        '******************************
        '受理詳細
        If jBn.KinsiStrCheck(TextKhJuriSyousai.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "受理詳細")
            arrFocusTargetCtrl.Add(TextKhJuriSyousai)
        End If
        '再発行理由
        If jBn.KinsiStrCheck(TextKhSaihakkouRiyuu.Text) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "再発行理由")
            arrFocusTargetCtrl.Add(TextKhSaihakkouRiyuu)
        End If

        '●バイト数チェック(文字列入力フィールドが対象)
        '******************************
        '* <改良工事報告書情報>
        '******************************
        '受理詳細
        If jBn.ByteCheckSJIS(TextKhJuriSyousai.Text, TextKhJuriSyousai.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "受理詳細")
            arrFocusTargetCtrl.Add(TextKhJuriSyousai)
        End If
        '再発行理由
        If jBn.ByteCheckSJIS(TextKhSaihakkouRiyuu.Text, TextKhSaihakkouRiyuu.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "再発行理由")
            arrFocusTargetCtrl.Add(TextKhSaihakkouRiyuu)
        End If

        '●その他チェック
        '******************************
        '* <改良工事,追加工事>
        '******************************
        '(Chk25:<改良工事画面><改良工事>商品コード＝入力、かつ、<改良工事画面><追加工事>商品コード＝入力、かつ、以下の条件のいずれかに当てはまる場合、エラーとする。)
        If SelectKjSyouhinCd.SelectedValue <> "" And SelectTjSyouhinCd.SelectedValue <> "" Then
            '・<改良工事画面><改良工事>商品コード＝"B2008"（JIO固定ｺｰﾄﾞ1）、かつ、<改良工事画面><追加工事>商品コード≠"B2009"（JIO固定ｺｰﾄﾞ2）
            If SelectKjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO And SelectTjSyouhinCd.SelectedValue <> EarthConst.SH_CD_JIO2 Then
                errMess += Messages.MSG077W
                arrFocusTargetCtrl.Add(SelectTjSyouhinCd)
            End If
            '・<改良工事画面><改良工事>商品コード≠"B2008"（JIO固定ｺｰﾄﾞ1）、かつ、<改良工事画面><追加工事>商品コード＝"B2009"（JIO固定ｺｰﾄﾞ2）
            If SelectKjSyouhinCd.SelectedValue <> EarthConst.SH_CD_JIO And SelectTjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO2 Then
                errMess += Messages.MSG077W
                arrFocusTargetCtrl.Add(SelectKjSyouhinCd)
            End If
        End If

        '(Chk26:工事画面でB2008の時は工事会社を999800に選択したら登録時エラーにする)
        '改良工事.商品コード
        If SelectKjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO Then
            '改良工事.工事会社コード
            If TextKjKoujiKaisyaCd.Text = EarthConst.KOJ_K_CD_JIO Then '工事仕様(999800)
                errMess += Messages.MSG101E
                arrFocusTargetCtrl.Add(TextKjKoujiKaisyaCd)
            End If
        End If

        '(Chk28:<改良工事画面><改良工事>商品コード＝"B2008"（JIO固定ｺｰﾄﾞ1）、かつ、改良工事.工事会社に入力がある場合に、以下の条件のチェックを行なう。
        '<改良工事画面><共通情報>加盟店コード＋<改良工事画面><改良工事>工事会社(調査会社コード)が加盟店調査会社設定マスタに指定工事会社の登録がない場合、エラーとする。)
        '改良工事.商品コード
        If SelectKjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO Then
            '改良工事.工事会社=入力かつ、改良工事.完工速報着日=入力
            If TextKjKoujiKaisyaCd.Text <> String.Empty And Me.TextKjKankouSokuhouTyakuDate.Text <> String.Empty Then
                '改良工事.工事会社コード
                If KjLogic.ChkExistSiteiKoujiGaisya(ucGyoumuKyoutuu.AccKameitenCd.Value, TextKjKoujiKaisyaCd.Text) = False Then '指定調査会社のチェック
                    errMess += Messages.MSG114E
                    arrFocusTargetCtrl.Add(TextKjKoujiKaisyaCd)
                End If
            End If

        End If

        '******************************
        '* <改良工事>
        '******************************
        '(Chk05:<改良工事画面>改良売上金額(税抜)＜<改良工事画面>改良仕入金額(税抜)、かつ、登録許可していない場合、確認メッセージを表示する。)
        '（入力内容が変更されている場合は、許可を無効にし、確認メッセージを表示する）	
        '確認OKの場合、登録許可する。	
        '<改良工事画面>追加改良売上金額、追加改良仕入金額も同様のチェックを行う。	
        '売上金額/税抜金額=>JSにてチェック

        '(Chk14:<改良工事画面><改良工事>完工速報着日＝入力、かつ、新規登録の場合、確認メッセージを表示する。)
        '確認OKの場合、登録許可する。（2度目からは、確認メッセージを表示せずに、無条件で登録許可する）
        '完工速報着日=>JSにてチェック

        '(Chk08:工事画面.<改良工事>請求書発行日＝入力、かつ、改良工事日＝未入力の場合、確認メッセージを表示する。)
        '確認OKの場合、登録許可する。（2度目からは、確認メッセージを表示せずに、無条件で登録許可する）	
        '請求書発行日=>JSにてチェック

        '(Chk09:工事画面.<改良工事>請求書発行日＜改良工事日の場合、確認メッセージを表示する。)
        '確認OKの場合、登録許可する。（2度目からは、確認メッセージを表示せずに、無条件で登録許可する）	
        '請求書発行日=>JSにてチェック

        '(Chk16:請求有無＝有り、かつ、売上年月日＝設定済、かつ、請求書発行日＝未入力の場合、エラーとする。)
        '請求書発行日
        If SelectKjSeikyuuUmu.SelectedValue = "1" And TextKjUriageNengappi.Text <> "" And TextKjSeikyuusyoHakkouDate.Text = "" Then
            errMess += Messages.MSG062E.Replace("@PARAM1", "請求書発行日")
            arrFocusTargetCtrl.Add(TextKjSeikyuusyoHakkouDate)
        End If

        '******************************
        '* <追加工事>
        '******************************
        '(Chk05:<改良工事画面>改良売上金額(税抜)＜<改良工事画面>改良仕入金額(税抜)、かつ、登録許可していない場合、確認メッセージを表示する。)
        '（入力内容が変更されている場合は、許可を無効にし、確認メッセージを表示する）	
        '確認OKの場合、登録許可する。	
        '<改良工事画面>追加改良売上金額、追加改良仕入金額も同様のチェックを行う。	
        '売上金額/税抜金額=>JSにてチェック

        '(Chk15:<改良工事画面><追加工事>追加完工速報着日＝入力、かつ、新規登録の場合、確認メッセージを表示する。)
        '確認OKの場合、登録許可する。（2度目からは、確認メッセージを表示せずに、無条件で登録許可する）
        '完工速報着日=>JSにてチェック

        '(Chk10:<改良工事画面><追加工事>請求書発行日＝入力、かつ、追加改良工事日＝未入力の場合、確認メッセージを表示する。)
        '確認OKの場合、登録許可する。（2度目からは、確認メッセージを表示せずに、無条件で登録許可する）	
        '請求書発行日=>JSにてチェック

        '(Chk11:<改良工事画面><追加工事>請求書発行日＜追加改良工事日の場合、確認メッセージを表示する。)
        '確認OKの場合、登録許可する。（2度目からは、確認メッセージを表示せずに、無条件で登録許可する）	
        '請求書発行日=>JSにてチェック

        '(Chk16:請求有無＝有り、かつ、売上年月日＝設定済、かつ、請求書発行日＝未入力の場合、エラーとする。)
        '請求書発行日
        If SelectTjSeikyuuUmu.SelectedValue = "1" And TextTjUriageNengappi.Text <> "" And TextTjSeikyuusyoHakkouDate.Text = "" Then
            errMess += Messages.MSG062E.Replace("@PARAM1", "請求書発行日")
            arrFocusTargetCtrl.Add(TextTjSeikyuusyoHakkouDate)
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
        '地盤データは更新対象のみ
        '邸別請求データは全て更新
        '*************************

        Dim JibanLogic As New JibanLogic
        Dim jrOld As New JibanRecord
        ' 現在の地盤データをDBから取得する
        jrOld = JibanLogic.GetJibanData(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou)

        ' 画面内容より地盤レコードを生成する
        Dim jibanRec As New JibanRecordKairyouKouji

        ' 邸別データ修正用のロジッククラス
        Dim logic As New KairyouKoujiLogic

        '進捗T更新用に、DB上の値をセットする
        JibanLogic.SetSintyokuJibanData(jrOld, jibanRec)

        '商品1～3のコピー
        JibanLogic.ps_CopyTeibetuSyouhinData(jrOld, jibanRec)

        '***************************************
        ' 地盤データ(共通情報)
        '***************************************
        ' 区分
        jibanRec.Kbn = ucGyoumuKyoutuu.Kubun
        ' 番号（保証書NO）
        jibanRec.HosyousyoNo = ucGyoumuKyoutuu.Bangou
        ' データ破棄種別
        cl.SetDisplayString(ucGyoumuKyoutuu.DataHakiSyubetu, jibanRec.DataHakiSyubetu)
        ' データ破棄日
        If ucGyoumuKyoutuu.DataHakiSyubetu = "0" Then
            jibanRec.DataHakiDate = DateTime.MinValue
        Else
            cl.SetDisplayString(ucGyoumuKyoutuu.DataHakiDate, jibanRec.DataHakiDate)
        End If
        ' 施主名
        jibanRec.SesyuMei = ucGyoumuKyoutuu.SesyuMei
        ' 物件住所1
        jibanRec.BukkenJyuusyo1 = ucGyoumuKyoutuu.Jyuusyo1
        ' 物件住所2
        jibanRec.BukkenJyuusyo2 = ucGyoumuKyoutuu.Jyuusyo2
        ' 物件住所3
        jibanRec.BukkenJyuusyo3 = ucGyoumuKyoutuu.Jyuusyo3
        ' 備考
        jibanRec.Bikou = ucGyoumuKyoutuu.Bikou
        ' 備考2
        jibanRec.Bikou2 = ucGyoumuKyoutuu.Bikou2
        ' 更新者ユーザーID
        jibanRec.UpdLoginUserId = userinfo.LoginUserId
        ' 更新日時 読み込み時のタイムスタンプ
        If ucGyoumuKyoutuu.AccupdateDateTime.Value = "" Then
            jibanRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
        Else
            jibanRec.UpdDatetime = DateTime.ParseExact(ucGyoumuKyoutuu.AccupdateDateTime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
        End If
        '調査方法コード
        jibanRec.TysHouhou = ucGyoumuKyoutuu.TyousaHouhouCd
        '調査方法名
        jibanRec.TysHouhouMeiIf = ucGyoumuKyoutuu.TyousaHouhouMei
        '調査会社コード
        jibanRec.TysKaisyaCd = ucGyoumuKyoutuu.TyousaKaishaCd
        '調査会社事業所コード
        jibanRec.TysKaisyaJigyousyoCd = ucGyoumuKyoutuu.TyousaKaishaJigyousyoCd
        '調査会社名
        jibanRec.TysKaisyaMeiIf = ucGyoumuKyoutuu.TyousaKaishaMei
        '加盟店コード
        jibanRec.KameitenCd = ucGyoumuKyoutuu.AccKameitenCd.Value
        '加盟店名
        jibanRec.KameitenMeiIf = ucGyoumuKyoutuu.KameitenMei
        '加盟店Tel
        jibanRec.KameitenTelIf = ucGyoumuKyoutuu.KameitenTel
        '加盟店Fax
        jibanRec.KameitenFaxIf = ucGyoumuKyoutuu.KameitenFax
        '加盟店Mail
        jibanRec.KameitenMailIf = ucGyoumuKyoutuu.KameitenMail
        '構造名
        jibanRec.KouzouMeiIf = ucGyoumuKyoutuu.KouzouMei

        '***************************************
        ' 地盤データ(改良工事情報)
        '***************************************
        '工事担当者コード
        cl.SetDisplayString(TextKoujiTantousyaCd.Text, jibanRec.SyouninsyaCd)

        '**********************
        ' 地盤データ(改良工事)
        '**********************
        '工事仕様確認
        cl.SetDisplayString(SelectKjKoujiSiyouKakunin.SelectedValue, jibanRec.KojSiyouKakunin)
        '確認日
        cl.SetDisplayString(TextKjKakuninDate.Text, jibanRec.KojSiyouKakuninDate)
        '工事会社等
        Dim tmpTys As String = TextKjKoujiKaisyaCd.Text
        If tmpTys.Length >= 6 Then   '長さ6以上必須
            jibanRec.KojGaisyaCd = tmpTys.Substring(0, tmpTys.Length - 2) '工事会社コード
            jibanRec.KojGaisyaJigyousyoCd = tmpTys.Substring(tmpTys.Length - 2, 2) '工事会社事業所コード
        End If
        '工事会社請求
        Dim intTmp1 As Integer = IIf(CheckBoxKjKoujiKaisyaSeikyuu.Checked = True, 1, Integer.MinValue)
        cl.SetDisplayString(intTmp1, jibanRec.KojGaisyaSeikyuuUmu)
        '改良工事種別
        cl.SetDisplayString(SelectKjKairyouKoujiSyubetu.SelectedValue, jibanRec.KairyKojSyubetu)
        '完了予定日
        cl.SetDisplayString(TextKjKanryouYoteiDate.Text, jibanRec.KairyKojKanryYoteiDate)
        '工事日
        cl.SetDisplayString(TextKjKoujiDate.Text, jibanRec.KairyKojDate)
        '完工速報着日
        cl.SetDisplayString(TextKjKankouSokuhouTyakuDate.Text, jibanRec.KairyKojSokuhouTykDate)


        '***************************************
        ' 邸別請求データ(改良工事)
        '***************************************
        jibanRec.KairyouKoujiRecord = GetSeikyuuCtrlDataKj()

        '**********************
        ' 地盤データ(追加工事)
        '**********************
        '工事会社等
        Dim tmpTysTuika As String = TextTjKoujiKaisyaCd.Text
        If tmpTysTuika.Length >= 6 Then   '長さ6以上必須
            jibanRec.TKojKaisyaCd = tmpTysTuika.Substring(0, tmpTysTuika.Length - 2) '工事会社コード
            jibanRec.TKojKaisyaJigyousyoCd = tmpTysTuika.Substring(tmpTysTuika.Length - 2, 2) '工事会社事業所コード
        End If
        '改良工事種別
        cl.SetDisplayString(SelectTjKairyouKoujiSyubetu.SelectedValue, jibanRec.TKojSyubetu)
        '完了予定日
        cl.SetDisplayString(TextTjKanryouYoteiDate.Text, jibanRec.TKojKanryYoteiDate)
        '工事日
        cl.SetDisplayString(TextTjKoujiDate.Text, jibanRec.TKojDate)
        '完工速報着日
        cl.SetDisplayString(TextTjKankouSokuhouTyakuDate.Text, jibanRec.TKojSokuhouTykDate)

        '***************************************
        ' 邸別請求データ(追加工事)
        '***************************************
        '追加工事データ自動設定チェック
        Dim recKoujiKkk As New KoujiKakakuRecord
        '工事画面.<改良工事>商品コード＝"B2008"（JIO固定ｺｰﾄﾞ1）、且つ工事画面.<改良工事>売上年月日＝入力の場合
        If SelectKjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO And TextKjUriageNengappi.Text <> String.Empty Then

            '邸別請求レコード(分類コード=140)の存在チェック
            If logic.ChkTjDataAutoSetting(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.Bangou) = False Then
                jibanRec.TuikaKoujiRecord = GetSeikyuuCtrlDataTj() '自動設定対象外

            Else '上記以外の場合

                '完工速報着日
                If Me.TextKjKankouSokuhouTyakuDate.Text = String.Empty Then '未入力
                    jibanRec.TuikaKoujiRecord = GetSeikyuuCtrlDataTj() '自動設定対象外

                Else '入力
                    recKoujiKkk = GetKoujiKakakuRec(jrOld.KameitenCd, _
                                                    jrOld.EigyousyoCd, _
                                                    jrOld.KeiretuCd, _
                                                    EarthConst.SH_CD_JIO2, _
                                                    jibanRec.KojGaisyaCd + jibanRec.KojGaisyaJigyousyoCd)
                    SetJibanDataTjAuto(jibanRec, recKoujiKkk) '自動設定時における地盤データコピー
                    jibanRec.TuikaKoujiRecord = GetSeikyuuCtrlDataTjAuto(recKoujiKkk) '自動設定対象
                End If

            End If

        Else '上記以外の場合
            jibanRec.TuikaKoujiRecord = GetSeikyuuCtrlDataTj() '自動設定対象外

        End If

        '工事会社請求
        Dim intTmp2 As Integer = IIf(CheckBoxTjKoujiKaisyaSeikyuu.Checked = True, 1, Integer.MinValue)
        cl.SetDisplayString(intTmp2, jibanRec.TKojKaisyaSeikyuuUmu)

        '***************************************
        ' 地盤データ(改良工事報告書情報)
        '***************************************
        '受理
        cl.SetDisplayString(SelectKhJuri.SelectedValue, jibanRec.KojHkksUmu)
        '受理詳細
        jibanRec.KojHkksJuriSyousai = TextKhJuriSyousai.Text
        '受理日
        cl.SetDisplayString(TextKhJuriDate.Text, jibanRec.KojHkksJuriDate)
        '発送日
        cl.SetDisplayString(TextKhHassouDate.Text, jibanRec.KojHkksHassouDate)
        '再発行日
        cl.SetDisplayString(TextKhSaihakkouDate.Text, jibanRec.KojHkksSaihakDate)

        '***************************************
        ' 邸別請求データ(改良工事報告書)
        '***************************************
        jibanRec.KoujiHoukokusyoRecord = GetSeikyuuCtrlDataKh()

        '***************************************
        ' 画面入力項目以外
        '***************************************
        '更新者
        jibanRec.Kousinsya = cbLogic.GetKousinsya(userinfo.LoginUserId, DateTime.Now)

        '************************************************
        ' 保証書発行状況、保証書発行状況設定日、保証商品有無の自動設定
        '************************************************
        '商品の自動設定後に行なう
        cbLogic.ps_AutoSetHosyousyoHakJyjy(EarthEnum.emGamenInfo.KairyouKouji, jibanRec)

        '物件履歴データの自動セット
        Dim brRec As BukkenRirekiRecord = cbLogic.MakeBukkenRirekiRecHosyouUmu(jibanRec, cl.GetDisplayString(jrOld.HosyouSyouhinUmu), cl.GetDisplayString(jrOld.KeikakusyoSakuseiDate))

        If Not brRec Is Nothing Then
            '物件履歴レコードのチェック
            Dim strErrMsg As String = String.Empty
            If cbLogic.checkInputBukkenRireki(brRec, strErrMsg) = False Then
                MLogic.AlertMessage(Me, strErrMsg, 0, "ErrBukkenRireki")
                Exit Function
            End If
        End If
        '*********************************************************
        '===========================================================

        ' データの更新を行います
        If logic.SaveJibanData(Me, jibanRec, brRec) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispAction()
        Dim jBn As New Jiban '地盤画面クラス

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' プルダウン/コード入力連携設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        jBn.SetPullCdScriptSrc(TextKoujiTantousyaCd, SelectKoujiTantousya) '工事担当者

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript関連セット
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim onFocusPostBackScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScript As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this)){__doPostBack(this.id,'');}}else{checkNumberAddFig(this);}"
        Dim onFocusScript As String = "removeFig(this);"
        Dim onBlurScript As String = "checkNumberAddFig(this);"
        Dim checkDate As String = "checkDate(this);"
        Dim checkKingaku As String = "checkKingaku(this);"
        Dim checkNumber As String = "checkNumber(this);"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '金額用(登録許可)
        Dim onBlurPostBackScriptKingakuKjChk05 As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this)){document.getElementById('" & HiddenKjChk05.ClientID & "').value='';__doPostBack(this.id,'');}}else{checkNumberAddFig(this);}"
        Dim onBlurPostBackScriptKingakuTjChk05 As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this)){document.getElementById('" & HiddenTjChk05.ClientID & "').value='';__doPostBack(this.id,'');}}else{checkNumberAddFig(this);}"

        '日付項目用
        Dim onFocusPostBackScriptDate As String = "setTempValueForOnBlur(this);"
        Dim onBlurPostBackScriptDate As String = "if(checkTempValueForOnBlur(this)){if(checkDate(this)){__doPostBack(this.id,'');}}else{checkDate(this);}"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 工事会社コード、追加工事会社コードおよびボタン
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        TextKjKoujiKaisyaCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callKjKoujiKaisyaSearch(this);};copyToKjHidden(this);"
        TextKjKoujiKaisyaCd.Attributes("onfocus") = "setTempValueForOnBlur(this);"
        TextTjKoujiKaisyaCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callTjKoujiKaisyaSearch(this);};copyToTjHidden(this);"
        TextTjKoujiKaisyaCd.Attributes("onfocus") = "setTempValueForOnBlur(this);"

        ButtonKjKoujiKaisyaSearch.Attributes("onclick") = "SetChangeMaeValue('" & HiddenKjKoujiKaisyaCdMae.ClientID & "','" & TextKjKoujiKaisyaCd.ClientID & "');"
        ButtonKjKoujiKaisyaSearch.Attributes("onmousedown") = "JSkoujiKaisyaSearchTypeKj=9;"
        ButtonKjKoujiKaisyaSearch.Attributes("onkeydown") = "if(event.keyCode==13||event.keyCode==32)JSkoujiKaisyaSearchTypeKj=9;"
        ButtonTjKoujiKaisyaSearch.Attributes("onclick") = "SetChangeMaeValue('" & HiddenTjKoujiKaisyaCdMae.ClientID & "','" & TextTjKoujiKaisyaCd.ClientID & "');"
        ButtonTjKoujiKaisyaSearch.Attributes("onmousedown") = "JSkoujiKaisyaSearchTypeTj=9;"
        ButtonTjKoujiKaisyaSearch.Attributes("onkeydown") = "if(event.keyCode==13||event.keyCode==32)JSkoujiKaisyaSearchTypeTj=9;"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 金額系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '***********************
        '* 改良工事
        '***********************
        '売上金額/税抜金額
        TextKjUriageZeinukiKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextKjUriageZeinukiKingaku.Attributes("onblur") = onBlurPostBackScriptKingakuKjChk05
        TextKjUriageZeinukiKingaku.Attributes("onkeydown") = disabledOnkeydown
        '仕入金額/税抜金額
        TextKjSiireZeinukiKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextKjSiireZeinukiKingaku.Attributes("onblur") = onBlurPostBackScriptKingakuKjChk05
        TextKjSiireZeinukiKingaku.Attributes("onkeydown") = disabledOnkeydown
        '***********************
        '* 追加工事
        '***********************
        '売上金額/税抜金額
        TextTjUriageZeinukiKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextTjUriageZeinukiKingaku.Attributes("onblur") = onBlurPostBackScriptKingakuTjChk05
        TextTjUriageZeinukiKingaku.Attributes("onkeydown") = disabledOnkeydown
        '仕入金額/税抜金額
        TextTjSiireZeinukiKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextTjSiireZeinukiKingaku.Attributes("onblur") = onBlurPostBackScriptKingakuTjChk05
        TextTjSiireZeinukiKingaku.Attributes("onkeydown") = disabledOnkeydown
        '***********************
        '* 改良工事報告書
        '***********************
        '工務店請求税抜金額
        TextKhKoumutenSeikyuuKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextKhKoumutenSeikyuuKingaku.Attributes("onblur") = onBlurPostBackScript
        TextKhKoumutenSeikyuuKingaku.Attributes("onkeydown") = disabledOnkeydown
        '実請求税抜金額
        TextKhJituseikyuuKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextKhJituseikyuuKingaku.Attributes("onblur") = onBlurPostBackScript
        TextKhJituseikyuuKingaku.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 日付系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '***********************
        '* 改良工事
        '***********************
        '確認日
        TextKjKakuninDate.Attributes("onblur") = checkDate
        TextKjKakuninDate.Attributes("onkeydown") = disabledOnkeydown
        '完了予定日
        TextKjKanryouYoteiDate.Attributes("onblur") = checkDate
        TextKjKanryouYoteiDate.Attributes("onkeydown") = disabledOnkeydown
        '工事日
        TextKjKoujiDate.Attributes("onblur") = checkDate
        TextKjKoujiDate.Attributes("onkeydown") = disabledOnkeydown
        '完工速報着日日
        TextKjKankouSokuhouTyakuDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextKjKankouSokuhouTyakuDate.Attributes("onfocus") = onFocusPostBackScriptDate
        TextKjKankouSokuhouTyakuDate.Attributes("onkeydown") = disabledOnkeydown
        '請求書発行日
        TextKjSeikyuusyoHakkouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextKjSeikyuusyoHakkouDate.Attributes("onfocus") = onFocusPostBackScriptDate
        TextKjSeikyuusyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown

        '***********************
        '* 追加工事
        '***********************
        '完了予定日
        TextTjKanryouYoteiDate.Attributes("onblur") = checkDate
        TextTjKanryouYoteiDate.Attributes("onkeydown") = disabledOnkeydown
        '工事日
        TextTjKoujiDate.Attributes("onblur") = checkDate
        TextTjKoujiDate.Attributes("onkeydown") = disabledOnkeydown
        '完工速報着日日
        TextTjKankouSokuhouTyakuDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextTjKankouSokuhouTyakuDate.Attributes("onfocus") = onFocusPostBackScriptDate
        TextTjKankouSokuhouTyakuDate.Attributes("onkeydown") = disabledOnkeydown
        '請求書発行日
        TextTjSeikyuusyoHakkouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextTjSeikyuusyoHakkouDate.Attributes("onfocus") = onFocusPostBackScriptDate
        TextTjSeikyuusyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown

        '***********************
        '* 改良工事報告書情報
        '***********************
        '受理日
        TextKhJuriDate.Attributes("onblur") = checkDate
        TextKhJuriDate.Attributes("onkeydown") = disabledOnkeydown
        '発送日
        TextKhHassouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextKhHassouDate.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenKhHassouDateMae.ClientID & "','" & TextKhHassouDate.ClientID & "');" & onFocusPostBackScriptDate
        TextKhHassouDate.Attributes("onkeydown") = disabledOnkeydown
        '再発行日
        TextKhSaihakkouDate.Attributes("onblur") = onBlurPostBackScriptDate
        TextKhSaihakkouDate.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenKhSaihakkouDateMae.ClientID & "','" & TextKhSaihakkouDate.ClientID & "');" & onFocusPostBackScriptDate
        TextKhSaihakkouDate.Attributes("onkeydown") = disabledOnkeydown
        '請求書発行日
        TextKhSeikyuusyoHakkouDate.Attributes("onblur") = checkDate
        TextKhSeikyuusyoHakkouDate.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ドロップダウンリスト
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '請求有無
        SelectKhSeikyuuUmu.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenKhSeikyuuUmuMae.ClientID & "','" & SelectKhSeikyuuUmu.ClientID & "')"
        '改良工事/商品コード
        SelectKjSyouhinCd.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenKjSyouhinCdMae.ClientID & "','" & SelectKjSyouhinCd.ClientID & "')"
        '追加工事/商品コード
        SelectTjSyouhinCd.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenTjSyouhinCdMae.ClientID & "','" & SelectTjSyouhinCd.ClientID & "')"
        '改良工事/改良工事種別
        SelectKjKairyouKoujiSyubetu.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenKjKairyouKoujiSyubetuMae.ClientID & "','" & SelectKjKairyouKoujiSyubetu.ClientID & "')"
        '追加工事/改良工事種別
        SelectTjKairyouKoujiSyubetu.Attributes("onfocus") = "SetChangeMaeValue('" & HiddenTjKairyouKoujiSyubetuMae.ClientID & "','" & SelectTjKairyouKoujiSyubetu.ClientID & "')"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 数値系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '工事担当者
        TextKoujiTantousyaCd.Attributes("onblur") += checkNumber
        TextKoujiTantousyaCd.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 機能別テーブルの表示切替
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '改良工事情報
        Me.AncKairyouKouji.HRef = "JavaScript:changeDisplay('" & Me.TBodyKairyouKoujiInfo.ClientID & "');SetDisplayStyle('" & Me.HiddenKairyouKoujiInfoStyle.ClientID & "','" & Me.TBodyKairyouKoujiInfo.ClientID & "');"
        '工事報告書情報
        Me.AncKoujiHoukokusyo.HRef = "JavaScript:changeDisplay('" & Me.TBodyKoujiHoukokusyoInfo.ClientID & "');SetDisplayStyle('" & Me.HiddenKoujiHoukokusyoInfoStyle.ClientID & "','" & Me.TBodyKoujiHoukokusyoInfo.ClientID & "');"


    End Sub

    ''' <summary>
    ''' 業務共通[ユーザーコントロール]のucGyoumuKyoutuu_OyaGamenAction_hensyuで呼ばれる処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ucGyoumuKyoutuu_OyaGamenAction_hensyu(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal blnDisabled As Boolean) Handles ucGyoumuKyoutuu.OyaGamenAction_hensyu
        'コントロールの有効/無効
        CheckHakiDisable(blnDisabled)
    End Sub

    ''' <summary>
    ''' 破棄種別によって、コントロールの有効/無効を切替える
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckHakiDisable(Optional ByVal blnFlg As Boolean = False)
        '地盤画面共通クラス
        Dim jBn As New Jiban

        '有効化、無効化の対象外にするコントロール郡
        Dim noTarget As New Hashtable
        noTarget.Add(divKairyouKouji, True) '改良工事情報
        noTarget.Add(divKairyouKoujiHoukokusyo, True) '改良工事報告書情報
        noTarget.Add(ButtonTouroku1.ID, True) '登録ボタン1
        noTarget.Add(ButtonTouroku2.ID, True) '登録ボタン2

        If blnFlg Then
            '全てのコントロールを無効化()
            jBn.ChangeDesabledAll(divKairyouKouji, True, noTarget)
            jBn.ChangeDesabledAll(divKairyouKoujiHoukokusyo, True, noTarget)
        Else
            '全てのコントロールを有効化()
            jBn.ChangeDesabledAll(divKairyouKouji, False, noTarget)
            jBn.ChangeDesabledAll(divKairyouKoujiHoukokusyo, False, noTarget)
        End If

    End Sub

#Region "画面制御"

    ''' <summary>
    ''' コントロールの初期起動時の画面制御/改良工事
    ''' </summary>
    ''' <remarks>コントロールの初期起動時の画面制御を仕様書の優先順に記載する</remarks>
    Public Sub SetEnableControlInitKj()

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable
        jSM.Hash2Ctrl(TdKjKoujiSiyouKakunin, EarthConst.MODE_VIEW, ht) '工事仕様確認
        jSM.Hash2Ctrl(TdKjKakuninDate, EarthConst.MODE_VIEW, ht) '確認日
        jSM.Hash2Ctrl(TdKjKoujiKaisyaCd, EarthConst.MODE_VIEW, ht) '工事会社
        jSM.Hash2Ctrl(TdKjKoujiKaisyaSeikyuu, EarthConst.MODE_VIEW, ht) '工事会社請求

        ButtonKjKoujiKaisyaSearch.Style("display") = "none"

        '売上処理済(邸別請求テーブル.売上計上FLG＝1)
        If SpanKjUriageSyoriZumi.InnerHtml = EarthConst.URIAGE_ZUMI Then '売上処理済の場合
        Else
            cl.chgDispSyouhinPull(SelectKjKoujiSiyouKakunin, SpanKjKoujiSiyouKakunin) '工事仕様確認
            cl.chgDispSyouhinText(TextKjKoujiKaisyaCd) '工事会社
            ButtonKjKoujiKaisyaSearch.Style("display") = "inline"
            cl.chgDispCheckBox(CheckBoxKjKoujiKaisyaSeikyuu, SpanKjKoujiKaisyaSeikyuu) '工事会社請求

            If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '工事仕様確認＝有の場合
                cl.chgDispSyouhinText(TextKjKakuninDate) '確認日

            End If

        End If

    End Sub

    ''' <summary>
    ''' コントロールの画面制御/改良工事
    ''' </summary>
    ''' <remarks>コントロールの画面制御を仕様書の優先順に記載する</remarks>
    Public Sub SetEnableControlKj()

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable
        jSM.Hash2Ctrl(TdKjKakuninDate, EarthConst.MODE_VIEW, ht) '確認日
        jSM.Hash2Ctrl(TdKjSyouhinCd, EarthConst.MODE_VIEW, ht) '商品コード
        jSM.Hash2Ctrl(TdKjSeikyuuUmu, EarthConst.MODE_VIEW, ht) '請求有無
        jSM.Hash2Ctrl(TdKjUriageKingaku, EarthConst.MODE_VIEW, ht) '売上金額/税抜金額
        jSM.Hash2Ctrl(TdKjSiireKingaku, EarthConst.MODE_VIEW, ht) '仕入金額/税抜金額
        jSM.Hash2Ctrl(TdKjSeikyuusyoHakkouDate, EarthConst.MODE_VIEW, ht) '請求書発行日

        '●優先順1
        '発注書確定(邸別請求テーブル.発注書確定FLG＝1)＝＞なし

        '●優先順2
        '売上処理済(邸別請求テーブル.売上計上FLG＝1)
        If SpanKjUriageSyoriZumi.InnerHtml = EarthConst.URIAGE_ZUMI Then
        Else
            '●優先順3
            '商品コード＝空白
            If SelectKjSyouhinCd.SelectedValue = "" Then
                cl.chgDispSyouhinPull(SelectKjSyouhinCd, SpanKjSyouhinCd) '商品コード

            Else
                cl.chgDispSyouhinPull(SelectKjSyouhinCd, SpanKjSyouhinCd) '商品コード

                '●優先順4

                If SelectKjSeikyuuUmu.SelectedValue = "0" Or SelectKjSeikyuuUmu.SelectedValue = "" Then '請求有無 無or空白
                    cl.chgDispSyouhinPull(SelectKjSeikyuuUmu, SpanKjSeikyuuUmu) '請求有無
                    cl.chgDispSyouhinText(TextKjSiireZeinukiKingaku) '仕入金額/税抜金額

                    '●優先順5
                ElseIf SelectKjSeikyuuUmu.SelectedValue = "1" Then '有
                    cl.chgDispSyouhinPull(SelectKjSeikyuuUmu, SpanKjSeikyuuUmu) '請求有無
                    cl.chgDispSyouhinText(TextKjUriageZeinukiKingaku) '売上金額/税抜金額
                    cl.chgDispSyouhinText(TextKjSiireZeinukiKingaku) '仕入金額/税抜金額
                    cl.chgDispSyouhinText(TextKjSeikyuusyoHakkouDate) '請求書発行日

                End If
            End If

        End If
        '●優先順6(画面.発注書確定＝確定)＝＞なし
        '●優先順7(優先順6以外)＝＞なし
        '発注書確定≠確定

        '**********************
        '* 工事仕様確認変更時
        '**********************
        '工事仕様確認
        If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '有
            cl.chgDispSyouhinText(TextKjKakuninDate) '確認日

        End If

    End Sub

    ''' <summary>
    ''' コントロールの初期起動時の画面制御/追加工事
    ''' </summary>
    ''' <remarks>コントロールの初期起動時の画面制御を仕様書の優先順に記載する</remarks>
    Public Sub SetEnableControlInitTj()
        '地盤画面共通クラス
        Dim jBn As New Jiban
        '有効化、無効化の対象外にするコントロール郡
        Dim noTarget As New Hashtable
        noTarget.Add(divKairyouKouji, True) '改良工事情報

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable

        ButtonTjKoujiKaisyaSearch.Style("display") = "inline"

        '売上処理済(邸別請求テーブル.売上計上FLG＝1)
        If SpanKjUriageSyoriZumi.InnerHtml <> EarthConst.URIAGE_ZUMI Then '改良工事/未売上処理

            '商品コード＝"B2008"、且つ売上年月日＝入力
            If SelectKjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO And TextKjUriageNengappi.Text <> "" Then
                If SpanTjUriageSyorizumi.InnerHtml = EarthConst.URIAGE_ZUMI Then
                    jSM.Hash2Ctrl(TdTjKoujiKaisyaCd, EarthConst.MODE_VIEW, ht) '工事会社
                    ButtonTjKoujiKaisyaSearch.Style("display") = "none"
                End If
            Else
                jSM.Hash2Ctrl(TdTjKoujiKaisyaCd, EarthConst.MODE_VIEW, ht) '工事会社
                ButtonTjKoujiKaisyaSearch.Style("display") = "none"
                jSM.Hash2Ctrl(TdTjKairyouKoujiSyubetu, EarthConst.MODE_VIEW, ht) '改良工事種別
                jSM.Hash2Ctrl(TdTjKanryouYoteiDate, EarthConst.MODE_VIEW, ht) '完了予定日
                jSM.Hash2Ctrl(TdTjKoujiDate, EarthConst.MODE_VIEW, ht) '工事日
                jSM.Hash2Ctrl(TdTjKankouSokuhouTyakuDate, EarthConst.MODE_VIEW, ht) '完工速報着日
            End If

        Else
            If SpanTjUriageSyorizumi.InnerHtml = EarthConst.URIAGE_ZUMI Then
                jSM.Hash2Ctrl(TdTjKoujiKaisyaCd, EarthConst.MODE_VIEW, ht) '工事会社
                ButtonTjKoujiKaisyaSearch.Style("display") = "none"
                jSM.Hash2Ctrl(TdTjKoujiKaisyaSeikyuu, EarthConst.MODE_VIEW, ht) '工事会社請求
            End If

        End If
        '商品コード＝"B2008"、且つ売上年月日＝入力
        If (SelectKjSyouhinCd.SelectedValue = EarthConst.SH_CD_JIO And TextKjUriageNengappi.Text <> "") = False Then
            jSM.Hash2Ctrl(TdTjKoujiKaisyaSeikyuu, EarthConst.MODE_VIEW, ht) '工事会社請求
        End If


    End Sub

    ''' <summary>
    ''' コントロールの画面制御/追加工事
    ''' </summary>
    ''' <remarks>コントロールの画面制御を仕様書の優先順に記載する</remarks>
    Public Sub SetEnableControlTj()

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable
        jSM.Hash2Ctrl(TdTjSyouhinCd, EarthConst.MODE_VIEW, ht) '商品コード
        jSM.Hash2Ctrl(TdTjSeikyuuUmu, EarthConst.MODE_VIEW, ht) '請求有無
        jSM.Hash2Ctrl(TdTjUriageKingaku, EarthConst.MODE_VIEW, ht) '売上金額/税抜金額
        jSM.Hash2Ctrl(TdTjSiireKingaku, EarthConst.MODE_VIEW, ht) '仕入金額/税抜金額
        jSM.Hash2Ctrl(TdTjSeikyuusyoHakkouDate, EarthConst.MODE_VIEW, ht) '請求書発行日

        '●優先順1
        '発注書確定(邸別請求テーブル.発注書確定FLG＝1)＝＞なし

        '●優先順2
        '売上処理済(邸別請求テーブル.売上計上FLG＝1)＝＞なし
        If SpanTjUriageSyorizumi.InnerHtml = EarthConst.URIAGE_ZUMI Then
        Else

            '●優先順3
            '改良工事.売上処理済、あるいは未売上処理でかつJIO式商品で売上年月日が入力ではない場合
            If SpanKjUriageSyoriZumi.InnerHtml = EarthConst.URIAGE_ZUMI Or _
                (SpanKjUriageSyoriZumi.InnerHtml <> EarthConst.URIAGE_ZUMI And _
                (HiddenKjSyouhinCdOld.Value = EarthConst.SH_CD_JIO And HiddenKjUriageNengappiOld.Value <> "")) Then

                '●優先順4
                '商品コード＝空白
                If SelectTjSyouhinCd.SelectedValue = "" Then
                    cl.chgDispSyouhinPull(SelectTjSyouhinCd, SpanTjSyouhinCd) '商品コード

                Else

                    cl.chgDispSyouhinPull(SelectTjSyouhinCd, SpanTjSyouhinCd) '商品コード

                    '●優先順4

                    If SelectTjSeikyuuUmu.SelectedValue = "0" Or SelectTjSeikyuuUmu.SelectedValue = "" Then '請求有無 無or空白
                        cl.chgDispSyouhinPull(SelectTjSeikyuuUmu, SpanTjSeikyuuUmu) '請求有無
                        cl.chgDispSyouhinText(TextTjSiireZeinukiKingaku) '仕入金額/税抜金額

                        '●優先順5
                    ElseIf SelectTjSeikyuuUmu.SelectedValue = "1" Then '有
                        cl.chgDispSyouhinPull(SelectTjSeikyuuUmu, SpanTjSeikyuuUmu) '請求有無
                        cl.chgDispSyouhinText(TextTjUriageZeinukiKingaku) '売上金額/税抜金額
                        cl.chgDispSyouhinText(TextTjSiireZeinukiKingaku) '仕入金額/税抜金額
                        cl.chgDispSyouhinText(TextTjSeikyuusyoHakkouDate) '請求書発行日

                    End If
                End If

                '●優先順6(画面.発注書確定＝確定)＝＞なし
                '●優先順7(優先順6以外)＝＞なし
                '発注書確定≠確定

            End If

        End If

    End Sub

    ''' <summary>
    ''' コントロールの画面制御/工事報告書再発行
    ''' </summary>
    ''' <remarks>コントロールの画面制御を仕様書の優先順に記載する</remarks>
    Public Sub SetEnableControlKh()

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable
        htNotTarget.Add(TextKhSeikyuusaki.ID, TextKhSeikyuusaki) '請求先
        htNotTarget.Add(TextKhUriageNengappi.ID, TextKhUriageNengappi) '売上年月日
        htNotTarget.Add(TextKhHattyuusyoKakutei.ID, TextKhHattyuusyoKakutei) '発注書確定
        htNotTarget.Add(TextKhHattyuusyoKingaku.ID, TextKhHattyuusyoKingaku) '発注書金額
        htNotTarget.Add(TextKhHattyuusyoKakuninDate.ID, TextKhHattyuusyoKakuninDate) '発注書確認日
        jSM.Hash2Ctrl(TdKhHassouDate, EarthConst.MODE_VIEW, ht) '発送日
        jSM.Hash2Ctrl(TdKhSaihakkouDate, EarthConst.MODE_VIEW, ht) '再発行日
        jSM.Hash2Ctrl(TrKhSyouhin, EarthConst.MODE_VIEW, ht, htNotTarget) '商品テーブル
        jSM.Hash2Ctrl(TdKhSaihakkouRiyuu, EarthConst.MODE_VIEW, ht) '再発行理由

        '●優先順1
        '発注書確定(邸別請求テーブル.発注書確定FLG＝1)＝＞なし

        '●優先順2
        '売上処理済(邸別請求テーブル.売上計上FLG＝1)
        If SpanKhUriageSyorizumi.InnerHtml = EarthConst.URIAGE_ZUMI Then '変更箇所なしのため、画面から取得
            cl.chgDispSyouhinText(TextKhSaihakkouRiyuu) '再発行理由

            '●優先順3
        ElseIf TextKhHassouDate.Text.Length = 0 Then '調査報告書発送日＝未入力
            cl.chgDispSyouhinText(TextKhHassouDate) '発送日
        Else
            cl.chgDispSyouhinText(TextKhHassouDate) '発送日

            '●優先順4
            If TextKhSaihakkouDate.Text.Length = 0 Then '再発行日＝未入力
                cl.chgDispSyouhinText(TextKhSaihakkouDate) '再発行日

                '●優先順5,6
            ElseIf SelectKhSeikyuuUmu.SelectedValue = "0" Or SelectKhSeikyuuUmu.SelectedValue = "" Then '請求 無or空白
                cl.chgDispSyouhinText(TextKhSaihakkouDate) '再発行日
                cl.chgDispSyouhinPull(SelectKhSeikyuuUmu, SpanKhSeikyuuUmu) '請求有無
                cl.chgDispSyouhinText(TextKhSaihakkouRiyuu) '再発行理由

                '●優先順7
            Else
                cl.chgDispSyouhinText(TextKhSaihakkouDate) '再発行日
                cl.chgDispSyouhinPull(SelectKhSeikyuuUmu, SpanKhSeikyuuUmu) '請求有無
                cl.chgDispSyouhinText(TextKhSaihakkouRiyuu) '再発行理由

                Dim kameitenlogic As New KameitenSearchLogic
                Dim blnTorikesi As Boolean = False
                Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                '(*6)加盟店が取消の場合、他請求3系列以外の扱いとする
                If record.Torikesi <> 0 Then
                    record.KeiretuCd = ""
                End If

                If TextKhSeikyuusaki.Text = EarthConst.SEIKYU_TASETU And cl.getKeiretuFlg(record.KeiretuCd) = False Then

                    cl.chgDispSyouhinText(TextKhJituseikyuuKingaku) '実請求税抜金額
                    cl.chgDispSyouhinText(TextKhSeikyuusyoHakkouDate) '請求書発行日

                    '●優先順8
                Else
                    cl.chgDispSyouhinText(TextKhJituseikyuuKingaku) '実請求税抜金額
                    cl.chgDispSyouhinText(TextKhSeikyuusyoHakkouDate) '請求書発行日
                    cl.chgDispSyouhinText(TextKhKoumutenSeikyuuKingaku) '工務店請求税抜金額
                End If

            End If

        End If


        '●優先順9(画面.発注書確定＝確定)＝＞なし

        '●優先順10(優先順9以外)＝＞なし
        ''発注書確定≠確定

        '受理変更
        If SelectKhJuri.SelectedValue = "2" Or SelectKhJuri.SelectedValue = "3" Then
            jSM.Hash2Ctrl(TdKhHassouDate, EarthConst.MODE_VIEW, ht) '発送日
            jSM.Hash2Ctrl(TdKhSaihakkouDate, EarthConst.MODE_VIEW, ht) '再発行日
        End If

    End Sub

#End Region

#Region "工事会社関連"

    ''' <summary>
    ''' 改良工事/工事会社変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKjKoujiKaisyaCd_ChangeSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        Dim tmpScript As String = ""

        'コードの入力
        If TextKjKoujiKaisyaCd.Text <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetKoujikaishaSearchResult(TextKjKoujiKaisyaCd.Text, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi, _
                                                                            ucGyoumuKyoutuu.AccKameitenCd.Value)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            TextKjKoujiKaisyaCd.Text = recData.TysKaisyaCd & recData.JigyousyoCd
            TextKjKoujiKaisyaMei.Text = recData.TysKaisyaMei
            If Me.TextKjKoujiKaisyaMei.Text = String.Empty Then '未設定時、半角スペースを表示
                Me.TextKjKoujiKaisyaMei.Text = " "
            End If
            Me.HiddenKjKoujiKaisyaCdMae.Value = Me.TextKjKoujiKaisyaCd.Text

            ' 工事会社NG設定
            If recData.KahiKbn = 9 Then
                TextKjKoujiKaisyaCd.Style("color") = "red"
                TextKjKoujiKaisyaMei.Style("color") = "red"

                tmpScript = "alert('" & Messages.MSG103W & "');" & vbCrLf
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextKjKoujiKaisyaCd_ChangeSub", tmpScript, True)

            Else
                TextKjKoujiKaisyaCd.Style("color") = "blue"
                TextKjKoujiKaisyaMei.Style("color") = "blue"
            End If

            '工事価格セット処理
            If SetSyouhinInfoFromKojM(EnumKoujiType.KairyouKouji, EarthEnum.emKojKkkActionType.KojKaisyaCd) = True Then
                '金額の再計算
                SetKingakuUriage(EnumKoujiType.KairyouKouji)
            End If

            'セットフォーカス
            setFocusAJ(ButtonKjKoujiKaisyaSearch)

        Else

            TextKjKoujiKaisyaCd.Style("color") = "black"
            TextKjKoujiKaisyaMei.Style("color") = "black"

            '工事会社コードが未確定なのでクリア
            TextKjKoujiKaisyaMei.Text = ""

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpFocusScript = "objEBI('" & ButtonKjKoujiKaisyaSearch.ClientID & "').focus();"
            tmpScript = "callSearch('" & TextKjKoujiKaisyaCd.ClientID & EarthConst.SEP_STRING & _
                                             ucGyoumuKyoutuu.AccKameitenCd.ClientID & "','" & _
                                             UrlConst.SEARCH_KOUJIKAISYA & "','" & _
                                             TextKjKoujiKaisyaCd.ClientID & EarthConst.SEP_STRING & _
                                             TextKjKoujiKaisyaMei.ClientID & _
                                             "','" & ButtonKjKoujiKaisyaSearch.ClientID & "');"
            If callWindow Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & tmpScript, True)
            ElseIf HiddenKoujiKaisyaSearchTypeKj.Value = "1" Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & "if(JSkoujiKaisyaSearchTypeKj==9)" & tmpScript, True)
            End If

        End If

        tmpScript = "JSkoujiKaisyaSearchTypeKj=0;"
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "resetSearchType", tmpScript, True)

    End Sub

    ''' <summary>
    ''' 改良工事/工事会社検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKjKoujiKaisyaSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        '1.画面の設定
        If HiddenKoujiKaisyaSearchTypeKj.Value <> "1" Then
            TextKjKoujiKaisyaCd_ChangeSub(sender, e)
        Else
            'コードonchangeで呼ばれた場合
            TextKjKoujiKaisyaCd_ChangeSub(sender, e, False)
            HiddenKoujiKaisyaSearchTypeKj.Value = String.Empty
        End If

        '請求有無による請求書発行日自動設定
        If Me.SelectKjSeikyuuUmu.SelectedValue = "0" Or Me.SelectKjSeikyuuUmu.SelectedValue = "" Then
            Me.TextKjSeikyuusyoHakkouDate.Text = ""
        End If

        '工事会社NG設定
        ucGyoumuKyoutuu.SetKoujiKaisyaNG(TextKjKoujiKaisyaCd.Text, TextKjKoujiKaisyaCd.Text)

        '工事会社請求変更時処理(共通)
        Me.ChgKjKaisyaSeikyuuUmu()

        '画面制御
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' 追加工事/工事会社変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextTjKoujiKaisyaCd_ChangeSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        Dim tmpScript As String = ""

        'コードの入力
        If TextTjKoujiKaisyaCd.Text <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetKoujikaishaSearchResult(TextTjKoujiKaisyaCd.Text, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi, _
                                                                            ucGyoumuKyoutuu.AccKameitenCd.Value)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            TextTjKoujiKaisyaCd.Text = recData.TysKaisyaCd & recData.JigyousyoCd
            TextTjKoujiKaisyaMei.Text = recData.TysKaisyaMei
            If Me.TextTjKoujiKaisyaMei.Text = String.Empty Then '未設定時、半角スペースを表示
                Me.TextTjKoujiKaisyaMei.Text = " "
            End If
            Me.HiddenTjKoujiKaisyaCdMae.Value = Me.TextTjKoujiKaisyaCd.Text

            ' 工事会社NG設定
            If recData.KahiKbn = 9 Then
                TextTjKoujiKaisyaCd.Style("color") = "red"
                TextTjKoujiKaisyaMei.Style("color") = "red"

                tmpScript = "alert('" & Messages.MSG103W & "');" & vbCrLf
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextTjKoujiKaisyaCd_ChangeSub", tmpScript, True)
            Else
                TextTjKoujiKaisyaCd.Style("color") = "blue"
                TextTjKoujiKaisyaMei.Style("color") = "blue"
            End If

            '工事価格セット処理
            If SetSyouhinInfoFromKojM(EnumKoujiType.TuikaKouji, EarthEnum.emKojKkkActionType.KojKaisyaCd) = True Then
                '金額の再計算(工事価格から取得出来た場合のみ再計算)
                SetKingakuUriage(EnumKoujiType.TuikaKouji)
            End If

            'セットフォーカス
            setFocusAJ(ButtonTjKoujiKaisyaSearch)
        Else

            TextTjKoujiKaisyaCd.Style("color") = "black"
            TextTjKoujiKaisyaMei.Style("color") = "black"

            '追加工事会社コードが未確定なのでクリア
            TextTjKoujiKaisyaMei.Text = ""

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpFocusScript = "objEBI('" & ButtonTjKoujiKaisyaSearch.ClientID & "').focus();"
            tmpScript = "callSearch('" & TextTjKoujiKaisyaCd.ClientID & EarthConst.SEP_STRING & _
                                             ucGyoumuKyoutuu.AccKameitenCd.ClientID & "','" & _
                                             UrlConst.SEARCH_KOUJIKAISYA & "','" & _
                                             TextTjKoujiKaisyaCd.ClientID & EarthConst.SEP_STRING & _
                                             TextTjKoujiKaisyaMei.ClientID & _
                                             "','" & ButtonTjKoujiKaisyaSearch.ClientID & "');"
            If callWindow Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & tmpScript, True)
            ElseIf HiddenKoujiKaisyaSearchTypeTj.Value = "1" Then
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpFocusScript & "if(JSkoujiKaisyaSearchTypeTj==9)" & tmpScript, True)
            End If

        End If

        tmpScript = "JSkoujiKaisyaSearchTypeTj=0;"
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "resetSearchType", tmpScript, True)

    End Sub

    ''' <summary>
    ''' 追加工事/工事会社検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTjKoujiKaisyaSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If HiddenKoujiKaisyaSearchTypeTj.Value <> "1" Then
            TextTjKoujiKaisyaCd_ChangeSub(sender, e)
        Else
            'コードonchangeで呼ばれた場合
            TextTjKoujiKaisyaCd_ChangeSub(sender, e, False)
            HiddenKoujiKaisyaSearchTypeTj.Value = String.Empty
        End If

        '請求有無による請求書発行日自動設定
        If Me.SelectTjSeikyuuUmu.SelectedValue = "0" Or Me.SelectTjSeikyuuUmu.SelectedValue = "" Then
            Me.TextTjSeikyuusyoHakkouDate.Text = ""
        End If

        '工事会社NG設定
        ucGyoumuKyoutuu.SetKoujiKaisyaNG(TextTjKoujiKaisyaCd.Text, TextTjKoujiKaisyaCd.Text)

        '<共通情報>区分=Eあるいは、<改良工事>工事会社請求=チェックの場合
        If ucGyoumuKyoutuu.Kubun = "E" Or Me.CheckBoxTjKoujiKaisyaSeikyuu.Checked Then
            '工事会社請求有無変更時処理[共通]
            Me.ChgTjKoujiKaisyaSeikyuuUmu()

            '画面制御
            SetEnableControlTj()
        End If

    End Sub

#End Region

#Region "自動設定/請求書発行日"

    ''' <summary>
    ''' [改良工事]請求書発行日の設定
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <remarks></remarks>
    Private Sub SetKjAutoSeikyuusyoHakkouDate(ByVal strSyouhinCd As String)
        Dim strDtTmp As String

        '請求締め日のセット
        Me.ucSeikyuuSiireLinkKai.SetSeikyuuSimeDate(strSyouhinCd, CheckBoxKjKoujiKaisyaSeikyuu.Checked, TextKjKoujiKaisyaCd.Text)

        '請求書発行日の自動設定
        strDtTmp = Me.ucSeikyuuSiireLinkKai.GetSeikyuusyoHakkouDate()

        TextKjSeikyuusyoHakkouDate.Text = strDtTmp

    End Sub

    ''' <summary>
    ''' [追加工事]請求書発行日の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTjAutoSeikyuusyoHakkouDate(ByVal strSyouhinCd As String)
        Dim strDtTmp As String

        '請求締め日のセット
        Me.ucSeikyuuSiireLinkTui.SetSeikyuuSimeDate(strSyouhinCd, CheckBoxTjKoujiKaisyaSeikyuu.Checked, TextTjKoujiKaisyaCd.Text)

        '請求書発行日の自動設定
        strDtTmp = Me.ucSeikyuuSiireLinkTui.GetSeikyuusyoHakkouDate()

        TextTjSeikyuusyoHakkouDate.Text = strDtTmp

    End Sub

    ''' <summary>
    ''' 請求書発行日を取得
    ''' </summary>
    ''' <param name="ctrlLink">請求先/仕入先リンクユーザーコントロール</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <returns>請求書発行日</returns>
    ''' <remarks></remarks>
    Private Function GetSeikyuusyoHakkouDate(ByVal ctrlLink As SeikyuuSiireLinkCtrl, ByVal strSyouhinCd As String) As String
        Dim strDtTmp As String

        '請求締日のセット
        ctrlLink.SetSeikyuuSimeDate(strSyouhinCd)

        '請求書発行日の自動設定
        strDtTmp = ctrlLink.GetSeikyuusyoHakkouDate()

        Return strDtTmp

    End Function

#End Region

#Region "改良工事報告書変更時処理"

    ''' <summary>
    ''' (25) [改良工事報告書情報]受理変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKhJuri_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strJuri As String = SelectKhJuri.SelectedValue '受理
        Dim strJuriDate As String = TextKhJuriDate.Text '受理日
        Dim strHassouDate As String = TextKhHassouDate.Text '発送日
        Dim strHizukeHassouDate As String = "" '日付マスタ.報告書発送日

        setFocusAJ(SelectKhJuri)

        If strJuri = "1" Then '受理＝1（有り）の場合

            If strJuriDate = String.Empty Then '受理日＝未入力の場合
                ' システム日付をセット
                TextKhJuriDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)

                If strHassouDate = String.Empty Then '発送日＝未入力の場合

                    Dim houkokusyoLogic As New HoukokusyoLogic
                    Dim strRet As String = String.Empty

                    '報告書発送日の自動設定
                    '※受理日＞上記の日付マスタ.報告書発送日の場合は、日付マスタ.報告書発送日＋1ヶ月を編集する
                    strRet = houkokusyoLogic.GetHoukokusyoHassoudate(ucGyoumuKyoutuu.Kubun _
                                                            , TextKhJuriDate.Text)
                    If strRet <> String.Empty Then
                        TextKhHassouDate.Text = strRet
                    End If

                End If

            End If

        ElseIf strJuri = "2" Or strJuri = "3" Then '調査報告書受理＝2,3（保留、又は、送付不要）の場合

            If strJuriDate = String.Empty Then '受理日＝未入力の場合
                ' システム日付をセット
                TextKhJuriDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
            End If

        ElseIf strJuri = "4" Then '調査報告書受理＝4（再発送）の場合
            If strJuriDate = String.Empty Then '受理日＝未入力の場合
                ' システム日付をセット
                TextKhJuriDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
            End If

        End If

        '画面制御
        SetEnableControlKh()
    End Sub

    ''' <summary>
    ''' (27) [改良工事報告書情報]発送日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKhHassouDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""

        'セットフォーカス
        setFocusAJ(TextKhHassouDate)

        '発送日
        If TextKhHassouDate.Text.Length <> 0 Then '入力あり
            '自動設定はなし、画面制御のみ＝＞特に処理なし

            'セットフォーカス
            setFocusAJ(TextKhSaihakkouDate)

        ElseIf TextKhHassouDate.Text.Length = 0 Then '入力なし

            '(*4)発注書金額≠0,かつ,≠Nullの場合、エラーメッセージ（037）を表示し、自動設定（クリア）を行わずに、未入力に変更した項目(発送日/再発行日/請求有無)は元の値に戻す。
            If TextKhHattyuusyoKingaku.Text <> "0" And TextKhHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                TextKhHassouDate.Text = HiddenKhHassouDateMae.Value '変更前の値に戻す ※注 Javascriptでの設定ではChangeイベントが発生しない
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextKhHassouDate_TextChanged", tmpScript, True)
                Exit Sub
            End If

            'クリア
            TextKhSaihakkouDate.Text = "" '再発行日
            TextKhSaihakkouRiyuu.Text = "" '再発行理由

            Me.ClearControlKh()

        End If

        '画面制御
        SetEnableControlKh()
    End Sub

    ''' <summary>
    ''' (28) [改良工事報告書情報]再発行日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKhSaihakkouDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""
        Dim strSyouhinCd As String = TextKhSyouhinCd.Text

        'セットフォーカス
        setFocusAJ(TextKhSaihakkouDate)

        '再発行日
        If TextKhSaihakkouDate.Text.Length <> 0 Then '入力あり

            'セットフォーカス
            setFocusAJ(SelectKhSeikyuuUmu)

            '請求有無
            If SelectKhSeikyuuUmu.SelectedValue = "1" Then '有
                '商品コード
                If TextKhSyouhinCd.Text.Length <> 0 Then '設定済
                    '請求書発行日
                    If TextKhSeikyuusyoHakkouDate.Text.Length = 0 Then '未入力
                        '請求書発行日の自動設定
                        Dim strDtTmp As String = GetSeikyuusyoHakkouDate(Me.ucSeikyuuSiireLink, strSyouhinCd)
                        TextKhSeikyuusyoHakkouDate.Text = strDtTmp
                    End If

                    '売上年月日
                    If TextKhUriageNengappi.Text.Length = 0 Then '未入力
                        '売上年月日の自動設定
                        TextKhUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If

                End If

            ElseIf SelectKhSeikyuuUmu.SelectedValue = "0" Then '無
                '商品コード
                If TextKhSyouhinCd.Text.Length <> 0 Then '設定済
                    '売上年月日
                    If TextKhUriageNengappi.Text.Length = 0 Then '未入力
                        '売上年月日の自動設定
                        TextKhUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If
                End If
            End If

        Else '入力なし

            '(*4)発注書金額≠0,かつ,≠Nullの場合、エラーメッセージ（037）を表示し、自動設定（クリア）を行わずに、未入力に変更した項目(発送日/再発行日/請求有無)は元の値に戻す。
            If TextKhHattyuusyoKingaku.Text <> "0" And TextKhHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                TextKhSaihakkouDate.Text = HiddenKhSaihakkouDateMae.Value '変更前の値に戻す ※注 Javascriptでの設定ではChangeイベントが発生しない
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextKhSaihakkouDate_TextChanged", tmpScript, True)
                Exit Sub
            End If

            '●クリア
            TextKhSaihakkouRiyuu.Text = "" '再発行理由

            Me.ClearControlKh()

        End If

        '画面制御
        SetEnableControlKh()

    End Sub

    ''' <summary>
    ''' (29) [改良工事報告書情報]請求有無変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKhSeikyuuUmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""
        Dim blnTorikesi As Boolean = False
        '直接請求、他請求の情報を取得
        Dim syouhinRec As Syouhin23Record

        'セットフォーカス
        setFocusAJ(SelectKhSeikyuuUmu)

        '仕入額は常に0円
        Me.HiddenKhSiireGaku.Value = "0"
        Me.HiddenKhSiireSyouhiZei.Value = "0"

        '請求有無
        If SelectKhSeikyuuUmu.SelectedValue = "" Then '空白

            '(*4)発注書金額≠0,かつ,≠Nullの場合、エラーメッセージ（037）を表示し、自動設定（クリア）を行わずに、未入力に変更した項目(発送日/再発行日/請求有無)は元の値に戻す。
            If TextKhHattyuusyoKingaku.Text <> "0" And TextKhHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                SelectKhSeikyuuUmu.SelectedValue = HiddenKhSeikyuuUmuMae.Value '変更前の値に戻す ※注 Javascriptでの設定ではChangeイベントが発生しない
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectKhSeikyuuUmu_SelectedIndexChanged", tmpScript, True)
                Exit Sub
            End If

            '●クリア
            Me.ClearControlKh()

        ElseIf SelectKhSeikyuuUmu.SelectedValue = "0" Then '無
            '金額の0クリア
            Clear0SyouhinTableKh()

            '商品コード/商品名の自動設定▲
            Me.SetSyouhinInfoKh()

            '金額の再計算
            SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo)

        ElseIf SelectKhSeikyuuUmu.SelectedValue = "1" Then '有

            '商品コード/商品名の自動設定▲
            Me.SetSyouhinInfoKh()

            '商品コード
            If TextKhSyouhinCd.Text = String.Empty Then '設定なし
                '請求有無
                SelectKhSeikyuuUmu.SelectedValue = "" '空白

                SetEnableControlKh() '画面制御
                Exit Sub
            Else
                '請求書発行日
                If TextKhSeikyuusyoHakkouDate.Text.Length = 0 Then
                    '請求書発行日の自動設定
                    Dim strDttmp As String = GetSeikyuusyoHakkouDate(Me.ucSeikyuuSiireLink, Me.TextKhSyouhinCd.Text)
                    TextKhSeikyuusyoHakkouDate.Text = strDttmp
                End If

                TextKhUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9) '売上年月日

                '発注書確定
                If TextKhHattyuusyoKakutei.Text.Length = 0 Then '(*5)発注書確定が空白の場合は、「0：未確定」を設定する
                    TextKhHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI
                End If
            End If

            '*************************
            '* 以下、自動設定処理
            '*************************
            If TextKhSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '直接請求

                '(*6)加盟店が取消の場合、他請求3系列以外の扱いとする
                Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                If record Is Nothing Then 'データ取得できなかった場合
                    SelectKhSeikyuuUmu.SelectedValue = "" '取得NG(請求有無のクリア)

                    ClearBlnkSyouhinTable() '空白クリア
                End If

                If record.Torikesi <> 0 Then '取消フラグがたっている場合
                    '**************************************************
                    ' 他請求（系列以外）
                    '**************************************************
                    ' 工務店請求額は０
                    TextKhKoumutenSeikyuuKingaku.Text = "0"

                    '実請求税抜金額の自動設定
                    syouhinRec = JibanLogic.GetSyouhinInfo(TextKhSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo, ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        HiddenKhZeiritu.Value = syouhinRec.Zeiritu '税率
                        HiddenKhZeiKbn.Value = syouhinRec.ZeiKbn '税区分

                        '実請求税抜金額＝商品マスタ.標準価格
                        TextKhJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                    End If

                Else
                    '**************************************************
                    ' 直接請求
                    '**************************************************
                    '工務店(A)
                    '実請求(A)
                    syouhinRec = JibanLogic.GetSyouhinInfo(TextKhSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo, ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        '工務店請求税抜金額＝商品マスタ.標準価格
                        TextKhKoumutenSeikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)
                        '実請求税抜金額＝画面.工務店請求税抜金額
                        TextKhJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                        HiddenKhZeiritu.Value = syouhinRec.Zeiritu '税率
                        HiddenKhZeiKbn.Value = syouhinRec.ZeiKbn '税区分
                    End If

                End If

            ElseIf TextKhSeikyuusaki.Text = EarthConst.SEIKYU_TASETU Then '他請求
                Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                If record Is Nothing Then 'データ取得できなかった場合
                    SelectKhSeikyuuUmu.SelectedValue = "" '取得NG(請求有無のクリア)

                    ClearBlnkSyouhinTable() '空白クリア
                End If

                '(*6)加盟店が取消の場合、他請求3系列以外の扱いとする
                If record.Torikesi <> 0 Then
                    record.KeiretuCd = ""
                End If

                If cl.getKeiretuFlg(record.KeiretuCd) Then '3系列
                    '工務店(A)
                    '実請求(B)
                    syouhinRec = JibanLogic.GetSyouhinInfo(TextKhSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo, ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        HiddenKhZeiritu.Value = syouhinRec.Zeiritu '税率
                        HiddenKhZeiKbn.Value = syouhinRec.ZeiKbn '税区分

                        '工務店請求税抜金額＝商品マスタ.標準価格
                        TextKhKoumutenSeikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                        '※画面.工務店請求税抜金額＝0 の場合は 0 固定
                        If TextKhKoumutenSeikyuuKingaku.Text = "0" Then
                            TextKhJituseikyuuKingaku.Text = "0" '実請求税抜金額

                        Else
                            '**************************************************
                            ' 他請求（3系列）
                            '**************************************************
                            Dim zeinukiGaku As Integer = 0

                            If JibanLogic.GetSeikyuuGaku(sender, _
                                                          3, _
                                                          record.KeiretuCd, _
                                                          TextKhSyouhinCd.Text, _
                                                          syouhinRec.HyoujunKkk, _
                                                          zeinukiGaku) Then
                                ' 実請求金額へセット
                                TextKhJituseikyuuKingaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                            End If

                        End If

                    End If

                Else '3系列以外

                    '工務店(B)
                    '実請求(C)
                    '**************************************************
                    ' 他請求（系列以外）
                    '**************************************************
                    ' 工務店請求額は０
                    TextKhKoumutenSeikyuuKingaku.Text = "0"

                    '実請求税抜金額の自動設定
                    syouhinRec = JibanLogic.GetSyouhinInfo(TextKhSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo, ucGyoumuKyoutuu.AccKameitenCd.Value)
                    If Not syouhinRec Is Nothing Then
                        HiddenKhZeiritu.Value = syouhinRec.Zeiritu '税率
                        HiddenKhZeiKbn.Value = syouhinRec.ZeiKbn '税区分

                        '実請求税抜金額＝商品マスタ.標準価格
                        TextKhJituseikyuuKingaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                    End If

                End If

            End If

            '金額の再計算
            SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo)

        End If

        '画面制御
        SetEnableControlKh()

    End Sub

    ''' <summary>
    ''' (30) [改良工事報告書情報]工務店請求税抜金額変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKhKoumutenSeikyuuKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim syouhinRec As New Syouhin23Record

        'セットフォーカス
        setFocusAJ(TextKhJituseikyuuKingaku)

        '請求
        If SelectKhSeikyuuUmu.SelectedValue = "1" Then '有
            '商品コード
            If TextKhSyouhinCd.Text.Length <> 0 Then '設定済

                '税情報設定
                SetKhZeiInfo(TextKhSyouhinCd.Text)

                '請求先
                If TextKhSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '直接請求

                    '実請求税抜金額＝画面.工務店請求税抜金額
                    TextKhJituseikyuuKingaku.Text = TextKhKoumutenSeikyuuKingaku.Text

                    SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo) '金額再計算

                ElseIf TextKhSeikyuusaki.Text = EarthConst.SEIKYU_TASETU Then '他請求

                    Dim kameitenlogic As New KameitenSearchLogic
                    Dim blnTorikesi As Boolean = False
                    Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(ucGyoumuKyoutuu.Kubun, ucGyoumuKyoutuu.AccKameitenCd.Value, "", blnTorikesi)
                    '(*6)加盟店が取消の場合、他請求3系列以外の扱いとする
                    If record.Torikesi <> 0 Then
                        record.KeiretuCd = ""
                    End If

                    If cl.getKeiretuFlg(record.KeiretuCd) Then '3系列

                        '<表2>実請求税抜金額（掛率）の設定▲

                        Dim logic As New JibanLogic
                        Dim koumuten_gaku As Integer = 0
                        Dim zeinuki_gaku As Integer = 0

                        cl.SetDisplayString(TextKhKoumutenSeikyuuKingaku.Text, koumuten_gaku)
                        koumuten_gaku = IIf(koumuten_gaku = Integer.MinValue, 0, koumuten_gaku)

                        If logic.GetSeikyuuGaku(sender, _
                                                5, _
                                                record.KeiretuCd, _
                                                TextKhSyouhinCd.Text, _
                                                koumuten_gaku, _
                                                zeinuki_gaku) Then


                            '商品情報を取得(キー:商品コード)
                            syouhinRec = JibanLogic.GetSyouhinInfo(TextKhSyouhinCd.Text, EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo, ucGyoumuKyoutuu.AccKameitenCd.Value)
                            If Not syouhinRec Is Nothing Then
                                '(*3)請求有無変更時に、自動設定された工務店請求金額が0（商品マスタ.標準価格＝0）の場合、1回のみ実請求金額の自動設定を行う。
                                If syouhinRec.HyoujunKkk = 0 Then
                                    If HiddenKhJituseikyuu1Flg.Value = "" Then
                                        HiddenKhJituseikyuu1Flg.Value = "1" 'フラグをたてる

                                        ' 税抜金額（実請求金額）へセット
                                        TextKhJituseikyuuKingaku.Text = zeinuki_gaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                                    End If
                                    '*****************
                                    '* 地盤システムの処理に合わせるため、コメントアウト
                                    '*****************
                                    'Else
                                    '    ' 税抜金額（実請求金額）へセット
                                    '    TextKhJituseikyuuKingaku.Text = zeinuki_gaku.ToString(EarthConst.FORMAT_KINGAKU1)

                                End If

                            End If
                        End If

                        SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo) '金額再計算

                    End If

                End If
            End If

        End If

        '画面制御
        SetEnableControlKh()
    End Sub

    ''' <summary>
    ''' (31) [改良工事報告書情報]実請求税抜金額変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKhJituseikyuuKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'セットフォーカス
        setFocusAJ(TextKhSeikyuusyoHakkouDate)

        '実請求税抜金額
        If TextKhJituseikyuuKingaku.Text.Length = 0 Then '入力なし
            TextKhSyouhizei.Text = "" '消費税
            TextKhZeikomiKingaku.Text = "" '税込金額

            SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo) '金額の再計算

        Else '入力あり

            '請求有無
            If SelectKhSeikyuuUmu.SelectedValue = "1" Then '有
                '商品コード
                If TextKhSyouhinCd.Text.Length <> 0 Then '設定済

                    '税情報設定
                    SetKhZeiInfo(TextKhSyouhinCd.Text)

                    '請求先
                    If TextKhSeikyuusaki.Text = EarthConst.SEIKYU_TYOKUSETU Then '直接請求

                        '工務店請求税抜金額＝画面.実請求税抜金額
                        TextKhKoumutenSeikyuuKingaku.Text = TextKhJituseikyuuKingaku.Text

                        SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo) '金額再計算

                    Else '直接請求以外
                        SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo) '金額再計算
                    End If

                End If

            End If

        End If

        '画面制御
        SetEnableControlKh()

    End Sub

    ''' <summary>
    ''' 商品の基本情報をセットする(工事報告書再発行)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSyouhinInfoKh()

        '直接請求、他請求の情報を取得
        Dim syouhinRec As Syouhin23Record

        '商品コード/商品名の自動設定
        syouhinRec = JibanLogic.GetSyouhinInfo("", EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo, Me.ucGyoumuKyoutuu.AccKameitenCd.Value)
        If syouhinRec Is Nothing Then
            TextKhSyouhinCd.Text = "" '商品コード
            SpanKhSyouhinMei.InnerHtml = "" '商品名
        Else
            TextKhSyouhinCd.Text = cl.GetDispStr(syouhinRec.SyouhinCd) '商品コード
            SpanKhSyouhinMei.InnerHtml = cl.GetDispStr(syouhinRec.SyouhinMei) '商品名
            HiddenKhZeiritu.Value = syouhinRec.Zeiritu '税率
            HiddenKhZeiKbn.Value = syouhinRec.ZeiKbn '税区分

            '画面上で請求先が指定されている場合、レコードの請求先を上書き
            If Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value <> String.Empty Then
                '請求先をレコードにセット
                syouhinRec.SeikyuuSakiCd = Me.ucSeikyuuSiireLink.AccSeikyuuSakiCd.Value
                syouhinRec.SeikyuuSakiBrc = Me.ucSeikyuuSiireLink.AccSeikyuuSakiBrc.Value
                syouhinRec.SeikyuuSakiKbn = Me.ucSeikyuuSiireLink.AccSeikyuuSakiKbn.Value
            End If
            Me.TextKhSeikyuusaki.Text = syouhinRec.SeikyuuSakiType '●請求先
        End If

    End Sub

    ''' <summary>
    ''' クリア/工事報告書再発行
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearControlKh()

        SelectKhSeikyuuUmu.SelectedValue = "" '請求有無
        SpanKhSeikyuuUmu.Style.Add("display", "none") 'SPAN請求有無
        SpanKhSeikyuuUmu.InnerHtml = "" 'SPAN請求有無

        TextKhSyouhinCd.Text = "" '商品コード
        SpanKhSyouhinMei.InnerHtml = "" '商品名
        TextKhKoumutenSeikyuuKingaku.Text = "" '工務店請求金額
        TextKhJituseikyuuKingaku.Text = "" '実請求金額
        TextKhSyouhizei.Text = "" '消費税
        HiddenKhSiireGaku.Value = ""    '仕入れ額
        HiddenKhSiireSyouhiZei.Value = ""   '仕入消費税額
        TextKhZeikomiKingaku.Text = "" '税込金額
        TextKhSeikyuusyoHakkouDate.Text = "" '請求書発行日
        TextKhUriageNengappi.Text = "" '売上年月日
        TextKhHattyuusyoKakutei.Text = "" '発注書確定

        '金額の再計算
        SetKingakuUriage(EnumKoujiType.KairyouKoujiHoukokusyo)

    End Sub

#End Region

#Region "改良工事変更時処理"

    ''' <summary>
    ''' (2) [改良工事情報][改良工事]工事仕様確認変更時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKjKoujiSiyouKakunin_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim TyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)
        Dim strKameitenCd As String = Me.ucGyoumuKyoutuu.AccKameitenCd.Value
        Dim strSyouhinCd As String = Me.SelectKjSyouhinCd.SelectedValue

        setFocusAJ(SelectKjKoujiSiyouKakunin)

        '1.確認日の入力制御
        '工事仕様確認
        If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '有

            If TextKjKakuninDate.Text = "" Then '未入力
                TextKjKakuninDate.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
            End If

        ElseIf SelectKjKoujiSiyouKakunin.SelectedValue = "" Then '空白

            TextKjKakuninDate.Text = ""

        End If

        '2.<改良工事>の画面設定
        '(1)<改良工事>請求書発行日の設定
        If ucGyoumuKyoutuu.Kubun = "E" Then
            '商品コード
            If Me.SelectKjSyouhinCd.SelectedValue <> String.Empty Then '入力済
                '請求有無
                If Me.SelectKjSeikyuuUmu.SelectedValue = "1" Then '有
                    '完工速報着日
                    If Me.TextKjKankouSokuhouTyakuDate.Text <> String.Empty Then '入力済
                        '請求書発行日の自動設定
                        Me.SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                    End If
                End If

            End If
        End If

        '直工事商品チェック
        If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) Then
            '請求有無
            If SelectKjSeikyuuUmu.SelectedValue = "1" Then '有
                '工事仕様確認
                If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '有
                    '工事会社請求
                    If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '未チェック
                        '請求書発行日
                        If TextKjSeikyuusyoHakkouDate.Text = "" Then '空白
                            '請求書発行日の自動設定
                            SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                        End If

                        '売上年月日
                        If TextKjUriageNengappi.Text = "" Then '空白
                            TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                        End If
                    End If
                End If

            ElseIf SelectKjSeikyuuUmu.SelectedValue = "0" Then '無
                '工事仕様確認
                If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '有
                    '工事会社請求
                    If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '未チェック
                        '売上年月日
                        If TextKjUriageNengappi.Text = "" Then '空白
                            TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                        End If
                    End If
                End If
            End If

            '完工速報着日
            If TextKjKankouSokuhouTyakuDate.Text = "" Then '未入力
                '工事仕様確認
                If SelectKjKoujiSiyouKakunin.SelectedValue = "" Then '空白
                    '請求書発行日
                    TextKjSeikyuusyoHakkouDate.Text = ""
                    '売上年月日
                    TextKjUriageNengappi.Text = ""
                End If
            End If

            '請求有無=有or無
            If Me.SelectKjSeikyuuUmu.SelectedValue = "1" Or Me.SelectKjSeikyuuUmu.SelectedValue = "0" Then
                '完工速報着日
                If TextKjKankouSokuhouTyakuDate.Text = "" Then
                    '工事仕様確認=有でかつ、工事会社請求有無=チェック状態
                    If Me.SelectKjKoujiSiyouKakunin.SelectedValue = "1" And Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked Then
                        '請求書発行日
                        TextKjSeikyuusyoHakkouDate.Text = ""
                        '売上年月日
                        TextKjUriageNengappi.Text = ""
                    End If
                End If
            End If

        End If

        '画面制御
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (5) [改良工事情報][改良工事]工事会社請求変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub CheckBoxKjKoujiKaisyaSeikyuu_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strSyouhinCd As String = Me.SelectKjSyouhinCd.SelectedValue
        Dim strKoujiCd As String = Me.TextKjKoujiKaisyaCd.Text.Trim()
        Dim strKameitenCd As String = Me.ucGyoumuKyoutuu.AccKameitenCd.Value
        Dim dicSeikyuu As New Dictionary(Of String, String)

        setFocusAJ(CheckBoxKjKoujiKaisyaSeikyuu)

        '1.<改良工事>の自動設定
        Me.ChgKjKaisyaSeikyuuUmu()

        '画面制御
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' 自動設定(請求書発行日、売上年月日)/工事会社請求有無変更時処理[共通]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChgKjKaisyaSeikyuuUmu()
        Dim strKameitenCd As String = Me.ucGyoumuKyoutuu.AccKameitenCd.Value
        Dim strSyouhinCd As String = Me.SelectKjSyouhinCd.SelectedValue
        '商品コード
        If SelectKjSyouhinCd.SelectedValue <> "" Then '入力
            '請求有無
            If SelectKjSeikyuuUmu.SelectedValue = "1" Then '有
                '完工速報着日
                If TextKjKankouSokuhouTyakuDate.Text <> "" Then '入力
                    '請求書発行日の自動設定
                    SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                End If
            End If

            '直工事商品チェック
            If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) Then
                '完工速報着日
                If TextKjKankouSokuhouTyakuDate.Text = "" Then '未入力
                    '工事会社請求
                    If CheckBoxKjKoujiKaisyaSeikyuu.Checked Then 'チェック
                        '請求書発行日
                        TextKjSeikyuusyoHakkouDate.Text = ""
                        '売上年月日
                        TextKjUriageNengappi.Text = ""
                    End If
                End If

                '請求有無
                If SelectKjSeikyuuUmu.SelectedValue = "1" Then '有
                    '工事仕様確認
                    If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '有
                        '工事会社請求
                        If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '未チェック

                            '請求書発行日の自動設定
                            SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)

                            '売上年月日
                            If TextKjUriageNengappi.Text = "" Then '空白
                                TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                            End If
                        End If
                    End If

                ElseIf SelectKjSeikyuuUmu.SelectedValue = "0" Then '無
                    '工事仕様確認
                    If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '有
                        '工事会社請求
                        If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '未チェック
                            '請求書発行日
                            TextKjSeikyuusyoHakkouDate.Text = ""
                            '売上年月日
                            If TextKjUriageNengappi.Text = "" Then '空白
                                TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                            End If
                        End If
                    End If

                End If

                '請求有無=有or無
                If Me.SelectKjSeikyuuUmu.SelectedValue = "1" Or Me.SelectKjSeikyuuUmu.SelectedValue = "0" Then
                    '完工速報着日
                    If TextKjKankouSokuhouTyakuDate.Text = "" Then
                        '工事仕様確認=有でかつ、工事会社請求有無=チェック状態
                        If Me.SelectKjKoujiSiyouKakunin.SelectedValue = "1" And Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked Then
                            '請求書発行日
                            TextKjSeikyuusyoHakkouDate.Text = ""
                            '売上年月日
                            TextKjUriageNengappi.Text = ""
                        End If
                    End If
                End If

            End If

        End If

        '画面制御
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (6) [改良工事情報][改良工事]改良工事種別変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKjKairyouKoujiSyubetu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim tmpScript As String = ""
        Dim KjLogic As New KairyouKoujiLogic
        Dim blnRet As Boolean = False

        setFocusAJ(SelectKjKairyouKoujiSyubetu)

        If SelectKjKairyouKoujiSyubetu.SelectedValue <> String.Empty Then
            '1.判定改良工事種別のチェック

            '①判定工事種別設定マスタより、以下の条件に該当するデータを抽出する。
            blnRet = KjLogic.GetKairyouKoujiSyubetu( _
                            HiddenHanteiSetuzokuMoji.Value _
                            , SelectKjKairyouKoujiSyubetu.SelectedValue _
                            , HiddenHantei1Cd.Value _
                            , HiddenHantei2Cd.Value _
                            )

            '②該当データが存在しない場合、もしくは、<改良工事情報>判定接続＝2（又は）の場合、以下の処理を行う。▲
            If blnRet = False Or HiddenHanteiSetuzokuMoji.Value = "2" Then '又は
                '判定工事種別確認メッセージを表示する。
                If SelectKjKairyouKoujiSyubetu.SelectedValue <> HiddenKjKairyouKoujiSyubetuMae.Value Then
                    tmpScript = "checkKoujiSyubetu('" & Messages.MSG079C & "','" & Messages.MSG080C & "','" & SelectKjKairyouKoujiSyubetu.ClientID & "','" & HiddenKjKairyouKoujiSyubetuMae.ClientID & "');"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectKjKairyouKoujiSyubetu_SelectedIndexChanged", tmpScript, True)
                End If
            End If
        End If

        '画面制御
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (8) [改良工事情報][改良工事]商品コード変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKjSyouhinCd_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim KjLogic As New KairyouKoujiLogic
        Dim SyouhinMeisaiRecord As New SyouhinMeisaiRecord
        Dim tmpScript As String = ""

        setFocusAJ(SelectKjSyouhinCd)

        Dim strKameitenCd As String = Me.ucGyoumuKyoutuu.AccKameitenCd.Value
        Dim strSyouhinCd As String = Me.SelectKjSyouhinCd.SelectedValue

        '1.<改良工事>の自動設定
        '商品コード
        If SelectKjSyouhinCd.SelectedValue <> "" Then '設定済

            '保証商品有無<>"1"かつ、変更前.商品コード=未入力
            If Me.HiddenHosyouSyouhinUmuOld.Value <> "1" And HiddenKjSyouhinCdMae.Value = "" Then

                '商品コード確認メッセージを表示する。
                If SelectKjSyouhinCd.SelectedValue <> HiddenKjSyouhinCdMae.Value Then
                    tmpScript = "ChkSyohinCd('" & Messages.MSG115C & "','" & SelectKjSyouhinCd.ClientID & "','" & HiddenKjSyouhinCdMae.ClientID & "');"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectKjSyouhinCd_SelectedIndexChanged", tmpScript, True)
                End If

            End If

            '請求有無が空白の場合
            If SelectKjSeikyuuUmu.SelectedValue = "" Then
                SelectKjSeikyuuUmu.SelectedValue = "1" '有
            End If

            '********************************************************************
            '★商品マスタと工事価格マスタからの取得を切り替える

            '商品・売上金額・請求有無の自動設定（商品M or 工事価格M）
            If SetSyouhinInfoFromKojM(EnumKoujiType.KairyouKouji, EarthEnum.emKojKkkActionType.SyouhinCd) = False Then
                SetSyouhinInfo(EnumKoujiType.KairyouKouji)
            End If

            '金額の再計算
            SetKingakuUriage(EnumKoujiType.KairyouKouji)

            '発注書確定が空白の場合
            If TextKjHattyuusyoKakutei.Text = "" Then
                TextKjHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI '未確定
            End If

            '請求有無
            If SelectKjSeikyuuUmu.SelectedValue = "1" Then '有

                '完工速報着日
                If TextKjKankouSokuhouTyakuDate.Text <> "" Then '設定済

                    '請求書発行日
                    If TextKjSeikyuusyoHakkouDate.Text = "" Then '空白
                        '請求書発行日の自動設定
                        SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                    End If

                    '売上年月日
                    If TextKjUriageNengappi.Text = "" Then '空白
                        TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If

                End If

            ElseIf Me.SelectKjSeikyuuUmu.SelectedValue = "0" Or Me.SelectKjSeikyuuUmu.SelectedValue = "" Then   '無し
                '請求書発行日の自動設定
                Me.TextKjSeikyuusyoHakkouDate.Text = ""
            End If

            '**********************************************************************

            '直工事商品チェック
            '変更前=B2000番台でかつ、変更後=B2000番台以外
            If cl.ChkSyouhinCdB2000(HiddenKjSyouhinCdMae.Value) AndAlso cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) = False Then
                '邸別請求テーブル.売上計上FLG<>1、かつ、完工速報着日=未入力
                If Me.SpanKjUriageSyoriZumi.InnerHtml <> EarthConst.URIAGE_ZUMI And Me.TextKjKankouSokuhouTyakuDate.Text = String.Empty Then '未入力
                    '請求書発行日
                    Me.TextKjSeikyuusyoHakkouDate.Text = String.Empty
                    '売上年月日
                    Me.TextKjUriageNengappi.Text = String.Empty
                End If
            End If

            '直工事商品チェック
            If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) Then
                '請求有無
                If SelectKjSeikyuuUmu.SelectedValue = "1" Then '有
                    '工事仕様確認
                    If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '有
                        '工事会社請求
                        If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '未チェック
                            '請求書発行日
                            If TextKjSeikyuusyoHakkouDate.Text = "" Then '空白
                                '請求書発行日の自動設定
                                SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                            End If

                            '売上年月日
                            If TextKjUriageNengappi.Text = "" Then '空白
                                TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                            End If
                        End If
                    End If

                Else '無
                    '工事仕様確認
                    If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '有
                        '工事会社請求
                        If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '未チェック
                            '売上年月日
                            If TextKjUriageNengappi.Text = "" Then '空白
                                TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                            End If
                        End If
                    End If

                End If

                '請求有無=有or無
                If Me.SelectKjSeikyuuUmu.SelectedValue = "1" Or Me.SelectKjSeikyuuUmu.SelectedValue = "0" Then
                    '完工速報着日
                    If TextKjKankouSokuhouTyakuDate.Text = "" Then
                        '工事仕様確認=有でかつ、工事会社請求有無=チェック状態
                        If Me.SelectKjKoujiSiyouKakunin.SelectedValue = "1" And Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked Then
                            '請求書発行日
                            TextKjSeikyuusyoHakkouDate.Text = ""
                            '売上年月日
                            TextKjUriageNengappi.Text = ""
                        End If
                    End If
                End If

            End If

        Else '未入力

            '(*1)発注書金額≠0,かつ,≠Nullの場合、エラーメッセージ（037）を表示し、自動設定（クリア）を行わずに、未入力に変更した項目(発送日/再発行日/請求有無)は元の値に戻す。
            If TextKjHattyuusyoKingaku.Text <> "0" And TextKjHattyuusyoKingaku.Text <> "" Then
                tmpScript = "alert('" & Messages.MSG010E & "');" & vbCrLf
                SelectKjSyouhinCd.SelectedValue = HiddenKjSyouhinCdMae.Value '変更前の値に戻す ※注 Javascriptでの設定ではChangeイベントが発生しない
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectKjSyouhinCd_SelectedIndexChanged", tmpScript, True)
                Exit Sub
            End If

            SelectKjSeikyuuUmu.SelectedValue = "" '請求
            TextKjUriageNengappi.Text = "" '売上年月日
            TextKjUriageZeinukiKingaku.Text = "" '売上金額/税抜金額
            TextKjUriageSyouhizei.Text = "" '売上金額/消費税
            TextKjUriageZeikomiKingaku.Text = "" '売上金額/税込金額
            TextKjSiireZeinukiKingaku.Text = "" '仕入金額/税抜金額
            TextKjSiireSyouhizei.Text = "" '仕入金額/消費税
            TextKjSiireZeikomiKingaku.Text = "" '仕入金額/税込金額
            TextKjZangaku.Text = "0" '残額
            TextKjSeikyuusyoHakkouDate.Text = "" '請求書発行日
            TextKjHattyuusyoKakutei.Text = "" '発注書確定

        End If

        '画面制御
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (9) [改良工事情報][改良工事]請求有無変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectKjSeikyuuUmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strKameitenCd As String = Me.ucGyoumuKyoutuu.AccKameitenCd.Value
        Dim strSyouhinCd As String = Me.SelectKjSyouhinCd.SelectedValue

        setFocusAJ(SelectKjSeikyuuUmu)

        '商品コード
        If SelectKjSyouhinCd.SelectedValue <> "" Then '設定済

            '請求有無
            If SelectKjSeikyuuUmu.SelectedValue = "1" Then '有

                '売上金額/税抜金額が空白の場合
                If TextKjUriageZeinukiKingaku.Text = "" Or TextKjUriageZeinukiKingaku.Text = "0" Then

                    '商品・売上金額・請求有無の自動設定（商品M or 工事価格M）
                    If SetSyouhinInfoFromKojM(EnumKoujiType.KairyouKouji, EarthEnum.emKojKkkActionType.SeikyuuUmu) = False Then
                        SetSyouhinInfo(EnumKoujiType.KairyouKouji)
                    End If

                End If

                SetKingakuUriage(EnumKoujiType.KairyouKouji) '金額の再計算

                '直工事商品チェック
                If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) Then
                    '工事仕様確認
                    If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '有
                        '工事会社請求
                        If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '未チェック
                            '請求書発行日
                            If TextKjSeikyuusyoHakkouDate.Text = "" Then '空白
                                '請求書発行日の自動設定
                                SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                            End If
                            '売上年月日
                            TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)

                        End If
                    End If

                    '請求有無=有or無
                    If Me.SelectKjSeikyuuUmu.SelectedValue = "1" Or Me.SelectKjSeikyuuUmu.SelectedValue = "0" Then
                        '完工速報着日
                        If TextKjKankouSokuhouTyakuDate.Text = "" Then
                            '工事仕様確認=有でかつ、工事会社請求有無=チェック状態
                            If Me.SelectKjKoujiSiyouKakunin.SelectedValue = "1" And Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked Then
                                '請求書発行日
                                TextKjSeikyuusyoHakkouDate.Text = ""
                                '売上年月日
                                TextKjUriageNengappi.Text = ""
                            End If
                        End If
                    End If

                End If

                '完工速報着日
                If TextKjKankouSokuhouTyakuDate.Text <> "" Then '設定済

                    '請求書発行日の自動設定▲
                    If TextKjSeikyuusyoHakkouDate.Text = "" Then '空白
                        '請求書発行日の自動設定
                        SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)

                    End If

                    '売上年月日
                    TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)

                End If


            ElseIf SelectKjSeikyuuUmu.SelectedValue = "0" Or SelectKjSeikyuuUmu.SelectedValue = "" Then '無or空白

                '売上金額系の0クリア
                TextKjUriageZeinukiKingaku.Text = "0"
                TextKjUriageSyouhizei.Text = "0"
                TextKjUriageZeikomiKingaku.Text = "0"

                SetKingakuUriage(EnumKoujiType.KairyouKouji) '金額の再計算

                TextKjSeikyuusyoHakkouDate.Text = "" '請求書発行日

                '直工事商品チェック
                If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) Then
                    '工事仕様確認
                    If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '有
                        '工事会社請求
                        If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '未チェック
                            '売上年月日
                            TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                        End If
                    End If

                    '請求有無=有or無
                    If Me.SelectKjSeikyuuUmu.SelectedValue = "1" Or Me.SelectKjSeikyuuUmu.SelectedValue = "0" Then
                        '完工速報着日
                        If TextKjKankouSokuhouTyakuDate.Text = "" Then
                            '工事仕様確認=有でかつ、工事会社請求有無=チェック状態
                            If Me.SelectKjKoujiSiyouKakunin.SelectedValue = "1" And Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked Then
                                '請求書発行日
                                TextKjSeikyuusyoHakkouDate.Text = ""
                                '売上年月日
                                TextKjUriageNengappi.Text = ""
                            End If
                        End If
                    End If
                End If

                '完工速報着日
                If TextKjKankouSokuhouTyakuDate.Text <> "" Then '設定済
                    '売上年月日
                    TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                End If

            End If

        End If


        '画面制御
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (10) [改良工事情報][改良工事]改良売上金額（税抜）変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKjUriageZeinukiKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(TextKjSiireZeinukiKingaku)

        SetKingakuUriage(EnumKoujiType.KairyouKouji) '金額の再計算

        '画面制御
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (11) [改良工事情報][改良工事]改良仕入金額（税抜）変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKjSiireZeinukiKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(TextKjKoujiDate)

        SetKingakuSiire(EnumKoujiType.KairyouKouji) '金額の再計算

        '画面制御
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (13) [改良工事情報][改良工事]完工速報着日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKjKankouSokuhouTyakuDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strKameitenCd As String = Me.ucGyoumuKyoutuu.AccKameitenCd.Value
        Dim strSyouhinCd As String = Me.SelectKjSyouhinCd.SelectedValue

        setFocusAJ(TextKjKankouSokuhouTyakuDate)

        '商品コード
        If SelectKjSyouhinCd.SelectedValue <> "" Then '設定済

            '請求有無
            If SelectKjSeikyuuUmu.SelectedValue = "1" Then '有

                setFocusAJ(TextKjSeikyuusyoHakkouDate)

                '完工速報着日
                If TextKjKankouSokuhouTyakuDate.Text <> "" Then '設定済

                    '請求書発行日の自動設定▲
                    If TextKjSeikyuusyoHakkouDate.Text = "" Then '空白
                        '請求書発行日の自動設定
                        SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)

                    End If

                    '売上年月日
                    If TextKjUriageNengappi.Text = "" Then '空白
                        TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If
                End If

            ElseIf SelectKjSeikyuuUmu.SelectedValue = "0" Then '無

                '完工速報着日
                If TextKjKankouSokuhouTyakuDate.Text <> "" Then '設定済
                    '売上年月日
                    If TextKjUriageNengappi.Text = "" Then '空白
                        TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If
                End If

            End If

            '直工事商品チェック
            If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) = False Then '<>直工事
                '邸別請求テーブル.売上計上FLG<>1、かつ、完工速報着日=未入力
                If Me.SpanKjUriageSyoriZumi.InnerHtml <> EarthConst.URIAGE_ZUMI And Me.TextKjKankouSokuhouTyakuDate.Text = String.Empty Then '未入力
                    '請求書発行日
                    Me.TextKjSeikyuusyoHakkouDate.Text = String.Empty
                    '売上年月日
                    Me.TextKjUriageNengappi.Text = String.Empty
                End If

            Else '直工事

                '邸別請求テーブル.売上計上FLG<>1、かつ、完工速報着日=未入力
                If Me.SpanKjUriageSyoriZumi.InnerHtml <> EarthConst.URIAGE_ZUMI And TextKjKankouSokuhouTyakuDate.Text = String.Empty Then '未入力

                    '工事仕様確認
                    If Me.SelectKjKoujiSiyouKakunin.SelectedValue = String.Empty Then '空白
                        '請求書発行日
                        Me.TextKjSeikyuusyoHakkouDate.Text = String.Empty
                        '売上年月日
                        Me.TextKjUriageNengappi.Text = String.Empty
                    End If

                    '工事会社請求
                    If CheckBoxKjKoujiKaisyaSeikyuu.Checked Then 'チェック
                        '請求書発行日
                        Me.TextKjSeikyuusyoHakkouDate.Text = String.Empty
                        '売上年月日
                        Me.TextKjUriageNengappi.Text = String.Empty
                    End If

                End If

            End If

        End If

        '画面制御
        SetEnableControlKj()

    End Sub

    ''' <summary>
    ''' (14) [改良工事情報][改良工事]請求書発行日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKjSeikyuusyoHakkouDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""

        setFocusAJ(TextKjSeikyuusyoHakkouDate)

        '請求書発行日
        If TextKjSeikyuusyoHakkouDate.Text = "" Then Exit Sub '未入力の場合、処理を抜ける

        '工事日
        '(1)工事日＝未入力の場合
        If TextKjKoujiDate.Text = "" Then '未入力
            '登録許可にOKしていない場合
            If HiddenKjSeikyuusyoHakkouDateMsg1.Value <> "1" Then
                '登録確認メッセージを表示する。
                tmpScript = "if(confirm('" & Messages.MSG078W & "')){" & vbCrLf
                tmpScript &= "  objEBI('" & HiddenKjSeikyuusyoHakkouDateMsg1.ClientID & "').value = '1';" & vbCrLf
                tmpScript &= "}" & vbCrLf
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextKjSeikyuusyoHakkouDate_TextChanged1", tmpScript, True)
            End If
        End If

        '(2)工事日＞<改良工事>請求書発行日の場合
        If TextKjKoujiDate.Text.Length <> 0 And TextKjSeikyuusyoHakkouDate.Text.Length <> 0 Then

            Dim dtKouji As Date = Date.Parse(TextKjKoujiDate.Text)
            Dim dtSeikyuu As Date = Date.Parse(TextKjSeikyuusyoHakkouDate.Text)

            If dtKouji > dtSeikyuu Then

                '登録許可にOKしていない場合
                If HiddenKjSeikyuusyoHakkouDateMsg2.Value <> "1" Then

                    '登録確認メッセージを表示する。
                    '請求書発行日が改良工事日より古い日付ですが、<改行>請求書発行日を登録できるようにしますか？
                    Dim strMsg As String = Messages.MSG066C.Replace("@PARAM1", "工事日")

                    '確認メッセージ表示
                    tmpScript = "if(confirm('" & strMsg & "')){" & vbCrLf
                    tmpScript &= "  objEBI('" & HiddenKjSeikyuusyoHakkouDateMsg2.ClientID & "').value = '1';" & vbCrLf
                    tmpScript &= "}" & vbCrLf
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextKjSeikyuusyoHakkouDate_TextChanged2", tmpScript, True)
                End If

            End If

        End If

    End Sub

#End Region

#Region "追加工事変更時処理"

    ''' <summary>
    ''' (16) [改良工事情報][追加工事]改良工事種別変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectTjKairyouKoujiSyubetu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim tmpScript As String = ""
        Dim KjLogic As New KairyouKoujiLogic
        Dim blnRet As Boolean = False

        setFocusAJ(SelectTjKairyouKoujiSyubetu)

        '1.判定改良工事種別のチェック

        '①判定工事種別設定マスタより、以下の条件に該当するデータを抽出する。
        blnRet = KjLogic.GetKairyouKoujiSyubetu( _
                        HiddenHanteiSetuzokuMoji.Value _
                        , SelectTjKairyouKoujiSyubetu.SelectedValue _
                        , HiddenHantei1Cd.Value _
                        , HiddenHantei2Cd.Value _
                        )

        '②該当データが存在しない場合、もしくは、<改良工事情報>判定接続＝2（又は）の場合、以下の処理を行う。▲
        If blnRet = False Or HiddenHanteiSetuzokuMoji.Value = "2" Then '又は
            '判定工事種別確認メッセージを表示する。
            If SelectTjKairyouKoujiSyubetu.SelectedValue <> HiddenTjKairyouKoujiSyubetuMae.Value Then
                tmpScript = "checkKoujiSyubetu('" & Messages.MSG079C & "','" & Messages.MSG080C & "','" & SelectTjKairyouKoujiSyubetu.ClientID & "','" & HiddenTjKairyouKoujiSyubetuMae.ClientID & "');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectTjKairyouKoujiSyubetu_SelectedIndexChanged", tmpScript, True)
            End If
        End If

        '画面制御
        SetEnableControlTj()

    End Sub

    ''' <summary>
    ''' (19) [改良工事情報][追加工事]完工速報着日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextTjKankouSokuhouTyakuDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strSyouhinCd As String = SelectTjSyouhinCd.SelectedValue

        setFocusAJ(TextTjKankouSokuhouTyakuDate)

        '商品コード
        If SelectTjSyouhinCd.SelectedValue <> "" Then '設定済

            '請求有無
            If SelectTjSeikyuuUmu.SelectedValue = "1" Then '有

                setFocusAJ(TextTjSeikyuusyoHakkouDate)

                '完工速報着日
                If TextTjKankouSokuhouTyakuDate.Text <> "" Then '設定済

                    '請求書発行日の自動設定▲
                    If TextTjSeikyuusyoHakkouDate.Text = "" Then '空白
                        '請求書発行日の自動設定
                        SetTjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                    End If

                    '売上年月日
                    If TextTjUriageNengappi.Text = "" Then '空白
                        TextTjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If

                Else '未入力
                    '未売上
                    If SpanTjUriageSyorizumi.InnerHtml <> EarthConst.URIAGE_ZUMI Then
                        '画面起動時に完工速報着日が空で、未売上状態だった場合
                        '請求書発行日,売上年月日のをクリア
                        TextTjSeikyuusyoHakkouDate.Text = String.Empty
                        TextTjUriageNengappi.Text = String.Empty
                    End If
                End If

            ElseIf SelectTjSeikyuuUmu.SelectedValue = "0" Then '無

                '完工速報着日
                If TextTjKankouSokuhouTyakuDate.Text <> "" Then '設定済
                    '売上年月日
                    TextTjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)

                Else '未入力

                    '未売上
                    If SpanTjUriageSyorizumi.InnerHtml <> EarthConst.URIAGE_ZUMI Then
                        '請求書発行日,売上年月日のをクリア
                        TextTjSeikyuusyoHakkouDate.Text = String.Empty
                        TextTjUriageNengappi.Text = String.Empty
                    End If
                End If

            End If
        End If

        '画面制御
        SetEnableControlTj()

    End Sub

    ''' <summary>
    ''' (20) [改良工事情報][追加工事]商品コード変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectTjSyouhinCd_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strSyouhinCd As String = SelectTjSyouhinCd.SelectedValue

        setFocusAJ(SelectTjSyouhinCd)

        '1.<追加工事>の自動設定
        '商品コード
        If SelectTjSyouhinCd.SelectedValue <> "" Then '設定済

            '請求有無が空白の場合
            If SelectTjSeikyuuUmu.SelectedValue = "" Then
                SelectTjSeikyuuUmu.SelectedValue = "1" '有
            End If

            '商品・売上金額・請求有無の自動設定（商品M or 工事価格M）
            If SetSyouhinInfoFromKojM(EnumKoujiType.TuikaKouji, EarthEnum.emKojKkkActionType.SyouhinCd) = False Then
                SetSyouhinInfo(EnumKoujiType.TuikaKouji)
            End If

            '金額の再計算
            SetKingakuUriage(EnumKoujiType.TuikaKouji)

            '発注書確定が空白の場合
            If TextTjHattyuusyoKakutei.Text = "" Then
                TextTjHattyuusyoKakutei.Text = EarthConst.MIKAKUTEI '未確定
            End If

            '請求有無
            If SelectTjSeikyuuUmu.SelectedValue = "1" Then '有

                '完工速報着日
                If TextTjKankouSokuhouTyakuDate.Text <> "" Then '設定済

                    '請求書発行日
                    If TextTjSeikyuusyoHakkouDate.Text = "" Then '空白
                        '請求書発行日の自動設定
                        SetTjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                    End If

                    '売上年月日
                    If TextTjUriageNengappi.Text = "" Then '空白
                        TextTjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                    End If

                End If

            ElseIf Me.SelectTjSeikyuuUmu.SelectedValue = "0" Or Me.SelectTjSeikyuuUmu.SelectedValue = "" Then   '無し
                '請求書発行日の自動設定
                Me.TextTjSeikyuusyoHakkouDate.Text = ""
            End If


        Else '未入力

            '(*1)発注書金額≠0,かつ,≠Nullの場合、エラーメッセージ（037）を表示し、自動設定（クリア）を行わずに、未入力に変更した項目(発送日/再発行日/請求有無)は元の値に戻す。
            If TextTjHattyuusyoKingaku.Text <> "0" And TextTjHattyuusyoKingaku.Text <> "" Then
                Dim tmpScript As String = "alert('" & Messages.MSG010E & "');" & vbCrLf
                SelectTjSyouhinCd.SelectedValue = HiddenTjSyouhinCdMae.Value '変更前の値に戻す ※注 Javascriptでの設定ではChangeイベントが発生しない
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SelectTjSyouhinCd_SelectedIndexChanged", tmpScript, True)
                Exit Sub
            End If

            SelectTjSeikyuuUmu.SelectedValue = "" '請求
            TextTjUriageNengappi.Text = "" '売上年月日
            TextTjUriageZeinukiKingaku.Text = "" '売上金額/税抜金額
            TextTjUriageSyouhizei.Text = "" '売上金額/消費税
            TextTjUriageZeikomiKingaku.Text = "" '売上金額/税込金額
            TextTjSiireZeinukiKingaku.Text = "" '仕入金額/税抜金額
            TextTjSiireSyouhizei.Text = "" '仕入金額/消費税
            TextTjSiireZeikomiKingaku.Text = "" '仕入金額/税込金額
            TextTjZangaku.Text = "0" '残額
            TextTjSeikyuusyoHakkouDate.Text = "" '請求書発行日
            TextTjHattyuusyoKakutei.Text = "" '発注書確定

        End If

        '画面制御
        SetEnableControlTj()

    End Sub

    ''' <summary>
    ''' (21) [改良工事情報][追加工事]請求有無変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectTjSeikyuuUmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strSyouhinCd As String = SelectTjSyouhinCd.SelectedValue

        setFocusAJ(SelectTjSeikyuuUmu)

        '商品コード
        If SelectTjSyouhinCd.SelectedValue <> "" Then '設定済

            '請求有無
            If SelectTjSeikyuuUmu.SelectedValue = "1" Then '有

                '売上金額/税抜金額が空白の場合
                If TextTjUriageZeinukiKingaku.Text = "" Or TextTjUriageZeinukiKingaku.Text = "0" Then

                    '商品・売上金額・請求有無の自動設定（商品M or 工事価格M）
                    If SetSyouhinInfoFromKojM(EnumKoujiType.TuikaKouji, EarthEnum.emKojKkkActionType.SeikyuuUmu) = False Then
                        SetSyouhinInfo(EnumKoujiType.TuikaKouji)
                    End If

                End If

                SetKingakuUriage(EnumKoujiType.TuikaKouji) '金額の再計算

                '完工速報着日
                If TextTjKankouSokuhouTyakuDate.Text <> "" Then '設定済

                    '請求書発行日の自動設定▲
                    If TextTjSeikyuusyoHakkouDate.Text = "" Then '空白
                        '請求書発行日の自動設定
                        SetTjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                    End If

                    '売上年月日
                    TextTjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)

                End If


            ElseIf SelectTjSeikyuuUmu.SelectedValue = "0" Or SelectTjSeikyuuUmu.SelectedValue = "" Then '無 or 空白

                '売上金額系の0クリア
                TextTjUriageZeinukiKingaku.Text = "0"
                TextTjUriageSyouhizei.Text = "0"
                TextTjUriageZeikomiKingaku.Text = "0"

                SetKingakuUriage(EnumKoujiType.TuikaKouji) '金額の再計算

                TextTjSeikyuusyoHakkouDate.Text = "" '請求書発行日

                '完工速報着日
                If TextTjKankouSokuhouTyakuDate.Text <> "" Then '設定済
                    '売上年月日
                    TextTjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                End If

            End If

        End If

        '画面制御
        SetEnableControlTj()
    End Sub

    ''' <summary>
    ''' (22) [改良工事情報][追加工事]追加改良売上金額（税抜）変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextTjUriageZeinukiKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(TextTjSiireZeinukiKingaku)

        SetKingakuUriage(EnumKoujiType.TuikaKouji) '金額の再計算

        '画面制御
        SetEnableControlTj()

    End Sub

    ''' <summary>
    ''' (23) [改良工事情報][追加工事]追加改良仕入金額（税抜）変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextTjSiireZeinukiKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(TextTjKoujiDate)

        SetKingakuSiire(EnumKoujiType.TuikaKouji) '金額の再計算

        '画面制御
        SetEnableControlTj()

    End Sub

    ''' <summary>
    ''' (24) [改良工事情報][追加工事]請求書発行日変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextTjSeikyuusyoHakkouDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tmpScript As String = ""

        setFocusAJ(TextTjSeikyuusyoHakkouDate)

        '請求書発行日
        If TextTjSeikyuusyoHakkouDate.Text = "" Then Exit Sub '未入力の場合、処理を抜ける

        '工事日
        '(1)工事日＝未入力の場合
        If TextTjKoujiDate.Text = "" Then '未入力
            '登録許可にOKしていない場合
            If HiddenTjSeikyuusyoHakkouDateMsg1.Value <> "1" Then
                '登録確認メッセージを表示する。
                tmpScript = "if(confirm('" & Messages.MSG078W & "')){" & vbCrLf
                tmpScript &= "  objEBI('" & HiddenTjSeikyuusyoHakkouDateMsg1.ClientID & "').value = '1';" & vbCrLf
                tmpScript &= "}" & vbCrLf
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextTjSeikyuusyoHakkouDate_TextChanged1", tmpScript, True)
            End If
        End If

        '(2)工事日＞<改良工事>請求書発行日の場合
        If TextTjKoujiDate.Text.Length <> 0 And TextTjSeikyuusyoHakkouDate.Text.Length <> 0 Then

            Dim dtKouji As Date = Date.Parse(TextTjKoujiDate.Text)
            Dim dtSeikyuu As Date = Date.Parse(TextTjSeikyuusyoHakkouDate.Text)

            If dtKouji > dtSeikyuu Then

                '登録許可にOKしていない場合
                If HiddenTjSeikyuusyoHakkouDateMsg2.Value <> "1" Then

                    '登録確認メッセージを表示する。
                    '請求書発行日が改良工事日より古い日付ですが、<改行>請求書発行日を登録できるようにしますか？
                    Dim strMsg As String = Messages.MSG066C.Replace("@PARAM1", "工事日")

                    setFocusAJ(TextTjSeikyuusyoHakkouDate)

                    '確認メッセージ表示
                    tmpScript = "if(confirm('" & strMsg & "')){" & vbCrLf
                    tmpScript &= "  objEBI('" & HiddenTjSeikyuusyoHakkouDateMsg2.ClientID & "').value = '1';" & vbCrLf
                    tmpScript &= "}" & vbCrLf
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "TextTjSeikyuusyoHakkouDate_TextChanged2", tmpScript, True)
                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' (5) [改良工事情報][追加工事]工事会社請求変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub CheckBoxTjKoujiKaisyaSeikyuu_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        setFocusAJ(CheckBoxTjKoujiKaisyaSeikyuu)

        '工事会社請求有無変更時処理[共通]
        Me.ChgTjKoujiKaisyaSeikyuuUmu()

        '画面制御
        SetEnableControlTj()

    End Sub

    ''' <summary>
    ''' 追加工事.自動設定(請求書発行日、売上年月日)/工事会社請求有無変更時処理[共通]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChgTjKoujiKaisyaSeikyuuUmu()
        Dim strSyouhinCd As String = Me.SelectTjSyouhinCd.SelectedValue
        Dim strKoujiCd As String = Me.TextTjKoujiKaisyaCd.Text.Trim()
        Dim strKameitenCd As String = Me.ucGyoumuKyoutuu.AccKameitenCd.Value

        '2.<追加工事>の自動設定
        '商品コード
        If SelectTjSyouhinCd.SelectedValue <> "" Then '入力
            '請求有無
            If SelectTjSeikyuuUmu.SelectedValue = "1" Then '有
                '完工速報着日
                If TextTjKankouSokuhouTyakuDate.Text <> "" Then '入力
                    '請求書発行日の自動設定
                    SetTjAutoSeikyuusyoHakkouDate(strSyouhinCd)
                End If
            End If
        End If

    End Sub

#End Region

    ''' <summary>
    ''' ダミーボタン押下にてPostBack処理を行なう(商品コード変更時処理のキャンセルのため)
    ''' ※初期読込時のみ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonDummy_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.ButtonDummy.Value = "1"

        If ButtonTouroku1.Disabled = False Then
            setFocusAJ(ButtonTouroku1)
        End If

        Me.UpdatePanelKairyouKouji.Update()

    End Sub

    ''' <summary>
    ''' コピーボタン押下時の請求書発行日、売上年月日のチェックおよび自動設定
    ''' ※初期読込時なので、請求書発行日で必要な加盟店コード、区分は別途指定(ユーザーコントロール指定のため)
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ChkOnCopyAutoSetting(ByVal strKameitenCd As String, ByVal strKbn As String)
        Dim strSyouhinCd As String = SelectKjSyouhinCd.SelectedValue

        '売上処理済判断フラグの取得
        Dim strUriageZumi As String = cl.GetUriageSyoriZumiFlg(Me.SpanKjUriageSyoriZumi.InnerHtml)
        '表示モードの取得
        Dim strViewMode As String = cl.GetViewMode(strUriageZumi, userinfo.KeiriGyoumuKengen)
        '請求書発行日の自動設定用に、請求先・仕入先変更リンクに加盟店等の情報をセット
        Me.ucSeikyuuSiireLinkKai.SetVariableValueCtrlFromParent(strKameitenCd _
                                                                , Me.SelectKjSyouhinCd.SelectedValue _
                                                                , Me.TextKjKoujiKaisyaCd.Text _
                                                                , strUriageZumi _
                                                                , Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked _
                                                                , Me.TextKjKoujiKaisyaCd.Text _
                                                                , _
                                                                , strViewMode)

        '物件指定にてコピー処理を行なった場合、請求書発行日/売上年月日の自動設定を行なう
        If Not pStrCopy Is Nothing AndAlso pStrCopy = "1" Then

            '商品コード
            If SelectKjSyouhinCd.SelectedValue <> "" Then '入力
                '請求有無
                If SelectKjSeikyuuUmu.SelectedValue = "1" Then '有
                    '1)完工速報着日
                    If TextKjKankouSokuhouTyakuDate.Text <> "" Then '入力
                        '請求書発行日の自動設定
                        SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)

                        '売上年月日
                        If TextKjUriageNengappi.Text = "" Then '空白
                            TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                        End If

                    End If

                ElseIf SelectKjSeikyuuUmu.SelectedValue = "0" Then '無

                    '2)完工速報着日
                    If TextKjKankouSokuhouTyakuDate.Text <> String.Empty Then '入力
                        '請求書発行日
                        TextKjSeikyuusyoHakkouDate.Text = String.Empty

                        '売上年月日
                        If TextKjUriageNengappi.Text = "" Then '空白
                            TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                        End If
                    End If

                End If

                '直工事商品チェック
                If cl.ChkSyouhinCdB2000(SelectKjSyouhinCd.SelectedValue) Then

                    '請求有無
                    If SelectKjSeikyuuUmu.SelectedValue = "1" Then '有
                        '工事仕様確認
                        If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '有
                            '3)工事会社請求
                            If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '未チェック

                                '請求書発行日の自動設定
                                SetKjAutoSeikyuusyoHakkouDate(strSyouhinCd)

                                '売上年月日
                                If TextKjUriageNengappi.Text = "" Then '空白
                                    TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                                End If
                            End If
                        End If

                    ElseIf SelectKjSeikyuuUmu.SelectedValue = "0" Then '無
                        '工事仕様確認
                        If SelectKjKoujiSiyouKakunin.SelectedValue = "1" Then '有
                            '4)工事会社請求
                            If CheckBoxKjKoujiKaisyaSeikyuu.Checked = False Then '未チェック
                                '請求書発行日
                                TextKjSeikyuusyoHakkouDate.Text = String.Empty

                                '売上年月日
                                If TextKjUriageNengappi.Text = "" Then '空白
                                    TextKjUriageNengappi.Text = Date.Now.ToString(EarthConst.FORMAT_DATE_TIME_9)
                                End If
                            End If
                        End If

                    End If

                    '完工速報着日
                    If Me.TextKjKankouSokuhouTyakuDate.Text = String.Empty Then
                        '6)工事仕様確認
                        If SelectKjKoujiSiyouKakunin.SelectedValue = "" Then '空白
                            '請求書発行日
                            TextKjSeikyuusyoHakkouDate.Text = String.Empty

                            '売上年月日
                            TextKjUriageNengappi.Text = String.Empty

                        End If

                        '7)工事会社請求
                        If Me.CheckBoxKjKoujiKaisyaSeikyuu.Checked Then
                            '請求書発行日
                            TextKjSeikyuusyoHakkouDate.Text = String.Empty

                            '売上年月日
                            TextKjUriageNengappi.Text = String.Empty

                        End If
                    End If

                Else '2000番台以外

                    '直工事商品チェック
                    If cl.ChkSyouhinCdB2000(Me.SelectKjSyouhinCd.SelectedValue) = False Then
                        '5)完工速報着日=未入力
                        If TextKjKankouSokuhouTyakuDate.Text = String.Empty Then '未入力
                            TextKjSeikyuusyoHakkouDate.Text = "" '請求書発行日
                            TextKjUriageNengappi.Text = "" '売上年月日
                        End If
                    End If

                End If

            Else '未入力

                SelectKjSeikyuuUmu.SelectedValue = "" '請求
                TextKjUriageNengappi.Text = "" '売上年月日
                TextKjUriageZeinukiKingaku.Text = "" '売上金額/税抜金額
                TextKjUriageSyouhizei.Text = "" '売上金額/消費税
                TextKjUriageZeikomiKingaku.Text = "" '売上金額/税込金額
                TextKjSiireZeinukiKingaku.Text = "" '仕入金額/税抜金額
                TextKjSiireSyouhizei.Text = "" '仕入金額/消費税
                TextKjSiireZeikomiKingaku.Text = "" '仕入金額/税込金額
                TextKjZangaku.Text = "0" '残額
                TextKjSeikyuusyoHakkouDate.Text = "" '請求書発行日
                TextKjHattyuusyoKakutei.Text = "" '発注書確定
            End If

        End If

    End Sub

    ''' <summary>
    '''商品毎の請求・仕入先が変更されていないかをチェックし、
    '''変更されている場合情報の再取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setSeikyuuSiireHenkou(ByVal sender As Object, ByVal e As System.EventArgs)
        '請求先、仕入先が変更された行をチェックし、存在した場合は
        '各行の請求有無変更時の処理を実行する

        '****************
        ' 改良工事
        '****************
        '請求仕入変更リンクUC内のチェックフラグHiddenを参照し、フラグが立っている場合は処理実施
        If Me.ucSeikyuuSiireLinkKai.AccHiddenChkSeikyuuSakiChg.Value = "1" Then

            '商品コード変更時処理
            Me.SelectKjSyouhinCd_SelectedIndexChanged(sender, e)

            'フラグ初期化
            Me.ucSeikyuuSiireLinkKai.AccHiddenChkSeikyuuSakiChg.Value = String.Empty

            'フォーカスは請求仕入変更リンク
            setFocusAJ(Me.ucSeikyuuSiireLinkKai.AccLinkSeikyuuSiireHenkou)

            '変更された商品が有った場合、UpdatePanelをUpdate
            Me.UpdatePanelKairyouKoujiInfo.Update()

        End If

        '****************
        ' 追加工事
        '****************
        '請求仕入変更リンクUC内のチェックフラグHiddenを参照し、フラグが立っている場合は処理実施
        If Me.ucSeikyuuSiireLinkTui.AccHiddenChkSeikyuuSakiChg.Value = "1" Then

            '商品コード変更時処理
            Me.SelectTjSyouhinCd_SelectedIndexChanged(sender, e)

            'フラグ初期化
            Me.ucSeikyuuSiireLinkTui.AccHiddenChkSeikyuuSakiChg.Value = String.Empty

            'フォーカスは請求仕入変更リンク
            setFocusAJ(Me.ucSeikyuuSiireLinkTui.AccLinkSeikyuuSiireHenkou)

            '変更された商品が有った場合、UpdatePanelをUpdate
            Me.UpdatePanelKairyouKoujiInfo.Update()

        End If

        '**************************
        ' 改良工事報告書
        '**************************
        '請求仕入変更リンクUC内のチェックフラグHiddenを参照し、フラグが立っている場合は処理実施
        If Me.ucSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then

            '請求有無変更時処理
            Me.SelectKhSeikyuuUmu_SelectedIndexChanged(sender, e)

            'フラグ初期化
            Me.ucSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty

            'フォーカスは請求仕入変更リンク
            setFocusAJ(Me.ucSeikyuuSiireLink.AccLinkSeikyuuSiireHenkou)

            '変更された商品が有った場合、UpdatePanelをUpdate
            Me.UpdatePanelKairyouKoujiHoukokusyoInfo.Update()

        End If

    End Sub
End Class