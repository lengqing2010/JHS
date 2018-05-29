Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' 系列コード検索POPUP
''' </summary>
''' <remarks></remarks>
Public Class KeiretuSearchDA

    ''' <summary>
    ''' 「系列マスタ」テープルより、系列情報を取得する
    ''' </summary>
    ''' <param name="strRows">検索上限件数</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strKeiretuMei">系列名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/15　李宇(大連情報システム部)　新規作成</history>
    Public Function SelKiretuJyouhou(ByVal strRows As String, _
                                     ByVal strKeiretuCd As String, _
                                     ByVal strKeiretuMei As String, _
                                     ByVal blnTorikesi As Boolean, ByVal blnAimai As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strKeiretuCd, _
                                                                                          strKeiretuMei, _
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
            .AppendLine("    keiretu_cd")                   '系列コード
            .AppendLine("   ,keiretu_mei")                  '系列名
            .AppendLine("   ,CASE WHEN torikesi = '0' THEN")
            .AppendLine("         ''")
            .AppendLine("         ELSE '取消'")
            .AppendLine("         END AS Torikesi")         '取消
            .AppendLine("FROM")
            .AppendLine("   m_keiretu WITH(READCOMMITTED)") '系列マスタ
            .AppendLine("WHERE")
            If strKeiretuCd <> "" Then
                .AppendLine("   keiretu_cd LIKE @keiretu_cd")
            Else
                .AppendLine("1 = 1")
            End If
            If strKeiretuMei <> "" Then
                .AppendLine("   AND keiretu_mei LIKE @keiretu_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   keiretu_cd")
        End With

        'バラメタ
        If blnAimai Then
            paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 6, strKeiretuCd & "%"))    '系列コード
            paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 42, "%" & strKeiretuMei & "%")) '系列名
        Else
            paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, strKeiretuCd))    '系列コード
            paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 40, strKeiretuMei)) '系列名
        End If
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '取消

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKiretuJyouhou", paramList.ToArray())

        Return dsReturn.Tables("dtKiretuJyouhou")

    End Function

    ''' <summary>
    ''' 検索したデータ件数を取得する
    ''' </summary>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strKeiretuMei">系列名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/16　李宇(大連情報システム部)　新規作成</history>
    Public Function SelKiretuJyouhouCount(ByVal strKeiretuCd As String, _
                                          ByVal strKeiretuMei As String, _
                                          ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strKeiretuCd, _
                                                                                          strKeiretuMei, _
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
            .AppendLine("    COUNT(keiretu_cd)")
            .AppendLine("FROM")
            .AppendLine("    m_keiretu WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            If strKeiretuCd <> "" Then
                .AppendLine("     keiretu_cd LIKE @keiretu_cd")
            Else
                .AppendLine("     1 = 1")
            End If
            If strKeiretuMei <> "" Then
                .AppendLine("    AND keiretu_mei LIKE @keiretu_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi")
            End If
        End With

        'バラメタ
        paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 6, strKeiretuCd & "%"))          '系列コード
        paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 42, "%" & strKeiretuMei & "%")) '系列名
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '取消

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKiretuJyouhouCount", paramList.ToArray())

        Return dsReturn.Tables("dtKiretuJyouhouCount")

    End Function

End Class
