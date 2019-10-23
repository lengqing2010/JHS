Imports System.text
Imports System.Data.SqlClient


''' <summary>
''' 調査方法情報の取得用クラスです
''' </summary>
''' <remarks></remarks>
Public Class TyousahouhouDataAccess
    Inherits AbsDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' コネクションストリングを取得
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 調査方法ｺｰﾄﾞを引数に名称を取得します
    ''' </summary>
    ''' <param name="no"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataBy(ByVal no As Integer, _
                              ByRef name As String) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDataBy", _
                                                    no, _
                                                    name)

        ' パラメータ
        Const paramNo As String = "@CHOUSANO"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT tys_houhou_no,tys_houhou_mei,tys_houhou_mei_ryaku ")
        commandTextSb.Append("  FROM m_tyousahouhou WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE tys_houhou_no = " + paramNo)
        commandTextSb.Append("  ORDER BY tys_houhou_no ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramNo, SqlDbType.Int, 0, no)}

        ' データの取得
        Dim TyousahouhouDataSet As New TyousahouhouDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            TyousahouhouDataSet, TyousahouhouDataSet.TyousahouhouTable.TableName, commandParameters)

        Dim TyousahouhouTable As TyousahouhouDataSet.TyousahouhouTableDataTable = TyousahouhouDataSet.TyousahouhouTable

        If TyousahouhouTable.Count = 0 Then
            Debug.WriteLine("取得出来ませんでした")
            Return False
        Else
            Dim row As TyousahouhouDataSet.TyousahouhouTableRow = TyousahouhouTable(0)
            name = row.tys_houhou_mei
        End If

        Return True

    End Function

    ''' <summary>
    ''' コンボボックス設定用の調査方法レコードを全て取得します
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
        Dim row As TyousahouhouDataSet.TyousahouhouTableRow

        commandTextSb.Append("SELECT tys_houhou_no,tys_houhou_mei,tys_houhou_mei_ryaku ")
        commandTextSb.Append("  FROM m_tyousahouhou WITH (READCOMMITTED) ")

        '取消の場合はDDLに含めない
        commandTextSb.Append("  WHERE")
        commandTextSb.Append("  torikesi = 0")

        commandTextSb.Append("  ORDER BY tys_houhou_no ")

        ' データの取得
        Dim TyousahouhouDataSet As New TyousahouhouDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            TyousahouhouDataSet, TyousahouhouDataSet.TyousahouhouTable.TableName)

        Dim TyousahouhouDataTable As TyousahouhouDataSet.TyousahouhouTableDataTable = _
                    TyousahouhouDataSet.TyousahouhouTable

        If TyousahouhouDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In TyousahouhouDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.tys_houhou_no.ToString + ":" + row.tys_houhou_mei, row.tys_houhou_no.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.tys_houhou_mei, row.tys_houhou_no.ToString, dt))
                End If
            Next

        End If

    End Sub

    ''' <summary>
    ''' 引数(調査方法NO)に紐付くレコードを取得する
    ''' </summary>
    ''' <param name="intTysHouhouNo">調査方法NO</param>
    ''' <returns>引数(調査方法NO)に紐付くレコード情報</returns>
    ''' <remarks></remarks>
    Public Function getTyousahouhouRecord(ByVal intTysHouhouNo As Integer) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getTyousahouhouRecord" _
                                                    , intTysHouhouNo _
                                                    )

        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@TYOUSAHOUHOUNO", SqlDbType.Int, 4, intTysHouhouNo)}

        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append("SELECT ")
        cmdTextSb.Append("       tys_houhou_no ")
        cmdTextSb.Append("       , torikesi ")
        cmdTextSb.Append("       , tys_houhou_mei_ryaku ")
        cmdTextSb.Append("       , tys_houhou_mei ")
        cmdTextSb.Append("       , genka_settei_fuyou_flg ")
        cmdTextSb.Append("       , kakaku_settei_fuyou_flg ")
        cmdTextSb.Append("       , add_login_user_id ")
        cmdTextSb.Append("       , add_datetime ")
        cmdTextSb.Append("       , upd_login_user_id ")
        cmdTextSb.Append("       , upd_datetime")
        cmdTextSb.Append("       FROM m_tyousahouhou ")
        cmdTextSb.Append("       WHERE tys_houhou_no = @TYOUSAHOUHOUNO")

        ' データ取得＆返却
        Dim cmnDtAcc As New CmnDataAccess
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

End Class
