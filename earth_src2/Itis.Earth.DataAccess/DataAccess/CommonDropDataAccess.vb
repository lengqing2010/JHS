Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

Public Class CommonDropDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString
    ''' <summary> ���ʃf�[�^���擾����</summary>
    Public Function SelCommonInfo(ByVal intKbn As Integer) As CommonDropDataSet.DropTableDataTable

        'DataSet�C���X�^���X�̐���
        Dim dsCommonDrop As New CommonDropDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        Select Case intKbn
            Case 1
                commandTextSb.AppendLine(" SELECT ")
                commandTextSb.AppendLine("      todouhuken_cd AS cd, ")
                commandTextSb.AppendLine("      todouhuken_mei AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_todoufuken WITH(READCOMMITTED) ")
                commandTextSb.AppendLine("  WHERE @torikesi =@torikesi ")
            Case 2
                commandTextSb.AppendLine(" SELECT ")
                commandTextSb.AppendLine("      kameiten_cd AS cd, ")
                commandTextSb.AppendLine("      kameiten_mei1 AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_kameiten WITH(READCOMMITTED) ")
                commandTextSb.AppendLine("  WHERE torikesi=@torikesi ")
            Case 3
                commandTextSb.AppendLine(" SELECT ")
                commandTextSb.AppendLine("      eigyousyo_cd AS cd, ")
                commandTextSb.AppendLine("      eigyousyo_mei AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_eigyousyo WITH(READCOMMITTED) ")
                commandTextSb.AppendLine("  WHERE torikesi=@torikesi ")
            Case 4
                commandTextSb.AppendLine(" SELECT ")
                commandTextSb.AppendLine("      keiretu_cd AS cd, ")
                commandTextSb.AppendLine("      keiretu_mei AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_keiretu WITH(READCOMMITTED) ")
                commandTextSb.AppendLine("  WHERE torikesi=@torikesi ")
            Case 5
                commandTextSb.AppendLine(" SELECT ")
                commandTextSb.AppendLine("      kbn AS cd, ")
                commandTextSb.AppendLine("      kbn_mei AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_data_kbn WITH(READCOMMITTED) ")
                commandTextSb.AppendLine("  WHERE torikesi=@torikesi ")
            Case 6
                commandTextSb.AppendLine(" SELECT ")
                commandTextSb.AppendLine("      code AS cd, ")
                commandTextSb.AppendLine("      meisyou AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_meisyou WITH(READCOMMITTED) ")
                commandTextSb.AppendLine("  WHERE meisyou_syubetu='55' ")
                commandTextSb.AppendLine("  ORDER BY hyouji_jyun")
            Case 7
                commandTextSb.AppendLine(" SELECT ")
                commandTextSb.AppendLine("      code AS cd, ")
                commandTextSb.AppendLine("      meisyou AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_kakutyou_meisyou WITH(READCOMMITTED) ")
                commandTextSb.AppendLine("  WHERE meisyou_syubetu='8' ")
                commandTextSb.AppendLine("  ORDER BY hyouji_jyun")
        End Select
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 5, "0"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonDrop, _
                    dsCommonDrop.DropTable.TableName, paramList.ToArray)

        Return dsCommonDrop.DropTable

    End Function
    
End Class
