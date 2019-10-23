CREATE VIEW [jhs_sys].[v_hanbai_kakaku]
AS
/***********************************************
”Ì”„‰¿Ši‚ðŽæ“¾‚·‚éVIEW
***********************************************/
  SELECT
       MHK.aitesaki_syubetu
      ,MHK.aitesaki_cd
      ,MHK.syouhin_cd
      ,MHK.tys_houhou_no
      ,CASE 
         WHEN MHK.koukai_flg = 0 THEN
           NULL
         WHEN MHK.koukai_flg IS NULL THEN
           NULL
         ELSE
           CASE MHK.koumuten_seikyuu_gaku
             WHEN 0 THEN MHK.jitu_seikyuu_gaku
             ELSE ISNULL(MHK.koumuten_seikyuu_gaku, 0)
           END
       END seikyuu_gaku
  FROM
       m_hanbai_kakaku MHK
  WHERE
   MHK.torikesi         = 0
