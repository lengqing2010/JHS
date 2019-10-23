Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �d���f�[�^�̎擾�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class TyoufukuDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �敪�A���������������ɏd�����R�[�h���擾���܂�
    ''' </summary>
    ''' <param name="kubun">�敪</param>
    ''' <param name="hosyousyoNo">�\�����̕ۏ؏�NO</param>
    ''' <param name="searchItem1">�e�[�u�����ږ��P</param>
    ''' <param name="condition1">�`�F�b�N�Ώۃf�[�^�P</param>
    ''' <param name="searchItem2">�e�[�u�����ږ��Q</param>
    ''' <param name="condition2">�`�F�b�N�Ώۃf�[�^�Q</param>
    ''' <returns></returns>
    ''' <remarks>�P�̂̏d���`�F�b�N���ɂ͍��ږ��A�f�[�^���P�w��<br/>
    '''          �ꗗ�f�[�^�擾���͎{�喼�A�Z�����Ɏw�肷��</remarks>
    Public Function GetDataBy(ByVal kubun As String, _
                              ByVal hosyousyoNo As String, _
                              ByVal searchItem1 As String, _
                              ByVal condition1 As String, _
                              ByVal searchItem2 As String, _
                              ByVal condition2 As String) As TyoufukuDataSet.TyoufukuTableDataTable

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDataBy", _
                                                    kubun, _
                                                    hosyousyoNo, _
                                                    searchItem1, _
                                                    condition1, _
                                                    searchItem2, _
                                                    condition2)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString

        ' ���t�͈�From�i�������U�����O�̂P���j���擾
        Dim dateFrom As New DateTime(Date.Today.AddMonths(-5).Year, _
                                     Date.Today.AddMonths(-5).Month, _
                                     1, 0, 0, 0)
        ' ���t�͈�To�i�������j���擾
        Dim dateTo As New DateTime(Date.Today.Year, _
                                   Date.Today.Month, _
                                   Date.DaysInMonth(Date.Today.Year, Date.Today.Month), _
                                   23, 59, 59)

        ' �ۏ؏�NO�̌��������𐶐�
        Dim strFrom As String = Format(dateFrom, "yyyyMM") & "0000"
        Dim strTo As String = Format(dateTo, "yyyyMM") & "9999"

        ' �{�喼�A�Z�����Ɍ�������ۂ̒ǉ�����
        Dim strAll As String = ""
        Dim strCondition2 As String = ""

        If searchItem2.Trim() <> "" Then
            strAll = " OR " & searchItem2 & " = "
            strCondition2 = "@CONDITION2"
        End If

        ' �p�����[�^
        Dim arParams As String() = {"@KUBUN", _
                                    "@MYHOSYOUSYONO", _
                                    "@DATEFROM", _
                                    "@DATETO", _
                                    "@CONDITION1", _
                                    searchItem1, _
                                    strAll, _
                                    strCondition2}


        Dim commandText As String = "SELECT " & _
                                    "    h.haki_syubetu, " & _
                                    "    z.kbn, " & _
                                    "    z.hosyousyo_no, " & _
                                    "    z.sesyu_mei, " & _
                                    "    z.bukken_jyuusyo1, " & _
                                    "    z.bukken_jyuusyo2, " & _
                                    "    km.kameiten_mei1, " & _
                                    "    z.bikou " & _
                                    "FROM " & _
                                    "    t_jiban z WITH (READCOMMITTED) " & _
                                    "    LEFT JOIN m_data_haki h WITH (READCOMMITTED) ON (z.data_haki_syubetu = h.data_haki_no) " & _
                                    "    LEFT JOIN m_kameiten km WITH (READCOMMITTED) ON (z.kameiten_cd   = km.kameiten_cd) " & _
                                    "WHERE " & _
                                    "    z.kbn = {0}  " & _
                                    "AND z.hosyousyo_no <> {1}  " & _
                                    "AND z.hosyousyo_no BETWEEN {2} AND {3} " & _
                                    "AND ({5} = {4} {6}{7} ) " & _
                                    "ORDER BY z.hosyousyo_no "

        ' �p�����[�^�փf�[�^��ݒ�
        Dim commandParameters() As SqlParameter

        If searchItem2.Trim() <> "" Then
            commandParameters = New SqlParameter() _
            {SQLHelper.MakeParam(arParams(0), SqlDbType.Char, 1, kubun), _
             SQLHelper.MakeParam(arParams(1), SqlDbType.VarChar, 10, hosyousyoNo), _
             SQLHelper.MakeParam(arParams(2), SqlDbType.VarChar, 10, strFrom), _
             SQLHelper.MakeParam(arParams(3), SqlDbType.VarChar, 10, strTo), _
             SQLHelper.MakeParam(arParams(4), SqlDbType.VarChar, 30, condition1), _
             SQLHelper.MakeParam(arParams(7), SqlDbType.VarChar, 30, condition2)}
        Else
            commandParameters = New SqlParameter() _
            {SQLHelper.MakeParam(arParams(0), SqlDbType.Char, 1, kubun), _
             SQLHelper.MakeParam(arParams(1), SqlDbType.VarChar, 10, hosyousyoNo), _
             SQLHelper.MakeParam(arParams(2), SqlDbType.VarChar, 10, strFrom), _
             SQLHelper.MakeParam(arParams(3), SqlDbType.VarChar, 10, strTo), _
             SQLHelper.MakeParam(arParams(4), SqlDbType.VarChar, 30, condition1)}
        End If

        ' �p�����[�^��SQL�ɔ��f
        commandText = String.Format(commandText, arParams)

        ' �f�[�^�̎擾
        Dim tyoufukuDataSet As New TyoufukuDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
            tyoufukuDataSet, tyoufukuDataSet.TyoufukuTable.TableName, commandParameters)

        Dim tyoufukuTable As TyoufukuDataSet.TyoufukuTableDataTable = tyoufukuDataSet.TyoufukuTable

        Return tyoufukuTable

    End Function

End Class
