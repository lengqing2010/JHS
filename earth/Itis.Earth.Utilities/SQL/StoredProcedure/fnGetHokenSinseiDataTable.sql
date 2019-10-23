-- =========================================================================================				
-- Description:	�ۏ؏��Ǘ��e�[�u���𑖍����āA���n���O(��)�̐\���^����f�[�^���擾����			
--				
--              ITEM�F				
--              ITEM�F				
--              ITEM�F				
--				  ���n���O�ی����{��
--				  ���n���O�ی��K�p�\����{��
--				  ���n����ی����{��
--				  ���n����ی��K�p�\����{��
--				  ���n����ی�������
--              ���������F				
--              ITEM�F				
-- =========================================================================================				
CREATE FUNCTION [jhs_sys].[fnGetHokenSinseiDataTable]				
(				
  @FlgHokenHantei INT                  --[�K�{] 1=�O�ی�/�\��,2=�O�ی�/���,3=��ی�/�\��,4=��ی�/���										
  ,@SYORI_DATE		DATETIME		   --��������						
)										
RETURNS @retData TABLE										
(										
  [kbn]						CHAR(1)     	--�敪
  ,[hosyousyo_no]				VARCHAR(10)	--�ԍ�			
  ,[hw_mae_hkn_jissi_date]			DATETIME    	--���n���O�ی����{��					
  ,[hw_mae_hkn_tekiyou_yotei_jissi_date]	DATETIME    	--���n���O�ی��K�p�\����{��									
  ,[hw_ato_hkn_jissi_date]			DATETIME    	--���n����ی����{��					
  ,[hw_ato_hkn_tekiyou_yotei_jissi_date]		DATETIME    	--���n����ی��K�p�\����{��									
  ,[hw_ato_hkn_torikesi_syubetsu]		INT 		--���n����ی�������							
  ,[tokutei_syouhin_jissi_date]			DATETIME    	--���菤�i���{��						
  ,[tokutei_syouhin_tekiyou_yotei_jissi_date] 	DATETIME	--���菤�i�K�p�\����{��									
  ,[tokutei_syouhin_flg]				INT 		--���菤�iFLG					
  ,[hosyou_kikan]				INT 		--�ۏ؊���					
)										
AS			
BEGIN			

  /****************************************			
  *�T�D�����n���O�ی��^�\���f�[�^���擾����			
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
		  OR (THK.hw_mae_hkn_jissi_date < THK.hw_mae_hkn_tekiyou_yotei_jissi_date							--�|�������Ȃ̂Ŏ��
			OR
		        (THK.hoken_kikan is not null and THK.hosyou_kikan is not null
			AND
			(THK.hosyou_kikan=10 and THK.hoken_kikan<>10)
				OR
			(THK.hosyou_kikan=20 and THK.hoken_kikan<>15 and TJ.hosyousyo_hak_date>='2013/2/1')	--��20�N�ۏ؂�15�N�ی��͊|�������Ȃ�
			)
		))
--		 AND (THK.hw_mae_hkn_tekiyou_yotei_jissi_date < TJ.hosyou_kaisi_date	
--		  OR TJ.hosyou_kaisi_date IS NULL)))	
		 AND ISNULL(TJ.hoken_kotei_flg,0) Not In (1,2)	

	END		

  /****************************************			
  *�U�D�����n���O�ی��^����f�[�^���擾����			
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
--		  OR THK.hw_mae_hkn_jissi_date < THK.hw_mae_hkn_tekiyou_yotei_jissi_date)							--�|�������Ȃ̂Ŏ��
		  OR (THK.hw_mae_hkn_jissi_date < THK.hw_mae_hkn_tekiyou_yotei_jissi_date							--�|�������Ȃ̂Ŏ��
			OR
		        (THK.hoken_kikan is not null and THK.hosyou_kikan is not null
			AND
			(THK.hosyou_kikan=10 and THK.hoken_kikan<>10)
				OR
			(THK.hosyou_kikan=20 and THK.hoken_kikan<>15 and TJ.hosyousyo_hak_date>='2013/2/1')	--��20�N�ۏ؂�15�N�ی��͊|�������Ȃ�
			)
		))
--		 AND (THK.hw_mae_hkn_tekiyou_yotei_jissi_date < TJ.hosyou_kaisi_date							
--		  OR TJ.hosyou_kaisi_date IS NULL)))							
		 AND ISNULL(TJ.hoken_kotei_flg,0) Not In (1,2)							
	END								

  /****************************************									
  *�V�D�����n����ی��^�\���f�[�^���擾����									
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
  *�W�D�����n����ی��^���			
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
		  OR THK.hw_ato_hkn_jissi_date < THK.hw_ato_hkn_tekiyou_yotei_jissi_date)							--�|�������Ȃ̂Ŏ��
--		 AND (TJ.hosyou_kaisi_date <= CONVERT(VARCHAR, cast((cast(year(@SYORI_DATE) AS VARCHAR(4)) + '-' + cast(month(@SYORI_DATE) AS VARCHAR(2)) + '-1') AS DATETIME) - 1, 111))))							
		 AND ISNULL(TJ.hoken_kotei_flg, 0) NOT In(2)							
	END								

  /****************************************									
  *�X�D���菤�i�ʒm									
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
  *�Z�D���菤�i�ʒm/���					
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

