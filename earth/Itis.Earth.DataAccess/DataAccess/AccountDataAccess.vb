Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �F�؁E�A�J�E���g�f�[�^�̎擾�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class AccountDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �A�J�E���g���R�[�h���擾���܂�
    ''' </summary>
    ''' <param name="loginUserId">���O�C�����[�U�[ID</param>
    ''' <returns>�A�J�E���g�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetAccountData(ByVal loginUserId As String) As AccountDataSet.AccountTableDataTable

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CreateRow", _
                                            loginUserId)

        ' �p�����[�^
        Const strParamLoginUser As String = "@LOGINUSER"

        Dim commandText As String = " SELECT  " & _
                                    "     N.login_user_id, " & _
                                    "     A.account_no, " & _
                                    "     A.account, " & _
                                    "     A.simei, " & _
                                    "     A.bikou, " & _
                                    "     A.irai_gyoumu_kengen, " & _
                                    "     A.kekka_gyoumu_kengen, " & _
                                    "     A.hosyou_gyoumu_kengen, " & _
                                    "     A.hkks_gyoumu_kengen, " & _
                                    "     A.koj_gyoumu_kengen, " & _
                                    "     A.keiri_gyoumu_kengen, " & _
                                    "     A.kaiseki_master_kanri_kengen, " & _
                                    "     A.eigyou_master_kanri_kengen, " & _
                                    "     A.kkk_master_kanri_kengen, " & _
                                    "     A.hansoku_uri_kengen, " & _
                                    "     A.data_haki_kengen, " & _
                                    "     A.system_kanrisya_kengen, " & _
                                    "     A.sinki_nyuuryoku_kengen, " & _
                                    "     A.hattyuusyo_kanri_kengen, " & _
                                    "     M.Department, " & _
                                    "     M.DisplayName " & _
                                    " FROM  " & _
                                    "     m_jiban_ninsyou N WITH (READCOMMITTED) " & _
                                    " LEFT OUTER JOIN " & _
                                    "     m_account A WITH (READCOMMITTED) ON A.account_no = N.account_no " & _
                                    " LEFT OUTER JOIN " & _
                                    "     m_jhs_mailbox M WITH (READCOMMITTED) ON M.PrimaryWindowsNTAccount = N.login_user_id " & _
                                    " WHERE " & _
                                    "     ISNULL(A.torikesi,0) = 0 " & _
                                    " AND " & _
                                    "     N.login_user_id = " & strParamLoginUser

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamLoginUser, SqlDbType.VarChar, 30, loginUserId)}

        ' �f�[�^�̎擾
        Dim accountDataSet As New AccountDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
            accountDataSet, accountDataSet.AccountTable.TableName, commandParameters)

        Dim AccountTable As AccountDataSet.AccountTableDataTable = AccountDataSet.AccountTable

        Return AccountTable

    End Function

End Class
