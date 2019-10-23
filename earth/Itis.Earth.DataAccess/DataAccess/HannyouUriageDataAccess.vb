Imports System.text
Imports System.Data.SqlClient

Public Class HannyouUriageDataAccess
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
#End Region

    ''' <summary>
    ''' 汎用売上テーブルの情報を取得します
    ''' </summary>
    ''' <param name="keyRec">検索キーレコードクラス</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks>検索キーレコードクラスをKEYにして取得</remarks>
    Public Function getSearchTable(ByVal keyRec As HannyouUriageDataKeyRecord) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSearchTable" _
                                                    , keyRec _
                                                    )
        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter

        Const strPrmTourokuNengappiFrom As String = "@TOUROKU_NENGAPPI_FROM" '登録年月日_FROM
        Const strPrmTourokuNengappiTo As String = "@TOUROKU_NENGAPPI_TO" '登録年月日_TO
        Const strPrmSyouhinCd As String = "@SYOUHIN_CD" '商品コード
        Const strPrmSeikyuuSakiKbn As String = "@SEIKYUU_SAKI_KBN" '請求先区分
        Const strPrmSeikyuuSakiCd As String = "@SEIKYUU_SAKI_CD" '請求先コード
        Const strPrmSeikyuuSakiBrc As String = "@SEIKYUU_SAKI_BRC" '請求先枝番
        Const strPrmSeikyuuSakiMeiKana As String = "@SEIKYUUSAKIMEI_KANA" '請求先名カナ
        Const strPrmSeikyuuDateFrom As String = "@SEIKYUU_DATE_FROM" '請求年月日_FROM
        Const strPrmSeikyuuDateTo As String = "@SEIKYUU_DATE_TO" '請求年月日_TO
        Const strPrmUriDateFrom As String = "@URIDATE_FROM" '売上年月日_FROM
        Const strPrmUriDateTo As String = "@URIDATE_TO" '売上年月日_TO
        Const strPrmDenpyouUriDateFrom As String = "@DENPYOU_URIDATE_FROM" '伝票売上年月日_FROM'
        Const strPrmDenpyouUriDateTo As String = "@DENPYOU_URIDATE_T0" '伝票売上年月日_TO'
        Const strPrmKbn As String = "@KBN" '区分
        Const strPrmBangou As String = "@BANGOU" '番号
        Const strPrmSesyumei As String = "@SESYU_MEI" '施主名

        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      THU.han_uri_unique_no ")
        cmdTextSb.Append("      , THU.seikyuu_saki_cd ")
        cmdTextSb.Append("      , THU.seikyuu_saki_brc ")
        cmdTextSb.Append("      , THU.seikyuu_saki_kbn ")
        cmdTextSb.Append("      , VIW.seikyuu_saki_mei ")
        cmdTextSb.Append("      , THU.syouhin_cd ")
        cmdTextSb.Append("      , THU.hin_mei ")
        cmdTextSb.Append("      , THU.tekiyou ")
        cmdTextSb.Append("      , THU.suu ")
        cmdTextSb.Append("      , THU.tanka ")
        cmdTextSb.Append("      , THU.zeiritu ")
        cmdTextSb.Append("      , THU.uri_date ")
        cmdTextSb.Append("      , THU.denpyou_uri_date ")
        cmdTextSb.Append("      , THU.seikyuu_date")
        cmdTextSb.Append("      , THU.syouhizei_gaku")
        cmdTextSb.Append("      , THU.kbn")
        cmdTextSb.Append("      , THU.bangou")
        cmdTextSb.Append("      , THU.sesyu_mei")
        cmdTextSb.Append("   FROM ")
        cmdTextSb.Append("       ( SELECT ")
        cmdTextSb.Append("                  SUB.han_uri_unique_no ")
        cmdTextSb.Append("                  , SUB.seikyuu_saki_cd ")
        cmdTextSb.Append("                  , SUB.seikyuu_saki_brc ")
        cmdTextSb.Append("                  , SUB.seikyuu_saki_kbn ")
        cmdTextSb.Append("                  , SUB.syouhin_cd ")
        cmdTextSb.Append("                  , SUB.hin_mei ")
        cmdTextSb.Append("                  , SUB.tekiyou ")
        cmdTextSb.Append("                  , SUB.suu ")
        cmdTextSb.Append("                  , SUB.tanka ")
        cmdTextSb.Append("                  , MSZ.zeiritu ")
        cmdTextSb.Append("                  , SUB.uri_date ")
        cmdTextSb.Append("                  , SUB.denpyou_uri_date ")
        cmdTextSb.Append("                  , SUB.seikyuu_date")
        cmdTextSb.Append("                  , SUB.syouhizei_gaku")
        cmdTextSb.Append("                  , SUB.kbn")
        cmdTextSb.Append("                  , SUB.bangou")
        cmdTextSb.Append("                  , SUB.sesyu_mei")
        cmdTextSb.Append("            FROM t_hannyou_uriage SUB ")
        cmdTextSb.Append("            LEFT OUTER JOIN m_syouhizei MSZ ")
        cmdTextSb.Append("              ON SUB.zei_kbn = MSZ.zei_kbn ")
        cmdTextSb.Append("            WHERE 1 = 1 ")

        '***********************************************************************
        '登録年月日(From,To)
        '***********************************************************************
        '日付が一つでも設定されている場合、条件を作成
        If keyRec.AddDatetimeFrom <> DateTime.MinValue Or _
            keyRec.AddDatetimeTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.AddDatetimeFrom <> DateTime.MinValue And _
                keyRec.AddDatetimeTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, SUB.add_datetime ,111) BETWEEN " & strPrmTourokuNengappiFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(strPrmTourokuNengappiTo)
            Else
                If keyRec.AddDatetimeFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, SUB.add_datetime ,111) >= " & strPrmTourokuNengappiFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, SUB.add_datetime ,111) <= " & strPrmTourokuNengappiTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 商品コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SyouhinCd) Then
            cmdTextSb.Append(" AND SUB.syouhin_cd = " & strPrmSyouhinCd)
        End If

        '***********************************************************************
        ' 請求先区分
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiKbn) Then
            cmdTextSb.Append(" AND SUB.seikyuu_saki_kbn = " & strPrmSeikyuuSakiKbn)
        End If

        '***********************************************************************
        ' 請求先コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiCd) Then
            cmdTextSb.Append(" AND SUB.seikyuu_saki_cd = " & strPrmSeikyuuSakiCd)
        End If

        '***********************************************************************
        ' 請求先枝番
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiBrc) Then
            cmdTextSb.Append(" AND SUB.seikyuu_saki_brc = " & strPrmSeikyuuSakiBrc)
        End If

        '***********************************************************************
        ' 請求年月日(From,To)
        '***********************************************************************
        '日付が一つでも設定されている場合、条件を作成
        If keyRec.SeikyuuDateFrom <> DateTime.MinValue Or _
            keyRec.SeikyuuDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.SeikyuuDateFrom <> DateTime.MinValue And _
                keyRec.SeikyuuDateTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, SUB.seikyuu_date ,111) BETWEEN " & strPrmSeikyuuDateFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(strPrmSeikyuuDateTo)
            Else
                If keyRec.SeikyuuDateFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, SUB.seikyuu_date ,111) >= " & strPrmSeikyuuDateFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, SUB.seikyuu_date ,111) <= " & strPrmSeikyuuDateTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 売上年月日(From,To)
        '***********************************************************************
        '日付が一つでも設定されている場合、条件を作成
        If keyRec.UriDateFrom <> DateTime.MinValue Or _
            keyRec.UriDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.UriDateFrom <> DateTime.MinValue And _
                keyRec.UriDateTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, SUB.uri_date ,111) BETWEEN " & strPrmUriDateFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(strPrmUriDateTo)
            Else
                If keyRec.UriDateFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, SUB.uri_date ,111) >= " & strPrmUriDateFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, SUB.uri_date ,111) <= " & strPrmUriDateTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 伝票売上年月日(From,To)
        '***********************************************************************
        '日付が一つでも設定されている場合、条件を作成
        If keyRec.DenpyouUriDateFrom <> DateTime.MinValue Or _
            keyRec.DenpyouUriDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.DenpyouUriDateFrom <> DateTime.MinValue And _
                keyRec.DenpyouUriDateTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, SUB.denpyou_uri_date ,111) BETWEEN " & strPrmDenpyouUriDateFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(strPrmDenpyouUriDateTo)
            Else
                If keyRec.DenpyouUriDateFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, SUB.denpyou_uri_date ,111) >= " & strPrmDenpyouUriDateFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, SUB.denpyou_uri_date ,111) <= " & strPrmDenpyouUriDateTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 区分
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.Kbn) Then
            cmdTextSb.Append(" AND SUB.kbn = " & strPrmKbn)
        End If

        '***********************************************************************
        ' 番号
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.Bangou) Then
            cmdTextSb.Append(" AND SUB.bangou = " & strPrmBangou)
        End If

        '***********************************************************************
        ' 施主名
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SesyuMei) Then
            cmdTextSb.Append(" AND SUB.sesyu_mei = " & strPrmSesyumei)
        End If

        '***********************************************************************
        ' 取消
        '***********************************************************************
        If keyRec.Torikesi = 0 Then
            cmdTextSb.Append("  AND SUB.torikesi = 0 ")
        End If

        '***********************************************************************
        'VIEW結合(サブクエリ)
        '***********************************************************************
        cmdTextSb.Append("    ) THU  ")
        cmdTextSb.Append("    LEFT OUTER JOIN v_seikyuu_saki_info VIW ")
        cmdTextSb.Append("    ON THU.seikyuu_saki_cd = VIW.seikyuu_saki_cd ")
        cmdTextSb.Append("    AND THU.seikyuu_saki_brc = VIW.seikyuu_saki_brc ")
        cmdTextSb.Append("    AND THU.seikyuu_saki_kbn = VIW.seikyuu_saki_kbn ")

        '***********************************************************************
        ' 請求先名カナ
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiMeiKana) Then
            cmdTextSb.Append(" WHERE VIW.seikyuu_saki_kana LIKE " & strPrmSeikyuuSakiMeiKana)
        End If

        '***********************************************************************
        '表示順序の付与（売上年月日,汎用売上ユニークNO）
        '***********************************************************************
        cmdTextSb.Append(" ORDER BY ")
        cmdTextSb.Append("   THU.uri_date ")
        cmdTextSb.Append("   , THU.han_uri_unique_no ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(strPrmTourokuNengappiFrom, SqlDbType.DateTime, 16, IIf(keyRec.AddDatetimeFrom = DateTime.MinValue, DBNull.Value, keyRec.AddDatetimeFrom)), _
            SQLHelper.MakeParam(strPrmTourokuNengappiTo, SqlDbType.DateTime, 16, IIf(keyRec.AddDatetimeTo = DateTime.MinValue, DBNull.Value, keyRec.AddDatetimeTo)), _
            SQLHelper.MakeParam(strPrmSyouhinCd, SqlDbType.VarChar, 8, keyRec.SyouhinCd), _
            SQLHelper.MakeParam(strPrmSeikyuuSakiKbn, SqlDbType.Char, 1, keyRec.SeikyuuSakiKbn), _
            SQLHelper.MakeParam(strPrmSeikyuuSakiCd, SqlDbType.VarChar, 5, keyRec.SeikyuuSakiCd), _
            SQLHelper.MakeParam(strPrmSeikyuuSakiBrc, SqlDbType.VarChar, 2, keyRec.SeikyuuSakiBrc), _
            SQLHelper.MakeParam(strPrmSeikyuuSakiMeiKana, SqlDbType.VarChar, 40, keyRec.SeikyuuSakiMeiKana & Chr(37)), _
            SQLHelper.MakeParam(strPrmSeikyuuDateFrom, SqlDbType.DateTime, 16, IIf(keyRec.SeikyuuDateFrom = DateTime.MinValue, DBNull.Value, keyRec.SeikyuuDateFrom)), _
            SQLHelper.MakeParam(strPrmSeikyuuDateTo, SqlDbType.DateTime, 16, IIf(keyRec.SeikyuuDateTo = DateTime.MinValue, DBNull.Value, keyRec.SeikyuuDateTo)), _
            SQLHelper.MakeParam(strPrmUriDateFrom, SqlDbType.DateTime, 16, IIf(keyRec.UriDateFrom = DateTime.MinValue, DBNull.Value, keyRec.UriDateFrom)), _
            SQLHelper.MakeParam(strPrmUriDateTo, SqlDbType.DateTime, 16, IIf(keyRec.UriDateTo = DateTime.MinValue, DBNull.Value, keyRec.UriDateTo)), _
            SQLHelper.MakeParam(strPrmDenpyouUriDateFrom, SqlDbType.DateTime, 16, IIf(keyRec.DenpyouUriDateFrom = DateTime.MinValue, DBNull.Value, keyRec.DenpyouUriDateFrom)), _
            SQLHelper.MakeParam(strPrmDenpyouUriDateTo, SqlDbType.DateTime, 16, IIf(keyRec.DenpyouUriDateTo = DateTime.MinValue, DBNull.Value, keyRec.DenpyouUriDateTo)), _
            SQLHelper.MakeParam(strPrmKbn, SqlDbType.Char, 1, keyRec.Kbn), _
            SQLHelper.MakeParam(strPrmBangou, SqlDbType.VarChar, 10, keyRec.Bangou), _
            SQLHelper.MakeParam(strPrmSesyumei, SqlDbType.VarChar, 50, keyRec.SesyuMei)}

        ' データ取得＆返却
        Dim cmnDtAcc As New CmnDataAccess
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 汎用売上テーブルの情報をPKで取得します
    ''' </summary>
    ''' <param name="intHanUriNo">主キー項目値</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks>検索キーレコードクラスをKEYにして取得</remarks>
    Public Function getSearchTable(ByVal intHanUriNo As Integer) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSearchTable" _
                                                    , intHanUriNo _
                                                    )
        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter

        Const strPrmHanUriNo As String = "@HANNYOU_URIAGE_UNIQUE_NO" '汎用売上ユニークNO

        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append("SELECT ")
        cmdTextSb.Append("       THU.han_uri_unique_no ")
        cmdTextSb.Append("       , THU.torikesi ")
        cmdTextSb.Append("       , THU.tekiyou ")
        cmdTextSb.Append("       , THU.uri_date ")
        cmdTextSb.Append("       , THU.denpyou_uri_date ")
        cmdTextSb.Append("       , THU.seikyuu_date ")
        cmdTextSb.Append("       , THU.seikyuu_saki_cd ")
        cmdTextSb.Append("       , THU.seikyuu_saki_brc ")
        cmdTextSb.Append("       , THU.seikyuu_saki_kbn ")
        cmdTextSb.Append("       , THU.syouhin_cd ")
        cmdTextSb.Append("       , THU.hin_mei ")
        cmdTextSb.Append("       , THU.suu ")
        cmdTextSb.Append("       , THU.tanka ")
        cmdTextSb.Append("       , THU.syanai_genka ")
        cmdTextSb.Append("       , THU.zei_kbn ")
        cmdTextSb.Append("       , THU.syouhizei_gaku ")
        cmdTextSb.Append("       , THU.uri_keijyou_flg ")
        cmdTextSb.Append("       , THU.uri_keijyou_date ")
        cmdTextSb.Append("       , THU.kbn")
        cmdTextSb.Append("       , THU.bangou")
        cmdTextSb.Append("       , THU.sesyu_mei")
        cmdTextSb.Append("       , THU.add_login_user_id ")
        cmdTextSb.Append("       , THU.add_login_user_name ")
        cmdTextSb.Append("       , THU.add_datetime ")
        cmdTextSb.Append("       , ISNULL(THU.upd_login_user_id,THU.add_login_user_id) AS upd_login_user_id ")
        cmdTextSb.Append("       , THU.upd_login_user_name ")
        cmdTextSb.Append("       , ISNULL(THU.upd_datetime,THU.add_datetime) AS upd_datetime")
        cmdTextSb.Append("       , VIW.seikyuu_saki_mei ")
        cmdTextSb.Append("       , THU.zeiritu   ")
        cmdTextSb.Append("       , THU.uriage_ten_kbn ")
        cmdTextSb.Append("       , THU.uriage_ten_cd ")
        cmdTextSb.Append("  FROM v_seikyuu_saki_info VIW ")
        cmdTextSb.Append("    ,( SELECT ")
        cmdTextSb.Append("         SUB.han_uri_unique_no ")
        cmdTextSb.Append("         , SUB.torikesi ")
        cmdTextSb.Append("         , SUB.tekiyou ")
        cmdTextSb.Append("         , SUB.uri_date ")
        cmdTextSb.Append("         , SUB.denpyou_uri_date ")
        cmdTextSb.Append("         , SUB.seikyuu_date ")
        cmdTextSb.Append("         , SUB.seikyuu_saki_cd ")
        cmdTextSb.Append("         , SUB.seikyuu_saki_brc ")
        cmdTextSb.Append("         , SUB.seikyuu_saki_kbn ")
        cmdTextSb.Append("         , SUB.syouhin_cd ")
        cmdTextSb.Append("         , SUB.hin_mei ")
        cmdTextSb.Append("         , SUB.suu ")
        cmdTextSb.Append("         , SUB.tanka ")
        cmdTextSb.Append("         , SUB.syanai_genka ")
        cmdTextSb.Append("         , SUB.zei_kbn ")
        cmdTextSb.Append("         , SUB.syouhizei_gaku ")
        cmdTextSb.Append("         , SUB.uri_keijyou_flg ")
        cmdTextSb.Append("         , SUB.uri_keijyou_date ")
        cmdTextSb.Append("         , SUB.kbn")
        cmdTextSb.Append("         , SUB.bangou")
        cmdTextSb.Append("         , SUB.sesyu_mei")
        cmdTextSb.Append("         , SUB.add_login_user_id ")
        cmdTextSb.Append("         , SUB.add_login_user_name ")
        cmdTextSb.Append("         , SUB.add_datetime ")
        cmdTextSb.Append("         , ISNULL(SUB.upd_login_user_id,SUB.add_login_user_id) AS upd_login_user_id ")
        cmdTextSb.Append("         , SUB.upd_login_user_name ")
        cmdTextSb.Append("         , ISNULL(SUB.upd_datetime,SUB.add_datetime) AS upd_datetime ")
        cmdTextSb.Append("         , MSZ.zeiritu  ")
        cmdTextSb.Append("         , SUB.uriage_ten_kbn ")
        cmdTextSb.Append("         , SUB.uriage_ten_cd ")
        cmdTextSb.Append("       FROM t_hannyou_uriage SUB ")
        cmdTextSb.Append("       LEFT OUTER JOIN m_syouhizei MSZ ")
        cmdTextSb.Append("        ON SUB.zei_kbn = MSZ.zei_kbn ")
        cmdTextSb.Append("       WHERE 1 = 1 ")

        '***********************************************************************
        ' 汎用売上ユニークNO
        '***********************************************************************
        cmdTextSb.Append("  AND SUB.han_uri_unique_no = " & strPrmHanUriNo)

        '***********************************************************************
        'VIEW結合(サブクエリ)
        '***********************************************************************
        cmdTextSb.Append("      ) AS THU ")
        cmdTextSb.Append("  WHERE THU.seikyuu_saki_cd = VIW.seikyuu_saki_cd ")
        cmdTextSb.Append("  AND THU.seikyuu_saki_brc = VIW.seikyuu_saki_brc ")
        cmdTextSb.Append("  AND THU.seikyuu_saki_kbn = VIW.seikyuu_saki_kbn ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(strPrmHanUriNo, SqlDbType.Int, 4, intHanUriNo) _
        }

        ' データ取得＆返却
        Dim cmnDtAcc As New CmnDataAccess
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

End Class
