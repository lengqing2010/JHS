Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

Public Class TyousaSijisyoDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>調査指示情報</summary>
    ''' <returns>調査指示情報</returns>
    ''' <history>2016/11/24　李松涛(大連情報システム部)　新規作成</history>
    Public Function SelTyousaSijisyo(ByVal kbn As String _
                                , ByVal hosyousyo_no As String ) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            '.AppendLine("SELECT ")
            '.AppendLine("       t_jiban.kbn                   AS 区分, ")
            '.AppendLine("       t_jiban.hosyousyo_no          AS 物件番号, ")
            '.AppendLine("       m_tyousakaisya.tys_kaisya_mei AS 調査会社名, ")
            '.AppendLine("       m_kameiten.jiosaki_flg        AS JIO先, ")
            '.AppendLine("       m_syouhin.syouhin_mei         AS 商品名, ")
            '.AppendLine("       m_kameiten.kameiten_mei1      AS 加盟店名, ")
            '.AppendLine("       t_jiban.sesyu_mei             AS 物件名称, ")
            '.AppendLine("       [bukken_jyuusyo1] + [bukken_jyuusyo2] ")
            '.AppendLine("       + [bukken_jyuusyo3]                   AS 物件住所, ")
            '.AppendLine("       m_syouhin.syouhin_mei         AS 商品, ")
            '.AppendLine("       t_jiban.douji_irai_tousuu     AS 依頼棟数, ")
            '.AppendLine("       m_tyousahouhou.tys_houhou_mei AS 調査方法, ")
            '.AppendLine("       t_jiban.tatiai_umu            AS 立会い有無, ")
            '.AppendLine("		t_jiban.tyousa_date            AS 調査日,")
            '.AppendLine("		t_jiban.tyousa_kaishi_jikan            AS 調査時刻,")
            '.AppendLine("		t_jiban.yoyaku_jyoukyou            AS 予約状況,")
            '.AppendLine("		t_jiban.tottukki_jikou            AS 特記事項,")
            '.AppendLine("		t_jiban.tys_shijisyo_sakuseisya            AS 調査指示書作成者,")
            '.AppendLine("		t_jiban.tottukki_jikou_henkou_rireki            AS 特記事項_変更履歴")

            '.AppendLine("FROM   (((m_kameiten ")
            '.AppendLine("          INNER JOIN t_jiban ")
            '.AppendLine("                  ON m_kameiten.kameiten_cd = ")
            '.AppendLine("                     t_jiban.kameiten_cd) ")
            '.AppendLine("         INNER JOIN (m_syouhin ")
            '.AppendLine("                     INNER JOIN t_teibetu_seikyuu ")
            '.AppendLine("                             ON m_syouhin.syouhin_cd = ")
            '.AppendLine("                                t_teibetu_seikyuu.syouhin_cd) ")
            '.AppendLine("                 ON ( t_jiban.hosyousyo_no = ")
            '.AppendLine("                      t_teibetu_seikyuu.hosyousyo_no ) ")
            '.AppendLine("                    AND ( t_jiban.kbn = t_teibetu_seikyuu.kbn )) ")
            '.AppendLine("        INNER JOIN m_tyousakaisya ")
            '.AppendLine("                ON ( t_jiban.tys_kaisya_jigyousyo_cd = ")
            '.AppendLine("                   m_tyousakaisya.jigyousyo_cd ) ")
            '.AppendLine("                   AND ( t_jiban.tys_kaisya_cd = ")
            '.AppendLine("                         m_tyousakaisya.tys_kaisya_cd )) ")
            '.AppendLine("       INNER JOIN m_tyousahouhou ")
            '.AppendLine("               ON t_jiban.tys_houhou = ")
            '.AppendLine("                  m_tyousahouhou.tys_houhou_no ")


            '仕様変更　2017/03/06 鄭鴻　変更↓

            '.AppendLine("SELECT ")
            '.AppendLine("	t_jiban.kbn                   AS 区分, ")
            '.AppendLine("	t_jiban.hosyousyo_no          AS 物件番号, ")
            '.AppendLine("	m_tyousakaisya.tys_kaisya_mei AS 調査会社名, ")
            '.AppendLine("	m_kameiten.jiosaki_flg        AS JIO先, ")
            '.AppendLine("	m_syouhin.syouhin_mei         AS 商品名, ")
            '.AppendLine("	m_kameiten.kameiten_mei1      AS 加盟店名, ")
            '.AppendLine("	t_jiban.sesyu_mei             AS 物件名称, ")
            '.AppendLine("	[bukken_jyuusyo1] + [bukken_jyuusyo2] ")
            '.AppendLine("	+ [bukken_jyuusyo3]           AS 物件住所, ")
            '.AppendLine("	m_syouhin.syouhin_mei         AS 商品, ")
            '.AppendLine("	t_jiban.douji_irai_tousuu     AS 依頼棟数, ")
            '.AppendLine("	m_tyousahouhou.tys_houhou_mei AS 調査方法, ")
            '.AppendLine("	t_jiban.tatiai_umu            AS 立会い有無, ")
            '.AppendLine("	t_jiban.tyousa_date           AS 調査日,")
            '.AppendLine("	t_jiban.tyousa_kaishi_jikan   AS 調査時刻,")
            '.AppendLine("	t_jiban.yoyaku_jyoukyou       AS 予約状況,")
            '.AppendLine("	t_jiban.tottukki_jikou        AS 特記事項,")
            '.AppendLine("	m_jhs_mailbox.DisplayName     AS 調査指示書作成者,")
            '.AppendLine("	t_jiban.tottukki_jikou_henkou_rireki            AS 特記事項_変更履歴")
            '.AppendLine("FROM ")
            '.AppendLine("	t_jiban")
            '.AppendLine("	INNER JOIN m_kameiten")
            '.AppendLine("		ON m_kameiten.kameiten_cd = t_jiban.kameiten_cd")
            '.AppendLine("	INNER JOIN t_teibetu_seikyuu")
            '.AppendLine("		ON t_jiban.hosyousyo_no = t_teibetu_seikyuu.hosyousyo_no")
            '.AppendLine("		AND t_jiban.kbn = t_teibetu_seikyuu.kbn	")
            '.AppendLine("	INNER JOIN m_syouhin")
            '.AppendLine("		ON m_syouhin.syouhin_cd = t_teibetu_seikyuu.syouhin_cd")
            '.AppendLine("	INNER JOIN m_tyousakaisya ")
            '.AppendLine("		ON t_jiban.tys_kaisya_jigyousyo_cd = m_tyousakaisya.jigyousyo_cd")
            '.AppendLine("       AND t_jiban.tys_kaisya_cd = m_tyousakaisya.tys_kaisya_cd")
            '.AppendLine("	INNER JOIN m_tyousahouhou ")
            '.AppendLine("		ON t_jiban.tys_houhou = m_tyousahouhou.tys_houhou_no")
            '.AppendLine("	LEFT JOIN m_jhs_mailbox ")
            '.AppendLine("		ON t_jiban.tys_shijisyo_sakuseisya = m_jhs_mailbox.AliasName")

            '.AppendLine("WHERE  t_teibetu_seikyuu.bunrui_cd = '100' ")
            '.AppendLine("       AND t_jiban.kbn = @kbn ") 'A
            '.AppendLine("       AND t_jiban.hosyousyo_no = @hosyousyo_no ") '2009030675


            .AppendLine("DECLARE @msg VARCHAR(8000)")
            .AppendLine("DECLARE @hyouji_syouhin_mei VARCHAR(100)")
            .AppendLine("DECLARE @idx int")
            .AppendLine("SET @msg = ''")
            .AppendLine("SET @idx = 0")
            .AppendLine("DECLARE cursor1")
            .AppendLine(" ")
            .AppendLine("CURSOR FOR ")
            .AppendLine("SELECT ")
            .AppendLine("	ISNULL(m_tys_shijisyo_option_tys_houhou_hyouji.hyouji_syouhin_mei,'')  as hyouji_syouhin_mei")
            .AppendLine("	FROM t_teibetu_seikyuu")
            .AppendLine("	LEFT JOIN m_tys_shijisyo_option_tys_houhou_hyouji")
            .AppendLine("	ON t_teibetu_seikyuu.syouhin_cd = m_tys_shijisyo_option_tys_houhou_hyouji.syouhin_cd")
            .AppendLine("	WHERE kbn = '" & kbn & "' ")
            .AppendLine("	AND hosyousyo_no= '" & hosyousyo_no & "'  ")
            .AppendLine("	AND ISNULL(m_tys_shijisyo_option_tys_houhou_hyouji.hyouji_syouhin_mei,'')<>''")
            .AppendLine("	ORDER BY hyouji_jyun")
            .AppendLine("OPEN CURSOR1")
            .AppendLine("FETCH NEXT FROM CURSOR1 INTO @hyouji_syouhin_mei")
            .AppendLine("WHILE @@FETCH_STATUS = 0")
            .AppendLine("BEGIN ")
            .AppendLine("	IF (@idx>0) ")
            .AppendLine("	BEGIN ")
            .AppendLine("		SET @msg = @msg+'、'")
            .AppendLine("	END ")
            .AppendLine("	SET @msg = @msg+@hyouji_syouhin_mei")
            .AppendLine("	SET @idx = @idx+1")
            .AppendLine("FETCH NEXT FROM CURSOR1 INTO @hyouji_syouhin_mei")
            .AppendLine("END")
            .AppendLine("CLOSE CURSOR1")
            .AppendLine("DEALLOCATE CURSOR1")

            .AppendLine("SELECT ")
            .AppendLine("	t_jiban.kbn                                     AS 区分, ")
            .AppendLine("	t_jiban.hosyousyo_no                            AS 物件番号, ")
            .AppendLine("	m_tyousakaisya.tys_kaisya_mei                   AS 調査会社名, ")
            .AppendLine("	m_kameiten.jiosaki_flg                          AS JIO先, ")
            .AppendLine("	m_syouhin.syouhin_mei                           AS 商品名, ")
            .AppendLine("	m_kameiten.kameiten_mei1                        AS 加盟店名, ")
            .AppendLine("	t_jiban.sesyu_mei                               AS 物件名称, ")
            .AppendLine("	[bukken_jyuusyo1] + [bukken_jyuusyo2] ")
            .AppendLine("	+ [bukken_jyuusyo3]                             AS 物件住所, ")
            .AppendLine("	m_syouhin.syouhin_mei                           AS 商品, ")
            .AppendLine("	t_jiban.douji_irai_tousuu                       AS 依頼棟数, ")
            .AppendLine("	m_tyousahouhou.tys_houhou_mei                   AS 調査方法, ")
            .AppendLine("	@msg                                            AS オプション調査方法, ")
            .AppendLine("	t_jiban.tatiai_umu                              AS 立会い有無, ")
            .AppendLine("	t_jiban.tatiaisya_cd                            AS 立会者コード, ")
            .AppendLine("	t_jiban.tatiaisya_sonota_bikou                  AS 立会い有無_その他備考, ")
            .AppendLine("	t_jiban.tyousa_date                             AS 調査日,")
            .AppendLine("	t_jiban.tyousa_kaishi_jikan                     AS 調査時刻,")
            .AppendLine("	t_jiban.yoyaku_jyoukyou                         AS 予約状況,")
            .AppendLine("	t_jiban.tottukki_jikou                          AS 特記事項,")
            .AppendLine("	t_jiban.jhs_tyousei_tantou                      AS 調査指示書作成者,")
            .AppendLine("	t_jiban.jhs_tehaitantou_syozoku                 AS JHS手配担当_所属,")
            .AppendLine("	TTHR .tottukki_jikou_henkou_rireki1             AS 特記事項_変更履歴,")

            .AppendLine("	t_jiban.annaizu                     AS 案内図,")
            .AppendLine("	t_jiban.haitizu                     AS 配置図,")
            .AppendLine("	t_jiban.kakukai_heimenzu            AS 各階平面図 ")

            .AppendLine("FROM ")
            .AppendLine("	t_jiban")
            .AppendLine("	INNER JOIN m_kameiten")
            .AppendLine("		ON m_kameiten.kameiten_cd = t_jiban.kameiten_cd")
            .AppendLine("	INNER JOIN t_teibetu_seikyuu")
            .AppendLine("		ON t_jiban.hosyousyo_no = t_teibetu_seikyuu.hosyousyo_no")
            .AppendLine("		AND t_jiban.kbn = t_teibetu_seikyuu.kbn	")
            .AppendLine("	INNER JOIN m_syouhin")
            .AppendLine("		ON m_syouhin.syouhin_cd = t_teibetu_seikyuu.syouhin_cd")
            .AppendLine("	INNER JOIN m_tyousakaisya ")
            .AppendLine("		ON t_jiban.tys_kaisya_jigyousyo_cd = m_tyousakaisya.jigyousyo_cd")
            .AppendLine("       AND t_jiban.tys_kaisya_cd = m_tyousakaisya.tys_kaisya_cd")
            .AppendLine("	INNER JOIN m_tyousahouhou ")
            .AppendLine("		ON t_jiban.tys_houhou = m_tyousahouhou.tys_houhou_no")

            .AppendLine("	LEFT JOIN (SELECT TTHR1.kbn AS kbn, ")
            .AppendLine("		               TTHR1.hosyousyo_no AS hosyousyo_no,")
            .AppendLine("		               TTHR1.rireki_no AS rireki_no,")
            .AppendLine("		               TTHR2.tottukki_jikou_henkou_rireki1 AS tottukki_jikou_henkou_rireki1")
            .AppendLine("		        FROM")
            .AppendLine("		            (SELECT MAX(rireki_no) AS rireki_no,")
            .AppendLine("		                    kbn,")
            .AppendLine("		                    hosyousyo_no ")
            .AppendLine("		             FROM   t_tottukkijikou_henkou_rireki")
            .AppendLine("                    GROUP BY kbn,")
            .AppendLine("                             hosyousyo_no) AS TTHR1")
            .AppendLine("               INNER JOIN t_tottukkijikou_henkou_rireki AS TTHR2")
            .AppendLine("                    ON TTHR1.kbn = TTHR2.kbn")
            .AppendLine("                    AND TTHR1.hosyousyo_no = TTHR2.hosyousyo_no")
            .AppendLine("                    AND TTHR1.rireki_no = TTHR2.rireki_no) AS TTHR ")
            .AppendLine("       ON  t_jiban.kbn = TTHR.kbn")
            .AppendLine("       AND t_jiban.hosyousyo_no = TTHR.hosyousyo_no")


            .AppendLine("WHERE  t_teibetu_seikyuu.bunrui_cd = '100' ")
            .AppendLine("       AND t_jiban.kbn = @kbn ") 'A
            .AppendLine("       AND t_jiban.hosyousyo_no = @hosyousyo_no ") '2009030675
            '仕様変更　2017/03/06 鄭鴻　変更↑
            
        End With

        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, kbn))
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, hosyousyo_no))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTyousaKaisyaMei", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function
End Class
