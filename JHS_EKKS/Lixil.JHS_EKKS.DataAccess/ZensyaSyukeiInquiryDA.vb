Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text
Public Class ZensyaSyukeiInquiryDA


    '''' <summary>
    '''' データ有無
    '''' </summary>
    '''' <param name="strKeikakuNendo">計画_年度</param>
    '''' <returns>年度データ</returns>
    '''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    'Public Function SelNendoData(ByVal strKeikakuNendo As String) As Data.DataTable
    '    'EMAB障害対応情報の格納処理
    '    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)
    '    '戻りデータセット
    '    Dim dsReturn As New Data.DataSet

    '    'SQLコメント
    '    Dim commandTextSb As New StringBuilder

    '    'バラメタ格納
    '    Dim paramList As New List(Of SqlClient.SqlParameter)
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        .AppendLine("	* ")
    '        .AppendLine("FROM ")
    '        .AppendLine("	t_keikaku_kanri WITH (READCOMMITTED) ")
    '        .AppendLine("WHERE ")
    '        .AppendLine("keikaku_nendo=@strKeikakuNendo")

    '    End With
    '    'バラメタ
    '    paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '計画_年度
    '    '実行
    '    FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "nendoData", paramList.ToArray)

    '    '戻る値
    '    Return dsReturn.Tables("nendoData")
    'End Function

    ''' <summary>
    ''' 4月～3月の計画件数の集計値、計画金額の集計値、計画粗利の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画_年度</param>
    ''' <returns>集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function SelNendoKeikaku(ByVal strKeikakuNendo As String) As DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("       sum(sub.keikakuKensuuCount)")
            .AppendLine("      ,sum(sub.keikakuKingakuCount)")
            .AppendLine("      ,sum(sub.keikakuArariCount)")
            .AppendLine("      ,sum(rowsCount)")
            .AppendLine("FROM(")
            .AppendLine("SELECT ")
            .AppendLine("	(ISNULL(sum(TZKK.[1gatu_keikaku_kensuu]),0)+ISNULL(sum(TZKK.[2gatu_keikaku_kensuu]),0)+ISNULL(sum(TZKK.[3gatu_keikaku_kensuu]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[4gatu_keikaku_kensuu]),0)+ISNULL(sum(TZKK.[5gatu_keikaku_kensuu]),0)+ISNULL(sum(TZKK.[6gatu_keikaku_kensuu]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[7gatu_keikaku_kensuu]),0)+ISNULL(sum(TZKK.[8gatu_keikaku_kensuu]),0)+ISNULL(sum(TZKK.[9gatu_keikaku_kensuu]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[10gatu_keikaku_kensuu]),0)+ISNULL(sum([11gatu_keikaku_kensuu]),0)+ISNULL(sum(TZKK.[12gatu_keikaku_kensuu]),0))")
            .AppendLine("    as keikakuKensuuCount ")    '4月～3月計画件数の集計値
            .AppendLine("   ,(ISNULL(sum(TZKK.[1gatu_keikaku_kingaku]),0)+ISNULL(sum(TZKK.[2gatu_keikaku_kingaku]),0)+ISNULL(sum(TZKK.[3gatu_keikaku_kingaku]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[4gatu_keikaku_kingaku]),0)+ISNULL(sum(TZKK.[5gatu_keikaku_kingaku]),0)+ISNULL(sum(TZKK.[6gatu_keikaku_kingaku]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[7gatu_keikaku_kingaku]),0)+ISNULL(sum(TZKK.[8gatu_keikaku_kingaku]),0)+ISNULL(sum(TZKK.[9gatu_keikaku_kingaku]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[10gatu_keikaku_kingaku]),0)+ISNULL(sum(TZKK.[11gatu_keikaku_kingaku]),0)+ISNULL(sum([12gatu_keikaku_kingaku]),0))")
            .AppendLine("    as keikakuKingakuCount ")  '4月～3月計画金額の集計値
            .AppendLine("   ,(ISNULL(sum(TZKK.[1gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[2gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[3gatu_keikaku_arari]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[4gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[5gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[6gatu_keikaku_arari]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[7gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[8gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[9gatu_keikaku_arari]),0)")
            .AppendLine("   +ISNULL(sum(TZKK.[10gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[11gatu_keikaku_arari]),0)+ISNULL(sum(TZKK.[12gatu_keikaku_arari]),0))")
            .AppendLine("   as keikakuArariCount ")     '4月～3月計画粗利の集計値
            .AppendLine("  ,COUNT(*) AS rowsCount")
            .AppendLine("FROM ")
            .AppendLine("	   t_keikaku_kanri AS TZKK WITH (READCOMMITTED)   ")
            .AppendLine("      INNER JOIN   ")
            .AppendLine("      m_keikaku_kameiten AS MKK  WITH (READCOMMITTED) ")
            .AppendLine("      ON")
            .AppendLine("            TZKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("            AND")
            .AppendLine("            TZKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("      INNER JOIN")
            .AppendLine("      m_keikaku_kanri_syouhin AS MKKS  WITH (READCOMMITTED)")
            .AppendLine("      ON")
            .AppendLine("      TZKK.keikaku_nendo = MKKS.keikaku_nendo")
            .AppendLine("      AND")
            .AppendLine("      TZKK.keikaku_kanri_syouhin_cd = MKKS.keikaku_kanri_syouhin_cd")
            .AppendLine("WHERE TZKK.keikaku_nendo=@strKeikakuNendo")
            .AppendLine("      AND EXISTS(")
            .AppendLine("                      SELECT keikaku_nendo")
            .AppendLine("                      FROM  ")
            .AppendLine("                             t_keikaku_kanri AS SUB_TZKK WITH(READCOMMITTED) ")
            .AppendLine("                      WHERE")
            .AppendLine("                             SUB_TZKK.keikaku_nendo =TZKK.keikaku_nendo ")
            .AppendLine("                             AND   SUB_TZKK.kameiten_cd = TZKK.kameiten_cd")
            .AppendLine("                             AND   SUB_TZKK.keikaku_kanri_syouhin_cd = TZKK.keikaku_kanri_syouhin_cd")
            .AppendLine("                      GROUP BY")
            .AppendLine("                             keikaku_nendo,kameiten_cd,keikaku_kanri_syouhin_cd")
            .AppendLine("                      HAVING")
            .AppendLine("                             TZKK.keikaku_nendo = SUB_TZKK.keikaku_nendo")
            .AppendLine("                             AND    ")
            .AppendLine("                             SUB_TZKK.kameiten_cd = TZKK.kameiten_cd")
            .AppendLine("                             AND    ")
            .AppendLine("                             SUB_TZKK.keikaku_kanri_syouhin_cd = TZKK.keikaku_kanri_syouhin_cd")
            .AppendLine("                             AND ")
            .AppendLine("                             CASE WHEN ISNULL(CONVERT(VARCHAR,TZKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                                   + CONVERT(VARCHAR,TZKK.add_datetime,121) ")
            .AppendLine("                             = MAX")
            .AppendLine("                             (")
            .AppendLine("                              CASE WHEN ISNULL(CONVERT(VARCHAR,SUB_TZKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                                   + CONVERT(VARCHAR,SUB_TZKK.add_datetime,121)")
            .AppendLine("                             ) ")
            .AppendLine("                ) ")
            .AppendLine(" UNION ALL")
            .AppendLine("SELECT")
            .AppendLine("         (ISNULL(sum(TFKK.[1gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[2gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[3gatu_keikaku_kensuu]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[4gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[5gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[6gatu_keikaku_kensuu]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[7gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[8gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[9gatu_keikaku_kensuu]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[10gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[11gatu_keikaku_kensuu]),0)+ISNULL(sum(TFKK.[12gatu_keikaku_kensuu]),0))")
            .AppendLine("         as keikakuKensuuCount ")
            .AppendLine("         ,(ISNULL(sum(TFKK.[1gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[2gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[3gatu_keikaku_kingaku]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[4gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[5gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[6gatu_keikaku_kingaku]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[7gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[8gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[9gatu_keikaku_kingaku]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[10gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[11gatu_keikaku_kingaku]),0)+ISNULL(sum(TFKK.[12gatu_keikaku_kingaku]),0))")
            .AppendLine("         as keikakuKingakuCount ")
            .AppendLine("         , (ISNULL(sum(TFKK.[1gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[2gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[3gatu_keikaku_arari]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[4gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[5gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[6gatu_keikaku_arari]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[7gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[8gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[9gatu_keikaku_arari]),0)")
            .AppendLine("         +ISNULL(sum(TFKK.[10gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[11gatu_keikaku_arari]),0)+ISNULL(sum(TFKK.[12gatu_keikaku_arari]),0))")
            .AppendLine("         as keikakuArariCount ")
            .AppendLine("         ,COUNT(*) AS rowsCount")
            .AppendLine(" FROM t_fc_keikaku_kanri AS TFKK WITH(READCOMMITTED) ")
            .AppendLine(" INNER JOIN")
            .AppendLine("      m_keikaku_kanri_syouhin AS MKKS  WITH (READCOMMITTED)")
            .AppendLine("  ON")
            .AppendLine("      TFKK.keikaku_nendo = MKKS.keikaku_nendo")
            .AppendLine("  AND")
            .AppendLine("      TFKK.keikaku_kanri_syouhin_cd = MKKS.keikaku_kanri_syouhin_cd")
            .AppendLine(" WHERE EXISTS(  ")
            .AppendLine("     SELECT keikaku_nendo,  ")
            .AppendLine("            MAX(add_datetime) AS add_datetime,  ")
            .AppendLine("            busyo_cd,  ")
            .AppendLine("            keikaku_kanri_syouhin_cd  ")
            .AppendLine("     FROM t_fc_keikaku_kanri AS SUB_TFKK WITH(READCOMMITTED) ")
            .AppendLine("     WHERE keikaku_nendo = @strKeikakuNendo   ")
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
            .AppendLine(" AND TFKK.keikaku_nendo = @strKeikakuNendo ")
            .AppendLine(" GROUP BY TFKK.keikaku_kanri_syouhin_cd ) sub  ")

        End With

        'バラメタ
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '計画_年度
        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "nendoKeikaku", paramList.ToArray)

        Return dsReturn.Tables("nendoKeikaku")

    End Function

    '''' <summary>
    '''' データ有無
    '''' </summary>
    '''' <param name="strKeikakuNendo">年度</param>
    '''' <returns>年度データ</returns>
    '''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    'Public Function SelNendoKensuuData(ByVal strKeikakuNendo As String) As Data.DataTable
    '    'EMAB障害対応情報の格納処理
    '    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)
    '    '戻りデータセット
    '    Dim dsReturn As New Data.DataSet

    '    'SQLコメント
    '    Dim commandTextSb As New StringBuilder

    '    'バラメタ格納
    '    Dim paramList As New List(Of SqlClient.SqlParameter)
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        .AppendLine("	* ")
    '        .AppendLine("FROM ")
    '        .AppendLine("	t_jisseki_kanri WITH (READCOMMITTED) ")
    '        .AppendLine("WHERE ")
    '        .AppendLine("keikaku_nendo=@strKeikakuNendo")

    '    End With
    '    'バラメタ
    '    paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '計画_年度
    '    '実行
    '    FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "nendoKensuuData", paramList.ToArray)

    '    '戻る値
    '    Return dsReturn.Tables("nendoKensuuData")
    'End Function

    ''' <summary>
    ''' 選択年度に応じた年度の「実績件数」
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画_年度</param>
    ''' <returns>実績件数集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function SelNendoJissekiKensuu(ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	(ISNULL(SUM([1gatu_jisseki_kensuu]),0)+ISNULL(SUM([2gatu_jisseki_kensuu]),0)+ISNULL(SUM([3gatu_jisseki_kensuu]),0)  ")
            .AppendLine("      +ISNULL(SUM([4gatu_jisseki_kensuu]),0)+ISNULL(SUM([5gatu_jisseki_kensuu]),0)+ISNULL(SUM([6gatu_jisseki_kensuu]),0)")
            .AppendLine("    +ISNULL(SUM([7gatu_jisseki_kensuu]),0)+ISNULL(SUM([8gatu_jisseki_kensuu]),0)+ISNULL(SUM([9gatu_jisseki_kensuu]),0)")
            .AppendLine("      +ISNULL(SUM([10gatu_jisseki_kensuu]),0)+ISNULL(SUM([11gatu_jisseki_kensuu]),0)+ISNULL(SUM([12gatu_jisseki_kensuu]),0))")
            .AppendLine("   AS  keikakuKensuuCount ")     '4月～3月実績件数の集計値    
            .AppendLine("FROM ")
            .AppendLine("    t_jisseki_kanri  AS TJKK WITH (READCOMMITTED) ")
            .AppendLine("INNER JOIN")
            .AppendLine("    m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("    TJKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("AND")
            .AppendLine("    TJKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("INNER JOIN    m_keikaku_kanri_syouhin  MKKS WITH(READCOMMITTED)   ")
            .AppendLine("ON ")
            .AppendLine("    TJKK.keikaku_kanri_syouhin_cd=MKKS.keikaku_kanri_syouhin_cd ")
            .AppendLine("AND ")
            .AppendLine("    TJKK.keikaku_nendo=MKKS.keikaku_nendo")
            .AppendLine("WHERE ")
            .AppendLine("    TJKK.keikaku_nendo=@strKeikakuNendo ")
            .AppendLine("    AND")
            .AppendLine("    MKKS.bunbetu_cd='1' ")

        End With
        'バラメタ
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '計画_年度
        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "nendoJittusekiKensuu", paramList.ToArray)

        Return dsReturn.Tables("nendoJittusekiKensuu")
    End Function

    ''' <summary>
    ''' 実績金額、実績粗利の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画_年度</param>
    ''' <returns>実績金額、実績粗利の集計値のデータ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function SelNendoJisseki(ByVal strKeikakuNendo As String) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)
        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	(ISNULL(SUM([1gatu_jisseki_kingaku]),0)+ISNULL(SUM([2gatu_jisseki_kingaku]),0)+ISNULL(SUM([3gatu_jisseki_kingaku]),0)")
            .AppendLine("   +ISNULL(SUM([4gatu_jisseki_kingaku]),0)+ISNULL(SUM([5gatu_jisseki_kingaku]),0)+ISNULL(SUM([6gatu_jisseki_kingaku]),0)")
            .AppendLine("   +ISNULL(SUM([7gatu_jisseki_kingaku]),0)+ISNULL(SUM([8gatu_jisseki_kingaku]),0)+ISNULL(SUM([9gatu_jisseki_kingaku]),0)")
            .AppendLine("   +ISNULL(SUM([10gatu_jisseki_kingaku]),0)+ISNULL(SUM([11gatu_jisseki_kingaku]),0)+ISNULL(SUM([12gatu_jisseki_kingaku]),0))")
            .AppendLine("   AS jissekiKingakuCount ")  '4月～3月実績金額の集計値
            .AppendLine("   ,(ISNULL(SUM([1gatu_jisseki_arari]),0)+ISNULL(SUM([2gatu_jisseki_arari]),0)+ISNULL(SUM([3gatu_jisseki_arari]),0)")
            .AppendLine("   +ISNULL(SUM([4gatu_jisseki_arari]),0)+ISNULL(SUM([5gatu_jisseki_arari]),0)+ISNULL(SUM([6gatu_jisseki_arari]),0)")
            .AppendLine("   +ISNULL(SUM([7gatu_jisseki_arari]),0)+ISNULL(SUM([8gatu_jisseki_arari]),0)+ISNULL(SUM([9gatu_jisseki_arari]),0)")
            .AppendLine("   +ISNULL(SUM([10gatu_jisseki_arari]),0)+ISNULL(SUM([11gatu_jisseki_arari]),0)+ISNULL(SUM([12gatu_jisseki_arari]),0))")
            .AppendLine("   AS jissekiArariCount")     '4月～3月実績粗利の集計値
            .AppendLine("   ,COUNT(*) AS rowsCount")
            .AppendLine("FROM ")
            .AppendLine("    t_jisseki_kanri  AS TJKK WITH (READCOMMITTED) ")
            .AppendLine("INNER JOIN")
            .AppendLine("    m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("    TJKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("AND")
            .AppendLine("    TJKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("INNER JOIN    m_keikaku_kanri_syouhin  MKKS WITH(READCOMMITTED)   ")
            .AppendLine("ON ")
            .AppendLine("    TJKK.keikaku_kanri_syouhin_cd=MKKS.keikaku_kanri_syouhin_cd ")
            .AppendLine("AND ")
            .AppendLine("    TJKK.keikaku_nendo=MKKS.keikaku_nendo")
            .AppendLine("WHERE ")
            .AppendLine("    TJKK.keikaku_nendo=@strKeikakuNendo ")

        End With
        'バラメタ
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '計画_年度
        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "nendoJittuseki", paramList.ToArray)

        Return dsReturn.Tables("nendoJittuseki")
    End Function

    ''' <summary>
    ''' 期間計画件数の集計値、計画金額の集計値、計画粗利の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">年度</param>
    ''' <param name="strKikan">期間</param>
    ''' <returns>集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function SelKikanKeikaku(ByVal strKeikakuNendo As String, ByVal strKikan As String) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKikan)
        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("       sum(sub.keikakuKensuuCount)")
            .AppendLine("      ,sum(sub.keikakuKingakuCount)")
            .AppendLine("      ,sum(sub.keikakuArariCount)")
            .AppendLine("      ,sum(rowsCount)")

            .AppendLine("FROM(")
            .AppendLine("SELECT ")
            If strKikan = "上期" Then
                .AppendLine("ISNULL(SUM(TZKK.[4gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[5gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[6gatu_keikaku_kensuu]),0)") '計画件数
            End If
            If strKikan = "四半期(4,5,6月)" Then
                .AppendLine("ISNULL(SUM(TZKK.[4gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[5gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[6gatu_keikaku_kensuu]),0)") '計画件数
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "上期" Then
                .AppendLine("+")
            End If
            If strKikan = "上期" Then
                .AppendLine("ISNULL(SUM(TZKK.[7gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[8gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[9gatu_keikaku_kensuu]),0)")          '計画件数
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "四半期(7,8,9月)" Then
                .AppendLine("ISNULL(SUM(TZKK.[7gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[8gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[9gatu_keikaku_kensuu]),0)")          '計画件数
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "下期" Then
                .AppendLine("ISNULL(SUM(TZKK.[10gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[11gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[12gatu_keikaku_kensuu]),0)")       '計画件数
            End If
            If strKikan = "四半期(10,11,12月)" Then
                .AppendLine("ISNULL(SUM(TZKK.[10gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[11gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[12gatu_keikaku_kensuu]),0)")       '計画件数
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "下期" Then
                .AppendLine("+")
            End If
            If strKikan = "下期" Then
                .AppendLine("ISNULL(SUM(TZKK.[1gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[2gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[3gatu_keikaku_kensuu]),0)")          '計画件数
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "四半期(1,2,3月)" Then
                .AppendLine("ISNULL(SUM(TZKK.[1gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[2gatu_keikaku_kensuu]),0)+ISNULL(SUM(TZKK.[3gatu_keikaku_kensuu]),0)")          '計画件数
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "上期" Then
                .AppendLine(",ISNULL(SUM(TZKK.[4gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[5gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[6gatu_keikaku_kingaku]),0)")       '計画金額
            End If
            If strKikan = "四半期(4,5,6月)" Then
                .AppendLine(",ISNULL(SUM(TZKK.[4gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[5gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[6gatu_keikaku_kingaku]),0)")       '計画金額
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "上期" Then
                .AppendLine("+")
            End If
            If strKikan = "上期" Then
                .AppendLine("ISNULL(SUM(TZKK.[7gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[8gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[9gatu_keikaku_kingaku]),0)")       '計画金額
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "四半期(7,8,9月)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TZKK.[7gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[8gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[9gatu_keikaku_kingaku]),0)")       '計画金額
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "下期" Then
                .AppendLine(",ISNULL(SUM(TZKK.[10gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[11gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[12gatu_keikaku_kingaku]),0)")    '計画金額
            End If
            If strKikan = "四半期(10,11,12月)" Then
                .AppendLine(",ISNULL(SUM(TZKK.[10gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[11gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[12gatu_keikaku_kingaku]),0)")    '計画金額
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "下期" Then
                .AppendLine("+")
            End If
            If strKikan = "下期" Then
                .AppendLine("ISNULL(SUM(TZKK.[1gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[2gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[3gatu_keikaku_kingaku]),0)")       '計画金額
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "四半期(1,2,3月)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TZKK.[1gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[2gatu_keikaku_kingaku]),0)+ISNULL(SUM(TZKK.[3gatu_keikaku_kingaku]),0)")       '計画金額
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "上期" Then
                .AppendLine(",ISNULL(SUM(TZKK.[4gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[5gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[6gatu_keikaku_arari]),0)")             '計画粗利
            End If
            If strKikan = "四半期(4,5,6月)" Then
                .AppendLine(",ISNULL(SUM(TZKK.[4gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[5gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[6gatu_keikaku_arari]),0)")             '計画粗利
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "上期" Then
                .AppendLine(" +")
            End If
            If strKikan = "上期" Then
                .AppendLine("ISNULL(SUM(TZKK.[7gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[8gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[9gatu_keikaku_arari]),0) ")            '計画粗利
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "四半期(7,8,9月)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TZKK.[7gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[8gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[9gatu_keikaku_arari]),0) ")            '計画粗利
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "下期" Then
                .AppendLine(",ISNULL(SUM(TZKK.[10gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[11gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[12gatu_keikaku_arari]),0) ")         '計画粗利
            End If
            If strKikan = "四半期(10,11,12月)" Then
                .AppendLine(",ISNULL(SUM(TZKK.[10gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[11gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[12gatu_keikaku_arari]),0) ")         '計画粗利
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "下期" Then
                .AppendLine("+")
            End If
            If strKikan = "下期" Then
                .AppendLine("ISNULL(SUM(TZKK.[1gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[2gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[3gatu_keikaku_arari]),0)")             '計画粗利
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "四半期(1,2,3月)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TZKK.[1gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[2gatu_keikaku_arari]),0)+ISNULL(SUM(TZKK.[3gatu_keikaku_arari]),0)")             '計画粗利
                .AppendLine(" as keikakuArariCount")
            End If
            .AppendLine(",COUNT(*) AS rowsCount")
            .AppendLine("FROM ")
            .AppendLine("	   t_keikaku_kanri AS TZKK WITH (READCOMMITTED)   ")
            .AppendLine("      INNER JOIN   ")
            .AppendLine("      m_keikaku_kameiten AS MKK  WITH (READCOMMITTED) ")
            .AppendLine("      ON")
            .AppendLine("            TZKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("            AND")
            .AppendLine("            TZKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("      INNER JOIN")
            .AppendLine("      m_keikaku_kanri_syouhin AS MKKS  WITH (READCOMMITTED)")
            .AppendLine("      ON")
            .AppendLine("      TZKK.keikaku_nendo = MKKS.keikaku_nendo")
            .AppendLine("      AND")
            .AppendLine("      TZKK.keikaku_kanri_syouhin_cd = MKKS.keikaku_kanri_syouhin_cd")
            .AppendLine("WHERE TZKK.keikaku_nendo=@strKeikakuNendo")
            .AppendLine("      AND EXISTS(")
            .AppendLine("                      SELECT keikaku_nendo")
            .AppendLine("                      FROM  ")
            .AppendLine("                             t_keikaku_kanri AS SUB_TZKK WITH(READCOMMITTED) ")
            .AppendLine("                      WHERE")
            .AppendLine("                             SUB_TZKK.keikaku_nendo =TZKK.keikaku_nendo ")
            .AppendLine("                             AND   SUB_TZKK.kameiten_cd = TZKK.kameiten_cd")
            .AppendLine("                             AND   SUB_TZKK.keikaku_kanri_syouhin_cd = TZKK.keikaku_kanri_syouhin_cd")
            .AppendLine("                      GROUP BY")
            .AppendLine("                             keikaku_nendo,kameiten_cd,keikaku_kanri_syouhin_cd")
            .AppendLine("                      HAVING")
            .AppendLine("                             TZKK.keikaku_nendo = SUB_TZKK.keikaku_nendo")
            .AppendLine("                             AND    ")
            .AppendLine("                             SUB_TZKK.kameiten_cd = TZKK.kameiten_cd")
            .AppendLine("                             AND    ")
            .AppendLine("                             SUB_TZKK.keikaku_kanri_syouhin_cd = TZKK.keikaku_kanri_syouhin_cd")
            .AppendLine("                             AND ")
            .AppendLine("                             CASE WHEN ISNULL(CONVERT(VARCHAR,TZKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                                   + CONVERT(VARCHAR,TZKK.add_datetime,121) ")
            .AppendLine("                             = MAX")
            .AppendLine("                             (")
            .AppendLine("                              CASE WHEN ISNULL(CONVERT(VARCHAR,SUB_TZKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                                   + CONVERT(VARCHAR,SUB_TZKK.add_datetime,121)")
            .AppendLine("                             ) ")
            .AppendLine("                ) ")
            .AppendLine(" UNION ALL")
            .AppendLine("SELECT")
            If strKikan = "上期" Then
                .AppendLine("ISNULL(SUM(TFKK.[4gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[5gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[6gatu_keikaku_kensuu]),0)") '計画件数
            End If
            If strKikan = "四半期(4,5,6月)" Then
                .AppendLine("ISNULL(SUM(TFKK.[4gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[5gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[6gatu_keikaku_kensuu]),0)") '計画件数
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "上期" Then
                .AppendLine("+")
            End If
            If strKikan = "上期" Then
                .AppendLine("ISNULL(SUM(TFKK.[7gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[8gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[9gatu_keikaku_kensuu]),0)")          '計画件数
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "四半期(7,8,9月)" Then
                .AppendLine("ISNULL(SUM(TFKK.[7gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[8gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[9gatu_keikaku_kensuu]),0)")          '計画件数
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "下期" Then
                .AppendLine("ISNULL(SUM(TFKK.[10gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[11gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[12gatu_keikaku_kensuu]),0)")       '計画件数
            End If
            If strKikan = "四半期(10,11,12月)" Then
                .AppendLine("ISNULL(SUM(TFKK.[10gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[11gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[12gatu_keikaku_kensuu]),0)")       '計画件数
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "下期" Then
                .AppendLine("+")
            End If
            If strKikan = "下期" Then
                .AppendLine("ISNULL(SUM(TFKK.[1gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[2gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[3gatu_keikaku_kensuu]),0)")          '計画件数
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "四半期(1,2,3月)" Then
                .AppendLine("ISNULL(SUM(TFKK.[1gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[2gatu_keikaku_kensuu]),0)+ISNULL(SUM(TFKK.[3gatu_keikaku_kensuu]),0)")          '計画件数
                .AppendLine(" as keikakuKensuuCount")
            End If
            If strKikan = "上期" Then
                .AppendLine(",ISNULL(SUM(TFKK.[4gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[5gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[6gatu_keikaku_kingaku]),0)")       '計画金額
            End If
            If strKikan = "四半期(4,5,6月)" Then
                .AppendLine(",ISNULL(SUM(TFKK.[4gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[5gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[6gatu_keikaku_kingaku]),0)")       '計画金額
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "上期" Then
                .AppendLine("+")
            End If
            If strKikan = "上期" Then
                .AppendLine("ISNULL(SUM(TFKK.[7gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[8gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[9gatu_keikaku_kingaku]),0)")       '計画金額
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "四半期(7,8,9月)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TFKK.[7gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[8gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[9gatu_keikaku_kingaku]),0)")       '計画金額
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "下期" Then
                .AppendLine(",ISNULL(SUM(TFKK.[10gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[11gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[12gatu_keikaku_kingaku]),0)")    '計画金額
            End If
            If strKikan = "四半期(10,11,12月)" Then
                .AppendLine(",ISNULL(SUM(TFKK.[10gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[11gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[12gatu_keikaku_kingaku]),0)")    '計画金額
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "下期" Then
                .AppendLine("+")
            End If
            If strKikan = "下期" Then
                .AppendLine("ISNULL(SUM(TFKK.[1gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[2gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[3gatu_keikaku_kingaku]),0)")       '計画金額
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "四半期(1,2,3月)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TFKK.[1gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[2gatu_keikaku_kingaku]),0)+ISNULL(SUM(TFKK.[3gatu_keikaku_kingaku]),0)")       '計画金額
                .AppendLine("as keikakuKingakuCount")
            End If
            If strKikan = "上期" Then
                .AppendLine(",ISNULL(SUM(TFKK.[4gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[5gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[6gatu_keikaku_arari]),0)")             '計画粗利
            End If
            If strKikan = "四半期(4,5,6月)" Then
                .AppendLine(",ISNULL(SUM(TFKK.[4gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[5gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[6gatu_keikaku_arari]),0)")             '計画粗利
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "上期" Then
                .AppendLine(" +")
            End If
            If strKikan = "上期" Then
                .AppendLine("ISNULL(SUM(TFKK.[7gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[8gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[9gatu_keikaku_arari]),0) ")            '計画粗利
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "四半期(7,8,9月)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TFKK.[7gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[8gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[9gatu_keikaku_arari]),0) ")            '計画粗利
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "下期" Then
                .AppendLine(",ISNULL(SUM(TFKK.[10gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[11gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[12gatu_keikaku_arari]),0) ")         '計画粗利
            End If
            If strKikan = "四半期(10,11,12月)" Then
                .AppendLine(",ISNULL(SUM(TFKK.[10gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[11gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[12gatu_keikaku_arari]),0) ")         '計画粗利
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "下期" Then
                .AppendLine("+")
            End If
            If strKikan = "下期" Then
                .AppendLine("ISNULL(SUM(TFKK.[1gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[2gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[3gatu_keikaku_arari]),0)")             '計画粗利
                .AppendLine(" as keikakuArariCount")
            End If
            If strKikan = "四半期(1,2,3月)" Then
                .Append(",")
                .AppendLine("ISNULL(SUM(TFKK.[1gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[2gatu_keikaku_arari]),0)+ISNULL(SUM(TFKK.[3gatu_keikaku_arari]),0)")             '計画粗利
                .AppendLine(" as keikakuArariCount")
            End If
            .AppendLine(",COUNT(*) AS rowsCount")
            .AppendLine(" FROM t_fc_keikaku_kanri AS TFKK WITH(READCOMMITTED) ")
            .AppendLine(" INNER JOIN")
            .AppendLine("      m_keikaku_kanri_syouhin AS MKKS  WITH (READCOMMITTED)")
            .AppendLine("  ON")
            .AppendLine("      TFKK.keikaku_nendo = MKKS.keikaku_nendo")
            .AppendLine("  AND")
            .AppendLine("      TFKK.keikaku_kanri_syouhin_cd = MKKS.keikaku_kanri_syouhin_cd")
            .AppendLine(" WHERE EXISTS(  ")
            .AppendLine("     SELECT keikaku_nendo,  ")
            .AppendLine("            MAX(add_datetime) AS add_datetime,  ")
            .AppendLine("            busyo_cd,  ")
            .AppendLine("            keikaku_kanri_syouhin_cd  ")
            .AppendLine("     FROM t_fc_keikaku_kanri AS SUB_TFKK WITH(READCOMMITTED) ")
            .AppendLine("     WHERE keikaku_nendo = @strKeikakuNendo   ")
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
            .AppendLine(" AND TFKK.keikaku_nendo = @strKeikakuNendo ")
            .AppendLine(" GROUP BY TFKK.keikaku_kanri_syouhin_cd ) sub  ")
        End With
        'バラメタ
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '計画_年度

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "kikanKeikaku", paramList.ToArray)

        Return dsReturn.Tables("kikanKeikaku")
    End Function

    ''' <summary>
    ''' 期間実績件数の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">年度</param>
    ''' <param name="strKikan">期間</param>
    ''' <returns>集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function SelKikanJissekiKensuu(ByVal strKeikakuNendo As String, ByVal strKikan As String) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKikan)
        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("SELECT ")
            If strKikan = "上期" OrElse strKikan = "四半期(4,5,6月)" Then
                .AppendLine("ISNULL(SUM([4gatu_jisseki_kensuu]),0)+ISNULL(SUM([5gatu_jisseki_kensuu]),0)+ISNULL(SUM([6gatu_jisseki_kensuu]),0)	 ") '実績件数
            End If
            If strKikan = "上期" Then
                .AppendLine("+")
            End If
            If strKikan = "上期" OrElse strKikan = "四半期(7,8,9月)" Then
                .AppendLine("ISNULL(SUM([7gatu_jisseki_kensuu]),0)+ISNULL(SUM([8gatu_jisseki_kensuu]),0)+ISNULL(SUM([9gatu_jisseki_kensuu]),0)	 ") '実績件数
            End If
            If strKikan = "下期" OrElse strKikan = "四半期(10,11,12月)" Then
                .AppendLine("ISNULL(SUM([10gatu_jisseki_kensuu]),0)+ISNULL(SUM([11gatu_jisseki_kensuu]),0)+ISNULL(SUM([12gatu_jisseki_kensuu]),0)	 ") '実績件数
            End If
            If strKikan = "下期" Then
                .AppendLine("+")
            End If
            If strKikan = "下期" OrElse strKikan = "四半期(1,2,3月)" Then
                .AppendLine("ISNULL(SUM([1gatu_jisseki_kensuu]),0)+ISNULL(SUM([2gatu_jisseki_kensuu]),0)+ISNULL(SUM([3gatu_jisseki_kensuu]),0)	 ") '実績件数
            End If
            .AppendLine("FROM ")
            .AppendLine("    t_jisseki_kanri  AS TJKK WITH (READCOMMITTED) ")
            .AppendLine("INNER JOIN")
            .AppendLine("    m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("    TJKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("AND")
            .AppendLine("    TJKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("INNER JOIN    m_keikaku_kanri_syouhin  MKKS WITH(READCOMMITTED)   ")
            .AppendLine("ON ")
            .AppendLine("    TJKK.keikaku_kanri_syouhin_cd=MKKS.keikaku_kanri_syouhin_cd ")
            .AppendLine("AND ")
            .AppendLine("    TJKK.keikaku_nendo=MKKS.keikaku_nendo")
            .AppendLine("WHERE ")
            .AppendLine("    TJKK.keikaku_nendo=@strKeikakuNendo ")
            .AppendLine("    AND")
            .AppendLine("    MKKS.bunbetu_cd='1' ")
        End With
        'バラメタ
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '計画_年度

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "kikanJittusekiKensuu", paramList.ToArray)

        Return dsReturn.Tables("kikanJittusekiKensuu")
    End Function

    ''' <summary>
    ''' 期間実績金額、粗利の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">年度</param>
    ''' <param name="strKikan">期間</param>
    ''' <returns>集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function SelKikanJisseki(ByVal strKeikakuNendo As String, ByVal strKikan As String) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKikan)
        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("SELECT ")
            If strKikan = "上期" OrElse strKikan = "四半期(4,5,6月)" Then
                .AppendLine("ISNULL(SUM([4gatu_jisseki_kingaku]),0)+ISNULL(SUM([5gatu_jisseki_kingaku]),0)+ISNULL(SUM([6gatu_jisseki_kingaku]),0)	 ") '実績金額
            End If
            If strKikan = "上期" Then
                .AppendLine("+")
            End If
            If strKikan = "上期" OrElse strKikan = "四半期(7,8,9月)" Then
                .AppendLine("ISNULL(SUM([7gatu_jisseki_kingaku]),0)+ISNULL(SUM([8gatu_jisseki_kingaku]),0)+ISNULL(SUM([9gatu_jisseki_kingaku]),0) ") '実績金額
            End If
            If strKikan = "下期" OrElse strKikan = "四半期(10,11,12月)" Then
                .AppendLine("ISNULL(SUM([10gatu_jisseki_kingaku]),0)+ISNULL(SUM([11gatu_jisseki_kingaku]),0)+ISNULL(SUM([12gatu_jisseki_kingaku]),0)	 ") '実績金額
            End If
            If strKikan = "下期" Then
                .AppendLine("+")
            End If
            If strKikan = "下期" OrElse strKikan = "四半期(1,2,3月)" Then
                .AppendLine("ISNULL(SUM([1gatu_jisseki_kingaku]),0)+ISNULL(SUM([2gatu_jisseki_kingaku]),0)+ISNULL(SUM([3gatu_jisseki_kingaku]),0)") '実績金額
            End If
            If strKikan = "上期" OrElse strKikan = "四半期(4,5,6月)" Then
                .AppendLine(",ISNULL(SUM([4gatu_jisseki_arari]),0)+ISNULL(SUM([5gatu_jisseki_arari]),0)+ISNULL(SUM([6gatu_jisseki_arari]),0)	 ") '実績粗利
            End If
            If strKikan = "上期" Then
                .AppendLine("+")
            End If
            If strKikan = "上期" OrElse strKikan = "四半期(7,8,9月)" Then
                If strKikan = "四半期(7,8,9月)" Then
                    .Append(",")
                End If
                .AppendLine("ISNULL(SUM([7gatu_jisseki_arari]),0)+ISNULL(SUM([8gatu_jisseki_arari]),0)+ISNULL(SUM([9gatu_jisseki_arari]),0)  ") '実績粗利
            End If
            If strKikan = "下期" OrElse strKikan = "四半期(10,11,12月)" Then
                .AppendLine(",ISNULL(SUM([10gatu_jisseki_arari]),0)+ISNULL(SUM([11gatu_jisseki_arari]),0)+ISNULL(SUM([12gatu_jisseki_arari]),0)  	 ") '実績粗利
            End If
            If strKikan = "下期" Then
                .AppendLine("+")
            End If
            If strKikan = "下期" OrElse strKikan = "四半期(1,2,3月)" Then
                If strKikan = "四半期(1,2,3月)" Then
                    .Append(",")
                End If
                .AppendLine("ISNULL(SUM([1gatu_jisseki_arari]),0)+ISNULL(SUM([2gatu_jisseki_arari]),0)+ISNULL(SUM([3gatu_jisseki_arari]),0)") '実績粗利
            End If
            .AppendLine("         ,COUNT(*) AS rowsCount")
            .AppendLine("FROM ")
            .AppendLine("    t_jisseki_kanri  AS TJKK WITH (READCOMMITTED) ")
            .AppendLine("INNER JOIN")
            .AppendLine("    m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("    TJKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("AND")
            .AppendLine("    TJKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("INNER JOIN    m_keikaku_kanri_syouhin  MKKS WITH(READCOMMITTED)   ")
            .AppendLine("ON ")
            .AppendLine("    TJKK.keikaku_kanri_syouhin_cd=MKKS.keikaku_kanri_syouhin_cd ")
            .AppendLine("AND ")
            .AppendLine("    TJKK.keikaku_nendo=MKKS.keikaku_nendo")
            .AppendLine("WHERE ")
            .AppendLine("    TJKK.keikaku_nendo=@strKeikakuNendo ")

        End With
        'バラメタ
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '計画_年度

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "kikanJittuseki", paramList.ToArray)

        Return dsReturn.Tables("kikanJittuseki")
    End Function

    ''' <summary>
    ''' 計画件数、計画金額、計画粗利の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">年度</param>
    ''' <param name="intBegin">月から</param>
    ''' <param name="intEnd">月まで</param>
    ''' <returns>集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function SelTukiKeikaku(ByVal strKeikakuNendo As String, ByVal intBegin As Integer, ByVal intEnd As Integer) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, intBegin, intEnd)
        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("       sum(sub.keikakuKensuuCount)")
            .AppendLine("      ,sum(sub.keikakuKingakuCount)")
            .AppendLine("      ,sum(sub.keikakuArariCount)")
            .AppendLine("      ,sum(rowsCount)")
            .AppendLine("FROM(")
            .AppendLine("SELECT ")
            .AppendLine("    (")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(TZKK.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kensuu]),0)") '4月～5月計画件数の集計値
                Next
            Else
                .AppendLine("+ ISNULL(SUM(TZKK.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kensuu]),0)")
            End If

            .AppendLine(" ) AS keikakuKensuuCount")
            .AppendLine("    ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(TZKK.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kingaku]),0)") '4月～5月計画金額の集計値
                Next
            Else
                .AppendLine(" + ISNULL(SUM(TZKK.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kingaku]),0)")
            End If

            .AppendLine(" ) AS keikakuKingakuCount")
            .AppendLine("    ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(TZKK.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_arari]),0)") '4月～5月計画粗利の集計値
                Next
            Else
                .AppendLine(" + ISNULL(SUM(TZKK.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_arari]),0)")

            End If

            .AppendLine(" ) AS keikakuArariCount")
            .AppendLine("  ,COUNT(*) AS rowsCount")
            .AppendLine("FROM ")
            .AppendLine("	   t_keikaku_kanri AS TZKK WITH (READCOMMITTED)   ")
            .AppendLine("      INNER JOIN   ")
            .AppendLine("      m_keikaku_kameiten AS MKK  WITH (READCOMMITTED) ")
            .AppendLine("      ON")
            .AppendLine("            TZKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("            AND")
            .AppendLine("            TZKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("      INNER JOIN")
            .AppendLine("      m_keikaku_kanri_syouhin AS MKKS  WITH (READCOMMITTED)")
            .AppendLine("      ON")
            .AppendLine("      TZKK.keikaku_nendo = MKKS.keikaku_nendo")
            .AppendLine("      AND")
            .AppendLine("      TZKK.keikaku_kanri_syouhin_cd = MKKS.keikaku_kanri_syouhin_cd")
            .AppendLine("WHERE TZKK.keikaku_nendo=@strKeikakuNendo")
            .AppendLine("      AND EXISTS(")
            .AppendLine("                      SELECT keikaku_nendo")
            .AppendLine("                      FROM  ")
            .AppendLine("                             t_keikaku_kanri AS SUB_TZKK WITH(READCOMMITTED) ")
            .AppendLine("                      WHERE")
            .AppendLine("                             SUB_TZKK.keikaku_nendo =TZKK.keikaku_nendo ")
            .AppendLine("                             AND   SUB_TZKK.kameiten_cd = TZKK.kameiten_cd")
            .AppendLine("                             AND   SUB_TZKK.keikaku_kanri_syouhin_cd = TZKK.keikaku_kanri_syouhin_cd")
            .AppendLine("                      GROUP BY")
            .AppendLine("                             keikaku_nendo,kameiten_cd,keikaku_kanri_syouhin_cd")
            .AppendLine("                      HAVING")
            .AppendLine("                             TZKK.keikaku_nendo = SUB_TZKK.keikaku_nendo")
            .AppendLine("                             AND    ")
            .AppendLine("                             SUB_TZKK.kameiten_cd = TZKK.kameiten_cd")
            .AppendLine("                             AND    ")
            .AppendLine("                             SUB_TZKK.keikaku_kanri_syouhin_cd = TZKK.keikaku_kanri_syouhin_cd")
            .AppendLine("                             AND ")
            .AppendLine("                             CASE WHEN ISNULL(CONVERT(VARCHAR,TZKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                                   + CONVERT(VARCHAR,TZKK.add_datetime,121) ")
            .AppendLine("                             = MAX")
            .AppendLine("                             (")
            .AppendLine("                              CASE WHEN ISNULL(CONVERT(VARCHAR,SUB_TZKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("                                   + CONVERT(VARCHAR,SUB_TZKK.add_datetime,121)")
            .AppendLine("                             ) ")
            .AppendLine("                ) ")
            .AppendLine("UNION ALL")
            .AppendLine("SELECT ")
            .AppendLine("    (")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(TFKK.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kensuu]),0)") '4月～5月計画件数の集計値
                Next
            Else
                .AppendLine("+ ISNULL(SUM(TFKK.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kensuu]),0)")
            End If

            .AppendLine(" ) AS keikakuKensuuCount")
            .AppendLine("    ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(TFKK.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kingaku]),0)") '4月～5月計画金額の集計値
                Next
            Else
                .AppendLine(" + ISNULL(SUM(TFKK.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kingaku]),0)")
            End If

            .AppendLine(" ) AS keikakuKingakuCount")
            .AppendLine("    ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(TFKK.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_arari]),0)") '4月～5月計画粗利の集計値
                Next
            Else
                .AppendLine(" + ISNULL(SUM(TFKK.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_arari]),0)")

            End If
            .AppendLine(" ) AS keikakuArariCount")
            .AppendLine("  ,COUNT(*) AS rowsCount")
            .AppendLine(" FROM t_fc_keikaku_kanri AS TFKK WITH(READCOMMITTED) ")
            .AppendLine(" INNER JOIN")
            .AppendLine("      m_keikaku_kanri_syouhin AS MKKS  WITH (READCOMMITTED)")
            .AppendLine("  ON")
            .AppendLine("      TFKK.keikaku_nendo = MKKS.keikaku_nendo")
            .AppendLine("  AND")
            .AppendLine("      TFKK.keikaku_kanri_syouhin_cd = MKKS.keikaku_kanri_syouhin_cd")
            .AppendLine(" WHERE EXISTS(  ")
            .AppendLine("     SELECT keikaku_nendo,  ")
            .AppendLine("            MAX(add_datetime) AS add_datetime,  ")
            .AppendLine("            busyo_cd,  ")
            .AppendLine("            keikaku_kanri_syouhin_cd  ")
            .AppendLine("     FROM t_fc_keikaku_kanri AS SUB_TFKK WITH(READCOMMITTED) ")
            .AppendLine("     WHERE keikaku_nendo = @strKeikakuNendo   ")
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
            .AppendLine(" AND TFKK.keikaku_nendo = @strKeikakuNendo ")
            .AppendLine(" GROUP BY TFKK.keikaku_kanri_syouhin_cd ) sub  ")
        End With
        'バラメタ
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '計画_年度

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "tukiKeikaku", paramList.ToArray)

        Return dsReturn.Tables("tukiKeikaku")
    End Function

    ''' <summary>
    ''' 実績件数の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">年度</param>
    ''' <param name="intBegin">月から</param>
    ''' <param name="intEnd">月まで</param>
    ''' <returns>集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function SelTukiJissekiKensuu(ByVal strKeikakuNendo As String, ByVal intBegin As Integer, ByVal intEnd As Integer) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, intBegin, intEnd)
        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    (")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_jisseki_kensuu]),0)") '実績件数の集計値
                Next
            Else
                .AppendLine(" + ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_jisseki_kensuu]),0)") '実績件数の集計値
            End If
           
            .AppendLine(" ) AS jissekikensuuCount")

            .AppendLine("FROM ")
            .AppendLine("    t_jisseki_kanri  AS TJKK WITH (READCOMMITTED) ")
            .AppendLine("INNER JOIN")
            .AppendLine("    m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("    TJKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("AND")
            .AppendLine("    TJKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("INNER JOIN    m_keikaku_kanri_syouhin  MKKS WITH(READCOMMITTED)   ")
            .AppendLine("ON ")
            .AppendLine("    TJKK.keikaku_kanri_syouhin_cd=MKKS.keikaku_kanri_syouhin_cd ")
            .AppendLine("AND ")
            .AppendLine("    TJKK.keikaku_nendo=MKKS.keikaku_nendo")
            .AppendLine("WHERE ")
            .AppendLine("    TJKK.keikaku_nendo=@strKeikakuNendo ")
            .AppendLine("    AND")
            .AppendLine("    MKKS.bunbetu_cd='1' ")
        End With

        'バラメタ
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))    '計画_年度

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "tukiJissekiKensuu", paramList.ToArray)

        Return dsReturn.Tables("tukiJissekiKensuu")
    End Function

    ''' <summary>
    ''' 実績金額、実績粗利の集計値
    ''' </summary>
    ''' <param name="strKeikakuNendo">年度</param>
    ''' <param name="intBegin">月から</param>
    ''' <param name="intEnd">月まで</param>
    ''' <returns>集計値データ</returns>
    ''' <history>2012/11/30　趙冬雪(大連情報システム部)　新規作成</history>
    Public Function SelTukiJisseki(ByVal strKeikakuNendo As String, ByVal intBegin As Integer, ByVal intEnd As Integer) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, intBegin, intEnd)
        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    (")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_jisseki_kingaku]),0)") '実績金額の集計値
                Next
            Else
                .AppendLine("+ ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_jisseki_kingaku]),0)")
            End If
            .AppendLine(" ) AS jissekiKensuuCount")
            .AppendLine("    ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_jisseki_arari]),0)") '実績粗利の集計値
                Next
            Else
                .AppendLine("+ ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_jisseki_arari]),0)")
            End If
            
            .AppendLine(" ) AS keikakuArariCount")
            .AppendLine("         ,COUNT(*) AS rowsCount")
            .AppendLine("FROM ")
            .AppendLine("    t_jisseki_kanri  AS TJKK WITH (READCOMMITTED) ")
            .AppendLine("INNER JOIN")
            .AppendLine("    m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("    TJKK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("AND")
            .AppendLine("    TJKK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("INNER JOIN    m_keikaku_kanri_syouhin  MKKS WITH(READCOMMITTED)   ")
            .AppendLine("ON ")
            .AppendLine("    TJKK.keikaku_kanri_syouhin_cd=MKKS.keikaku_kanri_syouhin_cd ")
            .AppendLine("AND ")
            .AppendLine("    TJKK.keikaku_nendo=MKKS.keikaku_nendo")
            .AppendLine("WHERE ")
            .AppendLine("    TJKK.keikaku_nendo=@strKeikakuNendo ")

        End With

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'バラメタ
        paramList.Add(MakeParam("@strKeikakuNendo", SqlDbType.Char, 4, strKeikakuNendo))   '計画_年度

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "tukiJissekiTwo", paramList.ToArray)

        Return dsReturn.Tables("tukiJissekiTwo")
    End Function

End Class
