Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' TBL_M�񍐏��󗝂ւ̐ڑ��N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class HkksJuriAccess
    Inherits AbsDataAccess

    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̗L���ȋ敪���R�[�h��S�Ď擾���܂�
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, ByVal withSpaceRow As Boolean, Optional ByVal withCode As Boolean = True, Optional ByVal blnTorikesi As Boolean = True)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", dt, withSpaceRow, withCode)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As HkksJuriDataSet.HkksJuriTableRow

        commandTextSb.Append("SELECT hkks_jyuri_no,hkks_jyuri_jyky")
        commandTextSb.Append("  FROM m_hkks_juri")
        commandTextSb.Append("  ORDER BY hkks_jyuri_no")

        ' �f�[�^�̎擾
        Dim hkksJuriDataSet As New HkksJuriDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            hkksJuriDataSet, hkksJuriDataSet.HkksJuriTable.TableName)

        Dim hkksJuriDataTable As HkksJuriDataSet.HkksJuriTableDataTable = _
                    hkksJuriDataSet.HkksJuriTable

        If hkksJuriDataTable.Count <> 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In hkksJuriDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.hkks_jyuri_no.ToString + ":" + row.hkks_jyuri_jyky, row.hkks_jyuri_no.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.hkks_jyuri_jyky, row.hkks_jyuri_no.ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
