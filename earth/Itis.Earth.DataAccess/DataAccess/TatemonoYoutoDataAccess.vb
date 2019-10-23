Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 建物用途情報の取得用クラスです
''' </summary>
''' <remarks></remarks>
Public Class TatemonoYoutoDataAccess
    Inherits AbsDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' コネクションストリングを取得
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' コンボボックス設定用の建物用途レコードを全て取得します
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
        Dim row As TatemonoYoutoDataSet.TatemonoYoutoTableRow

        commandTextSb.Append("SELECT tatemono_youto_no,tatemono_youto_mei")
        commandTextSb.Append("  FROM m_tatemono_youto WITH (READCOMMITTED) ")
        commandTextSb.Append("  ORDER BY tatemono_youto_no ")

        ' データの取得
        Dim TatemonoYoutoDataSet As New TatemonoYoutoDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            TatemonoYoutoDataSet, TatemonoYoutoDataSet.TatemonoYoutoTable.TableName)

        Dim TatemonoYoutoDataTable As TatemonoYoutoDataSet.TatemonoYoutoTableDataTable = _
                    TatemonoYoutoDataSet.TatemonoYoutoTable

        If TatemonoYoutoDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In TatemonoYoutoDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.tatemono_youto_no.ToString + ":" + row.tatemono_youto_mei, row.tatemono_youto_no.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.tatemono_youto_mei, row.tatemono_youto_no.ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
