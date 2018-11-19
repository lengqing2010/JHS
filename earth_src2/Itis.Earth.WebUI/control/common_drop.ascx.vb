Imports Itis.Earth.BizLogic

Partial Public Class common_drop
    Inherits System.Web.UI.UserControl
    Public Enum kbn As Integer
        todoufuken = 1
        builder = 2
        eigyousyo = 3
        keiretu = 4
        kubun = 5
        syubetsu = 6
        haccyu_teisi = 7
    End Enum
    Private CommonDropLogic As New CommonDropLogic
    ''' <summary> ClientID属性をセット</summary>
    Public ReadOnly Property DdlClientID() As String
        Get
            Return ddlCommonDrop.ClientID
        End Get
    End Property
    ''' <summary>Style属性をセット</summary>
    Public WriteOnly Property GetStyle() As kbn
        Set(ByVal value As kbn)
            Dim dtTodouhuken As Data.DataTable = CommonDropLogic.GetCommonDropInfo(value)
            SetValue(dtTodouhuken)
        End Set
    End Property
    ''' <summary>Value属性をセット</summary>
    Private Sub SetValue(ByVal dtTodouhuken As DataTable)
        Dim intRow As Integer = 0
        For intRow = 0 To dtTodouhuken.Rows.Count - 1
            Dim ddlist As New ListItem

            ddlist = New ListItem
            If dtTodouhuken.Rows(intRow).Item(1).ToString = "" Then
                ddlist.Text = dtTodouhuken.Rows(intRow).Item(0).ToString
            Else
                ddlist.Text = dtTodouhuken.Rows(intRow).Item(0).ToString & "：" & dtTodouhuken.Rows(intRow).Item(1).ToString
            End If
            ddlist.Value = dtTodouhuken.Rows(intRow).Item(0).ToString
            ddlCommonDrop.Items.Add(ddlist)
        Next
        Me.ddlCommonDrop.Items.Insert(0, New ListItem(String.Empty, String.Empty))

    End Sub
    ''' <summary>disabled属性をセット</summary>
    Public WriteOnly Property disabled() As Boolean
        Set(ByVal value As Boolean)
            ddlCommonDrop.Attributes.Add("disabled", value)
        End Set
    End Property
    Public WriteOnly Property CssClass() As String
        Set(ByVal value As String)
            ddlCommonDrop.CssClass = value
        End Set
    End Property
    Public WriteOnly Property GetWidth() As Integer
        Set(ByVal value As Integer)
            ddlCommonDrop.Width = Unit.Pixel(value)
        End Set
    End Property
    ''' <summary>Enabled属性をセット</summary>
    Public WriteOnly Property Enabled() As Boolean
        Set(ByVal value As Boolean)
            ddlCommonDrop.Enabled = value
        End Set
    End Property
    ''' <summary>SelectedValue属性をセット</summary>
    Public Property SelectedValue() As String
        Get
            Return Me.ddlCommonDrop.Items(ddlCommonDrop.SelectedIndex).Value
        End Get

        Set(ByVal strCD As String)
            Dim intCount As Long
            For intCount = 0 To ddlCommonDrop.Items.Count - 1
                If ddlCommonDrop.Items(intCount).Value = strCD Then
                    ddlCommonDrop.SelectedIndex = intCount
                    Exit For
                End If
            Next

        End Set
    End Property
    ''' <summary>SelectedText属性をセット</summary>
    Public Property SelectedText() As String
        Get
            Return Me.ddlCommonDrop.Items(ddlCommonDrop.SelectedIndex).Text
        End Get

        Set(ByVal strCD As String)
            Dim intCount As Long
            For intCount = 0 To ddlCommonDrop.Items.Count - 1
                If ddlCommonDrop.Items(intCount).Value = strCD Then
                    ddlCommonDrop.SelectedIndex = intCount
                    Exit For
                End If
            Next
        End Set
    End Property

    ''' <summary>色</summary>
    Public Property TextColor() As System.Drawing.Color
        Get
            Return Me.ddlCommonDrop.ForeColor
        End Get

        Set(ByVal strCD As System.Drawing.Color)
            Me.ddlCommonDrop.ForeColor = strCD
        End Set
    End Property

    Public ReadOnly Property Obj() As DropDownList
        Get
            Return Me.ddlCommonDrop
        End Get
    End Property

    ''' <summary>
    ''' OnClick
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property OnChange() As String
        Set(ByVal value As String)
            ViewState("subNameOnChange") = value
            Me.ddlCommonDrop.Attributes.Item("onchange") = "document.getElementById('" & Me.btnEvent1.ClientID & "').click()"
        End Set
    End Property

    Private Sub btnEvent1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEvent1.Click
        If ViewState("subNameOnChange") IsNot Nothing AndAlso ViewState("subNameOnChange").ToString.Trim <> String.Empty Then
            Dim functionName As String = ViewState("subNameOnChange").ToString
            Dim csScript As New StringBuilder
            Dim pPage As Page = Me.Parent.Page
            Dim pType As Type = pPage.GetType
            Dim fname As String = functionName.Replace("(", String.Empty).Replace(")", String.Empty).Replace(";", String.Empty).Trim
            Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod(fname)

            If Not methodInfo Is Nothing Then
                methodInfo.Invoke(pPage, New Object() {})
            End If
        End If
    End Sub
End Class