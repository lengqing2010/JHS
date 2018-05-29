Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports Itis.Earth.DataAccess
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' ���̃e�[�u�����̎擾�p�N���X�ł�
''' </summary>
''' <remarks>�{�N���X�Ŗ��̏����擾����ꍇ�͎��O�ɖ��̎�ʃv���p�e�B�ɏ���ݒ肵�Ă�������</remarks>
Public Class MeisyouDataAccess
    Inherits AbsDataAccess

    Public Enum MeisyouType
        ''' <summary>
        ''' 01�F���i�敪�i�n�Ճf�[�^�E���i���i�ݒ�}�X�^�j
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN_KBN = 1
        ''' <summary>
        ''' 02�F�����T�v�i�n�Ճf�[�^�E���i���i�ݒ�}�X�^�j
        ''' </summary>
        ''' <remarks></remarks>
        TYOUSA_GAIYOU = 2
        ''' <summary>
        ''' 03�F���i�ݒ�ꏊ�i�n�Ճf�[�^�E���i���i�ݒ�}�X�^�j
        ''' </summary>
        ''' <remarks></remarks>
        KAKAKU_SETTEI = 3
        ''' <summary>
        ''' 04�F�ی��\���敪�i�n�Ճf�[�^�E���i���i�ݒ�}�X�^�j
        ''' </summary>
        ''' <remarks></remarks>
        HOKEN_SINSEI = 4
        ''' <summary>
        ''' 05�F�����m�F�������i�����X�}�X�^�E�n�Ճf�[�^�j
        ''' </summary>
        ''' <remarks></remarks>
        NYUUKIN_KAKUNIN = 5
        ''' <summary>
        ''' 06�F�o�R���i�n�Ճf�[�^�j
        ''' </summary>
        ''' <remarks></remarks>
        KEIYU_MEI = 6
        ''' <summary>
        ''' 07�F������FLG�i�����X�}�X�^�j
        ''' </summary>
        ''' <remarks></remarks>
        HATTYUUSYO_FLG = 7
        ''' <summary>
        ''' 08�F�������Ϗ�FLG�i�����X�}�X�^�j
        ''' </summary>
        ''' <remarks></remarks>
        TYS_MITUMORI_FLG = 8
        ''' <summary>
        ''' 09�F�����X���l�i�����X���l�ݒ�}�X�^�j
        ''' </summary>
        ''' <remarks></remarks>
        KAMEITEN_BIKO = 9
        ''' <summary>
        ''' 10�F�����X���ӎ����i�����X���ӎ����ݒ�}�X�^�j
        ''' </summary>
        ''' <remarks></remarks>
        KAMEITEN_TYUUIJIKOU = 10
        ''' <summary>
        ''' 11�F��`�䗦��
        ''' </summary>
        ''' <remarks></remarks>
        TEGATA_HIRITU = 11
        ''' <summary>
        ''' 12�F���͉��䗦��
        ''' </summary>
        ''' <remarks></remarks>
        KYOURYOKU_KAIHI = 12

        ''' <summary>
        ''' 110�F�w�萿�����L��
        ''' shitei_seikyuusyo_umu
        ''' </summary>
        ''' <remarks></remarks>
        SHITEI_SEIKYUUSYO_UMU = 110

    End Enum

    ''' <summary>
    ''' ���̎�ʁi���w�莞�͑S���擾�j
    ''' </summary>
    ''' <remarks></remarks>
    Private meisyou_syubetu As String = "%"

    ' �R�l�N�V�����X�g�����O���擾
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' ���ނ������ɖ��̂��擾���܂�
    ''' </summary>
    ''' <param name="type">�擾���閼�̎��</param>
    ''' <param name="code">key�ƂȂ�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMeisyou(ByVal type As MeisyouType, _
                                ByVal code As Integer, _
                              ByRef name As String) As Boolean

        ' �p�����[�^
        Const paramCode As String = "@CODE"
        Const paramSyubetu As String = "@SYUBETU"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT meisyou_syubetu,code,meisyou,hyouji_jyun ")
        commandTextSb.Append("  FROM m_meisyou ")
        commandTextSb.Append("  WHERE meisyou_syubetu like " & paramSyubetu)
        commandTextSb.Append("  AND   code     = " & paramCode)
        commandTextSb.Append("  ORDER BY hyouji_jyun ")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramSyubetu, SqlDbType.Char, 2, type.ToString("00")), _
             SQLHelper.MakeParam(paramCode, SqlDbType.Int, 1, code)}

        ' �f�[�^�̎擾
        Dim MeisyouDataSet As New MeisyouDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            MeisyouDataSet, MeisyouDataSet.MeisyouTable.TableName, commandParameters)

        Dim MeisyouTable As MeisyouDataSet.MeisyouTableDataTable = MeisyouDataSet.MeisyouTable

        If MeisyouTable.Count = 0 Then
            Debug.WriteLine("�擾�o���܂���ł���")
            Return False
        Else
            Dim row As MeisyouDataSet.MeisyouTableRow = MeisyouTable(0)
            name = row.meisyou
        End If

        Return True

    End Function

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̗L���Ȗ��̃e�[�u�����R�[�h��S�Ď擾���܂�
    ''' </summary>
    ''' <param name="type">�擾���閼�̎��</param>
    ''' <param name="dt">�f�[�^�e�[�u��</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overloads Sub GetDropdownData(ByVal type As MeisyouType, _
                                         ByRef dt As DataTable, _
                                         ByVal withSpaceRow As Boolean, _
                                         Optional ByVal withCode As Boolean = True)

        ' ���i��ʂ̎w��
        meisyou_syubetu = type.ToString("00")

        ' ���ʂ̃R���{�f�[�^�ݒ胁�\�b�h���g�p
        GetDropdownData(dt, withSpaceRow, withCode)

    End Sub

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̗L���Ȗ��̃e�[�u�����R�[�h��S�Ď擾���܂�<br/>
    ''' �{���\�b�h�𒼐ڎ��s�����ꍇ�A�R�[�h�X�ȊO�̑S�Ẵ��R�[�h���擾����܂�
    ''' </summary>
    ''' <param name="dt" >�f�[�^�e�[�u��</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, _
                                         ByVal withSpaceRow As Boolean, _
                                         Optional ByVal withCode As Boolean = True)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As MeisyouDataSet.MeisyouTableRow

        Const paramSyubetu As String = "@SYUBETU"

        commandTextSb.Append("SELECT code,meisyou ")
        commandTextSb.Append("  FROM m_meisyou ")
        commandTextSb.Append("  WHERE meisyou_syubetu = " & paramSyubetu)
        commandTextSb.Append("  AND   code     <> 9 ")
        commandTextSb.Append("  ORDER BY hyouji_jyun ")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramSyubetu, SqlDbType.Char, 2, meisyou_syubetu)}

        ' �f�[�^�̎擾
        Dim MeisyouDataSet As New MeisyouDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            MeisyouDataSet, MeisyouDataSet.MeisyouTable.TableName, commandParameters)

        Dim MeisyouDataTable As MeisyouDataSet.MeisyouTableDataTable = _
                    MeisyouDataSet.MeisyouTable

        If MeisyouDataTable.Count <> 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In MeisyouDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.code.ToString + ":" + row.meisyou, row.code, dt))
                Else
                    dt.Rows.Add(CreateRow(row.meisyou, row.code, dt))
                End If
            Next

        End If


    End Sub

End Class
