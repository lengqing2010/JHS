-- =============================================
-- Description:    <売上データ作成トリガー(ON 汎用売上 AFTER INSERT, UPDATE)>
-- =============================================
CREATE TRIGGER [jhs_sys].[trUriageDataByHanyouOnInsUpd] 
   ON  [jhs_sys].[t_hannyou_uriage] 
   AFTER INSERT,UPDATE
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    --カーソル用変数宣言
    DECLARE @pKey               int             --入金取込ユニークNO
    DECLARE @torikesiINS        int             --取消（INSERTED用）
    DECLARE @torikesiDEL        int             --取消（DELETED用）
    DECLARE @denpyouUriDate     DATETIME        --伝票売上年月日
    DECLARE @minusFlg           int             --赤伝票発行フラグ
    DECLARE @plusFlg            int             --黒伝票発行フラグ
    DECLARE @maxNum             int             --最大伝票ユニークNO
    DECLARE @motoNum            int             --取消元伝票ユニークNO
    DECLARE @changeNum          int             --更新対象がある場合の伝票ユニークNO
    DECLARE @tableType          int             --テーブル種別
    DECLARE @UN                 CHAR(2)         --伝票種別：売上(UN)
    DECLARE @UR                 CHAR(2)         --伝票種別：売上取消(UR)
    DECLARE @userId             VARCHAR(30)     --登録ログインユーザーID
    DECLARE @userNm             VARCHAR(128)    --登録ログインユーザー名
    DECLARE @seikyuuSakiName    VARCHAR(100)    --請求先名
    DECLARE @delUriDate         DATETIME        --deletedテーブルの伝票売上年月日
    DECLARE @insSeikyuuDate     DATETIME        --請求書発行日(inserted)
    DECLARE @today              VARCHAR(20)     --本日日付(日付部分のみ)
    DECLARE @bitUriDateChange   BIT             --売上年月日変更BIT
    
    --初期化
    SET @pKey = ''
    SET @UN = 'UN';
    SET @UR = 'UR';
    SET @tableType = 9;         --テーブルタイプ(汎用売上：9)
    SET @today = CONVERT(VARCHAR,GETDATE(),111)

    /******************************************************
     *deletedテーブルのクローンを作成(INDEXを使用するため)*
     ******************************************************/
    SELECT * INTO #deleted_bk FROM deleted
    CREATE INDEX idx_tmp_deleted ON #deleted_bk(han_uri_unique_no)

    /******************************************
     *更新された行をカーソルで取得し、処理する*
     ******************************************/
    --カーソルの定義
    DECLARE CUR_URIAGE_HANNYOU
    CURSOR FOR 
        SELECT
             INS.han_uri_unique_no
            ,INS.torikesi
            ,DEL.torikesi
            ,INS.denpyou_uri_date
            ,ISNULL(INS.upd_login_user_id, INS.add_login_user_id)
            ,ISNULL(INS.upd_login_user_name, INS.add_login_user_name)
            ,VIW.seikyuu_saki_mei
            ,CASE ISNULL(CONVERT(BIGINT,DEL.tanka) * DEL.suu,0)
                WHEN 0 THEN NULL   --元々の売上額がゼロ円だった場合
                ELSE DEL.denpyou_uri_date
             END
            ,INS.seikyuu_date
            ,CASE 
                  WHEN ISNULL(INS.uri_date,0) <> ISNULL(DEL.uri_date,0) THEN 1
                  ELSE 0
             END
          FROM 
            inserted INS
          LEFT OUTER JOIN #deleted_bk DEL
            ON INS.han_uri_unique_no = DEL.han_uri_unique_no
          LEFT OUTER JOIN [jhs_sys].[v_seikyuu_saki_info] VIW
            ON INS.seikyuu_saki_cd  = VIW.seikyuu_saki_cd
           AND INS.seikyuu_saki_brc = VIW.seikyuu_saki_brc
           AND INS.seikyuu_saki_kbn = VIW.seikyuu_saki_kbn

    --カーソルを開く
    OPEN CUR_URIAGE_HANNYOU

    FETCH NEXT FROM CUR_URIAGE_HANNYOU
    INTO @pKey
        ,@torikesiINS
        ,@torikesiDEL
        ,@denpyouUriDate
        ,@userId
        ,@userNm
        ,@seikyuuSakiName
        ,@delUriDate
        ,@insSeikyuuDate
        ,@bitUriDateChange

    -- カーソルで取得した行が終端に達するまで処理を継続する
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- 伝票ユニークNOのリセット
        SET @maxNum = NULL;
        SET @motoNum = NULL;
        -- 伝票発行フラグのリセット
        SET @minusFlg = NULL;
        SET @plusFlg = NULL;

        /************************************************
         * 更新された汎用売上データのPKと同じPKをもつ、 *
         * 売上データの最大伝票ユニークNOを取得         *
         ************************************************/
        SELECT @maxNum = MAX(URI.denpyou_unique_no)
          FROM [jhs_sys].[t_uriage_data] URI
         WHERE URI.himoduke_cd = @pKey
           AND URI.himoduke_table_type = @tableType

        /***************************************************************
        * 上で取得した最大伝票ユニークNOの取消元伝票ユニークNOを取得   *
        ****************************************************************/
        --「取得○」：直近がマイナス伝票
        --「取得×」：直近がプラス伝票
        SELECT @motoNum = URI.torikesi_moto_denpyou_unique_no
          FROM [jhs_sys].[t_uriage_data] URI
         WHERE 1=1
           AND URI.denpyou_unique_no = @maxNum
        
        /********************************************************
        * 更新された入金取込ユニークNOのデータと、入金データの
        * 比較対象が一つでも異なる場合、伝票ユニークNOを@changeNumに格納する
        ********************************************************/
        SELECT @changeNum = URI.denpyou_unique_no
          FROM [jhs_sys].[t_hannyou_uriage] INS
         INNER JOIN [jhs_sys].[t_uriage_data] URI
            ON URI.denpyou_unique_no = @maxNum
         WHERE INS.han_uri_unique_no = @pKey
           AND (
                   ISNULL(URI.kbn,'')                 <> ISNULL(INS.kbn,'')                   --区分
                OR ISNULL(URI.bangou,'')              <> ISNULL(INS.bangou,'')                --番号(保証書NO)
                OR ISNULL(URI.uri_date,'')            <> ISNULL(INS.uri_date,'')              --売上年月日
                OR ISNULL(URI.denpyou_uri_date,'')    <> ISNULL(@denpyouUriDate,'')           --伝票売上年月日
                OR ISNULL(URI.uri_keijyou_flg,'')     <> ISNULL(INS.uri_keijyou_flg,'')       --売上処理FLG(売上計上FLG)
                OR ISNULL(URI.seikyuu_date,'')        <> ISNULL(INS.seikyuu_date,'')          --請求年月日
                OR ISNULL(URI.seikyuu_saki_cd, '')    <> ISNULL(INS.seikyuu_saki_cd, '')      --請求先コード
                OR ISNULL(URI.seikyuu_saki_brc, '')   <> ISNULL(INS.seikyuu_saki_brc, '')     --請求先枝番
                OR ISNULL(URI.seikyuu_saki_kbn, '')   <> ISNULL(INS.seikyuu_saki_kbn, '')     --請求先区分
                OR ISNULL(URI.syouhin_cd,'')          <> ISNULL(INS.syouhin_cd,'')            --商品コード
                OR ISNULL(URI.hinmei,'')              <> ISNULL(INS.hin_mei,'')               --品名
                OR ISNULL(URI.suu,'')                 <> ISNULL(INS.suu,'')                   --数量
                OR ISNULL(URI.uri_gaku,'')            <> ISNULL(CONVERT(BIGINT,INS.tanka) * INS.suu,'') --売上金額
                OR ISNULL(URI.sotozei_gaku,'')        <> ISNULL(INS.syouhizei_gaku,'')        --外税額
                );

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

        -- 更新対象に紐付く伝票データがなく、
        -- 更新前データの売上年月日が未指定か取消状態だった場合、
        -- 新規データとして扱う
        IF @maxNum IS NULL AND (@delUriDate IS NULL OR @torikesiDEL <> 0)
            -- 取消ではない場合
            IF @torikesiINS = 0
                SET @plusFlg = 1;

        /***********************************************
        * フラグがある場合、赤伝票（マイナス伝票）発行 *
        ************************************************/
        IF @minusFlg = 1
        BEGIN
            INSERT 
            INTO [jhs_sys].[t_uriage_data] ( 
                denpyou_syubetu
                , torikesi_moto_denpyou_unique_no
                , kbn
                , bangou
                , himoduke_cd
                , himoduke_table_type
                , uri_date
                , denpyou_uri_date
                , uri_keijyou_flg
                , kameiten_cd
                , seikyuu_date
                , seikyuu_saki_cd
                , seikyuu_saki_brc
                , seikyuu_saki_kbn
                , seikyuu_saki_mei
                , syouhin_cd
                , hinmei
                , suu
                , tani
                , tanka
                , syanai_genka
                , uri_gaku
                , sotozei_gaku
                , zei_kbn
                , add_login_user_id
                , add_login_user_name
                , add_datetime
                , upd_login_user_id
                , upd_datetime
            ) 
            SELECT
                @UR
                , URIOLD.denpyou_unique_no
                , URIOLD.kbn
                , URIOLD.bangou
                , URIOLD.himoduke_cd
                , @tableType
                , URIOLD.uri_date
                , CASE 
                    WHEN @plusFlg IS NULL --削除に伴なう発行
                        THEN (CASE
						  	                WHEN URIOLD.uri_keijyou_flg = '1'
						  	                THEN @today
						  	                ELSE URIOLD.denpyou_uri_date
			                        END)
                    ELSE --更新に伴なう発行
                        (CASE @bitUriDateChange 
                            WHEN 1 THEN URIOLD.denpyou_uri_date
                            ELSE ISNULL(@denpyouUriDate, @today) 
                        END)
                    END
                , URIOLD.uri_keijyou_flg
                , URIOLD.kameiten_cd
                , CASE 
                    WHEN @plusFlg IS NULL  --削除に伴なう発行
                    THEN [jhs_sys].[fnGetSeikyuuDate] ( 
                        URIOLD.seikyuu_saki_cd
                        , URIOLD.seikyuu_saki_brc
                        , URIOLD.seikyuu_saki_kbn
                        , @today
                    ) 
                    ELSE --更新に伴なう発行
                      CASE @bitUriDateChange 
                        WHEN 1 THEN URIOLD.seikyuu_date 
                        ELSE ISNULL( 
                          @insSeikyuuDate
                          , [jhs_sys].[fnGetSeikyuuDate]( 
                              URIOLD.seikyuu_saki_cd
                              , URIOLD.seikyuu_saki_brc
                              , URIOLD.seikyuu_saki_kbn
                              , @today
                          )
                      )END
                END
                , URIOLD.seikyuu_saki_cd
                , URIOLD.seikyuu_saki_brc
                , URIOLD.seikyuu_saki_kbn
                , URIOLD.seikyuu_saki_mei
                , URIOLD.syouhin_cd
                , URIOLD.hinmei
                , URIOLD.suu * - 1
                , URIOLD.tani
                , URIOLD.tanka
                , URIOLD.syanai_genka
                , URIOLD.uri_gaku * - 1
                , URIOLD.sotozei_gaku * - 1
                , URIOLD.zei_kbn
                , @userId
                , @userNm
                , GETDATE()
                , NULL
                , NULL 
            FROM
                [jhs_sys].[t_uriage_data] URIOLD 
            WHERE
                URIOLD.denpyou_unique_no = @maxNum; 
        END

        /***************************************************
        * フラグがある場合、もしくは伝票ユニークNOの最大値 *
        * が取得出来ない場合、黒伝票（プラス伝票）発行     *
        ****************************************************/
        IF @plusFlg = 1 
        BEGIN
            INSERT INTO [jhs_sys].[t_uriage_data]
                (
                denpyou_syubetu
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
                    @UN
                    ,NULL
                    ,INS.kbn
                    ,INS.bangou
                    ,INS.han_uri_unique_no
                    ,@tableType
                    ,INS.uri_date
                    ,@denpyouUriDate
                    ,INS.uri_keijyou_flg
                    ,NULL
                    ,INS.seikyuu_date
                    ,INS.seikyuu_saki_cd
                    ,INS.seikyuu_saki_brc
                    ,INS.seikyuu_saki_kbn
                    ,@seikyuuSakiName
                    ,INS.syouhin_cd
                    ,INS.hin_mei
                    ,INS.suu
                    ,MS.tani
                    ,INS.tanka
                    ,MS.syanai_genka
                    ,CONVERT(BIGINT,INS.tanka) * INS.suu
                    ,INS.syouhizei_gaku
                    ,INS.zei_kbn
                    ,@userId
                    ,@userNm
                    ,GETDATE()
                    ,NULL
                    ,NULL
                  FROM [jhs_sys].[t_hannyou_uriage] AS INS
                  LEFT OUTER JOIN [jhs_sys].[m_syouhin] MS
                    ON INS.syouhin_cd = MS.syouhin_cd
                  LEFT OUTER JOIN [jhs_sys].[m_syouhizei] MSZ
                    ON INS.zei_kbn = MSZ.zei_kbn
                 WHERE INS.han_uri_unique_no = @pKey
                   AND INS.denpyou_uri_date IS NOT NULL
                   AND ISNULL(CONVERT(BIGINT,INS.tanka) * INS.suu,0) <> 0 
        END

        --次の１件を取得する
        FETCH NEXT FROM CUR_URIAGE_HANNYOU
        INTO @pKey
            ,@torikesiINS
            ,@torikesiDEL
            ,@denpyouUriDate
            ,@userId
            ,@userNm
            ,@seikyuuSakiName
            ,@delUriDate
            ,@insSeikyuuDate
            ,@bitUriDateChange
    END
    
    -- カーソルを閉じる
    CLOSE CUR_URIAGE_HANNYOU

    -- カーソルのメモリを開放
    DEALLOCATE CUR_URIAGE_HANNYOU
END






