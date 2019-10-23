Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' ﾃﾞｰﾀ破棄種別情報の取得用クラスです
''' </summary>
''' <remarks></remarks>
Public Class DataHakiDataAccess
    Inherits AbsDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' コネクションストリングを取得
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' コンボボックス設定用のﾃﾞｰﾀ破棄種別レコードを全て取得します
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, _
                                         ByVal withSpaceRow As Boolean, _
                                         Optional ByVal withCode As Boolean = True, _
                                         Optional ByVal blnTorikesi As Boolean = True)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", _
                                                    dt, _
                                                    withSpaceRow, _
                                                    withCode)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As DataHakiDataSet.DataHakiTableRow

        commandTextSb.Append("SELECT data_haki_no,haki_syubetu")
        commandTextSb.Append("  FROM m_data_haki WITH (READCOMMITTED) ")
        commandTextSb.Append("  ORDER BY data_haki_no ")

        ' データの取得
        Dim DataHakiDataSet As New DataHakiDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            DataHakiDataSet, DataHakiDataSet.DataHakiTable.TableName)

        Dim DataHakiDataTable As DataHakiDataSet.DataHakiTableDataTable = _
                    DataHakiDataSet.DataHakiTable

        If DataHakiDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "0", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In DataHakiDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.data_haki_no.ToString + ":" + row.haki_syubetu, row.data_haki_no.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.haki_syubetu, row.data_haki_no.ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
