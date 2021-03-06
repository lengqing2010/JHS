-- =============================================
-- Author:		<TIS>
-- Create date: <2012/03/22>
-- Description:	<入金状況の入金無し内容を細分化する>
-- =============================================
CREATE FUNCTION [jhs_sys].[fnGetNyuukinNasiInfo] (@retType int,@kbn CHAR(1),@bangou VARCHAR(10),@today datetime)
RETURNS VARCHAR(5)
AS
BEGIN
	DECLARE @NyuukinKknnJkn CHAR(1)			--入金確認条件
	DECLARE @NyuukinJyky VARCHAR(5)			--入金状況
	DECLARE @KameitenCd VARCHAR(5)			--加盟店コード
	DECLARE @BunruiCd VARCHAR(3)			--分類コード
	DECLARE @GamenHyoujiNo int				--画面表示NO
	DECLARE @SyouhinCd VARCHAR(8)			--商品コード
	DECLARE @SeikyuuSakiCd VARCHAR(5)		--請求先コード
	DECLARE @SeikyuuSakiBrc VARCHAR(2)		--請求先枝番
	DECLARE @SeikyuuSakiKbn VARCHAR(1)		--請求先区分
	DECLARE @SeikyuusyoHakDate Datetime		--請求書発行日
	DECLARE @tableType          int
	DECLARE @JdgFlg	VARCHAR(4)				-- 入金無し判断フラグ

	--初期化
	SET @NyuukinKknnJkn = NULL
	SET @NyuukinJyky = 2
	SET @BunruiCd = NULL
	SET @GamenHyoujiNo = 0
	SET @SyouhinCd = NULL
	SET @SeikyuuSakiCd = NULL
	SET @SeikyuuSakiBrc = NULL
	SET @SeikyuuSakiKbn = NULL
	SET @tableType = 1;         --テーブルタイプ(邸別請求：１)
	SET @JdgFlg = NULL
	SET @today = CONVERT(VARCHAR, @today, 111)

	--区分,番号より加盟店Mの入金確認条件を取得する
	IF (@kbn IS NOT NULL AND @bangou IS NOT NULL)
	BEGIN
	  SELECT
		@KameitenCd = TJ.kameiten_cd
		,@NyuukinKknnJkn = MK.nyuukin_kakunin_jyouken
	  FROM jhs_sys.t_jiban TJ
		LEFT OUTER JOIN jhs_sys.m_kameiten MK
		  ON TJ.kameiten_cd = MK.kameiten_cd
      WHERE TJ.kbn = @kbn
        AND TJ.hosyousyo_no = @bangou
	END

	--回収1種別1、口振OKフラグ、入金予定日格納用テーブル
	DECLARE @Materials TABLE
	(pKey VARCHAR(4), Kaisyuu INT, Okflg int, NyuukinDate datetime)

	--入金確認条件が0(通常通り用入金確認)の場合
	--入金確認条件が4(要入金確認(自動引落))の場合
	--入金確認条件がNullもしくは空文字の場合
	--入金確認条件が0～6以外の場合
	IF (@NyuukinKknnJkn = 0 
		OR @NyuukinKknnJkn = 4
		OR @NyuukinKknnJkn not between '0' and '6'
		OR NullIf(@NyuukinKknnJkn,'') is null)
	BEGIN
		DECLARE TeibetuTable CURSOR FOR
		SELECT
			 TTS.bunrui_cd
		   , TTS.gamen_hyouji_no
		   , TTS.syouhin_cd
		   , TTS.seikyuu_saki_cd
		   , TTS.seikyuu_saki_brc
		   , TTS.seikyuu_saki_kbn
		   , TTS.seikyuusyo_hak_date
		FROM
			 jhs_sys.t_jiban TJ
				  LEFT OUTER JOIN
					  (SELECT
							TS.kbn
						  , TS.hosyousyo_no
						  , TS.bunrui_cd
						  , TS.gamen_hyouji_no
						  , TS.syouhin_cd
						  , TS.seikyuu_saki_cd
						  , TS.seikyuu_saki_brc
						  , TS.seikyuu_saki_kbn
						  , TS.seikyuusyo_hak_date
						  ,
							CASE
								 WHEN NullIf(TS.syouhizei_gaku, '') is null
								 THEN (TS.uri_gaku +(TS.uri_gaku * MS.zeiritu))
								 ELSE (TS.uri_gaku + TS.syouhizei_gaku)
							END seikyuu_gaku
						  , ISNULL(TN.zeikomi_nyuukin_gaku, 0) nyuukin_gaku
						  , ISNULL(TN.zeikomi_henkin_gaku, 0) henkin_gaku
					   FROM
							jhs_sys.t_jiban TJJ
								 INNER JOIN jhs_sys.t_teibetu_seikyuu TS
								   ON TJJ.kbn=TS.kbn
								  AND TJJ.hosyousyo_no=TS.hosyousyo_no
								 LEFT OUTER JOIN jhs_sys.m_syouhizei MS
								   ON TS.zei_kbn = MS.zei_kbn
								 LEFT OUTER JOIN jhs_sys.t_teibetu_nyuukin TN
								   ON TS.kbn = TN.kbn
								  AND TS.hosyousyo_no = TN.hosyousyo_no
								  AND TS.bunrui_cd = TN.bunrui_cd
								  AND TS.gamen_hyouji_no = TN.gamen_hyouji_no
					   WHERE
							1 = 1
						AND (TS.bunrui_cd = '100'
						 OR TS.bunrui_cd = '110'
						 OR TS.bunrui_cd = '115'
						 OR TS.bunrui_cd = '120'
						 OR (TS.bunrui_cd = '130'
						AND isnull(TJJ.koj_gaisya_seikyuu_umu, 0) =0)
						 OR (TS.bunrui_cd = '140'
						AND isnull(TJJ.t_koj_kaisya_seikyuu_umu, 0) =0)
						 OR TS.bunrui_cd = '180')
						AND TS.seikyuu_umu = '1'
					  )
					   TTS
					ON TJ.kbn = TTS.kbn
				   AND TJ.hosyousyo_no = TTS.hosyousyo_no
		WHERE
			 TJ.kbn = @kbn
		 AND TJ.hosyousyo_no = @bangou
		 AND (ISNULL(TTS.seikyuu_gaku, 0) - ISNULL(TTS.nyuukin_gaku, 0) + ISNULL(TTS.henkin_gaku, 0)) > '0';

		/**********************
		* 集めた邸別情報カーソルから必要材料を一時テーブルに格納
		***********************/
		OPEN TeibetuTable
			
		--初めの一件を取得
		FETCH NEXT FROM TeibetuTable
		INTO @BunruiCd,@GamenHyoujiNo,@SyouhinCd,@SeikyuuSakiCd,@SeikyuuSakiBrc,@SeikyuuSakiKbn,@SeikyuusyoHakDate

		--カーソル内の情報を走査
		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF @seikyuuSakiCd IS NULL OR @seikyuuSakiBrc IS NULL OR @seikyuuSakiKbn IS NULL
				--変更請求先が無い場合は、テーブル関数から取得
				SELECT 
					@seikyuuSakiCd = [seikyuu_saki_cd]
					, @seikyuuSakiBrc = [seikyuu_saki_brc]
					, @seikyuuSakiKbn = [seikyuu_saki_kbn]
				FROM
					[jhs_sys].[fnGetSeikyuuSakiKeyDataTable](@tableType, @kbn, @bangou, @BunruiCd, @SyouhinCd, @kameitenCd);

			--一時テーブルに格納
			INSERT INTO
				 @Materials
			(
				 pKey
			   , Kaisyuu
			   , Okflg
			   , NyuukinDate
			)
			SELECT
				 @BunruiCd+CONVERT
			(
				 varchar
			   , @GamenHyoujiNo
			)
			   , MSS.kaisyuu1_syubetu1
			   , MSS.koufuri_ok_flg
			   , [jhs_sys].[fnGetNyuukinYoteiDate]
			(
				 @seikyuuSakiCd
			   , @seikyuuSakiBrc
			   , @seikyuuSakiKbn
			   , @SeikyuusyoHakDate
			)
			FROM
				 jhs_sys.m_seikyuu_saki MSS
			WHERE
				 MSS.seikyuu_saki_cd = @seikyuuSakiCd
			 AND MSS.seikyuu_saki_brc = @seikyuuSakiBrc
			 AND MSS.seikyuu_saki_kbn = @seikyuuSakiKbn
			
			--次の一件を取得
			FETCH NEXT FROM TeibetuTable
			INTO @BunruiCd,@GamenHyoujiNo,@SyouhinCd,@SeikyuuSakiCd,@SeikyuuSakiBrc,@SeikyuuSakiKbn,@SeikyuusyoHakDate				
		END

		--終了処理
		CLOSE TeibetuTable
		DEALLOCATE TeibetuTable

	END

	--入金確認条件が1(入金確認不要(地盤))の場合
	IF (@NyuukinKknnJkn = 1)
	BEGIN
		DECLARE TeibetuTable CURSOR FOR
		SELECT
			 TTS.bunrui_cd
		   , TTS.gamen_hyouji_no
		   , TTS.syouhin_cd
		   , TTS.seikyuu_saki_cd
		   , TTS.seikyuu_saki_brc
		   , TTS.seikyuu_saki_kbn
		   , TTS.seikyuusyo_hak_date
		FROM
			 jhs_sys.t_jiban TJ
				  LEFT OUTER JOIN
					  (SELECT
							TS.kbn
						  , TS.hosyousyo_no
						  , TS.bunrui_cd
						  , TS.gamen_hyouji_no
						  , TS.syouhin_cd
						  , TS.seikyuu_saki_cd
						  , TS.seikyuu_saki_brc
						  , TS.seikyuu_saki_kbn
						  , TS.seikyuusyo_hak_date
						  , 
							 CASE
								  WHEN NullIf(TS.syouhizei_gaku, '') is null
								  THEN (TS.uri_gaku +(TS.uri_gaku * MS.zeiritu))
								  ELSE (TS.uri_gaku + TS.syouhizei_gaku)
							 END seikyuu_gaku
						  , ISNULL(TN.zeikomi_nyuukin_gaku, 0) nyuukin_gaku
						  , ISNULL(TN.zeikomi_henkin_gaku, 0) henkin_gaku
					   FROM
							jhs_sys.t_jiban TJJ
								 INNER JOIN jhs_sys.t_teibetu_seikyuu TS
								   ON TJJ.kbn=TS.kbn
								  AND TJJ.hosyousyo_no=TS.hosyousyo_no
								 LEFT OUTER JOIN jhs_sys.m_syouhizei MS
								   ON TS.zei_kbn = MS.zei_kbn
								 LEFT OUTER JOIN jhs_sys.t_teibetu_nyuukin TN
								   ON TS.kbn = TN.kbn
								  AND TS.hosyousyo_no = TN.hosyousyo_no
								  AND TS.bunrui_cd = TN.bunrui_cd
								  AND TS.gamen_hyouji_no = TN.gamen_hyouji_no
					   WHERE
							1 = 1
						AND ((TS.bunrui_cd = '130'
						AND isnull(TJJ.koj_gaisya_seikyuu_umu, 0) =0)
						 OR (TS.bunrui_cd = '140'
						AND isnull(TJJ.t_koj_kaisya_seikyuu_umu, 0) =0))
						AND TS.seikyuu_umu = '1'
					  )
					   TTS
					ON TJ.kbn = TTS.kbn
				   AND TJ.hosyousyo_no = TTS.hosyousyo_no
		WHERE
			 TJ.kbn = @kbn
		 AND TJ.hosyousyo_no = @bangou
		 AND (ISNULL(TTS.seikyuu_gaku, 0) - ISNULL(TTS.nyuukin_gaku, 0) + ISNULL(TTS.henkin_gaku, 0)) > '0';

		/**********************
		* 集めた邸別情報カーソルから必要材料を一時テーブルに格納
		***********************/
		OPEN TeibetuTable
			
		--初めの一件を取得
		FETCH NEXT FROM TeibetuTable
		INTO @BunruiCd,@GamenHyoujiNo,@SyouhinCd,@SeikyuuSakiCd,@SeikyuuSakiBrc,@SeikyuuSakiKbn,@SeikyuusyoHakDate

		--カーソル内の情報を走査
		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF @seikyuuSakiCd IS NULL OR @seikyuuSakiBrc IS NULL OR @seikyuuSakiKbn IS NULL
				--変更請求先が無い場合は、テーブル関数から取得
				SELECT 
					@seikyuuSakiCd = [seikyuu_saki_cd]
					, @seikyuuSakiBrc = [seikyuu_saki_brc]
					, @seikyuuSakiKbn = [seikyuu_saki_kbn]
				FROM
					[jhs_sys].[fnGetSeikyuuSakiKeyDataTable](@tableType, @kbn, @bangou, @BunruiCd, @SyouhinCd, @kameitenCd);

			--一時テーブルに格納
			INSERT INTO
				 @Materials
			(
				 pKey
			   , Kaisyuu
			   , Okflg
			   , NyuukinDate
			)
			SELECT
				 @BunruiCd+CONVERT
			(
				 varchar
			   , @GamenHyoujiNo
			)
			   , MSS.kaisyuu1_syubetu1
			   , MSS.koufuri_ok_flg
			   , [jhs_sys].[fnGetNyuukinYoteiDate]
			(
				 @seikyuuSakiCd
			   , @seikyuuSakiBrc
			   , @seikyuuSakiKbn
			   , @SeikyuusyoHakDate
			)
			FROM
				 jhs_sys.m_seikyuu_saki MSS
			WHERE
				 MSS.seikyuu_saki_cd = @seikyuuSakiCd
			 AND MSS.seikyuu_saki_brc = @seikyuuSakiBrc
			 AND MSS.seikyuu_saki_kbn = @seikyuuSakiKbn
			
			--次の一件を取得
			FETCH NEXT FROM TeibetuTable
			INTO @BunruiCd,@GamenHyoujiNo,@SyouhinCd,@SeikyuuSakiCd,@SeikyuuSakiBrc,@SeikyuuSakiKbn,@SeikyuusyoHakDate				
		END

		--終了処理
		CLOSE TeibetuTable
		DEALLOCATE TeibetuTable
	END


	--入金確認条件が3(工事代金のみ要入金確認)の場合
	IF (@NyuukinKknnJkn = 3)
	BEGIN
		DECLARE TeibetuTable CURSOR FOR
		SELECT
			 TTS.bunrui_cd
		   , TTS.gamen_hyouji_no
		   , TTS.syouhin_cd
		   , TTS.seikyuu_saki_cd
		   , TTS.seikyuu_saki_brc
		   , TTS.seikyuu_saki_kbn
		   , TTS.seikyuusyo_hak_date
		FROM
			 jhs_sys.t_jiban TJ
				  LEFT OUTER JOIN
					  (SELECT
							TS.kbn
						  , TS.hosyousyo_no
						  , TS.bunrui_cd
						  , TS.gamen_hyouji_no
						  , TS.syouhin_cd
						  , TS.seikyuu_saki_cd
						  , TS.seikyuu_saki_brc
						  , TS.seikyuu_saki_kbn
						  , TS.seikyuusyo_hak_date
						  , 
							 CASE
								  WHEN NullIf(TS.syouhizei_gaku, '') is null
								  THEN (TS.uri_gaku +(TS.uri_gaku * MS.zeiritu))
								  ELSE (TS.uri_gaku + TS.syouhizei_gaku)
							 END seikyuu_gaku
						  , ISNULL(TN.zeikomi_nyuukin_gaku, 0) nyuukin_gaku
						  , ISNULL(TN.zeikomi_henkin_gaku, 0) henkin_gaku
					   FROM
							jhs_sys.t_jiban TJJ
								 INNER JOIN jhs_sys.t_teibetu_seikyuu TS
								   ON TJJ.kbn=TS.kbn
								  AND TJJ.hosyousyo_no=TS.hosyousyo_no
								 LEFT OUTER JOIN jhs_sys.m_syouhizei MS
								   ON TS.zei_kbn = MS.zei_kbn
								 LEFT OUTER JOIN jhs_sys.t_teibetu_nyuukin TN
								   ON TS.kbn = TN.kbn
								  AND TS.hosyousyo_no = TN.hosyousyo_no
								  AND TS.bunrui_cd = TN.bunrui_cd
								  AND TS.gamen_hyouji_no = TN.gamen_hyouji_no
					   WHERE
							1 = 1
						AND ((TS.bunrui_cd = '130'
						AND isnull(TJJ.koj_gaisya_seikyuu_umu, 0) =0)
						 OR (TS.bunrui_cd = '140'
						AND isnull(TJJ.t_koj_kaisya_seikyuu_umu, 0) =0))
						AND TS.seikyuu_umu = '1'
					  )
					   TTS
					ON TJ.kbn = TTS.kbn
				   AND TJ.hosyousyo_no = TTS.hosyousyo_no
		WHERE
			 TJ.kbn = @kbn
		 AND TJ.hosyousyo_no = @bangou
		 AND (ISNULL(TTS.seikyuu_gaku, 0) - ISNULL(TTS.nyuukin_gaku, 0) + ISNULL(TTS.henkin_gaku, 0)) > '0';

		/**********************
		* 集めた邸別情報カーソルから必要材料を一時テーブルに格納
		***********************/
		OPEN TeibetuTable
			
		--初めの一件を取得
		FETCH NEXT FROM TeibetuTable
		INTO @BunruiCd,@GamenHyoujiNo,@SyouhinCd,@SeikyuuSakiCd,@SeikyuuSakiBrc,@SeikyuuSakiKbn,@SeikyuusyoHakDate

		--カーソル内の情報を走査
		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF @seikyuuSakiCd IS NULL OR @seikyuuSakiBrc IS NULL OR @seikyuuSakiKbn IS NULL
				--変更請求先が無い場合は、テーブル関数から取得
				SELECT 
					@seikyuuSakiCd = [seikyuu_saki_cd]
					, @seikyuuSakiBrc = [seikyuu_saki_brc]
					, @seikyuuSakiKbn = [seikyuu_saki_kbn]
				FROM
					[jhs_sys].[fnGetSeikyuuSakiKeyDataTable](@tableType, @kbn, @bangou, @BunruiCd, @SyouhinCd, @kameitenCd);

			--一時テーブルに格納
			INSERT INTO
				 @Materials
			(
				 pKey
			   , Kaisyuu
			   , Okflg
			   , NyuukinDate
			)
			SELECT
				 @BunruiCd+CONVERT
			(
				 varchar
			   , @GamenHyoujiNo
			)
			   , MSS.kaisyuu1_syubetu1
			   , MSS.koufuri_ok_flg
			   , [jhs_sys].[fnGetNyuukinYoteiDate]
			(
				 @seikyuuSakiCd
			   , @seikyuuSakiBrc
			   , @seikyuuSakiKbn
			   , @SeikyuusyoHakDate
			)
			FROM
				 jhs_sys.m_seikyuu_saki MSS
			WHERE
				 MSS.seikyuu_saki_cd = @seikyuuSakiCd
			 AND MSS.seikyuu_saki_brc = @seikyuuSakiBrc
			 AND MSS.seikyuu_saki_kbn = @seikyuuSakiKbn
			
			--次の一件を取得
			FETCH NEXT FROM TeibetuTable
			INTO @BunruiCd,@GamenHyoujiNo,@SyouhinCd,@SeikyuuSakiCd,@SeikyuuSakiBrc,@SeikyuuSakiKbn,@SeikyuusyoHakDate				
		END

		--終了処理
		CLOSE TeibetuTable
		DEALLOCATE TeibetuTable
	END

	--入金確認条件が5(調査代金のみ要入金確認)の場合
	IF (@NyuukinKknnJkn = 5)
	BEGIN
		DECLARE TeibetuTable CURSOR FOR
		SELECT
			 TTS.bunrui_cd
		   , TTS.gamen_hyouji_no
		   , TTS.syouhin_cd
		   , TTS.seikyuu_saki_cd
		   , TTS.seikyuu_saki_brc
		   , TTS.seikyuu_saki_kbn
		   , TTS.seikyuusyo_hak_date
		FROM
			 jhs_sys.t_jiban TJ
				  LEFT OUTER JOIN
					  (SELECT
							TS.kbn
						  , TS.hosyousyo_no
						  , TS.bunrui_cd
						  , TS.gamen_hyouji_no
						  , TS.syouhin_cd
						  , TS.seikyuu_saki_cd
						  , TS.seikyuu_saki_brc
						  , TS.seikyuu_saki_kbn
						  , TS.seikyuusyo_hak_date
						  , 
							 CASE
								  WHEN NullIf(TS.syouhizei_gaku, '') is null
								  THEN (TS.uri_gaku +(TS.uri_gaku * MS.zeiritu))
								  ELSE (TS.uri_gaku + TS.syouhizei_gaku)
							 END seikyuu_gaku
						  , ISNULL(TN.zeikomi_nyuukin_gaku, 0) nyuukin_gaku
						  , ISNULL(TN.zeikomi_henkin_gaku, 0) henkin_gaku
					   FROM
							jhs_sys.t_teibetu_seikyuu TS
								 LEFT OUTER JOIN jhs_sys.m_syouhizei MS
								   ON TS.zei_kbn = MS.zei_kbn
								 LEFT OUTER JOIN jhs_sys.t_teibetu_nyuukin TN
								   ON TS.kbn = TN.kbn
								  AND TS.hosyousyo_no = TN.hosyousyo_no
								  AND TS.bunrui_cd = TN.bunrui_cd
								  AND TS.gamen_hyouji_no = TN.gamen_hyouji_no
					   WHERE
							1 = 1
						AND (TS.bunrui_cd = '100'
						 OR TS.bunrui_cd = '110'
						 OR TS.bunrui_cd = '115'
						 OR TS.bunrui_cd = '120'
						 OR TS.bunrui_cd = '180')
						AND TS.seikyuu_umu = '1'
					  )
					   TTS
					ON TJ.kbn = TTS.kbn
				   AND TJ.hosyousyo_no = TTS.hosyousyo_no
		WHERE
			 TJ.kbn = @kbn
		 AND TJ.hosyousyo_no = @bangou
		 AND (ISNULL(TTS.seikyuu_gaku, 0) - ISNULL(TTS.nyuukin_gaku, 0) + ISNULL(TTS.henkin_gaku, 0)) > '0';

		/**********************
		* 集めた邸別情報カーソルから必要材料を一時テーブルに格納
		***********************/
		OPEN TeibetuTable
			
		--初めの一件を取得
		FETCH NEXT FROM TeibetuTable
		INTO @BunruiCd,@GamenHyoujiNo,@SyouhinCd,@SeikyuuSakiCd,@SeikyuuSakiBrc,@SeikyuuSakiKbn,@SeikyuusyoHakDate

		--カーソル内の情報を走査
		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF @seikyuuSakiCd IS NULL OR @seikyuuSakiBrc IS NULL OR @seikyuuSakiKbn IS NULL
				--変更請求先が無い場合は、テーブル関数から取得
				SELECT 
					@seikyuuSakiCd = [seikyuu_saki_cd]
					, @seikyuuSakiBrc = [seikyuu_saki_brc]
					, @seikyuuSakiKbn = [seikyuu_saki_kbn]
				FROM
					[jhs_sys].[fnGetSeikyuuSakiKeyDataTable](@tableType, @kbn, @bangou, @BunruiCd, @SyouhinCd, @kameitenCd);

			--一時テーブルに格納
			INSERT INTO
				 @Materials
			(
				 pKey
			   , Kaisyuu
			   , Okflg
			   , NyuukinDate
			)
			SELECT
				 @BunruiCd+CONVERT
			(
				 varchar
			   , @GamenHyoujiNo
			)
			   , MSS.kaisyuu1_syubetu1
			   , MSS.koufuri_ok_flg
			   , CONVERT(VARCHAR, [jhs_sys].[fnGetNyuukinYoteiDate](@seikyuuSakiCd, @seikyuuSakiBrc, @seikyuuSakiKbn, @SeikyuusyoHakDate), 111)
			FROM
				 jhs_sys.m_seikyuu_saki MSS
			WHERE
				 MSS.seikyuu_saki_cd = @seikyuuSakiCd
			 AND MSS.seikyuu_saki_brc = @seikyuuSakiBrc
			 AND MSS.seikyuu_saki_kbn = @seikyuuSakiKbn
			
			--次の一件を取得
			FETCH NEXT FROM TeibetuTable
			INTO @BunruiCd,@GamenHyoujiNo,@SyouhinCd,@SeikyuuSakiCd,@SeikyuuSakiBrc,@SeikyuuSakiKbn,@SeikyuusyoHakDate				
		END

		--終了処理
		CLOSE TeibetuTable
		DEALLOCATE TeibetuTable
	END

	/**********************
	* 一時テーブルを材料に入金無し詳細を判断する
	***********************/
	BEGIN
		--画面からの呼出(入金状況確定済)
		IF (@retType <> '1')
		BEGIN
			SELECT @NyuukinJyky = (datename(mm,MAX(NyuukinDate)) + '/' + datename(dd,MAX(NyuukinDate)))
			  FROM @Materials
			RETURN @NyuukinJyky
		END

		--2:入金無し
		SELECT @JdgFlg = pKey
		  FROM @Materials
		 WHERE ISNULL(Kaisyuu,'') <> '5'

		IF (@JdgFlg is not null)
		BEGIN
			RETURN '2'
		END
		
		--5：引落NG
		SELECT @JdgFlg = pKey
		  FROM @Materials
		 WHERE ISNULL(Kaisyuu,'') = '5'
		   AND ISNULL(Okflg,'') <> '1'

		IF (@JdgFlg is not null)
		BEGIN
			RETURN '5'
		END

		--6:引落失敗
		SELECT @JdgFlg = pKey
		  FROM @Materials
		 WHERE ISNULL(Kaisyuu,'') = '5'
		   AND ISNULL(Okflg,'') = '1'
		   AND (SELECT MIN(NyuukinDate) from @Materials) < @today

		IF (@JdgFlg is not null)
		BEGIN
			RETURN '6'
		END

		--7:MM/DD引落
		SELECT @JdgFlg = pKey
		  FROM @Materials
		 WHERE ISNULL(Kaisyuu,'') = '5'
		   AND ISNULL(Okflg,'') = '1'
		   AND NyuukinDate >= @today

		IF (@JdgFlg is not null)
		BEGIN
			RETURN '7'
		END
	END

	RETURN @NyuukinJyky
END
