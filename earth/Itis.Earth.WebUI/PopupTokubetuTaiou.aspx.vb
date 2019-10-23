Partial Public Class PopupTokubetuTaiou
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' 特別対応データ情報/行ユーザーコントロール格納リスト
    ''' </summary>
    ''' <remarks></remarks>
    Private pListCtrl As New List(Of TokubetuTaiouRecordCtrl)

    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    Dim cbLogic As New CommonBizLogic
    Dim MLogic As New MessageLogic
    Dim mttLogic As New TokubetuTaiouMstLogic   '特別対応マスタ
    Dim tttLogic As New TokubetuTaiouLogic      '特別対応トラン
    Dim jLogic As New JibanLogic                '地盤ロジック
    Dim kLogic As New KameitenSearchLogic       '加盟店検索ロジック

#Region "イベント"
    ''' <summary>
    ''' 子コントロールで起動したチェックボックスチェックイベントを親画面で反映する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">画面ID</param>
    ''' <remarks></remarks>
    Public Sub SetCheckTokubetuTaiouChangeAction(ByVal sender As System.Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal strId As String)

        Me.SetCheckTokubetuTaiouChangeOyaAction(strId)
    End Sub
#End Region

#Region "プロパティ"

#Region "パラメータ/各業務画面"
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

    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _kameitenCd As String
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrKameitenCd() As String
        Get
            Return _kameitenCd
        End Get
        Set(ByVal value As String)
            _kameitenCd = value
        End Set
    End Property

    ''' <summary>
    ''' 調査方法No
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _TysHouhouNo As String
    ''' <summary>
    ''' 調査方法No
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrTysHouhouNo() As String
        Get
            Return _TysHouhouNo
        End Get
        Set(ByVal value As String)
            _TysHouhouNo = value
        End Set
    End Property

    ''' <summary>
    ''' 商品コード(倉庫コード="100")
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _SyouhinCd As String
    ''' <summary>
    ''' 商品コード(倉庫コード="100")
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrSyouhinCd() As String
        Get
            Return _SyouhinCd
        End Get
        Set(ByVal value As String)
            _SyouhinCd = value
        End Set
    End Property

    ''' <summary>
    ''' 商品コード(商品1,2,3情報)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ArrSyouhinCd As String
    ''' <summary>
    ''' 商品コード(商品1,2,3情報)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrArrSyouhinCd() As String
        Get
            Return _ArrSyouhinCd
        End Get
        Set(ByVal value As String)
            _ArrSyouhinCd = value
        End Set
    End Property

    ''' <summary>
    ''' 計上FLG(商品1,2,3情報)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ArrKeijouFlg As String
    ''' <summary>
    ''' 計上FLG(商品1,2,3情報)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrArrKeijouFlg() As String
        Get
            Return _ArrKeijouFlg
        End Get
        Set(ByVal value As String)
            _ArrKeijouFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 発注書金額(商品1,2,3情報)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ArrHattyuuKingaku As String
    ''' <summary>
    ''' 発注書金額(商品1,2,3情報)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrArrHattyuuKingaku() As String
        Get
            Return _ArrHattyuuKingaku
        End Get
        Set(ByVal value As String)
            _ArrHattyuuKingaku = value
        End Set
    End Property

    ''' <summary>
    ''' 特別対応ツールチップDisplayコード(商品1,2,3情報)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ArrDisplayCd As String
    ''' <summary>
    ''' 特別対応ツールチップDisplayコード(商品1,2,3情報)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrArrDisplayCd() As String
        Get
            Return _ArrDisplayCd
        End Get
        Set(ByVal value As String)
            _ArrDisplayCd = value
        End Set
    End Property

    ''' <summary>
    ''' 特別対応更新対象コード(商品1,2,3情報)(不要)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChgTokuCd As String
    ''' <summary>
    ''' 特別対応更新対象コード(商品1,2,3情報)(不要)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrChgTokuCd() As String
        Get
            Return _ChgTokuCd
        End Get
        Set(ByVal value As String)
            _ChgTokuCd = value
        End Set
    End Property

    ''' <summary>
    ''' 画面モード
    ''' </summary>
    ''' <remarks></remarks>
    Private _GamenMode As EarthEnum.emTokubetuTaiouSearchType
    ''' <summary>
    ''' 画面モード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pEmGamenMode() As EarthEnum.emTokubetuTaiouSearchType
        Get
            Return _GamenMode
        End Get
        Set(ByVal value As EarthEnum.emTokubetuTaiouSearchType)
            _GamenMode = value
        End Set
    End Property

    ''' <summary>
    ''' 特別対応価格反映用フラグ
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _TokutaiKkkHaneiFlg As String
    ''' <summary>
    ''' 特別対応価格反映用フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrTokutaiKkkHaneiFlg() As String
        Get
            Return _TokutaiKkkHaneiFlg
        End Get
        Set(ByVal value As String)
            _TokutaiKkkHaneiFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 連棟物件数
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _RentouBukkenSuu As String
    ''' <summary>
    ''' 特別対応価格反映用フラグ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrRentouBukkenSuu() As String
        Get
            Return _RentouBukkenSuu
        End Get
        Set(ByVal value As String)
            _RentouBukkenSuu = value
        End Set
    End Property

#End Region

#Region "コントロールID接頭語"
    Private Const CTBL_NAME_TOKUBETU_TAIOU As String = "CtrlTokubetu_"
#End Region

#Region "パラメータ(配列)"
    Private pStrArrSyouhin1Cd() As String
    Private pStrArrSyouhin2Cd() As String
    Private pStrArrSyouhin3Cd() As String
    Private pStrArrUriKeijyouFlg1() As String
    Private pStrArrUriKeijyouFlg2() As String
    Private pStrArrUriKeijyouFlg3() As String
    Private pStrArrHattyuuKingaku1() As String
    Private pStrArrHattyuuKingaku2() As String
    Private pStrArrHattyuuKingaku3() As String
    Private pStrArrDisplayCd1() As String
    Private pStrArrDisplayCd2() As String
    Private pStrArrDisplayCd3() As String
    Private pStrArrUpdDatetime As String
    Private pStrArrUpdDatetime1() As String
    Private pStrArrUpdDatetime2() As String
    Private pStrArrUpdDatetime3() As String
#End Region

#Region "画面固有コントロール値"
    Private Const SETTEI_SAKI As String = "商品"
    Private Const KEIJYOU_ZUMI As String = "済"

    ''' <summary>
    ''' 配列タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum emArrType
        ''' <summary>
        ''' 商品コード
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN_CD = 1
        ''' <summary>
        ''' 売上計上FLG
        ''' </summary>
        ''' <remarks></remarks>
        KEIJYOU_FLG = 2
        ''' <summary>
        ''' 発注書金額
        ''' </summary>
        ''' <remarks></remarks>
        HATTYUU_KINGAKU = 3
        ''' <summary>
        ''' 更新日時
        ''' </summary>
        ''' <remarks></remarks>
        UPD_DATETIME = 4
        ''' <summary>
        ''' 特別対応ツールチップDipslayコード
        ''' </summary>
        ''' <remarks></remarks>
        DISPLAY_CD = 5
    End Enum
#End Region

#End Region

#Region "初期読込時処理系"
    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban    '地盤画面共通クラス
        Dim jSM As New JibanSessionManager

        'マスターページ情報を取得(ScriptManager用)
        masterAjaxSM = AjaxScriptManager

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合
            cl.CloseWindow(Me)
            Me.ButtonTouroku1.Visible = False
            Me.ButtonGetMaster.Visible = False
            Exit Sub
        End If

        If IsPostBack = False Then '初期起動時
            'パラメータ取得
            pStrKbn = Request("kbn")
            pStrBangou = Request("no")
            pStrKameitenCd = Request("kameitencd")
            pStrTysHouhouNo = Request("TysHouhouNo")
            pStrSyouhinCd = Request("SyouhinCd")
            pStrArrSyouhinCd = Request("ArrSyouhinCd")
            pStrArrKeijouFlg = Request("ArrKeijouFlg")
            pStrArrHattyuuKingaku = Request("ArrHattyuuKingaku")
            pStrArrDisplayCd = Request("ArrDisplayCd")
            pStrChgTokuCd = Request("ChgTokuCd")
            pEmGamenMode = Request("GamenMode")
            pStrTokutaiKkkHaneiFlg = Request("TokutaiKkkHaneiFlg")
            pStrRentouBukkenSuu = Request("RentouBukkenSuu")

            ' パラメータ不足時は画面を閉じる
            If pStrKbn Is Nothing Or pStrBangou Is Nothing Then
                cl.CloseWindow(Me)
                Me.ButtonTouroku1.Visible = False
                Me.ButtonGetMaster.Visible = False
                Exit Sub
            Else
                '共通情報の設定
                Me.HiddenKubun.Value = pStrKbn
                Me.HiddenNo.Value = pStrBangou
            End If

            '●権限のチェック
            '以下のいずれかの権限がない場合、当画面を閉じる

            '依頼業務権限
            '報告書業務権限
            '工事業務権限
            '保証業務権限
            '結果業務権限
            If userinfo.IraiGyoumuKengen = 0 _
                        And userinfo.HoukokusyoGyoumuKengen = 0 _
                        And userinfo.KoujiGyoumuKengen = 0 _
                        And userinfo.HosyouGyoumuKengen = 0 _
                        And userinfo.KekkaGyoumuKengen = 0 Then
                Me.ButtonTouroku1.Visible = False
                Me.ButtonGetMaster.Visible = False
            End If

            '****************************************************************************
            ' 特別対応データ取得
            '****************************************************************************
            Dim jr As New JibanRecordBase
            jr = jLogic.GetJibanData(pStrKbn, pStrBangou)

            '地盤データが存在する場合、画面に表示させる
            If jLogic.ExistsJibanData(pStrKbn, pStrBangou) AndAlso jr IsNot Nothing Then
                Me.SetCtrlFromJibanRec(sender, e, jr)
            Else
                cl.CloseWindow(Me)
                Me.ButtonTouroku1.Visible = False
                Me.ButtonGetMaster.Visible = False
                Exit Sub
            End If

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            'ボタン押下イベントの設定
            Me.setBtnEvent()

            '邸別画面からは参照のみ
            If pEmGamenMode = EarthEnum.emTokubetuTaiouSearchType.TeibetuSyuusei Then
                Me.ButtonTouroku1.Visible = False
            End If

            Me.ButtonClose.Focus()
        Else
            '画面項目設定処理(ポストバック用)
            Me.setDisplayPostBack()

        End If

    End Sub

    ''' <summary>
    ''' ページロードコンプリート
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupTokubetuTaiou_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        '明細行数取得
        Me.HiddenLineCnt.Value = pListCtrl.Count.ToString

        ' 一覧の背景色再設定
        Dim tmpScript As String
        tmpScript = "settingTable();"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "makeRowStripes", tmpScript, True)

    End Sub

    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行う（参照処理用）
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)
        Dim total_row As Integer = 0
        Dim dataArray As New List(Of KameitenSearchRecord)

        '****************************************************************************
        ' 地盤データ取得&セット&パラメータ取得
        '****************************************************************************
        ' コンボ設定ヘルパークラスを生成
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList
        Dim strTmpVal As String = String.Empty

        'ドロップダウンリストの設定（区分）
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Kubun, False, True)
        If jr.Kbn IsNot Nothing Then
            objDrpTmp.SelectedValue = jr.Kbn
        Else
            objDrpTmp.SelectedItem.Text = String.Empty
        End If

        '区分
        Me.TextKbn.Text = objDrpTmp.SelectedItem.Text
        '区分（隠し項目）
        Me.HiddenKbn.Value = jr.Kbn
        '番号
        Me.TextBangou.Text = cl.GetDispStr(jr.HosyousyoNo)
        '施主名
        Me.TextSesyuMei.Text = cl.GetDisplayString(jr.SesyuMei)

        '更新日時 なければ 登録日時
        Me.HiddenRegUpdDate.Value = IIf(jr.UpdDatetime <> Date.MinValue, _
                                        jr.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2), _
                                        jr.AddDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2))


        '加盟店コード・商品コード・調査方法NOがいずれか不足の場合はDBより取得する
        If pStrKameitenCd Is Nothing OrElse pStrKameitenCd = String.Empty _
            Or pStrTysHouhouNo Is Nothing OrElse pStrTysHouhouNo = String.Empty _
                Or pStrSyouhinCd Is Nothing OrElse pStrSyouhinCd = String.Empty Then

            Me.HiddenKameitenCd.Value = cl.GetDisplayString(jr.KameitenCd)
            Me.HiddenTysHouhouNo.Value = cl.GetDisplayString(jr.TysHouhou)
            If Not jr.Syouhin1Record Is Nothing Then
                Me.HiddenSyouhinCd.Value = jr.Syouhin1Record.SyouhinCd
            End If
        Else
            Me.HiddenKameitenCd.Value = pStrKameitenCd
            Me.HiddenTysHouhouNo.Value = pStrTysHouhouNo
            Me.HiddenSyouhinCd.Value = pStrSyouhinCd
        End If

        'KEY情報が取得できない場合、画面を閉じる
        If Me.HiddenKameitenCd.Value = String.Empty _
            OrElse Me.HiddenTysHouhouNo.Value = String.Empty _
                OrElse Me.HiddenSyouhinCd.Value = String.Empty Then
            cl.CloseWindow(Me)
            Exit Sub
        End If

        '加盟店コード
        Me.TextKameitenCd.Text = Me.HiddenKameitenCd.Value

        '加盟店名
        dataArray = kLogic.GetKameitenSearchResult(jr.Kbn _
                                         , Me.HiddenKameitenCd.Value _
                                         , False _
                                         , total_row)
        If total_row = 1 Then
            Dim recData As KameitenSearchRecord = dataArray(0)
            Me.TextKameitenMei.Text = cl.GetDisplayString(recData.KameitenMei1)
        Else
            Me.TextKameitenMei.Text = String.Empty
        End If

        'ドロップダウンリストの設定（商品1）
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Syouhin1)
        strTmpVal = cl.GetDispNum(Me.HiddenSyouhinCd.Value, "")
        If cl.ChkDropDownList(objDrpTmp, strTmpVal) Then
            'DDLにあれば、選択する
            objDrpTmp.SelectedValue = strTmpVal
        Else
            Dim recSyouhin As New SyouhinMeisaiRecord
            recSyouhin = jLogic.GetSyouhinRecord(strTmpVal)
            If Not recSyouhin Is Nothing Then
                objDrpTmp.Items.Add(New ListItem(recSyouhin.SyouhinCd & ":" & recSyouhin.SyouhinMei, recSyouhin.SyouhinCd))
                objDrpTmp.SelectedValue = recSyouhin.SyouhinCd  '選択状態
            End If
        End If
        '商品1
        Me.TextSyouhin1.Text = objDrpTmp.SelectedItem.Text

        'ドロップダウンリストの設定（調査方法）
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.TyousaHouhou)
        '調査方法のDDL表示処理
        cl.ps_SetSelectTextBoxTysHouhou(CInt(Me.HiddenTysHouhouNo.Value), objDrpTmp, True, Me.TextTysHouhou)


        '****************************************************************************
        ' パラメータをHiddenに設定
        '****************************************************************************
        Me.SetPrmJibanRec(sender, e, jr)

        '***************************************
        ' 邸別請求データ
        '***************************************
        '●商品情報(画面値より生成)
        Me.ps_SetSyouhin123ToTeibetuRec(jr)

        '****************************************************************************
        ' 特別対応マスタ取得/特別対応データ設定
        '****************************************************************************
        Me.SetCtrlFromDataRec(sender, e, jr)

    End Sub

    ''' <summary>
    ''' 特別対応マスタ取得/特別対応データ設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)
        Dim ctrlTokubetuInfoRec As New TokubetuTaiouRecordCtrl
        Dim listRes As List(Of TokubetuTaiouRecordBase)
        Dim dtRec As TokubetuTaiouRecordBase
        Dim intTotalCnt As Integer = 0

        Dim strSetteiSaki As String      '設定先
        Dim search_shouhin() As String   '商品判定用
        Dim intSearchSyouhin As Integer  '商品判定用
        Dim strSyouhinBunrui As String
        Dim strSyouhinGamenHyoujiNo As String
        Dim intCnt As Integer            'カウンタ
        Dim intGamenHyoujiNo As Integer

        Dim htTtSetteiSaki As New Dictionary(Of Integer, String)
        Dim SyouhinRec As SyouhinMeisaiRecord = jLogic.GetSyouhinRecord(Me.HiddenSyouhinCd.Value) '商品名
        Dim strSyouhin1Mei As String = String.Empty '商品名
        If Not SyouhinRec Is Nothing Then
            strSyouhin1Mei = SyouhinRec.SyouhinMei
        End If

        '特別対応マスタ
        listRes = mttLogic.GetTokubetuTaiouInfo(sender, Me.HiddenKbn.Value, Me.TextBangou.Text, Me.HiddenKameitenCd.Value, Me.HiddenSyouhinCd.Value, Me.HiddenTysHouhouNo.Value, intTotalCnt)
        '検索結果件数が-1の場合、エラーなので、処理終了
        If intTotalCnt = -1 Then
            Exit Sub
        End If

        '計上FLG群
        SplitPrm(Me.HiddenArrKeijyouFlg.Value, pStrArrUriKeijyouFlg1, pStrArrUriKeijyouFlg2, pStrArrUriKeijyouFlg3)
        '発注書金額
        SplitPrm(Me.HiddenArrHattyuuKingaku.Value, pStrArrHattyuuKingaku1, pStrArrHattyuuKingaku2, pStrArrHattyuuKingaku3)

        '特別対応ツールチップ情報
        If Me.HiddenArrDisplayCd.Value <> String.Empty Then
            SplitPrm(Me.HiddenArrDisplayCd.Value, pStrArrDisplayCd1, pStrArrDisplayCd2, pStrArrDisplayCd3)

            cbLogic.SetTokubetuTaiouCdTeibetuKey(htTtSetteiSaki, EarthEnum.emTeibetuBunrui.SYOUHIN1, pStrArrDisplayCd1)
            cbLogic.SetTokubetuTaiouCdTeibetuKey(htTtSetteiSaki, EarthEnum.emTeibetuBunrui.SYOUHIN2, pStrArrDisplayCd2)
            cbLogic.SetTokubetuTaiouCdTeibetuKey(htTtSetteiSaki, EarthEnum.emTeibetuBunrui.SYOUHIN3, pStrArrDisplayCd3)
        End If

        '特別対応レコードリストをベースにループ
        For intCnt = 0 To listRes.Count - 1
            'レコードが存在する場合のみ画面表示
            dtRec = listRes(intCnt)
            If Not dtRec Is Nothing AndAlso dtRec.mTokubetuTaiouCd <> Integer.MinValue Then
                'ユーザコントロールの読込とIDの付与
                ctrlTokubetuInfoRec = Me.LoadControl("control/TokubetuTaiouRecordCtrl.ascx")
                ctrlTokubetuInfoRec.ID = CTBL_NAME_TOKUBETU_TAIOU & (intCnt + 1).ToString

                'チェックボックスイベントの実装
                AddHandler ctrlTokubetuInfoRec.SetCheckTokubetuTaiouChangeAction, AddressOf Me.SetCheckTokubetuTaiouChangeAction

                'テーブルに明細行を一行追加
                Me.tblMeisai.Controls.Add(ctrlTokubetuInfoRec)

                'データをコントロールにセット
                ctrlTokubetuInfoRec.SetCtrlFromDataRec(sender, e, dtRec)

                '設定先が赤字でも受注画面情報より価格反映処理済の場合、青字として設定先を上書きする
                If Me.HiddenArrDisplayCd.Value <> String.Empty Then
                    Dim intTmpTtCd As Integer = cl.GetDisplayString(ctrlTokubetuInfoRec.AccHdnTokubetuTaiouCd.Value)
                    '設定先が既にセットされている場合、DB値になくても設定先をセットする
                    If htTtSetteiSaki.ContainsKey(intTmpTtCd) Then
                        strSetteiSaki = htTtSetteiSaki(intTmpTtCd)
                        If strSetteiSaki <> String.Empty Then
                            search_shouhin = strSetteiSaki.Split(EarthConst.UNDER_SCORE)
                            strSyouhinBunrui = search_shouhin(0) '商品1,2,3
                            strSyouhinGamenHyoujiNo = search_shouhin(1) '画面表示NO

                            Select Case strSyouhinBunrui
                                Case "1" '商品1
                                    ctrlTokubetuInfoRec.AccHdnBunruiCd.Value = EarthConst.SOUKO_CD_SYOUHIN_1
                                    ctrlTokubetuInfoRec.AccHdnGamenHyoujiNo.Value = strSyouhinGamenHyoujiNo

                                Case "2" '商品2
                                    ctrlTokubetuInfoRec.AccHdnBunruiCd.Value = cbLogic.pf_getBunruiCd(ctrlTokubetuInfoRec.AccTextKasanSyouhinCd.Value)
                                    ctrlTokubetuInfoRec.AccHdnGamenHyoujiNo.Value = strSyouhinGamenHyoujiNo

                                Case "3" '商品3
                                    ctrlTokubetuInfoRec.AccHdnBunruiCd.Value = EarthConst.SOUKO_CD_SYOUHIN_3
                                    ctrlTokubetuInfoRec.AccHdnGamenHyoujiNo.Value = strSyouhinGamenHyoujiNo

                            End Select
                            '赤字→青字に設定
                            ctrlTokubetuInfoRec.AccHdnHiddenSetteiSakiStyle.Value = EarthConst.STYLE_COLOR_BLUE

                        End If
                    End If
                End If

                '商品コード1が異なる場合、金額加算商品コード・金額加算商品名を上書きする
                If ctrlTokubetuInfoRec.AccTextKasanSyouhinCd.Value <> String.Empty Then
                    If ctrlTokubetuInfoRec.AccTextKasanSyouhinCd.Value <> Me.HiddenSyouhinCd.Value Then
                        If ctrlTokubetuInfoRec.AccHdnBunruiCd.Value = EarthConst.SOUKO_CD_SYOUHIN_1 Then
                            ctrlTokubetuInfoRec.AccTextKasanSyouhinCd.Value = Me.HiddenSyouhinCd.Value
                            ctrlTokubetuInfoRec.AccTextKasanSyouhinMei.Value = strSyouhin1Mei
                        End If
                    End If
                End If

                '青字項目を対象に、商品情報から売上計上FLG、発注書金額を設定
                If ctrlTokubetuInfoRec.AccHdnHiddenSetteiSakiStyle.Value = EarthConst.STYLE_COLOR_BLUE Then
                    '設定先を取得
                    cl.SetDisplayString(ctrlTokubetuInfoRec.AccHdnGamenHyoujiNo.Value, intGamenHyoujiNo)
                    strSetteiSaki = cbLogic.DevideTokubetuCd(sender, ctrlTokubetuInfoRec.AccHdnBunruiCd.Value, intGamenHyoujiNo)
                    If strSetteiSaki <> String.Empty Then
                        '売上計上、発注書金額
                        search_shouhin = strSetteiSaki.Split(EarthConst.UNDER_SCORE)
                        strSyouhinBunrui = search_shouhin(0) '商品1,2,3
                        strSyouhinGamenHyoujiNo = search_shouhin(1) '画面表示NO

                        intSearchSyouhin = CInt(strSyouhinGamenHyoujiNo)
                        intSearchSyouhin -= 1

                        Select Case strSyouhinBunrui
                            Case "1" '商品1
                                Me.SetUriKeijyouHattyuuKingaku(ctrlTokubetuInfoRec, pStrArrUriKeijyouFlg1(intSearchSyouhin), pStrArrHattyuuKingaku1(intSearchSyouhin))
                            Case "2" '商品2
                                Me.SetUriKeijyouHattyuuKingaku(ctrlTokubetuInfoRec, pStrArrUriKeijyouFlg2(intSearchSyouhin), pStrArrHattyuuKingaku2(intSearchSyouhin))
                            Case "3" '商品3
                                Me.SetUriKeijyouHattyuuKingaku(ctrlTokubetuInfoRec, pStrArrUriKeijyouFlg3(intSearchSyouhin), pStrArrHattyuuKingaku3(intSearchSyouhin))
                        End Select
                    End If
                End If

                '画面表示した項目をリストに追加
                pListCtrl.Add(ctrlTokubetuInfoRec)
            End If
        Next

        '画面表示時点の値を、Hiddenに保持(変更チェック用)
        If Me.HiddenOpenValues.Value = String.Empty Then
            Me.HiddenOpenValues.Value = Me.getCtrlValuesString()
        End If

        '設定先再設定処理
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", "objEBI('" & Me.ButtonSetSetteiSaki.ClientID & "').click();", True)

    End Sub

    ''' <summary>
    ''' 登録/修正ボタンの設定
    ''' </summary>
    ''' <remarks>
    ''' DB更新中、画面のグレイアウトを行なう。<br/>
    ''' </remarks>
    Private Sub setBtnEvent()

        Dim tmpCheckTouroku As String = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this,null,1);}else{return false;}"
        Dim tmpScript As String = "if(CheckTouroku()){" & tmpCheckTouroku & "}else{return false;}"
        Dim tmpScript2 As String = "if(confirm('" & Messages.MSG181C & "')){}else{return false;}"

        '登録許可MSG確認後、OKの場合更新処理を行う
        Me.ButtonTouroku1.Attributes("onclick") = "setAction('" & EarthEnum.emTokubetuTaiouActBtn.BtnOther & "');" & tmpScript

        'マスタ再取得ボタン
        Dim tmpGetMaster As String = "setAction('" & EarthEnum.emTokubetuTaiouActBtn.BtnMaster & "');" & tmpScript2
        Me.ButtonGetMaster.Attributes("onclick") = "if(CheckGetMaster()){" & tmpGetMaster & "}else{return false;}"

    End Sub

    ''' <summary>
    ''' 特別対応マスタ取得/マスタ値を設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRecMst(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ctrlTokubetuInfoRec As New TokubetuTaiouRecordCtrl
        Dim dtRec As New TokubetuTaiouRecordBase
        Dim intTotalCnt As Integer = 0
        Dim intCnt As Integer = 0 'カウンタ

        With Me.tblMeisai.Controls
            For intCnt = 1 To .Count - 1
                ctrlTokubetuInfoRec = Me.LoadControl("control/TokubetuTaiouRecordCtrl.ascx")
                ctrlTokubetuInfoRec.ID = CTBL_NAME_TOKUBETU_TAIOU & intCnt.ToString

                ctrlTokubetuInfoRec = pListCtrl(intCnt - 1)

                dtRec = New TokubetuTaiouRecordBase

                '画面情報をレコードにセット
                ctrlTokubetuInfoRec.GetRowCtrlToDataRec(dtRec)

                'データをコントロールにセット
                ctrlTokubetuInfoRec.SetCtrlFromHiddenMst(sender, e, dtRec)

                .Add(ctrlTokubetuInfoRec)

            Next

        End With

        '設定先をセット
        Me.SetCheckTokubetuTaiouChangeOyaAction(Me.ButtonGetMaster.ClientID)

        'マスタ再取得ボタン押下時はメッセージを表示
        '完了メッセージの表示とテーブルレイアウトを設定
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Info", "alert('" & Messages.MSG184E & "');", True)

    End Sub

    ''' <summary>
    ''' 画面項目設定処理(ポストバック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDisplayPostBack()
        Dim intMakeCnt As Integer = 0

        '行数の取得
        intMakeCnt = Integer.Parse(Me.HiddenLineCnt.Value)

        '行作成
        For intCnt As Integer = 0 To intMakeCnt - 1
            Me.createRow(intCnt)
        Next

    End Sub

    ''' <summary>
    ''' 行を作成します
    ''' </summary>
    ''' <param name="intRowCnt">作成する行数</param>
    ''' <remarks></remarks>
    Private Sub createRow(ByVal intRowCnt As Integer)
        Dim ctrlTokubetuInfoRec As New TokubetuTaiouRecordCtrl

        With Me.tblMeisai.Controls
            ctrlTokubetuInfoRec = Me.LoadControl("control/TokubetuTaiouRecordCtrl.ascx")
            ctrlTokubetuInfoRec.ID = CTBL_NAME_TOKUBETU_TAIOU & (intRowCnt + 1).ToString

            'チェックボックスチェックイベントの実装
            AddHandler ctrlTokubetuInfoRec.SetCheckTokubetuTaiouChangeAction, AddressOf Me.SetCheckTokubetuTaiouChangeAction

            .Add(ctrlTokubetuInfoRec)
        End With

        pListCtrl.Add(ctrlTokubetuInfoRec)

    End Sub
#End Region

#Region "DB更新処理系"
    ''' <summary>
    ''' 入力項目のチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function checkInput() As Boolean
        'チェックなし
        Return True
    End Function

    ''' <summary>
    ''' 画面の内容をDBに反映する
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function SaveData() As Boolean
        Dim listRec As List(Of TokubetuTaiouRecordBase)
        Dim jr As New JibanRecordBase
        Dim intTmp As Integer
        Dim blnAuto As Boolean

        '地盤データの取得
        jr = Me.GetCtrlFromJibanRec()

        '各行ごとに画面からレコードクラスに入れ込み
        listRec = Me.GetRowCtrlToList()

        '特別対応処理成否
        cl.SetDisplayString(Me.HiddenGamenMode.Value, intTmp)
        If intTmp = EarthEnum.emTokubetuTaiouSearchType.IraiKakunin Then
            blnAuto = False
        Else
            blnAuto = True
        End If

        'データの更新を行う
        If tttLogic.saveData(Me, jr, listRec, blnAuto) = False Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' 画面表示項目から地盤レコードへの値セットを行う（排他チェック用）
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCtrlFromJibanRec() As JibanRecordBase
        Dim jr As New JibanRecordBase

        ' 現在の地盤データをDBから取得する
        jr = jLogic.GetJibanData(Me.HiddenKbn.Value, Me.TextBangou.Text)

        '特別対応処理成否
        Dim intTmp As Integer = 0
        cl.SetDisplayString(Me.HiddenGamenMode.Value, intTmp)

        '***************************************
        ' 地盤データ
        '***************************************
        With jr
            '●KEY情報→画面情報より上書き
            '加盟店コード
            .KameitenCd = Me.HiddenKameitenCd.Value
            ''商品コード※後続でセット
            '.Syouhin1Record.SyouhinCd = Me.HiddenSyouhinCd.Value
            '調査方法NO
            .TysHouhou = Me.HiddenTysHouhouNo.Value

            '***************************************
            ' 邸別請求データ
            '***************************************
            If intTmp = EarthEnum.emTokubetuTaiouSearchType.IraiKakunin Then
                '●商品情報(画面値より生成)
                Me.ps_SetSyouhin123ToTeibetuRec(jr)
            End If

            '更新者ユーザID
            .UpdLoginUserId = userinfo.LoginUserId
            '更新日時
            .UpdDatetime = Date.Parse(Me.HiddenRegUpdDate.Value)

            '連棟物件数
            .RentouBukkenSuu = cl.GetDispNum(Me.HiddenRentouBukkenSuu.Value, "1")

        End With

        Return jr
    End Function

    ''' <summary>
    ''' 引き渡されたHiddenをもとに、商品123(邸別請求)情報を生成し、取得する
    ''' </summary>
    ''' <param name="jibanRec">地盤レコードクラス</param>
    ''' <remarks></remarks>
    Private Sub ps_SetSyouhin123ToTeibetuRec(ByRef jibanRec As JibanRecordBase)

        With jibanRec
            '商品情報をクリア
            .Syouhin1Record = Nothing
            .Syouhin2Records = Nothing
            .Syouhin3Records = Nothing
        End With

        '商品1レコードオブジェクトの生成
        jLogic.CreateSyouhin1Rec(Me, jibanRec)
        '商品2レコードオブジェクトの生成
        jLogic.CreateSyouhin23Rec(Me, jibanRec)
        '商品3レコードオブジェクトの生成
        jLogic.CreateSyouhin23Rec(Me, jibanRec, True)

        '●商品情報→画面情報より上書き
        '商品コード群
        SplitPrm(Me.HiddenArrSyouhinCd.Value, pStrArrSyouhin1Cd, pStrArrSyouhin2Cd, pStrArrSyouhin3Cd)
        '計上FLG群
        SplitPrm(Me.HiddenArrKeijyouFlg.Value, pStrArrUriKeijyouFlg1, pStrArrUriKeijyouFlg2, pStrArrUriKeijyouFlg3)
        '発注書金額
        SplitPrm(Me.HiddenArrHattyuuKingaku.Value, pStrArrHattyuuKingaku1, pStrArrHattyuuKingaku2, pStrArrHattyuuKingaku3)
        '更新日時
        SplitPrm(Me.HiddenArrUpdDatetime.Value, pStrArrUpdDatetime1, pStrArrUpdDatetime2, pStrArrUpdDatetime3)

        '商品1
        If Not jibanRec.Syouhin1Record Is Nothing Then

            With jibanRec.Syouhin1Record
                '商品コード
                cl.SetDisplayString(pStrArrSyouhin1Cd(0), .SyouhinCd)
                '分類コード
                .BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_1
                '画面表示No
                .GamenHyoujiNo = 1
                '売上計上FLG
                cl.SetDisplayString(pStrArrUriKeijyouFlg1(0), .UriKeijyouFlg)
                '発注書金額
                cl.SetDisplayString(pStrArrHattyuuKingaku1(0), .HattyuusyoGaku)
                '更新日時
                If Not pStrArrUpdDatetime1 Is Nothing AndAlso pStrArrUpdDatetime1(0) <> EarthConst.BRANK_STRING Then
                    .UpdDatetime = DateTime.ParseExact(pStrArrUpdDatetime1(0), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                End If
            End With

        End If

        '商品2
        If Not jibanRec.Syouhin2Records Is Nothing Then
            For intCnt As Integer = 1 To EarthConst.SYOUHIN2_COUNT
                If jibanRec.Syouhin2Records.ContainsKey(intCnt) Then

                    With jibanRec.Syouhin2Records(intCnt)
                        '商品コード
                        cl.SetDisplayString(pStrArrSyouhin2Cd(intCnt - 1), .SyouhinCd)
                        '分類コード
                        .BunruiCd = cbLogic.pf_getBunruiCd(.SyouhinCd)
                        '画面表示No
                        .GamenHyoujiNo = intCnt
                        '売上計上FLG
                        cl.SetDisplayString(pStrArrUriKeijyouFlg2(intCnt - 1), .UriKeijyouFlg)
                        '発注書金額
                        cl.SetDisplayString(pStrArrHattyuuKingaku2(intCnt - 1), .HattyuusyoGaku)
                        '更新日時
                        If Not pStrArrUpdDatetime2 Is Nothing AndAlso pStrArrUpdDatetime2(intCnt - 1) <> EarthConst.BRANK_STRING Then
                            .UpdDatetime = DateTime.ParseExact(pStrArrUpdDatetime2(intCnt - 1), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                        End If
                    End With

                End If
            Next
        End If
        '商品コード未設定は削除
        jLogic.DeleteSyouhin23Rec(Me, jibanRec)

        '商品3
        If Not jibanRec.Syouhin3Records Is Nothing Then
            For intCnt As Integer = 1 To EarthConst.SYOUHIN3_COUNT

                If jibanRec.Syouhin3Records.ContainsKey(intCnt) Then

                    With jibanRec.Syouhin3Records(intCnt)
                        '商品コード
                        cl.SetDisplayString(pStrArrSyouhin3Cd(intCnt - 1), .SyouhinCd)
                        '分類コード
                        .BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_3
                        '画面表示No
                        .GamenHyoujiNo = intCnt
                        '売上計上FLG
                        cl.SetDisplayString(pStrArrUriKeijyouFlg3(intCnt - 1), .UriKeijyouFlg)
                        '発注書金額
                        cl.SetDisplayString(pStrArrHattyuuKingaku3(intCnt - 1), .HattyuusyoGaku)
                        '更新日時
                        If Not pStrArrUpdDatetime3 Is Nothing AndAlso pStrArrUpdDatetime3(intCnt - 1) <> EarthConst.BRANK_STRING Then
                            .UpdDatetime = DateTime.ParseExact(pStrArrUpdDatetime3(intCnt - 1), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                        End If
                    End With

                End If
            Next
        End If
        '商品コード未設定は削除
        jLogic.DeleteSyouhin23Rec(Me, jibanRec, True)


    End Sub

    ''' <summary>
    ''' 画面の各明細行情報をレコードクラスに取得し、地盤レコードクラスのリストを返却する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetRowCtrlToList() As List(Of TokubetuTaiouRecordBase)
        Dim listRec As New List(Of TokubetuTaiouRecordBase)
        Dim intCntCtrl As Integer = 0
        Dim dtRec As New TokubetuTaiouRecordBase
        Dim ctrlTokubetu As New TokubetuTaiouRecordCtrl
        Dim intMaxCnt As Integer = 0

        intMaxCnt = pListCtrl.Count

        '***************************************
        ' 特別対応データ
        '***************************************
        For intCntCtrl = 1 To intMaxCnt
            ctrlTokubetu = pListCtrl(intCntCtrl - 1)

            dtRec = New TokubetuTaiouRecordBase

            '区分
            dtRec.Kbn = Me.HiddenKbn.Value
            '番号（保証書NO）
            dtRec.HosyousyoNo = Me.TextBangou.Text
            '更新者ユーザID
            dtRec.UpdLoginUserId = userinfo.LoginUserId

            '画面情報をレコードにセット
            ctrlTokubetu.GetRowCtrlToDataRec(dtRec)

            listRec.Add(dtRec)
        Next

        Return listRec
    End Function

#End Region

#Region "特別対応価格対応"

#Region "画面表示関連"
    ''' <summary>
    ''' パラメータをHiddenに格納
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Private Sub SetPrmJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByRef jr As JibanRecordBase)
        '画面モード
        Me.HiddenGamenMode.Value = pEmGamenMode

        '特別対応価格反映用フラグ(商品1変更フラグ)
        Me.HiddenTokutaiKkkHaneiFlg.Value = IIf(pStrTokutaiKkkHaneiFlg = "1", pStrTokutaiKkkHaneiFlg, "0")

        '連棟物件数
        Me.HiddenRentouBukkenSuu.Value = IIf(pStrRentouBukkenSuu = Nothing OrElse pStrRentouBukkenSuu = String.Empty, "1", pStrRentouBukkenSuu)

        '商品コード群
        If pStrArrSyouhinCd Is Nothing OrElse pStrArrSyouhinCd = String.Empty Then
            pStrArrSyouhinCd = SetArrFromTeibetuSeikyuu(jr, emArrType.SYOUHIN_CD)           '不足の場合はDBより取得する
        End If
        Me.HiddenArrSyouhinCd.Value = pStrArrSyouhinCd                                      'Hiddenに退避

        '計上FLG群
        If pStrArrKeijouFlg Is Nothing OrElse pStrArrKeijouFlg = String.Empty Then
            pStrArrKeijouFlg = SetArrFromTeibetuSeikyuu(jr, emArrType.KEIJYOU_FLG)          '不足の場合はDBより取得する
        End If
        Me.HiddenArrKeijyouFlg.Value = pStrArrKeijouFlg                                     'Hiddenに退避

        '発注書金額群
        If pStrArrHattyuuKingaku Is Nothing OrElse pStrArrHattyuuKingaku = String.Empty Then
            pStrArrHattyuuKingaku = SetArrFromTeibetuSeikyuu(jr, emArrType.HATTYUU_KINGAKU) '不足の場合はDBより取得する
        End If
        Me.HiddenArrHattyuuKingaku.Value = pStrArrHattyuuKingaku                            'Hiddenに退避

        '更新日時
        If pEmGamenMode <> EarthEnum.emTokubetuTaiouSearchType.IraiKakunin Then             '受注画面以外から呼ばれた場合、DBより取得する
            pStrArrUpdDatetime = SetArrFromTeibetuSeikyuu(jr, emArrType.UPD_DATETIME)
            Me.HiddenArrUpdDatetime.Value = pStrArrUpdDatetime                              'Hiddenに退避
        End If

        '特別対応ツールチップDisplayコード
        Me.HiddenArrDisplayCd.Value = pStrArrDisplayCd                                      'Hiddenに退避
        '特別対応更新対象コード(不要)
        Me.HiddenChgTokuCd.Value = pStrChgTokuCd                                            'Hiddenに退避

    End Sub

    ''' <summary>
    ''' 区切り文字($$$)を使い、DB値を文字列にする
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <param name="emType">配列タイプ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetArrFromTeibetuSeikyuu(ByVal jr As JibanRecordBase, ByVal emType As emArrType)

        Dim intCnt As Integer
        Dim strArr As String
        Dim strSepString2 As String = EarthConst.SEP_STRING & EarthConst.SEP_STRING                         '商品各行区切り文字

        '商品1
        strArr = GetArrFromTeibetuSeikyuu(jr.Syouhin1Record, emType)
        strArr &= EarthConst.SEP_STRING
        '商品2
        For intCnt = 1 To EarthConst.SYOUHIN2_COUNT
            If Not jr.Syouhin2Records Is Nothing AndAlso jr.Syouhin2Records.ContainsKey(intCnt) Then

                strArr &= strSepString2 & GetArrFromTeibetuSeikyuu(jr.Syouhin2Records(intCnt), emType)
            Else
                If emType = emArrType.HATTYUU_KINGAKU Then
                    strArr &= strSepString2 & "0"
                Else
                    strArr &= strSepString2 & EarthConst.BRANK_STRING
                End If
            End If
        Next
        strArr &= EarthConst.SEP_STRING
        '商品3
        For intCnt = 1 To EarthConst.SYOUHIN3_COUNT
            If Not jr.Syouhin3Records Is Nothing AndAlso jr.Syouhin3Records.ContainsKey(intCnt) Then

                strArr &= strSepString2 & GetArrFromTeibetuSeikyuu(jr.Syouhin3Records(intCnt), emType)
            Else
                If emType = emArrType.HATTYUU_KINGAKU Then
                    strArr &= strSepString2 & "0"
                Else
                    strArr &= strSepString2 & EarthConst.BRANK_STRING
                End If
            End If
        Next

        Return strArr
    End Function

    ''' <summary>
    ''' 文字列または半角スペースを返す
    ''' </summary>
    ''' <param name="dtRec">邸別請求レコード</param>
    ''' <param name="emType">配列タイプ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetArrFromTeibetuSeikyuu(ByVal dtRec As TeibetuSeikyuuRecord, ByVal emType As emArrType) As String
        Dim strTmp As String = String.Empty

        If emType = emArrType.SYOUHIN_CD Then                                       '商品コード
            strTmp = dtRec.SyouhinCd

        ElseIf emType = emArrType.KEIJYOU_FLG Then                                  '売上計上FLG
            strTmp = cl.GetDisplayString(dtRec.UriKeijyouFlg, String.Empty)

        ElseIf emType = emArrType.HATTYUU_KINGAKU Then                              '発注書金額
            strTmp = cl.GetDisplayString(dtRec.HattyuusyoGaku, "0")

        ElseIf emType = emArrType.UPD_DATETIME Then                                 '更新日時
            '受注画面から呼ばれた場合は設定しない
            If pEmGamenMode = EarthEnum.emTokubetuTaiouSearchType.IraiKakunin Then
                strTmp = String.Empty
            Else
                strTmp = IIf(dtRec.UpdDatetime = DateTime.MinValue, Format(dtRec.AddDatetime, EarthConst.FORMAT_DATE_TIME_1), Format(dtRec.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
            End If

        End If

        '値が入っていない場合半角スペースを返す
        If strTmp Is Nothing OrElse strTmp Is String.Empty Then
            Return EarthConst.BRANK_STRING
        Else
            Return strTmp
        End If

    End Function

    ''' <summary>
    ''' パラメータを分割して配列に格納する
    ''' </summary>
    ''' <param name="strPrm">パラメータ</param>
    ''' <param name="strArr1">商品1配列</param>
    ''' <param name="strArr2">商品2配列</param>
    ''' <param name="strArr3">商品3配列</param>
    ''' <remarks></remarks>
    Private Sub SplitPrm(ByVal strPrm As String, ByRef strArr1() As String, ByRef strArr2() As String, ByRef strArr3() As String)

        Dim strSepString3 As String = EarthConst.SEP_STRING & EarthConst.SEP_STRING & EarthConst.SEP_STRING '商品1～3区切り文字
        Dim strSepString2 As String = EarthConst.SEP_STRING & EarthConst.SEP_STRING                         '商品各行区切り文字

        Dim arrTmp() As String
        '配列の初期化
        arrTmp = Nothing
        strArr1 = Nothing
        strArr2 = Nothing
        strArr3 = Nothing

        If strPrm <> String.Empty Then
            '区切り文字で分割
            arrTmp = Split(strPrm, strSepString3)

            '区切り文字で分割し、各商品配列へ
            strArr1 = Split(arrTmp(0), strSepString2)
            strArr2 = Split(arrTmp(1), strSepString2)
            strArr3 = Split(arrTmp(2), strSepString2)
        End If

    End Sub

    ''' <summary>
    ''' 画面コントロールの値を結合し、文字列化する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesString() As String

        Dim listRec As New List(Of TokubetuTaiouRecordBase)
        Dim dtRec As New TokubetuTaiouRecordBase
        Dim sb As New StringBuilder

        '各行ごとに画面からレコードクラスに入れ込み
        listRec = GetRowCtrlToList()

        For Each dtRec In listRec

            If dtRec.CheckJyky = True Then
                '特別対応コード
                sb.Append(dtRec.TokubetuTaiouCd & EarthConst.SEP_STRING)
            End If
        Next

        Return (sb.ToString)
    End Function

    ''' <summary>
    ''' 特別対応画面/売上計上・発注書金額をセット
    ''' </summary>
    ''' <param name="ctrlRec">特別対応レコードCtrl</param>
    ''' <param name="strKeijyouFlg">計上FLG</param>
    ''' <param name="strHattyuuKingaku">発注書金額</param>
    ''' <remarks></remarks>
    Private Sub SetUriKeijyouHattyuuKingaku(ByRef ctrlRec As TokubetuTaiouRecordCtrl, ByVal strKeijyouFlg As String, ByVal strHattyuuKingaku As String)
        '売上計上FLG
        If strKeijyouFlg = EarthConst.ARI_VAL Then
            ctrlRec.AccHdnUriKeijyou.Value = EarthConst.ARI_VAL
            ctrlRec.AccTextUriKeijyou.Value = KEIJYOU_ZUMI
        Else
            ctrlRec.AccHdnUriKeijyou.Value = EarthConst.NASI_VAL
            ctrlRec.AccTextUriKeijyou.Value = String.Empty
        End If

        '発注書金額
        ctrlRec.AccHdnHattyuuKingaku.Value = strHattyuuKingaku

    End Sub

#End Region

#Region "チェックボックス関連"
    ''' <summary>
    ''' 特別対応チェックボックス/チェック状態変更時、設定先を再取得する
    ''' </summary>
    ''' <param name="ttList">特別対応レコードのリスト</param>
    ''' <remarks></remarks>
    Private Sub ps_SetSetteiSaki(ByRef ttList As List(Of TokubetuTaiouRecordBase))
        Dim ttRec As New TokubetuTaiouRecordBase
        Dim ctrlTokubetu As New TokubetuTaiouRecordCtrl
        Dim strResult As String

        Dim intCntCtrl As Integer = 0
        Dim intMaxCnt As Integer = 0
        intMaxCnt = pListCtrl.Count

        For intCntCtrl = 1 To intMaxCnt

            ttRec = ttList(intCntCtrl - 1)
            ctrlTokubetu = pListCtrl(intCntCtrl - 1)

            '設定先を再取得
            If Not ttRec Is Nothing Then
                '取得結果を指定の特別対応レコードコントロールの設定先に対して設定する
                strResult = cbLogic.DevideTokubetuCd(Me, ttRec.BunruiCd, ttRec.GamenHyoujiNo)

                '設定先の表示切替処理を行なう
                If strResult <> String.Empty Then
                    '設定先を表示
                    ctrlTokubetu.AccTextSetteiSaki.Value = SETTEI_SAKI & strResult
                    '分類コード
                    ctrlTokubetu.AccHdnBunruiCd.Value = cl.GetDisplayString(ttRec.BunruiCd)
                    '画面表示NO
                    ctrlTokubetu.AccHdnGamenHyoujiNo.Value = cl.GetDisplayString(ttRec.GamenHyoujiNo)
                Else
                    '設定先を表示
                    ctrlTokubetu.AccTextSetteiSaki.Value = String.Empty
                    '分類コード
                    ctrlTokubetu.AccHdnBunruiCd.Value = String.Empty
                    '画面表示NO
                    ctrlTokubetu.AccHdnGamenHyoujiNo.Value = String.Empty
                End If

            End If
        Next
    End Sub

    ''' <summary>
    ''' 特別対応レコード(DB値)に画面情報をセットして返却
    ''' </summary>
    ''' <param name="jibanRec">地盤レコード</param>
    ''' <returns>特別対応レコードのリスト</returns>
    ''' <remarks></remarks>
    Private Function GetTokubetuInfo(ByVal jibanRec As JibanRecordBase) As List(Of TokubetuTaiouRecordBase)

        Dim ctrlTokubetu As New TokubetuTaiouRecordCtrl
        Dim ttDispRec As New TokubetuTaiouRecordBase
        Dim ttList As New List(Of TokubetuTaiouRecordBase)
        Dim intCnt As Integer
        Dim intListCnt As Integer
        Dim intCtrlCnt As Integer = 0

        With jibanRec
            If Not jibanRec Is Nothing AndAlso Not .Syouhin1Record Is Nothing Then
                '特別対応マスタベースの特別対応データを取得
                ttList = mttLogic.GetTokubetuTaiouInfo(Me, _
                                                       .Kbn, _
                                                       .HosyousyoNo, _
                                                       .KameitenCd, _
                                                       .Syouhin1Record.SyouhinCd, _
                                                       .TysHouhou, _
                                                       intCnt)
            End If
        End With

        '処理件数チェック
        If intCnt <= 0 Then
            Return Nothing
        End If
        '***************************************
        ' 特別対応データ
        '***************************************
        For intCtrlCnt = 0 To pListCtrl.Count - 1

            ctrlTokubetu = pListCtrl(intCtrlCnt)
            ttDispRec = New TokubetuTaiouRecordBase

            '画面情報をレコードにセット
            ttDispRec = ctrlTokubetu.GetChkRowCtrlToDataRec

            '特別対応マスタベースの特別対応データ
            For intListCnt = 0 To ttList.Count - 1

                'DB値を画面情報で上書き
                If ttList(intListCnt).mTokubetuTaiouCd = ttDispRec.TokubetuTaiouCd Then
                    With ttList(intListCnt)
                        .TokubetuTaiouCd = ttDispRec.TokubetuTaiouCd
                        .Torikesi = IIf(ttDispRec.CheckJyky = True, 0, 1)
                        .BunruiCd = ttDispRec.BunruiCd
                        .GamenHyoujiNo = ttDispRec.GamenHyoujiNo
                        .KasanSyouhinCd = ttDispRec.KasanSyouhinCd
                        .KoumutenKasanGaku = ttDispRec.KoumutenKasanGaku
                        .UriKasanGaku = ttDispRec.UriKasanGaku
                        .KkkSyoriFlg = ttDispRec.KkkSyoriFlg
                        .UpdFlg = IIf(ttDispRec.HenkouCheck = True, 1, 0)
                        .SetteiSakiStyle = ttDispRec.SetteiSakiStyle
                    End With

                    Exit For
                End If
            Next
        Next

        Return ttList
    End Function

    ''' <summary>
    ''' チェックボックスのClientIDをもとに該当のCtrlを取得
    ''' </summary>
    ''' <param name="strId">チェックボックスのClientID</param>
    ''' <returns>特別対応レコードCtrl</returns>
    ''' <remarks></remarks>
    Public Function GetChkRowCtrl(ByVal strId As String) As TokubetuTaiouRecordCtrl
        Dim ctrlTokubetu As New TokubetuTaiouRecordCtrl
        Dim intCntCtrl As Integer = 0
        Dim intMaxCnt As Integer = 0

        intMaxCnt = pListCtrl.Count

        '***************************************
        ' 特別対応データ
        '***************************************
        For intCntCtrl = 1 To intMaxCnt

            ctrlTokubetu = pListCtrl(intCntCtrl - 1)

            If ctrlTokubetu.AccCheckBoxTokubetuTaiou.ClientID = strId Then
                Return ctrlTokubetu
            End If
        Next

        Return Nothing
    End Function

#End Region

#End Region

#Region "ボタンイベント"
    ''' <summary>
    ''' 修正実行ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTouroku1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTouroku1.ServerClick

        '入力チェック
        If checkInput() = False Then Exit Sub

        ' 画面の内容をDBに反映する
        If SaveData() Then '登録成功

            '特別対応ボタン色戻しFLGを立てる(親画面用)
            Me.HiddenPressMasterFlg.Value = EarthEnum.emTokubetuTaiouActBtn.PressBtnMstTouroku

            '画面を閉じる
            Dim tmpScript1 As String = "window.returnValue = " & Me.HiddenPressMasterFlg.Value & ";" & "window.close();" '画面を閉じる
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", tmpScript1, True)

        Else
            '登録失敗
            MLogic.AlertMessage(sender, Messages.MSG019E.Replace("@PARAM1", "登録/修正"), 0, "ButtonTouroku1_ServerClick")
        End If

    End Sub

    ''' <summary>
    ''' マスター再取得ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonGetMaster_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGetMaster.ServerClick
        '****************************************************************************
        ' 加盟店商品調査方法特別対応マスタ設定
        '****************************************************************************
        Me.SetCtrlFromDataRecMst(sender, e)

    End Sub

    ''' <summary>
    ''' 特別対応チェックボックス/チェック状態変更時、該当の特別対応の設定先を判定して画面にセットする
    ''' </summary>
    ''' <param name="strClientId">チェックボックスのClientID</param>
    ''' <remarks></remarks>
    Private Sub SetCheckTokubetuTaiouChangeOyaAction(ByVal strClientId As String)

        Dim jr As New JibanRecordBase
        Dim ttRec As New TokubetuTaiouRecordBase            '画面情報
        Dim ttList As New List(Of TokubetuTaiouRecordBase)  '画面情報
        Dim intTmpKingakuAction As EarthEnum.emKingakuAction = EarthEnum.emKingakuAction.KINGAKU_NOT_ACTION
        Dim intCnt As Integer = 0

        '地盤データの取得
        jr = Me.GetCtrlFromJibanRec()

        '特別対応データの取得
        ttList = GetRowCtrlToList()

        '取得結果をもとに設定先情報を判定する
        Dim blnSyouhin1Henkou As Boolean = IIf(Me.HiddenTokutaiKkkHaneiFlg.Value = "1", True, False)
        intTmpKingakuAction = cbLogic.pf_ChkTokubetuTaiouKkk(Me, ttList, jr, blnSyouhin1Henkou)
        '※チェック時イベントではエラーMSGは表示しない
        'If intTmpKingakuAction <= EarthEnum.emKingakuAction.KINGAKU_ALERT Then
        '    MLogic.AlertMessage(Me, Messages.MSG200W.Replace("@PARAM1", cbLogic.AccTokubetuTaiouKkkMsg), 0, "KkkException")
        'End If

        'チェックされている全行の設定先を再取得
        Me.ps_SetSetteiSaki(ttList)

        'フォーカスの再設定
        Dim tmpScript As String
        If strClientId <> String.Empty Then
            tmpScript = "objEBI('" & strClientId & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "focusSet", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' 設定先再設定処理(非表示)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonSetSetteiSaki_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSetSetteiSaki.ServerClick
        Me.SetCheckTokubetuTaiouChangeOyaAction(String.Empty)
    End Sub

#End Region

End Class