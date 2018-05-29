Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text
Public Class CommonDA
    ''' <summary>LOG�̐V�K����</summary>
    Public Function InsUrlLog(ByVal strUrl As String, ByVal strUserId As String, ByVal strSousa As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strUrl, strUserId, strSousa)
       

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)


        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" t_sansyou_rireki_kanri ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine("  (login_user_id,")
        commandTextSb.AppendLine("  url, ")
        commandTextSb.AppendLine("  sousa, ")
        commandTextSb.AppendLine("  add_datetime) ")
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  @login_user_id ,")
        commandTextSb.AppendLine("  @url, ")
        commandTextSb.AppendLine("  @sousa, ")
        commandTextSb.AppendLine("  GETDATE() ")

        '�p�����[�^�̐ݒ�

        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId))
        paramList.Add(MakeParam("@url", SqlDbType.VarChar, 550, strUrl))
        paramList.Add(MakeParam("@sousa", SqlDbType.VarChar, 100, strSousa))
        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        If ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), paramList.ToArray) > 0 Then
            '�I������
            commandTextSb = Nothing
            Return True
        Else
            '�I������
            commandTextSb = Nothing
            Return False
        End If

        
    End Function
    ''' <summary>�V�X�e�����擾</summary>
    Public Function SelSystemDate() As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        '�߂�f�[�^�Z�b�g
        Dim dsInfo As New Data.DataSet
        Dim commandTextSb As New System.Text.StringBuilder



        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" GETDATE() ")

        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, _
                    dsInfo, "dsInfo")

        Return dsInfo.Tables("dsInfo")
    End Function

#Region "�Ɩ�����"
    ''' <summary>
    ''' �v��p_���̃}�X�^����v��N�x���X�g���擾����
    ''' </summary>
    ''' <returns>�v��N�x���X�g</returns>
    ''' <remarks>�v��p_���̃}�X�^����v��N�x���X�g���擾����</remarks>
    ''' <history>
    ''' <para>2012/11/14 P-44979 ���V �V�K�쐬 </para>
    ''' </history>
    Public Function SelKeikakuNendoData() As DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" SELECT ")
            .AppendLine("         CONVERT(VARCHAR,code) AS [code], ")
            .AppendLine("         meisyou ")
            .AppendLine(" FROM ")
            .AppendLine("         m_keikakuyou_meisyou WITH(READCOMMITTED)  ")
            .AppendLine(" WHERE ")
            .AppendLine("         meisyou_syubetu = '01' ")
            .AppendLine(" ORDER BY ")
            .AppendLine("         code ASC ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "01"))     '���̎��

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "dsInfo")

        Return dsInfo.Tables(0)
    End Function
#End Region
End Class
