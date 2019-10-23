
Partial Public Class PopupBukkenSitei
    Inherits System.Web.UI.Page

    Dim JibanLogic As New JibanLogic
    Dim KairyouKoujiLogic As New KairyouKoujiLogic
    Dim KidouType As String = String.Empty

    ''' <summary>
    ''' PageLoad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            ' コンボ設定ヘルパークラスを生成
            Dim helper As New DropDownHelper
            ' 区分コンボにデータをバインドする
            helper.SetDropDownList(SelectKubun, DropDownHelper.DropDownType.Kubun)

            'リクエストから情報取得
            SelectKubun.SelectedValue = Request("kbn")
            If Request("no") <> String.Empty Then
                If Mid(Request("no"), 7) = "9999" Then
                    TextNo.Value = Integer.Parse(Request("no")) - 9998
                Else
                    TextNo.Value = Integer.Parse(Request("no")) + 1
                End If
                'TextNo.Value = Integer.Parse(Request("no")) + 1
            End If
            HiddenOldKubun.Value = Request("kbn")
            HiddenOldNo.Value = Request("no")
            HiddenKidouType.Value = Request("type")
            KidouType = HiddenKidouType.Value

            '画面表示設定
            SetDisplay()

        Else
            KidouType = HiddenKidouType.Value

        End If

    End Sub

    ''' <summary>
    ''' 画面表示設定
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub SetDisplay()

        'コントロール共通設定
        ButtonBukkenOpenExe.Style("display") = "none"
        ButtonCopyOpenExe.Style("display") = "none"
        ButtonCopyOpen.Style("display") = "none"
        TextNo.Attributes("onblur") = "checkNumber(this);"
        SelectKubun.Attributes("onchange") = "objEBI('" & TextNo.ClientID & "').value=''"


        '画面コメント表示切替
        SpanPopupMess1.InnerText = String.Empty
        SpanPopupMess2.InnerText = "区分、番号を指定してください。"

        Select Case KidouType
            Case "irai"
                SpanPopupMess1.InnerText = "登録/修正処理が完了しました。"
                SetFocus(ButtonClose)
            Case "teibetu"
                SelectKubun.SelectedValue = "S"
                SetFocus(TextNo)
            Case "teibetuA"
                SpanPopupMess1.InnerText = "登録/修正処理が完了しました。"
                SetFocus(ButtonClose)
            Case "teinyuu"
                SelectKubun.SelectedValue = "S"
                SetFocus(TextNo)
            Case "teinyuuA"
                SpanPopupMess1.InnerText = "登録/修正処理が完了しました。"
                SetFocus(ButtonClose)
            Case "koujiA"
                SpanPopupMess1.InnerText = "登録/修正処理が完了しました。"
                ButtonCopyOpen.Style("display") = "inline"
                SetFocus(ButtonClose)
        End Select




    End Sub


    ''' <summary>
    ''' 物件画面起動ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonBukkenOpenExe_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBukkenOpenExe.ServerClick

        '表示最大件数
        Dim rec As New JibanKeyRecord
        rec.Kbn1 = SelectKubun.SelectedValue
        rec.HosyousyoNoFrom = TextNo.Value
        rec.HosyousyoNoTo = TextNo.Value
        rec.DataHakiSyubetu = Integer.MinValue
        Dim endCount As Integer = 1
        Dim totalCount As Integer = 0

        '物件検索実行
        Dim resultArray As List(Of JibanSearchRecord) = JibanLogic.GetJibanSearchData(sender, rec, 1, endCount, totalCount)

        If totalCount > 0 Then
            If (KidouType = "teibetu" Or KidouType = "teibetuA") And resultArray(0).DataHakiSyubetu IsNot Nothing Then
                ' 邸別修正起動時は、破棄種別がセット済みの場合、メッセージを表示し、終了
                AlertMessages(sender, Messages.MSG071E)
                Exit Sub
            End If
            '取得できた場合、フラグにTrue文字をセット
            HiddenOkFlg.Value = Boolean.TrueString
        ElseIf totalCount = 0 Then
            ' 検索結果ゼロ件の場合、メッセージを表示し、終了
            AlertMessages(sender, Messages.MSG020E)
            Exit Sub
        ElseIf totalCount = -1 Then
            ' エラーの場合、終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 物件コピーボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonCopyOpenExe_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCopyOpenExe.ServerClick

        '画面情報をキーにコピー先工事データを取得する
        Dim CopySakiKoujiData As KoujiCopyRecord
        CopySakiKoujiData = KairyouKoujiLogic.GetKoujiCopyData(SelectKubun.SelectedValue, TextNo.Value)

        If CopySakiKoujiData.HosyousyoNo Is Nothing Then
            'データが取得できなかった場合、メッセージを表示
            AlertMessages(sender, Messages.MSG020E)
            Exit Sub
        End If

        '------------ 「改良工事」向けコピー機能 ------------'
        'コピー先データ内容チェック
        '  売り上げ処理済みチェック
        If CopySakiKoujiData.UriKeijyouDate <> DateTime.MinValue Then
            '改良工事が売上処理済みの場合、エラーメッセージを表示し、終了
            AlertMessages(sender, Messages.MSG067E)
            Exit Sub
        End If

        'エラーメッセージ用スクリプト
        Dim tmpScript As String = String.Empty

        '画面情報をキーにコピー元工事データを取得する
        Dim CopyMotoKoujiData As KoujiCopyRecord
        CopyMotoKoujiData = KairyouKoujiLogic.GetKoujiCopyData(HiddenOldKubun.Value, HiddenOldNo.Value)

        'コピー元に工事商品が設定されていない場合、警告表示
        If CopyMotoKoujiData.SyouhinCd = String.Empty Then
            If CopySakiKoujiData.HattyuusyoGaku <> Integer.MinValue OrElse CopySakiKoujiData.HattyuusyoGaku = 0 Then
                'コピー元の工事商品が未入力で、コピー先の発注書金額が入力済みの場合、エラーメッセージを表示し、終了
                AlertMessages(sender, Messages.MSG135E)
                Exit Sub
            Else
                tmpScript = "if(confirm('" & Messages.MSG102C & "'))"
            End If
        End If

        '  コピー先項目セット済みチェック
        If CheckKoujiDataSetZumi(CopySakiKoujiData) Then

            '取得できた場合、フラグにTrue文字をセット
            tmpScript += "document.getElementById('" & HiddenOkFlg.ClientID & "').value='" & Boolean.TrueString & "';"

        Else

            '＠<改良工事>完工速報着日が入力されているか否かで、確認メッセージを変える。確認OKの場合、実行ボタン押下。
            If CopySakiKoujiData.KairyKojSokuhouTykDate <> Date.MinValue Then
                tmpScript += "if(confirm('" & Messages.MSG069C & "'))if(confirm('" & Messages.MSG070C & "'))document.getElementById('" & HiddenOkFlg.ClientID & "').value='" & Boolean.TrueString & "';"
            Else
                tmpScript += "if(confirm('" & Messages.MSG069C & "'))document.getElementById('" & HiddenOkFlg.ClientID & "').value='" & Boolean.TrueString & "';"
            End If

        End If

        ScriptManager.RegisterStartupScript(sender, sender.GetType(), "err", tmpScript, True)

    End Sub


    ''' <summary>
    ''' JavaScriptのAlertメッセージ表示用処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="mes"></param>
    ''' <remarks></remarks>
    Protected Sub AlertMessages(ByVal sender As System.Object, ByVal mes As String)

        Dim tmpScript As String = "alert('" & mes & "');"
        ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "err", tmpScript, True)

    End Sub

    ''' <summary>
    ''' コピー先項目セット済みチェック
    ''' </summary>
    ''' <param name="CopySakiKoujiData">チェック対象JibanRecord</param>
    ''' <returns>チェック結果</returns>
    ''' <remarks></remarks>
    Protected Function CheckKoujiDataSetZumi(ByVal CopySakiKoujiData As KoujiCopyRecord) As Boolean

        Dim checkFlg As Boolean = True

        '＠工事担当者.コード
        If CopySakiKoujiData.SyouninsyaCd <> Integer.MinValue Then
            checkFlg = False
        End If

        '＠工事担当者.担当者名
        '  コードのチェックでカバーする

        '＠<改良工事>工事仕様確認
        If CopySakiKoujiData.KojSiyouKakunin <> Integer.MinValue Then
            checkFlg = False
        End If

        '＠<改良工事>確認日
        If CopySakiKoujiData.KojSiyouKakuninDate <> Date.MinValue Then
            checkFlg = False
        End If

        '＠<改良工事>工事会社コード
        If CopySakiKoujiData.KojGaisyaCd <> String.Empty Then
            checkFlg = False
        End If

        '＠<改良工事>工事会社名
        '  コードのチェックでカバーする

        '＠<改良工事>工事会社請求
        If CopySakiKoujiData.KojGaisyaSeikyuuUmu <> Integer.MinValue Then
            checkFlg = False
        End If

        '＠<改良工事>改良工事種別
        If CopySakiKoujiData.KairyKojSyubetu <> Integer.MinValue Then
            checkFlg = False
        End If

        '＠<改良工事>完了予定日
        If CopySakiKoujiData.KairyKojKanryYoteiDate <> Date.MinValue Then
            checkFlg = False
        End If

        '＠<改良工事>完工速報着日
        If CopySakiKoujiData.KairyKojSokuhouTykDate <> Date.MinValue Then
            checkFlg = False
        End If

        ' 改良工事邸別請求データ関連
        '＠<改良工事>商品コード
        If CopySakiKoujiData.SyouhinCd <> String.Empty Then
            checkFlg = False
        End If

        '＠<改良工事>請求
        If CopySakiKoujiData.SeikyuuUmu <> Integer.MinValue Then
            checkFlg = False
        End If

        '＠<改良工事><売上金額>税抜金額
        If CopySakiKoujiData.UriGaku <> Integer.MinValue Then
            checkFlg = False
        End If

        '＠<改良工事><売上金額>消費税
        '＠<改良工事><売上金額>税込金額
        '  <改良工事><売上金額>税抜金額のチェックでカバーする

        '＠<改良工事><仕入金額>税抜金額
        If CopySakiKoujiData.SiireGaku <> Integer.MinValue Then
            checkFlg = False
        End If

        '＠<改良工事><仕入金額>消費税
        '＠<改良工事><仕入金額>税込金額
        '  <改良工事><仕入金額>税抜金額のチェックでカバーする

        Return checkFlg

    End Function

End Class