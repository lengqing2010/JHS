
-- =============================================
-- Author:		Sorun
-- Create date: 2010/03/05
-- Description:	物件/邸別請求の情報、あるいは店別(初期)請求の情報を元に、請求先コード/請求先枝番/請求先名を取得する
-- =============================================
CREATE FUNCTION [jhs_sys].[fnGetSeikyuuSakiKeyDataTable] 
(
  @FlgTeiOrTen INT                    --[必須] 1=邸別請求をベース 2=店別請求をベース 3=店別初期請求をベース
  ,@kbn CHAR(1)                       --[@FlgTeiOrTen=1の場合必須] 区分
  ,@bangou VARCHAR(10)                --[@FlgTeiOrTen=1の場合必須] 番号
  ,@bunruiCd VARCHAR(3)               --[必須] 分類コード
  ,@SyouhinCd VARCHAR(8)              --[必須] 商品コード
  ,@KameitenOrEigyousyoCd VARCHAR(10) --[必須] FlgTeiOrTen=1の場合：地盤テーブル.加盟店コード  FlgTeiOrTen=2の場合：店別(初期)請求テーブル.店コード
)
RETURNS @retSeikyuuSakiKeyData TABLE
(
  [seikyuu_saki_cd] VARCHAR(100)      --請求先コード
  ,[seikyuu_saki_brc] VARCHAR(100)    --請求先枝番
  ,[seikyuu_saki_kbn] CHAR(1)         --請求先区分
  ,[seikyuu_saki_name] VARCHAR(100)   --請求先名
  ,[seikyuu_saki_name2] VARCHAR(100)   --請求先名2
)
AS
BEGIN
  DECLARE @SeikyuuSakiCd VARCHAR(100)
  DECLARE @SeikyuuSakiBrc VARCHAR(100)
  DECLARE @SeikyuuSakiKbn CHAR(1)
  DECLARE @SeikyuuSakiName VARCHAR(100)
  DECLARE @SeikyuuSakiName2 VARCHAR(100)
  DECLARE @bunruiKoj VARCHAR(3)
  DECLARE @bunruiTKoj VARCHAR(3)
  DECLARE @bunruiFCGaiHansoku VARCHAR(3)
  DECLARE @flgKoujikaisyaSeikyuu INT
  
  SET @SeikyuuSakiCd = NULL
  SET @SeikyuuSakiBrc = NULL
  SET @SeikyuuSakiKbn = NULL
  SET @SeikyuuSakiName = NULL
  SET @SeikyuuSakiName2 = NULL
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
      --工事会社請求の場合、調査会社マスタから請求先を取得(VIEWを使用)
      SELECT
        @SeikyuuSakiCd = seikyuu_saki_cd
        , @SeikyuuSakiBrc = seikyuu_saki_brc
        , @SeikyuuSakiKbn = seikyuu_saki_kbn
      FROM
        jhs_sys.v_syouhin_siire_seikyuu_saki_jiban_tyskaisya 
      WHERE
        kbn = @kbn
        AND hosyousyo_no = @bangou
        AND syouhin_cd = @SyouhinCd
  END 
  
  IF @SeikyuuSakiCd IS NULL AND ISNULL(@flgKoujikaisyaSeikyuu,0) <> 1
  --ここまでで請求先コードが取得できておらず、工事会社請求でもない場合
  BEGIN
    IF @FlgTeiOrTen = 2 AND @bunruiCd <> @bunruiFCGaiHansoku
      --店別請求で、FC外販促品以外の場合、営業所マスタから請求先を取得
      SELECT
        @SeikyuuSakiCd = seikyuu_saki_cd
        , @SeikyuuSakiBrc = seikyuu_saki_brc
        , @SeikyuuSakiKbn = seikyuu_saki_kbn
      FROM
        jhs_sys.m_eigyousyo
      WHERE
        eigyousyo_cd = @KameitenOrEigyousyoCd

    ELSE
      --加盟店マスタから請求先を取得(VIEWを使用)
      SELECT
        @SeikyuuSakiCd = seikyuu_saki_cd
        , @SeikyuuSakiBrc = seikyuu_saki_brc
        , @SeikyuuSakiKbn = seikyuu_saki_kbn
      FROM
        jhs_sys.v_syouhin_seikyuusaki_kameiten
      WHERE
        syouhin_cd = @SyouhinCd 
        AND kameiten_cd = @KameitenOrEigyousyoCd
  END

  IF @SeikyuuSakiCd IS NOT NULL AND @SeikyuuSakiBrc IS NOT NULL AND @SeikyuuSakiKbn IS NOT NULL
  BEGIN
      --請求先名を取得(VIEWを使用)
      SELECT
        @SeikyuuSakiName = seikyuu_saki_mei 
        , @SeikyuuSakiName2 = seikyuu_saki_mei2 
      FROM
        jhs_sys.v_seikyuu_saki_info 
      WHERE
        seikyuu_saki_cd = @SeikyuuSakiCd 
        AND seikyuu_saki_brc = @SeikyuuSakiBrc 
        AND seikyuu_saki_kbn = @SeikyuuSakiKbn 
  END

  INSERT @retSeikyuuSakiKeyData
  SELECT @SeikyuuSakiCd, @SeikyuuSakiBrc, @SeikyuuSakiKbn, @SeikyuuSakiName, @SeikyuuSakiName2

  RETURN
  
END

