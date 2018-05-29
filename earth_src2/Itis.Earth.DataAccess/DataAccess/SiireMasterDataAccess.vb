Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class SiireMasterDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    Public Function SelSiireInfo(ByVal strKaisya As String, ByVal strKameitenCd As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" ISNULL(m_siire_kakaku.tys_kaisya_cd,'')+'-'+ISNULL(m_siire_kakaku.jigyousyo_cd,'')")
        commandTextSb.AppendLine("  +','+ISNULL(m_tyousakaisya.tys_kaisya_mei,'')")
        commandTextSb.AppendLine("  ,ISNULL(m_siire_kakaku.kameiten_cd,'') ")
        commandTextSb.AppendLine("  +','+ISNULL(m_kameiten.kameiten_mei1,'') ")
        commandTextSb.AppendLine("  ,m_siire_kakaku.siire_kkk1")
        commandTextSb.AppendLine("  ,m_siire_kakaku.siire_kkk2")
        commandTextSb.AppendLine("  ,m_siire_kakaku.siire_kkk3")
        commandTextSb.AppendLine("  ,m_siire_kakaku.oboegaki_umu")
        commandTextSb.AppendLine(" ,ISNULL(CONVERT(varchar(19),m_siire_kakaku.upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM m_siire_kakaku WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" LEFT JOIN m_tyousakaisya WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" ON m_siire_kakaku.tys_kaisya_cd=m_tyousakaisya.tys_kaisya_cd  ")
        commandTextSb.AppendLine(" AND m_siire_kakaku.jigyousyo_cd=m_tyousakaisya.jigyousyo_cd  ")
        commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" ON m_siire_kakaku.kameiten_cd=m_kameiten.kameiten_cd  ")

        commandTextSb.AppendLine(" WHERE  m_siire_kakaku.tys_kaisya_cd IS NOT NULL  ")

        If strKaisya.Trim = "" And strKameitenCd.Trim = "" Then
            commandTextSb.AppendLine(" AND  m_siire_kakaku.tys_kaisya_cd+m_siire_kakaku.jigyousyo_cd=@Kaisya  ")
            commandTextSb.AppendLine(" AND  m_siire_kakaku.kameiten_cd=@Kameiten ")
        Else
            If strKameitenCd.Trim <> "" Then
                commandTextSb.AppendLine(" AND  m_siire_kakaku.kameiten_cd=@Kameiten ")
            End If
            If strKaisya.Trim <> "" Then
                commandTextSb.AppendLine(" AND  m_siire_kakaku.tys_kaisya_cd+m_siire_kakaku.jigyousyo_cd=@Kaisya  ")
            End If
        End If

        commandTextSb.AppendLine(" ORDER BY  m_siire_kakaku.tys_kaisya_cd+m_siire_kakaku.jigyousyo_cd,m_siire_kakaku.kameiten_cd ")
        'パラメータの設定
        paramList.Add(MakeParam("@Kaisya", SqlDbType.VarChar, 7, strKaisya))
        paramList.Add(MakeParam("@Kameiten", SqlDbType.VarChar, 5, strKameitenCd))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                         "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)

    End Function
    Public Function SelSiireInfo2(ByVal strKaisya As String, ByVal strKameitenCd As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" ISNULL(m_siire_kakaku.tys_kaisya_cd,'')+'-'+ISNULL(m_siire_kakaku.jigyousyo_cd,'') as jigyousyo_cd")
        commandTextSb.AppendLine("  ,ISNULL(m_tyousakaisya.tys_kaisya_mei,'') as tys_kaisya_mei")
        commandTextSb.AppendLine("  ,ISNULL(m_siire_kakaku.kameiten_cd,'') as kameiten_cd ")
        commandTextSb.AppendLine("  ,ISNULL(m_kameiten.kameiten_mei1,'')  as kameiten_mei1")
        commandTextSb.AppendLine("  ,m_siire_kakaku.siire_kkk1")
        commandTextSb.AppendLine("  ,m_siire_kakaku.siire_kkk2")
        commandTextSb.AppendLine("  ,m_siire_kakaku.siire_kkk3")
        commandTextSb.AppendLine(" ,ISNULL(CONVERT(varchar(19),m_siire_kakaku.upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM m_siire_kakaku WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" LEFT JOIN m_tyousakaisya WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" ON m_siire_kakaku.tys_kaisya_cd=m_tyousakaisya.tys_kaisya_cd  ")
        commandTextSb.AppendLine(" AND m_siire_kakaku.jigyousyo_cd=m_tyousakaisya.jigyousyo_cd  ")
        commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" ON m_siire_kakaku.kameiten_cd=m_kameiten.kameiten_cd  ")

        commandTextSb.AppendLine(" WHERE  m_siire_kakaku.tys_kaisya_cd IS NOT NULL  ")

        If strKaisya.Trim = "" And strKameitenCd.Trim = "" Then
            commandTextSb.AppendLine(" AND  m_siire_kakaku.tys_kaisya_cd+m_siire_kakaku.jigyousyo_cd=@Kaisya  ")
            commandTextSb.AppendLine(" AND  m_siire_kakaku.kameiten_cd=@Kameiten ")
        Else
            If strKameitenCd.Trim <> "" Then
                commandTextSb.AppendLine(" AND  m_siire_kakaku.kameiten_cd=@Kameiten ")
            End If
            If strKaisya.Trim <> "" Then
                commandTextSb.AppendLine(" AND  m_siire_kakaku.tys_kaisya_cd+m_siire_kakaku.jigyousyo_cd=@Kaisya  ")
            End If
        End If

        commandTextSb.AppendLine(" ORDER BY  m_siire_kakaku.tys_kaisya_cd+m_siire_kakaku.jigyousyo_cd,m_siire_kakaku.kameiten_cd ")
        'パラメータの設定
        paramList.Add(MakeParam("@Kaisya", SqlDbType.VarChar, 7, strKaisya))
        paramList.Add(MakeParam("@Kameiten", SqlDbType.VarChar, 5, strKameitenCd))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                         "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)

    End Function
    Public Function SelJyuufuku(ByVal strKaisya As String, ByVal strKameitenCd As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" kameiten_cd ,ISNULL(CONVERT(varchar(19),upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_siire_kakaku  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  kameiten_cd=@KameitenCd  ")
        commandTextSb.AppendLine(" AND tys_kaisya_cd+jigyousyo_cd=@Kaisya  ")

        'パラメータの設定
        paramList.Add(MakeParam("@KameitenCd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@Kaisya", SqlDbType.VarChar, 7, strKaisya))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    ''' <summary>調査会社データを取得する</summary>
    Public Function SelTyousaInfo(ByVal strCd As String, _
                                  ByVal blnDelete As Boolean) As DataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("      tys_kaisya_cd, ")
        commandTextSb.AppendLine("      jigyousyo_cd, ")
        commandTextSb.AppendLine("      tys_kaisya_mei ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_tyousakaisya  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE tys_kaisya_cd IS NOT NULL ")
        If blnDelete = True Then
            commandTextSb.Append(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        End If


        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND tys_kaisya_cd+jigyousyo_cd = @strCd ")
        End If

        'パラメータの設定
        paramList.Add(MakeParam("@strCd", SqlDbType.VarChar, 7, strCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.tyousakaisyaTable.TableName, paramList.ToArray)

        Return dsCommonSearch.tyousakaisyaTable
    End Function
    Public Function SelHaita(ByVal strKaisya As String, ByVal strKameitenCd As String, ByVal strKousin As String) As DataTable

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
        commandTextSb.AppendLine(" m_siire_kakaku  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE    ")
        commandTextSb.AppendLine(" tys_kaisya_cd+jigyousyo_cd=@strKaisya  ")
        commandTextSb.AppendLine(" AND kameiten_cd=@KameitenCd  ")
        commandTextSb.AppendLine(" AND CONVERT(varchar(19),upd_datetime,21)<>@upd_datetime  ")
        commandTextSb.AppendLine(" AND upd_datetime IS NOT NULL  ")
        'パラメータの設定

        paramList.Add(MakeParam("@strKaisya", SqlDbType.VarChar, 7, strKaisya))
        paramList.Add(MakeParam("@KameitenCd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousin))


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function DelSyouhin(ByVal strKaisya As String, ByVal strKameitenCd As String) As Integer

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New SyouhinDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("DELETE FROM m_siire_kakaku WITH (UPDLOCK) ")
        commandTextSb.AppendLine(" WHERE tys_kaisya_cd+jigyousyo_cd=@strKaisya")
        commandTextSb.AppendLine(" AND kameiten_cd=@KameitenCd")


        'パラメータの設定()
        paramList.Add(MakeParam("@strKaisya", SqlDbType.VarChar, 7, strKaisya))
        paramList.Add(MakeParam("@KameitenCd", SqlDbType.VarChar, 5, strKameitenCd))

        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function

    Public Function InsSiire(ByVal strKaisya As String, _
                            ByVal strJigyousyo As String, _
                            ByVal strKameiten As String, _
                            ByVal strk1 As String, _
                            ByVal strk2 As String, _
                            ByVal strk3 As String, _
                            ByVal strUmu As String, _
                            ByVal strUserId As String) As Integer

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New SyouhinDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("INSERT INTO m_siire_kakaku WITH (UPDLOCK) ")
        commandTextSb.AppendLine("(tys_kaisya_cd")
        commandTextSb.AppendLine(",jigyousyo_cd")
        commandTextSb.AppendLine(",kameiten_cd")
        commandTextSb.AppendLine(",siire_kkk1")
        commandTextSb.AppendLine(",siire_kkk2")
        commandTextSb.AppendLine(",siire_kkk3")
        commandTextSb.AppendLine(",oboegaki_umu")
        commandTextSb.AppendLine(",add_login_user_id")
        commandTextSb.AppendLine(",add_datetime)")

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" @tys_kaisya_cd")
        commandTextSb.AppendLine(",@jigyousyo_cd")
        commandTextSb.AppendLine(",@kameiten_cd")
        commandTextSb.AppendLine(",@siire_kkk1")
        commandTextSb.AppendLine(",@siire_kkk2")
        commandTextSb.AppendLine(",@siire_kkk3")
        commandTextSb.AppendLine(",@strUmu")
        commandTextSb.AppendLine(",@add_login_user_id")
        commandTextSb.AppendLine(",GETDATE() ")


        'パラメータの設定()
        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strKaisya))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyo))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameiten))
        If strUmu = "" Then
            paramList.Add(MakeParam("@strUmu", SqlDbType.VarChar, 10, DBNull.Value))
        Else
            paramList.Add(MakeParam("@strUmu", SqlDbType.VarChar, 10, strUmu))
        End If
        If strk1 = "" Then
            paramList.Add(MakeParam("@siire_kkk1", SqlDbType.VarChar, 10, DBNull.Value))
        Else
            paramList.Add(MakeParam("@siire_kkk1", SqlDbType.VarChar, 10, strk1))
        End If
        If strk2 = "" Then
            paramList.Add(MakeParam("@siire_kkk2", SqlDbType.VarChar, 10, DBNull.Value))
        Else
            paramList.Add(MakeParam("@siire_kkk2", SqlDbType.VarChar, 10, strk2))
        End If
        If strk3 = "" Then
            paramList.Add(MakeParam("@siire_kkk3", SqlDbType.VarChar, 10, DBNull.Value))
        Else
            paramList.Add(MakeParam("@siire_kkk3", SqlDbType.VarChar, 10, strk3))
        End If
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function


    Public Function UpdSiire(ByVal strKaisya As String, _
                            ByVal strJigyousyo As String, _
                            ByVal strKameiten As String, _
                            ByVal strk1 As String, _
                            ByVal strk2 As String, _
                            ByVal strk3 As String, _
                            ByVal strUmu As String, _
                            ByVal strUserId As String) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" UPDATE m_siire_kakaku  WITH (UPDLOCK)    ")
        commandTextSb.AppendLine(" SET tys_kaisya_cd=@strKaisya ")
        commandTextSb.AppendLine(" ,jigyousyo_cd=@jigyousyo_cd ")
        commandTextSb.AppendLine(" ,kameiten_cd=@KameitenCd ")
        commandTextSb.AppendLine(" ,siire_kkk1=@siire_kkk1 ")
        commandTextSb.AppendLine(" ,siire_kkk2=@siire_kkk2 ")
        commandTextSb.AppendLine(" ,siire_kkk3=@siire_kkk3 ")
        commandTextSb.AppendLine(" ,oboegaki_umu=@strUmu ")
        commandTextSb.AppendLine(" ,upd_login_user_id=@login_user_id")
        commandTextSb.AppendLine(" ,upd_datetime=GETDATE()")
        commandTextSb.AppendLine(" WHERE tys_kaisya_cd+jigyousyo_cd=@strKaisya+@jigyousyo_cd")
        commandTextSb.AppendLine(" AND kameiten_cd=@KameitenCd")
        'パラメータの設定()
        paramList.Add(MakeParam("@strKaisya", SqlDbType.VarChar, 5, strKaisya))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyo))
        paramList.Add(MakeParam("@KameitenCd", SqlDbType.VarChar, 5, strKameiten))
        If strk1 = "" Then
            paramList.Add(MakeParam("@siire_kkk1", SqlDbType.VarChar, 10, DBNull.Value))
        Else
            paramList.Add(MakeParam("@siire_kkk1", SqlDbType.VarChar, 10, strk1))
        End If
        If strk2 = "" Then
            paramList.Add(MakeParam("@siire_kkk2", SqlDbType.VarChar, 10, DBNull.Value))
        Else
            paramList.Add(MakeParam("@siire_kkk2", SqlDbType.VarChar, 10, strk2))
        End If
        If strk3 = "" Then
            paramList.Add(MakeParam("@siire_kkk3", SqlDbType.VarChar, 10, DBNull.Value))
        Else
            paramList.Add(MakeParam("@siire_kkk3", SqlDbType.VarChar, 10, strk3))
        End If
        If strUmu = "" Then
            paramList.Add(MakeParam("@strUmu", SqlDbType.VarChar, 10, DBNull.Value))
        Else
            paramList.Add(MakeParam("@strUmu", SqlDbType.VarChar, 10, strUmu))
        End If
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId))
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)



    End Function
End Class
