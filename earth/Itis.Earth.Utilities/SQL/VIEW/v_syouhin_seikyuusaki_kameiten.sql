CREATE VIEW [jhs_sys].[v_syouhin_seikyuusaki_kameiten] AS
/***********************************************
 加盟店をベースに請求先を取得するためのVIEW
 項目：商品コード、倉庫コード(分類コード)、商品区分１、加盟店コード、
       請求先コード、請求先枝番、請求先区分
 ユニーク検索キー：商品コード、加盟店コード
***********************************************/
SELECT
    MSK.syouhin_cd
    , MSK.souko_cd
    , MSK.syouhin_kbn3
    , MSK.kameiten_cd
    , CASE
        WHEN MSH.kameiten_cd IS NULL
        THEN MSK.seikyuu_saki_cd
        ELSE MSH.seikyuu_henkou_saki
        END AS seikyuu_saki_cd
    , CASE
        WHEN MSH.kameiten_cd IS NULL
        THEN MSK.seikyuu_saki_brc
        ELSE MSH.brc
        END AS seikyuu_saki_brc
    , CASE
        WHEN MSH.kameiten_cd IS NULL
        THEN MSK.seikyuu_saki_kbn
        ELSE MSH.seikyuu_saki_kbn
        END AS seikyuu_saki_kbn
FROM
    (
        --商品マスタを親テーブルとする
        SELECT
            MS.syouhin_cd
            , MS.souko_cd
            , MS.syouhin_kbn3
            , MK.kameiten_cd AS kameiten_cd
            , MK.tys_seikyuu_saki_cd AS seikyuu_saki_cd
            , MK.tys_seikyuu_saki_brc AS seikyuu_saki_brc
            , MK.tys_seikyuu_saki_kbn AS seikyuu_saki_kbn
        FROM
            jhs_sys.m_syouhin MS
            INNER JOIN jhs_sys.m_kameiten MK
                ON MK.tys_seikyuu_saki_cd IS NOT NULL
        WHERE
            MS.syouhin_kbn3 LIKE 'T%'
        UNION ALL
        SELECT
            MS.syouhin_cd
            , MS.souko_cd
            , MS.syouhin_kbn3
            , MK.kameiten_cd AS kameiten_cd
            , MK.koj_seikyuu_saki_cd AS seikyuu_saki_cd
            , MK.koj_seikyuu_saki_brc AS seikyuu_saki_brc
            , MK.koj_seikyuu_saki_kbn AS seikyuu_saki_kbn
        FROM
            jhs_sys.m_syouhin MS
            INNER JOIN jhs_sys.m_kameiten MK
                ON MK.koj_seikyuu_saki_cd IS NOT NULL
        WHERE
            MS.syouhin_kbn3 LIKE 'K%'
        UNION ALL
        SELECT
            MS.syouhin_cd
            , MS.souko_cd
            , MS.syouhin_kbn3
            , MK.kameiten_cd AS kameiten_cd
            , MK.hansokuhin_seikyuu_saki_cd AS seikyuu_saki_cd
            , MK.hansokuhin_seikyuu_saki_brc AS seikyuu_saki_brc
            , MK.hansokuhin_seikyuu_saki_kbn AS seikyuu_saki_kbn
        FROM
            jhs_sys.m_syouhin MS
            INNER JOIN jhs_sys.m_kameiten MK
                ON MK.hansokuhin_seikyuu_saki_cd IS NOT NULL
        WHERE
            MS.syouhin_kbn3 LIKE 'H%'
        UNION ALL
        SELECT
            MS.syouhin_cd
            , MS.souko_cd
            , MS.syouhin_kbn3
            , MK.kameiten_cd AS kameiten_cd
            , MK.tatemono_seikyuu_saki_cd AS seikyuu_saki_cd
            , MK.tatemono_seikyuu_saki_brc AS seikyuu_saki_brc
            , MK.tatemono_seikyuu_saki_kbn AS seikyuu_saki_kbn
        FROM
            jhs_sys.m_syouhin MS
            INNER JOIN jhs_sys.m_kameiten MK
                ON MK.tatemono_seikyuu_saki_cd IS NOT NULL
        WHERE
            MS.syouhin_kbn3 LIKE 'S%'
                UNION ALL
        SELECT
            MS.syouhin_cd
            , MS.souko_cd
            , MS.syouhin_kbn3
            , MK.kameiten_cd AS kameiten_cd
            , MK.seikyuu_saki_cd5 AS seikyuu_saki_cd
            , MK.seikyuu_saki_brc5 AS seikyuu_saki_brc
            , MK.seikyuu_saki_kbn5 AS seikyuu_saki_kbn
        FROM
            jhs_sys.m_syouhin MS
            INNER JOIN jhs_sys.m_kameiten MK
                ON MK.seikyuu_saki_cd5 IS NOT NULL
        WHERE
            MS.syouhin_kbn3 LIKE '5%'
                UNION ALL
        SELECT
            MS.syouhin_cd
            , MS.souko_cd
            , MS.syouhin_kbn3
            , MK.kameiten_cd AS kameiten_cd
            , MK.seikyuu_saki_cd6 AS seikyuu_saki_cd
            , MK.seikyuu_saki_brc6 AS seikyuu_saki_brc
            , MK.seikyuu_saki_kbn6 AS seikyuu_saki_kbn
        FROM
            jhs_sys.m_syouhin MS
            INNER JOIN jhs_sys.m_kameiten MK
                ON MK.seikyuu_saki_cd6 IS NOT NULL
        WHERE
            MS.syouhin_kbn3 LIKE '6%'
                UNION ALL
        SELECT
            MS.syouhin_cd
            , MS.souko_cd
            , MS.syouhin_kbn3
            , MK.kameiten_cd AS kameiten_cd
            , MK.seikyuu_saki_cd7 AS seikyuu_saki_cd
            , MK.seikyuu_saki_brc7 AS seikyuu_saki_brc
            , MK.seikyuu_saki_kbn7 AS seikyuu_saki_kbn
        FROM
            jhs_sys.m_syouhin MS
            INNER JOIN jhs_sys.m_kameiten MK
                ON MK.seikyuu_saki_cd7 IS NOT NULL
        WHERE
            MS.syouhin_kbn3 LIKE '7%'
    ) MSK
    --請求先変更マスタに登録がある場合、そちらを優先
    LEFT OUTER JOIN jhs_sys.m_seikyuu_saki_henkou MSH
        ON MSK.kameiten_cd = MSH.kameiten_cd
        AND MSK.syouhin_kbn3 = MSH.syouhin_kbn
