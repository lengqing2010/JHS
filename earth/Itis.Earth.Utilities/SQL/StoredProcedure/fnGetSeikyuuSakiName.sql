
--@FlgTeiOrTen:1=Teibetu 2=Tenbetu
CREATE FUNCTION [jhs_sys].[fnGetSeikyuuSakiName] (@FlgTeiOrTen INT,@kbn CHAR(1),@bangou VARCHAR(10),@bunruiCd VARCHAR(3),@SyouhinCd VARCHAR(8),@KameitenOrEigyousyoCd VARCHAR(10))
RETURNS VARCHAR(100)
AS
BEGIN
  DECLARE @SeikyuusakiName VARCHAR(100)
  DECLARE @bunruiKoj VARCHAR(3)
  DECLARE @bunruiTKoj VARCHAR(3)
  DECLARE @bunruiFCGaiHansoku VARCHAR(3)
  DECLARE @flgKoujikaisyaSeikyuu INT
  
  SET @SeikyuusakiName = NULL
  SET @bunruiKoj = '130'
  SET @bunruiTKoj = '140'
  SET @bunruiFCGaiHansoku = '220'

  IF @FlgTeiOrTen = 1 AND @bunruiCd IN (@bunruiKoj, @bunruiTKoj) AND (@kbn IS NOT NULL AND @bangou IS NOT NULL)
  BEGIN
    --工事商品の場合、工事会社請求か否かをチェック
    SELECT
      @flgKoujikaisyaSeikyuu = CASE
        WHEN @bunruiCd = @bunruiKoj
        THEN koj_gaisya_seikyuu_umu 
        WHEN @bunruiCd = @bunruiTKoj
        THEN t_koj_kaisya_seikyuu_umu
        ELSE NULL 
        END
    FROM
      jhs_sys.t_jiban 
    WHERE
      kbn = @kbn
      AND hosyousyo_no = @bangou

    IF @flgKoujikaisyaSeikyuu = 1
      --工事会社請求の場合、調査会社マスタから請求先名を取得(VIEWを使用)
      SELECT
        @SeikyuusakiName = MT.seikyuu_saki_shri_saki_mei 
      FROM
        jhs_sys.v_syouhin_siire_seikyuu_saki_jiban_tyskaisya VST 
        INNER JOIN jhs_sys.m_tyousakaisya MT 
          ON VST.seikyuu_saki_cd = MT.tys_kaisya_cd 
          AND VST.seikyuu_saki_brc = MT.jigyousyo_cd 
      WHERE
        VST.kbn = @kbn
        AND VST.hosyousyo_no = @bangou
        AND VST.syouhin_cd = @SyouhinCd
  END 
  
  IF @SeikyuusakiName IS NULL AND ISNULL(@flgKoujikaisyaSeikyuu,0) <> 1
  BEGIN
    IF @FlgTeiOrTen = 2 AND @bunruiCd <> @bunruiFCGaiHansoku
      --店別請求で、FC外販促品以外の場合、営業所マスタから請求先名を取得
      SELECT
        @SeikyuusakiName = ME2.seikyuu_saki_mei 
      FROM
        jhs_sys.m_eigyousyo ME1 
        INNER JOIN jhs_sys.m_eigyousyo ME2 
          ON ME1.seikyuu_saki_cd = ME2.eigyousyo_cd 
      WHERE
        ME1.eigyousyo_cd = @KameitenOrEigyousyoCd

    ELSE
      --加盟店マスタから請求先名(加盟店正式名)を取得(VIEWを使用)
      SELECT
        @SeikyuusakiName = Mk.kameiten_seisiki_mei 
      FROM
        jhs_sys.v_syouhin_seikyuusaki_kameiten VSK 
        INNER JOIN jhs_sys.m_kameiten MK 
          ON VSK.seikyuu_saki_cd = MK.kameiten_cd 
      WHERE
        VSK.syouhin_cd = @SyouhinCd 
        AND VSK.kameiten_cd = @KameitenOrEigyousyoCd
  
  END

  RETURN @SeikyuusakiName
  
END

