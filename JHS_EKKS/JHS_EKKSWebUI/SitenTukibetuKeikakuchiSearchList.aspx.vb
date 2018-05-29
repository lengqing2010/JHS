Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.BizLogic
Imports Lixil.JHS_EKKS.Utilities
Imports System.Data

''' <summary>
''' 支店 月別計画値照会
''' </summary>
''' <remarks>支店 月別計画値照会</remarks>
''' <history>
''' <para>2012/11/24 P-44979 張紅 新規作成 </para>
''' </history>
Partial Class SitenTukibetuKeikakuchiSearchList
    Inherits System.Web.UI.Page

#Region "プライベート変数"

    Private objCommon As New Common
    Private objSitenTukibetuKeikakuchiSearchListBC As New Lixil.JHS_EKKS.BizLogic.SitenTukibetuKeikakuchiSearchListBC
    Public CommonConstBC As Lixil.JHS_EKKS.BizLogic.CommonConstBC

#End Region

#Region "定数"

    Private Const CON_TITLE As String = "支店 月別計画値設定"
    'パース
    Public Const sDirName As String = "C:\jhs_ekks\"
    'サービスパース
    Public sv_templist As String = getfiledatetimelist("data")

#End Region

#Region "イベント"
    ''' <summary>
    ''' Page_Load
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        'ユーザー権限のチェック
        Dim CommonCheck As New CommonCheck
        CommonCheck.CommonNinsyou(String.Empty, Master.loginUserInfo, kegen.UserIdOnly)

        Call check()

        If Not IsPostBack Then

            Call PageInit()

            '「Excel出力」ボタンを押下する
            Me.btnSyuturyoku1.OnClientClick = "body_onLoad(""1"");return false;"
        Else
            Select Case hidSeni.Value
                Case "1" 'Excel出力

                    MakePopJavaScript()
                    ClientScript.RegisterStartupScript(Me.GetType(), "ERR", "setTimeout('PopPrint()',10);", True)
            End Select
            hidSeni.Value = ""

        End If

        'JavaScriptを作成する
        Call MakeJavaScript()

        '画面のJS EVENT設定
        Call SetJsEvent()

    End Sub

    ''' <summary>
    ''' 支店押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Protected Sub btnShitenKensaku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShitenKensaku.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        Call ShowPop(True)

    End Sub

    ''' <summary>
    ''' 月別計画表示ボタンをクリックする場合
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Protected Sub btnKensaku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensaku.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        'チェックを行う
        If CheckInput() = False Then
            '画面初期のセット
            Me.divMeisai.Visible = False
            Me.btnTorikomi.Enabled = False
            Me.btnKeikakuMinaosi.Enabled = False
            Me.btnSitenbetuTukiConfirm.Enabled = False
            Me.lblSumi.Text = ""
            Exit Sub
        End If

        Dim strNendo As String
        Dim strBusyoCd As String
        Dim addDate As New ArrayList
        Dim addEigyouKbn As New ArrayList
        strNendo = Me.ddlNendo.SelectedValue.Trim.ToString
        strBusyoCd = Me.hidCitenCd.Value.Trim.ToString

        '計画データ
        Dim dtKeikaku As New Data.DataTable
        dtKeikaku = objSitenTukibetuKeikakuchiSearchListBC.GetSitenbetuTukiKeikakuKanri(strNendo, strBusyoCd)
        '去年データ
        Dim dtLastYear As New Data.DataTable
        dtLastYear = objSitenTukibetuKeikakuchiSearchListBC.GetJissekiKanri(strNendo, strBusyoCd)

        '該当データが存在しない場合、
        If dtKeikaku.Rows.Count = 0 AndAlso dtLastYear.Rows.Count = 0 Then
            'メッセージを表示する
            objCommon.SetShowMessage(Me, CommonMessage.MSG011E)
            '画面初期のセット
            Me.divMeisai.Visible = False
            Me.btnTorikomi.Enabled = True
            Me.btnKeikakuMinaosi.Enabled = False
            Me.btnSitenbetuTukiConfirm.Enabled = False
            lblSumi.Text = ""

            Exit Sub
        Else
            Me.divMeisai.Visible = True
        End If

        If dtKeikaku.Rows.Count = 0 Then
            'ボタンの制御
            Me.btnTorikomi.Enabled = True
            Me.btnSitenbetuTukiConfirm.Enabled = False
            Me.btnKeikakuMinaosi.Enabled = False
            lblSumi.Text = ""

        Else

            '登録日時の格納
            For i As Integer = 0 To dtKeikaku.Rows.Count - 1
                addDate.Add(dtKeikaku.Rows(i).Item("add_datetime"))
                addEigyouKbn.Add(dtKeikaku.Rows(i).Item("eigyou_kbn"))
            Next

            '計画値不変FLG
            If dtKeikaku.Rows(0).Item("kakutei_flg").ToString = "1" Then
                Me.btnTorikomi.Enabled = False
                Me.btnSitenbetuTukiConfirm.Enabled = False
                lblSumi.Text = "「計画済」"
                lblSumi.Style.Add("color", "blue")

                '計画値不変FLG
                If dtKeikaku.Rows(0).Item("keikaku_huhen_flg").ToString = "1" Then
                    Me.btnKeikakuMinaosi.Enabled = False
                Else
                    Me.btnKeikakuMinaosi.Enabled = True
                End If

            Else
                Me.btnSitenbetuTukiConfirm.Enabled = True
                Me.btnKeikakuMinaosi.Enabled = False
                lblSumi.Text = ""

                '計画値不変FLG
                If dtKeikaku.Rows(0).Item("keikaku_huhen_flg").ToString = "1" Then
                    Me.btnTorikomi.Enabled = False
                Else
                    Me.btnTorikomi.Enabled = True
                End If

            End If

            If dtKeikaku.Rows.Count < 3 Then
                '事業別にデータの作成
                dtKeikaku = MeisaiDataSakusei(dtKeikaku)
            End If


        End If

        If dtLastYear.Rows.Count < 3 AndAlso dtLastYear.Rows.Count <> 0 Then
            '事業別にデータの作成
            dtLastYear = MeisaiDataSakusei(dtLastYear)
        End If

        '明細のデータセット
        SetMeisaiData(dtKeikaku, dtLastYear)

        ViewState("nendo") = strNendo
        ViewState("busyoCd") = strBusyoCd
        ViewState("busyoMei") = Me.tbxSiten.Text.ToString
        ViewState("addDate") = addDate
        ViewState("addEigyouKbn") = addEigyouKbn
    End Sub

#End Region

#Region "メンッド"

    ''' <summary>
    ''' 画面初期化
    ''' </summary>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Private Sub PageInit()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim CommonBC As New CommonBC
        '年度　選択のセット
        Dim dtKeikakuNendo As Data.DataTable = CommonBC.GetKeikakuNendoData()
        ddlNendo.DataSource = dtKeikakuNendo
        ddlNendo.DataBind()
        Me.ddlNendo.SelectedIndex = -1

        Dim strSysNen As String
        'システム年度を取得する
        strSysNen = objCommon.GetSystemYear()

        'システム年度を設定する
        For i As Integer = 0 To Me.ddlNendo.Items.Count - 1
            If Me.ddlNendo.Items(i).Value.Equals(strSysNen) Then
                Me.ddlNendo.Items(i).Selected = True
                Exit For
            End If
        Next

        '画面初期のセット
        Me.divMeisai.Visible = False
        Me.btnTorikomi.Enabled = False
        Me.btnKeikakuMinaosi.Enabled = False
        Me.btnSitenbetuTukiConfirm.Enabled = False
        Me.lblSumi.Text = ""

    End Sub

    ''' <summary>
    ''' 共通POPUP
    ''' </summary>
    ''' <param name="blnPop"></param>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Private Function ShowPop(ByVal blnPop As Boolean) As Integer

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, blnPop)

        Dim csScript As New StringBuilder

        Dim SitenSearchBC As New SitenSearchBC
        Dim dtSiten As Data.DataTable
        If blnPop Then
            dtSiten = SitenSearchBC.GetBusyoKanri("0", tbxSiten.Text, False)
        Else
            dtSiten = SitenSearchBC.GetBusyoKanri("0", tbxSiten.Text, False, False)
        End If

        ShowPop = dtSiten.Rows.Count
        If dtSiten.Rows.Count = 1 Then
            Me.tbxSiten.Text = dtSiten.Rows(0).Item(0).ToString
            Me.hidCitenCd.Value = dtSiten.Rows(0).Item(1).ToString
        Else
            If blnPop Then
                'popUp検索の場合
                csScript.AppendLine("window.open('./PopupSearch/SitenSearch.aspx?formName=" & Me.Form.ClientID & "&strSitenMei='+ escape($ID('" & Me.tbxSiten.ClientID & "').value)+'&field=" & Me.tbxSiten.ClientID & "'+'&fieldCd=" & Me.hidCitenCd.ClientID & "', 'SitenSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
                'ページ応答で、クライアント側のスクリプト ブロックを出力します
                ClientScript.RegisterStartupScript(Me.GetType(), "1", csScript.ToString, True)
            Else
                '複数の場合
                If ShowPop > 1 Then
                    If Me.hidCitenCd.Value.ToString.Trim <> String.Empty Then
                        For i As Integer = 0 To dtSiten.Rows.Count - 1
                            If Me.hidCitenCd.Value.ToString.Trim = dtSiten.Rows(i).Item(1).ToString() Then
                                ShowPop = 1
                                Exit For
                            End If
                        Next

                    End If
                End If

            End If
        End If

    End Function

    ''' <summary>
    ''' JavaScriptを作成する
    ''' </summary>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Private Sub MakeJavaScript()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")

            .AppendLine("</script>  ")
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

    ''' <summary>
    ''' 画面のJS EVENT設定
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Private Sub SetJsEvent()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        'CSV出力ボタン押下時
        Me.btnSyuturyoku.OnClick = "BtnSyuturyoku_Click()"

        'CSV取り込みボタン押下時
        Me.btnTorikomi.Button.Attributes.Add("onClick", "window.open('SitenTukibetuKeikakuchiInput.aspx', 'PopupCSVInput');return false;")

        '計画見直しボタン押下時
        Me.btnKeikakuMinaosi.OnClick = "BtnKeikakuMinaosi_Click()"

        '支店別 月別計画値確定ボタン押下時
        Me.btnSitenbetuTukiConfirm.OnClick = "BtnSitenbetuTukiConfirm_Click()"

        Me.tbxSiten.Attributes("onkeydown") = "if (event.keyCode==13){event.keyCode=9;}"

    End Sub

    ''' <summary>
    ''' CSV出力ボタンをクリックする時
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Sub BtnSyuturyoku_Click()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        'チェックを行う
        If CheckInput() = False Then
            Exit Sub
        End If

        '画面遷移
        Dim csScript As New StringBuilder

        With csScript
            .AppendLine("function window.onload()")
            .AppendLine("{")
            .AppendLine("  $ID('" & Me.btnSyuturyoku1.ClientID & "').click();")
            .AppendLine("}")
        End With

        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ClientScript.RegisterStartupScript(Me.GetType(), "PopupWaitMsg", csScript.ToString, True)

    End Sub

    ''' <summary>
    ''' 計画見直しボタンをクリックする時
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Sub BtnKeikakuMinaosi_Click()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        If objSitenTukibetuKeikakuchiSearchListBC.SetKakuteiFlg(ViewState("nendo").ToString, _
                                                                ViewState("busyoCd").ToString, _
                                                                ViewState("addDate"), _
                                                                ViewState("addEigyouKbn"), _
                                                                Master.loginUserInfo.Items(0).Account.ToString, _
                                                                "0") Then
            Me.btnTorikomi.Enabled = True
            Me.btnSitenbetuTukiConfirm.Enabled = True
            Me.btnKeikakuMinaosi.Enabled = False
            Me.lblSumi.Text = ""

        End If

        '画面の再表示
        Dim strNendo As String
        Dim strBusyoCd As String
        strNendo = ViewState("nendo").ToString
        strBusyoCd = ViewState("busyoCd").ToString
        '画面のセット
        Me.tbxSiten.Text = ViewState("busyoMei").ToString
        Me.ddlNendo.SelectedValue = strNendo
        Me.hidCitenCd.Value = strBusyoCd

        '計画データ
        Dim dtKeikaku As New Data.DataTable
        dtKeikaku = objSitenTukibetuKeikakuchiSearchListBC.GetSitenbetuTukiKeikakuKanri(strNendo, strBusyoCd)
        '去年データ
        Dim dtLastYear As New Data.DataTable
        dtLastYear = objSitenTukibetuKeikakuchiSearchListBC.GetJissekiKanri(strNendo, strBusyoCd)

        If dtKeikaku.Rows.Count < 3 Then
            '事業別にデータの作成
            dtKeikaku = MeisaiDataSakusei(dtKeikaku)
        End If

        '計画値不変FLG
        If dtKeikaku.Rows(0).Item("keikaku_huhen_flg").ToString = "1" Then
            Me.btnTorikomi.Enabled = False
        End If

        If dtLastYear.Rows.Count < 3 AndAlso dtLastYear.Rows.Count <> 0 Then
            '事業別にデータの作成
            dtLastYear = MeisaiDataSakusei(dtLastYear)
        End If

        '明細のデータセット
        SetMeisaiData(dtKeikaku, dtLastYear)

    End Sub

    ''' <summary>
    ''' 支店別 月別計画値確定ボタンをクリックする時
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Sub BtnSitenbetuTukiConfirm_Click()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)


        If objSitenTukibetuKeikakuchiSearchListBC.SetKakuteiFlg(ViewState("nendo").ToString, _
                                                                    ViewState("busyoCd").ToString, _
                                                                    ViewState("addDate"), _
                                                                    ViewState("addEigyouKbn"), _
                                                                    Master.loginUserInfo.Items(0).Account.ToString, _
                                                                    "1") Then
            Me.btnTorikomi.Enabled = False
            Me.btnSitenbetuTukiConfirm.Enabled = False
            Me.btnKeikakuMinaosi.Enabled = True

            Me.lblSumi.Text = "「計画済」"
            Me.lblSumi.Style.Add("color", "blue")

        End If

        '画面の再表示
        Dim strNendo As String
        Dim strBusyoCd As String
        strNendo = ViewState("nendo").ToString
        strBusyoCd = ViewState("busyoCd").ToString

        '計画データ
        Dim dtKeikaku As New Data.DataTable
        dtKeikaku = objSitenTukibetuKeikakuchiSearchListBC.GetSitenbetuTukiKeikakuKanri(strNendo, strBusyoCd)
        '去年データ
        Dim dtLastYear As New Data.DataTable
        dtLastYear = objSitenTukibetuKeikakuchiSearchListBC.GetJissekiKanri(strNendo, strBusyoCd)

        If dtKeikaku.Rows.Count < 3 Then
            '事業別にデータの作成
            dtKeikaku = MeisaiDataSakusei(dtKeikaku)
        End If

        If dtLastYear.Rows.Count < 3 AndAlso dtLastYear.Rows.Count <> 0 Then
            '事業別にデータの作成
            dtLastYear = MeisaiDataSakusei(dtLastYear)
        End If

        '計画値不変FLG
        If dtKeikaku.Rows(0).Item("keikaku_huhen_flg").ToString = "1" Then
            Me.btnKeikakuMinaosi.Enabled = False
        End If

        '明細のデータセット
        SetMeisaiData(dtKeikaku, dtLastYear)

    End Sub

    ''' <summary>
    ''' 明細データを作成する
    ''' </summary>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Private Function MeisaiDataSakusei(ByVal dtTableBefore As Data.DataTable) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim dtTable As New Data.DataTable
        Dim drEigyouKbn As Data.DataRow()

        dtTable = dtTableBefore.Copy
        dtTable.Clear()

        drEigyouKbn = dtTableBefore.Select(" eigyou_kbn = '1' ")
        '営業
        If drEigyouKbn.Length = 0 Then
            dtTable.Rows.Add()
            dtTable.Rows(0).Item("eigyou_kbn") = "1"
        Else
            dtTable.Rows.Add(drEigyouKbn(0).ItemArray)
        End If
        '特販
        drEigyouKbn = dtTableBefore.Select(" eigyou_kbn = '3' ")
        If drEigyouKbn.Length = 0 Then
            dtTable.Rows.Add()
            dtTable.Rows(1).Item("eigyou_kbn") = "3"
        Else
            dtTable.Rows.Add(drEigyouKbn(0).ItemArray)
        End If
        'ＦＣ
        drEigyouKbn = dtTableBefore.Select(" eigyou_kbn = '4' ")
        If drEigyouKbn.Length = 0 Then
            dtTable.Rows.Add()
            dtTable.Rows(2).Item("eigyou_kbn") = "4"
        Else
            dtTable.Rows.Add(drEigyouKbn(0).ItemArray)
        End If

        Return dtTable

    End Function

    ''' <summary>
    ''' 入力チェックチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckInput() As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        If Me.ddlNendo.SelectedValue = "" Then
            'メッセージを表示する
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "年度 選択")
            Me.ddlNendo.Focus()
            Return False
        End If

        If Me.tbxSiten.Text.Trim = "" Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG001E, "支店名")
            Me.tbxSiten.Focus()
            Return False
        End If
        Dim intSitenDataCount As Integer
        intSitenDataCount = ShowPop(False)
        If intSitenDataCount = 0 Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG022E, "支店")
            Me.tbxSiten.Focus()
            Return False

        ElseIf intSitenDataCount > 1 Then
            objCommon.SetShowMessage(Me, CommonMessage.MSG062E, "支店")
            Me.tbxSiten.Focus()
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 明細データのセット
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Sub SetMeisaiData(ByVal dtKeikaku As Data.DataTable, ByVal dtLastYear As Data.DataTable)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, dtKeikaku, dtLastYear)

        '月別
        SitenTukibetuKeikakuchiList4.ShowMeisaiData(dtKeikaku, dtLastYear, "4")
        SitenTukibetuKeikakuchiList5.ShowMeisaiData(dtKeikaku, dtLastYear, "5")
        SitenTukibetuKeikakuchiList6.ShowMeisaiData(dtKeikaku, dtLastYear, "6")
        SitenTukibetuKeikakuchiList7.ShowMeisaiData(dtKeikaku, dtLastYear, "7")
        SitenTukibetuKeikakuchiList8.ShowMeisaiData(dtKeikaku, dtLastYear, "8")
        SitenTukibetuKeikakuchiList9.ShowMeisaiData(dtKeikaku, dtLastYear, "9")
        SitenTukibetuKeikakuchiList10.ShowMeisaiData(dtKeikaku, dtLastYear, "10")
        SitenTukibetuKeikakuchiList11.ShowMeisaiData(dtKeikaku, dtLastYear, "11")
        SitenTukibetuKeikakuchiList12.ShowMeisaiData(dtKeikaku, dtLastYear, "12")
        SitenTukibetuKeikakuchiList1.ShowMeisaiData(dtKeikaku, dtLastYear, "1")
        SitenTukibetuKeikakuchiList2.ShowMeisaiData(dtKeikaku, dtLastYear, "2")
        SitenTukibetuKeikakuchiList3.ShowMeisaiData(dtKeikaku, dtLastYear, "3")
        '四半期
        SitenTukibetuKeikakuchiList456.ShowMeisaiData(dtKeikaku, dtLastYear, "456")
        SitenTukibetuKeikakuchiList789.ShowMeisaiData(dtKeikaku, dtLastYear, "789")
        SitenTukibetuKeikakuchiList101112.ShowMeisaiData(dtKeikaku, dtLastYear, "101112")
        SitenTukibetuKeikakuchiList123.ShowMeisaiData(dtKeikaku, dtLastYear, "123")
        '上期
        SitenTukibetuKeikakuchiListKamiki.ShowMeisaiData(dtKeikaku, dtLastYear, "kamiki")
        '下期
        SitenTukibetuKeikakuchiListSimoki.ShowMeisaiData(dtKeikaku, dtLastYear, "simoki")
        '年度集計
        SitenTukibetuKeikakuchiListNendo.ShowMeisaiData(dtKeikaku, dtLastYear, "nendo")

    End Sub

    ''' <summary>
    ''' EXCELテンプレートファイルチェク
    ''' </summary>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Private Sub check()
        Dim csType As Type = Page.GetType
        Dim csName As String = "check"
        Dim csScript As New StringBuilder

        csScript.Append("<script language='vbscript' type='text/vbscript'>" & vbCrLf)
        csScript.Append("function body_onLoad(obj)" & vbCrLf)
        csScript.Append("   Dim sv_templist,cr_templist,strHour, strMinute,strYEAR, strMONTH, strDAY" & vbCrLf)
        csScript.Append("   Dim TimeStamp,dwn_flg,c_obj_Fso,File_Path,c_obj_Folder,c_obj_File,fl,i" & vbCrLf)
        csScript.Append("   On Error Resume Next" & vbCrLf)

        csScript.Append("       dwn_flg = false" & vbCrLf)
        csScript.Append("       sv_templist=""" & sv_templist & """" & vbCrLf)
        csScript.Append("       cr_templist =""""" & vbCrLf)
        csScript.Append("       File_Path =""" & sDirName & """" & vbCrLf)
        csScript.Append("       If sv_templist <> """" Then" & vbCrLf)

        csScript.Append("           Set c_obj_Fso = CreateObject(""Scripting.FileSystemObject"")" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = c_obj_Fso.GetFolder(File_Path)" & vbCrLf)
        csScript.Append("           Set c_obj_File = c_obj_Folder.Files" & vbCrLf)
        csScript.Append("           For Each fl In c_obj_File" & vbCrLf)

        csScript.Append("           If (Ucase(right(fl.Name,3)) = ""XLT"" OR  Ucase(right(fl.Name,3)) = ""XLS"") Then" & vbCrLf)
        csScript.Append("                   If cr_templist <> """" Then" & vbCrLf)
        csScript.Append("                       cr_templist = cr_templist & "",""" & vbCrLf)
        csScript.Append("                   End If" & vbCrLf)
        csScript.Append("                   strYEAR   = Right(""0000"" & CStr(YEAR(fl.DateLastModified)), 4)" & vbCrLf)
        csScript.Append("                   strMONTH  = Right(""00"" & CStr(MONTH(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strDAY    = Right(""00"" & Cstr(DAY(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strHour   = Right(""00"" & CStr(Hour(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strMinute = Right(""00"" & CStr(Minute(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   timeStamp = strYEAR & strMONTH & strDAY & strHour & strMinute" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"" "","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,""/"","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"":"","""")" & vbCrLf)

        csScript.Append("                   cr_templist = cr_templist & trim(fl.Name) & "":"" & trim(timestamp)" & vbCrLf)

        csScript.Append("               End If" & vbCrLf)
        csScript.Append("           Next" & vbCrLf)

        csScript.Append("           Set c_obj_File = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Fso = Nothing" & vbCrLf)
        csScript.Append("           If Err > 0 or cr_templist = """" Then" & vbCrLf)
        csScript.Append("               dwn_flg = true" & vbCrLf)
        csScript.Append("           Else" & vbCrLf)
        csScript.Append("               ar_cr = split(Ucase(cr_templist), "","")" & vbCrLf)
        csScript.Append("               ar_sv = split(Ucase(sv_templist), "","")" & vbCrLf)
        csScript.Append("               For i = 0 to Ubound(ar_sv)" & vbCrLf)
        csScript.Append("                   rc = Filter(ar_cr, ar_sv(i))" & vbCrLf)
        csScript.Append("                   If Ubound(rc) = 0 then" & vbCrLf)
        csScript.Append("                       If rc(0) = """" then" & vbCrLf)
        csScript.Append("                           dwn_flg = true" & vbCrLf)
        csScript.Append("                           Exit For" & vbCrLf)
        csScript.Append("                       End if" & vbCrLf)
        csScript.Append("                   Else" & vbCrLf)
        csScript.Append("                       dwn_flg = true" & vbCrLf)
        csScript.Append("                       Exit For" & vbCrLf)
        csScript.Append("                   End if" & vbCrLf)
        csScript.Append("               Next" & vbCrLf)
        csScript.Append("           End if" & vbCrLf)
        csScript.Append("           If dwn_flg = true Then" & vbCrLf)
        csScript.Append("               call download(obj)" & vbCrLf)
        csScript.Append("           End If" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("       If dwn_flg = false Then" & vbCrLf)
        csScript.Append("           fncSubmit(obj)" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("End function" & vbCrLf)

        csScript.Append("function body_onLoad2(obj)" & vbCrLf)
        csScript.Append("   Dim sv_templist,cr_templist,strHour, strMinute,strYEAR, strMONTH, strDAY" & vbCrLf)
        csScript.Append("   Dim TimeStamp,dwn_flg,c_obj_Fso,File_Path,c_obj_Folder,c_obj_File,fl,i" & vbCrLf)
        csScript.Append("   On Error Resume Next" & vbCrLf)
        csScript.Append("       dwn_flg = false" & vbCrLf)
        csScript.Append("       sv_templist=""" & sv_templist & """" & vbCrLf)
        csScript.Append("       cr_templist =""""" & vbCrLf)
        csScript.Append("       File_Path =""" & sDirName & """" & vbCrLf)
        csScript.Append("       If sv_templist <> """" Then" & vbCrLf)
        csScript.Append("           Set c_obj_Fso = CreateObject(""Scripting.FileSystemObject"")" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = c_obj_Fso.GetFolder(File_Path)" & vbCrLf)
        csScript.Append("           Set c_obj_File = c_obj_Folder.Files" & vbCrLf)
        csScript.Append("           For Each fl In c_obj_File" & vbCrLf)
        csScript.Append("               If (Ucase(right(fl.Name,3)) = ""XLT"" OR  Ucase(right(fl.Name,3)) = ""XLS"") Then" & vbCrLf)
        csScript.Append("                   If cr_templist <> """" Then" & vbCrLf)
        csScript.Append("                       cr_templist = cr_templist & "",""" & vbCrLf)
        csScript.Append("                   End If" & vbCrLf)
        csScript.Append("                   strYEAR   = Right(""0000"" & CStr(YEAR(fl.DateLastModified)), 4)" & vbCrLf)
        csScript.Append("                   strMONTH  = Right(""00"" & CStr(MONTH(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strDAY    = Right(""00"" & Cstr(DAY(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strHour   = Right(""00"" & CStr(Hour(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   strMinute = Right(""00"" & CStr(Minute(fl.DateLastModified)), 2)" & vbCrLf)
        csScript.Append("                   timeStamp = strYEAR & strMONTH & strDAY & strHour & strMinute" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"" "","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,""/"","""")" & vbCrLf)
        csScript.Append("                   timestamp = replace(timestamp,"":"","""")" & vbCrLf)
        csScript.Append("                   cr_templist = cr_templist & trim(fl.Name) & "":"" & trim(timestamp)" & vbCrLf)
        csScript.Append("               End If" & vbCrLf)
        csScript.Append("           Next" & vbCrLf)

        csScript.Append("           Set c_obj_File = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Folder = Nothing" & vbCrLf)
        csScript.Append("           Set c_obj_Fso = Nothing" & vbCrLf)
        csScript.Append("           If Err > 0 or cr_templist = """" Then" & vbCrLf)
        csScript.Append("               dwn_flg = true" & vbCrLf)
        csScript.Append("           Else" & vbCrLf)
        csScript.Append("               ar_cr = split(Ucase(cr_templist), "","")" & vbCrLf)
        csScript.Append("               ar_sv = split(Ucase(sv_templist), "","")" & vbCrLf)
        csScript.Append("               For i = 0 to Ubound(ar_sv)" & vbCrLf)
        csScript.Append("                   rc = Filter(ar_cr, ar_sv(i))" & vbCrLf)
        csScript.Append("                   If Ubound(rc) = 0 then" & vbCrLf)
        csScript.Append("                       If rc(0) = """" then" & vbCrLf)
        csScript.Append("                           dwn_flg = true" & vbCrLf)
        csScript.Append("                           Exit For" & vbCrLf)
        csScript.Append("                       End if" & vbCrLf)
        csScript.Append("                   Else" & vbCrLf)
        csScript.Append("                       dwn_flg = true" & vbCrLf)
        csScript.Append("                       Exit For" & vbCrLf)
        csScript.Append("                   End if" & vbCrLf)
        csScript.Append("               Next" & vbCrLf)
        csScript.Append("           End if" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("       If dwn_flg = false Then" & vbCrLf)
        csScript.Append("           call fncSubmit(obj)" & vbCrLf)
        csScript.Append("       else" & vbCrLf)
        csScript.Append("           call body_load3(obj)" & vbCrLf)
        csScript.Append("       End If" & vbCrLf)
        csScript.Append("End function" & vbCrLf)
        csScript.Append("</script>" & vbCrLf)

        csScript.Append("<script language='javascript' type='text/javascript'>" & vbCrLf)
        csScript.Append("function download(obj){" & vbCrLf)
        csScript.Append("   window.location.href='data/JHS_EKKS.lha';" & vbCrLf)
        csScript.Append("   body_load3(obj);" & vbCrLf)
        csScript.Append("}" & vbCrLf)

        csScript.Append("function body_load3(obj){" & vbCrLf)
        csScript.Append("   setTimeout('body_onLoad2(' + obj + ')',1000);" & vbCrLf)
        csScript.Append("}" & vbCrLf)

        csScript.Append("function fncSubmit(obj) {" & vbCrLf)

        csScript.Append(Form.Name & "." & hidSeni.ClientID & ".value = obj;" & vbCrLf)
        csScript.Append(Form.Name & ".submit();" & vbCrLf)
        csScript.Append("}" & vbCrLf)
        csScript.Append("</script>" & vbCrLf)

        ClientScript.RegisterStartupScript(csType, csName, csScript.ToString)

    End Sub

    ''' <summary>
    ''' JavaScript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Protected Sub MakePopJavaScript()

        Dim csType As Type = Page.GetType()
        Dim csName As String = "MakePopJavaScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")
            .AppendLine("    function PopPrint(){")
            .AppendLine("       ShowModal();")
            .AppendLine("       var objwindow=window.open(encodeURI('WaitMsg.aspx?url=SitenTukibetuKeikakuchiExcelOutput.aspx?strNo='+escape('" & Me.ddlNendo.SelectedValue.Trim.ToString & "," & Me.hidCitenCd.Value.Trim.ToString & "," & Me.tbxSiten.Text.Trim.ToString & "')+'|divID=" & Me.Master.DivBuySelName.ClientID & "," & Me.Master.DivDisableId.ClientID & "'),'proxy_operation','width=450,height=150,status=no,resizable=no,directories=no,scrollbars=no,left=0,top=0');" & vbCrLf)
            .AppendLine("       objwindow.focus();")
            .AppendLine("    }" & vbCrLf)
            .AppendLine("</script>")
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

    Private Function getfiledatetimelist(ByVal path As String) As String
        Dim fo As New Scripting.FileSystemObject
        Dim fp As String
        Dim fr As Scripting.Folder
        Dim fc As Scripting.Files
        Dim fl As Scripting.File
        Dim fname As String, s As String, timestamp As String
        Dim strHour As String, strMinute As String
        Dim strYEAR As String, strMONTH As String, strDAY As String

        fp = Server.MapPath(path)
        fr = fo.GetFolder(fp)
        fc = fr.Files
        s = ""
        For Each fl In fc
            fname = fl.Name
            If (UCase(Right(fname, 3)) = "XLT" Or UCase(Right(fname, 3)) = "XLS") Then
                If s <> "" Then
                    s = s & ","
                End If
                timestamp = CStr(fl.DateLastModified)
                ''日付時間軸の整形
                strYEAR = Right("0000" & CStr(Year(fl.DateLastModified)), 4)
                strMONTH = Right("00" & CStr(Month(fl.DateLastModified)), 2)
                strDAY = Right("00" & CStr(Day(fl.DateLastModified)), 2)
                strHour = Right("00" & CStr(Hour(fl.DateLastModified)), 2)
                strMinute = Right("00" & CStr(Minute(fl.DateLastModified)), 2)
                timestamp = strYEAR & strMONTH & strDAY & strHour & strMinute
                timestamp = Replace(timestamp, " ", "")
                timestamp = Replace(timestamp, "/", "")
                timestamp = Replace(timestamp, ":", "")
                s = s & Trim(fname) & ":" & Trim(timestamp)
            End If
        Next
        getfiledatetimelist = s
        fo = Nothing
        fp = Nothing
        fr = Nothing
        fc = Nothing
        fl = Nothing
    End Function

#End Region

End Class
