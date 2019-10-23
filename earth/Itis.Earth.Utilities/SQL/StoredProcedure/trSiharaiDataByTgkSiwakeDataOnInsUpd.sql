-- =============================================
-- Description: <�x���f�[�^�쐬�g���K�[(ON ������v�d��f�[�^ AFTER INSERT, UPDATE)>
-- =============================================
CREATE TRIGGER [jhs_sys].[trSiharaiDataByTgkSiwakeDataOnInsUpd] 
   ON  jhs_sys.t_tgk_siwake_data 
   AFTER INSERT,UPDATE
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    --�J�[�\���p�ϐ��錾
    DECLARE @pKey               int         --�d�󃆃j�[�NNO(INSERTED)
    DECLARE @tgkJigyouCd        VARCHAR(10) --������v���Ə��R�[�h
    DECLARE @tgkShriSakiCd      VARCHAR(10) --������v�x����R�[�h
    DECLARE @shriSakiMei        VARCHAR(100)--������v�x����R�[�h
    DECLARE @siharaiDate        datetime    --�x���N����
    DECLARE @furikomi           bigint      --�x���z [�U��]
    DECLARE @sousai             bigint      --�x���z [���E]
    DECLARE @tekiyou            VARCHAR(200)--�E�v��
    DECLARE @userid             VARCHAR(30) --�o�^���O�C�����[�U�[ID
    DECLARE @BindString         CHAR(1)     --�͂�����[������^]:������v��
    
    DECLARE @minusFlg           int         --�ԓ`�[���s�t���O
    DECLARE @plusFlg            int         --���`�[���s�t���O
    DECLARE @maxNum             int         --����d�󃆃j�[�NNO�̍ő�`�[���j�[�NNO
    DECLARE @motoNum            int         --������`�[���j�[�NNO
    DECLARE @changeNum          int         --�X�V�Ώۂ�����ꍇ�̓`�[���j�[�NNO
    DECLARE @HN                 CHAR(2)     --�`�[��ʁF�x��
    DECLARE @HR                 CHAR(2)     --�`�[��ʁF�x�����

    --������
    SET @HN = 'HN';
    SET @HR = 'HR';
    SET @BindString = '"';

    /******************************************************
     *deleted�e�[�u���̃N���[�����쐬(INDEX���g�p���邽��)*
     ******************************************************/
    SELECT * INTO #deleted_bk FROM deleted
    CREATE INDEX idx_tmp_deleted ON #deleted_bk(siwake_unique_no)

    /******************************************
     *�X�V���ꂽ�s���J�[�\���Ŏ擾���A��������*
     ******************************************/
    --�J�[�\���̒�`
    DECLARE CUR_SIHARAI_DATA
    CURSOR FOR 
        SELECT
            INS.siwake_unique_no
/*2013/7/3�C��
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
            , CASE 	WHEN REPLACE(INS.data_category,@BindString,'')='F58' THEN '�i���j���{�Z��ۏ،����@�\' 
			WHEN REPLACE(INS.data_category,@BindString,'') in ('W001','W002') and REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='03919' and REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'') = '21202' AND REPLACE(INS.CR_SUB_ACCOUNT_CD_B,@BindString,'') = '9300' THEN REPLACE(INS.DR_OTHER_NAME,@BindString,'')
		ELSE REPLACE(INS.dr_other_name,@BindString,'') END kari_aitesaki_mei
            , SUBSTRING(REPLACE(INS.account_date,@BindString,''), 1, 4) + '/' + 
              SUBSTRING(REPLACE(INS.account_date,@BindString,''), 5, 2) + '/' + 
              SUBSTRING(REPLACE(INS.account_date,@BindString,''), 7, 2) keijou_date
/*2012/11/28�C��
            , CASE REPLACE(INS.data_category,@BindString,'')
                WHEN 'W001' THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULL�̏ꍇ0
                WHEN 'F17' THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULL�̏ꍇ0
                ELSE 0 
                END furikomi
            , CASE REPLACE(INS.data_category,@BindString,'')
                WHEN 'F19' THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULL�̏ꍇ0
                ELSE 0 
                END sousai
*/
            , CASE	WHEN REPLACE(INS.data_category,@BindString,'')= 'F17' AND REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='21202' THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULL�̏ꍇ0
			WHEN REPLACE(INS.data_category,@BindString,'') in ('W001','W002') THEN
				CASE 	WHEN 	(REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='21202' AND REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'')<>'49726')
/*					OR																		--2013/11/6�폜
						(REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='49726' AND REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'') = '01228')*/	--2013/11/6�폜
					OR
						(REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='49726' AND REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'') = '22101' AND REPLACE(INS.CR_SUB_ACCOUNT_CD_B,@BindString,'') = '018')
					THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULL�̏ꍇ0
					WHEN (REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='65234' AND REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'') = '21202')	/*2013/7/3�ǉ�*/
					THEN CAST(ISNULL(INS.dr_journal_line_amount,0)*(-1.05) AS INT)  --NULL�̏ꍇ0								/*2013/7/3�ǉ�*/
	                		ELSE 0 END
			WHEN REPLACE(INS.data_category,@BindString,'')= 'F58' AND REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='21202' THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULL�̏ꍇ0	/*2013/7/3�ǉ�*/
		ELSE 0
                END furikomi
            , CASE	WHEN REPLACE(INS.data_category,@BindString,'')='F19' AND REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='21202' THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULL�̏ꍇ0
			WHEN REPLACE(INS.data_category,@BindString,'') in ('W001','W002') AND REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='49726' THEN
				CASE WHEN 	/*REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'')='01518'		--2013/11/6�폜
					OR*/										--2013/11/6�폜
						(REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'') = '22101' AND REPLACE(INS.CR_SUB_ACCOUNT_CD_B,@BindString,'') = '777')
				THEN CAST(ISNULL(INS.dr_journal_line_amount,0) AS INT) --NULL�̏ꍇ0
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
--            REPLACE(INS.data_category,@BindString,'') IN ('W001', 'F17', 'F19') 2012/11/28�C��
--            (REPLACE(INS.data_category,@BindString,'') IN ('F17', 'F19') AND REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='21202')	2013/7/3�C��
            (REPLACE(INS.data_category,@BindString,'') IN ('F17', 'F19','F58') AND REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='21202')
	OR
            (REPLACE(INS.data_category,@BindString,'') IN ('W001', 'W002') AND
		((REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='21202' AND REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'')<>'49726')
		OR
		 (REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='49726' AND 
			(/*REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'') in ('01518','01228')		--2013/11/6�폜
			OR*/											--2013/11/6�폜
			(REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'')='22101' AND REPLACE(INS.CR_SUB_ACCOUNT_CD_B,@BindString,'') in ('018','777'))
			)
		)
		OR
		(REPLACE(INS.DR_ACCOUNT_CD_B,@BindString,'')='65234' AND REPLACE(INS.CR_ACCOUNT_CD_B,@BindString,'')='21202'))	--2013/7/3�ǉ�
	  )
    --�J�[�\�����J��
    OPEN CUR_SIHARAI_DATA

    --�J�[�\�����ŏ��̈�s���擾
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

    -- �J�[�\���Ŏ擾�����s���I�[�ɒB����܂ŏ������p������
    WHILE @@FETCH_STATUS = 0
    BEGIN 

        -- �`�[���j�[�NNO�̃��Z�b�g
        SET @maxNum = NULL;
		    SET @motoNum = NULL;
        -- �`�[���s�t���O�̃��Z�b�g
        SET @minusFlg = NULL;
        SET @plusFlg = NULL;

        /*******************************************************
        * �X�V���ꂽ�f�[�^�̎d�󃆃j�[�NNO�ƕR�t��
        * �x���f�[�^�̓`�[���j�[�NNO�̍ő�l���擾
        ********************************************************/
        SELECT @maxNum = MAX(SHR.denpyou_unique_no)
          FROM [jhs_sys].[t_siharai_data] SHR
         WHERE SHR.tgk_siwake_unique_no = @pKey
        
        /********************************************************
        * �X�V���ꂽ�d�󃆃j�[�NNO�̃f�[�^�ƁA�x���f�[�^��
        * ��r�Ώۂ���ł��قȂ�ꍇ�A�`�[���j�[�NNO��@changeNum�Ɋi�[����
        ********************************************************/
        SELECT
            @changeNum = SHR.denpyou_unique_no 
        FROM
            [jhs_sys].[t_siharai_data] SHR 
        WHERE
            SHR.denpyou_unique_no = @maxNum 
            AND SHR.torikesi_moto_denpyou_unique_no IS NULL --���ߓ`�[������`�[�Ŗ��������w��
            AND ( 
                ISNULL(SHR.skk_jigyou_cd, '') <> ISNULL(@tgkJigyouCd, '') 
                OR ISNULL(SHR.skk_shri_saki_cd, '') <> ISNULL(@tgkShriSakiCd, '') 
                OR ISNULL(SHR.siharai_date, '') <> ISNULL(@siharaiDate, '') 
                OR ISNULL(SHR.furikomi, '') <> ISNULL(@furikomi, '') 
                OR ISNULL(SHR.sousai, '') <> ISNULL(@sousai, '') 
                OR ISNULL(SHR.tekiyou_mei, '') <> ISNULL(@tekiyou, '')
            ) 
        
        /****************************************************
         * �X�V�����̔���                                   *
         *    @minusFlg = 1 �� �v���X�`�[���s               *
         *    @plusFlg = 1 �� �}�C�i�X�`�[���s              *
         ****************************************************/
 -- �X�V�Ώۃf�[�^�ƍX�V�ΏۂɕR�t�����߂̓`�[�f�[�^�ɍ��ق�����ꍇ
        -- �}�C�i�X�`�[�𔭍s����
        IF @changeNum IS NOT NULL 
            SET @minusFlg = 1

        -- �x���N�������w�肳��Ă���A�U���A���E�̉��ꂩ�̎x���z���[���ȊO�̏ꍇ
        -- �v���X�`�[�𔭍s����
        IF @siharaiDate IS NOT NULL AND (ISNULL(@furikomi, 0) <> 0 OR ISNULL(@sousai, 0) <> 0)
            SET @plusFlg = 1

        /***********************************************
        * �t���O������ꍇ�A�ԓ`�[�i�}�C�i�X�`�[�j���s *
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
        * �t���O������ꍇ�A�������͓`�[���j�[�NNO�̍ő�l *
        * ���擾�o���Ȃ��ꍇ�A���`�[�i�v���X�`�[�j���s     *
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

        --���̂P�����擾����
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
    
    -- �J�[�\�������
    CLOSE CUR_SIHARAI_DATA

    -- �J�[�\���̃��������J��
    DEALLOCATE CUR_SIHARAI_DATA
    
END











