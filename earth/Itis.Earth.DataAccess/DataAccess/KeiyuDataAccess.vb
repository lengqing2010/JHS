Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �o�R���̎擾�p�N���X�ł�
''' </summary>
''' <remarks>�o�R�͖��̃e�[�u���̖��̎��"02"</remarks>
Public Class KeiyuDataAccess
    Inherits AbsDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' �R�l�N�V�����X�g�����O���擾
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �o�R���ނ������ɖ��̂��擾���܂�
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataBy(ByVal code As Integer, _
                              ByRef name As String) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDataBy", _
                                                    code, _
                                                    name)

        ' �p�����[�^
        Const paramCode As String = "@CODE"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT code,meisyou ")
        commandTextSb.Append("  FROM m_meisyou WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE meisyou_syubetu = '06' ")
        commandTextSb.Append("  AND   code     = " + paramCode)
        commandTextSb.Append("  ORDER BY hyouji_jyun ")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramCode, SqlDbType.Int, 1, code)}

        ' �f�[�^�̎擾
        Dim KeiyuDataSet As New KeiyuDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            KeiyuDataSet, KeiyuDataSet.KeiyuTable.TableName, commandParameters)

        Dim KeiyuTable As KeiyuDataSet.KeiyuTableDataTable = KeiyuDataSet.KeiyuTable

        If KeiyuTable.Count = 0 Then
            Debug.WriteLine("�擾�o���܂���ł���")
            Return False
        Else
            Dim row As KeiyuDataSet.KeiyuTableRow = KeiyuTable(0)
            name = row.meisyou
        End If

        Return True

    End Function

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̗L���Ȍo�R���R�[�h��S�Ď擾���܂�
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
        Dim row As KeiyuDataSet.KeiyuTableRow

        commandTextSb.Append("SELECT code,meisyou ")
        commandTextSb.Append("  FROM m_meisyou WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE meisyou_syubetu = '06' ")
        commandTextSb.Append("  ORDER BY hyouji_jyun ")

        ' �f�[�^�̎擾
        Dim KeiyuDataSet As New KeiyuDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            KeiyuDataSet, KeiyuDataSet.KeiyuTable.TableName)

        Dim KeiyuDataTable As KeiyuDataSet.KeiyuTableDataTable = _
                    KeiyuDataSet.KeiyuTable

        If KeiyuDataTable.Count <> 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", " ", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In KeiyuDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.code.ToString + ":" + row.meisyou, row.code, dt))
                Else
                    dt.Rows.Add(CreateRow(row.meisyou, row.code, dt))
                End If
            Next

        End If


    End Sub

End Class
