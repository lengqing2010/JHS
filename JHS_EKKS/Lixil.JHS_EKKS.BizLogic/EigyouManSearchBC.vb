Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess
Public Class EigyouManSearchBC
    Private egyouManSearchDA As New DataAccess.EigyouManSearchDA

    ''' <summary>
    ''' ユーザーデータを取得する
    ''' </summary>
    ''' <param name="strRows">検索上限件数</param>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <param name="strUserName">ユーザー名</param>
    ''' <param name="blnTorikesi" >取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/15　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function GetUserInfo(ByVal strRows As String, _
                                     ByVal strUserId As String, _
                                     ByVal strUserName As String, _
                                     ByVal blnTorikesi As Boolean, Optional ByVal blnAimai As Boolean = True) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strUserId, _
                                                                                          strUserName, blnAimai)

        Return egyouManSearchDA.SelUserInfo(strRows, strUserId, strUserName, blnTorikesi, blnAimai)

    End Function

    ''' <summary>
    ''' 検索したデータ件数を取得する
    ''' </summary>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <param name="strUserName">ユーザー名</param>
    ''' <param name="blnTorikesi" >取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/16　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function GetUserCount(ByVal strUserId As String, _
                                       ByVal strUserName As String, ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strUserId, _
                                                                                          strUserName)

        Return egyouManSearchDA.SelUserCount(strUserId, strUserName, blnTorikesi)

    End Function

End Class
