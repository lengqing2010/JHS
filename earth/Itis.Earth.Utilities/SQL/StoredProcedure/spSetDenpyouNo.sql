
-- =============================================
-- Description:	伝票番号採番処理(売上、仕入、入金、支払データテーブル)
-- =============================================
CREATE PROCEDURE [jhs_sys].[spSetDenpyouNo]
	@RETURN_VALUE VARCHAR(100) OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @UPD_USER_ID             VARCHAR(30) --更新ログインユーザーID
	
	--初期化
  SET @UPD_USER_ID = 'denpyou_no_saiban'
  
  -- Insert statements for procedure here

  --------売上データテーブル---------
  UPDATE [jhs_sys].[t_uriage_data] 
  SET
    denpyou_no = 
    RIGHT ( 
      '00000' + CAST ( 
        ( 
          SELECT
            COALESCE(MAX(CAST (denpyou_no AS INT)), 0) + 1 
          FROM
            [jhs_sys].[t_uriage_data] T1 
          WHERE
            T1.denpyou_uri_date = [jhs_sys].[t_uriage_data].denpyou_uri_date
        ) + ( 
          SELECT
            COUNT(*) 
          FROM
            [jhs_sys].[t_uriage_data] TT 
          WHERE
            TT.denpyou_unique_no < [jhs_sys].[t_uriage_data].denpyou_unique_no 
            AND TT.denpyou_uri_date = [jhs_sys].[t_uriage_data].denpyou_uri_date
            AND (TT.denpyou_no IS NULL OR TT.denpyou_no = '')
        ) AS varchar
      ) 
      , 5
    ) 
    , upd_login_user_id = @UPD_USER_ID
    , upd_datetime = GETDATE()
  WHERE
    (denpyou_no IS NULL OR denpyou_no = '')
    AND denpyou_uri_date IS NOT NULL

  SET @RETURN_VALUE = CAST(@@ROWCOUNT AS VARCHAR(100)) + ','

  --------仕入データテーブル---------
  UPDATE [jhs_sys].[t_siire_data] 
  SET
    denpyou_no = 
    RIGHT ( 
      '00000' + CAST ( 
        ( 
          SELECT
            COALESCE(MAX(CAST (denpyou_no AS INT)), 0) + 1 
          FROM
            [jhs_sys].[t_siire_data] T1 
          WHERE
            T1.denpyou_siire_date = [jhs_sys].[t_siire_data].denpyou_siire_date
        ) + ( 
          SELECT
            COUNT(*) 
          FROM
            [jhs_sys].[t_siire_data] TT 
          WHERE
            TT.denpyou_unique_no < [jhs_sys].[t_siire_data].denpyou_unique_no 
            AND TT.denpyou_siire_date = [jhs_sys].[t_siire_data].denpyou_siire_date
            AND (TT.denpyou_no IS NULL OR TT.denpyou_no = '')
        ) AS varchar
      ) 
      , 5
    ) 
    , upd_login_user_id = @UPD_USER_ID
    , upd_datetime = GETDATE()
  WHERE
    (denpyou_no IS NULL OR denpyou_no = '')
    AND denpyou_siire_date IS NOT NULL

  SET @RETURN_VALUE = @RETURN_VALUE + CAST(@@ROWCOUNT AS VARCHAR(100)) + ','

  --------入金データテーブル---------
  UPDATE [jhs_sys].[t_nyuukin_data] 
  SET
    denpyou_no = 
    RIGHT ( 
      '00000' + CAST ( 
        ( 
          SELECT
            COALESCE(MAX(CAST (denpyou_no AS INT)), 0) + 1 
          FROM
            [jhs_sys].[t_nyuukin_data] T1 
          WHERE
            T1.nyuukin_date = [jhs_sys].[t_nyuukin_data].nyuukin_date
        ) + ( 
          SELECT
            COUNT(*) 
          FROM
            [jhs_sys].[t_nyuukin_data] TT 
          WHERE
            TT.denpyou_unique_no < [jhs_sys].[t_nyuukin_data].denpyou_unique_no 
            AND TT.nyuukin_date = [jhs_sys].[t_nyuukin_data].nyuukin_date
            AND (TT.denpyou_no IS NULL OR TT.denpyou_no = '')
        ) AS varchar
      ) 
      , 5
    ) 
    , upd_login_user_id = @UPD_USER_ID
    , upd_datetime = GETDATE()
  WHERE
    (denpyou_no IS NULL OR denpyou_no = '')
    AND nyuukin_date IS NOT NULL

  SET @RETURN_VALUE = @RETURN_VALUE + CAST(@@ROWCOUNT AS VARCHAR(100)) + ','

  --------支払データテーブル---------
  UPDATE [jhs_sys].[t_siharai_data] 
  SET
    denpyou_no = 
    RIGHT ( 
      '00000' + CAST ( 
        ( 
          SELECT
            COALESCE(MAX(CAST (denpyou_no AS INT)), 0) + 1 
          FROM
            [jhs_sys].[t_siharai_data] T1 
          WHERE
            T1.siharai_date = [jhs_sys].[t_siharai_data].siharai_date
        ) + ( 
          SELECT
            COUNT(*) 
          FROM
            [jhs_sys].[t_siharai_data] TT 
          WHERE
            TT.denpyou_unique_no < [jhs_sys].[t_siharai_data].denpyou_unique_no 
            AND TT.siharai_date = [jhs_sys].[t_siharai_data].siharai_date
            AND (TT.denpyou_no IS NULL OR TT.denpyou_no = '')
        ) AS varchar
      ) 
      , 5
    ) 
    , upd_login_user_id = @UPD_USER_ID
    , upd_datetime = GETDATE()
  WHERE
    (denpyou_no IS NULL OR denpyou_no = '')
    AND siharai_date IS NOT NULL

  SET @RETURN_VALUE = @RETURN_VALUE + CAST(@@ROWCOUNT AS VARCHAR(100))

END

