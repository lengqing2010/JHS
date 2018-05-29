Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.Utilities.CommonMessage
Imports Lixil.JHS_EKKS.BizLogic
Imports System.Data.DataTable
Imports System.Collections.Generic
Partial Class SitenSearch
    Inherits System.Web.UI.Page
#Region "プライベート変数"

    Private SitenSearchBC As New Lixil.JHS_EKKS.BizLogic.SitenSearchBC

#End Region

#Region "イベント"

    ''' <summary>
    ''' 画面の初期表示
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
     '''  <history>2012/11/19　趙冬雪(大連情報システム部)　新規作成</history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        'JavaScript
        Call MakeJavaScript()

        If Not IsPostBack Then


            Me.tbxSitenMei.Text = Request.QueryString("strSitenMei") '支店名

            '"▲"と"▼"リンクは表示しない
            Me.lnkBtnSitenMeiUp.Visible = False
            Me.lnkBtnSitenMeiDown.Visible = False
            Me.lnkBtnTorikesiUp.Visible = False
            Me.lnkBtnTorikesiDown.Visible = False

            '検索上限件数DropdownListを設定する
            Call SetSitenMei()

            'フォーカスにセットする
            Me.tbxSitenMei.Focus()

        End If

        'クリアボタン
        Me.btnClear.Attributes.Add("onClick", "return fncClear('" & Me.tbxSitenMei.ClientID & "','" & Me.ddlSitenMei.ClientID & "');")
        '閉じるボタン
        Me.btnTojiru.Attributes.Add("onClick", "fncClose();return false;")

    End Sub

    ''' <summary>
    ''' [検索実行]ボタンを押下する
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    '''  <history>2012/11/19　趙冬雪(大連情報システム部)　新規作成</history>
    Protected Sub btnKensakuJltukou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuJltukou.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '支店情報を取得する
        Dim dtSitenMei As Data.DataTable
        dtSitenMei = SitenSearchBC.GetBusyoKanri(Me.ddlSitenMei.SelectedValue, _
                                                            Me.tbxSitenMei.Text, _
                                                            Me.chkTorikesi.Checked)

        '明細情報を表示する
        If dtSitenMei.Rows.Count > 0 Then
            Me.grdMeisai.DataSource = dtSitenMei
            Me.grdMeisai.DataBind()

            '"▲"と"▼"リンクは表示する
            Me.lnkBtnSitenMeiUp.Visible = True
            Me.lnkBtnSitenMeiDown.Visible = True
            Me.lnkBtnTorikesiUp.Visible = True
            Me.lnkBtnTorikesiDown.Visible = True


            Me.lnkBtnSitenMeiUp.Style.Add("color", "SkyBlue")
            Me.lnkBtnSitenMeiDown.Style.Add("color", "SkyBlue")
            Me.lnkBtnTorikesiUp.Style.Add("color", "SkyBlue")
            Me.lnkBtnTorikesiDown.Style.Add("color", "SkyBlue")
        Else
            Me.grdMeisai.DataSource = Nothing
            Me.grdMeisai.DataBind()

            '"▲"と"▼"リンクは表示する
            Me.lnkBtnSitenMeiUp.Visible = False
            Me.lnkBtnSitenMeiDown.Visible = False
            Me.lnkBtnTorikesiUp.Visible = False
            Me.lnkBtnTorikesiDown.Visible = False

            'メッセージを表示する
            Dim common As New Common
            common.SetShowMessage(Me, MSG011E)

        End If

        '明細データの件数を取得する
        Dim dtMeisaiCount As Data.DataTable
        dtMeisaiCount = SitenSearchBC.GetDataCount(Me.tbxSitenMei.Text, Me.chkTorikesi.Checked)

        '明細行は1件です
        If dtSitenMei.Rows.Count = 1 Then
            '支店名は前画面に戻る、POPUPを閉めます。
            Call SetValueScript()

            '検索上限件数ドロップダウンリストは"100件"を選択する時
        ElseIf Me.ddlSitenMei.SelectedValue.Equals("0") Then
            '検索したデータを100件以内時
            If CDbl(dtMeisaiCount.Rows(0).Item(0).ToString) > 100 Then
                'Me.lblKensakuKeltuka.Text = dtSitenMei.Rows.Count.ToString
                'Me.lblKensakuKeltuka.Style.Add("color", "black")
                Me.lblKensakuKeltuka.Text = dtSitenMei.Rows.Count & "/" & dtMeisaiCount.Rows(0).Item(0).ToString
                Me.lblKensakuKeltuka.Style.Add("color", "red")
            Else
                '検索したデータを100件を越えした時
                'Me.lblKensakuKeltuka.Text = dtSitenMei.Rows.Count & "/" & dtMeisaiCount.Rows(0).Item(0).ToString
                'Me.lblKensakuKeltuka.Style.Add("color", "red")
                Me.lblKensakuKeltuka.Text = dtSitenMei.Rows.Count.ToString
                Me.lblKensakuKeltuka.Style.Add("color", "black")
            End If

            '検索上限件数ドロップダウンリストは"無制限"を選択する時
        ElseIf Me.ddlSitenMei.SelectedValue.Equals("1") Then
            Me.lblKensakuKeltuka.Text = dtSitenMei.Rows.Count.ToString
            Me.lblKensakuKeltuka.Style.Add("color", "black")

        End If

    End Sub

    ''' <summary>
    ''' GridViewデータ取込む
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.Web.UI.WebControls.GridViewRowEventArgs</param>
    ''' <remarks></remarks>
    '''  <history>2012/11/19　趙冬雪(大連情報システム部)　新規作成</history>
    Protected Sub grdMeisai_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMeisai.RowDataBound

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If e.Row.RowType = DataControlRowType.DataRow Then
            'クッリク時、ピンク色を変更する
            e.Row.Attributes.Add("onclick", "selectedLineColor(this);")

            Dim lblMeisai As Label = CType(e.Row.FindControl("lblSitenMeiValue"), Label)
            Dim hidMeisaiCd As HiddenField = CType(e.Row.FindControl("hidSitenCdValue"), HiddenField)

            Dim strHidModuru As String = Request.QueryString("fieldHid")
            Dim strBtn As String = Request.QueryString("fieldBtn")

            'ダブルクリックする時
            e.Row.Attributes.Add("ondblclick", "fncSetItem('" & Request.QueryString("field") & _
                                                          "','" & Request.QueryString("fieldCd") & _
                                                           "','" & lblMeisai.ClientID & _
                                                           "','" & hidMeisaiCd.ClientID & _
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
    '''  <history>2012/11/19　趙冬雪(大連情報システム部)　新規作成</history>
    Protected Sub SetJyunjyo(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnSitenMeiUp.Click, _
                                                                                          lnkBtnSitenMeiDown.Click, _
                                                                                          lnkBtnTorikesiUp.Click, _
                                                                                          lnkBtnTorikesiDown.Click



        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '支店情報を取得する
        Dim dtSitenMei As Data.DataTable
        dtSitenMei = SitenSearchBC.GetBusyoKanri(Me.ddlSitenMei.SelectedValue, _
                                                            Me.tbxSitenMei.Text, Me.chkTorikesi.Checked)
        '明細情報を表示する
        If dtSitenMei.Rows.Count > 0 Then
            Me.grdMeisai.DataSource = dtSitenMei
            Me.grdMeisai.DataBind()

            '"▲"と"▼"リンクは表示する
            Me.lnkBtnSitenMeiUp.Visible = True
            Me.lnkBtnSitenMeiDown.Visible = True
            Me.lnkBtnTorikesiUp.Visible = True
            Me.lnkBtnTorikesiDown.Visible = True

            Me.lnkBtnSitenMeiUp.Style.Add("color", "SkyBlue")
            Me.lnkBtnSitenMeiDown.Style.Add("color", "SkyBlue")
            Me.lnkBtnTorikesiUp.Style.Add("color", "SkyBlue")
            Me.lnkBtnTorikesiDown.Style.Add("color", "SkyBlue")
        Else
            Me.grdMeisai.DataSource = Nothing
            Me.grdMeisai.DataBind()

            '"▲"と"▼"リンクは表示しない
            Me.lnkBtnSitenMeiUp.Visible = False
            Me.lnkBtnSitenMeiDown.Visible = False
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
        dv = dtSitenMei.DefaultView

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
            Case "SitenMei"
                strKoumoku = "busyo_mei"
            Case "Torikesi"
                strKoumoku = "Torikesi"
            Case Else
                strKoumoku = String.Empty
        End Select

        dv.Sort = strKoumoku & Space(1) & strJyunjyo

        Me.lnkBtnSitenMeiUp.Style.Add("color", "SkyBlue")
        Me.lnkBtnSitenMeiDown.Style.Add("color", "SkyBlue")


        lnkButton.Style.Add("color", "IndianRed")

        If dv.ToTable.Rows.Count > 0 Then
            Me.grdMeisai.DataSource = dv
            Me.grdMeisai.DataBind()
        Else
            Me.grdMeisai.DataSource = Nothing
            Me.grdMeisai.DataBind()

            '"▲"と"▼"リンクは表示しない
            Me.lnkBtnSitenMeiUp.Visible = False
            Me.lnkBtnSitenMeiDown.Visible = False
            Me.lnkBtnTorikesiUp.Visible = False
            Me.lnkBtnTorikesiDown.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' 検索上限件数DropdownListを設定する
    ''' </summary>
    ''' <remarks></remarks>
    '''  <history>2012/11/19　趙冬雪(大連情報システム部)　新規作成</history>
    Private Sub SetSitenMei()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '検索上限件数DropdownListをクリアする
        Me.ddlSitenMei.Items.Clear()

        '検索上限件数DropdownListをセットする
        Me.ddlSitenMei.Items.Insert(0, New ListItem("100件", "0"))
        Me.ddlSitenMei.Items.Insert(1, New ListItem("無制限", "1"))

    End Sub

    ''' <summary>
    '''  JS作成
    ''' </summary>
    '''  <history>2012/11/19　趙冬雪(大連情報システム部)　新規作成</history>
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
            .AppendLine("function fncClear(objmei,objddl)")
            .AppendLine("{")
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
            .AppendLine("       window.opener.$ID(objCd).innerText=$ID(strObjCd).value;")
            .AppendLine("       if(strObjHidModoru != '')")
            .AppendLine("       {")
            .AppendLine("          window.opener.$ID(strObjHidModoru).value='Siten';")
            .AppendLine("          window.opener.$ID(strObjBtn).click();")
            .AppendLine("       }")
            .AppendLine("       window.opener.$ID(objMei).select();")
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
    '''  <history>2012/11/19　趙冬雪(大連情報システム部)　新規作成</history>
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
            .AppendLine("    eval('window.opener.'+'" & Request.QueryString("formName") & "'+'.'+'" & Request.QueryString("field") & "').innerText='" & CType(Me.grdMeisai.Rows(0).FindControl("lblSitenMeiValue"), Label).Text & "';")
            .AppendLine("    eval('window.opener.'+'" & Request.QueryString("formName") & "'+'.'+'" & Request.QueryString("fieldCd") & "').innerText='" & CType(Me.grdMeisai.Rows(0).FindControl("hidSitenCdValue"), HiddenField).Value & "';")
            .AppendLine("    if(('" & Request.QueryString("fieldHid") & "') != '')")
            .AppendLine("    {")
            .AppendLine("       eval('window.opener.'+'" & Request.QueryString("formName") & "'+'.'+'" & Request.QueryString("fieldHid") & "').value='Siten';")
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
