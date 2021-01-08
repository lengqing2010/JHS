'�N���X���W���[������
' ��������=====================================================================
'
'  �f�[�^�x�[�X�ւ̎Q�ƁA�X�V��e�Ղɍs���N���X���W���[��(Oracle)
'
' �y���W���[�����z
'       ORCLHelper.vb
'
' �y�T�v�z
'		�f�[�^�Z�b�g���擾���A�f�[�^�Z�b�g�̎Q�ƁA�X�V�����{����B
'
'       �񋟂��郁�\�b�h�͎��̒ʂ�ł��B
'
'           1) AttachParameters
'				OracleParameter�z���OracleCommand�ɒǉ����܂��B
'
'           2) PrepareCommand
'				���̕��@�͊J���āi�K�v�ɉ����āj�A�ڑ��A���߃^�C�v�ƃp�����[�^�����蓖�Ă܂��B
'
'           3) ExecuteScalar
'				�ڑ�������A�N�G���A�p�����[�^��n�����Ƃɂ��Q�ƌn�̃N�G�������s����A
'				�߂�l�Ƃ��ĒP��̏W�v�l���Ԃ����B
'
'           4) ExecuteNonQuery
'				�ڑ�������A�N�G���A�p�����[�^��n�����Ƃɂ��X�V�n�̃N�G�������s����A
'				�߂�l�Ƃ��ĕύX���ꂽ���R�[�h�����Ԃ����B
'
'           5) ExecuteDataset
'				�ڑ�������A�N�G���A�p�����[�^��n�����Ƃɂ��Q�ƌn�̃N�G�������s����A
'				�߂�l�Ƃ���DataSet���Ԃ����B
'
'           6) FillDataset
'				�ڑ�������A�R�}���h�^�C�v�A�N�G���A�f�[�^�Z�b�g�A�e�[�u�����A
'               �p�����[�^��n�����Ƃɂ��Q�ƌn�̃N�G�������s����A�����̃f�[�^�Z�b�g��
'               �e�[�u�������擾���܂��B
'
'			7) UpdateDataset
'				SQL�X�e�[�g�����g���f�[�^�Z�b�g���X�V���A�f�[�^�x�[�X���X�V����B
'
'			8) MakeParam
'				�p�����[�^���쐬���ĕԋp���܂��B
'
' �y���\�b�h�̗��p���@�z
'		�N���X��`��(ORCLHelper)���Q�Ƃ��Ă��������B
'
' �y�ݒ�z
'       �Q�Ƃ̒ǉ�
'			�v���W�F�N�g
'				Oracle.DataAccess
'
' �y�X�V�����z
'
' ��������=====================================================================
Imports Oracle.DataAccess.Client
Imports Itis.ApplicationBlocks.ExceptionManagement

''' <summary>
''' ���O��� ORCLHelper ���g�p����ۂ́A�����\�Ńx�X�g�v���N�e�B�X�̃\�����[�V������񋟂���N���X�ł��B
''' </summary>
Public NotInheritable Class ORCLHelper
#Region "private utility methods & constructors"
    Private Sub New()
    End Sub

    ''' <summary>
    ''' OracleParameter�z���OracleCommand�ɒǉ����܂��B
    ''' </summary>
    ''' <param name="command">�ǉ�����commandParameters��command�B</param>
    ''' <param name="commandParameters">commandParameters�z���command�ɒǉ����܂��B</param>
    Private Shared Sub AttachParameters(ByVal command As OracleCommand, ByVal commandParameters() As OracleParameter)
        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.AttachParameters", _
            command, commandParameters)
        If (command Is Nothing) Then Throw New ArgumentNullException("command")
        If (Not commandParameters Is Nothing) Then
            Dim p As OracleParameter
            For Each p In commandParameters
                If (Not p Is Nothing) Then
                    ' commandParameters��Direction�ɂ���āAValue��ݒ肵�܂��B
                    If (p.Direction = ParameterDirection.InputOutput OrElse p.Direction = ParameterDirection.Input) AndAlso p.Value Is Nothing Then
                        p.Value = DBNull.Value
                    End If
                    command.Parameters.Add(p)
                End If
            Next p
        End If
    End Sub ' AttachParameters

    ''' <summary>
    ''' ���̕��@�͊J���āi�K�v�ɉ����āj�A�ڑ��A���߃^�C�v�ƃp�����[�^�����蓖�Ă܂��B
    ''' </summary>
    ''' <param name="command">���������OracleCommand�B</param>
    ''' <param name="connection">�L����OracleConnection�i���̃R�}���h�����s����j�B</param>
    ''' <param name="commandType">�X�g�A�h�v���V�[�W���ATransact-SQL ���̃R�}���h�^�C�v�B</param>
    ''' <param name="commandText">�X�g�A�h�v���V�[�W�����A�������� Transact-SQL �R�}���h�B</param>
    ''' <param name="commandParameters">�p�����^���C�Y�h�N�G�������̔z��B</param>
    Private Shared Sub PrepareCommand(ByVal command As OracleCommand, _
                                      ByVal connection As OracleConnection, _
                                      ByVal commandType As CommandType, _
                                      ByVal commandText As String, _
                                      ByVal commandParameters() As OracleParameter, ByRef mustCloseConnection As Boolean)
        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.PrepareCommand", _
            command, connection, commandType, commandText, commandParameters, mustCloseConnection)

        If (command Is Nothing) Then Throw New ArgumentNullException("command")
        If (commandText Is Nothing OrElse commandText.Length = 0) Then Throw New ArgumentNullException("commandText")

        ' �񋟂��ꂽ�ڑ����J���Ă��Ȃ��Ȃ�΁A������J���܂��B
        If connection.State <> ConnectionState.Open Then
            connection.Open()
            mustCloseConnection = True
        Else
            mustCloseConnection = False
        End If

        ' �ڑ����R�}���h�ƌ��т��܂��B
        command.Connection = connection

        ' ���߃e�L�X�g���Z�b�g���܂�(�X�g�A�h�v���V�[�W�����܂���SQL����)�B
        command.CommandText = commandText

        ' ���߃^�C�v���Z�b�g���܂��B
        command.CommandType = commandType

        ' �񋟂����Ȃ�΁A���߃p�����[�^��t���܂��B
        If Not (commandParameters Is Nothing) Then
            AttachParameters(command, commandParameters)
        End If
        Return
    End Sub ' PrepareCommand

#End Region

#Region "ExecuteDataset"

    ''' <summary>
    ''' �Q�ƌn�N�G�������s���ADataSet ���擾���܂��B
    ''' </summary>
    ''' <param name="connectionString">OracleConnection �p�̐ڑ�������B</param>
    ''' <param name="commandType">�X�g�A�h�v���V�[�W���ATransact-SQL ���̃R�}���h�^�C�v�B</param>
    ''' <param name="commandText">�X�g�A�h�v���V�[�W�����A�������� Transact-SQL �R�}���h�B</param>
    ''' <returns>�R�}���h�̎��s���ʂ��i�[���� DataSet�B</returns>
    ''' <remarks>
    ''' ���̃��\�b�h�ł͌^�t�f�[�^�Z�b�g���g�p�ł��܂���B
    ''' �^�t�f�[�^�Z�b�g���g�p����ꍇ�́AFillDataset ���\�b�h���g�p���܂��B
    ''' <example>
    ''' �g�p��F
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
        ' connection���쐬���ĊJ���܂��B�ŏI��Dispose�ɂ��܂��B
        Dim connection As OracleConnection = Nothing

        Try
            connection = New OracleConnection(connectionString)
            connection.Open()

            ' ExecuteDataset�֐����Ăт܂��B(OracleParameter��Nothing�ɃZ�b�g���܂�)
            Return ExecuteDataset(connection, commandType, commandText, CType(Nothing, OracleParameter()))
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' �Q�ƌn�N�G�������s���ADataSet ���擾���܂��B
    ''' �p�����^���C�Y�h�N�G�����g�p���܂��B
    ''' </summary>
    ''' <param name="connectionString">OracleConnection �p�̐ڑ�������B</param>
    ''' <param name="commandType">�X�g�A�h�v���V�[�W���ATransact-SQL ���̃R�}���h�^�C�v�B</param>
    ''' <param name="commandText">�X�g�A�h�v���V�[�W�����A�������� Transact-SQL �R�}���h�B</param>
    ''' <param name="commandParameters">�p�����^���C�Y�h�N�G�������̔z��B</param>
    ''' <returns>�R�}���h�̎��s���ʂ��i�[���� DataSet�B</returns>
    ''' <remarks>
    ''' ���̃��\�b�h�ł͌^�t�f�[�^�Z�b�g���g�p�ł��܂���B
    ''' �^�t�f�[�^�Z�b�g���g�p����ꍇ�́AFillDataset ���\�b�h���g�p���܂��B
    ''' <example>
    ''' �g�p��F
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
        ' connection���쐬���ĊJ���܂��B�ŏI��Dispose�ɂ��܂��B
        Dim connection As OracleConnection = Nothing

        Try
            connection = New OracleConnection(connectionString)
            connection.Open()

            ' ExecuteDataset�֐����Ăт܂��B
            Return ExecuteDataset(connection, commandType, commandText, commandParameters)
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' �Q�ƌn�N�G�������s���ADataSet ���擾���܂��B
    ''' </summary>
    ''' <param name="connection">OracleConnection</param>
    ''' <param name="commandType">�X�g�A�h�v���V�[�W���ATransact-SQL ���̃R�}���h�^�C�v�B</param>
    ''' <param name="commandText">�X�g�A�h�v���V�[�W�����A�������� Transact-SQL �R�}���h�B</param>
    ''' <param name="commandParameters">�p�����^���C�Y�h�N�G�������̔z��B</param>
    ''' <returns>�R�}���h�̎��s���ʂ��i�[���� DataSet�B</returns>
    ''' <remarks>
    ''' ���̃��\�b�h�ł͌^�t�f�[�^�Z�b�g���g�p�ł��܂���B
    ''' �^�t�f�[�^�Z�b�g���g�p����ꍇ�́AFillDataset ���\�b�h���g�p���܂��B
    '''</remarks>
    Private Overloads Shared Function ExecuteDataset(ByVal connection As OracleConnection, _
                                                    ByVal commandType As CommandType, _
                                                    ByVal commandText As String, _
                                                    ByVal ParamArray commandParameters() As OracleParameter) As DataSet
        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.ExecuteDataset", _
                    connection, commandType, commandText, commandParameters)
        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
        ' �R�}���h���쐬���܂��B
        Dim cmd As New OracleCommand
        Dim ds As New DataSet
        Dim dataAdatpter As OracleDataAdapter = Nothing
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, connection, commandType, commandText, commandParameters, mustCloseConnection)

        Try
            ' OracleDataAdapter��DataSet���쐬���܂��B
            dataAdatpter = New OracleDataAdapter(cmd)

            ' Fill�֐���DataSet�ɒl���Z�b�g���܂��B
            dataAdatpter.Fill(ds)


            cmd.Parameters.Clear()
        Finally
            If (Not dataAdatpter Is Nothing) Then dataAdatpter.Dispose()
        End Try
        If (mustCloseConnection) Then connection.Close()

        ' dataset�ɖ߂�܂��B
        Return ds
    End Function ' ExecuteDataset

#End Region

#Region "ExecuteScalar"

    ''' <summary>
    ''' �Q�ƌn�N�G�������s���A���ʃZ�b�g�̍ŏ��̍s�ɂ���ŏ��̗���擾���܂��B
    ''' �c��̗�܂��͍s�͖�������܂��B
    ''' </summary>
    ''' <param name="connectionString">OracleConnection �p�̐ڑ�������B</param>
    ''' <param name="commandType">�X�g�A�h�v���V�[�W���ATransact-SQL ���̃R�}���h�^�C�v�B</param>
    ''' <param name="commandText">�X�g�A�h�v���V�[�W�����A�������� Transact-SQL �R�}���h�B</param>
    ''' <returns>���ʃZ�b�g�̍ŏ��̍s�̍ŏ��̗�B���ʃZ�b�g����̏ꍇ�́Anull �Q�ƁB</returns>
    ''' <remarks><example>
    ''' �g�p��F
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
        ' connection���쐬���ĊJ���܂��B�ŏI��Dispose�ɂ��܂��B
        Dim connection As OracleConnection = Nothing
        Dim rtnObj As Object

        Try
            connection = New OracleConnection(connectionString)
            connection.Open()

            ' ExecuteScalar�֐����Ăт܂��B(OracleParameter��Nothing�ɃZ�b�g���܂�)
            rtnObj = ExecuteScalar( _
            connection, commandType, commandText, CType(Nothing, OracleParameter()))
            Return rtnObj
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' �Q�ƌn�N�G�������s���A���ʃZ�b�g�̍ŏ��̍s�ɂ���ŏ��̗���擾���܂��B
    ''' �p�����^���C�Y�h�N�G�����g�p���܂��B
    ''' �c��̗�܂��͍s�͖�������܂��B
    ''' </summary>
    ''' <param name="connectionString">OracleConnection �p�̐ڑ�������B</param>
    ''' <param name="commandType">�X�g�A�h�v���V�[�W���ATransact-SQL ���̃R�}���h�^�C�v�B</param>
    ''' <param name="commandText">�X�g�A�h�v���V�[�W�����A�������� Transact-SQL �R�}���h�B</param>
    ''' <param name="commandParameters">�p�����^���C�Y�h�N�G�������̔z��B</param>
    ''' <returns>���ʃZ�b�g�̍ŏ��̍s�̍ŏ��̗�B���ʃZ�b�g����̏ꍇ�́Anull �Q�ƁB</returns>
    ''' <remarks><example>
    ''' �g�p��F
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
        ' connection���쐬���ĊJ���܂��B�ŏI��Dispose�ɂ��܂��B
        Dim connection As OracleConnection = Nothing
        Dim rtnObj As Object

        Try
            connection = New OracleConnection(connectionString)
            connection.Open()

            ' ExecuteScalar�֐����Ăт܂��B(OracleParameter��Nothing�ɃZ�b�g���܂�)
            rtnObj = ExecuteScalar( _
            connection, commandType, commandText, commandParameters)
            Return rtnObj
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' �Q�ƌn�N�G�������s���A���ʃZ�b�g�̍ŏ��̍s�ɂ���ŏ��̗���擾���܂��B
    ''' </summary>
    ''' <param name="connection">OracleConnection�B</param>
    ''' <param name="commandType">�X�g�A�h�v���V�[�W���ATransact-SQL ���̃R�}���h�^�C�v�B</param>
    ''' <param name="commandText">�X�g�A�h�v���V�[�W�����A�������� Transact-SQL �R�}���h�B</param>
    ''' <param name="commandParameters">�p�����^���C�Y�h�N�G�������̔z��B</param>
    ''' <returns>���ʃZ�b�g�̍ŏ��̍s�̍ŏ��̗�B���ʃZ�b�g����̏ꍇ�́Anull �Q�ƁB</returns>
    Private Overloads Shared Function ExecuteScalar(ByVal connection As OracleConnection, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String, _
                                                   ByVal ParamArray commandParameters() As OracleParameter) As Object

        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")

        ' �R�}���h���쐬���܂��B
        Dim cmd As New OracleCommand
        Dim retval As Object
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, connection, commandType, commandText, commandParameters, mustCloseConnection)

        ' �N�G�������s���܂��B
        retval = cmd.ExecuteScalar()

        ' Parameters�̒��g���N���A���܂��B
        cmd.Parameters.Clear()

        If (mustCloseConnection) Then connection.Close()

        Return retval

    End Function ' ExecuteScalar

#End Region

#Region "ExecuteNonQuery"

    ''' <summary>
    ''' �X�V�n�N�G�������s���A�X�V�����s����Ԃ��܂��B
    ''' </summary>
    ''' <param name="connectionString">OracleConnection �p�̐ڑ�������B</param>
    ''' <param name="commandType">�X�g�A�h�v���V�[�W���ATransact-SQL ���̃R�}���h�^�C�v�B</param>
    ''' <param name="commandText">�X�g�A�h�v���V�[�W�����A�������� Transact-SQL �R�}���h�B</param>
    ''' <returns>�R�}���h���s�ɂ���ĉe�����y�ڂ����s���B</returns>
    ''' <remarks><example>
    ''' �g�p��F
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

        ' connection���쐬���ĊJ���܂��B�ŏI��Dispose�ɂ��܂��B
        Dim connection As OracleConnection = Nothing
        Dim cnt As Integer

        Try
            connection = New OracleConnection(connectionString)

            connection.Open()

            ' ExecuteNonQuery�֐����Ăт܂��B
            cnt = ExecuteNonQuery( _
            connection, commandType, commandText, CType(Nothing, OracleParameter()))
            Return cnt
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' �X�V�n�N�G�������s���A�X�V�����s����Ԃ��܂��B
    ''' �p�����^���C�Y�h�N�G�����g�p���܂��B
    ''' </summary>
    ''' <param name="connectionString">OracleConnection �p�̐ڑ�������B</param>
    ''' <param name="commandType">�X�g�A�h�v���V�[�W���ATransact-SQL ���̃R�}���h�^�C�v�B</param>
    ''' <param name="commandText">�X�g�A�h�v���V�[�W�����A�������� Transact-SQL �R�}���h�B</param>
    ''' <param name="commandParameters">�p�����^���C�Y�h�N�G�������̔z��B</param>
    ''' <returns>�R�}���h���s�ɂ���ĉe�����y�ڂ����s���B</returns>
    ''' <remarks><example>
    ''' �g�p��F
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

        ' connection���쐬���ĊJ���܂��B�ŏI��Dispose�ɂ��܂��B
        Dim connection As OracleConnection = Nothing
        Dim cnt As Integer

        Try
            connection = New OracleConnection(connectionString)

            connection.Open()

            ' ExecuteNonQuery�֐����Ăт܂��B
            cnt = ExecuteNonQuery( _
            connection, commandType, commandText, commandParameters)
            Return cnt
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' �X�V�n�N�G�������s���A�X�V�����s����Ԃ��܂��B
    ''' �p�����^���C�Y�h�N�G�����g�p���܂��B
    ''' </summary>
    ''' <param name="connection">OracleConnection�B</param>
    ''' <param name="commandType">�X�g�A�h�v���V�[�W���ATransact-SQL ���̃R�}���h�^�C�v�B</param>
    ''' <param name="commandText">�X�g�A�h�v���V�[�W�����A�������� Transact-SQL �R�}���h�B</param>
    ''' <param name="commandParameters">�p�����^���C�Y�h�N�G�������̔z��B</param>
    ''' <returns>�R�}���h���s�ɂ���ĉe�����y�ڂ����s���B</returns>
    ''' <remarks><example>
    ''' �g�p��F
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

        ' �R�}���h���쐬���܂��B
        Dim cmd As New OracleCommand
        Dim retval As Integer
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, connection, commandType, commandText, commandParameters, mustCloseConnection)

        ' �N�G�������s���܂��B
        retval = cmd.ExecuteNonQuery()

        ' Parameters�̒��g���N���A���܂��B
        cmd.Parameters.Clear()

        If (mustCloseConnection) Then connection.Close()

        Return retval
    End Function ' ExecuteNonQuery

#End Region

#Region "FillDataset"

    ''' <summary>
    ''' �Q�ƌn�N�G�������s���A���ʃZ�b�g���f�[�^�Z�b�g�Ɋi�[���܂��B
    ''' </summary>
    ''' <param name="connectionString">OracleConnection �p�̐ڑ�������B</param>
    ''' <param name="commandType">�X�g�A�h�v���V�[�W���ATransact-SQL ���̃R�}���h�^�C�v�B</param>
    ''' <param name="commandText">�X�g�A�h�v���V�[�W�����A�������� Transact-SQL �R�}���h�B</param>
    ''' <param name="ds">�R�}���h���s�ɂ�萶������錋�ʃZ�b�g���i�[����f�[�^�Z�b�g�B</param>
    ''' <param name="tableName">���ʃZ�b�g���i�[����f�[�^�Z�b�g���� DataTable ���B</param>
    ''' <remarks><example>
    ''' �g�p��F<br/>
    ''' �^�t�f�[�^�Z�b�g���p��
    ''' <code>
    '''   ORCLHelper.FillDataset(connString, CommandType.Text, _
    '''       "SELECT * FROM ORDERS", orderDataSet, orderDataSet.Orders.TableName)
    ''' </code><br/>
    ''' �^���f�[�^�Z�b�g���p��
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
                Throw New ArgumentException("tableNames��Nothing�������͒l�������Ă��܂���B", "tableNames")
            End If
        Next

        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        If (ds Is Nothing) Then Throw New ArgumentNullException("dataSet")

        ' connection���쐬���ĊJ���܂��B�ŏI��Dispose�ɂ��܂��B
        Dim connection As OracleConnection = Nothing
        Try
            connection = New OracleConnection(connectionString)

            connection.Open()

            ' FillDataset�֐����Ăт܂��B(OracleParameter��Nothing�ɃZ�b�g���܂�)
            FillDataset(connection, commandType, commandText, ds, tableNames, CType(Nothing, OracleParameter()))
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Sub

    ''' <summary>
    ''' �Q�ƌn�N�G�������s���A���ʃZ�b�g���f�[�^�Z�b�g�Ɋi�[���܂��B
    ''' �p�����^���C�Y�h�N�G�����g�p���܂��B
    ''' </summary>
    ''' <param name="connectionString">OrclConnection �p�̐ڑ�������B</param>
    ''' <param name="commandType">�X�g�A�h�v���V�[�W���ATransact-SQL ���̃R�}���h�^�C�v�B</param>
    ''' <param name="commandText">�X�g�A�h�v���V�[�W�����A�������� Transact-SQL �R�}���h�B</param>
    ''' <param name="ds">�R�}���h���s�ɂ�萶������錋�ʃZ�b�g���i�[����f�[�^�Z�b�g�B</param>
    ''' <param name="tableName">���ʃZ�b�g���i�[����f�[�^�Z�b�g���� DataTable ���B</param>
    ''' <param name="commandParameters">�p�����^���C�Y�h�N�G�������̔z��B</param>
    ''' <remarks><example>
    ''' �g�p��F<br/>
    ''' �^�t�f�[�^�Z�b�g���p��
    ''' <code>
    '''   Dim cmdParams() As OracleParameter = _
    '''       {ORCLHelper.MakeParam("PRODID", OracleDbType.Int32, 0, 24)}
    '''   ORCLHelper.FillDataset(connString, CommandType.Text, _
    '''       "SELECT * FROM ORDERS WHERE PRODID = :PRODID", orderDataSet, _
    '''       orderDataSet.Orders.TableName, cmdParams)
    ''' </code><br/>
    ''' �^���f�[�^�Z�b�g���p��
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

        '����5 tableName 
        Dim tableNames() As String = New String() {tableName}

        Dim index As Integer
        For index = 0 To tableNames.Length - 1
            If (tableNames(index) Is Nothing OrElse tableNames(index).Length = 0) Then
                Throw New ArgumentException("tableNames��Nothing�������͒l�������Ă��܂���B", "tableNames")
            End If
        Next

        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        If (ds Is Nothing) Then Throw New ArgumentNullException("dataSet")

        ' connection���쐬���ĊJ���܂��B�ŏI��Dispose�ɂ��܂��B
        Dim connection As OracleConnection = Nothing
        Try
            connection = New OracleConnection(connectionString)

            connection.Open()

            ' FillDataset�֐����Ăт܂��B
            FillDataset(connection, commandType, commandText, ds, tableNames, commandParameters)
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try

    End Sub

    ''' <summary>
    ''' �Q�ƌn�N�G�������s���A���ʃZ�b�g���f�[�^�Z�b�g�Ɋi�[���܂��B
    ''' ������ DataTable �����w�肵�܂��B
    ''' </summary>
    ''' <param name="connectionString">OrclConnection �p�̐ڑ�������B</param>
    ''' <param name="commandType">�X�g�A�h�v���V�[�W���ATransact-SQL ���̃R�}���h�^�C�v�B</param>
    ''' <param name="commandText">�X�g�A�h�v���V�[�W�����A�������� Transact-SQL �R�}���h�B</param>
    ''' <param name="ds">�R�}���h���s�ɂ�萶������錋�ʃZ�b�g���i�[����f�[�^�Z�b�g�B</param>
    ''' <param name="tableNames">���ʃZ�b�g���i�[����f�[�^�Z�b�g���� DataTable ���̔z��B</param>
    ''' <remarks><example>
    ''' �g�p��F<br/>
    ''' �^�t�f�[�^�Z�b�g���p��
    ''' <code>
    '''   ORCLHelper.FillDataset(connString, CommandType.Text, _
    '''       "SELECT * FROM ORDERS", orderDataSet, New String() {orderDataSet.Orders.TableName})
    ''' </code><br/>
    ''' �^���f�[�^�Z�b�g���p��
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

        ' connection���쐬���ĊJ���܂��B�ŏI��Dispose�ɂ��܂��B
        Dim connection As OracleConnection = Nothing
        Try
            connection = New OracleConnection(connectionString)

            connection.Open()

            ' FillDataset�֐����Ăт܂��B
            FillDataset(connection, commandType, commandText, ds, tableNames, CType(Nothing, OracleParameter()))
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Sub

    ''' <summary>
    ''' �Q�ƌn�N�G�������s���A���ʃZ�b�g���f�[�^�Z�b�g�Ɋi�[���܂��B
    ''' ������ DataTable �����w�肵�܂��B
    ''' �p�����^���C�Y�h�N�G�����g�p���܂��B
    ''' </summary>
    ''' <param name="connectionString">OrclConnection �p�̐ڑ�������B</param>
    ''' <param name="commandType">�X�g�A�h�v���V�[�W���ATransact-SQL ���̃R�}���h�^�C�v�B</param>
    ''' <param name="commandText">�X�g�A�h�v���V�[�W�����A�������� Transact-SQL �R�}���h�B</param>
    ''' <param name="ds">�R�}���h���s�ɂ�萶������錋�ʃZ�b�g���i�[����f�[�^�Z�b�g�B</param>
    ''' <param name="tableNames">���ʃZ�b�g���i�[����f�[�^�Z�b�g���� DataTable ���̔z��B</param>
    ''' <param name="commandParameters">�p�����^���C�Y�h�N�G�������̔z��B</param>
    ''' <remarks><example>
    ''' �g�p��F<br/>
    ''' �^�t�f�[�^�Z�b�g���p��
    ''' <code>
    '''   Dim cmdParams() As OracleParameter = _
    '''       {ORCLHelper.MakeParam("PRODID", OracleDbType.Int32, 0, 24)}
    '''   ORCLHelper.FillDataset(connString, CommandType.Text, _
    '''       "SELECT * FROM ORDERS WHERE PRODID = :PRODID", orderDataSet, _
    '''       New String() {orderDataSet.Orders.TableName}, cmdParams)
    ''' </code><br/>
    ''' �^���f�[�^�Z�b�g���p��
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

        ' connection���쐬���ĊJ���܂��B�ŏI��Dispose�ɂ��܂��B
        Dim connection As OracleConnection = Nothing
        Try
            connection = New OracleConnection(connectionString)

            connection.Open()

            ' FillDataset�֐����Ăт܂��B
            FillDataset(connection, commandType, commandText, ds, tableNames, commandParameters)
        Finally
            If Not connection Is Nothing Then connection.Dispose()
        End Try
    End Sub

    ''' <summary>
    ''' �Q�ƌn�N�G�������s���A���ʃZ�b�g���f�[�^�Z�b�g�Ɋi�[���܂��B
    ''' </summary>
    ''' <param name="connection">OracleConnection</param>
    ''' <param name="commandType">�X�g�A�h�v���V�[�W���ATransact-SQL ���̃R�}���h�^�C�v�B</param>
    ''' <param name="commandText">�X�g�A�h�v���V�[�W�����A�������� Transact-SQL �R�}���h�B</param>
    ''' <param name="dataSet">�R�}���h���s�ɂ�萶������錋�ʃZ�b�g���i�[����f�[�^�Z�b�g�B</param>
    ''' <param name="tableNames">���ʃZ�b�g���i�[����f�[�^�Z�b�g���� DataTable ���B</param>
    Private Overloads Shared Sub FillDataset(ByVal connection As OracleConnection, ByVal commandType As CommandType, _
        ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames() As String, _
        ByVal ParamArray commandParameters() As OracleParameter)

        UnTrappedExceptionManager.AddMethodEntrance("ORCLHelper.FillDataset", _
                            connection, commandType, commandText, dataSet, tableNames, commandParameters)

        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
        If (dataSet Is Nothing) Then Throw New ArgumentNullException("dataSet")

        ' OracleCommand���쐬���ĊJ���܂��B�ŏI��Dispose�ɂ��܂��B
        Dim command As New OracleCommand

        Dim mustCloseConnection As Boolean = False
        PrepareCommand(command, connection, commandType, commandText, commandParameters, mustCloseConnection)

        ' OracleDataAdapter��DataSet���쐬���܂��B
        Dim dataAdapter As OracleDataAdapter = New OracleDataAdapter(command)

        Try
            ' �e�[�u���}�b�s���O��ǉ����܂��B
            If Not tableNames Is Nothing AndAlso tableNames.Length > 0 Then

                Dim tableName As String = "Table"
                Dim index As Integer

                For index = 0 To tableNames.Length - 1
                    If (tableNames(index) Is Nothing OrElse tableNames(index).Length = 0) Then Throw New ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames")
                    dataAdapter.TableMappings.Add(tableName, tableNames(index))
                    tableName = "Table" & (index + 1).ToString()
                Next
            End If

            ' Fill�֐���DataSet�ɒl���Z�b�g���܂��B
            dataAdapter.Fill(dataSet)

            ' Parameters�̒��g���N���A���܂��B
            command.Parameters.Clear()
        Finally
            If (Not dataAdapter Is Nothing) Then dataAdapter.Dispose()
        End Try

        If (mustCloseConnection) Then connection.Close()

    End Sub

#End Region

#Region "UpdateDataset"

    ''' <summary>
    ''' �f�[�^�Z�b�g���̎w��� DataTable �ɑ΂���}���s�A�X�V�s�A�܂��͍폜�s�ɑ΂��āA
    ''' SELECT �����玩���I�ɐ�������� INSERT�AUPDATE�A�܂��� DELETE �������s���܂��B
    ''' </summary>
    ''' <param name="connectionString">OrclConnection �p�̐ڑ�������B</param>
    ''' <param name="selectText">�f�[�^�Z�b�g���擾����ۂɎg�p���� SELECT ���B</param>
    ''' <param name="ds">�ҏW�i�}���A�X�V�A�폜�j������s�����A�X�V���̃f�[�^�Z�b�g�B</param>
    ''' <param name="tableName">�f�[�^�Z�b�g���̍X�V���Ώۂ� DataTable ���B</param>
    ''' <returns>�f�[�^�Z�b�g���Ő���ɍX�V���ꂽ�s�̐��B</returns>
    ''' <remarks>
    ''' ��Q�����Ɏw�肷�� SELECT ���́A�ȉ��𖞂����K�v������܂��B<br/>
    ''' �E���Ȃ��Ƃ� 1 �̎�L�[�܂��͈�ӂ̗��Ԃ����ł��邱�ƁB<br/>
    ''' �E�Ώۂ̃e�[�u�����P��̃e�[�u���ł��邱�ƁB(�������������e�[�u���ł͂Ȃ����ƁB)<br/>
    ''' �E�����[�V�����V�b�v�̂Ȃ��e�[�u���ł��邱�ƁB<br/>
    ''' �E�񖼂܂��̓e�[�u�����ɃX�y�[�X�A�s���I�h (.)�A�^�╄ (?)�A���p���A���̑��̉p�����ȊO�̓��ꕶ�����܂܂�Ă��Ȃ����ƁB
    ''' <example>
    ''' �g�p��F
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

        '�߂�l
        Dim rtnInt As Integer

        '�ڑ��p�I�u�W�F�N�g�̍쐬
        Dim conn As OracleConnection = New OracleConnection
        conn.ConnectionString = connectionString

        'SELECT�p�R�}���h�E�I�u�W�F�N�g�쐬
        Dim selectCmd As OracleCommand = New OracleCommand
        selectCmd.Connection = conn
        selectCmd.CommandText = selectText

        '�f�[�^�A�_�v�^�̍쐬
        Dim da As OracleDataAdapter = New OracleDataAdapter
        Try
            da.SelectCommand = selectCmd

            '�R�}���h��������
            Dim cb As OracleCommandBuilder = New OracleCommandBuilder(da)

            '�f�[�^�x�[�X�̍X�V
            rtnInt = da.Update(ds, tableName)

            '�f�[�^�Z�b�g�̍X�V���e���R�~�b�g����
            ds.AcceptChanges()
        Finally
            If (Not da Is Nothing) Then da.Dispose()
            If (Not conn Is Nothing) Then conn.Dispose()
        End Try

        Return rtnInt
    End Function

    ''' <summary>
    ''' �f�[�^�Z�b�g���̎w��� DataTable �ɑ΂���}���s�A�X�V�s�A�܂��͍폜�s�ɑ΂��āA
    ''' �w�肵�� INSERT�AUPDATE�A�܂��� DELETE �������s���܂��B
    ''' </summary>
    ''' <param name="insertCommand">INSERT �p�́A�X�g�A�h�v���V�[�W���������� Transact-SQL �R�}���h�B</param>
    ''' <param name="updateCommand">UPDATE �p�́A�X�g�A�h�v���V�[�W���������� Transact-SQL �R�}���h�B</param>
    ''' <param name="deleteCommand">DELETE �p�́A�X�g�A�h�v���V�[�W���������� Transact-SQL �R�}���h�B</param>
    ''' <param name="ds">�ҏW�i�}���A�X�V�A�폜�j������s�����A�X�V���̃f�[�^�Z�b�g�B</param>
    ''' <param name="tableName">�f�[�^�Z�b�g���̍X�V���Ώۂ� DataTable ���B</param>
    ''' <remarks><example>
    ''' �g�p��F
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

        ' �f�[�^�A�_�v�^�̍쐬
        Dim dataAdapter As New OracleDataAdapter
        Try
            ' �f�[�^�A�_�v�^�[���߂��Z�b�g���܂��B
            dataAdapter.UpdateCommand = updateCommand
            dataAdapter.InsertCommand = insertCommand
            dataAdapter.DeleteCommand = deleteCommand

            ' �f�[�^�x�[�X�̍X�V
            dataAdapter.Update(ds, tableName)

            ' �f�[�^�Z�b�g�̍X�V���e���R�~�b�g���܂��B
            ds.AcceptChanges()
        Finally
            If (Not dataAdapter Is Nothing) Then dataAdapter.Dispose()
        End Try
    End Sub

#End Region

#Region "MakeParam"

    ''' <summary>
    ''' �p�����^���C�Y�h�N�G�������𐶐����܂��B
    ''' </summary>
    ''' <param name="parameterName">���蓖�Ă�p�����[�^�̖��O�B</param>
    ''' <param name="dbType">OracleDbType �l�� 1 �B</param>
    ''' <param name="size">�p�����[�^�̒����B</param>
    ''' <param name="value">�l������ Object�B</param>
    ''' <returns>�p�����^���C�Y�h�N�G�������B</returns>
    ''' <remarks>
    ''' ��R������ size �́A�ȉ��̂悤�Ɏw�肵�܂��B<br/>
    ''' �E�^�i��Q�����Ɏw�肷�� dbType�j���o�C�i���^�ƕ�����^�̏ꍇ�́A�K�؂ȃT�C�Y���w�肵�܂��B
    ''' �E����ȊO�̌^�̏ꍇ�́A�ݒ肳�ꂽ�l�͖�������܂����A����I�� 0 ��ݒ肵�Ă��������B
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
