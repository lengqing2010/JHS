CREATE VIEW [jhs_sys].[v_seikyuu_saki_info]
AS
/***********************************************
 請求先マスタをベースに、関連マスタから請求先名や住所情報を取得するVIEW
 カナ名での請求先検索等に使用
 項目：請求先コード、請求先枝番、請求先区分、請求先マスタ.取消、
       請求先名、請求先名２、請求先カナ、
       請求書送付先[住所１、住所２、郵便番号、FAX番号]
 ユニーク検索キー：請求先コード、請求先枝番、請求先区分
***********************************************/
SELECT
    MSS.seikyuu_saki_cd
    , MSS.seikyuu_saki_brc
    , MSS.seikyuu_saki_kbn
    , MSS.torikesi
    , MMM.seikyuu_saki_mei
    , MMM.seikyuu_saki_mei2
    , MMM.seikyuu_saki_kana
    , CASE 
        WHEN MSS.skysy_soufu_jyuusyo1 IS NULL 
        THEN MMM.skysy_soufu_jyuusyo1 
        ELSE MSS.skysy_soufu_jyuusyo1 
        END skysy_soufu_jyuusyo1
    , CASE 
        WHEN MSS.skysy_soufu_jyuusyo1 IS NULL 
        THEN MMM.skysy_soufu_jyuusyo2 
        ELSE MSS.skysy_soufu_jyuusyo2 
        END skysy_soufu_jyuusyo2
    , CASE 
        WHEN MSS.skysy_soufu_jyuusyo1 IS NULL 
        THEN MMM.skysy_soufu_yuubin_no 
        ELSE MSS.skysy_soufu_yuubin_no 
        END skysy_soufu_yuubin_no
    , CASE 
        WHEN MSS.skysy_soufu_jyuusyo1 IS NULL 
        THEN MMM.skysy_soufu_tel_no 
        ELSE MSS.skysy_soufu_tel_no 
        END skysy_soufu_tel_no
    , CASE 
        WHEN MSS.skysy_soufu_jyuusyo1 IS NULL 
        THEN MMM.skysy_soufu_fax_no 
        ELSE MSS.skysy_soufu_fax_no 
        END skysy_soufu_fax_no 
    , MSS.ginkou_siten_cd
FROM
    jhs_sys.m_seikyuu_saki MSS 
    INNER JOIN ( 
        --加盟店マスタ
        SELECT
            MKM.kameiten_cd AS seikyuu_saki_cd
            , NULL AS seikyuu_saki_brc
            , '0' AS seikyuu_saki_kbn
            , MKM.kameiten_seisiki_mei AS seikyuu_saki_mei
            , MKM.kameiten_seisiki_mei_kana AS seikyuu_saki_mei2
            , ISNULL(MKM.tenmei_kana1, '') + ISNULL(MKM.tenmei_kana2, '') AS seikyuu_saki_kana
            , MKJ.jyuusyo1 AS skysy_soufu_jyuusyo1
            , MKJ.jyuusyo2 AS skysy_soufu_jyuusyo2
            , MKJ.yuubin_no AS skysy_soufu_yuubin_no
            , MKJ.tel_no AS skysy_soufu_tel_no
            , MKJ.fax_no AS skysy_soufu_fax_no 
        FROM
            jhs_sys.m_kameiten MKM 
            LEFT OUTER JOIN jhs_sys.m_kameiten_jyuusyo MKJ --加盟店住所マスタ
                ON MKJ.kameiten_cd = MKM.kameiten_cd 
                AND MKJ.seikyuusyo_flg = - 1 
        UNION ALL
        --調査会社マスタ
        SELECT
            tys_kaisya_cd
            , jigyousyo_cd
            , '1'
            , ISNULL(NULLIF(seikyuu_saki_shri_saki_mei,''),tys_kaisya_mei)
            , NULL
            , ISNULL(NULLIF(seikyuu_saki_shri_saki_kana,''),tys_kaisya_mei_kana)
            , ISNULL(NULLIF(skysy_soufu_jyuusyo1,''),jyuusyo1)
            , ISNULL(NULLIF(skysy_soufu_jyuusyo2,''),jyuusyo2)
            , ISNULL(NULLIF(skysy_soufu_yuubin_no,''),yuubin_no)
            , ISNULL(NULLIF(skysy_soufu_tel_no,''),tel_no)
            , ISNULL(NULLIF(shri_you_fax_no,''),fax_no)
        FROM
            jhs_sys.m_tyousakaisya
    ) MMM 
        ON MSS.seikyuu_saki_cd = MMM.seikyuu_saki_cd 
        AND MSS.seikyuu_saki_brc = CASE 
            WHEN MMM.seikyuu_saki_brc IS NULL 
            THEN MSS.seikyuu_saki_brc 
            ELSE MMM.seikyuu_saki_brc 
            END 
        AND MSS.seikyuu_saki_kbn = MMM.seikyuu_saki_kbn
