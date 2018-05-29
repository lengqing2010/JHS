Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text
Public Class SitenSearchDA
    ''' <summary>
    ''' �����Ǘ��}�X�^�̃f�[�^���擾����
    ''' </summary>
    ''' <param name="strRows">�f�[�^��</param>
    ''' <param name="strBusyoMei">������</param>
    ''' <param name="blnTorikesi" >���</param>
    ''' <returns>�����Ǘ��}�X�^�f�[�^</returns>
    ''' <history>2012/11/17�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelBusyoKanri(ByVal strRows As String, _
                                     ByVal strBusyoMei As String, _
                                     ByVal blnTorikesi As Boolean, _
                                     ByVal blnAimai As Boolean, ByVal strBusyoCD As String) As Data.DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                         strBusyoMei, blnTorikesi, blnAimai)

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
            .AppendLine("     busyo_mei")                   '������
            .AppendLine("    ,busyo_cd")                    '�����R�[�h
            .AppendLine("   ,CASE WHEN torikesi = '0' THEN")
            .AppendLine("         ''")
            .AppendLine("         ELSE '���'")
            .AppendLine("         END AS Torikesi")         '���
            .AppendLine("FROM")
            .AppendLine("   m_busyo_kanri  WITH(READCOMMITTED)") '�����Ǘ��}�X�^
            .AppendLine("WHERE")
            .AppendLine("   sosiki_level = '4'")
            If strBusyoMei <> "" Then
                .AppendLine("   AND busyo_mei LIKE @strBusyoMei ")
            End If
            If strBusyoCD <> "" Then
                .AppendLine("   AND busyo_cd = @strBusyoCD ")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   busyo_mei")
        End With

        '�o�����^
        If blnAimai Then
            paramList.Add(MakeParam("@strBusyoMei", SqlDbType.VarChar, 42, "%" & strBusyoMei & "%"))    '������
        Else
            paramList.Add(MakeParam("@strBusyoMei", SqlDbType.VarChar, 40, strBusyoMei))    '������
        End If
        paramList.Add(MakeParam("@strBusyoCD", SqlDbType.VarChar, 40, strBusyoCD))    '������
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))
        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtSitenMei", paramList.ToArray())

        Return dsReturn.Tables("dtSitenMei")

    End Function
    ''' <summary>
    '''�����Ǘ��}�X�^�̃f�[�^�������擾���� 
    ''' </summary>
    ''' <param name="strBusyoMei">������</param>
    ''' <param name="blnTorikesi" >���</param>
    ''' <returns>�����Ǘ��}�X�^�f�[�^</returns>
    ''' <history>2012/11/17�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelDataCount(ByVal strBusyoMei As String, ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strBusyoMei, blnTorikesi)
        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    COUNT(busyo_cd)")
            .AppendLine("FROM")
            .AppendLine("    m_busyo_kanri  WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("     sosiki_level = '4'")
            If strBusyoMei <> "" Then
                .AppendLine("    AND busyo_mei LIKE @strBusyoMei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi")
            End If
        End With

        '�o�����^
        paramList.Add(MakeParam("@strBusyoMei", SqlDbType.VarChar, 42, "%" & strBusyoMei & "%")) '�x�X��
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '���

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtSitenMeiCount", paramList.ToArray())

        Return dsReturn.Tables("dtSitenMeiCount")
    End Function

End Class
