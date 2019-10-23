Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' TBL_Mﾃﾞｰﾀ区分への接続クラスです
''' </summary>
''' <remarks></remarks>
Public Class KubunDataAccess2
    Inherits AbsDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' コンボボックス設定用の有効な区分レコードを全て取得します
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, _
                                         ByVal withSpaceRow As Boolean, _
                                         Optional ByVal withCode As Boolean = True, _
                                         Optional ByVal blnTorikesi As Boolean = True)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", _
                                            dt, _
                                            withSpaceRow, _
                                            withCode)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As KubunDataSet.DataKbnTableRow

        commandTextSb.Append(" SELECT k.kbn, k.torikesi, ")
        commandTextSb.Append("        ISNULL(k.kbn_mei + ' [' + LEFT(CONVERT(VARCHAR,h.hosyousyo_no_nengetu,111),7) + ']',k.kbn_mei) kbn_mei ")
        commandTextSb.Append("  FROM m_data_kbn k WITH (READCOMMITTED) ")
        commandTextSb.Append("       LEFT OUTER JOIN m_hiduke_save h WITH (READCOMMITTED) ON h.kbn = k.kbn ")
        commandTextSb.Append("  WHERE k.torikesi = 0 ")
        commandTextSb.Append("  ORDER BY k.kbn ")

        ' データの取得
        Dim kubunDataSet As New KubunDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            kubunDataSet, kubunDataSet.DataKbnTable.TableName)

        Dim kubunDataTable As KubunDataSet.DataKbnTableDataTable = _
                    kubunDataSet.DataKbnTable

        If kubunDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In kubunDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.kbn + ":" + row.kbn_mei, row.kbn, dt))
                Else
                    dt.Rows.Add(CreateRow(row.kbn_mei, row.kbn, dt))
                End If
            Next

        End If

    End Sub

End Class
