Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 入金データの取得クラスです
''' </summary>
''' <remarks></remarks>
Public Class NyuukinDataAccess
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    Private cmnDtAcc As New CmnDataAccess

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim strLogic As New StringLogic
#End Region

    ''' <summary>
    ''' 入金データを取得します
    ''' </summary>
    ''' <param name="keyRec"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNyuukinDataInfo(ByVal keyRec As NyuukinDataKeyRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetNyuukinDataInfo", keyRec)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        Dim strCmnSql As String = GetNyuukinCmnSql(keyRec)
        Dim cmdParams() As SqlParameter = GetNyuSqlCmnParams(keyRec)

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("        NYU.denpyou_unique_no")
        cmdTextSb.Append("      , NYU.denpyou_no")
        cmdTextSb.Append("      , NYU.denpyou_syubetu")
        cmdTextSb.Append("      , NYU.torikesi_moto_denpyou_unique_no")
        cmdTextSb.Append("      , NYU.nyuukin_torikomi_unique_no")
        cmdTextSb.Append("      , NYU.seikyuu_saki_cd")
        cmdTextSb.Append("      , NYU.seikyuu_saki_brc")
        cmdTextSb.Append("      , NYU.seikyuu_saki_kbn")
        cmdTextSb.Append("      , NYU.seikyuu_saki_mei")
        cmdTextSb.Append("      , NYU.syougou_kouza_no")
        cmdTextSb.Append("      , NYU.nyuukin_date")
        cmdTextSb.Append("      , NYU.genkin")
        cmdTextSb.Append("      , NYU.kogitte")
        cmdTextSb.Append("      , NYU.furikomi")
        cmdTextSb.Append("      , NYU.tegata")
        cmdTextSb.Append("      , NYU.sousai")
        cmdTextSb.Append("      , NYU.nebiki")
        cmdTextSb.Append("      , NYU.sonota")
        cmdTextSb.Append("      , NYU.kyouryoku_kaihi")
        cmdTextSb.Append("      , NYU.kouza_furikae")
        cmdTextSb.Append("      , NYU.furikomi_tesuuryou")
        cmdTextSb.Append("      , NYU.tegata_kijitu")
        cmdTextSb.Append("      , NYU.tegata_no")
        cmdTextSb.Append("      , NYU.tekiyou_mei")
        cmdTextSb.Append("      , NYU.add_login_user_id")
        cmdTextSb.Append("      , NYU.add_login_user_name")
        cmdTextSb.Append("      , NYU.add_datetime")
        cmdTextSb.Append("      , NYU.upd_login_user_id")
        cmdTextSb.Append("      , NYU.upd_datetime")
        cmdTextSb.Append(strCmnSql)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 入金データ[CSV出力用]を取得します
    ''' </summary>
    ''' <param name="keyRec"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNyuukinDataCsv(ByVal keyRec As NyuukinDataKeyRecord) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetNyuukinDataCsv", keyRec)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        Dim strCmnSql As String = GetNyuukinCmnSql(keyRec)
        Dim cmdParams() As SqlParameter = GetNyuSqlCmnParams(keyRec)

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      NYU.denpyou_unique_no '伝票ユニークNO' ")
        cmdTextSb.Append("    , NYU.denpyou_no '伝票番号' ")
        cmdTextSb.Append("    , NYU.denpyou_syubetu '伝票種別' ")
        cmdTextSb.Append("    , NYU.torikesi_moto_denpyou_unique_no '取消元伝票ユニークNO' ")
        cmdTextSb.Append("    , NYU.nyuukin_torikomi_unique_no '入金取込ユニークNO' ")
        cmdTextSb.Append("    , NYU.seikyuu_saki_cd '請求先コード' ")
        cmdTextSb.Append("    , NYU.seikyuu_saki_brc '請求先枝番' ")
        cmdTextSb.Append("    , NYU.seikyuu_saki_kbn '請求先区分' ")
        cmdTextSb.Append("    , NYU.seikyuu_saki_mei '請求先名' ")
        cmdTextSb.Append("    , NYU.syougou_kouza_no '照合口座No.' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, NYU.nyuukin_date, 111) '入金日' ")
        cmdTextSb.Append("    , NYU.genkin '入金額 [現金]' ")
        cmdTextSb.Append("    , NYU.kogitte '入金額 [小切手]' ")
        cmdTextSb.Append("    , NYU.furikomi '入金額 [振込]' ")
        cmdTextSb.Append("    , NYU.tegata '入金額 [手形]' ")
        cmdTextSb.Append("    , NYU.sousai '入金額 [相殺]' ")
        cmdTextSb.Append("    , NYU.nebiki '入金額 [値引]' ")
        cmdTextSb.Append("    , NYU.sonota '入金額 [その他]' ")
        cmdTextSb.Append("    , NYU.kyouryoku_kaihi '入金額 [協力会費]' ")
        cmdTextSb.Append("    , NYU.kouza_furikae '入金額 [口座振替]' ")
        cmdTextSb.Append("    , NYU.furikomi_tesuuryou '入金額 [振込手数料]' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, NYU.tegata_kijitu, 111) '手形期日' ")
        cmdTextSb.Append("    , NYU.tegata_no '手形NO' ")
        cmdTextSb.Append("    , NYU.tekiyou_mei '摘要名' ")
        cmdTextSb.Append("    , NYU.add_login_user_id '登録ログインユーザーID' ")
        cmdTextSb.Append("    , NYU.add_login_user_name '登録ログインユーザー名' ")
        cmdTextSb.Append("    , NYU.add_datetime '登録日時' ")
        cmdTextSb.Append(strCmnSql)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 入金データ取得用の共通SQLクエリを取得
    ''' </summary>
    ''' <param name="keyRec">入金データKeyレコード</param>
    ''' <returns>入金データ取得用の共通SQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetNyuukinCmnSql(ByVal keyRec As NyuukinDataKeyRecord) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetNyuukinCmnSql", keyRec)
        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append("   FROM ")
        cmdTextSb.Append("        t_nyuukin_data NYU WITH (READCOMMITTED) ")
        cmdTextSb.Append(" LEFT OUTER JOIN v_seikyuu_saki_info VIW WITH (READCOMMITTED) ")
        cmdTextSb.Append("    ON NYU.seikyuu_saki_cd = VIW.seikyuu_saki_cd ")
        cmdTextSb.Append("   AND NYU.seikyuu_saki_brc = VIW.seikyuu_saki_brc ")
        cmdTextSb.Append("   AND NYU.seikyuu_saki_kbn = VIW.seikyuu_saki_kbn ")
        cmdTextSb.Append(" WHERE 1 = 1 ")

        '***********************************************************************
        '伝票番号
        '***********************************************************************
        If Not (keyRec.DenNoFrom Is String.Empty) Or Not (keyRec.DenNoTo Is String.Empty) Then

            cmdTextSb.Append(" AND ")

            If Not (keyRec.DenNoFrom Is String.Empty) And Not (keyRec.DenNoTo Is String.Empty) Then
                ' 両方指定有りはBETWEEN
                cmdTextSb.Append(" NYU.denpyou_no BETWEEN " & DBparamDenNoFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBparamDenNoTo)
            Else
                If Not keyRec.DenNoFrom Is String.Empty Then
                    ' 伝票番号Fromのみ
                    cmdTextSb.Append(" NYU.denpyou_no >= " & DBparamDenNoFrom)
                Else
                    ' 伝票番号Toのみ
                    cmdTextSb.Append(" NYU.denpyou_no <= " & DBparamDenNoTo)
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
                cmdTextSb.Append(" CONVERT(VARCHAR, NYU.add_datetime ,111) BETWEEN " & DBparamAddDatetimeFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBparamAddDatetimeTo)
            Else
                If keyRec.AddDatetimeFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, NYU.add_datetime ,111) >= " & DBparamAddDatetimeFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, NYU.add_datetime ,111) <= " & DBparamAddDatetimeTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 入金日
        '***********************************************************************
        '日付が一つでも設定されている場合、条件を作成
        If keyRec.NyuukinDateFrom <> DateTime.MinValue Or _
            keyRec.NyuukinDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.NyuukinDateFrom <> DateTime.MinValue And _
                keyRec.NyuukinDateTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, NYU.nyuukin_date ,111) ")
                cmdTextSb.Append(" BETWEEN " & DBparamNyuukinDateFrom)
                cmdTextSb.Append(" AND " & DBparamNyuukinDateTo)
            Else
                If keyRec.NyuukinDateFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, NYU.nyuukin_date ,111) >= " & DBparamNyuukinDateFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, NYU.nyuukin_date ,111) <= " & DBparamNyuukinDateTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 請求先区分
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiKbn) Then
            cmdTextSb.Append(" AND NYU.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn)
        End If

        '***********************************************************************
        ' 請求先コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiCd) Then
            cmdTextSb.Append(" AND NYU.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd)
        End If

        '***********************************************************************
        ' 請求先枝番
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiBrc) Then
            cmdTextSb.Append(" AND NYU.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc)
        End If

        '***********************************************************************
        ' 請求先名カナ
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiMeiKana) Then
            cmdTextSb.Append("   AND VIW.seikyuu_saki_kana like " & DBparamSeikyuuSakiMeiKana)
        End If

        '***********************************************************************
        '最新伝票のみ表示
        '***********************************************************************
        If keyRec.NewDenpyouDisp = 0 Then

            cmdTextSb.Append(" AND ")
            cmdTextSb.Append("  NYU.denpyou_unique_no IN ( ")
            cmdTextSb.Append(" SELECT ")
            cmdTextSb.Append("  MAX(NYU2.denpyou_unique_no) ")
            cmdTextSb.Append(" FROM ")
            cmdTextSb.Append("  t_nyuukin_data NYU2 ")
            cmdTextSb.Append(" GROUP BY ")
            cmdTextSb.Append("  NYU2.nyuukin_torikomi_unique_no ")
            cmdTextSb.Append(" ) ")

        End If

        '***********************************************************************
        '表示順序の付与（伝票ユニークNo→売上年月日→伝票番号）
        '***********************************************************************
        cmdTextSb.Append(" ORDER BY ")
        cmdTextSb.Append("     NYU.denpyou_unique_no ")
        cmdTextSb.Append("   , NYU.nyuukin_date ")
        cmdTextSb.Append("   , NYU.denpyou_no ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 入金データ取得用の共通SQLパラメータを取得
    ''' </summary>
    ''' <param name="keyRec">入金データKeyレコード</param>
    ''' <returns>入金データ取得用の共通SQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetNyuSqlCmnParams(ByVal keyRec As NyuukinDataKeyRecord) As SqlParameter()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetNyuSqlCmnParams", keyRec)

        Dim dtAddDateFrom As Object = IIf(keyRec.AddDatetimeFrom = DateTime.MinValue, DBNull.Value, keyRec.AddDatetimeFrom)
        Dim dtAddDateTo As Object = IIf(keyRec.AddDatetimeTo = DateTime.MinValue, DBNull.Value, keyRec.AddDatetimeTo)
        Dim dtSeikyuuFrom As Object = IIf(keyRec.NyuukinDateFrom = DateTime.MinValue, DBNull.Value, keyRec.NyuukinDateFrom)
        Dim dtSeikyuuTo As Object = IIf(keyRec.NyuukinDateTo = DateTime.MinValue, DBNull.Value, keyRec.NyuukinDateTo)

        'パラメータへ設定
        Dim cmdParams() As SqlParameter = _
         {SQLHelper.MakeParam(DBparamSeikyuuSakiMeiKana, SqlDbType.VarChar, 100, keyRec.SeikyuuSakiMeiKana & Chr(37)), _
         SQLHelper.MakeParam(DBparamDenNoFrom, SqlDbType.Char, 5, keyRec.DenNoFrom), _
         SQLHelper.MakeParam(DBparamDenNoTo, SqlDbType.Char, 5, keyRec.DenNoTo), _
         SQLHelper.MakeParam(DBparamAddDatetimeFrom, SqlDbType.DateTime, 16, dtAddDateFrom), _
         SQLHelper.MakeParam(DBparamAddDatetimeTo, SqlDbType.DateTime, 16, dtAddDateTo), _
         SQLHelper.MakeParam(DBparamNyuukinDateFrom, SqlDbType.DateTime, 16, dtSeikyuuFrom), _
         SQLHelper.MakeParam(DBparamNyuukinDateTo, SqlDbType.DateTime, 16, dtSeikyuuTo), _
         SQLHelper.MakeParam(DBparamSeikyuuSakiKbn, SqlDbType.Char, 1, keyRec.SeikyuuSakiKbn), _
         SQLHelper.MakeParam(DBparamSeikyuuSakiCd, SqlDbType.VarChar, 5, keyRec.SeikyuuSakiCd), _
         SQLHelper.MakeParam(DBparamSeikyuuSakiBrc, SqlDbType.VarChar, 2, keyRec.SeikyuuSakiBrc)}

        Return cmdParams

    End Function

#Region "SQLパラメータ"
    'パラメータ
    Private Const DBparamSeikyuuSakiMeiKana As String = "@SEIKYUUSAKIMEIKANA"
    Private Const DBparamDenNoFrom As String = "@DENNOFROM"
    Private Const DBparamDenNoTo As String = "@DENNOTO"
    Private Const DBparamAddDatetimeFrom As String = "@ADDDATETIMEFROM"
    Private Const DBparamAddDatetimeTo As String = "@ADDDATETIMETO"
    Private Const DBparamNyuukinDateFrom As String = "@NYUUKIN_DATE_FROM"
    Private Const DBparamNyuukinDateTo As String = "@NYUUKIN_DATE_TO"
    Private Const DBparamSeikyuuSakiKbn As String = "@SEIKYUUSAKIKBN"
    Private Const DBparamSeikyuuSakiCd As String = "@SEIKYUUSAKICD"
    Private Const DBparamSeikyuuSakiBrc As String = "@SEIKYUUSAKIBRC"
#End Region

End Class
