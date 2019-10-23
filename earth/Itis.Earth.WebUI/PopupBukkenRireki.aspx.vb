
Partial Public Class PopupBukkenRireki
    Inherits System.Web.UI.Page

#Region "物件履歴・行コントロールID接頭語"
    Private Const BUKKEN_RIREKI_CTRL_NAME As String = "CtrlBukkenRireki_"
    Private Const SELECT_SYUBETU_CTRL_NAME As String = "SelectSyubetu_"
#End Region

    ''' <summary>
    ''' 物件履歴明細行情報/行ユーザーコントロール格納リスト
    ''' </summary>
    ''' <remarks></remarks>
    Private pListCtrl As New List(Of BukkenRirekiRecordCtrl)

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim iraiSession As New IraiSession
    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    ''' <summary>
    ''' メッセージクラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic

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

        ' Key情報を保持
        Dim arrSearchTerm() As String

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合
            cl.CloseWindow(Me)
            Me.ButtonSinki.Style("display") = "none"
            Exit Sub
        End If

        If IsPostBack = False Then '初期起動時

            '●パラメータのチェック
            If Context.Items("sendSearchTerms") IsNot Nothing Then '物件履歴修正からの呼出
                arrSearchTerm = Split(Context.Items("sendSearchTerms"), EarthConst.SEP_STRING)
            Else
                '各業務画面からの呼出
                arrSearchTerm = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
                '物件ダイレクト画面からの呼出
                pStrKbn = Request("sendPage_kubun")
                pStrBangou = Request("sendPage_hosyoushoNo")
            End If

            If arrSearchTerm.Length >= 2 Then
                pStrKbn = arrSearchTerm(0)     '親画面からPOSTされた情報1 ：区分
                pStrBangou = arrSearchTerm(1)     '親画面からPOSTされた情報2 ：保証書NO
            End If

            ' パラメータ不足時は画面を表示しない
            If pStrKbn Is Nothing Or pStrBangou Is Nothing Then
                cl.CloseWindow(Me)
                Me.ButtonSinki.Style("display") = "none"
                Exit Sub
            End If

            '●権限のチェック
            '以下のいずれかの権限がない場合、画面参照のみ
            '依頼業務権限
            '結果業務権限
            '保証業務権限
            '報告書業務権限
            '工事業務権限
            '経理業務権限
            If userinfo.IraiGyoumuKengen = 0 _
                And userinfo.KekkaGyoumuKengen = 0 _
                And userinfo.HosyouGyoumuKengen = 0 _
                And userinfo.HoukokusyoGyoumuKengen = 0 _
                And userinfo.KoujiGyoumuKengen = 0 _
                And userinfo.KeiriGyoumuKengen = 0 Then

                Me.HiddenKengen.Value = "0"

                '新規ボタン
                Me.ButtonSinki.Style("display") = "none"
            Else
                Me.HiddenKengen.Value = "1"

                '新規ボタン
                Me.ButtonSinki.Style("display") = "inline"
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            Dim helper As New DropDownHelper

            '種別コンボにセット
            helper.SetMeisyouDropDownList(Me.SelectSyubetu, EarthConst.emMeisyouType.BUKKEN_RIREKI_SYUBETU, True, True)

            '●ダミーコンボにセット
            helper.SetMeisyouDropDownList(Me.SelectTmpCode, EarthConst.emMeisyouType.BUKKEN_RIREKI_SYUBETU, False, True)

            'ダミードロップダウンリストの生成
            Me.CreateDropDownList(Me.SelectTmpCode)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            '画面上部・取消行非表示
            Me.RadioTorikesiDispNone.Checked = True

            '画面上部.種別絞込
            Me.SelectSyubetu.Attributes("onchange") = "SyubetuDisp();"

            '****************************************************************************
            ' 地盤データ取得
            '****************************************************************************
            Dim logic As New JibanLogic
            Dim jibanRec As New JibanRecordBase
            jibanRec = logic.GetJibanData(pStrKbn, pStrBangou)

            '地盤読み込みデータがセッションに存在する場合、画面に表示させる
            If logic.ExistsJibanData(pStrKbn, pStrBangou) AndAlso jibanRec IsNot Nothing Then
                Me.SetCtrlFromJibanRec(sender, e, jibanRec) '地盤データをコントロールにセット
            Else
                cl.CloseWindow(Me)
                Me.ButtonSinki.Style("display") = "none"
                Exit Sub
            End If

            '****************************************************************************
            ' 物件履歴データ取得
            '****************************************************************************
            Me.SetCtrlFromDataRec(sender, e)

            Me.ButtonClose.Focus() 'フォーカス

        Else
            '画面項目設定処理(ポストバック用)
            Me.setDisplayPostBack()

            'ダミードロップダウンリストの生成
            Me.CreateDropDownList(Me.SelectTmpCode)

        End If

    End Sub

    ''' <summary>
    ''' ページロードコンプリート
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        '明細行数取得
        Me.HiddenLineCnt.Value = pListCtrl.Count.ToString
    End Sub

    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行う（参照処理用）
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)

        ' コンボ設定ヘルパークラスを生成
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

        '画面コントロールに設定
        '●ダミーコンボにセット
        objDrpTmp = New DropDownList
        helper.SetDropDownList(objDrpTmp, DropDownHelper.DropDownType.Kubun, False, True)
        objDrpTmp.SelectedValue = jr.Kbn
        Me.TextKbn.Text = objDrpTmp.SelectedItem.Text '区分
        Me.HiddenKbn.Value = jr.Kbn '隠しフィールド

        Me.TextBangou.Text = cl.GetDispStr(jr.HosyousyoNo) '番号
        Me.TextSesyuMei.Text = cl.GetDispStr(jr.SesyuMei) '施主名

    End Sub

    ''' <summary>
    ''' 物件履歴レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="sender">地盤レコード</param>
    ''' <param name="e">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim jSM As New JibanSessionManager 'セッション管理クラス
        Dim jBn As New Jiban '地盤画面クラス

        Dim logic As New BukkenRirekiLogic
        Dim listRec As List(Of BukkenRirekiRecord)

        Dim intCnt As Integer = 0 'カウンタ

        Dim ctrlBukkenInfoRec As New BukkenRirekiRecordCtrl

        'データの取得
        listRec = logic.getBukkenRirekiList(sender, pStrKbn, pStrBangou)

        If Not listRec Is Nothing AndAlso listRec.Count > 0 Then
            '件数分ループ
            For intCnt = 0 To listRec.Count - 1

                ctrlBukkenInfoRec = Me.LoadControl("control/BukkenRirekiRecordCtrl.ascx")
                ctrlBukkenInfoRec.ID = BUKKEN_RIREKI_CTRL_NAME & (intCnt + 1).ToString

                'テーブルに明細行を一行追加
                Me.tblMeisai.Controls.Add(ctrlBukkenInfoRec)

                'データをコントロールにセット
                ctrlBukkenInfoRec.SetCtrlFromDataRec(sender, e, listRec(intCnt), CInt(Me.HiddenKengen.Value))

                '更新ログインユーザID
                ctrlBukkenInfoRec.AccStrLoginUser = userinfo.LoginUserId

                pListCtrl.Add(ctrlBukkenInfoRec)

            Next

        End If

    End Sub

#Region "プライベートメソッド"

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
    ''' 行を作成します
    ''' </summary>
    ''' <param name="intRowCnt">作成する行数</param>
    ''' <remarks></remarks>
    Private Sub createRow(ByVal intRowCnt As Integer)

        Dim ctrlBukkenInfoRec As New BukkenRirekiRecordCtrl

        For intCnt As Integer = 0 To intRowCnt - 1

            With Me.tblMeisai.Controls

                ctrlBukkenInfoRec = Me.LoadControl("control/BukkenRirekiRecordCtrl.ascx")
                ctrlBukkenInfoRec.ID = BUKKEN_RIREKI_CTRL_NAME & (intCnt + 1).ToString

                .Add(ctrlBukkenInfoRec)

            End With

            pListCtrl.Add(ctrlBukkenInfoRec)

        Next

    End Sub

    ''' <summary>
    ''' ダミードロップダウンリストの生成
    ''' </summary>
    ''' <param name="SelectTarget">対象元ドロップダウンリスト</param>
    ''' <remarks>名称M.名称種別=16に紐付く名称M.コード=名称M.名称種別のダミードロップダウンリストを生成する</remarks>
    Private Sub CreateDropDownList(ByRef SelectTarget As DropDownList)

        Dim helper As New DropDownHelper

        '●ダミーコンボにセット
        Dim objDrpTmp As DropDownList

        Dim intCnt As Integer = 0
        Dim intValue As Integer

        If SelectTarget.Items.Count <= 0 Then
            Dim strMsg As String = Messages.MSG113E.Replace("@PARAM1", "種別")
            Dim tmpScript As String = "alert('" & strMsg & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CreateDropDownList", tmpScript, True)

            cl.CloseWindow(Me)
            Me.ButtonSinki.Style("display") = "none"
            Exit Sub
        End If

        For intCnt = 0 To SelectTarget.Items.Count - 1
            intValue = SelectTarget.Items(intCnt).Value 'Value値取得

            objDrpTmp = New DropDownList
            objDrpTmp.ID = SELECT_SYUBETU_CTRL_NAME & intValue.ToString 'ID付与
            objDrpTmp.Style("display") = "none" '非表示
            helper.SetMeisyouDropDownList(objDrpTmp, intValue) '値セット

            Me.divSelect.Controls.Add(objDrpTmp) 'コントロール追加
        Next

    End Sub

#End Region

#Region "ボタンイベント"

    ''' <summary>
    ''' 再読込 ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonReload_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReload.ServerClick

        '登録完了後、画面をリロードするために、キー情報を引き渡す
        Context.Items("sendSearchTerms") = Me.HiddenKbn.Value & EarthConst.SEP_STRING & Me.TextBangou.Text

        '画面遷移（リロード）
        Server.Transfer(UrlConst.POPUP_BUKKEN_RIREKI)

    End Sub
#End Region

End Class