Imports System.text
Imports System.Data.SqlClient

Public Class BukkenRirekiDataAccess
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
#End Region

    ''' <summary>
    ''' 物件履歴テーブルの情報を取得します
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <returns>物件履歴データテーブル</returns>
    ''' <remarks>区分と保証書NOをKEYにして取得</remarks>
    Public Function getBukkenRirekiTable(ByVal strKbn As String, ByVal strHosyousyoNo As String) As BukkenRirekiDataSet.BukkenRirekiTableDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getBukkenRirekiTable" _
                                                    , strKbn _
                                                    , strHosyousyoNo)
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim RirekiDataSet As New BukkenRirekiDataSet
        Dim RirekiTable As BukkenRirekiDataSet.BukkenRirekiTableDataTable

        commandTextSb.Append(" SELECT kbn")
        commandTextSb.Append("       ,hosyousyo_no")
        commandTextSb.Append("       ,rireki_syubetu")
        commandTextSb.Append("       ,rireki_no")
        commandTextSb.Append("       ,nyuuryoku_no")
        commandTextSb.Append("       ,naiyou")
        commandTextSb.Append("       ,hanyou_date")
        commandTextSb.Append("       ,hanyou_cd")
        commandTextSb.Append("       ,kanri_date")
        commandTextSb.Append("       ,kanri_cd")
        commandTextSb.Append("       ,henkou_kahi_flg")
        commandTextSb.Append("       ,torikesi")
        commandTextSb.Append("       ,add_login_user_id")
        commandTextSb.Append("       ,add_datetime")
        commandTextSb.Append("       ,upd_login_user_id")
        commandTextSb.Append("       ,upd_datetime")
        commandTextSb.Append("       ,ISNULL(upd_datetime,add_datetime) AS sort_date")
        commandTextSb.Append("   FROM t_bukken_rireki")
        commandTextSb.Append("  WHERE kbn          = @KBN")
        commandTextSb.Append("    AND hosyousyo_no = @HOSYOUSYONO")
        commandTextSb.Append("  ORDER BY ")
        commandTextSb.Append("          sort_date DESC")
        commandTextSb.Append("          ,nyuuryoku_no DESC")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, strKbn), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, strHosyousyoNo)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            RirekiDataSet, RirekiDataSet.BukkenRirekiTable.TableName, cmdParams)
        RirekiTable = RirekiDataSet.BukkenRirekiTable

        Return RirekiTable
    End Function

    ''' <summary>
    ''' 物件履歴テーブルの情報をPKで取得します
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <param name="intNyuuryokuNo">入力NO</param>
    ''' <returns>物件履歴データテーブル</returns>
    ''' <remarks>PKで物件履歴テーブルの1レコードを取得</remarks>
    Public Function getBukkenRirekiTable(ByVal strKbn As String, ByVal strHosyousyoNo As String, ByVal intNyuuryokuNo As Integer) As BukkenRirekiDataSet.BukkenRirekiTableDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getBukkenRirekiTable" _
                                                    , strKbn _
                                                    , strHosyousyoNo _
                                                    , intNyuuryokuNo)
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim RirekiDataSet As New BukkenRirekiDataSet
        Dim RirekiTable As BukkenRirekiDataSet.BukkenRirekiTableDataTable

        commandTextSb.Append(" SELECT kbn")
        commandTextSb.Append("       ,hosyousyo_no")
        commandTextSb.Append("       ,rireki_syubetu")
        commandTextSb.Append("       ,rireki_no")
        commandTextSb.Append("       ,nyuuryoku_no")
        commandTextSb.Append("       ,naiyou")
        commandTextSb.Append("       ,hanyou_date")
        commandTextSb.Append("       ,hanyou_cd")
        commandTextSb.Append("       ,kanri_date")
        commandTextSb.Append("       ,kanri_cd")
        commandTextSb.Append("       ,henkou_kahi_flg")
        commandTextSb.Append("       ,torikesi")
        commandTextSb.Append("       ,add_login_user_id")
        commandTextSb.Append("       ,add_datetime")
        commandTextSb.Append("       ,upd_login_user_id")
        commandTextSb.Append("       ,upd_datetime")
        commandTextSb.Append("   FROM t_bukken_rireki")
        commandTextSb.Append("  WHERE kbn          = @KBN")
        commandTextSb.Append("    AND hosyousyo_no = @HOSYOUSYONO")
        commandTextSb.Append("    AND nyuuryoku_no = @NYUURYOKUNO")
        commandTextSb.Append("  ORDER BY kbn")
        commandTextSb.Append("          ,hosyousyo_no")
        commandTextSb.Append("          ,nyuuryoku_no")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, strKbn), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, strHosyousyoNo), _
            SQLHelper.MakeParam("@NYUURYOKUNO", SqlDbType.Int, 4, intNyuuryokuNo)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            RirekiDataSet, RirekiDataSet.BukkenRirekiTable.TableName, cmdParams)
        RirekiTable = RirekiDataSet.BukkenRirekiTable

        Return RirekiTable
    End Function

    ''' <summary>
    ''' 物件履歴テーブルから、指定された区分、保証書NOで入力NOの最大値を取得します
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strHosyousyoNo">保証書NO</param>
    ''' <returns>入力NOの最大値</returns>
    ''' <remarks></remarks>
    Public Function getMaxNo(ByVal strKbn As String, ByVal strHosyousyoNo As String) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getMaxNo" _
                                                    , strKbn _
                                                    , strHosyousyoNo)
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim RirekiDataSet As New BukkenRirekiDataSet
        Dim MaxNoTable As BukkenRirekiDataSet.MaxNyuuryokuNoTableDataTable

        Dim intMaxNo

        commandTextSb.Append(" SELECT MAX(nyuuryoku_no) max_no")
        commandTextSb.Append("   FROM t_bukken_rireki")
        commandTextSb.Append("  WHERE kbn          = @KBN")
        commandTextSb.Append("    AND hosyousyo_no = @HOSYOUSYONO")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, strKbn), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, strHosyousyoNo)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            RirekiDataSet, RirekiDataSet.MaxNyuuryokuNoTable.TableName, cmdParams)
        MaxNoTable = RirekiDataSet.MaxNyuuryokuNoTable

        intMaxNo = MaxNoTable.Rows(0).Item("max_no")

        If IsDBNull(intMaxNo) Then
            intMaxNo = 0
        End If

        Return intMaxNo

    End Function

End Class
