Public Partial Class PopupSeikyuusyoMiinsatu
    Inherits System.Web.UI.Page

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

    '行コントロールID接頭語
    Private Const CTRL_NAME_TR As String = "resultTr_"

    '共通ロジック
    Private cl As New CommonLogic
    'メッセージクラス
    Private MLogic As New MessageLogic
    'ロジッククラス
    Dim MyLogic As New SeikyuuMiinsatuDataSearchLogic

    '請求データレコードクラス
    Private dtRec As New SeikyuuDataRecord


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

        'スクリプトマネージャーを取得（ScriptManager用）
        masterAjaxSM = Me.SM1

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合
            cl.CloseWindow(Me)
            Exit Sub
        End If

        If IsPostBack = False Then

            '●権限のチェック
            '経理業務権限
            If userinfo.KeiriGyoumuKengen = 0 Then
                cl.CloseWindow(Me)
            End If

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            'なし

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            'フォーカス設定
            BtnClose.Focus()

            '****************************************************************************
            ' 請求書未印刷一覧データ取得
            '****************************************************************************
            SetSearchResult(sender, e)

        Else

        End If
    End Sub

    ''' <summary>
    ''' 請求書未発行データをテーブルに表示する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchResult(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Const CSS_TEXT_CENTER = "textCenter"
        Const CSS_DATE = "date"

        Dim end_count As Integer = 100 '表示最大件数
        Dim total_count As Integer = 0 '取得件数

        '検索実行
        Dim resultArray As New List(Of SeikyuuDataRecord)
        resultArray = MyLogic.GetSeikyuuMiinsatuData(sender, dtRec, 1, end_count, total_count)

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
            displayCount = end_count & " / " & CommonLogic.Instance.GetDisplayString(total_count)
        End If

        '検索結果件数を設定
        resultCount.InnerHtml = displayCount

        '************************
        '* 画面テーブルへ出力
        '************************

        Dim objTr As HtmlTableRow
        Dim objTdSeikyuuSakiCd As HtmlTableCell         '請求先コード
        Dim objTdSeikyuuSakiMei As HtmlTableCell        '請求先名
        Dim objTdSeikyuuSimeDate As HtmlTableCell       '請求締め日
        Dim objTdSeikyuusyoHakDate As HtmlTableCell     '請求書発行日
        Dim objTdSeikyuuSyoYousi As HtmlTableCell       '請求書式

        Dim rowCnt As Integer = 0 'カウンタ

        '検索結果からセルに格納
        For Each dtRec In resultArray

            rowCnt += 1

            'インスタンス化
            objTr = New HtmlTableRow
            objTdSeikyuuSakiCd = New HtmlTableCell
            objTdSeikyuuSakiMei = New HtmlTableCell
            objTdSeikyuuSimeDate = New HtmlTableCell
            objTdSeikyuusyoHakDate = New HtmlTableCell
            objTdSeikyuuSyoYousi = New HtmlTableCell

            '値の設定
            objTdSeikyuuSakiCd.InnerHtml = cl.GetDispSeikyuuSakiCd(dtRec.SeikyuuSakiKbn, dtRec.SeikyuuSakiCd, dtRec.SeikyuuSakiBrc, False)
            objTdSeikyuuSakiMei.InnerHtml = cl.GetDisplayString(dtRec.SeikyuuSakiMei, EarthConst.HANKAKU_SPACE)
            objTdSeikyuuSimeDate.InnerHtml = cl.GetDisplayString(dtRec.SeikyuuSimeDate, EarthConst.HANKAKU_SPACE)
            objTdSeikyuusyoHakDate.InnerHtml = cl.GetDisplayString(dtRec.SeikyuusyoHakDate, EarthConst.HANKAKU_SPACE)
            objTdSeikyuuSyoYousi.InnerHtml = cl.GetDisplayString(dtRec.KaisyuuSeikyuusyoYousiMei, EarthConst.HANKAKU_SPACE)

            'スタイル、クラス設定
            objTdSeikyuuSakiCd.Attributes("class") = CSS_TEXT_CENTER
            objTdSeikyuuSimeDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER
            objTdSeikyuusyoHakDate.Attributes("class") = CSS_DATE & EarthConst.BRANK_STRING & CSS_TEXT_CENTER

            '行IDとJSイベントの付与
            objTr.ID = CTRL_NAME_TR & rowCnt
            If rowCnt = 1 Then
                objTr.Attributes("tabindex") = 0
                objTr.Attributes("onfocus") = "this.firstChild.fireEvent('onmousedown');moveScrollTop();"
            Else
                objTr.Attributes("tabindex") = -1
            End If

            'セルを行に格納
            objTr.Controls.Add(objTdSeikyuuSakiCd)
            objTr.Controls.Add(objTdSeikyuuSakiMei)
            objTr.Controls.Add(objTdSeikyuuSimeDate)
            objTr.Controls.Add(objTdSeikyuusyoHakDate)
            objTr.Controls.Add(objTdSeikyuuSyoYousi)

            '1行を追加
            Me.searchGrid.Controls.Add(objTr)
        Next

    End Sub

End Class