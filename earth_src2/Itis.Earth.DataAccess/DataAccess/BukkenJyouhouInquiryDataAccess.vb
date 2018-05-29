Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
''' <summary>�����X�������Ɖ��</summary>
''' <remarks>�����X�������Ɖ�@�\��񋟂���</remarks>
''' <history>
''' <para>2009/07/15�@�n���R(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class BukkenJyouhouInquiryDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary> �������擾</summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>�������̃f�[�^</returns>
    Public Function SelBukkenJyouhouInfo(ByVal strKameitenCd As String) As BukkenJyouhouInquiryDataSet.BukkenJyouhouInquiryDataTableDataTable

        ' DataSet�C���X�^���X�̐���()
        Dim dsBukkenJyouhouInquiryDataSet As New BukkenJyouhouInquiryDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	MK.kameiten_cd, ")                              '�����X����	
            .AppendLine("	MK.kameiten_mei1, ")                            '�����X��1	
            .AppendLine("	MK.tenmei_kana1, ")                             '�X����1	
            .AppendLine("	(MJM.DisplayName) AS eigyou_tantousya_mei, ")                     '�c�ƒS����	
            .AppendLine("	(TJ.kbn + TJ.hosyousyo_no) AS hosyousyo_no, ")  '�敪�ƕۏ؏�NO	
            .AppendLine("	TJ.sesyu_mei, ")                                '�{�喼	
            .AppendLine("	(ISNULL(TJ.bukken_jyuusyo1,'') + ISNULL(TJ.bukken_jyuusyo2,'') + ISNULL(TJ.bukken_jyuusyo3,'')) AS bukken_jyuusyo, ")    '�����Z��123	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.irai_date,111) AS irai_date, ")                                '�˗���	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.syoudakusyo_tys_date,111) AS syoudakusyo_tys_date, ")                     '������������	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.tys_jissi_date,111) AS tys_jissi_date, ")                           '�������{��	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.keikakusyo_sakusei_date,111) AS keikakusyo_sakusei_date, ")                  '�v�揑�쐬��	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.tys_hkks_hak_date,111) AS tys_hkks_hak_date, ")                        '�����񍐏�������	
            .AppendLine("	MT1.tantousya_mei, ")                           '�S���Җ�	
            .AppendLine("	MKS.ks_siyou, ")                                '��b�d�l	
            .AppendLine("	kouhou = ")
            .AppendLine("	CASE WHEN koj_hantei_flg = '0' THEN '�H���Ȃ�' ")
            .AppendLine("	ELSE MKKS.kairy_koj_syubetu ")
            .AppendLine("	END, ")                                         '���ǍH�����	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.kairy_koj_kanry_yotei_date,111) AS kairy_koj_kanry_yotei_date, ")               '���ǍH�������\���	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.kairy_koj_date,111) AS kairy_koj_date, ")                           '���ǍH����	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.kairy_koj_sokuhou_tyk_date,111) AS kairy_koj_sokuhou_tyk_date, ")               '���ǍH�����H���񒅓�	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.koj_hkks_hassou_date,111) AS koj_hkks_hassou_date, ")                     'K17�񍐏�������	
            .AppendLine("	MT2.tantousya_mei AS kouji_tantousya_mei, ")    '�S���Җ�	
            .AppendLine("	MHHJ.hosyousyo_hak_jyky, ")                     '�ۏ؏����s��	
            .AppendLine("	CONVERT(VARCHAR(12),TJ.hosyousyo_hak_date,111) AS hosyousyo_hak_date, ")                       '�ۏ؏����s��	


            'isnull(TTS.uri_gaku,0)+isnull(TTS.syouhizei_gaku,0)

            '.AppendLine("	TBL1.uriagegaku, ")                             '����z	
            .AppendLine("	isnull(TTS2.uri_gaku,0)+isnull(TTS2.syouhizei_gaku,0) as uriagegaku, ")

            .AppendLine("	TBL2.nyukingaku, ")                             '�����z
            .AppendLine("	(ISNULL(TJ.tys_kaisya_cd,'') + ISNULL(TJ.tys_kaisya_jigyousyo_cd,'')) AS tys_kaisya_cd, ")     '������ЃR�[�h
            .AppendLine("	MTK1.tys_kaisya_mei AS tys_kaisya_mei, ")                          '������Ж�
            .AppendLine("	(ISNULL(TJ.koj_gaisya_cd,'') + ISNULL(TJ.koj_gaisya_jigyousyo_cd,'')) AS koj_gaisya_cd, ")     '�H����ЃR�[�h
            .AppendLine("	MTK2.tys_kaisya_mei AS koj_gaisya_mei  ")                          '�H����Ж�
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten MK WITH (READCOMMITTED) ")            '�����X�}�X�^	
            .AppendLine("LEFT JOIN ")
            .AppendLine("	t_jiban TJ WITH (READCOMMITTED) ")               '�n�Ճe�[�u��	
            .AppendLine("ON ")
            .AppendLine("	MK.kameiten_cd = TJ.kameiten_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("  m_jhs_mailbox MJM WITH (READCOMMITTED) ")         '�Ј��A�J�E���g���}�X�^
            .AppendLine("ON ")
            .AppendLine("	MK.eigyou_tantousya_mei = MJM.PrimaryWindowsNTAccount ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tantousya MT1 WITH (READCOMMITTED) ")          '�S����Ͻ�	
            .AppendLine("ON ")
            .AppendLine("	TJ.tantousya_cd = MT1.tantousya_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_ks_siyou MKS WITH (READCOMMITTED) ")           '��b�d�lϽ�	
            .AppendLine("ON ")
            .AppendLine("	TJ.hantei_cd1 = MKS.ks_siyou_no ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_kairy_koj_syubetu MKKS WITH (READCOMMITTED) ") '���ǍH�����Ͻ�	
            .AppendLine("ON ")
            .AppendLine("	TJ.kairy_koj_syubetu = MKKS.kairy_koj_syubetu_no ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tantousya MT2 WITH (READCOMMITTED) ")          '�S����Ͻ�	
            .AppendLine("ON ")
            .AppendLine("	TJ.syouninsya_cd = MT2.tantousya_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_hosyousyo_hak_jyky MHHJ WITH (READCOMMITTED) ")    '�ۏ؏����s��Ͻ�	
            .AppendLine("ON ")
            .AppendLine("	TJ.hosyousyo_hak_jyky = MHHJ.hosyousyo_hak_jyky_no ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("    t_teibetu_seikyuu TTS2 ")
            .AppendLine("ON TJ.hosyousyo_no = TTS2.hosyousyo_no And TJ.kbn = TTS2.kbn")

            .AppendLine("LEFT JOIN ")
            .AppendLine("	(SELECT ")
            .AppendLine("		TJ1.kbn, ")
            .AppendLine("		TJ1.hosyousyo_no, ")
            .AppendLine("		TTS.seikyuu_umu, ")
            .AppendLine("		FLOOR(SUM(ISNULL(TTS.uri_gaku,0) * (ISNULL(MS.zeiritu,0)+1))) AS uriagegaku ")
            .AppendLine("	FROM ")
            .AppendLine("		t_jiban TJ1 WITH (READCOMMITTED) ")              '�n�Ճe�[�u��	
            .AppendLine("	LEFT JOIN ")
            .AppendLine("		t_teibetu_seikyuu TTS WITH (READCOMMITTED) ")    '�@�ʐ����e�[�u��	
            .AppendLine("	ON		 ")
            .AppendLine("		TJ1.hosyousyo_no = TTS.hosyousyo_no ")
            .AppendLine("	AND ")
            .AppendLine("		TJ1.kbn = TTS.kbn ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("		m_syouhizei MS WITH (READCOMMITTED) ")           '����Ń}�X�^	
            .AppendLine("	ON ")
            .AppendLine("		TTS.zei_kbn = MS.zei_kbn ")
            .AppendLine("	WHERE ")
            .AppendLine("		TTS.seikyuu_umu = '1' ")
            .AppendLine("	AND ")
            .AppendLine("		TJ1.kameiten_cd = @kameiten_cd ")
            .AppendLine("	GROUP BY ")
            .AppendLine("		TJ1.kbn, ")
            .AppendLine("		TJ1.hosyousyo_no, ")
            .AppendLine("		TTS.seikyuu_umu) TBL1 ")
            .AppendLine("ON ")
            .AppendLine("	TJ.hosyousyo_no = TBL1.hosyousyo_no ")
            .AppendLine("AND ")
            .AppendLine("	TJ.kbn = TBL1.kbn ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	(SELECT ")
            .AppendLine("		TJ2.kbn, ")
            .AppendLine("		TJ2.hosyousyo_no, ")
            .AppendLine("		FLOOR(SUM(ISNULL(TTN.zeikomi_nyuukin_gaku,0))) AS nyukingaku ")
            .AppendLine("	FROM ")
            .AppendLine("		t_jiban TJ2 WITH (READCOMMITTED) ")              '�n�Ճe�[�u��	
            .AppendLine("	LEFT JOIN ")
            .AppendLine("		t_teibetu_nyuukin TTN WITH (READCOMMITTED) ")    '�@�ʓ����e�[�u��	
            .AppendLine("	ON ")
            .AppendLine("		TJ2.hosyousyo_no = TTN.hosyousyo_no ")
            .AppendLine("	AND ")
            .AppendLine("		TJ2.kbn = TTN.kbn ")
            .AppendLine("	WHERE ")
            .AppendLine("		TJ2.kameiten_cd = @kameiten_cd ")
            .AppendLine("	GROUP BY ")
            .AppendLine("		TJ2.kbn, ")
            .AppendLine("		TJ2.hosyousyo_no) TBL2 ")
            .AppendLine("ON ")
            .AppendLine("	TJ.hosyousyo_no = TBL2.hosyousyo_no ")
            .AppendLine("AND ")
            .AppendLine("	TJ.kbn = TBL2.kbn ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tyousakaisya MTK1 WITH (READCOMMITTED) ")           '������Ѓ}�X�^
            .AppendLine("ON ")
            .AppendLine("	TJ.tys_kaisya_cd = MTK1.tys_kaisya_cd ")
            .AppendLine("AND ")
            .AppendLine("	TJ.tys_kaisya_jigyousyo_cd = MTK1.jigyousyo_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tyousakaisya MTK2 WITH (READCOMMITTED) ")           '������Ѓ}�X�^
            .AppendLine("ON ")
            .AppendLine("	TJ.koj_gaisya_cd = MTK2.tys_kaisya_cd ")
            .AppendLine("AND ")
            .AppendLine("	TJ.koj_gaisya_jigyousyo_cd = MTK2.jigyousyo_cd ")
            .AppendLine("WHERE ")
            .AppendLine("	MK.kameiten_cd = @kameiten_cd ")
            .AppendLine("AND ")
            .AppendLine("   TJ.kameiten_cd = @kameiten_cd ")
            '�@�ʐ����e�[�u��.�����L��=1
            .AppendLine("AND ")
            .AppendLine("   TBL1.seikyuu_umu = '1' ")
            '�n�Ճe�[�u��.�ް��j�����=0
            .AppendLine("AND ")
            .AppendLine("   TJ.data_haki_syubetu = '0' ")
            '�n�Ճe�[�u��.���ΏۊOFLG=1�ȊO
            .AppendLine("AND ")
            .AppendLine("	(TJ.saiken_taisyougai_flg <> '1' ")
            .AppendLine("       OR TJ.saiken_taisyougai_flg IS NULL) ")
            .AppendLine("AND ")
            '����z>=�����z
            .AppendLine("	(((TBL1.uriagegaku > TBL2.nyukingaku) OR (TBL1.uriagegaku IS NULL AND TBL2.nyukingaku IS NULL)) ")
            .AppendLine("   OR ")
            '�ۏ؏����s��IS NULL
            .AppendLine("   TJ.hosyousyo_hak_date IS NULL) ")
            .AppendLine("ORDER BY ")
            .AppendLine("   irai_date DESC")

        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsBukkenJyouhouInquiryDataSet, _
                    dsBukkenJyouhouInquiryDataSet.BukkenJyouhouInquiryDataTable.TableName, paramList.ToArray)

        Return dsBukkenJyouhouInquiryDataSet.BukkenJyouhouInquiryDataTable

    End Function

    ''' <summary>
    ''' �u����v�����擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>�u����v�f�[�^</returns>
    ''' <hidtory>2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ�</hidtory>
    Public Function SelTorikesi(ByVal strKameitenCd As String) As Data.DataTable

        Dim dsReturn As New DataSet

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	MK.torikesi ")
            .AppendLine("	,ISNULL(MKM.meisyou,'') AS meisyou ")
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten AS MK WITH(READCOMMITTED) ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM.meisyou_syubetu = 56 ")
            .AppendLine("		AND ")
            .AppendLine("		MK.torikesi = MKM.code ")
            .AppendLine("WHERE ")
            .AppendLine("	MK.kameiten_cd = @kameiten_cd ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTorikesi", paramList.ToArray)

        Return dsReturn.Tables("dtTorikesi")

    End Function

End Class
