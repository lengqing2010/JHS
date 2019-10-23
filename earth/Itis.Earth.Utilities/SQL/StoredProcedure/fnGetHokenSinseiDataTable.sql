-- =========================================================================================				
-- Description:	保証書管理テーブルを走査して、引渡し前(後)の申請／取消データを取得する			
--				
--              ITEM：				
--              ITEM：				
--              ITEM：				
--				  引渡し前保険実施日
--				  引渡し前保険適用予定実施日
--				  引渡し後保険実施日
--				  引渡し後保険適用予定実施日
--				  引渡し後保険取消種別
--              結合条件：				
--              ITEM：				
-- =========================================================================================				
CREATE FUNCTION [jhs_sys].[fnGetHokenSinseiDataTable]				
(				
  @FlgHokenHantei INT                  --[必須] 1=前保険/申請,2=前保険/取消,3=後保険/申請,4=後保険/取消										
  ,@SYORI_DATE		DATETIME		   --処理日時						
)										
RETURNS @retData TABLE										
(										
  [kbn]						CHAR(1)     	--区分
  ,[hosyousyo_no]				VARCHAR(10)	--番号			
  ,[hw_mae_hkn_jissi_date]			DATETIME    	--引渡し前保険実施日					
  ,[hw_mae_hkn_tekiyou_yotei_jissi_date]	DATETIME    	--引渡し前保険適用予定実施日									
  ,[hw_ato_hkn_jissi_date]			DATETIME    	--引渡し後保険実施日					
  ,[hw_ato_hkn_tekiyou_yotei_jissi_date]		DATETIME    	--引渡し後保険適用予定実施日									
  ,[hw_ato_hkn_torikesi_syubetsu]		INT 		--引渡し後保険取消種別							
  ,[tokutei_syouhin_jissi_date]			DATETIME    	--特定商品実施日						
  ,[tokutei_syouhin_tekiyou_yotei_jissi_date] 	DATETIME	--特定商品適用予定実施日									
  ,[tokutei_syouhin_flg]				INT 		--特定商品FLG					
  ,[hosyou_kikan]				INT 		--保証期間					
)										
AS			
BEGIN			

  /****************************************			
  *Ⅰ．お引渡し前保険／申請データを取得する			
  *****************************************/			
	IF @FlgHokenHantei = 1		
	BEGIN		

		INSERT INTO	
			@retData
		SELECT	
			 THK.kbn
		   , THK.hosyousyo_no	
		   , THK.hw_mae_hkn_jissi_date	
		   , THK.hw_mae_hkn_tekiyou_yotei_jissi_date	
		   , THK.hw_ato_hkn_jissi_date			
		   , THK.hw_ato_hkn_tekiyou_yotei_jissi_date			
		   , THK.hw_ato_hkn_torikesi_syubetsu			
		   , THK.tokutei_syouhin_jissi_date			
		   , THK.tokutei_syouhin_tekiyou_yotei_jissi_date			
		   , THK.tokutei_syouhin_flg			
		   , isnull(THK.hosyou_kikan,TJ.hosyou_kikan) hosyou_kikan			
		FROM			
			 t_hosyousyo_kanri THK		
				  INNER JOIN t_jiban TJ	
					ON THK.kbn = TJ.kbn
				   AND THK.hosyousyo_no = TJ.hosyousyo_no	
		WHERE			
			 ((THK.hw_mae_hkn_jissi_date IS NULL		
		 AND THK.hw_mae_hkn_tekiyou_yotei_jissi_date IS NOT NULL)			
--		  OR ((THK.hw_mae_hkn_jissi_date < THK.hw_mae_hkn_tekiyou_yotei_jissi_date)			
--		  OR THK.hw_mae_hkn_jissi_date < THK.hw_mae_hkn_tekiyou_yotei_jissi_date)			
		  OR (THK.hw_mae_hkn_jissi_date < THK.hw_mae_hkn_tekiyou_yotei_jissi_date							--掛け直しなので取消
			OR
		        (THK.hoken_kikan is not null and THK.hosyou_kikan is not null
			AND
			(THK.hosyou_kikan=10 and THK.hoken_kikan<>10)
				OR
			(THK.hosyou_kikan=20 and THK.hoken_kikan<>15 and TJ.hosyousyo_hak_date>='2013/2/1')	--旧20年保証は15年保険は掛け直さない
			)
		))
--		 AND (THK.hw_mae_hkn_tekiyou_yotei_jissi_date < TJ.hosyou_kaisi_date	
--		  OR TJ.hosyou_kaisi_date IS NULL)))	
		 AND ISNULL(TJ.hoken_kotei_flg,0) Not In (1,2)	

	END		

  /****************************************			
  *Ⅱ．お引渡し前保険／取消データを取得する			
  *****************************************/			
	IF @FlgHokenHantei = 2		
	BEGIN		

		INSERT INTO	
			@retData
		SELECT	
			 THK.kbn
		   , THK.hosyousyo_no			
		   , THK.hw_mae_hkn_jissi_date			
		   , THK.hw_mae_hkn_tekiyou_yotei_jissi_date			
		   , THK.hw_ato_hkn_jissi_date			
		   , THK.hw_ato_hkn_tekiyou_yotei_jissi_date			
		   , THK.hw_ato_hkn_torikesi_syubetsu			
		   , THK.tokutei_syouhin_jissi_date			
		   , THK.tokutei_syouhin_tekiyou_yotei_jissi_date			
		   , THK.tokutei_syouhin_flg			
		   , isnull(THK.hosyou_kikan,TJ.hosyou_kikan) hosyou_kikan			
		FROM			
			 t_hosyousyo_kanri THK		
				  INNER JOIN t_jiban TJ	
					ON THK.kbn = TJ.kbn
				   AND THK.hosyousyo_no = TJ.hosyousyo_no	
		WHERE			
			 ((THK.hw_mae_hkn_jissi_date IS NOT NULL		
		 AND THK.hw_mae_hkn_tekiyou_yotei_jissi_date IS NULL)							
--		  OR ((THK.hw_mae_hkn_jissi_date < THK.hw_mae_hkn_tekiyou_yotei_jissi_date)							
--		  OR THK.hw_mae_hkn_jissi_date < THK.hw_mae_hkn_tekiyou_yotei_jissi_date)							--掛け直しなので取消
		  OR (THK.hw_mae_hkn_jissi_date < THK.hw_mae_hkn_tekiyou_yotei_jissi_date							--掛け直しなので取消
			OR
		        (THK.hoken_kikan is not null and THK.hosyou_kikan is not null
			AND
			(THK.hosyou_kikan=10 and THK.hoken_kikan<>10)
				OR
			(THK.hosyou_kikan=20 and THK.hoken_kikan<>15 and TJ.hosyousyo_hak_date>='2013/2/1')	--旧20年保証は15年保険は掛け直さない
			)
		))
--		 AND (THK.hw_mae_hkn_tekiyou_yotei_jissi_date < TJ.hosyou_kaisi_date							
--		  OR TJ.hosyou_kaisi_date IS NULL)))							
		 AND ISNULL(TJ.hoken_kotei_flg,0) Not In (1,2)							
	END								

  /****************************************									
  *Ⅲ．お引渡し後保険／申請データを取得する									
  *****************************************/									
	IF @FlgHokenHantei = 3								
	BEGIN								

		INSERT INTO							
			@retData						
		SELECT			
			 THK.kbn		
		   , THK.hosyousyo_no			
		   , THK.hw_mae_hkn_jissi_date			
		   , THK.hw_mae_hkn_tekiyou_yotei_jissi_date			
		   , THK.hw_ato_hkn_jissi_date			
		   , THK.hw_ato_hkn_tekiyou_yotei_jissi_date			
		   , THK.hw_ato_hkn_torikesi_syubetsu			
		   , THK.tokutei_syouhin_jissi_date			
		   , THK.tokutei_syouhin_tekiyou_yotei_jissi_date			
		   , THK.tokutei_syouhin_flg			
		   , isnull(THK.hosyou_kikan,TJ.hosyou_kikan) hosyou_kikan			
		FROM			
			 t_hosyousyo_kanri THK		
				  INNER JOIN t_jiban TJ	
					ON THK.kbn = TJ.kbn
				   AND THK.hosyousyo_no = TJ.hosyousyo_no	
		WHERE	
			 ((THK.hw_ato_hkn_jissi_date IS NULL
		 AND THK.hw_ato_hkn_tekiyou_yotei_jissi_date IS NOT NULL)	
--		  OR ((THK.hw_ato_hkn_jissi_date < THK.hw_ato_hkn_tekiyou_yotei_jissi_date)	
		  OR THK.hw_ato_hkn_jissi_date < THK.hw_ato_hkn_tekiyou_yotei_jissi_date)	
--		 AND (TJ.hosyou_kaisi_date <= CONVERT(VARCHAR, cast((cast(year(@SYORI_DATE) AS VARCHAR(4)) + '-' + cast(month(@SYORI_DATE) AS VARCHAR(2)) + '-1') AS DATETIME) - 1, 111))))	
		 AND ISNULL(TJ.hoken_kotei_flg, 0) NOT In(2)	
	END		

  /****************************************			
  *Ⅳ．お引渡し後保険／取消			
  *****************************************/			
	IF @FlgHokenHantei = 4		
	BEGIN		

		INSERT INTO	
			@retData		
		SELECT			
			 THK.kbn		
		   , THK.hosyousyo_no			
		   , THK.hw_mae_hkn_jissi_date			
		   , THK.hw_mae_hkn_tekiyou_yotei_jissi_date			
		   , THK.hw_ato_hkn_jissi_date			
		   , THK.hw_ato_hkn_tekiyou_yotei_jissi_date			
		   , THK.hw_ato_hkn_torikesi_syubetsu			
		   , THK.tokutei_syouhin_jissi_date			
		   , THK.tokutei_syouhin_tekiyou_yotei_jissi_date			
		   , THK.tokutei_syouhin_flg			
		   , isnull(THK.hosyou_kikan,TJ.hosyou_kikan) hosyou_kikan			
		FROM			
			 t_hosyousyo_kanri THK		
				  INNER JOIN t_jiban TJ	
					ON THK.kbn = TJ.kbn
				   AND THK.hosyousyo_no = TJ.hosyousyo_no					
		WHERE							
			 ((THK.hw_ato_hkn_jissi_date IS NOT NULL						
		 AND THK.hw_ato_hkn_tekiyou_yotei_jissi_date IS NULL)							
--		  OR ((THK.hw_ato_hkn_jissi_date < THK.hw_ato_hkn_tekiyou_yotei_jissi_date)							
		  OR THK.hw_ato_hkn_jissi_date < THK.hw_ato_hkn_tekiyou_yotei_jissi_date)							--掛け直しなので取消
--		 AND (TJ.hosyou_kaisi_date <= CONVERT(VARCHAR, cast((cast(year(@SYORI_DATE) AS VARCHAR(4)) + '-' + cast(month(@SYORI_DATE) AS VARCHAR(2)) + '-1') AS DATETIME) - 1, 111))))							
		 AND ISNULL(TJ.hoken_kotei_flg, 0) NOT In(2)							
	END								

  /****************************************									
  *Ⅴ．特定商品通知									
  *****************************************/									
	IF @FlgHokenHantei = 5								
	BEGIN								

		INSERT INTO		
			@retData	
		SELECT		
			 THK.kbn	
		   , THK.hosyousyo_no		
		   , THK.hw_mae_hkn_jissi_date		
		   , THK.hw_mae_hkn_tekiyou_yotei_jissi_date		
		   , THK.hw_ato_hkn_jissi_date		
		   , THK.hw_ato_hkn_tekiyou_yotei_jissi_date		
		   , THK.hw_ato_hkn_torikesi_syubetsu		
		   , THK.tokutei_syouhin_jissi_date		
		   , THK.tokutei_syouhin_tekiyou_yotei_jissi_date		
		   , THK.tokutei_syouhin_flg		
		   , isnull(THK.hosyou_kikan,TJ.hosyou_kikan) hosyou_kikan			
		FROM		
			 t_hosyousyo_kanri THK	
				  INNER JOIN t_jiban TJ
					ON THK.kbn = TJ.kbn
				   AND THK.hosyousyo_no = TJ.hosyousyo_no	
		WHERE			
			 THK.tokutei_syouhin_jissi_date is null		
		 AND THK.tokutei_syouhin_tekiyou_yotei_jissi_date is NOT null			

	END				

  /****************************************					
  *六．特定商品通知/取消					
  *****************************************/					
	IF @FlgHokenHantei = 6				
	BEGIN				

		INSERT INTO			
			@retData		
		SELECT			
			 THK.kbn		
		   , THK.hosyousyo_no			
		   , THK.hw_mae_hkn_jissi_date			
		   , THK.hw_mae_hkn_tekiyou_yotei_jissi_date			
		   , THK.hw_ato_hkn_jissi_date			
		   , THK.hw_ato_hkn_tekiyou_yotei_jissi_date			
		   , THK.hw_ato_hkn_torikesi_syubetsu			
		   , THK.tokutei_syouhin_jissi_date			
		   , THK.tokutei_syouhin_tekiyou_yotei_jissi_date			
		   , THK.tokutei_syouhin_flg			
		   , isnull(THK.hosyou_kikan,TJ.hosyou_kikan) hosyou_kikan			
		FROM			
			 t_hosyousyo_kanri THK		
				  INNER JOIN t_jiban TJ	
					ON THK.kbn = TJ.kbn
				   AND THK.hosyousyo_no = TJ.hosyousyo_no	
		WHERE	
			 THK.tokutei_syouhin_jissi_date is NOT null
		 AND THK.tokutei_syouhin_tekiyou_yotei_jissi_date is null	

	END		

	RETURN		

END			

