Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �r���_�[���̎擾�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class BuilderDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �r���_�[�����擾���܂�
    ''' </summary>
    ''' <param name="kameitenCd">�����X�R�[�h</param>
    ''' <returns>�r���_�[���e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetBuilderData(ByVal kameitenCd As String) As BuilderDataSet.BuilderTableDataTable

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetBuilderData", _
                                            kameitenCd)

        ' �p�����[�^
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim commandText As String = " SELECT " & _
                                    "     k.kameiten_cd, " & _
                                    "     k.nyuuryoku_no, " & _
                                    "     m.meisyou As tyuuijikou_syubetu, " & _
                                    "     k.nyuuryoku_date, " & _
                                    "     k.uketukesya_mei, " & _
                                    "     k.naiyou " & _
                                    " FROM " & _
                                    "     m_kameiten_tyuuijikou k WITH (READCOMMITTED)" & _
                                    " LEFT OUTER JOIN  " & _
                                    "     m_meisyou m WITH (READCOMMITTED) ON m.code = k.tyuuijikou_syubetu   " & _
                                    "                 AND m.meisyou_syubetu = '10' " & _
                                    " WHERE " & _
                                    "     k.kameiten_cd = " & strParamKameitenCd & _
                                    " ORDER BY k.tyuuijikou_syubetu, k.nyuuryoku_date desc, k.nyuuryoku_no desc "

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, kameitenCd)}

        ' �f�[�^�̎擾
        Dim BuilderDataSet As New BuilderDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
            BuilderDataSet, BuilderDataSet.BuilderTable.TableName, commandParameters)

        Dim BuilderTable As BuilderDataSet.BuilderTableDataTable = BuilderDataSet.BuilderTable

        Return BuilderTable

    End Function

    ''' <summary>
    ''' �����X�R�[�h�̒��ӎ�����13�����݂��Ȃ��ꍇ�A�G���[��Ԃ��B
    ''' </summary>
    ''' <param name="kameitenCd">�����X�R�[�h</param>
    ''' <returns>True or False</returns>
    ''' <remarks>�������X�R�[�h=�����͎��A�X���[</remarks>
    Public Function ChkBuilderData13(ByVal kameitenCd As String) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkBuilderData", _
                                            kameitenCd)

        '�����X�R�[�h=�����͎��A�X���[
        If kameitenCd.Trim = String.Empty Then
            Return True
        End If

        ' �p�����[�^
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("     COUNT(k.kameiten_cd) ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("     m_kameiten_tyuuijikou k WITH (READCOMMITTED)")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("     k.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND ")
        commandTextSb.Append("     k.tyuuijikou_syubetu = '13'")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, kameitenCd)}

        ' �f�[�^�̎擾
        Dim data As Object = Nothing

        ' �������s
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' �擾�o���Ȃ��ꍇ�A�󔒂�ԋp
        If data Is Nothing OrElse IsDBNull(data) Then
            Return False
        End If
        '�Y���f�[�^�Ȃ�
        If data = 0 Then
            Return False
        End If
        '�Y���f�[�^����
        If data > 0 Then
            Return True
        End If
        Return False

    End Function

    ''' <summary>
    ''' �����X�R�[�h�̒��ӎ����� 55 �����݂���ꍇ�ATRUE ��Ԃ��B
    ''' </summary>
    ''' <param name="kameitenCd">�����X�R�[�h</param>
    ''' <returns>True or False</returns>
    ''' <remarks>�������X�R�[�h=�����͎��AFalse </remarks>
    Public Function ChkBuilderData55(ByVal kameitenCd As String) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkBuilderData", _
                                            kameitenCd)

        '�����X�R�[�h=�����͎��A�X���[
        If kameitenCd.Trim = String.Empty Then
            Return False
        End If

        ' �p�����[�^
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("     COUNT(k.kameiten_cd) ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("     m_kameiten_tyuuijikou k WITH (READCOMMITTED)")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("     k.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND ")
        commandTextSb.Append("     k.tyuuijikou_syubetu = 55")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, kameitenCd)}

        ' �f�[�^�̎擾
        Dim data As Object = Nothing

        ' �������s
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' �擾�o���Ȃ��ꍇ�A�󔒂�ԋp
        If data Is Nothing OrElse IsDBNull(data) Then
            Return False
        End If
        '�Y���f�[�^�Ȃ�
        If data = 0 Then
            Return False
        End If
        '�Y���f�[�^����
        If data > 0 Then
            Return True
        End If
        Return False

    End Function
End Class
