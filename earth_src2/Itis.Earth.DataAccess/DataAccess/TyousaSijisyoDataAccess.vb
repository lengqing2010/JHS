Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

Public Class TyousaSijisyoDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>ฒธwฆ๎๑</summary>
    ''' <returns>ฒธwฆ๎๑</returns>
    ''' <history>2016/11/24@ผ(ๅA๎๑VXe)@VK์ฌ</history>
    Public Function SelTyousaSijisyo(ByVal kbn As String _
                                , ByVal hosyousyo_no As String ) As Data.DataTable

        'DataSetCX^Xฬถฌ
        Dim dsReturn As New Data.DataSet

        'SQLถฬถฌ
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQLถ
        With commandTextSb
            '.AppendLine("SELECT ")
            '.AppendLine("       t_jiban.kbn                   AS ๆช, ")
            '.AppendLine("       t_jiban.hosyousyo_no          AS จิ, ")
            '.AppendLine("       m_tyousakaisya.tys_kaisya_mei AS ฒธ๏ะผ, ")
            '.AppendLine("       m_kameiten.jiosaki_flg        AS JIOๆ, ")
            '.AppendLine("       m_syouhin.syouhin_mei         AS คiผ, ")
            '.AppendLine("       m_kameiten.kameiten_mei1      AS มฟXผ, ")
            '.AppendLine("       t_jiban.sesyu_mei             AS จผฬ, ")
            '.AppendLine("       [bukken_jyuusyo1] + [bukken_jyuusyo2] ")
            '.AppendLine("       + [bukken_jyuusyo3]                   AS จZ, ")
            '.AppendLine("       m_syouhin.syouhin_mei         AS คi, ")
            '.AppendLine("       t_jiban.douji_irai_tousuu     AS ห, ")
            '.AppendLine("       m_tyousahouhou.tys_houhou_mei AS ฒธ๛@, ")
            '.AppendLine("       t_jiban.tatiai_umu            AS ง๏ขLณ, ")
            '.AppendLine("		t_jiban.tyousa_date            AS ฒธ๚,")
            '.AppendLine("		t_jiban.tyousa_kaishi_jikan            AS ฒธ,")
            '.AppendLine("		t_jiban.yoyaku_jyoukyou            AS \๑๓ต,")
            '.AppendLine("		t_jiban.tottukki_jikou            AS มL,")
            '.AppendLine("		t_jiban.tys_shijisyo_sakuseisya            AS ฒธwฆ์ฌา,")
            '.AppendLine("		t_jiban.tottukki_jikou_henkou_rireki            AS มL_ฯX๐")

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


            'dlฯX@2017/03/06 A@ฯXซ

            '.AppendLine("SELECT ")
            '.AppendLine("	t_jiban.kbn                   AS ๆช, ")
            '.AppendLine("	t_jiban.hosyousyo_no          AS จิ, ")
            '.AppendLine("	m_tyousakaisya.tys_kaisya_mei AS ฒธ๏ะผ, ")
            '.AppendLine("	m_kameiten.jiosaki_flg        AS JIOๆ, ")
            '.AppendLine("	m_syouhin.syouhin_mei         AS คiผ, ")
            '.AppendLine("	m_kameiten.kameiten_mei1      AS มฟXผ, ")
            '.AppendLine("	t_jiban.sesyu_mei             AS จผฬ, ")
            '.AppendLine("	[bukken_jyuusyo1] + [bukken_jyuusyo2] ")
            '.AppendLine("	+ [bukken_jyuusyo3]           AS จZ, ")
            '.AppendLine("	m_syouhin.syouhin_mei         AS คi, ")
            '.AppendLine("	t_jiban.douji_irai_tousuu     AS ห, ")
            '.AppendLine("	m_tyousahouhou.tys_houhou_mei AS ฒธ๛@, ")
            '.AppendLine("	t_jiban.tatiai_umu            AS ง๏ขLณ, ")
            '.AppendLine("	t_jiban.tyousa_date           AS ฒธ๚,")
            '.AppendLine("	t_jiban.tyousa_kaishi_jikan   AS ฒธ,")
            '.AppendLine("	t_jiban.yoyaku_jyoukyou       AS \๑๓ต,")
            '.AppendLine("	t_jiban.tottukki_jikou        AS มL,")
            '.AppendLine("	m_jhs_mailbox.DisplayName     AS ฒธwฆ์ฌา,")
            '.AppendLine("	t_jiban.tottukki_jikou_henkou_rireki            AS มL_ฯX๐")
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
            .AppendLine("		SET @msg = @msg+'A'")
            .AppendLine("	END ")
            .AppendLine("	SET @msg = @msg+@hyouji_syouhin_mei")
            .AppendLine("	SET @idx = @idx+1")
            .AppendLine("FETCH NEXT FROM CURSOR1 INTO @hyouji_syouhin_mei")
            .AppendLine("END")
            .AppendLine("CLOSE CURSOR1")
            .AppendLine("DEALLOCATE CURSOR1")

            .AppendLine("SELECT ")
            .AppendLine("	t_jiban.kbn                                     AS ๆช, ")
            .AppendLine("	t_jiban.hosyousyo_no                            AS จิ, ")
            .AppendLine("	m_tyousakaisya.tys_kaisya_mei                   AS ฒธ๏ะผ, ")
            .AppendLine("	m_kameiten.jiosaki_flg                          AS JIOๆ, ")
            .AppendLine("	m_syouhin.syouhin_mei                           AS คiผ, ")
            .AppendLine("	m_kameiten.kameiten_mei1                        AS มฟXผ, ")
            .AppendLine("	t_jiban.sesyu_mei                               AS จผฬ, ")
            .AppendLine("	[bukken_jyuusyo1] + [bukken_jyuusyo2] ")
            .AppendLine("	+ [bukken_jyuusyo3]                             AS จZ, ")
            .AppendLine("	m_syouhin.syouhin_mei                           AS คi, ")
            .AppendLine("	t_jiban.douji_irai_tousuu                       AS ห, ")
            .AppendLine("	m_tyousahouhou.tys_houhou_mei                   AS ฒธ๛@, ")
            .AppendLine("	@msg                                            AS IvVฒธ๛@, ")
            .AppendLine("	t_jiban.tatiai_umu                              AS ง๏ขLณ, ")
            .AppendLine("	t_jiban.tatiaisya_cd                            AS ง๏าR[h, ")
            .AppendLine("	t_jiban.tatiaisya_sonota_bikou                  AS ง๏ขLณ_ปฬผ๕l, ")
            .AppendLine("	t_jiban.tyousa_date                             AS ฒธ๚,")
            .AppendLine("	t_jiban.tyousa_kaishi_jikan                     AS ฒธ,")
            .AppendLine("	t_jiban.yoyaku_jyoukyou                         AS \๑๓ต,")
            .AppendLine("	t_jiban.tottukki_jikou                          AS มL,")
            .AppendLine("	t_jiban.jhs_tyousei_tantou                      AS ฒธwฆ์ฌา,")
            .AppendLine("	t_jiban.jhs_tehaitantou_syozoku                 AS JHS่zS_ฎ,")
            .AppendLine("	TTHR .tottukki_jikou_henkou_rireki1             AS มL_ฯX๐,")

            .AppendLine("	t_jiban.annaizu                     AS ฤเ},")
            .AppendLine("	t_jiban.haitizu                     AS zu},")
            .AppendLine("	t_jiban.kakukai_heimenzu            AS eKฝส} ")

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
            'dlฯX@2017/03/06 A@ฯXช
            
        End With

        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, kbn))
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, hosyousyo_no))

        ' ๕ภs
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTyousaKaisyaMei", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function
End Class
