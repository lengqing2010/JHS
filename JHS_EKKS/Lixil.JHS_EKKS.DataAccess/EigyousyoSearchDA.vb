Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' 営業所検索POPUP
''' </summary>
''' <remarks></remarks>
Public Class EigyousyoSearchDA

    ''' <summary>
    ''' 「部署管理マスタ」テープルより、営業所名を取得する
    ''' </summary>
    ''' <param name="strRows">検索上限件数</param>
    ''' <param name="strEigyousyoMei">営業所名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/17　李宇(大連情報システム部)　新規作成</history>
    Public Function SelEigyousyoMei(ByVal strRows As String, _
                                     ByVal strEigyousyoMei As String, _
                                     ByVal blnTorikesi As Boolean, ByVal blnAimai As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
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
            .AppendLine("     busyo_mei")                   '部署名
            .AppendLine("    ,busyo_cd")                    '部署コード
            .AppendLine("    ,CASE WHEN torikesi = '0' THEN")
            .AppendLine("         ''")
            .AppendLine("         ELSE '取消'")
            .AppendLine("         END AS Torikesi")         '取消
            .AppendLine("FROM")
            .AppendLine("   m_busyo_kanri  WITH(READCOMMITTED)") '部署管理マスタ
            .AppendLine("WHERE")
            .AppendLine("   sosiki_level = '5'")
            If strEigyousyoMei <> "" Then
                .AppendLine("   AND busyo_mei LIKE @eigyousyo_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   busyo_mei")
        End With

  
        'バラメタ
        If blnAimai Then
            paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 42, "%" & strEigyousyoMei & "%"))    '部署名
        Else
            paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 40, strEigyousyoMei))    '部署名
        End If
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                                     '取消

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtEigyousyoMei", paramList.ToArray())

        Return dsReturn.Tables("dtEigyousyoMei")

    End Function

    ''' <summary>
    ''' 検索したデータ件数を取得する
    ''' </summary>
    ''' <param name="strEigyousyoMei">営業所名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/17　李宇(大連情報システム部)　新規作成</history>
    Public Function SelEigyousyoMeiCount(ByVal strEigyousyoMei As String, _
                                         ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
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
            .AppendLine("    COUNT(busyo_mei)")
            .AppendLine("FROM")
            .AppendLine("    m_busyo_kanri  WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("     sosiki_level = '5'")
            If strEigyousyoMei <> "" Then
                .AppendLine("    AND busyo_mei LIKE @eigyousyo_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
        End With

        'バラメタ
        paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 42, "%" & strEigyousyoMei & "%")) '営業所名
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                                     '取消

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtEigyousyoMeiCount", paramList.ToArray())

        Return dsReturn.Tables("dtEigyousyoMeiCount")

    End Function
End Class
