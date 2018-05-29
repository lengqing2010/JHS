﻿Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.Utilities.CommonMessage
Imports Lixil.JHS_EKKS.BizLogic
Imports System.Data.DataTable
Imports System.Collections.Generic

''' <summary>
''' 登録事業者検索POPUP
''' </summary>
''' <remarks></remarks>
Partial Class PopupSearch_TourokuJigyousyaSearch
    Inherits System.Web.UI.Page

#Region "プライベート変数"

    Private tourokuJigyousyaSearchBC As New Lixil.JHS_EKKS.BizLogic.TourokuJigyousyaSearchBC

#End Region

#Region "イベント"

    ''' <summary>
    ''' 画面の初期表示
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        'JavaScript
        Call MakeJavaScript()

        If Not IsPostBack Then

            '登録事業者
            Me.tbxKameitenMei.Text = Request.QueryString("strTourokuJigyousya")

            '"▲"と"▼"リンクは表示しない
            Call SetLink(False)

            '検索上限件数DropdownListを設定する
            Call SetKensakuKensu()

            'フォーカスにセットする
            Me.tbxKameitenCd.Focus()
        End If

        'クリアボタン
        Me.btnClear.Attributes.Add("onClick", "return fncClear('" & Me.tbxKameitenCd.ClientID & "','" & Me.tbxKameitenMei.ClientID & "','" & Me.ddlKensakuKensu.ClientID & "');")
        '閉じるボタン
        Me.btnTojiru.Attributes.Add("onClick", "fncClose();return false;")

    End Sub

    ''' <summary>
    ''' [検索実行]ボタンを押下する
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Protected Sub btnKensakuJltukou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuJltukou.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '系列情報を取得する
        Dim dtTourokuJigyousya As Data.DataTable
        dtTourokuJigyousya = tourokuJigyousyaSearchBC.GetTourokuJigyousya(Me.ddlKensakuKensu.SelectedValue, _
                                                                          Me.tbxKameitenCd.Text, _
                                                                          Me.tbxKameitenMei.Text, _
                                                                          Me.chkTorikesi.Checked)

        '明細情報を表示する
        If dtTourokuJigyousya.Rows.Count > 0 Then

            '"▲"と"▼"リンクは表示する
            Call SetLink(True)

            'linkButtonの色はSkyBlueにセットする
            Call SetLinkColor()

            Me.grdMeisai.DataSource = dtTourokuJigyousya
            Me.grdMeisai.DataBind()

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
        dtMeisaiCount = tourokuJigyousyaSearchBC.GetTourokuJigyousyaCount(Me.tbxKameitenCd.Text, _
                                                                          Me.tbxKameitenMei.Text, _
                                                                          Me.chkTorikesi.Checked)

        '明細行は1件です
        If dtTourokuJigyousya.Rows.Count = 1 Then
            '加盟店名は前画面に戻る、POPUPを閉めます。
            Call SetValueScript()

            '検索上限件数ドロップダウンリストは"100件"を選択する時
        ElseIf Me.ddlKensakuKensu.SelectedValue.Equals("0") Then

            If CDbl(dtMeisaiCount.Rows(0).Item(0).ToString) > 100 Then
                '索したデータを100件を越えした時
                Me.lblKensakuKeltuka.Text = dtTourokuJigyousya.Rows.Count & "/" & dtMeisaiCount.Rows(0).Item(0).ToString
                Me.lblKensakuKeltuka.Style.Add("color", "red")

            Else
                '検索したデータを100件以内時
                Me.lblKensakuKeltuka.Text = dtTourokuJigyousya.Rows.Count.ToString
                Me.lblKensakuKeltuka.Style.Add("color", "black")
            End If

            '検索上限件数ドロップダウンリストは"無制限"を選択する時
        ElseIf Me.ddlKensakuKensu.SelectedValue.Equals("1") Then
            Me.lblKensakuKeltuka.Text = dtTourokuJigyousya.Rows.Count.ToString
            Me.lblKensakuKeltuka.Style.Add("color", "black")

        End If

    End Sub

    ''' <summary>
    ''' GridViewデータ取込む
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.Web.UI.WebControls.GridViewRowEventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Protected Sub grdMeisai_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMeisai.RowDataBound

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If e.Row.RowType = DataControlRowType.DataRow Then

            'クッリク時、ピンク色を変更する
            e.Row.Attributes.Add("onclick", "selectedLineColor(this);")

            Dim lblMeisai As Label = CType(e.Row.FindControl("lblKameitenMeiValue"), Label)
            Dim lblMeisaiCd As Label = CType(e.Row.FindControl("lblKameitenCdValue"), Label)

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

    ''' <summary>
    ''' "▲"と"▼"リンクの順序
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Protected Sub SetJyunjyo(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnKameitenCdUp.Click, _
                                                                                          lnkBtnKameitenCdDown.Click, _
                                                                                          lnkBtnKameitenKanaMeiUp.Click, _
                                                                                          lnkBtnKameitenKanaMeiDown.Click, _
                                                                                          lnkBtnKameitenMeiUp.Click, _
                                                                                          lnkBtnKameitenMeiDown.Click, _
                                                                                          lnkBtnTodouhukenMeiUp.Click, _
                                                                                          lnkBtnTodouhukenMeiDown.Click, _
                                                                                          lnkBtnTorikesiUp.Click, _
                                                                                          lnkBtnTorikesiDown.Click


        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '登録事業者情報を取得する
        Dim dtourokuJigyousya As Data.DataTable
        dtourokuJigyousya = tourokuJigyousyaSearchBC.GetTourokuJigyousya(Me.ddlKensakuKensu.SelectedValue, _
                                                                       Me.tbxKameitenCd.Text, _
                                                                       Me.tbxKameitenMei.Text, _
                                                                       Me.chkTorikesi.Checked)

        '明細情報を表示する
        If dtourokuJigyousya.Rows.Count > 0 Then

            '"▲"と"▼"リンクは表示する
            Call SetLink(True)

            'linkButtonの色はSkyBlueにセットする
            Call SetLinkColor()

            Me.grdMeisai.DataSource = dtourokuJigyousya
            Me.grdMeisai.DataBind()

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
        dv = dtourokuJigyousya.DefaultView

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
            Case "KameitenCd"
                strKoumoku = "kameiten_cd"
            Case "KameitenMei"
                strKoumoku = "kameiten_mei1"
            Case "TodouhukenMei"
                strKoumoku = "todouhuken_mei"
            Case "KameitenKanaMei"
                strKoumoku = "tenmei_kana1"
            Case "Torikesi"
                strKoumoku = "Torikesi"
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
    ''' 検索上限件数DropdownListを設定する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Private Sub SetKensakuKensu()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '検索上限件数DropdownListをクリアする
        Me.ddlKensakuKensu.Items.Clear()

        '検索上限件数DropdownListをセットする
        Me.ddlKensakuKensu.Items.Insert(0, New ListItem("100件", "0"))
        Me.ddlKensakuKensu.Items.Insert(1, New ListItem("無制限", "1"))

    End Sub

    ''' <summary>
    ''' "▲"と"▼"リンクは表示する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Private Sub SetLink(ByVal linkKbn As Boolean)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        If linkKbn = True Then

            '"▲"と"▼"リンクは表示する
            Me.lnkBtnKameitenCdUp.Visible = True
            Me.lnkBtnKameitenCdDown.Visible = True
            Me.lnkBtnKameitenMeiUp.Visible = True
            Me.lnkBtnKameitenMeiDown.Visible = True
            Me.lnkBtnTodouhukenMeiUp.Visible = True
            Me.lnkBtnTodouhukenMeiDown.Visible = True
            Me.lnkBtnKameitenKanaMeiUp.Visible = True
            Me.lnkBtnKameitenKanaMeiDown.Visible = True
            Me.lnkBtnTorikesiUp.Visible = True
            Me.lnkBtnTorikesiDown.Visible = True
        Else
            '"▲"と"▼"リンクは表示しない
            Me.lnkBtnKameitenCdUp.Visible = False
            Me.lnkBtnKameitenCdDown.Visible = False
            Me.lnkBtnKameitenMeiUp.Visible = False
            Me.lnkBtnKameitenMeiDown.Visible = False
            Me.lnkBtnTodouhukenMeiUp.Visible = False
            Me.lnkBtnTodouhukenMeiDown.Visible = False
            Me.lnkBtnKameitenKanaMeiUp.Visible = False
            Me.lnkBtnKameitenKanaMeiDown.Visible = False
            Me.lnkBtnTorikesiUp.Visible = False
            Me.lnkBtnTorikesiDown.Visible = False

        End If

    End Sub

    ''' <summary>
    ''' linkButtonの色はSkyBlueにセットする
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Private Sub SetLinkColor()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        'linkButtonの色はSkyBlueにセットする
        Me.lnkBtnKameitenCdUp.Style.Add("color", "SkyBlue")
        Me.lnkBtnKameitenCdDown.Style.Add("color", "SkyBlue")
        Me.lnkBtnKameitenMeiUp.Style.Add("color", "SkyBlue")
        Me.lnkBtnKameitenMeiDown.Style.Add("color", "SkyBlue")
        Me.lnkBtnTodouhukenMeiUp.Style.Add("color", "SkyBlue")
        Me.lnkBtnTodouhukenMeiDown.Style.Add("color", "SkyBlue")
        Me.lnkBtnKameitenKanaMeiUp.Style.Add("color", "SkyBlue")
        Me.lnkBtnKameitenKanaMeiDown.Style.Add("color", "SkyBlue")
        Me.lnkBtnTorikesiUp.Style.Add("color", "SkyBlue")
        Me.lnkBtnTorikesiDown.Style.Add("color", "SkyBlue")

    End Sub

    ''' <summary>
    '''  JS作成
    ''' </summary>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
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
            .AppendLine("function fncClear(objCd,objmei,objddl)")
            .AppendLine("{")
            .AppendLine("   $ID(objCd).innerText='';")
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
            .AppendLine("          window.opener.$ID(strObjHidModoru).value='Tourokusya';")
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
    '''  <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
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
            .AppendLine("    eval('window.opener.'+'" & Request.QueryString("formName") & "'+'.'+'" & Request.QueryString("field") & "').innerText='" & CType(Me.grdMeisai.Rows(0).FindControl("lblKameitenMeiValue"), Label).Text & "';")
            .AppendLine("    eval('window.opener.'+'" & Request.QueryString("formName") & "'+'.'+'" & Request.QueryString("fieldCd") & "').innerText='" & CType(Me.grdMeisai.Rows(0).FindControl("lblKameitenCdValue"), Label).Text & "';")
            .AppendLine("    if(('" & Request.QueryString("fieldHid") & "') != '')")
            .AppendLine("    {")
            .AppendLine("       eval('window.opener.'+'" & Request.QueryString("formName") & "'+'.'+'" & Request.QueryString("fieldHid") & "').value='Tourokusya';")
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
