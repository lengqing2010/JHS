Imports System.Data.SqlClient
''' <summary>
''' 画面やDBのテーブルに直接紐づかない共通的な処理を行うDataAccessクラスです。
''' </summary>
''' <remarks></remarks>
Public Class CmnDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "プロパティ"

    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private conStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' データセット
    ''' </summary>
    ''' <remarks></remarks>
    Private pDataSet As DataSet
    ''' <summary>
    ''' データセット
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property dsCmn() As DataSet
        Get
            Return pDataSet
        End Get
        Set(ByVal value As DataSet)
            pDataSet = value
        End Set
    End Property

    ''' <summary>
    ''' データセット
    ''' </summary>
    ''' <remarks></remarks>
    Private pDataTable As DataTable
    ''' <summary>
    ''' データセット
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property dtCmn() As DataTable
        Get
            Return pDataTable
        End Get
        Set(ByVal value As DataTable)
            pDataTable = value
        End Set
    End Property

#End Region

    '初期処理(インスタンス化)
    Private Sub init()
        dsCmn = New DataSet
        dtCmn = New DataTable
    End Sub

    ''' <summary>
    ''' SQLクエリの検索結果でデータテーブルを取得する
    ''' </summary>
    ''' <param name="strSql">SQLクエリ</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks>パラメータなし</remarks>
    Public Function getDataTable(ByVal strSql As String) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getDataTable", strSql)

        '初期処理
        Me.init()

        ' 検索実行
        dsCmn = ExecuteDataset(conStr, _
                             CommandType.Text, _
                             strSql _
                             )

        '返却用データテーブルへ格納
        dtCmn = dsCmn.Tables(0)

        Return dtCmn
    End Function

    ''' <summary>
    ''' SQLクエリの検索結果でデータテーブルを取得する
    ''' </summary>
    ''' <param name="strSql">SQLクエリ</param>
    ''' <param name="sqlParams">SQLパラメータ</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks>パラメータあり</remarks>
    Public Function getDataTable(ByVal strSql As String, ByVal sqlParams() As SqlParameter) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getDataTable", strSql, sqlParams)

        '初期処理
        Me.init()

        ' 検索実行
        dsCmn = ExecuteDataset(conStr, _
                             CommandType.Text, _
                             strSql, _
                             sqlParams _
                             )

        '返却用データテーブルへ格納
        dtCmn = dsCmn.Tables(0)

        Return dtCmn
    End Function

    ''' <summary>
    ''' データセットのデータテーブルにSQLクエリの検索結果をセットし、データテーブルを取得する
    ''' </summary>
    ''' <param name="strSql">SQLクエリ</param>
    ''' <param name="dsCmn">データセット</param>
    ''' <param name="strTblName">テーブル名</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks>パラメータなし</remarks>
    Public Function getDataTable(ByVal strSql As String, ByRef dsCmn As DataSet, ByVal strTblName As String)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getDataTable", strSql, dsCmn, strTblName)

        ' 検索実行
        SQLHelper.FillDataset(conStr, CommandType.Text, strSql, dsCmn, strTblName)

        Return dsCmn.Tables(strTblName)
    End Function

    ''' <summary>
    ''' データセットのデータテーブルにSQLクエリの検索結果をセットし、データテーブルを取得する
    ''' </summary>
    ''' <param name="strSql">SQLクエリ</param>
    ''' <param name="dsCmn">データセット</param>
    ''' <param name="strTblName">テーブル名</param>
    ''' <param name="sqlParams">SQLパラメータ</param>
    ''' <remarks>パラメータあり</remarks>
    Public Function getDataTable(ByVal strSql As String, ByRef dsCmn As DataSet, ByVal strTblName As String, ByVal sqlParams() As SqlParameter)
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getDataTable", strSql, dsCmn, strTblName, sqlParams)

        ' 検索実行
        SQLHelper.FillDataset(conStr, CommandType.Text, strSql, dsCmn, strTblName, sqlParams)

        Return dsCmn.Tables(strTblName)
    End Function

    ''' <summary>
    ''' SQLパラメータを結合します
    ''' </summary>
    ''' <param name="AddToParams">結合されるSQLパラメータ</param>
    ''' <param name="AddFromParams">結合するSQLパラメータ</param>
    ''' <remarks></remarks>
    Public Sub AddSqlParameter(ByRef AddToParams() As SqlParameter, ByVal AddFromParams() As SqlParameter)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".AddSqlParameter" _
                                                    , AddToParams _
                                                    , AddFromParams)
        Dim intArrLength As Integer

        intArrLength = AddToParams.Length
        ReDim Preserve AddToParams(intArrLength + AddFromParams.Length - 1)

        AddFromParams.CopyTo(AddToParams, intArrLength)

    End Sub

End Class
