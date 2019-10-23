-- =====================================================
-- Description:	保証書管理テーブル更新処理(月次)
--  1.保証書管理データ.お引渡し前保険[保険申請] 引渡し前保険,引渡し前保険年月日,引渡し前保険実施日
--  2.保証書管理データ.お引渡し後保険[保険申請] 引渡し後保険,引渡し後保険年月日,引渡し後保険実施日
-- =====================================================
CREATE PROCEDURE [jhs_sys].[spSetGetujiHosyousyo_setup]
   @COUNT_HK_MAE                  INT     OUTPUT
  ,@COUNT_HK_ATO                  INT     OUTPUT
  ,@COUNT_HK_TOKUTEI			        INT     OUTPUT
  ,@ERR_NO                        INT     OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
	
	SET NOCOUNT ON;

	DECLARE @UPD_USER_ID               VARCHAR(30)  --更新ログインユーザーID
	DECLARE @UPD_DATETIME              DATETIME     --更新日時

	--初期化
  SET @UPD_USER_ID = 'system'
  SET @UPD_DATETIME = GETDATE()
  
/*************************************************************************
 * 1.保証書管理データ.お引渡し前保険[保険申請] 引渡し前保険,引渡し前保険年月日,引渡し前保険実施日
 **************************************************************************/
 UPDATE
     [jhs_sys].[t_hosyousyo_kanri]
  SET
     hw_mae_hkn = ISNULL(func.hw_hkn, 0) --該当データある場合、判定した結果をセット。該当データない場合は0。
   , hw_mae_hkn_date = func.hw_hkn_date
   , hw_mae_hkn_jissi_date = func.hw_hkn_jissi_date
   , syori_flg = '2'
   , syori_datetime = @UPD_DATETIME
   , hoken_kikan = func.hoken_kikan
   , upd_login_user_id = @UPD_USER_ID
   , upd_datetime = @UPD_DATETIME
  FROM [jhs_sys].[t_hosyousyo_kanri] HK
      LEFT OUTER JOIN
          (SELECT
                br.kbn
              , br.hosyousyo_no
              , CASE
                     WHEN sub.kbn IS NOT NULL
                     THEN 1
                     ELSE 0
                END AS hw_hkn
              , br.hanyou_date AS hw_hkn_date
              , br.kanri_date AS hw_hkn_jissi_date
              , Case when br.hanyou_cd='022' then 15 else 10 end AS hoken_kikan
           FROM
                jhs_sys.t_bukken_rireki br
              , (SELECT
                     brsub.kbn
                   , brsub.hosyousyo_no
                   , ISNULL(MAX(brsub.nyuuryoku_no),0) AS max
                FROM
                     jhs_sys.t_bukken_rireki brsub
                WHERE
                     brsub.rireki_syubetu = '17'
                 AND brsub.rireki_no <> '0'
                 AND brsub.torikesi = '0'
                GROUP BY
                     brsub.kbn
                   , brsub.hosyousyo_no
               )
                sub
           WHERE
                br.kbn = sub.kbn
            AND br.hosyousyo_no = sub.hosyousyo_no
            AND br.nyuuryoku_no = sub.max
            AND br.rireki_syubetu = '17'
            AND br.rireki_no IN('11','21')
          ) AS func
         ON HK.kbn = func.kbn
         AND HK.hosyousyo_no = func.hosyousyo_no
         
	                
  /*************************
  *処理件数、エラーNOセット
  *************************/
  SELECT
      @ERR_NO = @@ERROR
      ,@COUNT_HK_MAE = @@ROWCOUNT

  --エラーチェック
  IF @ERR_NO <> 0
  BEGIN
      -- トランザクションをロールバック（キャンセル）
      ROLLBACK TRANSACTION
      RETURN @ERR_NO
  END
  
/*************************************************************************
 * 2.保証書管理データ.お引渡し後保険[保険申請] 引渡し後保険,引渡し後保険年月日,引渡し後保険実施日
 **************************************************************************/
 UPDATE
     [jhs_sys].[t_hosyousyo_kanri]
  SET
       hw_ato_hkn = ISNULL(func.hw_hkn,0) --該当データある場合、判定した結果をセット。該当データない場合は0。
     , hw_ato_hkn_date = func.hw_hkn_date
     , hw_ato_hkn_jissi_date = func.hw_hkn_jissi_date
     , syori_flg = 2
     , syori_datetime = @UPD_DATETIME
     , upd_login_user_id = @UPD_USER_ID
     , upd_datetime = @UPD_DATETIME
  FROM
       [jhs_sys].[t_hosyousyo_kanri] HK
            LEFT OUTER JOIN (
                 SELECT
                     br.kbn
                    , br.hosyousyo_no
                    , CASE WHEN sub.kbn IS NOT NULL
                      THEN 1
                      ELSE 0
                      END AS hw_hkn
                    , br.hanyou_date AS hw_hkn_date
                    , br.kanri_date AS hw_hkn_jissi_date
                FROM
                      jhs_sys.t_bukken_rireki br
                      ,
                       (SELECT
                             brsub.kbn
                           , brsub.hosyousyo_no
                           , ISNULL(MAX(brsub.nyuuryoku_no),0) AS max
                        FROM
                             jhs_sys.t_bukken_rireki brsub
                        WHERE
                             brsub.rireki_syubetu = '18'
                         AND brsub.rireki_no <> '0'
                         AND brsub.torikesi = '0'
                        GROUP BY
                             brsub.kbn
                           , brsub.hosyousyo_no
                       )
                        sub
                 WHERE
                      br.kbn = sub.kbn
                  AND br.hosyousyo_no = sub.hosyousyo_no
                  AND br.nyuuryoku_no = sub.max
                  AND br.rireki_syubetu = '18'
                  AND br.rireki_no = '11'
                ) AS func
               ON HK.kbn = func.kbn
               AND HK.hosyousyo_no = func.hosyousyo_no

  /*************************
  *処理件数、エラーNOセット
  *************************/
  SELECT
      @ERR_NO = @@ERROR
      ,@COUNT_HK_ATO = @@ROWCOUNT

  --エラーチェック
  IF @ERR_NO <> 0
  BEGIN
      -- トランザクションをロールバック（キャンセル）
      ROLLBACK TRANSACTION
      RETURN @ERR_NO
  END

/*************************************************************************
 * 3.保証書管理データ.増改築通知[保険申請] 特定商品実施日,特定商品FLG
 **************************************************************************/
UPDATE
     [jhs_sys].[t_hosyousyo_kanri]
SET
     tokutei_syouhin_jissi_date = NULL
   , tokutei_syouhin_flg = '0'
   , syori_flg = 2
   , syori_datetime = @UPD_DATETIME
   , upd_login_user_id = @UPD_USER_ID
   , upd_datetime = @UPD_DATETIME
FROM
     [jhs_sys].[t_hosyousyo_kanri] HK2
WHERE
     NOT exists
    (SELECT
          *
     FROM
          [jhs_sys].[t_hosyousyo_kanri] HK
               INNER JOIN [jhs_sys].[t_jiban] TJ
                 ON HK.kbn = TJ.kbn
                AND HK.hosyousyo_no = TJ.hosyousyo_no
               INNER JOIN
                   (SELECT
                         TTS.kbn
                       , TTS.hosyousyo_no
                    FROM
                        (SELECT
                              TS.kbn
                            , TS.hosyousyo_no
                            , TS.bunrui_cd
                            , TS.syouhin_cd
                            , min(TS.uri_date) AS min_uri_date
                         FROM
                              t_teibetu_seikyuu TS
                         GROUP BY
                              TS.kbn
                            , TS.hosyousyo_no
                            , TS.bunrui_cd
                            , TS.syouhin_cd
                         HAVING
                              min(TS.uri_date) is NOT null
                          AND TS.bunrui_cd in(100, 110, 115, 120)
                        )
                         TTS
                              LEFT OUTER JOIN
                                  (SELECT
                                        *
                                   FROM
                                        m_syouhin
                                  )
                                   MS
                                ON TTS.syouhin_cd = MS.syouhin_cd
                    GROUP BY
                         TTS.kbn
                       , TTS.hosyousyo_no
                    HAVING
                         max(ISNULL(NullIf(MS.tokutei_hoken_sinsei_flg, ''), 0)) <> 0
                   )
                    ren
                 ON TJ.kbn = ren.kbn
                AND TJ.hosyousyo_no = ren.hosyousyo_no
     WHERE
          TJ.data_haki_syubetu IN('0', '99')
      AND ren.kbn = HK2.kbn
      AND ren.hosyousyo_no = HK2.hosyousyo_no
    )

  /*************************
  *処理件数、エラーNOセット
  *************************/
  SELECT
      @ERR_NO = @@ERROR
      ,@COUNT_HK_TOKUTEI = @@ROWCOUNT

  --エラーチェック
  IF @ERR_NO <> 0
  BEGIN
      -- トランザクションをロールバック（キャンセル）
      ROLLBACK TRANSACTION
      RETURN @ERR_NO
  END

  
-- トランザクションをコミット
COMMIT TRANSACTION
RETURN @@ERROR 
  
END


GO
