Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' TBL_M�ް��敪�ւ̐ڑ��N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class KubunDataAccess
    Inherits AbsDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �敪�������ɋ敪���R�[�h���擾���܂�
    ''' </summary>
    ''' <param name="kubun"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataBy(ByVal kubun As String, _
                              ByRef torikeshi As Integer, _
                              ByRef kubunMei As String) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDataBy", _
                                            kubun, _
                                            torikeshi, _
                                            kubunMei)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString

        ' �p�����[�^
        Const paramKubun As String = "@KUBUN"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT kbn,torikesi,kbn_mei")
        commandTextSb.Append("  FROM m_data_kbn WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE kbn = " & paramKubun)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, kubun)}

        ' �f�[�^�̎擾
        Dim KubunDataSet As New KubunDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            KubunDataSet, KubunDataSet.DataKbnTable.TableName, commandParameters)

        Dim kubunTable As KubunDataSet.DataKbnTableDataTable = KubunDataSet.DataKbnTable

        If kubunTable.Count = 0 Then
            Debug.WriteLine("�擾�o���܂���ł���")
            Return False
        Else
            Dim row As KubunDataSet.DataKbnTableRow = kubunTable(0)
            torikeshi = row.torikesi
            kubunMei = row.kbn_mei
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

        commandTextSb.Append("SELECT kbn,torikesi,kbn_mei")
        commandTextSb.Append("  FROM m_data_kbn WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE torikesi = 0 ORDER BY kbn")

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

    ''' <summary>
    ''' ����(�敪)�ɕR�t�����R�[�h���擾����
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <returns>����(�敪)�ɕR�t�����R�[�h���</returns>
    ''' <remarks></remarks>
    Public Function getKubunRecord(ByVal strKubun As String) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getKubunRecord" _
                                                    , strKubun _
                                                    )
        ' �p�����[�^
        Dim cmdParams() As SqlClient.SqlParameter

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KUBUN", SqlDbType.Char, 1, strKubun)}

        'SQL�N�G������
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append("SELECT ")
        cmdTextSb.Append("       kbn ")
        cmdTextSb.Append("       , torikesi ")
        cmdTextSb.Append("       , kbn_mei ")
        cmdTextSb.Append("       , genka_master_hisansyou_flg ")
        cmdTextSb.Append("       , add_login_user_id ")
        cmdTextSb.Append("       , add_datetime ")
        cmdTextSb.Append("       , upd_login_user_id ")
        cmdTextSb.Append("       , upd_datetime ")
        cmdTextSb.Append("       FROM m_data_kbn ")
        cmdTextSb.Append("       WHERE kbn = @KUBUN")

        ' �f�[�^�擾���ԋp
        Dim cmnDtAcc As New CmnDataAccess
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

End Class
