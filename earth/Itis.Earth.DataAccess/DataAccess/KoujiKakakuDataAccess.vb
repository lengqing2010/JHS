Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 工事価格の取得クラス
''' </summary>
''' <remarks></remarks>
Public Class KoujiKakakuDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "コントロール値"
    Private Const strUrigaku As String = "uri_gaku"
    Private Const strKojGaisyaSeikyuuUmu As String = "koj_gaisya_seikyuu_umu"
    Private Const strSeikyuuUmu As String = "seikyuu_umu"
#End Region

#Region "工事価格の取得"
    ''' <summary>
    ''' 工事価格の取得
    ''' </summary>
    ''' <param name="intAitesakiSyubetu">相手先種別</param>
    ''' <param name="strAitesakiCd">相手先コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strKojGaisyaCd">工事会社コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKoujiKakakuInfo(ByVal intAitesakiSyubetu As Integer, _
                                       ByVal strAitesakiCd As String, _
                                       ByVal strSyouhinCd As String, _
                                       ByVal strKojGaisyaCd As String _
                                       ) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKoujiKakakuInfo", _
                                                    intAitesakiSyubetu, _
                                                    strAitesakiCd, _
                                                    strSyouhinCd, _
                                                    strKojGaisyaCd _
                                                    )

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        'パラメータ
        Dim cmdParams() As SqlParameter = Me.GetKoujiKakakuSqlCmnParams(intAitesakiSyubetu, _
                                                                        strAitesakiCd, _
                                                                        strSyouhinCd, _
                                                                        strKojGaisyaCd)
        'SELECT句
        Dim strCmnSelect As String = Me.GetCmnSqlSelect()
        'TABLE句
        Dim strCmnTable As String = Me.GetCmnSqlTable()
        'WHERE句
        Dim strCmnWhere As String = Me.GetCmnSqlWhere(intAitesakiSyubetu, _
                                                      strAitesakiCd, _
                                                      strSyouhinCd, _
                                                      strKojGaisyaCd)
        '****************
        '* SELECT項目
        '****************
        cmdTextSb.Append(strCmnSelect)

        '****************
        '* TABLE項目
        '****************
        cmdTextSb.Append(strCmnTable)

        '****************
        '* WHERE句
        '****************
        cmdTextSb.Append(strCmnWhere)

        'データ取得
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function
#End Region

#Region "共通クエリ"

#Region "SELECT句"
    ''' <summary>
    ''' 工事価格マスタ取得用の共通SELECTクエリを取得
    ''' </summary>
    ''' <returns>工事価格マスタ取得用の共通SELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlSelect")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("      kk.aitesaki_syubetu ")
        cmdTextSb.AppendLine("    , kk.aitesaki_cd ")
        cmdTextSb.AppendLine("    , kk.syouhin_cd ")
        cmdTextSb.AppendLine("    , kk.koj_gaisya_cd ")
        cmdTextSb.AppendLine("    , kk.koj_gaisya_jigyousyo_cd ")
        cmdTextSb.AppendLine("    , kk.torikesi ")
        cmdTextSb.AppendLine("    , kk.uri_gaku ")
        cmdTextSb.AppendLine("    , s.zei_kbn ")
        cmdTextSb.AppendLine("    , z.zeiritu ")
        cmdTextSb.AppendLine("    , kk.koj_gaisya_seikyuu_umu ")
        cmdTextSb.AppendLine("    , kk.seikyuu_umu ")
        cmdTextSb.AppendLine("    , isnull(kk.upd_login_user_id, kk.add_login_user_id) AS upd_login_user_id ")
        cmdTextSb.AppendLine("    , isnull(kk.upd_datetime, kk.add_datetime) AS upd_datetime ")

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "TABLE句"
    ''' <summary>
    ''' 工事価格マスタ取得用の共通TABLEクエリを取得
    ''' </summary>
    ''' <returns>工事価格マスタ取得用の共通TABLEクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlTable")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* TABLE項目
        '****************
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      m_koj_kakaku kk WITH (READCOMMITTED) ")
        cmdTextSb.AppendLine("           LEFT OUTER JOIN m_syouhin s WITH (READCOMMITTED) ")
        cmdTextSb.AppendLine("             ON kk.syouhin_cd = s.syouhin_cd ")
        cmdTextSb.AppendLine("           LEFT OUTER JOIN m_syouhizei z WITH (READCOMMITTED) ")
        cmdTextSb.AppendLine("             ON s.zei_kbn = z.zei_kbn ")

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "WHERE句"
    ''' <summary>
    ''' 工事価格マスタ取得用の共通WHEREクエリを取得
    ''' </summary>
    ''' <param name="intAitesakiSyubetu">相手先種別</param>
    ''' <param name="strAitesakiCd">相手先コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strKojGaisyaCd">工事会社コード</param>
    ''' <returns>工事価格マスタ取得用の共通WHEREクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlWhere(ByVal intAitesakiSyubetu As Integer, _
                                    ByVal strAitesakiCd As String, _
                                    ByVal strSyouhinCd As String, _
                                    ByVal strKojGaisyaCd As String _
                                    ) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlWhere", _
                                                    intAitesakiSyubetu, _
                                                    strAitesakiCd, _
                                                    strSyouhinCd, _
                                                    strKojGaisyaCd)
        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.AppendLine(" WHERE 1 = 1 ")
        cmdTextSb.AppendLine("  AND kk.aitesaki_syubetu = " & DBprmAitesakiSyubetu)
        cmdTextSb.AppendLine("  AND kk.aitesaki_cd = " & DBprmAitesakiCd)
        cmdTextSb.AppendLine("  AND kk.syouhin_cd = " & DBprmSyouhinCd)
        cmdTextSb.AppendLine("  AND kk.koj_gaisya_cd + kk.koj_gaisya_jigyousyo_cd = " & DBprmKoujiKaisyaCd)
        cmdTextSb.AppendLine("  AND kk.torikesi = 0 ")

        Return cmdTextSb.ToString
    End Function
#End Region

#End Region

#Region "SQLパラメータ"

#Region "パラメータ定義"
    '相手先種別
    Private Const DBprmAitesakiSyubetu As String = "@AITESAKI_SYUBETU"
    '相手先コード
    Private Const DBprmAitesakiCd As String = "@AITESAKI_CD"
    '商品コード
    Private Const DBprmSyouhinCd As String = "@SYOUHIN_CD"
    '工事会社コード
    Private Const DBprmKoujiKaisyaCd As String = "@KOUJI_KAISYA_CD"
#End Region

    ''' <summary>
    ''' 工事価格マスタ取得用の共通SQLパラメータを取得
    ''' </summary>
    ''' <param name="intAitesakiSyubetu">相手先種別</param>
    ''' <param name="strAitesakiCd">相手先コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strKojGaisyaCd">工事会社コード</param>
    ''' <returns>工事価格マスタ取得用の共通SQLパラメータを取得</returns>
    ''' <remarks></remarks>
    Private Function GetKoujiKakakuSqlCmnParams(ByVal intAitesakiSyubetu As Integer, _
                                                ByVal strAitesakiCd As String, _
                                                ByVal strSyouhinCd As String, _
                                                ByVal strKojGaisyaCd As String _
                                                ) As SqlParameter()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKoujiKakakuSqlCmnParams", _
                                                    intAitesakiSyubetu, _
                                                    strAitesakiCd, _
                                                    strSyouhinCd, _
                                                    strKojGaisyaCd)

        '相手先種別
        Dim objAitesakiSyubetu As Object = IIf(intAitesakiSyubetu = Integer.MinValue, DBNull.Value, intAitesakiSyubetu)
        '相手先コード
        Dim objAitesakiCd As Object = IIf(strAitesakiCd = String.Empty, DBNull.Value, strAitesakiCd)
        '商品コード
        Dim objSyouhinCd As Object = IIf(strSyouhinCd = String.Empty, DBNull.Value, strSyouhinCd)
        '工事会社コード
        Dim objKoujiKaisyaCd As Object = IIf(strKojGaisyaCd = String.Empty, DBNull.Value, strKojGaisyaCd)

        'パラメータへ設定
        Dim cmdParams() As SqlParameter = { _
                SQLHelper.MakeParam(DBprmAitesakiSyubetu, SqlDbType.Int, 4, objAitesakiSyubetu), _
                SQLHelper.MakeParam(DBprmAitesakiCd, SqlDbType.VarChar, 5, objAitesakiCd), _
                SQLHelper.MakeParam(DBprmSyouhinCd, SqlDbType.VarChar, 8, objSyouhinCd), _
                SQLHelper.MakeParam(DBprmKoujiKaisyaCd, SqlDbType.VarChar, 7, objKoujiKaisyaCd) _
        }

        Return cmdParams
    End Function

#End Region

End Class
