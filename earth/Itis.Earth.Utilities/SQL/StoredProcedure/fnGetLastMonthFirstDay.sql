-- ==========================================================================
-- Author:      <Sorun>
-- Create date: <2010/12/17>
-- Description: <前月1日を取得する>
-- ==========================================================================
CREATE FUNCTION [jhs_sys].[fnGetLastMonthFirstDay] (@BaseDay DATETIME)
RETURNS DATETIME
AS
BEGIN
  DECLARE @LastMonthFirstDay DATETIME
  
  SET @LastMonthFirstDay = NULL

  --基準日-1ヶ月で前月を取得
  SELECT @LastMonthFirstDay =
	CAST(
		(DATENAME(YEAR, DATEADD(MONTH, -1, @BaseDay)) +
		'-' + DATENAME(MONTH, DATEADD(MONTH, -1, @BaseDay)) + 
		'-' + '01') AS datetime)

  RETURN @LastMonthFirstDay
  
END

