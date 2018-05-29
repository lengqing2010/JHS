Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' �o�^���Ǝ�POPUP
''' </summary>
''' <remarks></remarks>
Public Class TourokuJigyousyaSearchDA

    ''' <summary>
    ''' �u�����X�R�[�h�v�A�u�����X���v�A�u�s���{�����v�Ɓu�����X�J�i���v�̌�����������
    ''' </summary>
    ''' <param name="strRows">�����������</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strKameitenMei">�����X��</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTourokuJigyousya(ByVal strRows As String, _
                                        ByVal strKameitenCd As String, _
                                        ByVal strKameitenMei As String, _
                                        ByVal blnTorikesi As Boolean, ByVal blnAimai As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
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
            .AppendLine("    MK.kameiten_cd")                   '�����X�R�[�h
            .AppendLine("   ,MK.kameiten_mei1")                 '�����X��
            .AppendLine("   ,MT.todouhuken_mei")
            .AppendLine("   ,MK.tenmei_kana1")
            .AppendLine("   ,CASE WHEN MK.torikesi = '0' THEN")
            .AppendLine("         ''")
            .AppendLine("         ELSE '���'")
            .AppendLine("         END AS Torikesi")         '���
            .AppendLine("FROM")
            .AppendLine("   m_kameiten AS MK WITH(READCOMMITTED)")  '�����X�}�X�^
            .AppendLine("   LEFT JOIN")
            .AppendLine("       m_todoufuken AS MT WITH(READCOMMITTED)") '�s���{���}�X�^
            .AppendLine("       ON MK.todouhuken_cd = MT.todouhuken_cd")
            .AppendLine("WHERE")
            If strKameitenCd <> "" Then
                .AppendLine("   MK.kameiten_cd LIKE @kameiten_cd")
            Else
                .AppendLine("   1 = 1")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("   AND (MK.kameiten_mei1 LIKE @kameiten_mei")
                .AppendLine("   OR MK.kameiten_mei2 LIKE @kameiten_mei)")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND MK.torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   MK.kameiten_cd")
        End With

        '�o�����^
        If blnAimai Then
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd & "%")) '�����X�R�[�h
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, "%" & strKameitenMei & "%")) '�����X��
        Else
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 4, strKameitenCd)) '�����X�R�[�h
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 38, strKameitenMei)) '�����X��
        End If
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '���

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTourokuJigyousya", paramList.ToArray())

        Return dsReturn.Tables("dtTourokuJigyousya")

    End Function

    ''' <summary>
    ''' ���������f�[�^�������擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strKameitenMei">�����X��</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTourokuJigyousyaCount(ByVal strKameitenCd As String, _
                                             ByVal strKameitenMei As String, _
                                             ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
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
            .AppendLine("    COUNT(kameiten_cd)")
            .AppendLine("FROM")
            .AppendLine("    m_kameiten WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            If strKameitenCd <> "" Then
                .AppendLine("   kameiten_cd LIKE @kameiten_cd")
            Else
                .AppendLine("   1 = 1")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("   AND (kameiten_mei1 LIKE @kameiten_mei")
                .AppendLine("   OR kameiten_mei2 LIKE @kameiten_mei)")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
        End With

        '�o�����^
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 6, strKameitenCd & "%")) '�����X�R�[�h
        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 42, "%" & strKameitenMei & "%")) '�����X��
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '���

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTourokuJigyousyaCount", paramList.ToArray())

        Return dsReturn.Tables("dtTourokuJigyousyaCount")

    End Function

End Class
