Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class TantousyaMasterDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    Public Function SelSimeiInfo(ByVal strNO As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" ISNULL(simei,'')   ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_account   WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE CONVERT(VARCHAR(10),account_no)=@account_no  ")
        'パラメータの設定
        paramList.Add(MakeParam("@account_no", SqlDbType.VarChar, 10, strNO))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function SelTantouInfo(ByVal strCd As String, Optional ByVal strMei As String = "", Optional ByVal strKBN As String = "", Optional ByVal strKen As String = "") As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT   ")
        If strCd = "" And strMei = "" And strKBN = "" Then
            commandTextSb.AppendLine(" top 0 ")
        End If
        commandTextSb.AppendLine(" m_tantousya.tantousya_cd ")
        commandTextSb.AppendLine(" ,ISNULL(m_tantousya.tantousya_mei,'')  ")
        commandTextSb.AppendLine(" ,ISNULL(m_tantousya.hyouji_kbn,'')")
        commandTextSb.AppendLine(" ,ISNULL(CONVERT(varchar(19),m_tantousya.upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM m_tantousya WITH (READCOMMITTED)  ")

        If strKen = "like" Then
            commandTextSb.AppendLine(" WHERE  m_tantousya.tantousya_cd like @tantousya_cd  ")
            paramList.Add(MakeParam("@tantousya_cd", SqlDbType.VarChar, 11, strCd & "%"))
        Else
            commandTextSb.AppendLine(" WHERE  m_tantousya.tantousya_cd =@tantousya_cd  ")
            paramList.Add(MakeParam("@tantousya_cd", SqlDbType.VarChar, 10, strCd))
        End If

        If strMei.Trim <> "" Then
            If strKen = "like" Then
                commandTextSb.AppendLine(" AND  m_tantousya.tantousya_mei like @tantousya_mei  ")
                paramList.Add(MakeParam("@tantousya_mei", SqlDbType.VarChar, 31, strMei & "%"))
            Else
                commandTextSb.AppendLine(" AND  m_tantousya.tantousya_mei=@tantousya_mei  ")
                paramList.Add(MakeParam("@tantousya_mei", SqlDbType.VarChar, 30, strMei))
            End If


        End If
        If strKBN.Trim <> "" Then
            commandTextSb.AppendLine(" AND  m_tantousya.hyouji_kbn=@hyouji_kbn  ")
            paramList.Add(MakeParam("@hyouji_kbn", SqlDbType.VarChar, 10, strKBN))
        End If
        commandTextSb.AppendLine(" ORDER BY  m_tantousya.tantousya_cd ")



        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                         "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)

    End Function

    Public Function SelHaita(ByVal strCd As String, ByVal strKousin As String) As DataTable

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
        commandTextSb.AppendLine(" m_tantousya  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  CONVERT(VARCHAR(10),tantousya_cd)=@tantousya_cd  ")
        commandTextSb.AppendLine(" AND CONVERT(varchar(19),upd_datetime,21)<>@upd_datetime  ")
        commandTextSb.AppendLine(" AND upd_datetime IS NOT NULL  ")

        'パラメータの設定
        paramList.Add(MakeParam("@tantousya_cd", SqlDbType.VarChar, 10, strCd))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousin))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function
    Public Function SelJyuufuku(ByVal strCd As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" tantousya_cd ,ISNULL(CONVERT(VARCHAR(19),upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_tantousya  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  CONVERT(VARCHAR(10),tantousya_cd)=@tantousya_cd  ")


        'パラメータの設定
        paramList.Add(MakeParam("@tantousya_cd", SqlDbType.VarChar, 10, strCd))


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function InsTantousya(ByVal strCd As String, ByVal strMei As String, ByVal strKBN As String, ByVal strUserId As String) As Integer

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New SyouhinDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("INSERT INTO m_tantousya WITH (UPDLOCK) ")
        commandTextSb.AppendLine("(tantousya_cd")
        commandTextSb.AppendLine(",tantousya_mei")
        commandTextSb.AppendLine(",hyouji_kbn")
        commandTextSb.AppendLine(",add_login_user_id")
        commandTextSb.AppendLine(",add_datetime)")
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" @tantousya_cd")
        commandTextSb.AppendLine(",@tantousya_mei")
        commandTextSb.AppendLine(",@hyouji_kbn")
        commandTextSb.AppendLine(",@add_login_user_id")
        commandTextSb.AppendLine(",GETDATE() ")

        'パラメータの設定()
        paramList.Add(MakeParam("@tantousya_cd", SqlDbType.VarChar, 10, strCd))
        paramList.Add(MakeParam("@tantousya_mei", SqlDbType.VarChar, 30, strMei))
        paramList.Add(MakeParam("@hyouji_kbn", SqlDbType.VarChar, 10, strKBN))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function
    Public Function UpdTantou(ByVal strCd As String, ByVal strMei As String, ByVal strKBN As String, ByVal strUserId As String) As Integer



        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" UPDATE m_tantousya  WITH (UPDLOCK)    ")
        commandTextSb.AppendLine(" SET tantousya_cd=@tantousya_cd ")
        commandTextSb.AppendLine(" ,tantousya_mei=@tantousya_mei ")
        commandTextSb.AppendLine(" ,hyouji_kbn=@hyouji_kbn ")
        commandTextSb.AppendLine(" ,upd_login_user_id=@login_user_id")
        commandTextSb.AppendLine(" ,upd_datetime=GETDATE()")
        commandTextSb.AppendLine(" WHERE tantousya_cd=@tantousya_cd")

        'パラメータの設定()
        paramList.Add(MakeParam("@tantousya_cd", SqlDbType.VarChar, 10, strCd))
        paramList.Add(MakeParam("@tantousya_mei", SqlDbType.VarChar, 30, strMei))
        paramList.Add(MakeParam("@hyouji_kbn", SqlDbType.VarChar, 10, strKBN))


        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId))
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)
    End Function

    Public Function DelTantou(ByVal strCd As String) As Integer



        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" DELETE FROM m_tantousya  WITH (UPDLOCK)    ")
      
        commandTextSb.AppendLine(" WHERE tantousya_cd=@tantousya_cd")

        'パラメータの設定()
        paramList.Add(MakeParam("@tantousya_cd", SqlDbType.VarChar, 10, strCd))
       
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)



    End Function
End Class
