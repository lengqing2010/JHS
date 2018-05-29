Imports System.Web
Imports System.Drawing
Imports System.Text

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class CommonButton
    Inherits System.Web.UI.WebControls.Button

#Region "�ϐ�"
    Private _IsPageLoadFlg As Boolean
    Private _PowerEnabled As Boolean
#End Region

#Region "�v���p�e�B"
    ''' <summary>
    ''' �����t���O
    ''' </summary>
    ''' <value>True:�����𔻒f����AFalse:�����𔻒f���Ȃ�</value>
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
    ''' �����L���t���O
    ''' </summary>
    ''' <value>True:�����L��AFalse:��������</value>
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
    '            '��������ꍇ�A���̃{�^�����g�p�ɂ���
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

#Region "����"

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
        End If

        IsPageLoadFlg = True
        MyBase.OnLoad(e)
    End Sub

    Protected Overrides Sub OnClick(ByVal e As System.EventArgs)
        '���ʏ���


        '��ʏ���
        MyBase.OnClick(e)


    End Sub
#End Region
End Class
