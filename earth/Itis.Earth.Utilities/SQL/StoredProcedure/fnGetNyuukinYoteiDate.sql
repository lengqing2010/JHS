-- =============================================
-- Author:		<TIS>
-- Create date: <2012/3/15>
-- Description:	<入金予定日を算出する>
-- =============================================
CREATE FUNCTION [jhs_sys].[fnGetNyuukinYoteiDate](@seikyuuSakiCd VARCHAR(10),
	                                             @seikyuusakiBrc VARCHAR(10),
		                                         @seikyuuSakiKbn CHAR(1),
												 @SeikyuusyoHakDate DATETIME)
RETURNS DATETIME
AS
BEGIN
  DECLARE @KaisyuuYoteiGessuu INT
  DECLARE @KaisyuuYoteiDate VARCHAR(2)
  DECLARE @LastDay DATETIME
  DECLARE @NyuukinYoteiDate DATETIME
  
  SET @KaisyuuYoteiGessuu = NULL
  SET @KaisyuuYoteiDate = NULL
  SET @LastDay = NULL  
  SET @NyuukinYoteiDate = NULL
  
  --請求書発行日
  IF @SeikyuusyoHakDate IS NOT NULL AND @SeikyuusyoHakDate <> ''
  BEGIN

	--請求先情報を取得
	SELECT @KaisyuuYoteiGessuu = ms.kaisyuu_yotei_gessuu
		   , @KaisyuuYoteiDate = ms.kaisyuu_yotei_date
	FROM
		[jhs_sys].[m_seikyuu_saki] ms
	WHERE
		ms.seikyuu_saki_cd = @seikyuuSakiCd
	AND ms.seikyuu_saki_brc = @seikyuusakiBrc
	AND ms.seikyuu_saki_kbn = @seikyuuSakiKbn

	IF @KaisyuuYoteiGessuu IS NOT NULL AND ISNUMERIC(@KaisyuuYoteiDate) = 1
		IF @KaisyuuYoteiDate <> '0' AND @KaisyuuYoteiDate <> '00' AND @KaisyuuYoteiDate <> '-'
		BEGIN
			--請求書発行日+回収予定月数を取得
			SELECT @NyuukinYoteiDate =
				DATEADD(MONTH
						, + @KaisyuuYoteiGessuu
						, @SeikyuusyoHakDate)

			--上記で取得した月の末日を取得
			SELECT @LastDay = [jhs_sys].[fnGetLastDay](@NyuukinYoteiDate)

			--上記で取得した月の末日を取得
			IF @KaisyuuYoteiDate > DAY(@LastDay)
				SET @NyuukinYoteiDate = @LastDay
			ELSE
				SET @NyuukinYoteiDate = CONVERT(DATETIME,
												CONVERT(VARCHAR, YEAR(@NyuukinYoteiDate)) +
													'/' + CONVERT(VARCHAR, MONTH(@NyuukinYoteiDate)) +
													'/' + CONVERT(VARCHAR,@KaisyuuYoteiDate),
												111)
		END
  END
	RETURN @NyuukinYoteiDate
END
