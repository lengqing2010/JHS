

CREATE VIEW [jhs_sys].[v_syouhin_siire_seikyuu_saki_jiban_tyskaisya] AS 
/***********************************************
 調査会社ベースに請求先、仕入先、支払集計先を取得するためのVIEW
 項目：区分、番号、商品コード、倉庫コード(分類コード)、調査会社コード、事業所コード、
 　　　請求先コード、請求先枝番、請求先区分、支払集計先事業所コード
 ユニーク検索キー：区分、番号、商品コード
***********************************************/
SELECT
  TJR.kbn
  , TJR.hosyousyo_no
  , MS.syouhin_cd
  , MS.souko_cd
  , MT.tys_kaisya_cd AS siire_saki_tys_kaisya_cd
  , MT.jigyousyo_cd AS siire_saki_tys_jigyousyo_cd
  , MT.tys_kaisya_mei AS siire_saki_tys_kaisya_mei
  , MT.shri_jigyousyo_cd
  , MT.seikyuu_saki_cd
  , MT.seikyuu_saki_brc
  , MT.seikyuu_saki_kbn
FROM
  --商品マスタを親テーブルとする
  jhs_sys.m_syouhin MS 
  --地盤テーブルの調査会社、工事会社、追加工事会社情報をUNIONする
  INNER JOIN ( 
    --調査会社一覧
    SELECT
      kbn
      , hosyousyo_no
      , 'tys' AS type
      , tys_kaisya_cd
      , tys_kaisya_jigyousyo_cd 
    FROM
      jhs_sys.t_jiban 
    WHERE
      tys_kaisya_cd IS NOT NULL
      AND tys_kaisya_jigyousyo_cd IS NOT NULL

    UNION ALL 
    --工事会社一覧
    SELECT
      kbn
      , hosyousyo_no
      , 'koj'
      , koj_gaisya_cd
      , koj_gaisya_jigyousyo_cd 
    FROM
      jhs_sys.t_jiban 
    WHERE
      koj_gaisya_cd IS NOT NULL
      AND koj_gaisya_jigyousyo_cd IS NOT NULL

    UNION ALL 
    --追加工事会社一覧
    SELECT
      kbn
      , hosyousyo_no
      , 'tkoj'
      , t_koj_kaisya_cd
      , t_koj_kaisya_jigyousyo_cd 
    FROM
      jhs_sys.t_jiban
    WHERE
      t_koj_kaisya_cd IS NOT NULL
      AND t_koj_kaisya_jigyousyo_cd IS NOT NULL
  ) TJR 
    ON CASE 
      WHEN MS.souko_cd IN ('100', '110', '115', '120') 
      THEN 'tys' 
      WHEN MS.souko_cd = '130' 
      THEN 'koj' 
      WHEN MS.souko_cd = '140' 
      THEN 'tkoj' 
      END = TJR.type 
  --それぞれの請求先、仕入先を取得するために、調査会社マスタと結合
  INNER JOIN jhs_sys.m_tyousakaisya MT 
    ON TJR.tys_kaisya_cd = MT.tys_kaisya_cd 
    AND TJR.tys_kaisya_jigyousyo_cd = MT.jigyousyo_cd 




