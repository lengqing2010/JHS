''' <summary>
''' DBアクセスに関する管理クラス
''' </summary>
''' <remarks>NotInheritable:インスタンスを生成させないようにする
''' 初回アクセス時にのみ起動し、DB接続文字列を設定する。
''' </remarks>
Public NotInheritable Class ManagerDA
    ''' <summary>
    ''' DB接続文字列の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared connStr As String = System.Configuration.ConfigurationManager. _
        ConnectionStrings("ConnectionString").ConnectionString
    ''' <summary>
    ''' DB接続文字列の取得
    ''' </summary>
    ''' <value>DB接続文字列</value>
    ''' <returns>DB接続文字列</returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Connection() As String
        Get
            Return connStr
        End Get
    End Property

End Class
