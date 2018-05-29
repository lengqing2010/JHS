Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text
Public Class CommonDA
    ''' <summary>LOGの新規処理</summary>
    Public Function InsUrlLog(ByVal strUrl As String, ByVal strUserId As String, ByVal strSousa As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strUrl, strUserId, strSousa)
       

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)


        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" t_sansyou_rireki_kanri ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine("  (login_user_id,")
        commandTextSb.AppendLine("  url, ")
        commandTextSb.AppendLine("  sousa, ")
        commandTextSb.AppendLine("  add_datetime) ")
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  @login_user_id ,")
        commandTextSb.AppendLine("  @url, ")
        commandTextSb.AppendLine("  @sousa, ")
        commandTextSb.AppendLine("  GETDATE() ")

        'パラメータの設定

        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId))
        paramList.Add(MakeParam("@url", SqlDbType.VarChar, 550, strUrl))
        paramList.Add(MakeParam("@sousa", SqlDbType.VarChar, 100, strSousa))
        '更新されたデータセットを DB へ書き込み
        If ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), paramList.ToArray) > 0 Then
            '終了処理
            commandTextSb = Nothing
            Return True
        Else
            '終了処理
            commandTextSb = Nothing
            Return False
        End If

        
    End Function
    ''' <summary>システムを取得</summary>
    Public Function SelSystemDate() As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        '戻りデータセット
        Dim dsInfo As New Data.DataSet
        Dim commandTextSb As New System.Text.StringBuilder



        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" GETDATE() ")

        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, _
                    dsInfo, "dsInfo")

        Return dsInfo.Tables("dsInfo")
    End Function

#Region "業務共通"
    ''' <summary>
    ''' 計画用_名称マスタから計画年度リストを取得する
    ''' </summary>
    ''' <returns>計画年度リスト</returns>
    ''' <remarks>計画用_名称マスタから計画年度リストを取得する</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Function SelKeikakuNendoData() As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" SELECT ")
            .AppendLine("         CONVERT(VARCHAR,code) AS [code], ")
            .AppendLine("         meisyou ")
            .AppendLine(" FROM ")
            .AppendLine("         m_keikakuyou_meisyou WITH(READCOMMITTED)  ")
            .AppendLine(" WHERE ")
            .AppendLine("         meisyou_syubetu = '01' ")
            .AppendLine(" ORDER BY ")
            .AppendLine("         code ASC ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "01"))     '名称種別

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo")

        Return dsInfo.Tables(0)
    End Function
#End Region
End Class
