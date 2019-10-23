Partial Public Class SearchSeikyuusyoSimeDateRireki
    Inherits System.Web.UI.Page

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

    '共通ロジック
    Private cl As New CommonLogic
    '請求書締日履歴ロジッククラス
    Dim MyLogic As New SeikyuuSimeDateRerekiSearchLogic

#Region "コントロール値"
    Private Const CTRL_NAME_TR1 As String = "DataTable_resultTr1_"
    Private Const CTRL_NAME_TR2 As String = "DataTable_resultTr2_"
    Private Const CTRL_NAME_TD_SENTOU As String = "DataTable_Sentou_Td_"
    Private Const CTRL_NAME_CHECK_TAISYOU As String = "ChkTaisyou_"
    Private Const CTRL_NAME_HIDDEN_UPDDATETIME As String = "HdnUpdDatetime_"
    Private Const CTRL_NAME_HIDDEN_KAGAMI_UPDDATETIME As String = "HdnKagamiUpdDatetime_"
#End Region

#Region "CSSクラス名"
    Private Const CSS_TEXT_CENTER = "textCenter"
    Private Const CSS_KINGAKU = "kingaku"
    Private Const CSS_DATE = "date"
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

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合
            Response.Redirect(UrlConst.MAIN)
            Exit Sub
        End If

        If IsPostBack = False Then

            '●権限のチェック
            '経理業務権限
            If userinfo.KeiriGyoumuKengen = 0 Then
                Response.Redirect(UrlConst.MAIN)
                Exit Sub
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            Dim helper As New DropDownHelper

            ' 請求先区分コンボにデータをバインドする
            helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuSakiKbn, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            setDispAction()

            'ボタン押下イベントの設定
            setBtnEvent()

            'フォーカス設定
            Me.SelectSeikyuuSakiKbn.Focus()

        End If

    End Sub

#Region "ボタンイベント"

    ''' <summary>
    ''' 請求先検索ボタン押下時の処理
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub btnSeikyuuSakiSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeikyuuSakiSearch.ServerClick
        Dim blnResult As Boolean

        '請求先検索画面呼出
        blnResult = cl.CallSeikyuuSakiSearchWindow(sender, e, Me _
                                                        , Me.SelectSeikyuuSakiKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc _
                                                        , Me.TextSeikyuuSakiMei, Me.btnSeikyuuSakiSearch)
        If blnResult Then
            'フォーカスセット
            setFocusAJ(Me.btnSeikyuuSakiSearch)
        End If
    End Sub

    ''' <summary>
    ''' 検索実行ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick
        setFocusAJ(Me.btnSearch) 'フォーカス

        Me.SetCmnSearchResult(sender, e)

    End Sub

    ''' <summary>
    ''' 戻るボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonReturn_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReturn.ServerClick

        '請求書データ作成画面へ戻る
        Response.Redirect(UrlConst.SEIKYUUSYO_DATA_SAKUSEI)

    End Sub

    ''' <summary>
    ''' 履歴取消ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonRirekiTorikesi_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRirekiTorikesi.ServerClick
        Dim tmpScript As String = ""
        Dim objActBtn As HtmlInputButton = Me.ButtonRirekiTorikesi

        ' 入力チェック
        If Me.checkInput(objActBtn) = False Then Exit Sub

        ' 画面の内容をDBに反映する
        If Me.SaveData() Then '登録成功

            tmpScript = "gNoDataMsgFlg = '1';"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonRirekiTorikesi_ServerClick", tmpScript, True)

            Me.SetCmnSearchResult(sender, e)

        Else '登録失敗

            Me.SetCmnSearchResult(sender, e)

            setFocusAJ(Me.btnSearch) 'フォーカス

            tmpScript = "alert('" & Messages.MSG147E.Replace("@PARAM1", "履歴取消") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ButtonRirekiTorikesi_ServerClick", tmpScript, True)
            Exit Sub
        End If
    End Sub

#End Region

#Region "プライベートメソッド"

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
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' ボタンのイベントハンドラを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '検索実行ボタン
        Me.btnSearch.Attributes("onclick") = "checkJikkou(0);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 請求書NOのイベントハンドラを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSeikyuusyoNoFrom.Attributes("onblur") = "if(checkNumber(this))setFromTo(this);"
        Me.TextSeikyuusyoNoTo.Attributes("onblur") = "if(checkNumber(this))this.value = paddingStr(this.value,15,'0');"
       
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'マスタ検索系コード入力項目イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSeikyuuSakiCd.Attributes("onblur") = "clrName(this,'" & TextSeikyuuSakiMei.ClientID & "');"
        Me.TextSeikyuuSakiBrc.Attributes("onblur") = "clrName(this,'" & TextSeikyuuSakiMei.ClientID & "');"
        Me.SelectSeikyuuSakiKbn.Attributes("onblur") = "clrName(this,'" & TextSeikyuuSakiMei.ClientID & "');"

    End Sub

    ''' <summary>
    ''' 各種ボタンの設定
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Private Sub setBtnEvent()
        Dim strChkTaisyou As String = "ChkTaisyou(@PARAM1)"
        Dim strChkJikkou As String = "checkJikkou(@PARAM1)"

        'イベントハンドラ登録
        Dim tmpTouroku As String = "if(confirm('" & Messages.MSG197C & "')){setWindowOverlay(this);}else{return false;}"
        Dim tmpScript As String = "if(" & strChkTaisyou & " && " & strChkJikkou & "){" & tmpTouroku & "}else{return false;}"

        '確認MSG後、OKの場合後続処理を行なう
        Me.ButtonRirekiTorikesi.Attributes("onclick") = tmpScript.Replace("@PARAM1", EarthEnum.emSearchSeikyuuSimeDateRirekiBtnType.Torikesi)

    End Sub

    ''' <summary>
    ''' 画面項目から検索キーレコードへの値セット
    ''' </summary>
    ''' <param name="keyRec">抽出レコードクラスのキー</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromCtrl(ByRef keyRec As SeikyuuSimeDateRirekiKeyRecord)
        Dim SeikyuusyoHakkouNengetuFrom As String = ""
        Dim SeikyuusyoHakkouNengetuTo As String = ""

        '請求先区分
        keyRec.SeikyuuSakiKbn = IIf(Me.SelectSeikyuuSakiKbn.SelectedValue <> String.Empty, Me.SelectSeikyuuSakiKbn.SelectedValue, String.Empty)
        '請求先コード
        keyRec.SeikyuuSakiCd = IIf(Me.TextSeikyuuSakiCd.Value <> String.Empty, Me.TextSeikyuuSakiCd.Value, String.Empty)
        '請求先枝番
        keyRec.SeikyuuSakiBrc = IIf(Me.TextSeikyuuSakiBrc.Value <> String.Empty, Me.TextSeikyuuSakiBrc.Value, String.Empty)
        '請求先カナ名
        keyRec.SeikyuuSakiMeiKana = IIf(Me.TextSeikyuuSakiMeiKana.Value <> String.Empty, Me.TextSeikyuuSakiMeiKana.Value, String.Empty)
        '請求年月日_FROM
        keyRec.SeikyuusyoHakDateFrom = IIf(Me.TextSeikyuusyoHakkouDateFrom.Value <> "", Me.TextSeikyuusyoHakkouDateFrom.Value, DateTime.MinValue)
        '請求年月日_TO
        keyRec.SeikyuusyoHakDateTo = IIf(Me.TextSeikyuusyoHakkouDateTo.Value <> "", Me.TextSeikyuusyoHakkouDateTo.Value, DateTime.MinValue)
        '請求書No_FROM
        keyRec.SeikyuusyoNoFrom = IIf(Me.TextSeikyuusyoNoFrom.Value <> String.Empty, Me.TextSeikyuusyoNoFrom.Value, String.Empty)
        '請求書No_TO
        keyRec.SeikyuusyoNoTo = IIf(Me.TextSeikyuusyoNoTo.Value <> String.Empty, Me.TextSeikyuusyoNoTo.Value, String.Empty)
        '取消
        keyRec.Torikesi = IIf(Me.CheckTorikesiTaisyou.Checked, 0, Integer.MinValue)
        '最新請求締め日のみ表示
        keyRec.NewRirekiDisp = IIf(Me.CheckSaisinSeikyuuSimeDate.Checked, CheckSaisinSeikyuuSimeDate.Value, Integer.MinValue)

    End Sub

    ''' <summary>
    ''' 検索実行時処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCmnSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim keyRec As New SeikyuuSimeDateRirekiKeyRecord

        '検索条件を設定
        Me.SetSearchKeyFromCtrl(keyRec)

        '検索結果を画面に表示
        Me.SetSearchResult(sender, e, keyRec)

    End Sub

    ''' <summary>
    ''' 検索条件で検索した内容を検索結果テーブルに表示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal keyRec As SeikyuuSimeDateRirekiKeyRecord)

        '表示最大件数
        Dim end_count As Integer = Integer.Parse(maxSearchCount.Value)
        Dim total_count As Integer = 0 '取得件数

        '検索実行
        Dim resultArray As New List(Of SeikyuuSimeDateRirekiRecord)

        '対象チェックボックスの初期化
        Me.CheckAll.Checked = False

        resultArray = MyLogic.GetSeikyuuSimeDateRirekiDataInfo(sender, keyRec, 1, end_count, total_count)

        ' 検索結果ゼロ件の場合、メッセージを表示
        If total_count = 0 Then
            ' 検索結果ゼロ件の場合、メッセージを表示
            Dim tmpScript As String = "if(gNoDataMsgFlg != '1'){alert('" & Messages.MSG020E & "')}gNoDataMsgFlg = null;"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "kensakuZero", tmpScript, True)
        ElseIf total_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            Exit Sub
        End If

        '表示件数制限処理
        Dim displayCount As String = ""
        displayCount = total_count
        resultCount.Style.Remove("color")

        If end_count < total_count Then
            resultCount.Style("color") = "red"
            displayCount = CommonLogic.Instance.GetDisplayString(end_count) & " / " & CommonLogic.Instance.GetDisplayString(total_count)
        End If

        '検索結果件数を設定
        resultCount.InnerHtml = displayCount

        '************************
        '* 画面テーブルへ出力
        '************************

        '行カウンタ
        Dim rowCnt As Integer = 0
        Dim strSpace As String = EarthConst.HANKAKU_SPACE

        '************
        '* セル幅取得
        '************
        '各セルの幅設定用のリスト作成（タイトル行の幅をベースにする）
        Dim widthList1 As New List(Of String)
        Dim tableWidth1 As Integer = 0

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

        '************
        '* 変数宣言
        '************
        'TabIndex
        Dim intTrTabIndex As Integer = 5
        'Tr
        Dim objTr1 As HtmlTableRow
        Dim objTr2 As HtmlTableRow

        Dim objTdReturn As New HtmlTableCell

        'Hidden
        Dim objHdnSimeDateRirekiPk As HtmlInputHidden   'PK5つを格納
        Dim objHdnUpdDatetime As HtmlInputHidden        '更新日時
        Dim objHdnKagamiUpdDatetime As HtmlInputHidden  '請求鑑更新日時

        'CheckBox 
        Dim objChkTaisyou As HtmlInputCheckBox          '対象

        'Table Cell
        Dim objTdTaisyou As HtmlTableCell               '対象
        Dim objTdRirekiNo As HtmlTableCell              '履歴NO
        Dim objTdTorikesi As HtmlTableCell              '取消
        Dim objTdSeikyuuSakiCd As HtmlTableCell         '請求先コード
        Dim objTdSeikyuuSakiMei As HtmlTableCell        '請求先名
        Dim objTdSeikyuuSakiMei2 As HtmlTableCell       '請求先名2
        Dim objTdSeikyuusyoHakDate As HtmlTableCell     '請求書発行日
        Dim objTdKonkaiGoseikyuuGaku As HtmlTableCell   '請求金額
        Dim objTdSeikyuusyoNo As HtmlTableCell          '請求書NO
        Dim objTdZenTaisyouFlg As HtmlTableCell         '全対象フラグ

        Dim SimeDateRirekiPK As String = ""             '生成した対象チェックボックスのPKを格納

        '取得したデータを画面に表示
        For Each data As SeikyuuSimeDateRirekiRecord In resultArray
            rowCnt += 1

            'Tr
            objTr1 = New HtmlTableRow
            objTr2 = New HtmlTableRow

            '***********
            '* Table1
            '********
            'Hidden
            objHdnSimeDateRirekiPk = New HtmlInputHidden
            objHdnUpdDatetime = New HtmlInputHidden         'Hidden更新日時
            objHdnKagamiUpdDatetime = New HtmlInputHidden   'Hidden請求鑑更新日時

            'CheckBox 
            objChkTaisyou = New HtmlInputCheckBox           '対象チェックボックス

            'Table Cell
            objTdTaisyou = New HtmlTableCell               '対象
            objTdRirekiNo = New HtmlTableCell              '履歴NO
            objTdTorikesi = New HtmlTableCell              '取消
            objTdSeikyuuSakiCd = New HtmlTableCell         '請求先コード

            '***********
            '* Table2
            '********
            objTdSeikyuuSakiMei = New HtmlTableCell        '請求先名
            objTdSeikyuuSakiMei2 = New HtmlTableCell       '請求先名2
            objTdSeikyuusyoHakDate = New HtmlTableCell     '請求書発行日
            objTdKonkaiGoseikyuuGaku = New HtmlTableCell   '請求金額
            objTdSeikyuusyoNo = New HtmlTableCell          '請求書NO
            objTdZenTaisyouFlg = New HtmlTableCell         '全対象フラグ

            '検索結果配列からセルに格納
            objTdTaisyou.ID = CTRL_NAME_TD_SENTOU & rowCnt

            '******************
            '* 先頭列
            '*************
            If data.MaxRirekiNo = 1 Then
                'Hidden請求書締め日履歴PK
                objHdnSimeDateRirekiPk.ID = "returnHidden" & rowCnt
                With objHdnSimeDateRirekiPk
                    .Value &= data.SeikyuuSakiCd & EarthConst.SEP_STRING
                    .Value &= data.SeikyuuSakiBrc & EarthConst.SEP_STRING
                    .Value &= data.SeikyuuSakiKbn & EarthConst.SEP_STRING
                    .Value &= data.SeikyuusyoHakNengetu & EarthConst.SEP_STRING
                    .Value &= data.SeikyuuSimeDate
                End With
                objTdTaisyou.Controls.Add(objHdnSimeDateRirekiPk)
                objTdTaisyou.Attributes("class") = "searchReturnValues"
                SimeDateRirekiPK = objHdnSimeDateRirekiPk.Value

                'Hidden更新日時
                objHdnUpdDatetime.ID = CTRL_NAME_HIDDEN_UPDDATETIME & rowCnt
                objHdnUpdDatetime.Value = IIf(data.UpdDatetime = DateTime.MinValue, Format(data.AddDatetime, EarthConst.FORMAT_DATE_TIME_1), Format(data.UpdDatetime, EarthConst.FORMAT_DATE_TIME_1))
                objTdTaisyou.Controls.Add(objHdnUpdDatetime)

                'Hidden請求鑑更新日時
                objHdnKagamiUpdDatetime.ID = CTRL_NAME_HIDDEN_KAGAMI_UPDDATETIME & rowCnt
                objHdnKagamiUpdDatetime.Value = Format(data.SkUpdDatetime, EarthConst.FORMAT_DATE_TIME_1)
                objTdTaisyou.Controls.Add(objHdnKagamiUpdDatetime)

                '対象チェックボックス(戻り要素(hidden)は、必ず先頭列の先頭のInputタグにセットすること)
                objChkTaisyou.ID = CTRL_NAME_CHECK_TAISYOU & SimeDateRirekiPK
                objChkTaisyou.Attributes("tabindex") = Integer.Parse(Me.CheckTorikesiTaisyou.Attributes("tabindex")) + intTrTabIndex + rowCnt
                objTdTaisyou.Controls.Add(objChkTaisyou)
            Else
                objTdTaisyou.InnerHtml = strSpace
            End If
            '******************

            objTdRirekiNo.InnerHtml = cl.GetDisplayString(data.RirekiNo, strSpace)

            '取消が0以外の場合は、検索結果欄に「取消」と表示
            If data.Torikesi = 0 Then
                objTdTorikesi.InnerHtml = strSpace
            Else
                objTdTorikesi.InnerHtml = EarthConst.TORIKESI
            End If

            objTdSeikyuuSakiCd.InnerHtml = cl.GetDispSeikyuuSakiCd(data.SeikyuuSakiKbn, data.SeikyuuSakiCd, data.SeikyuuSakiBrc, False)
            objTdSeikyuuSakiMei.InnerHtml = cl.GetDisplayString(data.SeikyuuSakiMei, strSpace)
            objTdSeikyuuSakiMei2.InnerHtml = cl.GetDisplayString(data.SeikyuuSakiMei2, strSpace)
            objTdSeikyuusyoHakDate.InnerHtml = cl.GetDisplayString(data.SeikyuusyoHakDate, strSpace)
            objTdKonkaiGoseikyuuGaku.InnerHtml = cl.GetDisplayString(Format(data.KonkaiGoseikyuuGaku, EarthConst.FORMAT_KINGAKU_1), strSpace)
            objTdSeikyuusyoNo.InnerHtml = cl.GetDisplayString(data.SeikyuusyoNo, strSpace)

            '全対象フラグが1の場合は、検索結果欄に「あり」と表示。全対象フラグが0の場合は、検索結果欄に「なし」と表示。
            If data.ZenTaisyouFlg = 1 Then
                objTdZenTaisyouFlg.InnerHtml = EarthConst.ARI_HIRAGANA
            ElseIf data.ZenTaisyouFlg = 0 Then
                objTdZenTaisyouFlg.InnerHtml = EarthConst.NASI_HIRAGANA
            Else
                objTdZenTaisyouFlg.InnerHtml = strSpace
            End If

            '各セルの幅設定
            If rowCnt = 1 Then
                objTdTaisyou.Style("width") = widthList1(0)
                objTdRirekiNo.Style("width") = widthList1(1)
                objTdTorikesi.Style("width") = widthList1(2)
                objTdSeikyuuSakiCd.Style("width") = widthList1(3)

                objTdSeikyuuSakiMei.Style("width") = widthList2(0)
                objTdSeikyuuSakiMei2.Style("width") = widthList2(1)
                objTdSeikyuusyoHakDate.Style("width") = widthList2(2)
                objTdKonkaiGoseikyuuGaku.Style("width") = widthList2(3)
                objTdSeikyuusyoNo.Style("width") = widthList2(4)
                objTdZenTaisyouFlg.Style("width") = widthList2(5)
            End If

            'スタイル、クラス設定
            objTdTaisyou.Attributes("class") = CSS_TEXT_CENTER
            objTdRirekiNo.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuusyoNo.Attributes("class") = CSS_TEXT_CENTER
            objTdTorikesi.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuuSakiCd.Attributes("class") = CSS_TEXT_CENTER

            objTdSeikyuuSakiMei.Attributes("class") = ""
            objTdSeikyuuSakiMei2.Attributes("class") = ""
            objTdKonkaiGoseikyuuGaku.Attributes("class") = CSS_KINGAKU
            objTdSeikyuusyoHakDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdZenTaisyouFlg.Attributes("class") = CSS_TEXT_CENTER

            '行IDとJSイベントの付与
            objTr1.ID = CTRL_NAME_TR1 & rowCnt
            objTr1.Attributes("tabindex") = -1

            '行にセルをセット1
            With objTr1.Controls
                .Add(objTdTaisyou) '戻り要素(hidden)は、必ず先頭列の先頭のInputタグにセットすること
                .Add(objTdRirekiNo)
                .Add(objTdTorikesi)
                .Add(objTdSeikyuuSakiCd)
            End With

            '行IDとJSイベントの付与
            objTr2.ID = CTRL_NAME_TR2 & rowCnt
            If rowCnt = 1 Then
                objTr2.Attributes("tabindex") = Integer.Parse(Me.CheckTorikesiTaisyou.Attributes("tabindex")) + intTrTabIndex
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop(" & DivLeftData.ClientID & ");"
            Else
                '2行目以降はタブ移動なし
                objTr2.Attributes("tabindex") = -1
            End If

            '行にセルをセット2()
            With objTr2.Controls
                .Add(objTdSeikyuuSakiMei)
                .Add(objTdSeikyuuSakiMei2)
                .Add(objTdSeikyuusyoHakDate)
                .Add(objTdKonkaiGoseikyuuGaku)
                .Add(objTdSeikyuusyoNo)
                .Add(objTdZenTaisyouFlg)
            End With

            'テーブルに行をセット
            TableDataTable1.Controls.Add(objTr1)
            TableDataTable2.Controls.Add(objTr2)

        Next

    End Sub
#End Region

#Region "DB更新処理系"
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
    Public Function checkInput(ByVal prmActBtn As HtmlInputButton) As Boolean
        'エラーメッセージ初期化
        Dim errMess As String = String.Empty
        Dim arrFocusTargetCtrl As New ArrayList

        Dim tmpCtrl As HtmlControl
        If prmActBtn.ID = Me.ButtonRirekiTorikesi.ID Then
            tmpCtrl = Me.ButtonRirekiTorikesi
        Else
            Return False
        End If

        '対象チェックボックスにてデータがセットされているかをチェック
        If Me.HiddenSimeDateRirekiPk.Value = String.Empty _
            Or Me.HiddenUpdDatetime.Value = String.Empty Then
            errMess += Messages.MSG140E
            arrFocusTargetCtrl.Add(tmpCtrl)
        End If

        'エラー無しの場合、Trueを返す
        Return True
    End Function

    ''' <summary>
    ''' 画面の内容をDBに反映する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function SaveData() As Boolean
        Dim listRec As New List(Of SeikyuuSimeDateRirekiRecord)

        '画面からレコードクラスにセット
        listRec = Me.GetCtrlToDataRec()

        ' データの更新を行います
        If MyLogic.saveData(Me, listRec) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 画面の各情報をレコードクラスに取得し、DB更新用レコードクラスを返却する
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCtrlToDataRec() As List(Of SeikyuuSimeDateRirekiRecord)

        Dim listRec As New List(Of SeikyuuSimeDateRirekiRecord)
        Dim dtRec As New SeikyuuSimeDateRirekiRecord
        Dim intCnt As Integer = 0
        Dim arrKeySeikyuusyoSimeDateRireki() As String = Nothing
        Dim arrKeyInfo() As String = Nothing
        Dim arrKeyUpdDatetime() As String = Nothing
        Dim arrKagamiUpdDatetime() As String = Nothing

        If Me.HiddenSimeDateRirekiPk.Value <> String.Empty Then
            arrKeySeikyuusyoSimeDateRireki = Split(Me.HiddenSimeDateRirekiPk.Value, EarthConst.SEP_STRING & EarthConst.SEP_STRING)
        End If

        If Me.HiddenUpdDatetime.Value <> String.Empty Then
            arrKeyUpdDatetime = Split(Me.HiddenUpdDatetime.Value, EarthConst.SEP_STRING & EarthConst.SEP_STRING)
        End If

        If Me.HiddenKagamiUpdDatetime.Value <> String.Empty Then
            arrKagamiUpdDatetime = Split(Me.HiddenKagamiUpdDatetime.Value, EarthConst.SEP_STRING & EarthConst.SEP_STRING)
        End If

        '履歴NOと更新日時と請求鑑更新日時の個数が同一の場合()
        If arrKeySeikyuusyoSimeDateRireki.Length = arrKeyUpdDatetime.Length _
            AndAlso arrKeySeikyuusyoSimeDateRireki.Length = arrKagamiUpdDatetime.Length Then

            If Not arrKeySeikyuusyoSimeDateRireki Is Nothing _
                AndAlso Not arrKeyUpdDatetime Is Nothing _
                AndAlso Not arrKagamiUpdDatetime Is Nothing Then

                For intCnt = 0 To arrKeySeikyuusyoSimeDateRireki.Length - 1
                    '履歴NOあるいは更新日時が空白の場合、次の処理へ
                    If arrKeySeikyuusyoSimeDateRireki(intCnt) = String.Empty _
                        OrElse arrKeyUpdDatetime(intCnt) = String.Empty _
                        OrElse arrKagamiUpdDatetime(intCnt) = String.Empty Then
                        Continue For
                    End If

                    arrKeyInfo = Split(arrKeySeikyuusyoSimeDateRireki(intCnt), EarthConst.SEP_STRING)

                    '***************************************
                    ' 履歴取消データ
                    '***************************************
                    dtRec = New SeikyuuSimeDateRirekiRecord

                    '取消
                    dtRec.Torikesi = 1

                    '請求先コード
                    cl.SetDisplayString(arrKeyInfo(0), dtRec.SeikyuuSakiCd)

                    '請求先枝番
                    cl.SetDisplayString(arrKeyInfo(1), dtRec.SeikyuuSakiBrc)

                    '請求先区分
                    cl.SetDisplayString(arrKeyInfo(2), dtRec.SeikyuuSakiKbn)

                    '請求書発行年月
                    cl.SetDisplayString(arrKeyInfo(3), dtRec.SeikyuusyoHakNengetu)

                    '請求締め日
                    cl.SetDisplayString(arrKeyInfo(4), dtRec.SeikyuuSimeDate)

                    '更新者ユーザーID
                    dtRec.UpdLoginUserId = userinfo.LoginUserId

                    '更新日時 読み込み時のタイムスタンプ
                    If arrKeyUpdDatetime(intCnt) = "" Then
                        dtRec.UpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
                    Else
                        dtRec.UpdDatetime = DateTime.ParseExact(arrKeyUpdDatetime(intCnt), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                    End If

                    '請求鑑更新日時 読み込み時のタイムスタンプ
                    If arrKagamiUpdDatetime(intCnt) = "" Then
                        dtRec.SkUpdDatetime = System.Data.SqlTypes.SqlDateTime.Null
                    Else
                        dtRec.SkUpdDatetime = DateTime.ParseExact(arrKagamiUpdDatetime(intCnt), EarthConst.FORMAT_DATE_TIME_1, Nothing)
                    End If

                    listRec.Add(dtRec)
                Next

            End If
        End If

        Return listRec
    End Function

#Region "更新日時"
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' 更新日時
    ''' </summary>
    ''' <value></value>
    ''' <returns> 更新日時</returns>
    ''' <remarks>排他制御を行う為、検索時の更新日付を設定してください<br/>
    '''          更新時の日付はシステム日付が設定されます</remarks>
    <TableMap("upd_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#End Region

End Class