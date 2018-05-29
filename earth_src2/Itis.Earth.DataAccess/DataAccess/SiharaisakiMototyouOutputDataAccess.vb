Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
''' <summary>請求先元帳帳票出力処理</summary>
''' <history>
''' <para>2010/07/14　王霽陽(大連情報システム部)　新規作成</para>
''' </history>
Public Class SiharaisakiMototyouOutputDataAccess
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    Private connDBUser As String = System.Configuration.ConfigurationManager.AppSettings("connDBUser").ToString

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
#End Region

#Region "支払先元帳"

#Region "支払先元帳_繰越残高取得"

    ''' <summary>
    ''' 支払先元帳_繰越残高取得
    ''' </summary>
    ''' <param name="strTysKaisyaCd">調査会社コード</param>
    ''' <param name="strFromDate">年月日FROM</param>
    ''' <returns>繰越残高</returns>
    ''' <remarks></remarks>
    Public Function SelSiharaiSakiMototyouKurikosiZan(ByVal strTysKaisyaCd As String, _
                                                   ByVal strFromDate As String _
                                                   ) As Object

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsSiharaiSakiMototyouKurikosiZanDataSet As New Data.DataSet
        dsSiharaiSakiMototyouKurikosiZanDataSet.Tables.Add()
        Dim objReturn As Object

        ' SQL生成
        Dim sb As New StringBuilder()
        sb.AppendLine("SELECT                                                                                                                                ")
        sb.AppendLine("    ISNULL(tkurikosi.tougetu_kurikosi_zan, 0) + ISNULL(tkurikosi.siire_goukei, 0) - ISNULL(tkurikosi.siharai_goukei, 0) kurikosi_zan  ")
        sb.AppendLine("FROM                                                                                                                                  ")
        sb.AppendLine("    (                                                                                                                                 ")
        sb.AppendLine("        SELECT                                                                                                                        ")
        sb.AppendLine("            sm.tys_kaisya_cd                                                                                                          ")
        sb.AppendLine("            , sm.shri_jigyousyo_cd                                                                                                    ")
        sb.AppendLine("            , (                                                                                                                       ")
        sb.AppendLine("                SELECT                                                                                                                ")
        sb.AppendLine("                    sum(isnull(s2.siire_gaku, 0)) + sum(CONVERT(BIGINT, isnull(s2.sotozei_gaku, 0))) kurikosi_zan                     ")
        sb.AppendLine("                FROM                                                                                                                  ")
        sb.AppendLine("                    m_tyousakaisya tth3                                                                                       ")
        sb.AppendLine("                    INNER JOIN t_siire_data s2                                                                                ")
        sb.AppendLine("                        ON tth3.tys_kaisya_cd = s2.tys_kaisya_cd                                                                      ")
        sb.AppendLine("                        AND tth3.jigyousyo_cd = s2.tys_kaisya_jigyousyo_cd                                                            ")
        sb.AppendLine("                WHERE                                                                                                                 ")
        sb.AppendLine("                    tth3.tys_kaisya_cd = sm.tys_kaisya_cd                                                                             ")
        sb.AppendLine("                    AND tth3.shri_jigyousyo_cd = sm.shri_jigyousyo_cd                                                                 ")
        sb.AppendLine("                    AND s2.denpyou_siire_date BETWEEN DATEADD(                                                                        ")
        sb.AppendLine("                        DAY                                                                                                           ")
        sb.AppendLine("                        , + 1                                                                                                         ")
        sb.AppendLine("                        , ISNULL(ukt.taisyou_nengetu, '1900/1/1')                                                                     ")
        sb.AppendLine("                    ) AND DATEADD(DAY, - 1, @fromDate)                                                                               ")

        '2010/10/07 仕入データは、仕入処理FLG=1のみ対象とする 馬艶軍追加 ↓
        sb.AppendLine("		                AND  ")
        sb.AppendLine("			                ISNULL(s2.siire_keijyou_flg,'0') = '1'  ")
        '2010/10/07 仕入データは、仕入処理FLG=1のみ対象とする 馬艶軍追加 ↑

        sb.AppendLine("            ) siire_goukei                                                                                                            ")
        sb.AppendLine("            , (                                                                                                                       ")
        sb.AppendLine("                SELECT                                                                                                                ")
        sb.AppendLine("                    sum(isnull(h2.furikomi, 0)) + sum(isnull(h2.sousai, 0)) kurikosi_zan                                              ")
        sb.AppendLine("                FROM                                                                                                                  ")
        sb.AppendLine("                    m_tyousakaisya tt                                                                                         ")
        sb.AppendLine("                    INNER JOIN t_siharai_data h2                                                                              ")
        sb.AppendLine("                        ON tt.skk_shri_saki_cd = h2.skk_shri_saki_cd                                                                  ")
        sb.AppendLine("                        AND tt.skk_jigyousyo_cd = h2.skk_jigyou_cd                                                                    ")
        sb.AppendLine("                WHERE                                                                                                                 ")
        sb.AppendLine("                    tt.tys_kaisya_cd = sm.tys_kaisya_cd                                                                               ")
        sb.AppendLine("                    AND tt.shri_jigyousyo_cd = sm.shri_jigyousyo_cd                                                                   ")
        sb.AppendLine("                    AND h2.siharai_date BETWEEN DATEADD(                                                                              ")
        sb.AppendLine("                        DAY                                                                                                           ")
        sb.AppendLine("                        , + 1                                                                                                         ")
        sb.AppendLine("                        , ISNULL(ukt.taisyou_nengetu, '1900/1/1')                                                                     ")
        sb.AppendLine("                    ) AND DATEADD(DAY, - 1, @fromDate)                                                                               ")
        sb.AppendLine("                GROUP BY                                                                                                              ")
        sb.AppendLine("                    tt.tys_kaisya_cd                                                                                                  ")
        sb.AppendLine("                    , tt.shri_jigyousyo_cd                                                                                            ")
        sb.AppendLine("            ) siharai_goukei                                                                                                          ")
        sb.AppendLine("            , ukt.tougetu_kurikosi_zan                                                                                                ")
        sb.AppendLine("        FROM                                                                                                                          ")
        sb.AppendLine("            m_tyousakaisya sm                                                                                                 ")
        sb.AppendLine("            LEFT OUTER JOIN (                                                                                                         ")
        sb.AppendLine("                SELECT                                                                                                                ")
        sb.AppendLine("                    uk.taisyou_nengetu                                                                                                ")
        sb.AppendLine("                    , uk.tys_kaisya_cd                                                                                                ")
        sb.AppendLine("                    , uk.shri_jigyousyo_cd                                                                                            ")
        sb.AppendLine("                    , uk.tougetu_kurikosi_zan                                                                                         ")
        sb.AppendLine("                FROM                                                                                                                  ")
        sb.AppendLine("                    t_kaikake_data uk                                                                                         ")
        sb.AppendLine("                    LEFT OUTER JOIN (                                                                                                 ")
        sb.AppendLine("                        SELECT                                                                                                        ")
        sb.AppendLine("                            tys_kaisya_cd                                                                                             ")
        sb.AppendLine("                            , shri_jigyousyo_cd                                                                                       ")
        sb.AppendLine("                            , max(taisyou_nengetu) taisyou_nengetu                                                                    ")
        sb.AppendLine("                        FROM                                                                                                          ")
        sb.AppendLine("                            t_kaikake_data                                                                                    ")
        sb.AppendLine("                        WHERE                                                                                                         ")
        sb.AppendLine("                            taisyou_nengetu <= " & connDBUser & "fnGetLastDay(DATEADD(MONTH, - 1, @fromDate))                                  ")
        sb.AppendLine("                        GROUP BY                                                                                                      ")
        sb.AppendLine("                            tys_kaisya_cd                                                                                             ")
        sb.AppendLine("                            , shri_jigyousyo_cd                                                                                       ")
        sb.AppendLine("                    ) ukm                                                                                                             ")
        sb.AppendLine("                        ON uk.tys_kaisya_cd = ukm.tys_kaisya_cd                                                                       ")
        sb.AppendLine("                        AND uk.shri_jigyousyo_cd = ukm.shri_jigyousyo_cd                                                              ")
        sb.AppendLine("                WHERE                                                                                                                 ")
        sb.AppendLine("                    uk.taisyou_nengetu = ukm.taisyou_nengetu                                                                          ")
        sb.AppendLine("            ) ukt                                                                                                                     ")
        sb.AppendLine("                ON sm.tys_kaisya_cd = ukt.tys_kaisya_cd                                                                               ")
        sb.AppendLine("                AND sm.shri_jigyousyo_cd = ukt.shri_jigyousyo_cd                                                                      ")
        sb.AppendLine("        WHERE                                                                                                                         ")
        sb.AppendLine("            sm.jigyousyo_cd = sm.shri_jigyousyo_cd                                                                                    ")
        sb.AppendLine("    ) tkurikosi                                                                                                                       ")
        sb.AppendLine("WHERE                                                                                                                                 ")
        sb.AppendLine("    tkurikosi.tys_kaisya_cd + tkurikosi.shri_jigyousyo_cd = @strTysKaisyaCd                                                              ")

        'パラメータの設定
        paramList.Add(MakeParam("@strTysKaisyaCd", SqlDbType.VarChar, 7, strTysKaisyaCd))   '調査会社コード 
        paramList.Add(MakeParam("@fromDate", SqlDbType.VarChar, 10, strFromDate))           '年月日FROM

        '検索実行
        FillDataset(connStr, CommandType.Text, sb.ToString(), dsSiharaiSakiMototyouKurikosiZanDataSet, dsSiharaiSakiMototyouKurikosiZanDataSet.Tables(0).TableName, paramList.ToArray)

        If dsSiharaiSakiMototyouKurikosiZanDataSet.Tables(0).Rows.Count = 0 Then
            objReturn = 0
        Else
            objReturn = dsSiharaiSakiMototyouKurikosiZanDataSet.Tables(0).Rows(0).Item("kurikosi_zan").ToString
        End If

        Return objReturn

    End Function

#End Region

#Region "支払先元帳_伝票データ取得"

    ''' <summary>
    ''' 支払先元帳_伝票データ取得
    ''' </summary>
    ''' <param name="strTysKaisyaCd">調査会社コード</param>
    ''' <param name="strJigyousyoCd">支払集計先事業所コード</param>
    ''' <param name="strFromDate">年月日FROM</param>
    ''' <param name="strToDate">年月日TO</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function SelSiharaiSakiMototyouData(ByVal strTysKaisyaCd As String, _
                                                   ByVal strJigyousyoCd As String, _
                                                   ByVal strFromDate As String, _
                                                   ByVal strToDate As String _
                                                   ) As DataTable

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsSelSiharaiSakiMototyouDataDataSet As New Data.DataSet
        dsSelSiharaiSakiMototyouDataDataSet.Tables.Add()

        ' SQL生成
        Dim sb As New StringBuilder()
        'SQL文
        With sb
            '--仕入データテーブル
            .AppendLine("SELECT                                                                                               ")
            .AppendLine("toutou.nengappi                                                                                      ")
            .AppendLine(",toutou.kamoku                                                                                       ")
            .AppendLine(",toutou.syouhin_cd                                                                                   ")
            .AppendLine(",toutou.hinmei                                                                                       ")
            .AppendLine(",toutou.kyakuno                                                                                      ")
            .AppendLine(",toutou.jutyuu_bukken_mei                                                                            ")
            .AppendLine(",toutou.suu                                                                                          ")
            .AppendLine(",toutou.tanka                                                                                        ")
            .AppendLine(",toutou.siire_gaku                                                                                   ")
            .AppendLine(",toutou.sotozei_gaku                                                                                 ")
            .AppendLine(",toutou.kingaku                                                                                      ")
            .AppendLine(",toutou.denpyou_no                                                                                   ")
            .AppendLine("FROM                                                                                                 ")
            .AppendLine("( SELECT                                                                                             ")
            .AppendLine("-- tu.denpyou_unique_no                                                                              ")
            .AppendLine("tu.denpyou_siire_date nengappi                                                                       ")
            .AppendLine(", '仕入' kamoku                                                                                      ")
            .AppendLine(", tu.syouhin_cd                                                                                      ")
            .AppendLine(", tu.hinmei                                                                                          ")
            .AppendLine(", ISNULL(tu.kbn,'') + ISNULL(tu.bangou,'') AS kyakuno                                                ")
            .AppendLine(", tj.jutyuu_bukken_mei                                                                               ")
            .AppendLine(", tu.suu                                                                                             ")
            .AppendLine(", tu.tanka                                                                                           ")
            .AppendLine(", tu.siire_gaku                                                                                      ")
            .AppendLine(", tu.sotozei_gaku                                                                                    ")
            .AppendLine(", ISNULL(tu.siire_gaku,0) + ISNULL(tu.sotozei_gaku,0) AS  kingaku                                    ")
            .AppendLine(", tu.denpyou_no                                                                                      ")
            .AppendLine(" FROM--抽出期間内の仕入データを取得                                                                    ")
            .AppendLine("      t_siire_data tu                                                                                ")
            .AppendLine("INNER JOIN                                                                                           ")
            .AppendLine("m_tyousakaisya mt                                                                                    ")
            .AppendLine("ON                                                                                                   ")
            .AppendLine("mt.tys_kaisya_cd = tu.tys_kaisya_cd                                                                  ")
            .AppendLine("AND                                                                                                  ")
            .AppendLine("mt.jigyousyo_cd = tu.tys_kaisya_jigyousyo_cd                                                         ")
            .AppendLine("LEFT OUTER JOIN                                                                                      ")
            .AppendLine("t_jiban tj                                                                                           ")
            .AppendLine("ON                                                                                                   ")
            .AppendLine("tu.kbn = tj.kbn                                                                                      ")
            .AppendLine("AND                                                                                                  ")
            .AppendLine("tu.bangou = tj.hosyousyo_no                                                                          ")
            .AppendLine("WHERE                                                                                                ")
            .AppendLine("    mt.tys_kaisya_cd = @strTysKaisyaCd                                                                ")
            .AppendLine("AND                                                                                                  ")
            .AppendLine("    mt.shri_jigyousyo_cd = @strJigyousyoCd                                                           ")
            .AppendLine("AND tu.denpyou_siire_date BETWEEN @fromDate AND @toDate                                              ")

            '2010/10/07 仕入データは、仕入処理FLG=1のみ対象とする 馬艶軍追加 ↓
            sb.AppendLine("AND  ")
            sb.AppendLine("	ISNULL(tu.siire_keijyou_flg,'0') = '1'  ")
            '2010/10/07 仕入データは、仕入処理FLG=1のみ対象とする 馬艶軍追加 ↑

            .AppendLine("UNION ALL                                                                                            ")
            .AppendLine("--支払データテーブル                                                                                  ")
            .AppendLine("SELECT --[振込]                                                                                      ")
            .AppendLine("tu.siharai_date                                                                                      ")
            .AppendLine(", '支払'                                                                                             ")
            .AppendLine(", ''                                                                                                 ")
            .AppendLine(", '振込'                                                                                             ")
            .AppendLine(", ''                                                                                                 ")
            .AppendLine(", tu.tekiyou_mei                                                                                     ")
            .AppendLine(", NULL                                                                                               ")
            .AppendLine(", NULL                                                                                               ")
            .AppendLine(", NULL                                                                                               ")
            .AppendLine(", NULL                                                                                               ")
            .AppendLine(", tu.furikomi                                                                                        ")
            .AppendLine(", tu.denpyou_no                                                                                      ")
            .AppendLine("FROM                                                                                                 ")
            .AppendLine("    t_siharai_data tu                                                                                ")
            .AppendLine("INNER JOIN                                                                                           ")
            .AppendLine("m_tyousakaisya mt                                                                                    ")
            .AppendLine("ON                                                                                                   ")
            .AppendLine("tu.skk_jigyou_cd = mt.skk_jigyousyo_cd                                                               ")
            .AppendLine("AND                                                                                                  ")
            .AppendLine("tu.skk_shri_saki_cd = mt.skk_shri_saki_cd                                                            ")
            .AppendLine("WHERE                                                                                                ")
            .AppendLine("mt.tys_kaisya_cd = @strTysKaisyaCd                                                                   ")
            .AppendLine("AND                                                                                                  ")
            .AppendLine("mt.jigyousyo_cd = @strJigyousyoCd                                                                    ")
            .AppendLine("AND                                                                                                  ")
            .AppendLine("tu.furikomi <> 0                                                                                     ")
            .AppendLine("AND tu.siharai_date BETWEEN @fromDate AND @toDate                                                    ")
            .AppendLine("UNION ALL                                                                                            ")
            .AppendLine("SELECT--[相殺]                                                                                       ")
            .AppendLine("tu.siharai_date                                                                                      ")
            .AppendLine(", '支払'                                                                                             ")
            .AppendLine(", ''                                                                                                 ")
            .AppendLine(", '相殺'                                                                                             ")
            .AppendLine(", ''                                                                                                 ")
            .AppendLine(", tu.tekiyou_mei                                                                                     ")
            .AppendLine(", NULL                                                                                               ")
            .AppendLine(", NULL                                                                                               ")
            .AppendLine(", NULL                                                                                               ")
            .AppendLine(", NULL                                                                                               ")
            .AppendLine(", tu.sousai                                                                                          ")
            .AppendLine(", tu.denpyou_no                                                                                      ")
            .AppendLine("FROM                                                                                                 ")
            .AppendLine("    t_siharai_data tu                                                                                ")
            .AppendLine("INNER JOIN                                                                                           ")
            .AppendLine("m_tyousakaisya mt                                                                                    ")
            .AppendLine("ON                                                                                                   ")
            .AppendLine("tu.skk_jigyou_cd = mt.skk_jigyousyo_cd                                                               ")
            .AppendLine("AND                                                                                                  ")
            .AppendLine("tu.skk_shri_saki_cd = mt.skk_shri_saki_cd                                                            ")
            .AppendLine("WHERE                                                                                                ")
            .AppendLine("    mt.tys_kaisya_cd = @strTysKaisyaCd                                                               ") '調査会社コード
            .AppendLine("AND                                                                                                  ")
            .AppendLine("mt.jigyousyo_cd = @strJigyousyoCd                                                                     ") '支払集計先事業所コード
            .AppendLine("AND                                                                                                  ")
            .AppendLine("tu.sousai <> 0                                                                                       ")
            .AppendLine("AND tu.siharai_date BETWEEN @fromDate AND @toDate                                                    ")
            .AppendLine(") AS toutou                                                                                          ")
            .AppendLine("ORDER BY                                                                                             ")
            .AppendLine("toutou.nengappi                                                                                      ")
            .AppendLine(",toutou.kamoku                                                                                       ")
            .AppendLine(",toutou.denpyou_no                                                                                   ")

        End With

        'パラメータの設定
        paramList.Add(MakeParam("@strTysKaisyaCd", SqlDbType.VarChar, 5, strTysKaisyaCd))   '調査会社コード 
        paramList.Add(MakeParam("@strJigyousyoCd", SqlDbType.VarChar, 2, strJigyousyoCd))   '支払集計先事業所コード
        paramList.Add(MakeParam("@fromDate", SqlDbType.VarChar, 10, strFromDate))           '年月日FROM
        paramList.Add(MakeParam("@toDate", SqlDbType.VarChar, 10, strToDate))               '年月日TO

        '検索実行
        FillDataset(connStr, CommandType.Text, sb.ToString(), dsSelSiharaiSakiMototyouDataDataSet, dsSelSiharaiSakiMototyouDataDataSet.Tables(0).TableName, paramList.ToArray)

        Return dsSelSiharaiSakiMototyouDataDataSet.Tables(0)

    End Function

#End Region

#End Region

End Class
