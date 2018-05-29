Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

Public Class KeikakuKanriErrorDetailsDA

    ''' <summary>
    '''�v��Ǘ��\_�捞�G���[���擾
    ''' </summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <returns>�v��Ǘ��\_�捞�G���[���e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/26�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Public Function SelKeikakuTorikomiError(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDatetime As String) As DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
                                                                                          , strEdiJouhouSakuseiDate _
                                                                                          , strSyoriDatetime)
        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   TOP 100 ")
            .AppendLine("	edi_jouhou_sakusei_date ")
            .AppendLine("	,gyou_no ")
            .AppendLine("	,syori_datetime ")
            .AppendLine("	,error_naiyou ")
            .AppendLine("FROM ")
            .AppendLine("	t_keikaku_torikomi_error WITH (READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            .AppendLine("	AND CONVERT(varchar(100), syori_datetime, 112) + REPLACE(CONVERT(varchar(100), syori_datetime, 114), ':', '') = @syori_datetime ")
            .AppendLine("ORDER BY ")
            .AppendLine("	gyou_no ")
        End With

        paramList.Add(SQLHelper.MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))
        paramList.Add(SQLHelper.MakeParam("@syori_datetime", SqlDbType.VarChar, 20, strSyoriDatetime))

        '���s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "KeikakuTorikomiError", paramList.ToArray)

        '�߂�l
        Return dsReturn.Tables("KeikakuTorikomiError")

    End Function

    ''' <summary>
    '''�v��Ǘ��\_�捞�G���[��񌏐��擾
    ''' </summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <returns>�v��Ǘ��\_�捞�G���[���e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/26�@�k�o(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Public Function SelKeikakuTorikomiErrorCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDatetime As String) As String

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
                                                                                          , strEdiJouhouSakuseiDate _
                                                                                          , strSyoriDatetime)
        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	Count(edi_jouhou_sakusei_date) AS count ")
            .AppendLine("FROM ")
            .AppendLine("	t_keikaku_torikomi_error WITH (READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            .AppendLine("	AND CONVERT(varchar(100), syori_datetime, 112) + REPLACE(CONVERT(varchar(100), syori_datetime, 114), ':', '') = @syori_datetime ")
        End With

        paramList.Add(SQLHelper.MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))
        paramList.Add(SQLHelper.MakeParam("@syori_datetime", SqlDbType.VarChar, 20, strSyoriDatetime))

        '���s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "KeikakuTorikomiErrorCount", paramList.ToArray)

        '�߂�l
        Return dsReturn.Tables("KeikakuTorikomiErrorCount").Rows(0).Item("count").ToString

    End Function

End Class
