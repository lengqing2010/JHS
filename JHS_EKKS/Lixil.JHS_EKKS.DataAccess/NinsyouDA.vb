Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text
Public Class NinsyouDA
    ''' <summary>
    ''' �����Ǘ��}�X�^���A�J�E���g���R�[�h���擾���܂�
    ''' </summary>
    ''' <param name="strLoginUserId">���O�C�����[�U�[ID</param>
    ''' <returns>�����Ǘ��}�X�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function SelUserData(ByVal strLoginUserId As String) As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLoginUserId)

        'SQL�R�����g
        Dim sqlBuffer As New System.Text.StringBuilder

        '�߂�f�[�^�Z�b�g
        Dim dsInfo As New Data.DataSet
        'SQL���̐���
        Dim commandTextSb As New StringBuilder
        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("account_no")
            .AppendLine(",account")
            .AppendLine(",torikesi")
            .AppendLine(",simei")
            .AppendLine(",syozoku_busyo")
            .AppendLine(",bikou")
            .AppendLine(",eigyou_keikaku_kenri_sansyou")
            .AppendLine(",uri_yojitu_kanri_sansyou")
            .AppendLine(",zensya_keikaku_kengen")
            .AppendLine(",sitenbetu_nen_keikaku_kengen")
            .AppendLine(",sitenbetu_getuji_keikaku_torikomi")
            .AppendLine(",sitenbetu_getuji_keikaku_kakutei")
            .AppendLine(",keikaku_minaosi_kengen")
            .AppendLine(",keikaku_kakutei_kengen")
            .AppendLine(",keikaku_torikomi_kengen")
            .AppendLine(",sitenbetu_getuji_keikaku_minaosi ")

            .AppendLine("FROM m_kengen_kanri  WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	account = @account ")
            .AppendLine("	AND torikesi = @torikesi ")
        End With
        paramList.Add(MakeParam("@account", SqlDbType.VarChar, 16, strLoginUserId))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, "0"))

        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, _
            dsInfo, "dsInfo", paramList.ToArray)



        Return dsInfo.Tables("dsInfo")

    End Function
End Class
