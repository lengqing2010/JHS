Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �K�w���̎擾�p�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class KaisouDataAccess
    Inherits AbsDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' �R�l�N�V�����X�g�����O���擾
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̊K�w���R�[�h��S�Ď擾���܂�
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
        Dim row As KaisouDataSet.KaisouTableRow

        commandTextSb.Append("SELECT kaisou_no,kaisou")
        commandTextSb.Append("  FROM m_kaisou WITH (READCOMMITTED) ")
        commandTextSb.Append("  ORDER BY kaisou_no ")

        ' �f�[�^�̎擾
        Dim KaisouDataSet As New KaisouDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            KaisouDataSet, KaisouDataSet.KaisouTable.TableName)

        Dim KaisouDataTable As KaisouDataSet.KaisouTableDataTable = _
                    KaisouDataSet.KaisouTable

        If KaisouDataTable.Count <> 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In KaisouDataTable

                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.kaisou_no.ToString + ":" + row.kaisou, row.kaisou_no.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.kaisou, row.kaisou_no.ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
