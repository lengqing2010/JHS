Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' 営業所検索_計画管理_加盟店検索照会指示用POPUP
''' </summary>
''' <remarks></remarks>
Public Class EigyousyoSearchSyoukaiJisiyouDA

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
    Public Function SelEigyousyo(ByVal strRows As String, _
                                   ByVal strEigyousyoCd As String, _
                                   ByVal strEigyousyoMei As String, _
                                   ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strEigyousyoCd, _
                                                                                          strEigyousyoMei, _
                                                                                          blnTorikesi)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            If strRows = "0" Then
                .AppendLine("   TOP 100")
            End If
            .AppendLine("     eigyousyo_cd")                   '営業所コード
            .AppendLine("    ,eigyousyo_mei")                  '営業所名
            .AppendLine("FROM")
            .AppendLine("   m_eigyousyo  WITH(READCOMMITTED)") '営業所マスタ
            .AppendLine("WHERE")
            .AppendLine("   1 = 1")
            If strEigyousyoCd <> "" Then
                .AppendLine("   AND eigyousyo_cd LIKE @eigyousyo_cd")
            End If
            If strEigyousyoMei <> "" Then
                .AppendLine("   AND eigyousyo_mei LIKE @eigyousyo_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   eigyousyo_cd")
        End With


        'バラメタ
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 6, strEigyousyoCd & "%"))            '営業所コード
        paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 42, "%" & strEigyousyoMei & "%"))   '営業所名
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                                    '取消

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtEigyousyo", paramList.ToArray())

        Return dsReturn.Tables("dtEigyousyo")

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
    Public Function SelEigyousyoCount(ByVal strEigyousyoCd As String, _
                                         ByVal strEigyousyoMei As String, _
                                         ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strEigyousyoCd, _
                                                                                          strEigyousyoMei, _
                                                                                          blnTorikesi)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    COUNT(eigyousyo_cd)")
            .AppendLine("FROM")
            .AppendLine("    m_eigyousyo  WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("     1 = 1")
            If strEigyousyoCd <> "" Then
                .AppendLine("   AND eigyousyo_cd LIKE @eigyousyo_cd")
            End If
            If strEigyousyoMei <> "" Then
                .AppendLine("   AND eigyousyo_mei LIKE @eigyousyo_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
        End With

        'バラメタ
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strEigyousyoCd & "%"))            '営業所コード
        paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 40, "%" & strEigyousyoMei & "%"))   '営業所名
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                                    '取消

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtEigyousyoCount", paramList.ToArray())

        Return dsReturn.Tables("dtEigyousyoCount")

    End Function

End Class
