Partial Public Class SearchNyuukinData
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim userinfo As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager
    Dim cl As New CommonLogic
    Dim sl As New StringLogic
    ''' <summary>
    ''' メッセージクラス
    ''' </summary>
    ''' <remarks></remarks>
    Dim MLogic As New MessageLogic

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
            Me.setDispAction()

            'ボタン押下イベントの設定
            Me.setBtnEvent()

            'フォーカス設定
            Me.TextDenpyouBangouFrom.Focus()

        End If
    End Sub

#Region "ボタンイベント"

    ''' <summary>
    ''' 検索実行時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub search_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles search.ServerClick
        Dim MyLogic As New NyuukinDataSearchLogic
        Dim listResult As List(Of NyuukinDataRecord)

        '入金データテーブルレコードクラス
        Dim recKey As New NyuukinDataKeyRecord

        '●検索条件の取得
        Me.SetSearchKeyFromCtrl(recKey)

        '表示最大件数
        Dim end_count As Integer = maxSearchCount.Value
        Dim total_count As Integer = 0
        Dim intCsvMaxCnt As Integer = Integer.Parse(EarthConst.MAX_CSV_OUTPUT)

        '検索実行
        listResult = MyLogic.GetNyuukinDataInfo(sender, recKey, 1, end_count, total_count)

        ' 検索結果ゼロ件の場合、メッセージを表示
        If total_count = 0 Then
            ' 検索結果ゼロ件の場合、メッセージを表示
            MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
            Me.HiddenCsvOutPut.Value = ""
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

        '表示件数制限処理
        Dim displayCount As String = ""
        displayCount = total_count
        resultCount.Style.Remove("color")
        If Val(maxSearchCount.Value) < total_count Then
            resultCount.Style("color") = "red"
            displayCount = maxSearchCount.Value & " / " & cl.GetDisplayString(total_count)
        End If

        '検索結果件数を設定
        resultCount.InnerHtml = displayCount

        '●画面にセット
        Me.SetCtrlFromDataRec(listResult)

    End Sub

    ''' <summary>
    ''' 請求先検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSeikyuuSakiSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeikyuuSakiSearch.ServerClick
        Dim blnResult As Boolean

        '請求先検索画面呼出
        blnResult = cl.CallSeikyuuSakiSearchWindow(sender, e, Me _
                                                , Me.SelectSeikyuuSakiKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc _
                                                , Me.TextSeikyuuSakiMei, Me.btnSeikyuuSakiSearch)

        '赤色文字変更対応を配列に格納
        Dim objChgColor As Object() = New Object() {Me.SelectSeikyuuSakiKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakiMei, Me.TextTorikesiRiyuu}
        '取消理由取得設定と色替処理
        cl.GetKameitenTorikesiRiyuuMain(Me.SelectSeikyuuSakiKbn.SelectedValue, Me.TextSeikyuuSakiCd.Value, TextTorikesiRiyuu, True, False, objChgColor)

        If blnResult Then
            'フォーカスセット
            setFocusAJ(Me.btnSeikyuuSakiSearch)
        End If

    End Sub

    ''' <summary>
    ''' CSV出力ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonCsv_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenCsv.ServerClick
        'Dim strFileNm As String = String.Empty  '出力ファイル名

        '入金データテーブルレコードクラス
        Dim recKey As New NyuukinDataKeyRecord
        Dim dtCsv As DataTable
        Dim MyLogic As New NyuukinDataSearchLogic

        '●検索条件の取得
        Me.SetSearchKeyFromCtrl(recKey)

        '件数
        Dim total_count As Integer = 0

        '検索実行
        dtCsv = MyLogic.GetNyuukinDataCsv(sender, recKey, total_count)

        ' 検索結果ゼロ件の場合、メッセージを表示
        If total_count = 0 Then
            ' 検索結果ゼロ件の場合、メッセージを表示
            MLogic.AlertMessage(sender, Messages.MSG020E, 0, "kensakuZero")
            Exit Sub
        ElseIf total_count = -1 Then
            ' 検索結果件数が-1の場合、エラーなので、処理終了
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV出力"), 0, "kensakuErr")
            Exit Sub
        End If

        '出力用データテーブルを基に、CSV出力を行なう
        If cl.OutPutFileFromDtTable(EarthConst.FILE_NAME_CSV_NYUUKIN_DATA, dtCsv) = False Then
            ' 出力用文字列がないので、処理終了
            MLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "CSV出力"), 0, "OutputErr")
            Exit Sub
        End If

    End Sub

#End Region

#Region "プライベートメソッド"

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '伝票番号イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextDenpyouBangouFrom.Attributes("onblur") = "this.value = paddingStr(this.value, 5, '0');"
        Me.TextDenpyouBangouTo.Attributes("onblur") = "this.value = paddingStr(this.value, 5, '0');"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'マスタ検索系コード入力項目イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSeikyuuSakiCd.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.TextSeikyuuSakiBrc.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.SelectSeikyuuSakiKbn.Attributes("onblur") = "clrSeikyuuInfo(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)

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
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 実行ボタン関連
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        '検索実行ボタンのイベントハンドラを設定
        Me.btnSearch.Attributes("onclick") = "checkJikkou(0);"

        'CSV出力ボタンのイベントハンドラを設定
        Me.ButtonCsv.Attributes("onclick") = "checkJikkou(1);"

    End Sub

    ''' <summary>
    ''' 画面項目から検索キーレコードへの値セットを行なう。
    ''' </summary>
    ''' <param name="recKey">抽出レコードクラスのキー</param>
    ''' <remarks></remarks>
    Public Sub SetSearchKeyFromCtrl(ByRef recKey As NyuukinDataKeyRecord)

        '伝票番号 From
        recKey.DenNoFrom = IIf(Me.TextDenpyouBangouFrom.Value <> "", Me.TextDenpyouBangouFrom.Value, String.Empty)
        '伝票番号 To 
        recKey.DenNoTo = IIf(Me.TextDenpyouBangouTo.Value <> "", Me.TextDenpyouBangouTo.Value, String.Empty)
        '伝票作成日(登録年月日) From
        recKey.AddDatetimeFrom = IIf(Me.TextAddDateFrom.Value <> "", Me.TextAddDateFrom.Value, DateTime.MinValue)
        '伝票作成日(登録年月日) To
        recKey.AddDatetimeTo = IIf(Me.TextAddDateTo.Value <> "", Me.TextAddDateTo.Value, DateTime.MinValue)
        recKey.NyuukinDateFrom = IIf(Me.TextNyuukinDateFrom.Value <> "", Me.TextNyuukinDateFrom.Value, DateTime.MinValue)
        '入金年月日 To
        recKey.NyuukinDateTo = IIf(Me.TextNyuukinDateTo.Value <> "", Me.TextNyuukinDateTo.Value, DateTime.MinValue)
        '請求先コード
        recKey.SeikyuuSakiCd = IIf(Me.TextSeikyuuSakiCd.Value <> "", Me.TextSeikyuuSakiCd.Value, String.Empty)
        '請求先枝番
        recKey.SeikyuuSakiBrc = IIf(Me.TextSeikyuuSakiBrc.Value <> "", Me.TextSeikyuuSakiBrc.Value, String.Empty)
        '請求先区分
        recKey.SeikyuuSakiKbn = IIf(Me.SelectSeikyuuSakiKbn.SelectedValue <> "", Me.SelectSeikyuuSakiKbn.SelectedValue, String.Empty)
        '請求先カナ名
        recKey.SeikyuuSakiMeiKana = IIf(Me.TextSeikyuuSakiMeiKana.Value <> "", Me.TextSeikyuuSakiMeiKana.Value, String.Empty)
        '最新伝票のみ表示
        recKey.NewDenpyouDisp = Integer.MinValue

    End Sub

    ''' <summary>
    ''' 抽出レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="listResult">抽出レコードクラスのリスト</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal listResult As List(Of NyuukinDataRecord))

        Dim objTr1 As New HtmlTableRow
        Dim objTr2 As New HtmlTableRow

        Dim objTdDenUnqNo As New HtmlTableCell          '伝票ユニークNO
        Dim objTdDenpyouSyubetu As HtmlTableCell        '伝票種別
        Dim objTdSeikyuuSakiCd As HtmlTableCell         '請求先コード
        Dim objTdSeikyuuSakiMei As HtmlTableCell        '請求先名
        Dim objTdNyuukinDate As HtmlTableCell           '入金年月日

        Dim objTdGoukeiGaku As HtmlTableCell            '伝票合計金額
        Dim objTdGenkin As HtmlTableCell                '現金
        Dim objTdKogitte As HtmlTableCell               '小切手
        Dim objTdKouzaFurikae As HtmlTableCell          '口座振替
        Dim objTdFurikomi As HtmlTableCell              '振込
        Dim objTdTegata As HtmlTableCell                '手形
        Dim objTdKyouryokuKaihi As HtmlTableCell        '協力会費
        Dim objTdFurikomiTesuuRyou As HtmlTableCell     '振込手数料
        Dim objTdSousai As HtmlTableCell                '相殺
        Dim objTdNebiki As HtmlTableCell                '値引
        Dim objTdSonota As HtmlTableCell                'その他

        Dim objTdTegataKijitu As HtmlTableCell          '手形期日
        Dim objTdTekiyouMei As HtmlTableCell            '摘要名
        Dim objTdNkTorikomiNo As HtmlTableCell          '入金取込NO

        '取得した売上データを画面に表示
        Dim dtRec As New NyuukinDataRecord
        Dim rowCnt As Integer = 0 'カウンタ

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

        '取得した売上データを画面に表示
        For Each dtRec In listResult

            rowCnt += 1

            'インスタンス化
            objTr1 = New HtmlTableRow
            objTr2 = New HtmlTableRow

            objTdDenUnqNo = New HtmlTableCell              '伝票ユニークNO
            objTdDenpyouSyubetu = New HtmlTableCell        '伝票種別
            objTdSeikyuuSakiCd = New HtmlTableCell         '請求先コード
            objTdSeikyuuSakiMei = New HtmlTableCell        '請求先名
            objTdNyuukinDate = New HtmlTableCell           '入金年月日

            objTdGoukeiGaku = New HtmlTableCell            '伝票合計金額
            objTdGenkin = New HtmlTableCell                '現金
            objTdKogitte = New HtmlTableCell               '小切手
            objTdKouzaFurikae = New HtmlTableCell          '口座振替
            objTdFurikomi = New HtmlTableCell              '振込
            objTdTegata = New HtmlTableCell                '手形
            objTdKyouryokuKaihi = New HtmlTableCell        '協力会費
            objTdFurikomiTesuuRyou = New HtmlTableCell     '振込手数料
            objTdSousai = New HtmlTableCell                '相殺
            objTdNebiki = New HtmlTableCell                '値引
            objTdSonota = New HtmlTableCell                'その他

            objTdTegataKijitu = New HtmlTableCell          '手形期日
            objTdTekiyouMei = New HtmlTableCell            '摘要名
            objTdNkTorikomiNo = New HtmlTableCell          '入金取込NO

            '値の設定
            objTdDenUnqNo.InnerHtml = cl.GetDisplayString(dtRec.DenpyouUniqueNo, EarthConst.HANKAKU_SPACE)
            objTdDenpyouSyubetu.InnerHtml = cl.GetDisplayString(dtRec.DenpyouSyubetu, EarthConst.HANKAKU_SPACE)
            objTdSeikyuuSakiCd.InnerHtml = cl.GetDispSeikyuuSakiCd(dtRec.SeikyuuSakiKbn, dtRec.SeikyuuSakiCd, dtRec.SeikyuuSakiBrc, False)
            objTdSeikyuuSakiMei.InnerHtml = cl.GetDisplayString(dtRec.SeikyuuSakiMei, EarthConst.HANKAKU_SPACE)
            objTdNyuukinDate.InnerHtml = cl.GetDisplayString(dtRec.NyuukinDate, EarthConst.HANKAKU_SPACE)

            objTdGoukeiGaku.InnerHtml = Format(dtRec.DenpyouGoukeiGaku, EarthConst.FORMAT_KINGAKU_2)
            objTdGenkin.InnerHtml = IIf(dtRec.Genkin = Long.MinValue, "0", Format(dtRec.Genkin, EarthConst.FORMAT_KINGAKU_2))
            objTdKogitte.InnerHtml = IIf(dtRec.Kogitte = Long.MinValue, "0", Format(dtRec.Kogitte, EarthConst.FORMAT_KINGAKU_2))
            objTdKouzaFurikae.InnerHtml = IIf(dtRec.KouzaFurikae = Long.MinValue, "0", Format(dtRec.KouzaFurikae, EarthConst.FORMAT_KINGAKU_2))
            objTdFurikomi.InnerHtml = IIf(dtRec.Furikomi = Long.MinValue, "0", Format(dtRec.Furikomi, EarthConst.FORMAT_KINGAKU_2))
            objTdTegata.InnerHtml = IIf(dtRec.Tegata = Long.MinValue, "0", Format(dtRec.Tegata, EarthConst.FORMAT_KINGAKU_2))
            objTdKyouryokuKaihi.InnerHtml = IIf(dtRec.KyouryokuKaihi = Long.MinValue, "0", Format(dtRec.KyouryokuKaihi, EarthConst.FORMAT_KINGAKU_2))
            objTdFurikomiTesuuRyou.InnerHtml = IIf(dtRec.FurikomiTesuuryou = Long.MinValue, "0", Format(dtRec.FurikomiTesuuryou, EarthConst.FORMAT_KINGAKU_2))
            objTdSousai.InnerHtml = IIf(dtRec.Sousai = Long.MinValue, "0", Format(dtRec.Sousai, EarthConst.FORMAT_KINGAKU_2))
            objTdNebiki.InnerHtml = IIf(dtRec.Nebiki = Long.MinValue, "0", Format(dtRec.Nebiki, EarthConst.FORMAT_KINGAKU_2))
            objTdSonota.InnerHtml = IIf(dtRec.Sonota = Long.MinValue, "0", Format(dtRec.Sonota, EarthConst.FORMAT_KINGAKU_2))

            objTdTegataKijitu.InnerHtml = cl.GetDisplayString(dtRec.TegataKijitu, EarthConst.HANKAKU_SPACE)
            If dtRec.TekiyouMei Is Nothing Then
                objTdTekiyouMei.InnerHtml = cl.GetDisplayString(dtRec.TekiyouMei, EarthConst.HANKAKU_SPACE)
            Else
                objTdTekiyouMei.InnerHtml = cl.GetDisplayString(dtRec.TekiyouMei.Trim, EarthConst.HANKAKU_SPACE)
            End If
            objTdNkTorikomiNo.InnerHtml = cl.GetDisplayString(dtRec.NyuukinTorikomiUniqueNo, EarthConst.HANKAKU_SPACE)

            '各セルの幅設定
            If rowCnt = 1 Then
                objTdDenUnqNo.Style("width") = widthList1(0)
                objTdDenpyouSyubetu.Style("width") = widthList1(1)
                objTdSeikyuuSakiCd.Style("width") = widthList1(2)
                objTdSeikyuuSakiMei.Style("width") = widthList1(3)
                objTdNyuukinDate.Style("width") = widthList1(4)

                objTdGoukeiGaku.Style("width") = widthList2(0)
                objTdGenkin.Style("width") = widthList2(1)
                objTdKogitte.Style("width") = widthList2(2)
                objTdKouzaFurikae.Style("width") = widthList2(3)
                objTdFurikomi.Style("width") = widthList2(4)
                objTdTegata.Style("width") = widthList2(5)
                objTdKyouryokuKaihi.Style("width") = widthList2(6)
                objTdFurikomiTesuuRyou.Style("width") = widthList2(7)
                objTdSousai.Style("width") = widthList2(8)
                objTdNebiki.Style("width") = widthList2(9)
                objTdSonota.Style("width") = widthList2(10)

                objTdTegataKijitu.Style("width") = widthList2(11)
                objTdTekiyouMei.Style("width") = widthList2(12)
                objTdNkTorikomiNo.Style("width") = widthList2(13)
            End If

            'スタイル、クラス設定
            objTdDenUnqNo.Attributes("class") = "textCenter"
            objTdDenpyouSyubetu.Attributes("class") = "textCenter"
            If objTdDenpyouSyubetu.InnerHtml = EarthConst.FR Then
                objTdDenpyouSyubetu.Style("color") = "red"
            End If
            objTdSeikyuuSakiCd.Attributes("class") = "textCenter"
            objTdSeikyuuSakiMei.Attributes("class") = ""
            objTdNyuukinDate.Attributes("class") = "date textCenter"

            objTdGoukeiGaku.Attributes("class") = "kingaku"
            objTdGenkin.Attributes("class") = "kingaku"
            objTdKogitte.Attributes("class") = "kingaku"
            objTdKouzaFurikae.Attributes("class") = "kingaku"
            objTdFurikomi.Attributes("class") = "kingaku"
            objTdTegata.Attributes("class") = "kingaku"
            objTdKyouryokuKaihi.Attributes("class") = "kingaku"
            objTdFurikomiTesuuRyou.Attributes("class") = "kingaku"
            objTdSousai.Attributes("class") = "kingaku"
            objTdNebiki.Attributes("class") = "kingaku"
            objTdSonota.Attributes("class") = "kingaku"

            objTdTegataKijitu.Attributes("class") = "date textCenter"
            objTdTekiyouMei.Attributes("class") = ""
            objTdNkTorikomiNo.Attributes("class") = "number"

            objTr1.ID = "DataTable_resultTr1_" & rowCnt
            objTr1.Attributes("tabindex") = -1

            '行にセルをセット1
            With objTr1.Controls
                .Add(objTdDenUnqNo)
                .Add(objTdDenpyouSyubetu)
                .Add(objTdSeikyuuSakiCd)
                .Add(objTdSeikyuuSakiMei)
                .Add(objTdNyuukinDate)
            End With

            objTr2.ID = "DataTable_resultTr2_" & rowCnt
            If rowCnt = 1 Then
                objTr2.Attributes("tabindex") = Integer.Parse(CheckSaisinDenpyou.Attributes("tabindex")) + 10
                objTr2.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop(" & DivLeftData.ClientID & ");"
            Else
                '2行目以降はタブ移動なし
                objTr2.Attributes("tabindex") = -1
            End If

            '行にセルとセット2
            With objTr2.Controls
                .Add(objTdGoukeiGaku)
                .Add(objTdGenkin)
                .Add(objTdKogitte)
                .Add(objTdKouzaFurikae)
                .Add(objTdFurikomi)
                .Add(objTdTegata)
                .Add(objTdKyouryokuKaihi)
                .Add(objTdFurikomiTesuuRyou)
                .Add(objTdSousai)
                .Add(objTdNebiki)
                .Add(objTdSonota)
                .Add(objTdTegataKijitu)
                .Add(objTdTekiyouMei)
                .Add(objTdNkTorikomiNo)
            End With

            'テーブルに行をセット
            TableDataTable1.Controls.Add(objTr1)
            TableDataTable2.Controls.Add(objTr2)

        Next

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
#End Region

End Class