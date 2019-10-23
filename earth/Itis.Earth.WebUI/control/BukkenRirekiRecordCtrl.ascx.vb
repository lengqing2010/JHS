
Partial Public Class BukkenRirekiRecordCtrl
    Inherits System.Web.UI.UserControl

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

#Region "コントロール値"
    Private Const CTRL_VALUE_BUTTON_SYUUSEI As String = "修正"
    Private Const CTRL_VALUE_BUTTON_KAKUNIN As String = "確認"
    Private Const CTRL_VALUE_SPACE As String = "&nbsp;"
#End Region

#Region "カスタムイベント宣言"
    ''' <summary>
    ''' 親画面の処理実行用カスタムイベント
    ''' </summary>
    ''' <remarks></remarks>
    Public Event OyaGamenAction()

#End Region

#Region "ユーザーコントロールへの外部からのアクセス用Getter/Setter"

    ''' <summary>
    ''' 外部からのアクセス用 for HiddenLoginUser
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccStrLoginUser() As String
        Get
            Return Me.HiddenLoginUser.Value
        End Get
        Set(ByVal value As String)
            Me.HiddenLoginUser.Value = value
        End Set
    End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '****************************************************************************
        ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
        '****************************************************************************
        'ボタン押下イベントの設定
        Me.setBtnEvent()

    End Sub

    ''' <summary>
    ''' 取消ボタンの設定
    ''' </summary>
    ''' <remarks>
    ''' DB更新の確認を行なう。<br/>
    ''' OK時：DB更新を行なう。<br/>
    ''' キャンセル時：DB更新を中断する。<br/>
    ''' </remarks>
    Private Sub setBtnEvent()

        'イベントハンドラ登録
        Dim tmpScriptOverLay As String = "setWindowOverlay(this,null,1);"
        Dim tmpScript As String = "if(confirm('" & Messages.MSG130C & "')){" & tmpScriptOverLay & "}else{return false;}"

        '取消許可MSG確認後、OKの場合DB更新処理を行なう
        ButtonTorikesi.Attributes("onclick") = tmpScript

    End Sub

#Region "ボタンイベント"

    ''' <summary>
    ''' 取消 ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTorikesi_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTorikesi.ServerClick

        Dim strScript As String = String.Empty
        Dim strFocusScript As String = String.Empty

        Dim objBtn As HtmlInputButton = CType(sender, HtmlInputButton)

        ' 画面の内容をDBに反映する
        If Me.SaveData(objBtn) Then '登録成功

            '登録完了後、画面をリロードするために、キー情報を引き渡す
            Context.Items("sendSearchTerms") = Me.HiddenKbn.Value & EarthConst.SEP_STRING & Me.HiddenBangou.Value

            '画面遷移（リロード）
            Server.Transfer(UrlConst.POPUP_BUKKEN_RIREKI)

        Else '登録失敗
            strScript = "alert('" & Messages.MSG019E.Replace("@PARAM1", "取消") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonUpdate_ServerClick2", strScript, True)
            Exit Sub
        End If

    End Sub

#End Region

#Region "プライベートメソッド"

    ''' <summary>
    ''' 画面の内容をDBに反映する
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function SaveData(ByVal objBtn As HtmlInputButton) As Boolean
        '*************************
        '物件履歴データを更新する
        '*************************
        Dim logic As New BukkenRirekiLogic
        Dim blnExe As Boolean = False

        '各行ごとに画面からレコードクラスに入れ込み
        Dim dataRec As BukkenRirekiRecord = Nothing

        dataRec = Me.GetRowCtrlToDataRec()
        If dataRec Is Nothing Then
            Return False
        End If

        ' データの更新を行います
        'ボタン押下別処理
        Select Case objBtn.ID
            Case Me.ButtonTorikesi.ID '取消
                'UPDATE
                blnExe = True
            Case Else '取消以外はエラー
                Return False
        End Select

        If logic.saveData(Me, dataRec, blnExe) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 物件履歴レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="br">物件履歴レコード</param>
    ''' <param name="intKengen">権限(有:1、無:0)</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal sender As Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal br As BukkenRirekiRecord _
                                    , Optional ByVal intKengen As Integer = 1 _
                                    )

        Dim helper As New DropDownHelper

        ' 種別コンボにデータをバインドする
        helper.SetMeisyouDropDownList(Me.SelectSyubetu, EarthConst.emMeisyouType.BUKKEN_RIREKI_SYUBETU)

        '******************************************
        '* 画面コントロールに設定
        '******************************************
        '(履歴)種別
        '存在チェック
        If cl.ChkDropDownList(Me.SelectSyubetu, cl.GetDispNum(br.RirekiSyubetu)) Then
            Me.SelectSyubetu.SelectedValue = cl.GetDisplayString(br.RirekiSyubetu, "")
            Me.SpanSyubetu.InnerHtml = Me.SelectSyubetu.SelectedItem.Text

            'コード(履歴NO)
            '存在チェック
            If cl.ChkDropDownList(Me.SelectSyubetu, cl.GetDispNum(br.RirekiSyubetu)) Then
                Me.HiddenBunrui.Value = cl.GetDisplayString(br.RirekiNo, "")
            End If

        Else
            Me.SpanSyubetu.InnerHtml = CTRL_VALUE_SPACE
            Me.SpanBunrui.InnerHtml = CTRL_VALUE_SPACE
        End If
        '取消
        If cl.GetDisplayString(br.Torikesi) = 0 Then
            Me.SpanTorikesi.InnerHtml = CTRL_VALUE_SPACE
        Else
            Me.SpanTorikesi.InnerHtml = EarthConst.TORIKESI
        End If
        '変更可否
        If cl.GetDisplayString(br.HenkouKahiFlg) = 0 Then
            Me.SpanHenkouFukaFlg.InnerHtml = CTRL_VALUE_SPACE
        Else
            Me.SpanHenkouFukaFlg.InnerHtml = EarthConst.HENKOU_FUKA
        End If
        '(汎用)日付
        Me.SpanHizuke.InnerHtml = cl.GetDisplayString(br.HanyouDate, CTRL_VALUE_SPACE)
        '汎用コード
        Me.SpanHanyouCode.InnerHtml = cl.GetDisplayString(br.HanyouCd, CTRL_VALUE_SPACE)
        '内容
        Me.SpanNaiyou.InnerHtml = cl.GetDisplayString(br.Naiyou, CTRL_VALUE_SPACE)

        '****************************
        '* Hidden項目
        '****************************
        '区分
        Me.HiddenKbn.Value = cl.GetDisplayString(br.Kbn)
        '番号
        Me.HiddenBangou.Value = cl.GetDisplayString(br.HosyousyoNo)
        '入力NO
        Me.HiddenNyuuryokuNo.Value = cl.GetDisplayString(br.NyuuryokuNo)
        '更新日時
        Me.HiddenUpdDatetime.Value = IIf(br.UpdDatetime = Date.MinValue, "", Format(br.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))

        '****************************
        '* Javascript
        '****************************
        Me.ButtonSyuusei.Attributes("onclick") = "PopupSyousai('" & Me.HiddenNyuuryokuNo.Value & "')"

        '明細行の表示設定
        Me.SetDispControl(br, intKengen)

    End Sub

    ''' <summary>
    ''' 画面の各明細行情報をレコードクラスに取得し、地盤レコードクラスのリストを返却する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetRowCtrlToDataRec() As BukkenRirekiRecord

        Dim intCnt As Integer = 0
        Dim ctrl As New BukkenRirekiRecordCtrl
        ' 画面内容より地盤レコードを生成する
        Dim dataRec As New BukkenRirekiRecord

        With ctrl

            '***************************************
            ' 物件履歴データ
            '***************************************
            ' 区分
            cl.SetDisplayString(Me.HiddenKbn.Value, dataRec.Kbn)
            ' 番号
            cl.SetDisplayString(Me.HiddenBangou.Value, dataRec.HosyousyoNo)
            ' 履歴種別
            cl.SetDisplayString(Me.SelectSyubetu.SelectedValue, dataRec.RirekiSyubetu)
            ' 履歴NO
            cl.SetDisplayString(Me.HiddenBunrui.Value, dataRec.RirekiNo)
            ' 入力NO
            cl.SetDisplayString(Me.HiddenNyuuryokuNo.Value, dataRec.NyuuryokuNo)
            ' 内容
            cl.SetDisplayString(Me.SpanNaiyou.InnerHtml, dataRec.Naiyou)
            ' (汎用)日付
            cl.SetDisplayString(Me.SpanHizuke.InnerHtml, dataRec.HanyouDate)
            ' 汎用コード
            cl.SetDisplayString(Me.SpanHanyouCode.InnerHtml, dataRec.HanyouCd)
            ' 更新者ユーザーID
            dataRec.UpdLoginUserId = Me.HiddenLoginUser.Value
            ' 更新日時 読み込み時のタイムスタンプ
            If Me.HiddenUpdDatetime.Value = "" Then
                dataRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
            Else
                dataRec.UpdDatetime = DateTime.ParseExact(Me.HiddenUpdDatetime.Value, EarthConst.FORMAT_DATE_TIME_1, Nothing)
            End If

        End With

        Return dataRec
    End Function

#Region "画面制御"

    ''' <summary>
    ''' 明細行の表示設定を行う。
    ''' </summary>
    ''' <param name="dataRec" >物件履歴レコード</param>
    ''' <param name="intKengen" >権限(有:1、無:0)</param>
    ''' <remarks>サーバー側では取消行は常に非表示。表示切替はJSにて制御</remarks>
    Public Sub SetDispControl(ByVal dataRec As BukkenRirekiRecord, ByVal intKengen As Integer)

        '取消行の場合、非表示
        If Me.SpanTorikesi.InnerHtml = EarthConst.TORIKESI Then
            Me.Tr1.Style("display") = "none"
            Me.Tr2.Style("display") = "none"
        Else
            Me.Tr1.Style("display") = "inline"
            Me.Tr2.Style("display") = "inline"
        End If

        If intKengen = 0 Then '権限なし時
            Me.ButtonSyuusei.Value = CTRL_VALUE_BUTTON_KAKUNIN '確認
            Me.ButtonTorikesi.Disabled = True '取消

        Else
            '登録日付
            Dim strTmpAddDate As String = IIf(dataRec.AddDatetime = Date.MinValue, String.Empty, Format(dataRec.AddDatetime, EarthConst.FORMAT_DATE_TIME_3))
            'システム日付
            Dim strNowDate As String = Format(Now.Date, EarthConst.FORMAT_DATE_TIME_3)

            '新規登録時
            If strTmpAddDate = String.Empty Then
                Me.ButtonSyuusei.Value = CTRL_VALUE_BUTTON_KAKUNIN '確認
                Me.ButtonTorikesi.Disabled = True '取消

            ElseIf strTmpAddDate = strNowDate Then '更新時：登録日付=システム日付

                '取消or変更不可
                If Me.SpanTorikesi.InnerHtml = EarthConst.TORIKESI Or Me.SpanHenkouFukaFlg.InnerHtml = EarthConst.HENKOU_FUKA Then
                    Me.ButtonSyuusei.Value = CTRL_VALUE_BUTTON_KAKUNIN '確認
                    Me.ButtonTorikesi.Disabled = True '取消

                Else
                    Me.ButtonSyuusei.Value = CTRL_VALUE_BUTTON_SYUUSEI '修正
                    Me.ButtonTorikesi.Disabled = False '取消

                End If

            Else '登録日付<>システム日付
                Me.ButtonSyuusei.Value = CTRL_VALUE_BUTTON_KAKUNIN '確認
                Me.ButtonTorikesi.Disabled = True '取消

            End If

        End If

    End Sub

#End Region

#End Region

End Class