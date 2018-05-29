Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' 営業所検索_計画管理_加盟店検索照会指示用POPUP
''' </summary>
''' <remarks></remarks>
Public Class EigyousyoSearchSyoukaiJisiyouBC

    Private eigyousyoSearchSyoukaiJisiyouDA As New DataAccess.EigyousyoSearchSyoukaiJisiyouDA

    ''' <summary>
    ''' 「営業所マスタ」から、営業所コードを取得する
    ''' </summary>
    ''' <param name="strRows">検索上限件数</param>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    ''' <param name="strEigyousyoMei">営業所名</param>
    ''' <param name="blnTorikesi">先画面の退会した加盟店をチェック</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/07/18　李宇(大連情報システム部)　新規作成</history>
    Public Function GetEigyousyo(ByVal strRows As String, _
                                   ByVal strEigyousyoCd As String, _
                                   ByVal strEigyousyoMei As String, _
                                   ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strEigyousyoCd, _
                                                                                          strEigyousyoMei, _
                                                                                          blnTorikesi)

        Return eigyousyoSearchSyoukaiJisiyouDA.SelEigyousyo(strRows, strEigyousyoCd, strEigyousyoMei, blnTorikesi)
    End Function

    ''' <summary>
    ''' 検索したデータ件数を取得する
    ''' </summary>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    ''' <param name="strEigyousyoMei">営業所名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/07/18　李宇(大連情報システム部)　新規作成</history>
    Public Function GetEigyousyoCount(ByVal strEigyousyoCd As String, _
                                        ByVal strEigyousyoMei As String, _
                                        ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strEigyousyoCd, _
                                                                                          strEigyousyoMei, _
                                                                                          blnTorikesi)

        Return eigyousyoSearchSyoukaiJisiyouDA.SelEigyousyoCount(strEigyousyoCd, strEigyousyoMei, blnTorikesi)
    End Function

End Class
