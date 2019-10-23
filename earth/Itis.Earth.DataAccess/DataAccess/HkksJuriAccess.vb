Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' TBL_M報告書受理への接続クラスです
''' </summary>
''' <remarks></remarks>
Public Class HkksJuriAccess
    Inherits AbsDataAccess

    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' コンボボックス設定用の有効な区分レコードを全て取得します
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, ByVal withSpaceRow As Boolean, Optional ByVal withCode As Boolean = True, Optional ByVal blnTorikesi As Boolean = True)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", dt, withSpaceRow, withCode)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As HkksJuriDataSet.HkksJuriTableRow

        commandTextSb.Append("SELECT hkks_jyuri_no,hkks_jyuri_jyky")
        commandTextSb.Append("  FROM m_hkks_juri")
        commandTextSb.Append("  ORDER BY hkks_jyuri_no")

        ' データの取得
        Dim hkksJuriDataSet As New HkksJuriDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            hkksJuriDataSet, hkksJuriDataSet.HkksJuriTable.TableName)

        Dim hkksJuriDataTable As HkksJuriDataSet.HkksJuriTableDataTable = _
                    hkksJuriDataSet.HkksJuriTable

        If hkksJuriDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In hkksJuriDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.hkks_jyuri_no.ToString + ":" + row.hkks_jyuri_jyky, row.hkks_jyuri_no.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.hkks_jyuri_jyky, row.hkks_jyuri_no.ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
