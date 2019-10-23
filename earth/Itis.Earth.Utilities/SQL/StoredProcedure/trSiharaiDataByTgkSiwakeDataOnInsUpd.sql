-- =============================================
-- Description: <支払データ作成トリガー(ON 統合会計仕訳データ AFTER INSERT, UPDATE)>
-- =============================================
CREATE TRIGGER [jhs_sys].[trSiharaiDataByTgkSiwakeDataOnInsUpd] 
   ON  jhs_sys.t_tgk_siwake_data 
   AFTER INSERT,UPDATE
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    --カーソル用変数宣言
    DECLARE @pKey               int         --仕訳ユニークNO(INSERTED)
    DECLARE @tgkJigyouCd        VARCHAR(10) --統合会計事業所コード
    DECLARE @tgkShriSakiCd      VARCHAR(10) --統合会計支払先コード
    DECLARE @shriSakiMei        VARCHAR(100)--統合会計支払先コード
    DECLARE @siharaiDate        datetime    --支払年月日
    DECLARE @furikomi           bigint      --支払額 [振込]
    DECLARE @sousai             bigint      --支払額 [相殺]
    DECLARE @tekiyou            VARCHAR(200)--摘要名
    DECLARE @userid             VARCHAR(30) --登録ログインユーザーID
    DECLARE @BindString         CHAR(1)     --囲い文字[文字列型]:統合会計側
    
    DECLARE @minusFlg           int         --赤伝票発行フラグ
    DECLARE @plusFlg            int         --黒伝票発行フラグ
    DECLARE @maxNum             int         --同一仕訳ユニークNOの最大伝票ユニークNO
    DECLARE @motoNum            int         --取消元伝票ユニークNO
    DECLARE @changeNum          int         --更新対象がある場合の伝票ユニークNO
    DECLARE @HN                 CHAR(2)     --伝票種別：支払
    DECLARE @HR                 CHAR(2)     --伝票種別：支払取消

    --初期化
    SET @HN = 'HN';
    SET @HR = 'HR';
    SET @BindString = '"';

    /******************************************************
     *deletedテーブルのクローンを作成(INDEXを使用するため)*
     ******************************************************/
    SELECT * INTO #deleted_bk FROM deleted
    CREATE INDEX idx_tmp_deleted ON #deleted_bk(siwake_unique_no)

    /******************************************
     *更新された行をカーソルで取得し、処理する*
     ******************************************/
    --カーソルの定義
    DECLARE CUR_SIHARAI_DATA
    CURSOR FOR 
        SELECT
            INS.siwake_unique_no
/*2013/7/3修正
            , SUBSTRING(REPLACE(INS.dr_other_cd_a,@BindString,''), 1, 4) tgk_jigyou_cd    
            , CASE REPLACE(INS.dr_other_cd_a,@BindString,'')
                WHEN '99999999999999' THEN '999999'
                ELSE '00' + SUBSTRING(REPLACE(INS.dr_other_cd_a,@BindString,''), 9, 4)
                END END tgk_shri_saki_cd
            , REPLACE(INS.dr_other_name,@BindString,'') kari_aitesaki_mei*/
            , CASE 	WHEN REPLACE(INS.data_category,@BindString,'')='F58' THEN 'YMP8' 
			WHEN REPLACE(INS.data_category,@BindString,'') in ('W001','W002') and REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='03919' and REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'') = '21202' AND REPLACE(INS.CR_SUB_ACCOUNT_CD_B,@BindString,'') = '9300' THEN SUBSTRING(REPLACE(INS.DR_OTHER_CD_A,@BindString,''), 1, 4)
		ELSE SUBSTRING(REPLACE(INS.dr_other_cd_a,@BindString,''), 1, 4) END tgk_jigyou_cd    
            , CASE 	WHEN REPLACE(INS.data_category,@BindString,'')='F58' THEN '007246' 
			WHEN REPLACE(INS.data_category,@BindString,'') in ('W001','W002') and REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='03919' and REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'') = '21202' AND REPLACE(INS.CR_SUB_ACCOUNT_CD_B,@BindString,'') = '9300' THEN  '00' + SUBSTRING(REPLACE(INS.DR_OTHER_CD_A,@BindString,''), 9, 4)
			WHEN REPLACE(INS.dr_other_cd_a,@BindString,'')='99999999999999' THEN '999999'
                	ELSE '00' + SUBSTRING(REPLACE(INS.dr_other_cd_a,@BindString,''), 9, 4)	END tgk_shri_saki_cd
            , CASE 	WHEN REPLACE(INS.data_category,@BindString,'')='F58' THEN '（株）日本住宅保証検査機構' 
			WHEN REPLACE(INS.data_category,@BindString,'') in ('W001','W002') and REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='03919' and REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'') = '21202' AND REPLACE(INS.CR_SUB_ACCOUNT_CD_B,@BindString,'') = '9300' THEN REPLACE(INS.DR_OTHER_NAME,@BindString,'')
		ELSE REPLACE(INS.dr_other_name,@BindString,'') END kari_aitesaki_mei
            , SUBSTRING(REPLACE(INS.account_date,@BindString,''), 1, 4) + '/' + 
              SUBSTRING(REPLACE(INS.account_date,@BindString,''), 5, 2) + '/' + 
              SUBSTRING(REPLACE(INS.account_date,@BindString,''), 7, 2) keijou_date
/*2012/11/28修正
            , CASE REPLACE(INS.data_category,@BindString,'')
                WHEN 'W001' THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULLの場合0
                WHEN 'F17' THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULLの場合0
                ELSE 0 
                END furikomi
            , CASE REPLACE(INS.data_category,@BindString,'')
                WHEN 'F19' THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULLの場合0
                ELSE 0 
                END sousai
*/
            , CASE	WHEN REPLACE(INS.data_category,@BindString,'')= 'F17' AND REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='21202' THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULLの場合0
			WHEN REPLACE(INS.data_category,@BindString,'') in ('W001','W002') THEN
				CASE 	WHEN 	(REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='21202' AND REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'')<>'49726')
/*					OR																		--2013/11/6削除
						(REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='49726' AND REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'') = '01228')*/	--2013/11/6削除
					OR
						(REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='49726' AND REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'') = '22101' AND REPLACE(INS.CR_SUB_ACCOUNT_CD_B,@BindString,'') = '018')
					THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULLの場合0
					WHEN (REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='65234' AND REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'') = '21202')	/*2013/7/3追加*/
					THEN CAST(ISNULL(INS.dr_journal_line_amount,0)*(-1.05) AS INT)  --NULLの場合0								/*2013/7/3追加*/
	                		ELSE 0 END
			WHEN REPLACE(INS.data_category,@BindString,'')= 'F58' AND REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='21202' THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULLの場合0	/*2013/7/3追加*/
		ELSE 0
                END furikomi
            , CASE	WHEN REPLACE(INS.data_category,@BindString,'')='F19' AND REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='21202' THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULLの場合0
			WHEN REPLACE(INS.data_category,@BindString,'') in ('W001','W002') AND REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='49726' THEN
				CASE WHEN 	/*REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'')='01518'		--2013/11/6削除
					OR*/										--2013/11/6削除
						(REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'') = '22101' AND REPLACE(INS.CR_SUB_ACCOUNT_CD_B,@BindString,'') = '777')
				THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULLの場合0
	                		ELSE 0 END
		ELSE 0
                END sousai

            , REPLACE(INS.journal_description,@BindString,'') tekiyou_kanji
            , ISNULL(INS.upd_login_user_id, INS.add_login_user_id) login_user_id 
        FROM
            inserted INS 
            LEFT OUTER JOIN #deleted_bk DEL 
                ON INS.siwake_unique_no = DEL.siwake_unique_no 
        WHERE
--            REPLACE(INS.data_category,@BindString,'') IN ('W001', 'F17', 'F19') 2012/11/28修正
--            (REPLACE(INS.data_category,@BindString,'') IN ('F17', 'F19') AND REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='21202')	2013/7/3修正
            (REPLACE(INS.data_category,@BindString,'') IN ('F17', 'F19','F58') AND REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='21202')
	OR
            (REPLACE(INS.data_category,@BindString,'') IN ('W001', 'W002') AND
		((REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='21202' AND REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'')<>'49726')
		OR
		 (REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='49726' AND 
			(/*REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'') in ('01518','01228')		--2013/11/6削除
			OR*/											--2013/11/6削除
			(REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'')='22101' AND REPLACE(INS.CR_SUB_ACCOUNT_CD_B,@BindString,'') in ('018','777'))
			)
		)
		OR
		(REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='65234' AND REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'')='21202'))	--2013/7/3追加
	  )
    --カーソルを開く
    OPEN CUR_SIHARAI_DATA

    --カーソルより最初の一行を取得
    FETCH NEXT FROM CUR_SIHARAI_DATA
    INTO @pKey
        ,@tgkJigyouCd
        ,@tgkShriSakiCd
        ,@shriSakiMei
        ,@siharaiDate
        ,@furikomi
        ,@sousai
        ,@tekiyou
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
        * 更新されたデータの仕訳ユニークNOと紐付く
        * 支払データの伝票ユニークNOの最大値を取得
        ********************************************************/
        SELECT @maxNum = MAX(SHR.denpyou_unique_no)
          FROM [jhs_sys].[t_siharai_data] SHR
         WHERE SHR.tgk_siwake_unique_no = @pKey
        
        /********************************************************
        * 更新された仕訳ユニークNOのデータと、支払データの
        * 比較対象が一つでも異なる場合、伝票ユニークNOを@changeNumに格納する
        ********************************************************/
        SELECT
            @changeNum = SHR.denpyou_unique_no 
        FROM
            [jhs_sys].[t_siharai_data] SHR 
        WHERE
            SHR.denpyou_unique_no = @maxNum 
            AND SHR.torikesi_moto_denpyou_unique_no IS NULL --直近伝票が取消伝票で無い事を指定
            AND ( 
                ISNULL(SHR.skk_jigyou_cd, '') <> ISNULL(@tgkJigyouCd, '') 
                OR ISNULL(SHR.skk_shri_saki_cd, '') <> ISNULL(@tgkShriSakiCd, '') 
                OR ISNULL(SHR.siharai_date, '') <> ISNULL(@siharaiDate, '') 
                OR ISNULL(SHR.furikomi, '') <> ISNULL(@furikomi, '') 
                OR ISNULL(SHR.sousai, '') <> ISNULL(@sousai, '') 
                OR ISNULL(SHR.tekiyou_mei, '') <> ISNULL(@tekiyou, '')
            ) 
        
        /****************************************************
         * 更新処理の判定                                   *
         *    @minusFlg = 1 → プラス伝票発行               *
         *    @plusFlg = 1 → マイナス伝票発行              *
         ****************************************************/
 -- 更新対象データと更新対象に紐付く直近の伝票データに差異がある場合
        -- マイナス伝票を発行する
        IF @changeNum IS NOT NULL 
            SET @minusFlg = 1

        -- 支払年月日が指定されており、振込、相殺の何れかの支払額がゼロ以外の場合
        -- プラス伝票を発行する
        IF @siharaiDate IS NOT NULL AND (ISNULL(@furikomi, 0) <> 0 OR ISNULL(@sousai, 0) <> 0)
            SET @plusFlg = 1

        /***********************************************
        * フラグがある場合、赤伝票（マイナス伝票）発行 *
        ************************************************/
        IF @minusFlg = 1
        BEGIN
            INSERT INTO [jhs_sys].[t_siharai_data] ( 
                    denpyou_syubetu
                    ,torikesi_moto_denpyou_unique_no
                    ,tgk_siwake_unique_no
                    ,skk_jigyou_cd
                    ,skk_shri_saki_cd
                    ,shri_saki_mei
                    ,siharai_date
                    ,furikomi
                    ,sousai
                    ,tekiyou_mei
                    ,add_login_user_id
                    ,add_login_user_name
                    ,add_datetime
                )
                SELECT
                    @HR
                    ,SHIOLD.denpyou_unique_no
                    ,SHIOLD.tgk_siwake_unique_no
                    ,SHIOLD.skk_jigyou_cd
                    ,SHIOLD.skk_shri_saki_cd
                    ,SHIOLD.shri_saki_mei
                    ,SHIOLD.siharai_date
                    ,SHIOLD.furikomi * -1
                    ,SHIOLD.sousai * -1
                    ,SHIOLD.tekiyou_mei
                    ,@userid
                    ,jhs_sys.fnGetAddUpdUserName(@userid)
                    ,GETDATE()
                  FROM [jhs_sys].[t_siharai_data] SHIOLD
                 WHERE SHIOLD.denpyou_unique_no = @changeNum
        END
        
        
        /***************************************************
        * フラグがある場合、もしくは伝票ユニークNOの最大値 *
        * が取得出来ない場合、黒伝票（プラス伝票）発行     *
        ****************************************************/
        IF @plusFlg = 1 
        BEGIN
            INSERT INTO [jhs_sys].[t_siharai_data] ( 
                    denpyou_syubetu
                    ,tgk_siwake_unique_no
                    ,skk_jigyou_cd
                    ,skk_shri_saki_cd
                    ,shri_saki_mei
                    ,siharai_date
                    ,furikomi
                    ,sousai
                    ,tekiyou_mei
                    ,add_login_user_id
                    ,add_login_user_name
                    ,add_datetime
                ) 
                SELECT
                    @HN
                    ,@pKey
                    ,@tgkJigyouCd
                    ,@tgkShriSakiCd
                    ,@shriSakiMei
                    ,@siharaiDate
                    ,@furikomi
                    ,@sousai
                    ,@tekiyou
                    ,@userid
                    ,jhs_sys.fnGetAddUpdUserName(@userid)
                    ,GETDATE()
        END

        --次の１件を取得する
        FETCH NEXT FROM CUR_SIHARAI_DATA
        INTO @pKey
            ,@tgkJigyouCd
            ,@tgkShriSakiCd
            ,@shriSakiMei
            ,@siharaiDate
            ,@furikomi
            ,@sousai
            ,@tekiyou
            ,@userid
    END
    
    -- カーソルを閉じる
    CLOSE CUR_SIHARAI_DATA

    -- カーソルのメモリを開放
    DEALLOCATE CUR_SIHARAI_DATA
    
END











