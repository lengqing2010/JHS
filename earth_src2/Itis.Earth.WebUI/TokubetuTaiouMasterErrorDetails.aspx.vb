Imports Itis.Earth.BizLogic

Partial Public Class TokubetuTaiouMasterErrorDetails
    Inherits System.Web.UI.Page

    ''' <summary>加盟店商品調査方法特別対応マスタエラー確認</summary>
    ''' <remarks>加盟店商品調査方法特別対応マスタエラー確認機能を提供する</remarks>
    ''' <history>2011年3月11日　ジン登閣（大連情報システム部）新規作成</history>
    Private tokubetuTaiouMasterLogic As New TokubetuTaiouMasterLogic
    Private commonCheck As New CommonCheck
    Protected scrollHeight As Integer = 0

    ''' <summary>ページロッド</summary> 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '権限チェック
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnEigyouKengen As Boolean
        blnEigyouKengen = commonChk.CommonNinnsyou(strUserID, "eigyou_master_kanri_kengen,kaiseki_master_kanri_kengen")
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
        Me.btnCsvOutput.Attributes.Add("onClick", "if(!fncCsvOut()){return false;}")
    End Sub

    ''' <summary>CSV出力ボタンの処理</summary>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click

        '特別対応エラーCSVデータを取得する
        Dim dtTokubetuTaiouErrorCSV As New Data.DataTable
        dtTokubetuTaiouErrorCSV = tokubetuTaiouMasterLogic.GetTokubetuTaiouErrorCSV(CStr(ViewState("EdiJouhouDate")), CStr(ViewState("SyoriDate")))
        'CSVファイル名設定
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("TokubetuTaiouMasterErrCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSVファイルヘッダ設定
        writer.WriteLine(EarthConst.conTokubetuTaiouErrCsvHeader)

        'CSVファイル内容設定
        For Each row As Data.DataRow In dtTokubetuTaiouErrorCSV.Rows
            writer.WriteLine(row.Item("edi_jouhou_sakusei_date"), row.Item("gyou_no"), Me.SetTimeType(row.Item("syori_datetime").ToString), row.Item("aitesaki_syubetu"), _
                                        row.Item("aitesaki_cd"), row.Item("aitesaki_mei"), row.Item("syouhin_cd"), row.Item("syouhin_mei"), row.Item("tys_houhou_no"), _
                                        row.Item("tys_houhou"), row.Item("tokubetu_taiou_cd"), row.Item("tokubetu_taiou_meisyou"), row.Item("torikesi"), _
                                        row.Item("kasan_syouhin_cd"), row.Item("kasan_syouhin_mei"), row.Item("syokiti"), row.Item("uri_kasan_gaku"), row.Item("koumuten_kasan_gaku"), _
                                        row.Item("add_login_user_id"), Me.SetTimeType(row.Item("add_datetime").ToString), row.Item("upd_login_user_id"), Me.SetTimeType(row.Item("upd_datetime").ToString))
        Next

        'CSVファイルダウンロード
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()

    End Sub

    ''' <summary>
    ''' 日付形式設定
    ''' </summary>
    Private Function SetTimeType(ByVal strTime As String) As String
        If Not strTime.Trim.Equals(String.Empty) Then
            Return CDate(strTime).ToString("yyyy/MM/dd HH:mm:ss")
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' 画面初期化
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub SetInitData()

        Dim strSyoriDate As String = String.Empty   '処理日時
        Dim strEdiDate As String    'EDI情報作成日

        If Not Request("sendSearchTerms") Is Nothing Then
            strSyoriDate = Split(Request("sendSearchTerms").ToString, "$$$")(0)
            Me.lblSyoriDate.Text = Left(Split(strSyoriDate, "$$$")(0), 4)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & "/" & strSyoriDate.Substring(4, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & "/" & strSyoriDate.Substring(6, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & " " & strSyoriDate.Substring(8, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & ":" & strSyoriDate.Substring(10, 2)
            Me.lblSyoriDate.Text = Me.lblSyoriDate.Text & ":" & strSyoriDate.Substring(12, 2) '処理日時
            Me.lblFileMei.Text = Split(Request("sendSearchTerms").ToString, "$$$")(1)   '入力ファイル名
            Me.lblFileMei.ToolTip = Split(Request("sendSearchTerms").ToString, "$$$")(1)
            strEdiDate = Split(Request("sendSearchTerms").ToString, "$$$")(2)           'EDI情報作成日
        Else
            Me.lblSyoriDate.Text = String.Empty     '処理日時
            Me.lblFileMei.Text = String.Empty       '入力ファイル名
            strEdiDate = String.Empty               'EDI情報作成日
        End If

        '検索データを取得する
        Dim dtTokubetuTaiouError As New Data.DataTable
        dtTokubetuTaiouError = tokubetuTaiouMasterLogic.GetTokubetuTaiouError(strEdiDate, strSyoriDate)
        '検索結果を設定する
        Me.grdMeisaiLeft.DataSource = dtTokubetuTaiouError
        Me.grdMeisaiLeft.DataBind()
        Me.grdMeisaiRight.DataSource = dtTokubetuTaiouError
        Me.grdMeisaiRight.DataBind()
        '検索結果件数を取得する
        Dim intCount As Integer = tokubetuTaiouMasterLogic.GetTokubetuTaiouErrorCount(strEdiDate, strSyoriDate)

        '検索結果件数を設定する
        SetKensakuCount(intCount)
        'CSVデータ件数を設定する
        Me.hidCSVCount.Value = intCount
        ViewState("EdiJouhouDate") = strEdiDate
        ViewState("SyoriDate") = strSyoriDate
        'データが0件の場合、エラーメッセージを表示する
        If intCount = 0 Then
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' 検索結果を設定
    ''' </summary>
    Private Sub SetKensakuCount(ByVal intCount As Integer)
        If intCount > 100 Then
            Me.lblCount.Text = "100 / " & CStr(intCount)
            Me.lblCount.ForeColor = Drawing.Color.Red
            scrollHeight = 100 * 22 + 1
        Else
            Me.lblCount.Text = CStr(intCount)
            Me.lblCount.ForeColor = Drawing.Color.Black
            scrollHeight = intCount * 22 + 1
        End If
    End Sub

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
            '閉めるボタンの処理
            .AppendLine("function fncClose(){")
            .AppendLine("   window.close();")
            .AppendLine("   return false;")
            .AppendLine("}")
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
            '.AppendLine("function fncScrollV(){")
            '.AppendLine("   var divbody=" & Me.divMeisai.ClientID & ";")
            '.AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            '.AppendLine("   divbody.scrollTop = divVscroll.scrollTop;")
            '.AppendLine("}")
            .AppendLine("function fncScrollV(){")
            .AppendLine("   var divbodyleft=" & Me.divMeisaiLeft.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divMeisaiRight.ClientID & ";")
            .AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   divbodyleft.scrollTop = divVscroll.scrollTop;")
            .AppendLine("   divbodyright.scrollTop = divVscroll.scrollTop;")
            .AppendLine("}")
            .AppendLine("function fncScrollH(){")
            .AppendLine("   var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divMeisaiRight.ClientID & ";")
            .AppendLine("   var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("   divheadright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("   divbodyright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("}")
            'CSV出力ボタンを押下する場合、確認メッセージを表示する。
            .AppendLine("function fncCsvOut(){")
            .AppendLine("   document.all." & Me.hidCsvOut.ClientID & ".value='';")
            .AppendLine("   if(document.getElementById('" & Me.hidCSVCount.ClientID & "').value > " & CInt(System.Configuration.ConfigurationManager.AppSettings("CsvDownMax")) & "){")
            .AppendLine("       if(confirm('" & Messages.Instance.MSG013C.Replace("@PARAM1", System.Configuration.ConfigurationManager.AppSettings("CsvDownMax")) & "')){")
            .AppendLine("           return ture;")
            .AppendLine("       }else{")
            .AppendLine("           return false; ")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("   else ")
            .AppendLine("   { ")
            .AppendLine("       return true; ")
            .AppendLine("   } ")
            .AppendLine("}")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub

    Private Sub grdBodyRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMeisaiRight.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim str1 As String = CType(e.Row.Cells(5).FindControl("uri_kasan_gaku"), Label).Text
            Dim str2 As String = CType(e.Row.Cells(6).FindControl("koumuten_kasan_gaku"), Label).Text

            If String.IsNullOrEmpty(str1) Then
                CType(e.Row.Cells(5).FindControl("uri_kasan_gaku"), Label).Text = String.Empty
            Else
                CType(e.Row.Cells(5).FindControl("uri_kasan_gaku"), Label).Text = FormatNumber(str1.Trim, 0)
            End If

            If String.IsNullOrEmpty(str2) Then
                CType(e.Row.Cells(6).FindControl("koumuten_kasan_gaku"), Label).Text = String.Empty
            Else
                CType(e.Row.Cells(6).FindControl("koumuten_kasan_gaku"), Label).Text = FormatNumber(str2.Trim, 0)
            End If
        End If
    End Sub
End Class