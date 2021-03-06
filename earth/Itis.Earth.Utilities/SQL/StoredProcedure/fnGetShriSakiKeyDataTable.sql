
-- =============================================
-- Author:		Sorun
-- Create date: 2010/03/05
-- Description:	物件の区分/番号/商品コードを元に、
--              (支払先)調査会社コード/(支払先)調査会社事業所コード/(支払先)請求先支払先名を取得する
-- =============================================
CREATE FUNCTION [jhs_sys].[fnGetShriSakiKeyDataTable] 
(
  @kbn CHAR(1)            --区分
  ,@bangou VARCHAR(10)    --番号
  ,@SyouhinCd VARCHAR(8)  --商品コード
)
RETURNS @retShriSakiKeyData TABLE
(
  [shri_saki_cd] VARCHAR(100)
  ,[shri_saki_brc] VARCHAR(100)
  ,[shri_saki_name] VARCHAR(100)
)
AS
BEGIN
  DECLARE @ShriSakiCd VARCHAR(100)
  DECLARE @ShriSakiBrc VARCHAR(100)
  DECLARE @ShriSakiName VARCHAR(100)
  
  SET @ShriSakiCd = NULL
  SET @ShriSakiBrc = NULL
  SET @ShriSakiName = NULL

  --調査会社マスタから請求先支払先名を取得(VIEWを使用)
  SELECT
    @ShriSakiCd = MT.tys_kaisya_cd  
    ,@ShriSakiBrc = MT.jigyousyo_cd 
    ,@ShriSakiName = MT.seikyuu_saki_shri_saki_mei 
  FROM
    jhs_sys.v_syouhin_siire_seikyuu_saki_jiban_tyskaisya VST 
    INNER JOIN jhs_sys.m_tyousakaisya MT 
      ON VST.siire_saki_tys_kaisya_cd = MT.tys_kaisya_cd 
      AND VST.shri_jigyousyo_cd = MT.jigyousyo_cd 
  WHERE
    VST.kbn = @kbn
    AND VST.hosyousyo_no = @bangou
    AND VST.syouhin_cd = @SyouhinCd

  INSERT @retShriSakiKeyData
  SELECT @ShriSakiCd, @ShriSakiBrc, @ShriSakiName
	
	RETURN 

END

