Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' 都道府県検索POPUP
''' </summary>
''' <remarks></remarks>
Public Class TodoufukenSearchDA

    ''' <summary>
    ''' 「都道府県名」の検索処理する
    ''' </summary>
    ''' <param name="strRows">検索上限件数</param>
    ''' <param name="strTodoufukenMei">都道府県名</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    '''  <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Public Function SelTodoufukenMei(ByVal strRows As String, _
                                     ByVal strTodoufukenMei As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strTodoufukenMei)

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
            .AppendLine("    todouhuken_cd")                   '都道府県コード
            .AppendLine("   ,todouhuken_mei")                  '都道府県名
            .AppendLine("FROM")
            .AppendLine("   m_todoufuken WITH(READCOMMITTED)")  '都道府県マスタ
            .AppendLine("WHERE")
            .AppendLine("1 = 1")

            If strTodoufukenMei <> "" Then
                .AppendLine("   AND todouhuken_mei LIKE @todoufuken_mei")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   todouhuken_mei")
        End With

        'バラメタ
        paramList.Add(MakeParam("@todoufuken_mei", SqlDbType.VarChar, 42, "%" & strTodoufukenMei & "%")) '都道府県名

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTodoufukenMei", paramList.ToArray())

        Return dsReturn.Tables("dtTodoufukenMei")

    End Function

    ''' <summary>
    ''' 検索したデータ件数を取得する
    ''' </summary>
    ''' <param name="strTodoufukenMei">都道府県名</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Public Function SelTodoufukenMeiCount(ByVal strTodoufukenMei As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strTodoufukenMei)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    COUNT(todouhuken_mei)")
            .AppendLine("FROM")
            .AppendLine("    m_todoufuken WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("     1 = 1")
            If strTodoufukenMei <> "" Then
                .AppendLine("    AND todouhuken_mei LIKE @todoufuken_mei")
            End If
        End With

        'バラメタ
        paramList.Add(MakeParam("@todoufuken_mei", SqlDbType.VarChar, 42, "%" & strTodoufukenMei & "%")) '都道府県名

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTodoufukenMeiCount", paramList.ToArray())

        Return dsReturn.Tables("dtTodoufukenMeiCount")

    End Function

End Class
