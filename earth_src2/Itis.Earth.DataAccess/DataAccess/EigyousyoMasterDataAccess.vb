Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
''' <summary>
''' ������Ѓ}�X�^
''' </summary>
''' <history>
''' <para>2010/05/15�@�n���R(��A)�@�V�K�쐬</para>
''' </history>
Public Class EigyousyoMasterDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

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
    ''' �c�Ə��}�X�^
    ''' </summary>
    ''' <param name="strEigyousyo_Cd">�c�Ə��R�[�h</param>
    ''' <param name="strEigyousyoCd">�c�Ə��R�[�h</param>
    Public Function SelEigyousyoInfo(ByVal strEigyousyo_Cd As String, _
                                         ByVal strEigyousyoCd As String, _
                                         ByVal btn As String) As EigyousyoDataSet.m_eigyousyoDataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New EigyousyoDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   ME.eigyousyo_cd, ") '�c�Ə��R�[�h
            .AppendLine("   ME.torikesi, ") '���
            .AppendLine("   ME.eigyousyo_mei, ") '�c�Ə���
            .AppendLine("   ME.eigyousyo_kana, ") '�c�Ə��J�i
            .AppendLine("   ME.eigyousyo_mei_inji_umu, ") '�c�Ə����󎚗L��
            .AppendLine("   ME.seikyuu_saki_cd, ") '������R�[�h
            .AppendLine("   ME.seikyuu_saki_brc, ") '������}��
            .AppendLine("   ME.seikyuu_saki_kbn, ") '������敪
            .AppendLine("   ME.yuubin_no, ") '�X�֔ԍ�
            .AppendLine("   ME.jyuusyo1, ") '�Z��1
            .AppendLine("   ME.jyuusyo2, ") '�Z��2
            .AppendLine("   ME.tel_no, ") '�d�b�ԍ�
            .AppendLine("   ME.fax_no, ") 'FAX�ԍ�
            .AppendLine("   ME.busyo_mei, ") '������
            .AppendLine("   ME.seikyuu_saki_mei, ") '�����於
            .AppendLine("   ME.seikyuu_saki_kana, ") '������J�i
            .AppendLine("   ME.skysy_soufu_jyuusyo1, ") '���������t��Z��1
            .AppendLine("   ME.skysy_soufu_jyuusyo2, ") '���������t��Z��2
            .AppendLine("   ME.skysy_soufu_yuubin_no, ") '���������t��X�֔ԍ�
            .AppendLine("   ME.skysy_soufu_tel_no, ") '���������t��d�b�ԍ�
            .AppendLine("   ME.skysy_soufu_fax_no, ") '���������t��FAX�ԍ�
            .AppendLine("   ME.add_login_user_id, ") '�o�^���O�C�����[�U�[ID
            .AppendLine("   ME.add_datetime, ") '�o�^����
            .AppendLine("   ME.upd_login_user_id, ") '�X�V���O�C�����[�U�[ID
            .AppendLine("   ME.upd_datetime, ") '�X�V����
            '=========2012/04/10 �ԗ� 405738 �ǉ���================================
            .AppendLine("   ME.syuukei_fc_ten_cd, ") '�W�vFC�p����
            .AppendLine("   ME.eria_cd, ") '�G���A�R�[�h
            .AppendLine("   ME.block_cd, ") '�u���b�N�R�[�h
            .AppendLine("   ME.fc_ten_kbn, ") 'FC�X�敪
            .AppendLine("	CASE ")
            .AppendLine("	    WHEN ME.fc_nyuukai_date IS NOT NULL THEN ")
            .AppendLine("	        LEFT(CONVERT(VARCHAR(10),ME.fc_nyuukai_date,111),7) ") '--FC����N�� ")
            .AppendLine("	    ELSE ")
            .AppendLine("	        '' ")
            .AppendLine("	    END AS fc_nyuukai_date, ")
            .AppendLine("	CASE ")
            .AppendLine("	    WHEN ME.fc_taikai_date IS NOT NULL THEN ")
            .AppendLine("	        LEFT(CONVERT(VARCHAR(10),ME.fc_taikai_date,111),7) ") '--FC�މ�N�� ")
            .AppendLine("	    ELSE ")
            .AppendLine("	        '' ")
            .AppendLine("	    END AS fc_taikai_date, ")
            .AppendLine("   ME.fc_tys_kaisya_cd, ") '(FC)������ЃR�[�h
            .AppendLine("   ME.fc_jigyousyo_cd, ") '(FC)���Ə��R�[�h
            '=========2012/04/10 �ԗ� 405738 �ǉ���================================
            .AppendLine("   VSSI.seikyuu_saki_mei AS seikyuu_saki_mei1 ")        '�����於
            .AppendLine("FROM ")
            .AppendLine("   m_eigyousyo ME WITH (READCOMMITTED) ")      '�c�Ə��}�X�^
            .AppendLine("LEFT JOIN ")
            .AppendLine("   v_seikyuu_saki_info VSSI WITH (READCOMMITTED) ")       '��������VIEW
            .AppendLine("ON ")
            .AppendLine("  ME.seikyuu_saki_cd = VSSI.seikyuu_saki_cd ")
            .AppendLine("AND ")
            .AppendLine("  ME.seikyuu_saki_brc = VSSI.seikyuu_saki_brc ")
            .AppendLine("AND ")
            .AppendLine("  ME.seikyuu_saki_kbn = VSSI.seikyuu_saki_kbn ")
            .AppendLine("WHERE ")
            .AppendLine("   ME.eigyousyo_cd IS NOT NULL ")
            If btn = "btnSearch" Then
                .AppendLine("AND ")
                .AppendLine("   ME.eigyousyo_cd = @eigyousyo_cd ")
            Else
                .AppendLine("AND ")
                .AppendLine("   ME.eigyousyo_cd = @eigyousyocd ")
            End If
        End With

        '�p�����[�^�̐ݒ�
        If btn = "btnSearch" Then
            paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strEigyousyo_Cd))
        Else
            paramList.Add(MakeParam("@eigyousyocd", SqlDbType.VarChar, 5, strEigyousyoCd))
        End If

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    dsDataSet.m_eigyousyo.TableName, paramList.ToArray)

        '�߂�
        Return dsDataSet.m_eigyousyo
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
    ''' �r���`�F�b�N�p
    ''' </summary>
    ''' <param name="strEigyousyoCd">�c�Ə��R�[�h</param>
    ''' <param name="strKousinDate">�X�V����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelHaita(ByVal strEigyousyoCd As String, ByVal strKousinDate As String) As DataTable

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
            .AppendLine("  m_eigyousyo WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("  eigyousyo_cd = @eigyousyo_cd  ")
            .AppendLine("AND ")
            .AppendLine("   CONVERT(varchar(19),CONVERT(datetime,upd_datetime,21),21)<>CONVERT(varchar(19),CONVERT(datetime,@upd_datetime,21),21) ")
            .AppendLine("AND ")
            .AppendLine("   upd_datetime IS NOT NULL  ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strEigyousyoCd))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousinDate))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
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
            If strTrue = True Then
                .AppendLine("AND ")
                .AppendLine("   torikesi = @torikesi ")
            End If
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, strSeikyuuSakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, strSeikyuuSakiBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, strSeikyuuSakiKbn))
        If strTrue = True Then
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, "0"))
        End If

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
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, "2"))
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

    ''' <summary>
    ''' �c�Ə��}�X�^�e�[�u���̏C��
    ''' </summary>
    ''' <param name="dtEigyousyo">�C���̃f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdEigyousyo(ByVal dtEigyousyo As EigyousyoDataSet.m_eigyousyoDataTable) As Integer

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("   m_eigyousyo WITH (UPDLOCK) ")
            .AppendLine("SET ")

            .AppendLine("   torikesi = @torikesi, ")    '���
            .AppendLine("   eigyousyo_mei = @eigyousyo_mei, ")    '�c�Ə���
            .AppendLine("   eigyousyo_kana = @eigyousyo_kana, ")    '�c�Ə��J�i
            .AppendLine("   eigyousyo_mei_inji_umu = @eigyousyo_mei_inji_umu, ")    '�c�Ə����󎚗L��
            '.AppendLine("   pca_seikyuu_cd = @pca_seikyuu_cd, ")    'PCA������R�[�h
            .AppendLine("   seikyuu_saki_cd = @seikyuu_saki_cd, ")    '������R�[�h
            .AppendLine("   seikyuu_saki_brc = @seikyuu_saki_brc, ")    '������}��
            .AppendLine("   seikyuu_saki_kbn = @seikyuu_saki_kbn, ")    '������敪
            '.AppendLine("   hansokuhin_sime_date = @hansokuhin_sime_date, ")    '�̑��i���ߓ�
            .AppendLine("   yuubin_no = @yuubin_no, ")    '�X�֔ԍ�
            .AppendLine("   jyuusyo1 = @jyuusyo1, ")    '�Z��1
            .AppendLine("   jyuusyo2 = @jyuusyo2, ")    '�Z��2
            .AppendLine("   tel_no = @tel_no, ")    '�d�b�ԍ�
            .AppendLine("   fax_no = @fax_no, ")    'FAX�ԍ�
            .AppendLine("   busyo_mei = @busyo_mei, ")    '������
            .AppendLine("   seikyuu_saki_mei = @seikyuu_saki_mei, ")    '�����於
            .AppendLine("   seikyuu_saki_kana = @seikyuu_saki_kana, ")    '������J�i
            .AppendLine("   skysy_soufu_jyuusyo1 = @skysy_soufu_jyuusyo1, ")    '���������t��Z��1
            .AppendLine("   skysy_soufu_jyuusyo2 = @skysy_soufu_jyuusyo2, ")    '���������t��Z��2
            .AppendLine("   skysy_soufu_yuubin_no = @skysy_soufu_yuubin_no, ")    '���������t��X�֔ԍ�
            .AppendLine("   skysy_soufu_tel_no = @skysy_soufu_tel_no, ")    '���������t��d�b�ԍ�
            .AppendLine("   skysy_soufu_fax_no = @skysy_soufu_fax_no, ")    '���������t��FAX�ԍ�
            '=========2012/04/10 �ԗ� 405738 �ǉ���================================
            .AppendLine("   syuukei_fc_ten_cd = @syuukei_fc_ten_cd, ")   '�W�vFC�p����
            .AppendLine("   eria_cd = @eria_cd, ")   '�G���A�R�[�h
            .AppendLine("   block_cd = @block_cd, ")   '�u���b�N�R�[�h
            .AppendLine("   fc_ten_kbn = @fc_ten_kbn, ")   'FC�X�敪
            .AppendLine("   fc_nyuukai_date = @fc_nyuukai_date, ")   'FC����N��
            .AppendLine("   fc_taikai_date = @fc_taikai_date, ")   'FC�މ�N��
            .AppendLine("   fc_tys_kaisya_cd = @fc_tys_kaisya_cd, ")   '(FC)������ЃR�[�h
            .AppendLine("   fc_jigyousyo_cd = @fc_jigyousyo_cd, ")   '(FC)���Ə��R�[�h
            '=========2012/04/10 �ԗ� 405738 �ǉ���================================
            .AppendLine("   upd_login_user_id = @upd_login_user_id, ")    '�X�V���O�C�����[�U�[ID
            .AppendLine("   upd_datetime = GETDATE() ")    '�X�V����
            .AppendLine("WHERE ")
            .AppendLine("  eigyousyo_cd = @eigyousyo_cd ")
        End With

        '�p�����[�^�̐ݒ�
        With paramList
            '������ЃR�[�h
            .Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 255, dtEigyousyo(0).eigyousyo_cd))
            '���
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtEigyousyo(0).torikesi))
            '�c�Ə���
            .Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).eigyousyo_mei = "", DBNull.Value, dtEigyousyo(0).eigyousyo_mei)))
            '�c�Ə��J�i
            .Add(MakeParam("@eigyousyo_kana", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).eigyousyo_kana = "", DBNull.Value, dtEigyousyo(0).eigyousyo_kana)))
            '�c�Ə����󎚗L��
            .Add(MakeParam("@eigyousyo_mei_inji_umu", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).eigyousyo_mei_inji_umu = "", DBNull.Value, dtEigyousyo(0).eigyousyo_mei_inji_umu)))
            '������R�[�h
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_cd = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_cd)))
            '������}��
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_brc = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_brc)))
            '������敪
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_kbn = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_kbn)))
            '�Z��1
            .Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).jyuusyo1 = "", DBNull.Value, dtEigyousyo(0).jyuusyo1)))
            '�Z��2
            .Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).jyuusyo2 = "", DBNull.Value, dtEigyousyo(0).jyuusyo2)))
            '�X�֔ԍ�
            .Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).yuubin_no = "", DBNull.Value, dtEigyousyo(0).yuubin_no)))
            '�d�b�ԍ�
            .Add(MakeParam("@tel_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).tel_no = "", DBNull.Value, dtEigyousyo(0).tel_no)))
            'FAX�ԍ�
            .Add(MakeParam("@fax_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).fax_no = "", DBNull.Value, dtEigyousyo(0).fax_no)))
            '������
            .Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).busyo_mei = "", DBNull.Value, dtEigyousyo(0).busyo_mei)))
            '�����於
            .Add(MakeParam("@seikyuu_saki_mei", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_mei = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_mei)))
            '������J�i
            .Add(MakeParam("@seikyuu_saki_kana", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_kana = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_kana)))
            '���������t��Z��1
            .Add(MakeParam("@skysy_soufu_jyuusyo1", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_jyuusyo1 = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_jyuusyo1)))
            '���������t��Z��2
            .Add(MakeParam("@skysy_soufu_jyuusyo2", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_jyuusyo2 = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_jyuusyo2)))
            '���������t��X�֔ԍ�
            .Add(MakeParam("@skysy_soufu_yuubin_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_yuubin_no = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_yuubin_no)))
            '���������t��d�b�ԍ�
            .Add(MakeParam("@skysy_soufu_tel_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_tel_no = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_tel_no)))
            '���������t��FAX�ԍ�
            .Add(MakeParam("@skysy_soufu_fax_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_fax_no = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_fax_no)))

            '=========2012/04/10 �ԗ� 405738 �ǉ���================================
            '�W�vFC�p����
            .Add(MakeParam("@syuukei_fc_ten_cd", SqlDbType.VarChar, 5, IIf(dtEigyousyo(0).syuukei_fc_ten_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).syuukei_fc_ten_cd)))
            '�G���A�R�[�h
            .Add(MakeParam("@eria_cd", SqlDbType.VarChar, 2, IIf(dtEigyousyo(0).eria_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).eria_cd)))
            '�u���b�N�R�[�h
            .Add(MakeParam("@block_cd", SqlDbType.VarChar, 2, IIf(dtEigyousyo(0).block_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).block_cd)))
            'FC�X�敪
            .Add(MakeParam("@fc_ten_kbn", SqlDbType.Int, 10, IIf(dtEigyousyo(0).fc_ten_kbn.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_ten_kbn)))
            'FC����N��
            .Add(MakeParam("@fc_nyuukai_date", SqlDbType.DateTime, 20, IIf(dtEigyousyo(0).fc_nyuukai_date.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_nyuukai_date & "/01")))
            'FC�މ�N��
            .Add(MakeParam("@fc_taikai_date", SqlDbType.DateTime, 20, IIf(dtEigyousyo(0).fc_taikai_date.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_taikai_date & "/01")))
            '(FC)������ЃR�[�h
            .Add(MakeParam("@fc_tys_kaisya_cd", SqlDbType.VarChar, 5, IIf(dtEigyousyo(0).fc_tys_kaisya_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_tys_kaisya_cd)))
            '(FC)���Ə��R�[�h
            .Add(MakeParam("@fc_jigyousyo_cd", SqlDbType.VarChar, 2, IIf(dtEigyousyo(0).fc_jigyousyo_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_jigyousyo_cd)))
            '=========2012/04/10 �ԗ� 405738 �ǉ���================================

            '�X�V���O�C�����[�U�[ID
            .Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).upd_login_user_id = "", DBNull.Value, dtEigyousyo(0).upd_login_user_id)))
        End With

        ' �N�G�����s
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' �o�^����
    ''' </summary>
    ''' <param name="dtEigyousyo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsEigyousyo(ByVal dtEigyousyo As EigyousyoDataSet.m_eigyousyoDataTable) As Integer

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("   m_eigyousyo WITH (UPDLOCK) ")
            .AppendLine("( ")
            .AppendLine("   eigyousyo_cd, ")   '�c�Ə��R�[�h
            .AppendLine("   torikesi, ")   '���
            .AppendLine("   eigyousyo_mei, ")   '�c�Ə���
            .AppendLine("   eigyousyo_kana, ")   '�c�Ə��J�i
            .AppendLine("   eigyousyo_mei_inji_umu, ")   '�c�Ə����󎚗L��
            '.AppendLine("   pca_seikyuu_cd, ")   'PCA������R�[�h
            .AppendLine("   seikyuu_saki_cd, ")   '������R�[�h
            .AppendLine("   seikyuu_saki_brc, ")   '������}��
            .AppendLine("   seikyuu_saki_kbn, ")   '������敪
            '.AppendLine("   hansokuhin_sime_date, ")   '�̑��i���ߓ�
            .AppendLine("   yuubin_no, ")   '�X�֔ԍ�
            .AppendLine("   jyuusyo1, ")   '�Z��1
            .AppendLine("   jyuusyo2, ")   '�Z��2
            .AppendLine("   tel_no, ")   '�d�b�ԍ�
            .AppendLine("   fax_no, ")   'FAX�ԍ�
            .AppendLine("   busyo_mei, ")   '������
            .AppendLine("   seikyuu_saki_mei, ")   '�����於
            .AppendLine("   seikyuu_saki_kana, ")   '������J�i
            .AppendLine("   skysy_soufu_jyuusyo1, ")   '���������t��Z��1
            .AppendLine("   skysy_soufu_jyuusyo2, ")   '���������t��Z��2
            .AppendLine("   skysy_soufu_yuubin_no, ")   '���������t��X�֔ԍ�
            .AppendLine("   skysy_soufu_tel_no, ")   '���������t��d�b�ԍ�
            .AppendLine("   skysy_soufu_fax_no, ")   '���������t��FAX�ԍ�
            '=========2012/04/10 �ԗ� 405738 �ǉ���================================
            .AppendLine("   syuukei_fc_ten_cd, ")   '�W�vFC�p����
            .AppendLine("   eria_cd, ")   '�G���A�R�[�h
            .AppendLine("   block_cd, ")   '�u���b�N�R�[�h
            .AppendLine("   fc_ten_kbn, ")   'FC�X�敪
            .AppendLine("   fc_nyuukai_date, ")   'FC����N��
            .AppendLine("   fc_taikai_date, ")   'FC�މ�N��
            .AppendLine("   fc_tys_kaisya_cd, ")   '(FC)������ЃR�[�h
            .AppendLine("   fc_jigyousyo_cd, ")   '(FC)���Ə��R�[�h
            '=========2012/04/10 �ԗ� 405738 �ǉ���================================
            .AppendLine("   add_login_user_id, ")   '�o�^���O�C�����[�U�[ID
            .AppendLine("   add_datetime ")   '�o�^����
            .AppendLine(") ")
            .AppendLine("SELECT ")
            .AppendLine("   @eigyousyo_cd, ")   '�c�Ə��R�[�h
            .AppendLine("   @torikesi, ")   '���
            .AppendLine("   @eigyousyo_mei, ")   '�c�Ə���
            .AppendLine("   @eigyousyo_kana, ")   '�c�Ə��J�i
            .AppendLine("   @eigyousyo_mei_inji_umu, ")   '�c�Ə����󎚗L��
            '.AppendLine("   @pca_seikyuu_cd, ")   'PCA������R�[�h
            .AppendLine("   @seikyuu_saki_cd, ")   '������R�[�h
            .AppendLine("   @seikyuu_saki_brc, ")   '������}��
            .AppendLine("   @seikyuu_saki_kbn, ")   '������敪
            '.AppendLine("   @hansokuhin_sime_date, ")   '�̑��i���ߓ�
            .AppendLine("   @yuubin_no, ")   '�X�֔ԍ�
            .AppendLine("   @jyuusyo1, ")   '�Z��1
            .AppendLine("   @jyuusyo2, ")   '�Z��2
            .AppendLine("   @tel_no, ")   '�d�b�ԍ�
            .AppendLine("   @fax_no, ")   'FAX�ԍ�
            .AppendLine("   @busyo_mei, ")   '������
            .AppendLine("   @seikyuu_saki_mei, ")   '�����於
            .AppendLine("   @seikyuu_saki_kana, ")   '������J�i
            .AppendLine("   @skysy_soufu_jyuusyo1, ")   '���������t��Z��1
            .AppendLine("   @skysy_soufu_jyuusyo2, ")   '���������t��Z��2
            .AppendLine("   @skysy_soufu_yuubin_no, ")   '���������t��X�֔ԍ�
            .AppendLine("   @skysy_soufu_tel_no, ")   '���������t��d�b�ԍ�
            .AppendLine("   @skysy_soufu_fax_no, ")   '���������t��FAX�ԍ�
            '=========2012/04/10 �ԗ� 405738 �ǉ���================================
            .AppendLine("   @syuukei_fc_ten_cd, ")   '�W�vFC�p����
            .AppendLine("   @eria_cd, ")   '�G���A�R�[�h
            .AppendLine("   @block_cd, ")   '�u���b�N�R�[�h
            .AppendLine("   @fc_ten_kbn, ")   'FC�X�敪
            .AppendLine("   @fc_nyuukai_date, ")   'FC����N��
            .AppendLine("   @fc_taikai_date, ")   'FC�މ�N��
            .AppendLine("   @fc_tys_kaisya_cd, ")   '(FC)������ЃR�[�h
            .AppendLine("   @fc_jigyousyo_cd, ")   '(FC)���Ə��R�[�h
            '=========2012/04/10 �ԗ� 405738 �ǉ���================================
            .AppendLine("   @add_login_user_id, ")   '�o�^���O�C�����[�U�[ID
            .AppendLine("   GETDATE() ")   '�o�^����
        End With

        '�p�����[�^�̐ݒ�
        With paramList
            '������ЃR�[�h
            .Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 255, dtEigyousyo(0).eigyousyo_cd))
            '���
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtEigyousyo(0).torikesi))
            '�c�Ə���
            .Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).eigyousyo_mei = "", DBNull.Value, dtEigyousyo(0).eigyousyo_mei)))
            '�c�Ə��J�i
            .Add(MakeParam("@eigyousyo_kana", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).eigyousyo_kana = "", DBNull.Value, dtEigyousyo(0).eigyousyo_kana)))
            '�c�Ə����󎚗L��
            .Add(MakeParam("@eigyousyo_mei_inji_umu", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).eigyousyo_mei_inji_umu = "", DBNull.Value, dtEigyousyo(0).eigyousyo_mei_inji_umu)))
            '������R�[�h
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_cd = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_cd)))
            '������}��
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_brc = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_brc)))
            '������敪
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_kbn = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_kbn)))
            '�Z��1
            .Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).jyuusyo1 = "", DBNull.Value, dtEigyousyo(0).jyuusyo1)))
            '�Z��2
            .Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).jyuusyo2 = "", DBNull.Value, dtEigyousyo(0).jyuusyo2)))
            '�X�֔ԍ�
            .Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).yuubin_no = "", DBNull.Value, dtEigyousyo(0).yuubin_no)))
            '�d�b�ԍ�
            .Add(MakeParam("@tel_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).tel_no = "", DBNull.Value, dtEigyousyo(0).tel_no)))
            'FAX�ԍ�
            .Add(MakeParam("@fax_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).fax_no = "", DBNull.Value, dtEigyousyo(0).fax_no)))
            '������
            .Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).busyo_mei = "", DBNull.Value, dtEigyousyo(0).busyo_mei)))
            '�����於
            .Add(MakeParam("@seikyuu_saki_mei", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_mei = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_mei)))
            '������J�i
            .Add(MakeParam("@seikyuu_saki_kana", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).seikyuu_saki_kana = "", DBNull.Value, dtEigyousyo(0).seikyuu_saki_kana)))
            '���������t��Z��1
            .Add(MakeParam("@skysy_soufu_jyuusyo1", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_jyuusyo1 = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_jyuusyo1)))
            '���������t��Z��2
            .Add(MakeParam("@skysy_soufu_jyuusyo2", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_jyuusyo2 = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_jyuusyo2)))
            '���������t��X�֔ԍ�
            .Add(MakeParam("@skysy_soufu_yuubin_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_yuubin_no = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_yuubin_no)))
            '���������t��d�b�ԍ�
            .Add(MakeParam("@skysy_soufu_tel_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_tel_no = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_tel_no)))
            '���������t��FAX�ԍ�
            .Add(MakeParam("@skysy_soufu_fax_no", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).skysy_soufu_fax_no = "", DBNull.Value, dtEigyousyo(0).skysy_soufu_fax_no)))

            '=========2012/04/10 �ԗ� 405738 �ǉ���================================
            '�W�vFC�p����
            .Add(MakeParam("@syuukei_fc_ten_cd", SqlDbType.VarChar, 5, IIf(dtEigyousyo(0).syuukei_fc_ten_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).syuukei_fc_ten_cd)))
            '�G���A�R�[�h
            .Add(MakeParam("@eria_cd", SqlDbType.VarChar, 2, IIf(dtEigyousyo(0).eria_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).eria_cd)))
            '�u���b�N�R�[�h
            .Add(MakeParam("@block_cd", SqlDbType.VarChar, 2, IIf(dtEigyousyo(0).block_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).block_cd)))
            'FC�X�敪
            .Add(MakeParam("@fc_ten_kbn", SqlDbType.Int, 10, IIf(dtEigyousyo(0).fc_ten_kbn.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_ten_kbn)))
            'FC����N��
            .Add(MakeParam("@fc_nyuukai_date", SqlDbType.DateTime, 20, IIf(dtEigyousyo(0).fc_nyuukai_date.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_nyuukai_date & "/01")))
            'FC�މ�N��
            .Add(MakeParam("@fc_taikai_date", SqlDbType.DateTime, 20, IIf(dtEigyousyo(0).fc_taikai_date.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_taikai_date & "/01")))
            '(FC)������ЃR�[�h
            .Add(MakeParam("@fc_tys_kaisya_cd", SqlDbType.VarChar, 5, IIf(dtEigyousyo(0).fc_tys_kaisya_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_tys_kaisya_cd)))
            '(FC)���Ə��R�[�h
            .Add(MakeParam("@fc_jigyousyo_cd", SqlDbType.VarChar, 2, IIf(dtEigyousyo(0).fc_jigyousyo_cd.Trim.Equals(String.Empty), DBNull.Value, dtEigyousyo(0).fc_jigyousyo_cd)))
            '=========2012/04/10 �ԗ� 405738 �ǉ���================================

            '�o�^���O�C�����[�U�[ID
            .Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 255, IIf(dtEigyousyo(0).add_login_user_id = "", DBNull.Value, dtEigyousyo(0).add_login_user_id)))
        End With

        ' �N�G�����s
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

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
    ''' �����X�}�X�^���擾����B
    ''' </summary>
    Public Function SelKameiten(ByVal strEigyousyoCd As String) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   kameiten_cd ")
            .AppendLine("FROM ")
            .AppendLine("   m_kameiten WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   eigyousyo_cd = @eigyousyo_cd ")
            .AppendLine("AND ")
            .AppendLine("   (kbn = @kbn ")
            .AppendLine("   OR kbn = @kbn2 )")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strEigyousyoCd))
        paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, "A"))
        paramList.Add(MakeParam("@kbn2", SqlDbType.VarChar, 1, "C"))
        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �����X�Z���}�X�^
    ''' </summary>
    Public Function SelKameitenJyuusyo(ByVal strKameitenCd As String, Optional ByVal strFlg As String = "") As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   kameiten_cd ")
            .AppendLine("FROM ")
            .AppendLine("   m_kameiten_jyuusyo WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   kameiten_cd = @kameiten_cd ")
            If strFlg = "1" Then
                .AppendLine("AND ")
                .AppendLine("   (jyuusyo_no <> @jyuusyo_no OR jyuusyo_no IS NULL) ")
            Else
                .AppendLine("AND ")
                .AppendLine("   jyuusyo_no = @jyuusyo_no ")
            End If
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.VarChar, 1, "2"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �����X�Z���}�X�^�X�V�ƒǉ����e�擾
    ''' </summary>
    Public Function SelNaiyou(ByVal strKameitenCd As String) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   ME.jyuusyo1, ")
            .AppendLine("   ME.jyuusyo2, ")
            .AppendLine("   ME.yuubin_no, ")
            .AppendLine("   ME.tel_no, ")
            .AppendLine("   ME.fax_no, ")
            .AppendLine("   ME.busyo_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_kameiten MK WITH (READCOMMITTED)  ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_eigyousyo ME WITH (READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("   MK.eigyousyo_cd = ME.eigyousyo_cd ")
            .AppendLine("WHERE ")
            .AppendLine("   MK.kameiten_cd = @kameiten_cd ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �����X�Z���}�X�^�ǉ�����
    ''' </summary>
    ''' <param name="strKameitenCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsKameitenJyuusyo(ByVal strKameitenCd As String, ByVal dtNaiyou As Data.DataTable, ByVal strFlg As String, ByVal strUserId As String) As Integer

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("   m_kameiten_jyuusyo WITH (UPDLOCK) ")
            .AppendLine("( ")
            .AppendLine("   kameiten_cd, ")   '�����X�R�[�h
            .AppendLine("   jyuusyo_no, ")   '�Z��NO
            .AppendLine("   jyuusyo1, ")   '�Z��1
            .AppendLine("   jyuusyo2, ")   '�Z��2
            .AppendLine("   yuubin_no, ")   '�X�֔ԍ�
            .AppendLine("   tel_no, ")   '�d�b�ԍ�
            .AppendLine("   fax_no, ")   'FAX�ԍ�
            .AppendLine("   busyo_mei, ")   '������
            .AppendLine("   daihyousya_mei, ")   '��\�Җ�
            .AppendLine("   add_nengetu, ")   '�o�^�N��
            .AppendLine("   seikyuusyo_flg, ")   '������FLG
            .AppendLine("   hosyousyo_flg, ")   '�ۏ؏�FLG
            .AppendLine("   hkks_flg, ")   '�񍐏�FLG
            .AppendLine("   teiki_kankou_flg, ")   '������sFLG
            .AppendLine("   bikou1, ")   '���l1
            .AppendLine("   bikou2, ")   '���l2
            .AppendLine("   upd_date, ")   '�X�V��
            .AppendLine("   mail_address, ")   '���[���A�h���X
            .AppendLine("   kasi_hosyousyo_flg, ")   '���r�ۏ؏�FLG
            .AppendLine("   koj_hkks_flg, ")   '�H���񍐏�FLG
            .AppendLine("   kensa_hkks_flg, ")   '�����񍐏�FLG
            .AppendLine("   add_login_user_id, ")   '�o�^���O�C�����[�U�[ID
            .AppendLine("   add_datetime ")   '�o�^����
            .AppendLine(") ")
            .AppendLine("SELECT ")
            .AppendLine("   @kameiten_cd, ")   '�����X�R�[�h
            .AppendLine("   @jyuusyo_no, ")   '�Z��NO
            .AppendLine("   @jyuusyo1, ")   '�Z��1
            .AppendLine("   @jyuusyo2, ")   '�Z��2
            .AppendLine("   @yuubin_no, ")   '�X�֔ԍ�
            .AppendLine("   @tel_no, ")   '�d�b�ԍ�
            .AppendLine("   @fax_no, ")   'FAX�ԍ�
            .AppendLine("   @busyo_mei, ")   '������
            .AppendLine("   @daihyousya_mei, ")   '��\�Җ�
            .AppendLine("   @add_nengetu, ")   '�o�^�N��
            .AppendLine("   @seikyuusyo_flg, ")   '������FLG
            .AppendLine("   @hosyousyo_flg, ")   '�ۏ؏�FLG
            .AppendLine("   @hkks_flg, ")   '�񍐏�FLG
            .AppendLine("   @teiki_kankou_flg, ")   '������sFLG
            .AppendLine("   @bikou1, ")   '���l1
            .AppendLine("   @bikou2, ")   '���l2
            .AppendLine("   GETDATE(), ")   '�X�V��
            .AppendLine("   @mail_address, ")   '���[���A�h���X
            .AppendLine("   @kasi_hosyousyo_flg, ")   '���r�ۏ؏�FLG
            .AppendLine("   @koj_hkks_flg, ")   '�H���񍐏�FLG
            .AppendLine("   @kensa_hkks_flg, ")   '�����񍐏�FLG
            .AppendLine("   @add_login_user_id, ")   '�o�^���O�C�����[�U�[ID
            .AppendLine("   GETDATE() ")   '�o�^����
        End With

        '�p�����[�^�̐ݒ�
        With paramList
            '�����X�R�[�h
            .Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 255, strKameitenCd.ToUpper))
            '�Z��NO
            .Add(MakeParam("@jyuusyo_no", SqlDbType.VarChar, 255, "2"))
           
            '�Z��1
            .Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("jyuusyo1")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("jyuusyo1"))))
            '�Z��2
            .Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("jyuusyo2")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("jyuusyo2"))))
            '�X�֔ԍ�
            .Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("yuubin_no")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("yuubin_no"))))
            '�d�b�ԍ�
            .Add(MakeParam("@tel_no", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("tel_no")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("tel_no"))))
            'FAX�ԍ�
            .Add(MakeParam("@fax_no", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("fax_no")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("fax_no"))))
            '������
            .Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("busyo_mei")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("busyo_mei"))))

            '��\�Җ�
            .Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 255, DBNull.Value))
            '�o�^�N��
            .Add(MakeParam("@add_nengetu", SqlDbType.VarChar, 255, DBNull.Value))
            '������FLG
            .Add(MakeParam("@seikyuusyo_flg", SqlDbType.VarChar, 255, strFlg))
            '�ۏ؏�FLG
            .Add(MakeParam("@hosyousyo_flg", SqlDbType.VarChar, 255, strFlg))
            '�񍐏�FLG
            .Add(MakeParam("@hkks_flg", SqlDbType.VarChar, 255, strFlg))
            '������sFLG
            .Add(MakeParam("@teiki_kankou_flg", SqlDbType.VarChar, 255, strFlg))

            '���l1
            .Add(MakeParam("@bikou1", SqlDbType.VarChar, 255, DBNull.Value))
            '���l2
            .Add(MakeParam("@bikou2", SqlDbType.VarChar, 255, DBNull.Value))
            '���[���A�h���X
            .Add(MakeParam("@mail_address", SqlDbType.VarChar, 255, DBNull.Value))

            '���r�ۏ؏�FLG
            .Add(MakeParam("@kasi_hosyousyo_flg", SqlDbType.VarChar, 255, strFlg))
            '�H���񍐏�FLG
            .Add(MakeParam("@koj_hkks_flg", SqlDbType.VarChar, 255, strFlg))
            '�����񍐏�FLG
            .Add(MakeParam("@kensa_hkks_flg", SqlDbType.VarChar, 255, strFlg))

            '�o�^���O�C�����[�U�[ID
            .Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 255, strUserId))
        End With

        ' �N�G�����s
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' �����X�Z���}�X�^�X�V����
    ''' </summary>
    ''' <param name="strKameitenCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdKameitenJyuusyo(ByVal strKameitenCd As String, ByVal dtNaiyou As Data.DataTable, ByVal strUserId As String) As Integer

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("   m_kameiten_jyuusyo WITH (UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("   jyuusyo1 = @jyuusyo1, ")   '�Z��1
            .AppendLine("   jyuusyo2 = @jyuusyo2, ")   '�Z��2
            .AppendLine("   yuubin_no = @yuubin_no, ")   '�X�֔ԍ�
            .AppendLine("   tel_no = @tel_no, ")   '�d�b�ԍ�
            .AppendLine("   fax_no = @fax_no, ")   'FAX�ԍ�
            .AppendLine("   busyo_mei = @busyo_mei, ")   '������
            .AppendLine("   upd_date = GETDATE(), ")   '�X�V��
            .AppendLine("   upd_login_user_id = @upd_login_user_id, ")   '�X�V���O�C�����[�U�[ID
            .AppendLine("   upd_datetime = GETDATE() ")   '�X�V����
            .AppendLine("WHERE ")
            .AppendLine("  kameiten_cd = @kameiten_cd ")
            .AppendLine("AND ")
            .AppendLine("  jyuusyo_no = @jyuusyo_no ")
        End With

        '�p�����[�^�̐ݒ�
        With paramList
            '�����X�R�[�h
            .Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 255, strKameitenCd))

            '�Z��1
            .Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("jyuusyo1")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("jyuusyo1"))))
            '�Z��2
            .Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("jyuusyo2")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("jyuusyo2"))))
            '�X�֔ԍ�
            .Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("yuubin_no")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("yuubin_no"))))
            '�d�b�ԍ�
            .Add(MakeParam("@tel_no", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("tel_no")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("tel_no"))))
            'FAX�ԍ�
            .Add(MakeParam("@fax_no", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("fax_no")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("fax_no"))))
            '������
            .Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 255, IIf(TrimNull(dtNaiyou.Rows(0).Item("busyo_mei")) = "", DBNull.Value, dtNaiyou.Rows(0).Item("busyo_mei"))))

            '�Z��No
            .Add(MakeParam("@jyuusyo_no", SqlDbType.VarChar, 255, "2"))

            '�X�V���O�C�����[�U�[ID
            .Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 255, strUserId))
        End With

        ' �N�G�����s
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function


    ''' <summary>
    ''' �����X�Z���}�X�^�A�g�Ǘ��e�[�u��
    ''' </summary>
    ''' <history>20101108 �n���R �����X�Z���}�X�^�A�g�Ǘ��e�[�u�����ǉ��E�X�V����K�v������܂��B</history>
    Public Function SelKameitenJyuusyoRenkei(ByVal strKameitenCd As String) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   kameiten_cd ")
            .AppendLine("FROM ")
            .AppendLine("   m_kameiten_jyuusyo_renkei WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   kameiten_cd = @kameiten_cd ")
            .AppendLine("AND ")
            .AppendLine("   jyuusyo_no = @jyuusyo_no ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.VarChar, 1, "2"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �����X�Z���}�X�^�A�g�Ǘ��e�[�u���̍X�V����
    ''' </summary>
    ''' <history>20101108 �n���R �����X�Z���}�X�^�A�g�Ǘ��e�[�u�����ǉ��E�X�V����K�v������܂��B</history>
    Public Function UpdKameitenJyuusyoRenkei(ByVal strKameitenCd As String, ByVal strUserId As String) As Integer

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("   m_kameiten_jyuusyo_renkei WITH (UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("   renkei_siji_cd = @renkei_siji_cd, ")   '�A�g�w���R�[�h
            .AppendLine("   sousin_jyky_cd = @sousin_jyky_cd, ")   '���M�󋵃R�[�h
            .AppendLine("   sousin_kanry_datetime = @sousin_kanry_datetime, ")   '���M��������
            .AppendLine("   upd_login_user_id = @upd_login_user_id, ")   '�X�V���O�C�����[�U�[ID
            .AppendLine("   upd_datetime = GETDATE() ")   '�X�V����
            .AppendLine("WHERE ")
            .AppendLine("  kameiten_cd = @kameiten_cd ")
            .AppendLine("AND ")
            .AppendLine("  jyuusyo_no = @jyuusyo_no ")
        End With

        '�p�����[�^�̐ݒ�
        With paramList
            '�����X�R�[�h
            .Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 255, strKameitenCd))
            '�Z��No
            .Add(MakeParam("@jyuusyo_no", SqlDbType.VarChar, 255, "2"))

            '�A�g�w���R�[�h
            .Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, 2))
            '���M�󋵃R�[�h
            .Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
            '���M��������
            .Add(MakeParam("@sousin_kanry_datetime", SqlDbType.DateTime, 255, DBNull.Value))

            '�X�V���O�C�����[�U�[ID
            .Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 255, strUserId))
        End With

        ' �N�G�����s
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' �����X�Z���}�X�^�A�g�Ǘ��e�[�u���̓o�^����
    ''' </summary>
    ''' <history>20101108 �n���R �����X�Z���}�X�^�A�g�Ǘ��e�[�u�����ǉ��E�X�V����K�v������܂��B</history>
    Public Function InsKameitenJyuusyoRenkei(ByVal strKameitenCd As String, ByVal strUserId As String) As Integer

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("   m_kameiten_jyuusyo_renkei WITH (UPDLOCK) ")
            .AppendLine("( ")
            .AppendLine("   kameiten_cd, ")   '�����X�R�[�h
            .AppendLine("   jyuusyo_no, ")   '�Z��NO
            .AppendLine("   renkei_siji_cd, ")   '�A�g�w���R�[�h
            .AppendLine("   sousin_jyky_cd, ")   '���M�󋵃R�[�h
            .AppendLine("   upd_login_user_id, ")   '�o�^���O�C�����[�U�[ID
            .AppendLine("   upd_datetime ")   '�o�^����
            .AppendLine(") ")
            .AppendLine("SELECT ")
            .AppendLine("   @kameiten_cd, ")   '�����X�R�[�h
            .AppendLine("   @jyuusyo_no, ")   '�Z��NO
            .AppendLine("   @renkei_siji_cd, ")   '�A�g�w���R�[�h
            .AppendLine("   @sousin_jyky_cd, ")   '���M�󋵃R�[�h
            .AppendLine("   @upd_login_user_id, ")   '�o�^���O�C�����[�U�[ID
            .AppendLine("   GETDATE() ")   '�o�^����
        End With

        '�p�����[�^�̐ݒ�
        With paramList
            '�����X�R�[�h
            .Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 255, strKameitenCd.ToUpper))
            '�Z��NO
            .Add(MakeParam("@jyuusyo_no", SqlDbType.VarChar, 255, "2"))

            '�A�g�w���R�[�h
            .Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, 1))
            '���M�󋵃R�[�h
            .Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))

            '�o�^���O�C�����[�U�[ID
            .Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 255, strUserId))
        End With

        ' �N�G�����s
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>�󔒂��폜</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function

    ''' <summary>
    ''' CSV�f�[�^���擾����
    ''' </summary>
    ''' <returns>CSV�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function SelEigyousyoCsv() As Data.DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	ISNULL(MKM.meisyou,'') AS meisyou ") '--�u���b�N�� ")
            .AppendLine("	,ISNULL(ME.eigyousyo_cd,'') AS eigyousyo_cd ") '--�c�Ə��R�[�h ")
            .AppendLine("	,'����' AS fc_ten_kbn ") '--FC�敪 ")
            .AppendLine("	,ISNULL(ME.fc_jigyousyo_cd,'') AS fc_jigyousyo_cd ") '--(FC)���Ə��R�[�h ")
            .AppendLine("	,ISNULL(ME.eigyousyo_mei,'') AS eigyousyo_mei ") '--�c�Ə��� ")
            .AppendLine("	,ISNULL(MT.tys_kaisya_mei,'') AS tys_kaisya_mei ") '--������Ж� ")
            .AppendLine("	,ISNULL(MT.yakusyoku_mei,'') AS yakusyoku_mei ") '--��E�� ")
            .AppendLine("	,ISNULL(MT.daihyousya_mei,'') AS daihyousya_mei ") '--��\�Җ� ")
            .AppendLine("	,ISNULL(ME.yuubin_no,'') AS yuubin_no ") '--�X�֔ԍ� ")
            .AppendLine("	,ISNULL(ME.jyuusyo1,'') + ' ' + ISNULL(ME.jyuusyo2,'') AS jyuusyo ") '--�Z��1 + �Z��2 ")
            .AppendLine("	,ISNULL(ME.tel_no,'') AS tel_no ") '--�d�b�ԍ� ")
            .AppendLine("	,ISNULL(ME.fax_no,'') AS fax_no ") '--FAX�ԍ� ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),SUB_TTS.nyuuryoku_date,111),'') AS nyuuryoku_date ") '--���͓� ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),SUB_TTS.seikyuusyo_hak_date,111),'') AS seikyuusyo_hak_date ") '--���������s�� ")
            .AppendLine("	,ISNULL(SUB_TTS.syouhin_cd,'') AS syouhin_cd ") '--���i���� ")
            .AppendLine("	,ISNULL(MS.syouhin_mei,'') AS syouhin_mei ") '--���i�� ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),SUB_TTS.koumuten_seikyuu_tanka),'') AS koumuten_seikyuu_tanka ") '--�H���X�����Ŕ����z(�H���X�����P��) ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),SUB_TTS.tanka),'') AS jituseikyuu_zeinu_kingaku ") '--�������Ŕ����z(�P��) ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),SUB_TTS.syouhizei_gaku),'') AS syouhizei_gaku ") '--�����(����Ŋz) ")
            .AppendLine("	,(ISNULL(SUB_TTS.tanka,0) + ISNULL(SUB_TTS.syouhizei_gaku,0)) AS zeikomi_kingaku ") '--�ō����z(�P���{����Ŋz) ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),SUB_TTS.tanka),'') AS tanka ") '--�P�� ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),SUB_TTS.suu),'') AS suu ") '--���� ")
            .AppendLine("	,ISNULL(ME.eigyousyo_cd,'') AS seijyuusaki_cd ") '--�����溰��(�c�Ə��R�[�h) ")
            .AppendLine("	,ISNULL(ME.eigyousyo_mei,'') AS seijyuusaki_mei ") '--�����溰��(�c�Ə���) ")
            .AppendLine("	 ")
            .AppendLine("FROM ")
            .AppendLine("	m_eigyousyo AS ME WITH(READCOMMITTED) ") '--�c�Ə��}�X�^ ")
            .AppendLine("	INNER JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH(READCOMMITTED) ") '--�g�����̃}�X�^ ")
            .AppendLine("	ON ")
            .AppendLine("		MKM.code = ME.block_cd ") '--�g������M.���� = �c�Ə�M.�u���b�N�R�[�h ")
            .AppendLine("		AND ")
            .AppendLine("		MKM.meisyou_syubetu = @meisyou_syubetu ") '--�g������M.���̎�� = "61" ")
            .AppendLine("		AND ")
            .AppendLine("		ME.fc_ten_kbn = @fc_ten_kbn ") '--�c�Ə�M.FC�敪 = "1"(����) ")
            .AppendLine("		AND ")
            .AppendLine("		ME.torikesi = @torikesi ") '--�c�Ə�M.��� = "0" ")
            .AppendLine("	INNER JOIN ")
            .AppendLine("	m_tyousakaisya AS MT WITH(READCOMMITTED) ") '--������Ѓ}�X�^ ")
            .AppendLine("	ON ")
            .AppendLine("		MT.tys_kaisya_cd = ME.fc_tys_kaisya_cd ") '--�������M.������к��� = �c�Ə�M.(FC)������к��� ")
            .AppendLine("		AND ")
            .AppendLine("		MT.jigyousyo_cd = ME.fc_jigyousyo_cd ") '--�������M.���Ə����� = �c�Ə�M.(FC)���Ə����� ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			mise_cd ") '--�X�R�[�h ")
            .AppendLine("			,nyuuryoku_date ") '--���͓� ")
            .AppendLine("			,seikyuusyo_hak_date ") '--���������s�� ")
            .AppendLine("			,syouhin_cd ") '--���i���� ")
            .AppendLine("			,koumuten_seikyuu_tanka ") '--�H���X�����P�� ")
            .AppendLine("			,tanka ") '--�P�� ")
            .AppendLine("			,syouhizei_gaku ") '--����Ŋz ")
            .AppendLine("			,suu ") '--���� ")
            .AppendLine("		FROM ")
            .AppendLine("			t_tenbetu_seikyuu WITH(READCOMMITTED) ") '--�X�ʐ����e�[�u�� ")
            .AppendLine("		WHERE ")
            .AppendLine("		LEFT(CONVERT(VARCHAR(10),nyuuryoku_date,112),6) = LEFT(CONVERT(VARCHAR(10),GETDATE(),112),6) ") '--�X�ʐ���T.���͓� �� �V�X�e���N�� ���܂� ")
            .AppendLine("	) AS SUB_TTS	 ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_TTS.mise_cd = ME.eigyousyo_cd ") '--�X�ʐ����}�X�^.�X�R�[�h = �c�Ə��}�X�^.�c�Ə��R�[�h ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_syouhin AS MS WITH(READCOMMITTED) ") '--���i�}�X�^ ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_TTS.syouhin_cd = MS.syouhin_cd ") '--���i�}�X�^ ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, 61))
        paramList.Add(MakeParam("@fc_ten_kbn", SqlDbType.Int, 10, 1))
        paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, "0"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtEigyousyoCsv", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables("dtEigyousyoCsv")

    End Function

    ''' <summary>
    ''' �V�X�e�����t���擾����
    ''' </summary>
    ''' <returns>CSV�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function SelSystemDateYMD() As Data.DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("CONVERT(VARCHAR(10),GETDATE(),112) as system_date	 ")
        End With

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtSystemDateYMD")

        '�߂�
        Return dsDataSet.Tables("dtSystemDateYMD")

    End Function

    ''' <summary>
    ''' ddl�̃f�[�^���擾����
    ''' </summary>
    ''' <returns>ddl�̃f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function SelDdlList(ByVal strMeisyouSyubetu As Integer) As Data.DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	code AS code ")
            .AppendLine("	,CONVERT(VARCHAR(10),code) + '�F' + ISNULL(meisyou,'') AS meisyou ")
            .AppendLine("FROM ")
            .AppendLine("	m_kakutyou_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	meisyou_syubetu = @meisyou_syubetu ")
            .AppendLine("ORDER BY ")
            .AppendLine("	hyouji_jyun ASC ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, strMeisyouSyubetu))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtDdl", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables("dtDdl")

    End Function

    ''' <summary>
    ''' ������Џ�񌏐����擾����
    ''' </summary>
    ''' <returns>ddl�̃f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function SelTyousaKaisyaCount(ByVal strTyousaKaisyaCd As String) As Data.DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(tys_kaisya_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	m_tyousakaisya WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_kaisya_cd + jigyousyo_cd LIKE @tys_kaisya_cd ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 8, strTyousaKaisyaCd & "%"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtTyousaKaisyaCount", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables("dtTyousaKaisyaCount")

    End Function

    ''' <summary>
    ''' ������Џ����擾����
    ''' </summary>
    ''' <returns>ddl�̃f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function SelTyousaKaisyaInfo(ByVal strTyousaKaisyaCd As String) As Data.DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	tys_kaisya_cd ") '--������ЃR�[�h ")
            .AppendLine("	,jigyousyo_cd ") '--���Ə��R�[�h ")
            .AppendLine("	,tys_kaisya_mei ") '--������Ж� ")
            .AppendLine("	,tys_kaisya_mei_kana ") '--������Ж��J�i ")
            .AppendLine("	,daihyousya_mei ") '--��\�Җ� ")
            .AppendLine("	,yakusyoku_mei ") '--��E�� ")
            .AppendLine("	,jyuusyo1 ") '--�Z��1 ")
            .AppendLine("	,jyuusyo2 ") '--�Z��2 ")
            .AppendLine("	,yuubin_no ") '--�X�֔ԍ� ")
            .AppendLine("	,tel_no ") '--�d�b�ԍ� ")
            .AppendLine("	,fax_no ") '--FAX�ԍ� ")
            .AppendLine("	,japan_kai_kbn ") '--JAPAN��敪 ")
            .AppendLine("	,CASE ")
            .AppendLine("	    WHEN japan_kai_nyuukai_date IS NOT NULL THEN ")
            .AppendLine("	        LEFT(CONVERT(VARCHAR(10),japan_kai_nyuukai_date,111),7) ") '--JAPAN�����N�� ")
            .AppendLine("	    ELSE ")
            .AppendLine("	        '' ")
            .AppendLine("	    END AS japan_kai_nyuukai_date ")
            .AppendLine("	,CASE ")
            .AppendLine("	    WHEN japan_kai_taikai_date IS NOT NULL THEN ")
            .AppendLine("	        LEFT(CONVERT(VARCHAR(10),japan_kai_taikai_date,111),7) ") '--JAPAN��މ�N�� ")
            .AppendLine("	    ELSE ")
            .AppendLine("	        '' ")
            .AppendLine("	    END AS japan_kai_taikai_date ")
            .AppendLine("	,report_jhs_token_flg ") '--ReportJHS�g�[�N���L���t���O ")
            .AppendLine("	,tkt_jbn_tys_syunin_skk_flg ") '--��n�n�Ւ�����C���i�L���t���O ")
            .AppendLine("FROM ")
            .AppendLine("	m_tyousakaisya WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	tys_kaisya_cd + jigyousyo_cd LIKE @tys_kaisya_cd ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 8, strTyousaKaisyaCd & "%"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtTyousaKaisyaInfo", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables("dtTyousaKaisyaInfo")

    End Function

    ''' <summary>
    ''' �Œ�`���[�W�̓��͓����擾����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function SelKoteiTyaaji(ByVal strEigyousyoCd As String, ByVal blnThisMonth As Boolean) As Data.DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	LEFT(CONVERT(VARCHAR(10),MAX(nyuuryoku_date),112),6) AS nyuuryoku_date ")
            .AppendLine("   ,MAX(nyuuryoku_date_no) as nyuuryoku_date_no ")
            .AppendLine("FROM ")
            .AppendLine("	t_tenbetu_seikyuu WITH(READCOMMITTED) ") '--�X�ʐ����e�[�u�� ")
            .AppendLine("WHERE ")
            .AppendLine("	mise_cd = @mise_cd ") '--�X�R�[�h ")
            .AppendLine("	AND ")
            .AppendLine("	bunrui_cd = @bunrui_cd ") '--���ރR�[�h ")
            .AppendLine("	AND ")
            .AppendLine("	syouhin_cd = @syouhin_cd ") '--���i�R�[�h ")
            If blnThisMonth Then
                .AppendLine("	AND ")
                .AppendLine("	LEFT(CONVERT(VARCHAR(10),nyuuryoku_date,112),6) = LEFT(CONVERT(VARCHAR(10),GETDATE(),112),6) ") '--���͓� ")
            End If
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strEigyousyoCd))
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, "230"))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, "L2001"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtKoteiTyaajiYM", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables("dtKoteiTyaajiYM")

    End Function

    ''' <summary>
    ''' �X�ʐ����e�[�u����o�^����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function InsTenbetuSeikyuu(ByVal strEigyousyoCd As String, ByVal strUserId As String) As Boolean

        Dim intInsCount As Integer

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("	t_tenbetu_seikyuu WITH(UPDLOCK) ") '--�X�ʐ����e�[�u�� ")
            .AppendLine("	( ")
            .AppendLine("		mise_cd ") '--�X�R�[�h ")
            .AppendLine("		,bunrui_cd ") '--���ރR�[�h ")
            .AppendLine("		,nyuuryoku_date ") '--���͓� ")
            .AppendLine("		,nyuuryoku_date_no ") '--���͓�NO ")
            .AppendLine("		,hassou_date ") '--������ ")
            .AppendLine("		,seikyuusyo_hak_date ") '--���������s�� ")
            .AppendLine("		,uri_date ") '--����N���� ")
            .AppendLine("		,denpyou_uri_date ") '--�`�[����N���� ")
            .AppendLine("		,uri_keijyou_flg ") '--���㏈��FLG(����v��FLG) ")
            .AppendLine("		,syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("		,tanka ") '--�P�� ")
            .AppendLine("		,suu ") '--���� ")
            .AppendLine("		,zei_kbn ") '--�ŋ敪 ")
            .AppendLine("		,syouhizei_gaku ") '--����Ŋz ")
            .AppendLine("		,koumuten_seikyuu_tanka ") '--�H���X�����P�� ")
            .AppendLine("		,add_login_user_id ") '--�o�^���O�C�����[�U�[ID ")
            .AppendLine("		,add_datetime ") '--�o�^���� ")
            .AppendLine("	) ")
            .AppendLine("SELECT ")
            .AppendLine("	(@eigyousyo_cd + ' ') AS mise_cd ") '--�X�R�[�h ")
            .AppendLine("	,@bunrui_cd AS bunrui_cd ") '--���ރR�[�h ")
            .AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111)) AS nyuuryoku_date ") '--���͓� ")
            '============2012/05/14 �ԗ� �v�]�̑Ή� �C����=========================
            .AppendLine("   ,@nyuuryoku_date_no AS nyuuryoku_date_no") '--���͓�NO ")
            '.AppendLine("	,( ")
            '.AppendLine("		SELECT ")
            '.AppendLine("			CASE ")
            '.AppendLine("				WHEN MAX(nyuuryoku_date_no) IS NULL THEN ")
            '.AppendLine("					1 ")
            '.AppendLine("				ELSE ")
            '.AppendLine("					MAX(nyuuryoku_date_no) + 1 ")
            '.AppendLine("				END as nyuuryoku_date_no ")
            '.AppendLine("		FROM ")
            '.AppendLine("			t_tenbetu_seikyuu WITH(READCOMMITTED) ")
            '.AppendLine("		WHERE ")
            '.AppendLine("			mise_cd = @eigyousyo_cd ")
            '.AppendLine("			AND ")
            '.AppendLine("			bunrui_cd = @bunrui_cd ")
            '.AppendLine("			AND ")
            '.AppendLine("			syouhin_cd = @syouhin_cd ")
            '.AppendLine("			 ")
            '.AppendLine("	) AS nyuuryoku_date_no ") '--���͓�NO ")
            '============2012/05/14 �ԗ� �v�]�̑Ή� �C����=========================
            .AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111)) AS hassou_date ") '--������ ")
            .AppendLine("	,( ")
            .AppendLine("		SELECT ")
            .AppendLine("			CASE ISNULL(MSS.seikyuu_sime_date,'') ")
            .AppendLine("				WHEN '' THEN ")
            .AppendLine("					DATEADD(DAY,-1,CONVERT(DATETIME,LEFT(CONVERT(VARCHAR(10),DATEADD(MONTH,1,GETDATE()),111),8) + '01')) ")
            .AppendLine("				WHEN '31' THEN ")
            .AppendLine("					DATEADD(DAY,-1,CONVERT(DATETIME,LEFT(CONVERT(VARCHAR(10),DATEADD(MONTH,1,GETDATE()),111),8) + '01')) ")
            .AppendLine("				ELSE ")
            .AppendLine("					CASE ISDATE(LEFT(CONVERT(VARCHAR(10),GETDATE(),111),8) + MSS.seikyuu_sime_date) ")
            .AppendLine("						WHEN 1 THEN ")
            .AppendLine("							CONVERT(DATETIME,LEFT(CONVERT(VARCHAR(10),GETDATE(),111),8) + MSS.seikyuu_sime_date) ")
            .AppendLine("						ELSE ")
            .AppendLine("							DATEADD(DAY,-1,CONVERT(DATETIME,LEFT(CONVERT(VARCHAR(10),DATEADD(MONTH,1,GETDATE()),111),8) + '01')) ")
            .AppendLine("						END				 ")
            .AppendLine("				END AS seikyuu_sime_date ")
            .AppendLine("		FROM ")
            .AppendLine("			m_seikyuu_saki AS MSS WITH(READCOMMITTED) ")
            .AppendLine("			INNER JOIN ")
            .AppendLine("			m_eigyousyo AS ME WITH(READCOMMITTED) ")
            .AppendLine("			ON ")
            .AppendLine("				MSS.seikyuu_saki_cd = ME.seikyuu_saki_cd ")
            .AppendLine("				AND ")
            .AppendLine("				MSS.seikyuu_saki_brc = ME.seikyuu_saki_brc ")
            .AppendLine("				AND ")
            .AppendLine("				MSS.seikyuu_saki_kbn = ME.seikyuu_saki_kbn ")
            .AppendLine("		WHERE ")
            .AppendLine("				ME.eigyousyo_cd = @eigyousyo_cd ")
            .AppendLine("	) AS seikyuusyo_hak_date ") '--���������s�� ")
            .AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111)) AS uri_date ") '--����N���� ")
            .AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111)) AS denpyou_uri_date ") '--�`�[����N���� ")
            .AppendLine("	,@uri_keijyou_flg AS uri_keijyou_flg ") '--���㏈��FLG(����v��FLG) ")
            .AppendLine("	,@syouhin_cd AS syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("	,MS.hyoujun_kkk AS tanka ") '--�P��(���i�}�X�^.�W�����i) ")
            .AppendLine("	,@suu AS suu ") '--���� ")
            .AppendLine("	,MS.zei_kbn AS zei_kbn ") '--�ŋ敪(���i�}�X�^.�ŋ敪) ")
            .AppendLine("	,ISNULL(MS.hyoujun_kkk,0) * MSZ.zeiritu AS syouhizei_gaku ") '--����Ŋz(�P���~����Ń}�X�^.�ŗ�) ")
            .AppendLine("	,@koumuten_seikyuu_tanka AS koumuten_seikyuu_tanka ") '--�H���X�����P�� ")
            .AppendLine("	,@user_id AS add_login_user_id ") '--�o�^���O�C�����[�U�[ID ")
            .AppendLine("	,GETDATE() AS add_datetime ") '--�o�^���� ")
            .AppendLine("	 ")
            .AppendLine("FROM ")
            .AppendLine("	m_syouhin AS MS WITH(READCOMMITTED) ") '--���i�}�X�^ ")
            .AppendLine("	INNER JOIN ")
            .AppendLine("	m_syouhizei AS MSZ WITH(READCOMMITTED) ") '--����Ń}�X�^ ")
            .AppendLine("	ON ")
            .AppendLine("		MS.zei_kbn = MSZ.zei_kbn ") '--�ŋ敪 ")
            .AppendLine("WHERE ")
            .AppendLine("	MS.syouhin_cd = @syouhin_cd ") '--���i�R�[�h ")
        End With

        '�p�����[�^�̐ݒ�
        With paramList
            .Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strEigyousyoCd))
            .Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, "230"))
            .Add(MakeParam("@nyuuryoku_date_no", SqlDbType.Int, 10, "1"))
            .Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, "L2001"))
            .Add(MakeParam("@uri_keijyou_flg", SqlDbType.Int, 10, 0))
            .Add(MakeParam("@suu", SqlDbType.Int, 10, 1))
            .Add(MakeParam("@koumuten_seikyuu_tanka", SqlDbType.Int, 10, 0))
            .Add(MakeParam("@user_id", SqlDbType.VarChar, 40, strUserId))

        End With

        Try
            ' �N�G�����s
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

    ''' <summary>
    ''' ������̑��݃`�F�b�N����
    ''' </summary>
    ''' <returns>������̌���</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function SelSeikyuusakiCheck(ByVal strEigyousyoCd As String) As Data.DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(ME.eigyousyo_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	m_eigyousyo AS ME WITH(READCOMMITTED) ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_seikyuu_saki AS MSS WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		ME.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
            .AppendLine("		AND ")
            .AppendLine("		ME.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
            .AppendLine("		AND ")
            .AppendLine("		ME.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
            .AppendLine("WHERE ")
            .AppendLine("	MSS.seikyuu_saki_cd IS NOT NULL ")
            .AppendLine("	AND ")
            .AppendLine("	ME.eigyousyo_cd = @eigyousyo_cd ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strEigyousyoCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtSeikyuusakiCheck", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables("dtSeikyuusakiCheck")

    End Function


    ''' <summary>
    ''' �X�ʐ����e�[�u���A�g�Ǘ��e�[�u���̑��݃`�F�b�N����
    ''' </summary>
    ''' <returns>�X�ʐ����e�[�u���A�g�Ǘ��e�[�u���̌���</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function SelTenbetuSeikyuuRenkeiCount(ByVal strEigyousyoCd As String) As Data.DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(mise_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	t_tenbetu_seikyuu_renkei WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	mise_cd = @mise_cd ")
            .AppendLine("	AND ")
            .AppendLine("	bunrui_cd = @bunrui_cd ")
            .AppendLine("	AND ")
            .AppendLine("	CONVERT(VARCHAR(10),nyuuryoku_date,111) = CONVERT(VARCHAR(10),GETDATE(),111) ")
            .AppendLine("	AND ")
            '.AppendLine("	nyuuryoku_date_no =  ")
            '.AppendLine("		( ")
            '.AppendLine("			SELECT ")
            '.AppendLine("				ISNULL(MAX(nyuuryoku_date_no),0) AS nyuuryoku_date_no ")
            '.AppendLine("			FROM ")
            '.AppendLine("				t_tenbetu_seikyuu WITH(READCOMMITTED) ")
            '.AppendLine("			WHERE ")
            '.AppendLine("				mise_cd = @mise_cd ")
            '.AppendLine("				AND ")
            '.AppendLine("				bunrui_cd = @bunrui_cd ")
            '.AppendLine("				AND ")
            '.AppendLine("				syouhin_cd = @syouhin_cd ")
            '.AppendLine("		) ")

            .AppendLine("	nyuuryoku_date_no = @nyuuryoku_date_no ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strEigyousyoCd))
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, "230"))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, "L2001"))

        paramList.Add(MakeParam("@nyuuryoku_date_no", SqlDbType.Int, 10, "1"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtTenbetuSeikyuuRenkeiCount", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables("dtTenbetuSeikyuuRenkeiCount")

    End Function

    ''' <summary>
    ''' �X�ʐ����e�[�u���A�g�Ǘ��e�[�u����o�^����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function InsTenbetuSeikyuuRenkei(ByVal strEigyousyoCd As String, ByVal strUserId As String) As Boolean

        Dim intInsCount As Integer

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("	t_tenbetu_seikyuu_renkei WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		mise_cd ")
            .AppendLine("		,bunrui_cd ")
            .AppendLine("		,nyuuryoku_date ")
            .AppendLine("		,nyuuryoku_date_no ")
            .AppendLine("		,renkei_siji_cd ")
            .AppendLine("		,sousin_jyky_cd ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            '.AppendLine("SELECT  ")
            '.AppendLine("	@mise_cd AS mise_cd ")
            '.AppendLine("	,@bunrui_cd AS bunrui_cd ")
            '.AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111)) AS nyuuryoku_date ")
            '.AppendLine("	,@nyuuryoku_date_no AS nyuuryoku_date_no  ")
            '.AppendLine("	,@renkei_siji_cd AS renkei_siji_cd ")
            '.AppendLine("	,@sousin_jyky_cd AS sousin_jyky_cd ")
            '.AppendLine("	,@user_id AS upd_login_user_id ")
            '.AppendLine("	,GETDATE() AS upd_datetime ")
            '.AppendLine("FROM  ")
            '.AppendLine("	t_tenbetu_seikyuu WITH(READCOMMITTED)  ")
            '.AppendLine("WHERE  ")
            '.AppendLine("	mise_cd = @mise_cd  ")
            '.AppendLine("	AND  ")
            '.AppendLine("	bunrui_cd = @bunrui_cd  ")
            '.AppendLine("	AND  ")
            '.AppendLine("	syouhin_cd = @syouhin_cd ")

            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@mise_cd ")
            .AppendLine("	,@bunrui_cd ")
            .AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111)) ")
            .AppendLine("	,@nyuuryoku_date_no ")
            .AppendLine("	,@renkei_siji_cd ")
            .AppendLine("	,@sousin_jyky_cd ")
            .AppendLine("	,@user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        '�p�����[�^�̐ݒ�
        With paramList
            .Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strEigyousyoCd))
            .Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, "230"))
            .Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, "L2001"))
            .Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, 1))
            .Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 10, 0))
            .Add(MakeParam("@user_id", SqlDbType.VarChar, 40, strUserId))

            .Add(MakeParam("@nyuuryoku_date_no", SqlDbType.Int, 10, "1"))
        End With

        Try
            ' �N�G�����s
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

    ''' <summary>
    ''' �X�ʐ����e�[�u���A�g�Ǘ��e�[�u�����X�V����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 �ԗ�</history>
    Public Function UpdTenbetuSeikyuuRenkei(ByVal strEigyousyoCd As String, ByVal strUserId As String) As Boolean

        Dim UpdInsCount As Integer

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("	t_tenbetu_seikyuu_renkei WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	renkei_siji_cd = @renkei_siji_cd ")
            .AppendLine("	,sousin_jyky_cd = @sousin_jyky_cd ")
            .AppendLine("	,upd_login_user_id = @user_id ")
            .AppendLine("	,upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	mise_cd = @mise_cd ")
            .AppendLine("	AND ")
            .AppendLine("	bunrui_cd = @bunrui_cd ")
            .AppendLine("	AND ")
            .AppendLine("	CONVERT(VARCHAR(10),nyuuryoku_date,111) = CONVERT(VARCHAR(10),GETDATE(),111) ")
            .AppendLine("	AND ")
            '.AppendLine("	nyuuryoku_date_no =  ")
            '.AppendLine("		( ")
            '.AppendLine("			SELECT ")
            '.AppendLine("				ISNULL(MAX(nyuuryoku_date_no),0) AS nyuuryoku_date_no ")
            '.AppendLine("			FROM ")
            '.AppendLine("				t_tenbetu_seikyuu WITH(READCOMMITTED) ")
            '.AppendLine("			WHERE ")
            '.AppendLine("				mise_cd = @mise_cd ")
            '.AppendLine("				AND ")
            '.AppendLine("				bunrui_cd = @bunrui_cd ")
            '.AppendLine("				AND ")
            '.AppendLine("				syouhin_cd = @syouhin_cd ")
            '.AppendLine("		) ")

            .AppendLine("	nyuuryoku_date_no = @nyuuryoku_date_no ")
        End With

        '�p�����[�^�̐ݒ�
        With paramList
            .Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strEigyousyoCd))
            .Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, "230"))
            .Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, "L2001"))
            .Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, 2))
            .Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 10, 0))
            .Add(MakeParam("@user_id", SqlDbType.VarChar, 40, strUserId))

            paramList.Add(MakeParam("@nyuuryoku_date_no", SqlDbType.Int, 10, "1"))

        End With

        Try
            ' �N�G�����s
            UpdInsCount = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        Catch ex As Exception
            Return False
        End Try

        If UpdInsCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

End Class


