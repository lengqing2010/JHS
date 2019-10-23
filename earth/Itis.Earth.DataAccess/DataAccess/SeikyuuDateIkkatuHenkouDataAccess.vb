Imports System.Data.SqlClient
Imports System.Text

Public Class SeikyuuDateIkkatuHenkouDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "SQLパラメータ"

    Private Const DBparamSeikyuuSakiKbn As String = "@SEIKYUUSAKIKBN"
    Private Const DBparamSeikyuuSakiCd As String = "@SEIKYUUSAKICD"
    Private Const DBparamSeikyuuSakiBrc As String = "@SEIKYUUSAKIBRC"
    Private Const DBparamSeikyuuDate As String = "@SEIKYUUDATE"
    Private Const DBparamUpdDateTime As String = "@UPDDATETIME"
    Private Const DBparamLoginUserId As String = "@LOGINUSERID"

    Private sqlParams() As SqlClient.SqlParameter

#End Region

#Region "コンストラクタ"
    ''' <summary>
    ''' 更新用コンストラクタ
    ''' </summary>
    ''' <param name="seiCd">請求先コード</param>
    ''' <param name="seiBrc">請求先枝番</param>
    ''' <param name="seiKbn">請求先区分</param>
    ''' <param name="seiDate">請求年月日</param>
    ''' <param name="updDate">更新年月日</param>
    ''' <param name="updUser">更新ログインユーザーID</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal seiCd As String, _
                   ByVal seiBrc As String, _
                   ByVal seiKbn As String, _
                   ByVal seiDate As Date, _
                   ByVal updDate As DateTime, _
                   ByVal updUser As String)

        'SQLパラメータへ設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiCd, SqlDbType.VarChar, 5, seiCd), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiBrc, SqlDbType.VarChar, 2, seiBrc), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiKbn, SqlDbType.Char, 1, seiKbn), _
                                        SQLHelper.MakeParam(DBparamSeikyuuDate, SqlDbType.DateTime, 1, seiDate.Date), _
                                        SQLHelper.MakeParam(DBparamUpdDateTime, SqlDbType.DateTime, 1, updDate), _
                                        SQLHelper.MakeParam(DBparamLoginUserId, SqlDbType.VarChar, 30, updUser) _
                                        }
    End Sub

    ''' <summary>
    ''' 検索用コンストラクタ
    ''' </summary>
    ''' <param name="seiCd">請求先コード</param>
    ''' <param name="seiBrc">請求先枝番</param>
    ''' <param name="seiKbn">請求先区分</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal seiCd As String, _
                   ByVal seiBrc As String, _
                   ByVal seiKbn As String)

        'SQLパラメータへ設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiCd, SqlDbType.VarChar, 5, seiCd), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiBrc, SqlDbType.VarChar, 2, seiBrc), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiKbn, SqlDbType.Char, 1, seiKbn) _
                                        }
    End Sub
#End Region


#Region "請求年月日一括変更処理"

    ''' <summary>
    ''' 請求書発行日一括更新＠邸別請求テーブル
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updSeikyuuDateTeibetuSeikyuu() As Integer

        Dim intResult As Integer = 0
        Dim sb As New StringBuilder()
        'TABLE句
        Dim strTeibetuSeikyuuTable As String = Me.GetTeibetuSeikyuuSqlTable()
        'WHERE句
        Dim strTeibetuSeikyuuWhere As String = Me.GetTeibetuSeikyuuSqlWhere()

        ' 更新用SQL設定
        sb.AppendLine("UPDATE jhs_sys.t_teibetu_seikyuu ")
        sb.AppendLine("SET")
        sb.AppendLine("    seikyuusyo_hak_date = " & DBparamSeikyuuDate & "")
        sb.AppendLine("    , upd_login_user_id = " & DBparamLoginUserId & "")
        sb.AppendLine("    , upd_datetime = " & DBparamUpdDateTime & " ")

        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_teibetu_seikyuu ts ")

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(strTeibetuSeikyuuWhere)

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sb.ToString(), _
                                    sqlParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 請求書発行日一括更新＠店別請求テーブル
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updSeikyuuDateTenbetuSeikyuu() As Integer

        Dim intResult As Integer = 0
        Dim sb As New StringBuilder()
        'TABLE句
        Dim strTenbetuSeikyuuTable As String = Me.GetTenbetuSeikyuuSqlTable()
        'WHERE句
        Dim strTenbetuSeikyuuWhere As String = Me.GetTenbetuSeikyuuSqlWhere()

        sb.AppendLine("UPDATE jhs_sys.t_tenbetu_seikyuu ")
        sb.AppendLine("SET")
        sb.AppendLine("    seikyuusyo_hak_date = " & DBparamSeikyuuDate & "")
        sb.AppendLine("    , upd_login_user_id = " & DBparamLoginUserId & "")
        sb.AppendLine("    , upd_datetime = " & DBparamUpdDateTime & " ")

        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_tenbetu_seikyuu ts ")

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(strTenbetuSeikyuuWhere)

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sb.ToString(), _
                                    sqlParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 請求書発行日一括更新＠店別初期請求テーブル
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updSeikyuuDateTenbetuSyokiSeikyuu() As Integer

        Dim intResult As Integer = 0
        Dim sb As New StringBuilder()
        'TABLE句
        Dim strTenbetuSyokiSeikyuuTable As String = Me.GetTenbetuSyokiSeikyuuSqlTable()
        'WHERE句
        Dim strTenbetuSyokiSeikyuuWhere As String = Me.GetTenbetuSyokiSeikyuuSqlWhere()

        ' 更新用SQL設定
        sb.AppendLine("UPDATE jhs_sys.t_tenbetu_syoki_seikyuu ")
        sb.AppendLine("SET")
        sb.AppendLine("    seikyuusyo_hak_date = " & DBparamSeikyuuDate & "")
        sb.AppendLine("    , upd_login_user_id = " & DBparamLoginUserId & "")
        sb.AppendLine("    , upd_datetime = " & DBparamUpdDateTime & " ")

        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_tenbetu_syoki_seikyuu ts ")

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(strTenbetuSyokiSeikyuuWhere)

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sb.ToString(), _
                                    sqlParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 請求書発行日一括更新＠汎用売上テーブル
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updSeikyuuDateHannyouUriage() As Integer

        Dim intResult As Integer = 0
        Dim sb As New StringBuilder()
        'TABLE句
        Dim strHannyouUriageTable As String = Me.GetHannyouUriageSqlTable()
        'WHERE句
        Dim strHannyouUriageWhere As String = Me.GetHannyouUriageSqlWhere()

        ' 更新用SQL設定
        sb.AppendLine("UPDATE jhs_sys.t_hannyou_uriage ")
        sb.AppendLine("SET")
        sb.AppendLine("    seikyuu_date = " & DBparamSeikyuuDate & "")
        sb.AppendLine("    , upd_login_user_id = " & DBparamLoginUserId & "")
        sb.AppendLine("    , upd_datetime = " & DBparamUpdDateTime & " ")

        '****************
        '* TABLE項目
        '****************
        sb.AppendLine(strHannyouUriageTable)

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(strHannyouUriageWhere)

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sb.ToString(), _
                                    sqlParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 売上データテーブルの請求年月日をUPDATE(元テーブルが削除されたマイナス伝票を対象)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateUriDataSeikyuuDate() As Integer

        Dim intResult As Integer = 0
        Dim sb As New StringBuilder()
        'TABLE句
        Dim strUriageDataTable As String = Me.GetUriageDataSqlTable()
        'WHERE句
        Dim strUriageDataWhere As String = Me.GetUriageDataSqlWhere()

        'SQL文生成
        sb.AppendLine("UPDATE jhs_sys.t_uriage_data ")
        sb.AppendLine("SET")
        sb.AppendLine("    seikyuu_date = " & DBparamSeikyuuDate & "")
        sb.AppendLine("    , upd_login_user_id = " & DBparamLoginUserId & "")
        sb.AppendLine("    , upd_datetime = " & DBparamUpdDateTime & " ")

        '****************
        '* TABLE項目
        '****************
        sb.AppendLine(strUriageDataTable)

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(strUriageDataWhere)

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sb.ToString(), _
                                    sqlParams)

        Return intResult
    End Function

    ''' <summary>
    ''' 売上データテーブルの請求年月日をUPDATE(取消元伝票データより、請求年月日を取得および更新)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateTorikesiUriDataSeikyuuDate() As Integer

        Dim intResult As Integer = 0
        Dim sb As New StringBuilder()
        'TABLE句
        Dim strUriageDataTable As String = Me.GetUriageDataSeikyuuDateSqlTable()
        'WHERE句
        Dim strUriageDataWhere As String = Me.GetUriageDataSeikyuuDateSqlWhere()

        'SQL文生成
        sb.AppendLine("UPDATE jhs_sys.t_uriage_data ")
        sb.AppendLine("SET")
        sb.AppendLine("    seikyuu_date = ud2.seikyuu_date ")
        sb.AppendLine("    , upd_login_user_id = " & DBparamLoginUserId & "")
        sb.AppendLine("    , upd_datetime = " & DBparamUpdDateTime & " ")

        '****************
        '* TABLE項目
        '****************
        sb.AppendLine(strUriageDataTable)

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(strUriageDataWhere)

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sb.ToString(), _
                                    sqlParams)

        Return intResult
    End Function

#End Region

#Region "一括変更対象データの取得"

    ''' <summary>
    ''' 請求年月日一括変更処理実行画面/邸別請求データを取得します
    ''' </summary>
    ''' <returns>邸別請求テーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuTbl() As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuTbl")

        'SQL文の生成
        Dim sb As New StringBuilder

        'SELECT句
        Dim strTeibetuSeikyuuSelect As String = Me.GetTeibetuSeikyuuSqlSelect()
        'TABLE句
        Dim strTeibetuSeikyuuTable As String = Me.GetTeibetuSeikyuuSqlTable()
        'WHERE句
        Dim strTeibetuSeikyuuWhere As String = Me.GetTeibetuSeikyuuSqlWhere()
        'ORDER BY句
        Dim strTeibetuSeikyuuOrderBy As String = Me.GetTeibetuSeikyuuSqlOrderBy()

        '****************
        '* SELECT項目
        '****************
        sb.AppendLine(strTeibetuSeikyuuSelect)

        '****************
        '* TABLE項目
        '****************
        sb.AppendLine(strTeibetuSeikyuuTable)

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(strTeibetuSeikyuuWhere)

        '***********************************************************************
        ' ORDER BY句（区分→保証書NO→分類コード→画面表示NO）
        '***********************************************************************
        sb.Append(strTeibetuSeikyuuOrderBy)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)
    End Function

    ''' <summary>
    ''' 請求年月日一括変更処理実行画面/店別請求データを取得します
    ''' </summary>
    ''' <returns>店別請求テーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTenbetuSeikyuuTbl() As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSeikyuuTbl")

        'SQL文の生成
        Dim sb As New StringBuilder

        'SELECT句
        Dim strTenbetuSeikyuuSelect As String = Me.GetTenbetuSeikyuuSqlSelect()
        'TABLE句
        Dim strTenbetuSeikyuuTable As String = Me.GetTenbetuSeikyuuSqlTable()
        'WHERE句
        Dim strTenbetuSeikyuuWhere As String = Me.GetTenbetuSeikyuuSqlWhere()
        'ORDER BY句
        Dim strTenbetuSeikyuuOrderBy As String = Me.GetTenbetuSeikyuuSqlOrderBy()

        '****************
        '* SELECT項目
        '****************
        sb.AppendLine(strTenbetuSeikyuuSelect)

        '****************
        '* TABLE項目
        '****************
        sb.AppendLine(strTenbetuSeikyuuTable)

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(strTenbetuSeikyuuWhere)

        '***********************************************************************
        ' ORDER BY句（分類コード→店コード→入力日→入力日NO）
        '***********************************************************************
        sb.Append(strTenbetuSeikyuuOrderBy)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)
    End Function

    ''' <summary>
    ''' 請求年月日一括変更処理実行画面/店別初期請求データを取得します
    ''' </summary>
    ''' <returns>店別初期請求テーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTenbetuSyokiSeikyuuTbl() As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSyokiSeikyuuTbl")

        'SQL文の生成
        Dim sb As New StringBuilder

        'SELECT句
        Dim strTenbetuSyokiSeikyuuSelect As String = Me.GetTenbetuSyokiSeikyuuSqlSelect()
        'TABLE句
        Dim strTenbetuSyokiSeikyuuTable As String = Me.GetTenbetuSyokiSeikyuuSqlTable()
        'WHERE句
        Dim strTenbetuSyokiSeikyuuWhere As String = Me.GetTenbetuSyokiSeikyuuSqlWhere()
        'ORDER BY句
        Dim strTenbetuSyokiSeikyuuOrderBy As String = Me.GetTenbetuSyokiSeikyuuSqlOrderBy()

        '****************
        '* SELECT項目
        '****************
        sb.AppendLine(strTenbetuSyokiSeikyuuSelect)

        '****************
        '* TABLE項目
        '****************
        sb.AppendLine(strTenbetuSyokiSeikyuuTable)

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(strTenbetuSyokiSeikyuuWhere)

        '***********************************************************************
        ' ORDER BY句（店コード、分類コード）
        '***********************************************************************
        sb.Append(strTenbetuSyokiSeikyuuOrderBy)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)
    End Function

    ''' <summary>
    ''' 請求年月日一括変更処理実行画面/汎用売上データを取得します
    ''' </summary>
    ''' <returns>汎用売上テーブル</returns>
    ''' <remarks></remarks>
    Public Function GetHannyouUriageTbl() As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHannyouUriageTbl")

        'SQL文の生成
        Dim sb As New StringBuilder

        'SELECT句
        Dim strHannyouUriageSelect As String = Me.GetHannyouUriageSqlSelect()
        'TABLE句
        Dim strHannyouUriageTable As String = Me.GetHannyouUriageSqlTable()
        'WHERE句
        Dim strHannyouUriageWhere As String = Me.GetHannyouUriageSqlWhere()
        'ORDER BY句
        Dim strHannyouUriageOrderBy As String = Me.GetHannyouUriageSqlOrderBy()

        '****************
        '* SELECT項目
        '****************
        sb.AppendLine(strHannyouUriageSelect)

        '****************
        '* TABLE項目
        '****************
        sb.AppendLine(strHannyouUriageTable)

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(strHannyouUriageWhere)

        '***********************************************************************
        ' ORDER BY句（店コード、分類コード）
        '***********************************************************************
        sb.Append(strHannyouUriageOrderBy)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)
    End Function

    ''' <summary>
    ''' 請求年月日一括変更処理実行画面/売上データを取得します
    ''' </summary>
    ''' <returns>売上テーブル</returns>
    ''' <remarks></remarks>
    Public Function GetUriageDataTbl() As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataTbl")

        'SQL文の生成
        Dim sb As New StringBuilder

        'SELECT句
        Dim strUriageDataSelect As String = Me.GetUriageDataSqlSelect()
        'TABLE句
        Dim strUriageDataTable As String = Me.GetUriageDataSqlTable()
        'WHERE句
        Dim strUriageDataWhere As String = Me.GetUriageDataSqlWhere()
        'ORDER BY句
        Dim strUriageDataOrderBy As String = Me.GetUriageDataSqlOrderBy()

        '****************
        '* SELECT項目
        '****************
        sb.AppendLine(strUriageDataSelect)

        '****************
        '* TABLE項目
        '****************
        sb.AppendLine(strUriageDataTable)

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(strUriageDataWhere)

        '***********************************************************************
        ' ORDER BY句（店コード、分類コード）
        '***********************************************************************
        sb.Append(strUriageDataOrderBy)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)
    End Function

    ''' <summary>
    ''' 請求年月日一括変更処理実行画面/売上データを取得します
    ''' </summary>
    ''' <returns>売上テーブル</returns>
    ''' <remarks></remarks>
    Public Function GetUriageDataSeikyuuDateTbl() As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataSeikyuuDateTbl")

        'SQL文の生成
        Dim sb As New StringBuilder

        'SELECT句
        Dim strUriageDataSelect As String = Me.GetUriageDataSqlSelect()
        'TABLE句
        Dim strUriageDataTable As String = Me.GetUriageDataSeikyuuDateSqlTable()
        'WHERE句
        Dim strUriageDataWhere As String = Me.GetUriageDataSeikyuuDateSqlWhere()
        'ORDER BY句
        Dim strUriageDataOrderBy As String = Me.GetUriageDataSqlOrderBy()

        '****************
        '* SELECT項目
        '****************
        sb.AppendLine(strUriageDataSelect)

        '****************
        '* TABLE項目
        '****************
        sb.AppendLine(strUriageDataTable)

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(strUriageDataWhere)

        '***********************************************************************
        ' ORDER BY句（店コード、分類コード）
        '***********************************************************************
        sb.Append(strUriageDataOrderBy)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)
    End Function

#End Region

#Region "共通クエリ"

#Region "SELECT句"
    ''' <summary>
    ''' 邸別請求データ取得用の共通SELECTクエリを取得
    ''' </summary>
    ''' <returns>邸別請求データ取得用の共通SELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTeibetuSeikyuuSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuSqlSelect")

        'SQL文の生成
        Dim sb As New StringBuilder

        '****************
        '* SELECT項目
        '****************
        sb.AppendLine(" SELECT ")
        sb.AppendLine("      ts.kbn ")
        sb.AppendLine("    , ts.hosyousyo_no AS bangou ")
        sb.AppendLine("    , j.sesyu_mei ")
        sb.AppendLine("    , j.kameiten_cd ")
        sb.AppendLine("    , ts.syouhin_cd ")
        sb.AppendLine("    , ms.syouhin_mei ")
        sb.AppendLine("    , NULL AS suu ")
        sb.AppendLine("    , NULL AS tanka ")
        sb.AppendLine("    , ts.uri_gaku ")
        sb.AppendLine("    , ts.seikyuusyo_hak_date ")
        sb.AppendLine("    , ts.uri_date ")
        sb.AppendLine("    , ts.denpyou_uri_date ")
        sb.AppendLine("    , ISNULL(ts.upd_login_user_id, ts.add_login_user_id) AS upd_login_user_id ")
        sb.AppendLine("    , ISNULL(ts.upd_datetime, ts.add_datetime) AS upd_datetime ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 店別請求データ取得用の共通SELECTクエリを取得
    ''' </summary>
    ''' <returns>店別請求データ取得用の共通SELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSeikyuuSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSeikyuuSqlSelect")

        'SQL文の生成
        Dim sb As New StringBuilder

        '****************
        '* SELECT項目
        '****************
        sb.AppendLine(" SELECT ")
        sb.AppendLine("      NULL AS kbn ")
        sb.AppendLine("    , NULL AS bangou ")
        sb.AppendLine("    , NULL AS sesyu_mei ")
        sb.AppendLine("    , ts.mise_cd AS kameiten_cd ")
        sb.AppendLine("    , ts.syouhin_cd ")
        sb.AppendLine("    , ms.syouhin_mei ")
        sb.AppendLine("    , ts.suu ")
        sb.AppendLine("    , ts.tanka ")
        sb.AppendLine("    , NULL AS uri_gaku ")
        sb.AppendLine("    , ts.seikyuusyo_hak_date ")
        sb.AppendLine("    , ts.uri_date ")
        sb.AppendLine("    , ts.denpyou_uri_date ")
        sb.AppendLine("    , ISNULL(ts.upd_login_user_id, ts.add_login_user_id) AS upd_login_user_id ")
        sb.AppendLine("    , ISNULL(ts.upd_datetime, ts.add_datetime) AS upd_datetime ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 店別初期請求データ取得用の共通SELECTクエリを取得
    ''' </summary>
    ''' <returns>店別初期請求データ取得用の共通SELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSyokiSeikyuuSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSyokiSeikyuuSqlSelect")

        'SQL文の生成
        Dim sb As New StringBuilder

        '****************
        '* SELECT項目
        '****************
        sb.AppendLine(" SELECT ")
        sb.AppendLine("      NULL AS kbn ")
        sb.AppendLine("    , NULL AS bangou ")
        sb.AppendLine("    , NULL AS sesyu_mei ")
        sb.AppendLine("    , ts.mise_cd AS kameiten_cd ")
        sb.AppendLine("    , ts.syouhin_cd ")
        sb.AppendLine("    , ms.syouhin_mei ")
        sb.AppendLine("    , NULL AS suu ")
        sb.AppendLine("    , NULL AS tanka ")
        sb.AppendLine("    , ts.uri_gaku ")
        sb.AppendLine("    , ts.seikyuusyo_hak_date ")
        sb.AppendLine("    , ts.uri_date ")
        sb.AppendLine("    , ts.denpyou_uri_date ")
        sb.AppendLine("    , ISNULL(ts.upd_login_user_id, ts.add_login_user_id) AS upd_login_user_id ")
        sb.AppendLine("    , ISNULL(ts.upd_datetime, ts.add_datetime) AS upd_datetime ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 汎用売上データ取得用の共通SELECTクエリを取得
    ''' </summary>
    ''' <returns>汎用売上データ取得用の共通SELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetHannyouUriageSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHannyouUriageSqlSelect")

        'SQL文の生成
        Dim sb As New StringBuilder

        '****************
        '* SELECT項目
        '****************
        sb.AppendLine(" SELECT ")
        sb.AppendLine("      hu.kbn ")
        sb.AppendLine("    , hu.bangou ")
        sb.AppendLine("    , hu.sesyu_mei ")
        sb.AppendLine("    , NULL AS kameiten_cd ")
        sb.AppendLine("    , hu.syouhin_cd ")
        sb.AppendLine("    , hu.hin_mei AS syouhin_mei ")
        sb.AppendLine("    , hu.suu ")
        sb.AppendLine("    , hu.tanka ")
        sb.AppendLine("    , NULL AS uri_gaku ")
        sb.AppendLine("    , hu.seikyuu_date AS seikyuusyo_hak_date ")
        sb.AppendLine("    , hu.uri_date ")
        sb.AppendLine("    , hu.denpyou_uri_date ")
        sb.AppendLine("    , ISNULL(hu.upd_login_user_id, hu.add_login_user_id) AS upd_login_user_id ")
        sb.AppendLine("    , ISNULL(hu.upd_datetime, hu.add_datetime) AS upd_datetime ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 売上データ取得用の共通SELECTクエリを取得
    ''' </summary>
    ''' <returns>売上データ取得用の共通SELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetUriageDataSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataSqlSelect")

        'SQL文の生成
        Dim sb As New StringBuilder

        '****************
        '* SELECT項目
        '****************
        sb.AppendLine(" SELECT ")
        sb.AppendLine("      ud.kbn ")
        sb.AppendLine("    , ud.bangou ")
        sb.AppendLine("    , ISNULL(j.sesyu_mei, hu.sesyu_mei) AS sesyu_mei ")
        sb.AppendLine("    , ud.kameiten_cd ")
        sb.AppendLine("    , ud.syouhin_cd ")
        sb.AppendLine("    , ud.hinmei AS syouhin_mei ")
        sb.AppendLine("    , ud.suu ")
        sb.AppendLine("    , NULL AS tanka ")
        sb.AppendLine("    , ud.uri_gaku ")
        sb.AppendLine("    , ud.seikyuu_date AS seikyuusyo_hak_date ")
        sb.AppendLine("    , ud.uri_date ")
        sb.AppendLine("    , ud.denpyou_uri_date ")
        sb.AppendLine("    , ISNULL(ud.upd_login_user_id, ud.add_login_user_id) AS upd_login_user_id ")
        sb.AppendLine("    , ISNULL(ud.upd_datetime, ud.add_datetime) AS upd_datetime ")

        Return sb.ToString
    End Function

#End Region

#Region "TABLE句"
    ''' <summary>
    ''' 邸別請求データ取得用の共通TABLEクエリを取得
    ''' </summary>
    ''' <returns>請求データ取得用の共通TABLEクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTeibetuSeikyuuSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuSqlTable")

        'SQL文の生成
        Dim sb As New StringBuilder

        '****************
        '* TABLE項目(メイン)
        '****************
        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_teibetu_seikyuu ts ")
        sb.AppendLine("           LEFT OUTER JOIN t_jiban j ")
        sb.AppendLine("             ON ts.kbn = j.kbn ")
        sb.AppendLine("            AND ts.hosyousyo_no = j.hosyousyo_no ")
        sb.AppendLine("           LEFT OUTER JOIN m_syouhin ms ")
        sb.AppendLine("             ON ts.syouhin_cd = ms.syouhin_cd ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 店別請求データ取得用の共通TABLEクエリを取得
    ''' </summary>
    ''' <returns>店別請求データ取得用の共通TABLEクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSeikyuuSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSeikyuuSqlTable")

        'SQL文の生成
        Dim sb As New StringBuilder

        '****************
        '* TABLE項目(メイン)
        '****************
        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_tenbetu_seikyuu ts ")
        sb.AppendLine("           LEFT OUTER JOIN m_syouhin ms ")
        sb.AppendLine("             ON ts.syouhin_cd = ms.syouhin_cd ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 店別初期請求データ取得用の共通TABLEクエリを取得
    ''' </summary>
    ''' <returns>店別初期請求データ取得用の共通TABLEクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSyokiSeikyuuSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSyokiSeikyuuSqlTable")

        'SQL文の生成
        Dim sb As New StringBuilder

        '****************
        '* TABLE項目(メイン)
        '****************
        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_tenbetu_syoki_seikyuu ts ")
        sb.AppendLine("           LEFT OUTER JOIN m_syouhin ms ")
        sb.AppendLine("             ON ts.syouhin_cd = ms.syouhin_cd ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 汎用売上データ取得用の共通TABLEクエリを取得
    ''' </summary>
    ''' <returns>汎用売上データ取得用の共通TABLEクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetHannyouUriageSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHannyouUriageSqlTable")

        'SQL文の生成
        Dim sb As New StringBuilder

        '****************
        '* TABLE項目(メイン)
        '****************
        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_hannyou_uriage hu ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 売上データ取得用の共通TABLEクエリを取得
    ''' </summary>
    ''' <returns>売上データ取得用の共通TABLEクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetUriageDataSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataSqlTable")

        'SQL文の生成
        Dim sb As New StringBuilder

        '****************
        '* TABLE項目(メイン)
        '****************
        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_uriage_data ud ")
        sb.AppendLine("           LEFT OUTER JOIN t_jiban j ")
        sb.AppendLine("             ON ud.kbn = j.kbn ")
        sb.AppendLine("            AND ud.bangou = j.hosyousyo_no ")
        sb.AppendLine("           LEFT OUTER JOIN t_hannyou_uriage hu ")
        sb.AppendLine("             ON ud.himoduke_cd = CAST(hu.han_uri_unique_no AS VARCHAR) ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 売上データ取得用の共通TABLEクエリを取得
    ''' </summary>
    ''' <returns>売上データ取得用の共通TABLEクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetUriageDataSeikyuuDateSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataSeikyuuDateSqlTable")

        'SQL文の生成
        Dim sb As New StringBuilder

        '****************
        '* TABLE項目(メイン)
        '****************
        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_uriage_data ud ")
        sb.AppendLine("           LEFT OUTER JOIN ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.t_uriage_data ")
        sb.AppendLine("               ) ")
        sb.AppendLine("                ud2 ")
        sb.AppendLine("             ON ud.torikesi_moto_denpyou_unique_no = ud2.denpyou_unique_no ")

        Return sb.ToString

    End Function
#End Region

#Region "WHERE句"
    ''' <summary>
    ''' 邸別請求データ取得用の共通WHEREクエリを取得
    ''' </summary>
    ''' <returns>邸別請求データ取得用の共通WHEREクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTeibetuSeikyuuSqlWhere() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuSqlWhere")

        'SQL文の生成
        Dim sb As New StringBuilder()

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(" WHERE ")
        sb.AppendLine("      EXISTS ")
        sb.AppendLine("     (SELECT ")
        sb.AppendLine("           td.* ")
        sb.AppendLine("      FROM ")
        sb.AppendLine("          (SELECT ")
        sb.AppendLine("                dd.* ")
        sb.AppendLine("           FROM ")
        sb.AppendLine("                jhs_sys.v_seikyuusyo_mihakkou_uri_data dd ")
        sb.AppendLine("           WHERE ")
        sb.AppendLine("                --マイナス伝票の有るプラス伝票を除外 ")
        sb.AppendLine("                dd.denpyou_syubetu = 'UN' ")
        sb.AppendLine("            AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.v_seikyuusyo_mihakkou_uri_data dr ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     dr.denpyou_syubetu = 'UR' ")
        sb.AppendLine("                 AND dr.torikesi_moto_denpyou_unique_no = dd.denpyou_unique_no ")
        sb.AppendLine("               ) ")
        sb.AppendLine("          ) ")
        sb.AppendLine("           tt ")
        sb.AppendLine("                INNER JOIN jhs_sys.t_teibetu_seikyuu td ")
        sb.AppendLine("                  ON (tt.himoduke_table_type = 1 ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 1, 1) = td.kbn ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 5, 10) = td.hosyousyo_no ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 18, 2) = SUBSTRING(td.bunrui_cd, 1, 2) ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 24, LEN(tt.himoduke_cd)) = CAST(td.gamen_hyouji_no AS VARCHAR)) ")
        sb.AppendLine("      WHERE ")
        sb.AppendLine("           tt.seikyuu_date IS NOT NULL ")
        sb.AppendLine("       AND td.seikyuusyo_hak_date IS NOT NULL ")
        sb.AppendLine("       AND td.kbn = ts.kbn ")
        sb.AppendLine("       AND td.hosyousyo_no = ts.hosyousyo_no ")
        sb.AppendLine("       AND td.bunrui_cd = ts.bunrui_cd ")
        sb.AppendLine("       AND td.gamen_hyouji_no = ts.gamen_hyouji_no ")
        sb.AppendLine("       AND tt.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn & " ")
        sb.AppendLine("     ) ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 店別請求データ取得用の共通WHEREクエリを取得
    ''' </summary>
    ''' <returns>店別請求データ取得用の共通WHEREクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSeikyuuSqlWhere() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSeikyuuSqlWhere")

        'SQL文の生成
        Dim sb As New StringBuilder()

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(" WHERE ")
        sb.AppendLine("      EXISTS ")
        sb.AppendLine("     (SELECT ")
        sb.AppendLine("           td.* ")
        sb.AppendLine("      FROM ")
        sb.AppendLine("          (SELECT ")
        sb.AppendLine("                dd.* ")
        sb.AppendLine("           FROM ")
        sb.AppendLine("                jhs_sys.v_seikyuusyo_mihakkou_uri_data dd ")
        sb.AppendLine("           WHERE ")
        sb.AppendLine("                --マイナス伝票の有るプラス伝票を除外 ")
        sb.AppendLine("                dd.denpyou_syubetu = 'UN' ")
        sb.AppendLine("            AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.v_seikyuusyo_mihakkou_uri_data dr ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     dr.denpyou_syubetu = 'UR' ")
        sb.AppendLine("                 AND dr.torikesi_moto_denpyou_unique_no = dd.denpyou_unique_no ")
        sb.AppendLine("               ) ")
        sb.AppendLine("          ) ")
        sb.AppendLine("           tt ")
        sb.AppendLine("                INNER JOIN jhs_sys.t_tenbetu_seikyuu td ")
        sb.AppendLine("                  ON (tt.himoduke_table_type = 2 ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 1, 5) = td.mise_cd ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 9, 3) = td.bunrui_cd ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 15, 8) = CONVERT(VARCHAR, td.nyuuryoku_date, 112) ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 26, LEN(tt.himoduke_cd)) = CAST(td.nyuuryoku_date_no AS VARCHAR)) ")
        sb.AppendLine("      WHERE ")
        sb.AppendLine("           tt.seikyuu_date IS NOT NULL ")
        sb.AppendLine("       AND td.seikyuusyo_hak_date IS NOT NULL ")
        sb.AppendLine("       AND td.mise_cd = ts.mise_cd ")
        sb.AppendLine("       AND td.bunrui_cd = ts.bunrui_cd ")
        sb.AppendLine("       AND td.nyuuryoku_date = ts.nyuuryoku_date ")
        sb.AppendLine("       AND td.nyuuryoku_date_no = ts.nyuuryoku_date_no ")
        sb.AppendLine("       AND tt.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn & " ")
        sb.AppendLine("     ) ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 店別初期請求データ取得用の共通WHEREクエリを取得
    ''' </summary>
    ''' <returns>店別初期請求データ取得用の共通WHEREクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSyokiSeikyuuSqlWhere() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSyokiSeikyuuSqlWhere")

        'SQL文の生成
        Dim sb As New StringBuilder()

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(" WHERE ")
        sb.AppendLine("      EXISTS ")
        sb.AppendLine("     (SELECT ")
        sb.AppendLine("           td.* ")
        sb.AppendLine("      FROM ")
        sb.AppendLine("          (SELECT ")
        sb.AppendLine("                dd.* ")
        sb.AppendLine("           FROM ")
        sb.AppendLine("                jhs_sys.v_seikyuusyo_mihakkou_uri_data dd ")
        sb.AppendLine("           WHERE ")
        sb.AppendLine("                --マイナス伝票の有るプラス伝票を除外 ")
        sb.AppendLine("                dd.denpyou_syubetu = 'UN' ")
        sb.AppendLine("            AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.v_seikyuusyo_mihakkou_uri_data dr ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     dr.denpyou_syubetu = 'UR' ")
        sb.AppendLine("                 AND dr.torikesi_moto_denpyou_unique_no = dd.denpyou_unique_no ")
        sb.AppendLine("               ) ")
        sb.AppendLine("          ) ")
        sb.AppendLine("           tt ")
        sb.AppendLine("                INNER JOIN jhs_sys.t_tenbetu_syoki_seikyuu td ")
        sb.AppendLine("                  ON (tt.himoduke_table_type = 3 ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 1, 5) = td.mise_cd ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 9, LEN(tt.himoduke_cd)) = td.bunrui_cd) ")
        sb.AppendLine("      WHERE ")
        sb.AppendLine("           tt.seikyuu_date IS NOT NULL ")
        sb.AppendLine("       AND td.seikyuusyo_hak_date IS NOT NULL ")
        sb.AppendLine("       AND td.mise_cd = ts.mise_cd ")
        sb.AppendLine("       AND td.bunrui_cd = ts.bunrui_cd ")
        sb.AppendLine("       AND tt.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn & " ")
        sb.AppendLine("     ) ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 汎用売上データ取得用の共通WHEREクエリを取得
    ''' </summary>
    ''' <returns>汎用売上データ取得用の共通WHEREクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetHannyouUriageSqlWhere() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHannyouUriageSqlWhere")

        'SQL文の生成
        Dim sb As New StringBuilder()

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(" WHERE ")
        sb.AppendLine("      EXISTS ")
        sb.AppendLine("     (SELECT ")
        sb.AppendLine("           td.* ")
        sb.AppendLine("      FROM ")
        sb.AppendLine("          (SELECT ")
        sb.AppendLine("                dd.* ")
        sb.AppendLine("           FROM ")
        sb.AppendLine("                jhs_sys.v_seikyuusyo_mihakkou_uri_data dd ")
        sb.AppendLine("           WHERE ")
        sb.AppendLine("                --マイナス伝票の有るプラス伝票を除外 ")
        sb.AppendLine("                dd.denpyou_syubetu = 'UN' ")
        sb.AppendLine("            AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.v_seikyuusyo_mihakkou_uri_data dr ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     dr.denpyou_syubetu = 'UR' ")
        sb.AppendLine("                 AND dr.torikesi_moto_denpyou_unique_no = dd.denpyou_unique_no ")
        sb.AppendLine("               ) ")
        sb.AppendLine("          ) ")
        sb.AppendLine("           tt ")
        sb.AppendLine("                INNER JOIN jhs_sys.t_hannyou_uriage td ")
        sb.AppendLine("                  ON (tt.himoduke_table_type = 9 ")
        sb.AppendLine("                 AND tt.himoduke_cd = td.han_uri_unique_no) ")
        sb.AppendLine("      WHERE ")
        sb.AppendLine("           tt.seikyuu_date IS NOT NULL ")
        sb.AppendLine("       AND td.seikyuu_date IS NOT NULL ")
        sb.AppendLine("       AND td.han_uri_unique_no = hu.han_uri_unique_no ")
        sb.AppendLine("       AND tt.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn & " ")
        sb.AppendLine("     ) ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 売上データ取得用の共通WHEREクエリを取得(元テーブルが削除されたマイナス伝票を対象)
    ''' </summary>
    ''' <returns>売上データ取得用の共通WHEREクエリを取得(元テーブルが削除されたマイナス伝票を対象)</returns>
    ''' <remarks></remarks>
    Private Function GetUriageDataSqlWhere() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataSqlWhere")

        'SQL文の生成
        Dim sb As New StringBuilder()

        '****************
        '* WHERE項目
        '****************
        sb.AppendLine(" WHERE ")
        sb.AppendLine("      EXISTS ")
        sb.AppendLine("     (SELECT ")
        sb.AppendLine("           * ")
        sb.AppendLine("      FROM ")
        sb.AppendLine("           jhs_sys.v_seikyuusyo_mihakkou_uri_data tt ")
        sb.AppendLine("      WHERE ")
        sb.AppendLine("           tt.denpyou_unique_no = ud.denpyou_unique_no ")
        sb.AppendLine("       AND tt.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn & " ")
        sb.AppendLine("       AND tt.denpyou_syubetu = 'UR' ")
        sb.AppendLine("       AND tt.uri_keijyou_flg = 1 ")
        sb.AppendLine("       AND (( ")
        sb.AppendLine("           --邸別請求に存在しない売上データ ")
        sb.AppendLine("           tt.himoduke_table_type = 1 ")
        sb.AppendLine("       AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.t_teibetu_seikyuu td ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     substring(tt.himoduke_cd, 1, 1) = td.kbn ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 5, 10) = td.hosyousyo_no ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 18, 2) = SUBSTRING(td.bunrui_cd, 1, 2) ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 24, LEN(tt.himoduke_cd)) = CAST(td.gamen_hyouji_no AS VARCHAR) ")
        sb.AppendLine("               ) ")
        sb.AppendLine("          ) ")
        sb.AppendLine("        OR ( ")
        sb.AppendLine("           --店別請求に存在しない売上データ ")
        sb.AppendLine("           tt.himoduke_table_type = 2 ")
        sb.AppendLine("       AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.t_tenbetu_seikyuu td ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     substring(tt.himoduke_cd, 1, 5) = td.mise_cd ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 9, 3) = td.bunrui_cd ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 15, 8) = CONVERT(VARCHAR, td.nyuuryoku_date, 112) ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 26, LEN(tt.himoduke_cd)) = CAST(td.nyuuryoku_date_no AS VARCHAR) ")
        sb.AppendLine("               ) ")
        sb.AppendLine("          ) ")
        sb.AppendLine("        OR ( ")
        sb.AppendLine("           --店別初期請求に存在しない売上データ ")
        sb.AppendLine("           tt.himoduke_table_type = 3 ")
        sb.AppendLine("       AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.t_tenbetu_syoki_seikyuu td ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     substring(tt.himoduke_cd, 1, 5) = td.mise_cd ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 9, LEN(tt.himoduke_cd)) = td.bunrui_cd ")
        sb.AppendLine("               ) ")
        sb.AppendLine("          ) ")
        sb.AppendLine("        OR ( ")
        sb.AppendLine("           --汎用売上に存在しない売上データ ")
        sb.AppendLine("           tt.himoduke_table_type = 9 ")
        sb.AppendLine("       AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.t_hannyou_uriage td ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     tt.himoduke_cd = td.han_uri_unique_no ")
        sb.AppendLine("                    )")
        sb.AppendLine("                )")
        sb.AppendLine("            )")
        sb.AppendLine("    ) ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 売上データ取得用の共通WHEREクエリを取得(計上済でかつ黒/赤伝票で請求年月日が異なる請求書未作成のマイナス伝票を対象)
    ''' </summary>
    ''' <returns>売上データ取得用の共通WHEREクエリを取得(計上済でかつ黒/赤伝票で請求年月日が異なる請求書未作成のマイナス伝票を対象)</returns>
    ''' <remarks></remarks>
    Private Function GetUriageDataSeikyuuDateSqlWhere() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataSeikyuuDateSqlWhere")

        'SQL文の生成
        Dim sb As New StringBuilder()

        '****************
        '* WHERE項目
        '****************
        sb.Append(" WHERE ")
        sb.Append("      ud.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd & " ")
        sb.Append("  AND ud.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc & " ")
        sb.Append("  AND ud.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn & " ")
        sb.Append("  AND ud.uri_keijyou_flg = '1' ")
        sb.Append("  AND CONVERT(VARCHAR, ud.seikyuu_date, 111) <> CONVERT(VARCHAR, ud2.seikyuu_date, 111) ")
        sb.Append("  AND EXISTS ")
        sb.Append("     (SELECT ")
        sb.Append("           * ")
        sb.Append("      FROM ")
        sb.Append("           jhs_sys.v_seikyuusyo_mihakkou_uri_data tt ")
        sb.Append("      WHERE ")
        sb.Append("           tt.denpyou_unique_no = ud.denpyou_unique_no ")
        sb.Append("       AND tt.seikyuu_saki_cd = ud.seikyuu_saki_cd ")
        sb.Append("       AND tt.seikyuu_saki_brc = ud.seikyuu_saki_brc ")
        sb.Append("       AND tt.seikyuu_saki_kbn = ud.seikyuu_saki_kbn ")
        sb.Append("       AND tt.denpyou_syubetu = 'UR' ")
        sb.Append("     ) ")
        sb.Append("  ")

        Return sb.ToString
    End Function

#End Region

#Region "ORDER BY句"

    ''' <summary>
    ''' 邸別請求データ取得用の共通ORDER BYクエリを取得
    ''' </summary>
    ''' <returns>邸別請求データ取得用の共通ORDER BYクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTeibetuSeikyuuSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuSqlOrderBy")

        'SQL文の生成
        Dim sb As New StringBuilder

        sb.AppendLine(" ORDER BY ")
        sb.AppendLine("      ts.kbn ")
        sb.AppendLine("    , ts.hosyousyo_no ")
        sb.AppendLine("    , ts.bunrui_cd ")
        sb.AppendLine("    , ts.gamen_hyouji_no ")
        sb.AppendLine("")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 店別請求データ取得用の共通ORDER BYクエリを取得
    ''' </summary>
    ''' <returns>店別請求データ取得用の共通ORDER BYクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSeikyuuSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSeikyuuSqlOrderBy")

        'SQL文の生成
        Dim sb As New StringBuilder

        sb.AppendLine(" ORDER BY ")
        sb.AppendLine("      ts.bunrui_cd ")
        sb.AppendLine("    , ts.mise_cd ")
        sb.AppendLine("    , ts.nyuuryoku_date ")
        sb.AppendLine("    , ts.nyuuryoku_date_no ")
        sb.AppendLine("")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 店別初期請求データ取得用の共通ORDER BYクエリを取得
    ''' </summary>
    ''' <returns>店別初期請求データ取得用の共通ORDER BYクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSyokiSeikyuuSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSyokiSeikyuuSqlOrderBy")

        'SQL文の生成
        Dim sb As New StringBuilder

        sb.AppendLine(" ORDER BY ")
        sb.AppendLine("      ts.mise_cd ")
        sb.AppendLine("    , ts.bunrui_cd ")
        sb.AppendLine("")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 汎用売上データ取得用の共通ORDER BYクエリを取得
    ''' </summary>
    ''' <returns>汎用売上データ取得用の共通ORDER BYクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetHannyouUriageSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHannyouUriageSqlOrderBy")

        'SQL文の生成
        Dim sb As New StringBuilder

        sb.AppendLine(" ORDER BY ")
        sb.AppendLine("      hu.kbn ")
        sb.AppendLine("    , hu.bangou ")
        sb.AppendLine("    , hu.syouhin_cd ")
        sb.AppendLine("    , hu.seikyuu_date ")
        sb.AppendLine("    , hu.han_uri_unique_no ")
        sb.AppendLine("")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' 売上データ取得用の共通ORDER BYクエリを取得
    ''' </summary>
    ''' <returns>売上データ取得用の共通ORDER BYクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetUriageDataSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataSqlOrderBy")

        'SQL文の生成
        Dim sb As New StringBuilder

        sb.AppendLine(" ORDER BY ")
        sb.AppendLine("      ud.kbn ")
        sb.AppendLine("    , ud.bangou ")
        sb.AppendLine("    , ud.syouhin_cd ")
        sb.AppendLine("    , ud.seikyuu_date ")
        sb.AppendLine("    , ud.denpyou_unique_no ")
        sb.AppendLine("")

        Return sb.ToString
    End Function

#End Region

#End Region

End Class
