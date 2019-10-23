Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �V�z���֏��̎擾�p�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class ShintikuTatekaeDataAccess
    Inherits AbsDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' �R�l�N�V�����X�g�����O���擾
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̐V�z���փ��R�[�h��S�Ď擾���܂�
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, _
                                         ByVal withSpaceRow As Boolean, _
                                         Optional ByVal withCode As Boolean = True, _
                                         Optional ByVal blnTorikesi As Boolean = True)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", _
                                                    dt, _
                                                    withSpaceRow, _
                                                    withCode)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As ShintikuTatekaeDataSet.ShintikuTatekaeTableRow

        commandTextSb.Append("SELECT sintiku_tatekae_no,sintiku_tatekae ")
        commandTextSb.Append("  FROM m_sintiku_tatekae WITH (READCOMMITTED) ")
        commandTextSb.Append("  ORDER BY sintiku_tatekae_no ")

        ' �f�[�^�̎擾
        Dim ShintikuTatekaeDataSet As New ShintikuTatekaeDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            ShintikuTatekaeDataSet, ShintikuTatekaeDataSet.ShintikuTatekaeTable.TableName)

        Dim ShintikuTatekaeDataTable As ShintikuTatekaeDataSet.ShintikuTatekaeTableDataTable = _
                    ShintikuTatekaeDataSet.ShintikuTatekaeTable

        If ShintikuTatekaeDataTable.Count <> 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In ShintikuTatekaeDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.sintiku_tatekae_no.ToString + ":" + row.sintiku_tatekae, row.sintiku_tatekae_no.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.sintiku_tatekae, row.sintiku_tatekae_no.ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
