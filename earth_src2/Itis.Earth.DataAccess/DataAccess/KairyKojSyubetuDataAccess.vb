Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class KairyKojSyubetuDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    Public Function SelKojSyubetuInfo(ByVal strSyubetuNo As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ")

        commandTextSb.AppendLine(" kairy_koj_syubetu_no AS code ")
        commandTextSb.AppendLine(" ,kairy_koj_syubetu AS meisyou  ")
        commandTextSb.AppendLine(" ,ISNULL(CONVERT(VARCHAR(19),upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM m_kairy_koj_syubetu WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE kairy_koj_syubetu_no IS NOT NULL  ")
        If strSyubetuNo.Trim = "" Then
            'commandTextSb.AppendLine(" AND kairy_koj_syubetu_no IS NULL  ")
        Else

            commandTextSb.AppendLine(" AND kairy_koj_syubetu_no=@kairy_koj_syubetu_no  ")
            paramList.Add(MakeParam("@kairy_koj_syubetu_no", SqlDbType.VarChar, 10, strSyubetuNo))
        End If
        commandTextSb.AppendLine(" ORDER BY kairy_koj_syubetu_no")
        'パラメータの設定

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                         "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)

    End Function


    Public Function InsKojSyubetu(ByVal strSyubetuNo As String, _
                               ByVal strSyubetu As String, _
                               ByVal strUserId As String) As Integer



        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("INSERT INTO m_kairy_koj_syubetu WITH (UPDLOCK) ")
        commandTextSb.AppendLine("(kairy_koj_syubetu_no")
        commandTextSb.AppendLine(",kairy_koj_syubetu")
        commandTextSb.AppendLine(",add_login_user_id")
        commandTextSb.AppendLine(",add_datetime)")

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" @kairy_koj_syubetu_no")
        commandTextSb.AppendLine(",@kairy_koj_syubetu")
        commandTextSb.AppendLine(",@add_login_user_id")
        commandTextSb.AppendLine(",GETDATE() ")


        'パラメータの設定()
        paramList.Add(MakeParam("@kairy_koj_syubetu_no", SqlDbType.VarChar, 10, strSyubetuNo))
        paramList.Add(MakeParam("@kairy_koj_syubetu", SqlDbType.VarChar, 20, strSyubetu))


        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function
    Public Function SelHaita(ByVal strSyubetuNo As String, ByVal strKousin As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" upd_login_user_id  ")
        commandTextSb.AppendLine(" ,upd_datetime  ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_kairy_koj_syubetu  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  kairy_koj_syubetu_no=@kairy_koj_syubetu_no  ")
        commandTextSb.AppendLine(" AND CONVERT(varchar(19),upd_datetime,21)<>@upd_datetime  ")
        commandTextSb.AppendLine(" AND upd_datetime IS NOT NULL  ")
        'パラメータの設定
        paramList.Add(MakeParam("@kairy_koj_syubetu_no", SqlDbType.VarChar, 10, strSyubetuNo))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousin))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function UpdKojSyubetu(ByVal strSyubetuNo As String, _
                               ByVal strSyubetu As String, _
                               ByVal strUserId As String) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" UPDATE m_kairy_koj_syubetu  WITH (UPDLOCK)    ")
        commandTextSb.AppendLine(" SET kairy_koj_syubetu_no=@kairy_koj_syubetu_no ")
        commandTextSb.AppendLine(" ,kairy_koj_syubetu=@kairy_koj_syubetu ")
        commandTextSb.AppendLine(" ,upd_login_user_id=@add_login_user_id")
        commandTextSb.AppendLine(" ,upd_datetime=GETDATE()")
        commandTextSb.AppendLine(" WHERE  kairy_koj_syubetu_no=@kairy_koj_syubetu_no  ")

        'パラメータの設定()
        paramList.Add(MakeParam("@kairy_koj_syubetu_no", SqlDbType.VarChar, 10, strSyubetuNo))
        paramList.Add(MakeParam("@kairy_koj_syubetu", SqlDbType.VarChar, 20, strSyubetu))

        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)



    End Function

    Public Function DelKojSyubetu(ByVal strSyubetuNo As String) As Integer

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New SyouhinDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("DELETE FROM m_kairy_koj_syubetu WITH (UPDLOCK) ")
        commandTextSb.AppendLine(" WHERE  kairy_koj_syubetu_no=@kairy_koj_syubetu_no  ")


        'パラメータの設定
         paramList.Add(MakeParam("@kairy_koj_syubetu_no", SqlDbType.VarChar, 10, strSyubetuNo))
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function
End Class
