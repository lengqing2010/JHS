Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 都道府県情報の取得用クラスです
''' </summary>
''' <remarks></remarks>
Public Class TodoufukenDataAccess
    Inherits AbsDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' コネクションストリングを取得
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 都道府県ｺｰﾄﾞを引数に都道府県名を取得します
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataBy(ByVal code As Integer, _
                              ByRef name As String) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDataBy", _
                                                    code, _
                                                    name)

        Dim ds As New DataSet
        Dim MeisyouDataTable As New DataTable
        Dim commandTextSb As New StringBuilder()

        ' パラメータ
        Const paramCode As String = "@CODE"

        commandTextSb.Append("SELECT todouhuken_cd,todouhuken_mei ")
        commandTextSb.Append("  FROM m_todoufuken WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE todouhuken_cd = " + paramCode)
        commandTextSb.Append("  ORDER BY todouhuken_cd ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramCode, SqlDbType.Char, 1, code)}

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
            name = MeisyouDataTable.Rows(0)("todouhuken_mei").ToString
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

        commandTextSb.Append("SELECT todouhuken_cd,todouhuken_mei ")
        commandTextSb.Append("  FROM m_todoufuken WITH (READCOMMITTED) ")
        commandTextSb.Append("  ORDER BY todouhuken_cd ")

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
                    dt.Rows.Add(CreateRow(row("todouhuken_cd").ToString + ":" + row("todouhuken_mei").ToString, row("todouhuken_cd").ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row("todouhuken_mei").ToString, row("todouhuken_cd").ToString, dt))
                End If
            Next

        End If

    End Sub

End Class

