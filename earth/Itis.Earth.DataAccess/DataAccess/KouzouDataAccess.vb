Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 構造情報の取得用クラスです
''' </summary>
''' <remarks></remarks>
Public Class KouzouDataAccess
    Inherits AbsDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' コネクションストリングを取得
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' コンボボックス設定用の構造レコードを全て取得します
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
        Dim row As KouzouDataSet.KouzouTableRow

        commandTextSb.Append("SELECT kouzou_no,kouzou")
        commandTextSb.Append("  FROM m_kouzou WITH (READCOMMITTED) ")
        commandTextSb.Append("  ORDER BY kouzou_no ")

        ' データの取得
        Dim KouzouDataSet As New KouzouDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            KouzouDataSet, KouzouDataSet.KouzouTable.TableName)

        Dim KouzouDataTable As KouzouDataSet.KouzouTableDataTable = _
                    KouzouDataSet.KouzouTable

        If KouzouDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In KouzouDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.kouzou_no.ToString + ":" + row.kouzou, row.kouzou_no.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.kouzou, row.kouzou_no.ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
