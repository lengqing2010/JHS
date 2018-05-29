Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Data
Imports System.Collections.Generic
Imports Lixil.JHS_EKKS.Utilities

Partial Class ZensyaSyukeiInquiry
    Inherits System.Web.UI.Page

#Region "プライベート変数"
    Private zensyaSyukeiInquiryBC As New Lixil.JHS_EKKS.BizLogic.ZensyaSyukeiInquiryBC
    Private objCommonBC As New Lixil.JHS_EKKS.BizLogic.CommonBC
    Private objCommon As New Common
    Public CommonConstBC As Lixil.JHS_EKKS.BizLogic.CommonConstBC
#End Region

#Region "イベント"
    ''' <summary>
    ''' 初期表示
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        Dim CommonCheck As New CommonCheck
        CommonCheck.CommonNinsyou(String.Empty, Master.loginUserInfo, kegen.UserIdOnly)
        If Not IsPostBack Then
            Call setLoad()
        End If
        Call SetJsEvent()

    End Sub

    ''' <summary>
    ''' 月別表示１のchange事件
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Protected Sub ddlBeginTuki_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBeginTuki.SelectedIndexChanged
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        '月別表示１の値
        Dim selValue As String
        selValue = Me.ddlBeginTuki.SelectedValue
        If ddlBeginTuki.SelectedItem.Value = "0" Then

            Me.ddlEndTuki.Items.Clear()
            Exit Sub
        End If
        '月別表示２をクリア
        Me.ddlEndTuki.Items.Clear()
        Dim index As Integer = 1
        Me.ddlEndTuki.Items.Insert(0, New ListItem("", "0"))
        For i As Integer = CType(selValue, Integer) + 1 To 15
            Dim j As Integer
            If i > 12 Then
                j = i - 12
            Else
                j = i
            End If

            Me.ddlEndTuki.Items.Insert(index, New ListItem(j.ToString & "月", i))
            index = index + 1
            Me.ddlEndTuki.SelectedValue = CType(selValue, Integer) + 1

        Next
    End Sub

    ''' <summary>
    ''' 月別表示２のchange事件
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Protected Sub ddlEndTuki_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEndTuki.SelectedIndexChanged
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        '月別表示２だけ選べるように
        If Me.ddlBeginTuki.SelectedItem.Value = "0" AndAlso Me.ddlEndTuki.SelectedItem.Value <> "0" Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG048E)
            Me.ddlBeginTuki.Focus()
        End If
    End Sub
#End Region

#Region "メンッド"

    ''' <summary>
    ''' 初期設定
    ''' </summary>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Sub setLoad()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        '年度を設定
        Call setNendo()
       
        Dim strSysTuki As Integer = Convert.ToDateTime(objCommonBC.SelSystemDate.Rows(0).Item(0).ToString).Month
        '月別表示は初期を未選択を設定
        Me.ddlBeginTuki.Items.Insert(0, New ListItem("", "0"))
        Me.ddlEndTuki.Items.Insert(0, New ListItem("", "0"))
        Me.ddlBeginTuki.SelectedIndex = 1

        'ddlBeginTuki初期設定4月なので、システム月　5月　の場合のみ
        If (strSysTuki = 4) OrElse (strSysTuki = 5) Then
            'ddlEndTuki初期設定を空白
            Me.ddlEndTuki.SelectedIndex = 0
        Else
           
            ddlEndTuki.SelectedValue = IIf(strSysTuki < 4, (strSysTuki + 12 - 1).ToString, (strSysTuki - 1).ToString)

        End If
   
    End Sub

    ''' <summary>
    ''' 年度を取得
    ''' </summary>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Sub setNendo()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
       
        Dim dtNendo As Data.DataTable
        dtNendo = objCommonBC.GetKeikakuNendoData()
        If dtNendo.Rows.Count > 0 Then
            '年度別集計
            Me.ddlNendoNendo.DataValueField = "code"
            Me.ddlNendoNendo.DataTextField = "meisyou"
            Me.ddlNendoNendo.DataSource = dtNendo
            Me.ddlNendoNendo.DataBind()

            '期間別集計
            Me.ddlNendoKikan.DataValueField = "code"
            Me.ddlNendoKikan.DataTextField = "meisyou"
            Me.ddlNendoKikan.DataSource = dtNendo
            Me.ddlNendoKikan.DataBind()
            '月別集計
            Me.ddlNendoTuki.DataValueField = "code"
            Me.ddlNendoTuki.DataTextField = "meisyou"
            Me.ddlNendoTuki.DataSource = dtNendo
            Me.ddlNendoTuki.DataBind()

            Me.ddlNendoNendo.Items.Insert(0, New ListItem("", "0"))
            Me.ddlNendoKikan.Items.Insert(0, New ListItem("", "0"))
            Me.ddlNendoTuki.Items.Insert(0, New ListItem("", "0"))

            '当前年度をデフォルト表示とする
            Dim strSysNen As Integer = objCommon.GetSystemYear()
            For i As Integer = 0 To dtNendo.Rows.Count - 1
                If dtNendo.Rows(i).Item(0).ToString.Substring(0, 4) = strSysNen Then
                    '年度別集計
                    ddlNendoNendo.SelectedValue = dtNendo.Rows(i).Item(0)
                    '期間別集計
                    ddlNendoKikan.SelectedValue = dtNendo.Rows(i).Item(0)
                    '月別集計
                    ddlNendoTuki.SelectedValue = dtNendo.Rows(i).Item(0)
                    Exit For
                End If

            Next
        Else
            '年度別集計
            Me.ddlNendoNendo.DataSource = Nothing
            Me.ddlNendoNendo.DataBind()
            '期間別集計
            Me.ddlNendoKikan.DataSource = Nothing
            Me.ddlNendoKikan.DataBind()
            '月別集計
            Me.ddlNendoTuki.DataSource = Nothing
            Me.ddlNendoTuki.DataBind()
        End If
        
    End Sub

    ''' <summary>
    ''' OnClick事件を追加
    ''' </summary>
    ''' <history>																
    ''' <para>2012/12/3 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Public Sub SetJsEvent()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        '年度表示ボタンのClick事件
        Me.btnHyouji.OnClick = "btnHyouji_Click()"
        '年度詳細ボタンのClick事件
        Me.btnSyosai.OnClick = "btnSyosai_Click()"
        '期間表示ボタンのClick事件
        Me.btnKikanHyouji.OnClick = "btnKikanHyouji_Click()"
        '期間詳細ボタンのClick事件
        Me.btnKikanSyousai.OnClick = "btnKikanSyousai_Click()"
        '月別表示ボタンのClick事件
        Me.btnTukiHyouji.OnClick = "btnTukiHyouji_Click()"
        '月別詳細ボタンのClick事件
        Me.btnTukiSyousai.OnClick = "btnTukiSyousai_Click()"

        btnHyouji.Button.Style("width") = "70px"
        btnSyosai.Button.Style("width") = "70px"
        btnKikanHyouji.Button.Style("width") = "70px"
        btnKikanSyousai.Button.Style("width") = "70px"
        btnTukiHyouji.Button.Style("width") = "70px"
        btnTukiSyousai.Button.Style("width") = "70px"

        btnHyouji.Button.Style("padding-top") = "2px"
        btnSyosai.Button.Style("padding-top") = "2px"
        btnKikanHyouji.Button.Style("padding-top") = "2px"
        btnKikanSyousai.Button.Style("padding-top") = "2px"
        btnTukiHyouji.Button.Style("padding-top") = "2px"
        btnTukiSyousai.Button.Style("padding-top") = "2px"

    End Sub
    ''' <summary>
    ''' 年度部分
    ''' </summary>
    ''' <history>																
    ''' <para>2012/12/3 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Public Sub btnHyouji_Click()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        If Me.ddlNendoNendo.SelectedValue = 0 Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "年度")
            Me.ddlNendoNendo.Focus()
            Exit Sub
        End If
        '計画値を取得
        Dim dtData As Data.DataTable
        dtData = zensyaSyukeiInquiryBC.GetNendoKeikaku(Me.ddlNendoNendo.SelectedValue)
        If dtData.Rows(0).Item(3).ToString = 0 Then
            '計画管理テーブル　に年度の対象データがない場合
            objCommon.SetShowMessage(Me, CommonMessage.MSG049E)
            '計画調査件数
            Me.lblKeikakuKensuu.Text = ""
            '計画金額
            Me.lblKeikakuUriKingaku.Text = ""
            '計画粗利
            Me.lblKeikakuArari.Text = ""

        Else
            '年度別計画集計値
            Dim dtKeikaku As Data.DataTable
            dtKeikaku = zensyaSyukeiInquiryBC.GetNendoKeikaku(Me.ddlNendoNendo.SelectedValue.ToString.Substring(0, 4))
            If dtKeikaku.Rows.Count > 0 Then
                '計画調査件数
                Me.lblKeikakuKensuu.Text = FormatNumber(dtKeikaku.Rows(0).Item(0).ToString, 0)
                '計画金額
                Me.lblKeikakuUriKingaku.Text = FormatNumber(dtKeikaku.Rows(0).Item(1).ToString, 0)
                '計画粗利
                Me.lblKeikakuArari.Text = FormatNumber(dtKeikaku.Rows(0).Item(2).ToString, 0)

            End If
        End If

        '実績値を取得
        Dim dtKensuuData As Data.DataTable
        dtKensuuData = zensyaSyukeiInquiryBC.GetNendoJisseki(Me.ddlNendoNendo.SelectedValue)
        If dtKensuuData.Rows(0).Item(2).ToString = 0 Then
            '実績管理テーブル　にデータがない場合
            objCommon.SetShowMessage(Me, CommonMessage.MSG050E)
            '実績件数
            Me.lblJissekiKensuu.Text = ""
            '実績金額
            Me.lblJissekiKingaku.Text = ""
            '実績粗利
            Me.lblJissekiSori.Text = ""
        Else
            '4月～3月実績件数の集計値
            Dim dtNendoJittuseki As Data.DataTable
            dtNendoJittuseki = zensyaSyukeiInquiryBC.GetNendoJissekiKensuu(Me.ddlNendoNendo.SelectedValue.ToString.Substring(0, 4))
            If dtNendoJittuseki.Rows.Count > 0 Then
                '実績件数
                Me.lblJissekiKensuu.Text = FormatNumber(dtNendoJittuseki.Rows(0).Item(0).ToString, 0)
            End If

            '実績金額,実績粗利の集計値
            Dim dtNendoHoka As Data.DataTable
            dtNendoHoka = zensyaSyukeiInquiryBC.GetNendoJisseki(Me.ddlNendoNendo.SelectedValue.ToString.Substring(0, 4))
            If dtNendoHoka.Rows.Count > 0 Then
                '実績金額
                Me.lblJissekiKingaku.Text = FormatNumber(dtNendoHoka.Rows(0).Item(0).ToString, 0)
                '実績粗利
                Me.lblJissekiSori.Text = FormatNumber(dtNendoHoka.Rows(0).Item(1).ToString, 0)
            End If
        End If
    End Sub
    ''' <summary>
    ''' 期間部分
    ''' </summary>
    ''' <history>																
    ''' <para>2012/12/3 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Public Sub btnKikanHyouji_Click()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        If Me.ddlNendoKikan.SelectedValue = 0 Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "年度")
            Me.ddlNendoKikan.Focus()
            Exit Sub
        End If
        '計画値を取得
        Dim dtData As Data.DataTable
        dtData = zensyaSyukeiInquiryBC.GetKikanKeikaku(Me.ddlNendoKikan.SelectedValue.ToString.Substring(0, 4), Me.ddlKikan.SelectedItem.Text)
        If dtData.Rows(0).Item(3).ToString = 0 Then
            '計画管理テーブル　に年度の対象データがない場合
            objCommon.SetShowMessage(Me, CommonMessage.MSG051E)
            '計画調査件数
            Me.lblKikanKensuu.Text = ""
            '計画金額
            Me.lblKikanUriKingaku.Text = ""
            '計画粗利
            Me.lblKikanArari.Text = ""

        Else
            '期間別計画集計値
            Dim dtKeikaku As Data.DataTable
            dtKeikaku = zensyaSyukeiInquiryBC.GetKikanKeikaku(Me.ddlNendoKikan.SelectedValue.ToString.Substring(0, 4), Me.ddlKikan.SelectedItem.Text)
            If dtKeikaku.Rows.Count > 0 Then
                '計画調査件数
                Me.lblKikanKensuu.Text = FormatNumber(dtKeikaku.Rows(0).Item(0).ToString, 0)
                '計画金額
                Me.lblKikanUriKingaku.Text = FormatNumber(dtKeikaku.Rows(0).Item(1).ToString, 0)
                '計画粗利
                Me.lblKikanArari.Text = FormatNumber(dtKeikaku.Rows(0).Item(2).ToString, 0)

            End If
        End If

        '実績値を取得
        Dim dtKensuuData As Data.DataTable
        dtKensuuData = zensyaSyukeiInquiryBC.GetKikanJisseki(Me.ddlNendoKikan.SelectedValue.ToString.Substring(0, 4), Me.ddlKikan.SelectedItem.Text)
        If dtKensuuData.Rows(0).Item(2).ToString = 0 Then
            '実績管理テーブル　にデータがない場合
            objCommon.SetShowMessage(Me, CommonMessage.MSG052E)
            '実績件数
            Me.lblKikanJissekiKensuu.Text = ""
            '実績金額
            Me.lblKikanJissekiKingaku.Text = ""
            '実績粗利
            Me.lblKikanJissekiSori.Text = ""
        Else

            '期間実績件数の集計値
            Dim dtJittuseki As Data.DataTable
            dtJittuseki = zensyaSyukeiInquiryBC.GetKikanJissekiKensuu(Me.ddlNendoKikan.SelectedValue.ToString.Substring(0, 4), Me.ddlKikan.SelectedItem.Text)
            If dtJittuseki.Rows.Count > 0 Then
                '実績件数
                Me.lblKikanJissekiKensuu.Text = FormatNumber(dtJittuseki.Rows(0).Item(0).ToString, 0)
            End If

            '実績金額,実績粗利の集計値
            Dim dtKikanHoka As Data.DataTable
            dtKikanHoka = zensyaSyukeiInquiryBC.GetKikanJisseki(Me.ddlNendoKikan.SelectedValue.ToString.Substring(0, 4), Me.ddlKikan.SelectedItem.Text)
            If dtKikanHoka.Rows.Count > 0 Then
                '実績金額
                Me.lblKikanJissekiKingaku.Text = FormatNumber(dtKikanHoka.Rows(0).Item(0).ToString, 0)
                '実績粗利
                Me.lblKikanJissekiSori.Text = FormatNumber(dtKikanHoka.Rows(0).Item(1).ToString, 0)

            End If
        End If
    End Sub
    ''' <summary>
    ''' 月別部分
    ''' </summary>
    ''' <history>																
    ''' <para>2012/12/3 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Public Sub btnTukiHyouji_Click()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        If Me.ddlNendoTuki.SelectedValue = 0 Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "年度")
            Me.ddlNendoTuki.Focus()
            Exit Sub
        End If
        If Me.ddlBeginTuki.SelectedValue = "0" Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG048E)
            Me.ddlBeginTuki.Focus()
            Exit Sub
        End If
        '計画値を取得
        Dim dtData As Data.DataTable
        dtData = zensyaSyukeiInquiryBC.GetTukiKeikaku(Me.ddlNendoTuki.SelectedValue.ToString.Substring(0, 4), Me.ddlBeginTuki.SelectedItem.Value, Me.ddlEndTuki.SelectedItem.Value)
        If dtData.Rows(0).Item(3).ToString = 0 Then
            '計画管理テーブル　に年度の対象データがない場合
            objCommon.SetShowMessage(Me, CommonMessage.MSG053E)
            '計画調査件数
            Me.lblTukiKensuu.Text = ""
            '計画金額
            Me.lblTukiUriKingaku.Text = ""
            '計画粗利
            Me.lblTukiArari.Text = ""

        Else
            '月別計画集計値
            Dim dtTukiKeikaku As Data.DataTable
            dtTukiKeikaku = zensyaSyukeiInquiryBC.GetTukiKeikaku(Me.ddlNendoTuki.SelectedValue.ToString.Substring(0, 4), Me.ddlBeginTuki.SelectedItem.Value, Me.ddlEndTuki.SelectedItem.Value)
            If dtTukiKeikaku.Rows.Count > 0 Then
                '計画調査件数
                Me.lblTukiKensuu.Text = FormatNumber(dtTukiKeikaku.Rows(0).Item(0).ToString, 0)
                '計画金額
                Me.lblTukiUriKingaku.Text = FormatNumber(dtTukiKeikaku.Rows(0).Item(1).ToString, 0)
                '計画粗利
                Me.lblTukiArari.Text = FormatNumber(dtTukiKeikaku.Rows(0).Item(2).ToString, 0)
            End If
        End If
        '実績値を取得
        Dim dtKensuuData As Data.DataTable
        dtKensuuData = zensyaSyukeiInquiryBC.GetTukiJisseki(Me.ddlNendoTuki.SelectedValue.ToString.Substring(0, 4), Me.ddlBeginTuki.SelectedItem.Value, Me.ddlEndTuki.SelectedItem.Value)
        If dtKensuuData.Rows(0).Item(2).ToString = 0 Then
            '実績管理テーブル　にデータがない場合
            objCommon.SetShowMessage(Me, CommonMessage.MSG054E)
            '実績件数
            Me.lblTukiJissekiKensuu.Text = ""
            '実績金額
            Me.lblTukiJissekiKingaku.Text = ""
            '実績粗利
            Me.lblTukiJissekiSori.Text = ""
        Else

            '月別実績件数の集計値
            Dim dtJittusekiKensuu As Data.DataTable
            dtJittusekiKensuu = zensyaSyukeiInquiryBC.GetTukiJissekiKensuu(Me.ddlNendoTuki.SelectedValue.ToString.Substring(0, 4), Me.ddlBeginTuki.SelectedItem.Value, Me.ddlEndTuki.SelectedItem.Value)
            If dtJittusekiKensuu.Rows.Count > 0 Then
                '実績件数
                Me.lblTukiJissekiKensuu.Text = FormatNumber(dtJittusekiKensuu.Rows(0).Item(0).ToString, 0)
            End If

            '実績金額,実績粗利の集計値
            Dim dtTukiHoka As Data.DataTable
            dtTukiHoka = zensyaSyukeiInquiryBC.GetTukiJisseki(Me.ddlNendoTuki.SelectedValue.ToString.Substring(0, 4), Me.ddlBeginTuki.SelectedItem.Value, Me.ddlEndTuki.SelectedItem.Value)
            If dtTukiHoka.Rows.Count > 0 Then
                '実績金額
                Me.lblTukiJissekiKingaku.Text = FormatNumber(dtTukiHoka.Rows(0).Item(0).ToString, 0)
                '実績粗利
                Me.lblTukiJissekiSori.Text = FormatNumber(dtTukiHoka.Rows(0).Item(1).ToString, 0)

            End If
        End If
    End Sub
    ''' <summary>
    ''' 年度詳細ボタン
    ''' </summary>
    ''' <history>																
    ''' <para>2012/12/3 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Public Sub btnSyosai_Click()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        If Me.ddlNendoNendo.SelectedValue = 0 Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "年度")
            Me.ddlNendoNendo.Focus()
            Exit Sub
        End If
        'Server.Transfer("ZensyaSyukeiDetails.aspx?strYear=" + Me.ddlNendoNendo.SelectedValue)
        Dim csScript As New StringBuilder
        Dim csName As String = "setNendoScript"
        csScript.AppendLine("window.open('./ZensyaSyukeiDetails.aspx?strYear='+$ID('" & Me.ddlNendoNendo.ClientID & "').value,'','menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=1010,height=700,top=0,left=0');")
        ClientScript.RegisterStartupScript(Me.GetType(), csName, csScript.ToString, True)
    End Sub
    ''' <summary>
    ''' 期間詳細ボタン
    ''' </summary>
    ''' <history>																
    ''' <para>2012/12/3 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Public Sub btnKikanSyousai_Click()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        If Me.ddlNendoKikan.SelectedValue = 0 Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "年度")
            Me.ddlNendoKikan.Focus()
            Exit Sub
        End If
        Dim csScript As New StringBuilder
        Dim csName As String = "setKikanScript"
        csScript.AppendLine("window.open('./ZensyaSyukeiDetails.aspx?strYear='+$ID('" & Me.ddlNendoKikan.ClientID & "').value+'&strKi='+$ID('" & Me.ddlKikan.ClientID & "').value,'','menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=1010,height=700,top=0,left=0');")
        ClientScript.RegisterStartupScript(Me.GetType(), csName, csScript.ToString, True)

    End Sub
    ''' <summary>
    ''' 月別詳細ボタン
    ''' </summary>
    ''' <history>																
    ''' <para>2012/12/3 P-44979 趙冬雪(大連情報システム部)  新規作成 </para>																															
    ''' </history>
    Public Sub btnTukiSyousai_Click()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        If Me.ddlNendoTuki.SelectedValue = 0 Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "年度")
            Me.ddlNendoTuki.Focus()
            Exit Sub
        End If
        If Me.ddlBeginTuki.SelectedValue = "0" Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG048E)
            Me.ddlBeginTuki.Focus()
            Exit Sub
        End If
        
        Dim csScript As New StringBuilder
        Dim csName As String = "setScript"

        Dim beginTuki As String = Me.ddlBeginTuki.SelectedItem.Text.Replace("月", "")
        Dim endTuki As String = Me.ddlEndTuki.SelectedItem.Text.Replace("月", "")
        If Me.ddlEndTuki.SelectedItem.Text <> String.Empty Then
        
            csScript.AppendLine("window.open('./ZensyaSyukeiDetails.aspx?strYear='+$ID('" & Me.ddlNendoTuki.ClientID & "').value+'&strBeginMonth='+('" & beginTuki & "')+'&strEndMonth='+('" & endTuki & "'),'','menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=1010,height=700,top=0,left=0');")
        Else
            
            csScript.AppendLine("window.open('./ZensyaSyukeiDetails.aspx?strYear='+$ID('" & Me.ddlNendoTuki.ClientID & "').value+'&strBeginMonth='+('" & beginTuki & "')+'&strEndMonth=','','menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=1010,height=700,top=00,left=0');")
        End If

        ClientScript.RegisterStartupScript(Me.GetType(), csName, csScript.ToString, True)

    End Sub
#End Region

End Class
