-- ===========================================================================
-- Description:	地盤データに関連する保証書管理データが存在しないテーブルを返す
-- ===========================================================================
CREATE FUNCTION [jhs_sys].[fnGetHosyousyoKanriKeyDataTable] 
(
  @FlgExists INT                  --[必須] 1=存在するデータ,0=存在しないデータ
)
RETURNS @retJibanKeyData TABLE
(
  [kbn] CHAR(1)                    --区分
  ,[hosyousyo_no] VARCHAR(100)     --番号
)
AS
BEGIN
  /*********************************************************
  *地盤データ内で保証書管理データが未存在のデータを取得する
  *********************************************************/
  INSERT INTO
       @retJibanKeyData
  SELECT
       TJ.kbn
     , TJ.hosyousyo_no
  FROM
       [jhs_sys].[t_jiban] TJ
            LEFT OUTER JOIN [jhs_sys].[t_hosyousyo_kanri] HK
              ON TJ.kbn = HK.kbn
             AND TJ.hosyousyo_no = HK.hosyousyo_no
  WHERE (HK.kbn IS NULL OR HK.hosyousyo_no IS NULL)
  
  RETURN
  
END
