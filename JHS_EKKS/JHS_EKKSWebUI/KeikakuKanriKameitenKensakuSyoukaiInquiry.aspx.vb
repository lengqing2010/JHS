Imports Lixil.JHS_EKKS.BizLogic
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Messages = Lixil.JHS_EKKS.Utilities.CommonMessage
Imports System.Collections.Generic

Partial Class KeikakuKanriKameitenKensakuSyoukaiInquiry
    Inherits System.Web.UI.Page

    Protected CommonConstBC As CommonConstBC

    Protected gridviewRightId As String = String.Empty
    Protected tblRightId As String = String.Empty

    Private commonBC As New CommonBC
    Private keikakuKanriKameitenKensakuSyoukaiInquiryBC As New KeikakuKanriKameitenKensakuSyoukaiInquiryBC

    ''' <summary>
    ''' 画面の初期表示
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        Dim objCommonCheck As New CommonCheck
        Dim strUserID As String = ""
        objCommonCheck.CommonNinsyou(strUserID, Me.Master.loginUserInfo, kegen.UserIdOnly)
        Dim ninsyouBC As New NinsyouBC
        If ninsyouBC.GetUserInfo(strUserID).Items.Count = 0 Then
            Context.Items("strFailureMsg") = Messages.MSG023E
            Server.Transfer("./CommonErr.aspx")
        End If

        gridviewRightId = Me.grdItiranRight.ClientID
        tblRightId = Me.tableScrollV.ClientID

        If Not IsPostBack Then
            'Dim common As New Common
            'common.SetURL(Me, strUserID, "計画管理_加盟店検索照会指示")

            Dim arrUrl() As String = Me.Request.Url.OriginalString.Split("/")
            arrUrl(arrUrl.Length - 1) = "KeikakuKanriKameitenKensakuSyoukaiInquiry.aspx"
            commonBC.SetUserInfo(String.Join("/", arrUrl), strUserID, "計画管理_加盟店検索照会指示")

            'DropDownListを設定する
            Call Me.SetDropDownList()

            ''前次の検索条件
            'If Not Context.Items("Parameter") Is Nothing Then
            '    ViewState("Parameter") = Context.Items("Parameter")
            '    Call Me.SetBefore()
            'End If

        End If

        Call Me.MakeJavaScript()

        '検索実行
        Me.btnKensakujiltukou.OnClick = "btnKensakujiltukou_Click()"
        'Me.btnKensakujiltukou.Attributes.Add("onClick", "if(!fncKensakuCheck()){return false;};")

        '全区分
        Me.chkKubunAll.Attributes.Add("onClick", "fncSetKubunVal();")
        '加盟店検索
        Me.btnKameitenSearch1.Attributes.Add("onClick", "fncShowKameitenPopup('1');return false;")
        Me.btnKameitenSearch2.Attributes.Add("onClick", "fncShowKameitenPopup('2');return false;")
        '営業所検索
        Me.btnEigyousyoCdSearch1.Attributes.Add("onClick", "fncShowEigyousyoPopup('1');return false;")
        Me.btnEigyousyoCdSearch2.Attributes.Add("onClick", "fncShowEigyousyoPopup('2');return false;")
        '営業担当者
        'Me.btnEigyouTantousyaSearch.Attributes.Add("onClick", "fncShowEigyouTantousyaPopup();return false;")
        Me.tbxEigyouTantousyaCd.Attributes.Add("onPropertyChange", "fncSetMei('" & Me.tbxEigyouTantousyaCd.ClientID & "','" & Me.tbxEigyouTantousyaMei.ClientID & "');")
        Me.tbxEigyouTantousyaMei.Attributes.Add("readOnly", "true")
        '「営業担当者」と「系列」を変更する時
        '
        '系列
        Me.btnKeiretuSearch.Attributes.Add("onClick", "fncShowKeiretuPopup();return false;")
        Me.tbxKeiretuCd.Attributes.Add("onPropertyChange", "fncSetMei('" & Me.tbxKeiretuCd.ClientID & "','" & Me.tbxKeiretuMei.ClientID & "');")
        Me.tbxKeiretuMei.Attributes.Add("readOnly", "true")
        '検索クリア
        Me.btnKensakuClear.Attributes.Add("onClick", "fncClear();return false;")

        Me.btnKeisakuKameitenJyouhouSyoukai.Attributes.Add("disabled", "true")

    End Sub

    ''' <summary>
    ''' 「営業担当者」検索ボタンを押下する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Protected Sub btnEigyouTantousyaSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEigyouTantousyaSearch.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        Dim sitenSearchBC As New EigyouManSearchBC
        Dim dtUserInfo As New Data.DataTable

        dtUserInfo = sitenSearchBC.GetUserInfo("0", Me.tbxEigyouTantousyaCd.Text, "", False)

        If dtUserInfo.Rows.Count = 1 Then
            Me.tbxEigyouTantousyaMei.Text = dtUserInfo.Rows(0).Item(1).ToString
            Me.tbxEigyouTantousyaCd.Text = dtUserInfo.Rows(0).Item(0).ToString
        Else
            Dim csScript As New StringBuilder
            csScript.AppendLine("window.open('./PopupSearch/EigyouManSearch.aspx?formName=" & Me.Form.ClientID & "&strEigyouManCd='+escape($ID('" & Me.tbxEigyouTantousyaCd.ClientID & "').value)+'&strEigyouManMei=&field=" & Me.tbxEigyouTantousyaMei.ClientID & "'+'&fieldCd=" & Me.tbxEigyouTantousyaCd.ClientID & "', 'EigyouManSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            'ページ応答で、クライアント側のスクリプト ブロックを出力します
            ClientScript.RegisterStartupScript(Me.GetType(), "EigyouTantousyaSearch", csScript.ToString, True)
        End If

    End Sub

    ''' <summary>
    ''' 「検索実行」ボタンを押下する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Sub btnKensakujiltukou_Click()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '入力チェック
        If Not Me.CheckInput() Then
            Return
        End If

        '営業担当者を設定する
        Call Me.SetEigyouTantousya()

        '系列を設定する
        Call Me.SetKeiretu()

        '入力項目を取得する
        Dim dicPrm As New Dictionary(Of String, String)
        dicPrm = Me.GetParameter()

        '明細データを取得する
        Dim dtKameitenInfo As New Data.DataTable
        dtKameitenInfo = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetKameitenInfo(dicPrm)

        If dtKameitenInfo.Rows.Count > 0 Then
            Me.grdItiranLeft.DataSource = dtKameitenInfo
            Me.grdItiranLeft.DataBind()
            Me.grdItiranRight.DataSource = dtKameitenInfo
            Me.grdItiranRight.DataBind()

            'gridviewRightId = Me.grdItiranRight.ClientID
            'tblRightId = Me.tableScrollV.ClientID
        Else
            Me.grdItiranLeft.DataSource = Nothing
            Me.grdItiranLeft.DataBind()
            Me.grdItiranRight.DataSource = Nothing
            Me.grdItiranRight.DataBind()

            'gridviewRightId = String.Empty
            'tblRightId = String.Empty
        End If

        ViewState("Parameter") = dicPrm
        ViewState("Sort") = String.Empty
        Me.hidSelectRowIndex.Value = String.Empty
        Me.hidHScroll.Value = String.Empty
        Me.hidVScroll.Value = String.Empty

        '明細データの件数を取得する
        Dim intCount As Integer = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetKameitenCount(dicPrm)

        If Me.ddlKensakuJyouken.SelectedValue = "max" Then
            Me.lblCount.Text = intCount
            Me.lblCount.ForeColor = Drawing.Color.Black
        Else
            If intCount > Me.ddlKensakuJyouken.SelectedValue Then
                Me.lblCount.Text = Me.ddlKensakuJyouken.SelectedValue & " / " & intCount
                Me.lblCount.ForeColor = Drawing.Color.Red
            Else
                Me.lblCount.Text = dtKameitenInfo.Rows.Count
                Me.lblCount.ForeColor = Drawing.Color.Black
            End If
        End If

        If intCount = 0 Then
            Call Me.SetButtonVisible(False)
            Call Me.SetErrorMessage(Messages.MSG011E, String.Empty)
        Else
            Call Me.SetButtonVisible(True)
            Call Me.SetButtonColor()

            Me.btnKbnUp.ForeColor = Drawing.Color.IndianRed

            ViewState("dtKameitenInfo") = dtKameitenInfo
        End If

    End Sub

    ''' <summary>
    ''' 「計画_加盟店情報照会」ボタンを押下する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Protected Sub btnKeisakuKameitenJyouhouSyoukai_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKeisakuKameitenJyouhouSyoukai.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)


        Dim intRowIndex As Integer = Convert.ToInt32(Me.hidSelectRowIndex.Value)
        Dim strKameitenCd As String = String.Empty
        strKameitenCd = Me.grdItiranLeft.Rows(intRowIndex).Cells(3).Text

        Dim dicPrm As New Dictionary(Of String, String)
        dicPrm = CType(ViewState("Parameter"), Dictionary(Of String, String))
        dicPrm.Add("RowIndex", Me.hidSelectRowIndex.Value)
        dicPrm.Add("Sort", ViewState("Sort").ToString)
        dicPrm.Add("HScroll", Me.hidHScroll.Value)
        dicPrm.Add("VScroll", Me.hidVScroll.Value)


        Context.Items("KeikakuNendo") = Me.ddlYear.SelectedValue
        Context.Items("KameitenCd") = strKameitenCd
        Context.Items("Parameter") = dicPrm

        Server.Transfer("KeikakuKameitenJyouhouInquiry.aspx")

    End Sub


    ''' <summary>
    ''' DropDownListを設定する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Private Sub SetDropDownList()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim dtDdlData As Data.DataTable

        '年度
        Me.ddlYear.Items.Clear()

        dtDdlData = commonBC.GetKeikakuNendoData()

        Me.ddlYear.DataValueField = "code"
        Me.ddlYear.DataTextField = "meisyou"

        Me.ddlYear.DataSource = dtDdlData
        Me.ddlYear.DataBind()

        '   先頭行
        Me.ddlYear.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        'システム日時を取得する
        Dim strYear As String
        Dim objCommon As New Common
        strYear = objCommon.GetSystemYear()

        '   初期年度を設定する
        Me.ddlYear.SelectedIndex = -1

        '   システム年度を設定する
        For i As Integer = 0 To Me.ddlYear.Items.Count - 1
            If Me.ddlYear.Items(i).Value.Equals(Convert.ToString(strYear)) Then
                Me.ddlYear.Items(i).Selected = True
                Exit For
            End If
        Next
        Me.hidYear.Value = Me.ddlYear.SelectedValue

        '区分(1,2,3)
        Me.ddlKbn1.Items.Clear()
        Me.ddlKbn2.Items.Clear()
        Me.ddlKbn3.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetKbnInfo()

        Me.ddlKbn1.DataValueField = "cd"
        Me.ddlKbn1.DataTextField = "mei"
        Me.ddlKbn1.DataSource = dtDdlData
        Me.ddlKbn1.DataBind()
        Me.ddlKbn1.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        Me.ddlKbn2.DataValueField = "cd"
        Me.ddlKbn2.DataTextField = "mei"
        Me.ddlKbn2.DataSource = dtDdlData
        Me.ddlKbn2.DataBind()
        Me.ddlKbn2.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        Me.ddlKbn3.DataValueField = "cd"
        Me.ddlKbn3.DataTextField = "mei"
        Me.ddlKbn3.DataSource = dtDdlData
        Me.ddlKbn3.DataBind()
        Me.ddlKbn3.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '管轄支店
        Me.ddlKankatuSiten.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetSitenInfo()

        Me.ddlKankatuSiten.DataValueField = "busyo_cd"
        Me.ddlKankatuSiten.DataTextField = "busyo_mei"
        Me.ddlKankatuSiten.DataSource = dtDdlData
        Me.ddlKankatuSiten.DataBind()
        Me.ddlKankatuSiten.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '都道府県
        Me.ddlTodoufuken.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetTodoufukenInfo()

        Me.ddlTodoufuken.DataValueField = "cd"
        Me.ddlTodoufuken.DataTextField = "mei"
        Me.ddlTodoufuken.DataSource = dtDdlData
        Me.ddlTodoufuken.DataBind()
        Me.ddlTodoufuken.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '営業区分
        Me.ddlEigyouKbn.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetMeisyouInfo("05")
        Me.ddlEigyouKbn.DataValueField = "code"
        Me.ddlEigyouKbn.DataTextField = "meisyou"
        Me.ddlEigyouKbn.DataSource = dtDdlData
        Me.ddlEigyouKbn.DataBind()
        Me.ddlEigyouKbn.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '営業担当所属
        Me.ddlEigyouTantouSyozaku.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetKakutyouMeisyouInfo("30")
        Me.ddlEigyouTantouSyozaku.DataValueField = "code"
        Me.ddlEigyouTantouSyozaku.DataTextField = "meisyou"
        Me.ddlEigyouTantouSyozaku.DataSource = dtDdlData
        Me.ddlEigyouTantouSyozaku.DataBind()
        Me.ddlEigyouTantouSyozaku.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '業態
        Me.ddlGyoutai.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetMeisyouInfo("20")
        Me.ddlGyoutai.DataValueField = "code"
        Me.ddlGyoutai.DataTextField = "meisyou"
        Me.ddlGyoutai.DataSource = dtDdlData
        Me.ddlGyoutai.DataBind()
        Me.ddlGyoutai.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '加盟店属性1
        Me.ddlKameitenZokusei1.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetMeisyouInfo("21")
        Me.ddlKameitenZokusei1.DataValueField = "code"
        Me.ddlKameitenZokusei1.DataTextField = "meisyou"
        Me.ddlKameitenZokusei1.DataSource = dtDdlData
        Me.ddlKameitenZokusei1.DataBind()
        Me.ddlKameitenZokusei1.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '加盟店属性2
        Me.ddlKameitenZokusei2.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetMeisyouInfo("22")
        Me.ddlKameitenZokusei2.DataValueField = "code"
        Me.ddlKameitenZokusei2.DataTextField = "meisyou"
        Me.ddlKameitenZokusei2.DataSource = dtDdlData
        Me.ddlKameitenZokusei2.DataBind()
        Me.ddlKameitenZokusei2.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '加盟店属性3
        Me.ddlKameitenZokusei3.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetMeisyouInfo("23")
        Me.ddlKameitenZokusei3.DataValueField = "code"
        Me.ddlKameitenZokusei3.DataTextField = "meisyou"
        Me.ddlKameitenZokusei3.DataSource = dtDdlData
        Me.ddlKameitenZokusei3.DataBind()
        Me.ddlKameitenZokusei3.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '加盟店属性4
        Me.ddlKameitenZokusei4.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetKakutyouMeisyouInfo("21")
        Me.ddlKameitenZokusei4.DataValueField = "code"
        Me.ddlKameitenZokusei4.DataTextField = "meisyou"
        Me.ddlKameitenZokusei4.DataSource = dtDdlData
        Me.ddlKameitenZokusei4.DataBind()
        Me.ddlKameitenZokusei4.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '加盟店属性5
        Me.ddlKameitenZokusei5.Items.Clear()

        dtDdlData = keikakuKanriKameitenKensakuSyoukaiInquiryBC.GetKakutyouMeisyouInfo("22")
        Me.ddlKameitenZokusei5.DataValueField = "code"
        Me.ddlKameitenZokusei5.DataTextField = "meisyou"
        Me.ddlKameitenZokusei5.DataSource = dtDdlData
        Me.ddlKameitenZokusei5.DataBind()
        Me.ddlKameitenZokusei5.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '計画値有無
        Me.ddlKeisakutiUmu.Items.Clear()

        Me.ddlKeisakutiUmu.Items.Add(New ListItem(String.Empty, String.Empty))
        Me.ddlKeisakutiUmu.Items.Add(New ListItem("0：計画有", "0"))
        Me.ddlKeisakutiUmu.Items.Add(New ListItem("1：計画無", "1"))

    End Sub

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Private Function CheckInput() As Boolean

        Dim commonCheck As New CommonCheck
        Dim strErrorMessage As String = String.Empty

        '   「対象年度」の必須入力チェック
        If Me.ddlYear.SelectedValue.Equals(String.Empty) Then
            Call Me.SetErrorMessage(String.Format(Messages.MSG001E, "対象年度"), Me.ddlYear.ClientID)
            Return False
        End If

        '   「区分」の必須入力チェック
        If (Not Me.chkKubunAll.Checked) AndAlso Me.ddlKbn1.SelectedValue.Equals(String.Empty) AndAlso Me.ddlKbn2.SelectedValue.Equals(String.Empty) AndAlso Me.ddlKbn3.SelectedValue.Equals(String.Empty) Then
            Call Me.SetErrorMessage(String.Format(Messages.MSG001E, "区分"), Me.ddlKbn1.ClientID)
            Return False
        End If

        '   「加盟店名」
        If Not Me.tbxKameitenMei.Text.Trim.Equals(String.Empty) Then
            '   禁止文字チェック
            If Not commonCheck.kinsiStrCheck(Me.tbxKameitenMei.Text.Trim) Then
                Call Me.SetErrorMessage(String.Format(Messages.MSG033E, "加盟店名"), Me.tbxKameitenMei.ClientID)
                Return False
            End If

            '   バイト数チェック
            strErrorMessage = commonCheck.CheckByte(Me.tbxKameitenMei.Text.Trim, 40, "加盟店名", kbn.ZENKAKU)
            If Not strErrorMessage.Equals(String.Empty) Then
                Call Me.SetErrorMessage(String.Format(Messages.MSG073E, "加盟店名"), Me.tbxKameitenMei.ClientID)
                Return False
            End If
        End If

        '   「加盟店コード(FROM)」
        If Not Me.tbxKameitenCd1.Text.Trim.Equals(String.Empty) Then
            '   半角英数チェック
            strErrorMessage = commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCd1.Text.Trim, "加盟店コード")
            If Not strErrorMessage.Equals(String.Empty) Then
                Call Me.SetErrorMessage(strErrorMessage, Me.tbxKameitenCd1.ClientID)
                Return False
            End If
        End If

        '   「加盟店コード(TO)」
        If Not Me.tbxKameitenCd2.Text.Trim.Equals(String.Empty) Then
            '   半角英数チェック
            strErrorMessage = commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCd2.Text.Trim, "加盟店コード")
            If Not strErrorMessage.Equals(String.Empty) Then
                Call Me.SetErrorMessage(strErrorMessage, Me.tbxKameitenCd2.ClientID)
                Return False
            End If

            '   範囲チェック
            If Me.tbxKameitenCd1.Text.Trim.Equals(String.Empty) Then
                '   加盟店(FROM)が未入力の場合
                Call Me.SetErrorMessage(String.Format(Messages.MSG074E, "加盟店コード(From)"), Me.tbxKameitenCd1.ClientID)
                Return False
            Else
                If String.Compare(Me.tbxKameitenCd1.Text.Trim, Me.tbxKameitenCd2.Text.Trim) > 0 Then
                    '   加盟店コード(FROM) > 加盟店コード(TO)の場合
                    Call Me.SetErrorMessage(String.Format(Messages.MSG024E, "加盟店コード", "加盟店コード"), Me.tbxKameitenCd2.ClientID)
                    Return False
                End If
            End If
        End If

        '   「営業所コード(FROM)」
        If Not Me.tbxEigyousyoCd1.Text.Trim.Equals(String.Empty) Then
            '   半角英数チェック
            strErrorMessage = commonCheck.ChkHankakuEisuuji(Me.tbxEigyousyoCd1.Text.Trim, "営業所コード")
            If Not strErrorMessage.Equals(String.Empty) Then
                Call Me.SetErrorMessage(strErrorMessage, Me.tbxEigyousyoCd1.ClientID)
                Return False
            End If
        End If

        '   「営業所コード(TO)」
        If Not Me.tbxEigyousyoCd2.Text.Trim.Equals(String.Empty) Then
            '   半角英数チェック
            strErrorMessage = commonCheck.ChkHankakuEisuuji(Me.tbxEigyousyoCd2.Text.Trim, "営業所コード")
            If Not strErrorMessage.Equals(String.Empty) Then
                Call Me.SetErrorMessage(strErrorMessage, Me.tbxEigyousyoCd2.ClientID)
                Return False
            End If

            '   範囲チェック
            If Me.tbxEigyousyoCd1.Text.Trim.Equals(String.Empty) Then
                '   営業所(FROM)が未入力の場合
                Call Me.SetErrorMessage(String.Format(Messages.MSG074E, "営業所コード(From)"), Me.tbxEigyousyoCd1.ClientID)
                Return False
            Else
                If String.Compare(Me.tbxEigyousyoCd1.Text.Trim, Me.tbxEigyousyoCd2.Text.Trim) > 0 Then
                    '   営業所コード(FROM) > 営業所コード(TO)の場合
                    Call Me.SetErrorMessage(String.Format(Messages.MSG024E, "営業所コード", "営業所コード"), Me.tbxEigyousyoCd2.ClientID)
                    Return False
                End If
            End If
        End If

        '   「営業担当者」
        If Not Me.tbxEigyouTantousyaCd.Text.Trim.Equals(String.Empty) Then
            '   半角英数チェック
            strErrorMessage = commonCheck.ChkHankakuEisuuji(Me.tbxEigyouTantousyaCd.Text.Trim, "営業担当者(ID)")
            If Not strErrorMessage.Equals(String.Empty) Then
                Call Me.SetErrorMessage(strErrorMessage, Me.tbxEigyouTantousyaCd.ClientID)
                Return False
            End If
        End If

        '   「系列コード」
        If Not Me.tbxKeiretuCd.Text.Trim.Equals(String.Empty) Then
            '   半角英数チェック
            strErrorMessage = commonCheck.ChkHankakuEisuuji(Me.tbxKeiretuCd.Text.Trim, "系列コード")
            If Not strErrorMessage.Equals(String.Empty) Then
                Call Me.SetErrorMessage(strErrorMessage, Me.tbxKeiretuCd.ClientID)
                Return False
            End If
        End If

        '   「年間棟数(FROM)」
        If Not Me.tbxNenkanTousuu1.Text.Trim.Equals(String.Empty) Then
            '   半角英数チェック
            strErrorMessage = commonCheck.CheckHankaku(Me.tbxNenkanTousuu1.Text.Trim, "年間棟数", "1")
            If Not strErrorMessage.Equals(String.Empty) Then
                Call Me.SetErrorMessage(strErrorMessage, Me.tbxNenkanTousuu1.ClientID)
                Return False
            End If
        End If

        '   「年間棟数(TO)」
        If Not Me.tbxNenkanTousuu2.Text.Trim.Equals(String.Empty) Then
            '   半角英数チェック
            strErrorMessage = commonCheck.CheckHankaku(Me.tbxNenkanTousuu2.Text.Trim, "年間棟数", "1")
            If Not strErrorMessage.Equals(String.Empty) Then
                Call Me.SetErrorMessage(strErrorMessage, Me.tbxNenkanTousuu2.ClientID)
                Return False
            End If

            '   範囲チェック
            If Me.tbxNenkanTousuu1.Text.Trim.Equals(String.Empty) Then
                '   加盟店(FROM)が未入力の場合
                Call Me.SetErrorMessage(String.Format(Messages.MSG074E, "年間棟数(From)"), Me.tbxNenkanTousuu1.ClientID)
                Return False
            Else
                If Convert.ToInt32(Me.tbxNenkanTousuu1.Text.Trim) > Convert.ToInt32(Me.tbxNenkanTousuu2.Text.Trim) Then
                    '   年間棟数(FROM) > 年間棟数(TO)の場合
                    Call Me.SetErrorMessage(String.Format(Messages.MSG024E, "年間棟数", "年間棟数"), Me.tbxNenkanTousuu2.ClientID)
                    Return False
                End If
            End If
        End If

        '   「計画管理_年間棟数(FROM)」
        If Not Me.tbxKeikakuyouNenkanTousuu1.Text.Trim.Equals(String.Empty) Then
            '   半角英数チェック
            strErrorMessage = commonCheck.CheckHankaku(Me.tbxKeikakuyouNenkanTousuu1.Text.Trim, "計画管理_年間棟数", "1")
            If Not strErrorMessage.Equals(String.Empty) Then
                Call Me.SetErrorMessage(strErrorMessage, Me.tbxKeikakuyouNenkanTousuu1.ClientID)
                Return False
            End If
        End If

        '   「計画管理_年間棟数(TO)」
        If Not Me.tbxKeikakuyouNenkanTousuu2.Text.Trim.Equals(String.Empty) Then
            '   半角英数チェック
            strErrorMessage = commonCheck.CheckHankaku(Me.tbxKeikakuyouNenkanTousuu2.Text.Trim, "計画管理_年間棟数", "1")
            If Not strErrorMessage.Equals(String.Empty) Then
                Call Me.SetErrorMessage(strErrorMessage, Me.tbxKeikakuyouNenkanTousuu2.ClientID)
                Return False
            End If

            '   範囲チェック
            If Me.tbxKeikakuyouNenkanTousuu1.Text.Trim.Equals(String.Empty) Then
                '   加盟店(FROM)が未入力の場合
                Call Me.SetErrorMessage(String.Format(Messages.MSG074E, "計画管理_年間棟数(From)"), Me.tbxKeikakuyouNenkanTousuu1.ClientID)
                Return False
            Else
                If Convert.ToInt32(Me.tbxKeikakuyouNenkanTousuu1.Text.Trim) > Convert.ToInt32(Me.tbxKeikakuyouNenkanTousuu2.Text.Trim) Then
                    '   年間棟数(FROM) > 年間棟数(TO)の場合
                    Call Me.SetErrorMessage(String.Format(Messages.MSG024E, "計画管理_年間棟数", "計画管理_年間棟数"), Me.tbxKeikakuyouNenkanTousuu2.ClientID)
                    Return False
                End If
            End If
        End If

        '   「加盟店属性6」
        If Not Me.tbxKameitenZokusei6.Text.Trim.Equals(String.Empty) Then
            '   禁止文字チェック
            If Not commonCheck.kinsiStrCheck(Me.tbxKameitenZokusei6.Text.Trim) Then
                Call Me.SetErrorMessage(String.Format(Messages.MSG033E, "加盟店属性6"), Me.tbxKameitenZokusei6.ClientID)
                Return False
            End If

            '   バイト数チェック
            strErrorMessage = commonCheck.CheckByte(Me.tbxKameitenZokusei6.Text.Trim, 40, "加盟店属性6", kbn.ZENKAKU)
            If Not strErrorMessage.Equals(String.Empty) Then
                Call Me.SetErrorMessage(String.Format(Messages.MSG073E, "加盟店属性6"), Me.tbxKameitenZokusei6.ClientID)
                Return False
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' パラメータを取得する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Private Function GetParameter() As Dictionary(Of String, String)

        Dim dicPrm As New Dictionary(Of String, String)

        '検索上限
        dicPrm.Add("KensakuJyouken", Me.ddlKensakuJyouken.SelectedValue.Trim)
        '計画年度
        dicPrm.Add("KeikakuNendo", Me.ddlYear.SelectedValue.Trim)

        '区分
        If Me.chkKubunAll.Checked Then
            dicPrm.Add("Kbn", String.Empty)
        Else
            Dim lstKbn As New List(Of String)

            If Not Me.ddlKbn1.SelectedValue.Equals(String.Empty) Then
                lstKbn.Add(Me.ddlKbn1.SelectedValue)
            End If

            If Not Me.ddlKbn2.SelectedValue.Equals(String.Empty) Then
                lstKbn.Add(Me.ddlKbn2.SelectedValue)
            End If

            If Not Me.ddlKbn3.SelectedValue.Equals(String.Empty) Then
                lstKbn.Add(Me.ddlKbn3.SelectedValue)
            End If

            dicPrm.Add("Kbn", String.Join(",", lstKbn.ToArray))
        End If
        '区分1
        dicPrm.Add("Kbn1", Me.ddlKbn1.SelectedValue.Trim)
        '区分2
        dicPrm.Add("Kbn2", Me.ddlKbn2.SelectedValue.Trim)
        '区分3
        dicPrm.Add("Kbn3", Me.ddlKbn3.SelectedValue.Trim)
        '取消
        dicPrm.Add("Torikesi", IIf(Me.chkTaikai.Checked, String.Empty, "0"))
        '加盟店名
        dicPrm.Add("KameitenMei", Me.tbxKameitenMei.Text.Trim)
        '加盟店コード1
        dicPrm.Add("KameitenCd1", Me.tbxKameitenCd1.Text.Trim)
        '加盟店コード2
        dicPrm.Add("KameitenCd2", Me.tbxKameitenCd2.Text.Trim)
        '営業所コード1
        dicPrm.Add("EigyousyaCd1", Me.tbxEigyousyoCd1.Text.Trim)
        '営業所コード2
        dicPrm.Add("EigyousyaCd2", Me.tbxEigyousyoCd2.Text.Trim)
        '系列コード
        dicPrm.Add("KeiretuCd", Me.tbxKeiretuCd.Text.Trim)
        '管轄支店
        dicPrm.Add("Siten", Me.ddlKankatuSiten.SelectedValue.Trim)
        '都道府県コード
        dicPrm.Add("TodoufukenCd", Me.ddlTodoufuken.SelectedValue.Trim)
        '営業担当者
        dicPrm.Add("EigyouTantousya", Me.tbxEigyouTantousyaCd.Text.Trim)
        '営業区分
        dicPrm.Add("EigyouKbn", Me.ddlEigyouKbn.SelectedValue.Trim)
        '営業担当所属
        dicPrm.Add("EigyouTantouSyozaku", Me.ddlEigyouTantouSyozaku.SelectedValue.Trim)
        '業態
        dicPrm.Add("Gyoutai", Me.ddlGyoutai.SelectedValue.Trim)
        '年間棟数1
        dicPrm.Add("NenkanTousuu1", Me.tbxNenkanTousuu1.Text.Trim)
        '年間棟数2
        dicPrm.Add("NenkanTousuu2", Me.tbxNenkanTousuu2.Text.Trim)
        '計画用_年間棟数1
        dicPrm.Add("KeikakuyouNenkanTousuu1", Me.tbxKeikakuyouNenkanTousuu1.Text.Trim)
        '計画用_年間棟数2
        dicPrm.Add("KeikakuyouNenkanTousuu2", Me.tbxKeikakuyouNenkanTousuu2.Text.Trim)
        '加盟店属性1
        dicPrm.Add("KameitenZokusei1", Me.ddlKameitenZokusei1.SelectedValue.Trim)
        '加盟店属性2
        dicPrm.Add("KameitenZokusei2", Me.ddlKameitenZokusei2.SelectedValue.Trim)
        '加盟店属性3
        dicPrm.Add("KameitenZokusei3", Me.ddlKameitenZokusei3.SelectedValue.Trim)
        '加盟店属性4
        dicPrm.Add("KameitenZokusei4", Me.ddlKameitenZokusei4.SelectedValue.Trim)
        '加盟店属性5
        dicPrm.Add("KameitenZokusei5", Me.ddlKameitenZokusei5.SelectedValue.Trim)
        '加盟店属性6
        dicPrm.Add("KameitenZokusei6", Me.tbxKameitenZokusei6.Text.Trim)
        '計画値有無
        dicPrm.Add("Keikaku0Flg", Me.ddlKeisakutiUmu.SelectedValue.Trim)

        Return dicPrm

    End Function


    ''' <summary>
    ''' エラーメッセージを設定する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Private Sub SetErrorMessage(ByVal strErrorMessage As String, ByVal strErrorItemId As String)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, strErrorMessage, strErrorItemId)

        Me.hidErrorMessage.Value = strErrorMessage '.Replace("\r\n", "$$$")
        Me.hidErrorItemId.Value = strErrorItemId

        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")
            .AppendLine("   strErrorMesage='" & strErrorMessage & "';")
            .AppendLine("   strErrorItemId='" & strErrorItemId & "';")
            .AppendLine("</script>")
        End With

        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        Me.ClientScript.RegisterStartupScript(Me.GetType, "SetErrorMessage", csScript.ToString)

    End Sub


    Private Sub SetButtonVisible(ByVal blnVisibleFlg As Boolean)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, blnVisibleFlg)

        Me.btnTorikesiUp.Visible = blnVisibleFlg
        Me.btnTorikesiDown.Visible = blnVisibleFlg

        Me.btnKbnUp.Visible = blnVisibleFlg
        Me.btnKbnDown.Visible = blnVisibleFlg

        Me.btnKameitenCdUp.Visible = blnVisibleFlg
        Me.btnKameitenCdDown.Visible = blnVisibleFlg

        Me.btnKameitenMeiUp.Visible = blnVisibleFlg
        Me.btnKameitenMeiDown.Visible = blnVisibleFlg

        Me.btnEigyouKbnUp.Visible = blnVisibleFlg
        Me.btnEigyouKbnDown.Visible = blnVisibleFlg

        Me.btnEigyouTantousyaUp.Visible = blnVisibleFlg
        Me.btnEigyouTantousyaDown.Visible = blnVisibleFlg

        Me.btnKankatuSitenUp.Visible = blnVisibleFlg
        Me.btnKankatuSitenDown.Visible = blnVisibleFlg

        Me.btnTodoufukenUp.Visible = blnVisibleFlg
        Me.btnTodoufukenDown.Visible = blnVisibleFlg

        Me.btnEigyousyoCdUp.Visible = blnVisibleFlg
        Me.btnEigyousyoCdDown.Visible = blnVisibleFlg

        Me.btnKeiretuCdUp.Visible = blnVisibleFlg
        Me.btnKeiretuCdDown.Visible = blnVisibleFlg

        Me.btnEigyouTantouSyozakuUp.Visible = blnVisibleFlg
        Me.btnEigyouTantouSyozakuDown.Visible = blnVisibleFlg

        Me.btnGyoutaiUp.Visible = blnVisibleFlg
        Me.btnGyoutaiDown.Visible = blnVisibleFlg

        Me.btnNenkanTousuuUp.Visible = blnVisibleFlg
        Me.btnNenkanTousuuDown.Visible = blnVisibleFlg

        Me.btnKeikakuyouNenkanTousuuUp.Visible = blnVisibleFlg
        Me.btnKeikakuyouNenkanTousuuDown.Visible = blnVisibleFlg

        Me.btnKameitenZokusei1Up.Visible = blnVisibleFlg
        Me.btnKameitenZokusei1Down.Visible = blnVisibleFlg

        Me.btnKameitenZokusei2Up.Visible = blnVisibleFlg
        Me.btnKameitenZokusei2Down.Visible = blnVisibleFlg

        Me.btnKameitenZokusei3Up.Visible = blnVisibleFlg
        Me.btnKameitenZokusei3Down.Visible = blnVisibleFlg

        Me.btnKameitenZokusei4Up.Visible = blnVisibleFlg
        Me.btnKameitenZokusei4Down.Visible = blnVisibleFlg

        Me.btnKameitenZokusei5Up.Visible = blnVisibleFlg
        Me.btnKameitenZokusei5Down.Visible = blnVisibleFlg

        Me.btnKameitenZokusei6Up.Visible = blnVisibleFlg
        Me.btnKameitenZokusei6Down.Visible = blnVisibleFlg

    End Sub

    Private Sub SetButtonColor()
        Me.btnTorikesiUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnTorikesiDown.ForeColor = Drawing.Color.SkyBlue

        Me.btnKbnUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnKbnDown.ForeColor = Drawing.Color.SkyBlue

        Me.btnKameitenCdUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnKameitenCdDown.ForeColor = Drawing.Color.SkyBlue

        Me.btnKameitenMeiUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnKameitenMeiDown.ForeColor = Drawing.Color.SkyBlue

        Me.btnEigyouKbnUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnEigyouKbnDown.ForeColor = Drawing.Color.SkyBlue

        Me.btnEigyouTantousyaUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnEigyouTantousyaDown.ForeColor = Drawing.Color.SkyBlue

        Me.btnKankatuSitenUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnKankatuSitenDown.ForeColor = Drawing.Color.SkyBlue

        Me.btnTodoufukenUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnTodoufukenDown.ForeColor = Drawing.Color.SkyBlue

        Me.btnEigyousyoCdUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnEigyousyoCdDown.ForeColor = Drawing.Color.SkyBlue

        Me.btnKeiretuCdUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnKeiretuCdDown.ForeColor = Drawing.Color.SkyBlue

        Me.btnEigyouTantouSyozakuUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnEigyouTantouSyozakuDown.ForeColor = Drawing.Color.SkyBlue

        Me.btnGyoutaiUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnGyoutaiDown.ForeColor = Drawing.Color.SkyBlue

        Me.btnNenkanTousuuUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnNenkanTousuuDown.ForeColor = Drawing.Color.SkyBlue

        Me.btnKeikakuyouNenkanTousuuUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnKeikakuyouNenkanTousuuDown.ForeColor = Drawing.Color.SkyBlue

        Me.btnKameitenZokusei1Up.ForeColor = Drawing.Color.SkyBlue
        Me.btnKameitenZokusei1Down.ForeColor = Drawing.Color.SkyBlue

        Me.btnKameitenZokusei2Up.ForeColor = Drawing.Color.SkyBlue
        Me.btnKameitenZokusei2Down.ForeColor = Drawing.Color.SkyBlue

        Me.btnKameitenZokusei3Up.ForeColor = Drawing.Color.SkyBlue
        Me.btnKameitenZokusei3Down.ForeColor = Drawing.Color.SkyBlue

        Me.btnKameitenZokusei4Up.ForeColor = Drawing.Color.SkyBlue
        Me.btnKameitenZokusei4Down.ForeColor = Drawing.Color.SkyBlue

        Me.btnKameitenZokusei5Up.ForeColor = Drawing.Color.SkyBlue
        Me.btnKameitenZokusei5Down.ForeColor = Drawing.Color.SkyBlue

        Me.btnKameitenZokusei6Up.ForeColor = Drawing.Color.SkyBlue
        Me.btnKameitenZokusei6Down.ForeColor = Drawing.Color.SkyBlue
    End Sub

    Protected Sub SetSort(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTorikesiUp.Click, _
                                                                                       btnTorikesiDown.Click, _
                                                                                       btnKbnUp.Click, _
                                                                                       btnKbnDown.Click, _
                                                                                       btnKameitenCdUp.Click, _
                                                                                       btnKameitenCdDown.Click, _
                                                                                       btnKameitenMeiUp.Click, _
                                                                                       btnKameitenMeiDown.Click, _
                                                                                       btnEigyouKbnUp.Click, _
                                                                                       btnEigyouKbnDown.Click, _
                                                                                       btnEigyouTantousyaUp.Click, _
                                                                                       btnEigyouTantousyaDown.Click, _
                                                                                       btnKankatuSitenUp.Click, _
                                                                                       btnKankatuSitenDown.Click, _
                                                                                       btnTodoufukenUp.Click, _
                                                                                       btnTodoufukenDown.Click, _
                                                                                       btnEigyousyoCdUp.Click, _
                                                                                       btnEigyousyoCdDown.Click, _
                                                                                       btnKeiretuCdUp.Click, _
                                                                                       btnKeiretuCdDown.Click, _
                                                                                       btnEigyouTantouSyozakuUp.Click, _
                                                                                       btnEigyouTantouSyozakuDown.Click, _
                                                                                       btnGyoutaiUp.Click, _
                                                                                       btnGyoutaiDown.Click, _
                                                                                       btnNenkanTousuuUp.Click, _
                                                                                       btnNenkanTousuuDown.Click, _
                                                                                       btnKeikakuyouNenkanTousuuUp.Click, _
                                                                                       btnKeikakuyouNenkanTousuuDown.Click, _
                                                                                       btnKameitenZokusei1Up.Click, _
                                                                                       btnKameitenZokusei1Down.Click, _
                                                                                       btnKameitenZokusei2Up.Click, _
                                                                                       btnKameitenZokusei2Down.Click, _
                                                                                       btnKameitenZokusei3Up.Click, _
                                                                                       btnKameitenZokusei3Down.Click, _
                                                                                       btnKameitenZokusei4Up.Click, _
                                                                                       btnKameitenZokusei4Down.Click, _
                                                                                       btnKameitenZokusei5Up.Click, _
                                                                                       btnKameitenZokusei5Down.Click, _
                                                                                       btnKameitenZokusei6Up.Click, _
                                                                                       btnKameitenZokusei6Down.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)


        Dim strItem As String = String.Empty
        Dim strSort As String = String.Empty

        strItem = CType(sender, LinkButton).ID.Replace("btn", String.Empty).Replace("Up", String.Empty).Replace("Down", String.Empty)

        If CType(sender, LinkButton).ID.IndexOf("Up") >= 0 Then
            strSort = "ASC"
        Else
            strSort = "DESC"
        End If

        Call Me.SetButtonColor()
        CType(sender, LinkButton).ForeColor = Drawing.Color.IndianRed

        Dim dvKameitenInfo As Data.DataView = CType(ViewState("dtKameitenInfo"), Data.DataTable).DefaultView
        dvKameitenInfo.Sort = strItem & " " & strSort

        ViewState("Sort") = strItem & " " & strSort

        Me.grdItiranLeft.DataSource = dvKameitenInfo
        Me.grdItiranLeft.DataBind()
        Me.grdItiranRight.DataSource = dvKameitenInfo
        Me.grdItiranRight.DataBind()

        Me.hidSelectRowIndex.Value = String.Empty
        Me.hidVScroll.Value = String.Empty


    End Sub

    ''' <summary>
    ''' 前次の画面を復元する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Private Sub SetBefore()


        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim dicPrm As Dictionary(Of String, String)
        dicPrm = CType(ViewState("Parameter"), Dictionary(Of String, String))

        If dicPrm Is Nothing Then
            Return
        End If


        '検索上限
        Call Me.SetDropDownListValue(Me.ddlKensakuJyouken, dicPrm("KensakuJyouken"))
        '計画年度
        Call Me.SetDropDownListValue(Me.ddlYear, dicPrm("KeikakuNendo"))
        '全区分
        Me.chkKubunAll.Checked = dicPrm("Kbn").Equals(String.Empty)
        '区分1
        Call Me.SetDropDownListValue(Me.ddlKbn1, dicPrm("Kbn1"))
        '区分2
        Call Me.SetDropDownListValue(Me.ddlKbn2, dicPrm("Kbn2"))
        '区分3
        Call Me.SetDropDownListValue(Me.ddlKbn3, dicPrm("Kbn3"))
        '取消
        Me.chkTaikai.Checked = dicPrm("Torikesi").Equals(String.Empty)
        '加盟店名
        Me.tbxKameitenMei.Text = dicPrm("KameitenMei")
        '加盟店コード1
        Me.tbxKameitenCd1.Text = dicPrm("KameitenCd1")
        '加盟店コード2
        Me.tbxKameitenCd2.Text = dicPrm("KameitenCd2")
        '営業所コード1
        Me.tbxEigyousyoCd1.Text = dicPrm("EigyousyaCd1")
        '営業所コード2
        Me.tbxEigyousyoCd2.Text = dicPrm("EigyousyaCd2")
        '系列コード
        Me.tbxKeiretuCd.Text = dicPrm("KeiretuCd")
        '管轄支店 
        Call Me.SetDropDownListValue(Me.ddlKankatuSiten, dicPrm("Siten"))
        '都道府県コード
        Call Me.SetDropDownListValue(Me.ddlTodoufuken, dicPrm("TodoufukenCd"))
        '営業担当者
        Me.tbxEigyouTantousyaCd.Text = dicPrm("EigyouTantousya")
        '営業区分
        Call Me.SetDropDownListValue(Me.ddlEigyouKbn, dicPrm("EigyouKbn"))
        '営業担当所属
        Call Me.SetDropDownListValue(Me.ddlEigyouTantouSyozaku, dicPrm("EigyouTantouSyozaku"))
        '業態
        Call Me.SetDropDownListValue(Me.ddlGyoutai, dicPrm("Gyoutai"))
        '年間棟数1
        Me.tbxNenkanTousuu1.Text = dicPrm("NenkanTousuu1")
        '年間棟数2
        Me.tbxNenkanTousuu2.Text = dicPrm("NenkanTousuu2")
        '計画用_年間棟数1
        Me.tbxKeikakuyouNenkanTousuu1.Text = dicPrm("KeikakuyouNenkanTousuu1")
        '計画用_年間棟数2
        Me.tbxKeikakuyouNenkanTousuu2.Text = dicPrm("KeikakuyouNenkanTousuu2")
        '加盟店属性1
        Call Me.SetDropDownListValue(Me.ddlKameitenZokusei1, dicPrm("KameitenZokusei1"))
        '加盟店属性2
        Call Me.SetDropDownListValue(Me.ddlKameitenZokusei2, dicPrm("KameitenZokusei2"))
        '加盟店属性3
        Call Me.SetDropDownListValue(Me.ddlKameitenZokusei3, dicPrm("KameitenZokusei3"))
        '加盟店属性4
        Call Me.SetDropDownListValue(Me.ddlKameitenZokusei4, dicPrm("KameitenZokusei4"))
        '加盟店属性5
        Call Me.SetDropDownListValue(Me.ddlKameitenZokusei5, dicPrm("KameitenZokusei5"))
        '加盟店属性6
        Me.tbxKameitenZokusei6.Text = dicPrm("KameitenZokusei6")
        '計画値有無
        Call Me.SetDropDownListValue(Me.ddlKeisakutiUmu, dicPrm("Keikaku0Flg"))

        '検索実行
        Call Me.btnKensakujiltukou_Click()

        If Not dicPrm("Sort").Equals(String.Empty) Then

            Dim strSort() As String = dicPrm("Sort").Split(" ")
            Dim strSortBtnId As String = "btn" & strSort(0) & IIf(strSort(1).Equals("ASC"), "Up", "Down")
            Dim lnk As LinkButton
            If Not Me.divHeadLeft.FindControl(strSortBtnId) Is Nothing Then
                lnk = Me.divHeadLeft.FindControl(strSortBtnId)
            Else
                lnk = Me.divHeadRight.FindControl(strSortBtnId)
            End If

            Call Me.SetSort(lnk, New System.EventArgs())
        End If

        'If Not dicPrm("HScroll").Equals(String.Empty) Then
        '    Me.hidHScroll.Value = dicPrm("HScroll")
        'End If

        'If Not dicPrm("VScroll").Equals(String.Empty) Then
        '    Me.hidVScroll.Value = dicPrm("VScroll")
        'End If

        'If Not dicPrm("RowIndex").Equals(String.Empty) Then
        '    Me.hidSelectRowIndex.Value = dicPrm("RowIndex")
        'End If

    End Sub

    ''' <summary>
    ''' DDLを設定する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Private Sub SetDropDownListValue(ByVal ddl As DropDownList, ByVal strValue As String)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, ddl, strValue)

        Try
            ddl.SelectedValue = strValue
        Catch ex As Exception
            ddl.SelectedIndex = 0
        End Try

    End Sub

    ''' <summary>
    ''' 営業担当者を設定する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Private Sub SetEigyouTantousya()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        If Not Me.tbxEigyouTantousyaCd.Text.Trim.Equals(String.Empty) Then


            Dim dt As New Data.DataTable
            Dim sitenSearchBC As New EigyouManSearchBC
            dt = sitenSearchBC.GetUserInfo("0", Me.tbxEigyouTantousyaCd.Text.Trim, "", False, False)

            If dt.Rows.Count > 0 Then
                Me.tbxEigyouTantousyaMei.Text = dt.Rows(0).Item(1).ToString
            Else
                Me.tbxEigyouTantousyaMei.Text = String.Empty
            End If
        Else
            Me.tbxEigyouTantousyaCd.Text = String.Empty
            Me.tbxEigyouTantousyaMei.Text = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' 系列を設定する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Private Sub SetKeiretu()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        If Not Me.tbxKeiretuCd.Text.Trim.Equals(String.Empty) Then


            Dim dt As New Data.DataTable
            Dim keiretuSearchBC As New KeiretuSearchBC
            dt = keiretuSearchBC.GetKiretuJyouhou("0", Me.tbxKeiretuCd.Text.Trim, "", False, False)

            If dt.Rows.Count > 0 Then
                Me.tbxKeiretuMei.Text = dt.Rows(0).Item(1).ToString
            Else
                Me.tbxKeiretuMei.Text = String.Empty
            End If
        Else
            Me.tbxKeiretuCd.Text = String.Empty
            Me.tbxKeiretuMei.Text = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' JavaScriptを作成する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Private Sub MakeJavaScript()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")
            '区分を設定する
            .AppendLine("   function fncSetKubunVal(){")
            .AppendLine("       var objKbn1 = document.getElementById('" & Me.ddlKbn1.ClientID & "');")
            .AppendLine("       var objKbn2 = document.getElementById('" & Me.ddlKbn2.ClientID & "');")
            .AppendLine("       var objKbn3 = document.getElementById('" & Me.ddlKbn3.ClientID & "');")
            .AppendLine("       var objKbnAll = document.getElementById('" & Me.chkKubunAll.ClientID & "');")
            .AppendLine("       if(objKbnAll.checked == true){")
            .AppendLine("           objKbn1.selectedIndex = 0;")
            .AppendLine("           objKbn2.selectedIndex = 0;")
            .AppendLine("           objKbn3.selectedIndex = 0;")
            .AppendLine("           objKbn1.disabled = true;")
            .AppendLine("           objKbn2.disabled = true;")
            .AppendLine("           objKbn3.disabled = true;")
            .AppendLine("       }else{")
            .AppendLine("           objKbn1.disabled = false;")
            .AppendLine("           objKbn2.disabled = false;")
            .AppendLine("           objKbn3.disabled = false;")
            .AppendLine("       }")
            .AppendLine("   }")
            '加盟店検索popup
            .AppendLine("   function fncShowKameitenPopup(kbn)")
            .AppendLine("   {")
            .AppendLine("       var objYear = $ID('" & Me.ddlYear.ClientID & "');")
            .AppendLine("       if(objYear.value == '')")
            .AppendLine("       {")
            .AppendLine("           alert('" & String.Format(Messages.MSG001E, "対象年度") & "');")
            .AppendLine("           objYear.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       var strCdValue = '';")
            .AppendLine("       var strCdId = '';")
            .AppendLine("       if(kbn == '1')")
            .AppendLine("       {")
            .AppendLine("           strCdValue = $ID('" & Me.tbxKameitenCd1.ClientID & "').value;")
            .AppendLine("           strCdId = '" & Me.tbxKameitenCd1.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       else")
            .AppendLine("       {")
            .AppendLine("           strCdValue = $ID('" & Me.tbxKameitenCd2.ClientID & "').value;")
            .AppendLine("           strCdId = '" & Me.tbxKameitenCd2.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       var strTorikesi = !$ID('" & Me.chkTaikai.ClientID & "').checked;")
            .AppendLine("       window.open('./PopupSearch/KeikakuKanriKameitenSearch.aspx?formName=" & Me.Form.ClientID & "&strYear='+objYear.value+'&strTorikesi='+strTorikesi+'&strKameitenCdValue='+ escape(strCdValue)+'&strKameitenCdId='+ strCdId +'&strKameitenMeiId=', 'KeikakuKanriKameitenSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '営業所検索popup
            .AppendLine("   function fncShowEigyousyoPopup(kbn)")
            .AppendLine("   {")
            .AppendLine("       var strCdValue = '';")
            .AppendLine("       var strCdId = '';")
            .AppendLine("       if(kbn == '1')")
            .AppendLine("       {")
            .AppendLine("           strCdValue = $ID('" & Me.tbxEigyousyoCd1.ClientID & "').value;")
            .AppendLine("           strCdId = '" & Me.tbxEigyousyoCd1.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       else")
            .AppendLine("       {")
            .AppendLine("           strCdValue = $ID('" & Me.tbxEigyousyoCd2.ClientID & "').value;")
            .AppendLine("           strCdId = '" & Me.tbxEigyousyoCd2.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       var strTorikesi = !$ID('" & Me.chkTaikai.ClientID & "').checked;")
            .AppendLine("       window.open('./PopupSearch/EigyousyoSearchSyoukaiJisiyou.aspx?formName=" & Me.Form.ClientID & "&strTorikesi='+ strTorikesi +'&strEigyousyoCdValue='+ escape(strCdValue)+'&strEigyousyoCdId='+strCdId+'&strEigyousyoMeiId=', 'EigyousyoSearchSyoukaiJisiyou', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '営業担当者検索popup
            .AppendLine("   function fncShowEigyouTantousyaPopup()")
            .AppendLine("   {")
            .AppendLine("       var strCdValue = '';")
            .AppendLine("       var strCdId = '';")
            .AppendLine("       var strMeiId = '';")
            .AppendLine("       strCdValue = $ID('" & Me.tbxEigyouTantousyaCd.ClientID & "').value;")
            .AppendLine("       strCdId = '" & Me.tbxEigyouTantousyaCd.ClientID & "';")
            .AppendLine("       strMeiId = '" & Me.tbxEigyouTantousyaMei.ClientID & "';")
            .AppendLine("       window.open('./PopupSearch/EigyouManSearch.aspx?formName=" & Me.Form.ClientID & "&strEigyouManCd='+escape(strCdValue)+'&strEigyouManMei=&field='+strMeiId+'&fieldCd='+strCdId, 'EigyouManSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '系列検索popup
            .AppendLine("   function fncShowKeiretuPopup()")
            .AppendLine("   {")
            .AppendLine("       var strCdValue = '';")
            .AppendLine("       var strCdId = '';")
            .AppendLine("       var strMeiId = '';")
            .AppendLine("       strCdValue = $ID('" & Me.tbxKeiretuCd.ClientID & "').value;")
            .AppendLine("       strCdId = '" & Me.tbxKeiretuCd.ClientID & "';")
            .AppendLine("       strMeiId = '" & Me.tbxKeiretuMei.ClientID & "';")
            .AppendLine("       var strTorikesi = !$ID('" & Me.chkTaikai.ClientID & "').checked;")
            .AppendLine("       window.open('./PopupSearch/KeiretuSearch.aspx?formName=" & Me.Form.ClientID & "&strTorikesi='+strTorikesi+'&strKeiretuCdValue='+strCdValue+'&strKeiretuCd=&field=" & Me.tbxKeiretuMei.ClientID & "'+'&fieldMei=" & Me.tbxKeiretuCd.ClientID & "', 'KeiretuSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '検索クリア
            .AppendLine("   function fncClear()")
            .AppendLine("   {")
            .AppendLine("       $ID('" & Me.ddlYear.ClientID & "').value = $ID('" & Me.hidYear.ClientID & "').value;")
            .AppendLine("       $ID('" & Me.chkKubunAll.ClientID & "').checked = true;")
            .AppendLine("       fncSetKubunVal();")
            .AppendLine("       $ID('" & Me.chkTaikai.ClientID & "').checked = false;")
            .AppendLine("       $ID('" & Me.tbxKameitenMei.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.tbxKameitenCd1.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.tbxKameitenCd2.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.ddlKankatuSiten.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.ddlTodoufuken.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.tbxEigyousyoCd1.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.tbxEigyousyoCd2.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.tbxEigyouTantousyaCd.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.tbxEigyouTantousyaMei.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.tbxKeiretuCd.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.tbxKeiretuMei.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.ddlEigyouKbn.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.ddlEigyouTantouSyozaku.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.ddlGyoutai.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.tbxNenkanTousuu1.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.tbxNenkanTousuu2.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.ddlKameitenZokusei1.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.ddlKameitenZokusei2.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.ddlKameitenZokusei3.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.tbxKeikakuyouNenkanTousuu1.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.tbxKeikakuyouNenkanTousuu2.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.ddlKameitenZokusei4.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.ddlKameitenZokusei5.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.tbxKameitenZokusei6.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.ddlKeisakutiUmu.ClientID & "').value = '';")
            .AppendLine("       $ID('" & Me.ddlKensakuJyouken.ClientID & "').value = '100';")
            .AppendLine("   }")
            'エラーメッセージを表示する
            .AppendLine("   var strErrorMesage='';")
            .AppendLine("   var strErrorItemId='';")
            .AppendLine("   function fncShowErrorMessage()")
            .AppendLine("   {")
            .AppendLine("       if(strErrorMesage != '')")
            .AppendLine("       {")
            .AppendLine("           alert(strErrorMesage);")
            .AppendLine("           if(strErrorItemId != '')")
            .AppendLine("           {")
            .AppendLine("               $ID(strErrorItemId).focus();")
            .AppendLine("               try{ $ID(strErrorItemId).select();}catch(e){}")
            .AppendLine("           }")
            .AppendLine("       }")
            .AppendLine("       strErrorMesage = '';")
            .AppendLine("       strErrorItemId = '';")
            .AppendLine("   }")

            '「営業担当者」と「系列」を変更する時
            .AppendLine("   function fncSetMei(strCdId,strMeiId)")
            .AppendLine("   {")
            .AppendLine("       if($ID(strCdId).value.Trim() == '')")
            .AppendLine("       {")
            .AppendLine("           $ID(strMeiId).value = '';")
            .AppendLine("       }")
            .AppendLine("   }")

            'ボタンを活性化する
            .AppendLine("   function funDisableButton()")
            .AppendLine("   {")
            .AppendLine("       $ID('" & Me.btnKeisakuKameitenJyouhouSyoukai.ClientID & "').disabled = false;")
            .AppendLine("   }")

            .AppendLine("   function fncSaveHScroll(){")
            .AppendLine("       var divHscroll=" & Me.divScrollH.ClientID & ";")
            .AppendLine("       document.getElementById('" & Me.hidHScroll.ClientID & "').value = divHscroll.scrollLeft;")
            .AppendLine("   }")
            .AppendLine("   function fncSaveVScroll(){")
            .AppendLine("       var divVscroll=" & Me.divScrollV.ClientID & ";")
            .AppendLine("       document.getElementById('" & Me.hidVScroll.ClientID & "').value =divVscroll.scrollTop;")
            .AppendLine("   }")
            .AppendLine("   function fncSetScroll(){")
            .AppendLine("       var divheadright=$ID('" & Me.divHeadRight.ClientID & "');")

            .AppendLine("       var divBodyLeft=$ID('" & Me.divBodyLeft.ClientID & "');")
            .AppendLine("       var divbodyright=$ID('" & Me.divBodyRight.ClientID & "');")

            .AppendLine("       var divScrollV=$ID('" & Me.divScrollV.ClientID & "');")
            .AppendLine("       var divHscroll=$ID('" & Me.divScrollH.ClientID & "');")

            .AppendLine("       var hidHScroll=$ID('" & Me.hidHScroll.ClientID & "');")
            .AppendLine("       var hidVScroll=$ID('" & Me.hidVScroll.ClientID & "');")

            .AppendLine("       var strScrollLeft=hidHScroll.value;")
            .AppendLine("       var strScrollTop=hidVScroll.value;")
            .AppendLine("       if(strScrollLeft != '')")
            .AppendLine("       {")
            .AppendLine("           divheadright.scrollLeft = strScrollLeft;")
            .AppendLine("           divbodyright.scrollLeft = strScrollLeft;")
            .AppendLine("           divHscroll.scrollLeft = strScrollLeft;")
            .AppendLine("       }")
            .AppendLine("       if(strScrollTop != '')")
            .AppendLine("       {")
            .AppendLine("           divBodyLeft.scrollTop = strScrollTop;")
            .AppendLine("           divbodyright.scrollTop = strScrollTop;")
            .AppendLine("           divScrollV.scrollTop = strScrollTop;")
            .AppendLine("       }")
            .AppendLine("   }")
            '「検索実行」
            .AppendLine("   function fncKensakuCheck()")
            .AppendLine("   {")
            .AppendLine("       if(document.getElementById('" & Me.ddlKensakuJyouken.ClientID & "').value=='max'){")
            .AppendLine("           if (!confirm('" & Messages.MSG075E & "')){")
            .AppendLine("               return false;")
            .AppendLine("           }")
            .AppendLine("       }")
            .AppendLine("       return true;")
            .AppendLine("   }")

            '選択した行
            .AppendLine("   function fncSetSelectedRow()")
            .AppendLine("   {")
            .AppendLine("       var intRowIndex = $ID('" & Me.hidSelectRowIndex.ClientID & "').value;")
            .AppendLine("       if(intRowIndex != '')")
            .AppendLine("       {")
            .AppendLine("           $ID('" & Me.grdItiranLeft.ClientID & "').childNodes[0].childNodes[intRowIndex].childNodes[0].childNodes[0].click();")
            .AppendLine("       }")
            .AppendLine("   }")

            .AppendLine("   function fncScrollV(){")
            .AppendLine("       var divbodyleft=" & Me.divBodyLeft.ClientID & ";")
            .AppendLine("       var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("       var divVscroll=" & Me.divScrollV.ClientID & ";")
            .AppendLine("       divbodyleft.scrollTop = divVscroll.scrollTop;")
            .AppendLine("       divbodyright.scrollTop = divVscroll.scrollTop;")
            .AppendLine("   }")
            .AppendLine("   function fncScrollH(){")
            .AppendLine("       var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("       var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("       var divHscroll=" & Me.divScrollH.ClientID & ";")
            .AppendLine("       divheadright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("       divbodyright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("   }")
            .AppendLine("   function wheel(event){")
            .AppendLine("       var delta = 0;")
            .AppendLine("       if(!event)")
            .AppendLine("           event = window.event;")
            .AppendLine("       if (event.wheelDelta){")
            .AppendLine("           delta = event.wheelDelta/120;")
            .AppendLine("           if (window.opera)")
            .AppendLine("               delta = -delta;")
            .AppendLine("       } else if(event.detail){")
            .AppendLine("           delta = -event.detail/3;")
            .AppendLine("       }")
            .AppendLine("       if (delta)")
            .AppendLine("           handle(delta);")
            .AppendLine("   }")
            .AppendLine("   function handle(delta){")
            .AppendLine("      var divVscroll=" & Me.divScrollV.ClientID & ";")
            .AppendLine("      if (delta < 0){")
            .AppendLine("          divVscroll.scrollTop = divVscroll.scrollTop + 15;")
            .AppendLine("      }else{")
            .AppendLine("          divVscroll.scrollTop = divVscroll.scrollTop - 15;")
            .AppendLine("      }")
            .AppendLine("   }")

            .AppendLine("   function fncSetLineColor(obj,index){")
            .AppendLine("       document.getElementById('" & Me.hidSelectRowIndex.ClientID & "').value = index;")
            .AppendLine("       var obj1 = objEBI('" + Me.grdItiranRight.ClientID + "').childNodes[0].childNodes[index] ")
            .AppendLine("       setSelectedLineColor(obj,obj1);")
            .AppendLine("   }")



            .AppendLine("</script>  ")
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

End Class
