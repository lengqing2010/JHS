Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Public Class WaribikiMasterDataAccess
    ''' <summary>
    ''' 多棟割引データを取得
    ''' </summary>
    ''' <param name="strKameitenCdFrom">加盟店コード（FROM）</param>
    ''' <param name="strKameitenCdTo">加盟店コード（TO）</param>
    ''' <param name="strKameitenMei">加盟店名</param>
    ''' <param name="strKameitenKana">加盟店カナ</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strSyouhin">商品コード</param>
    ''' <param name="strSearchCount">検索件数</param>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <returns>多棟割引データ情報</returns>
    ''' <remarks></remarks>
    Public Function SelWaribiki(ByVal strKameitenCdFrom As String, _
                                ByVal strKameitenCdTo As String, _
                                ByVal strKameitenMei As String, _
                                ByVal strKameitenKana As String, _
                                ByVal strKeiretuCd As String, _
                                ByVal strSyouhin As String, _
                                ByVal strSearchCount As String) _
                                As DataAccess.WaribikiDataSet.WaribikiTableDataTable
        '---  接続文字列  ---

        Dim sbSql As New StringBuilder
        Dim lstParam As New List(Of SqlClient.SqlParameter)
        Dim strCon As String = System.Configuration.ConfigurationManager.ConnectionStrings("EarthConnectionString").ConnectionString

        '---  返却値格納用データセット  ---
        Dim dsWaribiki As New DataAccess.WaribikiDataSet

        With sbSql
            '---  Sql文の生成  ---
            .AppendLine("SELECT ")
            If strSearchCount = "100" Then
                .AppendLine("      TOP 100 ")
            End If
            .AppendLine("	ISNULL(TouKubun1.kbn,ISNULL(TouKubun2.kbn,TouKubun3.kbn)) AS kbn")
            .AppendLine(",	ISNULL(TouKubun1.kameiten_cd,ISNULL(TouKubun2.kameiten_cd,TouKubun3.kameiten_cd)) AS kameiten_cd")
            .AppendLine(",	ISNULL(TouKubun1.kameiten_mei, ISNULL(TouKubun2.kameiten_mei, TouKubun3.kameiten_mei)) AS kameiten_mei")
            .AppendLine(",	TouKubun1.syouhin_cd AS syouhin_cd1")
            .AppendLine(",	TouKubun1.syouhin_mei AS syouhin_mei1")
            .AppendLine(",	TouKubun2.syouhin_cd AS syouhin_cd2")
            .AppendLine(",	TouKubun2.syouhin_mei AS syouhin_mei2")
            .AppendLine(",	TouKubun3.syouhin_cd AS syouhin_cd3")
            .AppendLine(",	TouKubun3.syouhin_mei AS syouhin_mei3")
            .AppendLine("FROM")
            .AppendLine("(SELECT ")
            .AppendLine("	kmt.kbn")
            .AppendLine(",	tou.kameiten_cd")
            .AppendLine(",	kmt.kameiten_mei1 AS kameiten_mei")
            .AppendLine(",	kmt.kameiten_mei2")
            .AppendLine(",	kmt.tenmei_kana1")
            .AppendLine(",	kmt.tenmei_kana2")
            .AppendLine(",	kmt.keiretu_cd")
            .AppendLine(",	kmt.torikesi")
            .AppendLine(",	tou.syouhin_cd ")
            .AppendLine(",	sh.syouhin_mei ")
            .AppendLine("FROM ")
            .AppendLine("	(SELECT toukubun, ")
            .AppendLine("		kameiten_cd, ")
            .AppendLine("		syouhin_cd")
            .AppendLine("	FROM ")
            .AppendLine("		m_tatouwaribiki_settei WITH(READCOMMITTED)")
            .AppendLine("	WHERE ")
            .AppendLine("	    toukubun=@toukubun1 ")
            .AppendLine("	) AS tou ")
            .AppendLine("LEFT JOIN")
            .AppendLine("		(SELECT kbn , ")
            .AppendLine("			kameiten_cd,  ")
            .AppendLine("			torikesi,  ")
            .AppendLine("			kameiten_mei1,  ")
            .AppendLine("			kameiten_mei2,  ")
            .AppendLine("			tenmei_kana1,  ")
            .AppendLine("			tenmei_kana2,  ")
            .AppendLine("			keiretu_cd  ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten WITH(READCOMMITTED)")
            .AppendLine("		) AS kmt ")
            .AppendLine("	ON tou.kameiten_cd=kmt.kameiten_cd ")
            .AppendLine("LEFT JOIN")
            .AppendLine("	(SELECT syouhin.syouhin_cd , ")
            .AppendLine("		syouhin.syouhin_mei  ")
            .AppendLine("	FROM ")
            .AppendLine("		(SELECT syouhin_cd , ")
            .AppendLine("			syouhin_mei  ")
            .AppendLine("		FROM ")
            .AppendLine("			m_syouhin WITH(READCOMMITTED)")
            .AppendLine("		WHERE ")
            .AppendLine("			souko_cd=@souko_cd")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT DISTINCT '00000' AS syouhin_cd , ")
            .AppendLine("			'自動設定なし' AS syouhin_mei  ")
            .AppendLine("		FROM ")
            .AppendLine("		m_syouhin WITH(READCOMMITTED)) AS syouhin")
            .AppendLine("		) AS sh ")
            .AppendLine("	ON tou.syouhin_cd=sh.syouhin_cd  ")
            .AppendLine(")AS TouKubun1")
            .AppendLine("FULL OUTER JOIN")
            .AppendLine("(SELECT ")
            .AppendLine("	kmt.kbn")
            .AppendLine(",	tou.kameiten_cd")
            .AppendLine(",	kmt.kameiten_mei1 AS kameiten_mei")
            .AppendLine(",	kmt.kameiten_mei2")
            .AppendLine(",	kmt.tenmei_kana1")
            .AppendLine(",	kmt.tenmei_kana2")
            .AppendLine(",	kmt.keiretu_cd")
            .AppendLine(",	kmt.torikesi")
            .AppendLine(",	tou.syouhin_cd ")
            .AppendLine(",	sh.syouhin_mei ")
            .AppendLine("FROM ")
            .AppendLine("	(SELECT toukubun, ")
            .AppendLine("		kameiten_cd, ")
            .AppendLine("		syouhin_cd")
            .AppendLine("	FROM ")
            .AppendLine("		m_tatouwaribiki_settei WITH(READCOMMITTED)")
            .AppendLine("	WHERE ")
            .AppendLine("	    toukubun=@toukubun2 ")
            .AppendLine("	) AS tou ")
            .AppendLine("LEFT JOIN")
            .AppendLine("		(SELECT kbn , ")
            .AppendLine("			kameiten_cd,  ")
            .AppendLine("			torikesi,  ")
            .AppendLine("			kameiten_mei1,  ")
            .AppendLine("			kameiten_mei2,  ")
            .AppendLine("			tenmei_kana1,  ")
            .AppendLine("			tenmei_kana2,  ")
            .AppendLine("			keiretu_cd  ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten WITH(READCOMMITTED)")
            .AppendLine("		) AS kmt ")
            .AppendLine("	ON tou.kameiten_cd=kmt.kameiten_cd ")
            .AppendLine("LEFT JOIN")
            .AppendLine("	(SELECT syouhin.syouhin_cd , ")
            .AppendLine("		syouhin.syouhin_mei  ")
            .AppendLine("	FROM ")
            .AppendLine("		(SELECT syouhin_cd , ")
            .AppendLine("			syouhin_mei  ")
            .AppendLine("		FROM ")
            .AppendLine("			m_syouhin WITH(READCOMMITTED)")
            .AppendLine("		WHERE ")
            .AppendLine("			souko_cd=@souko_cd")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT DISTINCT '00000' AS syouhin_cd , ")
            .AppendLine("			'自動設定なし' AS syouhin_mei  ")
            .AppendLine("		FROM ")
            .AppendLine("		m_syouhin WITH(READCOMMITTED)) AS syouhin")
            .AppendLine("		) AS sh ")
            .AppendLine("	ON tou.syouhin_cd=sh.syouhin_cd  ")
            .AppendLine(")AS TouKubun2")
            .AppendLine("ON TouKubun1.kameiten_cd=TouKubun2.kameiten_cd")
            .AppendLine("FULL OUTER JOIN")
            .AppendLine("(SELECT ")
            .AppendLine("	kmt.kbn")
            .AppendLine(",	tou.kameiten_cd")
            .AppendLine(",	kmt.kameiten_mei1 AS kameiten_mei")
            .AppendLine(",	kmt.kameiten_mei2")
            .AppendLine(",	kmt.tenmei_kana1")
            .AppendLine(",	kmt.tenmei_kana2")
            .AppendLine(",	kmt.keiretu_cd")
            .AppendLine(",	kmt.torikesi")
            .AppendLine(",	tou.syouhin_cd ")
            .AppendLine(",	sh.syouhin_mei ")
            .AppendLine("FROM ")
            .AppendLine("	(SELECT toukubun, ")
            .AppendLine("		kameiten_cd, ")
            .AppendLine("		syouhin_cd")
            .AppendLine("	FROM ")
            .AppendLine("		m_tatouwaribiki_settei WITH(READCOMMITTED)")
            .AppendLine("	WHERE ")
            .AppendLine("	    toukubun=@toukubun3 ")
            .AppendLine("	) AS tou ")
            .AppendLine("LEFT JOIN")
            .AppendLine("		(SELECT kbn , ")
            .AppendLine("			kameiten_cd,  ")
            .AppendLine("			torikesi,  ")
            .AppendLine("			kameiten_mei1,  ")
            .AppendLine("			kameiten_mei2,  ")
            .AppendLine("			tenmei_kana1,  ")
            .AppendLine("			tenmei_kana2,  ")
            .AppendLine("			keiretu_cd  ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten WITH(READCOMMITTED)")
            .AppendLine("		) AS kmt ")
            .AppendLine("	ON tou.kameiten_cd=kmt.kameiten_cd ")
            .AppendLine("LEFT JOIN")
            .AppendLine("	(SELECT syouhin.syouhin_cd , ")
            .AppendLine("		syouhin.syouhin_mei  ")
            .AppendLine("	FROM ")
            .AppendLine("		(SELECT syouhin_cd , ")
            .AppendLine("			syouhin_mei  ")
            .AppendLine("		FROM ")
            .AppendLine("			m_syouhin WITH(READCOMMITTED)")
            .AppendLine("		WHERE ")
            .AppendLine("			souko_cd=@souko_cd")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT DISTINCT '00000' AS syouhin_cd , ")
            .AppendLine("			'自動設定なし' AS syouhin_mei  ")
            .AppendLine("		FROM ")
            .AppendLine("		m_syouhin WITH(READCOMMITTED)) AS syouhin")
            .AppendLine("		) AS sh ")
            .AppendLine("	ON tou.syouhin_cd=sh.syouhin_cd  ")
            .AppendLine(")AS TouKubun3")
            .AppendLine("ON TouKubun1.kameiten_cd=TouKubun3.kameiten_cd")
            .AppendLine("OR TouKubun2.kameiten_cd=TouKubun3.kameiten_cd")
            .AppendLine("WHERE cast((isnull(TouKubun1.torikesi,'0')+isnull(TouKubun2.torikesi,'0')+isnull(TouKubun3.torikesi,'0'))  AS INT ) = 0")
            If strKameitenCdFrom <> String.Empty Then
                If strKameitenCdTo = String.Empty Then
                    .AppendLine("AND ")
                    .AppendLine("	ISNULL(TouKubun1.kameiten_cd,ISNULL(TouKubun2.kameiten_cd,TouKubun3.kameiten_cd)) = @kameiten_cd_from ")
                Else
                    .AppendLine("AND ")
                    .AppendLine("	(ISNULL(TouKubun1.kameiten_cd,ISNULL(TouKubun2.kameiten_cd,TouKubun3.kameiten_cd)) >= @kameiten_cd_from ")
                    .AppendLine("AND ")
                    .AppendLine("	ISNULL(TouKubun1.kameiten_cd,ISNULL(TouKubun2.kameiten_cd,TouKubun3.kameiten_cd)) <= @kameiten_cd_to) ")
                End If
            End If
            If strKameitenMei <> String.Empty Then
                .AppendLine("AND (")
                .AppendLine("	ISNULL(ISNULL(TouKubun1.kameiten_mei,ISNULL(TouKubun2.kameiten_mei,TouKubun3.kameiten_mei)),'') LIKE @kameiten_mei1 ")
                .AppendLine("OR")
                .AppendLine("	ISNULL(ISNULL(TouKubun1.kameiten_mei2,ISNULL(TouKubun2.kameiten_mei2,TouKubun3.kameiten_mei2)),'') LIKE @kameiten_mei2) ")
            End If
            If strKameitenKana <> String.Empty Then
                .AppendLine("AND (")
                .AppendLine("	ISNULL(ISNULL(TouKubun1.tenmei_kana1,ISNULL(TouKubun2.tenmei_kana1,TouKubun3.tenmei_kana1)),'') LIKE @tenmei_kana1 ")
                .AppendLine("OR")
                .AppendLine("	ISNULL(ISNULL(TouKubun1.tenmei_kana2,ISNULL(TouKubun2.tenmei_kana2,TouKubun3.tenmei_kana2)),'') LIKE @tenmei_kana2) ")
            End If
            If strKeiretuCd <> String.Empty Then
                .AppendLine("AND ")
                .AppendLine("	ISNULL(ISNULL(TouKubun1.keiretu_cd,ISNULL(TouKubun2.keiretu_cd,TouKubun3.keiretu_cd)),'') = @keiretu_cd ")
            End If
            If strSyouhin <> String.Empty Then
                .AppendLine("AND ")
                .AppendLine("	ISNULL(ISNULL(TouKubun1.syouhin_cd,ISNULL(TouKubun2.syouhin_cd,TouKubun3.syouhin_cd)),'') = @syouhin_cd ")
            End If
            .AppendLine("ORDER BY ISNULL(TouKubun1.kameiten_cd,ISNULL(TouKubun2.kameiten_cd,TouKubun3.kameiten_cd))")

        End With

        'パラメータ         
        lstParam.Add(MakeParam("@souko_cd", SqlDbType.VarChar, 3, "115"))
        lstParam.Add(MakeParam("@toukubun1", SqlDbType.Int, 4, "1"))
        lstParam.Add(MakeParam("@toukubun2", SqlDbType.Int, 4, "2"))
        lstParam.Add(MakeParam("@toukubun3", SqlDbType.Int, 4, "3"))
        lstParam.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, strKameitenCdFrom))
        lstParam.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 5, strKameitenCdTo))
        lstParam.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 40, "%" & strKameitenMei & "%"))
        lstParam.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 40, "%" & strKameitenMei & "%"))
        lstParam.Add(MakeParam("@tenmei_kana1", SqlDbType.VarChar, 20, "%" & strKameitenKana & "%"))
        lstParam.Add(MakeParam("@tenmei_kana2", SqlDbType.VarChar, 20, "%" & strKameitenKana & "%"))
        lstParam.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, strKeiretuCd))
        lstParam.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhin))

        SQLHelper.FillDataset(strCon, CommandType.Text, _
        sbSql.ToString, _
        dsWaribiki, dsWaribiki.WaribikiTable.TableName, lstParam.ToArray)

        lstParam.Clear()

        Return dsWaribiki.WaribikiTable

    End Function
    ''' <summary>
    ''' 多棟割引データ件数を取得
    ''' </summary>
    ''' <param name="strKameitenCdFrom">加盟店コード（FROM）</param>
    ''' <param name="strKameitenCdTo">加盟店コード（TO）</param>
    ''' <param name="strKameitenMei">加盟店名</param>
    ''' <param name="strKameitenKana">加盟店カナ</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strSyouhin">商品コード</param>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <returns>多棟割引データ件数</returns>
    ''' <remarks></remarks>
    Public Function SelWaribikiCount(ByVal strKameitenCdFrom As String, _
                                     ByVal strKameitenCdTo As String, _
                                     ByVal strKameitenMei As String, _
                                     ByVal strKameitenKana As String, _
                                     ByVal strKeiretuCd As String, _
                                     ByVal strSyouhin As String) As Integer
        '---  接続文字列  ---

        Dim sbSql As New StringBuilder
        Dim lstParam As New List(Of SqlClient.SqlParameter)
        Dim strCon As String = System.Configuration.ConfigurationManager.ConnectionStrings("EarthConnectionString").ConnectionString

        '---  返却値格納用  ---
        Dim dsWaribikiCount As New DataAccess.WaribikiDataSet
        Dim intWaribiki As Integer
        intWaribiki = 0

        With sbSql
            '---  Sql文の生成  ---
            .AppendLine("SELECT COUNT(*) AS num")
            .AppendLine("FROM")
            .AppendLine("(SELECT ")
            .AppendLine("	kmt.kbn")
            .AppendLine(",	tou.kameiten_cd")
            .AppendLine(",	kmt.kameiten_mei1 AS kameiten_mei")
            .AppendLine(",	kmt.kameiten_mei2")
            .AppendLine(",	kmt.tenmei_kana1")
            .AppendLine(",	kmt.tenmei_kana2")
            .AppendLine(",	kmt.keiretu_cd")
            .AppendLine(",	kmt.torikesi")
            .AppendLine(",	tou.syouhin_cd ")
            .AppendLine(",	sh.syouhin_mei ")
            .AppendLine("FROM ")
            .AppendLine("	(SELECT toukubun, ")
            .AppendLine("		kameiten_cd, ")
            .AppendLine("		syouhin_cd")
            .AppendLine("	FROM ")
            .AppendLine("		m_tatouwaribiki_settei WITH(READCOMMITTED)")
            .AppendLine("	WHERE ")
            .AppendLine("	    toukubun=@toukubun1 ")
            .AppendLine("	) AS tou ")
            .AppendLine("LEFT JOIN")
            .AppendLine("		(SELECT kbn , ")
            .AppendLine("			kameiten_cd,  ")
            .AppendLine("			torikesi,  ")
            .AppendLine("			kameiten_mei1,  ")
            .AppendLine("			kameiten_mei2,  ")
            .AppendLine("			tenmei_kana1,  ")
            .AppendLine("			tenmei_kana2,  ")
            .AppendLine("			keiretu_cd  ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten WITH(READCOMMITTED)")
            .AppendLine("		) AS kmt ")
            .AppendLine("	ON tou.kameiten_cd=kmt.kameiten_cd ")
            .AppendLine("LEFT JOIN")
            .AppendLine("	(SELECT syouhin.syouhin_cd , ")
            .AppendLine("		syouhin.syouhin_mei  ")
            .AppendLine("	FROM ")
            .AppendLine("		(SELECT syouhin_cd , ")
            .AppendLine("			syouhin_mei  ")
            .AppendLine("		FROM ")
            .AppendLine("			m_syouhin WITH(READCOMMITTED)")
            .AppendLine("		WHERE ")
            .AppendLine("			souko_cd=@souko_cd")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT DISTINCT '00000' AS syouhin_cd , ")
            .AppendLine("			'自動設定なし' AS syouhin_mei  ")
            .AppendLine("		FROM ")
            .AppendLine("		m_syouhin WITH(READCOMMITTED)) AS syouhin")
            .AppendLine("		) AS sh ")
            .AppendLine("	ON tou.syouhin_cd=sh.syouhin_cd  ")
            .AppendLine(")AS TouKubun1")
            .AppendLine("FULL OUTER JOIN")
            .AppendLine("(SELECT ")
            .AppendLine("	kmt.kbn")
            .AppendLine(",	tou.kameiten_cd")
            .AppendLine(",	kmt.kameiten_mei1 AS kameiten_mei")
            .AppendLine(",	kmt.kameiten_mei2")
            .AppendLine(",	kmt.tenmei_kana1")
            .AppendLine(",	kmt.tenmei_kana2")
            .AppendLine(",	kmt.keiretu_cd")
            .AppendLine(",	kmt.torikesi")
            .AppendLine(",	tou.syouhin_cd ")
            .AppendLine(",	sh.syouhin_mei ")
            .AppendLine("FROM ")
            .AppendLine("	(SELECT toukubun, ")
            .AppendLine("		kameiten_cd, ")
            .AppendLine("		syouhin_cd")
            .AppendLine("	FROM ")
            .AppendLine("		m_tatouwaribiki_settei WITH(READCOMMITTED)")
            .AppendLine("	WHERE ")
            .AppendLine("	    toukubun=@toukubun2 ")
            .AppendLine("	) AS tou ")
            .AppendLine("LEFT JOIN")
            .AppendLine("		(SELECT kbn , ")
            .AppendLine("			kameiten_cd,  ")
            .AppendLine("			torikesi,  ")
            .AppendLine("			kameiten_mei1,  ")
            .AppendLine("			kameiten_mei2,  ")
            .AppendLine("			tenmei_kana1,  ")
            .AppendLine("			tenmei_kana2,  ")
            .AppendLine("			keiretu_cd  ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten WITH(READCOMMITTED)")
            .AppendLine("		) AS kmt ")
            .AppendLine("	ON tou.kameiten_cd=kmt.kameiten_cd ")
            .AppendLine("LEFT JOIN")
            .AppendLine("	(SELECT syouhin.syouhin_cd , ")
            .AppendLine("		syouhin.syouhin_mei  ")
            .AppendLine("	FROM ")
            .AppendLine("		(SELECT syouhin_cd , ")
            .AppendLine("			syouhin_mei  ")
            .AppendLine("		FROM ")
            .AppendLine("			m_syouhin WITH(READCOMMITTED)")
            .AppendLine("		WHERE ")
            .AppendLine("			souko_cd=@souko_cd")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT DISTINCT '00000' AS syouhin_cd , ")
            .AppendLine("			'自動設定なし' AS syouhin_mei  ")
            .AppendLine("		FROM ")
            .AppendLine("		m_syouhin WITH(READCOMMITTED)) AS syouhin")
            .AppendLine("		) AS sh ")
            .AppendLine("	ON tou.syouhin_cd=sh.syouhin_cd  ")
            .AppendLine(")AS TouKubun2")
            .AppendLine("ON TouKubun1.kameiten_cd=TouKubun2.kameiten_cd")
            .AppendLine("FULL OUTER JOIN")
            .AppendLine("(SELECT ")
            .AppendLine("	kmt.kbn")
            .AppendLine(",	tou.kameiten_cd")
            .AppendLine(",	kmt.kameiten_mei1 AS kameiten_mei")
            .AppendLine(",	kmt.kameiten_mei2")
            .AppendLine(",	kmt.tenmei_kana1")
            .AppendLine(",	kmt.tenmei_kana2")
            .AppendLine(",	kmt.keiretu_cd")
            .AppendLine(",	kmt.torikesi")
            .AppendLine(",	tou.syouhin_cd ")
            .AppendLine(",	sh.syouhin_mei ")
            .AppendLine("FROM ")
            .AppendLine("	(SELECT toukubun, ")
            .AppendLine("		kameiten_cd, ")
            .AppendLine("		syouhin_cd")
            .AppendLine("	FROM ")
            .AppendLine("		m_tatouwaribiki_settei WITH(READCOMMITTED)")
            .AppendLine("	WHERE ")
            .AppendLine("	    toukubun=@toukubun3 ")
            .AppendLine("	) AS tou ")
            .AppendLine("LEFT JOIN")
            .AppendLine("		(SELECT kbn , ")
            .AppendLine("			kameiten_cd,  ")
            .AppendLine("			torikesi,  ")
            .AppendLine("			kameiten_mei1,  ")
            .AppendLine("			kameiten_mei2,  ")
            .AppendLine("			tenmei_kana1,  ")
            .AppendLine("			tenmei_kana2,  ")
            .AppendLine("			keiretu_cd  ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten WITH(READCOMMITTED)")
            .AppendLine("		) AS kmt ")
            .AppendLine("	ON tou.kameiten_cd=kmt.kameiten_cd ")
            .AppendLine("LEFT JOIN")
            .AppendLine("	(SELECT syouhin.syouhin_cd , ")
            .AppendLine("		syouhin.syouhin_mei  ")
            .AppendLine("	FROM ")
            .AppendLine("		(SELECT syouhin_cd , ")
            .AppendLine("			syouhin_mei  ")
            .AppendLine("		FROM ")
            .AppendLine("			m_syouhin WITH(READCOMMITTED)")
            .AppendLine("		WHERE ")
            .AppendLine("			souko_cd=@souko_cd")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT DISTINCT '00000' AS syouhin_cd , ")
            .AppendLine("			'自動設定なし' AS syouhin_mei  ")
            .AppendLine("		FROM ")
            .AppendLine("		m_syouhin WITH(READCOMMITTED)) AS syouhin")
            .AppendLine("		) AS sh ")
            .AppendLine("	ON tou.syouhin_cd=sh.syouhin_cd  ")
            .AppendLine(")AS TouKubun3")
            .AppendLine("ON TouKubun1.kameiten_cd=TouKubun3.kameiten_cd")
            .AppendLine("OR TouKubun2.kameiten_cd=TouKubun3.kameiten_cd")
            .AppendLine("WHERE cast((isnull(TouKubun1.torikesi,'0')+isnull(TouKubun2.torikesi,'0')+isnull(TouKubun3.torikesi,'0'))  AS INT ) = 0")
            If strKameitenCdFrom <> String.Empty Then
                If strKameitenCdTo = String.Empty Then
                    .AppendLine("AND ")
                    .AppendLine("	ISNULL(TouKubun1.kameiten_cd,ISNULL(TouKubun2.kameiten_cd,TouKubun3.kameiten_cd)) >= @kameiten_cd_from ")
                Else
                    .AppendLine("AND ")
                    .AppendLine("	(ISNULL(TouKubun1.kameiten_cd,ISNULL(TouKubun2.kameiten_cd,TouKubun3.kameiten_cd)) >= @kameiten_cd_from ")
                    .AppendLine("AND ")
                    .AppendLine("	ISNULL(TouKubun1.kameiten_cd,ISNULL(TouKubun2.kameiten_cd,TouKubun3.kameiten_cd)) <= @kameiten_cd_to) ")
                End If
            End If
            If strKameitenMei <> String.Empty Then
                .AppendLine("AND (")
                .AppendLine("	ISNULL(ISNULL(TouKubun1.kameiten_mei,ISNULL(TouKubun2.kameiten_mei,TouKubun3.kameiten_mei)),'') LIKE @kameiten_mei1 ")
                .AppendLine("OR")
                .AppendLine("	ISNULL(ISNULL(TouKubun1.kameiten_mei2,ISNULL(TouKubun2.kameiten_mei2,TouKubun3.kameiten_mei2)),'') LIKE @kameiten_mei2) ")
            End If
            If strKameitenKana <> String.Empty Then
                .AppendLine("AND (")
                .AppendLine("	ISNULL(ISNULL(TouKubun1.tenmei_kana1,ISNULL(TouKubun2.tenmei_kana1,TouKubun3.tenmei_kana1)),'') LIKE @tenmei_kana1 ")
                .AppendLine("OR")
                .AppendLine("	ISNULL(ISNULL(TouKubun1.tenmei_kana2,ISNULL(TouKubun2.tenmei_kana2,TouKubun3.tenmei_kana2)),'') LIKE @tenmei_kana2) ")
            End If
            If strKeiretuCd <> String.Empty Then
                .AppendLine("AND ")
                .AppendLine("	ISNULL(ISNULL(TouKubun1.keiretu_cd,ISNULL(TouKubun2.keiretu_cd,TouKubun3.keiretu_cd)),'') = @keiretu_cd ")
            End If
            If strSyouhin <> String.Empty Then
                .AppendLine("AND ")
                .AppendLine("	ISNULL(ISNULL(TouKubun1.syouhin_cd,ISNULL(TouKubun2.syouhin_cd,TouKubun3.syouhin_cd)),'') = @syouhin_cd ")
            End If
        End With

        'パラメータ 
        lstParam.Add(MakeParam("@souko_cd", SqlDbType.VarChar, 3, "115"))
        lstParam.Add(MakeParam("@toukubun1", SqlDbType.Int, 4, "1"))
        lstParam.Add(MakeParam("@toukubun2", SqlDbType.Int, 4, "2"))
        lstParam.Add(MakeParam("@toukubun3", SqlDbType.Int, 4, "3"))
        lstParam.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, strKameitenCdFrom))
        lstParam.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 5, strKameitenCdTo))
        lstParam.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 40, "%" & strKameitenMei & "%"))
        lstParam.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 40, "%" & strKameitenMei & "%"))
        lstParam.Add(MakeParam("@tenmei_kana1", SqlDbType.VarChar, 20, "%" & strKameitenKana & "%"))
        lstParam.Add(MakeParam("@tenmei_kana2", SqlDbType.VarChar, 20, "%" & strKameitenKana & "%"))
        lstParam.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, strKeiretuCd))
        lstParam.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhin))

        SQLHelper.FillDataset(strCon, CommandType.Text, _
        sbSql.ToString, _
        dsWaribikiCount, dsWaribikiCount.WaribikiCountTable.TableName, lstParam.ToArray)

        lstParam.Clear()
        intWaribiki = Convert.ToInt32(dsWaribikiCount.WaribikiCountTable.Rows(0).Item("num"))
        Return intWaribiki
    End Function
End Class
