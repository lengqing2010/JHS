Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.Utilities.CommonMessage
Imports Lixil.JHS_EKKS.BizLogic
Imports System.Data.DataTable
Imports System.Collections.Generic

''' <summary>
''' 種別検索
''' </summary>
''' <remarks></remarks>
Partial Class PopupSearch_SyubetuSearch
    Inherits System.Web.UI.Page

#Region "プライベート変数"

    Private SyubetuSearchBC As New Lixil.JHS_EKKS.BizLogic.SyubetuSearchBC

#End Region

#Region "イベント"

    ''' <summary>
    ''' 画面の初期表示
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/09/03　李宇(大連情報システム部)　新規作成</history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        'JavaScript
        Call MakeJavaScript()

        If Not IsPostBack Then

            '種別コード
            Me.tbxSyubetuCd.Text = Request.QueryString("strSyubetuCd")

            '"▲"と"▼"リンクは表示しない
            Call SetLink(False)

            '検索上限件数DropdownListを設定する
            Call SetKensakuKensu()

            'フォーカスにセットする
            Me.tbxSyubetuCd.Focus()
        End If

        'クリアボタン
        Me.btnClear.Attributes.Add("onClick", "return fncClear('" & Me.tbxSyubetuCd.ClientID & "','" & Me.tbxSyubetuMei.ClientID & "','" & Me.ddlKensakuKensu.ClientID & "');")
        '閉じるボタン
        Me.btnTojiru.Attributes.Add("onClick", "fncClose();return false;")

    End Sub

    ''' <summary>
    ''' [検索実行]ボタンを押下する
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/09/03　李宇(大連情報システム部)　新規作成</history>
    Protected Sub btnKensakuJltukou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuJltukou.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '種別情報を取得する
        Dim dtSyubetuJyouhou As Data.DataTable
        dtSyubetuJyouhou = SyubetuSearchBC.SelSyubetu(Me.ddlKensakuKensu.SelectedValue, _
                                                      Me.tbxSyubetuCd.Text, _
                                                      Me.tbxSyubetuMei.Text)

        '明細情報を表示する
        If dtSyubetuJyouhou.Rows.Count > 0 Then
            Me.grdMeisai.DataSource = dtSyubetuJyouhou
            Me.grdMeisai.DataBind()

            '"▲"と"▼"リンクは表示する
            Call SetLink(True)

            'linkButtonの色はSkyBlueにセットする
            Call SetLinkColor()

        Else
            Me.grdMeisai.DataSource = Nothing
            Me.grdMeisai.DataBind()

            '"▲"と"▼"リンクは表示しない
            Call SetLink(False)

            'メッセージを表示する
            Dim common As New Common
            common.SetShowMessage(Me, MSG011E)

        End If

        '明細データの件数を取得する
        Dim dtMeisaiCount As Data.DataTable
        dtMeisaiCount = SyubetuSearchBC.SelSyubetuCount(Me.tbxSyubetuCd.Text, Me.tbxSyubetuMei.Text)

        '明細行は1件です
        If dtSyubetuJyouhou.Rows.Count = 1 Then
            '種別コードは前画面に戻る、POPUPを閉めます。
            Call SetValueScript()

            '検索上限件数ドロップダウンリストは"100件"を選択する時
        ElseIf Me.ddlKensakuKensu.SelectedValue.Equals("100") Then

            If CDbl(dtMeisaiCount.Rows(0).Item(0)) > 100 Then
                '索したデータを100件を越えした時
                Me.lblKensakuKeltuka.Text = dtSyubetuJyouhou.Rows.Count & "/" & dtMeisaiCount.Rows(0).Item(0)
                Me.lblKensakuKeltuka.Style.Add("color", "red")
            Else
                '検索したデータを100件以内時
                Me.lblKensakuKeltuka.Text = dtSyubetuJyouhou.Rows.Count.ToString
                Me.lblKensakuKeltuka.Style.Add("color", "black")

            End If

            '検索上限件数ドロップダウンリストは"無制限"を選択する時
        ElseIf Me.ddlKensakuKensu.SelectedValue.Equals("max") Then
            Me.lblKensakuKeltuka.Text = dtSyubetuJyouhou.Rows.Count.ToString
            Me.lblKensakuKeltuka.Style.Add("color", "black")

        End If

    End Sub

    ''' <summary>
    ''' GridViewデータ取込む
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.Web.UI.WebControls.GridViewRowEventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/09/03　李宇(大連情報システム部)　新規作成</history>
    Protected Sub grdMeisai_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMeisai.RowDataBound

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If e.Row.RowType = DataControlRowType.DataRow Then
            'クッリク時、ピンク色を変更する
            e.Row.Attributes.Add("onclick", "selectedLineColor(this);")

            Dim lblMeisai As Label = CType(e.Row.FindControl("lblSyubetuMeiValue"), Label)
            Dim hidMeisaiCd As HiddenField = CType(e.Row.FindControl("hidSyubetuCd"), HiddenField)

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

    ''' <summary>
    ''' "▲"と"▼"リンクの順序
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/09/03　李宇(大連情報システム部)　新規作成</history>
    Protected Sub SetJyunjyo(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnSyubetuCdUp.Click, _
                                                                                          lnkBtnSyubetuCdDown.Click, _
                                                                                          lnkBtnSyubetuMeiUp.Click, _
                                                                                          lnkBtnSyubetuMeiDown.Click


        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '種別情報を取得する
        Dim dtSyubetuMei As Data.DataTable
        dtSyubetuMei = SyubetuSearchBC.SelSyubetu(Me.ddlKensakuKensu.SelectedValue, _
                                                    Me.tbxSyubetuCd.Text, _
                                                    Me.tbxSyubetuMei.Text)

        '明細情報を表示する
        If dtSyubetuMei.Rows.Count > 0 Then
            Me.grdMeisai.DataSource = dtSyubetuMei
            Me.grdMeisai.DataBind()

            '"▲"と"▼"リンクは表示する
            Call SetLink(True)

            'linkButtonの色はSkyBlueにセットする
            Call SetLinkColor()

        Else
            Me.grdMeisai.DataSource = Nothing
            Me.grdMeisai.DataBind()

            '"▲"と"▼"リンクは表示しない
            Call SetLink(False)

            '検索結果
            Me.lblKensakuKeltuka.Style.Add("color", "black")
            Me.lblKensakuKeltuka.Text = "0"

            'メッセージを表示する
            Dim common As New Common
            common.SetShowMessage(Me, MSG011E)

        End If

        Dim dv As Data.DataView
        dv = dtSyubetuMei.DefaultView

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
            Case "SyubetuCd"
                strKoumoku = "cd"
            Case "SyubetuMei"
                strKoumoku = "mei"
            Case Else
                strKoumoku = String.Empty
        End Select

        dv.Sort = strKoumoku & Space(1) & strJyunjyo

        'linkButtonの色はSkyBlueにセットする
        Call SetLinkColor()

        'linkButtonの色はIndianRedにセットする
        lnkButton.Style.Add("color", "IndianRed")

        If dv.ToTable.Rows.Count > 0 Then
            Me.grdMeisai.DataSource = dv
            Me.grdMeisai.DataBind()
        Else
            Me.grdMeisai.DataSource = Nothing
            Me.grdMeisai.DataBind()

            '"▲"と"▼"リンクは表示しない
            Call SetLink(False)

        End If

    End Sub

#End Region

#Region "メソッド"

    ''' <summary>
    ''' "▲"と"▼"リンクは表示する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/09/03　李宇(大連情報システム部)　新規作成</history>
    Private Sub SetLink(ByVal blnLinkKbn As Boolean)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        If blnLinkKbn = True Then

            '"▲"と"▼"リンクは表示する
            Me.lnkBtnSyubetuCdUp.Visible = True
            Me.lnkBtnSyubetuCdDown.Visible = True
            Me.lnkBtnSyubetuMeiUp.Visible = True
            Me.lnkBtnSyubetuMeiDown.Visible = True
        Else
            '"▲"と"▼"リンクは表示しない
            Me.lnkBtnSyubetuCdUp.Visible = False
            Me.lnkBtnSyubetuCdDown.Visible = False
            Me.lnkBtnSyubetuMeiUp.Visible = False
            Me.lnkBtnSyubetuMeiDown.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' linkButtonの色はSkyBlueにセットする
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/09/03　李宇(大連情報システム部)　新規作成</history>
    Private Sub SetLinkColor()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        'linkButtonの色はSkyBlueにセットする
        Me.lnkBtnSyubetuCdUp.Style.Add("color", "SkyBlue")
        Me.lnkBtnSyubetuCdDown.Style.Add("color", "SkyBlue")
        Me.lnkBtnSyubetuMeiUp.Style.Add("color", "SkyBlue")
        Me.lnkBtnSyubetuMeiDown.Style.Add("color", "SkyBlue")

    End Sub

    ''' <summary>
    ''' 検索上限件数DropdownListを設定する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/09/03　李宇(大連情報システム部)　新規作成</history>
    Private Sub SetKensakuKensu()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '検索上限件数DropdownListをクリアする
        Me.ddlKensakuKensu.Items.Clear()

        '検索上限件数DropdownListをセットする
        Me.ddlKensakuKensu.Items.Insert(0, New ListItem("100件", "100"))
        Me.ddlKensakuKensu.Items.Insert(1, New ListItem("無制限", "max"))

    End Sub

    ''' <summary>
    '''  JS作成
    ''' </summary>
    ''' <history>2013/09/03　李宇(大連情報システム部)　新規作成</history>
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
            .AppendLine("function fncSetItem(objMei,objCd,strObjMei,strObjCd)")
            .AppendLine("{")
            .AppendLine("   if(window.opener == null || window.opener.closed)")
            .AppendLine("   {")
            .AppendLine("       alert('" & String.Format(MSG035E) & "');")
            .AppendLine("       return false;")
            .AppendLine("   }else")
            .AppendLine("   {")
            .AppendLine("       window.opener.$ID(objCd).innerText=$ID(strObjCd).value;")
            .AppendLine("       window.opener.$ID(objMei).innerText=$ID(strObjMei).innerText;")
            '.AppendLine("       window.opener.$ID(objCd).select();")
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
    ''' <history>2013/09/03　李宇(大連情報システム部)　新規作成</history>
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
            .AppendLine("    window.opener.$ID('" & Request.QueryString("field") & "').innerText='" & CType(Me.grdMeisai.Rows(0).FindControl("lblSyubetuMeiValue"), Label).Text & "';")
            .AppendLine("    window.opener.$ID('" & Request.QueryString("fieldCd") & "').innerText='" & CType(Me.grdMeisai.Rows(0).FindControl("hidSyubetuCd"), HiddenField).Value & "';")
            .AppendLine("    window.close();")
            .AppendLine("}")

            .AppendLine("</script>")
        End With

        ClientScript.RegisterStartupScript(Me.GetType, "", csScript.ToString)

    End Sub

#End Region

End Class
