Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' TBL_消費税への接続クラスです
''' </summary>
''' <remarks></remarks>
Public Class SyouhizeiAccess
    Inherits AbsDataAccess

    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' ｺｰﾄﾞを引数に名称を取得します
    ''' </summary>
    ''' <param name="code">コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMeisyou( _
                                ByVal code As String _
                                , ByRef name As String _
                                , Optional ByVal blnPercent As Boolean = False _
                                ) As Boolean

        Dim ds As New DataSet
        Dim MeisyouDataTable As New DataTable
        Dim commandTextSb As New StringBuilder()

        ' パラメータ
        Const prmZeiKbn As String = "@ZEI_KBN"

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("  zei_kbn ")
        If blnPercent Then
            commandTextSb.Append("  ,CONVERT(VARCHAR, CONVERT(INTEGER, zeiritu * 100)) + '%' AS zeiritu ") '数値→文字列 + '%'
        Else
            commandTextSb.Append("  ,zeiritu ")
        End If
        commandTextSb.Append("  FROM m_syouhizei ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append(" 	zei_kbn = " & prmZeiKbn)
        commandTextSb.Append("  ORDER BY zei_kbn ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(prmZeiKbn, SqlDbType.Char, 1, code)}

        ' 検索実行
        ds = ExecuteDataset(connStr, _
                             CommandType.Text, _
                             commandTextSb.ToString, _
                             commandParameters _
                             )

        '返却用データテーブルへ格納
        MeisyouDataTable = ds.Tables(0)

        If MeisyouDataTable.Rows.Count = 0 Then
            Return False
        Else
            name = MeisyouDataTable.Rows(0)("zeiritu").ToString
        End If

        Return True

    End Function

    ''' <summary>
    ''' コンボボックス設定用の有効な区分レコードを全て取得します
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, ByVal withSpaceRow As Boolean, Optional ByVal withCode As Boolean = True, Optional ByVal blnTorikesi As Boolean = True)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", dt, withSpaceRow, withCode)

        Dim ds As New DataSet
        Dim resDataTable As New DataTable
        Dim commandTextSb As New StringBuilder()
        Dim row As DataRow

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("  zei_kbn ")
        commandTextSb.Append("  ,CONVERT(VARCHAR, CONVERT(INTEGER, zeiritu * 100)) + '%' AS zeiritu ") '数値→文字列 + '%'
        commandTextSb.Append("  FROM m_syouhizei ")
        commandTextSb.Append("  ORDER BY zei_kbn ")

        ' 検索実行
        ds = ExecuteDataset(connStr, _
                             CommandType.Text, _
                             commandTextSb.ToString _
                             )

        'データテーブルへ格納
        resDataTable = ds.Tables(0)

        If resDataTable.Rows.Count > 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In resDataTable.Rows
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row("zei_kbn").ToString + ":【" + row("zeiritu").ToString & "】", row("zei_kbn").ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row("zeiritu").ToString, row("zei_kbn").ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
