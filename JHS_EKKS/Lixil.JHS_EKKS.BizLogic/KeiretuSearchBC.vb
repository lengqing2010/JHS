Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' 系列コード検索POPUP
''' </summary>
''' <remarks></remarks>
Public Class KeiretuSearchBC

    Private keiretuSearchDA As New DataAccess.KeiretuSearchDA

    ''' <summary>
    ''' 「系列マスタ」テープルより、系列情報を取得する
    ''' </summary>
    ''' <param name="strRows">検索上限件数</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strKeiretuMei">系列名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/15　李宇(大連情報システム部)　新規作成</history>
    Public Function GetKiretuJyouhou(ByVal strRows As String, _
                                     ByVal strKeiretuCd As String, _
                                     ByVal strKeiretuMei As String, _
                                     ByVal blnTorikesi As Boolean, Optional ByVal blnAimai As Boolean = True) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strKeiretuCd, _
                                                                                          strKeiretuMei, _
                                                                                          blnTorikesi)

        Return keiretuSearchDA.SelKiretuJyouhou(strRows, strKeiretuCd, strKeiretuMei, blnTorikesi, blnAimai)

    End Function

    ''' <summary>
    ''' 検索したデータ件数を取得する
    ''' </summary>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strKeiretuMei">系列名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/16　李宇(大連情報システム部)　新規作成</history>
    Public Function GetKiretuJyouhouCount(ByVal strKeiretuCd As String, _
                                          ByVal strKeiretuMei As String, _
                                          ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strKeiretuCd, _
                                                                                          strKeiretuMei, _
                                                                                          blnTorikesi)

        Return keiretuSearchDA.SelKiretuJyouhouCount(strKeiretuCd, strKeiretuMei, blnTorikesi)

    End Function

End Class
