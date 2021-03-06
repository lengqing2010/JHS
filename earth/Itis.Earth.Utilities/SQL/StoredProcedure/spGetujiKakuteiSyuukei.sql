-- =============================================
-- Description:	売掛金・買掛金月次集計処理
-- =============================================
CREATE PROCEDURE [jhs_sys].[spGetujiKakuteiSyuukei]
	@INSERT_COUNT_URIKAKE       INT         OUTPUT
	,@INSERT_COUNT_KAIKAKE      INT         OUTPUT
	,@UPDATE_COUNT_YOYAKU       INT         OUTPUT
	,@UPDATE_COUNT_KAGAMI       INT         OUTPUT
	,@SYORI_NENGETU             DATETIME    OUTPUT
	,@FROM_DATE                 DATETIME    OUTPUT
	,@TO_DATE                   DATETIME    OUTPUT
	,@USER_ID                   VARCHAR(30) OUTPUT
	,@ERR_NO        INT         OUTPUT
AS
BEGIN TRANSACTION
	SET NOCOUNT ON;
    
    DECLARE @MIN_DATE DATETIME
    DECLARE @ZENGETU_MATU_DATE DATETIME
    
    SET @MIN_DATE = '1900/1/1'

    --月次確定処理管理テーブルから予約中の対象年月(max)と、ユーザーID取得
    SELECT
        @SYORI_NENGETU = syori_nengetu
        , @USER_ID = ISNULL(upd_login_user_id, add_login_user_id) 
    FROM
        jhs_sys.t_getuji_kakutei_yoyaku_kanri 
    WHERE
        syori_nengetu = ( 
            SELECT
                MIN(syori_nengetu) 
            FROM
                jhs_sys.t_getuji_kakutei_yoyaku_kanri 
            WHERE
                syori_joukyou = 1
        ) 

    IF @SYORI_NENGETU IS NOT NULL
    BEGIN
        --伝票データの集計開始(処理年月の月初日)、終了年月(処理年月の末日)をセット
        SET @FROM_DATE = DATENAME(YEAR, @SYORI_NENGETU) + '-' + DATENAME(MONTH, @SYORI_NENGETU) + '-01'
        SET @TO_DATE = jhs_sys.fnGetLastDay(@SYORI_NENGETU)
        SET @ZENGETU_MATU_DATE = DATEADD(DAY, -1, @FROM_DATE)

        --念のため、同一処理年月のデータを削除(不要な可能性有り)
        --売掛金データテーブル
        DELETE 
        FROM
            [jhs_sys].[t_urikake_data] 
        WHERE
            taisyou_nengetu = @SYORI_NENGETU

        --買掛金データテーブル
        DELETE 
        FROM
            [jhs_sys].[t_kaikake_data] 
        WHERE
            taisyou_nengetu = @SYORI_NENGETU

        /*データ集計*/
        --売掛金データテーブルにデータをInsert
        INSERT 
        INTO [jhs_sys].[t_urikake_data] ( 
            taisyou_nengetu
            , seikyuu_saki_cd
            , seikyuu_saki_brc
            , seikyuu_saki_kbn
            , tougetu_kurikosi_zan
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
            , tougetu_nyuukin_goukei
            , tougetu_uriage_daka
            , tougetu_zei_nado
            , add_login_user_id
            , add_datetime
        ) 
        SELECT
            @SYORI_NENGETU
            , ssm.seikyuu_saki_cd
            , ssm.seikyuu_saki_brc
            , ssm.seikyuu_saki_kbn
            , ISNULL(kurikosid.tougetu_kurikosi_zan, 0)
            , ISNULL(nd.genkin, 0)
            , ISNULL(nd.kogitte, 0)
            , ISNULL(nd.furikomi, 0)
            , ISNULL(nd.tegata, 0)
            , ISNULL(nd.sousai, 0)
            , ISNULL(nd.nebiki, 0)
            , ISNULL(nd.sonota, 0)
            , ISNULL(nd.kyouryoku_kaihi, 0)
            , ISNULL(nd.kouza_furikae, 0)
            , ISNULL(nd.furikomi_tesuuryou, 0)
            , ISNULL(nd.tougetu_nyuukin_goukei, 0)
            , ISNULL(ud.tougetu_uriage_daka, 0)
            , ISNULL(ud.tougetu_zei_nado, 0)
            , @USER_ID
            , GETDATE() 
        FROM
            jhs_sys.m_seikyuu_saki ssm 
            LEFT OUTER JOIN ( 
                SELECT
                    u.seikyuu_saki_cd
                    , u.seikyuu_saki_brc
                    , u.seikyuu_saki_kbn
                    , sum(isnull(u.uri_gaku, 0)) tougetu_uriage_daka
                    , sum(CONVERT(BIGINT,isnull(u.sotozei_gaku, 0))) tougetu_zei_nado 
                FROM
                    jhs_sys.t_uriage_data u 
                WHERE
                    u.denpyou_uri_date BETWEEN @FROM_DATE AND @TO_DATE 
                    AND ISNULL(u.uri_keijyou_flg,0) = 1
                GROUP BY
                    u.seikyuu_saki_cd
                    , u.seikyuu_saki_brc
                    , u.seikyuu_saki_kbn
            ) ud 
                ON ssm.seikyuu_saki_cd = ud.seikyuu_saki_cd 
                AND ssm.seikyuu_saki_brc = ud.seikyuu_saki_brc 
                AND ssm.seikyuu_saki_kbn = ud.seikyuu_saki_kbn 
            LEFT OUTER JOIN ( 
                SELECT
                    n.seikyuu_saki_cd
                    , n.seikyuu_saki_brc
                    , n.seikyuu_saki_kbn
                    , sum(isnull(n.genkin, 0)) genkin
                    , sum(isnull(n.kogitte, 0)) kogitte
                    , sum(isnull(n.furikomi, 0)) furikomi
                    , sum(isnull(n.tegata, 0)) tegata
                    , sum(isnull(n.sousai, 0)) sousai
                    , sum(isnull(n.nebiki, 0)) nebiki
                    , sum(isnull(n.sonota, 0)) sonota
                    , sum(isnull(n.kyouryoku_kaihi, 0)) kyouryoku_kaihi
                    , sum(isnull(n.kouza_furikae, 0)) kouza_furikae
                    , sum(isnull(n.furikomi_tesuuryou, 0)) furikomi_tesuuryou
                    , sum(isnull(n.genkin, 0)) + sum(isnull(n.kogitte, 0)) + sum(isnull(n.furikomi, 0)) + sum(isnull(n.tegata, 0)) 
                    + sum(isnull(n.sousai, 0)) + sum(isnull(n.nebiki, 0)) + sum(isnull(n.sonota, 0)) + sum(isnull(n.kyouryoku_kaihi, 0)) 
                    + sum(isnull(n.kouza_furikae, 0)) + sum(isnull(n.furikomi_tesuuryou, 0)) tougetu_nyuukin_goukei 
                FROM
                    jhs_sys.t_nyuukin_data n 
                WHERE
                    n.nyuukin_date BETWEEN @FROM_DATE AND @TO_DATE 
                GROUP BY
                    n.seikyuu_saki_cd
                    , n.seikyuu_saki_brc
                    , n.seikyuu_saki_kbn
            ) nd 
                ON ssm.seikyuu_saki_cd = nd.seikyuu_saki_cd 
                AND ssm.seikyuu_saki_brc = nd.seikyuu_saki_brc 
                AND ssm.seikyuu_saki_kbn = nd.seikyuu_saki_kbn 
            LEFT OUTER JOIN ( 
                SELECT
                    tkurikosi.seikyuu_saki_cd
                    , tkurikosi.seikyuu_saki_brc
                    , tkurikosi.seikyuu_saki_kbn
                    , ISNULL(tkurikosi.uriage_goukei, 0) - ISNULL(tkurikosi.nyuukin_goukei, 0) + ISNULL(( 
                        SELECT
                            ISNULL(tougetu_kurikosi_zan, 0)
                        FROM
                            [jhs_sys].[t_urikake_data] 
                        WHERE
                            taisyou_nengetu = ( 
                                SELECT
                                    MAX(taisyou_nengetu) 
                                FROM
                                    [jhs_sys].[t_urikake_data] 
                                WHERE
                                    taisyou_nengetu <= @ZENGETU_MATU_DATE
                                    AND seikyuu_saki_cd = tkurikosi.seikyuu_saki_cd 
                                    AND seikyuu_saki_brc = tkurikosi.seikyuu_saki_brc
                                    AND seikyuu_saki_kbn = tkurikosi.seikyuu_saki_kbn
                            ) 
                            AND seikyuu_saki_cd = tkurikosi.seikyuu_saki_cd
                            AND seikyuu_saki_brc = tkurikosi.seikyuu_saki_brc
                            AND seikyuu_saki_kbn = tkurikosi.seikyuu_saki_kbn
                    ), 0) tougetu_kurikosi_zan
                FROM
                    ( 
                        SELECT
                            sm.seikyuu_saki_cd
                            , sm.seikyuu_saki_brc
                            , sm.seikyuu_saki_kbn
                            , ( 
                                SELECT
                                    sum(isnull(u2.uri_gaku, 0)) + sum(CONVERT(BIGINT,isnull(u2.sotozei_gaku, 0))) kurikosi_zan 
                                FROM
                                    jhs_sys.t_uriage_data u2 
                                WHERE
                                    u2.seikyuu_saki_cd = sm.seikyuu_saki_cd 
                                    AND u2.seikyuu_saki_brc = sm.seikyuu_saki_brc 
                                    AND u2.seikyuu_saki_kbn = sm.seikyuu_saki_kbn 
                                    AND u2.denpyou_uri_date BETWEEN @FROM_DATE AND @TO_DATE
                                    AND ISNULL(u2.uri_keijyou_flg,0) = 1
                            ) uriage_goukei
                            , ( 
                                SELECT
                                    sum(isnull(n2.genkin, 0)) + sum(isnull(n2.kogitte, 0)) + sum(isnull(n2.furikomi, 0)) + sum(isnull(n2.tegata, 0)) 
                                    + sum(isnull(n2.sousai, 0)) + sum(isnull(n2.nebiki, 0)) + sum(isnull(n2.sonota, 0)) + sum(isnull(n2.kyouryoku_kaihi, 0)) 
                                    + sum(isnull(n2.kouza_furikae, 0)) + sum(isnull(n2.furikomi_tesuuryou, 0)) kurikosi_zan 
                                FROM
                                    jhs_sys.t_nyuukin_data n2 
                                WHERE
                                    n2.seikyuu_saki_cd = sm.seikyuu_saki_cd 
                                    AND n2.seikyuu_saki_brc = sm.seikyuu_saki_brc 
                                    AND n2.seikyuu_saki_kbn = sm.seikyuu_saki_kbn 
                                    AND n2.nyuukin_date BETWEEN @FROM_DATE AND @TO_DATE
                            ) nyuukin_goukei 
                        FROM
                            jhs_sys.m_seikyuu_saki sm
                    ) tkurikosi 
                WHERE
                    tkurikosi.uriage_goukei IS NOT NULL 
                    OR tkurikosi.nyuukin_goukei IS NOT NULL
            ) kurikosid 
                ON ssm.seikyuu_saki_cd = kurikosid.seikyuu_saki_cd 
                AND ssm.seikyuu_saki_brc = kurikosid.seikyuu_saki_brc 
                AND ssm.seikyuu_saki_kbn = kurikosid.seikyuu_saki_kbn 
        WHERE
            ud.seikyuu_saki_cd IS NOT NULL 
            OR nd.seikyuu_saki_cd IS NOT NULL 
            OR kurikosid.seikyuu_saki_cd IS NOT NULL

        --処理件数、エラーNOセット
        SELECT
            @ERR_NO = @@ERROR
            , @INSERT_COUNT_URIKAKE = @@ROWCOUNT

        --エラーチェック
        IF @ERR_NO <> 0
        BEGIN
            -- トランザクションをロールバック（キャンセル）
            ROLLBACK TRANSACTION
            RETURN @ERR_NO
        END


        /*データ集計*/
        --買掛金データテーブルにデータをInsert
        INSERT 
        INTO [jhs_sys].[t_kaikake_data] ( 
            taisyou_nengetu
            , tys_kaisya_cd
            , shri_jigyousyo_cd
            , tougetu_kurikosi_zan
            , furikomi
            , sousai
            , tougetu_shri_goukei
            , tougetu_siire_nado
            , tougetu_zei_nado
            , add_login_user_id
            , add_datetime
        ) 
        SELECT
            @SYORI_NENGETU
            , tkm.tys_kaisya_cd
            , tkm.shri_jigyousyo_cd
            , ISNULL(kurikosid.tougetu_kurikosi_zan, 0)
            , ISNULL(hd.furikomi, 0)
            , ISNULL(hd.sousai, 0)
            , ISNULL(hd.tougetu_siharai_goukei, 0)
            , ISNULL(sd.tougetu_siire_daka, 0)
            , ISNULL(sd.tougetu_zei_nado, 0)
            , @USER_ID
            , GETDATE() 
        FROM
            ( 
                SELECT
                    tys_kaisya_cd
                    , shri_jigyousyo_cd 
                FROM
                    jhs_sys.m_tyousakaisya 
                WHERE
                    shri_jigyousyo_cd IS NOT NULL 
                GROUP BY
                    tys_kaisya_cd
                    , shri_jigyousyo_cd
            ) tkm 
            LEFT OUTER JOIN ( 
                --仕入データ集計
                SELECT
                    tth1.tys_kaisya_cd
                    , tth1.shri_jigyousyo_cd
                    , sum(isnull(si.siire_gaku, 0)) tougetu_siire_daka
                    , sum(CONVERT(BIGINT,isnull(si.sotozei_gaku, 0))) tougetu_zei_nado 
                FROM
                    jhs_sys.m_tyousakaisya tth1 
                    INNER JOIN jhs_sys.t_siire_data si 
                        ON tth1.tys_kaisya_cd = si.tys_kaisya_cd 
                        AND tth1.jigyousyo_cd = si.tys_kaisya_jigyousyo_cd 
                WHERE
                    si.denpyou_siire_date BETWEEN @FROM_DATE AND @TO_DATE 
                    AND ISNULL(si.siire_keijyou_flg,0) = 1
                GROUP BY
                    tth1.tys_kaisya_cd
                    , tth1.shri_jigyousyo_cd
            ) sd 
                ON tkm.tys_kaisya_cd = sd.tys_kaisya_cd 
                AND tkm.shri_jigyousyo_cd = sd.shri_jigyousyo_cd 
            LEFT OUTER JOIN ( 
                --支払データ集計
                SELECT
                    tth2.tys_kaisya_cd
                    , tth2.shri_jigyousyo_cd
                    , sum(isnull(sh.furikomi, 0)) furikomi
                    , sum(isnull(sh.sousai, 0)) sousai
                    , sum(isnull(sh.furikomi, 0)) + sum(isnull(sh.sousai, 0)) tougetu_siharai_goukei 
                FROM
                    jhs_sys.m_tyousakaisya tth2
                    INNER JOIN jhs_sys.t_siharai_data sh 
                        ON tth2.skk_shri_saki_cd = sh.skk_shri_saki_cd 
                        AND tth2.skk_jigyousyo_cd = sh.skk_jigyou_cd 
                WHERE
                    tth2.jigyousyo_cd = tth2.shri_jigyousyo_cd 
                    AND sh.siharai_date BETWEEN @FROM_DATE AND @TO_DATE 
                GROUP BY
                    tth2.tys_kaisya_cd
                    , tth2.shri_jigyousyo_cd
            ) hd 
                ON tkm.tys_kaisya_cd = hd.tys_kaisya_cd 
                AND tkm.shri_jigyousyo_cd = hd.shri_jigyousyo_cd 
            LEFT OUTER JOIN ( 
                --繰越残集計
                SELECT
                    tkurikosi.tys_kaisya_cd
                    , tkurikosi.shri_jigyousyo_cd
                    , ISNULL(tkurikosi.siire_goukei, 0) - ISNULL(tkurikosi.siharai_goukei, 0)  + ISNULL(( 
                        SELECT
                            ISNULL(tougetu_kurikosi_zan, 0)
                        FROM
                            [jhs_sys].[t_kaikake_data] 
                        WHERE
                            taisyou_nengetu = ( 
                                SELECT
                                    MAX(taisyou_nengetu) 
                                FROM
                                    [jhs_sys].[t_kaikake_data] 
                                WHERE
                                    taisyou_nengetu <= @ZENGETU_MATU_DATE
                                    AND tys_kaisya_cd = tkurikosi.tys_kaisya_cd
                                    AND shri_jigyousyo_cd = tkurikosi.shri_jigyousyo_cd
                            ) 
                            AND tys_kaisya_cd = tkurikosi.tys_kaisya_cd
                            AND shri_jigyousyo_cd = tkurikosi.shri_jigyousyo_cd
                        ), 0) tougetu_kurikosi_zan 
                FROM
                    ( 
                        SELECT
                            t2.tys_kaisya_cd
                            , t2.shri_jigyousyo_cd
                            , ( 
                                --仕入繰越残集計
                                SELECT
                                    sum(isnull(s2.siire_gaku, 0)) + sum(CONVERT(BIGINT,isnull(s2.sotozei_gaku, 0))) kurikosi_zan 
                                FROM
                                    jhs_sys.m_tyousakaisya tth3 
                                    INNER JOIN jhs_sys.t_siire_data s2 
                                        ON tth3.tys_kaisya_cd = s2.tys_kaisya_cd 
                                        AND tth3.jigyousyo_cd = s2.tys_kaisya_jigyousyo_cd 
                                WHERE
                                    tth3.tys_kaisya_cd = t2.tys_kaisya_cd 
                                    AND tth3.shri_jigyousyo_cd = t2.shri_jigyousyo_cd 
                                    AND s2.denpyou_siire_date BETWEEN @FROM_DATE AND @TO_DATE 
                                    AND ISNULL(s2.siire_keijyou_flg,0) = 1
                                GROUP BY
                                    tth3.tys_kaisya_cd
                                    , tth3.shri_jigyousyo_cd
                            ) siire_goukei
                            , ( 
                                --支払繰越残集計
                                SELECT
                                    sum(isnull(h2.furikomi, 0)) + sum(isnull(h2.sousai, 0)) kurikosi_zan 
                                FROM
                                    jhs_sys.m_tyousakaisya tt 
                                    INNER JOIN jhs_sys.t_siharai_data h2 
                                        ON tt.skk_shri_saki_cd = h2.skk_shri_saki_cd 
                                        AND tt.skk_jigyousyo_cd = h2.skk_jigyou_cd 
                                WHERE
                                    tt.tys_kaisya_cd = t2.tys_kaisya_cd 
                                    AND tt.jigyousyo_cd = t2.shri_jigyousyo_cd 
                                    AND h2.siharai_date BETWEEN @FROM_DATE AND @TO_DATE 
                                GROUP BY
                                    tt.tys_kaisya_cd
                                    , tt.shri_jigyousyo_cd
                            ) siharai_goukei 
                        FROM
                            jhs_sys.m_tyousakaisya t2 
                        WHERE
                            t2.jigyousyo_cd = t2.shri_jigyousyo_cd
                        GROUP BY
                            t2.tys_kaisya_cd
                            , t2.shri_jigyousyo_cd
                    ) tkurikosi 
                WHERE
                    tkurikosi.siire_goukei IS NOT NULL 
                    OR tkurikosi.siharai_goukei IS NOT NULL
            ) kurikosid 
                ON tkm.tys_kaisya_cd = kurikosid.tys_kaisya_cd 
                AND tkm.shri_jigyousyo_cd = kurikosid.shri_jigyousyo_cd 
        WHERE
            sd.tys_kaisya_cd IS NOT NULL 
            OR hd.tys_kaisya_cd IS NOT NULL 
            OR kurikosid.tys_kaisya_cd IS NOT NULL


        --処理件数、エラーNOセット
        SELECT
            @ERR_NO = @@ERROR
            , @INSERT_COUNT_KAIKAKE = @@ROWCOUNT

        --エラーチェック
        IF @ERR_NO <> 0
        BEGIN
            -- トランザクションをロールバック（キャンセル）
            ROLLBACK TRANSACTION
            RETURN @ERR_NO
        END


        /*月次確定予約管理テーブル更新*/
        --処理状況、実行日時を更新
        UPDATE [jhs_sys].[t_getuji_kakutei_yoyaku_kanri] 
        SET
            syori_joukyou = 9
            , syori_jikkou_datetime = GETDATE() 
            , upd_login_user_id = @USER_ID
            , upd_datetime = GETDATE() 
        WHERE
            syori_nengetu = @SYORI_NENGETU

        --エラーNOセット
        --処理件数、エラーNOセット
        SELECT
            @ERR_NO = @@ERROR
            , @UPDATE_COUNT_YOYAKU = @@ROWCOUNT

        --エラーチェック
        IF @ERR_NO <> 0
        BEGIN
            -- トランザクションをロールバック（キャンセル）
            ROLLBACK TRANSACTION
            RETURN @ERR_NO
        END

        /*請求鑑テーブル更新*/
        --印刷対象外の請求書用紙が指定されている請求鑑データを対象に、
        --請求書印刷日に現在日時をセットし、印刷済みとする。
        UPDATE [jhs_sys].[t_seikyuu_kagami] 
        SET
            seikyuusyo_insatu_date = GETDATE() 
            ,upd_login_user_id = @USER_ID
            ,upd_datetime = GETDATE() 
        WHERE
            SUBSTRING(kaisyuu_seikyuusyo_yousi_hannyou_cd, 1, 1) = '9' 
            AND seikyuusyo_insatu_date IS NULL 
            AND seikyuusyo_hak_date BETWEEN @FROM_DATE AND @TO_DATE 
            AND torikesi = 0

        --エラーNOセット
        --処理件数、エラーNOセット
        SELECT
            @ERR_NO = @@ERROR
            , @UPDATE_COUNT_KAGAMI = @@ROWCOUNT

        --エラーチェック
        IF @ERR_NO <> 0
        BEGIN
            -- トランザクションをロールバック（キャンセル）
            ROLLBACK TRANSACTION
            RETURN @ERR_NO
        END

    END
   
-- トランザクションをコミット
COMMIT TRANSACTION
RETURN @@ERROR
