-- =====================================================	
-- Description:	�ۏ؏��Ǘ��e�[�u���X�V����(����)
--  1.�ۏ؏��Ǘ��f�[�^.�����n���O�ی�[�ی��\��] �K�p�\����{���̂�	
--  2.�ۏ؏��Ǘ��f�[�^.�����n����ی�[�ی��\��] �K�p�\����{���̂�	
--  3.�ۏ؏��Ǘ��f�[�^.�����n����ی�������	
--  4.�ۏ؏��Ǘ��f�[�^.���菤�i�K�p�\����{��	
-- =====================================================	
CREATE PROCEDURE [jhs_sys].[spSetGetujiHosyousyo_yotei_date]	
   @COUNT_HK_MAE_SINSEI                 INT         OUTPUT --�����n���O�ی��\��	
  ,@COUNT_HK_ATO_SINSEI                 INT         OUTPUT --�����n����ی��\��	
  ,@COUNT_HK_ATO_TORIKESI1              INT         OUTPUT --�����n����ی�����E������1--�����n����ی������ʁE�ی��s�v�t���O������
  ,@COUNT_HK_ATO_TORIKESI2              INT         OUTPUT --�����n����ی�����E������2
  ,@COUNT_HK_ATO_TORIKESI3              INT         OUTPUT --�����n����ی�����E������3
  ,@COUNT_HK_ATO_TORIKESI4              INT         OUTPUT --�����n����ی�����E������4
  ,@COUNT_HK_TOKUTEI                    INT         OUTPUT --���菤�i�K�p�\����{��	
  ,@ERR_NO                              INT         OUTPUT --���s����	
AS	
BEGIN	
	BEGIN TRANSACTION

	SET NOCOUNT ON;

	DECLARE @UPD_USER_ID               VARCHAR(30)  --�X�V���O�C�����[�U�[ID
	DECLARE @UPD_DATETIME              DATETIME     --�X�V����
	DECLARE @SYORI_DATE                VARCHAR(10)  --������
	DECLARE @SYORI_BEFORE_MONTH_LAST_DATE     VARCHAR(10)  --�����N����_�O������

	--������
  SET @UPD_USER_ID = 'system'	
  SET @UPD_DATETIME = GETDATE()	
  	
  --������			
  SET @SYORI_DATE = CONVERT(VARCHAR, @UPD_DATETIME, 111)			
  			
  --�O������			
  SELECT @SYORI_BEFORE_MONTH_LAST_DATE = CONVERT(VARCHAR,cast((cast(year(@SYORI_DATE) AS VARCHAR(4))			
	+'-'+cast(month(@SYORI_DATE) AS VARCHAR(2))+'-1') AS DATETIME)-1,111)		

/*************************************************************************			
 * 1.�ۏ؏��Ǘ��f�[�^.�����n���O�ی�[�ی��\��] �K�p�\����{���̂�			
 **************************************************************************/			
 UPDATE			
     [jhs_sys].[t_hosyousyo_kanri]			
  SET			
     hw_mae_hkn_tekiyou_yotei_jissi_date =			
--		 CASE	
--			  WHEN TJ.koj_hantei_kekka_flg IS NULL --NULL�͐�ɔ�r���Ă���
--			  THEN NULL							
--			  WHEN TJ.koj_hantei_kekka_flg = 0							
--			  THEN sub.uri_date							
--			  WHEN TJ.koj_hantei_kekka_flg = 1							
--			  THEN sub_koj.uri_date							
--			  ELSE NULL							
--		 END								
	 CASE WHEN TJ.koj_hantei_kekka_flg IS NULL --NULL�͐�ɔ�r���Ă���									
		THEN NULL								
	          WHEN TJ.koj_hantei_kekka_flg = 0									
		THEN								
			CASE WHEN HK.hw_mae_hkn_date IS NOT NULL 							
			           AND CONVERT(VARCHAR,HK.hw_mae_hkn_date,111)<'2013/02/01'  							
				THEN						
					CASE WHEN HK.hw_mae_hkn_jissi_date IS NOT NULL					
					           AND sub.koj_uri_date IS NOT NULL					
					           AND CONVERT(VARCHAR,HK.hw_mae_hkn_jissi_date,111) >= sub.koj_uri_date			
						THEN CONVERT(VARCHAR,HK.hw_mae_hkn_jissi_date,111)		
					         WHEN ISNULL(sub.koj_kanri_date,sub.uri_date) IS NOT NULL			
						THEN		
							CASE WHEN ISNULL(sub.koj_kanri_date,sub.uri_date) BETWEEN '2013/02/01' AND @SYORI_BEFORE_MONTH_LAST_DATE	
								THEN ISNULL(sub.koj_kanri_date,sub.uri_date)
							         WHEN ISNULL(sub.koj_kanri_date,sub.uri_date)<'2013/02/01'	
								THEN CONVERT(VARCHAR,HK.hw_mae_hkn_jissi_date,111)
							END	
					END			
			        WHEN ISNULL(sub.koj_kanri_date,sub.uri_date) IS NOT NULL AND ISNULL(sub.koj_kanri_date,sub.uri_date)<=@SYORI_BEFORE_MONTH_LAST_DATE					
			           AND (HK.hw_ato_hkn_jissi_date IS NULL OR CONVERT(VARCHAR,HK.hw_ato_hkn_jissi_date,111)='2013/02/01' OR TJ.hosyou_kaisi_date IS NULL OR 					
				(HK.hw_ato_hkn_jissi_date IS NOT NULL AND TJ.hosyou_kaisi_date IS NOT NULL AND HK.hw_ato_hkn_jissi_date<TJ.hosyou_kaisi_date))				
--			           AND (HK.hw_mae_hkn_jissi_date IS NULL OR ISNULL(sub.koj_kanri_date,sub.uri_date)>='2013/02/01')				
			           AND (HK.hw_mae_hkn_jissi_date IS NULL OR ISNULL(sub.koj_kanri_date,sub.uri_date) is not null)				
				THEN ISNULL(sub.koj_kanri_date,sub.uri_date)				
			END					
	          WHEN TJ.koj_hantei_kekka_flg = 1							
		THEN						
			CASE WHEN HK.hw_mae_hkn_date IS NOT NULL 					
			           AND CONVERT(VARCHAR,HK.hw_mae_hkn_date,111)<'2013/02/01'  					
				THEN				
					CASE WHEN HK.hw_mae_hkn_jissi_date IS NOT NULL			
					           AND sub.koj_uri_date IS NOT NULL			
					           AND CONVERT(VARCHAR,HK.hw_mae_hkn_jissi_date,111) >= sub.koj_uri_date			
						THEN CONVERT(VARCHAR,HK.hw_mae_hkn_jissi_date,111)		
					         WHEN sub.koj_kanri_date IS NOT NULL			
						THEN		
							CASE WHEN sub.koj_kanri_date BETWEEN '2013/02/01' AND @SYORI_BEFORE_MONTH_LAST_DATE	
								THEN sub.koj_kanri_date
							         WHEN sub.koj_kanri_date<'2013/02/01'	
								THEN CONVERT(VARCHAR,HK.hw_mae_hkn_jissi_date,111)
							END	
					END	
			        WHEN sub.koj_kanri_date IS NOT NULL AND sub.koj_kanri_date<=@SYORI_BEFORE_MONTH_LAST_DATE			
			           AND (HK.hw_ato_hkn_jissi_date IS NULL OR CONVERT(VARCHAR,HK.hw_ato_hkn_jissi_date,111)='2013/02/01' OR TJ.hosyou_kaisi_date IS NULL OR 			
				(HK.hw_ato_hkn_jissi_date IS NOT NULL AND TJ.hosyou_kaisi_date IS NOT NULL AND HK.hw_ato_hkn_jissi_date<TJ.hosyou_kaisi_date))		
--			           AND (HK.hw_mae_hkn_jissi_date IS NULL OR sub.koj_kanri_date>='2013/02/01')			
			           AND (HK.hw_mae_hkn_jissi_date IS NULL OR sub.koj_kanri_date is not null)			
				THEN sub.koj_kanri_date		
			END			
	          ELSE NULL					
	END					
   , syori_flg = '2'						
   , syori_datetime = @UPD_DATETIME						
   , upd_login_user_id = @UPD_USER_ID						
   , upd_datetime = @UPD_DATETIME						
  FROM [jhs_sys].[t_hosyousyo_kanri] HK						
      INNER JOIN [jhs_sys].[t_jiban] TJ						
        ON HK.kbn = TJ.kbn						
       AND HK.hosyousyo_no = TJ.hosyousyo_no			
      LEFT OUTER JOIN [jhs_sys].[m_hosyousyo_hak_jyky] HJ			
        ON TJ.hosyousyo_hak_jyky = HJ.hosyousyo_hak_jyky_no			
      LEFT OUTER JOIN			
          (SELECT			
                TJ_sub.kbn			
              , TJ_sub.hosyousyo_no			
--              , CONVERT(VARCHAR,MIN(ISNULL(TS_KOJ.uri_date, TS.min_uri_date)),111) AS uri_date			
              , CONVERT(VARCHAR,TS.min_uri_date,111) AS uri_date			
              , CONVERT(VARCHAR,TS_KOJ.uri_date,111) AS koj_uri_date			
--��
	,CONVERT(VARCHAR,Case		
		When TJ_sub.koj_hkks_juri_date  is NULL Then TJ_sub.kairy_koj_sokuhou_tyk_date 	
		When TJ_sub.kairy_koj_sokuhou_tyk_date is NULL Then TJ_sub.koj_hkks_juri_date 	
		When TJ_sub.kairy_koj_sokuhou_tyk_date<=TJ_sub.koj_hkks_juri_date Then TJ_sub.kairy_koj_sokuhou_tyk_date 	
		Else TJ_sub.koj_hkks_juri_date End,111) AS koj_kanri_date	
--�@�p�^�[���i�ۏ؏��Ɠ����Ŋ��H���񒅓��̂݁j
--	,TJ_sub.kairy_koj_sokuhou_tyk_date  AS koj_kanri_date	
--�A�p�^�[���i�ǉ����H���񒅓��D��j
--	,isnull(TJ_sub.t_koj_sokuhou_tyk_date,TJ_sub.kairy_koj_sokuhou_tyk_date)  AS koj_kanri_date	
--�B�p�^�[���i���H���񒅓��ƒǉ����H���񒅓��̐V�������t�j
--	,CONVERT(VARCHAR,Case		
--		When TJ_sub.t_koj_sokuhou_tyk_date  is NULL Then TJ_sub.kairy_koj_sokuhou_tyk_date 	
--		When TJ_sub.kairy_koj_sokuhou_tyk_date is NULL Then TJ_sub.t_koj_sokuhou_tyk_date 	
--		When TJ_sub.kairy_koj_sokuhou_tyk_date>=TJ_sub.t_koj_sokuhou_tyk_date Then TJ_sub.kairy_koj_sokuhou_tyk_date 	
--		Else TJ_sub.t_koj_sokuhou_tyk_date End,111) AS koj_kanri_date	
           FROM			
                jhs_sys.t_jiban TJ_sub
                     LEFT OUTER JOIN
                      (SELECT
                            ts.kbn
                          , ts.hosyousyo_no
                          , MIN(ts.uri_date) AS min_uri_date
                       FROM
                            [jhs_sys].[t_teibetu_seikyuu] ts
                                 LEFT OUTER JOIN m_syouhin ms
                                   ON ts.syouhin_cd = ms.syouhin_cd
                       WHERE
                            ms.hosyou_umu = 1
                        AND ts.bunrui_cd IN('100', '110', '115', '120')
                       GROUP BY
                            ts.kbn
                          , ts.hosyousyo_no
                      )TS
                       ON TJ_sub.kbn = TS.kbn
                      AND TJ_sub.hosyousyo_no = TS.hosyousyo_no
                     LEFT OUTER JOIN [jhs_sys].[t_teibetu_seikyuu] TS_KOJ
                       ON TJ_sub.kbn = TS_KOJ.kbn
                      AND TJ_sub.hosyousyo_no = TS_KOJ.hosyousyo_no
                      AND TS_KOJ.bunrui_cd = '130'
                      AND TS_KOJ.gamen_hyouji_no = 1
--           GROUP BY
--                TJ_sub.kbn
--              , TJ_sub.hosyousyo_no
          )
           AS sub
        ON TJ.kbn = sub.kbn
       AND TJ.hosyousyo_no = sub.hosyousyo_no
--      LEFT OUTER JOIN
--          (SELECT	
--                TJ_sub.kbn	
--              , TJ_sub.hosyousyo_no	
--              , CONVERT(VARCHAR,TS_KOJ.uri_date,111) AS uri_date	
--           FROM	
--                jhs_sys.t_jiban TJ_sub	
--                     LEFT OUTER JOIN [jhs_sys].[t_teibetu_seikyuu] TS_KOJ	
--                       ON TJ_sub.kbn = TS_KOJ.kbn	
--                      AND TJ_sub.hosyousyo_no = TS_KOJ.hosyousyo_no	
--           WHERE TS_KOJ.bunrui_cd = '130'	
--            AND TS_KOJ.gamen_hyouji_no = 1	
--          )	
--           AS sub_koj	
--        ON TJ.kbn = sub_koj.kbn	
--       AND TJ.hosyousyo_no = sub_koj.hosyousyo_no	
	WHERE
		 TJ.data_haki_syubetu IN('0', '99')					
	 AND HJ.mihak_list_inji_umu = 1						
	 AND TJ.hosyou_syouhin_umu = 1						
	 AND NOT EXISTS --���Еی��ł͂Ȃ�						
		(SELECT					
			  *				
		 FROM					
			  jhs_sys.t_bukken_rireki br				
				   INNER JOIN			
					   (SELECT		
							 brsub.kbn
						   , brsub.hosyousyo_no	
						   , ISNULL(max(brsub.nyuuryoku_no),0) AS max	
						FROM	
							 jhs_sys.t_bukken_rireki brsub
						WHERE	
							 brsub.rireki_syubetu = '19'
						 AND brsub.rireki_no <> '0'	
						 AND brsub.torikesi = '0'	
						GROUP BY	
							 brsub.kbn
						   , brsub.hosyousyo_no	
					   )		
						AS sub	
					 ON br.kbn = sub.kbn		
					AND br.hosyousyo_no = sub.hosyousyo_no		
					AND br.nyuuryoku_no = sub.max		
		 WHERE					
			  br.torikesi = '0'				
		  AND HK.kbn = br.kbn					
		  AND HK.hosyousyo_no = br.hosyousyo_no					
		  AND br.rireki_no <> '31'					
		 )
	   AND ((	
	        (TJ.koj_hantei_kekka_flg = 0	
	            AND	
--	          sub.uri_date IS NOT NULL --�Y�����R�[�h�����݂������	
	          ISNULL(ISNULL(sub.koj_kanri_date,sub.koj_uri_date),sub.uri_date) IS NOT NULL --�Y�����R�[�h�����݂������	
--	            AND	
--	         (	
--	           sub.uri_date <= @SYORI_BEFORE_MONTH_LAST_DATE --�o�b�`�������̑O���ȑO	
--              AND		
--	            (TJ.hosyou_kaisi_date IS NULL OR CONVERT(VARCHAR,TJ.hosyou_kaisi_date,111) > sub.uri_date OR 	
--		(HK.hw_mae_hkn = 1 AND CONVERT(VARCHAR,TJ.hosyou_kaisi_date,111) <= sub.uri_date))
--	           )	
	        )	
	    )	
	      OR	
	      (	
	        (TJ.koj_hantei_kekka_flg = 1	
	          AND	
--	           sub_koj.uri_date IS NOT NULL --�Y�����R�[�h�����݂������	
	           ISNULL(sub.koj_kanri_date,sub.koj_uri_date) IS NOT NULL --�Y�����R�[�h�����݂������	
--	            AND	
--	           (	
--	             sub_koj.uri_date <= @SYORI_BEFORE_MONTH_LAST_DATE --�o�b�`�������̑O���ȑO	
--	              AND	
--	            (TJ.hosyou_kaisi_date IS NULL OR CONVERT(VARCHAR,TJ.hosyou_kaisi_date,111) > sub_koj.uri_date OR 	
--		(HK.hw_mae_hkn = 1 AND CONVERT(VARCHAR,TJ.hosyou_kaisi_date,111) <= sub_koj.uri_date))
--	           )	
	 )	
	  ))	
	                 	
  /*************************		
  *���������A�G���[NO�Z�b�g
  *************************/
  SELECT
      @ERR_NO = @@ERROR
      ,@COUNT_HK_MAE_SINSEI = @@ROWCOUNT

  --�G���[�`�F�b�N
  IF @ERR_NO <> 0
  BEGIN
      -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j
      ROLLBACK TRANSACTION
      RETURN @ERR_NO
  END
  
/*************************************************************************
 * 2.�ۏ؏��Ǘ��f�[�^.�����n����ی�[�ی��\��] �K�p�\����{���̂�
 **************************************************************************/				
 UPDATE				
     [jhs_sys].[t_hosyousyo_kanri]				
  SET				
--     hw_ato_hkn_tekiyou_yotei_jissi_date = TJ.hosyou_kaisi_date				
     hw_ato_hkn_tekiyou_yotei_jissi_date = 				
	CASE WHEN 		HK.hw_mae_hkn_date IS NOT NULL AND 	
			CONVERT(VARCHAR,HK.hw_mae_hkn_date,111) < '2013/02/01' AND 	
			HK.hw_mae_hkn_jissi_date IS NOT NULL AND	
			HK.hw_mae_hkn_tekiyou_yotei_jissi_date IS NOT NULL AND	
			CONVERT(VARCHAR,HK.hw_mae_hkn_jissi_date,111) = CONVERT(VARCHAR,HK.hw_mae_hkn_tekiyou_yotei_jissi_date,111) AND	
			((HK.hw_ato_hkn_date IS NOT NULL AND CONVERT(VARCHAR,HK.hw_ato_hkn_date,111) >= '2013/02/01') OR	
			 (HK.hw_ato_hkn_jissi_date IS NOT NULL AND 	
			     (TJ.hosyou_kaisi_date IS NULL OR	
			      (TJ.hosyou_kaisi_date IS NOT NULL AND 	
			       CONVERT(VARCHAR,HK.hw_ato_hkn_jissi_date,111) < CONVERT(VARCHAR,TJ.hosyou_kaisi_date,111)))))	
		THEN '2013/02/01'		
	         WHEN		HK.hw_ato_hkn_jissi_date IS NOT NULL AND	
			TJ.hosyou_kaisi_date IS NOT NULL AND	
			CONVERT(VARCHAR,HK.hw_ato_hkn_jissi_date,111) >= CONVERT(VARCHAR,TJ.hosyou_kaisi_date,111) AND	
			(HK.hw_mae_hkn_tekiyou_yotei_jissi_date IS NULL OR 	
			  (HK.hw_mae_hkn_tekiyou_yotei_jissi_date IS NOT NULL AND CONVERT(VARCHAR,HK.hw_mae_hkn_tekiyou_yotei_jissi_date,111)<'2013/02/01'))	
		THEN TJ.hosyou_kaisi_date		
	END			
     , syori_flg = 2				
     , syori_datetime = @UPD_DATETIME				
     , upd_login_user_id = @UPD_USER_ID				
     , upd_datetime = @UPD_DATETIME				
  FROM				
       [jhs_sys].[t_hosyousyo_kanri] HK				
            INNER JOIN [jhs_sys].[t_jiban] TJ				
              ON HK.kbn = TJ.kbn				
             AND HK.hosyousyo_no = TJ.hosyousyo_no									
            LEFT OUTER JOIN [jhs_sys].[m_hosyousyo_hak_jyky] HJ									
              ON TJ.hosyousyo_hak_jyky = HJ.hosyousyo_hak_jyky_no									
  WHERE TJ.data_haki_syubetu IN('0','99')									
   AND HJ.mihak_list_inji_umu = 1									
   AND ((TJ.hosyou_kaisi_date IS NOT NULL AND 									
	CONVERT(VARCHAR, TJ.hosyou_kaisi_date, 111) <= @SYORI_BEFORE_MONTH_LAST_DATE)								
									 --�o�b�`�������̑O���ȑO
OR									
          (HK.hw_mae_hkn_tekiyou_yotei_jissi_date IS NOT NULL AND 									
	CONVERT(VARCHAR, HK.hw_mae_hkn_tekiyou_yotei_jissi_date, 111) < '2013/02/01'))								
	 AND NOT EXISTS --���Еی��ł͂Ȃ�								
		(SELECT							
			  *						
		 FROM							
			  jhs_sys.t_bukken_rireki br						
				   INNER JOIN			
					   (SELECT		
							 brsub.kbn
						   , brsub.hosyousyo_no	
						   , ISNULL(max(brsub.nyuuryoku_no),0) AS max	
						FROM	
							 jhs_sys.t_bukken_rireki brsub
						WHERE	
							 brsub.rireki_syubetu = '19'
						 AND brsub.rireki_no <> '0'	
						 AND brsub.torikesi = '0'	
						GROUP BY	
							 brsub.kbn
						   , brsub.hosyousyo_no	
					   )		
						AS sub	
					 ON br.kbn = sub.kbn
					AND br.hosyousyo_no = sub.hosyousyo_no
					AND br.nyuuryoku_no = sub.max
		 WHERE			
			  br.torikesi = '0'		
		  AND HK.kbn = br.kbn			
		  AND HK.hosyousyo_no = br.hosyousyo_no			
		  AND br.rireki_no <> '31'			
		 )   			
    					
  /*************************					
  *���������A�G���[NO�Z�b�g					
  *************************/					
  SELECT					
      @ERR_NO = @@ERROR					
      ,@COUNT_HK_ATO_SINSEI = @@ROWCOUNT					

  --�G���[�`�F�b�N	
  IF @ERR_NO <> 0	
  BEGIN	
      -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j	
      ROLLBACK TRANSACTION	
      RETURN @ERR_NO	
  END	
  	
/*************************************************************************	
 * 3.�ۏ؏��Ǘ��f�[�^.�����n����ی�������	
 **************************************************************************/	
--	 --���n����ی��E���1
--	 UPDATE
--	     [jhs_sys].[t_hosyousyo_kanri]
--	  SET
--	       hw_ato_hkn_torikesi_syubetsu = 0
--	     , syori_flg = '2'
--	     , syori_datetime = @UPD_DATETIME
--	     , upd_login_user_id = @UPD_USER_ID
--	     , upd_datetime = @UPD_DATETIME
--	  FROM
--	       [jhs_sys].[t_hosyousyo_kanri] HK
--	        INNER JOIN [jhs_sys].[t_jiban] TJ
--	          ON HK.kbn = TJ.kbn
--	          AND HK.hosyousyo_no = TJ.hosyousyo_no
--	        LEFT OUTER JOIN [jhs_sys].[m_hosyousyo_hak_jyky] HJ
--	          ON TJ.hosyousyo_hak_jyky = HJ.hosyousyo_hak_jyky_no
--	  WHERE 
--	    (
--	      (
--	        TJ.data_haki_syubetu NOT IN('0','99')
--	          OR							
--	        HJ.mihak_list_inji_umu = 0							
--	      )							
--		 AND NOT EXISTS --���Еی��ł͂Ȃ�						
--			(SELECT					
--				  *				
--			 FROM					
--				  jhs_sys.t_bukken_rireki br				
--					   INNER JOIN			
--						   (SELECT		
--								 brsub.kbn
--							   , brsub.hosyousyo_no	
--							   , ISNULL(max(brsub.nyuuryoku_no),0) AS max	
--							FROM	
--								 jhs_sys.t_bukken_rireki brsub
--							WHERE	
--								 brsub.rireki_syubetu = '19'
--							 AND brsub.rireki_no <> '0'	
--							 AND brsub.torikesi = '0'	
--							GROUP BY	
--								 brsub.kbn
--							   , brsub.hosyousyo_no	
--						   )		
--							AS sub	
--						 ON br.kbn = sub.kbn		
--						AND br.hosyousyo_no = sub.hosyousyo_no		
--						AND br.nyuuryoku_no = sub.max		
--			 WHERE					
--				  br.torikesi = '0'				
--			  AND HK.kbn = br.kbn					
--			  AND HK.hosyousyo_no = br.hosyousyo_no					
--			  AND br.rireki_no <> '31'					
--			 )
--	   )		
--			
--	  /*************************		
--	  *���������A�G���[NO�Z�b�g		
--	  *************************/		
--	  SELECT		
--	      @ERR_NO = @@ERROR		
--	      ,@COUNT_HK_ATO_TORIKESI1 = @@ROWCOUNT		
--			
--	  --�G���[�`�F�b�N		
--	  IF @ERR_NO <> 0		
--	  BEGIN		
--	      -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j		
--	      ROLLBACK TRANSACTION		
--	      RETURN @ERR_NO		
--	  END
--	  
--	 --���n����ی��E���2
--	 UPDATE
--	     [jhs_sys].[t_hosyousyo_kanri]
--	  SET
--	       hw_ato_hkn_torikesi_syubetsu = 0
--	     , syori_flg = '2'
--	     , syori_datetime = @UPD_DATETIME
--	     , upd_login_user_id = @UPD_USER_ID
--	     , upd_datetime = @UPD_DATETIME
--	  FROM
--	       [jhs_sys].[t_hosyousyo_kanri] HK
--	        INNER JOIN [jhs_sys].[t_jiban] TJ
--	          ON HK.kbn = TJ.kbn
--	          AND HK.hosyousyo_no = TJ.hosyousyo_no
--	        LEFT OUTER JOIN [jhs_sys].[m_hosyousyo_hak_jyky] HJ	
--	          ON TJ.hosyousyo_hak_jyky = HJ.hosyousyo_hak_jyky_no	
--	  WHERE	
--	     (TJ.hosyousyo_hak_date IS NULL OR 	
--		(TJ.hosyousyo_hak_date IS NOT NULL AND 
--		CONVERT(VARCHAR, TJ.hosyousyo_hak_date, 111) >= @SYORI_DATE)) --�o�b�`�������ȍ~
--		
--	  /*************************	
--	  *���������A�G���[NO�Z�b�g	
--	  *************************/	
--	  SELECT	
--	      @ERR_NO = @@ERROR	
--	      ,@COUNT_HK_ATO_TORIKESI2 = @@ROWCOUNT	
--		
--	  --�G���[�`�F�b�N	
--	  IF @ERR_NO <> 0	
--	  BEGIN
--	      -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j
--	      ROLLBACK TRANSACTION
--	      RETURN @ERR_NO
--	  END
--	  
--	 --���n����ی��E���3
--	 UPDATE
--	     [jhs_sys].[t_hosyousyo_kanri]
--	  SET
--	     hw_ato_hkn_torikesi_syubetsu = 0
--	     , syori_flg = '2'
--	     , syori_datetime = @UPD_DATETIME
--	     , upd_login_user_id = @UPD_USER_ID
--	     , upd_datetime = @UPD_DATETIME
--	  WHERE 
--	    hw_ato_hkn_tekiyou_yotei_jissi_date IS NULL
--	   
--	  /*************************
--	  *���������A�G���[NO�Z�b�g
--	  *************************/
--	  SELECT
--	      @ERR_NO = @@ERROR
--	      ,@COUNT_HK_ATO_TORIKESI3 = @@ROWCOUNT
--	
--	  --�G���[�`�F�b�N
--	  IF @ERR_NO <> 0
--	  BEGIN
--	      -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j
--	      ROLLBACK TRANSACTION
--	      RETURN @ERR_NO
--	  END
--	 
--	 --���n����ی��E���4
--	 UPDATE
--	     [jhs_sys].[t_hosyousyo_kanri]
--	  SET
--	     hw_ato_hkn_torikesi_syubetsu = 1
--	     , syori_flg = '2'
--	     , syori_datetime = @UPD_DATETIME
--	     , upd_login_user_id = @UPD_USER_ID
--	     , upd_datetime = @UPD_DATETIME
--	  WHERE 
--	    CONVERT(VARCHAR,hw_ato_hkn_tekiyou_yotei_jissi_date,111) <> CONVERT(VARCHAR,hw_ato_hkn_jissi_date,111)
--	  /*************************
--	  *���������A�G���[NO�Z�b�g
--	  *************************/
--	  SELECT
--	      @ERR_NO = @@ERROR
--	      ,@COUNT_HK_ATO_TORIKESI4 = @@ROWCOUNT
	
 --���n����ی��E�����ʂɂ͑S��0���Z�b�g	
 UPDATE	
     [jhs_sys].[t_hosyousyo_kanri]	
  SET	
     hw_ato_hkn_torikesi_syubetsu = 0	
     , hkn_huyou_flg = 0	
     , syori_flg = '2'	
     , syori_datetime = @UPD_DATETIME	
     , upd_login_user_id = @UPD_USER_ID	
     , upd_datetime = @UPD_DATETIME	
   	
  /*************************	
  *���������A�G���[NO�Z�b�g	
  *************************/	
  SELECT	
      @ERR_NO = @@ERROR	
      ,@COUNT_HK_ATO_TORIKESI1 = @@ROWCOUNT	

  --�G���[�`�F�b�N	
  IF @ERR_NO <> 0		
  BEGIN		
      -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j		
      ROLLBACK TRANSACTION		
      RETURN @ERR_NO		
  END		

/*************************************************************************		
 * 4.�ۏ؏��Ǘ��f�[�^.���菤�i�K�p�\����{��		
 **************************************************************************/		
	UPDATE	
		 [jhs_sys].[t_hosyousyo_kanri]
	SET	
		 tokutei_syouhin_tekiyou_yotei_jissi_date = ren.min_uri_date
		 , syori_flg = 2
		 , syori_datetime = @UPD_DATETIME
		 , upd_login_user_id = @UPD_USER_ID					
		 , upd_datetime = @UPD_DATETIME					
	FROM						
		 [jhs_sys].[t_hosyousyo_kanri] HK					
			  INNER JOIN [jhs_sys].[t_jiban] TJ				
				ON HK.kbn = TJ.kbn			
			   AND HK.hosyousyo_no = TJ.hosyousyo_no				
			  INNER JOIN				
				  (SELECT			
						TTS.kbn	
					  , TTS.hosyousyo_no		
					  , MIN(TTS.uri_date) AS min_uri_date		
				   FROM			
					   (SELECT		
							 TSB.kbn
						   , TSB.hosyousyo_no	
						   , TSB.uri_date		
						FROM		
							 t_teibetu_seikyuu TSB	
						WHERE		
							 TSB.bunrui_cd in(100, 110, 115, 120)	
					   )			
						TTS		
					 INNER JOIN			
						 (SELECT		
							   TS.kbn	
							 , TS.hosyousyo_no	
						  FROM		
							   t_teibetu_seikyuu TS	
								LEFT OUTER JOIN m_syouhin MS
								  ON TS.syouhin_cd = MS.syouhin_cd
						  GROUP BY		
							   TS.kbn
							 , TS.hosyousyo_no
						  HAVING	
							   max(ISNULL(MS.tokutei_hoken_sinsei_flg, 0)) <> 0
						 )	
						  sub	
					   ON TTS.kbn = sub.kbn		
					  AND TTS.hosyousyo_no = sub.hosyousyo_no		
				   GROUP BY			
						TTS.kbn	
					  , TTS.hosyousyo_no		
				   HAVING			
						min(nullif(TTS.uri_date, '')) IS NOT NULL	
				  )			
				   ren			
				ON TJ.kbn = ren.kbn			
			   AND TJ.hosyousyo_no = ren.hosyousyo_no
	WHERE		
		 TJ.data_haki_syubetu IN('0', '99')  	

  /*************************			
  *���������A�G���[NO�Z�b�g			
  *************************/			
  SELECT			
      @ERR_NO = @@ERROR			
      ,@COUNT_HK_TOKUTEI = @@ROWCOUNT			

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
