Option Explicit On
Option Strict On

Imports Lixil.JHS_EKKS.BizLogic
Imports Lixil.JHS_EKKS.Utilities
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase

''' <summary>
''' 計画管理 CSV取込
''' </summary>
''' <remarks></remarks>
Partial Class KeikakuKanriInput
    Inherits System.Web.UI.Page

#Region "プライベート変数"
    Public CommonConstBC As Lixil.JHS_EKKS.BizLogic.CommonConstBC
    Private objKeikakuKanriInputBC As New KeikakuKanriInputBC
    Private objCommon As New Common
    Private csvKbn As String
#End Region

#Region "定数"
    Private Const csvKeikaku As String = "B1"           '計画ＣＳＶ取込
    Private Const csvMikomi As String = "B2"            '見込ＣＳＶ取込
    Private Const csvFcKeikaku As String = "B3"         'ＦＣ用計画ＣＳＶ取込
    Private Const csvFcMikomi As String = "B4"          'ＦＣ用見込ＣＳＶ取込
#End Region

#Region "イベント"

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        '権限チェック
        Dim strUserID As String = ""

        Dim CommonCheck As New CommonCheck
        If CommonCheck.CommonNinsyou(strUserID, Master.loginUserInfo, kegen.KeikakuTorikomiKengen) Then
            Me.btnCsvInput.Button.Enabled = True
        Else
            Me.btnCsvInput.Button.Enabled = False
        End If

        If Not IsPostBack Then
            If Not Request.QueryString("csvKbn") Is Nothing AndAlso String.IsNullOrEmpty(hidCsvKbn.Text) Then
                hidCsvKbn.Text = Request.QueryString("csvKbn").ToString
            End If

            '画面を設定
            Call Me.SetGamen()
        End If

        'Javascript作成
        Call Me.MakeJavascript()

        ''DIV非表示
        'CloseCover()

        'CSV取込ボタン
        Me.btnCsvInput.Button.Attributes.Add("onClick", "if(!fncCheckCsvPath()){return false;}")
        '｢閉じる｣ボタン
        Me.btnClose.Attributes.Add("onClick", "fncClose();return false;")

        'CSV取込ボタンのClick事件
        Me.btnCsvInput.OnClick = "btnCsvInput_Click()"

    End Sub

    ''' <summary>
    ''' 「CSV取込」ボタンクリック処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Function btnCsvInput_Click() As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim strUmuFlg As String = String.Empty
        Dim strMessage As String = objKeikakuKanriInputBC.ChkCsvFile(Me.fupCsvInput, hidCsvKbn.Text, strUmuFlg)


        '画面を設定
        Call Me.SetGamen()

        If strMessage = String.Empty Then

            '完了メッセージを表示する
            If strUmuFlg = "1" Then
                SetShowMessage(CommonMessage.MSG040E, String.Empty)
            Else
                SetShowMessage(CommonMessage.MSG039E, String.Empty)
            End If
        Else
            'エラーメッセージを表示する
            SetShowMessage(strMessage, String.Empty)
        End If

        Return True
    End Function

    ''' <summary>
    ''' データ行がデータにバインドされたとき処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Private Sub grdInputKanri_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdInputKanri.RowDataBound
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = CDate(e.Row.Cells(0).Text).ToString("yyyy/MM/dd HH:mm:ss")
            Dim linkButton As Web.UI.WebControls.LinkButton = CType(e.Row.Cells(2).Controls(1), LinkButton)
            If linkButton.Text = "無し" Then
                linkButton.Visible = False
                e.Row.Cells(2).Text = "無し"
            Else
                Dim sendSearchTerms As String = CType(e.Row.Cells(2).Controls(5), HiddenField).Value & "$$$" & CType(e.Row.Cells(1).Controls(1), Label).Text _
                                                & "$$$" & CType(e.Row.Cells(2).Controls(3), HiddenField).Value
                linkButton.Attributes.Add("style", "text-decoration:underline; color:Blue; cursor:hand;")
                linkButton.Attributes.Add("onClick", "fncErrorDetails('" & sendSearchTerms & "');return false;")
            End If
        End If
    End Sub

#End Region

#Region "メンッド"

    ''' <summary>
    ''' クライアント スクリプトを Page オブジェクトに登録する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Private Sub MakeJavascript()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        Dim sbScript As New StringBuilder

        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            'エラー確認画面をポップアップする
            .AppendLine("   function fncErrorDetails(strValue){")
            .AppendLine("       var sendSearchTerms;")
            .AppendLine("       window.open('KeikakuKanriErrorDetails.aspx?sendSearchTerms='+encodeURIComponent(strValue),'ErrorDetails','menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("   }")
            'CLOSEボタン処理
            .AppendLine("   function fncClose(){")
            .AppendLine("       window.close();")
            .AppendLine("   }")
            '「CSV取込」ボタンを押下する場合、入力チェック
            .AppendLine("   function fncCheckCsvPath(){")
            .AppendLine("       var strId = '" & Me.fupCsvInput.ClientID & "';")
            .AppendLine("       var strPath = $ID('" & Me.fupCsvInput.ClientID & "').value;")
            .AppendLine("       if(strPath == ''){")
            .AppendLine("           alert('" & CommonMessage.MSG036E & "');")
            .AppendLine("           $ID(strId).focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '拡張子チェック
            .AppendLine("       if(strPath.toLowerCase().substr(strPath.length-4,strPath.length) != '.csv'){")
            .AppendLine("           alert('" & CommonMessage.MSG037E & "');")
            .AppendLine("           $ID(strId).focus();")
            .AppendLine("           $ID(strId).select();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            'ファイル存在チェック
            .AppendLine("       if(!FileExist(strPath)){")
            .AppendLine("           alert('" & CommonMessage.MSG011E & "');")
            .AppendLine("           $ID(strId).focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            'サイズチェック
            .AppendLine("       if(GetSize(strPath) == 0){")
            .AppendLine("           alert('" & CommonMessage.MSG060E & "');")
            .AppendLine("           $ID(strId).focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '確認メッセージ
            Select Case hidCsvKbn.Text
                Case csvKeikaku
                    .AppendLine("       var strMsg = '" & String.Format(CommonMessage.MSG038C, "@path", "計画管理") & "'")
                Case csvMikomi
                    .AppendLine("       var strMsg = '" & String.Format(CommonMessage.MSG038C, "@path", "予定見込管理") & "'")
                Case csvFcKeikaku
                    .AppendLine("       var strMsg = '" & String.Format(CommonMessage.MSG038C, "@path", "FC用計画管理") & "'")
                Case csvFcMikomi
                    .AppendLine("       var strMsg = '" & String.Format(CommonMessage.MSG038C, "@path", "FC用予定見込管理") & "'")
            End Select
            .AppendLine("       strMsg = strMsg.replace('@path',strPath);")
            .AppendLine("       if(confirm(strMsg)){")
            .AppendLine("           return true;")
            .AppendLine("       }else{ ")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("   }")
            'ファイル存在チェック関数
            .AppendLine("   function FileExist(fPath){")
            .AppendLine("        if( (fPath.indexOf(':') < 0) && (fPath.substr(0,2) != '\\\\') ) ")
            .AppendLine("        { ")
            .AppendLine("           return false; ")
            .AppendLine("        } ")
            .AppendLine("       var sfso=new ActiveXObject(""Scripting.FileSystemObject"");")
            .AppendLine("       if(sfso.FileExists(fPath)){")
            .AppendLine("           sfso=null;return true; ")
            .AppendLine("       }")
            .AppendLine("       else{")
            .AppendLine("           sfso=null;return false; ")
            .AppendLine("       }")
            .AppendLine("   }")
            'ファイルサイズを取得
            .AppendLine("   function GetSize(files){")
            .AppendLine("       var fso,f;")
            .AppendLine("	    var fSize;   ")
            .AppendLine("       fso=new ActiveXObject(""Scripting.FileSystemObject"");")
            .AppendLine("       f=fso.GetFile(files);")
            .AppendLine("       fSize = f.size; ")
            .AppendLine("       sfso=null;f=null; ")
            .AppendLine("	  return fSize; ")
            .AppendLine("   }")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub

    ''' <summary>
    ''' 画面を設定
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Private Sub SetGamen()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        'アップ管理データを取得
        Dim dtInputKanri As New Data.DataTable
        dtInputKanri = objKeikakuKanriInputBC.GetInputKanri()

        If dtInputKanri.Rows.Count > 0 Then
            Me.grdInputKanri.DataSource = dtInputKanri
            Me.grdInputKanri.DataBind()

            '検索結果を設定
            Dim strCount As String = objKeikakuKanriInputBC.GetInputKanriCount()

            If Convert.ToInt64(strCount) > 100 Then
                Me.lblCount.Text = "100/" & strCount
                '赤色
                Me.lblCount.ForeColor = Drawing.Color.Red
            Else
                Me.lblCount.Text = strCount
                '黒色
                Me.lblCount.ForeColor = Drawing.Color.Black
            End If

        Else
            '検索結果を設定
            Me.lblCount.Text = "0"
            '黒色
            Me.lblCount.ForeColor = Drawing.Color.Black
        End If

        'CSV取込ボタンの名称表示
        Select Case hidCsvKbn.Text
            Case csvKeikaku
                btnCsvInput.Button.Text = "計画CSV取込"
            Case csvMikomi
                btnCsvInput.Button.Text = "見込CSV取込"
            Case csvFcKeikaku
                btnCsvInput.Button.Text = "FC用計画CSV取込"
            Case csvFcMikomi
                btnCsvInput.Button.Text = "FC用見込CSV取込"
            Case Else
                btnCsvInput.Button.Text = String.Empty.PadLeft(20, " "c)
                Me.btnCsvInput.Button.Enabled = False
        End Select
    End Sub

    '''' <summary>
    '''' DIV非表示
    '''' </summary>
    '''' <remarks></remarks>
    '''' <history>																
    '''' <para>2012/11/23 P-44979 曹敬仁 新規作成 </para>																															
    '''' </history>
    'Public Sub CloseCover()
    '    'EMAB障害対応情報の格納処理
    '    EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
    '                           MyMethod.GetCurrentMethod.Name)
    '    Dim csScript As New StringBuilder

    '    csScript.AppendLine("<script language='javascript' type='text/javascript'>")
    '    csScript.AppendLine("fncClosecover();")
    '    csScript.AppendLine("</script>")

    '    Page.ClientScript.RegisterStartupScript(Page.GetType, "CloseCover", csScript.ToString)
    'End Sub

    ''' <summary>メッセージ表示</summary>
    ''' <param name="strMessage">メッセージ</param>
    ''' <param name="strObjId">クライアントID</param>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Private Sub SetShowMessage(ByVal strMessage As String, ByVal strObjId As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("	window.name = 'EigyouKeikakuKanriMenu.aspx';setMenuBgColor();")
            .AppendLine("	alert('" & strMessage & "');")
            If strObjId <> String.Empty Then
                .AppendLine("   document.getElementById('" & strObjId & "').focus();")
                .AppendLine("  try{ document.getElementById('" & strObjId & "').select();}catch(e){}")
            End If
            .AppendLine("}")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
    End Sub
#End Region
    
End Class
