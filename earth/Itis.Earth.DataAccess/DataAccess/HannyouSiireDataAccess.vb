Imports System.text
Imports System.Data.SqlClient

Public Class HannyouSiireDataAccess
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
#End Region

    ''' <summary>
    ''' 汎用仕入テーブルの情報を取得します
    ''' </summary>
    ''' <param name="keyRec">検索キーレコードクラス</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks>検索キーレコードクラスをKEYにして取得</remarks>
    Public Function getSearchTable(ByVal keyRec As HannyouSiireDataKeyRecord) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSearchTable" _
                                                    , keyRec _
                                                    )
        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter

        Const strPrmTourokuNengappiFrom As String = "@TOUROKU_NENGAPPI_FROM" '登録年月日_FROM
        Const strPrmTourokuNengappiTo As String = "@TOUROKU_NENGAPPI_TO" '登録年月日_TO
        Const strPrmSyouhinCd As String = "@SYOUHIN_CD" '商品コード
        Const strPrmTysKaisyaCd As String = "@TYOUSA_KAISYA_CD" '調査会社コード+事業所コード
        Const strPrmTysKaisyaMeiKana As String = "@TYOUSA_KAISYAMEI_KANA" '調査会社名カナ
        Const strPrmSiireDateFrom As String = "@SIIRE_DATE_FROM" '仕入年月日_FROM
        Const strPrmSiireDateTo As String = "@SIIRE_DATE_TO" '仕入年月日_TO
        Const strPrmDenpyouSiireDateFrom As String = "@DENPYOU_SIIRE_DATE_FROM" '伝票仕入年月日_FROM
        Const strPrmDenpyouSiireDateTo As String = "@DENPYOU_SIIRE_DATE_TO" '伝票仕入年月日_TO
        Const strPrmKbn As String = "@KBN" '区分
        Const strPrmBangou As String = "@BANGOU" '番号
        Const strPrmSesyumei As String = "@SESYU_MEI" '施主名

        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      HS.han_siire_unique_no ")
        cmdTextSb.Append("      , HS.tys_kaisya_cd ")
        cmdTextSb.Append("      , HS.tys_kaisya_jigyousyo_cd ")
        cmdTextSb.Append("      , TYS.tys_kaisya_mei ")
        cmdTextSb.Append("      , HS.syouhin_cd ")
        cmdTextSb.Append("      , MS.syouhin_mei ")
        cmdTextSb.Append("      , HS.tekiyou ")
        cmdTextSb.Append("      , HS.suu ")
        cmdTextSb.Append("      , HS.tanka ")
        cmdTextSb.Append("      , MSZ.zeiritu ")
        cmdTextSb.Append("      , HS.siire_date ")
        cmdTextSb.Append("      , HS.denpyou_siire_date ")
        cmdTextSb.Append("      , HS.syouhizei_gaku ")
        cmdTextSb.Append("      , HS.kbn")
        cmdTextSb.Append("      , HS.bangou")
        cmdTextSb.Append("      , HS.sesyu_mei")
        cmdTextSb.Append("  FROM t_hannyou_siire HS ")
        cmdTextSb.Append("  LEFT OUTER JOIN m_tyousakaisya TYS ")
        cmdTextSb.Append("      ON HS.tys_kaisya_cd = TYS.tys_kaisya_cd ")
        cmdTextSb.Append("      AND HS.tys_kaisya_jigyousyo_cd = TYS.jigyousyo_cd ")
        cmdTextSb.Append("  LEFT OUTER JOIN m_syouhin MS ")
        cmdTextSb.Append("      ON HS.syouhin_cd = MS.syouhin_cd ")
        cmdTextSb.Append("  LEFT OUTER JOIN m_syouhizei MSZ ")
        cmdTextSb.Append("      ON HS.zei_kbn = MSZ.zei_kbn ")
        cmdTextSb.Append("  WHERE 1 = 1 ")

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
                cmdTextSb.Append(" CONVERT(VARCHAR, HS.add_datetime ,111) BETWEEN " & strPrmTourokuNengappiFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(strPrmTourokuNengappiTo)
            Else
                If keyRec.AddDatetimeFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, HS.add_datetime ,111) >= " & strPrmTourokuNengappiFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, HS.add_datetime ,111) <= " & strPrmTourokuNengappiTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 商品コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SyouhinCd) Then
            cmdTextSb.Append(" AND HS.syouhin_cd = " & strPrmSyouhinCd)
        End If

        '***********************************************************************
        ' 調査会社コード + 事業所コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.TysKaisyaCd) Then
            cmdTextSb.Append(" AND HS.tys_kaisya_cd + HS.tys_kaisya_jigyousyo_cd = " & strPrmTysKaisyaCd)
        End If

        '***********************************************************************
        ' 調査会社名カナ
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.TysKaisyaMeiKana) Then
            cmdTextSb.Append("   AND TYS.tys_kaisya_mei_kana LIKE " & strPrmTysKaisyaMeiKana)
        End If

        '***********************************************************************
        ' 仕入年月日(From,To)
        '***********************************************************************
        '日付が一つでも設定されている場合、条件を作成
        If keyRec.SiireDateFrom <> DateTime.MinValue Or _
            keyRec.SiireDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.SiireDateFrom <> DateTime.MinValue And _
                keyRec.SiireDateTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, HS.siire_date ,111) BETWEEN " & strPrmSiireDateFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(strPrmSiireDateTo)
            Else
                If keyRec.SiireDateFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, HS.siire_date ,111) >= " & strPrmSiireDateFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, HS.siire_date ,111) <= " & strPrmSiireDateTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 伝票仕入年月日(From,To)
        '***********************************************************************
        '日付が一つでも設定されている場合、条件を作成
        If keyRec.DenpyouSiireDateFrom <> DateTime.MinValue Or _
            keyRec.DenpyouSiireDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.DenpyouSiireDateFrom <> DateTime.MinValue And _
                keyRec.DenpyouSiireDateTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, HS.denpyou_siire_date ,111) BETWEEN " & strPrmDenpyouSiireDateFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(strPrmDenpyouSiireDateTo)
            Else
                If keyRec.DenpyouSiireDateFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, HS.denpyou_siire_date ,111) >= " & strPrmDenpyouSiireDateFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, HS.denpyou_siire_date ,111) <= " & strPrmDenpyouSiireDateTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 区分
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.Kbn) Then
            cmdTextSb.Append(" AND HS.kbn = " & strPrmKbn)
        End If

        '***********************************************************************
        ' 番号
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.Bangou) Then
            cmdTextSb.Append(" AND HS.bangou = " & strPrmBangou)
        End If

        '***********************************************************************
        ' 施主名
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SesyuMei) Then
            cmdTextSb.Append(" AND HS.sesyu_mei = " & strPrmSesyumei)
        End If

        '***********************************************************************
        ' 取消
        '***********************************************************************
        If keyRec.Torikesi = 0 Then
            cmdTextSb.Append("  AND HS.torikesi = 0 ")
        End If

        '***********************************************************************
        '表示順序の付与（売上年月日,汎用売上ユニークNO）
        '***********************************************************************
        cmdTextSb.Append(" ORDER BY ")
        cmdTextSb.Append("   HS.siire_date ")
        cmdTextSb.Append("   , HS.han_siire_unique_no ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(strPrmTourokuNengappiFrom, SqlDbType.DateTime, 16, IIf(keyRec.AddDatetimeFrom = DateTime.MinValue, DBNull.Value, keyRec.AddDatetimeFrom)), _
            SQLHelper.MakeParam(strPrmTourokuNengappiTo, SqlDbType.DateTime, 16, IIf(keyRec.AddDatetimeTo = DateTime.MinValue, DBNull.Value, keyRec.AddDatetimeTo)), _
            SQLHelper.MakeParam(strPrmSyouhinCd, SqlDbType.VarChar, 8, keyRec.SyouhinCd), _
            SQLHelper.MakeParam(strPrmTysKaisyaCd, SqlDbType.VarChar, 7, keyRec.TysKaisyaCd), _
            SQLHelper.MakeParam(strPrmTysKaisyaMeiKana, SqlDbType.VarChar, 40, keyRec.TysKaisyaMeiKana & Chr(37)), _
            SQLHelper.MakeParam(strPrmSiireDateFrom, SqlDbType.DateTime, 16, IIf(keyRec.SiireDateFrom = DateTime.MinValue, DBNull.Value, keyRec.SiireDateFrom)), _
            SQLHelper.MakeParam(strPrmSiireDateTo, SqlDbType.DateTime, 16, IIf(keyRec.SiireDateTo = DateTime.MinValue, DBNull.Value, keyRec.SiireDateTo)), _
            SQLHelper.MakeParam(strPrmDenpyouSiireDateFrom, SqlDbType.DateTime, 16, IIf(keyRec.DenpyouSiireDateFrom = DateTime.MinValue, DBNull.Value, keyRec.DenpyouSiireDateFrom)), _
            SQLHelper.MakeParam(strPrmDenpyouSiireDateTo, SqlDbType.DateTime, 16, IIf(keyRec.DenpyouSiireDateTo = DateTime.MinValue, DBNull.Value, keyRec.DenpyouSiireDateTo)), _
            SQLHelper.MakeParam(strPrmKbn, SqlDbType.Char, 1, keyRec.Kbn), _
            SQLHelper.MakeParam(strPrmBangou, SqlDbType.VarChar, 10, keyRec.Bangou), _
            SQLHelper.MakeParam(strPrmSesyumei, SqlDbType.VarChar, 50, keyRec.SesyuMei)}

        ' データ取得＆返却
        Dim cmnDtAcc As New CmnDataAccess
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 汎用仕入テーブルの情報をPKで取得します
    ''' </summary>
    ''' <param name="intHanSiireNo">主キー項目値</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks>検索キーレコードクラスをKEYにして取得</remarks>
    Public Function getSearchTable(ByVal intHanSiireNo As Integer) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSearchTable" _
                                                    , intHanSiireNo _
                                                    )
        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter

        Const strPrmHanSiireNo As String = "@HANNYOU_SIIRE_UNIQUE_NO" '汎用仕入ユニークNO

        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      HS.han_siire_unique_no ")
        cmdTextSb.Append("      , HS.torikesi ")
        cmdTextSb.Append("      , HS.tekiyou ")
        cmdTextSb.Append("      , HS.siire_date ")
        cmdTextSb.Append("      , HS.denpyou_siire_date ")
        cmdTextSb.Append("      , HS.tys_kaisya_cd ")
        cmdTextSb.Append("      , HS.tys_kaisya_jigyousyo_cd ")
        cmdTextSb.Append("      , TYS.tys_kaisya_mei ")
        cmdTextSb.Append("      , HS.kameiten_cd ")
        cmdTextSb.Append("      , KT.kameiten_mei1 ")
        cmdTextSb.Append("      , HS.syouhin_cd ")
        cmdTextSb.Append("      , MS.syouhin_mei ")
        cmdTextSb.Append("      , HS.suu ")
        cmdTextSb.Append("      , HS.tanka ")
        cmdTextSb.Append("      , HS.zei_kbn ")
        cmdTextSb.Append("      , HS.syouhizei_gaku ")
        cmdTextSb.Append("      , MSZ.zeiritu ")
        cmdTextSb.Append("      , HS.siire_keijyou_flg ")
        cmdTextSb.Append("      , HS.siire_keijyou_date ")
        cmdTextSb.Append("      , HS.kbn")
        cmdTextSb.Append("      , HS.bangou")
        cmdTextSb.Append("      , HS.sesyu_mei")
        cmdTextSb.Append("      , HS.add_login_user_id ")
        cmdTextSb.Append("      , HS.add_login_user_name ")
        cmdTextSb.Append("      , HS.add_datetime ")
        cmdTextSb.Append("      , ISNULL(HS.upd_login_user_id,HS.add_login_user_id) AS upd_login_user_id ")
        cmdTextSb.Append("      , HS.upd_login_user_name ")
        cmdTextSb.Append("      , ISNULL(HS.upd_datetime,HS.add_datetime) AS upd_datetime")
        cmdTextSb.Append("  FROM t_hannyou_siire HS ")
        cmdTextSb.Append("  LEFT OUTER JOIN m_kameiten KT ")
        cmdTextSb.Append("      ON HS.kameiten_cd = KT.kameiten_cd ")
        cmdTextSb.Append("  LEFT OUTER JOIN m_tyousakaisya TYS ")
        cmdTextSb.Append("      ON HS.tys_kaisya_cd = TYS.tys_kaisya_cd ")
        cmdTextSb.Append("      AND HS.tys_kaisya_jigyousyo_cd = TYS.jigyousyo_cd ")
        cmdTextSb.Append("  LEFT OUTER JOIN m_syouhin MS ")
        cmdTextSb.Append("      ON HS.syouhin_cd = MS.syouhin_cd ")
        cmdTextSb.Append("  LEFT OUTER JOIN m_syouhizei MSZ ")
        cmdTextSb.Append("      ON HS.zei_kbn = MSZ.zei_kbn ")
        cmdTextSb.Append("  WHERE 1 = 1 ")

        '***********************************************************************
        ' 汎用売上ユニークNO
        '***********************************************************************
        cmdTextSb.Append("  AND HS.han_siire_unique_no = " & strPrmHanSiireNo)

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(strPrmHanSiireNo, SqlDbType.Int, 4, intHanSiireNo) _
        }

        ' データ取得＆返却
        Dim cmnDtAcc As New CmnDataAccess
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

End Class
