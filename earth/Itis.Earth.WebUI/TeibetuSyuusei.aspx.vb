
Partial Public Class TeibetuSyuusei
    Inherits System.Web.UI.Page
    ''' <summary>
    ''' 依頼内容ユーザーコントロール用ヘッダ
    ''' </summary>
    ''' <remarks></remarks>
    Protected USR_CTRL_IRAI_NAIYOU As String

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim user_info As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    Dim MLogic As New MessageLogic
    Dim jLogic As New JibanLogic
    Dim cbLogic As New CommonBizLogic
    Dim tLogic As New TokubetuTaiouLogic

#Region "コントロールタイプ"
    Enum CtrlTypes
        ''' <summary>
        ''' 商品明細コントロール
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinCtrl = 1
        ''' <summary>
        ''' 商品２コントロール
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin2Ctrl = 2
        ''' <summary>
        ''' 商品３コントロール
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin3Ctrl = 3
        ''' <summary>
        ''' 工事コントロール
        ''' </summary>
        ''' <remarks></remarks>
        KoujiCtrl = 4
    End Enum
#End Region

#Region "プロパティ"
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
    Public Property Kbn() As String
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
    Public Property No() As String
        Get
            Return _no
        End Get
        Set(ByVal value As String)
            _no = value
        End Set
    End Property
#End Region

#Region "イベント"
    ''' <summary>
    ''' ページロード時のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス

        '依頼内容ユーザーコントロール用ヘッダの取得
        USR_CTRL_IRAI_NAIYOU = Me.IraiNaiyou.ClientID & Me.ClientIDSeparator.ToString

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ユーザー基本認証
        jBn.UserAuth(user_info)

        '認証結果によって画面表示を切替える
        If user_info IsNot Nothing Then

        Else
            Response.Redirect(UrlConst.MAIN)
        End If

        If IsPostBack = False Then

            ' Key情報を保持
            _kbn = Request("kbn")
            _no = Request("no")

            ' パラメータ不足時は画面を表示しない
            If _kbn Is Nothing Or _
               _no Is Nothing Then
                Me.ButtonTouroku1.Style("display") = "none"
                Me.ButtonTouroku2.Style("display") = "none"
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            Else
                If jLogic.ExistsJibanData(_kbn, _no) = False Then
                    cl.CloseWindow(Me)
                    Me.ButtonTouroku1.Style("display") = "none"
                    Me.ButtonTouroku2.Style("display") = "none"
                    Response.Redirect(UrlConst.MAIN)
                    Exit Sub
                End If

            End If

            ' 非表示項目に設定
            HiddenKubun.Value = _kbn
            HiddenBangou.Value = _no

            '請求有無と売上金額の整合性チェックフラグ
            HiddenSeikyuuUmuCheck.Value = String.Empty
            '変更箇所チェックフラグ
            HiddenChgValChk.Value = String.Empty

            ' 地盤データを画面に設定する
            SetJibanData()

            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            setDispAction()

        Else
            '商品毎の請求・仕入先が変更されていないかをチェックし、
            '変更されている場合情報の再取得
            setSeikyuuSiireHenkou(sender, e)
        End If

    End Sub

    ''' <summary>
    ''' 登録/修正 実行ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTourokuExe_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTourokuExe.ServerClick

        ' 入力チェック
        If Not CheckInput() Then
            Exit Sub
        End If

        ' 画面の内容をDBに反映する
        SaveData()

    End Sub

#Region "税込金額変更時のイベント群"
    ''' <summary>
    ''' 商品１税込金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin1(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin1Record01.ChangeZeikomiGaku

        ' 商品２の税込金額を取得
        Dim syouhin2Zeikomi As Integer = CtrlTeibetuSyouhin2.ZeikomiKingaku

        ' 解約払戻しの税込金額を取得
        Dim kaiyakuZeikomi As Integer = HosyouKaiyaku.ZeikomiKingaku

        ' 商品１の残額再設定
        NyuukinZangakuCtrlSyouhin1.CalcZangaku(zeikomigaku + syouhin2Zeikomi + kaiyakuZeikomi)
        NyuukinZangakuCtrlSyouhin1.UpdateZangakuPanel.Update()
    End Sub

    ''' <summary>
    ''' 商品２税込金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin2(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles CtrlTeibetuSyouhin2.ChangeZeikomiGaku

        ' 商品１の税込金額を取得
        Dim syouhin1Zeikomi As Integer = Syouhin1Record01.ZeikomiKingaku

        ' 解約払戻しの税込金額を取得
        Dim kaiyakuZeikomi As Integer = HosyouKaiyaku.ZeikomiKingaku

        ' 商品１の残額再設定
        NyuukinZangakuCtrlSyouhin1.CalcZangaku(zeikomigaku + syouhin1Zeikomi + kaiyakuZeikomi)
        NyuukinZangakuCtrlSyouhin1.UpdateZangakuPanel.Update()

    End Sub

    ''' <summary>
    ''' 解約払戻税込金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiKaiyaku(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles HosyouKaiyaku.ChangeZeikomiGaku

        ' 商品１の税込金額を取得
        Dim syouhin1Zeikomi As Integer = Syouhin1Record01.ZeikomiKingaku

        ' 商品２の税込金額を取得
        Dim syouhin2Zeikomi As Integer = CtrlTeibetuSyouhin2.ZeikomiKingaku

        ' 商品１の残額再設定
        NyuukinZangakuCtrlSyouhin1.CalcZangaku(zeikomigaku + syouhin1Zeikomi + syouhin2Zeikomi)
        NyuukinZangakuCtrlSyouhin1.UpdateZangakuPanel.Update()

    End Sub

    ''' <summary>
    ''' 商品３税込金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin3(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles CtrlTeibetuSyouhin3.ChangeZeikomiGaku

        ' 商品３の残額再設定
        NyuukinZangakuCtrlSyouhin3.CalcZangaku(zeikomigaku)
        NyuukinZangakuCtrlSyouhin3.UpdateZangakuPanel.Update()

    End Sub

    ''' <summary>
    ''' 改良工事税込金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiKairyouKouji(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Kairyoukouji.ChangeZeikomiGaku

        ' 改良工事の残額再設定
        Kairyoukouji.ZanGaku.CalcZangaku(zeikomigaku)
        Kairyoukouji.ZanGaku.UpdateZangakuPanel.Update()
    End Sub

    ''' <summary>
    ''' 追加工事税込金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiTuikaKouji(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Tuikakouji.ChangeZeikomiGaku

        ' 追加工事の残額再設定
        Tuikakouji.ZanGaku.CalcZangaku(zeikomigaku)
        Tuikakouji.ZanGaku.UpdateZangakuPanel.Update()
    End Sub

    ''' <summary>
    ''' 調査報告書税込金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiTyousaHoukokusyo(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles HoukokusyoTyousa.ChangeZeikomiGaku

        ' 調査報告書の残額再設定
        NyuukinZangakuCtrlHoukokusyoTyousa.CalcZangaku(zeikomigaku)
        NyuukinZangakuCtrlHoukokusyoTyousa.UpdateZangakuPanel.Update()
    End Sub

    ''' <summary>
    ''' 工事報告書税込金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiKoujiHoukokusyo(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles HoukokusyoKouji.ChangeZeikomiGaku

        ' 工事報告書の残額再設定
        NyuukinZangakuCtrlHoukokusyoKouji.CalcZangaku(zeikomigaku)
        NyuukinZangakuCtrlHoukokusyoKouji.UpdateZangakuPanel.Update()
    End Sub

    ''' <summary>
    ''' 保証書税込金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiKoujiHosyousyo(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles HosyouHosyousyo.ChangeZeikomiGaku

        ' 保証書の残額再設定
        NyuukinZangakuCtrlHosyou.CalcZangaku(zeikomigaku)
        NyuukinZangakuCtrlHosyou.UpdateZangakuPanel.Update()
    End Sub

    ''' <summary>
    ''' 依頼コントロールより投げられる発注金額要求イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GetSyouhin1HattyuuKingaku(ByVal sender As System.Object, _
                                          ByVal e As System.EventArgs) Handles IraiNaiyou.GetHattyuuKingaku

        ' 商品１の発注書金額を依頼コントロールにトスする
        IraiNaiyou.Syouhin1HattyuuKingaku = Syouhin1Record01.HattyuusyoKingaku
    End Sub

#End Region

    ''' <summary>
    ''' ページロードコンプリート
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        '工事商品については、依頼内容ユーザーコントロールのロード時では商品が取得できない為、
        'このタイミングでセットする
        SetSeikyuuSiireInfo(Me.Kairyoukouji.ClientID)
        SetSeikyuuSiireInfo(Me.Tuikakouji.ClientID)

        '特別対応ボタン
        ChgDispTokubetuTaiou()
        Me.UpdatePanelTokubetuTaiou.Update()

    End Sub

#End Region

#Region "プライベートメソッド"
    ''' <summary>
    ''' 地盤データを画面に設定する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJibanData()

        Dim logic As New JibanLogic
        Dim teibetuLogic As New TeibetuSyuuseiLogic
        Dim record As New JibanRecord
        Dim tmpSyouhin1Cd As String = String.Empty

        ' 再読み込み用
        If _kbn = "" Or _kbn Is Nothing Then
            _kbn = HiddenKubun.Value
        End If
        If _no = "" Or _no Is Nothing Then
            _no = HiddenBangou.Value
        End If

        ' 地盤データを取得する
        record = logic.GetJibanData(_kbn, _no)

        ' 非表示項目の設定
        '地盤テーブル.更新者からログインユーザID、更新日時を取得
        cl.SetKousinsya(record.Kousinsya, TextSaisyuuKousinsya.Value, TextSaisyuuKousinDateTime.Value)
        '更新日時
        HiddenUpdDatetime.Value = IIf(record.UpdDatetime = Date.MinValue, _
                                      record.AddDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2), _
                                      record.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2))

        ' 共通情報コントロールへデータを設定
        Kyoutuu.Kubun = record.Kbn
        Kyoutuu.Bangou = record.HosyousyoNo
        Kyoutuu.Bikou1 = record.Bikou
        Kyoutuu.Bikou2 = record.Bikou2
        Kyoutuu.Sesyumei = record.SesyuMei
        Kyoutuu.Jyuusyo1 = record.BukkenJyuusyo1
        Kyoutuu.Jyuusyo2 = record.BukkenJyuusyo2
        Kyoutuu.Jyuusyo3 = record.BukkenJyuusyo3
        Kyoutuu.TyousaJissibi = record.TysJissiDate.ToString(EarthConst.FORMAT_DATE_TIME_2)
        Kyoutuu.KairyouKoujiJissibi = record.KairyKojDate
        Kyoutuu.KairyouKoujiKankou = record.KairyKojSokuhouTykDate
        Kyoutuu.TuikaKoujiJissibi = record.TKojDate
        Kyoutuu.TuikaKoujiKankou = record.TKojSokuhouTykDate
        Kyoutuu.KaisekiTantouCd = record.TantousyaCd
        Kyoutuu.KaisekiTantouMei = record.TantousyaMei
        Kyoutuu.KoujiTantouCd = record.SyouninsyaCd
        Kyoutuu.KoujiTantouMei = record.SyouninsyaMei
        Kyoutuu.HanteiCd1 = record.HanteiCd1
        Kyoutuu.HanteiCd2 = record.HanteiCd2
        Kyoutuu.HanteiSetuzokuMoji = record.HanteiSetuzokuMoji
        ' 経理業務権限を設定
        Kyoutuu.KeiriGyoumuKengen = user_info.KeiriGyoumuKengen

        ' 依頼内容コントロールへデータを設定
        IraiNaiyou.Kbn = record.Kbn
        IraiNaiyou.KameitenCd = record.KameitenCd
        IraiNaiyou.TysKaisyaCd = record.TysKaisyaCd
        IraiNaiyou.TysKaisyaJigyousyoCd = record.TysKaisyaJigyousyoCd
        IraiNaiyou.DoujiIraiTousuu = record.DoujiIraiTousuu
        IraiNaiyou.TatemonoYoutoNo = record.TatemonoYoutoNo
        IraiNaiyou.TysHouhou = cl.GetDisplayString(record.TysHouhou)
        IraiNaiyou.TysGaiyou = IIf(record.TysGaiyou = 0, 9, record.TysGaiyou)
        IraiNaiyou.SyouhinKbn = record.SyouhinKbn
        ' 経理業務権限を設定
        IraiNaiyou.KeiriGyoumuKengen = user_info.KeiriGyoumuKengen
        'ReportIF進捗ステータス
        IraiNaiyou.StatusIfCd = record.StatusIf
        If EarthConst.Instance.IF_STATUS.ContainsKey(IraiNaiyou.StatusIfCd) Then
            IraiNaiyou.StatusIfName = EarthConst.Instance.IF_STATUS(IraiNaiyou.StatusIfCd)
        Else
            IraiNaiyou.StatusIfName = IraiNaiyou.StatusIfCd
        End If

        '商品1レコードがある場合のみ設定
        If record.Syouhin1Record IsNot Nothing Then

            '依頼内容・商品1(DDLを設定)
            IraiNaiyou.Syouhin1 = record.Syouhin1Record.SyouhinCd
            IraiNaiyou.Syouhin1Mei = record.Syouhin1Record.SyouhinMei

            '特別対応ボタン用商品1
            tmpSyouhin1Cd = cl.GetDisplayString(record.Syouhin1Record.SyouhinCd)

        End If

        '○特別対応ボタン用にデフォルトの加盟店・商品コード・調査方法をセット
        If String.IsNullOrEmpty(Me.HiddenKakuteiValuesTokubetu.Value) Then
            Me.HiddenKakuteiValuesTokubetu.Value = cl.GetDisplayString(record.KameitenCd) & EarthConst.SEP_STRING _
                                                 & tmpSyouhin1Cd & EarthConst.SEP_STRING _
                                                 & cl.GetDisplayString(record.TysHouhou) & EarthConst.SEP_STRING
        End If

        Dim settingInfoRec As New TeibetuSettingInfoRecord
        settingInfoRec.Kubun = record.Kbn
        settingInfoRec.Bangou = record.HosyousyoNo
        settingInfoRec.UpdLoginUserId = user_info.LoginUserId
        settingInfoRec.KeiriGyoumuKengen = _
            IIf(user_info.KeiriGyoumuKengen = Integer.MinValue, 0, user_info.KeiriGyoumuKengen)
        settingInfoRec.HattyuusyoKanriKengen = _
            IIf(user_info.HattyuusyoKanriKengen = Integer.MinValue, 0, user_info.HattyuusyoKanriKengen)

        ' 解約払戻し返金フラグ
        If record.HenkinSyoriFlg = 1 Then
            LabelKaiyakuMessage.Text = EarthConst.HENKIN_ZUMI
        End If

        '*******************************************************************
        ' 商品１
        '*******************************************************************
        SetTeibetuRec(CtrlTypes.SyouhinCtrl, _
                      Syouhin1Record01, _
                      record.Syouhin1Record, _
                      settingInfoRec, _
                      True, _
                      False, _
                      "100", _
                      NyuukinZangakuCtrlSyouhin1, _
                      record.getZeikomiGaku(New String() {"100", "110", "180"}), _
                      record.getNyuukinGaku("100"))

        '*******************************************************************
        ' 商品２
        '*******************************************************************
        ' 共通設定情報
        CtrlTeibetuSyouhin2.SettingInfo = settingInfoRec

        ' 商品２レコードへデータを設定
        CtrlTeibetuSyouhin2.Syouhin2Records = record.Syouhin2Records

        '*******************************************************************
        ' 商品３
        '*******************************************************************
        ' 共通設定情報
        CtrlTeibetuSyouhin3.SettingInfo = settingInfoRec

        ' 商品３レコードへデータを設定
        CtrlTeibetuSyouhin3.Syouhin3Records = record.Syouhin3Records

        ' 残額をセット
        NyuukinZangakuCtrlSyouhin3.CalcZangaku(record.getZeikomiGaku(New String() {"120"}), _
                                               record.getNyuukinGaku("120"))

        '*******************************************************************
        ' 商品１～３ 特別対応ツールチップ
        '*******************************************************************
        Me.GetTokubetuTaiouCd()

        '*******************************************************************
        ' 改良工事
        '*******************************************************************
        SetTeibetuRec(CtrlTypes.KoujiCtrl, _
                      Kairyoukouji, _
                      record.KairyouKoujiRecord, _
                      settingInfoRec, _
                      False, _
                      False, _
                      "130", _
                      Kairyoukouji.ZanGaku, _
                      record.getZeikomiGaku(New String() {"130"}), _
                      record.getNyuukinGaku("130"))

        ' 加盟店コード
        Kairyoukouji.KameitenCd = record.KameitenCd
        ' 工事会社請求有無
        Kairyoukouji.KoujiKaisyaSeikyuuUmu = record.KojGaisyaSeikyuuUmu

        ' 工事会社コード
        If record.KojGaisyaCd Is Nothing Then
            Kairyoukouji.KoujiKaisyaCd = ""
        Else
            Kairyoukouji.KoujiKaisyaCd = record.KojGaisyaCd
        End If

        ' 工事会社事業所コード
        If record.KojGaisyaJigyousyoCd Is Nothing Then
            Kairyoukouji.KoujiKaisyaJigyousyoCd = ""
        Else
            Kairyoukouji.KoujiKaisyaJigyousyoCd = record.KojGaisyaJigyousyoCd
        End If

        '*******************************************************************
        ' 追加工事
        '*******************************************************************
        SetTeibetuRec(CtrlTypes.KoujiCtrl, _
                      Tuikakouji, _
                      record.TuikaKoujiRecord, _
                      settingInfoRec, _
                      False, _
                      False, _
                      "140", _
                      Tuikakouji.ZanGaku, _
                      record.getZeikomiGaku(New String() {"140"}), _
                      record.getNyuukinGaku("140"))

        ' 加盟店コード
        Tuikakouji.KameitenCd = record.KameitenCd
        ' 追加工事会社請求有無
        Tuikakouji.KoujiKaisyaSeikyuuUmu = record.TKojKaisyaSeikyuuUmu

        ' 追加工事会社コード
        If record.TKojKaisyaCd Is Nothing Then
            Tuikakouji.KoujiKaisyaCd = ""
        Else
            Tuikakouji.KoujiKaisyaCd = record.TKojKaisyaCd
        End If

        ' 追加工事会社事業所コード
        If record.TKojKaisyaJigyousyoCd Is Nothing Then
            Tuikakouji.KoujiKaisyaJigyousyoCd = ""
        Else
            Tuikakouji.KoujiKaisyaJigyousyoCd = record.TKojKaisyaJigyousyoCd
        End If

        '*******************************************************************
        ' 調査報告書
        '*******************************************************************
        SetTeibetuRec(CtrlTypes.SyouhinCtrl, _
                      HoukokusyoTyousa, _
                      record.TyousaHoukokusyoRecord, _
                      settingInfoRec, _
                      True, _
                      True, _
                      "150", _
                      NyuukinZangakuCtrlHoukokusyoTyousa, _
                      record.getZeikomiGaku(New String() {"150"}), _
                      record.getNyuukinGaku("150"))

        '*******************************************************************
        ' 工事報告書
        '*******************************************************************
        SetTeibetuRec(CtrlTypes.SyouhinCtrl, _
                      HoukokusyoKouji, _
                      record.KoujiHoukokusyoRecord, _
                      settingInfoRec, _
                      True, _
                      True, _
                      "160", _
                      NyuukinZangakuCtrlHoukokusyoKouji, _
                      record.getZeikomiGaku(New String() {"160"}), _
                      record.getNyuukinGaku("160"))

        '*******************************************************************
        ' 保証書
        '*******************************************************************
        SetTeibetuRec(CtrlTypes.SyouhinCtrl, _
                      HosyouHosyousyo, _
                      record.HosyousyoRecord, _
                      settingInfoRec, _
                      True, _
                      True, _
                      "170", _
                      NyuukinZangakuCtrlHosyou, _
                      record.getZeikomiGaku(New String() {"170"}), _
                      record.getNyuukinGaku("170"))

        '*******************************************************************
        ' 解約払戻
        '*******************************************************************
        SetTeibetuRec(CtrlTypes.SyouhinCtrl, _
                      HosyouKaiyaku, _
                      record.KaiyakuHaraimodosiRecord, _
                      settingInfoRec, _
                      True, _
                      True, _
                      "180")

        '********************************************************************************
        ' 売上データ存在チェック用 売上データテーブルに有効な伝票データを持たないリスト
        '********************************************************************************
        Dim denpyouNGList As String
        denpyouNGList = teibetuLogic.GetTeibetuSeikyuuDenpyouHakkouZumiUriageData(record.Kbn, record.HosyousyoNo)
        HiddenDenpyouNGList.Value = denpyouNGList

    End Sub

    ''' <summary>
    ''' 邸別データを各明細コントロールに設定します
    ''' </summary>
    ''' <param name="ctrlType">コントロール種別</param>
    ''' <param name="ctrl">設定対象の各種明細コントロール</param>
    ''' <param name="record">地盤レコード</param>
    ''' <param name="settingInfoRec">共通設定情報</param>
    ''' <param name="recEnabled">レコード無しの場合にコントロールを非活性化する場合true</param>
    ''' <param name="seikyuuUmu">請求有無を非活性制御より除外</param>
    ''' <param name="bunruiCd">分類コード（報告書・保証書）</param>
    ''' <param name="zangakuCtrl">残額設定用コントロール（設定時のみ指定）</param>
    ''' <param name="zeikomigaku">残額計算用の税込金額</param>
    ''' <param name="nyuukingaku">残額計算用の入金額</param>
    ''' <remarks></remarks>
    Private Sub SetTeibetuRec(ByVal ctrlType As CtrlTypes, _
                              ByVal ctrl As Object, _
                              ByVal record As TeibetuSeikyuuRecord, _
                              ByVal settingInfoRec As TeibetuSettingInfoRecord, _
                              Optional ByVal recEnabled As Boolean = False, _
                              Optional ByVal seikyuuUmu As Boolean = False, _
                              Optional ByVal bunruiCd As String = "", _
                              Optional ByVal zangakuCtrl As NyuukinZangakuCtrl = Nothing, _
                              Optional ByVal zeikomigaku As Integer = Integer.MinValue, _
                              Optional ByVal nyuukingaku As Integer = Integer.MinValue)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetTeibetuRec", _
                                                    ctrlType, _
                                                    ctrl, _
                                                    record, _
                                                    settingInfoRec, _
                                                    recEnabled, _
                                                    seikyuuUmu, _
                                                    bunruiCd, _
                                                    zangakuCtrl, _
                                                    zeikomigaku, _
                                                    nyuukingaku)

        ' コントロールの種類別に設定を行う
        Select Case ctrlType
            Case CtrlTypes.SyouhinCtrl

                ' 商品レコードインスタンスに設定
                Dim syouhinCtrl As TeibetuSyouhinRecordCtrl = ctrl

                ' 共通設定情報
                syouhinCtrl.SettingInfo = settingInfoRec

                ' データ無しの場合にコントロールを非活性化する場合
                If recEnabled = True Then
                    If record Is Nothing Then
                        syouhinCtrl.Enabled = False

                        If seikyuuUmu = True Then
                            ' 請求有無を活性化
                            syouhinCtrl.EnableSeikyuuUmu = True
                            ' 報告書・保証書用に分類コードを設定
                            syouhinCtrl.BunruiCd = bunruiCd
                        End If
                    End If
                End If

                ' レコードへデータを設定
                syouhinCtrl.TeibetuSeikyuuRec = record

            Case CtrlTypes.KoujiCtrl

                ' 工事コントロールインスタンスに設定
                Dim koujiCtrl As TeibetuKoujiRecordCtrl = ctrl

                ' 共通設定情報
                koujiCtrl.SettingInfo = settingInfoRec

                ' 工事レコードへデータを設定
                koujiCtrl.TeibetuSeikyuuRec = record

        End Select

        If Not zangakuCtrl Is Nothing Then
            ' 残額をセット
            zangakuCtrl.CalcZangaku(zeikomigaku, nyuukingaku)
        End If

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        '登録/修正実行ボタン押下時のイベントハンドラ
        Dim tmpScript = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this);objEBI('" & ButtonTourokuExe.ClientID & "').click();}else{return false;}"
        ButtonTouroku1.Attributes("onclick") = tmpScript
        ButtonTouroku2.Attributes("onclick") = tmpScript

        '商品4
        cl.getSyouhin4MasterPath(ButtonSyouhin4, _
                                 user_info, _
                                 Me.Kyoutuu.AccKubun.ClientID, _
                                 Me.Kyoutuu.AccBangou.ClientID, _
                                 Me.IraiNaiyou.KameitenCdBox.ClientID, _
                                 Me.IraiNaiyou.TyousaKaishaCdBox.ClientID)

        '特別対応
        cl.getTokubetuTaiouLinkPathJT(Me.ButtonTokubetuTaiou, _
                                    user_info, _
                                    Me.Kyoutuu.AccKubun.ClientID, _
                                    Me.Kyoutuu.AccBangou.ClientID, _
                                    Me.IraiNaiyou.KameitenCdBox.ClientID, _
                                    Me.IraiNaiyou.AccTysHouhou.ClientID, _
                                    Me.IraiNaiyou.AccSelectSyouhin1.ClientID, _
                                    Me.HiddenKakuteiValuesTokubetu.ClientID, _
                                    Me.ButtonTokubetuTaiou.ClientID, _
                                    EarthEnum.emTokubetuTaiouSearchType.TeibetuSyuusei)

    End Sub

    ''' <summary>
    ''' 依頼コントロールで取得した商品１自動設定情報を商品１コントロールに反映する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="syouhinRec"></param>
    ''' <remarks></remarks>
    Private Sub SetSyouhin1Data(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs, _
                                ByVal syouhinRec As Syouhin1AutoSetRecord) Handles IraiNaiyou.Syouhin1SetAction

        Syouhin1Record01.AutoSetSyouhinRecord = syouhinRec ' 商品レコード
        Syouhin1Record01.UpdateSyouhinPanel.Update()

    End Sub

    ''' <summary>
    ''' ユーザーコントロールで設定した請求先・仕入先情報を画面の隠し項目に反映する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">画面ID</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuSiireInfo(ByVal sender As System.Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal strId As String) Handles IraiNaiyou.SetSeikyuuSiireSakiAction _
                                                                    , Syouhin1Record01.SetSeikyuuSiireSakiAction _
                                                                    , CtrlTeibetuSyouhin2.SetSeikyuuSiireSakiAction _
                                                                    , CtrlTeibetuSyouhin3.SetSeikyuuSiireSakiAction _
                                                                    , Kairyoukouji.SetSeikyuuSiireSakiAction _
                                                                    , Tuikakouji.SetSeikyuuSiireSakiAction _
                                                                    , HoukokusyoTyousa.SetSeikyuuSiireSakiAction _
                                                                    , HoukokusyoKouji.SetSeikyuuSiireSakiAction _
                                                                    , HosyouHosyousyo.SetSeikyuuSiireSakiAction _
                                                                    , HosyouKaiyaku.SetSeikyuuSiireSakiAction
        SetSeikyuuSiireInfo(strId)
    End Sub

    ''' <summary>
    ''' ユーザーコントロールで設定した工事価格マスタ情報を画面の隠し項目に反映する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">画面ID</param>
    ''' <remarks></remarks>
    Private Sub GetKojMInfoAction(ByVal sender As System.Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal strId As String) Handles IraiNaiyou.GetKojMInfoAction _
                                                                    , Kairyoukouji.GetKojMInfoAction _
                                                                    , Tuikakouji.GetKojMInfoAction

        Me.SetKojMInfo(strId)
    End Sub

    ''' <summary>
    ''' 画面の隠し項目を使い、工事価格を取得する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">画面ID</param>
    ''' <remarks></remarks>
    Private Sub SetKojMInfoAction(ByVal sender As System.Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal strId As String) Handles IraiNaiyou.SetKojMInfoAction

        Me.SetKojKakaku(strId)
    End Sub

    ''' <summary>
    ''' 依頼・商品コントロールから原価・販売価格マスタへのチェックを行う（子用）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId"></param>
    ''' <remarks></remarks>
    Public Sub CheckGenkaHanbaiKkkMaster(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs, _
                                 ByVal strId As String) Handles IraiNaiyou.CheckGenkaHanbaiKkkMasterAction

        If IsPostBack = True Then
            Dim lgcJiban As New JibanLogic
            Dim strAlertMes As String = String.Empty
            Dim blnGetGenkaFlg As Boolean = False
            Dim blnGetHanbaiFlg As Boolean = False
            Dim hin1InfoRec As New Syouhin1InfoRecord
            Dim hin1AutoSetRecord As New Syouhin1AutoSetRecord

            '商品1が無い場合はチェックしない
            If Syouhin1Record01.TeibetuSeikyuuRec Is Nothing Then
                Exit Sub
            End If

            '原価マスタへの取得
            blnGetGenkaFlg = lgcJiban.GetSyoudakusyoKingaku1(Syouhin1Record01.TeibetuSeikyuuRec.SyouhinCd, _
                                                             Kyoutuu.Kubun, _
                                                             IraiNaiyou.TysHouhou, _
                                                             IraiNaiyou.TysGaiyou, _
                                                             IraiNaiyou.DoujiIraiTousuu, _
                                                             IraiNaiyou.TyousaKaishaCdBox.Value, _
                                                             IraiNaiyou.KameitenCd, _
                                                             0, _
                                                             IraiNaiyou.KeiretuCd, _
                                                             False)

            '画面項目の設定
            hin1InfoRec.SyouhinCd = Syouhin1Record01.TeibetuSeikyuuRec.SyouhinCd
            hin1InfoRec.TysKaisyaCd = IraiNaiyou.TyousaKaishaCdBox.Value
            hin1InfoRec.TyousaHouhouNo = IraiNaiyou.TysHouhou
            hin1InfoRec.KameitenCd = IraiNaiyou.KameitenCd
            hin1InfoRec.EigyousyoCd = IraiNaiyou.EigyousyoCd
            hin1InfoRec.KeiretuCd = IraiNaiyou.KeiretuCd

            '販売価格マスタへの取得
            blnGetHanbaiFlg = lgcJiban.GetHanbaiKingaku1(hin1InfoRec, hin1AutoSetRecord)

            'マスタ値取得不可のパターンによって表示内容を切り替える
            If blnGetGenkaFlg = False And blnGetHanbaiFlg = False Then
                strAlertMes += Messages.MSG180E
                strAlertMes += Messages.MSG182E
            ElseIf blnGetGenkaFlg = False Then
                strAlertMes += Messages.MSG180E
            ElseIf blnGetHanbaiFlg = False Then
                strAlertMes += Messages.MSG182E
            End If
            '調査会社コードが設定済みの場合のみ
            If IraiNaiyou.TyousaKaishaCdBox.Value <> String.Empty AndAlso strAlertMes <> String.Empty Then
                'メッセージ表示
                MLogic.AlertMessage(sender, strAlertMes, 0, "GetKakakuError")
                '調査会社を元に戻す
                IraiNaiyou.ReturnTyousakaisyaCdNm(sender, e)
            End If

        End If

    End Sub

    ''' <summary>
    ''' 依頼コントロールで設定した工事価格マスタ情報を画面の隠し項目に反映する
    ''' </summary>
    ''' <param name="strId">画面ID</param>
    ''' <remarks></remarks>
    Private Sub SetKojMInfo(Optional ByVal strId As String = "")
        '工事価格マスタ取得用の画面情報
        Dim keyRec As KoujiKakakuKeyRecord = New KoujiKakakuKeyRecord
        With keyRec
            .KameitenCd = Me.IraiNaiyou.KameitenCd
            .KeiretuCd = Me.IraiNaiyou.KeiretuCd
            .EigyousyoCd = Me.IraiNaiyou.EigyousyoCd
        End With

        Select Case True
            Case strId.IndexOf(Me.IraiNaiyou.ClientID) >= 0 '依頼内容CTRL変更時
                '改良工事
                Me.Kairyoukouji.SetKojKkkMstInfo(keyRec)

                '追加工事
                Me.Tuikakouji.SetKojKkkMstInfo(keyRec)

            Case strId.IndexOf(Me.Kairyoukouji.ClientID) >= 0 '改良工事CTRL変更時
                '改良工事
                Me.Kairyoukouji.SetKojKkkMstInfo(keyRec)

            Case strId.IndexOf(Me.Tuikakouji.ClientID) >= 0 '追加工事CTRL変更時
                '追加工事
                Me.Tuikakouji.SetKojKkkMstInfo(keyRec)

        End Select

    End Sub

    ''' <summary>
    ''' 邸別工事レコードコントロールの工事価格取得処理を呼び出す
    ''' </summary>
    ''' <param name="strId">画面ID</param>
    ''' <remarks></remarks>
    Private Sub SetKojKakaku(Optional ByVal strId As String = "")

        If strId.IndexOf(Me.IraiNaiyou.ClientID) >= 0 Then '依頼内容CTRL変更時
            '改良工事
            If Kairyoukouji.SetSyouhinInfoFromKojM(EarthEnum.emKojKkkActionType.KameitenCd) = False Then
                '直工事以外の場合は価格取得を行なわない
            End If

            '追加工事
            If Tuikakouji.SetSyouhinInfoFromKojM(EarthEnum.emKojKkkActionType.KameitenCd) = False Then
                '直工事以外の場合は価格取得を行なわない
            End If

        End If

    End Sub

    ''' <summary>
    ''' 依頼コントロールで設定した請求先・仕入先情報を画面の隠し項目に反映する
    ''' </summary>
    ''' <param name="strId">画面ID</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuSiireInfo(Optional ByVal strId As String = "")
        '請求先・仕入先変更画面用情報
        Dim strKameitenCd As String = Me.IraiNaiyou.KameitenCd
        Dim strKeiretuCd As String = Me.IraiNaiyou.KeiretuCd
        Dim strTysKaisyaCd As String = Me.IraiNaiyou.TyousaKaishaCdBox.Value

        Select Case True
            Case strId.IndexOf(Me.IraiNaiyou.ClientID) >= 0, strId = String.Empty
                '依頼内容変更時
                '***** 全商品のレコードへ加盟店CDと調査会社CDの情報をセット *****
                '商品１
                Me.Syouhin1Record01.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
                '商品２
                Me.CtrlTeibetuSyouhin2.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
                '商品３
                Me.CtrlTeibetuSyouhin3.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
                '改良工事
                Me.Kairyoukouji.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd)
                '追加工事
                Me.Tuikakouji.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd)
                '報告書(調査)
                Me.HoukokusyoTyousa.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
                '報告書(工事)
                Me.HoukokusyoKouji.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
                '保証
                Me.HosyouHosyousyo.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
                '解約払戻
                Me.HosyouKaiyaku.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)

            Case strId.IndexOf(Me.Syouhin1Record01.ClientID) >= 0
                '商品1変更時
                Me.Syouhin1Record01.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)

            Case strId.IndexOf(Me.CtrlTeibetuSyouhin2.ClientID) >= 0
                '商品2変更時
                Me.CtrlTeibetuSyouhin2.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd, strId)

            Case strId.IndexOf(Me.CtrlTeibetuSyouhin3.ClientID) >= 0
                '商品3変更時
                Me.CtrlTeibetuSyouhin3.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd, strId)

            Case strId.IndexOf(Me.Kairyoukouji.ClientID) >= 0
                '改良工事変更時
                Me.Kairyoukouji.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd)

            Case strId.IndexOf(Me.Tuikakouji.ClientID) >= 0
                '追加工事変更時
                Me.Tuikakouji.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd)

            Case strId.IndexOf(Me.HoukokusyoTyousa.ClientID) >= 0
                '報告書(調査)変更時
                Me.HoukokusyoTyousa.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)

            Case strId.IndexOf(Me.HoukokusyoKouji.ClientID) >= 0
                '報告書(工事)変更時
                Me.HoukokusyoKouji.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)

            Case strId.IndexOf(Me.HosyouHosyousyo.ClientID) >= 0
                '保証変更時
                Me.HosyouHosyousyo.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)

            Case strId.IndexOf(Me.HosyouKaiyaku.ClientID) >= 0
                '解約払戻変更時
                Me.HosyouKaiyaku.SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
        End Select


    End Sub

    ''' <summary>
    ''' 商品1の請求先情報取得処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GetSeikyuuSakiInfoSyouhin1(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) Handles IraiNaiyou.GetSyouhin1SeikyuuSakiInfo
        Dim strSeikyuuSakiCd As String
        Dim strSeikyuuSakibrc As String
        Dim strSeikyuuSakikbn As String

        strSeikyuuSakiCd = Syouhin1Record01.AccSeikyuuSiireLink.AccSeikyuuSakiCd.Value
        strSeikyuuSakibrc = Syouhin1Record01.AccSeikyuuSiireLink.AccSeikyuuSakiBrc.Value
        strSeikyuuSakikbn = Syouhin1Record01.AccSeikyuuSiireLink.AccSeikyuuSakiKbn.Value

        IraiNaiyou.Syouhin1SeikyuuSakiCd = strSeikyuuSakiCd
        IraiNaiyou.Syouhin1SeikyuuSakiBrc = strSeikyuuSakibrc
        IraiNaiyou.Syouhin1SeikyuuSakiKbn = strSeikyuuSakikbn

    End Sub

    ''' <summary>
    ''' 請求タイプの設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">カスタムイベント発生元ID</param>
    ''' <param name="strSeikyuuSakiTypeStr">請求先タイプ(直接請求/他請求)</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuType(ByVal sender As System.Object _
                                        , ByVal e As System.EventArgs _
                                        , ByVal strId As String _
                                        , ByVal strSeikyuuSakiTypeStr As String _
                                        , ByVal strKeiretuCd As String _
                                        , ByVal strKameitenCd As String) Handles IraiNaiyou.SetSeikyuuTypeAction _
                                                                                , Syouhin1Record01.SetSeikyuuTypeAction _
                                                                                , CtrlTeibetuSyouhin2.SetSeikyuuTypeAction _
                                                                                , CtrlTeibetuSyouhin3.SetSeikyuuTypeAction _
                                                                                , HoukokusyoTyousa.SetSeikyuuTypeAction _
                                                                                , HoukokusyoKouji.SetSeikyuuTypeAction _
                                                                                , HosyouHosyousyo.SetSeikyuuTypeAction _
                                                                                , HosyouKaiyaku.SetSeikyuuTypeAction

        '請求タイプ
        Dim enSeikyuuType As EarthEnum.EnumSeikyuuType
        Dim lgcJiban As New JibanLogic

        If strSeikyuuSakiTypeStr = EarthConst.SEIKYU_TYOKUSETU Then
            ' 直接請求
            If lgcJiban.GetKeiretuFlg(strKeiretuCd) = 1 Then
                ' 系列
                enSeikyuuType = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu
            Else
                ' 系列以外
                enSeikyuuType = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu
            End If
        Else
            ' 他請求
            If lgcJiban.GetKeiretuFlg(strKeiretuCd) = 1 Then
                ' 系列
                enSeikyuuType = EarthEnum.EnumSeikyuuType.TaSeikyuuKeiretu
            Else
                ' 系列以外
                enSeikyuuType = EarthEnum.EnumSeikyuuType.TaSeikyuuNotKeiretu
            End If
        End If

        'カスタムイベント発生元IDによる分岐
        Select Case True
            Case strId.IndexOf(Me.IraiNaiyou.ClientID) >= 0
                '商品1レコードへ請求タイプの設定
                Me.Syouhin1Record01.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , EarthConst.Instance.ARRAY_SHOUHIN_LINES(0))
                '商品2レコードへ請求タイプの設定
                Me.CtrlTeibetuSyouhin2.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd)
                '商品3レコードへ請求タイプの設定
                Me.CtrlTeibetuSyouhin3.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd)

                '報告書調査レコードへ請求タイプの設定
                Me.HoukokusyoTyousa.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd)

                '報告書工事レコードへ請求タイプの設定
                Me.HoukokusyoKouji.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd)

                '保証レコードへ請求タイプの設定
                Me.HosyouHosyousyo.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd)

                '解約払戻レコードへ請求タイプの設定
                Me.HosyouKaiyaku.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd)

            Case strId.IndexOf(Me.Syouhin1Record01.ClientID) >= 0
                '商品1レコードへ請求タイプの設定
                Me.Syouhin1Record01.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , EarthConst.Instance.ARRAY_SHOUHIN_LINES(0))
            Case strId.IndexOf(Me.CtrlTeibetuSyouhin2.ClientID) >= 0
                '商品2レコードへ請求タイプの設定
                Me.CtrlTeibetuSyouhin2.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , strId)

            Case strId.IndexOf(Me.CtrlTeibetuSyouhin3.ClientID) >= 0
                '商品3レコードへ請求タイプの設定
                Me.CtrlTeibetuSyouhin3.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , strId)

            Case strId.IndexOf(Me.HoukokusyoTyousa.ClientID) >= 0
                '報告書調査レコードへ請求タイプの設定
                Me.HoukokusyoTyousa.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , strId)

            Case strId.IndexOf(Me.HoukokusyoKouji.ClientID) >= 0
                '報告書工事レコードへ請求タイプの設定
                Me.HoukokusyoKouji.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , strId)

            Case strId.IndexOf(Me.HosyouHosyousyo.ClientID) >= 0
                '保証レコードへ請求タイプの設定
                Me.HosyouHosyousyo.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , strId)

            Case strId.IndexOf(Me.HosyouKaiyaku.ClientID) >= 0
                '解約払戻レコードへ請求タイプの設定
                Me.HosyouKaiyaku.SetSeikyuuType(enSeikyuuType _
                                                    , strKeiretuCd _
                                                    , strKameitenCd _
                                                    , strId)
        End Select
    End Sub

    ''' <summary>
    ''' 調査方法の設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="TyousaHouhouNo">調査方法No</param>
    ''' <remarks></remarks>
    Private Sub SetTysHouhou(ByVal sender As System.Object _
                                        , ByVal e As System.EventArgs _
                                        , ByRef TyousaHouhouNo As Integer) Handles CtrlTeibetuSyouhin3.SetTysHouhouAction
        TyousaHouhouNo = IraiNaiyou.TysHouhou
    End Sub

    ''' <summary>
    '''商品毎の請求・仕入先が変更されていないかをチェックし、
    '''変更されている場合情報の再取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setSeikyuuSiireHenkou(ByVal sender As Object, ByVal e As System.EventArgs)
        '*************************************************************
        '* 請求先、仕入先が変更された行をチェックし、存在した場合は
        '* 各行の請求有無変更時の処理を実行する
        '*************************************************************
        '商品1
        If Me.Syouhin1Record01.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
            ' 依頼コントロールの商品１設定ロジックを実行し、結果が商品１に反映される
            IraiNaiyou.Syouhin1Set(sender, e)
            Me.Syouhin1Record01.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
            '変更された商品が有った場合、処理終了(原則として、1商品毎しか変更されないため)
            Exit Sub
        End If
        '商品2
        If CtrlTeibetuSyouhin2.setSeikyuuSiireHenkou(sender, e) Then
            '変更された商品が有った場合、処理終了(原則として、1商品毎しか変更されないため)
            Exit Sub
        End If
        '商品3
        If CtrlTeibetuSyouhin3.setSeikyuuSiireHenkou(sender, e) Then
            '変更された商品が有った場合、処理終了(原則として、1商品毎しか変更されないため)
            Exit Sub
        End If
        '報告書(調査)
        If Me.HoukokusyoTyousa.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
            Me.HoukokusyoKouji.DispMode = TeibetuSyouhinRecordCtrl.DisplayMode.HOUKOKUSYO
            Me.HoukokusyoTyousa.SetSyouhinEtc(sender, e, True)
            Me.HoukokusyoTyousa.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
            '変更された商品が有った場合、処理終了(原則として、1商品毎しか変更されないため)
            Exit Sub
        End If
        '報告書(工事)
        If Me.HoukokusyoKouji.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
            Me.HoukokusyoKouji.DispMode = TeibetuSyouhinRecordCtrl.DisplayMode.HOUKOKUSYO
            Me.HoukokusyoKouji.SetSyouhinEtc(sender, e, True)
            Me.HoukokusyoKouji.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
            '変更された商品が有った場合、処理終了(原則として、1商品毎しか変更されないため)
            Exit Sub
        End If
        '保証
        If Me.HosyouHosyousyo.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
            Me.HosyouHosyousyo.DispMode = TeibetuSyouhinRecordCtrl.DisplayMode.HOSYOU
            Me.HosyouHosyousyo.SetSyouhinEtc(sender, e, True)
            Me.HosyouHosyousyo.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
            '変更された商品が有った場合、処理終了(原則として、1商品毎しか変更されないため)
            Exit Sub
        End If
        '解約払戻
        If Me.HosyouKaiyaku.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
            Me.HosyouKaiyaku.DispMode = TeibetuSyouhinRecordCtrl.DisplayMode.KAIYAKU
            Me.HosyouKaiyaku.SetSyouhinEtc(sender, e, True)
            Me.HosyouKaiyaku.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
            '変更された商品が有った場合、処理終了(原則として、1商品毎しか変更されないため)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 商品１の実請求額、工務店請求額を依頼画面へセットする<br/>
    ''' 商品１コントロールで変更されるタイミングで本メソッドが呼ばれます
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="jituSeikyuuGaku">実請求金額</param>
    ''' <param name="koumutenSeikyuuGaku">工務店請求額</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuKingaku(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs, _
                                  ByVal jituSeikyuuGaku As String, _
                                  ByVal koumutenSeikyuuGaku As String) Handles Syouhin1Record01.KingakuSetAction

        IraiNaiyou.Syouhin1JituSeikyuuGaku = jituSeikyuuGaku           ' 実請求額
        IraiNaiyou.Syouhin1KoumutenSeikyuuGaku = koumutenSeikyuuGaku   ' 工務店請求額
        IraiNaiyou.UpdateSyouhin1Seikyuu.Update()

    End Sub

    ''' <summary>
    ''' 依頼コントロールの商品１設定ロジックを実行
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecKakakuSettei(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin1Record01.ExecKakakuSettei

        ' 依頼コントロールの商品１設定ロジックを実行し、結果が商品１に反映される
        IraiNaiyou.Syouhin1Set(Me, e)

    End Sub

    ''' <summary>
    ''' 画面の内容をDBに反映する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveData()

        '完了メッセージ兼、次物件指定ポップアップ表示のためのフラグをクリア
        callModalFlg.Value = String.Empty

        ' 現在の地盤データをDBから取得する
        Dim jibanLogic As New JibanLogic
        Dim jibanRecOld As New JibanRecord
        jibanRecOld = jibanLogic.GetJibanData(HiddenKubun.Value, HiddenBangou.Value)

        ' 画面内容より地盤レコードを生成する
        Dim jibanRec As New JibanRecordTeibetuSyuusei

        '進捗T更新用に、DB上の値をセットする
        jibanLogic.SetSintyokuJibanData(jibanRecOld, jibanRec)

        ' 邸別データ修正用のロジッククラス
        Dim logic As New TeibetuSyuuseiLogic

        '***************************************
        ' 地盤データ
        '***************************************
        ' 区分
        jibanRec.Kbn = HiddenKubun.Value
        ' 番号（保証書NO）
        jibanRec.HosyousyoNo = HiddenBangou.Value
        ' 更新者ユーザーID
        jibanRec.UpdLoginUserId = user_info.LoginUserId
        ' 加盟店コード
        jibanRec.KameitenCd = IraiNaiyou.KameitenCd
        ' 商品区分
        jibanRec.SyouhinKbn = IraiNaiyou.SyouhinKbn
        ' 備考
        jibanRec.Bikou = Kyoutuu.Bikou1
        ' 備考2
        jibanRec.Bikou2 = Kyoutuu.Bikou2
        ' 調査会社ｺｰﾄﾞ
        jibanRec.TysKaisyaCd = IraiNaiyou.TysKaisyaCd
        ' 調査会社事業所ｺｰﾄﾞ
        jibanRec.TysKaisyaJigyousyoCd = IraiNaiyou.TysKaisyaJigyousyoCd
        ' 建物用途
        jibanRec.TatemonoYoutoNo = IraiNaiyou.TatemonoYoutoNo
        ' 調査方法
        jibanRec.TysHouhou = IraiNaiyou.TysHouhou
        ' 調査概要
        jibanRec.TysGaiyou = IraiNaiyou.TysGaiyou
        ' 更新日時 読み込み時のタイムスタンプ
        jibanRec.UpdDatetime = Date.Parse(HiddenUpdDatetime.Value)
        ' 同時依頼棟数
        jibanRec.DoujiIraiTousuu = IraiNaiyou.DoujiIraiTousuu
        '************************
        ' 工事会社
        '************************
        ' 工事会社ｺｰﾄﾞ
        jibanRec.KojGaisyaCd = Kairyoukouji.KoujiKaisyaCd
        ' 工事会社事業所ｺｰﾄﾞ
        jibanRec.KojGaisyaJigyousyoCd = Kairyoukouji.KoujiKaisyaJigyousyoCd
        ' 工事会社請求有無
        jibanRec.KojGaisyaSeikyuuUmu = Kairyoukouji.KoujiKaisyaSeikyuuUmu
        '************************
        ' 追加工事会社
        '************************
        ' 追加工事会社ｺｰﾄﾞ
        jibanRec.TKojKaisyaCd = Tuikakouji.KoujiKaisyaCd
        ' 追加工事会社事業所ｺｰﾄﾞ
        jibanRec.TKojKaisyaJigyousyoCd = Tuikakouji.KoujiKaisyaJigyousyoCd
        ' 追加工事会社請求有無
        jibanRec.TKojKaisyaSeikyuuUmu = Tuikakouji.KoujiKaisyaSeikyuuUmu

        '***************************************
        ' 邸別請求データ
        '***************************************

        ' 商品１の邸別請求データをセットします
        jibanRec.Syouhin1Record = Syouhin1Record01.TeibetuSeikyuuRec

        ' 商品２の邸別請求データをセットします
        If Not CtrlTeibetuSyouhin2.Syouhin2Records Is Nothing Then
            jibanRec.Syouhin2Records = CtrlTeibetuSyouhin2.Syouhin2Records
        End If

        ' 商品３の邸別請求データをセットします
        If Not CtrlTeibetuSyouhin3.Syouhin3Records Is Nothing Then
            jibanRec.Syouhin3Records = CtrlTeibetuSyouhin3.Syouhin3Records
        End If

        ' 改良工事の邸別請求データをセットします
        jibanRec.KairyouKoujiRecord = Kairyoukouji.TeibetuSeikyuuRec

        ' 追加工事の邸別請求データをセットします
        jibanRec.TuikaKoujiRecord = Tuikakouji.TeibetuSeikyuuRec

        ' 調査報告書の邸別請求データをセットします
        jibanRec.TyousaHoukokusyoRecord = HoukokusyoTyousa.TeibetuSeikyuuRec

        ' 工事報告書の邸別請求データをセットします
        jibanRec.KoujiHoukokusyoRecord = HoukokusyoKouji.TeibetuSeikyuuRec

        ' 保証書の邸別請求データをセットします
        jibanRec.HosyousyoRecord = HosyouHosyousyo.TeibetuSeikyuuRec

        ' 解約払戻の邸別請求データをセットします
        jibanRec.KaiyakuHaraimodosiRecord = HosyouKaiyaku.TeibetuSeikyuuRec

        '更新者
        jibanRec.Kousinsya = cbLogic.GetKousinsya(user_info.LoginUserId, DateTime.Now)

        '*********************************************************
        '●保証関連の自動設定
        cbLogic.ps_AutoSetHosyousyoHakJyjy(EarthEnum.emGamenInfo.TeibetuSyuusei, jibanRec)

        '物件履歴データの自動セット
        Dim brRec As BukkenRirekiRecord = cbLogic.MakeBukkenRirekiRecHosyouUmu(jibanRec, cl.GetDisplayString(jibanRecOld.HosyouSyouhinUmu), cl.GetDisplayString(jibanRecOld.KeikakusyoSakuseiDate))

        If Not brRec Is Nothing Then
            '物件履歴レコードのチェック
            Dim strErrMsg As String = String.Empty
            If cbLogic.checkInputBukkenRireki(brRec, strErrMsg) = False Then
                MLogic.AlertMessage(Me, strErrMsg, 0, "ErrBukkenRireki")
                Exit Sub
            End If
        End If
        '*********************************************************

        '特別対応価格対応
        Dim strTokubetuTaiouCd As String = String.Empty
        Dim strTmp As String

        '商品１
        If Me.Syouhin1Record01.AccTokubetuTaiouUpdFlg.Value = EarthConst.ARI_VAL Then
            strTokubetuTaiouCd = Me.Syouhin1Record01.AccTokubetuTaiouToolTip.AccTaisyouCd.Value
        End If

        '商品２
        strTmp = Me.CtrlTeibetuSyouhin2.GetCdFromToolTip
        If strTmp <> String.Empty Then
            If strTokubetuTaiouCd = String.Empty Then
                strTokubetuTaiouCd = strTmp
            Else
                strTokubetuTaiouCd &= EarthConst.SEP_STRING & strTmp
            End If
        End If

        '商品３
        strTmp = Me.CtrlTeibetuSyouhin3.GetCdFromToolTip
        If strTmp <> String.Empty Then
            If strTokubetuTaiouCd = String.Empty Then
                strTokubetuTaiouCd = strTmp
            Else
                strTokubetuTaiouCd &= EarthConst.SEP_STRING & strTmp
            End If
        End If

        Dim sender As New Object
        Dim ttLogic As New TokubetuTaiouLogic
        Dim intTokubetuCnt As Integer = 0
        Dim listRec As New List(Of TokubetuTaiouRecordBase)

        If strTokubetuTaiouCd <> String.Empty Then
            listRec = ttLogic.GetTokubetuTaiouDataInfo(sender, _
                                                         jibanRec.Kbn, _
                                                         jibanRec.HosyousyoNo, _
                                                         strTokubetuTaiouCd, _
                                                         intTokubetuCnt)
        End If

        If intTokubetuCnt <= 0 Then
            listRec = Nothing
        End If

        ' データの更新を行います
        If logic.SaveJibanData(Me, jibanRec, brRec, listRec) Then

            ' 再読み込みする
            SetJibanData()

            '完了メッセージ兼、次物件指定ポップアップ表示のためにフラグをセット
            callModalFlg.Value = Boolean.TrueString

            '請求有無と売上金額の整合性チェックフラグ
            HiddenSeikyuuUmuCheck.Value = String.Empty
            '変更箇所チェックフラグ
            HiddenChgValChk.Value = String.Empty

        End If

    End Sub

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <returns>チェック結果 True:OK False:NG</returns>
    ''' <remarks></remarks>
    Private Function CheckInput() As Boolean

        'エラーメッセージ初期化
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New List(Of Control)
        Dim denpyouErrMess As String = String.Empty
        Dim tmpDenpyouNgList As String = HiddenDenpyouNGList.Value
        Dim seikyuuUmuErrMess As String = String.Empty '請求有無チェック用
        Dim strChgPartMess As String = String.Empty '変更箇所用

        ' エラーメッセージを保存
        Dim saveErrMess As String = errMess
        Dim saveErrMess2 As String = strChgPartMess '変更箇所用

        ' 共通情報のエラーチェック
        Kyoutuu.CheckInput(errMess, arrFocusTargetCtrl, "共通情報", strChgPartMess)

        If errMess <> saveErrMess OrElse strChgPartMess <> saveErrMess2 Then
            ' エラーメッセージが追加されたので依頼情報を展開する
            Kyoutuu.KyoutuuInfo.Attributes("style") = "display:inline;"
            saveErrMess = errMess
            saveErrMess2 = strChgPartMess
        End If

        ' 依頼情報のエラーチェック
        IraiNaiyou.CheckInput(errMess, arrFocusTargetCtrl, "依頼情報", strChgPartMess, Syouhin1Record01.SyouhinCd)

        If errMess <> saveErrMess OrElse strChgPartMess <> saveErrMess2 Then
            ' エラーメッセージが追加されたので依頼情報を展開する
            IraiNaiyou.IraiTBody.Attributes("style") = "display:inline;"
            saveErrMess = errMess
            saveErrMess2 = strChgPartMess
        End If

        ' 商品１のエラーチェック
        Syouhin1Record01.CheckInput(errMess, arrFocusTargetCtrl, "商品１", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' 商品２のエラーチェック
        CtrlTeibetuSyouhin2.CheckInput(errMess, arrFocusTargetCtrl, "商品２", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        If errMess <> saveErrMess OrElse strChgPartMess <> saveErrMess2 Then
            ' エラーメッセージが追加されたので商品1/2情報を展開する
            TBodySyouhin12.Attributes("style") = "display:inline;"
            saveErrMess = errMess
            saveErrMess2 = strChgPartMess
        End If

        ' 商品３のエラーチェック
        CtrlTeibetuSyouhin3.CheckInput(errMess, arrFocusTargetCtrl, "商品３", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        If errMess <> saveErrMess OrElse strChgPartMess <> saveErrMess2 Then
            ' エラーメッセージが追加されたので商品3情報を展開する
            TBodySyouhin3.Attributes("style") = "display:inline;"
            saveErrMess = errMess
            saveErrMess2 = strChgPartMess
        End If

        ' 改良工事のエラーチェック
        Kairyoukouji.CheckInput(errMess, arrFocusTargetCtrl, "改良工事", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' 追加工事のエラーチェック
        Tuikakouji.CheckInput(errMess, arrFocusTargetCtrl, "追加工事", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        If errMess <> saveErrMess OrElse strChgPartMess <> saveErrMess2 Then
            ' エラーメッセージが追加されたので改良工事情報を展開する
            TBodyKairyou.Attributes("style") = "display:inline;"
            saveErrMess = errMess
            saveErrMess2 = strChgPartMess
        End If

        ' 調査報告書のエラーチェック
        HoukokusyoTyousa.CheckInput(errMess, arrFocusTargetCtrl, "調査報告書", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' 工事報告書のエラーチェック
        HoukokusyoKouji.CheckInput(errMess, arrFocusTargetCtrl, "工事報告書", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        If errMess <> saveErrMess OrElse strChgPartMess <> saveErrMess2 Then
            ' エラーメッセージが追加されたので報告書情報を展開する
            TBodyHoukokusyo.Attributes("style") = "display:inline;"
            saveErrMess = errMess
            saveErrMess2 = strChgPartMess
        End If

        ' 保証書のエラーチェック
        HosyouHosyousyo.CheckInput(errMess, arrFocusTargetCtrl, "保証書", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' 解約払戻のエラーチェック
        HosyouKaiyaku.CheckInput(errMess, arrFocusTargetCtrl, "解約払戻", tmpDenpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        If errMess <> saveErrMess OrElse strChgPartMess <> saveErrMess2 Then
            ' エラーメッセージが追加されたので保証書情報を展開する
            TBodyHosyou.Attributes("style") = "display:inline;"
            saveErrMess = errMess
            saveErrMess2 = strChgPartMess
        End If

        '**********************
        '* 各種チェック処理
        '**********************
        '●変更箇所
        Dim strTmpChgChk As String = strChgPartMess.Replace("\r\n", "") 'JSにて改行コードが変換されるため置換
        If strChgPartMess <> "" And Me.HiddenChgValChk.Value <> strTmpChgChk Then
            'フォーカスセット
            ButtonTouroku1.Focus()
            'エラーメッセージ表示
            Dim strMsg As String = Messages.MSG178C.Replace("@PARAM1", strChgPartMess)
            Dim tmpScript = "if(confirm('" & strMsg & "')){document.getElementById('" & HiddenChgValChk.ClientID & "').value='" & strTmpChgChk & "'; autoExeButtonId = '" & ButtonTouroku1.ClientID & "';};"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If
        '●入力チェック全般
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
        '●伝票関連
        If denpyouErrMess <> "" Then
            'フォーカスセット
            ButtonTouroku1.Focus()
            'エラーメッセージ表示
            Dim tmpScript = "if(confirm('" & Messages.MSG152C.Replace("@PARAM1", denpyouErrMess) & "')){document.getElementById('" & HiddenDenpyouNGList.ClientID & "').value=''; autoExeButtonId = '" & ButtonTouroku1.ClientID & "';};"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If
        '●請求有無関連
        If seikyuuUmuErrMess <> "" And HiddenSeikyuuUmuCheck.Value <> "1" Then
            'フォーカスセット
            ButtonTouroku1.Focus()
            'エラーメッセージ表示
            Dim tmpScript = "if(confirm('" & String.Format(Messages.MSG156C, seikyuuUmuErrMess) & "')){document.getElementById('" & HiddenSeikyuuUmuCheck.ClientID & "').value='1'; autoExeButtonId = '" & ButtonTouroku1.ClientID & "';};"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        Return True
    End Function
#Region "特別対応"

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する(特別対応)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringTokubetu() As String
        Dim sb As New StringBuilder

        sb.Append(Me.IraiNaiyou.KameitenCd & EarthConst.SEP_STRING)                         '加盟店コード
        sb.Append(Me.IraiNaiyou.AccSelectSyouhin1.SelectedValue & EarthConst.SEP_STRING)    '商品1コード
        sb.Append(Me.IraiNaiyou.AccTysHouhou.SelectedValue & EarthConst.SEP_STRING)         '調査方法No

        Return (sb.ToString)
    End Function

    ''' <summary>
    ''' 特別対応
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChgDispTokubetuTaiou()

        If Me.HiddenKakuteiValuesTokubetu.Value <> getCtrlValuesStringTokubetu() Then
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
    ''' 特別対応データを取得する(ツールチップ表示用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetTokubetuTaiouCd()

        Dim intCnt As Integer = 0
        Dim ttList As New List(Of TokubetuTaiouRecordBase)

        '区分、保証書NOをキーに特別対応データを取得
        ttList = tLogic.GetTokubetuTaiouDataInfo(Me, Kbn, No, String.Empty, intCnt)

        '振分け処理
        Me.DevideTokubetuTaiouCd(ttList)

    End Sub

    ''' <summary>
    ''' 特別対応データのリストを画面の各ツールチップに振り分ける
    ''' </summary>
    ''' <param name="ttList">特別対応レコードのリスト</param>
    ''' <remarks></remarks>
    Private Sub DevideTokubetuTaiouCd(ByVal ttList As List(Of TokubetuTaiouRecordBase))

        Dim recTmp As New TokubetuTaiouRecordBase       '作業用
        Dim strTokubetuTaiouCd As String = String.Empty '特別対応コード
        Dim strResult As String                         '振分先
        Dim emType As EarthEnum.emToolTipType           'ツールチップ表示タイプ

        '特別対応データ更新フラグを初期化
        Me.Syouhin1Record01.AccTokubetuTaiouUpdFlg.Value = EarthConst.NASI_VAL
        Me.Syouhin1Record01.AccTokubetuTaiouToolTip.AccDisplayCd.Value = String.Empty
        Me.Syouhin1Record01.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty

        Me.CtrlTeibetuSyouhin2.ClearTokubetuTaiouInfo()
        Me.CtrlTeibetuSyouhin3.ClearTokubetuTaiouUpdFlg()

        If Not ttList Is Nothing Then
            For Each recTmp In ttList
                '特別対応コードを作業用に取得
                strTokubetuTaiouCd = cl.GetDisplayString(recTmp.TokubetuTaiouCd)

                '振分け先を取得
                strResult = cbLogic.checkDevideTaisyou(Me, recTmp)

                '振分け先のHiddenに追加
                If strResult <> String.Empty Then
                    Dim search_shouhin As String = strResult.Split(EarthConst.UNDER_SCORE)(0)   '商品判定用
                    Dim strRowNo As String = strResult.Split(EarthConst.UNDER_SCORE)(1)         '

                    'ツールチップHiddenに特別対応コードを格納
                    If search_shouhin = "1" Then
                        'ツールチップ設定対象かチェック
                        emType = cbLogic.checkToolTipSetValue(Me, recTmp, Me.Syouhin1Record01.BunruiCd, Me.Syouhin1Record01.GamenHyoujiNo, Me.Syouhin1Record01.AccUriageSyori.SelectedValue)
                        If emType <> EarthEnum.emToolTipType.NASI Then
                            '表示用
                            Me.Syouhin1Record01.AccTokubetuTaiouToolTip.SetDisplayCd(strTokubetuTaiouCd)
                            '登録用
                            If Me.Syouhin1Record01.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty Then
                                Me.Syouhin1Record01.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = strTokubetuTaiouCd
                            Else
                                Me.Syouhin1Record01.AccTokubetuTaiouToolTip.AccTaisyouCd.Value &= EarthConst.SEP_STRING & strTokubetuTaiouCd
                            End If

                            If emType = EarthEnum.emToolTipType.SYUSEI Then
                                Me.Syouhin1Record01.AccTokubetuTaiouToolTip.AcclblTokubetuTaiou.Text = EarthConst.SYUU_TOOL_TIP
                            End If
                        End If

                    ElseIf search_shouhin = "2" Then
                        Me.CtrlTeibetuSyouhin2.SetTokubetuTaiouToolTip(strTokubetuTaiouCd, strRowNo, recTmp)

                    ElseIf search_shouhin = "3" Then
                        Me.CtrlTeibetuSyouhin3.SetTokubetuTaiouToolTip(strTokubetuTaiouCd, strRowNo, recTmp)

                    End If

                End If
            Next
        End If
    End Sub

#End Region

#End Region

End Class