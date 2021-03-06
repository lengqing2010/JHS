-- =========================================================
-- Description:	保証書管理の状況を判定し、値を返却する
--      Return: 0:引渡パラメータ不正、1:完了データ、2:未完了データ
-- =========================================================
CREATE FUNCTION [jhs_sys].[fnGetHosyousyoJyky] (@kbn CHAR(1),@bangou VARCHAR(10))
RETURNS INT
AS
BEGIN
  DECLARE @BukkenJyky INT
  
  SET @BukkenJyky = 0

  IF (@kbn IS NOT NULL AND @bangou IS NOT NULL)
  BEGIN
    SELECT
      @BukkenJyky = (
          CASE
               WHEN hk.kbn IS NOT NULL
               THEN 1
               ELSE 0
          END
     )
     FROM
          jhs_sys.t_hosyousyo_kanri hk
     WHERE
          hk.kbn = @kbn
      AND hk.hosyousyo_no = @bangou
      AND (
          hk.kaiseki_kanry = 1
          AND (
               ( hk.koj_kanry = 1)
               OR (hk.koj_umu = 0 AND hk.koj_kanry = 0)
          )
          AND 
            (
              (
               (hk.nyuukin_kakunin_jyouken = 2 AND hk.nyuukin_jyky = 3)
               OR (hk.nyuukin_kakunin_jyouken = 6 AND hk.nyuukin_jyky = 0)
               OR (hk.nyuukin_kakunin_jyouken <> 2 AND hk.nyuukin_kakunin_jyouken <> 6 AND hk.nyuukin_jyky = 1)
               OR (hk.nyuukin_kakunin_jyouken IS NULL AND hk.nyuukin_jyky = 1)
              )
              OR hk.nyuukin_jyky = 0
              OR hk.nyuukin_jyky = 7
            )  
--          AND (hk.kasi = 0 OR hk.kasi = 1)
      )

  END 

  RETURN @BukkenJyky
END




