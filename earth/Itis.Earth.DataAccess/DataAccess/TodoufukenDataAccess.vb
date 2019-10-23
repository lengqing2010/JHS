Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �s���{�����̎擾�p�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class TodoufukenDataAccess
    Inherits AbsDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' �R�l�N�V�����X�g�����O���擾
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �s���{�����ނ������ɓs���{�������擾���܂�
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

        Dim ds As New DataSet
        Dim MeisyouDataTable As New DataTable
        Dim commandTextSb As New StringBuilder()

        ' �p�����[�^
        Const paramCode As String = "@CODE"

        commandTextSb.Append("SELECT todouhuken_cd,todouhuken_mei ")
        commandTextSb.Append("  FROM m_todoufuken WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE todouhuken_cd = " + paramCode)
        commandTextSb.Append("  ORDER BY todouhuken_cd ")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramCode, SqlDbType.Char, 1, code)}

        ' �������s
        ds = ExecuteDataset(connStr, _
                             CommandType.Text, _
                             commandTextSb.ToString, _
                             commandParameters _
                             )

        '�ԋp�p�f�[�^�e�[�u���֊i�[
        MeisyouDataTable = ds.Tables(0)

        If MeisyouDataTable.Rows.Count = 0 Then
            Return False
        Else
            name = MeisyouDataTable.Rows(0)("todouhuken_mei").ToString
        End If

        Return True

    End Function

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̗L���ȋ敪���R�[�h��S�Ď擾���܂�
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, ByVal withSpaceRow As Boolean, Optional ByVal withCode As Boolean = True, Optional ByVal blnTorikesi As Boolean = True)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", dt, withSpaceRow, withCode)

        Dim ds As New DataSet
        Dim resDataTable As New DataTable
        Dim commandTextSb As New StringBuilder()
        Dim row As DataRow

        commandTextSb.Append("SELECT todouhuken_cd,todouhuken_mei ")
        commandTextSb.Append("  FROM m_todoufuken WITH (READCOMMITTED) ")
        commandTextSb.Append("  ORDER BY todouhuken_cd ")

        ' �������s
        ds = ExecuteDataset(connStr, _
                             CommandType.Text, _
                             commandTextSb.ToString _
                             )

        '�f�[�^�e�[�u���֊i�[
        resDataTable = ds.Tables(0)

        If resDataTable.Rows.Count > 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In resDataTable.Rows
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row("todouhuken_cd").ToString + ":" + row("todouhuken_mei").ToString, row("todouhuken_cd").ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row("todouhuken_mei").ToString, row("todouhuken_cd").ToString, dt))
                End If
            Next

        End If

    End Sub

End Class

