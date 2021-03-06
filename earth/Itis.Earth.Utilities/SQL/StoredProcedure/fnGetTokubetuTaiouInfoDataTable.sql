-- =============================================
-- Author:		TIS
-- Create date: 2011/11/15
-- Description:	加盟店,商品コード,調査方法NO,区分,番号をKEYに、特別対応情報を取得する
-- =============================================
CREATE FUNCTION [jhs_sys].[fnGetTokubetuTaiouInfoDataTable] 
(
  @KameitenCd VARCHAR(5)  --[必須]加盟店コード
  ,@SyouhinCd VARCHAR(8)  --[必須]商品コード
  ,@TysHouhouNo INT       --[必須]調査方法NO
  ,@kbn CHAR(1)           --[必須]区分
  ,@bangou VARCHAR(10)    --[必須]番号
)
RETURNS @retKeyData TABLE
(
  [m_tokubetu_taiou_cd] INT  --特別対応コード
  ,[kbn] CHAR(1)           --区分
  ,[hosyousyo_no] VARCHAR(10)    --番号
  ,[torikesi] INT  --取消
  ,[bunrui_cd] INT  --分類コード
  ,[gamen_hyouji_no] INT  --画面表示NO
  ,[m_kasan_syouhin_cd] VARCHAR(8)  --金額加算商品コード
  ,[m_uri_kasan_gaku] INT --実請求加算金額
  ,[m_koumuten_kasan_gaku] INT  --工務店請求加算金額
  ,[m_aitesaki_syubetu] INT --相手先種別
  ,[m_aitesaki_cd] VARCHAR(5) --相手先コード 
  ,[m_syokiti] INT --初期値
  ,[kasan_syouhin_cd] VARCHAR(8)  --金額加算商品コード
  ,[uri_kasan_gaku] INT --実請求加算金額
  ,[koumuten_kasan_gaku] INT  --工務店請求加算金額
  ,[upd_datetime] datetime  --更新日時
  ,[tokubetu_taiou_meisyou] VARCHAR(40)    --特別対応名称
  ,[syouhin_mei] VARCHAR(40)    --商品名
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
         mtt.tokubetu_taiou_cd AS m_tokubetu_taiou_cd
         , ttt.kbn
         , ttt.hosyousyo_no
         , ISNULL(ttt.torikesi,0) AS torikesi
         , ttt.bunrui_cd        
         , ttt.gamen_hyouji_no
         , mkst.kasan_syouhin_cd AS m_kasan_syouhin_cd
         , ISNULL(mkst.uri_kasan_gaku,0) AS m_uri_kasan_gaku 
         , ISNULL(mkst.koumuten_kasan_gaku,0) AS m_koumuten_kasan_gaku 
         , mkst.aitesaki_syubetu
         , mkst.aitesaki_cd
         , ISNULL(mkst.syokiti,0) --NULLの場合は0に補完
         , ttt.kasan_syouhin_cd AS kasan_syouhin_cd
         , ISNULL(ttt.uri_kasan_gaku,0) AS uri_kasan_gaku --NULLの場合は0に補完
         , ISNULL(ttt.koumuten_kasan_gaku,0) AS koumuten_kasan_gaku --NULLの場合は0に補完
         ,
         CASE
              WHEN ttt.tokubetu_taiou_cd IS NOT NULL
              THEN ISNULL(ttt.upd_datetime,ttt.add_datetime)
              ELSE NULL
         END AS upd_datetime
         , mtt.tokubetu_taiou_meisyou
         , ms.syouhin_mei
      FROM m_tokubetu_taiou mtt
          LEFT OUTER JOIN t_tokubetu_taiou ttt
            ON mtt.tokubetu_taiou_cd = ttt.tokubetu_taiou_cd
           AND ttt.kbn = @kbn
           AND ttt.hosyousyo_no = @bangou
           AND ttt.torikesi = 0 --取消されていない
          LEFT OUTER JOIN [jhs_sys].[fnGetTokubetuTaiouKeyDataTable](@pKameitenCd, @SyouhinCd, @TysHouhouNo) sub
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
          LEFT OUTER JOIN m_syouhin ms
           ON ms.syouhin_cd = 
            CASE
              WHEN ttt.kasan_syouhin_cd IS NOT NULL THEN ttt.kasan_syouhin_cd
              ELSE mkst.kasan_syouhin_cd
            END

  END 
  
  RETURN
  
END


