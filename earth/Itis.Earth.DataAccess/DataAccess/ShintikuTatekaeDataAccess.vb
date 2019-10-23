Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 新築建替情報の取得用クラスです
''' </summary>
''' <remarks></remarks>
Public Class ShintikuTatekaeDataAccess
    Inherits AbsDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' コネクションストリングを取得
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' コンボボックス設定用の新築建替レコードを全て取得します
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
        Dim row As ShintikuTatekaeDataSet.ShintikuTatekaeTableRow

        commandTextSb.Append("SELECT sintiku_tatekae_no,sintiku_tatekae ")
        commandTextSb.Append("  FROM m_sintiku_tatekae WITH (READCOMMITTED) ")
        commandTextSb.Append("  ORDER BY sintiku_tatekae_no ")

        ' データの取得
        Dim ShintikuTatekaeDataSet As New ShintikuTatekaeDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            ShintikuTatekaeDataSet, ShintikuTatekaeDataSet.ShintikuTatekaeTable.TableName)

        Dim ShintikuTatekaeDataTable As ShintikuTatekaeDataSet.ShintikuTatekaeTableDataTable = _
                    ShintikuTatekaeDataSet.ShintikuTatekaeTable

        If ShintikuTatekaeDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In ShintikuTatekaeDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.sintiku_tatekae_no.ToString + ":" + row.sintiku_tatekae, row.sintiku_tatekae_no.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.sintiku_tatekae, row.sintiku_tatekae_no.ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
