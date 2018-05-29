Imports Lixil.JHS_EKKS.BizLogic
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Messages = Lixil.JHS_EKKS.Utilities.CommonMessage
Imports System.Collections.Generic

Partial Class CommonControl_KeikaikuKanriKameitenBikouInquiry
    Inherits System.Web.UI.UserControl

    Private keikaikuKanriKameitenBikouInquiryBC As New KeikaikuKanriKameitenBikouInquiryBC
    Private CommonCheckFuc As New CommonCheck()

    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Property KameitenCd() As String
        Get
            Return ViewState("KameitenCd").ToString
        End Get
        Set(ByVal value As String)
            ViewState("KameitenCd") = value
        End Set
    End Property



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

        Call Me.MakeJavaScript()

        If Not IsPostBack Then

            If Me.divNaiyou.Visible Then
                '画面表示
                Call Me.GamenInit()
            End If

        End If

        Me.btnKensaku.Attributes.Add("onClick", "fncShowSyubetuPopup('" & Me.tbxBikousyubetu.ClientID & "','" & Me.lblSyubetumei.ClientID & "');return false;")

        Me.lblSyubetumei.Attributes.Add("readOnly", "true")



    End Sub

    ''' <summary>
    ''' GridViewデータ取込む
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.Web.UI.WebControls.GridViewRowEventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Protected Sub grdBeikou_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBeikou.RowDataBound

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If e.Row.RowType = DataControlRowType.DataRow Then


            Dim tbxCd As System.Web.UI.WebControls.TextBox
            tbxCd = CType(e.Row.FindControl("tbxBikousyubetu"), TextBox)

            Dim tbxMei As System.Web.UI.WebControls.TextBox
            tbxMei = CType(e.Row.FindControl("tbxBikousyubetuMei"), TextBox)

            Dim btnSearch As System.Web.UI.WebControls.Button
            btnSearch = CType(e.Row.FindControl("btnKensaku1"), Button)

            btnSearch.Attributes.Add("onClick", "fncShowSyubetuPopup('" & tbxCd.ClientID & "','" & tbxMei.ClientID & "');return false;")

            tbxMei.Attributes.Add("readOnly", "true")

        End If

    End Sub

    Public Sub grdBikousyubetuText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        Dim tbxCd As TextBox
        Dim tbxMei As TextBox
        Dim tbxNaiyou As TextBox

        tbxCd = CType(sender, TextBox)
        tbxMei = CType(CType(tbxCd.Parent.Parent, GridViewRow).FindControl("tbxBikousyubetuMei"), TextBox)
        tbxNaiyou = CType(CType(tbxCd.Parent.Parent, GridViewRow).FindControl("tbxNaiyou"), TextBox)


        Dim text As String
        text = tbxCd.Text.Trim

        If text.Equals(String.Empty) Then
            tbxCd.Text = String.Empty
            tbxMei.Text = String.Empty

            Call Me.SetMessage(String.Empty, tbxCd.ClientID)
        Else

            '種別チェック
            If Not Me.CheckBikouSyubetu(tbxCd, tbxMei, True) Then
                Return
            End If

            '名称を取得する
            Dim bikousyubetuMei As String
            bikousyubetuMei = keikaikuKanriKameitenBikouInquiryBC.Getkameitensyubetu(text)

            If bikousyubetuMei.Equals(String.Empty) Then
                Call Me.SetMessage(String.Format(Messages.MSG076E, "種別"), tbxCd.ClientID)
                tbxCd.Text = String.Empty
                tbxMei.Text = String.Empty
                Return
            Else
                tbxMei.Text = bikousyubetuMei
                Call Me.SetMessage(String.Empty, tbxNaiyou.ClientID)
            End If

        End If

    End Sub


    Protected Sub tbxBikousyubetu_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxBikousyubetu.TextChanged

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        Dim text As String
        text = Me.tbxBikousyubetu.Text

        If text.Equals(String.Empty) Then
            Me.tbxBikousyubetu.Text = String.Empty
            Me.lblSyubetumei.Text = String.Empty

            Call Me.SetMessage(String.Empty, Me.tbxBikousyubetu.ClientID)
        Else
            '種別チェック
            If Not Me.CheckBikouSyubetu(Me.tbxBikousyubetu, Me.lblSyubetumei, True) Then
                Return
            End If

            '名称を取得する
            Dim bikousyubetuMei As String
            bikousyubetuMei = keikaikuKanriKameitenBikouInquiryBC.Getkameitensyubetu(text)

            If bikousyubetuMei.Equals(String.Empty) Then
                Call Me.SetMessage(String.Format(Messages.MSG076E, "種別"), Me.tbxBikousyubetu.ClientID)
                Me.tbxBikousyubetu.Text = String.Empty
                Me.lblSyubetumei.Text = String.Empty
                Return
            Else
                Me.lblSyubetumei.Text = bikousyubetuMei
                Call Me.SetMessage(String.Empty, Me.tbxNaiyou.ClientID)
            End If

        End If

    End Sub

    ''' <summary>
    ''' 「新規」ボタンを押下する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Protected Sub btnXinki_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnXinki.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '種別チェック
        If Not Me.CheckBikouSyubetu(Me.tbxBikousyubetu, Me.lblSyubetumei) Then
            Return
        End If

        '内容チェック
        If Not Me.CheckNaiyou(Me.tbxNaiyou) Then
            Return
        End If

        '他の端末で更新されたチェック
        If Not Me.CheckUpdTime(Me.tbxBikousyubetu) Then
            Return
        End If

        '新規
        If keikaikuKanriKameitenBikouInquiryBC.SetInsBikou(Me.GetPrm(Me.tbxBikousyubetu.Text.Trim, String.Empty, Me.tbxNaiyou.Text.Trim)) Then
            '成功
            Call Me.GamenInit()

            Call Me.SetMessage(String.Format(Messages.MSG077E, "備考"), Me.tbxBikousyubetu.ClientID)
        Else
            '失敗
            Call Me.SetMessage(String.Format(Messages.MSG078E, "備考"), Me.tbxBikousyubetu.ClientID)

        End If


    End Sub

    ''' <summary>
    ''' 「登録」ボタンを押下する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Sub UpdBikou(ByVal sender As Object, ByVal e As System.EventArgs)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        Dim tbxCd As TextBox
        Dim tbxMei As TextBox
        Dim tbxNaiyou As TextBox
        Dim hidNyuuryokuNo As HiddenField

        tbxCd = CType(CType(sender.Parent.Parent, GridViewRow).FindControl("tbxBikousyubetu"), TextBox)
        tbxMei = CType(CType(sender.Parent.Parent, GridViewRow).FindControl("tbxBikousyubetuMei"), TextBox)
        tbxNaiyou = CType(CType(sender.Parent.Parent, GridViewRow).FindControl("tbxNaiyou"), TextBox)
        hidNyuuryokuNo = CType(CType(sender.Parent.Parent, GridViewRow).FindControl("hidNyuuryokuNo"), HiddenField)

        '種別チェック
        If Not Me.CheckBikouSyubetu(tbxCd, tbxMei) Then
            Return
        End If

        '内容チェック
        If Not Me.CheckNaiyou(tbxNaiyou) Then
            Return
        End If

        '他の端末で更新されたチェック
        If Not Me.CheckUpdTime(tbxCd) Then
            Return
        End If

        '登録
        If keikaikuKanriKameitenBikouInquiryBC.SetUpdBikou(Me.GetPrm(tbxCd.Text.Trim, hidNyuuryokuNo.Value, tbxNaiyou.Text.Trim)) Then
            '成功
            Call Me.GamenInit()

            Call Me.SetMessage(String.Format(Messages.MSG077E, "備考"), String.Empty)
        Else
            '失敗
            Call Me.SetMessage(String.Format(Messages.MSG078E, "備考"), tbxCd.ClientID)

        End If

    End Sub

    ''' <summary>
    ''' 「削除」ボタンを押下する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Sub DelBikou(ByVal sender As Object, ByVal e As System.EventArgs)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        Dim tbxCd As TextBox
        Dim tbxMei As TextBox
        Dim tbxNaiyou As TextBox
        Dim hidNyuuryokuNo As HiddenField

        tbxCd = CType(CType(sender.Parent.Parent, GridViewRow).FindControl("tbxBikousyubetu"), TextBox)
        tbxMei = CType(CType(sender.Parent.Parent, GridViewRow).FindControl("tbxBikousyubetuMei"), TextBox)
        tbxNaiyou = CType(CType(sender.Parent.Parent, GridViewRow).FindControl("tbxNaiyou"), TextBox)
        hidNyuuryokuNo = CType(CType(sender.Parent.Parent, GridViewRow).FindControl("hidNyuuryokuNo"), HiddenField)

        '他の端末で更新されたチェック
        If Not Me.CheckUpdTime(tbxCd) Then
            Return
        End If

        '削除
        If keikaikuKanriKameitenBikouInquiryBC.SetDelBikou(Me.GetPrm(tbxCd.Text.Trim, hidNyuuryokuNo.Value, tbxNaiyou.Text.Trim)) Then
            '成功
            Call Me.GamenInit()

            Call Me.SetMessage(String.Format(Messages.MSG077E, "備考"), String.Empty)
        Else
            '失敗
            Call Me.SetMessage(String.Format(Messages.MSG078E, "備考"), tbxCd.ClientID)

        End If

    End Sub


    ''' <summary>
    ''' 画面表示
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GamenInit()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '加盟店備考更新日取得
        Dim dtMaxDate As New Data.DataTable
        dtMaxDate = keikaikuKanriKameitenBikouInquiryBC.GetKameitenBikouMaxUpdTime(ViewState("KameitenCd").ToString)

        If dtMaxDate.Rows.Count > 0 Then
            If dtMaxDate.Rows(0).Item("maxtime").ToString.Equals(String.Empty) Then
                Me.hidMaxDate.Value = String.Empty
            Else
                Me.hidMaxDate.Value = Convert.ToDateTime(dtMaxDate.Rows(0).Item("maxtime")).ToString("yyyy/MM/dd HH:mm:ss")
            End If
        Else
            Me.hidMaxDate.Value = String.Empty
        End If

        '備考明細を取得する
        Dim dtBikouInfo As New Data.DataTable
        dtBikouInfo = keikaikuKanriKameitenBikouInquiryBC.GetBikouInfo(ViewState("KameitenCd").ToString)

        Me.grdBeikou.DataSource = dtBikouInfo
        Me.grdBeikou.DataBind()

        '備考新規行を設定する
        Me.tbxBikousyubetu.Text = String.Empty
        Me.lblSyubetumei.Text = String.Empty
        Me.tbxNaiyou.Text = String.Empty

        Me.hidScroll.Value = String.Empty

    End Sub

    ''' <summary>
    ''' 種別チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckBikouSyubetu(ByVal tbxBikouSyubetu As TextBox, ByVal tbxBikouSyubetuMei As TextBox, Optional ByVal clearFlg As Boolean = False) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, tbxBikouSyubetu)

        Dim strBikouSyubetu As String = tbxBikouSyubetu.Text.Trim

        Dim msg As String
        msg = CommonCheckFuc.CheckHissuNyuuryoku(strBikouSyubetu, "種別")
        If msg <> String.Empty Then
            Call Me.SetMessage(msg, tbxBikouSyubetu.ClientID)
            If clearFlg Then
                tbxBikouSyubetu.Text = String.Empty
                tbxBikouSyubetuMei.Text = String.Empty
            End If
            Return False
        End If

        msg = CommonCheckFuc.CheckHankaku(strBikouSyubetu, "種別", "1")
        If msg <> String.Empty Then
            Call Me.SetMessage(msg, tbxBikouSyubetu.ClientID)
            If clearFlg Then
                tbxBikouSyubetu.Text = String.Empty
                tbxBikouSyubetuMei.Text = String.Empty
            End If
            Return False
        End If

        msg = CommonCheckFuc.CheckByte(strBikouSyubetu, 4, "種別")
        If msg <> String.Empty Then
            Call Me.SetMessage(msg, tbxBikouSyubetu.ClientID)
            If clearFlg Then
                tbxBikouSyubetu.Text = String.Empty
                tbxBikouSyubetuMei.Text = String.Empty
            End If
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 内容チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckNaiyou(ByVal tbxNaiyou As TextBox) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, tbxNaiyou)

        Dim strNaiyou As String = tbxNaiyou.Text.Trim

        Dim msg As String
        If Not strNaiyou.Equals(String.Empty) Then
            '   禁止文字チェック
            If Not CommonCheckFuc.kinsiStrCheck(strNaiyou) Then
                Call Me.SetMessage(String.Format(Messages.MSG033E, "備考_内容"), tbxNaiyou.ClientID)
                Return False
            End If

            '   バイト数チェック
            msg = CommonCheckFuc.CheckByte(strNaiyou, 80, "備考_内容", kbn.ZENKAKU)
            If Not msg.Equals(String.Empty) Then
                Call Me.SetMessage(String.Format(Messages.MSG073E, "備考_内容"), tbxNaiyou.ClientID)
                Return False
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' 他の端末で更新されたチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckUpdTime(ByVal tbxCd As TextBox) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, tbxNaiyou)

        '加盟店備考更新日取得
        Dim strMaxUpdTime As String
        Dim strUserId As String
        Dim dtMaxDate As New Data.DataTable
        dtMaxDate = keikaikuKanriKameitenBikouInquiryBC.GetKameitenBikouMaxUpdTime(ViewState("KameitenCd").ToString)

        If dtMaxDate.Rows.Count > 0 Then
            If dtMaxDate.Rows(0).Item("maxtime").ToString.Equals(String.Empty) Then
                strMaxUpdTime = String.Empty
                strUserId = String.Empty
            Else
                strMaxUpdTime = Convert.ToDateTime(dtMaxDate.Rows(0).Item("maxtime")).ToString("yyyy/MM/dd HH:mm:ss")
                strUserId = dtMaxDate.Rows(0).Item("theuser").ToString()
            End If
        Else
            strMaxUpdTime = String.Empty
            strUserId = String.Empty
        End If

        '他の端末で更新されたチェック
        If Me.hidMaxDate.Value <> String.Empty AndAlso strMaxUpdTime <> String.Empty Then
            If Me.hidMaxDate.Value < strMaxUpdTime Then
                Call Me.SetMessage(String.Format(Messages.MSG079E, strUserId, strMaxUpdTime), tbxCd.ClientID)
                Return False
            End If
        ElseIf Me.hidMaxDate.Value <> String.Empty AndAlso strMaxUpdTime = String.Empty Then
            Call Me.SetMessage(Messages.MSG080E, tbxCd.ClientID)
            Return False
        Else
            If Me.hidMaxDate.Value = String.Empty AndAlso strMaxUpdTime <> String.Empty Then
                Call Me.SetMessage(String.Format(Messages.MSG079E, strUserId, strMaxUpdTime), tbxCd.ClientID)
                Return False
            End If
        End If

        Return True

    End Function


    Private Function GetPrm(ByVal strBikouSyubetu As String, ByVal strNyuuryokuNo As String, ByVal strNaiyou As String) As Dictionary(Of String, String)

        Dim dicPrm As New Dictionary(Of String, String)

        Dim CommonCheck As New CommonCheck
        Dim LoginUserInfoList As New LoginUserInfoList
        Dim UserId As String = ""

        CommonCheck.CommonNinsyou(UserId, LoginUserInfoList, kegen.UserIdOnly)


        dicPrm.Add("kameiten_cd", ViewState("KameitenCd").ToString)
        dicPrm.Add("bikou_syubetu", strBikouSyubetu)
        dicPrm.Add("nyuuryoku_no", strNyuuryokuNo)
        dicPrm.Add("naiyou", strNaiyou)
        dicPrm.Add("kousinsya", LoginUserInfoList.Items(0).AccountNo)
        dicPrm.Add("add_login_user_id", UserId)

        Return dicPrm

    End Function

    ''' <summary>
    ''' エラーメッセージを設定する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Private Sub SetMessage(ByVal strErrorMessage As String, ByVal strErrorItemId As String)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, strErrorMessage, strErrorItemId)

        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")
            .AppendLine("   strMessage='" & strErrorMessage & "';")
            .AppendLine("   strItemId='" & strErrorItemId & "';")
            .AppendLine("</script>")
        End With

        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        Me.Parent.Page.ClientScript.RegisterStartupScript(Me.GetType, "SetBikouErrorMessage", csScript.ToString)

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
        Dim csName As String = "setBikouScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")

            .AppendLine("   function fncShowSyubetuPopup(strCdId,strMeiId)")
            .AppendLine("   {")
            .AppendLine("       var objCd = $ID(strCdId);")
            .AppendLine("       window.open('./PopupSearch/SyubetuSearch.aspx?formName=" & Me.Parent.Page.Form.ClientID & "&strSyubetuCd='+objCd.value+'&fieldCd='+strCdId+'&field='+strMeiId, 'SyubetuSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            .AppendLine("       return false;")
            .AppendLine("   }")

            .AppendLine("   function fncBikouLoad()")
            .AppendLine("   {")
            .AppendLine("       fncSetBikouScroll();")
            .AppendLine("       fncShowBikouErrorMessage();")
            .AppendLine("   }")


            .AppendLine("   function fncSaveBikouScroll(){")
            .AppendLine("       var divMeisai=$ID('" & Me.divMeisai.ClientID & "');")
            .AppendLine("       document.getElementById('" & Me.hidScroll.ClientID & "').value = divMeisai.scrollTop;")
            .AppendLine("   }")
            .AppendLine("   function fncSetBikouScroll(){")
            .AppendLine("       var divMeisai=$ID('" & Me.divMeisai.ClientID & "');")
            .AppendLine("       if(divMeisai != null)")
            .AppendLine("       {")
            .AppendLine("           var hidScroll=$ID('" & Me.hidScroll.ClientID & "');")
            .AppendLine("           if(hidScroll.value != '')")
            .AppendLine("           {")
            .AppendLine("               divMeisai.scrollTop = hidScroll.value;")
            .AppendLine("           }")
            .AppendLine("       }")
            .AppendLine("   }")


            'エラーメッセージを表示する
            .AppendLine("   var strMessage='';")
            .AppendLine("   var strItemId='';")
            .AppendLine("   function fncShowBikouErrorMessage()")
            .AppendLine("   {")
            .AppendLine("       if(strMessage != '')")
            .AppendLine("       {")
            .AppendLine("           alert(strMessage);")
            .AppendLine("       }")
            .AppendLine("       if($ID(strItemId) != null)")
            .AppendLine("       {")
            .AppendLine("           $ID(strItemId).focus();")
            .AppendLine("           try{ $ID(strItemId).select();}catch(e){}")
            .AppendLine("       }")
            .AppendLine("       strMessage = '';")
            .AppendLine("       strItemId = '';")
            .AppendLine("   }")


            .AppendLine("</script>  ")
        End With

        Me.Parent.Page.ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

    Protected Sub lnkTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkTitle.Click

        Me.divNaiyou.Visible = Not Me.divNaiyou.Visible

        If Me.divNaiyou.Visible Then
            Call Me.GamenInit()
        End If

    End Sub

End Class
