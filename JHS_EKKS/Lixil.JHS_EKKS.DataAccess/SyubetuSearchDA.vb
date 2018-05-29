Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' 種別検索
''' </summary>
''' <remarks></remarks>
Public Class SyubetuSearchDA

    ''' <summary>
    ''' 種別情報を検索する
    ''' </summary>
    ''' <param name="intRows">検索上限件数</param>
    ''' <param name="code">種別コード</param>
    ''' <param name="mei">種別名</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/03　李宇(大連情報システム部)　新規作成</history>
    Public Function SelSyubetu(ByVal intRows As String, _
                               ByVal code As String, _
                               ByVal mei As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          intRows, _
                                                                                          code, _
                                                                                          mei)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        With commandTextSb
            .AppendLine("SELECT          ")
            If intRows <> "max" Then
                .AppendLine("      TOP " & intRows)
            End If

            .AppendLine("   code AS cd")
            .AppendLine("   ,meisyou AS mei")
            .AppendLine("FROM            ")
            .AppendLine("   m_keikakuyou_meisyou WITH (READCOMMITTED)")

            .AppendLine("WHERE")
            .AppendLine("   meisyou_syubetu='30' ")

            If code.Trim <> "" Then
                .AppendLine(" AND code LIKE @code ")
            End If
            If mei.Trim <> "" Then
                .AppendLine(" AND meisyou LIKE @meisyou ")
            End If

            .AppendLine(" ORDER BY code ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@code", SqlDbType.VarChar, 5, code & "%"))
        paramList.Add(MakeParam("@meisyou", SqlDbType.VarChar, 82, "%" & mei & "%"))

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtSyubetuMeiCount", paramList.ToArray())

        Return dsReturn.Tables("dtSyubetuMeiCount")

    End Function

    ''' <summary>
    ''' 検索したデータ件数を取得する
    ''' </summary>
    ''' <param name="code">種別コード</param>
    ''' <param name="mei">種別名</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/03　李宇(大連情報システム部)　新規作成</history>
    Public Function SelSyubetuCount(ByVal code As String, ByVal mei As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          code, _
                                                                                          mei)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        With commandTextSb
            ' 加盟店マスト情報のSql 
            .AppendLine("SELECT          ")
            .AppendLine("   count(code) AS CNT")
            '.AppendLine("   ,meisyou AS mei")
            .AppendLine("FROM            ")
            .AppendLine("   m_keikakuyou_meisyou WITH (READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine(" meisyou_syubetu='30' ")

            If code.Trim <> "" Then
                .AppendLine(" AND code LIKE @code ")
            End If
            If mei.Trim <> "" Then
                .AppendLine(" AND meisyou LIKE @meisyou ")
            End If
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@code", SqlDbType.VarChar, 5, code & "%"))
        paramList.Add(MakeParam("@meisyou", SqlDbType.VarChar, 82, "%" & mei & "%"))

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtSyubetuMeiCount", paramList.ToArray())

        Return dsReturn.Tables("dtSyubetuMeiCount")

    End Function
End Class
