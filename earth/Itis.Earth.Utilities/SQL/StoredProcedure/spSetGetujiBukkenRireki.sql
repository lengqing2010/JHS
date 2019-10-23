-- =====================================================							
-- Description:	���������e�[�u�������X�V����						
--				�ۏ؏��Ǘ��e�[�u�������X�V�����i�ی���ЁA�ی��\�����A�ی��\���敪�j			
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

	--�ϐ��̐錾	
	DECLARE @ADD_USER_ID             VARCHAR(30)  --�ǉ����O�C�����[�U�[ID	
	DECLARE @ADD_DATETIME            DATETIME     --�ǉ�����	

	--������	
	SET @ADD_USER_ID = 'system'	
	SET @ADD_DATETIME = GETDATE()	

/*************************************************************************		
 * �U�D�����n���O�ی��^��� [���������e�[�u���̒ǉ�����]		
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
	   , ('����ݒ��' + ISNULL(CONVERT(varchar, (			
			  CASE	
				   WHEN TJ.hosyousyo_hak_jyky_settei_date IS NOT NULL
				   THEN TJ.hosyousyo_hak_jyky_settei_date
				   ELSE TJ.data_haki_date
			  END), 111), '') + '�B�ی��\�����') AS naiyou	
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
  * ���������A�G���[NO�Z�b�g�i��������ǉ��E�O�ی��^����j						
  *************************/	
	SELECT
	  @ERR_NO = @@ERROR
	  ,@COUNT_HW_MAE_TORIKESI = @@ROWCOUNT

	--�G���[�`�F�b�N
	IF @ERR_NO <> 0
	BEGIN
	  -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j
	  ROLLBACK TRANSACTION
	  RETURN @ERR_NO
	END          

  /*************************	
  * �ۏ؏��Ǘ��e�[�u���X�V�i�O�ی��^����j	
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
  * ���������A�G���[NO�Z�b�g�i�ۏ؏��Ǘ��e�[�u���X�V�E�O�ی��^����j	
  *************************/	
	SELECT
	  @ERR_NO = @@ERROR
	  ,@COUNT_HK_UPDATE = @@ROWCOUNT

	--�G���[�`�F�b�N
	IF @ERR_NO <> 0
	BEGIN
	  -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j
	  ROLLBACK TRANSACTION
	  RETURN @ERR_NO
	END 

/*************************************************************************	
 * �T�D�����n���O�ی��^�\�� [���������e�[�u���̒ǉ�����]		
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
--				   THEN '�H�������' 		
				   THEN '���H���񒅓��܂��͍H���񍐏��󗝓�' 		
--               			+ ISNULL(CONVERT(varchar, HK.hw_mae_hkn_tekiyou_yotei_jissi_date, 111), '') 			
--						+ '�B�@���n���܂Łi����3�N�ԁj'
				   ELSE '���������' 		
--				   ELSE '���������' 		
--               			+ ISNULL(CONVERT(varchar, HK.hw_mae_hkn_tekiyou_yotei_jissi_date, 111), '') 			
--						+ '�B�@���n���܂Łi����3�N�ԁj'
--			  END) AS naiyou			
			END
       			+ ISNULL(CONVERT(varchar, HK.hw_mae_hkn_tekiyou_yotei_jissi_date, 111), '') 			
			 + '����3�N��+���n������'	
			+Case When ISNULL(HK.hosyou_kikan,10)=20 AND ISNULL(TJ.hosyousyo_hak_date,'2013/2/1')>='2013/2/1' Then '15' Else '10' End+'�N��')  AS naiyou
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
  * ���������A�G���[NO�Z�b�g�i��������ǉ��E�O�ی��^�\���j						
  *************************/	
	SELECT
	  @ERR_NO = @@ERROR
	  ,@COUNT_HW_MAE_SINSEI = @@ROWCOUNT

	--�G���[�`�F�b�N
	IF @ERR_NO <> 0
	BEGIN
	  -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j
	  ROLLBACK TRANSACTION
	  RETURN @ERR_NO
	END

  /*************************	
  * �ۏ؏��Ǘ��e�[�u���X�V�i�O�ی��^�\���j	
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
  * ���������A�G���[NO�Z�b�g�i�n�Ճe�[�u���X�V�E�O�ی��^�\���j	
  *************************/	
	SELECT
	  @ERR_NO = @@ERROR
	  ,@COUNT_HK_UPDATE = @@ROWCOUNT

	--�G���[�`�F�b�N
	IF @ERR_NO <> 0
	BEGIN
	  -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j
	  ROLLBACK TRANSACTION
	  RETURN @ERR_NO
	END 


/*************************************************************************		
 * �W�D�����n����ی��^��� [���������e�[�u���̒ǉ�����]		
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
--					  THEN '���n�ύX��' + 		
--						ISNULL(CONVERT(varchar, TJ.hosyousyo_saihak_date, 111), '') + 	
--						'�B���n���ύX�ɂ��ی��\�����'	
--					  WHEN HK.hw_ato_hkn_torikesi_syubetsu = '0'		
--					  THEN '����ݒ��' + ISNULL(CONVERT(varchar, (		
--						CASE	
--							 WHEN TJ.hosyousyo_hak_jyky_settei_date IS NOT NULL
--							 THEN TJ.hosyousyo_hak_jyky_settei_date
--							 ELSE TJ.data_haki_date
--						END), 111), '') + '�B�ی��\�����'	
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
  * ���������A�G���[NO�Z�b�g�i��������ǉ��E��ی��^�\���j				
  *************************/				
	SELECT			
	  @ERR_NO = @@ERROR			
	  ,@COUNT_HW_ATO_TORIKESI = @@ROWCOUNT			

	--�G���[�`�F�b�N			
	IF @ERR_NO <> 0	
	BEGIN	
	  -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j	
	  ROLLBACK TRANSACTION	
	  RETURN @ERR_NO	
	END	



/*************************************************************************		
 * �V�D�����n����ی��^�\�� [���������e�[�u���̒ǉ�����]		
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
--	   , ('�ۏ؊J�n��' 		
--			+ ISNULL(CONVERT(varchar, HK.hw_ato_hkn_tekiyou_yotei_jissi_date, 111), '') 
--			+ '�B�A���n������10�N��') AS naiyou
	   , '2013/1/31�܂łɈ��n�O�ی��\���ς݂Ŗ����n�܂��͕ۏ؊J�n���ύX�ňꊇ�ی��\���i2013�N2��1������13�N�ԁj'  AS naiyou		
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
  * ���������A�G���[NO�Z�b�g�i��������ǉ��E��ی��^�\���j				
  *************************/				
	SELECT			
	  @ERR_NO = @@ERROR	
	  ,@COUNT_HW_ATO_SINSEI = @@ROWCOUNT	

	--�G���[�`�F�b�N	
	IF @ERR_NO <> 0	
	BEGIN	
	  -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j	
	  ROLLBACK TRANSACTION	
	  RETURN @ERR_NO	
	END	

  /*************************		
  * �ۏ؏��Ǘ��e�[�u���X�V�i��ی��^�\���j		
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
  * ���������A�G���[NO�Z�b�g�i�n�Ճe�[�u���X�V�E��ی��^�\���j				
  *************************/				
	SELECT			
	  @ERR_NO = @@ERROR		
	  ,@COUNT_HK_UPDATE = @@ROWCOUNT		

	--�G���[�`�F�b�N		
	IF @ERR_NO <> 0		
	BEGIN		
	  -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j		
	  ROLLBACK TRANSACTION		
	  RETURN @ERR_NO		
	END 		

/*************************************************************************			
 * �Y�D���菤�i�ʒm�^��� [���������e�[�u���̒ǉ�����]			
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
	   , ('�����' + ISNULL(CONVERT(varchar, HK.tokutei_syouhin_jissi_date, 111), '') + '�B���菤�i�ʒm') AS naiyou		
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
  * ���������A�G���[NO�Z�b�g�i���菤�i�ʒm�^����j						
  *************************/						
	SELECT					
	  @ERR_NO = @@ERROR					
	  ,@COUNT_TT_TORIKESI = @@ROWCOUNT					

	--�G���[�`�F�b�N		
	IF @ERR_NO <> 0		
	BEGIN		
	  -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j		
	  ROLLBACK TRANSACTION		
	  RETURN @ERR_NO		
	END		

/*************************************************************************			
 * �X�D���菤�i�ʒm [���������e�[�u���̒ǉ�����]			
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
	   , ('�����' + ISNULL(CONVERT(varchar, HK.tokutei_syouhin_jissi_date, 111), '') + '�B���菤�i�ʒm_' + 			
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
  * ���������A�G���[NO�Z�b�g�i���菤�i�ʒm�^����j					
  *************************/					
	SELECT				
	  @ERR_NO = @@ERROR				
	  ,@COUNT_TT_TUUTI = @@ROWCOUNT				

	--�G���[�`�F�b�N				
	IF @ERR_NO <> 0				
	BEGIN
	  -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j
	  ROLLBACK TRANSACTION
	  RETURN @ERR_NO
	END

-- �g�����U�N�V�������R�~�b�g	
COMMIT TRANSACTION	
RETURN @@ERROR 	
  	
END
GO
