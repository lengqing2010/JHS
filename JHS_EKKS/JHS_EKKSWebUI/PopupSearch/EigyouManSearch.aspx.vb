Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.Utilities.CommonMessage
Imports Lixil.JHS_EKKS.BizLogic
Imports System.Data.DataTable
Imports System.Collections.Generic

Partial Class EigyouManSearch
    Inherits System.Web.UI.Page
#Region "プライベート変数"

    Private eigyouManSearchBC As New Lixil.JHS_EKKS.BizLogic.EigyouManSearchBC

#End Region

#Region "イベント"

    ''' <summary>
    ''' 画面の初期表示
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2012/11/15　趙冬雪(大連情報システム部)　新規作成</history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        'JavaScript
        Call MakeJavaScript()

        If Not IsPostBack Then

            If Not Request.QueryString("strEigyouManCd") Is Nothing Then
                Me.tbxUserCd.Text = Request.QueryString("strEigyouManCd") '前画面．営業担当者
            End If
            Me.tbxSiMei.Text = Request.QueryString("strEigyouManMei") '前画面．営業担当者名

            '"▲"と"▼"リンクは表示しない
            '"▲"と"▼"リンクは表示しない

            Me.lnkBtnUserIdUp.Visible = False
            Me.lnkBtnUserIdDown.Visible = False
            Me.lnkBtnUserNameUp.Visible = False
            Me.lnkBtnUserNameDown.Visible = False
            Me.lnkBtnTorikesiUp.Visible = False
            Me.lnkBtnTorikesiDown.Visible = False

            '検索上限件数DropdownListを設定する
            Call SetKensakuKensu()

            'フォーカスにセットする
            Me.tbxUserCd.Focus()

        End If

        'クリアボタン
        Me.btnClear.Attributes.Add("onClick", "return fncClear('" & Me.tbxUserCd.ClientID & "','" & Me.tbxSiMei.ClientID & "','" & Me.ddlUserMei.ClientID & "');")
        '閉じるボタン
        Me.btnTojiru.Attributes.Add("onClick", "fncClose();return false;")

    End Sub

    ''' <summary>
    ''' [検索実行]ボタンを押下する
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2012/11/15　趙冬雪(大連情報システム部)　新規作成</history>
    Protected Sub btnKensakuJltukou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuJltukou.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '営業マン情報を取得する
        Dim dtUser As Data.DataTable
        dtUser = eigyouManSearchBC.GetUserInfo(Me.ddlUserMei.SelectedValue, _
                                                            Me.tbxUserCd.Text, _
                                                            Me.tbxSiMei.Text, _
                                                            Me.chkTorikesi.Checked)

        '明細情報を表示する
        If dtUser.Rows.Count > 0 Then
            Me.grdMeisai.DataSource = dtUser
            Me.grdMeisai.DataBind()

            '"▲"と"▼"リンクは表示する
            Me.lnkBtnUserIdUp.Visible = True
            Me.lnkBtnUserIdDown.Visible = True
            Me.lnkBtnUserNameUp.Visible = True
            Me.lnkBtnUserNameDown.Visible = True
            Me.lnkBtnTorikesiUp.Visible = True
            Me.lnkBtnTorikesiDown.Visible = True

            Me.lnkBtnUserIdUp.Style.Add("color", "SkyBlue")
            Me.lnkBtnUserIdDown.Style.Add("color", "SkyBlue")
            Me.lnkBtnUserNameUp.Style.Add("color", "SkyBlue")
            Me.lnkBtnUserNameDown.Style.Add("color", "SkyBlue")
            Me.lnkBtnTorikesiUp.Style.Add("color", "SkyBlue")
            Me.lnkBtnTorikesiDown.Style.Add("color", "SkyBlue")
        Else
            Me.grdMeisai.DataSource = Nothing
            Me.grdMeisai.DataBind()

            '"▲"と"▼"リンクは表示する
            Me.lnkBtnUserIdUp.Visible = False
            Me.lnkBtnUserIdDown.Visible = False
            Me.lnkBtnUserNameUp.Visible = False
            Me.lnkBtnUserNameDown.Visible = False
            Me.lnkBtnTorikesiUp.Visible = False
            Me.lnkBtnTorikesiDown.Visible = False
            'メッセージを表示する
            Dim common As New Common
            common.SetShowMessage(Me, MSG011E)
            
            ''検索上限件数DropdownListを設定する
            'Call SetKensakuKensu()
        End If

        '明細データの件数を取得する
        Dim dtMeisaiCount As Data.DataTable
        dtMeisaiCount = eigyouManSearchBC.GetUserCount(Me.tbxUserCd.Text, _
                                                              Me.tbxSiMei.Text, Me.chkTorikesi.Checked)

        '明細行は1件です
        If dtUser.Rows.Count = 1 Then
            '営業マン営業担当者名は前画面に戻る、POPUPを閉めます。
            Call SetValueScript()

            '検索上限件数ドロップダウンリストは"100件"を選択する時
        ElseIf Me.ddlUserMei.SelectedValue.Equals("0") Then
            '検索したデータを100件以内時
            If CDbl(dtMeisaiCount.Rows(0).Item(0).ToString) > 100 Then
                'Me.lblKensakuKeltuka.Text = dtUser.Rows.Count.ToString
                'Me.lblKensakuKeltuka.Style.Add("color", "black")
                Me.lblKensakuKeltuka.Text = dtUser.Rows.Count & "/" & dtMeisaiCount.Rows(0).Item(0).ToString
                Me.lblKensakuKeltuka.Style.Add("color", "red")
            Else
                '索したデータを100件を越えした時
                'Me.lblKensakuKeltuka.Text = dtUser.Rows.Count & "/" & dtMeisaiCount.Rows(0).Item(0).ToString
                'Me.lblKensakuKeltuka.Style.Add("color", "red")
                Me.lblKensakuKeltuka.Text = dtUser.Rows.Count.ToString
                Me.lblKensakuKeltuka.Style.Add("color", "black")
            End If

            '検索上限件数ドロップダウンリストは"無制限"を選択する時
        ElseIf Me.ddlUserMei.SelectedValue.Equals("1") Then
            Me.lblKensakuKeltuka.Text = dtUser.Rows.Count.ToString
            Me.lblKensakuKeltuka.Style.Add("color", "black")

        End If

    End Sub

    ''' <summary>
    ''' GridViewデータ取込む
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.Web.UI.WebControls.GridViewRowEventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2012/11/15　趙冬雪(大連情報システム部)　新規作成</history>
    Protected Sub grdMeisai_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMeisai.RowDataBound

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If e.Row.RowType = DataControlRowType.DataRow Then
            'クッリク時、ピンク色を変更する
            e.Row.Attributes.Add("onclick", "selectedLineColor(this);")

            Dim lblMeisai As Label = CType(e.Row.FindControl("lblSiMeiValue"), Label)
            Dim lblMeisaiCd As Label = CType(e.Row.FindControl("lblUserCdValue"), Label)

            Dim strHidModuru As String = Request.QueryString("fieldHid")
            Dim strBtn As String = Request.QueryString("fieldBtn")

            'ダブルクリックする時
            e.Row.Attributes.Add("ondblclick", "fncSetItem('" & Request.QueryString("field") & _
                                                           "','" & Request.QueryString("fieldCd") & _
                                                           "','" & lblMeisai.ClientID & _
                                                           "','" & lblMeisaiCd.ClientID & _
                                                           "','" & strHidModuru & _
                                                           "','" & strBtn & _
                                                           "');")

        End If

    End Sub

#End Region

#Region "メソッド"

    ''' <summary>
    ''' "▲"と"▼"リンクの順序
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2012/11/15　趙冬雪(大連情報システム部)　新規作成</history>
    Protected Sub SetJyunjyo(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnUserIdUp.Click, _
                                                                                          lnkBtnUserIdDown.Click, _
                                                                                          lnkBtnUserNameUp.Click, _
                                                                                          lnkBtnUserNameDown.Click, _
                                                                                          lnkBtnTorikesiUp.Click, _
                                                                                          lnkBtnTorikesiDown.Click
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '営業マン情報を取得する
        Dim dtEigyouMan As Data.DataTable
        dtEigyouMan = eigyouManSearchBC.GetUserInfo(Me.ddlUserMei.SelectedValue, _
                                                            Me.tbxUserCd.Text, _
                                                            Me.tbxSiMei.Text, Me.chkTorikesi.Checked)
        '明細情報を表示する
        If dtEigyouMan.Rows.Count > 0 Then
            Me.grdMeisai.DataSource = dtEigyouMan
            Me.grdMeisai.DataBind()

            '"▲"と"▼"リンクは表示する
            Me.lnkBtnUserIdUp.Visible = True
            Me.lnkBtnUserIdDown.Visible = True

            Me.lnkBtnUserIdUp.Style.Add("color", "SkyBlue")
            Me.lnkBtnUserIdDown.Style.Add("color", "SkyBlue")
            Me.lnkBtnTorikesiUp.Style.Add("color", "SkyBlue")
            Me.lnkBtnTorikesiDown.Style.Add("color", "SkyBlue")
        Else
            Me.grdMeisai.DataSource = Nothing
            Me.grdMeisai.DataBind()

            '"▲"と"▼"リンクは表示しない
            Me.lnkBtnUserIdUp.Visible = False
            Me.lnkBtnUserIdDown.Visible = False
            Me.lnkBtnTorikesiUp.Visible = False
            Me.lnkBtnTorikesiDown.Visible = False
            '検索結果
            Me.lblKensakuKeltuka.Style.Add("color", "black")
            Me.lblKensakuKeltuka.Text = "0"

            'メッセージを表示する
            Dim common As New Common
            common.SetShowMessage(Me, MSG011E)

        End If

        Dim dv As Data.DataView
        dv = dtEigyouMan.DefaultView

        Dim lnkButton As System.Web.UI.WebControls.LinkButton
        lnkButton = CType(sender, LinkButton)

        '操作の項目
        Dim strKoumoku As String
        '順序
        Dim strJyunjyo As String

        If lnkButton.ID.IndexOf("Up") > 0 Then
            strJyunjyo = "ASC"
        ElseIf lnkButton.ID.IndexOf("Down") > 0 Then
            strJyunjyo = "DESC"
        Else
            strJyunjyo = String.Empty
        End If

        Select Case lnkButton.ID.Replace("Up", "").Replace("Down", "").Replace("lnkBtn", "")
            Case "UserId"
                strKoumoku = "login_user_id"
            Case "UserName"
                strKoumoku = "DisplayName"
            Case "Torikesi"
                strKoumoku = "Torikesi"
            Case Else
                strKoumoku = String.Empty
        End Select

        dv.Sort = strKoumoku & Space(1) & strJyunjyo

        Me.lnkBtnUserIdUp.Style.Add("color", "SkyBlue")
        Me.lnkBtnUserIdDown.Style.Add("color", "SkyBlue")
        Me.lnkBtnUserNameUp.Style.Add("color", "SkyBlue")
        Me.lnkBtnUserNameDown.Style.Add("color", "SkyBlue")
        Me.lnkBtnTorikesiUp.Style.Add("color", "SkyBlue")
        Me.lnkBtnTorikesiDown.Style.Add("color", "SkyBlue")
        lnkButton.Style.Add("color", "IndianRed")

        If dv.ToTable.Rows.Count > 0 Then
            Me.grdMeisai.DataSource = dv
            Me.grdMeisai.DataBind()
        Else
            Me.grdMeisai.DataSource = Nothing
            Me.grdMeisai.DataBind()

            '"▲"と"▼"リンクは表示しない
            Me.lnkBtnUserIdUp.Visible = False
            Me.lnkBtnUserIdDown.Visible = False
            Me.lnkBtnUserNameUp.Visible = False
            Me.lnkBtnUserNameDown.Visible = False
            Me.lnkBtnTorikesiUp.Visible = False
            Me.lnkBtnTorikesiDown.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' 検索上限件数DropdownListを設定する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/11/15　趙冬雪(大連情報システム部)　新規作成</history>
    Private Sub SetKensakuKensu()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '検索上限件数DropdownListをクリアする
        Me.ddlUserMei.Items.Clear()

        '検索上限件数DropdownListをセットする
        Me.ddlUserMei.Items.Insert(0, New ListItem("100件", "0"))
        Me.ddlUserMei.Items.Insert(1, New ListItem("無制限", "1"))

    End Sub

    ''' <summary>
    '''  JS作成
    ''' </summary>
    ''' <history>2012/11/15　趙冬雪(大連情報システム部)　新規作成</history>
    Protected Sub MakeJavaScript()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                                MyMethod.GetCurrentMethod.Name)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            '画面データをクリアする
            .AppendLine("function fncClear(objcd,objmei,objddl)")
            .AppendLine("{")
            .AppendLine("   $ID(objcd).innerText='';")
            .AppendLine("   $ID(objmei).innerText='';")
            .AppendLine("   $ID(objddl).selectedIndex=0;")
            .AppendLine("return false;")
            .AppendLine("}")

            '画面の閉じる処理
            .AppendLine("function fncClose()")
            .AppendLine("{")
            .AppendLine("   self.close();")
            .AppendLine("}")

            '明細の行をダブルクリックする
            .AppendLine("function fncSetItem(objMei,objCd,strObjMei,strObjCd,strObjHidModoru,strObjBtn)")
            .AppendLine("{")
            .AppendLine("   if(window.opener == null || window.opener.closed)")
            .AppendLine("   {")
            .AppendLine("       alert('" & String.Format(MSG035E) & "');")
            .AppendLine("       return false;")
            .AppendLine("   }else")
            .AppendLine("   {")
            .AppendLine("       window.opener.$ID(objMei).innerText=$ID(strObjMei).innerText;")
            .AppendLine("       window.opener.$ID(objCd).innerText=$ID(strObjCd).innerText;")
            .AppendLine("       if(strObjHidModoru != '')")
            .AppendLine("       {")
            .AppendLine("          window.opener.$ID(strObjHidModoru).value='EigyouMan';")
            .AppendLine("          window.opener.$ID(strObjBtn).click();")
            .AppendLine("       }")
            .AppendLine("       if(window.opener.$ID(objMei).readOnly != true)")
            .AppendLine("       {")
            .AppendLine("           window.opener.$ID(objMei).select();")
            .AppendLine("       }")
            .AppendLine("       window.close();")
            .AppendLine("   }")
            .AppendLine("}")

            .AppendLine("</script>")
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

    ''' <summary>
    ''' 明細データは1行がある
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/11/15　趙冬雪(大連情報システム部)　新規作成</history>
    Private Sub SetValueScript()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                                MyMethod.GetCurrentMethod.Name)

        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")
            .AppendLine("if(window.opener == null || window.opener.closed)")
            .AppendLine("{")
            .AppendLine("    alert('" & MSG035E & "');")
            .AppendLine("}else")
            .AppendLine("{")
            .AppendLine("    eval('window.opener.'+'" & Request.QueryString("formName") & "'+'.'+'" & Request.QueryString("field") & "').innerText='" & CType(Me.grdMeisai.Rows(0).FindControl("lblSiMeiValue"), Label).Text & "';")
            .AppendLine("    eval('window.opener.'+'" & Request.QueryString("formName") & "'+'.'+'" & Request.QueryString("fieldCd") & "').innerText='" & CType(Me.grdMeisai.Rows(0).FindControl("lblUserCdValue"), Label).Text & "';")
            .AppendLine("    if(('" & Request.QueryString("fieldHid") & "') != '')")
            .AppendLine("    {")
            .AppendLine("       eval('window.opener.'+'" & Request.QueryString("formName") & "'+'.'+'" & Request.QueryString("fieldHid") & "').value='EigyouMan';")
            .AppendLine("       eval('window.opener.'+'" & Request.QueryString("formName") & "'+'.'+'" & Request.QueryString("fieldBtn") & "').click();")
            .AppendLine("    }")
            .AppendLine("    window.close();")
            .AppendLine("}")

            .AppendLine("</script>")
        End With

        ClientScript.RegisterStartupScript(Me.GetType, "", csScript.ToString)

    End Sub

#End Region
End Class
