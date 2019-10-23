Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �ۏ؏�DB�i�[��Ǘ��}�X�^�ւ̐ڑ��N���X
''' </summary>
''' <remarks></remarks>
Public Class HosyousyoDbDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �ۏ؏�DB�����擾
    ''' </summary>
    ''' <param name="kubun"></param>
    ''' <param name="ym"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHosyousyoDbInfo(ByVal kubun As String, ByVal ym As String) As DataTable
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyousyoDbInfo", kubun, ym)

        Dim cmnDtAcc As New CmnDataAccess
        Dim cmdTextSb As New StringBuilder()

        '�p�����[�^
        Const strParamKbn As String = "@KUBUN"
        Const strParamDate As String = "@DATE"

        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("      kbn ")
        cmdTextSb.AppendLine("    , date_from ")
        cmdTextSb.AppendLine("    , date_to ")
        cmdTextSb.AppendLine("    , kakunousaki_file_pass ")
        cmdTextSb.AppendLine("    , add_login_user_id ")
        cmdTextSb.AppendLine("    , add_datetime ")
        cmdTextSb.AppendLine("    , upd_login_user_id ")
        cmdTextSb.AppendLine("    , upd_datetime ")
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      m_hosyousyo_db_kanri ")
        cmdTextSb.AppendLine(" WHERE ")
        cmdTextSb.AppendLine("      kbn = " & strParamKbn)
        cmdTextSb.AppendLine("  AND date_from <= " & strParamDate)
        cmdTextSb.AppendLine("  AND date_to >= " & strParamDate)

        ' �p�����[�^�֐ݒ�
        Dim sqlParams() As SqlParameter = {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kubun), _
                                           SQLHelper.MakeParam(strParamDate, SqlDbType.VarChar, 6, ym)}

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, sqlParams)
    End Function

End Class
