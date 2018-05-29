Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class Syouhin2MasterDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    Public Function SelJyuufuku(ByVal strKameitenCd As String, ByVal strSyouhinCd As String) As DataTable

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
        commandTextSb.AppendLine(" m_tokuteiten_syouhin2_settei  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  kameiten_cd=@KameitenCd  ")
        commandTextSb.AppendLine(" AND syouhin_cd=@SyouhinCd  ")

        'パラメータの設定
        paramList.Add(MakeParam("@KameitenCd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@SyouhinCd", SqlDbType.VarChar, 8, strSyouhinCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function
    Public Function SelSyouhinInfo(ByVal strKameitenCd As String, ByVal strSyouhinCd As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" m_tokuteiten_syouhin2_settei.kameiten_cd ")
        commandTextSb.AppendLine(" +','+ISNULL(m_kameiten.kameiten_mei1,'') AS kameiten_cd ")
        commandTextSb.AppendLine(" ,m_tokuteiten_syouhin2_settei.syouhin_cd ")
        commandTextSb.AppendLine(" +','+ISNULL(m_syouhin.syouhin_mei,'') ")
        commandTextSb.AppendLine(" ,ISNULL(CONVERT(varchar(19),m_tokuteiten_syouhin2_settei.upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM m_tokuteiten_syouhin2_settei WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" ON m_tokuteiten_syouhin2_settei.kameiten_cd=m_kameiten.kameiten_cd  ")
        commandTextSb.AppendLine(" LEFT JOIN m_syouhin WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" ON m_tokuteiten_syouhin2_settei.syouhin_cd=m_syouhin.syouhin_cd  ")

        commandTextSb.AppendLine(" WHERE  m_tokuteiten_syouhin2_settei.kameiten_cd IS NOT NULL  ")

        If strKameitenCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND  m_tokuteiten_syouhin2_settei.kameiten_cd=@KameitenCd  ")

        End If
        If strSyouhinCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND  m_tokuteiten_syouhin2_settei.syouhin_cd=@SyouhinCd  ")
        End If
        commandTextSb.AppendLine(" ORDER BY  m_tokuteiten_syouhin2_settei.kameiten_cd,m_tokuteiten_syouhin2_settei.syouhin_cd ")
        'パラメータの設定
        paramList.Add(MakeParam("@KameitenCd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@SyouhinCd", SqlDbType.VarChar, 8, strSyouhinCd))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                         "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)

    End Function

    Public Function InsSyouhin(ByVal strKameitenCd As String, ByVal strSyouhinCd As String, ByVal strUserId As String) As Integer

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New SyouhinDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("INSERT INTO m_tokuteiten_syouhin2_settei WITH (UPDLOCK) ")
        commandTextSb.AppendLine("(kameiten_cd")
        commandTextSb.AppendLine(",syouhin_cd")
        commandTextSb.AppendLine(",add_login_user_id")
        commandTextSb.AppendLine(",add_datetime")
        commandTextSb.AppendLine(",upd_login_user_id")
        commandTextSb.AppendLine(",upd_datetime)")
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" @KameitenCd")
        commandTextSb.AppendLine(",@SyouhinCd")

        commandTextSb.AppendLine(",@add_login_user_id")
        commandTextSb.AppendLine(",GETDATE() ")
        commandTextSb.AppendLine(",@add_login_user_id")
        commandTextSb.AppendLine(",GETDATE() ")

        'パラメータの設定()
        paramList.Add(MakeParam("@KameitenCd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@SyouhinCd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function

    Public Function DelSyouhin(ByVal strKameitenCd As String, ByVal strSyouhinCd As String) As Integer

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New SyouhinDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("DELETE FROM m_tokuteiten_syouhin2_settei WITH (UPDLOCK) ")
        commandTextSb.AppendLine(" WHERE kameiten_cd=@KameitenCd")
        commandTextSb.AppendLine(" AND syouhin_cd=@SyouhinCd")


        'パラメータの設定()
        paramList.Add(MakeParam("@KameitenCd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@SyouhinCd", SqlDbType.VarChar, 8, strSyouhinCd))
        'paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function

    Public Function SelHaita(ByVal strKameitenCd As String, ByVal strSyouhinCd As String, ByVal strKousin As String) As DataTable

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
        commandTextSb.AppendLine(" m_tokuteiten_syouhin2_settei  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE    ")
        commandTextSb.AppendLine(" kameiten_cd=@KameitenCd  ")
        commandTextSb.AppendLine(" AND syouhin_cd=@SyouhinCd  ")
        commandTextSb.AppendLine(" AND CONVERT(varchar(19),upd_datetime,21)<>@upd_datetime  ")
        commandTextSb.AppendLine(" AND upd_datetime IS NOT NULL  ")
        'パラメータの設定

        paramList.Add(MakeParam("@KameitenCd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@SyouhinCd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousin))


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    ''' <summary>商品データを取得する</summary>
    Public Function SelSyouhin(ByVal strSyouhinCd As String, _
                                         ByVal strTorikesi As String, _
                                         ByVal soukoCd As String) As CommonSearchDataSet.SyouhinTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim intCnt As Integer = 0
        'SQL文
        commandTextSb.AppendLine(" SELECT ")

        commandTextSb.AppendLine("      syouhin.syouhin_cd, ")
        commandTextSb.AppendLine("      syouhin.syouhin_mei ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("		(SELECT syouhin_cd , ")
        commandTextSb.AppendLine("			syouhin_mei,torikesi  ")
        commandTextSb.AppendLine("		FROM ")
        commandTextSb.AppendLine("			m_syouhin WITH(READCOMMITTED)")
        commandTextSb.AppendLine("		WHERE ")
        For intCnt = 0 To Split(soukoCd, ",").Length - 1
            If intCnt = 0 Then
                commandTextSb.AppendLine("	souko_cd=@souko_cd" & intCnt)
                paramList.Add(MakeParam("@souko_cd" & intCnt, SqlDbType.VarChar, 3, Split(soukoCd, ",")(0)))
            Else
                commandTextSb.AppendLine(" OR	souko_cd=@souko_cd" & intCnt)
                paramList.Add(MakeParam("@souko_cd" & intCnt, SqlDbType.VarChar, 3, Split(soukoCd, ",")(intCnt)))

            End If
        Next
        commandTextSb.AppendLine("		) AS syouhin")
        commandTextSb.AppendLine(" WHERE syouhin.syouhin_cd IS NOT NULL ")


        If strSyouhinCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND syouhin.syouhin_cd = @SyouhinCd ")
        End If
        'If strSyouhinMei.Trim <> "" Then
        '    commandTextSb.AppendLine(" AND syouhin.syouhin_mei = @SyouhinMei ")
        'End If
        If strTorikesi = "0" Then
            commandTextSb.Append(" AND syouhin.torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        End If

        commandTextSb.Append(" ORDER BY syouhin.syouhin_cd ")
        'パラメータの設定
        paramList.Add(MakeParam("@SyouhinCd", SqlDbType.VarChar, 9, strSyouhinCd))
        'paramList.Add(MakeParam("@SyouhinMei", SqlDbType.VarChar, 42, strSyouhinMei))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.SyouhinTable.TableName, paramList.ToArray)

        Return dsCommonSearch.SyouhinTable
    End Function

    ''' <summary>加盟店データを取得する</summary>
    Public Function SelKameitenKensakuInfo(ByVal strKameitenCd As String, ByVal strTorikesi As String) As CommonSearchDataSet.KameitenSearchTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        Dim commandTextSb As New StringBuilder()
        Dim kbnCount As Integer = 1
        Dim tmpKbn1 As String = ""
        Dim tmpKbn2 As String = ""
        Dim tmpKbn3 As String = ""
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.Append("  k.kameiten_cd, k.kameiten_mei1, t.todouhuken_mei, k.tenmei_kana1")
        '============2012/03/29 車龍 405721案件の対応 追加↓=========================
        commandTextSb.Append("  ,k.torikesi ")
        commandTextSb.Append("  ,ISNULL(MKM.meisyou,'') AS torikesi_txt ")
        '============2012/03/29 車龍 405721案件の対応 追加↑=========================
        commandTextSb.Append("  FROM m_kameiten  k  WITH(READCOMMITTED)")
        commandTextSb.Append("  LEFT OUTER JOIN m_todoufuken  t  WITH(READCOMMITTED) ON t.todouhuken_cd = k.todouhuken_cd ")
        '============2012/03/29 車龍 405721案件の対応 追加↓=========================
        commandTextSb.Append("  LEFT OUTER JOIN m_kakutyou_meisyou  MKM  WITH(READCOMMITTED) ON MKM.meisyou_syubetu = 56 AND k.torikesi = MKM.code ")
        '============2012/03/29 車龍 405721案件の対応 追加↑=========================
        commandTextSb.Append("  WHERE k.kameiten_cd IS NOT NULL")
        If strTorikesi = "0" Then
            commandTextSb.Append(" AND k.torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        End If



        If strKameitenCd <> "" Then
            commandTextSb.Append(" AND k.kameiten_cd = @kameiten_cd")
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 6, strKameitenCd))
        End If

        commandTextSb.Append(" ORDER BY k.tenmei_kana1 ")


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.KameitenSearchTable.TableName, paramList.ToArray)


        Return dsCommonSearch.KameitenSearchTable
    End Function

End Class
