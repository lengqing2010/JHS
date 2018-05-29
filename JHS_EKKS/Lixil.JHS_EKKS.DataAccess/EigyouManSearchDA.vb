Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text
Public Class EigyouManSearchDA
    ''' <summary>
    ''' ユーザーデータを取得する
    ''' </summary>
    ''' <param name="strRows">検索上限件数</param>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <param name="strUserName">ユーザー名</param>
    ''' <param name="blnTorikesi" >取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/15　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function SelUserInfo(ByVal strRows As String, _
                                     ByVal strUserId As String, _
                                     ByVal strUserName As String, _
                                     ByVal blnTorikesi As Boolean, ByVal blnAimai As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strUserId, _
                                                                                          strUserName, blnTorikesi)

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
            .AppendLine("     msyou.login_user_id")                                'ユーザーID
            .AppendLine("    ,mbox.DisplayName")                                   'ユーザー名
            .AppendLine("   ,CASE WHEN torikesi = '0' THEN")
            .AppendLine("         ''")
            .AppendLine("         ELSE '取消'")
            .AppendLine("         END AS Torikesi")                                '取消
            .AppendLine("FROM")
            .AppendLine("     m_jiban_ninsyou msyou  WITH(READCOMMITTED)")         '地盤認証マスタ
            .AppendLine("INNER JOIN ")
            .AppendLine("     m_jhs_mailbox  mbox WITH(READCOMMITTED)")            '社員アカウント情報マスタ
            .AppendLine("ON")
            .AppendLine("     msyou.login_user_id=mbox.PrimaryWindowsNTAccount ")
            .AppendLine("WHERE")
            .AppendLine("msyou.login_user_id  is not null")
            If strUserId <> "" Then
                .AppendLine("AND msyou.login_user_id LIKE @strUserId")
            End If
            If strUserName <> "" Then
                .AppendLine("AND mbox.DisplayName LIKE @strUserName")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   msyou.login_user_id ASC ")
        End With

        'バラメタ
        If blnAimai Then
            paramList.Add(MakeParam("@strUserId", SqlDbType.VarChar, 30, strUserId & "%"))            'ユーザーID
            paramList.Add(MakeParam("@strUserName", SqlDbType.VarChar, 130, "%" & strUserName & "%")) 'ユーザー名
        Else
            paramList.Add(MakeParam("@strUserId", SqlDbType.VarChar, 31, strUserId))            'ユーザーID
            paramList.Add(MakeParam("@strUserName", SqlDbType.VarChar, 128, strUserName)) 'ユーザー名
        End If

        
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                             '取消

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtUser", paramList.ToArray())

        Return dsReturn.Tables("dtUser")

    End Function

    ''' <summary>
    ''' 検索したデータ件数を取得する
    ''' </summary>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <param name="strUserName">ユーザー名</param>
    ''' <param name="blnTorikesi" >取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/16　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function SelUserCount(ByVal strUserId As String, _
                                          ByVal strUserName As String, _
                                          ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strUserId, _
                                                                                          strUserName, blnTorikesi)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("    COUNT(login_user_id) ")
            .AppendLine("FROM")
            .AppendLine("     m_jiban_ninsyou msyou  WITH(READCOMMITTED)")              '地盤認証マスタ
            .AppendLine("INNER JOIN ")
            .AppendLine("     m_jhs_mailbox  mbox WITH(READCOMMITTED)")            '社員アカウント情報マスタ
            .AppendLine("ON")
            .AppendLine("     msyou.login_user_id=mbox.PrimaryWindowsNTAccount ")
            .AppendLine("WHERE")
            .AppendLine("   msyou.login_user_id IS NOT NULL")
            If strUserId <> "" Then
                .AppendLine(" AND msyou.login_user_id LIKE @strUserId")
            End If
            If strUserName <> "" Then
                .AppendLine(" AND mbox.DisplayName LIKE @strUserName")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi")
            End If
        End With

        'バラメタ
        paramList.Add(MakeParam("@strUserId", SqlDbType.VarChar, 31, strUserId & "%"))    'ユーザーコード
        paramList.Add(MakeParam("@strUserName", SqlDbType.VarChar, 130, "%" & strUserName & "%")) 'ユーザー名
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '取消

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtCount", paramList.ToArray())

        Return dsReturn.Tables("dtCount")

    End Function
End Class
