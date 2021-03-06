CREATE VIEW [jhs_sys].[v_seikyuu_nyuukin_jouhou]
AS
SELECT
  kbn
  , hosyousyo_no
  , bunrui_cd
  , uri_gaku AS zeikomi_seikyuu_gaku
  , ISNULL(zeikomi_nyuukin_gaku, 0) AS zeikomi_nyuukin_gaku
  , ISNULL(zeikomi_henkin_gaku, 0) AS zeikomi_henkin_gaku
  , uri_gaku - ISNULL(zeikomi_jitu_nyuukin_gaku, 0) AS nyuukin_zangaku 
FROM
  ( 
    SELECT
      TSG1.kbn
      , TSG1.hosyousyo_no
      , TSG1.bunrui_cd
      , TSG1.uri_gaku
      , TNG1.zeikomi_nyuukin_gaku
      , TNG1.zeikomi_henkin_gaku
      , TNG1.zeikomi_jitu_nyuukin_gaku 
    FROM
      ( 
        SELECT
             TS.kbn
           , TS.hosyousyo_no
           , TS.bunrui_cd
           , SUM(TS.uri_gaku) AS uri_gaku
        FROM
            (SELECT
                  TS.kbn
                , TS.hosyousyo_no
                , '100' AS bunrui_cd
                , (ISNULL(TS.uri_gaku, 0)
                   + ROUND(ISNULL(TS.syouhizei_gaku
                                 , ISNULL(TS.uri_gaku, 0) * ISNULL(SZ.zeiritu, 0))
                          , 0
                          , 1)
                  ) AS uri_gaku
             FROM
                  jhs_sys.t_teibetu_seikyuu TS
                       LEFT OUTER JOIN jhs_sys.m_syouhizei AS SZ
                         ON TS.zei_kbn = SZ.zei_kbn
             WHERE
                  (TS.bunrui_cd BETWEEN '100' AND '115')
               OR (TS.bunrui_cd = '180')
            )
             TS
        GROUP BY
             TS.kbn
           , TS.hosyousyo_no
           , TS.bunrui_cd
      ) AS TSG1 
      LEFT OUTER JOIN ( 
        SELECT
          kbn
          , hosyousyo_no
          , bunrui_cd
          , zeikomi_nyuukin_gaku
          , zeikomi_henkin_gaku
          , ISNULL(zeikomi_nyuukin_gaku, 0) - ISNULL(zeikomi_henkin_gaku, 0) AS zeikomi_jitu_nyuukin_gaku 
        FROM
          jhs_sys.t_teibetu_nyuukin AS TN 
        WHERE
          (bunrui_cd = '100')
      ) AS TNG1 
        ON TSG1.kbn = TNG1.kbn 
        AND TSG1.hosyousyo_no = TNG1.hosyousyo_no 
    UNION ALL 
    SELECT
      TSG2.kbn
      , TSG2.hosyousyo_no
      , TSG2.bunrui_cd
      , TSG2.uri_gaku
      , TNG2.zeikomi_nyuukin_gaku
      , TNG2.zeikomi_henkin_gaku
      , TNG2.zeikomi_jitu_nyuukin_gaku 
    FROM
      ( 
        SELECT
             TS.kbn
           , TS.hosyousyo_no
           , TS.bunrui_cd
           , SUM(TS.uri_gaku) AS uri_gaku
        FROM
            (SELECT
                  TS.kbn
                , TS.hosyousyo_no
                , TS.bunrui_cd
                , (ISNULL(TS.uri_gaku, 0)
                   + ROUND(ISNULL(TS.syouhizei_gaku
                                 , ISNULL(TS.uri_gaku, 0) * ISNULL(SZ.zeiritu, 0))
                          , 0
                          , 1)
                  ) AS uri_gaku
             FROM
                  jhs_sys.t_teibetu_seikyuu TS
                       LEFT OUTER JOIN jhs_sys.m_syouhizei AS SZ
                         ON TS.zei_kbn = SZ.zei_kbn
             WHERE
                  (TS.bunrui_cd BETWEEN '120' AND '170')
            )
             TS
        GROUP BY
             TS.kbn
           , TS.hosyousyo_no
           , TS.bunrui_cd
      ) AS TSG2 
      LEFT OUTER JOIN ( 
        SELECT
          kbn
          , hosyousyo_no
          , bunrui_cd
          , zeikomi_nyuukin_gaku
          , zeikomi_henkin_gaku
          , ISNULL(zeikomi_nyuukin_gaku, 0) - ISNULL(zeikomi_henkin_gaku, 0) AS zeikomi_jitu_nyuukin_gaku 
        FROM
          jhs_sys.t_teibetu_nyuukin AS TN 
        WHERE
          (bunrui_cd BETWEEN '120' AND '170')
      ) AS TNG2 
        ON TSG2.kbn = TNG2.kbn 
        AND TSG2.hosyousyo_no = TNG2.hosyousyo_no 
        AND TSG2.bunrui_cd = TNG2.bunrui_cd
  ) AS seikyuu_nyuukin_jouhou

