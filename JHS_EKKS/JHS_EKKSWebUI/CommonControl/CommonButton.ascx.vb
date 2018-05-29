Imports System.Drawing
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.BizLogic
Partial Class CommonControl_CommonButton
    Inherits System.Web.UI.UserControl
    Private KegenCheck As kegen = Nothing
    Private subName As String = ""

    Public WriteOnly Property ButtonKegen() As kegen
        Set(ByVal value As kegen)
            KegenCheck = value
        End Set
    End Property

    ''' <summary>
    ''' Width
    ''' </summary>
    ''' <value></value>
    ''' <returns>Width</returns>
    ''' <remarks></remarks>
    Public Property Width() As Web.UI.WebControls.Unit
        Get
            Return Me.btnCommon.Width
        End Get
        Set(ByVal value As Web.UI.WebControls.Unit)
            Me.btnCommon.Width = value
        End Set
    End Property

    ''' <summary>
    ''' Height
    ''' </summary>
    ''' <value></value>
    ''' <returns>Height</returns>
    ''' <remarks></remarks>
    Public Property Height() As Web.UI.WebControls.Unit
        Get
            Return Me.btnCommon.Height
        End Get
        Set(ByVal value As Web.UI.WebControls.Unit)
            Me.btnCommon.Height = value
        End Set
    End Property

    ''' <summary>
    ''' Text
    ''' </summary>
    ''' <value></value>
    ''' <returns>Text</returns>
    ''' <remarks></remarks>
    Public Property OnClientClick() As String
        Get
            Return Me.btnCommon.OnClientClick
        End Get
        Set(ByVal value As String)
            Me.btnCommon.OnClientClick = value
        End Set
    End Property
    ''' <summary>
    ''' Text
    ''' </summary>
    ''' <value></value>
    ''' <returns>Text</returns>
    ''' <remarks></remarks>
    Public Property Text() As String
        Get
            Return Me.btnCommon.Text
        End Get
        Set(ByVal value As String)
            Me.btnCommon.Text = value
        End Set
    End Property
    ''' <summary>
    ''' CSS
    ''' </summary>
    ''' <value></value>
    ''' <returns>CSS</returns>
    ''' <remarks></remarks>
    Public Property Cssclass() As String
        Get
            Return Me.btnCommon.CssClass
        End Get
        Set(ByVal value As String)
            Me.btnCommon.CssClass = value
        End Set
    End Property
    ''' <summary>
    ''' TabIndex
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property TabIndex() As Short
        Get
            Return Me.btnCommon.TabIndex
        End Get
        Set(ByVal value As Short)
            Me.btnCommon.TabIndex = value
        End Set
    End Property

    Public Property BackColor() As color
        Get
            BackColor = Me.btnCommon.BackColor
        End Get
        Set(ByVal value As color)
            Me.btnCommon.BackColor = value
        End Set
    End Property

    ''' <summary>
    ''' OnClick
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property OnClick() As String
        Set(ByVal value As String)
            subName = value
        End Set
    End Property

    ''' <summary>
    ''' Button
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property Button() As Button
        Get
            Return Me.btnCommon
        End Get
    End Property

    ''' <summary>
    ''' ButtonClientId
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public ReadOnly Property ButtonClientId() As String
        Get
            Return Me.btnCommon.ClientID
        End Get
    End Property

    Public Property Enabled() As Boolean
        Get
            Enabled = Me.btnCommon.Enabled
        End Get
        Set(ByVal value As Boolean)
            Dim UserId As String = ""
            Dim CommonCheck As New CommonCheck
            Dim LoginUserInfoList As New LoginUserInfoList
            Dim blnEnabled As Boolean

            '権限チェック
            If Not KegenCheck = Nothing Then
                blnEnabled = CommonCheck.CommonNinsyou(UserId, LoginUserInfoList, KegenCheck)
            Else
                blnEnabled = CommonCheck.CommonNinsyou(UserId, LoginUserInfoList, kegen.UserIdOnly)
            End If

            If blnEnabled Then
                Me.btnCommon.Enabled = value
            Else
                If Not value Then
                    Me.btnCommon.Enabled = value
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' ボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnCommon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCommon.Click
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                     MyMethod.GetCurrentMethod.Name)
        Dim cCommon As New Common
        cCommon.RunSub(Me.Parent.Page, subName)
        'アクセス記録の保持
        If Not ViewState("UserId") Is Nothing Then
            cCommon.SetURL(Page, ViewState("UserId").ToString, btnCommon.Text)
        Else
            Dim userInfo As New LoginUserInfoList
            Dim strUserId As String = ""
            Dim CommonCheck As New CommonCheck
            CommonCheck.CommonNinsyou(strUserId, userInfo, kegen.UserIdOnly)
            cCommon.SetURL(Page, strUserId, btnCommon.Text)
        End If

    End Sub

    ''' <summary>
    ''' フォーム初期化
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                     MyMethod.GetCurrentMethod.Name)
        Dim UserId As String = ""
        Dim CommonCheck As New CommonCheck
        Dim LoginUserInfoList As New LoginUserInfoList
        Dim blnEnabled As Boolean

        If Not IsPostBack Then
            '権限チェック
            If btnCommon.Enabled Then
                If Not KegenCheck = Nothing Then
                    blnEnabled = CommonCheck.CommonNinsyou(UserId, LoginUserInfoList, KegenCheck)
                    Me.btnCommon.PowerEnabled = blnEnabled
                    If btnCommon.Enabled Then
                        btnCommon.Enabled = blnEnabled
                    End If
                Else
                    blnEnabled = CommonCheck.CommonNinsyou(UserId, LoginUserInfoList, kegen.UserIdOnly)
                    Me.btnCommon.PowerEnabled = blnEnabled
                    If btnCommon.Enabled Then
                        btnCommon.Enabled = blnEnabled
                    End If
                End If
            Else
                Call CommonCheck.CommonNinsyou(UserId, LoginUserInfoList, kegen.UserIdOnly)
            End If

            ViewState("UserId") = UserId
        End If
    End Sub
End Class
