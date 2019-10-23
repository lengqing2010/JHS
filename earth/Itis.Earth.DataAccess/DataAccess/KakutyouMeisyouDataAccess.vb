Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 拡張名称マスタ情報の取得用クラスです
''' </summary>
''' <remarks>本クラスで名称情報を取得する場合はインスタンス時に名称種別プロパティに情報を設定してください</remarks>
Public Class KakutyouMeisyouDataAccess
    Inherits AbsDataAccess

    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' コネクションストリングを取得
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' コンストラクタ
    ''' <newpara>※名称種別を指定すること</newpara>
    ''' </summary>
    ''' <param name="intVal">EarthConst.emKtMeisyouTypeを使用すること</param>
    ''' <remarks>※EarthConst値を指定すること</remarks>
    Public Sub New(ByVal intVal As EarthConst.emKtMeisyouType)
        Me.pIntMeisyouSyubetu = intVal
    End Sub

    '''' <summary>
    '''' 名称種別
    '''' </summary>
    '''' <remarks></remarks>
    Private pIntMeisyouSyubetu As Integer = 0

#Region "SQL/パラメータ変数"
    Const pStrPrmCode As String = "@CODE"
    Const pStrPrmSyubetu As String = "@SYUBETU"
    Const pStrHannyouCd As String = "hannyou_cd"
    Const pStrHannyouNo As String = "hannyou_no"
#End Region

    ''' <summary>
    ''' ｺｰﾄﾞを引数に名称を取得します
    ''' </summary>
    ''' <param name="code">コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMeisyou( _
                                ByVal code As String _
                                , ByRef name As String _
                                ) As Boolean

        Dim ds As New DataSet
        Dim MeisyouDataTable As New DataTable
        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append(" 	meisyou_syubetu")
        commandTextSb.Append(" 	,code")
        commandTextSb.Append(" 	,meisyou")
        commandTextSb.Append(" 	,hyouji_jyun ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append(" 	m_kakutyou_meisyou WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append(" 	meisyou_syubetu = " & pStrPrmSyubetu)
        commandTextSb.Append(" AND")
        commandTextSb.Append(" 	code = " & pStrPrmCode)
        commandTextSb.Append(" ORDER BY ")
        commandTextSb.Append(" 	hyouji_jyun ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(pStrPrmSyubetu, SqlDbType.Int, 4, pIntMeisyouSyubetu), _
             SQLHelper.MakeParam(pStrPrmCode, SqlDbType.VarChar, 10, code)}

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
            name = MeisyouDataTable.Rows(0)("meisyou").ToString
        End If

        Return True

    End Function

    ''' <summary>
    ''' コンボボックス設定用の有効な名称テーブルレコードを全て取得します(※拡張名称M専用)<br/>
    ''' </summary>
    ''' <param name="dt" >データテーブル</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetKtMeisyouDropdownData(ByRef dt As DataTable, _
                                         ByVal withSpaceRow As Boolean, _
                                         Optional ByVal withCode As Boolean = True, _
                                         Optional ByVal blnTorikesi As Boolean = True)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKtMeisyouDropdownData", dt, withSpaceRow, withCode)


        Dim ds As New DataSet
        Dim MeisyouDataTable As New DataTable
        Dim commandTextSb As New StringBuilder()
        Dim row As DataRow

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append(" 	code ")
        commandTextSb.Append(" 	,meisyou ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append(" 	m_kakutyou_meisyou WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append(" 	meisyou_syubetu = " & pStrPrmSyubetu)
        commandTextSb.Append(" ORDER BY ")
        commandTextSb.Append(" 	hyouji_jyun ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(pStrPrmSyubetu, SqlDbType.Int, 4, pIntMeisyouSyubetu)}

        ' 検索実行
        ds = ExecuteDataset(connStr, _
                             CommandType.Text, _
                             commandTextSb.ToString, _
                             commandParameters _
                             )

        'データテーブルへ格納
        MeisyouDataTable = ds.Tables(0)

        If MeisyouDataTable.Rows.Count > 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In MeisyouDataTable.Rows
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row("code").ToString + ":" + row("meisyou").ToString, row("code").ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row("meisyou").ToString, row("code").ToString, dt))
                End If
            Next

        End If

    End Sub

    ''' <summary>
    ''' コンボボックス設定用の有効な名称テーブルレコードを全て取得します(※拡張名称M専用、表示項目をパラメータで指定)
    ''' </summary>
    ''' <param name="dt" >データテーブル</param>
    ''' <param name="type">拡張名称Mドロップダウンタイプ</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <param name="blnTorikesi">（任意）拡張名称Mの項目に取消は存在しない</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetKtMeisyouHannyouDropdownData(ByRef dt As DataTable, _
                                                         ByVal type As EarthEnum.emKtMeisyouType, _
                                                         ByVal withSpaceRow As Boolean, _
                                                         Optional ByVal withCode As Boolean = True, _
                                                         Optional ByVal blnTorikesi As Boolean = True)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKtMeisyouHannyouDropdownData", _
                                                    dt, _
                                                    type, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    blnTorikesi)

        Dim ds As New DataSet
        Dim MeisyouDataTable As New DataTable
        Dim commandTextSb As New StringBuilder()
        Dim row As DataRow
        Dim strItem As String = String.Empty
        Dim strNullVal As String = String.Empty

        'パラメータによって表示項目切り替え
        If type = EarthEnum.emKtMeisyouType.HannyouCd Then
            strItem = pStrHannyouCd
            strNullVal = "''"
        ElseIf type = EarthEnum.emKtMeisyouType.HannyouNo Then
            strItem = pStrHannyouNo
            strNullVal = "0"
        Else
            dt = Nothing
            Exit Sub
        End If

        '****************
        '* SELECT項目
        '****************
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" 	code ")
        commandTextSb.AppendLine(" 	,ISNULL(" & strItem & ", " & strNullVal & ")AS " & strItem)
        
        '****************
        '* TABLE項目
        '****************
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine(" 	m_kakutyou_meisyou WITH (READCOMMITTED) ")

        '****************
        '* WHERE項目
        '****************
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" 	meisyou_syubetu = " & pStrPrmSyubetu)

        '****************
        '* ORDER BY項目
        '****************
        commandTextSb.AppendLine(" ORDER BY ")
        commandTextSb.AppendLine(" 	hyouji_jyun ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(pStrPrmSyubetu, SqlDbType.Int, 4, pIntMeisyouSyubetu)}

        ' 検索実行
        ds = ExecuteDataset(connStr, _
                             CommandType.Text, _
                             commandTextSb.ToString, _
                             commandParameters _
                             )

        'データテーブルへ格納
        MeisyouDataTable = ds.Tables(0)

        If MeisyouDataTable.Rows.Count > 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In MeisyouDataTable.Rows
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row("code").ToString + ":" + row(strItem).ToString, row("code").ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row(strItem).ToString, row("code").ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
