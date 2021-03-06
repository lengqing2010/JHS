-- =============================================
-- Author:		<TIS>
-- Create date: <2012/03/13>
-- Description:	<加盟店の入金確認条件より、入金状況を返却する>
-- =============================================
CREATE FUNCTION [jhs_sys].[fnGetNyuukinJyky] (@kbn CHAR(1),@bangou VARCHAR(10),@today datetime)
RETURNS CHAR(1)
AS
BEGIN
	DECLARE @NyuukinKknnJkn CHAR(1) --入金確認条件
	DECLARE @NyuukinJyky CHAR(1)	--入金状況
	DECLARE @retType int			--返却タイプ（=1:コード,<>1:MM/DD）

	SET @NyuukinKknnJkn = NULL
	SET @NyuukinJyky = 0
	SET @retType = 1

	--区分,番号より加盟店Mの入金確認条件を取得する
	IF (@kbn IS NOT NULL AND @bangou IS NOT NULL)
	BEGIN
	  SELECT
		@NyuukinKknnJkn = MK.nyuukin_kakunin_jyouken
	  FROM jhs_sys.t_jiban TJ
		LEFT OUTER JOIN jhs_sys.m_kameiten MK
		  ON TJ.kameiten_cd = MK.kameiten_cd
      WHERE TJ.kbn = @kbn
        AND TJ.hosyousyo_no = @bangou
	END

	--入金確認条件が0(通常通り用入金確認)の場合
	--入金確認条件が4(要入金確認(自動引落))の場合
	--入金確認条件がNullもしくは空文字の場合
	--入金確認条件が0～6以外の場合
	IF (@NyuukinKknnJkn = 0 
		OR @NyuukinKknnJkn = 4
		OR @NyuukinKknnJkn not between '0' and '6'
		OR NullIf(@NyuukinKknnJkn,'') is null)
	BEGIN
		SELECT
			 @NyuukinJyky =(
				  CASE
					   WHEN TTS.kbn is null
					   THEN '0'
					   ELSE (
								 CASE
									  WHEN (ISNULL(TTS.seikyuu_gaku, 0) - ISNULL(TTS.nyuukin_gaku, 0) + ISNULL(TTS.henkin_gaku, 0)) <= '0'
									  THEN '1'
									  ELSE [jhs_sys].[fnGetNyuukinNasiInfo](@retType,@kbn,@bangou,@today)
								 END)
				  END)
		FROM
			 t_jiban TJ
				  LEFT OUTER JOIN
					  (SELECT
							TS.kbn
						  , TS.hosyousyo_no
						  , SUM(
								 CASE
									  WHEN NullIf(TS.syouhizei_gaku, '') is null
									  THEN (TS.uri_gaku +(TS.uri_gaku * MS.zeiritu))
									  ELSE (TS.uri_gaku + TS.syouhizei_gaku)
								 END) seikyuu_gaku
						  , SUM(ISNULL(TN.zeikomi_nyuukin_gaku, 0)) nyuukin_gaku
						  , SUM(ISNULL(TN.zeikomi_henkin_gaku, 0)) henkin_gaku
					   FROM
							t_jiban TJJ
								 INNER JOIN t_teibetu_seikyuu TS
								   ON TJJ.kbn=TS.kbn
								  AND TJJ.hosyousyo_no=TS.hosyousyo_no
								 LEFT OUTER JOIN m_syouhizei MS
								   ON TS.zei_kbn = MS.zei_kbn
								 LEFT OUTER JOIN t_teibetu_nyuukin TN
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
					   GROUP BY
							TS.kbn
						  , TS.hosyousyo_no
					  )
					   TTS
					ON TJ.kbn = TTS.kbn
				   AND TJ.hosyousyo_no = TTS.hosyousyo_no
		WHERE
			 TJ.kbn = @kbn
		 AND TJ.hosyousyo_no = @bangou
	END

	--入金確認条件が1(入金確認不要(地盤))の場合
	IF (@NyuukinKknnJkn = 1)
	BEGIN
		SELECT
			 @NyuukinJyky =(
				  CASE
					   WHEN TTS.kbn is null
					   THEN '0'
					   ELSE (
								 CASE
									  WHEN (ISNULL(TTS.seikyuu_gaku, 0) - ISNULL(TTS.nyuukin_gaku, 0) + ISNULL(TTS.henkin_gaku, 0)) <= '0'
									  THEN '1'
									  ELSE [jhs_sys].[fnGetNyuukinNasiInfo](@retType,@kbn,@bangou,@today)
								 END)
				  END)
		FROM
			 t_jiban TJ
				  LEFT OUTER JOIN
					  (SELECT
							TS.kbn
						  , TS.hosyousyo_no
						  , SUM(
								 CASE
									  WHEN NullIf(TS.syouhizei_gaku, '') is null
									  THEN (TS.uri_gaku +(TS.uri_gaku * MS.zeiritu))
									  ELSE (TS.uri_gaku + TS.syouhizei_gaku)
								 END) seikyuu_gaku
						  , SUM(ISNULL(TN.zeikomi_nyuukin_gaku, 0)) nyuukin_gaku
						  , SUM(ISNULL(TN.zeikomi_henkin_gaku, 0)) henkin_gaku
					   FROM
							t_jiban TJJ
								 INNER JOIN t_teibetu_seikyuu TS
								   ON TJJ.kbn=TS.kbn
								  AND TJJ.hosyousyo_no=TS.hosyousyo_no
								 LEFT OUTER JOIN m_syouhizei MS
								   ON TS.zei_kbn = MS.zei_kbn
								 LEFT OUTER JOIN t_teibetu_nyuukin TN
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
					   GROUP BY
							TS.kbn
						  , TS.hosyousyo_no
					  )
					   TTS
					ON TJ.kbn = TTS.kbn
				   AND TJ.hosyousyo_no = TTS.hosyousyo_no
		WHERE
			 TJ.kbn = @kbn
		 AND TJ.hosyousyo_no = @bangou
	END

	--入金確認条件が2(要発注書確認)の場合
	IF (@NyuukinKknnJkn = 2)
	BEGIN
		SELECT
			 @NyuukinJyky =(
				  CASE
					   WHEN TTS.kbn is null
					   THEN '0'
					   ELSE (
								 CASE
									  WHEN (ISNULL(TTS.seikyuu_gaku, 0) - ISNULL(TTS.nyuukin_gaku, 0) + ISNULL(TTS.henkin_gaku, 0)) <= '0'
									  THEN '3'
									  ELSE '4'
								 END)
				  END)
		FROM
			 t_jiban TJ
				  LEFT OUTER JOIN
					  (SELECT
							TS.kbn
						  , TS.hosyousyo_no
						  , SUM(
								 CASE
									  WHEN NullIf(TS.syouhizei_gaku, '') is null
									  THEN (TS.uri_gaku +(TS.uri_gaku * MS.zeiritu))
									  ELSE (TS.uri_gaku + TS.syouhizei_gaku)
								 END) seikyuu_gaku
						  , (
								 CASE
									  WHEN SUM(ISNULL(TN.zeikomi_nyuukin_gaku, 0)) > SUM(ISNULL(TS.hattyuusyo_gaku, 0)) +SUM(
												CASE
													 WHEN NullIf(TS.syouhizei_gaku, '') is null
													 THEN (TS.uri_gaku * MS.zeiritu)
													 ELSE (TS.syouhizei_gaku)
												END)
									  THEN SUM(ISNULL(TN.zeikomi_nyuukin_gaku, 0))
									  ELSE SUM(ISNULL(TS.hattyuusyo_gaku, 0)) +SUM(
												CASE
													 WHEN NullIf(TS.syouhizei_gaku, '') is null
													 THEN (TS.uri_gaku * MS.zeiritu)
													 ELSE (TS.syouhizei_gaku)
												END)
								 END) nyuukin_gaku
						  , SUM(ISNULL(TN.zeikomi_henkin_gaku, 0)) henkin_gaku
					   FROM
							t_jiban TJJ
								 INNER JOIN t_teibetu_seikyuu TS
								   ON TJJ.kbn=TS.kbn
								  AND TJJ.hosyousyo_no=TS.hosyousyo_no
								 LEFT OUTER JOIN m_syouhizei MS
								   ON TS.zei_kbn = MS.zei_kbn
								 LEFT OUTER JOIN t_teibetu_nyuukin TN
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
					   GROUP BY
							TS.kbn
						  , TS.hosyousyo_no
					  )
					   TTS
					ON TJ.kbn = TTS.kbn
				   AND TJ.hosyousyo_no = TTS.hosyousyo_no
		WHERE
			 TJ.kbn = @kbn
		 AND TJ.hosyousyo_no = @bangou
	END

	--入金確認条件が3(工事代金のみ要入金確認)の場合
	IF (@NyuukinKknnJkn = 3)
	BEGIN
		SELECT
			 @NyuukinJyky =(
				  CASE
					   WHEN TTS.kbn is null
					   THEN '0'
					   ELSE (
								 CASE
									  WHEN (ISNULL(TTS.seikyuu_gaku, 0) - ISNULL(TTS.nyuukin_gaku, 0) + ISNULL(TTS.henkin_gaku, 0)) <= '0'
									  THEN '1'
									  ELSE [jhs_sys].[fnGetNyuukinNasiInfo](@retType,@kbn,@bangou,@today)
								 END)
				  END)
		FROM
			 t_jiban TJ
				  LEFT OUTER JOIN
					  (SELECT
							TS.kbn
						  , TS.hosyousyo_no
						  , SUM(
								 CASE
									  WHEN NullIf(TS.syouhizei_gaku, '') is null
									  THEN (TS.uri_gaku +(TS.uri_gaku * MS.zeiritu))
									  ELSE (TS.uri_gaku + TS.syouhizei_gaku)
								 END) seikyuu_gaku
						  , SUM(ISNULL(TN.zeikomi_nyuukin_gaku, 0)) nyuukin_gaku
						  , SUM(ISNULL(TN.zeikomi_henkin_gaku, 0)) henkin_gaku
					   FROM
							t_jiban TJJ
								 INNER JOIN t_teibetu_seikyuu TS
								   ON TJJ.kbn=TS.kbn
								  AND TJJ.hosyousyo_no=TS.hosyousyo_no
								 LEFT OUTER JOIN m_syouhizei MS
								   ON TS.zei_kbn = MS.zei_kbn
								 LEFT OUTER JOIN t_teibetu_nyuukin TN
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
					   GROUP BY
							TS.kbn
						  , TS.hosyousyo_no
					  )
					   TTS
					ON TJ.kbn = TTS.kbn
				   AND TJ.hosyousyo_no = TTS.hosyousyo_no
		WHERE
			 TJ.kbn = @kbn
		 AND TJ.hosyousyo_no = @bangou
	END

	--入金確認条件が5(調査代金のみ要入金確認)の場合
	IF (@NyuukinKknnJkn = 5)
	BEGIN
		SELECT
			 @NyuukinJyky =(
				  CASE
					   WHEN TTS.kbn is null
					   THEN '0'
					   ELSE (
								 CASE
									  WHEN (ISNULL(TTS.seikyuu_gaku, 0) - ISNULL(TTS.nyuukin_gaku, 0) + ISNULL(TTS.henkin_gaku, 0)) <= '0'
									  THEN '1'
									  ELSE [jhs_sys].[fnGetNyuukinNasiInfo](@retType,@kbn,@bangou,@today)
								 END)
				  END)
		FROM
			 t_jiban TJ
				  LEFT OUTER JOIN
					  (SELECT
							TS.kbn
						  , TS.hosyousyo_no
						  , SUM(
								 CASE
									  WHEN NullIf(TS.syouhizei_gaku, '') is null
									  THEN (TS.uri_gaku +(TS.uri_gaku * MS.zeiritu))
									  ELSE (TS.uri_gaku + TS.syouhizei_gaku)
								 END) seikyuu_gaku
						  , SUM(ISNULL(TN.zeikomi_nyuukin_gaku, 0)) nyuukin_gaku
						  , SUM(ISNULL(TN.zeikomi_henkin_gaku, 0)) henkin_gaku
					   FROM
							t_teibetu_seikyuu TS
								 LEFT OUTER JOIN m_syouhizei MS
								   ON TS.zei_kbn = MS.zei_kbn
								 LEFT OUTER JOIN t_teibetu_nyuukin TN
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
					   GROUP BY
							TS.kbn
						  , TS.hosyousyo_no
					  )
					   TTS
					ON TJ.kbn = TTS.kbn
				   AND TJ.hosyousyo_no = TTS.hosyousyo_no
		WHERE
			 TJ.kbn = @kbn
		 AND TJ.hosyousyo_no = @bangou
	END

	--入金確認条件が6(入金確認不要(瑕疵・地盤))の場合
	IF (@NyuukinKknnJkn = 6)
	BEGIN
		select @NyuukinJyky = '0'
	END

	RETURN @NyuukinJyky
END
