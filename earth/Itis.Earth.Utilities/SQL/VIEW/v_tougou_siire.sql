CREATE VIEW [jhs_sys].[v_tougou_siire]
AS
/***********************************************
仕入データテーブルから「仕入・費用」用のCSV項目を取得するVIEW
***********************************************/
SELECT
     '12001' kikan_system_cd
   , convert(varchar, year(s.denpyou_siire_date)) +case WHEN month(s.denpyou_siire_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(s.denpyou_siire_date)) keijyo_yyyymm
        , '0212001'+substring(convert(varchar, year(s.denpyou_siire_date)), 3, 2) +case WHEN month(s.denpyou_siire_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(s.denpyou_siire_date)) +substring('000000000'+convert(varchar, s.denpyou_unique_no), len(convert(varchar, s.denpyou_unique_no)) +1, 9) renkei_no
        , '0796' kaisya_cd
        , 'Q001' data_syubetu
        ,
          CASE
               WHEN substring(s.denpyou_syubetu, 2, 1) ='N'
               THEN '5SN'
               WHEN substring(s.denpyou_syubetu, 2, 1) <>'N'
               THEN '5SR'
          END syubetu_kbn1
        , z.tougou_syubetu_kbn2 syubetu_kbn2
        , CAST('' AS nvarchar(1)) syubetu_kbn3
        , CAST('' AS nvarchar(1)) syubetu_kbn4
        , CAST('' AS nvarchar(1)) syubetu_kbn5
        , substring('00000000000000000000'+convert(varchar, s.denpyou_unique_no), len(convert(varchar, s.denpyou_unique_no)) +1, 20) denpyou_no
        , '1' meisai_no
        , CAST('' AS nvarchar(1)) nyuuryokusya_cd
        , CAST('' AS nvarchar(1)) nyuuryokusya
        , CAST('' AS nvarchar(1)) syouninsya_cd
        , CAST('' AS nvarchar(1)) syouninsya
        , convert(varchar, year(s.denpyou_siire_date)) +case WHEN month(s.denpyou_siire_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(s.denpyou_siire_date)) +case WHEN day(s.denpyou_siire_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(s.denpyou_siire_date)) keijyo_yyyymmdd
        , CAST('' AS nvarchar(1)) nouhin_yyyymmdd
        , convert(varchar, year(s.denpyou_siire_date)) +case WHEN month(s.denpyou_siire_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(s.denpyou_siire_date)) +case WHEN day(s.denpyou_siire_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(s.denpyou_siire_date)) kensyuu_yyyymmdd
        , 'YMP8  ' hassei_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' hassei_jigyousyo_mei
        , 'YMP8  ' siire_hiyou_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' siire_hiyou_jigyousyo_mei
        , CAST('' AS nvarchar(1)) line_cd
        , CAST('' AS nvarchar(1)) line_mei
        , 'YMP8  ' kaikake_mibarai_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kaikake_mibarai_jigyousyo_mei
        , t.skk_jigyousyo_cd+'  '+t.skk_shri_saki_cd+'  ' siharaisaki_cd
        , CAST('' AS nvarchar(1)) siharaisaki_mei
        , s.syouhin_cd syouhin_cd
        , CAST('' AS nvarchar(1)) souhin_mei
        , abs(suu) suuryou
        , s.tanka tanka
        , CAST('' AS nvarchar(1)) invice_no
        , 'JPY' torihiki_tuuka_cd
        , 'JPY' kanri_tuuka_cd
        , 1 kansan_tuuka_rate
        , convert(varchar, year(s.denpyou_siire_date)) +case WHEN month(s.denpyou_siire_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(s.denpyou_siire_date)) +case WHEN day(s.denpyou_siire_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(s.denpyou_siire_date)) kansan_yyyymmdd
        , s.siire_gaku kanri_tuuka_kingaku
        , s.siire_gaku torihiki_tuuka_kingaku
        , s.sotozei_gaku kanri_tuuka_zeigaku
        , s.sotozei_gaku torihiki_tuuka_zeigaku
        , z.tougou_siire_zei_kbn zei_kbn
        , CAST('' AS nvarchar(1)) ringi_no
        , CAST('' AS nvarchar(1)) kouji_pj_bukken_no
        , CAST('' AS nvarchar(1)) yobi_no
        , CAST('' AS nvarchar(1)) tinryou_bukken_no
        ,
          CASE
               WHEN substring(s.denpyou_syubetu, 2, 1) ='N'
               THEN '0'
               ELSE '1'
          END torikesi_flg
        , CAST('' AS nvarchar(1)) head_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou2
        , CAST('' AS nvarchar(1)) meisai_tekiyou3
        ,
          CASE
               WHEN substring(s.denpyou_syubetu, 2, 1) ='N'
               THEN CAST('' AS nvarchar(1))
               ELSE substring('00000000000000000000'+convert(varchar, s.torikesi_moto_denpyou_unique_no), len(convert(varchar, s.torikesi_moto_denpyou_unique_no)) +1, 20)
          END torikesi_denpyou_no
        ,
          CASE
               WHEN substring(s.denpyou_syubetu, 2, 1) ='N'
               THEN CAST('' AS nvarchar(1))
               ELSE '1'
          END torikesi_moto_meisai_no
        , CAST('' AS nvarchar(1)) aite_kamoku_cd
        , CAST('' AS nvarchar(1)) aite_kamoku_mei
        , CAST('' AS nvarchar(1)) aite_saimoku_cd
        , CAST('' AS nvarchar(1)) aite_saimoku_mei
        , CAST('' AS nvarchar(1)) simukekoku_cd
        , CAST('' AS nvarchar(1))gensankoku_cd
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
          t_siire_data s
               LEFT JOIN m_syouhizei z
                 ON s.zei_kbn=z.zei_kbn
               LEFT JOIN m_tyousakaisya t
                 ON s.tys_kaisya_cd=t.tys_kaisya_cd
                AND s.tys_kaisya_jigyousyo_cd=t.jigyousyo_cd
     WHERE
          ISNULL(s.siire_keijyou_flg, 0) =1
      AND ISNULL(s.tougou_sousin_flg, 0) =1
      AND t.skk_jigyousyo_cd IS NOT NULL
      AND t.skk_shri_saki_cd IS NOT NULL