Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' 登録事業者POPUP
''' </summary>
''' <remarks></remarks>
Public Class TourokuJigyousyaSearchDA

    ''' <summary>
    ''' 「加盟店コード」、「加盟店名」、「都道府県名」と「加盟店カナ名」の検索処理する
    ''' </summary>
    ''' <param name="strRows">検索上限件数</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKameitenMei">加盟店名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Public Function SelTourokuJigyousya(ByVal strRows As String, _
                                        ByVal strKameitenCd As String, _
                                        ByVal strKameitenMei As String, _
                                        ByVal blnTorikesi As Boolean, ByVal blnAimai As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
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
            .AppendLine("    MK.kameiten_cd")                   '加盟店コード
            .AppendLine("   ,MK.kameiten_mei1")                 '加盟店名
            .AppendLine("   ,MT.todouhuken_mei")
            .AppendLine("   ,MK.tenmei_kana1")
            .AppendLine("   ,CASE WHEN MK.torikesi = '0' THEN")
            .AppendLine("         ''")
            .AppendLine("         ELSE '取消'")
            .AppendLine("         END AS Torikesi")         '取消
            .AppendLine("FROM")
            .AppendLine("   m_kameiten AS MK WITH(READCOMMITTED)")  '加盟店マスタ
            .AppendLine("   LEFT JOIN")
            .AppendLine("       m_todoufuken AS MT WITH(READCOMMITTED)") '都道府県マスタ
            .AppendLine("       ON MK.todouhuken_cd = MT.todouhuken_cd")
            .AppendLine("WHERE")
            If strKameitenCd <> "" Then
                .AppendLine("   MK.kameiten_cd LIKE @kameiten_cd")
            Else
                .AppendLine("   1 = 1")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("   AND (MK.kameiten_mei1 LIKE @kameiten_mei")
                .AppendLine("   OR MK.kameiten_mei2 LIKE @kameiten_mei)")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND MK.torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   MK.kameiten_cd")
        End With

        'バラメタ
        If blnAimai Then
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd & "%")) '加盟店コード
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, "%" & strKameitenMei & "%")) '加盟店名
        Else
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 4, strKameitenCd)) '加盟店コード
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 38, strKameitenMei)) '加盟店名
        End If
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '取消

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTourokuJigyousya", paramList.ToArray())

        Return dsReturn.Tables("dtTourokuJigyousya")

    End Function

    ''' <summary>
    ''' 検索したデータ件数を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKameitenMei">加盟店名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Public Function SelTourokuJigyousyaCount(ByVal strKameitenCd As String, _
                                             ByVal strKameitenMei As String, _
                                             ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
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
            .AppendLine("    COUNT(kameiten_cd)")
            .AppendLine("FROM")
            .AppendLine("    m_kameiten WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            If strKameitenCd <> "" Then
                .AppendLine("   kameiten_cd LIKE @kameiten_cd")
            Else
                .AppendLine("   1 = 1")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("   AND (kameiten_mei1 LIKE @kameiten_mei")
                .AppendLine("   OR kameiten_mei2 LIKE @kameiten_mei)")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
        End With

        'バラメタ
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 6, strKameitenCd & "%")) '加盟店コード
        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 42, "%" & strKameitenMei & "%")) '加盟店名
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '取消

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTourokuJigyousyaCount", paramList.ToArray())

        Return dsReturn.Tables("dtTourokuJigyousyaCount")

    End Function

End Class
