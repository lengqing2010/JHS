Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' お知らせデータの取得クラスです
''' </summary>
''' <remarks></remarks>
Public Class OsiraseDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' お知らせレコードを取得します
    ''' </summary>
    ''' <returns>お知らせデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetOsiraseData() As OsiraseDataSet.OsiraseTableDataTable

        Dim commandText As String = " SELECT " & _
                                    "   nengappi, " & _
                                    "   nyuuryoku_busyo, " & _
                                    "   nyuuryoku_mei, " & _
                                    "   hyouji_naiyou, " & _
                                    "   link_saki" & _
                                    " FROM " & _
                                    "    t_osirase WITH (READCOMMITTED) " & _
                                    " WHERE " & _
                                    "    torikesi = 0  " & _
                                    " ORDER BY nengappi desc"

        ' データの取得
        Dim OsiraseDataSet As New OsiraseDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
            OsiraseDataSet, OsiraseDataSet.OsiraseTable.TableName)

        Dim OsiraseTable As OsiraseDataSet.OsiraseTableDataTable = OsiraseDataSet.OsiraseTable

        Return OsiraseTable

    End Function

End Class
