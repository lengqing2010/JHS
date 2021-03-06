-- ==========================================================================
-- Description: <請求先コード・請求年月日から履歴Tをベースに、直近の請求(予定)日を取得する>
-- ==========================================================================
CREATE FUNCTION [jhs_sys].[fnGetSeikyuuSimeDate] (@seikyuuSakiCd VARCHAR(10),
                                             @seikyuusakiBrc VARCHAR(10),
                                             @seikyuuSakiKbn CHAR(1),
                                             @today DATETIME)
RETURNS VARCHAR(9)
AS
BEGIN
    DECLARE @SeikyuuSimeDate DATETIME
    DECLARE @simeBi INT
    DECLARE @getuMatuDate DATETIME
    DECLARE @yokuGetuDate DATETIME
    DECLARE @ZenTaisyouFlg INT
    DECLARE @ZERO VARCHAR

    SET @SeikyuuSimeDate = NULL
    SET @getuMatuDate = NULL
    SET @yokuGetuDate = NULL
    SET @today = CONVERT(VARCHAR, @today, 111)
    SET @ZenTaisyouFlg = NULL
    SET @ZERO = '0'
    
	    
	--履歴TよりMIN日の取得
	SELECT
	     @SeikyuuSimeDate = sub.min_sime_date
	   , @ZenTaisyouFlg = GF.zen_taisyou_flg
	FROM
	    (SELECT
	          DR.seikyuu_saki_cd
	        , DR.seikyuu_saki_brc
	        , DR.seikyuu_saki_kbn
	        , DR.seikyuusyo_hak_nengetu
	        , min(
	               CASE
	                    WHEN Nullif(DR.seikyuu_sime_date, '') IS NOT NULL
	                    THEN dateadd(day, CONVERT(int, DR.seikyuu_sime_date) -1, DR.seikyuusyo_hak_nengetu)
	                    ELSE null
	               END) AS min_sime_date
	     FROM
	         (SELECT
	               *
	          FROM
	               t_seikyuusyo_sime_date_rireki
	          WHERE
	               dateadd(day, CONVERT(int, seikyuu_sime_date) -1, seikyuusyo_hak_nengetu) >= @today
	           AND torikesi = 0
	         )
	          DR
	     GROUP BY
	          DR.seikyuu_saki_cd
	        , DR.seikyuu_saki_brc
	        , DR.seikyuu_saki_kbn
	        , DR.seikyuusyo_hak_nengetu
	     HAVING
	          1=1
	      AND DR.seikyuu_saki_cd = @seikyuuSakiCd
	      AND DR.seikyuu_saki_brc = @seikyuusakiBrc
	      AND DR.seikyuu_saki_kbn = @seikyuuSakiKbn
	      AND YEAR(DR.seikyuusyo_hak_nengetu) = YEAR(@today)
	      AND MONTH(DR.seikyuusyo_hak_nengetu) = MONTH(@today)
	    )
	     AS sub
	          INNER JOIN t_seikyuusyo_sime_date_rireki GF
	            ON sub.seikyuu_saki_cd = GF.seikyuu_saki_cd
	           AND sub.seikyuu_saki_brc = GF.seikyuu_saki_brc
	           AND sub.seikyuu_saki_kbn = GF.seikyuu_saki_kbn
	           AND sub.seikyuusyo_hak_nengetu = GF.seikyuusyo_hak_nengetu
	           AND DAY(sub.min_sime_date) = GF.seikyuu_sime_date


    --ここまでの処理で請求年月日が取得できた場合は履歴Tからの日付を戻す
    IF @SeikyuuSimeDate IS NOT NULL
    RETURN CONVERT(VARCHAR, @SeikyuuSimeDate, 112) + CONVERT(VARCHAR, @ZenTaisyouFlg) 

	--履歴Tの対象日＋1ヶ月先よりMIN日の取得
	SELECT
	     @SeikyuuSimeDate = sub.min_sime_date
	   , @ZenTaisyouFlg = GF.zen_taisyou_flg
	FROM
	    (SELECT
	          DR.seikyuu_saki_cd
	        , DR.seikyuu_saki_brc
	        , DR.seikyuu_saki_kbn
	        , DR.seikyuusyo_hak_nengetu
	        , min(
	               CASE
	                    WHEN Nullif(DR.seikyuu_sime_date, '') IS NOT NULL
	                    THEN dateadd(day, CONVERT(int, DR.seikyuu_sime_date) -1, DR.seikyuusyo_hak_nengetu)
	                    ELSE null
	               END) AS min_sime_date
	     FROM
	         (SELECT
	               *
	          FROM
	               t_seikyuusyo_sime_date_rireki
	          WHERE
	               dateadd(day, CONVERT(int, seikyuu_sime_date) -1, seikyuusyo_hak_nengetu) >= @today
	           AND torikesi = 0
	         )
	          DR
	     GROUP BY
	          DR.seikyuu_saki_cd
	        , DR.seikyuu_saki_brc
	        , DR.seikyuu_saki_kbn
	        , DR.seikyuusyo_hak_nengetu
	     HAVING
	          1=1
	      AND DR.seikyuu_saki_cd = @seikyuuSakiCd
	      AND DR.seikyuu_saki_brc = @seikyuusakiBrc
	      AND DR.seikyuu_saki_kbn = @seikyuuSakiKbn
	      AND YEAR(DR.seikyuusyo_hak_nengetu) = YEAR(DATEADD(m,1,@today))
	      AND MONTH(DR.seikyuusyo_hak_nengetu) = MONTH(DATEADD(m,1,@today))
	    )
	     AS sub
	          INNER JOIN t_seikyuusyo_sime_date_rireki GF
	            ON sub.seikyuu_saki_cd = GF.seikyuu_saki_cd
	           AND sub.seikyuu_saki_brc = GF.seikyuu_saki_brc
	           AND sub.seikyuu_saki_kbn = GF.seikyuu_saki_kbn
	           AND sub.seikyuusyo_hak_nengetu = GF.seikyuusyo_hak_nengetu
	           AND DAY(sub.min_sime_date) = GF.seikyuu_sime_date


    --ここまでの処理で請求年月日が取得できた場合は履歴T+1ヶ月からの日付を戻す
    IF @SeikyuuSimeDate IS NOT NULL
    RETURN CONVERT(VARCHAR, @SeikyuuSimeDate, 112) + CONVERT(VARCHAR, @ZenTaisyouFlg) 
	
	--請求先Mからの締め日取得
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
	RETURN CONVERT(VARCHAR, @SeikyuuSimeDate, 112) + @ZERO
  
END
