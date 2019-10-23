
Partial Public Class TeibetuIraiNaiyouCtrl
    Inherits System.Web.UI.UserControl

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim masterAjaxSM As New ScriptManager

    ''' <summary>
    ''' メッセージクラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic
    Dim cLogic As New CommonLogic
    Dim jLogic As New JibanLogic
    Dim cbLogic As New CommonBizLogic

#Region "プロパティ"

#Region "依頼情報"
    ''' <summary>
    ''' 依頼情報
    ''' </summary>
    ''' <value></value>
    ''' <returns> 依頼情報リンクID</returns>
    ''' <remarks></remarks>
    Public Property IraiTBody() As HtmlGenericControl
        Get
            Return TBodyIrai
        End Get
        Set(ByVal value As HtmlGenericControl)
            TBodyIrai = value
        End Set
    End Property
#End Region

#Region "依頼情報タイトル部情報ID"
    ''' <summary>
    ''' 依頼情報タイトル部情報ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> 依頼情報タイトル部情報ID</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IraiTitleInfobarID() As String
        Get
            Return IraiTitleInfobar.ClientID
        End Get
    End Property
#End Region

#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 区分</returns>
    ''' <remarks></remarks>
    Public Property Kbn() As String
        Get
            Return HiddenKbn.Value
        End Get
        Set(ByVal value As String)
            HiddenKbn.Value = value
        End Set
    End Property
#End Region

#Region "加盟店ｺｰﾄﾞ"
    ''' <summary>
    ''' 加盟店ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    Public Property KameitenCd() As String
        Get
            Return TextKameitenCd.Value
        End Get
        Set(ByVal value As String)
            TextKameitenCd.Value = value
        End Set
    End Property
#End Region

#Region "加盟店コード"
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店コード</returns>
    ''' <remarks></remarks>
    Public Property KameitenCdBox() As HtmlInputText
        Get
            Return TextKameitenCd
        End Get
        Set(ByVal value As HtmlInputText)
            TextKameitenCd = value
        End Set
    End Property
#End Region

#Region "営業所コード"
    ''' <summary>
    ''' 営業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 営業所コード</returns>
    ''' <remarks></remarks>
    Public Property EigyousyoCd() As String
        Get
            Return HiddenEigyousyoCd.Value
        End Get
        Set(ByVal value As String)
            HiddenEigyousyoCd.Value = value
        End Set
    End Property
#End Region

#Region "系列コード"
    ''' <summary>
    ''' 系列コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 系列コード</returns>
    ''' <remarks></remarks>
    Public Property KeiretuCd() As String
        Get
            Return HiddenKeiretuCd.Value
        End Get
        Set(ByVal value As String)
            HiddenKeiretuCd.Value = value
        End Set
    End Property
#End Region

#Region "調査会社コード"
    ''' <summary>
    ''' 調査会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社コード</returns>
    ''' <remarks></remarks>
    Public Property TyousaKaishaCdBox() As HtmlInputText
        Get
            Return TextTyousaKaishaCd
        End Get
        Set(ByVal value As HtmlInputText)
            TextTyousaKaishaCd = value
        End Set
    End Property
#End Region

#Region "調査会社ｺｰﾄﾞ"
    ''' <summary>
    ''' 調査会社ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    Public Property TysKaisyaCd() As String
        Get
            Return HiddenTysKaisyaCd.Value
        End Get
        Set(ByVal value As String)
            HiddenTysKaisyaCd.Value = value
            TextTyousaKaishaCd.Value = HiddenTysKaisyaCd.Value.Trim() & HiddenTysKaisyaJigyousyoCd.Value.Trim()
        End Set
    End Property
#End Region

#Region "調査会社事業所ｺｰﾄﾞ"
    Private strTysKaisyaJigyousyoCd As String
    ''' <summary>
    ''' 調査会社事業所ｺｰﾄﾞ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社事業所ｺｰﾄﾞ</returns>
    ''' <remarks></remarks>
    Public Property TysKaisyaJigyousyoCd() As String
        Get
            Return HiddenTysKaisyaJigyousyoCd.Value
        End Get
        Set(ByVal value As String)
            HiddenTysKaisyaJigyousyoCd.Value = value
            TextTyousaKaishaCd.Value = HiddenTysKaisyaCd.Value.Trim() & HiddenTysKaisyaJigyousyoCd.Value.Trim()
        End Set
    End Property
#End Region

#Region "ReportIF進捗ステータス名称"
    ''' <summary>
    ''' ReportIF進捗ステータス名称
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF進捗ステータス名称</returns>
    ''' <remarks></remarks>
    Public Property StatusIfName() As String
        Get
            Return TextStatusIf.Value
        End Get
        Set(ByVal value As String)
            TextStatusIf.Value = value
        End Set
    End Property
#End Region

#Region "ReportIF進捗ステータスコード"
    ''' <summary>
    ''' ReportIF進捗ステータスコード
    ''' </summary>
    ''' <value></value>
    ''' <returns> ReportIF進捗ステータスコード </returns>
    ''' <remarks></remarks>
    Public Property StatusIfCd() As String
        Get
            Return HiddenStatusIf.Value
        End Get
        Set(ByVal value As String)
            HiddenStatusIf.Value = value
        End Set
    End Property
#End Region

#Region "同時依頼棟数"
    ''' <summary>
    ''' 同時依頼棟数
    ''' </summary>
    ''' <remarks></remarks>
    Private _doujiIraiTousuu As Integer
    ''' <summary>
    ''' 同時依頼棟数
    ''' </summary>
    ''' <value></value>
    ''' <returns> 同時依頼棟数</returns>
    ''' <remarks></remarks>
    Public Property DoujiIraiTousuu() As Integer
        Get
            If TextDoujiIraiTousuu.Value = String.Empty Then
                Return 1
            Else
                Return Integer.Parse(TextDoujiIraiTousuu.Value.Replace(",", ""))
            End If
        End Get
        Set(ByVal value As Integer)
            _doujiIraiTousuu = value
        End Set
    End Property
    Public Property DoujiIraiTousuuData() As Integer
        Get
            Return _doujiIraiTousuu
        End Get
        Set(ByVal value As Integer)
            value = _doujiIraiTousuu
        End Set
    End Property
#End Region

#Region "建物用途NO"
    ''' <summary>
    ''' 建物用途NO
    ''' </summary>
    ''' <remarks></remarks>
    Private _tatemonoYoutoNo As Integer
    ''' <summary>
    ''' 建物用途NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 建物用途NO</returns>
    ''' <remarks></remarks>
    Public Property TatemonoYoutoNo() As Integer
        Get
            Return IIf(SelectTatemonoYouto.SelectedValue = "", 99, Integer.Parse(SelectTatemonoYouto.SelectedValue))
        End Get
        Set(ByVal value As Integer)
            _tatemonoYoutoNo = value
        End Set
    End Property
#End Region

#Region "調査方法"
    ''' <summary>
    ''' 調査方法
    ''' </summary>
    ''' <remarks></remarks>
    Private _tysHouhou As Integer
    ''' <summary>
    ''' 調査方法
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査方法</returns>
    ''' <remarks></remarks>
    Public Property TysHouhou() As Integer
        Get
            ' 調査方法は必須
            If SelectTyousaHouhou.SelectedValue = "" Then
                Return 99
            Else
                Return Integer.Parse(SelectTyousaHouhou.SelectedValue)
            End If
        End Get
        Set(ByVal value As Integer)
            _tysHouhou = value
        End Set
    End Property
    Public Property TysHouhouData() As Integer
        Get
            Return _tysHouhou
        End Get
        Set(ByVal value As Integer)
            _tysHouhou = value
        End Set
    End Property
#End Region

#Region "調査方法(外部からのアクセス用)"
    ''' <summary>
    ''' 外部からのアクセス用 for SelectTyousaHouhou
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccTysHouhou() As DropDownList
        Get
            Return SelectTyousaHouhou
        End Get
        Set(ByVal value As DropDownList)
            SelectTyousaHouhou = value
        End Set
    End Property
#End Region

#Region "調査概要"
    ''' <summary>
    ''' 調査概要
    ''' </summary>
    ''' <remarks></remarks>
    Private _tysGaiyou As Integer
    ''' <summary>
    ''' 調査概要
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査概要</returns>
    ''' <remarks></remarks>
    Public Property TysGaiyou() As Integer
        Get
            ' 調査概要未選択は9なのでNullは9にしておく
            If SelectTyousaGaiyou.Value = "" Then
                Return 9
            Else
                Return Integer.Parse(SelectTyousaGaiyou.Value)
            End If
        End Get
        Set(ByVal value As Integer)
            _tysGaiyou = value
        End Set
    End Property
    Public Property TysGaiyouData() As Integer
        Get
            Return _tysGaiyou
        End Get
        Set(ByVal value As Integer)
            _tysGaiyou = value
        End Set
    End Property
#End Region

#Region "商品1コード"
    ''' <summary>
    ''' 商品1コード
    ''' </summary>
    ''' <remarks></remarks>
    Private _syouhin1 As String
    ''' <summary>
    ''' 商品1コード
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品1コード</returns>
    ''' <remarks></remarks>
    Public Property Syouhin1() As String
        Get
            Return SelectSyouhin1.SelectedValue
        End Get
        Set(ByVal value As String)
            _syouhin1 = value
        End Set
    End Property
    Public Property Syouhin1Data() As String
        Get
            Return _syouhin1
        End Get
        Set(ByVal value As String)
            _syouhin1 = value
        End Set
    End Property
#End Region

#Region "商品1コード(外部からのアクセス用)"
    ''' <summary>
    ''' 外部からのアクセス用 for SelectSyouhin1
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccSelectSyouhin1() As DropDownList
        Get
            Return SelectSyouhin1
        End Get
        Set(ByVal value As DropDownList)
            SelectSyouhin1 = value
        End Set
    End Property
#End Region

#Region "商品1名称"
    ''' <summary>
    ''' 商品1名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _syouhin1Mei As String
    ''' <summary>
    ''' 商品1名称
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品1名称</returns>
    ''' <remarks></remarks>
    Public Property Syouhin1Mei() As String
        Get
            Return SelectSyouhin1.Text
        End Get
        Set(ByVal value As String)
            _syouhin1Mei = value
        End Set
    End Property
#End Region

#Region "商品区分"
    ''' <summary>
    ''' 商品区分
    ''' </summary>
    ''' <remarks></remarks>
    Private _syouhinKbn As Integer
    ''' <summary>
    ''' 商品区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 商品区分</returns>
    ''' <remarks></remarks>
    Public Property SyouhinKbn() As Integer
        Get
            If RadioSyouhinKbn1.Checked Then
                Return 1
            ElseIf RadioSyouhinKbn2.Checked Then
                Return 2
            ElseIf RadioSyouhinKbn3.Checked Then
                Return 3
            Else
                Return 9
            End If
        End Get
        Set(ByVal value As Integer)
            _syouhinKbn = value

            RadioSyouhinKbn1.Checked = False
            RadioSyouhinKbn2.Checked = False
            RadioSyouhinKbn3.Checked = False
            RadioSyouhinKbn9.Checked = False

            If value = 1 Then
                RadioSyouhinKbn1.Checked = True
            ElseIf value = 2 Then
                RadioSyouhinKbn2.Checked = True
            ElseIf value = 3 Then
                RadioSyouhinKbn3.Checked = True
            ElseIf value = 9 Then
                RadioSyouhinKbn9.Checked = True
            End If
        End Set
    End Property
#End Region

#Region "商品１請求額のUpdatePanel"
    ''' <summary>
    ''' 商品１請求額のUpdatePanel
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品１請求額のUpdatePanel</returns>
    ''' <remarks></remarks>
    Public Property UpdateSyouhin1Seikyuu() As UpdatePanel
        Get
            Return UpdatePanelSyouhin1Seikyuu
        End Get
        Set(ByVal value As UpdatePanel)
            UpdatePanelSyouhin1Seikyuu = value
        End Set
    End Property
#End Region

#Region "商品１実請求額"
    ''' <summary>
    ''' 商品１実請求額
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品１実請求額</returns>
    ''' <remarks></remarks>
    Public Property Syouhin1JituSeikyuuGaku() As String
        Get
            Return HiddenJituSeikyuuGaku.Value
        End Get
        Set(ByVal value As String)
            HiddenJituSeikyuuGaku.Value = value
        End Set
    End Property
#End Region

#Region "商品１工務店請求額"
    ''' <summary>
    ''' 商品１工務店請求額
    ''' </summary>
    ''' <value></value>
    ''' <returns>商品１工務店請求額</returns>
    ''' <remarks></remarks>
    Public Property Syouhin1KoumutenSeikyuuGaku() As String
        Get
            Return HiddenKoumutenSeikyuugaku.Value
        End Get
        Set(ByVal value As String)
            HiddenKoumutenSeikyuugaku.Value = value
        End Set
    End Property
#End Region

#Region "経理業務権限"
    ''' <summary>
    ''' 経理業務権限
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KeiriGyoumuKengen() As Integer
        Get
            Return HiddenKeiriGyoumuKengen.Value
        End Get
        Set(ByVal value As Integer)
            HiddenKeiriGyoumuKengen.Value = value
        End Set
    End Property
#End Region

#Region "商品１発注金額"
    ''' <summary>
    ''' 商品１発注金額
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Syouhin1HattyuuKingaku() As String
        Get
            Return HiddenHattyuuKingaku.Value
        End Get
        Set(ByVal value As String)
            HiddenHattyuuKingaku.Value = value
        End Set
    End Property
#End Region

#Region "商品１の請求先情報"
    '商品1の請求先コード
    Private strSyouhin1SeikyuuSakiCd As String
    ''' <summary>
    ''' 商品1の請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Syouhin1SeikyuuSakiCd() As String
        Get
            Return strSyouhin1SeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strSyouhin1SeikyuuSakiCd = value
        End Set
    End Property

    '商品1の請求先枝番
    Private strSyouhin1SeikyuuSakiBrc As String
    ''' <summary>
    ''' 商品1の請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Syouhin1SeikyuuSakiBrc() As String
        Get
            Return strSyouhin1SeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strSyouhin1SeikyuuSakiBrc = value
        End Set
    End Property

    '商品1の請求先区分
    Private strSyouhin1SeikyuuSakiKbn As String
    ''' <summary>
    ''' 商品1の請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Syouhin1SeikyuuSakiKbn() As String
        Get
            Return strSyouhin1SeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSyouhin1SeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#End Region

#Region "カスタムイベント"
    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント（商品１設定用）
    ''' </summary>
    ''' <remarks>
    ''' 実装サンプル１
    ''' <example>
    ''' ●本クラスに実装するコードの例
    ''' <code>
    ''' ' 親画面のメソッド実行<br/>
    ''' <b>RaiseEvent Syouhin1SetAction(Me, e, syouhin1Rec)</b>
    ''' </code>
    ''' </example><br/>
    ''' 実装サンプル２
    ''' <example>
    ''' ●親画面に実装するメソッドの例です
    ''' <code>
    ''' ''' <summary><br/>
    ''' ''' 依頼コントロールで取得した商品１自動設定情報を商品１コントロールに反映する<br/>
    ''' ''' </summary><br/>
    ''' ''' <param name="sender"></param><br/>
    ''' ''' <param name="e"></param><br/>
    ''' ''' <param name="syouhinRec"></param><br/>
    ''' ''' <remarks></remarks><br/>
    ''' Private Sub SetSyouhin1Data(ByVal sender As System.Object, _<br/>
    '''                             ByVal e As System.EventArgs, _<br/>
    '''                             ByVal syouhinRec As Syouhin1AutoSetRecord, _<br/>
    '''                             ByVal dicIraiInfo As Dictionary(Of String, String)) Handles IraiNaiyou.Syouhin1SetAction<br/><br/><br/>
    '''     ' 処理を実装します<br/><br/>
    ''' End Sub<br/>
    ''' </code>
    ''' </example>
    ''' </remarks>
    Public Event Syouhin1SetAction(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs, _
                                ByVal syouhinRec As Syouhin1AutoSetRecord)

    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント（加盟店変更時の系列情報）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="keiretuCd">系列コード</param>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <remarks></remarks>
    Public Event KeiretuSetAction(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal keiretuCd As String, _
                                      ByVal kameitenCd As String)

    ''' <summary>
    ''' 商品１の請求先情報を取得
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event GetSyouhin1SeikyuuSakiInfo(ByVal sender As System.Object _
                                            , ByVal e As System.EventArgs)
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
    ''' 商品１の発注書金額要求用カスタムイベント
    ''' </summary>
    ''' <remarks></remarks>
    Public Event GetHattyuuKingaku(ByVal sender As System.Object _
                                    , ByVal e As System.EventArgs)

    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント（請求先・仕入先情報設定用）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">画面ID</param>
    ''' <remarks></remarks>
    Public Event SetSeikyuuSiireSakiAction(ByVal sender As System.Object _
                                            , ByVal e As System.EventArgs _
                                            , ByVal strId As String)

    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント（工事価格マスタ取得アクション用）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">画面ID</param>
    ''' <remarks></remarks>
    Public Event GetKojMInfoAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)

    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント（工事価格設定用）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">画面ID</param>
    ''' <remarks></remarks>
    Public Event SetKojMInfoAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)

    ''' <summary>
    ''' 親画面の処理実行用イベント（原価・販売価格マスタ存在チェック）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId"></param>
    ''' <remarks></remarks>
    Public Event CheckGenkaHanbaiKkkMasterAction(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs, _
                                  ByVal strId As String)

#End Region

#Region "イベントハンドラ"
    ''' <summary>
    ''' ページロード時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim MyLogic As New JibanLogic

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        If Not IsPostBack Then

            ' ドロップダウンリスト設定
            SetDropDownData()

            ' プルダウン連動コード呼び出し
            SetPullCdScript()

            ' 初期データ設定
            ' 同時依頼棟数
            TextDoujiIraiTousuu.Value = IIf(_doujiIraiTousuu = 0, 1, _doujiIraiTousuu)
            ' 建物用途
            TextTatemonoYoutoCd.Value = IIf(_tatemonoYoutoNo < 1, 1, _tatemonoYoutoNo)
            SelectTatemonoYouto.SelectedValue = TextTatemonoYoutoCd.Value
            '調査方法のDDL表示処理
            cLogic.ps_SetSelectTextBoxTysHouhou(_tysHouhou, Me.SelectTyousaHouhou, False)
            TextTyousaHouhouCd.Value = Me.SelectTyousaHouhou.SelectedValue
            HiddenTyousaHouhou.Value = TextTyousaHouhouCd.Value

            ' 調査概要
            SelectTyousaGaiyou.Value = _tysGaiyou
            HiddenTyousaGaiyou.Value = SelectTyousaGaiyou.Value
            ' 商品区分
            RadioSyouhinKbn1.Checked = (RadioSyouhinKbn1.Value = _syouhinKbn.ToString())
            RadioSyouhinKbn2.Checked = (RadioSyouhinKbn2.Value = _syouhinKbn.ToString())
            RadioSyouhinKbn3.Checked = (RadioSyouhinKbn3.Value = _syouhinKbn.ToString())
            RadioSyouhinKbn9.Checked = (RadioSyouhinKbn9.Value = _syouhinKbn.ToString())
            HiddenSyouhinKbn.Value = SyouhinKbn.ToString()
            ' 商品1
            HiddenSyouhin1Pre.Value = _syouhin1
            If cLogic.ChkDropDownList(SelectSyouhin1, cLogic.GetDispNum(_syouhin1)) Then
                'DDLにあれば、選択する
                SelectSyouhin1.SelectedValue = cLogic.GetDispNum(_syouhin1)
            Else
                'DDLになければ、アイテムを追加
                SelectSyouhin1.Items.Add(New ListItem(_syouhin1 & ":" & _syouhin1Mei, _syouhin1))
                SelectSyouhin1.SelectedValue = cLogic.GetDispNum(_syouhin1)     '選択状態
            End If

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            SetDispAction(sender, e)

            '商品区分の値によって、表示を切り替える
            CheckSyouhinkubun()

            ' 加盟店情報の設定
            ButtonKameitenKensaku_ServerClick(sender, e)

            ' 調査会社設定
            ButtonTyousaKaishaKensaku_ServerClick(sender, e)

            ' 依頼内容タイトル部、情報設定
            SetIraiTitleInfo()

            IraiDispLink.HRef = "javascript:changeDisplayIrai('" & _
                                 TBodyIrai.ClientID & _
                                 "');changeDisplay('" & _
                                 IraiTitleInfobar.ClientID & _
                                 "');"

            ' 経理権限が無い場合、非活性
            If HiddenKeiriGyoumuKengen.Value = "0" Then
                EnableHtmlInput(TextKameitenCd, False)
                EnableHtmlInput(TextDoujiIraiTousuu, False)
                EnableHtmlInput(TextTatemonoYoutoCd, False)
                EnableHtmlInput(TextTyousaKaishaCd, False)
                EnableHtmlInput(TextTyousaHouhouCd, False)
                EnableDropDownList(SelectSyouhin1, False)
                EnableDropDownList(SelectTatemonoYouto, False)
                EnableDropDownList(SelectTyousaHouhou, False)
                EnableHtmlSelect(SelectTyousaGaiyou, False)
                EnableRadio(RadioSyouhinKbn1, False)
                EnableRadio(RadioSyouhinKbn2, False)
                EnableRadio(RadioSyouhinKbn3, False)
                EnableRadio(RadioSyouhinKbn9, False)

                ButtonKameitenKensaku.Visible = False
                ButtonKameitenTyuuijouhou.Visible = False
                ButtonTyousaKaishaKensaku.Visible = False

            End If

            '画面表示時点の値を、Hiddenに保持(仕入 変更チェック用)
            If Me.HiddenOpenValue.Value = String.Empty Then
                Me.HiddenOpenValue.Value = Me.getCtrlValuesStringAll()
            End If

        End If

    End Sub

    ''' <summary>
    ''' 加盟店検索ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKameitenKensaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' 入力されているコードをキーに、マスタを検索し、データを1件のみ取得できた場合は
        ' 画面情報を更新する。データが無い場合は、検索画面を表示する
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of KameitenSearchRecord)

        Dim recData As New KameitenSearchRecord

        ' 初回起動時のみ
        If IsPostBack = False Then
            blnTorikesi = False
        End If

        ' 取得件数を絞り込む場合、引数を追加してください
        If TextKameitenCd.Value <> "" Then '入力有
            recData = kameitenSearchLogic.GetKameitenSearchResult(HiddenKbn.Value, _
                                                                  TextKameitenCd.Value, _
                                                                  "", _
                                                                  blnTorikesi)
        End If

        '該当有
        If Not recData.KameitenCd Is Nothing Then

            '●ビルダー注意事項チェック
            If kameitenSearchLogic.ChkBuilderData13(recData.KameitenCd) Then
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.TrueString
            Else
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.FalseString
            End If

            '●ビルダー注意事項チェック
            Dim strErrMsg As String = String.Empty
            If cbLogic.ChkErrBuilderData(Me.SelectTyousaGaiyou.Value _
                                                    , Me.TextKameitenCd.Value _
                                                    , Me.HiddenKameitenTyuuiJikou.Value _
                                                    , strErrMsg) = False Then
                '商品１が取得できていても、ビルダー注意事項チェックでNGの場合、処理終了(関連情報を更新する前に終了する)
                MLogic.AlertMessage(sender, strErrMsg, 0, "Syouhin1SetError")
                Me.TextKameitenCd.Value = Me.HiddenkameitenCdOld.Value

                Me.UpdatePanelKameiten.Update()
                Exit Sub
            End If

            HiddenkameitenCdOld.Value = recData.KameitenCd
            TextKameitenMei.Value = recData.KameitenMei1
            TextKeiretuNm.Value = recData.KeiretuMei
            TextEigyousyoMei.Value = recData.EigyousyoMei
            Me.HiddenEigyousyoCd.Value = recData.EigyousyoCd
            HiddenKeiretuCd.Value = recData.KeiretuCd
            HiddenTysSeikyuuSaki.Value = recData.TysSeikyuuSaki
            HiddenHansokuSeikyuuSaki.Value = recData.HansokuhinSeikyuuSaki
            SpanTyousaMitumoriFlg.InnerText = recData.TysMitsyoMsg
            SpanHattyuusyoFlg.InnerText = recData.HattyuusyoMsg
            TextJioSakiFlg.Value = IIf(recData.JioSakiFLG = 1, EarthConst.JIO_SAKI, String.Empty)

            '********************************************************************
            '★初期読み以外、工事価格マスタから価格取得
            If IsPostBack = True Then

                '親画面へイベント通知(工事商品価格設定)
                RaiseEvent GetKojMInfoAction(sender, e, Me.ClientID)
                RaiseEvent SetKojMInfoAction(sender, e, Me.ClientID)
            End If

            '加盟店取消理由設定
            setTorikesiRiyuu(recData.Kbn, recData.KameitenCd)

            'フォーカスセット
            SetFocus(ButtonKameitenKensaku)

        Else '該当無

            '●ビルダー注意事項フラグ初期化
            Me.HiddenKameitenTyuuiJikou.Value = String.Empty

            If blnTorikesi = False Then
                ' 初期起動時は検索しない
                Return
            End If

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpScript As String = "callSearch('" & HiddenKbn.ClientID & _
                                                       EarthConst.SEP_STRING & _
                                                       TextKameitenCd.ClientID & _
                                                       "','" & _
                                                       UrlConst.SEARCH_KAMEITEN & _
                                                       "','" & _
                                                       TextKameitenCd.ClientID & _
                                                       EarthConst.SEP_STRING & _
                                                       TextKameitenMei.ClientID & _
                                                       "','" & _
                                                       ButtonKameitenKensaku.ClientID & "');"

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)

            '検索結果より再検索を実行する
            If TextKameitenCd.Value <> "" Then '入力有
                recData = kameitenSearchLogic.GetKameitenSearchResult(HiddenKbn.Value, _
                                                                      TextKameitenCd.Value, _
                                                                      "", _
                                                                      blnTorikesi)
                '該当有
                If Not recData.KameitenCd Is Nothing Then

                    '●ビルダー注意事項チェック
                    If kameitenSearchLogic.ChkBuilderData13(recData.KameitenCd) Then
                        Me.HiddenKameitenTyuuiJikou.Value = Boolean.TrueString
                    Else
                        Me.HiddenKameitenTyuuiJikou.Value = Boolean.FalseString
                    End If

                    '●ビルダー注意事項チェック
                    Dim strErrMsg As String = String.Empty
                    If cbLogic.ChkErrBuilderData(Me.SelectTyousaGaiyou.Value, Me.TextKameitenCd.Value, Me.HiddenKameitenTyuuiJikou.Value, strErrMsg) = False Then
                        '商品１が取得できていても、ビルダー注意事項チェックでNGの場合、処理終了(関連情報を更新する前に終了する)
                        MLogic.AlertMessage(sender, strErrMsg, 0, "Syouhin1SetError")
                        Me.TextKameitenCd.Value = Me.HiddenkameitenCdOld.Value

                        Me.UpdatePanelKameiten.Update()
                        Exit Sub
                    End If

                    HiddenkameitenCdOld.Value = recData.KameitenCd
                    TextKameitenMei.Value = recData.KameitenMei1
                    TextKeiretuNm.Value = recData.KeiretuMei
                    HiddenKeiretuCd.Value = recData.KeiretuCd
                    HiddenTysSeikyuuSaki.Value = recData.TysSeikyuuSaki
                    HiddenHansokuSeikyuuSaki.Value = recData.HansokuhinSeikyuuSaki
                    TextEigyousyoMei.Value = recData.EigyousyoMei
                    Me.HiddenEigyousyoCd.Value = recData.EigyousyoCd
                    SpanTyousaMitumoriFlg.InnerText = recData.TysMitsyoMsg
                    SpanHattyuusyoFlg.InnerText = recData.HattyuusyoMsg
                    TextJioSakiFlg.Value = IIf(recData.JioSakiFLG = 1, EarthConst.JIO_SAKI, String.Empty)

                Else '該当無
                    HiddenkameitenCdOld.Value = ""
                    TextKameitenMei.Value = ""
                    TextKeiretuNm.Value = ""
                    HiddenKeiretuCd.Value = ""
                    HiddenTysSeikyuuSaki.Value = ""
                    HiddenHansokuSeikyuuSaki.Value = ""
                    TextEigyousyoMei.Value = ""
                    Me.HiddenEigyousyoCd.Value = ""
                    SpanTyousaMitumoriFlg.InnerText = ""
                    SpanHattyuusyoFlg.InnerText = ""
                    TextJioSakiFlg.Value = ""
                End If

            Else '入力無
                HiddenkameitenCdOld.Value = ""
                TextKameitenMei.Value = ""
                TextKeiretuNm.Value = ""
                TextEigyousyoMei.Value = ""
                Me.HiddenEigyousyoCd.Value = ""
                SpanTyousaMitumoriFlg.InnerText = ""
                SpanHattyuusyoFlg.InnerText = ""
                TextJioSakiFlg.Value = ""
            End If
        End If

        '初回起動時以外
        If IsPostBack = True Then
            '商品１再計算
            ButtonSetSyouhin1_ServerClick(sender, e)
        End If

    End Sub

    ''' <summary>
    ''' 調査会社検索ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTyousaKaishaKensaku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        ' 初回起動時のみ
        If IsPostBack = False Then
            blnTorikesi = False
        End If

        If TextTyousaKaishaCd.Value <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetTyousakaisyaSearchResult(TextTyousaKaishaCd.Value, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi, _
                                                                            TextKameitenCd.Value)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            'HiddenTysKaisyaCdOld.Value = recData.TysKaisyaCd & recData.JigyousyoCd
            'HiddenTysKaisyaNmOld.Value = recData.TysKaisyaMei
            TextTyousaKaishaNm.Value = recData.TysKaisyaMei
            TysKaisyaCd = recData.TysKaisyaCd
            TysKaisyaJigyousyoCd = recData.JigyousyoCd
            ' 調査会社NG設定
            If recData.KahiKbn = 9 Then
                HiddenTysKaisyaNg.Value = EarthConst.TYOUSA_KAISYA_NG
                TextTyousaKaishaCd.Style("color") = "red"
                TextTyousaKaishaNm.Style("color") = "red"
            Else
                HiddenTysKaisyaNg.Value = String.Empty
                TextTyousaKaishaCd.Style("color") = "blue"
                TextTyousaKaishaNm.Style("color") = "blue"
            End If

            'フォーカスセット
            SetFocus(ButtonTyousaKaishaKensaku)
        Else
            '表示色を初期化
            TextTyousaKaishaCd.Style.Remove("color")
            TextTyousaKaishaNm.Style.Remove("color")

            '調査会社コードOld、調査会社名をクリア
            'HiddenTysKaisyaCdOld.Value = String.Empty
            TextTyousaKaishaNm.Value = String.Empty

            If blnTorikesi = False Then
                ' 初期起動時は検索しない
                Return
            End If

            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpScript = "callSearch('" & TextTyousaKaishaCd.ClientID & _
                                             EarthConst.SEP_STRING & _
                                             TextKameitenCd.ClientID & _
                                             "','" & _
                                             UrlConst.SEARCH_TYOUSAKAISYA & _
                                             "','" & _
                                             TextTyousaKaishaCd.ClientID & _
                                             EarthConst.SEP_STRING & _
                                             TextTyousaKaishaNm.ClientID & _
                                             EarthConst.SEP_STRING & _
                                             HiddenTysKaisyaNg.ClientID & _
                                             "','" & _
                                             ButtonTyousaKaishaKensaku.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)

        End If

        '原価・販売価格マスタへの存在チェック
        RaiseEvent CheckGenkaHanbaiKkkMasterAction(sender, e, Me.ClientID)

        If TextTyousaKaishaNm.Value <> String.Empty Then
            HiddenTysKaisyaCdOld.Value = TextTyousaKaishaCd.Value
            HiddenTysKaisyaNmOld.Value = TextTyousaKaishaNm.Value
        End If

        '親画面の請求先・仕入先情報画面用情報のセットメソッドを実行
        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetDispAction(ByVal sender As Object, ByVal e As System.EventArgs)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 他システムへのリンクボタン設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '加盟店注意事項
        cLogic.getKameitenTyuuijouhouPath(TextKameitenCd.ClientID, ButtonKameitenTyuuijouhou)

        '商品１設定関連ののonchangeイベントハンドラを設定
        SetSyouhin1Script()

        'イベントハンドラを設定
        TextKameitenCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){checkNumber(this);}"
        TextTatemonoYoutoCd.Attributes("onblur") += "if(checkTempValueForOnBlur(this)){checkNumber(this);}"
        TextTyousaKaishaCd.Attributes("onblur") = "if(checkTempValueForOnBlur(this)){checkNumber(this);}"
        TextTyousaHouhouCd.Attributes("onblur") += "if(checkTempValueForOnBlur(this)){checkNumber(this);}"

        TextKameitenCd.Attributes("onkeydown") = "if (event.keyCode == 13){return false;};"
        TextDoujiIraiTousuu.Attributes("onkeydown") = "if (event.keyCode == 13){return false;};"
        TextTatemonoYoutoCd.Attributes("onkeydown") = "if (event.keyCode == 13){return false;};"
        TextTyousaKaishaCd.Attributes("onkeydown") = "if (event.keyCode == 13){return false;};"
        TextTyousaHouhouCd.Attributes("onkeydown") = "if (event.keyCode == 13){return false;};"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 進捗ステータスによって、調査会社の編集可否を切り替え
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        If StatusIfCd > EarthConst.IF_STATUS_TOUROKU_ZUMI Then
            Dim jSM As New JibanSessionManager 'セッション管理クラス
            Dim ht As New Hashtable
            jSM.Hash2Ctrl(UpdatePanelTysKaisya, EarthConst.MODE_VIEW, ht)
            ButtonTyousaKaishaKensaku.Disabled = True
            ButtonTyousaKaishaKensaku.Visible = False
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cLogic.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing)

    End Sub

    ''' <summary>
    ''' 商品１の自動設定ボタン押下時イベント<br/>
    ''' 加盟店コード、建物用途の変更時に押下されます<br/>
    ''' 本ボタン押下はaspx内のJavaScriptで実施 [ function callSetSyouhin1(objThis) ]<br/>
    ''' 上記スクリプトへの設定はこのファイルの [ SetSyouhin1Script() ] で実施しています<br/>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSetSyouhin1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Syouhin1Set(sender, e)

    End Sub

    ''' <summary>
    ''' 商品１の自動設定ボタン押下時イベント(調査概要セット処理も行なう)<br/>
    ''' 商品区分、調査方法の変更時に押下されます<br/>
    ''' 本ボタン押下はaspx内のJavaScriptで実施 [ function callSetSyouhin1TysGaiyou(objThis) ]<br/>
    ''' 上記スクリプトへの設定はこのファイルの [ SetSyouhin1Script() ] で実施しています<br/>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSetSyouhin1TysGaiyou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Syouhin1Set(sender, e, True)

    End Sub

#End Region

#Region "プライベートメソッド"
    ''' <summary>
    ''' ドロップダウンリスト設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDropDownData()

        Dim helper As New DropDownHelper
        'Dim kbn_logic As New KubunLogic
        ' 調査方法コンボにデータをバインドする
        helper.SetDropDownList(SelectTyousaHouhou, DropDownHelper.DropDownType.TyousaHouhou, True, False)
        ' 商品1コンボにデータをバインドする
        helper.SetDropDownList(SelectSyouhin1, DropDownHelper.DropDownType.Syouhin1, True, True, 0, False)
        ' 調査概要コンボにデータをバインドする
        helper.SetDropDownList(SelectTyousaGaiyou, DropDownHelper.DropDownType.TyousaGaiyou, True)
        ' 建物用途コンボにデータをバインドする
        helper.SetDropDownList(SelectTatemonoYouto, DropDownHelper.DropDownType.TatemonoYouto, True, False)

    End Sub

    ''' <summary>
    ''' プルダウン連動コード入力項目スクリプト設定呼び出し
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetPullCdScript()

        Dim jBn As New Jiban '地盤画面クラス
        jBn.SetPullCdScriptSrc(TextTatemonoYoutoCd, SelectTatemonoYouto)    ' 建物用途
        jBn.SetPullCdScriptSrc(TextTyousaHouhouCd, SelectTyousaHouhou)      ' 調査方法

    End Sub

    ''' <summary>
    ''' 商品区分の値によって、表示を切り替える
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckSyouhinkubun()

        'ラジオボタンは非表示で商品区分は全てラベル化
        If RadioSyouhinKbn1.Checked Then
            ' 60年保証
            SpanSyouhinKbn1.Style("display") = "inline"
            SpanSyouhinKbn2.Style("display") = "none"
            SpanSyouhinKbn3.Style("display") = "none"
            SpanSyouhinKbn9.Style("display") = "none"
        ElseIf RadioSyouhinKbn2.Checked Then
            ' 土地販売
            SpanSyouhinKbn1.Style("display") = "none"
            SpanSyouhinKbn2.Style("display") = "inline"
            SpanSyouhinKbn3.Style("display") = "none"
            SpanSyouhinKbn9.Style("display") = "none"
        ElseIf RadioSyouhinKbn3.Checked Then
            ' リフォーム
            SpanSyouhinKbn1.Style("display") = "none"
            SpanSyouhinKbn2.Style("display") = "none"
            SpanSyouhinKbn3.Style("display") = "inline"
            SpanSyouhinKbn9.Style("display") = "none"
        Else
            ' 未設定は商品区分 9 
            SpanSyouhinKbn1.Style("display") = "none"
            SpanSyouhinKbn2.Style("display") = "none"
            SpanSyouhinKbn3.Style("display") = "none"
            SpanSyouhinKbn9.Style("display") = "inline"
        End If

    End Sub

    ''' <summary>
    ''' 商品１設定関連ののonchangeイベントハンドラを設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSyouhin1Script()

        '商品１設定関連ののonchangeイベントハンドラを設定
        Dim tmpScript As String = "callSetSyouhin1(this);"
        Dim tmpScript_TysGaiyou As String = "callSetSyouhin1TysGaiyou(this);"
        Dim ChkTysGaiyouScript As String = "callChkTysGaiyou(this)"
        Dim ChkBuilderScript As String = "callChkBuilder(this)"

        SelectTyousaHouhou.Attributes("onchange") += tmpScript_TysGaiyou      ' 調査方法
        SelectTatemonoYouto.Attributes("onchange") += tmpScript     ' 建物用途
        SelectTyousaGaiyou.Attributes("onchange") = "if(" & ChkBuilderScript & ")" & "if(" & ChkTysGaiyouScript & ");"  ' 調査概要
        SelectSyouhin1.Attributes("onchange") = tmpScript_TysGaiyou '商品1

        '同時依頼棟数
        Me.TextDoujiIraiTousuu.Attributes("onblur") = "if(checkTempValueForOnBlur_DoujiIrai(this)){if(checkNumberAddFig(this)) " & ChkTysGaiyouScript & "}else{checkNumberAddFig(this);}"
        Me.TextDoujiIraiTousuu.Attributes("onfocus") = "removeFig(this);setTempValueForOnBlur_DoujiIrai(this);"

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

    ''' <summary>
    ''' 依頼情報タイトル部の情報設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetIraiTitleInfo()

        IraiTitleInfobar.InnerHtml = "&nbsp;&nbsp;【" & _
                                     TextKameitenCd.Value & "】 【" & _
                                     TextKameitenMei.Value & "】 【" & _
                                     TextTyousaKaishaCd.Value & "】 【" & _
                                     TextTyousaKaishaNm.Value & "】"
    End Sub

    ''' <summary>
    ''' 商品コード１の設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Public Sub Syouhin1Set(ByVal sender As System.Object, _
                            ByVal e As System.EventArgs, _
                            Optional ByVal blnTysGaiyou As Boolean = False)

        ' 商品１設定情報取得
        Dim syouhin1Rec As Syouhin1AutoSetRecord = Nothing

        ' データ取得用ロジッククラス
        Dim logic As New JibanLogic
        ' データ取得用パラメータクラス
        Dim param_rec As New Syouhin1InfoRecord
        ' 取得レコード格納クラス
        Dim syouhin_rec As New Syouhin1AutoSetRecord
        ' 商品1取得ステータス
        Dim intSetSts As Integer = 0
        ' エラーメッセージ
        Dim strErrMsg As String = String.Empty

        ' データ取得用のパラメータセット
        param_rec.Kubun = HiddenKbn.Value    ' 区分

        If RadioSyouhinKbn1.Checked = True Then
            ' 60年保証(EARTHでは非表示)
            param_rec.SyouhinKbn = 1        ' 商品区分
        ElseIf RadioSyouhinKbn2.Checked = True Then
            ' 土地販売
            param_rec.SyouhinKbn = 2        ' 商品区分
        ElseIf RadioSyouhinKbn3.Checked = True Then
            ' リフォーム
            param_rec.SyouhinKbn = 3        ' 商品区分
        Else
            ' 未設定は商品区分 9 
            param_rec.SyouhinKbn = 9        ' 商品区分
        End If

        ' 建物用途
        CommonLogic.Instance.SetDisplayString(TextTatemonoYoutoCd.Value, param_rec.TatemonoYouto)
        ' 商品コード1
        CommonLogic.Instance.SetDisplayString(SelectSyouhin1.SelectedValue, param_rec.SyouhinCd)
        ' 調査方法
        CommonLogic.Instance.SetDisplayString(SelectTyousaHouhou.SelectedValue, param_rec.TyousaHouhouNo)
        ' 調査会社CD+事業所CD
        CommonLogic.Instance.SetDisplayString(TextTyousaKaishaCd.Value, param_rec.TysKaisyaCd)
        ' 同時依頼棟数
        CommonLogic.Instance.SetDisplayString(TextDoujiIraiTousuu.Value, param_rec.DoujiIraiTousuu)
        ' 加盟店コード
        param_rec.KameitenCd = TextKameitenCd.Value
        ' 系列コード
        param_rec.KeiretuCd = HiddenKeiretuCd.Value
        ' 営業所コード
        param_rec.EigyousyoCd = Me.HiddenEigyousyoCd.Value
        ' 系列フラグ
        param_rec.KeiretuFlg = logic.GetKeiretuFlg(HiddenKeiretuCd.Value)

        ' 商品レコードの税抜金額と工務店請求額をセットする
        CommonLogic.Instance.SetDisplayString(HiddenJituSeikyuuGaku.Value, _
                                              param_rec.ZeinukiKingaku1)        ' 実請求金額
        CommonLogic.Instance.SetDisplayString(HiddenKoumutenSeikyuugaku.Value, _
                                              param_rec.KoumutenKingaku1)       ' 工務店請求額
        '商品１の請求先情報を取得
        RaiseEvent GetSyouhin1SeikyuuSakiInfo(Me, e)

        '請求先情報の取得
        syouhin_rec.SeikyuuSakiCd = strSyouhin1SeikyuuSakiCd
        syouhin_rec.SeikyuuSakiBrc = strSyouhin1SeikyuuSakiBrc
        syouhin_rec.SeikyuuSakiKbn = strSyouhin1SeikyuuSakiKbn

        ' 商品１情報を取得し画面にセット
        If logic.GetSyouhin1Info(sender, param_rec, syouhin_rec, intSetSts) = True Then
            syouhin1Rec = syouhin_rec
            '商品1取得ステータスをセット
            syouhin1Rec.SetSts = intSetSts
        End If

        ' 商品コードが取得できず、発注金額入力＞０の場合、元に戻す
        If syouhin_rec.SyouhinCd Is Nothing Then

            RaiseEvent GetHattyuuKingaku(Me, e)

            If HiddenHattyuuKingaku.Value <> "0" Then
                TextTyousaHouhouCd.Value = HiddenTyousaHouhou.Value
                SelectTyousaHouhou.SelectedValue = HiddenTyousaHouhou.Value
                SelectTyousaGaiyou.Value = HiddenTyousaGaiyou.Value
                HiddenSyouhinKbn.Value = IIf(HiddenSyouhinKbn.Value = "", "9", HiddenSyouhinKbn.Value)
                SyouhinKbn = Integer.Parse(HiddenSyouhinKbn.Value)
                Me.SelectSyouhin1.SelectedValue = Me.HiddenSyouhin1Pre.Value

                ' クリアできないメッセージ
                ScriptManager.RegisterClientScriptBlock(sender, _
                                                        sender.GetType(), _
                                                        "alert", _
                                                        "alert('" & _
                                                        Messages.MSG010E & _
                                                        "')", True)

                HiddenTyousaHouhou.Value = TextTyousaHouhouCd.Value
                HiddenTyousaGaiyou.Value = SelectTyousaGaiyou.Value
                HiddenSyouhinKbn.Value = SyouhinKbn.ToString()
                Me.HiddenSyouhin1Pre.Value = Me.SelectSyouhin1.SelectedValue

                UpdatePanelSyouhinKbn.Update()
                UpdatePanelTyousaHouhou.Update()
                UpdatePanelTyousaGaiyou.Update()
                Me.UpdatePanelSyouhin1.Update()

                ' 処理中断
                Exit Sub
            End If

        End If

        '原価マスタ取得不可の場合は、エラーメッセージのみ表示
        If intSetSts = EarthEnum.emSyouhin1Error.GetGenka Then
            strErrMsg += cbLogic.GetSyouhin1ErrMsg(intSetSts)
        End If

        '販売価格マスタ取得不可の場合は、エラーメッセージのみ表示
        If intSetSts = EarthEnum.emSyouhin1Error.GetHanbai Then
            strErrMsg += cbLogic.GetSyouhin1ErrMsg(intSetSts)
        End If

        '原価・販売価格マスタ両にない場合は、両方のメッセージを表示
        If intSetSts = EarthEnum.emSyouhin1Error.GetGenkaHanbai Then
            strErrMsg += cbLogic.GetSyouhin1ErrMsg(EarthEnum.emSyouhin1Error.GetGenka)
            strErrMsg += cbLogic.GetSyouhin1ErrMsg(EarthEnum.emSyouhin1Error.GetHanbai)
        End If

        'メッセージがある場合のみ表示
        If strErrMsg <> "" Then
            ScriptManager.RegisterClientScriptBlock(sender, _
                                                    sender.GetType(), _
                                                    "alert", _
                                                    "alert('" & _
                                                    strErrMsg & _
                                                    "')", True)
        End If

        ' 親画面のメソッド実行
        RaiseEvent Syouhin1SetAction(Me, e, syouhin1Rec)

        If syouhin_rec.SyouhinCd IsNot Nothing Then

            '請求タイプの設定
            RaiseEvent SetSeikyuuTypeAction(Me _
                                            , e _
                                            , Me.ClientID _
                                            , syouhin1Rec.SeikyuuSakiType _
                                            , param_rec.KeiretuCd _
                                            , param_rec.KameitenCd)

        End If

        If blnTysGaiyou Then
            If syouhin_rec.SyouhinCd IsNot Nothing Then
                '調査概要の設定
                Me.SelectTyousaGaiyou.Value = cLogic.GetDispNum(syouhin_rec.TyousaGaiyou, "")
            Else
                '調査概要の設定
                Me.SelectTyousaGaiyou.Value = "9"
            End If
            Me.HiddenTyousaGaiyou.Value = Me.SelectTyousaGaiyou.Value
        End If

        HiddenTyousaHouhou.Value = TextTyousaHouhouCd.Value
        HiddenSyouhinKbn.Value = SyouhinKbn.ToString()
        Me.HiddenSyouhin1Pre.Value = Me.SelectSyouhin1.SelectedValue

        Me.UpdatePanelSyouhinKbn.Update()
        Me.UpdatePanelTyousaHouhou.Update()
        Me.UpdatePanelTyousaGaiyou.Update()
        Me.UpdatePanelSyouhin1.Update()

        '親画面の請求先・仕入先情報画面用情報のセットメソッドを実行
        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, Me.ClientID)

    End Sub

#Region "コントロールの活性制御"
    ''' <summary>
    ''' テキストボックス単体の活性制御
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableHtmlInput(ByRef ctrl As HtmlInputText, _
                               ByVal enabled As Boolean, _
                               Optional ByVal isHissu As Boolean = False)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnableHtmlInput", _
                                                    ctrl, enabled, isHissu)

        If enabled Then
            ctrl.Attributes("ReadOnly") = ""
            If isHissu Then
                ctrl.Attributes("class") = "codeNumber hissu"
            Else
                ctrl.Attributes("class") = "codeNumber"
            End If
        Else
            ctrl.Attributes("ReadOnly") = "readonly"
            ctrl.Attributes("class") = "readOnlyStyle"
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
        Else
            ctrl.BackColor = Drawing.Color.Transparent
            ctrl.BorderStyle = BorderStyle.None
        End If

    End Sub

    ''' <summary>
    ''' ドロップダウン単体の活性制御
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableHtmlSelect(ByRef ctrl As HtmlSelect, _
                                 ByVal enabled As Boolean)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnableHtmlSelect", _
                                                    ctrl, enabled)

        If enabled Then
            ctrl.Disabled = False
        Else
            ctrl.Disabled = True
        End If

    End Sub

    ''' <summary>
    ''' ラジオボタン単体の活性制御
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableRadio(ByRef ctrl As HtmlInputRadioButton, _
                            ByVal enabled As Boolean)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EnableHtmlSelect", _
                                                    ctrl, enabled)

        If enabled Then
            ctrl.Disabled = False
        Else
            ctrl.Disabled = True
        End If

    End Sub


#End Region

#Region "画面コントロールの変更箇所対応"

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する(全項目)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringAll() As String

        Dim sb As New StringBuilder

        '●表示項目
        '加盟店コード
        sb.Append(Me.TextKameitenCd.Value & EarthConst.SEP_STRING)

        '同時依頼棟数
        sb.Append(Me.TextDoujiIraiTousuu.Value & EarthConst.SEP_STRING)
        '建物用途TEXT
        sb.Append(Me.TextTatemonoYoutoCd.Value & EarthConst.SEP_STRING)
        '建物用途DDL
        sb.Append(Me.SelectTatemonoYouto.SelectedValue & EarthConst.SEP_STRING)

        '調査会社コード
        sb.Append(Me.TextTyousaKaishaCd.Value & EarthConst.SEP_STRING)

        '調査方法TEXT
        sb.Append(Me.TextTyousaHouhouCd.Value & EarthConst.SEP_STRING)
        '調査方法DDL
        sb.Append(Me.SelectTyousaHouhou.SelectedValue & EarthConst.SEP_STRING)

        '調査概要DDL
        sb.Append(Me.SelectTyousaGaiyou.Value & EarthConst.SEP_STRING)

        '商品1DDL
        sb.Append(Me.SelectSyouhin1.SelectedValue & EarthConst.SEP_STRING)

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
                .Add("0", "加盟店コード")
                .Add("1", "同時依頼棟数")
                .Add("2", "建物用途TEXT")
                .Add("3", "建物用途DDL")
                .Add("4", "調査会社コード")
                .Add("5", "調査方法TEXT")
                .Add("6", "調査方法DDL")
                .Add("7", "調査概要")
                .Add("8", "商品1")
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
            .Add("0", Me.TextKameitenCd)
            .Add("1", Me.TextDoujiIraiTousuu)
            .Add("2", Me.TextTatemonoYoutoCd)
            .Add("3", Me.SelectTatemonoYouto)
            .Add("4", Me.TextTyousaKaishaCd)
            .Add("5", Me.TextTyousaHouhouCd)
            .Add("6", Me.SelectTyousaHouhou)
            .Add("7", Me.SelectTyousaGaiyou)
            .Add("8", Me.SelectSyouhin1)
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

    ''' <summary>
    ''' 取消理由の設定
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <remarks></remarks>
    Private Sub setTorikesiRiyuu(ByVal strKbn As String, ByVal strKameitenCd As String)

        '色替え処理対象のコントロールを配列に格納(※取消理由テキストボックス以外)
        Dim objArray() As Object = New Object() {Me.TextKameitenCd, Me.TextKameitenMei}

        '取消理由と加盟店情報の文字色設定
        cLogic.GetKameitenTorikesiRiyuu(strKbn _
                                        , strKameitenCd _
                                        , Me.TextTorikesiRiyuu _
                                        , True _
                                        , False _
                                        , objArray)

    End Sub

#End Region

#Region "パブリックメソッド"
    ''' <summary>
    ''' エラーチェック
    ''' </summary>
    ''' <param name="errMess"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <param name="typeWord"></param>
    ''' <param name="strChgPartMess"></param>
    ''' <remarks></remarks>
    Public Sub CheckInput(ByRef errMess As String, _
                          ByRef arrFocusTargetCtrl As List(Of Control), _
                          ByVal typeWord As String, _
                          ByRef strChgPartMess As String, _
                          ByVal strLavelSyouhin1Cd As String)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckInput", _
                                                    errMess, _
                                                    arrFocusTargetCtrl, _
                                                    typeWord, _
                                                    strChgPartMess, _
                                                    strLavelSyouhin1Cd)

        '地盤画面共通クラス
        Dim jBn As New Jiban
        Dim strErrMsg As String = String.Empty '作業用
        Dim blnKamentenFlg As Boolean = False
        Dim kameitenSearchLogic As New KameitenSearchLogic

        Dim setuzoku As String = typeWord
        If typeWord <> "" Then
            setuzoku = typeWord & "："
        End If

        '比較実施(変更チェック)
        Dim strChgVal As String = Me.getCtrlValuesStringAll()
        If Me.HiddenOpenValue.Value <> strChgVal Then
            Dim strCtrlNm As String = String.Empty
            strCtrlNm = Me.ChkChgCtrlName(Me.HiddenOpenValue.Value, strChgVal, Me.HiddenKeyValue.Value) '変更箇所名の取得
            strChgPartMess += "[" & typeWord & "]\r\n" & strCtrlNm & "\r\n"
        End If

        '必須チェック
        If TextKameitenCd.Value = "" Or TextKameitenMei.Value = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", setuzoku & "加盟店")
            arrFocusTargetCtrl.Add(TextKameitenCd)
            blnKamentenFlg = True 'フラグを立てる
        End If
        If TextTatemonoYoutoCd.Value = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", setuzoku & "建物用途")
            arrFocusTargetCtrl.Add(TextTatemonoYoutoCd)
        End If
        If TextTyousaKaishaCd.Value = "" Or TextTyousaKaishaNm.Value = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", setuzoku & "調査会社")
            arrFocusTargetCtrl.Add(TextTyousaKaishaCd)
        End If
        If SelectSyouhin1.SelectedValue = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", setuzoku & "商品1")
            arrFocusTargetCtrl.Add(SelectSyouhin1)
        End If
        If TextTyousaHouhouCd.Value = "" Then
            errMess += Messages.MSG013E.Replace("@PARAM1", setuzoku & "調査方法")
            arrFocusTargetCtrl.Add(TextTyousaHouhouCd)
        End If

        'コード入力値変更チェック
        If TextKameitenCd.Value <> HiddenkameitenCdOld.Value Then
            errMess += Messages.MSG030E.Replace("@PARAM1", setuzoku & "加盟店コード")
            arrFocusTargetCtrl.Add(ButtonKameitenKensaku)
            blnKamentenFlg = True 'フラグを立てる
        End If
        If TextTyousaKaishaCd.Value <> HiddenTysKaisyaCdOld.Value Then
            errMess += Messages.MSG030E.Replace("@PARAM1", setuzoku & "調査会社コード")
            arrFocusTargetCtrl.Add(ButtonTyousaKaishaKensaku)
        End If

        '桁数チェック
        If jBn.SuutiStrCheck(TextDoujiIraiTousuu.Value, 4, 0) = False Then
            errMess += Messages.MSG027E.Replace("@PARAM1", setuzoku & _
                                                "同時依頼棟数").Replace("@PARAM2", "4").Replace("@PARAM3", "0")
            arrFocusTargetCtrl.Add(TextDoujiIraiTousuu)
        End If

        '商品1コード差異チェック
        If SelectSyouhin1.SelectedValue <> strLavelSyouhin1Cd Then
            'エラーメッセージ表示
            errMess += Messages.MSG041E.Replace("範囲指定", "商品1の選択内容と表示ラベル")
            arrFocusTargetCtrl.Add(SelectSyouhin1)
        End If

        'その他チェック
        '●調査概要/同時依頼棟数チェック
        strErrMsg = String.Empty
        If cbLogic.ChkErrTysGaiyou(Me.SelectTyousaGaiyou.Value, Me.TextDoujiIraiTousuu.Value, strErrMsg) = False Then
            errMess += strErrMsg
            arrFocusTargetCtrl.Add(Me.TextDoujiIraiTousuu)
        End If
        '●ビルダー注意事項チェック(加盟店関連のエラーがない場合チェックする)
        strErrMsg = String.Empty
        If blnKamentenFlg = False Then
            If kameitenSearchLogic.ChkBuilderData13(TextKameitenCd.Value) Then
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.TrueString
            Else
                Me.HiddenKameitenTyuuiJikou.Value = Boolean.FalseString
            End If
            If cbLogic.ChkErrBuilderData(Me.SelectTyousaGaiyou.Value, Me.TextKameitenCd.Value, Me.HiddenKameitenTyuuiJikou.Value, strErrMsg) = False Then
                errMess += strErrMsg
                arrFocusTargetCtrl.Add(Me.TextKameitenCd)
            End If
        End If

    End Sub

    ''' <summary>
    ''' 調査会社コードと名称を変更前の値に戻す
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReturnTyousakaisyaCdNm(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.TextTyousaKaishaCd.Value = Me.HiddenTysKaisyaCdOld.Value
        Me.TextTyousaKaishaNm.Value = Me.HiddenTysKaisyaNmOld.Value
    End Sub

#End Region

End Class
