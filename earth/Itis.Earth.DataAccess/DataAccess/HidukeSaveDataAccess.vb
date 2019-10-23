Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' ���t�}�X�^�Ɋ֌W����f�[�^�A�N�Z�X�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class HidukeSaveDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �R�l�N�V�����X�g�����O
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �敪�������ɕ񍐏����������擾���܂�
    ''' </summary>
    ''' <param name="kubun">�敪</param>
    ''' <returns>�񍐏�������</returns>
    ''' <remarks></remarks>
    Public Function GetHoukokusyoHassouDate(ByVal kubun As String) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHoukokusyoHassouDate", _
                                    kubun)

        ' �߂�l
        Dim strHoukokusyoHassouDate As String = ""

        ' �p�����[�^
        Const strParamKubun As String = "@KUBUN"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT kbn,hosyousyo_hak_date,hosyousyo_no_nengetu,hkks_hassou_date ")
        commandTextSb.Append("  FROM m_hiduke_save WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE kbn = " & strParamKubun)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKubun, SqlDbType.Char, 1, kubun)}

        ' �f�[�^�擾���ԋp
        Dim cmnDtAcc As New CmnDataAccess
        Dim dTbl As New DataTable
        dTbl = cmnDtAcc.getDataTable(commandTextSb.ToString, commandParameters)
        If dTbl.Rows.Count > 0 Then
            If Not dTbl.Rows(0)("hkks_hassou_date") Is Nothing AndAlso Not dTbl.Rows(0)("hkks_hassou_date") Is DBNull.Value Then
                strHoukokusyoHassouDate = Format(dTbl.Rows(0)("hkks_hassou_date"), "yyyy/MM/dd")
            End If
        End If

        Return strHoukokusyoHassouDate

    End Function

    ''' <summary>
    ''' �敪�������ɕۏ؏����s�����擾���܂�
    ''' </summary>
    ''' <param name="kubun">�敪</param>
    ''' <returns>�ۏ؏����s��</returns>
    ''' <remarks></remarks>
    Public Function GetHosyousyoHakkouDate(ByVal kubun As String) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyousyoHakkouDate", _
                                    kubun)

        ' �߂�l
        Dim strHosyousyoHakkouDate As String = ""

        ' �p�����[�^
        Const strParamKubun As String = "@KUBUN"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT kbn,hosyousyo_hak_date,hosyousyo_no_nengetu,hkks_hassou_date ")
        commandTextSb.Append("  FROM m_hiduke_save WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE kbn = " & strParamKubun)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKubun, SqlDbType.Char, 1, kubun)}

        ' �f�[�^�擾���ԋp
        Dim cmnDtAcc As New CmnDataAccess
        Dim dTbl As New DataTable
        dTbl = cmnDtAcc.getDataTable(commandTextSb.ToString, commandParameters)
        If dTbl.Rows.Count > 0 Then
            If Not dTbl.Rows(0)("hosyousyo_hak_date") Is Nothing AndAlso Not dTbl.Rows(0)("hosyousyo_hak_date") Is DBNull.Value Then
                strHosyousyoHakkouDate = Format(dTbl.Rows(0)("hosyousyo_hak_date"), "yyyy/MM/dd")
            End If
        End If

        Return strHosyousyoHakkouDate

    End Function

    ''' <summary>
    ''' ���t�}�X�^���R�[�h���擾���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <returns>���t�}�X�^�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetHidukeSaveData(ByVal kbn As String) As HidukeSaveDataSet.HidukeSaveTableDataTable

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CreateRow", _
                                            kbn)

        ' �p�����[�^
        Const strParamKbn As String = "@KBN"

        Dim commandText As String = " SELECT  " & _
                                    "     kbn, " & _
                                    "     hosyousyo_hak_date, " & _
                                    "     hosyousyo_no_nengetu, " & _
                                    "     hkks_hassou_date, " & _
                                    "     upd_login_user_id, " & _
                                    "     upd_datetime " & _
                                    " FROM  " & _
                                    "     m_hiduke_save (READCOMMITTED) " & _
                                    " WHERE " & _
                                    "     kbn = " & strParamKbn

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn)}

        ' �f�[�^�̎擾
        Dim hidukeDataSet As New HidukeSaveDataSet

        SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
            hidukeDataSet, hidukeDataSet.HidukeSaveTable.TableName, commandParameters)

        Dim hidukeTable As HidukeSaveDataSet.HidukeSaveTableDataTable = hidukeDataSet.HidukeSaveTable

        Return hidukeTable

    End Function
End Class
