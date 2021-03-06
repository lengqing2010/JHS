-- =========================================================================================
-- Description:	区分、番号をKEYに邸別請求データ.分類コードのMIN(売上年月日)を取得する
--              売上年月日(0:調査、1:工事)
-- =========================================================================================
CREATE FUNCTION [jhs_sys].[fnGetTeibetuMinUriDateDataTable] 
(
  @FlgKojHantei             INT           --[必須] 0=ベタ(調査売上),1=工事(工事売上)
  ,@SYORI_DATE               VARCHAR(10)   --処理日
)
RETURNS @retData TABLE
(
  [kbn]                   CHAR(1)             --区分
  ,[hosyousyo_no]         VARCHAR(10)         --番号
  ,[jissi_date]           DATETIME            --MIN(売上年月日)
)
AS
BEGIN
  DECLARE @bunrui100  VARCHAR(3)
  DECLARE @bunrui110  VARCHAR(3)
  DECLARE @bunrui115  VARCHAR(3)
  DECLARE @bunrui120  VARCHAR(3)
  DECLARE @bunrui130  VARCHAR(3)
  SET @bunrui100 = '100'
  SET @bunrui110 = '110'
  SET @bunrui115 = '115'
  SET @bunrui120 = '120'
  SET @bunrui130 = '130'

  /***********************
  *工事判定フラグ=0の場合
  ***********************/
  IF @FlgKojHantei = 0
  BEGIN  
    /*****************************************************************************
    *邸別データ内の調査商品.保証有無=1のデータでMIN(売上年月日)のデータを取得する
    *-商品(調査:100,110,115,120)
    ******************************************************************************/
    INSERT INTO
         @retData
    SELECT
          TJ.kbn
        , TJ.hosyousyo_no
        , MIN(TS.uri_date) min_uri_date
     FROM
          jhs_sys.t_jiban TJ
               LEFT OUTER JOIN [jhs_sys].[t_teibetu_seikyuu] TS
                 ON TJ.kbn = TS.kbn
                AND TJ.hosyousyo_no = TS.hosyousyo_no
               LEFT OUTER JOIN [jhs_sys].[m_syouhin] MS
                 ON TS.syouhin_cd = MS.syouhin_cd
     WHERE
          MS.hosyou_umu = 1
      AND TS.bunrui_cd IN(@bunrui100, @bunrui110, @bunrui115, @bunrui120) --商品1～3
      AND (TJ.hosyou_kaisi_date IS NULL OR TJ.hosyou_kaisi_date > uri_date) 
     GROUP BY
          TJ.kbn
        , TJ.hosyousyo_no
        , TJ.hosyou_kaisi_date
     HAVING (CONVERT(VARCHAR, MIN(TS.uri_date), 111) <= @SYORI_DATE) --バッチ処理日以前
      AND (TJ.hosyou_kaisi_date IS NULL OR TJ.hosyou_kaisi_date > MIN(uri_date))
          
    END
    
  /***********************
  *工事判定フラグ=1の場合
  ***********************/
  IF @FlgKojHantei = 1
  BEGIN  
    /**********************************************************
    *邸別データ内の工事商品における売上年月日のデータを取得する
    *-商品(工事:130)
    ***********************************************************/
    INSERT INTO
         @retData
    SELECT
          TJ.kbn
        , TJ.hosyousyo_no
        , TS.uri_date min_uri_date
     FROM
          jhs_sys.t_jiban TJ
               LEFT OUTER JOIN [jhs_sys].[t_teibetu_seikyuu] TS
                 ON TJ.kbn = TS.kbn
                AND TJ.hosyousyo_no = TS.hosyousyo_no
     WHERE
          TS.bunrui_cd IN(@bunrui130) --工事商品
      AND (CONVERT(VARCHAR, TS.uri_date, 111) <= @SYORI_DATE) --バッチ処理日以前
      AND (TJ.hosyou_kaisi_date IS NULL OR TJ.hosyou_kaisi_date > TS.uri_date)
      
    END
    
  RETURN
  
END
