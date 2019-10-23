-- =====================================================
-- Description:	�ۏ؏��Ǘ��e�[�u���ǉ�/�X�V����[����]
-- =====================================================
CREATE PROCEDURE [jhs_sys].[spSetHosyousyoKanri]
     @COUNT_HK_INSERT                 INT         OUTPUT  --�ۏ؏��Ǘ��f�[�^[�V�K�ǉ�]
    ,@COUNT_HK_INSERT_UPDATE          INT         OUTPUT  --�ۏ؏��Ǘ��f�[�^[�V�K�ǉ�+�X�V]
    ,@COUNT_HK_BUKKEN_JYKY_UPDATE     INT         OUTPUT  --�ۏ؏��Ǘ��f�[�^[�X�V�E�����󋵂̂�]
	,@COUNT_IF_UPDATE                 INT         OUTPUT  --ReportIF�f�[�^[�X�V]
    ,@ERR_NO                          INT         OUTPUT  --�G���[���
AS
BEGIN
  BEGIN TRANSACTION
  
	SET NOCOUNT ON;
	
	DECLARE @UPD_USER_ID               VARCHAR(30)  --�X�V���O�C�����[�U�[ID
	DECLARE @UPD_DATETIME              DATETIME     --�X�V����
	DECLARE @retType				   int			--�ԋp�^�C�v�i=1:�R�[�h�A<>1:MMDD�j
		
	--������
  SET @UPD_USER_ID = 'system'
  SET @UPD_DATETIME = getdate()
  SET @retType = 1

 /*********************************************
 * �ۏ؏��Ǘ��f�[�^�̖����݃f�[�^��V�K�ǉ�����
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
  *���������A�G���[NO�Z�b�g
  *************************/
  SELECT
      @ERR_NO = @@ERROR
      ,@COUNT_HK_INSERT = @@ROWCOUNT

  --�G���[�`�F�b�N
  IF @ERR_NO <> 0
  BEGIN
      -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j
      ROLLBACK TRANSACTION
      RETURN @ERR_NO
  END

  /*********************************************
  * �ۏ؏��Ǘ��e�[�u�����ꎞ�e�[�u���֑ޔ�
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
  *���������A�G���[NO�Z�b�g
  *************************/
  SELECT
      @ERR_NO = @@ERROR

  --�G���[�`�F�b�N
  IF @ERR_NO <> 0
  BEGIN
      -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j
      ROLLBACK TRANSACTION
      RETURN @ERR_NO
  END

 /******************************************
 * �ۏ؏��Ǘ��f�[�^���X�V����[�ۏ؏���]
 * - �ی��\���ȊO���Ώ�
 ******************************************/
  UPDATE
       [jhs_sys].[t_hosyousyo_kanri]
  SET
       kaiseki_kanry =
       CASE
--            WHEN TJ.hantei_cd1 in('97', '113', '1635')
            WHEN KS.ks_siyou like '%�ۗ�%' or KS.ks_siyou like '%�Ē���%' or KS.ks_siyou like '%�ۏ؂Ȃ�%' or KS.ks_siyou like '%�ۏؖ���%' or KS.ks_siyou like '%�v�Č���%' or KS.ks_siyou like '%���̑�,%' or (TJ.hantei_cd1 = '113' and HK.kbn<>'C')
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
		WHEN ISNULL(TJ.koj_hantei_kekka_flg, 0)=1 OR (KS.ks_siyou like '%�΍�%' AND TJ.hantei_cd1<>1396) OR KS.ks_siyou like '%��͊m�F%' or TJ.kbn='C'
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
  *���������A�G���[NO�Z�b�g
  *************************/
  SELECT
      @ERR_NO = @@ERROR
      ,@COUNT_HK_INSERT_UPDATE = @@ROWCOUNT

  --�G���[�`�F�b�N
  IF @ERR_NO <> 0
  BEGIN
      -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j
      ROLLBACK TRANSACTION
      RETURN @ERR_NO
  END
  
 /*************************************************************************
 * �ۏ؏��Ǘ��f�[�^���X�V����[�Ɩ��������̂�]
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
 * �ۏ؏��Ǘ��f�[�^���X�V����[�Ɩ��������o�ߔN��]
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
 * �ۏ؏��Ǘ��f�[�^���X�V����[�����󋵂̂�]
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
  *���������A�G���[NO�Z�b�g
  *************************/
  SELECT
      @ERR_NO = @@ERROR
      ,@COUNT_HK_BUKKEN_JYKY_UPDATE = @@ROWCOUNT

  --�G���[�`�F�b�N
  IF @ERR_NO <> 0
  BEGIN
      -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j
      ROLLBACK TRANSACTION
      RETURN @ERR_NO
  END 
  
  /*********************************************
  * �ύX������ꍇ�̂�ReportIF�e�[�u���𓯊��X�V
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
				   THEN '�|'
				   WHEN THK.koj_kanry = '1'
				   THEN '��'
				   ELSE '�~'
			  END)
	   , koj_kanry_naiyou =(
			  CASE
				   WHEN THK.koj_kanry = '0'
					 OR THK.koj_kanry = '1'
				   THEN NULL
				   ELSE '�H�����{�̊m�F�����Ă���܂���'
			  END)
	   , nyuukin_jyky_handan =(
			  CASE
				   WHEN THK.nyuukin_jyky = '0'
				   THEN '�|'
				   WHEN THK.nyuukin_jyky = '1'
					 OR THK.nyuukin_jyky = '3'
					 OR THK.nyuukin_jyky = '7'
				   THEN '��'
				   ELSE '�~'
			  END)
	   , nyuukin_jyky_naiyou =(
			  CASE
				   WHEN THK.nyuukin_jyky = '3'
				   THEN '��������Ȃ������ꍇ�͖����ƂȂ�܂�'
				   WHEN THK.nyuukin_jyky = '7'
				   THEN '�����������ł��Ȃ������ꍇ�͖����ƂȂ�܂�'
				   WHEN THK.nyuukin_jyky = '2'
                     OR THK.nyuukin_jyky = '4'
					 OR THK.nyuukin_jyky = '5'
					 OR THK.nyuukin_jyky = '6'
				   THEN '������z���s�����Ă��܂�'
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
  *���������A�G���[NO�Z�b�g
  *************************/
  SELECT
      @ERR_NO = @@ERROR
      ,@COUNT_IF_UPDATE = @@ROWCOUNT

  --�G���[�`�F�b�N
  IF @ERR_NO <> 0
  BEGIN
      -- �g�����U�N�V���������[���o�b�N�i�L�����Z���j
      ROLLBACK TRANSACTION
      RETURN @ERR_NO
  END 

-- �g�����U�N�V�������R�~�b�g
COMMIT TRANSACTION
RETURN @@ERROR 
  
END
GO
