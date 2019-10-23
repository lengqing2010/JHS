Imports System.text
Imports System.Data.SqlClient
Public Class TenbetuSyuuseiSyouhinDataAccess
    Inherits AbsDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "検索モード"
    ''' <summary>
    ''' コントロールの検索モード
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SearchMode
        ''' <summary>
        ''' 登録料
        ''' </summary>
        ''' <remarks></remarks>
        TOUROKU_RYOU = 200
        ''' <summary>
        ''' 販促品初期ツール料
        ''' </summary>
        ''' <remarks></remarks>
        TOOL_RYOU = 210
        ''' <summary>
        ''' FC以外販促品
        ''' </summary>
        ''' <remarks></remarks>
        NOT_FC_RYOU = 220
        ''' <summary>
        ''' FC販促品
        ''' </summary>
        ''' <remarks></remarks>
        FC_RYOU = 230
    End Enum
#End Region

    ''' <summary>
    ''' 検索商品種類
    ''' </summary>
    ''' <remarks></remarks>
    Private searchKoujiMode As Integer

    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="mode"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal mode As SearchMode)
        searchKoujiMode = mode
    End Sub

#Region "店別修正用商品コンボデータ作成"
    ''' <summary>
    ''' コンボボックス設定用の有効な商品レコードを全て取得します
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <param name="blnTorikesi" >（任意）取消データを付加する場合:true</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, ByVal withSpaceRow As Boolean, Optional ByVal withCode As Boolean = True, Optional ByVal blnTorikesi As Boolean = True)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", dt, withSpaceRow, withCode, blnTorikesi)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As SyouhinDataSet.SyouhinTableRow

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    syouhin_cd ")
        commandTextSb.Append("    ,syouhin_mei ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    jhs_sys.m_syouhin WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE ")
        Select Case searchKoujiMode
            Case SearchMode.TOUROKU_RYOU
                commandTextSb.Append(String.Format(" souko_cd = '{0}' ", "200"))
            Case SearchMode.TOOL_RYOU
                commandTextSb.Append(String.Format(" souko_cd = '{0}' ", "210"))
            Case SearchMode.NOT_FC_RYOU
                commandTextSb.Append(String.Format(" souko_cd = '{0}' ", "220"))
            Case SearchMode.FC_RYOU
                commandTextSb.Append(String.Format(" souko_cd = '{0}' ", "230"))
        End Select
        If blnTorikesi Then
            commandTextSb.Append(" AND torikesi = 0 ")
        End If
        commandTextSb.Append(" ORDER BY ")
        commandTextSb.Append("    syouhin_cd")

        Dim test As String = commandTextSb.ToString()

        ' データの取得
        Dim dsSyouhin As New SyouhinDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            dsSyouhin, dsSyouhin.SyouhinTable.TableName)

        Dim syouhinDataTable As SyouhinDataSet.SyouhinTableDataTable = _
                    dsSyouhin.SyouhinTable

        If syouhinDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In syouhinDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.syouhin_cd + ":" + row.syouhin_mei, row.syouhin_cd, dt))
                Else
                    dt.Rows.Add(CreateRow(row.syouhin_mei, row.syouhin_cd, dt))
                End If
            Next

        End If

    End Sub
#End Region
End Class
