Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Partial Public Class HanbaiKakakuMasterErrorDetails
    Inherits System.Web.UI.Page

    ''' <summary>販売価格マスタ照会</summary>
    ''' <remarks>販売価格マスタ照会機能を提供する</remarks>
    ''' <history>
    ''' <para>2011/03/01　呉営(大連情報システム部)　新規作成</para>
    ''' </history>
    Private hanbaiKakakuErrLogic As New HanbaiKakakuMasterLogic
    Private commonCheck As New CommonCheck
    Protected scrollHeight As Integer = 0

    ''' <summary>ページロッド</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '権限チェック
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnEigyouKengen As Boolean
        blnEigyouKengen = commonChk.CommonNinnsyou(strUserID, "eigyou_master_kanri_kengen")
        If Not blnEigyouKengen Then
            'エラー画面へ遷移して、エラーメッセージを表示する
            Context.Items("strFailureMsg") = Messages.Instance.MSG020E
            Server.Transfer("CommonErr.aspx")
        End If

        If Not IsPostBack Then
            '参照履歴管理テーブルを登録する。
            commonCheck.SetURL(Me, strUserID)
            '初期化
            Call SetInitData()
        End If
        'javascript作成
        MakeJavascript()
        '｢閉じる｣ボタン処理
        Me.btnClose.Attributes.Add("onClick", "return fncClose();")
        '｢CSV出力｣ボタン処理
        Me.btnCsvOutput.Attributes.Add("onClick", "return fncCsvOut();")

    End Sub
    ''' <summary>CSV出力</summary>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click

        '販売価格エラーデータを取得する
        Dim dtHanbaiKakakuErrCsvInfo As HanbaiKakakuMasterDataSet.HanbaiKakakuErrCSVTableDataTable
        '販売価格エラーCSVデータを取得する
        dtHanbaiKakakuErrCsvInfo = hanbaiKakakuErrLogic.SelHanbaiKakakuErrCsv(ViewState("EdiJouhouDate"), ViewState("SyoriDate"))

        'CSVファイル名設定
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("HanbaiKakakuMasterErrCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSVファイルヘッダ設定
        writer.WriteLine(EarthConst.conHanbaiKakakuErrCsvHeader)

        'CSVファイル内容設定
        For Each row As HanbaiKakakuMasterDataSet.HanbaiKakakuErrCSVTableRow In dtHanbaiKakakuErrCsvInfo
            writer.WriteLine(row.edi_jouhou_sakusei_date, row.gyou_no, SetTimeType(row.syori_datetime), _
                row.aitesaki_syubetu, row.aitesaki_cd, row.aitesaki_mei, row.syouhin_cd, _
                row.syouhin_mei, row.tys_houhou_no, row.tys_houhou, row.torikesi, _
                row.koumuten_seikyuu_gaku, row.koumuten_seikyuu_gaku_henkou_flg, _
                row.jitu_seikyuu_gaku, row.jitu_seikyuu_gaku_henkou_flg, row.koukai_flg, _
                row.add_login_user_id, SetTimeType(row.add_datetime), row.upd_login_user_id, _
                SetTimeType(row.upd_datetime))
        Next

        'CSVファイルダウンロード
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()
    End Sub
    ''' <summary>初期データをセットする</summary>
    Protected Sub SetInitData()

        Dim strSyoriDate As String = String.Empty   '処理日時
        Dim strEdiDate As String = String.Empty     'EDI情報作成日
        If Not Request("sendSearchTerms") Is Nothing Then
            strEdiDate = Split(Request("sendSearchTerms").ToString, "$$$")(2)                   'EDI情報作成日
            strSyoriDate = Split(Request("sendSearchTerms").ToString, "$$$")(0)                 '処理日時
            Me.lblSyoriDate.Text = Left(Split(strSyoriDate, "$$$")(0), 4)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & "/" & strSyoriDate.Substring(4, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & "/" & strSyoriDate.Substring(6, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & " " & strSyoriDate.Substring(8, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & ":" & strSyoriDate.Substring(10, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & ":" & strSyoriDate.Substring(12, 2)   '処理日時
            Me.lblFileMei.Text = Split(Request("sendSearchTerms").ToString, "$$$")(1)           '入力ファイル名
            Me.lblFileMei.ToolTip = Split(Request("sendSearchTerms").ToString, "$$$")(1)        '入力ファイル名タイトル
        Else
            Me.lblSyoriDate.Text = String.Empty     '処理日時
            Me.lblFileMei.Text = String.Empty       '入力ファイル名
        End If

        '検索データを取得する
        Dim dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.HanbaiKakakuErrTableDataTable
        dtHanbaiKakakuInfo = hanbaiKakakuErrLogic.GetHanbaiKakakuErr(strEdiDate, strSyoriDate)
        '検索結果を設定する
        Me.grdBodyLeft.DataSource = dtHanbaiKakakuInfo
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = dtHanbaiKakakuInfo
        Me.grdBodyRight.DataBind()
        '検索結果件数を取得する
        Dim intCount As Integer = hanbaiKakakuErrLogic.GetHanbaiKakakuErrCount(strEdiDate, strSyoriDate)
        '検索結果件数を設定する
        SetKensakuCount(intCount)
        'CSVデータ件数を設定する
        Me.hidCsvCount.Value = intCount
        ViewState("EdiJouhouDate") = strEdiDate
        ViewState("SyoriDate") = strSyoriDate
        'データが0件の場合、エラーメッセージを表示する
        If intCount = 0 Then
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
            Exit Sub
        Else
            '数字列設定
            SetKingaku()
        End If
    End Sub
    ''' <summary>検索結果件数を設定する</summary>
    ''' <param name="intCount">検索結果件数</param>
    Private Sub SetKensakuCount(ByVal intCount As Integer)

        If intCount > 100 Then
            Me.lblCount.Text = "100 / " & intCount
            Me.lblCount.ForeColor = Drawing.Color.Red
            scrollHeight = 100 * 22 + 1
        Else
            Me.lblCount.Text = intCount
            Me.lblCount.ForeColor = Drawing.Color.Black
            scrollHeight = intCount * 22 + 1
        End If

    End Sub
    ''' <summary>数字列設定</summary>
    Public Sub SetKingaku()

        Dim rowCount As Integer

        For rowCount = 0 To Me.grdBodyRight.Rows.Count - 1
            '工務店請求金額を設定する
            If Me.grdBodyRight.Rows(rowCount).Cells(1).Text.ToString.Trim.Equals("&nbsp;") Then
                Me.grdBodyRight.Rows(rowCount).Cells(1).Text = ""
            ElseIf commonCheck.CheckHankaku(Me.grdBodyRight.Rows(rowCount).Cells(1).Text, "", "") = String.Empty Then
                Me.grdBodyRight.Rows(rowCount).Cells(1).Text = FormatNumber(Me.grdBodyRight.Rows(rowCount).Cells(1).Text, 0)
            End If
            '実請求金額を設定する
            If Me.grdBodyRight.Rows(rowCount).Cells(3).Text.ToString.Trim.Equals("&nbsp;") Then
                Me.grdBodyRight.Rows(rowCount).Cells(3).Text = ""
            ElseIf commonCheck.CheckHankaku(Me.grdBodyRight.Rows(rowCount).Cells(3).Text, "", "") = String.Empty Then
                Me.grdBodyRight.Rows(rowCount).Cells(3).Text = FormatNumber(Me.grdBodyRight.Rows(rowCount).Cells(3).Text, 0)
            End If
        Next

    End Sub
    ''' <summary>日付変更</summary>
    ''' <param name="strTime">日付</param>
    ''' <returns>日付(yyyy/MM/dd HH:mm:ss)</returns>
    Private Function SetTimeType(ByVal strTime As String) As String
        If Not strTime.Trim.Equals(String.Empty) Then
            Return CDate(strTime).ToString("yyyy/MM/dd HH:mm:ss")
        Else
            Return String.Empty
        End If
    End Function
    ''' <summary>エラーメッセージ表示</summary>
    ''' <param name="strMessage">エラーメッセージ</param>
    ''' <param name="strObjId">クライアントID</param>
    Private Sub ShowMessage(ByVal strMessage As String, ByVal strObjId As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("	alert('" & strMessage & "');")
            If strObjId <> String.Empty Then
                .AppendLine("   document.getElementById('" & strObjId & "').focus();")
                .AppendLine("   document.getElementById('" & strObjId & "').select();")
            End If
            .AppendLine("}")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
    End Sub
    ''' <summary>Javascript作成</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            'スクロールを設定する
            .AppendLine("function wheel(event){")
            .AppendLine("   var delta = 0;")
            .AppendLine("   if(!event)")
            .AppendLine("       event = window.event;")
            .AppendLine("   if(event.wheelDelta){")
            .AppendLine("       delta = event.wheelDelta/120;")
            .AppendLine("       if(window.opera)")
            .AppendLine("           delta = -delta;")
            .AppendLine("       }else if(event.detail){")
            .AppendLine("           delta = -event.detail/3;")
            .AppendLine("       }")
            .AppendLine("   if(delta)")
            .AppendLine("       handle(delta);")
            .AppendLine("}")
            .AppendLine("function handle(delta){")
            .AppendLine("   var divVscroll=" & divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   if (delta < 0){")
            .AppendLine("       divVscroll.scrollTop = divVscroll.scrollTop + 15;")
            .AppendLine("   }else{")
            .AppendLine("       divVscroll.scrollTop = divVscroll.scrollTop - 15;")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("function fncScrollV(){")
            .AppendLine("   var divbodyleft=" & Me.divBodyLeft.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   divbodyleft.scrollTop = divVscroll.scrollTop;")
            .AppendLine("   divbodyright.scrollTop = divVscroll.scrollTop;")
            .AppendLine("}")
            .AppendLine("function fncScrollH(){")
            .AppendLine("   var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("   var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("   divheadright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("   divbodyright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("}")
            'クリアボタン処理
            .AppendLine("function fncClose(){")
            .AppendLine("   window.close();")
            .AppendLine("   return false;")
            .AppendLine("}")
            'CSV出力ボタンを押下する場合、確認メッセージを表示する。
            .AppendLine("function fncCsvOut(){")
            .AppendLine("   if(document.getElementById('" & Me.hidCsvCount.ClientID & "').value > " & CInt(System.Configuration.ConfigurationManager.AppSettings("CsvDownMax")) & "){")
            .AppendLine("       if(confirm('" & Messages.Instance.MSG013C.Replace("@PARAM1", System.Configuration.ConfigurationManager.AppSettings("CsvDownMax")) & "')){")
            .AppendLine("           document.forms[0].submit();")
            .AppendLine("       }else{")
            .AppendLine("           return false; ")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub

End Class