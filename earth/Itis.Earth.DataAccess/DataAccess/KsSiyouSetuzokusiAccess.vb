Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' TBL_M基礎仕様接続詞への接続クラスです
''' </summary>
''' <remarks></remarks>
Public Class KsSiyouSetuzokusiAccess
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
        Dim row As KsSiyouSetuzokusiDataSet.KsSiyouSetuzokusiTableRow

        commandTextSb.Append("SELECT ks_siyou_setuzokusi_no,ks_siyou_setuzokusi")
        commandTextSb.Append("  FROM m_ks_siyou_setuzokusi")
        commandTextSb.Append("  ORDER BY ks_siyou_setuzokusi_no")

        ' データの取得
        Dim ksSiyouSetuzokusiDataSet As New KsSiyouSetuzokusiDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            ksSiyouSetuzokusiDataSet, ksSiyouSetuzokusiDataSet.KsSiyouSetuzokusiTable.TableName)

        Dim ksSiyouSetuzokusiDataTable As KsSiyouSetuzokusiDataSet.KsSiyouSetuzokusiTableDataTable = _
                    ksSiyouSetuzokusiDataSet.KsSiyouSetuzokusiTable

        If ksSiyouSetuzokusiDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In ksSiyouSetuzokusiDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.ks_siyou_setuzokusi_no.ToString + ":" + row.ks_siyou_setuzokusi, row.ks_siyou_setuzokusi_no.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.ks_siyou_setuzokusi, row.ks_siyou_setuzokusi_no.ToString, dt))
                End If
            Next

        End If

    End Sub


End Class
