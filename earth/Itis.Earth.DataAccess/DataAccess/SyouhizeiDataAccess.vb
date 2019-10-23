Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' TBL_����łւ̐ڑ��N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class SyouhizeiAccess
    Inherits AbsDataAccess

    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' ���ނ������ɖ��̂��擾���܂�
    ''' </summary>
    ''' <param name="code">�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMeisyou( _
                                ByVal code As String _
                                , ByRef name As String _
                                , Optional ByVal blnPercent As Boolean = False _
                                ) As Boolean

        Dim ds As New DataSet
        Dim MeisyouDataTable As New DataTable
        Dim commandTextSb As New StringBuilder()

        ' �p�����[�^
        Const prmZeiKbn As String = "@ZEI_KBN"

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("  zei_kbn ")
        If blnPercent Then
            commandTextSb.Append("  ,CONVERT(VARCHAR, CONVERT(INTEGER, zeiritu * 100)) + '%' AS zeiritu ") '���l�������� + '%'
        Else
            commandTextSb.Append("  ,zeiritu ")
        End If
        commandTextSb.Append("  FROM m_syouhizei ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append(" 	zei_kbn = " & prmZeiKbn)
        commandTextSb.Append("  ORDER BY zei_kbn ")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(prmZeiKbn, SqlDbType.Char, 1, code)}

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
            name = MeisyouDataTable.Rows(0)("zeiritu").ToString
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

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("  zei_kbn ")
        commandTextSb.Append("  ,CONVERT(VARCHAR, CONVERT(INTEGER, zeiritu * 100)) + '%' AS zeiritu ") '���l�������� + '%'
        commandTextSb.Append("  FROM m_syouhizei ")
        commandTextSb.Append("  ORDER BY zei_kbn ")

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
                    dt.Rows.Add(CreateRow(row("zei_kbn").ToString + ":�y" + row("zeiritu").ToString & "�z", row("zei_kbn").ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row("zeiritu").ToString, row("zei_kbn").ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
