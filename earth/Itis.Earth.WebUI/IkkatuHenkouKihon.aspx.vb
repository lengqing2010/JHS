
Partial Public Class IkkatuHenkouKihon
    Inherits System.Web.UI.Page

#Region "一括変更変更対象一覧行コントロールID接頭語"
    Protected ME_CLIENT_ID As String
    Protected Const IKKATU_HENKOU1_CTRL_NAME As String = "uCtrl_"
#End Region

    ''' <summary>
    ''' 一括変更物件基本情報/行ユーザーコントロール格納リスト
    ''' </summary>
    ''' <remarks></remarks>
    Private pListCtrl As New List(Of IkkatuHenkouKihonRecordCtrl)

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

    Dim MyLogic As New IkkatuHenkouKihonLogic
    Dim cbLogic As New CommonBizLogic

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
        Dim logic As New JibanLogic

        'ContentPlaceHolderのIDを取得
        ME_CLIENT_ID = Me.ButtonIkkatuHenkou.Parent.ClientID & Me.ClientIDSeparator.ToString

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合
            Me.ButtonIkkatuHenkou.Visible = False
            CloseWindow()
            Exit Sub
        End If

        If IsPostBack = False Then '初期起動時

            '●パラメータのチェック
            ' Key情報を保持
            _kbn = Request("sendPage_kubun")
            _no = Request("sendPage_hosyoushoNo")

            ' パラメータ不足時は画面を表示しない
            If _kbn Is Nothing Or _no Is Nothing Then
                Me.ButtonIkkatuHenkou.Visible = False
                CloseWindow()
                Exit Sub
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
                Me.ButtonIkkatuHenkou.Visible = False
                CloseWindow()
                Exit Sub
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            ' コンボ設定ヘルパークラスを生成
            Dim helper As New DropDownHelper
            ' 経由コンボにデータをバインドする
            helper.SetDropDownList(SelectKeiyu, DropDownHelper.DropDownType.Keiyu, False, True)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            Me.SetDispAction()

            'ボタン押下イベントの設定
            Me.setBtnEvent()

            '****************************************************************************
            ' 地盤データ取得
            '****************************************************************************
            If logic.ExistsJibanData(_kbn, _no) Then
                Me.SetCtrlFromJibanRec(sender, e)
            Else
                Me.ButtonIkkatuHenkou.Visible = False
                CloseWindow()
                Exit Sub
            End If

            If Me.ButtonIkkatuHenkou.Disabled = False Then
                Me.ButtonIkkatuHenkou.Focus() '一括変更ボタンにフォーカス
            End If

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

        '画面制御
        Me.SetEnableControl()

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDispAction()
        Dim jBn As New Jiban '地盤画面クラス

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' プルダウン/コード入力連携設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'なし

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' Javascript関連セット
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim checkDate As String = "checkDate(this);"
        Dim disabledOnkeydown As String = "if (event.keyCode == 13){return false;};"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ***コードおよび検索ポップアップボタン
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '分譲コード
        Me.TextBunjouCode.Attributes("onblur") = "if(checkNumber(this)) checkMinus(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 金額系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'なし

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 日付系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '調査希望日
        Me.TextTyousaKibouDate.Attributes("onblur") = checkDate
        Me.TextTyousaKibouDate.Attributes("onkeydown") = disabledOnkeydown

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ ドロップダウンリスト
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'なし

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 数値系
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'なし

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
        Dim tmpScript As String = "if(confirm('" & Messages.MSG017C & "')){setWindowOverlay(this,null,1);}else{return false;}"

        '登録許可MSG確認後、OKの場合DB更新処理を行なう
        Me.ButtonIkkatuHenkou.Attributes("onclick") = tmpScript

    End Sub

    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="sender">地盤レコード</param>
    ''' <param name="e">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim jSM As New JibanSessionManager 'セッション管理クラス
        Dim jBn As New Jiban '地盤画面クラス

        Dim arrKbn() As String = Split(_kbn, EarthConst.SEP_STRING)
        Dim arrNo() As String = Split(_no, EarthConst.SEP_STRING)

        '区分と番号のパラメータ引渡し数が異なる場合
        If arrKbn.Length <> arrNo.Length Then
            CloseWindow()
            Exit Sub
        End If

        Dim logic As New JibanLogic
        Dim jibanRec As New JibanRecordBase

        Dim intCnt As Integer  'カウンタ
        Dim intDBCnt As Integer = 1 'DBカウント
        Dim intHissuChk As Integer = 0 '必須入力チェック

        For intCnt = 0 To arrKbn.Length - 1

            Dim ctrlBukkenInfoRec As New IkkatuHenkouKihonRecordCtrl

            '地盤データの取得
            jibanRec = logic.GetJibanData(arrKbn(intCnt), arrNo(intCnt), True)
            If Not jibanRec Is Nothing AndAlso jibanRec.Kbn IsNot Nothing Then

                '必須項目(加盟店コード)が未入力の場合
                If jibanRec.KameitenCd Is Nothing Then
                    intHissuChk = 1
                    Exit For
                End If

                ctrlBukkenInfoRec = Me.LoadControl("control/IkkatuHenkouKihonRecordCtrl.ascx")
                ctrlBukkenInfoRec.ID = IKKATU_HENKOU1_CTRL_NAME & intDBCnt.ToString

                'テーブルに明細行を一行追加
                Me.tblMeisai.Controls.Add(ctrlBukkenInfoRec)

                '地盤データをコントロールにセット
                ctrlBukkenInfoRec.SetCtrlFromJibanRec(sender, e, jibanRec)

                'CSSスタイルの設定
                If (intDBCnt Mod 2) = 0 Then '偶数行の場合
                    ctrlBukkenInfoRec.AccTrIkkatuHenkouRec1.Attributes("class") = "even"
                    ctrlBukkenInfoRec.AccTrIkkatuHenkouRec2.Attributes("class") = "even"
                    ctrlBukkenInfoRec.AccTrIkkatuHenkouRec3.Attributes("class") = "even"
                    ctrlBukkenInfoRec.AccTrIkkatuHenkouRec4.Attributes("class") = "even"
                Else '奇数行の場合
                    ctrlBukkenInfoRec.AccTrIkkatuHenkouRec1.Attributes("class") = "odd"
                    ctrlBukkenInfoRec.AccTrIkkatuHenkouRec2.Attributes("class") = "odd"
                    ctrlBukkenInfoRec.AccTrIkkatuHenkouRec3.Attributes("class") = "odd"
                    ctrlBukkenInfoRec.AccTrIkkatuHenkouRec4.Attributes("class") = "odd"
                End If

                pListCtrl.Add(ctrlBukkenInfoRec)

                intDBCnt += 1 'カウントアップ
            End If

        Next

        '該当データが一件もない場合
        If pListCtrl.Count = 0 Or intHissuChk <> 0 Then
            CloseWindow()
            Exit Sub
        End If


    End Sub

#Region "ボタンイベント"

    ''' <summary>
    ''' 登録/修正 実行ボタン１,２押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonIkkatuHenkou_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonIkkatuHenkou.ServerClick

        Dim tmpScript As String = ""

        ' 入力チェック
        If checkInput() = False Then Exit Sub

        ' 画面の内容をDBに反映する
        If SaveData() Then '登録成功
            '画面を閉じる
            CloseWindow()

        Else '登録失敗
            tmpScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "一括変更") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonIkkatuHenkou_ServerClick2", tmpScript, True)
        End If

    End Sub

#End Region

#Region "プライベートメソッド"

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
        Dim e As New System.EventArgs

        '地盤画面共通クラス
        Dim jBn As New Jiban

        '全てのコントロールを有効化(破棄種別チェック用)
        Dim noTarget As New Hashtable
        jBn.ChangeDesabledAll(Me, False, noTarget)

        'エラーメッセージ初期化
        Dim errMess As String = ""
        Dim arrFocusTargetCtrl As New List(Of Control)

        '変更対象一覧のセット数を取得（tblMeisaiの先頭にはLiteralControlが含まれる為、マイナス１）
        Dim intRowCnt As Integer = Me.tblMeisai.Controls.Count - 1
        Dim intCnt As Integer = 0
        Dim ctrlBukkenInfoRec As IkkatuHenkouKihonRecordCtrl

        Dim strTmpVal As String = String.Empty
        Dim dicTmp As New Dictionary(Of String, String)

        For intCnt = 0 To intRowCnt - 1
            '1行ずつチェック
            ctrlBukkenInfoRec = Me.tblMeisai.Controls(intCnt + 1)

            With ctrlBukkenInfoRec
                '入力チェック
                .checkInput(errMess, arrFocusTargetCtrl)

                '●≠リストの作成
                strTmpVal = .AccHiddenKbn.Value & .AccHiddenNo.Value
                If strTmpVal <> .AccTextNayoseCode.Text Then '画面.物件NO<>画面.物件名寄コード
                    dicTmp.Add(strTmpVal, .AccTextNayoseCode.Text)
                End If
            End With

        Next

        '名寄状況チェック
        For Each de As KeyValuePair(Of String, String) In dicTmp
            If Not dicTmp(de.Key) Is Nothing Then
                If dicTmp.ContainsKey(de.Value) Then
                    errMess += Messages.MSG172E.Replace("@PARAM1", de.Key.ToString).Replace("@PARAM2", de.Value.ToString)
                End If
            End If
        Next

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
        '邸別請求データは更新しない
        '*************************

        Dim logic As New IkkatuHenkouKihonLogic

        '各行ごとに画面からレコードクラスに入れ込み
        Dim listJibanRec As List(Of JibanRecordIkkatuHenkouKihon) = Nothing

        listJibanRec = Me.GetRowCtrlToJibanRec()
        If listJibanRec Is Nothing Then
            Return False
        End If

        ' データの更新を行います
        If logic.SaveJibanData(Me, listJibanRec) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 画面の各明細行情報をレコードクラスに取得し、地盤レコードクラスのリストを返却する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetRowCtrlToJibanRec() As List(Of JibanRecordIkkatuHenkouKihon)
        '*************************
        '地盤データは更新対象のみ
        '邸別請求データは更新しない
        '*************************

        Dim intCnt As Integer = 0
        Dim ctrl As IkkatuHenkouKihonRecordCtrl
        Dim listJibanRec As New List(Of JibanRecordIkkatuHenkouKihon)

        For Each ctrl In pListCtrl

            With ctrl

                Dim JibanLogic As New JibanLogic
                Dim jr As New JibanRecord
                ' 現在の地盤データをDBから取得する
                jr = JibanLogic.GetJibanData(.AccHiddenKbn.Value, .AccHiddenNo.Value)

                ' 画面内容より地盤レコードを生成する
                Dim jibanRec As New JibanRecordIkkatuHenkouKihon

                '進捗T更新用に、DB上の値をセットする
                JibanLogic.SetSintyokuJibanData(jr, jibanRec)

                '***************************************
                ' 地盤データ
                '***************************************
                ' 施主名
                cl.SetDisplayString(.AccTextSesyuMei.Text, jibanRec.SesyuMei)
                ' 調査希望日
                cl.SetDisplayString(.AccTextTyousaKibouDate.Text, jibanRec.TysKibouDate)
                ' 調査希望時間
                cl.SetDisplayString(.AccTextTyousaKibouJikan.Text, jibanRec.TysKibouJikan)
                ' 物件住所1
                cl.SetDisplayString(.AccTextBukkenJyuusyo1.Text, jibanRec.BukkenJyuusyo1)
                ' 物件住所2
                cl.SetDisplayString(.AccTextBukkenJyuusyo2.Text, jibanRec.BukkenJyuusyo2)
                ' 物件住所3
                cl.SetDisplayString(.AccTextBukkenJyuusyo3.Text, jibanRec.BukkenJyuusyo3)
                ' 備考
                cl.SetDisplayString(.AccTextBikou.Text, jibanRec.Bikou)
                ' 分譲コード
                cl.SetDisplayString(.AccTextBunjouCode.Text, jibanRec.BunjouCd)
                ' 物件名寄コード
                If .AccTextNayoseCode.Text.Trim() = String.Empty Then
                    cl.SetDisplayString(.AccTextKokyakuBangou.Text, jibanRec.BukkenNayoseCd)
                Else
                    cl.SetDisplayString(.AccTextNayoseCode.Text, jibanRec.BukkenNayoseCd)
                End If
                ' 受注物件名
                cl.SetDisplayString(.AccTextBukkenMei.Text, jibanRec.JyutyuuBukkenMei)
                ' 経由
                cl.SetDisplayString(.AccSelectKeiyu.SelectedValue, jibanRec.Keiyu)


                '***************************************
                ' 画面入力項目以外
                '***************************************
                ' 区分
                jibanRec.Kbn = .AccHiddenKbn.Value
                ' 番号（保証書NO）
                jibanRec.HosyousyoNo = .AccHiddenNo.Value
                ' 更新者ユーザーID
                jibanRec.UpdLoginUserId = userinfo.LoginUserId
                ' 更新日時 読み込み時のタイムスタンプ
                If .AccupdateDateTime.Value = "" Then
                    jibanRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
                Else
                    jibanRec.UpdDatetime = DateTime.ParseExact(.AccupdateDateTime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
                End If
                '更新者
                jibanRec.Kousinsya = cbLogic.GetKousinsya(userinfo.LoginUserId, DateTime.Now)

                listJibanRec.Add(jibanRec) 'リストに追加

            End With

        Next

        Return listJibanRec
    End Function

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

        Dim ctrlBukkenInfoRec As New IkkatuHenkouKihonRecordCtrl

        For intCnt As Integer = 0 To intRowCnt - 1

            With Me.tblMeisai.Controls

                ctrlBukkenInfoRec = Me.LoadControl("control/IkkatuHenkouKihonRecordCtrl.ascx")
                ctrlBukkenInfoRec.ID = IKKATU_HENKOU1_CTRL_NAME & (intCnt + 1).ToString

                .Add(ctrlBukkenInfoRec)

            End With

            pListCtrl.Add(ctrlBukkenInfoRec)

        Next
    End Sub

    ''' <summary>
    ''' コントロールの画面制御
    ''' </summary>
    ''' <remarks>コントロールの画面制御を行なう</remarks>
    Public Sub SetEnableControl()

        Dim ctrl As New IkkatuHenkouKihonRecordCtrl

        Dim jSM As New JibanSessionManager
        Dim ht As New Hashtable
        Dim htNotTarget As New Hashtable

        '依頼業務権限
        If userinfo.IraiGyoumuKengen = 0 Then
            '画面上部
            '調査希望日ボタン
            Me.ButtonCopyTyousaKibouDate.Disabled = True
            '調査希望時間ボタン
            Me.ButtonCopyTyousaKibouJikan.Disabled = True

            Me.ButtonCopyTyousaKibouDate.Attributes.Remove("onclick")
            Me.ButtonCopyTyousaKibouJikan.Attributes.Remove("onclick")

            Me.TextTyousaKibouDate.Enabled = False
            Me.TextTyousaKibouJikan.Enabled = False

            '明細行
            For Each ctrl In pListCtrl
                jSM.Hash2Ctrl(ctrl.AccTdTyousaKibouDate, EarthConst.MODE_VIEW, ht) '調査希望日
                jSM.Hash2Ctrl(ctrl.AccTdTyousaKibouJikan, EarthConst.MODE_VIEW, ht) '調査希望時間
            Next
        End If

    End Sub

    ''' <summary>
    ''' 当画面を閉じるスクリプトを生成する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CloseWindow()
        Dim tmpScript As String = "window.close();" '画面を閉じる
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseWindow", tmpScript, True)
    End Sub

#End Region

End Class