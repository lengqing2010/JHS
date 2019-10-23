Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' TBL_M�ް��敪�ւ̐ڑ��N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class KubunDataAccess2
    Inherits AbsDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̗L���ȋ敪���R�[�h��S�Ď擾���܂�
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
        Dim row As KubunDataSet.DataKbnTableRow

        commandTextSb.Append(" SELECT k.kbn, k.torikesi, ")
        commandTextSb.Append("        ISNULL(k.kbn_mei + ' [' + LEFT(CONVERT(VARCHAR,h.hosyousyo_no_nengetu,111),7) + ']',k.kbn_mei) kbn_mei ")
        commandTextSb.Append("  FROM m_data_kbn k WITH (READCOMMITTED) ")
        commandTextSb.Append("       LEFT OUTER JOIN m_hiduke_save h WITH (READCOMMITTED) ON h.kbn = k.kbn ")
        commandTextSb.Append("  WHERE k.torikesi = 0 ")
        commandTextSb.Append("  ORDER BY k.kbn ")

        ' �f�[�^�̎擾
        Dim kubunDataSet As New KubunDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            kubunDataSet, kubunDataSet.DataKbnTable.TableName)

        Dim kubunDataTable As KubunDataSet.DataKbnTableDataTable = _
                    kubunDataSet.DataKbnTable

        If kubunDataTable.Count <> 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In kubunDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.kbn + ":" + row.kbn_mei, row.kbn, dt))
                Else
                    dt.Rows.Add(CreateRow(row.kbn_mei, row.kbn, dt))
                End If
            Next

        End If

    End Sub

End Class
