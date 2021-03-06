-- ==========================================================================
-- Description: <請求先コード、(当日)日付をベースに、直近の請求(予定)日を取得する>
-- ==========================================================================
CREATE FUNCTION [jhs_sys].[fnGetSeikyuuDate] (@seikyuuSakiCd VARCHAR(10),
                                             @seikyuusakiBrc VARCHAR(10),
                                             @seikyuuSakiKbn CHAR(1),
                                             @today DATETIME)
RETURNS DATETIME
AS
BEGIN
    DECLARE @SeikyuuSimeDate DATETIME
    DECLARE @simeBi INT
    DECLARE @getuMatuDate DATETIME
    DECLARE @yokuGetuDate DATETIME

    SET @SeikyuuSimeDate = NULL
    SET @getuMatuDate = NULL
    SET @yokuGetuDate = NULL
    SET @today = CONVERT(VARCHAR, @today, 111)

    SELECT
         @simeBi =(
              CASE
                   WHEN ISNUMERIC(seikyuu_sime_date) = 1
                   THEN seikyuu_sime_date
                   ELSE 99
              END)
    FROM
        [jhs_sys].[m_seikyuu_saki]
    WHERE
        seikyuu_saki_cd = @seikyuuSakiCd 
        AND seikyuu_saki_brc = @seikyuuSakiBrc 
        AND seikyuu_saki_kbn = @seikyuuSakiKbn

    --当日の月の末日を取得
    SELECT
        @getuMatuDate = jhs_sys.fnGetLastDay(@today)

    --締め日が当月末日をより大きい場合、当月末日を使用
    IF @simeBi > DATENAME(DAY, @getuMatuDate)
        SET @SeikyuuSimeDate = @getuMatuDate
    ELSE
        SET @SeikyuuSimeDate = DATENAME(YEAR, @today) + '/' + 
                               DATENAME(MONTH, @today) + '/' + 
                               CONVERT(varchar,@simeBi)

    --セットされた締め日が当日より前の場合、翌月の締め日をセットする
    IF @SeikyuuSimeDate < @today
    BEGIN
        SET @yokuGetuDate = DATEADD(MONTH, + 1, @SeikyuuSimeDate)
        --当月締め日+1ヶ月した日付が、締め日より小さい場合、月末日をセットする
        IF @simeBi > DATENAME(DAY, @yokuGetuDate)
            BEGIN
                SELECT
                    @getuMatuDate = jhs_sys.fnGetLastDay(@yokuGetuDate)
                --取得した翌月末日が締め日より小さい場合、翌月末日をセットする
                IF @simeBi > DATENAME(DAY, @getuMatuDate)
                    SET @SeikyuuSimeDate = @getuMatuDate
                ELSE
                    SET @SeikyuuSimeDate = @yokuGetuDate
            END
        ELSE
            SET @SeikyuuSimeDate = DATENAME(YEAR, @yokuGetuDate) + '/' + 
                                   DATENAME(MONTH, @yokuGetuDate) + '/' + 
                                   CONVERT(varchar,@simeBi)
    END

    --ここまでの処理で請求日が取得できなかった場合、当月末日を請求日としてセット
    IF @SeikyuuSimeDate IS NULL 
        SET @SeikyuuSimeDate = jhs_sys.fnGetLastDay(@today)

    RETURN @SeikyuuSimeDate
  
END
