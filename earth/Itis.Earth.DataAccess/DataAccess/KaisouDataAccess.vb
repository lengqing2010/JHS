Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 階層情報の取得用クラスです
''' </summary>
''' <remarks></remarks>
Public Class KaisouDataAccess
    Inherits AbsDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' コネクションストリングを取得
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' コンボボックス設定用の階層レコードを全て取得します
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
        Dim row As KaisouDataSet.KaisouTableRow

        commandTextSb.Append("SELECT kaisou_no,kaisou")
        commandTextSb.Append("  FROM m_kaisou WITH (READCOMMITTED) ")
        commandTextSb.Append("  ORDER BY kaisou_no ")

        ' データの取得
        Dim KaisouDataSet As New KaisouDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            KaisouDataSet, KaisouDataSet.KaisouTable.TableName)

        Dim KaisouDataTable As KaisouDataSet.KaisouTableDataTable = _
                    KaisouDataSet.KaisouTable

        If KaisouDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In KaisouDataTable

                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.kaisou_no.ToString + ":" + row.kaisou, row.kaisou_no.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.kaisou, row.kaisou_no.ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
