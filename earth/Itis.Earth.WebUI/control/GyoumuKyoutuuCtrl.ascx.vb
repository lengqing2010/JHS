
Partial Public Class GyoumuKyoutuuCtrl
    Inherits System.Web.UI.UserControl

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

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
    ''' 共通ロジッククラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim cbLogic As New CommonBizLogic
    ''' <summary>
    ''' メッセージクラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic
#End Region

#Region "メンバ定数"

#Region "画面表示モード"
    ''' <summary>
    ''' コントロールの画面表示モード
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DisplayMode
        ''' <summary>
        ''' 報告書画面
        ''' </summary>
        ''' <remarks></remarks>
        HOUKOKUSYO = 1
        ''' <summary>
        ''' 改良工事画面
        ''' </summary>
        ''' <remarks></remarks>
        KAIRYOU = 2
        ''' <summary>
        ''' 保証画面
        ''' </summary>
        ''' <remarks></remarks>
        HOSYOU = 3
    End Enum
#End Region

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
    Public Event OyaGamenAction_hensyu(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal blnDisabled As Boolean)

    ''' <summary>
    ''' 登録完了時の親画面の処理実行用カスタムイベント
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OyaActAtAfterExe(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal irai1Mode As String, ByVal actMode As String, ByVal exeMode As String, ByVal result As Boolean)
#End Region

#Region "ユーザーコントロールへの外部からのアクセス用Getter/Setter"

    ''' <summary>
    ''' 外部からのアクセス用 for HtmlInputHidden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHiddenKubun() As HtmlInputHidden
        Get
            Return HiddenKubun
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenKubun = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextBangou
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccBangou() As HtmlInputText
        Get
            Return TextBangou
        End Get
        Set(ByVal value As HtmlInputText)
            TextBangou = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextKameitenCd
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccKameitenCd() As HtmlInputText
        Get
            Return TextKameitenCd
        End Get
        Set(ByVal value As HtmlInputText)
            TextKameitenCd = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for HiddenKameitenCdTextOld
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccKameitenCdTextOld() As HtmlInputHidden
        Get
            Return HiddenKameitenCdTextOld
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenKameitenCdTextOld = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for SelectDataHaki
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccDataHakiSyubetu() As DropDownList
        Get
            Return SelectDataHaki
        End Get
        Set(ByVal value As DropDownList)
            SelectDataHaki = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TextDataHakiDate
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccDataHakiDate() As HtmlInputText
        Get
            Return TextDataHakiDate
        End Get
        Set(ByVal value As HtmlInputText)
            TextDataHakiDate = value
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
    ''' 外部からのアクセス用 for lastUpdateDateTime
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccDateTimeDetail() As HtmlInputHidden
        Get
            Return HiddenUpdDatetime
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenUpdDatetime = value
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
    ''' 外部からのアクセス用 for 更新日付[DB更新用]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccupdateDateTime() As HtmlInputHidden
        Get
            Return updateDateTime
        End Get
        Set(ByVal value As HtmlInputHidden)
            updateDateTime = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for 調査会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>調査会社コード+事業所コード</remarks>
    Public Property AccTyousaKaisyaCd() As HtmlInputHidden
        Get
            Return HiddenTyousaKaisyaCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenTyousaKaisyaCd = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for 工事会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>工事会社コード+事業所コード</remarks>
    Public Property AccKoujiKaisyaCd() As HtmlInputHidden
        Get
            Return HiddenKoujiKaisyaCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenKoujiKaisyaCd = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TD破棄種別
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>破棄種別</remarks>
    Public Property AccTdDataHakiSyubetu() As HtmlTableCell
        Get
            Return TdDataHakiSyubetu
        End Get
        Set(ByVal value As HtmlTableCell)
            TdDataHakiSyubetu = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for TD破棄日
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>データ破棄日</remarks>
    Public Property AccTdDataHakiData() As HtmlTableCell
        Get
            Return TdDataHakiDate
        End Get
        Set(ByVal value As HtmlTableCell)
            TdDataHakiDate = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for 系列コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnKeiretuCd() As HtmlInputHidden
        Get
            Return HiddenKeiretuCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenKeiretuCd = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for 営業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnEigyousyoCd() As HtmlInputHidden
        Get
            Return HiddenEigyousyoCd
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenEigyousyoCd = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for 調査概要
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnTyousaGaiyou() As HtmlInputHidden
        Get
            Return HiddenTyousaGaiyou
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenTyousaGaiyou = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for 共通情報
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccKyoutuuInfo() As HtmlGenericControl
        Get
            Return kyoutuuInfo
        End Get
        Set(ByVal value As HtmlGenericControl)
            kyoutuuInfo = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for 共通情報スタイル
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccHdnKyoutuuInfoStyle() As HtmlInputHidden
        Get
            Return HiddenKyoutuuInfoStyle
        End Get
        Set(ByVal value As HtmlInputHidden)
            HiddenKyoutuuInfoStyle = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for 施主名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSesyuMei() As HtmlInputText
        Get
            Return TextSesyuMei
        End Get
        Set(ByVal value As HtmlInputText)
            TextSesyuMei = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for 物件住所1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccBukkenJyuusyo1() As HtmlInputText
        Get
            Return TextBukkenJyuusyo1
        End Get
        Set(ByVal value As HtmlInputText)
            TextBukkenJyuusyo1 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for 物件住所2
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccBukkenJyuusyo2() As HtmlInputText
        Get
            Return TextBukkenJyuusyo2
        End Get
        Set(ByVal value As HtmlInputText)
            TextBukkenJyuusyo2 = value
        End Set
    End Property

    ''' <summary>
    ''' 外部からのアクセス用 for 物件住所3
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccBukkenJyuusyo3() As HtmlInputText
        Get
            Return TextBukkenJyuusyo3
        End Get
        Set(ByVal value As HtmlInputText)
            TextBukkenJyuusyo3 = value
        End Set
    End Property

#End Region

#Region "プロパティ"
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <remarks></remarks>
    Private mode As DisplayMode
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <value></value>
    ''' <returns>コントロールの表示モード</returns>
    ''' <remarks>商品の種類により画面の表示を変更します</remarks>
    Public Property DispMode() As DisplayMode
        Get
            Return mode
        End Get
        Set(ByVal value As DisplayMode)
            mode = value
        End Set
    End Property

    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>隠しフィールドより取得</remarks>
    Public Property Kubun() As String
        Get
            Return HiddenKubun.Value
        End Get
        Set(ByVal value As String)
            HiddenKubun.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 番号（旧保証書NO）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Bangou() As String
        Get
            Return TextBangou.Value
        End Get
        Set(ByVal value As String)
            TextBangou.Value = value
        End Set
    End Property

    ''' <summary>
    ''' データ破棄種別
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataHakiSyubetu() As String
        Get
            Return SelectDataHaki.SelectedValue
        End Get
        Set(ByVal value As String)
            SelectDataHaki.SelectedValue = value
        End Set
    End Property

    ''' <summary>
    ''' データ破棄日
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataHakiDate() As String
        Get
            Return TextDataHakiDate.Value
        End Get
        Set(ByVal value As String)
            TextDataHakiDate.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SesyuMei() As String
        Get
            Return TextSesyuMei.Value
        End Get
        Set(ByVal value As String)
            TextSesyuMei.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 物件住所１
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Jyuusyo1() As String
        Get
            Return TextBukkenJyuusyo1.Value
        End Get
        Set(ByVal value As String)
            TextBukkenJyuusyo1.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 物件住所２
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Jyuusyo2() As String
        Get
            Return TextBukkenJyuusyo2.Value
        End Get
        Set(ByVal value As String)
            TextBukkenJyuusyo2.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 物件住所３
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Jyuusyo3() As String
        Get
            Return TextBukkenJyuusyo3.Value
        End Get
        Set(ByVal value As String)
            TextBukkenJyuusyo3.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Bikou() As String
        Get
            Return TextAreaBikou.Value
        End Get
        Set(ByVal value As String)
            TextAreaBikou.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 備考２
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Bikou2() As String
        Get
            Return TextAreaBikou2.Value
        End Get
        Set(ByVal value As String)
            TextAreaBikou2.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 調査会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TyousaKaishaCd() As String
        Get
            Return HiddenTyousaKaisyaCd1.Value
        End Get
        Set(ByVal value As String)
            HiddenTyousaKaisyaCd1.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 調査会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TyousaKaishaJigyousyoCd() As String
        Get
            Return HiddenTyousaKaisyaCd2.Value
        End Get
        Set(ByVal value As String)
            HiddenTyousaKaisyaCd2.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 調査会社名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TyousaKaishaMei() As String
        Get
            Return HiddenTyousaKaisyaMei.Value
        End Get
        Set(ByVal value As String)
            HiddenTyousaKaisyaMei.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 調査方法コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TyousaHouhouCd() As String
        Get
            Return HiddenTyousaHouhouCd.Value
        End Get
        Set(ByVal value As String)
            HiddenTyousaHouhouCd.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 調査方法名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TyousaHouhouMei() As String
        Get
            Return HiddenTyousaHouhouMei.Value
        End Get
        Set(ByVal value As String)
            HiddenTyousaHouhouMei.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 加盟店名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KameitenMei() As String
        Get
            Return TextKameitenMei.Value
        End Get
        Set(ByVal value As String)
            TextKameitenMei.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 加盟店電話番号
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KameitenTel() As String
        Get
            Return TextKameitenTel.Value
        End Get
        Set(ByVal value As String)
            TextKameitenTel.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 加盟店FAX番号
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KameitenFax() As String
        Get
            Return TextKameitenFax.Value
        End Get
        Set(ByVal value As String)
            TextKameitenFax.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 加盟店メールアドレス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KameitenMail() As String
        Get
            Return HiddenKameitenMail.Value
        End Get
        Set(ByVal value As String)
            HiddenKameitenMail.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 構造コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KouzouCd() As String
        Get
            Return HiddenKouzou.Value
        End Get
        Set(ByVal value As String)
            HiddenKouzou.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 構造名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KouzouMei() As String
        Get
            Return HiddenKouzouMei.Value
        End Get
        Set(ByVal value As String)
            HiddenKouzouMei.Value = value
        End Set
    End Property

#End Region

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

        If IsPostBack = False Then

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            ' コンボ設定ヘルパークラスを生成
            Dim helper As New DropDownHelper
            ' 破棄コンボにデータをバインドする
            helper.SetDropDownList(SelectDataHaki, DropDownHelper.DropDownType.DataHaki, True)

            '地盤読み込みデータがセッションに存在する場合、画面に表示させる
            If iraiSession.JibanData IsNot Nothing Then
                Dim jr As JibanRecordBase = iraiSession.JibanData
                SetCtrlFromJibanRec(sender, e, jr)
            End If

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            setDispAction()

            '画面制御(初期起動)
            Me.SetEnableControlInit()
        End If

    End Sub

    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行う（参照処理用）
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)

        ' 入力されているコードをキーに、マスタを検索し、データを1件のみ取得できた場合は
        ' 画面情報を更新する。データが無い場合は、検索画面を表示する
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim blnTorikesi As Boolean = False
        Dim dataArray As New List(Of KameitenSearchRecord)
        Dim JibanLogic As New JibanLogic

        Dim recData As New KameitenSearchRecord

        ' コンボ設定ヘルパークラスを生成
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        '画面コントロールに設定
        '●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Kubun, True, True)
        objDrpTmp.SelectedValue = jr.Kbn
        TextKubun.Value = objDrpTmp.SelectedItem.Text '区分
        HiddenKubun.Value = jr.Kbn '隠しフィールド

        TextBangou.Value = cl.GetDispStr(jr.HosyousyoNo) '番号
        SelectDataHaki.SelectedValue = cl.GetDispStr(jr.DataHakiSyubetu) 'データ破棄種別
        TextDataHakiDate.Value = cl.GetDispStr(cl.GetDispStr(jr.DataHakiDate)) 'データ破棄日
        TextSesyuMei.Value = cl.GetDispStr(jr.SesyuMei) '施主名
        TextBukkenJyuusyo1.Value = cl.GetDispStr(jr.BukkenJyuusyo1) '物件住所1
        TextBukkenJyuusyo2.Value = cl.GetDispStr(jr.BukkenJyuusyo2) '物件住所2
        TextBukkenJyuusyo3.Value = cl.GetDispStr(jr.BukkenJyuusyo3) '物件住所3
        TextAreaBikou.Value = cl.GetDispStr(jr.Bikou) '備考
        TextAreaBikou2.Value = cl.GetDispStr(jr.Bikou2) '備考2

        '●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Keiyu)
        objDrpTmp.SelectedValue = jr.Keiyu
        TextKeiyu.Value = objDrpTmp.SelectedItem.Text '経由

        TextTatemonoKensa.Value = cl.GetDispUmuStr(jr.KasiUmu, False, True) '建物検査※瑕疵有無

        TextTyousaRenrakusaki.Value = cl.GetDispStr(jr.TysRenrakusakiAtesakiMei) '調査連絡先・連絡先
        TextTyousaTel.Value = cl.GetDispStr(jr.TysRenrakusakiTel) '調査連絡先・TEL
        TextTyousaFax.Value = cl.GetDispStr(jr.TysRenrakusakiFax) '調査連絡先・FAX
        TextTyousaMiseTantousya.Value = cl.GetDispStr(jr.IraiTantousyaMei) '調査連絡先・店担当者
        TextTyousaMail.Value = cl.GetDispStr(jr.TysRenrakusakiMail) '調査連絡先・MAIL

        If cl.GetDispStr(jr.Kbn) <> "" And cl.GetDispStr(jr.KameitenCd) <> "" Then

            recData = kameitenSearchLogic.GetKameitenSearchResult(jr.Kbn, _
                                                                  jr.KameitenCd, _
                                                                  jr.TysKaisyaCd & jr.TysKaisyaJigyousyoCd, _
                                                                  blnTorikesi)

            '加盟店情報をセット
            Me.SetKameitenInfo(recData)

            '加盟店コードのDB値を退避
            Me.HiddenKameitenCdTextOld.Value = Me.TextKameitenCd.Value
        End If

        '●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetMeisyouDropDownList(objDrpTmp, EarthConst.emMeisyouType.SYOUHIN_KBN, False, False)
        objDrpTmp.SelectedValue = jr.SyouhinKbn
        If jr.SyouhinKbn = 9 Then
            TextSyouhinKbn.Value = " " '商品区分
        Else
            TextSyouhinKbn.Value = objDrpTmp.SelectedItem.Text '商品区分
        End If


        TextDoujiIraiTousuu.Value = cl.GetDispStr(jr.DoujiIraiTousuu) '同時依頼棟数

        '●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.TatemonoYouto, True, True)
        objDrpTmp.SelectedValue = jr.TatemonoYoutoNo
        TextTatemonoYouto.Value = objDrpTmp.SelectedItem.Text '建物用途

        TextKosuu.Value = cl.GetDispStr(jr.Kosuu) '戸数

        '更新日付
        updateDateTime.Value = IIf(jr.UpdDatetime = Date.MinValue, "", Format(jr.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
        HiddenUpdDatetime.Value = IIf(jr.UpdDatetime = Date.MinValue, "", jr.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2))

        '画面表示用 最終更新者、最終更新日時＝＞更新者から取得に変更
        cl.SetKousinsya(jr.Kousinsya, lastUpdateUserNm.Value, lastUpdateDateTime.Value)

        'Hidden項目
        HiddenTyousaKaisyaCd.Value = jr.TysKaisyaCd & jr.TysKaisyaJigyousyoCd '調査会社コード+調査会社事業所コード
        HiddenTyousaKaisyaCd1.Value = jr.TysKaisyaCd '調査会社コード
        HiddenTyousaKaisyaCd2.Value = jr.TysKaisyaJigyousyoCd '調査会社事業所コード
        HiddenKoujiKaisyaCd.Value = jr.KojGaisyaCd & jr.KojGaisyaJigyousyoCd '工事会社コード+工事会社事業所コード
        If jr.TysKaisyaCd <> "" And jr.TysKaisyaJigyousyoCd <> "" Then
            Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
            HiddenTyousaKaisyaMei.Value = tyousakaisyaSearchLogic.GetTyousaKaisyaMei(jr.TysKaisyaCd, jr.TysKaisyaJigyousyoCd, False) '調査会社
        End If
        '●ダミーコンボにセット（調査方法）
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.TyousaHouhou, True, False)
        '調査方法のDDL表示処理
        cl.ps_SetSelectTextBoxTysHouhou(jr.TysHouhou, objDrpTmp, False)
        HiddenTyousaHouhouCd.Value = objDrpTmp.SelectedValue '調査方法コード
        HiddenTyousaHouhouMei.Value = objDrpTmp.SelectedItem.Text '調査方法名

        '●ダミーコンボにセット（構造）
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Kouzou, True, False)
        objDrpTmp.SelectedValue = IIf(jr.Kouzou = Integer.MinValue, "", jr.Kouzou)
        HiddenKouzou.Value = objDrpTmp.SelectedValue '構造コード
        HiddenKouzouMei.Value = objDrpTmp.SelectedItem.Text '構造名

        '調査概要コード
        HiddenTyousaGaiyou.Value = IIf(jr.TysGaiyou = Integer.MinValue, "", jr.TysGaiyou)

        '****************************
        '(3) NG情報設定共通処理
        '****************************
        '判定コード1
        HiddenHanteiCd1.Value = cl.GetDispNum(jr.HanteiCd1, "")
        '判定コード2
        HiddenHanteiCd2.Value = cl.GetDispNum(jr.HanteiCd2, "")
        '工事会社コード+工事会社事業所コード
        HiddenKojGaisyaCd.Value = cl.GetDispStr(jr.KojGaisyaCd & jr.KojGaisyaJigyousyoCd)
        '追加工事会社コード+追加工事会社事業所コード
        HiddenTKojKaisyaCd.Value = cl.GetDispStr(jr.TKojKaisyaCd & jr.TKojKaisyaJigyousyoCd)

        'NG情報設定共通処理
        SetNGInfo()

        'セッションに画面情報を格納
        jSM.Ctrl2Hash(Me, jBn.IraiData)
        iraiSession.Irai1Data = jBn.IraiData

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        'タイトルバーに区分、保証書NO、施主名、住所１を表示
        TitleInfobar.InnerHtml = "【" & HiddenKubun.Value & "】 【" & TextBangou.Value & "】 【" & TextSesyuMei.Value & _
                                        "】 【" & TextBukkenJyuusyo1.Value & "】"

        LinkKyoutuuInfo.HRef &= "changeDisplay('" & TitleInfobar.ClientID & "');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 他システムへのリンクボタン設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '調査見積
        cl.getTyosaMitumoriFilePath(Kubun, Bangou, ButtonTyosaMitumori)

        '保証書DB
        cl.getHosyousyoDbFilePath(Kubun, Bangou, ButtonHosyousyoDB)

        'ReportJHS
        cl.getReportJHSPath(Kubun, Bangou, ButtonRJHS)

        '加盟店注意事項
        cl.getKameitenTyuuijouhouPath(TextKameitenCd.ClientID, ButtonKameitenTyuuijouhou)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'ビルダー情報表示ボタン
        ButtonBuilderCheck.Attributes("onclick") = "callSearch('" & TextKameitenCd.ClientID & "','" & UrlConst.BUILDER_INFO & "','','');"

        '物件履歴表示ボタン
        ButtonBukkenRireki.Attributes("onclick") = "callSearch('" & HiddenKubun.ClientID & EarthConst.SEP_STRING & TextBangou.ClientID & "','" & UrlConst.POPUP_BUKKEN_RIREKI & "','','');"

        '更新履歴表示ボタン
        ButtonKousinRireki.Attributes("onclick") = "callSearch('" & TextKubun.ClientID & EarthConst.SEP_STRING & TextBangou.ClientID & "','" & UrlConst.SEARCH_KOUSIN_RIREKI & "','','');"

        '特別対応
        cl.getTokubetuTaiouLinkPath(Me.ButtonTokubetuTaiou, _
                                     user_info, _
                                     TextKubun.ClientID, _
                                     TextBangou.ClientID, _
                                     "", _
                                     "", _
                                     "")

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' データ破棄種別によって、画面表示を切替える処理
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' データ破棄日は未指定時のみ活性化（その際はデータ破棄関係以外全て非活性となる）
        SelectDataHaki.Attributes("onchange") = "changeHaki(this);"
        CheckHakiDisable()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 加盟店コード
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextKameitenCd.Attributes("onfocus") = "removeFig(this);setTempValueForOnBlur(this);"
        Me.TextKameitenCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(checkNumber(this))callKameitenSearch(this);}else{checkNumber(this);}"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ テーブルの表示切替
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '共通情報
        LinkKyoutuuInfo.HRef = "JavaScript:changeDisplay('" & kyoutuuInfo.ClientID & "');SetDisplayStyle('" & HiddenKyoutuuInfoStyle.ClientID & "','" & kyoutuuInfo.ClientID & "');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)

    End Sub

#Region "画面制御"

    ''' <summary>
    ''' コントロールの初期起動時の画面制御
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetEnableControlInit()
        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable
        jSM.Hash2Ctrl(Me.TdKameiten, EarthConst.MODE_VIEW, ht)

        If Me.DispMode <> DisplayMode.HOSYOU Then
            Me.ButtonKameitenSearch.Style("display") = "none"
        End If
    End Sub

    ''' <summary>
    ''' 加盟店コードの画面制御
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetEnableKameiten(ByVal strBukkenJyky As String)

        '物件状況が「1:保証書依頼可」もしくは「3:済み」の時
        If Me.DispMode = DisplayMode.HOSYOU Then
            If strBukkenJyky = "1" OrElse strBukkenJyky = "3" Then
                cl.chgDispSyouhinText(Me.TextKameitenCd) '加盟店コード
                Me.ButtonKameitenSearch.Style("display") = "inline"
            Else
                Me.SetEnableControlInit()
                Me.ButtonKameitenSearch.Style("display") = "none"
            End If

            '業務共通Ctrlを最新化
            If IsPostBack = True Then
                Me.irai1MainUpdatePanel.Update()
            End If
        End If
    End Sub
#End Region

    ''' <summary>
    ''' 加盟店情報のクリアを行なう
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ClearKameitenInfo(Optional ByVal blnFlg As Boolean = True)

        '初期化処理
        If blnFlg Then
            Me.TextKameitenCd.Value = String.Empty
        End If
        '加盟店取消理由
        Me.TextTorikesiRiyuu.Text = cl.getTorikesiRiyuu(0, String.Empty)
        Me.TextKameitenMei.Value = String.Empty
        Me.TextKameitenTel.Value = String.Empty
        Me.TextKameitenFax.Value = String.Empty
        Me.HiddenKameitenMail.Value = String.Empty
        Me.TextBuilderNo.Value = String.Empty
        Me.HiddenKeiretuCd.Value = String.Empty
        Me.TextKeiretu.Value = String.Empty
        Me.HiddenEigyousyoCd.Value = String.Empty
        Me.TextEigyousyoMei.Value = String.Empty
        Me.TextMitsumoriHituyou.Value = String.Empty
        Me.TextHattyuusyoHituyou.Value = String.Empty
        Me.TextTyousaKaisyaNG.Value = String.Empty
        Me.TextJioSakiFlg.Value = String.Empty
        Me.TextFcTenMei.Value = String.Empty

        '加盟店コード/名称/取消理由の文字色スタイル
        cl.setStyleFontColor(Me.TextKameitenCd.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextKameitenMei.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextTorikesiRiyuu.Style, cl.getKameitenFontColor(0))

        'フォーカスの設定
        Me.setFocusAJ(Me.ButtonKameitenSearch)
    End Sub

    ''' <summary>
    ''' 加盟店検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKameitenSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKameitenSearch.ServerClick

        If kameitenSearchType.Value <> "1" Then
            kameitenSearchSub(sender, e)
        Else
            'コードonchangeで呼ばれた場合
            kameitenSearchSub(sender, e, False)
            kameitenSearchType.Value = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' 加盟店検索ボタン押下時の処理(実体)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub kameitenSearchSub(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal callWindow As Boolean = True)

        ' 入力されているコードをキーに、マスタを検索し、データを1件のみ取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim kLogic As New KameitenSearchLogic
        Dim total_row As Integer = 0
        Dim dataArray As New List(Of KameitenSearchRecord)
        Dim blnTorikesi As Boolean  '取消対象フラグ

        '加盟店コード=入力の場合
        If Me.TextKameitenCd.Value <> String.Empty Then

            'DB値と同じ場合、｢取消=0｣を条件に含めない
            If Me.TextKameitenCd.Value = Me.HiddenKameitenCdTextOld.Value Then
                blnTorikesi = False
            Else
                blnTorikesi = True
            End If

            '検索を実行
            dataArray = kLogic.GetKameitenSearchResult(Me.HiddenKubun.Value _
                                                        , Me.TextKameitenCd.Value _
                                                        , blnTorikesi _
                                                        , total_row)
        End If

        If total_row = 1 Then
            '商品情報を画面にセット
            Dim recData As KameitenSearchRecord = dataArray(0)
            '加盟店コードを入れ直す
            Me.TextKameitenCd.Value = dataArray(0).KameitenCd

            'フォーカスセット
            setFocusAJ(Me.ButtonKameitenSearch)
        Else
            If callWindow = True Then
                '加盟店名
                Me.TextKameitenMei.Value = String.Empty

                '検索画面表示用JavaScript『callSearch』を実行
                Dim tmpFocusScript = "objEBI('" & ButtonKameitenSearch.ClientID & "').focus();"
                Dim tmpScript As String = "callSearch('" & Me.HiddenKubun.ClientID & EarthConst.SEP_STRING & Me.TextKameitenCd.ClientID & _
                                                "','" & UrlConst.SEARCH_KAMEITEN & _
                                                "','" & Me.TextKameitenCd.ClientID & EarthConst.SEP_STRING & Me.TextKameitenMei.ClientID & _
                                                "','" & Me.ButtonKameitenSearch.ClientID & "');"


                tmpScript = tmpFocusScript + tmpScript
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSearch", tmpScript, True)
            End If
        End If

        ' 加盟店検索実行後処理実行
        kameitenSearchAfter_ServerClick(sender, e, blnTorikesi)

    End Sub

    ''' <summary>
    ''' 加盟店検索実行後処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="blnTorikesi">取消対象フラグ</param>
    ''' <remarks></remarks>
    Protected Sub kameitenSearchAfter_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal blnTorikesi As Boolean)

        '加盟店検索実行後処理(加盟店詳細情報、ビルダー情報取得)
        Dim kLogic As New KameitenSearchLogic
        Dim recData As KameitenSearchRecord = kLogic.GetKameitenSearchResult(Me.HiddenKubun.Value, Me.TextKameitenCd.Value, "", blnTorikesi)
        Dim strErrMsg As String = String.Empty

        If Me.TextKameitenCd.Value <> String.Empty Then    '入力
            If Not recData Is Nothing AndAlso recData.KameitenCd <> String.Empty Then
                '加盟店情報をセット
                Me.SetKameitenInfo(recData)
            Else
                'クリアを行なう
                ClearKameitenInfo(False)
            End If
        Else    '未入力
            ClearKameitenInfo()
        End If

        'NG情報設定共通処理
        SetNGInfo()

    End Sub

    ''' <summary>
    ''' 加盟店情報をセットする
    ''' </summary>
    ''' <param name="recData"></param>
    ''' <remarks></remarks>
    Private Sub SetKameitenInfo(ByVal recData As KameitenSearchRecord)

        If Not recData Is Nothing AndAlso recData.KameitenCd <> String.Empty Then
            '画面に値をセット
            TextKameitenCd.Value = cl.GetDispStr(recData.KameitenCd) '加盟店・コード
            TextKameitenMei.Value = cl.GetDispStr(recData.KameitenMei1) '加盟店・名称
            Me.TextTorikesiRiyuu.Text = cl.getTorikesiRiyuu(recData.Torikesi, recData.KtTorikesiRiyuu) '加盟店・取消理由
            TextKameitenTel.Value = cl.GetDispStr(recData.TelNo) '加盟店・TEL
            TextKameitenFax.Value = cl.GetDispStr(recData.FaxNo) '加盟店・FAX
            HiddenKameitenMail.Value = cl.GetDispStr(recData.MailAddress) '加盟店・Mail
            TextBuilderNo.Value = cl.GetDispStr(recData.BuilderNo) '加盟店・ビルダーNO
            HiddenKeiretuCd.Value = cl.GetDispStr(recData.KeiretuCd) '加盟店・系列コード
            TextKeiretu.Value = cl.GetDispStr(recData.KeiretuMei) '加盟店・系列
            HiddenEigyousyoCd.Value = cl.GetDispStr(recData.EigyousyoCd) '加盟店・営業所コード
            TextEigyousyoMei.Value = cl.GetDispStr(recData.EigyousyoMei) '加盟店・営業所/法人名
            TextMitsumoriHituyou.Value = cl.GetDispStr(recData.TysMitsyoMsg) '加盟店・見積書必須メッセージ
            TextHattyuusyoHituyou.Value = cl.GetDispStr(recData.HattyuusyoMsg) '加盟店・発注書必須メッセージ
            TextTyousaKaisyaNG.Value = cl.GetDispKahiStr(recData.KahiKbn) '加盟店・NG情報メッセージ
            TextJioSakiFlg.Value = cl.GetDisplayString(IIf(recData.JioSakiFLG = 1, EarthConst.JIO_SAKI, String.Empty)) 'JIO式フラグ
            Me.TextFcTenMei.Value = cl.GetDispStr(recData.FcTenMei) 'FC店名

            '加盟店コードを退避
            Me.HiddenKameitenCdTextMae.Value = TextKameitenCd.Value

            '加盟店コード/名称/取消理由の文字色スタイル
            cl.setStyleFontColor(Me.TextKameitenCd.Style, cl.getKameitenFontColor(recData.Torikesi))
            cl.setStyleFontColor(Me.TextKameitenMei.Style, cl.getKameitenFontColor(recData.Torikesi))
            cl.setStyleFontColor(Me.TextTorikesiRiyuu.Style, cl.getKameitenFontColor(recData.Torikesi))

        Else
            'クリアを行なう
            ClearKameitenInfo(False)

        End If

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
        noTarget.Add(ButtonTyosaMitumori.ID, True) '調査見積ボタン
        noTarget.Add(ButtonHosyousyoDB.ID, True) '保証書DBボタン
        noTarget.Add(ButtonRJHS.ID, True) 'R-JHSボタン
        noTarget.Add(SelectDataHaki.ID, True) 'データ破棄種別
        noTarget.Add(changeHaki.ID, True) 'データ破棄種別用イベントダミーボタン
        noTarget.Add(ButtonKameitenTyuuijouhou.ID, True) '注意情報ボタン
        noTarget.Add(ButtonBuilderCheck.ID, True) 'ビルダー情報ボタン
        noTarget.Add(ButtonBukkenRireki.ID, True) '物件履歴ボタン

        If SelectDataHaki.SelectedValue >= "1" Then
            noTarget.Add(TextDataHakiDate.ID, TextDataHakiDate) 'データ破棄日

            '全てのコントロールを無効化()
            jBn.ChangeDesabledAll(Me, True, noTarget)
            'データ破棄日の表示を切替える
            TextDataHakiDate.Style.Remove("display")
        Else
            '全てのコントロールを有効化()
            jBn.ChangeDesabledAll(Me, False, noTarget)
            'データ破棄日の表示を切替える
            TextDataHakiDate.Style("display") = "none"
        End If

        setFocusAJ(SelectDataHaki) 'フォーカス

    End Sub

    ''' <summary>
    ''' データ破棄コンボ変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub changeHaki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TextDataHakiDate.Value = String.Empty Then
            TextDataHakiDate.Value = Format(Today, "yyyy/MM/dd")
        End If
        '画面表示状態を再設定
        setDispAction()

        If SelectDataHaki.SelectedValue >= "1" Then
            RaiseEvent OyaGamenAction_hensyu(sender, e, True)
        Else
            RaiseEvent OyaGamenAction_hensyu(sender, e, False)
        End If

    End Sub

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <remarks></remarks>
    Public Function checkInput(Optional ByVal flgNextGamen As Boolean = False) As Boolean
        Dim e As New System.EventArgs

        '地盤画面共通クラス
        Dim jBn As New Jiban

        '全てのコントロールを有効化(破棄種別チェック用)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        'エラーメッセージ初期化
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New ArrayList

        If SelectDataHaki.SelectedValue >= "1" Then
            '破棄種別が選択されている場合、スルー

        Else

            'コード値チェック
            '変更後検索ボタン押下チェック
            If (Me.TextKameitenCd.Value <> Me.HiddenKameitenCdTextMae.Value) Or _
                (Me.TextKameitenCd.Value <> String.Empty And Me.TextKameitenMei.Value = String.Empty) Then
                errMess += Messages.MSG030E.Replace("@PARAM1", "加盟店コード")
                arrFocusTargetCtrl.Add(TextKameitenCd)
            End If

            '●必須チェック
            '<共通情報>
            '施主名
            If TextSesyuMei.Value.Length = 0 Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "施主名")
                arrFocusTargetCtrl.Add(TextSesyuMei)
            End If
            '物件住所1
            If TextBukkenJyuusyo1.Value.Length = 0 Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "物件住所1")
                arrFocusTargetCtrl.Add(TextBukkenJyuusyo1)
            End If
            '加盟店
            If String.IsNullOrEmpty(Me.TextKameitenCd.Value) Then
                errMess += Messages.MSG013E.Replace("@PARAM1", "加盟店コード")
                arrFocusTargetCtrl.Add(TextKameitenCd)
            End If

        End If

        '●禁則文字チェック(文字列入力フィールドが対象)
        '受理詳細
        If jBn.KinsiStrCheck(TextSesyuMei.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "施主名")
            arrFocusTargetCtrl.Add(TextSesyuMei)
        End If
        '物件住所1
        If jBn.KinsiStrCheck(TextBukkenJyuusyo1.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件住所1")
            arrFocusTargetCtrl.Add(TextBukkenJyuusyo1)
        End If
        '物件住所2
        If jBn.KinsiStrCheck(TextBukkenJyuusyo2.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件住所2")
            arrFocusTargetCtrl.Add(TextBukkenJyuusyo2)
        End If
        '物件住所3
        If jBn.KinsiStrCheck(TextBukkenJyuusyo3.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "物件住所3")
            arrFocusTargetCtrl.Add(TextBukkenJyuusyo3)
        End If
        '備考
        If jBn.KinsiStrCheck(TextAreaBikou.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "備考")
            arrFocusTargetCtrl.Add(TextAreaBikou)
        End If
        '備考2
        If jBn.KinsiStrCheck(TextAreaBikou2.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", "備考2")
            arrFocusTargetCtrl.Add(TextAreaBikou2)
        End If

        '改行変換(備考)
        If TextAreaBikou.Value <> "" Then
            TextAreaBikou.Value = TextAreaBikou.Value.Replace(vbCrLf, " ")
        End If
        If TextAreaBikou2.Value <> "" Then
            TextAreaBikou2.Value = TextAreaBikou2.Value.Replace(vbCrLf, " ")
        End If

        '●バイト数チェック(文字列入力フィールドが対象)
        '施主名
        If jBn.ByteCheckSJIS(TextSesyuMei.Value, TextSesyuMei.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "施主名")
            arrFocusTargetCtrl.Add(TextSesyuMei)
        End If
        '物件住所1
        If jBn.ByteCheckSJIS(TextBukkenJyuusyo1.Value, TextBukkenJyuusyo1.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "物件住所1")
            arrFocusTargetCtrl.Add(TextBukkenJyuusyo1)
        End If
        '物件住所2
        If jBn.ByteCheckSJIS(TextBukkenJyuusyo2.Value, TextBukkenJyuusyo2.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "物件住所2")
            arrFocusTargetCtrl.Add(TextBukkenJyuusyo2)
        End If
        '物件住所3
        If jBn.ByteCheckSJIS(TextBukkenJyuusyo3.Value, TextBukkenJyuusyo3.MaxLength) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "物件住所3")
            arrFocusTargetCtrl.Add(TextBukkenJyuusyo3)
        End If
        '備考
        If jBn.ByteCheckSJIS(TextAreaBikou.Value, 256) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "備考")
            arrFocusTargetCtrl.Add(TextAreaBikou)
        End If
        '備考2
        If jBn.ByteCheckSJIS(TextAreaBikou2.Value, 512) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", "備考2")
            arrFocusTargetCtrl.Add(TextAreaBikou2)
        End If

        '●桁数チェック
        '対象なし

        '●日付チェック
        '対象なし

        '●その他チェック
        '対象なし

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

#Region "(3) NG情報設定共通処理"
    ''' <summary>
    ''' NG情報設定共通処理
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetNGInfo()
        '調査会社NG
        SetTyousaKaisyaNG()
        '判定NG
        SetHanteiNG(HiddenHanteiCd1.Value, HiddenHanteiCd2.Value)
        '工事会社NG
        SetKoujiKaisyaNG(HiddenKojGaisyaCd.Value, HiddenTKojKaisyaCd.Value)

    End Sub

    ''' <summary>
    ''' 調査会社NG情報(表示切替のみ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetTyousaKaisyaNG()
        ' 調査会社NG設定
        If TextTyousaKaisyaNG.Value <> "" Then
            '表示
            TextTyousaKaisyaNG.Style.Remove("display")
        Else
            '非表示
            TextTyousaKaisyaNG.Style("display") = "none"
        End If
    End Sub

    ''' <summary>
    ''' 判定NG情報
    ''' </summary>
    ''' <param name="strHanteiCd1">判定コード1</param>
    ''' <param name="strHanteiCd2">判定コード2</param>
    ''' <remarks></remarks>
    Public Sub SetHanteiNG(ByVal strHanteiCd1 As String, ByVal strHanteiCd2 As String)

        If strHanteiCd1 = "" And strHanteiCd2 = "" Then
            TextHanteiNG.Value = ""
            '非表示
            TextHanteiNG.Style("display") = "none"
            Exit Sub
        End If

        Dim kisosiyouSearchLogic As New KisoSiyouLogic
        Dim blnTorikesi As Boolean = False
        Dim dataArray1 As New List(Of KisoSiyouRecord)
        Dim dataArray2 As New List(Of KisoSiyouRecord)
        Dim intCount1 As Integer = 0
        Dim intCount2 As Integer = 0
        Dim intFlg As Integer = 0

        '1.画面の設定
        '判定1
        If strHanteiCd1 <> "" Then
            dataArray1 = kisosiyouSearchLogic.GetKisoSiyouSearchResult( _
                                    strHanteiCd1 _
                                    , "" _
                                    , TextKameitenCd.Value _
                                    , True _
                                    , blnTorikesi _
                                    , intCount1 _
                                    )
        End If
        If intCount1 = 1 Then
            Dim recData1 As KisoSiyouRecord = dataArray1(0)
            ' 調査会社NG設定
            If recData1.KahiKbn = 9 Then
                TextHanteiNG.Value = EarthConst.HANTEI_NG
                '表示
                TextHanteiNG.Style.Remove("display")

                intFlg = 1
            Else
                TextHanteiNG.Value = ""
                '非表示
                TextHanteiNG.Style("display") = "none"
            End If
        End If

        '判定2
        If strHanteiCd2 <> "" Then
            dataArray2 = kisosiyouSearchLogic.GetKisoSiyouSearchResult( _
                                    strHanteiCd2 _
                                    , "" _
                                    , TextKameitenCd.Value _
                                    , True _
                                    , blnTorikesi _
                                    , intCount2 _
                                    )
        End If

        If intCount2 = 1 Then
            Dim recData2 As KisoSiyouRecord = dataArray2(0)
            ' 調査会社NG設定
            If recData2.KahiKbn = 9 Then
                TextHanteiNG.Value = EarthConst.HANTEI_NG
                '表示
                TextHanteiNG.Style.Remove("display")
            ElseIf intFlg = 1 Then
            Else
                TextHanteiNG.Value = ""
                '非表示
                TextHanteiNG.Style("display") = "none"
            End If
        End If
    End Sub

    ''' <summary>
    ''' 工事会社NG情報
    ''' </summary>
    ''' <param name="strKoujiKaisyaCd">工事会社コード</param>
    ''' <param name="strTuikaKoujiKaisyaCd">追加工事会社コード</param>
    ''' <remarks>
    ''' 工事会社(調査会社ｺｰﾄﾞ,事業所ｺｰﾄﾞ)
    ''' 追加工事会社(調査会社ｺｰﾄﾞ,事業所ｺｰﾄﾞ)
    ''' </remarks>
    Public Sub SetKoujiKaisyaNG(ByVal strKoujiKaisyaCd As String, ByVal strTuikaKoujiKaisyaCd As String)
        If strKoujiKaisyaCd = "" Then
            TextKoujiKaisyaNG.Value = ""
            '非表示
            TextKoujiKaisyaNG.Style("display") = "none"
            Exit Sub
        End If

        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim blnTorikesi As Boolean = False
        Dim recData1 As New KameitenSearchRecord
        Dim recData2 As New KameitenSearchRecord
        Dim intFlg As Integer = 0

        '***********
        '* 改良工事
        '***********
        recData1 = kameitenSearchLogic.GetKoujiKaisyaNGResult( _
                                    HiddenKubun.Value _
                                    , TextKameitenCd.Value _
                                    , strKoujiKaisyaCd _
                                    , True _
                                    )

        If Not recData1 Is Nothing Then

            ' 調査会社NG設定
            If recData1.KahiKbn = 9 Then
                TextKoujiKaisyaNG.Value = EarthConst.KOUJI_KAISYA_NG
                '表示
                TextKoujiKaisyaNG.Style.Remove("display")

                intFlg = 1
            Else
                TextKoujiKaisyaNG.Value = ""
                '非表示
                TextKoujiKaisyaNG.Style("display") = "none"
            End If
        Else
            TextKoujiKaisyaNG.Value = ""
        End If


        '***********
        '* 追加工事
        '***********
        recData2 = kameitenSearchLogic.GetKoujiKaisyaNGResult( _
                                    HiddenKubun.Value _
                                    , TextKameitenCd.Value _
                                    , strTuikaKoujiKaisyaCd _
                                    , True _
                                    )

        If Not recData2 Is Nothing Then

            ' 調査会社NG設定
            If recData2.KahiKbn = 9 Then
                TextKoujiKaisyaNG.Value = EarthConst.KOUJI_KAISYA_NG
                '表示
                TextKoujiKaisyaNG.Style.Remove("display")

            ElseIf intFlg = 1 Then
            Else
                TextKoujiKaisyaNG.Value = ""
                '非表示
                TextKoujiKaisyaNG.Style("display") = "none"
            End If
        Else
            TextKoujiKaisyaNG.Value = ""
        End If

    End Sub

#End Region

    ''' <summary>
    ''' Ajax動作時のフォーカスセット
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub setFocusAJ(ByVal ctrl As Control)
        'フォーカスセット
        masterAjaxSM.SetFocus(ctrl)
    End Sub

End Class