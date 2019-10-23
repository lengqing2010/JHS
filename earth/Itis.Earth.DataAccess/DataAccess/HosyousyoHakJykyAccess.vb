Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' TBL_M�ۏ؏����s�󋵂ւ̐ڑ��N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class HosyousyoHakJykyAccess
    Inherits AbsDataAccess

    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' �R�l�N�V�����X�g�����O���擾
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̗L���ȋ敪���R�[�h��S�Ď擾���܂�
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, ByVal withSpaceRow As Boolean, Optional ByVal withCode As Boolean = True, Optional ByVal blnTorikesi As Boolean = True)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", dt, withSpaceRow, withCode)

        Dim commandTextSb As New StringBuilder()

        Dim ds As New DataSet
        Dim dTblRes As New DataTable
        Dim row As DataRow

        commandTextSb.Append("SELECT ")
        commandTextSb.Append("     hosyousyo_hak_jyky_no ")
        commandTextSb.Append("   , hosyousyo_hak_jyky ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("     m_hosyousyo_hak_jyky ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("     riyou = 0 ")
        commandTextSb.Append(" ORDER BY ")
        commandTextSb.Append("     hyouji_jyun ")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = {}

        ' �������s
        ds = ExecuteDataset(connStr, _
                             CommandType.Text, _
                             commandTextSb.ToString, _
                             commandParameters _
                             )

        '�f�[�^�e�[�u���֊i�[
        dTblRes = ds.Tables(0)

        If dTblRes.Rows.Count <> 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In dTblRes.Rows
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row("hosyousyo_hak_jyky_no").ToString + ":" + row("hosyousyo_hak_jyky").ToString, row("hosyousyo_hak_jyky_no").ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row("hosyousyo_hak_jyky").ToString, row("hosyousyo_hak_jyky_no").ToString, dt))
                End If
            Next

        End If

    End Sub

    ''' <summary>
    ''' �ۏ؏����s��M�̕ۏ؏����s��NO�ƑΉ�����ۏؗL���̃f�[�^��ԋp����
    ''' </summary>
    ''' <param name="dt">�f�[�^�i�[�p�f�[�^�e�[�u��</param>
    ''' <param name="dicRet">�f�[�^�i�[�p�f�B�N�V���i��</param>
    ''' <remarks></remarks>
    Public Sub GetHosyousyoHakJykyInfo(ByRef dt As DataTable, ByRef dicRet As Dictionary(Of String, String))
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyousyoHakJykyData", dt)

        Dim commandTextSb As New StringBuilder()

        Dim ds As New DataSet
        Dim dTblRes As New DataTable
        Dim row As DataRow

        '�N�G������
        commandTextSb.Append("SELECT ")
        commandTextSb.Append("     hosyousyo_hak_jyky_no ")
        commandTextSb.Append("   , hosyousyo_hak_jyky ")
        commandTextSb.Append("   , mihak_list_inji_umu ")
        commandTextSb.Append("   , kokyaku_list_inji_umu ")
        commandTextSb.Append("   , hyouji_jyun ")
        commandTextSb.Append("   , riyou ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("     m_hosyousyo_hak_jyky ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("     1=1 ")
        commandTextSb.Append(" ORDER BY ")
        commandTextSb.Append("     hyouji_jyun ")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = {}

        ' �������s
        ds = ExecuteDataset(connStr, _
                             CommandType.Text, _
                             commandTextSb.ToString, _
                             commandParameters _
                             )

        '�f�[�^�e�[�u���֊i�[
        dTblRes = ds.Tables(0)

        If dTblRes.Rows.Count = 0 Then
            dTblRes = Nothing
            dicRet = Nothing
        Else
            dicRet = New Dictionary(Of String, String)

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In dTblRes.Rows
                dicRet.Add(row("hosyousyo_hak_jyky_no").ToString, row("mihak_list_inji_umu").ToString)
            Next
        End If
        dt = dTblRes
    End Sub

    ''' <summary>
    ''' ���ނ������ɖ��̂��擾���܂�
    ''' </summary>
    ''' <param name="code">�R�[�h</param>
    ''' <param name="name">����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMeisyou( _
                                ByVal code As String _
                                , ByRef name As String _
                                ) As Boolean

        Dim ds As New DataSet
        Dim MeisyouDataTable As New DataTable
        Dim commandTextSb As New StringBuilder()

        Const DBparamHosyousyoHakJykyNo As String = "@HOSYOUSYO_HAK_JYKY_NO"

        '�N�G������
        commandTextSb.Append("SELECT ")
        commandTextSb.Append("     hosyousyo_hak_jyky_no ")
        commandTextSb.Append("   , hosyousyo_hak_jyky ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("     m_hosyousyo_hak_jyky ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("     1=1 ")
        commandTextSb.Append(" AND ")
        commandTextSb.Append("     hosyousyo_hak_jyky_no =  " & DBparamHosyousyoHakJykyNo)

        ' �p�����[�^�֐ݒ�
        Dim cmdParams() As SqlParameter = _
                {SQLHelper.MakeParam(DBparamHosyousyoHakJykyNo, SqlDbType.Int, 4, code)}

        ' �������s
        ds = ExecuteDataset(connStr, _
                             CommandType.Text, _
                             commandTextSb.ToString, _
                             cmdParams _
                             )

        '�ԋp�p�f�[�^�e�[�u���֊i�[
        MeisyouDataTable = ds.Tables(0)

        If MeisyouDataTable.Rows.Count = 0 Then
            Return False
        Else
            name = MeisyouDataTable.Rows(0)("hosyousyo_hak_jyky").ToString
        End If

        Return True
    End Function

End Class
