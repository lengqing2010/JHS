''' <summary>
''' ���O�C�����[�U�[���̎擾�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class LoginUserLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' ���O�C�����[�U�[ID���EARTH�̃A�J�E���g�����擾���܂�<br/>
    ''' �A�J�E���g���̎擾�Ɏ��s�����ꍇ�Afalse��ԋp���܂�<br/>
    ''' </summary>
    ''' <param name="loginUserId">���O�C�����[�U�[ID</param>
    ''' <param name="userInfo">EARTH�̃��[�U�[����ێ����郌�R�[�h</param>
    ''' <returns>�擾���� true:���� false:���s</returns>
    ''' <remarks>���[�U�[���̎擾�Ɏ��s�����ꍇ�AEARTH�S�@�\�̎g�p�s��</remarks>
    Public Function MakeUserInfo(ByVal loginUserId As String, _
                                 ByRef userInfo As LoginUserInfo) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".MakeUserInfo", _
                                            loginUserId, _
                                            userInfo)

        ' �A�J�E���g�}�X�^�����p
        Dim dataAccess As New AccountDataAccess
        Dim dataTable As New AccountDataSet.AccountTableDataTable

        dataTable = dataAccess.GetAccountData(loginUserId)

        ' �w�肵�����R�[�h�N���X�Ɍ��ʂ��i�[����List(Of LoginUserInfo)���擾���܂�
        Dim list As List(Of LoginUserInfo) = DataMappingHelper.Instance.getMapArray(Of LoginUserInfo)(userInfo.GetType(), dataTable)

        ' �����������Ă��P���ڂ�Ԃ��i�^�p���[����1:1�j
        If list.Count > 0 Then
            userInfo = list(0)
        Else
            ' �ݒ�Ɏ��s�����ꍇ�AFalse��Ԃ�
            Return False
        End If

        Return True

    End Function


End Class
