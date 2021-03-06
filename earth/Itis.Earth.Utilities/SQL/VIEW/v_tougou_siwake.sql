CREATE VIEW [jhs_sys].[v_tougou_siwake]
AS
/***********************************************
入金データテーブルから「仕訳」用のCSV項目を取得するVIEW
***********************************************/
SELECT
     kikan_system_cd
   , keijyo_yyyymm
   , renkei_no
   , kaisya_cd
   , data_syubetu
   , syubetu_kbn1
   , syubetu_kbn2
   , syubetu_kbn3
   , syubetu_kbn4
   , syubetu_kbn5
   , denpyou_no
   , meisai_no
   , denpyou_nyuuryokusya_cd
   , denpyou_nyuuryokusya
   , denpyou_nyuuryoku_yyyymmdd
   , denpyou_saisyu_syouninsya_cd
   , denpyou_saisyu_syouninsya
   , denpyou_saisyu_syounin_yyyymmdd
   , keijyo_yyyymmdd
   , hassei_jigyousyo_cd
   , hassei_jigyousyo_mei
   , kari_kanjyo_kamoku_cd
   , kari_kanjyo_kamoku_mei
   , kari_kanjyo_saimoku_cd
   , kari_kanjyo_saimoku_mei
   , kari_jigyousyo_cd
   , kari_jigyousyo_mei
   , kari_aitesaki_kbn
   , kari_aitesaki
   , kari_aitesaki_mei
   , kari_zei_kbn
   , kari_kanri_tuuka_kingaku
   , kari_torihiki_tuuka_kingaku
   , kari_line_cd
   , kari_line_mei
   , kari_ringi_kessai_no
   , kasi_kanjyo_kamoku_cd
   , kasi_kanjyo_kamoku_mei
   , kasi_kanjyo_saimoku_cd
   , kasi_kanjyo_saimoku_mei
   , kasi_jigyousyo_cd
   , kasi_jigyousyo_mei
   , kasi_aitesaki_kbn
   , kasi_aitesaki
   , kasi_aitesaki_mei
   , kasi_zei_kbn
   , kasi_kanri_tuuka_kingaku
   , kasi_torihiki_tuuka_kingaku
   , kasi_line_cd
   , kasi_line_mei
   , kasi_ringi_kessai_no
   , kari_koji_pj_bukken_no
   , kasi_koji_pj_bukken_no
   , kari_yobi_no
   , kasi_yobi_no
   , kari_thema_cd
   , kasi_thema_cd
   , kari_kotei_sisan_no
   , kasi_kotei_sisan_no
   , siwake_tekiyou
   , meisai_tekiyou
   , meisai_tekiyou2
   , meisai_tekiyou3
   , invice_no
   , torihiki_tuuka_cd
   , kanri_tuuka_cd
   , kansan_tuuka_rate
   , kansan_yyyymmdd
   , simukekoku_cd
   , gensankoku_cd
   , misyu_seikyuusyo_syuturyoku_zumi_flg
   , data_renkei_yyyymmdd
   , tyousei_siwake_flg
   , moji_han_yobi1
   , moji_han_yobi2
   , moji_han_yobi3
   , moji_han_yobi4
   , moji_han_yobi5
   , moji_han_yobi6
   , moji_han_yobi7
   , moji_han_yobi8
   , moji_han_yobi9
   , moji_han_yobi10
   , moji_zen_yobi1
   , moji_zen_yobi2
   , moji_zen_yobi3
   , moji_zen_yobi4
   , moji_zen_yobi5
   , moji_zen_yobi6
   , moji_zen_yobi7
   , moji_zen_yobi8
   , moji_zen_yobi9
   , moji_zen_yobi10
   , suuti_yobi1
   , suuti_yobi2
   , suuti_yobi3
   , suuti_yobi4
   , suuti_yobi5
   , suuti_yobi6
   , suuti_yobi7
   , suuti_yobi8
   , suuti_yobi9
   , suuti_yobi10
FROM
    (SELECT
          '12001' kikan_system_cd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) keijyo_yyyymm
        , '0512001'+substring(convert(varchar, year(n.nyuukin_date)), 3, 2) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +substring('00000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 8) +'0' renkei_no
        , '0796' kaisya_cd
        , 'U017' data_syubetu
        , CAST('' AS nvarchar(1)) syubetu_kbn1
        , CAST('' AS nvarchar(1)) syubetu_kbn2
        , CAST('' AS nvarchar(1)) syubetu_kbn3
        , CAST('' AS nvarchar(1)) syubetu_kbn4
        , CAST('' AS nvarchar(1)) syubetu_kbn5
        , substring('0000000000000000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 20) +'0' denpyou_no
        , '1' meisai_no
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya_cd
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya
        , CAST('' AS nvarchar(1)) denpyou_nyuuryoku_yyyymmdd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya_cd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syounin_yyyymmdd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) keijyo_yyyymmdd
        , 'YMP8  ' hassei_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' hassei_jigyousyo_mei
        , '01116' kari_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_kamoku_mei
        , 'ZZZZ' kari_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_saimoku_mei
        , 'YMP8  ' kari_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kari_jigyousyo_mei
        , '9' kari_aitesaki_kbn
        , '99999999999999' kari_aitesaki
        , CAST('' AS nvarchar(1)) kari_aitesaki_mei
        , '99' kari_zei_kbn
        , isnull(n.genkin,0) kari_kanri_tuuka_kingaku
        , isnull(n.genkin,0) kari_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kari_line_cd
        , CAST('' AS nvarchar(1)) kari_line_mei
        , CAST('' AS nvarchar(1)) kari_ringi_kessai_no
        , '01518' kasi_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_kamoku_mei
        , 'ZZZZ' kasi_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_saimoku_mei
        , 'YMP8  ' kasi_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kasi_jigyousyo_mei
        ,
          CASE
               WHEN isnull(ms.skk_jigyousyo_cd, CAST('' AS nvarchar(1))) <>CAST('' AS nvarchar(1))
               THEN '1'
               ELSE '2'
          END kasi_aitesaki_kbn
        , ISNULL(convert(varchar(14),ms.skk_jigyousyo_cd),replace(n.seikyuu_saki_cd,' ','') + '$$$' + n.seikyuu_saki_brc + '$$$' + n.seikyuu_saki_kbn) kasi_aitesaki
        , CAST('' AS nvarchar(1)) kasi_aitesaki_mei
        , '99' kasi_zei_kbn
        , isnull(n.genkin,0) kasi_kanri_tuuka_kingaku
        , isnull(n.genkin,0) kasi_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kasi_line_cd
        , CAST('' AS nvarchar(1)) kasi_line_mei
        , CAST('' AS nvarchar(1)) kasi_ringi_kessai_no
        , CAST('' AS nvarchar(1)) kari_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kasi_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kari_yobi_no
        , CAST('' AS nvarchar(1)) kasi_yobi_no
        , CAST('' AS nvarchar(1)) kari_thema_cd
        , CAST('' AS nvarchar(1)) kasi_thema_cd
        , CAST('' AS nvarchar(1)) kari_kotei_sisan_no
        , CAST('' AS nvarchar(1)) kasi_kotei_sisan_no
        , CAST('' AS nvarchar(1)) siwake_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou2
        , CAST('' AS nvarchar(1)) meisai_tekiyou3
        , CAST('' AS nvarchar(1)) invice_no
        , 'JPY' torihiki_tuuka_cd
        , 'JPY' kanri_tuuka_cd
        , 1 kansan_tuuka_rate
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) kansan_yyyymmdd
        , CAST('' AS nvarchar(1)) simukekoku_cd
        , CAST('' AS nvarchar(1)) gensankoku_cd
        , CAST('' AS nvarchar(1)) misyu_seikyuusyo_syuturyoku_zumi_flg
        , convert(varchar, year(current_timestamp)) +case WHEN month(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(current_timestamp)) +case WHEN day(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(current_timestamp)) data_renkei_yyyymmdd
        , CAST('' AS nvarchar(1)) tyousei_siwake_flg
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
          t_nyuukin_data n
               LEFT JOIN m_seikyuu_saki ms
                 ON n.seikyuu_saki_cd=ms.seikyuu_saki_cd
                AND n.seikyuu_saki_brc=ms.seikyuu_saki_brc
                AND n.seikyuu_saki_kbn=ms.seikyuu_saki_kbn
     WHERE
          isnull(n.tougou_sousin_flg, 0) =1
      AND isnull(n.genkin,0)<>0
      AND isnull(n.tekiyou_mei,'') not LIKE '%返金%'
     UNION
          ALL
     SELECT
          '12001' kikan_system_cd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) keijyo_yyyymm
        , '0512001'+substring(convert(varchar, year(n.nyuukin_date)), 3, 2) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +substring('00000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 8) +'1' renkei_no
        , '0796' kaisya_cd
        , 'U017' data_syubetu
        , CAST('' AS nvarchar(1)) syubetu_kbn1
        , CAST('' AS nvarchar(1)) syubetu_kbn2
        , CAST('' AS nvarchar(1)) syubetu_kbn3
        , CAST('' AS nvarchar(1)) syubetu_kbn4
        , CAST('' AS nvarchar(1)) syubetu_kbn5
        , substring('0000000000000000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 20) +'1' denpyou_no
        , '1' meisai_no
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya_cd
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya
        , CAST('' AS nvarchar(1)) denpyou_nyuuryoku_yyyymmdd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya_cd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syounin_yyyymmdd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) keijyo_yyyymmdd
        , 'YMP8  ' hassei_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' hassei_jigyousyo_mei
        , '01168' kari_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_kamoku_mei
        , 'ZZZZ' kari_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_saimoku_mei
        , 'YMP8  ' kari_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kari_jigyousyo_mei
        , '9' kari_aitesaki_kbn
        , '99999999999999' kari_aitesaki
        , CAST('' AS nvarchar(1)) kari_aitesaki_mei
        , '99' kari_zei_kbn
        , isnull(n.kogitte,0) kari_kanri_tuuka_kingaku
        , isnull(n.kogitte,0) kari_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kari_line_cd
        , CAST('' AS nvarchar(1)) kari_line_mei
        , CAST('' AS nvarchar(1)) kari_ringi_kessai_no
        , '01518' kasi_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_kamoku_mei
        , 'ZZZZ' kasi_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_saimoku_mei
        , 'YMP8  ' kasi_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kasi_jigyousyo_mei
        ,
          CASE
               WHEN isnull(ms.skk_jigyousyo_cd, CAST('' AS nvarchar(1))) <>CAST('' AS nvarchar(1))
               THEN '1'
               ELSE '2'
          END kasi_aitesaki_kbn
        , ISNULL(convert(varchar(14),ms.skk_jigyousyo_cd),replace(n.seikyuu_saki_cd,' ','') + '$$$' + n.seikyuu_saki_brc + '$$$' + n.seikyuu_saki_kbn) kasi_aitesaki
        , CAST('' AS nvarchar(1)) kasi_aitesaki_mei
        , '99' kasi_zei_kbn
        , isnull(n.kogitte,0) kasi_kanri_tuuka_kingaku
        , isnull(n.kogitte,0) kasi_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kasi_line_cd
        , CAST('' AS nvarchar(1)) kasi_line_mei
        , CAST('' AS nvarchar(1)) kasi_ringi_kessai_no
        , CAST('' AS nvarchar(1)) kari_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kasi_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kari_yobi_no
        , CAST('' AS nvarchar(1)) kasi_yobi_no
        , CAST('' AS nvarchar(1)) kari_thema_cd
        , CAST('' AS nvarchar(1)) kasi_thema_cd
        , CAST('' AS nvarchar(1)) kari_kotei_sisan_no
        , CAST('' AS nvarchar(1)) kasi_kotei_sisan_no
        , CAST('' AS nvarchar(1)) siwake_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou2
        , CAST('' AS nvarchar(1)) meisai_tekiyou3
        , CAST('' AS nvarchar(1)) invice_no
        , 'JPY' torihiki_tuuka_cd
        , 'JPY' kanri_tuuka_cd
        , 1 kansan_tuuka_rate
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) kansan_yyyymmdd
        , CAST('' AS nvarchar(1)) simukekoku_cd
        , CAST('' AS nvarchar(1)) gensankoku_cd
        , CAST('' AS nvarchar(1)) misyu_seikyuusyo_syuturyoku_zumi_flg
        , convert(varchar, year(current_timestamp)) +case WHEN month(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(current_timestamp)) +case WHEN day(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(current_timestamp)) data_renkei_yyyymmdd
        , CAST('' AS nvarchar(1)) tyousei_siwake_flg
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
          t_nyuukin_data n
               LEFT JOIN m_seikyuu_saki ms
                 ON n.seikyuu_saki_cd=ms.seikyuu_saki_cd
                AND n.seikyuu_saki_brc=ms.seikyuu_saki_brc
                AND n.seikyuu_saki_kbn=ms.seikyuu_saki_kbn
     WHERE
          isnull(n.tougou_sousin_flg, 0) =1
      AND isnull(n.kogitte,0)<>0
      AND isnull(n.tekiyou_mei,'') not LIKE '%返金%'
     UNION
          ALL
     SELECT
          '12001' kikan_system_cd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) keijyo_yyyymm
        , '0512001'+substring(convert(varchar, year(n.nyuukin_date)), 3, 2) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +substring('00000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 8) +'2' renkei_no
        , '0796' kaisya_cd
        , 'U017' data_syubetu
        , CAST('' AS nvarchar(1)) syubetu_kbn1
        , CAST('' AS nvarchar(1)) syubetu_kbn2
        , CAST('' AS nvarchar(1)) syubetu_kbn3
        , CAST('' AS nvarchar(1)) syubetu_kbn4
        , CAST('' AS nvarchar(1)) syubetu_kbn5
        , substring('0000000000000000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 20) +'2' denpyou_no
        , '1' meisai_no
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya_cd
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya
        , CAST('' AS nvarchar(1)) denpyou_nyuuryoku_yyyymmdd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya_cd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syounin_yyyymmdd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) keijyo_yyyymmdd
        , 'YMP8  ' hassei_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' hassei_jigyousyo_mei
        , '01228' kari_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_kamoku_mei
        , 'ZZZZ' kari_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_saimoku_mei
        , 'YMP8  ' kari_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kari_jigyousyo_mei
        , '3' kari_aitesaki_kbn
        , 'YMP8  003959  ' kari_aitesaki
        , CAST('' AS nvarchar(1)) kari_aitesaki_mei
        , '99' kari_zei_kbn
        , isnull(n.furikomi, 0) +isnull(n.kouza_furikae, 0) kari_kanri_tuuka_kingaku
        , isnull(n.furikomi, 0) +isnull(n.kouza_furikae, 0) kari_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kari_line_cd
        , CAST('' AS nvarchar(1)) kari_line_mei
        , CAST('' AS nvarchar(1)) kari_ringi_kessai_no
        , '01518' kasi_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_kamoku_mei
        , 'ZZZZ' kasi_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_saimoku_mei
        , 'YMP8  ' kasi_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kasi_jigyousyo_mei
        ,
          CASE
               WHEN isnull(ms.skk_jigyousyo_cd, CAST('' AS nvarchar(1))) <>CAST('' AS nvarchar(1))
               THEN '1'
               ELSE '2'
          END kasi_aitesaki_kbn
        , ISNULL(convert(varchar(14),ms.skk_jigyousyo_cd),replace(n.seikyuu_saki_cd,' ','') + '$$$' + n.seikyuu_saki_brc + '$$$' + n.seikyuu_saki_kbn) kasi_ai
        , CAST('' AS nvarchar(1)) kasi_aitesaki_mei
        , '99' kasi_zei_kbn
        , isnull(n.furikomi, 0) +isnull(n.kouza_furikae, 0) kasi_kanri_tuuka_kingaku
        , isnull(n.furikomi, 0) +isnull(n.kouza_furikae, 0) kasi_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kasi_line_cd
        , CAST('' AS nvarchar(1)) kasi_line_mei
        , CAST('' AS nvarchar(1)) kasi_ringi_kessai_no
        , CAST('' AS nvarchar(1)) kari_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kasi_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kari_yobi_no
        , CAST('' AS nvarchar(1)) kasi_yobi_no
        , CAST('' AS nvarchar(1)) kari_thema_cd
        , CAST('' AS nvarchar(1)) kasi_thema_cd
        , CAST('' AS nvarchar(1)) kari_kotei_sisan_no
        , CAST('' AS nvarchar(1)) kasi_kotei_sisan_no
        , CAST('' AS nvarchar(1)) siwake_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou2
        , CAST('' AS nvarchar(1)) meisai_tekiyou3
        , CAST('' AS nvarchar(1)) invice_no
        , 'JPY' torihiki_tuuka_cd
        , 'JPY' kanri_tuuka_cd
        , 1 kansan_tuuka_rate
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) kansan_yyyymmdd
        , CAST('' AS nvarchar(1)) simukekoku_cd
        , CAST('' AS nvarchar(1)) gensankoku_cd
        , CAST('' AS nvarchar(1)) misyu_seikyuusyo_syuturyoku_zumi_flg
        , convert(varchar, year(current_timestamp)) +case WHEN month(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(current_timestamp)) +case WHEN day(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(current_timestamp)) data_renkei_yyyymmdd
        , CAST('' AS nvarchar(1)) tyousei_siwake_flg
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
          t_nyuukin_data n
               LEFT JOIN m_seikyuu_saki ms
                 ON n.seikyuu_saki_cd=ms.seikyuu_saki_cd
                AND n.seikyuu_saki_brc=ms.seikyuu_saki_brc
                AND n.seikyuu_saki_kbn=ms.seikyuu_saki_kbn
     WHERE
          isnull(n.tougou_sousin_flg, 0) =1
      AND isnull(n.furikomi, 0) +isnull(n.kouza_furikae, 0) <>0
      AND isnull(n.tekiyou_mei,'') not LIKE '%返金%'
     UNION
          ALL
     SELECT
          '12001' kikan_system_cd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) keijyo_yyyymm
        , '0512001'+substring(convert(varchar, year(n.nyuukin_date)), 3, 2) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +substring('00000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 8) +'3' renkei_no
        , '0796' kaisya_cd
        , 'U017' data_syubetu
        , CAST('' AS nvarchar(1)) syubetu_kbn1
        , CAST('' AS nvarchar(1)) syubetu_kbn2
        , CAST('' AS nvarchar(1)) syubetu_kbn3
        , CAST('' AS nvarchar(1)) syubetu_kbn4
        , CAST('' AS nvarchar(1)) syubetu_kbn5
        , substring('0000000000000000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 20) +'3' denpyou_no
        , '1' meisai_no
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya_cd
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya
        , CAST('' AS nvarchar(1)) denpyou_nyuuryoku_yyyymmdd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya_cd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syounin_yyyymmdd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) keijyo_yyyymmdd
        , 'YMP8  ' hassei_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' hassei_jigyousyo_mei
        , '01317' kari_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_kamoku_mei
        , 'ZZZZ' kari_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_saimoku_mei
        , 'YMP8  ' kari_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kari_jigyousyo_mei
        , '9' kari_aitesaki_kbn
        , '99999999999999' kari_aitesaki
        , CAST('' AS nvarchar(1)) kari_aitesaki_mei
        , '99' kari_zei_kbn
        , isnull(n.tegata,0) kari_kanri_tuuka_kingaku
        , isnull(n.tegata,0) kari_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kari_line_cd
        , CAST('' AS nvarchar(1)) kari_line_mei
        , CAST('' AS nvarchar(1)) kari_ringi_kessai_no
        , '01518' kasi_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_kamoku_mei
        , 'ZZZZ' kasi_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_saimoku_mei
        , 'YMP8  ' kasi_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kasi_jigyousyo_mei
        ,
          CASE
               WHEN isnull(ms.skk_jigyousyo_cd, CAST('' AS nvarchar(1))) <>CAST('' AS nvarchar(1))
               THEN '1'
               ELSE '2'
          END kasi_aitesaki_kbn
        , ISNULL(convert(varchar(14),ms.skk_jigyousyo_cd),replace(n.seikyuu_saki_cd,' ','') + '$$$' + n.seikyuu_saki_brc + '$$$' + n.seikyuu_saki_kbn) kasi_ai
        , CAST('' AS nvarchar(1)) kasi_aitesaki_mei
        , '99' kasi_zei_kbn
        , isnull(n.tegata,0) kasi_kanri_tuuka_kingaku
        , isnull(n.tegata,0) kasi_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kasi_line_cd
        , CAST('' AS nvarchar(1)) kasi_line_mei
        , CAST('' AS nvarchar(1)) kasi_ringi_kessai_no
        , CAST('' AS nvarchar(1)) kari_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kasi_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kari_yobi_no
        , CAST('' AS nvarchar(1)) kasi_yobi_no
        , CAST('' AS nvarchar(1)) kari_thema_cd
        , CAST('' AS nvarchar(1)) kasi_thema_cd
        , CAST('' AS nvarchar(1)) kari_kotei_sisan_no
        , CAST('' AS nvarchar(1)) kasi_kotei_sisan_no
        , CAST('' AS nvarchar(1)) siwake_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou2
        , CAST('' AS nvarchar(1)) meisai_tekiyou3
        , CAST('' AS nvarchar(1)) invice_no
        , 'JPY' torihiki_tuuka_cd
        , 'JPY' kanri_tuuka_cd
        , 1 kansan_tuuka_rate
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) kansan_yyyymmdd
        , CAST('' AS nvarchar(1)) simukekoku_cd
        , CAST('' AS nvarchar(1)) gensankoku_cd
        , CAST('' AS nvarchar(1)) misyu_seikyuusyo_syuturyoku_zumi_flg
        , convert(varchar, year(current_timestamp)) +case WHEN month(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(current_timestamp)) +case WHEN day(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(current_timestamp)) data_renkei_yyyymmdd
        , CAST('' AS nvarchar(1)) tyousei_siwake_flg
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
          t_nyuukin_data n
               LEFT JOIN m_seikyuu_saki ms
                 ON n.seikyuu_saki_cd=ms.seikyuu_saki_cd
                AND n.seikyuu_saki_brc=ms.seikyuu_saki_brc
                AND n.seikyuu_saki_kbn=ms.seikyuu_saki_kbn
     WHERE
          isnull(n.tougou_sousin_flg, 0) =1
      AND isnull(n.tegata,0)<>0
      AND isnull(n.tekiyou_mei,'') not LIKE '%返金%'
     UNION
          ALL
     SELECT
          '12001' kikan_system_cd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) keijyo_yyyymm
        , '0512001'+substring(convert(varchar, year(n.nyuukin_date)), 3, 2) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +substring('00000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 8) +'4' renkei_no
        , '0796' kaisya_cd
        , 'U017' data_syubetu
        , CAST('' AS nvarchar(1)) syubetu_kbn1
        , CAST('' AS nvarchar(1)) syubetu_kbn2
        , CAST('' AS nvarchar(1)) syubetu_kbn3
        , CAST('' AS nvarchar(1)) syubetu_kbn4
        , CAST('' AS nvarchar(1)) syubetu_kbn5
        , substring('0000000000000000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 20) +'4' denpyou_no
        , '1' meisai_no
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya_cd
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya
        , CAST('' AS nvarchar(1)) denpyou_nyuuryoku_yyyymmdd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya_cd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syounin_yyyymmdd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) keijyo_yyyymmdd
        , 'YMP8  ' hassei_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' hassei_jigyousyo_mei
        , '71690' kari_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_kamoku_mei
        , '7700' kari_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_saimoku_mei
        , 'YMP8  ' kari_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kari_jigyousyo_mei
        , '9' kari_aitesaki_kbn
        , '99999999999999' kari_aitesaki
        , CAST('' AS nvarchar(1)) kari_aitesaki_mei
        , s.tougou_siire_zei_kbn kari_zei_kbn
        , CONVERT(int,CASE WHEN isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)>0 THEN ceiling((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) ELSE floor((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) END) kari_kanri_tuuka_kingaku
        , CONVERT(int,CASE WHEN isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)>0 THEN ceiling((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) ELSE floor((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) END) kari_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kari_line_cd
        , CAST('' AS nvarchar(1)) kari_line_mei
        , CAST('' AS nvarchar(1)) kari_ringi_kessai_no
        , '01518' kasi_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_kamoku_mei
        , 'ZZZZ' kasi_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_saimoku_mei
        , 'YMP8  ' kasi_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kasi_jigyousyo_mei
        ,
          CASE
               WHEN isnull(ms.skk_jigyousyo_cd, CAST('' AS nvarchar(1))) <>CAST('' AS nvarchar(1))
               THEN '1'
               ELSE '2'
          END kasi_aitesaki_kbn
        , ISNULL(convert(varchar(14),ms.skk_jigyousyo_cd),replace(n.seikyuu_saki_cd,' ','') + '$$$' + n.seikyuu_saki_brc + '$$$' + n.seikyuu_saki_kbn) kasi_ai
        , CAST('' AS nvarchar(1)) kasi_aitesaki_mei
        , '99' kasi_zei_kbn
        , CONVERT(int,CASE WHEN isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)>0 THEN ceiling((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) ELSE floor((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) END) kasi_kanri_tuuka_kingaku
        , CONVERT(int,CASE WHEN isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)>0 THEN ceiling((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) ELSE floor((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) END) kasi_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kasi_line_cd
        , CAST('' AS nvarchar(1)) kasi_line_mei
        , CAST('' AS nvarchar(1)) kasi_ringi_kessai_no
        , CAST('' AS nvarchar(1)) kari_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kasi_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kari_yobi_no
        , CAST('' AS nvarchar(1)) kasi_yobi_no
        , CAST('' AS nvarchar(1)) kari_thema_cd
        , CAST('' AS nvarchar(1)) kasi_thema_cd
        , CAST('' AS nvarchar(1)) kari_kotei_sisan_no
        , CAST('' AS nvarchar(1)) kasi_kotei_sisan_no
        , CAST('' AS nvarchar(1)) siwake_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou2
        , CAST('' AS nvarchar(1)) meisai_tekiyou3
        , CAST('' AS nvarchar(1)) invice_no
        , 'JPY' torihiki_tuuka_cd
        , 'JPY' kanri_tuuka_cd
        , 1 kansan_tuuka_rate
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) kansan_yyyymmdd
        , CAST('' AS nvarchar(1)) simukekoku_cd
        , CAST('' AS nvarchar(1)) gensankoku_cd
        , CAST('' AS nvarchar(1)) misyu_seikyuusyo_syuturyoku_zumi_flg
        , convert(varchar, year(current_timestamp)) +case WHEN month(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(current_timestamp)) +case WHEN day(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(current_timestamp)) data_renkei_yyyymmdd
        , CAST('' AS nvarchar(1)) tyousei_siwake_flg
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
          t_nyuukin_data n
               LEFT JOIN m_seikyuu_saki ms
                 ON n.seikyuu_saki_cd=ms.seikyuu_saki_cd
                AND n.seikyuu_saki_brc=ms.seikyuu_saki_brc
                AND n.seikyuu_saki_kbn=ms.seikyuu_saki_kbn
        , m_syouhizei s
     WHERE
          isnull(n.tougou_sousin_flg, 0) =1
      AND isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0) <>0
      AND s.kihon_flg=1
      AND isnull(n.tekiyou_mei,'') not LIKE '%返金%'
     UNION
          ALL
     SELECT
          '12001' kikan_system_cd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) keijyo_yyyymm
        , '0512001'+substring(convert(varchar, year(n.nyuukin_date)), 3, 2) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +substring('00000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 8) +'5' renkei_no
        , '0796' kaisya_cd
        , 'U017' data_syubetu
        , CAST('' AS nvarchar(1)) syubetu_kbn1
        , CAST('' AS nvarchar(1)) syubetu_kbn2
        , CAST('' AS nvarchar(1)) syubetu_kbn3
        , CAST('' AS nvarchar(1)) syubetu_kbn4
        , CAST('' AS nvarchar(1)) syubetu_kbn5
        , substring('0000000000000000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 20) +'5' denpyou_no
        , '1' meisai_no
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya_cd
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya
        , CAST('' AS nvarchar(1)) denpyou_nyuuryoku_yyyymmdd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya_cd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syounin_yyyymmdd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) keijyo_yyyymmdd
        , 'YMP8  ' hassei_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' hassei_jigyousyo_mei
        , '03919' kari_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_kamoku_mei
        , '7700' kari_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_saimoku_mei
        , 'YMP8  ' kari_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kari_jigyousyo_mei
        , '9' kari_aitesaki_kbn
        , '99999999999999' kari_aitesaki
        , CAST('' AS nvarchar(1)) kari_aitesaki_mei
        , '77' kari_zei_kbn
        , (isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) -CONVERT(int,CASE WHEN isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)>0 THEN ceiling((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) ELSE floor((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) END) kari_kanri_tuuka_kingaku
        , (isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) -CONVERT(int,CASE WHEN isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)>0 THEN ceiling((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) ELSE floor((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) END) kari_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kari_line_cd
        , CAST('' AS nvarchar(1)) kari_line_mei
        , CAST('' AS nvarchar(1)) kari_ringi_kessai_no
        , '01518' kasi_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_kamoku_mei
        , 'ZZZZ' kasi_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_saimoku_mei
        , 'YMP8  ' kasi_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kasi_jigyousyo_mei
        ,
          CASE
               WHEN isnull(ms.skk_jigyousyo_cd, CAST('' AS nvarchar(1))) <>CAST('' AS nvarchar(1))
               THEN '1'
               ELSE '2'
          END kasi_aitesaki_kbn
        , ISNULL(convert(varchar(14),ms.skk_jigyousyo_cd),replace(n.seikyuu_saki_cd,' ','') + '$$$' + n.seikyuu_saki_brc + '$$$' + n.seikyuu_saki_kbn) kasi_ai
        , CAST('' AS nvarchar(1)) kasi_aitesaki_mei
        , '99' kasi_zei_kbn
        , (isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) -CONVERT(int,CASE WHEN isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)>0 THEN ceiling((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) ELSE floor((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) END) kasi_kanri_tuuka_kingaku
        , (isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) -CONVERT(int,CASE WHEN isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)>0 THEN ceiling((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) ELSE floor((isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0)) /(1+s.zeiritu)) END) kasi_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kasi_line_cd
        , CAST('' AS nvarchar(1)) kasi_line_mei
        , CAST('' AS nvarchar(1)) kasi_ringi_kessai_no
        , CAST('' AS nvarchar(1)) kari_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kasi_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kari_yobi_no
        , CAST('' AS nvarchar(1)) kasi_yobi_no
        , CAST('' AS nvarchar(1)) kari_thema_cd
        , CAST('' AS nvarchar(1)) kasi_thema_cd
        , CAST('' AS nvarchar(1)) kari_kotei_sisan_no
        , CAST('' AS nvarchar(1)) kasi_kotei_sisan_no
        , CAST('' AS nvarchar(1)) siwake_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou2
        , CAST('' AS nvarchar(1)) meisai_tekiyou3
        , CAST('' AS nvarchar(1)) invice_no
        , 'JPY' torihiki_tuuka_cd
        , 'JPY' kanri_tuuka_cd
        , 1 kansan_tuuka_rate
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) kansan_yyyymmdd
        , CAST('' AS nvarchar(1)) simukekoku_cd
        , CAST('' AS nvarchar(1)) gensankoku_cd
        , CAST('' AS nvarchar(1)) misyu_seikyuusyo_syuturyoku_zumi_flg
        , convert(varchar, year(current_timestamp)) +case WHEN month(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(current_timestamp)) +case WHEN day(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(current_timestamp)) data_renkei_yyyymmdd
        , CAST('' AS nvarchar(1)) tyousei_siwake_flg
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
          t_nyuukin_data n
               LEFT JOIN m_seikyuu_saki ms
                 ON n.seikyuu_saki_cd=ms.seikyuu_saki_cd
                AND n.seikyuu_saki_brc=ms.seikyuu_saki_brc
                AND n.seikyuu_saki_kbn=ms.seikyuu_saki_kbn
        , m_syouhizei s
     WHERE
          isnull(n.tougou_sousin_flg, 0) =1
      AND isnull(n.kyouryoku_kaihi, 0) +isnull(n.nebiki, 0) <>0
      AND s.kihon_flg=1
      AND isnull(n.tekiyou_mei,'') not LIKE '%返金%'
     UNION
          ALL
     SELECT
          '12001' kikan_system_cd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) keijyo_yyyymm
        , '0512001'+substring(convert(varchar, year(n.nyuukin_date)), 3, 2) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +substring('00000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 8) +'6' renkei_no
        , '0796' kaisya_cd
        , 'U017' data_syubetu
        , CAST('' AS nvarchar(1)) syubetu_kbn1
        , CAST('' AS nvarchar(1)) syubetu_kbn2
        , CAST('' AS nvarchar(1)) syubetu_kbn3
        , CAST('' AS nvarchar(1)) syubetu_kbn4
        , CAST('' AS nvarchar(1)) syubetu_kbn5
        , substring('0000000000000000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 20) +'6' denpyou_no
        , '1' meisai_no
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya_cd
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya
        , CAST('' AS nvarchar(1)) denpyou_nyuuryoku_yyyymmdd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya_cd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syounin_yyyymmdd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) keijyo_yyyymmdd
        , 'YMP8  ' hassei_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' hassei_jigyousyo_mei
        , '71335' kari_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_kamoku_mei
        , '0200' kari_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_saimoku_mei
        , 'YMP8  ' kari_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kari_jigyousyo_mei
        , '9' kari_aitesaki_kbn
        , '99999999999999' kari_aitesaki
        , CAST('' AS nvarchar(1)) kari_aitesaki_mei
        , s.tougou_siire_zei_kbn kari_zei_kbn
        , CONVERT(int,CASE WHEN isnull(furikomi_tesuuryou,0)>0 THEN ceiling(isnull(furikomi_tesuuryou,0)/(1+s.zeiritu)) ELSE floor(isnull(furikomi_tesuuryou,0)/(1+s.zeiritu)) END) kari_kanri_tuuka_kingaku
        , CONVERT(int,CASE WHEN isnull(furikomi_tesuuryou,0)>0 THEN ceiling(isnull(furikomi_tesuuryou,0)/(1+s.zeiritu)) ELSE floor(isnull(furikomi_tesuuryou,0)/(1+s.zeiritu)) END) kari_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kari_line_cd
        , CAST('' AS nvarchar(1)) kari_line_mei
        , CAST('' AS nvarchar(1)) kari_ringi_kessai_no
        , '01518' kasi_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_kamoku_mei
        , 'ZZZZ' kasi_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_saimoku_mei
        , 'YMP8  ' kasi_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kasi_jigyousyo_mei
        ,
          CASE
               WHEN isnull(ms.skk_jigyousyo_cd, CAST('' AS nvarchar(1))) <>CAST('' AS nvarchar(1))
               THEN '1'
               ELSE '2'
          END kasi_aitesaki_kbn
        , ISNULL(convert(varchar(14),ms.skk_jigyousyo_cd),replace(n.seikyuu_saki_cd,' ','') + '$$$' + n.seikyuu_saki_brc + '$$$' + n.seikyuu_saki_kbn) kasi_ai
        , CAST('' AS nvarchar(1)) kasi_aitesaki_mei
        , '99' kasi_zei_kbn
        , CONVERT(int,CASE WHEN isnull(furikomi_tesuuryou,0)>0 THEN ceiling(isnull(furikomi_tesuuryou,0)/(1+s.zeiritu)) ELSE floor(isnull(furikomi_tesuuryou,0)/(1+s.zeiritu)) END) kasi_kanri_tuuka_kingaku
        , CONVERT(int,CASE WHEN isnull(furikomi_tesuuryou,0)>0 THEN ceiling(isnull(furikomi_tesuuryou,0)/(1+s.zeiritu)) ELSE floor(isnull(furikomi_tesuuryou,0)/(1+s.zeiritu)) END) kasi_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kasi_line_cd
        , CAST('' AS nvarchar(1)) kasi_line_mei
        , CAST('' AS nvarchar(1)) kasi_ringi_kessai_no
        , CAST('' AS nvarchar(1)) kari_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kasi_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kari_yobi_no
        , CAST('' AS nvarchar(1)) kasi_yobi_no
        , CAST('' AS nvarchar(1)) kari_thema_cd
        , CAST('' AS nvarchar(1)) kasi_thema_cd
        , CAST('' AS nvarchar(1)) kari_kotei_sisan_no
        , CAST('' AS nvarchar(1)) kasi_kotei_sisan_no
        , CAST('' AS nvarchar(1)) siwake_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou2
        , CAST('' AS nvarchar(1)) meisai_tekiyou3
        , CAST('' AS nvarchar(1)) invice_no
        , 'JPY' torihiki_tuuka_cd
        , 'JPY' kanri_tuuka_cd
        , 1 kansan_tuuka_rate
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) kansan_yyyymmdd
        , CAST('' AS nvarchar(1)) simukekoku_cd
        , CAST('' AS nvarchar(1)) gensankoku_cd
        , CAST('' AS nvarchar(1)) misyu_seikyuusyo_syuturyoku_zumi_flg
        , convert(varchar, year(current_timestamp)) +case WHEN month(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(current_timestamp)) +case WHEN day(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(current_timestamp)) data_renkei_yyyymmdd
        , CAST('' AS nvarchar(1)) tyousei_siwake_flg
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
          t_nyuukin_data n
               LEFT JOIN m_seikyuu_saki ms
                 ON n.seikyuu_saki_cd=ms.seikyuu_saki_cd
                AND n.seikyuu_saki_brc=ms.seikyuu_saki_brc
                AND n.seikyuu_saki_kbn=ms.seikyuu_saki_kbn
        , m_syouhizei s
     WHERE
          isnull(n.tougou_sousin_flg, 0) =1
      AND isnull(n.furikomi_tesuuryou, 0) <>0
      AND s.kihon_flg=1
      AND isnull(n.tekiyou_mei,'') not LIKE '%返金%'
     UNION
          ALL
     SELECT
          '12001' kikan_system_cd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) keijyo_yyyymm
        , '0512001'+substring(convert(varchar, year(n.nyuukin_date)), 3, 2) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +substring('00000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 8) +'7' renkei_no
        , '0796' kaisya_cd
        , 'U017' data_syubetu
        , CAST('' AS nvarchar(1)) syubetu_kbn1
        , CAST('' AS nvarchar(1)) syubetu_kbn2
        , CAST('' AS nvarchar(1)) syubetu_kbn3
        , CAST('' AS nvarchar(1)) syubetu_kbn4
        , CAST('' AS nvarchar(1)) syubetu_kbn5
        , substring('0000000000000000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 20) +'7' denpyou_no
        , '1' meisai_no
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya_cd
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya
        , CAST('' AS nvarchar(1)) denpyou_nyuuryoku_yyyymmdd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya_cd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syounin_yyyymmdd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) keijyo_yyyymmdd
        , 'YMP8  ' hassei_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' hassei_jigyousyo_mei
        , '03919' kari_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_kamoku_mei
        , '7700' kari_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_saimoku_mei
        , 'YMP8  ' kari_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kari_jigyousyo_mei
        , '9' kari_aitesaki_kbn
        , '99999999999999' kari_aitesaki
        , CAST('' AS nvarchar(1)) kari_aitesaki_mei
        , '77' kari_zei_kbn
        , isnull(n.furikomi_tesuuryou, 0) -CONVERT(int,CASE WHEN isnull(n.furikomi_tesuuryou, 0)>0 THEN ceiling( isnull(n.furikomi_tesuuryou, 0) /(1+s.zeiritu)) ELSE floor( isnull(n.furikomi_tesuuryou, 0) /(1+s.zeiritu)) END) kari_kanri_tuuka_kingaku
        , isnull(n.furikomi_tesuuryou, 0) -CONVERT(int,CASE WHEN isnull(n.furikomi_tesuuryou, 0)>0 THEN ceiling( isnull(n.furikomi_tesuuryou, 0) /(1+s.zeiritu)) ELSE floor( isnull(n.furikomi_tesuuryou, 0) /(1+s.zeiritu)) END) kari_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kari_line_cd
        , CAST('' AS nvarchar(1)) kari_line_mei
        , CAST('' AS nvarchar(1)) kari_ringi_kessai_no
        , '01518' kasi_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_kamoku_mei
        , 'ZZZZ' kasi_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_saimoku_mei
        , 'YMP8  ' kasi_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kasi_jigyousyo_mei
        ,
          CASE
               WHEN isnull(ms.skk_jigyousyo_cd, CAST('' AS nvarchar(1))) <>CAST('' AS nvarchar(1))
               THEN '1'
               ELSE '2'
          END kasi_aitesaki_kbn
        , ISNULL(convert(varchar(14),ms.skk_jigyousyo_cd),replace(n.seikyuu_saki_cd,' ','') + '$$$' + n.seikyuu_saki_brc + '$$$' + n.seikyuu_saki_kbn) kasi_ai
        , CAST('' AS nvarchar(1)) kasi_aitesaki_mei
        , '99' kasi_zei_kbn
        , isnull(n.furikomi_tesuuryou, 0) -CONVERT(int,CASE WHEN isnull(n.furikomi_tesuuryou, 0)>0 THEN ceiling( isnull(n.furikomi_tesuuryou, 0) /(1+s.zeiritu)) ELSE floor( isnull(n.furikomi_tesuuryou, 0) /(1+s.zeiritu)) END) kasi_kanri_tuuka_kingaku
        , isnull(n.furikomi_tesuuryou, 0) -CONVERT(int,CASE WHEN isnull(n.furikomi_tesuuryou, 0)>0 THEN ceiling( isnull(n.furikomi_tesuuryou, 0) /(1+s.zeiritu)) ELSE floor( isnull(n.furikomi_tesuuryou, 0) /(1+s.zeiritu)) END) kasi_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kasi_line_cd
        , CAST('' AS nvarchar(1)) kasi_line_mei
        , CAST('' AS nvarchar(1)) kasi_ringi_kessai_no
        , CAST('' AS nvarchar(1)) kari_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kasi_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kari_yobi_no
        , CAST('' AS nvarchar(1)) kasi_yobi_no
        , CAST('' AS nvarchar(1)) kari_thema_cd
        , CAST('' AS nvarchar(1)) kasi_thema_cd
        , CAST('' AS nvarchar(1)) kari_kotei_sisan_no
        , CAST('' AS nvarchar(1)) kasi_kotei_sisan_no
        , CAST('' AS nvarchar(1)) siwake_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou2
        , CAST('' AS nvarchar(1)) meisai_tekiyou3
        , CAST('' AS nvarchar(1)) invice_no
        , 'JPY' torihiki_tuuka_cd
        , 'JPY' kanri_tuuka_cd
        , 1 kansan_tuuka_rate
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) kansan_yyyymmdd
        , CAST('' AS nvarchar(1)) simukekoku_cd
        , CAST('' AS nvarchar(1)) gensankoku_cd
        , CAST('' AS nvarchar(1)) misyu_seikyuusyo_syuturyoku_zumi_flg
        , convert(varchar, year(current_timestamp)) +case WHEN month(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(current_timestamp)) +case WHEN day(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(current_timestamp)) data_renkei_yyyymmdd
        , CAST('' AS nvarchar(1)) tyousei_siwake_flg
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
          t_nyuukin_data n
               LEFT JOIN m_seikyuu_saki ms
                 ON n.seikyuu_saki_cd=ms.seikyuu_saki_cd
                AND n.seikyuu_saki_brc=ms.seikyuu_saki_brc
                AND n.seikyuu_saki_kbn=ms.seikyuu_saki_kbn
        , m_syouhizei s
     WHERE
          isnull(n.tougou_sousin_flg, 0) =1
      AND isnull(n.furikomi_tesuuryou, 0) <>0
      AND s.kihon_flg=1
      AND isnull(n.tekiyou_mei,'') not LIKE '%返金%'
     UNION
          ALL
     SELECT
          '12001' kikan_system_cd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) keijyo_yyyymm
        , '0512001'+substring(convert(varchar, year(n.nyuukin_date)), 3, 2) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +substring('00000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 8) +'8' renkei_no
        , '0796' kaisya_cd
        , 'U017' data_syubetu
        , CAST('' AS nvarchar(1)) syubetu_kbn1
        , CAST('' AS nvarchar(1)) syubetu_kbn2
        , CAST('' AS nvarchar(1)) syubetu_kbn3
        , CAST('' AS nvarchar(1)) syubetu_kbn4
        , CAST('' AS nvarchar(1)) syubetu_kbn5
        , substring('0000000000000000000'+convert(varchar, n.denpyou_unique_no), len(convert(varchar, n.denpyou_unique_no)) +1, 20) +'8' denpyou_no
        , '1' meisai_no
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya_cd
        , CAST('' AS nvarchar(1)) denpyou_nyuuryokusya
        , CAST('' AS nvarchar(1)) denpyou_nyuuryoku_yyyymmdd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya_cd
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syouninsya
        , CAST('' AS nvarchar(1)) denpyou_saisyu_syounin_yyyymmdd
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) keijyo_yyyymmdd
        , 'YMP8  ' hassei_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' hassei_jigyousyo_mei
        , '49749' kari_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_kamoku_mei
        , 'ZZZZ' kari_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kari_kanjyo_saimoku_mei
        , 'YMP8  ' kari_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kari_jigyousyo_mei
        , '9' kari_aitesaki_kbn
        , '99999999999999' kari_aitesaki
        , CAST('' AS nvarchar(1)) kari_aitesaki_mei
        , '99' kari_zei_kbn
        , isnull(n.sousai,0) kari_kanri_tuuka_kingaku
        , isnull(n.sousai,0) kari_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kari_line_cd
        , CAST('' AS nvarchar(1)) kari_line_mei
        , CAST('' AS nvarchar(1)) kari_ringi_kessai_no
        , '01518' kasi_kanjyo_kamoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_kamoku_mei
        , 'ZZZZ' kasi_kanjyo_saimoku_cd
        , CAST('' AS nvarchar(1)) kasi_kanjyo_saimoku_mei
        , 'YMP8  ' kasi_jigyousyo_cd
        , 'ＪＨＳ（株）総務経理部' kasi_jigyousyo_mei
        ,
          CASE
               WHEN isnull(ms.skk_jigyousyo_cd, CAST('' AS nvarchar(1))) <>CAST('' AS nvarchar(1))
               THEN '1'
               ELSE '2'
          END kasi_aitesaki_kbn
        , ISNULL(convert(varchar(14),ms.skk_jigyousyo_cd),replace(n.seikyuu_saki_cd,' ','') + '$$$' + n.seikyuu_saki_brc + '$$$' + n.seikyuu_saki_kbn) kasi_ai
        , CAST('' AS nvarchar(1)) kasi_aitesaki_mei
        , '99' kasi_zei_kbn
        , isnull(n.sousai,0) kasi_kanri_tuuka_kingaku
        , isnull(n.sousai,0) kasi_torihiki_tuuka_kingaku
        , CAST('' AS nvarchar(1)) kasi_line_cd
        , CAST('' AS nvarchar(1)) kasi_line_mei
        , CAST('' AS nvarchar(1)) kasi_ringi_kessai_no
        , CAST('' AS nvarchar(1)) kari_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kasi_koji_pj_bukken_no
        , CAST('' AS nvarchar(1)) kari_yobi_no
        , CAST('' AS nvarchar(1)) kasi_yobi_no
        , CAST('' AS nvarchar(1)) kari_thema_cd
        , CAST('' AS nvarchar(1)) kasi_thema_cd
        , CAST('' AS nvarchar(1)) kari_kotei_sisan_no
        , CAST('' AS nvarchar(1)) kasi_kotei_sisan_no
        , CAST('' AS nvarchar(1)) siwake_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou
        , CAST('' AS nvarchar(1)) meisai_tekiyou2
        , CAST('' AS nvarchar(1)) meisai_tekiyou3
        , CAST('' AS nvarchar(1)) invice_no
        , 'JPY' torihiki_tuuka_cd
        , 'JPY' kanri_tuuka_cd
        , 1 kansan_tuuka_rate
        , convert(varchar, year(n.nyuukin_date)) +case WHEN month(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(n.nyuukin_date)) +case WHEN day(n.nyuukin_date) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(n.nyuukin_date)) kansan_yyyymmdd
        , CAST('' AS nvarchar(1)) simukekoku_cd
        , CAST('' AS nvarchar(1)) gensankoku_cd
        , CAST('' AS nvarchar(1)) misyu_seikyuusyo_syuturyoku_zumi_flg
        , convert(varchar, year(current_timestamp)) +case WHEN month(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, month(current_timestamp)) +case WHEN day(current_timestamp) <10 THEN '0'
ELSE CAST('' AS nvarchar(1)) END +convert(varchar, day(current_timestamp)) data_renkei_yyyymmdd
        , CAST('' AS nvarchar(1)) tyousei_siwake_flg
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
          t_nyuukin_data n
               LEFT JOIN m_seikyuu_saki ms
                 ON n.seikyuu_saki_cd=ms.seikyuu_saki_cd
                AND n.seikyuu_saki_brc=ms.seikyuu_saki_brc
                AND n.seikyuu_saki_kbn=ms.seikyuu_saki_kbn
        , m_syouhizei s
     WHERE
          isnull(n.tougou_sousin_flg, 0) =1
      AND isnull(n.sousai, 0) <>0
      AND s.kihon_flg=1
      AND isnull(n.tekiyou_mei,'') not LIKE '%返金%'
) TBL
