Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Microsoft.VisualBasic
Imports System.Web
Imports system.Web.HttpContext
Imports System.Configuration
Imports Lixil.JHS_EKKS.DataAccess
Public Class CommonBC
    Private CommonDA As New CommonDA
    '''<summary>LOGの新規処理</summary>
    Public Function SetUserInfo(ByVal strUrl As String, ByVal strUserId As String, ByVal strSousa As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strUrl, strUserId, strSousa)

        Return CommonDA.InsUrlLog(strUrl, strUserId, strSousa)
    End Function
    ''' <summary>システムを取得</summary>
    Public Function SelSystemDate() As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Return CommonDA.SelSystemDate()
    End Function

    ''' <summary>計画用_名称マスタから計画年度リストを取得する</summary>
    Public Function GetKeikakuNendoData() As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)
        Return CommonDA.SelKeikakuNendoData()
    End Function
End Class
