Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class GenkaMasterErrorDetails
    Inherits System.Web.UI.Page

    ''' <summary>原価マスタ照会</summary>
    ''' <remarks>原価マスタ照会機能を提供する</remarks>
    ''' <history>
    ''' <para>2011/03/01　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Private genkaMasterLogic As New GenkaMasterLogic
    Private commonCheck As New CommonCheck
    Protected scrollHeight As Integer = 0

    ''' <summary>ページロッド</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '権限チェック
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnEigyouKengen As Boolean
        blnEigyouKengen = commonChk.CommonNinnsyou(strUserID, "kaiseki_master_kanri_kengen")
        If Not blnEigyouKengen Then
            'エラー画面へ遷移して、エラーメッセージを表示する
            Context.Items("strFailureMsg") = Messages.Instance.MSG020E
            Server.Transfer("CommonErr.aspx")
        End If

        'javascript作成
        MakeJavascript()

        If Not IsPostBack Then
            '参照履歴管理テーブルを登録する。
            commonCheck.SetURL(Me, strUserID)
            '初期化
            Call SetInitData()

        Else

            ''CSV出力ボタンを押下する場合
            'If Me.hidCsvOut.Value = "1" Then

            '    'CSV出力
            '    Call CsvOutPut()
            'End If
            ''DIV非表示
            'CloseCover()
        End If

        '｢閉じる｣ボタン処理
        Me.btnClose.Attributes.Add("onClick", "return fncClose();")
        '｢CSV出力｣ボタン処理
        Me.btnCsvOutput.Attributes.Add("onClick", "if(!fncCsvOut()){return false;}")

    End Sub
    ''' <summary>CSV出力</summary>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click
        'Me.hidCsvOut.Value = "1"
        'ClientScript.RegisterStartupScript(Me.GetType, "", "<script language=javascript>document.forms[0].submit();</script>")

        'CSV出力
        Call CsvOutPut()
    End Sub

    '''' <summary>CSV出力</summary>
    Private Sub CsvOutPut()
        '販売価格エラーデータを取得する
        Dim dtGenkaErrCsvInfo As New Data.DataTable
        '販売価格エラーCSVデータを取得する
        dtGenkaErrCsvInfo = genkaMasterLogic.SelGenkaErrCsv(CStr(ViewState("EdiJouhouDate")), CStr(ViewState("SyoriDate")))

        'CSVファイル名設定
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("GenkaMasterErrCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSVファイルヘッダ設定
        writer.WriteLine(EarthConst.conGenkaErrCsvHeader)

        'CSVファイル内容設定
        For i As Integer = 0 To dtGenkaErrCsvInfo.Rows.Count - 1
            With dtGenkaErrCsvInfo.Rows(i)
                writer.WriteLine(.Item(0), .Item(1), .Item(2), .Item(3), .Item(4), .Item(5), .Item(6), .Item(7), .Item(8), .Item(9), _
                                 .Item(10), .Item(11), .Item(12), .Item(13), .Item(14), .Item(15), .Item(16), .Item(17), .Item(18), .Item(19), _
                                 .Item(20), .Item(21), .Item(22), .Item(23), .Item(24), .Item(25), .Item(26), .Item(27), .Item(28), .Item(29), _
                                 .Item(30), .Item(31), .Item(32), .Item(33), .Item(34), .Item(35), .Item(36), .Item(37), .Item(38), .Item(39), _
                                 .Item(40), .Item(41), .Item(42), .Item(43), .Item(44), Me.SetTimeType(.Item(45).ToString), .Item(46), Me.SetTimeType(.Item(47).ToString))
            End With
        Next

        'CSVファイルダウンロード
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()
    End Sub

    Private Function SetTimeType(ByVal strTime As String) As String
        If Not strTime.Trim.Equals(String.Empty) Then
            Return CDate(strTime).ToString("yyyy/MM/dd HH:mm:ss")
        Else
            Return String.Empty
        End If
    End Function
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
        Dim dtGenkaInfo As New Data.DataTable
        dtGenkaInfo = genkaMasterLogic.GetGenkaErr(strEdiDate, strSyoriDate)
        '検索結果を設定する
        Me.grdBodyLeft.DataSource = dtGenkaInfo
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = dtGenkaInfo
        Me.grdBodyRight.DataBind()
        '検索結果件数を取得する
        Dim intCount As Integer = genkaMasterLogic.GetGenkaErrCount(strEdiDate, strSyoriDate)
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
            Call Me.SetKingaku()
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
            .AppendLine("   document.all." & Me.hidCsvOut.ClientID & ".value='';")
            .AppendLine("   if(document.getElementById('" & Me.hidCsvCount.ClientID & "').value > " & CInt(System.Configuration.ConfigurationManager.AppSettings("CsvDownMax")) & "){")
            .AppendLine("       if(confirm('" & Messages.Instance.MSG013C.Replace("@PARAM1", System.Configuration.ConfigurationManager.AppSettings("CsvDownMax")) & "')){")
            .AppendLine("           return true;")
            .AppendLine("       }else{")
            .AppendLine("           return false; ")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("   else ")
            .AppendLine("   { ")
            .AppendLine("       return true; ")
            .AppendLine("   } ")
            .AppendLine("}")

            'DIV表示
            .AppendLine("   function fncShowModal(){")
            .AppendLine("      var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("      var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("      if(buyDiv.style.display=='none')")
            .AppendLine("      {")
            .AppendLine("       buyDiv.style.display='';")
            .AppendLine("       disable.style.display='';")
            .AppendLine("       disable.focus();")
            .AppendLine("      }else{")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("      }")
            .AppendLine("   }")
            'DIV非表示
            .AppendLine("   function fncClosecover(){")
            .AppendLine("       var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("       var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            '.AppendLine("alert('fncClosecover');")
            .AppendLine("   }")
            .AppendLine("</script>")

        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub

    ''' <summary>DIV非表示</summary>
    Public Sub CloseCover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat("fncClosecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseCover", csScript.ToString, True)
    End Sub


    ''' <summary>数字列設定</summary>
    Public Sub SetKingaku()

        Dim numIndex() As Integer = {1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29}

        Dim rowCount As Integer

        For rowCount = 0 To Me.grdBodyRight.Rows.Count - 1
            For Each i As Integer In numIndex
                If Me.grdBodyRight.Rows(rowCount).Cells(i).Text.ToString.Trim.Equals("&nbsp;") Then
                    Me.grdBodyRight.Rows(rowCount).Cells(i).Text = ""
                ElseIf commonCheck.CheckHankaku(Me.grdBodyRight.Rows(rowCount).Cells(i).Text, "", "") = String.Empty Then
                    Me.grdBodyRight.Rows(rowCount).Cells(i).Text = FormatNumber(Me.grdBodyRight.Rows(rowCount).Cells(i).Text, 0)
                End If
            Next
        Next

    End Sub

End Class