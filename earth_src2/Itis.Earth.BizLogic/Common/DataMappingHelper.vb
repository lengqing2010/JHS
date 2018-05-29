Imports System.Reflection
''' <summary>
''' DB�̌������ʂƃ��R�[�h�N���X�̃}�b�s���O���s���w���p�[�N���X�ł�(�ÓI�����o)
''' </summary>
''' <remarks></remarks>
Public Class DataMappingHelper

    '�ÓI�ϐ��Ƃ��ăN���X�^�̃C���X�^���X�𐶐�
    Private Shared _instance = New DataMappingHelper()

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
    ''' �w�肵�����R�[�h�^�C�v��ArrayList�ɕϊ����܂�
    ''' </summary>
    ''' <param name="record_type">�ݒ�惌�R�[�h�̃^�C�v</param>
    ''' <param name="table">�ݒ�Ώۂ̃f�[�^�e�[�u��</param>
    ''' <returns>�ݒ�惌�R�[�h��ArrayList</returns>
    ''' <remarks>
    ''' <example>
    ''' �ȉ��̗��DataAccess�N���X���擾����DataTable�̓��e��<br/>
    ''' ���背�R�[�h�N���X��ArrayList�Ƃ��Ď擾���܂�
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
    '''     ' �ݒ肵�������R�[�h�̃^�C�v�Ɛݒ�Ώۂ�DataTable�������Ƀf�[�^�ݒ�ς�ArrayList���擾
    '''     Dim list As ArrayList = helper.getMapArray(record.getType(), data_table)
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
    Public Function getMapArray(ByVal record_type As Type, _
                                ByVal table As DataTable) As ArrayList

        Dim list As New ArrayList

        ' �f�[�^�e�[�u���̌��������������{
        For Each row As DataRow In table.Rows
            ' �f�[�^�̃}�b�s���O���s���A�ݒ肵�����R�[�h��ArrayList�ɃZ�b�g
            list.Add(propertyMap(record_type, row))
        Next

        Return list

    End Function

    ''' <summary>
    ''' �f�[�^�e�[�u���Ɋi�[���ꂽ�f�[�^��
    ''' �w�肵�����R�[�h�^�C�v��ArrayList�ɕϊ����܂�
    ''' �J�n�ʒu�ƏI���ʒu���w�肵�ăf�[�^���o���\�ł�
    ''' �y�[�W���䎞���ŗ��p
    ''' </summary>
    ''' <param name="record_type">�ݒ�惌�R�[�h�̃^�C�v</param>
    ''' <param name="table">�ݒ�Ώۂ̃f�[�^�e�[�u��</param>
    ''' <param name="start_row">���o�J�n�s(1�s�ڂ�1) </param>
    ''' <param name="end_row">���o�I���s</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMapArray(ByVal record_type As Type, _
                                ByVal table As DataTable, _
                                ByVal start_row As Integer, _
                                ByVal end_row As Integer) As ArrayList

        Dim list As New ArrayList

        Dim i As Integer

        ' �J�n�s���I���s�̏ꍇ�A�����I��
        If start_row > end_row Then
            Return list
        End If

        ' �J�n�s���f�[�^�e�[�u�������̏ꍇ�A�����I��
        If start_row > table.Rows.Count Then
            Return list
        End If

        ' �͈͎w�蕪���������{
        For i = start_row To end_row

            ' �͈͓��Ńf�[�^�������Ȃ�����ݒ���I������
            If i > table.Rows.Count Then
                Exit For
            End If

            ' �s�f�[�^���擾
            Dim row As DataRow = table.Rows(i - 1)

            ' ���X�g�ɃZ�b�g
            list.Add(propertyMap(record_type, row))

        Next

        Return list

    End Function

    ''' <summary>
    ''' ���R�[�h�N���X�̃v���p�e�B�ƃf�[�^�e�[�u���̃}�b�s���O���s��
    ''' </summary>
    ''' <param name="record_type">�ݒ�惌�R�[�h�N���X�̃^�C�v</param>
    ''' <param name="row">�ݒ茳��DataRow</param>
    ''' <returns>�ݒ�惌�R�[�h</returns>
    ''' <remarks></remarks>
    Public Function propertyMap(ByVal record_type As Type, _
                                ByVal row As DataRow) As Object

        ' �ݒ茳�f�[�^�̃v���p�e�B���
        Dim row_info As System.Reflection.PropertyInfo

        ' �ݒ�惌�R�[�h�̃v���p�e�B���
        Dim property_info As System.Reflection.PropertyInfo

        ' �ݒ�惌�R�[�h�̃C���X�^���X���쐬
        Dim target As Object = record_type.InvokeMember(Nothing, _
                                BindingFlags.CreateInstance, _
                                Nothing, _
                                Nothing, _
                                New Object() {})

        ' �ݒ茳�f�[�^�Ɋ܂܂��v���p�e�B���ݒ菈�����s��
        For Each row_info In row.GetType().GetProperties

            Dim exit_loop As Boolean = False

            ' �ݒ��̃v���p�e�B��񕪃��[�v���A���ꍀ�ڂ�T��
            For Each property_info In record_type.GetProperties

                Dim list() As Object = property_info.GetCustomAttributes(GetType(TableMapAttribute), False)
                Dim item As TableMapAttribute

                ' �J�X�^���A�g���r���[�g���������s���i���̏����̏ꍇ�͂P�������Ȃ��ׂP��̂݁j
                For Each item In list

                    ' �ݒ茳�f�[�^�̃J�������ƃA�g���r���[�g�̃J����������v�����ꍇ�A
                    ' �ݒ�惌�R�[�h�Ƀf�[�^���Z�b�g����
                    ' ���j�f�[�^�e�[�u���̍��ڑ����ƃ��R�[�h�̑����͓���ɂ��ĉ�����
                    ' �@�@�@�f�[�^�e�[�u��Int32 ���R�[�hInteger �͖��Ȃ�
                    ' �@�@�@�f�[�^�e�[�u��Int32 ���R�[�hBoolean �̓G���[�ɂȂ�܂�
                    If row_info.Name = item.ItemName() Then

                        ' �ݒ茳�f�[�^�̎擾
                        Dim row_data As Object
                        Try
                            row_data = row.GetType().InvokeMember(item.ItemName(), _
                                        BindingFlags.GetProperty, _
                                        Nothing, _
                                        row, _
                                        Nothing)
                        Catch ex As Exception
                            ' �擾�Ɏ��s�����ꍇ�A�ݒ肵�Ȃ�
                            exit_loop = True
                            Exit For
                        End Try

                        ' �擾�l��NULL�̏ꍇ�͐ݒ肵�Ȃ�
                        If row_data Is Nothing Then
                            exit_loop = True
                            Exit For
                        End If

                        ' �ݒ��v���p�e�B�֐ݒ�
                        record_type.InvokeMember(property_info.Name, _
                                         BindingFlags.SetProperty, _
                                         Nothing, _
                                         target, _
                                         New Object() {row_data})

                        exit_loop = True
                        Exit For
                    End If
                Next

                ' �ݒ�ύ��ڂ̏ꍇ�A���̍��ڐݒ���s��
                If exit_loop = True Then
                    Exit For
                End If
            Next
        Next

        Return target

    End Function
End Class
