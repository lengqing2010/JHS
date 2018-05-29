Imports System.Transactions
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' �N�x�v��l�ݒ�
''' </summary>
''' <remarks>�N�x�v��l�ݒ�</remarks>
''' <history>
''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
''' </history>
Public Class NendoKeikakuInputBC
    Private objNendoKeikakuInputDA As New NendoKeikakuInputda

    ''' <summary>
    ''' �S�Ќv��Ǘ��e�[�u������v������擾����
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <returns>�S�Ђ̌v����</returns>
    ''' <remarks>�S�Ќv��Ǘ��e�[�u������v������擾����</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Function GetZensyaKeikakuKanriData(ByVal strKeikakuNendo As String) As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Return objNendoKeikakuInputDA.SelZensyaKeikakuKanriData(strKeikakuNendo)
    End Function

    ''' <summary>
    ''' �e�x�X�̌v������擾����
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <param name="intSysYearFlg">0:�V�X�e���N�x���Ȃ��A1:�V�X�e���N�x</param>
    ''' <returns>�e�x�X�̌v����</returns>
    ''' <remarks>�e�x�X�̌v������擾����</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Function GetSitenbetuKeikakuKanriData(ByVal strKeikakuNendo As String, ByVal intSysYearFlg As Integer) As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        If intSysYearFlg = 0 Then
            '�V�X�e���N�x���Ȃ��ꍇ
            Return objNendoKeikakuInputDA.SelBusyoKanriKeikakuKanriData(strKeikakuNendo)
        Else
            '�V�X�e���N�x�̏ꍇ
            Return objNendoKeikakuInputDA.SelSitenbetuKeikakuKanriData(strKeikakuNendo)
        End If

    End Function

    ''' <summary>
    ''' �S�Ђ̍ő�o�^�����̃f�[�^���擾����
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <returns>�S�Ђ̍ő�o�^�����̃f�[�^</returns>
    ''' <remarks>�S�Ђ̍ő�o�^�����̃f�[�^���擾����</remarks>
    Public Function GetMaxZensyaKeikakuKanriData(ByVal strKeikakuNendo As String) As String
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Dim dtValue As DataTable
        Dim strReturnValue As String
        dtValue = objNendoKeikakuInputDA.SelMaxZensyaKeikakuKanriData(strKeikakuNendo)

        If dtValue Is Nothing OrElse dtValue.Rows.Count <= 0 Then
            strReturnValue = ""
        Else
            strReturnValue = Convert.ToString(dtValue.Rows(0)("add_datetime"))
        End If

        Return strReturnValue
    End Function

    ''' <summary>
    ''' �x�X�̍ő�o�^�����̃f�[�^���擾����
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <returns>�x�X�̍ő�o�^�����̃f�[�^</returns>
    ''' <remarks>�x�X�̍ő�o�^�����̃f�[�^���擾����</remarks>
    Public Function GetMaxSitenbetuKeikakuKanriData(ByVal strKeikakuNendo As String) As String
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)


        Dim dtValue As DataTable
        Dim strReturnValue As String
        dtValue = objNendoKeikakuInputDA.SelMaxSitenbetuKeikakuKanriData(strKeikakuNendo)

        If dtValue Is Nothing OrElse dtValue.Rows.Count <= 0 Then
            strReturnValue = ""
        Else
            strReturnValue = Convert.ToString(dtValue.Rows(0)("add_datetime"))
        End If

        Return strReturnValue
    End Function

    ''' <summary>
    ''' �S�Ќv��Ǘ��e�[�u���ɓo�^����
    ''' </summary>
    ''' <param name="hstValues">�o�^�f�[�^</param>
    ''' <remarks>�S�Ќv��Ǘ��e�[�u���ɓo�^����</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Sub SetZensyaKeikakuKanriData(ByVal hstValues As Hashtable)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, hstValues)

        objNendoKeikakuInputDA.InsZensyaKeikakuKanriData(hstValues)
    End Sub

    ''' <summary>
    ''' �x�X�ʌv��Ǘ��e�[�u���ɓo�^����
    ''' </summary>
    ''' <param name="dtValue">�o�^�f�[�^</param>
    ''' <remarks>�x�X�ʌv��Ǘ��e�[�u���ɓo�^����</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Sub SetSitenbetuKeikakuKanriData(ByVal dtValue As DataTable)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtValue)

        Dim i As Integer
        Dim options As New TransactionOptions

        '�������x�� �X�i�b�v�V���b�g�ɖ����I�Ɏw��
        options.IsolationLevel = IsolationLevel.Snapshot
        Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required)
            Try
                For i = 0 To dtValue.Rows.Count - 1
                    objNendoKeikakuInputDA.InsSitenbetuKeikakuKanriData(dtValue.Rows(i))
                Next
                '�����̏ꍇ
                scope.Complete()
            Catch ex As Exception
                '���s�̏ꍇ
                scope.Dispose()
                Throw ex
            End Try
        End Using
    End Sub

End Class
