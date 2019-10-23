-- =====================================================								
-- Description:	�ۏ؏��Ǘ��e�[�u���X�V����(����)							
--  1.�ۏ؏��Ǘ��f�[�^.�����n���O�ی�[�ی��\��] ���n���O�ی�,���n���O�ی��N����,���n���O�ی����{��								
--  2.�ۏ؏��Ǘ��f�[�^.�����n����ی�[�ی��\��] ���n����ی�,���n����ی��N����,���n����ی����{��								
--  3.�ۏ؏��Ǘ��f�[�^.�����z�ʒm[�ی��\��] ���菤�i���{��,���菤�iFLG								
-- =====================================================								
CREATE PROCEDURE [jhs_sys].[spSetGetujiHosyousyo]								
   @COUNT_HK_MAE_SINSEI                 INT         OUTPUT							
  ,@COUNT_HK_ATO_SINSEI                 INT         OUTPUT							
  ,@COUNT_HK_TOKUTEI					INT			OUTPUT
  ,@ERR_NO                              INT         OUTPUT								
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

--	spSetGetujiHosyousyo_setup�őO�ی��E��ی��͑S�s�X�V���Ă���̂œ��菤�i�̍X�V�݂̂�OK�i2013/2/27�j
--	/*************************************************************************
--	 * 1.�ۏ؏��Ǘ��f�[�^.�����n���O�ی�[�ی��\��] ���n���O�ی�,���n���O�ی��N����,���n���O�ی����{��
--	 **************************************************************************/
--	 UPDATE
--	     [jhs_sys].[t_hosyousyo_kanri]
--	  SET
--	     hw_mae_hkn = ISNULL(func.hw_hkn, 0) --�Y���f�[�^����ꍇ�A���肵�����ʂ��Z�b�g�B�Y���f�[�^�Ȃ��ꍇ��0�B
--	   , hw_mae_hkn_date = func.hw_hkn_date
--	   , hw_mae_hkn_jissi_date = func.hw_hkn_jissi_date
--	   , syori_flg = '2'
--	   , syori_datetime = @UPD_DATETIME
--	   , upd_login_user_id = @UPD_USER_ID
--	   , upd_datetime = @UPD_DATETIME
--	  FROM [jhs_sys].[t_hosyousyo_kanri] HK
--	      INNER JOIN [jhs_sys].[t_jiban] TJ
--	        ON HK.kbn = TJ.kbn
--	       AND HK.hosyousyo_no = TJ.hosyousyo_no
--	      LEFT OUTER JOIN [jhs_sys].[m_hosyousyo_hak_jyky] HJ
--	        ON TJ.hosyousyo_hak_jyky = HJ.hosyousyo_hak_jyky_no
--	      LEFT OUTER JOIN
--	          (SELECT
--	                br.kbn
--	              , br.hosyousyo_no
--	              , CASE
--	                     WHEN sub.kbn IS NOT NULL
--	                     THEN 1
--	                     ELSE 0
--	                END AS hw_hkn
--	              , br.hanyou_date AS hw_hkn_date
--	              , br.kanri_date AS hw_hkn_jissi_date
--	           FROM
--	                jhs_sys.t_bukken_rireki br
--	              , (SELECT
--	                     brsub.kbn
--	                   , brsub.hosyousyo_no
--	                   , ISNULL(MAX(brsub.nyuuryoku_no),0) AS max
--	                FROM
--	                     jhs_sys.t_bukken_rireki brsub
--	                WHERE
--	                     brsub.rireki_syubetu = '17'
--	                 AND brsub.rireki_no <> '0'
--	                 AND brsub.torikesi = '0'
--	                GROUP BY
--	                     brsub.kbn
--	                   , brsub.hosyousyo_no
--	               )
--	                sub
--	           WHERE
--	                br.kbn = sub.kbn
--	            AND br.hosyousyo_no = sub.hosyousyo_no
--	            AND br.nyuuryoku_no = sub.max
--	            AND br.rireki_syubetu = '17'
--	            AND br.rireki_no IN('11','21')
--	          ) AS func
--	         ON HK.kbn = func.kbn
--	         AND HK.hosyousyo_no = func.hosyousyo_no
--	      LEFT OUTER JOIN
--	          (SELECT
--	                TJ_sub.kbn
--	              , TJ_sub.hosyousyo_no
--	              , CONVERT(VARCHAR,MIN(ISNULL(TS_KOJ.uri_date, TS.min_uri_date)),111) AS uri_date
--	           FROM
--	                jhs_sys.t_jiban TJ_sub
--	                     LEFT OUTER JOIN
--	                      (SELECT
--	                            ts.kbn
--	                          , ts.hosyousyo_no
--	                          , MIN(ts.uri_date) AS min_uri_date
--	                       FROM
--	                            [jhs_sys].[t_teibetu_seikyuu] ts
--	                                 LEFT OUTER JOIN m_syouhin ms
--	                                   ON ts.syouhin_cd = ms.syouhin_cd
--	                       WHERE
--	                            ms.hosyou_umu = 1
--	                        AND ts.bunrui_cd IN('100', '110', '115', '120')
--	                       GROUP BY
--	                            ts.kbn
--	                          , ts.hosyousyo_no
--	                      )TS
--	                       ON TJ_sub.kbn = TS.kbn
--	                      AND TJ_sub.hosyousyo_no = TS.hosyousyo_no
--	                     LEFT OUTER JOIN [jhs_sys].[t_teibetu_seikyuu] TS_KOJ
--	                       ON TJ_sub.kbn = TS_KOJ.kbn
--	                      AND TJ_sub.hosyousyo_no = TS_KOJ.hosyousyo_no
--	                      AND TS_KOJ.bunrui_cd = '130'
--	                      AND TS_KOJ.gamen_hyouji_no = 1
--	           GROUP BY
--	                TJ_sub.kbn
--	              , TJ_sub.hosyousyo_no
--	          )
--	           AS sub
--	        ON TJ.kbn = sub.kbn
--	       AND TJ.hosyousyo_no = sub.hosyousyo_no
--	      LEFT OUTER JOIN
--	          (SELECT
--	                TJ_sub.kbn
--	              , TJ_sub.hosyousyo_no
--	              , CONVERT(VARCHAR,TS_KOJ.uri_date,111) AS uri_date
--	           FROM
--	                jhs_sys.t_jiban TJ_sub
--	                     LEFT OUTER JOIN [jhs_sys].[t_teibetu_seikyuu] TS_KOJ
--	                       ON TJ_sub.kbn = TS_KOJ.kbn
--	                      AND TJ_sub.hosyousyo_no = TS_KOJ.hosyousyo_no
--	           WHERE TS_KOJ.bunrui_cd = '130'
--	            AND TS_KOJ.gamen_hyouji_no = 1					
--	          )					
--	           AS sub_koj					
--	        ON TJ.kbn = sub_koj.kbn					
--	       AND TJ.hosyousyo_no = sub_koj.hosyousyo_no					
--		WHERE				
--			 TJ.data_haki_syubetu IN('0', '99')			
--		 AND HJ.mihak_list_inji_umu = 1				
--		 AND TJ.hosyou_syouhin_umu = 1				
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
--		   AND ((				
--		        (TJ.koj_hantei_kekka_flg = 0				
--		            AND				
--		          sub.uri_date IS NOT NULL --�Y�����R�[�h�����݂������				
--		            AND				
--		         (				
--		           sub.uri_date <= @SYORI_BEFORE_MONTH_LAST_DATE --�o�b�`�������̑O���ȑO				
--	              AND					
--	            (TJ.hosyou_kaisi_date IS NULL OR CONVERT(VARCHAR,TJ.hosyou_kaisi_date,111) > sub.uri_date OR					
--		 (HK.hw_mae_hkn = 1 AND CONVERT(VARCHAR,TJ.hosyou_kaisi_date,111) <= sub.uri_date))	
--	           )		
--		        )	
--			    )
--		      OR	
--		      (	
--		        (TJ.koj_hantei_kekka_flg = 1	
--		          AND	
--	           sub_koj.uri_date IS NOT NULL --�Y�����R�[�h�����݂������		
--	            AND		
--	           (		
--	             sub_koj.uri_date <= @SYORI_BEFORE_MONTH_LAST_DATE --�o�b�`�������̑O���ȑO		
--	              AND		
--	            (TJ.hosyou_kaisi_date IS NULL OR CONVERT(VARCHAR,TJ.hosyou_kaisi_date,111) > sub_koj.uri_date OR 		
--		(HK.hw_mae_hkn = 1 AND CONVERT(VARCHAR,TJ.hosyou_kaisi_date,111) <= sub_koj.uri_date))	
--	           )		
--				 )
--		  ))		
--		                		
--	  /*************************			
--	  *���������A�G���[NO�Z�b�g			
--	  *************************/			
--	  SELECT			
--	      @ERR_NO = @@ERROR			
--	      ,@COUNT_HK_MAE_SINSEI = @@ROWCOUNT			
--				
--	  --�G���[�`�F�b�N			
--	  IF @ERR_NO <> 0			
--	  BEGIN			
--	      -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j			
--	      ROLLBACK TRANSACTION			
--	      RETURN @ERR_NO			
--	  END
--	  
--	/*************************************************************************
--	 * 2.�ۏ؏��Ǘ��f�[�^.�����n����ی�[�ی��\��] ���n���O�ی�,���n���O�ی��N����,���n���O�ی����{��
--	 **************************************************************************/
--	 UPDATE
--	     [jhs_sys].[t_hosyousyo_kanri]
--	  SET
--	       hw_ato_hkn = ISNULL(func.hw_hkn,0) --�Y���f�[�^����ꍇ�A���肵�����ʂ��Z�b�g�B�Y���f�[�^�Ȃ��ꍇ��0�B
--	     , hw_ato_hkn_date = func.hw_hkn_date
--	     , hw_ato_hkn_jissi_date = func.hw_hkn_jissi_date
--	     , syori_flg = 2
--	     , syori_datetime = @UPD_DATETIME
--	     , upd_login_user_id = @UPD_USER_ID
--	     , upd_datetime = @UPD_DATETIME
--	  FROM
--	       [jhs_sys].[t_hosyousyo_kanri] HK
--	            INNER JOIN [jhs_sys].[t_jiban] TJ
--	              ON HK.kbn = TJ.kbn
--	             AND HK.hosyousyo_no = TJ.hosyousyo_no
--	            LEFT OUTER JOIN [jhs_sys].[m_hosyousyo_hak_jyky] HJ
--	              ON TJ.hosyousyo_hak_jyky = HJ.hosyousyo_hak_jyky_no
--	            LEFT OUTER JOIN (
--	                 SELECT
--	                     br.kbn
--	                    , br.hosyousyo_no
--	                    , CASE WHEN sub.kbn IS NOT NULL
--	                      THEN 1
--	                      ELSE 0
--	                      END AS hw_hkn
--	                    , br.hanyou_date AS hw_hkn_date
--	                    , br.kanri_date AS hw_hkn_jissi_date
--	                FROM
--	                      jhs_sys.t_bukken_rireki br
--	                      ,
--	                       (SELECT
--	                             brsub.kbn
--	                           , brsub.hosyousyo_no
--	                           , ISNULL(MAX(brsub.nyuuryoku_no),0) AS max
--	                        FROM
--	                             jhs_sys.t_bukken_rireki brsub
--	                        WHERE
--	                             brsub.rireki_syubetu = '18'
--	                         AND brsub.rireki_no <> '0'
--	                         AND brsub.torikesi = '0'
--	                        GROUP BY
--	                             brsub.kbn
--	                           , brsub.hosyousyo_no
--	                       )									
--	                        sub									
--	                 WHERE									
--	                      br.kbn = sub.kbn									
--	                  AND br.hosyousyo_no = sub.hosyousyo_no									
--	                  AND br.nyuuryoku_no = sub.max									
--	                  AND br.rireki_syubetu = '18'									
--	                  AND br.rireki_no = '11'									
--	                ) AS func									
--	               ON HK.kbn = func.kbn									
--	               AND HK.hosyousyo_no = func.hosyousyo_no									
--	  WHERE TJ.data_haki_syubetu IN('0','99')									
--	   AND HJ.mihak_list_inji_umu = 1									
--	   AND (TJ.hosyou_kaisi_date IS NOT NULL AND 									
--		CONVERT(VARCHAR, TJ.hosyou_kaisi_date, 111) <= @SYORI_BEFORE_MONTH_LAST_DATE) 								
--										--�o�b�`�������̑O���ȑO
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
--	    							
--	  /*************************							
--	  *���������A�G���[NO�Z�b�g
--	  *************************/
--	  SELECT
--	      @ERR_NO = @@ERROR
--	      ,@COUNT_HK_ATO_SINSEI = @@ROWCOUNT
--	
--	  --�G���[�`�F�b�N
--	  IF @ERR_NO <> 0
--	  BEGIN
--	      -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j
--	      ROLLBACK TRANSACTION
--	      RETURN @ERR_NO
--	  END

/*************************************************************************	
 * 3.�ۏ؏��Ǘ��f�[�^.�����z�ʒm[�ی��\��] ���菤�i���{��,���菤�iFLG	
 **************************************************************************/					
	UPDATE				
		 [jhs_sys].[t_hosyousyo_kanri]			
	SET				
		 tokutei_syouhin_jissi_date =(			
			  CASE		
				   WHEN HK.tokutei_syouhin_jissi_date IS NULL	
					AND HK.tokutei_syouhin_tekiyou_yotei_jissi_date IS NOT NULL
				   THEN HK.tokutei_syouhin_tekiyou_yotei_jissi_date	
				   WHEN HK.tokutei_syouhin_jissi_date IS NOT NULL	
					AND HK.tokutei_syouhin_tekiyou_yotei_jissi_date IS NULL
				   THEN NULL	
				   ELSE HK.tokutei_syouhin_jissi_date	
			  END)		
	   , tokutei_syouhin_flg =(				
			  CASE		
				   WHEN HK.tokutei_syouhin_jissi_date IS NULL	
					AND HK.tokutei_syouhin_tekiyou_yotei_jissi_date IS NOT NULL
				   THEN ren.tokutei_hoken_sinsei_flg	
				   WHEN HK.tokutei_syouhin_jissi_date IS NOT NULL	
					AND HK.tokutei_syouhin_tekiyou_yotei_jissi_date IS NULL
				   THEN NULL	
				   ELSE HK.tokutei_syouhin_flg	
			  END)		
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
				   HAVING			
						max(ISNULL(NullIf(MS.tokutei_hoken_sinsei_flg, ''), 0)) <> 0	
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

/*************************************************************************	
 * 4.�ۏ؏��Ǘ��f�[�^.�ی��s�v�t���O�X�V	
 **************************************************************************/	
 UPDATE	
     [jhs_sys].[t_hosyousyo_kanri]	
  SET	
     hkn_huyou_flg = 1	
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
  WHERE	
	TJ.data_haki_syubetu not in (0,99)
         OR	
	ISNULL(HJ.mihak_list_inji_umu,0) = 0
	
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
	

-- �g�����U�N�V�������R�~�b�g
COMMIT TRANSACTION
RETURN @@ERROR 
  
END
GO
