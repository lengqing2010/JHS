Imports System
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 売上データの取得クラスです
''' </summary>
''' <remarks></remarks>
Public Class UriageDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim SLogic As New StringLogic
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "売上データの取得"
    ''' <summary>
    ''' 売上データを取得します
    ''' </summary>
    ''' <param name="keyRec">区分</param>
    ''' <returns>売上データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetUriageDataInfo(ByVal keyRec As UriageDataKeyRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataInfo", keyRec)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        Dim strCmnSql As String = GetUriageCmnSql(keyRec)
        Dim cmdParams() As SqlParameter = GetUriSqlCmnParams(keyRec)
        Dim cmnDtAcc As New CmnDataAccess

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      URI.denpyou_unique_no ")
        cmdTextSb.Append("    , URI.denpyou_no ")
        cmdTextSb.Append("    , URI.denpyou_syubetu ")
        cmdTextSb.Append("    , URI.torikesi_moto_denpyou_unique_no ")
        cmdTextSb.Append("    , URI.kbn ")
        cmdTextSb.Append("    , URI.bangou ")
        cmdTextSb.Append("    , URI.himoduke_cd ")
        cmdTextSb.Append("    , URI.himoduke_table_type ")
        cmdTextSb.Append("    , URI.uri_date ")
        cmdTextSb.Append("    , URI.denpyou_uri_date ")
        cmdTextSb.Append("    , URI.uri_keijyou_flg ")
        cmdTextSb.Append("    , URI.seikyuu_date ")
        cmdTextSb.Append("    , URI.seikyuu_saki_cd ")
        cmdTextSb.Append("    , URI.seikyuu_saki_brc ")
        cmdTextSb.Append("    , URI.seikyuu_saki_kbn ")
        cmdTextSb.Append("    , URI.seikyuu_saki_mei ")
        cmdTextSb.Append("    , URI.syouhin_cd ")
        cmdTextSb.Append("    , URI.hinmei ")
        cmdTextSb.Append("    , URI.suu ")
        cmdTextSb.Append("    , URI.tani ")
        cmdTextSb.Append("    , URI.tanka ")
        cmdTextSb.Append("    , URI.uri_gaku ")
        cmdTextSb.Append("    , URI.sotozei_gaku ")
        cmdTextSb.Append("    , URI.zei_kbn ")
        cmdTextSb.Append("    , URI.kameiten_cd ")
        cmdTextSb.Append("    , ( ")
        cmdTextSb.Append("           CASE ")
        cmdTextSb.Append("                WHEN Nullif(URI.kbn, '') IS NOT NULL ")
        cmdTextSb.Append("                 AND Nullif(URI.bangou, '') IS NOT NULL ")
        cmdTextSb.Append("                THEN ")
        cmdTextSb.Append("                     CASE ")
        cmdTextSb.Append("                          WHEN Nullif(TJB.sesyu_mei, '') IS NOT NULL ")
        cmdTextSb.Append("                          THEN TJB.sesyu_mei ")
        cmdTextSb.Append("                          WHEN Nullif(THU.kbn, '') IS NOT NULL ")
        cmdTextSb.Append("                           AND Nullif(THU.bangou, '') IS NOT NULL ")
        cmdTextSb.Append("                          THEN THU.sesyu_mei ")
        cmdTextSb.Append("                          ELSE NULL ")
        cmdTextSb.Append("                     END ")
        cmdTextSb.Append("                ELSE NULL ")
        cmdTextSb.Append("           END) AS sesyu_mei ")
        cmdTextSb.Append("    , MKT.kameiten_mei1 ")
        cmdTextSb.Append("    , URI.add_login_user_id ")
        cmdTextSb.Append("    , URI.add_datetime ")
        cmdTextSb.Append("    , URI.upd_login_user_id ")
        cmdTextSb.Append("    , URI.upd_datetime ")
        cmdTextSb.Append(strCmnSql)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 売上データテーブルをPKで取得します
    ''' </summary>
    ''' <param name="strDenUnqNo">伝票ユニークNO</param>
    ''' <returns>売上データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetUriageDataRec(ByVal strDenUnqNo As String) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataRec" _
                                            , strDenUnqNo _
                                            )

        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()
        Dim cmnDtAcc As New CmnDataAccess

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("     URI.denpyou_unique_no ")
        cmdTextSb.Append("   , URI.denpyou_no ")
        cmdTextSb.Append("   , URI.denpyou_syubetu ")
        cmdTextSb.Append("   , URI.torikesi_moto_denpyou_unique_no ")
        cmdTextSb.Append("   , URI.kbn ")
        cmdTextSb.Append("   , URI.bangou ")
        cmdTextSb.Append("   , URI.himoduke_cd ")
        cmdTextSb.Append("   , URI.himoduke_table_type ")
        cmdTextSb.Append("   , URI.uri_date ")
        cmdTextSb.Append("   , URI.denpyou_uri_date ")
        cmdTextSb.Append("   , URI.uri_keijyou_flg ")
        cmdTextSb.Append("   , URI.seikyuu_date ")
        cmdTextSb.Append("   , URI.seikyuu_saki_cd ")
        cmdTextSb.Append("   , URI.seikyuu_saki_brc ")
        cmdTextSb.Append("   , URI.seikyuu_saki_kbn ")
        cmdTextSb.Append("   , URI.seikyuu_saki_mei ")
        cmdTextSb.Append("   , URI.syouhin_cd ")
        cmdTextSb.Append("   , URI.hinmei ")
        cmdTextSb.Append("   , URI.suu ")
        cmdTextSb.Append("   , URI.tani ")
        cmdTextSb.Append("   , URI.tanka ")
        cmdTextSb.Append("   , URI.uri_gaku ")
        cmdTextSb.Append("   , URI.sotozei_gaku ")
        cmdTextSb.Append("   , URI.zei_kbn ")
        cmdTextSb.Append("   , URI.add_login_user_id ")
        cmdTextSb.Append("   , URI.add_datetime ")
        cmdTextSb.Append("   , URI.upd_login_user_id ")
        cmdTextSb.Append("   , URI.upd_datetime ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("   t_uriage_data URI WITH (READCOMMITTED) ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append(" URI.denpyou_unique_no = " & DBParamDenUnqNo)

        Dim cmdParams() As SqlParameter = {SQLHelper.MakeParam(DBParamDenUnqNo, SqlDbType.Int, 6, strDenUnqNo)}

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 売上データ[CSV出力用]を取得します
    ''' </summary>
    ''' <param name="keyRec">売上データKeyレコード</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetUriageDataCsv(ByVal keyRec As UriageDataKeyRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataInfo", keyRec)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        Dim strCmnSql As String = GetUriageCmnSql(keyRec)
        Dim cmdParams() As SqlParameter = GetUriSqlCmnParams(keyRec)
        Dim cmnDtAcc As New CmnDataAccess

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      URI.denpyou_unique_no '伝票ユニークNO' ")
        cmdTextSb.Append("    , URI.denpyou_no '伝票番号' ")
        cmdTextSb.Append("    , URI.denpyou_syubetu '伝票種別' ")
        cmdTextSb.Append("    , URI.torikesi_moto_denpyou_unique_no '取消元伝票ユニークNO' ")
        cmdTextSb.Append("    , URI.kbn '区分' ")
        cmdTextSb.Append("    , URI.bangou '番号' ")
        cmdTextSb.Append("    , ISNULL(TJB.sesyu_mei, THU.sesyu_mei) '施主名' ")
        cmdTextSb.Append("    , TJB.kameiten_cd '加盟店コード' ")
        cmdTextSb.Append("    , MKT.kameiten_mei1 '加盟店名' ")
        cmdTextSb.Append("    , URI.himoduke_cd '紐付けコード' ")
        cmdTextSb.Append("    , URI.himoduke_table_type '紐付け元テーブル種別' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, URI.uri_date, 111) '売上年月日' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, URI.denpyou_uri_date, 111) '伝票売上年月日' ")
        cmdTextSb.Append("    , URI.uri_keijyou_flg '売上処理FLG' ")
        cmdTextSb.Append("    , URI.kameiten_cd '加盟店コード' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, URI.seikyuu_date, 111) '請求年月日' ")
        cmdTextSb.Append("    , URI.seikyuu_saki_cd '請求先コード' ")
        cmdTextSb.Append("    , URI.seikyuu_saki_brc '請求先枝番' ")
        cmdTextSb.Append("    , URI.seikyuu_saki_kbn '請求先区分' ")
        cmdTextSb.Append("    , URI.seikyuu_saki_mei '請求先名' ")
        cmdTextSb.Append("    , URI.syouhin_cd '商品コード' ")
        cmdTextSb.Append("    , URI.hinmei '品名' ")
        cmdTextSb.Append("    , URI.suu '数量' ")
        cmdTextSb.Append("    , URI.tani '単位' ")
        cmdTextSb.Append("    , URI.tanka '単価' ")
        cmdTextSb.Append("    , URI.syanai_genka '社内原価' ")
        cmdTextSb.Append("    , URI.uri_gaku '売上金額' ")
        cmdTextSb.Append("    , URI.sotozei_gaku '外税額' ")
        cmdTextSb.Append("    , URI.zei_kbn '税区分' ")
        cmdTextSb.Append("    , URI.add_login_user_id '登録ログインユーザーID' ")
        cmdTextSb.Append("    , URI.add_login_user_name '登録ログインユーザー名' ")
        cmdTextSb.Append("    , URI.add_datetime '登録日時' ")
        cmdTextSb.Append(strCmnSql)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 売上データ取得用の共通SQLクエリを取得
    ''' </summary>
    ''' <param name="keyRec">売上データKeyレコード</param>
    ''' <returns>売上データ取得用の共通SQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetUriageCmnSql(ByVal keyRec As UriageDataKeyRecord) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageCmnSql", keyRec)
        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("   t_uriage_data URI WITH (READCOMMITTED) ")
        cmdTextSb.Append(" LEFT OUTER JOIN t_jiban TJB")
        cmdTextSb.Append("   ON URI.kbn          = TJB.kbn")
        cmdTextSb.Append("  AND URI.bangou = TJB.hosyousyo_no")
        cmdTextSb.Append(" LEFT OUTER JOIN m_kameiten MKT")
        cmdTextSb.Append("   ON URI.kameiten_cd  = MKT.kameiten_cd")
        cmdTextSb.Append(" LEFT OUTER JOIN t_hannyou_uriage THU ")
        cmdTextSb.Append("   ON URI.himoduke_cd = CAST(THU.han_uri_unique_no AS varchar) ")
        '***********************************************************************
        ' 請求先名カナ
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiMeiKana) Then
            cmdTextSb.Append(" LEFT OUTER JOIN v_seikyuu_saki_info VIW WITH (READCOMMITTED) ")
            cmdTextSb.Append("    ON URI.seikyuu_saki_cd = VIW.seikyuu_saki_cd ")
            cmdTextSb.Append("   AND URI.seikyuu_saki_brc = VIW.seikyuu_saki_brc ")
            cmdTextSb.Append("   AND URI.seikyuu_saki_kbn = VIW.seikyuu_saki_kbn ")
        End If
        cmdTextSb.Append(" WHERE 1 = 1 ")
        '***********************************************************************
        ' 請求先名カナ
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiMeiKana) Then
            cmdTextSb.Append("   AND VIW.seikyuu_saki_kana like " & DBparamSeikyuuSakiMeiKana)
        End If

        '***********************************************************************
        '区分
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.Kbn) Then
            cmdTextSb.Append(" AND URI.kbn = " & DBparamKbn)
        End If

        '***********************************************************************
        '伝票ユニーク番号
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.DenUnqNo) Then
            cmdTextSb.Append(" AND ")
            cmdTextSb.Append(" URI.denpyou_unique_no = " & DBParamDenUnqNo)
        End If


        '***********************************************************************
        '番号
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.BangouFrom) Or Not String.IsNullOrEmpty(keyRec.BangouTo) Then

            cmdTextSb.Append(" AND ")

            If Not (keyRec.BangouFrom Is String.Empty) And Not (keyRec.BangouTo Is String.Empty) Then
                ' 両方指定有りはBETWEEN
                cmdTextSb.Append(" URI.bangou BETWEEN " & DBparamBangouFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBparamBangouTo)
            Else
                If Not keyRec.BangouFrom Is String.Empty Then
                    ' 番号Fromのみ
                    cmdTextSb.Append(" URI.bangou >= " & DBparamBangouFrom)
                Else
                    ' 番号Toのみ
                    cmdTextSb.Append(" URI.bangou <= " & DBparamBangouTo)
                End If
            End If
        End If

        '***********************************************************************
        '伝票番号
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.DenNoFrom) Or Not String.IsNullOrEmpty(keyRec.DenNoTo) Then

            cmdTextSb.Append(" AND ")

            If Not (keyRec.DenNoFrom Is String.Empty) And Not (keyRec.DenNoTo Is String.Empty) Then
                ' 両方指定有りはBETWEEN
                cmdTextSb.Append(" URI.denpyou_no BETWEEN " & DBparamDenNoFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBparamDenNoTo)
            Else
                If Not keyRec.DenNoFrom Is String.Empty Then
                    ' 伝票番号Fromのみ
                    cmdTextSb.Append(" URI.denpyou_no >= " & DBparamDenNoFrom)
                Else
                    ' 伝票番号Toのみ
                    cmdTextSb.Append(" URI.denpyou_no <= " & DBparamDenNoTo)
                End If
            End If
        End If

        '***********************************************************************
        '伝票作成日
        '***********************************************************************
        '日付が一つでも設定されている場合、条件を作成
        If keyRec.AddDatetimeFrom <> DateTime.MinValue Or _
            keyRec.AddDatetimeTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.AddDatetimeFrom <> DateTime.MinValue And _
                keyRec.AddDatetimeTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, URI.add_datetime ,111) BETWEEN " & DBparamAddDatetimeFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBparamAddDatetimeTo)
            Else
                If keyRec.AddDatetimeFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, URI.add_datetime ,111) >= " & DBparamAddDatetimeFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, URI.add_datetime ,111) <= " & DBparamAddDatetimeTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 商品コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SyouhinCd) Then
            cmdTextSb.Append(" AND URI.syouhin_cd like " & DBparamSyouhinCd)
        End If

        '***********************************************************************
        ' 加盟店コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.KameitenCd) Then
            cmdTextSb.Append(" AND URI.kameiten_cd like " & DBparamKameitenCd)
        End If

        '***********************************************************************
        ' 請求先区分
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiKbn) Then
            cmdTextSb.Append(" AND URI.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn)
        End If

        '***********************************************************************
        ' 請求先コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiCd) Then
            cmdTextSb.Append(" AND URI.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd)
        End If

        '***********************************************************************
        ' 請求先枝番
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiBrc) Then
            cmdTextSb.Append(" AND URI.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc)
        End If

        '***********************************************************************
        ' 請求年月日
        '***********************************************************************
        '日付が一つでも設定されている場合、条件を作成
        If keyRec.SeikyuuDateFrom <> DateTime.MinValue Or _
            keyRec.SeikyuuDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.SeikyuuDateFrom <> DateTime.MinValue And _
                keyRec.SeikyuuDateTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, URI.seikyuu_date ,111) BETWEEN " & DBparamSeikyuuDateFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBparamSeikyuuDateTo)
            Else
                If keyRec.SeikyuuDateFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, URI.seikyuu_date ,111) >= " & DBparamSeikyuuDateFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, URI.seikyuu_date ,111) <= " & DBparamSeikyuuDateTo)
                End If
            End If
        End If


        '***********************************************************************
        ' 売上年月日
        '***********************************************************************
        '日付が一つでも設定されている場合、条件を作成
        If keyRec.UriDateFrom <> DateTime.MinValue Or _
            keyRec.UriDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.UriDateFrom <> DateTime.MinValue And _
                keyRec.UriDateTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, URI.uri_date ,111) BETWEEN " & DBparamUriDateFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBparamUriDateTo)
            Else
                If keyRec.UriDateFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, URI.uri_date ,111) >= " & DBparamUriDateFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, URI.uri_date ,111) <= " & DBparamUriDateTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 伝票売上年月日
        '***********************************************************************
        '日付が一つでも設定されている場合、条件を作成
        If keyRec.DenUriDateFrom <> DateTime.MinValue Or _
            keyRec.DenUriDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.DenUriDateFrom <> DateTime.MinValue And _
                keyRec.DenUriDateTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, URI.denpyou_uri_date ,111) BETWEEN " & DBparamDenUriDateFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBparamDenUriDateTo)
            Else
                If keyRec.DenUriDateFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, URI.denpyou_uri_date ,111) >= " & DBparamDenUriDateFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, URI.denpyou_uri_date ,111) <= " & DBparamDenUriDateTo)
                End If
            End If
        End If

        '***********************************************************************
        '最新伝票のみ表示
        '***********************************************************************
        If keyRec.NewDenpyouDisp = 0 Then
            cmdTextSb.Append(" AND ")
            cmdTextSb.Append("  URI.denpyou_unique_no IN ( ")
            cmdTextSb.Append(" SELECT ")
            cmdTextSb.Append("  MAX(URI2.denpyou_unique_no) ")
            cmdTextSb.Append(" FROM ")
            cmdTextSb.Append("  t_uriage_data URI2 ")
            cmdTextSb.Append(" GROUP BY ")
            cmdTextSb.Append("  REPLACE (URI2.himoduke_cd, '$$$115$$$', '$$$110$$$') ")
            cmdTextSb.Append("  , URI2.himoduke_table_type ")
            cmdTextSb.Append(" ) ")
        End If

        '***********************************************************************
        'マイナス伝票のみ表示
        '***********************************************************************
        If keyRec.MinusDenpyouDisp = 0 Then
            cmdTextSb.Append(" AND ")
            cmdTextSb.Append("  URI.denpyou_syubetu = '" & EarthConst.UR & "'")
        End If

        '***********************************************************************
        '計上済み伝票のみ表示
        '***********************************************************************
        If keyRec.KeijyouZumiDisp = 0 Then
            cmdTextSb.Append(" AND ")
            cmdTextSb.Append("  URI.uri_keijyou_flg = '1'")
        End If

        '***********************************************************************
        '表示順序の付与（伝票ユニークNO→売上年月日→伝票番号）
        '***********************************************************************
        cmdTextSb.Append(" ORDER BY ")
        cmdTextSb.Append("     URI.denpyou_unique_no ")
        cmdTextSb.Append("   , URI.uri_date ")
        cmdTextSb.Append("   , URI.denpyou_no ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 売上データ取得用の共通SQLパラメータを取得
    ''' </summary>
    ''' <param name="keyRec">売上データKeyレコード</param>
    ''' <returns>売上データ取得用の共通SQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetUriSqlCmnParams(ByVal keyRec As UriageDataKeyRecord) As SqlParameter()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriSqlCmnParams", keyRec)

        Dim dtAddDateFrom As Object = IIf(keyRec.AddDatetimeFrom = DateTime.MinValue, DBNull.Value, keyRec.AddDatetimeFrom)
        Dim dtAddDateTo As Object = IIf(keyRec.AddDatetimeTo = DateTime.MinValue, DBNull.Value, keyRec.AddDatetimeTo)
        Dim dtSeikyuuFrom As Object = IIf(keyRec.SeikyuuDateFrom = DateTime.MinValue, DBNull.Value, keyRec.SeikyuuDateFrom)
        Dim dtSeikyuuTo As Object = IIf(keyRec.SeikyuuDateTo = DateTime.MinValue, DBNull.Value, keyRec.SeikyuuDateTo)
        Dim dtUriFrom As Object = IIf(keyRec.UriDateFrom = DateTime.MinValue, DBNull.Value, keyRec.UriDateFrom)
        Dim dtUriTo As Object = IIf(keyRec.UriDateTo = DateTime.MinValue, DBNull.Value, keyRec.UriDateTo)
        Dim dtDenUriFrom As Object = IIf(keyRec.DenUriDateFrom = DateTime.MinValue, DBNull.Value, keyRec.DenUriDateFrom)
        Dim dtDenUriTo As Object = IIf(keyRec.DenUriDateTo = DateTime.MinValue, DBNull.Value, keyRec.DenUriDateTo)
        'パラメータへ設定
        Dim cmdParams() As SqlParameter = _
                {SQLHelper.MakeParam(DBparamKbn, SqlDbType.VarChar, 1, keyRec.Kbn), _
                 SQLHelper.MakeParam(DBparamBangouFrom, SqlDbType.VarChar, 10, keyRec.BangouFrom), _
                 SQLHelper.MakeParam(DBparamBangouTo, SqlDbType.VarChar, 10, keyRec.BangouTo), _
                 SQLHelper.MakeParam(DBParamDenUnqNo, SqlDbType.Int, 6, keyRec.DenUnqNo), _
                 SQLHelper.MakeParam(DBparamDenNoFrom, SqlDbType.Char, 5, keyRec.DenNoFrom), _
                 SQLHelper.MakeParam(DBparamDenNoTo, SqlDbType.Char, 5, keyRec.DenNoTo), _
                 SQLHelper.MakeParam(DBparamAddDatetimeFrom, SqlDbType.DateTime, 16, dtAddDateFrom), _
                 SQLHelper.MakeParam(DBparamAddDatetimeTo, SqlDbType.DateTime, 16, dtAddDateTo), _
                 SQLHelper.MakeParam(DBparamSyouhinCd, SqlDbType.VarChar, 8, keyRec.SyouhinCd), _
                 SQLHelper.MakeParam(DBparamKameitenCd, SqlDbType.VarChar, 5, keyRec.KameitenCd), _
                 SQLHelper.MakeParam(DBparamSeikyuuSakiKbn, SqlDbType.Char, 1, keyRec.SeikyuuSakiKbn), _
                 SQLHelper.MakeParam(DBparamSeikyuuSakiCd, SqlDbType.VarChar, 5, keyRec.SeikyuuSakiCd), _
                 SQLHelper.MakeParam(DBparamSeikyuuSakiBrc, SqlDbType.VarChar, 2, keyRec.SeikyuuSakiBrc), _
                 SQLHelper.MakeParam(DBparamSeikyuuSakiMeiKana, SqlDbType.VarChar, 100, keyRec.SeikyuuSakiMeiKana & Chr(37)), _
                 SQLHelper.MakeParam(DBparamSeikyuuDateFrom, SqlDbType.DateTime, 16, dtSeikyuuFrom), _
                 SQLHelper.MakeParam(DBparamSeikyuuDateTo, SqlDbType.DateTime, 16, dtSeikyuuTo), _
                 SQLHelper.MakeParam(DBparamUriDateFrom, SqlDbType.DateTime, 16, dtUriFrom), _
                 SQLHelper.MakeParam(DBparamUriDateTo, SqlDbType.DateTime, 16, dtUriTo), _
                 SQLHelper.MakeParam(DBparamDenUriDateFrom, SqlDbType.DateTime, 16, dtDenUriFrom), _
                 SQLHelper.MakeParam(DBparamDenUriDateTo, SqlDbType.DateTime, 16, dtDenUriTo)}

        Return cmdParams

    End Function

#Region "SQLパラメータ"
    Private Const DBparamKbn As String = "@KBN"
    Private Const DBparamBangouFrom As String = "@BANGOUFROM"
    Private Const DBparamBangouTo As String = "@BANGOUTO"
    Private Const DBParamDenUnqNo As String = "@DENUNQNO"
    Private Const DBparamDenNoFrom As String = "@DENNOFROM"
    Private Const DBparamDenNoTo As String = "@DENNOTO"
    Private Const DBparamAddDatetimeFrom As String = "@ADDDATETIMEFROM"
    Private Const DBparamAddDatetimeTo As String = "@ADDDATETIMETO"
    Private Const DBparamSyouhinCd As String = "@SYOUHINCD"
    Private Const DBparamKameitenCd As String = "@KAMEITENCD"
    Private Const DBparamSeikyuuSakiKbn As String = "@SEIKYUUSAKIKBN"
    Private Const DBparamSeikyuuSakiCd As String = "@SEIKYUUSAKICD"
    Private Const DBparamSeikyuuSakiBrc As String = "@SEIKYUUSAKIBRC"
    Private Const DBparamSeikyuuSakiMei As String = "@SEIKYUUSAKIMEI"
    Private Const DBparamSeikyuuSakiMeiKana As String = "@SEIKYUUSAKIMEIKANA"
    Private Const DBparamSeikyuuDateFrom As String = "@SEIKYUUDATEFROM"
    Private Const DBparamSeikyuuDateTo As String = "@SEIKYUUDATETO"
    Private Const DBparamUriDateFrom As String = "@URIDATEFROM"
    Private Const DBparamUriDateTo As String = "@URIDATETO"
    Private Const DBparamDenUriDateFrom As String = "@DENURIDATEFROM"
    Private Const DBparamDenUriDateTo As String = "@DENURIDATETO"
    Private Const DBparamTysKaisyaCd As String = "@TYSKAISYACD"
    Private Const DBparamEigyousyoCd As String = "@EIGYOUSYOCD"
#End Region

#End Region

#Region "汎用処理"
#Region "請求先情報取得"
    ''' <summary>
    ''' 請求先情報取得
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="seikyuuSakiMei">請求先名</param>
    ''' <param name="seikyuuSakiKana">請求先カナ</param>
    ''' <param name="torikesiFlg">取消データ対象フラグ(true:対象にしない / false:対象にする)</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function searchSeikyuuSakiInfo(ByVal seikyuuSakiCd As String, _
                                          ByVal seikyuuSakiBrc As String, _
                                          ByVal seikyuuSakiKbn As String, _
                                          ByVal seikyuuSakiMei As String, _
                                          ByVal seikyuuSakiKana As String, _
                                          Optional ByVal torikesiFlg As Boolean = False _
                                          ) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".searchSeikyuuSakiInfo", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    seikyuuSakiMei, _
                                                    seikyuuSakiKana, _
                                                    torikesiFlg)
        ' SQL生成
        Dim sqlSb As New StringBuilder()
        sqlSb.Append(" SELECT ")
        sqlSb.Append("     LTRIM(RTRIM(seikyuu_saki_cd)) seikyuu_saki_cd ")
        sqlSb.Append("   , LTRIM(RTRIM(seikyuu_saki_brc)) seikyuu_saki_brc ")
        sqlSb.Append("   , LTRIM(RTRIM(seikyuu_saki_kbn)) seikyuu_saki_kbn ")
        sqlSb.Append("   , seikyuu_saki_mei ")
        sqlSb.Append("   , seikyuu_saki_kana ")
        sqlSb.Append("   , skysy_soufu_jyuusyo1 ")
        sqlSb.Append("   , skysy_soufu_jyuusyo2 ")
        sqlSb.Append("   , skysy_soufu_yuubin_no ")
        sqlSb.Append("   , skysy_soufu_tel_no ")
        sqlSb.Append("   , skysy_soufu_fax_no  ")
        sqlSb.Append(" FROM ")
        sqlSb.Append("   [jhs_sys].[v_seikyuu_saki_info]  ")
        sqlSb.Append(" WHERE ")
        sqlSb.Append("   0 = 0 ")
        If seikyuuSakiCd IsNot String.Empty Then
            sqlSb.Append("   AND seikyuu_saki_cd LIKE " & DBparamSeikyuuSakiCd)
        End If
        If seikyuuSakiBrc IsNot String.Empty Then
            sqlSb.Append("   AND seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc)
        End If
        If seikyuuSakiKbn IsNot String.Empty Then
            sqlSb.Append("   AND seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn)
        End If
        If seikyuuSakiMei IsNot String.Empty Then
            sqlSb.Append("   AND seikyuu_saki_mei LIKE " & DBparamSeikyuuSakiMei)
        End If
        If seikyuuSakiKana IsNot String.Empty Then
            sqlSb.Append("   AND seikyuu_saki_kana LIKE " & DBparamSeikyuuSakiMeiKana)
        End If
        If torikesiFlg Then
            sqlSb.Append("   AND ISNULL(torikesi,1) = 0 ")
        End If

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = Nothing

        ' パラメータを設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiCd, SqlDbType.VarChar, 7, seikyuuSakiCd & Chr(37)), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiBrc, SqlDbType.VarChar, 2, seikyuuSakiBrc), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiKbn, SqlDbType.VarChar, 1, seikyuuSakiKbn), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiMei, SqlDbType.VarChar, 100, seikyuuSakiMei & Chr(37)), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiMeiKana, SqlDbType.VarChar, 100, seikyuuSakiKana & Chr(37)) _
                                        }

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sqlSb.ToString, sqlParams)

    End Function

    ''' <summary>
    ''' 請求先情報取得[PK]
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="torikesiFlg">取消データ対象フラグ(true:対象にしない / false:対象にする)</param>
    ''' <returns>請求先情報データテーブル</returns>
    ''' <remarks></remarks>
    Public Function searchSeikyuuSakiInfo(ByVal seikyuuSakiCd As String, _
                                          ByVal seikyuuSakiBrc As String, _
                                          ByVal seikyuuSakiKbn As String, _
                                          Optional ByVal torikesiFlg As Boolean = False _
                                          ) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".searchSeikyuuSakiInfo", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    torikesiFlg)

        Dim seikyuuSakiInfoDataTable As New UriageDataSet.SeikyuuSakiInfoDataTable
        Dim sqlSb As New StringBuilder()

        If seikyuuSakiCd = String.Empty Or seikyuuSakiBrc = String.Empty Or seikyuuSakiKbn = String.Empty Then
            Return seikyuuSakiInfoDataTable
            Exit Function
        End If

        ' SQL生成
        sqlSb.Append(" SELECT ")
        sqlSb.Append("     LTRIM(RTRIM(seikyuu_saki_cd)) seikyuu_saki_cd ")
        sqlSb.Append("   , LTRIM(RTRIM(seikyuu_saki_brc)) seikyuu_saki_brc ")
        sqlSb.Append("   , LTRIM(RTRIM(seikyuu_saki_kbn)) seikyuu_saki_kbn ")
        sqlSb.Append("   , seikyuu_saki_mei ")
        sqlSb.Append("   , seikyuu_saki_kana ")
        sqlSb.Append("   , skysy_soufu_jyuusyo1 ")
        sqlSb.Append("   , skysy_soufu_jyuusyo2 ")
        sqlSb.Append("   , skysy_soufu_yuubin_no ")
        sqlSb.Append("   , skysy_soufu_tel_no ")
        sqlSb.Append("   , skysy_soufu_fax_no  ")
        sqlSb.Append(" FROM ")
        sqlSb.Append("   [jhs_sys].[v_seikyuu_saki_info]  ")
        sqlSb.Append(" WHERE ")
        sqlSb.Append("   0 = 0 ")
        sqlSb.Append("   AND seikyuu_saki_cd = " & DBparamSeikyuuSakiCd)
        sqlSb.Append("   AND seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc)
        sqlSb.Append("   AND seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn)
        If torikesiFlg Then
            sqlSb.Append("   AND ISNULL(torikesi,1) = 0 ")
        End If

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = {SQLHelper.MakeParam(DBparamSeikyuuSakiCd, SqlDbType.VarChar, 5, seikyuuSakiCd), _
                                            SQLHelper.MakeParam(DBparamSeikyuuSakiBrc, SqlDbType.VarChar, 2, seikyuuSakiBrc), _
                                            SQLHelper.MakeParam(DBparamSeikyuuSakiKbn, SqlDbType.VarChar, 1, seikyuuSakiKbn)}
        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sqlSb.ToString, sqlParams)

    End Function

    ''' <summary>
    ''' 請求先情報検索結果件数取得
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="seikyuuSakiMei">請求先名</param>
    ''' <param name="seikyuuSakiKana">請求先カナ</param>
    ''' <param name="torikesiFlg">取消データ対象フラグ(true:対象にしない / false:対象にする)</param>
    ''' <returns>請求先情報検索結果件数</returns>
    ''' <remarks></remarks>
    Public Function searchSeikyuuSakiCnt(ByVal seikyuuSakiCd As String, _
                                          ByVal seikyuuSakiBrc As String, _
                                          ByVal seikyuuSakiKbn As String, _
                                          ByVal seikyuuSakiMei As String, _
                                          ByVal seikyuuSakiKana As String, _
                                          Optional ByVal torikesiFlg As Boolean = False _
                                          ) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".searchSeikyuuSakiCnt", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    seikyuuSakiMei, _
                                                    seikyuuSakiKana, _
                                                    torikesiFlg)
        ' SQL生成
        Dim sqlSb As New StringBuilder()
        sqlSb.Append(" SELECT ")
        sqlSb.Append("     count(seikyuu_saki_cd) rec_count")
        sqlSb.Append(" FROM ")
        sqlSb.Append("   [jhs_sys].[v_seikyuu_saki_info]  ")
        sqlSb.Append(" WHERE ")
        sqlSb.Append("   0 = 0 ")
        If seikyuuSakiCd IsNot String.Empty Then
            sqlSb.Append("   AND seikyuu_saki_cd LIKE " & DBparamSeikyuuSakiCd)
        End If
        If seikyuuSakiBrc IsNot String.Empty Then
            sqlSb.Append("   AND seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc)
        End If
        If seikyuuSakiKbn IsNot String.Empty Then
            sqlSb.Append("   AND seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn)
        End If
        If seikyuuSakiMei IsNot String.Empty Then
            sqlSb.Append("   AND seikyuu_saki_mei LIKE " & DBparamSeikyuuSakiMei)
        End If
        If seikyuuSakiKana IsNot String.Empty Then
            sqlSb.Append("   AND seikyuu_saki_kana LIKE " & DBparamSeikyuuSakiMeiKana)
        End If
        If torikesiFlg Then
            sqlSb.Append("   AND ISNULL(torikesi,1) = 0 ")
        End If

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = Nothing

        ' パラメータを設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiCd, SqlDbType.VarChar, 7, seikyuuSakiCd & Chr(37)), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiBrc, SqlDbType.VarChar, 2, seikyuuSakiBrc), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiKbn, SqlDbType.VarChar, 1, seikyuuSakiKbn), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiMei, SqlDbType.VarChar, 100, seikyuuSakiMei & Chr(37)), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiMeiKana, SqlDbType.VarChar, 100, seikyuuSakiKana & Chr(37)) _
                                        }

        ' 検索結果件数の取得
        Dim data As Object = Nothing

        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       sqlSb.ToString(), _
                                       sqlParams)

        ' 取得出来ない場合、0を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return 0
        End If

        Return data

    End Function

    ''' <summary>
    ''' 請求先情報取得(加盟店から)
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="blnTorikesi">取消データ対象フラグ(true:対象にしない / false:対象にする)</param>
    ''' <returns>請求先情報データテーブル</returns>
    ''' <remarks></remarks>
    Public Function searchSeikyuuSakiFromKameiten( _
                                          ByVal strKameitenCd As String, _
                                          ByVal strSyouhinCd As String, _
                                          Optional ByVal blnTorikesi As Boolean = False _
                                          ) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".searchKameitenSeikyuuSakiInfo", _
                                                    strKameitenCd, _
                                                    strSyouhinCd, _
                                                    blnTorikesi)
        ' SQL生成
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      LTRIM(RTRIM(seikyuu_saki_cd)) seikyuu_saki_cd")
        cmdTextSb.Append("    , LTRIM(RTRIM(seikyuu_saki_brc)) seikyuu_saki_brc")
        cmdTextSb.Append("    , LTRIM(RTRIM(seikyuu_saki_kbn)) seikyuu_saki_kbn")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      [jhs_sys].[v_syouhin_seikyuusaki_kameiten]")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      kameiten_cd = " & DBparamKameitenCd)
        cmdTextSb.Append("  AND syouhin_cd = " & DBparamSyouhinCd)
        If blnTorikesi Then
            cmdTextSb.Append("   AND ISNULL(torikesi,1) = 0 ")
        End If

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = Nothing

        ' パラメータを設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(DBparamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd), _
                                        SQLHelper.MakeParam(DBparamSyouhinCd, SqlDbType.VarChar, 8, strSyouhinCd) _
                                        }
        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, sqlParams)

    End Function

    ''' <summary>
    ''' 請求締日取得(請求先Mから)
    ''' </summary>
    ''' <param name="seikyuuSakiCd">請求先コード</param>
    ''' <param name="seikyuuSakiBrc">請求先枝番</param>
    ''' <param name="seikyuuSakiKbn">請求先区分</param>
    ''' <param name="torikesiFlg">取消</param>
    ''' <returns>請求締め日</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSimeDate(ByVal seikyuuSakiCd As String, _
                                          ByVal seikyuuSakiBrc As String, _
                                          ByVal seikyuuSakiKbn As String, _
                                          Optional ByVal torikesiFlg As Boolean = False _
                                          ) As String
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      seikyuu_sime_date")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      m_seikyuu_saki")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      seikyuu_saki_cd = " & DBparamSeikyuuSakiCd)
        cmdTextSb.Append("  AND seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc)
        cmdTextSb.Append("  AND seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn)
        If torikesiFlg Then
            cmdTextSb.Append("   AND ISNULL(torikesi,1) = 0 ")
        End If

        ' パラメータへ設定
        Dim cmdParams() As SqlParameter = {SQLHelper.MakeParam(DBparamSeikyuuSakiCd, SqlDbType.VarChar, 5, seikyuuSakiCd), _
                                            SQLHelper.MakeParam(DBparamSeikyuuSakiBrc, SqlDbType.VarChar, 2, seikyuuSakiBrc), _
                                            SQLHelper.MakeParam(DBparamSeikyuuSakiKbn, SqlDbType.VarChar, 1, seikyuuSakiKbn)}
        ' 請求締め日の取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       cmdTextSb.ToString(), _
                                       cmdParams)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return String.Empty
        End If

        Return data

    End Function

    ''' <summary>
    ''' 請求締め日取得(加盟店から)
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <returns>請求締め日</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSimeDateFromKameiten(ByVal strKameitenCd As String, ByVal strSyouhinCd As String) As String
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      MSS.seikyuu_sime_date")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      v_syouhin_seikyuusaki_kameiten VSK")
        cmdTextSb.Append("           LEFT OUTER JOIN m_seikyuu_saki MSS")
        cmdTextSb.Append("             ON VSK.seikyuu_saki_cd = MSS.seikyuu_saki_cd")
        cmdTextSb.Append("            AND VSK.seikyuu_saki_brc = MSS.seikyuu_saki_brc")
        cmdTextSb.Append("            AND VSK.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      VSK.kameiten_cd = " & DBparamKameitenCd)
        cmdTextSb.Append("  AND VSK.syouhin_cd = " & DBparamSyouhinCd)
        cmdTextSb.Append("  AND MSS.torikesi = 0")

        Dim cmdParams() As SqlParameter = {SQLHelper.MakeParam(DBparamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd), _
                                            SQLHelper.MakeParam(DBparamSyouhinCd, SqlDbType.VarChar, 8, strSyouhinCd)}
        ' 請求締め日の取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       cmdTextSb.ToString(), _
                                       cmdParams)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return String.Empty
        End If

        Return data
    End Function

    ''' <summary>
    ''' 請求締め日取得(調査会社から)
    ''' </summary>
    ''' <param name="strTysKaisyaCd">調査会社コード(会社コード + 事業所コード)</param>
    ''' <returns>請求締め日</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSimeDateFromTyousa(ByVal strTysKaisyaCd As String) As String
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      MSS.seikyuu_sime_date")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      m_tyousakaisya MTK")
        cmdTextSb.Append("           LEFT OUTER JOIN m_seikyuu_saki MSS")
        cmdTextSb.Append("             ON MTK.seikyuu_saki_cd=MSS.seikyuu_saki_cd")
        cmdTextSb.Append("            AND MTK.seikyuu_saki_brc = MSS.seikyuu_saki_brc")
        cmdTextSb.Append("            AND MTK.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      MSS.torikesi=0")
        cmdTextSb.Append("  AND MTK.tys_kaisya_cd + MTK.jigyousyo_cd = " & DBparamTysKaisyaCd)

        Dim cmdParams() As SqlParameter = {SQLHelper.MakeParam(DBparamTysKaisyaCd, SqlDbType.VarChar, 7, strTysKaisyaCd)}
        ' 請求締め日の取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       cmdTextSb.ToString(), _
                                       cmdParams)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return String.Empty
        End If

        Return data
    End Function

    ''' <summary>
    ''' 請求締め日取得(営業所から)
    ''' </summary>
    ''' <param name="strEigyousyoCd">営業所コード)</param>
    ''' <returns>請求締め日</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSimeDateFromEigyousyo(ByVal strEigyousyoCd As String) As String
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("      MSS.seikyuu_sime_date ")
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      m_eigyousyo MEI ")
        cmdTextSb.AppendLine("           LEFT OUTER JOIN m_seikyuu_saki MSS ")
        cmdTextSb.AppendLine("             ON MEI.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("            AND MEI.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("            AND MEI.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine(" WHERE ")
        cmdTextSb.AppendLine("      MSS.torikesi = 0 ")
        cmdTextSb.AppendLine("      AND MEI.eigyousyo_cd = " & DBparamEigyousyoCd)

        Dim cmdParams() As SqlParameter = {SQLHelper.MakeParam(DBparamEigyousyoCd, SqlDbType.VarChar, 5, strEigyousyoCd)}
        ' 請求締め日の取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       cmdTextSb.ToString(), _
                                       cmdParams)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return String.Empty
        End If

        Return data
    End Function

#End Region

#Region "加盟店請求先情報VIEW取得"
    ''' <summary>
    ''' 加盟店請求先情報VIEW取得[PK]
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="blnTorikesi">取消データ対象フラグ(true:対象にしない / false:対象にする)</param>
    ''' <returns>加盟店請求先情報データテーブル</returns>
    ''' <remarks></remarks>
    Public Function searchKameitenSeikyuuSakiInfo( _
                                          ByVal strKameitenCd As String, _
                                          ByVal strSyouhinCd As String, _
                                          Optional ByVal blnTorikesi As Boolean = False _
                                          ) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".searchKameitenSeikyuuSakiInfo", _
                                                    strKameitenCd, _
                                                    strSyouhinCd, _
                                                    blnTorikesi)
        ' SQL生成
        Dim sqlSb As New StringBuilder()
        sqlSb.Append(" SELECT ")
        sqlSb.Append("   VIW.syouhin_cd ")
        sqlSb.Append("   , VIW.souko_cd ")
        sqlSb.Append("   , VIW.syouhin_kbn3 ")
        sqlSb.Append("   , VIW.kameiten_cd ")
        sqlSb.Append("   , LTRIM(RTRIM(VIW.seikyuu_saki_cd)) seikyuu_saki_cd ")
        sqlSb.Append("   , LTRIM(RTRIM(VIW.seikyuu_saki_brc)) seikyuu_saki_brc ")
        sqlSb.Append("   , LTRIM(RTRIM(VIW.seikyuu_saki_kbn)) seikyuu_saki_kbn ")
        sqlSb.Append(" FROM ")
        sqlSb.Append("   [jhs_sys].[v_syouhin_seikyuusaki_kameiten] AS VIW ")
        sqlSb.Append(" LEFT OUTER JOIN m_seikyuu_saki AS MSK ")
        sqlSb.Append("   ON MSK.seikyuu_saki_cd = VIW.seikyuu_saki_cd ")
        sqlSb.Append("   AND MSK.seikyuu_saki_brc = VIW.seikyuu_saki_brc ")
        sqlSb.Append("   AND MSK.seikyuu_saki_kbn = VIW.seikyuu_saki_kbn ")
        sqlSb.Append(" WHERE ")
        sqlSb.Append("   0 = 0 ")
        If strKameitenCd IsNot String.Empty Then
            sqlSb.Append("   AND VIW.kameiten_cd = " & DBparamKameitenCd)
        End If
        If strSyouhinCd IsNot String.Empty Then
            sqlSb.Append("   AND VIW.syouhin_cd = " & DBparamSyouhinCd)
        End If
        If blnTorikesi Then
            sqlSb.Append("   AND ISNULL(MSK.torikesi,1) = 0 ")
        End If

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = Nothing

        ' パラメータを設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(DBparamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd), _
                                        SQLHelper.MakeParam(DBparamSyouhinCd, SqlDbType.VarChar, 8, strSyouhinCd) _
                                        }
        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sqlSb.ToString, sqlParams)
    End Function

#End Region

#Region "邸別請求データ修正画面用_売上データ発行済みチェックデータ取得"

    ''' <summary>
    ''' 邸別請求データ修正画面用_売上データ発行済みチェックデータ取得
    ''' 現在売上年月日がセットされている邸別請求データで、売上データテーブルに
    ''' プラス伝票が存在するデータを検索する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBangou">番号</param>
    ''' <returns>売上データテーブル.紐付けコード データテーブル</returns>
    ''' <remarks></remarks>
    Public Function searchTeibetuSeikyuuDenpyouHakkouZumiUriageData( _
                                                                    ByVal strKbn As String, _
                                                                    ByVal strBangou As String _
                                                                    ) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".searchTeibetuSeikyuuDenpyouHakkouZumiUriageData", _
                                                    strKbn, _
                                                    strBangou _
                                                    )
        ' SQL生成
        Dim sqlSb As New StringBuilder()
        sqlSb.AppendLine("SELECT")
        sqlSb.AppendLine("    ts.kbn")
        sqlSb.AppendLine("    , ts.hosyousyo_no")
        sqlSb.AppendLine("    , ts.bunrui_cd")
        sqlSb.AppendLine("    , ts.gamen_hyouji_no ")
        sqlSb.AppendLine("FROM")
        sqlSb.AppendLine("    jhs_sys.t_teibetu_seikyuu ts ")
        sqlSb.AppendLine("    LEFT OUTER JOIN ( ")
        sqlSb.AppendLine("        SELECT")
        sqlSb.AppendLine("            u1.* ")
        sqlSb.AppendLine("        FROM")
        sqlSb.AppendLine("            jhs_sys.t_uriage_data u1 ")
        sqlSb.AppendLine("        WHERE")
        sqlSb.AppendLine("            u1.denpyou_unique_no IN ( ")
        sqlSb.AppendLine("                SELECT")
        sqlSb.AppendLine("                    max(u2.denpyou_unique_no) ")
        sqlSb.AppendLine("                FROM")
        sqlSb.AppendLine("                    jhs_sys.t_uriage_data u2 ")
        sqlSb.AppendLine("                WHERE")
        sqlSb.AppendLine("                    u2.kbn = " & DBparamKbn)
        sqlSb.AppendLine("                    AND u2.bangou = " & DBparamBangouFrom)
        sqlSb.AppendLine("                GROUP BY")
        sqlSb.AppendLine("                    REPLACE (u2.himoduke_cd, '$$$115$$$', '$$$110$$$')")
        sqlSb.AppendLine("                    , u2.himoduke_table_type")
        sqlSb.AppendLine("            )")
        sqlSb.AppendLine("    ) tu ")
        sqlSb.AppendLine("        ON tu.himoduke_table_type = 1 ")
        sqlSb.AppendLine("        AND ts.kbn = substring(tu.himoduke_cd, 1, 1) ")
        sqlSb.AppendLine("        AND ts.hosyousyo_no = substring(tu.himoduke_cd, 5, 10) ")
        sqlSb.AppendLine("        AND ts.bunrui_cd = substring(tu.himoduke_cd, 18, 3) ")
        sqlSb.AppendLine("        AND ts.gamen_hyouji_no = substring(tu.himoduke_cd, 24, LEN(tu.himoduke_cd) - 23) ")
        sqlSb.AppendLine("WHERE")
        sqlSb.AppendLine("    ( ")
        sqlSb.AppendLine("        tu.denpyou_unique_no IS NULL ")
        sqlSb.AppendLine("        OR tu.denpyou_syubetu = 'UR'")
        sqlSb.AppendLine("    ) ")
        sqlSb.AppendLine("    AND ts.denpyou_uri_date IS NOT NULL ")
        sqlSb.AppendLine("    AND ISNULL(ts.uri_gaku, 0) > 0")

        If strKbn IsNot String.Empty Then
            sqlSb.Append("    AND ts.kbn = " & DBparamKbn)
        End If
        If strBangou IsNot String.Empty Then
            sqlSb.Append("    AND ts.hosyousyo_no = " & DBparamBangouFrom)
        End If

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = Nothing

        ' パラメータを設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(DBparamKbn, SqlDbType.VarChar, 1, strKbn), _
                                        SQLHelper.MakeParam(DBparamBangouFrom, SqlDbType.VarChar, 10, strBangou) _
                                        }
        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sqlSb.ToString, sqlParams)
    End Function

#End Region

#End Region

End Class
