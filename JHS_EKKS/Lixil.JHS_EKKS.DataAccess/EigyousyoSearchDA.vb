Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' �c�Ə�����POPUP
''' </summary>
''' <remarks></remarks>
Public Class EigyousyoSearchDA

    ''' <summary>
    ''' �u�����Ǘ��}�X�^�v�e�[�v�����A�c�Ə������擾����
    ''' </summary>
    ''' <param name="strRows">�����������</param>
    ''' <param name="strEigyousyoMei">�c�Ə���</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/17�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelEigyousyoMei(ByVal strRows As String, _
                                     ByVal strEigyousyoMei As String, _
                                     ByVal blnTorikesi As Boolean, ByVal blnAimai As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strEigyousyoMei, _
                                                                                          blnTorikesi)

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
            .AppendLine("    ,CASE WHEN torikesi = '0' THEN")
            .AppendLine("         ''")
            .AppendLine("         ELSE '���'")
            .AppendLine("         END AS Torikesi")         '���
            .AppendLine("FROM")
            .AppendLine("   m_busyo_kanri  WITH(READCOMMITTED)") '�����Ǘ��}�X�^
            .AppendLine("WHERE")
            .AppendLine("   sosiki_level = '5'")
            If strEigyousyoMei <> "" Then
                .AppendLine("   AND busyo_mei LIKE @eigyousyo_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   busyo_mei")
        End With

  
        '�o�����^
        If blnAimai Then
            paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 42, "%" & strEigyousyoMei & "%"))    '������
        Else
            paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 40, strEigyousyoMei))    '������
        End If
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                                     '���

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtEigyousyoMei", paramList.ToArray())

        Return dsReturn.Tables("dtEigyousyoMei")

    End Function

    ''' <summary>
    ''' ���������f�[�^�������擾����
    ''' </summary>
    ''' <param name="strEigyousyoMei">�c�Ə���</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/17�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelEigyousyoMeiCount(ByVal strEigyousyoMei As String, _
                                         ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strEigyousyoMei, _
                                                                                          blnTorikesi)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    COUNT(busyo_mei)")
            .AppendLine("FROM")
            .AppendLine("    m_busyo_kanri  WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("     sosiki_level = '5'")
            If strEigyousyoMei <> "" Then
                .AppendLine("    AND busyo_mei LIKE @eigyousyo_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
        End With

        '�o�����^
        paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 42, "%" & strEigyousyoMei & "%")) '�c�Ə���
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                                     '���

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtEigyousyoMeiCount", paramList.ToArray())

        Return dsReturn.Tables("dtEigyousyoMeiCount")

    End Function
End Class
