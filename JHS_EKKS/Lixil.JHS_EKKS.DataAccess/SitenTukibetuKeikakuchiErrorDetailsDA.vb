Option Explicit On
Option Strict On
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' �x�X ���ʌv��l EXCEL�捞�G���[
''' </summary>
''' <remarks></remarks>
Public Class SitenTukibetuKeikakuchiErrorDetailsDA

    ''' <summary>
    ''' �G���[���e���擾����
    ''' </summary>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <returns>Data.DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/27�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelErrorJyouhou(ByVal strSyoriDatetime As String, ByVal strEdiJouhouSakuseiDate As String) As Data.DataTable

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   TOP 100")
            .AppendLine("    gyou_no")                   '�sNO
            .AppendLine("   ,error_naiyou")              '�G���[���e
            .AppendLine("FROM")
            .AppendLine("   t_siten_tukibetu_torikomi_error WITH(READCOMMITTED)")  '�x�X�ʌ��ʌv��Ǘ��\_�捞�G���[���e�[�u��
            .AppendLine("WHERE")
            '.AppendLine("   syori_datetime = @syori_datetime")
            .AppendLine("       CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime")
            .AppendLine("   AND edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date")
            .AppendLine("ORDER BY")
            .AppendLine("   gyou_no")
        End With

        '�o�����^
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoriDatetime)) '��������
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate)) 'EDI���쐬��

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtErrorJyouhouCount", paramList.ToArray())

        Return dsReturn.Tables("dtErrorJyouhouCount")

    End Function

    ''' <summary>
    ''' �G���[���e�̃f�[�^�������擾����
    ''' </summary>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <returns>Data.DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/27�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelErrorJyouhouCount(ByVal strSyoriDatetime As String, ByVal strEdiJouhouSakuseiDate As String) As Data.DataTable

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   COUNT(*)")              '�G���[���e
            .AppendLine("FROM")
            .AppendLine("   t_siten_tukibetu_torikomi_error WITH(READCOMMITTED)")  '�x�X�ʌ��ʌv��Ǘ��\_�捞�G���[���e�[�u��
            .AppendLine("WHERE")
            '.AppendLine("   syori_datetime = @syori_datetime")
            .AppendLine("       CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime")
            .AppendLine("   AND edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date")
        End With

        '�o�����^
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoriDatetime)) '��������
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate)) 'EDI���쐬��

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtErrorJyouhouCount", paramList.ToArray())

        Return dsReturn.Tables("dtErrorJyouhouCount")

    End Function

End Class
