Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' �v��Ǘ�_�����X�@����POPUP
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKanriKameitenSearchDA

    ''' <summary>
    ''' �u�����X�R�[�h�v�A�u�����X���v�A�u�s���{�����v�̌�����������
    ''' </summary>
    ''' <param name="strRows">�����������</param>
    ''' <param name="strYear">�N�x</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strKameitenMei">�����X��</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKeikakuKanriKameiten(ByVal strRows As String, _
                                            ByVal strYear As String, _
                                            ByVal strKameitenCd As String, _
                                            ByVal strKameitenMei As String, _
                                            ByVal blnTorikesi As Boolean, ByVal blnAimai As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strYear, _
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
            .AppendLine("	kameiten_cd ") '--�����X�R�[�h ")
            .AppendLine("	,kameiten_mei ") '--�����X�� ")
            .AppendLine("	,todouhuken_mei ") '--�s���{���� ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN torikesi = '0' THEN ")
            .AppendLine("			'' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'���' ")
            .AppendLine("		END AS torikesi ") '--��� ")
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten WITH(READCOMMITTED) ") '--�v��Ǘ�_�����X�}�X�^ ")
            .AppendLine("WHERE ")
            .AppendLine("	keikaku_nendo = @keikaku_nendo ") '--�v��N�x ")
            If strKameitenCd <> "" Then
                .AppendLine("	AND ")
                .AppendLine("   kameiten_cd LIKE @kameiten_cd") '--�����X�R�[�h ")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("	AND ")
                .AppendLine("	kameiten_mei LIKE @kameiten_mei ") '--�����X�� ")
            End If
            If blnTorikesi = True Then
                .AppendLine("	AND ")
                .AppendLine("	torikesi = @torikesi ") '--��� ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   kameiten_cd ASC ")
        End With

        '�o�����^
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, strYear)) '�����X�R�[�h
        If blnAimai Then
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 17, strKameitenCd & "%")) '�����X�R�[�h
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 42, "%" & strKameitenMei & "%")) '�����X��
        Else
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 16, strKameitenCd)) '�����X�R�[�h
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, strKameitenMei)) '�����X��
        End If
        
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '���

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKeikakuKanriKameiten", paramList.ToArray())

        Return dsReturn.Tables("dtKeikakuKanriKameiten")

    End Function

    ''' <summary>
    ''' ���������f�[�^�������擾����
    ''' </summary>
    ''' <param name="strYear">�N�x</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strKameitenMei">�����X��</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKeikakuKanriKameitenCount(ByVal strYear As String, _
                                                 ByVal strKameitenCd As String, _
                                                 ByVal strKameitenMei As String, _
                                                 ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strYear, _
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
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten WITH(READCOMMITTED) ") '--�v��Ǘ�_�����X�}�X�^ ")
            .AppendLine("WHERE ")
            .AppendLine("	keikaku_nendo = @keikaku_nendo ") '--�v��N�x ")
            If strKameitenCd <> "" Then
                .AppendLine("	AND ")
                .AppendLine("   kameiten_cd LIKE @kameiten_cd") '--�����X�R�[�h ")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("	AND ")
                .AppendLine("	kameiten_mei LIKE @kameiten_mei ") '--�����X�� ")
            End If
            If blnTorikesi = True Then
                .AppendLine("	AND ")
                .AppendLine("	torikesi = @torikesi ") '--��� ")
            End If
        End With

        '�o�����^
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, strYear)) '�����X�R�[�h
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 17, strKameitenCd & "%")) '�����X�R�[�h
        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 42, "%" & strKameitenMei & "%")) '�����X��
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                               '���

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKeikakuKanriKameitenCount", paramList.ToArray())

        Return dsReturn.Tables("dtKeikakuKanriKameitenCount")

    End Function

End Class
