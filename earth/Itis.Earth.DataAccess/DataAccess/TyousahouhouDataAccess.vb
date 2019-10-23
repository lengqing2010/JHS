Imports System.text
Imports System.Data.SqlClient


''' <summary>
''' �������@���̎擾�p�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class TyousahouhouDataAccess
    Inherits AbsDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' �R�l�N�V�����X�g�����O���擾
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �������@���ނ������ɖ��̂��擾���܂�
    ''' </summary>
    ''' <param name="no"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataBy(ByVal no As Integer, _
                              ByRef name As String) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDataBy", _
                                                    no, _
                                                    name)

        ' �p�����[�^
        Const paramNo As String = "@CHOUSANO"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT tys_houhou_no,tys_houhou_mei,tys_houhou_mei_ryaku ")
        commandTextSb.Append("  FROM m_tyousahouhou WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE tys_houhou_no = " + paramNo)
        commandTextSb.Append("  ORDER BY tys_houhou_no ")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramNo, SqlDbType.Int, 0, no)}

        ' �f�[�^�̎擾
        Dim TyousahouhouDataSet As New TyousahouhouDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            TyousahouhouDataSet, TyousahouhouDataSet.TyousahouhouTable.TableName, commandParameters)

        Dim TyousahouhouTable As TyousahouhouDataSet.TyousahouhouTableDataTable = TyousahouhouDataSet.TyousahouhouTable

        If TyousahouhouTable.Count = 0 Then
            Debug.WriteLine("�擾�o���܂���ł���")
            Return False
        Else
            Dim row As TyousahouhouDataSet.TyousahouhouTableRow = TyousahouhouTable(0)
            name = row.tys_houhou_mei
        End If

        Return True

    End Function

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̒������@���R�[�h��S�Ď擾���܂�
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
        Dim row As TyousahouhouDataSet.TyousahouhouTableRow

        commandTextSb.Append("SELECT tys_houhou_no,tys_houhou_mei,tys_houhou_mei_ryaku ")
        commandTextSb.Append("  FROM m_tyousahouhou WITH (READCOMMITTED) ")

        '����̏ꍇ��DDL�Ɋ܂߂Ȃ�
        commandTextSb.Append("  WHERE")
        commandTextSb.Append("  torikesi = 0")

        commandTextSb.Append("  ORDER BY tys_houhou_no ")

        ' �f�[�^�̎擾
        Dim TyousahouhouDataSet As New TyousahouhouDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            TyousahouhouDataSet, TyousahouhouDataSet.TyousahouhouTable.TableName)

        Dim TyousahouhouDataTable As TyousahouhouDataSet.TyousahouhouTableDataTable = _
                    TyousahouhouDataSet.TyousahouhouTable

        If TyousahouhouDataTable.Count <> 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In TyousahouhouDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.tys_houhou_no.ToString + ":" + row.tys_houhou_mei, row.tys_houhou_no.ToString, dt))
                Else
                    dt.Rows.Add(CreateRow(row.tys_houhou_mei, row.tys_houhou_no.ToString, dt))
                End If
            Next

        End If

    End Sub

    ''' <summary>
    ''' ����(�������@NO)�ɕR�t�����R�[�h���擾����
    ''' </summary>
    ''' <param name="intTysHouhouNo">�������@NO</param>
    ''' <returns>����(�������@NO)�ɕR�t�����R�[�h���</returns>
    ''' <remarks></remarks>
    Public Function getTyousahouhouRecord(ByVal intTysHouhouNo As Integer) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getTyousahouhouRecord" _
                                                    , intTysHouhouNo _
                                                    )

        ' �p�����[�^
        Dim cmdParams() As SqlClient.SqlParameter

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@TYOUSAHOUHOUNO", SqlDbType.Int, 4, intTysHouhouNo)}

        'SQL�N�G������
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append("SELECT ")
        cmdTextSb.Append("       tys_houhou_no ")
        cmdTextSb.Append("       , torikesi ")
        cmdTextSb.Append("       , tys_houhou_mei_ryaku ")
        cmdTextSb.Append("       , tys_houhou_mei ")
        cmdTextSb.Append("       , genka_settei_fuyou_flg ")
        cmdTextSb.Append("       , kakaku_settei_fuyou_flg ")
        cmdTextSb.Append("       , add_login_user_id ")
        cmdTextSb.Append("       , add_datetime ")
        cmdTextSb.Append("       , upd_login_user_id ")
        cmdTextSb.Append("       , upd_datetime")
        cmdTextSb.Append("       FROM m_tyousahouhou ")
        cmdTextSb.Append("       WHERE tys_houhou_no = @TYOUSAHOUHOUNO")

        ' �f�[�^�擾���ԋp
        Dim cmnDtAcc As New CmnDataAccess
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

End Class
