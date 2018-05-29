Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports Lixil.JHS_EKKS.Utilities
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' 支店 月別計画値 EXCEL取込エラー
''' </summary>
''' <remarks></remarks>
Public Class SitenTukibetuKeikakuchiErrorDetailsBC

    Private sitenTukibetuKeikakuchiErrorDetailsDA As New DataAccess.SitenTukibetuKeikakuchiErrorDetailsDA

    ''' <summary>
    ''' エラー内容を取得する
    ''' </summary>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <returns>Data.DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/27　李宇(大連情報システム部)　新規作成</history>
    Public Function GetErrorJyouhou(ByVal strSyoriDatetime As String, _
                                         ByVal strEdiJouhouSakuseiDate As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strSyoriDatetime, _
                                                                                          strEdiJouhouSakuseiDate)

        Return sitenTukibetuKeikakuchiErrorDetailsDA.SelErrorJyouhou(strSyoriDatetime, strEdiJouhouSakuseiDate)

    End Function


    ''' <summary>
    ''' エラー内容のデータすうを取得する
    ''' </summary>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <returns>Data.DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/27　李宇(大連情報システム部)　新規作成</history>
    Public Function GetErrorJyouhouCount(ByVal strSyoriDatetime As String, _
                                         ByVal strEdiJouhouSakuseiDate As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strSyoriDatetime, _
                                                                                          strEdiJouhouSakuseiDate)

        Return sitenTukibetuKeikakuchiErrorDetailsDA.SelErrorJyouhouCount(strSyoriDatetime, strEdiJouhouSakuseiDate)

    End Function

End Class
