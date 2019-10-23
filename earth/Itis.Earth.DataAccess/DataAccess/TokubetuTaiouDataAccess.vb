Imports System
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 特別対応データの取得クラス
''' </summary>
''' <remarks></remarks>
Public Class TokubetuTaiouDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim SLogic As New StringLogic
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "特別対応データの取得"
    ''' <summary>
    ''' 特別対応データをリストで取得します
    ''' </summary>
    ''' <param name="strKbn">区分(主キー項目値)</param>
    ''' <param name="strHosyousyoNo">保証書NO(主キー項目値)</param>
    ''' <param name="strTokubetuTaiouCds">特別対応コード群(未指定で物件に紐付く特別対応データを全取得)</param>
    ''' <param name="blnToikesi">取消</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks>検索レコードクラスをKEYにして取得</remarks>
    Public Function GetTokubetuTaiouList(ByVal strKbn As String, _
                                         ByVal strHosyousyoNo As String, _
                                         ByVal strTokubetuTaiouCds As String, _
                                         ByVal blnToikesi As Boolean _
                                         ) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouList", _
                                                    strKbn, _
                                                    strHosyousyoNo, _
                                                    strTokubetuTaiouCds, _
                                                    blnToikesi _
                                                    )

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        'パラメータ
        Dim cmdParams() As SqlClient.SqlParameter = Me.GetTokubetuTaiouSqlCmnParams(strKbn, strHosyousyoNo)

        Dim intPrmCnt As Integer = 0
        Dim arrTokubetuTaiouCd() As String

        '特別対応コードの個別指定がある場合パラメータ追加
        If strTokubetuTaiouCds <> String.Empty Then
            '区切り文字で分割
            arrTokubetuTaiouCd = Split(strTokubetuTaiouCds, EarthConst.SEP_STRING)

            'パラメータの設定数を取得
            intPrmCnt = cmdParams.Length

            'パラメータ追加数分だけ配列を確保
            ReDim Preserve cmdParams(intPrmCnt + arrTokubetuTaiouCd.Length - 1)

            'パラメータに追加分を設定
            For intCnt As Integer = 0 To arrTokubetuTaiouCd.Length - 1
                cmdParams(intPrmCnt + intCnt) = SQLHelper.MakeParam(DBPrmTokubetuTaiouCds & (intCnt + 1).ToString, SqlDbType.Int, 4, arrTokubetuTaiouCd(intCnt))
            Next
        End If

        'SELECT句
        Dim strCmnSelect As String = Me.GetTokubetuTaiouSqlSelect()
        'TABLE句
        Dim strCmnTable As String = Me.GetCmnSqlTable()
        'WHERE句
        Dim strCmnWhere As String = Me.GetCmnSqlWhereList(strKbn, strHosyousyoNo, strTokubetuTaiouCds, blnToikesi)

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.Append(strCmnSelect)

        '****************
        '* TABLE項目
        '****************
        cmdTextSb.Append(strCmnTable)

        '****************
        '* WHERE項目
        '****************
        cmdTextSb.Append(strCmnWhere)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 特別対応データをPKで取得します
    ''' </summary>
    ''' <param name="strKbn">区分(主キー項目値)</param>
    ''' <param name="strHosyousyoNo">保証書NO(主キー項目値)</param>
    ''' <param name="intTokubetuTaiouCd">特別対応コード(主キー項目値)</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks>検索レコードクラスをKEYにして取得</remarks>
    Public Function GetTokubetuTaiouRec(ByVal strKbn As String, _
                                            ByVal strHosyousyoNo As String, _
                                            ByVal intTokubetuTaiouCd As Integer _
                                            ) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouRec", _
                                                    strKbn, _
                                                    strHosyousyoNo, _
                                                    intTokubetuTaiouCd _
                                                    )

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        'パラメータ
        Dim cmdParams() As SqlClient.SqlParameter = Me.GetTokubetuTaiouSqlCmnParams(strKbn, strHosyousyoNo, intTokubetuTaiouCd)

        'SELECT句
        Dim strCmnSelect As String = Me.GetTokubetuTaiouSqlSelect()
        'TABLE句
        Dim strCmnTable As String = Me.GetCmnSqlTable()
        'WHERE句
        Dim strCmnWhere As String = Me.GetCmnSqlWhere(strKbn, strHosyousyoNo, intTokubetuTaiouCd, False)

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.Append(strCmnSelect)

        '****************
        '* TABLE項目
        '****************
        cmdTextSb.Append(strCmnTable)

        '****************
        '* WHERE項目
        '****************
        cmdTextSb.Append(strCmnWhere)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function
#End Region

#Region "加盟店商品調査方法特別対応マスタの取得"
    ''' <summary>
    ''' 受注画面/加盟店商品調査方法特別対応マスタを取得します
    ''' </summary>
    ''' <param name="intAitesakiSyubetu">相手先種別</param>
    ''' <param name="strAitesakiCd">相手先コード</param>
    ''' <param name="strSyouhinCd">商品1コード</param>
    ''' <param name="intTysHouhouNo">調査方法NO</param>
    ''' <returns>加盟店商品調査方法特別対応マスタ</returns>
    ''' <remarks></remarks>
    Public Function GetKameiTokubetuTaiouInfo(ByVal intAitesakiSyubetu As Integer, _
                                              ByVal strAitesakiCd As String, _
                                              ByVal strSyouhinCd As String, _
                                              ByVal intTysHouhouNo As Integer _
                                              ) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameiTokubetuTaiouInfo", _
                                                    intAitesakiSyubetu, _
                                                    strAitesakiCd, _
                                                    strSyouhinCd, _
                                                    intTysHouhouNo _
                                                    )
        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        'パラメータ
        Dim cmdParams() As SqlParameter = Me.GetKameiTokubetuTaiouSqlCmnParams(intAitesakiSyubetu, strAitesakiCd, strSyouhinCd, intTysHouhouNo)

        'SELECT句
        Dim strCmnSelect As String = Me.GetKameiTokubetuTaiouSqlSelect()
        'TABLE句
        Dim strCmnTable As String = Me.GetCmnKameiSqlTable()
        'WHERE句
        Dim strCmnWhere As String = Me.GetCmnKameiSqlWhere()
        'ORDER BY句
        Dim strCmnOrderBy As String = Me.GetCmnKameiSqlOrderBy()

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.Append(strCmnSelect)

        '****************
        '* TABLE項目
        '****************
        cmdTextSb.Append(strCmnTable)

        '****************
        '* WHERE項目
        '****************
        cmdTextSb.Append(strCmnWhere)

        '*****************************
        ' ORDER BY項目（特別対応コード）
        '*****************************
        cmdTextSb.Append(strCmnOrderBy)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 加盟店商品調査方法特別対応マスタより対象となる特別対応レコードのみ取得する(新規受注用)
    ''' </summary>
    ''' <param name="strAitesakiCd">相手先コード</param>
    ''' <param name="strSyouhinCd">商品1コード</param>
    ''' <param name="intTysHouhouNo">調査方法NO</param>
    ''' <returns>加盟店商品調査方法特別対応マスタ</returns>
    ''' <remarks></remarks>
    Public Function GetDefalutKameiTokubetuTaiouInfoOnly( _
                                              ByVal strAitesakiCd As String, _
                                              ByVal strSyouhinCd As String, _
                                              ByVal intTysHouhouNo As Integer _
                                              ) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDefalutKameiTokubetuTaiouInfoOnly", _
                                                    strAitesakiCd, _
                                                    strSyouhinCd, _
                                                    intTysHouhouNo _
                                                    )
        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        'パラメータ
        Dim cmdParams() As SqlParameter = Me.GetKameiTokubetuTaiouSqlCmnParams(0, strAitesakiCd, strSyouhinCd, intTysHouhouNo)

        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("  * ")
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("  [jhs_sys].[fnGetTokubetuTaiouDefaultInfoDataTable](" & DBprmAitesakiCd & "," & DBprmSyouhinCd & "," & DBprmTysHouhouNo & ") ")

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

#End Region

#Region "共通クエリ"

#Region "SELECT句"
    ''' <summary>
    ''' 特別対応データ取得用のSELECTクエリを取得
    ''' </summary>
    ''' <returns>特別対応データ取得用の共通SELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTokubetuTaiouSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouSqlSelect")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.AppendLine(" SELECT ")
        '特別対応データ
        cmdTextSb.AppendLine("      ttt.kbn AS kbn ")
        cmdTextSb.AppendLine("    , ttt.hosyousyo_no AS hosyousyo_no ")
        cmdTextSb.AppendLine("    , ttt.tokubetu_taiou_cd AS tokubetu_taiou_cd ")
        cmdTextSb.AppendLine("    , ttt.torikesi AS torikesi ")
        cmdTextSb.AppendLine("    , ttt.bunrui_cd AS bunrui_cd ")
        cmdTextSb.AppendLine("    , ttt.gamen_hyouji_no AS gamen_hyouji_no ")
        cmdTextSb.AppendLine("    , ttt.kasan_syouhin_cd AS kasan_syouhin_cd ")
        cmdTextSb.AppendLine("    , ttt.uri_kasan_gaku AS uri_kasan_gaku ")
        cmdTextSb.AppendLine("    , ttt.koumuten_kasan_gaku AS koumuten_kasan_gaku ")
        cmdTextSb.AppendLine("    , mst.syouhin_mei AS syouhin_mei ")
        cmdTextSb.AppendLine("    , mst.souko_cd AS souko_cd ")
        'Old項目
        cmdTextSb.AppendLine("    , ttt.kasan_syouhin_cd_old AS kasan_syouhin_cd_old")
        cmdTextSb.AppendLine("    , ttt.uri_kasan_gaku_old AS uri_kasan_gaku_old ")
        cmdTextSb.AppendLine("    , ttt.koumuten_kasan_gaku_old AS koumuten_kasan_gaku_old ")

        cmdTextSb.AppendLine("    , ttt.kkk_syori_flg AS kkk_syori_flg ")
        cmdTextSb.AppendLine("    , ttt.upd_flg  AS upd_flg ")
        cmdTextSb.AppendLine("    , ISNULL(ttt.upd_login_user_id, ttt.add_login_user_id) AS upd_login_user_id ")
        cmdTextSb.AppendLine("    , ISNULL(ttt.upd_datetime, ttt.add_datetime) AS upd_datetime ")
        '特別対応マスタ
        cmdTextSb.AppendLine("    , mtt.tokubetu_taiou_cd AS m_tokubetu_taiou_cd ")
        cmdTextSb.AppendLine("    , mtt.torikesi AS m_torikesi ")
        cmdTextSb.AppendLine("    , mtt.tokubetu_taiou_meisyou AS m_tokubetu_taiou_meisyou ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 加盟店商品調査方法特別対応マスタ取得用のSELECTクエリを取得
    ''' </summary>
    ''' <returns>加盟店商品調査方法特別対応マスタ取得用の共通SELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetKameiTokubetuTaiouSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameiTokubetuTaiouSqlSelect")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.AppendLine(" SELECT ")
        '加盟店商品調査方法特別対応マスタ
        cmdTextSb.AppendLine("      mkt.aitesaki_syubetu ")
        cmdTextSb.AppendLine("    , mkt.aitesaki_cd ")
        cmdTextSb.AppendLine("    , mkt.syouhin_cd ")
        cmdTextSb.AppendLine("    , mkt.tys_houhou_no ")
        cmdTextSb.AppendLine("    , mkt.tokubetu_taiou_cd ")
        cmdTextSb.AppendLine("    , mkt.torikesi ")
        cmdTextSb.AppendLine("    , ISNULL(mkt.upd_login_user_id, mkt.add_login_user_id) AS upd_login_user_id ")
        cmdTextSb.AppendLine("    , ISNULL(mkt.upd_datetime, mkt.add_datetime) AS upd_datetime ")
        '特別対応マスタ
        cmdTextSb.AppendLine("    , mtt.tokubetu_taiou_cd AS m_tokubetu_taiou_cd ")
        cmdTextSb.AppendLine("    , mtt.torikesi AS m_torikesi ")
        cmdTextSb.AppendLine("    , mtt.tokubetu_taiou_meisyou ")

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "TABLE句"
    ''' <summary>
    ''' 特別対応データ取得用のTABLEクエリを取得
    ''' </summary>
    ''' <returns>特別対応データ取得用の共通TABLEクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlTable")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* TABLE項目
        '****************
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      m_tokubetu_taiou mtt ")
        cmdTextSb.AppendLine("           LEFT OUTER JOIN t_tokubetu_taiou ttt ")
        cmdTextSb.AppendLine("             ON mtt.tokubetu_taiou_cd = ttt.tokubetu_taiou_cd ")
        '商品M
        cmdTextSb.AppendLine("           LEFT OUTER JOIN m_syouhin mst ")
        cmdTextSb.AppendLine("            ON mst.syouhin_cd = ttt.kasan_syouhin_cd ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 加盟店商品調査方法特別対応マスタ取得用のTABLEクエリを取得
    ''' </summary>
    ''' <returns>加盟店商品調査方法特別対応マスタ取得用の共通TABLEクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnKameiSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnKameiSqlTable")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* TABLE項目
        '****************
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      m_tokubetu_taiou mtt ")
        cmdTextSb.AppendLine("           LEFT OUTER JOIN m_kamei_syouhin_tys_tokubetu_taiou mkt ")
        cmdTextSb.AppendLine("             ON mkt.tokubetu_taiou_cd = mtt.tokubetu_taiou_cd ")

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "WHERE句"
    ''' <summary>
    ''' 特別対応データ取得用の共通WHEREクエリを取得
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="intTokubetuTaiouCd">特別対応コード</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>特別対応データ取得用の共通WHEREクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlWhere(ByVal strKbn As String, _
                                    ByVal strHosyousyoNo As String, _
                                    ByVal intTokubetuTaiouCd As Integer, _
                                    ByVal blnTorikesi As Boolean _
                                    ) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlWhere", _
                                                    strKbn, _
                                                    strHosyousyoNo, _
                                                    intTokubetuTaiouCd, _
                                                    blnTorikesi _
                                                    )
        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.AppendLine(" WHERE 1 = 1 ")

        '***********************************************************************
        ' 区分
        '***********************************************************************
        If Not String.IsNullOrEmpty(strKbn) Then
            cmdTextSb.AppendLine("  AND ttt.kbn = " & DBprmKbn)
        End If

        '***********************************************************************
        ' 保証書NO
        '***********************************************************************
        If Not String.IsNullOrEmpty(strHosyousyoNo) Then
            cmdTextSb.AppendLine("  AND ttt.hosyousyo_no = " & DBprmHosyousyoNo)
        End If

        '***********************************************************************
        ' 特別対応コード
        '***********************************************************************
        If intTokubetuTaiouCd <> Integer.MinValue Then
            cmdTextSb.AppendLine("  AND ttt.tokubetu_taiou_cd = " & DBprmTokubetuTaiouCd)
        End If

        '***********************************************************************
        ' 取消
        '***********************************************************************
        If blnTorikesi Then
            cmdTextSb.AppendLine("  AND ttt.torikesi = 0")
        End If

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 特別対応データ取得用の共通WHEREクエリを取得
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="strTokubetuTaiouCds">特別対応コード群</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>特別対応データ取得用の共通WHEREクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlWhereList(ByVal strKbn As String, _
                                    ByVal strHosyousyoNo As String, _
                                    ByVal strTokubetuTaiouCds As String, _
                                    ByVal blnTorikesi As Boolean _
                                    ) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlWhereList", _
                                                    strKbn, _
                                                    strHosyousyoNo, _
                                                    strTokubetuTaiouCds, _
                                                    blnTorikesi _
                                                    )
        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.AppendLine(" WHERE 1 = 1 ")

        '***********************************************************************
        ' 区分
        '***********************************************************************
        If Not String.IsNullOrEmpty(strKbn) Then
            cmdTextSb.AppendLine("  AND ttt.kbn = " & DBprmKbn)
        End If

        '***********************************************************************
        ' 保証書NO
        '***********************************************************************
        If Not String.IsNullOrEmpty(strHosyousyoNo) Then
            cmdTextSb.AppendLine("  AND ttt.hosyousyo_no = " & DBprmHosyousyoNo)
        End If

        '***********************************************************************
        ' 特別対応コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(strTokubetuTaiouCds) Then
            Dim arrTokubetuTaiou() As String = Split(strTokubetuTaiouCds, EarthConst.SEP_STRING)
            If arrTokubetuTaiou.Length > 0 Then
                cmdTextSb.AppendLine("  AND ttt.tokubetu_taiou_cd IN(")
                For intCnt As Integer = 0 To arrTokubetuTaiou.Length - 1
                    If intCnt > 0 Then
                        cmdTextSb.AppendLine(",")
                    End If
                    cmdTextSb.AppendLine(DBPrmTokubetuTaiouCds & (intCnt + 1).ToString)
                Next
                cmdTextSb.AppendLine("  )")
            End If
        End If

        '***********************************************************************
        ' 取消
        '***********************************************************************
        If blnTorikesi Then
            cmdTextSb.AppendLine("  AND ttt.torikesi = 0")
        End If

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 加盟店商品調査方法特別対応マスタ取得用の共通WHEREクエリを取得
    ''' </summary>
    ''' <returns>加盟店商品調査方法特別対応マスタ取得用の共通WHEREクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnKameiSqlWhere() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnKameiSqlWhere")
        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.AppendLine(" WHERE 1 = 1 ")
        cmdTextSb.AppendLine("  AND mkt.aitesaki_syubetu = " & DBprmAitesakiSyubetu)
        cmdTextSb.AppendLine("  AND mkt.aitesaki_cd = " & DBprmAitesakiCd)
        cmdTextSb.AppendLine("  AND mkt.syouhin_cd = " & DBprmSyouhinCd)
        cmdTextSb.AppendLine("  AND mkt.tys_houhou_no = " & DBprmTysHouhouNo)
        cmdTextSb.AppendLine("  AND mkt.torikesi = 0 ")
        cmdTextSb.AppendLine("  AND mkt.syokiti = 1 ")
        cmdTextSb.AppendLine("  AND mtt.torikesi = 0 ")

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "ORDER BY句"
    ''' <summary>
    ''' 特別対応データ取得用のORDER BYクエリを取得
    ''' </summary>
    ''' <returns>特別対応データ取得用の共通ORDER BYクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlOrderBy")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        ' ORDER BY項目
        '****************
        cmdTextSb.AppendLine(" ORDER BY ")
        cmdTextSb.AppendLine("      ttt.tokubetu_taiou_cd ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 加盟店商品調査方法特別対応マスタ取得用のORDER BYクエリを取得
    ''' </summary>
    ''' <returns>請求データ取得用の共通ORDER BYクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnKameiSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnKameiSqlOrderBy")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        ' ORDER BY項目
        '****************
        cmdTextSb.AppendLine(" ORDER BY ")
        cmdTextSb.AppendLine("      mkt.aitesaki_syubetu ")
        cmdTextSb.AppendLine("    , mkt.aitesaki_cd ")
        cmdTextSb.AppendLine("    , mkt.syouhin_cd ")
        cmdTextSb.AppendLine("    , mkt.tys_houhou_no ")
        cmdTextSb.AppendLine("    , mkt.tokubetu_taiou_cd ")

        Return cmdTextSb.ToString
    End Function
#End Region

#End Region

#Region "SQLパラメータ"

#Region "パラメータ定義"
    '区分
    Private Const DBprmKbn As String = "@KBN"
    '保証書NO
    Private Const DBprmHosyousyoNo As String = "@HOSYOUSYO_NO"
    '特別対応コード
    Private Const DBprmTokubetuTaiouCd As String = "@TOKUBETU_TAIOU_CODE"
    '相手先種別
    Private Const DBprmAitesakiSyubetu As String = "@AITESAKI_SYUBETU"
    '相手先コード
    Private Const DBprmAitesakiCd As String = "@AITESAKI_CODE"
    '商品コード
    Private Const DBprmSyouhinCd As String = "@SYOUHIN_CODE"
    '調査方法NO
    Private Const DBprmTysHouhouNo As String = "@TYOUSA_HOUHOU_NO"
    '特別対応コード群
    Private Const DBPrmTokubetuTaiouCds As String = "@TOKUBETU_TAIOU_CODES_"
#End Region

    ''' <summary>
    ''' 特別対応データ取得用の共通SQLパラメータを取得
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="intTokubetuTaiouCd">特別対応コード</param>
    ''' <returns>特別対応データ取得用の共通SQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetTokubetuTaiouSqlCmnParams(ByVal strKbn As String, _
                                                  ByVal strHosyousyoNo As String, _
                                                  Optional ByVal intTokubetuTaiouCd As Integer = Integer.MinValue _
                                                  ) As SqlParameter()
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouSqlCmnParams", _
                                                    strKbn, _
                                                    strHosyousyoNo, _
                                                    intTokubetuTaiouCd _
                                                    )
        '区分
        Dim objKbn As Object = IIf(strKbn = String.Empty, DBNull.Value, strKbn)
        '保証書NO
        Dim objHosyousyoNo As Object = IIf(strHosyousyoNo = String.Empty, DBNull.Value, strHosyousyoNo)
        '特別対応コード
        Dim objTokubetuTaiouCd As Object = IIf(intTokubetuTaiouCd = Integer.MinValue, DBNull.Value, intTokubetuTaiouCd)

        'パラメータへ設定
        Dim cmdParams() As SqlParameter = { _
                 SQLHelper.MakeParam(DBprmKbn, SqlDbType.Char, 1, objKbn), _
                 SQLHelper.MakeParam(DBprmHosyousyoNo, SqlDbType.VarChar, 10, objHosyousyoNo), _
                 SQLHelper.MakeParam(DBprmTokubetuTaiouCd, SqlDbType.Int, 4, objTokubetuTaiouCd) _
        }

        Return cmdParams
    End Function

    ''' <summary>
    ''' 加盟店商品調査方法特別対応マスタ取得用の共通SQLパラメータを取得
    ''' </summary>
    ''' <param name="intAitesakiSyubetu">相手先種別</param>
    ''' <param name="strAitesakiCd">相手先コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="intTysHouhouNo">調査方法NO</param>
    ''' <returns>加盟店商品調査方法特別対応マスタ取得用の共通SQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetKameiTokubetuTaiouSqlCmnParams(ByVal intAitesakiSyubetu As Integer, _
                                                       ByVal strAitesakiCd As String, _
                                                       ByVal strSyouhinCd As String, _
                                                       ByVal intTysHouhouNo As Integer _
                                                       ) As SqlParameter()
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameiTokubetuTaiouSqlCmnParams", _
                                                    intAitesakiSyubetu, _
                                                    strAitesakiCd, _
                                                    strSyouhinCd, _
                                                    intTysHouhouNo _
                                                    )
        '相手先種別
        Dim objAitesakiSyubetu As Object = IIf(intAitesakiSyubetu = Integer.MinValue, DBNull.Value, intAitesakiSyubetu)
        '相手先コード
        Dim objAitesakiCd As Object = IIf(strAitesakiCd = String.Empty, DBNull.Value, strAitesakiCd)
        '商品コード
        Dim objSyouhinCd As Object = IIf(strSyouhinCd = String.Empty, DBNull.Value, strSyouhinCd)
        '調査方法NO
        Dim objTysHouhouNo As Object = IIf(intTysHouhouNo = Integer.MinValue, DBNull.Value, intTysHouhouNo)

        'パラメータへ設定
        Dim cmdParams() As SqlParameter = { _
                 SQLHelper.MakeParam(DBprmAitesakiSyubetu, SqlDbType.Int, 4, objAitesakiSyubetu), _
                 SQLHelper.MakeParam(DBprmAitesakiCd, SqlDbType.VarChar, 5, objAitesakiCd), _
                 SQLHelper.MakeParam(DBprmSyouhinCd, SqlDbType.VarChar, 8, objSyouhinCd), _
                 SQLHelper.MakeParam(DBprmTysHouhouNo, SqlDbType.Int, 4, objTysHouhouNo) _
        }

        Return cmdParams
    End Function

#End Region

End Class
