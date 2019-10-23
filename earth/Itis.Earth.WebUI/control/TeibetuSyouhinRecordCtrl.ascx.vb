
Partial Public Class TeibetuSyouhinRecordCtrl
    Inherits System.Web.UI.UserControl

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private masterAjaxSM As New ScriptManager

    Private jSM As New JibanSessionManager

    Private cBizLogic As New CommonBizLogic
    Private cLogic As New CommonLogic


#Region "コントロールの表示モード"
    ''' <summary>
    ''' コントロールの表示モード
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DisplayMode
        ''' <summary>
        ''' 商品１
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN1 = 1
        ''' <summary>
        ''' 商品２
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN2 = 2
        ''' <summary>
        ''' 商品３
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN3 = 3
        ''' <summary>
        ''' 報告書
        ''' </summary>
        ''' <remarks></remarks>
        HOUKOKUSYO = 4
        ''' <summary>
        ''' 保証
        ''' </summary>
        ''' <remarks></remarks>
        HOSYOU = 5
        ''' <summary>
        ''' 保証(解約払戻)
        ''' </summary>
        ''' <remarks></remarks>
        KAIYAKU = 6
    End Enum
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
    ''' 行のcssクラス名
    ''' </summary>
    ''' <remarks></remarks>
    Private _cssName As String
    ''' <summary>
    ''' 行のcssクラス名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CssName() As String
        Get
            Return _cssName
        End Get
        Set(ByVal value As String)
            _cssName = value
        End Set
    End Property

    ''' <summary>
    ''' コントロールの表示設定
    ''' </summary>
    ''' <remarks></remarks>
    Private _enabled As Boolean
    ''' <summary>
    ''' コントロールの表示設定
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Enabled() As Boolean
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            _enabled = value
            ' テキストボックス
            TextHattyuusyoKakuninbi.Enabled = _enabled
            TextHattyuusyoKingaku.Enabled = _enabled
            TextJituSeikyuuGaku.Enabled = _enabled
            TextSyouhizeiGaku.Enabled = _enabled
            TextSiireSyouhizeiGaku.Enabled = _enabled
            TextKoumutenSeikyuuGaku.Enabled = _enabled
            TextMitumorisyoSakuseibi.Enabled = _enabled
            TextSeikyuusyoHakkoubi.Enabled = _enabled
            TextSyoudakusyoKingaku.Enabled = _enabled
            TextUriageBi.Enabled = _enabled
            TextUriageNengappi.Enabled = _enabled
            TextDenpyouUriageNengappi.Enabled = _enabled
            TextDenpyouSiireNengappi.Enabled = _enabled
            ' ドロップダウンリスト
            SelectSeikyuuUmu.Enabled = _enabled
            SelectHattyuusyoKakutei.Enabled = _enabled
            SelectUriageSyori.Enabled = _enabled
        End Set
    End Property

    ''' <summary>
    ''' 請求有無の表示設定
    ''' </summary>
    ''' <remarks></remarks>
    Private _enableSeikyuuUmu As Boolean
    ''' <summary>
    ''' 請求有無の表示設定
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EnableSeikyuuUmu() As Boolean
        Get
            Return _enableSeikyuuUmu
        End Get
        Set(ByVal value As Boolean)
            _enableSeikyuuUmu = value
            ' ドロップダウンリスト
            SelectSeikyuuUmu.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' 行の表示設定
    ''' </summary>
    ''' <remarks></remarks>
    Private _rowVisible As Boolean = True
    ''' <summary>
    ''' 行の表示設定
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RowVisible() As Boolean
        Get
            Return _rowVisible
        End Get
        Set(ByVal value As Boolean)
            _rowVisible = value
        End Set
    End Property

    ''' <summary>
    ''' 空白行の表示設定コントロール行の下に空白行を設定するか
    ''' </summary>
    ''' <remarks></remarks>
    Private _isRowSpacer As Boolean = False
    ''' <summary>
    ''' 空白行の表示設定コントロール行の下に空白行を設定するか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsRowSpacer() As Boolean
        Get
            Return _isRowSpacer
        End Get
        Set(ByVal value As Boolean)
            _isRowSpacer = value
        End Set
    End Property

    ''' <summary>
    ''' 邸別請求レコード
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _teibetuSeikyuuRec As TeibetuSeikyuuRecord
    ''' <summary>
    ''' 邸別請求レコード
    ''' </summary>
    ''' <value>ＤＢより取得した邸別請求レコード</value>
    ''' <returns>画面内容より設定した邸別請求レコード</returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property TeibetuSeikyuuRec() As TeibetuSeikyuuRecord
        Get
            Return GetCtrlData()
        End Get
        Set(ByVal value As TeibetuSeikyuuRecord)
            _teibetuSeikyuuRec = value

            If Not _teibetuSeikyuuRec Is Nothing Then
                ' コントロールにデータをセット
                SetCtrlData(_teibetuSeikyuuRec)
            End If

        End Set
    End Property

#Region "邸別データ共通設定情報"
    ''' <summary>
    ''' 邸別データ共通設定情報
    ''' </summary>
    ''' <value></value>


    Public Property SettingInfo() As TeibetuSettingInfoRecord
        Get
            Dim info As New TeibetuSettingInfoRecord
            info.Kubun = HiddenKubun.Value                                                  ' 区分
            info.Bangou = HiddenBangou.Value                                                ' 番号（保証書NO）
            info.UpdLoginUserId = HiddenLoginUserId.Value                                   ' ログインユーザーID
            info.KeiriGyoumuKengen = Integer.Parse(HiddenKeiriGyoumuKengen.Value)           ' 経理業務権限
            info.HattyuusyoKanriKengen = Integer.Parse(HiddenHattyuusyoKanriKengen.Value)   ' 発注書管理権限
            Return info
        End Get
        Set(ByVal value As TeibetuSettingInfoRecord)
            HiddenKubun.Value = value.Kubun                                 ' 区分
            HiddenBangou.Value = value.Bangou                               ' 番号（保証書NO）
            HiddenLoginUserId.Value = value.UpdLoginUserId                  ' ログインユーザーID
            ' 経理業務権限
            HiddenKeiriGyoumuKengen.Value = value.KeiriGyoumuKengen.ToString()
            ' 発注書管理権限
            HiddenHattyuusyoKanriKengen.Value = value.HattyuusyoKanriKengen.ToString()
        End Set
    End Property
#End Region

    ''' <summary>
    ''' 画面表示NO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GamenHyoujiNo() As String
        Get
            Return HiddenGamenHyoujiNo.Value
        End Get
        Set(ByVal value As String)
            HiddenGamenHyoujiNo.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 請求タイプ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SeikyuuType() As String
        Get
            Return HiddenSeikyuuType.Value
        End Get
        Set(ByVal value As String)
            HiddenSeikyuuType.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KeiretuCd() As String
        Get
            Return HiddenKeiretuCd.Value
        End Get
        Set(ByVal value As String)
            HiddenKeiretuCd.Value = value
        End Set
    End Property

    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KameitenCd() As String
        Get
            Return HiddenKameitenCd.Value
        End Get
        Set(ByVal value As String)
            HiddenKameitenCd.Value = value
        End Set
    End Property

    ''' <summary>
    ''' UpdatePanel
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UpdateSyouhinPanel() As UpdatePanel
        Get
            Return UpdatePanelSyouhinRec
        End Get
        Set(ByVal value As UpdatePanel)
            UpdatePanelSyouhinRec = value
        End Set
    End Property

    ''' <summary>
    ''' 商品１自動設定用レコード
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AutoSetSyouhinRecord() As Syouhin1AutoSetRecord
        Set(ByVal value As Syouhin1AutoSetRecord)

            Dim strZeiKbn As String = String.Empty      '税区分
            Dim strZeiritu As String = String.Empty     '税率
            Dim strSyouhinCd As String = String.Empty   '商品コード

            ' 値が取得できた場合、コントロールに設定する。取得できない場合データを全てクリアする
            If value Is Nothing Then
                EnabledCtrl(False)
            Else

                '売上年月日で判断して、正しい税率を取得する
                strSyouhinCd = value.SyouhinCd
                If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                    '取得した税区分・税率をセット
                    value.ZeiKbn = strZeiKbn
                    value.Zeiritu = strZeiritu
                End If

                TextSyouhinCd.Text = value.SyouhinCd                    ' 商品コード
                HiddenBunruiCd.Value = EarthConst.SOUKO_CD_SYOUHIN_1    ' 分類コード
                HiddenSyouhinCdOld.Value = value.SyouhinCd              ' 商品コード(変更チェック用)
                SpanSyouhinName.Text = value.SyouhinNm                  ' 商品名
                HiddenZeiKbn.Value = value.ZeiKbn                       ' 税区分
                HiddenZeiritu.Value = value.Zeiritu                     ' 税率

                '請求有無＝有りの場合、請求金額をセット
                If SelectSeikyuuUmu.SelectedValue = "1" And _
                    value.SetSts <> EarthEnum.emSyouhin1Error.GetHanbai And _
                    value.SetSts <> EarthEnum.emSyouhin1Error.GetGenkaHanbai Then
                    '工務店請求額
                    TextKoumutenSeikyuuGaku.Text = value.KoumutenGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                    '実請求額
                    TextJituSeikyuuGaku.Text = value.JituGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                    SetZeigaku(Nothing)                                        ' 税額・税込額の設定
                End If

                ' 仕入消費税額設定
                SetSiireZeigaku()
                ' 税込金額変更を親コントロールに通知する
                Dim e As New System.EventArgs
                RaiseEvent ChangeZeikomiGaku(Me, e, CInt(IIf(TextZeikomiKingaku.Text = "", "0", TextZeikomiKingaku.Text)))

                '活性化制御
                EnabledCtrl(True)
                End If

        End Set
    End Property

#Region "商品コード"
    ''' <summary>
    ''' 商品コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品コード</returns>
    ''' <remarks></remarks>
    Public Property SyouhinCd() As String
        Get
            Return TextSyouhinCd.Text
        End Get
        Set(ByVal value As String)
            TextSyouhinCd.Text = value
        End Set
    End Property
#End Region

#Region "税込金額"
    ''' <summary>
    ''' 税込金額
    ''' </summary>
    ''' <value></value>


    Public ReadOnly Property ZeikomiKingaku() As Integer
        Get
            Dim strZeikomi As String = IIf(TextZeikomiKingaku.Text.Replace(",", "").Trim() = "", _
                                           "0", _
                                           TextZeikomiKingaku.Text.Replace(",", "").Trim())
            Return Integer.Parse(strZeikomi)
        End Get
    End Property
#End Region

#Region "発注書金額"
    ''' <summary>
    ''' 発注書金額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property HattyuusyoKingaku() As String
        Get
            Dim strHattyuu As String = IIf(TextHattyuusyoKingaku.Text.Replace(",", "").Trim() = "", _
                                           "0", _
                                           TextHattyuusyoKingaku.Text.Replace(",", "").Trim())
            Return strHattyuu
        End Get
    End Property
#End Region

#Region "分類コード"
    ''' <summary>
    ''' 分類コード
    ''' </summary>
    ''' <value>分類コード</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BunruiCd() As String
        Get
            Return HiddenBunruiCd.Value
        End Get
        Set(ByVal value As String)
            HiddenBunruiCd.Value = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' 調査方法
    ''' </summary>
    ''' <remarks></remarks>
    Private _tysHouhou As String
    ''' <summary>
    ''' 調査方法
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tysHouhou() As String
        Get
            Return _tysHouhou
        End Get
        Set(ByVal value As String)
            _tysHouhou = value
        End Set
    End Property

    ''' <summary>
    ''' 請求先・仕入先リンクの外部アクセス用
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSeikyuuSiireLink() As SeikyuuSiireLinkCtrl
        Get
            Return SeikyuuSiireLinkCtrl
        End Get
        Set(ByVal value As SeikyuuSiireLinkCtrl)
            SeikyuuSiireLinkCtrl = value
        End Set
    End Property

    ''' <summary>
    ''' 特別対応ツールチップの外部アクセス用
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouToolTip() As TokubetuTaiouToolTipCtrl
        Get
            Return TokubetuTaiouToolTipCtrl
        End Get
        Set(ByVal value As TokubetuTaiouToolTipCtrl)
            TokubetuTaiouToolTipCtrl = value
        End Set
    End Property

    ''' <summary>
    ''' 特別対応データ更新フラグの外部アクセス用
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTokubetuTaiouUpdFlg() As HiddenField
        Get
            Return HiddenTokubetuTaiouUpdFlg
        End Get
        Set(ByVal value As HiddenField)
            HiddenTokubetuTaiouUpdFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 売上処理プルダウンの外部アクセス用
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccUriageSyori() As DropDownList
        Get
            Return SelectUriageSyori
        End Get
        Set(ByVal value As DropDownList)
            SelectUriageSyori = value
        End Set
    End Property
#End Region

#Region "イベント"

    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント<br/>
    ''' 実請求額、工務店請求額を親画面へ渡す
    ''' </summary>
    ''' <remarks></remarks>
    Public Event KingakuSetAction(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs, _
                                ByVal jituSeikyuuGaku As String, _
                                ByVal koumutenSeikyuuGaku As String)

    ''' <summary>
    ''' 依頼コントロールの価格設定ロジックを親画面に実行させる
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ExecKakakuSettei(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs)

    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント（請求タイプ設定）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">カスタムイベント発生元ID</param>
    ''' <param name="strSeikyuuSakiTypeStr">請求先タイプ（直接請求/他請求）</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <remarks></remarks>
    Public Event SetSeikyuuTypeAction(ByVal sender As System.Object _
                                        , ByVal e As System.EventArgs _
                                        , ByVal strId As String _
                                        , ByVal strSeikyuuSakiTypeStr As String _
                                        , ByVal strKeiretuCd As String _
                                        , ByVal strKameitenCd As String)

    ''' <summary>
    ''' 依頼コントロールの調査方法をプロパティに設定する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="TyousaHouhouNo">調査方法No</param>
    ''' <remarks></remarks>
    Public Event SetTysHouhouAction(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs, _
                                 ByRef TyousaHouhouNo As Integer)

    ''' <summary>
    ''' 商品２，３の親コントロールに請求先情報の設定を要求する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event GetSeikyuuInfo(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs)

    ''' <summary>
    ''' 税込金額の変更を親コントロールに通知する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ChangeZeikomiGaku(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs, _
                                   ByVal zeikomigaku As Integer)

    ''' <summary>
    ''' 売上年月日の変更を親コントロールに通知する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ChangeUriageNengappi(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal uriagenengappi As String)

    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント（請求先・仕入先情報設定用）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">画面ID</param>
    ''' <remarks></remarks>
    Public Event SetSeikyuuSiireSakiAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)

    ''' <summary>
    ''' ページロード時のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        If Not IsPostBack Then

            ' プロパティの設定値を反映
            SyouhinRecord.Attributes("class") = _cssName

            If _isRowSpacer Then
                TableSpacer.Style("display") = "inline"
            Else
                TableSpacer.Style("display") = "none"
            End If

            If _rowVisible = False Then
                SyouhinRecord.Style("display") = "none"
            End If

            ' モードを保持する
            HiddenDispMode.Value = Me.DispMode

            ' 非表示項目にデフォルト値を設定
            If HiddenSeikyuuType.Value = String.Empty Then
                HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu
            End If

            ' ドロップダウンリストに設定するアイテム
            Dim itemBlank As New ListItem   ' 空白
            Dim itemAri As New ListItem     ' 有り
            Dim itemNasi As New ListItem    ' 無し

            itemBlank.Text = ""
            itemBlank.Value = ""
            itemAri.Text = "有"
            itemAri.Value = "1"
            itemNasi.Text = "無"
            itemNasi.Value = "0"

            ' 請求有無を保存する
            Dim saveSeikyuuUmu As String = SelectSeikyuuUmu.SelectedValue

            ' 表示モードで請求有無ドロップダウンを構築するのでアイテムを削除する
            SelectSeikyuuUmu.Items.Clear()

            TdSyoudakusyoKingaku.Attributes("class") = "boldBorderLeft"
            ' 画面表示設定
            Select Case Me.DispMode
                Case DisplayMode.SYOUHIN1
                    ' 商品１の場合
                    '非表示設定
                    TextSyouhinCd.ReadOnly = True
                    TextSyouhinCd.BackColor = Drawing.Color.Transparent     ' 背景を透過させる
                    TextSyouhinCd.Style("border-style") = "none"            ' 商品コードを表示スタイルに変更
                    TextSyouhinCd.Attributes("class") = "readOnlyStyle"     ' 商品コードを表示スタイルに変更
                    TextSyouhinCd.TabIndex = -1                             ' タブフォーカス無し
                    ButtonSyouhinKensaku.Style("display") = "none"          ' 商品検索ボタン
                    SelectKakutei.Style("display") = "none"                 ' 確定

                    ' 請求有無ドロップダウンアイテム設定
                    SelectSeikyuuUmu.Items.Add(itemAri)
                    SelectSeikyuuUmu.Items.Add(itemNasi)
                    SelectSeikyuuUmu.SelectedValue = saveSeikyuuUmu

                Case DisplayMode.SYOUHIN2
                    ' 商品２の場合
                    SelectKakutei.Style("display") = "none"                 ' 確定
                    HiddenTargetId.Value = "2"                              ' 商品検索用のID
                    ' 請求有無ドロップダウンアイテム設定
                    SelectSeikyuuUmu.Items.Add(itemAri)
                    SelectSeikyuuUmu.Items.Add(itemNasi)
                    SelectSeikyuuUmu.SelectedValue = saveSeikyuuUmu

                Case DisplayMode.SYOUHIN3
                    ' 商品３の場合
                    HiddenTargetId.Value = "3"                              ' 商品検索用のID
                    ' 請求有無ドロップダウンアイテム設定
                    SelectSeikyuuUmu.Items.Add(itemAri)
                    SelectSeikyuuUmu.Items.Add(itemNasi)
                    SelectSeikyuuUmu.SelectedValue = saveSeikyuuUmu

                Case DisplayMode.HOUKOKUSYO
                    ' 報告書の場合
                    '非表示設定
                    TextSyouhinCd.ReadOnly = True
                    TextSyouhinCd.BackColor = Drawing.Color.Transparent     ' 背景を透過させる
                    TextSyouhinCd.Style("border-style") = "none"            ' 商品コードを表示スタイルに変更
                    TextSyouhinCd.Attributes("class") = "readOnlyStyle"     ' 商品コードを表示スタイルに変更
                    TextSyouhinCd.TabIndex = -1                             ' タブフォーカス無し
                    ButtonSyouhinKensaku.Style("display") = "none"          ' 商品検索ボタン
                    SelectKakutei.Style("display") = "none"                 ' 確定
                    TdSpacer.Style("display") = "inline"                    ' 右端スペーサー

                    ' 請求有無ドロップダウンアイテム設定
                    SelectSeikyuuUmu.Items.Add(itemBlank)
                    SelectSeikyuuUmu.Items.Add(itemAri)
                    SelectSeikyuuUmu.Items.Add(itemNasi)
                    SelectSeikyuuUmu.SelectedValue = saveSeikyuuUmu

                    ' 承諾書金額、伝票仕入年月日を非表示にする
                    TdSyoudakusyoKingaku.Visible = False
                    TdDenpyouSiireNengappi.Visible = False


                Case DisplayMode.HOSYOU
                    ' 保証の場合
                    '非表示設定
                    TextSyouhinCd.ReadOnly = True
                    TextSyouhinCd.BackColor = Drawing.Color.Transparent     ' 背景を透過させる
                    TextSyouhinCd.Style("border-style") = "none"            ' 商品コードを表示スタイルに変更
                    TextSyouhinCd.Attributes("class") = "readOnlyStyle"     ' 商品コードを表示スタイルに変更
                    TextSyouhinCd.TabIndex = -1                             ' タブフォーカス無し
                    ButtonSyouhinKensaku.Style("display") = "none"          ' 商品検索ボタン
                    SelectKakutei.Style("display") = "none"                 ' 確定
                    TdSpacer.Style("display") = "inline"                    ' 右端スペーサー

                    ' 請求有無ドロップダウンアイテム設定
                    SelectSeikyuuUmu.Items.Add(itemBlank)
                    SelectSeikyuuUmu.Items.Add(itemAri)
                    SelectSeikyuuUmu.Items.Add(itemNasi)
                    SelectSeikyuuUmu.SelectedValue = saveSeikyuuUmu

                    ' 承諾書金額、伝票仕入年月日を非表示にする
                    TdSyoudakusyoKingaku.Visible = False
                    TdDenpyouSiireNengappi.Visible = False

                Case DisplayMode.KAIYAKU
                    ' 保証(解約払戻)の場合
                    '非表示設定
                    TextSyouhinCd.ReadOnly = True
                    TextSyouhinCd.BackColor = Drawing.Color.Transparent     ' 背景を透過させる
                    TextSyouhinCd.Style("border-style") = "none"            ' 商品コードを表示スタイルに変更
                    TextSyouhinCd.Attributes("class") = "readOnlyStyle"     ' 商品コードを表示スタイルに変更
                    TextSyouhinCd.TabIndex = -1                             ' タブフォーカス無し
                    ButtonSyouhinKensaku.Style("display") = "none"          ' 商品検索ボタン
                    SpanSyouhinName.Style("display") = "none"               ' 商品名
                    SelectKakutei.Style("display") = "none"                 ' 確定
                    TdSpacer.Style("display") = "inline"                    ' 右端スペーサー

                    ' 請求有無ドロップダウンアイテム設定
                    SelectSeikyuuUmu.Items.Add(itemBlank)
                    SelectSeikyuuUmu.Items.Add(itemAri)
                    SelectSeikyuuUmu.SelectedValue = saveSeikyuuUmu

                    ' 承諾書金額、伝票仕入年月日を非表示にする
                    TdSyoudakusyoKingaku.Visible = False
                    TdDenpyouSiireNengappi.Visible = False

                    HiddenHosyouMessage.Value = "0" '解約払戻申請有無変更フラグの初期化

                Case Else

            End Select

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            Me.SetDispAction()

            RaiseEvent SetSeikyuuTypeAction(sender _
                                            , e _
                                            , Me.ClientID _
                                            , Me.SeikyuuSiireLinkCtrl.SeikyuuSakiTypeStr _
                                            , Me.KeiretuCd _
                                            , Me.KameitenCd)

            '画面表示時点の値を、Hiddenに保持(売上 変更チェック用)
            If HiddenOpenValuesUriage.Value = String.Empty Then
                HiddenOpenValuesUriage.Value = getCtrlValuesStringUriage()
            End If
            '画面表示時点の値を、Hiddenに保持(仕入 変更チェック用)
            If HiddenOpenValuesSiire.Value = String.Empty Then
                HiddenOpenValuesSiire.Value = getCtrlValuesStringSiire()
            End If
            '画面表示時点の値を、Hiddenに保持(仕入 変更チェック用)
            If Me.HiddenOpenValue.Value = String.Empty Then
                Me.HiddenOpenValue.Value = Me.getCtrlValuesStringAll()
            End If
            '画面表示時点の値を、Hiddenに保持(特別対応価格対応 変更チェック用)
            If Me.HiddenOpenValuesTokubetuTaiou.Value = String.Empty Then
                Me.HiddenOpenValuesTokubetuTaiou.Value = getCtrlValuesStringTokubetuTaiou()
            End If

        End If
    End Sub

    ''' <summary>
    ''' ページ表示直前のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        '活性化制御
        Me.EnabledCtrlKengen()
    End Sub

    ''' <summary>
    ''' 工務店請求額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextKoumutenSeikyuuGaku_TextChanded(ByVal sender As System.Object, _
                                                      ByVal e As System.EventArgs) Handles TextKoumutenSeikyuuGaku.TextChanged

        ' モードの取得
        Dim mode As Integer = Integer.Parse(HiddenDispMode.Value)
        Dim strSeikyuuTypeStr As String

        '直接請求・他請求判断
        If Me.HiddenSeikyuuType.Value = "0" Or Me.HiddenSeikyuuType.Value = "2" Then
            strSeikyuuTypeStr = EarthConst.SEIKYU_TYOKUSETU
        Else
            strSeikyuuTypeStr = EarthConst.SEIKYU_TASETU
        End If

        RaiseEvent SetSeikyuuTypeAction(sender, e, Me.ClientID, strSeikyuuTypeStr, Me.HiddenKeiretuCd.Value, Me.HiddenKameitenCd.Value)

        ' 請求先情報を取得（未設定時は直接請求の系列以外にしておく（加盟店情報は必須なので最終的には指定される））
        Dim seikyuuType As Integer = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu
        If HiddenSeikyuuType.Value.ToString.Trim() <> String.Empty Then
            seikyuuType = Integer.Parse(HiddenSeikyuuType.Value)
        End If

        Select Case mode
            Case DisplayMode.SYOUHIN1, _
                 DisplayMode.SYOUHIN2, _
                 DisplayMode.SYOUHIN3, _
                 DisplayMode.HOUKOKUSYO, _
                 DisplayMode.HOSYOU
                '***************************************
                ' 商品１・商品２・商品３ 報告書・保証書
                '***************************************

                ' 画面の工務店請求金額を数値型に変換
                Dim koumutengaku As Integer = 0
                If TextKoumutenSeikyuuGaku.Text.Trim() <> String.Empty Then
                    koumutengaku = Integer.Parse(TextKoumutenSeikyuuGaku.Text.Replace(",", ""))
                End If

                TextKoumutenSeikyuuGaku.Text = koumutengaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                ' 請求先情報
                If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
                   HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then

                    '**************************************************
                    ' 直接請求
                    '**************************************************
                    ' ※工務店金額を実請求金額に設定
                    TextKoumutenSeikyuuGaku.Text = koumutengaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                    TextJituSeikyuuGaku.Text = koumutengaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                Else
                    '**************************************************
                    ' 他請求（系列）※系列以外は入力不可の為、関係なし
                    '**************************************************
                    If HiddenKingakuFlg.Value <> "2" Then

                        ' 報告書の場合、工務店金額が０の場合、一度だけ実請求金額を自動設定する
                        If mode = DisplayMode.HOUKOKUSYO And _
                           koumutengaku = 0 And _
                           HiddenKingakuFlg.Value = "1" Then
                            ' 報告書は1度のみ
                            Exit Sub
                        End If

                        Dim logic As New JibanLogic
                        Dim zeinukiGaku As Integer = 0

                        If logic.GetSeikyuuGaku(sender, _
                                                5, _
                                                HiddenKeiretuCd.Value, _
                                                TextSyouhinCd.Text, _
                                                koumutengaku, _
                                                zeinukiGaku) Then

                            ' 実請求金額へセット
                            TextJituSeikyuuGaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                            ' 工務店請求金額と実請求額の自動設定制御判定用フラグセット
                            HiddenKingakuFlg.Value = "1"

                        End If
                    End If
                End If

                ' 商品１の場合のみ請求金額を親画面へ投げる
                If HiddenDispMode.Value = "1" Then
                    ' 親画面に変更データをセット
                    RaiseEvent KingakuSetAction(Me, e, TextJituSeikyuuGaku.Text, TextKoumutenSeikyuuGaku.Text)
                End If

                SetZeigaku(e)

                'フォーカスセット
                SetFocus(TextJituSeikyuuGaku)

            Case DisplayMode.KAIYAKU
                '***************************************
                ' 解約払戻
                '***************************************
                ' 請求先情報
                If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
                   HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then
                    ' 直接請求の場合、実請求額にセット
                    TextJituSeikyuuGaku.Text = TextKoumutenSeikyuuGaku.Text
                    SetZeigaku(e)
                End If
            Case Else
        End Select

    End Sub

    ''' <summary>
    ''' 実請求額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextJituSeikyuuGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' モードの取得
        Dim mode As Integer = Integer.Parse(HiddenDispMode.Value)

        Select Case mode
            Case DisplayMode.SYOUHIN1, _
                 DisplayMode.SYOUHIN2, _
                 DisplayMode.SYOUHIN3
                '***************************************
                ' 商品１・商品２・商品３ 
                '***************************************
                RaiseEvent GetSeikyuuInfo(Me, e)

                ' 請求先情報を取得（未設定時は直接請求の系列以外にしておく（加盟店情報は必須なので最終的には指定される））
                Dim seikyuuType As Integer

                If TextJituSeikyuuGaku.Text.Trim() = "" Then
                    SetZeigaku(e)
                    'フォーカスセット
                    SetFocus(TextDenpyouUriageNengappi)
                    Exit Sub
                End If

                Dim strSeikyuuTypeStr As String

                '直接請求・他請求判断
                If Me.HiddenSeikyuuType.Value = "0" Or Me.HiddenSeikyuuType.Value = "2" Then
                    strSeikyuuTypeStr = EarthConst.SEIKYU_TYOKUSETU
                Else
                    strSeikyuuTypeStr = EarthConst.SEIKYU_TASETU
                End If

                RaiseEvent SetSeikyuuTypeAction(sender, e, Me.ClientID, strSeikyuuTypeStr, Me.HiddenKeiretuCd.Value, Me.HiddenKameitenCd.Value)

                If HiddenSeikyuuType.Value = "" Then
                    Me.HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu
                    seikyuuType = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu
                Else
                    seikyuuType = Integer.Parse(HiddenSeikyuuType.Value)
                End If

                ' 画面の実請求金額を数値型に変換
                Dim jitugaku As Integer = 0
                If TextJituSeikyuuGaku.Text.Trim() <> String.Empty Then
                    jitugaku = Integer.Parse(TextJituSeikyuuGaku.Text.Replace(",", ""))
                End If

                TextJituSeikyuuGaku.Text = jitugaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                ' 請求先情報
                If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
                   HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then

                    '**************************************************
                    ' 直接請求
                    '**************************************************
                    ' ※実請求金額を工務店金額に設定
                    TextJituSeikyuuGaku.Text = jitugaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                    TextKoumutenSeikyuuGaku.Text = jitugaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                ElseIf HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuKeiretu Then
                    '**************************************************
                    ' 他請求（系列）※系列以外は設定なし
                    '**************************************************
                    If HiddenKingakuFlg.Value <> "1" Then

                        Dim koumutenGaku As Integer = 0
                        Dim logic As New JibanLogic

                        ' 請求額算出メソッドへの引数設定（商品１の場合のみ6,他は4）
                        Dim param As Integer = IIf(mode = DisplayMode.SYOUHIN1, 6, 4)

                        ' 請求額を算出する
                        If logic.GetSeikyuuGaku(sender, _
                                                param, _
                                                HiddenKeiretuCd.Value, _
                                                TextSyouhinCd.Text, _
                                                jitugaku, _
                                                koumutenGaku) Then

                            ' 工務店請求金額セット
                            TextKoumutenSeikyuuGaku.Text = koumutenGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

                            ' 工務店請求金額と実請求額の自動設定制御判定用フラグセット
                            HiddenKingakuFlg.Value = "2"

                        End If
                    End If
                End If

                ' 商品１の場合のみ請求金額を親画面へ投げる
                If HiddenDispMode.Value = "1" Then
                    ' 親画面に変更データをセット
                    RaiseEvent KingakuSetAction(Me, e, TextJituSeikyuuGaku.Text, TextKoumutenSeikyuuGaku.Text)
                End If

                SetZeigaku(e)

                'フォーカスセット
                SetFocus(TextSyouhizeiGaku)

            Case DisplayMode.HOUKOKUSYO, _
                 DisplayMode.HOSYOU, _
                 DisplayMode.KAIYAKU
                '***************************************
                ' 報告書・保証書・解約払戻
                '***************************************
                ' 請求先情報
                If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
                   HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then
                    '**************************************************
                    ' 直接請求
                    '**************************************************
                    ' ※工務店金額に実請求金額を設定
                    TextKoumutenSeikyuuGaku.Text = TextJituSeikyuuGaku.Text

                End If

                SetZeigaku(e)

                'フォーカスセット
                SetFocus(TextSyouhizeiGaku)

            Case Else
        End Select

    End Sub

    ''' <summary>
    ''' 消費税額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSyouhizeiGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If TextSyouhizeiGaku.Text.Trim() = "" Then
            TextSyouhizeiGaku.Text = "0"
        End If

        SetZeigaku(e, True)

        'フォーカスセット
        SetFocus(TextDenpyouUriageNengappi)

    End Sub

    ''' <summary>
    ''' 承諾書金額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSyoudakusyoKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If sender.Text.Trim() = "" Then
            sender.Text = "0"
        End If

        Dim jitugaku As Integer = Integer.Parse(sender.Text.Replace(",", ""))
        Dim zeigaku As Integer = 0
        If IsNumeric(Me.HiddenZeiritu.Value) = False Then
            Me.HiddenZeiritu.Value = "0"
        End If
        zeigaku = Fix(jitugaku * Decimal.Parse(HiddenZeiritu.Value))

        TextSiireSyouhizeiGaku.Text = zeigaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        'フォーカスセット
        SetFocus(TextDenpyouSiireNengappi)

    End Sub

    ''' <summary>
    ''' 仕入消費税額変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextSiireSyouhizeiGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If sender.Text.Trim() = "" Then
            sender.Text = "0"
        End If

        'フォーカスセット
        SetFocus(TextDenpyouSiireNengappi)

    End Sub

    ''' <summary>
    ''' 請求有無変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectSeikyuuUmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' モードの取得
        Dim mode As Integer = Integer.Parse(HiddenDispMode.Value)

        Select Case mode
            Case DisplayMode.SYOUHIN1
                '***************************************
                ' 商品１
                '***************************************
                If SelectSeikyuuUmu.SelectedValue = "1" Then
                    ' 請求有りの場合、依頼コントロールの価格設定を実行（親画面より実行）
                    RaiseEvent ExecKakakuSettei(Me, e)
                Else
                    ' 請求無しの場合
                    TextKoumutenSeikyuuGaku.Text = "0"
                    TextJituSeikyuuGaku.Text = "0"
                    SetZeigaku(e)
                End If


            Case DisplayMode.SYOUHIN2, _
                 DisplayMode.SYOUHIN3
                '***************************************
                ' 商品２・商品３
                '***************************************
                If SelectSeikyuuUmu.SelectedValue = "1" Then
                    ' 請求有りの場合、商品再検索を実行
                    ButtonSyouhinKensaku_ServerClick(sender, e)

                Else
                    ' 請求無しの場合
                    TextKoumutenSeikyuuGaku.Text = "0"
                    TextJituSeikyuuGaku.Text = "0"
                    SetZeigaku(e)
                End If

            Case DisplayMode.HOUKOKUSYO, _
                     DisplayMode.HOSYOU, _
                     DisplayMode.KAIYAKU
                '***************************************
                ' 報告書・保証書・解約払戻
                '***************************************
                SetSyouhinEtc(sender, e)

        End Select

        HiddenSeikyuuUmuOld.Value = SelectSeikyuuUmu.SelectedValue

        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

        'フォーカスセット
        SetFocus(SelectSeikyuuUmu)

    End Sub

    ''' <summary>
    ''' 売上処理変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectUriageSyori_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If SelectUriageSyori.SelectedValue = "0" Then
            ' 売上処理日をクリア
            TextUriageBi.Text = ""
        Else
            ' システム日付をセット
            TextUriageBi.Text = Date.Now.ToString("yyyy/MM/dd")
        End If

        'フォーカスセット
        SetFocus(SelectUriageSyori)

    End Sub

    ''' <summary>
    ''' 商品検索ボタン押下時のイベント（商品２・３）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSyouhinKensaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' 検索時の処理
        SearchSyouhin23(sender, e)

    End Sub

    ''' <summary>
    ''' 商品検索ボタン（非表示）押下時のイベント（商品２・３）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHiddenSyouhinKensaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' 検索時の処理
        SearchSyouhin23(sender, e, False)

    End Sub

    ''' <summary>
    ''' 発注書金額変更時のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextHattyuusyoKingaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' 発注書自動確定処理
        HattyuuAfterUpdate(sender)
        SetFocus(SelectHattyuusyoKakutei)
    End Sub

    ''' <summary>
    ''' 発注書確定ドロップダウン変更時のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub SelectHattyuusyoKakutei_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' 発注書自動確定処理
        FunCheckHKakutei(1, sender)
        SetFocus(SelectHattyuusyoKakutei)

        ' Old値に現在の値をセット
        HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue
    End Sub

    ''' <summary>
    ''' 売上年月日変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextUriageNengappi_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        '売上年月日
        If Me.TextUriageNengappi.Text <> String.Empty Then '未入力
            '伝票売上年月日
            If Me.TextDenpyouUriageNengappi.Text = String.Empty Then
                '売上年月日をセット
                Me.TextDenpyouUriageNengappi.Text = Me.TextUriageNengappi.Text
                TextDenpyouUriageNengappi_TextChanged(sender, e)
            End If
            '伝票仕入年月日
            If Me.TextDenpyouSiireNengappi.Text = String.Empty Then
                '売上年月日をセット
                Me.TextDenpyouSiireNengappi.Text = Me.TextUriageNengappi.Text
            End If

            ' 税区分・税率を再取得
            strSyouhinCd = Me.TextSyouhinCd.Text

            ' 商品コード変更時、Oldに画面.商品コードをセット
            If HiddenSyouhinCdOld.Value <> TextSyouhinCd.Text Then
                HiddenSyouhinCdOld.Value = TextSyouhinCd.Text
            End If

            If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                ' 取得した税区分をセット
                Me.HiddenZeiKbn.Value = strZeiKbn
                Me.HiddenZeiritu.Value = strZeiritu

                ' 税区分・税率が空白の場合、実請求税抜金額と承諾書金額に空白をセット
                If Me.HiddenZeiritu.Value = String.Empty OrElse Me.HiddenZeiritu.Value = String.Empty Then
                    Me.TextJituSeikyuuGaku.Text = ""
                    Me.TextSyoudakusyoKingaku.Text = ""
                End If

                ' 実請求の金額再設定
                SetZeigaku(e)
                ' 仕入消費税額設定
                SetSiireZeigaku()
                ' 税込金額変更を親コントロールに通知する
                RaiseEvent ChangeZeikomiGaku(Me, e, CInt(IIf(TextZeikomiKingaku.Text = "", "0", TextZeikomiKingaku.Text)))
            End If
        Else
            '売上年月日未入力の場合

            ' 税区分・税率を再取得

            strSyouhinCd = Me.TextSyouhinCd.Text
            If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                ' 取得した税区分をセット
                Me.HiddenZeiKbn.Value = strZeiKbn
                Me.HiddenZeiritu.Value = strZeiritu

                ' 税区分・税率が空白の場合、実請求税抜金額と承諾書金額に空白をセット
                If Me.HiddenZeiritu.Value = String.Empty OrElse Me.HiddenZeiritu.Value = String.Empty Then
                    Me.TextJituSeikyuuGaku.Text = ""
                    Me.TextSyoudakusyoKingaku.Text = ""
                End If

                ' 実請求の金額再設定
                SetZeigaku(e)
                ' 仕入消費税額設定
                SetSiireZeigaku()
                ' 税込金額変更を親コントロールに通知する
                RaiseEvent ChangeZeikomiGaku(Me, e, CInt(IIf(TextZeikomiKingaku.Text = "", "0", TextZeikomiKingaku.Text)))
            End If
        End If

        SetFocus(TextDenpyouUriageNengappi)

    End Sub

    ''' <summary>
    ''' 伝票売上年月日変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TextDenpyouUriageNengappi_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        '伝票売上年月日
        If Me.TextDenpyouUriageNengappi.Text <> String.Empty Then
            '締め日の設定
            Me.SeikyuuSiireLinkCtrl.SetSeikyuuSimeDate()
            ' 請求書発行日取得
            Dim strHakDate As String = Me.SeikyuuSiireLinkCtrl.GetSeikyuusyoHakkouDate(Me.TextDenpyouUriageNengappi.Text)
            Me.TextSeikyuusyoHakkoubi.Text = strHakDate
        Else
            Me.TextSeikyuusyoHakkoubi.Text = String.Empty
        End If

        '請求有無にフォーカス
        SetFocus(TextSeikyuusyoHakkoubi)

    End Sub

#End Region

#Region "プライベートメソッド"

    ''' <summary>
    ''' 商品２，３検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SearchSyouhin23(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs, _
                                Optional ByVal ProcType As Boolean = True)

        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim SyouhinSearchLogic As New SyouhinSearchLogic
        Dim total_row As Integer

        ' モードの取得
        Dim mode As Integer = Integer.Parse(HiddenDispMode.Value)

        ' 調査方法の取得
        Dim TyousaHouhouNo As Integer = Integer.MinValue
        RaiseEvent SetTysHouhouAction(Me, e, TyousaHouhouNo)

        '商品２，３検索を実行
        Dim dataArray As List(Of Syouhin23Record) = SyouhinSearchLogic.GetSyouhinInfo(TextSyouhinCd.Text, _
                                                                                      "", _
                                                                                      (mode = DisplayMode.SYOUHIN2), _
                                                                                      total_row, _
                                                                                      TyousaHouhouNo)

        If dataArray.Count = 1 Then
            '商品情報を画面にセット
            Dim recData As Syouhin23Record = dataArray(0)

            'フォーカスセット
            SetFocus(ButtonSyouhinKensaku)
        ElseIf ProcType = True Then
            '検索ポップアップを起動

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpScript = "callSearch('" & TextSyouhinCd.ClientID & EarthConst.SEP_STRING & _
                                        HiddenTargetId.ClientID & _
                                        "','" & UrlConst.SEARCH_SYOUHIN & "','" & _
                                        TextSyouhinCd.ClientID & "','" & _
                                        ButtonSyouhinKensaku.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)

            Exit Sub

        End If

        '商品２，３設定
        SetSyouhin23(sender, e, ProcType)

        ' 商品コードを保存
        HiddenSyouhinCdOld.Value = TextSyouhinCd.Text

        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDispAction()
        'イベントハンドラを設定
        Dim onFocusPostBackScript As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScript As String = "if(checkTempValueForOnBlur(this)){if(checkNumberAddFig(this))__doPostBack(this.id,'');}else{checkNumberAddFig(this);}"
        Dim onFocusScript As String = "removeFig(this);"
        Dim onBlurScript As String = "checkNumberAddFig(this);"
        Dim checkDate As String = "checkDate(this);"
        Dim onFocusPostBackScriptDate As String = "removeFig(this);setTempValueForOnBlur(this);"
        Dim onBlurPostBackScriptDate As String = "if(checkTempValueForOnBlur(this)){if(checkDate(this))__doPostBack(this.id,'');}else{checkDate(this);}"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '工務店請求税抜金額
        TextKoumutenSeikyuuGaku.Attributes("onfocus") = onFocusPostBackScript
        TextKoumutenSeikyuuGaku.Attributes("onblur") = onBlurPostBackScript
        TextKoumutenSeikyuuGaku.Attributes("onkeydown") = disabledOnkeydown
        '実請求税抜金額
        TextJituSeikyuuGaku.Attributes("onfocus") = onFocusPostBackScript
        TextJituSeikyuuGaku.Attributes("onblur") = onBlurPostBackScript
        TextJituSeikyuuGaku.Attributes("onkeydown") = disabledOnkeydown
        '消費税額
        TextSyouhizeiGaku.Attributes("onfocus") = onFocusPostBackScript
        TextSyouhizeiGaku.Attributes("onblur") = onBlurPostBackScript
        TextSyouhizeiGaku.Attributes("onkeydown") = disabledOnkeydown
        '商品コード
        TextSyouhinCd.Attributes("onfocus") = "setTempValueForOnBlur(this);"
        TextSyouhinCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){if(this.value=='')objEBI('" & ButtonHiddenSyouhinKensaku.ClientID & "').click();}"
        TextSyouhinCd.Attributes("onkeydown") = disabledOnkeydown
        '発注書金額
        TextHattyuusyoKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextHattyuusyoKingaku.Attributes("onblur") = onBlurPostBackScript
        TextHattyuusyoKingaku.Attributes("onkeydown") = disabledOnkeydown
        '承諾書金額
        TextSyoudakusyoKingaku.Attributes("onfocus") = onFocusPostBackScript
        TextSyoudakusyoKingaku.Attributes("onblur") = onBlurPostBackScript
        TextSyoudakusyoKingaku.Attributes("onkeydown") = disabledOnkeydown
        '仕入消費税額
        TextSiireSyouhizeiGaku.Attributes("onfocus") = onFocusPostBackScript
        TextSiireSyouhizeiGaku.Attributes("onblur") = onBlurPostBackScript
        TextSiireSyouhizeiGaku.Attributes("onkeydown") = disabledOnkeydown
        '見積書作成日
        TextMitumorisyoSakuseibi.Attributes("onblur") = checkDate
        TextMitumorisyoSakuseibi.Attributes("onkeydown") = disabledOnkeydown
        '請求書発行日
        TextSeikyuusyoHakkoubi.Attributes("onblur") = checkDate
        TextSeikyuusyoHakkoubi.Attributes("onkeydown") = disabledOnkeydown
        '売上年月日
        TextUriageNengappi.Attributes("onfocus") = onFocusPostBackScriptDate
        TextUriageNengappi.Attributes("onblur") = onBlurPostBackScriptDate
        TextUriageNengappi.Attributes("onkeydown") = disabledOnkeydown
        '伝票売上年月日
        TextDenpyouUriageNengappi.Attributes("onfocus") = onFocusPostBackScriptDate
        TextDenpyouUriageNengappi.Attributes("onblur") = onBlurPostBackScriptDate
        TextDenpyouUriageNengappi.Attributes("onkeydown") = disabledOnkeydown
        '伝票仕入年月日
        TextDenpyouSiireNengappi.Attributes("onblur") = checkDate
        TextDenpyouSiireNengappi.Attributes("onkeydown") = disabledOnkeydown
        '発注書確認日
        TextHattyuusyoKakuninbi.Attributes("onblur") = checkDate
        TextHattyuusyoKakuninbi.Attributes("onkeydown") = disabledOnkeydown

    End Sub

#Region "邸別請求レコード編集"
    ''' <summary>
    ''' 邸別請求レコードデータをコントロールにセットします
    ''' </summary>
    ''' <param name="data"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlData(ByVal data As TeibetuSeikyuuRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetCtrlData", _
                                            data)

        ' 商品コード未設定時、何もセットしない
        If data.SyouhinCd Is Nothing Then
            Return
        ElseIf data.SyouhinCd = "" Then
            Return
        End If

        ' 区分
        HiddenKubun.Value = data.Kbn
        ' 保証書NO
        HiddenBangou.Value = data.HosyousyoNo
        ' 商品コード
        TextSyouhinCd.Text = data.SyouhinCd
        HiddenSyouhinCdOld.Value = data.SyouhinCd
        ' 商品名
        SpanSyouhinName.Text = data.SyouhinMei
        ' 商品３の場合のみ確定区分
        If Me.DispMode = DisplayMode.SYOUHIN3 Then
            SelectKakutei.SelectedValue = data.KakuteiKbn
        End If
        ' 工務店請求額
        TextKoumutenSeikyuuGaku.Text = IIf(data.KoumutenSeikyuuGaku = Integer.MinValue, _
                                           0, _
                                           data.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        ' 実請求金額
        TextJituSeikyuuGaku.Text = IIf(data.UriGaku = Integer.MinValue, _
                                       0, _
                                       data.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1))

        If TextJituSeikyuuGaku.Text <> "" Then

            ' 消費税額
            TextSyouhizeiGaku.Text = IIf(data.UriageSyouhiZeiGaku = Integer.MinValue, _
                                       0, _
                                       data.UriageSyouhiZeiGaku.ToString(EarthConst.FORMAT_KINGAKU_1))

            ' 税込金額
            TextZeikomiKingaku.Text = Format(data.ZeikomiUriGaku, EarthConst.FORMAT_KINGAKU_1)

        End If

        ' 承諾書金額
        TextSyoudakusyoKingaku.Text = IIf(data.SiireGaku = Integer.MinValue, _
                                          0, _
                                          data.SiireGaku.ToString(EarthConst.FORMAT_KINGAKU_1))

        If TextSyoudakusyoKingaku.Text <> "" Then
            ' 仕入消費税額
            TextSiireSyouhizeiGaku.Text = IIf(data.SiireSyouhiZeiGaku = Integer.MinValue, _
                                       0, _
                                       data.SiireSyouhiZeiGaku.ToString(EarthConst.FORMAT_KINGAKU_1))
        End If

        ' 見積書作成日
        If Not data.TysMitsyoSakuseiDate = Nothing Then
            TextMitumorisyoSakuseibi.Text = IIf(data.TysMitsyoSakuseiDate = DateTime.MinValue, _
                                                "", _
                                                data.TysMitsyoSakuseiDate.ToString("yyyy/MM/dd"))
        End If

        ' 請求書発行日
        If Not data.SeikyuusyoHakDate = Nothing Then
            TextSeikyuusyoHakkoubi.Text = IIf(data.SeikyuusyoHakDate = DateTime.MinValue, _
                                              "", _
                                              data.SeikyuusyoHakDate.ToString("yyyy/MM/dd"))
        End If

        ' 売上年月日
        If Not data.UriDate = Nothing Then
            TextUriageNengappi.Text = IIf(data.UriDate = DateTime.MinValue, _
                                          "", _
                                          data.UriDate.ToString("yyyy/MM/dd"))
        End If

        ' 伝票売上年月日(参照用)
        If Not data.DenpyouUriDate = Nothing Then
            TextDenpyouUriageNengappiDisplay.Text = IIf(data.DenpyouUriDate = Date.MinValue, _
                                                        "", _
                                                        data.DenpyouUriDate.ToString("yyyy/MM/dd"))
        End If
        ' 伝票売上年月日(修正用)
        If Not data.DenpyouUriDate = Nothing Then
            TextDenpyouUriageNengappi.Text = IIf(data.DenpyouUriDate = Date.MinValue, _
                                                 "", _
                                                 data.DenpyouUriDate.ToString("yyyy/MM/dd"))
        End If

        ' 伝票仕入年月日(参照用)
        If Not data.DenpyouSiireDate = Nothing Then
            TextDenpyouSiireNengappiDisplay.Text = IIf(data.DenpyouSiireDate = Date.MinValue, _
                                                        "", _
                                                        data.DenpyouSiireDate.ToString("yyyy/MM/dd"))
        End If
        ' 伝票仕入年月日(修正用)
        If Not data.DenpyouSiireDate = Nothing Then
            TextDenpyouSiireNengappi.Text = IIf(data.DenpyouSiireDate = Date.MinValue, _
                                                 "", _
                                                 data.DenpyouSiireDate.ToString("yyyy/MM/dd"))
        End If

        ' 請求有無
        SelectSeikyuuUmu.SelectedValue = data.SeikyuuUmu
        HiddenSeikyuuUmuOld.Value = SelectSeikyuuUmu.SelectedValue

        ' 売上処理
        SelectUriageSyori.SelectedValue = data.UriKeijyouFlg

        ' 売上計上日
        If Not data.UriKeijyouDate = Nothing Then
            TextUriageBi.Text = IIf(data.UriKeijyouDate = DateTime.MinValue, _
                                    "", _
                                    data.UriKeijyouDate.ToString("yyyy/MM/dd"))
        End If

        ' 発注書金額
        TextHattyuusyoKingaku.Text = IIf(data.HattyuusyoGaku = Integer.MinValue, _
                                         "", _
                                         data.HattyuusyoGaku.ToString("###,###,###"))
        ' 発注書金額(画面起動時の値)
        HiddenHattyuusyoKingakuOld.Value = IIf(data.HattyuusyoGaku = Integer.MinValue, _
                                               "", _
                                               data.HattyuusyoGaku.ToString("###,###,###"))

        ' 発注書確定
        SelectHattyuusyoKakutei.SelectedValue = data.HattyuusyoKakuteiFlg

        ' Old値に現在の値をセット
        HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue

        '発注書確定済みの場合、発注書金額を編集不可に設定
        If SelectHattyuusyoKakutei.SelectedValue = "1" Then
            EnableTextBox(TextHattyuusyoKingaku, False)

            ' 発注書確定済みの場合、経理権限が無い場合、発注書確定も非活性化
            If HiddenKeiriGyoumuKengen.Value = "0" Then
                EnableDropDownList(SelectHattyuusyoKakutei, False)
            End If
        End If

        ' 発注書確認日
        If Not data.HattyuusyoKakuninDate = Nothing Then
            TextHattyuusyoKakuninbi.Text = IIf(data.HattyuusyoKakuninDate = DateTime.MinValue, _
                                               "", _
                                               data.HattyuusyoKakuninDate.ToString("yyyy/MM/dd"))
        End If

        ' 入金額
        HiddenNyuukinGaku.Value = _
            IIf(data.SiireGaku = Integer.MinValue, 0, data.UriGaku.ToString())

        ' 税率
        HiddenZeiritu.Value = _
            IIf(data.Zeiritu = Decimal.MinValue, 0, data.Zeiritu.ToString())

        ' 画面表示NO
        HiddenGamenHyoujiNo.Value = data.GamenHyoujiNo.ToString()

        ' 分類コード
        HiddenBunruiCd.Value = data.BunruiCd

        ' 税区分
        HiddenZeiKbn.Value = data.ZeiKbn

        ' 更新日時
        If data.UpdDatetime = DateTime.MinValue Then
            HiddenUpdDatetime.Value = data.AddDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)
        Else
            HiddenUpdDatetime.Value = data.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2)
        End If

        '請求先/仕入先リンクへ邸別請求レコードの値をセット
        Me.SeikyuuSiireLinkCtrl.SetSeikyuuSiireLinkFromTeibetuRec(data)

    End Sub

    ''' <summary>
    ''' 邸別請求レコードデータにコントロールの内容をセットします
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCtrlData() As TeibetuSeikyuuRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCtrlData")

        ' 商品コード未設定時、何もセットしない
        If TextSyouhinCd.Text = "" Then
            Return Nothing
        End If

        ' 邸別請求レコード
        Dim record As New TeibetuSeikyuuRecord
        ' 区分
        record.Kbn = HiddenKubun.Value
        ' 保証書NO
        record.HosyousyoNo = HiddenBangou.Value

        ' 商品コード
        record.SyouhinCd = TextSyouhinCd.Text
        ' 商品名
        record.SyouhinMei = SpanSyouhinName.Text
        ' 商品３の場合のみ確定区分
        If Me.DispMode = DisplayMode.SYOUHIN3 Then
            record.KakuteiKbn = SelectKakutei.SelectedValue
        Else
            record.KakuteiKbn = Integer.MinValue
        End If
        ' 工務店請求額
        Dim strKoumutenGaku As String = TextKoumutenSeikyuuGaku.Text.Replace(",", "").Trim()

        If strKoumutenGaku.Trim() = "" Then
            record.KoumutenSeikyuuGaku = Integer.MinValue
        Else
            record.KoumutenSeikyuuGaku = Integer.Parse(strKoumutenGaku)
        End If
        ' 実請求金額
        Dim strUriGaku As String = TextJituSeikyuuGaku.Text.Replace(",", "").Trim()
        If strUriGaku.Trim() = "" Then
            record.UriGaku = Integer.MinValue
        Else
            record.UriGaku = Integer.Parse(strUriGaku)
        End If
        ' 税率
        record.Zeiritu = Decimal.Parse(HiddenZeiritu.Value)
        ' 税区分
        record.ZeiKbn = HiddenZeiKbn.Value
        ' 消費税額
        Dim strSyouhizeiGaku As String = TextSyouhizeiGaku.Text.Replace(",", "").Trim()
        If strSyouhizeiGaku.Trim() = "" Then
            record.UriageSyouhiZeiGaku = Integer.MinValue
        Else
            record.UriageSyouhiZeiGaku = Integer.Parse(strSyouhizeiGaku)
        End If
        ' 仕入消費税額
        Dim strSiireSyouhizeiGaku As String = TextSiireSyouhizeiGaku.Text.Replace(",", "").Trim()
        If strSiireSyouhizeiGaku.Trim() = "" Then
            record.SiireSyouhiZeiGaku = Integer.MinValue
        Else
            record.SiireSyouhiZeiGaku = Integer.Parse(strSiireSyouhizeiGaku)
        End If
        ' 承諾書金額
        If Me.DispMode = DisplayMode.SYOUHIN1 Or _
           Me.DispMode = DisplayMode.SYOUHIN2 Or _
           Me.DispMode = DisplayMode.SYOUHIN3 Then
            Dim strSiireGaku As String = TextSyoudakusyoKingaku.Text.Replace(",", "").Trim()
            If strSiireGaku.Trim() = "" Then
                record.SiireGaku = Integer.MinValue
            Else
                record.SiireGaku = Integer.Parse(strSiireGaku)
            End If
        Else
            record.SiireGaku = 0
        End If
        ' 見積書作成日
        If Not TextMitumorisyoSakuseibi.Text.Trim() = "" Then
            record.TysMitsyoSakuseiDate = Date.Parse(TextMitumorisyoSakuseibi.Text)
        End If
        ' 請求書発行日
        If Not TextSeikyuusyoHakkoubi.Text.Trim() = "" Then
            record.SeikyuusyoHakDate = Date.Parse(TextSeikyuusyoHakkoubi.Text)
        End If
        ' 売上年月日
        If Not TextUriageNengappi.Text.Trim() = "" Then
            record.UriDate = Date.Parse(TextUriageNengappi.Text)
        End If
        ' 伝票売上年月日(修正用)
        If Not TextDenpyouUriageNengappi.Text.Trim() = "" Then
            record.DenpyouUriDate = Date.Parse(TextDenpyouUriageNengappi.Text)
        End If
        ' 伝票仕入年月日(修正用)
        If Not TextDenpyouSiireNengappi.Text.Trim() = "" Then
            record.DenpyouSiireDate = Date.Parse(TextDenpyouSiireNengappi.Text)
        End If
        ' 請求有無
        record.SeikyuuUmu = IIf(SelectSeikyuuUmu.SelectedValue = "1", 1, 0)
        ' 売上処理
        record.UriKeijyouFlg = IIf(SelectUriageSyori.SelectedValue = "1", 1, 0)
        ' 売上計上日
        If Not TextUriageBi.Text.Trim() = "" Then
            record.UriKeijyouDate = Date.Parse(TextUriageBi.Text)
        End If
        ' 発注書金額
        Dim strHattyuusyoGaku As String = TextHattyuusyoKingaku.Text.Replace(",", "").Trim()
        If strHattyuusyoGaku.Trim() = "" Then
            record.HattyuusyoGaku = Integer.MinValue
        Else
            record.HattyuusyoGaku = Integer.Parse(strHattyuusyoGaku)
        End If
        ' 発注書確定
        record.HattyuusyoKakuteiFlg = IIf(SelectHattyuusyoKakutei.SelectedValue = "1", 1, 0)
        ' 発注書確認日
        If Not TextHattyuusyoKakuninbi.Text.Trim() = "" Then
            record.HattyuusyoKakuninDate = Date.Parse(TextHattyuusyoKakuninbi.Text)
        End If
        ' 画面表示NO
        HiddenGamenHyoujiNo.Value = IIf(HiddenGamenHyoujiNo.Value = "", "1", HiddenGamenHyoujiNo.Value)
        record.GamenHyoujiNo = Integer.Parse(HiddenGamenHyoujiNo.Value)
        ' 分類コード
        record.BunruiCd = HiddenBunruiCd.Value

        ' 更新日時
        If HiddenUpdDatetime.Value <> "" Then
            record.UpdDatetime = DateTime.Parse(HiddenUpdDatetime.Value)
        Else
            record.UpdDatetime = DateTime.MinValue
        End If
        ' 更新者ＩＤ
        record.UpdLoginUserId = HiddenLoginUserId.Value

        '請求先/仕入先リンクの情報を邸別請求レコードへセット
        Me.SeikyuuSiireLinkCtrl.SetTeibetuRecFromSeikyuuSiireLink(record)

        Return record

    End Function
#End Region

#Region "商品２・３設定"
    ''' <summary>
    ''' 商品２・３情報の設定（商品コードが確定している状態でCallする）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="ProcType"></param>
    ''' <remarks></remarks>
    Public Sub SetSyouhin23(ByVal sender As Object, ByVal e As System.EventArgs, _
                                Optional ByVal ProcType As Boolean = True)

        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        Dim syouhinCd As String = TextSyouhinCd.Text

        If TextSyouhinCd.Text = String.Empty Then

            Dim hatyuusyoKingaku As String = IIf(TextHattyuusyoKingaku.Text = "", "0", TextHattyuusyoKingaku.Text)

            ' 発注書金額が０か空白の場合、元に戻して処理終了
            If hatyuusyoKingaku <> "0" Then

                TextSyouhinCd.Text = HiddenSyouhinCdOld.Value

                If ProcType = False Then
                    ' クリアできないメッセージ
                    ScriptManager.RegisterClientScriptBlock(sender, _
                                                            sender.GetType(), _
                                                            "alert", _
                                                            "alert('" & _
                                                            Messages.MSG010E & _
                                                            "')", True)
                End If

                SetFocus(TextSyouhinCd)

                Exit Sub
            End If

            '商品コードが空の場合、行をクリアして処理終了
            EnabledCtrl(False)

            Exit Sub
        Else
            ' コントロールを活性化
            EnabledCtrl(True)
        End If

        '加盟店コードの取得
        Me.KameitenCd = Me.SeikyuuSiireLinkCtrl.AccKameitenCd.Value

        ' 情報取得用のパラメータクラス
        Dim syouhin23_info As New Syouhin23InfoRecord

        ' 商品の基本情報を取得
        Dim syouhin23_rec As Syouhin23Record = getSyouhinInfo(IIf(mode = DisplayMode.SYOUHIN2, "2", "3"), TextSyouhinCd.Text)

        If syouhin23_rec Is Nothing Then
            '商品コードが空の場合、行をクリアして処理終了
            EnabledCtrl(False)
            Exit Sub
        End If

        ' コード、名称をセット
        TextSyouhinCd.Text = syouhin23_rec.SyouhinCd
        HiddenBunruiCd.Value = syouhin23_rec.SoukoCd
        SpanSyouhinName.Text = syouhin23_rec.SyouhinMei

        '請求先が個別に指定されている場合、デフォルトの請求先を上書き
        If Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value <> String.Empty Then
            syouhin23_rec.SeikyuuSakiCd = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value
            syouhin23_rec.SeikyuuSakiBrc = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc.Value
            syouhin23_rec.SeikyuuSakiKbn = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn.Value
        End If

        ' 邸別請求情報取得用のロジッククラス
        Dim logic As New JibanLogic

        ' 商品コード及び画面の情報をセット
        syouhin23_info.Syouhin2Rec = syouhin23_rec                                                  ' 商品の基本情報
        syouhin23_info.SeikyuuUmu = SelectSeikyuuUmu.SelectedValue                                  ' 請求有無
        syouhin23_info.HattyuusyoKakuteiFlg = Integer.Parse(SelectHattyuusyoKakutei.SelectedValue)  ' 発注書確定フラグ

        syouhin23_info.KeiretuCd = HiddenKeiretuCd.Value                 ' 系列コード
        syouhin23_info.KeiretuFlg = GetKeiretuFlg(HiddenKeiretuCd.Value) ' 系列フラグ

        If syouhin23_info.Syouhin2Rec.SyouhinCd IsNot Nothing Then

            '請求タイプの設定
            RaiseEvent SetSeikyuuTypeAction(Me _
                                            , e _
                                            , Me.ClientID _
                                            , syouhin23_info.Syouhin2Rec.SeikyuuSakiType _
                                            , syouhin23_info.KeiretuCd _
                                            , Me.KameitenCd)
        End If

        '請求先タイプ（直接請求/他請求）の設定
        syouhin23_info.Seikyuusaki = syouhin23_info.Syouhin2Rec.SeikyuuSakiType

        ' 請求レコードの取得(確実に結果が有る)
        Dim teibetu_seikyuu_rec As TeibetuSeikyuuRecord = getSyouhin23SeikyuuInfo(sender, syouhin23_info)

        '売上年月日で判断して、正しい税率を取得する
        strSyouhinCd = TextSyouhinCd.Text
        If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '取得した税区分・税率をセット
            teibetu_seikyuu_rec.ZeiKbn = strZeiKbn
            teibetu_seikyuu_rec.Zeiritu = strZeiritu
            syouhin23_info.Syouhin2Rec.Zeiritu = strZeiritu

        End If

        ' コントロールを活性化
        EnabledCtrl(True)

        ' 価格情報をセット
        TextKoumutenSeikyuuGaku.Text = teibetu_seikyuu_rec.KoumutenSeikyuuGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        TextJituSeikyuuGaku.Text = teibetu_seikyuu_rec.UriGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        TextSyouhizeiGaku.Text = (Fix(teibetu_seikyuu_rec.UriGaku * syouhin23_info.Syouhin2Rec.Zeiritu)).ToString(EarthConst.FORMAT_KINGAKU_1)
        TextZeikomiKingaku.Text = teibetu_seikyuu_rec.UriGaku + Fix(teibetu_seikyuu_rec.UriGaku * syouhin23_info.Syouhin2Rec.Zeiritu).ToString(EarthConst.FORMAT_KINGAKU_1)

        ' 邸別データ修正は承諾書金額を自動設定しない
        ' TextSyoudakusyoKingaku.Text = teibetu_seikyuu_rec.SiireGaku.ToString(EarthConst.FORMAT_KINGAKU1)
        HiddenZeiKbn.Value = teibetu_seikyuu_rec.ZeiKbn
        HiddenZeiritu.Value = syouhin23_rec.Zeiritu
        HiddenKingakuFlg.Value = ""
        If HiddenHattyuusyoKingakuOld.Value <> "1" Then
            HiddenHattyuusyoKingakuOld.Value = ""
        End If

        ' 金額再設定
        SetZeigaku(e)
        ' 承諾（仕入）税額再設定
        SetSiireZeigaku()
    End Sub

    ''' <summary>
    ''' 商品２、３画面表示用の商品情報を取得します
    ''' </summary>
    ''' <param name="syouhin_type">商品２or３</param>
    ''' <param name="syouhin_cd">商品コード</param>
    ''' <returns>邸別請求レコード</returns>
    ''' <remarks></remarks>
    Private Function getSyouhinInfo(ByVal syouhin_type As String, _
                                    ByVal syouhin_cd As String) As Syouhin23Record

        Dim syouhin23_rec As Syouhin23Record = Nothing

        ' 情報取得用のロジッククラス
        Dim logic As New JibanLogic
        Dim count As Integer = 0

        ' 調査方法の取得
        Dim TyousaHouhouNo As Integer = Integer.MinValue
        RaiseEvent SetTysHouhouAction(Me, New EventArgs, TyousaHouhouNo)

        ' 商品情報を取得する（コード指定なので１件のみ取得される）
        Dim list As List(Of Syouhin23Record) = logic.GetSyouhin23(syouhin_cd, _
                                                                  "", _
                                                                  IIf(syouhin_type = "2", EarthEnum.EnumSyouhinKubun.Syouhin2_110, EarthEnum.EnumSyouhinKubun.Syouhin3), _
                                                                  count, _
                                                                  TyousaHouhouNo, _
                                                                  KameitenCd)

        ' 取得できない場合
        If list.Count < 1 Then
            Return syouhin23_rec
        End If

        ' 取得できた場合のみセット
        syouhin23_rec = list(0)

        Return syouhin23_rec

    End Function

    ''' <summary>
    ''' 商品２、３画面表示用の邸別請求データを取得します
    ''' </summary>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <param name="syouhin23_info">商品２，３情報取得用のパラメータクラス</param>
    ''' <returns>邸別請求レコード</returns>
    ''' <remarks></remarks>
    Private Function getSyouhin23SeikyuuInfo(ByVal sender As Object, _
                                             ByVal syouhin23_info As Syouhin23InfoRecord _
                                             ) As TeibetuSeikyuuRecord

        Dim teibetu_rec As TeibetuSeikyuuRecord = Nothing

        ' 情報取得用のロジッククラス
        Dim logic As New JibanLogic

        ' 請求データの取得
        teibetu_rec = logic.GetSyouhin23SeikyuuData(sender, syouhin23_info, 1)

        Return teibetu_rec

    End Function

#End Region
    ''' <summary>
    ''' 報告書・保証書・解約払戻の商品をセットする
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="blnLinkRefresh">リンク更新判断フラグ</param>
    ''' <remarks></remarks>
    Public Sub SetSyouhinEtc(ByVal sender As System.Object, ByVal e As System.EventArgs, _
                             Optional ByVal blnLinkRefresh As Boolean = False)
        Dim enabled As Boolean = True

        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        If SelectSeikyuuUmu.SelectedValue = "" Then

            Dim hatyuusyoKingaku As String = IIf(TextHattyuusyoKingaku.Text = "", "0", TextHattyuusyoKingaku.Text)

            ' 発注書金額が０か空白の場合、元に戻して処理終了
            If hatyuusyoKingaku <> "0" Then

                SelectSeikyuuUmu.SelectedValue = HiddenSeikyuuUmuOld.Value
                ' クリアできないメッセージ
                ScriptManager.RegisterClientScriptBlock(sender, _
                                                        sender.GetType(), _
                                                        "alert", _
                                                        "alert('" & _
                                                        Messages.MSG010E & _
                                                        "')", True)
                Exit Sub
            End If

            enabled = False

            Dim hattyuuKingaku As String = IIf(TextHattyuusyoKingaku.Text = "", _
                                               "0", _
                                               TextHattyuusyoKingaku.Text)

            ' 発注金額入力済みの場合、クリアしない
            If hattyuuKingaku > "0" Then
                ' 請求有に戻す
                SelectSeikyuuUmu.SelectedValue = "1"
                enabled = True
            End If
        ElseIf SelectSeikyuuUmu.SelectedValue = "0" Then

            ' 無しは報告書保証書のみで金額を0クリアする
            TextJituSeikyuuGaku.Text = "0"
            TextKoumutenSeikyuuGaku.Text = "0"
            SetZeigaku(e)
        End If

        Dim logic As New TeibetuSyuuseiLogic
        Dim syouhinRec As New SyouhinMeisaiRecord
        Dim strKameitenCd As String = Me.SeikyuuSiireLinkCtrl.AccKameitenCd.Value

        ' 商品コードを再設定(空白以外)
        If SelectSeikyuuUmu.SelectedValue = "0" Or _
           SelectSeikyuuUmu.SelectedValue = "1" Then

            Select Case mode
                Case DisplayMode.HOUKOKUSYO
                    If HiddenBunruiCd.Value = EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo Then
                        syouhinRec = logic.GetSyouhinInfo("" _
                                                        , EarthEnum.EnumSyouhinKubun.TyousaHoukokusyo _
                                                        , strKameitenCd)
                        '売上年月日で判断して、正しい税率を取得する
                        strSyouhinCd = syouhinRec.SyouhinCd
                        If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                            '取得した税区分・税率をセット
                            syouhinRec.ZeiKbn = strZeiKbn
                            syouhinRec.Zeiritu = strZeiritu
                        End If

                        HiddenZeiritu.Value = syouhinRec.Zeiritu
                        HiddenZeiKbn.Value = syouhinRec.ZeiKbn

                    Else
                        syouhinRec = logic.GetSyouhinInfo("" _
                                                        , EarthEnum.EnumSyouhinKubun.KoujiHoukokusyo _
                                                        , strKameitenCd)
                        '売上年月日で判断して、正しい税率を取得する
                        strSyouhinCd = syouhinRec.SyouhinCd
                        If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                            '取得した税区分・税率をセット
                            syouhinRec.ZeiKbn = strZeiKbn
                            syouhinRec.Zeiritu = strZeiritu
                        End If

                        HiddenZeiritu.Value = syouhinRec.Zeiritu
                        HiddenZeiKbn.Value = syouhinRec.ZeiKbn

                    End If

                Case DisplayMode.HOSYOU
                    syouhinRec = logic.GetSyouhinInfo("" _
                                                    , EarthEnum.EnumSyouhinKubun.Hosyousyo _
                                                    , strKameitenCd)
                    '売上年月日で判断して、正しい税率を取得する
                    strSyouhinCd = syouhinRec.SyouhinCd
                    If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                        '取得した税区分・税率をセット
                        syouhinRec.ZeiKbn = strZeiKbn
                        syouhinRec.Zeiritu = strZeiritu
                    End If

                    HiddenZeiritu.Value = syouhinRec.Zeiritu
                    HiddenZeiKbn.Value = syouhinRec.ZeiKbn

                Case DisplayMode.KAIYAKU
                    syouhinRec = logic.GetSyouhinInfo("" _
                                                    , EarthEnum.EnumSyouhinKubun.Kaiyaku _
                                                    , strKameitenCd)
                    '売上年月日で判断して、正しい税率を取得する
                    strSyouhinCd = syouhinRec.SyouhinCd
                    If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
                        '取得した税区分・税率をセット
                        syouhinRec.ZeiKbn = strZeiKbn
                        syouhinRec.Zeiritu = strZeiritu
                    End If

                    HiddenZeiritu.Value = syouhinRec.Zeiritu
                    HiddenZeiKbn.Value = syouhinRec.ZeiKbn

                    ' 解約は標準価格部に解約払戻し価格を設定する
                    syouhinRec.HyoujunKkk = _
                        logic.GetKaiyakuKakaku(HiddenKameitenCd.Value, HiddenKubun.Value)
            End Select

            '商品名
            TextSyouhinCd.Text = syouhinRec.SyouhinCd
            SpanSyouhinName.Text = syouhinRec.SyouhinMei

        End If

        '加盟店関連情報のセット
        syouhinRec.KameitenCdDisp = strKameitenCd
        syouhinRec.KeiretuCd = Me.KeiretuCd

        '請求先が個別に指定されている場合、デフォルトの請求先を上書き
        If Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value <> String.Empty Then
            syouhinRec.SeikyuuSakiCdDisp = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value
            syouhinRec.SeikyuuSakiBrcDisp = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc.Value
            syouhinRec.SeikyuuSakiKbnDisp = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn.Value
        End If

        If syouhinRec IsNot Nothing Then
            '請求タイプの設定
            RaiseEvent SetSeikyuuTypeAction(Me _
                                            , e _
                                            , Me.ClientID _
                                            , syouhinRec.SeikyuuSakiType _
                                            , syouhinRec.KeiretuCd _
                                            , syouhinRec.KameitenCdDisp)
        End If

        ' コントロールの活性制御
        EnabledCtrl(enabled)

        ' 請求金額の設定(解約払戻以外)
        If mode <> DisplayMode.KAIYAKU And _
           SelectSeikyuuUmu.SelectedValue = "1" Then
            ' 請求先情報
            If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
               HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then
                '**************************************************
                ' 直接請求
                '**************************************************
                ' ※標準価格を実請求金額・工務店金額に設定
                TextKoumutenSeikyuuGaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)
                TextJituSeikyuuGaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                SetZeigaku(e)
            ElseIf HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuKeiretu Then
                '**************************************************
                ' 他請求（系列）
                '**************************************************
                ' 標準価格を工務店金額に設定
                TextKoumutenSeikyuuGaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                ' 実請求額を取得する
                If TextKoumutenSeikyuuGaku.Text = "0" Then
                    TextJituSeikyuuGaku.Text = "0"
                Else
                    Dim jibanLogic As New JibanLogic
                    Dim zeinukiGaku As Integer = 0

                    If jibanLogic.GetSeikyuuGaku(sender, _
                                            3, _
                                            HiddenKeiretuCd.Value, _
                                            TextSyouhinCd.Text, _
                                            syouhinRec.HyoujunKkk, _
                                            zeinukiGaku) Then

                        ' 実請求金額へセット
                        TextJituSeikyuuGaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                    End If
                End If

                SetZeigaku(e)

            ElseIf HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuNotKeiretu Then
                '**************************************************
                ' 他請求（系列以外）
                '**************************************************
                ' 工務店請求額は０
                TextKoumutenSeikyuuGaku.Text = "0"
                ' 実請求額は標準価格
                TextJituSeikyuuGaku.Text = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                SetZeigaku(e)
            End If

        ElseIf mode = DisplayMode.KAIYAKU And _
               SelectSeikyuuUmu.SelectedValue = "1" Then

            '商品設定メソッド
            SetKaiyakuSyouhin(sender, e)

        End If

        If blnLinkRefresh Then
            RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)
        End If
    End Sub

    ''' <summary>
    ''' 消費税額、税込金額のセット
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZeigaku(ByVal e As System.EventArgs, Optional ByVal blnZeigaku As Boolean = False)

        ' 実請求額が空白の場合、消費税、税込金額を空白にする
        If TextJituSeikyuuGaku.Text.Trim() = "" Then
            TextSyouhizeiGaku.Text = ""
            TextZeikomiKingaku.Text = ""
            ' 税込金額変更を親コントロールに通知する
            RaiseEvent ChangeZeikomiGaku(Me, e, 0)
            Exit Sub
        ElseIf TextJituSeikyuuGaku.Text.Trim() = "0" Then
            TextSyouhizeiGaku.Text = "0"
            TextZeikomiKingaku.Text = "0"
            ' 税込金額変更を親コントロールに通知する
            RaiseEvent ChangeZeikomiGaku(Me, e, 0)
            Exit Sub
        End If

        Dim jitugaku As Integer = IIf(TextJituSeikyuuGaku.Text.Trim() = "", _
                                      0, _
                                      Integer.Parse(TextJituSeikyuuGaku.Text.Replace(",", "")))
        Dim zeigaku As Integer = 0
        If blnZeigaku Then '消費税額計算の場合
            zeigaku = IIf(TextSyouhizeiGaku.Text.Trim() = "", _
                                      0, _
                                      Integer.Parse(TextSyouhizeiGaku.Text.Replace(",", "")))
        Else
            If IsNumeric(Me.HiddenZeiritu.Value) = False Then
                Me.HiddenZeiritu.Value = "0"
            End If
            zeigaku = Fix(jitugaku * Decimal.Parse(HiddenZeiritu.Value))
        End If

        Dim zeikomi As Integer = jitugaku + zeigaku

        TextSyouhizeiGaku.Text = zeigaku.ToString(EarthConst.FORMAT_KINGAKU_1)
        TextZeikomiKingaku.Text = zeikomi.ToString(EarthConst.FORMAT_KINGAKU_1)

        If Not e Is Nothing Then
            ' 税込金額変更を親コントロールに通知する
            RaiseEvent ChangeZeikomiGaku(Me, e, zeikomi)
        End If
    End Sub

    ''' <summary>
    ''' 仕入消費税額のセット
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSiireZeigaku()
        Dim intSyouGaku As Integer = 0
        Dim intSiireZeiGaku As Integer = 0

        '承諾書金額が空白の場合、仕入消費税額を空白にする
        If Me.TextSyoudakusyoKingaku.Text.Trim() = String.Empty Then
            Me.TextSiireSyouhizeiGaku.Text = String.Empty
            Exit Sub
        ElseIf Me.TextSyoudakusyoKingaku.Text.Trim() = "0" Then
            Me.TextSiireSyouhizeiGaku.Text = "0"
            Exit Sub
        End If

        '税率の確認
        If IsNumeric(Me.HiddenZeiritu.Value) = False Then
            Me.HiddenZeiritu.Value = "0"
        End If

        '承諾書金額の取得
        intSyouGaku = Integer.Parse(Me.TextSyoudakusyoKingaku.Text.Replace(",", ""))

        '仕入消費税額の計算
        intSiireZeiGaku = Fix(intSyouGaku * Decimal.Parse(Me.HiddenZeiritu.Value))

        '画面へ設定
        Me.TextSiireSyouhizeiGaku.Text = intSiireZeiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

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
        masterAjaxSM.SetFocus(ctrl)
    End Sub

#Region "系列フラグ取得"
    ''' <summary>
    ''' 系列コードより系列フラグを取得します
    ''' </summary>
    ''' <param name="keiretuCd">系列コード</param>
    ''' <returns>系列フラグ</returns>
    ''' <remarks>1:系列</remarks>
    Private Function GetKeiretuFlg(ByVal keiretuCd As String) As Integer

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKeiretuFlg", _
                                            keiretuCd)

        Dim blnKeiretuFlg = cLogic.getKeiretuFlg(keiretuCd)

        If blnKeiretuFlg Then
            Return 1
        Else
            Return 0
        End If

    End Function
#End Region

#Region "発注書金額関連の処理"
    ''' <summary>
    ''' 発注金額変更時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub HattyuuAfterUpdate(ByVal sender As Object)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".HattyuuAfterUpdate", _
                                                    sender)

        Dim hattyuuKingaku As Integer = Integer.MinValue

        CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, hattyuuKingaku)

        TextHattyuusyoKakuninbi.Text = PfunZ010SetHatyuuYMD(hattyuuKingaku)

        '発注書確定チェック
        Select Case FunCheckHKakutei(2, sender)
            Case 1
                '
            Case 2
                SelectHattyuusyoKakutei.SelectedValue = "1"

                ' Old値に現在の値をセット
                HiddenHattyuusyoFlgOld.Value = SelectHattyuusyoKakutei.SelectedValue

                ' 発注書金額を入力不可に設定
                EnableTextBox(TextHattyuusyoKingaku, False)
        End Select

        '更新後発注書金額をoldに設定
        HiddenHattyuusyoKingakuOld.Value = TextHattyuusyoKingaku.Text
        TextHattyuusyoKingaku.Text = IIf(hattyuuKingaku = Integer.MinValue, "", hattyuuKingaku.ToString(EarthConst.FORMAT_KINGAKU_1))

    End Sub

    ''' <summary>
    ''' 発注書確認日設定処理
    ''' </summary>
    ''' <param name="rvntKingaku">発注書金額</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PfunZ010SetHatyuuYMD(ByVal rvntKingaku As Integer) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".PfunZ010SetHatyuuYMD", _
                                                    rvntKingaku)

        If rvntKingaku = 0 Or rvntKingaku = Integer.MinValue Then
            Return ""
        Else
            Return Date.Now.ToString("yyyy/MM/dd")
        End If
    End Function

    ''' <summary>
    ''' 発注書確定チェック
    ''' </summary>
    ''' <param name="rlngMode">1,発注書確定変更時.2,発注書金額変更時</param>
    ''' <param name="sender">クライアント スクリプト ブロックを登録するコントロール</param>
    ''' <returns>0:処理なし、1:発注書確定へフォーカス移行、2:自動確定</returns>
    ''' <remarks></remarks>
    Public Function FunCheckHKakutei(ByVal rlngMode As Long, _
                                     ByVal sender As Object) As Long

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".FunCheckHKakutei", _
                                                    rlngMode, sender)

        FunCheckHKakutei = 0

        If SelectHattyuusyoKakutei.SelectedValue = "1" And _
           TextHattyuusyoKingaku.Text IsNot TextJituSeikyuuGaku.Text Then

            Dim hattyuuKingaku As Integer = Integer.MinValue
            CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, hattyuuKingaku)
            ' 発注書確認日を設定
            TextHattyuusyoKakuninbi.Text = PfunZ010SetHatyuuYMD(hattyuuKingaku)

        End If

        ' 比較する金額を数値変換する
        Dim chkVal1 As Integer = 0
        Dim chkVal2 As Integer = 0
        CommonLogic.Instance.SetDisplayString(TextHattyuusyoKingaku.Text, chkVal1)
        CommonLogic.Instance.SetDisplayString(TextJituSeikyuuGaku.Text, chkVal2)

        ' 売上状況
        If rlngMode = 2 Then
            ' 発注書金額変更時はテキスト再変更後メッセージ表示
            If SelectUriageSyori.SelectedValue = EarthConst.URIAGE_ZUMI_CODE Then

                ' 比較して金額の比較によりメッセージを分ける
                If chkVal1 = chkVal2 Then
                    FunCheckHKakutei = 2
                    If HiddenHattyuusyoKingakuOld.Value <> "" And _
                       HiddenHattyuusyoFlgOld.Value <> "1" Then
                        ' 発注書自動確定
                        ScriptManager.RegisterClientScriptBlock(sender, _
                                                                sender.GetType(), _
                                                                "alert", _
                                                                "alert('" & _
                                                                Messages.MSG045C & _
                                                                "')", True)
                    End If
                End If
            End If
        Else
            If SelectHattyuusyoKakutei.SelectedValue = "1" Then
                ' 発注書確定変更時は金額相違でメッセージ表示
                FunCheckHKakutei = 2
                If chkVal1 <> chkVal2 Then
                    ' 発注書自動確定
                    ScriptManager.RegisterClientScriptBlock(sender, _
                                                            sender.GetType(), _
                                                            "alert", _
                                                            "alert('" & _
                                                            Messages.MSG046C & _
                                                            "')", True)
                End If
            End If
        End If

        '発注書確定済みの場合、発注書金額を編集不可に設定
        If SelectHattyuusyoKakutei.SelectedValue = "1" Then
            EnableTextBox(TextHattyuusyoKingaku, False)
        Else
            If HiddenHattyuusyoKanriKengen.Value = "0" And _
               HiddenKeiriGyoumuKengen.Value = "0" Then
            Else
                ' 経理権限か、発注書管理権限有る場合、活性化
                EnableTextBox(TextHattyuusyoKingaku, True)
            End If
        End If

    End Function
#End Region

#Region "コントロールの活性制御"

    ''' <summary>
    ''' コントロールの活性制御
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnabledCtrl(ByVal enabled As Boolean)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnabledCtrl", _
                                                    enabled)

        ' モードの取得
        Dim mode As Integer = Integer.Parse(HiddenDispMode.Value)

        If Not enabled Then

            ' データをクリア
            TextSyouhinCd.Text = ""                     ' 商品コード
            SpanSyouhinName.Text = ""              ' 商品名
            TextJituSeikyuuGaku.Text = ""               ' 実請求金額
            TextKoumutenSeikyuuGaku.Text = ""           ' 工務店請求額
            TextSyouhizeiGaku.Text = ""                 ' 消費税額
            TextSiireSyouhizeiGaku.Text = ""            ' 仕入消費税額
            TextZeikomiKingaku.Text = ""                ' 税込金額
            TextSyoudakusyoKingaku.Text = ""            ' 承諾書金額
            TextMitumorisyoSakuseibi.Text = ""          ' 見積書作成日
            TextSeikyuusyoHakkoubi.Text = ""            ' 請求書発行日
            TextUriageNengappi.Text = ""                ' 売上年月日
            TextDenpyouUriageNengappiDisplay.Text = ""  ' 伝票売上年月日(参照用)
            TextDenpyouUriageNengappi.Text = ""         ' 伝票売上年月日(修正用)
            TextDenpyouSiireNengappiDisplay.Text = ""   ' 伝票仕入年月日(参照用)
            TextDenpyouSiireNengappi.Text = ""          ' 伝票仕入年月日(修正用)
            If mode = DisplayMode.SYOUHIN1 Or _
               mode = DisplayMode.SYOUHIN2 Or _
               mode = DisplayMode.SYOUHIN3 Then
                SelectSeikyuuUmu.SelectedValue = "1"    ' 請求有無
            End If
            SelectKakutei.SelectedValue = "0"           ' 確定
            SelectUriageSyori.SelectedValue = "0"       ' 売上処理
            TextUriageBi.Text = ""                      ' 売上日
            TextHattyuusyoKingaku.Text = ""             ' 発注書金額
            TextHattyuusyoKakuninbi.Text = ""           ' 発注書確認日
            SelectHattyuusyoKakutei.SelectedValue = "0" ' 発注書確定
            HiddenHattyuusyoFlgOld.Value = ""           ' 発注書フラグOld
            HiddenKingakuFlg.Value = ""                 ' 金額フラグ
            Me.AccTokubetuTaiouToolTip.AccDisplayCd.Value = String.Empty    '特別対応ツールチップ(表示用）

        End If

        ' 活性制御
        TextKoumutenSeikyuuGaku.Enabled = enabled       ' 工務店請求額
        TextJituSeikyuuGaku.Enabled = enabled           ' 実請求金額
        TextSyouhizeiGaku.Enabled = enabled             ' 消費税額
        TextSiireSyouhizeiGaku.Enabled = enabled        ' 仕入消費税額
        TextSyoudakusyoKingaku.Enabled = enabled        ' 承諾書金額
        TextMitumorisyoSakuseibi.Enabled = enabled      ' 見積書作成日
        TextSeikyuusyoHakkoubi.Enabled = enabled        ' 請求書発行日
        TextUriageNengappi.Enabled = enabled            ' 売上年月日
        TextDenpyouUriageNengappiDisplay.Enabled = enabled  ' 伝票売上年月日(参照用)
        TextDenpyouUriageNengappi.Enabled = enabled         ' 伝票売上年月日(修正用)
        TextDenpyouSiireNengappiDisplay.Enabled = enabled   ' 伝票仕入年月日(参照用)
        TextDenpyouSiireNengappi.Enabled = enabled          ' 伝票仕入年月日(修正用)
        If mode = DisplayMode.SYOUHIN1 Or _
           mode = DisplayMode.SYOUHIN2 Or _
           mode = DisplayMode.SYOUHIN3 Then
            SelectSeikyuuUmu.Enabled = enabled          ' 請求有無
        End If
        SelectKakutei.Enabled = enabled                 ' 確定
        SelectUriageSyori.Enabled = enabled             ' 売上処理
        TextHattyuusyoKingaku.Enabled = enabled         ' 発注書金額
        TextHattyuusyoKakuninbi.Enabled = enabled       ' 発注書確認日
        SelectHattyuusyoKakutei.Enabled = enabled       ' 発注書確定

    End Sub

    ''' <summary>
    ''' テキストボックス単体の活性制御
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableTextBox(ByRef ctrl As TextBox, _
                              ByVal enabled As Boolean)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnableTextBox", _
                                                    ctrl, enabled)

        ctrl.ReadOnly = Not enabled

        If enabled Then
            ctrl.BackColor = Drawing.Color.White
            ctrl.BorderStyle = BorderStyle.NotSet
            ctrl.TabIndex = 0
        Else
            ctrl.BackColor = Drawing.Color.Transparent
            ctrl.BorderStyle = BorderStyle.None
            ctrl.TabIndex = -1
        End If

    End Sub

    ''' <summary>
    ''' ドロップダウン単体の活性制御
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableDropDownList(ByRef ctrl As DropDownList, _
                                   ByVal enabled As Boolean)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnableDropDownList", _
                                                    ctrl, enabled)
        ctrl.Enabled = enabled

        If enabled Then
            ctrl.BackColor = Drawing.Color.White
            ctrl.BorderStyle = BorderStyle.NotSet
            ctrl.TabIndex = 0
        Else
            ctrl.BackColor = Drawing.Color.Transparent
            ctrl.BorderStyle = BorderStyle.None
            ctrl.TabIndex = -1
        End If

    End Sub

    ''' <summary>
    ''' コントロールの有効無効を切替える[権限別]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EnabledCtrlKengen()

        ' 経理権限が無い場合、発注書以外非活性
        If HiddenKeiriGyoumuKengen.Value = "0" Then
            EnableTextBox(TextSyouhinCd, False) '商品コード
            EnableTextBox(TextKoumutenSeikyuuGaku, False) '工務店請求税抜金額
            EnableTextBox(TextJituSeikyuuGaku, False) '実請求金額
            EnableTextBox(TextSyouhizeiGaku, False) '消費税額
            EnableTextBox(TextSiireSyouhizeiGaku, False) '仕入消費税額
            EnableTextBox(TextSyoudakusyoKingaku, False) '承諾書金額
            EnableTextBox(TextMitumorisyoSakuseibi, False) '見積書金額
            EnableTextBox(TextSeikyuusyoHakkoubi, False) '請求書発行日
            EnableTextBox(TextUriageNengappi, False) '売上年月日
            EnableTextBox(TextDenpyouUriageNengappi, False) '伝票売上年月日
            EnableTextBox(TextDenpyouSiireNengappi, False) '伝票仕入年月日
            EnableTextBox(TextHattyuusyoKakuninbi, False) '発注書確認日
            EnableDropDownList(SelectSeikyuuUmu, False) '請求有無
            EnableDropDownList(SelectUriageSyori, False) '売上処理
            EnableDropDownList(SelectKakutei, False) '確定処理
            ButtonSyouhinKensaku.Visible = False '商品検索

        End If

        ' 経理権限及び発注書管理権限が無い場合、非活性化
        If HiddenHattyuusyoKanriKengen.Value = "0" And _
           HiddenKeiriGyoumuKengen.Value = "0" Then
            EnableTextBox(TextHattyuusyoKingaku, False) '発注書金額
            EnableDropDownList(SelectHattyuusyoKakutei, False) '発注書確定
        End If

    End Sub

#End Region

#Region "解約払戻変更時確認処理"

    ''' <summary>
    ''' 解約払戻商品セット処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetKaiyakuSyouhin(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strKameitenCd As String = Me.SeikyuuSiireLinkCtrl.AccKameitenCd.Value
        Dim logic As New TeibetuSyuuseiLogic
        Dim syouhinRec As New SyouhinMeisaiRecord
        Dim strZeiKbn As String = String.Empty      '税区分
        Dim strZeiritu As String = String.Empty     '税率
        Dim strSyouhinCd As String = String.Empty   '商品コード

        syouhinRec = logic.GetSyouhinInfo("" _
                                        , EarthEnum.EnumSyouhinKubun.Kaiyaku _
                                        , strKameitenCd)


        '加盟店関連情報のセット
        syouhinRec.KameitenCdDisp = strKameitenCd
        syouhinRec.KeiretuCd = Me.KeiretuCd

        '請求先が個別に指定されている場合、デフォルトの請求先を上書き
        If Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value <> String.Empty Then
            syouhinRec.SeikyuuSakiCdDisp = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value
            syouhinRec.SeikyuuSakiBrcDisp = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc.Value
            syouhinRec.SeikyuuSakiKbnDisp = Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn.Value
        End If

        If syouhinRec IsNot Nothing Then
            '請求タイプの設定
            RaiseEvent SetSeikyuuTypeAction(Me _
                                            , e _
                                            , Me.ClientID _
                                            , syouhinRec.SeikyuuSakiType _
                                            , syouhinRec.KeiretuCd _
                                            , syouhinRec.KameitenCdDisp)
        End If

        '商品名
        TextSyouhinCd.Text = syouhinRec.SyouhinCd
        SpanSyouhinName.Text = syouhinRec.SyouhinMei

        '※・保証開始日、保証有無、保証期間、保険会社、保険申請、保険申請月
        '解約払戻有無＝有り時はデフォルトクリアとなるため、初期化はしないで登録ボタン押下時処理を確認

        '売上年月日で判断して、正しい税率を取得する
        strSyouhinCd = syouhinRec.SyouhinCd
        If cBizLogic.getSyouhiZeirituUriage(Me.TextUriageNengappi.Text.Replace("/", ""), strSyouhinCd, strZeiKbn, strZeiritu) Then
            '取得した税区分・税率をセット
            syouhinRec.ZeiKbn = strZeiKbn
            syouhinRec.Zeiritu = strZeiritu
        End If

        HiddenZeiritu.Value = syouhinRec.Zeiritu
        HiddenZeiKbn.Value = syouhinRec.ZeiKbn

        ' 解約は標準価格部に解約払戻し価格を設定する
        syouhinRec.HyoujunKkk = _
            logic.GetKaiyakuKakaku(HiddenKameitenCd.Value, HiddenKubun.Value)

        '**************************************************
        ' 解約払戻
        '**************************************************
        Dim jibanLogic As New JibanLogic
        Dim zeinukiGaku As Integer = Math.Abs(syouhinRec.HyoujunKkk) * -1

        ' 実請求金額へセット
        TextJituSeikyuuGaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        If HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuKeiretu Or _
           HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TyokusetuSeikyuuNotKeiretu Then
            '**************************************************
            ' 直接請求
            '**************************************************
            ' 工務店請求額へ実請求金額をセット
            TextKoumutenSeikyuuGaku.Text = TextJituSeikyuuGaku.Text
        ElseIf HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuKeiretu Then
            '**************************************************
            ' 他請求（系列）
            '**************************************************
            zeinukiGaku = 0

            If JibanLogic.GetSeikyuuGaku(sender, _
                                    2, _
                                    HiddenKeiretuCd.Value, _
                                    TextSyouhinCd.Text, _
                                    syouhinRec.HyoujunKkk, _
                                    zeinukiGaku) Then

                zeinukiGaku = Math.Abs(zeinukiGaku) * -1

                ' 工務店請求額へセット
                TextKoumutenSeikyuuGaku.Text = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
            End If
        ElseIf HiddenSeikyuuType.Value = EarthEnum.EnumSeikyuuType.TaSeikyuuNotKeiretu Then
            '**************************************************
            ' 他請求（系列以外）
            '**************************************************
            ' 工務店請求額は０
            TextKoumutenSeikyuuGaku.Text = "0"
        End If

        SetZeigaku(e)

        HiddenSeikyuuUmuOld.Value = SelectSeikyuuUmu.SelectedValue

        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)


    End Sub


    ''' <summary>
    ''' 解約払戻申請有無変更・後処理(JSにてキャンセル時)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKaiyakuHaraiModosiCancel_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '解約払戻請求有無
        SelectSeikyuuUmu.SelectedValue = "" '空白
        HiddenSeikyuuUmuOld.Value = ""

        Enabled = False

        ' コントロールの活性制御
        EnabledCtrl(Enabled)

        SelectSeikyuuUmu.Enabled = True          ' 請求有無

        'フォーカスセット
        SetFocus(SelectSeikyuuUmu)

    End Sub

#End Region

#Region "画面コントロールの値を結合し、文字列化する"
    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する(売上)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringUriage() As String

        Dim sb As New StringBuilder

        sb.Append(TextSyouhinCd.Text & EarthConst.SEP_STRING)               '商品コード
        sb.Append(TextJituSeikyuuGaku.Text & EarthConst.SEP_STRING)         '売上金額(実請求税抜金額)
        sb.Append(TextSyouhizeiGaku.Text & EarthConst.SEP_STRING)           '消費税額
        sb.Append(HiddenZeiKbn.Value & EarthConst.SEP_STRING)               '税区分(非表示項目)
        sb.Append(TextDenpyouUriageNengappi.Text & EarthConst.SEP_STRING)   '伝票売上年月日
        sb.Append(TextUriageNengappi.Text & EarthConst.SEP_STRING)          '売上年月日
        sb.Append(SelectUriageSyori.SelectedValue & EarthConst.SEP_STRING)  '売上計上FLG
        sb.Append(TextSeikyuusyoHakkoubi.Text & EarthConst.SEP_STRING)      '請求書発行日

        sb.Append(SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value & EarthConst.SEP_STRING)          '請求先コード
        sb.Append(SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc.Value & EarthConst.SEP_STRING)         '請求先枝番
        sb.Append(SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn.Value & EarthConst.SEP_STRING)         '請求先区分

        Return (sb.ToString)

    End Function

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する(仕入)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringSiire() As String

        Dim sb As New StringBuilder

        sb.Append(TextSyouhinCd.Text & EarthConst.SEP_STRING)               '商品コード
        sb.Append(TextSyoudakusyoKingaku.Text & EarthConst.SEP_STRING)      '仕入金額(承諾書金額)
        sb.Append(TextSiireSyouhizeiGaku.Text & EarthConst.SEP_STRING)      '仕入消費税額
        sb.Append(TextDenpyouSiireNengappi.Text & EarthConst.SEP_STRING)    '伝票仕入年月日
        sb.Append(HiddenZeiKbn.Value & EarthConst.SEP_STRING)               '税区分(非表示項目)
        sb.Append(SelectUriageSyori.SelectedValue & EarthConst.SEP_STRING)  '売上計上FLG

        sb.Append(SeikyuuSiireLinkCtrl.AccTysKaisyaCd.Value & EarthConst.SEP_STRING)            '調査会社コード
        sb.Append(SeikyuuSiireLinkCtrl.AccTysKaisyaJigyousyoCd.Value & EarthConst.SEP_STRING)   '調査会社事業所コード

        Return (sb.ToString)

    End Function

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する(特別対応価格対応)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringTokubetuTaiou() As String

        Dim sb As New StringBuilder

        sb.Append(TextSyouhinCd.Text & EarthConst.SEP_STRING)               '商品コード
        sb.Append(TextKoumutenSeikyuuGaku.Text & EarthConst.SEP_STRING)     '工務店請求税抜金額
        sb.Append(TextJituSeikyuuGaku.Text & EarthConst.SEP_STRING)         '実請求税抜金額
        sb.Append(TextSyouhizeiGaku.Text & EarthConst.SEP_STRING)           '消費税額
        sb.Append(SelectSeikyuuUmu.SelectedValue & EarthConst.SEP_STRING)   '請求有無

        Return (sb.ToString)

    End Function

#Region "画面コントロールの変更箇所対応"

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する(全項目)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringAll() As String

        Dim sb As New StringBuilder

        '●非表示項目１
        sb.Append(HiddenKubun.Value & EarthConst.SEP_STRING)   '区分
        sb.Append(HiddenBangou.Value & EarthConst.SEP_STRING)   '保証書NO
        sb.Append(HiddenBunruiCd.Value & EarthConst.SEP_STRING)   '分類コード
        sb.Append(HiddenGamenHyoujiNo.Value & EarthConst.SEP_STRING)   '画面表示NO

        '●表示項目
        sb.Append(TextSyouhinCd.Text & EarthConst.SEP_STRING)   '商品コード
        sb.Append(SelectKakutei.SelectedValue & EarthConst.SEP_STRING)   '確定区分
        sb.Append(SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd.Value & EarthConst.SEP_STRING)   '請求先コード
        sb.Append(SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc.Value & EarthConst.SEP_STRING)   '請求先枝番
        sb.Append(SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn.Value & EarthConst.SEP_STRING)   '請求先区分
        sb.Append(SeikyuuSiireLinkCtrl.AccTysKaisyaCd.Value & EarthConst.SEP_STRING)   '調査会社コード
        sb.Append(SeikyuuSiireLinkCtrl.AccTysKaisyaJigyousyoCd.Value & EarthConst.SEP_STRING)   '調査会社事業所コード

        sb.Append(TextSyoudakusyoKingaku.Text & EarthConst.SEP_STRING)   '承諾書金額(仕入)
        sb.Append(TextSiireSyouhizeiGaku.Text & EarthConst.SEP_STRING)   '仕入消費税額
        sb.Append(TextDenpyouSiireNengappiDisplay.Text & EarthConst.SEP_STRING)   '伝票仕入年月日
        sb.Append(TextDenpyouSiireNengappi.Text & EarthConst.SEP_STRING)   '伝票仕入年月日修正

        sb.Append(TextKoumutenSeikyuuGaku.Text & EarthConst.SEP_STRING)   '工務店請求税抜金額
        sb.Append(TextJituSeikyuuGaku.Text & EarthConst.SEP_STRING)   '実請求税抜金額
        sb.Append(TextSyouhizeiGaku.Text & EarthConst.SEP_STRING)   '消費税額(売上)
        sb.Append(TextZeikomiKingaku.Text & EarthConst.SEP_STRING)   '税込金額(売上)
        sb.Append(TextDenpyouUriageNengappiDisplay.Text & EarthConst.SEP_STRING)   '伝票売上年月日
        sb.Append(TextDenpyouUriageNengappi.Text & EarthConst.SEP_STRING)   '伝票売上年月日修正
        sb.Append(TextUriageNengappi.Text & EarthConst.SEP_STRING)   '売上年月日
        sb.Append(SelectUriageSyori.SelectedValue & EarthConst.SEP_STRING)   '売上処理FLG(売上計上FLG)
        sb.Append(TextUriageBi.Text & EarthConst.SEP_STRING)   '売上処理日(売上計上日)

        sb.Append(TextSeikyuusyoHakkoubi.Text & EarthConst.SEP_STRING)   '請求書発行日
        sb.Append(SelectSeikyuuUmu.SelectedValue & EarthConst.SEP_STRING)   '請求有無

        sb.Append(TextHattyuusyoKingaku.Text & EarthConst.SEP_STRING)   '発注書金額
        sb.Append(SelectHattyuusyoKakutei.SelectedValue & EarthConst.SEP_STRING)   '発注書確定FLG
        sb.Append(TextHattyuusyoKakuninbi.Text & EarthConst.SEP_STRING)   '発注書確認日

        '●非表示項目２
        sb.Append(HiddenZeiKbn.Value & EarthConst.SEP_STRING)   '税区分(税率)


        'KEY情報の取得
        Me.getCtrlValuesStringAllKey()

        Return (sb.ToString)
    End Function

    ''' <summary>
    ''' 邸別請求テーブルの全項目情報を結合し、文字列化する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getCtrlValuesStringAllKey()
        Dim dic As New Dictionary(Of String, String)
        Dim strRecString As String = String.Empty
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic

        '画面表示時のDB値の連結値を取得
        If Me.HiddenKeyValue.Value = String.Empty Then

            With dic
                .Add("0", "区分")
                .Add("1", "保証書NO")
                .Add("2", "分類コード")
                .Add("3", "画面表示NO")
                .Add("4", "商品コード")
                .Add("5", "確定区分")
                .Add("6", "請求先コード")
                .Add("7", "請求先枝番")
                .Add("8", "請求先区分")
                .Add("9", "調査会社コード")
                .Add("10", "調査会社事業所コード")
                .Add("11", "承諾書金額(仕入)")
                .Add("12", "仕入消費税額")
                .Add("13", "伝票仕入年月日")
                .Add("14", "伝票仕入年月日修正")
                .Add("15", "工務店請求税抜金額")
                .Add("16", "実請求税抜金額")
                .Add("17", "消費税額(売上)")
                .Add("18", "税込金額(売上)")
                .Add("19", "伝票売上年月日")
                .Add("20", "伝票売上年月日修正")
                .Add("21", "売上年月日")
                .Add("22", "売上処理FLG(売上計上FLG)")
                .Add("23", "売上処理日(売上計上日)")
                .Add("24", "請求書発行日")
                .Add("25", "請求有無")
                .Add("26", "発注書金額")
                .Add("27", "発注書確定FLG")
                .Add("28", "発注書確認日")
                .Add("29", "税区分(税率)")
            End With

            strRecString = iLogic.getJoinString(dic.Values.GetEnumerator)
            Me.HiddenKeyValue.Value = strRecString
        End If

    End Sub

    ''' <summary>
    ''' 邸別請求テーブルの全項目情報を管理し、対象の項目が存在した場合、背景色を赤色に変更する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getCtrlValuesStringAllKeyCtrlId(ByVal strKey As String)
        Dim objRet As New Object
        Dim dic As New Dictionary(Of String, Object)
        Dim strRecString As String = String.Empty
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic

        'Key毎にオブジェクトをセット
        With dic
            .Add("0", Me.HiddenKubun)
            .Add("1", Me.HiddenBangou)
            .Add("2", Me.HiddenBunruiCd)
            .Add("3", Me.HiddenGamenHyoujiNo)
            .Add("4", Me.TextSyouhinCd)
            .Add("5", Me.SelectKakutei)
            .Add("6", Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiCd)
            .Add("7", Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiBrc)
            .Add("8", Me.SeikyuuSiireLinkCtrl.AccSeikyuuSakiKbn)
            .Add("9", Me.SeikyuuSiireLinkCtrl.AccTysKaisyaCd)
            .Add("10", Me.SeikyuuSiireLinkCtrl.AccTysKaisyaJigyousyoCd)
            .Add("11", Me.TextSyoudakusyoKingaku)
            .Add("12", Me.TextSiireSyouhizeiGaku)
            .Add("13", Me.TextDenpyouSiireNengappiDisplay)
            .Add("14", Me.TextDenpyouSiireNengappi)
            .Add("15", Me.TextKoumutenSeikyuuGaku)
            .Add("16", Me.TextJituSeikyuuGaku)
            .Add("17", Me.TextSyouhizeiGaku)
            .Add("18", Me.TextZeikomiKingaku)
            .Add("19", Me.TextDenpyouUriageNengappiDisplay)
            .Add("20", Me.TextDenpyouUriageNengappi)
            .Add("21", Me.TextUriageNengappi)
            .Add("22", Me.SelectUriageSyori)
            .Add("23", Me.TextUriageBi)
            .Add("24", Me.TextSeikyuusyoHakkoubi)
            .Add("25", Me.SelectSeikyuuUmu)
            .Add("26", Me.TextHattyuusyoKingaku)
            .Add("27", Me.SelectHattyuusyoKakutei)
            .Add("28", Me.TextHattyuusyoKakuninbi)
            .Add("29", Me.HiddenZeiKbn)
        End With

        '背景色変更処理
        Call cLogic.ChgHenkouCtrlBgColor(dic, strKey)

    End Sub

    ''' <summary>
    ''' 邸別請求テーブルの全項目情報を結合し、文字列化する。
    ''' 変更箇所の背景色を変更する
    ''' </summary>
    ''' <param name="strKey">KEY値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringAllKeyName(ByVal strKey As String, ByVal strCtrlNameKey As String) As String
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic
        Dim MyLogic As New TeibetuSyuuseiLogic

        Dim strKeyValues() As String
        Dim strHiddenKeyValues() As String
        Dim strRet As String = String.Empty

        Dim intCnt As Integer = 0
        Dim strTmp1 As String = String.Empty
        Dim strTmp2 As String = String.Empty
        Dim dicItem1 As Dictionary(Of String, String)
        Dim strColorId As String = String.Empty

        If strKey = String.Empty Then
            Return strRet
            Exit Function
        End If

        'DB値
        strKeyValues = iLogic.getArrayFromDollarSep(strKey)

        '項目名を取得
        strHiddenKeyValues = iLogic.getArrayFromDollarSep(strCtrlNameKey)
        dicItem1 = MyLogic.getDicItem(strHiddenKeyValues)

        For intCnt = 0 To strHiddenKeyValues.Length - 1

            If strKeyValues.Length <= intCnt Then Exit For

            strTmp1 = strKeyValues(intCnt)
            If strTmp1 <> String.Empty Then
                If dicItem1.ContainsKey(strTmp1) Then
                    If intCnt <> 0 Then '最初の項目に","は付けない
                        strRet &= ","
                    End If
                    '変更箇所の項目名称を取得
                    strRet &= dicItem1(strTmp1)
                    '背景色の変更
                    Me.getCtrlValuesStringAllKeyCtrlId(strTmp1)
                End If
            End If
        Next

        Return strRet
    End Function

    ''' <summary>
    ''' 変更のあったコントロール名称を文字列結合し、返却する
    ''' </summary>
    ''' <param name="strDbVal">DB値</param>
    ''' <param name="strChgVal">変更値</param>
    ''' <param name="strCtrlNm">コントロール名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkChgCtrlName(ByVal strDbVal As String, ByVal strChgVal As String, ByVal strCtrlNm As String) As String
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic
        Dim strDbValues() As String
        Dim strChgValues() As String
        Dim strRet As String = String.Empty
        Dim strKey As String = String.Empty

        Dim intCnt As Integer = 0
        Dim strTmp1 As String = String.Empty
        Dim strTmp2 As String = String.Empty

        'DB値あるいは変更値が未入力の場合
        If strDbVal = String.Empty OrElse strChgVal = String.Empty Then
            Return strRet
            Exit Function
        End If

        'DB値
        strDbValues = iLogic.getArrayFromDollarSep(strDbVal)
        '画面の値
        strChgValues = iLogic.getArrayFromDollarSep(strChgVal)

        '項目数が同じ場合
        If strDbValues.Length = strChgValues.Length Then
            For intCnt = 0 To strDbValues.Length - 1
                strTmp1 = strDbValues(intCnt)
                strTmp2 = strChgValues(intCnt)
                '変更箇所があればindexを退避
                If strTmp1 <> strTmp2 Then
                    strKey &= CStr(intCnt) & EarthConst.SEP_STRING
                End If
            Next
        End If

        'indexを元に、変更箇所の名称と背景色変更を行なう
        strRet = Me.getCtrlValuesStringAllKeyName(strKey, strCtrlNm)

        Return strRet
    End Function

#End Region

#End Region

#End Region

#Region "パブリックメソッド"
    ''' <summary>
    ''' エラーチェック
    ''' </summary>
    ''' <param name="errMess"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <param name="typeWord"></param>
    ''' <param name="denpyouNgList"></param>
    ''' <param name="denpyouErrMess"></param>
    ''' <param name="seikyuuUmuErrMess"></param>
    ''' <param name="strChgPartMess"></param>
    ''' <remarks></remarks>
    Public Sub CheckInput(ByRef errMess As String, _
                          ByRef arrFocusTargetCtrl As List(Of Control), _
                          ByVal typeWord As String, _
                          ByVal denpyouNgList As String, _
                          ByRef denpyouErrMess As String, _
                          ByRef seikyuuUmuErrMess As String, _
                          ByRef strChgPartMess As String _
                          )

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckInput", _
                                                    errMess, _
                                                    arrFocusTargetCtrl, _
                                                    typeWord, _
                                                    denpyouNgList, _
                                                    denpyouErrMess, _
                                                    seikyuuUmuErrMess, _
                                                    strChgPartMess _
                                                    )

        ' モードの取得
        Dim mode As Integer = Integer.Parse(HiddenDispMode.Value)

        '地盤画面共通クラス
        Dim jBn As New Jiban
        '月次予約確定月
        Dim dtGetujiKakuteiLastSyoriDate As DateTime = DateTime.MinValue

        Dim setuzoku As String = typeWord
        If typeWord <> "" Then
            setuzoku = typeWord & "："
        End If

        'DB読み込み時点の値を、現在画面の値と比較(変更有無チェック)
        If HiddenKubun.Value <> String.Empty AndAlso (HiddenOpenValuesUriage.Value <> String.Empty Or HiddenOpenValuesSiire.Value <> String.Empty) Then
            'DB読み込み時点の値が空の場合は比較対象外

            '比較実施(売上)
            If HiddenOpenValuesUriage.Value <> getCtrlValuesStringUriage() Then
                '変更有りの場合
                If denpyouNgList.Replace("$$$115$$$", "$$$110$$$").IndexOf(HiddenKubun.Value & EarthConst.SEP_STRING & _
                                                                           HiddenBangou.Value & EarthConst.SEP_STRING & _
                                                                           Me.BunruiCd.Replace("115", "110") & EarthConst.SEP_STRING & _
                                                                           Me.GamenHyoujiNo) >= 0 Then
                    denpyouErrMess += typeWord & ","
                End If

                '月次確定予約済みの処理年月「以前」の日付で伝票売上年月日を設定するのはエラー
                If cLogic.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouUriageNengappi.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                    errMess += String.Format(Messages.MSG161E, dtGetujiKakuteiLastSyoriDate.Month, typeWord, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month)
                    arrFocusTargetCtrl.Add(Me.TextDenpyouUriageNengappi)
                End If

            End If

            '比較実施(仕入)
            If HiddenOpenValuesSiire.Value <> getCtrlValuesStringSiire() Then
                '月次確定予約済みの処理年月「以前」の日付で伝票仕入年月日を設定するのはエラー
                If cLogic.chkGetujiKakuteiYoyakuzumi(Me.TextDenpyouSiireNengappi.Text, dtGetujiKakuteiLastSyoriDate) = False Then
                    errMess += String.Format(Messages.MSG161E, dtGetujiKakuteiLastSyoriDate.Month, typeWord, dtGetujiKakuteiLastSyoriDate.AddMonths(1).Month).Replace("伝票売上", "伝票仕入")
                    arrFocusTargetCtrl.Add(Me.TextDenpyouSiireNengappi)
                End If
            End If

            ' 商品１～３
            If mode = DisplayMode.SYOUHIN1 OrElse mode = DisplayMode.SYOUHIN2 OrElse mode = DisplayMode.SYOUHIN3 Then
                '比較実施(特別対応価格対応)
                If Me.HiddenOpenValuesTokubetuTaiou.Value <> getCtrlValuesStringTokubetuTaiou() Then
                    Me.AccTokubetuTaiouUpdFlg.Value = EarthConst.ARI_VAL
                Else
                    Me.AccTokubetuTaiouUpdFlg.Value = EarthConst.NASI_VAL
                End If
            End If

        End If

        '比較実施(変更チェック)
        Dim strChgVal As String = Me.getCtrlValuesStringAll()
        If Me.HiddenOpenValue.Value <> strChgVal Then
            Dim strCtrlNm As String = String.Empty
            strCtrlNm = Me.ChkChgCtrlName(Me.HiddenOpenValue.Value, strChgVal, Me.HiddenKeyValue.Value) '変更箇所名の取得
            strChgPartMess += "[" & typeWord & "]\r\n" & strCtrlNm & "\r\n"
        End If

        ' 商品１は必須
        If mode = DisplayMode.SYOUHIN1 Then
            If TextSyouhinCd.Text = String.Empty Then
                errMess += Messages.MSG013E.Replace("@PARAM1", setuzoku & "商品コード")
                arrFocusTargetCtrl.Add(TextSyouhinCd)
            End If
        End If

        ' 商品２・３は商品コードの変更チェックを行う
        If mode = DisplayMode.SYOUHIN2 Or _
           mode = DisplayMode.SYOUHIN3 Then
            If TextSyouhinCd.Text <> HiddenSyouhinCdOld.Value Then
                errMess += Messages.MSG030E.Replace("@PARAM1", setuzoku & "商品コード")
                arrFocusTargetCtrl.Add(ButtonSyouhinKensaku)
            End If
        End If

        '必須チェック
        '売上年月日と伝票売上年月日
        If Me.TextUriageNengappi.Text = String.Empty And Me.TextDenpyouUriageNengappi.Text <> String.Empty Then
            errMess += Messages.MSG153E.Replace("@PARAM1", setuzoku & "伝票売上年月日").Replace("@PARAM2", setuzoku & "売上年月日")
            arrFocusTargetCtrl.Add(TextUriageNengappi)
        End If
        '未売上の場合でかつ、売上年月日と伝票売上年月日、伝票仕入年月日が異なる場合
        If Me.SelectUriageSyori.SelectedValue = "0" Then
            '伝票売上年月日と比較
            '伝票仕入年月日と比較
            If Me.TextUriageNengappi.Text <> Me.TextDenpyouUriageNengappi.Text _
                Or Me.TextUriageNengappi.Text <> Me.TextDenpyouSiireNengappi.Text Then
                errMess += Messages.MSG144E.Replace("@PARAM1", setuzoku & "伝票売上年月日あるいは伝票仕入年月日").Replace("@PARAM2", setuzoku & "売上年月日").Replace("@PARAM3", "更新")
                arrFocusTargetCtrl.Add(Me.TextUriageNengappi)
            End If
        End If

        '入力値チェック
        If TextMitumorisyoSakuseibi.Text <> "" Then
            If DateTime.Parse(TextMitumorisyoSakuseibi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextMitumorisyoSakuseibi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "見積書作成日")
                arrFocusTargetCtrl.Add(TextMitumorisyoSakuseibi)
            End If
        End If

        If TextSeikyuusyoHakkoubi.Text <> "" Then
            If DateTime.Parse(TextSeikyuusyoHakkoubi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextSeikyuusyoHakkoubi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "請求書発行日")
                arrFocusTargetCtrl.Add(TextSeikyuusyoHakkoubi)
            End If
        End If

        If TextUriageNengappi.Text <> "" Then
            If DateTime.Parse(TextUriageNengappi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextUriageNengappi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "売上年月日")
                arrFocusTargetCtrl.Add(TextUriageNengappi)
            End If
        End If

        If TextDenpyouUriageNengappi.Text <> "" Then
            If DateTime.Parse(TextDenpyouUriageNengappi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextDenpyouUriageNengappi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "伝票売上年月日")
                arrFocusTargetCtrl.Add(TextDenpyouUriageNengappi)
            End If
        End If

        If TextDenpyouSiireNengappi.Text <> "" Then
            If DateTime.Parse(TextDenpyouSiireNengappi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextDenpyouSiireNengappi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "伝票仕入年月日")
                arrFocusTargetCtrl.Add(TextDenpyouSiireNengappi)
            End If
        End If

        If TextHattyuusyoKakuninbi.Text <> "" Then
            If DateTime.Parse(TextHattyuusyoKakuninbi.Text) > EarthConst.Instance.MAX_DATE Or _
               DateTime.Parse(TextHattyuusyoKakuninbi.Text) < EarthConst.Instance.MIN_DATE Then
                errMess += Messages.MSG014E.Replace("@PARAM1", setuzoku & "発注書確認日")
                arrFocusTargetCtrl.Add(TextHattyuusyoKakuninbi)
            End If
        End If

        Dim dtLogic As New DataLogic
        Dim intJituGaku As Integer = dtLogic.str2Int(TextJituSeikyuuGaku.Text.Replace(",", ""))

        '商品の請求有無と売上金額との関連チェック(請求無し・0円以外：NG / 請求あり・0円: 警告)
        If HiddenOpenValuesUriage.Value <> String.Empty Then
            If TextSyouhinCd.Text <> String.Empty Then
                If SelectSeikyuuUmu.SelectedValue = 0 And intJituGaku <> 0 Then
                    '請求無し・0円以外：NG
                    errMess += String.Format(Messages.MSG157E, typeWord)
                    arrFocusTargetCtrl.Add(SelectSeikyuuUmu)
                ElseIf SelectSeikyuuUmu.SelectedValue = 1 And intJituGaku = 0 Then
                    '請求あり・0円: 警告
                    seikyuuUmuErrMess += typeWord & "、"
                    arrFocusTargetCtrl.Add(TextJituSeikyuuGaku)
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' 基本請求先・仕入先情報の設定
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strTysKaisyaCd">調査会社コード</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <remarks></remarks>
    Public Sub SetDefaultSeikyuuSiireSakiInfo(ByVal strKameitenCd As String, ByVal strTysKaisyaCd As String, ByVal strKeiretuCd As String)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetDefaultSeikyuuSiireSakiInfo", _
                                                    strKameitenCd, _
                                                    strTysKaisyaCd)

        Dim strUriageZumi As String = String.Empty  '売上処理済み判断フラグ用

        '売上処理済チェック
        If Me.SelectUriageSyori.SelectedValue = "1" Then
            strUriageZumi = Me.SelectUriageSyori.SelectedValue
        End If

        '系列コードの設定
        Me.HiddenKeiretuCd.Value = strKeiretuCd

        Me.SeikyuuSiireLinkCtrl.SetVariableValueCtrlFromParent(strKameitenCd _
                                                                , Me.TextSyouhinCd.Text _
                                                                , strTysKaisyaCd _
                                                                , strUriageZumi _
                                                                , _
                                                                , _
                                                                , Me.TextDenpyouUriageNengappi.Text)
    End Sub

    ''' <summary>
    ''' 請求タイプの設定
    ''' </summary>
    ''' <param name="enSeikyuuType">請求タイプ</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strSyouhinLine">カスタムイベント発生元ID</param>
    ''' <remarks></remarks>
    Public Sub SetSeikyuuType(ByVal enSeikyuuType As EarthEnum.EnumSeikyuuType _
                            , ByVal strKeiretuCd As String _
                            , ByVal strKameitenCd As String _
                            , Optional ByVal strSyouhinLine As String = "")

        ' 請求先情報を商品１コントロールへ設定する
        ' 実請求金額・工務店請求額・請求有無の変更時判定に使用します
        Me.SeikyuuType = cLogic.GetDisplayString(Integer.Parse(enSeikyuuType))
        Me.KeiretuCd = strKeiretuCd
        Me.KameitenCd = strKameitenCd
        Me.UpdateSyouhinPanel.Update()
    End Sub

#End Region

End Class
