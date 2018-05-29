Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' �s���{������POPUP
''' </summary>
''' <remarks></remarks>
Public Class TodoufukenSearchDA

    ''' <summary>
    ''' �u�s���{�����v�̌�����������
    ''' </summary>
    ''' <param name="strRows">�����������</param>
    ''' <param name="strTodoufukenMei">�s���{����</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    '''  <history>2012/11/19�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTodoufukenMei(ByVal strRows As String, _
                                     ByVal strTodoufukenMei As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strTodoufukenMei)

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
            .AppendLine("    todouhuken_cd")                   '�s���{���R�[�h
            .AppendLine("   ,todouhuken_mei")                  '�s���{����
            .AppendLine("FROM")
            .AppendLine("   m_todoufuken WITH(READCOMMITTED)")  '�s���{���}�X�^
            .AppendLine("WHERE")
            .AppendLine("1 = 1")

            If strTodoufukenMei <> "" Then
                .AppendLine("   AND todouhuken_mei LIKE @todoufuken_mei")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   todouhuken_mei")
        End With

        '�o�����^
        paramList.Add(MakeParam("@todoufuken_mei", SqlDbType.VarChar, 42, "%" & strTodoufukenMei & "%")) '�s���{����

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTodoufukenMei", paramList.ToArray())

        Return dsReturn.Tables("dtTodoufukenMei")

    End Function

    ''' <summary>
    ''' ���������f�[�^�������擾����
    ''' </summary>
    ''' <param name="strTodoufukenMei">�s���{����</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTodoufukenMeiCount(ByVal strTodoufukenMei As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strTodoufukenMei)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    COUNT(todouhuken_mei)")
            .AppendLine("FROM")
            .AppendLine("    m_todoufuken WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("     1 = 1")
            If strTodoufukenMei <> "" Then
                .AppendLine("    AND todouhuken_mei LIKE @todoufuken_mei")
            End If
        End With

        '�o�����^
        paramList.Add(MakeParam("@todoufuken_mei", SqlDbType.VarChar, 42, "%" & strTodoufukenMei & "%")) '�s���{����

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTodoufukenMeiCount", paramList.ToArray())

        Return dsReturn.Tables("dtTodoufukenMeiCount")

    End Function

End Class
