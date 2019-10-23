Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' TBL_M�ی���Ђւ̐ڑ��N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class HokenKaisyaAccess
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
        Dim row As HokenKaisyaDataSet.HokenKaisyaTableRow

        commandTextSb.Append("SELECT hoken_kaisya_no,hoken_kaisya_mei")
        commandTextSb.Append("  FROM m_hoken_kaisya")
        commandTextSb.Append("  WHERE torikesi = 0")
        commandTextSb.Append("  ORDER BY hoken_kaisya_no")

        ' �f�[�^�̎擾
        Dim hokenKaisyaDataSet As New HokenKaisyaDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            hokenKaisyaDataSet, hokenKaisyaDataSet.HokenKaisyaTable.TableName)

        Dim hokenKaisyaDataTable As HokenKaisyaDataSet.HokenKaisyaTableDataTable = _
                    hokenKaisyaDataSet.HokenKaisyaTable

        If hokenKaisyaDataTable.Count <> 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In hokenKaisyaDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.hoken_kaisya_no.ToString + ":" + row.hoken_kaisya_mei, row.hoken_kaisya_no.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.hoken_kaisya_mei, row.hoken_kaisya_no.ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
