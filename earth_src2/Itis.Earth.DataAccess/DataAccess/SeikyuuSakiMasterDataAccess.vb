Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
''' <summary>
''' ������}�X�^
''' </summary>
''' <history>
''' <para>2010/05/24�@�n���R(��A)�@�V�K�쐬</para>
''' </history>
Public Class SeikyuuSakiMasterDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' ������o�^���`�}�X�^
    ''' </summary>
    Public Function SelSeikyuuSakiTourokuHinagataInfo() As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT   ")
            .AppendLine("   seikyuu_saki_brc   ")
            .AppendLine("   ,hyouji_naiyou   ")
            .AppendLine(" FROM  ")
            .AppendLine("   m_seikyuu_saki_touroku_hinagata WITH (READCOMMITTED)  ")
            .AppendLine(" ORDER BY seikyuu_saki_brc ")
        End With

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet")

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

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
    ''' ��������̎擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiInfo(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal SeikyuuSakiKbn As String) As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New SeikyuuSakiDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   MSS.seikyuu_saki_cd, ")     '������R�[�h
            .AppendLine("   MSS.seikyuu_saki_brc, ")        '������}��
            .AppendLine("   MSS.seikyuu_saki_kbn, ")        '������敪
            .AppendLine("   MSS.torikesi, ")        '���
            .AppendLine("   MSS.skk_jigyousyo_cd, ")        '�V��v���Ə��R�[�h
            .AppendLine("   MSS.kyuu_seikyuu_saki_cd, ")        '��������R�[�h

            .AppendLine("   MSS.skysy_soufu_jyuusyo1, ")        '���������t��Z��1
            .AppendLine("   MSS.skysy_soufu_jyuusyo2, ")        '���������t��Z��2
            .AppendLine("   MSS.skysy_soufu_yuubin_no, ")        '���������t��X�֔ԍ�
            .AppendLine("   MSS.skysy_soufu_tel_no, ")        '���������t��d�b�ԍ�
            .AppendLine("   MSS.skysy_soufu_fax_no, ")        '���������t��FAX�ԍ�

            .AppendLine("   MSS.tantousya_mei, ")       '�S���Җ�
            .AppendLine("   MSS.seikyuusyo_inji_bukken_mei_flg, ")      '�������󎚕������t���O
            .AppendLine("   MSS.nyuukin_kouza_no, ")        '���������ԍ�
            .AppendLine("   MSS.seikyuu_sime_date, ")       '�������ߓ�
            .AppendLine("   MSS.senpou_seikyuu_sime_date, ")        '����������ߓ�
            .AppendLine("   MSS.tyk_koj_seikyuu_timing_flg, ")      '���H�������^�C�~���O�t���O
            .AppendLine("   MSS.sousai_flg, ")      '���E�t���O
            .AppendLine("   MSS.kaisyuu_yotei_gessuu, ")        '����\�茎��
            .AppendLine("   MSS.kaisyuu_yotei_date, ")      '����\���
            .AppendLine("   MSS.seikyuusyo_hittyk_date, ")      '�������K����
            .AppendLine("   MSS.kaisyuu1_syubetu1, ")       '���1���1
            .AppendLine("   MSS.kaisyuu1_wariai1, ")        '���1����1
            .AppendLine("   MSS.kaisyuu1_tegata_site_gessuu, ")     '���1��`�T�C�g����
            .AppendLine("   MSS.kaisyuu1_tegata_site_date, ")       '���1��`�T�C�g��
            .AppendLine("   MSS.kaisyuu1_seikyuusyo_yousi, ")       '���1�������p��
            .AppendLine("   MSS.kaisyuu1_syubetu2, ")       '���1���2
            .AppendLine("   MSS.kaisyuu1_wariai2, ")        '���1����2
            .AppendLine("   MSS.kaisyuu1_syubetu3, ")       '���1���3
            .AppendLine("   MSS.kaisyuu1_wariai3, ")        '���1����3
            .AppendLine("   MSS.kaisyuu_kyoukaigaku, ")     '������E�z
            .AppendLine("   MSS.kaisyuu2_syubetu1, ")       '���2���1
            .AppendLine("   MSS.kaisyuu2_wariai1, ")        '���2����1
            .AppendLine("   MSS.kaisyuu2_tegata_site_gessuu, ")     '���2��`�T�C�g����
            .AppendLine("   MSS.kaisyuu2_tegata_site_date, ")       '���2��`�T�C�g��
            .AppendLine("   MSS.kaisyuu2_seikyuusyo_yousi, ")       '���2�������p��
            .AppendLine("   MSS.kaisyuu2_syubetu2, ")       '���2���2
            .AppendLine("   MSS.kaisyuu2_wariai2, ")        '���2����2
            .AppendLine("   MSS.kaisyuu2_syubetu3, ")       '���2���3
            .AppendLine("   MSS.kaisyuu2_wariai3, ")        '���2����3
            .AppendLine("   MSS.add_login_user_id, ")       '�o�^���O�C�����[�U�[ID
            .AppendLine("   MSS.add_datetime, ")        '�o�^����
            .AppendLine("   MSS.upd_login_user_id, ")       '�X�V���O�C�����[�U�[ID
            .AppendLine("   MSS.upd_datetime, ")        '�X�V����
            .AppendLine("   MSJ.skk_jigyousyo_mei, ")        '���Ə���
            .AppendLine("   VSSI.seikyuu_saki_mei ")        '�����於

            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
            .AppendLine("   ,MSS.nayose_saki_cd ")        '�����R�[�h
            .AppendLine("   ,MY.nayose_saki_name1 ")        '����於
            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������

            '==================2011/06/16 �ԗ�  �ǉ� �J�n��========================== 
            .AppendLine("   ,MSS.kessanji_nidosime_flg ")   '���Z����x���߃t���O
            '==================2011/06/16 �ԗ�  �ǉ� �I����==========================

            '==================2012/05/17 �ԗ� 407553�̑Ή� �ǉ���========================== 
            .AppendLine("   ,MSS.koufuri_ok_flg ") '���UOK�t���O
            .AppendLine("   ,MSS.tougou_tokuisaki_cd ") '������v���Ӑ溰��
            .AppendLine("   ,MSS.anzen_kaihi_en ") '���S���͉��_�~
            .AppendLine("   ,MSS.anzen_kaihi_wari ") '���S���͉��_����
            .AppendLine("   ,MSS.bikou ") '���l
            '==================2012/05/17 �ԗ� 407553�̑Ή� �ǉ���==========================
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            .AppendLine("   ,MSS.ginkou_siten_cd ") '��s�x�X�R�[�h
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��

            .AppendLine("FROM ")
            .AppendLine("   m_seikyuu_saki MSS WITH (READCOMMITTED) ")      '������}�X�^
            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_sinkaikei_jigyousyo MSJ WITH (READCOMMITTED) ")   '�V��v���Ə��}�X�^
            .AppendLine("ON ")
            .AppendLine("   MSS.skk_jigyousyo_cd = MSJ.skk_jigyousyo_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("   v_seikyuu_saki_info VSSI WITH (READCOMMITTED) ")   '��������
            .AppendLine("ON ")
            .AppendLine("   MSS.seikyuu_saki_cd = VSSI.seikyuu_saki_cd ")
            .AppendLine("AND ")
            .AppendLine("   MSS.seikyuu_saki_brc = VSSI.seikyuu_saki_brc ")
            .AppendLine("AND ")
            .AppendLine("   MSS.seikyuu_saki_kbn = VSSI.seikyuu_saki_kbn ")

            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_yosinkanri MY WITH (READCOMMITTED) ")   '�^�M�Ǘ��}�X�^
            .AppendLine("ON ")
            .AppendLine("   MSS.nayose_saki_cd = MY.nayose_saki_cd ")
            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������

            .AppendLine("WHERE ")
            .AppendLine("   MSS.seikyuu_saki_cd = @seikyuu_saki_cd ")
            .AppendLine("AND ")
            .AppendLine("   MSS.seikyuu_saki_brc = @seikyuu_saki_brc ")
            .AppendLine("AND ")
            .AppendLine("   MSS.seikyuu_saki_kbn = @seikyuu_saki_kbn ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuuSakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuuSakiBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, SeikyuuSakiKbn))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet,dsDataSet.m_seikyuu_saki.TableName, paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' ������}�X�^�o�^
    ''' </summary>
    ''' <param name="dtSeikyuuSaki">�o�^�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsSeikyuuSaki(ByVal dtSeikyuuSaki As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable) As Boolean

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("   m_seikyuu_saki WITH (UPDLOCK) ")
            .AppendLine("( ")
            .AppendLine("   seikyuu_saki_cd, ") '������R�[�h
            .AppendLine("   seikyuu_saki_brc, ")    '������}��
            .AppendLine("   seikyuu_saki_kbn, ")    '������敪
            .AppendLine("   torikesi, ")    '���
            .AppendLine("   skk_jigyousyo_cd, ")    '�V��v���Ə��R�[�h

            .AppendLine("   kyuu_seikyuu_saki_cd, ")    '��������R�[�h

            .AppendLine("   skysy_soufu_jyuusyo1, ")    '���������t��Z��1
            .AppendLine("   skysy_soufu_jyuusyo2, ")    '���������t��Z��2
            .AppendLine("   skysy_soufu_yuubin_no, ")    '���������t��X�֔ԍ�
            .AppendLine("   skysy_soufu_tel_no, ")    '���������t��d�b�ԍ�
            .AppendLine("   skysy_soufu_fax_no, ")    '���������t��FAX�ԍ�

            .AppendLine("   tantousya_mei, ")   '�S���Җ�
            .AppendLine("   seikyuusyo_inji_bukken_mei_flg, ")  '�������󎚕������t���O
            .AppendLine("   nyuukin_kouza_no, ")    '���������ԍ�
            .AppendLine("   seikyuu_sime_date, ")   '�������ߓ�
            .AppendLine("   senpou_seikyuu_sime_date, ")    '����������ߓ�
            .AppendLine("   tyk_koj_seikyuu_timing_flg, ")  '���H�������^�C�~���O�t���O
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
            .AppendLine("   kaisyuu2_wariai3, ")    '���2����3

            .AppendLine("   add_login_user_id, ")   '�o�^���O�C�����[�U�[ID
            .AppendLine("   add_datetime ")    '�o�^����

            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
            .AppendLine("   ,nayose_saki_cd ")    '�����R�[�h
            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������

            '==================2011/06/16 �ԗ�  �ǉ� �J�n��========================== 
            .AppendLine("   ,kessanji_nidosime_flg ")   '���Z����x���߃t���O
            '==================2011/06/16 �ԗ�  �ǉ� �I����==========================

            '==================2012/05/17 �ԗ� 407553�̑Ή� �ǉ���========================== 
            .AppendLine("   ,koufuri_ok_flg ") '���UOK�t���O
            .AppendLine("   ,tougou_tokuisaki_cd ") '������v���Ӑ溰��
            .AppendLine("   ,anzen_kaihi_en ") '���S���͉��_�~
            .AppendLine("   ,anzen_kaihi_wari ") '���S���͉��_����
            .AppendLine("   ,bikou ") '���l
            '==================2012/05/17 �ԗ� 407553�̑Ή� �ǉ���==========================

            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            .AppendLine("   ,ginkou_siten_cd ")           '��s�x�X�R�[�h
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��

            .AppendLine(") ")
            .AppendLine("SELECT ")
            .AppendLine("   @seikyuu_saki_cd, ")    '������R�[�h
            .AppendLine("   @seikyuu_saki_brc, ")   '������}��
            .AppendLine("   @seikyuu_saki_kbn, ")   '������敪
            .AppendLine("   @torikesi, ")   '���
            .AppendLine("   @skk_jigyousyo_cd, ")   '�V��v���Ə��R�[�h

            .AppendLine("   @kyuu_seikyuu_saki_cd, ")   '��������R�[�h

            .AppendLine("   @skysy_soufu_jyuusyo1, ")   '���������t��Z��
            .AppendLine("   @skysy_soufu_jyuusyo2, ")   '���������t��Z��
            .AppendLine("   @skysy_soufu_yuubin_no, ")   '���������t��X�֔ԍ�
            .AppendLine("   @skysy_soufu_tel_no, ")   '���������t��d�b�ԍ�
            .AppendLine("   @skysy_soufu_fax_no, ")   '���������t��FAX�ԍ�

            .AppendLine("   @tantousya_mei, ")  '�S���Җ�
            .AppendLine("   @seikyuusyo_inji_bukken_mei_flg, ") '�������󎚕������t���O
            .AppendLine("   @nyuukin_kouza_no, ")   '���������ԍ�
            .AppendLine("   @seikyuu_sime_date, ")  '�������ߓ�
            .AppendLine("   @senpou_seikyuu_sime_date, ")   '����������ߓ�
            .AppendLine("   @tyk_koj_seikyuu_timing_flg, ") '���H�������^�C�~���O�t���O
            .AppendLine("   @sousai_flg, ") '���E�t���O
            .AppendLine("   @kaisyuu_yotei_gessuu, ")   '����\�茎��
            .AppendLine("   @kaisyuu_yotei_date, ") '����\���
            .AppendLine("   @seikyuusyo_hittyk_date, ") '�������K����
            .AppendLine("   @kaisyuu1_syubetu1, ")  '���1���1
            .AppendLine("   @kaisyuu1_wariai1, ")   '���1����1
            .AppendLine("   @kaisyuu1_tegata_site_gessuu, ")    '���1��`�T�C�g����
            .AppendLine("   @kaisyuu1_tegata_site_date, ")  '���1��`�T�C�g��
            .AppendLine("   @kaisyuu1_seikyuusyo_yousi, ")  '���1�������p��
            .AppendLine("   @kaisyuu1_syubetu2, ")  '���1���2
            .AppendLine("   @kaisyuu1_wariai2, ")   '���1����2
            .AppendLine("   @kaisyuu1_syubetu3, ")  '���1���3
            .AppendLine("   @kaisyuu1_wariai3, ")   '���1����3
            .AppendLine("   @kaisyuu_kyoukaigaku, ")    '������E�z
            .AppendLine("   @kaisyuu2_syubetu1, ")  '���2���1
            .AppendLine("   @kaisyuu2_wariai1, ")   '���2����1
            .AppendLine("   @kaisyuu2_tegata_site_gessuu, ")    '���2��`�T�C�g����
            .AppendLine("   @kaisyuu2_tegata_site_date, ")  '���2��`�T�C�g��
            .AppendLine("   @kaisyuu2_seikyuusyo_yousi, ")  '���2�������p��
            .AppendLine("   @kaisyuu2_syubetu2, ")  '���2���2
            .AppendLine("   @kaisyuu2_wariai2, ")   '���2����2
            .AppendLine("   @kaisyuu2_syubetu3, ")  '���2���3
            .AppendLine("   @kaisyuu2_wariai3, ")   '���2����3
            .AppendLine("   @add_login_user_id, ")  '�o�^���O�C�����[�U�[ID
            .AppendLine("   GETDATE() ")   '�o�^����
            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
            .AppendLine("   ,@nayose_saki_cd ")    '�����R�[�h
            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������

            '==================2011/06/16 �ԗ�  �ǉ� �J�n��========================== 
            .AppendLine("   ,@kessanji_nidosime_flg ")   '���Z����x���߃t���O
            '==================2011/06/16 �ԗ�  �ǉ� �I����==========================

            '==================2012/05/17 �ԗ� 407553�̑Ή� �ǉ���========================== 
            .AppendLine("   ,@koufuri_ok_flg ") '���UOK�t���O
            .AppendLine("   ,@tougou_tokuisaki_cd ") '������v���Ӑ溰��
            .AppendLine("   ,@anzen_kaihi_en ") '���S���͉��_�~
            .AppendLine("   ,@anzen_kaihi_wari ") '���S���͉��_����
            .AppendLine("   ,@bikou ") '���l
            '==================2012/05/17 �ԗ� 407553�̑Ή� �ǉ���==========================
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            .AppendLine("   ,@ginkou_siten_cd ")           '��s�x�X�R�[�h
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
        End With

        '�p�����[�^�̐ݒ�
        With paramList
            '������R�[�h
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_cd)))
            '������}��
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_brc = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_brc)))
            '������敪
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_kbn = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_kbn)))
            '���
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtSeikyuuSaki(0).torikesi))
            '�V��v���Ə��R�[�h
            .Add(MakeParam("@skk_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skk_jigyousyo_cd = "", DBNull.Value, dtSeikyuuSaki(0).skk_jigyousyo_cd)))

            '��������R�[�h
            .Add(MakeParam("@kyuu_seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kyuu_seikyuu_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).kyuu_seikyuu_saki_cd)))

            '���������t��Z��1
            .Add(MakeParam("@skysy_soufu_jyuusyo1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_jyuusyo1 = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_jyuusyo1)))
            '���������t��Z��2
            .Add(MakeParam("@skysy_soufu_jyuusyo2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_jyuusyo2 = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_jyuusyo2)))
            '���������t��X�֔ԍ�
            .Add(MakeParam("@skysy_soufu_yuubin_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_yuubin_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_yuubin_no)))
            '���������t��d�b�ԍ�
            .Add(MakeParam("@skysy_soufu_tel_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_tel_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_tel_no)))
            '���������t��FAX�ԍ�
            .Add(MakeParam("@skysy_soufu_fax_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_fax_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_fax_no)))

            '�S���Җ�
            .Add(MakeParam("@tantousya_mei", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).tantousya_mei = "", DBNull.Value, dtSeikyuuSaki(0).tantousya_mei)))
            '�������󎚕������t���O
            .Add(MakeParam("@seikyuusyo_inji_bukken_mei_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuusyo_inji_bukken_mei_flg = "", DBNull.Value, dtSeikyuuSaki(0).seikyuusyo_inji_bukken_mei_flg)))
            '���������ԍ�
            .Add(MakeParam("@nyuukin_kouza_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).nyuukin_kouza_no = "", DBNull.Value, dtSeikyuuSaki(0).nyuukin_kouza_no)))
            '�������ߓ�
            .Add(MakeParam("@seikyuu_sime_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_sime_date = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_sime_date)))
            '����������ߓ�
            .Add(MakeParam("@senpou_seikyuu_sime_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).senpou_seikyuu_sime_date = "", DBNull.Value, dtSeikyuuSaki(0).senpou_seikyuu_sime_date)))
            '���H�������^�C�~���O�t���O
            .Add(MakeParam("@tyk_koj_seikyuu_timing_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).tyk_koj_seikyuu_timing_flg = "", DBNull.Value, dtSeikyuuSaki(0).tyk_koj_seikyuu_timing_flg)))
            '���E�t���O
            .Add(MakeParam("@sousai_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).sousai_flg = "", DBNull.Value, dtSeikyuuSaki(0).sousai_flg)))
            '����\�茎��
            .Add(MakeParam("@kaisyuu_yotei_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_yotei_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_yotei_gessuu)))
            '����\���
            .Add(MakeParam("@kaisyuu_yotei_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_yotei_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_yotei_date)))
            '�������K����
            .Add(MakeParam("@seikyuusyo_hittyk_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuusyo_hittyk_date = "", DBNull.Value, dtSeikyuuSaki(0).seikyuusyo_hittyk_date)))
            '���1���1
            .Add(MakeParam("@kaisyuu1_syubetu1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu1)))
            '���1����1
            .Add(MakeParam("@kaisyuu1_wariai1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai1)))
            '���1��`�T�C�g����
            .Add(MakeParam("@kaisyuu1_tegata_site_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_tegata_site_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_tegata_site_gessuu)))
            '���1��`�T�C�g��
            .Add(MakeParam("@kaisyuu1_tegata_site_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_tegata_site_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_tegata_site_date)))
            '���1�������p��
            .Add(MakeParam("@kaisyuu1_seikyuusyo_yousi", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_seikyuusyo_yousi = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_seikyuusyo_yousi)))
            '���1���2
            .Add(MakeParam("@kaisyuu1_syubetu2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu2)))
            '���1����2
            .Add(MakeParam("@kaisyuu1_wariai2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai2)))
            '���1���3
            .Add(MakeParam("@kaisyuu1_syubetu3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu3)))
            '���1����3
            .Add(MakeParam("@kaisyuu1_wariai3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai3)))
            '������E�z
            .Add(MakeParam("@kaisyuu_kyoukaigaku", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_kyoukaigaku = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_kyoukaigaku)))
            '���2���1
            .Add(MakeParam("@kaisyuu2_syubetu1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu1)))
            '���2����1
            .Add(MakeParam("@kaisyuu2_wariai1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai1)))
            '���2��`�T�C�g����
            .Add(MakeParam("@kaisyuu2_tegata_site_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_tegata_site_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_tegata_site_gessuu)))
            '���2��`�T�C�g��
            .Add(MakeParam("@kaisyuu2_tegata_site_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_tegata_site_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_tegata_site_date)))
            '���2�������p��
            .Add(MakeParam("@kaisyuu2_seikyuusyo_yousi", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_seikyuusyo_yousi = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_seikyuusyo_yousi)))
            '���2���2
            .Add(MakeParam("@kaisyuu2_syubetu2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu2)))
            '���2����2
            .Add(MakeParam("@kaisyuu2_wariai2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai2)))
            '���2���3
            .Add(MakeParam("@kaisyuu2_syubetu3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu3)))
            '���2����3
            .Add(MakeParam("@kaisyuu2_wariai3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai3)))
            '�o�^���O�C�����[�U�[ID
            .Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).add_login_user_id = "", DBNull.Value, dtSeikyuuSaki(0).add_login_user_id)))

            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
            .Add(MakeParam("@nayose_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).nayose_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).nayose_saki_cd)))
            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������

            '==================2011/06/16 �ԗ�  �ǉ� �J�n��========================== 
            .Add(MakeParam("@kessanji_nidosime_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kessanji_nidosime_flg = "", DBNull.Value, dtSeikyuuSaki(0).kessanji_nidosime_flg)))    '���Z����x���߃t���O
            '==================2011/06/16 �ԗ�  �ǉ� �I����==========================

            '==================2012/05/17 �ԗ� 407553�̑Ή� �ǉ���========================== 
            '���UOK�t���O
            .Add(MakeParam("@koufuri_ok_flg", SqlDbType.Int, 10, IIf(dtSeikyuuSaki(0).koufuri_ok_flg = "", DBNull.Value, dtSeikyuuSaki(0).koufuri_ok_flg)))
            '������v���Ӑ溰��
            .Add(MakeParam("@tougou_tokuisaki_cd", SqlDbType.VarChar, 10, IIf(dtSeikyuuSaki(0).tougou_tokuisaki_cd = "", DBNull.Value, dtSeikyuuSaki(0).tougou_tokuisaki_cd)))
            '���S���͉��_�~
            .Add(MakeParam("@anzen_kaihi_en", SqlDbType.Int, 10, IIf(dtSeikyuuSaki(0).anzen_kaihi_en = "", DBNull.Value, dtSeikyuuSaki(0).anzen_kaihi_en)))
            '���S���͉��_����
            .Add(MakeParam("@anzen_kaihi_wari", SqlDbType.Decimal, 10, IIf(dtSeikyuuSaki(0).anzen_kaihi_wari = "", DBNull.Value, dtSeikyuuSaki(0).anzen_kaihi_wari)))
            '���l
            .Add(MakeParam("@bikou", SqlDbType.VarChar, 40, IIf(dtSeikyuuSaki(0).bikou = "", DBNull.Value, dtSeikyuuSaki(0).bikou)))
            '==================2012/05/17 �ԗ� 407553�̑Ή� �ǉ���==========================
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            .Add(MakeParam("@ginkou_siten_cd", SqlDbType.VarChar, 3, IIf(dtSeikyuuSaki(0).ginkou_siten_cd = "", DBNull.Value, dtSeikyuuSaki(0).ginkou_siten_cd))) '��s�x�X�R�[�h
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��

        End With

        ' �N�G�����s
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' �r���`�F�b�N�p
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">������R�[�h</param>
    ''' <param name="strSeikyuuSakiBrc">������</param>
    ''' <param name="strKousinDate">�X�V����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelHaita(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal SeikyuuSakiKbn As String, ByVal strKousinDate As String) As DataTable

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
            .AppendLine("  m_seikyuu_saki WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("  seikyuu_saki_cd = @seikyuu_saki_cd  ")
            .AppendLine("AND ")
            .AppendLine("  seikyuu_saki_brc = @seikyuu_saki_brc  ")
            .AppendLine("AND ")
            .AppendLine("  seikyuu_saki_kbn = @seikyuu_saki_kbn  ")
            .AppendLine("AND ")
            .AppendLine("   CONVERT(varchar(19),CONVERT(datetime,upd_datetime,21),21)<>CONVERT(varchar(19),CONVERT(datetime,@upd_datetime,21),21) ")
            .AppendLine("AND ")
            .AppendLine("   upd_datetime IS NOT NULL  ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuuSakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuuSakiBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, SeikyuuSakiKbn))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousinDate))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    ''' <summary>
    ''' ������}�X�^�e�[�u���̏C��
    ''' </summary>
    ''' <param name="dtSeikyuuSaki">�C���̃f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdSeikyuuSaki(ByVal dtSeikyuuSaki As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable) As Integer

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("   m_seikyuu_saki WITH (UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("   torikesi = @torikesi, ") '���
            .AppendLine("   skk_jigyousyo_cd = @skk_jigyousyo_cd, ") '�V��v���Ə��R�[�h

            '==================2011/06/16 �ԗ�  �폜 �J�n��==========================
            '.AppendLine("   kyuu_seikyuu_saki_cd = @kyuu_seikyuu_saki_cd, ") '��������R�[�h
            '==================2011/06/16 �ԗ�  �폜 �I����==========================

            .AppendLine("   skysy_soufu_jyuusyo1 = @skysy_soufu_jyuusyo1, ")    '���������t��Z��1
            .AppendLine("   skysy_soufu_jyuusyo2 = @skysy_soufu_jyuusyo2, ")    '���������t��Z��2
            .AppendLine("   skysy_soufu_yuubin_no = @skysy_soufu_yuubin_no, ")    '���������t��X�֔ԍ�
            .AppendLine("   skysy_soufu_tel_no = @skysy_soufu_tel_no, ")    '���������t��d�b�ԍ�
            .AppendLine("   skysy_soufu_fax_no = @skysy_soufu_fax_no, ")    '���������t��FAX�ԍ�

            .AppendLine("   tantousya_mei = @tantousya_mei, ") '�S���Җ�
            .AppendLine("   seikyuusyo_inji_bukken_mei_flg = @seikyuusyo_inji_bukken_mei_flg, ") '�������󎚕������t���O
            .AppendLine("   nyuukin_kouza_no = @nyuukin_kouza_no, ") '���������ԍ�
            .AppendLine("   seikyuu_sime_date = @seikyuu_sime_date, ") '�������ߓ�
            .AppendLine("   senpou_seikyuu_sime_date = @senpou_seikyuu_sime_date, ") '����������ߓ�
            .AppendLine("   tyk_koj_seikyuu_timing_flg = @tyk_koj_seikyuu_timing_flg, ") '���H�������^�C�~���O�t���O
            .AppendLine("   sousai_flg = @sousai_flg, ") '���E�t���O
            .AppendLine("   kaisyuu_yotei_gessuu = @kaisyuu_yotei_gessuu, ") '����\�茎��
            .AppendLine("   kaisyuu_yotei_date = @kaisyuu_yotei_date, ") '����\���
            .AppendLine("   seikyuusyo_hittyk_date = @seikyuusyo_hittyk_date, ") '�������K����
            .AppendLine("   kaisyuu1_syubetu1 = @kaisyuu1_syubetu1, ") '���1���1
            .AppendLine("   kaisyuu1_wariai1 = @kaisyuu1_wariai1, ") '���1����1
            .AppendLine("   kaisyuu1_tegata_site_gessuu = @kaisyuu1_tegata_site_gessuu, ") '���1��`�T�C�g����
            .AppendLine("   kaisyuu1_tegata_site_date = @kaisyuu1_tegata_site_date, ") '���1��`�T�C�g��
            .AppendLine("   kaisyuu1_seikyuusyo_yousi = @kaisyuu1_seikyuusyo_yousi, ") '���1�������p��
            .AppendLine("   kaisyuu1_syubetu2 = @kaisyuu1_syubetu2, ") '���1���2
            .AppendLine("   kaisyuu1_wariai2 = @kaisyuu1_wariai2, ") '���1����2
            .AppendLine("   kaisyuu1_syubetu3 = @kaisyuu1_syubetu3, ") '���1���3
            .AppendLine("   kaisyuu1_wariai3 = @kaisyuu1_wariai3, ") '���1����3
            .AppendLine("   kaisyuu_kyoukaigaku = @kaisyuu_kyoukaigaku, ") '������E�z
            .AppendLine("   kaisyuu2_syubetu1 = @kaisyuu2_syubetu1, ") '���2���1
            .AppendLine("   kaisyuu2_wariai1 = @kaisyuu2_wariai1, ") '���2����1
            .AppendLine("   kaisyuu2_tegata_site_gessuu = @kaisyuu2_tegata_site_gessuu, ") '���2��`�T�C�g����
            .AppendLine("   kaisyuu2_tegata_site_date = @kaisyuu2_tegata_site_date, ") '���2��`�T�C�g��
            .AppendLine("   kaisyuu2_seikyuusyo_yousi = @kaisyuu2_seikyuusyo_yousi, ") '���2�������p��
            .AppendLine("   kaisyuu2_syubetu2 = @kaisyuu2_syubetu2, ") '���2���2
            .AppendLine("   kaisyuu2_wariai2 = @kaisyuu2_wariai2, ") '���2����2
            .AppendLine("   kaisyuu2_syubetu3 = @kaisyuu2_syubetu3, ") '���2���3
            .AppendLine("   kaisyuu2_wariai3 = @kaisyuu2_wariai3, ") '���2����3
            .AppendLine("   upd_login_user_id = @upd_login_user_id, ") '�X�V���O�C�����[�U�[ID
            .AppendLine("   upd_datetime = GETDATE() ") '�X�V����
            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
            .AppendLine("   ,nayose_saki_cd = @nayose_saki_cd ")    '�����R�[�h
            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������

            '==================2011/06/16 �ԗ�  �ǉ� �J�n��========================== 
            .AppendLine("   ,kessanji_nidosime_flg = @kessanji_nidosime_flg ")   '���Z����x���߃t���O
            '==================2011/06/16 �ԗ�  �ǉ� �I����==========================

            '==================2012/05/17 �ԗ� 407553�̑Ή� �ǉ���========================== 
            .AppendLine("   ,koufuri_ok_flg = @koufuri_ok_flg ") '���UOK�t���O
            .AppendLine("   ,tougou_tokuisaki_cd = @tougou_tokuisaki_cd ") '������v���Ӑ溰��
            .AppendLine("   ,anzen_kaihi_en = @anzen_kaihi_en ") '���S���͉��_�~
            .AppendLine("   ,anzen_kaihi_wari = @anzen_kaihi_wari ") '���S���͉��_����
            .AppendLine("   ,bikou = @bikou ") '���l
            '==================2012/05/17 �ԗ� 407553�̑Ή� �ǉ���==========================
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            .AppendLine("   ,ginkou_siten_cd = @ginkou_siten_cd ") '��s�x�X�R�[�h
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            .AppendLine("WHERE ")
            .AppendLine("  seikyuu_saki_cd = @seikyuu_saki_cd  ")
            .AppendLine("AND ")
            .AppendLine("  seikyuu_saki_brc = @seikyuu_saki_brc  ")
            .AppendLine("AND ")
            .AppendLine("  seikyuu_saki_kbn = @seikyuu_saki_kbn  ")
        End With

        '�p�����[�^�̐ݒ�
        With paramList
            '������R�[�h
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_cd)))
            '������}��
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_brc = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_brc)))
            '������敪
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_kbn = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_kbn)))
            '���
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtSeikyuuSaki(0).torikesi))
            '�V��v���Ə��R�[�h
            .Add(MakeParam("@skk_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skk_jigyousyo_cd = "", DBNull.Value, dtSeikyuuSaki(0).skk_jigyousyo_cd)))

            '==================2011/06/16 �ԗ�  �폜 �J�n��==========================
            ''��������R�[�h
            '.Add(MakeParam("@kyuu_seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kyuu_seikyuu_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).kyuu_seikyuu_saki_cd)))
            '==================2011/06/16 �ԗ�  �폜 �I����==========================

            '���������t��Z��1
            .Add(MakeParam("@skysy_soufu_jyuusyo1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_jyuusyo1 = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_jyuusyo1)))
            '���������t��Z��2
            .Add(MakeParam("@skysy_soufu_jyuusyo2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_jyuusyo2 = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_jyuusyo2)))
            '���������t��X�֔ԍ�
            .Add(MakeParam("@skysy_soufu_yuubin_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_yuubin_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_yuubin_no)))
            '���������t��d�b�ԍ�
            .Add(MakeParam("@skysy_soufu_tel_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_tel_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_tel_no)))
            '���������t��FAX�ԍ�
            .Add(MakeParam("@skysy_soufu_fax_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_fax_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_fax_no)))

            '�S���Җ�
            .Add(MakeParam("@tantousya_mei", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).tantousya_mei = "", DBNull.Value, dtSeikyuuSaki(0).tantousya_mei)))
            '�������󎚕������t���O
            .Add(MakeParam("@seikyuusyo_inji_bukken_mei_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuusyo_inji_bukken_mei_flg = "", DBNull.Value, dtSeikyuuSaki(0).seikyuusyo_inji_bukken_mei_flg)))
            '���������ԍ�
            .Add(MakeParam("@nyuukin_kouza_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).nyuukin_kouza_no = "", DBNull.Value, dtSeikyuuSaki(0).nyuukin_kouza_no)))
            '�������ߓ�
            .Add(MakeParam("@seikyuu_sime_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_sime_date = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_sime_date)))
            '����������ߓ�
            .Add(MakeParam("@senpou_seikyuu_sime_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).senpou_seikyuu_sime_date = "", DBNull.Value, dtSeikyuuSaki(0).senpou_seikyuu_sime_date)))
            '���H�������^�C�~���O�t���O
            .Add(MakeParam("@tyk_koj_seikyuu_timing_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).tyk_koj_seikyuu_timing_flg = "", DBNull.Value, dtSeikyuuSaki(0).tyk_koj_seikyuu_timing_flg)))
            '���E�t���O
            .Add(MakeParam("@sousai_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).sousai_flg = "", DBNull.Value, dtSeikyuuSaki(0).sousai_flg)))
            '����\�茎��
            .Add(MakeParam("@kaisyuu_yotei_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_yotei_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_yotei_gessuu)))
            '����\���
            .Add(MakeParam("@kaisyuu_yotei_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_yotei_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_yotei_date)))
            '�������K����
            .Add(MakeParam("@seikyuusyo_hittyk_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuusyo_hittyk_date = "", DBNull.Value, dtSeikyuuSaki(0).seikyuusyo_hittyk_date)))
            '���1���1
            .Add(MakeParam("@kaisyuu1_syubetu1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu1)))
            '���1����1
            .Add(MakeParam("@kaisyuu1_wariai1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai1)))
            '���1��`�T�C�g����
            .Add(MakeParam("@kaisyuu1_tegata_site_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_tegata_site_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_tegata_site_gessuu)))
            '���1��`�T�C�g��
            .Add(MakeParam("@kaisyuu1_tegata_site_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_tegata_site_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_tegata_site_date)))
            '���1�������p��
            .Add(MakeParam("@kaisyuu1_seikyuusyo_yousi", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_seikyuusyo_yousi = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_seikyuusyo_yousi)))
            '���1���2
            .Add(MakeParam("@kaisyuu1_syubetu2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu2)))
            '���1����2
            .Add(MakeParam("@kaisyuu1_wariai2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai2)))
            '���1���3
            .Add(MakeParam("@kaisyuu1_syubetu3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu3)))
            '���1����3
            .Add(MakeParam("@kaisyuu1_wariai3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai3)))
            '������E�z
            .Add(MakeParam("@kaisyuu_kyoukaigaku", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_kyoukaigaku = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_kyoukaigaku)))
            '���2���1
            .Add(MakeParam("@kaisyuu2_syubetu1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu1)))
            '���2����1
            .Add(MakeParam("@kaisyuu2_wariai1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai1)))
            '���2��`�T�C�g����
            .Add(MakeParam("@kaisyuu2_tegata_site_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_tegata_site_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_tegata_site_gessuu)))
            '���2��`�T�C�g��
            .Add(MakeParam("@kaisyuu2_tegata_site_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_tegata_site_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_tegata_site_date)))
            '���2�������p��
            .Add(MakeParam("@kaisyuu2_seikyuusyo_yousi", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_seikyuusyo_yousi = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_seikyuusyo_yousi)))
            '���2���2
            .Add(MakeParam("@kaisyuu2_syubetu2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu2)))
            '���2����2
            .Add(MakeParam("@kaisyuu2_wariai2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai2)))
            '���2���3
            .Add(MakeParam("@kaisyuu2_syubetu3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu3)))
            '���2����3
            .Add(MakeParam("@kaisyuu2_wariai3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai3)))
            '�X�V���O�C�����[�U�[ID
            .Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).upd_login_user_id = "", DBNull.Value, dtSeikyuuSaki(0).upd_login_user_id)))

            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
            .Add(MakeParam("@nayose_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).nayose_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).nayose_saki_cd)))
            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������

            '==================2011/06/16 �ԗ�  �ǉ� �J�n��========================== 
            .Add(MakeParam("@kessanji_nidosime_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kessanji_nidosime_flg = "", DBNull.Value, dtSeikyuuSaki(0).kessanji_nidosime_flg)))    '���Z����x���߃t���O
            '==================2011/06/16 �ԗ�  �ǉ� �I����==========================

            '==================2012/05/17 �ԗ� 407553�̑Ή� �ǉ���========================== 
            '���UOK�t���O
            .Add(MakeParam("@koufuri_ok_flg", SqlDbType.Int, 10, IIf(dtSeikyuuSaki(0).koufuri_ok_flg = "", DBNull.Value, dtSeikyuuSaki(0).koufuri_ok_flg)))
            '������v���Ӑ溰��
            .Add(MakeParam("@tougou_tokuisaki_cd", SqlDbType.VarChar, 10, IIf(dtSeikyuuSaki(0).tougou_tokuisaki_cd = "", DBNull.Value, dtSeikyuuSaki(0).tougou_tokuisaki_cd)))
            '���S���͉��_�~
            .Add(MakeParam("@anzen_kaihi_en", SqlDbType.Int, 10, IIf(dtSeikyuuSaki(0).anzen_kaihi_en = "", DBNull.Value, dtSeikyuuSaki(0).anzen_kaihi_en)))
            '���S���͉��_����
            .Add(MakeParam("@anzen_kaihi_wari", SqlDbType.Decimal, 10, IIf(dtSeikyuuSaki(0).anzen_kaihi_wari = "", DBNull.Value, dtSeikyuuSaki(0).anzen_kaihi_wari)))
            '���l
            .Add(MakeParam("@bikou", SqlDbType.VarChar, 40, IIf(dtSeikyuuSaki(0).bikou = "", DBNull.Value, dtSeikyuuSaki(0).bikou)))
            '==================2012/05/17 �ԗ� 407553�̑Ή� �ǉ���==========================
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            .Add(MakeParam("@ginkou_siten_cd", SqlDbType.VarChar, 3, IIf(dtSeikyuuSaki(0).ginkou_siten_cd = "", DBNull.Value, dtSeikyuuSaki(0).ginkou_siten_cd))) '��s�x�X�R�[�h
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
        End With

        ' �N�G�����s
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' ������o�^���`�}�X�^���̎擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiHinagataInfo(ByVal strSeikyuuSakiBrc As String) As SeikyuuSakiHinagataDataSet.m_seikyuu_saki_touroku_hinagataDataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New SeikyuuSakiHinagataDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   skk_jigyousyo_cd, ") '�V��v���Ə��R�[�h
            .AppendLine("   tantousya_mei, ") '�S���Җ�
            .AppendLine("   seikyuusyo_inji_bukken_mei_flg, ") '�������󎚕������t���O
            .AppendLine("   nyuukin_kouza_no, ") '���������ԍ�
            .AppendLine("   seikyuu_sime_date, ") '�������ߓ�
            .AppendLine("   senpou_seikyuu_sime_date, ") '����������ߓ�
            .AppendLine("   sousai_flg, ") '���E�t���O

            .AppendLine("   tyk_koj_seikyuu_timing_flg, ")  '���H�������^�C�~���O�t���O

            .AppendLine("   kaisyuu_yotei_gessuu, ") '����\�茎��
            .AppendLine("   kaisyuu_yotei_date, ") '����\���
            .AppendLine("   seikyuusyo_hittyk_date, ") '�������K����
            .AppendLine("   kaisyuu1_syubetu1, ") '���1���1
            .AppendLine("   kaisyuu1_wariai1, ") '���1����1
            .AppendLine("   kaisyuu1_tegata_site_gessuu, ") '���1��`�T�C�g����
            .AppendLine("   kaisyuu1_tegata_site_date, ") '���1��`�T�C�g��
            .AppendLine("   kaisyuu1_seikyuusyo_yousi, ") '���1�������p��
            .AppendLine("   kaisyuu1_syubetu2, ") '���1���2
            .AppendLine("   kaisyuu1_wariai2, ") '���1����2
            .AppendLine("   kaisyuu1_syubetu3, ") '���1���3
            .AppendLine("   kaisyuu1_wariai3, ") '���1����3
            .AppendLine("   kaisyuu_kyoukaigaku, ") '������E�z
            .AppendLine("   kaisyuu2_syubetu1, ") '���2���1
            .AppendLine("   kaisyuu2_wariai1, ") '���2����1
            .AppendLine("   kaisyuu2_tegata_site_gessuu, ") '���2��`�T�C�g����
            .AppendLine("   kaisyuu2_tegata_site_date, ") '���2��`�T�C�g��
            .AppendLine("   kaisyuu2_seikyuusyo_yousi, ") '���2�������p��
            .AppendLine("   kaisyuu2_syubetu2, ") '���2���2
            .AppendLine("   kaisyuu2_wariai2, ") '���2����2
            .AppendLine("   kaisyuu2_syubetu3, ") '���2���3
            .AppendLine("   kaisyuu2_wariai3 ") '���2����3
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            .AppendLine("   ,ginkou_siten_cd ") '��s�x�X�R�[�h
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            .AppendLine("FROM ")
            .AppendLine("   m_seikyuu_saki_touroku_hinagata WITH (READCOMMITTED) ")      '������o�^���`�}�X�^
            .AppendLine("WHERE ")
            .AppendLine("   seikyuu_saki_brc = @seikyuu_saki_brc ")

        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuuSakiBrc))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.m_seikyuu_saki_touroku_hinagata.TableName, paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �V��v���Ə��}�X�^
    ''' </summary>
    Public Function SelSinkaikeiJigyousyoInfo(ByVal strSkkJigyousyoCd As String) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   skk_jigyousyo_cd, ")
            .AppendLine("   skk_jigyousyo_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_sinkaikei_jigyousyo WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   skk_jigyousyo_cd = @skk_jigyousyo_cd ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@skk_jigyousyo_cd", SqlDbType.VarChar, 10, strSkkJigyousyoCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �����R�[�h�i�^�M�Ǘ��}�X�^�j
    ''' </summary>
    ''' <history>20100925�@�����R�[�h�A����於�@�ǉ��@�n���R</history>
    Public Function SelNayoseSakiInfo(ByVal strNayoseSakiCd As String) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   nayose_saki_cd, ")
            .AppendLine("   nayose_saki_name1 ")
            .AppendLine("FROM ")
            .AppendLine("   m_yosinkanri WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   nayose_saki_cd = @nayose_saki_cd ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@nayose_saki_cd", SqlDbType.VarChar, 5, strNayoseSakiCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' ������}�X�^�r���[
    ''' </summary>
    Public Function SelVSeikyuuSakiInfo(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String, ByVal blnDelete As Boolean) As CommonSearchDataSet.SeikyuuSakiTableDataTable

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
            .AppendLine("   seikyuu_saki_mei ")
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
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.SeikyuuSakiTable.TableName, paramList.ToArray)

        '�߂�
        Return dsDataSet.SeikyuuSakiTable

    End Function

    ''' <summary>
    ''' ���̑��}�X�^���݃`�F�b�N
    ''' </summary>
    Public Function SelSonzaiChk(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            Select Case strKbn
                Case "0"
                    .AppendLine("SELECT ")
                    .AppendLine("   kameiten_cd ")
                    .AppendLine("FROM ")
                    .AppendLine("   m_kameiten WITH (READCOMMITTED)  ")
                    .AppendLine("WHERE ")
                    .AppendLine("   kameiten_cd = @kameiten_cd ")
                    '�p�����[�^�̐ݒ�
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strCd))
                Case "1"
                    .AppendLine("SELECT ")
                    .AppendLine("   tys_kaisya_cd ")
                    .AppendLine("FROM ")
                    .AppendLine("   m_tyousakaisya WITH (READCOMMITTED)  ")
                    .AppendLine("WHERE ")
                    .AppendLine("   tys_kaisya_cd = @tys_kaisya_cd ")
                    .AppendLine("AND ")
                    .AppendLine("   jigyousyo_cd = @jigyousyo_cd ")
                    '�p�����[�^�̐ݒ�
                    paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strCd))
                    paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strBrc))
                Case "2"
                    .AppendLine("SELECT ")
                    .AppendLine("   eigyousyo_cd ")
                    .AppendLine("FROM ")
                    .AppendLine("   m_eigyousyo WITH (READCOMMITTED)  ")
                    .AppendLine("WHERE ")
                    .AppendLine("   eigyousyo_cd = @eigyousyo_cd ")
                    '�p�����[�^�̐ݒ�
                    paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strCd))
            End Select
        End With

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

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
    ''' ���U�n�j�t���O���擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 �ԗ� 407553�̑Ή� �ǉ�</history>
    Public Function SelKutiburiOkFlg() As Data.DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	code AS code ") '--�R�[�h ")
            .AppendLine("	,ISNULL(meisyou,'') AS meisyou ") '--���� ")
            .AppendLine("FROM ")
            .AppendLine("	m_kakutyou_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	meisyou_syubetu = @meisyou_syubetu ") '--���̎�� ")
            .AppendLine("ORDER BY ")
            .AppendLine("	hyouji_jyun ASC ") '--�\���� ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, "47"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsKutiburiOkFlg", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables("dsKutiburiOkFlg")

    End Function

    ''' <summary>
    ''' ��s�x�X�R�[�h���擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/05/29 �k�o �������̋�s�x�X�R�[�h�����ɔ���Earth���C</history>
    Public Function SelGinkouSitenCd() As Data.DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	code AS code ") '--�R�[�h ")
            .AppendLine("	,code + ':' + ISNULL(meisyou,'') AS meisyou ") '--���� ")
            .AppendLine("FROM ")
            .AppendLine("	m_kakutyou_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	meisyou_syubetu = @meisyou_syubetu ") '--���̎�� ")
            .AppendLine("ORDER BY ")
            .AppendLine("	hyouji_jyun ASC ") '--�\���� ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, "48"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsGinkouSitenCd", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables("dsGinkouSitenCd")

    End Function

    ''' <summary>
    ''' Max���U�n�j�t���O���擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 �ԗ� 407553�̑Ή� �ǉ�</history>
    Public Function SelMaxKutiburiOkFlg() As Data.DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	MAX(CONVERT(INT,tougou_tokuisaki_cd)) AS tougou_tokuisaki_cd_max ") '--������v���Ӑ溰�� ")
            .AppendLine("FROM ")
            .AppendLine("	m_seikyuu_saki ") '--������}�X�^ ")
        End With

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsMaxKutiburiOkFlg")

        '�߂�
        Return dsDataSet.Tables("dsMaxKutiburiOkFlg")

    End Function

End Class
