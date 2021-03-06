-- ==========================================================================
-- Author:      <T.Ezure>
-- Create date: <2010/03/11>
-- Description: <ログインユーザー名を取得する>
-- ==========================================================================
CREATE FUNCTION [jhs_sys].[fnGetAddUpdUserName] (@id VARCHAR(64))
RETURNS VARCHAR(128)
AS
BEGIN
  DECLARE @UserName VARCHAR(128)
  
  SET @UserName = NULL

  --社員アカウント情報マスタから表示名を取得
  SELECT
    @UserName = DisplayName
  FROM
	[jhs_sys].[m_jhs_mailbox]
  WHERE
	PrimaryWindowsNTAccount = @id

  --ログインユーザー名が取得できない場合、DBサーバのプロセス情報をセット
  IF @UserName IS NULL
    SELECT
      @UserName = SUBSTRING(REPLACE(ISNULL([hostname],'') +','+ 
                                    ISNULL([loginame],'') +','+ 
                                    ISNULL([program_name],''),' ','') , 1, 128) 
    FROM
      [master].[dbo].[sysprocesses] 
    WHERE
      [spid] = @@spid

  RETURN @UserName
  
END

