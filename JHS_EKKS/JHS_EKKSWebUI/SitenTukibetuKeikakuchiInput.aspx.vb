Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.BizLogic
Imports Lixil.JHS_EKKS.Utilities
Imports System.Data

''' <summary>
''' 「支店 月別計画値 ＣＳＶ取込」画面
''' </summary>
''' <remarks></remarks>
''' <history>
''' <para>2012/11/26 P-44979 王莎莎 新規作成 </para>
''' </history>
Partial Class SitenTukibetuKeikakuchiInput
    Inherits System.Web.UI.Page

#Region "プライベート変数"

    Public objSitenTukibetuKeikakuchiInputBC As New SitenTukibetuKeikakuchiInputBC '支店 月別計画値 ＣＳＶ取込BC
    Dim objCommonCheck As New CommonCheck
    Private objCommon As New Common
    Public CommonConstBC As Lixil.JHS_EKKS.BizLogic.CommonConstBC

#End Region

#Region "定数"

#End Region

#Region "イベント"

    ''' <summary>
    ''' Page_Load
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/26 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '画面初期表示
        SetGamenData()

        '「ＣＳＶ取込」ボタン使用状態セット
        If objCommonCheck.CommonNinsyou(String.Empty, Master.loginUserInfo, kegen.SitenbetuGetujiKeikakuTorikomi) Then
            Me.btnExcelInput.Button.Enabled = True
        Else
            Me.btnExcelInput.Button.Enabled = False
        End If

        'CSV取込ボタン
        Me.btnExcelInput.Button.Attributes.Add("onClick", "if(!fncCheckExcelPath()){return false;}")
        '｢閉じる｣ボタン
        Me.btnClose.Attributes.Add("onClick", "window.close();return false;")
        'CSV取込ボタンのClick事件
        Me.btnExcelInput.OnClick = "btnExcelInput_Click()"

        MakeJavascript()

    End Sub

    ''' <summary>
    ''' RowDataBound
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/26 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Protected Sub grdInputKanri_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdInputKanri.RowDataBound
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = CDate(e.Row.Cells(0).Text).ToString("yyyy/MM/dd HH:mm:ss")

            Dim linkButton As Web.UI.WebControls.LinkButton = CType(e.Row.Cells(2).Controls(1), LinkButton)
            If linkButton.Text = "無し" OrElse linkButton.Text = "なし" Then
                linkButton.Visible = False
                e.Row.Cells(2).Text = "無し"

            Else
                linkButton.Attributes.Add("style", "text-decoration:underline; color:Blue; cursor:hand;")
                linkButton.Attributes.Add("onClick", "fncErrorDetails('" & _
                                                                CType(e.Row.Cells(2).Controls(5), HiddenField).Value & "','" & _
                                                                CType(e.Row.Cells(1).Controls(1), Label).Text & "','" & _
                                                                CType(e.Row.Cells(2).Controls(3), HiddenField).Value & _
                                                      "');return false;")
            End If
        End If

    End Sub

#End Region

#Region "メンッド"

    ''' <summary>
    ''' 画面初期表示
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/26 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Private Sub SetGamenData()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '画面明細データ取得処理
        Dim dtMeisaiData As Data.DataTable = objSitenTukibetuKeikakuchiInputBC.SelTUploadKanri("1")
        '画面明細データ件数取得処理
        Dim strMeisaiCount As String = objSitenTukibetuKeikakuchiInputBC.SelTUploadKanri("").Rows(0).Item(0).ToString

        If strMeisaiCount > 0 Then
            '明細データバイト
            Me.grdInputKanri.DataSource = dtMeisaiData
            Me.grdInputKanri.DataBind()

            '検索結果件数セット
            If strMeisaiCount > 100 Then
                Me.lblCount.Text = "100/" & strMeisaiCount
                Me.lblCount.ForeColor = Drawing.Color.Red
            Else
                Me.lblCount.Text = strMeisaiCount
                Me.lblCount.ForeColor = Drawing.Color.Black
            End If

        Else
            '明細データバイト
            Me.grdInputKanri.DataSource = Nothing
            Me.grdInputKanri.DataBind()

            '検索結果件数セット
            Me.lblCount.Text = "0"
            Me.lblCount.ForeColor = Drawing.Color.Black

        End If

        'CSV取込テキストボックス フォーカスON
        Me.fupExcelInput.Focus()

    End Sub

    ''' <summary>
    ''' 「CSV取込」ボタンクリック処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Function btnExcelInput_Click() As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim strUmuFlg As String = String.Empty
        Dim strMessage As String = objSitenTukibetuKeikakuchiInputBC.ChkCsvFile(Me.fupExcelInput, strUmuFlg)


        '画面を設定
        Call Me.SetGamenData()

        If strMessage = String.Empty Then

            '完了メッセージを表示する
            If strUmuFlg = "1" Then
                objCommon.SetShowMessage(Me.Page, CommonMessage.MSG040E)
            Else
                objCommon.SetShowMessage(Me.Page, CommonMessage.MSG039E)
            End If
        Else
            'todo
            objCommon.SetShowMessage(Me.Page, strMessage)
        End If

        Return True
    End Function

    ''' <summary>
    ''' クライアント スクリプトを Page オブジェクトに登録する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/26 P-44979 王莎莎 新規作成 </para>																															
    ''' </history>
    Private Sub MakeJavascript()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim sbScript As New StringBuilder

        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            'エラー確認画面をポップアップする
            .AppendLine("   function fncErrorDetails(strSyoriDatetime,strFileMei,strEdiJouhouSakuseiDate){")
            .AppendLine("       var sendSearchTerms;")
            .AppendLine("       window.open('SitenTukibetuKeikakuchiErrorDetails.aspx?sendSearchTerms='+strSyoriDatetime+'$$$'+encodeURIComponent(strFileMei)+'$$$'+strEdiJouhouSakuseiDate, '')")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '「EXCEL取込」ボタンを押下する場合、入力チェック
            .AppendLine("   function fncCheckExcelPath(){")
            .AppendLine("       var strId = '" & Me.fupExcelInput.ClientID & "';")
            .AppendLine("       var strPath = $ID('" & Me.fupExcelInput.ClientID & "').value;")
            .AppendLine("       if(strPath == ''){")
            .AppendLine("           alert('" & CommonMessage.MSG036E & "');")
            .AppendLine("           $ID(strId).focus();")
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
            .AppendLine("           alert('" & CommonMessage.MSG063E & "');")
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
            '確認メッセージ
            .AppendLine("   var strMsg = '" & CommonMessage.MSG038C.Replace("{1}", "支店別月別計画管理") & "'")
            .AppendLine("   strMsg = strMsg.replace('{0}',strPath);")
            '.AppendLine("       var strMsg = '" & String.Format(CommonMessage.MSG038C, CType(Me.fupExcelInput.FindControl("fupExcelInput"), TextBox).Text.Trim, "支店別月別計画管理") & "'")
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
            .AppendLine("       var sfso=new ActiveXObject('Scripting.FileSystemObject');")
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
            .AppendLine("       fso=new ActiveXObject('Scripting.FileSystemObject');")
            .AppendLine("       f=fso.GetFile(files);")
            .AppendLine("       fSize = f.size; ")
            .AppendLine("       sfso=null;f=null; ")
            .AppendLine("	  return fSize; ")
            .AppendLine("   }")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub

#End Region

End Class
