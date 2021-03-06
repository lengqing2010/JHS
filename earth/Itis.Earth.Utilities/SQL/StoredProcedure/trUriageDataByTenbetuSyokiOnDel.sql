-- ==========================================================================
-- Description: <売上データ作成トリガー(ON 店別初期請求 AFTER DELETE)>
-- ==========================================================================
CREATE TRIGGER [jhs_sys].[trUriageDataByTenbetuSyokiOnDel]
   ON  jhs_sys.t_tenbetu_syoki_seikyuu
   AFTER DELETE, UPDATE
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    --カーソル用変数宣言
    DECLARE @miseCd             CHAR(5)     --店コード
    DECLARE @bunruiCd           CHAR(3)     --分類コード
    DECLARE @pKey               VARCHAR(30) --紐付けコード
    DECLARE @maxNum             int         --最大伝票ユニークNO
    DECLARE @maxChengeNum       int         --取消元伝票ユニークNO
    DECLARE @tableType          int
    DECLARE @strSeq             CHAR(3)
    DECLARE @UR                 CHAR(2)
    DECLARE @today              VARCHAR(20)  --本日日付(日付部分のみ)

    --初期化
    SET @pKey = ''
    SET @tableType = 3;         --テーブルタイプ(店別初期請求：3)
    SET @strSeq = '$$$';        --紐付けコードのセパレート文字列
    SET @UR = 'UR';             --伝票種別：売上取消(UR)
    SET @today = CONVERT(VARCHAR,GETDATE(),111)

    /******************************************
     *更新された行をカーソルで取得し、処理する*
     ******************************************/
    --カーソルの定義
    DECLARE CUR_URIAGE_TENBETU_SYOKI_DEL
    CURSOR FOR
        SELECT DEL.mise_cd
              ,DEL.bunrui_cd
              ,DEL.mise_cd + @strSeq
             + DEL.bunrui_cd
          FROM deleted AS DEL
          LEFT OUTER JOIN inserted AS INS
            ON DEL.mise_cd          = INS.mise_cd
           AND DEL.bunrui_cd        = INS.bunrui_cd
         WHERE
               INS.mise_cd IS NULL

    --カーソルを開く
    OPEN CUR_URIAGE_TENBETU_SYOKI_DEL

    --カーソルより最初の一行を取得
    FETCH NEXT FROM CUR_URIAGE_TENBETU_SYOKI_DEL
    INTO @miseCd
        ,@bunruiCd
        ,@pKey

    -- カーソルで取得した行が終端に達するまで処理を継続する
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- 伝票ユニークNOのリセット
        SET @maxNum = NULL;

        BEGIN
        /****************************************************
         * 削除された店別初期請求データのPKと同じPKをもつ、 *
         * 売上データの最大伝票ユニークNOを取得             *
         ****************************************************/
            SELECT @maxNum = MAX(URI.denpyou_unique_no)
              FROM [jhs_sys].[t_uriage_data] URI
             WHERE URI.himoduke_cd = @pKey
             AND URI.himoduke_table_type = @tableType
        END

        /********************************************************
         * 削除された行が売上データにある場合、赤伝票を登録する *
         ********************************************************/
        IF @maxNum IS NOT NULL
        BEGIN

            --削除を行ったログインユーザーIDを、ローカル一時テーブルから取得
            DECLARE @userid             VARCHAR(30) --ユーザーID
            IF object_id('tempdb..#TEMP_USER_INFO_FOR_TRIGGER') is not null
              SELECT TOP 1 @userid = login_user_id FROM #TEMP_USER_INFO_FOR_TRIGGER
            ELSE
              SET @userid = '$delete_user_unknown$'

            INSERT INTO [jhs_sys].[t_uriage_data]
                (
                denpyou_no
                ,denpyou_syubetu
                ,torikesi_moto_denpyou_unique_no
                ,kbn
                ,bangou
                ,himoduke_cd
                ,himoduke_table_type
                ,uri_date
                ,denpyou_uri_date
                ,uri_keijyou_flg
                ,kameiten_cd
                ,seikyuu_date
                ,seikyuu_saki_cd
                ,seikyuu_saki_brc
                ,seikyuu_saki_kbn
                ,seikyuu_saki_mei
                ,syouhin_cd
                ,hinmei
                ,suu
                ,tani
                ,tanka
                ,syanai_genka
                ,uri_gaku
                ,sotozei_gaku
                ,zei_kbn
                ,add_login_user_id
                ,add_login_user_name
          ,add_datetime
                ,upd_login_user_id
                ,upd_datetime
                )
            SELECT
                    NULL
                    ,@UR
                    ,URIOLD.denpyou_unique_no
                    ,URIOLD.kbn
                    ,URIOLD.bangou
                    ,URIOLD.himoduke_cd
                    ,@tableType
                    ,URIOLD.uri_date
					,(CASE 
						WHEN URIOLD.uri_keijyou_flg = '1'
						THEN @today
						ELSE URIOLD.denpyou_uri_date
					  END)
                    ,URIOLD.uri_keijyou_flg
                    ,URIOLD.kameiten_cd
                    , [jhs_sys].[fnGetSeikyuuDate]( 
                        URIOLD.seikyuu_saki_cd
                        , URIOLD.seikyuu_saki_brc
                        , URIOLD.seikyuu_saki_kbn
                        , @today
                    ) 
                    ,URIOLD.seikyuu_saki_cd
                    ,URIOLD.seikyuu_saki_brc
                    ,URIOLD.seikyuu_saki_kbn
                    ,URIOLD.seikyuu_saki_mei
                    ,URIOLD.syouhin_cd
                    ,URIOLD.hinmei
                    ,URIOLD.suu * -1
                    ,URIOLD.tani
                    ,URIOLD.tanka
                    ,URIOLD.syanai_genka
                    ,URIOLD.uri_gaku * -1
                    ,URIOLD.sotozei_gaku * -1
                    ,URIOLD.zei_kbn
					,@userid
                    ,jhs_sys.fnGetAddUpdUserName(@userid)
                    ,GETDATE()
                    ,NULL
                    ,NULL
                  FROM [jhs_sys].[t_uriage_data] URIOLD
                 WHERE URIOLD.denpyou_unique_no = @maxNum
                 AND URIOLD.torikesi_moto_denpyou_unique_no IS NULL;

        END
        -- 次の１件を取得する
        FETCH NEXT FROM
            CUR_URIAGE_TENBETU_SYOKI_DEL
        INTO @miseCd
            ,@bunruiCd
            ,@pKey
    END

    -- カーソルを閉じる
    CLOSE CUR_URIAGE_TENBETU_SYOKI_DEL

    -- カーソルのメモリを開放
    DEALLOCATE CUR_URIAGE_TENBETU_SYOKI_DEL
END
