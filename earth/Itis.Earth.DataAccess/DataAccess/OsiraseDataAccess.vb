Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' ���m�点�f�[�^�̎擾�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class OsiraseDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' ���m�点���R�[�h���擾���܂�
    ''' </summary>
    ''' <returns>���m�点�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetOsiraseData() As OsiraseDataSet.OsiraseTableDataTable

        Dim commandText As String = " SELECT " & _
                                    "   nengappi, " & _
                                    "   nyuuryoku_busyo, " & _
                                    "   nyuuryoku_mei, " & _
                                    "   hyouji_naiyou, " & _
                                    "   link_saki" & _
                                    " FROM " & _
                                    "    t_osirase WITH (READCOMMITTED) " & _
                                    " WHERE " & _
                                    "    torikesi = 0  " & _
                                    " ORDER BY nengappi desc"

        ' �f�[�^�̎擾
        Dim OsiraseDataSet As New OsiraseDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
            OsiraseDataSet, OsiraseDataSet.OsiraseTable.TableName)

        Dim OsiraseTable As OsiraseDataSet.OsiraseTableDataTable = OsiraseDataSet.OsiraseTable

        Return OsiraseTable

    End Function

End Class
