Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' �c�Ə�����_�v��Ǘ�_�����X�����Ɖ�w���pPOPUP
''' </summary>
''' <remarks></remarks>
Public Class EigyousyoSearchSyoukaiJisiyouDA

    ''' <summary>
    ''' �u�c�Ə��}�X�^�v����A�c�Ə��R�[�h���擾����
    ''' </summary>
    ''' <param name="strRows">�����������</param>
    ''' <param name="strEigyousyoCd">�c�Ə��R�[�h</param>
    ''' <param name="strEigyousyoMei">�c�Ə���</param>
    ''' <param name="blnTorikesi">���ʂ̑މ�������X���`�F�b�N</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/07/18�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelEigyousyo(ByVal strRows As String, _
                                   ByVal strEigyousyoCd As String, _
                                   ByVal strEigyousyoMei As String, _
                                   ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strEigyousyoCd, _
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
            .AppendLine("     eigyousyo_cd")                   '�c�Ə��R�[�h
            .AppendLine("    ,eigyousyo_mei")                  '�c�Ə���
            .AppendLine("FROM")
            .AppendLine("   m_eigyousyo  WITH(READCOMMITTED)") '�c�Ə��}�X�^
            .AppendLine("WHERE")
            .AppendLine("   1 = 1")
            If strEigyousyoCd <> "" Then
                .AppendLine("   AND eigyousyo_cd LIKE @eigyousyo_cd")
            End If
            If strEigyousyoMei <> "" Then
                .AppendLine("   AND eigyousyo_mei LIKE @eigyousyo_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   eigyousyo_cd")
        End With


        '�o�����^
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 6, strEigyousyoCd & "%"))            '�c�Ə��R�[�h
        paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 42, "%" & strEigyousyoMei & "%"))   '�c�Ə���
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                                    '���

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtEigyousyo", paramList.ToArray())

        Return dsReturn.Tables("dtEigyousyo")

    End Function

    ''' <summary>
    ''' ���������f�[�^�������擾����
    ''' </summary>
    ''' <param name="strEigyousyoCd">�c�Ə��R�[�h</param>
    ''' <param name="strEigyousyoMei">�c�Ə���</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/07/18�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelEigyousyoCount(ByVal strEigyousyoCd As String, _
                                         ByVal strEigyousyoMei As String, _
                                         ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strEigyousyoCd, _
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
            .AppendLine("    COUNT(eigyousyo_cd)")
            .AppendLine("FROM")
            .AppendLine("    m_eigyousyo  WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("     1 = 1")
            If strEigyousyoCd <> "" Then
                .AppendLine("   AND eigyousyo_cd LIKE @eigyousyo_cd")
            End If
            If strEigyousyoMei <> "" Then
                .AppendLine("   AND eigyousyo_mei LIKE @eigyousyo_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
        End With

        '�o�����^
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strEigyousyoCd & "%"))            '�c�Ə��R�[�h
        paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 40, "%" & strEigyousyoMei & "%"))   '�c�Ə���
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                                    '���

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtEigyousyoCount", paramList.ToArray())

        Return dsReturn.Tables("dtEigyousyoCount")

    End Function

End Class
