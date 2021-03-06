


CREATE VIEW [jhs_sys].[v_th_seikyuu] AS 
SELECT
  m_th_seikyuuyou_kakaku.syouhin_cd
  , m_th_seikyuuyou_kakaku.th_muke_kkk
  , m_th_seikyuuyou_kakaku.kameiten_muke_kkk
  , m_th_seikyuuyou_kakaku.syubetu_mei
  , m_th_seikyuuyou_kakaku.riyuu_cd
  , m_th_seikyuuyou_kakaku.riyuu
  , m_th_seikyuuyou_kakaku.syouhin_mei
  , m_th_seikyuuyou_kakaku.kakeritu 
FROM
  ( 
    SELECT
      syouhin_cd
      , MIN(th_muke_kkk) AS min_val_th_muke_kkk 
    FROM
      jhs_sys.m_th_seikyuuyou_kakaku 
    GROUP BY
      syouhin_cd
  ) min_th_muke_kkk
  , jhs_sys.m_th_seikyuuyou_kakaku as m_th_seikyuuyou_kakaku
WHERE
  m_th_seikyuuyou_kakaku.syouhin_cd = min_th_muke_kkk.syouhin_cd 
  AND m_th_seikyuuyou_kakaku.th_muke_kkk = min_th_muke_kkk.min_val_th_muke_kkk




