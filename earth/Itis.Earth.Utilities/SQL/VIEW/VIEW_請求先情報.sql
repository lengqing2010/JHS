CREATE OR REPLACE VIEW ZIBAN.VIEW_請求先情報
AS
/***********************************************
 請求先マスタをベースに、関連マスタから請求先名や住所情報を取得するVIEW（Oracle用）
 カナ名での請求先検索等に使用
 項目：請求先コード、請求先枝番、請求先区分、請求先マスタ.取消、
       請求先名、請求先名２、請求先カナ、
       請求書送付先[住所１、住所２、郵便番号、電話番号、FAX番号]
 ユニーク検索キー：請求先コード、請求先枝番、請求先区分
***********************************************/
SELECT
     MSS.請求先ｺｰﾄﾞ
   , MSS.請求先枝番
   , MSS.請求先区分
   , MSS.取消
   , MMM.請求先名
   , MMM.請求先名2
   , MMM.請求先ｶﾅ
   , (
          CASE
               WHEN MSS.請求書送付先住所1 IS NULL
               THEN MMM.請求書送付先住所1
               ELSE MSS.請求書送付先住所1
          END) AS "請求書送付先住所1"
   , (
          CASE
               WHEN MSS.請求書送付先住所1 IS NULL
               THEN MMM.請求書送付先住所2
               ELSE MSS.請求書送付先住所2
          END) AS "請求書送付先住所2"
   , (
          CASE
               WHEN MSS.請求書送付先住所1 IS NULL
               THEN MMM.請求書送付先郵便番号
               ELSE MSS.請求書送付先郵便番号
          END) AS "請求書送付先郵便番号"
   , (
          CASE
               WHEN MSS.請求書送付先住所1 IS NULL
               THEN MMM.請求書送付先電話番号
               ELSE MSS.請求書送付先電話番号
          END) AS "請求書送付先電話番号"
   , (
          CASE
               WHEN MSS.請求書送付先住所1 IS NULL
               THEN MMM.請求書送付先FAX番号
               ELSE MSS.請求書送付先FAX番号
          END) AS "請求書送付先FAX番号"
FROM
     ZIBAN.TBL_M請求先 MSS
          INNER JOIN
          --加盟店マスタ
              (SELECT
                    MKM.加盟店ｺｰﾄﾞ AS "請求先ｺｰﾄﾞ"
                  , NULL AS "請求先枝番"
                  , '0' AS "請求先区分"
                  , MKM.加盟店正式名 AS "請求先名"
                  , MKM.加盟店正式名ｶﾅ AS "請求先名2"
                  , (MKM.店名ｶﾅ1 || MKM.店名ｶﾅ2) AS "請求先ｶﾅ"
                  , MKJ.住所1 AS "請求書送付先住所1"
                  , MKJ.住所2 AS "請求書送付先住所2"
                  , MKJ.郵便番号 AS "請求書送付先郵便番号"
                  , MKJ.電話番号 AS "請求書送付先電話番号"
                  , MKJ.FAX番号 AS "請求書送付先FAX番号"
               FROM
                    ZIBAN.TBL_M加盟店 MKM
                  , ZIBAN.TBL_M加盟店住所 MKJ
               WHERE
                    MKM.加盟店ｺｰﾄﾞ = MKJ.加盟店ｺｰﾄﾞ(+)
                AND MKJ.請求書FLG(+) = '-1'
               UNION ALL
               --調査会社マスタ
               SELECT
                    MTY.調査会社ｺｰﾄﾞ AS "請求先ｺｰﾄﾞ"
                  , MTY.事業所ｺｰﾄﾞ AS "請求先枝番"
                  , '1' AS "請求先区分"
                  , NVL(NULLIF(MTY.請求先支払先名,''),MTY.調査会社名) AS "請求先名"
                  , NULL AS "請求先名2"
                  , NVL(NULLIF(MTY.請求先支払先名ｶﾅ,''),MTY.調査会社名ｶﾅ) AS "請求先ｶﾅ"
                  , NVL(NULLIF(MTY.請求書送付先住所1,''),MTY.住所1) AS "請求書送付先住所1"
                  , NVL(NULLIF(MTY.請求書送付先住所2,''),MTY.住所2) AS "請求書送付先住所2"
                  , NVL(NULLIF(MTY.請求書送付先郵便番号,''),MTY.郵便番号) AS "請求書送付先郵便番号"
                  , NVL(NULLIF(MTY.請求書送付先電話番号,''),MTY.電話番号) AS "請求書送付先電話番号"
                  , NVL(NULLIF(MTY.支払用FAX番号,''),MTY.FAX番号) AS "請求書送付先FAX番号"
               FROM
                    ZIBAN.TBL_M調査会社 MTY
              )
               MMM
            ON MSS.請求先ｺｰﾄﾞ = MMM.請求先ｺｰﾄﾞ
           AND MSS.請求先枝番 =(
                    CASE
                         WHEN MMM.請求先枝番 IS NULL
                         THEN MSS.請求先枝番
                         ELSE MMM.請求先枝番
                    END)
           AND MSS.請求先区分 = MMM.請求先区分
;
