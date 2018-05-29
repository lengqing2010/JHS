Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

Public Class KeikakuKanriErrorDetailsBC

    Private objKeikakuKanriErrorDetailsDA As New DataAccess.KeikakuKanriErrorDetailsDA

    ''' <summary>
    '''計画管理表_取込エラー情報取得
    ''' </summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <returns>計画管理表_取込エラー情報テーブル</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/26　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetKeikakuTorikomiError(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDatetime As String) As DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
                                                                                          , strEdiJouhouSakuseiDate _
                                                                                          , strSyoriDatetime)

        Return objKeikakuKanriErrorDetailsDA.SelKeikakuTorikomiError(strEdiJouhouSakuseiDate, strSyoriDatetime)

    End Function

    ''' <summary>
    '''計画管理表_取込エラー情報件数取得
    ''' </summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <returns>計画管理表_取込エラー情報テーブル</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/26　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetKeikakuTorikomiErrorCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDatetime As String) As String

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
                                                                                          , strEdiJouhouSakuseiDate _
                                                                                          , strSyoriDatetime)

        Return objKeikakuKanriErrorDetailsDA.SelKeikakuTorikomiErrorCount(strEdiJouhouSakuseiDate, strSyoriDatetime)

    End Function
End Class
