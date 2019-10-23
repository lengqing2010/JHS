Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' TBL_Mﾃﾞｰﾀ区分への接続クラスです
''' </summary>
''' <remarks></remarks>
Public Class KubunDataAccess
    Inherits AbsDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' 区分を引数に区分レコードを取得します
    ''' </summary>
    ''' <param name="kubun"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataBy(ByVal kubun As String, _
                              ByRef torikeshi As Integer, _
                              ByRef kubunMei As String) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDataBy", _
                                            kubun, _
                                            torikeshi, _
                                            kubunMei)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString

        ' パラメータ
        Const paramKubun As String = "@KUBUN"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT kbn,torikesi,kbn_mei")
        commandTextSb.Append("  FROM m_data_kbn WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE kbn = " & paramKubun)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, kubun)}

        ' データの取得
        Dim KubunDataSet As New KubunDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            KubunDataSet, KubunDataSet.DataKbnTable.TableName, commandParameters)

        Dim kubunTable As KubunDataSet.DataKbnTableDataTable = KubunDataSet.DataKbnTable

        If kubunTable.Count = 0 Then
            Debug.WriteLine("取得出来ませんでした")
            Return False
        Else
            Dim row As KubunDataSet.DataKbnTableRow = kubunTable(0)
            torikeshi = row.torikesi
            kubunMei = row.kbn_mei
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
        Dim row As KubunDataSet.DataKbnTableRow

        commandTextSb.Append("SELECT kbn,torikesi,kbn_mei")
        commandTextSb.Append("  FROM m_data_kbn WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE torikesi = 0 ORDER BY kbn")

        ' データの取得
        Dim kubunDataSet As New KubunDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            kubunDataSet, kubunDataSet.DataKbnTable.TableName)

        Dim kubunDataTable As KubunDataSet.DataKbnTableDataTable = _
                    kubunDataSet.DataKbnTable

        If kubunDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In kubunDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.kbn + ":" + row.kbn_mei, row.kbn, dt))
                Else
                    dt.Rows.Add(CreateRow(row.kbn_mei, row.kbn, dt))
                End If
            Next

        End If

    End Sub

    ''' <summary>
    ''' 引数(区分)に紐付くレコードを取得する
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <returns>引数(区分)に紐付くレコード情報</returns>
    ''' <remarks></remarks>
    Public Function getKubunRecord(ByVal strKubun As String) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getKubunRecord" _
                                                    , strKubun _
                                                    )
        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KUBUN", SqlDbType.Char, 1, strKubun)}

        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append("SELECT ")
        cmdTextSb.Append("       kbn ")
        cmdTextSb.Append("       , torikesi ")
        cmdTextSb.Append("       , kbn_mei ")
        cmdTextSb.Append("       , genka_master_hisansyou_flg ")
        cmdTextSb.Append("       , add_login_user_id ")
        cmdTextSb.Append("       , add_datetime ")
        cmdTextSb.Append("       , upd_login_user_id ")
        cmdTextSb.Append("       , upd_datetime ")
        cmdTextSb.Append("       FROM m_data_kbn ")
        cmdTextSb.Append("       WHERE kbn = @KUBUN")

        ' データ取得＆返却
        Dim cmnDtAcc As New CmnDataAccess
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

End Class
