
Partial Public Class SearchHinsituHosyousyoJyoukyou
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' リスト
    ''' </summary>
    ''' <remarks></remarks>
    Private pList As New List(Of HinsituHosyousyoJyoukyouSearchRecord)

    Dim userInfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cookieKey As String = "earth_kensaku_checked"
    'UtilitiesのMessegeLogicクラス
    Dim mLogic As New MessageLogic
    Dim cl As New CommonLogic
    Dim cbLogic As New CommonBizLogic
    Dim kameitenlogic As New KameitenSearchLogic
    '検索用レコードクラス
    Private rec As New HinsituHosyousyoJyoukyouRecord

    Const pStrSpace As String = EarthConst.HANKAKU_SPACE

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ユーザー基本認証
        jBn.UserAuth(userInfo)

        '認証結果によって画面表示を切替える
        If userInfo IsNot Nothing Then

        Else
            Response.Redirect(UrlConst.MAIN)
        End If

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '一括変更画面起動ボタン設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        If userInfo.IraiGyoumuKengen = -1 Or _
           userInfo.HoukokusyoGyoumuKengen = -1 Or _
           userInfo.KoujiGyoumuKengen = -1 Or _
           userInfo.HosyouGyoumuKengen = -1 Or _
           userInfo.KekkaGyoumuKengen = -1 Then
        End If
        If userInfo.IraiGyoumuKengen = -1 Then
        End If
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '選択物件一括受付ボタン設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '一括変更画面起動上限物件数セット(Web.configから取得)
        HiddenIkkatuUketukeMax.Value = EarthConst.MAX_IKKATU_UKETUKE

        '初期状態ではボタンはonclickイベントハンドラはクリア
        ButtonIkkatuUketuke.Attributes.Remove("onclick")

        ' アカウントマスタの保証業務権限ありの場合のみ、【選択物件一括受付】ボタンを活性
        If userInfo.HosyouGyoumuKengen <> -1 Then
            ButtonIkkatuUketuke.Disabled = True
        End If

        '各テーブルの表示状態を切り替える
        Me.kensakuInfo.Style("display") = Me.HiddenKensakuInfoStyle.Value

        If IsPostBack = False Then

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            ' 区分コンボにデータをバインドする
            Dim helper As New DropDownHelper
            helper.SetDropDownList(cmbKubun_1, DropDownHelper.DropDownType.Kubun)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            setDispAction()

            'フォーカス設定
            cmbKubun_1.Focus()

        Else

        End If

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()
        Dim today As Date = DateAndTime.Today   '本日日付オブジェクト

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 検索実行ボタン関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '検索実行ボタンのイベントハンドラを設定
        btnSearch.Attributes("onclick") = "checkJikkou(0);"
        'CSV出力ボタンのイベントハンドラを設定
        Me.ButtonCsv.Attributes("onclick") = "checkJikkou('1');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 区分、全区分関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '区分プルダウン、全区分チェックボックスのイベントハンドラを設定
        cmbKubun_1.Attributes("onchange") = "setKubunVal();"
        kubun_all.Attributes("onclick") = "setKubunVal();"
        ' 画面起動時デフォルトは「全区分」にチェック
        If IsPostBack = False Then
            kubun_all.Checked = True
        End If

        'マスタ検索系コード入力項目イベントハンドラ設定
        kameitenCd.Attributes("onblur") = "checkNumber(this);clrKameitenInfo(this);"
        tyousakaisyaCd.Attributes("onblur") = "checkNumber(this);clrName(this,'" & tyousakaisyaNm.ClientID & "');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '+ 機能別テーブルの表示切替
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '検索条件
        Me.AKensakuInfo.HRef = "JavaScript:changeDisplay('" & Me.kensakuInfo.ClientID & "');SetDisplayStyle('" & HiddenKensakuInfoStyle.ClientID & "','" & Me.kensakuInfo.ClientID & "');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing)

    End Sub

    ''' <summary>
    ''' 検索実行時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick
        '後処理用の変数セット
        HiddenHaitaDate.Value = Format(DateTime.Now, EarthConst.FORMAT_DATE_TIME_2)

        ' 検索ボタンにフォーカス
        btnSearch.Focus()

        '検索条件を設定
        SetSearchKeyFromCtrl(rec)

        '検索結果を画面に表示
        SetSearchResult(sender, e)

    End Sub

    ''' <summary>
    ''' 選択行ダブルクリック時の実行処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSend_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.ServerClick
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        '検索画面表示用JavaScript『callSearch』を実行
    End Sub

    ''' <summary>
    ''' 加盟店検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub kameitenSearch_ServerClick1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kameitenSearch.ServerClick
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim kameitenSearchLogic As New KameitenSearchLogic
        Dim dataArray As New List(Of KameitenSearchRecord)

        Dim kameiten_count As Integer

        Dim tmpScript As String = String.Empty

        ' 取得件数を絞り込む場合、引数を追加してください
        If kameitenCd.Value <> "" Then
            dataArray = kameitenSearchLogic.GetKameitenSearchResult(kubunVal.Value, _
                                                                    kameitenCd.Value, _
                                                                    False, _
                                                                    kameiten_count)
        End If

        If dataArray.Count = 1 Then
            Dim recData As KameitenSearchRecord = dataArray(0)
            kameitenCd.Value = recData.KameitenCd
            kameitenNm.Value = recData.KameitenMei1
            Me.TextTorikesiRiyuu.Text = cl.getTorikesiRiyuu(recData.Torikesi, recData.KtTorikesiRiyuu)

            '加盟店コード/名称/取消理由の文字色スタイル
            cl.setStyleFontColor(Me.kameitenCd.Style, cl.getKameitenFontColor(recData.Torikesi))
            cl.setStyleFontColor(Me.kameitenNm.Style, cl.getKameitenFontColor(recData.Torikesi))
            cl.setStyleFontColor(Me.TextTorikesiRiyuu.Style, cl.getKameitenFontColor(recData.Torikesi))

            'フォーカスセット
            masterAjaxSM.SetFocus(kameitenSearch)
        Else
            'フォーカスセット
            Dim tmpFocusScript = "objEBI('" & kameitenSearch.ClientID & "').focus();"
            '検索画面表示用JavaScript『callSearch』を実行
            tmpScript = "callSearch('" & kubunVal.ClientID & EarthConst.SEP_STRING & kameitenCd.ClientID & "','" & UrlConst.SEARCH_KAMEITEN & "','" & _
                            kameitenCd.ClientID & EarthConst.SEP_STRING & kameitenNm.ClientID & "','" & kameitenSearch.ClientID & "');"

            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' CSV出力ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHiddenCsv_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenCsv.ServerClick
        Me.HiddenCsvOutPut.Value = String.Empty 'フラグをクリア

        Dim strFileNm As String = String.Empty  '出力ファイル名
        Dim dtCsv As DataTable
        Dim myLogic As New JibanLogic

        '検索条件の設定
        Me.SetSearchKeyFromCtrl(rec)

        '件数
        Dim csv_count As Integer = 0

        '検索実行
        dtCsv = myLogic.GetJibanSearchHinsituRecordCsv(sender, rec, csv_count)

        ' 検索結果ゼロ件の場合、メッセージを表示
        If csv_count = 0 Then
            ' 検索結果ゼロ件の場合、メッセージを表示
            mLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
            Exit Sub
        ElseIf csv_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            mLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV出力"), 0, "kensakuErr")
            Exit Sub
        End If

        '出力用データテーブルを基に、CSV出力を行なう
        If cl.OutPutFileFromDtTable(EarthConst.FILE_NAME_CSV_HINSITU_HOSYOUSYO_JYOUKYOU_DATA, dtCsv) = False Then
            ' 出力用文字列がないので、処理終了
            mLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV出力"), 0, "OutputErr")
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 選択物件一括受付ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ikkatuUketuke_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ikkatuUketuke.ServerClick

        Dim lstChkOk As New List(Of HinsituHosyousyoJyoukyouSearchRecord)
        Dim lstJibanRec As New List(Of JibanRecordHosyou)

        'チェック付レコードの取得
        GetFromJibanRec(sender, e, pList)

        'チェック＆OKレコードをリスト格納
        checkInput(pList, lstChkOk)

        '保証書レコードに格納
        If SetRecToRec(lstChkOk, lstJibanRec) = False Then Exit Sub

        If SaveData(lstJibanRec) Then
            '登録成功

            '検索条件を設定
            SetSearchKeyFromHidden(rec)

            '検索結果を画面に表示
            SetSearchResult(sender, e)

            ' チェック用の値クリア
            HiddenIkkatuSyoriZumiKbn.Value = ""
            HiddenIkkatuSyoriZumiNo.Value = ""

        End If
    End Sub

    ''' <summary>
    ''' チェック付きレコードリストを取得する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="pList">List(Of HinsituHosyousyoJyoukyouSearchRecord)</param>
    ''' <remarks></remarks>
    Public Sub GetFromJibanRec(ByVal sender As Object, ByVal e As System.EventArgs, ByRef pList As List(Of HinsituHosyousyoJyoukyouSearchRecord))
        Dim jSM As New JibanSessionManager 'セッション管理クラス
        Dim jBn As New Jiban '地盤画面クラス

        Dim arrKbn() As String = Split(Me.HiddenSendKbn.Value, EarthConst.SEP_STRING)
        Dim arrNo() As String = Split(Me.HiddenSendHosyousyoNo.Value, EarthConst.SEP_STRING)

        Dim end_count As Integer = IIf(maxSearchCount.Value <> "max", maxSearchCount.Value, EarthConst.MAX_RESULT_COUNT)
        Dim logic As New JibanLogic
        Dim checked_count As Integer = 0

        Dim jibanRec As New JibanRecordBase
        Dim intCnt As Integer  'カウンタ

        '区分と番号のパラメータ引渡し数が異なる場合
        If arrKbn.Length <> arrNo.Length Then
            Exit Sub
        End If

        For intCnt = 0 To arrKbn.Length - 1

            Dim recBukkenInfo As New HinsituHosyousyoJyoukyouSearchRecord

            '最後のレコードはよける
            If intCnt = arrKbn.Length - 1 Then
                Exit For
            End If

            '検索条件の設定
            rec.Kbn1 = arrKbn(intCnt)
            rec.HosyousyoNoFrom = arrNo(intCnt)
            rec.HosyousyoNoTo = arrNo(intCnt)
            rec.DataHakiSyubetu = Integer.MinValue

            '検索
            recBukkenInfo = logic.GetJibanSearchIkkatuHinsituRecord(sender, rec, 1, end_count, checked_count)

            plist.Add(recBukkenInfo)
        Next

        '該当データが一件もない場合()
        If pList.Count = 0 Then
            Exit Sub
        End If


    End Sub

    ''' <summary>
    ''' 調査会社検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tyousakaisyaSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tyousakaisyaSearch.ServerClick
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        Dim tmpScript As String = String.Empty

        If tyousakaisyaCd.Value <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetTyousakaisyaSearchResult(tyousakaisyaCd.Value, "", "", "", False)
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            tyousakaisyaCd.Value = recData.TysKaisyaCd + recData.JigyousyoCd
            tyousakaisyaNm.Value = recData.TysKaisyaMei

            'フォーカスセット
            masterAjaxSM.SetFocus(tyousakaisyaSearch)
        Else
            '検索画面表示用JavaScript『callSearch』を実行
            tmpScript = "callSearch('" & tyousakaisyaCd.ClientID & "','" & UrlConst.SEARCH_TYOUSAKAISYA & "','" & _
                            tyousakaisyaCd.ClientID & EarthConst.SEP_STRING & tyousakaisyaNm.ClientID & "','" & tyousakaisyaSearch.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If
    End Sub

    ''' <summary>
    ''' 検索結果行の生成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub createDataTable(ByVal list As List(Of HinsituHosyousyoJyoukyouSearchRecord))

        '検索結果1件のみの場合の列ID格納用hiddenをクリア
        firstSend.Value = ""

        Dim lineCounter As Integer = 0
        Dim com As CommonLogic = CommonLogic.Instance

        '各セルの幅設定用のリスト作成（タイトル行の幅をベースにする）
        Dim widthList1 As New List(Of String)
        Dim tableWidth1 As Integer = 0

        '処理済レコードの区分・番号の取得
        Dim arrKbn() As String = Split(Me.HiddenIkkatuSyoriZumiKbn.Value, EarthConst.SEP_STRING)
        Dim arrNo() As String = Split(Me.HiddenIkkatuSyoriZumiNo.Value, EarthConst.SEP_STRING)
        Dim intCnt As Integer  'カウンタ

        For Each cell As HtmlTableCell In TableTitleTable1.Rows(0).Cells
            Dim tmpWidth = cell.Style("width")
            widthList1.Add(tmpWidth)
            tableWidth1 += Integer.Parse(tmpWidth.Replace("px", "")) + 3
        Next
        TableTitleTable1.Style("width") = tableWidth1 & "px"
        TableDataTable1.Style("width") = tableWidth1 & "px"

        Dim widthList2 As New List(Of String)
        Dim tableWidth2 As Integer = 0

        For Each cell As HtmlTableCell In TableTitleTable2.Rows(0).Cells
            Dim tmpWidth = cell.Style("width")
            widthList2.Add(tmpWidth)
            tableWidth2 += Integer.Parse(tmpWidth.Replace("px", "")) + 3
        Next
        TableTitleTable2.Style("width") = tableWidth2 & "px"
        TableDataTable2.Style("width") = tableWidth2 & "px"

        '選択物件一括受付ボタンのDisabled状態を確認
        Dim flgIkkatuOk As Boolean = False
        Dim flgIkkatuUketukeOk As Boolean = False
        If ButtonIkkatuUketuke.Disabled = False Then
            '選択物件一括受付ボタンの何れかが有効な場合
            If list.Count > 0 Then
                '検索結果がある場合、選択物件一括受付ボタンをセット＆表示
                flgIkkatuOk = True
                flgIkkatuUketukeOk = True
                ButtonIkkatuUketuke.Visible = True
                ButtonIkkatuUketuke.Attributes("onclick") = "checkIkkatuUketuke()"
            End If
        End If

        For Each data As HinsituHosyousyoJyoukyouSearchRecord In list
            lineCounter += 1

            Dim objTr1 As New HtmlTableRow
            Dim objTr2 As New HtmlTableRow
            Dim objImgOtherWin As New HtmlImage
            
            Dim objIptRtnHiddn1 As New HtmlInputHidden
            Dim objIptRtnHiddn2 As New HtmlInputHidden
            Dim objTdIkkatu As New HtmlTableCell            '一括チェックボックス
            Dim objCheckIkkatu As New HtmlInputCheckBox
            Dim objTdHaki As New HtmlTableCell              '破棄種別
            Dim objTdKubun As New HtmlTableCell             '区分
            Dim objTdBangou As New HtmlTableCell            '番号
            Dim objTdSeshuNm As New HtmlTableCell           '施主名
            Dim objTdJyuusho1 As New HtmlTableCell          '物件住所
            Dim objTdKameitenCd As New HtmlTableCell        '加盟店コード
            Dim objTdKameitenNm As New HtmlTableCell        '加盟店名
            Dim objTdKtTorikesi As New HtmlTableCell        '加盟店取消
            Dim strKtInfoColor As String = EarthConst.STYLE_COLOR_BLACK

            Dim objTdHakIraiTime As New HtmlTableCell                     '依頼日時
            Dim objTdHosyousyoHakIraisyoTykDate As New HtmlTableCell      '依頼書着日
            Dim objTdHosyousyoHakDate As New HtmlTableCell                '発行日
            Dim objTdHosyouKaisiDate As New HtmlTableCell                 '保証開始日
            Dim objTdKsSiyou As New HtmlTableCell                         '判定
            Dim objTdKojHkksJuriDate As New HtmlTableCell                 '工報受理日
            Dim objTdHosyouNasiRiyuu As New HtmlTableCell                 '保証なし理由
            Dim objTdDisplayName As New HtmlTableCell                     '営業担当者
            Dim objTdHosyousyoHakUmu As New HtmlTableCell                 '発行タイミング/初回発行方法
            Dim objTdHosyouKikanMK As New HtmlTableCell                   '加/保証期間
            Dim objTdHosyouKikanTJ As New HtmlTableCell                   '物/保証期間
            Dim objTdIraiDate As New HtmlTableCell                        '物件依頼日
            Dim objTdBikou As New HtmlTableCell                           '備考

            Dim objSpace1 As New HtmlGenericControl
            Dim objSpace2 As New HtmlGenericControl
            Dim objSpace3 As New HtmlGenericControl

            objSpace1.InnerHtml = pStrSpace & pStrSpace
            objSpace2.InnerHtml = pStrSpace & pStrSpace
            objSpace3.InnerHtml = pStrSpace & pStrSpace

            objIptRtnHiddn1.ID = "returnHidden" & lineCounter
            objIptRtnHiddn1.Value = data.Kbn & EarthConst.SEP_STRING & data.HosyousyoNo

            With objTdIkkatu.Controls
                .Add(objIptRtnHiddn1)  '戻り要素(hidden)は、必ず先頭列の先頭のInputタグにセットすること
            End With

            objTdIkkatu.Attributes("onclick") = "this.firstChild.click();"  'TDクリック時にもチェック操作を行う
            If (data.DataHakiSyubetu <> String.Empty) AndAlso (data.DataHakiSyubetu <> "0") Then
                '破棄済みの物件はチェックボックス非表示
                objCheckIkkatu.Visible = False
                objTdIkkatu.InnerHtml = pStrSpace
                With objTdIkkatu.Controls
                    .Add(objIptRtnHiddn1)  '戻り要素(hidden)は、必ず先頭列の先頭のInputタグにセットすること
                End With
                objTdIkkatu.Attributes.Remove("onclick")
            End If
            objCheckIkkatu.ID = "CheckIkkatu" & lineCounter
            objCheckIkkatu.Value = objIptRtnHiddn1.Value
            objTdIkkatu.Controls.Add(objCheckIkkatu)    '一括チェックボックス
            objTdHaki.InnerHtml = com.GetDisplayString(data.DataHakiSyubetu, pStrSpace)  '破棄種別
            objTdKubun.InnerHtml = com.GetDisplayString(data.Kbn, pStrSpace)             '区分
            objTdBangou.InnerHtml = com.GetDisplayString(data.HosyousyoNo, pStrSpace)    '番号
            objTdSeshuNm.InnerHtml = com.GetDisplayString(data.SesyuMei, "　")           '施主名

            objIptRtnHiddn2.ID = "returnHidden" & lineCounter
            objIptRtnHiddn2.Value = data.Kbn & EarthConst.SEP_STRING & data.HosyousyoNo
            objTdJyuusho1.InnerHtml = com.GetDisplayString(data.BukkenJyuusyo1 & data.BukkenJyuusyo2 & data.BukkenJyuusyo3, "　")          '物件住所
            objTdJyuusho1.Controls.Add(objIptRtnHiddn2)  '戻り要素(hidden)は、必ず先頭列の先頭のInputタグにセットすること
            objTdKameitenCd.InnerHtml = com.GetDisplayString(data.KameitenCd, pStrSpace)                                                   '加盟店コード
            objTdKameitenNm.InnerHtml = com.GetDisplayString(data.KameitenMei1, pStrSpace)                                                 '加盟店名
            objTdHakIraiTime.InnerHtml = com.GetDispStrDateTime(data.HakIraiTime, pStrSpace)                                          '依頼日時
            objTdHosyousyoHakIraisyoTykDate.InnerHtml = com.GetDisplayString(data.HosyousyoHakIraisyoTykDate, pStrSpace)            '依頼書着日
            objTdHosyousyoHakDate.InnerHtml = com.GetDisplayString(data.HosyousyoHakDate, pStrSpace)                                '発行日
            objTdHosyouKaisiDate.InnerHtml = com.GetDisplayString(data.HosyouKaisiDate, pStrSpace)                                  '保証開始日
            objTdKsSiyou.InnerHtml = com.GetDisplayString(data.KsSiyou1 & data.KsSiyouSetuzokusi & data.KsSiyou2, pStrSpace)        '判定
            objTdKojHkksJuriDate.InnerHtml = com.GetDisplayString(data.KojHkksJuriDate, pStrSpace)                                  '工報受理日
            objTdHosyouNasiRiyuu.InnerHtml = com.GetDisplayString(data.HosyouNasiRiyuu, pStrSpace)                                  '保証なし理由
            objTdDisplayName.InnerHtml = com.GetDisplayString(data.DisplayName, pStrSpace)                                          '営業担当者

            '加盟店発行設定/初回発行方法（発行タイミング）
            If data.HosyousyoHakUmu = 0 Then
                '依頼書
                objTdHosyousyoHakUmu.InnerHtml = com.GetDisplayString(EarthConst.HINSITU_HOSYOUSYO_JYOUKYOU_HAKKOUTIMING_IRAISYO, pStrSpace)
            ElseIf data.HosyousyoHakUmu = 1 Then
                '自動発行
                objTdHosyousyoHakUmu.InnerHtml = com.GetDisplayString(EarthConst.HINSITU_HOSYOUSYO_JYOUKYOU_HAKKOUTIMING_JIDOU, pStrSpace)
            ElseIf data.HosyousyoHakUmu = 2 Then
                '地盤モール
                objTdHosyousyoHakUmu.InnerHtml = com.GetDisplayString(EarthConst.HINSITU_HOSYOUSYO_JYOUKYOU_HAKKOUTIMING_JIBANMALL, pStrSpace)
            Else
                'NULL を想定＞空白
                objTdHosyousyoHakUmu.InnerHtml = pStrSpace
            End If

            objTdHosyouKikanMK.InnerHtml = com.GetDisplayString(data.HosyouKikanMK, pStrSpace)                                      '加/保証期間
            objTdHosyouKikanTJ.InnerHtml = com.GetDisplayString(data.HosyouKikanTJ, pStrSpace)                                      '物/保証期間
            objTdIraiDate.InnerHtml = com.GetDisplayString(data.IraiDate, pStrSpace)                                                '物件依頼日
            objTdBikou.InnerHtml = com.GetDisplayString(data.Bikou, pStrSpace)                                                      '備考

            'スタイル、Class設定
            objTdIkkatu.Attributes("class") = "textCenter"    '一括
            objCheckIkkatu.Style("height") = "15px;"

            '*****************************************
            ' 発行進捗状況の判断
            ' ☆ モール依頼（再発行含）済・未受付の場合はチェック有
            ' 進捗データ.発行依頼日時 <> 空白		
            '  AND	（進捗データ.発行依頼受付日時 = 空白
            '         OR 進捗データ.発行依頼日時 > 進捗データ.発行依頼受付日時）
            '  AND	（進捗データ.発行依頼キャンセル日時 = 空白
            '	      OR 進捗データ.発行依頼日時 > 進捗データ.発行依頼キャンセル日時）
            If data.HakIraiTime <> Nothing _
            AndAlso (data.HakIraiUkeDatetime = Nothing OrElse data.HakIraiTime > data.HakIraiUkeDatetime) _
            AndAlso (data.HakIraiCanDatetime = Nothing OrElse data.HakIraiTime > data.HakIraiCanDatetime) Then
                objCheckIkkatu.Checked() = True
            Else
                ' 依頼（再発行含）済・未受付以外は、廃棄済みと同様に非表示
                objCheckIkkatu.Visible = False
                objTdIkkatu.InnerHtml = pStrSpace
                With objTdIkkatu.Controls
                    .Add(objIptRtnHiddn1)  '戻り要素(hidden)は、必ず先頭列の先頭のInputタグにセットすること
                End With
                objTdIkkatu.Attributes.Remove("onclick")
            End If
            '*****************************************

            '*****************************************
            '発行進捗状況の判断
            'チェックを非表示にする
            '処理済区分・番号は画面表示中は保持し続ける
            For intCnt = 0 To arrKbn.Length - 1
                If arrKbn(intCnt) <> "" AndAlso arrKbn(intCnt) = data.Kbn AndAlso arrNo(intCnt) = data.HosyousyoNo Then
                    objCheckIkkatu.Style("visibility") = "hidden"
                End If
            Next
            '*****************************************

            objTdKubun.Attributes("class") = "textCenter"    '区分
            objTdBangou.Attributes("class") = "textCenter"   '番号
            objTdKameitenCd.Attributes("class") = "textCenter"    '加盟店コード
            objTdKtTorikesi.Attributes("class") = "textCenter"    '加盟店コード取消

            '真ん中
            objTdHosyousyoHakUmu.Attributes("class") = "textCenter"   '発行タイミング
            objTdHosyouKikanMK.Attributes("class") = "textCenter"     '加/保証期間
            objTdHosyouKikanTJ.Attributes("class") = "textCenter"     '物/保証期間

            'hidden
            objTdHaki.Style("display") = "none;"

            '日付型
            objTdHakIraiTime.Attributes("class") = "date textCenter"                   '依頼日時
            objTdHosyousyoHakIraisyoTykDate.Attributes("class") = "date textCenter"    '依頼書着日
            objTdHosyousyoHakDate.Attributes("class") = "date textCenter"              '発行日
            objTdHosyouKaisiDate.Attributes("class") = "date textCenter"               '保証開始日
            objTdKojHkksJuriDate.Attributes("class") = "date textCenter"               '工報受理日
            objTdIraiDate.Attributes("class") = "date textCenter"                      '物件依頼日

            '加盟店取消の場合、文字色変更
            strKtInfoColor = com.getKameitenFontColor(data.KtTorikesi)
            objTdKameitenCd.Style(EarthConst.STYLE_FONT_COLOR) = strKtInfoColor
            objTdKtTorikesi.Style(EarthConst.STYLE_FONT_COLOR) = strKtInfoColor
            objTdKameitenNm.Style(EarthConst.STYLE_FONT_COLOR) = strKtInfoColor

            '各セルの幅設定
            If lineCounter = 1 Then
                '左側
                objTdIkkatu.Style("width") = widthList1(0)    '一括チェックボックス
                'objTdHaki.Style("width") = widthList1(1)      '破棄種別
                objTdKubun.Style("width") = widthList1(1)     '区分
                objTdBangou.Style("width") = widthList1(2)    '番号
                objTdSeshuNm.Style("width") = widthList1(3)   '施主名
                '右側
                objTdJyuusho1.Style("width") = widthList2(0)                      '物件住所
                objTdKameitenCd.Style("width") = widthList2(1)                    '加盟店コード
                objTdKameitenNm.Style("width") = widthList2(2)                    '加盟店名
                objTdHakIraiTime.Style("width") = widthList2(3)                   '依頼日時
                objTdHosyousyoHakIraisyoTykDate.Style("width") = widthList2(4)    '依頼書着日
                objTdHosyousyoHakDate.Style("width") = widthList2(5)              '発行日
                objTdHosyouKaisiDate.Style("width") = widthList2(6)               '保証開始日
                objTdKsSiyou.Style("width") = widthList2(7)                       '判定
                objTdKojHkksJuriDate.Style("width") = widthList2(8)               '工報受理日
                objTdHosyouNasiRiyuu.Style("width") = widthList2(9)              '保証なし理由
                objTdDisplayName.Style("width") = widthList2(10)                  '営業担当者
                objTdHosyousyoHakUmu.Style("width") = widthList2(11)              '発行タイミング
                objTdHosyouKikanMK.Style("width") = widthList2(12)                '加/保証期間
                objTdHosyouKikanTJ.Style("width") = widthList2(13)                '物/保証期間
                objTdIraiDate.Style("width") = widthList2(14)                     '物件依頼日
                objTdBikou.Style("width") = widthList2(15)                        '備考
            End If

            objTr1.ID = "DataTable_resultTr1_" & lineCounter
            objTr1.Attributes("tabindex") = -1

            '行にセルをセット(左側)
            With objTr1.Controls
                .Add(objTdIkkatu)    '一括チェックボックス
                '.Add(objTdHaki)      '破棄種別
                .Add(objTdKubun)     '区分
                .Add(objTdBangou)    '番号
                .Add(objTdSeshuNm)   '施主名
            End With

            objTr2.ID = "DataTable_resultTr2_" & lineCounter
            If lineCounter = 1 Then
                '1行目にTabindexを設定＆フォーカス時にスクロールを最上段まで戻すJSを設定
                objTr2.Attributes("tabindex") = Integer.Parse(CheckHakiTaisyou.Attributes("tabindex")) + 10
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop(" & DivLeftData.ClientID & ");"
            Else
                '2行目以降はタブ移動なし
                objTr2.Attributes("tabindex") = -1
            End If

            '行にセルをセット(右側)
            With objTr2.Controls
                .Add(objTdJyuusho1)                      '物件住所
                .Add(objTdKameitenCd)                    '加盟店コード
                .Add(objTdKameitenNm)                    '加盟店名
                .Add(objTdHakIraiTime)                   '依頼日時
                .Add(objTdHosyousyoHakIraisyoTykDate)    '依頼書着日
                .Add(objTdHosyousyoHakDate)              '発行日
                .Add(objTdHosyouKaisiDate)               '保証開始日
                .Add(objTdKsSiyou)                       '判定
                .Add(objTdKojHkksJuriDate)               '工報受理日
                .Add(objTdHosyouNasiRiyuu)               '保証なし理由
                .Add(objTdDisplayName)                   '営業担当者
                .Add(objTdHosyousyoHakUmu)               '発行タイミング
                .Add(objTdHosyouKikanMK)                 '加/保証期間
                .Add(objTdHosyouKikanTJ)                 '物/保証期間
                .Add(objTdIraiDate)                      '物件依頼日
                .Add(objTdBikou)                         '備考
            End With

            'テーブルに行をセット
            '左側
            TableDataTable1.Controls.Add(objTr1)
            '右側
            TableDataTable2.Controls.Add(objTr2)

            If list.Count = 1 Then
                '検索結果1件のみの場合の列ID格納用hiddenに値をセット
                firstSend.Value = objTr1.ClientID
            End If
        Next

    End Sub

    ''' <summary>
    ''' 画面項目から検索キーレコードへの値セットを行う。
    ''' </summary>
    ''' <param name="recKey">抽出レコードクラスのキー</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromCtrl(ByRef recKey As HinsituHosyousyoJyoukyouRecord)
        ' 区分が条件に入るのは "全て以外" のとき
        If kubun_all.Checked = False Then
            ' 区分1
            rec.Kbn1 = IIf(cmbKubun_1.SelectedValue <> "", cmbKubun_1.SelectedValue, String.Empty)
        End If
        ' 保証書NO From
        rec.HosyousyoNoFrom = IIf(hoshouNo_from.Value <> "", hoshouNo_from.Value, String.Empty)
        ' 保証書NO To
        rec.HosyousyoNoTo = IIf(hoshouNo_to.Value <> "", hoshouNo_to.Value, String.Empty)
        ' 契約NO
        rec.KeiyakuNo = IIf(Me.TextKeiyakuNo.Value <> "", TextKeiyakuNo.Value, String.Empty)

        ' 物件名
        rec.SesyuMei = IIf(BukkenName.Value <> "", BukkenName.Value, String.Empty)
        ' 物件住所１＋２
        rec.BukkenJyuusyo12 = IIf(bukkenJyuusho12.Value <> "", bukkenJyuusho12.Value.Replace("％"c, "%"c), String.Empty)
        ' 備考
        rec.Bikou = IIf(TextBikou.Value <> "", TextBikou.Value, String.Empty)
        ' 調査会社コード・調査会社事業所コード
        rec.TysKaisyaCd = IIf(tyousakaisyaCd.Value <> "", tyousakaisyaCd.Value, String.Empty)

        ' 発行進捗状況1
        rec.HakkouStatus1 = IIf(chkHakkouStatus1.Checked, chkHakkouStatus1.Value, Integer.MinValue)
        ' 発行進捗状況2
        rec.HakkouStatus2 = IIf(chkHakkouStatus2.Checked, chkHakkouStatus2.Value, Integer.MinValue)
        ' 発行進捗状況3
        rec.HakkouStatus3 = IIf(chkHakkouStatus3.Checked, chkHakkouStatus3.Value, Integer.MinValue)
        ' 発行進捗状況4
        rec.HakkouStatus4 = IIf(chkHakkouStatus4.Checked, chkHakkouStatus4.Value, Integer.MinValue)
        ' 発行進捗状況5
        rec.HakkouStatus5 = IIf(chkHakkouStatus5.Checked, chkHakkouStatus5.Value, Integer.MinValue)
        ' 発行進捗状況6
        rec.HakkouStatus6 = IIf(chkHakkouStatus6.Checked, chkHakkouStatus6.Value, Integer.MinValue)

        ' 加盟店コード
        rec.KameitenCd = IIf(kameitenCd.Value <> "", kameitenCd.Value, String.Empty)
        ' 加盟店カナ
        rec.TenmeiKana1 = IIf(kameitenKana.Value <> "", kameitenKana.Value, String.Empty)
        ' 発行タイミング
        rec.HakkouTiming = IIf(cmbHakkouTiming.SelectedValue <> "", cmbHakkouTiming.SelectedValue, Integer.MinValue)

        ' 依頼書着日 空
        rec.HosyousyoHakkouIraisyoTyakuDateChk = IIf(chkIraisyoTykDateBlank.Checked, chkIraisyoTykDateBlank.Value, Integer.MinValue)
        ' 依頼書着日 From
        rec.HosyousyoHakkouIraisyoTyakuDateFrom = IIf(TextIraisyoTykDateFrom.Value <> "", TextIraisyoTykDateFrom.Value, DateTime.MinValue)
        ' 依頼書着日 To
        rec.HosyousyoHakkouIraisyoTyakuDateTo = IIf(TextIraisyoTykDateTo.Value <> "", TextIraisyoTykDateTo.Value, DateTime.MinValue)

        ' 発行日 空
        rec.HosyousyoHakkouDateChk = IIf(chkHakkouDateBlank.Checked, chkHakkouDateBlank.Value, Integer.MinValue)
        ' 発行日 From
        rec.HosyousyoHakkouDateFrom = IIf(TextHakkouDateFrom.Value <> "", TextHakkouDateFrom.Value, DateTime.MinValue)
        ' 発行日 To
        rec.HosyousyoHakkouDateTo = IIf(TextHakkouDateTo.Value <> "", TextHakkouDateTo.Value, DateTime.MinValue)

        ' 再発行日 From
        rec.HosyousyoSaihakDateFrom = IIf(TextSaihakkouDateFrom.Value <> "", TextSaihakkouDateFrom.Value, DateTime.MinValue)
        ' 再発行日 To
        rec.HosyousyoSaihakDateTo = IIf(TextSaihakkouDateTo.Value <> "", TextSaihakkouDateTo.Value, DateTime.MinValue)

        ' 発行依頼日 空
        rec.HakIraiTimeChk = IIf(chkHakkouIraiDateBlank.Checked, chkHakkouIraiDateBlank.Value, Integer.MinValue)
        ' 発行依頼日 From
        rec.HakIraiTimeFrom = IIf(TextHakkouIraiDateFrom.Value <> "", TextHakkouIraiDateFrom.Value, DateTime.MinValue)
        ' 発行依頼日 To
        rec.HakIraiTimeTo = IIf(TextHakkouIraiDateTo.Value <> "", TextHakkouIraiDateTo.Value, DateTime.MinValue)

        ' 物件依頼日 From
        rec.IraiDateFrom = IIf(TextBukkenIraiDateFrom.Value <> "", TextBukkenIraiDateFrom.Value, DateTime.MinValue)
        ' 物件依頼日 To
        rec.IraiDateTo = IIf(TextBukkenIraiDateTo.Value <> "", TextBukkenIraiDateTo.Value, DateTime.MinValue)

        ' 保証期間(加盟店)
        rec.HosyouKikanMK = IIf(TextHosyouKikanKameiten.Value <> "", TextHosyouKikanKameiten.Value, Integer.MinValue)
        ' 保証期間(物件)
        rec.HosyouKikanTJ = IIf(TextHosyouKikanBukken.Value <> "", TextHosyouKikanBukken.Value, Integer.MinValue)

        ' データ破棄種別
        rec.DataHakiSyubetu = IIf(CheckHakiTaisyou.Checked, CheckHakiTaisyou.Value, Integer.MinValue)

        ' Hidden項目へ検索条件を退避する
        SetHiddenCondition()

    End Sub

    ''' <summary>
    ''' 画面のHidden項目から検索キーレコードへの値セットを行う。
    ''' </summary>
    ''' <param name="recKey">抽出レコードクラスのキー</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromHidden(ByRef recKey As HinsituHosyousyoJyoukyouRecord)

        ' 区分1
        rec.Kbn1 = IIf(Me.HiddenKensakuKbn.Value <> "", Me.HiddenKensakuKbn.Value, String.Empty)

        ' 保証書NO From
        rec.HosyousyoNoFrom = IIf(Me.HiddenKensakuNoFrom.Value <> "", Me.HiddenKensakuNoFrom.Value, String.Empty)
        ' 保証書NO To
        rec.HosyousyoNoTo = IIf(Me.HiddenKensakuNoTo.Value <> "", Me.HiddenKensakuNoTo.Value, String.Empty)
        ' 契約NO()
        rec.KeiyakuNo = IIf(Me.HiddenKensakukeiyakuNo.Value <> "", Me.HiddenKensakukeiyakuNo.Value, String.Empty)

        ' 物件名
        rec.SesyuMei = IIf(Me.HiddenKensakuSesyuMei.Value <> "", Me.HiddenKensakuSesyuMei.Value, String.Empty)
        ' 物件住所１＋２
        rec.BukkenJyuusyo12 = IIf(Me.HiddenKensakuBukkenjyuusyo.Value <> "", Me.HiddenKensakuBukkenjyuusyo.Value.Replace("％"c, "%"c), String.Empty)
        ' 備考
        rec.Bikou = IIf(Me.HiddenKensakuBikou.Value <> "", Me.HiddenKensakuBikou.Value, String.Empty)
        ' 調査会社コード・調査会社事業所コード
        rec.TysKaisyaCd = IIf(Me.HiddenKensakuTysKaisya.Value <> "", Me.HiddenKensakuTysKaisya.Value, String.Empty)

        ' 発行進捗状況1
        rec.HakkouStatus1 = IIf(Me.HiddenKensakuHakSts1.Value <> "", Me.HiddenKensakuHakSts1.Value, Integer.MinValue)
        ' 発行進捗状況2
        rec.HakkouStatus2 = IIf(Me.HiddenKensakuHakSts2.Value <> "", Me.HiddenKensakuHakSts2.Value, Integer.MinValue)
        ' 発行進捗状況3
        rec.HakkouStatus3 = IIf(Me.HiddenKensakuHakSts3.Value <> "", Me.HiddenKensakuHakSts3.Value, Integer.MinValue)
        ' 発行進捗状況4
        rec.HakkouStatus4 = IIf(Me.HiddenKensakuHakSts4.Value <> "", Me.HiddenKensakuHakSts4.Value, Integer.MinValue)
        ' 発行進捗状況5
        rec.HakkouStatus5 = IIf(Me.HiddenKensakuHakSts5.Value <> "", Me.HiddenKensakuHakSts5.Value, Integer.MinValue)
        ' 発行進捗状況6
        rec.HakkouStatus6 = IIf(Me.HiddenKensakuHakSts6.Value <> "", Me.HiddenKensakuHakSts6.Value, Integer.MinValue)

        ' 加盟店コード
        rec.KameitenCd = IIf(Me.HiddenKensakuKameitenCd.Value <> "", Me.HiddenKensakuKameitenCd.Value, String.Empty)
        ' 加盟店カナ
        rec.TenmeiKana1 = IIf(Me.HiddenKensakuTenmeiKana.Value <> "", Me.HiddenKensakuTenmeiKana.Value, String.Empty)
        ' 発行タイミング
        rec.HakkouTiming = IIf(Me.HiddenKensakuHakTiming.Value <> "", Me.HiddenKensakuHakTiming.Value, Integer.MinValue)

        ' 依頼書着日 空
        rec.HosyousyoHakkouIraisyoTyakuDateChk = IIf(Me.HiddenKensakuHakIraiTykChk.Value <> "", Me.HiddenKensakuHakIraiTykChk.Value, Integer.MinValue)
        ' 依頼書着日 From
        rec.HosyousyoHakkouIraisyoTyakuDateFrom = IIf(Me.HiddenKensakuHakIraiTykFrom.Value <> "", Me.HiddenKensakuHakIraiTykFrom.Value, DateTime.MinValue)
        ' 依頼書着日 To
        rec.HosyousyoHakkouIraisyoTyakuDateTo = IIf(Me.HiddenKensakuHakIraiTykTo.Value <> "", Me.HiddenKensakuHakIraiTykTo.Value, DateTime.MinValue)

        ' 発行日 空
        rec.HosyousyoHakkouDateChk = IIf(Me.HiddenKensakuHakDtChk.Value <> "", Me.HiddenKensakuHakDtChk.Value, Integer.MinValue)
        ' 発行日 From
        rec.HosyousyoHakkouDateFrom = IIf(Me.HiddenKensakuHakDtFrom.Value <> "", Me.HiddenKensakuHakDtFrom.Value, DateTime.MinValue)
        ' 発行日 To
        rec.HosyousyoHakkouDateFrom = IIf(Me.HiddenKensakuHakDtTo.Value <> "", Me.HiddenKensakuHakDtTo.Value, DateTime.MinValue)

        ' 再発行日 From
        rec.HosyousyoSaihakDateFrom = IIf(Me.HiddenKensakuSaiHakDtFrom.Value <> "", Me.HiddenKensakuSaiHakDtFrom.Value, DateTime.MinValue)
        ' 再発行日 To
        rec.HosyousyoSaihakDateTo = IIf(Me.HiddenKensakuSaiHakDtTo.Value <> "", Me.HiddenKensakuSaiHakDtTo.Value, DateTime.MinValue)

        ' 発行依頼日 空
        rec.HakIraiTimeChk = IIf(Me.HiddenKensakuHakIraiTimeChk.Value <> "", Me.HiddenKensakuHakIraiTimeChk.Value, Integer.MinValue)
        ' 発行依頼日 From
        rec.HakIraiTimeFrom = IIf(Me.HiddenKensakuHakIraiTimeFrom.Value <> "", Me.HiddenKensakuHakIraiTimeFrom.Value, DateTime.MinValue)
        ' 発行依頼日 To
        rec.HakIraiTimeTo = IIf(Me.HiddenKensakuHakIraiTimeTo.Value <> "", Me.HiddenKensakuHakIraiTimeTo.Value, DateTime.MinValue)

        ' 物件依頼日 From
        rec.IraiDateFrom = IIf(Me.HiddenKensakuIraiDtFrom.Value <> "", Me.HiddenKensakuIraiDtFrom.Value, DateTime.MinValue)
        ' 物件依頼日 To
        rec.IraiDateTo = IIf(Me.HiddenKensakuIraiDtTo.Value <> "", Me.HiddenKensakuIraiDtTo.Value, DateTime.MinValue)

        ' 保証期間(加盟店)
        rec.HosyouKikanMK = IIf(Me.HiddenKensakuHosyouKknMk.Value <> "", Me.HiddenKensakuHosyouKknMk.Value, Integer.MinValue)
        ' 保証期間(物件)
        rec.HosyouKikanTJ = IIf(Me.HiddenKensakuHosyouKknTj.Value <> "", Me.HiddenKensakuHosyouKknTj.Value, Integer.MinValue)

        ' データ破棄種別
        rec.DataHakiSyubetu = IIf(Me.HiddenKensakuHakiSyubetu.Value <> "", Me.HiddenKensakuHakiSyubetu.Value, Integer.MinValue)

    End Sub

    ''' <summary>
    ''' 画面コントロールの検索条件値Hiddenに退避する(全項目)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHiddenCondition()

        ' 区分が条件に入るのは "全て以外" のとき
        If kubun_all.Checked = False Then
            ' 区分1
            Me.HiddenKensakuKbn.Value = IIf(cmbKubun_1.SelectedValue <> "", cmbKubun_1.SelectedValue, String.Empty)
        End If
        ' 保証書NO From
        Me.HiddenKensakuNoFrom.Value = IIf(hoshouNo_from.Value <> "", hoshouNo_from.Value, String.Empty)
        ' 保証書NO To
        Me.HiddenKensakuNoTo.Value = IIf(hoshouNo_to.Value <> "", hoshouNo_to.Value, String.Empty)
        ' 契約NO
        Me.HiddenKensakukeiyakuNo.Value = IIf(Me.TextKeiyakuNo.Value <> "", TextKeiyakuNo.Value, String.Empty)

        ' 物件名
        Me.HiddenKensakuSesyuMei.Value = IIf(BukkenName.Value <> "", BukkenName.Value, String.Empty)
        ' 物件住所１＋２
        Me.HiddenKensakuBukkenjyuusyo.Value = IIf(bukkenJyuusho12.Value <> "", bukkenJyuusho12.Value.Replace("％"c, "%"c), String.Empty)
        ' 備考
        Me.HiddenKensakuBikou.Value = IIf(TextBikou.Value <> "", TextBikou.Value, String.Empty)
        ' 調査会社コード・調査会社事業所コード
        Me.HiddenKensakuTysKaisya.Value = IIf(tyousakaisyaCd.Value <> "", tyousakaisyaCd.Value, String.Empty)

        ' 発行進捗状況1
        Me.HiddenKensakuHakSts1.Value = IIf(chkHakkouStatus1.Checked, chkHakkouStatus1.Value, Integer.MinValue)
        ' 発行進捗状況2
        Me.HiddenKensakuHakSts2.Value = IIf(chkHakkouStatus2.Checked, chkHakkouStatus2.Value, Integer.MinValue)
        ' 発行進捗状況3
        Me.HiddenKensakuHakSts3.Value = IIf(chkHakkouStatus3.Checked, chkHakkouStatus3.Value, Integer.MinValue)
        ' 発行進捗状況4
        Me.HiddenKensakuHakSts4.Value = IIf(chkHakkouStatus4.Checked, chkHakkouStatus4.Value, Integer.MinValue)
        ' 発行進捗状況5
        Me.HiddenKensakuHakSts5.Value = IIf(chkHakkouStatus5.Checked, chkHakkouStatus5.Value, Integer.MinValue)
        ' 発行進捗状況6
        Me.HiddenKensakuHakSts6.Value = IIf(chkHakkouStatus6.Checked, chkHakkouStatus6.Value, Integer.MinValue)

        ' 加盟店コード
        Me.HiddenKensakuKameitenCd.Value = IIf(kameitenCd.Value <> "", kameitenCd.Value, String.Empty)
        ' 加盟店カナ
        Me.HiddenKensakuTenmeiKana.Value = IIf(kameitenKana.Value <> "", kameitenKana.Value, String.Empty)
        ' 発行タイミング
        Me.HiddenKensakuHakTiming.Value = IIf(cmbHakkouTiming.SelectedValue <> "", cmbHakkouTiming.SelectedValue, Integer.MinValue)

        ' 依頼書着日 空
        Me.HiddenKensakuHakIraiTykChk.Value = IIf(chkIraisyoTykDateBlank.Checked, chkIraisyoTykDateBlank.Value, Integer.MinValue)
        ' 依頼書着日 From
        Me.HiddenKensakuHakIraiTykFrom.Value = IIf(TextIraisyoTykDateFrom.Value <> "", TextIraisyoTykDateFrom.Value, DateTime.MinValue)
        ' 依頼書着日 To
        Me.HiddenKensakuHakIraiTykTo.Value = IIf(TextIraisyoTykDateTo.Value <> "", TextIraisyoTykDateTo.Value, DateTime.MinValue)

        ' 発行日 空
        Me.HiddenKensakuHakDtChk.Value = IIf(chkHakkouDateBlank.Checked, chkHakkouDateBlank.Value, Integer.MinValue)
        ' 発行日 From
        Me.HiddenKensakuHakDtFrom.Value = IIf(TextHakkouDateFrom.Value <> "", TextHakkouDateFrom.Value, DateTime.MinValue)
        ' 発行日 To
        Me.HiddenKensakuHakDtTo.Value = IIf(TextHakkouDateTo.Value <> "", TextHakkouDateTo.Value, DateTime.MinValue)

        ' 再発行日 From
        Me.HiddenKensakuSaiHakDtFrom.Value = IIf(TextSaihakkouDateFrom.Value <> "", TextSaihakkouDateFrom.Value, DateTime.MinValue)
        ' 再発行日 To
        Me.HiddenKensakuSaiHakDtTo.Value = IIf(TextSaihakkouDateTo.Value <> "", TextSaihakkouDateTo.Value, DateTime.MinValue)

        ' 発行依頼日 空
        Me.HiddenKensakuHakIraiTimeChk.Value = IIf(chkHakkouIraiDateBlank.Checked, chkHakkouIraiDateBlank.Value, Integer.MinValue)
        ' 発行依頼日 From
        Me.HiddenKensakuHakIraiTimeFrom.Value = IIf(TextHakkouIraiDateFrom.Value <> "", TextHakkouIraiDateFrom.Value, DateTime.MinValue)
        ' 発行依頼日 To
        Me.HiddenKensakuHakIraiTimeTo.Value = IIf(TextHakkouIraiDateTo.Value <> "", TextHakkouIraiDateTo.Value, DateTime.MinValue)

        ' 物件依頼日 From
        Me.HiddenKensakuIraiDtFrom.Value = IIf(TextBukkenIraiDateFrom.Value <> "", TextBukkenIraiDateFrom.Value, DateTime.MinValue)
        ' 物件依頼日 To
        Me.HiddenKensakuIraiDtTo.Value = IIf(TextBukkenIraiDateTo.Value <> "", TextBukkenIraiDateTo.Value, DateTime.MinValue)

        ' 保証期間(加盟店)
        Me.HiddenKensakuHosyouKknMk.Value = IIf(TextHosyouKikanKameiten.Value <> "", TextHosyouKikanKameiten.Value, Integer.MinValue)
        ' 保証期間(物件)
        Me.HiddenKensakuHosyouKknTj.Value = IIf(TextHosyouKikanBukken.Value <> "", TextHosyouKikanBukken.Value, Integer.MinValue)

        ' データ破棄種別
        Me.HiddenKensakuHakiSyubetu.Value = IIf(CheckHakiTaisyou.Checked, CheckHakiTaisyou.Value, Integer.MinValue)
    End Sub

    ''' <summary>
    ''' 検索条件で検索した内容を検索結果テーブルに表示する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' 表示最大件数
        Dim end_count As Integer = IIf(maxSearchCount.Value <> "max", maxSearchCount.Value, EarthConst.MAX_RESULT_COUNT)
        Dim intCsvMaxCnt As Integer = Integer.Parse(EarthConst.MAX_CSV_OUTPUT)

        Dim logic As New JibanLogic
        Dim total_count As Integer = 0

        ' 検索実行
        Dim resultArray As List(Of HinsituHosyousyoJyoukyouSearchRecord) = logic.getJibanSearchHinsituRecord(sender, rec, 1, end_count, total_count)

        If total_count = 0 Then
            ' 検索結果ゼロ件の場合、メッセージを表示
            mLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
        ElseIf total_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            Exit Sub
        End If

        ' CSV出力上限以上の場合、確認メッセージを付与
        If total_count > intCsvMaxCnt Then
            Me.HiddenCsvMaxCnt.Value = "1"
        Else
            Me.HiddenCsvMaxCnt.Value = String.Empty
        End If

        ' 検索件数を画面に表示
        Dim displayCount As String = ""
        If end_count <> EarthConst.MAX_RESULT_COUNT And end_count < total_count Then
            resultCount.Style("color") = "red"
            displayCount = end_count & " / " & CommonLogic.Instance.GetDisplayString(total_count)
        Else
            resultCount.Style.Remove("color")
            displayCount = CommonLogic.Instance.GetDisplayString(total_count)
        End If
        resultCount.InnerHtml = displayCount

        ' 結果を画面に表示
        createDataTable(resultArray)

    End Sub

    ''' <summary>
    ''' 入力項目チェック
    ''' </summary>
    ''' <param name="pList">List(Of HinsituHosyousyoJyoukyouSearchRecord)</param>
    ''' <param name="lstChkOk">List(Of HinsituHosyousyoJyoukyouSearchRecord)</param>
    ''' <remarks>
    ''' コード入力値変更チェック<br/>
    ''' 必須チェック<br/>
    ''' 禁則文字チェック(文字列入力フィールドが対象)<br/>
    ''' バイト数チェック(文字列入力フィールドが対象)<br/>
    ''' 桁数チェック<br/>
    ''' 日付チェック<br/>
    ''' その他チェック<br/>
    ''' </remarks>
    Public Function checkInput(ByVal pList As List(Of HinsituHosyousyoJyoukyouSearchRecord), _
                               ByRef lstChkOk As List(Of HinsituHosyousyoJyoukyouSearchRecord)) As Boolean
        Dim e As New System.EventArgs

        'エラーメッセージ初期化
        Dim errMess As String = ""

        '画面の区分・番号を取得する
        Dim intRowCnt As Integer = Me.TableDataTable1.Controls.Count
        Dim intCnt As Integer = 0
        Dim listKbnNo As New List(Of String)

        For intCnt = 0 To pList.Count - 1

            'チェック1
            '地盤テーブル.更新日時 > 排他日時変数
            If pList(intCnt).UpdDatetime > Date.ParseExact(HiddenHaitaDate.Value, EarthConst.FORMAT_DATE_TIME_2, Nothing) Then
                '他で更新があったため更新できなかった物件があります
                errMess += Messages.MSG218E
                Continue For
            End If

            'チェック2
            '加盟店注意事項種別マスタ.注意事項種別 = 55のレコードが存在し、
            '進捗データ.発行依頼引渡日が空白の場合
            If pList(intCnt).TyuuijikouSyubetu <> 0 AndAlso _
               IIf(pList(intCnt).HakIraiHwDate = Nothing, "", pList(intCnt).HakIraiHwDate) = Nothing Then
                'お引渡し日が不正な物件が有ります
                errMess += Messages.MSG219E
                Continue For
            End If

            'チェック3
            '進捗データ.発行依頼引渡日がシステム日付の過去 3 年～未来 3 年の範囲外の場合
            If Not cl.checkDateHanniFromTo(pList(intCnt).HakIraiHwDate, Date.Today.AddYears(-3), Date.Today.AddYears(3)) Then
                'お引渡し日が不正な物件が有ります
                errMess += Messages.MSG219E
                Continue For
            End If

            'チェック4
            'ISNULL(地盤テーブル.保証書再発行日,地盤テーブル.保証書発行日) >= システム日付の場合
            If IIf(pList(intCnt).HosyousyoSaihakDate = Nothing, pList(intCnt).HosyousyoHakDate, pList(intCnt).HosyousyoSaihakDate) >= Date.Today Then
                '重複依頼の可能性がある物件があります
                errMess += Messages.MSG220E
                Continue For
            End If

            'チェック5
            '進捗データ.依頼物件名称の桁数 >50 の場合
            If pList(intCnt).HakIraiBknName <> String.Empty AndAlso pList(intCnt).HakIraiBknName.Length > 50 Then
                '物件名または物件住所の桁数が大きすぎる物件があります
                errMess += Messages.MSG221E
                Continue For
                'End If
            End If

            'チェック6
            '進捗データ.依頼物件所在地1の桁数 >32 の場合
            If pList(intCnt).HakIraiBknAdr1 <> String.Empty AndAlso pList(intCnt).HakIraiBknAdr1.Length > 32 Then
                '物件名または物件住所の桁数が大きすぎる物件があります
                errMess += Messages.MSG221E
                Continue For
            End If

            'チェック7
            '進捗データ.依頼物件所在地2の桁数 >32 の場合
            If pList(intCnt).HakIraiBknAdr2 <> String.Empty AndAlso pList(intCnt).HakIraiBknAdr2.Length > 32 Then
                '物件名または物件住所の桁数が大きすぎる物件があります
                errMess += Messages.MSG221E
                Continue For
            End If

            'チェック8
            '進捗データ.依頼物件所在地3の桁数 >54
            If pList(intCnt).HakIraiBknAdr3 <> String.Empty AndAlso pList(intCnt).HakIraiBknAdr3.Length > 54 Then
                '物件名または物件住所の桁数が大きすぎる物件があります
                errMess += Messages.MSG221E
                Continue For
            End If

            'チェック9
            '進捗データ.経由 =9 の場合
            If pList(intCnt).Keiyu = 9 Then
                '地盤モール連携対象外の物件があります
                errMess += Messages.MSG224E
                Continue For
            End If

            'チェック10
            '邸別請求(分類ｺｰﾄﾞ = 170)のレコードがある場合
            If pList(intCnt).HosyousyoNoTk <> String.Empty Then
                '金額訂正が必要な物件があります
                errMess += Messages.MSG222E
                Continue For
            End If

            'チェック11
            '保証書管理テーブル.物件状況 = 0 OR 2 の場合
            If pList(intCnt).BukkenJyky = 0 OrElse pList(intCnt).BukkenJyky = 2 Then
                '発行不可の物件があります
                errMess += Messages.MSG223E
                Continue For
            End If

            'OKだったレコードをリストに追加する
            lstChkOk.Add(pList(intCnt))

            'OKの区分・番号をHiddenに格納していく
            HiddenIkkatuSyoriZumiKbn.Value += pList(intCnt).Kbn + EarthConst.SEP_STRING
            HiddenIkkatuSyoriZumiNo.Value += pList(intCnt).HosyousyoNo + EarthConst.SEP_STRING

        Next

        'エラー発生時
        If errMess <> "" Then
            'エラーメッセージ表示
            Dim tmpScript = "alert('" & errMess & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", tmpScript, True)
            Return False
        End If

        'エラー無しの場合、Trueを返す
        Return True

    End Function

    ''' <summary>
    ''' DBに反映する
    ''' </summary>
    ''' <param name="lstJibanRec">List(Of JibanRecordHosyou)</param>
    ''' <remarks></remarks>
    Protected Function SaveData(ByVal lstJibanRec As List(Of JibanRecordHosyou)) As Boolean

        Dim hLogic As New HosyouLogic
        Dim JibanLogic As New JibanLogic
        Dim intCnt As Integer = 0

        For intCnt = 0 To lstJibanRec.Count - 1
            Dim jrOld As New JibanRecord
            ' 現在の地盤データをDBから取得する
            jrOld = JibanLogic.GetJibanData(lstJibanRec(intCnt).Kbn, lstJibanRec(intCnt).HosyousyoNo)

            ' データの更新を行います
            ' 物件履歴は作成しない
            If hLogic.SaveJibanData(Me, lstJibanRec(intCnt), Nothing) = False Then
                Return False
            End If
        Next

        Return True
    End Function

    ''' <summary>
    ''' 保証書レコードに品質保証書レコードをセットする
    ''' </summary>
    ''' <param name="lstChkOk">List(Of HinsituHosyousyoJyoukyouSearchRecord)</param>
    ''' <param name="lstJibanRec">List(Of JibanRecordHosyou)</param>
    ''' <remarks></remarks>
    Public Function SetRecToRec(ByVal lstChkOk As List(Of HinsituHosyousyoJyoukyouSearchRecord), _
                                ByRef lstJibanRec As List(Of JibanRecordHosyou)) As Boolean

        Dim intCnt As Integer = 0
        Dim JibanLogic As New JibanLogic

        For intCnt = 0 To lstChkOk.Count - 1
            Dim jr As New JibanRecord
            Dim jibanRec As New JibanRecordHosyou

            ' 現在の地盤データをDBから取得する
            jr = JibanLogic.GetJibanData(lstChkOk(intCnt).Kbn, lstChkOk(intCnt).HosyousyoNo)

            '進捗T更新用に、DB上の値をセットする
            JibanLogic.SetSintyokuJibanData(jr, jibanRec)

            '商品1～3のコピー
            JibanLogic.ps_CopyTeibetuSyouhinData(jr, jibanRec)

            '保証商品有無
            jibanRec.HosyouSyouhinUmu = jr.HosyouSyouhinUmu

            '***********************************************
            ' 地盤テーブル (共通情報)
            '***********************************************
            ' データ破棄種別
            cl.SetDisplayString(jr.DataHakiSyubetu, jibanRec.DataHakiSyubetu)
            ' データ破棄日
            If jr.DataHakiSyubetu = "0" Then
                jibanRec.DataHakiDate = DateTime.MinValue
            Else
                cl.SetDisplayString(jr.DataHakiDate, jibanRec.DataHakiDate)
            End If

            ' ☆区分
            jibanRec.Kbn = lstChkOk(intCnt).Kbn
            ' ☆番号（保証書NO）
            jibanRec.HosyousyoNo = lstChkOk(intCnt).HosyousyoNo
            ' ☆施主名    
            jibanRec.SesyuMei = lstChkOk(intCnt).HakIraiBknName
            ' ☆物件住所1,2,3
            jibanRec.BukkenJyuusyo1 = lstChkOk(intCnt).HakIraiBknAdr1
            jibanRec.BukkenJyuusyo2 = lstChkOk(intCnt).HakIraiBknAdr2
            jibanRec.BukkenJyuusyo3 = lstChkOk(intCnt).HakIraiBknAdr3
            ' 備考,備考2
            jibanRec.Bikou = jr.Bikou
            jibanRec.Bikou2 = jr.Bikou2
            '調査方法コード
            jibanRec.TysHouhou = jr.TysHouhou
            '調査方法名
            jibanRec.TysHouhouMeiIf = jr.TysHouhouMeiIf
            '調査会社コード
            jibanRec.TysKaisyaCd = jr.TysKaisyaCd
            '調査会社事業所コード
            jibanRec.TysKaisyaJigyousyoCd = jr.TysKaisyaJigyousyoCd
            '調査会社名
            jibanRec.TysKaisyaMeiIf = jr.TysKaisyaMeiIf
            '加盟店コード
            jibanRec.KameitenCd = jr.KameitenCd
            '加盟店名
            jibanRec.KameitenMeiIf = jr.KameitenMeiIf
            '加盟店Tel
            jibanRec.KameitenTelIf = jr.KameitenTelIf
            '加盟店Fax
            jibanRec.KameitenFaxIf = jr.KameitenFaxIf
            '加盟店Mail
            jibanRec.KameitenMailIf = jr.KameitenMailIf
            '構造名
            jibanRec.KouzouMeiIf = jr.KouzouMeiIf

            '***********************************************
            ' 地盤テーブル (保証情報)
            '***********************************************
            ' 契約NO
            cl.SetDisplayString(IIf(jr.KeiyakuNo = Nothing, "", jr.KeiyakuNo), jibanRec.KeiyakuNo)
            ' 基礎報告書有無
            cl.SetDisplayString(IIf(jr.KsHkksUmu = Nothing, "", jr.KsHkksUmu), jibanRec.KsHkksUmu)
            ' 基礎工事完了報告書着日
            cl.SetDisplayString(IIf(jr.KsKojKanryHkksTykDate = Nothing, "", jr.KsKojKanryHkksTykDate), jibanRec.KsKojKanryHkksTykDate)
            '変更予定加盟店コード
            cl.SetDisplayString(IIf(jr.HenkouYoteiKameitenCd = Nothing, "", jr.HenkouYoteiKameitenCd), jibanRec.HenkouYoteiKameitenCd)

            '************************************************
            ' 保証書発行状況、保証書発行状況設定日、保証商品有無の自動設定
            '************************************************
            '商品の自動設定後に行なう
            cbLogic.ps_AutoSetHosyousyoHakJyjy(EarthEnum.emGamenInfo.Hosyou, jibanRec, jr.HosyousyoHakJyky)

            ' ☆保証書発行日
            'cl.SetDisplayString(TextHosyousyoHakkouDate.Text, jibanRec.HosyousyoHakDate)

            ' ☆保証書発行日、保証書再発行日
            If lstChkOk(intCnt).HosyousyoHakDate = Nothing Then
                '地盤テーブル.保証書発行日が空白の場合

                '地盤テーブル.保証書発行日←画面.一括セット発行日
                jibanRec.HosyousyoHakDate = TextIkkatuHakkouDate.Value
                '地盤テーブル.再発行日
                cl.SetDisplayString(IIf(lstChkOk(intCnt).HosyousyoSaihakDate = Nothing, "", lstChkOk(intCnt).HosyousyoSaihakDate), jibanRec.HosyousyoSaihakDate)
            ElseIf lstChkOk(intCnt).HosyousyoSaihakDate = Nothing Then
                '地盤テーブル.保証書再発行日が空白の場合
                '地盤テーブル.保証書再発行日←画面.一括セット発行日

                '保証書発行日
                cl.SetDisplayString(lstChkOk(intCnt).HosyousyoHakDate, jibanRec.HosyousyoHakDate)
                ' 保証書再発行日
                cl.SetDisplayString(TextIkkatuHakkouDate.Value, jibanRec.HosyousyoSaihakDate)
            Else
                '保証書発行日
                cl.SetDisplayString(lstChkOk(intCnt).HosyousyoHakDate, jibanRec.HosyousyoHakDate)
                '地盤テーブル.再発行日←画面.一括セット発行日
                cl.SetDisplayString(TextIkkatuHakkouDate.Value, jibanRec.HosyousyoSaihakDate)

                '***********************************************
                ' 邸別請求テーブル 
                '***********************************************
                ' 保証書再発行
                jibanRec.HosyousyoRecord = GetTeibetuSeikyuuRec(lstChkOk(intCnt), jibanRec)

            End If

            ' 付保証明書FLG
            ' cl.SetDisplayString(jr.FuhoSyoumeisyoFlg, jibanRec.FuhoSyoumeisyoFlg)
            ' 付保証明書発送日が入力されている場合は、付保証明書FLGの自動設定を行なわない
            ' 加盟店備考設定マスタ.備考種別(=42)のチェック
            Dim hLogic As New HosyouLogic
            If jibanRec.FuhoSyoumeisyoHassouDate = Nothing And hLogic.ExistBikouSyubetu(jibanRec.KameitenCd) Then
                ' 付保証明書FLGの自動設定
                Dim KameitenSearchLogic As New KameitenSearchLogic
                Dim recData1 As New KameitenSearchRecord
                Dim tmpScript As String = ""

                recData1 = KameitenSearchLogic.GetFuhoSyoumeisyoInfo( _
                                                    jibanRec.Kbn _
                                                    , jibanRec.KameitenCd _
                                                    , False _
                                                    )
                If Not recData1 Is Nothing Then
                    ' 加盟店.付保証明書FLG
                    If recData1.FuhoSyoumeisyoFlg <> 1 Then '<>有り
                        jibanRec.FuhoSyoumeisyoFlg = "0" 'なし
                    End If

                    '付保証明書開始年月
                    If cl.GetDisplayString(recData1.FuhoSyoumeiKaisiNengetu) = "" Then '未入力
                        jibanRec.FuhoSyoumeisyoFlg = "0" 'なし
                    Else '入力
                        Dim dtHosyousyoHakkouDate As New DateTime '保証書発行日
                        Dim dtFuhoSyoumeiKaisiDate As New DateTime '加盟店.付保証明書開始年月

                        dtHosyousyoHakkouDate = jibanRec.HosyousyoHakDate
                        dtFuhoSyoumeiKaisiDate = recData1.FuhoSyoumeiKaisiNengetu
                        '保証書発行日 < 加盟店.付保証明書開始年月
                        If dtHosyousyoHakkouDate < dtFuhoSyoumeiKaisiDate Then
                            jibanRec.FuhoSyoumeisyoFlg = "0" 'なし
                        Else '保証書発行日 >= 加盟店.付保証明書開始年月
                            ' 加盟店.付保証明書FLG
                            If recData1.FuhoSyoumeisyoFlg = 1 Then '有り
                                jibanRec.FuhoSyoumeisyoFlg = "1" '有り
                            End If
                        End If
                    End If
                End If
            Else
                ' 自動設定しない
            End If


            ' 保証有無
            cl.SetDisplayString(jr.HosyouUmu, jibanRec.HosyouUmu)
            ' ☆保証開始日
            'cl.SetDisplayString(TextHosyouKaisiDate.Text, jibanRec.HosyouKaisiDate)
            cl.SetDisplayString(IIf(lstChkOk(intCnt).HakIraiHwDate = Nothing, "", lstChkOk(intCnt).HakIraiHwDate), jibanRec.HosyouKaisiDate)
            ' 保証期間
            cl.SetDisplayString(jr.HosyouKikan, jibanRec.HosyouKikan)
            ' 保証なし理由コード
            cl.SetDisplayString(IIf(jr.HosyouNasiRiyuuCd = Nothing, "", jr.HosyouNasiRiyuuCd), jibanRec.HosyouNasiRiyuuCd)
            ' 保証なし理由
            cl.SetDisplayString(IIf(jr.HosyouNasiRiyuu = Nothing, "", jr.HosyouNasiRiyuu), jibanRec.HosyouNasiRiyuu)
            ' 保証書発行依頼書有無
            cl.SetDisplayString(IIf(jr.HosyousyoHakIraisyoUmu = Nothing, "", jr.HosyousyoHakIraisyoUmu), jibanRec.HosyousyoHakIraisyoUmu)
            ' 保証書発行依頼書着日
            cl.SetDisplayString(IIf(jr.HosyousyoHakIraisyoTykDate = Nothing, "", jr.HosyousyoHakIraisyoTykDate), jibanRec.HosyousyoHakIraisyoTykDate)
            ' 業務完了日*
            ' ☆更新ログインユーザーID
            cl.SetDisplayString(userInfo.LoginUserId, jibanRec.UpdLoginUserId)
            ' ☆更新日時
            If lstChkOk(intCnt).UpdDatetime = Nothing Then
                jibanRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
            Else
                jibanRec.UpdDatetime = lstChkOk(intCnt).UpdDatetime
            End If

            '***************************************
            ' 画面入力項目以外
            '***************************************
            ' ☆更新者
            jibanRec.Kousinsya = cbLogic.GetKousinsya(userInfo.LoginUserId, DateTime.Now)

            ' ☆発行依頼受付日時
            jibanRec.HakIraiUkeDatetime = Date.Now

            'リストに追加
            lstJibanRec.Add(jibanRec)
        Next

        Return True
    End Function

#Region "邸別請求レコード作成（再発行）"
    ''' <summary>
    ''' 再発行の邸別請求レコードを取得します
    ''' </summary>
    ''' <param name="lstChkOk">HinsituHosyousyoJyoukyouSearchRecord</param>
    ''' <param name="jibanRec">JibanRecordHosyou</param>
    ''' <returns>再発行の邸別請求レコード</returns>
    ''' <remarks></remarks>
    Protected Function GetTeibetuSeikyuuRec(ByVal lstChkOk As HinsituHosyousyoJyoukyouSearchRecord, _
                                            ByVal jibanRec As JibanRecordHosyou) As TeibetuSeikyuuRecord
        Dim record As New TeibetuSeikyuuRecord
        Const strSaiHakRiyuu As String = "2度目の再発行あり"
        Dim jLogic As New JibanLogic
        Dim dt As DataTable

        ' 区分・番号で邸別請求Tを検索し、存在すれば、必要項目のみUpd
        ' 存在しなければ設定する
        dt = jLogic.GetTeibetuSeikyuuData(jibanRec.Kbn, jibanRec.HosyousyoNo, EarthConst.SOUKO_CD_HOSYOUSYO)
        If dt.Rows.Count > 0 Then

            '** KEY情報 *******************
            ' 区分
            cl.SetDisplayString(lstChkOk.Kbn, record.Kbn)
            ' 番号（保証書NO）
            cl.SetDisplayString(lstChkOk.HosyousyoNo, record.HosyousyoNo)
            ' 分類コード
            record.BunruiCd = EarthConst.SOUKO_CD_HOSYOUSYO
            ' 画面表示NO
            record.GamenHyoujiNo = 1

            '** 明細情報 *******************
            ' ☆請求有無(地盤テーブル.請求(請求有無)←1：有)
            cl.SetDisplayString(1, record.SeikyuuUmu)
            ' ☆備考(再発行理由)(邸別請求.備考←邸別請求.備考 +“3通目)
            cl.SetDisplayString(dt.Rows(0)("bikou") & strSaiHakRiyuu, record.Bikou)

            ' 更新ログインユーザーID
            cl.SetDisplayString(userInfo.LoginUserId, record.UpdLoginUserId)
            '更新日時(排他制御用)
            record.UpdDatetime = Me.HiddenHaitaDate.Value

        Else

            '** 邸別請求TにInsertする場合、自動設定項目 *******************
            ' 商品の自動設定
            SetSyouhinInfoSh(jibanRec)

            ' 請求締め日のセット
            Me.HiddenSimeDate.Value = cl.GetDisplayString(cbLogic.GetSeikyuuSimeDateFromKameiten(String.Empty _
                                                                                    , String.Empty _
                                                                                    , String.Empty _
                                                                                    , jibanRec.KameitenCd _
                                                                                    , Me.HiddenTextShSyouhinCd.Value))

            ' 請求書発行日の自動設定
            HiddenTextShSeikyuusyoHakkouDate.Value = GetSeikyuusyoHakkouDate()

            HiddenTextShUriageNengappi.Value = Date.Now.ToString("yyyy/MM/dd") '売上年月日

            ' 発注書確定
            HiddenTextShHattyuusyoKakutei.Value = EarthConst.MIKAKUTEI

            ' 金額系自動設定
            SetKingaku(jibanRec)

            '金額の再計算
            SetZeiKingaku()
            '**************************************************************

            '** KEY情報 *******************
            ' 区分
            cl.SetDisplayString(lstChkOk.Kbn, record.Kbn)
            ' 番号（保証書NO）
            cl.SetDisplayString(lstChkOk.HosyousyoNo, record.HosyousyoNo)
            ' 分類コード
            record.BunruiCd = EarthConst.SOUKO_CD_HOSYOUSYO
            ' 画面表示NO
            record.GamenHyoujiNo = 1

            '** 明細情報 *******************
            ' 商品コード
            cl.SetDisplayString(Me.HiddenTextShSyouhinCd.Value, record.SyouhinCd)
            ' 売上金額
            cl.SetDisplayString(HiddenTextShJituseikyuuKingaku.Value, record.UriGaku)
            ' 仕入金額
            cl.SetDisplayString(0, record.SiireGaku)
            ' 仕入消費税額
            cl.SetDisplayString(0, record.SiireSyouhiZeiGaku)
            ' 税区分
            cl.SetDisplayString(HiddenShZeiKbn.Value, record.ZeiKbn)
            ' 税率
            cl.SetDisplayString(HiddenShZeiritu.Value, record.Zeiritu)
            ' 消費税額
            cl.SetDisplayString(HiddenTextShSyouhizei.Value, record.UriageSyouhiZeiGaku)
            ' 請求書発行日
            cl.SetDisplayString(HiddenTextShSeikyuusyoHakkouDate.Value, record.SeikyuusyoHakDate)
            ' 売上年月日
            cl.SetDisplayString(HiddenTextShUriageNengappi.Value, record.UriDate)
            ' 伝票売上年月日(ロジッククラスで自動セット)
            record.DenpyouUriDate = DateTime.MinValue
            ' ☆請求有無(地盤テーブル.請求(請求有無)←1：有)
            cl.SetDisplayString(1, record.SeikyuuUmu)
            ' 売上計上FLG (Insertのみ)
            record.UriKeijyouFlg = 0
            ' 売上計上日
            record.UriKeijyouDate = Date.MinValue
            ' 確定区分
            record.KakuteiKbn = Integer.MinValue
            ' ☆備考(再発行理由)(邸別請求.備考←邸別請求.備考 +“3通目)
            cl.SetDisplayString(strSaiHakRiyuu, record.Bikou)
            ' 工務店請求額
            cl.SetDisplayString(HiddenTextShKoumutenSeikyuuKingaku.Value, record.KoumutenSeikyuuGaku)
            ' 発注書金額
            cl.SetDisplayString(0, record.HattyuusyoGaku)
            ' 発注書確認日
            cl.SetDisplayString("", record.HattyuusyoKakuninDate)
            ' 一括入金FLG
            record.IkkatuNyuukinFlg = Integer.MinValue
            ' 調査見積書作成日
            record.TysMitsyoSakuseiDate = Date.MinValue
            ' 発注書確定FLG
            record.HattyuusyoKakuteiFlg = IIf(HiddenTextShHattyuusyoKakutei.Value = EarthConst.KAKUTEI, 1, 0)

            ' 更新ログインユーザーID
            cl.SetDisplayString(userInfo.LoginUserId, record.UpdLoginUserId)
            '更新日時(排他制御用)
            'record.UpdDatetime = cl.getDispHaitaUpdTime(Me.HiddenShUpdDateTime)
            record.UpdDatetime = Me.HiddenHaitaDate.Value

        End If

        Return record
    End Function
#End Region

    ''' <summary>
    ''' 金額設定(保証書再発行)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZeiKingaku()
        ' 税抜価格（実請求金額）
        Dim zeinuki_ctrl As HtmlInputHidden
        ' 消費税率
        Dim zeiritu_ctrl As HtmlInputHidden
        ' 消費税額
        Dim zeigaku_ctrl As HtmlInputHidden

        zeinuki_ctrl = HiddenTextShJituseikyuuKingaku
        zeiritu_ctrl = HiddenShZeiritu
        zeigaku_ctrl = HiddenTextShSyouhizei

        Dim zeinuki As Integer = 0
        Dim zeiritu As Decimal = 0
        Dim zeigaku As Integer = 0
        Dim zeikomi_gaku As Integer = 0

        If zeinuki_ctrl.Value = "" Then '未入力
            zeinuki_ctrl.Value = ""
            zeigaku_ctrl.Value = ""
        Else '入力あり
            cl.SetDisplayString(CInt(zeinuki_ctrl.Value), zeinuki)
            cl.SetDisplayString(zeiritu_ctrl.Value, zeiritu)

            zeinuki = IIf(zeinuki = Integer.MinValue, 0, zeinuki)
            zeiritu = IIf(zeiritu = Decimal.MinValue, 0, zeiritu)

            zeigaku = Fix(zeinuki * zeiritu)
            zeikomi_gaku = zeinuki + zeigaku

            zeigaku_ctrl.Value = Format(zeigaku, EarthConst.FORMAT_KINGAKU_1) '消費税
        End If

    End Sub

    ''' <summary>
    ''' 金額系の自動設定(保証書再発行)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKingaku(ByVal jibanRec As JibanRecordHosyou)

        Dim blnTorikesi As Boolean = False
        Dim syouhinRec As New Syouhin23Record
        Dim jibanlogic As New JibanLogic
        Dim sender As New Object

        If HiddenTextShSeikyuusaki.Value = EarthConst.SEIKYU_TYOKUSETU Then '直接請求

            '(*6)加盟店が取消の場合、他請求3系列以外の扱いとする
            Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(jibanRec.Kbn, jibanRec.KameitenCd, "", blnTorikesi)

            If record.Torikesi <> 0 Then '取消フラグがたっている場合
                '**************************************************
                ' 他請求（系列以外）
                '**************************************************
                ' 工務店請求額は０
                HiddenTextShKoumutenSeikyuuKingaku.Value = "0"

                '実請求税抜金額の自動設定
                syouhinRec = JibanLogic.GetSyouhinInfo(HiddenTextShSyouhinCd.Value, EarthEnum.EnumSyouhinKubun.Hosyousyo, jibanRec.KameitenCd)
                If Not syouhinRec Is Nothing Then
                    HiddenShZeiritu.Value = syouhinRec.Zeiritu '税率
                    HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '税区分

                    '実請求税抜金額＝商品マスタ.標準価格
                    HiddenTextShJituseikyuuKingaku.Value = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)
                End If

            Else
                '**************************************************
                ' 直接請求
                '**************************************************
                '工務店(A)
                '実請求(A)

                syouhinRec = jibanlogic.GetSyouhinInfo(HiddenTextShSyouhinCd.Value, EarthEnum.EnumSyouhinKubun.Hosyousyo, jibanRec.KameitenCd)
                If Not syouhinRec Is Nothing Then
                    '工務店請求税抜金額＝商品マスタ.標準価格
                    HiddenTextShKoumutenSeikyuuKingaku.Value = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)
                    '実請求税抜金額＝画面.工務店請求税抜金額
                    HiddenTextShJituseikyuuKingaku.Value = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                    HiddenShZeiritu.Value = syouhinRec.Zeiritu '税率
                    HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '税区分
                End If

            End If

        ElseIf HiddenTextShSeikyuusaki.Value = EarthConst.SEIKYU_TASETU Then '他請求

            Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(jibanRec.Kbn, jibanRec.KameitenCd, "", blnTorikesi)
            '(*6)加盟店が取消の場合、他請求3系列以外の扱いとする
            If record.Torikesi <> 0 Then
                record.KeiretuCd = ""
            End If

            If cl.getKeiretuFlg(record.KeiretuCd) Then '3系列
                '**************************************************
                ' 他請求（3系列）
                '**************************************************
                '工務店(A)
                '実請求(B)

                syouhinRec = jibanlogic.GetSyouhinInfo(HiddenTextShSyouhinCd.Value, EarthEnum.EnumSyouhinKubun.Hosyousyo, jibanRec.KameitenCd)
                If Not syouhinRec Is Nothing Then
                    HiddenShZeiritu.Value = syouhinRec.Zeiritu '税率
                    HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '税区分

                    '工務店請求税抜金額＝商品マスタ.標準価格
                    HiddenTextShKoumutenSeikyuuKingaku.Value = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                    '※画面.工務店請求税抜金額＝0 の場合は 0 固定
                    If HiddenTextShKoumutenSeikyuuKingaku.Value = "0" Then
                        HiddenTextShJituseikyuuKingaku.Value = "0" '実請求税抜金額

                    Else

                        Dim zeinukiGaku As Integer = 0

                        If jibanlogic.GetSeikyuuGakuHinsitu(sender, _
                                                      3, _
                                                      record.KeiretuCd, _
                                                      HiddenTextShSyouhinCd.Value, _
                                                      syouhinRec.HyoujunKkk, _
                                                      zeinukiGaku) Then
                            ' 実請求金額へセット
                            HiddenTextShJituseikyuuKingaku.Value = zeinukiGaku.ToString(EarthConst.FORMAT_KINGAKU_1)
                        End If

                    End If

                End If

            Else '3系列以外

                '**************************************************
                ' 他請求（3系列以外）
                '**************************************************
                '工務店(B)
                '実請求(C)

                ' 工務店請求額は０
                HiddenTextShKoumutenSeikyuuKingaku.Value = "0"

                '実請求税抜金額の自動設定
                syouhinRec = jibanlogic.GetSyouhinInfo(HiddenTextShSyouhinCd.Value, EarthEnum.EnumSyouhinKubun.Hosyousyo, jibanRec.KameitenCd)
                If Not syouhinRec Is Nothing Then
                    HiddenShZeiritu.Value = syouhinRec.Zeiritu '税率
                    HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '税区分

                    '実請求税抜金額＝商品マスタ.標準価格
                    HiddenTextShJituseikyuuKingaku.Value = syouhinRec.HyoujunKkk.ToString(EarthConst.FORMAT_KINGAKU_1)

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' 請求書発行日の取得
    ''' </summary>
    ''' <returns>請求書発行日</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuusyoHakkouDate(Optional ByVal strDenUriDate As String = "") As String
        Dim SeikyuusyoHakkouDate As String = cl.GetDisplayString(cbLogic.CalcSeikyuusyoHakkouDate(Me.HiddenSimeDate.Value, strDenUriDate))

        Return SeikyuusyoHakkouDate
    End Function

#Region "商品の自動設定"
    ''' <summary>
    ''' 商品の基本情報をセットする(保証書再発行)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSyouhinInfoSh(ByVal jibanRec As JibanRecordHosyou)
        '直接請求、他請求の情報を取得
        Dim syouhinRec As Syouhin23Record
        Dim JibanLogic As New JibanLogic

        '商品コード/商品名の自動設定
        syouhinRec = JibanLogic.GetSyouhinInfo("", EarthEnum.EnumSyouhinKubun.Hosyousyo, jibanRec.KameitenCd)
        If syouhinRec Is Nothing Then
            HiddenTextShSyouhinCd.Value = "" '商品コード
        Else
            HiddenTextShSyouhinCd.Value = cl.GetDispStr(syouhinRec.SyouhinCd) '商品コード
            HiddenShZeiritu.Value = syouhinRec.Zeiritu '税率
            HiddenShZeiKbn.Value = syouhinRec.ZeiKbn '税区分
            Me.HiddenTextShSeikyuusaki.Value = syouhinRec.SeikyuuSakiType '●請求先
        End If
    End Sub
#End Region

End Class