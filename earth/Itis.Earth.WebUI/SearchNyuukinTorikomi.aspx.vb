
Partial Public Class SearchNyuukinTorikomi
    Inherits System.Web.UI.Page

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

#Region "入金ファイル取込・行コントロールID接頭語"
    Private Const CTRL_NAME_TR As String = "resultTr_"
    Private Const CTRL_NAME_HIDDEN_NK_TORIKOMI_NO As String = "HiddenNkTorikomiNo_"
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

        If IsPostBack = False Then '初期起動時

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
            Dim kbn_logic As New KubunLogic

            ' 請求先区分コンボにデータをバインドする
            helper.SetKtMeisyouDropDownList(Me.SelectSeikyuuKbn, EarthConst.emKtMeisyouType.SEIKYUUSAKI_KBN, True, False)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            setDispAction()

            Me.TextNyuukinNoFrom.Focus() 'フォーカス

        End If

    End Sub

#Region "プライベートメソッド"

    ''' <summary>
    ''' 入金ファイル取込レコードから画面表示項目への値セットを行なう。
    ''' </summary>
    ''' <param name="listRec">入金ファイル取込レコードのリスト</param>
    ''' <remarks></remarks>
    Public Sub SetCtrlFromDataRec(ByVal listRec As List(Of NyuukinFileTorikomiRecord))

        '行カウンタ
        Dim rowCnt As Integer = 0
        Dim intCnt As Integer = 0

        Dim lngTotalGaku As Long = 0 '総合計金額

        Dim strSpace As String = EarthConst.HANKAKU_SPACE
        Dim intTorikesi As Integer = 0

        Dim objTr As New HtmlTableRow
        Dim objTdReturn As New HtmlTableCell

        Dim objHdnNkTorikomiDenpyouNo As HtmlInputHidden 'Hidden入金取込ユニークNO

        Dim objTdNyuukinTorikomiNo As HtmlTableCell '入金取込No
        Dim objTdEdiInfoMakeDate As HtmlTableCell   'EDI情報作成日
        Dim objTdTorikesi As HtmlTableCell          '取消
        Dim objTdNyuukinDate As HtmlTableCell       '入金日
        Dim objTdDenpyouNo As HtmlTableCell         '伝票番号
        Dim objTdSeikyuuSakiCd As HtmlTableCell     '請求先コード
        Dim objTdSeikyuuSakiMei As HtmlTableCell    '請求先名
        Dim objTdSyougouKouzaNo As HtmlTableCell    '照合口座No.
        Dim objTdDenpyouGoukeiKingaku As HtmlTableCell '伝票合計金額

        'EDI情報作成日時
        Dim dtEdiDate As New Date

        '取得した売上データを画面に表示
        For intCnt = 0 To listRec.Count - 1

            rowCnt += 1

            objTr = New HtmlTableRow
            objTdReturn = New HtmlTableCell

            objHdnNkTorikomiDenpyouNo = New HtmlInputHidden 'Hidden入金取込ユニークNO

            objTdNyuukinTorikomiNo = New HtmlTableCell  '入金取込No
            objTdEdiInfoMakeDate = New HtmlTableCell    'EDI情報作成日
            objTdTorikesi = New HtmlTableCell           '取消
            objTdNyuukinDate = New HtmlTableCell        '入金日
            objTdDenpyouNo = New HtmlTableCell          '取込伝票番号
            objTdSeikyuuSakiCd = New HtmlTableCell      '請求先コード
            objTdSeikyuuSakiMei = New HtmlTableCell     '請求先名
            objTdSyougouKouzaNo = New HtmlTableCell     '照合口座No.
            objTdDenpyouGoukeiKingaku = New HtmlTableCell  '伝票合計金額

            '検索結果配列からセルに格納
            objTdNyuukinTorikomiNo.InnerHtml = cl.GetDisplayString(listRec(intCnt).NyuukinTorikomiUniqueNo, strSpace)
            objHdnNkTorikomiDenpyouNo.ID = CTRL_NAME_HIDDEN_NK_TORIKOMI_NO & rowCnt
            If objTdNyuukinTorikomiNo.InnerHtml = strSpace Then
                objHdnNkTorikomiDenpyouNo.Value = "" 'Hiddenにセット
            Else
                objHdnNkTorikomiDenpyouNo.Value = objTdNyuukinTorikomiNo.InnerHtml 'Hiddenにセット
            End If
            objTdNyuukinTorikomiNo.Controls.Add(objHdnNkTorikomiDenpyouNo)
            objTdEdiInfoMakeDate.InnerHtml = cl.GetDisplayString(listRec(intCnt).EdiJouhouSakuseiDate, strSpace)
            intTorikesi = cl.GetDisplayString(listRec(intCnt).Torikesi)
            If intTorikesi = 0 Then
                objTdTorikesi.InnerHtml = strSpace
            Else
                objTdTorikesi.InnerHtml = EarthConst.TORIKESI
            End If
            objTdNyuukinDate.InnerHtml = cl.GetDisplayString(listRec(intCnt).NyuukinDate, strSpace)
            objTdDenpyouNo.InnerHtml = cl.GetDisplayString(listRec(intCnt).TorikomiDenpyouNo, strSpace)
            objTdSeikyuuSakiCd.InnerHtml = cl.GetDispSeikyuuSakiCd(listRec(intCnt).SeikyuuSakiKbn, listRec(intCnt).SeikyuuSakiCd, listRec(intCnt).SeikyuuSakiBrc, False)
            objTdSeikyuuSakiMei.InnerHtml = cl.GetDisplayString(listRec(intCnt).SeikyuuSakiMei, strSpace)
            objTdSyougouKouzaNo.InnerHtml = cl.GetDisplayString(listRec(intCnt).SyougouKouzaNo, strSpace)
            objTdDenpyouGoukeiKingaku.InnerHtml = Format(listRec(intCnt).DenpyouGoukeiGaku, EarthConst.FORMAT_KINGAKU_2)

            lngTotalGaku += listRec(intCnt).DenpyouGoukeiGaku '総合計金額を加算

            'スタイル、クラス設定
            objTdNyuukinTorikomiNo.Attributes("class") = "number"
            objTdEdiInfoMakeDate.Attributes("class") = "date textCenter"
            objTdTorikesi.Attributes("class") = "textCenter"
            objTdNyuukinDate.Attributes("class") = "date textCenter"
            objTdDenpyouNo.Attributes("class") = "textCenter"
            objTdSeikyuuSakiCd.Attributes("class") = ""
            objTdSyougouKouzaNo.Attributes("class") = ""
            objTdDenpyouGoukeiKingaku.Attributes("class") = "kingaku"

            '行IDとJSイベントの付与
            objTr.ID = CTRL_NAME_TR & rowCnt
            If rowCnt = 1 Then
                objTr.Attributes("tabindex") = 0
                objTr.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                objTr.Attributes("tabindex") = -1
            End If

            'セルを行に格納
            objTr.Controls.Add(objTdNyuukinTorikomiNo)
            objTr.Controls.Add(objTdEdiInfoMakeDate)
            objTr.Controls.Add(objTdTorikesi)
            objTr.Controls.Add(objTdNyuukinDate)
            objTr.Controls.Add(objTdDenpyouNo)
            objTr.Controls.Add(objTdSeikyuuSakiCd)
            objTr.Controls.Add(objTdSeikyuuSakiMei)
            objTr.Controls.Add(objTdSyougouKouzaNo)
            objTr.Controls.Add(objTdDenpyouGoukeiKingaku)

            '1行を追加
            Me.searchGrid.Controls.Add(objTr)

        Next

        '総合計金額を設定
        Me.TdTotalKingaku.InnerHtml = Format(lngTotalGaku, EarthConst.FORMAT_KINGAKU_2)

    End Sub

    ''' <summary>
    ''' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDispAction()
        '++++++++++
        '入金取込NO
        '++++++++++
        Me.TextNyuukinNoFrom.Attributes("onblur") = "if(checkNumber(this)) checkMinus(this);"
        Me.TextNyuukinNoTo.Attributes("onblur") = "if(checkNumber(this)) checkMinus(this);"
        '++++++++++
        '伝票番号
        '++++++++++
        Me.TextTorikomiDenpyouNoFrom.Attributes("onblur") = "if(checkNumber(this)){this.value = paddingStr(this.value, 6, '0');}"
        Me.TextTorikomiDenpyouNoTo.Attributes("onblur") = "if(checkNumber(this)){this.value = paddingStr(this.value, 6, '0');}"
        '++++++++++
        '入金日
        '++++++++++
        Me.TextNyuukinDateFrom.Attributes("onblur") = "checkDate(this);"
        Me.TextNyuukinDateTo.Attributes("onblur") = "checkDate(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        'マスタ検索系コード入力項目イベントハンドラ設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Me.TextSeikyuuSakiCd.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.TextSeikyuuSakiBrc.Attributes("onblur") = "clrSeikyuuInfo(this);"
        Me.SelectSeikyuuKbn.Attributes("onblur") = "clrSeikyuuInfo(this);"

        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        ' 取消理由テキストボックスのスタイルを設定
        '+++++++++++++++++++++++++++++++++++++++++++++++++++
        cl.chgVeiwMode(Me.TextTorikesiRiyuu, Nothing, True)

    End Sub

    ''' <summary>
    ''' 画面項目から検索キーレコードへの値セットを行なう。
    ''' </summary>
    ''' <param name="recKey">抽出レコードクラスのキー</param>
    ''' <remarks></remarks>
    Public Sub SetSearchKeyFromCtrl(ByRef recKey As NyuukinFileTorikomiKeyRecord)

        '入金取込NO_FROM
        recKey.NyuukinTorikomiNoFrom = IIf(Me.TextNyuukinNoFrom.Value <> String.Empty, Me.TextNyuukinNoFrom.Value, Integer.MinValue)
        '入金取込NO_TO
        recKey.NyuukinTorikomiNoTo = IIf(Me.TextNyuukinNoTo.Value <> String.Empty, Me.TextNyuukinNoTo.Value, Integer.MinValue)
        '取込伝票NO_FROM
        recKey.TorikomiDenpyouNoFrom = IIf(Me.TextTorikomiDenpyouNoFrom.Value <> String.Empty, Me.TextTorikomiDenpyouNoFrom.Value, String.Empty)
        '取込伝票NO_TO
        recKey.TorikomiDenpyouNoTo = IIf(Me.TextTorikomiDenpyouNoTo.Value <> String.Empty, Me.TextTorikomiDenpyouNoTo.Value, String.Empty)
        '入金日_FROM
        recKey.NyuukinDateFrom = IIf(Me.TextNyuukinDateFrom.Value <> String.Empty, Me.TextNyuukinDateFrom.Value, DateTime.MinValue)
        '入金日_TO
        recKey.NyuukinDateTo = IIf(Me.TextNyuukinDateTo.Value <> String.Empty, Me.TextNyuukinDateTo.Value, DateTime.MinValue)
        'EDI情報作成日
        recKey.EdiJouhouSakuseiDate = IIf(Me.TextEdiJouhouSakuseiDate.Value <> String.Empty, Me.TextEdiJouhouSakuseiDate.Value, String.Empty)
        '請求先コード
        recKey.SeikyuuSakiCd = IIf(Me.TextSeikyuuSakiCd.Value <> "", Me.TextSeikyuuSakiCd.Value, String.Empty)
        '請求先枝番
        recKey.SeikyuuSakiBrc = IIf(Me.TextSeikyuuSakiBrc.Value <> "", Me.TextSeikyuuSakiBrc.Value, String.Empty)
        '請求先区分
        recKey.SeikyuuSakiKbn = IIf(Me.SelectSeikyuuKbn.SelectedValue <> "", Me.SelectSeikyuuKbn.SelectedValue, String.Empty)
        '取消
        recKey.Torikesi = IIf(Me.CheckBoxTorikesiTaisyou.Checked, 0, Integer.MinValue)

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

#Region "ボタンイベント"

    ''' <summary>
    ''' 検索実行時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonHiddenSearch_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHiddenSearch.ServerClick
        Dim NkLogic As New NyuukinTorikomiLogic
        Dim rec As New NyuukinFileTorikomiKeyRecord
        Dim listNkTorikomi As New List(Of NyuukinFileTorikomiRecord)

        '●検索条件の取得
        Me.SetSearchKeyFromCtrl(rec)

        '表示最大件数
        Dim end_count As Integer = IIf(maxSearchCount.Value <> "max", maxSearchCount.Value, EarthConst.MAX_RESULT_COUNT)
        Dim total_count As Integer = 0

        '●該当データをDBから取得
        listNkTorikomi = NkLogic.getNyuukinFileTorikomiList(sender, rec, 1, end_count, total_count)

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

        '●テーブルにセット
        Me.SetCtrlFromDataRec(listNkTorikomi)

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
                                                        , Me.SelectSeikyuuKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc _
                                                        , Me.TextSeikyuuSakiMei, Me.btnSeikyuuSakiSearch)

        '赤色文字変更対応を配列に格納
        Dim objChgColor As Object() = New Object() {Me.SelectSeikyuuKbn, Me.TextSeikyuuSakiCd, Me.TextSeikyuuSakiBrc, Me.TextSeikyuuSakiMei, Me.TextTorikesiRiyuu}
        '取消理由取得設定と色替処理
        cl.GetKameitenTorikesiRiyuuMain(Me.SelectSeikyuuKbn.SelectedValue, Me.TextSeikyuuSakiCd.Value, Me.TextTorikesiRiyuu, True, False, objChgColor)

        If blnResult Then
            'フォーカスセット
            setFocusAJ(Me.btnSeikyuuSakiSearch)
        End If

    End Sub

#End Region

End Class