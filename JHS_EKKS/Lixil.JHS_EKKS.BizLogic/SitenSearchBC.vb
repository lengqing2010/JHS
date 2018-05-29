Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess
Public Class SitenSearchBC
    Private sitenSearchDA As New DataAccess.SitenSearchDA

    ''' <summary>
    ''' 部署管理マスタのデータを取得する
    ''' </summary>
    ''' <param name="strRows">データ数</param>
    ''' <param name="strBusyoMei">部署名</param>
    ''' <param name="blnTorikesi" >取消</param> 
    ''' <returns>部署管理マスタデータ</returns>
    ''' <history>2012/11/17　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function GetBusyoKanri(ByVal strRows As String, _
                                     ByVal strBusyoMei As String, ByVal blnTorikesi As Boolean, Optional ByVal blnAimai As Boolean = True, Optional ByVal strBusyoCD As String = "") As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strBusyoMei, _
                                                                                          blnTorikesi, _
                                                                                          blnAimai)

        Return sitenSearchDA.SelBusyoKanri(strRows, strBusyoMei, blnTorikesi, blnAimai, strBusyoCD)

    End Function

    ''' <summary>
    '''部署管理マスタのデータ件数を取得する 
    ''' </summary>
    ''' <param name="strBusyoMei">部署名</param>
    ''' <param name="blnTorikesi" >取消</param>
    ''' <returns>部署管理マスタデータ</returns>
    ''' <history>2012/11/17　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function GetDataCount(ByVal strBusyoMei As String, ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strBusyoMei)

        Return sitenSearchDA.SelDataCount(strBusyoMei, blnTorikesi)

    End Function
End Class
