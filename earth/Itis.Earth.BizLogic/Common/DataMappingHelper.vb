Imports System.Reflection
''' <summary>
''' DB�̌������ʂƃ��R�[�h�N���X�̃}�b�s���O���s���w���p�[�N���X�ł�(�ÓI�����o)
''' </summary>
''' <remarks></remarks>
Public Class DataMappingHelper

    '�ÓI�ϐ��Ƃ��ăN���X�^�̃C���X�^���X�𐶐�
    Private Shared _instance = New DataMappingHelper()

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    '�ÓI�֐��Ƃ��ăN���X�^�̃C���X�^���X��Ԃ��֐���p��
    Public Shared ReadOnly Property Instance() As DataMappingHelper
        Get
            '�ÓI�ϐ����������Ă����ꍇ�̂݁A�C���X�^���X�𐶐�����
            If IsDBNull(_instance) Then
                _instance = New DataMappingHelper()
            End If
            Return _instance
        End Get
    End Property

    ''' <summary>
    ''' �f�[�^�e�[�u���Ɋi�[���ꂽ�f�[�^��
    ''' �w�肵�����R�[�h�^�C�v��List�ɕϊ����܂�
    ''' </summary>
    ''' <param name="recordType">�ݒ�惌�R�[�h�̃^�C�v</param>
    ''' <param name="table">�ݒ�Ώۂ̃f�[�^�e�[�u��</param>
    ''' <returns>�ݒ�惌�R�[�h��Generic List(of T)</returns>
    ''' <remarks>
    ''' <example>
    ''' �ȉ��̗��DataAccess�N���X���擾����DataTable�̓��e��<br/>
    ''' ���背�R�[�h�N���X��Generic List(of T)�Ƃ��Ď擾���܂�
    ''' <code>
    ''' �f�ȉ��̂悤�ȃ��R�[�h�N���X��p�ӂ��܂�
    ''' Public Class LoginUserInfo
    '''     ''' <summary>
    '''     ''' �A�J�E���gNO
    '''     ''' </summary>
    '''     <remarks></remarks>
    '''     Private intAccountNo As Integer
    '''     ''' <summary>
    '''     ''' �A�J�E���gNO
    '''     ''' </summary>
    '''     ''' <value></value>
    '''     ''' <returns>�A�J�E���gNO</returns>
    '''     ''' <remarks></remarks>
    '''     ��TableMap("account_no")�� _
    '''     Public Property AccountNo() As Integer
    '''         Get
    '''             Return intAccountNo
    '''         End Get
    '''         Set(ByVal value As Integer)
    '''             intAccountNo = value
    '''         End Set
    '''     End Property
    ''' End Class
    ''' </code>
    ''' 
    ''' ��TableMap("@@@@@")�� �� @@@@@ ������DataTable�̍��ڂ�ݒ肵�܂�<br/>
    ''' ���ӓ_�Ƃ��đ����������ɂ��ĉ������i�����͎��۔��p�ł��j<br/><br/>
    ''' 
    ''' DB�������s���A�f�[�^�e�[�u����ԋp�N���X������Ɖ��肵�A�擾�f�[�^��<br/>
    ''' ���R�[�h�Ƀ}�b�s���O�����ł�
    ''' 
    ''' <code>
    ''' Private Sub xxxxx()
    ''' 
    '''     Dim data_access As New xxDataAccess 
    '''     Dim data_table As New DataTable
    ''' 
    '''     ' DB���l���擾���f�[�^�e�[�u���փZ�b�g
    '''     data_table = data_access.getTableData()
    ''' 
    '''     ' �f�[�^�}�b�s���O�p�N���X
    '''     Dim helper As New DataMappingHelper
    '''     Dim record As New LoginUserInfo
    ''' 
    '''     ' �ݒ肵�������R�[�h�̃^�C�v�Ɛݒ�Ώۂ�DataTable�������Ƀf�[�^�ݒ�ς�Generic List���擾
    '''     Dim list As List(of LoginUserInfo) = helper.getMapArray(of LoginUserInfo)(record.getType(), data_table)
    ''' 
    '''     ' �擾�����f�[�^�Ŋe��ҏW�������s��
    '''     For Each record As LoginUserInfo.AccountTableRow In list
    '''         ' ���̃v���p�e�B�ɂ͎擾�����f�[�^���ݒ肳��Ă���
    '''         record.AccountNo
    '''     Next
    ''' 
    ''' End Sub
    ''' </code>
    ''' </example>
    ''' </remarks>
    Public Function getMapArray(Of T)(ByVal recordType As Type, _
                            ByVal table As DataTable) As List(Of T)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getMapArray", recordType, table)

        Dim list As New List(Of T)

        '�Z�b�g��f�[�^�e�[�u���̃J�������ꗗ�̃n�b�V����ݒ�
        Dim hashColumnNames As Hashtable = getColumnNamesHashtable(table)
        If hashColumnNames Is Nothing OrElse hashColumnNames.Count = 0 Then
            Return list
        End If

        ' �f�[�^�e�[�u���̌��������������{
        For Each row As DataRow In table.Rows
            ' �f�[�^�̃}�b�s���O���s���A�ݒ肵�����R�[�h��List(Of T)�ɃZ�b�g
            list.Add(propertyMap(recordType, row, hashColumnNames))
        Next

        Return list

    End Function

    ''' <summary>
    ''' �f�[�^�e�[�u���Ɋi�[���ꂽ�f�[�^��
    ''' �w�肵�����R�[�h�^�C�v��List(Of T)�ɕϊ����܂�
    ''' �J�n�ʒu�ƏI���ʒu���w�肵�ăf�[�^���o���\�ł�
    ''' �y�[�W���䎞���ŗ��p
    ''' </summary>
    ''' <param name="recordType">�ݒ�惌�R�[�h�̃^�C�v</param>
    ''' <param name="table">�ݒ�Ώۂ̃f�[�^�e�[�u��</param>
    ''' <param name="startRow">���o�J�n�s(1�s�ڂ�1) </param>
    ''' <param name="endRow">���o�I���s</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMapArray(Of T)(ByVal recordType As Type, _
                                ByVal table As DataTable, _
                                ByVal startRow As Integer, _
                                ByVal endRow As Integer) As List(Of T)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getMapArray", _
                                                    recordType, _
                                                    table, _
                                                    startRow, _
                                                    endRow)

        Dim list As New List(Of T)

        Dim i As Integer

        ' �J�n�s���I���s�̏ꍇ�A�����I��
        If startRow > endRow Then
            Return list
        End If

        ' �J�n�s���f�[�^�e�[�u�������̏ꍇ�A�����I��
        If startRow > table.Rows.Count Then
            Return list
        End If

        '�Z�b�g��f�[�^�e�[�u���̃J�������ꗗ�̃n�b�V����ݒ�
        Dim hashColumnNames As Hashtable = getColumnNamesHashtable(table)
        If hashColumnNames Is Nothing OrElse hashColumnNames.Count = 0 Then
            Return list
        End If

        ' �͈͎w�蕪���������{
        For i = startRow To endRow

            ' �͈͓��Ńf�[�^�������Ȃ�����ݒ���I������
            If i > table.Rows.Count Then
                Exit For
            End If

            ' �s�f�[�^���擾
            Dim row As DataRow = table.Rows(i - 1)

            ' ���X�g�ɃZ�b�g
            list.Add(propertyMap(recordType, row, hashColumnNames))

        Next

        Return list

    End Function

    ''' <summary>
    ''' �Z�b�g��f�[�^�e�[�u���̃J�������ꗗ�̃n�b�V����ݒ�
    ''' </summary>
    ''' <param name="table">�Ώۃf�[�^�e�[�u��</param>
    ''' <returns>�J�������Z�b�g�ς݃n�b�V���e�[�u��</returns>
    ''' <remarks></remarks>
    Function getColumnNamesHashtable(ByVal table As DataTable) As Hashtable
        Dim hashColumnNames As New Hashtable
        If table.Rows.Count > 0 Then
            Dim tmpRow As DataRow = table.Rows(0)
            For Each column As DataColumn In tmpRow.Table.Columns
                hashColumnNames.Add(column.ColumnName, True)
            Next
        End If
        Return hashColumnNames
    End Function

    ''' <summary>
    ''' ���R�[�h�N���X�̃v���p�e�B�ƃf�[�^�e�[�u���̃}�b�s���O���s��
    ''' </summary>
    ''' <param name="recordType">�ݒ�惌�R�[�h�N���X�̃^�C�v</param>
    ''' <param name="row">�ݒ茳��DataRow</param>
    ''' <returns>�ݒ�惌�R�[�h</returns>
    ''' <remarks></remarks>
    Public Function propertyMap(ByVal recordType As Type, _
                                ByVal row As DataRow, _
                                ByVal hashColumnNames As Hashtable) As Object

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".propertyMap", _
                                            recordType, _
                                            row)

        ' �ݒ�惌�R�[�h�̃v���p�e�B���
        Dim propertyInfo As System.Reflection.PropertyInfo

        ' �ݒ�惌�R�[�h�̃C���X�^���X���쐬
        Dim target As Object = recordType.InvokeMember(Nothing, _
                                BindingFlags.CreateInstance, _
                                Nothing, _
                                Nothing, _
                                New Object() {})

        Dim exitLoop As Boolean = False

        ' �ݒ��̃v���p�e�B��񕪃��[�v���A���ꍀ�ڂ�T��
        For Each propertyInfo In recordType.GetProperties

            Dim list() As Object = propertyInfo.GetCustomAttributes(GetType(TableMapAttribute), False)
            Dim item As TableMapAttribute

            ' �J�X�^���A�g���r���[�g���������s���i���̏����̏ꍇ�͂P�������Ȃ��ׂP��̂݁j
            For Each item In list

                ' �ݒ茳�f�[�^�̃J�������ƃA�g���r���[�g�̃J����������v�����ꍇ�A
                ' �ݒ�惌�R�[�h�Ƀf�[�^���Z�b�g����
                ' ���j�f�[�^�e�[�u���̍��ڑ����ƃ��R�[�h�̑����͓���ɂ��ĉ�����
                ' �@�@�@�f�[�^�e�[�u��Int32 ���R�[�hInteger �͖��Ȃ�
                ' �@�@�@�f�[�^�e�[�u��Int32 ���R�[�hBoolean �̓G���[�ɂȂ�܂�

                ' �ݒ茳�f�[�^�̎擾
                Dim rowData As Object = Nothing
                Try
                    If hashColumnNames.ContainsKey(item.ItemName()) Then
                        '�Z�b�g�Ώۂ̃J���������݂���ꍇ
                        ' DBNull�̏ꍇ�A�ݒ肵�Ȃ��iDBNull�ˊe�^�C�v�ւ̃L���X�g��O����������ׁj
                        If Not row(item.ItemName()).GetType Is GetType(DBNull) Then
                            rowData = row(item.ItemName())
                        End If
                    Else
                        '�Z�b�g�ΏۃJ���������݂��Ȃ��ꍇ�A���[�v�𔲂���
                        exitLoop = True
                        Exit For
                    End If

                Catch ex As Exception
                    ' �擾�Ɏ��s�����ꍇ�A�ݒ肵�Ȃ�
                    exitLoop = True
                    Exit For
                End Try

                ' �擾�l��NULL�̏ꍇ�͐ݒ肵�Ȃ�
                If rowData Is Nothing Then
                    exitLoop = True
                    Exit For
                End If

                ' �ݒ��v���p�e�B�֐ݒ�
                recordType.InvokeMember(propertyInfo.Name, _
                                 BindingFlags.SetProperty, _
                                 Nothing, _
                                 target, _
                                 New Object() {rowData})

                exitLoop = True
                Exit For
            Next
        Next

        Return target

    End Function
End Class
