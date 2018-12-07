Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

Public Class SeikyusyoFcwOutputDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 請求書帳票出力データ検索する
    ''' </summary>
    ''' <param name="strSeikyusyo_no">請求先コード</param>
    ''' <returns>請求書帳票出力データ</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19　孫微微(大連)　新規作成
    ''' </history>
    Public Function SelSeikyusyoFcwOutputData(ByVal strSeikyusyo_no As String) As Data.DataTable

        ' DataSetインスタンスの生成()
        Dim dsRetrun As New Data.DataSet
        dsRetrun.Tables.Add()

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT    tk.seikyuu_saki_cd + '-' + tk.seikyuu_saki_brc + ")
            '2011/02/14 「請求先ｺｰﾄﾞ」の【   T】印字の判定変更 付龍追加 ↓
            '.AppendLine("       CASE WHEN LEFT(km.hannyou_cd,1) = '1' THEN '   T' ELSE '' END AS seikyuu --請求先コード-請求先枝番")
            .AppendLine("       CASE WHEN LEFT(tk.kaisyuu_seikyuusyo_yousi_hannyou_cd,1) = '1' THEN '   T' ELSE '' END AS seikyuu --請求先コード-請求先枝番")
            '2011/02/14 「請求先ｺｰﾄﾞ」の【   T】印字の判定変更 付龍追加 ↑
            .AppendLine("             ,tk.seikyuusyo_hak_date                        --請求書発行日")
            .AppendLine("             ,tk.yuubin_no                                  --郵便番号")
            .AppendLine("             ,tk.jyuusyo1                                   --住所1")
            .AppendLine("             ,tk.jyuusyo2                                   --住所2")
            .AppendLine("             ,tk.seikyuu_saki_mei                           --請求先名")
            .AppendLine("             ,tk.seikyuu_saki_mei2                          --請求先名2")
            .AppendLine("             ,isnull(tk.tantousya_mei,'御担当者') ")
            .AppendLine("                                         AS tantousya_mei   --担当者名")
            '===============2011/05/31 405693_EARTH経理２次要望対応 車龍 修正 開始↓===========================
            ''2010/10/22 汎用売上テーブル対象とする 付龍追加 ↓
            ''.AppendLine("             ,ISNULL(tu.kbn,'') + ISNULL(tu.bangou,'') as bukken_no --物件番号")
            '.AppendLine("             ,CASE ")
            '.AppendLine("	            WHEN TU.himoduke_table_type =  '1' ") '--売上データテーブル.紐付けテーブルタイプ=1の時")
            '.AppendLine("	            THEN ISNULL(TU.kbn,'') +ISNULL(TU.bangou,'') ") '--売上データテーブル.区分＋番号")
            '.AppendLine("	            WHEN TU.himoduke_table_type =  '9' ") '--売上データテーブル.紐付けテーブルタイプ=9の時")
            '.AppendLine("	            THEN ISNULL(THU.kbn,'') +ISNULL(THU.bangou,'') ") '--汎用売上テーブル.区分＋番号")
            '.AppendLine("	            ELSE '' ")
            '.AppendLine("	            END AS bukken_no ") '--物件番号")
            ''2010/10/22 汎用売上テーブル対象とする 付龍追加 ↑
            ''2010/10/22 汎用売上テーブル対象とする 付龍追加 ↓
            ''.AppendLine("             ,CASE WHEN tk.seikyuusyo_inji_bukken_mei_flg = '0' THEN")
            ''.AppendLine("                      tj.sesyu_mei                          --施主名")
            ''.AppendLine("                     WHEN tk.seikyuusyo_inji_bukken_mei_flg = '1' THEN")
            ''.AppendLine("                      tj.jutyuu_bukken_mei                  --受注物件名")
            ''.AppendLine("              END AS bukken_mei")
            '.AppendLine("              ,CASE ")
            '.AppendLine("	            WHEN TU.himoduke_table_type =  '1' ") '--売上データテーブル.紐付けテーブルタイプ=1の時")
            '.AppendLine("	            THEN ")
            '.AppendLine("                   CASE WHEN tk.seikyuusyo_inji_bukken_mei_flg = '0' THEN")
            '.AppendLine("                            tj.sesyu_mei                          --施主名")
            '.AppendLine("                         WHEN tk.seikyuusyo_inji_bukken_mei_flg = '1' THEN")
            '.AppendLine("                            tj.jutyuu_bukken_mei                  --受注物件名")
            '.AppendLine("                    END ")
            '.AppendLine("	            WHEN TU.himoduke_table_type =  '9' ") '--売上データテーブル.紐付けテーブルタイプ=9の時")
            '.AppendLine("	            THEN THU.sesyu_mei ") '--汎用売上テーブル.施主名")
            '.AppendLine("	            ELSE '' ")
            '.AppendLine("	            END AS bukken_mei ") '--摘要名(施主名)")
            ''2010/10/22 汎用売上テーブル対象とする 付龍追加 ↑
            .AppendLine("               ,ISNULL(tu.kbn,'') + ISNULL(tu.bangou,'') as bukken_no ") '--物件番号(売上データテーブル.区分＋番号)
            .AppendLine("               ,CASE ")
            .AppendLine("                   WHEN tj.kbn IS NOT NULL THEN ") '--地盤テーブルが存在する時
            .AppendLine("                       CASE ")
            .AppendLine("                           WHEN tk.seikyuusyo_inji_bukken_mei_flg = '0' THEN")
            .AppendLine("                               tj.sesyu_mei                          --施主名")
            .AppendLine("                           WHEN tk.seikyuusyo_inji_bukken_mei_flg = '1' THEN")
            .AppendLine("                               tj.jutyuu_bukken_mei                  --受注物件名")
            .AppendLine("                           END ")
            .AppendLine("                   WHEN TU.himoduke_table_type =  '9' ") '--売上データテーブル.紐付けテーブルタイプ=9の時")
            .AppendLine("                       AND tj.kbn IS NULL ")   '--かつ 地盤テーブルが存在しない時
            .AppendLine("                   THEN THU.sesyu_mei ") '--汎用売上テーブル.施主名")
            .AppendLine("                   ELSE '' ")
            .AppendLine("                   END AS bukken_mei ") '--摘要名(施主名)")
            '===============2011/05/31 405693_EARTH経理２次要望対応 車龍 修正 終了↑===========================
            .AppendLine("             ,tk.konkai_kaisyuu_yotei_date                  --入金予定日")
            .AppendLine("             ,LEFT(ISNULL(tk.kaisyuu_seikyuusyo_yousi,''),1) AS furikomi_flg --振込先表示内容フラグ")
            .AppendLine("             ,SUBSTRING(ISNULL(tk.kaisyuu_seikyuusyo_yousi,''),2,1) AS yousi_flg --用紙フラグ")
            .AppendLine("             ,CASE WHEN LEFT(tk.kaisyuu_seikyuusyo_yousi,1) = '1' THEN")
            .AppendLine("                     nyuukin_kouza_no                       --入金口座番号")
            .AppendLine("              END nyuukin_kouza_no")
            .AppendLine("             ,tk.kurikosi_gaku                              --前回繰越残高")
            .AppendLine("             ,tk.gonyuukin_gaku                             --御入金額")
            .AppendLine("             ,tk.sousai_gaku                                --相殺")
            .AppendLine("             ,tk.tyousei_gaku                               --調整額")
            .AppendLine("             ,tk.konkai_goseikyuu_gaku                      --今回御請求額")
            .AppendLine("             ,tk.konkai_kurikosi_gaku                       --繰越残高")
            .AppendLine("             ,tu.himoduke_cd                                --紐付けコード")
            .AppendLine("             ,tu.himoduke_table_type                        --紐付け元テーブル種別")
            .AppendLine("             ,tu.kbn  AS uri_kbn                            --(売上)区分")
            .AppendLine("             ,tu.bangou  AS uri_bangou                      --(売上)番号")
            .AppendLine("             ,tu.seikyuu_saki_cd  AS ten_cd                 --(請求先)店コード")
            '.AppendLine("             ,tu.seikyuu_saki_mei  AS ten_mei               --(請求先)店名")
            .AppendLine("             ,ISNULL(tu.seikyuu_saki_mei,'')  AS ten_mei    --(請求先)店名")
            .AppendLine("             ,tu.denpyou_uri_date                           --売上年月日")
            .AppendLine("             ,tu.syouhin_cd                                 --商品cd")
       
            .AppendLine("             ,tu.hinmei                                     --商品名")
            .AppendLine(" ,CASE ")
            .AppendLine(" WHEN km1.code+km2.code IS NULL THEN ")
            .AppendLine("            tu.hinmei ")
            .AppendLine(" ELSE ")
            .AppendLine(" RTRIM(tu.hinmei)  +'['+m_tyousahouhou.tys_houhou_mei_ryaku+']' ")
            .AppendLine(" END AS hinmei--商品名 ")

            ''2018/12/05 李松涛 JHS0003_Earth請求書帳票の項目 追加 依頼担当者名↓
            '.AppendLine(" ,CASE ")
            '.AppendLine("       WHEN km1.code+km2.code IS NULL THEN ")
            '.AppendLine("            tu.hinmei ")
            '.AppendLine("  ELSE ")
            '.AppendLine("       RTRIM(tu.hinmei)  +'['+m_tyousahouhou.tys_houhou_mei_ryaku+']' ")
            '.AppendLine("  END + ' ' + tj.irai_tantousya_mei + ' 様' AS hinmei --商品名 ")
            ''2018/12/05 李松涛 JHS0003_Earth請求書帳票の項目追加 依頼担当者名↑


            .AppendLine("             ,tu.suu                                        --数量")
            .AppendLine("             ,tu.tanka                                      --単価")
            .AppendLine("             ,tu.uri_gaku                                   --税別金額")
            .AppendLine("             ,tu.sotozei_gaku                               --外税額")
            .AppendLine("             ,ISNULL(tu.uri_gaku,0) + ISNULL(tu.sotozei_gaku,0) as zeikomi_gaku --税込金額")
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
            .AppendLine("             ,ISNULL(tk.ginkou_siten_mei,'') + '(' + ISNULL(tk.ginkou_siten_cd,'') + ')' as ginkou_siten_cd  --銀行支店コード")
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑
            '===============↓2014/02/06 407662_消費税増税対応_Earth 車龍 追加 開始↓===========================
            .AppendLine(",MSZ.zeiritu ") '--税率
            '===============↑2014/02/06 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================

            '2018/12/05 李松涛 JHS0003_Earth請求書帳票の項目追加 ↓
            '依頼担当者名
            .AppendLine(",ISNULL(tj.irai_tantousya_mei,'') as irai_tantousya_mei  --依頼担当者名")
            '契約No
            .AppendLine(",ISNULL(tj.keiyaku_no,'') as keiyaku_no  --契約No")

            .AppendLine(",SUBSTRING(ISNULL(tk.kaisyuu_seikyuusyo_yousi,''),3,1) AS koumoku_hyouji_flg --項目表示フラグ")
            '2018/12/05 李松涛 JHS0003_Earth請求書帳票の項目追加 ↑

            .AppendLine("")
            .AppendLine("FROM      t_seikyuu_kagami AS tk                            --請求鑑テーブル")
            .AppendLine("LEFT JOIN  t_seikyuu_meisai AS tm                           --請求明細テーブル")
            .AppendLine("ON         tk.seikyuusyo_no = tm.seikyuusyo_no              --請求書NO")
            .AppendLine("AND        tm.inji_taisyo_flg = '1'")
            .AppendLine("LEFT JOIN  t_uriage_data      AS tu                         --売上データテーブル")
            .AppendLine("ON         tm.denpyou_unique_no = tu.denpyou_unique_no      --伝票ユニークNO")
            .AppendLine("LEFT JOIN  t_jiban               AS tj                      --地盤テーブル")
            .AppendLine("ON         tj.kbn = tu.kbn                                  --区分")
            .AppendLine("AND        tj.hosyousyo_no = tu.bangou                      --地盤テーブル.保証書NO=売上データテーブル.番号")
            '2010/10/22 汎用売上テーブル対象とする 付龍追加 ↓
            .AppendLine("LEFT JOIN  t_hannyou_uriage      AS THU                     --汎用売上テーブル")
            .AppendLine("ON         TU.himoduke_cd = CONVERT(VARCHAR,THU.han_uri_unique_no) --汎用売上ユニークNO")
            '2010/10/22 汎用売上テーブル対象とする 付龍追加 ↑
            .AppendLine("LEFT JOIN  m_kakutyou_meisyou    AS km                      --拡張名称マスタ")
            .AppendLine("ON         km.meisyou_syubetu = '3'                         --区分")
            .AppendLine("AND        km.code = tk.kaisyuu_seikyuusyo_yousi            --コード")
            .AppendLine(" LEFT JOIN m_tyousahouhou ")
            .AppendLine(" ON tj.tys_houhou = m_tyousahouhou.tys_houhou_no ")
            .AppendLine(" LEFT JOIN m_kakutyou_meisyou AS km1 ")
            .AppendLine(" ON tu.syouhin_cd = km1.code ")
            .AppendLine(" AND km1.meisyou_syubetu = 26 ")
            .AppendLine(" LEFT JOIN m_kakutyou_meisyou AS km2 ")
            .AppendLine(" ON tj.tys_houhou = km2.code ")
            .AppendLine(" AND km2.meisyou_syubetu = 27 ")
            '===============↓2014/02/06 407662_消費税増税対応_Earth 車龍 追加 開始↓===========================
            .AppendLine("LEFT JOIN  ")
            .AppendLine("m_syouhizei AS MSZ ") '--消費税マスタ
            .AppendLine("   ON ") '--税区分
            .AppendLine("       MSZ.zei_kbn = tu.zei_kbn ") '--税区分
            '===============↑2014/02/06 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================
            .AppendLine("WHERE    tk.seikyuusyo_no =  '" & strSeikyusyo_no & "'")
            '===============↓2014/02/06 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
            '.AppendLine("ORDER BY tk.seikyuusyo_no,tu.kbn + tu.bangou,tu.syouhin_cd,tu.denpyou_uri_date")
            .AppendLine("ORDER BY tk.seikyuusyo_no,tu.kbn + tu.bangou,tu.syouhin_cd,tu.denpyou_uri_date,MSZ.zeiritu")
            '===============↑2014/02/06 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsRetrun, _
                    dsRetrun.Tables(0).TableName, paramList.ToArray)

        Return dsRetrun.Tables(0)

    End Function

    ''' <summary>
    ''' 請求先書Noデータを取得する
    ''' </summary>
    ''' <param name="strSeikyusyo_no">請求先書No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19　孫微微(大連)　新規作成
    ''' </history>
    Public Function SelSeikyusyoNoData(ByVal strSeikyusyo_no As String) As Data.DataTable

        'DataSetインスタンスの生成()
        Dim dsRetrun As New Data.DataSet
        dsRetrun.Tables.Add()

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  DISTINCT  tk.seikyuusyo_no    --請求先書No")
            .AppendLine("FROM      t_seikyuu_kagami AS tk      --請求鑑テーブル")
            .AppendLine("WHERE    tk.seikyuusyo_no IN (" & strSeikyusyo_no & ")")
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsRetrun, _
                    dsRetrun.Tables(0).TableName, paramList.ToArray)

        Return dsRetrun.Tables(0)

    End Function

    ''' <summary>
    ''' ログインユーザー情報を取得する。
    ''' </summary>
    ''' <param name="loginID">ログインユーザーコード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19　楚建偉(大連)　新規作成
    ''' </history>
    Public Function SelLoginUserName(ByVal loginID As String) As String

        'DataSetインスタンスの生成()
        Dim dsRetrun As New Data.DataSet
        dsRetrun.Tables.Add()

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT DisplayName ")
            .AppendLine("FROM m_jhs_mailbox ")
            .AppendLine("WHERE DirectoryName = '" & loginID & "'")
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsRetrun, _
                    dsRetrun.Tables(0).TableName, paramList.ToArray)

        If dsRetrun.Tables(0).Rows.Count = 0 Then
            Return "[NO DATA FOUND]"
        Else
            Return dsRetrun.Tables(0).Rows(0).Item("DisplayName").ToString
        End If

    End Function

    ''' <summary>
    ''' 種類一の店名を取得する
    ''' </summary>
    ''' <param name="UA_kbn">区分</param>
    ''' <param name="UA_bangou">番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19　楚建偉(大連)　新規作成
    ''' </history>
    Public Function SelHimodekeSyurui_1_tenmei(ByVal UA_kbn As String, ByVal UA_bangou As String) _
    As Data.DataTable

        'DataSetインスタンスの生成()
        Dim dsRetrun As New Data.DataSet
        dsRetrun.Tables.Add()

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT KM.kameiten_cd AS ten_cd,KM.kameiten_seisiki_mei AS ten_mei ")
            .AppendLine("FROM t_jiban AS JB ")
            .AppendLine("LEFT JOIN m_kameiten AS KM ")
            .AppendLine("ON JB.kameiten_cd = KM.kameiten_cd")
            .AppendLine("WHERE JB.kbn = '" & UA_kbn & "'")
            .AppendLine("AND JB.hosyousyo_no = '" & UA_bangou & "'")
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsRetrun, _
                    dsRetrun.Tables(0).TableName, paramList.ToArray)

        Return dsRetrun.Tables(0)

    End Function

    ''' <summary>
    ''' 種類二の店名を取得する
    ''' </summary>
    ''' <param name="UA_himodukecd_ten_cd">店コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19　楚建偉(大連)　新規作成
    ''' </history>
    Public Function SelHimodekeSyurui_2_tenmei(ByVal UA_himodukecd_ten_cd As String) _
    As Data.DataTable

        'DataSetインスタンスの生成()
        Dim dsRetrun As New Data.DataSet
        dsRetrun.Tables.Add()

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT KM.kameiten_cd AS ten_cd,KM.kameiten_seisiki_mei AS ten_mei ")
            .AppendLine("FROM t_tenbetu_seikyuu AS TBSK ")
            .AppendLine("LEFT JOIN m_kameiten AS KM ")
            .AppendLine("ON TBSK.mise_cd = KM.kameiten_cd")
            .AppendLine("WHERE TBSK.mise_cd = '" & UA_himodukecd_ten_cd & "'")
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsRetrun, _
                    dsRetrun.Tables(0).TableName, paramList.ToArray)


        If dsRetrun.Tables(0).Rows.Count > 0 Then
            Return dsRetrun.Tables(0)
        End If

        'SQL文
        commandTextSb = New StringBuilder

        With commandTextSb
            .AppendLine("SELECT ME.eigyousyo_cd AS ten_cd,ME.eigyousyo_mei AS ten_mei ")
            .AppendLine("FROM t_tenbetu_seikyuu AS TBSK ")
            .AppendLine("LEFT JOIN m_eigyousyo AS ME ")
            .AppendLine("ON TBSK.mise_cd = ME.eigyousyo_cd")
            .AppendLine("WHERE TBSK.mise_cd = '" & UA_himodukecd_ten_cd & "'")
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsRetrun, _
                    dsRetrun.Tables(0).TableName, paramList.ToArray)

        Return dsRetrun.Tables(0)

    End Function

    ''' <summary>
    ''' 種類三の店名を取得する
    ''' </summary>
    ''' <param name="UA_himodukecd_ten_cd">店コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19　楚建偉(大連)　新規作成
    ''' </history>
    Public Function SelHimodekeSyurui_3_tenmei(ByVal UA_himodukecd_ten_cd As String) _
   As Data.DataTable

        'DataSetインスタンスの生成()
        Dim dsRetrun As New Data.DataSet
        dsRetrun.Tables.Add()

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT KM.kameiten_cd AS ten_cd,KM.kameiten_seisiki_mei AS ten_mei ")
            .AppendLine("FROM t_tenbetu_syoki_seikyuu AS TBSK ")
            .AppendLine("LEFT JOIN m_kameiten AS KM ")
            .AppendLine("ON TBSK.mise_cd = KM.kameiten_cd")
            .AppendLine("WHERE TBSK.mise_cd = '" & UA_himodukecd_ten_cd & "'")
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsRetrun, _
                    dsRetrun.Tables(0).TableName, paramList.ToArray)

        Return dsRetrun.Tables(0)

    End Function

End Class
