Imports System.Transactions
Imports System.text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
''' <summary>
''' 入金エラー処理に関する処理を行うロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class NyuukinErrorLogic
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "プロパティ"
#Region "ログインユーザーID"
    ''' <summary>
    ''' ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strLoginUserId As String
    ''' <summary>
    ''' ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns> ログインユーザーID</returns>
    ''' <remarks></remarks>
    Public Property LoginUserId() As String
        Get
            Return strLoginUserId
        End Get
        Set(ByVal value As String)
            strLoginUserId = value
        End Set
    End Property
#End Region
#End Region

#Region "初期処理"
    ''' <summary>
    ''' 入金エラー情報を取得します
    ''' </summary>
    ''' <param name="strEdiJouhou">EDI情報作成日</param>
    ''' <returns>入金エラー情報を格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetNyuukinErrData(ByVal intNyuukinFileKbn As Integer, ByVal strEdiJouhou As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetNyuukinErrData" _
                                                    , intNyuukinFileKbn _
                                                    , strEdiJouhou)
        Dim clsDataAccess As New NyuukinErrorDataAccess
        Dim nyuukinErrTable As NyuukinErrorDataSet.ErrorJouhouDataTable

        nyuukinErrTable = clsDataAccess.GetNyuukinErrData(intNyuukinFileKbn, strEdiJouhou)

        Return nyuukinErrTable
    End Function
#End Region
End Class
