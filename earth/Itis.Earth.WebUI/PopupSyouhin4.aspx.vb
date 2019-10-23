Partial Public Class PopupSyouhin4
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' 商品4明細行情報/行ユーザーコントロール格納リスト
    ''' </summary>
    ''' <remarks></remarks>
    Private pListCtrl As New List(Of Syouhin4RecordCtrl)

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    Dim MLogic As New MessageLogic
    Dim cbLogic As New CommonBizLogic

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
    ''' 調査会社コード
    ''' </summary>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Private _TysKaisyaCd As String
    ''' <summary>
    ''' 調査会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>リクエストパラメータ格納用</remarks>
    Public Property pStrTysKaisyaCd() As String
        Get
            Return _TysKaisyaCd
        End Get
        Set(ByVal value As String)
            _TysKaisyaCd = value
        End Set
    End Property

#End Region

#Region "商品4・行コントロールID接頭語"
    Private Const SYOUHIN4_CTRL_NAME As String = EarthConst.USR_CTRL_ID_ITEM4
#End Region

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban    '地盤画面共通クラス
        Dim jSM As New JibanSessionManager
        Dim intKeiriKengen As Integer = 0
        Dim intIraiKengen As Integer = 0
        Dim intHattyuusyoKengen As Integer = 0

        'マスターページ情報を取得(ScriptManager用)
        masterAjaxSM = AjaxScriptManager

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合
            cl.CloseWindow(Me)
            Me.ButtonTouroku1.Visible = False
            Me.ButtonAddNewRow.Visible = False
            Exit Sub
        End If

        If IsPostBack = False Then '初期起動時

            pStrKbn = Request("kbn")
            pStrBangou = Request("no")
            pStrKameitenCd = Request("kameitencd")
            pStrTysKaisyaCd = Request("TysKaisyaCd")

            ' パラメータ不足時は画面を閉じる
            If pStrKbn Is Nothing Or pStrBangou Is Nothing Then
                cl.CloseWindow(Me)
                Me.ButtonTouroku1.Visible = False
                Me.ButtonAddNewRow.Visible = False
                Exit Sub
            End If

            '●権限の設定

            '経理業務権限、依頼業務権限、発注書管理権限
            intKeiriKengen = userinfo.KeiriGyoumuKengen
            intIraiKengen = userinfo.IraiGyoumuKengen
            intHattyuusyoKengen = userinfo.HattyuusyoKanriKengen

            '経理権限が無い場合は登録、追加ボタンを無効化
            If intKeiriKengen <> "-1" Then
                Me.ButtonTouroku1.Visible = False
                Me.ButtonAddNewRow.Visible = False
            End If

            'ユーザコントロール用にHiddenにセット
            Me.HiddenKeiriGyoumuKengen.Value = intKeiriKengen
            Me.HiddenIraiGyoumuKengen.Value = intIraiKengen
            Me.HiddenHattyuusyoKanriKengen.Value = intHattyuusyoKengen

            '共通情報の設定
            Me.HiddenKubun.Value = pStrKbn
            Me.HiddenNo.Value = pStrBangou
            Me.HiddenKameitenCd.Value = pStrKameitenCd
            Me.HiddenJibanTysKaisyaCd.Value = pStrTysKaisyaCd

            '****************************************************************************
            ' 地盤データ取得
            '****************************************************************************
            Dim logic As New JibanLogic
            Dim jibanRec As New JibanRecordBase
            jibanRec = logic.GetJibanData(pStrKbn, pStrBangou)

            '地盤データが存在する場合、画面に表示させる
            If jibanRec IsNot Nothing Then
                Me.SetCtrlFromJibanRec(sender, e, jibanRec)
            Else
                cl.CloseWindow(Me)
                Exit Sub
            End If

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            'ボタン押下イベントの設定
            Me.setBtnEvent()

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
    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        '明細行数取得
        Me.HiddenLineCnt.Value = pListCtrl.Count.ToString

        ' 一覧の背景色再設定
        Dim tmpScript As String
        tmpScript = "settingTable();"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "makeRowStripes", tmpScript, True)

        If IsPostBack = True Then
            '請求書情報の再設定
            setSeikyuuSiireHenkou(sender, e)
        End If

    End Sub

    ''' <summary>
    ''' 入力項目のチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function checkInput() As Boolean
        'エラーメッセージ初期化
        Dim errMess As String = ""
        Dim errTarget As String = ""
        Dim arrFocusTargetCtrl As New List(Of Control)
        Dim tmpScript As String = ""

        Dim intCnt As Integer = 0
        Dim intRowCnt As Integer = Me.tblMeisai.Controls.Count - 1
        Dim ctrlSyouhin4Rec As Syouhin4RecordCtrl

        '商品4ユーザコントロールのエラーチェック
        For intCnt = 0 To intRowCnt - 1
            ctrlSyouhin4Rec = Me.tblMeisai.Controls(intCnt + 1)
            ctrlSyouhin4Rec.CheckInput(errMess, arrFocusTargetCtrl, intCnt + 1)
        Next

        If errMess <> "" Then
            'フォーカスセット
            If arrFocusTargetCtrl.Count > 0 Then
                arrFocusTargetCtrl(0).Focus()
            End If
            'エラーメッセージ表示
            tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 画面の内容をDBに反映する
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function SaveData() As Boolean
        Dim lgcSyouhin As New Syouhin4Logic
        Dim jibanRec As New JibanRecord

        '各行ごとに画面からレコードクラスに入れ込み
        jibanRec = GetRowCtrlToSyouhinRec()

        'データの更新を行う
        If lgcSyouhin.SaveJibanData(Me, jibanRec, pListCtrl.Count) = False Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' 画面の各明細行情報をレコードクラスに取得し、地盤レコードクラスのリストを返却する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetRowCtrlToSyouhinRec() As JibanRecord
        Dim dicSyouhin4Records As New Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Dim jibanRec As New JibanRecord
        Dim intCntCtrl As Integer = 0

        '***************************************
        ' 地盤データ
        '***************************************
        '区分
        jibanRec.Kbn = Me.HiddenKbn.Value
        '番号（保証書NO）
        jibanRec.HosyousyoNo = Me.TextBangou.Text
        '更新者ユーザID
        jibanRec.UpdLoginUserId = userinfo.LoginUserId
        '更新日時
        jibanRec.UpdDatetime = Date.Parse(Me.HiddenRegUpdDate.Value)
        '更新者
        jibanRec.Kousinsya = cbLogic.GetKousinsya(userinfo.LoginUserId, DateTime.Now)

        '更新用にDictionaryを再生成する
        jibanRec.Syouhin4Records = New Dictionary(Of Integer, TeibetuSeikyuuRecord)

        '商品4情報の取得
        For intCntCtrl = 1 To pListCtrl.Count
            Dim ctrlSyouhin As Syouhin4RecordCtrl = pListCtrl(intCntCtrl - 1)

            '画面情報をレコードにセット
            jibanRec.Syouhin4Records.Add(intCntCtrl, ctrlSyouhin.setTeibetuToSyouhin)

            '親画面から共通情報をセット
            jibanRec.Syouhin4Records.Item(intCntCtrl).Kbn = Me.HiddenKbn.Value '区分
            jibanRec.Syouhin4Records.Item(intCntCtrl).HosyousyoNo = Me.TextBangou.Text '番号
            jibanRec.Syouhin4Records.Item(intCntCtrl).BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_4 '分類コード

        Next

        Return jibanRec
    End Function

    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行う（参照処理用）
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)

        '****************************************************************************
        ' 地盤データ取得&セット
        '****************************************************************************
        ' コンボ設定ヘルパークラスを生成
        Dim helper As New DropDownHelper
        Dim objDrpTmp As New DropDownList

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
        Me.TextSesyuMei.Text = cl.GetDispStr(jr.SesyuMei)
        '更新日時 なければ 登録日時
        Me.HiddenRegUpdDate.Value = IIf(jr.UpdDatetime <> Date.MinValue, _
                                        jr.UpdDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2), _
                                        jr.AddDatetime.ToString(EarthConst.FORMAT_DATE_TIME_2))

        '****************************************************************************
        ' データ取得&セット
        '****************************************************************************
        Me.SetCtrlFromDataRec(sender, e, jr)

    End Sub

    ''' <summary>
    ''' 各レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)
        Dim jSM As New JibanSessionManager 'セッション管理クラス
        Dim jBn As New Jiban '地盤画面クラス
        Dim IEnumRec As IEnumerator(Of TeibetuSeikyuuRecord)    '商品4レコード列挙用
        Dim logic As New JibanLogic
        Dim intCnt As Integer = 0 'カウンタ
        Dim ctrlSyouhin4InfoRec As New Syouhin4RecordCtrl
        Dim wkTsRec As New TeibetuSeikyuuRecord

        '商品4レコードの存在チェック
        If jr.Syouhin4Records Is Nothing Then
            Exit Sub
        End If

        '商品4レコード群から列挙オブジェクトに格納する
        IEnumRec = jr.Syouhin4Records.Values.GetEnumerator
        While IEnumRec.MoveNext
            wkTsRec = IEnumRec.Current  '1件ずつワークに格納
            '商品コードが存在する場合のみ画面表示
            If wkTsRec.SyouhinCd IsNot Nothing Then
                'ユーザコントロールの読込とIDの付与
                ctrlSyouhin4InfoRec = Me.LoadControl("control/Syouhin4RecordCtrl.ascx")
                ctrlSyouhin4InfoRec.ID = SYOUHIN4_CTRL_NAME & (intCnt + 1).ToString

                '共通情報の設定
                ctrlSyouhin4InfoRec.AccHdnKbn.Value = Me.HiddenKubun.Value                           '区分
                ctrlSyouhin4InfoRec.AccHdnKameitenCd.Value = Me.HiddenKameitenCd.Value               '加盟店コード
                ctrlSyouhin4InfoRec.AccHdnJibanTysKaisyaCd.Value = Me.HiddenJibanTysKaisyaCd.Value   '調査会社コード（地盤）
                ctrlSyouhin4InfoRec.KeiriGyoumuKengen = Me.HiddenKeiriGyoumuKengen.Value             '経理業務権限
                ctrlSyouhin4InfoRec.IraiGyoumuKengen = Me.HiddenIraiGyoumuKengen.Value               '依頼業務権限
                ctrlSyouhin4InfoRec.HattyuusyoKanriKengen = Me.HiddenHattyuusyoKanriKengen.Value     '発注書管理権限

                'テーブルに明細行を一行追加
                Me.tblMeisai.Controls.Add(ctrlSyouhin4InfoRec)

                'レコードから画面項目へセット
                ctrlSyouhin4InfoRec.SetCtrlFromDataRec(sender, e, jr, wkTsRec)

                '画面表示した項目をリストに追加
                pListCtrl.Add(ctrlSyouhin4InfoRec)

                intCnt += 1
            End If
        End While
    End Sub

    ''' <summary>
    ''' 商品毎の請求・仕入先が変更されていないかをチェックし、
    ''' 変更されている場合の再取得
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Function setSeikyuuSiireHenkou(ByVal sender As Object, ByVal e As System.EventArgs) As Boolean
        Dim ctrlSyouhin As Syouhin4RecordCtrl

        '一覧のレコード分請求先変更かのチェック
        For intCntCtrl As Integer = 1 To Me.tblMeisai.Controls.Count - 1
            ctrlSyouhin = FindControl(SYOUHIN4_CTRL_NAME & intCntCtrl)
            '請求先情報が変更されていた場合
            If ctrlSyouhin.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
                '商品検索ボタン押下時の処理を実行
                ctrlSyouhin.SetSyouhin(sender, e)
                Me.UpdatePanelSyouhin4.Update()
                '請求先変更フラグを初期化
                ctrlSyouhin.AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
                '変更された商品が有った場合、ループ終了(原則として、1商品毎しか変更されないため)
                Return True
                Exit Function
            End If
        Next

        Return False
    End Function

#Region "プライベートメソッド"
    ''' <summary>
    ''' 登録/修正ボタンの設定
    ''' </summary>
    ''' <remarks>
    ''' DB更新中、画面のグレイアウトを行なう。<br/>
    ''' </remarks>
    Private Sub setBtnEvent()
        'イベントハンドラ登録
        Dim tmpScript As String = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this,null,1);}else{return false;}"

        '登録許可MSG確認後、OKの場合更新処理を行う
        Me.ButtonTouroku1.Attributes("onclick") = tmpScript
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
        For intCnt As Integer = 0 To intMakeCnt - 1
            Me.createRow(intCnt)
        Next

    End Sub

    ''' <summary>
    ''' 行を作成します
    ''' </summary>
    ''' <param name="intRowCnt">現在の行数</param>
    ''' <remarks></remarks>
    Private Sub createRow(ByVal intRowCnt As Integer, Optional ByVal enabled As Boolean = False)
        Dim ctrlSyouhin4InfoRec As New Syouhin4RecordCtrl

        With Me.tblMeisai.Controls
            ctrlSyouhin4InfoRec = Me.LoadControl("control/Syouhin4RecordCtrl.ascx")
            ctrlSyouhin4InfoRec.ID = SYOUHIN4_CTRL_NAME & (intRowCnt + 1).ToString

            '地盤情報の設定
            ctrlSyouhin4InfoRec.AccHdnKbn.Value = Me.HiddenKubun.Value                          '区分
            ctrlSyouhin4InfoRec.AccHdnKameitenCd.Value = Me.HiddenKameitenCd.Value              '加盟店コード
            ctrlSyouhin4InfoRec.AccHdnJibanTysKaisyaCd.Value = Me.HiddenJibanTysKaisyaCd.Value  '調査会社コード（地盤）
            ctrlSyouhin4InfoRec.KeiriGyoumuKengen = Me.HiddenKeiriGyoumuKengen.Value            '経理業務権限
            ctrlSyouhin4InfoRec.IraiGyoumuKengen = Me.HiddenIraiGyoumuKengen.Value              '依頼業務権限
            ctrlSyouhin4InfoRec.HattyuusyoKanriKengen = Me.HiddenHattyuusyoKanriKengen.Value    '発注書管理権限

            .Add(ctrlSyouhin4InfoRec)
        End With

        pListCtrl.Add(ctrlSyouhin4InfoRec)

        '新規行の場合は非活性化
        If enabled Then
            ctrlSyouhin4InfoRec.initCtrl(False)
        End If
    End Sub

#End Region

#Region "ボタンイベント"

    ''' <summary>
    ''' 修正実行ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTouroku1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTouroku1.ServerClick

        '商品情報チェック
        If checkInput() = False Then Exit Sub

        ' 画面の内容をDBに反映する
        If SaveData() Then '登録成功
            '画面を閉じる
            cl.CloseWindow(Me)
        Else
            '登録失敗
            MLogic.AlertMessage(sender, Messages.MSG019E.Replace("@PARAM1", "登録/修正"), 0, "ButtonTouroku1_ServerClick")
        End If

    End Sub

    ''' <summary>
    ''' 新規行追加ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonAddNewRow_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddNewRow.ServerClick
        Dim intRowCnt As Integer

        '現在行の取得
        intRowCnt = IIf(Me.HiddenLineCnt.Value = String.Empty, 0, Me.HiddenLineCnt.Value)

        createRow(intRowCnt, True)

    End Sub

#End Region

End Class