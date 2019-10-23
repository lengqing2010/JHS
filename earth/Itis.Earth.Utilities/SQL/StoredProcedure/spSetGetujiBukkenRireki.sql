-- =====================================================							
-- Description:	物件履歴テーブル月次更新処理						
--				保証書管理テーブル月次更新処理（保険会社、保険申請月、保険申請区分）			
-- =====================================================							
CREATE PROCEDURE [jhs_sys].[spSetGetujiBukkenRireki]							
   @COUNT_HW_MAE_TORIKESI       INT         OUTPUT							
  ,@COUNT_HW_MAE_SINSEI         INT         OUTPUT							
  ,@COUNT_HW_ATO_TORIKESI       INT         OUTPUT							
  ,@COUNT_HW_ATO_SINSEI         INT         OUTPUT							
  ,@COUNT_HK_UPDATE             INT         OUTPUT							
  ,@COUNT_TT_TUUTI				INT			OUTPUT
  ,@COUNT_TT_TORIKESI			INT			OUTPUT	
  ,@ERR_NO                      INT         OUTPUT							
AS							
BEGIN							
	BEGIN TRANSACTION						

	SET NOCOUNT ON;	

	--変数の宣言	
	DECLARE @ADD_USER_ID             VARCHAR(30)  --追加ログインユーザーID	
	DECLARE @ADD_DATETIME            DATETIME     --追加日時	

	--初期化	
	SET @ADD_USER_ID = 'system'	
	SET @ADD_DATETIME = GETDATE()	

/*************************************************************************		
 * Ⅱ．お引渡し前保険／取消 [物件履歴テーブルの追加処理]		
 **************************************************************************/		
	INSERT INTO	
		 [jhs_sys].[t_bukken_rireki]
	(	
		 kbn
	   , hosyousyo_no	
	   , rireki_syubetu	
	   , rireki_no	
	   , nyuuryoku_no	
	   , naiyou	
	   , hanyou_date	
	   , hanyou_cd	
	   , kanri_date	
	   , kanri_cd	
	   , henkou_kahi_flg	
	   , torikesi	
	   , add_login_user_id	
	   , add_datetime	
	)	
	SELECT			
		 TJ.kbn		
	   , TJ.hosyousyo_no			
	   , '17' AS rireki_syubetu			
	   , '31' AS rireki_no			
	   , (ISNULL(BR.nyuuryoku_no, 0) + 1) AS nyuuryoku_no			
	   , ('取消設定日' + ISNULL(CONVERT(varchar, (			
			  CASE	
				   WHEN TJ.hosyousyo_hak_jyky_settei_date IS NOT NULL
				   THEN TJ.hosyousyo_hak_jyky_settei_date
				   ELSE TJ.data_haki_date
			  END), 111), '') + '。保険申請取消') AS naiyou	
	   , [jhs_sys].[fnGetLastMonthFirstDay](@ADD_DATETIME) AS hanyou_date			
	   , BRHAN.hanyou_cd AS hanyou_cd			
	   , CONVERT(varchar, (			
			  CASE	
				   WHEN TJ.hosyousyo_hak_jyky_settei_date IS NOT NULL
				   THEN TJ.hosyousyo_hak_jyky_settei_date
				   ELSE TJ.data_haki_date
			  END), 111) AS kanri_date	
	   , (RIGHT ('000000' + CONVERT(varchar, ISNULL(TJ.hantei_cd1, 0)), 6) + '_' + CONVERT(varchar, ISNULL(TJ.hantei_setuzoku_moji, 0)) +			
		 '_' + RIGHT ('000000' + CONVERT(varchar, ISNULL(TJ.hantei_cd2, 0)), 6) + '_' + 		
		RIGHT ('000' + CONVERT(varchar, ISNULL(TJ.hosyousyo_hak_jyky, 0)), 3)) AS kanri_cd		
	   , '1' AS henkou_kahi_flg			
	   , '0' AS torikesi			
	   , @ADD_USER_ID AS add_login_user_id			
	   , @ADD_DATETIME AS add_datetime			
	FROM			
		 [jhs_sys].fnGetHokenSinseiDataTable(2, @ADD_DATETIME) HK		
			  INNER JOIN [jhs_sys].[t_jiban] TJ	
				ON TJ.kbn = HK.kbn
			   AND TJ.hosyousyo_no = HK.hosyousyo_no	
			  LEFT OUTER JOIN			
				  (SELECT		
						kbn
					  , hosyousyo_no	
					  , max(nyuuryoku_no) AS nyuuryoku_no	
				   FROM		
						[jhs_sys].[t_bukken_rireki]
				   GROUP BY		
						kbn
					  , hosyousyo_no	
				  )		
				   BR		
				ON TJ.kbn = BR.kbn		
			   AND TJ.hosyousyo_no = BR.hosyousyo_no			
			  LEFT OUTER JOIN			
				  (SELECT		
						TBR.kbn	
					  , TBR.hosyousyo_no		
					  , max(TBR.nyuuryoku_no) AS nyuuryoku_no		
				   FROM			
					   (SELECT		
							 kbn
						   , hosyousyo_no	
						   , nyuuryoku_no	
						FROM	
							 [jhs_sys].[t_bukken_rireki]
						WHERE	
							 rireki_syubetu = '17'
						 AND (rireki_no = '11'	
						  OR rireki_no = '21')	
					   )		
						TBR	
				   GROUP BY		
						TBR.kbn
					  , TBR.hosyousyo_no	
				  )		
				   BRSIN		
				ON TJ.kbn = BRSIN.kbn		
			   AND TJ.hosyousyo_no = BRSIN.hosyousyo_no			
			  LEFT OUTER JOIN [jhs_sys].[t_bukken_rireki] BRHAN			
				ON BRSIN.kbn = BRHAN.kbn		
			   AND BRSIN.hosyousyo_no = BRHAN.hosyousyo_no			
			   AND BRSIN.nyuuryoku_no = BRHAN.nyuuryoku_no			
	WHERE					
		 ISNULL(TJ.hoken_kotei_flg, 0) NOT In(1, 2)				

  /*************************						
  * 処理件数、エラーNOセット（物件履歴追加・前保険／取消）						
  *************************/	
	SELECT
	  @ERR_NO = @@ERROR
	  ,@COUNT_HW_MAE_TORIKESI = @@ROWCOUNT

	--エラーチェック
	IF @ERR_NO <> 0
	BEGIN
	  -- トランザクションをロールバック（キャンセル）
	  ROLLBACK TRANSACTION
	  RETURN @ERR_NO
	END          

  /*************************	
  * 保証書管理テーブル更新（前保険／取消）	
  *************************/	
	UPDATE			
		 [jhs_sys].[t_hosyousyo_kanri]		
	SET			
		 hoken_kaisya = NULL		
	   , hoken_sinsei_tuki = NULL			
	   , hoken_sinsei_kbn = NULL			
	   , syori_flg = '2'			
       , syori_datetime = @ADD_DATETIME				
	   , upd_login_user_id = @ADD_USER_ID			
	   , upd_datetime = @ADD_DATETIME			
	FROM			
		 [jhs_sys].fnGetHokenSinseiDataTable(2,@ADD_DATETIME) HK		
			  INNER JOIN [jhs_sys].[t_hosyousyo_kanri] THK	
				ON HK.kbn = THK.kbn
			   AND HK.hosyousyo_no = THK.hosyousyo_no	

  /*************************	
  * 処理件数、エラーNOセット（保証書管理テーブル更新・前保険／取消）	
  *************************/	
	SELECT
	  @ERR_NO = @@ERROR
	  ,@COUNT_HK_UPDATE = @@ROWCOUNT

	--エラーチェック
	IF @ERR_NO <> 0
	BEGIN
	  -- トランザクションをロールバック（キャンセル）
	  ROLLBACK TRANSACTION
	  RETURN @ERR_NO
	END 

/*************************************************************************	
 * Ⅰ．お引渡し前保険／申請 [物件履歴テーブルの追加処理]		
 **************************************************************************/		
	INSERT INTO	
		 [jhs_sys].[t_bukken_rireki]
	(	
		 kbn
	   , hosyousyo_no	
	   , rireki_syubetu	
	   , rireki_no	
	   , nyuuryoku_no	
	   , naiyou	
	   , hanyou_date	
	   , hanyou_cd	
	   , kanri_date	
	   , kanri_cd	
	   , henkou_kahi_flg	
	   , torikesi			
	   , add_login_user_id			
	   , add_datetime			
	)			
	SELECT			
		 TJ.kbn		
	   , TJ.hosyousyo_no			
	   , '17' AS rireki_syubetu			
	   , (			
			  CASE	
				   WHEN ISNULL(TJ.koj_hantei_kekka_flg, 0) = '1'
				   THEN '21'
				   ELSE '11'
			  END) AS rireki_no	
	   , (ISNULL(BR.nyuuryoku_no, 0) + 1) AS nyuuryoku_no			
	   , (			
			  CASE			
				   WHEN ISNULL(TJ.koj_hantei_kekka_flg, 0) = '1'		
--				   THEN '工事売上日' 		
				   THEN '完工速報着日または工事報告書受理日' 		
--               			+ ISNULL(CONVERT(varchar, HK.hw_mae_hkn_tekiyou_yotei_jissi_date, 111), '') 			
--						+ '。①引渡しまで（又は3年間）'
				   ELSE '調査売上日' 		
--				   ELSE '調査売上日' 		
--               			+ ISNULL(CONVERT(varchar, HK.hw_mae_hkn_tekiyou_yotei_jissi_date, 111), '') 			
--						+ '。①引渡しまで（又は3年間）'
--			  END) AS naiyou			
			END
       			+ ISNULL(CONVERT(varchar, HK.hw_mae_hkn_tekiyou_yotei_jissi_date, 111), '') 			
			 + 'から3年間+引渡しから'	
			+Case When ISNULL(HK.hosyou_kikan,10)=20 AND ISNULL(TJ.hosyousyo_hak_date,'2013/2/1')>='2013/2/1' Then '15' Else '10' End+'年間')  AS naiyou
	   , [jhs_sys].[fnGetLastMonthFirstDay](@ADD_DATETIME) AS hanyou_date					
--	   , '017' AS hanyou_cd					
--	   , '021' AS hanyou_cd					
	   , Case When ISNULL(HK.hosyou_kikan,10)=20 AND ISNULL(TJ.hosyousyo_hak_date,'2013/2/1')>='2013/2/1' Then '022' Else '021' End AS hanyou_cd					
	   , HK.hw_mae_hkn_tekiyou_yotei_jissi_date AS kanri_date					
	   , (RIGHT ('000000' + CONVERT(varchar, ISNULL(TJ.hantei_cd1, 0)), 6) 					
   			+ '_' + CONVERT(varchar, ISNULL(TJ.hantei_setuzoku_moji, 0)) 			
			+ '_' + RIGHT ('000000' + CONVERT(varchar, ISNULL(TJ.hantei_cd2, 0)), 6) 			
			+ '_' + RIGHT ('000' + CONVERT(varchar, ISNULL(TJ.hosyousyo_hak_jyky, 0)), 3)) AS kanri_cd			
	   , '1' AS henkou_kahi_flg					
	   , '0' AS torikesi					
	   , @ADD_USER_ID AS add_login_user_id					
	   , @ADD_DATETIME AS add_datetime					
	FROM					
		 [jhs_sys].fnGetHokenSinseiDataTable(1,@ADD_DATETIME) HK				
			  INNER JOIN [jhs_sys].[t_jiban] TJ			
				ON TJ.kbn = HK.kbn		
			   AND TJ.hosyousyo_no = HK.hosyousyo_no			
			  LEFT OUTER JOIN			
				  (SELECT		
						kbn
					  , hosyousyo_no	
					  , max(nyuuryoku_no) AS nyuuryoku_no	
				   FROM		
						[jhs_sys].[t_bukken_rireki]
				   GROUP BY		
						kbn
					  , hosyousyo_no	
				  )		
				   BR		
				ON BR.kbn = TJ.kbn		
			   AND BR.hosyousyo_no = TJ.hosyousyo_no			
	WHERE					
		ISNULL(TJ.hoken_kotei_flg, 0) NOT In(1, 2)				


  /*************************						
  * 処理件数、エラーNOセット（物件履歴追加・前保険／申請）						
  *************************/	
	SELECT
	  @ERR_NO = @@ERROR
	  ,@COUNT_HW_MAE_SINSEI = @@ROWCOUNT

	--エラーチェック
	IF @ERR_NO <> 0
	BEGIN
	  -- トランザクションをロールバック（キャンセル）
	  ROLLBACK TRANSACTION
	  RETURN @ERR_NO
	END

  /*************************	
  * 保証書管理テーブル更新（前保険／申請）	
  *************************/	
	UPDATE			
		 [jhs_sys].[t_hosyousyo_kanri]		
	SET			
		 hoken_kaisya = '2'		
	   , hoken_sinsei_tuki = [jhs_sys].[fnGetLastMonthFirstDay](@ADD_DATETIME)			
--	   , hoken_sinsei_kbn = '17'			
--	   , hoken_sinsei_kbn = '21'			
	   , hoken_sinsei_kbn = Case When ISNULL(HK.hosyou_kikan,10)=20 AND ISNULL(TJ.hosyousyo_hak_date,'2013/2/1')>='2013/2/1' Then 22 Else 21 End
	   , syori_flg = '2'			
       , syori_datetime = @ADD_DATETIME				
	   , upd_login_user_id = @ADD_USER_ID			
	   , upd_datetime = @ADD_DATETIME			
	FROM			
		 [jhs_sys].fnGetHokenSinseiDataTable(1,@ADD_DATETIME) HK		
			  INNER JOIN [jhs_sys].[t_hosyousyo_kanri] THK	
				ON HK.kbn = THK.kbn
			   AND HK.hosyousyo_no = THK.hosyousyo_no	
			  INNER JOIN [jhs_sys].[t_jiban] TJ	
				ON HK.kbn = TJ.kbn
			   AND HK.hosyousyo_no = TJ.hosyousyo_no	

  /*************************	
  * 処理件数、エラーNOセット（地盤テーブル更新・前保険／申請）	
  *************************/	
	SELECT
	  @ERR_NO = @@ERROR
	  ,@COUNT_HK_UPDATE = @@ROWCOUNT

	--エラーチェック
	IF @ERR_NO <> 0
	BEGIN
	  -- トランザクションをロールバック（キャンセル）
	  ROLLBACK TRANSACTION
	  RETURN @ERR_NO
	END 


/*************************************************************************		
 * Ⅳ．お引渡し後保険／取消 [物件履歴テーブルの追加処理]		
 **************************************************************************/		
	INSERT INTO	
		 [jhs_sys].[t_bukken_rireki]
	(	
		 kbn
	   , hosyousyo_no	
	   , rireki_syubetu	
	   , rireki_no	
	   , nyuuryoku_no	
	   , naiyou	
	   , hanyou_date	
	   , hanyou_cd	
--	   , kanri_date	
	   , henkou_kahi_flg			
	   , torikesi			
	   , add_login_user_id			
	   , add_datetime			
	)			
	SELECT			
		 TJ.kbn		
	   , TJ.hosyousyo_no			
	   , '18' AS rireki_syubetu			
	   , '31' AS rireki_no			
	   , (ISNULL(BR.nyuuryoku_no, 0) + 1) AS nyuuryoku_no			
--	   , (			
--		  CASE		
--			   WHEN HK.hw_ato_hkn_torikesi_syubetsu IS NOT NULL	
--			   THEN (	
--				 CASE
--					  WHEN HK.hw_ato_hkn_torikesi_syubetsu = '1'		
--					  THEN '引渡変更日' + 		
--						ISNULL(CONVERT(varchar, TJ.hosyousyo_saihak_date, 111), '') + 	
--						'。引渡し変更による保険申請取消'	
--					  WHEN HK.hw_ato_hkn_torikesi_syubetsu = '0'		
--					  THEN '取消設定日' + ISNULL(CONVERT(varchar, (		
--						CASE	
--							 WHEN TJ.hosyousyo_hak_jyky_settei_date IS NOT NULL
--							 THEN TJ.hosyousyo_hak_jyky_settei_date
--							 ELSE TJ.data_haki_date
--						END), 111), '') + '。保険申請取消'	
--				 END)			
--			   ELSE NULL				
--		  END) AS naiyou					
	   , '' AS naiyou						
	   , [jhs_sys].[fnGetLastMonthFirstDay](@ADD_DATETIME) AS hanyou_date						
	   , BRHAN.hanyou_cd AS hanyou_cd												
--	   , (												
--		  CASE											
--			   WHEN HK.hw_ato_hkn_torikesi_syubetsu IS NOT NULL										
--			   THEN (										
--				 CASE									
--					  WHEN HK.hw_ato_hkn_torikesi_syubetsu = '1'								
--					  THEN CONVERT(varchar, TJ.hosyousyo_saihak_date, 111)								
--					  WHEN HK.hw_ato_hkn_torikesi_syubetsu = '0'								
--					  THEN CONVERT(varchar, (								
--						CASE							
--							 WHEN TJ.hosyousyo_hak_jyky_settei_date IS NOT NULL						
--							 THEN TJ.hosyousyo_hak_jyky_settei_date						
--							 ELSE TJ.data_haki_date						
--						END), 111)							
--				 END)									
--			   ELSE NULL										
--		  END) AS kanri_date											
	   , '1' AS henkou_kahi_flg												
	   , '0' AS torikesi												
	   , @ADD_USER_ID AS add_login_user_id												
	   , @ADD_DATETIME AS add_datetime												
	FROM												
		 [jhs_sys].fnGetHokenSinseiDataTable(4, @ADD_DATETIME) HK											
			  INNER JOIN [jhs_sys].[t_jiban] TJ										
				ON TJ.kbn = HK.kbn									
			   AND TJ.hosyousyo_no = HK.hosyousyo_no										
			  LEFT OUTER JOIN										
				  (SELECT									
						kbn							
					  , hosyousyo_no								
					  , max(nyuuryoku_no) AS nyuuryoku_no								
				   FROM		
						[jhs_sys].[t_bukken_rireki]
				   GROUP BY		
						kbn
					  , hosyousyo_no	
				  )		
				   BR		
				ON TJ.kbn = BR.kbn		
			   AND TJ.hosyousyo_no = BR.hosyousyo_no			
			  LEFT OUTER JOIN			
				  (SELECT		
						TBR.kbn
					  , TBR.hosyousyo_no	
					  , max(TBR.nyuuryoku_no) AS nyuuryoku_no	
				   FROM		
					   (SELECT	
							 kbn
						   , hosyousyo_no	
						   , nyuuryoku_no	
						FROM	
							 [jhs_sys].[t_bukken_rireki]
						WHERE	
							 rireki_syubetu = '18'
						 AND rireki_no = '11'	
					   )		
						TBR	
				   GROUP BY			
						TBR.kbn	
					  , TBR.hosyousyo_no		
				  )			
				   BRSIN			
				ON TJ.kbn = BRSIN.kbn			
			   AND TJ.hosyousyo_no = BRSIN.hosyousyo_no	
			  LEFT OUTER JOIN [jhs_sys].[t_bukken_rireki] BRHAN	
				ON BRSIN.kbn = BRHAN.kbn
			   AND BRSIN.hosyousyo_no = BRHAN.hosyousyo_no	
			   AND BRSIN.nyuuryoku_no = BRHAN.nyuuryoku_no	
	WHERE			
		 ISNULL(TJ.hoken_kotei_flg, 0) NOT In(2)		

  /*************************				
  * 処理件数、エラーNOセット（物件履歴追加・後保険／申請）				
  *************************/				
	SELECT			
	  @ERR_NO = @@ERROR			
	  ,@COUNT_HW_ATO_TORIKESI = @@ROWCOUNT			

	--エラーチェック			
	IF @ERR_NO <> 0	
	BEGIN	
	  -- トランザクションをロールバック（キャンセル）	
	  ROLLBACK TRANSACTION	
	  RETURN @ERR_NO	
	END	



/*************************************************************************		
 * Ⅲ．お引渡し後保険／申請 [物件履歴テーブルの追加処理]		
 **************************************************************************/		
	INSERT INTO	
		 [jhs_sys].[t_bukken_rireki]
	(	
		 kbn
	   , hosyousyo_no	
	   , rireki_syubetu	
	   , rireki_no	
	   , nyuuryoku_no	
	   , naiyou	
	   , hanyou_date	
	   , hanyou_cd	
	   , kanri_date	
	   , henkou_kahi_flg	
	   , torikesi	
	   , add_login_user_id	
	   , add_datetime	
	)	
	SELECT	
		 TJ.kbn
	   , TJ.hosyousyo_no	
	   , '18' AS rireki_syubetu		
	   , '11' AS rireki_no		
	   , (ISNULL(BR.nyuuryoku_no, 0) + 1) AS nyuuryoku_no		
--	   , ('保証開始日' 		
--			+ ISNULL(CONVERT(varchar, HK.hw_ato_hkn_tekiyou_yotei_jissi_date, 111), '') 
--			+ '。②引渡しから10年間') AS naiyou
	   , '2013/1/31までに引渡前保険申請済みで未引渡または保証開始日変更で一括保険申請（2013年2月1日から13年間）'  AS naiyou		
	   , [jhs_sys].[fnGetLastMonthFirstDay](@ADD_DATETIME) AS hanyou_date		
	   , Case When ISNULL(BR17.hanyou_date,'2010/2/1')>='2010/2/1' OR ISNULL(BR17.rireki_no,31)=31 		
--		then '018' else '016' end AS hanyou_cd	
		then '019' else '020' end AS hanyou_cd	
--	   , HK.hw_ato_hkn_tekiyou_yotei_jissi_date AS kanri_date		
	   , '2013/02/01' AS kanri_date		
	   , '1' AS henkou_kahi_flg		
	   , '0' AS torikesi		
	   , @ADD_USER_ID AS add_login_user_id		
	   , @ADD_DATETIME AS add_datetime					
	FROM					
		 [jhs_sys].fnGetHokenSinseiDataTable(3,@ADD_DATETIME) HK				
			  INNER JOIN [jhs_sys].[t_jiban] TJ			
				ON TJ.kbn = HK.kbn		
			   AND TJ.hosyousyo_no = HK.hosyousyo_no			
			  LEFT OUTER JOIN			
				  (SELECT		
						kbn
					  , hosyousyo_no	
					  , max(nyuuryoku_no) AS nyuuryoku_no	
				   FROM		
						[jhs_sys].[t_bukken_rireki]
				   GROUP BY		
						kbn
					  , hosyousyo_no	
				  )	
				   BR	
				ON BR.kbn = TJ.kbn	
			   AND BR.hosyousyo_no = TJ.hosyousyo_no		
			  LEFT OUTER JOIN		
				  (SELECT	
					kbn
					  , hosyousyo_no
					  , max(nyuuryoku_no) AS nyuuryoku_no
				   FROM	
					[jhs_sys].[t_bukken_rireki]
				   WHERE	
					  rireki_syubetu='17'
				   GROUP BY	
					kbn
					  , hosyousyo_no
				  )
				   BR17MAX
				ON BR17MAX.kbn = TJ.kbn
			   AND BR17MAX.hosyousyo_no = TJ.hosyousyo_no	
			  LEFT OUTER JOIN	
				[jhs_sys].[t_bukken_rireki]   BR17
				ON BR17MAX.kbn = BR17.kbn
			   AND BR17MAX.hosyousyo_no = BR17.hosyousyo_no	
			   AND BR17MAX.nyuuryoku_no = BR17.nyuuryoku_no	
	WHERE			
		ISNULL(TJ.hoken_kotei_flg, 0) NOT In(2)		

  /*************************				
  * 処理件数、エラーNOセット（物件履歴追加・後保険／申請）				
  *************************/				
	SELECT			
	  @ERR_NO = @@ERROR	
	  ,@COUNT_HW_ATO_SINSEI = @@ROWCOUNT	

	--エラーチェック	
	IF @ERR_NO <> 0	
	BEGIN	
	  -- トランザクションをロールバック（キャンセル）	
	  ROLLBACK TRANSACTION	
	  RETURN @ERR_NO	
	END	

  /*************************		
  * 保証書管理テーブル更新（後保険／申請）		
  *************************/		
	UPDATE	
		 [jhs_sys].[t_hosyousyo_kanri]
	SET			
		 hoken_kaisya = '2'		
	   , syori_flg = '2'			
       , syori_datetime = @ADD_DATETIME				
	   , upd_login_user_id = @ADD_USER_ID			
	   , upd_datetime = @ADD_DATETIME			
	FROM			
		 [jhs_sys].fnGetHokenSinseiDataTable(3,@ADD_DATETIME) HK		
			  INNER JOIN [jhs_sys].[t_hosyousyo_kanri] THK	
				ON HK.kbn = THK.kbn
			   AND HK.hosyousyo_no = THK.hosyousyo_no	

  /*************************				
  * 処理件数、エラーNOセット（地盤テーブル更新・後保険／申請）				
  *************************/				
	SELECT			
	  @ERR_NO = @@ERROR		
	  ,@COUNT_HK_UPDATE = @@ROWCOUNT		

	--エラーチェック		
	IF @ERR_NO <> 0		
	BEGIN		
	  -- トランザクションをロールバック（キャンセル）		
	  ROLLBACK TRANSACTION		
	  RETURN @ERR_NO		
	END 		

/*************************************************************************			
 * Ⅵ．特定商品通知／取消 [物件履歴テーブルの追加処理]			
 **************************************************************************/			
	INSERT INTO		
			 [jhs_sys].[t_bukken_rireki]
		(	
			 kbn
		   , hosyousyo_no	
		   , rireki_syubetu	
		   , rireki_no	
		   , nyuuryoku_no	
		   , naiyou	
		   , hanyou_date	
		   , hanyou_cd	
		   , kanri_date	
		   , henkou_kahi_flg	
		   , torikesi	
		   , add_login_user_id	
		   , add_datetime	
		)	
	SELECT		
		 TJ.kbn	
	   , TJ.hosyousyo_no		
	   , '34' AS rireki_syubetu		
	   , '31' AS rireki_no		
	   , (ISNULL(BR.nyuuryoku_no, 0) + 1) AS nyuuryoku_no		
	   , ('売上日' + ISNULL(CONVERT(varchar, HK.tokutei_syouhin_jissi_date, 111), '') + '。特定商品通知') AS naiyou		
	   , [jhs_sys].[fnGetLastMonthFirstDay](@ADD_DATETIME) AS hanyou_date		
	   , sub.tokutei_hoken_sinsei_flg AS hanyou_cd		
	   , HK.tokutei_syouhin_jissi_date AS kanri_date		
	   , '1' AS henkou_kahi_flg		
	   , '0' AS torikesi		
	   , @ADD_USER_ID AS add_login_user_id		
	   , @ADD_DATETIME AS add_datetime		
	FROM		
		 [jhs_sys].fnGetHokenSinseiDataTable(6, @ADD_DATETIME) HK	
			  INNER JOIN [jhs_sys].[t_jiban] TJ
				ON TJ.kbn = HK.kbn		
			   AND TJ.hosyousyo_no = HK.hosyousyo_no			
			  LEFT OUTER JOIN			
				  (SELECT		
						kbn
					  , hosyousyo_no	
					  , max(nyuuryoku_no) AS nyuuryoku_no	
				   FROM		
						[jhs_sys].[t_bukken_rireki]
				   GROUP BY		
						kbn
					  , hosyousyo_no	
				  )		
				   BR		
				ON BR.kbn = TJ.kbn		
			   AND BR.hosyousyo_no = TJ.hosyousyo_no			
			  INNER JOIN				
				  (SELECT			
						TTS.kbn	
					  , TTS.hosyousyo_no		
					  , max(NullIf(MS.tokutei_hoken_sinsei_flg, '')) AS tokutei_hoken_sinsei_flg		
				   FROM			
					   (SELECT		
							 TS.kbn
						   , TS.hosyousyo_no	
						   , TS.bunrui_cd	
						   , TS.syouhin_cd	
						   , min(TS.uri_date) AS min_uri_date	
						FROM	
							 t_teibetu_seikyuu TS
						GROUP BY	
							 TS.kbn
						   , TS.hosyousyo_no			
						   , TS.bunrui_cd			
						   , TS.syouhin_cd			
						HAVING			
							 min(TS.uri_date) is NOT null		
						 AND TS.bunrui_cd in(100, 110, 115, 120)			
					   )				
						TTS			
							 LEFT OUTER JOIN		
								 (SELECT	
									   *
								  FROM	
									   m_syouhin
								 )	
								  MS	
							   ON TTS.syouhin_cd = MS.syouhin_cd		
				   GROUP BY		
						TTS.kbn
					  , TTS.hosyousyo_no	
				   HAVING max(ISNULL(NullIf(MS.tokutei_hoken_sinsei_flg, ''), 0)) <> 0		
				  )		
				   sub		
				ON sub.kbn = TJ.kbn		
			   AND sub.hosyousyo_no = TJ.hosyousyo_no			

  /*************************						
  * 処理件数、エラーNOセット（特定商品通知／取消）						
  *************************/						
	SELECT					
	  @ERR_NO = @@ERROR					
	  ,@COUNT_TT_TORIKESI = @@ROWCOUNT					

	--エラーチェック		
	IF @ERR_NO <> 0		
	BEGIN		
	  -- トランザクションをロールバック（キャンセル）		
	  ROLLBACK TRANSACTION		
	  RETURN @ERR_NO		
	END		

/*************************************************************************			
 * Ⅴ．特定商品通知 [物件履歴テーブルの追加処理]			
 **************************************************************************/			
	INSERT INTO		
			 [jhs_sys].[t_bukken_rireki]
		(	
			 kbn
		   , hosyousyo_no	
		   , rireki_syubetu
		   , rireki_no
		   , nyuuryoku_no
		   , naiyou
		   , hanyou_date
		   , hanyou_cd
		   , kanri_date
		   , henkou_kahi_flg
		   , torikesi
		   , add_login_user_id
		   , add_datetime
		)
	SELECT	
		 TJ.kbn
	   , TJ.hosyousyo_no	
	   , '34' AS rireki_syubetu	
	   , '11' AS rireki_no			
	   , (ISNULL(BR.nyuuryoku_no, 0) + 1) AS nyuuryoku_no			
	   , ('売上日' + ISNULL(CONVERT(varchar, HK.tokutei_syouhin_jissi_date, 111), '') + '。特定商品通知_' + 			
		ISNULL(CONVERT(varchar, sub.tokutei_hoken_sinsei_flg),'')) AS naiyou		
	   , [jhs_sys].[fnGetLastMonthFirstDay](@ADD_DATETIME) AS hanyou_date			
	   , sub.tokutei_hoken_sinsei_flg AS hanyou_cd			
	   , HK.tokutei_syouhin_jissi_date AS kanri_date			
	   , '1' AS henkou_kahi_flg			
	   , '0' AS torikesi			
	   , @ADD_USER_ID AS add_login_user_id			
	   , @ADD_DATETIME AS add_datetime			
	FROM			
		 [jhs_sys].fnGetHokenSinseiDataTable(5, @ADD_DATETIME) HK		
			  INNER JOIN [jhs_sys].[t_jiban] TJ	
				ON TJ.kbn = HK.kbn
			   AND TJ.hosyousyo_no = HK.hosyousyo_no	
			  LEFT OUTER JOIN			
				  (SELECT		
						kbn
					  , hosyousyo_no	
					  , max(nyuuryoku_no) AS nyuuryoku_no	
				   FROM		
						[jhs_sys].[t_bukken_rireki]
				   GROUP BY		
						kbn
					  , hosyousyo_no	
				  )		
				   BR		
				ON BR.kbn = TJ.kbn		
			   AND BR.hosyousyo_no = TJ.hosyousyo_no			
			  INNER JOIN			
				  (SELECT		
						TTS.kbn	
					  , TTS.hosyousyo_no		
					  , max(NullIf(MS.tokutei_hoken_sinsei_flg, '')) AS tokutei_hoken_sinsei_flg		
				   FROM			
					   (SELECT		
							 TS.kbn
						   , TS.hosyousyo_no	
						   , TS.bunrui_cd	
						   , TS.syouhin_cd	
						   , min(TS.uri_date) AS min_uri_date	
						FROM	
							 t_teibetu_seikyuu TS
						GROUP BY	
							 TS.kbn
						   , TS.hosyousyo_no	
						   , TS.bunrui_cd	
						   , TS.syouhin_cd			
						HAVING			
							 min(TS.uri_date) is NOT null		
						 AND TS.bunrui_cd in(100, 110, 115, 120)			
					   )				
						TTS			
							 LEFT OUTER JOIN		
								 (SELECT	
									   *
								  FROM	
									   m_syouhin
								 )	
								  MS	
							   ON TTS.syouhin_cd = MS.syouhin_cd		
				   GROUP BY					
						TTS.kbn			
					  , TTS.hosyousyo_no
				   HAVING max(ISNULL(NullIf(MS.tokutei_hoken_sinsei_flg, ''), 0)) <> 0	
				  )	
				   sub	
				ON sub.kbn = TJ.kbn	
			   AND sub.hosyousyo_no = TJ.hosyousyo_no		

  /*************************					
  * 処理件数、エラーNOセット（特定商品通知／取消）					
  *************************/					
	SELECT				
	  @ERR_NO = @@ERROR				
	  ,@COUNT_TT_TUUTI = @@ROWCOUNT				

	--エラーチェック				
	IF @ERR_NO <> 0				
	BEGIN
	  -- トランザクションをロールバック（キャンセル）
	  ROLLBACK TRANSACTION
	  RETURN @ERR_NO
	END

-- トランザクションをコミット	
COMMIT TRANSACTION	
RETURN @@ERROR 	
  	
END
GO
