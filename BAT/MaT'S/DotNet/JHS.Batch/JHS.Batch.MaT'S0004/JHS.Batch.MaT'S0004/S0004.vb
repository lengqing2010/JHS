Option Explicit On
Option Strict On

Imports System.Data
Imports System.Text
Imports System.IO
Imports System.Transactions
Imports JHS.Batch.SqlExecutor
Imports JHS.Batch
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase

''' <summary>
''' ���я������ъǗ�ð��قɊi�[����
''' </summary>
''' <remarks>�v��Ǘ��\�ɕ\�������邽�߂̎��я������ъǗ�ð��قɊi�[������B</remarks>
''' <history>
''' <para>2012/10/18 ��A/���V �V�K�쐬 P-44979</para>
''' </history>
Public Class S0004
#Region "�萔"
    '�o�b�`ID
    Private Const CON_BATCH_ID As String = "bat_set4"
#End Region

#Region "�ϐ�"
    '�eEvent/Method�̓��쎞�ɂ�����A"EMAB��Q�Ή����̊i�[����"�����A���N���X��
    Private ReadOnly mMyNamePeriod As String = MyClass.GetType.FullName
    'DB�ڑ��X�g�����O
    Private mDBconnectionEarth As String
    Private mDBconnectionJHS As String
    'DB�ڑ�
    Private mConnectionEarth As SqlExecutor
    Private mConnectionJHS As SqlExecutor
    '���O���b�Z�[�W
    Private mLogMsg As New StringBuilder()
    '�V�K����
    Private mInsCount As Integer = 0
#End Region

#Region "Main����"
    ''' <summary>
    ''' Main����
    ''' </summary>
    ''' <param name="argv"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/���V �V�K�쐬 P-44979</para>
    ''' </history>
    Public Shared Function Main(ByVal argv As String()) As Integer
        Dim btProcess As S0004

        '������
        btProcess = New S0004()

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(btProcess.mMyNamePeriod & MyMethod.GetCurrentMethod.Name, argv)

        Try
            'DB�ڑ��X�g�����O
            btProcess.mDBconnectionEarth = Definition.GetConnectionStringEarth()
            btProcess.mDBconnectionJHS = Definition.GetConnectionStringJHS()

            'DB�ڑ�
            btProcess.mConnectionEarth = New SqlExecutor(btProcess.mDBconnectionEarth)
            btProcess.mConnectionJHS = New SqlExecutor(btProcess.mDBconnectionJHS)

            '�又�����Ăэ���()
            Call btProcess.Main_Process()

            Return 0
        Catch ex As Exception
            Dim strErrorMsg As String = ""
            If ex.Data.Item("ERROR_LOG") IsNot Nothing Then
                strErrorMsg = Convert.ToString(ex.Data.Item("ERROR_LOG"))
                btProcess.mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & strErrorMsg)
            End If

            '�ُ�𔭐�����ꍇ�A���O�t�@�C���ɏo�͂���
            btProcess.mLogMsg.AppendLine(ex.Message)

            btProcess.mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & "��L�̏��������[���o�b�N���܂����B")
            btProcess.mInsCount = 0
            Return 9
        Finally
            If btProcess.mConnectionEarth IsNot Nothing Then
                'DB�ڑ��̃O���[�Y
                btProcess.mConnectionEarth.Close()
                btProcess.mConnectionEarth.Dispose()
            End If

            If btProcess.mConnectionJHS IsNot Nothing Then
                'DB�ڑ��̃O���[�Y
                btProcess.mConnectionJHS.Close()
                btProcess.mConnectionJHS.Dispose()
            End If

            '�V�K���������O�t�@�C���ɏo�͂���
            btProcess.mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
            "���ъǗ��e�[�u����" & _
            Convert.ToString(btProcess.mInsCount) & _
            "���f�[�^���}������܂����B")

            '���O�o��
            Console.WriteLine(btProcess.mLogMsg.ToString())
        End Try
    End Function

#End Region

#Region "�又��"
    ''' <summary>
    ''' �又��
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/���V �V�K�쐬 P-44979</para>
    ''' </history>
    Private Sub Main_Process()
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(mMyNamePeriod & MyMethod.GetCurrentMethod.Name)

        Dim dtYear As DataTable                 '�v��N�x
        Dim strYear As String                   '�v��N�x
        Dim strBeginMonth As String             '�J�n��
        Dim strEndMonth As String               '������
        Dim dtEarthData As DataTable            '�d���������f�[�^
        Dim dtZennenEarthData As DataTable      '�O�N�d���������f�[�^
        Dim drData() As DataRow
        Dim options As New Transactions.TransactionOptions
        Dim i As Integer
        Dim j As Integer

        mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
                            "�v��N�x�h�m�h�t�@�C���ǎ�鏈�����J�n���܂����B")

        '�v��N�x�b�s�k�t�@�C����Ǎ��݁A�v��N�x���擾����
        dtYear = Definition.GetKeikakuNendo("S0004")

        '����ɓǎ�邱�Ƃ��ł��Ȃ������ꍇ�A�I������
        If dtYear.Rows.Count <= 0 Then
            Exit Sub
        End If

        mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
                            "�d���������̎��ъǗ��f�[�^�̎擾�������J�n���܂����B")

        'DB�ڑ��̃I�[�v��
        mConnectionEarth.Open()
        mConnectionJHS.Open(True)

        Try
            For i = 0 To dtYear.Rows.Count - 1
                strYear = Convert.ToString(dtYear.Rows(i)("Year"))
                strBeginMonth = Convert.ToString(dtYear.Rows(i)("BeginMonth"))
                strEndMonth = Convert.ToString(dtYear.Rows(i)("EndMonth"))

                '�d���������f�[�^���擾����
                dtEarthData = SelEarthData(mConnectionEarth, strBeginMonth, strEndMonth)

                If dtEarthData IsNot Nothing Then
                    mLogMsg.AppendLine(Format(Date.Now, "yyyy�NMM��dd�� HH:mm:ss  ") & _
                                        strYear & "�N�x�ɂd���������̎��ъǗ��e�[�u������" & _
                                        Convert.ToString(dtEarthData.Rows.Count) & _
                                        "���f�[�^���擾����܂����B")

                    '�O�N�d���������f�[�^���擾����
                    dtZennenEarthData = SelZennenEarthData(mConnectionEarth, strBeginMonth, strEndMonth)
                Else
                    Exit For
                End If

                '�Y���N�x�̃f�[�^���폜����
                DelJHSData(mConnectionJHS, strYear)

                dtEarthData.Columns("zennen_heikin_tanka").ReadOnly = False
                dtEarthData.Columns("zennen_siire_heikin_tanka").ReadOnly = False

                '�d���������̎��ъǗ��f�[�^�ɂ��A���[�v����
                For j = 0 To dtEarthData.Rows.Count - 1
                    drData = dtZennenEarthData.Select("kameiten_cd = '" & Convert.ToString(dtEarthData.Rows(j)("kameiten_cd")) _
                                & "' AND syouhin_cd = '" & Convert.ToString(dtEarthData.Rows(j)("syouhin_cd")) & "'")

                    '�O�N�f�[�^��ݒ肷��
                    If drData.Length > 0 Then
                        dtEarthData.Rows(j)("zennen_heikin_tanka") = drData(0)("heikin_tanka")
                        dtEarthData.Rows(j)("zennen_siire_heikin_tanka") = drData(0)("siire_heikin_tanka")
                    End If

                    '�󒍃f�[�^���[�N�V�K����
                    mInsCount = mInsCount + InsJHSData(mConnectionJHS, strYear, dtEarthData.Rows(j))
                Next

                '�f�[�^���������
                If dtEarthData IsNot Nothing Then
                    dtEarthData.Dispose()
                    dtEarthData = Nothing
                End If
            Next

            '�����̏ꍇ
            mConnectionJHS.Commit()
        Catch ex As Exception
            '���s�̏ꍇ
            mConnectionJHS.Rollback()
            Throw ex
        End Try
    End Sub
#End Region

#Region "SQL��"
    ''' <summary>
    ''' �d���������̃f�[�^���擾����
    ''' </summary>
    ''' <param name="objConnection">DB�ڑ�</param>
    ''' <param name="strBeginMonth">�J�n��</param>
    ''' <param name="strEndMonth">������</param>
    ''' <returns>�s�q�`�h�m�󒍃f�[�^</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/���V �V�K�쐬 P-44979</para>
    ''' </history>
    Private Function SelEarthData(ByVal objConnection As SqlExecutor, _
                                  ByVal strBeginMonth As String, _
                                  ByVal strEndMonth As String) As Data.DataTable
        'SQL�R�����g
        Dim sqlBuffer As New System.Text.StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '2013/10/23 ���F�ǉ��@��
        '�敪
        Dim strKubun As String
        strKubun = Definition.GetKubunName4
        '2013/10/23 ���F�ǉ��@��

        Try
            'SQL��	
            'With sqlBuffer
            '    .AppendLine(" SELECT kameiten_mei1, ")
            '    .AppendLine(" kameiten_cd, ")
            '    .AppendLine(" syouhin_cd, ")
            '    .AppendLine(" syouhin_mei, ")
            '    .AppendLine(" SUM(uri_gaku4) AS uri_gaku4, ")
            '    .AppendLine(" SUM(uri_gaku5) AS uri_gaku5, ")
            '    .AppendLine(" SUM(uri_gaku6) AS uri_gaku6, ")
            '    .AppendLine(" SUM(uri_gaku7) AS uri_gaku7, ")
            '    .AppendLine(" SUM(uri_gaku8) AS uri_gaku8, ")
            '    .AppendLine(" SUM(uri_gaku9) AS uri_gaku9, ")
            '    .AppendLine(" SUM(uri_gaku10) AS uri_gaku10, ")
            '    .AppendLine(" SUM(uri_gaku11) AS uri_gaku11, ")
            '    .AppendLine(" SUM(uri_gaku12) AS uri_gaku12, ")
            '    .AppendLine(" SUM(uri_gaku1) AS uri_gaku1, ")
            '    .AppendLine(" SUM(uri_gaku2) AS uri_gaku2, ")
            '    .AppendLine(" SUM(uri_gaku3) AS uri_gaku3, ")
            '    .AppendLine(" SUM(uri_suu4) AS uri_suu4, ")
            '    .AppendLine(" SUM(uri_suu5) AS uri_suu5, ")
            '    .AppendLine(" SUM(uri_suu6) AS uri_suu6, ")
            '    .AppendLine(" SUM(uri_suu7) AS uri_suu7, ")
            '    .AppendLine(" SUM(uri_suu8) AS uri_suu8, ")
            '    .AppendLine(" SUM(uri_suu9) AS uri_suu9, ")
            '    .AppendLine(" SUM(uri_suu10) AS uri_suu10, ")
            '    .AppendLine(" SUM(uri_suu11) AS uri_suu11, ")
            '    .AppendLine(" SUM(uri_suu12) AS uri_suu12, ")
            '    .AppendLine(" SUM(uri_suu1) AS uri_suu1, ")
            '    .AppendLine(" SUM(uri_suu2) AS uri_suu2, ")
            '    .AppendLine(" SUM(uri_suu3) AS uri_suu3, ")
            '    .AppendLine(" SUM(siire_gaku4) AS siire_gaku4, ")
            '    .AppendLine(" SUM(siire_gaku5) AS siire_gaku5, ")
            '    .AppendLine(" SUM(siire_gaku6) AS siire_gaku6, ")
            '    .AppendLine(" SUM(siire_gaku7) AS siire_gaku7, ")
            '    .AppendLine(" SUM(siire_gaku8) AS siire_gaku8, ")
            '    .AppendLine(" SUM(siire_gaku9) AS siire_gaku9, ")
            '    .AppendLine(" SUM(siire_gaku10) AS siire_gaku10, ")
            '    .AppendLine(" SUM(siire_gaku11) AS siire_gaku11, ")
            '    .AppendLine(" SUM(siire_gaku12) AS siire_gaku12, ")
            '    .AppendLine(" SUM(siire_gaku1) AS siire_gaku1, ")
            '    .AppendLine(" SUM(siire_gaku2) AS siire_gaku2, ")
            '    .AppendLine(" SUM(siire_gaku3) AS siire_gaku3, ")
            '    .AppendLine(" SUM(siire_suu4) AS siire_suu4, ")
            '    .AppendLine(" SUM(siire_suu5) AS siire_suu5, ")
            '    .AppendLine(" SUM(siire_suu6) AS siire_suu6, ")
            '    .AppendLine(" SUM(siire_suu7) AS siire_suu7, ")
            '    .AppendLine(" SUM(siire_suu8) AS siire_suu8, ")
            '    .AppendLine(" SUM(siire_suu9) AS siire_suu9, ")
            '    .AppendLine(" SUM(siire_suu10) AS siire_suu10, ")
            '    .AppendLine(" SUM(siire_suu11) AS siire_suu11, ")
            '    .AppendLine(" SUM(siire_suu12) AS siire_suu12, ")
            '    .AppendLine(" SUM(siire_suu1) AS siire_suu1, ")
            '    .AppendLine(" SUM(siire_suu2) AS siire_suu2, ")
            '    .AppendLine(" SUM(siire_suu3) AS siire_suu3, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku4),0) - ISNULL(SUM(siire_gaku4),0) AS uri_arari4, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku5),0) - ISNULL(SUM(siire_gaku5),0) AS uri_arari5, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku6),0) - ISNULL(SUM(siire_gaku6),0) AS uri_arari6, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku7),0) - ISNULL(SUM(siire_gaku7),0) AS uri_arari7, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku8),0) - ISNULL(SUM(siire_gaku8),0) AS uri_arari8, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku9),0) - ISNULL(SUM(siire_gaku9),0) AS uri_arari9, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku10),0) - ISNULL(SUM(siire_gaku10),0) AS uri_arari10, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku11),0) - ISNULL(SUM(siire_gaku11),0) AS uri_arari11, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku12),0) - ISNULL(SUM(siire_gaku12),0) AS uri_arari12, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku1),0) - ISNULL(SUM(siire_gaku1),0) AS uri_arari1, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku2),0) - ISNULL(SUM(siire_gaku2),0) AS uri_arari2, ")
            '    .AppendLine(" ISNULL(SUM(uri_gaku3),0) - ISNULL(SUM(siire_gaku3),0) AS uri_arari3, ")
            '    .AppendLine(" 0 AS zennen_heikin_tanka, ")
            '    .AppendLine(" 0 AS zennen_siire_heikin_tanka ")
            '    .AppendLine(" FROM ( ")
            '    .AppendLine(" SELECT kameiten_mei1, ")
            '    .AppendLine(" kameiten_cd, ")
            '    .AppendLine(" syouhin_cd, ")
            '    .AppendLine(" syouhin_mei, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/4' THEN uri_gaku ELSE 0 END AS uri_gaku4, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/5' THEN uri_gaku ELSE 0 END AS uri_gaku5, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/6' THEN uri_gaku ELSE 0 END AS uri_gaku6, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/7' THEN uri_gaku ELSE 0 END AS uri_gaku7, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/8' THEN uri_gaku ELSE 0 END AS uri_gaku8, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/9' THEN uri_gaku ELSE 0 END AS uri_gaku9, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/10' THEN uri_gaku ELSE 0 END AS uri_gaku10, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/11' THEN uri_gaku ELSE 0 END AS uri_gaku11, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/12' THEN uri_gaku ELSE 0 END AS uri_gaku12, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/1' THEN uri_gaku ELSE 0 END AS uri_gaku1, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/2' THEN uri_gaku ELSE 0 END AS uri_gaku2, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/3' THEN uri_gaku ELSE 0 END AS uri_gaku3, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/4' THEN uri_suu ELSE 0 END AS uri_suu4, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/5' THEN uri_suu ELSE 0 END AS uri_suu5, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/6' THEN uri_suu ELSE 0 END AS uri_suu6, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/7' THEN uri_suu ELSE 0 END AS uri_suu7, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/8' THEN uri_suu ELSE 0 END AS uri_suu8, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/9' THEN uri_suu ELSE 0 END AS uri_suu9, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/10' THEN uri_suu ELSE 0 END AS uri_suu10, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/11' THEN uri_suu ELSE 0 END AS uri_suu11, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/12' THEN uri_suu ELSE 0 END AS uri_suu12, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/1' THEN uri_suu ELSE 0 END AS uri_suu1, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/2' THEN uri_suu ELSE 0 END AS uri_suu2, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/3' THEN uri_suu ELSE 0 END AS uri_suu3, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/4' THEN siire_gaku ELSE 0 END AS siire_gaku4, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/5' THEN siire_gaku ELSE 0 END AS siire_gaku5, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/6' THEN siire_gaku ELSE 0 END AS siire_gaku6, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/7' THEN siire_gaku ELSE 0 END AS siire_gaku7, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/8' THEN siire_gaku ELSE 0 END AS siire_gaku8, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/9' THEN siire_gaku ELSE 0 END AS siire_gaku9, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/10' THEN siire_gaku ELSE 0 END AS siire_gaku10, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/11' THEN siire_gaku ELSE 0 END AS siire_gaku11, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/12' THEN siire_gaku ELSE 0 END AS siire_gaku12, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/1' THEN siire_gaku ELSE 0 END AS siire_gaku1, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/2' THEN siire_gaku ELSE 0 END AS siire_gaku2, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/3' THEN siire_gaku ELSE 0 END AS siire_gaku3, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/4' THEN siire_suu ELSE 0 END AS siire_suu4, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/5' THEN siire_suu ELSE 0 END AS siire_suu5, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/6' THEN siire_suu ELSE 0 END AS siire_suu6, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/7' THEN siire_suu ELSE 0 END AS siire_suu7, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/8' THEN siire_suu ELSE 0 END AS siire_suu8, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/9' THEN siire_suu ELSE 0 END AS siire_suu9, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/10' THEN siire_suu ELSE 0 END AS siire_suu10, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/11' THEN siire_suu ELSE 0 END AS siire_suu11, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/12' THEN siire_suu ELSE 0 END AS siire_suu12, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/1' THEN siire_suu ELSE 0 END AS siire_suu1, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/2' THEN siire_suu ELSE 0 END AS siire_suu2, ")
            '    .AppendLine(" CASE WHEN denpyou_date = '/3' THEN siire_suu ELSE 0 END AS siire_suu3 ")
            '    .AppendLine(" FROM ( ")
            '    .AppendLine(" SELECT k.kameiten_mei1, ")
            '    .AppendLine(" k.kameiten_cd, ")
            '    .AppendLine(" '/'+CONVERT(VARCHAR,MONTH(denpyou_date)) AS denpyou_date, ")
            '    .AppendLine(" s.syouhin_syubetu1 AS syouhin_cd, ")
            '    .AppendLine(" km.meisyou AS syouhin_mei, ")
            '    .AppendLine(" SUM(urisiire.uri_gaku) AS uri_gaku, ")

            '    '------�w�ENo29�̎d�l�ύX--------�C��(2013.03.05)-----------Begin
            '    '.AppendLine(" SUM(CASE s.souko_cd WHEN '140' THEN 0 ELSE uri_suu END) AS uri_suu, ")
            '    .AppendLine(" SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE uri_suu END) AS uri_suu, ")
            '    '------�w�ENo29�̎d�l�ύX--------�C��(2013.03.05)-----------End

            '    .AppendLine(" SUM(urisiire.siire_gaku) AS siire_gaku, ")

            '    '------�w�ENo29�̎d�l�ύX--------�C��(2013.03.05)-----------Begin
            '    '.AppendLine(" SUM(CASE s.souko_cd WHEN '140' THEN 0 ELSE siire_suu END) AS siire_suu ")
            '    .AppendLine(" SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE siire_suu END) AS siire_suu ")
            '    '------�w�ENo29�̎d�l�ύX--------�C��(2013.03.05)-----------End

            '    .AppendLine(" FROM m_kameiten k WITH(READUNCOMMITTED) ")
            '    .AppendLine(" LEFT JOIN ( ")
            '    '�R�t���e�[�u���^�C�v��1�i�@�ʐ����j�͔����ް�ð��ق̉����X���ނŏW�v
            '    .AppendLine("  SELECT kameiten_cd, ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  denpyou_uri_date denpyou_date, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(u.kbn,'')+ISNULL(u.bangou,'') AS bukken_bangou ")
            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN @beginYear AND @endYear ")
            '    .AppendLine("  AND u.himoduke_table_type=1 ")
            '    .AppendLine("  UNION ALL ")
            '    '�R�t���e�[�u���^�C�v��2�i�X�ʐ����j�͔����ް�ð��ق̕R�t�����ސ擪5���iAF�Ŏn�܂���̂͏����j�ŏW�v
            '    .AppendLine("  SELECT SUBSTRING(u.himoduke_cd,1,5), ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  denpyou_uri_date denpyou_date, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------Begin
            '    .AppendLine("  '' AS bukken_bangou ")
            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN @beginYear AND @endYear ")
            '    .AppendLine("  AND u.himoduke_table_type=2  ")
            '    .AppendLine("  AND SUBSTRING(u.himoduke_cd,1,2)<>'AF' ")
            '    .AppendLine("  UNION ALL ")
            '    '�R�t���e�[�u���^�C�v��3�i�X�ʏ��������j�͔����ް�ð��ق̕R�t�����ސ擪5���ŏW�v
            '    .AppendLine("  SELECT SUBSTRING(u.himoduke_cd,1,5), ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  denpyou_uri_date denpyou_date, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------Begin
            '    .AppendLine("  '' AS bukken_bangou ")
            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_jiban j ")
            '    .AppendLine("  ON u.kbn=j.kbn ")
            '    .AppendLine("  AND u.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN @beginYear AND @endYear ")
            '    .AppendLine("  AND u.himoduke_table_type=3 ")
            '    .AppendLine("  UNION ALL ")
            '    '�R�t���e�[�u���^�C�v��9�i�ėp����j�͔����ް�ð��ق̉����X���ނŏW�v
            '    .AppendLine("  SELECT j.kameiten_cd, ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  denpyou_uri_date denpyou_date, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(u.kbn,'')+ISNULL(u.bangou,'') AS bukken_bangou ")
            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_jiban j ")
            '    .AppendLine("  ON u.kbn=j.kbn ")
            '    .AppendLine("  AND u.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN @beginYear AND @endYear ")
            '    .AppendLine("  AND u.himoduke_table_type=9 ")
            '    .AppendLine("  UNION ALL ")
            '    '�R�t���e�[�u���^�C�v��1�i�@�ʐ����j�͒n�Ճe�[�u�������X���ނŏW�v
            '    .AppendLine("  SELECT j.kameiten_cd, ")
            '    .AppendLine("  s.syouhin_cd, ")
            '    .AppendLine("  s.denpyou_siire_date denpyou_date, ")
            '    .AppendLine("  0 uri_gaku, ")
            '    .AppendLine("  0 uri_suu, ")
            '    .AppendLine("  siire_gaku, ")
            '    .AppendLine("  s.suu siire_suu, ")

            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(s.kbn,'')+ISNULL(s.bangou,'') AS bukken_bangou ")
            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------End

            '    .AppendLine("  FROM t_siire_data s WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_jiban j WITH(READUNCOMMITTED) ")
            '    .AppendLine("  ON s.kbn=j.kbn ")
            '    .AppendLine("  AND s.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE s.denpyou_siire_date BETWEEN @beginYear AND @endYear ")
            '    .AppendLine("  AND s.himoduke_table_type=1 ")
            '    .AppendLine("  UNION ALL ")
            '    '�R�t���e�[�u���^�C�v��9�i�ėp�d���j�͔ėp�d���e�[�u���̉����X���ނ��A�󔒂�������n�Ճe�[�u���̉����X���ނŏW�v�i�擾�ł��Ȃ����̂��ł邪�d���Ȃ��j
            '    .AppendLine("  SELECT ISNULL(h.kameiten_cd,j.kameiten_cd), ")
            '    .AppendLine("  s.syouhin_cd, ")
            '    .AppendLine("  s.denpyou_siire_date denpyou_date, ")
            '    .AppendLine("  0 uri_gaku, ")
            '    .AppendLine("  0 uri_suu, ")
            '    .AppendLine("  siire_gaku, ")
            '    .AppendLine("  s.suu siire_suu, ")

            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(s.kbn,'')+ISNULL(s.bangou,'') AS bukken_bangou ")
            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------End

            '    .AppendLine("  FROM t_siire_data s WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_hannyou_siire h WITH(READUNCOMMITTED) ")
            '    .AppendLine("  ON s.himoduke_cd=h.han_siire_unique_no ")
            '    .AppendLine("  LEFT JOIN t_jiban j ")
            '    .AppendLine("  ON s.kbn=j.kbn ")
            '    .AppendLine("  and s.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE s.denpyou_siire_date BETWEEN @beginYear AND @endYear ")
            '    .AppendLine("  AND s.himoduke_table_type=9 ")
            '    .AppendLine(" ) urisiire ")
            '    .AppendLine(" ON k.kameiten_cd=urisiire.kameiten_cd ")
            '    .AppendLine(" INNER JOIN m_syouhin s WITH(READUNCOMMITTED) ")
            '    .AppendLine(" ON urisiire.syouhin_cd=s.syouhin_cd ")
            '    .AppendLine(" INNER JOIN m_kakutyou_meisyou km ")
            '    .AppendLine(" ON s.syouhin_syubetu1=km.code ")
            '    .AppendLine(" AND km.meisyou_syubetu='51' ")

            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------Begin
            '    .AppendLine(" LEFT JOIN t_teibetu_seikyuu t WITH(READUNCOMMITTED) ")
            '    .AppendLine(" ON urisiire.bukken_bangou=t.kbn+t.hosyousyo_no ")
            '    .AppendLine(" AND s.souko_cd='140' ")
            '    .AppendLine(" AND t.bunrui_cd='130' ")
            '    .AppendLine(" AND t.uri_gaku<>0 ")
            '    .AppendLine(" AND t.denpyou_uri_date IS NOT NULL ")
            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------End

            '    '���̂�����ȊO�̋敪�̌n��̐ݒ�ɂȂ����炱��ł�NG
            '    .AppendLine(" WHERE ISNULL(s.syouhin_syubetu1,'')<>'' ")

            '    '2013/10/23 ���F�C���@��
            '    .AppendLine(" AND k.kbn IN (" & strKubun & ") ")
            '    '2013/10/23 ���F�C���@��

            '    '�����X�E�N���E���i���1�ŏW�v
            '    .AppendLine(" GROUP BY k.kameiten_mei1, ")
            '    .AppendLine(" k.kameiten_cd, ")
            '    .AppendLine(" '/'+CONVERT(VARCHAR,MONTH(denpyou_date)), ")
            '    .AppendLine(" s.syouhin_syubetu1, ")
            '    .AppendLine(" km.meisyou ")
            '    .AppendLine(" ) AS SUB_MK ")
            '    .AppendLine(" ) AS MK ")
            '    .AppendLine(" GROUP BY kameiten_mei1, ")
            '    .AppendLine(" kameiten_cd, ")
            '    .AppendLine(" syouhin_cd, ")
            '    .AppendLine(" syouhin_mei ")
            '    .AppendLine(" ORDER BY  ")
            '    .AppendLine(" kameiten_cd, ")
            '    .AppendLine(" syouhin_cd ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0004_01)
            strSql = strSql.Replace("@strKubun", strKubun)
            sqlBuffer.AppendLine(strSql)

            '�o�����^
            paramList.Add(MakeParam("@beginYear", SqlDbType.VarChar, 10, strBeginMonth))         '�J�n�N��
            paramList.Add(MakeParam("@endYear", SqlDbType.VarChar, 10, strEndMonth))             '�I���N��

            '�d���������̃f�[�^
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())
        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "�d���������̃f�[�^�̎擾�������ُ�I�����܂����B")
            End If

            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' �O�N�d���������̃f�[�^���擾����
    ''' </summary>
    ''' <param name="objConnection">DB�ڑ�</param>
    ''' <param name="strBeginMonth">�J�n��</param>
    ''' <param name="strEndMonth">������</param>
    ''' <returns>�O�N�s�q�`�h�m�󒍃f�[�^</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/���V �V�K�쐬 P-44979</para>
    ''' </history>
    Private Function SelZennenEarthData(ByVal objConnection As SqlExecutor, _
                                        ByVal strBeginMonth As String, _
                                        ByVal strEndMonth As String) As Data.DataTable
        'SQL�R�����g
        Dim sqlBuffer As New System.Text.StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '2013/10/23 ���F�ǉ��@��
        '�敪
        Dim strKubun As String
        strKubun = Definition.GetKubunName4
        '2013/10/23 ���F�ǉ��@��

        Try
            'SQL��	
            'With sqlBuffer
            '    .AppendLine(" SELECT ")
            '    .AppendLine(" k.kameiten_cd, ")                                                 '�����X����
            '    .AppendLine(" s.syouhin_syubetu1 AS syouhin_cd, ")                              '�v��Ǘ�_���i�R�[�h
            '    '------�w�ENo29�̎d�l�ύX--------�C��(2013.03.05)-----------Begin
            '    '.AppendLine(" CASE WHEN ISNULL(SUM(CASE s.souko_cd WHEN '140' THEN 0 ELSE uri_suu END),0) = 0 THEN 0 ELSE ")
            '    '.AppendLine(" ISNULL(SUM(uri_gaku),0) / SUM(CASE s.souko_cd WHEN '140' THEN 0 ELSE uri_suu END) END AS heikin_tanka, ")    '�O�N_���ϒP��
            '    '.AppendLine(" CASE WHEN ISNULL(SUM(CASE s.souko_cd WHEN '140' THEN 0 ELSE siire_suu END),0) = 0 THEN 0 ELSE ")
            '    '.AppendLine(" ISNULL(SUM(siire_gaku),0) / SUM(CASE s.souko_cd WHEN '140' THEN 0 ELSE siire_suu END) END AS siire_heikin_tanka ")   '�O�N_�d�����ϒP��
            '    .AppendLine(" CASE WHEN ISNULL(SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE uri_suu END),0) = 0 THEN 0 ELSE ")
            '    .AppendLine(" ISNULL(SUM(urisiire.uri_gaku),0) / SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE uri_suu END) END AS heikin_tanka, ")    '�O�N_���ϒP��
            '    .AppendLine(" CASE WHEN ISNULL(SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE siire_suu END),0) = 0 THEN 0 ELSE ")
            '    .AppendLine(" ISNULL(SUM(urisiire.siire_gaku),0) / SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE siire_suu END) END AS siire_heikin_tanka ")   '�O�N_�d�����ϒP��
            '    '------�w�ENo29�̎d�l�ύX--------�C��(2013.03.05)-----------End
            '    .AppendLine(" FROM m_kameiten k WITH(READUNCOMMITTED) ")
            '    .AppendLine(" LEFT JOIN ( ")
            '    '�R�t���e�[�u���^�C�v��1�i�@�ʐ����j�͔����ް�ð��ق̉����X���ނŏW�v
            '    .AppendLine("  SELECT kameiten_cd, ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(u.kbn,'')+ISNULL(u.bangou,'') AS bukken_bangou  ")
            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) ")
            '    .AppendLine("  AND u.himoduke_table_type=1 ")
            '    .AppendLine("  UNION ALL ")
            '    '�R�t���e�[�u���^�C�v��2�i�X�ʐ����j�͔����ް�ð��ق̕R�t�����ސ擪5���iAF�Ŏn�܂���̂͏����j�ŏW�v
            '    .AppendLine("  SELECT SUBSTRING(u.himoduke_cd,1,5), ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------Begin
            '    .AppendLine("  '' AS bukken_bangou  ")
            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) ")
            '    .AppendLine("  AND u.himoduke_table_type=2  ")
            '    .AppendLine("  AND SUBSTRING(u.himoduke_cd,1,2)<>'AF' ")
            '    .AppendLine("  UNION ALL ")
            '    '�R�t���e�[�u���^�C�v��3�i�X�ʏ��������j�͔����ް�ð��ق̕R�t�����ސ擪5���ŏW�v
            '    .AppendLine("  SELECT SUBSTRING(u.himoduke_cd,1,5), ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------Begin
            '    .AppendLine("  '' AS bukken_bangou  ")
            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_jiban j  ")
            '    .AppendLine("  ON u.kbn=j.kbn  ")
            '    .AppendLine("  AND u.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) ")
            '    .AppendLine("  AND u.himoduke_table_type=3 ")
            '    .AppendLine("  UNION ALL ")
            '    '�R�t���e�[�u���^�C�v��9�i�ėp����j�͔����ް�ð��ق̉����X���ނŏW�v
            '    .AppendLine("  SELECT j.kameiten_cd, ")
            '    .AppendLine("  u.syouhin_cd, ")
            '    .AppendLine("  uri_gaku, ")
            '    .AppendLine("  u.suu uri_suu, ")
            '    .AppendLine("  0 siire_gaku, ")
            '    .AppendLine("  0 siire_suu, ")

            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(u.kbn,'')+ISNULL(u.bangou,'') AS bukken_bangou  ")
            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------End

            '    .AppendLine("  FROM t_uriage_data u WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_jiban j  ")
            '    .AppendLine("  ON u.kbn=j.kbn  ")
            '    .AppendLine("  AND u.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE u.denpyou_uri_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) ")
            '    .AppendLine("  AND u.himoduke_table_type=9 ")
            '    .AppendLine("  UNION ALL  ")
            '    '�R�t���e�[�u���^�C�v��1�i�@�ʐ����j�͒n�Ճe�[�u�������X���ނŏW�v
            '    .AppendLine("  SELECT j.kameiten_cd, ")
            '    .AppendLine("  s.syouhin_cd, ")
            '    .AppendLine("  0 uri_gaku, ")
            '    .AppendLine("  0 uri_suu, ")
            '    .AppendLine("  siire_gaku, ")
            '    .AppendLine("  s.suu siire_suu, ")

            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(s.kbn,'')+ISNULL(s.bangou,'') AS bukken_bangou  ")
            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------End

            '    .AppendLine("  FROM t_siire_data s WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_jiban j WITH(READUNCOMMITTED)  ")
            '    .AppendLine("  ON s.kbn=j.kbn  ")
            '    .AppendLine("  AND s.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE s.denpyou_siire_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) ")
            '    .AppendLine("  AND s.himoduke_table_type=1 ")
            '    .AppendLine("  UNION ALL  ")
            '    '�R�t���e�[�u���^�C�v��9�i�ėp�d���j�͔ėp�d���e�[�u���̉����X���ނ��A�󔒂�������n�Ճe�[�u���̉����X���ނŏW�v�i�擾�ł��Ȃ����̂��ł邪�d���Ȃ��j
            '    .AppendLine("  SELECT ISNULL(h.kameiten_cd,j.kameiten_cd), ")
            '    .AppendLine("  s.syouhin_cd, ")
            '    .AppendLine("  0 uri_gaku, ")
            '    .AppendLine("  0 uri_suu, ")
            '    .AppendLine("  siire_gaku, ")
            '    .AppendLine("  s.suu siire_suu, ")

            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------Begin
            '    .AppendLine("  ISNULL(s.kbn,'')+ISNULL(s.bangou,'') AS bukken_bangou  ")
            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------End

            '    .AppendLine("  FROM t_siire_data s WITH(READUNCOMMITTED) ")
            '    .AppendLine("  INNER JOIN t_hannyou_siire h WITH(READUNCOMMITTED)  ")
            '    .AppendLine("  ON s.himoduke_cd=h.han_siire_unique_no ")
            '    .AppendLine("  LEFT JOIN t_jiban j  ")
            '    .AppendLine("  ON s.kbn=j.kbn  ")
            '    .AppendLine("  and s.bangou=j.hosyousyo_no ")
            '    .AppendLine("  WHERE s.denpyou_siire_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) ")
            '    .AppendLine("  AND s.himoduke_table_type=9 ")
            '    .AppendLine(" ) urisiire   ")
            '    .AppendLine(" ON k.kameiten_cd=urisiire.kameiten_cd ")
            '    .AppendLine(" INNER JOIN m_syouhin s WITH(READUNCOMMITTED)  ")
            '    .AppendLine(" ON urisiire.syouhin_cd=s.syouhin_cd ")
            '    .AppendLine(" INNER JOIN m_kakutyou_meisyou km  ")
            '    .AppendLine(" ON s.syouhin_syubetu1=km.code  ")
            '    .AppendLine(" AND km.meisyou_syubetu='51' ")

            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------Begin
            '    .AppendLine(" LEFT JOIN t_teibetu_seikyuu t WITH(READUNCOMMITTED) ")
            '    .AppendLine(" ON urisiire.bukken_bangou=t.kbn+t.hosyousyo_no ")
            '    .AppendLine(" AND s.souko_cd='140' ")
            '    .AppendLine(" AND t.bunrui_cd='130' ")
            '    .AppendLine(" AND t.uri_gaku<>0 ")
            '    .AppendLine(" AND t.denpyou_uri_date IS NOT NULL ")
            '    '------�w�ENo29�̎d�l�ύX--------�ǉ�(2013.03.05)-----------End

            '    '���̂�����ȊO�̋敪�̌n��̐ݒ�ɂȂ����炱��ł�NG
            '    .AppendLine(" WHERE ISNULL(s.syouhin_syubetu1,'')<>''  ")
            '    '2013/10/23 ���F�C���@��
            '    .AppendLine(" AND k.kbn IN (" & strKubun & ")  ")
            '    '2013/10/23 ���F�C���@��
            '    '�����X�E�N���E���i���1�ŏW�v
            '    .AppendLine(" GROUP BY ")
            '    .AppendLine(" k.kameiten_cd, ")
            '    .AppendLine(" s.syouhin_syubetu1 ")
            '    .AppendLine(" ORDER BY ")
            '    .AppendLine(" k.kameiten_cd, ")
            '    .AppendLine(" s.syouhin_syubetu1 ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0004_02)
            strSql = strSql.Replace("@strKubun", strKubun)
            sqlBuffer.AppendLine(strSql)

            '�o�����^
            paramList.Add(MakeParam("@beginYear", SqlDbType.VarChar, 10, strBeginMonth))         '�J�n�N��
            paramList.Add(MakeParam("@endYear", SqlDbType.VarChar, 10, strEndMonth))             '�I���N��

            '�d���������̃f�[�^
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())
        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "�d���������̑O�N�f�[�^�̎擾�������ُ�I�����܂����B")
            End If

            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' JHS�̃f�[�^���폜����
    ''' </summary>
    ''' <param name="objConnection">DB�ڑ�</param>
    ''' <param name="strYear">�Ώ۔N�x</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/���V �V�K�쐬 P-44979</para>
    ''' </history>
    Private Sub DelJHSData(ByVal objConnection As SqlExecutor, _
                           ByVal strYear As String)
        'SQL�R�����g
        Dim sqlBuffer As New System.Text.StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL��	
            'With sqlBuffer
            '    .AppendLine(" DELETE FROM t_jisseki_kanri ")
            '    .AppendLine(" WHERE keikaku_nendo = @keikaku_nendo ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0004_03)
            sqlBuffer.AppendLine(strSql)

            '�o�����^
            paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strYear))      '�v��_�N�x

            objConnection.ExecuteNonQuery(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())
        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "���ъǗ��e�[�u���̃f�[�^�̍폜�������ُ�I�����܂����B")
            End If

            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' JHS�̃f�[�^��o�^����
    ''' </summary>
    ''' <param name="objConnection">DB�ڑ�</param>
    ''' <param name="strYear">�v��N�x</param>
    ''' <param name="drData">�o�^�f�[�^</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 ��A/���V �V�K�쐬 P-44979</para>
    ''' </history>
    Private Function InsJHSData(ByVal objConnection As SqlExecutor, _
                                ByVal strYear As String, _
                                ByVal drData As DataRow) As Integer

        'SQL�R�����g
        Dim sqlBuffer As New System.Text.StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL��	
            'With sqlBuffer
            '    .AppendLine(" INSERT INTO t_jisseki_kanri ( ")
            '    .AppendLine(" [keikaku_nendo], ")
            '    .AppendLine(" [kameiten_cd], ")
            '    .AppendLine(" [keikaku_kanri_syouhin_cd], ")
            '    .AppendLine(" [kameiten_mei], ")
            '    .AppendLine(" [bunbetu_cd], ")
            '    .AppendLine(" [zennen_heikin_tanka], ")
            '    .AppendLine(" [zennen_siire_heikin_tanka], ")
            '    .AppendLine(" [4gatu_jisseki_kensuu], ")
            '    .AppendLine(" [4gatu_jisseki_kingaku], ")
            '    .AppendLine(" [4gatu_jisseki_arari], ")
            '    .AppendLine(" [5gatu_jisseki_kensuu], ")
            '    .AppendLine(" [5gatu_jisseki_kingaku], ")
            '    .AppendLine(" [5gatu_jisseki_arari], ")
            '    .AppendLine(" [6gatu_jisseki_kensuu], ")
            '    .AppendLine(" [6gatu_jisseki_kingaku], ")
            '    .AppendLine(" [6gatu_jisseki_arari], ")
            '    .AppendLine(" [7gatu_jisseki_kensuu], ")
            '    .AppendLine(" [7gatu_jisseki_kingaku], ")
            '    .AppendLine(" [7gatu_jisseki_arari], ")
            '    .AppendLine(" [8gatu_jisseki_kensuu], ")
            '    .AppendLine(" [8gatu_jisseki_kingaku], ")
            '    .AppendLine(" [8gatu_jisseki_arari], ")
            '    .AppendLine(" [9gatu_jisseki_kensuu], ")
            '    .AppendLine(" [9gatu_jisseki_kingaku], ")
            '    .AppendLine(" [9gatu_jisseki_arari], ")
            '    .AppendLine(" [10gatu_jisseki_kensuu], ")
            '    .AppendLine(" [10gatu_jisseki_kingaku], ")
            '    .AppendLine(" [10gatu_jisseki_arari], ")
            '    .AppendLine(" [11gatu_jisseki_kensuu], ")
            '    .AppendLine(" [11gatu_jisseki_kingaku], ")
            '    .AppendLine(" [11gatu_jisseki_arari], ")
            '    .AppendLine(" [12gatu_jisseki_kensuu], ")
            '    .AppendLine(" [12gatu_jisseki_kingaku], ")
            '    .AppendLine(" [12gatu_jisseki_arari], ")
            '    .AppendLine(" [1gatu_jisseki_kensuu], ")
            '    .AppendLine(" [1gatu_jisseki_kingaku], ")
            '    .AppendLine(" [1gatu_jisseki_arari], ")
            '    .AppendLine(" [2gatu_jisseki_kensuu], ")
            '    .AppendLine(" [2gatu_jisseki_kingaku], ")
            '    .AppendLine(" [2gatu_jisseki_arari], ")
            '    .AppendLine(" [3gatu_jisseki_kensuu], ")
            '    .AppendLine(" [3gatu_jisseki_kingaku], ")
            '    .AppendLine(" [3gatu_jisseki_arari], ")
            '    .AppendLine(" [add_login_user_id], ")
            '    .AppendLine(" [add_datetime], ")
            '    .AppendLine(" [upd_login_user_id], ")
            '    .AppendLine(" [upd_datetime] ")
            '    .AppendLine(" ) ")
            '    .AppendLine(" SELECT ")
            '    .AppendLine(" @keikaku_nendo, ")
            '    .AppendLine(" @kameiten_cd, ")
            '    .AppendLine(" @keikaku_kanri_syouhin_cd, ")
            '    .AppendLine(" @kameiten_mei, ")
            '    .AppendLine(" ISNULL((SELECT bunbetu_cd ")
            '    .AppendLine(" FROM m_keikaku_kanri_syouhin ")
            '    .AppendLine(" WHERE keikaku_nendo = @keikaku_nendo ")
            '    .AppendLine(" AND keikaku_kanri_syouhin_cd = @keikaku_kanri_syouhin_cd),''), ")
            '    .AppendLine(" @zennen_heikin_tanka, ")
            '    .AppendLine(" @zennen_siire_heikin_tanka, ")
            '    .AppendLine(" @gatu_jisseki_kensuu4, ")
            '    .AppendLine(" @gatu_jisseki_kingaku4, ")
            '    .AppendLine(" @gatu_jisseki_arari4, ")
            '    .AppendLine(" @gatu_jisseki_kensuu5, ")
            '    .AppendLine(" @gatu_jisseki_kingaku5, ")
            '    .AppendLine(" @gatu_jisseki_arari5, ")
            '    .AppendLine(" @gatu_jisseki_kensuu6, ")
            '    .AppendLine(" @gatu_jisseki_kingaku6, ")
            '    .AppendLine(" @gatu_jisseki_arari6, ")
            '    .AppendLine(" @gatu_jisseki_kensuu7, ")
            '    .AppendLine(" @gatu_jisseki_kingaku7, ")
            '    .AppendLine(" @gatu_jisseki_arari7, ")
            '    .AppendLine(" @gatu_jisseki_kensuu8, ")
            '    .AppendLine(" @gatu_jisseki_kingaku8, ")
            '    .AppendLine(" @gatu_jisseki_arari8, ")
            '    .AppendLine(" @gatu_jisseki_kensuu9, ")
            '    .AppendLine(" @gatu_jisseki_kingaku9, ")
            '    .AppendLine(" @gatu_jisseki_arari9, ")
            '    .AppendLine(" @gatu_jisseki_kensuu10, ")
            '    .AppendLine(" @gatu_jisseki_kingaku10, ")
            '    .AppendLine(" @gatu_jisseki_arari10, ")
            '    .AppendLine(" @gatu_jisseki_kensuu11, ")
            '    .AppendLine(" @gatu_jisseki_kingaku11, ")
            '    .AppendLine(" @gatu_jisseki_arari11, ")
            '    .AppendLine(" @gatu_jisseki_kensuu12, ")
            '    .AppendLine(" @gatu_jisseki_kingaku12, ")
            '    .AppendLine(" @gatu_jisseki_arari12, ")
            '    .AppendLine(" @gatu_jisseki_kensuu1, ")
            '    .AppendLine(" @gatu_jisseki_kingaku1, ")
            '    .AppendLine(" @gatu_jisseki_arari1, ")
            '    .AppendLine(" @gatu_jisseki_kensuu2, ")
            '    .AppendLine(" @gatu_jisseki_kingaku2, ")
            '    .AppendLine(" @gatu_jisseki_arari2, ")
            '    .AppendLine(" @gatu_jisseki_kensuu3, ")
            '    .AppendLine(" @gatu_jisseki_kingaku3, ")
            '    .AppendLine(" @gatu_jisseki_arari3, ")
            '    .AppendLine(" @add_login_user_id, ")
            '    .AppendLine(" GETDATE(), ")
            '    .AppendLine(" NULL, ")
            '    .AppendLine(" NULL ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0004_04)
            sqlBuffer.AppendLine(strSql)

            '�o�����^
            paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strYear))                              '�v��_�N�x
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, drData("kameiten_cd")))               '�����X����
            paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, drData("syouhin_cd")))   '�v��Ǘ�_���i�R�[�h
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, drData("kameiten_mei1")))           '�����X��
            paramList.Add(MakeParam("@zennen_heikin_tanka", SqlDbType.BigInt, 12, drData("zennen_heikin_tanka")))   '�O�N_���ϒP��
            paramList.Add(MakeParam("@zennen_siire_heikin_tanka", SqlDbType.BigInt, 12, drData("zennen_siire_heikin_tanka"))) '�O�N_�d�����ϒP��
            paramList.Add(MakeParam("@gatu_jisseki_kensuu4", SqlDbType.BigInt, 12, drData("uri_suu4")))         '4��_���ь���
            paramList.Add(MakeParam("@gatu_jisseki_kingaku4", SqlDbType.BigInt, 12, drData("uri_gaku4")))       '4��_���ы��z
            paramList.Add(MakeParam("@gatu_jisseki_arari4", SqlDbType.BigInt, 12, drData("uri_arari4")))        '4��_���ёe��
            paramList.Add(MakeParam("@gatu_jisseki_kensuu5", SqlDbType.BigInt, 12, drData("uri_suu5")))         '5��_���ь���
            paramList.Add(MakeParam("@gatu_jisseki_kingaku5", SqlDbType.BigInt, 12, drData("uri_gaku5")))       '5��_���ы��z
            paramList.Add(MakeParam("@gatu_jisseki_arari5", SqlDbType.BigInt, 12, drData("uri_arari5")))        '5��_���ёe��
            paramList.Add(MakeParam("@gatu_jisseki_kensuu6", SqlDbType.BigInt, 12, drData("uri_suu6")))         '6��_���ь���
            paramList.Add(MakeParam("@gatu_jisseki_kingaku6", SqlDbType.BigInt, 12, drData("uri_gaku6")))       '6��_���ы��z
            paramList.Add(MakeParam("@gatu_jisseki_arari6", SqlDbType.BigInt, 12, drData("uri_arari6")))        '6��_���ёe��
            paramList.Add(MakeParam("@gatu_jisseki_kensuu7", SqlDbType.BigInt, 12, drData("uri_suu7")))         '7��_���ь���
            paramList.Add(MakeParam("@gatu_jisseki_kingaku7", SqlDbType.BigInt, 12, drData("uri_gaku7")))       '7��_���ы��z
            paramList.Add(MakeParam("@gatu_jisseki_arari7", SqlDbType.BigInt, 12, drData("uri_arari7")))        '7��_���ёe��
            paramList.Add(MakeParam("@gatu_jisseki_kensuu8", SqlDbType.BigInt, 12, drData("uri_suu8")))         '8��_���ь���
            paramList.Add(MakeParam("@gatu_jisseki_kingaku8", SqlDbType.BigInt, 12, drData("uri_gaku8")))       '8��_���ы��z
            paramList.Add(MakeParam("@gatu_jisseki_arari8", SqlDbType.BigInt, 12, drData("uri_arari8")))        '8��_���ёe��
            paramList.Add(MakeParam("@gatu_jisseki_kensuu9", SqlDbType.BigInt, 12, drData("uri_suu9")))         '9��_���ь���
            paramList.Add(MakeParam("@gatu_jisseki_kingaku9", SqlDbType.BigInt, 12, drData("uri_gaku9")))       '9��_���ы��z
            paramList.Add(MakeParam("@gatu_jisseki_arari9", SqlDbType.BigInt, 12, drData("uri_arari9")))        '9��_���ёe��
            paramList.Add(MakeParam("@gatu_jisseki_kensuu10", SqlDbType.BigInt, 12, drData("uri_suu10")))       '10��_���ь���
            paramList.Add(MakeParam("@gatu_jisseki_kingaku10", SqlDbType.BigInt, 12, drData("uri_gaku10")))     '10��_���ы��z
            paramList.Add(MakeParam("@gatu_jisseki_arari10", SqlDbType.BigInt, 12, drData("uri_arari10")))      '10��_���ёe��
            paramList.Add(MakeParam("@gatu_jisseki_kensuu11", SqlDbType.BigInt, 12, drData("uri_suu11")))       '11��_���ь���
            paramList.Add(MakeParam("@gatu_jisseki_kingaku11", SqlDbType.BigInt, 12, drData("uri_gaku11")))     '11��_���ы��z
            paramList.Add(MakeParam("@gatu_jisseki_arari11", SqlDbType.BigInt, 12, drData("uri_arari11")))      '11��_���ёe��
            paramList.Add(MakeParam("@gatu_jisseki_kensuu12", SqlDbType.BigInt, 12, drData("uri_suu12")))       '12��_���ь���
            paramList.Add(MakeParam("@gatu_jisseki_kingaku12", SqlDbType.BigInt, 12, drData("uri_gaku12")))     '12��_���ы��z
            paramList.Add(MakeParam("@gatu_jisseki_arari12", SqlDbType.BigInt, 12, drData("uri_arari12")))      '12��_���ёe��
            paramList.Add(MakeParam("@gatu_jisseki_kensuu1", SqlDbType.BigInt, 12, drData("uri_suu1")))         '1��_���ь���
            paramList.Add(MakeParam("@gatu_jisseki_kingaku1", SqlDbType.BigInt, 12, drData("uri_gaku1")))       '1��_���ы��z
            paramList.Add(MakeParam("@gatu_jisseki_arari1", SqlDbType.BigInt, 12, drData("uri_arari1")))        '1��_���ёe��
            paramList.Add(MakeParam("@gatu_jisseki_kensuu2", SqlDbType.BigInt, 12, drData("uri_suu2")))         '2��_���ь���
            paramList.Add(MakeParam("@gatu_jisseki_kingaku2", SqlDbType.BigInt, 12, drData("uri_gaku2")))       '2��_���ы��z
            paramList.Add(MakeParam("@gatu_jisseki_arari2", SqlDbType.BigInt, 12, drData("uri_arari2")))        '2��_���ёe��
            paramList.Add(MakeParam("@gatu_jisseki_kensuu3", SqlDbType.BigInt, 12, drData("uri_suu3")))         '3��_���ь���
            paramList.Add(MakeParam("@gatu_jisseki_kingaku3", SqlDbType.BigInt, 12, drData("uri_gaku3")))       '3��_���ы��z
            paramList.Add(MakeParam("@gatu_jisseki_arari3", SqlDbType.BigInt, 12, drData("uri_arari3")))        '3��_���ёe��
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, CON_BATCH_ID))                 '�o�^���O�C�����[�U�[ID

            Return objConnection.ExecuteNonQuery(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())
        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "���ъǗ��e�[�u���̃f�[�^�̑}���������ُ�I�����܂����B")
            End If

            Throw ex
        End Try
    End Function
#End Region
End Class

