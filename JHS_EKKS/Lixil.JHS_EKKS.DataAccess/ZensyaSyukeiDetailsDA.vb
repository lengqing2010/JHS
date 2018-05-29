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
''' 全社集計詳細
''' </summary>
''' <remarks>全社集計詳細</remarks>
''' <history>
''' <para>2012/11/28 P-44979 王新 新規作成 </para>
''' </history>
Public Class ZensyaSyukeiDetailsDA

    ''' <summary>
    ''' 年度比率管理テーブルから工事比率を取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <returns>工事比率</returns>
    ''' <remarks>年度比率管理テーブルから工事比率を取得する</remarks>
    ''' <history>
    ''' <para>2012/11/28 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Function SelKakoJittusekiKanriData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" SELECT ISNULL(AVG(TNHK.koj_hantei_ritu),0) AS koj_hantei_ritu, ")
            .AppendLine("     ISNULL(AVG(TNHK.koj_jyuchuu_ritu),0) AS koj_jyuchuu_ritu, ")
            .AppendLine("     ISNULL(AVG(TNHK.tyoku_koj_ritu),0) AS tyoku_koj_ritu, ")
            .AppendLine("     MKK.eigyou_kbn ")
            .AppendLine(" FROM t_nendo_hiritu_kanri AS TNHK WITH(READCOMMITTED) ")
            .AppendLine(" INNER JOIN m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine(" ON TNHK.keikaku_nendo = MKK.keikaku_nendo ")
            .AppendLine(" AND TNHK.kameiten_cd = MKK.kameiten_cd ")
            .AppendLine(" WHERE TNHK.keikaku_nendo = @keikaku_nendo ")
            .AppendLine(" AND MKK.eigyou_kbn IN (@eigyou_kbn1,@eigyou_kbn2, ")
            .AppendLine("                        @eigyou_kbn3,@eigyou_kbn4) ")
            .AppendLine(" GROUP BY MKK.eigyou_kbn ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))  '計画年度(YYYY)
        paramList.Add(MakeParam("@eigyou_kbn1", SqlDbType.VarChar, 1, "1"))             '営業区分(営業)
        paramList.Add(MakeParam("@eigyou_kbn2", SqlDbType.VarChar, 1, "2"))             '営業区分(新規)
        paramList.Add(MakeParam("@eigyou_kbn3", SqlDbType.VarChar, 1, "3"))             '営業区分(特販)
        paramList.Add(MakeParam("@eigyou_kbn4", SqlDbType.VarChar, 1, "4"))             '営業区分(FC)

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)
    End Function

    ''' <summary>
    ''' 全社の詳細集計データを取得する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <param name="strEigyouKbn">営業区分</param>
    ''' <param name="arrList">月リスト</param>
    ''' <returns>詳細集計データ</returns>
    ''' <remarks>全社の詳細集計データを取得する</remarks>
    ''' <history>
    ''' <para>2012/11/28 P-44979 王新 新規作成 </para>
    ''' </history>
    Public Function SelSensyaSyuukeiData(ByVal strKeikakuNendo As String, _
                                         ByVal strEigyouKbn As String, _
                                         ByVal arrList As ArrayList) As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strEigyouKbn, arrList)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder
        Dim strInPara As String         '営業区分
        Dim i As Integer

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        strInPara = "(" & strEigyouKbn & ")"

        With sqlBuffer
            .AppendLine(" SELECT MKKS.keikaku_kanri_syouhin_cd AS syouhin_cd, ")
            .AppendLine("   MKKS.bunbetu_cd, ")
            .AppendLine("   MKKS.keikaku_kanri_syouhin_mei AS syouhin_mei, ")
            '前年実績データ
            .AppendLine("   ISNULL(CONVERT(VARCHAR,GTJK.g_jittuseki_kensuu),'') AS g_jittuseki_kensuu, ")
            .AppendLine("   ISNULL(CONVERT(VARCHAR,GTJK.g_jittuseki_kingaku),'') AS g_jittuseki_kingaku, ")
            .AppendLine("   ISNULL(CONVERT(VARCHAR,GTJK.g_jittuseki_arari),'') AS g_jittuseki_arari, ")
            '当年実績データ
            .AppendLine("   ISNULL(CONVERT(VARCHAR,TJK.zennen_heikin_tanka),'') AS zennen_heikin_tanka, ")
            .AppendLine("   ISNULL(CONVERT(VARCHAR,TJK.jittuseki_kensuu),'') AS jittuseki_kensuu, ")
            .AppendLine("   ISNULL(CONVERT(VARCHAR,TJK.jittuseki_kingaku),'') AS jittuseki_kingaku, ")
            .AppendLine("   ISNULL(CONVERT(VARCHAR,TJK.jittuseki_arari),'') AS jittuseki_arari, ")
            '当年計画データ
            .AppendLine("   ISNULL(CONVERT(VARCHAR,TKK.keikaku_kensuu),'') AS keikaku_kensuu, ")
            .AppendLine("   ISNULL(CONVERT(VARCHAR,TKK.keikaku_kingaku),'') AS keikaku_kingaku, ")
            .AppendLine("   ISNULL(CONVERT(VARCHAR,TKK.keikaku_arari),'') AS keikaku_arari, ")
            '当年見込データ
            .AppendLine("   ISNULL(CONVERT(VARCHAR,TYMK.mikomi_kensuu),'') AS mikomi_kensuu, ")
            .AppendLine("   ISNULL(CONVERT(VARCHAR,TYMK.mikomi_kingaku),'') AS mikomi_kingaku, ")
            .AppendLine("   ISNULL(CONVERT(VARCHAR,TYMK.mikomi_arari),'') AS mikomi_arari, ")
            '見込-計画
            .AppendLine("   ISNULL(CONVERT(VARCHAR,(TYMK.mikomi_kensuu - TKK.keikaku_kensuu)),'') AS mikomi_keikaku_kensuu, ")
            .AppendLine("   ISNULL(CONVERT(VARCHAR,(TYMK.mikomi_kingaku - TKK.keikaku_kingaku)),'') AS mikomi_keikaku_kingaku, ")
            .AppendLine("   ISNULL(CONVERT(VARCHAR,(TYMK.mikomi_arari - TKK.keikaku_arari)),'') AS mikomi_keikaku_arari, ")
            '計画達成率データ(実績/計画)
            .AppendLine("   CASE WHEN ISNULL(TKK.keikaku_kensuu,0) = 0 THEN 0 ELSE ")
            .AppendLine("   ROUND(ISNULL(TJK.jittuseki_kensuu,0) * 0.1 / ISNULL(TKK.keikaku_kensuu,1) * 1000,1) END AS tasseiritu_kensuu, ")
            .AppendLine("   CASE WHEN ISNULL(TKK.keikaku_kingaku,0) = 0 THEN 0 ELSE ")
            .AppendLine("   ROUND(ISNULL(TJK.jittuseki_kingaku,0) * 0.1 / ISNULL(TKK.keikaku_kingaku,1) * 1000,1) END AS tasseiritu_kingaku, ")
            .AppendLine("   CASE WHEN ISNULL(TKK.keikaku_arari,0) = 0 THEN 0 ELSE ")
            .AppendLine("   ROUND(ISNULL(TJK.jittuseki_arari,0) * 0.1 / ISNULL(TKK.keikaku_arari,1) * 1000,1) END AS tasseiritu_arari, ")
            '見込進捗率データ(実績/見込)
            .AppendLine("   CASE WHEN ISNULL(TYMK.mikomi_kensuu,0) = 0 THEN 0 ELSE ")
            .AppendLine("   ROUND(ISNULL(TJK.jittuseki_kensuu,0) * 0.1 / ISNULL(TYMK.mikomi_kensuu,1) * 1000,1) END AS sintyokuritu_kensuu, ")
            .AppendLine("   CASE WHEN ISNULL(TYMK.mikomi_kingaku,0) = 0 THEN 0 ELSE ")
            .AppendLine("   ROUND(ISNULL(TJK.jittuseki_kingaku,0) * 0.1 / ISNULL(TYMK.mikomi_kingaku,1) * 1000,1) END AS sintyokuritu_kingaku, ")
            .AppendLine("   CASE WHEN ISNULL(TYMK.mikomi_arari,0) = 0 THEN 0 ELSE ")
            .AppendLine("   ROUND(ISNULL(TJK.jittuseki_arari,0) * 0.1 / ISNULL(TYMK.mikomi_arari,1) * 1000,1) END AS sintyokuritu_arari ")
            .AppendLine(" FROM m_keikaku_kanri_syouhin AS MKKS WITH(READCOMMITTED) ")
            .AppendLine(" LEFT OUTER JOIN ( ")
            '当年実績データ
            .AppendLine("     SELECT TJK.keikaku_kanri_syouhin_cd, ")
            .AppendLine(" ISNULL(AVG(zennen_heikin_tanka),0) AS zennen_heikin_tanka, ")
            .AppendLine(" ISNULL(SUM( ")
            For i = 0 To arrList.Count - 1
                If i = arrList.Count - 1 Then
                    .AppendLine(" ISNULL(TJK.[" & Convert.ToString(arrList(i)) & "gatu_jisseki_kensuu],0) ")
                Else
                    .AppendLine(" ISNULL(TJK.[" & Convert.ToString(arrList(i)) & "gatu_jisseki_kensuu],0) + ")
                End If
            Next
            .AppendLine(" ),0) AS jittuseki_kensuu, ")
            .AppendLine(" ISNULL(SUM( ")
            For i = 0 To arrList.Count - 1
                If i = arrList.Count - 1 Then
                    .AppendLine(" ISNULL(TJK.[" & Convert.ToString(arrList(i)) & "gatu_jisseki_kingaku],0) ")
                Else
                    .AppendLine(" ISNULL(TJK.[" & Convert.ToString(arrList(i)) & "gatu_jisseki_kingaku],0) + ")
                End If
            Next
            .AppendLine(" ),0) AS jittuseki_kingaku, ")
            .AppendLine(" ISNULL(SUM( ")
            For i = 0 To arrList.Count - 1
                If i = arrList.Count - 1 Then
                    .AppendLine(" ISNULL(TJK.[" & Convert.ToString(arrList(i)) & "gatu_jisseki_arari],0) ")
                Else
                    .AppendLine(" ISNULL(TJK.[" & Convert.ToString(arrList(i)) & "gatu_jisseki_arari],0) + ")
                End If
            Next
            .AppendLine(" ),0) AS jittuseki_arari ")
            .AppendLine(" FROM t_jisseki_kanri AS TJK WITH(READCOMMITTED) ")
            .AppendLine(" INNER JOIN m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine(" ON TJK.keikaku_nendo = MKK.keikaku_nendo ")
            .AppendLine(" AND TJK.kameiten_cd = MKK.kameiten_cd ")
            .AppendLine(" WHERE TJK.keikaku_nendo = @keikaku_nendo ")
            '.AppendLine(" AND MKK.torikesi = @torikesi ")
            .AppendLine(" AND MKK.eigyou_kbn IN " & strInPara)
            .AppendLine(" GROUP BY TJK.keikaku_kanri_syouhin_cd ")
            .AppendLine(" ) AS TJK ")
            .AppendLine(" ON MKKS.keikaku_kanri_syouhin_cd = TJK.keikaku_kanri_syouhin_cd ")
            .AppendLine(" LEFT OUTER JOIN ( ")
            '前年実績データ
            .AppendLine(" SELECT TJK.keikaku_kanri_syouhin_cd, ")
            .AppendLine(" ISNULL(SUM( ")
            For i = 0 To arrList.Count - 1
                If i = arrList.Count - 1 Then
                    .AppendLine(" ISNULL(TJK.[" & Convert.ToString(arrList(i)) & "gatu_jisseki_kensuu],0) ")
                Else
                    .AppendLine(" ISNULL(TJK.[" & Convert.ToString(arrList(i)) & "gatu_jisseki_kensuu],0) + ")
                End If
            Next
            .AppendLine(" ),0) AS g_jittuseki_kensuu, ")
            .AppendLine(" ISNULL(SUM( ")
            For i = 0 To arrList.Count - 1
                If i = arrList.Count - 1 Then
                    .AppendLine(" ISNULL(TJK.[" & Convert.ToString(arrList(i)) & "gatu_jisseki_kingaku],0) ")
                Else
                    .AppendLine(" ISNULL(TJK.[" & Convert.ToString(arrList(i)) & "gatu_jisseki_kingaku],0) + ")
                End If
            Next
            .AppendLine(" ),0) AS g_jittuseki_kingaku, ")
            .AppendLine(" ISNULL(SUM( ")
            For i = 0 To arrList.Count - 1
                If i = arrList.Count - 1 Then
                    .AppendLine(" ISNULL(TJK.[" & Convert.ToString(arrList(i)) & "gatu_jisseki_arari],0) ")
                Else
                    .AppendLine(" ISNULL(TJK.[" & Convert.ToString(arrList(i)) & "gatu_jisseki_arari],0) + ")
                End If
            Next
            .AppendLine(" ),0) AS g_jittuseki_arari ")
            .AppendLine(" FROM t_jisseki_kanri AS TJK WITH(READCOMMITTED) ")
            .AppendLine(" INNER JOIN m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine(" ON TJK.keikaku_nendo = MKK.keikaku_nendo ")
            .AppendLine(" AND TJK.kameiten_cd = MKK.kameiten_cd ")
            .AppendLine(" WHERE TJK.keikaku_nendo = CONVERT(VARCHAR,CONVERT(INT,@keikaku_nendo) - 1) ")
            '.AppendLine(" AND MKK.torikesi = @torikesi ")
            .AppendLine(" AND MKK.eigyou_kbn IN " & strInPara)
            .AppendLine(" GROUP BY TJK.keikaku_kanri_syouhin_cd ")
            .AppendLine(" ) AS GTJK ")
            .AppendLine(" ON MKKS.keikaku_kanri_syouhin_cd = GTJK.keikaku_kanri_syouhin_cd ")
            .AppendLine(" LEFT OUTER JOIN ( ")
            '計画データ
            .AppendLine(" SELECT keikaku_kanri_syouhin_cd,")
            .AppendLine(" SUM(keikaku_kensuu) AS keikaku_kensuu,")
            .AppendLine(" SUM(keikaku_kingaku) AS keikaku_kingaku,")
            .AppendLine(" SUM(keikaku_arari) AS keikaku_arari")
            .AppendLine(" FROM (")
            .AppendLine("  SELECT TKK.keikaku_kanri_syouhin_cd, ")
            .AppendLine("  ISNULL(SUM( ")
            For i = 0 To arrList.Count - 1
                If i = arrList.Count - 1 Then
                    .AppendLine(" ISNULL(TKK.[" & Convert.ToString(arrList(i)) & "gatu_keikaku_kensuu],0) ")
                Else
                    .AppendLine(" ISNULL(TKK.[" & Convert.ToString(arrList(i)) & "gatu_keikaku_kensuu],0) + ")
                End If
            Next
            .AppendLine("  ),0) AS keikaku_kensuu, ")
            .AppendLine("  ISNULL(SUM( ")
            For i = 0 To arrList.Count - 1
                If i = arrList.Count - 1 Then
                    .AppendLine(" ISNULL(TKK.[" & Convert.ToString(arrList(i)) & "gatu_keikaku_kingaku],0) ")
                Else
                    .AppendLine(" ISNULL(TKK.[" & Convert.ToString(arrList(i)) & "gatu_keikaku_kingaku],0) + ")
                End If
            Next
            .AppendLine("  ),0) AS keikaku_kingaku, ")
            .AppendLine("  ISNULL(SUM( ")
            For i = 0 To arrList.Count - 1
                If i = arrList.Count - 1 Then
                    .AppendLine(" ISNULL(TKK.[" & Convert.ToString(arrList(i)) & "gatu_keikaku_arari],0) ")
                Else
                    .AppendLine(" ISNULL(TKK.[" & Convert.ToString(arrList(i)) & "gatu_keikaku_arari],0) + ")
                End If
            Next
            .AppendLine("  ),0) AS keikaku_arari ")
            .AppendLine("  FROM t_keikaku_kanri AS TKK WITH(READCOMMITTED) ")
            .AppendLine("  INNER JOIN m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("  ON TKK.keikaku_nendo = MKK.keikaku_nendo ")
            .AppendLine("  AND TKK.kameiten_cd = MKK.kameiten_cd ")
            .AppendLine("  WHERE EXISTS( ")
            .AppendLine("      SELECT keikaku_nendo, ")
            .AppendLine("             MAX(add_datetime) AS add_datetime, ")
            .AppendLine("             kameiten_cd, ")
            .AppendLine("             keikaku_kanri_syouhin_cd ")
            .AppendLine("      FROM t_keikaku_kanri AS SUB_TKK WITH(READCOMMITTED) ")
            .AppendLine("      WHERE keikaku_nendo = @keikaku_nendo ")
            .AppendLine("      GROUP BY keikaku_nendo, ")
            .AppendLine("               kameiten_cd, ")
            .AppendLine("               keikaku_kanri_syouhin_cd ")
            .AppendLine("      HAVING TKK.keikaku_nendo = SUB_TKK.keikaku_nendo ")
            .AppendLine("      AND TKK.kameiten_cd = SUB_TKK.kameiten_cd ")
            .AppendLine("      AND TKK.keikaku_kanri_syouhin_cd = SUB_TKK.keikaku_kanri_syouhin_cd ")
            .AppendLine("      AND CASE WHEN ISNULL(CONVERT(VARCHAR,TKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("          + CONVERT(VARCHAR,TKK.add_datetime,121) ")
            .AppendLine("          = MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,SUB_TKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("          + CONVERT(VARCHAR,SUB_TKK.add_datetime,121)) ")
            .AppendLine("      ) ")
            .AppendLine("  AND TKK.keikaku_nendo = @keikaku_nendo ")
            '.AppendLine("  AND MKK.torikesi = @torikesi ")
            .AppendLine("  AND MKK.eigyou_kbn IN " & strInPara)
            .AppendLine("  GROUP BY TKK.keikaku_kanri_syouhin_cd ")

            '営業区分の中に、「4:FC」がある場合
            If strEigyouKbn.IndexOf("4") >= 0 Then
                .AppendLine(" UNION ALL ")
                'FC用計画管理テーブル
                .AppendLine(" SELECT TFKK.keikaku_kanri_syouhin_cd, ")
                .AppendLine(" ISNULL(SUM( ")
                For i = 0 To arrList.Count - 1
                    If i = arrList.Count - 1 Then
                        .AppendLine(" ISNULL(TFKK.[" & Convert.ToString(arrList(i)) & "gatu_keikaku_kensuu],0) ")
                    Else
                        .AppendLine(" ISNULL(TFKK.[" & Convert.ToString(arrList(i)) & "gatu_keikaku_kensuu],0) + ")
                    End If
                Next
                .AppendLine("  ),0) AS keikaku_kensuu, ")
                .AppendLine("  ISNULL(SUM( ")
                For i = 0 To arrList.Count - 1
                    If i = arrList.Count - 1 Then
                        .AppendLine(" ISNULL(TFKK.[" & Convert.ToString(arrList(i)) & "gatu_keikaku_kingaku],0) ")
                    Else
                        .AppendLine(" ISNULL(TFKK.[" & Convert.ToString(arrList(i)) & "gatu_keikaku_kingaku],0) + ")
                    End If
                Next
                .AppendLine("  ),0) AS keikaku_kingaku, ")
                .AppendLine("  ISNULL(SUM( ")
                For i = 0 To arrList.Count - 1
                    If i = arrList.Count - 1 Then
                        .AppendLine(" ISNULL(TFKK.[" & Convert.ToString(arrList(i)) & "gatu_keikaku_arari],0) ")
                    Else
                        .AppendLine(" ISNULL(TFKK.[" & Convert.ToString(arrList(i)) & "gatu_keikaku_arari],0) + ")
                    End If
                Next
                .AppendLine(" ),0) AS keikaku_arari  ")
                .AppendLine(" FROM t_fc_keikaku_kanri AS TFKK WITH(READCOMMITTED) ")
                .AppendLine(" WHERE EXISTS(  ")
                .AppendLine("     SELECT keikaku_nendo,  ")
                .AppendLine("            MAX(add_datetime) AS add_datetime,  ")
                .AppendLine("            busyo_cd,  ")
                .AppendLine("            keikaku_kanri_syouhin_cd  ")
                .AppendLine("     FROM t_fc_keikaku_kanri AS SUB_TFKK WITH(READCOMMITTED) ")
                .AppendLine("     WHERE keikaku_nendo = @keikaku_nendo  ")
                .AppendLine("     GROUP BY keikaku_nendo,  ")
                .AppendLine("              busyo_cd,  ")
                .AppendLine("              keikaku_kanri_syouhin_cd  ")
                .AppendLine("     HAVING TFKK.keikaku_nendo = SUB_TFKK.keikaku_nendo  ")
                .AppendLine("     AND TFKK.busyo_cd = SUB_TFKK.busyo_cd  ")
                .AppendLine("     AND TFKK.keikaku_kanri_syouhin_cd = SUB_TFKK.keikaku_kanri_syouhin_cd  ")
                .AppendLine("     AND CASE WHEN ISNULL(CONVERT(VARCHAR,TFKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END  ")
                .AppendLine("         + CONVERT(VARCHAR,TFKK.add_datetime,121)  ")
                .AppendLine("         = MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,SUB_TFKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
                .AppendLine("         + CONVERT(VARCHAR,SUB_TFKK.add_datetime,121))  ")
                .AppendLine("     )  ")
                .AppendLine(" AND TFKK.keikaku_nendo = @keikaku_nendo ")
                .AppendLine(" GROUP BY TFKK.keikaku_kanri_syouhin_cd  ")
            End If
            .AppendLine("     ) AS SUB_TKK ")
            .AppendLine("     GROUP BY keikaku_kanri_syouhin_cd ")
            .AppendLine(" ) AS TKK ")
            .AppendLine(" ON MKKS.keikaku_kanri_syouhin_cd = TKK.keikaku_kanri_syouhin_cd ")
            .AppendLine("    ")
            .AppendLine(" LEFT OUTER JOIN ( ")
            '見込データ
            .AppendLine(" SELECT keikaku_kanri_syouhin_cd, ")
            .AppendLine("  SUM(mikomi_kensuu) AS mikomi_kensuu, ")
            .AppendLine("  SUM(mikomi_kingaku) AS mikomi_kingaku, ")
            .AppendLine("  SUM(mikomi_arari) AS mikomi_arari ")
            .AppendLine(" FROM ( ")
            .AppendLine("  SELECT TYMK.keikaku_kanri_syouhin_cd, ")
            .AppendLine("         ISNULL(SUM( ")
            For i = 0 To arrList.Count - 1
                If i = arrList.Count - 1 Then
                    .AppendLine(" ISNULL(TYMK.[" & Convert.ToString(arrList(i)) & "gatu_mikomi_kensuu],0) ")
                Else
                    .AppendLine(" ISNULL(TYMK.[" & Convert.ToString(arrList(i)) & "gatu_mikomi_kensuu],0) + ")
                End If
            Next
            .AppendLine("  ),0) AS mikomi_kensuu, ")
            .AppendLine("  ISNULL(SUM( ")
            For i = 0 To arrList.Count - 1
                If i = arrList.Count - 1 Then
                    .AppendLine(" ISNULL(TYMK.[" & Convert.ToString(arrList(i)) & "gatu_mikomi_kingaku],0) ")
                Else
                    .AppendLine(" ISNULL(TYMK.[" & Convert.ToString(arrList(i)) & "gatu_mikomi_kingaku],0) + ")
                End If
            Next
            .AppendLine("  ),0) AS mikomi_kingaku, ")
            .AppendLine("  ISNULL(SUM( ")
            For i = 0 To arrList.Count - 1
                If i = arrList.Count - 1 Then
                    .AppendLine(" ISNULL(TYMK.[" & Convert.ToString(arrList(i)) & "gatu_mikomi_arari],0) ")
                Else
                    .AppendLine(" ISNULL(TYMK.[" & Convert.ToString(arrList(i)) & "gatu_mikomi_arari],0) + ")
                End If
            Next
            .AppendLine("   ),0) AS mikomi_arari ")
            .AppendLine("  FROM t_yotei_mikomi_kanri AS TYMK WITH(READCOMMITTED) ")
            .AppendLine("  INNER JOIN ( ")
            .AppendLine("      SELECT keikaku_nendo, ")
            .AppendLine("             MAX(add_datetime) AS add_datetime, ")
            .AppendLine("             kameiten_cd, ")
            .AppendLine("             keikaku_kanri_syouhin_cd ")
            .AppendLine("      FROM t_yotei_mikomi_kanri AS TYMK WITH(READCOMMITTED) ")
            .AppendLine("      WHERE keikaku_nendo = @keikaku_nendo ")
            .AppendLine("      GROUP BY keikaku_nendo, ")
            .AppendLine("               kameiten_cd, ")
            .AppendLine("               keikaku_kanri_syouhin_cd ")
            .AppendLine("  ) AS SUB_TYMK ")
            .AppendLine("  ON TYMK.keikaku_nendo = SUB_TYMK.keikaku_nendo ")
            .AppendLine("  AND TYMK.kameiten_cd = SUB_TYMK.kameiten_cd ")
            .AppendLine("  AND TYMK.add_datetime = SUB_TYMK.add_datetime ")
            .AppendLine("  AND TYMK.keikaku_kanri_syouhin_cd = SUB_TYMK.keikaku_kanri_syouhin_cd ")
            .AppendLine("  INNER JOIN m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("  ON TYMK.keikaku_nendo = MKK.keikaku_nendo ")
            .AppendLine("  AND TYMK.kameiten_cd = MKK.kameiten_cd ")
            .AppendLine("  WHERE TYMK.keikaku_nendo = @keikaku_nendo ")
            '.AppendLine("  AND MKK.torikesi = @torikesi ")
            .AppendLine("  AND MKK.eigyou_kbn IN " & strInPara)
            .AppendLine("  GROUP BY TYMK.keikaku_kanri_syouhin_cd ")

            '営業区分の中に、「4:FC」がある場合
            If strEigyouKbn.IndexOf("4") >= 0 Then
                .AppendLine(" UNION ALL ")
                'FC用予定見込管理テーブル
                .AppendLine(" SELECT TFYMK.keikaku_kanri_syouhin_cd,  ")
                .AppendLine(" ISNULL(SUM(  ")
                For i = 0 To arrList.Count - 1
                    If i = arrList.Count - 1 Then
                        .AppendLine(" ISNULL(TFYMK.[" & Convert.ToString(arrList(i)) & "gatu_mikomi_kensuu],0) ")
                    Else
                        .AppendLine(" ISNULL(TFYMK.[" & Convert.ToString(arrList(i)) & "gatu_mikomi_kensuu],0) + ")
                    End If
                Next
                .AppendLine("  ),0) AS mikomi_kensuu, ")
                .AppendLine("  ISNULL(SUM( ")
                For i = 0 To arrList.Count - 1
                    If i = arrList.Count - 1 Then
                        .AppendLine(" ISNULL(TFYMK.[" & Convert.ToString(arrList(i)) & "gatu_mikomi_kingaku],0) ")
                    Else
                        .AppendLine(" ISNULL(TFYMK.[" & Convert.ToString(arrList(i)) & "gatu_mikomi_kingaku],0) + ")
                    End If
                Next
                .AppendLine("  ),0) AS mikomi_kingaku, ")
                .AppendLine("  ISNULL(SUM( ")
                For i = 0 To arrList.Count - 1
                    If i = arrList.Count - 1 Then
                        .AppendLine(" ISNULL(TFYMK.[" & Convert.ToString(arrList(i)) & "gatu_mikomi_arari],0) ")
                    Else
                        .AppendLine(" ISNULL(TFYMK.[" & Convert.ToString(arrList(i)) & "gatu_mikomi_arari],0) + ")
                    End If
                Next
                .AppendLine(" ),0) AS mikomi_arari ")
                .AppendLine(" FROM t_fc_yotei_mikomi_kanri AS TFYMK WITH(READCOMMITTED) ")
                .AppendLine(" INNER JOIN ( ")
                .AppendLine("     SELECT keikaku_nendo, ")
                .AppendLine("            MAX(add_datetime) AS add_datetime, ")
                .AppendLine("            busyo_cd, ")
                .AppendLine("            keikaku_kanri_syouhin_cd ")
                .AppendLine("     FROM t_fc_yotei_mikomi_kanri AS TFYMK WITH(READCOMMITTED) ")
                .AppendLine("     WHERE keikaku_nendo = @keikaku_nendo ")
                .AppendLine("     GROUP BY keikaku_nendo, ")
                .AppendLine("              busyo_cd, ")
                .AppendLine("              keikaku_kanri_syouhin_cd ")
                .AppendLine(" ) AS SUB_TFYMK ")
                .AppendLine(" ON TFYMK.keikaku_nendo = SUB_TFYMK.keikaku_nendo ")
                .AppendLine(" AND TFYMK.busyo_cd = SUB_TFYMK.busyo_cd ")
                .AppendLine(" AND TFYMK.add_datetime = SUB_TFYMK.add_datetime ")
                .AppendLine(" AND TFYMK.keikaku_kanri_syouhin_cd = SUB_TFYMK.keikaku_kanri_syouhin_cd ")
                .AppendLine(" WHERE TFYMK.keikaku_nendo = @keikaku_nendo ")
                .AppendLine(" GROUP BY TFYMK.keikaku_kanri_syouhin_cd ")
            End If
            .AppendLine("  ) AS SUB_TYMK ")
            .AppendLine("  GROUP BY keikaku_kanri_syouhin_cd ")
            .AppendLine(" ) AS TYMK ")
            .AppendLine(" ON MKKS.keikaku_kanri_syouhin_cd = TYMK.keikaku_kanri_syouhin_cd ")
            .AppendLine(" WHERE MKKS.keikaku_nendo = @keikaku_nendo ")
            '.AppendLine(" AND MKKS.torikesi = @torikesi ")
            .AppendLine(" AND MKKS.bunbetu_cd IN ('1','2','9') ")               '分別コード(1:調査、2:工事、9:その他)
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))  '計画年度(YYYY)
        'paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, 0))                      '取消

        '*「Queueエラーが発生する場合、SQL文の空白を適当に削除してください」----
        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo", paramList.ToArray())

        Return dsInfo.Tables(0)
    End Function
End Class
