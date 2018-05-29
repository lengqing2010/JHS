Imports System.Configuration
Imports Itis.Earth.DataAccess

''' <summary>
''' �ڑ���������Ǘ�����N���X�ł��B
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
    ''' �C���X�^���X���擾���܂��B
    ''' </summary>
    ''' <returns>�B��̃C���X�^���X</returns>
    Public Shared ReadOnly Property Instance() As ConnectionManager
        Get
            Return singleton
        End Get
    End Property

    ''' <summary>
    ''' �ڑ���������擾���܂��B
    ''' </summary>
    ''' <returns>�ڑ�������</returns>
    Public ReadOnly Property ConnectionString() As String
        Get
            Return Me.EarthConnectionString
        End Get
    End Property

End Class
