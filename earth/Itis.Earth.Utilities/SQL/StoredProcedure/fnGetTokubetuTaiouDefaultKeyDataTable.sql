
-- =============================================
-- Author:		TIS
-- Create date: 2011/11/16
-- Description:	加盟店,商品コード,調査方法NOをKEYにデフォルト特別対応(初期値=1)のKEY情報を取得する
-- =============================================
CREATE FUNCTION [jhs_sys].[fnGetTokubetuTaiouDefaultKeyDataTable] 
(
  @KameitenCd VARCHAR(5)  --[必須]加盟店コード
  ,@SyouhinCd VARCHAR(8)  --[必須]商品コード
  ,@TysHouhouNo INT       --[必須]調査方法NO
)
RETURNS @retKeyData TABLE
(
  [tokubetu_taiou_cd] INT --特別対応コード
  ,[aitesaki_syubetu] INT --相手先種別
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
      mtt.tokubetu_taiou_cd
      , sub.aitesaki_syubetu
    FROM
         m_tokubetu_taiou mtt
              LEFT OUTER JOIN
                  (SELECT
                        mtt.tokubetu_taiou_cd
                      ,
                        CASE
                             WHEN MIN(mkst.aitesaki_syubetu) = 10
                             THEN 0 --10を0に補完
                             ELSE MIN(mkst.aitesaki_syubetu)
                        END AS aitesaki_syubetu
                   FROM
                        m_tokubetu_taiou mtt
                             LEFT OUTER JOIN
                                 (SELECT
                                       CASE
                                            WHEN sub.aitesaki_syubetu = 0
                                            THEN 10 --0を10に補完
                                            ELSE sub.aitesaki_syubetu
                                       END AS aitesaki_syubetu
                                     , sub.tokubetu_taiou_cd
                                     , sub.kasan_syouhin_cd
                                  FROM
                                      (SELECT
                                            *
                                       FROM
                                            m_kamei_syouhin_tys_tokubetu_taiou mkst
                                       WHERE
                                            mkst.aitesaki_syubetu = 1
                                        AND mkst.aitesaki_cd = @pKameitenCd
                                        AND mkst.syouhin_cd = @SyouhinCd
                                        AND mkst.tys_houhou_no = @TysHouhouNo
                                        --AND mkst.syokiti = 1
                                        AND mkst.torikesi = 0
                                       UNION
                                            ALL
                                       SELECT
                                            *
                                       FROM
                                            m_kamei_syouhin_tys_tokubetu_taiou mkst
                                       WHERE
                                            mkst.aitesaki_syubetu = 5
                                        AND mkst.aitesaki_cd = @pEigyousyoCd
                                        AND mkst.syouhin_cd = @SyouhinCd
                                        AND mkst.tys_houhou_no = @TysHouhouNo
                                        --AND mkst.syokiti = 1
                                        AND mkst.torikesi = 0
                                       UNION
                                            ALL
                                       SELECT
                                            *
                                       FROM
                                            m_kamei_syouhin_tys_tokubetu_taiou mkst
                                       WHERE
                                            mkst.aitesaki_syubetu = 7
                                        AND mkst.aitesaki_cd = @pKeiretuCd
                                        AND mkst.syouhin_cd = @SyouhinCd
                                        AND mkst.tys_houhou_no = @TysHouhouNo
                                        --AND mkst.syokiti = 1
                                        AND mkst.torikesi = 0
                                       UNION
                                            ALL
                                       SELECT
                                            *
                                       FROM
                                            m_kamei_syouhin_tys_tokubetu_taiou mkst
                                       WHERE
                                            mkst.aitesaki_syubetu = 0
                                        AND mkst.aitesaki_cd = 'ALL'
                                        AND mkst.syouhin_cd = @SyouhinCd
                                        AND mkst.tys_houhou_no = @TysHouhouNo
                                        --AND mkst.syokiti = 1
                                        AND mkst.torikesi = 0
                                      )
                                       AS sub
                                 )
                                  AS mkst
                                ON mtt.tokubetu_taiou_cd = mkst.tokubetu_taiou_cd
                   GROUP BY
                        mtt.tokubetu_taiou_cd
                  )
                   AS sub
                ON mtt.tokubetu_taiou_cd = sub.tokubetu_taiou_cd

  END 

  RETURN
  
END


