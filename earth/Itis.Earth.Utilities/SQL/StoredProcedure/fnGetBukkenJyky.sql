-- =========================================================
-- Description:	各種条件毎に物件状況をセットする値を返却する
-- =========================================================
CREATE FUNCTION [jhs_sys].[fnGetBukkenJyky] (@kbn CHAR(1),@bangou VARCHAR(10))
RETURNS INT
AS
BEGIN
  DECLARE @BukkenJyky     INT
  DECLARE @bunruiKaiyaku  VARCHAR(3)
  
  SET @BukkenJyky = 0
  SET @bunruiKaiyaku = '180'

  IF (@kbn IS NOT NULL AND @bangou IS NOT NULL)
  BEGIN
    SELECT
         @BukkenJyky = CASE
              WHEN tj.hosyousyo_hak_date IS NOT NULL
              THEN 3
              WHEN tj.data_haki_syubetu <> '0'
              THEN 0
              WHEN
                  (SELECT
                        ISNULL(hj.mihak_list_inji_umu,'0')
                   FROM
                        jhs_sys.m_hosyousyo_hak_jyky hj
                   WHERE
                        hj.hosyousyo_hak_jyky_no = tj.hosyousyo_hak_jyky
                  )
                   = '0'
              THEN 0
              WHEN EXISTS
                  (SELECT
                        ts.kbn
                   FROM
                        jhs_sys.t_teibetu_seikyuu ts
                   WHERE
                        ts.kbn = tj.kbn
                    AND ts.hosyousyo_no = tj.hosyousyo_no
                    AND ts.bunrui_cd = @bunruiKaiyaku
                    AND ts.gamen_hyouji_no = 1
                  )
              THEN 0
              WHEN (SELECT [jhs_sys].[fnGetHosyousyoJyky](@kbn,@bangou) ) = 1
              THEN 1
              ELSE 2
         END
    FROM
         [jhs_sys].[t_jiban] tj
    WHERE tj.kbn = @kbn
    AND   tj.hosyousyo_no = @bangou
  END 

  RETURN @BukkenJyky
END




