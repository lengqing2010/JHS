Partial Public Class SearchHannyouSiire
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    ''' <summary>
    ''' メッセージクラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic

#Region "行コントロールID接頭語"
    Private Const CTRL_NAME_TR As String = "resultTr_"
    Private Const CTRL_NAME_HIDDEN_UNIQUE_NO As String = "HdnUniNo_"
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
            'なし

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            setDispAction()

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            'なし

            Me.TextSyouhinCd.Focus() 'フォーカス

        End If

    End Sub

#Region "ボタンイベント"

    ''' <summary>
    ''' 検索処理の実行
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick
        Dim MyLogic As New HannyouSiireLogic
        Dim listResult As List(Of HannyouSiireRecord)

        'Keyレコードクラス
        Dim recKey As New HannyouSiireDataKeyRecord

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
    ''' 商品検索ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSyouhinSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim lgcSyouhinSearch As New UriageDataSearchLogic
        Dim dataArray As New List(Of SyouhinMeisaiRecord)
        Dim total_count As Integer = 0
        Dim tmpScript As String = String.Empty
        Dim strSyouhinCd As String = String.Empty

        '画面からコードを取得
        strSyouhinCd = IIf(Me.TextSyouhinCd.Value <> "", Me.TextSyouhinCd.Value, String.Empty)

        If strSyouhinCd <> String.Empty Then
            '商品検索を実行
            dataArray = lgcSyouhinSearch.GetSyouhinInfo(strSyouhinCd, String.Empty, total_count)
        End If

        If dataArray.Count = 1 Then
            Dim recData As SyouhinMeisaiRecord = dataArray(0)
            Me.TextSyouhinCd.Value = recData.SyouhinCd
            Me.TextHinmei.Value = recData.SyouhinMei

            'フォーカスセット
            masterAjaxSM.SetFocus(Me.btnSyouhinSearch)
        Else
            Dim tmpFocusScript = "objEBI('" & btnSyouhinSearch.ClientID & "').focus();"
            '検索画面表示用JavaScript『callSearch』を実行
            tmpScript = "callSearch('" & Me.TextSyouhinCd.ClientID & EarthConst.SEP_STRING & Me.hdnSyouhinType.ClientID & "','" _
                                       & UrlConst.SEARCH_SYOUHIN & "','" _
                                       & Me.TextSyouhinCd.ClientID & "','" _
                                       & Me.btnSyouhinSearch.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If
    End Sub

    ''' <summary>
    ''' 調査会社検索ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonTysKaisyaSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' 入力されているコードをキーに、マスタを検索し、データを取得できた場合は
        ' 画面情報を更新する。データが無い場合、検索画面を表示する
        Dim tyousakaisyaSearchLogic As New TyousakaisyaSearchLogic
        Dim blnTorikesi As Boolean = True
        Dim dataArray As New List(Of TyousakaisyaSearchRecord)

        If Me.TextTysKaisyaCd.Value <> "" Then
            dataArray = tyousakaisyaSearchLogic.GetTyousakaisyaSearchResult(Me.TextTysKaisyaCd.Value, _
                                                                            "", _
                                                                            "", _
                                                                            "", _
                                                                            blnTorikesi, _
                                                                            "")
        End If

        If dataArray.Count = 1 Then
            Dim recData As TyousakaisyaSearchRecord = dataArray(0)
            Me.TextTysKaisyaCd.Value = recData.TysKaisyaCd & recData.JigyousyoCd
            Me.TextTysKaisyaMei.Value = recData.TysKaisyaMei

            'フォーカスセット
            masterAjaxSM.SetFocus(Me.ButtonTysKaisyaSearch)
        Else
            '調査会社名をクリア
            Me.TextTysKaisyaMei.Value = String.Empty
            Me.HiddenKameitenCd.Value = String.Empty
            Dim tmpFocusScript = "objEBI('" & ButtonTysKaisyaSearch.ClientID & "').focus();"
            '検索画面表示用JavaScript『callSearch』を実行
            Dim tmpScript = "callSearch('" & Me.TextTysKaisyaCd.ClientID & EarthConst.SEP_STRING & Me.HiddenKameitenCd.ClientID & _
                                             "','" & UrlConst.SEARCH_TYOUSAKAISYA & _
                                             "','" & Me.TextTysKaisyaCd.ClientID & EarthConst.SEP_STRING & Me.TextTysKaisyaMei.ClientID & _
                                             "','" & Me.ButtonTysKaisyaSearch.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSearch", tmpScript, True)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' CSV取込ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>指定画面へ遷移する</remarks>
    Protected Sub BtnCsvInput_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCsvInput.ServerClick
        Response.Redirect(UrlConst.EARTH2_HANYOU_SIIRE_INPUT)
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

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 商品マスタポップアップ画面の分類指定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.hdnSyouhinType.Value = EarthEnum.EnumSyouhinKubun.AllSyouhin

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'マスタ検索系コード入力項目イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSyouhinCd.Attributes("onblur") = "clrName(this,'" & Me.TextHinmei.ClientID & "');"
        Me.TextTysKaisyaCd.Attributes("onblur") = "clrName(this,'" & Me.TextTysKaisyaMei.ClientID & "');"

        Dim onBlurHankakuEisuu As String = "hankakuEisuu(this);"
        '区分
        Me.TextKbn.Attributes("onblur") = onBlurHankakuEisuu
        '番号
        Me.TextHosyousyoNo.Attributes("onblur") = onBlurHankakuEisuu

    End Sub

    ''' <summary>
    ''' 画面項目から検索キーレコードへの値セットを行なう。
    ''' </summary>
    ''' <param name="recKey">抽出レコードクラスのキー</param>
    ''' <remarks></remarks>
    Private Sub SetSearchKeyFromCtrl(ByRef recKey As HannyouSiireDataKeyRecord)

        '商品コード
        recKey.SyouhinCd = IIf(Me.TextSyouhinCd.Value <> String.Empty, Me.TextSyouhinCd.Value, String.Empty)
        '登録年月日 From
        recKey.AddDatetimeFrom = IIf(Me.TextAddDateFrom.Value <> String.Empty, Me.TextAddDateFrom.Value, DateTime.MinValue)
        '登録年月日 To
        recKey.AddDatetimeTo = IIf(Me.TextAddDateTo.Value <> String.Empty, Me.TextAddDateTo.Value, DateTime.MinValue)
        '調査会社コード+事業所コード
        recKey.TysKaisyaCd = IIf(Me.TextTysKaisyaCd.Value <> String.Empty, Me.TextTysKaisyaCd.Value, String.Empty)
        '調査会社名カナ
        recKey.TysKaisyaMeiKana = IIf(Me.TextTysKaisyaMeiKana.Value <> String.Empty, Me.TextTysKaisyaMeiKana.Value, String.Empty)
        '仕入年月日 From
        recKey.SiireDateFrom = IIf(Me.TextSiireDateFrom.Value <> String.Empty, Me.TextSiireDateFrom.Value, DateTime.MinValue)
        '仕入年月日 To
        recKey.SiireDateTo = IIf(Me.TextSiireDateTo.Value <> String.Empty, Me.TextSiireDateTo.Value, DateTime.MinValue)
        '伝票仕入年月日 From
        recKey.DenpyouSiireDateFrom = IIf(Me.TextDenpyouSiireDateFrom.Value <> String.Empty, Me.TextDenpyouSiireDateFrom.Value, DateTime.MinValue)
        '伝票仕入年月日 To
        recKey.DenpyouSiireDateTo = IIf(Me.TextDenpyouSiireDateTo.Value <> String.Empty, Me.TextDenpyouSiireDateTo.Value, DateTime.MinValue)
        '区分
        recKey.Kbn = IIf(Me.TextKbn.Text <> String.Empty, Me.TextKbn.Text, String.Empty)
        '番号
        recKey.Bangou = IIf(Me.TextHosyousyoNo.Text <> String.Empty, Me.TextHosyousyoNo.Text, String.Empty)
        '取消
        recKey.Torikesi = IIf(Me.CheckTorikesiTaisyou.Checked, 0, Integer.MinValue)

    End Sub

    ''' <summary>
    ''' 抽出レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="listResult">抽出レコードクラスのリスト</param>
    ''' <remarks></remarks>
    Private Sub SetCtrlFromDataRec(ByVal listResult As List(Of HannyouSiireRecord))

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

        Dim objTr1 As HtmlTableRow
        Dim objTr2 As HtmlTableRow
        Dim objTdSiireNo As HtmlTableCell               '仕入NO
        Dim objTdTysKaisyaCd As HtmlTableCell           '調査会社コード
        Dim objTdTysKaisyaMei As HtmlTableCell          '調査会社名
        Dim objTdSyouhinCd As HtmlTableCell             '商品コード
        Dim objTdSyouhinMei As HtmlTableCell            '商品名
        Dim objTdTekiyou As HtmlTableCell               '摘要
        Dim objTdSiireGaku As HtmlTableCell             '仕入金額
        Dim objTdSiireDate As HtmlTableCell             '仕入年月日
        Dim objTdDenpyouSiireDate As HtmlTableCell      '伝票仕入年月日
        Dim objTdKbn As HtmlTableCell                   '区分
        Dim objTdBangou As HtmlTableCell                '番号
        Dim objTdSesyumei As HtmlTableCell              '施主名

        Dim objHdnUniqueNo As HtmlInputHidden           'HiddenユニークNO

        '取得した売上データを画面に表示
        Dim dtRec As New HannyouSiireRecord
        Dim rowCnt As Integer = 0 'カウンタ

        '検索結果からセルに格納
        For Each dtRec In listResult

            rowCnt += 1

            'インスタンス化
            objTr1 = New HtmlTableRow
            objTr2 = New HtmlTableRow
            objTdSiireNo = New HtmlTableCell
            objTdTysKaisyaCd = New HtmlTableCell
            objTdTysKaisyaMei = New HtmlTableCell
            objTdSyouhinCd = New HtmlTableCell
            objTdSyouhinMei = New HtmlTableCell
            objTdTekiyou = New HtmlTableCell
            objTdSiireGaku = New HtmlTableCell
            objTdSiireDate = New HtmlTableCell
            objTdDenpyouSiireDate = New HtmlTableCell
            objTdKbn = New HtmlTableCell
            objTdBangou = New HtmlTableCell
            objTdSesyumei = New HtmlTableCell

            objHdnUniqueNo = New HtmlInputHidden

            '値の設定
            objTdSiireNo.InnerHtml = cl.GetDisplayString(dtRec.HanSiireUnqNo, EarthConst.HANKAKU_SPACE)
            objHdnUniqueNo.ID = CTRL_NAME_HIDDEN_UNIQUE_NO & rowCnt
            If objTdSiireNo.InnerHtml = EarthConst.HANKAKU_SPACE Then
                objHdnUniqueNo.Value = "" 'Hiddenにセット
            Else
                objHdnUniqueNo.Value = objTdSiireNo.InnerHtml 'Hiddenにセット
            End If
            objTdSiireNo.Controls.Add(objHdnUniqueNo)
            objTdTysKaisyaCd.InnerHtml = cl.GetDisplayString(dtRec.TysKaisyaCd & dtRec.TysKaisyaJigyousyoCd, EarthConst.HANKAKU_SPACE)
            objTdTysKaisyaMei.InnerHtml = cl.GetDisplayString(dtRec.TysKaisyaMei, EarthConst.HANKAKU_SPACE)
            objTdSyouhinCd.InnerHtml = cl.GetDisplayString(dtRec.SyouhinCd, EarthConst.HANKAKU_SPACE)
            objTdSyouhinMei.InnerHtml = cl.GetDisplayString(dtRec.Hinmei, EarthConst.HANKAKU_SPACE)
            objTdTekiyou.InnerHtml = cl.GetDisplayString(dtRec.Tekiyou, EarthConst.HANKAKU_SPACE)
            objTdSiireGaku.InnerHtml = Format(dtRec.SiireGaku, EarthConst.FORMAT_KINGAKU_2)
            objTdSiireDate.InnerHtml = cl.GetDisplayString(dtRec.SiireDate, EarthConst.HANKAKU_SPACE)
            objTdDenpyouSiireDate.InnerHtml = cl.GetDisplayString(dtRec.DenpyouSiireDate, EarthConst.HANKAKU_SPACE)
            objTdKbn.InnerHtml = cl.GetDisplayString(dtRec.Kbn, EarthConst.HANKAKU_SPACE)
            objTdBangou.InnerHtml = cl.GetDisplayString(dtRec.Bangou, EarthConst.HANKAKU_SPACE)
            objTdSesyumei.InnerHtml = cl.GetDisplayString(dtRec.SesyuMei, EarthConst.HANKAKU_SPACE)

            '各セルの幅設定
            If rowCnt = 1 Then
                objTdSiireNo.Style("width") = widthList1(0)
                objTdTysKaisyaCd.Style("width") = widthList1(1)
                objTdTysKaisyaMei.Style("width") = widthList1(2)
                objTdSyouhinCd.Style("width") = widthList1(3)
                objTdSyouhinMei.Style("width") = widthList1(4)

                objTdSiireGaku.Style("width") = widthList2(0)
                objTdSiireDate.Style("width") = widthList2(1)
                objTdDenpyouSiireDate.Style("width") = widthList2(2)
                objTdKbn.Style("width") = widthList2(3)
                objTdBangou.Style("width") = widthList2(4)
                objTdSesyumei.Style("width") = widthList2(5)
                objTdTekiyou.Style("width") = widthList2(6)
            End If

            'スタイル、クラス設定
            objTdSiireNo.Attributes("class") = "textCenter"
            objTdTysKaisyaCd.Attributes("class") = "textCenter"
            objTdSyouhinCd.Attributes("class") = "textCenter"
            objTdSiireGaku.Attributes("class") = "kingaku"
            objTdSiireDate.Attributes("class") = "date textCenter"
            objTdDenpyouSiireDate.Attributes("class") = "date textCenter"
            objTdKbn.Attributes("class") = "textCenter"
            objTdBangou.Attributes("class") = "textCenter"

            '一覧左側行のID付与と格納
            objTr1.ID = "DataTable_resultTr1_" & rowCnt
            objTr1.Attributes("tabindex") = -1

            With objTr1.Controls
                .Add(objTdSiireNo)
                .Add(objTdTysKaisyaCd)
                .Add(objTdTysKaisyaMei)
                .Add(objTdSyouhinCd)
                .Add(objTdSyouhinMei)
            End With

            '一覧右側行のID付与と格納
            objTr2.ID = "DataTable_resultTr2_" & rowCnt

            If rowCnt = 1 Then
                objTr2.Attributes("tabindex") = Integer.Parse(CheckTorikesiTaisyou.Attributes("tabindex")) + 10
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop(" & DivLeftData.ClientID & ");"
            Else
                '2行目以降はタブ移動なし
                objTr2.Attributes("tabindex") = -1
            End If

            With objTr2.Controls
                .Add(objTdSiireGaku)
                .Add(objTdSiireDate)
                .Add(objTdDenpyouSiireDate)
                .Add(objTdKbn)
                .Add(objTdBangou)
                .Add(objTdSesyumei)
                .Add(objTdTekiyou)
            End With

            TableDataTable1.Controls.Add(objTr1)
            TableDataTable2.Controls.Add(objTr2)

        Next

    End Sub

#End Region

End Class