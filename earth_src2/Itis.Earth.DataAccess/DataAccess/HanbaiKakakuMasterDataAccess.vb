Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports System.Text

Public Class HanbaiKakakuMasterDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>������ʂ��擾����</summary>
    ''' <returns>������ʃf�[�^�e�[�u��</returns>
    Public Function SelAiteSakiSyubetu() As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsAiteSakiSyubetu As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   code ") '�R�[�h
            .AppendLine("   ,code + '�F' + ISNULL(meisyou,'') AS aitesaki_syubetu ")  '�������
            .AppendLine(" FROM ")
            .AppendLine("   m_kakutyou_meisyou WITH(READCOMMITTED) ") '�g�����̃}�X�^
            .AppendLine(" WHERE ")
            .AppendLine("	meisyou_syubetu = 23 ") '���̎��
            .AppendLine(" ORDER BY ")
            .AppendLine("   code ")
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsAiteSakiSyubetu, "dtAiteSakiSyubetu")

        Return dsAiteSakiSyubetu.Tables("dtAiteSakiSyubetu")

    End Function
    ''' <summary>���i���擾����</summary>
    ''' <returns>���i�f�[�^�e�[�u��</returns>
    Public Function SelSyouhin() As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsSyouhin As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   syouhin_cd ")  '���i�R�[�h
            .AppendLine("   ,syouhin_cd + '�F' + ISNULL(syouhin_mei,'') AS syouhin ") '���i
            .AppendLine(" FROM ")
            .AppendLine("   m_syouhin WITH(READCOMMITTED) ") '���i�}�X�^
            .AppendLine(" WHERE torikesi = 0 ")  '���
            .AppendLine(" AND souko_cd = '100' ") '�q�ɃR�[�h
            .AppendLine(" ORDER BY ")
            .AppendLine("   syouhin_cd ")
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsSyouhin, "dtSyouhin")

        Return dsSyouhin.Tables("dtSyouhin")

    End Function
    ''' <summary>�������@���擾����</summary>
    ''' <returns>�������@�f�[�^�e�[�u��</returns>
    Public Function SelTyousaHouhou() As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsTyousaHouhou As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	tys_houhou_no ") '�������@NO
            .AppendLine("	,CAST(tys_houhou_no AS VARCHAR) + '�F' + ISNULL(tys_houhou_mei,'') AS tys_houhou ")  '�������@
            .AppendLine("FROM ")
            .AppendLine("	m_tyousahouhou WITH(READCOMMITTED) ") '�������@�}�X�^
            .AppendLine(" ORDER BY ")
            .AppendLine("   tys_houhou_no ")
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyousaHouhou, "dtTyousahouhou")

        Return dsTyousaHouhou.Tables("dtTyousahouhou")

    End Function
    '''<summary>���������擾����</summary>
    ''' <param name="strAitesakiSyubetu">�������</param>
    ''' <param name="strTorikesiAitesaki">��������敪</param>
    ''' <param name="strAitesakiCd">�����R�[�h</param>
    ''' <returns>�������f�[�^�e�[�u��</returns>
    Public Function SelAiteSaki(ByVal strAitesakiSyubetu As String, _
                                ByVal strTorikesiAitesaki As String, _
                                ByVal strAitesakiCd As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        commandTextSb.AppendLine(" SELECT ")
        Select Case strAitesakiSyubetu
            Case "1"
                commandTextSb.AppendLine("      kameiten_cd AS cd, ")
                commandTextSb.AppendLine("      kameiten_mei1 AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_kameiten WITH(READCOMMITTED) ")
                commandTextSb.AppendLine(" WHERE  kameiten_cd = @kameiten_cd")
                '�p�����[�^�̐ݒ�
                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strAitesakiCd))

            Case "5"
                commandTextSb.AppendLine("      eigyousyo_cd AS cd, ")
                commandTextSb.AppendLine("      eigyousyo_mei AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_eigyousyo WITH(READCOMMITTED) ")
                commandTextSb.AppendLine(" WHERE eigyousyo_cd = @eigyousyo_cd ")
                '�p�����[�^�̐ݒ�
                paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strAitesakiCd))
            Case "7"
                commandTextSb.AppendLine("      keiretu_cd AS cd, ")
                commandTextSb.AppendLine("      keiretu_mei AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_keiretu WITH(READCOMMITTED) ")
                commandTextSb.AppendLine(" WHERE keiretu_cd = @keiretu_cd ")
                '�p�����[�^�̐ݒ�
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, strAitesakiCd))

        End Select

        If strTorikesiAitesaki <> String.Empty Then
            '����p�����[�^�̐ݒ�
            commandTextSb.AppendLine(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        End If

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    "dtAiteSaki", paramList.ToArray)

        Return dsHanbaiKakaku.Tables("dtAiteSaki")

    End Function
    ''' <summary>�̔����i�f�[�^���擾����</summary>
    ''' <param name="dtHanbaiKakakuInfo">�p�����[�^</param>
    ''' <returns>�̔����i�f�[�^�e�[�u��</returns>
    Public Function SelHanbaiKakakuInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
           As HanbaiKakakuMasterDataSet.HanbaiKakakuInfoTableDataTable

        'DataSet�C���X�^���X�̐���
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            If dtHanbaiKakakuInfo(0).kensaku_count = "100" Then
                .AppendLine("      TOP 100 ")
            ElseIf dtHanbaiKakakuInfo(0).kensaku_count = "10" Then
                .AppendLine("      TOP 10 ")
            End If
            '�̔����iM.�������:�g������M.����
            .AppendLine("   CAST(MHK.aitesaki_syubetu AS VARCHAR) + '�F' + ISNULL(MKM.meisyou,'') AS aitesaki ")
            .AppendLine("   ,MHK.aitesaki_cd ")                             '�̔����iM.�����R�[�h
            .AppendLine("   ,SUB.aitesaki_mei ")                            '�T�u.����於
            .AppendLine("   ,MHK.syouhin_cd ")                              '�̔����iM.���i�R�[�h
            .AppendLine("   ,MS.syouhin_mei ")                              '���iM.���i��
            '�̔����iM.�������@NO:�������@M.�������@����
            .AppendLine("   ,CAST(MHK.tys_houhou_no AS VARCHAR) + '�F'+ISNULL(MT.tys_houhou_mei,'') AS tys_houhou ")
            .AppendLine("   ,CASE MHK.torikesi ")
            .AppendLine("       WHEN 0 THEN '' ")
            .AppendLine("       ELSE '���' ")
            .AppendLine("    END AS torikesi ")                             '�̔����iM.���
            .AppendLine("   ,MHK.koumuten_seikyuu_gaku ")                   '�̔����iM.�H���X�������z
            .AppendLine("   ,CASE ISNULL(MHK.koumuten_seikyuu_gaku_henkou_flg,0) ")
            .AppendLine("       WHEN 0 THEN '�ύX�s��' ")
            .AppendLine("       ELSE '�ύX��' ")
            .AppendLine("    END AS koumuten_seikyuu_gaku_henkou_flg ")     '�̔����iM.�H���X�������z�ύXFLG
            .AppendLine("   ,MHK.jitu_seikyuu_gaku ")                       '�̔����iM.���������z
            .AppendLine("   ,CASE ISNULL(MHK.jitu_seikyuu_gaku_henkou_flg,0) ")
            .AppendLine("       WHEN 0 THEN '�ύX�s��' ")
            .AppendLine("       ELSE '�ύX��' ")
            .AppendLine("    END AS jitu_seikyuu_gaku_henkou_flg ")         '�̔����iM.���������z�ύXFLG
            .AppendLine("   ,CASE ISNULL(MHK.koukai_flg,0) ")
            .AppendLine("       WHEN 0 THEN '����J' ")
            .AppendLine("       ELSE '���J' ")
            .AppendLine("    END AS koukai_flg ")                           '�̔����iM.���J�t���O
            .AppendLine("   ,MHK.tys_houhou_no ")                           '�̔����iM.�������@NO
            .AppendLine(" FROM ")
            .AppendLine("   m_hanbai_kakaku MHK WITH(READCOMMITTED) ")      '�̔����iM
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_kakutyou_meisyou MKM WITH(READCOMMITTED) ")   '�g������M
            .AppendLine(" ON    MHK.aitesaki_syubetu = MKM.code ")
            .AppendLine(" AND   MKM.meisyou_syubetu = 23 ")
            .AppendLine(" LEFT OUTER JOIN ( ")
            .AppendLine("   SELECT ")
            .AppendLine("       0 AS aitesaki_syubetu ")
            .AppendLine("       ,'ALL' AS aitesaki_cd ")
            .AppendLine("       ,'�����Ȃ�' AS aitesaki_mei ")
            .AppendLine("       ,0 AS torikesi ")
            .AppendLine("   UNION ALL ")
            .AppendLine("   SELECT ")
            .AppendLine("       1 AS aitesaki_syubetu ")
            .AppendLine("       ,kameiten_cd AS aitesaki_cd ")
            .AppendLine("       ,kameiten_mei1 AS aitesaki_mei ")
            .AppendLine("       ,torikesi ")
            .AppendLine("   FROM ")
            .AppendLine("       m_kameiten WITH(READCOMMITTED) ")   '�����X�}�X�^
            .AppendLine("   UNION ALL ")
            .AppendLine("   SELECT ")
            .AppendLine("       5 AS aitesaki_syubetu ")
            .AppendLine("       ,eigyousyo_cd AS aitesaki_cd ")
            .AppendLine("       ,eigyousyo_mei AS aitesaki_mei ")
            .AppendLine("       ,torikesi ")
            .AppendLine("   FROM ")
            .AppendLine("       m_eigyousyo WITH(READCOMMITTED) ")   '�c�Ə��}�X�^
            .AppendLine("   UNION ALL ")
            .AppendLine("   SELECT ")
            .AppendLine("       7 AS aitesaki_syubetu ")
            .AppendLine("       ,keiretu_cd AS aitesaki_cd ")
            .AppendLine("       ,MIN(keiretu_mei) AS aitesaki_mei ")
            .AppendLine("       ,MIN(torikesi) AS torikesi ")
            .AppendLine("   FROM ")
            .AppendLine("       m_keiretu WITH(READCOMMITTED) ")   '�n��}�X�^
            .AppendLine("   GROUP BY ")
            .AppendLine("       keiretu_cd ")
            .AppendLine("   ) SUB ")
            .AppendLine(" ON    MHK.aitesaki_syubetu = SUB.aitesaki_syubetu ")
            .AppendLine(" AND   MHK.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_syouhin MS WITH(READCOMMITTED) ")   '���i�}�X�^
            .AppendLine(" ON    MHK.syouhin_cd = MS.syouhin_cd ")
            .AppendLine(" AND   MS.souko_cd = '100' ")
            .AppendLine(" AND   MS.torikesi = 0 ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_tyousahouhou MT WITH(READCOMMITTED) ")   '�������@�}�X�^
            .AppendLine(" ON    MHK.tys_houhou_no = MT.tys_houhou_no ")
            .AppendLine(" WHERE ")

            '�������
            .AppendLine(" MHK.aitesaki_syubetu = @aitesaki_syubetu ")
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).aitesaki_syubetu))

            '�����R�[�h
            If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_from ")
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_to ")
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            End If

            '���i�R�[�h
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                .AppendLine(" AND MHK.syouhin_cd = @syouhin_cd ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuInfo(0).syouhin_cd))
            End If

            '�������@
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                .AppendLine(" AND MHK.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).tys_houhou_no))
            End If

            '���
            If dtHanbaiKakakuInfo(0).torikesi <> String.Empty Then
                .AppendLine(" AND MHK.torikesi = @torikesi ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, 0))
            End If

            '��������
            If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                .AppendLine(" AND SUB.torikesi = @torikesi_aitesaki ")
                paramList.Add(MakeParam("@torikesi_aitesaki", SqlDbType.Int, 4, 0))
            End If

            '=======================2011/05/18 �ԗ� �d�l�ύX �ǉ� �J�n��==========================
            '\0�͑ΏۊO
            If dtHanbaiKakakuInfo(0).kingaku_0_taisyou_gai <> String.Empty Then
                .AppendLine(" AND MHK.jitu_seikyuu_gaku <> 0 ")
            End If
            '=======================2011/05/18 �ԗ� �d�l�ύX �ǉ� �I����==========================

            '�\�[�g��
            .AppendLine(" ORDER BY ")
            .AppendLine("      MHK.aitesaki_syubetu ")
            .AppendLine("      ,MHK.aitesaki_cd ")
            .AppendLine("      ,MHK.syouhin_cd ")
            .AppendLine("      ,MHK.tys_houhou_no ")
        End With

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    dsHanbaiKakaku.HanbaiKakakuInfoTable.TableName, paramList.ToArray)

        Return dsHanbaiKakaku.HanbaiKakakuInfoTable

    End Function
    ''' <summary>�̔����i�u�n��E�c�Ə��E�w��Ȃ��`�F�b�N�{�b�N�X�v=�`�F�b�N�̏ꍇ�̃f�[�^���擾����</summary>
    ''' <param name="dtHanbaiKakakuInfo">�p�����[�^</param>
    ''' <returns>�̔����i�f�[�^�e�[�u��</returns>
    Public Function SelHanbaiKakakuSeiteNasiInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
           As HanbaiKakakuMasterDataSet.HanbaiKakakuInfoTableDataTable

        'DataSet�C���X�^���X�̐���
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            If dtHanbaiKakakuInfo(0).kensaku_count = "100" Then
                .AppendLine("      TOP 100 ")
            ElseIf dtHanbaiKakakuInfo(0).kensaku_count = "10" Then
                .AppendLine("      TOP 10 ")
            End If
            '�T�u�̔����iM.�������:�g������M.����
            .AppendLine("	CAST(SUBMHK.aitesaki_syubetu AS VARCHAR) + '�F' + ISNULL(MKM.meisyou,'') AS aitesaki ")
            .AppendLine("	,SUBMHK.aitesaki_cd ")  '�T�u�̔����iM.�����R�[�h
            .AppendLine("	,SUB.aitesaki_mei ")    '�T�u.����於
            .AppendLine("	,SUBMHK.syouhin_cd ")   '�T�u�̔����iM.���i�R�[�h
            .AppendLine("	,MS.syouhin_mei ")      '���iM.���i��
            '�T�u�̔����iM.�������@�i�������@No�F�������@���j
            .AppendLine("	,CAST(SUBMHK.tys_houhou_no AS VARCHAR) + '�F' + ISNULL(MT.tys_houhou_mei,'') AS tys_houhou ")
            .AppendLine("	,CASE SUBMHK.torikesi  ")   '����i0:""�@0�ȊO:����j
            .AppendLine("		WHEN 0 THEN '' ")
            .AppendLine("		ELSE '���' ")
            .AppendLine("	 END AS torikesi ")
            .AppendLine("	,SUBMHK.koumuten_seikyuu_gaku ")    '�T�u�̔����iM.�H���X�����z
            .AppendLine("	,CASE ISNULL(SUBMHK.koumuten_seikyuu_gaku_henkou_flg,0) ")
            .AppendLine("		WHEN 0 THEN '�ύX�s��' ")
            .AppendLine("		ELSE '�ύX��' ")
            .AppendLine("	 END AS koumuten_seikyuu_gaku_henkou_flg ")     '�H���X�����z�ύX�t���O�i0:�ύX�s��,0�ȊO:�ύX�j
            .AppendLine("	,SUBMHK.jitu_seikyuu_gaku ")        '�T�u�̔����iM.�������z
            .AppendLine("	,CASE ISNULL(SUBMHK.jitu_seikyuu_gaku_henkou_flg,0) ")
            .AppendLine("		WHEN 0 THEN '�ύX�s��' ")
            .AppendLine("		ELSE '�ύX��' ")
            .AppendLine("	 END AS jitu_seikyuu_gaku_henkou_flg ")     '�T�u�̔����iM.�������z�t���O�i0:�ύX�s��,0�ȊO�F�ύX�j
            .AppendLine("	,CASE ISNULL(SUBMHK.koukai_flg,0) ")
            .AppendLine("		WHEN 0 THEN '����J' ")
            .AppendLine("		ELSE '���J' ")
            .AppendLine("	 END AS koukai_flg ")       '�T�u�̔����iM.���J�t���O�i0�F����J,0�ȊO:���J�j
            .AppendLine("	,SUBMHK.tys_houhou_no ")
            .AppendLine("FROM  ")
            .AppendLine("	( ")
            .AppendLine("		SELECT MHK.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHK WITH(READCOMMITTED)  ")     '�̔����i�}�X�^
            .AppendLine("		WHERE  ")
            .AppendLine("			MHK.aitesaki_syubetu = 1  ")
            .AppendLine("			AND MHK.aitesaki_cd = @aitesaki_cd ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT MHKM.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '�̔����i�}�X�^
            .AppendLine("		INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED)  ")    '�����X�}�X�^
            .AppendLine("		ON MHKM.aitesaki_syubetu = 5  ")
            .AppendLine("			AND MHKM.aitesaki_cd = MKM.eigyousyo_cd ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHK.*  ")
            .AppendLine("				FROM m_hanbai_kakaku MHK WITH(READCOMMITTED) ")     '�̔����i�}�X�^
            .AppendLine("				WHERE  ")
            .AppendLine("					MHK.aitesaki_syubetu = 1  ")
            .AppendLine("					AND  MHK.aitesaki_cd = @aitesaki_cd ")
            '============================2011/04/26 �ԗ� �ǉ� �J�n��=====================================
            .AppendLine("					AND  ISNULL(MHK.torikesi,0) = 0 ")          'ISNULL(���,0)=0
            '============================2011/04/26 �ԗ� �ǉ� �I����=====================================
            .AppendLine("			) AS SUBMHK1 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK1.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK1.tys_houhou_no ")
            .AppendLine("		WHERE  ")
            .AppendLine("			MKM.kameiten_cd = @aitesaki_cd  ")
            .AppendLine("			AND SUBMHK1.aitesaki_cd IS NULL ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT MHKM.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '�̔����i�}�X�^
            .AppendLine("		INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '�����X�}�X�^
            .AppendLine("		ON MHKM.aitesaki_syubetu = 7  ")
            .AppendLine("			AND MHKM.aitesaki_cd = MKM.keiretu_cd ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHK.*  ")
            .AppendLine("				FROM m_hanbai_kakaku MHK WITH(READCOMMITTED) ")     '�̔����i�}�X�^
            .AppendLine("				WHERE  ")
            .AppendLine("					MHK.aitesaki_syubetu = 1  ")
            .AppendLine("					AND  MHK.aitesaki_cd = @aitesaki_cd ")
            '============================2011/04/26 �ԗ� �ǉ� �J�n��=====================================
            .AppendLine("					AND  ISNULL(MHK.torikesi,0) = 0 ")          'ISNULL(���,0)=0
            '============================2011/04/26 �ԗ� �ǉ� �I����=====================================
            .AppendLine("			) AS SUBMHK1 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK1.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK1.tys_houhou_no ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHKM.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED)  ")    '�̔����i�}�X�^
            .AppendLine("				INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '�����X�}�X�^
            .AppendLine("				ON MHKM.aitesaki_syubetu = 5  ")
            .AppendLine("					AND MHKM.aitesaki_cd = MKM.eigyousyo_cd ")
            .AppendLine("					AND MKM.kameiten_cd = @aitesaki_cd ")
            '============================2011/04/26 �ԗ� �ǉ� �J�n��=====================================
            .AppendLine("					AND  ISNULL(MHKM.torikesi,0) = 0 ")          'ISNULL(���,0)=0
            '============================2011/04/26 �ԗ� �ǉ� �I����=====================================
            .AppendLine("			) AS SUBMHK5 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK5.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK5.tys_houhou_no ")
            .AppendLine("		WHERE  ")
            .AppendLine("			MKM.kameiten_cd = @aitesaki_cd  ")
            .AppendLine("			AND SUBMHK1.aitesaki_cd IS NULL ")
            .AppendLine("			AND SUBMHK5.aitesaki_cd IS NULL ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT MHKM.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '�̔����i�}�X�^
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHK.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHK WITH(READCOMMITTED) ")      '�̔����i�}�X�^
            .AppendLine("				WHERE  ")
            .AppendLine("					MHK.aitesaki_syubetu = 1  ")
            .AppendLine("					AND MHK.aitesaki_cd = @aitesaki_cd ")
            '============================2011/04/26 �ԗ� �ǉ� �J�n��=====================================
            .AppendLine("					AND  ISNULL(MHK.torikesi,0) = 0 ")          'ISNULL(���,0)=0
            '============================2011/04/26 �ԗ� �ǉ� �I����=====================================
            .AppendLine("			) AS SUBMHK1 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK1.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK1.tys_houhou_no ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHKM.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED)  ")        '�̔����i�}�X�^
            .AppendLine("				INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '�����X�}�X�^
            .AppendLine("				ON MHKM.aitesaki_syubetu = 5  ")
            .AppendLine("					AND MHKM.aitesaki_cd = MKM.eigyousyo_cd ")
            .AppendLine("					AND MKM.kameiten_cd = @aitesaki_cd ")
            '============================2011/04/26 �ԗ� �ǉ� �J�n��=====================================
            .AppendLine("					AND  ISNULL(MHKM.torikesi,0) = 0 ")          'ISNULL(���,0)=0
            '============================2011/04/26 �ԗ� �ǉ� �I����=====================================
            .AppendLine("			) AS SUBMHK5 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK5.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK5.tys_houhou_no ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHKM.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '�̔����i�}�X�^
            .AppendLine("				INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '�����X�}�X�^
            .AppendLine("				ON MHKM.aitesaki_syubetu = 7  ")
            .AppendLine("					AND MHKM.aitesaki_cd = MKM.keiretu_cd ")
            .AppendLine("					AND MKM.kameiten_cd = @aitesaki_cd ")
            '============================2011/04/26 �ԗ� �ǉ� �J�n��=====================================
            .AppendLine("					AND  ISNULL(MHKM.torikesi,0) = 0 ")          'ISNULL(���,0)=0
            '============================2011/04/26 �ԗ� �ǉ� �I����=====================================
            .AppendLine("			) AS SUBMHK7 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK7.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK7.tys_houhou_no ")
            .AppendLine("		WHERE  ")
            .AppendLine("			MHKM.aitesaki_syubetu = 0  ")
            .AppendLine("			AND SUBMHK1.aitesaki_cd IS NULL ")
            .AppendLine("			AND SUBMHK5.aitesaki_cd IS NULL  ")
            .AppendLine("			AND SUBMHK7.aitesaki_cd IS NULL ")
            .AppendLine("	) AS SUBMHK ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("		( ")
            .AppendLine("			SELECT  ")
            .AppendLine("				0 AS aitesaki_syubetu,  ")
            .AppendLine("				'ALL' AS aitesaki_cd,  ")
            .AppendLine("				'�����Ȃ�' AS aitesaki_mei,  ")
            .AppendLine("				0 AS torikesi ")
            .AppendLine("			UNION ALL ")
            .AppendLine("			SELECT  ")
            .AppendLine("				1 AS aitesaki_syubetu,  ")
            .AppendLine("				MK.kameiten_cd AS aitesaki_cd,  ")
            .AppendLine("				MK.kameiten_mei1 AS aitesaki_mei,  ")
            .AppendLine("				MK.torikesi AS torikesi ")
            .AppendLine("			FROM m_kameiten AS MK WITH(READCOMMITTED) ")        '�����X�}�X�^
            .AppendLine("			UNION ALL ")
            .AppendLine("			SELECT  ")
            .AppendLine("				5 AS aitesaki_syubetu,  ")
            .AppendLine("				ME.eigyousyo_cd AS aitesaki_cd,  ")
            .AppendLine("				ME.eigyousyo_mei AS aitesaki_mei,  ")
            .AppendLine("				ME.torikesi AS torikesi ")
            .AppendLine("			FROM m_eigyousyo AS ME WITH(READCOMMITTED) ")       '�c�Ə��}�X�^
            .AppendLine("			UNION ALL  ")
            .AppendLine("			SELECT  ")
            .AppendLine("				7 AS aitesaki_syubetu,  ")
            .AppendLine("				MK.keiretu_cd AS aitesaki_cd,  ")
            .AppendLine("				MIN(MK.keiretu_mei) AS aitesaki_mei,  ")
            .AppendLine("				MIN(MK.torikesi) AS torikesi ")
            .AppendLine("			FROM m_keiretu AS MK WITH(READCOMMITTED)  ")        '�n��}�X�^
            .AppendLine("			GROUP BY  ")
            .AppendLine("				MK.keiretu_cd ")
            .AppendLine("		) AS SUB ")
            .AppendLine("		ON SUBMHK.aitesaki_syubetu = SUB.aitesaki_syubetu  ")
            .AppendLine("			AND SUBMHK.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine("	LEFT JOIN m_syouhin AS MS WITH(READCOMMITTED) ")        '���i�}�X�^
            .AppendLine("		ON SUBMHK.syouhin_cd = MS.syouhin_cd  ")
            .AppendLine("			AND MS.souko_cd = '100'  ")
            .AppendLine("			AND MS.torikesi = 0 ")
            .AppendLine("	LEFT JOIN m_tyousahouhou AS MT WITH(READCOMMITTED) ")       '�������@�}�X�^
            .AppendLine("		ON SUBMHK.tys_houhou_no = MT.tys_houhou_no ")
            .AppendLine("	LEFT JOIN 	m_kakutyou_meisyou AS MKM WITH(READCOMMITTED) ")        '�g�����̃}�X�^
            .AppendLine("		ON SUBMHK.aitesaki_syubetu = MKM.code  ")
            .AppendLine("			AND MKM.meisyou_syubetu = '23' ")
            .AppendLine("WHERE(1=1)  ")
            '���i�R�[�h
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                .AppendLine("AND SUBMHK.syouhin_cd = @syouhin_cd  ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuInfo(0).syouhin_cd))
            End If

            '�������@
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                .AppendLine("AND SUBMHK.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).tys_houhou_no))
            End If

            '���
            '============================2011/04/26 �ԗ� �폜 �J�n��=====================================
            'If dtHanbaiKakakuInfo(0).torikesi <> String.Empty Then
            '============================2011/04/26 �ԗ� �폜 �I����=====================================
            .AppendLine("AND SUBMHK.torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, 0))
            '============================2011/04/26 �ԗ� �폜 �J�n��=====================================
            'End If
            '============================2011/04/26 �ԗ� �폜 �I����=====================================

            '��������
            If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                .AppendLine("AND SUB.torikesi = @torikesi_aitesaki ")
                paramList.Add(MakeParam("@torikesi_aitesaki", SqlDbType.Int, 4, 0))
            End If

            '=======================2011/05/18 �ԗ� �d�l�ύX �ǉ� �J�n��==========================
            '\0�͑ΏۊO
            If dtHanbaiKakakuInfo(0).kingaku_0_taisyou_gai <> String.Empty Then
                .AppendLine(" AND SUBMHK.jitu_seikyuu_gaku <> 0 ")
            End If
            '=======================2011/05/18 �ԗ� �d�l�ύX �ǉ� �I����==========================

            .AppendLine("ORDER BY  ")
            .AppendLine("SUBMHK.syouhin_cd  ")
            .AppendLine(",SUBMHK.tys_houhou_no ")
        End With

        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    dsHanbaiKakaku.HanbaiKakakuInfoTable.TableName, paramList.ToArray)

        Return dsHanbaiKakaku.HanbaiKakakuInfoTable

    End Function
    ''' <summary>�̔����i�f�[�^�������擾����</summary>
    ''' <param name="dtHanbaiKakakuInfo">�p�����[�^</param>
    ''' <returns>�̔����i�f�[�^����</returns>
    Public Function SelHanbaiKakakuInfoCount(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
           As Integer

        'DataSet�C���X�^���X�̐���
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT(*) AS count ")
            .AppendLine(" FROM ")
            .AppendLine("   m_hanbai_kakaku MHK WITH(READCOMMITTED) ")     '�̔����iM
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_kakutyou_meisyou MKM WITH(READCOMMITTED) ")  '�g������M
            .AppendLine(" ON    MHK.aitesaki_syubetu = MKM.code ")
            .AppendLine(" AND   MKM.meisyou_syubetu = 23 ")
            .AppendLine(" LEFT OUTER JOIN ( ")
            .AppendLine("   SELECT ")
            .AppendLine("       0 AS aitesaki_syubetu ")
            .AppendLine("       ,'ALL' AS aitesaki_cd ")
            .AppendLine("       ,'�����Ȃ�' AS aitesaki_mei ")
            .AppendLine("       ,0 AS torikesi ")
            .AppendLine("   UNION ALL ")
            .AppendLine("   SELECT ")
            .AppendLine("       1 AS aitesaki_syubetu ")
            .AppendLine("       ,kameiten_cd AS aitesaki_cd ")
            .AppendLine("       ,kameiten_mei1 AS aitesaki_mei ")
            .AppendLine("       ,torikesi ")
            .AppendLine("   FROM ")
            .AppendLine("       m_kameiten WITH(READCOMMITTED) ")   '�����X�}�X�^
            .AppendLine("   UNION ALL ")
            .AppendLine("   SELECT ")
            .AppendLine("       5 AS aitesaki_syubetu ")
            .AppendLine("       ,eigyousyo_cd AS aitesaki_cd ")
            .AppendLine("       ,eigyousyo_mei AS aitesaki_mei ")
            .AppendLine("       ,torikesi ")
            .AppendLine("   FROM ")
            .AppendLine("       m_eigyousyo WITH(READCOMMITTED) ")   '�c�Ə��}�X�^
            .AppendLine("   UNION ALL ")
            .AppendLine("   SELECT ")
            .AppendLine("       7 AS aitesaki_syubetu ")
            .AppendLine("       ,keiretu_cd AS aitesaki_cd ")
            .AppendLine("       ,MIN(keiretu_mei) AS aitesaki_mei ")
            .AppendLine("       ,MIN(torikesi) AS torikesi ")
            .AppendLine("   FROM ")
            .AppendLine("       m_keiretu WITH(READCOMMITTED) ")   '�n��}�X�^
            .AppendLine("   GROUP BY ")
            .AppendLine("       keiretu_cd ")
            .AppendLine("   ) SUB ")
            .AppendLine(" ON    MHK.aitesaki_syubetu = SUB.aitesaki_syubetu ")
            .AppendLine(" AND   MHK.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_syouhin MS WITH(READCOMMITTED) ")     '���i�}�X�^
            .AppendLine(" ON    MHK.syouhin_cd = MS.syouhin_cd ")
            .AppendLine(" AND   MS.souko_cd = '100' ")
            .AppendLine(" AND   MS.torikesi = 0 ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_tyousahouhou MT WITH(READCOMMITTED) ")   '�������@�}�X�^
            .AppendLine(" ON    MHK.tys_houhou_no = MT.tys_houhou_no ")
            .AppendLine(" WHERE ")

            '�������
            .AppendLine(" MHK.aitesaki_syubetu = @aitesaki_syubetu ")
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).aitesaki_syubetu))

            '�����R�[�h
            If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_from ")
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_to ")
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            End If

            '���i�R�[�h
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                .AppendLine(" AND MHK.syouhin_cd = @syouhin_cd ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuInfo(0).syouhin_cd))
            End If

            '�������@
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                .AppendLine(" AND MHK.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).tys_houhou_no))
            End If

            '���
            If dtHanbaiKakakuInfo(0).torikesi <> String.Empty Then
                .AppendLine(" AND MHK.torikesi = @torikesi ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, 0))
            End If

            '��������
            If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                .AppendLine(" AND SUB.torikesi = @torikesi_aitesaki ")
                paramList.Add(MakeParam("@torikesi_aitesaki", SqlDbType.Int, 4, 0))
            End If

            '=======================2011/05/18 �ԗ� �d�l�ύX �ǉ� �J�n��==========================
            '\0�͑ΏۊO
            If dtHanbaiKakakuInfo(0).kingaku_0_taisyou_gai <> String.Empty Then
                .AppendLine(" AND MHK.jitu_seikyuu_gaku <> 0 ")
            End If
            '=======================2011/05/18 �ԗ� �d�l�ύX �ǉ� �I����==========================

        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    "dtHanbaiKakakuCount", paramList.ToArray)

        Return dsHanbaiKakaku.Tables("dtHanbaiKakakuCount").Rows(0).Item("count")

    End Function
    ''' <summary>�̔����i�u�n��E�c�Ə��E�w��Ȃ��`�F�b�N�{�b�N�X�v=�`�F�b�N�̏ꍇ�̃f�[�^�̌������擾����</summary>
    ''' <param name="dtHanbaiKakakuInfo">�p�����[�^</param>
    ''' <returns>�̔����i�f�[�^����</returns>
    Public Function SelHanbaiKakakuSeiteNasiinfoCount(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
           As Integer

        'DataSet�C���X�^���X�̐���
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT(*) AS count ")
            .AppendLine("FROM  ")
            .AppendLine("	( ")
            .AppendLine("		SELECT MHK.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHK WITH(READCOMMITTED)  ")     '�̔����i�}�X�^
            .AppendLine("		WHERE  ")
            .AppendLine("			MHK.aitesaki_syubetu = 1  ")
            .AppendLine("			AND MHK.aitesaki_cd = @aitesaki_cd ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT MHKM.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '�̔����i�}�X�^
            .AppendLine("		INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED)  ")    '�����X�}�X�^
            .AppendLine("		ON MHKM.aitesaki_syubetu = 5  ")
            .AppendLine("			AND MHKM.aitesaki_cd = MKM.eigyousyo_cd ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHK.*  ")
            .AppendLine("				FROM m_hanbai_kakaku MHK WITH(READCOMMITTED) ")     '�̔����i�}�X�^
            .AppendLine("				WHERE  ")
            .AppendLine("					MHK.aitesaki_syubetu = 1  ")
            .AppendLine("					AND  MHK.aitesaki_cd = @aitesaki_cd ")
            '============================2011/04/26 �ԗ� �ǉ� �J�n��=====================================
            .AppendLine("					AND  ISNULL(MHK.torikesi,0) = 0 ")          'ISNULL(���,0)=0
            '============================2011/04/26 �ԗ� �ǉ� �I����=====================================
            .AppendLine("			) AS SUBMHK1 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK1.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK1.tys_houhou_no ")
            .AppendLine("		WHERE  ")
            .AppendLine("			MKM.kameiten_cd = @aitesaki_cd  ")
            .AppendLine("			AND SUBMHK1.aitesaki_cd IS NULL ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT MHKM.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '�̔����i�}�X�^
            .AppendLine("		INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '�����X�}�X�^
            .AppendLine("		ON MHKM.aitesaki_syubetu = 7  ")
            .AppendLine("			AND MHKM.aitesaki_cd = MKM.keiretu_cd ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHK.*  ")
            .AppendLine("				FROM m_hanbai_kakaku MHK WITH(READCOMMITTED) ")     '�̔����i�}�X�^
            .AppendLine("				WHERE  ")
            .AppendLine("					MHK.aitesaki_syubetu = 1  ")
            .AppendLine("					AND  MHK.aitesaki_cd = @aitesaki_cd ")
            '============================2011/04/26 �ԗ� �ǉ� �J�n��=====================================
            .AppendLine("					AND  ISNULL(MHK.torikesi,0) = 0 ")          'ISNULL(���,0)=0
            '============================2011/04/26 �ԗ� �ǉ� �I����=====================================
            .AppendLine("			) AS SUBMHK1 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK1.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK1.tys_houhou_no ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHKM.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED)  ")    '�̔����i�}�X�^
            .AppendLine("				INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '�����X�}�X�^
            .AppendLine("				ON MHKM.aitesaki_syubetu = 5  ")
            .AppendLine("					AND MHKM.aitesaki_cd = MKM.eigyousyo_cd ")
            .AppendLine("					AND MKM.kameiten_cd = @aitesaki_cd ")
            '============================2011/04/26 �ԗ� �ǉ� �J�n��=====================================
            .AppendLine("					AND  ISNULL(MHKM.torikesi,0) = 0 ")          'ISNULL(���,0)=0
            '============================2011/04/26 �ԗ� �ǉ� �I����=====================================
            .AppendLine("			) AS SUBMHK5 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK5.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK5.tys_houhou_no ")
            .AppendLine("		WHERE  ")
            .AppendLine("			MKM.kameiten_cd = @aitesaki_cd  ")
            .AppendLine("			AND SUBMHK1.aitesaki_cd IS NULL ")
            .AppendLine("			AND SUBMHK5.aitesaki_cd IS NULL ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT MHKM.*  ")
            .AppendLine("		FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '�̔����i�}�X�^
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHK.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHK WITH(READCOMMITTED) ")      '�̔����i�}�X�^
            .AppendLine("				WHERE  ")
            .AppendLine("					MHK.aitesaki_syubetu = 1  ")
            .AppendLine("					AND MHK.aitesaki_cd = @aitesaki_cd ")
            '============================2011/04/26 �ԗ� �ǉ� �J�n��=====================================
            .AppendLine("					AND  ISNULL(MHK.torikesi,0) = 0 ")          'ISNULL(���,0)=0
            '============================2011/04/26 �ԗ� �ǉ� �I����=====================================
            .AppendLine("			) AS SUBMHK1 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK1.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK1.tys_houhou_no ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHKM.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED)  ")        '�̔����i�}�X�^
            .AppendLine("				INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '�����X�}�X�^
            .AppendLine("				ON MHKM.aitesaki_syubetu = 5  ")
            .AppendLine("					AND MHKM.aitesaki_cd = MKM.eigyousyo_cd ")
            .AppendLine("					AND MKM.kameiten_cd = @aitesaki_cd ")
            '============================2011/04/26 �ԗ� �ǉ� �J�n��=====================================
            .AppendLine("					AND  ISNULL(MHKM.torikesi,0) = 0 ")          'ISNULL(���,0)=0
            '============================2011/04/26 �ԗ� �ǉ� �I����=====================================
            .AppendLine("			) AS SUBMHK5 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK5.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK5.tys_houhou_no ")
            .AppendLine("		LEFT JOIN ")
            .AppendLine("			( ")
            .AppendLine("				SELECT MHKM.*  ")
            .AppendLine("				FROM m_hanbai_kakaku AS MHKM WITH(READCOMMITTED) ")     '�̔����i�}�X�^
            .AppendLine("				INNER JOIN m_kameiten AS MKM WITH(READCOMMITTED) ")     '�����X�}�X�^
            .AppendLine("				ON MHKM.aitesaki_syubetu = 7  ")
            .AppendLine("					AND MHKM.aitesaki_cd = MKM.keiretu_cd ")
            .AppendLine("					AND MKM.kameiten_cd = @aitesaki_cd ")
            '============================2011/04/26 �ԗ� �ǉ� �J�n��=====================================
            .AppendLine("					AND  ISNULL(MHKM.torikesi,0) = 0 ")          'ISNULL(���,0)=0
            '============================2011/04/26 �ԗ� �ǉ� �I����=====================================
            .AppendLine("			) AS SUBMHK7 ")
            .AppendLine("		ON MHKM.syouhin_cd = SUBMHK7.syouhin_cd  ")
            .AppendLine("			AND MHKM.tys_houhou_no = SUBMHK7.tys_houhou_no ")
            .AppendLine("		WHERE  ")
            .AppendLine("			MHKM.aitesaki_syubetu = 0  ")
            .AppendLine("			AND SUBMHK1.aitesaki_cd IS NULL ")
            .AppendLine("			AND SUBMHK5.aitesaki_cd IS NULL  ")
            .AppendLine("			AND SUBMHK7.aitesaki_cd IS NULL ")
            .AppendLine("	) AS SUBMHK ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("		( ")
            .AppendLine("			SELECT  ")
            .AppendLine("				0 AS aitesaki_syubetu,  ")
            .AppendLine("				'ALL' AS aitesaki_cd,  ")
            .AppendLine("				'�����Ȃ�' AS aitesaki_mei,  ")
            .AppendLine("				0 AS torikesi ")
            .AppendLine("			UNION ALL ")
            .AppendLine("			SELECT  ")
            .AppendLine("				1 AS aitesaki_syubetu,  ")
            .AppendLine("				MK.kameiten_cd AS aitesaki_cd,  ")
            .AppendLine("				MK.kameiten_mei1 AS aitesaki_mei,  ")
            .AppendLine("				MK.torikesi AS torikesi ")
            .AppendLine("			FROM m_kameiten AS MK WITH(READCOMMITTED) ")        '�����X�}�X�^
            .AppendLine("			UNION ALL ")
            .AppendLine("			SELECT  ")
            .AppendLine("				5 AS aitesaki_syubetu,  ")
            .AppendLine("				ME.eigyousyo_cd AS aitesaki_cd,  ")
            .AppendLine("				ME.eigyousyo_mei AS aitesaki_mei,  ")
            .AppendLine("				ME.torikesi AS torikesi ")
            .AppendLine("			FROM m_eigyousyo AS ME WITH(READCOMMITTED) ")       '�c�Ə��}�X�^
            .AppendLine("			UNION ALL  ")
            .AppendLine("			SELECT  ")
            .AppendLine("				7 AS aitesaki_syubetu,  ")
            .AppendLine("				MK.keiretu_cd AS aitesaki_cd,  ")
            .AppendLine("				MIN(MK.keiretu_mei) AS aitesaki_mei,  ")
            .AppendLine("				MIN(MK.torikesi) AS torikesi ")
            .AppendLine("			FROM m_keiretu AS MK WITH(READCOMMITTED)  ")        '�n��}�X�^
            .AppendLine("			GROUP BY  ")
            .AppendLine("				MK.keiretu_cd ")
            .AppendLine("		) AS SUB ")
            .AppendLine("		ON SUBMHK.aitesaki_syubetu = SUB.aitesaki_syubetu  ")
            .AppendLine("			AND SUBMHK.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine("	LEFT JOIN m_syouhin AS MS WITH(READCOMMITTED) ")        '���i�}�X�^
            .AppendLine("		ON SUBMHK.syouhin_cd = MS.syouhin_cd  ")
            .AppendLine("			AND MS.souko_cd = '100'  ")
            .AppendLine("			AND MS.torikesi = 0 ")
            .AppendLine("	LEFT JOIN m_tyousahouhou AS MT WITH(READCOMMITTED) ")       '�������@�}�X�^
            .AppendLine("		ON SUBMHK.tys_houhou_no = MT.tys_houhou_no ")
            .AppendLine("	LEFT JOIN 	m_kakutyou_meisyou AS MKM WITH(READCOMMITTED) ")        '�g�����̃}�X�^
            .AppendLine("		ON SUBMHK.aitesaki_syubetu = MKM.code  ")
            .AppendLine("			AND MKM.meisyou_syubetu = '23' ")
            .AppendLine("WHERE(1=1)  ")
            '���i�R�[�h
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                .AppendLine("AND SUBMHK.syouhin_cd = @syouhin_cd  ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuInfo(0).syouhin_cd))
            End If

            '�������@
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                .AppendLine("AND SUBMHK.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).tys_houhou_no))
            End If

            '���
            '============================2011/04/26 �ԗ� �폜 �J�n��=====================================
            'If dtHanbaiKakakuInfo(0).torikesi <> String.Empty Then
            '============================2011/04/26 �ԗ� �폜 �I����=====================================
            .AppendLine("AND SUBMHK.torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, 0))
            '============================2011/04/26 �ԗ� �폜 �J�n��=====================================
            'End If
            '============================2011/04/26 �ԗ� �폜 �I����=====================================

            '��������
            If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                .AppendLine("AND SUB.torikesi = @torikesi_aitesaki ")
                paramList.Add(MakeParam("@torikesi_aitesaki", SqlDbType.Int, 4, 0))
            End If

            '=======================2011/05/18 �ԗ� �d�l�ύX �ǉ� �J�n��==========================
            '\0�͑ΏۊO
            If dtHanbaiKakakuInfo(0).kingaku_0_taisyou_gai <> String.Empty Then
                .AppendLine(" AND SUBMHK.jitu_seikyuu_gaku <> 0 ")
            End If
            '=======================2011/05/18 �ԗ� �d�l�ύX �ǉ� �I����==========================

        End With

        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    "dtHanbaiKakakuCount", paramList.ToArray)

        Return dsHanbaiKakaku.Tables("dtHanbaiKakakuCount").Rows(0).Item("count")

    End Function
    ''' <summary>���ݒ���܂ޔ̔����iCSV�f�[�^�������擾����</summary>
    ''' <param name="dtHanbaiKakakuInfo">�p�����[�^</param>
    ''' <returns>���ݒ���܂ޔ̔����iCSV�f�[�^����</returns>
    Public Function SelMiSeteiHanbaiKakakuCSVCount(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
           As Long

        'DataSet�C���X�^���X�̐���
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT_BIG(*) AS count ")
            .AppendLine(" FROM ")
            .AppendLine("   (SELECT ")
            .AppendLine("       tys_houhou_no ")
            .AppendLine("       ,tys_houhou_mei ")
            .AppendLine("    FROM m_tyousahouhou WITH(READCOMMITTED) ")     '�������@�}�X�^
            .AppendLine("    WHERE (kakaku_settei_fuyou_flg IS NULL OR kakaku_settei_fuyou_flg = 0) ")
            '�������@
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                .AppendLine(" AND tys_houhou_no = @tys_houhou_no ")
            End If
            .AppendLine("   ) MT ")
            .AppendLine(" CROSS JOIN ")
            .AppendLine("   (SELECT ")
            .AppendLine("       syouhin_cd ")
            .AppendLine("       ,syouhin_mei ")
            .AppendLine("    FROM m_syouhin WITH(READCOMMITTED) ")          '���i�}�X�^
            .AppendLine("    WHERE souko_cd = '100' ")
            .AppendLine("    AND torikesi = 0 ")
            '���i�R�[�h
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                .AppendLine(" AND syouhin_cd = @syouhin_cd ")
            End If
            .AppendLine("   ) MS ")
            .AppendLine(" CROSS JOIN ( ")
            Select Case dtHanbaiKakakuInfo(0).aitesaki_syubetu
                Case 0
                    .AppendLine(" SELECT ")
                    .AppendLine("   0 AS aitesaki_syubetu ")
                    .AppendLine("   ,'ALL' AS aitesaki_cd ")
                    .AppendLine("   ,'�����Ȃ�' AS aitesaki_mei ")
                Case 1
                    .AppendLine(" SELECT ")
                    .AppendLine("   1 AS aitesaki_syubetu ")
                    .AppendLine("   ,kameiten_cd AS aitesaki_cd ")
                    .AppendLine("   ,kameiten_mei1 AS aitesaki_mei ")
                    .AppendLine(" FROM ")
                    .AppendLine("   m_kameiten WITH(READCOMMITTED) ")   '�����X�}�X�^
                    .AppendLine(" WHERE 1=1 ")
                    '�����R�[�h
                    If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND kameiten_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                        .AppendLine(" AND kameiten_cd = @aitesaki_cd_from ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND kameiten_cd = @aitesaki_cd_to ")
                    End If
                    '��������
                    If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                        .AppendLine(" AND torikesi = 0 ")
                    End If
                Case 5
                    .AppendLine(" SELECT ")
                    .AppendLine("   5 AS aitesaki_syubetu ")
                    .AppendLine("   ,eigyousyo_cd AS aitesaki_cd ")
                    .AppendLine("   ,eigyousyo_mei AS aitesaki_mei ")
                    .AppendLine(" FROM ")
                    .AppendLine("   m_eigyousyo WITH(READCOMMITTED) ")   '�c�Ə��}�X�^
                    .AppendLine(" WHERE 1=1 ")
                    '�����R�[�h
                    If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND eigyousyo_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                        .AppendLine(" AND eigyousyo_cd = @aitesaki_cd_from ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND eigyousyo_cd = @aitesaki_cd_to ")
                    End If
                    '��������
                    If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                        .AppendLine(" AND torikesi = 0 ")
                    End If
                Case 7
                    .AppendLine(" SELECT ")
                    .AppendLine("   7 AS aitesaki_syubetu ")
                    .AppendLine("   ,keiretu_cd AS aitesaki_cd ")
                    .AppendLine("   ,MIN(keiretu_mei) AS aitesaki_mei ")
                    .AppendLine(" FROM ")
                    .AppendLine("   m_keiretu WITH(READCOMMITTED) ")   '�n��}�X�^
                    .AppendLine(" WHERE 1=1 ")
                    '�����R�[�h
                    If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND keiretu_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                        .AppendLine(" AND keiretu_cd = @aitesaki_cd_from ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND keiretu_cd = @aitesaki_cd_to ")
                    End If
                    '��������
                    If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                        .AppendLine(" AND torikesi = 0 ")
                    End If
                    .AppendLine(" GROUP BY ")
                    .AppendLine("   keiretu_cd ")
            End Select
            .AppendLine(" ) SUB ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_hanbai_kakaku MHK WITH(READCOMMITTED) ")     '�̔����iM
            .AppendLine(" ON    MS.syouhin_cd = MHK.syouhin_cd ")
            .AppendLine(" AND   MT.tys_houhou_no = MHK.tys_houhou_no ")
            .AppendLine(" AND   SUB.aitesaki_syubetu = MHK.aitesaki_syubetu ")
            .AppendLine(" AND   SUB.aitesaki_cd = MHK.aitesaki_cd ")

            '�����R�[�h
            If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            End If

            '���i�R�[�h
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuInfo(0).syouhin_cd))
            End If

            '�������@
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).tys_houhou_no))
            End If

        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    "dtHanbaiKakakuCsvCount", paramList.ToArray)

        Return dsHanbaiKakaku.Tables("dtHanbaiKakakuCsvCount").Rows(0).Item("count")

    End Function
    ''' <summary>���ݒ���܂ޔ̔����iCSV�f�[�^���擾����</summary>
    ''' <param name="dtHanbaiKakakuInfo">�p�����[�^</param>
    ''' <returns>���ݒ���܂ޔ̔����iCSV�f�[�^�e�[�u��</returns>
    Public Function SelMiSeteiHanbaiKakakuCSVInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
           As HanbaiKakakuMasterDataSet.HanbaiKakakuCSVInfoTableDataTable

        'DataSet�C���X�^���X�̐���
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            'EDI���쐬��
            .AppendLine("   RIGHT(CONVERT(varchar(10),GETDATE(),112),4) + REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date ")
            .AppendLine("   ,SUB.aitesaki_syubetu ")                        '�T�u.�������
            .AppendLine("   ,SUB.aitesaki_cd ")                             '�T�u.�����R�[�h
            .AppendLine("   ,SUB.aitesaki_mei ")                            '�T�u.����於
            .AppendLine("   ,MS.syouhin_cd ")                               '���iM.���i�R�[�h
            .AppendLine("   ,MS.syouhin_mei ")                              '���iM.���i��
            .AppendLine("   ,MT.tys_houhou_no ")                            '�������@M.�������@NO
            .AppendLine("   ,MT.tys_houhou_mei ")                           '�������@M.�������@
            .AppendLine("   ,MHK.torikesi ")                                '�̔����iM.���
            .AppendLine("   ,MHK.koumuten_seikyuu_gaku ")                   '�̔����iM.�H���X�������z
            .AppendLine("   ,MHK.koumuten_seikyuu_gaku_henkou_flg ")        '�̔����iM.�H���X�������z�ύXFLG
            .AppendLine("   ,MHK.jitu_seikyuu_gaku ")                       '�̔����iM.���������z
            .AppendLine("   ,MHK.jitu_seikyuu_gaku_henkou_flg ")            '�̔����iM.���������z�ύXFLG
            .AppendLine("   ,MHK.koukai_flg ")                              '�̔����iM.���J�t���O
            .AppendLine(" FROM ")
            .AppendLine("   (SELECT ")
            .AppendLine("       tys_houhou_no ")
            .AppendLine("       ,tys_houhou_mei ")
            .AppendLine("    FROM m_tyousahouhou WITH(READCOMMITTED) ")     '�������@�}�X�^
            .AppendLine("    WHERE (kakaku_settei_fuyou_flg IS NULL OR kakaku_settei_fuyou_flg = 0) ")
            '�������@
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                .AppendLine(" AND tys_houhou_no = @tys_houhou_no ")
            End If
            .AppendLine("   ) MT ")
            .AppendLine(" CROSS JOIN ")
            .AppendLine("   (SELECT ")
            .AppendLine("       syouhin_cd ")
            .AppendLine("       ,syouhin_mei ")
            .AppendLine("    FROM m_syouhin WITH(READCOMMITTED) ")          '���i�}�X�^
            .AppendLine("    WHERE souko_cd = '100' ")
            .AppendLine("    AND torikesi = 0 ")
            '���i�R�[�h
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                .AppendLine(" AND syouhin_cd = @syouhin_cd ")
            End If
            .AppendLine("   ) MS ")
            .AppendLine(" CROSS JOIN ( ")
            Select Case dtHanbaiKakakuInfo(0).aitesaki_syubetu
                Case 0
                    .AppendLine(" SELECT ")
                    .AppendLine("   0 AS aitesaki_syubetu ")
                    .AppendLine("   ,'ALL' AS aitesaki_cd ")
                    .AppendLine("   ,'�����Ȃ�' AS aitesaki_mei ")
                Case 1
                    .AppendLine(" SELECT ")
                    .AppendLine("   1 AS aitesaki_syubetu ")
                    .AppendLine("   ,kameiten_cd AS aitesaki_cd ")
                    .AppendLine("   ,kameiten_mei1 AS aitesaki_mei ")
                    .AppendLine(" FROM ")
                    .AppendLine("   m_kameiten WITH(READCOMMITTED) ")   '�����X�}�X�^
                    .AppendLine(" WHERE 1=1 ")
                    '�����R�[�h
                    If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND kameiten_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                        .AppendLine(" AND kameiten_cd = @aitesaki_cd_from ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND kameiten_cd = @aitesaki_cd_to ")
                    End If
                    '��������
                    If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                        .AppendLine(" AND torikesi = 0 ")
                    End If
                Case 5
                    .AppendLine(" SELECT ")
                    .AppendLine("   5 AS aitesaki_syubetu ")
                    .AppendLine("   ,eigyousyo_cd AS aitesaki_cd ")
                    .AppendLine("   ,eigyousyo_mei AS aitesaki_mei ")
                    .AppendLine(" FROM ")
                    .AppendLine("   m_eigyousyo WITH(READCOMMITTED) ")   '�c�Ə��}�X�^
                    .AppendLine(" WHERE 1=1 ")
                    '�����R�[�h
                    If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND eigyousyo_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                        .AppendLine(" AND eigyousyo_cd = @aitesaki_cd_from ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND eigyousyo_cd = @aitesaki_cd_to ")
                    End If
                    '��������
                    If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                        .AppendLine(" AND torikesi = 0 ")
                    End If
                Case 7
                    .AppendLine(" SELECT ")
                    .AppendLine("   7 AS aitesaki_syubetu ")
                    .AppendLine("   ,keiretu_cd AS aitesaki_cd ")
                    .AppendLine("   ,MIN(keiretu_mei) AS aitesaki_mei ")
                    .AppendLine(" FROM ")
                    .AppendLine("   m_keiretu WITH(READCOMMITTED) ")   '�n��}�X�^
                    .AppendLine(" WHERE 1=1 ")
                    '�����R�[�h
                    If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND keiretu_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                        .AppendLine(" AND keiretu_cd = @aitesaki_cd_from ")
                    ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                        .AppendLine(" AND keiretu_cd = @aitesaki_cd_to ")
                    End If
                    '��������
                    If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                        .AppendLine(" AND torikesi = 0 ")
                    End If
                    .AppendLine(" GROUP BY ")
                    .AppendLine("   keiretu_cd ")
            End Select
            .AppendLine(" ) SUB ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_hanbai_kakaku MHK WITH(READCOMMITTED) ")     '�̔����iM
            .AppendLine(" ON    MS.syouhin_cd = MHK.syouhin_cd ")
            .AppendLine(" AND   MT.tys_houhou_no = MHK.tys_houhou_no ")
            .AppendLine(" AND   SUB.aitesaki_syubetu = MHK.aitesaki_syubetu ")
            .AppendLine(" AND   SUB.aitesaki_cd = MHK.aitesaki_cd ")

            '�����R�[�h
            If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            End If

            '���i�R�[�h
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuInfo(0).syouhin_cd))
            End If

            '�������@
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).tys_houhou_no))
            End If

            '�\�[�g��
            .AppendLine(" ORDER BY ")
            .AppendLine("      SUB.aitesaki_syubetu ")
            .AppendLine("      ,SUB.aitesaki_cd ")
            .AppendLine("      ,MS.syouhin_cd ")
            .AppendLine("      ,MT.tys_houhou_no ")
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    dsHanbaiKakaku.HanbaiKakakuCSVInfoTable.TableName, paramList.ToArray)

        Return dsHanbaiKakaku.HanbaiKakakuCSVInfoTable

    End Function
    ''' <summary>�̔����iCSV�f�[�^���擾����</summary>
    ''' <param name="dtHanbaiKakakuInfo">�p�����[�^</param>
    ''' <returns>�̔����iCSV�f�[�^�e�[�u��</returns>
    Public Function SelHanbaiKakakuCSVInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
           As HanbaiKakakuMasterDataSet.HanbaiKakakuCSVInfoTableDataTable

        'DataSet�C���X�^���X�̐���
        Dim dsHanbaiKakaku As New HanbaiKakakuMasterDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            'EDI���쐬��
            .AppendLine("   RIGHT(CONVERT(varchar(10),GETDATE(),112),4) + REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date ")
            .AppendLine("   ,MHK.aitesaki_syubetu ")                        '�̔����iM.�������
            .AppendLine("   ,MHK.aitesaki_cd ")                             '�̔����iM.�����R�[�h
            .AppendLine("   ,SUB.aitesaki_mei ")                            '�T�u.����於
            .AppendLine("   ,MHK.syouhin_cd ")                              '�̔����iM.���i�R�[�h
            .AppendLine("   ,MS.syouhin_mei ")                              '���iM.���i��
            .AppendLine("   ,MHK.tys_houhou_no ")                           '�̔����iM.�������@NO
            .AppendLine("   ,MT.tys_houhou_mei ")                           '�������@M.�������@
            .AppendLine("   ,MHK.torikesi ")                                '�̔����iM.���
            .AppendLine("   ,MHK.koumuten_seikyuu_gaku ")                   '�̔����iM.�H���X�������z
            .AppendLine("   ,MHK.koumuten_seikyuu_gaku_henkou_flg ")        '�̔����iM.�H���X�������z�ύXFLG
            .AppendLine("   ,MHK.jitu_seikyuu_gaku ")                       '�̔����iM.���������z
            .AppendLine("   ,MHK.jitu_seikyuu_gaku_henkou_flg ")            '�̔����iM.���������z�ύXFLG
            .AppendLine("   ,MHK.koukai_flg ")                              '�̔����iM.���J�t���O
            .AppendLine(" FROM ")
            .AppendLine("   m_hanbai_kakaku MHK WITH(READCOMMITTED) ")      '�̔����iM
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_kakutyou_meisyou MKM WITH(READCOMMITTED) ")   '�g������M
            .AppendLine(" ON    MHK.aitesaki_syubetu = MKM.code ")
            .AppendLine(" AND   MKM.meisyou_syubetu = 23 ")
            .AppendLine(" LEFT OUTER JOIN ( ")
            .AppendLine("   SELECT ")
            .AppendLine("       0 AS aitesaki_syubetu ")
            .AppendLine("       ,'ALL' AS aitesaki_cd ")
            .AppendLine("       ,'�����Ȃ�' AS aitesaki_mei ")
            .AppendLine("       ,0 AS torikesi ")
            .AppendLine("   UNION ALL ")
            .AppendLine("   SELECT ")
            .AppendLine("       1 AS aitesaki_syubetu ")
            .AppendLine("       ,kameiten_cd AS aitesaki_cd ")
            .AppendLine("       ,kameiten_mei1 AS aitesaki_mei ")
            .AppendLine("       ,torikesi ")
            .AppendLine("   FROM ")
            .AppendLine("       m_kameiten WITH(READCOMMITTED) ")   '�����X�}�X�^
            .AppendLine("   UNION ALL ")
            .AppendLine("   SELECT ")
            .AppendLine("       5 AS aitesaki_syubetu ")
            .AppendLine("       ,eigyousyo_cd AS aitesaki_cd ")
            .AppendLine("       ,eigyousyo_mei AS aitesaki_mei ")
            .AppendLine("       ,torikesi ")
            .AppendLine("   FROM ")
            .AppendLine("       m_eigyousyo WITH(READCOMMITTED) ")   '�c�Ə��}�X�^
            .AppendLine("   UNION ALL ")
            .AppendLine("   SELECT ")
            .AppendLine("       7 AS aitesaki_syubetu ")
            .AppendLine("       ,keiretu_cd AS aitesaki_cd ")
            .AppendLine("       ,MIN(keiretu_mei) AS aitesaki_mei ")
            .AppendLine("       ,MIN(torikesi) AS torikesi ")
            .AppendLine("   FROM ")
            .AppendLine("       m_keiretu WITH(READCOMMITTED) ")   '�n��}�X�^
            .AppendLine("   GROUP BY ")
            .AppendLine("       keiretu_cd ")
            .AppendLine("   ) SUB ")
            .AppendLine(" ON    MHK.aitesaki_syubetu = SUB.aitesaki_syubetu ")
            .AppendLine(" AND   MHK.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_syouhin MS WITH(READCOMMITTED) ")   '���i�}�X�^
            .AppendLine(" ON    MHK.syouhin_cd = MS.syouhin_cd ")
            .AppendLine(" AND   MS.souko_cd = '100' ")
            .AppendLine(" AND   MS.torikesi = 0 ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_tyousahouhou MT WITH(READCOMMITTED) ")   '�������@�}�X�^
            .AppendLine(" ON    MHK.tys_houhou_no = MT.tys_houhou_no ")
            .AppendLine(" WHERE ")

            '�������
            .AppendLine(" MHK.aitesaki_syubetu = @aitesaki_syubetu ")
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).aitesaki_syubetu))

            '�����R�[�h
            If dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from <> String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to = String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_from ")
                paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_from))
            ElseIf dtHanbaiKakakuInfo(0).aitesaki_cd_from = String.Empty And dtHanbaiKakakuInfo(0).aitesaki_cd_to <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_to ")
                paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtHanbaiKakakuInfo(0).aitesaki_cd_to))
            End If

            '���i�R�[�h
            If dtHanbaiKakakuInfo(0).syouhin_cd <> String.Empty Then
                .AppendLine(" AND MHK.syouhin_cd = @syouhin_cd ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuInfo(0).syouhin_cd))
            End If

            '�������@
            If dtHanbaiKakakuInfo(0).tys_houhou_no <> String.Empty Then
                .AppendLine(" AND MHK.tys_houhou_no = @tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuInfo(0).tys_houhou_no))
            End If

            '���
            If dtHanbaiKakakuInfo(0).torikesi <> String.Empty Then
                .AppendLine(" AND MHK.torikesi = @torikesi ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, 0))
            End If

            '��������
            If dtHanbaiKakakuInfo(0).torikesi_aitesaki <> String.Empty Then
                .AppendLine(" AND SUB.torikesi = @torikesi_aitesaki ")
                paramList.Add(MakeParam("@torikesi_aitesaki", SqlDbType.Int, 4, 0))
            End If

            '=======================2011/05/18 �ԗ� �d�l�ύX �ǉ� �J�n��==========================
            '\0�͑ΏۊO
            If dtHanbaiKakakuInfo(0).kingaku_0_taisyou_gai <> String.Empty Then
                .AppendLine(" AND MHK.jitu_seikyuu_gaku <> 0 ")
            End If
            '=======================2011/05/18 �ԗ� �d�l�ύX �ǉ� �I����==========================

            '�\�[�g��
            .AppendLine(" ORDER BY ")
            .AppendLine("      MHK.aitesaki_syubetu ")
            .AppendLine("      ,MHK.aitesaki_cd ")
            .AppendLine("      ,MHK.syouhin_cd ")
            .AppendLine("      ,MHK.tys_houhou_no ")

        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, _
                    dsHanbaiKakaku.HanbaiKakakuCSVInfoTable.TableName, paramList.ToArray)

        Return dsHanbaiKakaku.HanbaiKakakuCSVInfoTable

    End Function
    ''' <summary>�A�b�v���[�h�Ǘ����擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��f�[�^�e�[�u��</returns>
    Public Function SelUploadKanri() As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsHanbaiKakaku As New Data.DataSet

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
            .AppendLine("	file_kbn = 3 ")         '�t�@�C���敪
            .AppendLine(" ORDER BY ")
            .AppendLine("	syori_datetime DESC ")  '��������(�~��)
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri")

        Return dsHanbaiKakaku.Tables("dtUploadKanri")

    End Function
    ''' <summary>�A�b�v���[�h�Ǘ��̌������擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��̌���</returns>
    Public Function SelUploadKanriCount() As Integer

        'DataSet�C���X�^���X�̐���
        Dim dsHanbaiKakaku As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("	COUNT(*) AS count ")                    '����
            .AppendLine(" FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")   '�A�b�v���[�h�Ǘ��e�[�u��
            .AppendLine(" WHERE ")
            .AppendLine("	file_kbn = 3 ")                         '�t�@�C���敪
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri")

        Return CInt(dsHanbaiKakaku.Tables("dtUploadKanri").Rows(0).Item("count").ToString)

    End Function
    ''' <summary>�����i��ʁE�R�[�h�j���݃`�F�b�N</summary>
    ''' <param name="strAitesakiSyubetu">�������</param>
    ''' <param name="strAitesakiCd">�����R�[�h</param>
    ''' <returns>�����i��ʁE�R�[�h�j���݋敪</returns>
    Public Function SelAitesaki(ByVal strAitesakiSyubetu As String, ByVal strAitesakiCd As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsAitesaki As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("	aitesaki_syubetu ") '�������
            .AppendLine("	,aitesaki_cd ")     '�����R�[�h
            .AppendLine(" FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			0				AS aitesaki_syubetu ")  '�������
            .AppendLine("			,'ALL'			AS aitesaki_cd ")       '�����R�[�h
            .AppendLine("			,'�����Ȃ�'	AS aitesaki_mei ")      '����於
            .AppendLine("			,0				AS torikesi ")          '���
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			1				AS aitesaki_syubetu ")  '�������
            .AppendLine("			,kameiten_cd	AS aitesaki_cd ")       '�����X�R�[�h
            .AppendLine("			,kameiten_mei1	AS aitesaki_mei ")      '�����X��1
            .AppendLine("			,torikesi		AS torikesi ")          '���
            .AppendLine("		FROM ")
            .AppendLine("			m_kameiten WITH(READCOMMITTED) ")       '�����X�}�X�^
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			5				AS aitesaki_syubetu ")  '�������
            .AppendLine("			,eigyousyo_cd	AS aitesaki_cd ")       '�c�Ə��R�[�h
            .AppendLine("			,eigyousyo_mei	AS aitesaki_mei ")      '�c�Ə���
            .AppendLine("			,torikesi		AS torikesi ")          '���
            .AppendLine("		FROM ")
            .AppendLine("			m_eigyousyo WITH(READCOMMITTED) ")      '�c�Ə��}�X�^
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			7				AS aitesaki_syubetu ")  '�������
            .AppendLine("			,keiretu_cd		AS aitesaki_cd ")       '�n��R�[�h
            .AppendLine("			,MIN(keiretu_mei)	AS aitesaki_mei ")  '�n��
            .AppendLine("			,MIN(torikesi)		AS torikesi ")      '���
            .AppendLine("		FROM ")
            .AppendLine("			m_keiretu WITH(READCOMMITTED) ")        '�n��}�X�^
            .AppendLine("		GROUP BY ")
            .AppendLine("			keiretu_cd ")                           '�n��R�[�h
            .AppendLine("	) AS TA ")
            .AppendLine(" WHERE aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine(" AND   aitesaki_cd = @aitesaki_cd ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, strAitesakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAitesakiCd))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsAitesaki, "dtAitesaki", paramList.ToArray)

        '�߂�l
        If dsAitesaki.Tables("dtAitesaki").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>���i�R�[�h���݃`�F�b�N</summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <returns>���i�R�[�h���݋敪</returns>
    Public Function SelSyouhinCd(ByVal strSyouhinCd As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsSyouhin As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	syouhin_cd ")
            .AppendLine("FROM  ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
        End With

        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsSyouhin, "dtSyouhinCd", paramList.ToArray)

        '�߂�l
        If dsSyouhin.Tables("dtSyouhinCd").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>�������@NO���݃`�F�b�N</summary>
    ''' <param name="strTysHouhouNo">�������@NO</param>
    ''' <returns>�������@NO���݋敪</returns>
    Public Function SelTysHouhou(ByVal strTysHouhouNo As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsTysHouhou As New Data.DataSet

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

        paramList.Add(MakeParam("@TyousahouhouNo", SqlDbType.Int, 4, strTysHouhouNo))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTysHouhou, "dtTysHouhou", paramList.ToArray)

        '�߂�l
        If dsTysHouhou.Tables("dtTysHouhou").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>�̔����i�f�[�^���݃`�F�b�N</summary>
    ''' <param name="strAitesakiSyubetu">�������</param>
    ''' <param name="strAitesakiCd">�����R�[�h</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="strTysHouhouNo">�������@NO</param>
    ''' <returns>�̔����i�f�[�^���݋敪</returns>
    Public Function SelHanbaiKakaku(ByVal strAitesakiSyubetu As String, ByVal strAitesakiCd As String, _
                                    ByVal strSyouhinCd As String, ByVal strTysHouhouNo As Integer) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsHanbaiKakaku As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   aitesaki_syubetu ")
            .AppendLine(" FROM ")
            .AppendLine("	m_hanbai_kakaku WITH(READCOMMITTED) ")
            .AppendLine(" WHERE ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine(" AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
            .AppendLine(" AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine(" AND ")
            .AppendLine("	tys_houhou_no = @tys_houhou_no ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, strAitesakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAitesakiCd))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, strTysHouhouNo))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtHanbaiKakaku", paramList.ToArray)

        '�߂�l
        If dsHanbaiKakaku.Tables("dtHanbaiKakaku").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>�̔����i�G���[���e�[�u����o�^����</summary>
    ''' <param name="dtHanbaiKakakuError">�G���[�f�[�^���</param>
    ''' <returns>�������s�敪</returns>
    Public Function InsHanbaiKakakuError(ByVal dtHanbaiKakakuError As Data.DataTable, _
                                         ByVal strUploadDate As String, _
                                         ByVal strUserId As String) As Boolean

        '�߂�l
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" INSERT INTO  ")
            .AppendLine("   m_hanbai_kakaku_error WITH(UPDLOCK) ")
            .AppendLine("   ( ")
            .AppendLine("		edi_jouhou_sakusei_date ")              'EDI���쐬��
            .AppendLine("		,gyou_no ")                             '�sNO
            .AppendLine("		,syori_datetime ")                      '��������
            .AppendLine("		,aitesaki_syubetu ")                    '�������
            .AppendLine("		,aitesaki_cd ")                         '�����R�[�h
            .AppendLine("		,aitesaki_mei ")                        '����於
            .AppendLine("		,syouhin_cd ")                          '���i�R�[�h
            .AppendLine("		,syouhin_mei ")                         '���i��
            .AppendLine("		,tys_houhou_no ")                       '�������@NO
            .AppendLine("		,tys_houhou ")                          '�������@
            .AppendLine("		,torikesi ")                            '���
            .AppendLine("		,koumuten_seikyuu_gaku ")               '�H���X�������z
            .AppendLine("		,koumuten_seikyuu_gaku_henkou_flg ")    '�H���X�������z�ύXFLG
            .AppendLine("		,jitu_seikyuu_gaku ")                   '���������z
            .AppendLine("		,jitu_seikyuu_gaku_henkou_flg ")        '���������z�ύXFLG
            .AppendLine("		,koukai_flg ")                          '���J�t���O
            .AppendLine("		,add_login_user_id ")                   '�o�^���O�C�����[�UID
            .AppendLine("		,add_datetime ")                        '�o�^����
            .AppendLine("		,upd_login_user_id ")                   '�X�V���O�C�����[�UID
            .AppendLine("		,upd_datetime ")                        '�X�V����
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@edi_jouhou_sakusei_date ")
            .AppendLine("	,@gyou_no ")
            .AppendLine("	,@syori_datetime ")
            .AppendLine("	,@aitesaki_syubetu ")
            .AppendLine("	,@aitesaki_cd ")
            .AppendLine("	,@aitesaki_mei ")
            .AppendLine("	,@syouhin_cd ")
            .AppendLine("	,@syouhin_mei ")
            .AppendLine("	,@tys_houhou_no ")
            .AppendLine("	,@tys_houhou ")
            .AppendLine("	,@torikesi ")
            .AppendLine("	,@koumuten_seikyuu_gaku ")
            .AppendLine("	,@koumuten_seikyuu_gaku_henkou_flg ")
            .AppendLine("	,@jitu_seikyuu_gaku ")
            .AppendLine("	,@jitu_seikyuu_gaku_henkou_flg ")
            .AppendLine("	,@koukai_flg ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(") ")
        End With

        For i As Integer = 0 To dtHanbaiKakakuError.Rows.Count - 1

            '�p�����[�^�̐ݒ�
            paramList.Clear()
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, dtHanbaiKakakuError.Rows(i).Item(0).ToString.Trim))
            paramList.Add(MakeParam("@gyou_no", SqlDbType.Int, 4, dtHanbaiKakakuError.Rows(i).Item(14).ToString.Trim))
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strUploadDate))))
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.VarChar, 5, dtHanbaiKakakuError.Rows(i).Item(1).ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtHanbaiKakakuError.Rows(i).Item(2).ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_mei", SqlDbType.VarChar, 40, dtHanbaiKakakuError.Rows(i).Item(3).ToString.Trim))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuError.Rows(i).Item(4).ToString.Trim))
            paramList.Add(MakeParam("@syouhin_mei", SqlDbType.VarChar, 40, dtHanbaiKakakuError.Rows(i).Item(5).ToString.Trim))
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.VarChar, 5, dtHanbaiKakakuError.Rows(i).Item(6).ToString.Trim))
            paramList.Add(MakeParam("@tys_houhou", SqlDbType.VarChar, 32, dtHanbaiKakakuError.Rows(i).Item(7).ToString.Trim))
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, dtHanbaiKakakuError.Rows(i).Item(8).ToString.Trim))
            paramList.Add(MakeParam("@koumuten_seikyuu_gaku", SqlDbType.VarChar, 10, dtHanbaiKakakuError.Rows(i).Item(9).ToString.Trim))
            paramList.Add(MakeParam("@koumuten_seikyuu_gaku_henkou_flg", SqlDbType.VarChar, 1, dtHanbaiKakakuError.Rows(i).Item(10).ToString.Trim))
            paramList.Add(MakeParam("@jitu_seikyuu_gaku", SqlDbType.VarChar, 10, dtHanbaiKakakuError.Rows(i).Item(11).ToString.Trim))
            paramList.Add(MakeParam("@jitu_seikyuu_gaku_henkou_flg", SqlDbType.VarChar, 1, dtHanbaiKakakuError.Rows(i).Item(12).ToString.Trim))
            paramList.Add(MakeParam("@koukai_flg", SqlDbType.VarChar, 1, dtHanbaiKakakuError.Rows(i).Item(13).ToString.Trim))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))

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
    ''' <summary>�̔����i�}�X�^��o�^�E�X�V����</summary>
    ''' <param name="dtHanbaiKakakuOk">�f�[�^���</param>
    ''' <returns>�������s�敪</returns>
    Public Function InsUpdHanbaiKakaku(ByVal dtHanbaiKakakuOk As Data.DataTable, _
                                       ByVal strUserId As String) As Boolean

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
            .AppendLine(" INSERT INTO  ")
            .AppendLine("	m_hanbai_kakaku WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		aitesaki_syubetu ")                     '�������
            .AppendLine("		,aitesaki_cd ")                         '�����R�[�h
            .AppendLine("		,syouhin_cd ")                          '���i�R�[�h
            .AppendLine("		,tys_houhou_no ")                       '�������@NO
            .AppendLine("		,torikesi ")                            '���
            .AppendLine("		,koumuten_seikyuu_gaku ")               '�H���X�������z
            .AppendLine("		,koumuten_seikyuu_gaku_henkou_flg ")    '�H���X�������z�ύXFLG
            .AppendLine("		,jitu_seikyuu_gaku ")                   '���������z
            .AppendLine("		,jitu_seikyuu_gaku_henkou_flg ")        '���������z�ύXFLG
            .AppendLine("		,koukai_flg ")                          '���J�t���O
            .AppendLine("		,add_login_user_id ")                   '�o�^���O�C�����[�UID
            .AppendLine("		,add_datetime ")                        '�o�^����
            .AppendLine("		,upd_login_user_id ")                   '�X�V���O�C�����[�UID
            .AppendLine("		,upd_datetime ")                        '�X�V����
            .AppendLine("	) ")
            .AppendLine(" VALUES ")
            .AppendLine(" ( ")
            .AppendLine("	@aitesaki_syubetu ")
            .AppendLine("	,@aitesaki_cd ")
            .AppendLine("	,@syouhin_cd ")
            .AppendLine("	,@tys_houhou_no ")
            .AppendLine("	,@torikesi ")
            .AppendLine("	,@koumuten_seikyuu_gaku ")
            .AppendLine("	,@koumuten_seikyuu_gaku_henkou_flg ")
            .AppendLine("	,@jitu_seikyuu_gaku ")
            .AppendLine("	,@jitu_seikyuu_gaku_henkou_flg ")
            .AppendLine("	,@koukai_flg ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(" ) ")
        End With

        '�X�V�psql��
        With strSqlUpd
            .AppendLine(" UPDATE ")
            .AppendLine("	m_hanbai_kakaku WITH(UPDLOCK) ")
            .AppendLine(" SET ")
            .AppendLine("	torikesi = @torikesi ")                             '���
            .AppendLine("	,koumuten_seikyuu_gaku = @koumuten_seikyuu_gaku ")  '�H���X�������z
            .AppendLine("	,koumuten_seikyuu_gaku_henkou_flg = @koumuten_seikyuu_gaku_henkou_flg ") '�H���X�������z�ύXFLG
            .AppendLine("	,jitu_seikyuu_gaku = @jitu_seikyuu_gaku ")          '���������z
            .AppendLine("	,jitu_seikyuu_gaku_henkou_flg = @jitu_seikyuu_gaku_henkou_flg ")    '���������z�ύXFLG
            .AppendLine("	,koukai_flg = @koukai_flg ")                    '���J�t���O
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")      '�X�V���O�C�����[�UID
            .AppendLine("	,upd_datetime = GETDATE() ")                    '�X�V����
            .AppendLine(" WHERE aitesaki_syubetu = @aitesaki_syubetu ")     '�������
            .AppendLine(" AND   aitesaki_cd = @aitesaki_cd ")               '�����R�[�h
            .AppendLine(" AND   syouhin_cd = @syouhin_cd ")                 '���i�R�[�h
            .AppendLine(" AND   tys_houhou_no = @tys_houhou_no ")           '�������@NO
        End With

        For i As Integer = 0 To dtHanbaiKakakuOk.Rows.Count - 1
            '�p�����[�^�̐ݒ�
            paramList.Clear()
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("aitesaki_syubetu").ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtHanbaiKakakuOk.Rows(i).Item("aitesaki_cd").ToString.Trim))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtHanbaiKakakuOk.Rows(i).Item("syouhin_cd").ToString.Trim))
            paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("tys_houhou_no").ToString.Trim))
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("torikesi").ToString.Trim))
            If dtHanbaiKakakuOk.Rows(i).Item("koumuten_seikyuu_gaku").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@koumuten_seikyuu_gaku", SqlDbType.Int, 4, DBNull.Value))
            Else
                paramList.Add(MakeParam("@koumuten_seikyuu_gaku", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("koumuten_seikyuu_gaku").ToString.Trim))
            End If
            If dtHanbaiKakakuOk.Rows(i).Item("koumuten_seikyuu_gaku_henkou_flg").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@koumuten_seikyuu_gaku_henkou_flg", SqlDbType.Int, 4, DBNull.Value))
            Else
                paramList.Add(MakeParam("@koumuten_seikyuu_gaku_henkou_flg", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("koumuten_seikyuu_gaku_henkou_flg").ToString.Trim))
            End If
            If dtHanbaiKakakuOk.Rows(i).Item("jitu_seikyuu_gaku").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@jitu_seikyuu_gaku", SqlDbType.Int, 4, DBNull.Value))
            Else
                paramList.Add(MakeParam("@jitu_seikyuu_gaku", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("jitu_seikyuu_gaku").ToString.Trim))
            End If
            If dtHanbaiKakakuOk.Rows(i).Item("jitu_seikyuu_gaku_henkou_flg").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@jitu_seikyuu_gaku_henkou_flg", SqlDbType.Int, 4, DBNull.Value))
            Else
                paramList.Add(MakeParam("@jitu_seikyuu_gaku_henkou_flg", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("jitu_seikyuu_gaku_henkou_flg").ToString.Trim))
            End If
            If dtHanbaiKakakuOk.Rows(i).Item("koukai_flg").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@koukai_flg", SqlDbType.Int, 4, DBNull.Value))
            Else
                paramList.Add(MakeParam("@koukai_flg", SqlDbType.Int, 4, dtHanbaiKakakuOk.Rows(i).Item("koukai_flg").ToString.Trim))
            End If
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId))
            '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������

            Try
                If dtHanbaiKakakuOk.Rows(i).Item("ins_upd_flg").Equals("0") Then
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
    ''' <param name="strUploadDate">��������</param>
    ''' <param name="strNyuuryokuFileMei">���̓t�@�C����</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strErrorUmu">�G���[�L��</param>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <returns>�������s�敪</returns>
    Public Function InsUploadKanri(ByVal strUploadDate As String, _
                                   ByVal strNyuuryokuFileMei As String, _
                                   ByVal strEdiJouhouSakuseiDate As String, _
                                   ByVal strErrorUmu As Integer, _
                                   ByVal strUserId As String) As Boolean

        '�߂�l
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine(" INSERT INTO  ")
            .AppendLine("	t_upload_kanri ")
            .AppendLine("	( ")
            .AppendLine("       syori_datetime ")
            .AppendLine("		,nyuuryoku_file_mei ")
            .AppendLine("		,edi_jouhou_sakusei_date ")
            .AppendLine("		,error_umu ")
            .AppendLine("		,file_kbn ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine(" VALUES ")
            .AppendLine(" ( ")
            .AppendLine("	@syori_datetime ")
            .AppendLine("	,@nyuuryoku_file_mei ")
            .AppendLine("	,@edi_jouhou_sakusei_date ")
            .AppendLine("	,@error_umu ")
            .AppendLine("	,3 ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(" ) ")
        End With

        paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strUploadDate))))
        paramList.Add(MakeParam("@nyuuryoku_file_mei", SqlDbType.VarChar, 128, strNyuuryokuFileMei))
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))
        paramList.Add(MakeParam("@error_umu", SqlDbType.Int, 4, strErrorUmu))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))

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
    ''' <summary>�̔����i�G���[�����擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�̔����i�G���[�f�[�^�e�[�u��</returns>
    Public Function SelHanbaiKakakuErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) _
                        As HanbaiKakakuMasterDataSet.HanbaiKakakuErrTableDataTable

        'DataSet�C���X�^���X�̐���
        Dim dsHanbaiKakakuErr As New HanbaiKakakuMasterDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP 100 ")
            .AppendLine("   MHKE.gyou_no ")                 '�̔����i�G���[���T.�sNO
            '�̔����i�G���[���T.������ʁF�g��M.����
            .AppendLine("   ,CASE WHEN ISNULL(MHKE.aitesaki_syubetu,'') = '' THEN '' ")
            .AppendLine("         ELSE MHKE.aitesaki_syubetu + '�F' + ISNULL(MKM.meisyou,'') ")
            .AppendLine("    END AS aitesaki ")
            .AppendLine("   ,MHKE.aitesaki_cd ")            '�̔����i�G���[���T.�����R�[�h
            .AppendLine("   ,MHKE.aitesaki_mei ")           '�̔����i�G���[���T.����於
            .AppendLine("   ,MHKE.syouhin_cd ")             '�̔����i�G���[���T.���i�R�[�h
            .AppendLine("   ,MHKE.syouhin_mei ")            '�̔����i�G���[���T.���i��
            '�̔����i�G���[���T.�������@NO�F�̔����i�G���[���T.�������@
            .AppendLine("   ,CASE WHEN ISNULL(MHKE.tys_houhou_no,'') = '' AND ISNULL(MHKE.tys_houhou,'') = '' THEN '' ")
            .AppendLine("         ELSE MHKE.tys_houhou_no + '�F' + MHKE.tys_houhou ")
            .AppendLine("    END AS tys_houhou ")
            .AppendLine("   ,CASE MHKE.torikesi ")
            .AppendLine("       WHEN '0' THEN '' ")
            .AppendLine("       ELSE '���' ")
            .AppendLine("    END AS torikesi ")                 '�̔����i�G���[���T.���
            .AppendLine("   ,MHKE.koumuten_seikyuu_gaku ")      '�̔����i�G���[���T.�H���X�������z
            .AppendLine("   ,CASE ISNULL(MHKE.koumuten_seikyuu_gaku_henkou_flg,'0') ")
            .AppendLine("       WHEN '0' THEN '�ύX�s��' ")
            .AppendLine("       ELSE '�ύX��' ")
            .AppendLine("    END AS koumuten_seikyuu_gaku_henkou_flg ")     '�̔����i�G���[���T.�H���X�������z�ύXFLG
            .AppendLine("   ,MHKE.jitu_seikyuu_gaku ")     '�̔����i�G���[���T.���������z
            .AppendLine("   ,CASE ISNULL(MHKE.jitu_seikyuu_gaku_henkou_flg,'0') ")
            .AppendLine("       WHEN '0' THEN '�ύX�s��' ")
            .AppendLine("       ELSE '�ύX��' ")
            .AppendLine("    END AS jitu_seikyuu_gaku_henkou_flg ")     '�̔����i�G���[���T.���������z�ύXFLG
            .AppendLine("   ,CASE ISNULL(MHKE.koukai_flg,'0') ")
            .AppendLine("       WHEN '0' THEN '����J' ")
            .AppendLine("       ELSE '���J' ")
            .AppendLine("    END AS koukai_flg ")   '�̔����i�G���[���T.���J�t���O
            .AppendLine(" FROM ")
            .AppendLine("   m_hanbai_kakaku_error MHKE WITH(READCOMMITTED) ")   '�̔����i�G���[���T
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_kakutyou_meisyou MKM WITH(READCOMMITTED) ")   '�g������M
            .AppendLine(" ON    MHKE.aitesaki_syubetu = MKM.code ")
            .AppendLine(" AND   MKM.meisyou_syubetu = 23 ")
            .AppendLine(" WHERE ")

            'EDI���쐬��
            .AppendLine(" MHKE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))

            '��������
            .AppendLine(" AND CONVERT(varchar(100),MHKE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MHKE.syori_datetime,114),':','') = @syori_datetime")
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))

            '�\�[�g��
            .AppendLine(" ORDER BY ")
            .AppendLine("      MHKE.gyou_no ")
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakakuErr, _
                    dsHanbaiKakakuErr.HanbaiKakakuErrTable.TableName, paramList.ToArray)

        Return dsHanbaiKakakuErr.HanbaiKakakuErrTable

    End Function
    ''' <summary>�̔����i�G���[�������擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�̔����i�G���[����</returns>
    Public Function SelHanbaiKakakuErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As String

        'DataSet�C���X�^���X�̐���
        Dim dsHanbaiKakakuErr As New HanbaiKakakuMasterDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT(*) AS count ")
            .AppendLine(" FROM ")
            .AppendLine("   m_hanbai_kakaku_error WITH(READCOMMITTED) ")   '�̔����i�G���[���T
            .AppendLine(" WHERE ")

            'EDI���
            .AppendLine(" edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))

            '��������
            .AppendLine(" AND CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime")
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoriDate))

        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakakuErr, _
                    "dtHanbaiKakakuErr", paramList.ToArray)

        Return dsHanbaiKakakuErr.Tables("dtHanbaiKakakuErr").Rows(0).Item("count")

    End Function
    ''' <summary>�̔����i�G���[CSV�����擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�̔����i�G���[CSV�f�[�^�e�[�u��</returns>
    Public Function SelHanbaiKakakuErrCsv(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) _
                    As HanbaiKakakuMasterDataSet.HanbaiKakakuErrCSVTableDataTable

        'DataSet�C���X�^���X�̐���
        Dim dsHanbaiKakakuErr As New HanbaiKakakuMasterDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP 5000 ")
            .AppendLine("   MHKE.edi_jouhou_sakusei_date ")             '�̔����i�G���[���T.EDI���쐬��
            .AppendLine("   ,MHKE.gyou_no ")                            '�̔����i�G���[���T.�sNO
            .AppendLine("   ,MHKE.syori_datetime ")                     '�̔����i�G���[���T.��������
            .AppendLine("   ,MHKE.aitesaki_syubetu ")                   '�̔����i�G���[���T.�������
            .AppendLine("   ,MHKE.aitesaki_cd ")                        '�̔����i�G���[���T.�����R�[�h
            .AppendLine("   ,MHKE.aitesaki_mei ")                       '�̔����i�G���[���T.����於
            .AppendLine("   ,MHKE.syouhin_cd ")                         '�̔����i�G���[���T.���i�R�[�h
            .AppendLine("   ,MHKE.syouhin_mei ")                        '�̔����i�G���[���T.���i��
            .AppendLine("   ,MHKE.tys_houhou_no ")                      '�̔����i�G���[���T.�������@NO
            .AppendLine("   ,MHKE.tys_houhou ")                         '�̔����i�G���[���T.�������@
            .AppendLine("   ,MHKE.torikesi ")                           '�̔����i�G���[���T.���
            .AppendLine("   ,MHKE.koumuten_seikyuu_gaku ")              '�̔����i�G���[���T.�H���X�������z
            .AppendLine("   ,MHKE.koumuten_seikyuu_gaku_henkou_flg ")   '�̔����i�G���[���T.�H���X�������z�ύXFLG
            .AppendLine("   ,MHKE.jitu_seikyuu_gaku ")                  '�̔����i�G���[���T.���������z
            .AppendLine("   ,MHKE.jitu_seikyuu_gaku_henkou_flg ")       '�̔����i�G���[���T.���������z�ύXFLG
            .AppendLine("   ,MHKE.koukai_flg ")                         '�̔����i�G���[���T.���J�t���O
            .AppendLine("   ,MHKE.add_login_user_id ")                  '�̔����i�G���[���T.�o�^���O�C�����[�UID
            .AppendLine("   ,MHKE.add_datetime ")                       '�̔����i�G���[���T.�o�^����
            .AppendLine("   ,MHKE.upd_login_user_id ")                  '�̔����i�G���[���T.�X�V���O�C�����[�UID
            .AppendLine("   ,MHKE.upd_datetime ")                       '�̔����i�G���[���T.�X�V����
            .AppendLine(" FROM ")
            .AppendLine("   m_hanbai_kakaku_error MHKE WITH(READCOMMITTED) ")   '�̔����i�G���[���T
            .AppendLine(" WHERE ")

            'EDI���쐬��
            .AppendLine(" MHKE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))

            '��������
            .AppendLine(" AND CONVERT(varchar(100),MHKE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MHKE.syori_datetime,114),':','') = @syori_datetime")
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoriDate))

            '�\�[�g��
            .AppendLine(" ORDER BY ")
            .AppendLine("      MHKE.edi_jouhou_sakusei_date ")      '�̔����i�G���[���T.EDI���쐬��
            .AppendLine("      ,MHKE.gyou_no ")                     '�̔����i�G���[���T.�sNO
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakakuErr, _
                    dsHanbaiKakakuErr.HanbaiKakakuErrCSVTable.TableName, paramList.ToArray)

        Return dsHanbaiKakakuErr.HanbaiKakakuErrCSVTable

    End Function

    '====================2011/05/16 �ԗ� �d�l�ύX �ǉ� �J�n��===========================
    ''' <summary>�̔����i�}�X�^�ʐݒ�f�[�^���擾����</summary>
    Public Function SelHanbaiKakakuKobeituSettei(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strTyousaHouhouNo As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	MHK.aitesaki_syubetu ")
            .AppendLine("	,MHK.aitesaki_cd ")
            .AppendLine("	,ISNULL(SUB.aitesaki_mei,'') AS aitesaki_mei ")
            .AppendLine("	,MHK.syouhin_cd ")
            .AppendLine("	,MHK.tys_houhou_no ")
            .AppendLine("	,MHK.torikesi ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MHK.koumuten_seikyuu_gaku),'') AS koumuten_seikyuu_gaku ")
            .AppendLine("	,ISNULL(MHK.koumuten_seikyuu_gaku_henkou_flg,0) AS koumuten_seikyuu_gaku_henkou_flg ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),MHK.jitu_seikyuu_gaku),'') AS jitu_seikyuu_gaku ")
            .AppendLine("	,ISNULL(MHK.jitu_seikyuu_gaku_henkou_flg,0) AS jitu_seikyuu_gaku_henkou_flg ")
            .AppendLine("	,ISNULL(MHK.koukai_flg,0) AS koukai_flg ")
            .AppendLine("FROM  ")
            .AppendLine("	m_hanbai_kakaku AS MHK WITH(READCOMMITTED) ")
            .AppendLine("	LEFT OUTER JOIN  ")
            .AppendLine("	( ")
            .AppendLine("		SELECT  ")
            .AppendLine("		    0 AS aitesaki_syubetu  ")
            .AppendLine("		    ,'ALL' AS aitesaki_cd  ")
            .AppendLine("		    ,'�����Ȃ�' AS aitesaki_mei  ")
            .AppendLine("		UNION ALL  ")
            .AppendLine("		SELECT  ")
            .AppendLine("		    1 AS aitesaki_syubetu  ")
            .AppendLine("		    ,kameiten_cd AS aitesaki_cd  ")
            .AppendLine("		    ,kameiten_mei1 AS aitesaki_mei  ")
            .AppendLine("		FROM  ")
            .AppendLine("		    m_kameiten WITH(READCOMMITTED)     ")
            .AppendLine("		UNION ALL  ")
            .AppendLine("		SELECT  ")
            .AppendLine("		    5 AS aitesaki_syubetu  ")
            .AppendLine("		    ,eigyousyo_cd AS aitesaki_cd  ")
            .AppendLine("		    ,eigyousyo_mei AS aitesaki_mei  ")
            .AppendLine("		FROM  ")
            .AppendLine("		    m_eigyousyo WITH(READCOMMITTED)     ")
            .AppendLine("		UNION ALL  ")
            .AppendLine("		SELECT  ")
            .AppendLine("		    7 AS aitesaki_syubetu  ")
            .AppendLine("		    ,keiretu_cd AS aitesaki_cd  ")
            .AppendLine("		    ,MIN(keiretu_mei) AS aitesaki_mei  ")
            .AppendLine("		FROM  ")
            .AppendLine("		    m_keiretu WITH(READCOMMITTED)     ")
            .AppendLine("		GROUP BY  ")
            .AppendLine("		    keiretu_cd  ")
            .AppendLine("	) SUB  ")
            .AppendLine("	ON ")
            .AppendLine("		MHK.aitesaki_syubetu = SUB.aitesaki_syubetu ")
            .AppendLine("		AND ")
            .AppendLine("		MHK.aitesaki_cd = SUB.aitesaki_cd ")
            .AppendLine("WHERE ")
            .AppendLine("	MHK.aitesaki_syubetu = @aitesaki_syubetu ")
            If Not strAiteSakiSyubetu.Trim.Equals("0") Then
                .AppendLine("	AND ")
                .AppendLine("	MHK.aitesaki_cd = @aitesaki_cd ")
            End If
            .AppendLine("	AND ")
            .AppendLine("	MHK.syouhin_cd = @syouhin_cd ")
            .AppendLine("	AND ")
            .AppendLine("	MHK.tys_houhou_no = @tys_houhou_no ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, strAiteSakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAiteSakiCd))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, strTyousaHouhouNo))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtHanbaiKakakuKobeituSettei", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>�̔����i�}�X�^�ʐݒ�̑��݃`�F�c�N</summary>
    Public Function CheckSonzai(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strTyousaHouhouNo As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	aitesaki_syubetu ")
            .AppendLine("FROM  ")
            .AppendLine("	m_hanbai_kakaku WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine("	AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
            .AppendLine("	AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine("	AND ")
            .AppendLine("	tys_houhou_no = @tys_houhou_no ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, strAiteSakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAiteSakiCd))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.Int, 4, strTyousaHouhouNo))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtHanbaiKakakuKobeituSettei", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function


    '====================2011/05/16 �ԗ� �d�l�ύX �ǉ� �I����===========================

End Class
