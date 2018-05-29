Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' �n��R�[�h����POPUP
''' </summary>
''' <remarks></remarks>
Public Class KeiretuSearchDA

    ''' <summary>
    ''' �u�n��}�X�^�v�e�[�v�����A�n������擾����
    ''' </summary>
    ''' <param name="strRows">�����������</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strKeiretuMei">�n��</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/15�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKiretuJyouhou(ByVal strRows As String, _
                                     ByVal strKeiretuCd As String, _
                                     ByVal strKeiretuMei As String, _
                                     ByVal blnTorikesi As Boolean, ByVal blnAimai As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strKeiretuCd, _
                                                                                          strKeiretuMei, _
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
            .AppendLine("    keiretu_cd")                   '�n��R�[�h
            .AppendLine("   ,keiretu_mei")                  '�n��
            .AppendLine("   ,CASE WHEN torikesi = '0' THEN")
            .AppendLine("         ''")
            .AppendLine("         ELSE '���'")
            .AppendLine("         END AS Torikesi")         '���
            .AppendLine("FROM")
            .AppendLine("   m_keiretu WITH(READCOMMITTED)") '�n��}�X�^
            .AppendLine("WHERE")
            If strKeiretuCd <> "" Then
                .AppendLine("   keiretu_cd LIKE @keiretu_cd")
            Else
                .AppendLine("1 = 1")
            End If
            If strKeiretuMei <> "" Then
                .AppendLine("   AND keiretu_mei LIKE @keiretu_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   keiretu_cd")
        End With

        '�o�����^
        If blnAimai Then
            paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 6, strKeiretuCd & "%"))    '�n��R�[�h
            paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 42, "%" & strKeiretuMei & "%")) '�n��
        Else
            paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, strKeiretuCd))    '�n��R�[�h
            paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 40, strKeiretuMei)) '�n��
        End If
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '���

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKiretuJyouhou", paramList.ToArray())

        Return dsReturn.Tables("dtKiretuJyouhou")

    End Function

    ''' <summary>
    ''' ���������f�[�^�������擾����
    ''' </summary>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strKeiretuMei">�n��</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/16�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKiretuJyouhouCount(ByVal strKeiretuCd As String, _
                                          ByVal strKeiretuMei As String, _
                                          ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strKeiretuCd, _
                                                                                          strKeiretuMei, _
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
            .AppendLine("    COUNT(keiretu_cd)")
            .AppendLine("FROM")
            .AppendLine("    m_keiretu WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            If strKeiretuCd <> "" Then
                .AppendLine("     keiretu_cd LIKE @keiretu_cd")
            Else
                .AppendLine("     1 = 1")
            End If
            If strKeiretuMei <> "" Then
                .AppendLine("    AND keiretu_mei LIKE @keiretu_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi")
            End If
        End With

        '�o�����^
        paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 6, strKeiretuCd & "%"))          '�n��R�[�h
        paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 42, "%" & strKeiretuMei & "%")) '�n��
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '���

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKiretuJyouhouCount", paramList.ToArray())

        Return dsReturn.Tables("dtKiretuJyouhouCount")

    End Function

End Class
