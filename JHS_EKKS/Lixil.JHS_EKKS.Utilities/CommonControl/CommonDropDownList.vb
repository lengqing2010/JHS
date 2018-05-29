Imports System.Web
Imports System.Drawing

Public Class CommonDropDownList
    Inherits System.Web.UI.WebControls.DropDownList

#Region "変数"
    Private _IsAddNullRow As Boolean = False
#End Region

#Region "プロパティ"
    ''' <summary>
    ''' 空白行の追加
    ''' </summary>
    ''' <value>True:追加、False:追加しない</value>
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
    ''' データを設定する
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

            '空白行を追加する
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
