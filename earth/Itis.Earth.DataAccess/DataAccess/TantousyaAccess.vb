Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' TBL_M担当者への接続クラスです
''' </summary>
''' <remarks></remarks>
Public Class TantousyaAccess
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
        Dim row As TantousyaDataSet.TantousyaTableRow

        commandTextSb.Append("SELECT tantousya_cd,tantousya_mei")
        commandTextSb.Append("  FROM m_tantousya")
        commandTextSb.Append("  WHERE hyouji_kbn = 0")
        commandTextSb.Append("  ORDER BY hyouji_kbn")

        ' データの取得
        Dim tantousyaDataSet As New TantousyaDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            tantousyaDataSet, tantousyaDataSet.TantousyaTable.TableName)

        Dim tantousyaDataTable As TantousyaDataSet.TantousyaTableDataTable = _
                    tantousyaDataSet.TantousyaTable

        If tantousyaDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In tantousyaDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.tantousya_cd.ToString + ":" + row.tantousya_mei, row.tantousya_cd.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.tantousya_mei, row.tantousya_cd.ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
