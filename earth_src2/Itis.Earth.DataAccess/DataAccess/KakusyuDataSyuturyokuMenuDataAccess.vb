Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class KakusyuDataSyuturyokuMenuDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    'データベース管理者
    Private connDBUser As String = System.Configuration.ConfigurationManager.AppSettings("connDBUser").ToString

    ''' <summary>
    ''' システム時間
    ''' </summary>
    ''' <returns></returns>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Public Function GetSysTime() As String
        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine("SELECT   ")
            .AppendLine("   GETDATE() ")
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet")

        '戻る
        Return CDate(dsDataSet.Tables(0).Rows(0).Item(0)).ToString("yyyy/MM/dd")

    End Function

    ''' <summary>
    ''' EXCEL仕訳売上データ出力
    ''' </summary>
    ''' <param name="strDateFrom">抽出期間FROM</param>
    ''' <param name="strDateTo">抽出期間TO</param>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Public Function GetExcelSiwakeUriage(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.ExcelSiwakeUriageDataTableDataTable
        ' DataSetインスタンスの生成
        Dim dsDataSet As New ExcelSiwakeDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	tekiyou ")
            .AppendLine("	,kari_zei ")
            .AppendLine("	,kari_kamoku_mei ")
            .AppendLine("	,kari_kamoku ")
            .AppendLine("	,kari_saimoku ")
            .AppendLine("	,kari_keisiki ")
            .AppendLine("	,kari_youto ")
            .AppendLine("	,kari_tukekaesaki ")
            .AppendLine("	,kari_line ")
            .AppendLine("	,kari_aitesaki ")
            .AppendLine("	,kari_kingaku ")
            .AppendLine("	,kasi_zei ")
            .AppendLine("	,kasi_kamoku_mei ")
            .AppendLine("	,kasi_kamoku ")
            .AppendLine("	,kasi_saimoku ")
            .AppendLine("	,kasi_keisiki ")
            .AppendLine("	,kasi_youto ")
            .AppendLine("	,kasi_tukekaesaki ")
            .AppendLine("	,kasi_line ")
            .AppendLine("	,kasi_aitesaki ")
            .AppendLine("	,kasi_kingaku ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			@tekiyou tekiyou ")
            .AppendLine("			,'99' kari_zei ")
            .AppendLine("			,'' kari_kamoku_mei ")
            .AppendLine("			,'01518' kari_kamoku ")
            .AppendLine("			,'' kari_saimoku ")
            .AppendLine("			,'' kari_keisiki ")
            .AppendLine("			,'' kari_youto ")
            .AppendLine("			,'YMP8' kari_tukekaesaki ")
            .AppendLine("			,'' kari_line ")
            .AppendLine("			,'9999999999' kari_aitesaki ")
            .AppendLine("			,SUM(ISNULL(tu.uri_gaku,0)) kari_kingaku ")
            .AppendLine("			,'11' kasi_zei ")
            .AppendLine("			,'' kasi_kamoku_mei ")
            .AppendLine("			,'51195' kasi_kamoku ")
            .AppendLine("			,'777' kasi_saimoku ")
            .AppendLine("			,'' kasi_keisiki ")
            .AppendLine("			,'' kasi_youto ")
            .AppendLine("			,'YMP8' kasi_tukekaesaki ")
            .AppendLine("			,'' kasi_line ")
            .AppendLine("			,'9999999999' kasi_aitesaki ")
            .AppendLine("			,SUM(ISNULL(tu.uri_gaku,0)) kasi_kingaku  ")
            .AppendLine("		FROM ")
            .AppendLine("			t_uriage_data tu  ")
            .AppendLine("		INNER JOIN  ")
            .AppendLine("			m_seikyuu_saki ms  ")
            .AppendLine("		ON  ")
            .AppendLine("			tu.seikyuu_saki_cd = ms.seikyuu_saki_cd  ")
            .AppendLine("		AND  ")
            .AppendLine("			tu.seikyuu_saki_brc = ms.seikyuu_saki_brc  ")
            .AppendLine("		AND  ")
            .AppendLine("			tu.seikyuu_saki_kbn = ms.seikyuu_saki_kbn  ")
            .AppendLine("		WHERE ")
            .AppendLine("			CONVERT(CHAR(10),CONVERT(DATETIME,ISNULL(tu.denpyou_uri_date,'1900/01/01')),111) BETWEEN CONVERT(CHAR(10),CONVERT(DATETIME,@fromDate),111) AND CONVERT(CHAR(10),CONVERT(DATETIME,@toDate),111)  ")
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.zei_kbn,0) > 0  ")
            .AppendLine("		AND  ")
            .AppendLine("			ms.skk_jigyousyo_cd IS NULL  ")
            '2010/10/07 売上データは、売上処理FLG=1のみ対象とする 馬艶軍追加 ↓
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.uri_keijyou_flg,'0') = '1'  ")
            '2010/10/07 売上データは、売上処理FLG=1のみ対象とする 馬艶軍追加 ↑
            '.AppendLine("		GROUP BY ")
            '.AppendLine("			ms.skk_jigyousyo_cd ")
            .AppendLine(" ")
            .AppendLine("	UNION ALL  ")
            .AppendLine(" ")
            .AppendLine("		SELECT ")
            .AppendLine("			@tekiyou tekiyou ")
            .AppendLine("			,'99' kari_zei ")
            .AppendLine("			,'' kari_kamoku_mei ")
            .AppendLine("			,'01518' kari_kamoku ")
            .AppendLine("			,'' kari_saimoku ")
            .AppendLine("			,'' kari_keisiki ")
            .AppendLine("			,'' kari_youto ")
            .AppendLine("			,'YMP8' kari_tukekaesaki ")
            .AppendLine("			,'' kari_line ")
            .AppendLine("			,'9999999999' kari_aitesaki ")
            .AppendLine("			,SUM(ISNULL(tu.sotozei_gaku,0)) kari_kingaku ")
            .AppendLine("			,'77' kasi_zei ")
            .AppendLine("			,'' kasi_kamoku_mei ")
            .AppendLine("			,'24293' kasi_kamoku ")
            .AppendLine("			,'777' kasi_saimoku ")
            .AppendLine("			,'' kasi_keisiki ")
            .AppendLine("			,'' kasi_youto ")
            .AppendLine("			,'YMP8' kasi_tukekaesaki ")
            .AppendLine("			,'' kasi_line ")
            .AppendLine("			,'9999999999' kasi_aitesaki ")
            .AppendLine("			,SUM(ISNULL(tu.sotozei_gaku,0)) kasi_kingaku  ")
            .AppendLine("		FROM ")
            .AppendLine("			t_uriage_data tu  ")
            .AppendLine("		INNER JOIN  ")
            .AppendLine("			m_seikyuu_saki ms  ")
            .AppendLine("		ON  ")
            .AppendLine("			tu.seikyuu_saki_cd = ms.seikyuu_saki_cd  ")
            .AppendLine("		AND  ")
            .AppendLine("			tu.seikyuu_saki_brc = ms.seikyuu_saki_brc  ")
            .AppendLine("		AND  ")
            .AppendLine("			tu.seikyuu_saki_kbn = ms.seikyuu_saki_kbn  ")
            .AppendLine("		WHERE ")
            .AppendLine("			CONVERT(CHAR(10),CONVERT(DATETIME,ISNULL(tu.denpyou_uri_date,'1900/01/01')),111) BETWEEN CONVERT(CHAR(10),CONVERT(DATETIME,@fromDate),111) AND CONVERT(CHAR(10),CONVERT(DATETIME,@toDate),111)  ")
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.zei_kbn,0) > 0  ")
            .AppendLine("		AND  ")
            .AppendLine("			ms.skk_jigyousyo_cd IS NULL  ")
            '2010/10/07 売上データは、売上処理FLG=1のみ対象とする 馬艶軍追加 ↓
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.uri_keijyou_flg,'0') = '1'  ")
            '2010/10/07 売上データは、売上処理FLG=1のみ対象とする 馬艶軍追加 ↑
            '.AppendLine("		GROUP BY ")
            '.AppendLine("			ms.skk_jigyousyo_cd ")
            .AppendLine(" ")
            .AppendLine("	UNION ALL  ")
            .AppendLine(" ")
            .AppendLine("		SELECT ")
            .AppendLine("			@tekiyou tekiyou ")
            .AppendLine("			,'99' kari_zei ")
            .AppendLine("			,'' kari_kamoku_mei ")
            .AppendLine("			,'01518' kari_kamoku ")
            .AppendLine("			,'' kari_saimoku ")
            .AppendLine("			,'' kari_keisiki ")
            .AppendLine("			,'' kari_youto ")
            .AppendLine("			,'YMP8' kari_tukekaesaki ")
            .AppendLine("			,'' kari_line ")
            .AppendLine("			,ms.skk_jigyousyo_cd kari_aitesaki ")
            .AppendLine("			,SUM(ISNULL(tu.uri_gaku,0)) kari_kingaku ")
            .AppendLine("			,'11' kasi_zei ")
            .AppendLine("			,'' kasi_kamoku_mei ")
            .AppendLine("			,'51195' kasi_kamoku ")
            .AppendLine("			,'777' kasi_saimoku ")
            .AppendLine("			,'' kasi_keisiki ")
            .AppendLine("			,'' kasi_youto ")
            .AppendLine("			,'YMP8' kasi_tukekaesaki ")
            .AppendLine("			,'' kasi_line ")
            .AppendLine("			,ms.skk_jigyousyo_cd kasi_aitesaki ")
            .AppendLine("			,SUM(ISNULL(tu.uri_gaku,0)) kasi_kingaku  ")
            .AppendLine("		FROM ")
            .AppendLine("			t_uriage_data tu  ")
            .AppendLine("		INNER JOIN  ")
            .AppendLine("			m_seikyuu_saki ms  ")
            .AppendLine("		ON  ")
            .AppendLine("			tu.seikyuu_saki_cd = ms.seikyuu_saki_cd  ")
            .AppendLine("		AND  ")
            .AppendLine("			tu.seikyuu_saki_brc = ms.seikyuu_saki_brc  ")
            .AppendLine("		AND  ")
            .AppendLine("			tu.seikyuu_saki_kbn = ms.seikyuu_saki_kbn  ")
            .AppendLine("		WHERE ")
            .AppendLine("			CONVERT(CHAR(10),CONVERT(DATETIME,ISNULL(tu.denpyou_uri_date,'1900/01/01')),111) BETWEEN CONVERT(CHAR(10),CONVERT(DATETIME,@fromDate),111) AND CONVERT(CHAR(10),CONVERT(DATETIME,@toDate),111)  ")
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.zei_kbn,0) > 0  ")
            .AppendLine("		AND  ")
            .AppendLine("			ms.skk_jigyousyo_cd IS NOT NULL  ")
            '2010/10/07 売上データは、売上処理FLG=1のみ対象とする 馬艶軍追加 ↓
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.uri_keijyou_flg,'0') = '1'  ")
            '2010/10/07 売上データは、売上処理FLG=1のみ対象とする 馬艶軍追加 ↑
            .AppendLine("		GROUP BY ")
            .AppendLine("			ms.skk_jigyousyo_cd  ")
            .AppendLine(" ")
            .AppendLine("	UNION ALL  ")
            .AppendLine(" ")
            .AppendLine("		SELECT ")
            .AppendLine("			@tekiyou tekiyou ")
            .AppendLine("			,'99' kari_zei ")
            .AppendLine("			,'' kari_kamoku_mei ")
            .AppendLine("			,'01518' kari_kamoku ")
            .AppendLine("			,'' kari_saimoku ")
            .AppendLine("			,'' kari_keisiki ")
            .AppendLine("			,'' kari_youto ")
            .AppendLine("			,'YMP8' kari_tukekaesaki ")
            .AppendLine("			,'' kari_line ")
            .AppendLine("			,ms.skk_jigyousyo_cd kari_aitesaki ")
            .AppendLine("			,SUM(ISNULL(tu.sotozei_gaku,0)) kari_kingaku ")
            .AppendLine("			,'77' kasi_zei ")
            .AppendLine("			,'' kasi_kamoku_mei ")
            .AppendLine("			,'24293' kasi_kamoku ")
            .AppendLine("			,'777' kasi_saimoku ")
            .AppendLine("			,'' kasi_keisiki ")
            .AppendLine("			,'' kasi_youto ")
            .AppendLine("			,'YMP8' kasi_tukekaesaki ")
            .AppendLine("			,'' kasi_line ")
            .AppendLine("			,'9999999999' kasi_aitesaki ")
            .AppendLine("			,SUM(ISNULL(tu.sotozei_gaku,0)) kasi_kingaku  ")
            .AppendLine("		FROM ")
            .AppendLine("			t_uriage_data tu  ")
            .AppendLine("		INNER JOIN  ")
            .AppendLine("			m_seikyuu_saki ms  ")
            .AppendLine("		ON  ")
            .AppendLine("			tu.seikyuu_saki_cd = ms.seikyuu_saki_cd  ")
            .AppendLine("		AND  ")
            .AppendLine("			tu.seikyuu_saki_brc = ms.seikyuu_saki_brc  ")
            .AppendLine("		AND  ")
            .AppendLine("			tu.seikyuu_saki_kbn = ms.seikyuu_saki_kbn  ")
            .AppendLine("		WHERE ")
            .AppendLine("			CONVERT(CHAR(10),CONVERT(DATETIME,ISNULL(tu.denpyou_uri_date,'1900/01/01')),111) BETWEEN CONVERT(CHAR(10),CONVERT(DATETIME,@fromDate),111) AND CONVERT(CHAR(10),CONVERT(DATETIME,@toDate),111)  ")
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.zei_kbn,0) > 0  ")
            .AppendLine("		AND  ")
            .AppendLine("			ms.skk_jigyousyo_cd IS NOT NULL  ")
            '2010/10/07 売上データは、売上処理FLG=1のみ対象とする 馬艶軍追加 ↓
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.uri_keijyou_flg,'0') = '1'  ")
            '2010/10/07 売上データは、売上処理FLG=1のみ対象とする 馬艶軍追加 ↑
            .AppendLine("		GROUP BY ")
            .AppendLine("			ms.skk_jigyousyo_cd  ")
            .AppendLine(" ")
            .AppendLine("	UNION ALL  ")
            .AppendLine(" ")
            .AppendLine("		SELECT ")
            .AppendLine("			@tekiyou tekiyou ")
            .AppendLine("			,'99' kari_zei ")
            .AppendLine("			,'' kari_kamoku_mei ")
            .AppendLine("			,'01518' kari_kamoku ")
            .AppendLine("			,'' kari_saimoku ")
            .AppendLine("			,'' kari_keisiki ")
            .AppendLine("			,'' kari_youto ")
            .AppendLine("			,'YMP8' kari_tukekaesaki ")
            .AppendLine("			,'' kari_line ")
            .AppendLine("			,'9999999999' kari_aitesaki ")
            .AppendLine("			,SUM(ISNULL(tu.uri_gaku,0)) kari_kingaku ")
            .AppendLine("			,'99' kasi_zei ")
            .AppendLine("			,'' kasi_kamoku_mei ")
            .AppendLine("			,'51195' kasi_kamoku ")
            .AppendLine("			,'777' kasi_saimoku ")
            .AppendLine("			,'' kasi_keisiki ")
            .AppendLine("			,'' kasi_youto ")
            .AppendLine("			,'YMP8' kasi_tukekaesaki ")
            .AppendLine("			,'' kasi_line ")
            .AppendLine("			,'9999999999' kasi_aitesaki ")
            .AppendLine("			,SUM(ISNULL(tu.uri_gaku,0)) kasi_kingaku  ")
            .AppendLine("		FROM ")
            .AppendLine("			t_uriage_data tu  ")
            .AppendLine("		INNER JOIN  ")
            .AppendLine("			m_seikyuu_saki ms  ")
            .AppendLine("		ON  ")
            .AppendLine("			tu.seikyuu_saki_cd = ms.seikyuu_saki_cd  ")
            .AppendLine("		AND  ")
            .AppendLine("			tu.seikyuu_saki_brc = ms.seikyuu_saki_brc  ")
            .AppendLine("		AND  ")
            .AppendLine("			tu.seikyuu_saki_kbn = ms.seikyuu_saki_kbn  ")
            .AppendLine("		WHERE ")
            .AppendLine("			CONVERT(CHAR(10),CONVERT(DATETIME,ISNULL(tu.denpyou_uri_date,'1900/01/01')),111) BETWEEN CONVERT(CHAR(10),CONVERT(DATETIME,@fromDate),111) AND CONVERT(CHAR(10),CONVERT(DATETIME,@toDate),111)  ")
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.zei_kbn,0) = 0  ")
            .AppendLine("		AND  ")
            .AppendLine("			ms.skk_jigyousyo_cd IS NULL  ")
            '2010/10/07 売上データは、売上処理FLG=1のみ対象とする 馬艶軍追加 ↓
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.uri_keijyou_flg,'0') = '1'  ")
            '2010/10/07 売上データは、売上処理FLG=1のみ対象とする 馬艶軍追加 ↑
            '.AppendLine("		GROUP BY ")
            '.AppendLine("			ms.skk_jigyousyo_cd ")
            .AppendLine(" ")
            .AppendLine("	UNION ALL  ")
            .AppendLine(" ")
            .AppendLine("		SELECT ")
            .AppendLine("			@tekiyou tekiyou ")
            .AppendLine("			,'99' kari_zei ")
            .AppendLine("			,'' kari_kamoku_mei ")
            .AppendLine("			,'01518' kari_kamoku ")
            .AppendLine("			,'' kari_saimoku ")
            .AppendLine("			,'' kari_keisiki ")
            .AppendLine("			,'' kari_youto ")
            .AppendLine("			,'YMP8' kari_tukekaesaki ")
            .AppendLine("			,'' kari_line ")
            .AppendLine("			,ms.skk_jigyousyo_cd kari_aitesaki ")
            .AppendLine("			,SUM(ISNULL(tu.uri_gaku,0)) kari_kingaku ")
            .AppendLine("			,'99' kasi_zei ")
            .AppendLine("			,'' kasi_kamoku_mei ")
            .AppendLine("			,'51195' kasi_kamoku ")
            .AppendLine("			,'777' kasi_saimoku ")
            .AppendLine("			,'' kasi_keisiki ")
            .AppendLine("			,'' kasi_youto ")
            .AppendLine("			,'YMP8' kasi_tukekaesaki ")
            .AppendLine("			,'' kasi_line ")
            .AppendLine("			,ms.skk_jigyousyo_cd kasi_aitesaki ")
            .AppendLine("			,SUM(ISNULL(tu.uri_gaku,0)) kasi_kingaku  ")
            .AppendLine("		FROM ")
            .AppendLine("			t_uriage_data tu  ")
            .AppendLine("		INNER JOIN  ")
            .AppendLine("			m_seikyuu_saki ms  ")
            .AppendLine("		ON  ")
            .AppendLine("			tu.seikyuu_saki_cd = ms.seikyuu_saki_cd  ")
            .AppendLine("		AND  ")
            .AppendLine("			tu.seikyuu_saki_brc = ms.seikyuu_saki_brc  ")
            .AppendLine("		AND  ")
            .AppendLine("			tu.seikyuu_saki_kbn = ms.seikyuu_saki_kbn  ")
            .AppendLine("		WHERE ")
            .AppendLine("			CONVERT(CHAR(10),CONVERT(DATETIME,ISNULL(tu.denpyou_uri_date,'1900/01/01')),111) BETWEEN CONVERT(CHAR(10),CONVERT(DATETIME,@fromDate),111) AND CONVERT(CHAR(10),CONVERT(DATETIME,@toDate),111)  ")
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.zei_kbn,0) = 0  ")
            .AppendLine("		AND  ")
            .AppendLine("			ms.skk_jigyousyo_cd IS NOT NULL  ")
            '2010/10/07 売上データは、売上処理FLG=1のみ対象とする 馬艶軍追加 ↓
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.uri_keijyou_flg,'0') = '1'  ")
            '2010/10/07 売上データは、売上処理FLG=1のみ対象とする 馬艶軍追加 ↑
            .AppendLine("		GROUP BY ")
            .AppendLine("			ms.skk_jigyousyo_cd ")
            .AppendLine("	) t  ")

            '貸方金額＜＞0
            .AppendLine("WHERE ")
            .AppendLine("   ISNULL(kasi_kingaku,0) <> 0 ")

            .AppendLine("ORDER BY ")
            .AppendLine("	t.kari_aitesaki ")
            .AppendLine("	,t.kasi_zei ")
        End With

        paramList.Add(MakeParam("@fromDate", SqlDbType.VarChar, 10, strDateFrom))
        paramList.Add(MakeParam("@toDate", SqlDbType.VarChar, 10, strDateTo))
        paramList.Add(MakeParam("@tekiyou", SqlDbType.VarChar, 255, "売上ＩＦ" & SetZenkakuSuuji(Right(strDateFrom, 5)) & "～" & SetZenkakuSuuji(Right(strDateTo, 5))))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.ExcelSiwakeUriageDataTable.TableName, paramList.ToArray)

        '戻る
        Return dsDataSet.ExcelSiwakeUriageDataTable
    End Function

    ''' <summary>
    ''' EXCEL仕訳仕入データ出力
    ''' </summary>
    ''' <param name="strDateFrom">抽出期間FROM</param>
    ''' <param name="strDateTo">抽出期間TO</param>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Public Function GetExcelSiwakeSiire(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.ExcelSiwakeSiireDataTableDataTable
        ' DataSetインスタンスの生成
        Dim dsDataSet As New ExcelSiwakeDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	tekiyou ")
            .AppendLine("	,kari_zei ")
            .AppendLine("	,kari_kamoku_mei ")
            .AppendLine("	,kari_kamoku ")
            .AppendLine("	,kari_saimoku ")
            .AppendLine("	,kari_keisiki ")
            .AppendLine("	,kari_youto ")
            .AppendLine("	,kari_tukekaesaki ")
            .AppendLine("	,kari_line ")
            .AppendLine("	,kari_aitesaki ")
            .AppendLine("	,kari_kingaku ")
            .AppendLine("	,kasi_zei ")
            .AppendLine("	,kasi_kamoku_mei ")
            .AppendLine("	,kasi_kamoku ")
            .AppendLine("	,kasi_saimoku ")
            .AppendLine("	,kasi_keisiki ")
            .AppendLine("	,kasi_youto ")
            .AppendLine("	,kasi_tukekaesaki ")
            .AppendLine("	,kasi_line ")
            .AppendLine("	,kasi_aitesaki ")
            .AppendLine("	,kasi_kingaku ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			@tekiyou tekiyou ")
            .AppendLine("			,'01' kari_zei ")
            .AppendLine("			,'' kari_kamoku_mei ")
            .AppendLine("			,'65286' kari_kamoku ")
            .AppendLine("			,'777' kari_saimoku ")
            .AppendLine("			,'' kari_keisiki ")
            .AppendLine("			,'' kari_youto ")
            .AppendLine("			,'YMP8' kari_tukekaesaki ")
            .AppendLine("			,'' kari_line ")
            .AppendLine("			,mts.skk_jigyousyo_cd + ")
            .AppendLine("			RIGHT (mts.skk_shri_saki_cd,4) kari_aitesaki ")
            .AppendLine("			,SUM(ISNULL(ts.siire_gaku,0)) kari_kingaku ")
            .AppendLine("			,'99' kasi_zei ")
            .AppendLine("			,'' kasi_kamoku_mei ")
            .AppendLine("			,'21202' kasi_kamoku ")
            .AppendLine("			,'920' kasi_saimoku ")
            .AppendLine("			,'' kasi_keisiki ")
            .AppendLine("			,'' kasi_youto ")
            .AppendLine("			,'YMP8' kasi_tukekaesaki ")
            .AppendLine("			,'' kasi_line ")
            .AppendLine("			,mts.skk_jigyousyo_cd + ")
            .AppendLine("			RIGHT (mts.skk_shri_saki_cd,4) kasi_aitesaki ")
            .AppendLine("			,SUM(ISNULL(ts.siire_gaku,0)) kasi_kingaku ")
            .AppendLine("		FROM ")
            .AppendLine("			t_siire_data ts ")
            .AppendLine("		INNER JOIN  ")
            .AppendLine("			m_tyousakaisya mt         --調査会社マスタ(仕入) ")
            .AppendLine("		ON  ")
            .AppendLine("			ts.tys_kaisya_cd = mt.tys_kaisya_cd ")
            .AppendLine("		AND  ")
            .AppendLine("			ts.tys_kaisya_jigyousyo_cd = mt.jigyousyo_cd ")
            .AppendLine("		INNER JOIN  ")
            .AppendLine("			m_tyousakaisya mts        --調査会社マスタ(支払:事業所コード=支払集計先事業所コード) ")
            .AppendLine("		ON  ")
            .AppendLine("			mt.tys_kaisya_cd = mts.tys_kaisya_cd ")

            '修正後で
            .AppendLine("		AND  ")
            .AppendLine("			mt.shri_jigyousyo_cd = mts.jigyousyo_cd ")

            .AppendLine("		AND  ")
            .AppendLine("			mts.jigyousyo_cd = mts.shri_jigyousyo_cd ")
            .AppendLine("		WHERE ")
            .AppendLine("			CONVERT(CHAR(10),CONVERT(DATETIME,ISNULL(ts.denpyou_siire_date,'1900/01/01')),111) BETWEEN CONVERT(CHAR(10),CONVERT(DATETIME,@fromDate),111) AND CONVERT(CHAR(10),CONVERT(DATETIME,@toDate),111)  ")
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(ts.zei_kbn,0) > 0 ")
            .AppendLine("		AND  ")
            .AppendLine("			mts.skk_jigyousyo_cd IS NOT NULL ")
            .AppendLine("		AND  ")
            .AppendLine("			mts.skk_shri_saki_cd IS NOT NULL ")
            '2010/10/07 仕入データは、仕入処理FLG=1のみ対象とする 馬艶軍追加 ↓
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(ts.siire_keijyou_flg,'0') = '1'  ")
            '2010/10/07 仕入データは、仕入処理FLG=1のみ対象とする 馬艶軍追加 ↑
            .AppendLine("		GROUP BY ")
            .AppendLine("			mts.skk_jigyousyo_cd + RIGHT (mts.skk_shri_saki_cd,4) ")
            .AppendLine("	UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			@tekiyou tekiyou ")
            .AppendLine("			,'77' kari_zei ")
            .AppendLine("			,'' kari_kamoku_mei ")
            .AppendLine("			,'03919' kari_kamoku ")
            .AppendLine("			,'777' kari_saimoku ")
            .AppendLine("			,'' kari_keisiki ")
            .AppendLine("			,'' kari_youto ")
            .AppendLine("			,'YMP8' kari_tukekaesaki ")
            .AppendLine("			,'' kari_line ")
            .AppendLine("			,'9999999999' kari_aitesaki ")
            .AppendLine("			,SUM(ISNULL(ts.sotozei_gaku,0)) kari_kingaku ")
            .AppendLine("			,'99' kasi_zei ")
            .AppendLine("			,'' kasi_kamoku_mei ")
            .AppendLine("			,'21202' kasi_kamoku ")
            .AppendLine("			,'930' kasi_saimoku ")
            .AppendLine("			,'' kasi_keisiki ")
            .AppendLine("			,'' kasi_youto ")
            .AppendLine("			,'YMP8' kasi_tukekaesaki ")
            .AppendLine("			,'' kasi_line ")
            .AppendLine("			,mts.skk_jigyousyo_cd + ")
            .AppendLine("			RIGHT (mts.skk_shri_saki_cd,4) kasi_aitesaki ")
            .AppendLine("			,SUM(ISNULL(ts.sotozei_gaku,0)) kasi_kingaku ")
            .AppendLine("		FROM ")
            .AppendLine("			t_siire_data ts ")
            .AppendLine("		INNER JOIN  ")
            .AppendLine("			m_tyousakaisya mt ")
            .AppendLine("		ON  ")
            .AppendLine("			ts.tys_kaisya_cd = mt.tys_kaisya_cd ")
            .AppendLine("		AND  ")
            .AppendLine("			ts.tys_kaisya_jigyousyo_cd = mt.jigyousyo_cd ")
            .AppendLine("		INNER JOIN  ")
            .AppendLine("			m_tyousakaisya mts ")
            .AppendLine("		ON  ")
            .AppendLine("			mt.tys_kaisya_cd = mts.tys_kaisya_cd ")

            '修正後で
            .AppendLine("		AND  ")
            .AppendLine("			mt.shri_jigyousyo_cd = mts.jigyousyo_cd ")

            .AppendLine("		AND  ")
            .AppendLine("			mts.jigyousyo_cd = mts.shri_jigyousyo_cd ")
            .AppendLine("		WHERE ")
            .AppendLine("			CONVERT(CHAR(10),CONVERT(DATETIME,ISNULL(ts.denpyou_siire_date,'1900/01/01')),111) BETWEEN CONVERT(CHAR(10),CONVERT(DATETIME,@fromDate),111) AND CONVERT(CHAR(10),CONVERT(DATETIME,@toDate),111)  ")
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(ts.zei_kbn,0) > 0 ")
            .AppendLine("		AND  ")
            .AppendLine("			mts.skk_jigyousyo_cd IS NOT NULL ")
            .AppendLine("		AND  ")
            .AppendLine("			mts.skk_shri_saki_cd IS NOT NULL ")
            '2010/10/07 仕入データは、仕入処理FLG=1のみ対象とする 馬艶軍追加 ↓
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(ts.siire_keijyou_flg,'0') = '1'  ")
            '2010/10/07 仕入データは、仕入処理FLG=1のみ対象とする 馬艶軍追加 ↑
            .AppendLine("		GROUP BY ")
            .AppendLine("			mts.skk_jigyousyo_cd + RIGHT (mts.skk_shri_saki_cd,4) ")
            .AppendLine("	UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			@tekiyou tekiyou ")
            .AppendLine("			,'99' kari_zei ")
            .AppendLine("			,'' kari_kamoku_mei ")
            .AppendLine("			,'65286' kari_kamoku ")
            .AppendLine("			,'888' kari_saimoku ")
            .AppendLine("			,'' kari_keisiki ")
            .AppendLine("			,'' kari_youto ")
            .AppendLine("			,'YMP8' kari_tukekaesaki ")
            .AppendLine("			,'' kari_line ")
            .AppendLine("			,mts.skk_jigyousyo_cd + ")
            .AppendLine("			RIGHT (mts.skk_shri_saki_cd,4) kari_aitesaki ")
            .AppendLine("			,SUM(ISNULL(ts.siire_gaku,0)) kari_kingaku ")
            .AppendLine("			,'99' kasi_zei ")
            .AppendLine("			,'' kasi_kamoku_mei ")
            .AppendLine("			,'21202' kasi_kamoku ")
            .AppendLine("			,'920' kasi_saimoku ")
            .AppendLine("			,'' kasi_keisiki ")
            .AppendLine("			,'' kasi_youto ")
            .AppendLine("			,'YMP8' kasi_tukekaesaki ")
            .AppendLine("			,'' kasi_line ")
            .AppendLine("			,mts.skk_jigyousyo_cd + ")
            .AppendLine("			RIGHT (mts.skk_shri_saki_cd,4) kasi_aitesaki ")
            .AppendLine("			,SUM(ISNULL(ts.siire_gaku,0)) kasi_kingaku ")
            .AppendLine("		FROM ")
            .AppendLine("			t_siire_data ts ")
            .AppendLine("		INNER JOIN  ")
            .AppendLine("			m_tyousakaisya mt ")
            .AppendLine("		ON  ")
            .AppendLine("			ts.tys_kaisya_cd = mt.tys_kaisya_cd ")
            .AppendLine("		AND  ")
            .AppendLine("			ts.tys_kaisya_jigyousyo_cd = mt.jigyousyo_cd ")
            .AppendLine("		INNER JOIN  ")
            .AppendLine("			m_tyousakaisya mts ")
            .AppendLine("		ON  ")
            .AppendLine("			mt.tys_kaisya_cd = mts.tys_kaisya_cd ")

            '修正後で
            .AppendLine("		AND  ")
            .AppendLine("			mt.shri_jigyousyo_cd = mts.jigyousyo_cd ")

            .AppendLine("		AND  ")
            .AppendLine("			mts.jigyousyo_cd = mts.shri_jigyousyo_cd ")
            .AppendLine("		WHERE ")
            .AppendLine("			CONVERT(CHAR(10),CONVERT(DATETIME,ISNULL(ts.denpyou_siire_date,'1900/01/01')),111) BETWEEN CONVERT(CHAR(10),CONVERT(DATETIME,@fromDate),111) AND CONVERT(CHAR(10),CONVERT(DATETIME,@toDate),111)  ")
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(ts.zei_kbn,0) = 0 ")
            .AppendLine("		AND  ")
            .AppendLine("			mts.skk_jigyousyo_cd IS NOT NULL ")
            .AppendLine("		AND  ")
            .AppendLine("			mts.skk_shri_saki_cd IS NOT NULL ")
            '2010/10/07 仕入データは、仕入処理FLG=1のみ対象とする 馬艶軍追加 ↓
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(ts.siire_keijyou_flg,'0') = '1'  ")
            '2010/10/07 仕入データは、仕入処理FLG=1のみ対象とする 馬艶軍追加 ↑
            .AppendLine("		GROUP BY ")
            .AppendLine("		mts.skk_jigyousyo_cd + RIGHT (mts.skk_shri_saki_cd,4) ")
            .AppendLine(") t ")

            '貸方金額＜＞0
            .AppendLine("WHERE ")
            .AppendLine("   kasi_kingaku <> 0 ")

            .AppendLine("ORDER BY ")
            .AppendLine("	t.kasi_aitesaki ")
            .AppendLine("	,t.kari_zei ")
        End With

        paramList.Add(MakeParam("@fromDate", SqlDbType.VarChar, 10, strDateFrom))
        paramList.Add(MakeParam("@toDate", SqlDbType.VarChar, 10, strDateTo))
        paramList.Add(MakeParam("@tekiyou", SqlDbType.VarChar, 255, "仕入ＩＦ" & SetZenkakuSuuji(Right(strDateFrom, 5)) & "～" & SetZenkakuSuuji(Right(strDateTo, 5))))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.ExcelSiwakeSiireDataTable.TableName, paramList.ToArray)

        '戻る
        Return dsDataSet.ExcelSiwakeSiireDataTable
    End Function

    ''' <summary>
    ''' EXCEL仕訳入金データ出力
    ''' </summary>
    ''' <param name="strDateFrom">抽出期間FROM</param>
    ''' <param name="strDateTo">抽出期間TO</param>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Public Function GetExcelSiwakeNyuukin(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.ExcelSiwakeNyuukinDataTableDataTable
        ' DataSetインスタンスの生成
        Dim dsDataSet As New ExcelSiwakeDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            '.AppendLine("SELECT ")
            '.AppendLine("	tekiyou ")
            '.AppendLine("	,kari_zei ")
            '.AppendLine("	,kari_kamoku_mei ")
            '.AppendLine("	,kari_kamoku ")
            '.AppendLine("	,kari_saimoku ")
            '.AppendLine("	,kari_keisiki ")
            '.AppendLine("	,kari_youto ")
            '.AppendLine("	,kari_tukekaesaki ")
            '.AppendLine("	,kari_line ")
            '.AppendLine("	,kari_aitesaki ")
            '.AppendLine("	,kari_kingaku ")
            '.AppendLine("	,kasi_zei ")
            '.AppendLine("	,kasi_kamoku_mei ")
            '.AppendLine("	,kasi_kamoku ")
            '.AppendLine("	,kasi_saimoku ")
            '.AppendLine("	,kasi_keisiki ")
            '.AppendLine("	,kasi_youto ")
            '.AppendLine("	,kasi_tukekaesaki ")
            '.AppendLine("	,kasi_line ")
            '.AppendLine("	,kasi_aitesaki ")
            '.AppendLine("	,kasi_kingaku ")
            '.AppendLine("FROM ")
            '.AppendLine("	( ")
            '.AppendLine("		SELECT ")
            '.AppendLine("			@tekiyou tekiyou ")
            '.AppendLine("			,'99' kari_zei ")
            '.AppendLine("			,'' kari_kamoku_mei ")
            '.AppendLine("			,tn.kamoku kari_kamoku ")
            '.AppendLine("			,tn.saimoku kari_saimoku ")
            '.AppendLine("			,'' kari_keisiki ")
            '.AppendLine("			,'' kari_youto ")
            '.AppendLine("			,'YMP8' kari_tukekaesaki ")
            '.AppendLine("			,'' kari_line ")
            '.AppendLine("			,'9999999999' kari_aitesaki ")
            '.AppendLine("			,SUM(ISNULL(tn.kingaku,0)) kari_kingaku ")
            '.AppendLine("			,'99' kasi_zei ")
            '.AppendLine("			,'' kasi_kamoku_mei ")
            '.AppendLine("			,'01518' kasi_kamoku ")
            '.AppendLine("			,'' kasi_saimoku ")
            '.AppendLine("			,'' kasi_keisiki ")
            '.AppendLine("			,'' kasi_youto ")
            '.AppendLine("			,'YMP8' kasi_tukekaesaki ")
            '.AppendLine("			,'' kasi_line ")
            '.AppendLine("			,'9999999999' kasi_aitesaki ")
            '.AppendLine("			,SUM(ISNULL(tn.kingaku,0)) kasi_kingaku ")
            '.AppendLine("		FROM ")
            '.AppendLine("			( ")
            '.AppendLine("				SELECT ")
            '.AppendLine("					seikyuu_saki_cd ")
            '.AppendLine("					,seikyuu_saki_brc ")
            '.AppendLine("					,seikyuu_saki_kbn ")
            '.AppendLine("					,nyuukin_date ")
            '.AppendLine("					,genkin kingaku ")
            '.AppendLine("					,'01116' kamoku ")
            '.AppendLine("					,'' saimoku ")
            '.AppendLine("				FROM ")
            '.AppendLine("					t_nyuukin_data ")
            '.AppendLine("			UNION ALL ")
            '.AppendLine("				SELECT ")
            '.AppendLine("					seikyuu_saki_cd ")
            '.AppendLine("					,seikyuu_saki_brc ")
            '.AppendLine("					,seikyuu_saki_kbn ")
            '.AppendLine("					,nyuukin_date ")
            '.AppendLine("					,kogitte ")
            '.AppendLine("					,'01168' kamoku ")
            '.AppendLine("					,'' saimoku ")
            '.AppendLine("				FROM ")
            '.AppendLine("					t_nyuukin_data ")
            '.AppendLine("			UNION ALL ")
            '.AppendLine("				SELECT ")
            '.AppendLine("					seikyuu_saki_cd ")
            '.AppendLine("					,seikyuu_saki_brc ")
            '.AppendLine("					,seikyuu_saki_kbn ")
            '.AppendLine("					,nyuukin_date ")
            '.AppendLine("					,ISNULL(furikomi,0) + ISNULL(kouza_furikae,0) ")
            '.AppendLine("					,'01228' kamoku ")
            '.AppendLine("					,'' saimoku ")
            '.AppendLine("				FROM ")
            '.AppendLine("					t_nyuukin_data ")
            '.AppendLine("			UNION ALL ")
            '.AppendLine("				SELECT ")
            '.AppendLine("					seikyuu_saki_cd ")
            '.AppendLine("					,seikyuu_saki_brc ")
            '.AppendLine("					,seikyuu_saki_kbn ")
            '.AppendLine("					,nyuukin_date ")
            '.AppendLine("					,tegata ")
            '.AppendLine("					,'01317' kamoku ")
            '.AppendLine("					,'' saimoku ")
            '.AppendLine("				FROM ")
            '.AppendLine("					t_nyuukin_data ")
            '.AppendLine("			UNION ALL ")
            '.AppendLine("				SELECT ")
            '.AppendLine("					seikyuu_saki_cd ")
            '.AppendLine("					,seikyuu_saki_brc ")
            '.AppendLine("					,seikyuu_saki_kbn ")
            '.AppendLine("					,nyuukin_date ")
            '.AppendLine("					,ISNULL(sousai,0) + ISNULL(kyouryoku_kaihi,0) + ISNULL(nebiki,0) + ISNULL(sonota,0) ")
            '.AppendLine("					,'49749' kamoku ")
            '.AppendLine("					,'' saimoku ")
            '.AppendLine("				FROM ")
            '.AppendLine("					t_nyuukin_data ")
            '.AppendLine("			UNION ALL ")
            '.AppendLine("				SELECT ")
            '.AppendLine("					seikyuu_saki_cd ")
            '.AppendLine("					,seikyuu_saki_brc ")
            '.AppendLine("					,seikyuu_saki_kbn ")
            '.AppendLine("					,nyuukin_date ")
            '.AppendLine("					,furikomi_tesuuryou ")
            '.AppendLine("					,'22101' kamoku ")
            '.AppendLine("					,'777' saimoku ")
            '.AppendLine("				FROM ")
            '.AppendLine("					t_nyuukin_data ")
            '.AppendLine("			) tn    --入金種別毎に科目、細目を設定した状態でSELECTし、統合(UNION ALL) ")
            '.AppendLine("		INNER JOIN  ")
            '.AppendLine("			m_seikyuu_saki ms ")
            '.AppendLine("		ON  ")
            '.AppendLine("			tn.seikyuu_saki_cd = ms.seikyuu_saki_cd ")
            '.AppendLine("		AND  ")
            '.AppendLine("			tn.seikyuu_saki_brc = ms.seikyuu_saki_brc ")
            '.AppendLine("		AND  ")
            '.AppendLine("			tn.seikyuu_saki_kbn = ms.seikyuu_saki_kbn ")
            '.AppendLine("		WHERE ")
            '.AppendLine("			CONVERT(CHAR(10),CONVERT(DATETIME,ISNULL(tn.nyuukin_date,'1900/01/01')),111) BETWEEN CONVERT(CHAR(10),CONVERT(DATETIME,@fromDate),111) AND CONVERT(CHAR(10),CONVERT(DATETIME,@toDate),111)  ")
            '.AppendLine("		AND  ")
            '.AppendLine("			ms.skk_jigyousyo_cd IS NULL ")
            '.AppendLine("		GROUP BY ")
            '.AppendLine("			tn.kamoku ")
            '.AppendLine("			,tn.saimoku ")
            ''.AppendLine("			,ms.skk_jigyousyo_cd ")
            '.AppendLine("	UNION ALL ")
            '.AppendLine("		SELECT ")
            '.AppendLine("			@tekiyou tekiyou ")
            '.AppendLine("			,'99' kari_zei ")
            '.AppendLine("			,'' kari_kamoku_mei ")
            '.AppendLine("			,tn.kamoku kari_kamoku ")
            '.AppendLine("			,tn.saimoku kari_saimoku ")
            '.AppendLine("			,'' kari_keisiki ")
            '.AppendLine("			,'' kari_youto ")
            '.AppendLine("			,'YMP8' kari_tukekaesaki ")
            '.AppendLine("			,'' kari_line ")
            '.AppendLine("			,ms.skk_jigyousyo_cd kari_aitesaki ")
            '.AppendLine("			,SUM(ISNULL(tn.kingaku,0)) kari_kingaku ")
            '.AppendLine("			,'99' kasi_zei ")
            '.AppendLine("			,'' kasi_kamoku_mei ")
            '.AppendLine("			,'01518' kasi_kamoku ")
            '.AppendLine("			,'' kasi_saimoku ")
            '.AppendLine("			,'' kasi_keisiki ")
            '.AppendLine("			,'' kasi_youto ")
            '.AppendLine("			,'YMP8' kasi_tukekaesaki ")
            '.AppendLine("			,'' kasi_line ")
            '.AppendLine("			,ms.skk_jigyousyo_cd kasi_aitesaki ")
            '.AppendLine("			,SUM(ISNULL(tn.kingaku,0)) kasi_kingaku ")
            '.AppendLine("		FROM ")
            '.AppendLine("			( ")
            '.AppendLine("				SELECT ")
            '.AppendLine("					seikyuu_saki_cd ")
            '.AppendLine("					,seikyuu_saki_brc ")
            '.AppendLine("					,seikyuu_saki_kbn ")
            '.AppendLine("					,nyuukin_date ")
            '.AppendLine("					,genkin kingaku ")
            '.AppendLine("					,'01116' kamoku ")
            '.AppendLine("					,'' saimoku ")
            '.AppendLine("				FROM ")
            '.AppendLine("					t_nyuukin_data ")
            '.AppendLine("			UNION ALL ")
            '.AppendLine("				SELECT ")
            '.AppendLine("					seikyuu_saki_cd ")
            '.AppendLine("					,seikyuu_saki_brc ")
            '.AppendLine("					,seikyuu_saki_kbn ")
            '.AppendLine("					,nyuukin_date ")
            '.AppendLine("					,kogitte ")
            '.AppendLine("					,'01168' kamoku ")
            '.AppendLine("					,'' saimoku ")
            '.AppendLine("				FROM ")
            '.AppendLine("					t_nyuukin_data ")
            '.AppendLine("			UNION ALL ")
            '.AppendLine("				SELECT ")
            '.AppendLine("					seikyuu_saki_cd ")
            '.AppendLine("					,seikyuu_saki_brc ")
            '.AppendLine("					,seikyuu_saki_kbn ")
            '.AppendLine("					,nyuukin_date ")
            '.AppendLine("					,ISNULL(furikomi,0) + ISNULL(kouza_furikae,0) ")
            '.AppendLine("					,'01228' kamoku ")
            '.AppendLine("					,'' saimoku ")
            '.AppendLine("				FROM ")
            '.AppendLine("					t_nyuukin_data ")
            '.AppendLine("			UNION ALL ")
            '.AppendLine("				SELECT ")
            '.AppendLine("					seikyuu_saki_cd ")
            '.AppendLine("					,seikyuu_saki_brc ")
            '.AppendLine("					,seikyuu_saki_kbn ")
            '.AppendLine("					,nyuukin_date ")
            '.AppendLine("					,tegata ")
            '.AppendLine("					,'01317' kamoku ")
            '.AppendLine("					,'' saimoku ")
            '.AppendLine("				FROM ")
            '.AppendLine("					t_nyuukin_data ")
            '.AppendLine("			UNION ALL ")
            '.AppendLine("				SELECT ")
            '.AppendLine("					seikyuu_saki_cd ")
            '.AppendLine("					,seikyuu_saki_brc ")
            '.AppendLine("					,seikyuu_saki_kbn ")
            '.AppendLine("					,nyuukin_date ")
            '.AppendLine("					,ISNULL(sousai,0) + ISNULL(kyouryoku_kaihi,0) + ISNULL(nebiki,0) + ISNULL(sonota,0) ")
            '.AppendLine("					,'49749' kamoku ")
            '.AppendLine("					,'' saimoku ")
            '.AppendLine("				FROM ")
            '.AppendLine("					t_nyuukin_data ")
            '.AppendLine("			UNION ALL ")
            '.AppendLine("				SELECT ")
            '.AppendLine("					seikyuu_saki_cd ")
            '.AppendLine("					,seikyuu_saki_brc ")
            '.AppendLine("					,seikyuu_saki_kbn ")
            '.AppendLine("					,nyuukin_date ")
            '.AppendLine("					,furikomi_tesuuryou ")
            '.AppendLine("					,'22101' kamoku ")
            '.AppendLine("					,'777' saimoku ")
            '.AppendLine("				FROM ")
            '.AppendLine("					t_nyuukin_data ")
            '.AppendLine("			) tn ")
            '.AppendLine("		INNER JOIN  ")
            '.AppendLine("			m_seikyuu_saki ms ")
            '.AppendLine("		ON  ")
            '.AppendLine("			tn.seikyuu_saki_cd = ms.seikyuu_saki_cd ")
            '.AppendLine("		AND  ")
            '.AppendLine("			tn.seikyuu_saki_brc = ms.seikyuu_saki_brc ")
            '.AppendLine("		AND  ")
            '.AppendLine("			tn.seikyuu_saki_kbn = ms.seikyuu_saki_kbn ")
            '.AppendLine("		WHERE ")
            '.AppendLine("			CONVERT(CHAR(10),CONVERT(DATETIME,ISNULL(tn.nyuukin_date,'1900/01/01')),111) BETWEEN CONVERT(CHAR(10),CONVERT(DATETIME,@fromDate),111) AND CONVERT(CHAR(10),CONVERT(DATETIME,@toDate),111)  ")
            '.AppendLine("		AND  ")
            '.AppendLine("			ms.skk_jigyousyo_cd IS NOT NULL ")
            '.AppendLine("		GROUP BY ")
            '.AppendLine("			tn.kamoku ")
            '.AppendLine("			,tn.saimoku ")
            '.AppendLine("			,ms.skk_jigyousyo_cd ")
            '.AppendLine("		) t ")

            ''貸方金額＜＞０
            '.AppendLine("WHERE ")
            '.AppendLine("   kasi_kingaku <> 0 ")

            '.AppendLine("ORDER BY ")
            '.AppendLine("	t.kari_aitesaki ")
            '.AppendLine("	,t.kari_kamoku ")
            .AppendLine("SELECT	 ")
            .AppendLine("    * 	 ")
            .AppendLine("FROM	 ")
            .AppendLine("    ( 	 ")
            .AppendLine("        SELECT	 ")
            .AppendLine("            @tekiyou tekiyou	 ")
            .AppendLine("            , tn.kari_zei	 ")
            .AppendLine("            , tn.kari_kamoku_mei	 ")
            .AppendLine("            , tn.kamoku kari_kamoku	 ")
            .AppendLine("            , tn.saimoku kari_saimoku	 ")
            .AppendLine("            , '' kari_keisiki	 ")
            .AppendLine("            , '' kari_youto	 ")
            .AppendLine("            , 'YMP8' kari_tukekaesaki	 ")
            .AppendLine("            , '' kari_line	 ")
            .AppendLine("            , kari_aitesaki	 ")
            .AppendLine("            , sum(tn.kingaku) kari_kingaku	 ")
            .AppendLine("            , '99' kasi_zei	 ")
            .AppendLine("            , '売掛金' kasi_kamoku_mei	 ")
            .AppendLine("            , '01518' kasi_kamoku	 ")
            .AppendLine("            , '' kasi_saimoku	 ")
            .AppendLine("            , '' kasi_keisiki	 ")
            .AppendLine("            , '' kasi_youto	 ")
            .AppendLine("            , 'YMP8' kasi_tukekaesaki	 ")
            .AppendLine("            , '' kasi_line	 ")
            .AppendLine("            , '9999999999' kasi_aitesaki	 ")
            .AppendLine("            , sum(tn.kingaku) kasi_kingaku 	 ")
            .AppendLine("        FROM	 ")
            .AppendLine("            ( 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '99' kari_zei	 ")
            .AppendLine("                    , '現金' kari_kamoku_mei	 ")
            .AppendLine("                    , '9999999999' kari_aitesaki	 ")
            .AppendLine("                    , genkin kingaku	 ")
            .AppendLine("                    , '01116' kamoku	 ")
            .AppendLine("                    , '' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data 	 ")
            .AppendLine("                UNION ALL 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '99' kari_zei	 ")
            .AppendLine("                    , '小切手' kari_kamoku_mei	 ")
            .AppendLine("                    , '9999999999' kari_aitesaki	 ")
            .AppendLine("                    , kogitte kingaku	 ")
            .AppendLine("                    , '01168' kamoku	 ")
            .AppendLine("                    , '' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data 	 ")
            .AppendLine("                UNION ALL 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '99' kari_zei	 ")
            .AppendLine("                    , '普通預金' kari_kamoku_mei	 ")
            .AppendLine("                    , 'YMP83959' kari_aitesaki	 ")
            .AppendLine("                    , furikomi + kouza_furikae kingaku	 ")
            .AppendLine("                    , '01228' kamoku	 ")
            .AppendLine("                    , '' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data 	 ")
            .AppendLine("                UNION ALL 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '99' kari_zei	 ")
            .AppendLine("                    , '受取手形（売掛金）' kari_kamoku_mei	 ")
            .AppendLine("                    , '9999999999' kari_aitesaki	 ")
            .AppendLine("                    , tegata kingaku	 ")
            .AppendLine("                    , '01317' kamoku	 ")
            .AppendLine("                    , '' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data 	 ")
            .AppendLine("                UNION ALL 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '01' kari_zei	 ")
            .AppendLine("                    , 'Ｈ－雑費' kari_kamoku_mei	 ")
            .AppendLine("                    , '9999999999' kari_aitesaki	 ")
            .AppendLine("                    , sousai + kyouryoku_kaihi + nebiki + sonota-round(sousai + kyouryoku_kaihi + nebiki + sonota-(sousai + kyouryoku_kaihi + nebiki + sonota)/1.05,0,1) kingaku	 ")
            .AppendLine("                    , '71690' kamoku	 ")
            .AppendLine("                    , '777' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data 	 ")
            .AppendLine("                UNION ALL 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '77' kari_zei	 ")
            .AppendLine("                    , '仮払消費税' kari_kamoku_mei	 ")
            .AppendLine("                    , '9999999999' kari_aitesaki	 ")
            .AppendLine("                    , round(sousai + kyouryoku_kaihi + nebiki + sonota-(sousai + kyouryoku_kaihi + nebiki + sonota)/1.05,0,1) kingaku	 ")
            .AppendLine("                    , '03919' kamoku	 ")
            .AppendLine("                    , '777' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data 	 ")
            .AppendLine("                UNION ALL 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '01' kari_zei	 ")
            .AppendLine("                    , 'Ｈ－通信費' kari_kamoku_mei	 ")
            .AppendLine("                    , '9999999999' kari_aitesaki	 ")
            .AppendLine("                    , furikomi_tesuuryou-round(furikomi_tesuuryou-furikomi_tesuuryou/1.05,0,1) kingaku	 ")
            .AppendLine("                    , '71335' kamoku	 ")
            .AppendLine("                    , '002' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data 	 ")
            .AppendLine("                UNION ALL 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '77' kari_zei	 ")
            .AppendLine("                    , '仮払消費税' kari_kamoku_mei	 ")
            .AppendLine("                    , '9999999999' kari_aitesaki	 ")
            .AppendLine("                    , round(furikomi_tesuuryou-furikomi_tesuuryou/1.05,0,1) kingaku	 ")
            .AppendLine("                    , '03919' kamoku	 ")
            .AppendLine("                    , '777' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data	 ")
            .AppendLine("            ) tn    --入金種別毎に科目、細目を設定した状態でSELECTし、統合(UNION ALL)	 ")
            .AppendLine("            INNER JOIN " & connDBUser & "m_seikyuu_saki ms 	 ")
            .AppendLine("                ON tn.seikyuu_saki_cd = ms.seikyuu_saki_cd 	 ")
            .AppendLine("                AND tn.seikyuu_saki_brc = ms.seikyuu_saki_brc 	 ")
            .AppendLine("                AND tn.seikyuu_saki_kbn = ms.seikyuu_saki_kbn 	 ")
            .AppendLine("        WHERE	 ")
            .AppendLine("            tn.nyuukin_date BETWEEN @fromDate AND @toDate 	 ")
            .AppendLine("            AND ms.skk_jigyousyo_cd IS NULL 	 ")
            .AppendLine("        GROUP BY	 ")
            .AppendLine("            tn.kari_zei	 ")
            .AppendLine("            , tn.kamoku	 ")
            .AppendLine("            , tn.saimoku 	 ")
            .AppendLine("            , tn.kari_kamoku_mei	 ")
            .AppendLine("            , tn.kari_aitesaki	 ")
            .AppendLine("        UNION ALL 	 ")
            .AppendLine("        SELECT	 ")
            .AppendLine("            @tekiyou tekiyou	 ")
            .AppendLine("            , tn.kari_zei	 ")
            .AppendLine("            , tn.kari_kamoku_mei	 ")
            .AppendLine("            , tn.kamoku kari_kamoku	 ")
            .AppendLine("            , tn.saimoku kari_saimoku	 ")
            .AppendLine("            , '' kari_keisiki	 ")
            .AppendLine("            , '' kari_youto	 ")
            .AppendLine("            , 'YMP8' kari_tukekaesaki	 ")
            .AppendLine("            , '' kari_line	 ")
            .AppendLine("            , kari_aitesaki	 ")
            .AppendLine("            , sum(tn.kingaku) kari_kingaku	 ")
            .AppendLine("            , '99' kasi_zei	 ")
            .AppendLine("            , '売掛金' kasi_kamoku_mei	 ")
            .AppendLine("            , '01518' kasi_kamoku	 ")
            .AppendLine("            , '' kasi_saimoku	 ")
            .AppendLine("            , '' kasi_keisiki	 ")
            .AppendLine("            , '' kasi_youto	 ")
            .AppendLine("            , 'YMP8' kasi_tukekaesaki	 ")
            .AppendLine("            , '' kasi_line	 ")
            .AppendLine("            , ms.skk_jigyousyo_cd kasi_aitesaki	 ")
            .AppendLine("            , sum(tn.kingaku) kasi_kingaku 	 ")
            .AppendLine("        FROM	 ")
            .AppendLine("            ( 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '99' kari_zei	 ")
            .AppendLine("                    , '現金' kari_kamoku_mei	 ")
            .AppendLine("                    , '9999999999' kari_aitesaki	 ")
            .AppendLine("                    , genkin kingaku	 ")
            .AppendLine("                    , '01116' kamoku	 ")
            .AppendLine("                    , '' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data 	 ")
            .AppendLine("                UNION ALL 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '99' kari_zei	 ")
            .AppendLine("                    , '小切手' kari_kamoku_mei	 ")
            .AppendLine("                    , '9999999999' kari_aitesaki	 ")
            .AppendLine("                    , kogitte kingaku	 ")
            .AppendLine("                    , '01168' kamoku	 ")
            .AppendLine("                    , '' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data 	 ")
            .AppendLine("                UNION ALL 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '99' kari_zei	 ")
            .AppendLine("                    , '普通預金' kari_kamoku_mei	 ")
            .AppendLine("                    , 'YMP83959' kari_aitesaki	 ")
            .AppendLine("                    , furikomi + kouza_furikae kingaku	 ")
            .AppendLine("                    , '01228' kamoku	 ")
            .AppendLine("                    , '' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data 	 ")
            .AppendLine("                UNION ALL 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '99' kari_zei	 ")
            .AppendLine("                    , '受取手形（売掛金）' kari_kamoku_mei	 ")
            .AppendLine("                    , '9999999999' kari_aitesaki	 ")
            .AppendLine("                    , tegata kingaku	 ")
            .AppendLine("                    , '01317' kamoku	 ")
            .AppendLine("                    , '' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data 	 ")
            .AppendLine("                UNION ALL 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '01' kari_zei	 ")
            .AppendLine("                    , 'Ｈ－雑費' kari_kamoku_mei	 ")
            .AppendLine("                    , '9999999999' kari_aitesaki	 ")
            .AppendLine("                    , sousai + kyouryoku_kaihi + nebiki + sonota-round(sousai + kyouryoku_kaihi + nebiki + sonota-(sousai + kyouryoku_kaihi + nebiki + sonota)/1.05,0,1) kingaku	 ")
            .AppendLine("                    , '71690' kamoku	 ")
            .AppendLine("                    , '777' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data 	 ")
            .AppendLine("                UNION ALL 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '77' kari_zei	 ")
            .AppendLine("                    , '仮払消費税' kari_kamoku_mei	 ")
            .AppendLine("                    , '9999999999' kari_aitesaki	 ")
            .AppendLine("                    , round(sousai + kyouryoku_kaihi + nebiki + sonota-(sousai + kyouryoku_kaihi + nebiki + sonota)/1.05,0,1) kingaku	 ")
            .AppendLine("                    , '03919' kamoku	 ")
            .AppendLine("                    , '777' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data 	 ")
            .AppendLine("                UNION ALL 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '01' kari_zei	 ")
            .AppendLine("                    , 'Ｈ－通信費' kari_kamoku_mei	 ")
            .AppendLine("                    , '9999999999' kari_aitesaki	 ")
            .AppendLine("                    , furikomi_tesuuryou-round(furikomi_tesuuryou/1.05-furikomi_tesuuryou/1.05,0,1) kingaku	 ")
            .AppendLine("                    , '71335' kamoku	 ")
            .AppendLine("                    , '002' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data	 ")
            .AppendLine("                UNION ALL 	 ")
            .AppendLine("                SELECT	 ")
            .AppendLine("                    seikyuu_saki_cd	 ")
            .AppendLine("                    , seikyuu_saki_brc	 ")
            .AppendLine("                    , seikyuu_saki_kbn	 ")
            .AppendLine("                    , nyuukin_date	 ")
            .AppendLine("                    , '77' kari_zei	 ")
            .AppendLine("                    , '仮払消費税' kari_kamoku_mei	 ")
            .AppendLine("                    , '9999999999' kari_aitesaki	 ")
            .AppendLine("                    , round(furikomi_tesuuryou/1.05-furikomi_tesuuryou/1.05,0,1) kingaku	 ")
            .AppendLine("                    , '03919' kamoku	 ")
            .AppendLine("                    , '777' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data	 ")
            .AppendLine("            ) tn 	 ")
            .AppendLine("            INNER JOIN " & connDBUser & "m_seikyuu_saki ms 	 ")
            .AppendLine("                ON tn.seikyuu_saki_cd = ms.seikyuu_saki_cd 	 ")
            .AppendLine("                AND tn.seikyuu_saki_brc = ms.seikyuu_saki_brc 	 ")
            .AppendLine("                AND tn.seikyuu_saki_kbn = ms.seikyuu_saki_kbn 	 ")
            .AppendLine("        WHERE	 ")
            .AppendLine("            tn.nyuukin_date BETWEEN @fromDate AND @toDate 	 ")
            .AppendLine("            AND ms.skk_jigyousyo_cd IS NOT NULL 	 ")
            .AppendLine("        GROUP BY	 ")
            .AppendLine("            tn.kari_zei	 ")
            .AppendLine("            , tn.kamoku	 ")
            .AppendLine("            , tn.saimoku	 ")
            .AppendLine("            , kari_kamoku_mei	 ")
            .AppendLine("            , ms.skk_jigyousyo_cd	 ")
            .AppendLine("            , tn.kari_aitesaki	 ")
            .AppendLine("    ) t 	 ")
            .AppendLine("WHERE kasi_kingaku <> 0	 ")
            .AppendLine("ORDER BY	 ")
            .AppendLine("    t.kari_saimoku	 ")
            .AppendLine("    , t.kari_zei	 ")
            .AppendLine("    , t.kari_kamoku	 ")
            .AppendLine("    , t.kasi_aitesaki	 ")
        End With

        paramList.Add(MakeParam("@fromDate", SqlDbType.VarChar, 10, strDateFrom))
        paramList.Add(MakeParam("@toDate", SqlDbType.VarChar, 10, strDateTo))
        paramList.Add(MakeParam("@tekiyou", SqlDbType.VarChar, 255, "入金ＩＦ" & SetZenkakuSuuji(Right(strDateFrom, 5)) & "～" & SetZenkakuSuuji(Right(strDateTo, 5))))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.ExcelSiwakeNyuukinDataTable.TableName, paramList.ToArray)

        '戻る
        Return dsDataSet.ExcelSiwakeNyuukinDataTable
    End Function

    ''' <summary>
    ''' 売掛金残高表
    ''' </summary>
    ''' <param name="strDateFrom">抽出期間FROM</param>
    ''' <param name="strDateTo">抽出期間TO</param>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Public Function GetUrikakekinZandakaHyou(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.UrikakekinZandakaHyouDataTable
        ' DataSetインスタンスの生成
        Dim dsDataSet As New ExcelSiwakeDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	TBL3.datakbn, ")
            .AppendLine("	TBL3.tokuisaki_cd, ")
            .AppendLine("	TBL3.tokuisaki_mei1, ")
            .AppendLine("	TBL3.tokuisaki_mei2, ")
            .AppendLine("	--繰越残高 ")
            .AppendLine("	TBL3.kurikosi_zanndaka, ")
            .AppendLine("	--現金・振込 ")
            .AppendLine("	TBL3.genkin_furikomi, ")
            .AppendLine("	--手形 ")
            .AppendLine("	TBL3.tegata, ")
            .AppendLine("	--相殺他 ")
            .AppendLine("	TBL3.sousaihoka, ")
            .AppendLine("	--入金合計 ")
            .AppendLine("	TBL3.nyuukin_goukei, ")
            .AppendLine("	--未回収残高 ")
            .AppendLine("	TBL3.mikaisyuu_zanndaka, ")
            .AppendLine("	--売上高 ")
            .AppendLine("	TBL3.uriagedaka, ")
            .AppendLine("	--消費税等 ")
            .AppendLine("	TBL3.syouhizeinado, ")
            .AppendLine("	--差引残高 ")
            .AppendLine("	TBL3.sasihiki_zanndaka, ")
            .AppendLine("	--手形残高 ")
            .AppendLine("	TBL3.tegata_zanndaka, ")
            .AppendLine("	--売上債権 ")
            .AppendLine("	TBL3.uriage_saiken ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			'' datakbn ")
            .AppendLine("			,(TBL2.seikyuu_saki_cd + '$$$' + TBL2.seikyuu_saki_brc + '$$$' + TBL2.seikyuu_saki_kbn) tokuisaki_cd --得意先コード ")
            .AppendLine("			,VS.seikyuu_saki_mei tokuisaki_mei1 --得意先名１ ")
            .AppendLine("			,VS.seikyuu_saki_mei2 tokuisaki_mei2 --得意先名２ ")
            .AppendLine("			--繰越残高 ")
            .AppendLine("			,ISNULL(TBL2.kurikosi_zan,0) kurikosi_zanndaka ")
            .AppendLine("			--現金・振込 ")
            .AppendLine("			,ISNULL(TBL2.genkin_furikomi,0) genkin_furikomi ")
            .AppendLine("			--手形 ")
            .AppendLine("			,ISNULL(TBL2.tegata,0) tegata ")
            .AppendLine("			--相殺他 ")
            .AppendLine("			,ISNULL(TBL2.sousaihoka,0) sousaihoka ")
            .AppendLine("			--入金合計(現金・振込＋手形＋相殺他) ")
            .AppendLine("			,ISNULL(TBL2.genkin_furikomi,0) + ISNULL(TBL2.tegata,0) + ISNULL(TBL2.sousaihoka,0) nyuukin_goukei ")
            .AppendLine("			--未回収残高(繰越残高－入金合計＝繰越残高－(現金・振込＋手形＋相殺他)) ")
            .AppendLine("			,ISNULL(TBL2.kurikosi_zan,0) - (ISNULL(TBL2.genkin_furikomi,0) + ISNULL(TBL2.tegata,0) + ISNULL(TBL2.sousaihoka,0)) mikaisyuu_zanndaka ")
            .AppendLine("			--売上高 ")
            .AppendLine("			,ISNULL(TBL2.uriagedaka,0) uriagedaka ")
            .AppendLine("			--消費税等 ")
            .AppendLine("			,ISNULL(TBL2.syouhizeinado,0) syouhizeinado ")
            .AppendLine("			--差引残高(繰越残高－入金合計＋売上高＋消費税等＝繰越残高－(現金・振込＋手形＋相殺他)＋売上高＋消費税等) ")
            .AppendLine("			,ISNULL(TBL2.kurikosi_zan,0) - (ISNULL(TBL2.genkin_furikomi,0) + ISNULL(TBL2.tegata,0) + ISNULL(TBL2.sousaihoka,0)) + (ISNULL(TBL2.uriagedaka,0) + ISNULL(TBL2.syouhizeinado,0)) sasihiki_zanndaka ")
            .AppendLine("			--手形残高 ")
            .AppendLine("			,ISNULL(TBL2.tegata_zanndaka,0) tegata_zanndaka ")
            .AppendLine("			--売上債権(差引残高＋手形残高＝((繰越残高－(現金・振込＋手形＋相殺他)＋売上高＋消費税等)＋手形残高) ")
            .AppendLine("			,ISNULL(TBL2.kurikosi_zan,0) - (ISNULL(TBL2.genkin_furikomi,0) + ISNULL(TBL2.tegata,0) + ISNULL(TBL2.sousaihoka,0)) + (ISNULL(TBL2.uriagedaka,0) + ISNULL(TBL2.syouhizeinado,0)) + ISNULL(TBL2.tegata_zanndaka,0) uriage_saiken ")
            .AppendLine("		FROM ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					 MSS.seikyuu_saki_cd ")
            .AppendLine("					,MSS.seikyuu_saki_brc ")
            .AppendLine("					,MSS.seikyuu_saki_kbn ")
            .AppendLine("					,TBL1.kurikosi_zan --繰越残高 ")
            .AppendLine("					,(tn.genkin + tn.kogitte + tn.furikomi + tn.kouza_furikae) AS genkin_furikomi --現金・振込([現金]＋[小切手]＋[振込]＋[口座振替]) ")
            .AppendLine("					,tn.tegata --手形 ")
            .AppendLine("					,(tn.sousai + tn.nebiki + tn.sonota + tn.kyouryoku_kaihi + tn.furikomi_tesuuryou) AS sousaihoka --相殺他([相殺]＋[値引]＋[その他]＋[協力会費]＋[振込手数料]) ")
            .AppendLine("					,(tu1.uri_gaku) AS uriagedaka --売上高(売上金額) ")
            .AppendLine("					,(tu1.sotozei_gaku) AS syouhizeinado --消費税等(外税額) ")
            .AppendLine("					,(tn3.tegata) AS tegata_zanndaka --手形残高(手形) ")
            .AppendLine("				FROM ")
            .AppendLine("					m_seikyuu_saki MSS ")
            .AppendLine("					LEFT JOIN ")
            .AppendLine("					/* ")
            .AppendLine("					繰越残高(TBL1) ")
            .AppendLine("					*/ ")
            .AppendLine("					( ")
            .AppendLine("						SELECT ")
            .AppendLine("							 sm.seikyuu_saki_cd ")
            .AppendLine("							,sm.seikyuu_saki_brc ")
            .AppendLine("							,sm.seikyuu_saki_kbn ")
            .AppendLine("							,ISNULL(ukt.tougetu_kurikosi_zan,0) + ISNULL(u2.uriage_goukei,0) - ISNULL(n2.nyuukin_goukei,0) AS kurikosi_zan --繰越残高 ")
            .AppendLine("						FROM ")
            .AppendLine("							m_seikyuu_saki sm ")
            .AppendLine("							LEFT OUTER JOIN ")
            .AppendLine("							( ")
            .AppendLine("								SELECT ")
            .AppendLine("									 uk.taisyou_nengetu ")
            .AppendLine("									,uk.seikyuu_saki_cd ")
            .AppendLine("									,uk.seikyuu_saki_brc ")
            .AppendLine("									,uk.seikyuu_saki_kbn ")
            .AppendLine("									,uk.tougetu_kurikosi_zan ")
            .AppendLine("								FROM ")
            .AppendLine("									t_urikake_data uk ")
            .AppendLine("									LEFT OUTER JOIN ")
            .AppendLine("									( ")
            .AppendLine("										SELECT ")
            .AppendLine("											 seikyuu_saki_cd ")
            .AppendLine("											,seikyuu_saki_brc ")
            .AppendLine("											,seikyuu_saki_kbn ")
            .AppendLine("											,MAX(taisyou_nengetu) taisyou_nengetu ")
            .AppendLine("										FROM ")
            .AppendLine("											t_urikake_data ")
            .AppendLine("										WHERE ")
            .AppendLine("											taisyou_nengetu <= " & connDBUser & " fnGetLastDay(DATEADD(MONTH,- 1,@fromDate)) ")
            .AppendLine("										GROUP BY ")
            .AppendLine("											 seikyuu_saki_cd ")
            .AppendLine("											,seikyuu_saki_brc ")
            .AppendLine("											,seikyuu_saki_kbn ")
            .AppendLine("									) ukm ")
            .AppendLine("									ON ")
            .AppendLine("										uk.seikyuu_saki_cd = ukm.seikyuu_saki_cd ")
            .AppendLine("										AND ")
            .AppendLine("										uk.seikyuu_saki_brc = ukm.seikyuu_saki_brc ")
            .AppendLine("										AND ")
            .AppendLine("										uk.seikyuu_saki_kbn = ukm.seikyuu_saki_kbn ")
            .AppendLine("								WHERE ")
            .AppendLine("									uk.taisyou_nengetu = ukm.taisyou_nengetu ")
            .AppendLine("							) ukt ")
            .AppendLine("							ON ")
            .AppendLine("								sm.seikyuu_saki_cd = ukt.seikyuu_saki_cd ")
            .AppendLine("								AND ")
            .AppendLine("								sm.seikyuu_saki_brc = ukt.seikyuu_saki_brc ")
            .AppendLine("								AND ")
            .AppendLine("								sm.seikyuu_saki_kbn = ukt.seikyuu_saki_kbn ")
            .AppendLine("							/* ")
            .AppendLine("							売上データテーブル.売上金額＋外税額 の合計 ")
            .AppendLine("							*/ ")
            .AppendLine("							LEFT OUTER JOIN ")
            .AppendLine("							( ")
            .AppendLine("								SELECT ")
            .AppendLine("									 sm.seikyuu_saki_cd ")
            .AppendLine("									,sm.seikyuu_saki_brc ")
            .AppendLine("									,sm.seikyuu_saki_kbn ")
            .AppendLine("									,SUM(isnull(u2.uri_gaku,0)) + SUM(isnull(u2.sotozei_gaku,0)) uriage_goukei ")
            .AppendLine("								FROM ")
            .AppendLine("									m_seikyuu_saki sm ")
            .AppendLine("									LEFT OUTER JOIN ")
            .AppendLine("									( ")
            .AppendLine("										SELECT ")
            .AppendLine("											 uk.taisyou_nengetu ")
            .AppendLine("											,uk.seikyuu_saki_cd ")
            .AppendLine("											,uk.seikyuu_saki_brc ")
            .AppendLine("											,uk.seikyuu_saki_kbn ")
            .AppendLine("										FROM ")
            .AppendLine("											t_urikake_data uk ")
            .AppendLine("											LEFT OUTER JOIN ")
            .AppendLine("											( ")
            .AppendLine("												SELECT ")
            .AppendLine("													 seikyuu_saki_cd ")
            .AppendLine("													,seikyuu_saki_brc ")
            .AppendLine("													,seikyuu_saki_kbn ")
            .AppendLine("													,MAX(taisyou_nengetu) taisyou_nengetu ")
            .AppendLine("												FROM ")
            .AppendLine("													t_urikake_data ")
            .AppendLine("												WHERE ")
            .AppendLine("													taisyou_nengetu <= " & connDBUser & " fnGetLastDay(DATEADD(MONTH,- 1,@fromDate)) ")
            .AppendLine("												GROUP BY ")
            .AppendLine("													 seikyuu_saki_cd ")
            .AppendLine("													,seikyuu_saki_brc ")
            .AppendLine("													,seikyuu_saki_kbn ")
            .AppendLine("											) ukm ")
            .AppendLine("											ON ")
            .AppendLine("												uk.seikyuu_saki_cd = ukm.seikyuu_saki_cd ")
            .AppendLine("												AND ")
            .AppendLine("												uk.seikyuu_saki_brc = ukm.seikyuu_saki_brc ")
            .AppendLine("												AND ")
            .AppendLine("												uk.seikyuu_saki_kbn = ukm.seikyuu_saki_kbn ")
            .AppendLine("										WHERE ")
            .AppendLine("											uk.taisyou_nengetu = ukm.taisyou_nengetu ")
            .AppendLine("									) ukt ")
            .AppendLine("									ON ")
            .AppendLine("										sm.seikyuu_saki_cd = ukt.seikyuu_saki_cd ")
            .AppendLine("										AND ")
            .AppendLine("										sm.seikyuu_saki_brc = ukt.seikyuu_saki_brc ")
            .AppendLine("										AND ")
            .AppendLine("										sm.seikyuu_saki_kbn = ukt.seikyuu_saki_kbn ")
            .AppendLine("									LEFT OUTER JOIN ")
            .AppendLine("									t_uriage_data u2 ")
            .AppendLine("									ON ")
            .AppendLine("										u2.seikyuu_saki_cd = sm.seikyuu_saki_cd ")
            .AppendLine("										AND ")
            .AppendLine("										u2.seikyuu_saki_brc = sm.seikyuu_saki_brc ")
            .AppendLine("										AND ")
            .AppendLine("										u2.seikyuu_saki_kbn = sm.seikyuu_saki_kbn ")
            .AppendLine("										AND ")
            .AppendLine("										u2.denpyou_uri_date BETWEEN DATEADD(DAY,+ 1,ISNULL(ukt.taisyou_nengetu,'1900/1/1')) AND DATEADD(DAY,- 1,@fromDate) ")
            .AppendLine("										AND ")
            .AppendLine("										ISNULL(u2.uri_keijyou_flg,'0') = '1' ")
            .AppendLine("								GROUP BY ")
            .AppendLine("									sm.seikyuu_saki_cd ")
            .AppendLine("									,sm.seikyuu_saki_brc ")
            .AppendLine("									,sm.seikyuu_saki_kbn ")
            .AppendLine("							) AS u2 ")
            .AppendLine("							ON ")
            .AppendLine("								u2.seikyuu_saki_cd = sm.seikyuu_saki_cd ")
            .AppendLine("								AND ")
            .AppendLine("								u2.seikyuu_saki_brc = sm.seikyuu_saki_brc ")
            .AppendLine("								AND ")
            .AppendLine("								u2.seikyuu_saki_kbn = sm.seikyuu_saki_kbn ")
            .AppendLine("							/* ")
            .AppendLine("							入金データテーブル. ")
            .AppendLine("							入金額 [現金]＋入金額 [小切手]＋入金額 [振込]＋入金額 [口座振替]＋入金額 [手形] ")
            .AppendLine("							＋入金額 [相殺]＋入金額 [値引]＋入金額 [その他]＋入金額 [協力会費]＋入金額 [振込手数料] ")
            .AppendLine("							の合計 ")
            .AppendLine("							*/ ")
            .AppendLine("							LEFT OUTER JOIN ")
            .AppendLine("							( ")
            .AppendLine("								SELECT ")
            .AppendLine("									 sm.seikyuu_saki_cd ")
            .AppendLine("									,sm.seikyuu_saki_brc ")
            .AppendLine("									,sm.seikyuu_saki_kbn ")
            .AppendLine("									,SUM(isnull(n2.genkin,0)) + SUM(isnull(n2.kogitte,0)) + SUM(isnull(n2.furikomi,0)) + SUM(isnull(n2.tegata,0)) ")
            .AppendLine("										+ SUM(isnull(n2.sousai,0)) + SUM(isnull(n2.nebiki,0)) + SUM(isnull(n2.sonota,0)) + SUM(isnull(n2.kyouryoku_kaihi,0)) ")
            .AppendLine("										+ SUM(isnull(n2.kouza_furikae,0)) + SUM(isnull(n2.furikomi_tesuuryou,0)) nyuukin_goukei ")
            .AppendLine("								FROM ")
            .AppendLine("									m_seikyuu_saki sm ")
            .AppendLine("									LEFT OUTER JOIN ")
            .AppendLine("									( ")
            .AppendLine("										SELECT ")
            .AppendLine("											 uk.taisyou_nengetu ")
            .AppendLine("											,uk.seikyuu_saki_cd ")
            .AppendLine("											,uk.seikyuu_saki_brc ")
            .AppendLine("											,uk.seikyuu_saki_kbn ")
            .AppendLine("										FROM ")
            .AppendLine("											t_urikake_data uk ")
            .AppendLine("											LEFT OUTER JOIN ")
            .AppendLine("											( ")
            .AppendLine("												SELECT ")
            .AppendLine("													 seikyuu_saki_cd ")
            .AppendLine("													,seikyuu_saki_brc ")
            .AppendLine("													,seikyuu_saki_kbn ")
            .AppendLine("													,MAX(taisyou_nengetu) taisyou_nengetu ")
            .AppendLine("												FROM ")
            .AppendLine("													t_urikake_data ")
            .AppendLine("												WHERE ")
            .AppendLine("													taisyou_nengetu <= " & connDBUser & " fnGetLastDay(DATEADD(MONTH,- 1,@fromDate)) ")
            .AppendLine("												GROUP BY ")
            .AppendLine("													 seikyuu_saki_cd ")
            .AppendLine("													,seikyuu_saki_brc ")
            .AppendLine("													,seikyuu_saki_kbn ")
            .AppendLine("											) ukm ")
            .AppendLine("											ON ")
            .AppendLine("												uk.seikyuu_saki_cd = ukm.seikyuu_saki_cd ")
            .AppendLine("												AND ")
            .AppendLine("												uk.seikyuu_saki_brc = ukm.seikyuu_saki_brc ")
            .AppendLine("												AND ")
            .AppendLine("												uk.seikyuu_saki_kbn = ukm.seikyuu_saki_kbn ")
            .AppendLine("										WHERE ")
            .AppendLine("											uk.taisyou_nengetu = ukm.taisyou_nengetu ")
            .AppendLine("									) ukt ")
            .AppendLine("									ON ")
            .AppendLine("										sm.seikyuu_saki_cd = ukt.seikyuu_saki_cd ")
            .AppendLine("										AND ")
            .AppendLine("										sm.seikyuu_saki_brc = ukt.seikyuu_saki_brc ")
            .AppendLine("										AND ")
            .AppendLine("										sm.seikyuu_saki_kbn = ukt.seikyuu_saki_kbn ")
            .AppendLine("									LEFT OUTER JOIN ")
            .AppendLine("									t_nyuukin_data n2 ")
            .AppendLine("									ON ")
            .AppendLine("										n2.seikyuu_saki_cd = sm.seikyuu_saki_cd ")
            .AppendLine("										AND ")
            .AppendLine("										n2.seikyuu_saki_brc = sm.seikyuu_saki_brc ")
            .AppendLine("										AND ")
            .AppendLine("										n2.seikyuu_saki_kbn = sm.seikyuu_saki_kbn ")
            .AppendLine("										AND ")
            .AppendLine("										n2.nyuukin_date BETWEEN DATEADD(DAY,+ 1,ISNULL(ukt.taisyou_nengetu,'1900/1/1')) AND DATEADD(DAY,- 1,@fromDate) ")
            .AppendLine("								GROUP BY ")
            .AppendLine("									sm.seikyuu_saki_cd ")
            .AppendLine("									,sm.seikyuu_saki_brc ")
            .AppendLine("									,sm.seikyuu_saki_kbn ")
            .AppendLine("							) AS n2 ")
            .AppendLine("							ON ")
            .AppendLine("								n2.seikyuu_saki_cd = sm.seikyuu_saki_cd ")
            .AppendLine("								AND ")
            .AppendLine("								n2.seikyuu_saki_brc = sm.seikyuu_saki_brc ")
            .AppendLine("								AND ")
            .AppendLine("								n2.seikyuu_saki_kbn = sm.seikyuu_saki_kbn ")
            .AppendLine("					) AS TBL1 ")
            .AppendLine("					ON ")
            .AppendLine("						MSS.seikyuu_saki_cd = TBL1.seikyuu_saki_cd ")
            .AppendLine("						AND ")
            .AppendLine("						MSS.seikyuu_saki_brc = TBL1.seikyuu_saki_brc ")
            .AppendLine("						AND ")
            .AppendLine("						MSS.seikyuu_saki_kbn = TBL1.seikyuu_saki_kbn ")
            .AppendLine("						 ")
            .AppendLine("					/* ")
            .AppendLine("					現金・振込 ")
            .AppendLine("					手形 ")
            .AppendLine("					相殺他 ")
            .AppendLine("					*/ ")
            .AppendLine("					LEFT OUTER JOIN ")
            .AppendLine("					( ")
            .AppendLine("						SELECT ")
            .AppendLine("							seikyuu_saki_cd ")
            .AppendLine("							,seikyuu_saki_brc ")
            .AppendLine("							,seikyuu_saki_kbn ")
            .AppendLine("							,SUM(isnull(genkin,0)) AS genkin --現金 ")
            .AppendLine("							,SUM(isnull(kogitte,0)) AS kogitte --小切手 ")
            .AppendLine("							,SUM(isnull(furikomi,0)) AS furikomi --振込 ")
            .AppendLine("							,SUM(isnull(kouza_furikae,0)) AS kouza_furikae --口座振替 ")
            .AppendLine("							,SUM(isnull(tegata,0)) AS tegata --手形 ")
            .AppendLine("							,SUM(isnull(sousai,0)) AS sousai --相殺 ")
            .AppendLine("							,SUM(isnull(nebiki,0)) AS nebiki --値引 ")
            .AppendLine("							,SUM(isnull(sonota,0)) AS sonota --その他 ")
            .AppendLine("							,SUM(isnull(kyouryoku_kaihi,0)) AS kyouryoku_kaihi --協力会費 ")
            .AppendLine("							,SUM(isnull(furikomi_tesuuryou,0)) AS furikomi_tesuuryou --振込手数料 ")
            .AppendLine("						FROM ")
            .AppendLine("							t_nyuukin_data ")
            .AppendLine("						WHERE ")
            .AppendLine("							--nyuukin_date BETWEEN @fromDate AND @toDate ")
            .AppendLine("							CONVERT(CHAR(10),ISNULL(nyuukin_date,'1900/01/01'),111) BETWEEN CONVERT(CHAR(10),@fromDate,111) AND CONVERT(CHAR(10),@toDate,111) ")
            .AppendLine("						GROUP BY ")
            .AppendLine("							seikyuu_saki_cd ")
            .AppendLine("							,seikyuu_saki_brc ")
            .AppendLine("							,seikyuu_saki_kbn ")
            .AppendLine("					) AS tn ")
            .AppendLine("					ON ")
            .AppendLine("						tn.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
            .AppendLine("						AND ")
            .AppendLine("						tn.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
            .AppendLine("						AND ")
            .AppendLine("						tn.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
            .AppendLine("						 ")
            .AppendLine("					/* ")
            .AppendLine("					売上高 ")
            .AppendLine("					消費税等 ")
            .AppendLine("					*/ ")
            .AppendLine("					LEFT OUTER JOIN ")
            .AppendLine("					( ")
            .AppendLine("						SELECT ")
            .AppendLine("							seikyuu_saki_cd ")
            .AppendLine("							,seikyuu_saki_brc ")
            .AppendLine("							,seikyuu_saki_kbn ")
            .AppendLine("							,SUM(isnull(uri_gaku,0)) AS uri_gaku --売上高 ")
            .AppendLine("							,SUM(isnull(sotozei_gaku,0)) AS sotozei_gaku --外税額 ")
            .AppendLine("						FROM ")
            .AppendLine("							t_uriage_data ")
            .AppendLine("						WHERE ")
            .AppendLine("							--denpyou_uri_date BETWEEN @fromDate AND @toDate ")
            .AppendLine("							CONVERT(CHAR(10),ISNULL(denpyou_uri_date,'1900/01/01'),111) BETWEEN CONVERT(CHAR(10),@fromDate,111) AND CONVERT(CHAR(10),@toDate,111) ")
            .AppendLine("							AND ")
            .AppendLine("							ISNULL(uri_keijyou_flg,'0') = '1' ")
            .AppendLine("						GROUP BY ")
            .AppendLine("							seikyuu_saki_cd ")
            .AppendLine("							,seikyuu_saki_brc ")
            .AppendLine("							,seikyuu_saki_kbn ")
            .AppendLine("					) AS tu1 ")
            .AppendLine("					ON ")
            .AppendLine("						tu1.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
            .AppendLine("						AND ")
            .AppendLine("						tu1.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
            .AppendLine("						AND ")
            .AppendLine("						tu1.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
            .AppendLine("						 ")
            .AppendLine("					/* ")
            .AppendLine("					手形残高 ")
            .AppendLine("					*/ ")
            .AppendLine("					LEFT OUTER JOIN ")
            .AppendLine("					( ")
            .AppendLine("						SELECT ")
            .AppendLine("							seikyuu_saki_cd ")
            .AppendLine("							,seikyuu_saki_brc ")
            .AppendLine("							,seikyuu_saki_kbn ")
            .AppendLine("							,SUM(isnull(tegata,0)) AS tegata --手形 ")
            .AppendLine("						FROM ")
            .AppendLine("							t_nyuukin_data ")
            .AppendLine("						WHERE ")
            .AppendLine("							CONVERT(CHAR(10),ISNULL(nyuukin_date,'1900/01/01'),111) <= CONVERT(CHAR(10),@toDate,111) ")
            .AppendLine("							AND ")
            .AppendLine("							--tegata_kijitu >= DATEADD(DAY,+ 1,@toDate) ")
            .AppendLine("							CONVERT(CHAR(10),ISNULL(tegata_kijitu,'1900/01/01'),111) >= CONVERT(CHAR(10),DATEADD(DAY,+ 1,@toDate),111) ")
            .AppendLine("						GROUP BY ")
            .AppendLine("							seikyuu_saki_cd ")
            .AppendLine("							,seikyuu_saki_brc ")
            .AppendLine("							,seikyuu_saki_kbn ")
            .AppendLine("					) AS tn3 ")
            .AppendLine("					ON ")
            .AppendLine("						tn3.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
            .AppendLine("						AND ")
            .AppendLine("						tn3.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
            .AppendLine("						AND ")
            .AppendLine("						tn3.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
            .AppendLine("			) AS TBL2		 ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("				v_seikyuu_saki_info VS ")
            .AppendLine("			ON ")
            .AppendLine("				TBL2.seikyuu_saki_cd = VS.seikyuu_saki_cd ")
            .AppendLine("			AND ")
            .AppendLine("				TBL2.seikyuu_saki_brc = VS.seikyuu_saki_brc ")
            .AppendLine("			AND ")
            .AppendLine("				TBL2.seikyuu_saki_kbn = VS.seikyuu_saki_kbn ")
            .AppendLine("			-- ORDER BY ")
            .AppendLine("			-- 	TBL2.seikyuu_saki_cd + TBL2.seikyuu_saki_brc + TBL2.seikyuu_saki_kbn ")
            .AppendLine("	) TBL3 ")
            .AppendLine("WHERE ")
            .AppendLine("	TBL3.kurikosi_zanndaka <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.genkin_furikomi <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.tegata <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.sousaihoka <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.nyuukin_goukei <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.mikaisyuu_zanndaka <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.uriagedaka <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.syouhizeinado <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.sasihiki_zanndaka <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.tegata_zanndaka <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.uriage_saiken <> 0 ")
            .AppendLine("ORDER BY ")
            .AppendLine("	TBL3.tokuisaki_cd ")
        End With

        paramList.Add(MakeParam("@fromDate", SqlDbType.VarChar, 10, strDateFrom))
        paramList.Add(MakeParam("@toDate", SqlDbType.VarChar, 10, strDateTo))

        ' 検索実行'
        'FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.UrikakekinZandakaHyou.TableName, paramList.ToArray)
        Dim timeout As Integer

        Try
            timeout = CInt(System.Configuration.ConfigurationManager.AppSettings("GetUrikakekinZandakaHyou").ToString)
        Catch ex As Exception
            timeout = 60
        End Try

        Dim InsPrintDataConnect As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(connStr)
        If InsPrintDataConnect.State = ConnectionState.Broken Then
            InsPrintDataConnect.Close()
            InsPrintDataConnect.Open()
        Else
            InsPrintDataConnect.Open()
        End If

        Dim sqlAdapter As SqlDataAdapter

        Dim param1 As New SqlParameter()
        param1.ParameterName = "@fromDate"
        param1.DbType = DbType.String
        param1.Value = strDateFrom

        Dim param2 As New SqlParameter()
        param2.ParameterName = "@toDate"
        param2.DbType = DbType.String
        param2.Value = strDateTo

        Dim SQLCommand As New System.Data.SqlClient.SqlCommand
        SQLCommand.CommandText = commandTextSb.ToString
        SQLCommand.CommandType = CommandType.Text
        SQLCommand.Connection = InsPrintDataConnect
        SQLCommand.CommandTimeout = timeout
        SQLCommand.Parameters.Add(param1)
        SQLCommand.Parameters.Add(param2)
        sqlAdapter = New SqlDataAdapter(SQLCommand)

        sqlAdapter.Fill(dsDataSet.UrikakekinZandakaHyou)

        SQLCommand.Dispose()
        InsPrintDataConnect.Close()
        InsPrintDataConnect.Dispose()

        '戻る
        Return dsDataSet.UrikakekinZandakaHyou
    End Function

    ''' <summary>
    ''' 買掛金残高表csv出力のデータを取得
    ''' </summary>
    ''' <param name="strDateFrom">抽出期間FROM</param>
    ''' <param name="strDateTo">抽出期間TO</param>
    ''' <history>2010/07/20 趙東莉(大連情報システム部)　新規作成</history>
    Public Function SelKaikakekinZandakaHyou(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.KaikakekinZandakaHyouDataTable
        ' DataSetインスタンスの生成
        Dim dsDataSet As New ExcelSiwakeDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	TBL3.datakbn ")
            .AppendLine("	,TBL3.tokuisaki_cd ")
            .AppendLine("	,TBL3.tokuisaki_mei1 ")
            .AppendLine("	,TBL3.tokuisaki_mei2 ")
            .AppendLine("	,TBL3.kurikosi_zanndaka ")
            .AppendLine("	,TBL3.furikomi ")
            .AppendLine("	,TBL3.sousai ")
            .AppendLine("	,TBL3.goukei ")
            .AppendLine("	,TBL3.gou_zandaka ")
            .AppendLine("	,TBL3.siire_gaku ")
            .AppendLine("	,TBL3.sotozei_gaku ")
            .AppendLine("	,TBL3.sai_zandaka ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			'' AS datakbn --データ区分 ")
            .AppendLine("			,A.CODE AS tokuisaki_cd --支払先コード ")
            .AppendLine("			,A.MEI1 AS tokuisaki_mei1 --支払先名1 ")
            .AppendLine("			,'' AS tokuisaki_mei2 --支払先名２ ")
            .AppendLine("			,ISNULL(A.ZENDAKA,0) AS kurikosi_zanndaka --繰越残高 ")
            .AppendLine("			,ISNULL(A.furikomi,0) AS furikomi --振込 ")
            .AppendLine("			,ISNULL(A.sousai,0) AS sousai --相殺 ")
            .AppendLine("			,ISNULL(A.furikomi,0)+ISNULL(A.sousai,0) AS goukei --支払合計 ")
            .AppendLine("			,ISNULL(A.ZENDAKA,0)-(ISNULL(A.furikomi,0)+ISNULL(A.sousai,0)) AS gou_zandaka --未払残高 ")
            .AppendLine("			,ISNULL(A.siire_gaku,0) AS siire_gaku --仕入等 ")
            .AppendLine("			,ISNULL(A.sotozei_gaku,0) AS sotozei_gaku --消費税等 ")
            .AppendLine("			,ISNULL(A.ZENDAKA,0)-(ISNULL(A.furikomi,0)+ISNULL(A.sousai,0))+ISNULL(A.siire_gaku,0)+ISNULL(A.sotozei_gaku,0) AS sai_zandaka --差引残高 ")
            .AppendLine("		FROM ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MT.tys_kaisya_cd --調査会社コード ")
            .AppendLine("					,MT.jigyousyo_cd --事業所コード ")
            .AppendLine("					,MT.tys_kaisya_cd + MT.jigyousyo_cd AS CODE --支払先コード ")
            .AppendLine("					,CASE ")
            .AppendLine("						WHEN ISNULL(MT.seikyuu_saki_shri_saki_mei,'') = '' THEN ")
            .AppendLine("							MT.tys_kaisya_mei ")
            .AppendLine("						ELSE ")
            .AppendLine("							MT.seikyuu_saki_shri_saki_mei ")
            .AppendLine("						END AS MEI1 --支払先名1 ")
            .AppendLine("					,ZENDAKA.zendaka --繰越残高 ")
            .AppendLine("					,TSD1.furikomi --振込 ")
            .AppendLine("					,TSD1.sousai --相殺 ")
            .AppendLine("					,s2.siire_gaku --仕入等 ")
            .AppendLine("					,s2.sotozei_gaku --消費税等 ")
            .AppendLine("				FROM ")
            .AppendLine("					( ")
            .AppendLine("						SELECT ")
            .AppendLine("							tys_kaisya_cd ")
            .AppendLine("							,jigyousyo_cd ")
            .AppendLine("							,shri_jigyousyo_cd ")
            .AppendLine("							,seikyuu_saki_shri_saki_mei ")
            .AppendLine("							,tys_kaisya_mei ")
            .AppendLine("							,skk_jigyousyo_cd ")
            .AppendLine("							,skk_shri_saki_cd ")
            .AppendLine("						FROM ")
            .AppendLine("							m_tyousakaisya ")
            .AppendLine("						WHERE ")
            .AppendLine("							jigyousyo_cd = shri_jigyousyo_cd --事業所コード＝支払集計先事業所コード ")
            .AppendLine("					) AS MT ")
            .AppendLine("					/* ")
            .AppendLine("					繰越残高(ZENDAKA) ")
            .AppendLine("					*/ ")
            .AppendLine("					LEFT JOIN ")
            .AppendLine("					( ")
            .AppendLine("						SELECT ")
            .AppendLine("							sm.tys_kaisya_cd ")
            .AppendLine("							,sm.shri_jigyousyo_cd ")
            .AppendLine("							,ISNULL(ukt.tougetu_kurikosi_zan,0) + ISNULL(s2.siire_goukei,0) - ISNULL(h2.siharai_goukei,0) AS zendaka --繰越残高 ")
            .AppendLine("						FROM ")
            .AppendLine("							( ")
            .AppendLine("								SELECT ")
            .AppendLine("									tys_kaisya_cd ")
            .AppendLine("									,shri_jigyousyo_cd ")
            .AppendLine("								FROM ")
            .AppendLine("									m_tyousakaisya ")
            .AppendLine("								WHERE ")
            .AppendLine("									jigyousyo_cd = shri_jigyousyo_cd --事業所コード＝支払集計先事業所コード ")
            .AppendLine("							) AS sm ")
            .AppendLine("							LEFT OUTER JOIN ")
            .AppendLine("							( ")
            .AppendLine("								SELECT ")
            .AppendLine("									uk.taisyou_nengetu ")
            .AppendLine("									,uk.tys_kaisya_cd ")
            .AppendLine("									,uk.shri_jigyousyo_cd ")
            .AppendLine("									,uk.tougetu_kurikosi_zan ")
            .AppendLine("								FROM ")
            .AppendLine("									t_kaikake_data uk ")
            .AppendLine("									LEFT OUTER JOIN ")
            .AppendLine("									( ")
            .AppendLine("										SELECT ")
            .AppendLine("											tys_kaisya_cd ")
            .AppendLine("											,shri_jigyousyo_cd ")
            .AppendLine("											,max(taisyou_nengetu) taisyou_nengetu ")
            .AppendLine("										FROM ")
            .AppendLine("											t_kaikake_data ")
            .AppendLine("										WHERE ")
            .AppendLine("											taisyou_nengetu <= " & connDBUser & " fnGetLastDay(DATEADD(MONTH, - 1, @fromDate)) ")
            .AppendLine("										GROUP BY ")
            .AppendLine("											tys_kaisya_cd ")
            .AppendLine("											,shri_jigyousyo_cd ")
            .AppendLine("									) ukm ")
            .AppendLine("									ON ")
            .AppendLine("										uk.tys_kaisya_cd = ukm.tys_kaisya_cd ")
            .AppendLine("										AND ")
            .AppendLine("										uk.shri_jigyousyo_cd = ukm.shri_jigyousyo_cd ")
            .AppendLine("								WHERE ")
            .AppendLine("									uk.taisyou_nengetu = ukm.taisyou_nengetu ")
            .AppendLine("							) ukt ")
            .AppendLine("							ON ")
            .AppendLine("								sm.tys_kaisya_cd = ukt.tys_kaisya_cd ")
            .AppendLine("								AND ")
            .AppendLine("								sm.shri_jigyousyo_cd = ukt.shri_jigyousyo_cd ")
            .AppendLine("							 ")
            .AppendLine("							--仕入データテーブル.仕入金額＋外税額 の合計 ")
            .AppendLine("							LEFT JOIN ")
            .AppendLine("							( ")
            .AppendLine("								SELECT ")
            .AppendLine("									sm.tys_kaisya_cd ")
            .AppendLine("									,sm.shri_jigyousyo_cd ")
            .AppendLine("									,SUM(ISNULL(s2.siire_gaku, 0)) + SUM(CONVERT(BIGINT,ISNULL(s2.sotozei_gaku, 0))) siire_goukei ")
            .AppendLine("								FROM ")
            .AppendLine("									( ")
            .AppendLine("										SELECT ")
            .AppendLine("											tys_kaisya_cd ")
            .AppendLine("											,shri_jigyousyo_cd ")
            .AppendLine("										FROM ")
            .AppendLine("											m_tyousakaisya ")
            .AppendLine("										WHERE ")
            .AppendLine("											jigyousyo_cd = shri_jigyousyo_cd --事業所コード＝支払集計先事業所コード ")
            .AppendLine("									) AS sm ")
            .AppendLine("									LEFT OUTER JOIN ")
            .AppendLine("									( ")
            .AppendLine("										SELECT ")
            .AppendLine("											uk.taisyou_nengetu ")
            .AppendLine("											,uk.tys_kaisya_cd ")
            .AppendLine("											,uk.shri_jigyousyo_cd ")
            .AppendLine("										FROM ")
            .AppendLine("											t_kaikake_data uk ")
            .AppendLine("											LEFT OUTER JOIN ")
            .AppendLine("											( ")
            .AppendLine("												SELECT ")
            .AppendLine("													tys_kaisya_cd ")
            .AppendLine("													,shri_jigyousyo_cd ")
            .AppendLine("													,max(taisyou_nengetu) taisyou_nengetu ")
            .AppendLine("												FROM ")
            .AppendLine("													t_kaikake_data ")
            .AppendLine("												WHERE ")
            .AppendLine("													taisyou_nengetu <= " & connDBUser & " fnGetLastDay(DATEADD(MONTH, - 1, @fromDate)) ")
            .AppendLine("												GROUP BY ")
            .AppendLine("													tys_kaisya_cd ")
            .AppendLine("													,shri_jigyousyo_cd ")
            .AppendLine("											) ukm ")
            .AppendLine("											ON ")
            .AppendLine("												uk.tys_kaisya_cd = ukm.tys_kaisya_cd ")
            .AppendLine("												AND ")
            .AppendLine("												uk.shri_jigyousyo_cd = ukm.shri_jigyousyo_cd ")
            .AppendLine("										WHERE ")
            .AppendLine("											uk.taisyou_nengetu = ukm.taisyou_nengetu ")
            .AppendLine("									) ukt ")
            .AppendLine("									ON ")
            .AppendLine("										sm.tys_kaisya_cd = ukt.tys_kaisya_cd ")
            .AppendLine("										AND ")
            .AppendLine("										sm.shri_jigyousyo_cd = ukt.shri_jigyousyo_cd ")
            .AppendLine("									LEFT JOIN ")
            .AppendLine("									t_siire_data s2 ")
            .AppendLine("									ON ")
            .AppendLine("										s2.tys_kaisya_cd = sm.tys_kaisya_cd ")
            .AppendLine("										AND ")
            .AppendLine("										s2.tys_kaisya_jigyousyo_cd = sm.shri_jigyousyo_cd ")
            .AppendLine("										AND ")
            .AppendLine("										s2.denpyou_siire_date BETWEEN DATEADD(DAY, + 1, ISNULL(ukt.taisyou_nengetu, '1900/1/1')) AND DATEADD(DAY, - 1, @fromDate) ")
            .AppendLine("										AND ")
            .AppendLine("										ISNULL(s2.siire_keijyou_flg,'0') = '1' ")
            .AppendLine("								 GROUP BY ")
            .AppendLine("									sm.tys_kaisya_cd ")
            .AppendLine("									,sm.shri_jigyousyo_cd ")
            .AppendLine("							)AS s2 ")
            .AppendLine("							ON ")
            .AppendLine("								sm.tys_kaisya_cd = s2.tys_kaisya_cd ")
            .AppendLine("								AND ")
            .AppendLine("								sm.shri_jigyousyo_cd = s2.shri_jigyousyo_cd ")
            .AppendLine("							 ")
            .AppendLine("							--支払データテーブル.支払額 [振込]＋支払額 [相殺] ")
            .AppendLine("							LEFT JOIN ")
            .AppendLine("							( ")
            .AppendLine("								SELECT ")
            .AppendLine("									sm.tys_kaisya_cd ")
            .AppendLine("									,sm.shri_jigyousyo_cd ")
            .AppendLine("									,SUM(ISNULL(h2.furikomi, 0)) + SUM(ISNULL(h2.sousai, 0)) siharai_goukei ")
            .AppendLine("								FROM ")
            .AppendLine("									( ")
            .AppendLine("										SELECT ")
            .AppendLine("											tys_kaisya_cd ")
            .AppendLine("											,shri_jigyousyo_cd ")
            .AppendLine("										FROM ")
            .AppendLine("											m_tyousakaisya ")
            .AppendLine("										WHERE ")
            .AppendLine("											jigyousyo_cd = shri_jigyousyo_cd --事業所コード＝支払集計先事業所コード ")
            .AppendLine("									) AS sm ")
            .AppendLine("									LEFT OUTER JOIN ")
            .AppendLine("									( ")
            .AppendLine("										SELECT ")
            .AppendLine("											uk.taisyou_nengetu ")
            .AppendLine("											,uk.tys_kaisya_cd ")
            .AppendLine("											,uk.shri_jigyousyo_cd ")
            .AppendLine("										FROM ")
            .AppendLine("											t_kaikake_data uk ")
            .AppendLine("											LEFT OUTER JOIN ")
            .AppendLine("											( ")
            .AppendLine("												SELECT ")
            .AppendLine("													tys_kaisya_cd ")
            .AppendLine("													,shri_jigyousyo_cd ")
            .AppendLine("													,max(taisyou_nengetu) taisyou_nengetu ")
            .AppendLine("												FROM ")
            .AppendLine("													t_kaikake_data ")
            .AppendLine("												WHERE ")
            .AppendLine("													taisyou_nengetu <= " & connDBUser & " fnGetLastDay(DATEADD(MONTH, - 1, @fromDate)) ")
            .AppendLine("												GROUP BY ")
            .AppendLine("													tys_kaisya_cd ")
            .AppendLine("													,shri_jigyousyo_cd ")
            .AppendLine("											) ukm ")
            .AppendLine("											ON ")
            .AppendLine("												uk.tys_kaisya_cd = ukm.tys_kaisya_cd ")
            .AppendLine("												AND ")
            .AppendLine("												uk.shri_jigyousyo_cd = ukm.shri_jigyousyo_cd ")
            .AppendLine("										WHERE ")
            .AppendLine("											uk.taisyou_nengetu = ukm.taisyou_nengetu ")
            .AppendLine("									) ukt ")
            .AppendLine("									ON ")
            .AppendLine("										sm.tys_kaisya_cd = ukt.tys_kaisya_cd ")
            .AppendLine("										AND ")
            .AppendLine("										sm.shri_jigyousyo_cd = ukt.shri_jigyousyo_cd ")
            .AppendLine("									LEFT JOIN ")
            .AppendLine("									( ")
            .AppendLine("										SELECT ")
            .AppendLine("											tt.tys_kaisya_cd ")
            .AppendLine("											,tt.jigyousyo_cd ")
            .AppendLine("											,h2.siharai_date ")
            .AppendLine("											,ISNULL(h2.furikomi, 0) AS furikomi ")
            .AppendLine("											,ISNULL(h2.sousai, 0) AS sousai ")
            .AppendLine("										FROM ")
            .AppendLine("											m_tyousakaisya tt ")
            .AppendLine("											INNER ")
            .AppendLine("											JOIN t_siharai_data h2 ")
            .AppendLine("											ON ")
            .AppendLine("												tt.skk_shri_saki_cd = h2.skk_shri_saki_cd ")
            .AppendLine("												AND ")
            .AppendLine("												tt.skk_jigyousyo_cd = h2.skk_jigyou_cd ")
            .AppendLine("									) AS h2 ")
            .AppendLine("									ON ")
            .AppendLine("									h2.tys_kaisya_cd = sm.tys_kaisya_cd ")
            .AppendLine("									AND ")
            .AppendLine("									h2.jigyousyo_cd = sm.shri_jigyousyo_cd ")
            .AppendLine("									AND ")
            .AppendLine("									h2.siharai_date BETWEEN DATEADD(DAY, + 1, ISNULL(ukt.taisyou_nengetu, '1900/1/1')) AND DATEADD(DAY, - 1, @fromDate) ")
            .AppendLine("								 GROUP BY ")
            .AppendLine("									sm.tys_kaisya_cd ")
            .AppendLine("									,sm.shri_jigyousyo_cd ")
            .AppendLine("							)AS h2 ")
            .AppendLine("							ON ")
            .AppendLine("								sm.tys_kaisya_cd = h2.tys_kaisya_cd ")
            .AppendLine("								AND ")
            .AppendLine("								sm.shri_jigyousyo_cd = h2.shri_jigyousyo_cd ")
            .AppendLine("					) AS ZENDAKA --繰越残高 ")
            .AppendLine("					ON ")
            .AppendLine("						ZENDAKA.tys_kaisya_cd = MT.tys_kaisya_cd ")
            .AppendLine("						AND ")
            .AppendLine("						ZENDAKA.shri_jigyousyo_cd = MT.shri_jigyousyo_cd ")
            .AppendLine("					/* ")
            .AppendLine("					振込/相殺(TSD1) ")
            .AppendLine("					*/ ")
            .AppendLine("					LEFT JOIN ")
            .AppendLine("					( ")
            .AppendLine("						SELECT ")
            .AppendLine("							skk_jigyou_cd ")
            .AppendLine("							,skk_shri_saki_cd ")
            .AppendLine("							,SUM(ISNULL(furikomi,0)) furikomi --振込 ")
            .AppendLine("							,SUM(ISNULL(sousai,0)) sousai --相殺 ")
            .AppendLine("						FROM ")
            .AppendLine("							t_siharai_data ")
            .AppendLine("						WHERE ")
            .AppendLine("							CONVERT(CHAR(10),siharai_date,111) BETWEEN CONVERT(CHAR(10),@fromDate,111) AND CONVERT(CHAR(10),@toDate,111) ")
            .AppendLine("						GROUP BY ")
            .AppendLine("							skk_jigyou_cd ")
            .AppendLine("							,skk_shri_saki_cd ")
            .AppendLine("					) AS TSD1 --振込/相殺 ")
            .AppendLine("					ON ")
            .AppendLine("						TSD1.skk_jigyou_cd = MT.skk_jigyousyo_cd ")
            .AppendLine("						AND ")
            .AppendLine("						TSD1.skk_shri_saki_cd = MT.skk_shri_saki_cd ")
            .AppendLine("					/* ")
            .AppendLine("					仕入等/消費税等(s2) ")
            .AppendLine("					*/ ")
            .AppendLine("					LEFT JOIN ")
            .AppendLine("					( ")
            .AppendLine("						SELECT ")
            .AppendLine("							tth3.tys_kaisya_cd ")
            .AppendLine("							,tth3.shri_jigyousyo_cd		 ")
            .AppendLine("							,SUM(isnull(s2.siire_gaku,0)) AS siire_gaku --仕入等 ")
            .AppendLine("							,SUM(isnull(s2.sotozei_gaku,0)) AS sotozei_gaku --消費税等 ")
            .AppendLine("						FROM ")
            .AppendLine("							m_tyousakaisya tth3 ")
            .AppendLine("							INNER JOIN ")
            .AppendLine("							t_siire_data s2 ")
            .AppendLine("							ON ")
            .AppendLine("								tth3.tys_kaisya_cd = s2.tys_kaisya_cd ")
            .AppendLine("								AND ")
            .AppendLine("								tth3.jigyousyo_cd = s2.tys_kaisya_jigyousyo_cd ")
            .AppendLine("								AND ")
            .AppendLine("								CONVERT(CHAR(10),s2.denpyou_siire_date,111) BETWEEN CONVERT(CHAR(10),@fromDate,111) AND CONVERT(CHAR(10),@toDate,111) ")
            .AppendLine("								AND ")
            .AppendLine("								ISNULL(siire_keijyou_flg,'0') = '1' ")
            .AppendLine("						GROUP BY ")
            .AppendLine("							tth3.tys_kaisya_cd ")
            .AppendLine("							,tth3.shri_jigyousyo_cd ")
            .AppendLine("					) AS s2 --仕入等/消費税等 ")
            .AppendLine("					ON ")
            .AppendLine("						s2.tys_kaisya_cd = MT.tys_kaisya_cd ")
            .AppendLine("						AND ")
            .AppendLine("						s2.shri_jigyousyo_cd = MT.shri_jigyousyo_cd ")
            .AppendLine("	 ")
            .AppendLine("			) A ")
            .AppendLine("	) TBL3 ")
            .AppendLine("WHERE ")
            .AppendLine("	TBL3.kurikosi_zanndaka <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.furikomi <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.sousai <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.goukei <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.gou_zandaka <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.siire_gaku <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.sotozei_gaku <> 0 ")
            .AppendLine("	OR ")
            .AppendLine("	TBL3.sai_zandaka <> 0 ")
            .AppendLine("ORDER BY ")
            .AppendLine("	TBL3.tokuisaki_cd ")
        End With

        paramList.Add(MakeParam("@fromDate", SqlDbType.VarChar, 10, strDateFrom))
        paramList.Add(MakeParam("@toDate", SqlDbType.VarChar, 10, strDateTo))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.KaikakekinZandakaHyou.TableName, paramList.ToArray)

        '戻る
        Return dsDataSet.KaikakekinZandakaHyou
    End Function

    ''' <summary>
    ''' 全角数字
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Function SetZenkakuSuuji(ByVal value As String) As String
        Dim str1 As String = value
        str1 = str1.Replace("0", "０")
        str1 = str1.Replace("1", "１")
        str1 = str1.Replace("2", "２")
        str1 = str1.Replace("3", "３")
        str1 = str1.Replace("4", "４")
        str1 = str1.Replace("5", "５")
        str1 = str1.Replace("6", "６")
        str1 = str1.Replace("7", "７")
        str1 = str1.Replace("8", "８")
        str1 = str1.Replace("9", "９")
        str1 = str1.Replace("/", "／")

        Return str1
    End Function

    ''' <summary>
    ''' 請求先マスタのCSV情報取得
    ''' </summary>
    ''' <returns>請求先マスタCSVテーブル</returns>
    ''' <remarks>請求先マスタのCSV情報のデータ</remarks>
    ''' <history>2009/07/15　車龍(大連情報システム部)　新規作成</history>
    Public Function Selm_seikyuu_sakiCSV() As KakusyuDataSyuturyokuMenuDataSet.m_seikyuu_sakiCSVTableDataTable

        ' DataSetインスタンスの生成
        Dim dsKakusyuDataSyuturyokuMenuDataSet As New KakusyuDataSyuturyokuMenuDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	(MSS.seikyuu_saki_cd+'$$$'+MSS.seikyuu_saki_brc+'$$$'+MSS.seikyuu_saki_kbn) AS tokuisaki_cd ") '--得意先コード 
            .AppendLine("	,VSS.seikyuu_saki_mei ") '--得意先名１ 
            .AppendLine("	,VSS.seikyuu_saki_mei2 ") '--得意先名2 
            .AppendLine("	,'' AS senpou_tantou_mei ") '--先方担当者名 
            .AppendLine("	,MSS.seikyuusyo_hittyk_date ") '--メールアドレス 
            .AppendLine("	,'0' AS master_kbn ") '--マスター区分 
            .AppendLine("	,(MSS.seikyuu_saki_cd+'$$$'+MSS.seikyuu_saki_brc+'$$$'+MSS.seikyuu_saki_kbn) AS seikyuu_saki_cd ") '--請求先コード 
            .AppendLine("	,'0' AS jiltuseki_kanri ") '--実績管理 
            .AppendLine("	,VSS.skysy_soufu_jyuusyo1 ") '--住所１ 
            .AppendLine("	,VSS.skysy_soufu_jyuusyo2 ") '--住所２ 
            .AppendLine("	,VSS.skysy_soufu_yuubin_no ") '--郵便番号 
            .AppendLine("	,VSS.skysy_soufu_tel_no ") '--電話番号 
            .AppendLine("	,MSS.nyuukin_kouza_no ") '--入金口座番号 
            .AppendLine("	,'0' AS tokuisaki_kbn1 ") '--得意先区分1 
            .AppendLine("	,'0' AS tokuisaki_kbn2 ") '--得意先区分2 
            .AppendLine("	,'0' AS tokuisaki_kbn3 ") '--得意先区分3 
            .AppendLine("	,'0' AS baika_no ") '--適用売価[売価No] 
            .AppendLine("	,'100.0' AS kakeritu ") '--適用売価[掛率] 
            .AppendLine("	,'0' AS zeikanzan ") '--適用売価[税換算] 
            .AppendLine("	,'0' AS syutantou_cd ") '--主担当者コード 
            .AppendLine("	,MSS.seikyuu_sime_date  ") '--請求締日 
            .AppendLine("	,'0' AS syouhizei_hasuu ") '--消費税端数 
            .AppendLine("	,'0' AS syouhizei_tuuti ") '--消費税通知 
            .AppendLine("	,MSS.kaisyuu1_syubetu1 ") '--回収種別1 
            .AppendLine("	,MSS.kaisyuu_kyoukaigaku ") '--回収種別境界額 
            .AppendLine("	,MSS.kaisyuu2_syubetu1 ") '--回収種別2
            .AppendLine("	,MSS.kaisyuu_yotei_gessuu ") '--回収予定月数
            .AppendLine("	,MSS.kaisyuu_yotei_date ") '--回収予定日 
            .AppendLine("	,'0' AS kaisyuuhouhou ") '--回収方法 
            .AppendLine("	,'0' AS yusin_gendogaku ") '--与信限度額 
            .AppendLine("	,'' AS kurikosi_zandaka ") '--繰越残高 
            .AppendLine("	,'1' AS nohinsyo_yousi ") '--納品書用紙 
            .AppendLine("	,'1' AS nohinsyu_syamei ") '--納品書社名 
            .AppendLine("	,MSS.kaisyuu1_seikyuusyo_yousi ") '--請求書用紙 
            .AppendLine("	,'0' AS seikyuusyo_syamei ") '--請求書社名 
            .AppendLine("	,'0' AS kankoutyou_kbn ") '--官公庁区分 
            .AppendLine("	,'0' AS keisyou ") '--敬称 
            .AppendLine("	,'' AS syaten_cd ") '--社店コード 
            .AppendLine("	,MSS.kyuu_seikyuu_saki_cd AS torihikisaki_cd ") '--取引先コード 
            .AppendLine("FROM ")
            .AppendLine("	m_seikyuu_saki AS MSS WITH(READCOMMITTED) ") '--請求先マスタ 
            .AppendLine("	INNER JOIN ")
            .AppendLine("	v_seikyuu_saki_info	AS VSS WITH(READCOMMITTED) ") '--請求先情報取得ビュー 
            .AppendLine("	ON ")
            .AppendLine("		MSS.seikyuu_saki_cd = VSS.seikyuu_saki_cd ") '--請求先コード 
            .AppendLine("		AND MSS.seikyuu_saki_brc = VSS.seikyuu_saki_brc ") '--請求先枝番 
            .AppendLine("		AND MSS.seikyuu_saki_kbn = VSS.seikyuu_saki_kbn ") '--請求先区分 
            .AppendLine("ORDER BY ") '--出力順 
            .AppendLine("	tokuisaki_cd ASC ") '--得意先コード 
        End With

        '検索実行()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKakusyuDataSyuturyokuMenuDataSet, _
                    dsKakusyuDataSyuturyokuMenuDataSet.m_seikyuu_sakiCSVTable.TableName)

        Return dsKakusyuDataSyuturyokuMenuDataSet.m_seikyuu_sakiCSVTable
    End Function

    ''' <summary>
    ''' 調査会社マスタのCSV情報取得
    ''' </summary>
    ''' <returns>調査会社マスタCSVテーブル</returns>
    ''' <remarks>調査会社マスタのCSV情報のデータ</remarks>
    ''' <history>2010/07/13 車龍(大連情報システム部)　新規作成</history>
    Public Function Selm_tyousakaisyaCSV() As KakusyuDataSyuturyokuMenuDataSet.m_tyousakaisyaCSVTableDataTable

        ' DataSetインスタンスの生成
        Dim dsKakusyuDataSyuturyokuMenuDataSet As New KakusyuDataSyuturyokuMenuDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	tys_kaisya_cd ") '--調査会社コード 
            .AppendLine("	,jigyousyo_cd ") '--事業所コード 
            .AppendLine("	,torikesi ") '--取消 
            .AppendLine("	,tys_kaisya_mei ") '--調査会社名 
            .AppendLine("	,tys_kaisya_mei_kana ") '--調査会社名カナ 
            .AppendLine("	,seikyuu_saki_shri_saki_mei ") '--請求先支払先名 
            .AppendLine("	,seikyuu_saki_shri_saki_kana ") '--請求先支払先名カナ 
            .AppendLine("	,jyuusyo1 ") '--住所1 
            .AppendLine("	,jyuusyo2 ") '--住所2 
            .AppendLine("	,yuubin_no ") '--郵便番号 
            .AppendLine("	,tel_no ") '--電話番号 
            .AppendLine("	,fax_no ") '--FAX番号 
            .AppendLine("	,pca_siiresaki_cd ") '--PCA用仕入先コード 
            .AppendLine("	,pca_seikyuu_cd ") '--PCA請求先コード 
            .AppendLine("	,seikyuu_saki_cd ") '--請求先コード 
            .AppendLine("	,seikyuu_saki_brc ") '--請求先枝番 
            .AppendLine("	,seikyuu_saki_kbn ") '--請求先区分 
            .AppendLine("	,seikyuu_sime_date ") '--請求締め日 
            .AppendLine("	,skysy_soufu_jyuusyo1 ") '--請求書送付先住所1 
            .AppendLine("	,skysy_soufu_jyuusyo2 ") '--請求書送付先住所2 
            .AppendLine("	,skysy_soufu_yuubin_no ") '--請求書送付先郵便番号 
            .AppendLine("	,skysy_soufu_tel_no ") '--請求書送付先電話番号 
            .AppendLine("	,skk_shri_saki_cd ") '--新会計支払先コード 
            .AppendLine("	,skk_jigyousyo_cd ") '--新会計事業所コード
            .AppendLine("	,shri_meisai_jigyousyo_cd ") '--支払明細集計先事業所コード
            .AppendLine("	,shri_jigyousyo_cd ") '--支払集計先事業所コード 
            .AppendLine("	,shri_sime_date ") '--支払締め日 
            .AppendLine("	,shri_yotei_gessuu ") '--支払予定月数 
            .AppendLine("	,fctring_kaisi_nengetu ") '--ファクタリング開始年月 
            .AppendLine("	,shri_you_fax_no ") '--支払用FAX番号 
            .AppendLine("	,ss_kijyun_kkk ") '--SS基準価格 
            .AppendLine("	,fc_ten_cd ") '--FC店コード 
            .AppendLine("	,kensa_center_cd ") '--検査センターコード
            .AppendLine("	,koj_hkks_tyokusou_flg ") '--工事報告書直送 
            .AppendLine("	,koj_hkks_tyokusou_upd_login_user_id ") '--工事報告書直送変更ログインユーザーID
            .AppendLine("	,koj_hkks_tyokusou_upd_datetime ") '--工事報告書直送変更日時 
            .AppendLine("	,tys_kaisya_flg ") '--調査会社フラグ 
            .AppendLine("	,koj_kaisya_flg ") '--工事会社フラグ 
            .AppendLine("	,japan_kai_kbn ") '--JAPAN会区分
            .AppendLine("	,japan_kai_nyuukai_date ") '--JAPAN会入会年月 
            .AppendLine("	,japan_kai_taikai_date ") '--JAPAN会退会年月 
            .AppendLine("	,fc_ten_kbn ") '--FC店区分 
            .AppendLine("	,fc_nyuukai_date ") '--FC入会年月 
            .AppendLine("	,fc_taikai_date ") '--FC退会年月 
            .AppendLine("	,torikesi_riyuu ") '--取消理由 
            .AppendLine("	,report_jhs_token_flg ") '--ReportJHSトークン有無フラグ 
            .AppendLine("	,tkt_jbn_tys_syunin_skk_flg ") '--宅地地盤調査主任資格有無フラグ 
            .AppendLine("FROM  ")
            .AppendLine("	m_tyousakaisya WITH(READCOMMITTED) ") '--調査会社マスタ 
            .AppendLine("ORDER BY ") '--出力順 
            .AppendLine("	tys_kaisya_cd ASC ") '--調査会社コード 
            .AppendLine("	,jigyousyo_cd ASC ") '--事業所コード
        End With

        '検索実行()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKakusyuDataSyuturyokuMenuDataSet, _
                    dsKakusyuDataSyuturyokuMenuDataSet.m_tyousakaisyaCSVTable.TableName)

        Return dsKakusyuDataSyuturyokuMenuDataSet.m_tyousakaisyaCSVTable
    End Function

    ''' <summary>
    ''' 商品マスタのCSV情報取得
    ''' </summary>
    ''' <returns>商品マスタCSVテーブル</returns>
    ''' <remarks>商品マスタのCSV情報のデータ</remarks>
    ''' <history>2010/07/13 車龍(大連情報システム部)　新規作成</history>
    Public Function Selm_syouhinCSV() As KakusyuDataSyuturyokuMenuDataSet.m_syouhinCSVTableDataTable

        ' DataSetインスタンスの生成
        Dim dsKakusyuDataSyuturyokuMenuDataSet As New KakusyuDataSyuturyokuMenuDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	syouhin_cd ") '--商品コード
            .AppendLine("	,syouhin_mei ") '--商品名
            .AppendLine("	,'0' AS sisutemu_kbn ") '--システム区分
            .AppendLine("	,'0' AS master_kbn ") '--マスター区分
            .AppendLine("	,'0' AS zaikokanri ") '--在庫管理
            .AppendLine("	,'0' AS jiltusekikanri ") '--実績管理
            .AppendLine("	,tani ") '--単位
            .AppendLine("	,'0' AS ikazu ") '--入数
            .AppendLine("	,shri_you_syouhin_mei ") '--規格・型番(支払用商品名)
            .AppendLine("	,'' AS iru ") '--色
            .AppendLine("	,'' AS saizu ") '--サイズ
            .AppendLine("	,syouhin_kbn1 ") '--商品区分1
            .AppendLine("	,syouhin_kbn2 ") '--商品区分2
            .AppendLine("	,syouhin_kbn3 ") '--商品区分3
            .AppendLine("	,zei_kbn ") '--税区分
            .AppendLine("	,zeikomi_kbn ") '--税込区分
            .AppendLine("	,'0' AS tanka ") '--単価
            .AppendLine("	,'0' AS suuryou ") '--数量
            .AppendLine("	,hyoujun_kkk ") '--標準価格
            .AppendLine("	,'0' AS genka ") '--原価
            .AppendLine("	,'0' AS haika1 ") '--売価1
            .AppendLine("	,'0' AS haika2 ") '--売価2
            .AppendLine("	,'0' AS haika3 ") '--売価3
            .AppendLine("	,'0' AS haika4 ") '--売価4
            .AppendLine("	,'0' AS haika5 ") '--売価5
            .AppendLine("	,souko_cd ") '--倉庫コード
            .AppendLine("	,'' AS syusiresaki_cd ") '--主仕入先コード
            .AppendLine("	,'' AS zaikotanka ") '--在庫単価
            .AppendLine("	,siire_kkk ") '--仕入価格
            .AppendLine("FROM ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ") '--商品マスタ
            .AppendLine("ORDER BY ") '--出力順
            .AppendLine("	syouhin_cd ASC ") '--商品コード
        End With

        '検索実行()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKakusyuDataSyuturyokuMenuDataSet, _
                    dsKakusyuDataSyuturyokuMenuDataSet.m_syouhinCSVTable.TableName)

        Return dsKakusyuDataSyuturyokuMenuDataSet.m_syouhinCSVTable
    End Function
    ''' <summary>
    ''' 銀行マスタのCSV情報取得
    ''' </summary>
    ''' <returns>銀行マスタCSVテーブル</returns>
    ''' <remarks>銀行マスタのCSV情報のデータ</remarks>
    ''' <history>2010/07/14 車龍(大連情報システム部)　新規作成</history>
    Public Function Selm_ginkouCSV() As KakusyuDataSyuturyokuMenuDataSet.m_ginkouCSVTableDataTable

        ' DataSetインスタンスの生成
        Dim dsKakusyuDataSyuturyokuMenuDataSet As New KakusyuDataSyuturyokuMenuDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	ginkou_cd ") '--銀行コード
            .AppendLine("	,ginkou_mei ") '--銀行名
            .AppendLine("	,siten_cd ") '--支店コード
            .AppendLine("	,siten_mei ") '--支店名
            .AppendLine("	,saisin_flg ") '--最新フラグ
            .AppendLine("FROM ")
            .AppendLine("	m_ginkou WITH(READCOMMITTED) ") '--銀行マスタ
            .AppendLine("ORDER BY ") '--
            .AppendLine("	ginkou_cd ASC ") '--銀行コード
            .AppendLine("	,siten_cd ASC ") '--支店コード
        End With

        '検索実行()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKakusyuDataSyuturyokuMenuDataSet, _
                    dsKakusyuDataSyuturyokuMenuDataSet.m_ginkouCSVTable.TableName)

        Return dsKakusyuDataSyuturyokuMenuDataSet.m_ginkouCSVTable
    End Function

    ''' <summary>
    ''' 売上データ出力のCSV情報取得
    ''' </summary>
    ''' <returns>売上データ出力CSVテーブル</returns>
    ''' <remarks>売上データ出力のCSV情報のデータ</remarks>
    ''' <history>
    ''' 2010/07/15 車龍(大連情報システム部)　新規作成
    ''' 2015/03/03 曹敬仁(大連情報システム部)　修正
    ''' </history>
    Public Function Seluriage_data_syuturyokuCSV(ByVal fromDate As String, _
                                                 ByVal toDate As String, _
                                                 ByVal lstSeikyuuSakiCd As List(Of String), _
                                                 ByVal lstSeikyuuSakiBrc As List(Of String), _
                                                 ByVal lstSeikyuuSakiKbn As List(Of String) _
                                                ) As KakusyuDataSyuturyokuMenuDataSet.uriage_data_syuturyokuCSVTableDataTable
        'Public Function Seluriage_data_syuturyokuCSV(ByVal fromDate As String, _
        '                                             ByVal toDate As String, _
        '                                             ByVal seikyuu_saki_cd As String, _
        '                                             ByVal seikyuu_saki_brc As String, _
        '                                             ByVal seikyuu_saki_kbn As String _
        '                                            ) As KakusyuDataSyuturyokuMenuDataSet.uriage_data_syuturyokuCSVTableDataTable

        '2015/03/02 曹敬仁(大連情報システム部)　追加  Start
        Dim hasCondition As Boolean = False
        '2015/03/02 曹敬仁(大連情報システム部)　追加  End

        ' DataSetインスタンスの生成
        Dim dsKakusyuDataSyuturyokuMenuDataSet As New KakusyuDataSyuturyokuMenuDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            '.AppendLine("SELECT")
            '.AppendLine("'0' AS denku ") '--伝区")
            '.AppendLine(",TU.denpyou_uri_date ") '--売上年月日")
            '.AppendLine(",ISNULL(TSMK.seikyuusyo_hak_date,TU.seikyuu_date) AS seikyuu_date ") '--請求年月日")
            '.AppendLine(",TU.denpyou_no ") '--伝票No.")
            '.AppendLine(",(TU.seikyuu_saki_cd+'$$$'+TU.seikyuu_saki_brc+'$$$'+TU.seikyuu_saki_kbn) AS tokuisaki_cd ") '--得意先コード")
            '.AppendLine(",TU.seikyuu_saki_mei AS seikyuu_saki_mei ") '--得意先名")
            '.AppendLine(",'' AS tyokusousaki_cd ") '--直送先コード ")
            '.AppendLine(",'' AS senpou_tantou_mei ") '--先方担当者名")
            '.AppendLine(",'0' AS bumon ") '--部門コード")
            '.AppendLine(",'0' AS tantou_cd ") '--担当者コード")
            '.AppendLine(",'0' AS tekiyou ") '--摘要コード")
            ''=======================2011/06/07 車龍 修正 開始↓=====================================
            ' ''2010/10/22 汎用売上テーブル対象とする 付龍追加 ↓
            ' ''.AppendLine(",TJ.sesyu_mei ") '--摘要名(施主名)")
            ''.AppendLine(",CASE ")
            ''.AppendLine("	WHEN TU.himoduke_table_type =  '1' ") '--売上データテーブル.紐付けテーブルタイプ=1の時")
            ''.AppendLine("	THEN TJ.sesyu_mei ") '--地盤テーブル.施主名")
            ''.AppendLine("	WHEN TU.himoduke_table_type =  '9' ") '--売上データテーブル.紐付けテーブルタイプ=9の時")
            ''.AppendLine("	THEN THU.sesyu_mei ") '--汎用売上テーブル.施主名")
            ''.AppendLine("	ELSE '' ")
            ''.AppendLine("	END AS sesyu_mei ") '--摘要名(施主名)")
            ' ''2010/10/22 汎用売上テーブル対象とする 付龍追加 ↑
            '.AppendLine(",CASE ")
            '.AppendLine("	WHEN TJ.kbn IS NOT NULL ") '--地盤テーブルが存在する時
            '.AppendLine("	THEN TJ.sesyu_mei ") '--地盤テーブル.施主名")
            '.AppendLine("	WHEN (TJ.kbn IS NULL) AND (TU.himoduke_table_type =  '9') ") '--上記以外 かつ 売上データテーブル.紐付けテーブルタイプ=9の時")
            '.AppendLine("	THEN THU.sesyu_mei ") '--汎用売上テーブル.施主名")
            '.AppendLine("	ELSE '' ")
            '.AppendLine("	END AS sesyu_mei ") '--摘要名(施主名)")
            ''=======================2011/06/07 車龍 修正 終了↑=====================================
            '.AppendLine(",'' AS bunrui_cd ") '--分類コード")
            '.AppendLine(",'' AS denpyou_kbn ") '--伝票区分")
            '.AppendLine(",TU.syouhin_cd ") '--商品コード")
            '.AppendLine(",'0' AS master_kbn ") '--マスター区分")
            '.AppendLine(",TU.hinmei ") '--品名")
            '.AppendLine(",'0' AS ku ") '--区")
            '.AppendLine(",MS.souko_cd ") '--倉庫コード")
            '.AppendLine(",'0' AS ikazu ") '--入数")
            '.AppendLine(",CASE ")
            '.AppendLine("	WHEN TU.himoduke_table_type =  '1' ") '--売上データテーブル.紐付け元テーブル種別＝1:邸別請求の場合、")
            '.AppendLine("	THEN TU.himoduke_cd ") '--画面表示NO：売上データテーブル.紐付けコード.split('$$$')(4番目)")
            '.AppendLine("	ELSE '0' ")
            '.AppendLine("	END AS hakokazu ") '--箱数")
            '.AppendLine(",TU.suu ") '--数量")
            '.AppendLine(",TU.tani ") '--単位")
            '.AppendLine(",TU.tanka ") '--単価")
            '.AppendLine(",TU.uri_gaku ") '--売上金額")
            '.AppendLine(",'0' AS gentanka ") '--原単価")
            '.AppendLine(",'0' AS genkaga ") '--原価額")
            '.AppendLine(",'' AS ararieki ") '--粗利益")
            '.AppendLine(",TU.sotozei_gaku ") '--外税額")
            '.AppendLine(",'0' AS utizei_gaku ") '--内税額")
            '.AppendLine(",TU.zei_kbn ") '--税区分")
            '.AppendLine(",MS.zeikomi_kbn ") '--税込区分")
            ''=======================2011/06/07 車龍 修正 開始↓=====================================
            ' ''2010/10/22 汎用売上テーブル対象とする 付龍追加 ↓
            ' ''.AppendLine(",(ISNULL(TU.kbn,'') +ISNULL(TU.bangou,'')) AS bikou ") '--備考")
            ''.AppendLine(",CASE ")
            ''.AppendLine("	WHEN TU.himoduke_table_type =  '1' ") '--売上データテーブル.紐付けテーブルタイプ=1の時")
            ''.AppendLine("	THEN ISNULL(TU.kbn,'') +ISNULL(TU.bangou,'') ") '--売上データテーブル.区分＋番号")
            ''.AppendLine("	WHEN TU.himoduke_table_type =  '9' ") '--売上データテーブル.紐付けテーブルタイプ=9の時")
            ''.AppendLine("	THEN ISNULL(THU.kbn,'') +ISNULL(THU.bangou,'') ") '--汎用売上テーブル.区分＋番号")
            ''.AppendLine("	ELSE '' ")
            ''.AppendLine("	END AS bikou ") '--備考")
            ' ''2010/10/22 汎用売上テーブル対象とする 付龍追加 ↑
            '.AppendLine(",(ISNULL(TU.kbn,'') +ISNULL(TU.bangou,'')) AS bikou ") '--備考")
            ''=======================2011/06/07 車龍 修正 終了↑=====================================
            '.AppendLine(",MS.hyoujun_kkk ") '--標準価格")
            '.AppendLine(",'0' AS douji_nyuuka_kbn ") '--同時入荷区分")
            '.AppendLine(",'' AS baitanka ") '--売単価")
            '.AppendLine(",'' AS baika_kingaku ") '--売価金額")
            ''=======================2011/06/07 車龍 修正 開始↓=====================================
            ''.AppendLine(",'' AS kikaku_kataban ")
            '.AppendLine(",ISNULL(MK.kameiten_mei1,'') AS kikaku_kataban ") '--規格・型番")
            ''.AppendLine(",'' AS iro ")
            '.AppendLine(",ISNULL(TU.kameiten_cd,'') AS iro ") '--色")
            ''=======================2011/06/07 車龍 修正 終了↑=====================================
            '.AppendLine(",'' AS saizu ") '--サイズ")
            '.AppendLine("FROM")
            '.AppendLine("	( ") '--抽出期間内の売上データを取得")
            '.AppendLine("	  ") '--伝票種別「UN」と「UR」のセットになっていないデータのみを抽出する")
            '.AppendLine("		SELECT ")
            '.AppendLine("			u1.*")
            '.AppendLine("		FROM")
            '.AppendLine("			t_uriage_data u1 WITH(READCOMMITTED) ") '--売上データテーブル")
            '.AppendLine("		WHERE")
            '.AppendLine("			u1.denpyou_uri_date BETWEEN @fromDate AND @toDate ")
            '.AppendLine("			AND ")
            '.AppendLine("			( ")
            '.AppendLine("				( ") '--マイナス伝票の有るプラス伝票を除外")
            '.AppendLine("					u1.denpyou_syubetu = 'UN' ")
            '.AppendLine("					AND NOT EXISTS")
            '.AppendLine("					( ")
            '.AppendLine("						SELECT")
            '.AppendLine("							* ")
            '.AppendLine("						FROM")
            '.AppendLine("							t_uriage_data u2 WITH(READCOMMITTED) ") '--売上データテーブル")
            '.AppendLine("						WHERE")
            '.AppendLine("							u2.denpyou_uri_date BETWEEN @fromDate AND @toDate ")
            '.AppendLine("							AND	u2.denpyou_syubetu = 'UR' ")
            '.AppendLine("							AND u2.torikesi_moto_denpyou_unique_no = u1.denpyou_unique_no")
            '.AppendLine("					)") '--取消元伝票ユニークNO") '--伝票ユニークNO")
            '.AppendLine("				) ")
            '.AppendLine("				OR ")
            '.AppendLine("				( ") '--プラス伝票の有るマイナス伝票を除外")
            '.AppendLine("					u1.denpyou_syubetu = 'UR' ")
            '.AppendLine("					AND NOT EXISTS")
            '.AppendLine("					( ")
            '.AppendLine("						SELECT")
            '.AppendLine("							* ")
            '.AppendLine("						FROM")
            '.AppendLine("							t_uriage_data u3 WITH(READCOMMITTED) ")
            '.AppendLine("						WHERE")
            '.AppendLine("							u3.denpyou_uri_date BETWEEN @fromDate AND @toDate ")
            '.AppendLine("							AND u3.denpyou_syubetu = 'UN' ")
            '.AppendLine("							AND u3.denpyou_unique_no = u1.torikesi_moto_denpyou_unique_no")
            '.AppendLine("					)")
            '.AppendLine("				)")
            '.AppendLine("			)")
            '.AppendLine("           AND ")
            '.AppendLine("           u1.uri_keijyou_flg = 1 ")
            '.AppendLine("	)TU ") '--売上データテーブル")
            '.AppendLine("	LEFT OUTER JOIN ")
            '.AppendLine("	t_jiban AS TJ WITH(READCOMMITTED) ") '--地盤テーブル")
            '.AppendLine("	ON")
            '.AppendLine("		TU.kbn = TJ.kbn ") '--区分")
            '.AppendLine("		AND TU.bangou = TJ.hosyousyo_no ") '--売上データテーブル.番号 = 地盤テーブル.保証書NO")
            ''2010/10/22 汎用売上テーブル対象とする 付龍追加 ↓
            '.AppendLine("	LEFT OUTER JOIN ")
            '.AppendLine("	t_hannyou_uriage AS THU WITH(READCOMMITTED) ") '--汎用売上テーブル")
            '.AppendLine("	ON")
            '.AppendLine("		TU.himoduke_cd = CONVERT(VARCHAR,THU.han_uri_unique_no) ") '--売上データテーブル.紐付けｺｰﾄﾞ = 汎用売上テーブル.汎用売上ユニークNO")
            ''2010/10/22 汎用売上テーブル対象とする 付龍追加 ↑
            '.AppendLine("	LEFT OUTER JOIN")
            '.AppendLine("		m_syouhin AS MS WITH(READCOMMITTED) ") '--商品マスタ")
            '.AppendLine("	ON")
            '.AppendLine("		TU.syouhin_cd = MS.syouhin_cd ") '--商品コード")
            '.AppendLine("	LEFT JOIN ")
            '.AppendLine("	( ")
            '.AppendLine("		SELECT ")
            '.AppendLine("			TSM.denpyou_unique_no ") '--伝票ユニークNO
            '.AppendLine("			,TSK.seikyuusyo_hak_date ") '--請求書発行日
            '.AppendLine("		FROM ")
            '.AppendLine("			t_seikyuu_meisai AS TSM WITH(READCOMMITTED) ") '--請求明細テーブル
            '.AppendLine("			INNER JOIN ")
            '.AppendLine("			t_seikyuu_kagami AS TSK WITH(READCOMMITTED) ") '--請求鑑テーブル
            '.AppendLine("			ON ")
            '.AppendLine("				TSM.seikyuusyo_no = TSK.seikyuusyo_no ") '--請求書NO
            '.AppendLine("				AND ")
            '.AppendLine("				TSK.torikesi = 0 ") '--取消
            '.AppendLine("	) AS TSMK ")
            '.AppendLine("	ON ")
            '.AppendLine("		TU.denpyou_unique_no = TSMK.denpyou_unique_no ") '--伝票ユニークNO
            ''=======================2011/06/07 車龍 追加 開始↓=====================================
            '.AppendLine("   LEFT JOIN ")
            '.AppendLine("   m_kameiten AS MK WITH(READCOMMITTED) ") '--加盟店マスタ
            '.AppendLine("   ON ")
            '.AppendLine("       TU.kameiten_cd = MK.kameiten_cd ") '--加盟店コード
            ''=======================2011/06/07 車龍 追加 終了↑=====================================

            .AppendLine("SELECT ")
            .AppendLine("	'0' AS denku ")
            .AppendLine("	,TU.denpyou_uri_date ")
            .AppendLine("	,ISNULL(TSMK.seikyuusyo_hak_date,TU.seikyuu_date) AS seikyuu_date ")
            .AppendLine("	,TU.denpyou_no ")
            .AppendLine("	,(TU.seikyuu_saki_cd+'$$$'+TU.seikyuu_saki_brc+'$$$'+TU.seikyuu_saki_kbn) AS tokuisaki_cd ")
            .AppendLine("	,TU.seikyuu_saki_mei AS seikyuu_saki_mei ")
            .AppendLine("	,'' AS tyokusousaki_cd ")
            .AppendLine("	,'' AS senpou_tantou_mei ")
            .AppendLine("	,'0' AS bumon ")
            .AppendLine("	,'0' AS tantou_cd ")
            .AppendLine("	,'0' AS tekiyou ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN TJ.kbn IS NOT NULL ")
            .AppendLine("		THEN TJ.sesyu_mei ")
            .AppendLine("		WHEN (TJ.kbn IS NULL) AND (TU.himoduke_table_type =  '9') ")
            .AppendLine("		THEN THU.sesyu_mei ")
            .AppendLine("		ELSE '' ")
            .AppendLine("		END AS sesyu_mei ")
            .AppendLine("	,'' AS bunrui_cd ")
            .AppendLine("	,'' AS denpyou_kbn ")
            .AppendLine("	,TU.syouhin_cd ")
            .AppendLine("	,'0' AS master_kbn ")
            .AppendLine("	,TU.hinmei ")
            .AppendLine("	,'0' AS ku ")
            .AppendLine("	,MS.souko_cd ")
            .AppendLine("	,'0' AS ikazu ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN TU.himoduke_table_type =  '1' ")
            .AppendLine("		THEN TU.himoduke_cd ")
            .AppendLine("		ELSE '0' ")
            .AppendLine("		END AS hakokazu ")
            .AppendLine("	,TU.suu ")
            .AppendLine("	,TU.tani ")
            .AppendLine("	,TU.tanka ")
            .AppendLine("	,TU.uri_gaku ")
            .AppendLine("	,'0' AS gentanka ")
            .AppendLine("	,'0' AS genkaga ")
            .AppendLine("	,'' AS ararieki ")
            .AppendLine("	,TU.sotozei_gaku ")
            .AppendLine("	,'0' AS utizei_gaku ")
            .AppendLine("	,TU.zei_kbn ")
            .AppendLine("	,MS.zeikomi_kbn ")
            .AppendLine("	,(ISNULL(TU.kbn,'') +ISNULL(TU.bangou,'')) AS bikou ")
            .AppendLine("	,MS.hyoujun_kkk ")
            .AppendLine("	,'0' AS douji_nyuuka_kbn ")
            .AppendLine("	,'' AS baitanka ")
            .AppendLine("	,'' AS baika_kingaku ")
            .AppendLine("	,ISNULL(MK.kameiten_mei1,'') AS kikaku_kataban ")
            .AppendLine("	,ISNULL(TU.kameiten_cd,'') AS iro ")
            .AppendLine("	,'' AS saizu ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			TU.denpyou_unique_no ")
            .AppendLine("			,TU.denpyou_uri_date ")
            .AppendLine("			,TU.seikyuu_date ")
            .AppendLine("			,TU.denpyou_no ")
            .AppendLine("			,TU.seikyuu_saki_cd ")
            .AppendLine("			,TU.seikyuu_saki_brc ")
            .AppendLine("			,TU.seikyuu_saki_kbn ")
            .AppendLine("			,TU.seikyuu_saki_mei ")
            .AppendLine("			,TU.himoduke_table_type ")
            .AppendLine("			,TU.syouhin_cd ")
            .AppendLine("			,TU.hinmei ")
            .AppendLine("			,TU.himoduke_cd ")
            .AppendLine("			,TU.suu ")
            .AppendLine("			,TU.tani ")
            .AppendLine("			,TU.tanka ")
            .AppendLine("			,TU.uri_gaku ")
            .AppendLine("			,TU.sotozei_gaku ")
            .AppendLine("			,TU.zei_kbn ")
            .AppendLine("			,TU.kbn ")
            .AppendLine("			,TU.bangou ")
            .AppendLine("			,TU.kameiten_cd ")
            .AppendLine("		FROM ")
            .AppendLine("			t_uriage_data TU WITH(READCOMMITTED) ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			t_uriage_data u2 WITH(READCOMMITTED) ")
            .AppendLine("			ON ")
            .AppendLine("				u2.torikesi_moto_denpyou_unique_no = TU.denpyou_unique_no ")
            .AppendLine("				AND ")
            .AppendLine("				u2.denpyou_syubetu = 'UR' ")
            .AppendLine("				AND ")
            .AppendLine("				u2.denpyou_uri_date BETWEEN @fromDate AND @toDate ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			t_uriage_data u3 WITH(READCOMMITTED) ")
            .AppendLine("			ON ")
            .AppendLine("				u3.denpyou_unique_no = TU.torikesi_moto_denpyou_unique_no ")
            .AppendLine("				AND ")
            .AppendLine("				u3.denpyou_syubetu = 'UN' ")
            .AppendLine("				AND ")
            .AppendLine("				u3.denpyou_uri_date BETWEEN @fromDate AND @toDate ")
            .AppendLine("				 ")
            .AppendLine("		WHERE ")
            .AppendLine("			TU.denpyou_uri_date BETWEEN @fromDate AND @toDate ")
            .AppendLine("			AND ")
            .AppendLine("			TU.uri_keijyou_flg = 1 ")
            .AppendLine("			AND ")
            .AppendLine("			( ")
            .AppendLine("				( ")
            .AppendLine("					TU.denpyou_syubetu = 'UN' ")
            .AppendLine("					AND ")
            .AppendLine("					u2.denpyou_unique_no IS NULL ")
            .AppendLine("				) ")
            .AppendLine("				OR ")
            .AppendLine("				( ")
            .AppendLine("					TU.denpyou_syubetu = 'UR' ")
            .AppendLine("					AND ")
            .AppendLine("					u3.denpyou_unique_no IS NULL ")
            .AppendLine("				) ")
            .AppendLine("			) ")
            .AppendLine("	) AS TU ")
            .AppendLine("	LEFT OUTER JOIN ")
            .AppendLine("	t_jiban AS TJ WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		TU.kbn = TJ.kbn ")
            .AppendLine("		AND TU.bangou = TJ.hosyousyo_no ")
            .AppendLine("	LEFT OUTER JOIN ")
            .AppendLine("	t_hannyou_uriage AS THU WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		TU.himoduke_cd = CONVERT(VARCHAR,THU.han_uri_unique_no) ")
            .AppendLine("	LEFT OUTER JOIN ")
            .AppendLine("		m_syouhin AS MS WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		TU.syouhin_cd = MS.syouhin_cd ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			TSM.denpyou_unique_no ")
            .AppendLine("			,TSK.seikyuusyo_hak_date ")
            .AppendLine("		FROM ")
            .AppendLine("			t_seikyuu_meisai AS TSM WITH(READCOMMITTED) ")
            .AppendLine("			INNER JOIN ")
            .AppendLine("			t_seikyuu_kagami AS TSK WITH(READCOMMITTED) ")
            .AppendLine("			ON ")
            .AppendLine("				TSM.seikyuusyo_no = TSK.seikyuusyo_no ")
            .AppendLine("				AND ")
            .AppendLine("				TSK.torikesi = 0 ")
            .AppendLine("	) AS TSMK ")
            .AppendLine("	ON ")
            .AppendLine("		TU.denpyou_unique_no = TSMK.denpyou_unique_no ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kameiten AS MK WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		TU.kameiten_cd = MK.kameiten_cd ")

            '2015/03/02 曹敬仁(大連情報システム部)　修正  Start
            'If seikyuu_saki_cd <> "" Then
            '    .AppendLine("WHERE")
            '    .AppendLine("	TU.seikyuu_saki_cd = @seikyuu_saki_cd ") '--請求先コード")
            '    .AppendLine("	AND TU.seikyuu_saki_brc = @seikyuu_saki_brc ") '--請求先枝番")
            '    .AppendLine("	AND TU.seikyuu_saki_kbn = @seikyuu_saki_kbn ") '--請求先区分")
            'End If
            For i As Integer = 0 To lstSeikyuuSakiCd.Count - 1
                If lstSeikyuuSakiCd(i) <> "" Then
                    If Not hasCondition Then
                        .AppendLine(" WHERE")
                        .AppendLine("	(TU.seikyuu_saki_cd = @seikyuu_saki_cd" & i & "") '--請求先コード"
                        .AppendLine("	AND TU.seikyuu_saki_brc = @seikyuu_saki_brc" & i & "") '--請求先枝番
                        .AppendLine("	AND TU.seikyuu_saki_kbn = @seikyuu_saki_kbn" & i & ")") '--請求先区分
                        hasCondition = True
                    Else
                        .AppendLine(" OR")
                        .AppendLine("	(TU.seikyuu_saki_cd = @seikyuu_saki_cd" & i & "") '--請求先コード
                        .AppendLine("	AND TU.seikyuu_saki_brc = @seikyuu_saki_brc" & i & "") '--請求先枝番
                        .AppendLine("	AND TU.seikyuu_saki_kbn = @seikyuu_saki_kbn" & i & ")") '--請求先区分
                    End If
                End If
            Next
            '2015/03/02 曹敬仁(大連情報システム部)　修正  End
            .AppendLine("ORDER BY ") '--出力順
            .AppendLine("   TU.denpyou_uri_date ASC ") '--売上年月日
            .AppendLine("   ,TU.denpyou_no ASC ") '--伝票No.
        End With

        'SQL文字列中でパラメータ
        With paramList
            .Add(MakeParam("@fromDate", SqlDbType.Char, 10, fromDate))
            .Add(MakeParam("@toDate", SqlDbType.Char, 10, toDate))
            '2015/03/02 曹敬仁(大連情報システム部)　修正  Start
            'If seikyuu_saki_cd <> "" Then
            '    .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, seikyuu_saki_cd))
            '    .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, seikyuu_saki_brc))
            '    .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, seikyuu_saki_kbn))
            'End If
            For i As Integer = 0 To lstSeikyuuSakiCd.Count - 1
                If lstSeikyuuSakiCd(i) <> "" Then
                    .Add(MakeParam("@seikyuu_saki_cd" & i, SqlDbType.VarChar, 5, lstSeikyuuSakiCd(i)))
                    .Add(MakeParam("@seikyuu_saki_brc" & i, SqlDbType.VarChar, 2, lstSeikyuuSakiBrc(i)))
                    .Add(MakeParam("@seikyuu_saki_kbn" & i, SqlDbType.VarChar, 1, lstSeikyuuSakiKbn(i)))
                End If
            Next
            '2015/03/02 曹敬仁(大連情報システム部)　修正  End
        End With

        '検索実行()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKakusyuDataSyuturyokuMenuDataSet, _
                    dsKakusyuDataSyuturyokuMenuDataSet.uriage_data_syuturyokuCSVTable.TableName, paramList.ToArray)

        Return dsKakusyuDataSyuturyokuMenuDataSet.uriage_data_syuturyokuCSVTable
    End Function

    ''' <summary>
    ''' 仕入データ出力のCSV情報取得
    ''' </summary>
    ''' <returns>仕入データ出力CSVテーブル</returns>
    ''' <remarks>仕入データ出力のCSV情報のデータ</remarks>
    ''' <history>2010/07/16 車龍(大連情報システム部)　新規作成</history>
    Public Function Selsiire_data_syuturyokuCSV(ByVal fromDate As String, _
                                                ByVal toDate As String, _
                                                ByVal strSiireCd As String _
                                               ) As KakusyuDataSyuturyokuMenuDataSet.siire_data_syuturyokuCSVTableDataTable

        ' DataSetインスタンスの生成
        Dim dsKakusyuDataSyuturyokuMenuDataSet As New KakusyuDataSyuturyokuMenuDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	'0' AS nyuuka_houhou ") '--入荷方法 ")
            .AppendLine("	,'0' AS kamoku_kbn ") '--科目区分 ")
            .AppendLine("	,'0' AS denku ") '--伝区 ")
            .AppendLine("	,TS.denpyou_siire_date ") '--入荷年月日 ")
            .AppendLine("	,'' AS seisan_date ") '--精算年月日 ")
            .AppendLine("	,TS.denpyou_no ") '--伝票No. ")
            .AppendLine("	,(TS.tys_kaisya_cd+'$$$'+TS.tys_kaisya_jigyousyo_cd) AS siiresaki_cd ") '--仕入先コード ")
            .AppendLine("	,TS.tys_kaisya_mei ") '--仕入先名 ")
            .AppendLine("	,'' AS senpou_tantou_mei ") '--先方担当者名 ")
            .AppendLine("	,'0' AS bumon ") '--部門コード ")
            .AppendLine("	,'0' AS tantou_cd ") '--担当者コード ")
            .AppendLine("	,'0' AS tekiyou ") '--摘要コード ")
            '=======================2011/06/07 車龍 修正 開始↓=====================================
            '2010/10/22 汎用仕入テーブル対象とする 付龍追加 ↓
            '.AppendLine("	,TJ.sesyu_mei ") '--摘要名(施主名) ")
            '.AppendLine(",CASE ")
            '.AppendLine("	WHEN TS.himoduke_table_type =  '1' ") '--仕入データテーブル.紐付けテーブルタイプ=1の時")
            '.AppendLine("	THEN TJ.sesyu_mei ") '--地盤テーブル.施主名")
            '.AppendLine("	WHEN TS.himoduke_table_type =  '9' ") '--仕入データテーブル.紐付けテーブルタイプ=9の時")
            '.AppendLine("	THEN THS.sesyu_mei ") '--汎用仕入テーブル.施主名")
            '.AppendLine("	ELSE '' ")
            '.AppendLine("	END AS sesyu_mei ") '--摘要名(施主名)")
            ''2010/10/22 汎用仕入テーブル対象とする 付龍追加 ↑
            .AppendLine(",CASE ")
            .AppendLine("	WHEN TJ.kbn IS NOT NULL ") '--地盤テーブルが存在する時
            .AppendLine("	THEN TJ.sesyu_mei ") '--地盤テーブル.施主名")
            .AppendLine("	WHEN (TJ.kbn IS NULL) AND (TS.himoduke_table_type =  '9') ") '--上記以外 かつ 仕入データテーブル.紐付けテーブルタイプ=9の時
            .AppendLine("	THEN THS.sesyu_mei ") '--汎用仕入テーブル.施主名")
            .AppendLine("	ELSE '' ")
            .AppendLine("	END AS sesyu_mei ") '--摘要名(施主名)")
            '=======================2011/06/07 車龍 修正 終了↑=====================================
            .AppendLine("	,TS.syouhin_cd ") '--商品コード ")
            .AppendLine("	,'0' AS master_kbn ") '--マスター区分 ")
            .AppendLine("	,TS.hinmei ") '--品名 ")
            .AppendLine("	,'' AS ku ") '--区 ")
            .AppendLine("	,'' AS souko_cd ") '--倉庫コード ")
            .AppendLine("	,'0' AS ikazu ") '--入数 ")
            .AppendLine("	,'0' AS hakokazu ") '--箱数 ")
            .AppendLine("	,TS.suu ") '--数量 ")
            .AppendLine("	,TS.tani ") '--単位 ")
            .AppendLine("	,TS.tanka ") '--単価 ")
            .AppendLine("	,TS.siire_gaku ") '--金額 ")
            .AppendLine("	,TS.sotozei_gaku ") '--外税額 ")
            .AppendLine("	,'0' AS utizei_gaku ") '--内税額 ")
            .AppendLine("	,TS.zei_kbn ") '--税区分 ")
            .AppendLine("	,'0' AS zeikomi_kbn ") '--税込区分 ")
            '=======================2011/06/07 車龍 修正 開始↓=====================================
            ''2010/10/22 汎用仕入テーブル対象とする 付龍追加 ↓
            ''.AppendLine("	,(ISNULL(TS.kbn,'')+ISNULL(TS.bangou,'')) AS bikou ") '--備考 ")
            '.AppendLine(",CASE ")
            '.AppendLine("	WHEN TS.himoduke_table_type =  '1' ") '--仕入データテーブル.紐付けテーブルタイプ=1の時")
            '.AppendLine("	THEN ISNULL(TS.kbn,'') +ISNULL(TS.bangou,'') ") '--仕入データテーブル.区分＋番号")
            '.AppendLine("	WHEN TS.himoduke_table_type =  '9' ") '--仕入データテーブル.紐付けテーブルタイプ=9の時")
            '.AppendLine("	THEN ISNULL(THS.kbn,'') +ISNULL(THS.bangou,'') ") '--汎用仕入テーブル.区分＋番号")
            '.AppendLine("	ELSE '' ")
            '.AppendLine("	END AS bikou ") '--備考")
            ''2010/10/22 汎用仕入テーブル対象とする 付龍追加 ↑
            .AppendLine("	,(ISNULL(TS.kbn,'')+ISNULL(TS.bangou,'')) AS bikou ") '--備考(仕入データテーブル.区分＋番号) 
            '=======================2011/06/07 車龍 修正 終了↑=====================================
            .AppendLine("	,'' AS kikaku_kataban ") '--規格・型番 ")
            .AppendLine("	,'' AS iro ") '--色 ")
            .AppendLine("	,'' AS saizu ") '--サイズ ")
            .AppendLine("FROM ")
            .AppendLine("	( ") '--抽出期間内の仕入データを取得 ")
            .AppendLine("	  ") '--伝票種別「SN」と「SR」のセットになっていないデータのみを抽出する ")
            .AppendLine("		SELECT  ")
            .AppendLine("			s1.* ")
            .AppendLine("		FROM ")
            .AppendLine("			t_siire_data s1 WITH(READCOMMITTED) ") '--仕入データテーブル ")
            .AppendLine("		WHERE ")
            .AppendLine("			s1.denpyou_siire_date BETWEEN @fromDate AND @toDate ")
            .AppendLine("			AND  ")
            .AppendLine("			(  ")
            .AppendLine("				( ") '--マイナス伝票の有るプラス伝票を除外 ")
            .AppendLine("					s1.denpyou_syubetu = 'SN'  ")
            .AppendLine("					AND NOT EXISTS ")
            .AppendLine("					(  ")
            .AppendLine("						SELECT ")
            .AppendLine("							*  ")
            .AppendLine("						FROM ")
            .AppendLine("							t_siire_data s2 WITH(READCOMMITTED) ") '--仕入データテーブル ")
            .AppendLine("						WHERE ")
            .AppendLine("							s2.denpyou_siire_date BETWEEN @fromDate AND @toDate ")
            .AppendLine("							AND	s2.denpyou_syubetu = 'SR'  ")
            .AppendLine("							AND s2.torikesi_moto_denpyou_unique_no = s1.denpyou_unique_no ")
            .AppendLine("					)") '--取消元伝票ユニークNO") '--伝票ユニークNO ")
            .AppendLine("				)  ")
            .AppendLine("				OR  ")
            .AppendLine("				( ") '--プラス伝票の有るマイナス伝票を除外 ")
            .AppendLine("					s1.denpyou_syubetu = 'SR'  ")
            .AppendLine("					AND NOT EXISTS ")
            .AppendLine("					(  ")
            .AppendLine("						SELECT ")
            .AppendLine("							*  ")
            .AppendLine("						FROM ")
            .AppendLine("							t_siire_data s3 WITH(READCOMMITTED)  ")
            .AppendLine("						WHERE ")
            .AppendLine("							s3.denpyou_siire_date BETWEEN @fromDate AND @toDate ")
            .AppendLine("							AND s3.denpyou_syubetu = 'SN'  ")
            .AppendLine("							AND s3.denpyou_unique_no = s1.torikesi_moto_denpyou_unique_no ")
            .AppendLine("					) ")
            .AppendLine("				) ")
            .AppendLine("			) ")
            .AppendLine("           AND ")
            .AppendLine("           s1.siire_keijyou_flg = 1 ")
            .AppendLine("	)TS ") '--仕入データテーブル ")
            .AppendLine("	INNER JOIN m_tyousakaisya AS MT WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		 MT.tys_kaisya_cd = TS.tys_kaisya_cd ") '--調査会社コード ")
            .AppendLine("		 AND MT.jigyousyo_cd = TS.tys_kaisya_jigyousyo_cd ") '--事業所コード ")
            .AppendLine("	LEFT OUTER JOIN  ")
            .AppendLine("	t_jiban AS TJ WITH(READCOMMITTED) ") '--地盤テーブル ")
            .AppendLine("	ON ")
            .AppendLine("		TS.kbn = TJ.kbn ") '--区分 ")
            .AppendLine("		AND TS.bangou = TJ.hosyousyo_no ") '--仕入データテーブル.番号 = 地盤テーブル.保証書NO ")
            '2010/10/22 汎用仕入テーブル対象とする 付龍追加 ↓
            .AppendLine("	LEFT OUTER JOIN ")
            .AppendLine("	t_hannyou_siire AS THS WITH(READCOMMITTED) ") '--汎用仕入テーブル")
            .AppendLine("	ON")
            .AppendLine("		TS.himoduke_cd = CONVERT(VARCHAR,THS.han_siire_unique_no) ") '--仕入データテーブル.紐付けｺｰﾄﾞ = 汎用仕入テーブル.汎用仕入ユニークNO")
            '2010/10/22 汎用仕入テーブル対象とする 付龍追加 ↑
            If strSiireCd <> "" Then
                .AppendLine("WHERE ")
                .AppendLine("	MT.tys_kaisya_cd + MT.jigyousyo_cd = @strSiireCd ") '--") '--調査会社コード + 事業所コード ")
            End If
            .AppendLine("ORDER BY ") '--出力順 ")
            .AppendLine("	TS.denpyou_siire_date ASC ") '--入荷年月日 ")
            .AppendLine("	,TS.denpyou_no ASC ") '--伝票No. ")
        End With

        'SQL文字列中でパラメータ
        With paramList
            .Add(MakeParam("@fromDate", SqlDbType.Char, 10, fromDate))
            .Add(MakeParam("@toDate", SqlDbType.Char, 10, toDate))
            If strSiireCd <> "" Then
                .Add(MakeParam("@strSiireCd", SqlDbType.VarChar, 7, strSiireCd))
            End If
        End With

        '検索実行()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKakusyuDataSyuturyokuMenuDataSet, _
                    dsKakusyuDataSyuturyokuMenuDataSet.siire_data_syuturyokuCSVTable.TableName, paramList.ToArray)

        Return dsKakusyuDataSyuturyokuMenuDataSet.siire_data_syuturyokuCSVTable
    End Function


    ''' <summary>
    ''' 請求先情報取得
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="seikyuuSakiMei">請求先名</param>
    ''' <param name="seikyuuSakiKana">請求先カナ</param>
    ''' <param name="torikesiFlg">取消データ対象フラグ(true:対象にしない / false:対象にする)</param>
    ''' <returns>データテーブル</returns>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Public Function searchSeikyuuSakiInfo(ByVal seikyuuSakiCd As String, _
                                          ByVal seikyuuSakiBrc As String, _
                                          ByVal seikyuuSakiKbn As String, _
                                          ByVal seikyuuSakiMei As String, _
                                          ByVal seikyuuSakiKana As String, _
                                          Optional ByVal torikesiFlg As Boolean = False _
                                          ) As DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New Data.DataSet

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("     LTRIM(RTRIM(seikyuu_saki_cd)) seikyuu_saki_cd ")
        commandTextSb.Append("   , LTRIM(RTRIM(seikyuu_saki_brc)) seikyuu_saki_brc ")
        commandTextSb.Append("   , LTRIM(RTRIM(seikyuu_saki_kbn)) seikyuu_saki_kbn ")
        commandTextSb.Append("   , seikyuu_saki_mei ")
        commandTextSb.Append("   , seikyuu_saki_kana ")
        commandTextSb.Append("   , skysy_soufu_jyuusyo1 ")
        commandTextSb.Append("   , skysy_soufu_jyuusyo2 ")
        commandTextSb.Append("   , skysy_soufu_yuubin_no ")
        commandTextSb.Append("   , skysy_soufu_tel_no ")
        commandTextSb.Append("   , skysy_soufu_fax_no  ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("   " & connDBUser & "v_seikyuu_saki_info  ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("   0 = 0 ")
        If seikyuuSakiCd IsNot String.Empty Then
            commandTextSb.Append("   AND seikyuu_saki_cd LIKE @seikyuuSakiCd")
        End If
        If seikyuuSakiBrc IsNot String.Empty Then
            commandTextSb.Append("   AND seikyuu_saki_brc = @seikyuuSakiBrc")
        End If
        If seikyuuSakiKbn IsNot String.Empty Then
            commandTextSb.Append("   AND seikyuu_saki_kbn = @seikyuuSakiKbn")
        End If
        If seikyuuSakiMei IsNot String.Empty Then
            commandTextSb.Append("   AND seikyuu_saki_mei LIKE @seikyuuSakiMei")
        End If
        If seikyuuSakiKana IsNot String.Empty Then
            commandTextSb.Append("   AND seikyuu_saki_kana LIKE @seikyuuSakiKana")
        End If
        If torikesiFlg Then
            commandTextSb.Append("   AND ISNULL(torikesi,1) = 0 ")
        End If

        'SQL文字列中でパラメータ
        With paramList
            .Add(MakeParam("@seikyuuSakiCd", SqlDbType.VarChar, 7, seikyuuSakiCd & "%"))
            .Add(MakeParam("@seikyuuSakiBrc", SqlDbType.VarChar, 2, seikyuuSakiBrc))
            .Add(MakeParam("@seikyuuSakiKbn", SqlDbType.VarChar, 1, seikyuuSakiKbn))
            .Add(MakeParam("@seikyuuSakiMei", SqlDbType.VarChar, 100, seikyuuSakiMei & "%"))
            .Add(MakeParam("@seikyuuSakiKana", SqlDbType.VarChar, 100, seikyuuSakiKana & "%"))
        End With

        '検索実行()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        'データ取得＆返却
        Return dsDataSet.Tables(0)

    End Function

    Enum EnumTyousakaisyaKensakuType
        ''' <summary>
        ''' 調査会社
        ''' </summary>
        ''' <remarks></remarks>
        TYOUSAKAISYA = 0
        ''' <summary>
        ''' 仕入先
        ''' </summary>
        ''' <remarks></remarks>
        SIIRESAKI = 1
        ''' <summary>
        ''' 支払先
        ''' </summary>
        ''' <remarks></remarks>
        SIHARAISAKI = 2
        ''' <summary>
        ''' 工事会社
        ''' </summary>
        ''' <remarks></remarks>
        KOUJIKAISYA = 3
    End Enum

    ''' <summary>
    ''' 調査会社マスタの検索を行う
    ''' </summary>
    ''' <param name="strTysKaiCd">調査会社ｺｰﾄﾞ</param>
    ''' <param name="strJigyousyoCd">事業所ｺｰﾄﾞ</param>
    ''' <param name="strTysKaiNm">調査会社名</param>
    ''' <param name="strTysKaiKana">調査会社名ｶﾅ</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <param name="kameitenCd">加盟店コード(可否区分チェック用)</param>
    ''' <param name="kensakuType">検索タイプ(EnumTyousakaisyakensakuType)</param>
    ''' <returns>TyousakaisyaSearchTableDataTable</returns>
    ''' <history>2010/07/19 馬艶軍(大連情報システム部)　新規作成</history>
    Public Function GetTyousakaisyaKensakuData(ByVal strTysKaiCd As String, _
                                      ByVal strJigyousyoCd As String, _
                                      ByVal strTysKaiNm As String, _
                                      ByVal strTysKaiKana As String, _
                                      ByVal blnDelete As Boolean, _
                                      ByVal kameitenCd As String, _
                                      ByVal kensakuType As EnumTyousakaisyaKensakuType _
                                      ) As DataTable

        ' DataSetインスタンスの生成
        Dim dsDataSet As New Data.DataSet

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' パラメータ
        Const strParamTysKaiCd As String = "@TYSKAICD"
        Const strParamJigyousyoCd As String = "@JIGYOUSHOCD"
        Const strParamTysKaiNm As String = "@TYSKAINM"
        Const strParamTysKaiKana As String = "@TYSKAIKANA"
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("  t.tys_kaisya_cd, ")
        commandTextSb.Append("  t.jigyousyo_cd, ")
        commandTextSb.Append("  t.torikesi, ")
        commandTextSb.Append("  t.tys_kaisya_mei, ")
        commandTextSb.Append("  t.tys_kaisya_mei_kana, ")
        commandTextSb.Append("  t.seikyuu_saki_shri_saki_mei, ")
        commandTextSb.Append("  t.seikyuu_saki_shri_saki_kana, ")
        commandTextSb.Append("  t.jyuusyo1, ")
        commandTextSb.Append("  t.jyuusyo2, ")
        commandTextSb.Append("  t.yuubin_no, ")
        commandTextSb.Append("  t.tel_no, ")
        commandTextSb.Append("  t.fax_no, ")
        commandTextSb.Append("  t.pca_siiresaki_cd, ")
        commandTextSb.Append("  t.pca_seikyuu_cd, ")
        commandTextSb.Append("  t.seikyuu_saki_cd, ")
        commandTextSb.Append("  t.seikyuu_saki_brc, ")
        commandTextSb.Append("  t.seikyuu_saki_kbn, ")
        commandTextSb.Append("  t.seikyuu_sime_date, ")
        commandTextSb.Append("  t.ss_kijyun_kkk, ")
        commandTextSb.Append("  t.fc_ten_cd, ")

        If kameitenCd = "" Then
            commandTextSb.Append(" 5 As kahi_kbn")
            commandTextSb.Append(" FROM m_tyousakaisya t WITH (READCOMMITTED) ")
        Else
            commandTextSb.Append(" ISNULL(k.kahi_kbn,5) As kahi_kbn")
            commandTextSb.Append(" FROM m_tyousakaisya t WITH (READCOMMITTED) ")
            commandTextSb.Append(" LEFT OUTER JOIN m_kameiten_tyousakaisya k WITH (READCOMMITTED) ")
            commandTextSb.Append(" ON t.tys_kaisya_cd = k.tys_kaisya_cd ")
            commandTextSb.Append(" AND t.jigyousyo_cd = k.jigyousyo_cd ")
            '検索タイプが工事会社場合、会社区分=2、それ以外の場合、1
            If kensakuType = EnumTyousakaisyaKensakuType.KOUJIKAISYA Then
                commandTextSb.Append(" AND k.kaisya_kbn = 2 ")
            Else
                commandTextSb.Append(" AND k.kaisya_kbn = 1 ")
            End If
            commandTextSb.Append(" AND k.kameiten_cd = " & strParamKameitenCd)
        End If

        commandTextSb.Append(" WHERE ")
        If blnDelete = True Then
            commandTextSb.Append(" t.torikesi = 0 ")
        Else
            commandTextSb.Append(" 0 = 0 ")
        End If
        '調査会社コードは「調査会社コード ＋ 事業所コード」で検索を行う
        If strTysKaiCd <> "" Then
            commandTextSb.Append(" AND t.tys_kaisya_cd + t.jigyousyo_cd Like " & strParamTysKaiCd)
        End If
        '会社名検索条件
        If strTysKaiNm <> "" Then
            '検索タイプ別に、検索先のカラムを変える
            If kensakuType = EnumTyousakaisyaKensakuType.SIHARAISAKI Then
                '支払先検索の場合、請求先支払先名で検索
                commandTextSb.Append(" AND t.seikyuu_saki_shri_saki_mei Like " & strParamTysKaiNm)
            Else
                '上記以外の場合、調査会社名カナで検索
                commandTextSb.Append(" AND t.tys_kaisya_mei Like " & strParamTysKaiNm)
            End If
        End If
        '会社名カナ検索条件
        If strTysKaiKana <> "" Then
            '検索タイプ別に、検索先のカラムを変える
            If kensakuType = EnumTyousakaisyaKensakuType.SIHARAISAKI Then
                '支払先検索の場合、請求先支払先名カナで検索
                commandTextSb.Append(" AND t.seikyuu_saki_shri_saki_kana Like " & strParamTysKaiKana)
            Else
                '上記以外の場合、調査会社名カナで検索
                commandTextSb.Append(" AND t.tys_kaisya_mei_kana Like " & strParamTysKaiKana)
            End If
        End If
        '検索タイプが支払先の場合、事業所コード＝支払集計先事業所コードのレコードのみを対象とする
        If kensakuType = EnumTyousakaisyaKensakuType.SIHARAISAKI Then
            commandTextSb.Append(" AND t.jigyousyo_cd = t.shri_jigyousyo_cd ")
        End If
        '加盟店コードが指定されている場合、並び替え順序を調整
        If kameitenCd = "" Then
            commandTextSb.Append(" ORDER BY t.tys_kaisya_mei_kana")
        Else
            commandTextSb.Append(" ORDER BY kahi_kbn, k.nyuuryoku_no, t.tys_kaisya_mei_kana")
        End If

        'SQL文字列中でパラメータ
        With paramList
            .Add(MakeParam(strParamTysKaiCd, SqlDbType.VarChar, 7, strTysKaiCd & Chr(37)))
            .Add(MakeParam(strParamJigyousyoCd, SqlDbType.VarChar, 2, strJigyousyoCd))
            .Add(MakeParam(strParamTysKaiNm, SqlDbType.VarChar, 42, strTysKaiNm & Chr(37)))
            .Add(MakeParam(strParamTysKaiKana, SqlDbType.VarChar, 22, strTysKaiKana & Chr(37)))
            .Add(MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, kameitenCd))
        End With

        '検索実行()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        'データ取得＆返却
        Return dsDataSet.Tables(0)

    End Function

End Class
