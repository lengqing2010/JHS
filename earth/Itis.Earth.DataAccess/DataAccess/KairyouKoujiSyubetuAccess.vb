Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' TBL_改良工事種別への接続クラスです
''' </summary>
''' <remarks></remarks>
Public Class KairyouKoujiSyubetuAccess
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
        Dim row As KairyouKoujiSyubetuDataSet.KairyKojSyubetuTableRow

        commandTextSb.Append("SELECT kairy_koj_syubetu_no,kairy_koj_syubetu")
        commandTextSb.Append("  FROM m_kairy_koj_syubetu")
        commandTextSb.Append("  ORDER BY kairy_koj_syubetu_no")

        ' データの取得
        Dim kairyouSyubetuDataSet As New KairyouKoujiSyubetuDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            kairyouSyubetuDataSet, kairyouSyubetuDataSet.KairyKojSyubetuTable.TableName)

        Dim kairyouKoujiSyubetuDataTable As KairyouKoujiSyubetuDataSet.KairyKojSyubetuTableDataTable = _
                    kairyouSyubetuDataSet.KairyKojSyubetuTable

        If kairyouKoujiSyubetuDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In kairyouKoujiSyubetuDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.kairy_koj_syubetu_no.ToString + ":" + row.kairy_koj_syubetu, row.kairy_koj_syubetu_no.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.kairy_koj_syubetu, row.kairy_koj_syubetu_no.ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
