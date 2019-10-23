Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 商品ごとの入金処理に関する処理を行うデータアクセスクラスです
''' </summary>
''' <remarks></remarks>
Public Class NyuukinSyoriDataAccess
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
#Region "メンバ変数"
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    Private strLogic As New StringLogic
    Private cmnDtAcc As New CmnDataAccess
#End Region
#Region "Dictionary用定数"
    'Public ReadOnly KEIRETU_CD As String = "KeiretuCd"
    'Public ReadOnly BUNRUI_WHERE As String = "BunruiCdWhere"
    Public ReadOnly SEIKYUU_FROM As String = "SeikyuuFrom"
    Public ReadOnly SEIKYUU_TO As String = "SeikyuuTo"
#End Region
#Region "定数"
    Private Const TEMP_TABLE As String = "##TEMP_WORK"
    Private Const TEMP_CSV_TABLE As String = "##TEMP_CSV_DATA"
    Private Const TEMP_SUM_TABLE As String = "##TEMP_SUMMARY"
#End Region

#Region "ページロード処理"
    ''' <summary>
    ''' 前回の入金データ取込情報を取得します
    ''' </summary>
    ''' <param name="intFileKbn">入金ファイル区分（デフォルト：1）</param>    ''' <returns>前回入金データ取込情報を格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetZenkaiTorikomiData(Optional ByVal intFileKbn As Integer = 0) As DataTable
        Dim zenkaiTorikomiTable As DataTable
        Dim uploadKanriDataSet As New UploadKanriDataSet
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        '前回入金データ取込情報取得用SQL設定
        cmdTextSb.Append(" SELECT TOP 1 ")
        cmdTextSb.Append("        syori_datetime ")
        cmdTextSb.Append("       ,edi_jouhou_sakusei_date")
        cmdTextSb.Append("       ,nyuuryoku_file_mei ")
        cmdTextSb.Append("       ,error_umu ")
        cmdTextSb.Append("   FROM ")
        cmdTextSb.Append("        t_upload_kanri ")
        cmdTextSb.Append("  WHERE ")
        If intFileKbn = 0 Then
            cmdTextSb.Append("        file_kbn IS NULL ")
        Else
            cmdTextSb.Append("        file_kbn = @FILEKBN ")
        End If
        cmdTextSb.Append("  ORDER BY ")
        cmdTextSb.Append("        syori_datetime DESC ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@FILEKBN", SqlDbType.Int, 4, intFileKbn)}

        ' 検索実行
        zenkaiTorikomiTable = cmnDtAcc.getDataTable(cmdTextSb.ToString _
                                                    , uploadKanriDataSet _
                                                    , uploadKanriDataSet.UploadJouhou.TableName _
                                                    , cmdParams)
        Return zenkaiTorikomiTable

    End Function
#End Region

#Region "一括入金処理"
    ''' <summary>
    ''' 対象の系列コードに設定されている一括入金対象の情報を取得します
    ''' </summary>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <returns>一括入金対象の情報を格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetIkkatuNyuukinTaisyouData(ByVal strKeiretuCd As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetIkkatuNyuukinTaisyouData", strKeiretuCd)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("        ISNULL(keiretu_cd,0) AS keiretu_cd ")
        cmdTextSb.Append("       ,ISNULL(syouhin1,0) AS syouhin1 ")
        cmdTextSb.Append("       ,ISNULL(syouhin2,0) AS syouhin2 ")
        cmdTextSb.Append("       ,ISNULL(waribiki_syouhin2,0) AS waribiki_syouhin2 ")
        cmdTextSb.Append("       ,ISNULL(syouhin3,0) AS syouhin3 ")
        cmdTextSb.Append("       ,ISNULL(kairy_koj,0) AS kairy_koj ")
        cmdTextSb.Append("       ,ISNULL(t_koj,0) AS t_koj ")
        cmdTextSb.Append("       ,ISNULL(tys_saihak,0) AS tys_saihak ")
        cmdTextSb.Append("       ,ISNULL(koj_saihak,0) AS koj_saihak ")
        cmdTextSb.Append("       ,ISNULL(hosyousyo_saihak,0) AS hosyousyo_saihak ")
        cmdTextSb.Append("       ,ISNULL(kaiyaku_harai_modoshi,0) AS kaiyaku_harai_modoshi ")
        cmdTextSb.Append("       ,ISNULL(syouhin4,0) AS syouhin4 ")
        cmdTextSb.Append("   FROM m_ikkatu_nyuukin_taisyou ")
        cmdTextSb.Append("  WHERE keiretu_cd = @KEIRETUCD ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@KEIRETUCD", SqlDbType.VarChar, 5, strKeiretuCd)}

        ' データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 邸別請求テーブルの処理対象を抽出します
    ''' </summary>
    ''' <returns>邸別請求テーブルの処理対象を格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuTaisyouData(ByVal htblParams As Hashtable) As DataTable

        '抽出用SQL
        Dim strSql As String = GetSqlSeikyuuTaisyou()
        '抽出用パラメータ()
        Dim cmdParams() As SqlClient.SqlParameter = GetParamSeikyuuTaisyou(htblParams)
        ' データ取得＆返却
        Return cmnDtAcc.getDataTable(strSql, cmdParams)

    End Function

    ''' <summary>
    ''' 請求総額を取得します
    ''' </summary>
    ''' <param name="htblParams">画面情報HashTable</param>
    ''' <returns>請求総額情報のデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSougaku(ByVal htblParams As Hashtable) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSougaku", htblParams)

        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      SUM(zeikomi_uriage_gaku) zeikomi_uriage_sougaku")
        cmdTextSb.Append("    , MAX(nyuukin_flg) nyuukin_flg")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           T.kbn")
        cmdTextSb.Append("         , T.hosyousyo_no")
        cmdTextSb.Append("         , T.bunrui_cd")
        cmdTextSb.Append("         , T.gamen_hyouji_no")
        cmdTextSb.Append("         , T.uri_gaku + ISNULL(T.syouhizei_gaku, T.mst_syouhizei_gaku) zeikomi_uriage_gaku")
        cmdTextSb.Append("         , T.nyuukin_flg")
        cmdTextSb.Append("         ,")
        cmdTextSb.Append("           CASE")
        cmdTextSb.Append("                WHEN t.seikyuu_saki_cd IS NOT NULL")
        cmdTextSb.Append("                 AND t.seikyuu_saki_brc IS NOT NULL")
        cmdTextSb.Append("                 AND T.seikyuu_saki_kbn IS NOT NULL")
        cmdTextSb.Append("                THEN t.seikyuu_saki_cd")
        cmdTextSb.Append("                ELSE v_cd")
        cmdTextSb.Append("           END seikyuu_saki_cd")
        cmdTextSb.Append("         ,")
        cmdTextSb.Append("           CASE")
        cmdTextSb.Append("                WHEN t.seikyuu_saki_cd IS NOT NULL")
        cmdTextSb.Append("                 AND t.seikyuu_saki_brc IS NOT NULL")
        cmdTextSb.Append("                 AND T.seikyuu_saki_kbn IS NOT NULL")
        cmdTextSb.Append("                THEN t.seikyuu_saki_brc")
        cmdTextSb.Append("                ELSE v_brc")
        cmdTextSb.Append("           END seikyuu_saki_brc")
        cmdTextSb.Append("         ,")
        cmdTextSb.Append("           CASE")
        cmdTextSb.Append("                WHEN t.seikyuu_saki_cd IS NOT NULL")
        cmdTextSb.Append("                 AND t.seikyuu_saki_brc IS NOT NULL")
        cmdTextSb.Append("                 AND T.seikyuu_saki_kbn IS NOT NULL")
        cmdTextSb.Append("                THEN t.seikyuu_saki_kbn")
        cmdTextSb.Append("                ELSE v_kbn")
        cmdTextSb.Append("           END seikyuu_saki_kbn")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("          (SELECT")
        cmdTextSb.Append("                T.kbn")
        cmdTextSb.Append("              , T.hosyousyo_no")
        cmdTextSb.Append("              , T.bunrui_cd")
        cmdTextSb.Append("              , T.gamen_hyouji_no")
        cmdTextSb.Append("              , ISNULL(T.uri_gaku, 0) uri_gaku")
        cmdTextSb.Append("              , T.syouhizei_gaku")
        cmdTextSb.Append("              , ROUND(ISNULL(TS.uri_gaku, 0) + ISNULL(TS.syouhizei_gaku, ISNULL(TS.uri_gaku, 0) *  ISNULL(MZ.zeiritu, 0)), 0, 1) mst_syouhizei_gaku")
        cmdTextSb.Append("              , T.seikyuu_saki_cd")
        cmdTextSb.Append("              , T.seikyuu_saki_brc")
        cmdTextSb.Append("              , T.seikyuu_saki_kbn")
        cmdTextSb.Append("              , (")
        cmdTextSb.Append("                     CASE T.ikkatu_nyuukin_flg")
        cmdTextSb.Append("                          WHEN 1")
        cmdTextSb.Append("                          THEN 1")
        cmdTextSb.Append("                          ELSE 0")
        cmdTextSb.Append("                     END) AS nyuukin_flg")
        cmdTextSb.Append("              ,")
        cmdTextSb.Append("                CASE")
        cmdTextSb.Append("                     WHEN T.bunrui_cd='130'")
        cmdTextSb.Append("                     THEN")
        cmdTextSb.Append("                          CASE")
        cmdTextSb.Append("                               WHEN T.koj_gaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                               THEN")
        cmdTextSb.Append("                                    CASE")
        cmdTextSb.Append("                                         WHEN T.koj_gaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                          AND T.koj_gaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                         THEN VT.seikyuu_saki_cd")
        cmdTextSb.Append("                                         ELSE ''")
        cmdTextSb.Append("                                    END")
        cmdTextSb.Append("                               ELSE VK.seikyuu_saki_cd")
        cmdTextSb.Append("                          END")
        cmdTextSb.Append("                     WHEN T.bunrui_cd='140'")
        cmdTextSb.Append("                     THEN")
        cmdTextSb.Append("                          CASE")
        cmdTextSb.Append("                               WHEN T.t_koj_kaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                               THEN")
        cmdTextSb.Append("                                    CASE")
        cmdTextSb.Append("                                         WHEN T.t_koj_kaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                          AND T.t_koj_kaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                         THEN VT.seikyuu_saki_cd")
        cmdTextSb.Append("                                         ELSE ''")
        cmdTextSb.Append("                                    END")
        cmdTextSb.Append("                               ELSE VK.seikyuu_saki_cd")
        cmdTextSb.Append("                          END")
        cmdTextSb.Append("                     ELSE VK.seikyuu_saki_cd")
        cmdTextSb.Append("                END v_cd")
        cmdTextSb.Append("              ,")
        cmdTextSb.Append("                CASE")
        cmdTextSb.Append("                     WHEN T.bunrui_cd='130'")
        cmdTextSb.Append("                     THEN")
        cmdTextSb.Append("                          CASE")
        cmdTextSb.Append("                               WHEN T.koj_gaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                               THEN")
        cmdTextSb.Append("                                    CASE")
        cmdTextSb.Append("                                         WHEN T.koj_gaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                          AND T.koj_gaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                         THEN VT.seikyuu_saki_brc")
        cmdTextSb.Append("                                         ELSE ''")
        cmdTextSb.Append("                                    END")
        cmdTextSb.Append("                               ELSE VK.seikyuu_saki_brc")
        cmdTextSb.Append("                          END")
        cmdTextSb.Append("                     WHEN T.bunrui_cd='140'")
        cmdTextSb.Append("                     THEN")
        cmdTextSb.Append("                          CASE")
        cmdTextSb.Append("                               WHEN T.t_koj_kaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                               THEN")
        cmdTextSb.Append("                                    CASE")
        cmdTextSb.Append("                                         WHEN T.t_koj_kaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                          AND T.t_koj_kaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                         THEN VT.seikyuu_saki_brc")
        cmdTextSb.Append("                                         ELSE ''")
        cmdTextSb.Append("                                    END")
        cmdTextSb.Append("                               ELSE VK.seikyuu_saki_brc")
        cmdTextSb.Append("                          END")
        cmdTextSb.Append("                     ELSE VK.seikyuu_saki_brc")
        cmdTextSb.Append("                END v_brc")
        cmdTextSb.Append("              ,")
        cmdTextSb.Append("                CASE")
        cmdTextSb.Append("                     WHEN T.bunrui_cd='130'")
        cmdTextSb.Append("                     THEN")
        cmdTextSb.Append("                          CASE")
        cmdTextSb.Append("                               WHEN T.koj_gaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                               THEN")
        cmdTextSb.Append("                                    CASE")
        cmdTextSb.Append("                                         WHEN T.koj_gaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                          AND T.koj_gaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                         THEN VT.seikyuu_saki_kbn")
        cmdTextSb.Append("                                         ELSE ''")
        cmdTextSb.Append("                                    END")
        cmdTextSb.Append("                               ELSE VK.seikyuu_saki_kbn")
        cmdTextSb.Append("                          END")
        cmdTextSb.Append("                     WHEN T.bunrui_cd='140'")
        cmdTextSb.Append("                     THEN")
        cmdTextSb.Append("                          CASE")
        cmdTextSb.Append("                               WHEN T.t_koj_kaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                               THEN")
        cmdTextSb.Append("                                    CASE")
        cmdTextSb.Append("                                         WHEN T.t_koj_kaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                          AND T.t_koj_kaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                         THEN VT.seikyuu_saki_kbn")
        cmdTextSb.Append("                                         ELSE ''")
        cmdTextSb.Append("                                    END")
        cmdTextSb.Append("                               ELSE VK.seikyuu_saki_kbn")
        cmdTextSb.Append("                          END")
        cmdTextSb.Append("                     ELSE VK.seikyuu_saki_kbn")
        cmdTextSb.Append("                END v_kbn")
        cmdTextSb.Append("           FROM")
        cmdTextSb.Append("               (SELECT")
        cmdTextSb.Append("                     TT.*")
        cmdTextSb.Append("                   , TJ.kameiten_cd")
        cmdTextSb.Append("                   , TJ.koj_gaisya_seikyuu_umu")
        cmdTextSb.Append("                   , TJ.koj_gaisya_cd")
        cmdTextSb.Append("                   , TJ.koj_gaisya_jigyousyo_cd")
        cmdTextSb.Append("                   , TJ.t_koj_kaisya_seikyuu_umu")
        cmdTextSb.Append("                   , TJ.t_koj_kaisya_cd")
        cmdTextSb.Append("                   , TJ.t_koj_kaisya_jigyousyo_cd")
        cmdTextSb.Append("                FROM")
        cmdTextSb.Append("                     t_teibetu_seikyuu TT")
        cmdTextSb.Append("                          LEFT OUTER JOIN t_jiban TJ")
        cmdTextSb.Append("                            ON TT.kbn=TJ.kbn")
        cmdTextSb.Append("                           AND TT.hosyousyo_no=TJ.hosyousyo_no")
        cmdTextSb.Append("                WHERE")
        cmdTextSb.Append("                     TT.seikyuusyo_hak_date >= @SEIKYUUFROM")
        cmdTextSb.Append("                 AND TT.seikyuusyo_hak_date <= @SEIKYUUTO")
        cmdTextSb.Append("                 AND TT.seikyuu_umu = 1")
        cmdTextSb.Append("               )")
        cmdTextSb.Append("                T")
        cmdTextSb.Append("                     LEFT OUTER JOIN v_syouhin_seikyuusaki_kameiten VK")
        cmdTextSb.Append("                       ON VK.syouhin_cd = T.syouhin_cd")
        cmdTextSb.Append("                      AND VK.kameiten_cd = T.kameiten_cd")
        cmdTextSb.Append("                     LEFT OUTER JOIN v_syouhin_siire_seikyuu_saki_jiban_tyskaisya VT")
        cmdTextSb.Append("                       ON VT.kbn = T.kbn")
        cmdTextSb.Append("                      AND VT.hosyousyo_no = T.hosyousyo_no")
        cmdTextSb.Append("                      AND VT.syouhin_cd = T.syouhin_cd")
        cmdTextSb.Append("                     LEFT OUTER JOIN m_syouhizei MZ")
        cmdTextSb.Append("                       ON T.zei_kbn=MZ.zei_kbn")
        cmdTextSb.Append("          ) T")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      T")
        cmdTextSb.Append("           INNER JOIN m_ikkatu_nyuukin_taisyou_new M")
        cmdTextSb.Append("             ON T.seikyuu_saki_cd = M.seikyuu_saki_cd")
        cmdTextSb.Append("            AND T.seikyuu_saki_brc = M.seikyuu_saki_brc")
        cmdTextSb.Append("            AND T.seikyuu_saki_kbn = M.seikyuu_saki_kbn")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      M.syori_flg = 1")

        '抽出用パラメータ
        Dim cmdParams() As SqlClient.SqlParameter = GetParamSeikyuuTaisyou(htblParams)
        'データ取得＆返却 
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 入金データ作成用ワークデータを格納するテンポラリテーブルを作成します
    ''' </summary>
    ''' <returns>実行件数</returns>
    ''' <remarks></remarks>
    Public Function setTempTableForNyuukinDate() As String
        Dim cmdTextSb As New StringBuilder()

        'テンポラリテーブルの作成
        cmdTextSb.Append(" CREATE TABLE ")
        cmdTextSb.Append(TEMP_TABLE)
        cmdTextSb.Append("       (kbn                  CHAR(1)     NOT NULL ")
        cmdTextSb.Append("       ,hosyousyo_no         VARCHAR(10) NOT NULL ")
        cmdTextSb.Append("       ,bunrui_cd            VARCHAR(3)  NOT NULL ")
        cmdTextSb.Append("       ,gamen_hyouji_no      INT         NOT NULL ")
        cmdTextSb.Append("       ,zeikomi_uriage_gaku  INT ")
        cmdTextSb.Append("       ,constraint TEMP_WORK_PKC primary key (kbn,hosyousyo_no,bunrui_cd,gamen_hyouji_no)) ")
        cmdTextSb.Append(" INSERT INTO " & TEMP_TABLE)
        cmdTextSb.Append("       (kbn ")
        cmdTextSb.Append("       ,hosyousyo_no ")
        cmdTextSb.Append("       ,bunrui_cd ")
        cmdTextSb.Append("       ,gamen_hyouji_no ")
        cmdTextSb.Append("       ,zeikomi_uriage_gaku) ")
        cmdTextSb.Append(GetSqlNyuukinTaisyou())

        Return cmdTextSb.ToString

    End Function

    ''' <summary>
    ''' 邸別入金テーブルを一括更新します
    ''' </summary>
    ''' <param name="htblParams">画面情報HashTable</param>
    ''' <param name="updLoginUserId">更新ログインユーザー</param>
    ''' <returns>更新件数</returns>
    ''' <remarks>複数レコードの更新を行う</remarks>
    Public Function UpdTeibetuNyuukin(ByVal strMakeTmpSql As String, ByVal htblParams As Hashtable, ByVal updLoginUserId As String) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdTeibetuNyuukin" _
                                                    , htblParams _
                                                    , updLoginUserId)
        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(strMakeTmpSql)
        cmdTextSb.Append(" UPDATE t_teibetu_nyuukin ")
        cmdTextSb.Append("    SET zeikomi_nyuukin_gaku = N.zeikomi_nyuukin_gaku + V.zeikomi_uriage_gaku ")
        cmdTextSb.Append("       ,saisyuu_nyuukin_date = @SAISYUUNYUUKINBI ")
        cmdTextSb.Append("       ,upd_login_user_id    = @UPDLOGINUSERID ")
        cmdTextSb.Append("       ,upd_datetime         = @UPDDATETIME ")
        cmdTextSb.Append("   FROM t_teibetu_nyuukin N ")
        cmdTextSb.Append("  INNER JOIN " & TEMP_TABLE & " V ")
        cmdTextSb.Append("     ON N.kbn = V.kbn ")
        cmdTextSb.Append("    AND N.hosyousyo_no = V.hosyousyo_no ")
        cmdTextSb.Append("    AND N.bunrui_cd = V.bunrui_cd ")
        cmdTextSb.Append("    AND N.gamen_hyouji_no = V.gamen_hyouji_no ")

        'テンポラリテーブル用パラメータ
        Dim cmdTmpParams() As SqlClient.SqlParameter = GetParamSeikyuuTaisyou(htblParams)

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@SAISYUUNYUUKINBI", SqlDbType.DateTime, 16, DateTime.Today), _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        'パラメータの結合
        cmnDtAcc.AddSqlParameter(cmdParams, cmdParams)

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別入金テーブルに一括登録します
    ''' </summary>
    ''' <returns>登録件数</returns>
    ''' <remarks>複数レコード</remarks>
    Public Function InsTeibetuNyuukin(ByVal addLoginUserId As String) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsTeibetuNyuukin", addLoginUserId)
        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" INSERT INTO ")
        cmdTextSb.Append("        t_teibetu_nyuukin ")
        cmdTextSb.Append("       (kbn ")
        cmdTextSb.Append("       ,hosyousyo_no ")
        cmdTextSb.Append("       ,bunrui_cd ")
        cmdTextSb.Append("       ,gamen_hyouji_no ")
        cmdTextSb.Append("       ,zeikomi_nyuukin_gaku ")
        cmdTextSb.Append("       ,zeikomi_henkin_gaku ")
        cmdTextSb.Append("       ,saisyuu_nyuukin_date ")
        cmdTextSb.Append("       ,add_login_user_id ")
        cmdTextSb.Append("       ,add_datetime) ")
        cmdTextSb.Append(" SELECT kbn ")
        cmdTextSb.Append("       ,hosyousyo_no ")
        cmdTextSb.Append("       ,bunrui_cd ")
        cmdTextSb.Append("       ,zeikomi_uriage_gaku ")
        cmdTextSb.Append("       ,gamen_hyouji_no ")
        cmdTextSb.Append("       ,0 ")
        cmdTextSb.Append("       ,@SAISYUUNYUUKINBI ")
        cmdTextSb.Append("       ,@ADDLOGINUSERID ")
        cmdTextSb.Append("       ,@ADDDATETIME ")
        cmdTextSb.Append("   FROM " & TEMP_TABLE & " V ")
        cmdTextSb.Append("  WHERE NOT EXISTS ( ")
        cmdTextSb.Append("                    SELECT * ")
        cmdTextSb.Append("                      FROM t_teibetu_nyuukin T ")
        cmdTextSb.Append("                     WHERE V.kbn = T.kbn ")
        cmdTextSb.Append("                       AND V.hosyousyo_no = T.hosyousyo_no ")
        cmdTextSb.Append("                       AND V.bunrui_cd = T.bunrui_cd ")
        cmdTextSb.Append("                       AND V.gamen_hyouji_no = T.gamen_hyouji_no ) ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@SAISYUUNYUUKINBI", SqlDbType.DateTime, 16, DateTime.Today), _
            SQLHelper.MakeParam("@ADDLOGINUSERID", SqlDbType.VarChar, 30, addLoginUserId), _
            SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別入金テーブル連携管理テーブルを更新する対象のキーを取得します
    ''' </summary>
    ''' <returns>更新対象のキーを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuNyuukinUpdTargetKey() As DataTable
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim dsRenkei As New TeibetuNyuukinRenkeiDataSet
        Dim dtblRenkei As DataTable

        '更新対象取得用SQL設定
        cmdTextSb.Append(" SELECT N.kbn ")
        cmdTextSb.Append("       ,N.hosyousyo_no ")
        cmdTextSb.Append("       ,N.bunrui_cd ")
        cmdTextSb.Append("       ,N.gamen_hyouji_no ")
        cmdTextSb.Append("       ,R.renkei_siji_cd ")
        cmdTextSb.Append("       ,R.sousin_jyky_cd ")
        cmdTextSb.Append("   FROM t_teibetu_nyuukin N ")
        cmdTextSb.Append("  INNER JOIN ")
        cmdTextSb.Append("        " & TEMP_TABLE & " V ")
        cmdTextSb.Append("     ON N.kbn             = V.kbn ")
        cmdTextSb.Append("    AND N.hosyousyo_no    = V.hosyousyo_no ")
        cmdTextSb.Append("    AND N.bunrui_cd       = V.bunrui_cd ")
        cmdTextSb.Append("    AND N.gamen_hyouji_no = V.gamen_hyouji_no ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        t_teibetu_nyuukin_renkei R WITH(UPDLOCK)")
        cmdTextSb.Append("     ON N.kbn = R.kbn ")
        cmdTextSb.Append("    AND N.hosyousyo_no    = R.hosyousyo_no ")
        cmdTextSb.Append("    AND N.bunrui_cd       = R.bunrui_cd ")
        cmdTextSb.Append("    AND N.gamen_hyouji_no = R.gamen_hyouji_no ")

        ' 検索実行
        dtblRenkei = cmnDtAcc.getDataTable(cmdTextSb.ToString _
                                            , dsRenkei _
                                            , dsRenkei.TeibetuNyuukinRenkeiTarget.TableName)
        Return dtblRenkei

    End Function

    ''' <summary>
    ''' 邸別入金テーブル連携管理テーブルに登録する対象のキーを取得します
    ''' </summary>
    ''' <returns>登録対象のキーを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuNyuukinInsTargetKey() As DataTable
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim dsRenkei As New TeibetuNyuukinRenkeiDataSet
        Dim dtblRenkei As DataTable

        cmdTextSb.Append(" SELECT V.kbn ")
        cmdTextSb.Append("       ,V.hosyousyo_no ")
        cmdTextSb.Append("       ,V.bunrui_cd ")
        cmdTextSb.Append("       ,V.gamen_hyouji_no ")
        cmdTextSb.Append("       ,R.renkei_siji_cd")
        cmdTextSb.Append("       ,R.sousin_jyky_cd")
        cmdTextSb.Append("   FROM " & TEMP_TABLE & " V ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        t_teibetu_nyuukin_renkei R WITH(UPDLOCK)")
        cmdTextSb.Append("     ON V.kbn = R.kbn ")
        cmdTextSb.Append("    AND V.hosyousyo_no = R.hosyousyo_no ")
        cmdTextSb.Append("    AND V.bunrui_cd = R.bunrui_cd ")
        cmdTextSb.Append("  WHERE NOT EXISTS ( ")
        cmdTextSb.Append("                    SELECT * ")
        cmdTextSb.Append("                      FROM t_teibetu_nyuukin T ")
        cmdTextSb.Append("                     WHERE V.kbn = T.kbn ")
        cmdTextSb.Append("                       AND V.hosyousyo_no = T.hosyousyo_no ")
        cmdTextSb.Append("                       AND V.bunrui_cd = T.bunrui_cd ) ")

        ' 検索実行
        dtblRenkei = cmnDtAcc.getDataTable(cmdTextSb.ToString _
                                            , dsRenkei _
                                            , dsRenkei.TeibetuNyuukinRenkeiTarget.TableName)
        Return dtblRenkei

    End Function

    ''' <summary>
    ''' 邸別請求テーブルを更新します
    ''' </summary>
    ''' <param name="htblParams">画面情報HashTable</param>
    ''' <param name="updLoginUserId">更新ログインユーザー</param>
    ''' <returns>処理件数</returns>
    ''' <remarks></remarks>
    Public Function UpdTeibetuSeikyuu(ByVal htblParams As Hashtable, ByVal updLoginUserId As String) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdTeibetuSeikyuu", htblParams, updLoginUserId)

        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim cmdParamsUpd() As SqlClient.SqlParameter
        Dim intResult As Integer

        cmdTextSb.Append(" UPDATE t_teibetu_seikyuu ")
        cmdTextSb.Append("    SET ikkatu_nyuukin_flg = 1 ")
        cmdTextSb.Append("       ,upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("       ,upd_datetime     = @UPDDATETIME ")
        cmdTextSb.Append("   FROM t_teibetu_seikyuu TS ")
        cmdTextSb.Append("  INNER JOIN t_jiban Z ")
        cmdTextSb.Append("     ON TS.kbn = Z.kbn ")
        cmdTextSb.Append("    AND TS.hosyousyo_no = Z.hosyousyo_no ")
        cmdTextSb.Append("  INNER JOIN m_kameiten KM ")
        cmdTextSb.Append("     ON Z.kameiten_cd = KM.kameiten_cd ")
        cmdTextSb.Append("  WHERE (TS.seikyuusyo_hak_date BETWEEN @SEIKYUUFROM AND @SEIKYUUTO) ")
        cmdTextSb.Append("    AND TS.seikyuu_umu = 1 ")
        cmdTextSb.Append("    AND KM.keiretu_cd = @KEIRETUCD  ")
        'cmdTextSb.Append("    AND " & htblParams(BUNRUI_WHERE))

        ' パラメータへ設定
        cmdParams = GetParamSeikyuuTaisyou(htblParams)
        cmdParamsUpd = New SqlParameter() { _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}
        'パラメータの結合
        cmnDtAcc.AddSqlParameter(cmdParams, cmdParamsUpd)

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別請求テーブル連携管理テーブルを更新する対象のキーを取得します
    ''' </summary>
    ''' <param name="htblParams">画面情報HashTable</param>
    ''' <returns>更新対象のキーを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuRenkeiTarget(ByVal htblParams As Hashtable) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuRenkeiTarget", htblParams)

        Dim cmdTextSb As New StringBuilder()
        Dim dsRenkei As New TeibetuRenkeiDataSet
        Dim dtblRenkei As DataTable

        cmdTextSb.Append(" SELECT TS.kbn ")
        cmdTextSb.Append("       ,TS.hosyousyo_no ")
        cmdTextSb.Append("       ,TS.bunrui_cd ")
        cmdTextSb.Append("       ,TS.gamen_hyouji_no ")
        cmdTextSb.Append("       ,R.renkei_siji_cd ")
        cmdTextSb.Append("       ,R.sousin_jyky_cd ")
        cmdTextSb.Append("   FROM t_teibetu_seikyuu TS ")
        cmdTextSb.Append("  INNER JOIN t_jiban Z ")
        cmdTextSb.Append("     ON TS.kbn = Z.kbn ")
        cmdTextSb.Append("    AND TS.hosyousyo_no = Z.hosyousyo_no ")
        cmdTextSb.Append("  INNER JOIN m_kameiten KM ")
        cmdTextSb.Append("     ON Z.kameiten_cd = KM.kameiten_cd ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        t_teibetu_seikyuu_renkei R WITH(UPDLOCK)")
        cmdTextSb.Append("     ON TS.kbn = R.kbn ")
        cmdTextSb.Append("    AND TS.hosyousyo_no = R.hosyousyo_no ")
        cmdTextSb.Append("    AND TS.bunrui_cd = R.bunrui_cd ")
        cmdTextSb.Append("    AND TS.gamen_hyouji_no = R.gamen_hyouji_no ")
        cmdTextSb.Append("  WHERE (TS.seikyuusyo_hak_date BETWEEN @SEIKYUUFROM AND @SEIKYUUTO) ")
        cmdTextSb.Append("    AND TS.seikyuu_umu = 1 ")
        cmdTextSb.Append("    AND KM.keiretu_cd = @KEIRETUCD  ")
        'cmdTextSb.Append("    AND " & htblParams(BUNRUI_WHERE))

        '抽出用パラメータ
        Dim cmdParams() As SqlClient.SqlParameter = GetParamSeikyuuTaisyou(htblParams)

        ' 検索実行
        dtblRenkei = cmnDtAcc.getDataTable(cmdTextSb.ToString _
                                            , dsRenkei _
                                            , dsRenkei.TeibetuRenkeiTarget.TableName _
                                            , cmdParams)
        Return dtblRenkei

    End Function

    ''' <summary>
    ''' 更新対象の地盤テーブルを絞り込むキーを取得し、テンポラリテーブルに格納します
    ''' </summary>
    ''' <returns>レコード数</returns>
    ''' <remarks>商品1,2,解約のいずれかが入金対象であり、残額0のもののキー取得</remarks>
    Public Function InsJibanUpdTgtKeyToTemp() As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim intResult As Integer

        cmdTextSb.Append(" SELECT WK.kbn, WK.hosyousyo_no ")
        cmdTextSb.Append("   INTO ##TEMP_JIBAN_UPD_TGT_KEY ")
        cmdTextSb.Append("   FROM (")
        cmdTextSb.Append("          SELECT kbn ")
        cmdTextSb.Append("                ,hosyousyo_no ")
        cmdTextSb.Append("                ,'100' AS bunrui_cd ")
        cmdTextSb.Append("            FROM " & TEMP_TABLE)
        cmdTextSb.Append("           WHERE (bunrui_cd BETWEEN '100' AND '115')")
        cmdTextSb.Append("              OR (bunrui_cd = '180')")
        cmdTextSb.Append("        ) WK ")
        cmdTextSb.Append("  INNER JOIN ")
        cmdTextSb.Append("        v_seikyuu_nyuukin_jouhou VW ")
        cmdTextSb.Append("     ON WK.kbn          = VW.kbn ")
        cmdTextSb.Append("    AND WK.hosyousyo_no = VW.hosyousyo_no ")
        cmdTextSb.Append("    AND WK.bunrui_cd    = VW.bunrui_cd ")
        cmdTextSb.Append("  INNER JOIN ")
        cmdTextSb.Append("        t_teibetu_seikyuu TS ")
        cmdTextSb.Append("     ON WK.kbn          = TS.kbn ")
        cmdTextSb.Append("    AND WK.hosyousyo_no = TS.hosyousyo_no ")
        cmdTextSb.Append("  WHERE WK.bunrui_cd       = '100' ")
        cmdTextSb.Append("    AND TS.bunrui_cd       = '180' ")
        cmdTextSb.Append("    AND VW.nyuukin_zangaku = 0 ")

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString())
        Return intResult
    End Function

    ''' <summary>
    ''' 地盤テーブルを更新します
    ''' </summary>
    ''' <param name="updLoginUserId">更新ログインユーザー</param>
    ''' <returns>処理件数</returns>
    ''' <remarks></remarks>
    Public Function UpdJibanTable(ByVal updLoginUserId As String) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdJibanTable" _
                                                    , updLoginUserId)
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer
        Dim strKousinsya As String = strLogic.GetKousinsya(updLoginUserId, DateTime.Now) '更新者

        commandTextSb.Append(" UPDATE t_jiban  ")
        commandTextSb.Append("    SET henkin_syori_flg  = 1 ")
        commandTextSb.Append("       ,henkin_syori_date = @HENKINSYORIDATE ")
        commandTextSb.Append("       ,upd_login_user_id = @UPDLOGINUSERID ")
        commandTextSb.Append("       ,upd_datetime      = @UPDDATETIME ")
        commandTextSb.Append("       ,kousinsya      = @KOUSINSYA ")
        commandTextSb.Append("   FROM t_jiban Z ")
        commandTextSb.Append("  INNER JOIN ")
        commandTextSb.Append("        ##TEMP_JIBAN_UPD_TGT_KEY WK ")
        commandTextSb.Append("     ON Z.kbn          = WK.kbn ")
        commandTextSb.Append("    AND Z.hosyousyo_no = WK.hosyousyo_no ")

        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@HENKINSYORIDATE", SqlDbType.DateTime, 16, DateTime.Today), _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, DateTime.Now), _
            SQLHelper.MakeParam("@KOUSINSYA", SqlDbType.VarChar, 30, strKousinsya)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 地盤テーブル連携管理テーブルを更新する対象のキーを取得します
    ''' </summary>
    ''' <returns>更新対象のキーを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetJibanRenkeiTarget() As DataTable
        Dim dsRenkei As New JibanRenkeiDataSet
        Dim cmdTextSb As New StringBuilder()
        Dim dtblRenkei As DataTable

        cmdTextSb.Append(" SELECT Z.kbn ")
        cmdTextSb.Append("       ,Z.hosyousyo_no ")
        cmdTextSb.Append("       ,R.renkei_siji_cd ")
        cmdTextSb.Append("       ,R.sousin_jyky_cd ")
        cmdTextSb.Append("  FROM t_jiban Z ")
        cmdTextSb.Append(" INNER JOIN ")
        cmdTextSb.Append("       ##TEMP_JIBAN_UPD_TGT_KEY WK ")
        cmdTextSb.Append("    ON Z.kbn          = WK.kbn ")
        cmdTextSb.Append("   AND Z.hosyousyo_no = WK.hosyousyo_no ")
        cmdTextSb.Append("  LEFT OUTER JOIN ")
        cmdTextSb.Append("       t_jiban_renkei R WITH(UPDLOCK)")
        cmdTextSb.Append("    ON Z.kbn          = R.kbn ")
        cmdTextSb.Append("   AND Z.hosyousyo_no = R.hosyousyo_no ")

        ' 検索実行
        dtblRenkei = cmnDtAcc.getDataTable(cmdTextSb.ToString _
                                            , dsRenkei _
                                            , dsRenkei.JibanRenkeiTarget.TableName)
        Return dtblRenkei

    End Function

    ''' <summary>
    ''' テンポラリテーブルを破棄します
    ''' </summary>
    ''' <returns>処理件数</returns>
    ''' <remarks></remarks>
    Public Function dropTemp() As Integer
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()

        'SQL文の初期化
        cmdTextSb = New StringBuilder
        cmdTextSb.Append(" DROP TABLE " & TEMP_TABLE)

        cmdTextSb.Append(" DROP TABLE ##TEMP_JIBAN_UPD_TGT_KEY ")
        intResult += ExecuteNonQuery(connStr, _
                            CommandType.Text, _
                            cmdTextSb.ToString())
        Return intResult
    End Function


#Region "プライベートメソッド"
    ''' <summary>
    ''' 邸別請求テーブルの処理対象を抽出するSQLを取得
    ''' </summary>
    ''' <returns>邸別請求テーブルの処理対象を抽出するSQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetSqlSeikyuuTaisyou() As String
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      T.*")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           T.kbn")
        cmdTextSb.Append("         , T.hosyousyo_no")
        cmdTextSb.Append("         , T.bunrui_cd")
        cmdTextSb.Append("         , T.gamen_hyouji_no")
        cmdTextSb.Append("         , T.uri_gaku + ISNULL(T.syouhizei_gaku, T.mst_syouhizei_gaku) zeikomi_uriage_gaku")
        cmdTextSb.Append("         , T.nyuukin_flg")
        cmdTextSb.Append("         ,")
        cmdTextSb.Append("           CASE")
        cmdTextSb.Append("                WHEN t.seikyuu_saki_cd IS NOT NULL")
        cmdTextSb.Append("                 AND t.seikyuu_saki_brc IS NOT NULL")
        cmdTextSb.Append("                 AND T.seikyuu_saki_kbn IS NOT NULL")
        cmdTextSb.Append("                THEN t.seikyuu_saki_cd")
        cmdTextSb.Append("                ELSE v_cd")
        cmdTextSb.Append("           END seikyuu_saki_cd")
        cmdTextSb.Append("         ,")
        cmdTextSb.Append("           CASE")
        cmdTextSb.Append("                WHEN t.seikyuu_saki_cd IS NOT NULL")
        cmdTextSb.Append("                 AND t.seikyuu_saki_brc IS NOT NULL")
        cmdTextSb.Append("                 AND T.seikyuu_saki_kbn IS NOT NULL")
        cmdTextSb.Append("                THEN t.seikyuu_saki_brc")
        cmdTextSb.Append("                ELSE v_brc")
        cmdTextSb.Append("           END seikyuu_saki_brc")
        cmdTextSb.Append("         ,")
        cmdTextSb.Append("           CASE")
        cmdTextSb.Append("                WHEN t.seikyuu_saki_cd IS NOT NULL")
        cmdTextSb.Append("                 AND t.seikyuu_saki_brc IS NOT NULL")
        cmdTextSb.Append("                 AND T.seikyuu_saki_kbn IS NOT NULL")
        cmdTextSb.Append("                THEN t.seikyuu_saki_kbn")
        cmdTextSb.Append("                ELSE v_kbn")
        cmdTextSb.Append("           END seikyuu_saki_kbn")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("          (SELECT")
        cmdTextSb.Append("                T.kbn")
        cmdTextSb.Append("              , T.hosyousyo_no")
        cmdTextSb.Append("              , T.bunrui_cd")
        cmdTextSb.Append("              , T.gamen_hyouji_no")
        cmdTextSb.Append("              , ISNULL(T.uri_gaku, 0) uri_gaku")
        cmdTextSb.Append("              , T.syouhizei_gaku")
        cmdTextSb.Append("              , ROUND(ISNULL(T.uri_gaku, 0) * ISNULL(MZ.zeiritu, 0), 0, 1) mst_syouhizei_gaku")
        cmdTextSb.Append("              , T.seikyuu_saki_cd")
        cmdTextSb.Append("              , T.seikyuu_saki_brc")
        cmdTextSb.Append("              , T.seikyuu_saki_kbn")
        cmdTextSb.Append("              , (")
        cmdTextSb.Append("                     CASE T.ikkatu_nyuukin_flg")
        cmdTextSb.Append("                          WHEN 1")
        cmdTextSb.Append("                          THEN 1")
        cmdTextSb.Append("                          ELSE 0")
        cmdTextSb.Append("                     END) AS nyuukin_flg")
        cmdTextSb.Append("              ,")
        cmdTextSb.Append("                CASE")
        cmdTextSb.Append("                     WHEN T.bunrui_cd='130'")
        cmdTextSb.Append("                     THEN")
        cmdTextSb.Append("                          CASE")
        cmdTextSb.Append("                               WHEN T.koj_gaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                               THEN")
        cmdTextSb.Append("                                    CASE")
        cmdTextSb.Append("                                         WHEN T.koj_gaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                          AND T.koj_gaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                         THEN VT.seikyuu_saki_cd")
        cmdTextSb.Append("                                         ELSE ''")
        cmdTextSb.Append("                                    END")
        cmdTextSb.Append("                               ELSE VK.seikyuu_saki_cd")
        cmdTextSb.Append("                          END")
        cmdTextSb.Append("                     WHEN T.bunrui_cd='140'")
        cmdTextSb.Append("                     THEN")
        cmdTextSb.Append("                          CASE")
        cmdTextSb.Append("                               WHEN T.t_koj_kaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                               THEN")
        cmdTextSb.Append("                                    CASE")
        cmdTextSb.Append("                                         WHEN T.t_koj_kaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                          AND T.t_koj_kaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                         THEN VT.seikyuu_saki_cd")
        cmdTextSb.Append("                                         ELSE ''")
        cmdTextSb.Append("                                    END")
        cmdTextSb.Append("                               ELSE VK.seikyuu_saki_cd")
        cmdTextSb.Append("                          END")
        cmdTextSb.Append("                     ELSE VK.seikyuu_saki_cd")
        cmdTextSb.Append("                END v_cd")
        cmdTextSb.Append("              ,")
        cmdTextSb.Append("                CASE")
        cmdTextSb.Append("                     WHEN T.bunrui_cd='130'")
        cmdTextSb.Append("                     THEN")
        cmdTextSb.Append("                          CASE")
        cmdTextSb.Append("                               WHEN T.koj_gaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                               THEN")
        cmdTextSb.Append("                                    CASE")
        cmdTextSb.Append("                                         WHEN T.koj_gaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                          AND T.koj_gaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                         THEN VT.seikyuu_saki_brc")
        cmdTextSb.Append("                                         ELSE ''")
        cmdTextSb.Append("                                    END")
        cmdTextSb.Append("                               ELSE VK.seikyuu_saki_brc")
        cmdTextSb.Append("                          END")
        cmdTextSb.Append("                     WHEN T.bunrui_cd='140'")
        cmdTextSb.Append("                     THEN")
        cmdTextSb.Append("                          CASE")
        cmdTextSb.Append("                               WHEN T.t_koj_kaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                               THEN")
        cmdTextSb.Append("                                    CASE")
        cmdTextSb.Append("                                         WHEN T.t_koj_kaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                          AND T.t_koj_kaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                         THEN VT.seikyuu_saki_brc")
        cmdTextSb.Append("                                         ELSE ''")
        cmdTextSb.Append("                                    END")
        cmdTextSb.Append("                               ELSE VK.seikyuu_saki_brc")
        cmdTextSb.Append("                          END")
        cmdTextSb.Append("                     ELSE VK.seikyuu_saki_brc")
        cmdTextSb.Append("                END v_brc")
        cmdTextSb.Append("              ,")
        cmdTextSb.Append("                CASE")
        cmdTextSb.Append("                     WHEN T.bunrui_cd='130'")
        cmdTextSb.Append("                     THEN")
        cmdTextSb.Append("                          CASE")
        cmdTextSb.Append("                               WHEN T.koj_gaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                               THEN")
        cmdTextSb.Append("                                    CASE")
        cmdTextSb.Append("                                         WHEN T.koj_gaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                          AND T.koj_gaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                         THEN VT.seikyuu_saki_kbn")
        cmdTextSb.Append("                                         ELSE ''")
        cmdTextSb.Append("                                    END")
        cmdTextSb.Append("                               ELSE VK.seikyuu_saki_kbn")
        cmdTextSb.Append("                          END")
        cmdTextSb.Append("                     WHEN T.bunrui_cd='140'")
        cmdTextSb.Append("                     THEN")
        cmdTextSb.Append("                          CASE")
        cmdTextSb.Append("                               WHEN T.t_koj_kaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                               THEN")
        cmdTextSb.Append("                                    CASE")
        cmdTextSb.Append("                                         WHEN T.t_koj_kaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                          AND T.t_koj_kaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                         THEN VT.seikyuu_saki_kbn")
        cmdTextSb.Append("                                         ELSE ''")
        cmdTextSb.Append("                                    END")
        cmdTextSb.Append("                               ELSE VK.seikyuu_saki_kbn")
        cmdTextSb.Append("                          END")
        cmdTextSb.Append("                     ELSE VK.seikyuu_saki_kbn")
        cmdTextSb.Append("                END v_kbn")
        cmdTextSb.Append("           FROM")
        cmdTextSb.Append("               (SELECT")
        cmdTextSb.Append("                     TT.*")
        cmdTextSb.Append("                   , TJ.kameiten_cd")
        cmdTextSb.Append("                   , TJ.koj_gaisya_seikyuu_umu")
        cmdTextSb.Append("                   , TJ.koj_gaisya_cd")
        cmdTextSb.Append("                   , TJ.koj_gaisya_jigyousyo_cd")
        cmdTextSb.Append("                   , TJ.t_koj_kaisya_seikyuu_umu")
        cmdTextSb.Append("                   , TJ.t_koj_kaisya_cd")
        cmdTextSb.Append("                   , TJ.t_koj_kaisya_jigyousyo_cd")
        cmdTextSb.Append("                FROM")
        cmdTextSb.Append("                     t_teibetu_seikyuu TT")
        cmdTextSb.Append("                          LEFT OUTER JOIN t_jiban TJ")
        cmdTextSb.Append("                            ON TT.kbn=TJ.kbn")
        cmdTextSb.Append("                           AND TT.hosyousyo_no=TJ.hosyousyo_no")
        cmdTextSb.Append("                WHERE")
        cmdTextSb.Append("                     TT.seikyuusyo_hak_date >= @SEIKYUUFROM")
        cmdTextSb.Append("                 AND TT.seikyuusyo_hak_date <= @SEIKYUUTO")
        cmdTextSb.Append("                 AND TT.seikyuu_umu = 1")
        cmdTextSb.Append("               )")
        cmdTextSb.Append("                T")
        cmdTextSb.Append("                     LEFT OUTER JOIN v_syouhin_seikyuusaki_kameiten VK")
        cmdTextSb.Append("                       ON VK.syouhin_cd = T.syouhin_cd")
        cmdTextSb.Append("                      AND VK.kameiten_cd = T.kameiten_cd")
        cmdTextSb.Append("                     LEFT OUTER JOIN v_syouhin_siire_seikyuu_saki_jiban_tyskaisya VT")
        cmdTextSb.Append("                       ON VT.kbn = T.kbn")
        cmdTextSb.Append("                      AND VT.hosyousyo_no = T.hosyousyo_no")
        cmdTextSb.Append("                      AND VT.syouhin_cd = T.syouhin_cd")
        cmdTextSb.Append("                     LEFT OUTER JOIN m_syouhizei MZ")
        cmdTextSb.Append("                       ON T.zei_kbn=MZ.zei_kbn")
        cmdTextSb.Append("          ) T")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      T")
        cmdTextSb.Append("           INNER JOIN m_ikkatu_nyuukin_taisyou_new M")
        cmdTextSb.Append("             ON T.seikyuu_saki_cd = M.seikyuu_saki_cd")
        cmdTextSb.Append("            AND T.seikyuu_saki_brc = M.seikyuu_saki_brc")
        cmdTextSb.Append("            AND T.seikyuu_saki_kbn = M.seikyuu_saki_kbn")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      M.syori_flg = 1")

        Return cmdTextSb.ToString

    End Function

    ''' <summary>
    ''' 邸別請求テーブルの処理対象を抽出するSQL用パラメータを取得
    ''' </summary>
    ''' <param name="htblParams">画面情報HashTable</param>
    ''' <returns>邸別請求テーブルの処理対象を抽出するSQL用パラメータ</returns>
    ''' <remarks></remarks>
    Private Function GetParamSeikyuuTaisyou(ByVal htblParams As Hashtable) As SqlClient.SqlParameter()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetParamSeikyuuTaisyou", htblParams)

        Dim cmdParams() As SqlClient.SqlParameter

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@SEIKYUUFROM", SqlDbType.DateTime, 16, htblParams(SEIKYUU_FROM)), _
            SQLHelper.MakeParam("@SEIKYUUTO", SqlDbType.DateTime, 16, htblParams(SEIKYUU_TO))}

        Return cmdParams

    End Function

    ''' <summary>
    ''' 邸別入金テーブルに一括更新する為に、処理対象の邸別請求データを加工して抽出するSQLを取得
    ''' </summary>
    ''' <returns>邸別入金テーブルに一括更新する為に、処理対象の邸別請求データを加工して抽出するSQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetSqlNyuukinTaisyou() As String

        Dim cmdTextSb As New StringBuilder()
        cmdTextSb.Append(" SELECT kbn ")
        cmdTextSb.Append("       ,hosyousyo_no ")
        cmdTextSb.Append("       ,bunrui_cd ")
        cmdTextSb.Append("       ,gamen_hyouji_no ")
        cmdTextSb.Append("       ,SUM(zeikomi_uriage_gaku) AS zeikomi_uriage_gaku ")
        cmdTextSb.Append("   FROM ( ")
        cmdTextSb.Append("          " & GetSqlSeikyuuTaisyou() & ") T")
        cmdTextSb.Append("  GROUP BY ")
        cmdTextSb.Append("        kbn ")
        cmdTextSb.Append("      , hosyousyo_no ")
        cmdTextSb.Append("      , bunrui_cd ")
        cmdTextSb.Append("      , gamen_hyouji_no ")

        Return cmdTextSb.ToString

    End Function

#End Region

#End Region

#Region "入金データ取り込み処理"
    ''' <summary>
    ''' 商品コードおよび倉庫(分類)コードを取得します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinBunrui() As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinBunrui")

        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("  syouhin_cd")
        cmdTextSb.Append("  ,souko_cd")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("  m_syouhin")

        'データ取得＆返却 
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString)

    End Function

    ''' <summary>
    ''' アップロード管理テーブルのEDI情報作成日を取得
    ''' </summary>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function getUpdMngEdi() As DataTable
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      edi_jouhou_sakusei_date")
        cmdTextSb.Append("    , nyuuryoku_file_mei")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      t_upload_kanri")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      file_kbn is null")

        Return cmnDtAcc.getDataTable(cmdTextSb.ToString)

    End Function

    ''' <summary>
    ''' CSVで取り込んだ情報を格納するテンポラリテーブルを作成します
    ''' </summary>
    ''' <returns>実行件数</returns>
    ''' <remarks></remarks>
    Public Function MakeTempTable() As Integer
        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder()

        'テンポラリテーブルの作成
        cmdTextSb.Append(" CREATE TABLE ")
        cmdTextSb.Append("       " & TEMP_TABLE)
        cmdTextSb.Append("       (kbn                  CHAR(1)     NOT NULL ")
        cmdTextSb.Append("       ,hosyousyo_no         VARCHAR(10) NOT NULL ")
        cmdTextSb.Append("       CONSTRAINT TEMP_WORK_PKC primary key (kbn,hosyousyo_no)) ")

        'テンポラリテーブルの作成
        cmdTextSb.Append(" CREATE TABLE ")
        cmdTextSb.Append("        " & TEMP_CSV_TABLE)
        cmdTextSb.Append("       (kbn                       CHAR(1)     ")
        cmdTextSb.Append("       ,hosyousyo_no              VARCHAR(10) ")
        cmdTextSb.Append("       ,bunrui_cd                 VARCHAR(3) ")
        cmdTextSb.Append("       ,gamen_hyouji_no           INTEGER ")
        cmdTextSb.Append("       ,zeikomi_nyuukin_gaku      INTEGER ")
        cmdTextSb.Append("       ,zeikomi_henkin_gaku       INTEGER ")
        cmdTextSb.Append("       ,saisyuu_nyuukin_date      DATETIME ")
        cmdTextSb.Append("       ,edi_jouhou_sakusei_date   VARCHAR(40) NOT NULL ")
        cmdTextSb.Append("       ,gyou_no                   INTEGER NOT NULL ")
        cmdTextSb.Append("       ,syori_datetime            DATETIME NOT NULL ")
        cmdTextSb.Append("       ,group_cd                  VARCHAR(30) ")
        cmdTextSb.Append("       ,kokyaku_cd                VARCHAR(30) ")
        cmdTextSb.Append("       ,tekiyou                   VARCHAR(510) ")
        cmdTextSb.Append("       ,nyuukin_gaku              INTEGER ")
        cmdTextSb.Append("       ,syouhin_cd                CHAR(8) ")
        cmdTextSb.Append("       ) ")

        ' クエリ実行
        intResult += ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString())
    End Function

    ''' <summary>
    ''' CSV取込データをテンポラリテーブルに登録します
    ''' </summary>
    ''' <param name="clsCsvRec">CSV取り込み情報格納レコード</param>
    ''' <param name="intRowNo">行NO</param>
    ''' <param name="dtSyoriDate">処理日</param>
    ''' <returns>処理件数</returns>
    ''' <remarks></remarks>
    Public Function InsCsvDataToTemp( _
                                    ByVal clsCsvRec As NyuukinDataCsvRecord _
                                    , ByRef intRowNo As Integer _
                                    , ByRef dtSyoriDate As DateTime _
                                    ) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsCsvDataToTemp" _
                            , clsCsvRec _
                            , intRowNo _
                            , dtSyoriDate _
                            )

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        commandTextSb.Append(" INSERT INTO ")
        commandTextSb.Append("        " & TEMP_CSV_TABLE)
        commandTextSb.Append("       (kbn ")
        commandTextSb.Append("       ,hosyousyo_no ")
        commandTextSb.Append("       ,bunrui_cd ")
        commandTextSb.Append("       ,gamen_hyouji_no ")
        commandTextSb.Append("       ,zeikomi_nyuukin_gaku ")
        commandTextSb.Append("       ,zeikomi_henkin_gaku ")
        commandTextSb.Append("       ,saisyuu_nyuukin_date ")
        commandTextSb.Append("       ,edi_jouhou_sakusei_date ")
        commandTextSb.Append("       ,gyou_no ")
        commandTextSb.Append("       ,syori_datetime ")
        commandTextSb.Append("       ,group_cd ")
        commandTextSb.Append("       ,kokyaku_cd ")
        commandTextSb.Append("       ,tekiyou ")
        commandTextSb.Append("       ,nyuukin_gaku ")
        commandTextSb.Append("       ,syouhin_cd ")
        commandTextSb.Append("       ) VALUES ( ")
        commandTextSb.Append("        @KBN ")
        commandTextSb.Append("       ,@HOSYOUSYONO ")
        commandTextSb.Append("       ,@BUNRUICD ")
        commandTextSb.Append("       ,@GAMENHYOUJINO ")
        commandTextSb.Append("       ,@ZEIKOMINYUUKINGAKU ")
        commandTextSb.Append("       ,@ZEIKOMIHENKINGAKU ")
        commandTextSb.Append("       ,@SAISYUUNYUUKINDATE ")
        commandTextSb.Append("       ,@EDIJOUHOUSAKUSEIDATE ")
        commandTextSb.Append("       ,@GYOUNO ")
        commandTextSb.Append("       ,@SYORIDATETIME ")
        commandTextSb.Append("       ,@GROUPCD ")
        commandTextSb.Append("       ,@KOKYAKUCD ")
        commandTextSb.Append("       ,@TEKIYOU ")
        commandTextSb.Append("       ,@NYUUKINGAKU ")
        commandTextSb.Append("       ,@SYOUHINCD ")
        commandTextSb.Append("       ) ")

        With clsCsvRec
            ' パラメータへ設定
            cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, .Kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, .HosyousyoNo), _
                SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, .BunruiCd), _
                SQLHelper.MakeParam("@GAMENHYOUJINO", SqlDbType.Int, 4, .GamenHyoujiNo), _
                SQLHelper.MakeParam("@ZEIKOMINYUUKINGAKU", SqlDbType.Int, 4, .NyuukinGaku + .TesuuRyou), _
                SQLHelper.MakeParam("@ZEIKOMIHENKINGAKU", SqlDbType.Int, 4, 0), _
                SQLHelper.MakeParam("@SAISYUUNYUUKINDATE", SqlDbType.DateTime, 16, .NyuukinDate), _
                SQLHelper.MakeParam("@EDIJOUHOUSAKUSEIDATE", SqlDbType.VarChar, 40, .EdiJouhou), _
                SQLHelper.MakeParam("@GYOUNO", SqlDbType.Int, 4, intRowNo), _
                SQLHelper.MakeParam("@SYORIDATETIME", SqlDbType.DateTime, 16, dtSyoriDate), _
                SQLHelper.MakeParam("@GROUPCD", SqlDbType.VarChar, 30, .GroupCd), _
                SQLHelper.MakeParam("@KOKYAKUCD", SqlDbType.VarChar, 30, .KokyakuCd), _
                SQLHelper.MakeParam("@TEKIYOU", SqlDbType.VarChar, 510, .Tekiyou), _
                SQLHelper.MakeParam("@NYUUKINGAKU", SqlDbType.Int, 4, .NyuukinGaku + .TesuuRyou), _
                SQLHelper.MakeParam("@SYOUHINCD", SqlDbType.Char, 8, .SeikyuuMeimoku)}
        End With

        ' クエリ実行
        intResult += ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 取得したCSV情報が邸別請求テーブルに存在するかチェックし、存在しないデータを取得します。
    ''' </summary>
    ''' <returns>邸別入金テーブルを引き当てるKEYを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function ChkExistNyuukinErrInfo() As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkExistNyuukinErrInfo")

        Dim CsvDataSet As New NyuukinCsvDataSet
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      TC.kbn ")
        cmdTextSb.Append("      ,TC.hosyousyo_no ")
        cmdTextSb.Append("      ,TC.bunrui_cd ")
        cmdTextSb.Append("      ,TC.gamen_hyouji_no ")
        cmdTextSb.Append("		,TC.edi_jouhou_sakusei_date ")
        cmdTextSb.Append("		,TC.gyou_no ")
        cmdTextSb.Append("		,TC.syori_datetime ")
        cmdTextSb.Append("		,TC.group_cd ")
        cmdTextSb.Append("		,TC.kokyaku_cd ")
        cmdTextSb.Append("		,TC.tekiyou ")
        cmdTextSb.Append("		,TC.nyuukin_gaku ")
        cmdTextSb.Append("		,ISNULL(TC.zeikomi_nyuukin_gaku,0) AS zeikomi_nyuukin_gaku ")
        cmdTextSb.Append("		,TC.syouhin_cd ")
        cmdTextSb.Append(" FROM " & TEMP_CSV_TABLE & " TC ")
        cmdTextSb.Append(" WHERE NOT EXISTS( ")
        cmdTextSb.Append("      SELECT ")
        cmdTextSb.Append("          TS.kbn ")
        cmdTextSb.Append("          ,TS.hosyousyo_no ")
        cmdTextSb.Append("          ,TS.bunrui_cd ")
        cmdTextSb.Append("          ,TS.gamen_hyouji_no ")
        cmdTextSb.Append("      FROM t_teibetu_seikyuu TS ")
        cmdTextSb.Append("      WHERE ")
        cmdTextSb.Append("          TS.kbn = ISNULL(TC.kbn ,'') ")
        cmdTextSb.Append("      AND ")
        cmdTextSb.Append("          TS.hosyousyo_no = ISNULL(TC.hosyousyo_no ,'') ")
        cmdTextSb.Append("      AND ")
        cmdTextSb.Append("          TS.bunrui_cd = ISNULL(TC.bunrui_cd ,'') ")
        cmdTextSb.Append("      AND ")
        cmdTextSb.Append("          TS.gamen_hyouji_no = ISNULL(TC.gamen_hyouji_no ,'') ")
        cmdTextSb.Append(" ) ")
        cmdTextSb.Append(" ORDER BY TC.kbn,TC.hosyousyo_no,TC.bunrui_cd,TC.gamen_hyouji_no ")

        'データ取得＆返却 
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString)

    End Function

    ''' <summary>
    ''' 入金エラー情報テーブルに登録します
    ''' </summary>
    ''' <param name="row">CSV取り込み情報格納レコード行</param>
    ''' <param name="addLoginUserId">登録ログインユーザー</param>
    ''' <returns>登録件数</returns>
    ''' <remarks></remarks>
    Public Function InsNyuukinError(ByVal row As DataRow, ByVal addLoginUserId As String) As Integer
        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" INSERT INTO ")
        cmdTextSb.Append("     t_nyuukin_error ")
        cmdTextSb.Append("         ( ")
        cmdTextSb.Append("           edi_jouhou_sakusei_date ")
        cmdTextSb.Append("          ,gyou_no ")
        cmdTextSb.Append("          ,syori_datetime ")
        cmdTextSb.Append("          ,group_cd ")
        cmdTextSb.Append("          ,kokyaku_cd ")
        cmdTextSb.Append("          ,tekiyou ")
        cmdTextSb.Append("          ,nyuukin_gaku ")
        cmdTextSb.Append("          ,syouhin_cd ")
        cmdTextSb.Append("          ,add_login_user_id ")
        cmdTextSb.Append("          ,add_datetime ")
        cmdTextSb.Append("         ) VALUES ( ")
        cmdTextSb.Append("           @EDIJOUHOUSAKUSEIDATE ")
        cmdTextSb.Append("          ,@GYOUNO ")
        cmdTextSb.Append("          ,@SYORIDATETIME ")
        cmdTextSb.Append("          ,@GROUPCD ")
        cmdTextSb.Append("          ,@KOKYAKUCD ")
        cmdTextSb.Append("          ,@TEKIYOU ")
        cmdTextSb.Append("          ,@NYUUKINGAKU ")
        cmdTextSb.Append("          ,@SYOUHINCD ")
        cmdTextSb.Append("          ,@ADDLOGINUSERID ")
        cmdTextSb.Append("          ,@ADDDATETIME ")
        cmdTextSb.Append("         ) ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@EDIJOUHOUSAKUSEIDATE", SqlDbType.VarChar, 40, row("edi_jouhou_sakusei_date")), _
                SQLHelper.MakeParam("@GYOUNO", SqlDbType.Int, 4, row("gyou_no")), _
                SQLHelper.MakeParam("@SYORIDATETIME", SqlDbType.DateTime, 16, row("syori_datetime")), _
                SQLHelper.MakeParam("@GROUPCD", SqlDbType.VarChar, 30, row("group_cd")), _
                SQLHelper.MakeParam("@KOKYAKUCD", SqlDbType.VarChar, 30, row("kokyaku_cd")), _
                SQLHelper.MakeParam("@TEKIYOU", SqlDbType.VarChar, 510, row("tekiyou")), _
                SQLHelper.MakeParam("@NYUUKINGAKU", SqlDbType.Int, 4, row("zeikomi_nyuukin_gaku")), _
                SQLHelper.MakeParam("@SYOUHINCD", SqlDbType.VarChar, 8, row("syouhin_cd")), _
                SQLHelper.MakeParam("@ADDLOGINUSERID", SqlDbType.VarChar, 30, addLoginUserId), _
                SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別入金テーブルを更新します
    ''' </summary>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Function DelNyuukinErrorData(ByVal row As DataRow) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DelNyuukinErrorData", row)

        Dim intResult As Integer
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        commandTextSb.Append(" DELETE ")
        commandTextSb.Append(" FROM " & TEMP_CSV_TABLE)
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("  kbn = @KBN ")
        commandTextSb.Append(" AND ")
        commandTextSb.Append("  hosyousyo_no = @HOSYOUSYONO ")
        commandTextSb.Append(" AND ")
        commandTextSb.Append("  bunrui_cd = @BUNRUICD ")
        commandTextSb.Append(" AND ")
        commandTextSb.Append("  gamen_hyouji_no = @GAMENHYOUJINO ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, row("kbn")), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, row("hosyousyo_no")), _
            SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, row("bunrui_cd")), _
            SQLHelper.MakeParam("@GAMENHYOUJINO", SqlDbType.Int, 4, row("gamen_hyouji_no"))}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult
    End Function

    ''' <summary>
    ''' CSV全データ格納用テンポラリテーブルを作成後、合算用テーブルにレコードを登録します
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddTempTableForGassanData() As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".AddTempTableForGassanData")

        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder()

        'テンポラリテーブルの作成
        cmdTextSb.Append(" CREATE TABLE ")
        cmdTextSb.Append("        " & TEMP_SUM_TABLE)
        cmdTextSb.Append("       (kbn                       CHAR(1)     NOT NULL ")
        cmdTextSb.Append("       ,hosyousyo_no              VARCHAR(10) NOT NULL ")
        cmdTextSb.Append("       ,bunrui_cd                 VARCHAR(3) NOT NULL ")
        cmdTextSb.Append("       ,gamen_hyouji_no           INTEGER ")
        cmdTextSb.Append("       ,zeikomi_nyuukin_gaku      INTEGER ")
        cmdTextSb.Append("       ,zeikomi_henkin_gaku       INTEGER ")
        cmdTextSb.Append("       ,saisyuu_nyuukin_date      DATETIME) ")

        cmdTextSb.Append(" INSERT INTO " & TEMP_SUM_TABLE)
        cmdTextSb.Append("       (kbn ")
        cmdTextSb.Append("       ,hosyousyo_no ")
        cmdTextSb.Append("       ,bunrui_cd ")
        cmdTextSb.Append("       ,gamen_hyouji_no ")
        cmdTextSb.Append("       ,zeikomi_nyuukin_gaku ")
        cmdTextSb.Append("       ,zeikomi_henkin_gaku ")
        cmdTextSb.Append("       ,saisyuu_nyuukin_date) ")
        cmdTextSb.Append(" SELECT kbn ")
        cmdTextSb.Append("       ,hosyousyo_no ")
        cmdTextSb.Append("       ,bunrui_cd ")
        cmdTextSb.Append("       ,gamen_hyouji_no ")
        cmdTextSb.Append("       ,SUM(zeikomi_nyuukin_gaku) AS zeikomi_nyuukin_gaku ")
        cmdTextSb.Append("       ,0 ")
        cmdTextSb.Append("       ,MAX(saisyuu_nyuukin_date) AS saisyuu_nyuukin_date ")
        cmdTextSb.Append("   FROM " & TEMP_CSV_TABLE)
        cmdTextSb.Append("  WHERE ")
        cmdTextSb.Append("      bunrui_cd BETWEEN '100' AND '190' ")
        cmdTextSb.Append("  GROUP BY ")
        cmdTextSb.Append("        kbn ")
        cmdTextSb.Append("      , hosyousyo_no ")
        cmdTextSb.Append("      , bunrui_cd ")
        cmdTextSb.Append("      , gamen_hyouji_no ")

        'インデックスの付与
        cmdTextSb.Append(" CREATE CLUSTERED INDEX ix_temp_sum_table " & vbCrLf)
        cmdTextSb.Append(" ON " & TEMP_SUM_TABLE & " ( " & vbCrLf)
        cmdTextSb.Append("                      kbn " & vbCrLf)
        cmdTextSb.Append("                    , hosyousyo_no " & vbCrLf)
        cmdTextSb.Append("                    , bunrui_cd " & vbCrLf)
        cmdTextSb.Append("                    , gamen_hyouji_no) " & vbCrLf)


        ' クエリ実行
        intResult += ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString())
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別入金テーブルを更新します
    ''' </summary>
    ''' <param name="updLoginUserId">更新ログインユーザー</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Function UpdExistTeibetuNyuukinData(ByVal updLoginUserId As String) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdExistTeibetuNyuukinData", updLoginUserId)

        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" UPDATE t_teibetu_nyuukin  ")
        cmdTextSb.Append(" SET ")
        cmdTextSb.Append("   zeikomi_nyuukin_gaku = (  ")
        cmdTextSb.Append("     TN.zeikomi_nyuukin_gaku + TG.zeikomi_nyuukin_gaku ")
        cmdTextSb.Append("   )  ")
        cmdTextSb.Append("   , saisyuu_nyuukin_date = (  ")
        cmdTextSb.Append("     CASE  ")
        cmdTextSb.Append("       WHEN TG.saisyuu_nyuukin_date > TN.saisyuu_nyuukin_date  ")
        cmdTextSb.Append("       THEN TG.saisyuu_nyuukin_date  ")
        cmdTextSb.Append("       ELSE TN.saisyuu_nyuukin_date  ")
        cmdTextSb.Append("       END ")
        cmdTextSb.Append("   )  ")
        cmdTextSb.Append("   , upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("   , upd_datetime = @UPDDATETIME ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("   t_teibetu_nyuukin TN  ")
        cmdTextSb.Append("   INNER JOIN " & TEMP_SUM_TABLE & " TG  ")
        cmdTextSb.Append("     ON TG.kbn = TN.kbn  ")
        cmdTextSb.Append("     AND TG.hosyousyo_no = TN.hosyousyo_no  ")
        cmdTextSb.Append("     AND TG.bunrui_cd = TN.bunrui_cd ")
        cmdTextSb.Append("     AND TG.gamen_hyouji_no = TN.gamen_hyouji_no ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' CSV取込データから邸別入金テーブルに存在しないレコードの取得を行います。
    ''' </summary>
    ''' <param name="blnExists">True:存在する、False:存在しない</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function ChkExistTeibetuNyuukinData(ByVal blnExists As Boolean) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkExistTeibetuNyuukinData", blnExists)

        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      TG.kbn")
        cmdTextSb.Append("      ,TG.hosyousyo_no")
        cmdTextSb.Append("      ,TG.bunrui_cd")
        cmdTextSb.Append("      ,TG.gamen_hyouji_no")
        cmdTextSb.Append("      ,TG.zeikomi_nyuukin_gaku")
        cmdTextSb.Append("      ,TG.zeikomi_henkin_gaku")
        cmdTextSb.Append("      ,TG.saisyuu_nyuukin_date")
        cmdTextSb.Append(" FROM " & TEMP_SUM_TABLE & " TG ")
        If blnExists Then
            cmdTextSb.Append(" WHERE EXISTS( ")
        Else
            cmdTextSb.Append(" WHERE NOT EXISTS( ")
        End If
        cmdTextSb.Append("      SELECT ")
        cmdTextSb.Append("          TN.kbn ")
        cmdTextSb.Append("          ,TN.hosyousyo_no ")
        cmdTextSb.Append("          ,TN.bunrui_cd ")
        cmdTextSb.Append("          ,TN.gamen_hyouji_no ")
        cmdTextSb.Append("      FROM t_teibetu_nyuukin TN ")
        cmdTextSb.Append("      WHERE ")
        cmdTextSb.Append("          TN.kbn = TG.kbn ")
        cmdTextSb.Append("      AND ")
        cmdTextSb.Append("          TN.hosyousyo_no = TG.hosyousyo_no ")
        cmdTextSb.Append("      AND ")
        cmdTextSb.Append("          TN.bunrui_cd = TG.bunrui_cd ")
        cmdTextSb.Append("      AND ")
        cmdTextSb.Append("          TN.gamen_hyouji_no = TG.gamen_hyouji_no ")
        cmdTextSb.Append(" ) ")
        cmdTextSb.Append(" ORDER BY TG.kbn,TG.hosyousyo_no,TG.bunrui_cd,TG.gamen_hyouji_no ")

        'データ取得＆返却 
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString)

    End Function

    ''' <summary>
    ''' 邸別入金テーブルに登録します
    ''' </summary>
    ''' <param name="row">データテーブル行</param>
    ''' <param name="addLoginUserId">登録ログインユーザー</param>
    ''' <returns>登録件数</returns>
    ''' <remarks></remarks>
    Public Function InsTeibetuNyuukinTorikomi( _
                                     ByVal row As DataRow _
                                    , ByVal addLoginUserId As String) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsTeibetuNyuukin" _
                                                    , row _
                                                    , addLoginUserId)
        Dim intResult As Integer
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        commandTextSb.Append(" INSERT INTO ")
        commandTextSb.Append("     t_teibetu_nyuukin ")
        commandTextSb.Append("         (")
        commandTextSb.Append("          kbn ")
        commandTextSb.Append("         ,hosyousyo_no ")
        commandTextSb.Append("         ,bunrui_cd ")
        commandTextSb.Append("         ,gamen_hyouji_no ")
        commandTextSb.Append("         ,zeikomi_nyuukin_gaku ")
        commandTextSb.Append("         ,zeikomi_henkin_gaku ")
        commandTextSb.Append("         ,saisyuu_nyuukin_date ")
        commandTextSb.Append("         ,add_login_user_id ")
        commandTextSb.Append("         ,add_datetime ")
        commandTextSb.Append("         ) VALUES ( ")
        commandTextSb.Append("          @KBN ")
        commandTextSb.Append("         ,@HOSYOUSYONO ")
        commandTextSb.Append("         ,@BUNRUICD ")
        commandTextSb.Append("         ,@GAMENHYOUJINO ")
        commandTextSb.Append("         ,@ZEIKOMINYUUKINGAKU ")
        commandTextSb.Append("         ,@ZEIKOMIHENKINGAKU ")
        commandTextSb.Append("         ,@SAISYUUNYUUKINDATE ")
        commandTextSb.Append("         ,@ADDLOGINUSERID ")
        commandTextSb.Append("         ,@ADDDATETIME ")
        commandTextSb.Append("         ) ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, row("kbn")), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, row("hosyousyo_no")), _
            SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, row("bunrui_cd")), _
            SQLHelper.MakeParam("@GAMENHYOUJINO", SqlDbType.Int, 4, row("gamen_hyouji_no")), _
            SQLHelper.MakeParam("@ZEIKOMINYUUKINGAKU", SqlDbType.Int, 4, row("zeikomi_nyuukin_gaku")), _
            SQLHelper.MakeParam("@ZEIKOMIHENKINGAKU", SqlDbType.Int, 4, 0), _
            SQLHelper.MakeParam("@SAISYUUNYUUKINDATE", SqlDbType.DateTime, 16, row("saisyuu_nyuukin_date")), _
            SQLHelper.MakeParam("@ADDLOGINUSERID", SqlDbType.VarChar, 30, addLoginUserId), _
            SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' CSV情報格納用テンポラリテーブルにレコードを登録します
    ''' </summary>
    ''' <param name="strValue">CSV取り込み情報格納レコード</param>
    ''' <returns>登録件数</returns>
    ''' <remarks></remarks>
    Public Function AddTempTableForTorikomiData(ByVal strValue As String) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".AddTempTableForTorikomiData" _
                                    , strValue)
        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim strParam() As String

        strParam = strValue.Split(","c)

        cmdTextSb.Append(" INSERT INTO ")
        cmdTextSb.Append("        " & TEMP_TABLE)
        cmdTextSb.Append("       (kbn ")
        cmdTextSb.Append("       ,hosyousyo_no ")
        cmdTextSb.Append("       ) VALUES ( ")
        cmdTextSb.Append("        @KBN ")
        cmdTextSb.Append("       ,@HOSYOUSYONO ")
        cmdTextSb.Append("       ) ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, strParam(0)), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, strParam(1))}

        ' クエリ実行
        intResult += ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
    End Function

    ''' <summary>
    ''' 地盤テーブルを更新します
    ''' </summary>
    ''' <param name="updLoginUserId">更新ログインユーザー</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Function UpdJibanTableForNyuukinTorikomi(ByVal updLoginUserId As String)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdTeibetuNyuukin" _
                                                    , updLoginUserId)
        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        Dim strKousinsya As String = strLogic.GetKousinsya(updLoginUserId, DateTime.Now) '更新者

        cmdTextSb.Append(" UPDATE")
        cmdTextSb.Append("      t_jiban")
        cmdTextSb.Append(" SET")
        cmdTextSb.Append("      henkin_syori_flg = 1")
        cmdTextSb.Append("    , henkin_syori_date = @HENKINSYORIDATE")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME")
        cmdTextSb.Append("    , kousinsya = @KOUSINSYA")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      t_jiban TJ")
        cmdTextSb.Append("           INNER JOIN")
        cmdTextSb.Append("               (SELECT")
        cmdTextSb.Append("                     TS.kbn")
        cmdTextSb.Append("                   , TS.hosyousyo_no")
        cmdTextSb.Append("                FROM")
        cmdTextSb.Append("                     " & TEMP_TABLE & " TJ")
        cmdTextSb.Append("                          INNER JOIN t_teibetu_seikyuu TS")
        cmdTextSb.Append("                            ON TJ.kbn = TS.kbn")
        cmdTextSb.Append("                           AND TJ.hosyousyo_no = TS.hosyousyo_no")
        cmdTextSb.Append("                          INNER JOIN t_teibetu_nyuukin TN")
        cmdTextSb.Append("                            ON TS.kbn = TN.kbn")
        cmdTextSb.Append("                           AND TS.hosyousyo_no = TN.hosyousyo_no")
        cmdTextSb.Append("                          LEFT OUTER JOIN m_syouhizei MZ")
        cmdTextSb.Append("                            ON TS.zei_kbn = MZ.zei_kbn")
        cmdTextSb.Append("                WHERE")
        cmdTextSb.Append("                     TS.bunrui_cd = '180'")
        cmdTextSb.Append("                 AND TN.bunrui_cd = '180'")
        cmdTextSb.Append("                 AND ROUND(ISNULL(TS.uri_gaku, 0) + ISNULL(TS.syouhizei_gaku, ISNULL(TS.uri_gaku, 0) *  ISNULL(MZ.zeiritu, 0)), 0, 1)  = (ISNULL(TN.zeikomi_nyuukin_gaku, 0) - ISNULL(TN.zeikomi_henkin_gaku, 0))")
        cmdTextSb.Append("               )")
        cmdTextSb.Append("                WK")
        cmdTextSb.Append("             ON TJ.kbn = WK.kbn")
        cmdTextSb.Append("            AND TJ.hosyousyo_no = WK.hosyousyo_no")
        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@HENKINSYORIDATE", SqlDbType.DateTime, 16, DateTime.Today), _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, DateTime.Now), _
            SQLHelper.MakeParam("@KOUSINSYA", SqlDbType.VarChar, 30, strKousinsya)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 地盤テーブル連携管理テーブルを更新する対象のキーを取得します
    ''' </summary>
    ''' <returns>更新対象のキーを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetJibanRenkeiTargetForNyuukinTorikomi() As DataTable
        Dim jibanRenkeiDataSet As New JibanRenkeiDataSet
        Dim updRenkeiTgtTable As DataTable
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT DISTINCT TJ.kbn ")
        cmdTextSb.Append("       ,TJ.hosyousyo_no ")
        cmdTextSb.Append("       ,JR.renkei_siji_cd ")
        cmdTextSb.Append("       ,JR.sousin_jyky_cd ")
        cmdTextSb.Append("   FROM t_jiban                       TJ ")
        cmdTextSb.Append("  INNER JOIN ")
        cmdTextSb.Append("        " & TEMP_TABLE & "                   WK ")
        cmdTextSb.Append("     ON TJ.kbn             =  WK.kbn ")
        cmdTextSb.Append("    AND TJ.hosyousyo_no    =  WK.hosyousyo_no ")
        cmdTextSb.Append("  INNER JOIN ")
        cmdTextSb.Append("        (SELECT kbn ")
        cmdTextSb.Append("               ,hosyousyo_no")
        cmdTextSb.Append("               ,'100' AS bunrui_cd")
        cmdTextSb.Append("           FROM t_teibetu_nyuukin")
        cmdTextSb.Append("          WHERE (bunrui_cd BETWEEN '100' AND '115')")
        cmdTextSb.Append("              OR (bunrui_cd = '180')")
        cmdTextSb.Append("          GROUP BY kbn")
        cmdTextSb.Append("                  ,hosyousyo_no")
        cmdTextSb.Append("                  ,bunrui_cd")
        cmdTextSb.Append("        ) TN ")
        cmdTextSb.Append("     ON TJ.kbn             =  TN.kbn ")
        cmdTextSb.Append("    AND TJ.hosyousyo_no    =  TN.hosyousyo_no ")
        cmdTextSb.Append("  INNER JOIN  ")
        cmdTextSb.Append("        t_teibetu_seikyuu             TS ")
        cmdTextSb.Append("     ON TJ.kbn             =  TS.kbn ")
        cmdTextSb.Append("    AND TJ.hosyousyo_no    =  TS.hosyousyo_no ")
        cmdTextSb.Append("  INNER JOIN ")
        cmdTextSb.Append("        v_seikyuu_nyuukin_jouhou      VW ")
        cmdTextSb.Append("     ON TJ.kbn             =  VW.kbn ")
        cmdTextSb.Append("    AND TJ.hosyousyo_no    =  VW.hosyousyo_no ")
        cmdTextSb.Append("    AND TN.bunrui_cd       =  VW.bunrui_cd ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        t_jiban_renkei                JR WITH(UPDLOCK)")
        cmdTextSb.Append("     ON TJ.kbn             = JR.kbn ")
        cmdTextSb.Append("    AND TJ.hosyousyo_no    = JR.hosyousyo_no ")
        cmdTextSb.Append("  WHERE TN.bunrui_cd       =  '100' ")
        cmdTextSb.Append("    AND TS.bunrui_cd       =  '180' ")
        cmdTextSb.Append("    AND VW.nyuukin_zangaku =  0 ")

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            jibanRenkeiDataSet, jibanRenkeiDataSet.JibanRenkeiTarget.TableName)
        updRenkeiTgtTable = jibanRenkeiDataSet.JibanRenkeiTarget

        Return updRenkeiTgtTable
    End Function

    ''' <summary>
    ''' アップロード管理テーブルに取込情報を登録します
    ''' </summary>
    ''' <param name="clsCsvRec">CSV取り込み情報格納レコード</param>
    ''' <param name="intErrUmuFlg">エラー有無判断フラグ</param>
    ''' <param name="addLoginUserId">登録ログインユーザー</param>
    ''' <returns>登録件数</returns>
    ''' <remarks></remarks>
    Public Function InsUpdateKanriTable(ByVal clsCsvRec As NyuukinDataCsvRecord, _
                                        ByVal intErrUmuFlg As Integer, _
                                        ByVal addLoginUserId As String) As Integer
        Dim intResult As Integer
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        commandTextSb.Append(" INSERT INTO  ")
        commandTextSb.Append("     t_upload_kanri ")
        commandTextSb.Append("         ( ")
        commandTextSb.Append("           syori_datetime ")
        commandTextSb.Append("          ,nyuuryoku_file_mei ")
        commandTextSb.Append("          ,edi_jouhou_sakusei_date ")
        commandTextSb.Append("          ,error_umu ")
        commandTextSb.Append("          ,add_login_user_id ")
        commandTextSb.Append("          ,add_datetime ")
        commandTextSb.Append("         ) VALUES ( ")
        commandTextSb.Append("           @SYORIDATETIME ")
        commandTextSb.Append("          ,@NYUURYOKUFILEMEI ")
        commandTextSb.Append("          ,@EDIJOUHOUSAKUSEIDATE ")
        commandTextSb.Append("          ,@ERRORUMU ")
        commandTextSb.Append("          ,@ADDLOGINUSERID ")
        commandTextSb.Append("          ,@ADDDATETIME ")
        commandTextSb.Append("         ) ")

        With clsCsvRec
            ' パラメータへ設定
            cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@SYORIDATETIME", SqlDbType.DateTime, 16, .TorikomiDate), _
                SQLHelper.MakeParam("@NYUURYOKUFILEMEI", SqlDbType.VarChar, 128, .FileName), _
                SQLHelper.MakeParam("@EDIJOUHOUSAKUSEIDATE", SqlDbType.VarChar, 40, .EdiJouhou), _
                SQLHelper.MakeParam("@ERRORUMU", SqlDbType.Int, 1, intErrUmuFlg), _
                SQLHelper.MakeParam("@ADDLOGINUSERID", SqlDbType.VarChar, 30, addLoginUserId), _
                SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}
        End With

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' テンポラリテーブルを破棄します
    ''' </summary>
    ''' <returns>処理件数</returns>
    ''' <remarks></remarks>
    Public Function dropTempForNyuukinTorikomi() As Integer
        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()

        'SQL文の初期化
        commandTextSb = New StringBuilder
        commandTextSb.Append(" DROP TABLE " & TEMP_TABLE)
        intResult += ExecuteNonQuery(connStr, _
                            CommandType.Text, _
                            commandTextSb.ToString())

        'SQL文の初期化
        commandTextSb = New StringBuilder
        commandTextSb.Append(" DROP TABLE " & TEMP_CSV_TABLE)
        intResult += ExecuteNonQuery(connStr, _
                            CommandType.Text, _
                            commandTextSb.ToString())

        'SQL文の初期化
        commandTextSb = New StringBuilder
        commandTextSb.Append(" DROP TABLE " & TEMP_SUM_TABLE)
        intResult += ExecuteNonQuery(connStr, _
                            CommandType.Text, _
                            commandTextSb.ToString())

        Return intResult
    End Function

#End Region

End Class
