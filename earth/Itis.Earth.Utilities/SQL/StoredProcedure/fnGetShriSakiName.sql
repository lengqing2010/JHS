
CREATE FUNCTION [jhs_sys].[fnGetShriSakiName] (@kbn CHAR(1),@bangou VARCHAR(10),@SyouhinCd VARCHAR(8))
RETURNS VARCHAR(100)
AS
BEGIN
  DECLARE @ShriSakiName VARCHAR(100)
  
  SET @ShriSakiName = NULL

  --調査会社マスタから支払先名を取得(VIEWを使用)
  SELECT
    @ShriSakiName = MT.seikyuu_saki_shri_saki_mei 
  FROM
    jhs_sys.v_syouhin_shri_seikyuu_saki_jiban_tyskaisya VST 
    INNER JOIN jhs_sys.m_tyousakaisya MT 
      ON VST.siire_saki_tys_kaisya_cd = MT.tys_kaisya_cd 
      AND VST.shri_jigyousyo_cd = MT.jigyousyo_cd 
  WHERE
    VST.kbn = @kbn
    AND VST.hosyousyo_no = @bangou
    AND VST.syouhin_cd = @SyouhinCd

  RETURN @ShriSakiName
  
END

