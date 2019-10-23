Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' TBL_M保証書発行状況への接続クラスです
''' </summary>
''' <remarks></remarks>
Public Class HosyousyoHakJykyAccess
    Inherits AbsDataAccess

    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' コネクションストリングを取得
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' コンボボックス設定用の有効な区分レコードを全て取得します
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, ByVal withSpaceRow As Boolean, Optional ByVal withCode As Boolean = True, Optional ByVal blnTorikesi As Boolean = True)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", dt, withSpaceRow, withCode)

        Dim commandTextSb As New StringBuilder()

        Dim ds As New DataSet
        Dim dTblRes As New DataTable
        Dim row As DataRow

        commandTextSb.Append("SELECT ")
        commandTextSb.Append("     hosyousyo_hak_jyky_no ")
        commandTextSb.Append("   , hosyousyo_hak_jyky ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("     m_hosyousyo_hak_jyky ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("     riyou = 0 ")
        commandTextSb.Append(" ORDER BY ")
        commandTextSb.Append("     hyouji_jyun ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = {}

        ' 検索実行
        ds = ExecuteDataset(connStr, _
                             CommandType.Text, _
                             commandTextSb.ToString, _
                             commandParameters _
                             )

        'データテーブルへ格納
        dTblRes = ds.Tables(0)

        If dTblRes.Rows.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In dTblRes.Rows
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row("hosyousyo_hak_jyky_no").ToString + ":" + row("hosyousyo_hak_jyky").ToString, row("hosyousyo_hak_jyky_no").ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row("hosyousyo_hak_jyky").ToString, row("hosyousyo_hak_jyky_no").ToString, dt))
                End If
            Next

        End If

    End Sub

    ''' <summary>
    ''' 保証書発行状況Mの保証書発行状況NOと対応する保証有無のデータを返却する
    ''' </summary>
    ''' <param name="dt">データ格納用データテーブル</param>
    ''' <param name="dicRet">データ格納用ディクショナリ</param>
    ''' <remarks></remarks>
    Public Sub GetHosyousyoHakJykyInfo(ByRef dt As DataTable, ByRef dicRet As Dictionary(Of String, String))
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyousyoHakJykyData", dt)

        Dim commandTextSb As New StringBuilder()

        Dim ds As New DataSet
        Dim dTblRes As New DataTable
        Dim row As DataRow

        'クエリ生成
        commandTextSb.Append("SELECT ")
        commandTextSb.Append("     hosyousyo_hak_jyky_no ")
        commandTextSb.Append("   , hosyousyo_hak_jyky ")
        commandTextSb.Append("   , mihak_list_inji_umu ")
        commandTextSb.Append("   , kokyaku_list_inji_umu ")
        commandTextSb.Append("   , hyouji_jyun ")
        commandTextSb.Append("   , riyou ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("     m_hosyousyo_hak_jyky ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("     1=1 ")
        commandTextSb.Append(" ORDER BY ")
        commandTextSb.Append("     hyouji_jyun ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = {}

        ' 検索実行
        ds = ExecuteDataset(connStr, _
                             CommandType.Text, _
                             commandTextSb.ToString, _
                             commandParameters _
                             )

        'データテーブルへ格納
        dTblRes = ds.Tables(0)

        If dTblRes.Rows.Count = 0 Then
            dTblRes = Nothing
            dicRet = Nothing
        Else
            dicRet = New Dictionary(Of String, String)

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In dTblRes.Rows
                dicRet.Add(row("hosyousyo_hak_jyky_no").ToString, row("mihak_list_inji_umu").ToString)
            Next
        End If
        dt = dTblRes
    End Sub

    ''' <summary>
    ''' ｺｰﾄﾞを引数に名称を取得します
    ''' </summary>
    ''' <param name="code">コード</param>
    ''' <param name="name">名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMeisyou( _
                                ByVal code As String _
                                , ByRef name As String _
                                ) As Boolean

        Dim ds As New DataSet
        Dim MeisyouDataTable As New DataTable
        Dim commandTextSb As New StringBuilder()

        Const DBparamHosyousyoHakJykyNo As String = "@HOSYOUSYO_HAK_JYKY_NO"

        'クエリ生成
        commandTextSb.Append("SELECT ")
        commandTextSb.Append("     hosyousyo_hak_jyky_no ")
        commandTextSb.Append("   , hosyousyo_hak_jyky ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("     m_hosyousyo_hak_jyky ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("     1=1 ")
        commandTextSb.Append(" AND ")
        commandTextSb.Append("     hosyousyo_hak_jyky_no =  " & DBparamHosyousyoHakJykyNo)

        ' パラメータへ設定
        Dim cmdParams() As SqlParameter = _
                {SQLHelper.MakeParam(DBparamHosyousyoHakJykyNo, SqlDbType.Int, 4, code)}

        ' 検索実行
        ds = ExecuteDataset(connStr, _
                             CommandType.Text, _
                             commandTextSb.ToString, _
                             cmdParams _
                             )

        '返却用データテーブルへ格納
        MeisyouDataTable = ds.Tables(0)

        If MeisyouDataTable.Rows.Count = 0 Then
            Return False
        Else
            name = MeisyouDataTable.Rows(0)("hosyousyo_hak_jyky").ToString
        End If

        Return True
    End Function

End Class
