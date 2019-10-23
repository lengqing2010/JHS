Partial Public Class SearchKousinRireki
    Inherits System.Web.UI.Page

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager
    Dim cookieKey As String = "earth_kensaku_checked"

    Dim cl As New CommonLogic
    Private DLogic As New DataLogic

    Dim MLogic As New MessageLogic
    Const pStrSpace As String = EarthConst.HANKAKU_SPACE

#Region "行コントロールID接頭語"
    Private Const CTRL_NAME_TR As String = "resultTr_"
    Private Const CTRL_NAME_HIDDEN_UNIQUE_NO As String = "HdnUniNo_"
#End Region


#Region "CSSクラス名"
    Private Const CSS_TEXT_CENTER = "textCenter"
    Private Const CSS_DATE = "date"
#End Region

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
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If IsPostBack = False Then

            '●パラメータのチェック
            If Context.Items("sendSearchTerms") IsNot Nothing Then
                arrSearchTerm = Split(Context.Items("sendSearchTerms"), EarthConst.SEP_STRING)
            Else '各業務画面からの呼出
                arrSearchTerm = Split(Request("sendSearchTerms"), EarthConst.SEP_STRING)
            End If

            If arrSearchTerm.Length >= 2 Then
                pStrKbn = arrSearchTerm(0)     '親画面からPOSTされた情報1 ：区分
                pStrBangou = arrSearchTerm(1)     '親画面からPOSTされた情報2 ：保証書NO
            End If

            '●アカウントマスタへの登録有無をチェック(無ければメインへ戻る)
            If userinfo.AccountNo = 0 _
                Then
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            ' 区分コンボにデータをバインドする
            Dim helper As New DropDownHelper
            Dim kbn_logic As New KubunLogic

            ' 区分コンボにデータをバインドする
            helper.SetDropDownList(SelectKubun, DropDownHelper.DropDownType.Kubun)
            ' 更新項目区分コンボにデータをバインドする
            helper.SetKtMeisyouDropDownList(Me.SelectKousinKoumoku, EarthConst.emKtMeisyouType.KOUSIN_KOUMOKU, True, False)

            '****************************************************************************
            ' 地盤データ取得
            '****************************************************************************
            Dim logic As New JibanLogic
            Dim jibanRec As New JibanRecordBase
            jibanRec = logic.GetJibanData(pStrKbn, pStrBangou)

            '地盤読み込みデータがセッションに存在する場合、画面に表示させる
            If logic.ExistsJibanData(pStrKbn, pStrBangou) And jibanRec IsNot Nothing Then
                Me.SetCtrlFromJibanRec(sender, e, jibanRec) '地盤データをコントロールにセット
            End If

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            setDispAction()
            SelectKubun.Focus()

            If SelectKubun.SelectedValue <> String.Empty And TextHosyousyoNo.Value <> String.Empty Then
                search_ServerClick(sender, e)
            Else
                Me.ButtonClose.Visible = False
            End If

        End If

    End Sub


    ''' <summary>
    ''' 地盤レコードから画面表示項目への値セットを行う（参照処理用）
    ''' </summary>
    ''' <param name="jr">地盤レコード</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByVal jr As JibanRecordBase)

        '画面コントロールに設定
        SelectKubun.SelectedValue = jr.Kbn

        Me.TextHosyousyoNo.Value = cl.GetDispStr(jr.HosyousyoNo) '番号

    End Sub


#Region "ボタンイベント"

    ''' <summary>
    ''' 検索処理の実行
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick

        Dim MyLogic As New KousinRirekiLogic
        Dim listResult As List(Of KousinRirekiDataRecord)

        ' 検索ボタンにフォーカス
        btnSearch.Focus()

        'Keyレコードクラス
        Dim recKey As New KousinRirekiDataKeyRecord

        'ボタン押下時点でのデフォルト起動画面選択状態をクッキーに保存
        Dim Cookie As HttpCookie = New HttpCookie(cookieKey)
        Cookie.Values.Add(chkHyoujiGamen1.ID, chkHyoujiGamen1.Checked.ToString())
        Cookie.Values.Add(chkHyoujiGamen2.ID, chkHyoujiGamen2.Checked.ToString())
        Cookie.Values.Add(chkHyoujiGamen3.ID, chkHyoujiGamen3.Checked.ToString())
        Cookie.Values.Add(chkHyoujiGamen4.ID, chkHyoujiGamen4.Checked.ToString())
        Cookie.Expires = DateTime.MaxValue ' 永続的保存クッキー
        Response.AppendCookie(Cookie)

        '●検索条件の取得
        Me.SetSearchKeyFromCtrl(recKey)

        '表示最大件数
        Dim end_count As Integer = IIf(maxSearchCount.Value <> "max", maxSearchCount.Value, EarthConst.MAX_RESULT_COUNT)
        Dim total_count As Integer = 0

        '●Key項目を元に、該当データをDBから抽出
        listResult = MyLogic.getSearchKeyDataList(sender, recKey, 1, end_count, total_count)

        '●画面にセット
        If total_count = 0 Then
            ' 検索結果ゼロ件の場合、メッセージを表示
            MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
        ElseIf total_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            Exit Sub
        End If

        '表示件数制限処理
        Dim displayCount As String = ""
        displayCount = total_count
        resultCount.Style.Remove("color")
        If maxSearchCount.Value <> "max" Then
            If Val(maxSearchCount.Value) < total_count Then
                resultCount.Style("color") = "red"
                displayCount = maxSearchCount.Value & " / " & CommonLogic.Instance.GetDisplayString(total_count)
            End If
        End If

        '検索結果件数を設定
        resultCount.InnerHtml = displayCount

        '●画面にセット
        Me.SetCtrlFromDataRec(listResult)

    End Sub

    ''' <summary>
    ''' 加盟店検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonkameitenSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kameitenSearch.ServerClick
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim dataArray As New List(Of KameitenSearchRecord)

        Dim total_count As Integer

        Dim tmpScript As String = String.Empty

        'If SelectKubun.SelectedValue = String.Empty Then
        '    '区分未選択の場合、エラー
        '    MLogic.AlertMessage(sender, Messages.MSG006E, 0, "error")
        '    masterAjaxSM.SetFocus(SelectKubun)
        '    Exit Sub
        'End If

        ' 取得件数を絞り込む場合、引数を追加してください
        If TextKameitenCd.Value <> "" Then
            dataArray = kameitenSearchLogic.GetKameitenSearchResult(SelectKubun.SelectedValue, _
                                                                    TextKameitenCd.Value, _
                                                                    False, _
                                                                    total_count)
        End If

        If dataArray.Count = 1 Then
            Dim recData As KameitenSearchRecord = dataArray(0)
            TextKameitenCd.Value = recData.KameitenCd
            kameitenNm.Value = recData.KameitenMei1
            Me.TextTorikesiRiyuu.Text = cl.getTorikesiRiyuu(recData.Torikesi, recData.KtTorikesiRiyuu)

            '加盟店コード/名称/取消理由の文字色スタイル
            cl.setStyleFontColor(Me.TextKameitenCd.Style, cl.getKameitenFontColor(recData.Torikesi))
            cl.setStyleFontColor(Me.kameitenNm.Style, cl.getKameitenFontColor(recData.Torikesi))
            cl.setStyleFontColor(Me.TextTorikesiRiyuu.Style, cl.getKameitenFontColor(recData.Torikesi))

            'フォーカスセット
            masterAjaxSM.SetFocus(kameitenSearch)
        Else
            'フォーカスセット
            Dim tmpFocusScript = "objEBI('" & kameitenSearch.ClientID & "').focus();"

            tmpScript = "callSearch('" & SelectKubun.ClientID & EarthConst.SEP_STRING & TextKameitenCd.ClientID & "','" & UrlConst.SEARCH_KAMEITEN & "','" & _
                            TextKameitenCd.ClientID & EarthConst.SEP_STRING & kameitenNm.ClientID & "','" & kameitenSearch.ClientID & "', 'SearchKameitenWindow');"

            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' 選択行ダブルクリック時の実行処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSend_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.ServerClick

    End Sub

#End Region


#Region "プライベートメソッド"

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 検索実行ボタン関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '検索実行ボタンのイベントハンドラを設定
        btnSearch.Attributes("onclick") = "checkJikkou();"

        'マスタ検索系コード入力項目イベントハンドラ設定
        TextKameitenCd.Attributes("onblur") = "checkNumber(this);clrKameitenInfo(this);"

        'クッキーからデフォルト起動画面選択状態を取得
        Dim Cookie As HttpCookie
        If Request.Cookies(cookieKey) IsNot Nothing Then
            Cookie = Request.Cookies(cookieKey)
            chkHyoujiGamen1.Checked = Cookie.Values(chkHyoujiGamen1.ID)
            chkHyoujiGamen2.Checked = Cookie.Values(chkHyoujiGamen2.ID)
            chkHyoujiGamen3.Checked = Cookie.Values(chkHyoujiGamen3.ID)
            chkHyoujiGamen4.Checked = Cookie.Values(chkHyoujiGamen4.ID)
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing)

    End Sub

    ''' <summary>
    ''' 画面項目から検索キーレコードへの値セットを行なう。
    ''' </summary>
    ''' <param name="recKey">抽出レコードクラスのキー</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromCtrl(ByRef recKey As KousinRirekiDataKeyRecord)

        '更新日 FROM
        recKey.KousinbiFrom = IIf(Me.TextKousinbiFrom.Value <> String.Empty, Me.TextKousinbiFrom.Value, DateTime.MinValue)
        '更新日 TO
        recKey.KousinbiTo = IIf(Me.TextKousinbiTo.Value <> String.Empty, Me.TextKousinbiTo.Value, DateTime.MinValue)
        '区分
        recKey.Kubun = IIf(Me.SelectKubun.SelectedValue <> String.Empty, Me.SelectKubun.SelectedValue, String.Empty)
        '保証書NO
        recKey.HosyousyoNo = IIf(Me.TextHosyousyoNo.Value <> String.Empty, Me.TextHosyousyoNo.Value, String.Empty)
        '更新項目
        recKey.KousinKoumoku = IIf(Me.SelectKousinKoumoku.SelectedItem.Text <> String.Empty, Me.SelectKousinKoumoku.SelectedItem.Text, String.Empty)
        '更新者
        recKey.Kousinsya = IIf(Me.TextKousinsya.Value <> String.Empty, Me.TextKousinsya.Value, String.Empty)

        '最新加盟店
        recKey.KameitenCd = IIf(Me.TextKameitenCd.Value <> String.Empty, Me.TextKameitenCd.Value, String.Empty)
        '最新加盟店カナ
        recKey.KameitenKana = IIf(Me.TextKameitenKana.Value <> String.Empty, Me.TextKameitenKana.Value, String.Empty)
        '更新前値
        recKey.KousinBeforeValue = IIf(Me.TextKousinBeforeValue.Value <> String.Empty, Me.TextKousinBeforeValue.Value, String.Empty)
        '更新後値
        recKey.KousinAfterValue = IIf(Me.TextKousinAfterValue.Value <> String.Empty, Me.TextKousinAfterValue.Value, String.Empty)

    End Sub

    ''' <summary>
    ''' 画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="listResult">KousinRirekiDataRecord</param>
    ''' <remarks></remarks>
    Private Sub SetCtrlFromDataRec(ByVal listResult As List(Of KousinRirekiDataRecord))
        '検索結果1件のみの場合の列ID格納用hiddenをクリア
        firstSend.Value = ""
        Dim lineCounter As Integer = 0

        '各セルの幅設定用のリスト作成（タイトル行の幅をベースにする）
        Dim widthList1 As New List(Of String)
        Dim tableWidth1 As Integer = 0

        '************************
        '* 画面テーブルへ出力
        '************************
        '行カウンタ
        Dim rowCnt As Integer = 0
        Dim strSpace As String = EarthConst.HANKAKU_SPACE
        Dim objTr1 As New HtmlTableRow


        '取得したデータを画面に表示
        For Each data As KousinRirekiDataRecord In listResult

            rowCnt += 1
            lineCounter += 1

            'Tr
            objTr1 = New HtmlTableRow

            Dim objTdKousinNitiji As New HtmlTableCell      '更新日時
            Dim objTdKubun As New HtmlTableCell             '区分
            Dim objTdHosyousyoNo As New HtmlTableCell       '保証書NO
            Dim objTdKousinKoumoku As New HtmlTableCell     '更新項目
            Dim objTdKousinPreVal As New HtmlTableCell      '更新前値
            Dim objTdKousinPostVal As New HtmlTableCell     '更新後値
            Dim objTdKousinsya As New HtmlTableCell         '更新者

            Dim objIptRtnHiddn1 As New HtmlInputHidden

            Dim objTdOtherWin As New HtmlTableCell
            Dim objAncOtherWin1 As New HtmlAnchor
            Dim objAncOtherWin2 As New HtmlAnchor
            Dim objAncOtherWin3 As New HtmlAnchor
            Dim objAncOtherWin4 As New HtmlAnchor
            Dim objSpace1 As New HtmlGenericControl
            Dim objSpace2 As New HtmlGenericControl
            Dim objSpace3 As New HtmlGenericControl

            Dim strKtInfoColor As String = EarthConst.STYLE_COLOR_BLACK

            '値の設定
            objTdKousinNitiji.InnerHtml = DLogic.dtTime2Str(data.KousinNitiji, EarthConst.FORMAT_DATE_TIME_7)
            objTdKubun.InnerHtml = data.Kubun
            objTdHosyousyoNo.InnerHtml = data.HosyousyoNo
            objTdKousinKoumoku.InnerHtml = data.KousinKoumoku
            objTdKousinPreVal.InnerHtml = cl.GetDisplayString(data.KousinPreValue, EarthConst.HANKAKU_SPACE)
            objTdKousinPostVal.InnerHtml = cl.GetDisplayString(data.KousinPostValue, EarthConst.HANKAKU_SPACE)
            objTdKousinsya.InnerHtml = cl.GetDisplayString(data.Kousinsya, EarthConst.HANKAKU_SPACE)

            objIptRtnHiddn1.ID = "returnHidden" & lineCounter
            objIptRtnHiddn1.Value = data.Kubun & EarthConst.SEP_STRING & data.HosyousyoNo

            objSpace1.InnerHtml = pStrSpace & pStrSpace
            objSpace2.InnerHtml = pStrSpace & pStrSpace
            objSpace3.InnerHtml = pStrSpace & pStrSpace

            objAncOtherWin1.InnerText = "受"
            objAncOtherWin1.HRef = "javascript:void(0)"
            objAncOtherWin1.Attributes("onclick") = "returnSelectValueOtherWin(this,1);"
            objAncOtherWin1.Attributes("tabindex") = "-1"
            objAncOtherWin2.InnerText = "報"
            objAncOtherWin2.HRef = "javascript:void(0)"
            objAncOtherWin2.Attributes("onclick") = "returnSelectValueOtherWin(this,2);"
            objAncOtherWin2.Attributes("tabindex") = "-1"
            objAncOtherWin3.InnerText = "工"
            objAncOtherWin3.HRef = "javascript:void(0)"
            objAncOtherWin3.Attributes("onclick") = "returnSelectValueOtherWin(this,3);"
            objAncOtherWin3.Attributes("tabindex") = "-1"
            objAncOtherWin4.InnerText = "保"
            objAncOtherWin4.HRef = "javascript:void(0)"
            objAncOtherWin4.Attributes("onclick") = "returnSelectValueOtherWin(this,4);"
            objAncOtherWin4.Attributes("tabindex") = "-1"

            objTdOtherWin.Style("text-align") = "center"

            With objTdOtherWin.Controls
                .Add(objIptRtnHiddn1)  '戻り要素(hidden)は、必ず先頭列の先頭のInputタグにセットすること
                .Add(objAncOtherWin1)
                .Add(objSpace1)
                .Add(objAncOtherWin2)
                .Add(objSpace2)
                .Add(objAncOtherWin3)
                .Add(objSpace3)
                .Add(objAncOtherWin4)
            End With

            'スタイル、クラス設定
            objTdKousinNitiji.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdKubun.Attributes("class") = CSS_TEXT_CENTER
            objTdHosyousyoNo.Attributes("class") = CSS_TEXT_CENTER

            '行IDとJSイベントの付与
            objTr1.ID = CTRL_NAME_TR & rowCnt
            If rowCnt = 1 Then
                objTr1.Attributes("tabindex") = 0
                objTr1.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                objTr1.Attributes("tabindex") = -1
            End If

            '行にセルをセット1
            With objTr1.Controls
                .Add(objTdOtherWin)
                .Add(objTdKousinNitiji)
                .Add(objTdKubun)
                .Add(objTdHosyousyoNo)
                .Add(objTdKousinKoumoku)
                .Add(objTdKousinPreVal)
                .Add(objTdKousinPostVal)
                .Add(objTdKousinsya)
            End With

            'テーブルに行をセット
            Me.searchGrid.Controls.Add(objTr1)

            If listResult.Count = 1 Then
                '検索結果1件のみの場合の列ID格納用hiddenに値をセット
                firstSend.Value = objTr1.ClientID
            End If

        Next

    End Sub
#End Region

End Class