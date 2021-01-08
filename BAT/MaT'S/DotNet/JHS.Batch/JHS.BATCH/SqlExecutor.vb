Imports System.Data.SqlClient
''' <summary>DB操作機能</summary>
Public Class SqlExecutor

    ''' <summary>DB接続</summary>
    Private mConnection As SqlConnection
    Private mDefaultCommandTimeout As Integer
    Private mTransaction As SqlTransaction

    ''' <summary>コンストラクタ</summary>
    ''' <param name="strConnection">DB接続</param>
    Public Sub New(ByVal strConnection As String)
        Me.mConnection = New SqlConnection(strConnection)
        Me.mDefaultCommandTimeout = 36000
    End Sub

    Public Sub New(ByVal strConnection As String, ByVal defaultCommandTimeout As Integer)
        Me.mConnection = New SqlConnection(strConnection)
        Me.mDefaultCommandTimeout = defaultCommandTimeout
    End Sub

    Public Sub Open(Optional ByVal transactionFlg As Boolean = False)
        Me.mConnection.Open()

        If transactionFlg Then
            mTransaction = Me.mConnection.BeginTransaction
        Else
            mTransaction = Nothing
        End If
    End Sub

    Public Sub Close()
        Me.mConnection.Close()
    End Sub

    Public Sub Dispose()
        Me.mConnection.Dispose()
        Me.mConnection = Nothing

        If mTransaction IsNot Nothing Then
            mTransaction.Dispose()
            mTransaction = Nothing
        End If
    End Sub

    Private mLastErrorCommandText As String = Nothing

    Public ReadOnly Property BaseConnection() As SqlConnection
        Get
            Return Me.mConnection
        End Get
    End Property

    Public ReadOnly Property LastErrorCommandText() As String
        Get
            Return Me.mLastErrorCommandText
        End Get
    End Property

    Public Property DefaultCommandTimeout() As Integer
        Get
            Return Me.mDefaultCommandTimeout
        End Get
        Set(ByVal value As Integer)
            Me.mDefaultCommandTImeout = value
        End Set
    End Property

    Public Sub Commit()
        If mTransaction IsNot Nothing Then
            mTransaction.Commit()
        End If
    End Sub

    Public Sub Rollback()
        If mTransaction IsNot Nothing Then
            mTransaction.Rollback()
        End If
    End Sub


#Region "コマンド実行"
    ''' <summary>コマンドを実行して結果をDataTableに格納</summary>




    Public Function ExecuteDataTable(ByVal commandType As CommandType, ByVal commandText As String, ByVal ParamArray params() As SqlParameter) As DataTable
        Return Me.ExecuteDataTable(CommandType, commandText, Me.mDefaultCommandTimeout, New DataTable, params)
    End Function

    Public Function ExecuteDataTable(ByVal commandType As CommandType, ByVal commandText As String, ByVal commandTimeout As Integer, ByVal ParamArray params() As SqlParameter) As DataTable
        Return Me.ExecuteDataTable(commandType, commandText, commandTimeout, New DataTable, params)
    End Function

    Public Function ExecuteDataTable(ByVal commandType As CommandType, ByVal commandText As String, ByVal table As DataTable, ByVal ParamArray params() As SqlParameter) As DataTable
        Return Me.ExecuteDataTable(commandType, commandText, Me.mDefaultCommandTimeout, table, params)
    End Function
    Public Function ExecuteDataTable(ByVal commandType As CommandType, ByVal commandText As String, ByVal commandTimeout As Integer, ByVal table As DataTable, ByVal ParamArray params() As SqlParameter) As DataTable
        Dim reader As SqlDataReader = Me.ExecuteReader(commandType, commandText, commandTimeout, params)
        table.Load(reader)
        reader.Close()
        Return table
    End Function

    ''' <summary>コマンドを実行してDataReaderを返す</summary>
    ''' <param name="commandType"></param>
    ''' <param name="commandText"></param>
    ''' <param name="params"></param>
    ''' <returns></returns>
    Public Function ExecuteReader(ByVal commandType As CommandType, ByVal commandText As String, ByVal ParamArray params() As SqlParameter) As SqlDataReader
        Return Me.ExecuteReader(commandType, commandText, Me.mDefaultCommandTimeout, params)
    End Function
    Public Function ExecuteReader(ByVal commandType As CommandType, ByVal commandText As String, ByVal commandTimeout As Integer, ByVal ParamArray params() As SqlParameter) As SqlDataReader
        Dim command As SqlCommand = Nothing
        Try
            command = New SqlCommand()
            command.Connection = mConnection
            command.CommandType = commandType
            command.CommandText = commandText
            command.CommandTimeout = commandTimeout

            If (mTransaction IsNot Nothing) Then command.Transaction = mTransaction

            If (params IsNot Nothing) Then command.Parameters.AddRange(params)
            Return command.ExecuteReader()
        Catch ex As SqlException
            Me.mLastErrorCommandText = commandText
            Throw
        Finally
            If (command IsNot Nothing) Then command.Dispose()
        End Try
    End Function

    ''' <summary>コマンドを実行して更新件数を返す</summary>
    ''' <param name="commandType"></param>
    ''' <param name="commandText"></param>
    ''' <param name="params"></param>
    ''' <returns></returns>
    Public Function ExecuteNonQuery(ByVal commandType As CommandType, ByVal commandText As String, ByVal ParamArray params() As SqlParameter) As Integer
        Return Me.ExecuteNonQuery(commandType, commandText, Me.mDefaultCommandTimeout, params)
    End Function

    Public Function ExecuteNonQuery(ByVal commandType As CommandType, ByVal commandText As String, ByVal commandTimeout As Integer, ByVal ParamArray params() As SqlParameter) As Integer
        Dim command As SqlCommand = Nothing
        Try
            command = New SqlCommand()
            command.Connection = mConnection
            command.CommandType = commandType
            command.CommandText = commandText
            command.CommandTimeout = commandTimeout

            If (mTransaction IsNot Nothing) Then command.Transaction = mTransaction

            If (params IsNot Nothing) Then command.Parameters.AddRange(params)
            Return command.ExecuteNonQuery()
        Catch ex As SqlException
            Me.mLastErrorCommandText = commandText
            Throw
        Finally
            If (command IsNot Nothing) Then command.Dispose()
        End Try
    End Function

    ''' <summary>コマンドを実行して最初の値を返す</summary>
    ''' <param name="commandType"></param>
    ''' <param name="commandText"></param>
    ''' <param name="params"></param>
    ''' <returns></returns>
    Public Function ExecuteScalar(ByVal commandType As CommandType, ByVal commandText As String, ByVal ParamArray params() As SqlParameter) As Object
        Return Me.ExecuteScalar(commandType, commandText, Me.mDefaultCommandTimeout, params)
    End Function

    Public Function ExecuteScalar(ByVal commandType As CommandType, ByVal commandText As String, ByVal commandTimeout As Integer, ByVal ParamArray params() As SqlParameter) As Object
        Dim command As SqlCommand = Nothing
        Try
            command = New SqlCommand()
            command.Connection = mConnection
            command.CommandType = commandType
            command.CommandText = commandText
            command.CommandTimeout = commandTimeout

            If (mTransaction IsNot Nothing) Then command.Transaction = mTransaction

            If (params IsNot Nothing) Then command.Parameters.AddRange(params)
            Return command.ExecuteScalar()
        Catch ex As SqlException
            Me.mLastErrorCommandText = commandText
            Throw
        Finally
            If (command IsNot Nothing) Then command.Dispose()
        End Try
    End Function

    Public Function ExecuteScalar(Of T)(ByVal commandType As CommandType, ByVal commandText As String, ByVal ParamArray params() As SqlParameter) As T
        Return CType(Me.ExecuteScalar(commandType, commandText, Me.mDefaultCommandTimeout, params), T)
    End Function

    Public Function ExecuteScalar(Of T)(ByVal commandType As CommandType, ByVal commandText As String, ByVal commandTimeout As Integer, ByVal ParamArray params() As SqlParameter) As T
        Return CType(Me.ExecuteScalar(commandType, commandText, commandTimeout, params), T)
    End Function
#End Region
End Class