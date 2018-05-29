Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text
Public Class EigyouManSearchDA
    ''' <summary>
    ''' ���[�U�[�f�[�^���擾����
    ''' </summary>
    ''' <param name="strRows">�����������</param>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <param name="strUserName">���[�U�[��</param>
    ''' <param name="blnTorikesi" >���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/15�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelUserInfo(ByVal strRows As String, _
                                     ByVal strUserId As String, _
                                     ByVal strUserName As String, _
                                     ByVal blnTorikesi As Boolean, ByVal blnAimai As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strUserId, _
                                                                                          strUserName, blnTorikesi)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            If strRows = "0" Then
                .AppendLine("   TOP 100")
            End If
            .AppendLine("     msyou.login_user_id")                                '���[�U�[ID
            .AppendLine("    ,mbox.DisplayName")                                   '���[�U�[��
            .AppendLine("   ,CASE WHEN torikesi = '0' THEN")
            .AppendLine("         ''")
            .AppendLine("         ELSE '���'")
            .AppendLine("         END AS Torikesi")                                '���
            .AppendLine("FROM")
            .AppendLine("     m_jiban_ninsyou msyou  WITH(READCOMMITTED)")         '�n�ՔF�؃}�X�^
            .AppendLine("INNER JOIN ")
            .AppendLine("     m_jhs_mailbox  mbox WITH(READCOMMITTED)")            '�Ј��A�J�E���g���}�X�^
            .AppendLine("ON")
            .AppendLine("     msyou.login_user_id=mbox.PrimaryWindowsNTAccount ")
            .AppendLine("WHERE")
            .AppendLine("msyou.login_user_id  is not null")
            If strUserId <> "" Then
                .AppendLine("AND msyou.login_user_id LIKE @strUserId")
            End If
            If strUserName <> "" Then
                .AppendLine("AND mbox.DisplayName LIKE @strUserName")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   msyou.login_user_id ASC ")
        End With

        '�o�����^
        If blnAimai Then
            paramList.Add(MakeParam("@strUserId", SqlDbType.VarChar, 30, strUserId & "%"))            '���[�U�[ID
            paramList.Add(MakeParam("@strUserName", SqlDbType.VarChar, 130, "%" & strUserName & "%")) '���[�U�[��
        Else
            paramList.Add(MakeParam("@strUserId", SqlDbType.VarChar, 31, strUserId))            '���[�U�[ID
            paramList.Add(MakeParam("@strUserName", SqlDbType.VarChar, 128, strUserName)) '���[�U�[��
        End If

        
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                             '���

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtUser", paramList.ToArray())

        Return dsReturn.Tables("dtUser")

    End Function

    ''' <summary>
    ''' ���������f�[�^�������擾����
    ''' </summary>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <param name="strUserName">���[�U�[��</param>
    ''' <param name="blnTorikesi" >���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/16�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelUserCount(ByVal strUserId As String, _
                                          ByVal strUserName As String, _
                                          ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strUserId, _
                                                                                          strUserName, blnTorikesi)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("    COUNT(login_user_id) ")
            .AppendLine("FROM")
            .AppendLine("     m_jiban_ninsyou msyou  WITH(READCOMMITTED)")              '�n�ՔF�؃}�X�^
            .AppendLine("INNER JOIN ")
            .AppendLine("     m_jhs_mailbox  mbox WITH(READCOMMITTED)")            '�Ј��A�J�E���g���}�X�^
            .AppendLine("ON")
            .AppendLine("     msyou.login_user_id=mbox.PrimaryWindowsNTAccount ")
            .AppendLine("WHERE")
            .AppendLine("   msyou.login_user_id IS NOT NULL")
            If strUserId <> "" Then
                .AppendLine(" AND msyou.login_user_id LIKE @strUserId")
            End If
            If strUserName <> "" Then
                .AppendLine(" AND mbox.DisplayName LIKE @strUserName")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi")
            End If
        End With

        '�o�����^
        paramList.Add(MakeParam("@strUserId", SqlDbType.VarChar, 31, strUserId & "%"))    '���[�U�[�R�[�h
        paramList.Add(MakeParam("@strUserName", SqlDbType.VarChar, 130, "%" & strUserName & "%")) '���[�U�[��
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '���

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtCount", paramList.ToArray())

        Return dsReturn.Tables("dtCount")

    End Function
End Class
