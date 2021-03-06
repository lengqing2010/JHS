-- ==========================================================================
-- Description: <仕入データ作成トリガー(ON 邸別請求 AFTER INSERT, UPDATE)>
-- ==========================================================================
CREATE TRIGGER [jhs_sys].[trSiireDataByTeibetuOnInsUpd] 
   ON  [jhs_sys].[t_teibetu_seikyuu] 
   AFTER INSERT,UPDATE
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    --カーソル用変数宣言
    DECLARE @kbn                CHAR(1)      --区分
    DECLARE @bangou             VARCHAR(10)  --番号
    DECLARE @bunruiCd           VARCHAR(3)   --分類コード
    DECLARE @gamenHyoujiNo      int          --画面表示NO
    DECLARE @syouhinCd          VARCHAR(10)  --商品コード
    DECLARE @DenpyouSiireDate   DATETIME     --伝票仕入年月日
    DECLARE @siireSakiCd        VARCHAR(10)  --仕入先コード
    DECLARE @siireSakiBrc       VARCHAR(10)  --仕入先枝番
    DECLARE @siireSakiName      VARCHAR(100) --仕入先名
    DECLARE @bitUriDateChange   BIT          --売上年月日変更BIT
    DECLARE @pKey               VARCHAR(30)  --紐付けコード
    DECLARE @pKey2              VARCHAR(30)  --紐付けコード2
    DECLARE @maxNum             int          --最大伝票ユニークNO
    DECLARE @maxChengeNum       int          --取消元伝票ユニークNO
    DECLARE @tableType          int
    DECLARE @strSeq             CHAR(3)
    DECLARE @strSyouhinSeq      CHAR(1)
    DECLARE @SN                 CHAR(2)
    DECLARE @SR                 CHAR(2)
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
    SET @SN = 'SN';             --伝票種別：仕入(SN)
    SET @SR = 'SR';             --伝票種別：仕入取消(SR)
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
    DECLARE CUR_SIIRE_TEIBETU
    CURSOR FOR 
        SELECT
              INS.kbn
            , INS.hosyousyo_no
            , INS.bunrui_cd
            , INS.gamen_hyouji_no
            , INS.syouhin_cd
            , INS.denpyou_siire_date
            , INS.tys_kaisya_cd
            , INS.tys_kaisya_jigyousyo_cd
            , ISNULL(INS.upd_login_user_id, INS.add_login_user_id)
            , CASE ISNULL(DEL.siire_gaku, 0) 
                WHEN 0 THEN NULL    --元々の仕入額がゼロ円だった場合
                ELSE DEL.denpyou_siire_date 
                END
            , CASE 
                WHEN ISNULL(INS.uri_date, 0) <> ISNULL(DEL.uri_date, 0) 
                THEN 1 
                ELSE 0 
                END 
        FROM
            inserted INS 
            LEFT OUTER JOIN #deleted_bk DEL 
                ON INS.kbn = DEL.kbn 
                AND INS.hosyousyo_no = DEL.hosyousyo_no 
                AND INS.bunrui_cd = DEL.bunrui_cd 
                AND INS.gamen_hyouji_no = DEL.gamen_hyouji_no

    --カーソルを開く
    OPEN CUR_SIIRE_TEIBETU

    --カーソルより最初の一行を取得
    FETCH NEXT FROM CUR_SIIRE_TEIBETU
    INTO @kbn
        ,@bangou
        ,@bunruiCd
        ,@gamenHyoujiNo
        ,@syouhinCd
        ,@DenpyouSiireDate
        ,@siireSakiCd
        ,@siireSakiBrc
        ,@userid
        ,@delUriDate
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
                     + @bunruiCdSub + '0' + @strSeq --上2桁を取得し、"_"を付与
                     + CAST(@gamenHyoujiNo AS VARCHAR)

        SET @pKey2 = @kbn + @strSeq
                     + @bangou + @strSeq
                     + @bunruiCdSub + '5' + @strSeq --上2桁を取得し、"_"を付与
                     + CAST(@gamenHyoujiNo AS VARCHAR)

        /************************************************
         * 更新された邸別請求データのPKと同じPKをもつ、 *
         * 仕入データの最大伝票ユニークNOを取得         *
         ************************************************/
        SELECT @maxNum = MAX(SIR.denpyou_unique_no)
          FROM [jhs_sys].[t_siire_data] SIR
         WHERE 1=1
           AND (SIR.himoduke_cd = @pKey OR SIR.himoduke_cd = @pKey2)
           AND SIR.himoduke_table_type = @tableType

        /*******************************************************
         * @maxNumの伝票データが取り消し伝票でないかをチェック *
         *******************************************************/
        SELECT
          @maxNum = CASE WHEN
            torikesi_moto_denpyou_unique_no IS NULL
                        THEN denpyou_unique_no 
                        ELSE NULL 
                    END 
        FROM
          [jhs_sys].[t_siire_data] 
        WHERE
          denpyou_unique_no = @maxNum

        /************************************************
         * 仕入先情報を取得 *
         ************************************************/
        IF @siireSakiCd IS NULL OR @siireSakiBrc IS NULL
          --調査会社コード、事業所コードが邸別請求にセットされていない場合、
          --関数を使用して取得
          SELECT 
            @siireSakiCd = siire_saki_tys_kaisya_cd
            , @siireSakiBrc = siire_saki_tys_jigyousyo_cd
            , @siireSakiName = siire_saki_tys_kaisya_mei
          FROM
            [jhs_sys].[v_syouhin_siire_seikyuu_saki_jiban_tyskaisya]
          WHERE
            kbn = @kbn
            AND hosyousyo_no = @bangou
            AND syouhin_cd = @syouhinCd

        ELSE
          --調査会社コード、事業所コードが邸別請求にセットされている場合、
          --調査会社マスタから仕入先名として調査会社名を取得
          SELECT 
            @siireSakiName = tys_kaisya_mei
          FROM
            [jhs_sys].[m_tyousakaisya]
          WHERE
            tys_kaisya_cd = @siireSakiCd
            AND jigyousyo_cd = @siireSakiBrc

        IF @maxNum IS NOT NULL
        BEGIN
            /********************************************************
             * 上で取得した最大伝票ユニークNOのデータと、更新された *
             * 邸別請求データの比較対象が一つでも異なる場合、       *
             * 伝票ユニークNOを@maxChengeNumに格納する              *
             * (@maxChengeNumが取消元伝票ユニークNOとなる)          *
             ********************************************************/
            SELECT @maxChengeNum = SIR.denpyou_unique_no
              FROM [jhs_sys].[t_teibetu_seikyuu] INS
             INNER JOIN [jhs_sys].[t_siire_data] SIR
                ON @maxNum                  = SIR.denpyou_unique_no
              LEFT OUTER JOIN [jhs_sys].[m_syouhin] MS
                ON INS.syouhin_cd           = MS.syouhin_cd
             WHERE 
                   INS.kbn                  = @kbn
               AND INS.hosyousyo_no         = @bangou
               AND INS.bunrui_cd            = @bunruiCd
               AND INS.gamen_hyouji_no      = @gamenHyoujiNo
               AND SIR.torikesi_moto_denpyou_unique_no IS NULL
               AND (
                      ISNULL(SIR.himoduke_cd,'')                <> @kbn + @strSeq + 
                                                                   @bangou + @strSeq + 
                                                                   @bunruiCd + @strSeq + 
                                                                   CAST(@gamenHyoujiNo AS VARCHAR)      --紐付コード
                   OR ISNULL(SIR.kbn, '')                       <> ISNULL(@kbn, '')                     --区分
                   OR ISNULL(SIR.bangou, '')                    <> ISNULL(@bangou, '')                  --番号(保証書NO)
                   OR ISNULL(SIR.siire_date, '')                <> ISNULL(INS.uri_date, '')             --仕入年月日(売上年月日)
                   OR ISNULL(SIR.denpyou_siire_date, '')        <> ISNULL(INS.denpyou_siire_date, '')   --伝票仕入年月日
                   OR ISNULL(SIR.siire_keijyou_flg, '')         <> ISNULL(INS.uri_keijyou_flg, '')      --仕入処理FLG(売上計上FLG)
                   OR ISNULL(SIR.tys_kaisya_cd, '')             <> ISNULL(@siireSakiCd, '')             --(仕入先)調査会社コード
                   OR ISNULL(SIR.tys_kaisya_jigyousyo_cd, '')   <> ISNULL(@siireSakiBrc, '')            --(仕入先)調査会社事業所コード
                   OR ISNULL(SIR.syouhin_cd, '')                <> ISNULL(INS.syouhin_cd, '')           --商品コード
                   OR ISNULL(SIR.suu, '')                       <> 1                                    --数量
                   OR ISNULL(SIR.siire_gaku, '')                <> ISNULL(INS.siire_gaku, '')           --仕入金額
                   OR ISNULL(SIR.sotozei_gaku, '')              <> ISNULL(INS.siire_syouhizei_gaku, '') --仕入消費税額
                   OR ISNULL(SIR.zei_kbn, '')                   <> ISNULL(INS.zei_kbn, '')              --税区分
                   );
        END

        /************************************
         * 比較対象が一つでも異なった場合は *
         * 仕入データへ赤伝票を登録する     *
         ************************************/
        IF @maxChengeNum IS NOT NULL
        BEGIN
            INSERT INTO [jhs_sys].[t_siire_data]
                (
                 denpyou_no
                ,denpyou_syubetu
                ,torikesi_moto_denpyou_unique_no
                ,kbn
                ,bangou
                ,himoduke_cd
                ,himoduke_table_type
                ,siire_date
                ,denpyou_siire_date
                ,siire_keijyou_flg
                ,bukken_tys_kaisya_cd
                ,bukken_tys_kaisya_jigyousyo_cd
                ,tys_kaisya_cd
                ,tys_kaisya_jigyousyo_cd
                ,tys_kaisya_mei
                ,syouhin_cd
                ,hinmei
                ,suu
                ,tani
                ,tanka
                ,siire_gaku
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
                    ,@SR
                    ,SIROLD.denpyou_unique_no
                    ,SIROLD.kbn
                    ,SIROLD.bangou
                    ,SIROLD.himoduke_cd
                    ,@tableType
                    ,SIROLD.siire_date
                    ,CASE @bitUriDateChange
                        WHEN 1 THEN SIROLD.denpyou_siire_date
                        ELSE ISNULL(@denpyouSiireDate, @today)
                     END
                    ,SIROLD.siire_keijyou_flg
                    ,SIROLD.bukken_tys_kaisya_cd
                    ,SIROLD.bukken_tys_kaisya_jigyousyo_cd
                    ,SIROLD.tys_kaisya_cd
                    ,SIROLD.tys_kaisya_jigyousyo_cd
                    ,SIROLD.tys_kaisya_mei
                    ,SIROLD.syouhin_cd
                    ,SIROLD.hinmei
                    ,SIROLD.suu * -1
                    ,SIROLD.tani
                    ,SIROLD.tanka
                    ,SIROLD.siire_gaku * -1
                    ,SIROLD.sotozei_gaku * -1
                    ,SIROLD.zei_kbn
                    ,@userid
                    ,jhs_sys.fnGetAddUpdUserName(@userid)
                    ,GETDATE()
                    ,NULL
                    ,NULL
                  FROM [jhs_sys].[t_siire_data] SIROLD
                 WHERE SIROLD.denpyou_unique_no = @maxChengeNum;
        END

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
            INSERT INTO [jhs_sys].[t_siire_data]
                (
                 denpyou_no
                ,denpyou_syubetu
                ,torikesi_moto_denpyou_unique_no
                ,kbn
                ,bangou
                ,himoduke_cd
                ,himoduke_table_type
                ,siire_date
                ,denpyou_siire_date
                ,siire_keijyou_flg
                ,bukken_tys_kaisya_cd
                ,bukken_tys_kaisya_jigyousyo_cd
                ,tys_kaisya_cd
                ,tys_kaisya_jigyousyo_cd
                ,tys_kaisya_mei
                ,syouhin_cd
                ,hinmei
                ,suu
                ,tani
                ,tanka
                ,siire_gaku
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
                    ,@SN
                    ,NULL
                    ,INS.kbn
                    ,INS.hosyousyo_no
                    ,INS.kbn + @strSeq
                        + INS.hosyousyo_no + @strSeq
                        + INS.bunrui_cd + @strSeq
                        + CAST(INS.gamen_hyouji_no AS VARCHAR)
                    ,@tableType
                    ,INS.uri_date
                    ,INS.denpyou_siire_date
                    ,INS.uri_keijyou_flg
                    ,TJ.tys_kaisya_cd
                    ,TJ.tys_kaisya_jigyousyo_cd
                    ,@siireSakiCd
                    ,@siireSakiBrc
                    ,@siireSakiName
                    ,INS.syouhin_cd
                    ,MS.syouhin_mei
                    ,1
                    ,MS.tani
                    ,INS.siire_gaku
                    ,INS.siire_gaku
                    ,INS.siire_syouhizei_gaku
                    ,INS.zei_kbn
                    ,@userid
                    ,jhs_sys.fnGetAddUpdUserName(@userid)
                    ,GETDATE()
                    ,NULL
                    ,NULL
                  FROM [jhs_sys].[t_teibetu_seikyuu] AS INS
                  LEFT OUTER JOIN [jhs_sys].[t_jiban] TJ
                    ON INS.kbn          = TJ.kbn
                   AND INS.hosyousyo_no = TJ.hosyousyo_no
                  LEFT OUTER JOIN [jhs_sys].[m_syouhin] MS
                    ON INS.syouhin_cd       = MS.syouhin_cd
             WHERE INS.kbn                  = @kbn
                   AND INS.hosyousyo_no     = @bangou
                   AND INS.bunrui_cd        = @bunruiCd
                   AND INS.gamen_hyouji_no  = @gamenHyoujiNo
                   AND INS.denpyou_siire_date IS NOT NULL
                   AND ISNULL(INS.siire_gaku, 0) <> 0 
        END

        -- 次の１件を取得する
        FETCH NEXT FROM 
            CUR_SIIRE_TEIBETU 
        INTO @kbn
            ,@bangou
            ,@bunruiCd
            ,@gamenHyoujiNo
            ,@syouhinCd
            ,@DenpyouSiireDate
            ,@siireSakiCd
            ,@siireSakiBrc
            ,@userid
            ,@delUriDate
            ,@bitUriDateChange

    END

    -- カーソルを閉じる
    CLOSE CUR_SIIRE_TEIBETU

    -- カーソルのメモリを開放
    DEALLOCATE CUR_SIIRE_TEIBETU
END
