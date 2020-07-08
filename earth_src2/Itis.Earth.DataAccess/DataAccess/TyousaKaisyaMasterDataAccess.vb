Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
''' <summary>
''' ������Ѓ}�X�^
''' </summary>
''' <history>
''' <para>2010/05/15�@�n���R(��A)�@�V�K�쐬</para>
''' </history>
Public Class TyousaKaisyaMasterDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    'Private connDbTable_m_fc As String = System.Configuration.ConfigurationManager.AppSettings("connDbTable_m_fc").ToString

    ''' <summary>
    ''' �g�����̃}�X�^
    ''' </summary>
    ''' <param name="strSyubetu">���̎��</param>
    Public Function SelKakutyouInfo(ByVal strSyubetu As String) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT   ")
            .AppendLine("   code   ")
            .AppendLine("   ,meisyou   ")
            .AppendLine(" FROM  ")
            .AppendLine("   m_kakutyou_meisyou WITH (READCOMMITTED)  ")
            .AppendLine(" WHERE  meisyou_syubetu=@meisyou_syubetu  ")
            .AppendLine(" ORDER BY hyouji_jyun ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 3, strSyubetu))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' ������Ѓ}�X�^
    ''' </summary>
    ''' <param name="strTysKaisyaCd">������ЃR�[�h</param>
    ''' <param name="strJigyousyoCd">���Ə��R�[�h</param>
    Public Function SelMTyousaKaisyaInfo(ByVal strTyousaKaisya_Cd As String, _
                                         ByVal strTysKaisyaCd As String, _
                                         ByVal strJigyousyoCd As String, _
                                         ByVal btn As String) As TyousaKaisyaDataSet.m_tyousakaisyaDataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New TyousaKaisyaDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   MTK.tys_kaisya_cd, ")       '������ЃR�[�h
            .AppendLine("   MTK.jigyousyo_cd, ")        '���Ə��R�[�h
            .AppendLine("   MTK.torikesi, ")        '���
            .AppendLine("   MTK.tys_kaisya_mei, ")      '������Ж�
            .AppendLine("   MTK.tys_kaisya_mei_kana, ")     '������Ж��J�i
            .AppendLine("   MTK.seikyuu_saki_shri_saki_mei, ")      '������x���於
            .AppendLine("   MTK.seikyuu_saki_shri_saki_kana, ")     '������x���於�J�i
            .AppendLine("   MTK.jyuusyo1, ")        '�Z��1
            .AppendLine("   MTK.jyuusyo2, ")        '�Z��2
            .AppendLine("   MTK.yuubin_no, ")       '�X�֔ԍ�
            .AppendLine("   MTK.tel_no, ")      '�d�b�ԍ�
            .AppendLine("   MTK.fax_no, ")      'FAX�ԍ�
            .AppendLine("   MTK.pca_siiresaki_cd, ")        'PCA�p�d����R�[�h
            .AppendLine("   MTK.pca_seikyuu_cd, ")      'PCA������R�[�h
            .AppendLine("   MTK.seikyuu_saki_cd, ")     '������R�[�h
            .AppendLine("   MTK.seikyuu_saki_brc, ")        '������}��
            .AppendLine("   MTK.seikyuu_saki_kbn, ")        '������敪
            .AppendLine("   MTK.seikyuu_sime_date, ")       '�������ߓ�
            .AppendLine("   MTK.skysy_soufu_jyuusyo1, ")        '���������t��Z��1
            .AppendLine("   MTK.skysy_soufu_jyuusyo2, ")        '���������t��Z��2
            .AppendLine("   MTK.skysy_soufu_yuubin_no, ")       '���������t��X�֔ԍ�
            .AppendLine("   MTK.skysy_soufu_tel_no, ")      '���������t��d�b�ԍ�
            .AppendLine("   MTK.skk_shri_saki_cd, ")        '�V��v�x����R�[�h
            .AppendLine("   MTK.skk_jigyousyo_cd, ")        '�V��v���Ə��R�[�h
            .AppendLine("   MTK.shri_meisai_jigyousyo_cd, ")        '�x�����׏W�v�掖�Ə��R�[�h
            .AppendLine("   MTK.shri_jigyousyo_cd, ")       '�x���W�v�掖�Ə��R�[�h
            .AppendLine("   MTK.shri_sime_date, ")      '�x�����ߓ�
            .AppendLine("   MTK.shri_yotei_gessuu, ")       '�x���\�茎��
            .AppendLine("   MTK.fctring_kaisi_nengetu, ")       '�t�@�N�^�����O�J�n�N��
            .AppendLine("   MTK.shri_you_fax_no, ")     '�x���pFAX�ԍ�
            .AppendLine("   MTK.ss_kijyun_kkk, ")       'SS����i
            .AppendLine("   MTK.fc_ten_cd, ")       'FC�X�R�[�h
            .AppendLine("   MTK.kensa_center_cd, ")     '�����Z���^�[�R�[�h
            .AppendLine("   MTK.koj_hkks_tyokusou_flg, ")       '�H���񍐏�����
            .AppendLine("   MTK.koj_hkks_tyokusou_upd_login_user_id, ")     '�H���񍐏������ύX���O�C�����[�U�[ID
            .AppendLine("   MTK.koj_hkks_tyokusou_upd_datetime, ")      '�H���񍐏������ύX����
            .AppendLine("   MTK.tys_kaisya_flg, ")      '������Ѓt���O
            .AppendLine("   MTK.koj_kaisya_flg, ")      '�H����Ѓt���O
            .AppendLine("   MTK.japan_kai_kbn, ")       'JAPAN��敪
            .AppendLine("   MTK.japan_kai_nyuukai_date, ")      'JAPAN�����N��
            .AppendLine("   MTK.japan_kai_taikai_date, ")       'JAPAN��މ�N��
            .AppendLine("   MTK.zenjyuhin_hosoku, ")            '�S�Z�i�敪�⑫


            .AppendLine("   MTK.fc_ten_kbn, ")      'FC�X�敪
            .AppendLine("   MTK.fc_nyuukai_date, ")     'FC����N��
            .AppendLine("   MTK.fc_taikai_date, ")      'FC�މ�N��
            .AppendLine("   MTK.torikesi_riyuu, ")      '������R
            .AppendLine("   MTK.report_jhs_token_flg, ")        'ReportJHS�g�[�N���L���t���O
            .AppendLine("   MTK.tkt_jbn_tys_syunin_skk_flg, ")      '��n�n�Ւ�����C���i�L���t���O
            .AppendLine("   MTK.add_login_user_id, ")       '�o�^���O�C�����[�U�[ID
            .AppendLine("   MTK.add_datetime, ")        '�o�^����
            .AppendLine("   MTK.upd_login_user_id, ")       '�X�V���O�C�����[�U�[ID
            .AppendLine("   MTK.upd_datetime, ")        '�X�V����
            .AppendLine("   MK.eigyousyo_mei AS keiretu_mei, ")        '�n��
            .AppendLine("   MSSS.shri_saki_mei_kanji, ")        '�x���於_����
            '.AppendLine("   MFC.fc_nm, ")        '�x���於_����
            .AppendLine("   VSSI.seikyuu_saki_mei, ")        '�����於

            .AppendLine("   MTK1.tys_kaisya_mei AS shri_kaisya_mei, ")        '�x���W�v�掖�Ə���
            .AppendLine("   MTK2.tys_kaisya_mei AS shri_meisai_kaisya_mei ")         '�x�����׏W�v�掖�Ə���

            '============2012/04/12 �ԗ� 405721 �ǉ���==========================
            .AppendLine("   ,MTK.daihyousya_mei ")        '��\�Җ�
            .AppendLine("   ,MTK.yakusyoku_mei ")        '��E��
            '============2012/04/12 �ԗ� 405721 �ǉ���==========================

            '2013/11/04 ���F�ǉ� ��
            .AppendLine("   ,MTK.sds_hoji_info ")        'SDS�ێ����
            .AppendLine("   ,MTK.sds_daisuu_info ")      'SDS�䐔���
            .AppendLine("   ,MTK.jituzai_flg ")          '����FLG
            '2013/11/04 ���F�ǉ���
            .AppendLine("   ,MTK.a1_lifnr ")          '����FLG
            .AppendLine("   ,MSSK.a1_a_zz_sort ")          '����FLG

            .AppendLine("FROM ")
            .AppendLine("   m_tyousakaisya MTK WITH (READCOMMITTED) ")      '������Ѓ}�X�^
            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_eigyousyo MK WITH (READCOMMITTED) ")            '�c�Ə��}�X�^
            .AppendLine("ON ")
            .AppendLine("  MTK.fc_ten_cd = MK.eigyousyo_cd ")

            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_sinkaikei_siharai_saki MSSS WITH (READCOMMITTED) ")       '�V��v�x����}�X�^
            .AppendLine("ON ")
            .AppendLine("  MTK.skk_jigyousyo_cd = MSSS.skk_jigyou_cd ")
            .AppendLine("AND ")
            .AppendLine("  MTK.skk_shri_saki_cd = MSSS.skk_shri_saki_cd ")
            '.AppendLine("LEFT JOIN ")
            '.AppendLine("   " & connDbTable_m_fc & " MFC WITH (READCOMMITTED) ")       '��������.FC�}�X�^
            '.AppendLine("ON ")
            '.AppendLine("  MTK.kensa_center_cd = MFC.fc_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("   v_seikyuu_saki_info VSSI WITH (READCOMMITTED) ")       '��������VIEW
            .AppendLine("ON ")
            .AppendLine("  MTK.seikyuu_saki_cd = VSSI.seikyuu_saki_cd ")
            .AppendLine("AND ")
            .AppendLine("  MTK.seikyuu_saki_brc = VSSI.seikyuu_saki_brc ")
            .AppendLine("AND ")
            .AppendLine("  MTK.seikyuu_saki_kbn = VSSI.seikyuu_saki_kbn ")

            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_tyousakaisya MTK1 WITH (READCOMMITTED) ")      '������Ѓ}�X�^")
            .AppendLine("ON ")
            .AppendLine("   MTK.tys_kaisya_cd = MTK1.tys_kaisya_cd ")
            .AppendLine("AND ")
            .AppendLine("   MTK.shri_jigyousyo_cd = MTK1.jigyousyo_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_tyousakaisya MTK2 WITH (READCOMMITTED) ")      '������Ѓ}�X�^")
            .AppendLine("ON ")
            .AppendLine("   MTK.tys_kaisya_cd = MTK2.tys_kaisya_cd ")
            .AppendLine("AND ")
            .AppendLine("   MTK.shri_meisai_jigyousyo_cd = MTK2.jigyousyo_cd ")

            .AppendLine("LEFT JOIN m_sinkaikei_siire_saki MSSK")
            .AppendLine("  ON MTK.a1_lifnr = MSSK.a1_lifnr ")

            .AppendLine("WHERE ")
            .AppendLine("   MTK.tys_kaisya_cd IS NOT NULL ")
            If btn = "btnSearch" Then
                .AppendLine("AND ")
                .AppendLine("   MTK.tys_kaisya_cd + MTK.jigyousyo_cd = @tys_kaisya_cd ")
            Else
                .AppendLine("AND ")
                .AppendLine("   MTK.tys_kaisya_cd = @tys_kaisya_cd ")
                .AppendLine("AND ")
                .AppendLine("   MTK.jigyousyo_cd = @jigyousyo_cd ")
            End If

        End With

        '�p�����[�^�̐ݒ�
        If btn = "btnSearch" Then
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 7, strTyousaKaisya_Cd))
        Else
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTysKaisyaCd))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))
        End If

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    dsDataSet.m_tyousakaisya.TableName, paramList.ToArray)

        '�߂�
        Return dsDataSet.m_tyousakaisya
    End Function

    ''' <summary>
    ''' �r���`�F�b�N�p
    ''' </summary>
    ''' <param name="strTysKaisyaCd">������ЃR�[�h</param>
    ''' <param name="strJigyousyoCd">���Ə��R�[�h</param>
    ''' <param name="strKousinDate">�X�V����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelHaita(ByVal strTysKaisyaCd As String, ByVal strJigyousyoCd As String, ByVal strKousinDate As String) As DataTable

        ' DataSet�C���X�^���X�̐���()
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("  upd_login_user_id ")
            .AppendLine("  ,upd_datetime ")
            .AppendLine("FROM ")
            .AppendLine("  m_tyousakaisya WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("  tys_kaisya_cd = @tys_kaisya_cd  ")
            .AppendLine("AND ")
            .AppendLine("  jigyousyo_cd = @jigyousyo_cd  ")
            .AppendLine("AND ")
            .AppendLine("   CONVERT(varchar(19),CONVERT(datetime,upd_datetime,21),21)<>CONVERT(varchar(19),CONVERT(datetime,@upd_datetime,21),21) ")
            .AppendLine("AND ")
            .AppendLine("   upd_datetime IS NOT NULL  ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTysKaisyaCd))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousinDate))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    ''' <summary>
    ''' ������Ѓ}�X�^�e�[�u���̏C��
    ''' </summary>
    ''' <param name="dtTyousaKaisya">�C���̃f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdTyousaKaisya(ByVal dtTyousaKaisya As TyousaKaisyaDataSet.m_tyousakaisyaDataTable, ByVal strHenkou As String, ByVal strDisplayName As String) As Integer

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("   m_tyousakaisya WITH (UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("   torikesi = @torikesi, ")    '���
            .AppendLine("   tys_kaisya_mei = @tys_kaisya_mei, ")    '������Ж�
            .AppendLine("   tys_kaisya_mei_kana = @tys_kaisya_mei_kana, ")  '������Ж��J�i
            .AppendLine("   seikyuu_saki_shri_saki_mei = @seikyuu_saki_shri_saki_mei, ")    '������x���於
            .AppendLine("   seikyuu_saki_shri_saki_kana = @seikyuu_saki_shri_saki_kana, ")  '������x���於�J�i
            .AppendLine("   jyuusyo1 = @jyuusyo1, ")    '�Z��1
            .AppendLine("   jyuusyo2 = @jyuusyo2, ")    '�Z��2
            .AppendLine("   yuubin_no = @yuubin_no, ")  '�X�֔ԍ�
            .AppendLine("   tel_no = @tel_no, ")    '�d�b�ԍ�
            .AppendLine("   fax_no = @fax_no, ")    'FAX�ԍ�
            '.AppendLine("   pca_siiresaki_cd = @pca_siiresaki_cd, ")    'PCA�p�d����R�[�h
            '.AppendLine("   pca_seikyuu_cd = @pca_seikyuu_cd, ")    'PCA������R�[�h
            .AppendLine("   seikyuu_saki_cd = @seikyuu_saki_cd, ")  '������R�[�h
            .AppendLine("   seikyuu_saki_brc = @seikyuu_saki_brc, ")    '������}��
            .AppendLine("   seikyuu_saki_kbn = @seikyuu_saki_kbn, ")    '������敪
            '.AppendLine("   seikyuu_sime_date = @seikyuu_sime_date, ")  '�������ߓ�
            .AppendLine("   skysy_soufu_jyuusyo1 = @skysy_soufu_jyuusyo1, ")    '���������t��Z��1
            .AppendLine("   skysy_soufu_jyuusyo2 = @skysy_soufu_jyuusyo2, ")    '���������t��Z��2
            .AppendLine("   skysy_soufu_yuubin_no = @skysy_soufu_yuubin_no, ")  '���������t��X�֔ԍ�
            .AppendLine("   skysy_soufu_tel_no = @skysy_soufu_tel_no, ")    '���������t��d�b�ԍ�
            .AppendLine("   skk_shri_saki_cd = @skk_shri_saki_cd, ")    '�V��v�x����R�[�h
            .AppendLine("   skk_jigyousyo_cd = @skk_jigyousyo_cd, ")    '�V��v���Ə��R�[�h
            .AppendLine("   shri_meisai_jigyousyo_cd = @shri_meisai_jigyousyo_cd, ")    '�x�����׏W�v�掖�Ə��R�[�h
            .AppendLine("   shri_jigyousyo_cd = @shri_jigyousyo_cd, ")  '�x���W�v�掖�Ə��R�[�h
            .AppendLine("   shri_sime_date = @shri_sime_date, ")    '�x�����ߓ�
            .AppendLine("   shri_yotei_gessuu = @shri_yotei_gessuu, ")  '�x���\�茎��
            .AppendLine("   fctring_kaisi_nengetu = @fctring_kaisi_nengetu, ")  '�t�@�N�^�����O�J�n�N��
            .AppendLine("   shri_you_fax_no = @shri_you_fax_no, ")  '�x���pFAX�ԍ�
            .AppendLine("   ss_kijyun_kkk = @ss_kijyun_kkk, ")  'SS����i
            .AppendLine("   fc_ten_cd = @fc_ten_cd, ")  'FC�X�R�[�h
            .AppendLine("   kensa_center_cd = @kensa_center_cd, ")  '�����Z���^�[�R�[�h
            .AppendLine("   koj_hkks_tyokusou_flg = @koj_hkks_tyokusou_flg, ")  '�H���񍐏�����
            .AppendLine("   koj_hkks_tyokusou_upd_login_user_id = @koj_hkks_tyokusou_upd_login_user_id, ")  '�H���񍐏������ύX���O�C�����[�U�[ID

            If strHenkou = "YES" Then
                .AppendLine("   koj_hkks_tyokusou_upd_datetime = GETDATE(), ")    '�H���񍐏������ύX����
            Else
                .AppendLine("   koj_hkks_tyokusou_upd_datetime = @koj_hkks_tyokusou_upd_datetime, ")    '�H���񍐏������ύX����
            End If

            .AppendLine("   tys_kaisya_flg = @tys_kaisya_flg, ")    '������Ѓt���O
            .AppendLine("   koj_kaisya_flg = @koj_kaisya_flg, ")    '�H����Ѓt���O
            .AppendLine("   japan_kai_kbn = @japan_kai_kbn, ")  'JAPAN��敪
            .AppendLine("   japan_kai_nyuukai_date = @japan_kai_nyuukai_date, ")    'JAPAN�����N��
            .AppendLine("   japan_kai_taikai_date = @japan_kai_taikai_date, ")  'JAPAN��މ�N��
            .AppendLine("   zenjyuhin_hosoku = @zenjyuhin_hosoku, ")

            .AppendLine("   fc_ten_kbn = @fc_ten_kbn, ")    'FC�X�敪
            .AppendLine("   fc_nyuukai_date = @fc_nyuukai_date, ")  'FC����N��
            .AppendLine("   fc_taikai_date = @fc_taikai_date, ")    'FC�މ�N��
            .AppendLine("   torikesi_riyuu = @torikesi_riyuu, ")    '������R
            .AppendLine("   report_jhs_token_flg = @report_jhs_token_flg, ")    'ReportJHS�g�[�N���L���t���O
            .AppendLine("   tkt_jbn_tys_syunin_skk_flg = @tkt_jbn_tys_syunin_skk_flg, ")    '��n�n�Ւ�����C���i�L���t���O
            '============2012/04/12 �ԗ� 405721 �ǉ���==========================
            .AppendLine("   daihyousya_mei = @daihyousya_mei, ")        '��\�Җ�
            .AppendLine("   yakusyoku_mei = @yakusyoku_mei, ")        '��E��
            '============2012/04/12 �ԗ� 405721 �ǉ���==========================
            '.AppendLine("   add_login_user_id = @add_login_user_id, ")  '�o�^���O�C�����[�U�[ID
            '.AppendLine("   add_datetime = @add_datetime, ")    '�o�^����
            '2013/11/04 ���F�ǉ� ��
            .AppendLine("   sds_hoji_info = @sds_hoji_info, ")        'SDS�ێ����
            .AppendLine("   sds_daisuu_info = @sds_daisuu_info, ")    'SDS�䐔���
            '2013/11/04 ���F�ǉ� ��
            .AppendLine("   jituzai_flg = @jituzai_flg, ")  '����FLG

            .AppendLine("   a1_lifnr = @a1_lifnr, ")  '����FLG

            .AppendLine("   upd_login_user_id = @upd_login_user_id, ")  '�X�V���O�C�����[�U�[ID
            .AppendLine("   upd_datetime = GETDATE() ")    '�X�V����
            .AppendLine("WHERE ")
            .AppendLine("  tys_kaisya_cd = @tys_kaisya_cd ")
            .AppendLine("AND ")
            .AppendLine("  jigyousyo_cd = @jigyousyo_cd ")
        End With

        '�p�����[�^�̐ݒ�
        With paramList
            '������ЃR�[�h
            .Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 255, dtTyousaKaisya(0).tys_kaisya_cd))
            '���Ə��R�[�h
            .Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 255, dtTyousaKaisya(0).jigyousyo_cd))
            '���
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtTyousaKaisya(0).torikesi))
            '������Ж�
            .Add(MakeParam("@tys_kaisya_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tys_kaisya_mei = "", DBNull.Value, dtTyousaKaisya(0).tys_kaisya_mei)))
            '������Ж��J�i
            .Add(MakeParam("@tys_kaisya_mei_kana", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tys_kaisya_mei_kana = "", DBNull.Value, dtTyousaKaisya(0).tys_kaisya_mei_kana)))
            '������x���於
            .Add(MakeParam("@seikyuu_saki_shri_saki_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_shri_saki_mei = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_shri_saki_mei)))
            '������x���於�J�i
            .Add(MakeParam("@seikyuu_saki_shri_saki_kana", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_shri_saki_kana = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_shri_saki_kana)))
            '�Z��1
            .Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).jyuusyo1 = "", DBNull.Value, dtTyousaKaisya(0).jyuusyo1)))
            '�Z��2
            .Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).jyuusyo2 = "", DBNull.Value, dtTyousaKaisya(0).jyuusyo2)))
            '�X�֔ԍ�
            .Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).yuubin_no = "", DBNull.Value, dtTyousaKaisya(0).yuubin_no)))
            '�d�b�ԍ�
            .Add(MakeParam("@tel_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tel_no = "", DBNull.Value, dtTyousaKaisya(0).tel_no)))
            'FAX�ԍ�
            .Add(MakeParam("@fax_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fax_no = "", DBNull.Value, dtTyousaKaisya(0).fax_no)))

            '������R�[�h
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_cd = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_cd)))
            '������}��
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_brc = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_brc)))
            '������敪
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_kbn = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_kbn)))

            '���������t��Z��1
            .Add(MakeParam("@skysy_soufu_jyuusyo1", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_jyuusyo1 = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_jyuusyo1)))
            '���������t��Z��2
            .Add(MakeParam("@skysy_soufu_jyuusyo2", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_jyuusyo2 = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_jyuusyo2)))
            '���������t��X�֔ԍ�
            .Add(MakeParam("@skysy_soufu_yuubin_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_yuubin_no = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_yuubin_no)))
            '���������t��d�b�ԍ�
            .Add(MakeParam("@skysy_soufu_tel_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_tel_no = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_tel_no)))
            '�V��v�x����R�[�h
            .Add(MakeParam("@skk_shri_saki_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skk_shri_saki_cd = "", DBNull.Value, dtTyousaKaisya(0).skk_shri_saki_cd)))
            '�V��v���Ə��R�[�h
            .Add(MakeParam("@skk_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skk_jigyousyo_cd = "", DBNull.Value, dtTyousaKaisya(0).skk_jigyousyo_cd)))
            '�x�����׏W�v�掖�Ə��R�[�h
            .Add(MakeParam("@shri_meisai_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_meisai_jigyousyo_cd = "", DBNull.Value, dtTyousaKaisya(0).shri_meisai_jigyousyo_cd)))
            '�x���W�v�掖�Ə��R�[�h
            .Add(MakeParam("@shri_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_jigyousyo_cd = "", DBNull.Value, dtTyousaKaisya(0).shri_jigyousyo_cd)))
            '�x�����ߓ�
            .Add(MakeParam("@shri_sime_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_sime_date = "", DBNull.Value, dtTyousaKaisya(0).shri_sime_date)))
            '�x���\�茎��
            .Add(MakeParam("@shri_yotei_gessuu", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_yotei_gessuu = "", DBNull.Value, dtTyousaKaisya(0).shri_yotei_gessuu)))
            '�t�@�N�^�����O�J�n�N��
            .Add(MakeParam("@fctring_kaisi_nengetu", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fctring_kaisi_nengetu = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).fctring_kaisi_nengetu))))
            '�x���pFAX�ԍ�
            .Add(MakeParam("@shri_you_fax_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_you_fax_no = "", DBNull.Value, dtTyousaKaisya(0).shri_you_fax_no)))
            'SS����i
            .Add(MakeParam("@ss_kijyun_kkk", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).ss_kijyun_kkk = "", DBNull.Value, dtTyousaKaisya(0).ss_kijyun_kkk)))
            'FC�X�R�[�h
            .Add(MakeParam("@fc_ten_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_ten_cd = "", DBNull.Value, dtTyousaKaisya(0).fc_ten_cd)))
            '�����Z���^�[�R�[�h
            .Add(MakeParam("@kensa_center_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).kensa_center_cd = "", DBNull.Value, dtTyousaKaisya(0).kensa_center_cd)))

            If strHenkou = "YES" Then
                '�H���񍐏�����
                .Add(MakeParam("@koj_hkks_tyokusou_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_hkks_tyokusou_flg = "", DBNull.Value, dtTyousaKaisya(0).koj_hkks_tyokusou_flg)))
                '�H���񍐏������ύX���O�C�����[�U�[ID
                .Add(MakeParam("@koj_hkks_tyokusou_upd_login_user_id", SqlDbType.VarChar, 255, IIf(strDisplayName = "", DBNull.Value, strDisplayName)))
            Else
                '�H���񍐏�����
                .Add(MakeParam("@koj_hkks_tyokusou_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_hkks_tyokusou_flg = "", DBNull.Value, dtTyousaKaisya(0).koj_hkks_tyokusou_flg)))
                '�H���񍐏������ύX���O�C�����[�U�[ID
                .Add(MakeParam("@koj_hkks_tyokusou_upd_login_user_id", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_hkks_tyokusou_upd_login_user_id = "", DBNull.Value, dtTyousaKaisya(0).koj_hkks_tyokusou_upd_login_user_id)))
                '�H���񍐏������ύX����
                .Add(MakeParam("@koj_hkks_tyokusou_upd_datetime", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_hkks_tyokusou_upd_datetime = "", DBNull.Value, toYYYYMMDDHHmmSS(dtTyousaKaisya(0).koj_hkks_tyokusou_upd_datetime))))
            End If


            '������Ѓt���O
            .Add(MakeParam("@tys_kaisya_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tys_kaisya_flg = "", DBNull.Value, dtTyousaKaisya(0).tys_kaisya_flg)))
            '�H����Ѓt���O
            .Add(MakeParam("@koj_kaisya_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_kaisya_flg = "", DBNull.Value, dtTyousaKaisya(0).koj_kaisya_flg)))
            'JAPAN��敪
            .Add(MakeParam("@japan_kai_kbn", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).japan_kai_kbn = "", DBNull.Value, dtTyousaKaisya(0).japan_kai_kbn)))
            'JAPAN�����N��
            .Add(MakeParam("@japan_kai_nyuukai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).japan_kai_nyuukai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).japan_kai_nyuukai_date))))
            'JAPAN��މ�N��
            .Add(MakeParam("@japan_kai_taikai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).japan_kai_taikai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).japan_kai_taikai_date))))

            '�S�Z�i�敪�⑫
            .Add(MakeParam("@zenjyuhin_hosoku", SqlDbType.VarChar, 80, IIf(dtTyousaKaisya(0).zenjyuhin_hosoku = "", DBNull.Value, dtTyousaKaisya(0).zenjyuhin_hosoku)))

            'FC�X�敪
            .Add(MakeParam("@fc_ten_kbn", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_ten_kbn = "", DBNull.Value, dtTyousaKaisya(0).fc_ten_kbn)))
            'FC����N��
            .Add(MakeParam("@fc_nyuukai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_nyuukai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).fc_nyuukai_date))))
            'FC�މ�N��
            .Add(MakeParam("@fc_taikai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_taikai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).fc_taikai_date))))
            '������R
            .Add(MakeParam("@torikesi_riyuu", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).torikesi_riyuu = "", DBNull.Value, dtTyousaKaisya(0).torikesi_riyuu)))
            'ReportJHS�g�[�N���L���t���O
            .Add(MakeParam("@report_jhs_token_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).report_jhs_token_flg = "", DBNull.Value, dtTyousaKaisya(0).report_jhs_token_flg)))
            '��n�n�Ւ�����C���i�L���t���O
            .Add(MakeParam("@tkt_jbn_tys_syunin_skk_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tkt_jbn_tys_syunin_skk_flg = "", DBNull.Value, dtTyousaKaisya(0).tkt_jbn_tys_syunin_skk_flg)))
            '============2012/04/12 �ԗ� 405721 �ǉ���==========================
            '��\�Җ�
            .Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).daihyousya_mei.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).daihyousya_mei)))
            '��E��
            .Add(MakeParam("@yakusyoku_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).yakusyoku_mei.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).yakusyoku_mei)))
            '============2012/04/12 �ԗ� 405721 �ǉ���==========================

            '2013/11/04 ���F�ǉ� ��
            'SDS�ێ����
            .Add(MakeParam("@sds_hoji_info", SqlDbType.Int, 10, IIf(dtTyousaKaisya(0).sds_hoji_info.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).sds_hoji_info)))
            'SDS�䐔���
            .Add(MakeParam("@sds_daisuu_info", SqlDbType.Int, 5, IIf(dtTyousaKaisya(0).sds_daisuu_info.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).sds_daisuu_info)))
            '2013/11/04 ���F�ǉ� ��
            .Add(MakeParam("@jituzai_flg", SqlDbType.Int, 4, IIf(dtTyousaKaisya(0).jituzai_flg.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).jituzai_flg)))

            .Add(MakeParam("@a1_lifnr", SqlDbType.VarChar, 10, IIf(dtTyousaKaisya(0).a1_lifnr.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).a1_lifnr)))


            '�X�V���O�C�����[�U�[ID
            .Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).upd_login_user_id = "", DBNull.Value, dtTyousaKaisya(0).upd_login_user_id)))

        End With

        ' �N�G�����s
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' �d���`�F�b�N_������}�X�^
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">������R�[�h</param>
    ''' <param name="strSeikyuuSakiBrc">������}��</param>
    ''' <param name="strSeikyuuSakiKbn">������敪</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSaki(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal strSeikyuuSakiKbn As String, Optional ByVal strTrue As Boolean = False) As DataTable

        ' DataSet�C���X�^���X�̐���()
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   seikyuu_saki_cd, ")
            .AppendLine("   seikyuu_saki_brc, ")
            .AppendLine("   seikyuu_saki_kbn ")
            .AppendLine("FROM ")
            .AppendLine("   m_seikyuu_saki WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   seikyuu_saki_cd = @seikyuu_saki_cd ")
            .AppendLine("AND ")
            .AppendLine("   seikyuu_saki_brc = @seikyuu_saki_brc ")
            .AppendLine("AND ")
            .AppendLine("   seikyuu_saki_kbn = @seikyuu_saki_kbn ")
            'If strTrue = True Then
            '    .AppendLine("AND ")
            '    .AppendLine("   torikesi = @torikesi ")
            'End If
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, strSeikyuuSakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, strSeikyuuSakiBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, strSeikyuuSakiKbn))
        'If strTrue = True Then
        '    paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, "0"))
        'End If

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    ''' <summary>
    ''' �d���`�F�b�N_������o�^���`�}�X�^
    ''' </summary>
    ''' <param name="strSeikyuuSakiBrc">������}��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiTH(ByVal strSeikyuuSakiBrc As String) As DataTable

        ' DataSet�C���X�^���X�̐���()
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   seikyuu_saki_brc ")
            .AppendLine("FROM ")
            .AppendLine("   m_seikyuu_saki_touroku_hinagata WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   seikyuu_saki_brc = @seikyuu_saki_brc ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, strSeikyuuSakiBrc))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    ''' <summary>
    ''' ������}�X�^�e�[�u���̓o�^
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">������R�[�h</param>
    ''' <param name="strSeikyuuSakiBrc">������}��</param>
    ''' <param name="strUserId">���O�C�����[�UID</param>
    ''' <param name="blnFlg">���݃t���O</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsSeikyuuSaki(ByVal strSeikyuuSakiCd As String, _
                                    ByVal strSeikyuuSakiBrc As String, _
                                    ByVal strUserId As String, _
                                    ByVal blnFlg As Boolean) As Integer

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("   m_seikyuu_saki WITH (UPDLOCK) ")
            .AppendLine("(")
            .AppendLine("   seikyuu_saki_cd, ") '������R�[�h
            .AppendLine("   seikyuu_saki_brc, ")    '������}��
            .AppendLine("   seikyuu_saki_kbn, ")    '������敪
            .AppendLine("   torikesi, ")    '���
            .AppendLine("   add_login_user_id, ")   '�o�^���O�C�����[�U�[ID
            .AppendLine("   add_datetime ")    '�o�^����
            If blnFlg = True Then
                .AppendLine("  , ")
                .AppendLine("   tantousya_mei, ")   '�S���Җ�
                .AppendLine("   seikyuusyo_inji_bukken_mei_flg, ")  '�������󎚕������t���O
                .AppendLine("   nyuukin_kouza_no, ")    '���������ԍ�
                .AppendLine("   seikyuu_sime_date, ")   '�������ߓ�
                .AppendLine("   senpou_seikyuu_sime_date, ")    '����������ߓ�
                .AppendLine("   sousai_flg, ")  '���E�t���O
                .AppendLine("   kaisyuu_yotei_gessuu, ")    '����\�茎��
                .AppendLine("   kaisyuu_yotei_date, ")  '����\���
                .AppendLine("   seikyuusyo_hittyk_date, ")  '�������K����
                .AppendLine("   kaisyuu1_syubetu1, ")   '���1���1
                .AppendLine("   kaisyuu1_wariai1, ")    '���1����1
                .AppendLine("   kaisyuu1_tegata_site_gessuu, ") '���1��`�T�C�g����
                .AppendLine("   kaisyuu1_tegata_site_date, ")   '���1��`�T�C�g��
                .AppendLine("   kaisyuu1_seikyuusyo_yousi, ")   '���1�������p��
                .AppendLine("   kaisyuu1_syubetu2, ")   '���1���2
                .AppendLine("   kaisyuu1_wariai2, ")    '���1����2
                .AppendLine("   kaisyuu1_syubetu3, ")   '���1���3
                .AppendLine("   kaisyuu1_wariai3, ")    '���1����3
                .AppendLine("   kaisyuu_kyoukaigaku, ") '������E�z
                .AppendLine("   kaisyuu2_syubetu1, ")   '���2���1
                .AppendLine("   kaisyuu2_wariai1, ")    '���2����1
                .AppendLine("   kaisyuu2_tegata_site_gessuu, ") '���2��`�T�C�g����
                .AppendLine("   kaisyuu2_tegata_site_date, ")   '���2��`�T�C�g��
                .AppendLine("   kaisyuu2_seikyuusyo_yousi, ")   '���2�������p��
                .AppendLine("   kaisyuu2_syubetu2, ")   '���2���2
                .AppendLine("   kaisyuu2_wariai2, ")    '���2����2
                .AppendLine("   kaisyuu2_syubetu3, ")   '���2���3
                .AppendLine("   kaisyuu2_wariai3 ")     '���2����3
                '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
                .AppendLine("   ,ginkou_siten_cd ")     '��s�x�X�R�[�h
                '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            End If
            .AppendLine(")")
            .AppendLine("SELECT ")
            .AppendLine("   @seikyuu_saki_cd, ")    '������R�[�h
            .AppendLine("   @seikyuu_saki_brc, ")   '������}��
            .AppendLine("   @seikyuu_saki_kbn, ")   '������敪
            .AppendLine("   @torikesi, ")           '���
            .AppendLine("   @add_login_user_id, ")  '�o�^���O�C�����[�U�[ID
            .AppendLine("   GETDATE() ")            '�o�^����
            If blnFlg = True Then
                .AppendLine("   ,")
                .AppendLine("   tantousya_mei, ")   '�S���Җ�
                .AppendLine("   seikyuusyo_inji_bukken_mei_flg, ")  '�������󎚕������t���O
                .AppendLine("   nyuukin_kouza_no, ")    '���������ԍ�
                .AppendLine("   seikyuu_sime_date, ")   '�������ߓ�
                .AppendLine("   senpou_seikyuu_sime_date, ")    '����������ߓ�
                .AppendLine("   sousai_flg, ")  '���E�t���O
                .AppendLine("   kaisyuu_yotei_gessuu, ")    '����\�茎��
                .AppendLine("   kaisyuu_yotei_date, ")  '����\���
                .AppendLine("   seikyuusyo_hittyk_date, ")  '�������K����
                .AppendLine("   kaisyuu1_syubetu1, ")   '���1���1
                .AppendLine("   kaisyuu1_wariai1, ")    '���1����1
                .AppendLine("   kaisyuu1_tegata_site_gessuu, ") '���1��`�T�C�g����
                .AppendLine("   kaisyuu1_tegata_site_date, ")   '���1��`�T�C�g��
                .AppendLine("   kaisyuu1_seikyuusyo_yousi, ")   '���1�������p��
                .AppendLine("   kaisyuu1_syubetu2, ")   '���1���2
                .AppendLine("   kaisyuu1_wariai2, ")    '���1����2
                .AppendLine("   kaisyuu1_syubetu3, ")   '���1���3
                .AppendLine("   kaisyuu1_wariai3, ")    '���1����3
                .AppendLine("   kaisyuu_kyoukaigaku, ") '������E�z
                .AppendLine("   kaisyuu2_syubetu1, ")   '���2���1
                .AppendLine("   kaisyuu2_wariai1, ")    '���2����1
                .AppendLine("   kaisyuu2_tegata_site_gessuu, ") '���2��`�T�C�g����
                .AppendLine("   kaisyuu2_tegata_site_date, ")   '���2��`�T�C�g��
                .AppendLine("   kaisyuu2_seikyuusyo_yousi, ")   '���2�������p��
                .AppendLine("   kaisyuu2_syubetu2, ")   '���2���2
                .AppendLine("   kaisyuu2_wariai2, ")    '���2����2
                .AppendLine("   kaisyuu2_syubetu3, ")   '���2���3
                .AppendLine("   kaisyuu2_wariai3 ")     '���2����3
                '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
                .AppendLine("   ,ginkou_siten_cd ")     '��s�x�X�R�[�h
                '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
                .AppendLine("FROM ")
                .AppendLine("   m_seikyuu_saki_touroku_hinagata WITH (READCOMMITTED) ")
                .AppendLine("WHERE ")
                .AppendLine("   seikyuu_saki_brc = @seikyuu_saki_brc ")
            End If
        End With

        '�p�����[�^�̐ݒ�
        With paramList
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(strSeikyuuSakiCd = "", DBNull.Value, strSeikyuuSakiCd.ToUpper)))
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(strSeikyuuSakiBrc = "", DBNull.Value, strSeikyuuSakiBrc.ToUpper)))
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, "1"))
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, "0"))
            .Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 255, IIf(strUserId = "", DBNull.Value, strUserId)))
        End With

        ' �N�G�����s
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' ������.�����{�^������������_������o�^���`�}�X�^�`�F�b�N
    ''' </summary>
    ''' <param name="strSeikyuuSakiBrc">������}��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiTouroku(ByVal strSeikyuuSakiBrc As String) As DataTable

        ' DataSet�C���X�^���X�̐���()
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   hyouji_naiyou ")
            .AppendLine("FROM ")
            .AppendLine("   m_seikyuu_saki_touroku_hinagata WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   seikyuu_saki_brc = @seikyuu_saki_brc ")
            .AppendLine("AND ")
            .AppendLine("   torikesi = '0' ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, strSeikyuuSakiBrc))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function InsTyousaKaisya(ByVal dtTyousaKaisya As TyousaKaisyaDataSet.m_tyousakaisyaDataTable, ByVal strHenkou As String, ByVal strDisplayName As String) As Integer

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("   m_tyousakaisya WITH (UPDLOCK) ")
            .AppendLine("( ")
            .AppendLine("   tys_kaisya_cd, ")   '������ЃR�[�h
            .AppendLine("   jigyousyo_cd, ")    '���Ə��R�[�h
            .AppendLine("   torikesi, ")    '���
            .AppendLine("   tys_kaisya_mei, ")  '������Ж�
            .AppendLine("   tys_kaisya_mei_kana, ") '������Ж��J�i
            .AppendLine("   seikyuu_saki_shri_saki_mei, ")  '������x���於
            .AppendLine("   seikyuu_saki_shri_saki_kana, ") '������x���於�J�i
            .AppendLine("   jyuusyo1, ")    '�Z��1
            .AppendLine("   jyuusyo2, ")    '�Z��2
            .AppendLine("   yuubin_no, ")   '�X�֔ԍ�
            .AppendLine("   tel_no, ")  '�d�b�ԍ�
            .AppendLine("   fax_no, ")  'FAX�ԍ�
            .AppendLine("   seikyuu_saki_cd, ") '������R�[�h
            .AppendLine("   seikyuu_saki_brc, ")    '������}��
            .AppendLine("   seikyuu_saki_kbn, ")    '������敪
            '.AppendLine("   seikyuu_sime_date, ")   '�������ߓ�
            .AppendLine("   skysy_soufu_jyuusyo1, ")    '���������t��Z��1
            .AppendLine("   skysy_soufu_jyuusyo2, ")    '���������t��Z��2
            .AppendLine("   skysy_soufu_yuubin_no, ")   '���������t��X�֔ԍ�
            .AppendLine("   skysy_soufu_tel_no, ")  '���������t��d�b�ԍ�
            .AppendLine("   skk_shri_saki_cd, ")    '�V��v�x����R�[�h
            .AppendLine("   skk_jigyousyo_cd, ")    '�V��v���Ə��R�[�h
            .AppendLine("   shri_meisai_jigyousyo_cd, ")    '�x�����׏W�v�掖�Ə��R�[�h
            .AppendLine("   shri_jigyousyo_cd, ")   '�x���W�v�掖�Ə��R�[�h
            .AppendLine("   shri_sime_date, ")  '�x�����ߓ�
            .AppendLine("   shri_yotei_gessuu, ")   '�x���\�茎��
            .AppendLine("   fctring_kaisi_nengetu, ")   '�t�@�N�^�����O�J�n�N��
            .AppendLine("   shri_you_fax_no, ") '�x���pFAX�ԍ�
            .AppendLine("   ss_kijyun_kkk, ")   'SS����i
            .AppendLine("   fc_ten_cd, ")   'FC�X�R�[�h
            .AppendLine("   kensa_center_cd, ") '�����Z���^�[�R�[�h
            .AppendLine("   koj_hkks_tyokusou_flg, ")   '�H���񍐏�����
            .AppendLine("   koj_hkks_tyokusou_upd_login_user_id, ") '�H���񍐏������ύX���O�C�����[�U�[ID
            .AppendLine("   koj_hkks_tyokusou_upd_datetime, ")  '�H���񍐏������ύX����
            .AppendLine("   tys_kaisya_flg, ")  '������Ѓt���O
            .AppendLine("   koj_kaisya_flg, ")  '�H����Ѓt���O
            .AppendLine("   japan_kai_kbn, ")   'JAPAN��敪
            .AppendLine("   japan_kai_nyuukai_date, ")  'JAPAN�����N��
            .AppendLine("   japan_kai_taikai_date, ")   'JAPAN��މ�N��
            .AppendLine("   zenjyuhin_hosoku, ")   '�S�Z�i�敪�⑫

            .AppendLine("   fc_ten_kbn, ")  'FC�X�敪
            .AppendLine("   fc_nyuukai_date, ") 'FC����N��
            .AppendLine("   fc_taikai_date, ")  'FC�މ�N��
            .AppendLine("   torikesi_riyuu, ")  '������R
            .AppendLine("   report_jhs_token_flg, ")    'ReportJHS�g�[�N���L���t���O
            .AppendLine("   tkt_jbn_tys_syunin_skk_flg, ")  '��n�n�Ւ�����C���i�L���t���O
            '============2012/04/12 �ԗ� 405721 �ǉ���==========================
            .AppendLine("   daihyousya_mei, ")        '��\�Җ�
            .AppendLine("   yakusyoku_mei, ")        '��E��
            '============2012/04/12 �ԗ� 405721 �ǉ���==========================
            '2013/11/04 ���F�ǉ� ��
            .AppendLine("   sds_hoji_info, ")        'SDS�ێ����
            .AppendLine("   sds_daisuu_info, ")      'SDS�䐔���
            .AppendLine("   jituzai_flg, ")
            '2013/11/04 ���F�ǉ� ��

            .AppendLine("   a1_lifnr, ")

            .AppendLine("   add_login_user_id, ")   '�o�^���O�C�����[�U�[ID
            .AppendLine("   add_datetime ")    '�o�^����
            .AppendLine(") ")
            .AppendLine("SELECT ")
            .AppendLine("   @tys_kaisya_cd, ")  '������ЃR�[�h
            .AppendLine("   @jigyousyo_cd, ")   '���Ə��R�[�h
            .AppendLine("   @torikesi, ")   '���
            .AppendLine("   @tys_kaisya_mei, ") '������Ж�
            .AppendLine("   @tys_kaisya_mei_kana, ")    '������Ж��J�i
            .AppendLine("   @seikyuu_saki_shri_saki_mei, ") '������x���於
            .AppendLine("   @seikyuu_saki_shri_saki_kana, ")    '������x���於�J�i
            .AppendLine("   @jyuusyo1, ")   '�Z��1
            .AppendLine("   @jyuusyo2, ")   '�Z��2
            .AppendLine("   @yuubin_no, ")  '�X�֔ԍ�
            .AppendLine("   @tel_no, ") '�d�b�ԍ�
            .AppendLine("   @fax_no, ") 'FAX�ԍ�
            .AppendLine("   @seikyuu_saki_cd, ")    '������R�[�h
            .AppendLine("   @seikyuu_saki_brc, ")   '������}��
            .AppendLine("   @seikyuu_saki_kbn, ")   '������敪
            '.AppendLine("   @seikyuu_sime_date, ")  '�������ߓ�
            .AppendLine("   @skysy_soufu_jyuusyo1, ")   '���������t��Z��1
            .AppendLine("   @skysy_soufu_jyuusyo2, ")   '���������t��Z��2
            .AppendLine("   @skysy_soufu_yuubin_no, ")  '���������t��X�֔ԍ�
            .AppendLine("   @skysy_soufu_tel_no, ") '���������t��d�b�ԍ�
            .AppendLine("   @skk_shri_saki_cd, ")   '�V��v�x����R�[�h
            .AppendLine("   @skk_jigyousyo_cd, ")   '�V��v���Ə��R�[�h
            .AppendLine("   @shri_meisai_jigyousyo_cd, ")   '�x�����׏W�v�掖�Ə��R�[�h
            .AppendLine("   @shri_jigyousyo_cd, ")  '�x���W�v�掖�Ə��R�[�h
            .AppendLine("   @shri_sime_date, ") '�x�����ߓ�
            .AppendLine("   @shri_yotei_gessuu, ")  '�x���\�茎��
            .AppendLine("   @fctring_kaisi_nengetu, ")  '�t�@�N�^�����O�J�n�N��
            .AppendLine("   @shri_you_fax_no, ")    '�x���pFAX�ԍ�
            .AppendLine("   @ss_kijyun_kkk, ")  'SS����i
            .AppendLine("   @fc_ten_cd, ")  'FC�X�R�[�h
            .AppendLine("   @kensa_center_cd, ")    '�����Z���^�[�R�[�h
            .AppendLine("   @koj_hkks_tyokusou_flg, ")  '�H���񍐏�����
            .AppendLine("   @koj_hkks_tyokusou_upd_login_user_id, ")    '�H���񍐏������ύX���O�C�����[�U�[ID
            If strHenkou = "YES" Then
                .AppendLine("   GETDATE(), ")   '�H���񍐏������ύX����
            Else
                .AppendLine("   @koj_hkks_tyokusou_upd_datetime, ") '�H���񍐏������ύX����
            End If
            .AppendLine("   @tys_kaisya_flg, ") '������Ѓt���O
            .AppendLine("   @koj_kaisya_flg, ") '�H����Ѓt���O
            .AppendLine("   @japan_kai_kbn, ")  'JAPAN��敪
            .AppendLine("   @japan_kai_nyuukai_date, ") 'JAPAN�����N��
            .AppendLine("   @japan_kai_taikai_date, ")  'JAPAN��މ�N��
            .AppendLine("   @zenjyuhin_hosoku, ")   '�S�Z�i�敪�⑫
            .AppendLine("   @fc_ten_kbn, ") 'FC�X�敪
            .AppendLine("   @fc_nyuukai_date, ")    'FC����N��
            .AppendLine("   @fc_taikai_date, ") 'FC�މ�N��
            .AppendLine("   @torikesi_riyuu, ") '������R
            .AppendLine("   @report_jhs_token_flg, ")   'ReportJHS�g�[�N���L���t���O
            .AppendLine("   @tkt_jbn_tys_syunin_skk_flg, ") '��n�n�Ւ�����C���i�L���t���O


            '============2012/04/12 �ԗ� 405721 �ǉ���==========================
            .AppendLine("   @daihyousya_mei, ")        '��\�Җ�
            .AppendLine("   @yakusyoku_mei, ")        '��E��
            '============2012/04/12 �ԗ� 405721 �ǉ���==========================
            '2013/11/04 ���F�ǉ� ��
            .AppendLine("   @sds_hoji_info, ")        'SDS�ێ����
            .AppendLine("   @sds_daisuu_info, ")      'SDS�䐔���
            .AppendLine("   @jituzai_flg, ")

            .AppendLine("   @a1_lifnr, ")
            '2013/11/04 ���F�ǉ� ��
            .AppendLine("   @add_login_user_id, ")  '�o�^���O�C�����[�U�[ID
            .AppendLine("   GETDATE() ")   '�o�^����
        End With

        '�p�����[�^�̐ݒ�
        With paramList
            '������ЃR�[�h
            .Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 255, dtTyousaKaisya(0).tys_kaisya_cd))
            '���Ə��R�[�h
            .Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 255, dtTyousaKaisya(0).jigyousyo_cd))
            '���
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtTyousaKaisya(0).torikesi))
            '������Ж�
            .Add(MakeParam("@tys_kaisya_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tys_kaisya_mei = "", DBNull.Value, dtTyousaKaisya(0).tys_kaisya_mei)))
            '������Ж��J�i
            .Add(MakeParam("@tys_kaisya_mei_kana", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tys_kaisya_mei_kana = "", DBNull.Value, dtTyousaKaisya(0).tys_kaisya_mei_kana)))
            '������x���於
            .Add(MakeParam("@seikyuu_saki_shri_saki_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_shri_saki_mei = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_shri_saki_mei)))
            '������x���於�J�i
            .Add(MakeParam("@seikyuu_saki_shri_saki_kana", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_shri_saki_kana = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_shri_saki_kana)))
            '�Z��1
            .Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).jyuusyo1 = "", DBNull.Value, dtTyousaKaisya(0).jyuusyo1)))
            '�Z��2
            .Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).jyuusyo2 = "", DBNull.Value, dtTyousaKaisya(0).jyuusyo2)))
            '�X�֔ԍ�
            .Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).yuubin_no = "", DBNull.Value, dtTyousaKaisya(0).yuubin_no)))
            '�d�b�ԍ�
            .Add(MakeParam("@tel_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tel_no = "", DBNull.Value, dtTyousaKaisya(0).tel_no)))
            'FAX�ԍ�
            .Add(MakeParam("@fax_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fax_no = "", DBNull.Value, dtTyousaKaisya(0).fax_no)))
            '������R�[�h
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_cd = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_cd)))
            '������}��
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_brc = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_brc)))
            '������敪
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).seikyuu_saki_kbn = "", DBNull.Value, dtTyousaKaisya(0).seikyuu_saki_kbn)))
            '���������t��Z��1
            .Add(MakeParam("@skysy_soufu_jyuusyo1", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_jyuusyo1 = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_jyuusyo1)))
            '���������t��Z��2
            .Add(MakeParam("@skysy_soufu_jyuusyo2", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_jyuusyo2 = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_jyuusyo2)))
            '���������t��X�֔ԍ�
            .Add(MakeParam("@skysy_soufu_yuubin_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_yuubin_no = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_yuubin_no)))
            '���������t��d�b�ԍ�
            .Add(MakeParam("@skysy_soufu_tel_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skysy_soufu_tel_no = "", DBNull.Value, dtTyousaKaisya(0).skysy_soufu_tel_no)))
            '�V��v�x����R�[�h
            .Add(MakeParam("@skk_shri_saki_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skk_shri_saki_cd = "", DBNull.Value, dtTyousaKaisya(0).skk_shri_saki_cd)))
            '�V��v���Ə��R�[�h
            .Add(MakeParam("@skk_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).skk_jigyousyo_cd = "", DBNull.Value, dtTyousaKaisya(0).skk_jigyousyo_cd)))
            '�x�����׏W�v�掖�Ə��R�[�h
            .Add(MakeParam("@shri_meisai_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_meisai_jigyousyo_cd = "", DBNull.Value, dtTyousaKaisya(0).shri_meisai_jigyousyo_cd)))
            '�x���W�v�掖�Ə��R�[�h
            .Add(MakeParam("@shri_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_jigyousyo_cd = "", DBNull.Value, dtTyousaKaisya(0).shri_jigyousyo_cd)))
            '�x�����ߓ�
            .Add(MakeParam("@shri_sime_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_sime_date = "", DBNull.Value, dtTyousaKaisya(0).shri_sime_date)))
            '�x���\�茎��
            .Add(MakeParam("@shri_yotei_gessuu", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_yotei_gessuu = "", DBNull.Value, dtTyousaKaisya(0).shri_yotei_gessuu)))
            '�t�@�N�^�����O�J�n�N��
            .Add(MakeParam("@fctring_kaisi_nengetu", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fctring_kaisi_nengetu = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).fctring_kaisi_nengetu))))
            '�x���pFAX�ԍ�
            .Add(MakeParam("@shri_you_fax_no", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).shri_you_fax_no = "", DBNull.Value, dtTyousaKaisya(0).shri_you_fax_no)))
            'SS����i
            .Add(MakeParam("@ss_kijyun_kkk", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).ss_kijyun_kkk = "", DBNull.Value, dtTyousaKaisya(0).ss_kijyun_kkk)))
            'FC�X�R�[�h
            .Add(MakeParam("@fc_ten_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_ten_cd = "", DBNull.Value, dtTyousaKaisya(0).fc_ten_cd)))
            '�����Z���^�[�R�[�h
            .Add(MakeParam("@kensa_center_cd", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).kensa_center_cd = "", DBNull.Value, dtTyousaKaisya(0).kensa_center_cd)))
            '�H���񍐏�����
            .Add(MakeParam("@koj_hkks_tyokusou_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_hkks_tyokusou_flg = "", DBNull.Value, dtTyousaKaisya(0).koj_hkks_tyokusou_flg)))

            If strHenkou = "YES" Then
                '�H���񍐏������ύX���O�C�����[�U�[ID
                .Add(MakeParam("@koj_hkks_tyokusou_upd_login_user_id", SqlDbType.VarChar, 255, IIf(strDisplayName = "", DBNull.Value, strDisplayName)))
            Else
                '�H���񍐏������ύX���O�C�����[�U�[ID
                .Add(MakeParam("@koj_hkks_tyokusou_upd_login_user_id", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_hkks_tyokusou_upd_login_user_id = "", DBNull.Value, dtTyousaKaisya(0).koj_hkks_tyokusou_upd_login_user_id)))
                '�H���񍐏������ύX����
                .Add(MakeParam("@koj_hkks_tyokusou_upd_datetime", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_hkks_tyokusou_upd_datetime = "", DBNull.Value, toYYYYMMDDHHmmSS(dtTyousaKaisya(0).koj_hkks_tyokusou_upd_datetime))))
            End If

            '������Ѓt���O
            .Add(MakeParam("@tys_kaisya_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tys_kaisya_flg = "", DBNull.Value, dtTyousaKaisya(0).tys_kaisya_flg)))
            '�H����Ѓt���O
            .Add(MakeParam("@koj_kaisya_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).koj_kaisya_flg = "", DBNull.Value, dtTyousaKaisya(0).koj_kaisya_flg)))
            'JAPAN��敪
            .Add(MakeParam("@japan_kai_kbn", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).japan_kai_kbn = "", DBNull.Value, dtTyousaKaisya(0).japan_kai_kbn)))
            'JAPAN�����N��
            .Add(MakeParam("@japan_kai_nyuukai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).japan_kai_nyuukai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).japan_kai_nyuukai_date))))
            'JAPAN��މ�N��
            .Add(MakeParam("@japan_kai_taikai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).japan_kai_taikai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).japan_kai_taikai_date))))

            '�S�Z�i�敪�⑫
            .Add(MakeParam("@zenjyuhin_hosoku", SqlDbType.VarChar, 80, IIf(dtTyousaKaisya(0).zenjyuhin_hosoku = "", DBNull.Value, dtTyousaKaisya(0).zenjyuhin_hosoku)))

            'FC�X�敪
            .Add(MakeParam("@fc_ten_kbn", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_ten_kbn = "", DBNull.Value, dtTyousaKaisya(0).fc_ten_kbn)))
            'FC����N��
            .Add(MakeParam("@fc_nyuukai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_nyuukai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).fc_nyuukai_date))))
            'FC�މ�N��
            .Add(MakeParam("@fc_taikai_date", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).fc_taikai_date = "", DBNull.Value, toYYYYMMDD(dtTyousaKaisya(0).fc_taikai_date))))
            '������R
            .Add(MakeParam("@torikesi_riyuu", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).torikesi_riyuu = "", DBNull.Value, dtTyousaKaisya(0).torikesi_riyuu)))
            'ReportJHS�g�[�N���L���t���O
            .Add(MakeParam("@report_jhs_token_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).report_jhs_token_flg = "", DBNull.Value, dtTyousaKaisya(0).report_jhs_token_flg)))
            '��n�n�Ւ�����C���i�L���t���O
            .Add(MakeParam("@tkt_jbn_tys_syunin_skk_flg", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).tkt_jbn_tys_syunin_skk_flg = "", DBNull.Value, dtTyousaKaisya(0).tkt_jbn_tys_syunin_skk_flg)))
            '============2012/04/12 �ԗ� 405721 �ǉ���==========================
            '��\�Җ�
            .Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).daihyousya_mei.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).daihyousya_mei)))
            '��E��
            .Add(MakeParam("@yakusyoku_mei", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).yakusyoku_mei.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).yakusyoku_mei)))
            '============2012/04/12 �ԗ� 405721 �ǉ���==========================
            '2013/11/04 ���F�ǉ� ��
            'SDS�ێ����
            .Add(MakeParam("@sds_hoji_info", SqlDbType.Int, 10, IIf(dtTyousaKaisya(0).sds_hoji_info.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).sds_hoji_info)))
            'SDS�䐔���
            .Add(MakeParam("@sds_daisuu_info", SqlDbType.Int, 5, IIf(dtTyousaKaisya(0).sds_daisuu_info.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).sds_daisuu_info)))
            '2013/11/04 ���F�ǉ� ��
            .Add(MakeParam("@jituzai_flg", SqlDbType.Int, 4, IIf(dtTyousaKaisya(0).jituzai_flg.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).jituzai_flg)))

            .Add(MakeParam("@a1_lifnr", SqlDbType.VarChar, 10, IIf(dtTyousaKaisya(0).a1_lifnr.Equals(String.Empty), DBNull.Value, dtTyousaKaisya(0).a1_lifnr)))

            '�o�^���O�C�����[�U�[ID
            .Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 255, IIf(dtTyousaKaisya(0).a1_lifnr = "", DBNull.Value, dtTyousaKaisya(0).a1_lifnr)))

        End With

        ' �N�G�����s
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    '''' <summary>
    '''' �e�b�}�X�^
    '''' </summary>
    '''' <param name="strFCCd">���������Z���^�[�R�[�h</param>
    'Public Function SelMfcInfo(ByVal strFCCd As String) As DataTable

    '    ' DataSet�C���X�^���X�̐���
    '    Dim dsDataSet As New DataSet

    '    'SQL���̐���
    '    Dim commandTextSb As New StringBuilder

    '    '�p�����[�^�i�[
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL��
    '    With commandTextSb
    '        .AppendLine(" SELECT   ")
    '        .AppendLine("   fc_cd   ")
    '        .AppendLine("   ,fc_nm   ")
    '        .AppendLine(" FROM  ")
    '        .AppendLine("   " & connDbTable_m_fc & " WITH (READCOMMITTED)  ")
    '        .AppendLine(" WHERE  fc_cd=@fc_cd ")
    '        .AppendLine(" ORDER BY fc_cd ")
    '    End With

    '    '�p�����[�^�̐ݒ�
    '    paramList.Add(MakeParam("@fc_cd", SqlDbType.VarChar, 10, strFCCd))

    '    ' �������s
    '    FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
    '                "dsDataSet", paramList.ToArray)

    '    '�߂�
    '    Return dsDataSet.Tables(0)

    'End Function

    ''' <summary>
    ''' �H���񍐏��������擾
    ''' </summary>
    ''' <param name="strUserId">���O�C�����[�U</param>
    Public Function SelKoujiInfo(ByVal strUserId As String) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT   ")
            .AppendLine("   MJM.DisplayName ")
            .AppendLine("FROM  ")
            .AppendLine("   m_jiban_ninsyou MJN WITH (READCOMMITTED)  ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_jhs_mailbox MJM WITH (READCOMMITTED)  ")
            .AppendLine("ON ")
            .AppendLine("   MJN.login_user_id = MJM.PrimaryWindowsNTAccount")
            .AppendLine("WHERE ")
            .AppendLine("   MJN.login_user_id = @login_user_id ")

        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 64, strUserId))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' MailAddress�@�擾
    ''' </summary>
    ''' <param name="yuubin_no">�X��No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMailAddress(ByVal yuubin_no As String) As DataSet

        ' DataSet�C���X�^���X�̐���
        Dim data As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        sql.AppendLine("SELECT (yuubin_no + ',' + isnull(todoufuken_mei,'')+     isnull(sikutyouson_mei,'')+      isnull(tiiki_mei,'')) as mei")
        sql.AppendLine("    from ")
        sql.AppendLine("    m_yuubin ")
        sql.AppendLine("    where ")
        sql.AppendLine("    yuubin_no like @yuubin_no order by yuubin_no")

        paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 8, yuubin_no & "%"))
        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), data, _
                   "mei", paramList.ToArray)

        Return data

    End Function

    ''' <summary>
    ''' ���t�^�ύX����
    ''' </summary>
    ''' <param name="ymd">�N����</param>
    ''' <returns></returns>
    ''' <remarks>2010/01/12 �n���R�i��A�j �V�K�쐬</remarks>
    Public Function toYYYYMMDD(ByVal ymd As Object) As Object

        If ymd.Equals(String.Empty) Or ymd.Equals(DBNull.Value) Then
            Return ymd
        Else
            Return CDate(ymd).ToString("yyyy/MM/dd")
        End If

    End Function

    ''' <summary>
    ''' ���t�^�ύX����
    ''' </summary>
    ''' <param name="ymd">�N����</param>
    ''' <returns></returns>
    ''' <remarks>2010/01/12 �n���R�i��A�j �V�K�쐬</remarks>
    Public Function toYYYYMMDDHHmmSS(ByVal ymd As Object) As Object

        If ymd.Equals(String.Empty) Or ymd.Equals(DBNull.Value) Then
            Return ymd
        Else
            Return CDate(ymd).ToString("yyyy/MM/dd hh:mm:ss")
        End If

    End Function

    ''' <summary>
    ''' FC�R�[�h���݃`�F�b�N
    ''' </summary>
    Public Function SelFCTenInfo(ByVal strFcCd As String) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   keiretu_cd ")
            .AppendLine("FROM ")
            .AppendLine("   m_keiretu WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   keiretu_cd = @keiretu_cd ")
            .AppendLine("AND ")
            .AppendLine("   kbn = @kbn ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, strFcCd))
        paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, "A"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �X�֔ԍ����݃`�F�b�N
    ''' </summary>
    Public Function SelYuubinInfo(ByVal strBangou As String) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   yuubin_no ")
            .AppendLine("FROM ")
            .AppendLine("   m_yuubin WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   yuubin_no = @yuubin_no ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 7, strBangou))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' ������Ѓ}�X�^
    ''' </summary>
    Public Function SelTyousaKaisya(ByVal strKaisyaCd As String, ByVal strJigyouCd As String, ByVal bloKbn As Boolean) As CommonSearchDataSet.tyousakaisyaTableDataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New CommonSearchDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   tys_kaisya_cd, ")
            .AppendLine("   jigyousyo_cd, ")
            .AppendLine("   tys_kaisya_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_tyousakaisya WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   tys_kaisya_cd + jigyousyo_cd = @strKaisyaCd ")
        End With
        If bloKbn = True Then
            commandTextSb.Append(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        End If

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@strKaisyaCd", SqlDbType.VarChar, 7, strKaisyaCd & strJigyouCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.tyousakaisyaTable.TableName, paramList.ToArray)

        '�߂�
        Return dsDataSet.tyousakaisyaTable

    End Function

    ''' <summary>
    ''' FC�X�}�X�^
    ''' </summary>
    Public Function SelFCTen(ByVal strFCCd As String) As CommonSearchDataSet.KeiretuTableDataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New CommonSearchDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   kbn, ")
            .AppendLine("   keiretu_cd, ")
            .AppendLine("   keiretu_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_keiretu WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   torikesi = @torikesi ")
            .AppendLine("AND ")
            .AppendLine("   keiretu_cd = @keiretu_cd ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, "0"))
        paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 6, strFCCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.KeiretuTable.TableName, paramList.ToArray)

        '�߂�
        Return dsDataSet.KeiretuTable

    End Function

    ''' <summary>
    ''' �V��v�x����}�X�^
    ''' </summary>
    Public Function SelSKK(ByVal strJigyouCd As String, ByVal strShriCd As String) As DataTable

        ' DataSet�C���X�^���X�̐���skk_jigyou_cd
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   skk_jigyou_cd, ")
            .AppendLine("   skk_shri_saki_cd, ")
            .AppendLine("   shri_saki_mei_kanji ")
            .AppendLine("FROM ")
            .AppendLine("   m_sinkaikei_siharai_saki WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   skk_jigyou_cd = @skk_jigyou_cd ")
            .AppendLine("AND ")
            .AppendLine("   skk_shri_saki_cd = @skk_shri_saki_cd ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@skk_jigyou_cd", SqlDbType.VarChar, 10, strJigyouCd))
        paramList.Add(MakeParam("@skk_shri_saki_cd", SqlDbType.VarChar, 10, strShriCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' ������}�X�^�r���[
    ''' </summary>
    Public Function SelVSeikyuuSakiInfo(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String, ByVal blnDelete As Boolean) As CommonSearchDataSet.SeikyuuSakiTable1DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New CommonSearchDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   seikyuu_saki_kbn, ")
            .AppendLine("   RTRIM(seikyuu_saki_cd) AS seikyuu_saki_cd, ")
            .AppendLine("   seikyuu_saki_brc, ")
            .AppendLine("   seikyuu_saki_mei, ")
            .AppendLine("   torikesi ")
            .AppendLine("FROM ")
            .AppendLine("   v_seikyuu_saki_info WITH(READCOMMITTED)  ")
            .AppendLine(" WHERE ")
        End With

        If blnDelete = True Then
            commandTextSb.Append(" torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, "0"))
        Else
            commandTextSb.Append("  0 = 0 ")
        End If

        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND seikyuu_saki_cd = @seikyuu_saki_cd ")
        End If
        If strBrc.Trim <> "" Then
            commandTextSb.AppendLine(" AND seikyuu_saki_brc = @seikyuu_saki_brc ")
        End If
        If strKbn.Trim <> "" Then
            commandTextSb.AppendLine(" AND seikyuu_saki_kbn = @seikyuu_saki_kbn ")
        End If

        '�p�����[�^�̐ݒ�
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, strKbn))
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, strCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, strBrc))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.SeikyuuSakiTable1.TableName, paramList.ToArray)

        '�߂�
        Return dsDataSet.SeikyuuSakiTable1

    End Function

    ''' <summary>
    ''' �c�Ə��}�X�^
    ''' </summary>
    Public Function SelEigyousyo(ByVal strFCCd As String) As CommonSearchDataSet.EigyousyoTableDataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New CommonSearchDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   eigyousyo_cd, ")
            .AppendLine("   eigyousyo_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_eigyousyo WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   torikesi = @torikesi ")
            .AppendLine("AND ")
            .AppendLine("   eigyousyo_cd = @eigyousyo_cd ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, "0"))
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 6, strFCCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.EigyousyoTable.TableName, paramList.ToArray)

        '�߂�
        Return dsDataSet.EigyousyoTable

    End Function

    '==============2011/06/24 �ԗ� �y�A�g������Ѓ}�X�^�̓o�^�E�폜�����z�̒ǉ� �J�n��======================
    ''' <summary>
    ''' �A�g������Ѓ}�X�^���擾����
    ''' </summary>
    Public Function SelRenkeiTyousakaisyaMaster(ByVal strTyousakaisyaCd As String, ByVal strJigyousyoCd As String) As Data.DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	tys_kaisya_cd ")
            .AppendLine("	,jigyousyo_cd ")
            .AppendLine("FROM ")
            .AppendLine("	m_renkei_tys_kaisya WITH (READCOMMITTED) ")
            .AppendLine("where ")
            .AppendLine("	tys_kaisya_cd = @tys_kaisya_cd ")
            .AppendLine("	and ")
            .AppendLine("	jigyousyo_cd = @jigyousyo_cd ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTyousakaisyaCd))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtRenkeiTyousakaisyaMaster", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �A�g������Ѓ}�X�^���폜����
    ''' </summary>
    Public Function DelRenkeiTyousakaisyaMaster(ByVal strTyousakaisyaCd As String, ByVal strJigyousyoCd As String) As Boolean

        '�폜����
        Dim intDelCount As Integer

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("DELETE  ")
            .AppendLine("FROM  ")
            .AppendLine("	m_renkei_tys_kaisya WITH (UPDLOCK) ")
            .AppendLine("WHERE  ")
            .AppendLine("	tys_kaisya_cd = @tys_kaisya_cd ")
            .AppendLine("	and ")
            .AppendLine("	jigyousyo_cd = @jigyousyo_cd ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTyousakaisyaCd))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))

        '���s
        Try
            intDelCount = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
        Catch ex As Exception
            Return False
        End Try

        If intDelCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' �A�g������Ѓ}�X�^��o�^����
    ''' </summary>
    Public Function InsRenkeiTyousakaisyaMaster(ByVal strTyousakaisyaCd As String, ByVal strJigyousyoCd As String, ByVal strUserId As String) As Boolean
        '�폜����
        Dim intInsCount As Integer

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("INSERT INTO  ")
            .AppendLine("	m_renkei_tys_kaisya WITH (UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		tys_kaisya_cd ")
            .AppendLine("		,jigyousyo_cd ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@tys_kaisya_cd ")
            .AppendLine("	,@jigyousyo_cd ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strTyousakaisyaCd))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strJigyousyoCd))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))

        '���s
        Try
            intInsCount = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
        Catch ex As Exception
            Return False
        End Try

        If intInsCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    '==============2011/06/24 �ԗ� �y�A�g������Ѓ}�X�^�̓o�^�E�폜�����z�̒ǉ� �I����======================

End Class
