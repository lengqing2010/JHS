Imports System
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 特別対応マスタの取得クラス
''' </summary>
''' <remarks></remarks>
Public Class TokubetuTaiouMstDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "特別対応マスタの取得"
    ''' <summary>
    ''' 特別対応画面/特別対応マスタを一覧取得します
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="intTysHouhouNo">調査方法NO</param>
    ''' <returns>特別対応マスタ</returns>
    ''' <remarks></remarks>
    Public Function GetTokubetuTaiouMstInfo(ByVal strKbn As String, _
                                            ByVal strHosyousyoNo As String, _
                                            ByVal strKameitenCd As String, _
                                            ByVal strEigyousyoCd As String, _
                                            ByVal strKeiretuCd As String, _
                                            ByVal strSyouhinCd As String, _
                                            ByVal intTysHouhouNo As Integer _
                                            ) As DataTable

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouMstInfo", _
                                                    strKbn, _
                                                    strHosyousyoNo, _
                                                    strKameitenCd, _
                                                    strEigyousyoCd, _
                                                    strKeiretuCd, _
                                                    strSyouhinCd, _
                                                    intTysHouhouNo)
        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        'パラメータ
        Dim cmdParams() As SqlParameter = Me.GetTokubetuTaiouMstSqlCmnParams(strKbn, _
                                                                               strHosyousyoNo, _
                                                                               strKameitenCd, _
                                                                               strEigyousyoCd, _
                                                                               strKeiretuCd, _
                                                                               strSyouhinCd, _
                                                                               intTysHouhouNo, _
                                                                               Integer.MinValue)
        'SELECT句
        Dim strCmnSelect As String = Me.GetCmnSqlSelect()
        'TABLE句
        Dim strCmnTable As String = Me.GetCmnSqlTable()
        'ORDER BY句
        Dim strCmnOrderBy As String = Me.GetCmnSqlOrderBy()

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
        'なし

        '*****************************
        ' ORDER BY項目（特別対応コード）
        '*****************************
        cmdTextSb.Append(strCmnOrderBy)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 特別対応画面/特別対応マスタをPKで取得します
    ''' </summary>
    ''' <param name="intTokubetuTaiouCd">特別対応コード(主キー項目値)</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks>検索レコードクラスをKEYにして取得</remarks>
    Public Function GetTokubetuTaiouMstRec( _
                                             ByVal intTokubetuTaiouCd As Integer _
                                            ) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouRec", _
                                                    intTokubetuTaiouCd _
                                                    )

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        'パラメータ
        Dim cmdParams() As SqlClient.SqlParameter = Me.GetTokubetuTaiouMstSqlCmnParams("", "", "", "", "", "", Integer.MinValue, intTokubetuTaiouCd)

        'SELECT句
        Dim strCmnSelect As String = Me.GetCmnSqlSelect()
        'TABLE句
        Dim strCmnTable As String = Me.GetCmnSqlTable()
        'WHERE句
        Dim strCmnWhere As String = Me.GetCmnSqlWhere(intTokubetuTaiouCd)

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
    ''' 特別対応ツールチップ用/特別対応マスタをPKで取得します
    ''' </summary>
    ''' <param name="listMtt">特別対応マスタのリスト</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTokubetuTaiouToolTip( _
                                             ByVal listMtt As List(Of TokubetuTaiouMstRecord) _
                                             ) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouToolTip", _
                                                    listMtt _
                                                    )
        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        'パラメータ
        Dim cmdParams() As SqlClient.SqlParameter = Me.GetTokubetuTaiouMstSqlCmnParams(listMtt)

        'SELECT句
        Dim strCmnSelect As String = Me.GetTokubetuTaiouMstSqlSelect()
        'TABLE句
        Dim strCmnTable As String = Me.GetTokubetuTaiouMstSqlTable()
        'WHERE句
        Dim strCmnWhere As String = Me.GetTokubetuTaiouMstSqlWhere(listMtt)
        'ORDER BY句
        Dim strCmnOrderBy As String = Me.GetCmnSqlOrderBy()

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

#End Region

#Region "共通クエリ"

#Region "SELECT句"
    ''' <summary>
    ''' 特別対応マスタ取得用のSELECTクエリを取得
    ''' </summary>
    ''' <returns>特別対応マスタ取得用の共通SELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlSelect")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.AppendLine(" SELECT ")

        '特別対応マスタ
        cmdTextSb.AppendLine("      mtt.tokubetu_taiou_cd AS m_tokubetu_taiou_cd ")
        cmdTextSb.AppendLine("    , mtt.torikesi AS m_torikesi ")
        cmdTextSb.AppendLine("    , mtt.tokubetu_taiou_meisyou AS m_tokubetu_taiou_meisyou ")
        '特別対応データ
        cmdTextSb.AppendLine("    , ttt.kbn AS kbn ")
        cmdTextSb.AppendLine("    , ttt.hosyousyo_no AS hosyousyo_no ")
        cmdTextSb.AppendLine("    , ttt.tokubetu_taiou_cd AS tokubetu_taiou_cd ")
        cmdTextSb.AppendLine("    , ttt.torikesi AS torikesi ")
        cmdTextSb.AppendLine("    , ttt.bunrui_cd AS bunrui_cd ")
        cmdTextSb.AppendLine("    , ttt.gamen_hyouji_no AS gamen_hyouji_no ")
        cmdTextSb.AppendLine("    , ttt.kasan_syouhin_cd AS kasan_syouhin_cd ")
        cmdTextSb.AppendLine("    , mst.syouhin_mei AS syouhin_mei ")
        cmdTextSb.AppendLine("    , mst.souko_cd AS souko_cd ")
        cmdTextSb.AppendLine("    , ttt.uri_kasan_gaku AS uri_kasan_gaku ")
        cmdTextSb.AppendLine("    , ttt.koumuten_kasan_gaku AS koumuten_kasan_gaku ")
        'Old項目
        cmdTextSb.AppendLine("    , ttt.kasan_syouhin_cd_old AS kasan_syouhin_cd_old ")
        cmdTextSb.AppendLine("    , mstold.syouhin_mei AS syouhin_mei_old ")
        cmdTextSb.AppendLine("    , mstold.souko_cd AS souko_cd_old ")
        cmdTextSb.AppendLine("    , ttt.uri_kasan_gaku_old AS uri_kasan_gaku_old ")
        cmdTextSb.AppendLine("    , ttt.koumuten_kasan_gaku_old AS koumuten_kasan_gaku_old ")

        cmdTextSb.AppendLine("    , ttt.kkk_syori_flg AS kkk_syori_flg ")
        cmdTextSb.AppendLine("    , ttt.upd_flg AS upd_flg ")
        cmdTextSb.AppendLine("    , ttt.add_login_user_id AS add_login_user_id ")
        cmdTextSb.AppendLine("    , ttt.add_datetime AS add_datetime ")
        cmdTextSb.AppendLine("    , ttt.upd_login_user_id AS upd_login_user_id ")
        cmdTextSb.AppendLine("    , ISNULL(ttt.upd_datetime, ttt.add_datetime) AS upd_datetime ")
        '加盟店特別対応マスタ
        cmdTextSb.AppendLine("    , ISNULL(sub2.syokiti, 0) AS k_syokiti ")
        cmdTextSb.AppendLine("    , ")
        cmdTextSb.AppendLine("      CASE ")
        cmdTextSb.AppendLine("           WHEN ISNULL(sub2.syokiti, 0) = 1 ")
        cmdTextSb.AppendLine("           THEN sub2.aitesaki_syubetu ")
        cmdTextSb.AppendLine("           ELSE mkst.aitesaki_syubetu ")
        cmdTextSb.AppendLine("      END AS k_aitesaki_syubetu ")
        cmdTextSb.AppendLine("    , ")
        cmdTextSb.AppendLine("      CASE ")
        cmdTextSb.AppendLine("           WHEN ISNULL(sub2.syokiti, 0) = 1 ")
        cmdTextSb.AppendLine("           THEN sub2.aitesaki_cd ")
        cmdTextSb.AppendLine("           ELSE mkst.aitesaki_cd ")
        cmdTextSb.AppendLine("      END AS k_aitesaki_cd ")
        cmdTextSb.AppendLine("    , ")
        cmdTextSb.AppendLine("      CASE ")
        cmdTextSb.AppendLine("           WHEN ISNULL(sub2.syokiti, 0) = 1 ")
        cmdTextSb.AppendLine("           THEN sub2.syouhin_cd ")
        cmdTextSb.AppendLine("           ELSE mkst.syouhin_cd ")
        cmdTextSb.AppendLine("      END AS k_syouhin_cd ")
        cmdTextSb.AppendLine("    , ")
        cmdTextSb.AppendLine("      CASE ")
        cmdTextSb.AppendLine("           WHEN ISNULL(sub2.syokiti, 0) = 1 ")
        cmdTextSb.AppendLine("           THEN sub2.tys_houhou_no ")
        cmdTextSb.AppendLine("           ELSE mkst.tys_houhou_no ")
        cmdTextSb.AppendLine("      END AS k_tys_houhou_no ")
        cmdTextSb.AppendLine("    , ")
        cmdTextSb.AppendLine("      CASE ")
        cmdTextSb.AppendLine("           WHEN ISNULL(sub2.syokiti, 0) = 1 ")
        cmdTextSb.AppendLine("           THEN sub2.tokubetu_taiou_cd ")
        cmdTextSb.AppendLine("           ELSE mkst.tokubetu_taiou_cd ")
        cmdTextSb.AppendLine("      END AS k_tokubetu_taiou_cd ")
        cmdTextSb.AppendLine("    , ")
        cmdTextSb.AppendLine("      CASE ")
        cmdTextSb.AppendLine("           WHEN ISNULL(sub2.syokiti, 0) = 1 ")
        cmdTextSb.AppendLine("           THEN sub2.kasan_syouhin_cd ")
        cmdTextSb.AppendLine("           ELSE mkst.kasan_syouhin_cd ")
        cmdTextSb.AppendLine("      END AS k_kasan_syouhin_cd ")
        cmdTextSb.AppendLine("    , ")
        cmdTextSb.AppendLine("      CASE ")
        cmdTextSb.AppendLine("           WHEN ISNULL(sub2.syokiti, 0) = 1 ")
        cmdTextSb.AppendLine("           THEN sub2.uri_kasan_gaku ")
        cmdTextSb.AppendLine("           ELSE mkst.uri_kasan_gaku ")
        cmdTextSb.AppendLine("      END AS k_uri_kasan_gaku ")
        cmdTextSb.AppendLine("    , ")
        cmdTextSb.AppendLine("      CASE ")
        cmdTextSb.AppendLine("           WHEN ISNULL(sub2.syokiti, 0) = 1 ")
        cmdTextSb.AppendLine("           THEN sub2. koumuten_kasan_gaku ")
        cmdTextSb.AppendLine("           ELSE mkst.koumuten_kasan_gaku ")
        cmdTextSb.AppendLine("      END AS k_koumuten_kasan_gaku ")
        cmdTextSb.AppendLine("    , ")
        cmdTextSb.AppendLine("      CASE ")
        cmdTextSb.AppendLine("           WHEN ISNULL(sub2.syokiti, 0) = 1 ")
        cmdTextSb.AppendLine("           THEN mstdef.syouhin_mei ")
        cmdTextSb.AppendLine("           ELSE msk.syouhin_mei ")
        cmdTextSb.AppendLine("      END AS k_syouhin_mei ")
        cmdTextSb.AppendLine("    , ")
        cmdTextSb.AppendLine("      CASE ")
        cmdTextSb.AppendLine("           WHEN ISNULL(sub2.syokiti, 0) = 1 ")
        cmdTextSb.AppendLine("           THEN mstdef.souko_cd ")
        cmdTextSb.AppendLine("           ELSE msk.souko_cd ")
        cmdTextSb.AppendLine("      END AS k_souko_cd ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 特別対応マスタ取得用のSELECTクエリを取得
    ''' </summary>
    ''' <returns>特別対応マスタ取得用のSELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTokubetuTaiouMstSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouMstSqlSelect")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("      mtt.tokubetu_taiou_cd ")
        cmdTextSb.AppendLine("    , mtt.torikesi ")
        cmdTextSb.AppendLine("    , mtt.tokubetu_taiou_meisyou ")
        cmdTextSb.AppendLine("    , mtt.add_login_user_id ")
        cmdTextSb.AppendLine("    , mtt.add_datetime ")
        cmdTextSb.AppendLine("    , mtt.upd_login_user_id ")
        cmdTextSb.AppendLine("    , mtt.upd_datetime ")

        Return cmdTextSb.ToString
    End Function

#End Region

#Region "TABLE句"
    ''' <summary>
    ''' 特別対応画面初期読込用のTABLEクエリを取得
    ''' </summary>
    ''' <returns>特別対応マスタ取得用の共通TABLEクエリを取得</returns>
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
        cmdTextSb.AppendLine("           LEFT OUTER JOIN t_tokubetu_taiou ttt --トラン(New,Old) ")
        cmdTextSb.AppendLine("             ON mtt.tokubetu_taiou_cd = ttt.tokubetu_taiou_cd ")
        cmdTextSb.AppendLine("            AND ttt.kbn = " & DBprmKbn)
        cmdTextSb.AppendLine("            AND ttt.hosyousyo_no = " & DBprmHosyousyoNo)
        '特別対応・KEY情報取得用サブクエリ
        cmdTextSb.AppendLine("           LEFT OUTER JOIN [jhs_sys].[fnGetTokubetuTaiouKeyDataTable](" & DBprmKameitenCd & "," & DBprmSyouhinCd & "," & DBprmTysHouhouNo & ") sub ")
        cmdTextSb.AppendLine("             ON mtt.tokubetu_taiou_cd = sub.tokubetu_taiou_cd ")
        '特別対応・詳細情報取得用サブクエリ
        cmdTextSb.AppendLine("           LEFT OUTER JOIN m_kamei_syouhin_tys_tokubetu_taiou mkst ")
        cmdTextSb.AppendLine("             ON mkst.aitesaki_syubetu = sub.aitesaki_syubetu ")
        cmdTextSb.AppendLine("            AND mkst.aitesaki_cd = ")
        cmdTextSb.AppendLine("                CASE ")
        cmdTextSb.AppendLine("                     WHEN sub.aitesaki_syubetu = 1 ")
        cmdTextSb.AppendLine("                     THEN " & DBprmKameitenCd)
        cmdTextSb.AppendLine("                     WHEN sub.aitesaki_syubetu = 5 ")
        cmdTextSb.AppendLine("                     THEN " & DBprmEigyousyoCd)
        cmdTextSb.AppendLine("                     WHEN sub.aitesaki_syubetu = 7 ")
        cmdTextSb.AppendLine("                     THEN " & DBprmKeiretuCd)
        cmdTextSb.AppendLine("                     WHEN sub.aitesaki_syubetu = 0 ")
        cmdTextSb.AppendLine("                     THEN 'ALL' ")
        cmdTextSb.AppendLine("                     ELSE '' ")
        cmdTextSb.AppendLine("                END ")
        cmdTextSb.AppendLine("            AND mkst.syouhin_cd = " & DBprmSyouhinCd)
        cmdTextSb.AppendLine("            AND mkst.tys_houhou_no = " & DBprmTysHouhouNo)
        cmdTextSb.AppendLine("            AND mkst.tokubetu_taiou_cd = sub.tokubetu_taiou_cd ")
        '特別対応・デフォルト登録(マスタ再取得)用サブクエリ
        cmdTextSb.AppendLine("           LEFT OUTER JOIN [jhs_sys].[fnGetTokubetuTaiouDefaultInfoDataTable](" & DBprmKameitenCd & "," & DBprmSyouhinCd & "," & DBprmTysHouhouNo & ") sub2 ")
        cmdTextSb.AppendLine("              ON mtt.tokubetu_taiou_cd = sub2.tokubetu_taiou_cd ")
        '特別対応データT
        cmdTextSb.AppendLine("           LEFT OUTER JOIN m_syouhin mst --トラン(New) ")
        cmdTextSb.AppendLine("             ON mst.syouhin_cd = ttt.kasan_syouhin_cd ")
        cmdTextSb.AppendLine("           LEFT OUTER JOIN m_syouhin mstold --トラン(Old) ")
        cmdTextSb.AppendLine("             ON mstold.syouhin_cd = ttt.kasan_syouhin_cd_old ")
        '加盟店特別対応M
        cmdTextSb.AppendLine("           LEFT OUTER JOIN m_syouhin msk --マスタ(初期値<>1) ")
        cmdTextSb.AppendLine("             ON msk.syouhin_cd = mkst.kasan_syouhin_cd ")
        cmdTextSb.AppendLine("           LEFT OUTER JOIN m_syouhin mstdef --マスタ(初期値=1) ")
        cmdTextSb.AppendLine("             ON mstdef.syouhin_cd = sub2.kasan_syouhin_cd ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' ツールチップ表示用のTABLEクエリを取得
    ''' </summary>
    ''' <returns>特別対応マスタ取得用のTABLEクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTokubetuTaiouMstSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouMstSqlTable")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* TABLE項目
        '****************
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      m_tokubetu_taiou mtt ")

        Return cmdTextSb.ToString
    End Function

#End Region

#Region "WHERE句"
    ''' <summary>
    ''' 特別対応マスタ取得用の共通WHEREクエリを取得
    ''' </summary>
    ''' <returns>特別対応マスタ取得用の共通WHEREクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlWhere( _
                                     ByVal intTokubetuTaiouCd As Integer _
                                    ) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlWhere", _
                                                    intTokubetuTaiouCd _
                                                    )
        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.AppendLine(" WHERE 1 = 1 ")

        '***********************************************************************
        ' 特別対応コード
        '***********************************************************************
        If Not (intTokubetuTaiouCd = Integer.MinValue) Then
            cmdTextSb.AppendLine("  AND mtt.tokubetu_taiou_cd = " & DBprmTokubetuTaiouCd)
        End If

        '***********************************************************************
        ' 取消
        '***********************************************************************
        cmdTextSb.AppendLine("  AND mtt.torikesi = 0 ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 特別対応マスタ取得用の共通WHEREクエリを取得
    ''' </summary>
    ''' <param name="listMtt">特別対応マスタのリスト</param>
    ''' <returns>特別対応マスタ取得用の共通WHEREクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetTokubetuTaiouMstSqlWhere( _
                                                  ByVal listMtt As List(Of TokubetuTaiouMstRecord) _
                                                ) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouMstSqlWhere", _
                                                    listMtt _
                                                    )
        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.AppendLine(" WHERE 1 = 1 ")

        '************************************
        ' 対象の特別対応コードのみ対象とする
        '************************************
        If Not listMtt Is Nothing AndAlso listMtt.Count <> 0 Then
            cmdTextSb.AppendLine(" AND ( ")

            For i As Integer = 0 To listMtt.Count - 1
                If i > 0 Then
                    cmdTextSb.AppendLine("      OR ")
                End If
                cmdTextSb.AppendLine("      mtt.tokubetu_taiou_cd = " & DBprmTokubetuTaiouCd & "_" & CStr(i))
            Next
            cmdTextSb.AppendLine(" ) ")
        End If

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "ORDER BY句"
    ''' <summary>
    ''' 請求データ取得用のORDER BYクエリを取得
    ''' </summary>
    ''' <returns>請求データ取得用の共通ORDER BYクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlOrderBy")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        ' ORDER BY項目
        '****************
        cmdTextSb.AppendLine(" ORDER BY ")
        cmdTextSb.AppendLine("      mtt.tokubetu_taiou_cd ")

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
    '加盟店コード
    Private Const DBprmKameitenCd As String = "@KAMEITEN_CODE"
    '営業所コード
    Private Const DBprmEigyousyoCd As String = "@EIGYOUSYO_CODE"
    '系列コード
    Private Const DBprmKeiretuCd As String = "@KEIRETU_CODE"
    '商品コード
    Private Const DBprmSyouhinCd As String = "@SYOUHIN_CODE"
    '調査方法NO
    Private Const DBprmTysHouhouNo As String = "@TYOUSA_HOUHOU_NO"
    '特別対応コード
    Private Const DBprmTokubetuTaiouCd As String = "@TOKUBETU_TAIOU_CODE"
#End Region

    ''' <summary>
    ''' 特別対応画面/特別対応マスタ取得用の共通SQLパラメータを取得
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="intTysHouhouNo">調査方法NO</param>
    ''' <param name="intTokubetuTaiouCd">特別対応コード</param>
    ''' <returns>加盟店商品調査方法特別対応マスタ取得用の共通SQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetTokubetuTaiouMstSqlCmnParams(ByVal strKbn As String, _
                                                     ByVal strHosyousyoNo As String, _
                                                     ByVal strKameitenCd As String, _
                                                     ByVal strEigyousyoCd As String, _
                                                     ByVal strKeiretuCd As String, _
                                                     ByVal strSyouhinCd As String, _
                                                     ByVal intTysHouhouNo As Integer, _
                                                     ByVal intTokubetuTaiouCd As Integer _
                                                     ) As SqlParameter()
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouMstSqlCmnParams", _
                                                    strKbn, _
                                                    strHosyousyoNo, _
                                                    strKameitenCd, _
                                                    strEigyousyoCd, _
                                                    strKeiretuCd, _
                                                    strSyouhinCd, _
                                                    intTysHouhouNo, _
                                                    intTokubetuTaiouCd)

        '区分
        Dim objKbn As Object = IIf(strKbn = String.Empty, DBNull.Value, strKbn)
        '保証書NO
        Dim objHosyousyoNo As Object = IIf(strHosyousyoNo = String.Empty, DBNull.Value, strHosyousyoNo)
        '加盟店コード
        Dim objKameitenCd As Object = IIf(strKameitenCd = String.Empty, DBNull.Value, strKameitenCd)
        '営業所コード
        Dim objEigyousyoCd As Object = IIf(strEigyousyoCd = String.Empty, DBNull.Value, strEigyousyoCd)
        '系列コード
        Dim objKeiretuCd As Object = IIf(strKeiretuCd = String.Empty, DBNull.Value, strKeiretuCd)
        '商品コード
        Dim objSyouhinCd As Object = IIf(strSyouhinCd = String.Empty, DBNull.Value, strSyouhinCd)
        '調査方法NO
        Dim objTysHouhouNo As Object = IIf(intTysHouhouNo = Integer.MinValue, DBNull.Value, intTysHouhouNo)
        '特別対応コード
        Dim objTokubetuTaiouCd As Object = IIf(intTokubetuTaiouCd = Integer.MinValue, DBNull.Value, intTokubetuTaiouCd)

        'パラメータへ設定
        Dim cmdParams() As SqlParameter = { _
                 SQLHelper.MakeParam(DBprmKbn, SqlDbType.Char, 1, objKbn), _
                 SQLHelper.MakeParam(DBprmHosyousyoNo, SqlDbType.VarChar, 10, objHosyousyoNo), _
                 SQLHelper.MakeParam(DBprmKameitenCd, SqlDbType.VarChar, 5, objKameitenCd), _
                 SQLHelper.MakeParam(DBprmEigyousyoCd, SqlDbType.VarChar, 5, objEigyousyoCd), _
                 SQLHelper.MakeParam(DBprmKeiretuCd, SqlDbType.VarChar, 5, objKeiretuCd), _
                 SQLHelper.MakeParam(DBprmSyouhinCd, SqlDbType.VarChar, 8, objSyouhinCd), _
                 SQLHelper.MakeParam(DBprmTysHouhouNo, SqlDbType.Int, 2, objTysHouhouNo), _
                 SQLHelper.MakeParam(DBprmTokubetuTaiouCd, SqlDbType.Int, 4, objTokubetuTaiouCd) _
        }

        Return cmdParams
    End Function

    ''' <summary>
    ''' ツールチップ用/特別対応マスタ取得用のSQLパラメータを取得
    ''' </summary>
    ''' <param name="listMtt">特別対応マスタのリスト</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTokubetuTaiouMstSqlCmnParams( _
                                                     ByVal listMtt As List(Of TokubetuTaiouMstRecord) _
                                                     ) As SqlParameter()
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokubetuTaiouMstSqlCmnParams", _
                                                    listMtt)

        Dim cmdParams() As SqlClient.SqlParameter   'SQLコマンドパラメータ設定用

        'リストが存在する場合
        If Not listMtt Is Nothing AndAlso listMtt.Count <> 0 Then
            'パラメータ追加数分だけ配列を確保
            ReDim Preserve cmdParams(listMtt.Count - 1)

            'パラメータに請求先を指定
            For j As Integer = 0 To listMtt.Count - 1
                cmdParams(j) = SQLHelper.MakeParam(DBprmTokubetuTaiouCd & "_" & CStr(j), SqlDbType.Int, 4, listMtt(j).TokubetuTaiouCd)
            Next

            Return cmdParams
        End If

        Return Nothing
    End Function

#End Region

End Class
