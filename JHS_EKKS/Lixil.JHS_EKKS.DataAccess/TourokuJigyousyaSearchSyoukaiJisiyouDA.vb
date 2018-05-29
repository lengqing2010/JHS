Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' �o�^���ƎҌ���_�v��Ǘ�_�����X�����Ɖ�w���pPOPUP
''' </summary>
''' <remarks></remarks>
Public Class TourokuJigyousyaSearchSyoukaiJisiyouDA

    ''' <summary>
    ''' �u�����X�R�[�h�v�A�u�����X���v�A�u�s���{�����v�Ɓu�����X�J�i���v�̌�����������
    ''' </summary>
    ''' <param name="strRows">�����������</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strKameitenMei">�����X��</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/7/29�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTourokuJigyousya(ByVal kbn As String, _
                                        ByVal strRows As String, _
                                        ByVal strKameitenCd As String, _
                                        ByVal strKameitenMei As String, _
                                        ByVal blnTorikesi As Boolean) As Data.DataTable

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
            .AppendLine("    MKKI.kameiten_cd")                   '�����X�R�[�h
            .AppendLine("   ,MKKI.keikakuyou_kameitenmei")                 '�����X��
            .AppendLine("   ,MT.todouhuken_mei")
            .AppendLine("   ,MKKI.tenmei_kana1")
            '.AppendLine("   ,CASE WHEN MK.torikesi = '0' THEN")
            '.AppendLine("         ''")
            '.AppendLine("         ELSE '���'")
            '.AppendLine("         END AS Torikesi")         '���
            .AppendLine("FROM")
            .AppendLine("   m_keikaku_kameiten_info AS MKKI WITH(READCOMMITTED)")  '�����X�}�X�^
            .AppendLine("   LEFT JOIN")
            .AppendLine("       m_todoufuken AS MT WITH(READCOMMITTED)") '�s���{���}�X�^
            .AppendLine("       ON MKKI.todouhuken_cd = MT.todouhuken_cd")
            .AppendLine("WHERE")
            .AppendLine("   1 = 1")
            .AppendLine("   AND MKKI.kbn = @kbn")
            If strKameitenCd <> "" Then
                .AppendLine("   AND MKKI.kameiten_cd LIKE @kameiten_cd")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("   AND MKKI.keikakuyou_kameitenmei LIKE @kameiten_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND MKKI.torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   MKKI.kameiten_cd")
        End With

        '�o�����^
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, kbn & "%")) '�敪
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 4, strKameitenCd)) '�����X�R�[�h
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))        '���
        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 80, "%" & strKameitenMei & "%")) '�����X��
        'If blnAimai Then
        '    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd & "%")) '�����X�R�[�h
        '    paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, "%" & strKameitenMei & "%")) '�����X��
        'Else
        '    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 4, strKameitenCd)) '�����X�R�[�h
        '    paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 38, strKameitenMei)) '�����X��
        'End If
        'paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '���

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
    ''' <history>2013/7/29�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTourokuJigyousyaCount(ByVal kbn As String, _
                                             ByVal strKameitenCd As String, _
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
            .AppendLine("    m_keikaku_kameiten_info WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("   1 = 1")
            .AppendLine("   AND MKKI.kbn = @kbn")
            If strKameitenCd <> "" Then
                .AppendLine("   AND kameiten_cd LIKE @kameiten_cd")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("   AND keikakuyou_kameitenmei LIKE @kameiten_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   kameiten_cd")
        End With

        '�o�����^
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, kbn & "%")) '�敪
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 6, strKameitenCd & "%")) '�����X�R�[�h
        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 80, "%" & strKameitenMei & "%")) '�����X��
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '���

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTourokuJigyousyaCount", paramList.ToArray())

        Return dsReturn.Tables("dtTourokuJigyousyaCount")

    End Function

End Class
