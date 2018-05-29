Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

Public Class SitenTukibetuKeikakuchiSearchListBC

    Private objSitenTukibetuKeikakuchiSearchListDA As New DataAccess.SitenTukibetuKeikakuchiSearchListDA

    ''' <summary>
    '''支店別月別計画管理テーブルにより、明細データを取得する
    ''' </summary>
    ''' <param name="keikakuNendo">計画年度</param>
    ''' <param name="busyoCd">部署コード</param>
    ''' <returns>支店別月別計画管理テーブル</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetSitenbetuTukiKeikakuKanri(ByVal keikakuNendo As String, ByVal busyoCd As String) As DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
                                                                                        , keikakuNendo, busyoCd)

        Return objSitenTukibetuKeikakuchiSearchListDA.SelSitenbetuTukiKeikakuKanri(keikakuNendo, busyoCd)

    End Function

    ''' <summary>
    '''支実績管理テーブルにより、前年明細データを取得する
    ''' </summary>
    ''' <param name="keikakuNendo">計画年度</param>
    ''' <param name="busyoCd">部署コード</param>
    ''' <returns>実績管理テーブルテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetJissekiKanri(ByVal keikakuNendo As String, ByVal busyoCd As String) As DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
                                                                                        , keikakuNendo, busyoCd)

        Return objSitenTukibetuKeikakuchiSearchListDA.SelJissekiKanri(Convert.ToString(DateAdd(DateInterval.Year, -1, CDate(keikakuNendo & "/01/01")).Year), busyoCd)

    End Function

    ''' <summary>
    '''支店別計画管理テーブルにより、CSVタイトル合計データを取得する
    ''' </summary>
    ''' <param name="keikakuNendo">計画年度</param>
    ''' <param name="busyoCd">部署コード</param>
    ''' <returns>支店別計画管理テーブル</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/06　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function GetSitenbetuKeikakuKanri(ByVal keikakuNendo As String, ByVal busyoCd As String) As DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
                                                                                        , keikakuNendo, busyoCd)

        Return objSitenTukibetuKeikakuchiSearchListDA.SelSitenbetuKeikakuKanri(keikakuNendo, busyoCd)

    End Function

    ''' <summary>
    ''' 確定FLGの更新
    ''' </summary>
    ''' <param name="keikakuNendo">計画_年度</param>
    ''' <param name="busyoCd">部署ｺｰﾄﾞ</param>
    ''' <param name="addDatetime">登録日時</param>
    ''' <param name="addEigyouKbn">営業区分</param>
    ''' <param name="userId">更新ログインユーザーID</param>
    ''' <param name="kakuteiFlg">確定FLG</param>
    ''' <returns></returns>
    ''' <history>2012/12/11　楊双(大連情報システム部)　新規作成 </history>
    Public Function SetKakuteiFlg(ByVal keikakuNendo As String, ByVal busyoCd As String, _
                                  ByVal addDatetime As ArrayList, ByVal addEigyouKbn As ArrayList, ByVal userId As String, _
                                  ByVal kakuteiFlg As String) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                keikakuNendo, busyoCd, addDatetime, addEigyouKbn, userId, kakuteiFlg)

        Dim i As Integer

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)

            Try

                For i = 0 To addDatetime.Count - 1

                    '確定FLGの更新
                    If Not objSitenTukibetuKeikakuchiSearchListDA.UpdKakuteiFlg(keikakuNendo, busyoCd, CType(addDatetime(i), DateTime).ToString("yyyy/MM/dd HH:mm:ss.fff"), addEigyouKbn(i).ToString, userId, kakuteiFlg) Then
                        Throw New ApplicationException
                    End If

                Next
                scope.Complete()

            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using

        Return True

    End Function

End Class
