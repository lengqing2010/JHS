Imports System.Configuration
Imports Itis.Earth.DataAccess

''' <summary>
''' 接続文字列を管理するクラスです。
''' </summary>
Public Class ConnectionManager
    Private Shared ReadOnly singleton As New ConnectionManager
    Private Sub New()
    End Sub
    '    Private ReadOnly EarthConnectionString As String _
    '        = Itis.Earth.DataAccess.Settings.Default.EarthConnectionString
    '           = ConfigurationManager.ConnectionStrings("EarthConnectionString").ConnectionString

    Private ReadOnly setting As ConnectionStringSettings = _
                ConfigurationManager.ConnectionStrings("EarthConnectionString")
    Private ReadOnly EarthConnectionString As String = setting.ConnectionString



    ''' <summary>
    ''' インスタンスを取得します。
    ''' </summary>
    ''' <returns>唯一のインスタンス</returns>
    Public Shared ReadOnly Property Instance() As ConnectionManager
        Get
            Return singleton
        End Get
    End Property

    ''' <summary>
    ''' 接続文字列を取得します。
    ''' </summary>
    ''' <returns>接続文字列</returns>
    Public ReadOnly Property ConnectionString() As String
        Get
            Return Me.EarthConnectionString
        End Get
    End Property

End Class
