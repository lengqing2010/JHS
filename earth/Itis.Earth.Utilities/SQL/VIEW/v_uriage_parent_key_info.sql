

CREATE VIEW [jhs_sys].[v_uriage_parent_key_info]
AS
SELECT
    TUD.denpyou_unique_no
   ,TUD.himoduke_cd
   ,TUD.himoduke_table_type
   ,
     CASE
          WHEN idx1 = 0
          THEN himoduke_cd
          ELSE ISNULL(SUBSTRING(TUD.himoduke_cd, 1, idx1 - 1), '')
     END parent_key_1
   ,
     CASE
          WHEN idx2 = 0
          THEN
               CASE
                    WHEN LEN(himoduke_cd) <(idx1 + sep)
                      OR idx1 = 0
                    THEN NULL
                    ELSE RIGHT(himoduke_cd, LEN(himoduke_cd) -(idx1 - 1 + sep))
               END
          ELSE ISNULL(SUBSTRING(TUD.himoduke_cd, idx1 + sep, idx2 -(idx1 + sep)), '')
     END parent_key_2
   ,
     CASE
          WHEN idx3 = 0
          THEN
               CASE
                    WHEN LEN(himoduke_cd) <(idx2 + sep)
                      OR idx1 = 0
                      OR idx2 = 0
                    THEN NULL
                    ELSE RIGHT(himoduke_cd, LEN(himoduke_cd) -(idx2 - 1 + sep))
               END
          ELSE ISNULL(SUBSTRING(TUD.himoduke_cd, idx2 + sep, idx3 -(idx2 + sep)), '')
     END parent_key_3
   ,
     CASE
          WHEN LEN(himoduke_cd) <(idx3 + sep)
            OR idx1 = 0
            OR idx2 = 0
            OR idx3 = 0
          THEN NULL
          ELSE RIGHT(himoduke_cd, LEN(himoduke_cd) -(idx3 - 1 + sep))
     END parent_key_4
FROM
     t_uriage_data TUD
          INNER JOIN
              (SELECT
                    denpyou_unique_no
                  ,
                    CASE
                         WHEN CHARINDEX('$$$', himoduke_cd) > 0
                         THEN CHARINDEX('$$$', himoduke_cd)
                         ELSE 0
                    END idx1
                  ,
                    CASE
                         WHEN CHARINDEX('$$$', himoduke_cd) > 0
                         THEN
                              CASE
                                   WHEN CHARINDEX('$$$', himoduke_cd, CHARINDEX('$$$', himoduke_cd) + LEN('$$$')) > 0
                                   THEN CHARINDEX('$$$', himoduke_cd, CHARINDEX('$$$', himoduke_cd) + LEN('$$$'))
                                   ELSE 0
                              END
                         ELSE 0
                    END idx2
                  ,
                    CASE
                         WHEN CHARINDEX('$$$', himoduke_cd) > 0
                          AND CHARINDEX('$$$', himoduke_cd, CHARINDEX('$$$', himoduke_cd) + LEN('$$$')) > 0
                         THEN
                              CASE
                                   WHEN CHARINDEX('$$$', himoduke_cd, CHARINDEX('$$$', himoduke_cd, CHARINDEX('$$$', himoduke_cd) +LEN('$$$')) + LEN('$$$')) > 0
                                   THEN CHARINDEX('$$$', himoduke_cd, CHARINDEX('$$$', himoduke_cd, CHARINDEX('$$$', himoduke_cd) +LEN('$$$')) + LEN('$$$'))
                                   ELSE 0
                              END
                         ELSE 0
                    END idx3
                  , LEN('$$$') sep
               FROM
                    t_uriage_data
              )
               IDX
            ON TUD.denpyou_unique_no = IDX.denpyou_unique_no

