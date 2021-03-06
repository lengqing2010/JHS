CREATE VIEW [jhs_sys].[v_seikyuusyo_mihakkou_uri_data]
AS
SELECT
    us.* 
FROM
    jhs_sys.t_uriage_data us 
WHERE
    NOT EXISTS( 
        --請求データ作成済みの伝票を除外
        SELECT
            * 
        FROM
            jhs_sys.t_seikyuu_kagami sk 
            INNER JOIN jhs_sys.t_seikyuu_meisai sm 
                ON sk.seikyuusyo_no = sm.seikyuusyo_no 
        WHERE
            sk.torikesi = 0 
            AND sm.denpyou_unique_no = us.denpyou_unique_no
    ) 

