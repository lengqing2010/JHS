Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class SeikyuuSakiHenkouMasterDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    Public Function SelSeikyuuInfo(ByVal strKameitenCd As String, ByVal strSyouhinKBN As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ISNULL(kameiten_cd,'')+','+ ")
        commandTextSb.AppendLine(" ISNULL(kameiten_mei1,'') AS kameiten ")
        '============2012/03/29 車龍 405721案件の対応 追加↓=========================
        commandTextSb.AppendLine(" ,CONVERT(VARCHAR(10),torikesi) + ':' + ISNULL(torikesi_txt,'') AS torikesi ")
        '============2012/03/29 車龍 405721案件の対応 追加↑=========================
        commandTextSb.AppendLine(" ,ISNULL(syouhin_kbn,'') AS syouhin_kbn ")
        commandTextSb.AppendLine(" ,ISNULL(seikyuu_saki_kbn,'')+','+ ")
        commandTextSb.AppendLine(" ISNULL(seikyuu_henkou_saki,'')+','+ISNULL(brc,'') ")
        commandTextSb.AppendLine(" +','+ISNULL(kameiten_seisiki_mei,'') AS seikyuu_saki ")
        commandTextSb.AppendLine(" ,upd_datetime ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" (SELECT m_seikyuu_saki_henkou.kameiten_cd AS kameiten_cd ")
        commandTextSb.AppendLine(" ,m_kameiten.kameiten_mei1 AS kameiten_mei1 ")
        commandTextSb.AppendLine(" ,m_seikyuu_saki_henkou.syouhin_kbn AS syouhin_kbn ")
        commandTextSb.AppendLine(" ,m_seikyuu_saki_henkou.seikyuu_saki_kbn AS seikyuu_saki_kbn ")
        commandTextSb.AppendLine(" ,m_seikyuu_saki_henkou.seikyuu_henkou_saki AS seikyuu_henkou_saki ")
        commandTextSb.AppendLine(" ,m_seikyuu_saki_henkou.brc AS brc ")
        commandTextSb.AppendLine(" ,m_kameiten2.kameiten_seisiki_mei AS kameiten_seisiki_mei ")
        commandTextSb.AppendLine(" ,ISNULL(CONVERT(varchar(19),m_seikyuu_saki_henkou.upd_datetime,21),'') AS upd_datetime ")
        '============2012/03/29 車龍 405721案件の対応 追加↓=========================
        commandTextSb.AppendLine(" ,m_kameiten.torikesi ")
        commandTextSb.AppendLine(" ,ISNULL(MKM.meisyou,'') AS torikesi_txt ")
        '============2012/03/29 車龍 405721案件の対応 追加↑=========================
        commandTextSb.AppendLine(" FROM  m_seikyuu_saki_henkou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_seikyuu_saki_henkou.kameiten_cd=m_kameiten.kameiten_cd ")
        '============2012/03/29 車龍 405721案件の対応 追加↓=========================
        commandTextSb.AppendLine(" LEFT OUTER JOIN m_kakutyou_meisyou MKM WITH(READCOMMITTED) ON MKM.meisyou_syubetu = 56 AND m_kameiten.torikesi = MKM.code ")
        '============2012/03/29 車龍 405721案件の対応 追加↑=========================
        commandTextSb.AppendLine(" LEFT JOIN m_kameiten AS m_kameiten2  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_seikyuu_saki_henkou.seikyuu_henkou_saki=m_kameiten2.kameiten_cd ")
        commandTextSb.AppendLine(" WHERE m_seikyuu_saki_henkou.seikyuu_saki_kbn='0'  ")
        commandTextSb.AppendLine(" AND m_seikyuu_saki_henkou.kameiten_cd=@kameiten_cd ")
        If strSyouhinKBN.Trim <> "" Then
            commandTextSb.AppendLine(" AND m_seikyuu_saki_henkou.syouhin_kbn=@KBN ")
        End If

        commandTextSb.AppendLine(" UNION ALL ")
        commandTextSb.AppendLine(" SELECT m_seikyuu_saki_henkou.kameiten_cd ")
        commandTextSb.AppendLine(" ,m_kameiten.kameiten_mei1 ")
        commandTextSb.AppendLine(" ,m_seikyuu_saki_henkou.syouhin_kbn ")
        commandTextSb.AppendLine(" ,m_seikyuu_saki_henkou.seikyuu_saki_kbn ")
        commandTextSb.AppendLine(" ,m_seikyuu_saki_henkou.seikyuu_henkou_saki ")
        commandTextSb.AppendLine(" ,m_seikyuu_saki_henkou.brc ")
        commandTextSb.AppendLine(" ,m_tyousakaisya.seikyuu_saki_shri_saki_mei ")
        commandTextSb.AppendLine(" ,ISNULL(CONVERT(varchar(19),m_seikyuu_saki_henkou.upd_datetime,21),'') AS upd_datetime ")
        '============2012/03/29 車龍 405721案件の対応 追加↓=========================
        commandTextSb.AppendLine(" ,m_kameiten.torikesi ")
        commandTextSb.AppendLine(" ,ISNULL(MKM.meisyou,'') AS torikesi_txt ")
        '============2012/03/29 車龍 405721案件の対応 追加↑=========================
        commandTextSb.AppendLine(" FROM  m_seikyuu_saki_henkou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_seikyuu_saki_henkou.kameiten_cd=m_kameiten.kameiten_cd ")
        '============2012/03/29 車龍 405721案件の対応 追加↓=========================
        commandTextSb.AppendLine(" LEFT OUTER JOIN m_kakutyou_meisyou MKM WITH(READCOMMITTED) ON MKM.meisyou_syubetu = 56 AND m_kameiten.torikesi = MKM.code ")
        '============2012/03/29 車龍 405721案件の対応 追加↑=========================
        commandTextSb.AppendLine(" LEFT JOIN m_tyousakaisya  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_seikyuu_saki_henkou.seikyuu_henkou_saki=m_tyousakaisya.tys_kaisya_cd ")
        commandTextSb.AppendLine(" AND m_seikyuu_saki_henkou.brc=m_tyousakaisya.jigyousyo_cd ")
        commandTextSb.AppendLine(" WHERE m_seikyuu_saki_henkou.seikyuu_saki_kbn='1'  ")
        commandTextSb.AppendLine(" AND m_seikyuu_saki_henkou.kameiten_cd=@kameiten_cd ")
        If strSyouhinKBN.Trim <> "" Then
            commandTextSb.AppendLine(" AND m_seikyuu_saki_henkou.syouhin_kbn=@KBN ")
        End If

        commandTextSb.AppendLine(" UNION ALL ")
        commandTextSb.AppendLine(" SELECT m_seikyuu_saki_henkou.kameiten_cd ")
        commandTextSb.AppendLine(" ,m_kameiten.kameiten_mei1 ")
        commandTextSb.AppendLine(" ,m_seikyuu_saki_henkou.syouhin_kbn ")
        commandTextSb.AppendLine(" ,m_seikyuu_saki_henkou.seikyuu_saki_kbn ")
        commandTextSb.AppendLine(" ,m_seikyuu_saki_henkou.seikyuu_henkou_saki ")
        commandTextSb.AppendLine(" ,m_seikyuu_saki_henkou.brc ")
        commandTextSb.AppendLine(" ,m_eigyousyo.seikyuu_saki_mei ")
        commandTextSb.AppendLine(" ,ISNULL(CONVERT(varchar(19),m_seikyuu_saki_henkou.upd_datetime,21),'') AS upd_datetime ")
        '============2012/03/29 車龍 405721案件の対応 追加↓=========================
        commandTextSb.AppendLine(" ,m_kameiten.torikesi ")
        commandTextSb.AppendLine(" ,ISNULL(MKM.meisyou,'') AS torikesi_txt ")
        '============2012/03/29 車龍 405721案件の対応 追加↑=========================
        commandTextSb.AppendLine(" FROM  m_seikyuu_saki_henkou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_seikyuu_saki_henkou.kameiten_cd=m_kameiten.kameiten_cd ")
        '============2012/03/29 車龍 405721案件の対応 追加↓=========================
        commandTextSb.AppendLine(" LEFT OUTER JOIN m_kakutyou_meisyou MKM WITH(READCOMMITTED) ON MKM.meisyou_syubetu = 56 AND m_kameiten.torikesi = MKM.code ")
        '============2012/03/29 車龍 405721案件の対応 追加↑=========================
        commandTextSb.AppendLine(" LEFT JOIN m_eigyousyo  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_seikyuu_saki_henkou.seikyuu_henkou_saki=m_eigyousyo.eigyousyo_cd ")
        commandTextSb.AppendLine(" WHERE m_seikyuu_saki_henkou.seikyuu_saki_kbn='2'  ")
        commandTextSb.AppendLine(" AND m_seikyuu_saki_henkou.kameiten_cd=@kameiten_cd ")
        If strSyouhinKBN.Trim <> "" Then
            commandTextSb.AppendLine(" AND m_seikyuu_saki_henkou.syouhin_kbn=@KBN ")
        End If
        commandTextSb.AppendLine(" ) AS MAIN ")
        commandTextSb.AppendLine(" ORDER BY  kameiten_cd,syouhin_kbn ")

        'パラメータの設定
        paramList.Add(MakeParam("@KBN", SqlDbType.VarChar, 10, strSyouhinKBN))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                         "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)

    End Function
    Public Function SelSeikyuuSakiMei(ByVal strCd As String, ByVal strBrc As String, ByVal strKBN As String, ByVal strTorikesi As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT seikyuu_saki_mei ")
        commandTextSb.AppendLine("  ,seikyuu_saki_kbn ")
        commandTextSb.AppendLine("  ,seikyuu_saki_cd ")
        commandTextSb.AppendLine("  ,seikyuu_saki_brc ")
        commandTextSb.AppendLine(" FROM v_seikyuu_saki_info WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE seikyuu_saki_cd is not null ")
        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND seikyuu_saki_cd=@seikyuu_saki_cd ")
            paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strCd))
        End If
        If strBrc.Trim <> "" Then
            commandTextSb.AppendLine(" AND seikyuu_saki_brc=@seikyuu_saki_brc ")
            paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strBrc))
        End If
        If strKBN.Trim <> "" Then
            commandTextSb.AppendLine(" AND seikyuu_saki_kbn=@seikyuu_saki_kbn ")
            paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, strKBN))
        End If


        If strTorikesi = "0" Then
            commandTextSb.Append(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        End If
        'パラメータの設定
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                         "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)

    End Function

    Public Function SelJyuufuku(ByVal strKameiten As String, ByVal strSyouhinKBN As String) As DataTable

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
        commandTextSb.AppendLine(" m_seikyuu_saki_henkou  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  kameiten_cd=@KameitenCd  ")
        commandTextSb.AppendLine(" AND syouhin_kbn=@syouhin_kbn  ")

        'パラメータの設定
        paramList.Add(MakeParam("@KameitenCd", SqlDbType.VarChar, 5, strKameiten))
        paramList.Add(MakeParam("@syouhin_kbn", SqlDbType.VarChar, 10, strSyouhinKBN))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function InsSeikyuuSaki(ByVal strKameiten As String, _
                           ByVal strSyouhinKBN As String, _
                           ByVal strHenkouSaki As String, _
                           ByVal strBrc As String, _
                           ByVal strSakiKBN As String, _
                           ByVal strUserId As String) As Integer



        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("INSERT INTO m_seikyuu_saki_henkou WITH (UPDLOCK) ")
        commandTextSb.AppendLine("(kameiten_cd")
        commandTextSb.AppendLine(",syouhin_kbn")
        commandTextSb.AppendLine(",seikyuu_henkou_saki")
        commandTextSb.AppendLine(",brc")
        commandTextSb.AppendLine(",seikyuu_saki_kbn")
        commandTextSb.AppendLine(",add_login_user_id")
        commandTextSb.AppendLine(",add_datetime)")

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" @kameiten_cd")
        commandTextSb.AppendLine(",@syouhin_kbn")
        commandTextSb.AppendLine(",@seikyuu_henkou_saki")
        commandTextSb.AppendLine(",@brc")
        commandTextSb.AppendLine(",@seikyuu_saki_kbn")
        commandTextSb.AppendLine(",@add_login_user_id")
        commandTextSb.AppendLine(",GETDATE() ")


        'パラメータの設定()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameiten))
        paramList.Add(MakeParam("@syouhin_kbn", SqlDbType.VarChar, 10, strSyouhinKBN))
        paramList.Add(MakeParam("@seikyuu_henkou_saki", SqlDbType.VarChar, 5, strHenkouSaki))
        paramList.Add(MakeParam("@brc", SqlDbType.VarChar, 2, strBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, strSakiKBN))

        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function
    Public Function SelHaita(ByVal strKameitenCd As String, ByVal strSyouhinKBN As String, ByVal strKousin As String) As DataTable

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
        commandTextSb.AppendLine(" m_seikyuu_saki_henkou  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE    ")
        commandTextSb.AppendLine(" syouhin_kbn=@syouhin_kbn  ")
        commandTextSb.AppendLine(" AND kameiten_cd=@KameitenCd  ")
        commandTextSb.AppendLine(" AND CONVERT(varchar(19),upd_datetime,21)<>@upd_datetime  ")
        commandTextSb.AppendLine(" AND upd_datetime IS NOT NULL  ")
        'パラメータの設定

        paramList.Add(MakeParam("@syouhin_kbn", SqlDbType.VarChar, 10, strSyouhinKBN))
        paramList.Add(MakeParam("@KameitenCd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousin))


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function UpdSeikyuuSaki(ByVal strKameiten As String, _
                           ByVal strSyouhinKBN As String, _
                           ByVal strHenkouSaki As String, _
                           ByVal strBrc As String, _
                           ByVal strSakiKBN As String, _
                           ByVal strUserId As String, ByVal strSyouhinmae As String) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" UPDATE m_seikyuu_saki_henkou  WITH (UPDLOCK)    ")
        commandTextSb.AppendLine(" SET kameiten_cd=@kameiten_cd ")
        commandTextSb.AppendLine(" ,syouhin_kbn=@syouhin_kbn ")
        commandTextSb.AppendLine(" ,seikyuu_henkou_saki=@seikyuu_henkou_saki ")
        commandTextSb.AppendLine(" ,brc=@brc ")
        commandTextSb.AppendLine(" ,seikyuu_saki_kbn=@seikyuu_saki_kbn ")
        commandTextSb.AppendLine(" ,upd_login_user_id=@add_login_user_id")
        commandTextSb.AppendLine(" ,upd_datetime=GETDATE()")
        commandTextSb.AppendLine(" WHERE kameiten_cd=@kameiten_cd")
        commandTextSb.AppendLine(" AND syouhin_kbn=@syouhin_kbn2")
        'パラメータの設定()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameiten))
        paramList.Add(MakeParam("@syouhin_kbn", SqlDbType.VarChar, 10, strSyouhinKBN))
        paramList.Add(MakeParam("@syouhin_kbn2", SqlDbType.VarChar, 10, strSyouhinmae))
        paramList.Add(MakeParam("@seikyuu_henkou_saki", SqlDbType.VarChar, 5, strHenkouSaki))
        paramList.Add(MakeParam("@brc", SqlDbType.VarChar, 2, strBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, strSakiKBN))

        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)



    End Function
    Public Function DelSeikyuuSaki(ByVal strKameitenCd As String, ByVal strSyouhinKBN As String) As Integer

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New SyouhinDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("DELETE FROM m_seikyuu_saki_henkou WITH (UPDLOCK) ")
        commandTextSb.AppendLine(" WHERE kameiten_cd=@kameiten_cd")
        commandTextSb.AppendLine(" AND syouhin_kbn=@syouhin_kbn")


        'パラメータの設定()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@syouhin_kbn", SqlDbType.VarChar, 10, strSyouhinKBN))

        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function
End Class
