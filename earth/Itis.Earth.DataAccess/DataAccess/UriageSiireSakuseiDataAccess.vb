Imports System.text
Imports System.Data.SqlClient
''' <summary>
''' 売上・仕入データ作成に関する処理を行うデータアクセスクラスです
''' </summary>
''' <remarks></remarks>
Public Class UriageSiireSakuseiDataAccess
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
#End Region

#Region "伝票マスタ操作"
    ''' <summary>
    ''' 伝票NO採番マスタの最終更新日時を取得します
    ''' </summary>
    ''' <param name="intDenpyouType">伝票種別</param>
    ''' <param name="blnLocked">アップデートロック判断フラグ</param>
    ''' <returns>伝票種別を基に取得した伝票マスタのデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetLastUpdDateDenpyouNo(ByVal intDenpyouType As Integer, _
                                    Optional ByVal blnLocked As Boolean = False) As DenpyouDataSet.LastUpdJouhouDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetLastUpdDate" _
                                                    , intDenpyouType _
                                                    , blnLocked)
        Dim DenpyouDataSet As New DenpyouDataSet
        Dim lstUpdJouhouTable As DenpyouDataSet.LastUpdJouhouDataTable
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" SELECT denpyou_syubetu ")
        cmdTextSb.Append("       ,saisyuu_denpyou_no ")
        cmdTextSb.Append("       ,saisyuu_sakusei_datetime ")
        cmdTextSb.Append("   FROM m_denpyou ")
        If blnLocked Then
            cmdTextSb.Append("   WITH(UPDLOCK) ")
        End If
        cmdTextSb.Append("  WHERE RTRIM(LTRIM(denpyou_syubetu)) = RTRIM(LTRIM(@DENPYOUSYUBETU)) ")
        cmdTextSb.Append("  GROUP BY denpyou_syubetu ")
        cmdTextSb.Append("          ,saisyuu_denpyou_no ")
        cmdTextSb.Append("          ,saisyuu_sakusei_datetime ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@DENPYOUSYUBETU", SqlDbType.Char, 2, intDenpyouType.ToString)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            DenpyouDataSet, DenpyouDataSet.LastUpdJouhou.TableName, cmdParams)
        lstUpdJouhouTable = DenpyouDataSet.LastUpdJouhou

        Return lstUpdJouhouTable
    End Function

    ''' <summary>
    ''' 指定された伝票種別の伝票NOを０に初期化します
    ''' </summary>
    ''' <param name="strDenpyouType">伝票種別</param>
    ''' <param name="updLoginUserId">更新ログインユーザー</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Function ResetDenpyouNo(ByVal strDenpyouType As String, _
                                   ByVal updLoginUserId As String, _
                                   ByVal updDatetime As DateTime) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ResetDenpyouNo" _
                                                    , strDenpyouType _
                                                    , updLoginUserId)

        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer

        cmdTextSb.Append(" UPDATE m_denpyou ")
        cmdTextSb.Append("    SET saisyuu_denpyou_no = 0 ")
        cmdTextSb.Append("       ,upd_login_user_id  = @UPDLOGINUSERID ")
        cmdTextSb.Append("       ,upd_datetime       = @UPDDATETIME ")
        cmdTextSb.Append("  WHERE RTRIM(LTRIM(denpyou_syubetu)) = RTRIM(LTRIM(@DENPYOUTYPE))")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@DENPYOUTYPE", SqlDbType.Char, 2, strDenpyouType), _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updDatetime)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' カウントアップ後の伝票NOを更新します
    ''' </summary>
    ''' <param name="intDenpyouType">伝票種別</param>
    ''' <param name="intDenpyouNo">伝票番号</param>
    ''' <param name="updLoginUserId">更新ログインユーザー</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Function UpdateDenpyouNo(ByVal intDenpyouType As Integer, _
                                    ByVal intDenpyouNo As Integer, _
                                    ByVal updLoginUserId As String, _
                                    ByVal updDatetime As DateTime) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateDenpyouNo" _
                                                    , intDenpyouType _
                                                    , intDenpyouNo _
                                                    , updLoginUserId)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer

        cmdTextSb.Append(" UPDATE m_denpyou ")
        cmdTextSb.Append("    SET saisyuu_denpyou_no       = @DENPYOUNO ")
        cmdTextSb.Append("       ,saisyuu_sakusei_datetime = @UPDDATETIME ")
        cmdTextSb.Append("       ,upd_login_user_id        = @UPDLOGINUSERID ")
        cmdTextSb.Append("       ,upd_datetime             = @UPDDATETIME ")
        cmdTextSb.Append("  WHERE RTRIM(LTRIM(denpyou_syubetu)) = RTRIM(LTRIM(@DENPYOUTYPE))")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@DENPYOUTYPE", SqlDbType.Char, 2, intDenpyouType.ToString), _
            SQLHelper.MakeParam("@DENPYOUNO", SqlDbType.Int, 4, intDenpyouNo), _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updDatetime)}
        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function
#End Region

#Region "ダウンロードデータテーブル操作"
    ''' <summary>
    ''' ファイル種別をKEYにしてダウンロードデータテーブルを削除します
    ''' </summary>
    ''' <param name="strFileType">ファイル種別</param>
    ''' <returns>削除件数</returns>
    ''' <remarks></remarks>
    Public Function DeleteDownLoadTable(ByVal strFileType As String) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteDownLoadTable" _
                                                    , strFileType)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer

        If strFileType = String.Empty Then
            Return intResult
            Exit Function
        End If

        cmdTextSb.Append(" DELETE FROM t_download_data ")
        cmdTextSb.Append("  WHERE file_syubetu = @FILETYPE ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@FILETYPE", SqlDbType.Char, 2, strFileType)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' ダウンロードデータをダウンロードデータテーブルに登録します
    ''' </summary>
    ''' <param name="strFileType">ファイル種別</param>
    ''' <param name="intGyouNo">行NO</param>
    ''' <param name="strGyouData">行データ</param>
    ''' <param name="addLoginUserid">登録ログインユーザー</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertDownLoadtable(ByVal strFileType As String, _
                                        ByVal intGyouNo As Integer, _
                                        ByVal strGyouData As String, _
                                        ByVal addLoginUserid As String, _
                                        ByVal updDatetime As DateTime) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertDownLoadtable" _
                                                    , strFileType _
                                                    , intGyouNo _
                                                    , strGyouData _
                                                    , addLoginUserid)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer

        cmdTextSb.Append(" INSERT INTO ")
        cmdTextSb.Append("      t_download_data ")
        cmdTextSb.Append("          ( file_syubetu ")
        cmdTextSb.Append("           ,gyou_no ")
        cmdTextSb.Append("           ,gyou_data ")
        cmdTextSb.Append("           ,add_login_user_id ")
        cmdTextSb.Append("           ,add_datetime ")
        cmdTextSb.Append("           ,last_login_user_id ")
        cmdTextSb.Append("           ,last_datetime ")
        cmdTextSb.Append("          ) VALUES ( ")
        cmdTextSb.Append("            @FILETYPE ")
        cmdTextSb.Append("           ,@GYOUNO ")
        cmdTextSb.Append("           ,@GYOUDATA ")
        cmdTextSb.Append("           ,@ADDLOGINUSERID ")
        cmdTextSb.Append("           ,@ADDDATETIME ")
        cmdTextSb.Append("           ,@ADDLOGINUSERID ")
        cmdTextSb.Append("           ,@ADDDATETIME ")
        cmdTextSb.Append("          ) ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@FILETYPE", SqlDbType.Char, 2, strFileType), _
            SQLHelper.MakeParam("@GYOUNO", SqlDbType.Int, 4, intGyouNo), _
            SQLHelper.MakeParam("@GYOUDATA", SqlDbType.VarChar, 1024, strGyouData), _
            SQLHelper.MakeParam("@ADDLOGINUSERID", SqlDbType.VarChar, 30, addLoginUserid), _
            SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, updDatetime)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' ダウンロードデータを取得します
    ''' </summary>
    ''' <returns>ダウンロードデータを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function SelectDownLoadTable(ByVal strFileType As String) As UriageSiireCsvDataSet.DownLoadTableDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SelectDownLoadTable" _
                                                    , strFileType)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim DlDataSet As New UriageSiireCsvDataSet
        Dim DlTable As UriageSiireCsvDataSet.DownLoadTableDataTable

        cmdTextSb.Append(" SELECT gyou_data ")
        cmdTextSb.Append("   FROM t_download_data ")
        cmdTextSb.Append("  WHERE RTRIM(LTRIM(file_syubetu)) = RTRIM(LTRIM(@FILETYPE)) ")
        cmdTextSb.Append("  ORDER BY ")
        cmdTextSb.Append("        gyou_no ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@FILETYPE", SqlDbType.Char, 2, strFileType)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            DlDataSet, DlDataSet.DownLoadTable.TableName, cmdParams)
        DlTable = DlDataSet.DownLoadTable

        Return DlTable
    End Function
#End Region

#Region "データ作成処理"
    ''' <summary>
    ''' 邸別調査売上CSV出力用データを取得します
    ''' </summary>
    ''' <param name="dtFrom">売上年月日From</param>
    ''' <param name="dtTo">売上年月日To</param>
    ''' <param name="intDenpyouNo">伝票番号</param>
    ''' <returns>邸別調査売上CSV出力用データを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetUriageTyousaOutPutData(ByVal dtFrom As DateTime, _
                                                ByVal dtTo As DateTime, _
                                                ByVal intDenpyouNo As Integer) As UriageSiireCsvDataSet.UriageCsvTableDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuTyousaOutPutData" _
                                                    , dtFrom _
                                                    , dtTo _
                                                    , intDenpyouNo)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim csvDataSet As New UriageSiireCsvDataSet
        Dim csvTable As UriageSiireCsvDataSet.UriageCsvTableDataTable
        Dim denpyouNoCol As New DataColumn

        '伝票NOの設定
        csvDataSet.UriageCsvTable.denpyou_noColumn.AutoIncrementSeed = intDenpyouNo + 1

        cmdTextSb.Append(" SELECT * ")
        cmdTextSb.Append("       ,suuryou * tanka AS uriage_kingaku  ")           '売上金額------- 売上金額
        cmdTextSb.Append("   FROM( ")
        cmdTextSb.Append("         SELECT '0' AS denku ")                         '伝区----------- 0（固定）
        cmdTextSb.Append("               ,TS.uri_date ")                          '売上年月日----- 売上年月日
        cmdTextSb.Append("               ,TS.seikyuusyo_hak_date ")               '請求発行日----- 請求書発行日
        cmdTextSb.Append("               ,MK.tys_seikyuu_saki ")                  '得意先ｺｰﾄﾞ----- 調査請求先
        cmdTextSb.Append("               ,MKT.kameiten_mei1 ")                    '得意先名------- 加盟店名1
        cmdTextSb.Append("               ,'' AS tyokusou_saki_cd ")               '直送先ｺｰﾄﾞ----- 空白
        cmdTextSb.Append("               ,MK.keiretu_cd ")                        '先方担当者名--- 系列コード
        cmdTextSb.Append("               ,'0' AS bumon_cd ")                      '部門ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,'0' AS tantousya_cd ")                  '担当者ｺｰﾄﾞ----- 0（固定）
        cmdTextSb.Append("               ,'0' AS tekiyou_cd ")                    '摘要ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,TJ.sesyu_mei + '　邸' AS sesyu_mei ")   '摘要名--------- 施主名
        cmdTextSb.Append("               ,'' AS bunrui_cd ")                      '分類ｺｰﾄﾞ------- 空白
        cmdTextSb.Append("               ,'' AS denpyou_kbn ")                    '伝票区分------- 空白
        cmdTextSb.Append("               ,TS.syouhin_cd ")                        '商品ｺｰﾄﾞ------- 商品コード
        cmdTextSb.Append("               ,'0' AS masta_kbn ")                     'ﾏｽﾀ区分-------- 0（固定）
        cmdTextSb.Append("               ,MS.syouhin_mei ")                       '品名----------- 商品名
        cmdTextSb.Append("               ,'0' AS ku ")                            '区------------- 0（固定）
        cmdTextSb.Append("               ,'0' AS souko_cd ")                      '倉庫ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,'0' AS iri_suu ")                       '入数----------- 0（固定）
        cmdTextSb.Append("               ,'0' AS hako_suu ")                      '箱数----------- 0（固定）
        cmdTextSb.Append("               ,(CASE TS.bunrui_cd ")
        cmdTextSb.Append("                   WHEN '115' ")
        cmdTextSb.Append("                     THEN -1 ")
        cmdTextSb.Append("                   ELSE   1 ")
        cmdTextSb.Append("                 END) AS suuryou ")                     '数量----------- 数量
        cmdTextSb.Append("               ,MS.tani ")                              '単位----------- 単位
        cmdTextSb.Append("               ,(CASE TS.bunrui_cd ")
        cmdTextSb.Append("                   WHEN '115' ")
        cmdTextSb.Append("                     THEN ABS(TS.uri_gaku) ")
        cmdTextSb.Append("                   ELSE   TS.uri_gaku ")
        cmdTextSb.Append("                 END) AS tanka ")                       '単価----------- 単価
        cmdTextSb.Append("               ,'0'  AS gen_tanka ")                    '原単価--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS genka_gaku ")                   '原価額--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS ara_rieki ")                    '粗利益--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS sotozei_gaku ")                 '外税額--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS utizei_gaku ")                  '内税額--------- 0（固定）
        cmdTextSb.Append("               ,TS.zei_kbn ")                           '税区分--------- 税区分
        cmdTextSb.Append("               ,MS.zeikomi_kbn ")                       '税込区分------- 税込区分
        cmdTextSb.Append("               ,TS.kbn + TS.hosyousyo_no AS bikou ")    '備考----------- 備考
        cmdTextSb.Append("               ,'0' AS hyoujun_kakaku ")                '標準価格------- 0（固定）
        cmdTextSb.Append("               ,'0' AS douji_nyuuka_kbn ")              '同時入荷区分--- 0（固定）
        cmdTextSb.Append("               ,'0' AS bai_tanka ")                     '売単価--------- 0（固定）
        cmdTextSb.Append("               ,'0' AS baika_kingaku ")                 '売価金額------- 0（固定）
        cmdTextSb.Append("               ,'' AS kikaku_kataban ")                 '規格･型番------ 空白
        cmdTextSb.Append("               ,'' AS color ")                          '色------------- 空白
        cmdTextSb.Append("               ,'' AS size ")                           'サイズ--------- 空白
        cmdTextSb.Append("               ,MK.kameiten_cd AS order_key1 ")         'ソートKEY1----- 加盟店コード
        cmdTextSb.Append("               ,TS.hosyousyo_no AS order_key2 ")        'ソートKEY2----- 保証書NO
        cmdTextSb.Append("               ,TS.bunrui_cd AS order_key3 ")           'ソートKEY3----- 分類コード
        cmdTextSb.Append("               ,TS.syouhin_cd AS order_key4 ")          'ソートKEY4----- 商品コード
        cmdTextSb.Append("               ,TS.kbn AS update_key1 ")                '更新KEY1------- 区分
        cmdTextSb.Append("               ,TS.hosyousyo_no AS update_key2 ")       '更新KEY2------- 保証書NO
        cmdTextSb.Append("               ,TS.bunrui_cd AS update_key3 ")          '更新KEY3------- 分類コード
        cmdTextSb.Append("               ,TS.gamen_hyouji_no AS update_key4 ")    '更新KEY4------- 画面表示NO
        cmdTextSb.Append("           FROM t_teibetu_seikyuu TS ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                t_jiban           TJ ")
        cmdTextSb.Append("             ON TS.kbn = TJ.kbn ")
        cmdTextSb.Append("            AND TS.hosyousyo_no = TJ.hosyousyo_no ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_syouhin         MS ")
        cmdTextSb.Append("             ON TS.syouhin_cd = MS.syouhin_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_kameiten        MK ")
        cmdTextSb.Append("             ON TJ.kameiten_cd = MK.kameiten_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_kameiten        MKT ")
        cmdTextSb.Append("             ON MK.tys_seikyuu_saki = MKT.kameiten_cd ")
        cmdTextSb.Append("          WHERE TS.bunrui_cd IN ('100','110','115','120') ")
        cmdTextSb.Append("            AND TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("            AND TS.uri_date <= @URIDATETO ")
        'cmdTextSb.Append("            AND TS.seikyuu_umu = 1 ")
        cmdTextSb.Append(" ) teibetu_tyousa_csv ")
        cmdTextSb.Append("  ORDER BY order_key1 ")
        cmdTextSb.Append("          ,order_key2 ")
        cmdTextSb.Append("          ,order_key3 ")
        cmdTextSb.Append("          ,order_key4 ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            csvDataSet, csvDataSet.UriageCsvTable.TableName, cmdParams)
        csvTable = csvDataSet.UriageCsvTable

        Return csvTable
    End Function

    ''' <summary>
    ''' 邸別工事売上CSV出力用データを取得します
    ''' </summary>
    ''' <param name="dtFrom">売上年月日From</param>
    ''' <param name="dtTo">売上年月日To</param>
    ''' <param name="intDenpyouNo">伝票番号</param>
    ''' <returns>邸別工事売上CSV出力用データを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetUriageKoujiOutPutData(ByVal dtFrom As DateTime, _
                                                ByVal dtTo As DateTime, _
                                                ByVal intDenpyouNo As Integer) As UriageSiireCsvDataSet.UriageCsvTableDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageKoujiOutPutData" _
                                                    , dtFrom _
                                                    , dtTo _
                                                    , intDenpyouNo)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim csvDataSet As New UriageSiireCsvDataSet
        Dim csvTable As UriageSiireCsvDataSet.UriageCsvTableDataTable
        Dim denpyouNoCol As New DataColumn

        '伝票NOの設定
        csvDataSet.UriageCsvTable.denpyou_noColumn.AutoIncrementSeed = intDenpyouNo + 1

        cmdTextSb.Append(" SELECT * ")
        cmdTextSb.Append("       ,suuryou * tanka AS uriage_kingaku  ")           '売上金額------- 売上金額
        cmdTextSb.Append("    FROM( ")
        cmdTextSb.Append("         SELECT '0' AS denku ")                         '伝区----------- 0（固定）
        cmdTextSb.Append("               ,TS.uri_date ")                          '売上年月日----- 売上年月日
        cmdTextSb.Append("               ,TS.seikyuusyo_hak_date ")               '請求発行日----- 請求書発行日
        cmdTextSb.Append("               ,(CASE ")
        cmdTextSb.Append("                   WHEN MKT.koj_seikyuusaki IS NOT NULL ")
        cmdTextSb.Append("                     THEN MKT.koj_seikyuusaki ")
        cmdTextSb.Append("                   WHEN MT.pca_seikyuu_cd  IS NOT NULL ")
        cmdTextSb.Append("                     THEN MT.pca_seikyuu_cd ")
        cmdTextSb.Append("                   WHEN MTT.pca_seikyuu_cd  IS NOT NULL ")
        cmdTextSb.Append("                     THEN MTT.pca_seikyuu_cd ")
        cmdTextSb.Append("                   ELSE NULL ")
        cmdTextSb.Append("                 END) AS tys_seikyuu_saki ")            '得意先ｺｰﾄﾞ----- 調査請求先
        cmdTextSb.Append("               ,(CASE ")
        cmdTextSb.Append("                   WHEN MKTT.kameiten_mei1 IS NOT NULL ")
        cmdTextSb.Append("                     THEN MKTT.kameiten_mei1 ")
        cmdTextSb.Append("                   WHEN MT.tys_kaisya_mei  IS NOT NULL ")
        cmdTextSb.Append("                     THEN MT.tys_kaisya_mei ")
        cmdTextSb.Append("                   WHEN MTT.tys_kaisya_mei  IS NOT NULL ")
        cmdTextSb.Append("                     THEN MTT.tys_kaisya_mei ")
        cmdTextSb.Append("                   ELSE NULL ")
        cmdTextSb.Append("                 END) AS kameiten_mei1 ")               '得意先名------- 加盟店名1
        cmdTextSb.Append("               ,'' AS tyokusou_saki_cd ")               '直送先ｺｰﾄﾞ----- 空白
        cmdTextSb.Append("               ,MK.keiretu_cd ")                        '先方担当者名--- 系列コード
        cmdTextSb.Append("               ,'0' AS bumon_cd ")                      '部門ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,'0' AS tantousya_cd ")                  '担当者ｺｰﾄﾞ----- 0（固定）
        cmdTextSb.Append("               ,'0' AS tekiyou_cd ")                    '摘要ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,TJ.sesyu_mei + '　邸' AS sesyu_mei ")   '摘要名--------- 施主名
        cmdTextSb.Append("               ,'' AS bunrui_cd ")                      '分類ｺｰﾄﾞ------- 空白
        cmdTextSb.Append("               ,'' AS denpyou_kbn ")                    '伝票区分------- 空白
        cmdTextSb.Append("               ,TS.syouhin_cd ")                        '商品ｺｰﾄﾞ------- 商品コード
        cmdTextSb.Append("               ,'0' AS masta_kbn ")                     'ﾏｽﾀ区分-------- 0（固定）
        cmdTextSb.Append("               ,MS.syouhin_mei ")                       '品名----------- 商品名
        cmdTextSb.Append("               ,'0' AS ku ")                            '区------------- 0（固定）
        cmdTextSb.Append("               ,'0' AS souko_cd ")                      '倉庫ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,'0' AS iri_suu ")                       '入数----------- 0（固定）
        cmdTextSb.Append("               ,'0' AS hako_suu ")                      '箱数----------- 0（固定）
        cmdTextSb.Append("               ,'1' AS suuryou ")                       '数量----------- 数量
        cmdTextSb.Append("               ,MS.tani ")                              '単位----------- 単位
        cmdTextSb.Append("               ,TS.uri_gaku AS tanka ")                 '単価----------- 単価
        cmdTextSb.Append("               ,'0'  AS gen_tanka ")                    '原単価--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS genka_gaku ")                   '原価額--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS ara_rieki ")                    '粗利益--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS sotozei_gaku ")                 '外税額--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS utizei_gaku ")                  '内税額--------- 0（固定）
        cmdTextSb.Append("               ,TS.zei_kbn ")                           '税区分--------- 税区分
        cmdTextSb.Append("               ,MS.zeikomi_kbn ")                       '税込区分------- 税込区分
        cmdTextSb.Append("               ,TS.kbn + TS.hosyousyo_no AS bikou ")    '備考----------- 備考
        cmdTextSb.Append("               ,'0' AS hyoujun_kakaku ")                '標準価格------- 0（固定）
        cmdTextSb.Append("               ,'0' AS douji_nyuuka_kbn ")              '同時入荷区分--- 0（固定）
        cmdTextSb.Append("               ,'0' AS bai_tanka ")                     '売単価--------- 0（固定）
        cmdTextSb.Append("               ,'0' AS baika_kingaku ")                 '売価金額------- 0（固定）
        cmdTextSb.Append("               ,'' AS kikaku_kataban ")                 '規格･型番------ 空白
        cmdTextSb.Append("               ,'' AS color ")                          '色------------- 空白
        cmdTextSb.Append("               ,'' AS size ")                           'サイズ--------- 空白
        cmdTextSb.Append("               ,MK.kameiten_cd AS order_key1 ")         'ソートKEY1----- 加盟店コード
        cmdTextSb.Append("               ,TS.hosyousyo_no AS order_key2 ")        'ソートKEY2----- 保証書NO
        cmdTextSb.Append("               ,TS.bunrui_cd AS order_key3 ")           'ソートKEY3----- 分類コード
        cmdTextSb.Append("               ,'' AS order_key4 ")                     'ソートKEY4----- 空白
        cmdTextSb.Append("               ,TS.kbn AS update_key1 ")                '更新KEY1------- 区分
        cmdTextSb.Append("               ,TS.hosyousyo_no AS update_key2 ")       '更新KEY2------- 保証書NO
        cmdTextSb.Append("               ,TS.bunrui_cd AS update_key3 ")          '更新KEY3------- 分類コード
        cmdTextSb.Append("               ,TS.gamen_hyouji_no AS update_key4 ")    '更新KEY4------- 画面表示NO
        cmdTextSb.Append("           FROM t_teibetu_seikyuu TS ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                t_jiban           TJ ")                 '地盤テーブル
        cmdTextSb.Append("             ON TS.kbn = TJ.kbn ")
        cmdTextSb.Append("            AND TS.hosyousyo_no = TJ.hosyousyo_no ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_syouhin         MS ")                 '商品マスタ
        cmdTextSb.Append("             ON TS.syouhin_cd = MS.syouhin_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_kameiten        MK ")                 '加盟店マスタ
        cmdTextSb.Append("             ON TJ.kameiten_cd = MK.kameiten_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_kameiten        MKT ")                '加盟店マスタ（得意先CD取得用）
        cmdTextSb.Append("             ON TJ.kameiten_cd = MKT.kameiten_cd ")
        cmdTextSb.Append("            AND ((TS.bunrui_cd = '130' ")
        cmdTextSb.Append("            AND   ISNULL(TJ.koj_gaisya_seikyuu_umu,0) <> 1) ")
        cmdTextSb.Append("             OR  (TS.bunrui_cd = '140' ")
        cmdTextSb.Append("            AND   ISNULL(TJ.t_koj_kaisya_seikyuu_umu,0) <> 1)) ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_kameiten        MKTT ")                '加盟店マスタ（得意先名取得用）
        cmdTextSb.Append("             ON MKT.koj_seikyuusaki = MKTT.kameiten_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_tyousakaisya    MT ")                  '調査会社マスタ
        cmdTextSb.Append("             ON TJ.koj_gaisya_cd = MT.tys_kaisya_cd ")
        cmdTextSb.Append("            AND TJ.koj_gaisya_jigyousyo_cd = MT.jigyousyo_cd ")
        cmdTextSb.Append("            AND TS.bunrui_cd = '130' ")
        cmdTextSb.Append("            AND TJ.koj_gaisya_seikyuu_umu = 1 ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_tyousakaisya    MTT ")                 '調査会社マスタ
        cmdTextSb.Append("             ON TJ.t_koj_kaisya_cd = MTT.tys_kaisya_cd ")
        cmdTextSb.Append("            AND TJ.t_koj_kaisya_jigyousyo_cd = MTT.jigyousyo_cd ")
        cmdTextSb.Append("            AND TS.bunrui_cd = '140' ")
        cmdTextSb.Append("            AND TJ.t_koj_kaisya_seikyuu_umu = 1 ")
        cmdTextSb.Append("          WHERE TS.bunrui_cd IN ('130','140') ")
        cmdTextSb.Append("            AND TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("            AND TS.uri_date <= @URIDATETO ")
        'cmdTextSb.Append("            AND TS.seikyuu_umu = 1 ")
        cmdTextSb.Append(" ) teibetu_tyousa_csv ")
        cmdTextSb.Append("  ORDER BY order_key1 ")
        cmdTextSb.Append("          ,order_key2 ")
        cmdTextSb.Append("          ,order_key3 ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            csvDataSet, csvDataSet.UriageCsvTable.TableName, cmdParams)
        csvTable = csvDataSet.UriageCsvTable

        Return csvTable
    End Function

    ''' <summary>
    ''' 邸別調査売上CSV出力処理のため、対象データへのロックを取得する
    ''' </summary>
    ''' <param name="dtFrom">売上年月日From</param>
    ''' <param name="dtTo">売上年月日To</param>
    ''' <returns>処理結果</returns>
    ''' <remarks></remarks>
    Public Function GetUriageTyousaOutPutDataLock(ByVal dtFrom As DateTime, _
                                                ByVal dtTo As DateTime) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageTyousaOutPutDataLock" _
                                                    , dtFrom _
                                                    , dtTo)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        'ロック用SQL生成：キー情報をWHERE条件に保持したSQLでないと、トランザクション全体のパフォーマンスが低下するため、分類コード毎に発行
        cmdTextSb.Append(" SELECT kbn, hosyousyo_no, bunrui_cd, gamen_hyouji_no FROM t_teibetu_seikyuu TS WITH(UPDLOCK,ROWLOCK)  ")
        cmdTextSb.Append("          WHERE TS.bunrui_cd = '100' ")
        cmdTextSb.Append("            AND TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("            AND TS.uri_date <= @URIDATETO ")
        'cmdTextSb.Append("            AND TS.seikyuu_umu = 1 ;")
        cmdTextSb.Append(" SELECT kbn, hosyousyo_no, bunrui_cd, gamen_hyouji_no FROM t_teibetu_seikyuu TS WITH(UPDLOCK,ROWLOCK)  ")
        cmdTextSb.Append("          WHERE TS.bunrui_cd = '110' ")
        cmdTextSb.Append("            AND TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("            AND TS.uri_date <= @URIDATETO ")
        'cmdTextSb.Append("            AND TS.seikyuu_umu = 1 ;")
        cmdTextSb.Append(" SELECT kbn, hosyousyo_no, bunrui_cd, gamen_hyouji_no FROM t_teibetu_seikyuu TS WITH(UPDLOCK,ROWLOCK)  ")
        cmdTextSb.Append("          WHERE TS.bunrui_cd = '115' ")
        cmdTextSb.Append("            AND TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("            AND TS.uri_date <= @URIDATETO ")
        'cmdTextSb.Append("            AND TS.seikyuu_umu = 1 ;")
        cmdTextSb.Append(" SELECT kbn, hosyousyo_no, bunrui_cd, gamen_hyouji_no FROM t_teibetu_seikyuu TS WITH(UPDLOCK,ROWLOCK)  ")
        cmdTextSb.Append("          WHERE TS.bunrui_cd = '120' ")
        cmdTextSb.Append("            AND TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("            AND TS.uri_date <= @URIDATETO ")
        'cmdTextSb.Append("            AND TS.seikyuu_umu = 1 ;")
        cmdTextSb.Append(" SELECT kbn, hosyousyo_no, bunrui_cd, gamen_hyouji_no FROM t_teibetu_seikyuu TS WITH(UPDLOCK,ROWLOCK)  ")
        cmdTextSb.Append("          WHERE TS.bunrui_cd = '190' ")
        cmdTextSb.Append("            AND TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("            AND TS.uri_date <= @URIDATETO ")
        'cmdTextSb.Append("            AND TS.seikyuu_umu = 1 ;")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo)}

        ' 検索実行
        ' クエリ実行
        Dim intResult As Integer = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)

        Return intResult
    End Function

    ''' <summary>
    ''' 邸別工事売上CSV出力処理のため、対象データへのロックを取得する
    ''' </summary>
    ''' <param name="dtFrom">売上年月日From</param>
    ''' <param name="dtTo">売上年月日To</param>
    ''' <returns>処理結果</returns>
    ''' <remarks></remarks>
    Public Function GetUriageKoujiOutPutDataLock(ByVal dtFrom As DateTime, _
                                                ByVal dtTo As DateTime) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageKoujiOutPutDataLock" _
                                                    , dtFrom _
                                                    , dtTo)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        'ロック用SQL生成：キー情報をWHERE条件に保持したSQLでないと、トランザクション全体のパフォーマンスが低下するため、分類コード毎に発行
        cmdTextSb.Append(" SELECT kbn, hosyousyo_no, bunrui_cd, gamen_hyouji_no FROM t_teibetu_seikyuu TS WITH(UPDLOCK,ROWLOCK)  ")
        cmdTextSb.Append("          WHERE TS.bunrui_cd = '130' ")
        cmdTextSb.Append("            AND TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("            AND TS.uri_date <= @URIDATETO ")
        'cmdTextSb.Append("            AND TS.seikyuu_umu = 1 ;")
        cmdTextSb.Append(" SELECT kbn, hosyousyo_no, bunrui_cd, gamen_hyouji_no FROM t_teibetu_seikyuu TS WITH(UPDLOCK,ROWLOCK)  ")
        cmdTextSb.Append("          WHERE TS.bunrui_cd = '140' ")
        cmdTextSb.Append("            AND TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("            AND TS.uri_date <= @URIDATETO ")
        'cmdTextSb.Append("            AND TS.seikyuu_umu = 1 ;")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo)}

        ' 検索実行
        ' クエリ実行
        Dim intResult As Integer = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)

        Return intResult
    End Function

    ''' <summary>
    ''' 邸別その他売上CSV出力用データを取得します
    ''' </summary>
    ''' <param name="dtFrom">売上年月日From</param>
    ''' <param name="dtTo">売上年月日To</param>
    ''' <param name="intDenpyouNo">伝票番号</param>
    ''' <returns>邸別その他売上CSV出力用データを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetUriageHokaOutPutData(ByVal dtFrom As DateTime, _
                                                ByVal dtTo As DateTime, _
                                                ByVal intDenpyouNo As Integer) As UriageSiireCsvDataSet.UriageCsvTableDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageHokaOutPutData" _
                                                    , dtFrom _
                                                    , dtTo _
                                                    , intDenpyouNo)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim csvDataSet As New UriageSiireCsvDataSet
        Dim csvTable As UriageSiireCsvDataSet.UriageCsvTableDataTable
        Dim denpyouNoCol As New DataColumn

        '伝票NOの設定
        csvDataSet.UriageCsvTable.denpyou_noColumn.AutoIncrementSeed = intDenpyouNo + 1

        cmdTextSb.Append(" SELECT * ")
        cmdTextSb.Append("       ,suuryou * tanka AS uriage_kingaku ")               '売上金額------- 売上金額
        cmdTextSb.Append("    FROM( ")
        cmdTextSb.Append("         SELECT '0' AS denku ")                            '伝区----------- 0（固定）
        cmdTextSb.Append("               ,TS.uri_date ")                             '売上年月日----- 売上年月日
        cmdTextSb.Append("               ,TS.seikyuusyo_hak_date ")                  '請求発行日----- 請求書発行日
        cmdTextSb.Append("               ,(CASE TS.bunrui_cd ")
        cmdTextSb.Append("                   WHEN '180' ")
        cmdTextSb.Append("                     THEN MK.tys_seikyuu_saki ")
        cmdTextSb.Append("                   ELSE MK.hansokuhin_seikyuusaki ")
        cmdTextSb.Append("                 END) AS tys_seikyuu_saki ")               '得意先ｺｰﾄﾞ----- 調査請求先
        cmdTextSb.Append("               ,(CASE TS.bunrui_cd ")
        cmdTextSb.Append("                   WHEN '180' ")
        cmdTextSb.Append("                     THEN MKT.kameiten_mei1 ")
        cmdTextSb.Append("                   ELSE MKTT.kameiten_mei1 ")
        cmdTextSb.Append("                 END) AS kameiten_mei1 ")                  '得意先名------- 加盟店名1
        cmdTextSb.Append("               ,'' AS tyokusou_saki_cd ")                  '直送先ｺｰﾄﾞ----- 空白
        cmdTextSb.Append("               ,MK.keiretu_cd ")                           '先方担当者名--- 系列コード
        cmdTextSb.Append("               ,'0' AS bumon_cd ")                         '部門ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,'0' AS tantousya_cd ")                     '担当者ｺｰﾄﾞ----- 0（固定）
        cmdTextSb.Append("               ,'0' AS tekiyou_cd ")                       '摘要ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,TJ.sesyu_mei + '　邸' AS sesyu_mei ")      '摘要名--------- 施主名
        cmdTextSb.Append("               ,'' AS bunrui_cd ")                         '分類ｺｰﾄﾞ------- 空白
        cmdTextSb.Append("               ,'' AS denpyou_kbn ")                       '伝票区分------- 空白
        cmdTextSb.Append("               ,TS.syouhin_cd ")                           '商品ｺｰﾄﾞ------- 商品コード
        cmdTextSb.Append("               ,'0' AS masta_kbn ")                        'ﾏｽﾀ区分-------- 0（固定）
        cmdTextSb.Append("               ,MS.syouhin_mei ")                          '品名----------- 商品名
        cmdTextSb.Append("               ,'0' AS ku ")                               '区------------- 0（固定）
        cmdTextSb.Append("               ,'0' AS souko_cd ")                         '倉庫ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,'0' AS iri_suu ")                          '入数----------- 0（固定）
        cmdTextSb.Append("               ,'0' AS hako_suu ")                         '箱数----------- 0（固定）
        cmdTextSb.Append("               ,(CASE TS.bunrui_cd ")
        cmdTextSb.Append("                   WHEN '180' ")
        cmdTextSb.Append("                     THEN -1 ")
        cmdTextSb.Append("                   ELSE   1 ")
        cmdTextSb.Append("                 END) AS suuryou ")                        '数量----------- 数量
        cmdTextSb.Append("               ,MS.tani ")                                 '単位----------- 単位
        cmdTextSb.Append("               ,(CASE TS.bunrui_cd ")
        cmdTextSb.Append("                   WHEN '180' ")
        cmdTextSb.Append("                     THEN ABS(TS.uri_gaku) ")
        cmdTextSb.Append("                   ELSE   TS.uri_gaku ")
        cmdTextSb.Append("                 END) AS tanka ")                          '単価----------- 単価
        cmdTextSb.Append("               ,'0'  AS gen_tanka ")                       '原単価--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS genka_gaku ")                      '原価額--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS ara_rieki ")                       '粗利益--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS sotozei_gaku ")                    '外税額--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS utizei_gaku ")                     '内税額--------- 0（固定）
        cmdTextSb.Append("               ,TS.zei_kbn ")                              '税区分--------- 税区分
        cmdTextSb.Append("               ,MS.zeikomi_kbn ")                          '税込区分------- 税込区分
        cmdTextSb.Append("               ,TS.kbn + TS.hosyousyo_no AS bikou ")       '備考----------- 備考
        cmdTextSb.Append("               ,'0' AS hyoujun_kakaku ")                   '標準価格------- 0（固定）
        cmdTextSb.Append("               ,'0' AS douji_nyuuka_kbn ")                 '同時入荷区分--- 0（固定）
        cmdTextSb.Append("               ,'0' AS bai_tanka ")                        '売単価--------- 0（固定）
        cmdTextSb.Append("               ,'0' AS baika_kingaku ")                    '売価金額------- 0（固定）
        cmdTextSb.Append("               ,'' AS kikaku_kataban ")                    '規格･型番------ 空白
        cmdTextSb.Append("               ,'' AS color ")                             '色------------- 空白
        cmdTextSb.Append("               ,'' AS size ")                              'サイズ--------- 空白
        cmdTextSb.Append("               ,MK.kameiten_cd AS order_key1 ")            'ソートKEY1----- 加盟店コード
        cmdTextSb.Append("               ,TS.hosyousyo_no AS order_key2 ")           'ソートKEY2----- 保証書NO
        cmdTextSb.Append("               ,TS.bunrui_cd AS order_key3 ")              'ソートKEY3----- 分類コード
        cmdTextSb.Append("               ,'' AS order_key4 ")                        'ソートKEY4----- 空白
        cmdTextSb.Append("               ,TS.kbn AS update_key1 ")                   '更新KEY1------- 区分
        cmdTextSb.Append("               ,TS.hosyousyo_no AS update_key2 ")          '更新KEY2------- 保証書NO
        cmdTextSb.Append("               ,TS.bunrui_cd AS update_key3 ")             '更新KEY3------- 分類コード
        cmdTextSb.Append("               ,TS.gamen_hyouji_no AS update_key4 ")    '更新KEY4------- 画面表示NO
        cmdTextSb.Append("           FROM t_teibetu_seikyuu TS WITH(UPDLOCK) ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                t_jiban           TJ ")
        cmdTextSb.Append("             ON TS.kbn = TJ.kbn ")
        cmdTextSb.Append("            AND TS.hosyousyo_no = TJ.hosyousyo_no ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_syouhin         MS ")
        cmdTextSb.Append("             ON TS.syouhin_cd = MS.syouhin_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_kameiten        MK ")
        cmdTextSb.Append("             ON TJ.kameiten_cd = MK.kameiten_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_kameiten        MKT ")
        cmdTextSb.Append("             ON MK.tys_seikyuu_saki = MKT.kameiten_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_kameiten        MKTT ")
        cmdTextSb.Append("             ON MK.hansokuhin_seikyuusaki = MKTT.kameiten_cd ")
        cmdTextSb.Append("          WHERE TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("            AND TS.uri_date <= @URIDATETO ")
        cmdTextSb.Append("            AND TS.bunrui_cd IN ('150','160','170','180') ")
        'cmdTextSb.Append("            AND TS.seikyuu_umu = 1 ")
        cmdTextSb.Append(" ) teibetu_tyousa_csv ")
        cmdTextSb.Append("  ORDER BY order_key1 ")
        cmdTextSb.Append("          ,order_key2 ")
        cmdTextSb.Append("          ,order_key3 ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            csvDataSet, csvDataSet.UriageCsvTable.TableName, cmdParams)
        csvTable = csvDataSet.UriageCsvTable

        Return csvTable
    End Function

    ''' <summary>
    ''' 店別売上CSV出力用データを取得します
    ''' </summary>
    ''' <param name="dtFrom">売上年月日From</param>
    ''' <param name="dtTo">売上年月日To</param>
    ''' <param name="intDenpyouNo">伝票番号</param>
    ''' <returns>店別売上CSV出力用データを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetUriageTenbetuOutPutData(ByVal dtFrom As DateTime, _
                                                    ByVal dtTo As DateTime, _
                                                    ByVal intDenpyouNo As Integer) As UriageSiireCsvDataSet.UriageCsvTableDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageTenbetuOutPutData" _
                                                    , dtFrom _
                                                    , dtTo _
                                                    , intDenpyouNo)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim csvDataSet As New UriageSiireCsvDataSet
        Dim csvTable As UriageSiireCsvDataSet.UriageCsvTableDataTable
        Dim denpyouNoCol As New DataColumn

        '伝票NOの設定
        csvDataSet.UriageCsvTable.denpyou_noColumn.AutoIncrementSeed = intDenpyouNo + 1

        cmdTextSb.Append(" SELECT * ")
        cmdTextSb.Append("       ,suuryou * tanka AS uriage_kingaku ")               '売上金額------- 売上金額
        cmdTextSb.Append("   FROM( ")
        cmdTextSb.Append("         SELECT '0' AS denku ")                            '伝区----------- 0（固定）
        cmdTextSb.Append("               ,TS.uri_date ")                             '売上年月日----- 売上年月日
        cmdTextSb.Append("               ,TS.seikyuusyo_hak_date ")                  '請求発行日----- 請求書発行日
        cmdTextSb.Append("               ,MK.tys_seikyuu_saki ")                     '得意先ｺｰﾄﾞ----- 調査請求先
        cmdTextSb.Append("               ,MKT.kameiten_mei1 ")                       '得意先名------- 加盟店名1
        cmdTextSb.Append("               ,'' AS tyokusou_saki_cd ")                  '直送先ｺｰﾄﾞ----- 空白
        cmdTextSb.Append("               ,MK.keiretu_cd ")                           '先方担当者名--- 系列コード
        cmdTextSb.Append("               ,'0' AS bumon_cd ")                         '部門ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,'0' AS tantousya_cd ")                     '担当者ｺｰﾄﾞ----- 0（固定）
        cmdTextSb.Append("               ,'0' AS tekiyou_cd ")                       '摘要ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,'' AS sesyu_mei ")                         '摘要名--------- 空白
        cmdTextSb.Append("               ,'' AS bunrui_cd ")                         '分類ｺｰﾄﾞ------- 空白
        cmdTextSb.Append("               ,'' AS denpyou_kbn ")                       '伝票区分------- 空白
        cmdTextSb.Append("               ,TS.syouhin_cd ")                           '商品ｺｰﾄﾞ------- 商品コード
        cmdTextSb.Append("               ,'0' AS masta_kbn ")                        'ﾏｽﾀ区分-------- 0（固定）
        cmdTextSb.Append("               ,MS.syouhin_mei ")                          '品名----------- 商品名
        cmdTextSb.Append("               ,'0' AS ku ")                               '区------------- 0（固定）
        cmdTextSb.Append("               ,'0' AS souko_cd ")                         '倉庫ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,'0' AS iri_suu ")                          '入数----------- 0（固定）
        cmdTextSb.Append("               ,'0' AS hako_suu ")                         '箱数----------- 0（固定）
        cmdTextSb.Append("               ,'1' AS suuryou ")                          '数量----------- 数量
        cmdTextSb.Append("               ,MS.tani ")                                 '単位----------- 単位
        cmdTextSb.Append("               ,TS.uri_gaku AS tanka ")                    '単価----------- 単価
        cmdTextSb.Append("               ,'0'  AS gen_tanka ")                       '原単価--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS genka_gaku ")                      '原価額--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS ara_rieki ")                       '粗利益--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS sotozei_gaku ")                    '外税額--------- 0（固定）
        cmdTextSb.Append("               ,'0'  AS utizei_gaku ")                     '内税額--------- 0（固定）
        cmdTextSb.Append("               ,TS.zei_kbn ")                              '税区分--------- 税区分
        cmdTextSb.Append("               ,MS.zeikomi_kbn ")                          '税込区分------- 税込区分
        cmdTextSb.Append("               ,'' AS bikou ")                             '備考----------- 空白
        cmdTextSb.Append("               ,'0' AS hyoujun_kakaku ")                   '標準価格------- 0（固定）
        cmdTextSb.Append("               ,'0' AS douji_nyuuka_kbn ")                 '同時入荷区分--- 0（固定）
        cmdTextSb.Append("               ,'0' AS bai_tanka ")                        '売単価--------- 0（固定）
        cmdTextSb.Append("               ,'0' AS baika_kingaku ")                    '売価金額------- 0（固定）
        cmdTextSb.Append("               ,'' AS kikaku_kataban ")                    '規格･型番------ 空白
        cmdTextSb.Append("               ,'' AS color ")                             '色------------- 空白
        cmdTextSb.Append("               ,'' AS size ")                              'サイズ--------- 空白
        cmdTextSb.Append("               ,MK.tys_seikyuu_saki AS order_key1 ")       'ソートKEY1----- 得意先コード
        cmdTextSb.Append("               ,TS.bunrui_cd AS order_key2 ")              'ソートKEY2----- 分類コード
        cmdTextSb.Append("               ,'' AS order_key3 ")                        'ソートKEY3----- 空白
        cmdTextSb.Append("               ,'' AS order_key4 ")                        'ソートKEY4----- 空白
        cmdTextSb.Append("               ,TS.mise_cd AS update_key1 ")               '更新KEY1------- 店コード
        cmdTextSb.Append("               ,TS.bunrui_cd AS update_key2 ")             '更新KEY2------- 分類コード
        cmdTextSb.Append("               ,NULL AS update_key3 ")                     '更新KEY3------- 空白
        cmdTextSb.Append("               ,NULL AS update_key4 ")                     '更新KEY4------- 空白
        cmdTextSb.Append("           FROM t_tenbetu_syoki_seikyuu TS WITH(UPDLOCK) ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_syouhin         MS ")
        cmdTextSb.Append("             ON TS.syouhin_cd = MS.syouhin_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_kameiten        MK ")
        cmdTextSb.Append("             ON TS.mise_cd = MK.kameiten_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_kameiten        MKT ")
        cmdTextSb.Append("             ON MK.tys_seikyuu_saki = MKT.kameiten_cd ")
        cmdTextSb.Append("          WHERE TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("            AND TS.uri_date <= @URIDATETO ")
        cmdTextSb.Append("            AND TS.bunrui_cd IN ('200','210') ")
        'cmdTextSb.Append("            AND TS.seikyuu_umu = 1 ")
        cmdTextSb.Append(" ) teibetu_tyousa_csv ")
        cmdTextSb.Append(" UNION ALL ")
        cmdTextSb.Append(" SELECT * ")
        cmdTextSb.Append("       ,suuryou * tanka AS uriage_kingaku ")               '売上金額------- 売上金額
        cmdTextSb.Append("   FROM( ")
        cmdTextSb.Append("         SELECT '0' AS denku ")                            '伝区----------- 0（固定）
        cmdTextSb.Append("               ,TS.uri_date ")                             '売上年月日----- 売上年月日
        cmdTextSb.Append("               ,TS.seikyuusyo_hak_date ")                  '請求発行日----- 請求書発行日
        cmdTextSb.Append("               ,(CASE TS.bunrui_cd ")
        cmdTextSb.Append("                   WHEN '220' ")
        cmdTextSb.Append("                     THEN MK.hansokuhin_seikyuusaki ")
        cmdTextSb.Append("                   ELSE ME.pca_seikyuu_cd ")
        cmdTextSb.Append("                 END) AS tys_seikyuu_saki ")               '得意先ｺｰﾄﾞ----- 調査請求先
        cmdTextSb.Append("               ,(CASE TS.bunrui_cd ")
        cmdTextSb.Append("                   WHEN '220' ")
        cmdTextSb.Append("                     THEN MKT.kameiten_mei1 ")
        cmdTextSb.Append("                   ELSE ME.eigyousyo_mei ")
        cmdTextSb.Append("                 END) AS kameiten_mei1 ")                  '得意先名------- 加盟店名1
        cmdTextSb.Append("               ,'' AS tyokusou_saki_cd ")                  '直送先ｺｰﾄﾞ----- 空白
        cmdTextSb.Append("               ,(CASE TS.bunrui_cd ")
        cmdTextSb.Append("                   WHEN '220' ")
        cmdTextSb.Append("                     THEN MK.keiretu_cd ")
        cmdTextSb.Append("                   ELSE ME.pca_seikyuu_cd ")
        cmdTextSb.Append("                 END) AS keiretu_cd ")                     '先方担当者名--- 系列コード
        cmdTextSb.Append("               ,'0' AS bumon_cd ")                         '部門ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,'0' AS tantousya_cd ")                     '担当者ｺｰﾄﾞ----- 0（固定）
        cmdTextSb.Append("               ,'0' AS tekiyou_cd ")                       '摘要ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,'' AS sesyu_mei ")                         '摘要名--------- 空白
        cmdTextSb.Append("               ,'' AS bunrui_cd ")                         '分類ｺｰﾄﾞ------- 空白
        cmdTextSb.Append("               ,'' AS denpyou_kbnv ")                      '伝票区分------- 空白
        cmdTextSb.Append("               ,TS.syouhin_cd ")                           '商品ｺｰﾄﾞ------- 商品コード
        cmdTextSb.Append("               ,'0' AS masta_kbn ")                        'ﾏｽﾀ区分-------- 0（固定）
        cmdTextSb.Append("               ,MS.syouhin_mei ")                          '品名----------- 商品名
        cmdTextSb.Append("               ,'0' AS ku ")                               '区------------- 0（固定）
        cmdTextSb.Append("               ,'0' AS souko_cd ")                         '倉庫ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,'0' AS iri_suu ")                          '入数----------- 0（固定）
        cmdTextSb.Append("               ,'0' AS hako_suu ")                         '箱数----------- 0（固定）
        cmdTextSb.Append("               ,TS.suu AS suuryou ")                      '数量----------- 数量
        cmdTextSb.Append("               ,MS.tani ")                                 '単位----------- 単位
        cmdTextSb.Append("               ,TS.tanka AS tanka ")                       '単価----------- 単価
        cmdTextSb.Append("               ,0 AS gen_tanka ")                          '原単価--------- 0（固定）
        cmdTextSb.Append("               ,0 AS genka_gaku ")                         '原価額--------- 0（固定）
        cmdTextSb.Append("               ,0 AS ara_rieki ")                          '粗利益--------- 0（固定）
        cmdTextSb.Append("               ,0 AS sotozei_gaku ")                       '外税額--------- 0（固定）
        cmdTextSb.Append("               ,0 AS utizei_gaku ")                        '内税額--------- 0（固定）
        cmdTextSb.Append("               ,TS.zei_kbn ")                              '税区分--------- 税区分
        cmdTextSb.Append("               ,MS.zeikomi_kbn ")                          '税込区分------- 税込区分
        cmdTextSb.Append("               ,'' AS bikou ")                             '備考----------- 空白
        cmdTextSb.Append("               ,'0' AS hyoujun_kakaku ")                   '標準価格------- 0（固定）
        cmdTextSb.Append("               ,'0' AS douji_nyuuka_kbn ")                 '同時入荷区分--- 0（固定）
        cmdTextSb.Append("               ,'0' AS bai_tanka ")                        '売単価--------- 0（固定）
        cmdTextSb.Append("               ,'0' AS baika_kingaku ")                    '売価金額------- 0（固定）
        cmdTextSb.Append("               ,'' AS kikaku_kataban ")                    '規格･型番------ 空白
        cmdTextSb.Append("               ,'' AS color ")                             '色------------- 空白
        cmdTextSb.Append("               ,'' AS size ")                              'サイズ--------- 空白
        cmdTextSb.Append("               ,(CASE TS.bunrui_cd ")
        cmdTextSb.Append("                   WHEN '220' ")
        cmdTextSb.Append("                     THEN MK.hansokuhin_seikyuusaki ")
        cmdTextSb.Append("                   ELSE ME.pca_seikyuu_cd ")
        cmdTextSb.Append("                 END) AS order_key1 ")       'ソートKEY1----- 得意先コード
        cmdTextSb.Append("               ,TS.bunrui_cd AS order_key2 ")              'ソートKEY2----- 分類コード
        cmdTextSb.Append("               ,TS.nyuuryoku_date AS order_key3 ")         'ソートKEY3----- 入力日
        cmdTextSb.Append("               ,TS.nyuuryoku_date_no AS order_key4 ")      'ソートKEY4----- 入力日NO
        cmdTextSb.Append("               ,TS.mise_cd AS update_key1 ")               '更新KEY1------- 店コード
        cmdTextSb.Append("               ,TS.bunrui_cd AS update_key2 ")             '更新KEY2------- 分類コード
        cmdTextSb.Append("               ,TS.nyuuryoku_date AS update_key3 ")        '更新KEY3------- 入力日
        cmdTextSb.Append("               ,TS.nyuuryoku_date_no AS update_key4 ")     '更新KEY4------- 入力日NO
        cmdTextSb.Append("           FROM t_tenbetu_seikyuu TS WITH(UPDLOCK) ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_syouhin         MS ")
        cmdTextSb.Append("             ON TS.syouhin_cd = MS.syouhin_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_kameiten        MK ")
        cmdTextSb.Append("             ON TS.mise_cd = MK.kameiten_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_kameiten        MKT ")
        cmdTextSb.Append("             ON MK.tys_seikyuu_saki = MKT.kameiten_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_eigyousyo       ME ")
        cmdTextSb.Append("             ON TS.mise_cd = ME.eigyousyo_cd ")
        cmdTextSb.Append("          WHERE TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("            AND TS.uri_date <= @URIDATETO ")
        cmdTextSb.Append("            AND TS.bunrui_cd IN ('220','230') ")
        cmdTextSb.Append(" ) teibetu_tyousa_csv ")
        cmdTextSb.Append("  ORDER BY order_key1 ")
        cmdTextSb.Append("          ,order_key2 ")
        cmdTextSb.Append("          ,order_key3 ")
        cmdTextSb.Append("          ,order_key4 ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            csvDataSet, csvDataSet.UriageCsvTable.TableName, cmdParams)
        csvTable = csvDataSet.UriageCsvTable

        Return csvTable
    End Function

    ''' <summary>
    ''' 邸別仕入調査CSV出力用データを取得します
    ''' </summary>
    ''' <param name="dtFrom">売上年月日From</param>
    ''' <param name="dtTo">売上年月日To</param>
    ''' <param name="intDenpyouNo">伝票番号</param>
    ''' <returns>邸別仕入調査CSV出力用データを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSiireTyousaOutPutData(ByVal dtFrom As DateTime, _
                                                ByVal dtTo As DateTime, _
                                                ByVal intDenpyouNo As Integer) As UriageSiireCsvDataSet.SiireCsvTableDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiireTyousaOutPutData" _
                                                    , dtFrom _
                                                    , dtTo _
                                                    , intDenpyouNo)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim csvDataSet As New UriageSiireCsvDataSet
        Dim csvTable As UriageSiireCsvDataSet.SiireCsvTableDataTable
        Dim denpyouNoCol As New DataColumn

        '伝票NOの設定
        csvDataSet.UriageCsvTable.denpyou_noColumn.AutoIncrementSeed = intDenpyouNo + 1

        cmdTextSb.Append(" SELECT '0' AS nyuuka_houhou ")
        cmdTextSb.Append("       ,'0' AS kamoku_kbn ")
        cmdTextSb.Append("       ,'0' AS denku ")
        cmdTextSb.Append("       ,TS.uri_date AS nyuuka_date ")
        cmdTextSb.Append("       ,TS.uri_date AS seisan_date ")
        cmdTextSb.Append("       ,MT.pca_siiresaki_cd AS siire_saki ")
        cmdTextSb.Append("       ,MT.tys_kaisya_mei AS siire_saki_mei ")
        cmdTextSb.Append("       ,MK.kameiten_mei1 AS keiretu_cd ")
        cmdTextSb.Append("       ,'000' AS bumon_cd ")
        cmdTextSb.Append("       ,'000' AS tantousya_cd ")
        cmdTextSb.Append("       ,'000' AS tekiyou_cd ")
        cmdTextSb.Append("       ,TJ.sesyu_mei AS sesyu_mei ")
        cmdTextSb.Append("       ,TS.syouhin_cd AS syouhin_cd ")
        cmdTextSb.Append("       ,'0' AS masta_kbn ")
        cmdTextSb.Append("       ,MS.syouhin_mei AS syouhin_mei ")
        cmdTextSb.Append("       ,'0' AS ku ")
        cmdTextSb.Append("       ,MS.souko_cd AS souko_cd ")
        cmdTextSb.Append("       ,'0000' AS iri_suu ")
        cmdTextSb.Append("       ,'00000' AS hako_suu ")
        cmdTextSb.Append("       ,'000000001' AS suuryou ")
        cmdTextSb.Append("       ,MS.tani AS tani ")
        cmdTextSb.Append("       ,(CASE TS.bunrui_cd ")
        cmdTextSb.Append("           WHEN '100' ")
        cmdTextSb.Append("             THEN TSS.siire_sum ")
        cmdTextSb.Append("           WHEN '120' ")
        cmdTextSb.Append("             THEN TS.siire_gaku ")
        cmdTextSb.Append("           ELSE NULL ")
        cmdTextSb.Append("         END ")
        cmdTextSb.Append("        ) AS tanka ")
        cmdTextSb.Append("       ,(CASE TS.bunrui_cd ")
        cmdTextSb.Append("           WHEN '100' ")
        cmdTextSb.Append("             THEN TSS.siire_sum ")
        cmdTextSb.Append("           WHEN '120' ")
        cmdTextSb.Append("             THEN TS.siire_gaku ")
        cmdTextSb.Append("           ELSE NULL ")
        cmdTextSb.Append("         END ")
        cmdTextSb.Append("        ) AS kingaku ")
        cmdTextSb.Append("       ,'0000000000' AS sotozei_gaku ")
        cmdTextSb.Append("       ,'0000000000' AS utizei_gaku ")
        cmdTextSb.Append("       ,TS.zei_kbn AS zei_kbn ")
        cmdTextSb.Append("       ,MS.zeikomi_kbn AS zeikomi_kbn ")
        cmdTextSb.Append("       ,TJ.kbn + TJ.hosyousyo_no + ' ' + TJ.kameiten_cd AS bikou ")
        cmdTextSb.Append("       ,'' AS kikaku_kataban ")
        cmdTextSb.Append("       ,'' AS color ")
        cmdTextSb.Append("       ,'' AS size ")
        cmdTextSb.Append("       ,MT.pca_siiresaki_cd AS order_key1 ")
        cmdTextSb.Append("       ,TJ.hosyousyo_no AS order_key2 ")
        cmdTextSb.Append("       ,TS.syouhin_cd AS order_key3 ")
        cmdTextSb.Append("       ,TS.kbn AS update_key1 ")
        cmdTextSb.Append("       ,TS.hosyousyo_no AS update_key2 ")
        cmdTextSb.Append("       ,TS.bunrui_cd AS update_key3 ")
        cmdTextSb.Append("       ,TS.gamen_hyouji_no AS update_key4 ")
        cmdTextSb.Append("   FROM t_teibetu_seikyuu TS WITH(UPDLOCK) ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        t_jiban           TJ ")
        cmdTextSb.Append("     ON TS.kbn = TJ.kbn ")
        cmdTextSb.Append("    AND TS.hosyousyo_no = TJ.hosyousyo_no ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        m_syouhin         MS ")
        cmdTextSb.Append("     ON TS.syouhin_cd = MS.syouhin_cd ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        m_kameiten        MK ")
        cmdTextSb.Append("     ON TJ.kameiten_cd = MK.kameiten_cd ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        m_tyousakaisya    MT ")
        cmdTextSb.Append("     ON TJ.tys_kaisya_cd = MT.tys_kaisya_cd ")
        cmdTextSb.Append("    AND TJ.tys_kaisya_jigyousyo_cd = MT.jigyousyo_cd ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        (SELECT ts.kbn ")
        cmdTextSb.Append("               ,ts.hosyousyo_no ")
        cmdTextSb.Append("               ,SUM(ts.siire_gaku) AS siire_sum ")
        cmdTextSb.Append("           FROM t_teibetu_seikyuu ts ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                t_jiban tj ")
        cmdTextSb.Append("             ON ts.kbn = tj.kbn ")
        cmdTextSb.Append("            AND ts.hosyousyo_no = tj.hosyousyo_no ")
        cmdTextSb.Append("          WHERE ts.bunrui_cd in ('100','110','115') ")
        cmdTextSb.Append("            AND tj.data_haki_syubetu = '0' ")
        cmdTextSb.Append("          GROUP BY ")
        cmdTextSb.Append("                ts.kbn ")
        cmdTextSb.Append("               ,ts.hosyousyo_no ")
        cmdTextSb.Append("         HAVING SUM(ts.siire_gaku) <> 0 ")
        cmdTextSb.Append("        ) TSS ")
        cmdTextSb.Append("     ON TS.kbn = TSS.kbn ")
        cmdTextSb.Append("    AND TS.hosyousyo_no = TSS.hosyousyo_no ")
        cmdTextSb.Append("  WHERE TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("    AND TS.uri_date <= @URIDATETO ")
        cmdTextSb.Append("    AND TS.bunrui_cd IN ('100','120') ")
        cmdTextSb.Append("    AND ((TS.bunrui_cd = '100' ")
        cmdTextSb.Append("    AND   TSS.siire_sum <> 0) ")
        cmdTextSb.Append("     OR  (TS.bunrui_cd = '120' ")
        cmdTextSb.Append("    AND   TS.siire_gaku <> 0)) ")
        cmdTextSb.Append("    AND TJ.data_haki_syubetu = '0' ")
        cmdTextSb.Append("  ORDER BY ")
        cmdTextSb.Append("        MT.pca_siiresaki_cd ")
        cmdTextSb.Append("       ,TJ.hosyousyo_no ")
        cmdTextSb.Append("       ,TS.syouhin_cd ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            csvDataSet, csvDataSet.SiireCsvTable.TableName, cmdParams)
        csvTable = csvDataSet.SiireCsvTable

        Return csvTable
    End Function

    ''' <summary>
    ''' 邸別仕入工事CSV出力用データを取得します
    ''' </summary>
    ''' <param name="dtFrom">売上年月日From</param>
    ''' <param name="dtTo">売上年月日To</param>
    ''' <param name="intDenpyouNo">伝票番号</param>
    ''' <returns>邸別仕入工事CSV出力用データを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSiireKoujiOutPutdata(ByVal dtFrom As DateTime, _
                                                ByVal dtTo As DateTime, _
                                                ByVal intDenpyouNo As Integer) As UriageSiireCsvDataSet.SiireCsvTableDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiireTyousaOutPutData" _
                                                    , dtFrom _
                                                    , dtTo _
                                                    , intDenpyouNo)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim csvDataSet As New UriageSiireCsvDataSet
        Dim csvTable As UriageSiireCsvDataSet.SiireCsvTableDataTable
        Dim denpyouNoCol As New DataColumn

        '伝票NOの設定
        csvDataSet.UriageCsvTable.denpyou_noColumn.AutoIncrementSeed = intDenpyouNo + 1

        cmdTextSb.Append(" SELECT '0' AS nyuuka_houhou ")
        cmdTextSb.Append("       ,'0' AS kamoku_kbn ")
        cmdTextSb.Append("       ,'0' AS denku ")
        cmdTextSb.Append("       ,TS.uri_date AS nyuuka_date ")
        cmdTextSb.Append("       ,TS.uri_date AS seisan_date ")
        cmdTextSb.Append("       ,(CASE TS.bunrui_cd  ")
        cmdTextSb.Append("           WHEN '130' ")
        cmdTextSb.Append("             THEN MT.pca_siiresaki_cd ")
        cmdTextSb.Append("           WHEN '140' ")
        cmdTextSb.Append("             THEN MTT.pca_siiresaki_cd ")
        cmdTextSb.Append("           ELSE NULL ")
        cmdTextSb.Append("         END ")
        cmdTextSb.Append("        ) AS siire_saki ")
        cmdTextSb.Append("       ,(CASE TS.bunrui_cd  ")
        cmdTextSb.Append("           WHEN '130' ")
        cmdTextSb.Append("             THEN MT.tys_kaisya_mei ")
        cmdTextSb.Append("           WHEN '140' ")
        cmdTextSb.Append("             THEN MTT.tys_kaisya_mei ")
        cmdTextSb.Append("           ELSE NULL ")
        cmdTextSb.Append("         END ")
        cmdTextSb.Append("        ) AS siire_saki_mei ")
        cmdTextSb.Append("       ,MK.kameiten_mei1 AS keiretu_cd ")
        cmdTextSb.Append("       ,'000' AS bumon_cd ")
        cmdTextSb.Append("       ,'000' AS tantousya_cd ")
        cmdTextSb.Append("       ,'000' AS tekiyou_cd ")
        cmdTextSb.Append("       ,TJ.sesyu_mei AS sesyu_mei ")
        cmdTextSb.Append("       ,TS.syouhin_cd AS syouhin_cd ")
        cmdTextSb.Append("       ,'0' AS masta_kbn ")
        cmdTextSb.Append("       ,MS.syouhin_mei AS syouhin_mei ")
        cmdTextSb.Append("       ,'0' AS ku ")
        cmdTextSb.Append("       ,MS.souko_cd AS souko_cd ")
        cmdTextSb.Append("       ,'0000' AS iri_suu ")
        cmdTextSb.Append("       ,'00000' AS hako_suu ")
        cmdTextSb.Append("       ,'000000001' AS suuryou ")
        cmdTextSb.Append("       ,MS.tani AS tani ")
        cmdTextSb.Append("       ,TS.siire_gaku AS tanka ")
        cmdTextSb.Append("       ,TS.siire_gaku AS kingaku ")
        cmdTextSb.Append("       ,'0000000000' AS sotozei_gaku ")
        cmdTextSb.Append("       ,'0000000000' AS utizei_gaku ")
        cmdTextSb.Append("       ,TS.zei_kbn AS zei_kbn ")
        cmdTextSb.Append("       ,MS.zeikomi_kbn AS zeikomi_kbn ")
        cmdTextSb.Append("       ,TJ.kbn + TJ.hosyousyo_no + ' ' + TJ.kameiten_cd AS bikou ")
        cmdTextSb.Append("       ,'' AS kikaku_kataban ")
        cmdTextSb.Append("       ,'' AS color ")
        cmdTextSb.Append("       ,'' AS size ")
        cmdTextSb.Append("       ,MT.pca_siiresaki_cd AS order_key1 ")
        cmdTextSb.Append("       ,TJ.hosyousyo_no AS order_key2 ")
        cmdTextSb.Append("       ,TS.syouhin_cd AS order_key3 ")
        cmdTextSb.Append("       ,TS.kbn AS update_key1 ")
        cmdTextSb.Append("       ,TS.hosyousyo_no AS update_key2 ")
        cmdTextSb.Append("       ,TS.bunrui_cd AS update_key3 ")
        cmdTextSb.Append("       ,TS.gamen_hyouji_no AS update_key4 ")
        cmdTextSb.Append("   FROM t_teibetu_seikyuu TS WITH(UPDLOCK) ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        t_jiban           TJ ")
        cmdTextSb.Append("     ON TS.kbn = TJ.kbn ")
        cmdTextSb.Append("    AND TS.hosyousyo_no = TJ.hosyousyo_no ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        m_syouhin         MS ")
        cmdTextSb.Append("     ON TS.syouhin_cd = MS.syouhin_cd ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        m_kameiten        MK ")
        cmdTextSb.Append("     ON TJ.kameiten_cd = MK.kameiten_cd ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        m_tyousakaisya    MT ")
        cmdTextSb.Append("     ON TJ.koj_gaisya_cd = MT.tys_kaisya_cd ")
        cmdTextSb.Append("    AND TJ.koj_gaisya_jigyousyo_cd = MT.jigyousyo_cd ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        m_tyousakaisya    MTT ")
        cmdTextSb.Append("     ON TJ.t_koj_kaisya_cd = MTT.tys_kaisya_cd ")
        cmdTextSb.Append("    AND TJ.t_koj_kaisya_jigyousyo_cd = MTT.jigyousyo_cd ")
        cmdTextSb.Append("  WHERE TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("    AND TS.uri_date <= @URIDATETO ")
        cmdTextSb.Append("    AND TS.bunrui_cd IN ('130','140') ")
        cmdTextSb.Append("    AND TS.siire_gaku <> 0 ")
        cmdTextSb.Append("    AND TJ.data_haki_syubetu = '0' ")
        cmdTextSb.Append("  ORDER BY ")
        cmdTextSb.Append("        MT.pca_siiresaki_cd ")
        cmdTextSb.Append("       ,TJ.hosyousyo_no ")
        cmdTextSb.Append("       ,TS.syouhin_cd ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            csvDataSet, csvDataSet.SiireCsvTable.TableName, cmdParams)
        csvTable = csvDataSet.SiireCsvTable

        Return csvTable
    End Function

    ''' <summary>
    ''' 邸別請求の更新（売上確定処理／発注書の自動確定処理）を行います
    ''' </summary>
    ''' <param name="dtFrom">売上年月日From</param>
    ''' <param name="dtTo">売上年月日To</param>
    ''' <param name="strBunruiCd">対象分類コード</param>
    ''' <param name="updLoginUserId">更新ログインユーザー</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Function UriageJidouKakuteiForTeibetu(ByVal dtFrom As DateTime, _
                                                 ByVal dtTo As DateTime, _
                                                 ByVal strBunruiCd As String, _
                                                 ByVal updLoginUserId As String, _
                                                 ByVal updDatetime As DateTime) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateTeibetuSeikyuuForUriage" _
                                                    , dtFrom _
                                                    , strBunruiCd _
                                                    , updLoginUserId)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer

        cmdTextSb.Append(" UPDATE")
        cmdTextSb.Append("      t_teibetu_seikyuu")
        cmdTextSb.Append(" SET")
        cmdTextSb.Append("      uri_keijyou_flg = '1'")
        cmdTextSb.Append("    , hattyuusyo_kakutei_flg =(")
        cmdTextSb.Append("           CASE")
        cmdTextSb.Append("                WHEN UPD.hattyuusyo_kakutei_flg = 0")
        cmdTextSb.Append("                 AND UPD.uri_gaku = UPD.hattyuusyo_gaku")
        cmdTextSb.Append("                THEN '1'")
        cmdTextSb.Append("                ELSE UPD.hattyuusyo_kakutei_flg")
        cmdTextSb.Append("           END)")
        cmdTextSb.Append("    , uri_keijyou_date = @URIKEIJOUDATE")
        cmdTextSb.Append("    , seikyuu_saki_cd =(")
        cmdTextSb.Append("           CASE")
        cmdTextSb.Append("                WHEN UPD.seikyuu_saki_cd IS NULL")
        cmdTextSb.Append("                 AND UPD.seikyuu_saki_brc IS NULL")
        cmdTextSb.Append("                 AND UPD.seikyuu_saki_kbn IS NULL")
        cmdTextSb.Append("                THEN T.v_cd")
        cmdTextSb.Append("                ELSE UPD.seikyuu_saki_cd")
        cmdTextSb.Append("           END)")
        cmdTextSb.Append("    , seikyuu_saki_brc =(")
        cmdTextSb.Append("           CASE")
        cmdTextSb.Append("                WHEN UPD.seikyuu_saki_cd IS NULL")
        cmdTextSb.Append("                 AND UPD.seikyuu_saki_brc IS NULL")
        cmdTextSb.Append("                 AND UPD.seikyuu_saki_kbn IS NULL")
        cmdTextSb.Append("                THEN T.v_brc")
        cmdTextSb.Append("                ELSE UPD.seikyuu_saki_brc")
        cmdTextSb.Append("           END)")
        cmdTextSb.Append("    , seikyuu_saki_kbn =(")
        cmdTextSb.Append("           CASE")
        cmdTextSb.Append("                WHEN UPD.seikyuu_saki_cd IS NULL")
        cmdTextSb.Append("                 AND UPD.seikyuu_saki_brc IS NULL")
        cmdTextSb.Append("                 AND UPD.seikyuu_saki_kbn IS NULL")
        cmdTextSb.Append("                THEN T.v_kbn")
        cmdTextSb.Append("                ELSE UPD.seikyuu_saki_kbn")
        cmdTextSb.Append("           END)")
        cmdTextSb.Append("    , tys_kaisya_cd = (")
        cmdTextSb.Append("           CASE ")
        cmdTextSb.Append("               WHEN UPD.tys_kaisya_cd IS NULL ")
        cmdTextSb.Append("                AND UPD.tys_kaisya_jigyousyo_cd IS NULL ")
        cmdTextSb.Append("               THEN T.v_siire_kaisya_cd ")
        cmdTextSb.Append("               ELSE UPD.tys_kaisya_cd ")
        cmdTextSb.Append("           END)")
        cmdTextSb.Append("    , tys_kaisya_jigyousyo_cd = (")
        cmdTextSb.Append("           CASE ")
        cmdTextSb.Append("               WHEN UPD.tys_kaisya_cd IS NULL ")
        cmdTextSb.Append("               AND UPD.tys_kaisya_jigyousyo_cd IS NULL ")
        cmdTextSb.Append("               THEN T.v_siire_jigyousyo_cd ")
        cmdTextSb.Append("               ELSE UPD.tys_kaisya_jigyousyo_cd ")
        cmdTextSb.Append("           END)")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      t_teibetu_seikyuu UPD")
        cmdTextSb.Append("           INNER JOIN")
        cmdTextSb.Append("               (SELECT")
        cmdTextSb.Append("                     T.*")
        cmdTextSb.Append("                   ,")
        cmdTextSb.Append("                     CASE")
        cmdTextSb.Append("                          WHEN T.bunrui_cd='130'")
        cmdTextSb.Append("                          THEN")
        cmdTextSb.Append("                               CASE")
        cmdTextSb.Append("                                    WHEN T.koj_gaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                                    THEN")
        cmdTextSb.Append("                                         CASE")
        cmdTextSb.Append("                                              WHEN T.koj_gaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                               AND T.koj_gaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                              THEN VT.seikyuu_saki_cd")
        cmdTextSb.Append("                                              ELSE ''")
        cmdTextSb.Append("                                         END")
        cmdTextSb.Append("                                    ELSE VK.seikyuu_saki_cd")
        cmdTextSb.Append("                               END")
        cmdTextSb.Append("                          WHEN T.bunrui_cd='140'")
        cmdTextSb.Append("                          THEN")
        cmdTextSb.Append("                               CASE")
        cmdTextSb.Append("                                    WHEN T.t_koj_kaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                                    THEN")
        cmdTextSb.Append("                                         CASE")
        cmdTextSb.Append("                                              WHEN T.t_koj_kaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                               AND T.t_koj_kaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                              THEN VT.seikyuu_saki_cd")
        cmdTextSb.Append("                                              ELSE ''")
        cmdTextSb.Append("                                         END")
        cmdTextSb.Append("                                    ELSE VK.seikyuu_saki_cd")
        cmdTextSb.Append("                               END")
        cmdTextSb.Append("                          ELSE VK.seikyuu_saki_cd")
        cmdTextSb.Append("                     END v_cd")
        cmdTextSb.Append("                   ,")
        cmdTextSb.Append("                     CASE")
        cmdTextSb.Append("                          WHEN T.bunrui_cd='130'")
        cmdTextSb.Append("                          THEN")
        cmdTextSb.Append("                               CASE")
        cmdTextSb.Append("                                    WHEN T.koj_gaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                                    THEN")
        cmdTextSb.Append("                                         CASE")
        cmdTextSb.Append("                                              WHEN T.koj_gaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                               AND T.koj_gaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                              THEN VT.seikyuu_saki_brc")
        cmdTextSb.Append("                                              ELSE ''")
        cmdTextSb.Append("                                         END")
        cmdTextSb.Append("                                    ELSE VK.seikyuu_saki_brc")
        cmdTextSb.Append("                               END")
        cmdTextSb.Append("                          WHEN T.bunrui_cd='140'")
        cmdTextSb.Append("                          THEN")
        cmdTextSb.Append("                               CASE")
        cmdTextSb.Append("                                    WHEN T.t_koj_kaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                                    THEN")
        cmdTextSb.Append("                                         CASE")
        cmdTextSb.Append("                                              WHEN T.t_koj_kaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                               AND T.t_koj_kaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                              THEN VT.seikyuu_saki_brc")
        cmdTextSb.Append("                                              ELSE ''")
        cmdTextSb.Append("                                         END")
        cmdTextSb.Append("                                    ELSE VK.seikyuu_saki_brc")
        cmdTextSb.Append("                               END")
        cmdTextSb.Append("                          ELSE VK.seikyuu_saki_brc")
        cmdTextSb.Append("                     END v_brc")
        cmdTextSb.Append("                   ,")
        cmdTextSb.Append("                     CASE")
        cmdTextSb.Append("                          WHEN T.bunrui_cd='130'")
        cmdTextSb.Append("                          THEN")
        cmdTextSb.Append("                               CASE")
        cmdTextSb.Append("                                    WHEN T.koj_gaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                                    THEN")
        cmdTextSb.Append("                                         CASE")
        cmdTextSb.Append("                                              WHEN T.koj_gaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                               AND T.koj_gaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                              THEN VT.seikyuu_saki_kbn")
        cmdTextSb.Append("                                              ELSE ''")
        cmdTextSb.Append("                                         END")
        cmdTextSb.Append("                                    ELSE VK.seikyuu_saki_kbn")
        cmdTextSb.Append("                               END")
        cmdTextSb.Append("                          WHEN T.bunrui_cd='140'")
        cmdTextSb.Append("                          THEN")
        cmdTextSb.Append("                               CASE")
        cmdTextSb.Append("                                    WHEN T.t_koj_kaisya_seikyuu_umu = 1")
        cmdTextSb.Append("                                    THEN")
        cmdTextSb.Append("                                         CASE")
        cmdTextSb.Append("                                              WHEN T.t_koj_kaisya_cd IS NOT NULL")
        cmdTextSb.Append("                                               AND T.t_koj_kaisya_jigyousyo_cd IS NOT NULL")
        cmdTextSb.Append("                                              THEN VT.seikyuu_saki_kbn")
        cmdTextSb.Append("                                              ELSE ''")
        cmdTextSb.Append("                                         END")
        cmdTextSb.Append("                                    ELSE VK.seikyuu_saki_kbn")
        cmdTextSb.Append("                               END")
        cmdTextSb.Append("                          ELSE VK.seikyuu_saki_kbn")
        cmdTextSb.Append("                     END v_kbn")
        cmdTextSb.Append("                  , VT.siire_saki_tys_kaisya_cd AS v_siire_kaisya_cd ")
        cmdTextSb.Append("                  , VT.siire_saki_tys_jigyousyo_cd AS v_siire_jigyousyo_cd ")
        cmdTextSb.Append("                FROM")
        cmdTextSb.Append("                    (SELECT")
        cmdTextSb.Append("                          TT.*")
        cmdTextSb.Append("                        , TJ.kameiten_cd")
        cmdTextSb.Append("                        , TJ.koj_gaisya_seikyuu_umu")
        cmdTextSb.Append("                        , TJ.koj_gaisya_cd")
        cmdTextSb.Append("                        , TJ.koj_gaisya_jigyousyo_cd")
        cmdTextSb.Append("                        , TJ.t_koj_kaisya_seikyuu_umu")
        cmdTextSb.Append("                        , TJ.t_koj_kaisya_cd")
        cmdTextSb.Append("                        , TJ.t_koj_kaisya_jigyousyo_cd")
        cmdTextSb.Append("                     FROM")
        cmdTextSb.Append("                          t_teibetu_seikyuu TT")
        cmdTextSb.Append("                               LEFT OUTER JOIN t_jiban TJ")
        cmdTextSb.Append("                                 ON TT.kbn=TJ.kbn")
        cmdTextSb.Append("                                AND TT.hosyousyo_no=TJ.hosyousyo_no")
        cmdTextSb.Append("                     WHERE")
        cmdTextSb.Append("                          TT.uri_date >= @URIDATEFOROM")
        cmdTextSb.Append("                      AND TT.uri_date <= @URIDATETO")
        cmdTextSb.Append("                      AND TT.bunrui_cd IN(" & strBunruiCd & ")")
        'cmdTextSb.Append("                      AND TT.seikyuu_umu = 1")
        cmdTextSb.Append("                    )")
        cmdTextSb.Append("                     T")
        cmdTextSb.Append("                          LEFT OUTER JOIN v_syouhin_seikyuusaki_kameiten VK")
        cmdTextSb.Append("                            ON VK.syouhin_cd = T.syouhin_cd")
        cmdTextSb.Append("                           AND VK.kameiten_cd = T.kameiten_cd")
        cmdTextSb.Append("                          LEFT OUTER JOIN v_syouhin_siire_seikyuu_saki_jiban_tyskaisya VT")
        cmdTextSb.Append("                            ON VT.kbn = T.kbn")
        cmdTextSb.Append("                           AND VT.hosyousyo_no = T.hosyousyo_no")
        cmdTextSb.Append("                           AND VT.syouhin_cd = T.syouhin_cd")
        cmdTextSb.Append("               ) T")
        cmdTextSb.Append("             ON UPD.kbn=T.kbn")
        cmdTextSb.Append("            AND UPD.hosyousyo_no=T.hosyousyo_no")
        cmdTextSb.Append("            AND UPD.bunrui_cd=T.bunrui_cd")
        cmdTextSb.Append("            AND UPD.gamen_hyouji_no=T.gamen_hyouji_no")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIKEIJOUDATE", SqlDbType.DateTime, 16, DateTime.Today), _
            SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo), _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updDatetime)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 仕入の売上確定処理を行います（調査）
    ''' </summary>
    ''' <param name="dtFrom">売上年月日From</param>
    ''' <param name="dtTo">売上年月日To</param>
    ''' <param name="updLoginUserId">更新ログインユーザー</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Function SiireUriageKakuteiSyoriForTyousa(ByVal dtFrom As DateTime, _
                                                     ByVal dtTo As DateTime, _
                                                     ByVal updLoginUserId As String, _
                                                     ByVal updDatetime As DateTime) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SiireUriageKakuteiSyoriForTyousa" _
                                                    , dtFrom _
                                                    , dtTo _
                                                    , updLoginUserId)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer

        cmdTextSb.Append(" UPDATE t_teibetu_seikyuu")
        cmdTextSb.Append(" SET  uri_keijyou_flg   = '1' ")
        cmdTextSb.Append("    , uri_keijyou_date  = @URIKEIJOUDATE ")
        cmdTextSb.Append("    , seikyuu_saki_cd = (")
        cmdTextSb.Append("      CASE ")
        cmdTextSb.Append("           WHEN TS.seikyuu_saki_cd IS NULL ")
        cmdTextSb.Append("            AND TS.seikyuu_saki_brc IS NULL ")
        cmdTextSb.Append("            AND TS.seikyuu_saki_kbn IS NULL ")
        cmdTextSb.Append("           THEN VK.seikyuu_saki_cd ")
        cmdTextSb.Append("           ELSE TS.seikyuu_saki_cd ")
        cmdTextSb.Append("      END)")
        cmdTextSb.Append("    , seikyuu_saki_brc = (")
        cmdTextSb.Append("      CASE ")
        cmdTextSb.Append("           WHEN TS.seikyuu_saki_cd IS NULL ")
        cmdTextSb.Append("            AND TS.seikyuu_saki_brc IS NULL ")
        cmdTextSb.Append("            AND TS.seikyuu_saki_kbn IS NULL ")
        cmdTextSb.Append("           THEN VK.seikyuu_saki_brc ")
        cmdTextSb.Append("           ELSE TS.seikyuu_saki_brc ")
        cmdTextSb.Append("      END)")
        cmdTextSb.Append("    , seikyuu_saki_kbn = (")
        cmdTextSb.Append("      CASE ")
        cmdTextSb.Append("           WHEN TS.seikyuu_saki_cd IS NULL ")
        cmdTextSb.Append("            AND TS.seikyuu_saki_brc IS NULL ")
        cmdTextSb.Append("            AND TS.seikyuu_saki_kbn IS NULL ")
        cmdTextSb.Append("           THEN VK.seikyuu_saki_kbn ")
        cmdTextSb.Append("           ELSE TS.seikyuu_saki_kbn ")
        cmdTextSb.Append("      END)")
        cmdTextSb.Append("    , tys_kaisya_cd = (")
        cmdTextSb.Append("           CASE ")
        cmdTextSb.Append("               WHEN TS.tys_kaisya_cd IS NULL ")
        cmdTextSb.Append("                AND TS.tys_kaisya_jigyousyo_cd IS NULL ")
        cmdTextSb.Append("               THEN VT.siire_saki_tys_kaisya_cd ")
        cmdTextSb.Append("               ELSE TS.tys_kaisya_cd ")
        cmdTextSb.Append("           END)")
        cmdTextSb.Append("    , tys_kaisya_jigyousyo_cd = (")
        cmdTextSb.Append("           CASE ")
        cmdTextSb.Append("               WHEN TS.tys_kaisya_cd IS NULL ")
        cmdTextSb.Append("               AND TS.tys_kaisya_jigyousyo_cd IS NULL ")
        cmdTextSb.Append("               THEN VT.siire_saki_tys_jigyousyo_cd ")
        cmdTextSb.Append("               ELSE TS.tys_kaisya_jigyousyo_cd ")
        cmdTextSb.Append("           END)")
        cmdTextSb.Append("     , upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("     , upd_datetime      = @UPDDATETIME ")
        cmdTextSb.Append("   FROM t_teibetu_seikyuu TS ")
        cmdTextSb.Append("  INNER JOIN ")
        cmdTextSb.Append("        t_jiban TJ ")
        cmdTextSb.Append("     ON TS.kbn          = TJ.kbn ")
        cmdTextSb.Append("    AND TS.hosyousyo_no = TJ.hosyousyo_no ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        (SELECT ts.kbn ")
        cmdTextSb.Append("               ,ts.hosyousyo_no ")
        cmdTextSb.Append("               ,SUM(ts.siire_gaku) AS siire_sum ")
        cmdTextSb.Append("           FROM t_teibetu_seikyuu ts ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                t_jiban tj ")
        cmdTextSb.Append("             ON ts.kbn = tj.kbn ")
        cmdTextSb.Append("            AND ts.hosyousyo_no = tj.hosyousyo_no ")
        cmdTextSb.Append("          WHERE ts.bunrui_cd in ('100','110','115') ")
        cmdTextSb.Append("            AND tj.data_haki_syubetu = '0' ")
        cmdTextSb.Append("          GROUP BY ")
        cmdTextSb.Append("                ts.kbn ")
        cmdTextSb.Append("               ,ts.hosyousyo_no ")
        cmdTextSb.Append("         HAVING SUM(ts.siire_gaku) <> 0 ")
        cmdTextSb.Append("        ) TSS ")
        cmdTextSb.Append("     ON TS.kbn          = TSS.kbn ")
        cmdTextSb.Append("    AND TS.hosyousyo_no = TSS.hosyousyo_no ")
        cmdTextSb.Append("   LEFT OUTER JOIN v_syouhin_seikyuusaki_kameiten VK ")
        cmdTextSb.Append("     ON VK.syouhin_cd = TS.syouhin_cd ")
        cmdTextSb.Append("     AND VK.kameiten_cd = TJ.kameiten_cd ")
        cmdTextSb.Append("   LEFT OUTER JOIN v_syouhin_siire_seikyuu_saki_jiban_tyskaisya VT ")
        cmdTextSb.Append("     ON VT.kbn = TS.kbn ")
        cmdTextSb.Append("     AND VT.hosyousyo_no = TS.hosyousyo_no ")
        cmdTextSb.Append("     AND VT.syouhin_cd = TS.syouhin_cd ")
        cmdTextSb.Append("  WHERE TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("    AND TS.uri_date <= @URIDATETO ")
        cmdTextSb.Append("    AND TS.bunrui_cd IN ('100','110','115','120','190') ")
        cmdTextSb.Append("    AND ((TS.bunrui_cd IN ('100','110','115') ")
        cmdTextSb.Append("    AND   TSS.siire_sum <> 0) ")
        cmdTextSb.Append("     OR  (TS.bunrui_cd in ('120','190') ")
        cmdTextSb.Append("    AND   TS.siire_gaku <> 0)) ")
        cmdTextSb.Append("    AND TS.seikyuu_umu = 0 ")
        cmdTextSb.Append("    AND TJ.data_haki_syubetu = '0' ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIKEIJOUDATE", SqlDbType.DateTime, 16, DateTime.Today), _
            SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo), _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updDatetime)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別請求テーブル連携管理テーブルを更新する対象のキーを取得します（調査）
    ''' </summary>
    ''' <param name="dtFrom">請求書発行日FROM</param>
    ''' <param name="dtTo">請求書発行日TO</param>
    ''' <returns>更新対象のキーを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuRenkeiTargetForTyousa(ByVal dtFrom As Date _
                                                , ByVal dtTo As Date) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuRenkeiTargetForTyousa" _
                                                    , dtFrom _
                                                    , dtTo)
        Dim teibetuRenkeiDataSet As New TeibetuRenkeiDataSet
        Dim updRenkeiTgtTable As TeibetuRenkeiDataSet.TeibetuRenkeiTargetDataTable
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" SELECT TS.kbn ")
        cmdTextSb.Append("       ,TS.hosyousyo_no ")
        cmdTextSb.Append("       ,TS.bunrui_cd ")
        cmdTextSb.Append("       ,TS.gamen_hyouji_no ")
        cmdTextSb.Append("       ,R.renkei_siji_cd ")
        cmdTextSb.Append("       ,R.sousin_jyky_cd ")
        cmdTextSb.Append("   FROM t_teibetu_seikyuu TS ")
        cmdTextSb.Append("  INNER JOIN ")
        cmdTextSb.Append("        t_jiban TJ ")
        cmdTextSb.Append("     ON TS.kbn          = TJ.kbn ")
        cmdTextSb.Append("    AND TS.hosyousyo_no = TJ.hosyousyo_no ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        (SELECT ts.kbn ")
        cmdTextSb.Append("               ,ts.hosyousyo_no ")
        cmdTextSb.Append("               ,SUM(ts.siire_gaku) AS siire_sum ")
        cmdTextSb.Append("           FROM t_teibetu_seikyuu ts ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                t_jiban tj ")
        cmdTextSb.Append("             ON ts.kbn = tj.kbn ")
        cmdTextSb.Append("            AND ts.hosyousyo_no = tj.hosyousyo_no ")
        cmdTextSb.Append("          WHERE ts.bunrui_cd in ('100','110','115') ")
        cmdTextSb.Append("            AND tj.data_haki_syubetu = '0' ")
        cmdTextSb.Append("          GROUP BY ")
        cmdTextSb.Append("                ts.kbn ")
        cmdTextSb.Append("               ,ts.hosyousyo_no ")
        cmdTextSb.Append("         HAVING SUM(ts.siire_gaku) <> 0 ")
        cmdTextSb.Append("        ) TSS ")
        cmdTextSb.Append("     ON TS.kbn          = TSS.kbn ")
        cmdTextSb.Append("    AND TS.hosyousyo_no = TSS.hosyousyo_no ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        t_teibetu_seikyuu_renkei R WITH(UPDLOCK)")
        cmdTextSb.Append("     ON TS.kbn = R.kbn ")
        cmdTextSb.Append("    AND TS.hosyousyo_no = R.hosyousyo_no ")
        cmdTextSb.Append("    AND TS.bunrui_cd = R.bunrui_cd ")
        cmdTextSb.Append("    AND TS.gamen_hyouji_no = R.gamen_hyouji_no ")
        cmdTextSb.Append("  WHERE TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("    AND TS.uri_date <= @URIDATETO ")
        cmdTextSb.Append("    AND TS.bunrui_cd IN ('100','110','115','120','190') ")
        cmdTextSb.Append("    AND ((TS.bunrui_cd IN ('100','110','115') ")
        cmdTextSb.Append("    AND   TSS.siire_sum <> 0) ")
        cmdTextSb.Append("     OR  (TS.bunrui_cd in ('120','190') ")
        cmdTextSb.Append("    AND   TS.siire_gaku <> 0)) ")
        cmdTextSb.Append("    AND TS.seikyuu_umu = 0 ")
        cmdTextSb.Append("    AND TJ.data_haki_syubetu = '0' ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
        SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            teibetuRenkeiDataSet, teibetuRenkeiDataSet.TeibetuRenkeiTarget.TableName, cmdParams)
        updRenkeiTgtTable = teibetuRenkeiDataSet.TeibetuRenkeiTarget

        Return updRenkeiTgtTable
    End Function

    ''' <summary>
    ''' 仕入の売上確定処理を行います（工事）
    ''' </summary>
    ''' <param name="dtFrom">売上年月日From</param>
    ''' <param name="dtTo">売上年月日To</param>
    ''' <param name="updLoginUserId">更新ログインユーザー</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Function SiireUriageKakuteiSyoriForKouji(ByVal dtFrom As DateTime, _
                                                    ByVal dtTo As DateTime, _
                                                    ByVal updLoginUserId As String, _
                                                    ByVal updDatetime As DateTime) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SiireUriageKakuteiSyori" _
                                                    , dtFrom _
                                                    , dtTo _
                                                    , updLoginUserId)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer

        cmdTextSb.Append(" UPDATE t_teibetu_seikyuu ")
        cmdTextSb.Append(" SET uri_keijyou_flg   = '1' ")
        cmdTextSb.Append("    ,uri_keijyou_date  = @URIKEIJOUDATE ")
        cmdTextSb.Append("    , seikyuu_saki_cd = (")
        cmdTextSb.Append("      CASE ")
        cmdTextSb.Append("           WHEN TS.seikyuu_saki_cd IS NOT NULL ")
        cmdTextSb.Append("            AND TS.seikyuu_saki_brc IS NOT NULL ")
        cmdTextSb.Append("            AND TS.seikyuu_saki_kbn IS NOT NULL ")
        cmdTextSb.Append("           THEN TS.seikyuu_saki_cd ")
        cmdTextSb.Append("           WHEN TS.bunrui_cd = '130' ")
        cmdTextSb.Append("            AND TJ.koj_gaisya_seikyuu_umu = 1  ")
        cmdTextSb.Append("           THEN VT.seikyuu_saki_cd ")
        cmdTextSb.Append("           WHEN TS.bunrui_cd = '140' ")
        cmdTextSb.Append("            AND TJ.t_koj_kaisya_seikyuu_umu = 1  ")
        cmdTextSb.Append("           THEN VT.seikyuu_saki_cd ")
        cmdTextSb.Append("           ELSE VK.seikyuu_saki_cd ")
        cmdTextSb.Append("      END)")
        cmdTextSb.Append("    , seikyuu_saki_brc = (")
        cmdTextSb.Append("      CASE ")
        cmdTextSb.Append("           WHEN TS.seikyuu_saki_cd IS NOT NULL ")
        cmdTextSb.Append("            AND TS.seikyuu_saki_brc IS NOT NULL ")
        cmdTextSb.Append("            AND TS.seikyuu_saki_kbn IS NOT NULL ")
        cmdTextSb.Append("           THEN TS.seikyuu_saki_brc ")
        cmdTextSb.Append("           WHEN TS.bunrui_cd = '130' ")
        cmdTextSb.Append("            AND TJ.koj_gaisya_seikyuu_umu = 1  ")
        cmdTextSb.Append("           THEN VT.seikyuu_saki_brc ")
        cmdTextSb.Append("           WHEN TS.bunrui_cd = '140' ")
        cmdTextSb.Append("            AND TJ.t_koj_kaisya_seikyuu_umu = 1  ")
        cmdTextSb.Append("           THEN VT.seikyuu_saki_brc ")
        cmdTextSb.Append("           ELSE VK.seikyuu_saki_brc ")
        cmdTextSb.Append("      END)")
        cmdTextSb.Append("    , seikyuu_saki_kbn = (")
        cmdTextSb.Append("      CASE ")
        cmdTextSb.Append("           WHEN TS.seikyuu_saki_cd IS NOT NULL ")
        cmdTextSb.Append("            AND TS.seikyuu_saki_brc IS NOT NULL ")
        cmdTextSb.Append("            AND TS.seikyuu_saki_kbn IS NOT NULL ")
        cmdTextSb.Append("           THEN TS.seikyuu_saki_kbn ")
        cmdTextSb.Append("           WHEN TS.bunrui_cd = '130' ")
        cmdTextSb.Append("            AND TJ.koj_gaisya_seikyuu_umu = 1  ")
        cmdTextSb.Append("           THEN VT.seikyuu_saki_kbn ")
        cmdTextSb.Append("           WHEN TS.bunrui_cd = '140' ")
        cmdTextSb.Append("            AND TJ.t_koj_kaisya_seikyuu_umu = 1  ")
        cmdTextSb.Append("           THEN VT.seikyuu_saki_kbn ")
        cmdTextSb.Append("           ELSE VK.seikyuu_saki_kbn ")
        cmdTextSb.Append("      END)")
        cmdTextSb.Append("    , tys_kaisya_cd = (")
        cmdTextSb.Append("           CASE ")
        cmdTextSb.Append("               WHEN TS.tys_kaisya_cd IS NULL ")
        cmdTextSb.Append("                AND TS.tys_kaisya_jigyousyo_cd IS NULL ")
        cmdTextSb.Append("               THEN VT.siire_saki_tys_kaisya_cd ")
        cmdTextSb.Append("               ELSE TS.tys_kaisya_cd ")
        cmdTextSb.Append("           END)")
        cmdTextSb.Append("    , tys_kaisya_jigyousyo_cd = (")
        cmdTextSb.Append("           CASE ")
        cmdTextSb.Append("               WHEN TS.tys_kaisya_cd IS NULL ")
        cmdTextSb.Append("               AND TS.tys_kaisya_jigyousyo_cd IS NULL ")
        cmdTextSb.Append("               THEN VT.siire_saki_tys_jigyousyo_cd ")
        cmdTextSb.Append("               ELSE TS.tys_kaisya_jigyousyo_cd ")
        cmdTextSb.Append("           END)")
        cmdTextSb.Append("     , upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("     , upd_datetime      = @UPDDATETIME ")
        cmdTextSb.Append("   FROM t_teibetu_seikyuu TS ")
        cmdTextSb.Append("  INNER JOIN ")
        cmdTextSb.Append("        t_jiban TJ ")
        cmdTextSb.Append("     ON TS.kbn          = TJ.kbn ")
        cmdTextSb.Append("    AND TS.hosyousyo_no = TJ.hosyousyo_no ")
        cmdTextSb.Append("   LEFT OUTER JOIN v_syouhin_seikyuusaki_kameiten VK ")
        cmdTextSb.Append("     ON VK.syouhin_cd = TS.syouhin_cd ")
        cmdTextSb.Append("     AND VK.kameiten_cd = TJ.kameiten_cd ")
        cmdTextSb.Append("   LEFT OUTER JOIN v_syouhin_siire_seikyuu_saki_jiban_tyskaisya VT ")
        cmdTextSb.Append("     ON VT.kbn = TS.kbn ")
        cmdTextSb.Append("     AND VT.hosyousyo_no = TS.hosyousyo_no ")
        cmdTextSb.Append("     AND VT.syouhin_cd = TS.syouhin_cd ")
        cmdTextSb.Append("  WHERE TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("    AND TS.uri_date <= @URIDATETO ")
        cmdTextSb.Append("    AND TS.bunrui_cd IN ('130','140') ")
        cmdTextSb.Append("    AND TS.siire_gaku  <> 0 ")
        cmdTextSb.Append("    AND TS.seikyuu_umu = 0 ")
        cmdTextSb.Append("    AND TJ.data_haki_syubetu = '0' ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIKEIJOUDATE", SqlDbType.DateTime, 16, DateTime.Today), _
            SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo), _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updDatetime)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別請求テーブル連携管理テーブルを更新する対象のキーを取得します（工事）
    ''' </summary>
    ''' <param name="dtFrom">請求書発行日FROM</param>
    ''' <param name="dtTo">請求書発行日TO</param>
    ''' <returns>更新対象のキーを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuRenkeiTargetForKouji(ByVal dtFrom As Date _
                                                , ByVal dtTo As Date) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuRenkeiTarget" _
                                                    , dtFrom _
                                                    , dtTo)
        Dim teibetuRenkeiDataSet As New TeibetuRenkeiDataSet
        Dim updRenkeiTgtTable As TeibetuRenkeiDataSet.TeibetuRenkeiTargetDataTable
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" SELECT TS.kbn ")
        cmdTextSb.Append("       ,TS.hosyousyo_no ")
        cmdTextSb.Append("       ,TS.bunrui_cd ")
        cmdTextSb.Append("       ,TS.gamen_hyouji_no ")
        cmdTextSb.Append("       ,R.renkei_siji_cd ")
        cmdTextSb.Append("       ,R.sousin_jyky_cd ")
        cmdTextSb.Append("   FROM t_teibetu_seikyuu TS ")
        cmdTextSb.Append("  INNER JOIN ")
        cmdTextSb.Append("        t_jiban TJ ")
        cmdTextSb.Append("     ON TS.kbn          = TJ.kbn ")
        cmdTextSb.Append("    AND TS.hosyousyo_no = TJ.hosyousyo_no ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        t_teibetu_seikyuu_renkei R WITH(UPDLOCK)")
        cmdTextSb.Append("     ON TS.kbn = R.kbn ")
        cmdTextSb.Append("    AND TS.hosyousyo_no = R.hosyousyo_no ")
        cmdTextSb.Append("    AND TS.bunrui_cd = R.bunrui_cd ")
        cmdTextSb.Append("    AND TS.gamen_hyouji_no = R.gamen_hyouji_no ")
        cmdTextSb.Append("  WHERE TS.uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("    AND TS.uri_date <= @URIDATETO ")
        cmdTextSb.Append("    AND TS.bunrui_cd IN ('130','140') ")
        cmdTextSb.Append("    AND TS.siire_gaku  <> 0 ")
        cmdTextSb.Append("    AND TS.seikyuu_umu = 0 ")
        cmdTextSb.Append("    AND TJ.data_haki_syubetu = '0' ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
        SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            teibetuRenkeiDataSet, teibetuRenkeiDataSet.TeibetuRenkeiTarget.TableName, cmdParams)
        updRenkeiTgtTable = teibetuRenkeiDataSet.TeibetuRenkeiTarget

        Return updRenkeiTgtTable
    End Function

    ''' <summary>
    ''' 売上確定処理（店別初期請求）を行います
    ''' </summary>
    ''' <param name="dtFrom">売上年月日From</param>
    ''' <param name="dtTo">売上年月日To</param>
    ''' <param name="updLoginUserId">更新ログインユーザー</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Function UriageKakuteiSyoriForTenbetuSyoki(ByVal dtFrom As DateTime, _
                                                      ByVal dtTo As DateTime, _
                                                      ByVal updLoginUserId As String, _
                                                      ByVal updDatetime As DateTime) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UriageKakuteiSyoriForTenbetuSyoki" _
                                                    , dtFrom _
                                                    , dtTo _
                                                    , updLoginUserId)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer

        cmdTextSb.Append(" UPDATE t_tenbetu_syoki_seikyuu")
        cmdTextSb.Append("    SET uri_keijyou_flg   = '1'")
        cmdTextSb.Append("       ,uri_keijyou_date  = @URIKEIJOUDATE")
        cmdTextSb.Append("       ,upd_login_user_id = @UPDLOGINUSERID")
        cmdTextSb.Append("       ,upd_datetime      = @UPDDATETIME")
        cmdTextSb.Append(" WHERE uri_date >= @URIDATEFOROM")
        cmdTextSb.Append("   AND uri_date <= @URIDATETO")
        cmdTextSb.Append("   AND bunrui_cd IN ('200','210')")
        'cmdTextSb.Append("   AND seikyuu_umu = 1")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIKEIJOUDATE", SqlDbType.DateTime, 16, DateTime.Today), _
            SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo), _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updDatetime)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 売上確定処理（店別初期請求）を行います
    ''' </summary>
    ''' <param name="strMiseCd">店コード</param>
    ''' <param name="strBunruiCd">分類コード</param>
    ''' <param name="updLoginUserId">更新ログインユーザー</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Function UriageKakuteiSyoriForTenbetuSyoki(ByVal strMiseCd As String, _
                                                      ByVal strBunruiCd As String, _
                                                      ByVal updLoginUserId As String, _
                                                      ByVal updDatetime As DateTime) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UriageKakuteiSyoriForTenbetuSyoki" _
                                                    , strMiseCd _
                                                    , strBunruiCd _
                                                    , updLoginUserId)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer

        cmdTextSb.Append(" UPDATE t_tenbetu_syoki_seikyuu ")
        cmdTextSb.Append("    SET uri_keijyou_flg   = '1' ")
        cmdTextSb.Append("       ,uri_keijyou_date  = @URIKEIJOUDATE ")
        cmdTextSb.Append("       ,upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("       ,upd_datetime      = @UPDDATETIME ")
        cmdTextSb.Append("  WHERE mise_cd         = @MISECD ")
        cmdTextSb.Append("    AND bunrui_cd       = @BUNRUICD ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@MISECD", SqlDbType.VarChar, 5, strMiseCd), _
            SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, strBunruiCd), _
            SQLHelper.MakeParam("@URIKEIJOUDATE", SqlDbType.DateTime, 16, DateTime.Today), _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updDatetime)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 売上確定処理（店別請求）を行います
    ''' </summary>
    ''' <param name="dtFrom">売上年月日From</param>
    ''' <param name="dtTo">売上年月日To</param>
    ''' <param name="updLoginUserId">更新ログインユーザー</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Function UriageKakuteiSyoriForTenbetu(ByVal dtFrom As DateTime, _
                                                 ByVal dtTo As DateTime, _
                                                 ByVal updLoginUserId As String, _
                                                 ByVal updDatetime As DateTime) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UriageKakuteiSyoriForTenbetu" _
                                                    , dtFrom _
                                                    , dtTo _
                                                    , updLoginUserId)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer

        cmdTextSb.Append(" UPDATE t_tenbetu_seikyuu")
        cmdTextSb.Append("    SET uri_keijyou_flg   = '1'")
        cmdTextSb.Append("       ,uri_keijyou_date  = @URIKEIJOUDATE")
        cmdTextSb.Append("       ,upd_login_user_id = @UPDLOGINUSERID")
        cmdTextSb.Append("       ,upd_datetime      = @UPDDATETIME")
        cmdTextSb.Append("  WHERE uri_date >= @URIDATEFOROM ")
        cmdTextSb.Append("    AND uri_date <= @URIDATETO ")
        cmdTextSb.Append("    AND bunrui_cd IN ('220','230') ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIKEIJOUDATE", SqlDbType.DateTime, 16, DateTime.Today), _
            SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo), _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updDatetime)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 売上確定処理（店別請求）を行います
    ''' </summary>
    ''' <param name="strMiseCd">店コード</param>
    ''' <param name="strBunruiCd">分類コード</param>
    ''' <param name="dtNyuuryokuDate">入力日</param>
    ''' <param name="intNyuuryokuDateNo">入力日No</param>
    ''' <param name="updLoginUserId">更新ログインユーザー</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Function UriageKakuteiSyoriForTenbetu(ByVal strMiseCd As String, _
                                                 ByVal strBunruiCd As String, _
                                                 ByVal dtNyuuryokuDate As DateTime, _
                                                 ByVal intNyuuryokuDateNo As Integer, _
                                                 ByVal updLoginUserId As String, _
                                                 ByVal updDatetime As DateTime) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UriageKakuteiSyoriForTenbetu" _
                                                    , strMiseCd _
                                                    , strBunruiCd _
                                                    , dtNyuuryokuDate _
                                                    , intNyuuryokuDateNo _
                                                    , updLoginUserId)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer

        cmdTextSb.Append(" UPDATE t_tenbetu_seikyuu ")
        cmdTextSb.Append("    SET uri_keijyou_flg   = '1' ")
        cmdTextSb.Append("       ,uri_keijyou_date  = @URIKEIJOUDATE ")
        cmdTextSb.Append("       ,upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("       ,upd_datetime      = @UPDDATETIME ")
        cmdTextSb.Append("  WHERE mise_cd            = @MISECD ")
        cmdTextSb.Append("    AND bunrui_cd          = @BUNRUICD ")
        cmdTextSb.Append("    AND nyuuryoku_date     = @NYUURYOKUDATE ")
        cmdTextSb.Append("    AND nyuuryoku_date_no  = @NYUURYOKUDATENO ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@MISECD", SqlDbType.VarChar, 5, strMiseCd), _
            SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, strBunruiCd), _
            SQLHelper.MakeParam("@NYUURYOKUDATE", SqlDbType.DateTime, 16, DateTime.Parse(dtNyuuryokuDate)), _
            SQLHelper.MakeParam("@NYUURYOKUDATENO", SqlDbType.Int, 4, Integer.Parse(intNyuuryokuDateNo)), _
            SQLHelper.MakeParam("@URIKEIJOUDATE", SqlDbType.DateTime, 16, DateTime.Today), _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updDatetime)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 発注書の自動確定処理を行います
    ''' </summary>
    ''' <param name="strDenpyouNo">伝票NO</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="strBunruiCd">分類CD</param>
    ''' <param name="intGamenHyoujiNo">画面表示NO</param>
    ''' <param name="addLoginUserId">登録ログインユーザー</param>
    ''' <returns>登録件数</returns>
    ''' <remarks></remarks>
    Public Function JidouKakuteiSyori(ByVal strDenpyouNo As String, _
                                      ByVal strKbn As String, _
                                      ByVal strHosyousyoNo As String, _
                                      ByVal strBunruiCd As String, _
                                      ByVal intGamenHyoujiNo As Integer, _
                                      ByVal addLoginUserId As String, _
                                      ByVal updDatetime As DateTime) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".JidouKakuteiSyori" _
                                                    , strDenpyouNo _
                                                    , strKbn _
                                                    , strHosyousyoNo _
                                                    , strBunruiCd _
                                                    , intGamenHyoujiNo _
                                                    , addLoginUserId)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer

        cmdTextSb.Append(" INSERT INTO t_hattyuusyo_jidou_kakutei ")
        cmdTextSb.Append("        ( syori_datetime ")
        cmdTextSb.Append("         ,denpyou_no ")
        cmdTextSb.Append("         ,keiretu_cd ")
        cmdTextSb.Append("         ,kameiten_cd ")
        cmdTextSb.Append("         ,kameiten_mei1 ")
        cmdTextSb.Append("         ,kameiten_mei2 ")
        cmdTextSb.Append("         ,hattyuusyo_kakunin_date ")
        cmdTextSb.Append("         ,kbn ")
        cmdTextSb.Append("         ,hosyousyo_no ")
        cmdTextSb.Append("         ,bunrui_cd ")
        cmdTextSb.Append("         ,gamen_hyouji_no ")
        cmdTextSb.Append("         ,sesyu_mei ")
        cmdTextSb.Append("         ,syouhin_mei ")
        cmdTextSb.Append("         ,hattyuusyo_gaku ")
        cmdTextSb.Append("         ,seikyuusyo_hak_date ")
        cmdTextSb.Append("         ,uri_date ")
        cmdTextSb.Append("         ,add_login_user_id ")
        cmdTextSb.Append("         ,add_datetime ")
        cmdTextSb.Append("         ,upd_login_user_id ")
        cmdTextSb.Append("         ,upd_datetime ")
        cmdTextSb.Append("        ) ")
        cmdTextSb.Append(" SELECT @ADDDATETIME ")
        cmdTextSb.Append("       ,@DENPYOUNO ")
        cmdTextSb.Append("       ,MK.keiretu_cd ")
        cmdTextSb.Append("       ,TJ.kameiten_cd ")
        cmdTextSb.Append("       ,MK.kameiten_mei1 ")
        cmdTextSb.Append("       ,MK.kameiten_mei2 ")
        cmdTextSb.Append("       ,TS.hattyuusyo_kakunin_date ")
        cmdTextSb.Append("       ,TS.kbn ")
        cmdTextSb.Append("       ,TS.hosyousyo_no ")
        cmdTextSb.Append("       ,TS.bunrui_cd ")
        cmdTextSb.Append("       ,TS.gamen_hyouji_no ")
        cmdTextSb.Append("       ,TJ.sesyu_mei ")
        cmdTextSb.Append("       ,MS.syouhin_mei ")
        cmdTextSb.Append("       ,TS.hattyuusyo_gaku ")
        cmdTextSb.Append("       ,TS.seikyuusyo_hak_date ")
        cmdTextSb.Append("       ,TS.uri_date ")
        cmdTextSb.Append("       ,@ADDLOGINUSERID ")
        cmdTextSb.Append("       ,@ADDDATETIME ")
        cmdTextSb.Append("       ,NULL ")
        cmdTextSb.Append("       ,NULL ")
        cmdTextSb.Append("   FROM t_teibetu_seikyuu  TS ")
        cmdTextSb.Append("       ,t_jiban            TJ ")
        cmdTextSb.Append("       ,m_kameiten         MK ")
        cmdTextSb.Append("       ,m_syouhin          MS ")
        cmdTextSb.Append("  WHERE TS.kbn              = @KBN ")
        cmdTextSb.Append("    AND TS.hosyousyo_no     = @HOSYOUSYONO ")
        cmdTextSb.Append("    AND TS.bunrui_cd        = @BUNRUICD ")
        cmdTextSb.Append("    AND TS.gamen_hyouji_no  = @GAMENHYOUJINO ")
        cmdTextSb.Append("    AND TS.syouhin_cd       = MS.syouhin_cd ")
        cmdTextSb.Append("    AND TS.kbn              = TJ.kbn ")
        cmdTextSb.Append("    AND TS.hosyousyo_no     = TJ.hosyousyo_no ")
        cmdTextSb.Append("    AND TJ.kameiten_cd      = MK.kameiten_cd ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@DENPYOUNO", SqlDbType.Char, 6, strDenpyouNo), _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, strKbn), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, strHosyousyoNo), _
            SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, strBunruiCd), _
            SQLHelper.MakeParam("@GAMENHYOUJINO", SqlDbType.Int, 4, intGamenHyoujiNo), _
            SQLHelper.MakeParam("@ADDLOGINUSERID", SqlDbType.VarChar, 30, addLoginUserId), _
            SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, updDatetime)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 売上確定処理（汎用売上）を行います
    ''' </summary>
    ''' <returns>処理件数</returns>
    ''' <remarks></remarks>
    Public Function UriageKakuteiSyoriForHannyou(ByVal dtFrom As DateTime, _
                                                 ByVal dtTo As DateTime, _
                                                 ByVal updLoginUserId As String, _
                                                 ByVal updDatetime As DateTime) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UriageKakuteiSyoriForHannyou" _
                                                    , dtFrom _
                                                    , dtTo _
                                                    , updLoginUserId)
        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" UPDATE")
        cmdTextSb.Append("      t_hannyou_uriage")
        cmdTextSb.Append(" SET")
        cmdTextSb.Append("      uri_keijyou_flg = 1")
        cmdTextSb.Append("    , uri_keijyou_date = @URIKEIJOUDATE")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID")
        cmdTextSb.Append("    , upd_login_user_name = jhs_sys.fnGetAddUpdUserName(@UPDLOGINUSERID)")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      torikesi = 0")
        cmdTextSb.Append("  AND uri_date BETWEEN @URIDATEFOROM AND @URIDATETO")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIKEIJOUDATE", SqlDbType.DateTime, 16, DateTime.Today), _
            SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo), _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updDatetime)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 仕入確定処理（汎用仕入）を行います
    ''' </summary>
    ''' <returns>処理件数</returns>
    ''' <remarks></remarks>
    Public Function SiireKakuteiSyoriForHannyou(ByVal dtFrom As DateTime, _
                                                ByVal dtTo As DateTime, _
                                                ByVal updLoginUserId As String, _
                                                ByVal updDatetime As DateTime) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SiireKakuteiSyoriForHannyou" _
                                                    , dtFrom _
                                                    , dtTo _
                                                    , updLoginUserId)
        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" UPDATE")
        cmdTextSb.Append("      t_hannyou_siire")
        cmdTextSb.Append(" SET")
        cmdTextSb.Append("      siire_keijyou_flg = 1")
        cmdTextSb.Append("    , siire_keijyou_date = @SIIREKEIJOUDATE")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID")
        cmdTextSb.Append("    , upd_login_user_name = jhs_sys.fnGetAddUpdUserName(@UPDLOGINUSERID)")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      torikesi = 0")
        cmdTextSb.Append("  AND siire_date BETWEEN @SIIREDATEFOROM AND @SIIREDATETO")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@SIIREKEIJOUDATE", SqlDbType.DateTime, 16, DateTime.Today), _
            SQLHelper.MakeParam("@SIIREDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@SIIREDATETO", SqlDbType.DateTime, 16, dtTo), _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updDatetime)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別請求テーブル連携管理テーブルを更新する対象のキーを取得します（商品４）
    ''' </summary>
    ''' <param name="dtFrom">請求書発行日FROM</param>
    ''' <param name="dtTo">請求書発行日TO</param>
    ''' <returns>更新対象のキーを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuRenkeiTartgetForSyouhin4(ByVal dtFrom As Date, ByVal dtTo As Date) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuRenkeiTartgetForSyouhin4" _
                                                    , dtFrom _
                                                    , dtTo)
        Dim teibetuRenkeiDataSet As New TeibetuRenkeiDataSet
        Dim updRenkeiTgtTable As TeibetuRenkeiDataSet.TeibetuRenkeiTargetDataTable
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      TS.kbn")
        cmdTextSb.Append("    , TS.hosyousyo_no")
        cmdTextSb.Append("    , TS.bunrui_cd")
        cmdTextSb.Append("    , TS.gamen_hyouji_no")
        cmdTextSb.Append("    , R.renkei_siji_cd")
        cmdTextSb.Append("    , R.sousin_jyky_cd")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      t_teibetu_seikyuu TS")
        cmdTextSb.Append("           INNER JOIN t_jiban TJ")
        cmdTextSb.Append("             ON TS.kbn = TJ.kbn")
        cmdTextSb.Append("            AND TS.hosyousyo_no = TJ.hosyousyo_no")
        cmdTextSb.Append("           LEFT OUTER JOIN t_teibetu_seikyuu_renkei R WITH (UPDLOCK)")
        cmdTextSb.Append("             ON TS.kbn = R.kbn")
        cmdTextSb.Append("            AND TS.hosyousyo_no = R.hosyousyo_no")
        cmdTextSb.Append("            AND TS.bunrui_cd = R.bunrui_cd")
        cmdTextSb.Append("            AND TS.gamen_hyouji_no = R.gamen_hyouji_no")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      TS.uri_date >= @URIDATEFOROM")
        cmdTextSb.Append("  AND TS.uri_date <= @URIDATETO")
        cmdTextSb.Append("  AND TS.bunrui_cd = '190'")
        'cmdTextSb.Append("  AND TS.seikyuu_umu = 1")
        cmdTextSb.Append("  AND TJ.data_haki_syubetu = '0'")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
        SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            teibetuRenkeiDataSet, teibetuRenkeiDataSet.TeibetuRenkeiTarget.TableName, cmdParams)
        updRenkeiTgtTable = teibetuRenkeiDataSet.TeibetuRenkeiTarget

        Return updRenkeiTgtTable

    End Function

#End Region

#Region "データ作成（月別割引）処理"
    ''' <summary>
    ''' 既存の月額実績割引データを削除します
    ''' </summary>
    ''' <param name="dtFromTheEnd">画面.売上年月日(FROM)の末日</param>
    ''' <param name="strLoginUserId">ログインユーザーID</param>
    ''' <returns>削除件数</returns>
    ''' <remarks></remarks>
    Public Function DeleteWaribikiData(ByVal dtFromTheEnd As Date, ByVal strLoginUserId As String) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteWaribikiData" _
                                                    , dtFromTheEnd)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer
        Dim clsJbAcc As New JibanDataAccess

        '削除処理時は、トリガ用ローカル一時テーブルを生成するSQLを追加
        cmdTextSb.Append(clsJbAcc.CreateUserInfoTempTableSQL(strLoginUserId))

        cmdTextSb.Append(" DELETE FROM t_tenbetu_seikyuu ")
        cmdTextSb.Append("  WHERE bunrui_cd = '240' ")
        cmdTextSb.Append("    AND uri_date  = @URIDATE ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIDATE", SqlDbType.DateTime, 16, dtFromTheEnd)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 店別割引データ削除対象の店別請求テーブルのデータを連携管理テーブルの更新用に取得します
    ''' </summary>
    ''' <param name="dtFromTheEnd">画面.売上年月日(FROM)の末日</param>
    ''' <param name="addLoginUserId">更新ログインユーザー</param>
    ''' <returns>連携対象件数</returns>
    ''' <remarks></remarks>
    Public Function GetWaribikiDataRenkeiTarget(ByVal dtFromTheEnd As Date, ByVal addLoginUserId As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteWaribikiDataRenkei" _
                                                    , dtFromTheEnd _
                                                    , addLoginUserId)
        Dim tenbetuRenkeiDataSet As New TenbetuRenkeiDataSet
        Dim updRenkeiTgtTable As TenbetuRenkeiDataSet.TenbetuRenkeiTargetDataTable
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" SELECT T.mise_cd ")
        cmdTextSb.Append("       ,T.bunrui_cd ")
        cmdTextSb.Append("       ,T.nyuuryoku_date ")
        cmdTextSb.Append("       ,T.nyuuryoku_date_no ")
        cmdTextSb.Append("       ,R.renkei_siji_cd ")
        cmdTextSb.Append("       ,R.sousin_jyky_cd  ")
        cmdTextSb.Append("   FROM t_tenbetu_seikyuu        T ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        t_tenbetu_seikyuu_renkei R WITH(UPDLOCK) ")
        cmdTextSb.Append("     ON T.mise_cd           = R.mise_cd ")
        cmdTextSb.Append("    AND T.bunrui_cd         = R.bunrui_cd ")
        cmdTextSb.Append("    AND T.nyuuryoku_date    = R.nyuuryoku_date ")
        cmdTextSb.Append("    AND T.nyuuryoku_date_no = R.nyuuryoku_date_no ")
        cmdTextSb.Append("  WHERE T.bunrui_cd = '240' ")
        cmdTextSb.Append("    AND T.uri_date  = @URIDATE ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIDATE", SqlDbType.DateTime, 16, dtFromTheEnd)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            tenbetuRenkeiDataSet, tenbetuRenkeiDataSet.TenbetuRenkeiTarget.TableName, cmdParams)
        updRenkeiTgtTable = tenbetuRenkeiDataSet.TenbetuRenkeiTarget

        Return updRenkeiTgtTable
    End Function

    ''' <summary>
    ''' 集計先営業所コード単位の割引対象件数を取得します
    ''' </summary>
    ''' <param name="dtFrom">売上年月日From</param>
    ''' <param name="dtTo">売上年月日To</param>
    ''' <returns>集計先営業所コード単位の割引対象件数を格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetCountWaribikiTarget(ByVal dtFrom As DateTime _
                                            , ByVal dtTo As DateTime) As UriageSiireCsvDataSet.WaribikiKensuuTableDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCountWaribikiTarget" _
                                                    , dtFrom _
                                                    , dtTo)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim waribikiDataSet As New UriageSiireCsvDataSet
        Dim waribikiTable As UriageSiireCsvDataSet.WaribikiKensuuTableDataTable

        cmdTextSb.Append(" SELECT LEFT(syukeisaki_eigyousyo_cd + '     ', 5) AS syukeisaki_eigyousyo_cd")
        cmdTextSb.Append("       ,COUNT(*) AS waribiki_count ")
        cmdTextSb.Append("   FROM t_teibetu_seikyuu     TTS ")
        cmdTextSb.Append("       ,t_jiban               TJB ")
        cmdTextSb.Append("       ,m_kameiten            MKM ")
        cmdTextSb.Append("       ,m_eigyousyo_waribiki  MEW ")
        cmdTextSb.Append("  WHERE TTS.kbn          = TJB.kbn ")
        cmdTextSb.Append("    AND TTS.hosyousyo_no = TJB.hosyousyo_no ")
        cmdTextSb.Append("    AND TTS.kbn          = 'A' ")
        'cmdTextSb.Append("    AND TTS.seikyuu_umu  = 1 ")
        cmdTextSb.Append("    AND TTS.syouhin_cd   = 'H0001' ")
        cmdTextSb.Append("    AND TTS.uri_date    >= @URIDATEFROM ")
        cmdTextSb.Append("    AND TTS.uri_date    <= @URIDATETO ")
        cmdTextSb.Append("    AND TJB.kameiten_cd  = MKM.kameiten_cd ")
        cmdTextSb.Append("    AND MKM.eigyousyo_cd = MEW.eigyousyo_cd ")
        cmdTextSb.Append("    AND MEW.syukeisaki_eigyousyo_cd <> '9' ")
        cmdTextSb.Append("  GROUP BY syukeisaki_eigyousyo_cd ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIDATEFROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            waribikiDataSet, waribikiDataSet.WaribikiKensuuTable.TableName, cmdParams)
        waribikiTable = waribikiDataSet.WaribikiKensuuTable

        Return waribikiTable
    End Function

    ''' <summary>
    ''' 商品コード：L3001の税区分を取得します
    ''' </summary>
    ''' <returns>商品コード：L3001の税区分</returns>
    ''' <remarks></remarks>
    Public Function GetZeiKbn() As String
        Dim cmdTextSb As New StringBuilder()
        Dim zeiKbnDataSet As New UriageSiireCsvDataSet
        Dim zeiKbnTable As UriageSiireCsvDataSet.ZeiKbnTableDataTable
        Dim zeiKbnRow As UriageSiireCsvDataSet.ZeiKbnTableRow
        Dim strZeiKbn As String

        cmdTextSb.Append("SELECT zei_kbn ")
        cmdTextSb.Append("  FROM m_syouhin ")
        cmdTextSb.Append(" WHERE syouhin_cd = 'L3001' ")

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            zeiKbnDataSet, zeiKbnDataSet.ZeiKbnTable.TableName)

        zeiKbnTable = zeiKbnDataSet.ZeiKbnTable
        zeiKbnRow = zeiKbnTable.Rows(0)
        strZeiKbn = zeiKbnRow.zei_kbn

        Return strZeiKbn
    End Function

    ''' <summary>
    ''' 商品コード：L3001の税率を取得します
    ''' </summary>
    ''' <returns>商品コード：L3001の税率</returns>
    ''' <remarks></remarks>
    Public Function GetZeiRitu() As Decimal
        Dim cmdTextSb As New StringBuilder()
        Dim cmnDtAcc As New CmnDataAccess
        Dim dtblZeiritu As DataTable
        Dim decZeiRitu As Decimal

        cmdTextSb.Append("SELECT ISNULL(MZ.zeiritu, 0) ")
        cmdTextSb.Append("  FROM m_syouhin MS")
        cmdTextSb.Append("  LEFT OUTER JOIN m_syouhizei MZ")
        cmdTextSb.Append("    ON MS.zei_kbn = MZ.zei_kbn ")
        cmdTextSb.Append(" WHERE MS.syouhin_cd = 'L3001' ")

        dtblZeiritu = cmnDtAcc.getDataTable(cmdTextSb.ToString)
        decZeiRitu = dtblZeiritu.Rows(0).Item(0)

        Return decZeiRitu
    End Function

    ''' <summary>
    ''' 店別請求テーブルへ月別割引情報を登録する
    ''' </summary>
    ''' <param name="strMiseCd">店コード</param>
    ''' <param name="intTanka">単価</param>
    ''' <param name="dtFromTheEnd">売上年月日FROMの月末</param>
    ''' <param name="addLoginUserId">登録ログインユーザー</param>
    ''' <returns>登録件数</returns>
    ''' <remarks></remarks>
    Public Function InsertWaribikiDataSyori(ByVal strMiseCd As String, _
                                            ByVal intTanka As Integer, _
                                            ByVal dtFromTheEnd As DateTime, _
                                            ByVal strZeiKbn As String, _
                                            ByVal decZeiritu As Decimal, _
                                            ByVal addLoginUserId As String, _
                                            ByVal updDatetime As DateTime _
                                            ) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertWaribikiDataSyori" _
                                                    , strMiseCd _
                                                    , intTanka _
                                                    , dtFromTheEnd _
                                                    , addLoginUserId)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer

        cmdTextSb.Append("  INSERT INTO ")
        cmdTextSb.Append("      t_tenbetu_seikyuu ")
        cmdTextSb.Append("          ( ")
        cmdTextSb.Append("            mise_cd ")
        cmdTextSb.Append("           ,bunrui_cd ")
        cmdTextSb.Append("           ,nyuuryoku_date ")
        cmdTextSb.Append("           ,nyuuryoku_date_no ")
        cmdTextSb.Append("           ,hassou_date ")
        cmdTextSb.Append("           ,seikyuusyo_hak_date ")
        cmdTextSb.Append("           ,uri_date ")
        cmdTextSb.Append("           ,uri_keijyou_flg ")
        cmdTextSb.Append("           ,uri_keijyou_date ")
        cmdTextSb.Append("           ,syouhin_cd ")
        cmdTextSb.Append("           ,tanka ")
        cmdTextSb.Append("           ,suu ")
        cmdTextSb.Append("           ,zei_kbn ")
        cmdTextSb.Append("           ,syouhizei_gaku ")
        cmdTextSb.Append("           ,add_login_user_id ")
        cmdTextSb.Append("           ,add_datetime ")
        cmdTextSb.Append("          ) VALUES ( ")
        cmdTextSb.Append("            @MISECD ")
        cmdTextSb.Append("           ,'240' ")
        cmdTextSb.Append("           ,@ADDDATETODAY ")
        cmdTextSb.Append("           ,1 ")
        cmdTextSb.Append("           ,@ADDDATETODAY ")
        cmdTextSb.Append("           ,@URIDATEFROMEND ")
        cmdTextSb.Append("           ,@URIDATEFROMEND ")
        cmdTextSb.Append("           ,1 ")
        cmdTextSb.Append("           ,@URIKEIJOUDATE ")
        cmdTextSb.Append("           ,'L3001' ")
        cmdTextSb.Append("           ,@TANKA ")
        cmdTextSb.Append("           ,-1 ")
        cmdTextSb.Append("           ,@ZEIKBN ")
        cmdTextSb.Append("           ,@SYOUHIZEI")
        cmdTextSb.Append("           ,@ADDLOGINUSERID ")
        cmdTextSb.Append("           ,@ADDDATETIME ")
        cmdTextSb.Append("          ) ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIKEIJOUDATE", SqlDbType.DateTime, 16, DateTime.Today), _
            SQLHelper.MakeParam("@MISECD", SqlDbType.VarChar, 5, strMiseCd), _
            SQLHelper.MakeParam("@TANKA", SqlDbType.Int, 4, intTanka), _
            SQLHelper.MakeParam("@ADDDATETODAY", SqlDbType.DateTime, 16, DateTime.Today), _
            SQLHelper.MakeParam("@URIDATEFROMEND", SqlDbType.DateTime, 16, dtFromTheEnd), _
            SQLHelper.MakeParam("@ZEIKBN", SqlDbType.VarChar, 1, strZeiKbn), _
            SQLHelper.MakeParam("@SYOUHIZEI", SqlDbType.Int, 4, Fix(intTanka * decZeiritu) * -1), _
            SQLHelper.MakeParam("@ADDLOGINUSERID", SqlDbType.VarChar, 30, addLoginUserId), _
            SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, updDatetime)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 店別売上（月別割引）CSV出力用データを取得します
    ''' </summary>
    ''' <param name="dtFrom">売上年月日From</param>
    ''' <param name="dtTo">売上年月日To</param>
    ''' <param name="intDenpyouNo">伝票番号</param>
    ''' <returns>店別売上（月別割引）CSV出力用データを格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetWaribikiTenbetuOutPutData(ByVal dtFrom As DateTime, _
                                                    ByVal dtTo As DateTime, _
                                                    ByVal intDenpyouNo As Integer) As UriageSiireCsvDataSet.UriageCsvTableDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetWaribikiTenbetuOutPutData" _
                                                    , dtFrom _
                                                    , dtTo _
                                                    , intDenpyouNo)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim csvDataSet As New UriageSiireCsvDataSet
        Dim csvTable As UriageSiireCsvDataSet.UriageCsvTableDataTable
        Dim denpyouNoCol As New DataColumn

        '伝票NOの設定
        csvDataSet.UriageCsvTable.denpyou_noColumn.AutoIncrementSeed = intDenpyouNo + 1

        cmdTextSb.Append(" SELECT * ")
        cmdTextSb.Append("       ,suuryou * tanka AS uriage_kingaku ")               '売上金額------- 売上金額
        cmdTextSb.Append("   FROM( ")
        cmdTextSb.Append("         SELECT '0' AS denku ")                            '伝区----------- 0（固定）
        cmdTextSb.Append("               ,TS.uri_date ")                             '売上年月日----- 売上年月日
        cmdTextSb.Append("               ,TS.seikyuusyo_hak_date ")                  '請求発行日----- 請求書発行日
        cmdTextSb.Append("               ,(CASE TS.bunrui_cd ")
        cmdTextSb.Append("                   WHEN '220' ")
        cmdTextSb.Append("                     THEN MK.hansokuhin_seikyuusaki ")
        cmdTextSb.Append("                   ELSE ME.pca_seikyuu_cd ")
        cmdTextSb.Append("                 END) AS tys_seikyuu_saki ")               '得意先ｺｰﾄﾞ----- 調査請求先
        cmdTextSb.Append("               ,(CASE TS.bunrui_cd ")
        cmdTextSb.Append("                   WHEN '220' ")
        cmdTextSb.Append("                     THEN MKT.kameiten_mei1 ")
        cmdTextSb.Append("                   ELSE ME.eigyousyo_mei ")
        cmdTextSb.Append("                 END) AS kameiten_mei1 ")                  '得意先名------- 加盟店名1
        cmdTextSb.Append("               ,'' AS tyokusou_saki_cd ")                  '直送先ｺｰﾄﾞ----- 空白
        cmdTextSb.Append("               ,(CASE TS.bunrui_cd ")
        cmdTextSb.Append("                   WHEN '220' ")
        cmdTextSb.Append("                     THEN MK.keiretu_cd ")
        cmdTextSb.Append("                   ELSE ME.pca_seikyuu_cd ")
        cmdTextSb.Append("                 END) AS keiretu_cd ")                     '先方担当者名--- 系列コード
        cmdTextSb.Append("               ,'0' AS bumon_cd ")                         '部門ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,'0' AS tantousya_cd ")                     '担当者ｺｰﾄﾞ----- 0（固定）
        cmdTextSb.Append("               ,'0' AS tekiyou_cd ")                       '摘要ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,'' AS sesyu_mei ")                         '摘要名--------- 空白
        cmdTextSb.Append("               ,'' AS bunrui_cd ")                         '分類ｺｰﾄﾞ------- 空白
        cmdTextSb.Append("               ,'' AS denpyou_kbnv ")                      '伝票区分------- 空白
        cmdTextSb.Append("               ,TS.syouhin_cd ")                           '商品ｺｰﾄﾞ------- 商品コード
        cmdTextSb.Append("               ,'0' AS masta_kbn ")                        'ﾏｽﾀ区分-------- 0（固定）
        cmdTextSb.Append("               ,MS.syouhin_mei ")                          '品名----------- 商品名
        cmdTextSb.Append("               ,'0' AS ku ")                               '区------------- 0（固定）
        cmdTextSb.Append("               ,'0' AS souko_cd ")                         '倉庫ｺｰﾄﾞ------- 0（固定）
        cmdTextSb.Append("               ,'0' AS iri_suu ")                          '入数----------- 0（固定）
        cmdTextSb.Append("               ,'0' AS hako_suu ")                         '箱数----------- 0（固定）
        cmdTextSb.Append("               ,TS.suu AS suuryou ")                      '数量----------- 数量
        cmdTextSb.Append("               ,MS.tani ")                                 '単位----------- 単位
        cmdTextSb.Append("               ,TS.tanka AS tanka ")                       '単価----------- 単価
        cmdTextSb.Append("               ,0 AS gen_tanka ")                          '原単価--------- 0（固定）
        cmdTextSb.Append("               ,0 AS genka_gaku ")                         '原価額--------- 0（固定）
        cmdTextSb.Append("               ,0 AS ara_rieki ")                          '粗利益--------- 0（固定）
        cmdTextSb.Append("               ,0 AS sotozei_gaku ")                       '外税額--------- 0（固定）
        cmdTextSb.Append("               ,0 AS utizei_gaku ")                        '内税額--------- 0（固定）
        cmdTextSb.Append("               ,TS.zei_kbn ")                              '税区分--------- 税区分
        cmdTextSb.Append("               ,MS.zeikomi_kbn ")                          '税込区分------- 税込区分
        cmdTextSb.Append("               ,'' AS bikou ")                             '備考----------- 空白
        cmdTextSb.Append("               ,'0' AS hyoujun_kakaku ")                   '標準価格------- 0（固定）
        cmdTextSb.Append("               ,'0' AS douji_nyuuka_kbn ")                 '同時入荷区分--- 0（固定）
        cmdTextSb.Append("               ,'0' AS bai_tanka ")                        '売単価--------- 0（固定）
        cmdTextSb.Append("               ,'0' AS baika_kingaku ")                    '売価金額------- 0（固定）
        cmdTextSb.Append("               ,'' AS kikaku_kataban ")                    '規格･型番------ 空白
        cmdTextSb.Append("               ,'' AS color ")                             '色------------- 空白
        cmdTextSb.Append("               ,'' AS size ")                              'サイズ--------- 空白
        cmdTextSb.Append("               ,MK.tys_seikyuu_saki AS order_key1 ")       'ソートKEY1----- 得意先コード
        cmdTextSb.Append("               ,TS.bunrui_cd AS order_key2 ")              'ソートKEY2----- 分類コード
        cmdTextSb.Append("               ,TS.nyuuryoku_date AS order_key3 ")         'ソートKEY3----- 入力日
        cmdTextSb.Append("               ,TS.nyuuryoku_date_no AS order_key4 ")      'ソートKEY4----- 入力日NO
        cmdTextSb.Append("               ,TS.mise_cd AS update_key1 ")               '更新KEY1------- 店コード
        cmdTextSb.Append("               ,TS.bunrui_cd AS update_key2 ")             '更新KEY2------- 分類コード
        cmdTextSb.Append("               ,TS.nyuuryoku_date AS update_key3 ")        '更新KEY3------- 入力日
        cmdTextSb.Append("               ,TS.nyuuryoku_date_no AS update_key4 ")     '更新KEY4------- 入力日NO
        cmdTextSb.Append("           FROM t_tenbetu_seikyuu TS WITH(UPDLOCK) ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_syouhin         MS ")
        cmdTextSb.Append("             ON TS.syouhin_cd = MS.syouhin_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_kameiten        MK ")
        cmdTextSb.Append("             ON TS.mise_cd = MK.kameiten_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_kameiten        MKT ")
        cmdTextSb.Append("             ON MK.tys_seikyuu_saki = MKT.kameiten_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_eigyousyo       ME ")
        cmdTextSb.Append("             ON TS.mise_cd = ME.eigyousyo_cd ")
        cmdTextSb.Append("          WHERE TS.uri_date   = @URIDATETO ")
        cmdTextSb.Append("            AND TS.bunrui_cd  = '240' ")
        cmdTextSb.Append("            AND TS.syouhin_cd = 'L3001' ")
        cmdTextSb.Append(" ) teibetu_tyousa_csv ")
        cmdTextSb.Append("  ORDER BY order_key1 ")
        cmdTextSb.Append("          ,order_key2 ")
        cmdTextSb.Append("          ,order_key3 ")
        cmdTextSb.Append("          ,order_key4 ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@URIDATEFOROM", SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtTo)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            csvDataSet, csvDataSet.UriageCsvTable.TableName, cmdParams)
        csvTable = csvDataSet.UriageCsvTable

        Return csvTable
    End Function
#End Region

#Region "統合会計連動対応"
    ''' <summary>
    ''' 売上/仕入/入金データTの更新処理(統合会計送信フラグ)を行います
    ''' </summary>
    ''' <returns>処理件数</returns>
    ''' <remarks></remarks>
    Public Function UpdTgkSousinFlg( _
                                    ByVal emBtnType As EarthEnum.emExecBtnType _
                                    , ByVal updLoginUserId As String _
                                    , ByVal updDatetime As DateTime _
                                    , ByVal dtUriDateTo As DateTime _
                                    ) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdTgkSousinFlg" _
                                                    , emBtnType _
                                                    , updLoginUserId _
                                                    , updDatetime _
                                                    , dtUriDateTo)
        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.AppendLine("UPDATE")
        Select Case emBtnType
            Case EarthEnum.emExecBtnType.BtnUriage
                cmdTextSb.AppendLine("  t_uriage_data ")
            Case EarthEnum.emExecBtnType.BtnSiire
                cmdTextSb.AppendLine("  t_siire_data ")
            Case EarthEnum.emExecBtnType.BtnNyuukin
                cmdTextSb.AppendLine("  t_nyuukin_data ")
        End Select
        cmdTextSb.Append(" SET")
        cmdTextSb.Append("      tougou_sousin_flg = 1 ")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME ")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      (tougou_sousin_flg IS NULL OR tougou_sousin_flg = 0) ")
        Select Case emBtnType
            Case EarthEnum.emExecBtnType.BtnUriage
                cmdTextSb.AppendLine(" AND uri_keijyou_flg = 1 ")
            Case EarthEnum.emExecBtnType.BtnSiire
                cmdTextSb.AppendLine(" AND siire_keijyou_flg = 1 ")
        End Select
        '売上年月日To
        Select Case emBtnType
            Case EarthEnum.emExecBtnType.BtnUriage
                cmdTextSb.Append(" AND CONVERT(VARCHAR, denpyou_uri_date ,111) <= @DENPYOU_URI_DATE_TO ")
            Case EarthEnum.emExecBtnType.BtnSiire
                cmdTextSb.Append(" AND CONVERT(VARCHAR, denpyou_siire_date ,111) <= @DENPYOU_URI_DATE_TO ")
            Case EarthEnum.emExecBtnType.BtnNyuukin
                cmdTextSb.Append(" AND CONVERT(VARCHAR, nyuukin_date ,111) <= @DENPYOU_URI_DATE_TO ")
        End Select

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updDatetime), _
            SQLHelper.MakeParam("@DENPYOU_URI_DATE_TO", SqlDbType.DateTime, 16, dtUriDateTo) _
            }

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

#End Region
End Class
