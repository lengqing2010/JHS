-- =========================================================================================
-- Description:	物件履歴データを走査して、引渡し前(後)データを取得する
--              ITEM：
--                区分
--                番号
--                引渡し前(後)保険[0:未、1:済]
--                引渡し前(後)保険年月[17-11 or 17-21(18-11)の汎用日付]
--                引渡し前(後)保険実施日[17-11 or 17-21(18-11)の管理日付]
--                引渡し前(後)保険適用予定実施日[保険申請対象になった場合、各売上年月日。以外、NULL]
--              結合条件：
--                区分、番号
-- =========================================================================================
CREATE FUNCTION [jhs_sys].[fnGetHwHokenDataTable]
(
  @FlgHokenHantei INT             --[必須] 0=引渡し前保険,1=引渡し後保険
--  ,@kbn           CHAR(1)         --区分
--  ,@bangou        VARCHAR(10)     --番号
)
RETURNS @retData TABLE
(
  [kbn]                                 CHAR(1)     --区分
  ,[hosyousyo_no]                       VARCHAR(10) --番号
  ,[hw_hkn]                           INT         --引渡し前(後)保険
  ,[hw_hkn_date]                   DATETIME    --引渡し前(後)保険年月
  ,[hw_hkn_jissi_date]                DATETIME    --引渡し前(後)保険実施日
)
AS
BEGIN

  DECLARE @hw_mae_hkn_rireki_syubetu  VARCHAR(2)
  SET @hw_mae_hkn_rireki_syubetu = '17'
  
--	--区分,番号より加盟店Mの入金確認条件を取得する
--	IF (@kbn IS NOT NULL AND @bangou IS NOT NULL)
--	BEGIN
    /******************************
    *引渡し前保険データを取得する
    *******************************/
    IF @FlgHokenHantei = 0
    BEGIN  
    
      INSERT INTO
           @retData
      SELECT
           br.kbn
          , br.hosyousyo_no
          , CASE WHEN sub.kbn IS NOT NULL
            THEN 1
            ELSE 0
            END hw_hkn
          , br.hanyou_date
          , br.kanri_date
      FROM
            jhs_sys.t_bukken_rireki br
            ,
             (SELECT
                   brsub.kbn
                 , brsub.hosyousyo_no
                 , MAX(brsub.nyuuryoku_no) AS max
              FROM
                   jhs_sys.t_bukken_rireki brsub
              WHERE
                   brsub.rireki_syubetu = '17'
               AND brsub.rireki_no <> 0
               AND brsub.torikesi = 0
              GROUP BY
                   brsub.kbn
                 , brsub.hosyousyo_no
             )
              sub
       WHERE
            br.kbn = sub.kbn
        AND br.hosyousyo_no = sub.hosyousyo_no
        AND br.nyuuryoku_no = sub.max
--        AND br.kbn = @kbn 
--        AND br.hosyousyo_no = @bangou
        AND br.rireki_syubetu = '17'
        AND (br.rireki_no = 11 OR br.rireki_no = 21)
    END
    
    /******************************
    *引渡し後保険データを取得する
    *******************************/
    IF @FlgHokenHantei = 1
    BEGIN  
    
      INSERT INTO
           @retData
      SELECT
           br.kbn
          , br.hosyousyo_no
          , CASE WHEN sub.kbn IS NOT NULL
            THEN 1
            ELSE 0
            END hw_hkn
          , br.hanyou_date
          , br.kanri_date
      FROM
            jhs_sys.t_bukken_rireki br
            ,
             (SELECT
                   brsub.kbn
                 , brsub.hosyousyo_no
                 , MAX(brsub.nyuuryoku_no) AS max
              FROM
                   jhs_sys.t_bukken_rireki brsub
              WHERE
                   brsub.rireki_syubetu = '18'
               AND brsub.rireki_no <> 0
               AND brsub.torikesi = 0
              GROUP BY
                   brsub.kbn
                 , brsub.hosyousyo_no
             )
              sub
       WHERE
            br.kbn = sub.kbn
        AND br.hosyousyo_no = sub.hosyousyo_no
        AND br.nyuuryoku_no = sub.max
--        AND br.kbn = @kbn 
--        AND br.hosyousyo_no = @bangou
        AND br.rireki_syubetu = '18'
        AND br.rireki_no = 11
       END
--    END
  
  RETURN
  
END