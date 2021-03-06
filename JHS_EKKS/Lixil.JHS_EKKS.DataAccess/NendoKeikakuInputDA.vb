Option Explicit On
Option Strict On

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' 年度計画値設定
''' </summary>
''' <remarks>年度計画値設定</remarks>
''' <history>
''' <para>2012/11/14 P-44979 王新 新規作成 </para>
''' </history>
Public Class NendoKeikakuInputDA

    ''' <summary>
    ''' 全社計画管理テーブルから計画情報を取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <returns>全社の計画情報</returns>
    ''' <remarks>全社計画管理テーブルから計画情報を取得する</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Function SelZensyaKeikakuKanriData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" SELECT ")
            .AppendLine("     keikaku_nendo, ")                 '計画年度
            .AppendLine("     add_datetime, ")                  '登録日時
            .AppendLine("     keikaku_kensuu, ")                '計画調査件数
            .AppendLine("     keikaku_uri_kingaku, ")           '計画売上金額
            .AppendLine("     keikaku_arari, ")                 '計画粗利
            .AppendLine("     keikaku_settutei_kome, ")         '計画設定時ｺﾒﾝﾄ
            .AppendLine("     kakutei_flg ")                    '確定FLG
            .AppendLine(" FROM ")
            .AppendLine("     t_zensya_keikaku_kanri AS TZKK WITH(READCOMMITTED) ")
            .AppendLine(" WHERE EXISTS ")
            .AppendLine(" ( ")
            .AppendLine("     SELECT keikaku_nendo, ")
            .AppendLine("         MAX(add_datetime) ")
            .AppendLine("     FROM t_zensya_keikaku_kanri AS SUB_TZKK WITH(READCOMMITTED)  ")
            .AppendLine("     WHERE keikaku_nendo = @keikaku_nendo ")
            .AppendLine("     GROUP BY keikaku_nendo ")
            .AppendLine("     HAVING TZKK.keikaku_nendo = SUB_TZKK.keikaku_nendo ")
            .AppendLine("     AND CASE WHEN ISNULL(CONVERT(VARCHAR,TZKK.kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                 + CONVERT(VARCHAR,TZKK.add_datetime,121)  ")
            .AppendLine("         = MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,SUB_TZKK.kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                 + CONVERT(VARCHAR,SUB_TZKK.add_datetime,121)) ")
            .AppendLine(" ) ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))     '計画年度(YYYY)

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)
    End Function

    ''' <summary>
    ''' 計画管理_加盟店ﾏｽﾀのデータを取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <returns>計画管理_加盟店ﾏｽﾀのデータ</returns>
    ''' <remarks>計画管理_加盟店ﾏｽﾀから支店名を取得する</remarks>
    Public Function SelKeikakuKameitenData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" SELECT ")
            .AppendLine("     kameiten_cd, ")
            .AppendLine("     eigyou_kbn, ")
            .AppendLine("     kameiten_mei, ")
            .AppendLine("     shiten_mei, ")
            .AppendLine("     busyo_cd ")
            .AppendLine(" FROM m_keikaku_kameiten ")
            .AppendLine(" WHERE keikaku_nendo = @keikaku_nendo ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, strKeikakuNendo))     '計画年度(YYYY)

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)
    End Function

    ''' <summary>
    ''' 支店別計画管理テーブルから各支店の計画情報を取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <returns>各支店の計画情報</returns>
    ''' <remarks>支店別計画管理テーブルから各支店の計画情報を取得する</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Function SelSitenbetuKeikakuKanriData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" SELECT TSKK.busyo_cd, ")                       '部署コード
            .AppendLine("     TSKK.busyo_mei, ")                         '部署名(支店名)
            .AppendLine("     TSKK.add_datetime, ")                     '登録日時
            .AppendLine("     TSKK.kakutei_flg, ")                      '確定FLG
            .AppendLine("     TSKK.eigyou_keikaku_kensuu, ")            '営業_計画調査件数
            .AppendLine("     TSKK.tokuhan_keikaku_kensuu, ")           '特販_計画調査件数
            .AppendLine("     TSKK.FC_keikaku_kensuu, ")                'FC_計画調査件数
            .AppendLine("     ISNULL(TSKK.Sum_keikaku_kensuu,0) AS Sum_keikaku_kensuu, ")           '合計調査件数

            .AppendLine("     TSKK.eigyou_keikaku_uri_kingaku, ")       '営業_計画売上金額
            .AppendLine("     TSKK.tokuhan_keikaku_uri_kingaku, ")      '特販_計画売上金額
            .AppendLine("     TSKK.FC_keikaku_uri_kingaku, ")           'FC_計画売上金額
            .AppendLine("     ISNULL(TSKK.Sum_keikaku_uri_kingaku,0) AS Sum_keikaku_uri_kingaku, ") '合計売上金額

            .AppendLine("     TSKK.eigyou_keikaku_arari, ")             '営業_計画粗利
            .AppendLine("     TSKK.tokuhan_keikaku_arari, ")            '特販_計画粗利
            .AppendLine("     TSKK.FC_keikaku_arari, ")                 'FC_計画粗利
            .AppendLine("     ISNULL(TSKK.Sum_keikaku_arari,0) AS Sum_keikaku_arari, ")              '合計粗利

            .AppendLine("     TJK.eigyou_jittuseki_kensuu, ")           '過去実績件数(営業)
            .AppendLine("     TJK.tokuhan_jittuseki_kensuu, ")          '過去実績件数(特販)
            .AppendLine("     TJK.FC_jittuseki_kensuu, ")               '過去実績件数(FC)
            .AppendLine("     (ISNULL(TJK.eigyou_jittuseki_kensuu,0) ")
            .AppendLine("     + ISNULL(TJK.tokuhan_jittuseki_kensuu,0)  ")
            .AppendLine("     + ISNULL(TJK.FC_jittuseki_kensuu,0)) AS Sum_jittuseki_kensuu, ")    '合計過去実績件数

            .AppendLine("     TJK.eigyou_jittuseki_kingaku, ")          '過去実績金額(営業)
            .AppendLine("     TJK.tokuhan_jittuseki_kingaku, ")         '過去実績金額(特販)
            .AppendLine("     TJK.FC_jittuseki_kingaku, ")              '過去実績金額(FC)
            .AppendLine("     (ISNULL(TJK.eigyou_jittuseki_kingaku,0) ")
            .AppendLine("     + ISNULL(TJK.tokuhan_jittuseki_kingaku,0) ")
            .AppendLine("     + ISNULL(TJK.FC_jittuseki_kingaku,0)) AS Sum_jittuseki_kingaku, ")  '合計過去実績金額
            .AppendLine("      ")
            .AppendLine("     TJK.eigyou_jittuseki_arari, ")            '過去実績粗利(営業)
            .AppendLine("     TJK.tokuhan_jittuseki_arari, ")           '過去実績粗利(特販)
            .AppendLine("     TJK.FC_jittuseki_arari, ")                '過去実績粗利(FC)
            .AppendLine("     (ISNULL(TJK.eigyou_jittuseki_arari,0) ")
            .AppendLine("     + ISNULL(TJK.tokuhan_jittuseki_arari,0) ")
            .AppendLine("     + ISNULL(TJK.FC_jittuseki_arari,0)) AS Sum_jittuseki_arari ")       '合計過去実績粗利
            .AppendLine(" FROM ( ")
            .AppendLine("     SELECT ")
            .AppendLine("         siten_mei AS busyo_mei, ")
            .AppendLine("         busyo_cd, ")
            .AppendLine("         add_datetime, ")
            .AppendLine("         kakutei_flg, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,eigyou_keikaku_kensuu),'') AS eigyou_keikaku_kensuu, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,tokuhan_keikaku_kensuu),'') AS tokuhan_keikaku_kensuu, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,FC_keikaku_kensuu),'') AS FC_keikaku_kensuu, ")
            .AppendLine("         (ISNULL(eigyou_keikaku_kensuu,0) + ISNULL(tokuhan_keikaku_kensuu,0) + ISNULL(FC_keikaku_kensuu,0)) AS Sum_keikaku_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,eigyou_keikaku_uri_kingaku),'') AS eigyou_keikaku_uri_kingaku, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,tokuhan_keikaku_uri_kingaku),'') AS tokuhan_keikaku_uri_kingaku, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,FC_keikaku_uri_kingaku),'') AS FC_keikaku_uri_kingaku, ")
            .AppendLine("         (ISNULL(eigyou_keikaku_uri_kingaku,0) + ISNULL(tokuhan_keikaku_uri_kingaku,0) + ISNULL(FC_keikaku_uri_kingaku,0)) AS Sum_keikaku_uri_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,eigyou_keikaku_arari),'') AS eigyou_keikaku_arari, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,tokuhan_keikaku_arari),'') AS tokuhan_keikaku_arari, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,FC_keikaku_arari),'') AS FC_keikaku_arari, ")
            .AppendLine("         (ISNULL(eigyou_keikaku_arari,0) + ISNULL(tokuhan_keikaku_arari,0) + ISNULL(FC_keikaku_arari,0)) AS Sum_keikaku_arari ")
            .AppendLine("     FROM ")
            .AppendLine("         t_sitenbetu_keikaku_kanri AS TKK WITH(READCOMMITTED) ")
            .AppendLine("     WHERE EXISTS ")
            .AppendLine("     ( ")
            .AppendLine("         SELECT busyo_cd, ")
            .AppendLine("                 MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                     + CONVERT(VARCHAR,add_datetime,121)) ")
            .AppendLine("         FROM  ")
            .AppendLine("             t_sitenbetu_keikaku_kanri AS SUBTKK WITH(READCOMMITTED) ")
            .AppendLine("         WHERE ")
            .AppendLine("             DATENAME(YYYY, keikaku_nendo) = @keikaku_nendo ")
            .AppendLine("         GROUP BY busyo_cd ")
            .AppendLine("         HAVING TKK.busyo_cd = SUBTKK.busyo_cd ")
            .AppendLine("         AND CASE WHEN ISNULL(CONVERT(VARCHAR,TKK.kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                     + CONVERT(VARCHAR,TKK.add_datetime,121)  ")
            .AppendLine("             = MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,SUBTKK.kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                     + CONVERT(VARCHAR,SUBTKK.add_datetime,121)) ")
            .AppendLine("     ) ")
            .AppendLine("     AND DATENAME(YYYY, TKK.keikaku_nendo) = @keikaku_nendo ")
            .AppendLine(" ) AS TSKK ")
            .AppendLine(" LEFT OUTER JOIN ( ")
            .AppendLine("     SELECT busyo_cd, ")
            .AppendLine("            SUM(eigyou_jittuseki_kensuu) AS eigyou_jittuseki_kensuu, ")
            .AppendLine("            SUM(tokuhan_jittuseki_kensuu) AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("            SUM(FC_jittuseki_kensuu) AS FC_jittuseki_kensuu, ")
            .AppendLine("            SUM(eigyou_jittuseki_kingaku) AS eigyou_jittuseki_kingaku, ")
            .AppendLine("            SUM(tokuhan_jittuseki_kingaku) AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("            SUM(FC_jittuseki_kingaku) AS FC_jittuseki_kingaku, ")
            .AppendLine("            SUM(eigyou_jittuseki_arari) AS eigyou_jittuseki_arari, ")
            .AppendLine("            SUM(tokuhan_jittuseki_arari) AS tokuhan_jittuseki_arari, ")
            .AppendLine("            SUM(FC_jittuseki_arari) AS FC_jittuseki_arari ")
            .AppendLine("     FROM ( ")
            '実績データを取得する
            .AppendLine("     SELECT MKK.busyo_cd, ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kensuu],0)) AS eigyou_jittuseki_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kingaku],0)) AS eigyou_jittuseki_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_arari],0)) AS eigyou_jittuseki_arari, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("         0 AS tokuhan_jittuseki_arari, ")
            .AppendLine("         0 AS FC_jittuseki_kensuu, ")
            .AppendLine("         0 AS FC_jittuseki_kingaku, ")
            .AppendLine("         0 AS FC_jittuseki_arari ")
            .AppendLine("     FROM m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("     INNER JOIN t_jisseki_kanri AS TJK WITH(READCOMMITTED) ")
            .AppendLine("     ON MKK.keikaku_nendo = TJK.keikaku_nendo ")
            .AppendLine("     AND MKK.kameiten_cd = TJK.kameiten_cd ")
            .AppendLine("     WHERE MKK.keikaku_nendo = @keikaku_nendo2 ")
            .AppendLine("     AND MKK.eigyou_kbn = @eigyou_kbn1 ")
            .AppendLine("     GROUP BY busyo_cd ")
            .AppendLine("     UNION ALL ")
            .AppendLine("     SELECT MKK.busyo_cd, ")
            .AppendLine("         0 AS eigyou_jittuseki_kensuu, ")
            .AppendLine("         0 AS eigyou_jittuseki_kingaku, ")
            .AppendLine("         0 AS eigyou_jittuseki_arari, ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kensuu],0)) AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kingaku],0)) AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_arari],0)) AS tokuhan_jittuseki_arari, ")
            .AppendLine("         0 AS FC_jittuseki_kensuu, ")
            .AppendLine("         0 AS FC_jittuseki_kingaku, ")
            .AppendLine("         0 AS FC_jittuseki_arari ")
            .AppendLine("     FROM m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("     INNER JOIN t_jisseki_kanri AS TJK WITH(READCOMMITTED) ")
            .AppendLine("     ON MKK.keikaku_nendo = TJK.keikaku_nendo ")
            .AppendLine("     AND MKK.kameiten_cd = TJK.kameiten_cd ")
            .AppendLine("     WHERE MKK.keikaku_nendo = @keikaku_nendo2 ")
            .AppendLine("     AND MKK.eigyou_kbn = @eigyou_kbn2 ")
            .AppendLine("     GROUP BY busyo_cd ")
            .AppendLine("     UNION ALL ")
            .AppendLine("     SELECT MKK.busyo_cd, ")
            .AppendLine("         0 AS eigyou_jittuseki_kensuu, ")
            .AppendLine("         0 AS eigyou_jittuseki_kingaku, ")
            .AppendLine("         0 AS eigyou_jittuseki_arari, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("         0 AS tokuhan_jittuseki_arari, ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kensuu],0)) AS FC_jittuseki_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kingaku],0)) AS FC_jittuseki_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_arari],0)) AS FC_jittuseki_arari ")
            .AppendLine("     FROM m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("     INNER JOIN t_jisseki_kanri AS TJK WITH(READCOMMITTED) ")
            .AppendLine("     ON MKK.keikaku_nendo = TJK.keikaku_nendo ")
            .AppendLine("     AND MKK.kameiten_cd = TJK.kameiten_cd ")
            .AppendLine("     WHERE MKK.keikaku_nendo = @keikaku_nendo2 ")
            .AppendLine("     AND MKK.eigyou_kbn = @eigyou_kbn3 ")
            .AppendLine("     GROUP BY busyo_cd ")
            .AppendLine("     ) AS TJK   ")
            .AppendLine("     GROUP BY busyo_cd  ")
            .AppendLine(" ) AS TJK ")
            .AppendLine(" ON TSKK.busyo_cd = TJK.busyo_cd ")
            .AppendLine(" ORDER BY ")
            .AppendLine("     TSKK.busyo_cd ASC ")

        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))     '計画年度(YYYY)
        paramList.Add(MakeParam("@keikaku_nendo2", SqlDbType.Char, 4, Convert.ToInt32(strKeikakuNendo) - 1))     '前年年度(YYYY)
        paramList.Add(MakeParam("@eigyou_kbn1", SqlDbType.VarChar, 1, "1"))     '営業区分(営業)
        paramList.Add(MakeParam("@eigyou_kbn2", SqlDbType.VarChar, 1, "3"))     '営業区分(特販)
        paramList.Add(MakeParam("@eigyou_kbn3", SqlDbType.VarChar, 1, "4"))     '営業区分(FC)

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)
    End Function

    ''' <summary>
    ''' 部署管理マスタから各支店の計画情報を取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <returns>各支店の計画情報</returns>
    ''' <remarks>部署管理マスタから各支店の計画情報を取得する</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Function SelBusyoKanriKeikakuKanriData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" SELECT MBK.busyo_cd, ")                       '部署コード
            .AppendLine("     MBK.busyo_mei, ")                         '部署名(支店名)
            .AppendLine("     TSKK.add_datetime, ")                     '登録日時
            .AppendLine("     TSKK.kakutei_flg, ")                      '確定FLG
            .AppendLine("     TSKK.eigyou_keikaku_kensuu, ")            '営業_計画調査件数
            .AppendLine("     TSKK.tokuhan_keikaku_kensuu, ")           '特販_計画調査件数
            .AppendLine("     TSKK.FC_keikaku_kensuu, ")                'FC_計画調査件数
            .AppendLine("     ISNULL(TSKK.Sum_keikaku_kensuu,0) AS Sum_keikaku_kensuu, ")           '合計調査件数

            .AppendLine("     TSKK.eigyou_keikaku_uri_kingaku, ")       '営業_計画売上金額
            .AppendLine("     TSKK.tokuhan_keikaku_uri_kingaku, ")      '特販_計画売上金額
            .AppendLine("     TSKK.FC_keikaku_uri_kingaku, ")           'FC_計画売上金額
            .AppendLine("     ISNULL(TSKK.Sum_keikaku_uri_kingaku,0) AS Sum_keikaku_uri_kingaku, ") '合計売上金額

            .AppendLine("     TSKK.eigyou_keikaku_arari, ")             '営業_計画粗利
            .AppendLine("     TSKK.tokuhan_keikaku_arari, ")            '特販_計画粗利
            .AppendLine("     TSKK.FC_keikaku_arari, ")                 'FC_計画粗利
            .AppendLine("     ISNULL(TSKK.Sum_keikaku_arari,0) AS Sum_keikaku_arari, ")              '合計粗利

            .AppendLine("     TJK.eigyou_jittuseki_kensuu, ")           '過去実績件数(営業)
            .AppendLine("     TJK.tokuhan_jittuseki_kensuu, ")          '過去実績件数(特販)
            .AppendLine("     TJK.FC_jittuseki_kensuu, ")               '過去実績件数(FC)
            .AppendLine("     (ISNULL(TJK.eigyou_jittuseki_kensuu,0) ")
            .AppendLine("     + ISNULL(TJK.tokuhan_jittuseki_kensuu,0)  ")
            .AppendLine("     + ISNULL(TJK.FC_jittuseki_kensuu,0)) AS Sum_jittuseki_kensuu, ")    '合計過去実績件数

            .AppendLine("     TJK.eigyou_jittuseki_kingaku, ")          '過去実績金額(営業)
            .AppendLine("     TJK.tokuhan_jittuseki_kingaku, ")         '過去実績金額(特販)
            .AppendLine("     TJK.FC_jittuseki_kingaku, ")              '過去実績金額(FC)
            .AppendLine("     (ISNULL(TJK.eigyou_jittuseki_kingaku,0) ")
            .AppendLine("     + ISNULL(TJK.tokuhan_jittuseki_kingaku,0) ")
            .AppendLine("     + ISNULL(TJK.FC_jittuseki_kingaku,0)) AS Sum_jittuseki_kingaku, ")  '合計過去実績金額
            .AppendLine("      ")
            .AppendLine("     TJK.eigyou_jittuseki_arari, ")            '過去実績粗利(営業)
            .AppendLine("     TJK.tokuhan_jittuseki_arari, ")           '過去実績粗利(特販)
            .AppendLine("     TJK.FC_jittuseki_arari, ")                '過去実績粗利(FC)
            .AppendLine("     (ISNULL(TJK.eigyou_jittuseki_arari,0) ")
            .AppendLine("     + ISNULL(TJK.tokuhan_jittuseki_arari,0) ")
            .AppendLine("     + ISNULL(TJK.FC_jittuseki_arari,0)) AS Sum_jittuseki_arari ")       '合計過去実績粗利
            .AppendLine(" FROM m_busyo_kanri AS MBK WITH(READCOMMITTED) ")
            .AppendLine(" LEFT OUTER JOIN ( ")
            .AppendLine("     SELECT ")
            .AppendLine("         busyo_cd, ")
            .AppendLine("         add_datetime, ")
            .AppendLine("         kakutei_flg, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,eigyou_keikaku_kensuu),'') AS eigyou_keikaku_kensuu, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,tokuhan_keikaku_kensuu),'') AS tokuhan_keikaku_kensuu, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,FC_keikaku_kensuu),'') AS FC_keikaku_kensuu, ")
            .AppendLine("         (ISNULL(eigyou_keikaku_kensuu,0) + ISNULL(tokuhan_keikaku_kensuu,0) + ISNULL(FC_keikaku_kensuu,0)) AS Sum_keikaku_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,eigyou_keikaku_uri_kingaku),'') AS eigyou_keikaku_uri_kingaku, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,tokuhan_keikaku_uri_kingaku),'') AS tokuhan_keikaku_uri_kingaku, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,FC_keikaku_uri_kingaku),'') AS FC_keikaku_uri_kingaku, ")
            .AppendLine("         (ISNULL(eigyou_keikaku_uri_kingaku,0) + ISNULL(tokuhan_keikaku_uri_kingaku,0) + ISNULL(FC_keikaku_uri_kingaku,0)) AS Sum_keikaku_uri_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,eigyou_keikaku_arari),'') AS eigyou_keikaku_arari, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,tokuhan_keikaku_arari),'') AS tokuhan_keikaku_arari, ")
            .AppendLine("         ISNULL(CONVERT(VARCHAR,FC_keikaku_arari),'') AS FC_keikaku_arari, ")
            .AppendLine("         (ISNULL(eigyou_keikaku_arari,0) + ISNULL(tokuhan_keikaku_arari,0) + ISNULL(FC_keikaku_arari,0)) AS Sum_keikaku_arari ")
            .AppendLine("     FROM ")
            .AppendLine("         t_sitenbetu_keikaku_kanri AS TKK WITH(READCOMMITTED) ")
            .AppendLine("     WHERE EXISTS ")
            .AppendLine("     ( ")
            .AppendLine("         SELECT busyo_cd, ")
            .AppendLine("                 MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                     + CONVERT(VARCHAR,add_datetime,121)) ")
            .AppendLine("         FROM  ")
            .AppendLine("             t_sitenbetu_keikaku_kanri AS SUBTKK WITH(READCOMMITTED) ")
            .AppendLine("         WHERE ")
            .AppendLine("             DATENAME(YYYY, keikaku_nendo) = @keikaku_nendo ")
            .AppendLine("         GROUP BY busyo_cd ")
            .AppendLine("         HAVING TKK.busyo_cd = SUBTKK.busyo_cd ")
            .AppendLine("         AND CASE WHEN ISNULL(CONVERT(VARCHAR,TKK.kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                     + CONVERT(VARCHAR,TKK.add_datetime,121)  ")
            .AppendLine("             = MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,SUBTKK.kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                     + CONVERT(VARCHAR,SUBTKK.add_datetime,121)) ")
            .AppendLine("     ) ")
            .AppendLine("     AND DATENAME(YYYY, TKK.keikaku_nendo) = @keikaku_nendo ")
            .AppendLine(" ) AS TSKK ")
            .AppendLine(" ON MBK.busyo_cd = TSKK.busyo_cd ")
            .AppendLine(" LEFT OUTER JOIN ( ")
            .AppendLine("     SELECT busyo_cd, ")
            .AppendLine("            SUM(eigyou_jittuseki_kensuu) AS eigyou_jittuseki_kensuu, ")
            .AppendLine("            SUM(tokuhan_jittuseki_kensuu) AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("            SUM(FC_jittuseki_kensuu) AS FC_jittuseki_kensuu, ")
            .AppendLine("            SUM(eigyou_jittuseki_kingaku) AS eigyou_jittuseki_kingaku, ")
            .AppendLine("            SUM(tokuhan_jittuseki_kingaku) AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("            SUM(FC_jittuseki_kingaku) AS FC_jittuseki_kingaku, ")
            .AppendLine("            SUM(eigyou_jittuseki_arari) AS eigyou_jittuseki_arari, ")
            .AppendLine("            SUM(tokuhan_jittuseki_arari) AS tokuhan_jittuseki_arari, ")
            .AppendLine("            SUM(FC_jittuseki_arari) AS FC_jittuseki_arari ")
            .AppendLine("     FROM ( ")
            '実績データを取得する
            .AppendLine("     SELECT MKK.busyo_cd, ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kensuu],0)) AS eigyou_jittuseki_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kingaku],0)) AS eigyou_jittuseki_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_arari],0)) AS eigyou_jittuseki_arari, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("         0 AS tokuhan_jittuseki_arari, ")
            .AppendLine("         0 AS FC_jittuseki_kensuu, ")
            .AppendLine("         0 AS FC_jittuseki_kingaku, ")
            .AppendLine("         0 AS FC_jittuseki_arari ")
            .AppendLine("     FROM m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("     INNER JOIN t_jisseki_kanri AS TJK WITH(READCOMMITTED) ")
            .AppendLine("     ON MKK.keikaku_nendo = TJK.keikaku_nendo ")
            .AppendLine("     AND MKK.kameiten_cd = TJK.kameiten_cd ")
            .AppendLine("     WHERE MKK.keikaku_nendo = @keikaku_nendo2 ")
            .AppendLine("     AND MKK.eigyou_kbn = @eigyou_kbn1 ")
            .AppendLine("     GROUP BY busyo_cd ")
            .AppendLine("     UNION ALL ")
            .AppendLine("     SELECT MKK.busyo_cd, ")
            .AppendLine("         0 AS eigyou_jittuseki_kensuu, ")
            .AppendLine("         0 AS eigyou_jittuseki_kingaku, ")
            .AppendLine("         0 AS eigyou_jittuseki_arari, ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kensuu],0)) AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kingaku],0)) AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_arari],0)) AS tokuhan_jittuseki_arari, ")
            .AppendLine("         0 AS FC_jittuseki_kensuu, ")
            .AppendLine("         0 AS FC_jittuseki_kingaku, ")
            .AppendLine("         0 AS FC_jittuseki_arari ")
            .AppendLine("     FROM m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("     INNER JOIN t_jisseki_kanri AS TJK WITH(READCOMMITTED) ")
            .AppendLine("     ON MKK.keikaku_nendo = TJK.keikaku_nendo ")
            .AppendLine("     AND MKK.kameiten_cd = TJK.kameiten_cd ")
            .AppendLine("     WHERE MKK.keikaku_nendo = @keikaku_nendo2 ")
            .AppendLine("     AND MKK.eigyou_kbn = @eigyou_kbn2 ")
            .AppendLine("     GROUP BY busyo_cd ")
            .AppendLine("     UNION ALL ")
            .AppendLine("     SELECT MKK.busyo_cd, ")
            .AppendLine("         0 AS eigyou_jittuseki_kensuu, ")
            .AppendLine("         0 AS eigyou_jittuseki_kingaku, ")
            .AppendLine("         0 AS eigyou_jittuseki_arari, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kensuu, ")
            .AppendLine("         0 AS tokuhan_jittuseki_kingaku, ")
            .AppendLine("         0 AS tokuhan_jittuseki_arari, ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kensuu],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kensuu],0)) AS FC_jittuseki_kensuu, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_kingaku],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_kingaku],0)) AS FC_jittuseki_kingaku, ")
            .AppendLine("          ")
            .AppendLine("         SUM(ISNULL([4gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([5gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([6gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([7gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([8gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([9gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([10gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([11gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([12gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([1gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([2gatu_jisseki_arari],0) +  ")
            .AppendLine("         ISNULL([3gatu_jisseki_arari],0)) AS FC_jittuseki_arari ")
            .AppendLine("     FROM m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("     INNER JOIN t_jisseki_kanri AS TJK WITH(READCOMMITTED) ")
            .AppendLine("     ON MKK.keikaku_nendo = TJK.keikaku_nendo ")
            .AppendLine("     AND MKK.kameiten_cd = TJK.kameiten_cd ")
            .AppendLine("     WHERE MKK.keikaku_nendo = @keikaku_nendo2 ")
            .AppendLine("     AND MKK.eigyou_kbn = @eigyou_kbn3 ")
            .AppendLine("     GROUP BY busyo_cd ")
            .AppendLine("     ) AS TJK   ")
            .AppendLine("     GROUP BY busyo_cd  ")
            .AppendLine(" ) AS TJK ")
            .AppendLine(" ON MBK.busyo_cd = TJK.busyo_cd ")
            .AppendLine(" LEFT JOIN ")
            .AppendLine(" m_keikakuyou_kakutyou_meisyou AS MKKM ")
            .AppendLine(" ON ")
            .AppendLine("   MKKM.meisyou_syubetu = '1' ")
            .AppendLine("   AND ")
            .AppendLine("   MBK.busyo_cd = MKKM.code")
            .AppendLine(" WHERE MBK.sosiki_level = @sosiki_level ")
            .AppendLine(" AND MBK.torikesi = @torikesi ")
            .AppendLine(" ORDER BY ")
            .AppendLine("     MKKM.hyouji_jyun ASC ")

        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))     '計画年度(YYYY)
        paramList.Add(MakeParam("@keikaku_nendo2", SqlDbType.Char, 4, Convert.ToInt32(strKeikakuNendo) - 1))     '前年年度(YYYY)
        paramList.Add(MakeParam("@eigyou_kbn1", SqlDbType.VarChar, 1, "1"))     '営業区分(営業)
        paramList.Add(MakeParam("@eigyou_kbn2", SqlDbType.VarChar, 1, "3"))     '営業区分(特販)
        paramList.Add(MakeParam("@eigyou_kbn3", SqlDbType.VarChar, 1, "4"))     '営業区分(FC)
        paramList.Add(MakeParam("@sosiki_level", SqlDbType.Int, 1, 4))          '組織レベル
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, 0))              '取消フラグ(0：有効、0以外：取消)

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)
    End Function

    ''' <summary>
    ''' 全社の最大登録日時のデータを取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <returns>全社の最大登録日時のデータ</returns>
    ''' <remarks>全社の最大登録日時のデータを取得する</remarks>
    Public Function SelMaxZensyaKeikakuKanriData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" SELECT MAX(add_datetime) AS add_datetime ")
            .AppendLine(" FROM t_zensya_keikaku_kanri WITH(READCOMMITTED) ")
            .AppendLine(" WHERE keikaku_nendo = @keikaku_nendo ")
            .AppendLine(" AND kakutei_flg = @kakutei_flg ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))     '計画年度(YYYY)
        paramList.Add(MakeParam("@kakutei_flg", SqlDbType.Int, 1, 1))     '確定FLG

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)

    End Function

    ''' <summary>
    ''' 支店の最大登録日時のデータを取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <returns>支店の最大登録日時のデータ</returns>
    ''' <remarks>支店の最大登録日時のデータを取得する</remarks>
    Public Function SelMaxSitenbetuKeikakuKanriData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" SELECT MAX(add_datetime) AS add_datetime ")
            .AppendLine(" FROM t_sitenbetu_keikaku_kanri WITH(READCOMMITTED) ")
            .AppendLine(" WHERE keikaku_nendo = @keikaku_nendo ")
            .AppendLine(" AND kakutei_flg = @kakutei_flg ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))     '計画年度(YYYY)
        paramList.Add(MakeParam("@kakutei_flg", SqlDbType.Int, 1, 1))     '確定FLG

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)

    End Function

    ''' <summary>
    ''' 全社計画管理テーブルに登録する
    ''' </summary>
    ''' <param name="hstValues">登録データ</param>
    ''' <remarks>全社計画管理テーブルに登録する</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Sub InsZensyaKeikakuKanriData(ByVal hstValues As Hashtable)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, hstValues)

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" INSERT INTO t_zensya_keikaku_kanri WITH(UPDLOCK) ( ")
            .AppendLine("     keikaku_nendo, ")
            .AppendLine("     add_datetime, ")
            .AppendLine("     keikaku_kensuu, ")
            .AppendLine("     keikaku_uri_kingaku, ")
            .AppendLine("     keikaku_arari, ")
            .AppendLine("     keikaku_henkou_flg, ")
            .AppendLine("     keikaku_settutei_kome, ")
            .AppendLine("     kakutei_flg, ")
            .AppendLine("     keikaku_huhen_flg, ")
            .AppendLine("     add_login_user_id ")
            .AppendLine(" )VALUES( ")
            .AppendLine("     @keikaku_nendo, ")
            .AppendLine("     GETDATE(), ")
            .AppendLine("     @keikaku_kensuu, ")
            .AppendLine("     @keikaku_uri_kingaku, ")
            .AppendLine("     @keikaku_arari, ")
            .AppendLine("     @keikaku_henkou_flg, ")
            .AppendLine("     @keikaku_settutei_kome, ")
            .AppendLine("     @kakutei_flg, ")
            .AppendLine("     @keikaku_huhen_flg, ")
            .AppendLine("     @add_login_user_id ")
            .AppendLine(" ) ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, hstValues("keikaku_nendo")))                '計画年度(YYYY)

        If Convert.ToString(hstValues("keikaku_kensuu")).Equals(String.Empty) Then
            paramList.Add(MakeParam("@keikaku_kensuu", SqlDbType.BigInt, 12, DBNull.Value))                         '計画調査件数
        Else
            paramList.Add(MakeParam("@keikaku_kensuu", SqlDbType.BigInt, 12, hstValues("keikaku_kensuu")))          '計画調査件数
        End If

        If Convert.ToString(hstValues("keikaku_uri_kingaku")).Equals(String.Empty) Then
            paramList.Add(MakeParam("@keikaku_uri_kingaku", SqlDbType.BigInt, 12, DBNull.Value))                    '計画売上金額
        Else
            paramList.Add(MakeParam("@keikaku_uri_kingaku", SqlDbType.BigInt, 12, hstValues("keikaku_uri_kingaku"))) '計画売上金額
        End If

        If Convert.ToString(hstValues("keikaku_arari")).Equals(String.Empty) Then
            paramList.Add(MakeParam("@keikaku_arari", SqlDbType.BigInt, 12, DBNull.Value))                          '計画粗利
        Else
            paramList.Add(MakeParam("@keikaku_arari", SqlDbType.BigInt, 12, hstValues("keikaku_arari")))            '計画粗利
        End If

        paramList.Add(MakeParam("@keikaku_henkou_flg", SqlDbType.Int, 1, hstValues("keikaku_henkou_flg")))          '計画変更FLG
        paramList.Add(MakeParam("@keikaku_settutei_kome", SqlDbType.VarChar, 80, hstValues("keikaku_settutei_kome"))) '計画設定時ｺﾒﾝﾄ
        paramList.Add(MakeParam("@kakutei_flg", SqlDbType.Int, 1, hstValues("kakutei_flg")))                        '確定FLG
        paramList.Add(MakeParam("@keikaku_huhen_flg", SqlDbType.Int, 1, hstValues("keikaku_huhen_flg")))            '計画値不変FLG
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, hstValues("add_login_user_id")))       '登録者ID

        SQLHelper.ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, paramList.ToArray())

    End Sub

    ''' <summary>
    ''' 支店別計画管理テーブルに登録する
    ''' </summary>
    ''' <param name="drValue">登録データ</param>
    ''' <remarks>支店別計画管理テーブルに登録する</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Sub InsSitenbetuKeikakuKanriData(ByVal drValue As DataRow)
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, drValue)

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" INSERT INTO t_sitenbetu_keikaku_kanri WITH(UPDLOCK) ( ")
            .AppendLine("     keikaku_nendo, ")
            .AppendLine("     siten_mei, ")
            .AppendLine("     add_datetime, ")
            .AppendLine("     busyo_cd, ")
            .AppendLine("     eigyou_keikaku_kensuu, ")
            .AppendLine("     tokuhan_keikaku_kensuu, ")
            .AppendLine("     FC_keikaku_kensuu, ")
            .AppendLine("     eigyou_keikaku_uri_kingaku, ")
            .AppendLine("     tokuhan_keikaku_uri_kingaku, ")
            .AppendLine("     FC_keikaku_uri_kingaku, ")
            .AppendLine("     eigyou_keikaku_arari, ")
            .AppendLine("     tokuhan_keikaku_arari, ")
            .AppendLine("     FC_keikaku_arari, ")
            .AppendLine("     keikaku_henkou_flg, ")
            .AppendLine("     keikaku_settutei_kome, ")
            .AppendLine("     kakutei_flg, ")
            .AppendLine("     keikaku_huhen_flg, ")
            .AppendLine("     add_login_user_id ")
            .AppendLine(" )VALUES( ")
            .AppendLine("     @keikaku_nendo, ")
            .AppendLine("     @siten_mei, ")
            .AppendLine("     @add_datetime, ")
            .AppendLine("     @busyo_cd, ")
            .AppendLine("     @eigyou_keikaku_kensuu, ")
            .AppendLine("     @tokuhan_keikaku_kensuu, ")
            .AppendLine("     @FC_keikaku_kensuu, ")
            .AppendLine("     @eigyou_keikaku_uri_kingaku, ")
            .AppendLine("     @tokuhan_keikaku_uri_kingaku, ")
            .AppendLine("     @FC_keikaku_uri_kingaku, ")
            .AppendLine("     @eigyou_keikaku_arari, ")
            .AppendLine("     @tokuhan_keikaku_arari, ")
            .AppendLine("     @FC_keikaku_arari, ")
            .AppendLine("     @keikaku_henkou_flg, ")
            .AppendLine("     @keikaku_settutei_kome, ")
            .AppendLine("     @kakutei_flg, ")
            .AppendLine("     @keikaku_huhen_flg, ")
            .AppendLine("     @add_login_user_id ")
            .AppendLine(" ) ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, drValue("keikaku_nendo")))                                 '計画年度(YYYY)
        paramList.Add(MakeParam("@siten_mei", SqlDbType.VarChar, 40, drValue("siten_mei")))                                     '支店名
        paramList.Add(MakeParam("@add_datetime", SqlDbType.DateTime, 20, drValue("add_datetime")))                              '登録日時
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, drValue("busyo_cd")))                                        '部署コード
        paramList.Add(MakeParam("@eigyou_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("eigyou_keikaku_kensuu")))              '営業_計画調査件数
        paramList.Add(MakeParam("@tokuhan_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("tokuhan_keikaku_kensuu")))            '特販_計画調査件数
        paramList.Add(MakeParam("@FC_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("FC_keikaku_kensuu")))                      'FC_計画調査件数
        paramList.Add(MakeParam("@eigyou_keikaku_uri_kingaku", SqlDbType.BigInt, 12, drValue("eigyou_keikaku_uri_kingaku")))    '営業_計画売上金額
        paramList.Add(MakeParam("@tokuhan_keikaku_uri_kingaku", SqlDbType.BigInt, 12, drValue("tokuhan_keikaku_uri_kingaku")))  '特販_計画売上金額
        paramList.Add(MakeParam("@FC_keikaku_uri_kingaku", SqlDbType.BigInt, 12, drValue("FC_keikaku_uri_kingaku")))            'FC_計画売上金額
        paramList.Add(MakeParam("@eigyou_keikaku_arari", SqlDbType.BigInt, 12, drValue("eigyou_keikaku_arari")))                '営業_計画粗利
        paramList.Add(MakeParam("@tokuhan_keikaku_arari", SqlDbType.BigInt, 12, drValue("tokuhan_keikaku_arari")))              '特販_計画粗利
        paramList.Add(MakeParam("@FC_keikaku_arari", SqlDbType.BigInt, 12, drValue("FC_keikaku_arari")))                        'FC_計画粗利
        paramList.Add(MakeParam("@keikaku_henkou_flg", SqlDbType.Int, 1, drValue("keikaku_henkou_flg")))                        '計画変更FLG
        paramList.Add(MakeParam("@keikaku_settutei_kome", SqlDbType.VarChar, 80, DBNull.Value))                                 '計画設定時ｺﾒﾝﾄ
        paramList.Add(MakeParam("@kakutei_flg", SqlDbType.Int, 1, drValue("kakutei_flg")))                                      '確定FLG
        paramList.Add(MakeParam("@keikaku_huhen_flg", SqlDbType.Int, 1, drValue("keikaku_huhen_flg")))                          '計画値不変FLG
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, drValue("add_login_user_id")))                     '登録者ID

        SQLHelper.ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, paramList.ToArray())

    End Sub
End Class
