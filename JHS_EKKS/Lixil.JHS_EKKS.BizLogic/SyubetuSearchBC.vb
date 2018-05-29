Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' 種別検索
''' </summary>
''' <remarks></remarks>
Public Class SyubetuSearchBC

    Private syubetuSearchDA As New DataAccess.SyubetuSearchDA

    ''' <summary>
    ''' 種別情報を検索する
    ''' </summary>
    ''' <param name="intRows">検索上限件数</param>
    ''' <param name="code">種別コード</param>
    ''' <param name="mei">種別名</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/03　李宇(大連情報システム部)　新規作成</history>
    Public Function SelSyubetu(ByVal intRows As String, _
                               ByVal code As String, _
                               ByVal mei As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          intRows, _
                                                                                          code, _
                                                                                          mei)

        Return syubetuSearchDA.SelSyubetu(intRows, code, mei)

    End Function

    ''' <summary>
    ''' 検索したデータ件数を取得する
    ''' </summary>
    ''' <param name="code">種別コード</param>
    ''' <param name="mei">種別名</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/03　李宇(大連情報システム部)　新規作成</history>
    Public Function SelSyubetuCount(ByVal code As String, _
                                    ByVal mei As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          code, _
                                                                                          mei)

        Return syubetuSearchDA.SelSyubetuCount(code, mei)

    End Function

End Class
