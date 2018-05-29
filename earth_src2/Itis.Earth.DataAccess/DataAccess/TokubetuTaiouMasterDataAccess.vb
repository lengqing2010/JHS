Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

Public Class TokubetuTaiouMasterDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>���i�R�[�h���擾����</summary>
    ''' <returns>���i�R�[�h�f�[�^�e�[�u��</returns>
    ''' <history>2011/03/03�@�W���o�t(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelSyouhinCd() As Data.DataTable

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	syouhin_cd ")                       '���i�R�[�h
            .AppendLine("	,(syouhin_cd + '�F' + syouhin_mei) AS syouhin_mei ") '���i���i���i�R�[�h�F���i���j
            .AppendLine("FROM ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ")                        '���i�}�X�^
            .AppendLine("WHERE ")
            .AppendLine("	torikesi = 0 ")                     '���
            .AppendLine("	AND ")
            .AppendLine("	souko_cd = '100' ")                 '�q�ɃR�[�h
        End With

        'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd")

        '�߂�f�[�^�e�[�u��
        Return dsReturn.Tables(0)

    End Function

    ''' <summary>�������@���擾����</summary>
    ''' <returns>�������@�f�[�^�e�[�u��</returns>
    ''' <history>2011/03/03�@�W���o�t(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTyousaHouhou() As Data.DataTable

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	tys_houhou_no ")                                                            '�������@NO
            .AppendLine("	,LTRIM(STR(tys_houhou_no) + '�F' + tys_houhou_mei) AS tys_houhou_mei ")     '�������@����
            .AppendLine("FROM ")
            .AppendLine("	m_tyousahouhou WITH(READCOMMITTED) ")                                       '�������@�}�X�^
        End With

        'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTyousahouhou")

        '�߂�f�[�^�e�[�u��
        Return dsReturn.Tables(0)

    End Function

    '''' <summary>�����X���i�������@���ʑΉ��}�X�^�����擾</summary>
    'Public Function SelTokubetuTaiouJyouhou(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable

    '    '�߂�f�[�^�Z�b�g
    '    Dim dsReturn As New Data.DataSet

    '    'SQL�R�����g
    '    Dim commandTextSb As New StringBuilder

    '    '�p�����[�^�i�[
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL��
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        If dtParamList.Item("kensuu").ToString <> "max" Then
    '            .AppendLine("   TOP " & dtParamList.Item("kensuu").ToString & "")
    '        End If
    '        .AppendLine("	MKSTTT.kameiten_cd ")   '--�����X�R�[�h
    '        .AppendLine("	,MK.kameiten_mei1 ")     '--�����X����
    '        .AppendLine("	,MKSTTT.syouhin_cd ")    '--���i�R�[�h
    '        .AppendLine("	,MS.syouhin_mei ")       '--���i����
    '        .AppendLine("	,(LTRIM(STR(MKSTTT.tys_houhou_no)) + '�F' +MT.tys_houhou_mei) AS tys_houhou ") '--�������@(�������@NO:�������@����)
    '        .AppendLine("	,MKSTTT.tys_houhou_no ")    '--�������@�R�[�h
    '        .AppendLine("	,MKSTTT.tokubetu_taiou_cd ")     '--���ʑΉ��R�[�h
    '        .AppendLine("	,MTT.tokubetu_taiou_meisyou ")   '--���ʑΉ�����
    '        .AppendLine("	,CASE ")
    '        .AppendLine("		WHEN MKSTTT.torikesi = 0 THEN ")
    '        .AppendLine("			'' ")
    '        .AppendLine("		ELSE ")
    '        .AppendLine("			'���' ")
    '        .AppendLine("		END AS torikesi ")  '--���
    '        .AppendLine("FROM")
    '        .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH (READCOMMITTED) ")    '--�����X���i�������@���ʑΉ��}�X�^
    '        .AppendLine("	LEFT JOIN m_kameiten AS MK WITH (READCOMMITTED) ")      '--�����X�}�X�^
    '        .AppendLine("	ON MKSTTT.kameiten_cd = MK.kameiten_cd ")       '--�����X�R�[�h
    '        .AppendLine("	LEFT JOIN m_syouhin AS MS WITH (READCOMMITTED) ")       '--���i�}�X�^
    '        .AppendLine("	ON MKSTTT.syouhin_cd =	MS.syouhin_cd ")        '--���i�R�[�h
    '        .AppendLine("	AND MS.souko_cd = '100' ")      '--�q�ɃR�[�h
    '        .AppendLine("	AND MS.torikesi = 0 ")        '--���
    '        .AppendLine("	LEFT JOIN m_tyousahouhou AS MT WITH (READCOMMITTED) ")      '--�������@�}�X�^
    '        .AppendLine("	ON MKSTTT.tys_houhou_no = MT.tys_houhou_no ")       '--�������@NO
    '        .AppendLine("	LEFT JOIN m_tokubetu_taiou AS MTT WITH (READCOMMITTED) ")       '--���ʑΉ��}�X�^
    '        .AppendLine("	ON MKSTTT.tokubetu_taiou_cd = MTT.tokubetu_taiou_cd ")      '--���ʑΉ��R�[�h
    '        .AppendLine("WHERE  (1=1) ")
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd BETWEEN @KameitenCdFrom AND @KameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString = String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd = @KameitenCdFrom ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString = String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd = @KameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("syouhin_cd").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.syouhin_cd = @syouhin_cd ")
    '            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
    '        End If
    '        If dtParamList.Item("tys_houhou_no").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.tys_houhou_no = @tys_houhou_no ")
    '            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
    '        End If
    '        If dtParamList.Item("tokubetu_taiou_cd").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ")
    '            paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
    '        End If
    '        If dtParamList.Item("torikesiFlg").ToString = "1" Then
    '            .AppendLine("   AND MKSTTT.torikesi = 0 ")
    '        End If
    '        .AppendLine("ORDER BY")
    '        .AppendLine("   MKSTTT.kameiten_cd ")
    '        .AppendLine("   ,MKSTTT.syouhin_cd ")
    '        .AppendLine("   ,MKSTTT.tys_houhou_no  ")
    '    End With

    '    'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhou", paramList.ToArray)

    '    '�߂�f�[�^�e�[�u��
    '    Return dsReturn.Tables(0)

    'End Function

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^�����擾</summary>
    Public Function SelTokubetuTaiouJyouhou(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            If dtParamList.Item("kensuu").ToString <> "max" Then
                .AppendLine("   TOP " & dtParamList.Item("kensuu").ToString & "")
            End If
            .AppendLine("	MKSTTT.aitesaki_syubetu ")              '�������
            .AppendLine("	,(LTRIM(STR(MKSTTT.aitesaki_syubetu)) + '�F' +MKM.meisyou) AS aitesaki_syubetu_layout ") '�������(�������:�g������)
            .AppendLine("	,MKSTTT.aitesaki_cd ")                  '�����R�[�h
            .AppendLine("	,MKM.meisyou ")                         '�g������
            .AppendLine("	,SUB.aitesaki_name ")                   '����於
            .AppendLine("	,MKSTTT.syouhin_cd ")                   '���i�R�[�h
            .AppendLine("	,MS.syouhin_mei ")                      '���i����
            .AppendLine("	,MKSTTT.tys_houhou_no ")                '�������@NO
            .AppendLine("	,MT.tys_houhou_mei ")                   '�������@����
            .AppendLine("	,(LTRIM(STR(MKSTTT.tys_houhou_no)) + '�F' +MT.tys_houhou_mei) AS tys_houhou ") '�������@(�������@NO:�������@����)
            .AppendLine("	,MKSTTT.tokubetu_taiou_cd ")            '���ʑΉ��R�[�h
            .AppendLine("	,MTT.tokubetu_taiou_meisyou ")          '���ʑΉ�����
            .AppendLine("	,MKSTTT.torikesi ")                     '���
            .AppendLine("	,MKSTTT.kasan_syouhin_cd ")             '���z���Z���i�R�[�h
            .AppendLine("	,MKSTTT.syokiti ")                      '�����l
            .AppendLine("	,MKSTTT.uri_kasan_gaku ")               '���������Z���z
            .AppendLine("	,MKSTTT.koumuten_kasan_gaku ")          '�H���X�������Z���z
            .AppendLine("FROM")
            .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH (READCOMMITTED) ")    '�����X���i�������@���ʑΉ��}�X�^
            .AppendLine("LEFT JOIN (")
            .AppendLine("	SELECT ")
            .AppendLine("	    '0' AS aitesaki_syubetu, ")
            .AppendLine("	    'ALL' AS aitesaki_cd, ")
            .AppendLine("	    '�����Ȃ�' AS aitesaki_name, ")
            .AppendLine("	    '0' AS torikesi ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '1' AS aitesaki_syubetu, ")
            .AppendLine("	    MK.kameiten_cd AS aitesaki_cd, ")
            .AppendLine("	    MK.kameiten_mei1 AS aitesaki_name, ")
            .AppendLine("	    MK.torikesi AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_kameiten AS MK WITH (READCOMMITTED) ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '5' AS aitesaki_syubetu, ")
            .AppendLine("	    ME.eigyousyo_cd AS aitesaki_cd, ")
            .AppendLine("	    ME.eigyousyo_mei AS aitesaki_name, ")
            .AppendLine("	    ME.torikesi AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_eigyousyo AS ME WITH (READCOMMITTED) ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '7' AS aitesaki_syubetu, ")
            .AppendLine("	    MKR.keiretu_cd AS aitesaki_cd, ")
            .AppendLine("	    MIN(MKR.keiretu_mei) AS aitesaki_name, ")
            .AppendLine("	    MIN(MKR.torikesi) AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_keiretu AS MKR WITH (READCOMMITTED) ")
            .AppendLine("	GROUP BY MKR.keiretu_cd ")
            .AppendLine(") AS SUB")
            .AppendLine("ON")
            .AppendLine("	MKSTTT.aitesaki_syubetu = SUB.aitesaki_syubetu")
            .AppendLine("	AND MKSTTT.aitesaki_cd = SUB.aitesaki_cd")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MKSTTT.aitesaki_syubetu = MKM.code")
            .AppendLine("	AND MKM.meisyou_syubetu = '23'")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MS.syouhin_cd = MKSTTT.syouhin_cd")
            .AppendLine("	AND MS.souko_cd = '100'")
            .AppendLine("	AND MS.torikesi = '0'")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tyousahouhou AS MT WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MT.tys_houhou_no = MKSTTT.tys_houhou_no")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tokubetu_taiou AS MTT WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MTT.tokubetu_taiou_cd = MKSTTT.tokubetu_taiou_cd")
            .AppendLine("WHERE  (1=1) ")
            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_syubetu = @aitesaki_syubetu")
                paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 5, dtParamList.Item("aitesakiSyubetu").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd = @aitesakiCdFrom ")
                paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
            End If

            If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd = @aitesakiCdTo ")
                paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("syouhin_cd").ToString) Then
                .AppendLine("   AND MKSTTT.syouhin_cd = @syouhin_cd ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("tys_houhou_no").ToString) Then
                .AppendLine("   AND MKSTTT.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("tokubetu_taiou_cd").ToString) Then
                .AppendLine("   AND MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ")
                paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
            End If

            If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.torikesi = 0 ")
            End If

            '============2012/05/21 �ԗ� 407553�̑Ή� �ǉ���===============================
            '��������͑ΏۊO=�`�F�b�N�̏ꍇ
            If dtParamList.Item("aitesakiTorikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND SUB.torikesi = 0 ")
            End If

            '\0�͑ΏۊO=�`�F�b�N�̏ꍇ
            If dtParamList.Item("kingaku0TorikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.uri_kasan_gaku <> 0 ")
            End If
            '============2012/05/21 �ԗ� 407553�̑Ή� �ǉ���===============================

            '---------------------------from 2013.03.09���F�ǉ�-----------------------------
            '�����l1�̂�=�`�F�b�N�̏ꍇ�A�T�u�����X���i�������@���ʑΉ�M.�����l�@=�@"1"
            If dtParamList.Item("Syokiti1Nomi").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.syokiti = '1' ")
            End If
            '---------------------------to   2013.03.09���F�ǉ�-----------------------------

            .AppendLine("ORDER BY")
            .AppendLine("   MKSTTT.aitesaki_syubetu ")
            .AppendLine("   ,MKSTTT.aitesaki_cd ")
            .AppendLine("   ,MKSTTT.syouhin_cd ")
            .AppendLine("   ,MKSTTT.tys_houhou_no  ")
            .AppendLine("   ,MKSTTT.tokubetu_taiou_cd  ")
        End With

        'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhou", paramList.ToArray)

        '�߂�f�[�^�e�[�u��
        Return dsReturn.Tables(0)

    End Function

    ''' <summary>������ʏ����擾</summary>
    Public Function SelAitesakiSyubetuList() As Data.DataTable
        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        Dim strSql As String = "SELECT code+':'+meisyou AS name, code AS value FROM m_kakutyou_meisyou WHERE meisyou_syubetu = '23'"

        'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
        FillDataset(connStr, CommandType.Text, strSql, dsReturn, "AitesakiSyubetuList")

        '�߂�f�[�^�e�[�u��
        Return dsReturn.Tables(0)
    End Function

    '''' <summary>�����X���i�������@���ʑΉ��}�X�^�S�������������擾</summary>
    'Public Function SelTokubetuTaiouCount(ByVal dtParamList As Dictionary(Of String, String)) As Integer

    '    '�߂�f�[�^�Z�b�g
    '    Dim dsReturn As New Data.DataSet

    '    'SQL�R�����g
    '    Dim commandTextSb As New StringBuilder

    '    '�p�����[�^�i�[
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL��
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        .AppendLine("	COUNT(*) AS kameiten_cd ")   '--�S�������X���R�[�h��
    '        .AppendLine("FROM")
    '        .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH (READCOMMITTED) ")    '--�����X���i�������@���ʑΉ��}�X�^
    '        .AppendLine("	LEFT JOIN m_kameiten AS MK WITH (READCOMMITTED) ")      '--�����X�}�X�^
    '        .AppendLine("	ON MKSTTT.kameiten_cd = MK.kameiten_cd ")       '--�����X�R�[�h
    '        .AppendLine("	LEFT JOIN m_syouhin AS MS WITH (READCOMMITTED) ")       '--���i�}�X�^
    '        .AppendLine("	ON MKSTTT.syouhin_cd =	MS.syouhin_cd ")        '--���i�R�[�h
    '        .AppendLine("	AND MS.souko_cd = '100' ")      '--�q�ɃR�[�h
    '        .AppendLine("	AND MS.torikesi = 0 ")        '--���
    '        .AppendLine("	LEFT JOIN m_tyousahouhou AS MT WITH (READCOMMITTED) ")      '--�������@�}�X�^
    '        .AppendLine("	ON MKSTTT.tys_houhou_no = MT.tys_houhou_no ")       '--�������@NO
    '        .AppendLine("	LEFT JOIN m_tokubetu_taiou AS MTT WITH (READCOMMITTED) ")       '--���ʑΉ��}�X�^
    '        .AppendLine("	ON MKSTTT.tokubetu_taiou_cd = MTT.tokubetu_taiou_cd ")      '--���ʑΉ��R�[�h
    '        .AppendLine("WHERE  (1=1) ")
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd BETWEEN @KameitenCdFrom AND @KameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString = String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd = @KameitenCdFrom ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString = String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd = @KameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("syouhin_cd").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.syouhin_cd = @syouhin_cd ")
    '            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
    '        End If
    '        If dtParamList.Item("tys_houhou_no").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.tys_houhou_no = @tys_houhou_no ")
    '            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
    '        End If
    '        If dtParamList.Item("tokubetu_taiou_cd").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ")
    '            paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
    '        End If
    '        If dtParamList.Item("torikesiFlg").ToString = "1" Then
    '            .AppendLine("   AND MKSTTT.torikesi = 0 ")
    '        End If
    '    End With

    '    'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "KensakuKensuu", paramList.ToArray)

    '    '�߂�f�[�^
    '    Return dsReturn.Tables(0).Rows(0).Item(0)

    'End Function

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^�S�������������擾</summary>
    Public Function SelTokubetuTaiouCount(ByVal dtParamList As Dictionary(Of String, String)) As Integer

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(*) AS kameiten_cd ")                                              '�S�����R�[�h��
            .AppendLine("FROM")
            .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH (READCOMMITTED) ")    '�����X���i�������@���ʑΉ��}�X�^
            .AppendLine("LEFT JOIN (")
            .AppendLine("	SELECT ")
            .AppendLine("	    '0' AS aitesaki_syubetu, ")
            .AppendLine("	    'ALL' AS aitesaki_cd, ")
            .AppendLine("	    '�����Ȃ�' AS aitesaki_name, ")
            .AppendLine("	    '0' AS torikesi ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '1' AS aitesaki_syubetu, ")
            .AppendLine("	    MK.kameiten_cd AS aitesaki_cd, ")
            .AppendLine("	    MK.kameiten_mei1 AS aitesaki_name, ")
            .AppendLine("	    MK.torikesi AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_kameiten AS MK WITH (READCOMMITTED) ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '5' AS aitesaki_syubetu, ")
            .AppendLine("	    ME.eigyousyo_cd AS aitesaki_cd, ")
            .AppendLine("	    ME.eigyousyo_mei AS aitesaki_name, ")
            .AppendLine("	    ME.torikesi AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_eigyousyo AS ME WITH (READCOMMITTED) ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '7' AS aitesaki_syubetu, ")
            .AppendLine("	    MKR.keiretu_cd AS aitesaki_cd, ")
            .AppendLine("	    MIN(MKR.keiretu_mei) AS aitesaki_name, ")
            .AppendLine("	    MIN(MKR.torikesi) AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_keiretu AS MKR WITH (READCOMMITTED) ")
            .AppendLine("	GROUP BY MKR.keiretu_cd ")
            .AppendLine(") AS SUB")
            .AppendLine("ON")
            .AppendLine("	MKSTTT.aitesaki_syubetu = SUB.aitesaki_syubetu")
            .AppendLine("	AND MKSTTT.aitesaki_cd = SUB.aitesaki_cd")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MKSTTT.aitesaki_syubetu = MKM.code")
            .AppendLine("	AND MKM.meisyou_syubetu = '23'")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MS.syouhin_cd = MKSTTT.syouhin_cd")
            .AppendLine("	AND MS.souko_cd = '100'")
            .AppendLine("	AND MS.torikesi = '0'")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tyousahouhou AS MT WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MT.tys_houhou_no = MKSTTT.tys_houhou_no")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tokubetu_taiou AS MTT WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MTT.tokubetu_taiou_cd = MKSTTT.tokubetu_taiou_cd")
            .AppendLine("WHERE  (1=1) ")
            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_syubetu = @aitesaki_syubetu")
                paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 5, dtParamList.Item("aitesakiSyubetu").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd = @aitesakiCdFrom ")
                paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
            End If

            If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd = @aitesakiCdTo ")
                paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("syouhin_cd").ToString) Then
                .AppendLine("   AND MKSTTT.syouhin_cd = @syouhin_cd ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("tys_houhou_no").ToString) Then
                .AppendLine("   AND MKSTTT.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("tokubetu_taiou_cd").ToString) Then
                .AppendLine("   AND MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ")
                paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
            End If

            If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.torikesi = 0 ")
            End If

            '============2012/05/21 �ԗ� 407553�̑Ή� �ǉ���===============================
            '��������͑ΏۊO=�`�F�b�N�̏ꍇ
            If dtParamList.Item("aitesakiTorikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND SUB.torikesi = 0 ")
            End If

            '\0�͑ΏۊO=�`�F�b�N�̏ꍇ
            If dtParamList.Item("kingaku0TorikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.uri_kasan_gaku <> 0 ")
            End If
            '============2012/05/21 �ԗ� 407553�̑Ή� �ǉ���===============================

            '---------------------------from 2013.03.09���F�ǉ�-----------------------------
            '�����l1�̂�=�`�F�b�N�̏ꍇ�A�T�u�����X���i�������@���ʑΉ�M.�����l�@=�@"1"
            If dtParamList.Item("Syokiti1Nomi").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.syokiti = '1' ")
            End If
            '---------------------------to   2013.03.09���F�ǉ�-----------------------------

        End With

        'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "KensakuKensuu", paramList.ToArray)

        '�߂�f�[�^
        Return dsReturn.Tables(0).Rows(0).Item(0)

    End Function

    '''' <summary>�����X���i�������@���ʑΉ��}�X�^CSV�����擾</summary>
    'Public Function SelTokubetuTaiouJyouhouCSV(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable

    '    '�߂�f�[�^�Z�b�g
    '    Dim dsReturn As New Data.DataSet

    '    'SQL�R�����g
    '    Dim commandTextSb As New StringBuilder

    '    '�p�����[�^�i�[
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL��
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        'EDI���쐬��
    '        .AppendLine("   RIGHT(CONVERT(varchar(10),GETDATE(),112),4)+REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date ")
    '        .AppendLine("	,ISNULL(MKSTTT.kameiten_cd,'') ")   '--�����X�R�[�h
    '        .AppendLine("	,ISNULL(MK.kameiten_mei1,'') ")     '--�����X����
    '        .AppendLine("	,ISNULL(MKSTTT.syouhin_cd,'') ")    '--���i�R�[�h
    '        .AppendLine("	,ISNULL(MS.syouhin_mei,'') ")       '--���i����
    '        .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.tys_houhou_no),'') ") '--�������@NO
    '        .AppendLine("	,ISNULL(MT.tys_houhou_mei,'') ")    '--�������@����
    '        .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.tokubetu_taiou_cd),'') ")     '--���ʑΉ��R�[�h
    '        .AppendLine("	,ISNULL(MTT.tokubetu_taiou_meisyou,'') ")   '--���ʑΉ�����
    '        .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.torikesi),'') ")      '--���
    '        .AppendLine("FROM")
    '        .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH (READCOMMITTED) ")    '--�����X���i�������@���ʑΉ��}�X�^
    '        .AppendLine("	LEFT JOIN m_kameiten AS MK WITH (READCOMMITTED) ")      '--�����X�}�X�^
    '        .AppendLine("	ON MKSTTT.kameiten_cd = MK.kameiten_cd ")       '--�����X�R�[�h
    '        .AppendLine("	LEFT JOIN m_syouhin AS MS WITH (READCOMMITTED) ")       '--���i�}�X�^
    '        .AppendLine("	ON MKSTTT.syouhin_cd =	MS.syouhin_cd ")        '--���i�R�[�h
    '        .AppendLine("	AND MS.souko_cd = '100' ")      '--�q�ɃR�[�h
    '        .AppendLine("	AND MS.torikesi = 0 ")        '--���
    '        .AppendLine("	LEFT JOIN m_tyousahouhou AS MT WITH (READCOMMITTED) ")      '--�������@�}�X�^
    '        .AppendLine("	ON MKSTTT.tys_houhou_no = MT.tys_houhou_no ")       '--�������@NO
    '        .AppendLine("	LEFT JOIN m_tokubetu_taiou AS MTT WITH (READCOMMITTED) ")       '--���ʑΉ��}�X�^
    '        .AppendLine("	ON MKSTTT.tokubetu_taiou_cd = MTT.tokubetu_taiou_cd ")      '--���ʑΉ��R�[�h
    '        .AppendLine("WHERE  (1=1) ")
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd BETWEEN @KameitenCdFrom AND @KameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString = String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd = @KameitenCdFrom ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString = String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.kameiten_cd = @KameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("syouhin_cd").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.syouhin_cd = @syouhin_cd ")
    '            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
    '        End If
    '        If dtParamList.Item("tys_houhou_no").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.tys_houhou_no = @tys_houhou_no ")
    '            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
    '        End If
    '        If dtParamList.Item("tokubetu_taiou_cd").ToString <> String.Empty Then
    '            .AppendLine("   AND MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ")
    '            paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
    '        End If
    '        If dtParamList.Item("torikesiFlg").ToString = "1" Then
    '            .AppendLine("   AND MKSTTT.torikesi = 0 ")
    '        End If
    '        .AppendLine("ORDER BY")
    '        .AppendLine("   MKSTTT.kameiten_cd ")
    '        .AppendLine("   ,MKSTTT.syouhin_cd ")
    '        .AppendLine("   ,MKSTTT.tys_houhou_no  ")
    '        .AppendLine("   ,MKSTTT.tokubetu_taiou_cd")
    '    End With

    '    'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhouCSV", paramList.ToArray)

    '    '�߂�f�[�^�e�[�u��
    '    Return dsReturn.Tables(0)

    'End Function

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^CSV�����擾</summary>
    Public Function SelTokubetuTaiouJyouhouCSV(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            'EDI���쐬��
            .AppendLine("   RIGHT(CONVERT(varchar(10),GETDATE(),112),4)+REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date ")
            .AppendLine("	,ISNULL(MKSTTT.aitesaki_syubetu,'') ")   '--�������
            .AppendLine("	,ISNULL(MKSTTT.aitesaki_cd,'') ")   '--�����R�[�h
            .AppendLine("	,ISNULL(SUB.aitesaki_name,'') ")     '--����於��
            .AppendLine("	,ISNULL(MKSTTT.syouhin_cd,'') ")    '--���i�R�[�h
            .AppendLine("	,ISNULL(MS.syouhin_mei,'') ")       '--���i����
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.tys_houhou_no),'') ") '--�������@NO
            .AppendLine("	,ISNULL(MT.tys_houhou_mei,'') ")    '--�������@����
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.tokubetu_taiou_cd),'') ")     '--���ʑΉ��R�[�h
            .AppendLine("	,ISNULL(MTT.tokubetu_taiou_meisyou,'') ")   '--���ʑΉ�����
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.torikesi),'') ")      '--���
            .AppendLine("	,ISNULL(MKSTTT.kasan_syouhin_cd,'') ")                      '--���z���Z���i�R�[�h
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.syokiti),'') ")          '--�����l
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.uri_kasan_gaku),'') ")      '--���������Z���z
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.koumuten_kasan_gaku),'') ")      '--�H���X�������Z���z
            .AppendLine("FROM")
            .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH (READCOMMITTED) ")    '�����X���i�������@���ʑΉ��}�X�^
            .AppendLine("LEFT JOIN (")
            .AppendLine("	SELECT ")
            .AppendLine("	    '0' AS aitesaki_syubetu, ")
            .AppendLine("	    'ALL' AS aitesaki_cd, ")
            .AppendLine("	    '�����Ȃ�' AS aitesaki_name, ")
            .AppendLine("	    '0' AS torikesi ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '1' AS aitesaki_syubetu, ")
            .AppendLine("	    MK.kameiten_cd AS aitesaki_cd, ")
            .AppendLine("	    MK.kameiten_mei1 AS aitesaki_name, ")
            .AppendLine("	    MK.torikesi AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_kameiten AS MK WITH (READCOMMITTED) ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '5' AS aitesaki_syubetu, ")
            .AppendLine("	    ME.eigyousyo_cd AS aitesaki_cd, ")
            .AppendLine("	    ME.eigyousyo_mei AS aitesaki_name, ")
            .AppendLine("	    ME.torikesi AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_eigyousyo AS ME WITH (READCOMMITTED) ")
            .AppendLine("	UNION ALL ")
            .AppendLine("	SELECT ")
            .AppendLine("	    '7' AS aitesaki_syubetu, ")
            .AppendLine("	    MKR.keiretu_cd AS aitesaki_cd, ")
            .AppendLine("	    MIN(MKR.keiretu_mei) AS aitesaki_name, ")
            .AppendLine("	    MIN(MKR.torikesi) AS torikesi ")
            .AppendLine("	FROM ")
            .AppendLine("	    m_keiretu AS MKR WITH (READCOMMITTED) ")
            .AppendLine("	GROUP BY MKR.keiretu_cd ")
            .AppendLine(") AS SUB")
            .AppendLine("ON")
            .AppendLine("	MKSTTT.aitesaki_syubetu = SUB.aitesaki_syubetu")
            .AppendLine("	AND MKSTTT.aitesaki_cd = SUB.aitesaki_cd")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MKSTTT.aitesaki_syubetu = MKM.code")
            .AppendLine("	AND MKM.meisyou_syubetu = '23'")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MS.syouhin_cd = MKSTTT.syouhin_cd")
            .AppendLine("	AND MS.souko_cd = '100'")
            .AppendLine("	AND MS.torikesi = '0'")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tyousahouhou AS MT WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MT.tys_houhou_no = MKSTTT.tys_houhou_no")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tokubetu_taiou AS MTT WITH (READCOMMITTED)")
            .AppendLine("ON")
            .AppendLine("	MTT.tokubetu_taiou_cd = MKSTTT.tokubetu_taiou_cd")
            .AppendLine("WHERE  (1=1) ")
            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_syubetu = @aitesaki_syubetu")
                paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 5, dtParamList.Item("aitesakiSyubetu").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd = @aitesakiCdFrom ")
                paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
            End If

            If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                .AppendLine("   AND MKSTTT.aitesaki_cd = @aitesakiCdTo ")
                paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("syouhin_cd").ToString) Then
                .AppendLine("   AND MKSTTT.syouhin_cd = @syouhin_cd ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("tys_houhou_no").ToString) Then
                .AppendLine("   AND MKSTTT.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
            End If

            If Not String.IsNullOrEmpty(dtParamList.Item("tokubetu_taiou_cd").ToString) Then
                .AppendLine("   AND MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ")
                paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
            End If

            If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.torikesi = 0 ")
            End If
            '============2012/05/21 �ԗ� 407553�̑Ή� �ǉ���===============================
            '��������͑ΏۊO=�`�F�b�N�̏ꍇ
            If dtParamList.Item("aitesakiTorikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND SUB.torikesi = 0 ")
            End If

            '\0�͑ΏۊO=�`�F�b�N�̏ꍇ
            If dtParamList.Item("kingaku0TorikesiFlg").ToString.Equals("1") Then
                .AppendLine("   AND MKSTTT.uri_kasan_gaku <> 0 ")
            End If
            '============2012/05/21 �ԗ� 407553�̑Ή� �ǉ���===============================

            '------------------From 2013.03.09  ���F�ǉ�����-----------------
            '�����l1�̂�=�`�F�b�N�̏ꍇ�A�����X���i�������@���ʑΉ�M.�����l�@=�@"1"
            If dtParamList.Item("Syokiti1Nomi").ToString.Equals("1") Then
                .AppendLine("	AND ")
                .AppendLine("	MKSTTT.syokiti = '1'")
            End If
            '------------------To   2013.03.09  ���F�ǉ�����-----------------

            .AppendLine("ORDER BY")
            .AppendLine("   MKSTTT.aitesaki_syubetu ")
            .AppendLine("   ,MKSTTT.aitesaki_cd ")
            .AppendLine("   ,MKSTTT.syouhin_cd ")
            .AppendLine("   ,MKSTTT.tys_houhou_no  ")
            .AppendLine("   ,MKSTTT.tokubetu_taiou_cd  ")
        End With

        'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhouCSV", paramList.ToArray)

        '�߂�f�[�^�e�[�u��
        Return dsReturn.Tables(0)

    End Function


    '''' <summary>���ݒ���܂މ����X���i�������@���ʑΉ��}�X�^CSV�f�[�^���擾</summary>
    '''' <returns>���ݒ���܂މ����X���i�������@���ʑΉ��}�X�^CSV�e�[�u��</returns>
    'Public Function SelTokubetuTaiouCSV(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable
    '    '�߂�f�[�^�Z�b�g
    '    Dim dsReturn As New Data.DataSet

    '    'SQL�R�����g
    '    Dim commandTextSb As New StringBuilder

    '    '�p�����[�^�i�[
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL��
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        'EDI���쐬��
    '        .AppendLine("   RIGHT(CONVERT(varchar(10),GETDATE(),112),4)+REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date ")
    '        .AppendLine(" 	,ISNULL(SKM.kameiten_cd,'') ") '�����X�R�[�h
    '        .AppendLine(" 	,ISNULL(SKM.kameiten_mei1,'') ")   '�����X����
    '        .AppendLine(" 	,ISNULL(SMS.syouhin_cd,'') ")  '���i�R�[�h
    '        .AppendLine(" 	,ISNULL(SMS.syouhin_mei,'') ") '���i��
    '        .AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),SMT.tys_houhou_no),'') ")   '�������@NO
    '        .AppendLine(" 	,ISNULL(SMT.tys_houhou_mei,'') ")  '�������@��
    '        .AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),SMTT.tokubetu_taiou_cd),'') ")  '���ʑΉ��R�[�h
    '        .AppendLine(" 	,ISNULL(SMTT.tokubetu_taiou_meisyou,'') ") '���ʑΉ�����
    '        .AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.torikesi),'') ") '���
    '        .AppendLine(" FROM ")
    '        .AppendLine(" 	(SELECT  ")
    '        .AppendLine(" 		tys_houhou_no ")
    '        .AppendLine(" 		,tys_houhou_mei ")
    '        .AppendLine(" 	 FROM ")
    '        .AppendLine(" 	  	m_tyousahouhou WITH(READCOMMITTED) ")   '�������@�}�X�^
    '        .AppendLine(" 	 WHERE ")
    '        .AppendLine(" 	  	torikesi = 0 ")
    '        If dtParamList.Item("tys_houhou_no").ToString <> String.Empty Then
    '            .AppendLine(" 	AND tys_houhou_no = @tys_houhou_no  ")
    '            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
    '        End If
    '        .AppendLine(" 	  )AS SMT ")    '�T�u�������@�}�X�^
    '        .AppendLine(" CROSS JOIN ")
    '        .AppendLine("  	 (SELECT ")
    '        .AppendLine("  	  	 syouhin_cd ")
    '        .AppendLine("  	  	 ,syouhin_mei ")
    '        .AppendLine("  	  FROM ")
    '        .AppendLine("  	  	  m_syouhin WITH(READCOMMITTED) ")  '���i�}�X�^
    '        .AppendLine(" 	  WHERE ")
    '        .AppendLine("  	  	  souko_cd = '100' AND torikesi = 0  ")
    '        If dtParamList.Item("syouhin_cd").ToString <> String.Empty Then
    '            .AppendLine(" 	  AND syouhin_cd =@syouhin_cd  ")
    '            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
    '        End If
    '        .AppendLine("  	  )AS SMS ")    '�T�u���i�}�X�^
    '        .AppendLine(" CROSS JOIN ")
    '        .AppendLine(" 	  (SELECT ")
    '        .AppendLine("  	  	  tokubetu_taiou_cd ")
    '        .AppendLine("  	  	  ,tokubetu_taiou_meisyou ")
    '        .AppendLine("  	   FROM ")
    '        .AppendLine("  	   	  m_tokubetu_taiou WITH(READCOMMITTED) ")   '���ʑΉ��}�X�^
    '        .AppendLine("  	   WHERE ")
    '        .AppendLine(" 	      torikesi = 0  ")
    '        If dtParamList.Item("tokubetu_taiou_cd").ToString <> String.Empty Then
    '            .AppendLine("  	  AND tokubetu_taiou_cd = @tokubetu_taiou_cd ")
    '            paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
    '        End If
    '        .AppendLine("  	   )AS SMTT ")  '�T�u���ʑΉ��}�X�^
    '        .AppendLine(" CROSS JOIN ")
    '        .AppendLine("  	   (SELECT ")
    '        .AppendLine("  	       kameiten_cd ")
    '        .AppendLine("  	       ,kameiten_mei1 ")
    '        .AppendLine("  	    FROM ")
    '        .AppendLine(" 	       m_kameiten WITH(READCOMMITTED) ")    '�����X�}�X�^
    '        .AppendLine("       WHERE(1=1) ")
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND kameiten_cd BETWEEN @kameitenCdFrom AND @kameitenCdTo  ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString = String.Empty Then
    '            .AppendLine("   AND kameiten_cd = @kameitenCdFrom ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString = String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND kameiten_cd = @kameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        .AppendLine("  	   	)AS SKM ")  '�T�u�����X�}�X�^
    '        .AppendLine("  LEFT JOIN ")
    '        .AppendLine("  	    m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ")
    '        .AppendLine("  ON  SMS.syouhin_cd = MKSTTT.syouhin_cd ")
    '        .AppendLine("  AND SMT.tys_houhou_no = MKSTTT.tys_houhou_no ")
    '        .AppendLine("  AND SMTT.tokubetu_taiou_cd = MKSTTT.tokubetu_taiou_cd ")
    '        .AppendLine("  AND SKM.kameiten_cd = MKSTTT.kameiten_cd ")
    '        .AppendLine(" ORDER BY ")
    '        .AppendLine(" 	 SKM.kameiten_cd ")
    '        .AppendLine(" 	,SMS.syouhin_cd ")
    '        .AppendLine(" 	,SMT.tys_houhou_no ")
    '    End With

    '    'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouCSV", paramList.ToArray)

    '    '�߂�f�[�^�e�[�u��
    '    Return dsReturn.Tables(0)

    'End Function

    ''' <summary>���ݒ���܂މ����X���i�������@���ʑΉ��}�X�^CSV�f�[�^���擾</summary>
    ''' <returns>���ݒ���܂މ����X���i�������@���ʑΉ��}�X�^CSV�e�[�u��</returns>
    Public Function SelTokubetuTaiouCSV(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable
        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            'EDI���쐬��
            .AppendLine("   RIGHT(CONVERT(varchar(10),GETDATE(),112),4)+REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date ")
            .AppendLine(" 	,ISNULL(SUB.aitesaki_syubetu,'') ")                             '�������
            .AppendLine(" 	,ISNULL(SUB.aitesaki_cd,'') ")                                  '�����R�[�h
            .AppendLine(" 	,ISNULL(SUB.aitesaki_name,'') ")                                '����於��
            .AppendLine(" 	,ISNULL(SMS.syouhin_cd,'') ")                                   '���i�R�[�h
            .AppendLine(" 	,ISNULL(SMS.syouhin_mei,'') ")                                  '���i��
            .AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),SMT.tys_houhou_no),'') ")           '�������@NO
            .AppendLine(" 	,ISNULL(SMT.tys_houhou_mei,'') ")                               '�������@��
            .AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),SMTT.tokubetu_taiou_cd),'') ")      '���ʑΉ��R�[�h
            .AppendLine(" 	,ISNULL(SMTT.tokubetu_taiou_meisyou,'') ")                      '���ʑΉ�����
            .AppendLine(" 	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.torikesi),'') ")             '���
            .AppendLine("	,ISNULL(MKSTTT.kasan_syouhin_cd,'') ")                          '���z���Z���i�R�[�h
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.syokiti),'') ")              '�����l
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.uri_kasan_gaku),'') ")       '���������Z���z
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MKSTTT.koumuten_kasan_gaku),'') ")  '�H���X�������Z���z
            .AppendLine(" FROM ")
            .AppendLine(" 	(SELECT  ")
            .AppendLine(" 		tys_houhou_no ")
            .AppendLine(" 		,tys_houhou_mei ")
            .AppendLine(" 	 FROM ")
            .AppendLine(" 	  	m_tyousahouhou WITH(READCOMMITTED) ")   '�������@�}�X�^
            .AppendLine(" 	 WHERE ")
            .AppendLine(" 	  	torikesi = 0 ")
            If dtParamList.Item("tys_houhou_no").ToString <> String.Empty Then
                .AppendLine(" 	AND tys_houhou_no = @tys_houhou_no  ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
            End If
            .AppendLine(" 	  )AS SMT ")    '�T�u�������@�}�X�^
            .AppendLine(" CROSS JOIN ")
            .AppendLine("  	 (SELECT ")
            .AppendLine("  	  	 syouhin_cd ")
            .AppendLine("  	  	 ,syouhin_mei ")
            .AppendLine("  	  FROM ")
            .AppendLine("  	  	  m_syouhin WITH(READCOMMITTED) ")  '���i�}�X�^
            .AppendLine(" 	  WHERE ")
            .AppendLine("  	  	  souko_cd = '100' AND torikesi = 0  ")
            If dtParamList.Item("syouhin_cd").ToString <> String.Empty Then
                .AppendLine(" 	  AND syouhin_cd =@syouhin_cd  ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
            End If
            .AppendLine("  	  )AS SMS ")    '�T�u���i�}�X�^
            .AppendLine(" CROSS JOIN ")
            .AppendLine(" 	  (SELECT ")
            .AppendLine("  	  	  tokubetu_taiou_cd ")
            .AppendLine("  	  	  ,tokubetu_taiou_meisyou ")
            .AppendLine("  	   FROM ")
            .AppendLine("  	   	  m_tokubetu_taiou WITH(READCOMMITTED) ")   '���ʑΉ��}�X�^
            .AppendLine("  	   WHERE ")
            .AppendLine(" 	      torikesi = 0  ")
            If dtParamList.Item("tokubetu_taiou_cd").ToString <> String.Empty Then
                .AppendLine("  	  AND tokubetu_taiou_cd = @tokubetu_taiou_cd ")
                paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
            End If
            .AppendLine("  	   )AS SMTT ")  '�T�u���ʑΉ��}�X�^
            .AppendLine(" CROSS JOIN ")
            If "0".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '0' AS aitesaki_syubetu, ")
                .AppendLine("	    'ALL' AS aitesaki_cd, ")
                .AppendLine("	    '�����Ȃ�' AS aitesaki_name ")
                .AppendLine("  	   )AS SUB ") '�T�u
            End If

            If "1".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '1' AS aitesaki_syubetu, ")
                .AppendLine("	    MK.kameiten_cd AS aitesaki_cd, ")
                .AppendLine("	    MK.kameiten_mei1 AS aitesaki_name ")
                .AppendLine("	FROM ")
                .AppendLine("	    m_kameiten AS MK WITH (READCOMMITTED) ")
                .AppendLine("WHERE  (1=1) ")
                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MK.kameiten_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If

                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MK.kameiten_cd = @aitesakiCdFrom ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                End If

                If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MK.kameiten_cd = @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If
                If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                    .AppendLine("   AND MK.torikesi = 0 ")
                End If
                .AppendLine("  	   )AS SUB ") '�T�u
            End If

            If "5".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '5' AS aitesaki_syubetu, ")
                .AppendLine("	    ME.eigyousyo_cd AS aitesaki_cd, ")
                .AppendLine("	    ME.eigyousyo_mei AS aitesaki_name ")
                .AppendLine("	FROM ")
                .AppendLine("	    m_eigyousyo AS ME WITH (READCOMMITTED) ")
                .AppendLine("WHERE  (1=1) ")
                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND ME.eigyousyo_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If

                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND ME.eigyousyo_cd = @aitesakiCdFrom ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                End If

                If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND ME.eigyousyo_cd = @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If
                If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                    .AppendLine("   AND ME.torikesi = 0 ")
                End If
                .AppendLine("  	   )AS SUB ") '�T�u
            End If

            If "7".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '7' AS aitesaki_syubetu, ")
                .AppendLine("	    MKR.keiretu_cd AS aitesaki_cd, ")
                .AppendLine("	    MIN(MKR.keiretu_mei) AS aitesaki_name ")
                .AppendLine("	FROM ")
                .AppendLine("	    m_keiretu AS MKR WITH (READCOMMITTED) ")
                .AppendLine("WHERE  (1=1) ")
                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MKR.keiretu_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If

                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MKR.keiretu_cd = @aitesakiCdFrom ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                End If

                If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MKR.keiretu_cd = @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If
                If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                    .AppendLine("   AND MKR.torikesi = 0 ")
                End If
                .AppendLine("	GROUP BY MKR.keiretu_cd ")
                .AppendLine("  	   )AS SUB ") '�T�u
            End If

            .AppendLine("  LEFT JOIN ")
            .AppendLine("  	    m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ")
            .AppendLine("  ON  SMS.syouhin_cd = MKSTTT.syouhin_cd ")
            .AppendLine("  AND SMT.tys_houhou_no = MKSTTT.tys_houhou_no ")
            .AppendLine("  AND SMTT.tokubetu_taiou_cd = MKSTTT.tokubetu_taiou_cd ")
            .AppendLine("  AND SUB.aitesaki_syubetu = MKSTTT.aitesaki_syubetu ")
            .AppendLine("  AND SUB.aitesaki_cd = MKSTTT.aitesaki_cd ")
            .AppendLine(" ORDER BY ")
            .AppendLine(" 	 MKSTTT.aitesaki_syubetu ")
            .AppendLine(" 	,MKSTTT.aitesaki_cd ")
            .AppendLine(" 	,SMS.syouhin_cd ")
            .AppendLine(" 	,SMT.tys_houhou_no ")
            .AppendLine(" 	,MKSTTT.tokubetu_taiou_cd ")
        End With

        'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouCSV", paramList.ToArray)

        '�߂�f�[�^�e�[�u��
        Return dsReturn.Tables(0)

    End Function

    '''' <summary>���ݒ���܂މ����X���i�������@���ʑΉ��}�X�^CSV�f�[�^�������擾</summary>
    '''' <returns>���ݒ���܂މ����X���i�������@���ʑΉ��}�X�^CSV�f�[�^����</returns>
    'Public Function SelTokubetuTaiouCSVCount(ByVal dtParamList As Dictionary(Of String, String)) As Long

    '    '�߂�f�[�^�Z�b�g
    '    Dim dsReturn As New Data.DataSet

    '    'SQL�R�����g
    '    Dim commandTextSb As New StringBuilder

    '    '�p�����[�^�i�[
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL��
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        .AppendLine("   COUNT_BIG(*) AS CSVCount")
    '        .AppendLine("FROM")
    '        .AppendLine(" 	(SELECT  ")
    '        .AppendLine(" 		tys_houhou_no ")    '�������@NO
    '        .AppendLine(" 		,tys_houhou_mei ")  '�������@����
    '        .AppendLine(" 	 FROM ")
    '        .AppendLine(" 	  	m_tyousahouhou WITH(READCOMMITTED) ")   '�������@�}�X�^
    '        .AppendLine(" 	 WHERE ")
    '        .AppendLine(" 	  	ISNULL (genka_settei_fuyou_flg,0) = 0 ")
    '        If dtParamList.Item("tys_houhou_no").ToString <> String.Empty Then
    '            .AppendLine(" 	AND tys_houhou_no = @tys_houhou_no  ")
    '            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
    '        End If
    '        .AppendLine(" 	  )AS SMT ")    '�T�u�������@�}�X�^
    '        .AppendLine(" CROSS JOIN ")
    '        .AppendLine("  	 (SELECT ")
    '        .AppendLine("  	  	 syouhin_cd ")  '-���i�R�[�h
    '        .AppendLine("  	  	 ,syouhin_mei ")    '���i��
    '        .AppendLine("  	  FROM ")
    '        .AppendLine("  	  	  m_syouhin WITH(READCOMMITTED) ")  '���i�}�X�^
    '        .AppendLine(" 	  WHERE ")
    '        .AppendLine("  	  	  souko_cd = '100' AND torikesi = 0  ")
    '        If dtParamList.Item("syouhin_cd").ToString <> String.Empty Then
    '            .AppendLine(" 	  AND syouhin_cd =@syouhin_cd  ")
    '            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
    '        End If
    '        .AppendLine("  	  )AS SMS ")    '�T�u���i�}�X�^
    '        .AppendLine(" CROSS JOIN ")
    '        .AppendLine(" 	  (SELECT ")
    '        .AppendLine("  	  	  tokubetu_taiou_cd ")  '���ʑΉ��R�[�h
    '        .AppendLine("  	  	  ,tokubetu_taiou_meisyou ")    '-���ʑΉ�����
    '        .AppendLine("  	   FROM ")
    '        .AppendLine("  	   	  m_tokubetu_taiou WITH(READCOMMITTED) ")   '���ʑΉ��}�X�^
    '        .AppendLine("  	   WHERE ")
    '        .AppendLine(" 	      torikesi = 0  ")
    '        If dtParamList.Item("tokubetu_taiou_cd").ToString <> String.Empty Then
    '            .AppendLine("  	  AND tokubetu_taiou_cd = @tokubetu_taiou_cd ")
    '            paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
    '        End If
    '        .AppendLine("  	   )AS SMTT ")  '�T�u���ʑΉ��}�X�^
    '        .AppendLine(" CROSS JOIN ")
    '        .AppendLine("  	   (SELECT ")
    '        .AppendLine("  	       kameiten_cd ")   '�����X�R�[�h
    '        .AppendLine("  	       ,kameiten_mei1 ")    '�����X����1
    '        .AppendLine("  	    FROM ")
    '        .AppendLine(" 	       m_kameiten WITH(READCOMMITTED) ")    '�����X�}�X�^
    '        .AppendLine("   WHERE(1=1) ")
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND kameiten_cd BETWEEN @kameitenCdFrom AND @kameitenCdTo  ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString <> String.Empty And dtParamList.Item("KameitenCdTo").ToString = String.Empty Then
    '            .AppendLine("   AND kameiten_cd = @kameitenCdFrom ")
    '            paramList.Add(MakeParam("@KameitenCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdFrom").ToString))
    '        End If
    '        If dtParamList.Item("KameitenCdFrom").ToString = String.Empty And dtParamList.Item("KameitenCdTo").ToString <> String.Empty Then
    '            .AppendLine("   AND kameiten_cd = @kameitenCdTo ")
    '            paramList.Add(MakeParam("@KameitenCdTo", SqlDbType.VarChar, 5, dtParamList.Item("KameitenCdTo").ToString))
    '        End If
    '        .AppendLine("  	   	)AS SKM ")  '�T�u�����X�}�X�^
    '        .AppendLine("  LEFT JOIN ")
    '        .AppendLine("  	    m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ")
    '        .AppendLine("  ON  SMS.syouhin_cd = MKSTTT.syouhin_cd ")
    '        .AppendLine("  AND SMT.tys_houhou_no = MKSTTT.tys_houhou_no ")
    '        .AppendLine("  AND SMTT.tokubetu_taiou_cd = MKSTTT.tokubetu_taiou_cd ")
    '        .AppendLine("  AND SKM.kameiten_cd = MKSTTT.kameiten_cd ")
    '    End With

    '    'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouCSV", paramList.ToArray)

    '    '�߂�f�[�^�e�[�u��
    '    Return dsReturn.Tables(0).Rows(0).Item(0)
    'End Function

    ''' <summary>���ݒ���܂މ����X���i�������@���ʑΉ��}�X�^CSV�f�[�^�������擾</summary>
    ''' <returns>���ݒ���܂މ����X���i�������@���ʑΉ��}�X�^CSV�f�[�^����</returns>
    Public Function SelTokubetuTaiouCSVCount(ByVal dtParamList As Dictionary(Of String, String)) As Long

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   COUNT_BIG(*) AS CSVCount")
            .AppendLine(" FROM ")
            .AppendLine(" 	(SELECT  ")
            .AppendLine(" 		tys_houhou_no ")
            .AppendLine(" 		,tys_houhou_mei ")
            .AppendLine(" 	 FROM ")
            .AppendLine(" 	  	m_tyousahouhou WITH(READCOMMITTED) ")   '�������@�}�X�^
            .AppendLine(" 	 WHERE ")
            .AppendLine(" 	  	torikesi = 0 ")
            If dtParamList.Item("tys_houhou_no").ToString <> String.Empty Then
                .AppendLine(" 	AND tys_houhou_no = @tys_houhou_no  ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
            End If
            .AppendLine(" 	  )AS SMT ")    '�T�u�������@�}�X�^
            .AppendLine(" CROSS JOIN ")
            .AppendLine("  	 (SELECT ")
            .AppendLine("  	  	 syouhin_cd ")
            .AppendLine("  	  	 ,syouhin_mei ")
            .AppendLine("  	  FROM ")
            .AppendLine("  	  	  m_syouhin WITH(READCOMMITTED) ")  '���i�}�X�^
            .AppendLine(" 	  WHERE ")
            .AppendLine("  	  	  souko_cd = '100' AND torikesi = 0  ")
            If dtParamList.Item("syouhin_cd").ToString <> String.Empty Then
                .AppendLine(" 	  AND syouhin_cd =@syouhin_cd  ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
            End If
            .AppendLine("  	  )AS SMS ")    '�T�u���i�}�X�^
            .AppendLine(" CROSS JOIN ")
            .AppendLine(" 	  (SELECT ")
            .AppendLine("  	  	  tokubetu_taiou_cd ")
            .AppendLine("  	  	  ,tokubetu_taiou_meisyou ")
            .AppendLine("  	   FROM ")
            .AppendLine("  	   	  m_tokubetu_taiou WITH(READCOMMITTED) ")   '���ʑΉ��}�X�^
            .AppendLine("  	   WHERE ")
            .AppendLine(" 	      torikesi = 0  ")
            If dtParamList.Item("tokubetu_taiou_cd").ToString <> String.Empty Then
                .AppendLine("  	  AND tokubetu_taiou_cd = @tokubetu_taiou_cd ")
                paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
            End If
            .AppendLine("  	   )AS SMTT ")  '�T�u���ʑΉ��}�X�^
            .AppendLine(" CROSS JOIN ")
            If "0".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '0' AS aitesaki_syubetu, ")
                .AppendLine("	    'ALL' AS aitesaki_cd, ")
                .AppendLine("	    '�����Ȃ�' AS aitesaki_name ")
                .AppendLine("  	   )AS SUB ") '�T�u
            End If

            If "1".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '1' AS aitesaki_syubetu, ")
                .AppendLine("	    MK.kameiten_cd AS aitesaki_cd, ")
                .AppendLine("	    MK.kameiten_mei1 AS aitesaki_name ")
                .AppendLine("	FROM ")
                .AppendLine("	    m_kameiten AS MK WITH (READCOMMITTED) ")
                .AppendLine("WHERE  (1=1) ")
                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MK.kameiten_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If

                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MK.kameiten_cd = @aitesakiCdFrom ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                End If

                If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MK.kameiten_cd = @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If
                If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                    .AppendLine("   AND MK.torikesi = 0 ")
                End If
                .AppendLine("  	   )AS SUB ") '�T�u
            End If

            If "5".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '5' AS aitesaki_syubetu, ")
                .AppendLine("	    ME.eigyousyo_cd AS aitesaki_cd, ")
                .AppendLine("	    ME.eigyousyo_mei AS aitesaki_name ")
                .AppendLine("	FROM ")
                .AppendLine("	    m_eigyousyo AS ME WITH (READCOMMITTED) ")
                .AppendLine("WHERE  (1=1) ")
                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND ME.eigyousyo_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If

                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND ME.eigyousyo_cd = @aitesakiCdFrom ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                End If

                If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND ME.eigyousyo_cd = @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If
                If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                    .AppendLine("   AND ME.torikesi = 0 ")
                End If
                .AppendLine("  	   )AS SUB ") '�T�u
            End If

            If "7".Equals(dtParamList.Item("aitesakiSyubetu").ToString) Then
                .AppendLine("	(SELECT ")
                .AppendLine("	    '7' AS aitesaki_syubetu, ")
                .AppendLine("	    MKR.keiretu_cd AS aitesaki_cd, ")
                .AppendLine("	    MIN(MKR.keiretu_mei) AS aitesaki_name ")
                .AppendLine("	FROM ")
                .AppendLine("	    m_keiretu AS MKR WITH (READCOMMITTED) ")
                .AppendLine("WHERE  (1=1) ")
                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MKR.keiretu_cd BETWEEN @aitesakiCdFrom AND @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If

                If Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MKR.keiretu_cd = @aitesakiCdFrom ")
                    paramList.Add(MakeParam("@aitesakiCdFrom", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom").ToString))
                End If

                If String.IsNullOrEmpty(dtParamList.Item("aitesakiCdFrom").ToString) AndAlso Not String.IsNullOrEmpty(dtParamList.Item("aitesakiCdTo").ToString) Then
                    .AppendLine("   AND MKR.keiretu_cd = @aitesakiCdTo ")
                    paramList.Add(MakeParam("@aitesakiCdTo", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdTo").ToString))
                End If
                If dtParamList.Item("torikesiFlg").ToString.Equals("1") Then
                    .AppendLine("   AND MKR.torikesi = 0 ")
                End If
                .AppendLine("	GROUP BY MKR.keiretu_cd ")
                .AppendLine("  	   )AS SUB ") '�T�u
            End If

            .AppendLine("  LEFT JOIN ")
            .AppendLine("  	    m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ")
            .AppendLine("  ON  SMS.syouhin_cd = MKSTTT.syouhin_cd ")
            .AppendLine("  AND SMT.tys_houhou_no = MKSTTT.tys_houhou_no ")
            .AppendLine("  AND SMTT.tokubetu_taiou_cd = MKSTTT.tokubetu_taiou_cd ")
            .AppendLine("  AND SUB.aitesaki_syubetu = MKSTTT.aitesaki_syubetu ")
            .AppendLine("  AND SUB.aitesaki_cd = MKSTTT.aitesaki_cd ")

        End With

        'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouCSV", paramList.ToArray)

        '�߂�f�[�^�e�[�u��
        Return dsReturn.Tables(0).Rows(0).Item(0)
    End Function

    ''' <summary>�A�b�v���[�h�Ǘ����擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��f�[�^�e�[�u��</returns>
    Public Function SelInputKanri() As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT TOP 100 ")
            .AppendLine("	CONVERT(varchar(100),syori_datetime,111) + ' ' + CONVERT(varchar(100),syori_datetime,108) AS syori_datetime ") '��������
            .AppendLine("	,CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') AS torikomi_datetime") '�捞����
            .AppendLine("	,nyuuryoku_file_mei ")      '���̓t�@�C����
            .AppendLine("	,CASE error_umu ")
            .AppendLine("    WHEN '1' THEN '����' ")
            .AppendLine("    WHEN '0' THEN '�Ȃ�' ")
            .AppendLine("    ELSE '' ")
            .AppendLine("    END AS error_umu ")            '�G���[�L��
            .AppendLine("    ,edi_jouhou_sakusei_date ")    'EDI���쐬��
            .AppendLine(" FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")  '�A�b�v���[�h�Ǘ��e�[�u��
            .AppendLine(" WHERE ")
            .AppendLine("	file_kbn = 4 ")         '�t�@�C���敪(4�F�����X���i�������@���ʑΉ�M�p)
            .AppendLine(" ORDER BY ")
            .AppendLine("	syori_datetime DESC ")  '��������(�~��)
        End With

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtInputKanri")

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>�A�b�v���[�h�Ǘ��̌������擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��̌���</returns>
    Public Function SelInputKanriCount() As Integer

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(*) ")    '����
            .AppendLine("FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")           '�A�b�v���[�h�Ǘ��e�[�u��
            .AppendLine("WHERE ")
            .AppendLine("	file_kbn = 4 ")             '�t�@�C���敪
        End With

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtInputKanri")

        Return dsReturn.Tables(0).Rows(0).Item(0).ToString

    End Function

    '''' <summary>�����X�R�[�h���擾����</summary>
    'Public Function SelKameitenCd(ByVal strKameitenCd As String) As Boolean

    '    'DataSet�C���X�^���X�̐���
    '    Dim dsReturn As New Data.DataSet

    '    'SQL���̐���
    '    Dim commandTextSb As New StringBuilder

    '    '�p�����[�^�i�[
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL��
    '    With commandTextSb
    '        .AppendLine("SELECT DISTINCT ")
    '        .AppendLine("   kameiten_cd ")
    '        .AppendLine("FROM ")
    '        .AppendLine("   m_kameiten WITH(READCOMMITTED) ")
    '        .AppendLine("WHERE ")
    '        .AppendLine("   kameiten_cd = @kameiten_cd")
    '    End With

    '    '�p�����[�^�쐬
    '    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

    '    ' �������s
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

    '    '�߂�l
    '    If dsReturn.Tables(0).Rows.Count > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If

    'End Function

    ''' <summary>������ʂ��擾����</summary>
    ''' <history>2012/02/07�@��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelAitesakiSyubetuInput(ByVal intAitesakiSyubetu As Integer, ByVal AitesakiCd As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	aitesaki_syubetu ") '--�������
            .AppendLine("	,aitesaki_cd ") '--�����R�[�h
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			0				AS aitesaki_syubetu ")  '--�������
            .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--�����R�[�h
            .AppendLine("			,'�����Ȃ�'	AS aitesaki_mei ")  '--����於
            .AppendLine("			,0				AS torikesi ")  '--���
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			1					AS aitesaki_syubetu ")
            .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--�����X�R�[�h
            .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--�����X��1
            .AppendLine("			,MKA.torikesi		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--�����X�}�X�^
            .AppendLine("	    UNION ALL ")
            .AppendLine("	    SELECT ")
            .AppendLine("	        '5' AS aitesaki_syubetu, ")
            .AppendLine("	        ME.eigyousyo_cd AS aitesaki_cd, ")
            .AppendLine("	        ME.eigyousyo_mei AS aitesaki_name, ")
            .AppendLine("	        ME.torikesi AS torikesi ")
            .AppendLine("	    FROM ")
            .AppendLine("	        m_eigyousyo AS ME WITH (READCOMMITTED) ")   '--�c�Ə��}�X�^
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			7						AS aitesaki_syubetu ")
            .AppendLine("			,MKE.keiretu_cd			AS aitesaki_cd ")   '--�n��R�[�h
            .AppendLine("			,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--�n��
            .AppendLine("			,MIN(MKE.torikesi)		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_keiretu AS MKE WITH(READCOMMITTED) ") '--�n��}�X�^
            .AppendLine("		GROUP BY ")
            .AppendLine("			keiretu_cd ")   '--�n��R�[�h
            .AppendLine("	) AS TA ")
            .AppendLine("WHERE ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine("	AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, intAitesakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, AitesakiCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtAitesakiSyubetu", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>���i�R�[�h���擾����</summary>
    Public Function SelSyouhinCd(ByVal strSyouhinCd As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("	syouhin_cd ")
            .AppendLine("FROM  ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
        End With

        '�p�����[�^�쐬
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>�������@NO���擾����</summary>
    Public Function SelTyousahouhouNo(ByVal intTyousahouhouNo As Integer) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT DISTINCT    ")
            .AppendLine("	tys_houhou_no ")
            .AppendLine("FROM  ")
            .AppendLine("	m_tyousahouhou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_houhou_no = @TyousahouhouNo ")
        End With

        '�p�����[�^�쐬
        paramList.Add(MakeParam("@TyousahouhouNo", SqlDbType.Int, 8, intTyousahouhouNo))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTyousahouhouNo", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>���ʑΉ��R�[�h���擾����</summary>
    Public Function SelTokubetuCd(ByVal intTokubetuCd As Integer) As Boolean
        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT DISTINCT    ")
            .AppendLine("   tokubetu_taiou_cd ")
            .AppendLine("FROM   ")
            .AppendLine("   m_tokubetu_taiou WITH(READCOMMITTED)    ")
            .AppendLine("WHERE  ")
            .AppendLine("   tokubetu_taiou_cd = @tokubetu_taiou_cd  ")
        End With

        '�p�����[�^�쐬
        paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 10, intTokubetuCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTokubetuTaiou", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    '''' <summary>�����X���i�������@���ʑΉ��f�[�^���擾</summary>
    'Public Function SelTokubetuTaiouJyouhou(ByVal strKameitenCd As String, ByVal strSyouhinCd As String, ByVal strTyshouhouNo As String, ByVal strTokubetuCd As String) As Boolean
    '    'DataSet�C���X�^���X�̐���
    '    Dim dsReturn As New Data.DataSet

    '    'SQL���̐���
    '    Dim commandTextSb As New StringBuilder

    '    '�p�����[�^�i�[
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    With commandTextSb
    '        .AppendLine(" SELECT DISTINCT ")
    '        .AppendLine("   kameiten_cd ")
    '        .AppendLine(" FROM ")
    '        .AppendLine("   m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ")
    '        .AppendLine(" WHERE ")
    '        .AppendLine("   kameiten_cd = @kameiten_cd ")
    '        .AppendLine(" AND syouhin_cd = @syouhin_cd ")
    '        .AppendLine(" AND tys_houhou_no = @tys_houhou_no ")
    '        .AppendLine(" AND tokubetu_taiou_cd = @tokubetu_taiou_cd ")
    '    End With

    '    '�p�����[�^�쐬
    '    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
    '    paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
    '    paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, strTyshouhouNo))
    '    paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, strTokubetuCd))

    '    ' �������s
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhou", paramList.ToArray)

    '    '�߂�l
    '    If dsReturn.Tables(0).Rows.Count > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If

    'End Function

    ''' <summary>�����X���i�������@���ʑΉ��f�[�^���擾</summary>
    Public Function SelTokubetuTaiouJyouhou(ByVal strAitesakiSyubetu As String, ByVal strAitesakiCd As String, ByVal strSyouhinCd As String, ByVal strTyshouhouNo As String, ByVal strTokubetuCd As String) As Boolean
        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine(" SELECT DISTINCT ")
            .AppendLine("   aitesaki_cd ")
            .AppendLine(" FROM ")
            .AppendLine("   m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ")
            .AppendLine(" WHERE ")
            .AppendLine("   aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine(" AND aitesaki_cd = @aitesaki_cd ")
            .AppendLine(" AND syouhin_cd = @syouhin_cd ")
            .AppendLine(" AND tys_houhou_no = @tys_houhou_no ")
            .AppendLine(" AND tokubetu_taiou_cd = @tokubetu_taiou_cd ")
        End With

        '�p�����[�^�쐬
        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 5, strAitesakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAitesakiCd))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, strTyshouhouNo))
        paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, strTokubetuCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhou", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>�G���[���e�[�u���ɃG���[���f�[�^��ǉ�</summary>
    Public Function InsTokubetuTaiouError(ByVal dtError As Data.DataTable) As Boolean

        '�߂�l
        Dim InsCount As Integer = 0

        'SQL�R�����g
        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou_error WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		edi_jouhou_sakusei_date ")
            .AppendLine("		,gyou_no ")
            .AppendLine("		,syori_datetime ")
            .AppendLine("		,aitesaki_syubetu ")
            .AppendLine("		,aitesaki_cd ")
            .AppendLine("		,aitesaki_mei ")
            .AppendLine("		,syouhin_cd ")
            .AppendLine("		,syouhin_mei ")
            .AppendLine("		,tys_houhou_no ")
            .AppendLine("		,tys_houhou ")
            .AppendLine("		,tokubetu_taiou_cd ")
            .AppendLine("		,tokubetu_taiou_meisyou ")
            .AppendLine("		,torikesi ")
            .AppendLine("		,kasan_syouhin_cd ")
            .AppendLine("		,kasan_syouhin_mei ")
            .AppendLine("		,syokiti ")
            .AppendLine("		,uri_kasan_gaku ")
            .AppendLine("		,koumuten_kasan_gaku ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine("		@edi_jouhou_sakusei_date ")
            .AppendLine("		,@gyou_no ")
            .AppendLine("		,@syori_datetime ")
            .AppendLine("		,@aitesaki_syubetu ")
            .AppendLine("		,@aitesaki_cd ")
            .AppendLine("		,@aitesaki_mei ")
            .AppendLine("		,@syouhin_cd ")
            .AppendLine("		,@syouhin_mei ")
            .AppendLine("		,@tys_houhou_no ")
            .AppendLine("		,@tys_houhou ")
            .AppendLine("		,@tokubetu_taiou_cd ")
            .AppendLine("		,@tokubetu_taiou_meisyou ")
            .AppendLine("		,@torikesi ")
            .AppendLine("		,@kasan_syouhin_cd ")
            .AppendLine("		,@kasan_syouhin_mei ")
            .AppendLine("		,@syokiti ")
            .AppendLine("		,@uri_kasan_gaku ")
            .AppendLine("		,@koumuten_kasan_gaku ")
            .AppendLine("		,@add_login_user_id ")
            .AppendLine("		,GETDATE() ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine("	) ")
        End With

        For i As Integer = 0 To dtError.Rows.Count - 1
            '�p�����[�^�̐ݒ�
            paramList.Clear()
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item(0).ToString.Trim)))
            paramList.Add(MakeParam("@gyou_no", SqlDbType.Int, 5, InsObj(dtError.Rows(i).Item(16).ToString.Trim)))
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(dtError.Rows(i).Item(17).ToString.Trim))))
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item(1).ToString.Trim)))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item(2).ToString.Trim)))
            paramList.Add(MakeParam("@aitesaki_mei", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item(3).ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item(4).ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_mei", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item(5).ToString.Trim)))
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item(6).ToString.Trim)))
            paramList.Add(MakeParam("@tys_houhou", SqlDbType.VarChar, 32, InsObj(dtError.Rows(i).Item(7).ToString.Trim)))
            paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item(8).ToString.Trim)))
            paramList.Add(MakeParam("@tokubetu_taiou_meisyou", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item(9).ToString.Trim)))
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item(10).ToString.Trim)))
            paramList.Add(MakeParam("@kasan_syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item(11).ToString.Trim)))
            paramList.Add(MakeParam("@kasan_syouhin_mei", SqlDbType.VarChar, 40, GetSyouhinMei(dtError.Rows(i).Item(11).ToString.Trim)))
            paramList.Add(MakeParam("@syokiti", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item(12).ToString.Trim)))
            paramList.Add(MakeParam("@uri_kasan_gaku", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item(13).ToString.Trim)))
            paramList.Add(MakeParam("@koumuten_kasan_gaku", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item(14).ToString.Trim)))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtError.Rows(i).Item(18))))

            '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
            Try
                InsCount = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
            Catch ex As Exception
                Return False
            End Try

            If Not (InsCount > 0) Then
                Return False
            End If
        Next

        Return True
    End Function

    Private Function GetSyouhinMei(ByVal strSyouhinCd As String) As String
        Dim strSyouhinMei As String

        Dim strSql As String = "SELECT syouhin_mei FROM m_syouhin WITH(READCOMMITTED) WHERE syouhin_cd = '" & strSyouhinCd & "'"

        strSyouhinMei = ExecuteScalar(connStr, CommandType.Text, strSql)

        If String.IsNullOrEmpty(strSyouhinMei) Then
            strSyouhinMei = String.Empty
        End If

        Return strSyouhinMei
    End Function

    '''' <summary>�����X���i�������@���ʑΉ��}�X�^�Ƀf�[�^��ǉ��ƍX�V</summary>
    'Public Function InsUpdTokubetuTaiou(ByVal dtOk As Data.DataTable) As Boolean
    '    '�߂�l
    '    Dim InsUpdCount As Integer = 0
    '    '�ǉ��psql��
    '    Dim strSqlIns As New System.Text.StringBuilder
    '    '�X�V�psql��
    '    Dim strSqlUpd As New System.Text.StringBuilder

    '    '�p�����[�^�i�[
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    '�ǉ��pSQL��
    '    With strSqlIns
    '        .AppendLine("INSERT INTO ")
    '        .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou WITH(UPDLOCK) ")
    '        .AppendLine("	( ")
    '        .AppendLine("		kameiten_cd ")
    '        .AppendLine("		,syouhin_cd ")
    '        .AppendLine("		,tys_houhou_no ")
    '        .AppendLine("		,tokubetu_taiou_cd ")
    '        .AppendLine("		,torikesi ")
    '        .AppendLine("		,add_login_user_id ")
    '        .AppendLine("		,add_datetime ")
    '        .AppendLine("		,upd_login_user_id ")
    '        .AppendLine("		,upd_datetime ")
    '        .AppendLine("	) ")
    '        .AppendLine("VALUES ")
    '        .AppendLine("	( ")
    '        .AppendLine("		@kameiten_cd ")
    '        .AppendLine("		,@syouhin_cd ")
    '        .AppendLine("		,@tys_houhou_no ")
    '        .AppendLine("		,@tokubetu_taiou_cd ")
    '        .AppendLine("		,@torikesi ")
    '        .AppendLine("		,@add_login_user_id ")
    '        .AppendLine("		,GETDATE() ")
    '        .AppendLine("		,NULL ")
    '        .AppendLine("		,NULL ")
    '        .AppendLine("	) ")
    '    End With

    '    With strSqlUpd
    '        .AppendLine("UPDATE ")
    '        .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou WITH(UPDLOCK) ")
    '        .AppendLine("SET ")
    '        .AppendLine("	torikesi = @torikesi ")
    '        .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
    '        .AppendLine("	,upd_datetime = GETDATE() ")
    '        .AppendLine("WHERE ")
    '        .AppendLine("	kameiten_cd = @kameiten_cd ")
    '        .AppendLine("   AND ")
    '        .AppendLine("	syouhin_cd = @syouhin_cd ")
    '        .AppendLine("   AND ")
    '        .AppendLine("	tys_houhou_no = @tys_houhou_no ")
    '        .AppendLine("   AND ")
    '        .AppendLine("	tokubetu_taiou_cd = @tokubetu_taiou_cd ")
    '    End With

    '    For i As Integer = 0 To dtOk.Rows.Count - 1
    '        '�p�����[�^�̐ݒ�
    '        paramList.Clear()
    '        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dtOk.Rows(i).Item("kameiten_cd").ToString.Trim))
    '        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtOk.Rows(i).Item("syouhin_cd").ToString.Trim))
    '        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtOk.Rows(i).Item("tys_houhou_no").ToString.Trim))
    '        paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 4, dtOk.Rows(i).Item("tokubetu_taiou_cd").ToString.Trim))
    '        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, dtOk.Rows(i).Item("torikesi").ToString.Trim))
    '        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dtOk.Rows(i).Item("add_login_user_id").ToString.Trim))
    '        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim))

    '        Try
    '            If dtOk.Rows(i).Item("ins_upd_flg").Equals("0") Then
    '                '�ǉ�
    '                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
    '            Else
    '                '�X�V
    '                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
    '            End If

    '            If Not (InsUpdCount > 0) Then
    '                Return False
    '            End If

    '        Catch ex As Exception
    '            Return False
    '        End Try

    '    Next

    '    Return True
    'End Function

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^�Ƀf�[�^��ǉ��ƍX�V</summary>
    ''' <history>2012/02/07�@��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function InsUpdTokubetuTaiou(ByVal dtOk As Data.DataTable) As Boolean
        '�߂�l
        Dim InsUpdCount As Integer = 0
        '�ǉ��psql��
        Dim strSqlIns As New System.Text.StringBuilder
        '�X�V�psql��
        Dim strSqlUpd As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�ǉ��pSQL��
        With strSqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		aitesaki_syubetu ")
            .AppendLine("		,aitesaki_cd ")
            .AppendLine("		,syouhin_cd ")
            .AppendLine("		,tys_houhou_no ")
            .AppendLine("		,tokubetu_taiou_cd ")
            .AppendLine("		,torikesi ")
            .AppendLine("		,kasan_syouhin_cd ")
            .AppendLine("		,syokiti ")
            .AppendLine("		,uri_kasan_gaku ")
            .AppendLine("		,koumuten_kasan_gaku ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine("		@aitesaki_syubetu ")
            .AppendLine("		,@aitesaki_cd ")
            .AppendLine("		,@syouhin_cd ")
            .AppendLine("		,@tys_houhou_no ")
            .AppendLine("		,@tokubetu_taiou_cd ")
            .AppendLine("		,@torikesi ")
            .AppendLine("		,@kasan_syouhin_cd ")
            .AppendLine("		,@syokiti ")
            .AppendLine("		,@uri_kasan_gaku ")
            .AppendLine("		,@koumuten_kasan_gaku ")
            .AppendLine("		,@add_login_user_id ")
            .AppendLine("		,GETDATE() ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine("	) ")
        End With

        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_kamei_syouhin_tys_tokubetu_taiou WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	torikesi = @torikesi ")
            .AppendLine("	,kasan_syouhin_cd = @kasan_syouhin_cd ")
            .AppendLine("	,syokiti = @syokiti ")
            .AppendLine("	,uri_kasan_gaku = @uri_kasan_gaku ")
            .AppendLine("	,koumuten_kasan_gaku = @koumuten_kasan_gaku ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
            .AppendLine("	,upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine("   AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
            .AppendLine("   AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine("   AND ")
            .AppendLine("	tys_houhou_no = @tys_houhou_no ")
            .AppendLine("   AND ")
            .AppendLine("	tokubetu_taiou_cd = @tokubetu_taiou_cd ")
        End With

        For i As Integer = 0 To dtOk.Rows.Count - 1
            '�p�����[�^�̐ݒ�
            paramList.Clear()
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 5, InsObj(dtOk.Rows(i).Item("aitesaki_syubetu").ToString.Trim)))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("aitesaki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtOk.Rows(i).Item("syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, InsObj(dtOk.Rows(i).Item("tys_houhou_no").ToString.Trim)))
            paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 4, InsObj(dtOk.Rows(i).Item("tokubetu_taiou_cd").ToString.Trim)))
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, InsObj(dtOk.Rows(i).Item("torikesi").ToString.Trim)))
            paramList.Add(MakeParam("@kasan_syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtOk.Rows(i).Item("kasan_syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@syokiti", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("syokiti").ToString.Trim)))
            paramList.Add(MakeParam("@uri_kasan_gaku", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("uri_kasan_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@koumuten_kasan_gaku", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("koumuten_kasan_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))

            Try
                If dtOk.Rows(i).Item("ins_upd_flg").Equals("0") Then
                    '�ǉ�
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
                Else
                    '�X�V
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
                End If

                If Not (InsUpdCount > 0) Then
                    Return False
                End If

            Catch ex As Exception
                Return False
            End Try

        Next

        Return True
    End Function

    Function InsObj(ByVal str As String) As Object
        If String.IsNullOrEmpty(str) Then
            Return DBNull.Value
        Else
            Return str
        End If
    End Function

    ''' <summary>�A�b�v���[�h�Ǘ��e�[�u����o�^����</summary>
    Public Function InsInputKanri(ByVal strSyoriDatetime As String, ByVal strNyuuryokuFileMei As String, ByVal strEdiJouhouSakuseiDate As String, ByVal strErrorUmu As Integer, ByVal strAddLoginUserId As String) As Boolean

        '�߂�l
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("INSERT INTO  ")
            .AppendLine("	t_upload_kanri ")
            .AppendLine("	( ")
            .AppendLine("		syori_datetime ")
            .AppendLine("		,nyuuryoku_file_mei ")
            .AppendLine("		,edi_jouhou_sakusei_date ")
            .AppendLine("		,error_umu ")
            .AppendLine("		,file_kbn ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@syori_datetime ")
            .AppendLine("	,@nyuuryoku_file_mei ")
            .AppendLine("	,@edi_jouhou_sakusei_date ")
            .AppendLine("	,@error_umu ")
            .AppendLine("	,4 ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(") ")
        End With

        paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSyoriDatetime))))
        paramList.Add(MakeParam("@nyuuryoku_file_mei", SqlDbType.VarChar, 128, strNyuuryokuFileMei))
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))
        paramList.Add(MakeParam("@error_umu", SqlDbType.Int, 8, strErrorUmu))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strAddLoginUserId))

        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        Try
            InsCount = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
        Catch ex As Exception
            Return False
        End Try

        If InsCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    '''' <summary>�����X���i�������@���ʑΉ��G���[�����擾����</summary>
    '''' <param name="strEdidate">EDI���쐬��</param>
    '''' <param name="strSyoridate">��������</param>
    '''' <returns>�����X���i�������@���ʑΉ��G���[�f�[�^�e�[�u��</returns>
    'Public Function SelTokubetuTaiouError(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
    '    'DataSet�C���X�^���X�̐���
    '    Dim dsReturn As New Data.DataSet
    '    'SQL�R�����g
    '    Dim commandTextSb As New StringBuilder
    '    ''�p�����[�^�i�[
    '    Dim paramList As New List(Of SqlClient.SqlParameter)
    '    'SQL��
    '    With commandTextSb
    '        .AppendLine(" SELECT ")
    '        .AppendLine("   TOP 100 ")
    '        .AppendLine("    MKSTTTE.edi_jouhou_sakusei_date ")     'EDI���쐬��
    '        .AppendLine("    ,MKSTTTE.gyou_no ")    '�sNO
    '        .AppendLine("    ,MKSTTTE.kameiten_cd ")    '�����X�R�[�h
    '        .AppendLine("    ,MKSTTTE.kameiten_mei ")   '�����X��
    '        .AppendLine("    ,MKSTTTE.syouhin_cd ")     '���i�R�[�h
    '        .AppendLine("    ,MKSTTTE.syouhin_mei ")    '���i��
    '        '�������@�i�������@NO:�������@���j
    '        .AppendLine("    ,CASE WHEN ISNULL(MKSTTTE.tys_houhou_no,'') = '' AND ISNULL(MKSTTTE.tys_houhou,'') = '' THEN '' ")
    '        .AppendLine("       ELSE MKSTTTE.tys_houhou_no + '�F' + MKSTTTE.tys_houhou ")
    '        .AppendLine("     END AS tys_houhou ")
    '        '.AppendLine("    ,(MKSTTTE.tys_houhou_no + '�F' + MKSTTTE.tys_houhou) AS tys_houhou ")   
    '        .AppendLine("    ,MKSTTTE.tokubetu_taiou_cd ")  '���ʑΉ��R�[�h
    '        .AppendLine("    ,MKSTTTE.tokubetu_taiou_meisyou ")     '���ʑΉ�����
    '        .AppendLine("    ,CASE MKSTTTE.torikesi ")
    '        .AppendLine("       WHEN '0' THEN '' ")
    '        .AppendLine("       ELSE '���'    ")
    '        .AppendLine("     END AS torikesi ")     '���
    '        .AppendLine("    ,MKSTTTE.add_login_user_id ")      '�o�^��ID
    '        .AppendLine("    ,MKSTTTE.add_datetime ")       '�o�^����
    '        .AppendLine("    ,MKSTTTE.upd_login_user_id ")      '�X�V��ID
    '        .AppendLine("    ,MKSTTTE.upd_datetime ")       '�X�V����
    '        .AppendLine(" FROM  ")
    '        .AppendLine("    m_kamei_syouhin_tys_tokubetu_taiou_error AS MKSTTTE WITH(READCOMMITTED) ")  '�����X���i�������@���ʑΉ��G���[�}�X�^
    '        .AppendLine(" WHERE ")
    '        .AppendLine("    MKSTTTE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")   'EDI���쐬��
    '        .AppendLine(" AND CONVERT(varchar(100),MKSTTTE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MKSTTTE.syori_datetime,114),':','') = @syori_datetime ")  '��������
    '        .AppendLine(" ORDER BY ")
    '        .AppendLine("    MKSTTTE.gyou_no ")
    '    End With
    '    'EDI���쐬��
    '    paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdidate))
    '    '��������
    '    paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))
    '    '�������s
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouError", paramList.ToArray)

    '    Return dsReturn.Tables(0)

    'End Function

    ''' <summary>�����X���i�������@���ʑΉ��G���[�����擾����</summary>
    ''' <param name="strEdidate">EDI���쐬��</param>
    ''' <param name="strSyoridate">��������</param>
    ''' <returns>�����X���i�������@���ʑΉ��G���[�f�[�^�e�[�u��</returns>
    Public Function SelTokubetuTaiouError(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet
        'SQL�R�����g
        Dim commandTextSb As New StringBuilder
        ''�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP 100 ")
            .AppendLine("    MKSTTTE.edi_jouhou_sakusei_date ")     'EDI���쐬��
            .AppendLine("    ,MKSTTTE.gyou_no ")                    '�sNO
            .AppendLine("    ,MKSTTTE.syori_datetime ")             '��������
            .AppendLine("    ,MKSTTTE.aitesaki_syubetu ")           '�������
            .AppendLine("    ,MKSTTTE.aitesaki_cd ")                '�����R�[�h
            .AppendLine("    ,MKSTTTE.aitesaki_mei ")               '����於
            .AppendLine("    ,MKSTTTE.syouhin_cd ")                 '���i�R�[�h
            .AppendLine("    ,MKSTTTE.syouhin_mei ")                '���i��
            '�������@�i�������@NO:�������@���j
            .AppendLine("    ,CASE WHEN ISNULL(MKSTTTE.tys_houhou_no,'') = '' AND ISNULL(MKSTTTE.tys_houhou,'') = '' THEN '' ")
            .AppendLine("       ELSE MKSTTTE.tys_houhou_no + '�F' + MKSTTTE.tys_houhou ")
            .AppendLine("     END AS tys_houhou ")
            '.AppendLine("    ,(MKSTTTE.tys_houhou_no + '�F' + MKSTTTE.tys_houhou) AS tys_houhou ")   
            .AppendLine("    ,MKSTTTE.tokubetu_taiou_cd ")          '���ʑΉ��R�[�h
            .AppendLine("    ,MKSTTTE.tokubetu_taiou_meisyou ")     '���ʑΉ�����
            .AppendLine("    ,CASE MKSTTTE.torikesi ")
            .AppendLine("       WHEN '0' THEN '' ")
            .AppendLine("       ELSE '���'    ")
            .AppendLine("     END AS torikesi ")                    '���
            .AppendLine("    ,MKSTTTE.kasan_syouhin_cd ")           '���z���Z���i�R�[�h
            .AppendLine("    ,MKSTTTE.kasan_syouhin_mei ")          '���z���Z���i��
            .AppendLine("    ,MKSTTTE.syokiti ")                    '�����l
            .AppendLine("    ,MKSTTTE.uri_kasan_gaku ")             '���������Z���z
            .AppendLine("    ,MKSTTTE.koumuten_kasan_gaku ")        '�H���X�������Z���z
            .AppendLine("    ,MKSTTTE.add_login_user_id ")          '�o�^��ID
            .AppendLine("    ,MKSTTTE.add_datetime ")               '�o�^����
            .AppendLine("    ,MKSTTTE.upd_login_user_id ")          '�X�V��ID
            .AppendLine("    ,MKSTTTE.upd_datetime ")               '�X�V����
            .AppendLine(" FROM  ")
            .AppendLine("    m_kamei_syouhin_tys_tokubetu_taiou_error AS MKSTTTE WITH(READCOMMITTED) ")  '�����X���i�������@���ʑΉ��G���[�}�X�^
            .AppendLine(" WHERE ")
            .AppendLine("    MKSTTTE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")   'EDI���쐬��
            .AppendLine(" AND CONVERT(varchar(100),MKSTTTE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MKSTTTE.syori_datetime,114),':','') = @syori_datetime ")  '��������
            .AppendLine(" ORDER BY ")
            .AppendLine("    MKSTTTE.gyou_no ")
        End With
        'EDI���쐬��
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdidate))
        '��������
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))
        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouError", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^�G���[CSV���擾</summary>
    ''' <returns>�����X���i�������@���ʑΉ��}�X�^�G���[CSV�e�[�u��</returns>
    Public Function SelTokubetuTaiouErrorCSV(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet
        'SQL�R�����g
        Dim commandTextSb As New StringBuilder
        ''�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP 5000")
            .AppendLine("    MKSTTTE.edi_jouhou_sakusei_date ")     'EDI���쐬��
            .AppendLine("    ,MKSTTTE.gyou_no ")                    '�sNO
            .AppendLine("    ,MKSTTTE.syori_datetime")              '��������
            .AppendLine("    ,MKSTTTE.aitesaki_syubetu ")           '�������
            .AppendLine("    ,MKSTTTE.aitesaki_cd ")                '�����R�[�h
            .AppendLine("    ,MKSTTTE.aitesaki_mei ")               '����於
            .AppendLine("    ,MKSTTTE.syouhin_cd ")                 '���i�R�[�h
            .AppendLine("    ,MKSTTTE.syouhin_mei ")                '���i��
            .AppendLine("    ,MKSTTTE.tys_houhou_no")               '�������@�R�[�h
            .AppendLine("    ,MKSTTTE.tys_houhou")                  '�������@����
            .AppendLine("    ,MKSTTTE.tokubetu_taiou_cd ")          '���ʑΉ��R�[�h
            .AppendLine("    ,MKSTTTE.tokubetu_taiou_meisyou ")     '���ʑΉ�����
            .AppendLine("    ,MKSTTTE.torikesi ")                   '���
            .AppendLine("    ,MKSTTTE.kasan_syouhin_cd ")           '���z���Z���i�R�[�h
            .AppendLine("    ,MKSTTTE.kasan_syouhin_mei ")          '���z���Z���i��
            .AppendLine("    ,MKSTTTE.syokiti ")                    '�����l
            .AppendLine("    ,MKSTTTE.uri_kasan_gaku ")             '���������Z���z
            .AppendLine("    ,MKSTTTE.koumuten_kasan_gaku ")        '�H���X�������Z���z
            .AppendLine("    ,MKSTTTE.add_login_user_id ")          '�o�^��ID
            .AppendLine("    ,MKSTTTE.add_datetime ")               '�o�^����
            .AppendLine("    ,MKSTTTE.upd_login_user_id ")          '�X�V��ID
            .AppendLine("    ,MKSTTTE.upd_datetime ")               '�X�V����
            .AppendLine(" FROM  ")
            .AppendLine("    m_kamei_syouhin_tys_tokubetu_taiou_error AS MKSTTTE WITH(READCOMMITTED) ")  '�����X���i�������@���ʑΉ��G���[�}�X�^
            .AppendLine(" WHERE ")
            .AppendLine("    MKSTTTE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")   'EDI���쐬��
            .AppendLine(" AND CONVERT(varchar(100),MKSTTTE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MKSTTTE.syori_datetime,114),':','') = @syori_datetime ")  '��������
            .AppendLine(" ORDER BY ")
            .AppendLine("    MKSTTTE.gyou_no ")
        End With
        'EDI���쐬��
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdidate))
        '��������
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))
        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouError", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>�����X���i�������ʑΉ��}�X�^�G���[�������擾����</summary>
    ''' <param name="strEdidate">EDI���쐬��</param>
    ''' <returns>�����X���i�������ʑΉ��}�X�^�G���[����</returns>
    Public Function SelTokubetuTaiouErrorCount(ByVal strEdidate As String, ByVal strSyoridate As String) As Integer
        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT(*) AS Maxcount ")
            .AppendLine(" FROM ")
            .AppendLine("   m_kamei_syouhin_tys_tokubetu_taiou_error AS MKSTTTE WITH(READCOMMITTED) ")  '�����X���i�������@���ʑΉ��G���[�}�X�^
            .AppendLine(" WHERE ")
            .AppendLine("    MKSTTTE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")  'EDI���쐬��
            .AppendLine(" AND CONVERT(varchar(100),MKSTTTE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MKSTTTE.syori_datetime,114),':','') = @syori_datetime ")  '��������
        End With

        'EDI���쐬��
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdidate))
        '��������
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))
        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouErrorCount", paramList.ToArray)

        Return dsReturn.Tables(0).Rows(0).Item("Maxcount")

    End Function

    ''' <summary>����於���擾����</summary>
    ''' <returns>����於�f�[�^�e�[�u��</returns>
    ''' <history>2012/02/08�@��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelAitesakiMei(ByVal intAitesakiSyubetu As Integer, ByVal AitesakiCd As String, ByVal strTorikesiAitesaki As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	aitesaki_syubetu ") '--�������
            .AppendLine("	,aitesaki_cd ") '--�����R�[�h
            .AppendLine("	,aitesaki_mei ")    '--����於
            .AppendLine("FROM  ")
            .AppendLine("	(  ")
            .AppendLine("		SELECT  ")
            .AppendLine("			0				AS aitesaki_syubetu ")  '--�������
            .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--�����R�[�h
            .AppendLine("			,'�����Ȃ�'	AS aitesaki_mei ")  '--����於
            .AppendLine("			,0				AS torikesi ")  '--���
            .AppendLine("		UNION ALL  ")
            .AppendLine("		SELECT  ")
            .AppendLine("			1					AS aitesaki_syubetu  ")
            .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--�����X�R�[�h
            .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--�����X��1
            .AppendLine("			,MKA.torikesi		AS torikesi  ")
            .AppendLine("		FROM  ")
            .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--�����X�}�X�^
            .AppendLine("	    UNION ALL ")
            .AppendLine("	    SELECT ")
            .AppendLine("	        '5'                 AS aitesaki_syubetu, ")
            .AppendLine("	        ME.eigyousyo_cd     AS aitesaki_cd, ")
            .AppendLine("	        ME.eigyousyo_mei    AS aitesaki_name, ")
            .AppendLine("	        ME.torikesi         AS torikesi ")
            .AppendLine("	    FROM ")
            .AppendLine("	        m_eigyousyo AS ME WITH (READCOMMITTED) ")
            .AppendLine("		UNION ALL  ")
            .AppendLine("		SELECT  ")
            .AppendLine("			7						AS aitesaki_syubetu  ")
            .AppendLine("			,MKE.keiretu_cd			AS aitesaki_cd ")   '--�n��R�[�h
            .AppendLine("			,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--�n��
            .AppendLine("			,MIN(MKE.torikesi)		AS torikesi  ")
            .AppendLine("		FROM  ")
            .AppendLine("			m_keiretu AS MKE WITH(READCOMMITTED) ") '--�n��}�X�^
            .AppendLine("		GROUP BY  ")
            .AppendLine("			keiretu_cd ")   '--�n��R�[�h
            .AppendLine("	) AS TA  ")
            .AppendLine(" WHERE  ")
            .AppendLine(" 	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine(" 	AND  ")
            .AppendLine(" 	aitesaki_cd = @aitesaki_cd ")
        End With

        If strTorikesiAitesaki <> String.Empty Then
            '����p�����[�^�̐ݒ�
            commandTextSb.AppendLine(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 8, "0"))
        End If

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, intAitesakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, AitesakiCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtAitesakiCd", paramList.ToArray)

        '�߂�l
        Return dsReturn.Tables(0)

    End Function

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^�����擾(�u�n��E�c�Ə��E�w�薳�����Ώۃ`�F�b�N�{�b�N�X�v=�`�F�b�N�̏ꍇ)</summary>
    ''' <history>2012/05/23 �ԗ� 407553�̑Ή� �ǉ�</history>
    Public Function SelTokubetuTaiouNasiInfo(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            If dtParamList.Item("kensuu").ToString <> "max" Then
                .AppendLine("   TOP " & dtParamList.Item("kensuu").ToString & "")
            End If
            .AppendLine("	SUB_MKSTTT.aitesaki_syubetu ") '--������� ")
            .AppendLine("	,(LTRIM(STR(SUB_MKSTTT.aitesaki_syubetu)) + '�F' +MKM.meisyou) AS aitesaki_syubetu_layout ") '�������(�������:�g������)
            .AppendLine("	,SUB_MKSTTT.aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("	,MKM.meisyou ")                         '�g������
            .AppendLine("	,SUB.aitesaki_name ")                   '����於
            .AppendLine("	,SUB_MKSTTT.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("	,MS.syouhin_mei ")                      '���i����
            .AppendLine("	,SUB_MKSTTT.tys_houhou_no ") '--�������@NO ")
            .AppendLine("	,MT.tys_houhou_mei ")                   '�������@����
            .AppendLine("	,(LTRIM(STR(SUB_MKSTTT.tys_houhou_no)) + '�F' +MT.tys_houhou_mei) AS tys_houhou ") '�������@(�������@NO:�������@����)
            .AppendLine("	,SUB_MKSTTT.tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("	,MTT.tokubetu_taiou_meisyou ")          '���ʑΉ�����
            .AppendLine("	,SUB_MKSTTT.torikesi ") '--��� ")
            .AppendLine("	,SUB_MKSTTT.kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("	,SUB_MKSTTT.syokiti ") '--�����l ")
            .AppendLine("	,SUB_MKSTTT.uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("	,SUB_MKSTTT.koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			aitesaki_syubetu ") '--������� ")
            .AppendLine("			,aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("			,syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("			,tys_houhou_no ") '--�������@NO ")
            .AppendLine("			,tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("			,torikesi ") '--��� ")
            .AppendLine("			,kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("			,syokiti ") '--�����l ")
            .AppendLine("			,uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("			,koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("		WHERE ")
            .AppendLine("			aitesaki_syubetu = @aitesaki_syubetu1 ") '--������� = 1 ")
            .AppendLine("			AND ")
            .AppendLine("			aitesaki_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("		UNION ALL ") '--==================================================================================================== ")
            .AppendLine("		SELECT ")
            .AppendLine("			MKSTTT.aitesaki_syubetu ") '--������� ")
            .AppendLine("			,MKSTTT.aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("			,MKSTTT.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("			,MKSTTT.tys_houhou_no ") '--�������@NO ")
            .AppendLine("			,MKSTTT.tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("			,MKSTTT.torikesi ") '--��� ")
            .AppendLine("			,MKSTTT.kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("			,MKSTTT.syokiti ") '--�����l ")
            .AppendLine("			,MKSTTT.uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("			,MKSTTT.koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("			INNER JOIN ")
            .AppendLine("			m_kameiten AS MK WITH(READCOMMITTED) ") '--�����X�}�X�^ ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.aitesaki_syubetu = @aitesaki_syubetu5 ") '--������� = 5 ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.aitesaki_cd = MK.eigyousyo_cd ") '--����M.�����R�[�h = ��M.�c�Ə��R�[�h ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					aitesaki_syubetu ") '--������� ")
            .AppendLine("					,aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					,syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("					,tys_houhou_no ") '--�������@NO ")
            .AppendLine("					,tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("					,torikesi ") '--��� ")
            .AppendLine("					,kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("					,syokiti ") '--�����l ")
            .AppendLine("					,uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("					,koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("				WHERE ")
            .AppendLine("					aitesaki_syubetu = @aitesaki_syubetu1 ") '--������� = 1 ")
            .AppendLine("					AND ")
            .AppendLine("					aitesaki_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					AND ")
            .AppendLine("					ISNULL(torikesi,0) = 0 ") '--��� = 0 ")
            .AppendLine("			 ")
            .AppendLine("			) AS SUB_MKSTTT_1 ") '--�����T�u1 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_1.syouhin_cd ") '--����M.���i�R�[�h = �����T�u1.���i�R�[�h ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_1.tys_houhou_no ") '--����M.�������@NO = �����T�u1.�������@NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_1.tokubetu_taiou_cd ") '--����M.���ʑΉ��R�[�h = �����T�u1.���ʑΉ��R�[�h
            .AppendLine("		WHERE ")
            .AppendLine("			MK.kameiten_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_1.aitesaki_cd IS NULL ") '--�����T�u1.�����R�[�h IS NULL ")
            .AppendLine("		UNION ALL ") '--==================================================================================================== ")
            .AppendLine("		SELECT ")
            .AppendLine("			MKSTTT.aitesaki_syubetu ") '--������� ")
            .AppendLine("			,MKSTTT.aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("			,MKSTTT.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("			,MKSTTT.tys_houhou_no ") '--�������@NO ")
            .AppendLine("			,MKSTTT.tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("			,MKSTTT.torikesi ") '--��� ")
            .AppendLine("			,MKSTTT.kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("			,MKSTTT.syokiti ") '--�����l ")
            .AppendLine("			,MKSTTT.uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("			,MKSTTT.koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("			INNER JOIN ")
            .AppendLine("			m_kameiten AS MK WITH(READCOMMITTED) ") '--�����X�}�X�^ ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.aitesaki_syubetu = @aitesaki_syubetu7 ") '--������� = 7 ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.aitesaki_cd = MK.keiretu_cd ") '--����M.�����R�[�h = ��M.�n��R�[�h ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					aitesaki_syubetu ") '--������� ")
            .AppendLine("					,aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					,syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("					,tys_houhou_no ") '--�������@NO ")
            .AppendLine("					,tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("					,torikesi ") '--��� ")
            .AppendLine("					,kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("					,syokiti ") '--�����l ")
            .AppendLine("					,uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("					,koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("				WHERE ")
            .AppendLine("					aitesaki_syubetu = @aitesaki_syubetu1 ") '--������� = 1 ")
            .AppendLine("					AND ")
            .AppendLine("					aitesaki_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					AND ")
            .AppendLine("					ISNULL(torikesi,0) = 0 ") '--��� = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_1 ") '--�����T�u1 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_1.syouhin_cd ") '--����M.���i�R�[�h = �����T�u1.���i�R�[�h ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_1.tys_houhou_no ") '--����M.�������@NO = �����T�u1.�������@NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_1.tokubetu_taiou_cd ") '--����M.���ʑΉ��R�[�h = �����T�u1.���ʑΉ��R�[�h
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MKSTTT.aitesaki_syubetu ") '--������� ")
            .AppendLine("					,MKSTTT.aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					,MKSTTT.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("					,MKSTTT.tys_houhou_no ") '--�������@NO ")
            .AppendLine("					,MKSTTT.tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("					,MKSTTT.torikesi ") '--��� ")
            .AppendLine("					,MKSTTT.kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("					,MKSTTT.syokiti ") '--�����l ")
            .AppendLine("					,MKSTTT.uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("					,MKSTTT.koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("					INNER JOIN ")
            .AppendLine("					m_kameiten AS MK WITH(READCOMMITTED) ") '--�����X�}�X�^ ")
            .AppendLine("					ON ")
            .AppendLine("						MKSTTT.aitesaki_syubetu = @aitesaki_syubetu5 ") '--������� = 5 ")
            .AppendLine("						AND ")
            .AppendLine("						MKSTTT.aitesaki_cd = MK.eigyousyo_cd ") '--����M.�����R�[�h = ��M.�c�Ə��R�[�h ")
            .AppendLine("						AND ")
            .AppendLine("						MK.kameiten_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("						AND ")
            .AppendLine("						ISNULL(MKSTTT.torikesi,0) = 0 ") '--��� = 0 ")
            .AppendLine("					 ")
            .AppendLine("			) AS SUB_MKSTTT_5 ") '--�����T�u5 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_5.syouhin_cd ") '--����M.���i�R�[�h = �����T�u5.���i�R�[�h ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_5.tys_houhou_no ") '--����M.�������@NO = �����T�u5.�������@NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_5.tokubetu_taiou_cd ") '--����M.���ʑΉ��R�[�h = �����T�u5.���ʑΉ��R�[�h
            .AppendLine("		WHERE ")
            .AppendLine("			MK.kameiten_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_1.aitesaki_cd IS NULL ") '--�����T�u1.�����R�[�h IS NULL ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_5.aitesaki_cd IS NULL ") '--�����T�u5.�����R�[�h IS NULL ")
            .AppendLine("		UNION ALL ") '--==================================================================================================== ")
            .AppendLine("		SELECT ")
            .AppendLine("			MKSTTT.aitesaki_syubetu ") '--������� ")
            .AppendLine("			,MKSTTT.aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("			,MKSTTT.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("			,MKSTTT.tys_houhou_no ") '--�������@NO ")
            .AppendLine("			,MKSTTT.tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("			,MKSTTT.torikesi ") '--��� ")
            .AppendLine("			,MKSTTT.kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("			,MKSTTT.syokiti ") '--�����l ")
            .AppendLine("			,MKSTTT.uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("			,MKSTTT.koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					aitesaki_syubetu ") '--������� ")
            .AppendLine("					,aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					,syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("					,tys_houhou_no ") '--�������@NO ")
            .AppendLine("					,tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("					,torikesi ") '--��� ")
            .AppendLine("					,kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("					,syokiti ") '--�����l ")
            .AppendLine("					,uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("					,koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("				WHERE ")
            .AppendLine("					aitesaki_syubetu = @aitesaki_syubetu1 ") '--������� = 1 ")
            .AppendLine("					AND ")
            .AppendLine("					aitesaki_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					AND ")
            .AppendLine("					ISNULL(torikesi,0) = 0 ") '--��� = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_1 ") '--�����T�u1 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_1.syouhin_cd ") '--����M.���i�R�[�h = �����T�u1.���i�R�[�h ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_1.tys_houhou_no ") '--����M.�������@NO = �����T�u1.�������@NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_1.tokubetu_taiou_cd ") '--����M.���ʑΉ��R�[�h = �����T�u1.���ʑΉ��R�[�h
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MKSTTT.aitesaki_syubetu ") '--������� ")
            .AppendLine("					,MKSTTT.aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					,MKSTTT.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("					,MKSTTT.tys_houhou_no ") '--�������@NO ")
            .AppendLine("					,MKSTTT.tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("					,MKSTTT.torikesi ") '--��� ")
            .AppendLine("					,MKSTTT.kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("					,MKSTTT.syokiti ") '--�����l ")
            .AppendLine("					,MKSTTT.uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("					,MKSTTT.koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("					INNER JOIN ")
            .AppendLine("					m_kameiten AS MK WITH(READCOMMITTED) ") '--�����X�}�X�^ ")
            .AppendLine("					ON ")
            .AppendLine("						MKSTTT.aitesaki_syubetu = @aitesaki_syubetu5 ") '--������� = 5 ")
            .AppendLine("						AND ")
            .AppendLine("						MKSTTT.aitesaki_cd = MK.eigyousyo_cd ") '--����M.�����R�[�h = ��M.�c�Ə��R�[�h ")
            .AppendLine("						AND ")
            .AppendLine("						MK.kameiten_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("						AND ")
            .AppendLine("						ISNULL(MKSTTT.torikesi,0) = 0 ") '--��� = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_5 ") '--�����T�u5 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_5.syouhin_cd ") '--����M.���i�R�[�h = �����T�u5.���i�R�[�h ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_5.tys_houhou_no ") '--����M.�������@NO = �����T�u5.�������@NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_5.tokubetu_taiou_cd ") '--����M.���ʑΉ��R�[�h = �����T�u5.���ʑΉ��R�[�h
            .AppendLine("				 ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MKSTTT.aitesaki_syubetu ") '--������� ")
            .AppendLine("					,MKSTTT.aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					,MKSTTT.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("					,MKSTTT.tys_houhou_no ") '--�������@NO ")
            .AppendLine("					,MKSTTT.tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("					,MKSTTT.torikesi ") '--��� ")
            .AppendLine("					,MKSTTT.kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("					,MKSTTT.syokiti ") '--�����l ")
            .AppendLine("					,MKSTTT.uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("					,MKSTTT.koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("					INNER JOIN ")
            .AppendLine("					m_kameiten AS MK WITH(READCOMMITTED) ") '--�����X�}�X�^ ")
            .AppendLine("					ON ")
            .AppendLine("						MKSTTT.aitesaki_syubetu = @aitesaki_syubetu7 ") '--������� = 7 ")
            .AppendLine("						AND ")
            .AppendLine("						MKSTTT.aitesaki_cd = MK.keiretu_cd ") '--����M.�����R�[�h = ��M.�n��R�[�h ")
            .AppendLine("						AND ")
            .AppendLine("						MK.kameiten_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("						AND ")
            .AppendLine("						ISNULL(MKSTTT.torikesi,0) = 0 ") '--��� = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_7 ") '--�����T�u7 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_7.syouhin_cd ") '--����M.���i�R�[�h = �����T�u7.���i�R�[�h ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_7.tys_houhou_no ") '--����M.�������@NO = �����T�u7.�������@NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_7.tokubetu_taiou_cd ") '--����M.���ʑΉ��R�[�h = �����T�u7.���ʑΉ��R�[�h
            .AppendLine("		WHERE ")
            .AppendLine("			MKSTTT.aitesaki_syubetu = @aitesaki_syubetu0 ") '--����M.������� = 0 ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_1.aitesaki_cd IS NULL ") '--�����T�u1.�����R�[�h IS NULL ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_5.aitesaki_cd IS NULL ") '--�����T�u5.�����R�[�h IS NULL ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_7.aitesaki_cd IS NULL ") '--�����T�u7.�����R�[�h IS NULL ")
            .AppendLine("	) AS SUB_MKSTTT ") '--�T�u�����X���i�������@���ʑΉ�M ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			'0' AS aitesaki_syubetu, ")
            .AppendLine("			'ALL' AS aitesaki_cd, ")
            .AppendLine("			'�����Ȃ�' AS aitesaki_name, ")
            .AppendLine("			'0' AS torikesi ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'1' AS aitesaki_syubetu, ")
            .AppendLine("			MK.kameiten_cd AS aitesaki_cd, ")
            .AppendLine("			MK.kameiten_mei1 AS aitesaki_name, ")
            .AppendLine("			MK.torikesi AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten AS MK WITH (READCOMMITTED) ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'5' AS aitesaki_syubetu, ")
            .AppendLine("			ME.eigyousyo_cd AS aitesaki_cd, ")
            .AppendLine("			ME.eigyousyo_mei AS aitesaki_name, ")
            .AppendLine("			ME.torikesi AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_eigyousyo AS ME WITH (READCOMMITTED) ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'7' AS aitesaki_syubetu, ")
            .AppendLine("			MKR.keiretu_cd AS aitesaki_cd, ")
            .AppendLine("			MIN(MKR.keiretu_mei) AS aitesaki_name, ")
            .AppendLine("			MIN(MKR.torikesi) AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_keiretu AS MKR WITH (READCOMMITTED) ")
            .AppendLine("		GROUP BY MKR.keiretu_cd ")
            .AppendLine("	) AS SUB ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.aitesaki_syubetu = SUB.aitesaki_syubetu ") '--������� ")
            .AppendLine("		AND ")
            .AppendLine("		SUB_MKSTTT.aitesaki_cd = SUB.aitesaki_cd ") '--������� ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH (READCOMMITTED) ") '--���iM ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.syouhin_cd = MS.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("		AND ")
            .AppendLine("		MS.souko_cd = @souko_cd ") '--�q�ɃR�[�h = "100" ")
            .AppendLine("		AND ")
            .AppendLine("		MS.torikesi = @torikesi ") '--��� = 0 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_tyousahouhou AS MT WITH (READCOMMITTED) ") '--�������@M ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.tys_houhou_no = MT.tys_houhou_no ") '--�������@NO ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_tokubetu_taiou AS MTT WITH (READCOMMITTED) ") '--���ʑΉ�M ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.tokubetu_taiou_cd = MTT.tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH (READCOMMITTED) ") '--�g������M ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.aitesaki_syubetu = MKM.code ") '--������� ")
            .AppendLine("		AND ")
            .AppendLine("		MKM.meisyou_syubetu = @meisyou_syubetu ") '--���̎�� ")
            .AppendLine("WHERE ")
            .AppendLine("	1 = 1 ")
            '���i�R�[�h
            If Not String.IsNullOrEmpty(dtParamList.Item("syouhin_cd").ToString) Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.syouhin_cd = @syouhin_cd ") '--���i�R�[�h ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
            End If
            '�������@NO
            If Not String.IsNullOrEmpty(dtParamList.Item("tys_houhou_no").ToString) Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.tys_houhou_no = @tys_houhou_no ") '--�������@NO ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
            End If
            '���ʑΉ��R�[�h
            If Not String.IsNullOrEmpty(dtParamList.Item("tokubetu_taiou_cd").ToString) Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
                paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
            End If
            '���
            .AppendLine("	AND ")
            .AppendLine("	SUB_MKSTTT.torikesi = @torikesi ") '--��� ")
            '��������͑ΏۊO=�`�F�b�N�̏ꍇ
            If dtParamList.Item("aitesakiTorikesiFlg").ToString.Equals("1") Then
                .AppendLine("	AND ")
                .AppendLine("	SUB.torikesi = @torikesi ") '--��������͑ΏۊO ")
            End If
            '\0�͑ΏۊO=�`�F�b�N�̏ꍇ
            If dtParamList.Item("kingaku0TorikesiFlg").ToString.Equals("1") Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.uri_kasan_gaku <> 0 ") '--\0�͑ΏۊO ")
            End If

            '------------------From 2013.03.09  ���F�ǉ�����-----------------
            '�����l1�̂�=�`�F�b�N�̏ꍇ�A�����X���i�������@���ʑΉ�M.�����l�@=�@"1"
            If dtParamList.Item("Syokiti1Nomi").ToString.Equals("1") Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.syokiti = '1'")
            End If
            '------------------To   2013.03.09  ���F�ǉ�����-----------------

            '����
            .AppendLine("ORDER BY ")
            .AppendLine("	SUB_MKSTTT.syouhin_cd ASC ") '--���i�R�[�h ")
            .AppendLine("	,SUB_MKSTTT.tys_houhou_no ASC ") '--�������@NO ")
            .AppendLine("	,SUB_MKSTTT.tokubetu_taiou_cd ASC ") '--���ʑΉ��R�[�h ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu0", SqlDbType.Int, 10, 0))
        paramList.Add(MakeParam("@aitesaki_syubetu1", SqlDbType.Int, 10, 1))
        paramList.Add(MakeParam("@aitesaki_syubetu5", SqlDbType.Int, 10, 5))
        paramList.Add(MakeParam("@aitesaki_syubetu7", SqlDbType.Int, 10, 7))

        paramList.Add(MakeParam("@souko_cd", SqlDbType.VarChar, 3, "100"))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, 0))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, 23))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom")))

        'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhouNasiInfo", paramList.ToArray)

        '�߂�f�[�^�e�[�u��
        Return dsReturn.Tables("TokubetuTaiouJyouhouNasiInfo")

    End Function

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^�������擾�u�n��E�c�Ə��E�w�薳�����Ώۃ`�F�b�N�{�b�N�X�v=�`�F�b�N�̏ꍇ</summary>
    ''' <history>2012/05/23 �ԗ� 407553�̑Ή� �ǉ�</history>
    Public Function SelTokubetuTaiouNasiCount(ByVal dtParamList As Dictionary(Of String, String)) As Integer

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   COUNT(SUB_MKSTTT.aitesaki_syubetu) AS count ") '--���� ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			aitesaki_syubetu ") '--������� ")
            .AppendLine("			,aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("			,syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("			,tys_houhou_no ") '--�������@NO ")
            .AppendLine("			,tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("			,torikesi ") '--��� ")
            .AppendLine("			,kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("			,syokiti ") '--�����l ")
            .AppendLine("			,uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("			,koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("		WHERE ")
            .AppendLine("			aitesaki_syubetu = @aitesaki_syubetu1 ") '--������� = 1 ")
            .AppendLine("			AND ")
            .AppendLine("			aitesaki_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("		UNION ALL ") '--==================================================================================================== ")
            .AppendLine("		SELECT ")
            .AppendLine("			MKSTTT.aitesaki_syubetu ") '--������� ")
            .AppendLine("			,MKSTTT.aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("			,MKSTTT.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("			,MKSTTT.tys_houhou_no ") '--�������@NO ")
            .AppendLine("			,MKSTTT.tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("			,MKSTTT.torikesi ") '--��� ")
            .AppendLine("			,MKSTTT.kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("			,MKSTTT.syokiti ") '--�����l ")
            .AppendLine("			,MKSTTT.uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("			,MKSTTT.koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("			INNER JOIN ")
            .AppendLine("			m_kameiten AS MK WITH(READCOMMITTED) ") '--�����X�}�X�^ ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.aitesaki_syubetu = @aitesaki_syubetu5 ") '--������� = 5 ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.aitesaki_cd = MK.eigyousyo_cd ") '--����M.�����R�[�h = ��M.�c�Ə��R�[�h ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					aitesaki_syubetu ") '--������� ")
            .AppendLine("					,aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					,syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("					,tys_houhou_no ") '--�������@NO ")
            .AppendLine("					,tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("					,torikesi ") '--��� ")
            .AppendLine("					,kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("					,syokiti ") '--�����l ")
            .AppendLine("					,uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("					,koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("				WHERE ")
            .AppendLine("					aitesaki_syubetu = @aitesaki_syubetu1 ") '--������� = 1 ")
            .AppendLine("					AND ")
            .AppendLine("					aitesaki_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					AND ")
            .AppendLine("					ISNULL(torikesi,0) = 0 ") '--��� = 0 ")
            .AppendLine("			 ")
            .AppendLine("			) AS SUB_MKSTTT_1 ") '--�����T�u1 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_1.syouhin_cd ") '--����M.���i�R�[�h = �����T�u1.���i�R�[�h ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_1.tys_houhou_no ") '--����M.�������@NO = �����T�u1.�������@NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_1.tokubetu_taiou_cd ") '--����M.���ʑΉ��R�[�h = �����T�u1.���ʑΉ��R�[�h
            .AppendLine("		WHERE ")
            .AppendLine("			MK.kameiten_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_1.aitesaki_cd IS NULL ") '--�����T�u1.�����R�[�h IS NULL ")
            .AppendLine("		UNION ALL ") '--==================================================================================================== ")
            .AppendLine("		SELECT ")
            .AppendLine("			MKSTTT.aitesaki_syubetu ") '--������� ")
            .AppendLine("			,MKSTTT.aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("			,MKSTTT.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("			,MKSTTT.tys_houhou_no ") '--�������@NO ")
            .AppendLine("			,MKSTTT.tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("			,MKSTTT.torikesi ") '--��� ")
            .AppendLine("			,MKSTTT.kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("			,MKSTTT.syokiti ") '--�����l ")
            .AppendLine("			,MKSTTT.uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("			,MKSTTT.koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("			INNER JOIN ")
            .AppendLine("			m_kameiten AS MK WITH(READCOMMITTED) ") '--�����X�}�X�^ ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.aitesaki_syubetu = @aitesaki_syubetu7 ") '--������� = 7 ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.aitesaki_cd = MK.keiretu_cd ") '--����M.�����R�[�h = ��M.�n��R�[�h ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					aitesaki_syubetu ") '--������� ")
            .AppendLine("					,aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					,syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("					,tys_houhou_no ") '--�������@NO ")
            .AppendLine("					,tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("					,torikesi ") '--��� ")
            .AppendLine("					,kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("					,syokiti ") '--�����l ")
            .AppendLine("					,uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("					,koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("				WHERE ")
            .AppendLine("					aitesaki_syubetu = @aitesaki_syubetu1 ") '--������� = 1 ")
            .AppendLine("					AND ")
            .AppendLine("					aitesaki_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					AND ")
            .AppendLine("					ISNULL(torikesi,0) = 0 ") '--��� = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_1 ") '--�����T�u1 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_1.syouhin_cd ") '--����M.���i�R�[�h = �����T�u1.���i�R�[�h ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_1.tys_houhou_no ") '--����M.�������@NO = �����T�u1.�������@NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_1.tokubetu_taiou_cd ") '--����M.���ʑΉ��R�[�h = �����T�u1.���ʑΉ��R�[�h
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MKSTTT.aitesaki_syubetu ") '--������� ")
            .AppendLine("					,MKSTTT.aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					,MKSTTT.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("					,MKSTTT.tys_houhou_no ") '--�������@NO ")
            .AppendLine("					,MKSTTT.tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("					,MKSTTT.torikesi ") '--��� ")
            .AppendLine("					,MKSTTT.kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("					,MKSTTT.syokiti ") '--�����l ")
            .AppendLine("					,MKSTTT.uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("					,MKSTTT.koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("					INNER JOIN ")
            .AppendLine("					m_kameiten AS MK WITH(READCOMMITTED) ") '--�����X�}�X�^ ")
            .AppendLine("					ON ")
            .AppendLine("						MKSTTT.aitesaki_syubetu = @aitesaki_syubetu5 ") '--������� = 5 ")
            .AppendLine("						AND ")
            .AppendLine("						MKSTTT.aitesaki_cd = MK.eigyousyo_cd ") '--����M.�����R�[�h = ��M.�c�Ə��R�[�h ")
            .AppendLine("						AND ")
            .AppendLine("						MK.kameiten_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("						AND ")
            .AppendLine("						ISNULL(MKSTTT.torikesi,0) = 0 ") '--��� = 0 ")
            .AppendLine("					 ")
            .AppendLine("			) AS SUB_MKSTTT_5 ") '--�����T�u5 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_5.syouhin_cd ") '--����M.���i�R�[�h = �����T�u5.���i�R�[�h ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_5.tys_houhou_no ") '--����M.�������@NO = �����T�u5.�������@NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_5.tokubetu_taiou_cd ") '--����M.���ʑΉ��R�[�h = �����T�u5.���ʑΉ��R�[�h
            .AppendLine("		WHERE ")
            .AppendLine("			MK.kameiten_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_1.aitesaki_cd IS NULL ") '--�����T�u1.�����R�[�h IS NULL ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_5.aitesaki_cd IS NULL ") '--�����T�u5.�����R�[�h IS NULL ")
            .AppendLine("		UNION ALL ") '--==================================================================================================== ")
            .AppendLine("		SELECT ")
            .AppendLine("			MKSTTT.aitesaki_syubetu ") '--������� ")
            .AppendLine("			,MKSTTT.aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("			,MKSTTT.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("			,MKSTTT.tys_houhou_no ") '--�������@NO ")
            .AppendLine("			,MKSTTT.tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("			,MKSTTT.torikesi ") '--��� ")
            .AppendLine("			,MKSTTT.kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("			,MKSTTT.syokiti ") '--�����l ")
            .AppendLine("			,MKSTTT.uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("			,MKSTTT.koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					aitesaki_syubetu ") '--������� ")
            .AppendLine("					,aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					,syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("					,tys_houhou_no ") '--�������@NO ")
            .AppendLine("					,tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("					,torikesi ") '--��� ")
            .AppendLine("					,kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("					,syokiti ") '--�����l ")
            .AppendLine("					,uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("					,koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("				WHERE ")
            .AppendLine("					aitesaki_syubetu = @aitesaki_syubetu1 ") '--������� = 1 ")
            .AppendLine("					AND ")
            .AppendLine("					aitesaki_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					AND ")
            .AppendLine("					ISNULL(torikesi,0) = 0 ") '--��� = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_1 ") '--�����T�u1 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_1.syouhin_cd ") '--����M.���i�R�[�h = �����T�u1.���i�R�[�h ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_1.tys_houhou_no ") '--����M.�������@NO = �����T�u1.�������@NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_1.tokubetu_taiou_cd ") '--����M.���ʑΉ��R�[�h = �����T�u1.���ʑΉ��R�[�h
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MKSTTT.aitesaki_syubetu ") '--������� ")
            .AppendLine("					,MKSTTT.aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					,MKSTTT.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("					,MKSTTT.tys_houhou_no ") '--�������@NO ")
            .AppendLine("					,MKSTTT.tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("					,MKSTTT.torikesi ") '--��� ")
            .AppendLine("					,MKSTTT.kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("					,MKSTTT.syokiti ") '--�����l ")
            .AppendLine("					,MKSTTT.uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("					,MKSTTT.koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("					INNER JOIN ")
            .AppendLine("					m_kameiten AS MK WITH(READCOMMITTED) ") '--�����X�}�X�^ ")
            .AppendLine("					ON ")
            .AppendLine("						MKSTTT.aitesaki_syubetu = @aitesaki_syubetu5 ") '--������� = 5 ")
            .AppendLine("						AND ")
            .AppendLine("						MKSTTT.aitesaki_cd = MK.eigyousyo_cd ") '--����M.�����R�[�h = ��M.�c�Ə��R�[�h ")
            .AppendLine("						AND ")
            .AppendLine("						MK.kameiten_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("						AND ")
            .AppendLine("						ISNULL(MKSTTT.torikesi,0) = 0 ") '--��� = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_5 ") '--�����T�u5 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_5.syouhin_cd ") '--����M.���i�R�[�h = �����T�u5.���i�R�[�h ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_5.tys_houhou_no ") '--����M.�������@NO = �����T�u5.�������@NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_5.tokubetu_taiou_cd ") '--����M.���ʑΉ��R�[�h = �����T�u5.���ʑΉ��R�[�h
            .AppendLine("				 ")
            .AppendLine("			LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MKSTTT.aitesaki_syubetu ") '--������� ")
            .AppendLine("					,MKSTTT.aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("					,MKSTTT.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("					,MKSTTT.tys_houhou_no ") '--�������@NO ")
            .AppendLine("					,MKSTTT.tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("					,MKSTTT.torikesi ") '--��� ")
            .AppendLine("					,MKSTTT.kasan_syouhin_cd ") '--���z���Z���i�R�[�h ")
            .AppendLine("					,MKSTTT.syokiti ") '--�����l ")
            .AppendLine("					,MKSTTT.uri_kasan_gaku ") '--���������Z���z ")
            .AppendLine("					,MKSTTT.koumuten_kasan_gaku ") '--�H���X�������Z���z ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kamei_syouhin_tys_tokubetu_taiou AS MKSTTT WITH(READCOMMITTED) ") '--�����X���i�������@���ʑΉ��}�X�^ ")
            .AppendLine("					INNER JOIN ")
            .AppendLine("					m_kameiten AS MK WITH(READCOMMITTED) ") '--�����X�}�X�^ ")
            .AppendLine("					ON ")
            .AppendLine("						MKSTTT.aitesaki_syubetu = @aitesaki_syubetu7 ") '--������� = 7 ")
            .AppendLine("						AND ")
            .AppendLine("						MKSTTT.aitesaki_cd = MK.keiretu_cd ") '--����M.�����R�[�h = ��M.�n��R�[�h ")
            .AppendLine("						AND ")
            .AppendLine("						MK.kameiten_cd = @aitesaki_cd ") '--�����R�[�h ")
            .AppendLine("						AND ")
            .AppendLine("						ISNULL(MKSTTT.torikesi,0) = 0 ") '--��� = 0 ")
            .AppendLine("			) AS SUB_MKSTTT_7 ") '--�����T�u7 ")
            .AppendLine("			ON ")
            .AppendLine("				MKSTTT.syouhin_cd = SUB_MKSTTT_7.syouhin_cd ") '--����M.���i�R�[�h = �����T�u7.���i�R�[�h ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tys_houhou_no = SUB_MKSTTT_7.tys_houhou_no ") '--����M.�������@NO = �����T�u7.�������@NO ")
            .AppendLine("				AND ")
            .AppendLine("				MKSTTT.tokubetu_taiou_cd = SUB_MKSTTT_7.tokubetu_taiou_cd ") '--����M.���ʑΉ��R�[�h = �����T�u7.���ʑΉ��R�[�h
            .AppendLine("		WHERE ")
            .AppendLine("			MKSTTT.aitesaki_syubetu = @aitesaki_syubetu0 ") '--����M.������� = 0 ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_1.aitesaki_cd IS NULL ") '--�����T�u1.�����R�[�h IS NULL ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_5.aitesaki_cd IS NULL ") '--�����T�u5.�����R�[�h IS NULL ")
            .AppendLine("			AND ")
            .AppendLine("			SUB_MKSTTT_7.aitesaki_cd IS NULL ") '--�����T�u7.�����R�[�h IS NULL ")
            .AppendLine("	) AS SUB_MKSTTT ") '--�T�u�����X���i�������@���ʑΉ�M ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			'0' AS aitesaki_syubetu, ")
            .AppendLine("			'ALL' AS aitesaki_cd, ")
            .AppendLine("			'�����Ȃ�' AS aitesaki_name, ")
            .AppendLine("			'0' AS torikesi ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'1' AS aitesaki_syubetu, ")
            .AppendLine("			MK.kameiten_cd AS aitesaki_cd, ")
            .AppendLine("			MK.kameiten_mei1 AS aitesaki_name, ")
            .AppendLine("			MK.torikesi AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten AS MK WITH (READCOMMITTED) ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'5' AS aitesaki_syubetu, ")
            .AppendLine("			ME.eigyousyo_cd AS aitesaki_cd, ")
            .AppendLine("			ME.eigyousyo_mei AS aitesaki_name, ")
            .AppendLine("			ME.torikesi AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_eigyousyo AS ME WITH (READCOMMITTED) ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'7' AS aitesaki_syubetu, ")
            .AppendLine("			MKR.keiretu_cd AS aitesaki_cd, ")
            .AppendLine("			MIN(MKR.keiretu_mei) AS aitesaki_name, ")
            .AppendLine("			MIN(MKR.torikesi) AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_keiretu AS MKR WITH (READCOMMITTED) ")
            .AppendLine("		GROUP BY MKR.keiretu_cd ")
            .AppendLine("	) AS SUB ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.aitesaki_syubetu = SUB.aitesaki_syubetu ") '--������� ")
            .AppendLine("		AND ")
            .AppendLine("		SUB_MKSTTT.aitesaki_cd = SUB.aitesaki_cd ") '--������� ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH (READCOMMITTED) ") '--���iM ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.syouhin_cd = MS.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("		AND ")
            .AppendLine("		MS.souko_cd = @souko_cd ") '--�q�ɃR�[�h = "100" ")
            .AppendLine("		AND ")
            .AppendLine("		MS.torikesi = @torikesi ") '--��� = 0 ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_tyousahouhou AS MT WITH (READCOMMITTED) ") '--�������@M ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.tys_houhou_no = MT.tys_houhou_no ") '--�������@NO ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_tokubetu_taiou AS MTT WITH (READCOMMITTED) ") '--���ʑΉ�M ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.tokubetu_taiou_cd = MTT.tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH (READCOMMITTED) ") '--�g������M ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_MKSTTT.aitesaki_syubetu = MKM.code ") '--������� ")
            .AppendLine("		AND ")
            .AppendLine("		MKM.meisyou_syubetu = @meisyou_syubetu ") '--���̎�� ")
            .AppendLine("WHERE ")
            .AppendLine("	1 = 1 ")
            '���i�R�[�h
            If Not String.IsNullOrEmpty(dtParamList.Item("syouhin_cd").ToString) Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.syouhin_cd = @syouhin_cd ") '--���i�R�[�h ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtParamList.Item("syouhin_cd").ToString))
            End If
            '�������@NO
            If Not String.IsNullOrEmpty(dtParamList.Item("tys_houhou_no").ToString) Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.tys_houhou_no = @tys_houhou_no ") '--�������@NO ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 5, dtParamList.Item("tys_houhou_no")))
            End If
            '���ʑΉ��R�[�h
            If Not String.IsNullOrEmpty(dtParamList.Item("tokubetu_taiou_cd").ToString) Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.tokubetu_taiou_cd = @tokubetu_taiou_cd ") '--���ʑΉ��R�[�h ")
                paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.Int, 5, dtParamList.Item("tokubetu_taiou_cd")))
            End If
            '���
            .AppendLine("	AND ")
            .AppendLine("	SUB_MKSTTT.torikesi = @torikesi ") '--��� ")
            '��������͑ΏۊO=�`�F�b�N�̏ꍇ
            If dtParamList.Item("aitesakiTorikesiFlg").ToString.Equals("1") Then
                .AppendLine("	AND ")
                .AppendLine("	SUB.torikesi = @torikesi ") '--��������͑ΏۊO ")
            End If
            '\0�͑ΏۊO=�`�F�b�N�̏ꍇ
            If dtParamList.Item("kingaku0TorikesiFlg").ToString.Equals("1") Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.uri_kasan_gaku <> 0 ") '--\0�͑ΏۊO ")
            End If

            '------------------From 2013.03.09  ���F�ǉ�����-----------------
            '�����l1�̂�=�`�F�b�N�̏ꍇ�A�����X���i�������@���ʑΉ�M.�����l�@=�@"1"
            If dtParamList.Item("Syokiti1Nomi").ToString.Equals("1") Then
                .AppendLine("	AND ")
                .AppendLine("	SUB_MKSTTT.syokiti = '1'")
            End If
            '------------------To   2013.03.09  ���F�ǉ�����-----------------

        End With

        paramList.Add(MakeParam("@aitesaki_syubetu0", SqlDbType.Int, 10, 0))
        paramList.Add(MakeParam("@aitesaki_syubetu1", SqlDbType.Int, 10, 1))
        paramList.Add(MakeParam("@aitesaki_syubetu5", SqlDbType.Int, 10, 5))
        paramList.Add(MakeParam("@aitesaki_syubetu7", SqlDbType.Int, 10, 7))

        paramList.Add(MakeParam("@souko_cd", SqlDbType.VarChar, 3, "100"))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, 0))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, 23))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtParamList.Item("aitesakiCdFrom")))

        'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouJyouhouNasiCount", paramList.ToArray)

        '�߂�f�[�^�e�[�u��
        Return Convert.ToInt32(dsReturn.Tables("TokubetuTaiouJyouhouNasiCount").Rows(0).Item(0))

    End Function

    ''' <summary>
    ''' ���l�����̎擾
    ''' </summary>
    ''' <returns>���l������</returns>
    Public Function SelStyleMeisyou() As Object
        Try
            Dim strSql As String = "SELECT meisyou FROM m_meisyou  WITH(READCOMMITTED) WHERE meisyou_syubetu='80' and  code=0 "
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return String.Empty
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

End Class
