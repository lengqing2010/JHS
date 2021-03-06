-- =============================================
-- Description:    <売上データ作成トリガー(ON 汎用売上 AFTER INSERT, UPDATE)>
-- =============================================
CREATE TRIGGER [jhs_sys].[trSiireDataByHanyouOnInsUpd] 
   ON  [jhs_sys].[t_hannyou_siire] 
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
    DECLARE @denpyouSiireDate   DATETIME        --伝票仕入年月日
    DECLARE @minusFlg           int             --赤伝票発行フラグ
    DECLARE @plusFlg            int             --黒伝票発行フラグ
    DECLARE @maxNum             int             --最大伝票ユニークNO
    DECLARE @motoNum            int             --取消元伝票ユニークNO
    DECLARE @changeNum          int             --更新対象がある場合の伝票ユニークNO
    DECLARE @tableType          int             --テーブル種別
    DECLARE @SN                 CHAR(2)         --伝票種別：仕入(SN)
    DECLARE @SR                 CHAR(2)         --伝票種別：仕入取消(SR)
    DECLARE @userId             VARCHAR(30)     --登録ログインユーザーID
    DECLARE @userNm             VARCHAR(128)    --登録ログインユーザー名
    DECLARE @siireSakiName      VARCHAR(100)    --仕入先名
    DECLARE @delSiireDate       DATETIME        --deletedテーブルの伝票仕入年月日
    DECLARE @today              VARCHAR(20)     --本日日付(日付部分のみ)
    DECLARE @bitSiireDateChange BIT             --仕入年月日変更BIT

    --初期化
    SET @pKey = ''
    SET @SN = 'SN';
    SET @SR = 'SR';
    SET @tableType = 9;         --テーブルタイプ(汎用売上：9)
    SET @today = CONVERT(VARCHAR,GETDATE(),111)

    /******************************************************
     *deletedテーブルのクローンを作成(INDEXを使用するため)*
     ******************************************************/
    SELECT * INTO #deleted_bk FROM deleted
    CREATE INDEX idx_tmp_deleted ON #deleted_bk(han_siire_unique_no)

    /******************************************
     *更新された行をカーソルで取得し、処理する*
     ******************************************/
    --カーソルの定義
    DECLARE CUR_SIIRE_HANNYOU
    CURSOR FOR 
        SELECT
             INS.han_siire_unique_no
            ,INS.torikesi
            ,DEL.torikesi
            ,INS.denpyou_siire_date
            ,MTY.tys_kaisya_mei
            ,ISNULL(INS.upd_login_user_id, INS.add_login_user_id)
            ,ISNULL(INS.upd_login_user_name, INS.add_login_user_name)
            ,CASE ISNULL(CONVERT(BIGINT,DEL.tanka) * DEL.suu,0)
                WHEN 0 THEN NULL   --元々の仕入額がゼロ円だった場合
                ELSE DEL.denpyou_siire_date
             END
            ,CASE 
                  WHEN ISNULL(INS.siire_date,0) <> ISNULL(DEL.siire_date,0) THEN 1
                  ELSE 0
             END
          FROM 
            inserted INS
          LEFT OUTER JOIN #deleted_bk DEL
            ON INS.han_siire_unique_no = DEL.han_siire_unique_no
          LEFT OUTER JOIN [jhs_sys].[m_tyousakaisya] MTY
            ON INS.tys_kaisya_cd  = MTY.tys_kaisya_cd
           AND INS.tys_kaisya_jigyousyo_cd = MTY.jigyousyo_cd

    --カーソルを開く
    OPEN CUR_SIIRE_HANNYOU

    FETCH NEXT FROM CUR_SIIRE_HANNYOU
    INTO @pKey
        ,@torikesiINS
        ,@torikesiDEL
        ,@denpyouSiireDate
        ,@siireSakiName
        ,@userId
        ,@userNm
        ,@delSiireDate
        ,@bitSiireDateChange

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
        SELECT @maxNum = MAX(SIR.denpyou_unique_no)
          FROM [jhs_sys].[t_siire_data] SIR
        WHERE SIR.himoduke_cd = @pKey
          AND SIR.himoduke_table_type = @tableType

        /***************************************************************
        * 上で取得した最大伝票ユニークNOの取消元伝票ユニークNOを取得   *
        ****************************************************************/
        --「取得○」：直近がマイナス伝票
        --「取得×」：直近がプラス伝票
        SELECT @motoNum = SIR.torikesi_moto_denpyou_unique_no
          FROM [jhs_sys].[t_siire_data] SIR
        WHERE 1=1
          AND SIR.denpyou_unique_no = @maxNum
        
        /********************************************************
        * 更新された入金取込ユニークNOのデータと、入金データの
        * 比較対象が一つでも異なる場合、伝票ユニークNOを@changeNumに格納する
        ********************************************************/
        SELECT @changeNum = SIR.denpyou_unique_no
          FROM [jhs_sys].[t_hannyou_siire] INS
          INNER JOIN [jhs_sys].[t_siire_data] SIR
          ON SIR.denpyou_unique_no = @maxNum
        WHERE INS.han_siire_unique_no = @pKey
           AND (
                   ISNULL(SIR.kbn,'')            <> ISNULL(INS.kbn,'')                      --区分
                OR ISNULL(SIR.bangou,'')         <> ISNULL(INS.bangou,'')                   --番号(保証書NO)
                OR ISNULL(SIR.siire_date, '')    <> ISNULL(INS.siire_date, '')              --仕入年月日
                OR ISNULL(SIR.denpyou_siire_date, '') 
                                                 <> ISNULL(@denpyouSiireDate, '')              --伝票仕入年月日
                OR ISNULL(SIR.siire_keijyou_flg, '')
                                                 <> ISNULL(INS.siire_keijyou_flg, '')       --仕入処理FLG(売上計上FLG)
                OR ISNULL(SIR.tys_kaisya_cd, '') <> ISNULL(INS.tys_kaisya_cd, '')           --(仕入)調査会社先コード
                OR ISNULL(SIR.tys_kaisya_jigyousyo_cd, '')
                                                 <> ISNULL(INS.tys_kaisya_jigyousyo_cd, '') --(仕入)調査会社事業所コード
                OR ISNULL(SIR.syouhin_cd, '')    <> ISNULL(INS.syouhin_cd, '')              --商品コード
                OR ISNULL(SIR.suu, '')           <> ISNULL(INS.suu, '')                     --数量
                OR ISNULL(SIR.siire_gaku, '')    <> ISNULL(CONVERT(BIGINT,INS.tanka) * INS.suu, '') --仕入金額
                OR ISNULL(SIR.sotozei_gaku, '')  <> ISNULL(INS.syouhizei_gaku, '')          --仕入消費税額
                OR ISNULL(SIR.zei_kbn, '')       <> ISNULL(INS.zei_kbn, '')                 --税区分
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
        -- 更新前データの仕入年月日が未指定か取消状態だった場合、
        -- 新規データとして扱う
        IF @maxNum IS NULL AND (@delSiireDate IS NULL OR @torikesiDEL <> 0)
            -- 取消ではない場合
            IF @torikesiINS = 0
                SET @plusFlg = 1;

        /***********************************************
        * フラグがある場合、赤伝票（マイナス伝票）発行 *
        ************************************************/
        IF @minusFlg = 1
        BEGIN
            INSERT INTO [jhs_sys].[t_siire_data]
                (
                denpyou_syubetu
                ,torikesi_moto_denpyou_unique_no
                ,kbn
                ,bangou
                ,himoduke_cd
                ,himoduke_table_type
                ,siire_date
                ,denpyou_siire_date
                ,siire_keijyou_flg
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
                    @SR
                    ,SIROLD.denpyou_unique_no
                    ,SIROLD.kbn
                    ,SIROLD.bangou
                    ,SIROLD.himoduke_cd
                    ,@tableType
                    ,SIROLD.siire_date
                    ,CASE
                        WHEN @plusFlg IS NULL --削除に伴なう発行
                        THEN (CASE
							                  WHEN SIROLD.siire_keijyou_flg = '1'
							                  THEN @today
							                  ELSE SIROLD.denpyou_siire_date
						                   END)
                        ELSE --更新に伴なう発行
                          (CASE @bitSiireDateChange
                            WHEN 1 THEN SIROLD.denpyou_siire_date
                            ELSE ISNULL(@denpyouSiireDate, @today)
                           END)
                        END
                    ,SIROLD.siire_keijyou_flg
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
                    ,@userId
                    ,@userNm
                    ,GETDATE()
                    ,NULL
                    ,NULL
                  FROM [jhs_sys].[t_siire_data] SIROLD
                 WHERE SIROLD.denpyou_unique_no = @maxNum;
        END
        
        
        /***************************************************
        * フラグがある場合、もしくは伝票ユニークNOの最大値 *
        * が取得出来ない場合、黒伝票（プラス伝票）発行     *
        ****************************************************/
        IF @plusFlg = 1 
        BEGIN
            INSERT INTO [jhs_sys].[t_siire_data]
                (
                denpyou_syubetu
                ,torikesi_moto_denpyou_unique_no
                ,kbn
                ,bangou
                ,himoduke_cd
                ,himoduke_table_type
                ,siire_date
                ,denpyou_siire_date
                ,siire_keijyou_flg
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
                    @SN
                    ,NULL
                    ,INS.kbn
                    ,INS.bangou
                    ,INS.han_siire_unique_no
                    ,@tableType
                    ,INS.siire_date
                    ,@denpyouSiireDate
                    ,INS.siire_keijyou_flg
                    ,INS.tys_kaisya_cd
                    ,INS.tys_kaisya_jigyousyo_cd
                    ,@siireSakiName
                    ,INS.syouhin_cd
                    ,MS.syouhin_mei
                    ,INS.suu
                    ,MS.tani
                    ,INS.tanka
                    ,CONVERT(BIGINT,INS.tanka) * INS.suu
                    ,INS.syouhizei_gaku
                    ,INS.zei_kbn
                    ,@userId
                    ,@userNm
                    ,GETDATE()
                    ,NULL
                    ,NULL
                  FROM [jhs_sys].[t_hannyou_siire] AS INS
                  LEFT OUTER JOIN [jhs_sys].[m_syouhin] MS
                    ON INS.syouhin_cd = MS.syouhin_cd
                 WHERE INS.han_siire_unique_no = @pKey
                   AND INS.denpyou_siire_date IS NOT NULL
                   AND ISNULL(CONVERT(BIGINT,INS.tanka) * INS.suu,0) <> 0
        END


        --次の１件を取得する
        FETCH NEXT FROM CUR_SIIRE_HANNYOU
        INTO @pKey
            ,@torikesiINS
            ,@torikesiDEL
            ,@denpyouSiireDate
            ,@siireSakiName
            ,@userId
            ,@userNm
            ,@delSiireDate
            ,@bitSiireDateChange
    END
    
    -- カーソルを閉じる
    CLOSE CUR_SIIRE_HANNYOU

    -- カーソルのメモリを開放
    DEALLOCATE CUR_SIIRE_HANNYOU
    
END







