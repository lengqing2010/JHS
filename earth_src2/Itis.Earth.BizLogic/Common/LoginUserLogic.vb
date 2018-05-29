Imports Itis.Earth.DataAccess

''' <summary>
''' ���O�C�����[�U�[���̎擾�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class LoginUserLogic

    ''' <summary>
    ''' ���O�C�����[�U�[ID���EARTH�̃A�J�E���g�����擾���܂�<br/>
    ''' �A�J�E���g���̎擾�Ɏ��s�����ꍇ�Afalse��ԋp���܂�<br/>
    ''' </summary>
    ''' <param name="login_user_id">���O�C�����[�U�[ID</param>
    ''' <param name="user_info">EARTH�̃��[�U�[����ێ����郌�R�[�h</param>
    ''' <returns>�擾���� true:���� false:���s</returns>
    ''' <remarks>���[�U�[���̎擾�Ɏ��s�����ꍇ�AEARTH�S�@�\�̎g�p�s��</remarks>
    Public Function makeUserInfo(ByVal login_user_id As String, _
                                 ByRef user_info As LoginUserInfo) As Boolean

        ' �A�J�E���g�}�X�^�����p
        Dim data_access As New AccountDataAccess
        Dim data_table As New AccountDataSet.AccountTableDataTable

        data_table = data_access.getAccountData(login_user_id)

        ' �w�肵�����R�[�h�N���X�Ɍ��ʂ��i�[����ArrayList���擾���܂�
        Dim list As ArrayList = DataMappingHelper.Instance.getMapArray(user_info.GetType(), data_table)

        ' �����������Ă��P���ڂ�Ԃ��i�^�p���[����1:1�j
        If list.Count > 0 Then
            user_info = list(0)
        Else
            ' �ݒ�Ɏ��s�����ꍇ�AFalse��Ԃ�
            Return False
        End If

        Return True

    End Function


End Class
