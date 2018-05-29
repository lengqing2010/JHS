Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text
Public Class SitenSearchDA
    ''' <summary>
    ''' 部署管理マスタのデータを取得する
    ''' </summary>
    ''' <param name="strRows">データ数</param>
    ''' <param name="strBusyoMei">部署名</param>
    ''' <param name="blnTorikesi" >取消</param>
    ''' <returns>部署管理マスタデータ</returns>
    ''' <history>2012/11/17　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function SelBusyoKanri(ByVal strRows As String, _
                                     ByVal strBusyoMei As String, _
                                     ByVal blnTorikesi As Boolean, _
                                     ByVal blnAimai As Boolean, ByVal strBusyoCD As String) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                         strBusyoMei, blnTorikesi, blnAimai)

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
            .AppendLine("   ,CASE WHEN torikesi = '0' THEN")
            .AppendLine("         ''")
            .AppendLine("         ELSE '取消'")
            .AppendLine("         END AS Torikesi")         '取消
            .AppendLine("FROM")
            .AppendLine("   m_busyo_kanri  WITH(READCOMMITTED)") '部署管理マスタ
            .AppendLine("WHERE")
            .AppendLine("   sosiki_level = '4'")
            If strBusyoMei <> "" Then
                .AppendLine("   AND busyo_mei LIKE @strBusyoMei ")
            End If
            If strBusyoCD <> "" Then
                .AppendLine("   AND busyo_cd = @strBusyoCD ")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   busyo_mei")
        End With

        'バラメタ
        If blnAimai Then
            paramList.Add(MakeParam("@strBusyoMei", SqlDbType.VarChar, 42, "%" & strBusyoMei & "%"))    '部署名
        Else
            paramList.Add(MakeParam("@strBusyoMei", SqlDbType.VarChar, 40, strBusyoMei))    '部署名
        End If
        paramList.Add(MakeParam("@strBusyoCD", SqlDbType.VarChar, 40, strBusyoCD))    '部署名
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))
        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtSitenMei", paramList.ToArray())

        Return dsReturn.Tables("dtSitenMei")

    End Function
    ''' <summary>
    '''部署管理マスタのデータ件数を取得する 
    ''' </summary>
    ''' <param name="strBusyoMei">部署名</param>
    ''' <param name="blnTorikesi" >取消</param>
    ''' <returns>部署管理マスタデータ</returns>
    ''' <history>2012/11/17　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function SelDataCount(ByVal strBusyoMei As String, ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strBusyoMei, blnTorikesi)
        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    COUNT(busyo_cd)")
            .AppendLine("FROM")
            .AppendLine("    m_busyo_kanri  WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("     sosiki_level = '4'")
            If strBusyoMei <> "" Then
                .AppendLine("    AND busyo_mei LIKE @strBusyoMei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi")
            End If
        End With

        'バラメタ
        paramList.Add(MakeParam("@strBusyoMei", SqlDbType.VarChar, 42, "%" & strBusyoMei & "%")) '支店名
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '取消

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtSitenMeiCount", paramList.ToArray())

        Return dsReturn.Tables("dtSitenMeiCount")
    End Function

End Class
