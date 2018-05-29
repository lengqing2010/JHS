Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text
Public Class NinsyouDA
    ''' <summary>
    ''' 権限管理マスタよりアカウントレコードを取得します
    ''' </summary>
    ''' <param name="strLoginUserId">ログインユーザーID</param>
    ''' <returns>権限管理マスタテーブル</returns>
    ''' <remarks></remarks>
    Public Function SelUserData(ByVal strLoginUserId As String) As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLoginUserId)

        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        '戻りデータセット
        Dim dsInfo As New Data.DataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder
        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("account_no")
            .AppendLine(",account")
            .AppendLine(",torikesi")
            .AppendLine(",simei")
            .AppendLine(",syozoku_busyo")
            .AppendLine(",bikou")
            .AppendLine(",eigyou_keikaku_kenri_sansyou")
            .AppendLine(",uri_yojitu_kanri_sansyou")
            .AppendLine(",zensya_keikaku_kengen")
            .AppendLine(",sitenbetu_nen_keikaku_kengen")
            .AppendLine(",sitenbetu_getuji_keikaku_torikomi")
            .AppendLine(",sitenbetu_getuji_keikaku_kakutei")
            .AppendLine(",keikaku_minaosi_kengen")
            .AppendLine(",keikaku_kakutei_kengen")
            .AppendLine(",keikaku_torikomi_kengen")
            .AppendLine(",sitenbetu_getuji_keikaku_minaosi ")

            .AppendLine("FROM m_kengen_kanri  WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	account = @account ")
            .AppendLine("	AND torikesi = @torikesi ")
        End With
        paramList.Add(MakeParam("@account", SqlDbType.VarChar, 16, strLoginUserId))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, "0"))

        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, _
            dsInfo, "dsInfo", paramList.ToArray)



        Return dsInfo.Tables("dsInfo")

    End Function
End Class
