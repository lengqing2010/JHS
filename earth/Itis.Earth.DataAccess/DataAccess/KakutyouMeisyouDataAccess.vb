Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �g�����̃}�X�^���̎擾�p�N���X�ł�
''' </summary>
''' <remarks>�{�N���X�Ŗ��̏����擾����ꍇ�̓C���X�^���X���ɖ��̎�ʃv���p�e�B�ɏ���ݒ肵�Ă�������</remarks>
Public Class KakutyouMeisyouDataAccess
    Inherits AbsDataAccess

    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' �R�l�N�V�����X�g�����O���擾
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �R���X�g���N�^
    ''' <newpara>�����̎�ʂ��w�肷�邱��</newpara>
    ''' </summary>
    ''' <param name="intVal">EarthConst.emKtMeisyouType���g�p���邱��</param>
    ''' <remarks>��EarthConst�l���w�肷�邱��</remarks>
    Public Sub New(ByVal intVal As EarthConst.emKtMeisyouType)
        Me.pIntMeisyouSyubetu = intVal
    End Sub

    '''' <summary>
    '''' ���̎��
    '''' </summary>
    '''' <remarks></remarks>
    Private pIntMeisyouSyubetu As Integer = 0

#Region "SQL/�p�����[�^�ϐ�"
    Const pStrPrmCode As String = "@CODE"
    Const pStrPrmSyubetu As String = "@SYUBETU"
    Const pStrHannyouCd As String = "hannyou_cd"
    Const pStrHannyouNo As String = "hannyou_no"
#End Region

    ''' <summary>
    ''' ���ނ������ɖ��̂��擾���܂�
    ''' </summary>
    ''' <param name="code">�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMeisyou( _
                                ByVal code As String _
                                , ByRef name As String _
                                ) As Boolean

        Dim ds As New DataSet
        Dim MeisyouDataTable As New DataTable
        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append(" 	meisyou_syubetu")
        commandTextSb.Append(" 	,code")
        commandTextSb.Append(" 	,meisyou")
        commandTextSb.Append(" 	,hyouji_jyun ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append(" 	m_kakutyou_meisyou WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append(" 	meisyou_syubetu = " & pStrPrmSyubetu)
        commandTextSb.Append(" AND")
        commandTextSb.Append(" 	code = " & pStrPrmCode)
        commandTextSb.Append(" ORDER BY ")
        commandTextSb.Append(" 	hyouji_jyun ")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(pStrPrmSyubetu, SqlDbType.Int, 4, pIntMeisyouSyubetu), _
             SQLHelper.MakeParam(pStrPrmCode, SqlDbType.VarChar, 10, code)}

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
            name = MeisyouDataTable.Rows(0)("meisyou").ToString
        End If

        Return True

    End Function

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̗L���Ȗ��̃e�[�u�����R�[�h��S�Ď擾���܂�(���g������M��p)<br/>
    ''' </summary>
    ''' <param name="dt" >�f�[�^�e�[�u��</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetKtMeisyouDropdownData(ByRef dt As DataTable, _
                                         ByVal withSpaceRow As Boolean, _
                                         Optional ByVal withCode As Boolean = True, _
                                         Optional ByVal blnTorikesi As Boolean = True)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKtMeisyouDropdownData", dt, withSpaceRow, withCode)


        Dim ds As New DataSet
        Dim MeisyouDataTable As New DataTable
        Dim commandTextSb As New StringBuilder()
        Dim row As DataRow

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append(" 	code ")
        commandTextSb.Append(" 	,meisyou ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append(" 	m_kakutyou_meisyou WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append(" 	meisyou_syubetu = " & pStrPrmSyubetu)
        commandTextSb.Append(" ORDER BY ")
        commandTextSb.Append(" 	hyouji_jyun ")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(pStrPrmSyubetu, SqlDbType.Int, 4, pIntMeisyouSyubetu)}

        ' �������s
        ds = ExecuteDataset(connStr, _
                             CommandType.Text, _
                             commandTextSb.ToString, _
                             commandParameters _
                             )

        '�f�[�^�e�[�u���֊i�[
        MeisyouDataTable = ds.Tables(0)

        If MeisyouDataTable.Rows.Count > 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In MeisyouDataTable.Rows
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row("code").ToString + ":" + row("meisyou").ToString, row("code").ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row("meisyou").ToString, row("code").ToString, dt))
                End If
            Next

        End If

    End Sub

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̗L���Ȗ��̃e�[�u�����R�[�h��S�Ď擾���܂�(���g������M��p�A�\�����ڂ��p�����[�^�Ŏw��)
    ''' </summary>
    ''' <param name="dt" >�f�[�^�e�[�u��</param>
    ''' <param name="type">�g������M�h���b�v�_�E���^�C�v</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <param name="blnTorikesi">�i�C�Ӂj�g������M�̍��ڂɎ���͑��݂��Ȃ�</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetKtMeisyouHannyouDropdownData(ByRef dt As DataTable, _
                                                         ByVal type As EarthEnum.emKtMeisyouType, _
                                                         ByVal withSpaceRow As Boolean, _
                                                         Optional ByVal withCode As Boolean = True, _
                                                         Optional ByVal blnTorikesi As Boolean = True)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKtMeisyouHannyouDropdownData", _
                                                    dt, _
                                                    type, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    blnTorikesi)

        Dim ds As New DataSet
        Dim MeisyouDataTable As New DataTable
        Dim commandTextSb As New StringBuilder()
        Dim row As DataRow
        Dim strItem As String = String.Empty
        Dim strNullVal As String = String.Empty

        '�p�����[�^�ɂ���ĕ\�����ڐ؂�ւ�
        If type = EarthEnum.emKtMeisyouType.HannyouCd Then
            strItem = pStrHannyouCd
            strNullVal = "''"
        ElseIf type = EarthEnum.emKtMeisyouType.HannyouNo Then
            strItem = pStrHannyouNo
            strNullVal = "0"
        Else
            dt = Nothing
            Exit Sub
        End If

        '****************
        '* SELECT����
        '****************
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" 	code ")
        commandTextSb.AppendLine(" 	,ISNULL(" & strItem & ", " & strNullVal & ")AS " & strItem)
        
        '****************
        '* TABLE����
        '****************
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine(" 	m_kakutyou_meisyou WITH (READCOMMITTED) ")

        '****************
        '* WHERE����
        '****************
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" 	meisyou_syubetu = " & pStrPrmSyubetu)

        '****************
        '* ORDER BY����
        '****************
        commandTextSb.AppendLine(" ORDER BY ")
        commandTextSb.AppendLine(" 	hyouji_jyun ")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(pStrPrmSyubetu, SqlDbType.Int, 4, pIntMeisyouSyubetu)}

        ' �������s
        ds = ExecuteDataset(connStr, _
                             CommandType.Text, _
                             commandTextSb.ToString, _
                             commandParameters _
                             )

        '�f�[�^�e�[�u���֊i�[
        MeisyouDataTable = ds.Tables(0)

        If MeisyouDataTable.Rows.Count > 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In MeisyouDataTable.Rows
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row("code").ToString + ":" + row(strItem).ToString, row("code").ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row(strItem).ToString, row("code").ToString, dt))
                End If
            Next

        End If

    End Sub

End Class
