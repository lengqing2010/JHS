Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' 系列コード検索POPUP
''' </summary>
''' <remarks></remarks>
Public Class EigyousyoSearchBC

    Private eigyousyoSearchDA As New DataAccess.EigyousyoSearchDA

    ''' <summary>
    ''' 「部署管理マスタ」テープルより、営業所名を取得する
    ''' </summary>
    ''' <param name="strRows">検索上限件数</param>
    ''' <param name="strEigyousyoMei">営業所名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/17　李宇(大連情報システム部)　新規作成</history>
    Public Function GetEigyousyoMei(ByVal strRows As String, _
                                     ByVal strEigyousyoMei As String, _
                                     ByVal blnTorikesi As Boolean, Optional ByVal blnAimai As Boolean = True) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strEigyousyoMei, _
                                                                                          blnTorikesi)

        Return eigyousyoSearchDA.SelEigyousyoMei(strRows, strEigyousyoMei, blnTorikesi, blnAimai)

    End Function

    ''' <summary>
    ''' 検索したデータ件数を取得する
    ''' </summary>
    ''' <param name="strEigyousyoMei">営業所名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/17　李宇(大連情報システム部)　新規作成</history>
    Public Function GetEigyousyoMeiCount(ByVal strEigyousyoMei As String, ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strEigyousyoMei, _
                                                                                          blnTorikesi)

        Return eigyousyoSearchDA.SelEigyousyoMeiCount(strEigyousyoMei, blnTorikesi)

    End Function

End Class
