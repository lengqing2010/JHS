Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �ް��j����ʏ��̎擾�p�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class DataHakiDataAccess
    Inherits AbsDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' �R�l�N�V�����X�g�����O���擾
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p���ް��j����ʃ��R�[�h��S�Ď擾���܂�
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, _
                                         ByVal withSpaceRow As Boolean, _
                                         Optional ByVal withCode As Boolean = True, _
                                         Optional ByVal blnTorikesi As Boolean = True)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", _
                                                    dt, _
                                                    withSpaceRow, _
                                                    withCode)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As DataHakiDataSet.DataHakiTableRow

        commandTextSb.Append("SELECT data_haki_no,haki_syubetu")
        commandTextSb.Append("  FROM m_data_haki WITH (READCOMMITTED) ")
        commandTextSb.Append("  ORDER BY data_haki_no ")

        ' �f�[�^�̎擾
        Dim DataHakiDataSet As New DataHakiDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            DataHakiDataSet, DataHakiDataSet.DataHakiTable.TableName)

        Dim DataHakiDataTable As DataHakiDataSet.DataHakiTableDataTable = _
                    DataHakiDataSet.DataHakiTable

        If DataHakiDataTable.Count <> 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "0", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In DataHakiDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.data_haki_no.ToString + ":" + row.haki_syubetu, row.data_haki_no.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.haki_syubetu, row.data_haki_no.ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
