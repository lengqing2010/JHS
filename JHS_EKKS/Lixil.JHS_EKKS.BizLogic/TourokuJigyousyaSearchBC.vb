Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' 都道府県検索POPUP
''' </summary>
''' <remarks></remarks>
Public Class TourokuJigyousyaSearchBC

    Private tourokuJigyousyaSearchDA As New DataAccess.TourokuJigyousyaSearchDA

    ''' <summary>
    ''' 「加盟店コード」、「加盟店名」、「都道府県名」と「加盟店カナ名」の検索処理する
    ''' </summary>
    ''' <param name="strRows">検索上限件数</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKameitenMei">加盟店名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Public Function GetTourokuJigyousya(ByVal strRows As String, _
                                     ByVal strKameitenCd As String, _
                                     ByVal strKameitenMei As String, _
                                     ByVal blnTorikesi As Boolean, Optional ByVal blnAimai As Boolean = True) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
                                                                                          blnTorikesi)

        Return tourokuJigyousyaSearchDA.SelTourokuJigyousya(strRows, strKameitenCd, strKameitenMei, blnTorikesi, blnAimai)

    End Function

    ''' <summary>
    ''' 検索したデータ件数を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKameitenMei">加盟店名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Public Function GetTourokuJigyousyaCount(ByVal strKameitenCd As String, _
                                          ByVal strKameitenMei As String, _
                                          ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
                                                                                          blnTorikesi)

        Return tourokuJigyousyaSearchDA.SelTourokuJigyousyaCount(strKameitenCd, strKameitenMei, blnTorikesi)

    End Function

End Class
