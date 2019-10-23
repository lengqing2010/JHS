-- =====================================================								
-- Description:	保証書管理テーブル更新処理(月次)							
--  1.保証書管理データ.お引渡し前保険[保険申請] 引渡し前保険,引渡し前保険年月日,引渡し前保険実施日								
--  2.保証書管理データ.お引渡し後保険[保険申請] 引渡し後保険,引渡し後保険年月日,引渡し後保険実施日								
--  3.保証書管理データ.増改築通知[保険申請] 特定商品実施日,特定商品FLG								
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

	DECLARE @UPD_USER_ID               VARCHAR(30)  --更新ログインユーザーID
	DECLARE @UPD_DATETIME              DATETIME     --更新日時
	DECLARE @SYORI_DATE                VARCHAR(10)  --処理日
	DECLARE @SYORI_BEFORE_MONTH_LAST_DATE     VARCHAR(10)  --処理年月日_前月末日

	--初期化
  SET @UPD_USER_ID = 'system'	
  SET @UPD_DATETIME = GETDATE()	
  	
  --処理日	
  SET @SYORI_DATE = CONVERT(VARCHAR, @UPD_DATETIME, 111)	

  --前月末日	
  SELECT @SYORI_BEFORE_MONTH_LAST_DATE = CONVERT(VARCHAR,cast((cast(year(@SYORI_DATE) AS VARCHAR(4))	
	+'-'+cast(month(@SYORI_DATE) AS VARCHAR(2))+'-1') AS DATETIME)-1,111)

--	spSetGetujiHosyousyo_setupで前保険・後保険は全行更新しているので特定商品の更新のみでOK（2013/2/27）
--	/*************************************************************************
--	 * 1.保証書管理データ.お引渡し前保険[保険申請] 引渡し前保険,引渡し前保険年月日,引渡し前保険実施日
--	 **************************************************************************/
--	 UPDATE
--	     [jhs_sys].[t_hosyousyo_kanri]
--	  SET
--	     hw_mae_hkn = ISNULL(func.hw_hkn, 0) --該当データある場合、判定した結果をセット。該当データない場合は0。
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
--		 AND NOT EXISTS --自社保険ではない				
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
--		          sub.uri_date IS NOT NULL --該当レコードが存在する条件				
--		            AND				
--		         (				
--		           sub.uri_date <= @SYORI_BEFORE_MONTH_LAST_DATE --バッチ処理日の前月以前				
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
--	           sub_koj.uri_date IS NOT NULL --該当レコードが存在する条件		
--	            AND		
--	           (		
--	             sub_koj.uri_date <= @SYORI_BEFORE_MONTH_LAST_DATE --バッチ処理日の前月以前		
--	              AND		
--	            (TJ.hosyou_kaisi_date IS NULL OR CONVERT(VARCHAR,TJ.hosyou_kaisi_date,111) > sub_koj.uri_date OR 		
--		(HK.hw_mae_hkn = 1 AND CONVERT(VARCHAR,TJ.hosyou_kaisi_date,111) <= sub_koj.uri_date))	
--	           )		
--				 )
--		  ))		
--		                		
--	  /*************************			
--	  *処理件数、エラーNOセット			
--	  *************************/			
--	  SELECT			
--	      @ERR_NO = @@ERROR			
--	      ,@COUNT_HK_MAE_SINSEI = @@ROWCOUNT			
--				
--	  --エラーチェック			
--	  IF @ERR_NO <> 0			
--	  BEGIN			
--	      -- トランザクションをロールバック（キャンセル）			
--	      ROLLBACK TRANSACTION			
--	      RETURN @ERR_NO			
--	  END
--	  
--	/*************************************************************************
--	 * 2.保証書管理データ.お引渡し後保険[保険申請] 引渡し前保険,引渡し前保険年月日,引渡し前保険実施日
--	 **************************************************************************/
--	 UPDATE
--	     [jhs_sys].[t_hosyousyo_kanri]
--	  SET
--	       hw_ato_hkn = ISNULL(func.hw_hkn,0) --該当データある場合、判定した結果をセット。該当データない場合は0。
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
--										--バッチ処理日の前月以前
--		 AND NOT EXISTS --自社保険ではない						
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
--	  *処理件数、エラーNOセット
--	  *************************/
--	  SELECT
--	      @ERR_NO = @@ERROR
--	      ,@COUNT_HK_ATO_SINSEI = @@ROWCOUNT
--	
--	  --エラーチェック
--	  IF @ERR_NO <> 0
--	  BEGIN
--	      -- トランザクションをロールバック（キャンセル）
--	      ROLLBACK TRANSACTION
--	      RETURN @ERR_NO
--	  END

/*************************************************************************	
 * 3.保証書管理データ.増改築通知[保険申請] 特定商品実施日,特定商品FLG	
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
  *処理件数、エラーNOセット							
  *************************/							
  SELECT
      @ERR_NO = @@ERROR
      ,@COUNT_HK_TOKUTEI = @@ROWCOUNT

  --エラーチェック
  IF @ERR_NO <> 0
  BEGIN
      -- トランザクションをロールバック（キャンセル）
      ROLLBACK TRANSACTION
      RETURN @ERR_NO
  END

/*************************************************************************	
 * 4.保証書管理データ.保険不要フラグ更新	
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
  *処理件数、エラーNOセット	
  *************************/	
  SELECT	
      @ERR_NO = @@ERROR	
      ,@COUNT_HK_ATO_SINSEI = @@ROWCOUNT	
	
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
