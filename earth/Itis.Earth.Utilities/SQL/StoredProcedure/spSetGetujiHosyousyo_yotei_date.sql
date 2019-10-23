-- =====================================================	
-- Description:	保証書管理テーブル更新処理(月次)
--  1.保証書管理データ.お引渡し前保険[保険申請] 適用予定実施日のみ	
--  2.保証書管理データ.お引渡し後保険[保険申請] 適用予定実施日のみ	
--  3.保証書管理データ.お引渡し後保険取消種別	
--  4.保証書管理データ.特定商品適用予定実施日	
-- =====================================================	
CREATE PROCEDURE [jhs_sys].[spSetGetujiHosyousyo_yotei_date]	
   @COUNT_HK_MAE_SINSEI                 INT         OUTPUT --お引渡し前保険申請	
  ,@COUNT_HK_ATO_SINSEI                 INT         OUTPUT --お引渡し後保険申請	
  ,@COUNT_HK_ATO_TORIKESI1              INT         OUTPUT --お引渡し後保険取消・取消種別1--お引渡し後保険取消種別・保険不要フラグ初期化
  ,@COUNT_HK_ATO_TORIKESI2              INT         OUTPUT --お引渡し後保険取消・取消種別2
  ,@COUNT_HK_ATO_TORIKESI3              INT         OUTPUT --お引渡し後保険取消・取消種別3
  ,@COUNT_HK_ATO_TORIKESI4              INT         OUTPUT --お引渡し後保険取消・取消種別4
  ,@COUNT_HK_TOKUTEI                    INT         OUTPUT --特定商品適用予定実施日	
  ,@ERR_NO                              INT         OUTPUT --実行結果	
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

/*************************************************************************			
 * 1.保証書管理データ.お引渡し前保険[保険申請] 適用予定実施日のみ			
 **************************************************************************/			
 UPDATE			
     [jhs_sys].[t_hosyousyo_kanri]			
  SET			
     hw_mae_hkn_tekiyou_yotei_jissi_date =			
--		 CASE	
--			  WHEN TJ.koj_hantei_kekka_flg IS NULL --NULLは先に比較しておく
--			  THEN NULL							
--			  WHEN TJ.koj_hantei_kekka_flg = 0							
--			  THEN sub.uri_date							
--			  WHEN TJ.koj_hantei_kekka_flg = 1							
--			  THEN sub_koj.uri_date							
--			  ELSE NULL							
--		 END								
	 CASE WHEN TJ.koj_hantei_kekka_flg IS NULL --NULLは先に比較しておく									
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
--旧
	,CONVERT(VARCHAR,Case		
		When TJ_sub.koj_hkks_juri_date  is NULL Then TJ_sub.kairy_koj_sokuhou_tyk_date 	
		When TJ_sub.kairy_koj_sokuhou_tyk_date is NULL Then TJ_sub.koj_hkks_juri_date 	
		When TJ_sub.kairy_koj_sokuhou_tyk_date<=TJ_sub.koj_hkks_juri_date Then TJ_sub.kairy_koj_sokuhou_tyk_date 	
		Else TJ_sub.koj_hkks_juri_date End,111) AS koj_kanri_date	
--①パターン（保証書と同じで完工速報着日のみ）
--	,TJ_sub.kairy_koj_sokuhou_tyk_date  AS koj_kanri_date	
--②パターン（追加完工速報着日優先）
--	,isnull(TJ_sub.t_koj_sokuhou_tyk_date,TJ_sub.kairy_koj_sokuhou_tyk_date)  AS koj_kanri_date	
--③パターン（完工速報着日と追加完工速報着日の新しい日付）
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
	 AND NOT EXISTS --自社保険ではない						
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
--	          sub.uri_date IS NOT NULL --該当レコードが存在する条件	
	          ISNULL(ISNULL(sub.koj_kanri_date,sub.koj_uri_date),sub.uri_date) IS NOT NULL --該当レコードが存在する条件	
--	            AND	
--	         (	
--	           sub.uri_date <= @SYORI_BEFORE_MONTH_LAST_DATE --バッチ処理日の前月以前	
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
--	           sub_koj.uri_date IS NOT NULL --該当レコードが存在する条件	
	           ISNULL(sub.koj_kanri_date,sub.koj_uri_date) IS NOT NULL --該当レコードが存在する条件	
--	            AND	
--	           (	
--	             sub_koj.uri_date <= @SYORI_BEFORE_MONTH_LAST_DATE --バッチ処理日の前月以前	
--	              AND	
--	            (TJ.hosyou_kaisi_date IS NULL OR CONVERT(VARCHAR,TJ.hosyou_kaisi_date,111) > sub_koj.uri_date OR 	
--		(HK.hw_mae_hkn = 1 AND CONVERT(VARCHAR,TJ.hosyou_kaisi_date,111) <= sub_koj.uri_date))
--	           )	
	 )	
	  ))	
	                 	
  /*************************		
  *処理件数、エラーNOセット
  *************************/
  SELECT
      @ERR_NO = @@ERROR
      ,@COUNT_HK_MAE_SINSEI = @@ROWCOUNT

  --エラーチェック
  IF @ERR_NO <> 0
  BEGIN
      -- トランザクションをロールバック（キャンセル）
      ROLLBACK TRANSACTION
      RETURN @ERR_NO
  END
  
/*************************************************************************
 * 2.保証書管理データ.お引渡し後保険[保険申請] 適用予定実施日のみ
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
									 --バッチ処理日の前月以前
OR									
          (HK.hw_mae_hkn_tekiyou_yotei_jissi_date IS NOT NULL AND 									
	CONVERT(VARCHAR, HK.hw_mae_hkn_tekiyou_yotei_jissi_date, 111) < '2013/02/01'))								
	 AND NOT EXISTS --自社保険ではない								
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
  	
/*************************************************************************	
 * 3.保証書管理データ.お引渡し後保険取消種別	
 **************************************************************************/	
--	 --引渡し後保険・取消1
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
--	   )		
--			
--	  /*************************		
--	  *処理件数、エラーNOセット		
--	  *************************/		
--	  SELECT		
--	      @ERR_NO = @@ERROR		
--	      ,@COUNT_HK_ATO_TORIKESI1 = @@ROWCOUNT		
--			
--	  --エラーチェック		
--	  IF @ERR_NO <> 0		
--	  BEGIN		
--	      -- トランザクションをロールバック（キャンセル）		
--	      ROLLBACK TRANSACTION		
--	      RETURN @ERR_NO		
--	  END
--	  
--	 --引渡し後保険・取消2
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
--		CONVERT(VARCHAR, TJ.hosyousyo_hak_date, 111) >= @SYORI_DATE)) --バッチ処理日以降
--		
--	  /*************************	
--	  *処理件数、エラーNOセット	
--	  *************************/	
--	  SELECT	
--	      @ERR_NO = @@ERROR	
--	      ,@COUNT_HK_ATO_TORIKESI2 = @@ROWCOUNT	
--		
--	  --エラーチェック	
--	  IF @ERR_NO <> 0	
--	  BEGIN
--	      -- トランザクションをロールバック（キャンセル）
--	      ROLLBACK TRANSACTION
--	      RETURN @ERR_NO
--	  END
--	  
--	 --引渡し後保険・取消3
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
--	  *処理件数、エラーNOセット
--	  *************************/
--	  SELECT
--	      @ERR_NO = @@ERROR
--	      ,@COUNT_HK_ATO_TORIKESI3 = @@ROWCOUNT
--	
--	  --エラーチェック
--	  IF @ERR_NO <> 0
--	  BEGIN
--	      -- トランザクションをロールバック（キャンセル）
--	      ROLLBACK TRANSACTION
--	      RETURN @ERR_NO
--	  END
--	 
--	 --引渡し後保険・取消4
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
--	  *処理件数、エラーNOセット
--	  *************************/
--	  SELECT
--	      @ERR_NO = @@ERROR
--	      ,@COUNT_HK_ATO_TORIKESI4 = @@ROWCOUNT
	
 --引渡し後保険・取消種別には全て0をセット	
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
  *処理件数、エラーNOセット	
  *************************/	
  SELECT	
      @ERR_NO = @@ERROR	
      ,@COUNT_HK_ATO_TORIKESI1 = @@ROWCOUNT	

  --エラーチェック	
  IF @ERR_NO <> 0		
  BEGIN		
      -- トランザクションをロールバック（キャンセル）		
      ROLLBACK TRANSACTION		
      RETURN @ERR_NO		
  END		

/*************************************************************************		
 * 4.保証書管理データ.特定商品適用予定実施日		
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

-- トランザクションをコミット
COMMIT TRANSACTION
RETURN @@ERROR 
  
END
GO
