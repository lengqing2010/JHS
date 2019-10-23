-- =====================================================
-- Description:	保証書管理テーブル追加/更新処理[日次]
-- =====================================================
CREATE PROCEDURE [jhs_sys].[spSetHosyousyoKanri]
     @COUNT_HK_INSERT                 INT         OUTPUT  --保証書管理データ[新規追加]
    ,@COUNT_HK_INSERT_UPDATE          INT         OUTPUT  --保証書管理データ[新規追加+更新]
    ,@COUNT_HK_BUKKEN_JYKY_UPDATE     INT         OUTPUT  --保証書管理データ[更新・物件状況のみ]
	,@COUNT_IF_UPDATE                 INT         OUTPUT  --ReportIFデータ[更新]
    ,@ERR_NO                          INT         OUTPUT  --エラー情報
AS
BEGIN
  BEGIN TRANSACTION
  
	SET NOCOUNT ON;
	
	DECLARE @UPD_USER_ID               VARCHAR(30)  --更新ログインユーザーID
	DECLARE @UPD_DATETIME              DATETIME     --更新日時
	DECLARE @retType				   int			--返却タイプ（=1:コード、<>1:MMDD）
		
	--初期化
  SET @UPD_USER_ID = 'system'
  SET @UPD_DATETIME = getdate()
  SET @retType = 1

 /*********************************************
 * 保証書管理データの未存在データを新規追加する
 *********************************************/
 INSERT INTO [jhs_sys].[t_hosyousyo_kanri]
  (
    kbn
    ,hosyousyo_no
    ,hw_mae_hkn
    ,hw_ato_hkn
    ,syori_flg
    ,syori_datetime
    ,add_login_user_id
    ,add_datetime
  )
 SELECT
  TJ.kbn
  ,TJ.hosyousyo_no
  ,0
  ,0
  ,0
  ,@UPD_DATETIME
  ,@UPD_USER_ID
  ,@UPD_DATETIME
 FROM [jhs_sys].[t_jiban] TJ
 WHERE NOT EXISTS
    (SELECT
          *
     FROM
          [jhs_sys].[t_hosyousyo_kanri] HK
     WHERE
          TJ.kbn = HK.kbn
      AND TJ.hosyousyo_no = HK.hosyousyo_no
    )

  /*************************
  *処理件数、エラーNOセット
  *************************/
  SELECT
      @ERR_NO = @@ERROR
      ,@COUNT_HK_INSERT = @@ROWCOUNT

  --エラーチェック
  IF @ERR_NO <> 0
  BEGIN
      -- トランザクションをロールバック（キャンセル）
      ROLLBACK TRANSACTION
      RETURN @ERR_NO
  END

  /*********************************************
  * 保証書管理テーブルを一時テーブルへ退避
  *********************************************/
  BEGIN
      IF  EXISTS (SELECT * FROM tempdb..sysobjects  WHERE id = OBJECT_ID(N'tempdb..#TEMP_HOSYOUSYO_KANRI'))
      DROP TABLE #TEMP_HOSYOUSYO_KANRI;
  END
  
  BEGIN
      SELECT
           THK.*
      INTO
           #TEMP_HOSYOUSYO_KANRI
      FROM
           t_hosyousyo_kanri THK;
  END

  /*************************
  *処理件数、エラーNOセット
  *************************/
  SELECT
      @ERR_NO = @@ERROR

  --エラーチェック
  IF @ERR_NO <> 0
  BEGIN
      -- トランザクションをロールバック（キャンセル）
      ROLLBACK TRANSACTION
      RETURN @ERR_NO
  END

 /******************************************
 * 保証書管理データを更新する[保証書状況]
 * - 保険申請以外が対象
 ******************************************/
  UPDATE
       [jhs_sys].[t_hosyousyo_kanri]
  SET
       kaiseki_kanry =
       CASE
--            WHEN TJ.hantei_cd1 in('97', '113', '1635')
            WHEN KS.ks_siyou like '%保留%' or KS.ks_siyou like '%再調査%' or KS.ks_siyou like '%保証なし%' or KS.ks_siyou like '%保証無し%' or KS.ks_siyou like '%要再検討%' or KS.ks_siyou like '%その他,%' or (TJ.hantei_cd1 = '113' and HK.kbn<>'C')
            THEN 2
            ELSE (
                      CASE
                           WHEN TJ.keikakusyo_sakusei_date IS NOT NULL
                             OR TJ.hantei_cd1 IS NOT NULL
                           THEN 1
                           ELSE 0
                      END)
       END
--     , koj_umu = ISNULL(TJ.koj_hantei_kekka_flg, 0)
     , koj_umu = 
	CASE
		WHEN ISNULL(TJ.koj_hantei_kekka_flg, 0)=1 OR (KS.ks_siyou like '%対策%' AND TJ.hantei_cd1<>1396) OR KS.ks_siyou like '%解析確認%' or TJ.kbn='C'
		THEN 1
		ELSE 0
	END
     , koj_kanry=
       CASE
            WHEN NOT EXISTS
                (SELECT
                      TS.kbn
                 FROM
                      t_teibetu_seikyuu TS
                 WHERE
                      TS.kbn = TJ.kbn
                  AND TS.hosyousyo_no = TJ.hosyousyo_no
                  AND TS.bunrui_cd = '130'
                  AND TS.gamen_hyouji_no = 1
                )
	    AND
		Nullif(TJ.kairy_koj_syubetu,'') is null
            THEN 0
            WHEN TJ.kairy_koj_sokuhou_tyk_date IS NULL
              OR TJ2.kbn IS NOT NULL
            THEN 2
            WHEN (TJ.koj_hkks_juri_date IS NULL
             AND ISNULL(TJ.koj_hkks_umu,'') <> 3) or ISNULL(TJ.koj_hkks_umu,'')=2
            THEN 3
            ELSE 1
       END
     , nyuukin_kakunin_jyouken = ISNULL(KT.nyuukin_kakunin_jyouken, 0)
     , nyuukin_jyky = [jhs_sys].[fnGetNyuukinJyky](TJ.kbn, TJ.hosyousyo_no, @UPD_DATETIME)
     , kasi =
       CASE
            WHEN ISNULL(TJ.kasi_umu, 0) = 0
            THEN 0
            WHEN
                (SELECT
                      ISNULL(MIN(ks.kensa_status_cd), 0) min_kensa_status_cd
                 FROM
                      [jhsfgm].[dbo].[t_kensa] ks
                 WHERE
                      ks.kbn = TJ.kbn
                  AND ks.hosyousyo_no = TJ.hosyousyo_no
                  AND ks.kensa_oiban = 0
                 GROUP BY
                      ks.kbn
                    , ks.hosyousyo_no
                )
                 <> 40
            THEN 2
            WHEN
                (SELECT
                      ISNULL(MIN(nyukin_flg), 0) min_nyukin_flg
                 FROM
                      [jhsfgm].[dbo].[t_ryoukin] rk
                 WHERE
                      rk.kbn = TJ.kbn
                  AND rk.hosyousyo_no = TJ.hosyousyo_no
                  AND rk.syohin_cd IS NOT NULL
                 GROUP BY
                      rk.kbn
                    , rk.hosyousyo_no
                )
                 = 0
            THEN 3
            WHEN
                (SELECT
                      ISNULL(MIN(houkokusyo_status), 0) min_houkokusyo_status
                 FROM
                      [jhsfgm].[dbo].[t_kensa_hokokusyo_kanri] khk
                 WHERE
                      khk.kbn = TJ.kbn
                  AND khk.hosyousyo_no = TJ.hosyousyo_no
                 GROUP BY
                      khk.kbn
                    , khk.hosyousyo_no
                )
                 < 41
            THEN 4
            ELSE 1
       END
     , syori_flg = 1
     , syori_datetime = @UPD_DATETIME
     , upd_login_user_id = @UPD_USER_ID
     , upd_datetime = @UPD_DATETIME
  FROM
       t_hosyousyo_kanri HK
            LEFT OUTER JOIN t_jiban TJ
              ON HK.kbn = TJ.kbn
             AND HK.hosyousyo_no = TJ.hosyousyo_no
            LEFT OUTER JOIN
                (SELECT
                      JI.kbn
                    , JI.hosyousyo_no
                 FROM
                      t_jiban JI
                           LEFT OUTER JOIN t_teibetu_seikyuu TS
                             ON JI.kbn = TS.kbn
                            AND JI.hosyousyo_no = TS.hosyousyo_no
                 WHERE
                      ISNULL(TS.syouhin_cd,'') <> 'B2009'
                  AND TS.bunrui_cd = '140'
                  AND JI.t_koj_sokuhou_tyk_date is null
                )
                 TJ2
              ON TJ.kbn = TJ2.kbn
             AND TJ.hosyousyo_no = TJ2.hosyousyo_no
            LEFT OUTER JOIN m_kameiten KT
              ON TJ.kameiten_cd = KT.kameiten_cd
            LEFT OUTER JOIN m_ks_siyou KS
              ON TJ.hantei_cd1 = KS.ks_siyou_no
 
  /*************************
  *処理件数、エラーNOセット
  *************************/
  SELECT
      @ERR_NO = @@ERROR
      ,@COUNT_HK_INSERT_UPDATE = @@ROWCOUNT

  --エラーチェック
  IF @ERR_NO <> 0
  BEGIN
      -- トランザクションをロールバック（キャンセル）
      ROLLBACK TRANSACTION
      RETURN @ERR_NO
  END
  
 /*************************************************************************
 * 保証書管理データを更新する[業務完了日のみ]
 **************************************************************************/
  UPDATE
       [jhs_sys].[t_hosyousyo_kanri]
  SET
       gyoumu_kanry_date = CONVERT(VARCHAR,
	case 	when isnull(koj_umu,0)= 0 then
		case	when TJ.kairy_koj_sokuhou_tyk_date is null then
				TS.uridate
		else
				case	when TS.uridate>=TJ.kairy_koj_sokuhou_tyk_date then
						TS.uridate
				else
						TJ.kairy_koj_sokuhou_tyk_date
				end
		end
	else
		case	when TJ.kairy_koj_sokuhou_tyk_date is null then
				null
		else
				case	when TS.uridate>=TJ.kairy_koj_sokuhou_tyk_date then
						TS.uridate
				else
						TJ.kairy_koj_sokuhou_tyk_date
				end
		end
	end,111)
     , syori_flg = 1
     , syori_datetime = @UPD_DATETIME
     , upd_login_user_id = @UPD_USER_ID
     , upd_datetime = @UPD_DATETIME
  FROM
       [jhs_sys].[t_hosyousyo_kanri] HK
  LEFT OUTER JOIN  [jhs_sys].[t_jiban] TJ
	ON HK.kbn=TJ.kbn and HK.hosyousyo_no=TJ.hosyousyo_no
  LEFT OUTER JOIN (
	SELECT T.kbn,T.hosyousyo_no,max(T.uri_date) uridate 
	FROM [jhs_sys].[t_teibetu_seikyuu] T 
	INNER JOIN [jhs_sys].[m_syouhin] S
	ON T.syouhin_cd=S.syouhin_cd
	WHERE isnull(S.hosyou_umu,0)=1
	GROUP BY T.kbn,T.hosyousyo_no
  ) TS
	ON HK.kbn=TS.kbn and HK.hosyousyo_no=TS.hosyousyo_no 
  WHERE
	HK.gyoumu_kanry_date is null
	or
	CONVERT(VARCHAR,ISNULL(TJ.hosyousyo_hak_date,current_timestamp),111)>=CONVERT(VARCHAR,current_timestamp,111)

 /*************************************************************************
 * 保証書管理データを更新する[業務完了日経過年数]
 **************************************************************************/
  UPDATE
       [jhs_sys].[t_hosyousyo_kanri]
  SET
       keika_nensuu = case when gyoumu_kanry_date is null then 0
	else DATEDIFF(yy,gyoumu_kanry_date,CONVERT(VARCHAR,current_timestamp,111)) 
		-case 	when month(gyoumu_kanry_date)>month(current_timestamp) then 1
			when month(gyoumu_kanry_date)=month(current_timestamp) and day(gyoumu_kanry_date)>day(current_timestamp) then 1
			else 0
		end
	end
     , syori_flg = 1
     , syori_datetime = @UPD_DATETIME
     , upd_login_user_id = @UPD_USER_ID
     , upd_datetime = @UPD_DATETIME
  FROM
       [jhs_sys].[t_hosyousyo_kanri] HK


 /*************************************************************************
 * 保証書管理データを更新する[物件状況のみ]
 **************************************************************************/
  UPDATE
       [jhs_sys].[t_hosyousyo_kanri]
  SET
       bukken_jyky = [jhs_sys].[fnGetBukkenJyky](HK.kbn, HK.hosyousyo_no)
     , syori_flg = 1
     , syori_datetime = @UPD_DATETIME
     , upd_login_user_id = @UPD_USER_ID
     , upd_datetime = @UPD_DATETIME
  FROM
       [jhs_sys].[t_hosyousyo_kanri] HK

  /*************************
  *処理件数、エラーNOセット
  *************************/
  SELECT
      @ERR_NO = @@ERROR
      ,@COUNT_HK_BUKKEN_JYKY_UPDATE = @@ROWCOUNT

  --エラーチェック
  IF @ERR_NO <> 0
  BEGIN
      -- トランザクションをロールバック（キャンセル）
      ROLLBACK TRANSACTION
      RETURN @ERR_NO
  END 
  
  /*********************************************
  * 変更がある場合のみReportIFテーブルを同期更新
  *********************************************/
  BEGIN
	UPDATE
		 ReportIF
	SET
		 bukken_jyky_naiyou = KM1.meisyou
	   , kaiseki_kanry_naiyou = KM2.meisyou
	   , koj_kanry_handan =(
			  CASE
				   WHEN THK.koj_kanry = '0'
				   THEN '－'
				   WHEN THK.koj_kanry = '1'
				   THEN '○'
				   ELSE '×'
			  END)
	   , koj_kanry_naiyou =(
			  CASE
				   WHEN THK.koj_kanry = '0'
					 OR THK.koj_kanry = '1'
				   THEN NULL
				   ELSE '工事実施の確認が取れておりません'
			  END)
	   , nyuukin_jyky_handan =(
			  CASE
				   WHEN THK.nyuukin_jyky = '0'
				   THEN '－'
				   WHEN THK.nyuukin_jyky = '1'
					 OR THK.nyuukin_jyky = '3'
					 OR THK.nyuukin_jyky = '7'
				   THEN '○'
				   ELSE '×'
			  END)
	   , nyuukin_jyky_naiyou =(
			  CASE
				   WHEN THK.nyuukin_jyky = '3'
				   THEN '入金されなかった場合は無効となります'
				   WHEN THK.nyuukin_jyky = '7'
				   THEN '口座引落ができなかった場合は無効となります'
				   WHEN THK.nyuukin_jyky = '2'
                     OR THK.nyuukin_jyky = '4'
					 OR THK.nyuukin_jyky = '5'
					 OR THK.nyuukin_jyky = '6'
				   THEN '御入金額が不足しています'
				   ELSE NULL
			  END)
        , send_sts = '00'
        , earth_data_upd_datetime = @UPD_DATETIME
        , earth_data_upd_login_user_id = @UPD_USER_ID
	FROM
		 ReportIF REP
			  INNER JOIN t_hosyousyo_kanri THK
				ON REP.kokyaku_no = THK.kbn + THK.hosyousyo_no
			  INNER JOIN #TEMP_HOSYOUSYO_KANRI TMP
				ON THK.kbn = TMP.kbn
			   AND THK.hosyousyo_no = TMP.hosyousyo_no
			  LEFT OUTER JOIN m_kakutyou_meisyou KM1
				ON KM1.meisyou_syubetu = '10'
			   AND THK.bukken_jyky = KM1.code
			  LEFT OUTER JOIN m_kakutyou_meisyou KM2
				ON KM2.meisyou_syubetu = '11'
			   AND REPLACE(THK.kaiseki_kanry, '2', '0') = KM2.code
	WHERE
		 ISNULL(THK.bukken_jyky, '') <> ISNULL(TMP.bukken_jyky, '')
	  OR ISNULL(THK.kaiseki_kanry, '') <> ISNULL(TMP.kaiseki_kanry, '')
	  OR ISNULL(THK.koj_umu, '') <> ISNULL(TMP.koj_umu, '')
	  OR ISNULL(THK.koj_kanry, '') <> ISNULL(TMP.koj_kanry, '')
	  OR ISNULL(THK.nyuukin_kakunin_jyouken, '') <> ISNULL(TMP.nyuukin_kakunin_jyouken, '')
	  OR ISNULL(THK.nyuukin_jyky, '') <> ISNULL(TMP.nyuukin_jyky, '')
	  OR ISNULL(THK.kasi, '') <> ISNULL(TMP.kasi, '');
  END

  /*************************
  *処理件数、エラーNOセット
  *************************/
  SELECT
      @ERR_NO = @@ERROR
      ,@COUNT_IF_UPDATE = @@ROWCOUNT

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
