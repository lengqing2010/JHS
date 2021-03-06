-- ==========================================================================
-- Description: <請求先コード・請求年月日から履歴Tをベースに、直近の請求締め日を取得する
-- ==========================================================================
CREATE FUNCTION [jhs_sys].[fnGetSeikyuuSimeBi] (@seikyuuSakiCd VARCHAR(10),
                                             @seikyuusakiBrc VARCHAR(10),
                                             @seikyuuSakiKbn CHAR(1),
                                             @today DATETIME)
RETURNS VARCHAR(2)
AS
BEGIN
	DECLARE @SeikyuuSimeBi VARCHAR(2)
    
    SET @SeikyuuSimeBi = NULL
    SET @today = CONVERT(VARCHAR, @today, 111)

	SELECT
	     @SeikyuuSimeBi =(
	          CASE
	               WHEN ISNUMERIC(DR.seikyuu_sime_date) = 1
	               THEN DR.seikyuu_sime_date
	               ELSE NULL
	          END)
	FROM
	     t_seikyuusyo_sime_date_rireki DR
	WHERE
	     DR.seikyuu_saki_cd = @seikyuuSakiCd
	 AND DR.seikyuu_saki_brc = @seikyuuSakiBrc
	 AND DR.seikyuu_saki_kbn = @seikyuuSakiKbn
	 AND YEAR(DR.seikyuusyo_hak_nengetu) = YEAR(@today)
	 AND MONTH(DR.seikyuusyo_hak_nengetu) = MONTH(@today)
	 AND (
	          CASE
	               WHEN ISNUMERIC(DR.seikyuu_sime_date) = 1
	               THEN CONVERT(int, seikyuu_sime_date)
	               ELSE NULL
	          END) = DAY(@today)

               
	--ここまでの処理で請求日が取得できた場合は履歴Tからの日付を戻す
    IF @SeikyuuSimeBi IS NOT NULL
    RETURN @SeikyuuSimeBi
    

    SELECT
         @SeikyuuSimeBi =(
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

	RETURN @SeikyuuSimeBi
    
END