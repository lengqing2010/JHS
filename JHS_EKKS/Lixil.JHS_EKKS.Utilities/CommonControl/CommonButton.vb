Imports System.Web
Imports System.Drawing
Imports System.Text

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class CommonButton
    Inherits System.Web.UI.WebControls.Button

#Region "変数"
    Private _IsPageLoadFlg As Boolean
    Private _PowerEnabled As Boolean
#End Region

#Region "プロパティ"
    ''' <summary>
    ''' 権限フラグ
    ''' </summary>
    ''' <value>True:権限を判断する、False:権限を判断しない</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property IsPageLoadFlg() As Boolean
        Get
            IsPageLoadFlg = _IsPageLoadFlg
        End Get
        Set(ByVal value As Boolean)
            _IsPageLoadFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 権限有無フラグ
    ''' </summary>
    ''' <value>True:権限有り、False:権限無し</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PowerEnabled() As Boolean
        Get
            PowerEnabled = _PowerEnabled
        End Get
        Set(ByVal value As Boolean)
            _PowerEnabled = value
        End Set
    End Property

    'Public Overrides Property Enabled() As Boolean
    '    Get
    '        Return MyBase.Enabled
    '    End Get
    '    Set(ByVal value As Boolean)
    '        If Not IsPageLoadFlg Then
    '            MyBase.Enabled = value
    '        Else
    '            '権限あり場合、このボタンを使用可にする
    '            If PowerEnabled Then
    '                MyBase.Enabled = value
    '            Else
    '                If Not value Then
    '                    MyBase.Enabled = value
    '                End If
    '            End If
    '        End If
    '    End Set
    'End Property
#End Region

#Region "事件"

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
        End If

        IsPageLoadFlg = True
        MyBase.OnLoad(e)
    End Sub

    Protected Overrides Sub OnClick(ByVal e As System.EventArgs)
        '共通処理


        '画面処理
        MyBase.OnClick(e)


    End Sub
#End Region
End Class
