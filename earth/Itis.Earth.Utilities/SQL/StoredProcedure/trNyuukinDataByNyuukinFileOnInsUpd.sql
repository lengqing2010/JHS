

-- =============================================
-- Author:      <S.Katane>
-- Create date: <2010/03/05>
-- Description: <入金データ作成トリガー(ON 入金ファイル取込 AFTER INSERT, UPDATE)>
-- =============================================
CREATE TRIGGER [jhs_sys].[trNyuukinDataByNyuukinFileOnInsUpd] 
   ON  [jhs_sys].[t_nyuukin_file_torikomi] 
   AFTER INSERT,UPDATE
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    --カーソル用変数宣言
    DECLARE @pKey               int         --入金取込ユニークNO
    DECLARE @torikesiINS        int         --取消（INSERTED用）
    DECLARE @torikesiDEL        int         --取消（DELETED用）
    DECLARE @minusFlg			int         --赤伝票発行フラグ
    DECLARE @plusFlg			int         --黒伝票発行フラグ
    DECLARE @maxNum             int         --最大伝票ユニークNO
    DECLARE @motoNum            int         --取消元伝票ユニークNO
    DECLARE @changeNum			int         --更新対象がある場合の伝票ユニークNO
    DECLARE @FN                 CHAR(2)		--伝票種別：入金(NN)
    DECLARE @FR                 CHAR(2)		--伝票種別：入金取消(NR)
	DECLARE @userid             VARCHAR(30) --登録ログインユーザーID

    --初期化
    SET @pKey = ''
    SET @FN = 'FN';
    SET @FR = 'FR';


    /******************************************
     *更新された行をカーソルで取得し、処理する*
     ******************************************/
    --カーソルの定義
    DECLARE CUR_NYUUKIN_FILE
    CURSOR FOR 
        SELECT INS.nyuukin_torikomi_unique_no
              ,ISNULL(INS.torikesi, 0)
              ,ISNULL(DEL.torikesi, 0)
              ,ISNULL(INS.upd_login_user_id, INS.add_login_user_id)
          FROM inserted INS
          LEFT OUTER JOIN deleted DEL
            ON INS.nyuukin_torikomi_unique_no = DEL.nyuukin_torikomi_unique_no

    --カーソルを開く
    OPEN CUR_NYUUKIN_FILE

    --カーソルより最初の一行を取得
    FETCH NEXT FROM CUR_NYUUKIN_FILE
    INTO @pKey
        ,@torikesiINS
        ,@torikesiDEL
        ,@userid

	-- カーソルで取得した行が終端に達するまで処理を継続する
    WHILE @@FETCH_STATUS = 0
    BEGIN 

		-- 伝票ユニークNOのリセット
		SET @maxNum = NULL;
		SET @motoNum = NULL;
		-- 伝票発行フラグのリセット
		SET @minusFlg = NULL;
		SET @plusFlg = NULL;

		/*******************************************************
        * 更新されたデータの入金取込ユニークNOと紐付く         *
        * 入金データの伝票ユニークNOの最大値を取得             *
        ********************************************************/
        SELECT @maxNum = MAX(NYU.denpyou_unique_no)
          FROM [jhs_sys].[t_nyuukin_data] NYU
         WHERE 1=1
           AND NYU.nyuukin_torikomi_unique_no = @pKey
		
		/***************************************************************
        * 上で取得した最大伝票ユニークNOの取消元伝票ユニークNOを取得   *
        ****************************************************************/
        --「取得○」：直近がマイナス伝票
        --「取得×」：直近がプラス伝票
        SELECT @motoNum = NYU.torikesi_moto_denpyou_unique_no
		  FROM [jhs_sys].[t_nyuukin_data] NYU
         WHERE 1=1
           AND NYU.denpyou_unique_no = @maxNum
        
        /********************************************************
        * 更新された入金取込ユニークNOのデータと、入金データの
        * 比較対象が一つでも異なる場合、伝票ユニークNOを@changeNumに格納する
        ********************************************************/
        SELECT @changeNum = NYU.denpyou_unique_no
          FROM [jhs_sys].[t_nyuukin_file_torikomi] INS
         INNER JOIN [jhs_sys].[t_nyuukin_data] NYU
            ON NYU.nyuukin_torikomi_unique_no = INS.nyuukin_torikomi_unique_no
		 WHERE INS.nyuukin_torikomi_unique_no = @pKey
		   AND NYU.denpyou_unique_no = @maxNum
		   AND (
		 	   ISNULL(NYU.seikyuu_saki_cd, '')		<> ISNULL(INS.seikyuu_saki_cd, '')
		 	OR ISNULL(NYU.seikyuu_saki_brc, '') 	<> ISNULL(INS.seikyuu_saki_brc, '')
		 	OR ISNULL(NYU.seikyuu_saki_kbn, '') 	<> ISNULL(INS.seikyuu_saki_kbn, '')
		    OR ISNULL(NYU.syougou_kouza_no, '') 	<> ISNULL(INS.syougou_kouza_no, '')
		    OR ISNULL(NYU.nyuukin_date, '')			<> ISNULL(INS.nyuukin_date, '')
		    OR ISNULL(NYU.genkin, '')				<> ISNULL(INS.genkin, '')
		    OR ISNULL(NYU.kogitte, '')				<> ISNULL(INS.kogitte, '')
		    OR ISNULL(NYU.furikomi, '')				<> ISNULL(INS.furikomi, '')
		    OR ISNULL(NYU.tegata, '')				<> ISNULL(INS.tegata, '')
		    OR ISNULL(NYU.sousai, '')				<> ISNULL(INS.sousai, '')
		    OR ISNULL(NYU.nebiki, '')				<> ISNULL(INS.nebiki, '')
		    OR ISNULL(NYU.sonota, '')				<> ISNULL(INS.sonota, '')
		    OR ISNULL(NYU.kyouryoku_kaihi, '')		<> ISNULL(INS.kyouryoku_kaihi, '')
			OR ISNULL(NYU.kouza_furikae, '')		<> ISNULL(INS.kouza_furikae, '')
			OR ISNULL(NYU.furikomi_tesuuryou, '')	<> ISNULL(INS.furikomi_tesuuryou, '')
		    OR ISNULL(NYU.tegata_kijitu, '')		<> ISNULL(INS.tegata_kijitu, '')
		    OR ISNULL(NYU.tegata_no, '')			<> ISNULL(INS.tegata_no, '')
		    OR ISNULL(NYU.tekiyou_mei, '')			<> ISNULL(INS.tekiyou_mei, '')
		    )
        
        /****************************************************
         * 更新処理の判定                                   *
         *    @minusFlg = 1 → プラス伝票発行               *
         *    @plusFlg = 1 → マイナス伝票発行              *
         ****************************************************/
        -- 更新対象に紐付く直近の伝票データがある場合
        IF @maxNum IS NOT NULL
        BEGIN
            -- 更新対象に紐付く直近の伝票データがプラス伝票の場合
            IF @motoNum IS NULL
                SET @minusFlg = 1;
            ELSE
                SET @plusFlg = 1;

            -- 更新対象データと更新対象に紐付く直近の伝票データに差異がある場合
            IF @changeNum IS NOT NULL
                SET @plusFlg = 1;
            ELSE
                -- 差異なし・取消に関与しない・直近の伝票データがプラス伝票の場合
                IF @torikesiINS = 0 AND @torikesiDEL = 0 AND @motoNum IS NULL
                    SET @minusFlg = NULL;

            -- 取消の場合はプラス伝票を発行しない
            IF @torikesiINS <> 0
                SET @plusFlg = NULL;

            -- 取消から復帰する場合はプラス伝票を発行する
            IF @torikesiDEL <> 0 AND @torikesiINS = 0
                SET @plusFlg = 1;
        END

        -- 更新対象に紐付く伝票データがない場合
        IF @maxNum IS NULL
        BEGIN
            -- 取消ではない場合
            IF @torikesiINS = 0
                SET @plusFlg = 1;
        END

		/***********************************************
        * フラグがある場合、赤伝票（マイナス伝票）発行 *
        ************************************************/
		IF @minusFlg = 1
		BEGIN
			INSERT INTO [jhs_sys].[t_nyuukin_data] ( 
				    denpyou_no
				  , denpyou_syubetu
				  , torikesi_moto_denpyou_unique_no
				  , nyuukin_torikomi_unique_no
				  , seikyuu_saki_cd
				  , seikyuu_saki_brc
				  , seikyuu_saki_kbn
				  , seikyuu_saki_mei
				  , syougou_kouza_no
				  , nyuukin_date
				  , genkin
				  , kogitte
				  , furikomi
				  , tegata
				  , sousai
				  , nebiki
				  , sonota
				  , kyouryoku_kaihi
				  , kouza_furikae
				  , furikomi_tesuuryou
				  , tegata_kijitu
				  , tegata_no
				  , tekiyou_mei
				  , add_login_user_id
                  , add_login_user_name
				  , add_datetime
				  , upd_login_user_id
				  , upd_datetime
				)
				SELECT
					NULL
					,@FR
					,NYUOLD.denpyou_unique_no
					,NYUOLD.nyuukin_torikomi_unique_no
					,NYUOLD.seikyuu_saki_cd
					,NYUOLD.seikyuu_saki_brc
					,NYUOLD.seikyuu_saki_kbn
					,NYUOLD.seikyuu_saki_mei
					,NYUOLD.syougou_kouza_no
					,NYUOLD.nyuukin_date
					,NYUOLD.genkin * -1
					,NYUOLD.kogitte * -1
					,NYUOLD.furikomi * -1
					,NYUOLD.tegata * -1
					,NYUOLD.sousai * -1
					,NYUOLD.nebiki * -1
					,NYUOLD.sonota * -1
					,NYUOLD.kyouryoku_kaihi * -1
					,NYUOLD.kouza_furikae * -1
					,NYUOLD.furikomi_tesuuryou * -1
					,NYUOLD.tegata_kijitu
					,NYUOLD.tegata_no
					,NYUOLD.tekiyou_mei
					,@userid
                    ,jhs_sys.fnGetAddUpdUserName(@userid)
					,GETDATE()
					,NULL
					,NULL
				  FROM [jhs_sys].[t_nyuukin_data] NYUOLD
				 WHERE NYUOLD.denpyou_unique_no = @maxNum
		END
		
		
		/***************************************************
        * フラグがある場合、もしくは伝票ユニークNOの最大値 *
        * が取得出来ない場合、黒伝票（プラス伝票）発行     *
        ****************************************************/
		IF @plusFlg = 1 
		BEGIN
			INSERT INTO [jhs_sys].[t_nyuukin_data] ( 
				    denpyou_no
				  , denpyou_syubetu
				  , torikesi_moto_denpyou_unique_no
				  , nyuukin_torikomi_unique_no
				  , seikyuu_saki_cd
				  , seikyuu_saki_brc
				  , seikyuu_saki_kbn
				  , seikyuu_saki_mei
				  , syougou_kouza_no
				  , nyuukin_date
				  , genkin
				  , kogitte
				  , furikomi
				  , tegata
				  , sousai
				  , nebiki
				  , sonota
				  , kyouryoku_kaihi
				  , kouza_furikae
				  , furikomi_tesuuryou
				  , tegata_kijitu
				  , tegata_no
				  , tekiyou_mei
				  , add_login_user_id
                  , add_login_user_name
				  , add_datetime
				  , upd_login_user_id
				  , upd_datetime
				) 
				SELECT
					NULL
					,@FN
					,NULL
					,INS.nyuukin_torikomi_unique_no
					,INS.seikyuu_saki_cd
					,INS.seikyuu_saki_brc
					,INS.seikyuu_saki_kbn
					,INS.seikyuu_saki_mei
					,INS.syougou_kouza_no
					,INS.nyuukin_date
					,INS.genkin
					,INS.kogitte
					,INS.furikomi
					,INS.tegata
					,INS.sousai
					,INS.nebiki
					,INS.sonota
					,INS.kyouryoku_kaihi
					,INS.kouza_furikae
					,INS.furikomi_tesuuryou
					,INS.tegata_kijitu
					,INS.tegata_no
					,INS.tekiyou_mei
					,@userid
                    ,jhs_sys.fnGetAddUpdUserName(@userid)
					,GETDATE()
					,NULL
					,NULL
				  FROM [jhs_sys].[t_nyuukin_file_torikomi] AS INS
				 WHERE INS.nyuukin_torikomi_unique_no = @pKey
				   AND INS.nyuukin_date IS NOT NULL
		END



		--次の１件を取得する
    	FETCH NEXT FROM CUR_NYUUKIN_FILE
    	INTO @pKey
        	,@torikesiINS
        	,@torikesiDEL
        	,@userid
	END
	
	-- カーソルを閉じる
    CLOSE CUR_NYUUKIN_FILE

    -- カーソルのメモリを開放
    DEALLOCATE CUR_NYUUKIN_FILE
	
END

