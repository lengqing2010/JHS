CREATE VIEW [jhs_sys].[v_tougou_uriage]
AS
/***********************************************
売上データテーブルから「売上・未収」用のCSV項目を取得するVIEW
***********************************************/
SELECT
     '12001' kikan_system_cd
   , convert(varchar, year(u.denpyou_uri_date)) +case WHEN month(u.denpyou_uri_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(u.denpyou_uri_date)) keijyo_yyyymm
        , '0112001'+substring(convert(varchar, year(u.denpyou_uri_date)), 3, 2) +case WHEN month(u.denpyou_uri_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(u.denpyou_uri_date)) +substring('000000000'+convert(varchar, u.denpyou_unique_no), len(convert(varchar, u.denpyou_unique_no)) +1, 9) renkei_no
        , '0796' kaisya_cd
        , 'P001' data_syubetu
        ,
          CASE
               WHEN substring(u.denpyou_syubetu, 2, 1) ='N'
               THEN '1UN'
               WHEN substring(u.denpyou_syubetu, 2, 1) <>'N'
               THEN '1UR'
          END syubetu_kbn1
        , z.tougou_syubetu_kbn2 syubetu_kbn2
        , CAST('' AS nvarchar(1)) syubetu_kbn3
        , CAST('' AS nvarchar(1)) syubetu_kbn4
        , CAST('' AS nvarchar(1)) syubetu_kbn5
        , substring('00000000000000000000'+convert(varchar, u.denpyou_unique_no), len(convert(varchar, u.denpyou_unique_no)) +1, 20) denpyou_no
        , '1' meisai_no
        , CAST('' AS nvarchar(1)) nyuuryokusya_cd
        , CAST('' AS nvarchar(1)) nyuuryokusya
        , CAST('' AS nvarchar(1)) syouninsya_cd
        , CAST('' AS nvarchar(1)) syouninsya
        , convert(varchar, year(u.denpyou_uri_date)) +case WHEN month(u.denpyou_uri_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(u.denpyou_uri_date)) +case WHEN day(u.denpyou_uri_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(u.denpyou_uri_date)) keijyo_yyyymmdd
        , convert(varchar, year(u.denpyou_uri_date)) +case WHEN month(u.denpyou_uri_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(u.denpyou_uri_date)) +case WHEN day(u.denpyou_uri_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(u.denpyou_uri_date)) syukka_yyyymmdd
        , convert(varchar, year(u.denpyou_uri_date)) +case WHEN month(u.denpyou_uri_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(u.denpyou_uri_date)) +case WHEN day(u.denpyou_uri_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(u.denpyou_uri_date)) tyakka_yyyymmdd
        , convert(varchar, year(u.denpyou_uri_date)) +case WHEN month(u.denpyou_uri_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(u.denpyou_uri_date)) +case WHEN day(u.denpyou_uri_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(u.denpyou_uri_date)) kensyu_yyyymmdd
--        , CAST('' AS nvarchar(1)) uri_misyu_kbn
        , 'YMP8  ' hassei_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' hassei_jigyousyo_mei
        , 'YMP8  ' uri_misyuu_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' uri_misyu_jigyousyo_mei
        , CAST('' AS nvarchar(1)) line_cd
        , CAST('' AS nvarchar(1)) line_mei
        , 'YMP8  ' urikake_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' urikake_jigyousyo_mei
        , u.seikyuu_saki_cd+'$$$'+u.seikyuu_saki_brc+'$$$'+u.seikyuu_saki_kbn tokuisaki_cd
        , CAST('' AS nvarchar(1)) tokuisaki_mei
        , u.syouhin_cd syouhin_cd
        , CAST('' AS nvarchar(1)) souhin_mei
        , abs(u.suu) suuryou
        , u.tanka tanka
        , CAST('' AS nvarchar(1)) invice_no
        , 'JPY' torihiki_tuuka_cd
        , 'JPY' kanri_tuuka_cd
        , 1 kansan_tuuka_rate
        , convert(varchar, year(u.denpyou_uri_date)) +case WHEN month(u.denpyou_uri_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(u.denpyou_uri_date)) +case WHEN day(u.denpyou_uri_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(u.denpyou_uri_date)) kansan_yyyymmdd
        , u.uri_gaku kanri_tuuka_kingaku
        , u.uri_gaku torihiki_tuuka_kingaku
        , u.sotozei_gaku kanri_tuuka_zeigaku
        , u.sotozei_gaku torihiki_tuuka_zeigaku
        , z.tougou_uri_zei_kbn zei_kbn
        , CAST('' AS nvarchar(1)) kouji_pj_bukken_no
        , CAST('' AS nvarchar(1)) yobi_no
        , CAST('' AS nvarchar(1)) syukkasaki_cd
        , CAST('' AS nvarchar(1)) syukkasaki_mei
        ,
          CASE
               WHEN substring(u.denpyou_syubetu, 2, 1) ='N'
               THEN '0'
               ELSE '1'
          END torikesi_flg
        , CAST('' AS nvarchar(1)) head_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou2
        , CAST('' AS nvarchar(1)) meisai_tekiyou3
        , CAST('' AS nvarchar(1)) eigyou_tantousya_cd
        , CAST('' AS nvarchar(1)) eigyou_tantousya_mei
        ,
          CASE
               WHEN substring(u.denpyou_syubetu, 2, 1) ='N'
               THEN CAST('' AS nvarchar(1))
               ELSE substring('00000000000000000000'+convert(varchar, u.torikesi_moto_denpyou_unique_no), len(convert(varchar, u.torikesi_moto_denpyou_unique_no)) +1, 20)
          END torikesi_denpyou_no
        ,
          CASE
               WHEN substring(u.denpyou_syubetu, 2, 1) ='N'
               THEN CAST('' AS nvarchar(1))
               ELSE '1'
          END torikesi_moto_meisai_no
        , CAST('' AS nvarchar(1)) misyuu_seikyuuso_shuturyokuzumi_flg
        , CAST('' AS nvarchar(1)) aite_kamoku_cd
        , CAST('' AS nvarchar(1)) aite_kamoku_mei
        , CAST('' AS nvarchar(1)) aite_saimoku_cd
        , CAST('' AS nvarchar(1)) aite_saimoku_mei
        , CAST('' AS nvarchar(1)) simukekoku_cd
        , CAST('' AS nvarchar(1)) gensankoku_cd
        , convert(varchar, year(current_timestamp)) +case WHEN month(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(current_timestamp)) +case WHEN day(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(current_timestamp)) data_renkei_yyyymmdd
        , CAST('' AS nvarchar(1)) moji_han_yobi1
        , CAST('' AS nvarchar(1)) moji_han_yobi2
        , CAST('' AS nvarchar(1)) moji_han_yobi3
        , CAST('' AS nvarchar(1)) moji_han_yobi4
        , CAST('' AS nvarchar(1)) moji_han_yobi5
        , CAST('' AS nvarchar(1)) moji_han_yobi6
        , CAST('' AS nvarchar(1)) moji_han_yobi7
        , CAST('' AS nvarchar(1)) moji_han_yobi8
        , CAST('' AS nvarchar(1)) moji_han_yobi9
        , CAST('' AS nvarchar(1)) moji_han_yobi10
        , CAST('' AS nvarchar(1)) moji_zen_yobi1
        , CAST('' AS nvarchar(1)) moji_zen_yobi2
        , CAST('' AS nvarchar(1)) moji_zen_yobi3
        , CAST('' AS nvarchar(1)) moji_zen_yobi4
        , CAST('' AS nvarchar(1)) moji_zen_yobi5
        , CAST('' AS nvarchar(1)) moji_zen_yobi6
        , CAST('' AS nvarchar(1)) moji_zen_yobi7
        , CAST('' AS nvarchar(1)) moji_zen_yobi8
        , CAST('' AS nvarchar(1)) moji_zen_yobi9
        , CAST('' AS nvarchar(1)) moji_zen_yobi10
        , CAST('' AS nvarchar(1)) suuti_yobi1
        , CAST('' AS nvarchar(1)) suuti_yobi2
        , CAST('' AS nvarchar(1)) suuti_yobi3
        , CAST('' AS nvarchar(1)) suuti_yobi4
        , CAST('' AS nvarchar(1)) suuti_yobi5
        , CAST('' AS nvarchar(1)) suuti_yobi6
        , CAST('' AS nvarchar(1)) suuti_yobi7
        , CAST('' AS nvarchar(1)) suuti_yobi8
        , CAST('' AS nvarchar(1)) suuti_yobi9
        , CAST('' AS nvarchar(1)) suuti_yobi10
     FROM
          t_uriage_data u
               LEFT JOIN m_syouhizei z
                 ON u.zei_kbn=z.zei_kbn
     WHERE
          ISNULL(u.uri_keijyou_flg, 0) =1
      AND ISNULL(u.tougou_sousin_flg, 0) =1
      AND u.seikyuu_saki_cd IS NOT NULL
      AND u.seikyuu_saki_brc IS NOT NULL
      AND u.seikyuu_saki_kbn IS NOT NULL