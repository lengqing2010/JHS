
CREATE VIEW [jhs_sys].[v_hanbai_kakaku_info]
AS
/***********************************************
 販売価格マスタをベースに、関連マスタから相手先名を取得するVIEW
  項目：相手先種別、相手先コード、相手先名
 ユニーク検索キー：相手先種別、相手先コード
***********************************************/
SELECT
     MHK.aitesaki_syubetu
     ,MHK.aitesaki_cd
     ,MMM.aitesaki_mei
FROM
     jhs_sys.m_hanbai_kakaku MHK
          INNER JOIN (
               --相手先なし
          SELECT
               'ALL' AS aitesaki_cd
               , '相手先なし' AS aitesaki_mei
               , '0' AS aitesaki_syubetu
          UNION ALL
               --加盟店マスタ
          SELECT
               MKM.kameiten_cd AS aitesaki_cd
               , MKM.kameiten_mei1 AS aitesaki_mei
               , '1' AS aitesaki_syubetu
          FROM
               jhs_sys.m_kameiten MKM
          UNION ALL
               --営業所マスタ
          SELECT
               MEG.eigyousyo_cd AS aitesaki_cd
               , MEG.eigyousyo_mei AS aitesaki_mei
               , '5' AS aitesaki_syubetu
          FROM
               jhs_sys.m_eigyousyo MEG
          UNION ALL
               --系列マスタ
          SELECT
               DISTINCT(MKR.keiretu_cd) AS aitesaki_cd
               , MKR.keiretu_mei AS aitesaki_mei
               , '7' AS aitesaki_syubetu
          FROM
               jhs_sys.m_keiretu MKR
          ) MMM
            ON MHK.aitesaki_cd = MMM.aitesaki_cd
           AND MHK.aitesaki_syubetu = MMM.aitesaki_syubetu

