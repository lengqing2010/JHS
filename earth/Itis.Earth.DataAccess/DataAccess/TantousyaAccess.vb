Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' TBL_M�S���҂ւ̐ڑ��N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class TantousyaAccess
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
        Dim row As TantousyaDataSet.TantousyaTableRow

        commandTextSb.Append("SELECT tantousya_cd,tantousya_mei")
        commandTextSb.Append("  FROM m_tantousya")
        commandTextSb.Append("  WHERE hyouji_kbn = 0")
        commandTextSb.Append("  ORDER BY hyouji_kbn")

        ' �f�[�^�̎擾
        Dim tantousyaDataSet As New TantousyaDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            tantousyaDataSet, tantousyaDataSet.TantousyaTable.TableName)

        Dim tantousyaDataTable As TantousyaDataSet.TantousyaTableDataTable = _
                    tantousyaDataSet.TantousyaTable

        If tantousyaDataTable.Count <> 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In tantousyaDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.tantousya_cd.ToString + ":" + row.tantousya_mei, row.tantousya_cd.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.tantousya_mei, row.tantousya_cd.ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
