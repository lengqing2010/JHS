Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess


''' <summary>
''' 計画管理_加盟店　検索POPUP
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKanriKameitenSearchBC

    Private KeikakuKanriKameitenSearchDA As New DataAccess.KeikakuKanriKameitenSearchDA

    ''' <summary>
    ''' 「加盟店コード」、「加盟店名」、「都道府県名」の検索処理する
    ''' </summary>
    ''' <param name="strRows">検索上限件数</param>
    ''' <param name="strYear">年度</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKameitenMei">加盟店名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Public Function GetKeikakuKanriKameiten(ByVal strRows As String, _
                                            ByVal strYear As String, _
                                            ByVal strKameitenCd As String, _
                                            ByVal strKameitenMei As String, _
                                            ByVal blnTorikesi As Boolean, Optional ByVal blnAimai As Boolean = True) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strYear, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
                                                                                          blnTorikesi)

        Return KeikakuKanriKameitenSearchDA.SelKeikakuKanriKameiten(strRows, strYear, strKameitenCd, strKameitenMei, blnTorikesi, blnAimai)

    End Function

    ''' <summary>
    ''' 検索したデータ件数を取得する
    ''' </summary>
    ''' <param name="strYear">年度</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKameitenMei">加盟店名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Public Function GetKeikakuKanriKameitenCount(ByVal strYear As String, _
                                                 ByVal strKameitenCd As String, _
                                                 ByVal strKameitenMei As String, _
                                                 ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strYear, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
                                                                                          blnTorikesi)

        Return KeikakuKanriKameitenSearchDA.SelKeikakuKanriKameitenCount(strYear, strKameitenCd, strKameitenMei, blnTorikesi)

    End Function

End Class
