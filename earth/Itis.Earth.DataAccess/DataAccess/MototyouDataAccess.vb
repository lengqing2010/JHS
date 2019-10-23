Imports System.Data.SqlClient
Imports System.Text

Public Class MototyouDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim SLogic As New StringLogic
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "SQLパラメータ"

    Private Const DBparamFromDate As String = "@FROM_DATE"
    Private Const DBparamToDate As String = "@TO_DATE"

    Private Const DBparamSeikyuuSakiKbn As String = "@SEIKYUUSAKIKBN"
    Private Const DBparamSeikyuuSakiCd As String = "@SEIKYUUSAKICD"
    Private Const DBparamSeikyuuSakiBrc As String = "@SEIKYUUSAKIBRC"

    Private Const DBparamTyousaKaisyaCd As String = "@TYSKAISYACD"

#End Region

#Region "請求先元帳"

#Region "請求先元帳_売掛金データテーブル最新取得"

    ''' <summary>
    ''' 請求先元帳_売掛金データテーブル最新取得
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="fromDate">Optional 年月日FROM 指定有りの場合、指定日の前月末までの範囲で最新を取得</param>
    ''' <returns>繰越残高</returns>
    ''' <remarks></remarks>
    Public Function urikakeDataNewest(ByVal seikyuuSakiCd As String, _
                                      ByVal seikyuuSakiBrc As String, _
                                      ByVal seikyuuSakiKbn As String, _
                                      ByVal fromDate As Date _
                                      ) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".urikakeDataNewest", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    fromDate _
                                                    )

        ' SQL生成
        Dim sb As New StringBuilder()
        sb.AppendLine("SELECT")
        sb.AppendLine("    uk.taisyou_nengetu")
        sb.AppendLine("    , uk.seikyuu_saki_cd")
        sb.AppendLine("    , uk.seikyuu_saki_brc")
        sb.AppendLine("    , uk.seikyuu_saki_kbn")
        sb.AppendLine("    , uk.tougetu_kurikosi_zan ")
        sb.AppendLine("FROM")
        sb.AppendLine("    jhs_sys.t_urikake_data uk ")
        sb.AppendLine("    LEFT OUTER JOIN ( ")
        sb.AppendLine("        SELECT")
        sb.AppendLine("            seikyuu_saki_cd")
        sb.AppendLine("            , seikyuu_saki_brc")
        sb.AppendLine("            , seikyuu_saki_kbn")
        sb.AppendLine("            , max(taisyou_nengetu) taisyou_nengetu ")
        sb.AppendLine("        FROM")
        sb.AppendLine("            jhs_sys.t_urikake_data ")
        If fromDate <> EarthConst.Instance.MIN_DATE Then
            sb.AppendLine("        WHERE")
            sb.AppendLine("            taisyou_nengetu <= jhs_sys.fnGetLastDay(DATEADD(MONTH, - 1, " & DBparamFromDate & ")) ")
        End If
        sb.AppendLine("        GROUP BY")
        sb.AppendLine("            seikyuu_saki_cd")
        sb.AppendLine("            , seikyuu_saki_brc")
        sb.AppendLine("            , seikyuu_saki_kbn")
        sb.AppendLine("    ) ukm ")
        sb.AppendLine("        ON uk.seikyuu_saki_cd = ukm.seikyuu_saki_cd ")
        sb.AppendLine("        AND uk.seikyuu_saki_brc = ukm.seikyuu_saki_brc ")
        sb.AppendLine("        AND uk.seikyuu_saki_kbn = ukm.seikyuu_saki_kbn ")
        sb.AppendLine("WHERE")
        sb.AppendLine("    uk.taisyou_nengetu = ukm.taisyou_nengetu ")
        sb.AppendLine("    AND uk.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd)
        sb.AppendLine("    AND uk.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc)
        sb.AppendLine("    AND uk.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn)
        sb.AppendLine("")

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = Nothing

        ' パラメータを設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiCd, SqlDbType.VarChar, 5, seikyuuSakiCd), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiBrc, SqlDbType.VarChar, 2, seikyuuSakiBrc), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiKbn, SqlDbType.VarChar, 1, seikyuuSakiKbn), _
                                        SQLHelper.MakeParam(DBparamFromDate, SqlDbType.DateTime, 1, fromDate) _
                                        }

        'データ取得＆返却
        Dim dt As New DataTable
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)

    End Function

#End Region

#Region "請求先元帳_繰越残高取得"

    ''' <summary>
    ''' 請求先元帳_繰越残高取得
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="fromDate">年月日FROM</param>
    ''' <returns>繰越残高</returns>
    ''' <remarks></remarks>
    Public Function seikyuuSakiMototyouKurikosiZan(ByVal seikyuuSakiCd As String, _
                                                   ByVal seikyuuSakiBrc As String, _
                                                   ByVal seikyuuSakiKbn As String, _
                                                   ByVal fromDate As Date _
                                                   ) As Object

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".seikyuuSakiMototyouKurikosiZan", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    fromDate _
                                                    )

        ' SQL生成
        Dim sb As New StringBuilder()
        sb.AppendLine("SELECT")
        sb.AppendLine("    ISNULL(tkurikosi.tougetu_kurikosi_zan, 0) + ISNULL(tkurikosi.uriage_goukei, 0) - ISNULL(tkurikosi.nyuukin_goukei, 0) ")
        sb.AppendLine("    kurikosi_zan ")
        sb.AppendLine("FROM")
        sb.AppendLine("    ( ")
        sb.AppendLine("        SELECT")
        sb.AppendLine("            sm.seikyuu_saki_cd")
        sb.AppendLine("            , sm.seikyuu_saki_brc")
        sb.AppendLine("            , sm.seikyuu_saki_kbn")
        sb.AppendLine("            , ( ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    sum(isnull(u2.uri_gaku, 0)) + sum(CONVERT(BIGINT,isnull(u2.sotozei_gaku, 0))) kurikosi_zan ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_uriage_data u2 ")
        sb.AppendLine("                WHERE")
        sb.AppendLine("                    u2.seikyuu_saki_cd = sm.seikyuu_saki_cd ")
        sb.AppendLine("                    AND u2.seikyuu_saki_brc = sm.seikyuu_saki_brc ")
        sb.AppendLine("                    AND u2.seikyuu_saki_kbn = sm.seikyuu_saki_kbn ")
        sb.AppendLine("                    AND u2.denpyou_uri_date BETWEEN DATEADD( ")
        sb.AppendLine("                        DAY")
        sb.AppendLine("                        , + 1")
        sb.AppendLine("                        , ISNULL(ukt.taisyou_nengetu, '1900/1/1')")
        sb.AppendLine("                    ) AND DATEADD(DAY, - 1, " & DBparamFromDate & ")")
        sb.AppendLine("                    AND ISNULL(u2.uri_keijyou_flg,0) = 1 ")
        sb.AppendLine("            ) uriage_goukei")
        sb.AppendLine("            , ( ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    sum(isnull(n2.genkin, 0)) + sum(isnull(n2.kogitte, 0)) + sum(isnull(n2.furikomi, 0)) + sum(isnull(n2.tegata, 0)) ")
        sb.AppendLine("                    + sum(isnull(n2.sousai, 0)) + sum(isnull(n2.nebiki, 0)) + sum(isnull(n2.sonota, 0)) + sum(isnull(n2.kyouryoku_kaihi, 0)) ")
        sb.AppendLine("                    + sum(isnull(n2.kouza_furikae, 0)) + sum(isnull(n2.furikomi_tesuuryou, 0)) kurikosi_zan ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_nyuukin_data n2 ")
        sb.AppendLine("                WHERE")
        sb.AppendLine("                    n2.seikyuu_saki_cd = sm.seikyuu_saki_cd ")
        sb.AppendLine("                    AND n2.seikyuu_saki_brc = sm.seikyuu_saki_brc ")
        sb.AppendLine("                    AND n2.seikyuu_saki_kbn = sm.seikyuu_saki_kbn ")
        sb.AppendLine("                    AND n2.nyuukin_date BETWEEN DATEADD( ")
        sb.AppendLine("                        DAY")
        sb.AppendLine("                        , + 1")
        sb.AppendLine("                        , ISNULL(ukt.taisyou_nengetu, '1900/1/1')")
        sb.AppendLine("                    ) AND DATEADD(DAY, - 1, " & DBparamFromDate & ")")
        sb.AppendLine("            ) nyuukin_goukei")
        sb.AppendLine("            , ukt.tougetu_kurikosi_zan ")
        sb.AppendLine("        FROM")
        sb.AppendLine("            jhs_sys.m_seikyuu_saki sm ")
        sb.AppendLine("            LEFT OUTER JOIN ( ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    uk.taisyou_nengetu")
        sb.AppendLine("                    , uk.seikyuu_saki_cd")
        sb.AppendLine("                    , uk.seikyuu_saki_brc")
        sb.AppendLine("                    , uk.seikyuu_saki_kbn")
        sb.AppendLine("                    , uk.tougetu_kurikosi_zan ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_urikake_data uk ")
        sb.AppendLine("                    LEFT OUTER JOIN ( ")
        sb.AppendLine("                        SELECT")
        sb.AppendLine("                            seikyuu_saki_cd")
        sb.AppendLine("                            , seikyuu_saki_brc")
        sb.AppendLine("                            , seikyuu_saki_kbn")
        sb.AppendLine("                            , max(taisyou_nengetu) taisyou_nengetu ")
        sb.AppendLine("                        FROM")
        sb.AppendLine("                            jhs_sys.t_urikake_data ")
        sb.AppendLine("                        WHERE")
        sb.AppendLine("                            taisyou_nengetu <= jhs_sys.fnGetLastDay(DATEADD(MONTH, - 1, " & DBparamFromDate & ")) ")
        sb.AppendLine("                        GROUP BY")
        sb.AppendLine("                            seikyuu_saki_cd")
        sb.AppendLine("                            , seikyuu_saki_brc")
        sb.AppendLine("                            , seikyuu_saki_kbn")
        sb.AppendLine("                    ) ukm ")
        sb.AppendLine("                        ON uk.seikyuu_saki_cd = ukm.seikyuu_saki_cd ")
        sb.AppendLine("                        AND uk.seikyuu_saki_brc = ukm.seikyuu_saki_brc ")
        sb.AppendLine("                        AND uk.seikyuu_saki_kbn = ukm.seikyuu_saki_kbn ")
        sb.AppendLine("                WHERE")
        sb.AppendLine("                    uk.taisyou_nengetu = ukm.taisyou_nengetu")
        sb.AppendLine("            ) ukt ")
        sb.AppendLine("                ON sm.seikyuu_saki_cd = ukt.seikyuu_saki_cd ")
        sb.AppendLine("                AND sm.seikyuu_saki_brc = ukt.seikyuu_saki_brc ")
        sb.AppendLine("                AND sm.seikyuu_saki_kbn = ukt.seikyuu_saki_kbn")
        sb.AppendLine("    ) tkurikosi ")
        sb.AppendLine("WHERE")
        sb.AppendLine("    tkurikosi.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd)
        sb.AppendLine("    AND tkurikosi.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc)
        sb.AppendLine("    AND tkurikosi.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn)
        sb.AppendLine("")

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = Nothing

        ' パラメータを設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiCd, SqlDbType.VarChar, 5, seikyuuSakiCd), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiBrc, SqlDbType.VarChar, 2, seikyuuSakiBrc), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiKbn, SqlDbType.VarChar, 1, seikyuuSakiKbn), _
                                        SQLHelper.MakeParam(DBparamFromDate, SqlDbType.DateTime, 1, fromDate) _
                                        }

        'データ取得＆返却
        ' 請求締め日の取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       sb.ToString(), _
                                       sqlParams)

        Return data

    End Function

#End Region

#Region "請求先元帳_伝票データ取得"

    ''' <summary>
    ''' 請求先元帳_伝票データ取得
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="fromDate">年月日FROM</param>
    ''' <param name="toDate">年月日TO</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function seikyuuSakiMototyouDenpyouData(ByVal seikyuuSakiCd As String, _
                                                   ByVal seikyuuSakiBrc As String, _
                                                   ByVal seikyuuSakiKbn As String, _
                                                   ByVal fromDate As Date, _
                                                   ByVal toDate As Date _
                                                   ) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".seikyuuSakiMototyouDenpyouData", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    fromDate, _
                                                    toDate _
                                                    )

        ' SQL生成
        Dim sb As New StringBuilder()
        sb.AppendLine("SELECT")
        sb.AppendLine("    dd.* ")
        sb.AppendLine("FROM")
        sb.AppendLine("    ( ")
        sb.AppendLine("        SELECT")
        sb.AppendLine("            tu.denpyou_unique_no")
        sb.AppendLine("            , tu.seikyuu_saki_cd")
        sb.AppendLine("            , tu.seikyuu_saki_brc")
        sb.AppendLine("            , tu.seikyuu_saki_kbn")
        sb.AppendLine("            , tu.denpyou_uri_date nengappi")
        sb.AppendLine("            , '売上' kamoku")
        sb.AppendLine("            , tu.syouhin_cd")
        sb.AppendLine("            , tu.hinmei")
        sb.AppendLine("            , tu.kbn + tu.bangou kokyaku_no")
        sb.AppendLine("            , ISNULL(tj.jutyuu_bukken_mei, tj.sesyu_mei) bukken_mei")
        sb.AppendLine("            , tu.suu")
        sb.AppendLine("            , tu.tanka")
        sb.AppendLine("            , tu.uri_gaku zeinuki_gaku")
        sb.AppendLine("            , tu.sotozei_gaku")
        sb.AppendLine("            , tu.uri_gaku + tu.sotozei_gaku kingaku")
        sb.AppendLine("            , tu.seikyuu_date")
        sb.AppendLine("            , sub.konkai_kaisyuu_yotei_date kaisyuu_yotei_date ")
        sb.AppendLine("            , tu.denpyou_no ")
        sb.AppendLine("        FROM")
        sb.AppendLine("            jhs_sys.t_uriage_data tu ")
        sb.AppendLine("            LEFT OUTER JOIN jhs_sys.t_jiban tj ")
        sb.AppendLine("                ON tu.kbn = tj.kbn ")
        sb.AppendLine("                AND tu.bangou = tj.hosyousyo_no ")
        sb.AppendLine("            LEFT OUTER JOIN ")
        sb.AppendLine("                 (SELECT ")
        sb.AppendLine("                          sm.denpyou_unique_no ")
        sb.AppendLine("                        , sk.konkai_kaisyuu_yotei_date ")
        sb.AppendLine("                     FROM ")
        sb.AppendLine("                          jhs_sys.t_seikyuu_kagami sk ")
        sb.AppendLine("                               INNER JOIN jhs_sys.t_seikyuu_meisai sm ")
        sb.AppendLine("                                 ON sk.seikyuusyo_no = sm.seikyuusyo_no ")
        sb.AppendLine("                     WHERE ")
        sb.AppendLine("                          sk.torikesi = 0 ")
        sb.AppendLine("                    ) ")
        sb.AppendLine("                     AS sub ")
        sb.AppendLine("                  ON tu.denpyou_unique_no = sub.denpyou_unique_no ")
        sb.AppendLine("        WHERE ")
        sb.AppendLine("            ISNULL(tu.uri_keijyou_flg,0) = 1 ")
        sb.AppendLine("        UNION ALL ")
        sb.AppendLine("        SELECT")
        sb.AppendLine("            denpyou_unique_no")
        sb.AppendLine("            , seikyuu_saki_cd")
        sb.AppendLine("            , seikyuu_saki_brc")
        sb.AppendLine("            , seikyuu_saki_kbn")
        sb.AppendLine("            , nyuukin_date")
        sb.AppendLine("            , '入金'")
        sb.AppendLine("            , NULL")
        sb.AppendLine("            , syouhin_mei")
        sb.AppendLine("            , NULL")
        sb.AppendLine("            , tekiyou_mei")
        sb.AppendLine("            , NULL")
        sb.AppendLine("            , NULL")
        sb.AppendLine("            , NULL")
        sb.AppendLine("            , NULL")
        sb.AppendLine("            , kingaku")
        sb.AppendLine("            , NULL")
        sb.AppendLine("            , NULL")
        sb.AppendLine("            , denpyou_no ")
        sb.AppendLine("        FROM")
        sb.AppendLine("            ( ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    denpyou_unique_no")
        sb.AppendLine("                    , seikyuu_saki_cd")
        sb.AppendLine("                    , seikyuu_saki_brc")
        sb.AppendLine("                    , seikyuu_saki_kbn")
        sb.AppendLine("                    , nyuukin_date")
        sb.AppendLine("                    , '現金' syouhin_mei")
        sb.AppendLine("                    , tekiyou_mei")
        sb.AppendLine("                    , genkin kingaku")
        sb.AppendLine("                    , denpyou_no ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_nyuukin_data ")
        sb.AppendLine("                UNION ALL ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    denpyou_unique_no")
        sb.AppendLine("                    , seikyuu_saki_cd")
        sb.AppendLine("                    , seikyuu_saki_brc")
        sb.AppendLine("                    , seikyuu_saki_kbn")
        sb.AppendLine("                    , nyuukin_date")
        sb.AppendLine("                    , '小切手'")
        sb.AppendLine("                    , tekiyou_mei")
        sb.AppendLine("                    , kogitte")
        sb.AppendLine("                    , denpyou_no ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_nyuukin_data ")
        sb.AppendLine("                UNION ALL ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    denpyou_unique_no")
        sb.AppendLine("                    , seikyuu_saki_cd")
        sb.AppendLine("                    , seikyuu_saki_brc")
        sb.AppendLine("                    , seikyuu_saki_kbn")
        sb.AppendLine("                    , nyuukin_date")
        sb.AppendLine("                    , '振込'")
        sb.AppendLine("                    , tekiyou_mei")
        sb.AppendLine("                    , furikomi")
        sb.AppendLine("                    , denpyou_no ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_nyuukin_data ")
        sb.AppendLine("                UNION ALL ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    denpyou_unique_no")
        sb.AppendLine("                    , seikyuu_saki_cd")
        sb.AppendLine("                    , seikyuu_saki_brc")
        sb.AppendLine("                    , seikyuu_saki_kbn")
        sb.AppendLine("                    , nyuukin_date")
        sb.AppendLine("                    , '口座振替'")
        sb.AppendLine("                    , tekiyou_mei")
        sb.AppendLine("                    , kouza_furikae")
        sb.AppendLine("                    , denpyou_no ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_nyuukin_data ")
        sb.AppendLine("                UNION ALL ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    denpyou_unique_no")
        sb.AppendLine("                    , seikyuu_saki_cd")
        sb.AppendLine("                    , seikyuu_saki_brc")
        sb.AppendLine("                    , seikyuu_saki_kbn")
        sb.AppendLine("                    , nyuukin_date")
        sb.AppendLine("                    , '手形'")
        sb.AppendLine("                    , tekiyou_mei")
        sb.AppendLine("                    , tegata")
        sb.AppendLine("                    , denpyou_no ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_nyuukin_data ")
        sb.AppendLine("                UNION ALL ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    denpyou_unique_no")
        sb.AppendLine("                    , seikyuu_saki_cd")
        sb.AppendLine("                    , seikyuu_saki_brc")
        sb.AppendLine("                    , seikyuu_saki_kbn")
        sb.AppendLine("                    , nyuukin_date")
        sb.AppendLine("                    , '相殺'")
        sb.AppendLine("                    , tekiyou_mei")
        sb.AppendLine("                    , sousai")
        sb.AppendLine("                    , denpyou_no ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_nyuukin_data ")
        sb.AppendLine("                UNION ALL ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    denpyou_unique_no")
        sb.AppendLine("                    , seikyuu_saki_cd")
        sb.AppendLine("                    , seikyuu_saki_brc")
        sb.AppendLine("                    , seikyuu_saki_kbn")
        sb.AppendLine("                    , nyuukin_date")
        sb.AppendLine("                    , '値引'")
        sb.AppendLine("                    , tekiyou_mei")
        sb.AppendLine("                    , nebiki")
        sb.AppendLine("                    , denpyou_no ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_nyuukin_data ")
        sb.AppendLine("                UNION ALL ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    denpyou_unique_no")
        sb.AppendLine("                    , seikyuu_saki_cd")
        sb.AppendLine("                    , seikyuu_saki_brc")
        sb.AppendLine("                    , seikyuu_saki_kbn")
        sb.AppendLine("                    , nyuukin_date")
        sb.AppendLine("                    , 'その他'")
        sb.AppendLine("                    , tekiyou_mei")
        sb.AppendLine("                    , sonota")
        sb.AppendLine("                    , denpyou_no ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_nyuukin_data ")
        sb.AppendLine("                UNION ALL ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    denpyou_unique_no")
        sb.AppendLine("                    , seikyuu_saki_cd")
        sb.AppendLine("                    , seikyuu_saki_brc")
        sb.AppendLine("                    , seikyuu_saki_kbn")
        sb.AppendLine("                    , nyuukin_date")
        sb.AppendLine("                    , '協力会費'")
        sb.AppendLine("                    , tekiyou_mei")
        sb.AppendLine("                    , kyouryoku_kaihi")
        sb.AppendLine("                    , denpyou_no ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_nyuukin_data ")
        sb.AppendLine("                UNION ALL ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    denpyou_unique_no")
        sb.AppendLine("                    , seikyuu_saki_cd")
        sb.AppendLine("                    , seikyuu_saki_brc")
        sb.AppendLine("                    , seikyuu_saki_kbn")
        sb.AppendLine("                    , nyuukin_date")
        sb.AppendLine("                    , '振込手数料'")
        sb.AppendLine("                    , tekiyou_mei")
        sb.AppendLine("                    , furikomi_tesuuryou")
        sb.AppendLine("                    , denpyou_no ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_nyuukin_data")
        sb.AppendLine("            ) nyuukin ")
        sb.AppendLine("        WHERE")
        sb.AppendLine("            ISNULL(nyuukin.kingaku, 0) <> 0 ")
        sb.AppendLine("    ) dd ")
        sb.AppendLine("WHERE")
        sb.AppendLine("    seikyuu_saki_cd = " & DBparamSeikyuuSakiCd)
        sb.AppendLine("    AND seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc)
        sb.AppendLine("    AND seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn)
        sb.AppendLine("    AND nengappi BETWEEN " & DBparamFromDate & " AND " & DBparamToDate & " ")
        sb.AppendLine("ORDER BY")
        sb.AppendLine("    seikyuu_saki_cd")
        sb.AppendLine("    , nengappi")
        sb.AppendLine("    , kamoku DESC")
        sb.AppendLine("    , denpyou_no")
        sb.AppendLine("    , hinmei")
        sb.AppendLine("")

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = Nothing

        ' パラメータを設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiCd, SqlDbType.VarChar, 5, seikyuuSakiCd), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiBrc, SqlDbType.VarChar, 2, seikyuuSakiBrc), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiKbn, SqlDbType.VarChar, 1, seikyuuSakiKbn), _
                                        SQLHelper.MakeParam(DBparamToDate, SqlDbType.DateTime, 1, toDate), _
                                        SQLHelper.MakeParam(DBparamFromDate, SqlDbType.DateTime, 1, fromDate) _
                                        }

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)

    End Function

#End Region

#End Region

#Region "支払先元帳"

#Region "支払先元帳_買掛金データテーブル最新取得"

    ''' <summary>
    ''' 支払先元帳_買掛金データテーブル最新取得
    ''' </summary>
    ''' <param name="tysKaisyaCd">調査会社コード+支払集計先事業所コード</param>
    ''' <param name="fromDate">Optional 年月日FROM 指定有りの場合、指定日の前月末までの範囲で最新を取得</param>
    ''' <returns>繰越残高</returns>
    ''' <remarks></remarks>
    Public Function kaikakeDataNewest(ByVal tysKaisyaCd As String, _
                                      ByVal fromDate As Date _
                                      ) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".kaikakeDataNewest", _
                                                    tysKaisyaCd, _
                                                    fromDate _
                                                    )

        ' SQL生成
        Dim sb As New StringBuilder()
        sb.AppendLine("SELECT")
        sb.AppendLine("    uk.taisyou_nengetu")
        sb.AppendLine("    , uk.tys_kaisya_cd")
        sb.AppendLine("    , uk.shri_jigyousyo_cd")
        sb.AppendLine("    , uk.tougetu_kurikosi_zan ")
        sb.AppendLine("FROM")
        sb.AppendLine("    jhs_sys.t_kaikake_data uk ")
        sb.AppendLine("    LEFT OUTER JOIN ( ")
        sb.AppendLine("        SELECT")
        sb.AppendLine("            tys_kaisya_cd")
        sb.AppendLine("            , shri_jigyousyo_cd ")
        sb.AppendLine("            , max(taisyou_nengetu) taisyou_nengetu ")
        sb.AppendLine("        FROM")
        sb.AppendLine("            jhs_sys.t_kaikake_data ")
        If fromDate <> EarthConst.Instance.MIN_DATE Then
            sb.AppendLine("        WHERE")
            sb.AppendLine("            taisyou_nengetu <= jhs_sys.fnGetLastDay(DATEADD(MONTH, - 1, " & DBparamFromDate & ")) ")
        End If
        sb.AppendLine("        GROUP BY")
        sb.AppendLine("            tys_kaisya_cd")
        sb.AppendLine("            , shri_jigyousyo_cd")
        sb.AppendLine("    ) ukm ")
        sb.AppendLine("        ON uk.tys_kaisya_cd = ukm.tys_kaisya_cd ")
        sb.AppendLine("        AND uk.shri_jigyousyo_cd = ukm.shri_jigyousyo_cd ")
        sb.AppendLine("WHERE")
        sb.AppendLine("    uk.taisyou_nengetu = ukm.taisyou_nengetu ")
        sb.AppendLine("    AND uk.tys_kaisya_cd + uk.shri_jigyousyo_cd = " & DBparamTyousaKaisyaCd)
        sb.AppendLine("")

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = Nothing

        ' パラメータを設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(DBparamTyousaKaisyaCd, SqlDbType.VarChar, 7, tysKaisyaCd), _
                                        SQLHelper.MakeParam(DBparamFromDate, SqlDbType.DateTime, 1, fromDate) _
                                        }

        'データ取得＆返却
        Dim dt As New DataTable
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)

    End Function

#End Region

#Region "支払先元帳_繰越残高取得"

    ''' <summary>
    ''' 支払先元帳_繰越残高取得
    ''' </summary>
    ''' <param name="tysKaisyaCd">調査会社コード</param>
    ''' <param name="fromDate">年月日FROM</param>
    ''' <returns>繰越残高</returns>
    ''' <remarks></remarks>
    Public Function siharaiSakiMototyouKurikosiZan(ByVal tysKaisyaCd As String, _
                                                   ByVal fromDate As Date _
                                                   ) As Object

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".siharaiSakiMototyouKurikosiZan", _
                                                    tysKaisyaCd, _
                                                    fromDate _
                                                    )

        ' SQL生成
        Dim sb As New StringBuilder()
        sb.AppendLine("SELECT")
        sb.AppendLine("    ISNULL(tkurikosi.tougetu_kurikosi_zan, 0) + ISNULL(tkurikosi.siire_goukei, 0) - ISNULL(tkurikosi.siharai_goukei, 0) kurikosi_zan ")
        sb.AppendLine("FROM")
        sb.AppendLine("    ( ")
        sb.AppendLine("        SELECT")
        sb.AppendLine("            sm.tys_kaisya_cd")
        sb.AppendLine("            , sm.shri_jigyousyo_cd")
        sb.AppendLine("            , ( ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    sum(isnull(s2.siire_gaku, 0)) + sum(CONVERT(BIGINT, isnull(s2.sotozei_gaku, 0))) kurikosi_zan ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.m_tyousakaisya tth3 ")
        sb.AppendLine("                    INNER JOIN jhs_sys.t_siire_data s2 ")
        sb.AppendLine("                        ON tth3.tys_kaisya_cd = s2.tys_kaisya_cd ")
        sb.AppendLine("                        AND tth3.jigyousyo_cd = s2.tys_kaisya_jigyousyo_cd ")
        sb.AppendLine("                WHERE")
        sb.AppendLine("                    tth3.tys_kaisya_cd = sm.tys_kaisya_cd ")
        sb.AppendLine("                    AND tth3.shri_jigyousyo_cd = sm.shri_jigyousyo_cd ")
        sb.AppendLine("                    AND s2.denpyou_siire_date BETWEEN DATEADD( ")
        sb.AppendLine("                        DAY")
        sb.AppendLine("                        , + 1")
        sb.AppendLine("                        , ISNULL(ukt.taisyou_nengetu, '1900/1/1')")
        sb.AppendLine("                    ) AND DATEADD(DAY, - 1, " & DBparamFromDate & ")")
        sb.AppendLine("                    AND ISNULL(s2.siire_keijyou_flg,0) = 1 ")
        sb.AppendLine("            ) siire_goukei")
        sb.AppendLine("            , ( ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    sum(isnull(h2.furikomi, 0)) + sum(isnull(h2.sousai, 0)) kurikosi_zan ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.m_tyousakaisya tt ")
        sb.AppendLine("                    INNER JOIN jhs_sys.t_siharai_data h2 ")
        sb.AppendLine("                        ON tt.skk_shri_saki_cd = h2.skk_shri_saki_cd ")
        sb.AppendLine("                        AND tt.skk_jigyousyo_cd = h2.skk_jigyou_cd ")
        sb.AppendLine("                WHERE")
        sb.AppendLine("                    tt.tys_kaisya_cd = sm.tys_kaisya_cd ")
        sb.AppendLine("                    AND tt.shri_jigyousyo_cd = sm.shri_jigyousyo_cd ")
        sb.AppendLine("                    AND h2.siharai_date BETWEEN DATEADD( ")
        sb.AppendLine("                        DAY")
        sb.AppendLine("                        , + 1")
        sb.AppendLine("                        , ISNULL(ukt.taisyou_nengetu, '1900/1/1')")
        sb.AppendLine("                    ) AND DATEADD(DAY, - 1, " & DBparamFromDate & ") ")
        sb.AppendLine("                GROUP BY")
        sb.AppendLine("                    tt.tys_kaisya_cd")
        sb.AppendLine("                    , tt.shri_jigyousyo_cd")
        sb.AppendLine("            ) siharai_goukei")
        sb.AppendLine("            , ukt.tougetu_kurikosi_zan ")
        sb.AppendLine("        FROM")
        sb.AppendLine("            jhs_sys.m_tyousakaisya sm ")
        sb.AppendLine("            LEFT OUTER JOIN ( ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    uk.taisyou_nengetu")
        sb.AppendLine("                    , uk.tys_kaisya_cd")
        sb.AppendLine("                    , uk.shri_jigyousyo_cd")
        sb.AppendLine("                    , uk.tougetu_kurikosi_zan ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_kaikake_data uk ")
        sb.AppendLine("                    LEFT OUTER JOIN ( ")
        sb.AppendLine("                        SELECT")
        sb.AppendLine("                            tys_kaisya_cd")
        sb.AppendLine("                            , shri_jigyousyo_cd")
        sb.AppendLine("                            , max(taisyou_nengetu) taisyou_nengetu ")
        sb.AppendLine("                        FROM")
        sb.AppendLine("                            jhs_sys.t_kaikake_data ")
        sb.AppendLine("                        WHERE")
        sb.AppendLine("                            taisyou_nengetu <= jhs_sys.fnGetLastDay(DATEADD(MONTH, - 1, " & DBparamFromDate & ")) ")
        sb.AppendLine("                        GROUP BY")
        sb.AppendLine("                            tys_kaisya_cd")
        sb.AppendLine("                            , shri_jigyousyo_cd")
        sb.AppendLine("                    ) ukm ")
        sb.AppendLine("                        ON uk.tys_kaisya_cd = ukm.tys_kaisya_cd ")
        sb.AppendLine("                        AND uk.shri_jigyousyo_cd = ukm.shri_jigyousyo_cd ")
        sb.AppendLine("                WHERE")
        sb.AppendLine("                    uk.taisyou_nengetu = ukm.taisyou_nengetu")
        sb.AppendLine("            ) ukt ")
        sb.AppendLine("                ON sm.tys_kaisya_cd = ukt.tys_kaisya_cd ")
        sb.AppendLine("                AND sm.shri_jigyousyo_cd = ukt.shri_jigyousyo_cd ")
        sb.AppendLine("        WHERE")
        sb.AppendLine("            sm.jigyousyo_cd = sm.shri_jigyousyo_cd")
        sb.AppendLine("    ) tkurikosi ")
        sb.AppendLine("WHERE")
        sb.AppendLine("    tkurikosi.tys_kaisya_cd + tkurikosi.shri_jigyousyo_cd = " & DBparamTyousaKaisyaCd & "")
        sb.AppendLine("")

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = Nothing

        ' パラメータを設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(DBparamTyousaKaisyaCd, SqlDbType.VarChar, 7, tysKaisyaCd), _
                                        SQLHelper.MakeParam(DBparamFromDate, SqlDbType.DateTime, 1, fromDate) _
                                        }

        'データ取得＆返却
        ' 請求締め日の取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       sb.ToString(), _
                                       sqlParams)

        Return data

    End Function

#End Region

#Region "支払先元帳_伝票データ取得"

    ''' <summary>
    ''' 支払先元帳_伝票データ取得
    ''' </summary>
    ''' <param name="tysKaisyaCd">調査会社コード</param>
    ''' <param name="fromDate">年月日FROM</param>
    ''' <param name="toDate">年月日TO</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function siharaiSakiMototyouDenpyouData(ByVal tysKaisyaCd As String, _
                                                   ByVal fromDate As Date, _
                                                   ByVal toDate As Date _
                                                   ) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".siharaiSakiMototyouDenpyouData", _
                                                    tysKaisyaCd, _
                                                    fromDate, _
                                                    toDate _
                                                    )

        ' SQL生成
        Dim sb As New StringBuilder()
        sb.AppendLine("SELECT")
        sb.AppendLine("    dd.* ")
        sb.AppendLine("FROM")
        sb.AppendLine("    ( ")
        sb.AppendLine("        SELECT")
        sb.AppendLine("            tu.denpyou_unique_no")
        sb.AppendLine("            , mt.tys_kaisya_cd")
        sb.AppendLine("            , mt.shri_jigyousyo_cd")
        sb.AppendLine("            , tu.denpyou_siire_date nengappi")
        sb.AppendLine("            , '仕入' kamoku")
        sb.AppendLine("            , tu.syouhin_cd")
        sb.AppendLine("            , tu.hinmei")
        sb.AppendLine("            , tu.kbn + tu.bangou kokyaku_no")
        sb.AppendLine("            , tj.jutyuu_bukken_mei bukken_mei")
        sb.AppendLine("            , tu.suu")
        sb.AppendLine("            , tu.tanka")
        sb.AppendLine("            , tu.siire_gaku zeinuki_gaku")
        sb.AppendLine("            , tu.sotozei_gaku")
        sb.AppendLine("            , tu.siire_gaku + tu.sotozei_gaku kingaku")
        sb.AppendLine("            , tu.denpyou_no ")
        sb.AppendLine("        FROM")
        sb.AppendLine("            jhs_sys.t_siire_data tu ")
        sb.AppendLine("            INNER JOIN jhs_sys.m_tyousakaisya mt ")
        sb.AppendLine("                ON mt.tys_kaisya_cd = tu.tys_kaisya_cd ")
        sb.AppendLine("                AND mt.jigyousyo_cd = tu.tys_kaisya_jigyousyo_cd ")
        sb.AppendLine("            LEFT OUTER JOIN jhs_sys.t_jiban tj ")
        sb.AppendLine("                ON tu.kbn = tj.kbn ")
        sb.AppendLine("                AND tu.bangou = tj.hosyousyo_no ")
        sb.AppendLine("        WHERE ")
        sb.AppendLine("            ISNULL(tu.siire_keijyou_flg,0) = 1 ")
        sb.AppendLine("        UNION ALL ")
        sb.AppendLine("        SELECT")
        sb.AppendLine("            siharai.denpyou_unique_no")
        sb.AppendLine("            , mt.tys_kaisya_cd")
        sb.AppendLine("            , mt.shri_jigyousyo_cd")
        sb.AppendLine("            , siharai.siharai_date")
        sb.AppendLine("            , '支払'")
        sb.AppendLine("            , NULL")
        sb.AppendLine("            , siharai.syouhin_mei")
        sb.AppendLine("            , NULL")
        sb.AppendLine("            , siharai.tekiyou_mei")
        sb.AppendLine("            , NULL")
        sb.AppendLine("            , NULL")
        sb.AppendLine("            , NULL")
        sb.AppendLine("            , NULL")
        sb.AppendLine("            , siharai.kingaku")
        sb.AppendLine("            , siharai.denpyou_no ")
        sb.AppendLine("        FROM")
        sb.AppendLine("            ( ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    denpyou_unique_no")
        sb.AppendLine("                    , skk_jigyou_cd")
        sb.AppendLine("                    , skk_shri_saki_cd")
        sb.AppendLine("                    , siharai_date")
        sb.AppendLine("                    , '振込' syouhin_mei")
        sb.AppendLine("                    , tekiyou_mei")
        sb.AppendLine("                    , furikomi kingaku")
        sb.AppendLine("                    , denpyou_no ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_siharai_data ")
        sb.AppendLine("                UNION ALL ")
        sb.AppendLine("                SELECT")
        sb.AppendLine("                    denpyou_unique_no")
        sb.AppendLine("                    , skk_jigyou_cd")
        sb.AppendLine("                    , skk_shri_saki_cd")
        sb.AppendLine("                    , siharai_date")
        sb.AppendLine("                    , '相殺' syouhin_mei")
        sb.AppendLine("                    , tekiyou_mei")
        sb.AppendLine("                    , sousai kingaku")
        sb.AppendLine("                    , denpyou_no ")
        sb.AppendLine("                FROM")
        sb.AppendLine("                    jhs_sys.t_siharai_data")
        sb.AppendLine("            ) siharai ")
        sb.AppendLine("            INNER JOIN jhs_sys.m_tyousakaisya mt ")
        sb.AppendLine("                ON siharai.skk_jigyou_cd = mt.skk_jigyousyo_cd ")
        sb.AppendLine("                AND siharai.skk_shri_saki_cd = mt.skk_shri_saki_cd ")
        sb.AppendLine("                AND mt.jigyousyo_cd = mt.shri_jigyousyo_cd ")
        sb.AppendLine("        WHERE")
        sb.AppendLine("            ISNULL(siharai.kingaku, 0) <> 0 ")
        sb.AppendLine("    ) dd ")
        sb.AppendLine("WHERE")
        sb.AppendLine("    tys_kaisya_cd + shri_jigyousyo_cd = " & DBparamTyousaKaisyaCd & " ")
        sb.AppendLine("    AND nengappi BETWEEN " & DBparamFromDate & " AND " & DBparamToDate & " ")
        sb.AppendLine("ORDER BY")
        sb.AppendLine("    tys_kaisya_cd")
        sb.AppendLine("    , nengappi")
        sb.AppendLine("    , kamoku")
        sb.AppendLine("    , denpyou_no")
        sb.AppendLine("    , hinmei")
        sb.AppendLine("")

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = Nothing

        ' パラメータを設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(DBparamTyousaKaisyaCd, SqlDbType.VarChar, 7, tysKaisyaCd), _
                                        SQLHelper.MakeParam(DBparamToDate, SqlDbType.DateTime, 1, toDate), _
                                        SQLHelper.MakeParam(DBparamFromDate, SqlDbType.DateTime, 1, fromDate) _
                                        }

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)

    End Function

#End Region

#End Region

End Class
