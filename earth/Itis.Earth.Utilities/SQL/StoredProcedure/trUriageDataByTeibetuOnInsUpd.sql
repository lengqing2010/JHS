-- =============================================
-- Description: <売上データ作成トリガー(ON 邸別請求 AFTER INSERT, UPDATE)>
-- =============================================
CREATE TRIGGER [jhs_sys].[trUriageDataByTeibetuOnInsUpd] 
   ON  [jhs_sys].[t_teibetu_seikyuu] 
   AFTER INSERT,UPDATE
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    --カーソル用変数宣言
    DECLARE @kbn                CHAR(1)     --区分
    DECLARE @bangou             VARCHAR(10) --番号
    DECLARE @bunruiCd           VARCHAR(3)  --分類コード
    DECLARE @gamenHyoujiNo      int         --画面表示NO
    DECLARE @syouhinCd          VARCHAR(10) --商品コード
    DECLARE @kameitenCd         VARCHAR(10) --加盟店コード
    DECLARE @denpyouUriDate     DATETIME    --伝票売上年月日
    DECLARE @seikyuuSakiCd      VARCHAR(10)   --請求先コード
    DECLARE @seikyuuSakiBrc     VARCHAR(10)   --請求先枝番
    DECLARE @seikyuuSakiKbn     VARCHAR(1)    --請求先区分
    DECLARE @seikyuuSakiName    VARCHAR(100)  --請求先名
    DECLARE @insSeikyuuDate     DATETIME      --請求書発行日(inserted)
    DECLARE @bitUriDateChange   BIT         --売上年月日変更BIT
    DECLARE @pKey               VARCHAR(30) --紐付けコード
    DECLARE @pKey2              VARCHAR(30) --紐付けコード2
    DECLARE @maxNum             int         --最大伝票ユニークNO
    DECLARE @maxChengeNum       int         --取消元伝票ユニークNO
    DECLARE @tableType          int
    DECLARE @strSeq             CHAR(3)
    DECLARE @strSyouhinSeq      CHAR(1)
    DECLARE @UN                 CHAR(2)
    DECLARE @UR                 CHAR(2)
    DECLARE @userid             VARCHAR(30) --登録ログインユーザーID
    DECLARE @delUriDate         DATETIME    --deletedテーブルの(伝票)売上年月日
    DECLARE @bunruiCdSub        VARCHAR(2)  --分類コード(頭2桁SUBSTRING)
    DECLARE @today              VARCHAR(20)  --本日日付(日付部分のみ)

    --初期化
    SET @pKey = ''
    SET @pKey2 = ''
    SET @tableType = 1;         --テーブルタイプ(邸別請求：１)
    SET @strSeq = '$$$';        --紐付けコードのセパレート文字列
    SET @strSyouhinSeq = '_';   --紐付けコードのセパレート文字列(商品分類用)
    SET @UN = 'UN';             --伝票種別：売上(UN)
    SET @UR = 'UR';             --伝票種別：売上取消(UR)
    SET @today = CONVERT(VARCHAR,GETDATE(),111)

    /******************************************************
     *deletedテーブルのクローンを作成(INDEXを使用するため)*
     ******************************************************/
    SELECT * INTO #deleted_bk FROM deleted
    CREATE INDEX idx_tmp_deleted ON #deleted_bk(kbn,hosyousyo_no,bunrui_cd,gamen_hyouji_no)

    /******************************************
     *更新された行をカーソルで取得し、処理する*
     ******************************************/
    --カーソルの定義
    DECLARE CUR_URIAGE_TEIBETU
    CURSOR FOR 
        SELECT INS.kbn
          ,INS.hosyousyo_no
          ,INS.bunrui_cd
          ,INS.gamen_hyouji_no
          ,INS.syouhin_cd
          ,TJ.kameiten_cd
          ,INS.denpyou_uri_date
          ,INS.seikyuu_saki_cd
          ,INS.seikyuu_saki_brc
          ,INS.seikyuu_saki_kbn
          ,ISNULL(INS.upd_login_user_id, INS.add_login_user_id)
          ,CASE ISNULL(DEL.uri_gaku,0)
                WHEN 0 THEN NULL   --元々の売上額がゼロ円だった場合
                ELSE DEL.denpyou_uri_date
           END
          ,INS.seikyuusyo_hak_date
          ,CASE 
                WHEN ISNULL(INS.uri_date,0) <> ISNULL(DEL.uri_date,0) THEN 1
                ELSE 0
           END
          FROM 
            inserted INS
            LEFT OUTER JOIN [jhs_sys].[t_jiban] TJ
              ON INS.kbn = TJ.kbn 
              AND INS.hosyousyo_no = TJ.hosyousyo_no
            LEFT OUTER JOIN #deleted_bk DEL
              ON INS.kbn = DEL.kbn 
              AND INS.hosyousyo_no = DEL.hosyousyo_no
              AND INS.bunrui_cd = DEL.bunrui_cd
              AND INS.gamen_hyouji_no = DEL.gamen_hyouji_no

    --カーソルを開く
    OPEN CUR_URIAGE_TEIBETU

    --カーソルより最初の一行を取得
    FETCH NEXT FROM CUR_URIAGE_TEIBETU
    INTO @kbn
        ,@bangou
        ,@bunruiCd
        ,@gamenHyoujiNo
        ,@syouhinCd
        ,@kameitenCd
        ,@denpyouUriDate
        ,@seikyuuSakiCd
        ,@seikyuuSakiBrc
        ,@seikyuuSakiKbn
        ,@userid
        ,@delUriDate
        ,@insSeikyuuDate
        ,@bitUriDateChange

    -- カーソルで取得した行が終端に達するまで処理を継続する
    WHILE @@FETCH_STATUS = 0
    BEGIN

        -- 伝票ユニークNOのリセット
        SET @maxNum = NULL;
        SET @maxChengeNum = NULL;
        SET @bunruiCdSub = SUBSTRING(@bunruiCd ,1 ,2)   --分類コード頭2桁を切り出し
        
        SET @pKey = @kbn + @strSeq
                     + @bangou + @strSeq
                     + @bunruiCdSub + '0' + @strSeq     --切り出した分類コードに、"0"を付与
                     + CAST(@gamenHyoujiNo AS VARCHAR)

        SET @pKey2 = @kbn + @strSeq
                     + @bangou + @strSeq
                     + @bunruiCdSub + '5' + @strSeq     --切り出した分類コードに、"5"を付与
                     + CAST(@gamenHyoujiNo AS VARCHAR)


        /************************************************
         * 更新された邸別請求データのPKと同じPKをもつ、 *
         * 売上データの最大伝票ユニークNOを取得         *
         ************************************************/
        SELECT @maxNum = MAX(URI.denpyou_unique_no)
          FROM [jhs_sys].[t_uriage_data] URI
         WHERE 1=1
           AND (URI.himoduke_cd = @pKey OR URI.himoduke_cd = @pKey2)
           AND URI.himoduke_table_type = @tableType

        /************************************************
         * @maxNumの伝票データが取り消し伝票でないかをチェック *
         ************************************************/
        SELECT
          @maxNum = CASE WHEN
                        URI.torikesi_moto_denpyou_unique_no IS NULL
                        THEN URI.denpyou_unique_no 
                        ELSE NULL 
                    END 
        FROM
        [jhs_sys].[t_uriage_data] URI 
        WHERE
          URI.denpyou_unique_no = @maxNum;

        /************************************************
         * 請求先情報を取得 *
         ************************************************/
        IF @seikyuuSakiCd IS NULL OR @seikyuuSakiBrc IS NULL OR @seikyuuSakiKbn IS NULL
          --請求先コードが邸別請求にセットされていない場合、関数を使用して取得
          SELECT 
            @seikyuuSakiCd = [seikyuu_saki_cd]
            , @seikyuuSakiBrc = [seikyuu_saki_brc]
            , @seikyuuSakiName = [seikyuu_saki_name]
            , @seikyuuSakiKbn = [seikyuu_saki_kbn]
          FROM
            [jhs_sys].[fnGetSeikyuuSakiKeyDataTable](@tableType, @kbn, @bangou, @bunruiCd, @syouhinCd, @kameitenCd)

        ELSE
          --請求先コードが邸別請求にセットされている場合、VIEWを使用して請求先名を取得
          SELECT 
            @seikyuuSakiName = seikyuu_saki_mei
          FROM
            [jhs_sys].[v_seikyuu_saki_info]
          WHERE
            seikyuu_saki_cd = @seikyuuSakiCd
            AND seikyuu_saki_brc = @seikyuuSakiBrc
            AND seikyuu_saki_kbn = @seikyuuSakiKbn
          

        IF @maxNum IS NOT NULL
        BEGIN
            /********************************************************
             * 上で取得した最大伝票ユニークNOのデータと、更新された *
             * 邸別請求データの比較対象が一つでも異なる場合、       *
             * 伝票ユニークNOを@maxChengeNumに格納する              *
             * (@maxChengeNumが取消元伝票ユニークNOとなる)          *
             ********************************************************/
            SELECT @maxChengeNum = URI.denpyou_unique_no
              FROM [jhs_sys].[t_teibetu_seikyuu] INS
             INNER JOIN [jhs_sys].[t_uriage_data] URI
                ON @maxNum                = URI.denpyou_unique_no
              LEFT OUTER JOIN [jhs_sys].[m_syouhin] MS
                ON INS.syouhin_cd         = MS.syouhin_cd
             WHERE 
                   INS.kbn                = @kbn
               AND INS.hosyousyo_no       = @bangou
               AND INS.bunrui_cd          = @bunruiCd
               AND INS.gamen_hyouji_no    = @gamenHyoujiNo
               AND URI.torikesi_moto_denpyou_unique_no IS NULL
               AND (
                      ISNULL(URI.himoduke_cd,'')        <> @kbn + @strSeq + 
                                                           @bangou + @strSeq + 
                                                           @bunruiCd + @strSeq + 
                                                           CAST(@gamenHyoujiNo AS VARCHAR)    --紐付コード
                   OR ISNULL(URI.kbn,'')                <> ISNULL(@kbn,'')                    --区分
                   OR ISNULL(URI.bangou,'')             <> ISNULL(@bangou,'')                 --番号(保証書NO)
                   OR ISNULL(URI.uri_date,'')           <> ISNULL(INS.uri_date,'')            --売上年月日
                   OR ISNULL(URI.denpyou_uri_date,'')   <> ISNULL(@denpyouUriDate,'')         --伝票売上年月日
                   OR ISNULL(URI.uri_keijyou_flg,'')    <> ISNULL(INS.uri_keijyou_flg,'')     --売上処理FLG(売上計上FLG)
                   OR ISNULL(URI.seikyuu_date,'')       <> ISNULL(INS.seikyuusyo_hak_date,'') --請求年月日
                   OR ISNULL(URI.seikyuu_saki_cd, '')   <> ISNULL(@seikyuuSakiCd,'')          --請求先コード
                   OR ISNULL(URI.seikyuu_saki_brc, '')  <> ISNULL(@seikyuuSakiBrc,'')         --請求先枝番
                   OR ISNULL(URI.seikyuu_saki_kbn, '')  <> ISNULL(@seikyuuSakikbn, '')        --請求先区分
                   OR ISNULL(URI.syouhin_cd,'')         <> ISNULL(INS.syouhin_cd,'')          --商品コード
                   OR ISNULL(URI.suu,'')                <> 1                                  --数量
                   OR ISNULL(URI.uri_gaku,'')           <> ISNULL(INS.uri_gaku,'')            --売上金額
                   OR ISNULL(URI.sotozei_gaku,'')       <> ISNULL(INS.syouhizei_gaku,'')      --外税額
                   );
        END

        /************************************
         * 比較対象が一つでも異なった場合は *
         * 売上データへ赤伝票を登録する     *
         ************************************/
        IF @maxChengeNum IS NOT NULL
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
                , CASE @bitUriDateChange 
                    WHEN 1 THEN URIOLD.denpyou_uri_date 
                    ELSE ISNULL(@denpyouUriDate, @today) 
                    END
                , URIOLD.uri_keijyou_flg
                , URIOLD.kameiten_cd
                , CASE @bitUriDateChange 
                    WHEN 1 THEN URIOLD.seikyuu_date 
                    ELSE ISNULL( 
                        @insSeikyuuDate
                        , [jhs_sys].[fnGetSeikyuuDate]( 
                            URIOLD.seikyuu_saki_cd
                            , URIOLD.seikyuu_saki_brc
                            , URIOLD.seikyuu_saki_kbn
                            , @today
                        )
                    ) 
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
                , @userid
                , jhs_sys.fnGetAddUpdUserName(@userid)
                , GETDATE()
                , NULL
                , NULL 
            FROM
                [jhs_sys].[t_uriage_data] URIOLD 
            WHERE
                URIOLD.denpyou_unique_no = @maxChengeNum; 

        /************************************************************
         * 更新された邸別請求データの比較対象が売上データと         *
         * 一つでも異なるか、そもそも売上データに登録されておらず、 * 
         * 更新前データの売上年月日にも値が入っていない場合、       *
         * 黒伝票を登録する                                         *
         * ※但し、更新された邸別請求データの売上年月日が           *
         *   未設定の場合は、登録しない                             *
         ************************************************************/
        IF @maxChengeNum IS NOT NULL OR (@maxNum IS NULL AND @delUriDate IS NULL)
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
                    ,INS.hosyousyo_no
                    ,INS.kbn + @strSeq
                        + INS.hosyousyo_no + @strSeq
                        + INS.bunrui_cd + @strSeq
                        + CAST(INS.gamen_hyouji_no AS VARCHAR)
                    ,@tableType
                    ,INS.uri_date
                    ,INS.denpyou_uri_date
                    ,INS.uri_keijyou_flg
                    ,@kameitenCd
                    ,INS.seikyuusyo_hak_date
                    ,@seikyuuSakiCd
                    ,@seikyuuSakiBrc
                    ,@seikyuuSakiKbn
                    ,@seikyuuSakiName
                    ,INS.syouhin_cd
                    ,MS.syouhin_mei
                    ,1
                    ,MS.tani
                    ,INS.uri_gaku
                    ,MS.syanai_genka
                    ,INS.uri_gaku
                    ,INS.syouhizei_gaku
                    ,INS.zei_kbn
                    ,@userid
                    ,jhs_sys.fnGetAddUpdUserName(@userid)
                    ,GETDATE()
                    ,NULL
                    ,NULL
                  FROM [jhs_sys].[t_teibetu_seikyuu] AS INS
                  LEFT OUTER JOIN [jhs_sys].[m_syouhin] MS
                    ON INS.syouhin_cd = MS.syouhin_cd
                  LEFT OUTER JOIN [jhs_sys].[m_syouhizei] MSZ
                    ON INS.zei_kbn = MSZ.zei_kbn
                 WHERE INS.kbn                  = @kbn
                   AND INS.hosyousyo_no         = @bangou
                   AND INS.bunrui_cd            = @bunruiCd
                   AND INS.gamen_hyouji_no      = @gamenHyoujiNo
                   AND INS.denpyou_uri_date IS NOT NULL
                   AND ISNULL(INS.uri_gaku, 0) <> 0 

        END
        -- 次の１件を取得する
        FETCH NEXT FROM 
            CUR_URIAGE_TEIBETU 
        INTO @kbn
            ,@bangou
            ,@bunruiCd
            ,@gamenHyoujiNo
            ,@syouhinCd
            ,@kameitenCd
            ,@denpyouUriDate
            ,@seikyuuSakiCd
            ,@seikyuuSakiBrc
            ,@seikyuuSakiKbn
            ,@userid
            ,@delUriDate
            ,@insSeikyuuDate
            ,@bitUriDateChange
    END

    -- カーソルを閉じる
    CLOSE CUR_URIAGE_TEIBETU

    -- カーソルのメモリを開放
    DEALLOCATE CUR_URIAGE_TEIBETU
END
