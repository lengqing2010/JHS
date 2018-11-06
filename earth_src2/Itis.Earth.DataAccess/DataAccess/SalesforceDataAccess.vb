Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports Itis.Earth.DataAccess
Imports System.text
Imports System.Data.SqlClient

Public Class SalesforceDataAccess
    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    '''salesforce����_�ҏW�񊈐��t���O���擾���܂�
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSalesforceHikasseiFlg(ByVal strKameitenCd As String) As String



        ' DataSet�C���X�^���X�̐���()
        Dim dsCommonSearch As New DataSet

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        Dim sb As New System.Text.StringBuilder
        sb.Append("select isnull(salesforce_hikassei_flg,'') from m_kameiten a " & vbCrLf)
        sb.Append("left join m_data_kbn b " & vbCrLf)
        sb.Append("on a.kbn = b.kbn " & vbCrLf)
        sb.Append("where kameiten_cd = @kameiten_cd")

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 6, strKameitenCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, sb.ToString(), dsCommonSearch, _
                    "tmp", paramList.ToArray)

        If dsCommonSearch.Tables(0).Rows.Count = 0 Then
            Return ""
        Else
            Return dsCommonSearch.Tables(0).Rows(0).Item(0).ToString
        End If

    End Function


    ''' <summary>
    '''salesforce����_�ҏW�񊈐��t���O���擾���܂�
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSalesforceHikasseiFlgByKbn(ByVal kbn As String) As String

        ' DataSet�C���X�^���X�̐���()
        Dim dsCommonSearch As New DataSet

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        Dim sb As New System.Text.StringBuilder
        sb.Append("select isnull(salesforce_hikassei_flg,'') from m_data_kbn a " & vbCrLf)
        sb.Append("where kbn = @kbn")

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, kbn))

        ' �������s
        FillDataset(connStr, CommandType.Text, sb.ToString(), dsCommonSearch, _
                    "tmp", paramList.ToArray)

        If dsCommonSearch.Tables(0).Rows.Count = 0 Then
            Return ""
        Else
            Return dsCommonSearch.Tables(0).Rows(0).Item(0).ToString
        End If

    End Function
End Class
