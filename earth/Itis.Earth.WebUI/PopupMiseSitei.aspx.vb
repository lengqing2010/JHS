
Partial Public Class PopupMiseSitei
    Inherits System.Web.UI.Page

    Dim masterAjaxSM As New ScriptManager

    Dim JibanLogic As New JibanLogic
    Dim KidouType As String = String.Empty
    Dim Kbn As String = String.Empty
    Dim IsFc As String = String.Empty
    Dim TenMd As String = String.Empty

    Dim cl As New CommonLogic

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        masterAjaxSM = AjaxScriptManager1

        'リクエスト値格納
        Kbn = Request("kbn")
        IsFc = Request("isfc")
        TenMd = Request("tenmd")
        HiddenKidouType.Value = Request("type")
        KidouType = HiddenKidouType.Value

        If IsPostBack = False Then

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            ' 区分コンボにデータをバインドする
            Dim helper As New DropDownHelper
            Dim kbn_logic As New KubunLogic
            helper.SetDropDownList(Me.SelectKubun, DropDownHelper.DropDownType.Kubun)

            '画面表示設定
            SetDisplay(sender, e)

        Else
            KidouType = HiddenKidouType.Value

        End If
    End Sub

    ''' <summary>
    ''' 画面表示設定
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub SetDisplay(ByVal sender As Object, ByVal e As System.EventArgs)

        'コントロール共通設定
        TextKameitenCd.Attributes("onblur") = "checkNumber(this);"
        TextKameitenCd.Attributes("onchange") = "clrKameitenInfo(this,objEBI('" & ButtonKameitenKennsaku.ClientID & "'),objEBI('" & HiddenClearFlg.ClientID & "'));"
        TextEigyousyoCd.Attributes("onchange") = "clrName(this,objEBI('" & ButtonEigyousyoSearch.ClientID & "'),objEBI('" & HiddenClearFlg.ClientID & "'));"
        RadioKameitenSitei.Attributes("onclick") = "objEBI('" & ButtonChangeSitei.ClientID & "').click();"
        RadioEigyousyoSitei.Attributes("onclick") = "objEBI('" & ButtonChangeSitei.ClientID & "').click();"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)

        'リクエストから情報取得
        If IsFc <> String.Empty Then
            If IsFc = "0" Then
                RadioKameitenSitei.Checked = True
                RadioEigyousyoSitei.Checked = False
                SelectKubun.SelectedValue = Request("kbn")
                SetFocus(TextKameitenCd)
            Else
                RadioKameitenSitei.Checked = False
                RadioEigyousyoSitei.Checked = True
                SetFocus(TextEigyousyoCd)
            End If
        End If

        '画面コメント表示切替
        SpanPopupMess1.InnerText = String.Empty
        SpanPopupMess2.InnerText = "加盟店または営業所を指定してください。"

        Select Case KidouType
            Case "tenbetu"
                RadioKameitenSitei.Checked = True
                SelectKubun.SelectedValue = "A"
                SetFocus(TextKameitenCd)
            Case "tenbetuA"
                SelectKubun.SelectedValue = Kbn
                SpanPopupMess1.InnerText = "登録/修正処理が完了しました。"
            Case "hansoku"
                RadioKameitenSitei.Checked = True
                SelectKubun.SelectedValue = "S"
                SetFocus(TextKameitenCd)
            Case "hansokuA"
                SelectKubun.SelectedValue = Kbn
                SpanPopupMess1.InnerText = "登録/修正処理が完了しました。"
        End Select

        ButtonChangeSitei_ServerClick(sender, e)

    End Sub

    ''' <summary>
    ''' 加盟店検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonKameitenKennsaku_ServerClick1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKameitenKennsaku.ServerClick
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim dataArray As New List(Of KameitenSearchRecord)
        Dim total_count As Integer = 5
        Dim tmpScript As String = String.Empty

        '加盟店コード=未入力でかつクリアフラグがたっている場合
        If Trim(TextKameitenCd.Text) = String.Empty And HiddenClearFlg.Value = "1" Then
            '加盟店情報初期化
            Me.clrKameitenInfo()

            setFocusAJ(ButtonKameitenKennsaku)
            Exit Sub
        End If

        '区分未選択の場合、エラー
        If SelectKubun.SelectedValue = String.Empty Then
            tmpScript = "alert('" & Messages.MSG006E & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "kbnerr", tmpScript, True)
            setFocusAJ(SelectKubun)
            Exit Sub
        End If

        ' 取得件数を絞り込む場合、引数を追加してください
        If TextKameitenCd.Text <> "" Then
            dataArray = kameitenSearchLogic.GetKameitenSearchResult(SelectKubun.SelectedValue, _
                                                                    TextKameitenCd.Text, _
                                                                    True, _
                                                                    total_count)
        End If

        If dataArray.Count = 1 Then
            '商品情報を画面にセット
            Dim recData As KameitenSearchRecord = dataArray(0)
            '加盟店コードを入れ直す
            Me.TextKameitenCd.Text = dataArray(0).KameitenCd

            '加盟店情報設定処理
            kameitenSearchAfter(sender, e)

        Else
            TextKameitenMei.Text = String.Empty
            TextKeiretu.Text = String.Empty
            TextKameiEigyousyoMei.Text = String.Empty

            '検索画面表示用JavaScript『callSearch』を実行
            tmpScript = "callSearch('" & SelectKubun.ClientID & EarthConst.SEP_STRING & TextKameitenCd.ClientID & "','" & UrlConst.SEARCH_KAMEITEN & "','" & _
                            TextKameitenCd.ClientID & EarthConst.SEP_STRING & TextKameitenMei.ClientID & "','" & ButtonKameitenKennsaku.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            Exit Sub
        End If

        'フォーカスの設定
        Me.setFocusAJ(Me.ButtonKameitenKennsaku)

    End Sub

    ''' <summary>
    ''' 加盟店検索実行後処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub kameitenSearchAfter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '加盟店検索実行後処理(加盟店詳細情報、ビルダー情報取得)
        Dim logic As New KameitenSearchLogic

        Dim record As KameitenSearchRecord = logic.GetKameitenSearchResult(SelectKubun.SelectedValue, TextKameitenCd.Text, "", True)

        If Not record Is Nothing Then
            Me.TextKameitenCd.Text = cl.GetDisplayString(record.KameitenCd)
            Me.TextKameitenMei.Text = cl.GetDisplayString(record.KameitenMei1)
            Me.TextTorikesiRiyuu.Text = cl.GetDisplayString(cl.getTorikesiRiyuu(record.Torikesi, record.KtTorikesiRiyuu)) '加盟店取消理由
            Me.TextKeiretu.Text = cl.GetDisplayString(record.KeiretuMei)
            Me.TextKameiEigyousyoMei.Text = cl.GetDisplayString(record.EigyousyoMei)

            '加盟店コードを退避
            Me.HiddenKameitenCdOld.Value = Me.TextKameitenCd.Text

            '加盟店コード/名称/取消理由の文字色スタイル
            cl.setStyleFontColor(Me.TextKameitenCd.Style, cl.getKameitenFontColor(record.Torikesi))
            cl.setStyleFontColor(Me.TextKameitenMei.Style, cl.getKameitenFontColor(record.Torikesi))
            cl.setStyleFontColor(Me.TextTorikesiRiyuu.Style, cl.getKameitenFontColor(record.Torikesi))
            cl.setStyleFontColor(Me.TextKeiretu.Style, cl.getKameitenFontColor(record.Torikesi))
            cl.setStyleFontColor(Me.TextKameiEigyousyoMei.Style, cl.getKameitenFontColor(record.Torikesi))
        Else
            '加盟店情報初期化
            Me.clrKameitenInfo()

        End If

    End Sub

    ''' <summary>
    ''' 加盟店関連情報をクリアし、文字スタイルを標準(黒)に戻す
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub clrKameitenInfo(Optional ByVal blnKameitenClear As Boolean = True)
        '加盟店情報をクリアする場合
        If blnKameitenClear Then
            '加盟店コード
            Me.TextKameitenCd.Text = String.Empty
            '加盟店名
            Me.TextKameitenMei.Text = String.Empty
            '加盟店取消理由
            Me.TextTorikesiRiyuu.Text = cl.getTorikesiRiyuu(0, String.Empty)
            '系列名
            TextKeiretu.Text = String.Empty
            '営業所名
            TextKameiEigyousyoMei.Text = String.Empty

            HiddenKameitenCdOld.Value = String.Empty
        End If

        HiddenClearFlg.Value = String.Empty

        '加盟店コード/名称/取消理由の文字色スタイル
        cl.setStyleFontColor(Me.TextKameitenCd.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextKameitenMei.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextTorikesiRiyuu.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextKeiretu.Style, cl.getKameitenFontColor(0))
        cl.setStyleFontColor(Me.TextKameiEigyousyoMei.Style, cl.getKameitenFontColor(0))

    End Sub

    ''' <summary>
    ''' 営業所検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonEigyousyoSearch_ServerClick1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEigyousyoSearch.ServerClick
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim blnResult As Boolean

        If Trim(TextEigyousyoCd.Text) = String.Empty And HiddenClearFlg.Value = "1" Then
            TextEigyousyoMei.Text = String.Empty
            HiddenClearFlg.Value = String.Empty
            HiddenEigyousyoCdOld.Value = String.Empty
            setFocusAJ(ButtonEigyousyoSearch)
            Exit Sub
        End If

        ' 営業所マスタを検索
        blnResult = cl.CallEigyousyoSearchWindow(sender _
                                                 , e _
                                                 , Me _
                                                 , Me.TextEigyousyoCd _
                                                 , Me.TextEigyousyoMei _
                                                 , Me.ButtonEigyousyoSearch _
                                                 , True _
                                                 )

        If blnResult Then
            'フォーカスセット
            setFocusAJ(Me.ButtonEigyousyoSearch)
            'Hiddenに退避
            HiddenEigyousyoCdOld.Value = TextEigyousyoCd.Text
        End If

    End Sub

    ''' <summary>
    ''' Ajax動作時のフォーカスセット
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Private Sub setFocusAJ(ByVal ctrl As Control)

        masterAjaxSM.SetFocus(ctrl)

    End Sub

    ''' <summary>
    ''' 指定ラジオ選択変更
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonChangeSitei_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '加盟店指定ラジオボタン
        Dim booleanSitei As Boolean = RadioKameitenSitei.Checked

        SelectKubun.Enabled = booleanSitei
        TextKameitenCd.Enabled = booleanSitei
        ButtonKameitenKennsaku.Disabled = (booleanSitei = False)
        TextEigyousyoCd.Enabled = (booleanSitei = False)
        ButtonEigyousyoSearch.Disabled = booleanSitei

        'チェック状態
        If booleanSitei Then
            '営業所情報をクリア
            TextEigyousyoCd.Text = String.Empty
            HiddenEigyousyoCdOld.Value = String.Empty
            TextEigyousyoMei.Text = String.Empty

            Me.clrKameitenInfo(False)

            setFocusAJ(TextKameitenCd)
        Else
            '加盟店情報をクリア
            Me.clrKameitenInfo()

            setFocusAJ(TextEigyousyoCd)
        End If

    End Sub
End Class