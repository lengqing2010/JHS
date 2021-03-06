
-- =============================================
-- Author:		TIS
-- Create date: 2011/11/16
-- Description:	加盟店,商品コード,調査方法NOをKEYにデフォルト特別対応(初期値=1)情報を取得する
-- =============================================
CREATE FUNCTION [jhs_sys].[fnGetTokubetuTaiouDefaultInfoDataTable] 
(
  @KameitenCd VARCHAR(5)  --[必須]加盟店コード
  ,@SyouhinCd VARCHAR(8)  --[必須]商品コード
  ,@TysHouhouNo INT       --[必須]調査方法NO
)
RETURNS @retKeyData TABLE
(
  [aitesaki_syubetu] INT
  ,[aitesaki_cd] VARCHAR(5)
  ,[syouhin_cd] VARCHAR(8)
  ,[tys_houhou_no] INT
  ,[tokubetu_taiou_cd] INT
  ,[kasan_syouhin_cd] VARCHAR(8)
  ,[syokiti] INT
  ,[uri_kasan_gaku] INT
  ,[koumuten_kasan_gaku] INT
)
AS
BEGIN
  DECLARE @pKameitenCd VARCHAR(5)
  DECLARE @pEigyousyoCd VARCHAR(5)
  DECLARE @pKeiretuCd VARCHAR(5)
  
  SET @pKameitenCd = NULL
  SET @pEigyousyoCd = NULL
  SET @pKeiretuCd = NULL

  --加盟店コードから営業所/系列コードを取得する
  SELECT
    @pKameitenCd = kameiten_cd
    ,@pEigyousyoCd = eigyousyo_cd
    ,@pKeiretuCd = keiretu_cd
  FROM
    jhs_sys.m_kameiten 
  WHERE
    kameiten_cd = @KameitenCd
           
  IF @pKameitenCd <> NULL OR @pKameitenCd <> ''
  BEGIN
    INSERT @retKeyData
    SELECT
      mkst.aitesaki_syubetu
      ,mkst.aitesaki_cd
      ,mkst.syouhin_cd
      ,mkst.tys_houhou_no
      ,mkst.tokubetu_taiou_cd
      ,mkst.kasan_syouhin_cd
      ,mkst.syokiti
      ,mkst.uri_kasan_gaku
      ,mkst.koumuten_kasan_gaku
    FROM
         m_tokubetu_taiou mtt
              LEFT OUTER JOIN [jhs_sys].[fnGetTokubetuTaiouDefaultKeyDataTable](@KameitenCd, @SyouhinCd, @TysHouhouNo) sub
                ON mtt.tokubetu_taiou_cd = sub.tokubetu_taiou_cd
              LEFT OUTER JOIN m_kamei_syouhin_tys_tokubetu_taiou mkst
                ON mkst.aitesaki_syubetu = sub.aitesaki_syubetu
               AND mkst.aitesaki_cd =
                   CASE
                        WHEN sub.aitesaki_syubetu = 1
                        THEN @pKameitenCd
                        WHEN sub.aitesaki_syubetu = 5
                        THEN @pEigyousyoCd
                        WHEN sub.aitesaki_syubetu = 7
                        THEN @pKeiretuCd
                        WHEN sub.aitesaki_syubetu = 0
                        THEN 'ALL'
                        ELSE ''
                   END
               AND mkst.syouhin_cd = @SyouhinCd
               AND mkst.tys_houhou_no = @TysHouhouNo
               AND mkst.tokubetu_taiou_cd = sub.tokubetu_taiou_cd
    WHERE
         mtt.torikesi = 0 --取消でない
     AND mkst.torikesi = 0 --取消でない
     AND mkst.syokiti = 1 --初期値=1

  END 
  
  RETURN
  
END


