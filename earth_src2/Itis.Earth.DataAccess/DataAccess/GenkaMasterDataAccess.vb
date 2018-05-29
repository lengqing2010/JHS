Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>�����}�X�^</summary>
''' <remarks>�����}�X�^�p�@�\��񋟂���</remarks>
''' <history>
''' <para>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class GenkaMasterDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>������Ж����擾����</summary>
    ''' <returns>������Ж��f�[�^�e�[�u��</returns>
    ''' <history>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTyousaKaisyaMei(ByVal strTyousaKaisyaCd As String, ByVal strJigyousyoCd As String, ByVal strKensakuTaisyouGai As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	tys_kaisya_mei ")
            .AppendLine("FROM  ")
            .AppendLine("	m_tyousakaisya WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_kaisya_cd = @tys_kaisya_cd ")
            .AppendLine("	AND ")
            .AppendLine("	jigyousyo_cd = @jigyousyo_cd ")

            If strKensakuTaisyouGai <> String.Empty Then
                '����p�����[�^�̐ݒ�
                .AppendLine("	AND ")
                .AppendLine("	torikesi = @torikesi ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 8, "0"))
            End If

        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTyousaKaisyaCd))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTyousaKaisyaMei", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>������ʂ��擾����</summary>
    ''' <returns>������ʃf�[�^�e�[�u��</returns>
    ''' <history>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelAiteSakiSyubetu() As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	code ")                                 '�R�[�h
            .AppendLine("	,(code + '�F' + meisyou) AS meisyou ")  '����
            .AppendLine("FROM ")
            .AppendLine("	m_kakutyou_meisyou WITH(READCOMMITTED) ")                   '�g�����̃}�X�^
            .AppendLine("WHERE ")
            .AppendLine("	meisyou_syubetu = 22 ")                 '���̎��
        End With

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtAiteSakiSyubetu")

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>����於���擾����</summary>
    ''' <returns>����於�f�[�^�e�[�u��</returns>
    ''' <history>2011/03/07�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
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

    ''' <summary>���i�R�[�h���擾����</summary>
    ''' <returns>���i�R�[�h�f�[�^�e�[�u��</returns>
    ''' <history>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelSyouhinCd() As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	syouhin_cd ")                       '���i�R�[�h
            .AppendLine("	,(syouhin_cd + '�F' + syouhin_mei) AS syouhin_mei ") '���i��
            .AppendLine("FROM ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ")                        '���i�}�X�^
            .AppendLine("WHERE ")
            .AppendLine("	torikesi = 0 ")                     '���
            .AppendLine("	and ")
            .AppendLine("	souko_cd = '100' ")                 '�q�ɃR�[�h
        End With

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd")

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>�������@���擾����</summary>
    ''' <returns>�������@�f�[�^�e�[�u��</returns>
    ''' <history>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTyousaHouhou() As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
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

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTyousahouhou")

        Return dsReturn.Tables(0)

    End Function


    ''' <summary>���������擾����</summary>
    ''' <returns>�������f�[�^�e�[�u��</returns>
    ''' <history>2011/02/28�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelGenkaJyouhou(ByVal strKensakuCount As String, ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As GenkaMasterDataSet.GenkaInfoTableDataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New GenkaMasterDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            '��������
            If strKensakuCount.Trim.Equals("10") Then
                .AppendLine("   TOP 10 ")
            End If
            If strKensakuCount.Trim.Equals("100") Then
                .AppendLine("   TOP 100 ")
            End If
            .AppendLine("	(MG.tys_kaisya_cd + '�F' + MG.jigyousyo_cd) AS tys_kaisya_cd ")  '--������ЃR�[�h(������ЃR�[�h�F���Ə��R�[�h)
            .AppendLine("	,MTS.tys_kaisya_mei ")  '--������Ж�")
            .AppendLine("	,(LTRIM(STR(MG.aitesaki_syubetu)) + '�F' + MKM.meisyou) AS aitesaki_syubetu ")   '--�������	(������ʁF����)
            .AppendLine("	,MG.aitesaki_cd ")  '--�����R�[�h
            .AppendLine("	,SUB.aitesaki_mei ")    '--����於
            .AppendLine("	,MG.syouhin_cd ")   '--���i�R�[�h
            .AppendLine("	,MS.syouhin_mei ")  '--���i��
            .AppendLine("	,(LTRIM(STR(MG.tys_houhou_no)) + '�F' + MTH.tys_houhou_mei) AS tys_houhou ") '--�������@(�������@NO�F�������@����)
            .AppendLine("   ,MG.tys_houhou_no ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN MG.torikesi = 0 THEN ")
            .AppendLine("			'' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'���' ")
            .AppendLine("		END AS torikesi ")  '--���
            .AppendLine("	,MG.tou_kkk1 ")   '--�����i1
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg1,0) = 0 THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg1 ")   '--�����i�ύXFLG1
            .AppendLine("	,MG.tou_kkk2 ")   '--�����i2
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg2,0) = 0 THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg2 ")   '--�����i�ύXFLG2
            .AppendLine("	,MG.tou_kkk3 ")   '--�����i3
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg3,0) = 0 THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg3 ")   '--�����i�ύXFLG3
            .AppendLine("	,MG.tou_kkk4 ")   '--�����i4
            .AppendLine("	,CASE")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg4,0) = 0 THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg4 ")   '--�����i�ύXFLG4
            .AppendLine("	,MG.tou_kkk5 ")   '--�����i5
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg5,0) = 0 THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg5 ")   '--�����i�ύXFLG5
            .AppendLine("	,MG.tou_kkk6 ")   '--�����i6
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg6,0) = 0 THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg6 ")   '--�����i�ύXFLG6
            .AppendLine("	,MG.tou_kkk7 ")   '--�����i7
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg7,0) = 0 THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg7 ")   '--�����i�ύXFLG7
            .AppendLine("	,MG.tou_kkk8 ")   '--�����i8
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg8,0) = 0 THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg8 ")   '--�����i�ύXFLG8
            .AppendLine("	,MG.tou_kkk9 ")   '--�����i9
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg9,0) = 0 THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg9 ")   '--�����i�ύXFLG9
            .AppendLine("	,MG.tou_kkk10 ") '--�����i10
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg10,0) = 0 THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg10 ")  '--�����i�ύXFLG10
            .AppendLine("	,MG.tou_kkk11t19 ")   '--�����i11�`19
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg11t19,0) = 0 THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg11t19 ")   '--�����i�ύXFLG11�`19
            .AppendLine("	,MG.tou_kkk20t29 ")   '--�����i20�`29
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg20t29,0) = 0 THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg20t29 ")   '--�����i�ύXFLG20�`29
            .AppendLine("	,MG.tou_kkk30t39 ")   '--�����i30�`39
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg30t39,0) = 0 THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg30t39 ")   '--�����i�ύXFLG30�`39
            .AppendLine("	,MG.tou_kkk40t49 ")   '--�����i40�`49
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg40t49,0) = 0 THEN ")
            .AppendLine("			'�ύX�s��'")
            .AppendLine("		ELSE")
            .AppendLine("			'�ύX��'")
            .AppendLine("		END AS tou_kkk_henkou_flg40t49 ")   '--�����i�ύXFLG40�`49
            .AppendLine("	,MG.tou_kkk50t ")   '--�����i50�`
            .AppendLine("	,CASE")
            .AppendLine("		WHEN ISNULL(MG.tou_kkk_henkou_flg50t,0) = 0 THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg50t ") '--�����i�ύXFLG50�`
            .AppendLine("FROM ")
            .AppendLine("	m_genka AS MG WITH(READCOMMITTED) ")    '--����M
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			0			AS aitesaki_syubetu ")  '--�������
            .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--�����R�[�h
            .AppendLine("			,'�����Ȃ�'		AS aitesaki_mei ")  '--����於
            .AppendLine("			,0			AS torikesi	 ") '--���")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			1			AS aitesaki_syubetu ")
            .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--�����X�R�[�h
            .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--�����X��1
            .AppendLine("			,MKA.torikesi		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--�����X�}�X�^
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			3			AS aitesaki_syubetu ")
            .AppendLine("			,'JIO'	AS aitesaki_cd ")
            .AppendLine("			,'JIO��'	AS aitesaki_mei ")
            .AppendLine("			,'0'		AS torikesi ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			7			        AS aitesaki_syubetu ")
            .AppendLine("			,MKE.keiretu_cd	    AS aitesaki_cd ")   '--�n��R�[�h
            .AppendLine("			,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--�n��
            .AppendLine("			,MIN(MKE.torikesi)		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_keiretu AS MKE WITH(READCOMMITTED) ") '--�n��}�X�^
            .AppendLine("           GROUP BY ")
            .AppendLine("               MKE.keiretu_cd ")
            .AppendLine("	) AS SUB ")
            .AppendLine("	ON ")
            .AppendLine("		MG.aitesaki_syubetu = SUB.aitesaki_syubetu ")
            .AppendLine("		AND ")
            .AppendLine("		MG.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_tyousakaisya AS MTS WITH(READCOMMITTED) ")    '--������Ѓ}�X�^
            .AppendLine("	ON ")
            .AppendLine("		MG.tys_kaisya_cd  =  MTS.tys_kaisya_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MG.jigyousyo_cd  =  MTS.jigyousyo_cd ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH(READCOMMITTED) ")  '--���i�}�X�^
            .AppendLine("	ON ")
            .AppendLine("		MG.syouhin_cd  =  MS.syouhin_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MS.souko_cd  =  '100' ")    '--�q�ɃR�[�h
            .AppendLine("		AND ")
            .AppendLine("		MS.torikesi = 0 ")
            .AppendLine("	LEFT JOIN")
            .AppendLine("	m_tyousahouhou AS MTH WITH(READCOMMITTED) ")    '--�������@�}�X�^
            .AppendLine("		ON ")
            .AppendLine("		MG.tys_houhou_no  =  MTH.tys_houhou_no ")   '--�������@NO
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH(READCOMMITTED) ")    '--�g�����̃}�X�^
            .AppendLine("	ON ")
            .AppendLine("		MG.aitesaki_syubetu=MKM.code ") '--����
            .AppendLine("		AND ")
            .AppendLine("		MKM.meisyou_syubetu='22' ") '--���̎��
            .AppendLine("WHERE ")
            '�������ЃR�[�h�
            If Not strTysKaisyaCd.Trim.Equals(String.Empty) Then
                .AppendLine("	MG.tys_kaisya_cd = @tys_kaisya_cd ")   '--������ЃR�[�h
                .AppendLine("	AND")
                .AppendLine("	MG.jigyousyo_cd = @jigyousyo_cd ")   '--���Ə��R�[�h
                .AppendLine("	AND ")
            End If
            '�������
            If Not strAtesakiSyubetu.Trim.Equals(String.Empty) Then
                .AppendLine("	MG.aitesaki_syubetu = @aitesaki_syubetu ")   '--�������
                .AppendLine("	AND")
                If (Not strAtesakiCdFrom.Trim.Equals(String.Empty)) AndAlso (Not strAtesakiCdTo.Trim.Equals(String.Empty)) Then
                    '�����R�[�hFROM�A�����R�[�hTO���������͂���Ă���ꍇ
                    .AppendLine("	MG.aitesaki_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")   '--�����R�[�h
                    .AppendLine("	AND ")
                Else
                    '�����R�[�hFROM�݂̂��邢�́A�����R�[�hTO�����͂���Ă���ꍇ
                    If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
                        .AppendLine("	MG.aitesaki_cd = @aitesaki_cd_from ")   '--�����R�[�h
                        .AppendLine("	AND ")
                    Else
                        If Not strAtesakiCdTo.Trim.Equals(String.Empty) Then
                            .AppendLine("	MG.aitesaki_cd = @aitesaki_cd_to ")   '--�����R�[�h
                            .AppendLine("	AND ")
                        End If
                    End If
                End If
            End If
            If Not strSyouhinCd.Trim.Equals(String.Empty) Then
                '���i�R�[�h=���͂̏ꍇ
                .AppendLine("	MG.syouhin_cd = @syouhin_cd ")   '--���i�R�[�h
                .AppendLine("	AND")
            End If
            If Not strHouhouCd.Equals(String.Empty) Then
                '�������@=���͂̏ꍇ
                .AppendLine("	MG.tys_houhou_no = @tys_houhou_no ")   '--�������@
                .AppendLine("	AND")
            End If
            If blnKensakuTaisyouGai.Equals(True) Then
                '����͌����ΏۊO=�`�F�b�N�̏ꍇ
                .AppendLine("	MG.torikesi = 0 ")   '--���
                .AppendLine("	AND")
            End If
            If blnAitesakiTaisyouGai.Equals(True) Then
                '��������͑ΏۊO=�`�F�b�N�̏ꍇ
                .AppendLine("	SUB.torikesi = 0 ")   '--���
                .AppendLine("	AND")
            End If
            .AppendLine("	1 = 1 ")
            .AppendLine("ORDER BY ")
            .AppendLine("	MG.tys_kaisya_cd ") '--������ЃR�[�h
            .AppendLine("	,MG.jigyousyo_cd ") '--���Ə��R�[�h
            .AppendLine("	,MG.aitesaki_syubetu ") '--�������
            .AppendLine("	,MG.aitesaki_cd ")  '--�����R�[�h
            .AppendLine("	,MG.syouhin_cd ")   '--���i�R�[�h
            .AppendLine("	,MG.tys_houhou_no ")    '--�������@NO
        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 4, Left(strTysKaisyaCd, 4)))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, IIf(strTysKaisyaCd.Length > 4, Mid(strTysKaisyaCd, 5, 6), String.Empty)))
        If Not strAtesakiSyubetu.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, CInt(strAtesakiSyubetu)))
        End If
        paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, strAtesakiCdFrom))
        paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, strAtesakiCdTo))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        If Not strHouhouCd.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 8, CInt(strHouhouCd)))
        End If

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, dsReturn.GenkaInfoTable.TableName, paramList.ToArray)

        Return dsReturn.GenkaInfoTable

    End Function





    ''' <summary>������񌏐����擾����</summary>
    ''' <returns>�������f�[�^�e�[�u��</returns>
    ''' <history>2011/02/28�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelGenkaJyouhouCount(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Integer

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(MG.tys_kaisya_cd) ")  '--����
            .AppendLine("FROM ")
            .AppendLine("	m_genka AS MG WITH(READCOMMITTED) ")    '--����M
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			0			AS aitesaki_syubetu ")  '--�������
            .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--�����R�[�h
            .AppendLine("			,'�����Ȃ�'		AS aitesaki_mei ")  '--����於
            .AppendLine("			,0			AS torikesi	 ") '--���")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			1			AS aitesaki_syubetu ")
            .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--�����X�R�[�h
            .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--�����X��1
            .AppendLine("			,MKA.torikesi		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--�����X�}�X�^
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			3			AS aitesaki_syubetu ")
            .AppendLine("			,'JIO'	AS aitesaki_cd ")
            .AppendLine("			,'JIO��'	AS aitesaki_mei ")
            .AppendLine("			,'0'		AS torikesi ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			7			        AS aitesaki_syubetu ")
            .AppendLine("			,MKE.keiretu_cd	    AS aitesaki_cd ")   '--�n��R�[�h
            .AppendLine("			,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--�n��
            .AppendLine("			,MIN(MKE.torikesi)		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_keiretu AS MKE WITH(READCOMMITTED) ") '--�n��}�X�^
            .AppendLine("           GROUP BY ")
            .AppendLine("               MKE.keiretu_cd ")
            .AppendLine("	) AS SUB ")
            .AppendLine("	ON ")
            .AppendLine("		MG.aitesaki_syubetu = SUB.aitesaki_syubetu ")
            .AppendLine("		AND ")
            .AppendLine("		MG.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_tyousakaisya AS MTS WITH(READCOMMITTED) ")    '--������Ѓ}�X�^
            .AppendLine("	ON ")
            .AppendLine("		MG.tys_kaisya_cd  =  MTS.tys_kaisya_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MG.jigyousyo_cd  =  MTS.jigyousyo_cd ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH(READCOMMITTED) ")  '--���i�}�X�^
            .AppendLine("	ON ")
            .AppendLine("		MG.syouhin_cd  =  MS.syouhin_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MS.souko_cd  =  '100' ")    '--�q�ɃR�[�h
            .AppendLine("		AND ")
            .AppendLine("		MS.torikesi = 0 ")
            .AppendLine("	LEFT JOIN")
            .AppendLine("	m_tyousahouhou AS MTH WITH(READCOMMITTED) ")    '--�������@�}�X�^
            .AppendLine("		ON ")
            .AppendLine("		MG.tys_houhou_no  =  MTH.tys_houhou_no ")   '--�������@NO
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH(READCOMMITTED) ")    '--�g�����̃}�X�^
            .AppendLine("	ON ")
            .AppendLine("		MG.aitesaki_syubetu=MKM.code ") '--����
            .AppendLine("		AND ")
            .AppendLine("		MKM.meisyou_syubetu='22' ") '--���̎��
            .AppendLine("WHERE ")
            '�������ЃR�[�h�
            If Not strTysKaisyaCd.Trim.Equals(String.Empty) Then
                .AppendLine("	MG.tys_kaisya_cd = @tys_kaisya_cd ")   '--������ЃR�[�h
                .AppendLine("	AND")
                .AppendLine("	MG.jigyousyo_cd = @jigyousyo_cd ")   '--���Ə��R�[�h
                .AppendLine("	AND ")
            End If
            '�������
            If Not strAtesakiSyubetu.Trim.Equals(String.Empty) Then
                .AppendLine("	MG.aitesaki_syubetu = @aitesaki_syubetu ")   '--�������
                .AppendLine("	AND")
                If (Not strAtesakiCdFrom.Trim.Equals(String.Empty)) AndAlso (Not strAtesakiCdTo.Trim.Equals(String.Empty)) Then
                    '�����R�[�hFROM�A�����R�[�hTO���������͂���Ă���ꍇ
                    .AppendLine("	MG.aitesaki_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")   '--�����R�[�h
                    .AppendLine("	AND ")
                Else
                    '�����R�[�hFROM�݂̂��邢�́A�����R�[�hTO�����͂���Ă���ꍇ
                    If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
                        .AppendLine("	MG.aitesaki_cd = @aitesaki_cd_from ")   '--�����R�[�h
                        .AppendLine("	AND ")
                    Else
                        If Not strAtesakiCdTo.Trim.Equals(String.Empty) Then
                            .AppendLine("	MG.aitesaki_cd = @aitesaki_cd_to ")   '--�����R�[�h
                            .AppendLine("	AND ")
                        End If
                    End If
                End If
            End If
            If Not strSyouhinCd.Trim.Equals(String.Empty) Then
                '���i�R�[�h=���͂̏ꍇ
                .AppendLine("	MG.syouhin_cd = @syouhin_cd ")   '--���i�R�[�h
                .AppendLine("	AND")
            End If
            If Not strHouhouCd.Equals(String.Empty) Then
                '�������@=���͂̏ꍇ
                .AppendLine("	MG.tys_houhou_no = @tys_houhou_no ")   '--�������@
                .AppendLine("	AND")
            End If
            If blnKensakuTaisyouGai.Equals(True) Then
                '����͌����ΏۊO=�`�F�b�N�̏ꍇ
                .AppendLine("	MG.torikesi = 0 ")   '--���
                .AppendLine("	AND")
            End If
            If blnAitesakiTaisyouGai.Equals(True) Then
                '��������͑ΏۊO=�`�F�b�N�̏ꍇ
                .AppendLine("	SUB.torikesi = 0 ")   '--���
                .AppendLine("	AND")
            End If
            .AppendLine("	1 = 1 ")
        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 4, Left(strTysKaisyaCd, 4)))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, IIf(strTysKaisyaCd.Length > 4, Mid(strTysKaisyaCd, 5, 6), String.Empty)))
        If Not strAtesakiSyubetu.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, CInt(strAtesakiSyubetu)))
        End If
        paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, strAtesakiCdFrom))
        paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, strAtesakiCdTo))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        If Not strHouhouCd.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 8, CInt(strHouhouCd)))
        End If

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtGenkaJyouhouCount", paramList.ToArray)

        Return CInt(dsReturn.Tables(0).Rows(0).Item(0).ToString.Trim)

    End Function

    ''' <summary>�����X�������擾����</summary>
    ''' <returns>�����X����</returns>
    Public Function SelKameitenCount(ByVal strAitesakiCdFrom As String, ByVal strAitesakiCdTo As String, ByVal strTorikesiAitesaki As String) As Integer

        'DataSet�C���X�^���X�̐���
        Dim dsKameiten As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT(*) AS count ")
            .AppendLine(" FROM ")
            .AppendLine("   m_kameiten WITH(READCOMMITTED) ")   '�����X�}�X�^
            .AppendLine(" WHERE ")

            '�����R�[�h
            .AppendLine(" kameiten_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
            paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, strAitesakiCdFrom))
            paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, strAitesakiCdTo))

            '��������
            If strTorikesiAitesaki <> String.Empty Then
                .AppendLine(" AND torikesi = @torikesi_aitesaki ")
                paramList.Add(MakeParam("@torikesi_aitesaki", SqlDbType.Int, 8, 0))
            End If

        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKameiten, _
                    "dtKameitenCount", paramList.ToArray)

        Return dsKameiten.Tables("dtKameitenCount").Rows(0).Item("count")

    End Function


    ''' <summary>���ݒ���܂ތ���CSV�f�[�^���擾����</summary>
    ''' <returns>���ݒ���܂ތ���CSV�f�[�^�e�[�u��</returns>
    Public Function SelMiSeteiGenkaCSVInfo(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	RIGHT(CONVERT(varchar(10),GETDATE(),112),4) + REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date  ")
            .AppendLine("	,ISNULL(SUB_MTK.tys_kaisya_cd,'')  ")
            .AppendLine("	,ISNULL(SUB_MTK.jigyousyo_cd,'')  ")
            .AppendLine("	,ISNULL(SUB_MTK.tys_kaisya_mei,'')  ")
            .AppendLine("	,ISNULL(SUB.aitesaki_syubetu,'')  ")
            .AppendLine("	,ISNULL(SUB.aitesaki_cd,'')  ")
            .AppendLine("	,ISNULL(SUB.aitesaki_mei,'')  ")
            .AppendLine("	,ISNULL(SUB_MSH.syouhin_cd,'')  ")
            .AppendLine("	,ISNULL(SUB_MSH.syouhin_mei,'')  ")
            .AppendLine("	,ISNULL(SUB_MTH.tys_houhou_no,'')  ")
            .AppendLine("	,ISNULL(SUB_MTH.tys_houhou_mei,'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.torikesi),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk1),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg1),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk2),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg2),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk3),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg3),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk4),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg4),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk5),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg5),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk6),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg6),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk7),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg7),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk8),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg8),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk9),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg9),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk10),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg10),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk11t19),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg11t19),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk20t29),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg20t29),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk30t39),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg30t39),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk40t49),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg40t49),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk50t),'')  ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg50t),'')  ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			tys_houhou_no ")
            .AppendLine("			,tys_houhou_mei ")
            .AppendLine("		FROM ")
            .AppendLine("			m_tyousahouhou WITH(READCOMMITTED) ")
            .AppendLine("		WHERE ")
            .AppendLine("			ISNULL(genka_settei_fuyou_flg,0) = 0 ") '--�����ݒ�s�v�t���O 
            '���������@=���͂̏ꍇ��
            If Not strHouhouCd.Trim.Equals(String.Empty) Then
                .AppendLine("			AND ")
                .AppendLine("			tys_houhou_no = @tys_houhou_no ")
            End If
            .AppendLine("	) AS SUB_MTH ") '--�T�u�������@M 
            .AppendLine("	CROSS JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			syouhin_cd ")
            .AppendLine("			,syouhin_mei ")
            .AppendLine("		FROM ")
            .AppendLine("			m_syouhin WITH(READCOMMITTED) ")
            .AppendLine("		WHERE ")
            .AppendLine("			souko_cd = '100' ")
            .AppendLine("			AND ")
            .AppendLine("			torikesi = 0 ")
            '�����i�R�[�h=���͂̏ꍇ��
            If Not strSyouhinCd.Trim.Equals(String.Empty) Then
                .AppendLine("			AND ")
                .AppendLine("			syouhin_cd = @syouhin_cd ")
            End If
            .AppendLine("	) AS SUB_MSH ") '--�T�u���iM ")
            .AppendLine("	CROSS JOIN ")
            .AppendLine("	( ")
            If strAtesakiSyubetu.Trim.Equals(String.Empty) Then
                .AppendLine("		SELECT ")
                .AppendLine("			0			AS aitesaki_syubetu ")  '--�������
                .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--�����R�[�h
                .AppendLine("			,'�����Ȃ�'		AS aitesaki_mei ")  '--����於
                .AppendLine("			,0			AS torikesi	 ") '--���")
                .AppendLine("		UNION ALL ")
                .AppendLine("		SELECT ")
                .AppendLine("			1			AS aitesaki_syubetu ")
                .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--�����X�R�[�h
                .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--�����X��1
                .AppendLine("			,MKA.torikesi		AS torikesi ")
                .AppendLine("		FROM ")
                .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--�����X�}�X�^
                '����������͑ΏۊO=�`�F�b�N�̏ꍇ��
                If blnAitesakiTaisyouGai.Equals(True) Then
                    .AppendLine("       WHERE ")
                    .AppendLine("           AND ")
                    .AppendLine("	        MKA.torikesi = 0 ")   '--���
                End If
                .AppendLine("		UNION ALL ")
                .AppendLine("		SELECT ")
                .AppendLine("			3			AS aitesaki_syubetu ")
                .AppendLine("			,'JIO'	AS aitesaki_cd ")
                .AppendLine("			,'JIO��'	AS aitesaki_mei ")
                .AppendLine("			,'0'		AS torikesi ")
                .AppendLine("		UNION ALL ")
                .AppendLine("		SELECT ")
                .AppendLine("			7			        AS aitesaki_syubetu ")
                .AppendLine("			,MKE.keiretu_cd	    AS aitesaki_cd ")   '--�n��R�[�h
                .AppendLine("			,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--�n��
                .AppendLine("			,MIN(MKE.torikesi)		AS torikesi ")
                .AppendLine("		FROM ")
                .AppendLine("			m_keiretu AS MKE WITH(READCOMMITTED) ") '--�n��}�X�^
                '����������͑ΏۊO=�`�F�b�N�̏ꍇ��
                If blnAitesakiTaisyouGai.Equals(True) Then
                    .AppendLine("       WHERE ")
                    .AppendLine("           AND ")
                    .AppendLine("	        MKA.torikesi = 0 ")   '--���
                End If
                .AppendLine("           GROUP BY ")
                .AppendLine("               MKE.keiretu_cd ")
            Else
                Select Case strAtesakiSyubetu
                    Case "0"
                        .AppendLine("		SELECT ")
                        .AppendLine("			0			AS aitesaki_syubetu ")  '--������� 
                        .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--�����R�[�h 
                        .AppendLine("			,'�����Ȃ�'		AS aitesaki_mei ")  '--����於 
                    Case "3"
                        .AppendLine("		SELECT ")
                        .AppendLine("			3			AS aitesaki_syubetu ")
                        .AppendLine("			,'JIO'	AS aitesaki_cd ")
                        .AppendLine("			,'JIO��'	AS aitesaki_mei ")
                    Case "1"
                        .AppendLine("		SELECT ")
                        .AppendLine("			1					AS aitesaki_syubetu ")
                        .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--�����X�R�[�h 
                        .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--�����X��1 
                        .AppendLine("		FROM ")
                        .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--�����X�}�X�^ 
                        .AppendLine("		WHERE ")
                        .AppendLine("           1=1 ")
                        '�������R�[�hFROM�A�����R�[�hTO���������͂���Ă���ꍇ��
                        If (Not strAtesakiCdFrom.Trim.Equals(String.Empty)) AndAlso (Not strAtesakiCdTo.Trim.Equals(String.Empty)) Then
                            .AppendLine("           AND ")
                            .AppendLine("			MKA.kameiten_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                        Else
                            '�������R�[�hFROM�݂̂��邢�́A�����R�[�hTO�����͂���Ă���ꍇ��
                            If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
                                .AppendLine("           AND ")
                                .AppendLine("	        MKA.kameiten_cd = @aitesaki_cd_from ")   '--�����X�R�[�h 
                            Else
                                If Not strAtesakiCdTo.Trim.Equals(String.Empty) Then
                                    .AppendLine("           AND ")
                                    .AppendLine("	        MKA.kameiten_cd = @aitesaki_cd_to ")   '--�����X�R�[�h
                                End If
                            End If
                        End If
                        '����������͑ΏۊO=�`�F�b�N�̏ꍇ��
                        If blnAitesakiTaisyouGai.Equals(True) Then
                            .AppendLine("           AND ")
                            .AppendLine("	        MKA.torikesi = 0 ")   '--���
                        End If
                    Case "7"
                        .AppendLine("			SELECT ")
                        .AppendLine("				7			 AS aitesaki_syubetu ")
                        .AppendLine("				,MKE.keiretu_cd	 AS aitesaki_cd ")  '--�n��R�[�h 
                        .AppendLine("				,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--�n�� 
                        .AppendLine("			FROM ")
                        .AppendLine("				m_keiretu AS MKE WITH(READCOMMITTED) ") '--�n��}�X�^ 
                        .AppendLine(" ")
                        .AppendLine("			WHERE ")
                        .AppendLine("           1=1 ")
                        '�������R�[�hFROM�A�����R�[�hTO���������͂���Ă���ꍇ��
                        If (Not strAtesakiCdFrom.Trim.Equals(String.Empty)) AndAlso (Not strAtesakiCdTo.Trim.Equals(String.Empty)) Then
                            .AppendLine("           AND ")
                            .AppendLine("           MKE.keiretu_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                        Else
                            '�������R�[�hFROM�݂̂��邢�́A�����R�[�hTO�����͂���Ă���ꍇ��
                            If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
                                .AppendLine("           AND ")
                                .AppendLine("	        MKE.keiretu_cd = @aitesaki_cd_from ")   '--�n��R�[�h
                            Else
                                If Not strAtesakiCdTo.Trim.Equals(String.Empty) Then
                                    .AppendLine("           AND ")
                                    .AppendLine("           MKE.keiretu_cd = @aitesaki_cd_to ")   '--�n��R�[�h
                                End If
                            End If
                        End If
                        '����������͑ΏۊO=�`�F�b�N�̏ꍇ��
                        If blnAitesakiTaisyouGai.Equals(True) Then
                            .AppendLine("           AND ")
                            .AppendLine("	        MKE.torikesi = 0 ")   '--���
                        End If
                        .AppendLine("			GROUP BY ")
                        .AppendLine("				MKE.keiretu_cd ")
                End Select

            End If
            .AppendLine("	) AS SUB ")
            .AppendLine("	CROSS JOIN  ")
            .AppendLine("	( ")
            .AppendLine("		SELECT  ")
            .AppendLine("			tys_kaisya_cd ")
            .AppendLine("			,jigyousyo_cd ")
            .AppendLine("			,torikesi ")
            .AppendLine("			,tys_kaisya_mei  ")
            .AppendLine("		FROM  ")
            .AppendLine("			m_tyousakaisya WITH(READCOMMITTED)  ")
            .AppendLine("		WHERE ")
            .AppendLine("           1=1 ")
            '�������ЃR�[�h�
            If Not strTysKaisyaCd.Trim.Equals(String.Empty) Then
                .AppendLine("           AND ")
                .AppendLine("			tys_kaisya_cd = @tys_kaisya_cd ") '--������ЃR�[�h
                .AppendLine("			AND ")
                .AppendLine("			jigyousyo_cd = @jigyousyo_cd ") '--���Ə��R�[�h
            End If
            .AppendLine("	) AS SUB_MTK ") '--�T�u�������M ")
            .AppendLine("	LEFT OUTER JOIN  ")
            .AppendLine("	m_genka as MG WITH(READCOMMITTED)  ")
            .AppendLine("	ON  ")
            .AppendLine("		SUB_MSH.syouhin_cd = MG.syouhin_cd ")   '--�T�u���iM.���i�R�[�h = ����M.���i�R�[�h  ")
            .AppendLine("		AND  ")
            .AppendLine("		SUB_MTH.tys_houhou_no = MG.tys_houhou_no ")   '--�T�u���iM.���i�R�[�h = ����M.���i�R�[�h  ")
            .AppendLine("		AND  ")
            .AppendLine("		SUB.aitesaki_syubetu = MG.aitesaki_syubetu ")   '--�T�u.�������=����M.�������  ")
            .AppendLine("		AND  ")
            .AppendLine("		SUB.aitesaki_cd = MG.aitesaki_cd ") '--�T�u.�����R�[�h=����M.�����R�[�h  ")
            .AppendLine("		AND  ")
            .AppendLine("		MG.tys_kaisya_cd = SUB_MTK.tys_kaisya_cd ") '--����M.������ЃR�[�h = �T�u�������M.������ЃR�[�h  ")
            .AppendLine("		AND  ")
            .AppendLine("		MG.jigyousyo_cd = SUB_MTK.jigyousyo_cd ")   '--����M.���Ə��R�[�h = �T�u�������M.���Ə��R�[�h  ")
            .AppendLine("		  ")
            .AppendLine("ORDER BY  ")
            .AppendLine("  ")
            .AppendLine("SUB_MTK.tys_kaisya_cd ")    '--������ЃR�[�h  ")
            .AppendLine(",SUB_MTK.jigyousyo_cd ")    '--���Ə��R�[�h  ")
            .AppendLine(",SUB.aitesaki_syubetu ")    '--�������  ")
            .AppendLine(",SUB.aitesaki_cd ") '--�����R�[�h  ")
            .AppendLine(",SUB_MSH.syouhin_cd ")  '--���i�R�[�h  ")
            .AppendLine(",SUB_MTH.tys_houhou_no ")   '--�������@NO ")
        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 4, Left(strTysKaisyaCd, 4)))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, IIf(strTysKaisyaCd.Length > 4, Mid(strTysKaisyaCd, 5, 6), String.Empty)))
        If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, CInt(strAtesakiSyubetu)))
        End If
        paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, strAtesakiCdFrom))
        paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, strAtesakiCdTo))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        If Not strHouhouCd.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 8, CInt(strHouhouCd)))
        End If

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtCSV", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>���ݒ���܂ތ���CSV�������擾����</summary>
    ''' <returns>���ݒ���܂ތ���CSV����</returns>
    Public Function SelMiSeteiGenkaCSVCount(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Long

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("   COUNT_BIG(*) ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			tys_houhou_no ")
            .AppendLine("			,tys_houhou_mei ")
            .AppendLine("		FROM ")
            .AppendLine("			m_tyousahouhou WITH(READCOMMITTED) ")
            .AppendLine("		WHERE ")
            .AppendLine("			ISNULL(genka_settei_fuyou_flg,0) = 0 ") '--�����ݒ�s�v�t���O 
            '���������@=���͂̏ꍇ��
            If Not strHouhouCd.Trim.Equals(String.Empty) Then
                .AppendLine("			AND ")
                .AppendLine("			tys_houhou_no = @tys_houhou_no ")
            End If
            .AppendLine("	) AS SUB_MTH ") '--�T�u�������@M 
            .AppendLine("	CROSS JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			syouhin_cd ")
            .AppendLine("			,syouhin_mei ")
            .AppendLine("		FROM ")
            .AppendLine("			m_syouhin WITH(READCOMMITTED) ")
            .AppendLine("		WHERE ")
            .AppendLine("			souko_cd = '100' ")
            .AppendLine("			AND ")
            .AppendLine("			torikesi = 0 ")
            '�����i�R�[�h=���͂̏ꍇ��
            If Not strSyouhinCd.Trim.Equals(String.Empty) Then
                .AppendLine("			AND ")
                .AppendLine("			syouhin_cd = @syouhin_cd ")
            End If
            .AppendLine("	) AS SUB_MSH ") '--�T�u���iM ")
            .AppendLine("	CROSS JOIN ")
            .AppendLine("	( ")
            If strAtesakiSyubetu.Trim.Equals(String.Empty) Then
                .AppendLine("		SELECT ")
                .AppendLine("			0			AS aitesaki_syubetu ")  '--�������
                .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--�����R�[�h
                .AppendLine("			,'�����Ȃ�'		AS aitesaki_mei ")  '--����於
                .AppendLine("			,0			AS torikesi	 ") '--���")
                .AppendLine("		UNION ALL ")
                .AppendLine("		SELECT ")
                .AppendLine("			1			AS aitesaki_syubetu ")
                .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--�����X�R�[�h
                .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--�����X��1
                .AppendLine("			,MKA.torikesi		AS torikesi ")
                .AppendLine("		FROM ")
                .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--�����X�}�X�^
                '����������͑ΏۊO=�`�F�b�N�̏ꍇ��
                If blnAitesakiTaisyouGai.Equals(True) Then
                    .AppendLine("       WHERE ")
                    .AppendLine("	        MKA.torikesi = 0 ")   '--���
                End If
                .AppendLine("		UNION ALL ")
                .AppendLine("		SELECT ")
                .AppendLine("			3			AS aitesaki_syubetu ")
                .AppendLine("			,'JIO'	AS aitesaki_cd ")
                .AppendLine("			,'JIO��'	AS aitesaki_mei ")
                .AppendLine("			,'0'		AS torikesi ")
                .AppendLine("		UNION ALL ")
                .AppendLine("		SELECT ")
                .AppendLine("			7			        AS aitesaki_syubetu ")
                .AppendLine("			,MKE.keiretu_cd	    AS aitesaki_cd ")   '--�n��R�[�h
                .AppendLine("			,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--�n��
                .AppendLine("			,MIN(MKE.torikesi)		AS torikesi ")
                .AppendLine("		FROM ")
                .AppendLine("			m_keiretu AS MKE WITH(READCOMMITTED) ") '--�n��}�X�^
                '����������͑ΏۊO=�`�F�b�N�̏ꍇ��
                If blnAitesakiTaisyouGai.Equals(True) Then
                    .AppendLine("       WHERE ")
                    .AppendLine("	        MKE.torikesi = 0 ")   '--���
                End If
                .AppendLine("           GROUP BY ")
                .AppendLine("               MKE.keiretu_cd ")
            Else
                Select Case strAtesakiSyubetu
                    Case "0"
                        .AppendLine("		SELECT ")
                        .AppendLine("			0			AS aitesaki_syubetu ")  '--������� 
                        .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--�����R�[�h 
                        .AppendLine("			,'�����Ȃ�'		AS aitesaki_mei ")  '--����於 
                    Case "3"
                        .AppendLine("		SELECT ")
                        .AppendLine("			3			AS aitesaki_syubetu ")
                        .AppendLine("			,'JIO'	AS aitesaki_cd ")
                        .AppendLine("			,'JIO��'	AS aitesaki_mei ")
                    Case "1"
                        .AppendLine("		SELECT ")
                        .AppendLine("			1					AS aitesaki_syubetu ")
                        .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--�����X�R�[�h 
                        .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--�����X��1 
                        .AppendLine("		FROM ")
                        .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--�����X�}�X�^ 
                        .AppendLine("		WHERE ")
                        .AppendLine("           1=1 ")
                        '�������R�[�hFROM�A�����R�[�hTO���������͂���Ă���ꍇ��
                        If (Not strAtesakiCdFrom.Trim.Equals(String.Empty)) AndAlso (Not strAtesakiCdTo.Trim.Equals(String.Empty)) Then
                            .AppendLine("           AND ")
                            .AppendLine("			MKA.kameiten_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                        Else
                            '�������R�[�hFROM�݂̂��邢�́A�����R�[�hTO�����͂���Ă���ꍇ��
                            If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
                                .AppendLine("           AND ")
                                .AppendLine("	        MKA.kameiten_cd = @aitesaki_cd_from ")   '--�����X�R�[�h 
                            Else
                                If Not strAtesakiCdTo.Trim.Equals(String.Empty) Then
                                    .AppendLine("           AND ")
                                    .AppendLine("	        MKA.kameiten_cd = @aitesaki_cd_to ")   '--�����X�R�[�h
                                End If
                            End If
                        End If
                        '����������͑ΏۊO=�`�F�b�N�̏ꍇ��
                        If blnAitesakiTaisyouGai.Equals(True) Then
                            .AppendLine("           AND ")
                            .AppendLine("	        MKA.torikesi = 0 ")   '--���
                        End If
                    Case "7"
                        .AppendLine("			SELECT ")
                        .AppendLine("				7			 AS aitesaki_syubetu ")
                        .AppendLine("				,MKE.keiretu_cd	 AS aitesaki_cd ")  '--�n��R�[�h 
                        .AppendLine("				,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--�n�� 
                        .AppendLine("			FROM ")
                        .AppendLine("				m_keiretu AS MKE WITH(READCOMMITTED) ") '--�n��}�X�^ 
                        .AppendLine(" ")
                        .AppendLine("			WHERE ")
                        .AppendLine("           1=1 ")
                        '�������R�[�hFROM�A�����R�[�hTO���������͂���Ă���ꍇ��
                        If (Not strAtesakiCdFrom.Trim.Equals(String.Empty)) AndAlso (Not strAtesakiCdTo.Trim.Equals(String.Empty)) Then
                            .AppendLine("           AND ")
                            .AppendLine("           MKE.keiretu_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                        Else
                            '�������R�[�hFROM�݂̂��邢�́A�����R�[�hTO�����͂���Ă���ꍇ��
                            If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
                                .AppendLine("           AND ")
                                .AppendLine("	        MKE.keiretu_cd = @aitesaki_cd_from ")   '--�n��R�[�h
                            Else
                                If Not strAtesakiCdTo.Trim.Equals(String.Empty) Then
                                    .AppendLine("           AND ")
                                    .AppendLine("           MKE.keiretu_cd = @aitesaki_cd_to ")   '--�n��R�[�h
                                End If
                            End If
                        End If
                        '����������͑ΏۊO=�`�F�b�N�̏ꍇ��
                        If blnAitesakiTaisyouGai.Equals(True) Then
                            .AppendLine("           AND ")
                            .AppendLine("	        MKE.torikesi = 0 ")   '--���
                        End If
                        .AppendLine("			GROUP BY ")
                        .AppendLine("				MKE.keiretu_cd ")
                End Select
            End If
            .AppendLine("	) AS SUB ")
            .AppendLine("	CROSS JOIN  ")
            .AppendLine("	( ")
            .AppendLine("		SELECT  ")
            .AppendLine("			tys_kaisya_cd ")
            .AppendLine("			,jigyousyo_cd ")
            .AppendLine("			,torikesi ")
            .AppendLine("			,tys_kaisya_mei  ")
            .AppendLine("		FROM  ")
            .AppendLine("			m_tyousakaisya WITH(READCOMMITTED)  ")
            .AppendLine("		WHERE ")
            .AppendLine("           1=1 ")
            '�������ЃR�[�h�
            If Not strTysKaisyaCd.Trim.Equals(String.Empty) Then
                .AppendLine("           AND ")
                .AppendLine("			tys_kaisya_cd = @tys_kaisya_cd ") '--������ЃR�[�h
                .AppendLine("			AND ")
                .AppendLine("			jigyousyo_cd = @jigyousyo_cd ") '--���Ə��R�[�h
            End If
            .AppendLine("	) AS SUB_MTK ") '--�T�u�������M ")
            .AppendLine("	LEFT OUTER JOIN  ")
            .AppendLine("	m_genka as MG WITH(READCOMMITTED)  ")
            .AppendLine("	ON  ")
            .AppendLine("		SUB_MSH.syouhin_cd = MG.syouhin_cd ")   '--�T�u���iM.���i�R�[�h = ����M.���i�R�[�h  ")
            .AppendLine("		AND  ")
            .AppendLine("		SUB_MTH.tys_houhou_no = MG.tys_houhou_no ")   '--�T�u���iM.���i�R�[�h = ����M.���i�R�[�h  ")
            .AppendLine("		AND  ")
            .AppendLine("		SUB.aitesaki_syubetu = MG.aitesaki_syubetu ")   '--�T�u.�������=����M.�������  ")
            .AppendLine("		AND  ")
            .AppendLine("		SUB.aitesaki_cd = MG.aitesaki_cd ") '--�T�u.�����R�[�h=����M.�����R�[�h  ")
            .AppendLine("		AND  ")
            .AppendLine("		MG.tys_kaisya_cd = SUB_MTK.tys_kaisya_cd ") '--����M.������ЃR�[�h = �T�u�������M.������ЃR�[�h  ")
            .AppendLine("		AND  ")
            .AppendLine("		MG.jigyousyo_cd = SUB_MTK.jigyousyo_cd ")   '--����M.���Ə��R�[�h = �T�u�������M.���Ə��R�[�h  ")
        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 4, Left(strTysKaisyaCd, 4)))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, IIf(strTysKaisyaCd.Length > 4, Mid(strTysKaisyaCd, 5, 6), String.Empty)))
        If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, CInt(strAtesakiSyubetu)))
        End If
        paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, strAtesakiCdFrom))
        paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, strAtesakiCdTo))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        If Not strHouhouCd.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 8, CInt(strHouhouCd)))
        End If

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtCSV", paramList.ToArray)

        Return CLng(dsReturn.Tables(0).Rows(0).Item(0))

    End Function


    ''' <summary>�������CSV���擾����</summary>
    ''' <returns>�������CSV�f�[�^�e�[�u��</returns>
    ''' <history>2011/02/28�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelGenkaJyouhouCSV(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	RIGHT(CONVERT(varchar(10),GETDATE(),112),4) + REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') ")
            .AppendLine("	,ISNULL(MG.tys_kaisya_cd,'') ")
            .AppendLine("	,ISNULL(MG.jigyousyo_cd,'') ")
            .AppendLine("	,ISNULL(MTS.tys_kaisya_mei,'') ")
            .AppendLine("	,ISNULL(MG.aitesaki_syubetu,'') ")
            .AppendLine("	,ISNULL(MG.aitesaki_cd,'') ")
            .AppendLine("	,ISNULL(SUB.aitesaki_mei,'') ")
            .AppendLine("	,ISNULL(MG.syouhin_cd,'') ")
            .AppendLine("	,ISNULL(MS.syouhin_mei,'') ")
            .AppendLine("	,ISNULL(MG.tys_houhou_no,'') ")
            .AppendLine("	,ISNULL(MTH.tys_houhou_mei,'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.torikesi),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk1),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg1),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk2),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg2),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk3),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg3),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk4),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg4),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk5),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg5),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk6),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg6),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk7),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg7),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk8),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg8),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk9),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg9),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk10),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg10),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk11t19),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg11t19),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk20t29),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg20t29),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk30t39),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg30t39),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk40t49),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg40t49),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk50t),'') ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MG.tou_kkk_henkou_flg50t),'') ")
            .AppendLine("FROM ")
            .AppendLine("	m_genka AS MG WITH(READCOMMITTED) ")    '--����M
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			0			AS aitesaki_syubetu ")  '--�������
            .AppendLine("			,'ALL'			AS aitesaki_cd ")   '--�����R�[�h
            .AppendLine("			,'�����Ȃ�'		AS aitesaki_mei ")  '--����於
            .AppendLine("			,0			AS torikesi	 ") '--���")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			3			AS aitesaki_syubetu ")
            .AppendLine("			,'JIO'	AS aitesaki_cd ")
            .AppendLine("			,'JIO��'	AS aitesaki_mei ")
            .AppendLine("			,0			AS torikesi	 ") '--���")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			1			AS aitesaki_syubetu ")
            .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--�����X�R�[�h
            .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--�����X��1
            .AppendLine("			,MKA.torikesi		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--�����X�}�X�^
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			7			        AS aitesaki_syubetu ")
            .AppendLine("			,MKE.keiretu_cd	    AS aitesaki_cd ")   '--�n��R�[�h
            .AppendLine("			,MIN(MKE.keiretu_mei)	AS aitesaki_mei ")  '--�n��
            .AppendLine("			,MIN(MKE.torikesi)		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_keiretu AS MKE WITH(READCOMMITTED) ") '--�n��}�X�^
            .AppendLine("           GROUP BY ")
            .AppendLine("               MKE.keiretu_cd ")
            .AppendLine("	) AS SUB ")
            .AppendLine("	ON ")
            .AppendLine("		MG.aitesaki_syubetu = SUB.aitesaki_syubetu ")
            .AppendLine("		AND ")
            .AppendLine("		MG.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_tyousakaisya AS MTS WITH(READCOMMITTED) ")    '--������Ѓ}�X�^
            .AppendLine("	ON ")
            .AppendLine("		MG.tys_kaisya_cd  =  MTS.tys_kaisya_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MG.jigyousyo_cd  =  MTS.jigyousyo_cd ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH(READCOMMITTED) ")  '--���i�}�X�^
            .AppendLine("	ON ")
            .AppendLine("		MG.syouhin_cd  =  MS.syouhin_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MS.souko_cd  =  '100' ")    '--�q�ɃR�[�h
            .AppendLine("		AND ")
            .AppendLine("		MS.torikesi = 0 ")
            .AppendLine("	LEFT JOIN")
            .AppendLine("	m_tyousahouhou AS MTH WITH(READCOMMITTED) ")    '--�������@�}�X�^
            .AppendLine("		ON ")
            .AppendLine("		MG.tys_houhou_no  =  MTH.tys_houhou_no ")   '--�������@NO
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH(READCOMMITTED) ")    '--�g�����̃}�X�^
            .AppendLine("	ON ")
            .AppendLine("		MG.aitesaki_syubetu=MKM.code ") '--����
            .AppendLine("		AND ")
            .AppendLine("		MKM.meisyou_syubetu='22' ") '--���̎��
            .AppendLine("WHERE ")
            '�������ЃR�[�h�
            If Not strTysKaisyaCd.Trim.Equals(String.Empty) Then
                .AppendLine("	MG.tys_kaisya_cd = @tys_kaisya_cd ")   '--������ЃR�[�h
                .AppendLine("	AND")
                .AppendLine("	MG.jigyousyo_cd = @jigyousyo_cd ")   '--���Ə��R�[�h
                .AppendLine("	AND ")
            End If

            If Not strAtesakiSyubetu.Trim.Equals(String.Empty) Then
                .AppendLine("	MG.aitesaki_syubetu = @aitesaki_syubetu ")   '--�������
                .AppendLine("	AND")
                If (Not strAtesakiCdFrom.Trim.Equals(String.Empty)) AndAlso (Not strAtesakiCdTo.Trim.Equals(String.Empty)) Then
                    '�����R�[�hFROM�A�����R�[�hTO���������͂���Ă���ꍇ
                    .AppendLine("	MG.aitesaki_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")   '--�����R�[�h
                    .AppendLine("	AND ")
                Else
                    '�����R�[�hFROM�݂̂��邢�́A�����R�[�hTO�����͂���Ă���ꍇ
                    If Not strAtesakiCdFrom.Trim.Equals(String.Empty) Then
                        .AppendLine("	MG.aitesaki_cd = @aitesaki_cd_from ")   '--�����R�[�h
                        .AppendLine("	AND ")
                    Else
                        If Not strAtesakiCdTo.Trim.Equals(String.Empty) Then
                            .AppendLine("	MG.aitesaki_cd = @aitesaki_cd_to ")   '--�����R�[�h
                            .AppendLine("	AND ")
                        End If
                    End If
                End If
            End If

            If Not strSyouhinCd.Trim.Equals(String.Empty) Then
                '���i�R�[�h=���͂̏ꍇ
                .AppendLine("	MG.syouhin_cd = @syouhin_cd ")   '--���i�R�[�h
                .AppendLine("	AND")
            End If
            If Not strHouhouCd.Equals(String.Empty) Then
                '�������@=���͂̏ꍇ
                .AppendLine("	MG.tys_houhou_no = @tys_houhou_no ")   '--�������@
                .AppendLine("	AND")
            End If
            If blnKensakuTaisyouGai.Equals(True) Then
                '����͌����ΏۊO=�`�F�b�N�̏ꍇ
                .AppendLine("	MG.torikesi = 0 ")   '--���
                .AppendLine("	AND")
            End If
            If blnAitesakiTaisyouGai.Equals(True) Then
                '��������͑ΏۊO=�`�F�b�N�̏ꍇ
                .AppendLine("	SUB.torikesi = 0 ")   '--���
                .AppendLine("	AND")
            End If
            .AppendLine("	1 = 1 ")
            .AppendLine("ORDER BY ")
            .AppendLine("	MG.tys_kaisya_cd ") '--������ЃR�[�h
            .AppendLine("	,MG.jigyousyo_cd ") '--���Ə��R�[�h
            .AppendLine("	,MG.aitesaki_syubetu ") '--�������
            .AppendLine("	,MG.aitesaki_cd ")  '--�����R�[�h
            .AppendLine("	,MG.syouhin_cd ")   '--���i�R�[�h
            .AppendLine("	,MG.tys_houhou_no ")    '--�������@NO
        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 4, Left(strTysKaisyaCd, 4)))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, IIf(strTysKaisyaCd.Length > 4, Mid(strTysKaisyaCd, 5, 6), String.Empty)))
        If Not strAtesakiSyubetu.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, CInt(strAtesakiSyubetu)))
        End If
        paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, strAtesakiCdFrom))
        paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, strAtesakiCdTo))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        If Not strHouhouCd.Trim.Equals(String.Empty) Then
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 8, CInt(strHouhouCd)))
        End If

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtCSV", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>�A�b�v���[�h�Ǘ����擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��f�[�^�e�[�u��</returns>
    ''' <history>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelInputKanri() As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine("SELECT TOP 100 ")
            .AppendLine("	CONVERT(varchar(100),syori_datetime,111) + ' ' + CONVERT(varchar(100),syori_datetime,108) AS syori_datetime ") '��������
            .AppendLine("	,CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') AS torikomi_datetime") '�捞����
            .AppendLine("	,nyuuryoku_file_mei ")  '���̓t�@�C����
            .AppendLine("	,error_umu ")           '�G���[�L��
            .AppendLine("    ,edi_jouhou_sakusei_date ")    'EDI���쐬��
            .AppendLine("FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")       '�A�b�v���[�h�Ǘ��e�[�u��
            .AppendLine("WHERE ")
            .AppendLine("	file_kbn = 2 ")         '�t�@�C���敪
            .AppendLine("ORDER BY ")
            .AppendLine("	syori_datetime DESC ")  '��������(�~��)
        End With

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtInputKanri")

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>�A�b�v���[�h�Ǘ��̌������擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��̌���</returns>
    ''' <history>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelInputKanriCount() As Integer

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(syori_datetime) ")    '����
            .AppendLine("FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")           '�A�b�v���[�h�Ǘ��e�[�u��
            .AppendLine("WHERE ")
            .AppendLine("	file_kbn = 2 ")             '�t�@�C���敪
        End With

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtInputKanri")

        Return CInt(dsReturn.Tables(0).Rows(0).Item(0).ToString)

    End Function

    ''' <summary>������Ж����擾����</summary>
    ''' <returns>������Ж��f�[�^�e�[�u��</returns>
    ''' <history>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTyousaKaisyaMeiInput(ByVal strTyousaKaisyaCd As String, ByVal strJigyousyoCd As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	tys_kaisya_mei ")
            .AppendLine("FROM  ")
            .AppendLine("	m_tyousakaisya WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_kaisya_cd = @tys_kaisya_cd ")
            .AppendLine("	AND ")
            .AppendLine("	jigyousyo_cd = @jigyousyo_cd ")
        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTyousaKaisyaCd))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTyousaKaisyaMei", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>������ʂ��擾����</summary>
    ''' <history>2011/03/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
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
            .AppendLine("			3			AS aitesaki_syubetu ")
            .AppendLine("			,'JIO'	AS aitesaki_cd ")
            .AppendLine("			,'JIO��'	AS aitesaki_mei ")
            .AppendLine("			,0			AS torikesi	 ") '--���")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			1					AS aitesaki_syubetu ")
            .AppendLine("			,MKA.kameiten_cd	AS aitesaki_cd ")   '--�����X�R�[�h
            .AppendLine("			,MKA.kameiten_mei1	AS aitesaki_mei ")  '--�����X��1
            .AppendLine("			,MKA.torikesi		AS torikesi ")
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten AS MKA WITH(READCOMMITTED) ")    '--�����X�}�X�^
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
    ''' <history>2011/03/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelSyouhinCdInput(ByVal strSyouhinCd As String) As Boolean

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
    ''' <history>2011/03/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTyousahouhouNoInput(ByVal intTyousahouhouNo As Integer) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	tys_houhou_no ")
            .AppendLine("FROM  ")
            .AppendLine("	m_tyousahouhou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_houhou_no = @TyousahouhouNo ")
        End With

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

    ''' <summary>�����f�[�^���擾����</summary>
    ''' <returns>�����f�[�^�e�[�u��</returns>
    ''' <history>2011/03/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelGenkaInputJyouhou(ByVal strTyousaKaisyaCd As String, ByVal strJigyousyoCd As String, ByVal intAitesakiSyubetu As Integer, ByVal strAitesakiCd As String, ByVal strSyouhinCd As String, ByVal strTyousaHouhouNo As Integer) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  DISTINCT ")
            .AppendLine("	tys_kaisya_cd ")
            .AppendLine("FROM ")
            .AppendLine("	m_genka WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_kaisya_cd = @tys_kaisya_cd ")
            .AppendLine("	AND ")
            .AppendLine("	jigyousyo_cd = @jigyousyo_cd ")
            .AppendLine("	AND ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine("	AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
            .AppendLine("	AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine("	AND ")
            .AppendLine("	tys_houhou_no = @tys_houhou_no ")
        End With

        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTyousaKaisyaCd))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))
        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, intAitesakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAitesakiCd))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 8, strTyousaHouhouNo))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtGenkaJyouhou", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function


    ''' <summary>�����G���[���e�e�[�u����o�^����</summary>
    ''' <history>2011/03/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function InsGenkaError(ByVal dtError As Data.DataTable) As Boolean
        '�߂�l
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("INSERT INTO  ")
            .AppendLine("	m_genka_error WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		edi_jouhou_sakusei_date ")
            .AppendLine("		,gyou_no ")
            .AppendLine("		,syori_datetime ")
            .AppendLine("		,tys_kaisya_cd ")
            .AppendLine("		,jigyousyo_cd ")
            .AppendLine("		,tys_kaisya_mei ")
            .AppendLine("		,aitesaki_syubetu ")
            .AppendLine("		,aitesaki_cd ")
            .AppendLine("		,aitesaki_mei ")
            .AppendLine("		,syouhin_cd ")
            .AppendLine("		,syouhin_mei ")
            .AppendLine("		,tys_houhou_no ")
            .AppendLine("		,tys_houhou ")
            .AppendLine("		,torikesi ")
            .AppendLine("		,tou_kkk1 ")
            .AppendLine("		,tou_kkk_henkou_flg1 ")
            .AppendLine("		,tou_kkk2 ")
            .AppendLine("		,tou_kkk_henkou_flg2 ")
            .AppendLine("		,tou_kkk3 ")
            .AppendLine("		,tou_kkk_henkou_flg3 ")
            .AppendLine("		,tou_kkk4 ")
            .AppendLine("		,tou_kkk_henkou_flg4 ")
            .AppendLine("		,tou_kkk5 ")
            .AppendLine("		,tou_kkk_henkou_flg5 ")
            .AppendLine("		,tou_kkk6 ")
            .AppendLine("		,tou_kkk_henkou_flg6 ")
            .AppendLine("		,tou_kkk7 ")
            .AppendLine("		,tou_kkk_henkou_flg7 ")
            .AppendLine("		,tou_kkk8 ")
            .AppendLine("		,tou_kkk_henkou_flg8 ")
            .AppendLine("		,tou_kkk9 ")
            .AppendLine("		,tou_kkk_henkou_flg9 ")
            .AppendLine("		,tou_kkk10 ")
            .AppendLine("		,tou_kkk_henkou_flg10 ")
            .AppendLine("		,tou_kkk11t19 ")
            .AppendLine("		,tou_kkk_henkou_flg11t19 ")
            .AppendLine("		,tou_kkk20t29 ")
            .AppendLine("		,tou_kkk_henkou_flg20t29 ")
            .AppendLine("		,tou_kkk30t39 ")
            .AppendLine("		,tou_kkk_henkou_flg30t39 ")
            .AppendLine("		,tou_kkk40t49 ")
            .AppendLine("		,tou_kkk_henkou_flg40t49 ")
            .AppendLine("		,tou_kkk50t ")
            .AppendLine("		,tou_kkk_henkou_flg50t ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@edi_jouhou_sakusei_date ")
            .AppendLine("	,@gyou_no ")
            .AppendLine("	,@syori_datetime ")
            .AppendLine("	,@tys_kaisya_cd ")
            .AppendLine("	,@jigyousyo_cd ")
            .AppendLine("	,@tys_kaisya_mei ")
            .AppendLine("	,@aitesaki_syubetu ")
            .AppendLine("	,@aitesaki_cd ")
            .AppendLine("	,@aitesaki_mei ")
            .AppendLine("	,@syouhin_cd ")
            .AppendLine("	,@syouhin_mei ")
            .AppendLine("	,@tys_houhou_no ")
            .AppendLine("	,@tys_houhou ")
            .AppendLine("	,@torikesi ")
            .AppendLine("	,@tou_kkk1 ")
            .AppendLine("	,@tou_kkk_henkou_flg1 ")
            .AppendLine("	,@tou_kkk2 ")
            .AppendLine("	,@tou_kkk_henkou_flg2 ")
            .AppendLine("	,@tou_kkk3 ")
            .AppendLine("	,@tou_kkk_henkou_flg3 ")
            .AppendLine("	,@tou_kkk4 ")
            .AppendLine("	,@tou_kkk_henkou_flg4 ")
            .AppendLine("	,@tou_kkk5 ")
            .AppendLine("	,@tou_kkk_henkou_flg5 ")
            .AppendLine("	,@tou_kkk6 ")
            .AppendLine("	,@tou_kkk_henkou_flg6 ")
            .AppendLine("	,@tou_kkk7 ")
            .AppendLine("	,@tou_kkk_henkou_flg7 ")
            .AppendLine("	,@tou_kkk8 ")
            .AppendLine("	,@tou_kkk_henkou_flg8 ")
            .AppendLine("	,@tou_kkk9 ")
            .AppendLine("	,@tou_kkk_henkou_flg9 ")
            .AppendLine("	,@tou_kkk10 ")
            .AppendLine("	,@tou_kkk_henkou_flg10 ")
            .AppendLine("	,@tou_kkk11t19 ")
            .AppendLine("	,@tou_kkk_henkou_flg11t19 ")
            .AppendLine("	,@tou_kkk20t29 ")
            .AppendLine("	,@tou_kkk_henkou_flg20t29 ")
            .AppendLine("	,@tou_kkk30t39 ")
            .AppendLine("	,@tou_kkk_henkou_flg30t39 ")
            .AppendLine("	,@tou_kkk40t49 ")
            .AppendLine("	,@tou_kkk_henkou_flg40t49 ")
            .AppendLine("	,@tou_kkk50t ")
            .AppendLine("	,@tou_kkk_henkou_flg50t ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(") ")
        End With

        For i As Integer = 0 To dtError.Rows.Count - 1

            '�p�����[�^�̐ݒ� 
            paramList.Clear()
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, dtError.Rows(i).Item(0).ToString.Trim))
            paramList.Add(MakeParam("@gyou_no", SqlDbType.Int, 8, CInt(dtError.Rows(i).Item(42).Trim)))
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(dtError.Rows(i).Item(43).ToString.Trim))))
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, dtError.Rows(i).Item(1).ToString.Trim))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, dtError.Rows(i).Item(2).ToString.Trim))
            paramList.Add(MakeParam("@tys_kaisya_mei", SqlDbType.VarChar, 40, dtError.Rows(i).Item(3).ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.VarChar, 5, dtError.Rows(i).Item(4).ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtError.Rows(i).Item(5).ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_mei", SqlDbType.VarChar, 40, dtError.Rows(i).Item(6).ToString.Trim))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtError.Rows(i).Item(7).ToString.Trim))
            paramList.Add(MakeParam("@syouhin_mei", SqlDbType.VarChar, 40, dtError.Rows(i).Item(8).ToString.Trim))
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.VarChar, 5, dtError.Rows(i).Item(9).ToString.Trim))
            paramList.Add(MakeParam("@tys_houhou", SqlDbType.VarChar, 32, dtError.Rows(i).Item(10).ToString.Trim))
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, dtError.Rows(i).Item(11).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk1", SqlDbType.VarChar, 10, dtError.Rows(i).Item(12).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg1", SqlDbType.VarChar, 1, dtError.Rows(i).Item(13).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk2", SqlDbType.VarChar, 10, dtError.Rows(i).Item(14).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg2", SqlDbType.VarChar, 1, dtError.Rows(i).Item(15).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk3", SqlDbType.VarChar, 10, dtError.Rows(i).Item(16).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg3", SqlDbType.VarChar, 1, dtError.Rows(i).Item(17).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk4", SqlDbType.VarChar, 10, dtError.Rows(i).Item(18).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg4", SqlDbType.VarChar, 1, dtError.Rows(i).Item(19).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk5", SqlDbType.VarChar, 10, dtError.Rows(i).Item(20).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg5", SqlDbType.VarChar, 1, dtError.Rows(i).Item(21).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk6", SqlDbType.VarChar, 10, dtError.Rows(i).Item(22).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg6", SqlDbType.VarChar, 1, dtError.Rows(i).Item(23).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk7", SqlDbType.VarChar, 10, dtError.Rows(i).Item(24).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg7", SqlDbType.VarChar, 1, dtError.Rows(i).Item(25).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk8", SqlDbType.VarChar, 10, dtError.Rows(i).Item(26).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg8", SqlDbType.VarChar, 1, dtError.Rows(i).Item(27).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk9", SqlDbType.VarChar, 10, dtError.Rows(i).Item(28).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg9", SqlDbType.VarChar, 1, dtError.Rows(i).Item(29).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk10", SqlDbType.VarChar, 10, dtError.Rows(i).Item(30).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg10", SqlDbType.VarChar, 1, dtError.Rows(i).Item(31).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk11t19", SqlDbType.VarChar, 10, dtError.Rows(i).Item(32).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg11t19", SqlDbType.VarChar, 1, dtError.Rows(i).Item(33).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk20t29", SqlDbType.VarChar, 10, dtError.Rows(i).Item(34).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg20t29", SqlDbType.VarChar, 1, dtError.Rows(i).Item(35).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk30t39", SqlDbType.VarChar, 10, dtError.Rows(i).Item(36).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg30t39", SqlDbType.VarChar, 1, dtError.Rows(i).Item(37).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk40t49", SqlDbType.VarChar, 10, dtError.Rows(i).Item(38).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg40t49", SqlDbType.VarChar, 1, dtError.Rows(i).Item(39).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk50t", SqlDbType.VarChar, 10, dtError.Rows(i).Item(40).ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg50t", SqlDbType.VarChar, 1, dtError.Rows(i).Item(41).ToString.Trim))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dtError.Rows(i).Item(44).ToString.Trim))

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

    ''' <summary>�����}�X�^��o�^or�X�V����</summary>
    ''' <history>2011/03/03�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function InsUpdGenkaMaster(ByVal dtOk As Data.DataTable) As Boolean
        '�߂�l
        Dim InsUpdCount As Integer = 0
        '�ǉ��psql��
        Dim strSqlIns As New System.Text.StringBuilder
        '�X�V�psql��
        Dim strSqlUpd As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�ǉ��psql��
        With strSqlIns
            .AppendLine("INSERT INTO  ")
            .AppendLine("	m_genka WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		tys_kaisya_cd ")
            .AppendLine("		,jigyousyo_cd ")
            .AppendLine("		,aitesaki_syubetu ")
            .AppendLine("		,aitesaki_cd ")
            .AppendLine("		,syouhin_cd ")
            .AppendLine("		,tys_houhou_no ")
            .AppendLine("		,torikesi ")
            .AppendLine("		,tou_kkk1 ")
            .AppendLine("		,tou_kkk_henkou_flg1 ")
            .AppendLine("		,tou_kkk2 ")
            .AppendLine("		,tou_kkk_henkou_flg2 ")
            .AppendLine("		,tou_kkk3 ")
            .AppendLine("		,tou_kkk_henkou_flg3 ")
            .AppendLine("		,tou_kkk4 ")
            .AppendLine("		,tou_kkk_henkou_flg4 ")
            .AppendLine("		,tou_kkk5 ")
            .AppendLine("		,tou_kkk_henkou_flg5 ")
            .AppendLine("		,tou_kkk6 ")
            .AppendLine("		,tou_kkk_henkou_flg6 ")
            .AppendLine("		,tou_kkk7 ")
            .AppendLine("		,tou_kkk_henkou_flg7 ")
            .AppendLine("		,tou_kkk8 ")
            .AppendLine("		,tou_kkk_henkou_flg8 ")
            .AppendLine("		,tou_kkk9 ")
            .AppendLine("		,tou_kkk_henkou_flg9 ")
            .AppendLine("		,tou_kkk10 ")
            .AppendLine("		,tou_kkk_henkou_flg10 ")
            .AppendLine("		,tou_kkk11t19 ")
            .AppendLine("		,tou_kkk_henkou_flg11t19 ")
            .AppendLine("		,tou_kkk20t29 ")
            .AppendLine("		,tou_kkk_henkou_flg20t29 ")
            .AppendLine("		,tou_kkk30t39 ")
            .AppendLine("		,tou_kkk_henkou_flg30t39 ")
            .AppendLine("		,tou_kkk40t49 ")
            .AppendLine("		,tou_kkk_henkou_flg40t49 ")
            .AppendLine("		,tou_kkk50t ")
            .AppendLine("		,tou_kkk_henkou_flg50t ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@tys_kaisya_cd ")
            .AppendLine("	,@jigyousyo_cd ")
            .AppendLine("	,@aitesaki_syubetu ")
            .AppendLine("	,@aitesaki_cd ")
            .AppendLine("	,@syouhin_cd ")
            .AppendLine("	,@tys_houhou_no ")
            .AppendLine("	,@torikesi ")
            .AppendLine("	,@tou_kkk1 ")
            .AppendLine("	,@tou_kkk_henkou_flg1 ")
            .AppendLine("	,@tou_kkk2 ")
            .AppendLine("	,@tou_kkk_henkou_flg2 ")
            .AppendLine("	,@tou_kkk3 ")
            .AppendLine("	,@tou_kkk_henkou_flg3 ")
            .AppendLine("	,@tou_kkk4 ")
            .AppendLine("	,@tou_kkk_henkou_flg4 ")
            .AppendLine("	,@tou_kkk5 ")
            .AppendLine("	,@tou_kkk_henkou_flg5 ")
            .AppendLine("	,@tou_kkk6 ")
            .AppendLine("	,@tou_kkk_henkou_flg6 ")
            .AppendLine("	,@tou_kkk7 ")
            .AppendLine("	,@tou_kkk_henkou_flg7 ")
            .AppendLine("	,@tou_kkk8 ")
            .AppendLine("	,@tou_kkk_henkou_flg8 ")
            .AppendLine("	,@tou_kkk9 ")
            .AppendLine("	,@tou_kkk_henkou_flg9 ")
            .AppendLine("	,@tou_kkk10 ")
            .AppendLine("	,@tou_kkk_henkou_flg10 ")
            .AppendLine("	,@tou_kkk11t19 ")
            .AppendLine("	,@tou_kkk_henkou_flg11t19 ")
            .AppendLine("	,@tou_kkk20t29 ")
            .AppendLine("	,@tou_kkk_henkou_flg20t29 ")
            .AppendLine("	,@tou_kkk30t39 ")
            .AppendLine("	,@tou_kkk_henkou_flg30t39 ")
            .AppendLine("	,@tou_kkk40t49 ")
            .AppendLine("	,@tou_kkk_henkou_flg40t49 ")
            .AppendLine("	,@tou_kkk50t ")
            .AppendLine("	,@tou_kkk_henkou_flg50t ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(") ")
        End With

        '�X�V�psql��
        With StrSqlUpd
            .AppendLine("UPDATE  ")
            .AppendLine("	m_genka WITH(UPDLOCK) ")
            .AppendLine("SET  ")
            .AppendLine("	torikesi = @torikesi ")
            .AppendLine("	,tou_kkk1 = @tou_kkk1 ")
            .AppendLine("	,tou_kkk_henkou_flg1 = @tou_kkk_henkou_flg1 ")
            .AppendLine("	,tou_kkk2 = @tou_kkk2 ")
            .AppendLine("	,tou_kkk_henkou_flg2 = @tou_kkk_henkou_flg2 ")
            .AppendLine("	,tou_kkk3 = @tou_kkk3 ")
            .AppendLine("	,tou_kkk_henkou_flg3 = @tou_kkk_henkou_flg3 ")
            .AppendLine("	,tou_kkk4 = @tou_kkk4 ")
            .AppendLine("	,tou_kkk_henkou_flg4 = @tou_kkk_henkou_flg4 ")
            .AppendLine("	,tou_kkk5 = @tou_kkk5 ")
            .AppendLine("	,tou_kkk_henkou_flg5 = @tou_kkk_henkou_flg5 ")
            .AppendLine("	,tou_kkk6 = @tou_kkk6 ")
            .AppendLine("	,tou_kkk_henkou_flg6 = @tou_kkk_henkou_flg6 ")
            .AppendLine("	,tou_kkk7 = @tou_kkk7 ")
            .AppendLine("	,tou_kkk_henkou_flg7 = @tou_kkk_henkou_flg7 ")
            .AppendLine("	,tou_kkk8 = @tou_kkk8 ")
            .AppendLine("	,tou_kkk_henkou_flg8 = @tou_kkk_henkou_flg8 ")
            .AppendLine("	,tou_kkk9 = @tou_kkk9 ")
            .AppendLine("	,tou_kkk_henkou_flg9 = @tou_kkk_henkou_flg9 ")
            .AppendLine("	,tou_kkk10 = @tou_kkk10 ")
            .AppendLine("	,tou_kkk_henkou_flg10 = @tou_kkk_henkou_flg10 ")
            .AppendLine("	,tou_kkk11t19 = @tou_kkk11t19 ")
            .AppendLine("	,tou_kkk_henkou_flg11t19 = @tou_kkk_henkou_flg11t19 ")
            .AppendLine("	,tou_kkk20t29 = @tou_kkk20t29 ")
            .AppendLine("	,tou_kkk_henkou_flg20t29 = @tou_kkk_henkou_flg20t29 ")
            .AppendLine("	,tou_kkk30t39 = @tou_kkk30t39 ")
            .AppendLine("	,tou_kkk_henkou_flg30t39 = @tou_kkk_henkou_flg30t39 ")
            .AppendLine("	,tou_kkk40t49 = @tou_kkk40t49 ")
            .AppendLine("	,tou_kkk_henkou_flg40t49 = @tou_kkk_henkou_flg40t49 ")
            .AppendLine("	,tou_kkk50t = @tou_kkk50t ")
            .AppendLine("	,tou_kkk_henkou_flg50t = @tou_kkk_henkou_flg50t ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
            .AppendLine("	,upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_kaisya_cd = @tys_kaisya_cd ")
            .AppendLine("	AND ")
            .AppendLine("	jigyousyo_cd = @jigyousyo_cd ")
            .AppendLine("	AND ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine("	AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
            .AppendLine("	AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine("	AND ")
            .AppendLine("	tys_houhou_no = @tys_houhou_no ")
        End With

        For i As Integer = 0 To dtOk.Rows.Count - 1
            '�p�����[�^�̐ݒ�
            paramList.Clear()
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, dtOk.Rows(i).Item("tys_kaisya_cd").ToString.Trim))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, dtOk.Rows(i).Item("jigyousyo_cd").ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 8, dtOk.Rows(i).Item("aitesaki_syubetu").ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtOk.Rows(i).Item("aitesaki_cd").ToString.Trim))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtOk.Rows(i).Item("syouhin_cd").ToString.Trim))
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 10, dtOk.Rows(i).Item("tys_houhou_no").ToString.Trim))
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, dtOk.Rows(i).Item("torikesi").ToString.Trim))
            paramList.Add(MakeParam("@tou_kkk1", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk1").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk1").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg1", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg1").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg1").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk2", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk2").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk2").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg2", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg2").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg2").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk3", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk3").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk3").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg3", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg3").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg3").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk4", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk4").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk4").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg4", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg4").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg4").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk5", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk5").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk5").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg5", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg5").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg5").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk6", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk6").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk6").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg6", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg6").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg6").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk7", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk7").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk7").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg7", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg7").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg7").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk8", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk8").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk8").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg8", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg8").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg8").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk9", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk9").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk9").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg9", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg9").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg9").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk10", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk10").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk10").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg10", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg10").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg10").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk11t19", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk11t19").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk11t19").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg11t19", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg11t19").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg11t19").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk20t29", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk20t29").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk20t29").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg20t29", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg20t29").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg20t29").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk30t39", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk30t39").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk30t39").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg30t39", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg30t39").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg30t39").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk40t49", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk40t49").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk40t49").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg40t49", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg40t49").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg40t49").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk50t", SqlDbType.Int, 10, IIf(dtOk.Rows(i).Item("tou_kkk50t").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk50t").ToString.Trim)))
            paramList.Add(MakeParam("@tou_kkk_henkou_flg50t", SqlDbType.Int, 1, IIf(dtOk.Rows(i).Item("tou_kkk_henkou_flg50t").ToString.Trim.Equals(String.Empty), DBNull.Value, dtOk.Rows(i).Item("tou_kkk_henkou_flg50t").ToString.Trim)))

            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dtOk.Rows(i).Item("add_login_user_id").ToString.Trim))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim))
            '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������

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

    ''' <summary>�A�b�v���[�h�Ǘ��e�[�u����o�^����</summary>
    ''' <history>2011/03/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
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
            .AppendLine("	,2 ")
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

    ''' <summary>�����G���[�����擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <returns>�����G���[�f�[�^�e�[�u��</returns>
    Public Function SelGenkaErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) As Data.DataTable
        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT   ")
            .AppendLine("	TOP 100   ")
            .AppendLine("	MGE.gyou_no ")
            .AppendLine("	,ISNULL(MGE.tys_kaisya_cd,'') + ISNULL(MGE.jigyousyo_cd,'') AS tys_kaisya_cd ")
            .AppendLine("	,MGE.tys_kaisya_mei  ")
            .AppendLine("   ,case ")
            .AppendLine("       WHEN (ISNULL(MGE.aitesaki_syubetu,'') + '�F' + ISNULL(MKM.meisyou,'')) = '�F' THEN ")
            .AppendLine("           '' ")
            .AppendLine("       ELSE ")
            .AppendLine("	        (ISNULL(MGE.aitesaki_syubetu,'') + '�F' + ISNULL(MKM.meisyou,'')) ")
            .AppendLine("       END AS aitesaki_syubetu ")
            .AppendLine("	,MGE.aitesaki_cd  ")
            .AppendLine("	,MGE.aitesaki_mei  ")
            .AppendLine("	,MGE.syouhin_cd  ")
            .AppendLine("	,MGE.syouhin_mei  ")
            .AppendLine("   ,case ")
            .AppendLine("       WHEN (ISNULL(MGE.tys_houhou_no,'') + '�F' + ISNULL(MGE.tys_houhou,'')) = '�F' THEN ")
            .AppendLine("           '' ")
            .AppendLine("       ELSE ")
            .AppendLine("	        (ISNULL(MGE.tys_houhou_no,'') + '�F' + ISNULL(MGE.tys_houhou,'')) ")
            .AppendLine("       END AS tys_houhou ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN MGE.torikesi = '0' THEN ")
            .AppendLine("			'' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'���' ")
            .AppendLine("		END AS torikesi ")  '--���
            .AppendLine("	,MGE.tou_kkk1 ")   '--�����i1
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg1,'0') = '0' THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg1 ")   '--�����i�ύXFLG1
            .AppendLine("	,MGE.tou_kkk2 ")   '--�����i2
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg2,'0') = '0' THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg2 ")   '--�����i�ύXFLG2
            .AppendLine("	,MGE.tou_kkk3 ")   '--�����i3
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg3,'0') = '0' THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg3 ")   '--�����i�ύXFLG3
            .AppendLine("	,MGE.tou_kkk4 ")   '--�����i4
            .AppendLine("	,CASE")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg4,'0') = '0' THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg4 ")   '--�����i�ύXFLG4
            .AppendLine("	,MGE.tou_kkk5 ")   '--�����i5
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg5,'0') = '0' THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg5 ")   '--�����i�ύXFLG5
            .AppendLine("	,MGE.tou_kkk6 ")   '--�����i6
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg6,'0') = '0' THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg6 ")   '--�����i�ύXFLG6
            .AppendLine("	,MGE.tou_kkk7 ")   '--�����i7
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg7,'0') = '0' THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg7 ")   '--�����i�ύXFLG7
            .AppendLine("	,MGE.tou_kkk8 ")   '--�����i8
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg8,'0') = '0' THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg8 ")   '--�����i�ύXFLG8
            .AppendLine("	,MGE.tou_kkk9 ")   '--�����i9
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg9,'0') = '0' THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg9 ")   '--�����i�ύXFLG9
            .AppendLine("	,MGE.tou_kkk10 ") '--�����i10
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg10,'0') = '0' THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg10 ")  '--�����i�ύXFLG10
            .AppendLine("	,MGE.tou_kkk11t19 ")   '--�����i11�`19
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg11t19,'0') = '0' THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg11t19 ")   '--�����i�ύXFLG11�`19
            .AppendLine("	,MGE.tou_kkk20t29 ")   '--�����i20�`29
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg20t29,'0') = '0' THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg20t29 ")   '--�����i�ύXFLG20�`29
            .AppendLine("	,MGE.tou_kkk30t39 ")   '--�����i30�`39
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg30t39,'0') = '0' THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg30t39 ")   '--�����i�ύXFLG30�`39
            .AppendLine("	,MGE.tou_kkk40t49 ")   '--�����i40�`49
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg40t49,'0') = '0' THEN ")
            .AppendLine("			'�ύX�s��'")
            .AppendLine("		ELSE")
            .AppendLine("			'�ύX��'")
            .AppendLine("		END AS tou_kkk_henkou_flg40t49 ")   '--�����i�ύXFLG40�`49
            .AppendLine("	,MGE.tou_kkk50t ")   '--�����i50�`
            .AppendLine("	,CASE")
            .AppendLine("		WHEN ISNULL(MGE.tou_kkk_henkou_flg50t,'0') = '0' THEN ")
            .AppendLine("			'�ύX�s��' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'�ύX��' ")
            .AppendLine("		END AS tou_kkk_henkou_flg50t ") '--�����i�ύXFLG50�`
            .AppendLine("FROM  ")
            .AppendLine("	m_genka_error AS MGE WITH(READCOMMITTED)  ")
            .AppendLine("	LEFT OUTER JOIN  ")
            .AppendLine("	m_kakutyou_meisyou MKM WITH(READCOMMITTED)  ")
            .AppendLine("	ON ")
            .AppendLine("		MGE.aitesaki_syubetu = MKM.code  ")
            .AppendLine("		AND ")
            .AppendLine("		MKM.meisyou_syubetu = 22  ")
            .AppendLine("WHERE ")
            .AppendLine("	MGE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            '��������
            .AppendLine(" AND CONVERT(varchar(100),MGE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MGE.syori_datetime,114),':','') = @syori_datetime")
            .AppendLine("ORDER BY ")
            .AppendLine("	MGE.gyou_no ")
        End With

        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtGenkaErr", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>�����G���[�������擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <returns>�����G���[����</returns>
    Public Function SelGenkaErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) As String

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT(*) AS count ")
            .AppendLine("FROM  ")
            .AppendLine("	m_genka_error AS MGE WITH(READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("	MGE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            '��������
            .AppendLine(" AND CONVERT(varchar(100),MGE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MGE.syori_datetime,114),':','') = @syori_datetime")
        End With

        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtHanbaiKakakuErr", paramList.ToArray)

        Return CInt(dsReturn.Tables(0).Rows(0).Item(0))

    End Function

    ''' <summary>�����G���[CSV�����擾����</summary>
    ''' <returns>�����G���[CSV�f�[�^�e�[�u��</returns>
    Public Function SelGenkaErrCsv(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT   ")
            .AppendLine("	TOP 5000   ")
            .AppendLine("	MGE.edi_jouhou_sakusei_date ")
            .AppendLine("	,MGE.gyou_no ")
            .AppendLine("   ,MGE.syori_datetime ")
            .AppendLine("	,MGE.tys_kaisya_cd ")
            .AppendLine("	,MGE.jigyousyo_cd ")
            .AppendLine("	,MGE.tys_kaisya_mei ")
            .AppendLine("	,MGE.aitesaki_syubetu ")
            .AppendLine("	,MGE.aitesaki_cd  ")
            .AppendLine("	,MGE.aitesaki_mei  ")
            .AppendLine("	,MGE.syouhin_cd  ")
            .AppendLine("	,MGE.syouhin_mei  ")
            .AppendLine("	,MGE.tys_houhou_no  ")
            .AppendLine("	,MGE.tys_houhou  ")
            .AppendLine("	,MGE.torikesi  ")
            .AppendLine("	,MGE.tou_kkk1  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg1  ")
            .AppendLine("	,MGE.tou_kkk2  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg2  ")
            .AppendLine("	,MGE.tou_kkk3  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg3  ")
            .AppendLine("	,MGE.tou_kkk4  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg4  ")
            .AppendLine("	,MGE.tou_kkk5  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg5  ")
            .AppendLine("	,MGE.tou_kkk6  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg6  ")
            .AppendLine("	,MGE.tou_kkk7  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg7  ")
            .AppendLine("	,MGE.tou_kkk8  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg8  ")
            .AppendLine("	,MGE.tou_kkk9  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg9  ")
            .AppendLine("	,MGE.tou_kkk10  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg10  ")
            .AppendLine("	,MGE.tou_kkk11t19  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg11t19  ")
            .AppendLine("	,MGE.tou_kkk20t29  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg20t29  ")
            .AppendLine("	,MGE.tou_kkk30t39  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg30t39  ")
            .AppendLine("	,MGE.tou_kkk40t49  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg40t49  ")
            .AppendLine("	,MGE.tou_kkk50t  ")
            .AppendLine("	,MGE.tou_kkk_henkou_flg50t  ")
            .AppendLine("	,MGE.add_login_user_id  ")
            .AppendLine("	,MGE.add_datetime  ")
            .AppendLine("	,MGE.upd_login_user_id  ")
            .AppendLine("	,MGE.upd_datetime  ")
            .AppendLine("FROM  ")
            .AppendLine("	m_genka_error AS MGE WITH(READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("	MGE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            '��������
            .AppendLine(" AND CONVERT(varchar(100),MGE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MGE.syori_datetime,114),':','') = @syori_datetime")
            .AppendLine("ORDER BY ")
            .AppendLine("	MGE.gyou_no ")
        End With

        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtGenkaErrCount", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

End Class
