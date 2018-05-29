Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class KeiretuMasterDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    Public Function SelKeiretuInfo(ByVal strKbn As String, ByVal strTorikesi As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" CASE   ")
        commandTextSb.AppendLine(" WHEN kbn.kbn_mei IS  null THEN kei.kbn  ")
        commandTextSb.AppendLine(" ELSE  ")
        commandTextSb.AppendLine(" kei.kbn+':'+kbn.kbn_mei  ")
        commandTextSb.AppendLine(" END AS kbn_mei ")
        commandTextSb.AppendLine(" ,kei.keiretu_cd  ")
        commandTextSb.AppendLine(" ,kei.keiretu_mei  ")
        commandTextSb.AppendLine(" ,kei.torikesi  ")
        commandTextSb.AppendLine(" ,ISNULL(CONVERT(varchar(19),kei.upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_keiretu AS kei  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" LEFT JOIN m_data_kbn AS kbn  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" ON kei.kbn=kbn.kbn  ")
        commandTextSb.AppendLine(" WHERE  kei.kbn=@kbn  ")
        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))

        If strTorikesi <> "" Then
            commandTextSb.AppendLine(" AND kei.torikesi=@torikesi  ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, strTorikesi))
        End If





        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)


    End Function


    Public Function SelJyuufuku(ByVal strKbn As String, ByVal strKeiretuCd As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" kbn ,ISNULL(CONVERT(varchar(19),kei.upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_keiretu AS kei  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  kei.kbn=@kbn  ")
        commandTextSb.AppendLine(" AND kei.keiretu_cd=@keiretu_cd  ")

        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, strKeiretuCd))


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function SelHaita(ByVal strKbn As String, ByVal strKeiretuCd As String, ByVal strKousin As String) As DataTable

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
        commandTextSb.AppendLine(" m_keiretu  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  kbn=@kbn  ")
        commandTextSb.AppendLine(" AND keiretu_cd=@keiretu_cd  ")
        commandTextSb.AppendLine(" AND CONVERT(varchar(19),upd_datetime,21)<>@upd_datetime  ")
        commandTextSb.AppendLine(" AND upd_datetime IS NOT NULL  ")
        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, strKeiretuCd))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousin))


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function InsKeiretu(ByVal strKbn As String, _
                                ByVal strTorikesi As String, _
                                ByVal strKeiretuCd As String, _
                                ByVal strKeiretuMei As String, _
                                ByVal strUserId As String) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" INSERT INTO m_keiretu  WITH (UPDLOCK)    ")
        commandTextSb.AppendLine(" (kbn ")
        commandTextSb.AppendLine(" ,torikesi ")
        commandTextSb.AppendLine(" ,keiretu_cd ")
        commandTextSb.AppendLine(" ,keiretu_mei ")
        commandTextSb.AppendLine(" ,add_login_user_id ")
        commandTextSb.AppendLine(" ,add_datetime)")
        commandTextSb.AppendLine(" VALUES  ")
        commandTextSb.AppendLine(" (@kbn ")
        commandTextSb.AppendLine(" ,@torikesi ")
        commandTextSb.AppendLine(" ,@keiretu_cd ")
        commandTextSb.AppendLine(" ,@keiretu_mei ")
        commandTextSb.AppendLine(" ,@add_login_user_id ")
        commandTextSb.AppendLine(" ,GETDATE())")
        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, strTorikesi))
        paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, strKeiretuCd))
        paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 40, strKeiretuMei))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))

        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)



    End Function

    Public Function UpdKeiretu(ByVal strKbn As String, _
                                ByVal strTorikesi As String, _
                                ByVal strKeiretuCd As String, _
                                ByVal strKeiretuMei As String, _
                                ByVal strUserId As String) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" UPDATE m_keiretu  WITH (UPDLOCK)    ")
        commandTextSb.AppendLine(" SET keiretu_mei=@keiretu_mei ")
        commandTextSb.AppendLine(" ,torikesi=@torikesi")
        commandTextSb.AppendLine(" ,upd_login_user_id=@login_user_id")
        commandTextSb.AppendLine(" ,upd_datetime=GETDATE()")
        commandTextSb.AppendLine(" WHERE  kbn=@kbn  ")
        commandTextSb.AppendLine(" AND keiretu_cd=@keiretu_cd  ")
        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, strTorikesi))
        paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, strKeiretuCd))
        paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 40, strKeiretuMei))
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId))

        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)



    End Function
End Class
