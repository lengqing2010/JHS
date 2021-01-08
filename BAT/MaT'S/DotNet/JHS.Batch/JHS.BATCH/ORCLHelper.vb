'クラスモジュール説明
' ┗◎◎┛=====================================================================
'
'  データベースへの参照、更新を容易に行うクラスモジュール(Oracle)
'
' 【モジュール名】
'       ORCLHelper.vb
'
' 【概要】
'		データセットを取得し、データセットの参照、更新を実施する。
'
'       提供するメソッドは次の通りです。
'
'           1) AttachParameters
'				OracleParameter配列をOracleCommandに追加します。
'
'           2) PrepareCommand
'				この方法は開いて（必要に応じて）、接続、命令タイプとパラメータを割り当てます。
'
'           3) ExecuteScalar
'				接続文字列、クエリ、パラメータを渡すことにより参照系のクエリが実行され、
'				戻り値として単一の集計値が返される。
'
'           4) ExecuteNonQuery
'				接続文字列、クエリ、パラメータを渡すことにより更新系のクエリが実行され、
'				戻り値として変更されたレコード数が返される。
'
'           5) ExecuteDataset
'				接続文字列、クエリ、パラメータを渡すことにより参照系のクエリが実行され、
'				戻り値としてDataSetが返される。
'
'           6) FillDataset
'				接続文字列、コマンドタイプ、クエリ、データセット、テーブル名、
'               パラメータを渡すことにより参照系のクエリが実行され、引数のデータセットに
'               テーブル情報を取得します。
'
'			7) UpdateDataset
'				SQLステートメントをデータセットを更新し、データベースを更新する。
'
'			8) MakeParam
'				パラメータを作成して返却します。
'
' 【メソッドの利用方法】
'		クラス定義書(ORCLHelper)を参照してください。
'
' 【設定】
'       参照の追加
'			プロジェクト
'				Oracle.DataAccess
'
' 【更新履歴】
'
' ┗◎◎┛=====================================================================
Imports Oracle.DataAccess.Client
Imports Itis.ApplicationBlocks.ExceptionManagement

''' <summary>
''' 名前空間 ORCLHelper を使用する際の、高性能でベストプラクティスのソリューションを提供するクラスです。
''' </summary>
Public NotInheritable Class ORCLHelper
#Region "private utility methods & constructors"
    Private Sub New()
    End Sub

    ''' <summary>
    ''' OracleParameter配列をOracleCommandに追加します。
    ''' </summary>
    ''' <param name="command">追加するcommandParametersのcommand。</param>
    ''' <param name="commandParameters">commandParameters配列をcommandに追加します。</param>
    Private Shared Sub AttachParameters(ByVal command As OracleCommand, ByVal commandParameters() As OracleParameter)
        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.AttachParameters", _
            command, commandParameters)
        If (command Is Nothing) Then Throw New ArgumentNullException("command")
        If (Not commandParameters Is Nothing) Then
            Dim p As OracleParameter
            For Each p In commandParameters
                If (Not p Is Nothing) Then
                    ' commandParametersのDirectionによって、Valueを設定します。
                    If (p.Direction = ParameterDirection.InputOutput OrElse p.Direction = ParameterDirection.Input) AndAlso p.Value Is Nothing Then
                        p.Value = DBNull.Value
                    End If
                    command.Parameters.Add(p)
                End If
            Next p
        End If
    End Sub ' AttachParameters

    ''' <summary>
    ''' この方法は開いて（必要に応じて）、接続、命令タイプとパラメータを割り当てます。
    ''' </summary>
    ''' <param name="command">準備されるOracleCommand。</param>
    ''' <param name="connection">有効なOracleConnection（このコマンドを実行する）。</param>
    ''' <param name="commandType">ストアドプロシージャ、Transact-SQL 等のコマンドタイプ。</param>
    ''' <param name="commandText">ストアドプロシージャ名、もしくは Transact-SQL コマンド。</param>
    ''' <param name="commandParameters">パラメタライズドクエリ引数の配列。</param>
    Private Shared Sub PrepareCommand(ByVal command As OracleCommand, _
                                      ByVal connection As OracleConnection, _
                                      ByVal commandType As CommandType, _
                                      ByVal commandText As String, _
                                      ByVal commandParameters() As OracleParameter, ByRef mustCloseConnection As Boolean)
        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.PrepareCommand", _
            command, connection, commandType, commandText, commandParameters, mustCloseConnection)

        If (command Is Nothing) Then Throw New ArgumentNullException("command")
        If (commandText Is Nothing OrElse commandText.Length = 0) Then Throw New ArgumentNullException("commandText")

        ' 提供された接続が開いていないならば、それを開けます。
        If connection.State <> ConnectionState.Open Then
            connection.Open()
            mustCloseConnection = True
        Else
            mustCloseConnection = False
        End If

        ' 接続をコマンドと結びつけます。
        command.Connection = connection

        ' 命令テキストをセットします(ストアドプロシージャ名またはSQL声明)。
        command.CommandText = commandText

        ' 命令タイプをセットします。
        command.CommandType = commandType

        ' 提供されるならば、命令パラメータを付けます。
        If Not (commandParameters Is Nothing) Then
            AttachParameters(command, commandParameters)
        End If
        Return
    End Sub ' PrepareCommand

#End Region

#Region "ExecuteDataset"

    ''' <summary>
    ''' 参照系クエリを実行し、DataSet を取得します。
    ''' </summary>
    ''' <param name="connectionString">OracleConnection 用の接続文字列。</param>
    ''' <param name="commandType">ストアドプロシージャ、Transact-SQL 等のコマンドタイプ。</param>
    ''' <param name="commandText">ストアドプロシージャ名、もしくは Transact-SQL コマンド。</param>
    ''' <returns>コマンドの実行結果を格納した DataSet。</returns>
    ''' <remarks>
    ''' このメソッドでは型付データセットを使用できません。
    ''' 型付データセットを使用する場合は、FillDataset メソッドを使用します。
    ''' <example>
    ''' 使用例：
    ''' <code>
    '''   Dim ds As DataSet = ORCLHelper.ExecuteDataset(connString, CommandType.Text, _
    '''       "SELECT * FROM AUTHORS")
    ''' </code></example></remarks>
    Public Overloads Shared Function ExecuteDataset( _
            ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String) _
            As DataSet
        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.ExecuteDataset", _
            connectionString, commandType, commandText)
        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        ' connectionを作成して開きます。最終にDisposeにします。
        Dim connection As OracleConnection = Nothing

        Try
            connection = New OracleConnection(connectionString)
            connection.Open()

            ' ExecuteDataset関数を呼びます。(OracleParameterをNothingにセットします)
            Return ExecuteDataset(connection, commandType, commandText, CType(Nothing, OracleParameter()))
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 参照系クエリを実行し、DataSet を取得します。
    ''' パラメタライズドクエリを使用します。
    ''' </summary>
    ''' <param name="connectionString">OracleConnection 用の接続文字列。</param>
    ''' <param name="commandType">ストアドプロシージャ、Transact-SQL 等のコマンドタイプ。</param>
    ''' <param name="commandText">ストアドプロシージャ名、もしくは Transact-SQL コマンド。</param>
    ''' <param name="commandParameters">パラメタライズドクエリ引数の配列。</param>
    ''' <returns>コマンドの実行結果を格納した DataSet。</returns>
    ''' <remarks>
    ''' このメソッドでは型付データセットを使用できません。
    ''' 型付データセットを使用する場合は、FillDataset メソッドを使用します。
    ''' <example>
    ''' 使用例：
    ''' <code>
    '''   Dim cmdParams() As OracleParameter = _
    '''       {ORCLHelper.MakeParam("STATE", OracleDbType.Varchar2, 2, "CA"), _
    '''       ORCLHelper.MakeParam("CONTRACT", OracleDbType.Int32, 0, 1)}
    '''   Dim ds As Dataset = ORCLHelper.ExecuteDataset(connString, CommandType.Text, _
    '''       "SELECT * FROM AUTHORS WHERE CITY = :CITY AND CONTRACT = :CONTRACT", _
    '''       cmdParams)
    ''' </code></example></remarks>
    Public Overloads Shared Function ExecuteDataset( _
                ByVal connectionString As String, ByVal commandType As CommandType, _
                ByVal commandText As String, ByVal ParamArray commandParameters As OracleParameter()) _
                As DataSet
        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.ExecuteDataset", _
                    connectionString, commandType, commandText, commandParameters)
        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        ' connectionを作成して開きます。最終にDisposeにします。
        Dim connection As OracleConnection = Nothing

        Try
            connection = New OracleConnection(connectionString)
            connection.Open()

            ' ExecuteDataset関数を呼びます。
            Return ExecuteDataset(connection, commandType, commandText, commandParameters)
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 参照系クエリを実行し、DataSet を取得します。
    ''' </summary>
    ''' <param name="connection">OracleConnection</param>
    ''' <param name="commandType">ストアドプロシージャ、Transact-SQL 等のコマンドタイプ。</param>
    ''' <param name="commandText">ストアドプロシージャ名、もしくは Transact-SQL コマンド。</param>
    ''' <param name="commandParameters">パラメタライズドクエリ引数の配列。</param>
    ''' <returns>コマンドの実行結果を格納した DataSet。</returns>
    ''' <remarks>
    ''' このメソッドでは型付データセットを使用できません。
    ''' 型付データセットを使用する場合は、FillDataset メソッドを使用します。
    '''</remarks>
    Private Overloads Shared Function ExecuteDataset(ByVal connection As OracleConnection, _
                                                    ByVal commandType As CommandType, _
                                                    ByVal commandText As String, _
                                                    ByVal ParamArray commandParameters() As OracleParameter) As DataSet
        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.ExecuteDataset", _
                    connection, commandType, commandText, commandParameters)
        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
        ' コマンドを作成します。
        Dim cmd As New OracleCommand
        Dim ds As New DataSet
        Dim dataAdatpter As OracleDataAdapter = Nothing
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, connection, commandType, commandText, commandParameters, mustCloseConnection)

        Try
            ' OracleDataAdapterとDataSetを作成します。
            dataAdatpter = New OracleDataAdapter(cmd)

            ' Fill関数でDataSetに値をセットします。
            dataAdatpter.Fill(ds)


            cmd.Parameters.Clear()
        Finally
            If (Not dataAdatpter Is Nothing) Then dataAdatpter.Dispose()
        End Try
        If (mustCloseConnection) Then connection.Close()

        ' datasetに戻ります。
        Return ds
    End Function ' ExecuteDataset

#End Region

#Region "ExecuteScalar"

    ''' <summary>
    ''' 参照系クエリを実行し、結果セットの最初の行にある最初の列を取得します。
    ''' 残りの列または行は無視されます。
    ''' </summary>
    ''' <param name="connectionString">OracleConnection 用の接続文字列。</param>
    ''' <param name="commandType">ストアドプロシージャ、Transact-SQL 等のコマンドタイプ。</param>
    ''' <param name="commandText">ストアドプロシージャ名、もしくは Transact-SQL コマンド。</param>
    ''' <returns>結果セットの最初の行の最初の列。結果セットが空の場合は、null 参照。</returns>
    ''' <remarks><example>
    ''' 使用例：
    ''' <code>
    '''   Dim orderCount As Integer = CInt(ORCLHelper.ExecuteScalar( _
    '''       connString, CommandType.Text, "SELECT COUNT (*) FROM ORDERS"))
    ''' </code></example></remarks>
    Public Overloads Shared Function ExecuteScalar( _
            ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String) _
            As Object
        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.ExecuteScalar", _
            connectionString, commandType, commandText)

        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        ' connectionを作成して開きます。最終にDisposeにします。
        Dim connection As OracleConnection = Nothing
        Dim rtnObj As Object

        Try
            connection = New OracleConnection(connectionString)
            connection.Open()

            ' ExecuteScalar関数を呼びます。(OracleParameterをNothingにセットします)
            rtnObj = ExecuteScalar( _
            connection, commandType, commandText, CType(Nothing, OracleParameter()))
            Return rtnObj
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 参照系クエリを実行し、結果セットの最初の行にある最初の列を取得します。
    ''' パラメタライズドクエリを使用します。
    ''' 残りの列または行は無視されます。
    ''' </summary>
    ''' <param name="connectionString">OracleConnection 用の接続文字列。</param>
    ''' <param name="commandType">ストアドプロシージャ、Transact-SQL 等のコマンドタイプ。</param>
    ''' <param name="commandText">ストアドプロシージャ名、もしくは Transact-SQL コマンド。</param>
    ''' <param name="commandParameters">パラメタライズドクエリ引数の配列。</param>
    ''' <returns>結果セットの最初の行の最初の列。結果セットが空の場合は、null 参照。</returns>
    ''' <remarks><example>
    ''' 使用例：
    ''' <code>
    '''   Dim cmdParams() As OracleParameter = _
    '''       {ORCLHelper.MakeParam("CITY", OracleDbType.VarChar2, 20, "Oakland"), _
    '''       ORCLHelper.MakeParam("CONTRACT", OracleDbType.Int32, 0, 1)}
    '''   Dim authorsCount As Integer = CInt(ORCLHelper.ExecuteScalar( _
    '''       connString, CommandType.Text, _
    '''       "SELECT COUNT(*) FROM AUTHORS WHERE CITY = :CITY AND CONTRACT = :CONTRACT", _
    '''       cmdParams))
    ''' </code></example></remarks>
    Public Overloads Shared Function ExecuteScalar( _
            ByVal connectionString As String, ByVal commandType As CommandType, _
            ByVal commandText As String, ByVal ParamArray commandParameters() As OracleParameter) _
            As Object
        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.ExecuteScalar", _
            connectionString, commandType, commandText, commandParameters)

        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        ' connectionを作成して開きます。最終にDisposeにします。
        Dim connection As OracleConnection = Nothing
        Dim rtnObj As Object

        Try
            connection = New OracleConnection(connectionString)
            connection.Open()

            ' ExecuteScalar関数を呼びます。(OracleParameterをNothingにセットします)
            rtnObj = ExecuteScalar( _
            connection, commandType, commandText, commandParameters)
            Return rtnObj
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 参照系クエリを実行し、結果セットの最初の行にある最初の列を取得します。
    ''' </summary>
    ''' <param name="connection">OracleConnection。</param>
    ''' <param name="commandType">ストアドプロシージャ、Transact-SQL 等のコマンドタイプ。</param>
    ''' <param name="commandText">ストアドプロシージャ名、もしくは Transact-SQL コマンド。</param>
    ''' <param name="commandParameters">パラメタライズドクエリ引数の配列。</param>
    ''' <returns>結果セットの最初の行の最初の列。結果セットが空の場合は、null 参照。</returns>
    Private Overloads Shared Function ExecuteScalar(ByVal connection As OracleConnection, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String, _
                                                   ByVal ParamArray commandParameters() As OracleParameter) As Object

        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")

        ' コマンドを作成します。
        Dim cmd As New OracleCommand
        Dim retval As Object
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, connection, commandType, commandText, commandParameters, mustCloseConnection)

        ' クエリを実行します。
        retval = cmd.ExecuteScalar()

        ' Parametersの中身をクリアします。
        cmd.Parameters.Clear()

        If (mustCloseConnection) Then connection.Close()

        Return retval

    End Function ' ExecuteScalar

#End Region

#Region "ExecuteNonQuery"

    ''' <summary>
    ''' 更新系クエリを実行し、更新した行数を返します。
    ''' </summary>
    ''' <param name="connectionString">OracleConnection 用の接続文字列。</param>
    ''' <param name="commandType">ストアドプロシージャ、Transact-SQL 等のコマンドタイプ。</param>
    ''' <param name="commandText">ストアドプロシージャ名、もしくは Transact-SQL コマンド。</param>
    ''' <returns>コマンド実行によって影響を及ぼした行数。</returns>
    ''' <remarks><example>
    ''' 使用例：
    ''' <code>
    '''   Dim affectedRowCount As Integer = ORCLHelper.ExecuteNonQuery( _
    '''       connString, CommandType.Text, _
    '''       "UPDATE AUTHORS SET CITY = 'TOKYO' WHERE AU_ID = '998-72-3567'")
    ''' </code></example></remarks>
    Public Overloads Shared Function ExecuteNonQuery( _
            ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String) _
            As Integer

        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.ExecuteNonQuery", _
                            connectionString, commandType, commandText)

        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")

        ' connectionを作成して開きます。最終にDisposeにします。
        Dim connection As OracleConnection = Nothing
        Dim cnt As Integer

        Try
            connection = New OracleConnection(connectionString)

            connection.Open()

            ' ExecuteNonQuery関数を呼びます。
            cnt = ExecuteNonQuery( _
            connection, commandType, commandText, CType(Nothing, OracleParameter()))
            Return cnt
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 更新系クエリを実行し、更新した行数を返します。
    ''' パラメタライズドクエリを使用します。
    ''' </summary>
    ''' <param name="connectionString">OracleConnection 用の接続文字列。</param>
    ''' <param name="commandType">ストアドプロシージャ、Transact-SQL 等のコマンドタイプ。</param>
    ''' <param name="commandText">ストアドプロシージャ名、もしくは Transact-SQL コマンド。</param>
    ''' <param name="commandParameters">パラメタライズドクエリ引数の配列。</param>
    ''' <returns>コマンド実行によって影響を及ぼした行数。</returns>
    ''' <remarks><example>
    ''' 使用例：
    ''' <code>
    '''   Dim cmdParams() As OracleParameter = _ <br/>
    '''       {ORCLHelper.MakeParam("CITY", OracleDbType.Int32, 20, "TOKYO"), _ 
    '''       ORCLHelper.MakeParam("AU_ID", OracleDbType.Int32, 11, "998-72-3567")}
    '''   Dim result As Integer = ORCLHelper.ExecuteNonQuery(connString, CommandType.Text, _
    '''       "UPDATE AUTHORS SET CITY = :CITY WHERE AU_ID = :AU_ID", cmdParams)
    ''' </code></example></remarks>
    Public Overloads Shared Function ExecuteNonQuery( _
            ByVal connectionString As String, ByVal commandType As CommandType, _
            ByVal commandText As String, ByVal ParamArray commandParameters() As OracleParameter) _
            As Integer
        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.ExecuteNonQuery", _
                            connectionString, commandType, commandText, commandParameters)

        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")

        ' connectionを作成して開きます。最終にDisposeにします。
        Dim connection As OracleConnection = Nothing
        Dim cnt As Integer

        Try
            connection = New OracleConnection(connectionString)

            connection.Open()

            ' ExecuteNonQuery関数を呼びます。
            cnt = ExecuteNonQuery( _
            connection, commandType, commandText, commandParameters)
            Return cnt
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 更新系クエリを実行し、更新した行数を返します。
    ''' パラメタライズドクエリを使用します。
    ''' </summary>
    ''' <param name="connection">OracleConnection。</param>
    ''' <param name="commandType">ストアドプロシージャ、Transact-SQL 等のコマンドタイプ。</param>
    ''' <param name="commandText">ストアドプロシージャ名、もしくは Transact-SQL コマンド。</param>
    ''' <param name="commandParameters">パラメタライズドクエリ引数の配列。</param>
    ''' <returns>コマンド実行によって影響を及ぼした行数。</returns>
    ''' <remarks><example>
    ''' 使用例：
    ''' <code>
    '''   Dim cmdParams() As OracleParameter = _ <br/>
    '''       {ORCLHelper.MakeParam("CITY", OracleDbType.Int32, 20, "TOKYO"), _ 
    '''       ORCLHelper.MakeParam("AU_ID", OracleDbType.Int32, 11, "998-72-3567")}
    '''   Dim result As Integer = ORCLHelper.ExecuteNonQuery(connString, CommandType.Text, _
    '''       "UPDATE AUTHORS SET CITY = :CITY WHERE AU_ID = :AU_ID", cmdParams)
    ''' </code></example></remarks>
    Private Overloads Shared Function ExecuteNonQuery(ByVal connection As OracleConnection, _
                                                     ByVal commandType As CommandType, _
                                                     ByVal commandText As String, _
                                                     ByVal ParamArray commandParameters() As OracleParameter) As Integer

        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")

        ' コマンドを作成します。
        Dim cmd As New OracleCommand
        Dim retval As Integer
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, connection, commandType, commandText, commandParameters, mustCloseConnection)

        ' クエリを実行します。
        retval = cmd.ExecuteNonQuery()

        ' Parametersの中身をクリアします。
        cmd.Parameters.Clear()

        If (mustCloseConnection) Then connection.Close()

        Return retval
    End Function ' ExecuteNonQuery

#End Region

#Region "FillDataset"

    ''' <summary>
    ''' 参照系クエリを実行し、結果セットをデータセットに格納します。
    ''' </summary>
    ''' <param name="connectionString">OracleConnection 用の接続文字列。</param>
    ''' <param name="commandType">ストアドプロシージャ、Transact-SQL 等のコマンドタイプ。</param>
    ''' <param name="commandText">ストアドプロシージャ名、もしくは Transact-SQL コマンド。</param>
    ''' <param name="ds">コマンド実行により生成される結果セットを格納するデータセット。</param>
    ''' <param name="tableName">結果セットを格納するデータセット内の DataTable 名。</param>
    ''' <remarks><example>
    ''' 使用例：<br/>
    ''' 型付データセット利用時
    ''' <code>
    '''   ORCLHelper.FillDataset(connString, CommandType.Text, _
    '''       "SELECT * FROM ORDERS", orderDataSet, orderDataSet.Orders.TableName)
    ''' </code><br/>
    ''' 型無データセット利用時
    ''' <code>
    '''   ORCLHelper.FillDataset(connString, CommandType.Text, _
    '''       "SELECT * FROM ORDERS", ds, "Orders")
    ''' </code></example></remarks>
    Public Overloads Shared Sub FillDataset( _
            ByVal connectionString As String, ByVal commandType As CommandType, _
            ByVal commandText As String, ByVal ds As DataSet, ByVal tableName As String)

        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.FillDataset", _
                    connectionString, commandType, commandText, ds, tableName)

        Dim tableNames() As String = New String() {tableName}
        For index As Integer = 0 To tableNames.Length - 1
            If (tableNames(index) Is Nothing OrElse tableNames(index).Length = 0) Then
                Throw New ArgumentException("tableNamesがNothingもしくは値が入っていません。", "tableNames")
            End If
        Next

        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        If (ds Is Nothing) Then Throw New ArgumentNullException("dataSet")

        ' connectionを作成して開きます。最終にDisposeにします。
        Dim connection As OracleConnection = Nothing
        Try
            connection = New OracleConnection(connectionString)

            connection.Open()

            ' FillDataset関数を呼びます。(OracleParameterをNothingにセットします)
            FillDataset(connection, commandType, commandText, ds, tableNames, CType(Nothing, OracleParameter()))
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Sub

    ''' <summary>
    ''' 参照系クエリを実行し、結果セットをデータセットに格納します。
    ''' パラメタライズドクエリを使用します。
    ''' </summary>
    ''' <param name="connectionString">OrclConnection 用の接続文字列。</param>
    ''' <param name="commandType">ストアドプロシージャ、Transact-SQL 等のコマンドタイプ。</param>
    ''' <param name="commandText">ストアドプロシージャ名、もしくは Transact-SQL コマンド。</param>
    ''' <param name="ds">コマンド実行により生成される結果セットを格納するデータセット。</param>
    ''' <param name="tableName">結果セットを格納するデータセット内の DataTable 名。</param>
    ''' <param name="commandParameters">パラメタライズドクエリ引数の配列。</param>
    ''' <remarks><example>
    ''' 使用例：<br/>
    ''' 型付データセット利用時
    ''' <code>
    '''   Dim cmdParams() As OracleParameter = _
    '''       {ORCLHelper.MakeParam("PRODID", OracleDbType.Int32, 0, 24)}
    '''   ORCLHelper.FillDataset(connString, CommandType.Text, _
    '''       "SELECT * FROM ORDERS WHERE PRODID = :PRODID", orderDataSet, _
    '''       orderDataSet.Orders.TableName, cmdParams)
    ''' </code><br/>
    ''' 型無データセット利用時
    ''' <code>
    '''   Dim cmdParams() As OracleParameter = _
    '''       {ORCLHelper.MakeParam("PRODID", OracleDbType.Int32, 0, 24)}
    '''   ORCLHelper.FillDataset(connString, CommandType.Text, _
    '''       "SELECT * FROM ORDERS WHERE PRODID = :PRODID", ds, "Orders", cmdParams)
    ''' </code></example></remarks>
    Public Overloads Shared Sub FillDataset( _
            ByVal connectionString As String, ByVal commandType As CommandType, _
            ByVal commandText As String, ByVal ds As DataSet, ByVal tableName As String, _
            ByVal ParamArray commandParameters() As OracleParameter)

        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.FillDataset", _
                    connectionString, commandType, commandText, ds, tableName, commandParameters)

        '引数5 tableName 
        Dim tableNames() As String = New String() {tableName}

        Dim index As Integer
        For index = 0 To tableNames.Length - 1
            If (tableNames(index) Is Nothing OrElse tableNames(index).Length = 0) Then
                Throw New ArgumentException("tableNamesがNothingもしくは値が入っていません。", "tableNames")
            End If
        Next

        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        If (ds Is Nothing) Then Throw New ArgumentNullException("dataSet")

        ' connectionを作成して開きます。最終にDisposeにします。
        Dim connection As OracleConnection = Nothing
        Try
            connection = New OracleConnection(connectionString)

            connection.Open()

            ' FillDataset関数を呼びます。
            FillDataset(connection, commandType, commandText, ds, tableNames, commandParameters)
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try

    End Sub

    ''' <summary>
    ''' 参照系クエリを実行し、結果セットをデータセットに格納します。
    ''' 複数の DataTable 名を指定します。
    ''' </summary>
    ''' <param name="connectionString">OrclConnection 用の接続文字列。</param>
    ''' <param name="commandType">ストアドプロシージャ、Transact-SQL 等のコマンドタイプ。</param>
    ''' <param name="commandText">ストアドプロシージャ名、もしくは Transact-SQL コマンド。</param>
    ''' <param name="ds">コマンド実行により生成される結果セットを格納するデータセット。</param>
    ''' <param name="tableNames">結果セットを格納するデータセット内の DataTable 名の配列。</param>
    ''' <remarks><example>
    ''' 使用例：<br/>
    ''' 型付データセット利用時
    ''' <code>
    '''   ORCLHelper.FillDataset(connString, CommandType.Text, _
    '''       "SELECT * FROM ORDERS", orderDataSet, New String() {orderDataSet.Orders.TableName})
    ''' </code><br/>
    ''' 型無データセット利用時
    ''' <code>
    '''   ORCLHelper.FillDataset(connString, CommandType.Text, _
    '''       "SELECT * FROM ORDERS", ds, New String() {"Orders"})
    ''' </code></example></remarks>
    Public Overloads Shared Sub FillDataset( _
            ByVal connectionString As String, ByVal commandType As CommandType, _
            ByVal commandText As String, ByVal ds As DataSet, ByVal tableNames() As String)

        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.FillDataset", _
                            connectionString, commandType, commandText, ds, tableNames)

        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        If (ds Is Nothing) Then Throw New ArgumentNullException("dataSet")

        ' connectionを作成して開きます。最終にDisposeにします。
        Dim connection As OracleConnection = Nothing
        Try
            connection = New OracleConnection(connectionString)

            connection.Open()

            ' FillDataset関数を呼びます。
            FillDataset(connection, commandType, commandText, ds, tableNames, CType(Nothing, OracleParameter()))
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Sub

    ''' <summary>
    ''' 参照系クエリを実行し、結果セットをデータセットに格納します。
    ''' 複数の DataTable 名を指定します。
    ''' パラメタライズドクエリを使用します。
    ''' </summary>
    ''' <param name="connectionString">OrclConnection 用の接続文字列。</param>
    ''' <param name="commandType">ストアドプロシージャ、Transact-SQL 等のコマンドタイプ。</param>
    ''' <param name="commandText">ストアドプロシージャ名、もしくは Transact-SQL コマンド。</param>
    ''' <param name="ds">コマンド実行により生成される結果セットを格納するデータセット。</param>
    ''' <param name="tableNames">結果セットを格納するデータセット内の DataTable 名の配列。</param>
    ''' <param name="commandParameters">パラメタライズドクエリ引数の配列。</param>
    ''' <remarks><example>
    ''' 使用例：<br/>
    ''' 型付データセット利用時
    ''' <code>
    '''   Dim cmdParams() As OracleParameter = _
    '''       {ORCLHelper.MakeParam("PRODID", OracleDbType.Int32, 0, 24)}
    '''   ORCLHelper.FillDataset(connString, CommandType.Text, _
    '''       "SELECT * FROM ORDERS WHERE PRODID = :PRODID", orderDataSet, _
    '''       New String() {orderDataSet.Orders.TableName}, cmdParams)
    ''' </code><br/>
    ''' 型無データセット利用時
    ''' <code>
    '''   Dim cmdParams() As OracleParameter = _
    '''       {ORCLHelper.MakeParam("PRODID", OracleDbType.Int32, 0, 24)}
    '''   ORCLHelper.FillDataset(connString, CommandType.Text, _
    '''       "SELECT * FROM ORDERS WHERE PRODID = :PRODID", ds, _
    '''       New String() {"Orders"}, cmdParams)
    ''' </code></example></remarks>
    Public Overloads Shared Sub FillDataset( _
            ByVal connectionString As String, ByVal commandType As CommandType, _
            ByVal commandText As String, ByVal ds As DataSet, ByVal tableNames() As String, _
            ByVal ParamArray commandParameters() As OracleParameter)

        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.FillDataset", _
                            connectionString, commandType, commandText, ds, tableNames, commandParameters)

        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        If (ds Is Nothing) Then Throw New ArgumentNullException("dataSet")

        ' connectionを作成して開きます。最終にDisposeにします。
        Dim connection As OracleConnection = Nothing
        Try
            connection = New OracleConnection(connectionString)

            connection.Open()

            ' FillDataset関数を呼びます。
            FillDataset(connection, commandType, commandText, ds, tableNames, commandParameters)
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Sub

    ''' <summary>
    ''' 参照系クエリを実行し、結果セットをデータセットに格納します。
    ''' </summary>
    ''' <param name="connection">OracleConnection</param>
    ''' <param name="commandType">ストアドプロシージャ、Transact-SQL 等のコマンドタイプ。</param>
    ''' <param name="commandText">ストアドプロシージャ名、もしくは Transact-SQL コマンド。</param>
    ''' <param name="dataSet">コマンド実行により生成される結果セットを格納するデータセット。</param>
    ''' <param name="tableNames">結果セットを格納するデータセット内の DataTable 名。</param>
    Private Overloads Shared Sub FillDataset(ByVal connection As OracleConnection, ByVal commandType As CommandType, _
        ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames() As String, _
        ByVal ParamArray commandParameters() As OracleParameter)

        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.FillDataset", _
                            connection, commandType, commandText, dataSet, tableNames, commandParameters)

        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
        If (dataSet Is Nothing) Then Throw New ArgumentNullException("dataSet")

        ' OracleCommandを作成して開きます。最終にDisposeにします。
        Dim command As New OracleCommand

        Dim mustCloseConnection As Boolean = False
        PrepareCommand(command, connection, commandType, commandText, commandParameters, mustCloseConnection)

        ' OracleDataAdapterとDataSetを作成します。
        Dim dataAdapter As OracleDataAdapter = New OracleDataAdapter(command)

        Try
            ' テーブルマッピングを追加します。
            If Not tableNames Is Nothing AndAlso tableNames.Length > 0 Then

                Dim tableName As String = "Table"
                Dim index As Integer

                For index = 0 To tableNames.Length - 1
                    If (tableNames(index) Is Nothing OrElse tableNames(index).Length = 0) Then Throw New ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames")
                    dataAdapter.TableMappings.Add(tableName, tableNames(index))
                    tableName = "Table" & (index + 1).ToString()
                Next
            End If

            ' Fill関数でDataSetに値をセットします。
            dataAdapter.Fill(dataSet)

            ' Parametersの中身をクリアします。
            command.Parameters.Clear()
        Finally
            If (Not dataAdapter Is Nothing) Then dataAdapter.Dispose()
        End Try

        If (mustCloseConnection) Then connection.Close()

    End Sub

#End Region

#Region "UpdateDataset"

    ''' <summary>
    ''' データセット内の指定の DataTable に対する挿入行、更新行、または削除行に対して、
    ''' SELECT 文から自動的に生成される INSERT、UPDATE、または DELETE 文を実行します。
    ''' </summary>
    ''' <param name="connectionString">OrclConnection 用の接続文字列。</param>
    ''' <param name="selectText">データセットを取得しる際に使用した SELECT 文。</param>
    ''' <param name="ds">編集（挿入、更新、削除）操作を行った、更新元のデータセット。</param>
    ''' <param name="tableName">データセット内の更新元対象の DataTable 名。</param>
    ''' <returns>データセット内で正常に更新された行の数。</returns>
    ''' <remarks>
    ''' 第２引数に指定する SELECT 文は、以下を満たす必要があります。<br/>
    ''' ・少なくとも 1 つの主キーまたは一意の列を返す文であること。<br/>
    ''' ・対象のテーブルが単一のテーブルであること。(複数結合したテーブルではないこと。)<br/>
    ''' ・リレーションシップのないテーブルであること。<br/>
    ''' ・列名またはテーブル名にスペース、ピリオド (.)、疑問符 (?)、引用符、その他の英数字以外の特殊文字が含まれていないこと。
    ''' <example>
    ''' 使用例：
    ''' <code>
    '''   Dim updateCount As Integer = ORCLHelper.UpdateDataset(connString, _
    '''       "SELECT * FROM EMPLOYEE", employeeDS, employeeDS.Employee.TableName)
    ''' </code></example></remarks>
    Public Overloads Shared Function UpdateDataset( _
            ByVal connectionString As String, ByVal selectText As String, _
            ByVal ds As DataSet, ByVal tableName As String) _
            As Integer

        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.UpdateDataset", _
                            connectionString, selectText, ds, tableName)

        '戻り値
        Dim rtnInt As Integer

        '接続用オブジェクトの作成
        Dim conn As OracleConnection = New OracleConnection
        conn.ConnectionString = connectionString

        'SELECT用コマンド・オブジェクト作成
        Dim selectCmd As OracleCommand = New OracleCommand
        selectCmd.Connection = conn
        selectCmd.CommandText = selectText

        'データアダプタの作成
        Dim da As OracleDataAdapter = New OracleDataAdapter
        Try
            da.SelectCommand = selectCmd

            'コマンド自動生成
            Dim cb As OracleCommandBuilder = New OracleCommandBuilder(da)

            'データベースの更新
            rtnInt = da.Update(ds, tableName)

            'データセットの更新内容をコミットする
            ds.AcceptChanges()
        Finally
            If (Not da Is Nothing) Then da.Dispose()
            If (Not conn Is Nothing) Then conn.Dispose()
        End Try

        Return rtnInt
    End Function

    ''' <summary>
    ''' データセット内の指定の DataTable に対する挿入行、更新行、または削除行に対して、
    ''' 指定した INSERT、UPDATE、または DELETE 文を実行します。
    ''' </summary>
    ''' <param name="insertCommand">INSERT 用の、ストアドプロシージャもしくは Transact-SQL コマンド。</param>
    ''' <param name="updateCommand">UPDATE 用の、ストアドプロシージャもしくは Transact-SQL コマンド。</param>
    ''' <param name="deleteCommand">DELETE 用の、ストアドプロシージャもしくは Transact-SQL コマンド。</param>
    ''' <param name="ds">編集（挿入、更新、削除）操作を行った、更新元のデータセット。</param>
    ''' <param name="tableName">データセット内の更新元対象の DataTable 名。</param>
    ''' <remarks><example>
    ''' 使用例：
    ''' <code>
    '''   ORCLHelper.UpdateDataset(insertCommand, deleteCommand, updateCommand, _
    '''       ordersDataSet, ordersDataSet.Orders.TableName)
    ''' </code></example></remarks>
    Public Overloads Shared Sub UpdateDataset( _
            ByVal insertCommand As OracleCommand, ByVal updateCommand As OracleCommand, _
            ByVal deleteCommand As OracleCommand, ByVal ds As DataSet, ByVal tableName As String)

        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.UpdateDataset", _
                            insertCommand, updateCommand, deleteCommand, ds, tableName)

        If (insertCommand Is Nothing) Then Throw New ArgumentNullException("insertCommand")
        If (deleteCommand Is Nothing) Then Throw New ArgumentNullException("deleteCommand")
        If (updateCommand Is Nothing) Then Throw New ArgumentNullException("updateCommand")
        If (ds Is Nothing) Then Throw New ArgumentNullException("dataSet")
        If (tableName Is Nothing OrElse tableName.Length = 0) Then Throw New ArgumentNullException("tableName")

        ' データアダプタの作成
        Dim dataAdapter As New OracleDataAdapter
        Try
            ' データアダプター命令をセットします。
            dataAdapter.UpdateCommand = updateCommand
            dataAdapter.InsertCommand = insertCommand
            dataAdapter.DeleteCommand = deleteCommand

            ' データベースの更新
            dataAdapter.Update(ds, tableName)

            ' データセットの更新内容をコミットします。
            ds.AcceptChanges()
        Finally
            If (Not dataAdapter Is Nothing) Then dataAdapter.Dispose()
        End Try
    End Sub

#End Region

#Region "MakeParam"

    ''' <summary>
    ''' パラメタライズドクエリ引数を生成します。
    ''' </summary>
    ''' <param name="parameterName">割り当てるパラメータの名前。</param>
    ''' <param name="dbType">OracleDbType 値の 1 つ。</param>
    ''' <param name="size">パラメータの長さ。</param>
    ''' <param name="value">値を示す Object。</param>
    ''' <returns>パラメタライズドクエリ引数。</returns>
    ''' <remarks>
    ''' 第３引数の size は、以下のように指定します。<br/>
    ''' ・型（第２引数に指定する dbType）がバイナリ型と文字列型の場合は、適切なサイズを指定します。
    ''' ・それ以外の型の場合は、設定された値は無視されますが、統一的に 0 を設定してください。
    ''' </remarks>
    Public Shared Function MakeParam( _
            ByVal parameterName As String, ByVal dbType As OracleDbType, _
            ByVal size As Integer, ByVal value As Object) _
            As OracleParameter
        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.MakeParam", _
                            parameterName, dbType, size, value)

        Dim work As OracleParameter = New OracleParameter(parameterName, dbType, size)
        work.Value = value
        Return work
    End Function

#End Region
End Class
