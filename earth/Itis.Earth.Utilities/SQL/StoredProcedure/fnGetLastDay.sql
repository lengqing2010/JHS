

-- ==========================================================================
-- Author:      <S.Katane>
-- Create date: <2010/06/21>
-- Description: <月末日を取得する>
-- ==========================================================================
CREATE FUNCTION [jhs_sys].[fnGetLastDay] (@BaseDay DATETIME)
RETURNS DATETIME
AS
BEGIN
  DECLARE @LastDay DATETIME
  
  SET @LastDay = NULL

  --基準日+1ヶ月-1日で月末日を取得
	SELECT @LastDay = 
	  DATEADD( 
		DAY
		, - 1
		, DATENAME(YEAR, DATEADD(MONTH, + 1, @BaseDay)) + 
		  '-' + DATENAME(MONTH, DATEADD(MONTH, + 1, @BaseDay)) 
		+ '-' + '01'
	  ) 

  RETURN @LastDay
  
END


