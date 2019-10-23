Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 調査概要情報の取得用クラスです
''' </summary>
''' <remarks>調査概要は名称テーブルの名称種別"02"</remarks>
Public Class TyousagaiyouDataAccess
    Inherits AbsDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' コネクションストリングを取得
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 調査概要ｺｰﾄﾞを引数に名称を取得します
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

        ' パラメータ
        Const paramCode As String = "@CODE"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT code,meisyou ")
        commandTextSb.Append("  FROM m_meisyou WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE meisyou_syubetu = '02' ")
        commandTextSb.Append("  AND   code     = " + paramCode)
        commandTextSb.Append("  ORDER BY hyouji_jyun ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramCode, SqlDbType.Int, 1, code)}

        ' データの取得
        Dim TyousagaiyouDataSet As New TyousagaiyouDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            TyousagaiyouDataSet, TyousagaiyouDataSet.TyousagaiyouTable.TableName, commandParameters)

        Dim TyousagaiyouTable As TyousagaiyouDataSet.TyousagaiyouTableDataTable = TyousagaiyouDataSet.TyousagaiyouTable

        If TyousagaiyouTable.Count = 0 Then
            Debug.WriteLine("取得出来ませんでした")
            Return False
        Else
            Dim row As TyousagaiyouDataSet.TyousagaiyouTableRow = TyousagaiyouTable(0)
            name = row.meisyou
        End If

        Return True

    End Function

    ''' <summary>
    ''' コンボボックス設定用の有効な調査概要レコードを全て取得します
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
        Dim row As TyousagaiyouDataSet.TyousagaiyouTableRow

        commandTextSb.Append("SELECT code,meisyou ")
        commandTextSb.Append("  FROM m_meisyou WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE meisyou_syubetu = '02' ")
        commandTextSb.Append("  AND   code     <> 9 ")
        commandTextSb.Append("  ORDER BY hyouji_jyun ")

        ' データの取得
        Dim TyousagaiyouDataSet As New TyousagaiyouDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            TyousagaiyouDataSet, TyousagaiyouDataSet.TyousagaiyouTable.TableName)

        Dim TyousagaiyouDataTable As TyousagaiyouDataSet.TyousagaiyouTableDataTable = _
                    TyousagaiyouDataSet.TyousagaiyouTable

        If TyousagaiyouDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "9", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In TyousagaiyouDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.code.ToString + ":" + row.meisyou, row.code, dt))
                Else
                    dt.Rows.Add(CreateRow(row.meisyou, row.code, dt))
                End If
            Next

        End If


    End Sub

End Class
