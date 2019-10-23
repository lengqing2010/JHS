Imports System
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 請求データの取得クラス
''' </summary>
''' <remarks></remarks>
Public Class SeikyuuMiinsatuDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim SLogic As New StringLogic
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "請求書データの取得"
    ''' <summary>
    ''' 請求書未印刷一覧画面/請求データを取得します
    ''' </summary>
    ''' <param name="dtRec">請求データレコードクラス</param>
    ''' <returns>請求データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSearchSeikyuusyoTbl(ByVal dtRec As SeikyuuDataRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSearchSeikyuusyoTbl", dtRec)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        'SELECT句
        Dim strCmnSelect As String = Me.GetSearchSeikyuusyoSqlSelect()
        'TABLE句
        Dim strCmnTable As String = Me.GetCmnSqlTable()
        'WHERE句
        Dim strCmnWhere As String = Me.GetCmnSqlWhere(dtRec)
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
        '* WHERE句
        '****************
        cmdTextSb.Append(strCmnWhere)

        '***********************************************************************
        ' ORDER BY句（請求書発行日DESC→請求先（区分・コード・枝番））
        '***********************************************************************
        cmdTextSb.Append(strCmnOrderBy)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString)
    End Function

#End Region

#Region "SELECT句"
    ''' <summary>
    ''' 未印刷請求データ取得用のSELECTクエリを取得
    ''' </summary>
    ''' <returns>未印刷請求データ取得用の共通SELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetSearchSeikyuusyoSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSearchSeikyuusyoSqlSelect")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      SK.seikyuu_saki_cd ")
        cmdTextSb.Append("    , SK.seikyuu_saki_brc ")
        cmdTextSb.Append("    , SK.seikyuu_saki_kbn ")
        cmdTextSb.Append("    , SK.seikyuu_saki_mei ")
        cmdTextSb.Append("    , SK.seikyuusyo_hak_date ")
        cmdTextSb.Append("    , SK.seikyuu_sime_date ")
        cmdTextSb.Append("    , ISNULL(MKM.meisyou, '') AS mst_meisyou ")

        Return cmdTextSb.ToString
    End Function

#End Region

#Region "TABLE句"
    ''' <summary>
    ''' 請求データ取得用のTABLEクエリを取得
    ''' </summary>
    ''' <returns>請求データ取得用の共通TABLEクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlTable")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* TABLE項目(メイン)
        '****************
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("    t_seikyuu_kagami SK WITH (READCOMMITTED) ")

        '***********************************************************************
        ' 拡張名称マスタ：名称(外部結合)
        '***********************************************************************
        cmdTextSb.Append(" LEFT OUTER JOIN m_kakutyou_meisyou MKM WITH (READCOMMITTED) ")
        cmdTextSb.Append("   ON MKM.meisyou_syubetu = 3 ")
        cmdTextSb.Append("  AND SK.kaisyuu_seikyuusyo_yousi = MKM.code ")

        Return cmdTextSb.ToString
    End Function

#End Region

#Region "WHERE句"
    ''' <summary>
    ''' 請求データ取得用のWHEREクエリを取得
    ''' </summary>
    ''' <param name="dtRec">請求データレコードクラス</param>
    ''' <returns>請求データ取得用の共通WHEREクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlWhere(ByVal dtRec As SeikyuuDataRecord) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlWhere", dtRec)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" WHERE 1 = 1 ")
        cmdTextSb.Append("  AND SK.seikyuusyo_insatu_date IS NULL ")
        cmdTextSb.Append("  AND SK.torikesi = 0 ")

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

        cmdTextSb.Append(" ORDER BY ")
        cmdTextSb.Append("      SK.seikyuu_saki_cd ")
        cmdTextSb.Append("    , SK.seikyuu_saki_brc ")
        cmdTextSb.Append("    , SK.seikyuu_saki_kbn ")
        cmdTextSb.Append("    , SK.seikyuusyo_hak_date ")

        Return cmdTextSb.ToString
    End Function
#End Region

End Class
