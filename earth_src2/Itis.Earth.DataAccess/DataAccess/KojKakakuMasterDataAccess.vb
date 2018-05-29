Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports System.Text

Public Class KojKakakuMasterDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>�H�����i�f�[�^�������擾����</summary>
    ''' <param name="dtInfo">�p�����[�^</param>
    ''' <returns>�H�����i�f�[�^����</returns>
    Public Function SelKojKakakuInfoCount(ByVal dtInfo As DataTable) As Integer

        'DataSet�C���X�^���X�̐���
        Dim dsKojKakaku As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT(*) AS count ")
            .AppendLine(ComSQL(dtInfo))

        End With
        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKakaku, _
                    "dtKoiKakakuCount", ComParam(dtInfo).ToArray)

        Return dsKojKakaku.Tables("dtKoiKakakuCount").Rows(0).Item("count")

    End Function
    Private Function ComParam(ByVal dtInfo As DataTable) As List(Of SqlClient.SqlParameter)
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        '�������

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, dtInfo.Rows(0).Item("aitesaki_syubetu")))

        '�����R�[�h
        If dtInfo.Rows(0).Item("aitesaki_cd_from") <> String.Empty And dtInfo.Rows(0).Item("aitesaki_cd_to") <> String.Empty Then
            paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtInfo.Rows(0).Item("aitesaki_cd_from")))
            paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtInfo.Rows(0).Item("aitesaki_cd_to")))
        ElseIf dtInfo.Rows(0).Item("aitesaki_cd_from") <> String.Empty And dtInfo.Rows(0).Item("aitesaki_cd_to") = String.Empty Then
            paramList.Add(MakeParam("@aitesaki_cd_from", SqlDbType.VarChar, 5, dtInfo.Rows(0).Item("aitesaki_cd_from")))
        ElseIf dtInfo.Rows(0).Item("aitesaki_cd_from") = String.Empty And dtInfo.Rows(0).Item("aitesaki_cd_to") <> String.Empty Then
            paramList.Add(MakeParam("@aitesaki_cd_to", SqlDbType.VarChar, 5, dtInfo.Rows(0).Item("aitesaki_cd_to")))
        End If

        '���i�R�[�h
        If dtInfo.Rows(0).Item("syouhin_cd") <> String.Empty Then
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtInfo.Rows(0).Item("syouhin_cd")))
        End If

        '�H�����
        If dtInfo.Rows(0).Item("kojkaisya_cd") <> String.Empty Then
            paramList.Add(MakeParam("@kojkaisya_cd", SqlDbType.VarChar, 8, dtInfo.Rows(0).Item("kojkaisya_cd")))
        End If

        '���
        If dtInfo.Rows(0).Item("torikesi") <> String.Empty Then
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, 0))
        End If

        '��������
        If dtInfo.Rows(0).Item("torikesi_aitesaki") <> String.Empty Then
            paramList.Add(MakeParam("@torikesi_aitesaki", SqlDbType.Int, 4, 0))
        End If
        Return paramList
    End Function
    Private Function ComSQL(ByVal dtInfo As DataTable) As String
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine(" FROM ")
            .AppendLine("   m_koj_kakaku MHK WITH(READCOMMITTED) ")     '�H�����iM
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
            .AppendLine(" AND  (MS.souko_cd = '130' OR MS.souko_cd = '140')")
            .AppendLine(" AND   MS.torikesi = 0 ")
            .AppendLine(" AND   MS.koj_type LIKE '1%' ")
            .AppendLine(" LEFT OUTER JOIN (")
            .AppendLine("   SELECT ")
            .AppendLine("       'ALL' AS tys_kaisya_cd ")
            .AppendLine("       ,'AL' AS jigyousyo_cd ")
            .AppendLine("       ,'�w�薳��' AS tys_kaisya_mei ")
            .AppendLine("   UNION ALL ")
            .AppendLine("   SELECT ")
            .AppendLine("       tys_kaisya_cd AS tys_kaisya_cd ")
            .AppendLine("       ,jigyousyo_cd AS jigyousyo_cd ")
            .AppendLine("       ,tys_kaisya_mei AS tys_kaisya_mei ")
            .AppendLine("   FROM ")
            .AppendLine("       m_tyousakaisya WITH(READCOMMITTED) ")   '������Ѓ}�X�^
            .AppendLine("   ) AS KKK ")
            .AppendLine(" ON    MHK.koj_gaisya_cd = KKK.tys_kaisya_cd ")
            .AppendLine(" AND    MHK.koj_gaisya_jigyousyo_cd = KKK.jigyousyo_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   m_kakutyou_meisyou MKM WITH(READCOMMITTED) ")  '�g������M
            .AppendLine(" ON    MHK.aitesaki_syubetu = MKM.code ")
            .AppendLine(" AND   MKM.meisyou_syubetu = 23 ")
            .AppendLine(" WHERE ")

            '�������
            .AppendLine(" MHK.aitesaki_syubetu = @aitesaki_syubetu ")

            '�����R�[�h
            If dtInfo.Rows(0).Item("aitesaki_cd_from") <> String.Empty And dtInfo.Rows(0).Item("aitesaki_cd_to") <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd BETWEEN @aitesaki_cd_from AND @aitesaki_cd_to ")
            ElseIf dtInfo.Rows(0).Item("aitesaki_cd_from") <> String.Empty And dtInfo.Rows(0).Item("aitesaki_cd_to") = String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_from ")
            ElseIf dtInfo.Rows(0).Item("aitesaki_cd_from") = String.Empty And dtInfo.Rows(0).Item("aitesaki_cd_to") <> String.Empty Then
                .AppendLine(" AND MHK.aitesaki_cd = @aitesaki_cd_to ")
            End If

            '���i�R�[�h
            If dtInfo.Rows(0).Item("syouhin_cd") <> String.Empty Then
                .AppendLine(" AND MHK.syouhin_cd = @syouhin_cd ")
            End If

            '�H�����
            If dtInfo.Rows(0).Item("kojkaisya_cd") <> String.Empty Then
                .AppendLine(" AND ISNULL(MHK.koj_gaisya_cd,'')+ISNULL(MHK.koj_gaisya_jigyousyo_cd,'') = @kojkaisya_cd ")
            End If

            '���
            If dtInfo.Rows(0).Item("torikesi") <> String.Empty Then
                .AppendLine(" AND MHK.torikesi = @torikesi ")
            End If

            '��������
            If dtInfo.Rows(0).Item("torikesi_aitesaki") <> String.Empty Then
                .AppendLine(" AND SUB.torikesi = @torikesi_aitesaki ")
            End If
        End With
        Return commandTextSb.ToString
    End Function
    ''' <summary>�H�����i�̃f�[�^���擾����</summary>
    ''' <param name="dtInfo">�p�����[�^</param>
    ''' <returns>�H�����i�f�[�^�e�[�u��</returns>
    Public Function SelKojKakakuSeiteInfo(ByVal dtInfo As DataTable) As DataTable
        'DataSet�C���X�^���X�̐���
        Dim dsKojKakaku As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            If dtInfo.Rows(0).Item("kensaku_count") = "100" Then
                .AppendLine("      TOP 100 ")
            ElseIf dtInfo.Rows(0).Item("kensaku_count") = "10" Then
                .AppendLine("      TOP 10 ")
            End If
            '�H�����iM.�������:�g������M.����
            .AppendLine("   CAST(MHK.aitesaki_syubetu AS VARCHAR) + '�F' + ISNULL(MKM.meisyou,'') AS aitesaki ")
            .AppendLine("   ,MHK.aitesaki_cd ")                             '�H�����iM.�����R�[�h
            .AppendLine("   ,SUB.aitesaki_mei ")                            '�T�u.����於
            .AppendLine("   ,MHK.syouhin_cd ")                              '�H�����iM.���i�R�[�h
            .AppendLine("   ,MS.syouhin_mei ")                              '���iM.���i��
            .AppendLine("   ,MHK.koj_gaisya_cd +MHK.koj_gaisya_jigyousyo_cd AS koj_cd") '�H�����iM.�H�R�[�h
            .AppendLine("   ,KKK.tys_kaisya_mei ")                          '�H�����M.�H����Ж�
            .AppendLine("   ,CASE MHK.torikesi ")
            .AppendLine("       WHEN 0 THEN '' ")
            .AppendLine("       ELSE '���' ")
            .AppendLine("    END AS torikesi ")                             '�H�����iM.���
            .AppendLine("   ,MHK.uri_gaku ")                                '�H�����iM.������z
            .AppendLine("   ,CASE MHK.koj_gaisya_seikyuu_umu ")
            .AppendLine("       WHEN 1 THEN '�L' ")
            .AppendLine("       ELSE '��' ")
            .AppendLine("    END AS kojumu ")                             '�H�����iM.�H����А����L��
            .AppendLine("   ,CASE ISNULL(MHK.seikyuu_umu,2) ")
            .AppendLine("       WHEN 0 THEN '��' ")
            .AppendLine("       WHEN 1 THEN '�L' ")
            .AppendLine("       ELSE '' ")
            .AppendLine("    END AS seikyuumu ")                             '�H�����iM.�����L��

            .AppendLine(ComSQL(dtInfo))
            '�\�[�g��
            .AppendLine(" ORDER BY ")
            .AppendLine("      MHK.aitesaki_syubetu ")
            .AppendLine("      ,MHK.aitesaki_cd ")
            .AppendLine("      ,MHK.syouhin_cd ")
            .AppendLine("      ,MHK.koj_gaisya_cd ")
            .AppendLine("      ,MHK.koj_gaisya_jigyousyo_cd ")
        End With

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKakaku, _
                    "dsKojKakaku", ComParam(dtInfo).ToArray)

        Return dsKojKakaku.Tables("dsKojKakaku")

    End Function
    ''' <summary>�H�����iCSV�f�[�^���擾����</summary>
    ''' <param name="dtInfo">�p�����[�^</param>
    ''' <returns>�H�����iCSV�f�[�^�e�[�u��</returns>
    Public Function SelKojKakakuCSVInfo(ByVal dtInfo As DataTable) As DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsKojKakaku As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            'EDI���쐬��
            .AppendLine("   RIGHT(CONVERT(varchar(10),GETDATE(),112),4) + REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_date ")
            .AppendLine("   ,ISNULL(MHK.aitesaki_syubetu,'') AS aitesaki_syubetu ")                         '�H�����iM.�������
            .AppendLine("   ,ISNULL(MHK.aitesaki_cd,'') AS  aitesaki_cd")                                   '�H�����iM.�����R�[�h
            .AppendLine("   ,ISNULL(SUB.aitesaki_mei,'') AS  aitesaki_mei")                                 '�T�u.����於
            .AppendLine("   ,ISNULL(MHK.syouhin_cd,'') AS  syouhin_cd")                                     '�H�����iM.���i�R�[�h
            .AppendLine("   ,ISNULL(MS.syouhin_mei,'') AS  syouhin_mei")                                    '���iM.���i��
            .AppendLine("   ,ISNULL(MHK.koj_gaisya_cd,'') AS  koj_gaisya_cd")                               '�H�����iM.�H����ЃR�[�h
            .AppendLine("   ,ISNULL(MHK.koj_gaisya_jigyousyo_cd,'') AS  koj_gaisya_jigyousyo_cd")           '�H�����iM.�H����Ў��Ə��R�[�h
            .AppendLine("   ,ISNULL(KKK.tys_kaisya_mei,'') AS  tys_kaisya_mei")                             '�H�����M.�H����Ж�
            .AppendLine("   ,ISNULL(MHK.torikesi,'') AS  torikesi")                                         '�H�����iM.���
            .AppendLine("   ,ISNULL(MHK.uri_gaku,'') AS  uri_gaku")                                         '�H�����iM.������z
            .AppendLine("   ,ISNULL(MHK.koj_gaisya_seikyuu_umu,'') AS  koj_gaisya_seikyuu_umu")             '�H�����iM.�H����А����L��
            .AppendLine("   ,ISNULL(MHK.seikyuu_umu,'') AS  seikyuu_umu")                                   '�H�����iM.�����L��
            .AppendLine(ComSQL(dtInfo))
            '�\�[�g��
            .AppendLine(" ORDER BY ")
            .AppendLine("      MHK.aitesaki_syubetu ")
            .AppendLine("      ,MHK.aitesaki_cd ")
            .AppendLine("      ,MHK.syouhin_cd ")
            .AppendLine("      ,MHK.koj_gaisya_cd ")
            .AppendLine("      ,MHK.koj_gaisya_jigyousyo_cd ")
        End With

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKakaku, _
                    "dsKojKakaku", ComParam(dtInfo).ToArray)
        Return dsKojKakaku.Tables("dsKojKakaku")

    End Function
    ''' <summary>���i���擾����</summary>
    ''' <returns>���i�f�[�^�e�[�u��</returns>
    Public Function SelSyouhin(Optional ByVal syohuinCd As String = "") As Data.DataTable

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
            .AppendLine(" WHERE  ")  '���
            .AppendLine("  (souko_cd = '130' OR souko_cd = '140')")
            .AppendLine(" AND   torikesi = 0 ")
            .AppendLine(" AND   koj_type LIKE '1%' ")
            If syohuinCd <> "" Then
                .AppendLine(" AND   syouhin_cd ='" & syohuinCd & "'")
            End If
            .AppendLine(" ORDER BY ")
            .AppendLine("   syouhin_cd ")
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsSyouhin, "dtSyouhin")

        Return dsSyouhin.Tables("dtSyouhin")

    End Function
    ''' <summary>�H����Ѓf�[�^���擾����</summary>
    Public Function SelKojKaisyaKensaku(ByVal strCd As String) As DataTable
        ' DataSet�C���X�^���X�̐���()
        Dim dsKojKaisya As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("      ISNULL(tys_kaisya_cd,'')+ISNULL(jigyousyo_cd,'') AS cd,tys_kaisya_mei ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_tyousakaisya  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  ")
        commandTextSb.AppendLine("  ISNULL(tys_kaisya_cd,'')+ISNULL(jigyousyo_cd,'') = @Cd ")

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@Cd", SqlDbType.VarChar, 7, strCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKaisya, _
                    "dsKojKaisya", paramList.ToArray)
        Return dsKojKaisya.Tables(0)

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
            .AppendLine("	file_kbn = 5 ")         '�t�@�C���敪
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
            .AppendLine("	file_kbn =5 ")                         '�t�@�C���敪
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri")

        Return CInt(dsHanbaiKakaku.Tables("dtUploadKanri").Rows(0).Item("count").ToString)

    End Function
    ''' <summary>�H�����i�f�[�^���݃`�F�b�N</summary>
    ''' <param name="strAitesakiSyubetu">�������</param>
    ''' <param name="strAitesakiCd">�����R�[�h</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="strKojCd">�H����ЃR�[�h</param>
    ''' <returns>�H�����i�f�[�^���݋敪</returns>
    Public Function SelKojKakaku(ByVal strAitesakiSyubetu As String, ByVal strAitesakiCd As String, _
                                    ByVal strSyouhinCd As String, ByVal strKojCd As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsKojKakaku As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   aitesaki_syubetu ")
            .AppendLine(" FROM ")
            .AppendLine("	m_koj_kakaku WITH(READCOMMITTED) ")
            .AppendLine(" WHERE ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine(" AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
            .AppendLine(" AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine(" AND ")
            .AppendLine("	ISNULL(koj_gaisya_cd,'')+ISNULL(koj_gaisya_jigyousyo_cd,'') = @koj_cd ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, strAitesakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAitesakiCd))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@koj_cd", SqlDbType.VarChar, 7, strKojCd))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKakaku, "dsKojKakaku", paramList.ToArray)

        '�߂�l
        If dsKojKakaku.Tables("dsKojKakaku").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>�̔����i�}�X�^��o�^�E�X�V����</summary>
    ''' <param name="dtKojKakakuOk">�f�[�^���</param>
    ''' <returns>�������s�敪</returns>
    Public Function InsUpdKojKakaku(ByVal dtKojKakakuOk As Data.DataTable, _
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
            .AppendLine("	m_koj_kakaku WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		aitesaki_syubetu ")                     '�������
            .AppendLine("		,aitesaki_cd ")                         '�����R�[�h
            .AppendLine("		,syouhin_cd ")                          '���i�R�[�h
            .AppendLine("		,koj_gaisya_cd ")                       '�H����ЃR�[�h
            .AppendLine("		,koj_gaisya_jigyousyo_cd ")             '�H����Ў��Ə��R�[�h
            .AppendLine("		,torikesi ")                            '���
            .AppendLine("		,uri_gaku ")                            '������z
            .AppendLine("		,koj_gaisya_seikyuu_umu ")              '�H����А����L��
            .AppendLine("		,seikyuu_umu ")                         '�����L��
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
            .AppendLine("	,@koj_gaisya_cd ")
            .AppendLine("	,@koj_gaisya_jigyousyo_cd ")
            .AppendLine("	,@torikesi ")
            .AppendLine("	,@uri_gaku ")
            .AppendLine("	,@koj_gaisya_seikyuu_umu ")
            .AppendLine("	,@seikyuu_umu ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(" ) ")
        End With

        '�X�V�psql��
        With strSqlUpd
            .AppendLine(" UPDATE ")
            .AppendLine("	m_koj_kakaku WITH(UPDLOCK) ")
            .AppendLine(" SET ")
            .AppendLine("	torikesi = @torikesi ")                             '���
            .AppendLine("	,uri_gaku = @uri_gaku ")  '������z
            .AppendLine("	,koj_gaisya_seikyuu_umu = @koj_gaisya_seikyuu_umu ") '�H����А����L��
            .AppendLine("	,seikyuu_umu = @seikyuu_umu ")                      '�����L��
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")      '�X�V���O�C�����[�UID
            .AppendLine("	,upd_datetime = GETDATE() ")                    '�X�V����
            .AppendLine(" WHERE aitesaki_syubetu = @aitesaki_syubetu ")     '�������
            .AppendLine(" AND   aitesaki_cd = @aitesaki_cd ")               '�����R�[�h
            .AppendLine(" AND   syouhin_cd = @syouhin_cd ")                 '���i�R�[�h
            .AppendLine(" AND   ISNULL(koj_gaisya_cd,'')+ISNULL(koj_gaisya_jigyousyo_cd,'') = @koj_cd ")           '�H����ЃR�[�h�A�H����Ў��Ə��R�[�h
        End With

        For i As Integer = 0 To dtKojKakakuOk.Rows.Count - 1
            '�p�����[�^�̐ݒ�
            paramList.Clear()
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 5, dtKojKakakuOk.Rows(i).Item("aitesaki_syubetu").ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtKojKakakuOk.Rows(i).Item("aitesaki_cd").ToString.Trim))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtKojKakakuOk.Rows(i).Item("syouhin_cd").ToString.Trim))
            paramList.Add(MakeParam("@koj_gaisya_cd", SqlDbType.VarChar, 5, dtKojKakakuOk.Rows(i).Item("koj_gaisya_cd").ToString.Trim))
            paramList.Add(MakeParam("@koj_gaisya_jigyousyo_cd", SqlDbType.VarChar, 2, dtKojKakakuOk.Rows(i).Item("koj_gaisya_jigyousyo_cd").ToString.Trim))

            paramList.Add(MakeParam("@koj_cd", SqlDbType.VarChar, 7, dtKojKakakuOk.Rows(i).Item("koj_gaisya_cd").ToString.Trim & dtKojKakakuOk.Rows(i).Item("koj_gaisya_jigyousyo_cd").ToString.Trim))

            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, dtKojKakakuOk.Rows(i).Item("torikesi").ToString.Trim))
            If dtKojKakakuOk.Rows(i).Item("uri_gaku").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@uri_gaku", SqlDbType.VarChar, 19, DBNull.Value))
            Else
                paramList.Add(MakeParam("@uri_gaku", SqlDbType.VarChar, 19, dtKojKakakuOk.Rows(i).Item("uri_gaku").ToString.Trim))
            End If
            If dtKojKakakuOk.Rows(i).Item("koj_gaisya_seikyuu_umu").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@koj_gaisya_seikyuu_umu", SqlDbType.Int, 4, DBNull.Value))
            Else
                paramList.Add(MakeParam("@koj_gaisya_seikyuu_umu", SqlDbType.Int, 4, dtKojKakakuOk.Rows(i).Item("koj_gaisya_seikyuu_umu").ToString.Trim))
            End If
            If dtKojKakakuOk.Rows(i).Item("seikyuu_umu").ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@seikyuu_umu", SqlDbType.Int, 4, DBNull.Value))
            Else
                paramList.Add(MakeParam("@seikyuu_umu", SqlDbType.Int, 4, dtKojKakakuOk.Rows(i).Item("seikyuu_umu").ToString.Trim))
            End If
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId))
            '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������

            Try
                If dtKojKakakuOk.Rows(i).Item("ins_upd_flg").Equals("0") Then
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
    ''' <summary>�̔����i�G���[���e�[�u����o�^����</summary>
    ''' <param name="dtKojKakakuError">�G���[�f�[�^���</param>
    ''' <returns>�������s�敪</returns>
    Public Function InsKojKakakuError(ByVal dtKojKakakuError As Data.DataTable, _
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
            .AppendLine("   m_koj_kakaku_error WITH(UPDLOCK) ")
            .AppendLine("   ( ")
            .AppendLine("		edi_jouhou_sakusei_date ")              'EDI���쐬��
            .AppendLine("		,gyou_no ")                             '�sNO
            .AppendLine("		,syori_datetime ")                      '��������
            .AppendLine("		,aitesaki_syubetu ")                    '�������
            .AppendLine("		,aitesaki_cd ")                         '�����R�[�h
            .AppendLine("		,aitesaki_mei ")                        '����於
            .AppendLine("		,syouhin_cd ")                          '���i�R�[�h
            .AppendLine("		,syouhin_mei ")                         '���i��
            .AppendLine("		,koj_gaisya_cd ")                       '�H����ЃR�[�h
            .AppendLine("		,koj_gaisya_jigyousyo_cd ")             '�H����Ў��Ə��R�[�h
            .AppendLine("		,koj_gaisya_mei ")                      '�H����Ж�
            .AppendLine("		,torikesi ")                            '���
            .AppendLine("		,uri_gaku ")                            '������z
            .AppendLine("		,koj_gaisya_seikyuu_umu ")              '�H����А����L��
            .AppendLine("		,seikyuu_umu ")                         '�����L��
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
            .AppendLine("	,@koj_gaisya_cd ")
            .AppendLine("	,@koj_gaisya_jigyousyo_cd ")
            .AppendLine("	,@koj_gaisya_mei ")
            .AppendLine("	,@torikesi ")
            .AppendLine("	,@uri_gaku ")
            .AppendLine("	,@koj_gaisya_seikyuu_umu ")
            .AppendLine("	,@seikyuu_umu ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(") ")
        End With

        For i As Integer = 0 To dtKojKakakuError.Rows.Count - 1

            '�p�����[�^�̐ݒ�
            paramList.Clear()
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, dtKojKakakuError.Rows(i).Item(0).ToString.Trim))
            paramList.Add(MakeParam("@gyou_no", SqlDbType.Int, 4, dtKojKakakuError.Rows(i).Item(12).ToString.Trim))
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strUploadDate))))
            paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.VarChar, 5, dtKojKakakuError.Rows(i).Item(1).ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, dtKojKakakuError.Rows(i).Item(2).ToString.Trim))
            paramList.Add(MakeParam("@aitesaki_mei", SqlDbType.VarChar, 40, dtKojKakakuError.Rows(i).Item(3).ToString.Trim))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtKojKakakuError.Rows(i).Item(4).ToString.Trim))
            paramList.Add(MakeParam("@syouhin_mei", SqlDbType.VarChar, 40, dtKojKakakuError.Rows(i).Item(5).ToString.Trim))

            paramList.Add(MakeParam("@koj_gaisya_cd", SqlDbType.VarChar, 5, Left(Right("       " & dtKojKakakuError.Rows(i).Item(6).ToString.Trim, 7), 5).Trim))
            paramList.Add(MakeParam("@koj_gaisya_jigyousyo_cd", SqlDbType.VarChar, 2, Right(dtKojKakakuError.Rows(i).Item(6).ToString.Trim, 2)))
            paramList.Add(MakeParam("@koj_gaisya_mei", SqlDbType.VarChar, 40, dtKojKakakuError.Rows(i).Item(7).ToString.Trim))


            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, dtKojKakakuError.Rows(i).Item(8).ToString.Trim))
            paramList.Add(MakeParam("@uri_gaku", SqlDbType.VarChar, 10, dtKojKakakuError.Rows(i).Item(9).ToString.Trim))
            If dtKojKakakuError.Rows(i).Item(10).ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@koj_gaisya_seikyuu_umu", SqlDbType.VarChar, 1, DBNull.Value))

            Else
                paramList.Add(MakeParam("@koj_gaisya_seikyuu_umu", SqlDbType.VarChar, 1, dtKojKakakuError.Rows(i).Item(10).ToString.Trim))

            End If
            If dtKojKakakuError.Rows(i).Item(11).ToString.Trim = String.Empty Then
                paramList.Add(MakeParam("@seikyuu_umu", SqlDbType.VarChar, 1, DBNull.Value))
            Else
                paramList.Add(MakeParam("@seikyuu_umu", SqlDbType.VarChar, 1, dtKojKakakuError.Rows(i).Item(11).ToString.Trim))

            End If

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
            .AppendLine("	,5 ")
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

    ''' <summary>�H�����i�G���[�����擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�H�����i�G���[�f�[�^�e�[�u��</returns>
    Public Function SelKojKakakuErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) _
                        As DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsKojKakakuErr As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP 100 ")
            .AppendLine("   MHKE.gyou_no ")                 '�H�����i�G���[���T.�sNO
            '�H�����i�G���[���T.������ʁF�g��M.����
            .AppendLine("   ,CASE WHEN ISNULL(MHKE.aitesaki_syubetu,'') = '' THEN '' ")
            .AppendLine("         ELSE MHKE.aitesaki_syubetu + '�F' + ISNULL(MKM.meisyou,'') ")
            .AppendLine("    END AS aitesaki ")
            .AppendLine("   ,MHKE.aitesaki_cd ")            '�H�����i�G���[���T.�����R�[�h
            .AppendLine("   ,MHKE.aitesaki_mei ")           '�H�����i�G���[���T.����於
            .AppendLine("   ,MHKE.syouhin_cd ")             '�H�����i�G���[���T.���i�R�[�h
            .AppendLine("   ,MHKE.syouhin_mei ")            '�H�����i�G���[���T.���i��
            .AppendLine("   ,ISNULL(MHKE.koj_gaisya_cd,'')+ISNULL(MHKE.koj_gaisya_jigyousyo_cd,'') AS koj_cd ")  '�H�����i�G���[���T.�H����ЃR�[�h
            .AppendLine("   ,MHKE.koj_gaisya_mei ") '�H�����i�G���[���T.�H����Ж�
            .AppendLine("   ,CASE MHKE.torikesi ")
            .AppendLine("       WHEN '0' THEN '' ")
            .AppendLine("       ELSE '���' ")
            .AppendLine("    END AS torikesi ")                 '�H�����i�G���[���T.���
            .AppendLine("   ,MHKE.uri_gaku ")      '�H�����i�G���[���T.������z
            .AppendLine("   ,CASE ISNULL(MHKE.koj_gaisya_seikyuu_umu,'0') ")
            .AppendLine("       WHEN '0' THEN '�H����А�����' ")
            .AppendLine("       ELSE '�H����А����L' ")
            .AppendLine("    END AS koj_gaisya_seikyuu_umu ")     '�H�����i�G���[���T.�H����А����L��
            .AppendLine("   ,CASE ISNULL(MHKE.seikyuu_umu,'0') ")
            .AppendLine("       WHEN '0' THEN '����' ")
            .AppendLine("       ELSE '�L��' ")
            .AppendLine("    END AS seikyuu_umu ")     '�H�����i�G���[���T.�����L��
            .AppendLine(" FROM ")
            .AppendLine("   m_koj_kakaku_error MHKE WITH(READCOMMITTED) ")   '�H�����i�G���[���T
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
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKakakuErr, _
                    "dsKojKakakuErr", paramList.ToArray)

        Return dsKojKakakuErr.Tables("dsKojKakakuErr")

    End Function
    ''' <summary>�H�����i�G���[�������擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�H�����i�G���[����</returns>
    Public Function SelKojKakakuErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As Integer

        'DataSet�C���X�^���X�̐���
        Dim dsKojKakakuErr As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   Count(edi_jouhou_sakusei_date) AS count ")
            .AppendLine(" FROM ")
            .AppendLine("   m_koj_kakaku_error WITH(READCOMMITTED) ")   '�H�����i�G���[���T
            .AppendLine(" WHERE ")

            'EDI���
            .AppendLine(" edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))

            '��������
            .AppendLine(" AND CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime")
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoriDate))

        End With
        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKakakuErr, _
                    "dsKojKakakuErr", paramList.ToArray)

        Return dsKojKakakuErr.Tables("dsKojKakakuErr").Rows(0).Item("count")
 


    End Function
    ''' <summary>�H�����i�G���[CSV�����擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�H�����i�G���[CSV�f�[�^�e�[�u��</returns>
    Public Function SelKojKakakuErrCsv(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) _
                    As DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsKojKakakuErr As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP 5000 ")
            .AppendLine("   MHKE.edi_jouhou_sakusei_date ")             '�H�����i�G���[���T.EDI���쐬��
            .AppendLine("   ,MHKE.gyou_no ")                            '�H�����i�G���[���T.�sNO
            .AppendLine("   ,MHKE.syori_datetime ")                     '�H�����i�G���[���T.��������
            .AppendLine("   ,MHKE.aitesaki_syubetu ")                   '�H�����i�G���[���T.�������
            .AppendLine("   ,MHKE.aitesaki_cd ")                        '�H�����i�G���[���T.�����R�[�h
            .AppendLine("   ,MHKE.aitesaki_mei ")                       '�H�����i�G���[���T.����於
            .AppendLine("   ,MHKE.syouhin_cd ")                         '�H�����i�G���[���T.���i�R�[�h
            .AppendLine("   ,MHKE.syouhin_mei ")                        '�H�����i�G���[���T.���i��
            .AppendLine("   ,ISNULL(MHKE.koj_gaisya_cd,'')+ ISNULL(MHKE.koj_gaisya_jigyousyo_cd,'')  AS koj_gaisya_cd ")                      '�H�����i�G���[���T.�H����ЃR�[�h
            .AppendLine("   ,MHKE.koj_gaisya_mei ")                         '�H�����i�G���[���T.�H����Ж�
            .AppendLine("   ,MHKE.torikesi ")                           '�H�����i�G���[���T.���
            .AppendLine("   ,MHKE.uri_gaku ")              '�H�����i�G���[���T.������z
            .AppendLine("   ,MHKE.koj_gaisya_seikyuu_umu ")   '�H�����i�G���[���T.�H����А����L��
            .AppendLine("   ,MHKE.seikyuu_umu ")                  '�H�����i�G���[���T.�����L��
            .AppendLine("   ,MHKE.add_login_user_id ")                  '�H�����i�G���[���T.�o�^���O�C�����[�UID
            .AppendLine("   ,MHKE.add_datetime ")                       '�H�����i�G���[���T.�o�^����
            .AppendLine("   ,MHKE.upd_login_user_id ")                  '�H�����i�G���[���T.�X�V���O�C�����[�UID
            .AppendLine("   ,MHKE.upd_datetime ")                       '�H�����i�G���[���T.�X�V����
            .AppendLine(" FROM ")
            .AppendLine("   m_koj_kakaku_error MHKE WITH(READCOMMITTED) ")   '�H�����i�G���[���T
            .AppendLine(" WHERE ")

            'EDI���쐬��
            .AppendLine(" MHKE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))

            '��������
            .AppendLine(" AND CONVERT(varchar(100),MHKE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),MHKE.syori_datetime,114),':','') = @syori_datetime")
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoriDate))

            '�\�[�g��
            .AppendLine(" ORDER BY ")
            .AppendLine("      MHKE.edi_jouhou_sakusei_date ")      '�H�����i�G���[���T.EDI���쐬��
            .AppendLine("      ,MHKE.gyou_no ")                     '�H�����i�G���[���T.�sNO
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKojKakakuErr, _
                    "dsKojKakakuErr", paramList.ToArray)

        Return dsKojKakakuErr.Tables("dsKojKakakuErr")

    End Function
    ''' <summary>�H�����i�}�X�^�ʐݒ�f�[�^���擾����</summary>
    Public Function SelKojKakakuKobeituSettei(ByVal dtInfo As DataTable) As Data.DataTable

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
            .AppendLine("   ,ISNULL(MHK.koj_gaisya_cd,'') + '�F' +ISNULL(MHK.koj_gaisya_jigyousyo_cd,'') AS koj_cd") '�H�����iM.�H�R�[�h
            .AppendLine("   ,KKK.tys_kaisya_mei ")                          '�H�����M.�H����Ж�
            .AppendLine("	,MHK.torikesi ")
            .AppendLine("	,ISNULL(ISNULL(MHK.uri_gaku,MS.hyoujun_kkk),'') AS uri_gaku ")
            .AppendLine("	,MHK.koj_gaisya_seikyuu_umu AS kojumu ")
            .AppendLine("	,MHK.seikyuu_umu AS seikyuumu ")
            .AppendLine(ComSQL(dtInfo))
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dsReturn", ComParam(dtInfo).ToArray)

        Return dsReturn.Tables(0)

    End Function
    ''' <summary>�H�����i�}�X�^�ʐݒ�̑��݃`�F�c�N</summary>
    Public Function CheckSonzai(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strKojKaisyaCd As String) As Data.DataTable

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
            .AppendLine("	m_koj_kakaku WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	aitesaki_syubetu = @aitesaki_syubetu ")
            .AppendLine("	AND ")
            .AppendLine("	aitesaki_cd = @aitesaki_cd ")
            .AppendLine("	AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine("	AND ")
            .AppendLine("	ISNULL(koj_gaisya_cd,'')+ISNULL(koj_gaisya_jigyousyo_cd,'') = @koj_cd ")
        End With

        paramList.Add(MakeParam("@aitesaki_syubetu", SqlDbType.Int, 4, strAiteSakiSyubetu))
        paramList.Add(MakeParam("@aitesaki_cd", SqlDbType.VarChar, 5, strAiteSakiCd))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@koj_cd", SqlDbType.VarChar, 7, strKojKaisyaCd))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dsReturn", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function
   

End Class
