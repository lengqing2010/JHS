CREATE OR REPLACE VIEW ZIBAN.VIEW_¿æîñ
AS
/***********************************************
 ¿æ}X^ðx[XÉAÖA}X^©ç¿æ¼âZîñðæ¾·éVIEWiOraclepj
 Ji¼ÅÌ¿æõÉgp
 ÚF¿æR[hA¿æ}ÔA¿ææªA¿æ}X^.æÁA
       ¿æ¼A¿æ¼QA¿æJiA
       ¿tæ[ZPAZQAXÖÔAdbÔAFAXÔ]
 j[NõL[F¿æR[hA¿æ}ÔA¿ææª
***********************************************/
SELECT
     MSS.¿æº°ÄÞ
   , MSS.¿æ}Ô
   , MSS.¿ææª
   , MSS.æÁ
   , MMM.¿æ¼
   , MMM.¿æ¼2
   , MMM.¿æ¶Å
   , (
          CASE
               WHEN MSS.¿tæZ1 IS NULL
               THEN MMM.¿tæZ1
               ELSE MSS.¿tæZ1
          END) AS "¿tæZ1"
   , (
          CASE
               WHEN MSS.¿tæZ1 IS NULL
               THEN MMM.¿tæZ2
               ELSE MSS.¿tæZ2
          END) AS "¿tæZ2"
   , (
          CASE
               WHEN MSS.¿tæZ1 IS NULL
               THEN MMM.¿tæXÖÔ
               ELSE MSS.¿tæXÖÔ
          END) AS "¿tæXÖÔ"
   , (
          CASE
               WHEN MSS.¿tæZ1 IS NULL
               THEN MMM.¿tædbÔ
               ELSE MSS.¿tædbÔ
          END) AS "¿tædbÔ"
   , (
          CASE
               WHEN MSS.¿tæZ1 IS NULL
               THEN MMM.¿tæFAXÔ
               ELSE MSS.¿tæFAXÔ
          END) AS "¿tæFAXÔ"
FROM
     ZIBAN.TBL_M¿æ MSS
          INNER JOIN
          --Á¿X}X^
              (SELECT
                    MKM.Á¿Xº°ÄÞ AS "¿æº°ÄÞ"
                  , NULL AS "¿æ}Ô"
                  , '0' AS "¿ææª"
                  , MKM.Á¿X³®¼ AS "¿æ¼"
                  , MKM.Á¿X³®¼¶Å AS "¿æ¼2"
                  , (MKM.X¼¶Å1 || MKM.X¼¶Å2) AS "¿æ¶Å"
                  , MKJ.Z1 AS "¿tæZ1"
                  , MKJ.Z2 AS "¿tæZ2"
                  , MKJ.XÖÔ AS "¿tæXÖÔ"
                  , MKJ.dbÔ AS "¿tædbÔ"
                  , MKJ.FAXÔ AS "¿tæFAXÔ"
               FROM
                    ZIBAN.TBL_MÁ¿X MKM
                  , ZIBAN.TBL_MÁ¿XZ MKJ
               WHERE
                    MKM.Á¿Xº°ÄÞ = MKJ.Á¿Xº°ÄÞ(+)
                AND MKJ.¿FLG(+) = '-1'
               UNION ALL
               --²¸ïÐ}X^
               SELECT
                    MTY.²¸ïÐº°ÄÞ AS "¿æº°ÄÞ"
                  , MTY.Æº°ÄÞ AS "¿æ}Ô"
                  , '1' AS "¿ææª"
                  , NVL(NULLIF(MTY.¿æx¥æ¼,''),MTY.²¸ïÐ¼) AS "¿æ¼"
                  , NULL AS "¿æ¼2"
                  , NVL(NULLIF(MTY.¿æx¥æ¼¶Å,''),MTY.²¸ïÐ¼¶Å) AS "¿æ¶Å"
                  , NVL(NULLIF(MTY.¿tæZ1,''),MTY.Z1) AS "¿tæZ1"
                  , NVL(NULLIF(MTY.¿tæZ2,''),MTY.Z2) AS "¿tæZ2"
                  , NVL(NULLIF(MTY.¿tæXÖÔ,''),MTY.XÖÔ) AS "¿tæXÖÔ"
                  , NVL(NULLIF(MTY.¿tædbÔ,''),MTY.dbÔ) AS "¿tædbÔ"
                  , NVL(NULLIF(MTY.x¥pFAXÔ,''),MTY.FAXÔ) AS "¿tæFAXÔ"
               FROM
                    ZIBAN.TBL_M²¸ïÐ MTY
              )
               MMM
            ON MSS.¿æº°ÄÞ = MMM.¿æº°ÄÞ
           AND MSS.¿æ}Ô =(
                    CASE
                         WHEN MMM.¿æ}Ô IS NULL
                         THEN MSS.¿æ}Ô
                         ELSE MMM.¿æ}Ô
                    END)
           AND MSS.¿ææª = MMM.¿ææª
;
