Imports System.Configuration
Imports System.IO
Imports System.Text
Imports System.Net
Imports System.Data.SqlClient
Imports System.Globalization.CultureInfo

''' <summary>環境情報に関する共通処理</summary>
Public Class Definition
#Region "定数"
    'ファイル拡張子
    Private Const CON_INI As String = ".INI"
    Private Const CON_DAT As String = ".DAT"
    Private Const CON_SQL As String = ".SQL"
#End Region

#Region "プロパティ"
    ''' <summary>
    ''' 各ファイルパースの初期設定
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property DiskMei() As String
        Get
            Return ConfigurationManager.AppSettings("DISK_MEI")
        End Get
    End Property

    ''' <summary>
    ''' INIファイルパス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property INIFilePath() As String
        Get
            Return DiskMei & ConfigurationManager.AppSettings("INIFilePath")
        End Get
    End Property

    ''' <summary>
    ''' 検査報告書コピー元のパス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property KensaHoukokusyoCopyFromPath() As String
        Get
            Return ConfigurationManager.AppSettings("KensaHoukokusyoCopyFromPath")
        End Get
    End Property

    ''' <summary>
    ''' 検査報告書コピー先のパス
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property KensaHoukokusyoCopyToPath() As String
        Get
            Return ConfigurationManager.AppSettings("KensaHoukokusyoCopyToPath")
        End Get
    End Property

    ''' <summary>
    ''' 検査報告書エラーフォルダ名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property KensaHoukokusyoCopyErrorFolderName() As String
        Get
            Return ConfigurationManager.AppSettings("KensaHoukokusyoCopyErrorFolderName")
        End Get
    End Property


#End Region

#Region "ファイルメソッド"
    ''' <summary>
    ''' READERモードでファイルオープンする。
    ''' </summary>
    ''' <param name="strFile">ファイル</param>
    Public Shared Function OpenFileReader(ByVal strFile As String) As StreamReader
        Return New StreamReader(strFile, System.Text.Encoding.GetEncoding(932))
    End Function
#End Region

#Region "外部共通ファイルの設定メソッド"
    ''' <summary>
    ''' 検査報告書コピー元のパスを取得する
    ''' </summary>
    Public Shared Function GetKensaHoukokusyoCopyFromPath() As String
        Return KensaHoukokusyoCopyFromPath
    End Function

    ''' <summary>
    ''' 検査報告書コピー先のパスを取得する
    ''' </summary>
    Public Shared Function GetKensaHoukokusyoCopyToPath() As String
        Return KensaHoukokusyoCopyToPath
    End Function

    ''' <summary>
    ''' 検査報告書エラーフォルダ名を取得する
    ''' </summary>
    Public Shared Function GetKensaHoukokusyoCopyErrorFolderName() As String
        Return KensaHoukokusyoCopyErrorFolderName
    End Function

#End Region

#Region "GetMainDbConnectionString"
    ''' <summary>DB接続文字列の取得</summary>
    Public Shared Function GetConnectionStringEarth() As String
        Return ConfigurationManager.ConnectionStrings("connectionStringEarth").ConnectionString
    End Function

    ''' <summary>DB接続文字列の取得</summary>
    Public Shared Function GetConnectionStringJhsfgm() As String
        Return ConfigurationManager.ConnectionStrings("connectionStringJhsfgm").ConnectionString
    End Function

#End Region

End Class

