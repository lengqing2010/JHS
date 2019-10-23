Imports System
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 支払データの取得クラスです
''' </summary>
''' <remarks></remarks>
Public Class SiharaiDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim strLogic As New StringLogic

#End Region

#Region "支払データの取得"
    ''' <summary>
    ''' 支払データを取得します
    ''' </summary>
    ''' <param name="keyRec">支払データKeyレコード</param>
    ''' <returns>支払データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSiharaiDataInfo(ByVal keyRec As SiharaiDataKeyRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiharaiDataInfo", keyRec)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        Dim strCmnSql As String = GetCmnSql(keyRec)
        Dim cmdParams() As SqlParameter = GetSqlCmnParams(keyRec)
        Dim cmnDtAcc As New CmnDataAccess

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append(" SD.denpyou_unique_no ")
        cmdTextSb.Append(" ,SD.denpyou_no ")
        cmdTextSb.Append(" ,SD.denpyou_syubetu ")
        cmdTextSb.Append(" ,SD.torikesi_moto_denpyou_unique_no ")
        cmdTextSb.Append(" ,TK.tys_kaisya_cd ")
        cmdTextSb.Append(" ,TK.jigyousyo_cd ")
        cmdTextSb.Append(" ,SD.skk_siwake_unique_no ")
        cmdTextSb.Append(" ,SD.skk_jigyou_cd ")
        cmdTextSb.Append(" ,SD.skk_shri_saki_cd ")
        cmdTextSb.Append(" ,SD.shri_saki_mei ")
        cmdTextSb.Append(" ,SD.siharai_date ")
        cmdTextSb.Append(" ,SD.furikomi ")
        cmdTextSb.Append(" ,SD.sousai ")
        cmdTextSb.Append(" ,SD.tekiyou_mei ")
        cmdTextSb.Append(" ,SD.add_login_user_id ")
        cmdTextSb.Append(" ,SD.add_login_user_name ")
        cmdTextSb.Append(" ,SD.add_datetime ")
        cmdTextSb.Append(" ,SD.upd_login_user_id ")
        cmdTextSb.Append(" ,SD.upd_datetime ")
        cmdTextSb.Append(strCmnSql)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 支払データ[CSV出力用]を取得します
    ''' </summary>
    ''' <param name="keyRec">支払データKeyレコード</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSiharaiDataCsv(ByVal keyRec As SiharaiDataKeyRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiharaiDataCsv", keyRec)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        Dim strCmnSql As String = GetCmnSql(keyRec)
        Dim cmdParams() As SqlParameter = GetSqlCmnParams(keyRec)
        Dim cmnDtAcc As New CmnDataAccess

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append(" SD.denpyou_unique_no '伝票ユニークNO' ")
        cmdTextSb.Append(" ,SD.denpyou_no '伝票番号' ")
        cmdTextSb.Append(" ,SD.denpyou_syubetu '伝票種別 '")
        cmdTextSb.Append(" ,SD.torikesi_moto_denpyou_unique_no'取消元伝票ユニークNO' ")
        cmdTextSb.Append(" ,TK.tys_kaisya_cd '調査会社コード'")
        cmdTextSb.Append(" ,SD.skk_siwake_unique_no '新会計仕訳ユニークNO' ")
        cmdTextSb.Append(" ,SD.skk_jigyou_cd '新会計事業所コード' ")
        cmdTextSb.Append(" ,SD.skk_shri_saki_cd '新会計支払先コード' ")
        cmdTextSb.Append(" ,SD.shri_saki_mei '支払先名' ")
        cmdTextSb.Append(" ,CONVERT(VARCHAR, SD.siharai_date, 111) '支払年月日' ")
        cmdTextSb.Append(" ,SD.furikomi '振込額' ")
        cmdTextSb.Append(" ,SD.sousai '相殺額' ")
        cmdTextSb.Append(" ,SD.tekiyou_mei '摘要' ")
        cmdTextSb.Append(" ,SD.add_login_user_id '登録ログインユーザーID' ")
        cmdTextSb.Append(" ,SD.add_login_user_name '登録ログインユーザー名' ")
        cmdTextSb.Append(" ,SD.add_datetime '登録日時' ")
        cmdTextSb.Append(strCmnSql)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' データ取得用の共通SQLクエリを取得
    ''' </summary>
    ''' <param name="keyRec">売上データKeyレコード</param>
    ''' <returns>データ取得用の共通SQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSql(ByVal keyRec As SiharaiDataKeyRecord) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSql", keyRec)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" FROM  ")
        cmdTextSb.Append(" t_siharai_data SD WITH(READCOMMITTED) ")
        cmdTextSb.Append(" LEFT OUTER JOIN ")
        cmdTextSb.Append(" m_tyousakaisya TK WITH(READCOMMITTED) ")
        cmdTextSb.Append(" ON SD.skk_jigyou_cd = TK.skk_jigyousyo_cd ")
        cmdTextSb.Append(" AND SD.skk_shri_saki_cd = TK.skk_shri_saki_cd ")
        cmdTextSb.Append(" AND TK.jigyousyo_cd = TK.shri_jigyousyo_cd ")

        cmdTextSb.Append(" WHERE 1 = 1 ")

        '***********************************************************************
        ' 支払年月日
        '***********************************************************************
        If keyRec.ShriDateFrom <> DateTime.MinValue And _
            keyRec.ShriDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")
            cmdTextSb.Append(" CONVERT(VARCHAR, SD.siharai_date ,111) BETWEEN " & DBparamShriDateFrom)
            cmdTextSb.Append(" AND ")
            cmdTextSb.Append(DBparamShriDateTo)
        End If

        '***********************************************************************
        '伝票番号
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.DenNoFrom) Or Not String.IsNullOrEmpty(keyRec.DenNoTo) Then

            cmdTextSb.Append(" AND ")

            If Not (keyRec.DenNoFrom Is String.Empty) And Not (keyRec.DenNoTo Is String.Empty) Then
                ' 両方指定有りはBETWEEN
                cmdTextSb.Append(" SD.denpyou_no BETWEEN " & DBparamDenNoFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBparamDenNoTo)
            Else
                If Not keyRec.DenNoFrom Is String.Empty Then
                    ' 伝票番号Fromのみ
                    cmdTextSb.Append(" SD.denpyou_no >= " & DBparamDenNoFrom)
                Else
                    ' 伝票番号Toのみ
                    cmdTextSb.Append(" SD.denpyou_no <= " & DBparamDenNoTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 調査会社コード + 事業所コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.TysKaisyaCd) Then
            cmdTextSb.Append(" AND TK.tys_kaisya_cd + TK.jigyousyo_cd = " & DBparamTysKaisyaCd)
        End If

        '***********************************************************************
        ' 新会計事業所コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SkkJigyouCd) Then
            cmdTextSb.Append(" AND SD.skk_jigyou_cd = " & DBparamSkkJigyousyoCd)
        End If

        '***********************************************************************
        ' 新会計支払先コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SkkShriSakiCd) Then
            cmdTextSb.Append(" AND SD.skk_shri_saki_cd = " & DBparamSkkShriSakiCd)
        End If

        '***********************************************************************
        '最新伝票のみ表示
        '***********************************************************************
        If keyRec.NewDenDisp = 0 Then
            cmdTextSb.Append(" AND SD.denpyou_unique_no IN ")
            cmdTextSb.Append("    (SELECT ")
            cmdTextSb.Append("          MAX(SD2.denpyou_unique_no) ")
            cmdTextSb.Append("     FROM ")
            cmdTextSb.Append("          t_siharai_data SD2 ")
            cmdTextSb.Append("     GROUP BY ")
            cmdTextSb.Append("          SD2.skk_siwake_unique_no ")
            cmdTextSb.Append("    ) ")
        End If

        '***********************************************************************
        '表示順序の付与（伝票ユニークNo→支払年月日→伝票番号）
        '***********************************************************************
        cmdTextSb.Append(" ORDER BY ")
        cmdTextSb.Append("     SD.denpyou_unique_no ")
        cmdTextSb.Append("   , SD.siharai_date ")
        cmdTextSb.Append("   , SD.denpyou_no ")

        Return cmdTextSb.ToString

    End Function

    ''' <summary>
    ''' データ取得用の共通SQLパラメータを取得
    ''' </summary>
    ''' <param name="keyRec">売上データKeyレコード</param>
    ''' <returns>データ取得用の共通SQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetSqlCmnParams(ByVal keyRec As SiharaiDataKeyRecord) As SqlParameter()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSqlCmnParams", keyRec)

        Dim dtShriFrom As Object = IIf(keyRec.ShriDateFrom = DateTime.MinValue, DBNull.Value, keyRec.ShriDateFrom)
        Dim dtShriTo As Object = IIf(keyRec.ShriDateTo = DateTime.MinValue, DBNull.Value, keyRec.ShriDateTo)

        'パラメータへ設定
        Dim cmdParams() As SqlParameter = { _
                                         SQLHelper.MakeParam(DBparamShriDateFrom, SqlDbType.DateTime, 16, dtShriFrom), _
                                         SQLHelper.MakeParam(DBparamShriDateTo, SqlDbType.DateTime, 16, dtShriTo), _
                                         SQLHelper.MakeParam(DBparamDenNoFrom, SqlDbType.Char, 5, keyRec.DenNoFrom), _
                                         SQLHelper.MakeParam(DBparamDenNoTo, SqlDbType.Char, 5, keyRec.DenNoTo), _
                                         SQLHelper.MakeParam(DBparamTysKaisyaCd, SqlDbType.Char, 7, keyRec.TysKaisyaCd), _
                                         SQLHelper.MakeParam(DBparamSkkJigyousyoCd, SqlDbType.VarChar, 10, keyRec.SkkJigyouCd), _
                                         SQLHelper.MakeParam(DBparamSkkShriSakiCd, SqlDbType.VarChar, 10, keyRec.SkkShriSakiCd) _
                                        }
        Return cmdParams

    End Function

#Region "SQLパラメータ"
    Private Const DBparamShriDateFrom As String = "@SHRI_SAKI_DATE_FROM"
    Private Const DBparamShriDateTo As String = "@SHRI_SAKI_DATE_TO"
    Private Const DBparamDenNoFrom As String = "@DENNO_FROM"
    Private Const DBparamDenNoTo As String = "@DENNO_TO"
    Private Const DBparamTysKaisyaCd As String = "@TYOUSA_KAISYA_CD"
    Private Const DBparamSkkJigyousyoCd As String = "@SKK_JIGYOU_CD"
    Private Const DBparamSkkShriSakiCd As String = "@SKK_SHRI_SAKI_CD"

#End Region

#End Region

End Class
