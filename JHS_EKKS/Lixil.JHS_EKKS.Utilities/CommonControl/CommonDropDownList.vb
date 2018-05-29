Imports System.Web
Imports System.Drawing

Public Class CommonDropDownList
    Inherits System.Web.UI.WebControls.DropDownList

#Region "�ϐ�"
    Private _IsAddNullRow As Boolean = False
#End Region

#Region "�v���p�e�B"
    ''' <summary>
    ''' �󔒍s�̒ǉ�
    ''' </summary>
    ''' <value>True:�ǉ��AFalse:�ǉ����Ȃ�</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsAddNullRow() As Boolean
        Get
            IsAddNullRow = Me._IsAddNullRow

        End Get
        Set(ByVal value As Boolean)
            Me._IsAddNullRow = value
        End Set
    End Property

    ''' <summary>
    ''' �f�[�^��ݒ肷��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property DataSource() As Object
        Get
            Return MyBase.DataSource
        End Get
        Set(ByVal value As Object)
            Dim dt As DataTable
            Dim dr As DataRow

            '�󔒍s��ǉ�����
            If IsAddNullRow Then
                dt = CType(value, DataTable)
                dr = dt.NewRow()
                dr(0) = ""
                dr(1) = ""
                dt.Rows.InsertAt(dr, 0)
                MyBase.DataSource = dt
            Else
                MyBase.DataSource = value
            End If

            'MyBase.DataSource = value
        End Set
    End Property

#End Region

End Class
