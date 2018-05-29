Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' ��ʌ���
''' </summary>
''' <remarks></remarks>
Public Class SyubetuSearchDA

    ''' <summary>
    ''' ��ʏ�����������
    ''' </summary>
    ''' <param name="intRows">�����������</param>
    ''' <param name="code">��ʃR�[�h</param>
    ''' <param name="mei">��ʖ�</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/03�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelSyubetu(ByVal intRows As String, _
                               ByVal code As String, _
                               ByVal mei As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          intRows, _
                                                                                          code, _
                                                                                          mei)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        With commandTextSb
            .AppendLine("SELECT          ")
            If intRows <> "max" Then
                .AppendLine("      TOP " & intRows)
            End If

            .AppendLine("   code AS cd")
            .AppendLine("   ,meisyou AS mei")
            .AppendLine("FROM            ")
            .AppendLine("   m_keikakuyou_meisyou WITH (READCOMMITTED)")

            .AppendLine("WHERE")
            .AppendLine("   meisyou_syubetu='30' ")

            If code.Trim <> "" Then
                .AppendLine(" AND code LIKE @code ")
            End If
            If mei.Trim <> "" Then
                .AppendLine(" AND meisyou LIKE @meisyou ")
            End If

            .AppendLine(" ORDER BY code ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@code", SqlDbType.VarChar, 5, code & "%"))
        paramList.Add(MakeParam("@meisyou", SqlDbType.VarChar, 82, "%" & mei & "%"))

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtSyubetuMeiCount", paramList.ToArray())

        Return dsReturn.Tables("dtSyubetuMeiCount")

    End Function

    ''' <summary>
    ''' ���������f�[�^�������擾����
    ''' </summary>
    ''' <param name="code">��ʃR�[�h</param>
    ''' <param name="mei">��ʖ�</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/03�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelSyubetuCount(ByVal code As String, ByVal mei As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          code, _
                                                                                          mei)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        With commandTextSb
            ' �����X�}�X�g����Sql 
            .AppendLine("SELECT          ")
            .AppendLine("   count(code) AS CNT")
            '.AppendLine("   ,meisyou AS mei")
            .AppendLine("FROM            ")
            .AppendLine("   m_keikakuyou_meisyou WITH (READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine(" meisyou_syubetu='30' ")

            If code.Trim <> "" Then
                .AppendLine(" AND code LIKE @code ")
            End If
            If mei.Trim <> "" Then
                .AppendLine(" AND meisyou LIKE @meisyou ")
            End If
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@code", SqlDbType.VarChar, 5, code & "%"))
        paramList.Add(MakeParam("@meisyou", SqlDbType.VarChar, 82, "%" & mei & "%"))

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtSyubetuMeiCount", paramList.ToArray())

        Return dsReturn.Tables("dtSyubetuMeiCount")

    End Function
End Class
