Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 仕入データの取得クラスです
''' </summary>
''' <remarks></remarks>
Public Class SiireDataAccess
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim strLogic As New StringLogic
#End Region

#Region "仕入データの取得"
    ''' <summary>
    ''' 仕入データを取得します
    ''' </summary>
    ''' <param name="keyRec">区分</param>
    ''' <returns>仕入データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSiireDataInfo(ByVal keyRec As SiireDataKeyRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiireDataInfo", keyRec)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        Dim strCmnSql As String = GetSiireCmnSql(keyRec)
        Dim cmdParams() As SqlParameter = GetsirSqlCmnParams(keyRec)
        Dim cmnDtAcc As New CmnDataAccess

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      SIR.denpyou_unique_no")
        cmdTextSb.Append("    , SIR.denpyou_no")
        cmdTextSb.Append("    , SIR.denpyou_syubetu")
        cmdTextSb.Append("    , SIR.torikesi_moto_denpyou_unique_no")
        cmdTextSb.Append("    , SIR.kbn")
        cmdTextSb.Append("    , SIR.bangou")
        cmdTextSb.Append("    , SIR.himoduke_cd")
        cmdTextSb.Append("    , SIR.himoduke_table_type")
        cmdTextSb.Append("    , SIR.siire_date")
        cmdTextSb.Append("    , SIR.denpyou_siire_date")
        cmdTextSb.Append("    , SIR.siire_keijyou_flg")
        cmdTextSb.Append("    , SIR.tys_kaisya_cd")
        cmdTextSb.Append("    , SIR.tys_kaisya_jigyousyo_cd")
        cmdTextSb.Append("    , SIR.tys_kaisya_mei")
        cmdTextSb.Append("    , SIR.syouhin_cd")
        cmdTextSb.Append("    , SIR.hinmei")
        cmdTextSb.Append("    , SIR.suu")
        cmdTextSb.Append("    , SIR.tani")
        cmdTextSb.Append("    , SIR.tanka")
        cmdTextSb.Append("    , SIR.siire_gaku")
        cmdTextSb.Append("    , SIR.sotozei_gaku")
        cmdTextSb.Append("    , SIR.zei_kbn")
        cmdTextSb.Append("    , SIR.add_login_user_id")
        cmdTextSb.Append("    , SIR.add_login_user_name")
        cmdTextSb.Append("    , SIR.add_datetime")
        cmdTextSb.Append("    , SIR.upd_login_user_id")
        cmdTextSb.Append("    , SIR.upd_datetime")
        cmdTextSb.Append("    , ( ")
        cmdTextSb.Append("           CASE ")
        cmdTextSb.Append("                WHEN Nullif(SIR.kbn, '') IS NOT NULL ")
        cmdTextSb.Append("                 AND Nullif(SIR.bangou, '') IS NOT NULL ")
        cmdTextSb.Append("                THEN ")
        cmdTextSb.Append("                     CASE ")
        cmdTextSb.Append("                          WHEN Nullif(TJB.sesyu_mei, '') IS NOT NULL ")
        cmdTextSb.Append("                          THEN TJB.sesyu_mei ")
        cmdTextSb.Append("                          WHEN Nullif(THS.kbn, '') IS NOT NULL ")
        cmdTextSb.Append("                           AND Nullif(THS.bangou, '') IS NOT NULL ")
        cmdTextSb.Append("                          THEN THS.sesyu_mei ")
        cmdTextSb.Append("                          ELSE NULL ")
        cmdTextSb.Append("                     END ")
        cmdTextSb.Append("                ELSE NULL ")
        cmdTextSb.Append("           END) AS sesyu_mei ")
        cmdTextSb.Append(strCmnSql)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 仕入データテーブルをPKで取得します
    ''' </summary>
    ''' <param name="strDenUnqNo">伝票ユニークNO</param>
    ''' <returns>仕入データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSiireDataRec(ByVal strDenUnqNo As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiireDataRec", strDenUnqNo)

        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()
        Dim cmnDtAcc As New CmnDataAccess

        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("      SII.denpyou_unique_no ")
        cmdTextSb.AppendLine("    , SII.denpyou_no ")
        cmdTextSb.AppendLine("    , SII.denpyou_syubetu ")
        cmdTextSb.AppendLine("    , SII.torikesi_moto_denpyou_unique_no ")
        cmdTextSb.AppendLine("    , SII.kbn ")
        cmdTextSb.AppendLine("    , SII.bangou ")
        cmdTextSb.AppendLine("    , SII.himoduke_cd ")
        cmdTextSb.AppendLine("    , SII.himoduke_table_type ")
        cmdTextSb.AppendLine("    , SII.siire_date ")
        cmdTextSb.AppendLine("    , SII.denpyou_siire_date ")
        cmdTextSb.AppendLine("    , SII.siire_keijyou_flg ")
        cmdTextSb.AppendLine("    , SII.bukken_tys_kaisya_cd ")
        cmdTextSb.AppendLine("    , SII.bukken_tys_kaisya_jigyousyo_cd ")
        cmdTextSb.AppendLine("    , SII.tys_kaisya_cd ")
        cmdTextSb.AppendLine("    , SII.tys_kaisya_jigyousyo_cd ")
        cmdTextSb.AppendLine("    , SII.tys_kaisya_mei ")
        cmdTextSb.AppendLine("    , SII.syouhin_cd ")
        cmdTextSb.AppendLine("    , SII.hinmei ")
        cmdTextSb.AppendLine("    , SII.suu ")
        cmdTextSb.AppendLine("    , SII.tani ")
        cmdTextSb.AppendLine("    , SII.tanka ")
        cmdTextSb.AppendLine("    , SII.siire_gaku ")
        cmdTextSb.AppendLine("    , SII.sotozei_gaku ")
        cmdTextSb.AppendLine("    , SII.zei_kbn ")
        cmdTextSb.AppendLine("    , SII.add_login_user_id ")
        cmdTextSb.AppendLine("    , SII.add_login_user_name ")
        cmdTextSb.AppendLine("    , SII.add_datetime ")
        cmdTextSb.AppendLine("    , SII.upd_login_user_id ")
        cmdTextSb.AppendLine("    , SII.upd_datetime ")
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      t_siire_data SII ")
        cmdTextSb.AppendLine("      WITH (READCOMMITTED) ")
        cmdTextSb.AppendLine(" WHERE ")
        cmdTextSb.AppendLine(" SII.denpyou_unique_no = " & DBParamDenUnqNo)

        Dim cmdParams() As SqlParameter = {SQLHelper.MakeParam(DBParamDenUnqNo, SqlDbType.Int, 6, strDenUnqNo)}

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 仕入データ[CSV出力用]を取得します
    ''' </summary>
    ''' <param name="keyRec">仕入データKeyレコード</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSiireDataCsv(ByVal keyRec As SiireDataKeyRecord) As DataTable
        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        Dim strCmnSql As String = GetSiireCmnSql(keyRec)
        Dim cmdParams() As SqlParameter = GetSirSqlCmnParams(keyRec)
        Dim cmnDtAcc As New CmnDataAccess

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      SIR.denpyou_unique_no  '伝票ユニークNO' ")
        cmdTextSb.Append("    , SIR.denpyou_no  '伝票番号' ")
        cmdTextSb.Append("    , SIR.denpyou_syubetu  '伝票種別' ")
        cmdTextSb.Append("    , SIR.torikesi_moto_denpyou_unique_no  '取消元伝票ユニークNO' ")
        cmdTextSb.Append("    , SIR.kbn  '区分' ")
        cmdTextSb.Append("    , SIR.bangou  '番号' ")
        cmdTextSb.Append("    , ISNULL(TJB.sesyu_mei, THS.sesyu_mei) '施主名' ")
        cmdTextSb.Append("    , SIR.himoduke_cd  '紐付けコード' ")
        cmdTextSb.Append("    , SIR.himoduke_table_type  '紐付け元テーブル種別' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, SIR.siire_date, 111)  '仕入年月日' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, SIR.denpyou_siire_date, 111)  '伝票仕入年月日' ")
        cmdTextSb.Append("    , SIR.siire_keijyou_flg  '仕入処理FLG' ")
        cmdTextSb.Append("    , SIR.bukken_tys_kaisya_cd  '物件調査会社コード' ")
        cmdTextSb.Append("    , SIR.bukken_tys_kaisya_jigyousyo_cd  '物件調査会社事業所コード' ")
        cmdTextSb.Append("    , SIR.tys_kaisya_cd  '調査会社コード' ")
        cmdTextSb.Append("    , SIR.tys_kaisya_jigyousyo_cd  '調査会社事業所コード' ")
        cmdTextSb.Append("    , SIR.tys_kaisya_mei  '調査会社名' ")
        cmdTextSb.Append("    , SIR.syouhin_cd  '商品コード' ")
        cmdTextSb.Append("    , SIR.hinmei  '品名' ")
        cmdTextSb.Append("    , SIR.suu  '数量' ")
        cmdTextSb.Append("    , SIR.tani  '単位' ")
        cmdTextSb.Append("    , SIR.tanka  '単価' ")
        cmdTextSb.Append("    , SIR.siire_gaku  '仕入金額' ")
        cmdTextSb.Append("    , SIR.sotozei_gaku  '外税額' ")
        cmdTextSb.Append("    , SIR.zei_kbn  '税区分' ")
        cmdTextSb.Append("    , SIR.add_login_user_id  '登録ログインユーザーID' ")
        cmdTextSb.Append("    , SIR.add_login_user_name  '登録ログインユーザー名' ")
        cmdTextSb.Append("    , SIR.add_datetime  '登録日時' ")
        cmdTextSb.Append(strCmnSql)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 仕入データ取得用の共通SQLクエリを取得
    ''' </summary>
    ''' <param name="keyRec">仕入データKeyレコード</param>
    ''' <returns>仕入データ取得用の共通SQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetSiireCmnSql(ByVal keyRec As SiireDataKeyRecord) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiireCmnSql", keyRec)
        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("   t_siire_data SIR ")
        cmdTextSb.Append("   LEFT OUTER JOIN m_tyousakaisya TYS ")
        cmdTextSb.Append("      ON SIR.tys_kaisya_cd            = TYS.tys_kaisya_cd ")
        cmdTextSb.Append("     AND SIR.tys_kaisya_jigyousyo_cd  = TYS.jigyousyo_cd ")
        cmdTextSb.Append("   LEFT OUTER JOIN t_jiban TJB ")
        cmdTextSb.Append("      ON SIR.kbn                      = TJB.kbn ")
        cmdTextSb.Append("     AND SIR.bangou                   = TJB.hosyousyo_no ")
        cmdTextSb.Append("   LEFT OUTER JOIN t_hannyou_siire THS ")
        cmdTextSb.Append("      ON SIR.himoduke_cd = CAST(THS.han_siire_unique_no AS varchar) ")
        cmdTextSb.Append(" WHERE 1 = 1 ")

        '***********************************************************************
        '伝票ユニーク番号
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.DenUnqNo) Then
            cmdTextSb.Append(" AND ")
            cmdTextSb.Append(" SIR.denpyou_unique_no = " & DBParamDenUnqNo)
        End If

        '***********************************************************************
        '区分
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.Kbn) Then
            cmdTextSb.Append(" AND SIR.kbn = " & DBparamKbn)
        End If

        '***********************************************************************
        '番号
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.BangouFrom) Or Not String.IsNullOrEmpty(keyRec.BangouTo) Then

            cmdTextSb.Append(" AND ")

            If Not (keyRec.BangouFrom Is String.Empty) And Not (keyRec.BangouTo Is String.Empty) Then
                ' 両方指定有りはBETWEEN
                cmdTextSb.Append(" SIR.bangou BETWEEN " & DBparamBangouFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBparamBangouTo)
            Else
                If Not keyRec.BangouFrom Is String.Empty Then
                    ' 番号Fromのみ
                    cmdTextSb.Append(" SIR.bangou >= " & DBparamBangouFrom)
                Else
                    ' 番号Toのみ
                    cmdTextSb.Append(" SIR.bangou <= " & DBparamBangouTo)
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
                cmdTextSb.Append(" SIR.denpyou_no BETWEEN " & DBparamDenNoFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBparamDenNoTo)
            Else
                If Not keyRec.DenNoFrom Is String.Empty Then
                    ' 伝票番号Fromのみ
                    cmdTextSb.Append(" SIR.denpyou_no >= " & DBparamDenNoFrom)
                Else
                    ' 伝票番号Toのみ
                    cmdTextSb.Append(" SIR.denpyou_no <= " & DBparamDenNoTo)
                End If
            End If
        End If

        '***********************************************************************
        '登録年月日
        '***********************************************************************
        '日付が一つでも設定されている場合、条件を作成
        If keyRec.AddDatetimeFrom <> DateTime.MinValue Or _
            keyRec.AddDatetimeTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.AddDatetimeFrom <> DateTime.MinValue And _
                keyRec.AddDatetimeTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, SIR.add_datetime ,111) BETWEEN " & DBparamAddDatetimeFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBparamAddDatetimeTo)
            Else
                If keyRec.AddDatetimeFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, SIR.add_datetime ,111) >= " & DBparamAddDatetimeFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, SIR.add_datetime ,111) <= " & DBparamAddDatetimeTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 商品コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SyouhinCd) Then
            cmdTextSb.Append(" AND SIR.syouhin_cd = " & DBparamSyouhinCd)
        End If

        '***********************************************************************
        ' 仕入先コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SiireSakiCd) Then
            cmdTextSb.Append(" AND SIR.tys_kaisya_cd = " & DBparamSiireSakiCd)
        End If

        '***********************************************************************
        ' 仕入先枝番
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SiireSakiBrc) Then
            cmdTextSb.Append(" AND SIR.tys_kaisya_jigyousyo_cd = " & DBparamSiireSakiBrc)
        End If

        '***********************************************************************
        ' 仕入先名カナ
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SiireSakiMeiKana) Then
            cmdTextSb.Append(" AND TYS.tys_kaisya_mei_kana like " & DBparamSiireSakiMeiKana)
        End If

        '***********************************************************************
        ' 仕入年月日
        '***********************************************************************
        '日付が一つでも設定されている場合、条件を作成
        If keyRec.SiireDateFrom <> DateTime.MinValue Or _
            keyRec.SiireDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.SiireDateFrom <> DateTime.MinValue And _
                keyRec.SiireDateTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, SIR.siire_date ,111) BETWEEN " & DBparamSiireDateFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBparamSiireDateTo)
            Else
                If keyRec.SiireDateFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, SIR.siire_date ,111) >= " & DBparamSiireDateFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, SIR.siire_date ,111) <= " & DBparamSiireDateTo)
                End If
            End If
        End If

        '***********************************************************************
        '最新伝票のみ表示
        '***********************************************************************
        If keyRec.NewDenpyouDisp = 0 Then

            cmdTextSb.Append(" AND ")
            cmdTextSb.Append("  SIR.denpyou_unique_no IN ( ")
            cmdTextSb.Append(" SELECT ")
            cmdTextSb.Append("  MAX(SIR2.denpyou_unique_no) ")
            cmdTextSb.Append(" FROM ")
            cmdTextSb.Append("  t_siire_data SIR2 ")
            cmdTextSb.Append(" GROUP BY ")
            cmdTextSb.Append("  REPLACE (SIR2.himoduke_cd, '$$$115$$$', '$$$110$$$') ")
            cmdTextSb.Append("  , SIR2.himoduke_table_type ")
            cmdTextSb.Append(" ) ")
        End If

        '***********************************************************************
        'マイナス伝票のみ表示
        '***********************************************************************
        If keyRec.MinusDenpyouDisp = 0 Then
            cmdTextSb.Append(" AND ")
            cmdTextSb.Append("  SIR.denpyou_syubetu = '" & EarthConst.SR & "'")
        End If

        '***********************************************************************
        '計上済み伝票のみ表示
        '***********************************************************************
        If keyRec.KeijyouZumiDisp = 0 Then
            cmdTextSb.Append(" AND ")
            cmdTextSb.Append("  SIR.siire_keijyou_flg = '1'")
        End If

        '***********************************************************************
        '表示順序の付与（伝票ユニークNo→売上年月日→伝票番号）
        '***********************************************************************
        cmdTextSb.Append(" ORDER BY ")
        cmdTextSb.Append("     SIR.denpyou_unique_no ")
        cmdTextSb.Append("   , SIR.siire_date ")
        cmdTextSb.Append("   , SIR.denpyou_no ")

        Return cmdTextSb.ToString()

    End Function

    ''' <summary>
    ''' 仕入データ取得用の共通SQLパラメータを取得
    ''' </summary>
    ''' <param name="keyRec">仕入データKeyレコード</param>
    ''' <returns>仕入データ取得用の共通SQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetSirSqlCmnParams(ByVal keyRec As SiireDataKeyRecord) As SqlParameter()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSirSqlParams", keyRec)

        Dim dtAddDateFrom As Object = IIf(keyRec.AddDatetimeFrom = DateTime.MinValue, DBNull.Value, keyRec.AddDatetimeFrom)
        Dim dtAddDateTo As Object = IIf(keyRec.AddDatetimeTo = DateTime.MinValue, DBNull.Value, keyRec.AddDatetimeTo)
        Dim dtSiireFrom As Object = IIf(keyRec.SiireDateFrom = DateTime.MinValue, DBNull.Value, keyRec.SiireDateFrom)
        Dim dtSiireTo As Object = IIf(keyRec.SiireDateTo = DateTime.MinValue, DBNull.Value, keyRec.SiireDateTo)
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
                 SQLHelper.MakeParam(DBparamSiireSakiCd, SqlDbType.VarChar, 5, keyRec.SiireSakiCd), _
                 SQLHelper.MakeParam(DBparamSiireSakiBrc, SqlDbType.VarChar, 2, keyRec.SiireSakiBrc), _
                 SQLHelper.MakeParam(DBparamSiireSakiMeiKana, SqlDbType.VarChar, 100, keyRec.SiireSakiMeiKana & Chr(37)), _
                 SQLHelper.MakeParam(DBparamSiireDateFrom, SqlDbType.DateTime, 16, dtSiireFrom), _
                 SQLHelper.MakeParam(DBparamSiireDateTo, SqlDbType.DateTime, 16, dtSiireTo)}

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
    Private Const DBparamSiireSakiCd As String = "@SIIRESAKICD"
    Private Const DBparamSiireSakiBrc As String = "@SIIRESAKIBRC"
    Private Const DBparamSiireSakiMeiKana As String = "@SIIRESAKIMEIKANA"
    Private Const DBparamSiireDateFrom As String = "@SIIREDATEFROM"
    Private Const DBparamSiireDateTo As String = "@SIIREDATETO"
#End Region

#End Region

#Region "仕入先情報取得"
    ''' <summary>
    ''' 仕入先情報取得
    ''' </summary>
    ''' <param name="siireSakiCd">仕入先コード</param>
    ''' <param name="siireSakiBrc">仕入先枝番</param>
    ''' <param name="siireSakiMei">仕入先名</param>
    ''' <param name="siireSakiKana">仕入先カナ</param>
    ''' <param name="torikesiFlg">取消データ対象フラグ(true:対象にしない / false:対象にする)</param>
    ''' <returns>仕入先情報データテーブル</returns>
    ''' <remarks></remarks>
    Public Function searchSiireSakiInfo(ByVal siireSakiCd As String, _
                                        ByVal siireSakiBrc As String, _
                                        ByVal siireSakiMei As String, _
                                        ByVal siireSakiKana As String, _
                                        Optional ByVal torikesiFlg As Boolean = False _
                                        ) As SiireDataSet.SiireSakiInfoDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".searchSiireSakiInfo", _
                                                    siireSakiCd, _
                                                    siireSakiBrc, _
                                                    siireSakiMei, _
                                                    siireSakiKana, _
                                                    torikesiFlg)

        ' パラメータ
        Const prmSiireSakiCd As String = "@SIIRESAKICD"
        Const prmSiireSakiBrc As String = "@SIIRESAKIBRD"
        Const prmSiireSakiMei As String = "@SIIRESAKIMEI"
        Const prmSiireSakiKana As String = "@SIIRESAKIKANA"

        ' SQL生成
        Dim sqlSb As New StringBuilder()
        sqlSb.Append(" SELECT ")
        sqlSb.Append("   MS.siire_saki_cd ")
        sqlSb.Append("   , MS.siire_saki_brc ")
        sqlSb.Append("   , MT.tys_kaisya_mei2 AS siire_saki_mei ")
        sqlSb.Append("   , MT.tys_kaisya_mei2_kana AS siire_saki_kana  ")
        sqlSb.Append(" FROM ")
        sqlSb.Append("   [jhs_sys].[m_siire_saki] MS  ")
        sqlSb.Append("   LEFT OUTER JOIN [jhs_sys].[m_tyousakaisya] MT  ")
        sqlSb.Append("     ON MS.siire_saki_cd = MT.tys_kaisya_cd  ")
        sqlSb.Append("     AND MS.siire_saki_brc = MT.jigyousyo_cd ")
        sqlSb.Append(" WHERE ")
        sqlSb.Append("   0 = 0 ")
        If siireSakiCd IsNot String.Empty Then
            sqlSb.Append("   AND MS.siire_saki_cd LIKE " & prmSiireSakiCd)
        End If
        If siireSakiBrc IsNot String.Empty Then
            sqlSb.Append("   AND MS.siire_saki_brc = " & prmSiireSakiBrc)
        End If
        If siireSakiMei IsNot String.Empty Then
            sqlSb.Append("   AND MT.tys_kaisya_mei2 LIKE " & prmSiireSakiMei)
        End If
        If siireSakiKana IsNot String.Empty Then
            sqlSb.Append("   AND MT.tys_kaisya_mei2_kana LIKE " & prmSiireSakiKana)
        End If
        If torikesiFlg Then
            sqlSb.Append("   AND ISNULL(MS.torikesi,1) = 0 ")
        End If

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = Nothing

        ' パラメータを設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(prmSiireSakiCd, SqlDbType.VarChar, 7, siireSakiCd & Chr(37)), _
                                        SQLHelper.MakeParam(prmSiireSakiBrc, SqlDbType.VarChar, 2, siireSakiBrc), _
                                        SQLHelper.MakeParam(prmSiireSakiMei, SqlDbType.VarChar, 100, siireSakiMei & Chr(37)), _
                                        SQLHelper.MakeParam(prmSiireSakiKana, SqlDbType.VarChar, 100, siireSakiKana & Chr(37)) _
                                        }

        ' データの取得
        Dim siireDataSet As New SiireDataSet()

        ' 検索実行(パラメータ有り)
        SQLHelper.FillDataset(connStr, _
                              CommandType.Text, _
                              sqlSb.ToString(), _
                              siireDataSet, _
                              siireDataSet.SiireSakiInfo.TableName, _
                              sqlParams)

        Dim siireSakiInfoDataTable As SiireDataSet.SiireSakiInfoDataTable = siireDataSet.SiireSakiInfo

        Return siireSakiInfoDataTable
    End Function
#End Region

End Class
