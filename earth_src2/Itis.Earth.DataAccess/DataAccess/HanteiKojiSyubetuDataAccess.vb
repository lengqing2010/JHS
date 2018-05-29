Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class HanteiKojiSyubetuDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    Public Function SelHanteiKojiSyubetuInfo(ByVal strKsSiyouNo As String, ByVal strKairyKojSyubetuNo As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("  SELECT   ")
        commandTextSb.AppendLine("    CONVERT(VARCHAR(10),m_hantei_koji_syubetu.ks_siyou_no)  ")
        commandTextSb.AppendLine("  +':'+ISNULL(m_ks_siyou.ks_siyou,'')   ")
        commandTextSb.AppendLine("  ,  CONVERT(VARCHAR(10),m_hantei_koji_syubetu.kairy_koj_syubetu_no )   ")
        commandTextSb.AppendLine("  +':'+ISNULL(m_kairy_koj_syubetu.kairy_koj_syubetu,'')   ")
        commandTextSb.AppendLine("  ,ISNULL(CONVERT(VARCHAR(19),m_hantei_koji_syubetu.upd_datetime,21),'') AS upd_datetime    ")
        commandTextSb.AppendLine("   FROM    ")
        commandTextSb.AppendLine("  m_hantei_koji_syubetu WITH (READCOMMITTED)   ")
        commandTextSb.AppendLine("  LEFT JOIN m_ks_siyou WITH (READCOMMITTED)   ")
        commandTextSb.AppendLine("  ON m_hantei_koji_syubetu.ks_siyou_no=m_ks_siyou.ks_siyou_no   ")
        commandTextSb.AppendLine("  LEFT JOIN m_kairy_koj_syubetu WITH (READCOMMITTED)   ")
        commandTextSb.AppendLine("  ON m_hantei_koji_syubetu.kairy_koj_syubetu_no=m_kairy_koj_syubetu.kairy_koj_syubetu_no      ")
        commandTextSb.AppendLine("  WHERE m_hantei_koji_syubetu.ks_siyou_no IS NOT NULL  ")


        If strKsSiyouNo.Trim = "" And strKairyKojSyubetuNo.Trim = "" Then
            commandTextSb.AppendLine(" AND CONVERT(VARCHAR(10),m_hantei_koji_syubetu.ks_siyou_no)=@ks_siyou_no  ")
            commandTextSb.AppendLine(" AND CONVERT(VARCHAR(10),m_hantei_koji_syubetu.kairy_koj_syubetu_no)=@kairy_koj_syubetu_no  ")

        Else
            If strKsSiyouNo.Trim <> "" Then
                commandTextSb.AppendLine(" AND CONVERT(VARCHAR(10),m_hantei_koji_syubetu.ks_siyou_no)=@ks_siyou_no  ")
            End If
            If strKairyKojSyubetuNo.Trim <> "" Then
                commandTextSb.AppendLine(" AND CONVERT(VARCHAR(10),m_hantei_koji_syubetu.kairy_koj_syubetu_no)=@kairy_koj_syubetu_no  ")
            End If


        End If
        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.VarChar, 10, strKsSiyouNo))
        paramList.Add(MakeParam("@kairy_koj_syubetu_no", SqlDbType.VarChar, 10, strKairyKojSyubetuNo))

        commandTextSb.AppendLine("  ORDER BY m_hantei_koji_syubetu.ks_siyou_no,m_hantei_koji_syubetu.kairy_koj_syubetu_no   ")



        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                             "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)

    End Function
    Public Function InsHanteiKojiSyubetu(ByVal strKsSiyouNo As String, ByVal strKairyKojSyubetuNo As String, _
                               ByVal strUserId As String) As Integer



        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("INSERT INTO m_hantei_koji_syubetu WITH (UPDLOCK) ")
        commandTextSb.AppendLine("(ks_siyou_no")
        commandTextSb.AppendLine(",kairy_koj_syubetu_no")
        commandTextSb.AppendLine(",add_login_user_id")
        commandTextSb.AppendLine(",add_datetime")
        commandTextSb.AppendLine(",upd_login_user_id")
        commandTextSb.AppendLine(",upd_datetime)")

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" @ks_siyou_no")
        commandTextSb.AppendLine(",@kairy_koj_syubetu_no")
        commandTextSb.AppendLine(",@add_login_user_id")
        commandTextSb.AppendLine(",GETDATE() ")
        commandTextSb.AppendLine(",@add_login_user_id")
        commandTextSb.AppendLine(",GETDATE() ")

        'パラメータの設定()
        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.VarChar, 10, strKsSiyouNo))
        paramList.Add(MakeParam("@kairy_koj_syubetu_no", SqlDbType.VarChar, 10, strKairyKojSyubetuNo))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function
    Public Function SelHaita(ByVal strKsSiyouNo As String, ByVal strKairyKojSyubetuNo As String, _
                                                            ByVal strKousin As String) As DataTable

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
        commandTextSb.AppendLine(" m_hantei_koji_syubetu  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE    ")
        commandTextSb.AppendLine(" ks_siyou_no=@ks_siyou_no  ")
        commandTextSb.AppendLine(" AND kairy_koj_syubetu_no=@kairy_koj_syubetu_no  ")
        commandTextSb.AppendLine(" AND CONVERT(varchar(19),upd_datetime,21)<>@upd_datetime  ")
        commandTextSb.AppendLine(" AND upd_datetime IS NOT NULL  ")
        'パラメータの設定

        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.VarChar, 10, strKsSiyouNo))
        paramList.Add(MakeParam("@kairy_koj_syubetu_no", SqlDbType.VarChar, 10, strKairyKojSyubetuNo))

        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousin))


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function SelJyuufuku(ByVal strKsSiyouNo As String, ByVal strKairyKojSyubetuNo As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" ISNULL(CONVERT(varchar(19),upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_hantei_koji_syubetu  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE ks_siyou_no=@ks_siyou_no  ")
        commandTextSb.AppendLine(" AND kairy_koj_syubetu_no=@kairy_koj_syubetu_no  ")

        'パラメータの設定
        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.VarChar, 10, strKsSiyouNo))
        paramList.Add(MakeParam("@kairy_koj_syubetu_no", SqlDbType.VarChar, 10, strKairyKojSyubetuNo))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function DelHanteiKojiSyubetu(ByVal strKsSiyouNo As String, ByVal strKairyKojSyubetuNo As String) As Integer

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New SyouhinDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("DELETE FROM m_hantei_koji_syubetu WITH (UPDLOCK) ")
        commandTextSb.AppendLine(" WHERE ks_siyou_no=@ks_siyou_no  ")
        commandTextSb.AppendLine(" AND kairy_koj_syubetu_no=@kairy_koj_syubetu_no  ")

        'パラメータの設定()
        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.VarChar, 10, strKsSiyouNo))
        paramList.Add(MakeParam("@kairy_koj_syubetu_no", SqlDbType.VarChar, 10, strKairyKojSyubetuNo))
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function
End Class
