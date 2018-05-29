Imports Lixil.JHS_EKKS.BizLogic
Imports Messages = Lixil.JHS_EKKS.Utilities.CommonMessage
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Partial Class EKKSMaster
    Inherits System.Web.UI.MasterPage
    Private Const conKaisiPage As String = "~/MainMenu.aspx;~/NendoKeikakuInput.aspx;~/KeikakuKanriInput.aspx;~/KeikakuKanriErrorDetails.aspx;~/SitenTukibetuKeikakuchiInput.aspx;~/SitenTukibetuKeikakuchiErrorDetails.aspx;~/CommonErr.aspx"
    Private masterLoginUserInfoList As New LoginUserInfoList

    ''' <summary>
    ''' 営業計画管理ボタン
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property EigyouButton() As Button
        Get
            Return Me.btnEigyouKeikaku
        End Get
    End Property
    ''' <summary>
    ''' 売上予実管理ボタン
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UriYojituButton() As Button
        Get
            Return Me.btnUriYojitu
        End Get
    End Property
    ''' <summary>
    ''' 計画用_加盟店情報照会
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property KeikakuKanriKameitenKensakuSyoukaiButton() As Button
        Get
            Return Me.btnKeikakuKanriKameitenKensakuSyoukai
        End Get
    End Property

    ''' <summary>
    ''' ユーザー権限と情報
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property loginUserInfo() As LoginUserInfoList
        Get
            Return masterLoginUserInfoList
        End Get
        Set(ByVal value As LoginUserInfoList)
            masterLoginUserInfoList = value
        End Set
    End Property

    Public WriteOnly Property BusyoName() As String
        Set(ByVal value As String)
            TdBusyoName.InnerText = value
        End Set
    End Property
    Public WriteOnly Property UserName() As String
        Set(ByVal value As String)
            TdUserName.InnerText = value
        End Set
    End Property
    Private _showMode As String = "ShowModal();"

    Public WriteOnly Property ShowMode() As String

        Set(ByVal value As String)
            _showMode = value
        End Set

    End Property

    Public ReadOnly Property DivDisableId() As UI.HtmlControls.HtmlGenericControl
        Get
            Return Me.disableDiv
        End Get
    End Property

    Public ReadOnly Property DivBuySelName() As UI.HtmlControls.HtmlGenericControl
        Get
            Return Me.buySelName
        End Get
    End Property



    ''' <summary>
    ''' フォーム初期化
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        Me.form1.Attributes("onsubmit") = _showMode

        If Not IsPostBack Then
            '業務メニュー
            linkMain.HRef = CommonConstBC.main
            ViewState("kaisiPage") = Context.Items("kaisiPage")
            If conKaisiPage.IndexOf(Page.AppRelativeVirtualPath) >= 0 Then 'IF 始め画面表示
                ViewState("kaisiPage") = conKaisiPage
            ElseIf ViewState("kaisiPage") Is Nothing Then
                ViewState("kaisiPage") = ""
                Context.Items("strFailureMsg") = ""
                Context.Server.Transfer("./MainMenu.aspx")
            End If

            '共通情報セット
            If masterLoginUserInfoList.Items.Count <> 0 Then
                Me.TdBusyoName.InnerText = masterLoginUserInfoList.Items(0).SyozokuBusyo
                Me.TdUserName.InnerText = masterLoginUserInfoList.Items(0).Simei
                '営業計画管理
                If masterLoginUserInfoList.Items(0).EigyouKeikakuKenriSansyou = -1 Then
                    Me.linkEigyouKeikaku.HRef = "about:blank"
                    linkEigyouKeikaku.Attributes("onclick") = "objEBI('" & btnEigyouKeikaku.ClientID & "').click();return false;"

                End If
                '売上予実管理
                If masterLoginUserInfoList.Items(0).UriYojituKanriSansyou = -1 Then
                    Me.linkUriYojitu.HRef = "about:blank"
                    linkUriYojitu.Attributes("onclick") = "objEBI('" & btnUriYojitu.ClientID & "').click();return false;"

                End If

                '計画用_加盟店情報照会
                Me.LinkKeikakuKanriKameitenKensakuSyoukai.HRef = "about:blank"
                LinkKeikakuKanriKameitenKensakuSyoukai.Attributes("onclick") = "objEBI('" & Me.btnKeikakuKanriKameitenKensakuSyoukai.ClientID & "').click();return false;"
            End If
            '稼動チェック
            Dim CommonCheck As New CommonCheck
            If "~/CommonErr.aspx".IndexOf(Page.AppRelativeVirtualPath) < 0 And Context.Items("Check") Is Nothing Then

                If CommonCheck.KadouCheck = False Then
                    Context.Items("Check") = "1"
                    'If blnJikan = False Then
                    Context.Items("strFailureMsg") = Messages.MSG034E
                    Context.Items("kaisiPage") = ViewState("kaisiPage")
                    Context.Server.Transfer("./CommonErr.aspx")
                Else
                    Context.Items("Check") = Nothing
                End If
            End If
        Else
            Context.Items("kaisiPage") = ViewState("kaisiPage")
        End If

    End Sub
    ''' <summary>
    ''' 営業計画管理クッリク
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnEigyouKeikaku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEigyouKeikaku.Click
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        Context.Items("kaisiPage") = ViewState("kaisiPage")
        Server.Transfer(CommonConstBC.eigyouKeikakuKanri)
    End Sub

    ''' <summary>
    ''' 売上予実管理クッリク
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUriYojitu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUriYojitu.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        Context.Items("kaisiPage") = ViewState("kaisiPage")
        Server.Transfer(CommonConstBC.uriageYojituKanri)
    End Sub

    ''' <summary>
    ''' 計画用_加盟店情報照会クッリク
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnKeikakuKanriKameitenKensakuSyoukai_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKeikakuKanriKameitenKensakuSyoukai.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        Context.Items("kaisiPage") = ViewState("kaisiPage")
        Server.Transfer(CommonConstBC.keikakuKanriKameitenKensakuSyoukai)
    End Sub
End Class

