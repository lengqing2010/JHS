Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class KakusyuDataSyuturyokuMenuDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    '�f�[�^�x�[�X�Ǘ���
    Private connDBUser As String = System.Configuration.ConfigurationManager.AppSettings("connDBUser").ToString

    ''' <summary>
    ''' �V�X�e������
    ''' </summary>
    ''' <returns></returns>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetSysTime() As String
        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine("SELECT   ")
            .AppendLine("   GETDATE() ")
        End With

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet")

        '�߂�
        Return CDate(dsDataSet.Tables(0).Rows(0).Item(0)).ToString("yyyy/MM/dd")

    End Function

    ''' <summary>
    ''' EXCEL�d�󔄏�f�[�^�o��
    ''' </summary>
    ''' <param name="strDateFrom">���o����FROM</param>
    ''' <param name="strDateTo">���o����TO</param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetExcelSiwakeUriage(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.ExcelSiwakeUriageDataTableDataTable
        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New ExcelSiwakeDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
            '2010/10/07 ����f�[�^�́A���㏈��FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.uri_keijyou_flg,'0') = '1'  ")
            '2010/10/07 ����f�[�^�́A���㏈��FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
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
            '2010/10/07 ����f�[�^�́A���㏈��FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.uri_keijyou_flg,'0') = '1'  ")
            '2010/10/07 ����f�[�^�́A���㏈��FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
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
            '2010/10/07 ����f�[�^�́A���㏈��FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.uri_keijyou_flg,'0') = '1'  ")
            '2010/10/07 ����f�[�^�́A���㏈��FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
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
            '2010/10/07 ����f�[�^�́A���㏈��FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.uri_keijyou_flg,'0') = '1'  ")
            '2010/10/07 ����f�[�^�́A���㏈��FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
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
            '2010/10/07 ����f�[�^�́A���㏈��FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.uri_keijyou_flg,'0') = '1'  ")
            '2010/10/07 ����f�[�^�́A���㏈��FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
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
            '2010/10/07 ����f�[�^�́A���㏈��FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(tu.uri_keijyou_flg,'0') = '1'  ")
            '2010/10/07 ����f�[�^�́A���㏈��FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
            .AppendLine("		GROUP BY ")
            .AppendLine("			ms.skk_jigyousyo_cd ")
            .AppendLine("	) t  ")

            '�ݕ����z����0
            .AppendLine("WHERE ")
            .AppendLine("   ISNULL(kasi_kingaku,0) <> 0 ")

            .AppendLine("ORDER BY ")
            .AppendLine("	t.kari_aitesaki ")
            .AppendLine("	,t.kasi_zei ")
        End With

        paramList.Add(MakeParam("@fromDate", SqlDbType.VarChar, 10, strDateFrom))
        paramList.Add(MakeParam("@toDate", SqlDbType.VarChar, 10, strDateTo))
        paramList.Add(MakeParam("@tekiyou", SqlDbType.VarChar, 255, "����h�e" & SetZenkakuSuuji(Right(strDateFrom, 5)) & "�`" & SetZenkakuSuuji(Right(strDateTo, 5))))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.ExcelSiwakeUriageDataTable.TableName, paramList.ToArray)

        '�߂�
        Return dsDataSet.ExcelSiwakeUriageDataTable
    End Function

    ''' <summary>
    ''' EXCEL�d��d���f�[�^�o��
    ''' </summary>
    ''' <param name="strDateFrom">���o����FROM</param>
    ''' <param name="strDateTo">���o����TO</param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetExcelSiwakeSiire(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.ExcelSiwakeSiireDataTableDataTable
        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New ExcelSiwakeDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
            .AppendLine("			m_tyousakaisya mt         --������Ѓ}�X�^(�d��) ")
            .AppendLine("		ON  ")
            .AppendLine("			ts.tys_kaisya_cd = mt.tys_kaisya_cd ")
            .AppendLine("		AND  ")
            .AppendLine("			ts.tys_kaisya_jigyousyo_cd = mt.jigyousyo_cd ")
            .AppendLine("		INNER JOIN  ")
            .AppendLine("			m_tyousakaisya mts        --������Ѓ}�X�^(�x��:���Ə��R�[�h=�x���W�v�掖�Ə��R�[�h) ")
            .AppendLine("		ON  ")
            .AppendLine("			mt.tys_kaisya_cd = mts.tys_kaisya_cd ")

            '�C�����
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
            '2010/10/07 �d���f�[�^�́A�d������FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(ts.siire_keijyou_flg,'0') = '1'  ")
            '2010/10/07 �d���f�[�^�́A�d������FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
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

            '�C�����
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
            '2010/10/07 �d���f�[�^�́A�d������FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(ts.siire_keijyou_flg,'0') = '1'  ")
            '2010/10/07 �d���f�[�^�́A�d������FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
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

            '�C�����
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
            '2010/10/07 �d���f�[�^�́A�d������FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
            .AppendLine("		AND  ")
            .AppendLine("			ISNULL(ts.siire_keijyou_flg,'0') = '1'  ")
            '2010/10/07 �d���f�[�^�́A�d������FLG=1�̂ݑΏۂƂ��� �n���R�ǉ� ��
            .AppendLine("		GROUP BY ")
            .AppendLine("		mts.skk_jigyousyo_cd + RIGHT (mts.skk_shri_saki_cd,4) ")
            .AppendLine(") t ")

            '�ݕ����z����0
            .AppendLine("WHERE ")
            .AppendLine("   kasi_kingaku <> 0 ")

            .AppendLine("ORDER BY ")
            .AppendLine("	t.kasi_aitesaki ")
            .AppendLine("	,t.kari_zei ")
        End With

        paramList.Add(MakeParam("@fromDate", SqlDbType.VarChar, 10, strDateFrom))
        paramList.Add(MakeParam("@toDate", SqlDbType.VarChar, 10, strDateTo))
        paramList.Add(MakeParam("@tekiyou", SqlDbType.VarChar, 255, "�d���h�e" & SetZenkakuSuuji(Right(strDateFrom, 5)) & "�`" & SetZenkakuSuuji(Right(strDateTo, 5))))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.ExcelSiwakeSiireDataTable.TableName, paramList.ToArray)

        '�߂�
        Return dsDataSet.ExcelSiwakeSiireDataTable
    End Function

    ''' <summary>
    ''' EXCEL�d������f�[�^�o��
    ''' </summary>
    ''' <param name="strDateFrom">���o����FROM</param>
    ''' <param name="strDateTo">���o����TO</param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetExcelSiwakeNyuukin(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.ExcelSiwakeNyuukinDataTableDataTable
        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New ExcelSiwakeDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
            '.AppendLine("			) tn    --������ʖ��ɉȖځA�זڂ�ݒ肵����Ԃ�SELECT���A����(UNION ALL) ")
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

            ''�ݕ����z�����O
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
            .AppendLine("            , '���|��' kasi_kamoku_mei	 ")
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
            .AppendLine("                    , '����' kari_kamoku_mei	 ")
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
            .AppendLine("                    , '���؎�' kari_kamoku_mei	 ")
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
            .AppendLine("                    , '���ʗa��' kari_kamoku_mei	 ")
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
            .AppendLine("                    , '����`�i���|���j' kari_kamoku_mei	 ")
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
            .AppendLine("                    , '�g�|�G��' kari_kamoku_mei	 ")
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
            .AppendLine("                    , '���������' kari_kamoku_mei	 ")
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
            .AppendLine("                    , '�g�|�ʐM��' kari_kamoku_mei	 ")
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
            .AppendLine("                    , '���������' kari_kamoku_mei	 ")
            .AppendLine("                    , '9999999999' kari_aitesaki	 ")
            .AppendLine("                    , round(furikomi_tesuuryou-furikomi_tesuuryou/1.05,0,1) kingaku	 ")
            .AppendLine("                    , '03919' kamoku	 ")
            .AppendLine("                    , '777' saimoku 	 ")
            .AppendLine("                FROM	 ")
            .AppendLine("                    " & connDBUser & "t_nyuukin_data	 ")
            .AppendLine("            ) tn    --������ʖ��ɉȖځA�זڂ�ݒ肵����Ԃ�SELECT���A����(UNION ALL)	 ")
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
            .AppendLine("            , '���|��' kasi_kamoku_mei	 ")
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
            .AppendLine("                    , '����' kari_kamoku_mei	 ")
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
            .AppendLine("                    , '���؎�' kari_kamoku_mei	 ")
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
            .AppendLine("                    , '���ʗa��' kari_kamoku_mei	 ")
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
            .AppendLine("                    , '����`�i���|���j' kari_kamoku_mei	 ")
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
            .AppendLine("                    , '�g�|�G��' kari_kamoku_mei	 ")
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
            .AppendLine("                    , '���������' kari_kamoku_mei	 ")
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
            .AppendLine("                    , '�g�|�ʐM��' kari_kamoku_mei	 ")
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
            .AppendLine("                    , '���������' kari_kamoku_mei	 ")
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
        paramList.Add(MakeParam("@tekiyou", SqlDbType.VarChar, 255, "�����h�e" & SetZenkakuSuuji(Right(strDateFrom, 5)) & "�`" & SetZenkakuSuuji(Right(strDateTo, 5))))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.ExcelSiwakeNyuukinDataTable.TableName, paramList.ToArray)

        '�߂�
        Return dsDataSet.ExcelSiwakeNyuukinDataTable
    End Function

    ''' <summary>
    ''' ���|���c���\
    ''' </summary>
    ''' <param name="strDateFrom">���o����FROM</param>
    ''' <param name="strDateTo">���o����TO</param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetUrikakekinZandakaHyou(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.UrikakekinZandakaHyouDataTable
        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New ExcelSiwakeDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	TBL3.datakbn, ")
            .AppendLine("	TBL3.tokuisaki_cd, ")
            .AppendLine("	TBL3.tokuisaki_mei1, ")
            .AppendLine("	TBL3.tokuisaki_mei2, ")
            .AppendLine("	--�J�z�c�� ")
            .AppendLine("	TBL3.kurikosi_zanndaka, ")
            .AppendLine("	--�����E�U�� ")
            .AppendLine("	TBL3.genkin_furikomi, ")
            .AppendLine("	--��` ")
            .AppendLine("	TBL3.tegata, ")
            .AppendLine("	--���E�� ")
            .AppendLine("	TBL3.sousaihoka, ")
            .AppendLine("	--�������v ")
            .AppendLine("	TBL3.nyuukin_goukei, ")
            .AppendLine("	--������c�� ")
            .AppendLine("	TBL3.mikaisyuu_zanndaka, ")
            .AppendLine("	--���㍂ ")
            .AppendLine("	TBL3.uriagedaka, ")
            .AppendLine("	--����œ� ")
            .AppendLine("	TBL3.syouhizeinado, ")
            .AppendLine("	--�����c�� ")
            .AppendLine("	TBL3.sasihiki_zanndaka, ")
            .AppendLine("	--��`�c�� ")
            .AppendLine("	TBL3.tegata_zanndaka, ")
            .AppendLine("	--����� ")
            .AppendLine("	TBL3.uriage_saiken ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			'' datakbn ")
            .AppendLine("			,(TBL2.seikyuu_saki_cd + '$$$' + TBL2.seikyuu_saki_brc + '$$$' + TBL2.seikyuu_saki_kbn) tokuisaki_cd --���Ӑ�R�[�h ")
            .AppendLine("			,VS.seikyuu_saki_mei tokuisaki_mei1 --���Ӑ於�P ")
            .AppendLine("			,VS.seikyuu_saki_mei2 tokuisaki_mei2 --���Ӑ於�Q ")
            .AppendLine("			--�J�z�c�� ")
            .AppendLine("			,ISNULL(TBL2.kurikosi_zan,0) kurikosi_zanndaka ")
            .AppendLine("			--�����E�U�� ")
            .AppendLine("			,ISNULL(TBL2.genkin_furikomi,0) genkin_furikomi ")
            .AppendLine("			--��` ")
            .AppendLine("			,ISNULL(TBL2.tegata,0) tegata ")
            .AppendLine("			--���E�� ")
            .AppendLine("			,ISNULL(TBL2.sousaihoka,0) sousaihoka ")
            .AppendLine("			--�������v(�����E�U���{��`�{���E��) ")
            .AppendLine("			,ISNULL(TBL2.genkin_furikomi,0) + ISNULL(TBL2.tegata,0) + ISNULL(TBL2.sousaihoka,0) nyuukin_goukei ")
            .AppendLine("			--������c��(�J�z�c���|�������v���J�z�c���|(�����E�U���{��`�{���E��)) ")
            .AppendLine("			,ISNULL(TBL2.kurikosi_zan,0) - (ISNULL(TBL2.genkin_furikomi,0) + ISNULL(TBL2.tegata,0) + ISNULL(TBL2.sousaihoka,0)) mikaisyuu_zanndaka ")
            .AppendLine("			--���㍂ ")
            .AppendLine("			,ISNULL(TBL2.uriagedaka,0) uriagedaka ")
            .AppendLine("			--����œ� ")
            .AppendLine("			,ISNULL(TBL2.syouhizeinado,0) syouhizeinado ")
            .AppendLine("			--�����c��(�J�z�c���|�������v�{���㍂�{����œ����J�z�c���|(�����E�U���{��`�{���E��)�{���㍂�{����œ�) ")
            .AppendLine("			,ISNULL(TBL2.kurikosi_zan,0) - (ISNULL(TBL2.genkin_furikomi,0) + ISNULL(TBL2.tegata,0) + ISNULL(TBL2.sousaihoka,0)) + (ISNULL(TBL2.uriagedaka,0) + ISNULL(TBL2.syouhizeinado,0)) sasihiki_zanndaka ")
            .AppendLine("			--��`�c�� ")
            .AppendLine("			,ISNULL(TBL2.tegata_zanndaka,0) tegata_zanndaka ")
            .AppendLine("			--�����(�����c���{��`�c����((�J�z�c���|(�����E�U���{��`�{���E��)�{���㍂�{����œ�)�{��`�c��) ")
            .AppendLine("			,ISNULL(TBL2.kurikosi_zan,0) - (ISNULL(TBL2.genkin_furikomi,0) + ISNULL(TBL2.tegata,0) + ISNULL(TBL2.sousaihoka,0)) + (ISNULL(TBL2.uriagedaka,0) + ISNULL(TBL2.syouhizeinado,0)) + ISNULL(TBL2.tegata_zanndaka,0) uriage_saiken ")
            .AppendLine("		FROM ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					 MSS.seikyuu_saki_cd ")
            .AppendLine("					,MSS.seikyuu_saki_brc ")
            .AppendLine("					,MSS.seikyuu_saki_kbn ")
            .AppendLine("					,TBL1.kurikosi_zan --�J�z�c�� ")
            .AppendLine("					,(tn.genkin + tn.kogitte + tn.furikomi + tn.kouza_furikae) AS genkin_furikomi --�����E�U��([����]�{[���؎�]�{[�U��]�{[�����U��]) ")
            .AppendLine("					,tn.tegata --��` ")
            .AppendLine("					,(tn.sousai + tn.nebiki + tn.sonota + tn.kyouryoku_kaihi + tn.furikomi_tesuuryou) AS sousaihoka --���E��([���E]�{[�l��]�{[���̑�]�{[���͉��]�{[�U���萔��]) ")
            .AppendLine("					,(tu1.uri_gaku) AS uriagedaka --���㍂(������z) ")
            .AppendLine("					,(tu1.sotozei_gaku) AS syouhizeinado --����œ�(�O�Ŋz) ")
            .AppendLine("					,(tn3.tegata) AS tegata_zanndaka --��`�c��(��`) ")
            .AppendLine("				FROM ")
            .AppendLine("					m_seikyuu_saki MSS ")
            .AppendLine("					LEFT JOIN ")
            .AppendLine("					/* ")
            .AppendLine("					�J�z�c��(TBL1) ")
            .AppendLine("					*/ ")
            .AppendLine("					( ")
            .AppendLine("						SELECT ")
            .AppendLine("							 sm.seikyuu_saki_cd ")
            .AppendLine("							,sm.seikyuu_saki_brc ")
            .AppendLine("							,sm.seikyuu_saki_kbn ")
            .AppendLine("							,ISNULL(ukt.tougetu_kurikosi_zan,0) + ISNULL(u2.uriage_goukei,0) - ISNULL(n2.nyuukin_goukei,0) AS kurikosi_zan --�J�z�c�� ")
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
            .AppendLine("							����f�[�^�e�[�u��.������z�{�O�Ŋz �̍��v ")
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
            .AppendLine("							�����f�[�^�e�[�u��. ")
            .AppendLine("							�����z [����]�{�����z [���؎�]�{�����z [�U��]�{�����z [�����U��]�{�����z [��`] ")
            .AppendLine("							�{�����z [���E]�{�����z [�l��]�{�����z [���̑�]�{�����z [���͉��]�{�����z [�U���萔��] ")
            .AppendLine("							�̍��v ")
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
            .AppendLine("					�����E�U�� ")
            .AppendLine("					��` ")
            .AppendLine("					���E�� ")
            .AppendLine("					*/ ")
            .AppendLine("					LEFT OUTER JOIN ")
            .AppendLine("					( ")
            .AppendLine("						SELECT ")
            .AppendLine("							seikyuu_saki_cd ")
            .AppendLine("							,seikyuu_saki_brc ")
            .AppendLine("							,seikyuu_saki_kbn ")
            .AppendLine("							,SUM(isnull(genkin,0)) AS genkin --���� ")
            .AppendLine("							,SUM(isnull(kogitte,0)) AS kogitte --���؎� ")
            .AppendLine("							,SUM(isnull(furikomi,0)) AS furikomi --�U�� ")
            .AppendLine("							,SUM(isnull(kouza_furikae,0)) AS kouza_furikae --�����U�� ")
            .AppendLine("							,SUM(isnull(tegata,0)) AS tegata --��` ")
            .AppendLine("							,SUM(isnull(sousai,0)) AS sousai --���E ")
            .AppendLine("							,SUM(isnull(nebiki,0)) AS nebiki --�l�� ")
            .AppendLine("							,SUM(isnull(sonota,0)) AS sonota --���̑� ")
            .AppendLine("							,SUM(isnull(kyouryoku_kaihi,0)) AS kyouryoku_kaihi --���͉�� ")
            .AppendLine("							,SUM(isnull(furikomi_tesuuryou,0)) AS furikomi_tesuuryou --�U���萔�� ")
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
            .AppendLine("					���㍂ ")
            .AppendLine("					����œ� ")
            .AppendLine("					*/ ")
            .AppendLine("					LEFT OUTER JOIN ")
            .AppendLine("					( ")
            .AppendLine("						SELECT ")
            .AppendLine("							seikyuu_saki_cd ")
            .AppendLine("							,seikyuu_saki_brc ")
            .AppendLine("							,seikyuu_saki_kbn ")
            .AppendLine("							,SUM(isnull(uri_gaku,0)) AS uri_gaku --���㍂ ")
            .AppendLine("							,SUM(isnull(sotozei_gaku,0)) AS sotozei_gaku --�O�Ŋz ")
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
            .AppendLine("					��`�c�� ")
            .AppendLine("					*/ ")
            .AppendLine("					LEFT OUTER JOIN ")
            .AppendLine("					( ")
            .AppendLine("						SELECT ")
            .AppendLine("							seikyuu_saki_cd ")
            .AppendLine("							,seikyuu_saki_brc ")
            .AppendLine("							,seikyuu_saki_kbn ")
            .AppendLine("							,SUM(isnull(tegata,0)) AS tegata --��` ")
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

        ' �������s'
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

        '�߂�
        Return dsDataSet.UrikakekinZandakaHyou
    End Function

    ''' <summary>
    ''' ���|���c���\csv�o�͂̃f�[�^���擾
    ''' </summary>
    ''' <param name="strDateFrom">���o����FROM</param>
    ''' <param name="strDateTo">���o����TO</param>
    ''' <history>2010/07/20 �Ⓦ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKaikakekinZandakaHyou(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.KaikakekinZandakaHyouDataTable
        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New ExcelSiwakeDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
            .AppendLine("			'' AS datakbn --�f�[�^�敪 ")
            .AppendLine("			,A.CODE AS tokuisaki_cd --�x����R�[�h ")
            .AppendLine("			,A.MEI1 AS tokuisaki_mei1 --�x���於1 ")
            .AppendLine("			,'' AS tokuisaki_mei2 --�x���於�Q ")
            .AppendLine("			,ISNULL(A.ZENDAKA,0) AS kurikosi_zanndaka --�J�z�c�� ")
            .AppendLine("			,ISNULL(A.furikomi,0) AS furikomi --�U�� ")
            .AppendLine("			,ISNULL(A.sousai,0) AS sousai --���E ")
            .AppendLine("			,ISNULL(A.furikomi,0)+ISNULL(A.sousai,0) AS goukei --�x�����v ")
            .AppendLine("			,ISNULL(A.ZENDAKA,0)-(ISNULL(A.furikomi,0)+ISNULL(A.sousai,0)) AS gou_zandaka --�����c�� ")
            .AppendLine("			,ISNULL(A.siire_gaku,0) AS siire_gaku --�d���� ")
            .AppendLine("			,ISNULL(A.sotozei_gaku,0) AS sotozei_gaku --����œ� ")
            .AppendLine("			,ISNULL(A.ZENDAKA,0)-(ISNULL(A.furikomi,0)+ISNULL(A.sousai,0))+ISNULL(A.siire_gaku,0)+ISNULL(A.sotozei_gaku,0) AS sai_zandaka --�����c�� ")
            .AppendLine("		FROM ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MT.tys_kaisya_cd --������ЃR�[�h ")
            .AppendLine("					,MT.jigyousyo_cd --���Ə��R�[�h ")
            .AppendLine("					,MT.tys_kaisya_cd + MT.jigyousyo_cd AS CODE --�x����R�[�h ")
            .AppendLine("					,CASE ")
            .AppendLine("						WHEN ISNULL(MT.seikyuu_saki_shri_saki_mei,'') = '' THEN ")
            .AppendLine("							MT.tys_kaisya_mei ")
            .AppendLine("						ELSE ")
            .AppendLine("							MT.seikyuu_saki_shri_saki_mei ")
            .AppendLine("						END AS MEI1 --�x���於1 ")
            .AppendLine("					,ZENDAKA.zendaka --�J�z�c�� ")
            .AppendLine("					,TSD1.furikomi --�U�� ")
            .AppendLine("					,TSD1.sousai --���E ")
            .AppendLine("					,s2.siire_gaku --�d���� ")
            .AppendLine("					,s2.sotozei_gaku --����œ� ")
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
            .AppendLine("							jigyousyo_cd = shri_jigyousyo_cd --���Ə��R�[�h���x���W�v�掖�Ə��R�[�h ")
            .AppendLine("					) AS MT ")
            .AppendLine("					/* ")
            .AppendLine("					�J�z�c��(ZENDAKA) ")
            .AppendLine("					*/ ")
            .AppendLine("					LEFT JOIN ")
            .AppendLine("					( ")
            .AppendLine("						SELECT ")
            .AppendLine("							sm.tys_kaisya_cd ")
            .AppendLine("							,sm.shri_jigyousyo_cd ")
            .AppendLine("							,ISNULL(ukt.tougetu_kurikosi_zan,0) + ISNULL(s2.siire_goukei,0) - ISNULL(h2.siharai_goukei,0) AS zendaka --�J�z�c�� ")
            .AppendLine("						FROM ")
            .AppendLine("							( ")
            .AppendLine("								SELECT ")
            .AppendLine("									tys_kaisya_cd ")
            .AppendLine("									,shri_jigyousyo_cd ")
            .AppendLine("								FROM ")
            .AppendLine("									m_tyousakaisya ")
            .AppendLine("								WHERE ")
            .AppendLine("									jigyousyo_cd = shri_jigyousyo_cd --���Ə��R�[�h���x���W�v�掖�Ə��R�[�h ")
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
            .AppendLine("							--�d���f�[�^�e�[�u��.�d�����z�{�O�Ŋz �̍��v ")
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
            .AppendLine("											jigyousyo_cd = shri_jigyousyo_cd --���Ə��R�[�h���x���W�v�掖�Ə��R�[�h ")
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
            .AppendLine("							--�x���f�[�^�e�[�u��.�x���z [�U��]�{�x���z [���E] ")
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
            .AppendLine("											jigyousyo_cd = shri_jigyousyo_cd --���Ə��R�[�h���x���W�v�掖�Ə��R�[�h ")
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
            .AppendLine("					) AS ZENDAKA --�J�z�c�� ")
            .AppendLine("					ON ")
            .AppendLine("						ZENDAKA.tys_kaisya_cd = MT.tys_kaisya_cd ")
            .AppendLine("						AND ")
            .AppendLine("						ZENDAKA.shri_jigyousyo_cd = MT.shri_jigyousyo_cd ")
            .AppendLine("					/* ")
            .AppendLine("					�U��/���E(TSD1) ")
            .AppendLine("					*/ ")
            .AppendLine("					LEFT JOIN ")
            .AppendLine("					( ")
            .AppendLine("						SELECT ")
            .AppendLine("							skk_jigyou_cd ")
            .AppendLine("							,skk_shri_saki_cd ")
            .AppendLine("							,SUM(ISNULL(furikomi,0)) furikomi --�U�� ")
            .AppendLine("							,SUM(ISNULL(sousai,0)) sousai --���E ")
            .AppendLine("						FROM ")
            .AppendLine("							t_siharai_data ")
            .AppendLine("						WHERE ")
            .AppendLine("							CONVERT(CHAR(10),siharai_date,111) BETWEEN CONVERT(CHAR(10),@fromDate,111) AND CONVERT(CHAR(10),@toDate,111) ")
            .AppendLine("						GROUP BY ")
            .AppendLine("							skk_jigyou_cd ")
            .AppendLine("							,skk_shri_saki_cd ")
            .AppendLine("					) AS TSD1 --�U��/���E ")
            .AppendLine("					ON ")
            .AppendLine("						TSD1.skk_jigyou_cd = MT.skk_jigyousyo_cd ")
            .AppendLine("						AND ")
            .AppendLine("						TSD1.skk_shri_saki_cd = MT.skk_shri_saki_cd ")
            .AppendLine("					/* ")
            .AppendLine("					�d����/����œ�(s2) ")
            .AppendLine("					*/ ")
            .AppendLine("					LEFT JOIN ")
            .AppendLine("					( ")
            .AppendLine("						SELECT ")
            .AppendLine("							tth3.tys_kaisya_cd ")
            .AppendLine("							,tth3.shri_jigyousyo_cd		 ")
            .AppendLine("							,SUM(isnull(s2.siire_gaku,0)) AS siire_gaku --�d���� ")
            .AppendLine("							,SUM(isnull(s2.sotozei_gaku,0)) AS sotozei_gaku --����œ� ")
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
            .AppendLine("					) AS s2 --�d����/����œ� ")
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

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.KaikakekinZandakaHyou.TableName, paramList.ToArray)

        '�߂�
        Return dsDataSet.KaikakekinZandakaHyou
    End Function

    ''' <summary>
    ''' �S�p����
    ''' </summary>
    ''' <param name="value">�l</param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Function SetZenkakuSuuji(ByVal value As String) As String
        Dim str1 As String = value
        str1 = str1.Replace("0", "�O")
        str1 = str1.Replace("1", "�P")
        str1 = str1.Replace("2", "�Q")
        str1 = str1.Replace("3", "�R")
        str1 = str1.Replace("4", "�S")
        str1 = str1.Replace("5", "�T")
        str1 = str1.Replace("6", "�U")
        str1 = str1.Replace("7", "�V")
        str1 = str1.Replace("8", "�W")
        str1 = str1.Replace("9", "�X")
        str1 = str1.Replace("/", "�^")

        Return str1
    End Function

    ''' <summary>
    ''' ������}�X�^��CSV���擾
    ''' </summary>
    ''' <returns>������}�X�^CSV�e�[�u��</returns>
    ''' <remarks>������}�X�^��CSV���̃f�[�^</remarks>
    ''' <history>2009/07/15�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function Selm_seikyuu_sakiCSV() As KakusyuDataSyuturyokuMenuDataSet.m_seikyuu_sakiCSVTableDataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsKakusyuDataSyuturyokuMenuDataSet As New KakusyuDataSyuturyokuMenuDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	(MSS.seikyuu_saki_cd+'$$$'+MSS.seikyuu_saki_brc+'$$$'+MSS.seikyuu_saki_kbn) AS tokuisaki_cd ") '--���Ӑ�R�[�h 
            .AppendLine("	,VSS.seikyuu_saki_mei ") '--���Ӑ於�P 
            .AppendLine("	,VSS.seikyuu_saki_mei2 ") '--���Ӑ於2 
            .AppendLine("	,'' AS senpou_tantou_mei ") '--����S���Җ� 
            .AppendLine("	,MSS.seikyuusyo_hittyk_date ") '--���[���A�h���X 
            .AppendLine("	,'0' AS master_kbn ") '--�}�X�^�[�敪 
            .AppendLine("	,(MSS.seikyuu_saki_cd+'$$$'+MSS.seikyuu_saki_brc+'$$$'+MSS.seikyuu_saki_kbn) AS seikyuu_saki_cd ") '--������R�[�h 
            .AppendLine("	,'0' AS jiltuseki_kanri ") '--���ъǗ� 
            .AppendLine("	,VSS.skysy_soufu_jyuusyo1 ") '--�Z���P 
            .AppendLine("	,VSS.skysy_soufu_jyuusyo2 ") '--�Z���Q 
            .AppendLine("	,VSS.skysy_soufu_yuubin_no ") '--�X�֔ԍ� 
            .AppendLine("	,VSS.skysy_soufu_tel_no ") '--�d�b�ԍ� 
            .AppendLine("	,MSS.nyuukin_kouza_no ") '--���������ԍ� 
            .AppendLine("	,'0' AS tokuisaki_kbn1 ") '--���Ӑ�敪1 
            .AppendLine("	,'0' AS tokuisaki_kbn2 ") '--���Ӑ�敪2 
            .AppendLine("	,'0' AS tokuisaki_kbn3 ") '--���Ӑ�敪3 
            .AppendLine("	,'0' AS baika_no ") '--�K�p����[����No] 
            .AppendLine("	,'100.0' AS kakeritu ") '--�K�p����[�|��] 
            .AppendLine("	,'0' AS zeikanzan ") '--�K�p����[�Ŋ��Z] 
            .AppendLine("	,'0' AS syutantou_cd ") '--��S���҃R�[�h 
            .AppendLine("	,MSS.seikyuu_sime_date  ") '--�������� 
            .AppendLine("	,'0' AS syouhizei_hasuu ") '--����Œ[�� 
            .AppendLine("	,'0' AS syouhizei_tuuti ") '--����Œʒm 
            .AppendLine("	,MSS.kaisyuu1_syubetu1 ") '--������1 
            .AppendLine("	,MSS.kaisyuu_kyoukaigaku ") '--�����ʋ��E�z 
            .AppendLine("	,MSS.kaisyuu2_syubetu1 ") '--������2
            .AppendLine("	,MSS.kaisyuu_yotei_gessuu ") '--����\�茎��
            .AppendLine("	,MSS.kaisyuu_yotei_date ") '--����\��� 
            .AppendLine("	,'0' AS kaisyuuhouhou ") '--������@ 
            .AppendLine("	,'0' AS yusin_gendogaku ") '--�^�M���x�z 
            .AppendLine("	,'' AS kurikosi_zandaka ") '--�J�z�c�� 
            .AppendLine("	,'1' AS nohinsyo_yousi ") '--�[�i���p�� 
            .AppendLine("	,'1' AS nohinsyu_syamei ") '--�[�i���Ж� 
            .AppendLine("	,MSS.kaisyuu1_seikyuusyo_yousi ") '--�������p�� 
            .AppendLine("	,'0' AS seikyuusyo_syamei ") '--�������Ж� 
            .AppendLine("	,'0' AS kankoutyou_kbn ") '--�������敪 
            .AppendLine("	,'0' AS keisyou ") '--�h�� 
            .AppendLine("	,'' AS syaten_cd ") '--�ГX�R�[�h 
            .AppendLine("	,MSS.kyuu_seikyuu_saki_cd AS torihikisaki_cd ") '--�����R�[�h 
            .AppendLine("FROM ")
            .AppendLine("	m_seikyuu_saki AS MSS WITH(READCOMMITTED) ") '--������}�X�^ 
            .AppendLine("	INNER JOIN ")
            .AppendLine("	v_seikyuu_saki_info	AS VSS WITH(READCOMMITTED) ") '--��������擾�r���[ 
            .AppendLine("	ON ")
            .AppendLine("		MSS.seikyuu_saki_cd = VSS.seikyuu_saki_cd ") '--������R�[�h 
            .AppendLine("		AND MSS.seikyuu_saki_brc = VSS.seikyuu_saki_brc ") '--������}�� 
            .AppendLine("		AND MSS.seikyuu_saki_kbn = VSS.seikyuu_saki_kbn ") '--������敪 
            .AppendLine("ORDER BY ") '--�o�͏� 
            .AppendLine("	tokuisaki_cd ASC ") '--���Ӑ�R�[�h 
        End With

        '�������s()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKakusyuDataSyuturyokuMenuDataSet, _
                    dsKakusyuDataSyuturyokuMenuDataSet.m_seikyuu_sakiCSVTable.TableName)

        Return dsKakusyuDataSyuturyokuMenuDataSet.m_seikyuu_sakiCSVTable
    End Function

    ''' <summary>
    ''' ������Ѓ}�X�^��CSV���擾
    ''' </summary>
    ''' <returns>������Ѓ}�X�^CSV�e�[�u��</returns>
    ''' <remarks>������Ѓ}�X�^��CSV���̃f�[�^</remarks>
    ''' <history>2010/07/13 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function Selm_tyousakaisyaCSV() As KakusyuDataSyuturyokuMenuDataSet.m_tyousakaisyaCSVTableDataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsKakusyuDataSyuturyokuMenuDataSet As New KakusyuDataSyuturyokuMenuDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	tys_kaisya_cd ") '--������ЃR�[�h 
            .AppendLine("	,jigyousyo_cd ") '--���Ə��R�[�h 
            .AppendLine("	,torikesi ") '--��� 
            .AppendLine("	,tys_kaisya_mei ") '--������Ж� 
            .AppendLine("	,tys_kaisya_mei_kana ") '--������Ж��J�i 
            .AppendLine("	,seikyuu_saki_shri_saki_mei ") '--������x���於 
            .AppendLine("	,seikyuu_saki_shri_saki_kana ") '--������x���於�J�i 
            .AppendLine("	,jyuusyo1 ") '--�Z��1 
            .AppendLine("	,jyuusyo2 ") '--�Z��2 
            .AppendLine("	,yuubin_no ") '--�X�֔ԍ� 
            .AppendLine("	,tel_no ") '--�d�b�ԍ� 
            .AppendLine("	,fax_no ") '--FAX�ԍ� 
            .AppendLine("	,pca_siiresaki_cd ") '--PCA�p�d����R�[�h 
            .AppendLine("	,pca_seikyuu_cd ") '--PCA������R�[�h 
            .AppendLine("	,seikyuu_saki_cd ") '--������R�[�h 
            .AppendLine("	,seikyuu_saki_brc ") '--������}�� 
            .AppendLine("	,seikyuu_saki_kbn ") '--������敪 
            .AppendLine("	,seikyuu_sime_date ") '--�������ߓ� 
            .AppendLine("	,skysy_soufu_jyuusyo1 ") '--���������t��Z��1 
            .AppendLine("	,skysy_soufu_jyuusyo2 ") '--���������t��Z��2 
            .AppendLine("	,skysy_soufu_yuubin_no ") '--���������t��X�֔ԍ� 
            .AppendLine("	,skysy_soufu_tel_no ") '--���������t��d�b�ԍ� 
            .AppendLine("	,skk_shri_saki_cd ") '--�V��v�x����R�[�h 
            .AppendLine("	,skk_jigyousyo_cd ") '--�V��v���Ə��R�[�h
            .AppendLine("	,shri_meisai_jigyousyo_cd ") '--�x�����׏W�v�掖�Ə��R�[�h
            .AppendLine("	,shri_jigyousyo_cd ") '--�x���W�v�掖�Ə��R�[�h 
            .AppendLine("	,shri_sime_date ") '--�x�����ߓ� 
            .AppendLine("	,shri_yotei_gessuu ") '--�x���\�茎�� 
            .AppendLine("	,fctring_kaisi_nengetu ") '--�t�@�N�^�����O�J�n�N�� 
            .AppendLine("	,shri_you_fax_no ") '--�x���pFAX�ԍ� 
            .AppendLine("	,ss_kijyun_kkk ") '--SS����i 
            .AppendLine("	,fc_ten_cd ") '--FC�X�R�[�h 
            .AppendLine("	,kensa_center_cd ") '--�����Z���^�[�R�[�h
            .AppendLine("	,koj_hkks_tyokusou_flg ") '--�H���񍐏����� 
            .AppendLine("	,koj_hkks_tyokusou_upd_login_user_id ") '--�H���񍐏������ύX���O�C�����[�U�[ID
            .AppendLine("	,koj_hkks_tyokusou_upd_datetime ") '--�H���񍐏������ύX���� 
            .AppendLine("	,tys_kaisya_flg ") '--������Ѓt���O 
            .AppendLine("	,koj_kaisya_flg ") '--�H����Ѓt���O 
            .AppendLine("	,japan_kai_kbn ") '--JAPAN��敪
            .AppendLine("	,japan_kai_nyuukai_date ") '--JAPAN�����N�� 
            .AppendLine("	,japan_kai_taikai_date ") '--JAPAN��މ�N�� 
            .AppendLine("	,fc_ten_kbn ") '--FC�X�敪 
            .AppendLine("	,fc_nyuukai_date ") '--FC����N�� 
            .AppendLine("	,fc_taikai_date ") '--FC�މ�N�� 
            .AppendLine("	,torikesi_riyuu ") '--������R 
            .AppendLine("	,report_jhs_token_flg ") '--ReportJHS�g�[�N���L���t���O 
            .AppendLine("	,tkt_jbn_tys_syunin_skk_flg ") '--��n�n�Ւ�����C���i�L���t���O 
            .AppendLine("FROM  ")
            .AppendLine("	m_tyousakaisya WITH(READCOMMITTED) ") '--������Ѓ}�X�^ 
            .AppendLine("ORDER BY ") '--�o�͏� 
            .AppendLine("	tys_kaisya_cd ASC ") '--������ЃR�[�h 
            .AppendLine("	,jigyousyo_cd ASC ") '--���Ə��R�[�h
        End With

        '�������s()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKakusyuDataSyuturyokuMenuDataSet, _
                    dsKakusyuDataSyuturyokuMenuDataSet.m_tyousakaisyaCSVTable.TableName)

        Return dsKakusyuDataSyuturyokuMenuDataSet.m_tyousakaisyaCSVTable
    End Function

    ''' <summary>
    ''' ���i�}�X�^��CSV���擾
    ''' </summary>
    ''' <returns>���i�}�X�^CSV�e�[�u��</returns>
    ''' <remarks>���i�}�X�^��CSV���̃f�[�^</remarks>
    ''' <history>2010/07/13 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function Selm_syouhinCSV() As KakusyuDataSyuturyokuMenuDataSet.m_syouhinCSVTableDataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsKakusyuDataSyuturyokuMenuDataSet As New KakusyuDataSyuturyokuMenuDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	syouhin_cd ") '--���i�R�[�h
            .AppendLine("	,syouhin_mei ") '--���i��
            .AppendLine("	,'0' AS sisutemu_kbn ") '--�V�X�e���敪
            .AppendLine("	,'0' AS master_kbn ") '--�}�X�^�[�敪
            .AppendLine("	,'0' AS zaikokanri ") '--�݌ɊǗ�
            .AppendLine("	,'0' AS jiltusekikanri ") '--���ъǗ�
            .AppendLine("	,tani ") '--�P��
            .AppendLine("	,'0' AS ikazu ") '--����
            .AppendLine("	,shri_you_syouhin_mei ") '--�K�i�E�^��(�x���p���i��)
            .AppendLine("	,'' AS iru ") '--�F
            .AppendLine("	,'' AS saizu ") '--�T�C�Y
            .AppendLine("	,syouhin_kbn1 ") '--���i�敪1
            .AppendLine("	,syouhin_kbn2 ") '--���i�敪2
            .AppendLine("	,syouhin_kbn3 ") '--���i�敪3
            .AppendLine("	,zei_kbn ") '--�ŋ敪
            .AppendLine("	,zeikomi_kbn ") '--�ō��敪
            .AppendLine("	,'0' AS tanka ") '--�P��
            .AppendLine("	,'0' AS suuryou ") '--����
            .AppendLine("	,hyoujun_kkk ") '--�W�����i
            .AppendLine("	,'0' AS genka ") '--����
            .AppendLine("	,'0' AS haika1 ") '--����1
            .AppendLine("	,'0' AS haika2 ") '--����2
            .AppendLine("	,'0' AS haika3 ") '--����3
            .AppendLine("	,'0' AS haika4 ") '--����4
            .AppendLine("	,'0' AS haika5 ") '--����5
            .AppendLine("	,souko_cd ") '--�q�ɃR�[�h
            .AppendLine("	,'' AS syusiresaki_cd ") '--��d����R�[�h
            .AppendLine("	,'' AS zaikotanka ") '--�݌ɒP��
            .AppendLine("	,siire_kkk ") '--�d�����i
            .AppendLine("FROM ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ") '--���i�}�X�^
            .AppendLine("ORDER BY ") '--�o�͏�
            .AppendLine("	syouhin_cd ASC ") '--���i�R�[�h
        End With

        '�������s()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKakusyuDataSyuturyokuMenuDataSet, _
                    dsKakusyuDataSyuturyokuMenuDataSet.m_syouhinCSVTable.TableName)

        Return dsKakusyuDataSyuturyokuMenuDataSet.m_syouhinCSVTable
    End Function
    ''' <summary>
    ''' ��s�}�X�^��CSV���擾
    ''' </summary>
    ''' <returns>��s�}�X�^CSV�e�[�u��</returns>
    ''' <remarks>��s�}�X�^��CSV���̃f�[�^</remarks>
    ''' <history>2010/07/14 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function Selm_ginkouCSV() As KakusyuDataSyuturyokuMenuDataSet.m_ginkouCSVTableDataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsKakusyuDataSyuturyokuMenuDataSet As New KakusyuDataSyuturyokuMenuDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	ginkou_cd ") '--��s�R�[�h
            .AppendLine("	,ginkou_mei ") '--��s��
            .AppendLine("	,siten_cd ") '--�x�X�R�[�h
            .AppendLine("	,siten_mei ") '--�x�X��
            .AppendLine("	,saisin_flg ") '--�ŐV�t���O
            .AppendLine("FROM ")
            .AppendLine("	m_ginkou WITH(READCOMMITTED) ") '--��s�}�X�^
            .AppendLine("ORDER BY ") '--
            .AppendLine("	ginkou_cd ASC ") '--��s�R�[�h
            .AppendLine("	,siten_cd ASC ") '--�x�X�R�[�h
        End With

        '�������s()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKakusyuDataSyuturyokuMenuDataSet, _
                    dsKakusyuDataSyuturyokuMenuDataSet.m_ginkouCSVTable.TableName)

        Return dsKakusyuDataSyuturyokuMenuDataSet.m_ginkouCSVTable
    End Function

    ''' <summary>
    ''' ����f�[�^�o�͂�CSV���擾
    ''' </summary>
    ''' <returns>����f�[�^�o��CSV�e�[�u��</returns>
    ''' <remarks>����f�[�^�o�͂�CSV���̃f�[�^</remarks>
    ''' <history>
    ''' 2010/07/15 �ԗ�(��A���V�X�e����)�@�V�K�쐬
    ''' 2015/03/03 ���h�m(��A���V�X�e����)�@�C��
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

        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
        Dim hasCondition As Boolean = False
        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End

        ' DataSet�C���X�^���X�̐���
        Dim dsKakusyuDataSyuturyokuMenuDataSet As New KakusyuDataSyuturyokuMenuDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            '.AppendLine("SELECT")
            '.AppendLine("'0' AS denku ") '--�`��")
            '.AppendLine(",TU.denpyou_uri_date ") '--����N����")
            '.AppendLine(",ISNULL(TSMK.seikyuusyo_hak_date,TU.seikyuu_date) AS seikyuu_date ") '--�����N����")
            '.AppendLine(",TU.denpyou_no ") '--�`�[No.")
            '.AppendLine(",(TU.seikyuu_saki_cd+'$$$'+TU.seikyuu_saki_brc+'$$$'+TU.seikyuu_saki_kbn) AS tokuisaki_cd ") '--���Ӑ�R�[�h")
            '.AppendLine(",TU.seikyuu_saki_mei AS seikyuu_saki_mei ") '--���Ӑ於")
            '.AppendLine(",'' AS tyokusousaki_cd ") '--������R�[�h ")
            '.AppendLine(",'' AS senpou_tantou_mei ") '--����S���Җ�")
            '.AppendLine(",'0' AS bumon ") '--����R�[�h")
            '.AppendLine(",'0' AS tantou_cd ") '--�S���҃R�[�h")
            '.AppendLine(",'0' AS tekiyou ") '--�E�v�R�[�h")
            ''=======================2011/06/07 �ԗ� �C�� �J�n��=====================================
            ' ''2010/10/22 �ėp����e�[�u���ΏۂƂ��� �t���ǉ� ��
            ' ''.AppendLine(",TJ.sesyu_mei ") '--�E�v��(�{�喼)")
            ''.AppendLine(",CASE ")
            ''.AppendLine("	WHEN TU.himoduke_table_type =  '1' ") '--����f�[�^�e�[�u��.�R�t���e�[�u���^�C�v=1�̎�")
            ''.AppendLine("	THEN TJ.sesyu_mei ") '--�n�Ճe�[�u��.�{�喼")
            ''.AppendLine("	WHEN TU.himoduke_table_type =  '9' ") '--����f�[�^�e�[�u��.�R�t���e�[�u���^�C�v=9�̎�")
            ''.AppendLine("	THEN THU.sesyu_mei ") '--�ėp����e�[�u��.�{�喼")
            ''.AppendLine("	ELSE '' ")
            ''.AppendLine("	END AS sesyu_mei ") '--�E�v��(�{�喼)")
            ' ''2010/10/22 �ėp����e�[�u���ΏۂƂ��� �t���ǉ� ��
            '.AppendLine(",CASE ")
            '.AppendLine("	WHEN TJ.kbn IS NOT NULL ") '--�n�Ճe�[�u�������݂��鎞
            '.AppendLine("	THEN TJ.sesyu_mei ") '--�n�Ճe�[�u��.�{�喼")
            '.AppendLine("	WHEN (TJ.kbn IS NULL) AND (TU.himoduke_table_type =  '9') ") '--��L�ȊO ���� ����f�[�^�e�[�u��.�R�t���e�[�u���^�C�v=9�̎�")
            '.AppendLine("	THEN THU.sesyu_mei ") '--�ėp����e�[�u��.�{�喼")
            '.AppendLine("	ELSE '' ")
            '.AppendLine("	END AS sesyu_mei ") '--�E�v��(�{�喼)")
            ''=======================2011/06/07 �ԗ� �C�� �I����=====================================
            '.AppendLine(",'' AS bunrui_cd ") '--���ރR�[�h")
            '.AppendLine(",'' AS denpyou_kbn ") '--�`�[�敪")
            '.AppendLine(",TU.syouhin_cd ") '--���i�R�[�h")
            '.AppendLine(",'0' AS master_kbn ") '--�}�X�^�[�敪")
            '.AppendLine(",TU.hinmei ") '--�i��")
            '.AppendLine(",'0' AS ku ") '--��")
            '.AppendLine(",MS.souko_cd ") '--�q�ɃR�[�h")
            '.AppendLine(",'0' AS ikazu ") '--����")
            '.AppendLine(",CASE ")
            '.AppendLine("	WHEN TU.himoduke_table_type =  '1' ") '--����f�[�^�e�[�u��.�R�t�����e�[�u����ʁ�1:�@�ʐ����̏ꍇ�A")
            '.AppendLine("	THEN TU.himoduke_cd ") '--��ʕ\��NO�F����f�[�^�e�[�u��.�R�t���R�[�h.split('$$$')(4�Ԗ�)")
            '.AppendLine("	ELSE '0' ")
            '.AppendLine("	END AS hakokazu ") '--����")
            '.AppendLine(",TU.suu ") '--����")
            '.AppendLine(",TU.tani ") '--�P��")
            '.AppendLine(",TU.tanka ") '--�P��")
            '.AppendLine(",TU.uri_gaku ") '--������z")
            '.AppendLine(",'0' AS gentanka ") '--���P��")
            '.AppendLine(",'0' AS genkaga ") '--�����z")
            '.AppendLine(",'' AS ararieki ") '--�e���v")
            '.AppendLine(",TU.sotozei_gaku ") '--�O�Ŋz")
            '.AppendLine(",'0' AS utizei_gaku ") '--���Ŋz")
            '.AppendLine(",TU.zei_kbn ") '--�ŋ敪")
            '.AppendLine(",MS.zeikomi_kbn ") '--�ō��敪")
            ''=======================2011/06/07 �ԗ� �C�� �J�n��=====================================
            ' ''2010/10/22 �ėp����e�[�u���ΏۂƂ��� �t���ǉ� ��
            ' ''.AppendLine(",(ISNULL(TU.kbn,'') +ISNULL(TU.bangou,'')) AS bikou ") '--���l")
            ''.AppendLine(",CASE ")
            ''.AppendLine("	WHEN TU.himoduke_table_type =  '1' ") '--����f�[�^�e�[�u��.�R�t���e�[�u���^�C�v=1�̎�")
            ''.AppendLine("	THEN ISNULL(TU.kbn,'') +ISNULL(TU.bangou,'') ") '--����f�[�^�e�[�u��.�敪�{�ԍ�")
            ''.AppendLine("	WHEN TU.himoduke_table_type =  '9' ") '--����f�[�^�e�[�u��.�R�t���e�[�u���^�C�v=9�̎�")
            ''.AppendLine("	THEN ISNULL(THU.kbn,'') +ISNULL(THU.bangou,'') ") '--�ėp����e�[�u��.�敪�{�ԍ�")
            ''.AppendLine("	ELSE '' ")
            ''.AppendLine("	END AS bikou ") '--���l")
            ' ''2010/10/22 �ėp����e�[�u���ΏۂƂ��� �t���ǉ� ��
            '.AppendLine(",(ISNULL(TU.kbn,'') +ISNULL(TU.bangou,'')) AS bikou ") '--���l")
            ''=======================2011/06/07 �ԗ� �C�� �I����=====================================
            '.AppendLine(",MS.hyoujun_kkk ") '--�W�����i")
            '.AppendLine(",'0' AS douji_nyuuka_kbn ") '--�������׋敪")
            '.AppendLine(",'' AS baitanka ") '--���P��")
            '.AppendLine(",'' AS baika_kingaku ") '--�������z")
            ''=======================2011/06/07 �ԗ� �C�� �J�n��=====================================
            ''.AppendLine(",'' AS kikaku_kataban ")
            '.AppendLine(",ISNULL(MK.kameiten_mei1,'') AS kikaku_kataban ") '--�K�i�E�^��")
            ''.AppendLine(",'' AS iro ")
            '.AppendLine(",ISNULL(TU.kameiten_cd,'') AS iro ") '--�F")
            ''=======================2011/06/07 �ԗ� �C�� �I����=====================================
            '.AppendLine(",'' AS saizu ") '--�T�C�Y")
            '.AppendLine("FROM")
            '.AppendLine("	( ") '--���o���ԓ��̔���f�[�^���擾")
            '.AppendLine("	  ") '--�`�[��ʁuUN�v�ƁuUR�v�̃Z�b�g�ɂȂ��Ă��Ȃ��f�[�^�݂̂𒊏o����")
            '.AppendLine("		SELECT ")
            '.AppendLine("			u1.*")
            '.AppendLine("		FROM")
            '.AppendLine("			t_uriage_data u1 WITH(READCOMMITTED) ") '--����f�[�^�e�[�u��")
            '.AppendLine("		WHERE")
            '.AppendLine("			u1.denpyou_uri_date BETWEEN @fromDate AND @toDate ")
            '.AppendLine("			AND ")
            '.AppendLine("			( ")
            '.AppendLine("				( ") '--�}�C�i�X�`�[�̗L��v���X�`�[�����O")
            '.AppendLine("					u1.denpyou_syubetu = 'UN' ")
            '.AppendLine("					AND NOT EXISTS")
            '.AppendLine("					( ")
            '.AppendLine("						SELECT")
            '.AppendLine("							* ")
            '.AppendLine("						FROM")
            '.AppendLine("							t_uriage_data u2 WITH(READCOMMITTED) ") '--����f�[�^�e�[�u��")
            '.AppendLine("						WHERE")
            '.AppendLine("							u2.denpyou_uri_date BETWEEN @fromDate AND @toDate ")
            '.AppendLine("							AND	u2.denpyou_syubetu = 'UR' ")
            '.AppendLine("							AND u2.torikesi_moto_denpyou_unique_no = u1.denpyou_unique_no")
            '.AppendLine("					)") '--������`�[���j�[�NNO") '--�`�[���j�[�NNO")
            '.AppendLine("				) ")
            '.AppendLine("				OR ")
            '.AppendLine("				( ") '--�v���X�`�[�̗L��}�C�i�X�`�[�����O")
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
            '.AppendLine("	)TU ") '--����f�[�^�e�[�u��")
            '.AppendLine("	LEFT OUTER JOIN ")
            '.AppendLine("	t_jiban AS TJ WITH(READCOMMITTED) ") '--�n�Ճe�[�u��")
            '.AppendLine("	ON")
            '.AppendLine("		TU.kbn = TJ.kbn ") '--�敪")
            '.AppendLine("		AND TU.bangou = TJ.hosyousyo_no ") '--����f�[�^�e�[�u��.�ԍ� = �n�Ճe�[�u��.�ۏ؏�NO")
            ''2010/10/22 �ėp����e�[�u���ΏۂƂ��� �t���ǉ� ��
            '.AppendLine("	LEFT OUTER JOIN ")
            '.AppendLine("	t_hannyou_uriage AS THU WITH(READCOMMITTED) ") '--�ėp����e�[�u��")
            '.AppendLine("	ON")
            '.AppendLine("		TU.himoduke_cd = CONVERT(VARCHAR,THU.han_uri_unique_no) ") '--����f�[�^�e�[�u��.�R�t������ = �ėp����e�[�u��.�ėp���テ�j�[�NNO")
            ''2010/10/22 �ėp����e�[�u���ΏۂƂ��� �t���ǉ� ��
            '.AppendLine("	LEFT OUTER JOIN")
            '.AppendLine("		m_syouhin AS MS WITH(READCOMMITTED) ") '--���i�}�X�^")
            '.AppendLine("	ON")
            '.AppendLine("		TU.syouhin_cd = MS.syouhin_cd ") '--���i�R�[�h")
            '.AppendLine("	LEFT JOIN ")
            '.AppendLine("	( ")
            '.AppendLine("		SELECT ")
            '.AppendLine("			TSM.denpyou_unique_no ") '--�`�[���j�[�NNO
            '.AppendLine("			,TSK.seikyuusyo_hak_date ") '--���������s��
            '.AppendLine("		FROM ")
            '.AppendLine("			t_seikyuu_meisai AS TSM WITH(READCOMMITTED) ") '--�������׃e�[�u��
            '.AppendLine("			INNER JOIN ")
            '.AppendLine("			t_seikyuu_kagami AS TSK WITH(READCOMMITTED) ") '--�����Ӄe�[�u��
            '.AppendLine("			ON ")
            '.AppendLine("				TSM.seikyuusyo_no = TSK.seikyuusyo_no ") '--������NO
            '.AppendLine("				AND ")
            '.AppendLine("				TSK.torikesi = 0 ") '--���
            '.AppendLine("	) AS TSMK ")
            '.AppendLine("	ON ")
            '.AppendLine("		TU.denpyou_unique_no = TSMK.denpyou_unique_no ") '--�`�[���j�[�NNO
            ''=======================2011/06/07 �ԗ� �ǉ� �J�n��=====================================
            '.AppendLine("   LEFT JOIN ")
            '.AppendLine("   m_kameiten AS MK WITH(READCOMMITTED) ") '--�����X�}�X�^
            '.AppendLine("   ON ")
            '.AppendLine("       TU.kameiten_cd = MK.kameiten_cd ") '--�����X�R�[�h
            ''=======================2011/06/07 �ԗ� �ǉ� �I����=====================================

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

            '2015/03/02 ���h�m(��A���V�X�e����)�@�C��  Start
            'If seikyuu_saki_cd <> "" Then
            '    .AppendLine("WHERE")
            '    .AppendLine("	TU.seikyuu_saki_cd = @seikyuu_saki_cd ") '--������R�[�h")
            '    .AppendLine("	AND TU.seikyuu_saki_brc = @seikyuu_saki_brc ") '--������}��")
            '    .AppendLine("	AND TU.seikyuu_saki_kbn = @seikyuu_saki_kbn ") '--������敪")
            'End If
            For i As Integer = 0 To lstSeikyuuSakiCd.Count - 1
                If lstSeikyuuSakiCd(i) <> "" Then
                    If Not hasCondition Then
                        .AppendLine(" WHERE")
                        .AppendLine("	(TU.seikyuu_saki_cd = @seikyuu_saki_cd" & i & "") '--������R�[�h"
                        .AppendLine("	AND TU.seikyuu_saki_brc = @seikyuu_saki_brc" & i & "") '--������}��
                        .AppendLine("	AND TU.seikyuu_saki_kbn = @seikyuu_saki_kbn" & i & ")") '--������敪
                        hasCondition = True
                    Else
                        .AppendLine(" OR")
                        .AppendLine("	(TU.seikyuu_saki_cd = @seikyuu_saki_cd" & i & "") '--������R�[�h
                        .AppendLine("	AND TU.seikyuu_saki_brc = @seikyuu_saki_brc" & i & "") '--������}��
                        .AppendLine("	AND TU.seikyuu_saki_kbn = @seikyuu_saki_kbn" & i & ")") '--������敪
                    End If
                End If
            Next
            '2015/03/02 ���h�m(��A���V�X�e����)�@�C��  End
            .AppendLine("ORDER BY ") '--�o�͏�
            .AppendLine("   TU.denpyou_uri_date ASC ") '--����N����
            .AppendLine("   ,TU.denpyou_no ASC ") '--�`�[No.
        End With

        'SQL�����񒆂Ńp�����[�^
        With paramList
            .Add(MakeParam("@fromDate", SqlDbType.Char, 10, fromDate))
            .Add(MakeParam("@toDate", SqlDbType.Char, 10, toDate))
            '2015/03/02 ���h�m(��A���V�X�e����)�@�C��  Start
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
            '2015/03/02 ���h�m(��A���V�X�e����)�@�C��  End
        End With

        '�������s()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKakusyuDataSyuturyokuMenuDataSet, _
                    dsKakusyuDataSyuturyokuMenuDataSet.uriage_data_syuturyokuCSVTable.TableName, paramList.ToArray)

        Return dsKakusyuDataSyuturyokuMenuDataSet.uriage_data_syuturyokuCSVTable
    End Function

    ''' <summary>
    ''' �d���f�[�^�o�͂�CSV���擾
    ''' </summary>
    ''' <returns>�d���f�[�^�o��CSV�e�[�u��</returns>
    ''' <remarks>�d���f�[�^�o�͂�CSV���̃f�[�^</remarks>
    ''' <history>2010/07/16 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function Selsiire_data_syuturyokuCSV(ByVal fromDate As String, _
                                                ByVal toDate As String, _
                                                ByVal strSiireCd As String _
                                               ) As KakusyuDataSyuturyokuMenuDataSet.siire_data_syuturyokuCSVTableDataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsKakusyuDataSyuturyokuMenuDataSet As New KakusyuDataSyuturyokuMenuDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	'0' AS nyuuka_houhou ") '--���ו��@ ")
            .AppendLine("	,'0' AS kamoku_kbn ") '--�Ȗڋ敪 ")
            .AppendLine("	,'0' AS denku ") '--�`�� ")
            .AppendLine("	,TS.denpyou_siire_date ") '--���הN���� ")
            .AppendLine("	,'' AS seisan_date ") '--���Z�N���� ")
            .AppendLine("	,TS.denpyou_no ") '--�`�[No. ")
            .AppendLine("	,(TS.tys_kaisya_cd+'$$$'+TS.tys_kaisya_jigyousyo_cd) AS siiresaki_cd ") '--�d����R�[�h ")
            .AppendLine("	,TS.tys_kaisya_mei ") '--�d���於 ")
            .AppendLine("	,'' AS senpou_tantou_mei ") '--����S���Җ� ")
            .AppendLine("	,'0' AS bumon ") '--����R�[�h ")
            .AppendLine("	,'0' AS tantou_cd ") '--�S���҃R�[�h ")
            .AppendLine("	,'0' AS tekiyou ") '--�E�v�R�[�h ")
            '=======================2011/06/07 �ԗ� �C�� �J�n��=====================================
            '2010/10/22 �ėp�d���e�[�u���ΏۂƂ��� �t���ǉ� ��
            '.AppendLine("	,TJ.sesyu_mei ") '--�E�v��(�{�喼) ")
            '.AppendLine(",CASE ")
            '.AppendLine("	WHEN TS.himoduke_table_type =  '1' ") '--�d���f�[�^�e�[�u��.�R�t���e�[�u���^�C�v=1�̎�")
            '.AppendLine("	THEN TJ.sesyu_mei ") '--�n�Ճe�[�u��.�{�喼")
            '.AppendLine("	WHEN TS.himoduke_table_type =  '9' ") '--�d���f�[�^�e�[�u��.�R�t���e�[�u���^�C�v=9�̎�")
            '.AppendLine("	THEN THS.sesyu_mei ") '--�ėp�d���e�[�u��.�{�喼")
            '.AppendLine("	ELSE '' ")
            '.AppendLine("	END AS sesyu_mei ") '--�E�v��(�{�喼)")
            ''2010/10/22 �ėp�d���e�[�u���ΏۂƂ��� �t���ǉ� ��
            .AppendLine(",CASE ")
            .AppendLine("	WHEN TJ.kbn IS NOT NULL ") '--�n�Ճe�[�u�������݂��鎞
            .AppendLine("	THEN TJ.sesyu_mei ") '--�n�Ճe�[�u��.�{�喼")
            .AppendLine("	WHEN (TJ.kbn IS NULL) AND (TS.himoduke_table_type =  '9') ") '--��L�ȊO ���� �d���f�[�^�e�[�u��.�R�t���e�[�u���^�C�v=9�̎�
            .AppendLine("	THEN THS.sesyu_mei ") '--�ėp�d���e�[�u��.�{�喼")
            .AppendLine("	ELSE '' ")
            .AppendLine("	END AS sesyu_mei ") '--�E�v��(�{�喼)")
            '=======================2011/06/07 �ԗ� �C�� �I����=====================================
            .AppendLine("	,TS.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("	,'0' AS master_kbn ") '--�}�X�^�[�敪 ")
            .AppendLine("	,TS.hinmei ") '--�i�� ")
            .AppendLine("	,'' AS ku ") '--�� ")
            .AppendLine("	,'' AS souko_cd ") '--�q�ɃR�[�h ")
            .AppendLine("	,'0' AS ikazu ") '--���� ")
            .AppendLine("	,'0' AS hakokazu ") '--���� ")
            .AppendLine("	,TS.suu ") '--���� ")
            .AppendLine("	,TS.tani ") '--�P�� ")
            .AppendLine("	,TS.tanka ") '--�P�� ")
            .AppendLine("	,TS.siire_gaku ") '--���z ")
            .AppendLine("	,TS.sotozei_gaku ") '--�O�Ŋz ")
            .AppendLine("	,'0' AS utizei_gaku ") '--���Ŋz ")
            .AppendLine("	,TS.zei_kbn ") '--�ŋ敪 ")
            .AppendLine("	,'0' AS zeikomi_kbn ") '--�ō��敪 ")
            '=======================2011/06/07 �ԗ� �C�� �J�n��=====================================
            ''2010/10/22 �ėp�d���e�[�u���ΏۂƂ��� �t���ǉ� ��
            ''.AppendLine("	,(ISNULL(TS.kbn,'')+ISNULL(TS.bangou,'')) AS bikou ") '--���l ")
            '.AppendLine(",CASE ")
            '.AppendLine("	WHEN TS.himoduke_table_type =  '1' ") '--�d���f�[�^�e�[�u��.�R�t���e�[�u���^�C�v=1�̎�")
            '.AppendLine("	THEN ISNULL(TS.kbn,'') +ISNULL(TS.bangou,'') ") '--�d���f�[�^�e�[�u��.�敪�{�ԍ�")
            '.AppendLine("	WHEN TS.himoduke_table_type =  '9' ") '--�d���f�[�^�e�[�u��.�R�t���e�[�u���^�C�v=9�̎�")
            '.AppendLine("	THEN ISNULL(THS.kbn,'') +ISNULL(THS.bangou,'') ") '--�ėp�d���e�[�u��.�敪�{�ԍ�")
            '.AppendLine("	ELSE '' ")
            '.AppendLine("	END AS bikou ") '--���l")
            ''2010/10/22 �ėp�d���e�[�u���ΏۂƂ��� �t���ǉ� ��
            .AppendLine("	,(ISNULL(TS.kbn,'')+ISNULL(TS.bangou,'')) AS bikou ") '--���l(�d���f�[�^�e�[�u��.�敪�{�ԍ�) 
            '=======================2011/06/07 �ԗ� �C�� �I����=====================================
            .AppendLine("	,'' AS kikaku_kataban ") '--�K�i�E�^�� ")
            .AppendLine("	,'' AS iro ") '--�F ")
            .AppendLine("	,'' AS saizu ") '--�T�C�Y ")
            .AppendLine("FROM ")
            .AppendLine("	( ") '--���o���ԓ��̎d���f�[�^���擾 ")
            .AppendLine("	  ") '--�`�[��ʁuSN�v�ƁuSR�v�̃Z�b�g�ɂȂ��Ă��Ȃ��f�[�^�݂̂𒊏o���� ")
            .AppendLine("		SELECT  ")
            .AppendLine("			s1.* ")
            .AppendLine("		FROM ")
            .AppendLine("			t_siire_data s1 WITH(READCOMMITTED) ") '--�d���f�[�^�e�[�u�� ")
            .AppendLine("		WHERE ")
            .AppendLine("			s1.denpyou_siire_date BETWEEN @fromDate AND @toDate ")
            .AppendLine("			AND  ")
            .AppendLine("			(  ")
            .AppendLine("				( ") '--�}�C�i�X�`�[�̗L��v���X�`�[�����O ")
            .AppendLine("					s1.denpyou_syubetu = 'SN'  ")
            .AppendLine("					AND NOT EXISTS ")
            .AppendLine("					(  ")
            .AppendLine("						SELECT ")
            .AppendLine("							*  ")
            .AppendLine("						FROM ")
            .AppendLine("							t_siire_data s2 WITH(READCOMMITTED) ") '--�d���f�[�^�e�[�u�� ")
            .AppendLine("						WHERE ")
            .AppendLine("							s2.denpyou_siire_date BETWEEN @fromDate AND @toDate ")
            .AppendLine("							AND	s2.denpyou_syubetu = 'SR'  ")
            .AppendLine("							AND s2.torikesi_moto_denpyou_unique_no = s1.denpyou_unique_no ")
            .AppendLine("					)") '--������`�[���j�[�NNO") '--�`�[���j�[�NNO ")
            .AppendLine("				)  ")
            .AppendLine("				OR  ")
            .AppendLine("				( ") '--�v���X�`�[�̗L��}�C�i�X�`�[�����O ")
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
            .AppendLine("	)TS ") '--�d���f�[�^�e�[�u�� ")
            .AppendLine("	INNER JOIN m_tyousakaisya AS MT WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		 MT.tys_kaisya_cd = TS.tys_kaisya_cd ") '--������ЃR�[�h ")
            .AppendLine("		 AND MT.jigyousyo_cd = TS.tys_kaisya_jigyousyo_cd ") '--���Ə��R�[�h ")
            .AppendLine("	LEFT OUTER JOIN  ")
            .AppendLine("	t_jiban AS TJ WITH(READCOMMITTED) ") '--�n�Ճe�[�u�� ")
            .AppendLine("	ON ")
            .AppendLine("		TS.kbn = TJ.kbn ") '--�敪 ")
            .AppendLine("		AND TS.bangou = TJ.hosyousyo_no ") '--�d���f�[�^�e�[�u��.�ԍ� = �n�Ճe�[�u��.�ۏ؏�NO ")
            '2010/10/22 �ėp�d���e�[�u���ΏۂƂ��� �t���ǉ� ��
            .AppendLine("	LEFT OUTER JOIN ")
            .AppendLine("	t_hannyou_siire AS THS WITH(READCOMMITTED) ") '--�ėp�d���e�[�u��")
            .AppendLine("	ON")
            .AppendLine("		TS.himoduke_cd = CONVERT(VARCHAR,THS.han_siire_unique_no) ") '--�d���f�[�^�e�[�u��.�R�t������ = �ėp�d���e�[�u��.�ėp�d�����j�[�NNO")
            '2010/10/22 �ėp�d���e�[�u���ΏۂƂ��� �t���ǉ� ��
            If strSiireCd <> "" Then
                .AppendLine("WHERE ")
                .AppendLine("	MT.tys_kaisya_cd + MT.jigyousyo_cd = @strSiireCd ") '--") '--������ЃR�[�h + ���Ə��R�[�h ")
            End If
            .AppendLine("ORDER BY ") '--�o�͏� ")
            .AppendLine("	TS.denpyou_siire_date ASC ") '--���הN���� ")
            .AppendLine("	,TS.denpyou_no ASC ") '--�`�[No. ")
        End With

        'SQL�����񒆂Ńp�����[�^
        With paramList
            .Add(MakeParam("@fromDate", SqlDbType.Char, 10, fromDate))
            .Add(MakeParam("@toDate", SqlDbType.Char, 10, toDate))
            If strSiireCd <> "" Then
                .Add(MakeParam("@strSiireCd", SqlDbType.VarChar, 7, strSiireCd))
            End If
        End With

        '�������s()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKakusyuDataSyuturyokuMenuDataSet, _
                    dsKakusyuDataSyuturyokuMenuDataSet.siire_data_syuturyokuCSVTable.TableName, paramList.ToArray)

        Return dsKakusyuDataSyuturyokuMenuDataSet.siire_data_syuturyokuCSVTable
    End Function


    ''' <summary>
    ''' ��������擾
    ''' </summary>
    ''' <param name="seikyuuSakiCd">������R�[�h</param>
    ''' <param name="seikyuuSakiBrc">������}��</param>
    ''' <param name="seikyuuSakiKbn">������敪</param>
    ''' <param name="seikyuuSakiMei">�����於</param>
    ''' <param name="seikyuuSakiKana">������J�i</param>
    ''' <param name="torikesiFlg">����f�[�^�Ώۃt���O(true:�Ώۂɂ��Ȃ� / false:�Ώۂɂ���)</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function searchSeikyuuSakiInfo(ByVal seikyuuSakiCd As String, _
                                          ByVal seikyuuSakiBrc As String, _
                                          ByVal seikyuuSakiKbn As String, _
                                          ByVal seikyuuSakiMei As String, _
                                          ByVal seikyuuSakiKana As String, _
                                          Optional ByVal torikesiFlg As Boolean = False _
                                          ) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New Data.DataSet

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL���̐���
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

        'SQL�����񒆂Ńp�����[�^
        With paramList
            .Add(MakeParam("@seikyuuSakiCd", SqlDbType.VarChar, 7, seikyuuSakiCd & "%"))
            .Add(MakeParam("@seikyuuSakiBrc", SqlDbType.VarChar, 2, seikyuuSakiBrc))
            .Add(MakeParam("@seikyuuSakiKbn", SqlDbType.VarChar, 1, seikyuuSakiKbn))
            .Add(MakeParam("@seikyuuSakiMei", SqlDbType.VarChar, 100, seikyuuSakiMei & "%"))
            .Add(MakeParam("@seikyuuSakiKana", SqlDbType.VarChar, 100, seikyuuSakiKana & "%"))
        End With

        '�������s()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�f�[�^�擾���ԋp
        Return dsDataSet.Tables(0)

    End Function

    Enum EnumTyousakaisyaKensakuType
        ''' <summary>
        ''' �������
        ''' </summary>
        ''' <remarks></remarks>
        TYOUSAKAISYA = 0
        ''' <summary>
        ''' �d����
        ''' </summary>
        ''' <remarks></remarks>
        SIIRESAKI = 1
        ''' <summary>
        ''' �x����
        ''' </summary>
        ''' <remarks></remarks>
        SIHARAISAKI = 2
        ''' <summary>
        ''' �H�����
        ''' </summary>
        ''' <remarks></remarks>
        KOUJIKAISYA = 3
    End Enum

    ''' <summary>
    ''' ������Ѓ}�X�^�̌������s��
    ''' </summary>
    ''' <param name="strTysKaiCd">������к���</param>
    ''' <param name="strJigyousyoCd">���Ə�����</param>
    ''' <param name="strTysKaiNm">������Ж�</param>
    ''' <param name="strTysKaiKana">������Ж���</param>
    ''' <param name="blnDelete">����Ώۃt���O</param>
    ''' <param name="kameitenCd">�����X�R�[�h(�ۋ敪�`�F�b�N�p)</param>
    ''' <param name="kensakuType">�����^�C�v(EnumTyousakaisyakensakuType)</param>
    ''' <returns>TyousakaisyaSearchTableDataTable</returns>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetTyousakaisyaKensakuData(ByVal strTysKaiCd As String, _
                                      ByVal strJigyousyoCd As String, _
                                      ByVal strTysKaiNm As String, _
                                      ByVal strTysKaiKana As String, _
                                      ByVal blnDelete As Boolean, _
                                      ByVal kameitenCd As String, _
                                      ByVal kensakuType As EnumTyousakaisyaKensakuType _
                                      ) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New Data.DataSet

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �p�����[�^
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
            '�����^�C�v���H����Џꍇ�A��Ћ敪=2�A����ȊO�̏ꍇ�A1
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
        '������ЃR�[�h�́u������ЃR�[�h �{ ���Ə��R�[�h�v�Ō������s��
        If strTysKaiCd <> "" Then
            commandTextSb.Append(" AND t.tys_kaisya_cd + t.jigyousyo_cd Like " & strParamTysKaiCd)
        End If
        '��Ж���������
        If strTysKaiNm <> "" Then
            '�����^�C�v�ʂɁA������̃J������ς���
            If kensakuType = EnumTyousakaisyaKensakuType.SIHARAISAKI Then
                '�x���挟���̏ꍇ�A������x���於�Ō���
                commandTextSb.Append(" AND t.seikyuu_saki_shri_saki_mei Like " & strParamTysKaiNm)
            Else
                '��L�ȊO�̏ꍇ�A������Ж��J�i�Ō���
                commandTextSb.Append(" AND t.tys_kaisya_mei Like " & strParamTysKaiNm)
            End If
        End If
        '��Ж��J�i��������
        If strTysKaiKana <> "" Then
            '�����^�C�v�ʂɁA������̃J������ς���
            If kensakuType = EnumTyousakaisyaKensakuType.SIHARAISAKI Then
                '�x���挟���̏ꍇ�A������x���於�J�i�Ō���
                commandTextSb.Append(" AND t.seikyuu_saki_shri_saki_kana Like " & strParamTysKaiKana)
            Else
                '��L�ȊO�̏ꍇ�A������Ж��J�i�Ō���
                commandTextSb.Append(" AND t.tys_kaisya_mei_kana Like " & strParamTysKaiKana)
            End If
        End If
        '�����^�C�v���x����̏ꍇ�A���Ə��R�[�h���x���W�v�掖�Ə��R�[�h�̃��R�[�h�݂̂�ΏۂƂ���
        If kensakuType = EnumTyousakaisyaKensakuType.SIHARAISAKI Then
            commandTextSb.Append(" AND t.jigyousyo_cd = t.shri_jigyousyo_cd ")
        End If
        '�����X�R�[�h���w�肳��Ă���ꍇ�A���ёւ������𒲐�
        If kameitenCd = "" Then
            commandTextSb.Append(" ORDER BY t.tys_kaisya_mei_kana")
        Else
            commandTextSb.Append(" ORDER BY kahi_kbn, k.nyuuryoku_no, t.tys_kaisya_mei_kana")
        End If

        'SQL�����񒆂Ńp�����[�^
        With paramList
            .Add(MakeParam(strParamTysKaiCd, SqlDbType.VarChar, 7, strTysKaiCd & Chr(37)))
            .Add(MakeParam(strParamJigyousyoCd, SqlDbType.VarChar, 2, strJigyousyoCd))
            .Add(MakeParam(strParamTysKaiNm, SqlDbType.VarChar, 42, strTysKaiNm & Chr(37)))
            .Add(MakeParam(strParamTysKaiKana, SqlDbType.VarChar, 22, strTysKaiKana & Chr(37)))
            .Add(MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, kameitenCd))
        End With

        '�������s()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�f�[�^�擾���ԋp
        Return dsDataSet.Tables(0)

    End Function

End Class
