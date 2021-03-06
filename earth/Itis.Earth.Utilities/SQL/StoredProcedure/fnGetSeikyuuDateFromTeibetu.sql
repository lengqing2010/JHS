
-- ==========================================================================
-- Description: 邸別請求テーブルをベースに、直近の請求(予定)日を取得する
-- 引数：区分、番号、分類コード、画面表示NO、システム日付
-- ==========================================================================
CREATE FUNCTION [jhs_sys].[fnGetSeikyuuDateFromTeibetu] (@kbn CHAR(1),
                                                        @bangou VARCHAR(10),
                                                        @bunruiCd VARCHAR(3),
                                                        @gamenNo INT,
                                                        @today DATETIME)
RETURNS DATETIME
AS
BEGIN

    DECLARE @syouhinCd          VARCHAR(10) --商品コード
    DECLARE @kameitenCd         VARCHAR(10) --加盟店コード
    DECLARE @seikyuuSakiCd      VARCHAR(10) --請求先コード
    DECLARE @seikyuuSakiBrc     VARCHAR(10) --請求先枝番
    DECLARE @seikyuuSakiKbn     VARCHAR(1)  --請求先区分
    DECLARE @SeikyuuDate        DATETIME    --請求年月日
    DECLARE @tableType          INT         --テーブルタイプ

    SET @tableType = 1          --テーブルタイプ(邸別請求：１)
    SET @SeikyuuDate = NULL
    SET @today = CONVERT(VARCHAR, @today, 111)

    /************************************************
     * 請求先情報を取得 *
     ************************************************/
    --邸別請求、地盤テーブルの情報を取得
    SELECT
        @syouhinCd = ts.syouhin_cd
        , @kameitenCd = tj.kameiten_cd
        , @seikyuuSakiCd = seikyuu_saki_cd
        , @seikyuuSakiBrc = seikyuu_saki_brc
        , @seikyuuSakiKbn = seikyuu_saki_kbn 
    FROM
        jhs_sys.t_teibetu_seikyuu ts 
        INNER JOIN jhs_sys.t_jiban tj 
            ON ts.kbn = tj.kbn 
            AND ts.hosyousyo_no = tj.hosyousyo_no 
    WHERE
        ts.kbn = @kbn 
        AND ts.hosyousyo_no = @bangou 
        AND ts.bunrui_cd = @bunruiCd 
        AND ts.gamen_hyouji_no = @gamenNo

    IF @seikyuuSakiCd IS NULL OR @seikyuuSakiBrc IS NULL OR @seikyuuSakiKbn IS NULL
      --請求先コードが邸別請求に個別でセットされていない場合、
      --関数を使用してデフォルト請求先を取得
      SELECT 
        @seikyuuSakiCd = [seikyuu_saki_cd]
        , @seikyuuSakiBrc = [seikyuu_saki_brc]
        , @seikyuuSakiKbn = [seikyuu_saki_kbn]
      FROM
        [jhs_sys].[fnGetSeikyuuSakiKeyDataTable](@tableType, @kbn, @bangou, @bunruiCd, @syouhinCd, @kameitenCd)

    --請求先キー情報が取得できた場合、請求日付を取得する
    IF @seikyuuSakiCd IS NOT NULL AND @seikyuuSakiBrc IS NOT NULL AND @seikyuuSakiKbn IS NOT NULL
        SET @SeikyuuDate = [jhs_sys].[fnGetSeikyuuDate](@seikyuuSakiCd,@seikyuuSakiBrc,@seikyuuSakiKbn,@today)

    --値戻し(請求先キー情報が取得できなかった場合は、NULLで戻る)
    RETURN @SeikyuuDate
  
END
