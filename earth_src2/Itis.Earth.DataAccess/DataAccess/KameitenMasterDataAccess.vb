Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

Public Class KameitenMasterDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

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
            .AppendLine("	file_kbn = 8 ")         '�t�@�C���敪(8�F�����X���ꊇ�捞�p)
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
            .AppendLine("	file_kbn = 8 ")             '�t�@�C���敪
        End With

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtInputKanri")

        Return dsReturn.Tables(0).Rows(0).Item(0).ToString

    End Function
    ''' <summary>�n��}�X�^���A�f�[�^���擾����</summary>
    Public Function SelKeiretu(ByVal strCd As String, ByVal strKbn As String, ByRef mei As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet
        mei = ""
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("   keiretu_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_keiretu WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   keiretu_cd = @cd")
            .AppendLine("   AND kbn = @kbn")
        End With

        '�p�����[�^�쐬
        paramList.Add(MakeParam("@cd", SqlDbType.VarChar, 5, strCd))

        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))


        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True

        Else
            mei = ""
            Return False
        End If

    End Function
    ''' <summary>�c�Ə��}�X�^���A�f�[�^���擾����</summary>
    Public Function SelEigyousyo(ByVal strCd As String, ByRef mei As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet
        mei = ""
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("   eigyousyo_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_eigyousyo WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   eigyousyo_cd = @cd")

        End With

        '�p�����[�^�쐬
        paramList.Add(MakeParam("@cd", SqlDbType.VarChar, 5, strCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True
        Else
            mei = ""
            Return False
        End If

    End Function


    ''' <summary>���i�R�[�h���擾����</summary>
    Public Function SelTodoufuken(ByVal strCd As String, ByRef mei As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet
        mei = ""
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("	todouhuken_mei ")
            .AppendLine("FROM  ")
            .AppendLine("	m_todoufuken WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	todouhuken_cd = @todouhuken_cd ")
        End With

        '�p�����[�^�쐬
        paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, strCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True
        Else
            mei = ""
            Return False
        End If

    End Function
    ''' <summary>�A�J�E���g�}�X�^���A�f�[�^���擾����</summary>
    Public Function SelAccount(ByVal strCd As String, ByRef mei As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet
        mei = ""
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("   simei ")
            .AppendLine("FROM ")
            .AppendLine("   m_jiban_ninsyou WITH(READCOMMITTED) left join  m_account WITH(READCOMMITTED) on m_jiban_ninsyou.account_no=m_account.account_no where m_jiban_ninsyou.login_user_id=@cd")


        End With

        '�p�����[�^�쐬
        paramList.Add(MakeParam("@cd", SqlDbType.VarChar, 64, strCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True
        Else
            mei = ""
            Return False
        End If

    End Function
    ''' <summary>������Ѓ}�X�^���A�f�[�^���擾����</summary>
    Public Function SelTyousakaisya(ByVal strCd As String, ByVal strCd2 As String, Optional ByRef mei As String = "") As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("   tys_kaisya_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_tyousakaisya WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   tys_kaisya_cd = @cd")
            .AppendLine("  AND jigyousyo_cd = @cd2")
        End With

        '�p�����[�^�쐬
        paramList.Add(MakeParam("@cd", SqlDbType.VarChar, 5, strCd))
        paramList.Add(MakeParam("@cd2", SqlDbType.VarChar, 2, strCd2))
        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True
        Else
            mei = ""
            Return False
        End If

    End Function
    ''' <summary>�����X�R�[�h���擾����</summary>
    Public Function SelKameitenCd(ByVal strKameitenCd As String, Optional ByVal strKbn As String = "", Optional ByRef mei As String = "") As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("   kameiten_mei1 ")
            .AppendLine("FROM ")
            .AppendLine("   m_kameiten WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   kameiten_cd = @kameiten_cd")
            If Not String.IsNullOrEmpty(strKbn) Then
                .AppendLine("   AND kbn = @kbn")
            End If
        End With

        '�p�����[�^�쐬
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        If Not String.IsNullOrEmpty(strKbn) Then
            paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        End If

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True
        Else
            mei = ""
            Return False
        End If

    End Function

    ''' <summary>���i�R�[�h���擾����</summary>
    Public Function SelSyouhinCd(ByVal strSyouhinCd As String, ByRef mei As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet
        mei = ""
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("	syouhin_mei ")
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
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True
        Else
            mei = ""
            Return False
        End If

    End Function

    '==================2012/03/13 �ԗ� �ǉ���====================================
    ''' <summary>���iϽ�.souko_cd = "115'�@�ɑ��݂��Ȃ��ꍇ�Afalse�ł�</summary>
    Public Function SelSoukoCheck(ByVal strSyouhinCd As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(syouhin_cd) AS cnt ")
            .AppendLine("FROM  ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	syouhin_cd = @syouhin_cd ") '--���i�R�[�h
            .AppendLine("	and ")
            .AppendLine("	souko_cd = @souko_cd ") '--�q�ɃR�[�h
        End With

        '�p�����[�^�쐬
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@souko_cd", SqlDbType.VarChar, 3, "115"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSoukoCheck", paramList.ToArray)

        '�߂�l
        If CInt(dsReturn.Tables(0).Rows(0).Item("cnt").ToString) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    '==================2012/03/13 �ԗ� �ǉ���====================================

    ''' <summary>���̃R�[�h���擾����</summary>
    Public Function SelMeisyouCd(ByVal strMeisyouCd As String, ByRef mei As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet
        mei = ""
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("	meisyou ")
            .AppendLine("FROM  ")
            .AppendLine("	m_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	code = @code ")
            .AppendLine("	AND meisyou_syubetu = '09' ")
        End With

        '�p�����[�^�쐬
        paramList.Add(MakeParam("@code", SqlDbType.VarChar, 8, strMeisyouCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtMeisyouCd", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True
        Else
            mei = ""
            Return False
        End If

    End Function

    ''' <summary>���̃R�[�h���擾����</summary>
    Public Function SelTatouwaribikiSettei(ByVal strKameitenCd As String, ByVal strKubun As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("	kameiten_cd ")
            .AppendLine("FROM  ")
            .AppendLine("	m_tatouwaribiki_settei WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            If strKubun <> "" Then
                .AppendLine(" AND toukubun = strKubun ")
            End If

        End With

        '�p�����[�^�쐬
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, strKameitenCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTatouwaribikiSettei", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>���̃R�[�h���擾����</summary>
    Public Function SelKameitenJyuusyo(ByVal strKameitenCd As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("	kameiten_cd ")
            .AppendLine("FROM  ")
            .AppendLine("	m_kameiten_jyuusyo WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine(" AND jyuusyo_no = 1 ")
        End With

        '�p�����[�^�쐬
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, strKameitenCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTatouwaribikiSettei", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>�G���[���e�[�u���ɃG���[���f�[�^��ǉ�</summary>
    Public Function InstKameitenInfoIttukatuError(ByVal dtError As Data.DataTable) As Boolean

        '�߂�l
        Dim InsCount As Integer = 0

        'SQL�R�����g
        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kameiten_info_ittukatu_error WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		edi_jouhou_sakusei_date")
            .AppendLine("		,gyou_no")
            .AppendLine("		,syori_datetime")
            .AppendLine("		,kbn")
            .AppendLine("		,kameiten_cd")
            .AppendLine("		,torikesi")
            .AppendLine("		,hattyuu_teisi_flg")
            .AppendLine("		,kameiten_mei1")
            .AppendLine("		,tenmei_kana1")
            .AppendLine("		,kameiten_mei2")
            .AppendLine("		,tenmei_kana2")
            .AppendLine("		,builder_no")
            .AppendLine("		,builder_mei")
            .AppendLine("		,keiretu_cd")
            .AppendLine("		,keiretu_mei")
            .AppendLine("		,eigyousyo_cd")
            .AppendLine("		,eigyousyo_mei")
            .AppendLine("		,kameiten_seisiki_mei")
            .AppendLine("		,kameiten_seisiki_mei_kana")
            .AppendLine("		,todouhuken_cd")
            .AppendLine("		,todouhuken_mei")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,nenkan_tousuu ") '--�N�ԓ���
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,fuho_syoumeisyo_flg")
            .AppendLine("		,fuho_syoumeisyo_kaisi_nengetu")
            .AppendLine("		,eigyou_tantousya_mei")
            .AppendLine("		,eigyou_tantousya_simei")
            .AppendLine("		,kyuu_eigyou_tantousya_mei")
            .AppendLine("		,kyuu_eigyou_tantousya_simei")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,koj_uri_syubetsu ") '--�H��������
            .AppendLine("		,koj_uri_syubetsu_mei ") '--�H�������ʖ�
            .AppendLine("		,jiosaki_flg ") '--JIO��t���O
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,kaiyaku_haraimodosi_kkk")
            .AppendLine("		,tou1_syouhin_cd")
            .AppendLine("		,tou1_syouhin_mei")
            .AppendLine("		,tou2_syouhin_cd")
            .AppendLine("		,tou2_syouhin_mei")
            .AppendLine("		,tou3_syouhin_cd")
            .AppendLine("		,tou3_syouhin_mei")
            .AppendLine("		,tys_seikyuu_saki_kbn")
            .AppendLine("		,tys_seikyuu_saki_cd")
            .AppendLine("		,tys_seikyuu_saki_brc")
            .AppendLine("		,tys_seikyuu_saki_mei")
            .AppendLine("		,koj_seikyuu_saki_kbn")
            .AppendLine("		,koj_seikyuu_saki_cd")
            .AppendLine("		,koj_seikyuu_saki_brc")
            .AppendLine("		,koj_seikyuu_saki_mei")
            .AppendLine("		,hansokuhin_seikyuu_saki_kbn")
            .AppendLine("		,hansokuhin_seikyuu_saki_cd")
            .AppendLine("		,hansokuhin_seikyuu_saki_brc")
            .AppendLine("		,hansokuhin_seikyuu_saki_mei")
            .AppendLine("		,tatemono_seikyuu_saki_kbn")
            .AppendLine("		,tatemono_seikyuu_saki_cd")
            .AppendLine("		,tatemono_seikyuu_saki_brc")
            .AppendLine("		,tatemono_seikyuu_saki_mei")
            .AppendLine("		,seikyuu_saki_kbn5")
            .AppendLine("		,seikyuu_saki_cd5")
            .AppendLine("		,seikyuu_saki_brc5")
            .AppendLine("		,seikyuu_saki_mei5")
            .AppendLine("		,seikyuu_saki_kbn6")
            .AppendLine("		,seikyuu_saki_cd6")
            .AppendLine("		,seikyuu_saki_brc6")
            .AppendLine("		,seikyuu_saki_mei6")
            .AppendLine("		,seikyuu_saki_kbn7")
            .AppendLine("		,seikyuu_saki_cd7")
            .AppendLine("		,seikyuu_saki_brc7")
            .AppendLine("		,seikyuu_saki_mei7")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,hosyou_kikan ") '--�ۏ؊���
            .AppendLine("		,hosyousyo_hak_umu ") '--�ۏ؏����s�L��
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,nyuukin_kakunin_jyouken")
            .AppendLine("		,nyuukin_kakunin_oboegaki")
            .AppendLine("		,tys_mitsyo_flg")
            .AppendLine("		,hattyuusyo_flg")
            .AppendLine("		,yuubin_no")
            .AppendLine("		,jyuusyo1")
            .AppendLine("		,jyuusyo2")
            .AppendLine("		,syozaichi_cd")
            .AppendLine("		,syozaichi_mei")
            .AppendLine("		,busyo_mei")
            .AppendLine("		,daihyousya_mei")
            .AppendLine("		,tel_no")
            .AppendLine("		,fax_no")
            .AppendLine("		,mail_address")
            .AppendLine("		,bikou1")
            .AppendLine("		,bikou2")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,add_date ") '--�o�^��
            .AppendLine("		,seikyuu_umu ") '--�����L��
            .AppendLine("		,syouhin_cd ") '--���i�R�[�h
            .AppendLine("		,syouhin_mei ") '--���i��
            .AppendLine("		,uri_gaku ") '--������z
            .AppendLine("		,koumuten_seikyuu_gaku ") '--�H���X�������z
            .AppendLine("		,seikyuusyo_hak_date ") '--���������s��
            .AppendLine("		,uri_date ") '--����N����
            .AppendLine("		,bikou ") '--���l
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            '==========2012/05/09 �ԗ� 407553�̑Ή� �C����======================
            .AppendLine("		,kameiten_upd_datetime")
            .AppendLine("		,tatouwari_upd_datetime1")
            .AppendLine("		,tatouwari_upd_datetime2")
            .AppendLine("		,tatouwari_upd_datetime3")
            .AppendLine("		,kameiten_jyuusyo_upd_datetime")
            '==========2012/05/09 �ԗ� 407553�̑Ή� �C����======================
            .AppendLine("		,bikou_syubetu1")
            .AppendLine("		,bikou_syubetu1_mei")
            .AppendLine("		,bikou_syubetu2")
            .AppendLine("		,bikou_syubetu2_mei")
            .AppendLine("		,bikou_syubetu3")
            .AppendLine("		,bikou_syubetu3_mei")
            .AppendLine("		,bikou_syubetu4")
            .AppendLine("		,bikou_syubetu4_mei")
            .AppendLine("		,bikou_syubetu5")
            .AppendLine("		,bikou_syubetu5_mei")
            .AppendLine("		,naiyou1")
            .AppendLine("		,naiyou2")
            .AppendLine("		,naiyou3")
            .AppendLine("		,naiyou4")
            .AppendLine("		,naiyou5")
            .AppendLine("		,add_login_user_id")
            .AppendLine("		,add_datetime")
            .AppendLine("		,upd_login_user_id")
            .AppendLine("		,upd_datetime")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine("		@edi_jouhou_sakusei_date")
            .AppendLine("		,@gyou_no")
            .AppendLine("		,@syori_datetime")
            .AppendLine("		,@kbn")
            .AppendLine("		,@kameiten_cd")
            .AppendLine("		,@torikesi")
            .AppendLine("		,@hattyuu_teisi_flg")
            .AppendLine("		,@kameiten_mei1")
            .AppendLine("		,@tenmei_kana1")
            .AppendLine("		,@kameiten_mei2")
            .AppendLine("		,@tenmei_kana2")
            .AppendLine("		,@builder_no")
            .AppendLine("		,@builder_mei")
            .AppendLine("		,@keiretu_cd")
            .AppendLine("		,@keiretu_mei")
            .AppendLine("		,@eigyousyo_cd")
            .AppendLine("		,@eigyousyo_mei")
            .AppendLine("		,@kameiten_seisiki_mei")
            .AppendLine("		,@kameiten_seisiki_mei_kana")
            .AppendLine("		,@todouhuken_cd")
            .AppendLine("		,@todouhuken_mei")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,@nenkan_tousuu ") '--�N�ԓ���
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,@fuho_syoumeisyo_flg")
            .AppendLine("		,@fuho_syoumeisyo_kaisi_nengetu")
            .AppendLine("		,@eigyou_tantousya_mei")
            .AppendLine("		,@eigyou_tantousya_simei")
            .AppendLine("		,@kyuu_eigyou_tantousya_mei")
            .AppendLine("		,@kyuu_eigyou_tantousya_simei")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,@koj_uri_syubetsu ") '--�H��������
            .AppendLine("		,@koj_uri_syubetsu_mei ") '--�H�������ʖ�
            .AppendLine("		,@jiosaki_flg ") '--JIO��t���O
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,@kaiyaku_haraimodosi_kkk")
            .AppendLine("		,@syouhin_cd1")
            .AppendLine("		,@syouhin_mei1")
            .AppendLine("		,@syouhin_cd2")
            .AppendLine("		,@syouhin_mei2")
            .AppendLine("		,@syouhin_cd3")
            .AppendLine("		,@syouhin_mei3")
            .AppendLine("		,@tys_seikyuu_saki_kbn")
            .AppendLine("		,@tys_seikyuu_saki_cd")
            .AppendLine("		,@tys_seikyuu_saki_brc")
            .AppendLine("		,@tys_seikyuu_saki_mei")
            .AppendLine("		,@koj_seikyuu_saki_kbn")
            .AppendLine("		,@koj_seikyuu_saki_cd")
            .AppendLine("		,@koj_seikyuu_saki_brc")
            .AppendLine("		,@koj_seikyuu_saki_mei")
            .AppendLine("		,@hansokuhin_seikyuu_saki_kbn")
            .AppendLine("		,@hansokuhin_seikyuu_saki_cd")
            .AppendLine("		,@hansokuhin_seikyuu_saki_brc")
            .AppendLine("		,@hansokuhin_seikyuu_saki_mei")
            .AppendLine("		,@tatemono_seikyuu_saki_kbn")
            .AppendLine("		,@tatemono_seikyuu_saki_cd")
            .AppendLine("		,@tatemono_seikyuu_saki_brc")
            .AppendLine("		,@tatemono_seikyuu_saki_mei")
            .AppendLine("		,@seikyuu_saki_kbn5")
            .AppendLine("		,@seikyuu_saki_cd5")
            .AppendLine("		,@seikyuu_saki_brc5")
            .AppendLine("		,@seikyuu_saki_mei5")
            .AppendLine("		,@seikyuu_saki_kbn6")
            .AppendLine("		,@seikyuu_saki_cd6")
            .AppendLine("		,@seikyuu_saki_brc6")
            .AppendLine("		,@seikyuu_saki_mei6")
            .AppendLine("		,@seikyuu_saki_kbn7")
            .AppendLine("		,@seikyuu_saki_cd7")
            .AppendLine("		,@seikyuu_saki_brc7")
            .AppendLine("		,@seikyuu_saki_mei7")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,@hosyou_kikan ") '--�ۏ؊���
            .AppendLine("		,@hosyousyo_hak_umu ") '--�ۏ؏����s�L��
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,@nyuukin_kakunin_jyouken")
            .AppendLine("		,@nyuukin_kakunin_oboegaki")
            .AppendLine("		,@tys_mitsyo_flg")
            .AppendLine("		,@hattyuusyo_flg")
            .AppendLine("		,@yuubin_no")
            .AppendLine("		,@jyuusyo1")
            .AppendLine("		,@jyuusyo2")
            .AppendLine("		,@syozaichi_cd")
            .AppendLine("		,@syozaichi_mei")
            .AppendLine("		,@busyo_mei")
            .AppendLine("		,@daihyousya_mei")
            .AppendLine("		,@tel_no")
            .AppendLine("		,@fax_no")
            .AppendLine("		,@mail_address")
            .AppendLine("		,@bikou1")
            .AppendLine("		,@bikou2")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,@add_date ") '--�o�^��
            .AppendLine("		,@seikyuu_umu ") '--�����L��
            .AppendLine("		,@syouhin_cd ") '--���i�R�[�h
            .AppendLine("		,@syouhin_mei ") '--���i��
            .AppendLine("		,@uri_gaku ") '--������z
            .AppendLine("		,@koumuten_seikyuu_gaku ") '--�H���X�������z
            .AppendLine("		,@seikyuusyo_hak_date ") '--���������s��
            .AppendLine("		,@uri_date ") '--����N����
            .AppendLine("		,@bikou ") '--���l
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            '==========2012/05/09 �ԗ� 407553�̑Ή� �C����======================
            .AppendLine("		,@kameiten_upd_datetime")
            .AppendLine("		,@tatouwari_upd_datetime1")
            .AppendLine("		,@tatouwari_upd_datetime2")
            .AppendLine("		,@tatouwari_upd_datetime3")
            .AppendLine("		,@kameiten_jyuusyo_upd_datetime")
            '==========2012/05/09 �ԗ� 407553�̑Ή� �C����======================
            .AppendLine("		,@bikou_syubetu1")
            .AppendLine("		,@bikou_syubetu1_mei")
            .AppendLine("		,@bikou_syubetu2")
            .AppendLine("		,@bikou_syubetu2_mei")
            .AppendLine("		,@bikou_syubetu3")
            .AppendLine("		,@bikou_syubetu3_mei")
            .AppendLine("		,@bikou_syubetu4")
            .AppendLine("		,@bikou_syubetu4_mei")
            .AppendLine("		,@bikou_syubetu5")
            .AppendLine("		,@bikou_syubetu5_mei")
            .AppendLine("		,@naiyou1")
            .AppendLine("		,@naiyou2")
            .AppendLine("		,@naiyou3")
            .AppendLine("		,@naiyou4")
            .AppendLine("		,@naiyou5")
            .AppendLine("		,@add_login_user_id")
            .AppendLine("		,CONVERT(VARCHAR(10),GETDATE(),111) ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine("	) ")
        End With

        For i As Integer = 0 To dtError.Rows.Count - 1
            '�p�����[�^�̐ݒ�
            paramList.Clear()
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("edi_jouhou_sakusei_date").ToString.Trim)))
            '==========2012/05/09 �ԗ� 407553�̑Ή� �C����======================
            'paramList.Add(MakeParam("@gyou_no", SqlDbType.Int, 5, InsObj(dtError.Rows(i).Item("gyou_no").ToString.Trim)))
            'paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(dtError.Rows(i).Item("syori_datetime").ToString.Trim))))
            paramList.Add(MakeParam("@gyou_no", SqlDbType.Int, 5, InsObj(dtError.Rows(i).Item("gyou_no").ToString.Trim)))
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(dtError.Rows(i).Item("syori_datetime").ToString.Trim))))
            '==========2012/05/09 �ԗ� 407553�̑Ή� �C����======================
            paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("kbn").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("kameiten_cd").ToString.Trim)))
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("torikesi").ToString.Trim)))
            paramList.Add(MakeParam("@hattyuu_teisi_flg", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item("hattyuu_teisi_flg").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("kameiten_mei1").ToString.Trim)))
            paramList.Add(MakeParam("@tenmei_kana1", SqlDbType.VarChar, 20, InsObj(dtError.Rows(i).Item("tenmei_kana1").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("kameiten_mei2").ToString.Trim)))
            paramList.Add(MakeParam("@tenmei_kana2", SqlDbType.VarChar, 20, InsObj(dtError.Rows(i).Item("tenmei_kana2").ToString.Trim)))
            paramList.Add(MakeParam("@builder_no", SqlDbType.VarChar, 9, InsObj(dtError.Rows(i).Item("builder_no").ToString.Trim)))
            paramList.Add(MakeParam("@builder_mei", SqlDbType.VarChar, 40, GetBuildMei(dtError.Rows(i).Item("builder_no").ToString.Trim)))
            paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("keiretu_cd").ToString.Trim)))
            paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("keiretu_mei").ToString.Trim)))
            paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("eigyousyo_cd").ToString.Trim)))
            paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 40, GetEigyousyoMei(dtError.Rows(i).Item("eigyousyo_cd").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_seisiki_mei", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("kameiten_seisiki_mei").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_seisiki_mei_kana", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("kameiten_seisiki_mei_kana").ToString.Trim)))
            paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("todouhuken_cd").ToString.Trim)))
            paramList.Add(MakeParam("@todouhuken_mei", SqlDbType.VarChar, 10, GetTodouhukenMei(dtError.Rows(i).Item("todouhuken_cd").ToString.Trim)))
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@nenkan_tousuu", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("nenkan_tousuu").ToString.Trim)))
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@fuho_syoumeisyo_flg", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("fuho_syoumeisyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@fuho_syoumeisyo_kaisi_nengetu", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item("fuho_syoumeisyo_kaisi_nengetu").ToString.Trim)))
            paramList.Add(MakeParam("@eigyou_tantousya_mei", SqlDbType.VarChar, 16, InsObj(dtError.Rows(i).Item("eigyou_tantousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@eigyou_tantousya_simei", SqlDbType.VarChar, 30, GetSimei(dtError.Rows(i).Item("eigyou_tantousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@kyuu_eigyou_tantousya_mei", SqlDbType.VarChar, 16, InsObj(dtError.Rows(i).Item("kyuu_eigyou_tantousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@kyuu_eigyou_tantousya_simei", SqlDbType.VarChar, 30, GetSimei(dtError.Rows(i).Item("kyuu_eigyou_tantousya_mei").ToString.Trim)))
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@koj_uri_syubetsu", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("koj_uri_syubetsu").ToString.Trim)))
            paramList.Add(MakeParam("@koj_uri_syubetsu_mei", SqlDbType.VarChar, 40, Me.GetKojUriSyubetuMei(dtError.Rows(i).Item("koj_uri_syubetsu").ToString.Trim)))
            paramList.Add(MakeParam("@jiosaki_flg", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("jiosaki_flg").ToString.Trim)))
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@kaiyaku_haraimodosi_kkk", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item("kaiyaku_haraimodosi_kkk").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd1", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item("syouhin_cd1").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_mei1", SqlDbType.VarChar, 40, GetSyouhinMei(dtError.Rows(i).Item("syouhin_cd1").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd2", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item("syouhin_cd2").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_mei2", SqlDbType.VarChar, 40, GetSyouhinMei(dtError.Rows(i).Item("syouhin_cd2").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd3", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item("syouhin_cd3").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_mei3", SqlDbType.VarChar, 40, GetSyouhinMei(dtError.Rows(i).Item("syouhin_cd3").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("tys_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("tys_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("tys_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_mei", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("tys_seikyuu_saki_mei").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("koj_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("koj_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("koj_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_mei", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("koj_seikyuu_saki_mei").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("hansokuhin_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("hansokuhin_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("hansokuhin_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_mei", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("hansokuhin_seikyuu_saki_mei").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("tatemono_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("tatemono_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("tatemono_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_mei", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("tatemono_seikyuu_saki_mei").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn5", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("seikyuu_saki_kbn5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd5", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("seikyuu_saki_cd5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc5", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("seikyuu_saki_brc5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_mei5", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("seikyuu_saki_mei5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn6", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("seikyuu_saki_kbn6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd6", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("seikyuu_saki_cd6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc6", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("seikyuu_saki_brc6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_mei6", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("seikyuu_saki_mei6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn7", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("seikyuu_saki_kbn7").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd7", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("seikyuu_saki_cd7").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc7", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("seikyuu_saki_brc7").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_mei7", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("seikyuu_saki_mei7").ToString.Trim)))
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@hosyou_kikan", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("hosyou_kikan").ToString.Trim)))
            paramList.Add(MakeParam("@hosyousyo_hak_umu", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("hosyousyo_hak_umu").ToString.Trim)))
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@nyuukin_kakunin_jyouken", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("nyuukin_kakunin_jyouken").ToString.Trim)))
            paramList.Add(MakeParam("@nyuukin_kakunin_oboegaki", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item("nyuukin_kakunin_oboegaki").ToString.Trim)))
            paramList.Add(MakeParam("@tys_mitsyo_flg", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("tys_mitsyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@hattyuusyo_flg", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("hattyuusyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item("yuubin_no").ToString.Trim)))
            paramList.Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("jyuusyo1").ToString.Trim)))
            paramList.Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 30, InsObj(dtError.Rows(i).Item("jyuusyo2").ToString.Trim)))
            paramList.Add(MakeParam("@syozaichi_cd", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("jyuusyo_cd").ToString.Trim)))
            paramList.Add(MakeParam("@syozaichi_mei", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item("jyuusyo_mei").ToString.Trim)))
            paramList.Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 50, InsObj(dtError.Rows(i).Item("busyo_mei").ToString.Trim)))
            paramList.Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 20, InsObj(dtError.Rows(i).Item("daihyousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 16, InsObj(dtError.Rows(i).Item("tel_no").ToString.Trim)))
            paramList.Add(MakeParam("@fax_no", SqlDbType.VarChar, 16, InsObj(dtError.Rows(i).Item("fax_no").ToString.Trim)))
            paramList.Add(MakeParam("@mail_address", SqlDbType.VarChar, 64, InsObj(dtError.Rows(i).Item("mail_address").ToString.Trim)))
            paramList.Add(MakeParam("@bikou1", SqlDbType.VarChar, 30, InsObj(dtError.Rows(i).Item("bikou1").ToString.Trim)))
            paramList.Add(MakeParam("@bikou2", SqlDbType.VarChar, 30, InsObj(dtError.Rows(i).Item("bikou2").ToString.Trim)))
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@add_date", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("add_date").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_umu", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("seikyuu_umu").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item("syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_mei", SqlDbType.VarChar, 40, Me.GetSyouhinMei(dtError.Rows(i).Item("syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@uri_gaku", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item("uri_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@koumuten_seikyuu_gaku", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item("koumuten_seikyuu_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuusyo_hak_date", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("seikyuusyo_hak_date").ToString.Trim)))
            paramList.Add(MakeParam("@uri_date", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("uri_date").ToString.Trim)))
            paramList.Add(MakeParam("@bikou", SqlDbType.VarChar, 30, InsObj(dtError.Rows(i).Item("bikou").ToString.Trim)))
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            '==========2012/05/09 �ԗ� 407553�̑Ή� �C����======================
            paramList.Add(MakeParam("@kameiten_upd_datetime", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("kameiten_upd_datetime").ToString.Trim)))
            paramList.Add(MakeParam("@tatouwari_upd_datetime1", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("tatouwari_upd_datetime_1").ToString.Trim)))
            paramList.Add(MakeParam("@tatouwari_upd_datetime2", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("tatouwari_upd_datetime_2").ToString.Trim)))
            paramList.Add(MakeParam("@tatouwari_upd_datetime3", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("tatouwari_upd_datetime_3").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_jyuusyo_upd_datetime", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("kameiten_jyuusyo_upd_datetime").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu1", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu1").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu1_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu1_mei").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu2", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu2").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu2_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu2_mei").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu3", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu3").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu3_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu3_mei").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu4", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu4").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu4_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu4_mei").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu5", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu5").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu5_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu5_mei").ToString.Trim)))
            'paramList.Add(MakeParam("@naiyou1", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou1").ToString.Trim)))
            'paramList.Add(MakeParam("@naiyou2", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou2").ToString.Trim)))
            'paramList.Add(MakeParam("@naiyou3", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou3").ToString.Trim)))
            'paramList.Add(MakeParam("@naiyou4", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou4").ToString.Trim)))
            'paramList.Add(MakeParam("@naiyou5", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou5").ToString.Trim)))
            'paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtError.Rows(i).Item("add_login_user_id").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu1", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu1").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu1_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu1").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu2", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu2").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu2_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu2").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu3", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu3").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu3_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu3").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu4", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu4").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu4_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu4").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu5", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu5").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu5_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu5").ToString.Trim)))
            paramList.Add(MakeParam("@naiyou1", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou1").ToString.Trim)))
            paramList.Add(MakeParam("@naiyou2", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou2").ToString.Trim)))
            paramList.Add(MakeParam("@naiyou3", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou3").ToString.Trim)))
            paramList.Add(MakeParam("@naiyou4", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou4").ToString.Trim)))
            paramList.Add(MakeParam("@naiyou5", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou5").ToString.Trim)))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtError.Rows(i).Item("add_login_user_id").ToString.Trim)))

            '==========2012/05/09 �ԗ� 407553�̑Ή� �C����======================


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

    ''' <summary>
    ''' �ǉ�_���l��ʖ��擾
    ''' </summary>
    ''' <param name="strBikouSyubetu">�ǉ�_���l���</param>
    ''' <returns>�ǉ�_���l��ʖ�</returns>
    Public Function GetBikouMeisyou(ByVal strBikouSyubetu As String) As Object
        Dim intCode As Integer
        Try
            intCode = CInt(strBikouSyubetu)
            Dim strSql As String = "SELECT meisyou FROM m_meisyou  WITH(READCOMMITTED) WHERE meisyou_syubetu='09' and  code='" & strBikouSyubetu & "'"
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return DBNull.Value
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try
    End Function

    ''' <summary>
    ''' �r���_�[���擾
    ''' </summary>
    ''' <param name="strBuildNo">�r���_�[No</param>
    ''' <returns>�r���_�[��</returns>
    Public Function GetBuildMei(ByVal strBuildNo As String) As Object
        Try
            Dim strSql As String = "SELECT kameiten_mei1 FROM m_kameiten  WITH(READCOMMITTED) WHERE kameiten_cd = '" & strBuildNo & "'"
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return DBNull.Value
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try
    End Function

    ''' <summary>
    ''' �c�Ə����擾
    ''' </summary>
    ''' <param name="strCode">�c�Ə��R�[�h</param>
    ''' <returns>�c�Ə���</returns>
    Public Function GetEigyousyoMei(ByVal strCode As String) As Object
        Try
            Dim strSql As String = "SELECT eigyousyo_mei FROM m_eigyousyo  WITH(READCOMMITTED) WHERE eigyousyo_cd = '" & strCode & "'"
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return DBNull.Value
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try
    End Function

    ''' <summary>
    ''' �s���{�����擾
    ''' </summary>
    ''' <param name="strCode">�s���{���R�[�h</param>
    ''' <returns>�s���{����</returns>
    Public Function GetTodouhukenMei(ByVal strCode As String) As Object
        Try
            Dim strSql As String = "SELECT todouhuken_mei FROM m_todoufuken  WITH(READCOMMITTED) WHERE todouhuken_cd = '" & strCode & "'"
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return DBNull.Value
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try
    End Function

    ''' <summary>
    ''' �c�ƒS���Җ��擾
    ''' </summary>
    ''' <param name="strCode">�R�[�h</param>
    ''' <returns>�c�ƒS���Җ�</returns>
    Public Function GetSimei(ByVal strCode As String) As Object
        Try
            Dim strSql As String = "SELECT simei FROM m_account  WITH(READCOMMITTED) WHERE account= '" & strCode & "'"
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return DBNull.Value
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try
    End Function

    ''' <summary>
    ''' ���i���擾
    ''' </summary>
    ''' <param name="strCode">���i�R�[�h</param>
    ''' <returns>���i��</returns>
    Public Function GetSyouhinMei(ByVal strCode As String) As Object
        Try
            Dim strSql As String = "SELECT syouhin_mei FROM m_syouhin  WITH(READCOMMITTED) WHERE syouhin_cd= '" & strCode & "'"
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return DBNull.Value
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try
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
            .AppendLine("	,8 ")
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

    ''' <summary>�����X�}�X�^�Ƀf�[�^���X�V</summary>
    ''' <history>2012/02/12�@��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function UpdKameiten(ByVal dtOk As Data.DataTable) As Boolean
        '�߂�l
        Dim InsUpdCount As Integer = 0

        '�X�V�psql��
        Dim strSqlUpd As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_kameiten WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	kbn = @kbn")
            .AppendLine("	,torikesi = @torikesi")
            .AppendLine("	,hattyuu_teisi_flg = @hattyuu_teisi_flg")
            .AppendLine("	,kameiten_mei1 = @kameiten_mei1")
            .AppendLine("	,tenmei_kana1 = @tenmei_kana1")
            .AppendLine("	,kameiten_mei2 = @kameiten_mei2")
            .AppendLine("	,tenmei_kana2 = @tenmei_kana2")
            .AppendLine("	,kameiten_seisiki_mei = @kameiten_seisiki_mei")
            .AppendLine("	,kameiten_seisiki_mei_kana = @kameiten_seisiki_mei_kana")
            .AppendLine("	,eigyousyo_cd = @eigyousyo_cd")
            .AppendLine("	,keiretu_cd = @keiretu_cd")
            .AppendLine("	,tys_seikyuu_saki_cd = @tys_seikyuu_saki_cd")
            .AppendLine("	,tys_seikyuu_saki_brc = @tys_seikyuu_saki_brc")
            .AppendLine("	,tys_seikyuu_saki_kbn = @tys_seikyuu_saki_kbn")
            .AppendLine("	,koj_seikyuu_saki_cd = @koj_seikyuu_saki_cd")
            .AppendLine("	,koj_seikyuu_saki_brc = @koj_seikyuu_saki_brc")
            .AppendLine("	,koj_seikyuu_saki_kbn = @koj_seikyuu_saki_kbn")
            .AppendLine("	,hansokuhin_seikyuu_saki_cd = @hansokuhin_seikyuu_saki_cd")
            .AppendLine("	,hansokuhin_seikyuu_saki_brc = @hansokuhin_seikyuu_saki_brc")
            .AppendLine("	,hansokuhin_seikyuu_saki_kbn = @hansokuhin_seikyuu_saki_kbn")
            .AppendLine("	,tatemono_seikyuu_saki_cd = @tatemono_seikyuu_saki_cd")
            .AppendLine("	,tatemono_seikyuu_saki_brc = @tatemono_seikyuu_saki_brc")
            .AppendLine("	,tatemono_seikyuu_saki_kbn = @tatemono_seikyuu_saki_kbn")
            .AppendLine("	,seikyuu_saki_cd5 = @seikyuu_saki_cd5")
            .AppendLine("	,seikyuu_saki_brc5 = @seikyuu_saki_brc5")
            .AppendLine("	,seikyuu_saki_kbn5 = @seikyuu_saki_kbn5")
            .AppendLine("	,seikyuu_saki_cd6 = @seikyuu_saki_cd6")
            .AppendLine("	,seikyuu_saki_brc6 = @seikyuu_saki_brc6")
            .AppendLine("	,seikyuu_saki_kbn6 = @seikyuu_saki_kbn6")
            .AppendLine("	,seikyuu_saki_cd7 = @seikyuu_saki_cd7")
            .AppendLine("	,seikyuu_saki_brc7 = @seikyuu_saki_brc7")
            .AppendLine("	,seikyuu_saki_kbn7 = @seikyuu_saki_kbn7")
            .AppendLine("	,kaiyaku_haraimodosi_kkk = @kaiyaku_haraimodosi_kkk")
            .AppendLine("	,todouhuken_cd = @todouhuken_cd")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,hosyou_kikan = @hosyou_kikan") '--�ۏ؊���
            .AppendLine("	,hosyousyo_hak_umu = @hosyousyo_hak_umu") '--�ۏ؏����s�L��
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,builder_no = @builder_no")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,nenkan_tousuu = @nenkan_tousuu") '--�N�ԓ���
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,nyuukin_kakunin_jyouken = @nyuukin_kakunin_jyouken")
            .AppendLine("	,nyuukin_kakunin_oboegaki = @nyuukin_kakunin_oboegaki")
            .AppendLine("	,eigyou_tantousya_mei = @eigyou_tantousya_mei")
            .AppendLine("	,tys_mitsyo_flg = @tys_mitsyo_flg")
            .AppendLine("	,hattyuusyo_flg = @hattyuusyo_flg")
            .AppendLine("	,kyuu_eigyou_tantousya_mei = @kyuu_eigyou_tantousya_mei")
            .AppendLine("	,fuho_syoumeisyo_flg = @fuho_syoumeisyo_flg")
            .AppendLine("	,fuho_syoumeisyo_kaisi_nengetu = @fuho_syoumeisyo_kaisi_nengetu")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,jiosaki_flg = @jiosaki_flg") '--JIO��FLG
            .AppendLine("	,koj_uri_syubetsu = @koj_uri_syubetsu") '--�H��������
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,upd_login_user_id = @upd_login_user_id")
            .AppendLine("	,upd_datetime = GETDATE()")


            .AppendLine(",ssgr_kkk=@ssgr_kkk") 'SSGR���i
            .AppendLine(",kaiseki_hosyou_kkk=@kaiseki_hosyou_kkk") '��͕ۏ؉��i
            .AppendLine(",koj_mitiraisyo_soufu_fuyou=@koj_mitiraisyo_soufu_fuyou") '�H�����ψ˗������t�s�v
            .AppendLine(",hikiwatasi_inji_umu=@hikiwatasi_inji_umu") '�ۏ؏����n���󎚗L��
            .AppendLine(",hosyousyo_hassou_umu=@hosyousyo_hassou_umu") '�ۏ؏��������@
            .AppendLine(",ekijyouka_tokuyaku_kakaku=@ekijyouka_tokuyaku_kakaku") '�t�󉻓����
            .AppendLine(",hosyousyo_hassou_umu_start_date=@hosyousyo_hassou_umu_start_date") '�ۏ؏��������@_�K�p�J�n��
            .AppendLine(",taiou_syouhin_kbn=@taiou_syouhin_kbn") '�Ή����i�敪
            .AppendLine(",taiou_syouhin_kbn_set_date=@taiou_syouhin_kbn_set_date") '�Ή����i�敪�ݒ��
            .AppendLine(",campaign_waribiki_flg=@campaign_waribiki_flg") '�L�����y�[������FLG
            .AppendLine(",campaign_waribiki_set_date=@campaign_waribiki_set_date") '�L�����y�[�������ݒ��
            .AppendLine(",online_waribiki_flg=@online_waribiki_flg") '�I�����C������FLG
            .AppendLine(",b_str_yuuryou_wide_flg=@b_str_yuuryou_wide_flg") 'B-STR�L�����C�hFLG



            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
        End With

        Dim strKameiten As String
        Dim strUserId As String

        For i As Integer = 0 To dtOk.Rows.Count - 1
            '�p�����[�^�̐ݒ�
            paramList.Clear()
            paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("kbn").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("torikesi").ToString.Trim)))
            paramList.Add(MakeParam("@hattyuu_teisi_flg", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("hattyuu_teisi_flg").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("kameiten_mei1").ToString.Trim)))
            paramList.Add(MakeParam("@tenmei_kana1", SqlDbType.VarChar, 20, InsObj(dtOk.Rows(i).Item("tenmei_kana1").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("kameiten_mei2").ToString.Trim)))
            paramList.Add(MakeParam("@tenmei_kana2", SqlDbType.VarChar, 20, InsObj(dtOk.Rows(i).Item("tenmei_kana2").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_seisiki_mei", SqlDbType.VarChar, 80, InsObj(dtOk.Rows(i).Item("kameiten_seisiki_mei").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_seisiki_mei_kana", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("kameiten_seisiki_mei_kana").ToString.Trim)))
            paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("eigyousyo_cd").ToString.Trim)))
            paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("keiretu_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("tys_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("tys_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("tys_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("koj_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("koj_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("koj_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("hansokuhin_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("hansokuhin_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("hansokuhin_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("tatemono_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("tatemono_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("tatemono_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn5", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("seikyuu_saki_kbn5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd5", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("seikyuu_saki_cd5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc5", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("seikyuu_saki_brc5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn6", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("seikyuu_saki_kbn6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd6", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("seikyuu_saki_cd6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc6", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("seikyuu_saki_brc6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn7", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("seikyuu_saki_kbn7").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd7", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("seikyuu_saki_cd7").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc7", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("seikyuu_saki_brc7").ToString.Trim)))
            paramList.Add(MakeParam("@kaiyaku_haraimodosi_kkk", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("kaiyaku_haraimodosi_kkk").ToString.Trim)))
            paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("todouhuken_cd").ToString.Trim)))
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@hosyou_kikan", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hosyou_kikan").ToString.Trim))) '--�ۏ؊���
            paramList.Add(MakeParam("@hosyousyo_hak_umu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hosyousyo_hak_umu").ToString.Trim))) '--�ۏ؏����s�L��
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@builder_no", SqlDbType.VarChar, 9, InsObj(dtOk.Rows(i).Item("builder_no").ToString.Trim)))
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@nenkan_tousuu", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("nenkan_tousuu").ToString.Trim))) '--�N�ԓ���
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@nyuukin_kakunin_jyouken", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("nyuukin_kakunin_jyouken").ToString.Trim)))
            paramList.Add(MakeParam("@nyuukin_kakunin_oboegaki", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("nyuukin_kakunin_oboegaki").ToString.Trim)))
            paramList.Add(MakeParam("@eigyou_tantousya_mei", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("eigyou_tantousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@tys_mitsyo_flg", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("tys_mitsyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@hattyuusyo_flg", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("hattyuusyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@kyuu_eigyou_tantousya_mei", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("kyuu_eigyou_tantousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@fuho_syoumeisyo_flg", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("fuho_syoumeisyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@fuho_syoumeisyo_kaisi_nengetu", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("fuho_syoumeisyo_kaisi_nengetu").ToString.Trim)))
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@jiosaki_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("jiosaki_flg").ToString.Trim))) '--JIO��FLG
            paramList.Add(MakeParam("@koj_uri_syubetsu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("koj_uri_syubetsu").ToString.Trim))) '--�H��������
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))


            paramList.Add(MakeParam("@ssgr_kkk", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("ssgr_kkk").ToString.Trim))) 'SSGR���i
            paramList.Add(MakeParam("@kaiseki_hosyou_kkk", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("kaiseki_hosyou_kkk").ToString.Trim))) '��͕ۏ؉��i
            paramList.Add(MakeParam("@koj_mitiraisyo_soufu_fuyou", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("koj_mitiraisyo_soufu_fuyou").ToString.Trim))) '�H�����ψ˗������t�s�v
            paramList.Add(MakeParam("@hikiwatasi_inji_umu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hikiwatasi_inji_umu").ToString.Trim))) '�ۏ؏����n���󎚗L��
            paramList.Add(MakeParam("@hosyousyo_hassou_umu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hosyousyo_hassou_umu").ToString.Trim))) '�ۏ؏��������@
            paramList.Add(MakeParam("@ekijyouka_tokuyaku_kakaku", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("ekijyouka_tokuyaku_kakaku").ToString.Trim))) '�t�󉻓����
            paramList.Add(MakeParam("@hosyousyo_hassou_umu_start_date", SqlDbType.DateTime, 20, InsObj(dtOk.Rows(i).Item("hosyousyo_hassou_umu_start_date").ToString.Trim))) '�ۏ؏��������@_�K�p�J�n��
            paramList.Add(MakeParam("@taiou_syouhin_kbn", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("taiou_syouhin_kbn").ToString.Trim))) '�Ή����i�敪
            paramList.Add(MakeParam("@taiou_syouhin_kbn_set_date", SqlDbType.DateTime, 20, InsObj(dtOk.Rows(i).Item("taiou_syouhin_kbn_set_date").ToString.Trim))) '�Ή����i�敪�ݒ��
            paramList.Add(MakeParam("@campaign_waribiki_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("campaign_waribiki_flg").ToString.Trim))) '�L�����y�[������FLG
            paramList.Add(MakeParam("@campaign_waribiki_set_date", SqlDbType.DateTime, 20, InsObj(dtOk.Rows(i).Item("campaign_waribiki_set_date").ToString.Trim))) '�L�����y�[�������ݒ��
            paramList.Add(MakeParam("@online_waribiki_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("online_waribiki_flg").ToString.Trim))) '�I�����C������FLG
            paramList.Add(MakeParam("@b_str_yuuryou_wide_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("b_str_yuuryou_wide_flg").ToString.Trim))) 'B-STR�L�����C�hFLG



            Try

                '�X�V
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)

                If Not (InsUpdCount > 0) Then
                    Return False
                End If

                strKameiten = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim
                strUserId = dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim

                If Not InsUpdKameitenRenkei(strKameiten, strUserId) Then
                    Throw New ApplicationException
                End If

            Catch ex As Exception
                Return False
            End Try

        Next

        Return True
    End Function

    ''' <summary>�����X�}�X�^�Ƀf�[�^���X�V</summary>
    ''' <history>2012/02/12�@��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function InsKameiten(ByVal dtOk As Data.DataTable) As Boolean
        '�߂�l
        Dim InsUpdCount As Integer = 0

        '�X�V�psql��
        Dim strSqlIns As New System.Text.StringBuilder
        Dim strSqlUpd As New System.Text.StringBuilder
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With strSqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kameiten WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("	kbn ")
            .AppendLine("	,kameiten_cd ")
            .AppendLine("	,torikesi ")
            .AppendLine("	,hattyuu_teisi_flg ")
            .AppendLine("	,kameiten_mei1 ")
            .AppendLine("	,tenmei_kana1 ")
            .AppendLine("	,kameiten_mei2 ")
            .AppendLine("	,tenmei_kana2 ")
            .AppendLine("	,kameiten_seisiki_mei ")
            .AppendLine("	,kameiten_seisiki_mei_kana ")
            .AppendLine("	,eigyousyo_cd ")
            .AppendLine("	,keiretu_cd ")
            .AppendLine("	,tys_seikyuu_saki_cd ")
            .AppendLine("	,tys_seikyuu_saki_brc ")
            .AppendLine("	,tys_seikyuu_saki_kbn ")
            .AppendLine("	,koj_seikyuu_saki_cd ")
            .AppendLine("	,koj_seikyuu_saki_brc ")
            .AppendLine("	,koj_seikyuu_saki_kbn ")
            .AppendLine("	,hansokuhin_seikyuu_saki_cd ")
            .AppendLine("	,hansokuhin_seikyuu_saki_brc ")
            .AppendLine("	,hansokuhin_seikyuu_saki_kbn ")
            .AppendLine("	,tatemono_seikyuu_saki_cd ")
            .AppendLine("	,tatemono_seikyuu_saki_brc ")
            .AppendLine("	,tatemono_seikyuu_saki_kbn ")
            .AppendLine("	,seikyuu_saki_cd5 ")
            .AppendLine("	,seikyuu_saki_brc5 ")
            .AppendLine("	,seikyuu_saki_kbn5 ")
            .AppendLine("	,seikyuu_saki_cd6 ")
            .AppendLine("	,seikyuu_saki_brc6 ")
            .AppendLine("	,seikyuu_saki_kbn6 ")
            .AppendLine("	,seikyuu_saki_cd7 ")
            .AppendLine("	,seikyuu_saki_brc7 ")
            .AppendLine("	,seikyuu_saki_kbn7 ")
            .AppendLine("	,kaiyaku_haraimodosi_kkk ")
            .AppendLine("	,todouhuken_cd ")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,hosyou_kikan ") '--�ۏ؊���
            .AppendLine("	,hosyousyo_hak_umu ") '--�ۏ؏����s�L��
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,builder_no ")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,nenkan_tousuu ") '--�N�ԓ���
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,nyuukin_kakunin_jyouken ")
            .AppendLine("	,nyuukin_kakunin_oboegaki ")
            .AppendLine("	,eigyou_tantousya_mei ")
            .AppendLine("	,tys_mitsyo_flg ")
            .AppendLine("	,hattyuusyo_flg ")
            .AppendLine("	,kyuu_eigyou_tantousya_mei ")
            .AppendLine("	,fuho_syoumeisyo_flg ")
            .AppendLine("	,fuho_syoumeisyo_kaisi_nengetu ")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,jiosaki_flg ") '--JIO��FLG
            .AppendLine("	,koj_uri_syubetsu ") '--�H��������
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,add_login_user_id ")
            .AppendLine("	,add_datetime ")

            .AppendLine(",ssgr_kkk")
            .AppendLine(",kaiseki_hosyou_kkk")
            .AppendLine(",koj_mitiraisyo_soufu_fuyou")
            .AppendLine(",hikiwatasi_inji_umu")
            .AppendLine(",hosyousyo_hassou_umu")
            .AppendLine(",ekijyouka_tokuyaku_kakaku")
            .AppendLine(",hosyousyo_hassou_umu_start_date")
            .AppendLine(",taiou_syouhin_kbn")
            .AppendLine(",taiou_syouhin_kbn_set_date")
            .AppendLine(",campaign_waribiki_flg")
            .AppendLine(",campaign_waribiki_set_date")
            .AppendLine(",online_waribiki_flg")
            .AppendLine(",b_str_yuuryou_wide_flg")

            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine("	 @kbn")
            .AppendLine("	, @kameiten_cd")
            .AppendLine("	, @torikesi")
            .AppendLine("	, @hattyuu_teisi_flg")
            .AppendLine("	, @kameiten_mei1")
            .AppendLine("	, @tenmei_kana1")
            .AppendLine("	, @kameiten_mei2")
            .AppendLine("	, @tenmei_kana2")
            .AppendLine("	, @kameiten_seisiki_mei")
            .AppendLine("	, @kameiten_seisiki_mei_kana")
            .AppendLine("	, @eigyousyo_cd")
            .AppendLine("	, @keiretu_cd")
            .AppendLine("	, @tys_seikyuu_saki_cd")
            .AppendLine("	, @tys_seikyuu_saki_brc")
            .AppendLine("	, @tys_seikyuu_saki_kbn")
            .AppendLine("	, @koj_seikyuu_saki_cd")
            .AppendLine("	, @koj_seikyuu_saki_brc")
            .AppendLine("	, @koj_seikyuu_saki_kbn")
            .AppendLine("	, @hansokuhin_seikyuu_saki_cd")
            .AppendLine("	, @hansokuhin_seikyuu_saki_brc")
            .AppendLine("	, @hansokuhin_seikyuu_saki_kbn")
            .AppendLine("	, @tatemono_seikyuu_saki_cd")
            .AppendLine("	, @tatemono_seikyuu_saki_brc")
            .AppendLine("	, @tatemono_seikyuu_saki_kbn")
            .AppendLine("	, @seikyuu_saki_cd5")
            .AppendLine("	, @seikyuu_saki_brc5")
            .AppendLine("	, @seikyuu_saki_kbn5")
            .AppendLine("	, @seikyuu_saki_cd6")
            .AppendLine("	, @seikyuu_saki_brc6")
            .AppendLine("	, @seikyuu_saki_kbn6")
            .AppendLine("	, @seikyuu_saki_cd7")
            .AppendLine("	, @seikyuu_saki_brc7")
            .AppendLine("	, @seikyuu_saki_kbn7")
            .AppendLine("	, @kaiyaku_haraimodosi_kkk")
            .AppendLine("	, @todouhuken_cd")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	, @hosyou_kikan ") '--�ۏ؊���
            .AppendLine("	, @hosyousyo_hak_umu ") '--�ۏ؏����s�L��
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	, @builder_no")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	, @nenkan_tousuu ") '--�N�ԓ���
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	, @nyuukin_kakunin_jyouken")
            .AppendLine("	, @nyuukin_kakunin_oboegaki")
            .AppendLine("	, @eigyou_tantousya_mei")
            .AppendLine("	, @tys_mitsyo_flg")
            .AppendLine("	, @hattyuusyo_flg")
            .AppendLine("	, @kyuu_eigyou_tantousya_mei")
            .AppendLine("	, @fuho_syoumeisyo_flg")
            .AppendLine("	, @fuho_syoumeisyo_kaisi_nengetu")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	, @jiosaki_flg ") '--JIO��FLG
            .AppendLine("	, @koj_uri_syubetsu ") '--�H��������
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	, @upd_login_user_id")
            .AppendLine("	 , GETDATE()")
            .AppendLine(",@ssgr_kkk")
            .AppendLine(",@kaiseki_hosyou_kkk")
            .AppendLine(",@koj_mitiraisyo_soufu_fuyou")
            .AppendLine(",@hikiwatasi_inji_umu")
            .AppendLine(",@hosyousyo_hassou_umu")
            .AppendLine(",@ekijyouka_tokuyaku_kakaku")
            .AppendLine(",@hosyousyo_hassou_umu_start_date")
            .AppendLine(",@taiou_syouhin_kbn")
            .AppendLine(",@taiou_syouhin_kbn_set_date")
            .AppendLine(",@campaign_waribiki_flg")
            .AppendLine(",@campaign_waribiki_set_date")
            .AppendLine(",@online_waribiki_flg")
            .AppendLine(",@b_str_yuuryou_wide_flg")
            .AppendLine("	) ")
        End With
        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_kameiten WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	kbn = @kbn")
            .AppendLine("	,torikesi = @torikesi")
            .AppendLine("	,hattyuu_teisi_flg = @hattyuu_teisi_flg")
            .AppendLine("	,kameiten_mei1 = @kameiten_mei1")
            .AppendLine("	,tenmei_kana1 = @tenmei_kana1")
            .AppendLine("	,kameiten_mei2 = @kameiten_mei2")
            .AppendLine("	,tenmei_kana2 = @tenmei_kana2")
            .AppendLine("	,kameiten_seisiki_mei = @kameiten_seisiki_mei")
            .AppendLine("	,kameiten_seisiki_mei_kana = @kameiten_seisiki_mei_kana")
            .AppendLine("	,eigyousyo_cd = @eigyousyo_cd")
            .AppendLine("	,keiretu_cd = @keiretu_cd")
            .AppendLine("	,tys_seikyuu_saki_cd = @tys_seikyuu_saki_cd")
            .AppendLine("	,tys_seikyuu_saki_brc = @tys_seikyuu_saki_brc")
            .AppendLine("	,tys_seikyuu_saki_kbn = @tys_seikyuu_saki_kbn")
            .AppendLine("	,koj_seikyuu_saki_cd = @koj_seikyuu_saki_cd")
            .AppendLine("	,koj_seikyuu_saki_brc = @koj_seikyuu_saki_brc")
            .AppendLine("	,koj_seikyuu_saki_kbn = @koj_seikyuu_saki_kbn")
            .AppendLine("	,hansokuhin_seikyuu_saki_cd = @hansokuhin_seikyuu_saki_cd")
            .AppendLine("	,hansokuhin_seikyuu_saki_brc = @hansokuhin_seikyuu_saki_brc")
            .AppendLine("	,hansokuhin_seikyuu_saki_kbn = @hansokuhin_seikyuu_saki_kbn")
            .AppendLine("	,tatemono_seikyuu_saki_cd = @tatemono_seikyuu_saki_cd")
            .AppendLine("	,tatemono_seikyuu_saki_brc = @tatemono_seikyuu_saki_brc")
            .AppendLine("	,tatemono_seikyuu_saki_kbn = @tatemono_seikyuu_saki_kbn")
            .AppendLine("	,seikyuu_saki_cd5 = @seikyuu_saki_cd5")
            .AppendLine("	,seikyuu_saki_brc5 = @seikyuu_saki_brc5")
            .AppendLine("	,seikyuu_saki_kbn5 = @seikyuu_saki_kbn5")
            .AppendLine("	,seikyuu_saki_cd6 = @seikyuu_saki_cd6")
            .AppendLine("	,seikyuu_saki_brc6 = @seikyuu_saki_brc6")
            .AppendLine("	,seikyuu_saki_kbn6 = @seikyuu_saki_kbn6")
            .AppendLine("	,seikyuu_saki_cd7 = @seikyuu_saki_cd7")
            .AppendLine("	,seikyuu_saki_brc7 = @seikyuu_saki_brc7")
            .AppendLine("	,seikyuu_saki_kbn7 = @seikyuu_saki_kbn7")
            .AppendLine("	,kaiyaku_haraimodosi_kkk = @kaiyaku_haraimodosi_kkk")
            .AppendLine("	,todouhuken_cd = @todouhuken_cd")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,hosyou_kikan = @hosyou_kikan") '--�ۏ؊���
            .AppendLine("	,hosyousyo_hak_umu = @hosyousyo_hak_umu") '--�ۏ؏����s�L��
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,builder_no = @builder_no")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,nenkan_tousuu = @nenkan_tousuu ") '--�N�ԓ���
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,nyuukin_kakunin_jyouken = @nyuukin_kakunin_jyouken")
            .AppendLine("	,nyuukin_kakunin_oboegaki = @nyuukin_kakunin_oboegaki")
            .AppendLine("	,eigyou_tantousya_mei = @eigyou_tantousya_mei")
            .AppendLine("	,tys_mitsyo_flg = @tys_mitsyo_flg")
            .AppendLine("	,hattyuusyo_flg = @hattyuusyo_flg")
            .AppendLine("	,kyuu_eigyou_tantousya_mei = @kyuu_eigyou_tantousya_mei")
            .AppendLine("	,fuho_syoumeisyo_flg = @fuho_syoumeisyo_flg")
            .AppendLine("	,fuho_syoumeisyo_kaisi_nengetu = @fuho_syoumeisyo_kaisi_nengetu")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,jiosaki_flg = @jiosaki_flg ") '--JIO��FLG
            .AppendLine("	,koj_uri_syubetsu = @koj_uri_syubetsu ") '--�H��������
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("	,upd_login_user_id = @upd_login_user_id")
            .AppendLine("	,upd_datetime = GETDATE()")

            .AppendLine(",ssgr_kkk=@ssgr_kkk") 'SSGR���i
            .AppendLine(",kaiseki_hosyou_kkk=@kaiseki_hosyou_kkk") '��͕ۏ؉��i
            .AppendLine(",koj_mitiraisyo_soufu_fuyou=@koj_mitiraisyo_soufu_fuyou") '�H�����ψ˗������t�s�v
            .AppendLine(",hikiwatasi_inji_umu=@hikiwatasi_inji_umu") '�ۏ؏����n���󎚗L��
            .AppendLine(",hosyousyo_hassou_umu=@hosyousyo_hassou_umu") '�ۏ؏��������@
            .AppendLine(",ekijyouka_tokuyaku_kakaku=@ekijyouka_tokuyaku_kakaku") '�t�󉻓����
            .AppendLine(",hosyousyo_hassou_umu_start_date=@hosyousyo_hassou_umu_start_date") '�ۏ؏��������@_�K�p�J�n��
            .AppendLine(",taiou_syouhin_kbn=@taiou_syouhin_kbn") '�Ή����i�敪
            .AppendLine(",taiou_syouhin_kbn_set_date=@taiou_syouhin_kbn_set_date") '�Ή����i�敪�ݒ��
            .AppendLine(",campaign_waribiki_flg=@campaign_waribiki_flg") '�L�����y�[������FLG
            .AppendLine(",campaign_waribiki_set_date=@campaign_waribiki_set_date") '�L�����y�[�������ݒ��
            .AppendLine(",online_waribiki_flg=@online_waribiki_flg") '�I�����C������FLG
            .AppendLine(",b_str_yuuryou_wide_flg=@b_str_yuuryou_wide_flg") 'B-STR�L�����C�hFLG


            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
        End With

        Dim strKameiten As String
        Dim strUserId As String

        For i As Integer = 0 To dtOk.Rows.Count - 1
            '�p�����[�^�̐ݒ�
            paramList.Clear()
            paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("kbn").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("torikesi").ToString.Trim)))
            paramList.Add(MakeParam("@hattyuu_teisi_flg", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("hattyuu_teisi_flg").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("kameiten_mei1").ToString.Trim)))
            paramList.Add(MakeParam("@tenmei_kana1", SqlDbType.VarChar, 20, InsObj(dtOk.Rows(i).Item("tenmei_kana1").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("kameiten_mei2").ToString.Trim)))
            paramList.Add(MakeParam("@tenmei_kana2", SqlDbType.VarChar, 20, InsObj(dtOk.Rows(i).Item("tenmei_kana2").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_seisiki_mei", SqlDbType.VarChar, 80, InsObj(dtOk.Rows(i).Item("kameiten_seisiki_mei").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_seisiki_mei_kana", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("kameiten_seisiki_mei_kana").ToString.Trim)))
            paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("eigyousyo_cd").ToString.Trim)))
            paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("keiretu_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("tys_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("tys_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("tys_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("koj_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("koj_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("koj_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("hansokuhin_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("hansokuhin_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("hansokuhin_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("tatemono_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("tatemono_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("tatemono_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn5", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("seikyuu_saki_kbn5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd5", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("seikyuu_saki_cd5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc5", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("seikyuu_saki_brc5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn6", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("seikyuu_saki_kbn6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd6", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("seikyuu_saki_cd6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc6", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("seikyuu_saki_brc6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn7", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("seikyuu_saki_kbn7").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd7", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("seikyuu_saki_cd7").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc7", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("seikyuu_saki_brc7").ToString.Trim)))
            paramList.Add(MakeParam("@kaiyaku_haraimodosi_kkk", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("kaiyaku_haraimodosi_kkk").ToString.Trim)))
            paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("todouhuken_cd").ToString.Trim)))
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@hosyou_kikan", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hosyou_kikan").ToString.Trim))) '--�ۏ؊���
            paramList.Add(MakeParam("@hosyousyo_hak_umu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hosyousyo_hak_umu").ToString.Trim))) '--�ۏ؏����s�L��
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@builder_no", SqlDbType.VarChar, 9, InsObj(dtOk.Rows(i).Item("builder_no").ToString.Trim)))
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@nenkan_tousuu", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("nenkan_tousuu").ToString.Trim))) '--�N�ԓ���
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@nyuukin_kakunin_jyouken", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("nyuukin_kakunin_jyouken").ToString.Trim)))
            paramList.Add(MakeParam("@nyuukin_kakunin_oboegaki", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("nyuukin_kakunin_oboegaki").ToString.Trim)))
            paramList.Add(MakeParam("@eigyou_tantousya_mei", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("eigyou_tantousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@tys_mitsyo_flg", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("tys_mitsyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@hattyuusyo_flg", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("hattyuusyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@kyuu_eigyou_tantousya_mei", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("kyuu_eigyou_tantousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@fuho_syoumeisyo_flg", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("fuho_syoumeisyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@fuho_syoumeisyo_kaisi_nengetu", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("fuho_syoumeisyo_kaisi_nengetu").ToString.Trim)))
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@jiosaki_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("jiosaki_flg").ToString.Trim))) '--JIO��FLG
            paramList.Add(MakeParam("@koj_uri_syubetsu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("koj_uri_syubetsu").ToString.Trim))) '--�H��������
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))



            paramList.Add(MakeParam("@ssgr_kkk", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("ssgr_kkk").ToString.Trim))) 'SSGR���i
            paramList.Add(MakeParam("@kaiseki_hosyou_kkk", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("kaiseki_hosyou_kkk").ToString.Trim))) '��͕ۏ؉��i
            paramList.Add(MakeParam("@koj_mitiraisyo_soufu_fuyou", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("koj_mitiraisyo_soufu_fuyou").ToString.Trim))) '�H�����ψ˗������t�s�v
            paramList.Add(MakeParam("@hikiwatasi_inji_umu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hikiwatasi_inji_umu").ToString.Trim))) '�ۏ؏����n���󎚗L��
            paramList.Add(MakeParam("@hosyousyo_hassou_umu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hosyousyo_hassou_umu").ToString.Trim))) '�ۏ؏��������@
            paramList.Add(MakeParam("@ekijyouka_tokuyaku_kakaku", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("ekijyouka_tokuyaku_kakaku").ToString.Trim))) '�t�󉻓����
            paramList.Add(MakeParam("@hosyousyo_hassou_umu_start_date", SqlDbType.DateTime, 20, InsObj(dtOk.Rows(i).Item("hosyousyo_hassou_umu_start_date").ToString.Trim))) '�ۏ؏��������@_�K�p�J�n��
            paramList.Add(MakeParam("@taiou_syouhin_kbn", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("taiou_syouhin_kbn").ToString.Trim))) '�Ή����i�敪
            paramList.Add(MakeParam("@taiou_syouhin_kbn_set_date", SqlDbType.DateTime, 20, InsObj(dtOk.Rows(i).Item("taiou_syouhin_kbn_set_date").ToString.Trim))) '�Ή����i�敪�ݒ��
            paramList.Add(MakeParam("@campaign_waribiki_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("campaign_waribiki_flg").ToString.Trim))) '�L�����y�[������FLG
            paramList.Add(MakeParam("@campaign_waribiki_set_date", SqlDbType.DateTime, 20, InsObj(dtOk.Rows(i).Item("campaign_waribiki_set_date").ToString.Trim))) '�L�����y�[�������ݒ��
            paramList.Add(MakeParam("@online_waribiki_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("online_waribiki_flg").ToString.Trim))) '�I�����C������FLG
            paramList.Add(MakeParam("@b_str_yuuryou_wide_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("b_str_yuuryou_wide_flg").ToString.Trim))) 'B-STR�L�����C�hFLG


            Try

                strKameiten = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim
                strUserId = dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim

                '�X�V
                If SelKameitenCd(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim) Then
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)

                    If Not InsUpdKameitenRenkei(strKameiten, strUserId) Then
                        Throw New ApplicationException
                    End If
                Else
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

                    If Not InsUpdKameitenRenkei(strKameiten, strUserId) Then
                        Throw New ApplicationException
                    End If
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

    ''' <summary>�����X�Z���}�X�^�^�Ƀf�[�^���X�V</summary>
    ''' <history>2012/02/13�@��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function UpdKameitenJyuusyo(ByVal dtOk As Data.DataTable) As Boolean
        '�߂�l
        Dim InsUpdCount As Integer = 0

        '�X�V�psql��
        Dim strSqlUpd As New System.Text.StringBuilder
        Dim strSqlIns As New System.Text.StringBuilder
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_kameiten_jyuusyo WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	todouhuken_cd = @todouhuken_cd")
            .AppendLine("	,jyuusyo1 = @jyuusyo1")
            .AppendLine("	,jyuusyo2 = @jyuusyo2")
            .AppendLine("	,yuubin_no = @yuubin_no")
            .AppendLine("	,tel_no = @tel_no")
            .AppendLine("	,fax_no = @fax_no")
            .AppendLine("	,busyo_mei = @busyo_mei")
            .AppendLine("	,daihyousya_mei = @daihyousya_mei")
            .AppendLine("	,bikou1 = @bikou1")
            .AppendLine("	,bikou2 = @bikou2")
            .AppendLine("	,mail_address = @mail_address")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id")
            .AppendLine("	,upd_datetime = GETDATE()")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("	AND jyuusyo_no = '1' ")
        End With
        With strSqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kameiten_jyuusyo WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("kameiten_cd ")
            .AppendLine(",jyuusyo_no ")
            .AppendLine(",todouhuken_cd ")
            .AppendLine(",jyuusyo1 ")
            .AppendLine(", jyuusyo2 ")
            .AppendLine(", yuubin_no ")
            .AppendLine(", tel_no ")
            .AppendLine(", fax_no ")
            .AppendLine(", busyo_mei ")
            .AppendLine(", daihyousya_mei ")
            '==========��2013/03/08 �ԗ� 407584 �ǉ���======================
            .AppendLine(", add_nengetu ") '--�o�^�N��
            .AppendLine(", seikyuusyo_flg ") '--������FLG
            .AppendLine(", hosyousyo_flg ") '--�ۏ؏�FLG
            .AppendLine(", hkks_flg ") '--�񍐏�FLG
            .AppendLine(", teiki_kankou_flg ") '--������sFLG
            '==========��2013/03/08 �ԗ� 407584 �ǉ���======================
            .AppendLine(", bikou1 ")
            .AppendLine(", bikou2 ")
            .AppendLine(", mail_address ")
            '==========��2013/03/08 �ԗ� 407584 �ǉ���======================
            .AppendLine(", kasi_hosyousyo_flg ") '--���r�ۏ؏�FLG
            .AppendLine(", koj_hkks_flg ") '--�H���񍐏�FLG
            .AppendLine(", kensa_hkks_flg ") '--�����񍐏�FLG
            '==========��2013/03/08 �ԗ� 407584 �ǉ���======================
            .AppendLine(", add_login_user_id ")
            .AppendLine(", add_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine(" @kameiten_cd ")
            .AppendLine(",1 ")
            .AppendLine(", @todouhuken_cd ")
            .AppendLine(", @jyuusyo1 ")
            .AppendLine(", @jyuusyo2 ")
            .AppendLine(", @yuubin_no ")
            .AppendLine(", @tel_no ")
            .AppendLine(", @fax_no ")
            .AppendLine(", @busyo_mei ")
            .AppendLine(", @daihyousya_mei ")
            '==========��2013/03/08 �ԗ� 407584 �ǉ���======================
            .AppendLine(", LEFT(CONVERT(VARCHAR(10),GETDATE(),111),7) ") '--�o�^�N��
            .AppendLine(", '-1' ") '--������FLG
            .AppendLine(", '-1' ") '--�ۏ؏�FLG
            .AppendLine(", '-1' ") '--�񍐏�FLG
            .AppendLine(", '-1' ") '--������sFLG
            '==========��2013/03/08 �ԗ� 407584 �ǉ���======================
            .AppendLine(", @bikou1 ")
            .AppendLine(", @bikou2 ")
            .AppendLine(", @mail_address ")
            '==========��2013/03/08 �ԗ� 407584 �ǉ���======================
            .AppendLine(", '-1' ") '--���r�ۏ؏�FLG
            .AppendLine(", '-1' ") '--�H���񍐏�FLG
            .AppendLine(", '-1' ") '--�����񍐏�FLG
            '==========��2013/03/08 �ԗ� 407584 �ǉ���======================
            .AppendLine(", @upd_login_user_id")
            .AppendLine(", GETDATE()")
            .AppendLine("	) ")

        End With

        Dim strKameiten As String
        Dim strUserId As String

        For i As Integer = 0 To dtOk.Rows.Count - 1
            '�p�����[�^�̐ݒ�
            paramList.Clear()
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
            paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("todouhuken_cd").ToString.Trim)))
            paramList.Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("jyuusyo1").ToString.Trim)))
            paramList.Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("jyuusyo2").ToString.Trim)))
            paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 8, InsObj(dtOk.Rows(i).Item("yuubin_no").ToString.Trim)))
            paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("tel_no").ToString.Trim)))
            paramList.Add(MakeParam("@fax_no", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("fax_no").ToString.Trim)))
            paramList.Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 50, InsObj(dtOk.Rows(i).Item("busyo_mei").ToString.Trim)))
            paramList.Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 20, InsObj(dtOk.Rows(i).Item("daihyousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@bikou1", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("bikou1").ToString.Trim)))
            paramList.Add(MakeParam("@bikou2", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("bikou2").ToString.Trim)))
            paramList.Add(MakeParam("@mail_address", SqlDbType.VarChar, 64, InsObj(dtOk.Rows(i).Item("mail_address").ToString.Trim)))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))

            strKameiten = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim
            strUserId = dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim

            Try
                If SelKameitenJyuusyo(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim) Then
                    '�X�V
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)

                    If Not InsUpdKameitenJyuusyoRenkei(strKameiten, strUserId) Then
                        Throw New ApplicationException
                    End If
                Else
                    'INS
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

                    If Not InsUpdKameitenJyuusyoRenkei(strKameiten, strUserId) Then
                        Throw New ApplicationException
                    End If
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

    ''' <summary>�����X�Z���}�X�^�^�Ƀf�[�^���X�V</summary>
    ''' <history>2012/02/13�@��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function InsKameitenJyuusyo(ByVal dtOk As Data.DataTable) As Boolean
        '�߂�l
        Dim InsUpdCount As Integer = 0

        '�X�V�psql��
        Dim strSqlUpd As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With strSqlUpd
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kameiten_jyuusyo WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("kameiten_cd ")
            .AppendLine(",jyuusyo_no ")
            .AppendLine(",todouhuken_cd ")
            .AppendLine(",jyuusyo1 ")
            .AppendLine(", jyuusyo2 ")
            .AppendLine(", yuubin_no ")
            .AppendLine(", tel_no ")
            .AppendLine(", fax_no ")
            .AppendLine(", busyo_mei ")
            .AppendLine(", daihyousya_mei ")
            .AppendLine(", bikou1 ")
            .AppendLine(", bikou2 ")
            .AppendLine(", mail_address ")
            .AppendLine(", add_login_user_id ")
            .AppendLine(", add_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine(" @kameiten_cd ")
            .AppendLine(",1 ")
            .AppendLine(", @todouhuken_cd ")
            .AppendLine(", @jyuusyo1 ")
            .AppendLine(", @jyuusyo2 ")
            .AppendLine(", @yuubin_no ")
            .AppendLine(", @tel_no ")
            .AppendLine(", @fax_no ")
            .AppendLine(", @busyo_mei ")
            .AppendLine(", @daihyousya_mei ")
            .AppendLine(", @bikou1 ")
            .AppendLine(", @bikou2 ")
            .AppendLine(", @mail_address ")

            .AppendLine("	, @upd_login_user_id")
            .AppendLine("	 , GETDATE()")
            .AppendLine("	) ")

        End With

        Dim strKameiten As String
        Dim strUserId As String

        For i As Integer = 0 To dtOk.Rows.Count - 1
            '�p�����[�^�̐ݒ�
            paramList.Clear()
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
            paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("todouhuken_cd").ToString.Trim)))
            paramList.Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("jyuusyo1").ToString.Trim)))
            paramList.Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("jyuusyo2").ToString.Trim)))
            paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 8, InsObj(dtOk.Rows(i).Item("yuubin_no").ToString.Trim)))
            paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("tel_no").ToString.Trim)))
            paramList.Add(MakeParam("@fax_no", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("fax_no").ToString.Trim)))
            paramList.Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 50, InsObj(dtOk.Rows(i).Item("busyo_mei").ToString.Trim)))
            paramList.Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 20, InsObj(dtOk.Rows(i).Item("daihyousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@bikou1", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("bikou1").ToString.Trim)))
            paramList.Add(MakeParam("@bikou2", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("bikou2").ToString.Trim)))
            paramList.Add(MakeParam("@mail_address", SqlDbType.VarChar, 64, InsObj(dtOk.Rows(i).Item("mail_address").ToString.Trim)))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))
            Try

                strKameiten = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim
                strUserId = dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim

                '�X�V
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)

                If Not (InsUpdCount > 0) Then
                    Return False
                End If

                If Not InsUpdKameitenJyuusyoRenkei(strKameiten, strUserId) Then
                    Throw New ApplicationException
                End If

            Catch ex As Exception
                Return False
            End Try

        Next

        Return True
    End Function

    '=================2012/03/14 �ԗ� �G���[�̑Ή� �ǉ���===================================
    ''' <summary>���������ݒ�}�X�^�Ƀf�[�^�𑶍݃`�F�b�N����</summary>
    ''' <history>2012/03/14 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function CheckTatouwaribikiSettei(ByVal strKameitenCd As String, ByVal strToukubun As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�ǉ��pSQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(kameiten_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	m_tatouwaribiki_settei WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("	AND ")
            .AppendLine("	toukubun = @toukubun ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@toukubun", SqlDbType.Int, 1, strToukubun))
        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTatouwaribikiSettei", paramList.ToArray())

        '�߂�l
        If CInt(dsReturn.Tables(0).Rows(0).Item("cnt")) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    '=================2012/03/14 �ԗ� �G���[�̑Ή� �ǉ���===================================

    ''' <summary>���������ݒ�}�X�^�Ƀf�[�^��ǉ��ƍX�V</summary>
    ''' <history>2012/02/13�@��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function InsUpdTatouwaribikiSettei(ByVal dtOk As Data.DataTable) As Boolean
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
            .AppendLine("	m_tatouwaribiki_settei WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		kameiten_cd ")
            .AppendLine("		,toukubun ")
            .AppendLine("		,syouhin_cd ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine("		@kameiten_cd ")
            .AppendLine("		,@toukubun ")
            .AppendLine("		,@syouhin_cd ")
            .AppendLine("		,@add_login_user_id ")
            .AppendLine("		,GETDATE() ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine("	) ")
        End With

        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_tatouwaribiki_settei WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
            .AppendLine("	,upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("   AND ")
            .AppendLine("	toukubun = @toukubun ")
        End With

        Dim kameitenCd As String '�����X�R�[�h
        Dim syouhinCd1 As String '���敪1 ���i�R�[�h
        Dim syouhinCd2 As String '���敪2 ���i�R�[�h
        Dim syouhinCd3 As String '���敪3 ���i�R�[�h

        Dim strUserId As String  '���[�U�[ID

        For i As Integer = 0 To dtOk.Rows.Count - 1

            Try
                kameitenCd = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim
                syouhinCd1 = dtOk.Rows(i).Item("syouhin_cd1").ToString.Trim
                syouhinCd2 = dtOk.Rows(i).Item("syouhin_cd2").ToString.Trim
                syouhinCd3 = dtOk.Rows(i).Item("syouhin_cd3").ToString.Trim
                strUserId = dtOk.Rows(i).Item("add_login_user_id").ToString.Trim

                '���敪1
                If Not String.IsNullOrEmpty(syouhinCd1) Then
                    '�p�����[�^�̐ݒ�
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameitenCd))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@toukubun", SqlDbType.Int, 1, 1))
                    paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, syouhinCd1))

                    If CheckTatouwaribikiSettei(kameitenCd, "1") Then
                        '���݂���ꍇ�A�X�V
                        InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
                        '���������ݒ�}�X�^�A�g
                        If Not Me.InsUpdTatouwaribikiSetteiRenkei(kameitenCd, "1", strUserId) Then
                            Throw New ApplicationException
                        End If
                    Else
                        '���݂��Ȃ��ꍇ�A�o�^
                        InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
                        '���������ݒ�}�X�^�A�g
                        If Not Me.InsUpdTatouwaribikiSetteiRenkei(kameitenCd, "1", strUserId) Then
                            Throw New ApplicationException
                        End If
                    End If
                End If

                '���敪2
                If Not String.IsNullOrEmpty(syouhinCd2) Then
                    '�p�����[�^�̐ݒ�
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameitenCd))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@toukubun", SqlDbType.Int, 1, 2))
                    paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, syouhinCd2))

                    If CheckTatouwaribikiSettei(kameitenCd, "2") Then
                        '���݂���ꍇ�A�X�V
                        InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
                        '���������ݒ�}�X�^�A�g
                        If Not Me.InsUpdTatouwaribikiSetteiRenkei(kameitenCd, "2", strUserId) Then
                            Throw New ApplicationException
                        End If
                    Else
                        '���݂��Ȃ��ꍇ�A�o�^
                        InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
                        '���������ݒ�}�X�^�A�g
                        If Not Me.InsUpdTatouwaribikiSetteiRenkei(kameitenCd, "2", strUserId) Then
                            Throw New ApplicationException
                        End If
                    End If
                End If

                '���敪3
                If Not String.IsNullOrEmpty(syouhinCd3) Then
                    '�p�����[�^�̐ݒ�
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameitenCd))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@toukubun", SqlDbType.Int, 1, 3))
                    paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, syouhinCd3))

                    If CheckTatouwaribikiSettei(kameitenCd, "3") Then
                        '���݂���ꍇ�A�X�V
                        InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
                        '���������ݒ�}�X�^�A�g
                        If Not Me.InsUpdTatouwaribikiSetteiRenkei(kameitenCd, "3", strUserId) Then
                            Throw New ApplicationException
                        End If
                    Else
                        '���݂��Ȃ��ꍇ�A�o�^
                        InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
                        '���������ݒ�}�X�^�A�g
                        If Not Me.InsUpdTatouwaribikiSetteiRenkei(kameitenCd, "3", strUserId) Then
                            Throw New ApplicationException
                        End If
                    End If
                End If

            Catch ex As Exception
                Return False
            End Try

        Next

        Return True
    End Function

    '=================2012/05/22 �ԗ� 407553�̑Ή� �ǉ���===================================

    ''' <summary>�����X�}�X�^�A�g�Ǘ��e�[�u���Ƀf�[�^�𑶍݃`�F�b�N����</summary>
    ''' <history>2012/05/22 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function CheckKameitenRenkei(ByVal strKameitenCd As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�ǉ��pSQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(kameiten_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten_renkei WITH(READCOMMITTED) ") '--�����X�}�X�^�A�g�Ǘ��e�[�u��
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ") '--�����X�R�[�h
        End With

        '�p�����[�^�̐ݒ�
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd)) '--�����X�R�[�h
        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKameitenRenkei", paramList.ToArray())

        '�߂�l
        If CInt(dsReturn.Tables(0).Rows(0).Item("cnt")) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>�����X�}�X�^�A�g�Ǘ��e�[�u���Ƀf�[�^��ǉ��ƍX�V</summary>
    ''' <history>2012/05/22 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function InsUpdKameitenRenkei(ByVal strKameitenCd As String, ByVal strUserId As String) As Boolean
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
            .AppendLine("	m_kameiten_renkei WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		kameiten_cd ")
            .AppendLine("		,renkei_siji_cd ")
            .AppendLine("		,sousin_jyky_cd ")
            .AppendLine("		,sousin_kanry_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@kameiten_cd ")
            .AppendLine("	,@renkei_siji_cd ")
            .AppendLine("	,@sousin_jyky_cd ")
            .AppendLine("	,NULL ")
            .AppendLine("	,@upd_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_kameiten_renkei WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	renkei_siji_cd = @renkei_siji_cd ")
            .AppendLine("	,sousin_jyky_cd = @sousin_jyky_cd ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
            .AppendLine("	,upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 10, "0")) '--���M�󋵃R�[�h
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(strUserId)))

        Try
            If CheckKameitenRenkei(strKameitenCd) Then
                '���݂���ꍇ�A�X�V
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "2")) '--�A�g�w���R�[�h(�ύX)
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
            Else
                '���݂��Ȃ��ꍇ�A�o�^
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "1")) '--�A�g�w���R�[�h(�V�K)
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
            End If

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    ''' <summary>�����X�Z���}�X�^�A�g�Ǘ��e�[�u���Ƀf�[�^�𑶍݃`�F�b�N����</summary>
    ''' <history>2012/05/22 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function CheckKameitenJyuusyoRenkei(ByVal strKameitenCd As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�ǉ��pSQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(kameiten_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten_jyuusyo_renkei WITH(READCOMMITTED) ") '--�����X�}�X�^�A�g�Ǘ��e�[�u��
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ") '--�����X�R�[�h
            .AppendLine("   AND ")
            .AppendLine("   jyuusyo_no = @jyuusyo_no ") '--�Z��NO
        End With

        '�p�����[�^�̐ݒ�
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd)) '--�����X�R�[�h
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Int, 10, "1")) '--�Z��NO
        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKameitenRenkei", paramList.ToArray())

        '�߂�l
        If CInt(dsReturn.Tables(0).Rows(0).Item("cnt")) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>�����X�Z���}�X�^�A�g�Ǘ��e�[�u���Ƀf�[�^��ǉ��ƍX�V</summary>
    ''' <history>2012/05/22 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function InsUpdKameitenJyuusyoRenkei(ByVal strKameitenCd As String, ByVal strUserId As String) As Boolean
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
            .AppendLine("	m_kameiten_jyuusyo_renkei WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		kameiten_cd ")
            .AppendLine("		,jyuusyo_no ")
            .AppendLine("		,renkei_siji_cd ")
            .AppendLine("		,sousin_jyky_cd ")
            .AppendLine("		,sousin_kanry_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@kameiten_cd ")
            .AppendLine("	,@jyuusyo_no ")
            .AppendLine("	,@renkei_siji_cd ")
            .AppendLine("	,@sousin_jyky_cd ")
            .AppendLine("	,NULL ")
            .AppendLine("	,@upd_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_kameiten_jyuusyo_renkei WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	renkei_siji_cd = @renkei_siji_cd ")
            .AppendLine("	,sousin_jyky_cd = @sousin_jyky_cd ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
            .AppendLine("	,upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("   AND ")
            .AppendLine("   jyuusyo_no = @jyuusyo_no ") '--�Z��NO
        End With

        '�p�����[�^�̐ݒ�
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Int, 10, "1")) '--�Z��NO
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 10, "0")) '--���M�󋵃R�[�h
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(strUserId)))

        Try
            If CheckKameitenJyuusyoRenkei(strKameitenCd) Then
                '���݂���ꍇ�A�X�V
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "2")) '--�A�g�w���R�[�h(�ύX)
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
            Else
                '���݂��Ȃ��ꍇ�A�o�^
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "1")) '--�A�g�w���R�[�h(�V�K)
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
            End If

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    ''' <summary>���������ݒ�}�X�^�A�g�Ǘ��e�[�u���Ƀf�[�^�𑶍݃`�F�b�N����</summary>
    ''' <history>2012/05/22 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function CheckTatouwaribikiSetteiRenkei(ByVal strKameitenCd As String, ByVal strToukubun As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�ǉ��pSQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(kameiten_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	m_tatouwaribiki_settei_renkei WITH(READCOMMITTED) ") '--���������ݒ�}�X�^�A�g�Ǘ��e�[�u��
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ") '--�����X�R�[�h
            .AppendLine("	AND ")
            .AppendLine("	toukubun = @toukubun ") '--���敪
        End With

        '�p�����[�^�̐ݒ�
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd)) '--�����X�R�[�h
        paramList.Add(MakeParam("@toukubun", SqlDbType.Int, 1, strToukubun)) '--���敪
        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTatouwaribikiSetteiRenkei", paramList.ToArray())

        '�߂�l
        If CInt(dsReturn.Tables(0).Rows(0).Item("cnt")) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>���������ݒ�}�X�^�A�g�Ǘ��e�[�u���Ƀf�[�^��ǉ��ƍX�V</summary>
    ''' <history>2012/05/22 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function InsUpdTatouwaribikiSetteiRenkei(ByVal strKameitenCd As String, ByVal strToukubun As String, ByVal strUserId As String) As Boolean
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
            .AppendLine("	m_tatouwaribiki_settei_renkei WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		kameiten_cd ")
            .AppendLine("		,toukubun ")
            .AppendLine("		,renkei_siji_cd ")
            .AppendLine("		,sousin_jyky_cd ")
            .AppendLine("		,sousin_kanry_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@kameiten_cd ")
            .AppendLine("	,@toukubun ")
            .AppendLine("	,@renkei_siji_cd ")
            .AppendLine("	,@sousin_jyky_cd ")
            .AppendLine("	,NULL ")
            .AppendLine("	,@upd_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_tatouwaribiki_settei_renkei WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	renkei_siji_cd = @renkei_siji_cd ")
            .AppendLine("	,sousin_jyky_cd = @sousin_jyky_cd ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
            .AppendLine("	,upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("   AND ")
            .AppendLine("	toukubun = @toukubun ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@toukubun", SqlDbType.Int, 10, strToukubun))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 10, "0")) '--���M�󋵃R�[�h
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(strUserId)))

        Try
            If CheckTatouwaribikiSetteiRenkei(strKameitenCd, strToukubun) Then
                '���݂���ꍇ�A�X�V
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "2")) '--�A�g�w���R�[�h(�ύX)
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
            Else
                '���݂��Ȃ��ꍇ�A�o�^
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "1")) '--�A�g�w���R�[�h(�V�K)
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
            End If

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    ''' <summary>�����X���l�ݒ�}�X�^�A�g�Ǘ��e�[�u���Ƀf�[�^��ǉ��ƍX�V</summary>
    ''' <history>2012/05/22 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function InsKameitenBikouSetteiRenkei(ByVal strKameitenCd As String, ByVal strNyuuryokuNo As String, ByVal strUserId As String) As Boolean
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
            .AppendLine("	m_kameiten_bikou_settei_renkei WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		kameiten_cd ")
            .AppendLine("		,nyuuryoku_no ")
            .AppendLine("		,renkei_siji_cd ")
            .AppendLine("		,sousin_jyky_cd ")
            .AppendLine("		,sousin_kanry_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@kameiten_cd ")
            .AppendLine("	,@nyuuryoku_no ")
            .AppendLine("	,@renkei_siji_cd ")
            .AppendLine("	,@sousin_jyky_cd ")
            .AppendLine("	,NULL ")
            .AppendLine("	,@upd_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, strNyuuryokuNo))
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "1")) '--�A�g�w���R�[�h(�V�K)
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 10, "0")) '--���M�󋵃R�[�h
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(strUserId)))

        Try
            '�o�^
            InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function
    '=================2012/05/22 �ԗ� 407553�̑Ή� �ǉ���===================================

    ''' <summary>�����X���l�ݒ�}�X�^�Ƀf�[�^��ǉ��ƍX�V</summary>
    ''' <history>2012/02/13�@��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function InsKameitenBikouSettei(ByVal dtOk As Data.DataTable) As Boolean
        '�߂�l
        Dim InsUpdCount As Integer = 0
        '�ǉ��psql��
        Dim strSqlIns As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '�ǉ��pSQL��
        With strSqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kameiten_bikou_settei WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		kameiten_cd ")
            .AppendLine("		,bikou_syubetu ")
            .AppendLine("		,nyuuryoku_no ")
            .AppendLine("		,naiyou ")
            .AppendLine("		,kousinsya ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine("		@kameiten_cd ")
            .AppendLine("		,@bikou_syubetu ")
            .AppendLine("		,@nyuuryoku_no ")
            .AppendLine("		,@naiyou ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL")
            .AppendLine("		,@add_login_user_id ")
            .AppendLine("		,GETDATE() ")
            .AppendLine("		,NULL ")
            .AppendLine("	) ")
        End With

        Dim strKameiten As String
        Dim strUserId As String
        Dim strMaxNyuuryokuNo As String

        For i As Integer = 0 To dtOk.Rows.Count - 1

            Try
                Dim bikouSyubetu1 As String = dtOk.Rows(i).Item("bikou_syubetu1").ToString.Trim
                Dim bikouSyubetu2 As String = dtOk.Rows(i).Item("bikou_syubetu2").ToString.Trim
                Dim bikouSyubetu3 As String = dtOk.Rows(i).Item("bikou_syubetu3").ToString.Trim
                Dim bikouSyubetu4 As String = dtOk.Rows(i).Item("bikou_syubetu4").ToString.Trim
                Dim bikouSyubetu5 As String = dtOk.Rows(i).Item("bikou_syubetu5").ToString.Trim

                strKameiten = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim
                strUserId = dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim


                '�ǉ�
                If Not String.IsNullOrEmpty(bikouSyubetu1) Then
                    '�p�����[�^�̐ݒ�
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.Int, 10, bikouSyubetu1))

                    '����No�i�������X�œo�^�̂������No���Q�Ƃ� MAX�l+1�j
                    strMaxNyuuryokuNo = GetMaxNyuuryokuNo(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)
                    paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, strMaxNyuuryokuNo))

                    paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, InsObj(dtOk.Rows(i).Item("naiyou1").ToString.Trim)))
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

                    If Not Me.InsKameitenBikouSetteiRenkei(strKameiten, strMaxNyuuryokuNo, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If

                If Not String.IsNullOrEmpty(bikouSyubetu2) Then
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.Int, 10, bikouSyubetu2))

                    '����No�i�������X�œo�^�̂������No���Q�Ƃ� MAX�l+1�j
                    strMaxNyuuryokuNo = GetMaxNyuuryokuNo(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)
                    paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, strMaxNyuuryokuNo))

                    paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, InsObj(dtOk.Rows(i).Item("naiyou2").ToString.Trim)))
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

                    If Not Me.InsKameitenBikouSetteiRenkei(strKameiten, strMaxNyuuryokuNo, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If

                If Not String.IsNullOrEmpty(bikouSyubetu3) Then
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.Int, 10, bikouSyubetu3))

                    '����No�i�������X�œo�^�̂������No���Q�Ƃ� MAX�l+1�j
                    strMaxNyuuryokuNo = GetMaxNyuuryokuNo(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)
                    paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, strMaxNyuuryokuNo))

                    paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, InsObj(dtOk.Rows(i).Item("naiyou3").ToString.Trim)))
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

                    If Not Me.InsKameitenBikouSetteiRenkei(strKameiten, strMaxNyuuryokuNo, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If

                If Not String.IsNullOrEmpty(bikouSyubetu4) Then
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.Int, 10, bikouSyubetu4))

                    '����No�i�������X�œo�^�̂������No���Q�Ƃ� MAX�l+1�j
                    strMaxNyuuryokuNo = GetMaxNyuuryokuNo(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)
                    paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, strMaxNyuuryokuNo))

                    paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, InsObj(dtOk.Rows(i).Item("naiyou4").ToString.Trim)))
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

                    If Not Me.InsKameitenBikouSetteiRenkei(strKameiten, strMaxNyuuryokuNo, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If

                If Not String.IsNullOrEmpty(bikouSyubetu5) Then
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.Int, 10, bikouSyubetu5))

                    '����No�i�������X�œo�^�̂������No���Q�Ƃ� MAX�l+1�j
                    strMaxNyuuryokuNo = GetMaxNyuuryokuNo(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)
                    paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, strMaxNyuuryokuNo))

                    paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, InsObj(dtOk.Rows(i).Item("naiyou5").ToString.Trim)))
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

                    If Not Me.InsKameitenBikouSetteiRenkei(strKameiten, strMaxNyuuryokuNo, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If

            Catch ex As Exception
                Return False
            End Try

        Next

        Return True
    End Function

    ''' <summary>
    ''' ����No�i�������X�œo�^�̂������No���Q�Ƃ� MAX�l+1�j
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>����No</returns>
    ''' <remarks>2012/02/13�@��(��A���V�X�e����)�@�V�K�쐬</remarks>
    Private Function GetMaxNyuuryokuNo(ByVal strKameitenCd As String) As Integer
        '�߂�l
        Dim intRtn As Integer = 0
        Dim objMaxNo As Object
        'sql��
        Dim strSql As String = "SELECT MAX(nyuuryoku_no) FROM m_kameiten_bikou_settei  WITH(READCOMMITTED) WHERE kameiten_cd='" & strKameitenCd & "'"

        objMaxNo = ExecuteScalar(connStr, CommandType.Text, strSql)

        If objMaxNo Is DBNull.Value Then
            intRtn = 0
        Else
            intRtn = CInt(objMaxNo)
        End If

        Return intRtn + 1
    End Function

    ''' <summary>�����X���ꊇ�捞�G���[�����擾����</summary>
    ''' <param name="strEdidate">EDI���쐬��</param>
    ''' <param name="strSyoridate">��������</param>
    ''' <returns>�����X���ꊇ�捞�G���[�f�[�^�e�[�u��</returns>
    Public Function SelKameitenInfoIttukatuError(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
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
            .AppendLine("		edi_jouhou_sakusei_date")
            .AppendLine("		,gyou_no")
            .AppendLine("		,syori_datetime")
            .AppendLine("		,kbn")
            .AppendLine("		,kameiten_cd")
            .AppendLine("		,torikesi")
            .AppendLine("		,hattyuu_teisi_flg")
            .AppendLine("		,kameiten_mei1")
            .AppendLine("		,tenmei_kana1")
            .AppendLine("		,kameiten_mei2")
            .AppendLine("		,tenmei_kana2")
            .AppendLine("		,builder_no")
            .AppendLine("		,builder_mei")
            .AppendLine("		,keiretu_cd")
            .AppendLine("		,keiretu_mei")
            .AppendLine("		,eigyousyo_cd")
            .AppendLine("		,eigyousyo_mei")
            .AppendLine("		,kameiten_seisiki_mei")
            .AppendLine("		,kameiten_seisiki_mei_kana")
            .AppendLine("		,todouhuken_cd")
            .AppendLine("		,todouhuken_mei")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,nenkan_tousuu ") '--�N�ԓ���
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,fuho_syoumeisyo_flg")
            .AppendLine("		,fuho_syoumeisyo_kaisi_nengetu")
            .AppendLine("		,eigyou_tantousya_mei")
            .AppendLine("		,eigyou_tantousya_simei")
            .AppendLine("		,kyuu_eigyou_tantousya_mei")
            .AppendLine("		,kyuu_eigyou_tantousya_simei")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,koj_uri_syubetsu ") '--�H��������
            .AppendLine("		,koj_uri_syubetsu_mei ") '--�H�������ʖ�
            .AppendLine("		,jiosaki_flg ") '--JIO��t���O
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,kaiyaku_haraimodosi_kkk")
            .AppendLine("		,tou1_syouhin_cd")
            .AppendLine("		,tou1_syouhin_mei")
            .AppendLine("		,tou2_syouhin_cd")
            .AppendLine("		,tou2_syouhin_mei")
            .AppendLine("		,tou3_syouhin_cd")
            .AppendLine("		,tou3_syouhin_mei")
            .AppendLine("		,tys_seikyuu_saki_kbn")
            .AppendLine("		,tys_seikyuu_saki_cd")
            .AppendLine("		,tys_seikyuu_saki_brc")
            .AppendLine("		,tys_seikyuu_saki_mei")
            .AppendLine("		,koj_seikyuu_saki_kbn")
            .AppendLine("		,koj_seikyuu_saki_cd")
            .AppendLine("		,koj_seikyuu_saki_brc")
            .AppendLine("		,koj_seikyuu_saki_mei")
            .AppendLine("		,hansokuhin_seikyuu_saki_kbn")
            .AppendLine("		,hansokuhin_seikyuu_saki_cd")
            .AppendLine("		,hansokuhin_seikyuu_saki_brc")
            .AppendLine("		,hansokuhin_seikyuu_saki_mei")
            .AppendLine("		,tatemono_seikyuu_saki_kbn")
            .AppendLine("		,tatemono_seikyuu_saki_cd")
            .AppendLine("		,tatemono_seikyuu_saki_brc")
            .AppendLine("		,tatemono_seikyuu_saki_mei")
            .AppendLine("		,seikyuu_saki_kbn5")
            .AppendLine("		,seikyuu_saki_cd5")
            .AppendLine("		,seikyuu_saki_brc5")
            .AppendLine("		,seikyuu_saki_mei5")
            .AppendLine("		,seikyuu_saki_kbn6")
            .AppendLine("		,seikyuu_saki_cd6")
            .AppendLine("		,seikyuu_saki_brc6")
            .AppendLine("		,seikyuu_saki_mei6")
            .AppendLine("		,seikyuu_saki_kbn7")
            .AppendLine("		,seikyuu_saki_cd7")
            .AppendLine("		,seikyuu_saki_brc7")
            .AppendLine("		,seikyuu_saki_mei7")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,hosyou_kikan ") '--�ۏ؊���
            .AppendLine("		,hosyousyo_hak_umu ") '--�ۏ؏����s�L��
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,nyuukin_kakunin_jyouken")
            .AppendLine("		,nyuukin_kakunin_oboegaki")
            .AppendLine("		,tys_mitsyo_flg")
            .AppendLine("		,hattyuusyo_flg")
            .AppendLine("		,yuubin_no")
            .AppendLine("		,jyuusyo1")
            .AppendLine("		,jyuusyo2")
            .AppendLine("		,syozaichi_cd")
            .AppendLine("		,syozaichi_mei")
            .AppendLine("		,busyo_mei")
            .AppendLine("		,daihyousya_mei")
            .AppendLine("		,tel_no")
            .AppendLine("		,fax_no")
            .AppendLine("		,mail_address")
            .AppendLine("		,bikou1")
            .AppendLine("		,bikou2")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,add_date") '--�o�^��
            .AppendLine("		,seikyuu_umu") '--�����L��
            .AppendLine("		,syouhin_cd") '--���i�R�[�h
            .AppendLine("		,syouhin_mei") '--���i��
            .AppendLine("		,uri_gaku") '--������z
            .AppendLine("		,koumuten_seikyuu_gaku") '--�H���X�������z
            .AppendLine("		,seikyuusyo_hak_date") '--���������s��
            .AppendLine("		,uri_date") '--����N����
            .AppendLine("		,bikou") '--���l
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            '==========2012/05/09 �ԗ� 407553�̑Ή� �C����======================
            .AppendLine("		,kameiten_upd_datetime")
            .AppendLine("		,tatouwari_upd_datetime1")
            .AppendLine("		,tatouwari_upd_datetime2")
            .AppendLine("		,tatouwari_upd_datetime3")
            .AppendLine("		,kameiten_jyuusyo_upd_datetime")
            '==========2012/05/09 �ԗ� 407553�̑Ή� �C����======================
            .AppendLine("		,bikou_syubetu1")
            .AppendLine("		,bikou_syubetu1_mei")
            .AppendLine("		,bikou_syubetu2")
            .AppendLine("		,bikou_syubetu2_mei")
            .AppendLine("		,bikou_syubetu3")
            .AppendLine("		,bikou_syubetu3_mei")
            .AppendLine("		,bikou_syubetu4")
            .AppendLine("		,bikou_syubetu4_mei")
            .AppendLine("		,bikou_syubetu5")
            .AppendLine("		,bikou_syubetu5_mei")
            .AppendLine("		,naiyou1")
            .AppendLine("		,naiyou2")
            .AppendLine("		,naiyou3")
            .AppendLine("		,naiyou4")
            .AppendLine("		,naiyou5")
            .AppendLine("		,add_login_user_id")
            .AppendLine("		,add_datetime")
            .AppendLine("		,upd_login_user_id")
            .AppendLine("		,upd_datetime")
            .AppendLine(" FROM  ")
            .AppendLine("    m_kameiten_info_ittukatu_error WITH(READCOMMITTED) ")  '�����X���ꊇ�捞�G���[���e�[�u��
            .AppendLine(" WHERE ")
            .AppendLine("    edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")   'EDI���쐬��
            .AppendLine(" AND CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime ")  '��������
            .AppendLine(" ORDER BY ")
            .AppendLine("    gyou_no ")
        End With
        'EDI���쐬��
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdidate))
        '��������
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))
        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "KameitenInfoIttukatuError", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>�����X���ꊇ�捞�G���[�������擾����</summary>
    ''' <param name="strEdidate">EDI���쐬��</param>
    ''' <returns>�����X���ꊇ�捞�G���[����</returns>
    Public Function SelKameitenInfoIttukatuErrorCount(ByVal strEdidate As String, ByVal strSyoridate As String) As Integer
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
            .AppendLine("   m_kameiten_info_ittukatu_error  WITH(READCOMMITTED) ")  '�����X���ꊇ�捞�G���[���e�[�u��
            .AppendLine(" WHERE ")
            .AppendLine("    edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")  'EDI���쐬��
            .AppendLine(" AND CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime ")  '��������
        End With

        'EDI���쐬��
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdidate))
        '��������
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))
        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "KameitenInfoIttukatuErrorCount", paramList.ToArray)

        Return dsReturn.Tables(0).Rows(0).Item("Maxcount")

    End Function

    ''' <summary>�����X���ꊇ�捞�G���[CSV���擾</summary>
    ''' <returns>�����X���ꊇ�捞�G���[CSV�e�[�u��</returns>
    Public Function SelKameitenInfoIttukatuErrorCsv(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
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
            .AppendLine("		edi_jouhou_sakusei_date")
            .AppendLine("		,gyou_no")
            .AppendLine("		,syori_datetime")
            .AppendLine("		,kbn")
            .AppendLine("		,kameiten_cd")
            .AppendLine("		,torikesi")
            .AppendLine("		,hattyuu_teisi_flg")
            .AppendLine("		,kameiten_mei1")
            .AppendLine("		,tenmei_kana1")
            .AppendLine("		,kameiten_mei2")
            .AppendLine("		,tenmei_kana2")
            .AppendLine("		,builder_no")
            .AppendLine("		,builder_mei")
            .AppendLine("		,keiretu_cd")
            .AppendLine("		,keiretu_mei")
            .AppendLine("		,eigyousyo_cd")
            .AppendLine("		,eigyousyo_mei")
            .AppendLine("		,kameiten_seisiki_mei")
            .AppendLine("		,kameiten_seisiki_mei_kana")
            .AppendLine("		,todouhuken_cd")
            .AppendLine("		,todouhuken_mei")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,nenkan_tousuu ") '--�N�ԓ���
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,fuho_syoumeisyo_flg")
            .AppendLine("		,fuho_syoumeisyo_kaisi_nengetu")
            .AppendLine("		,eigyou_tantousya_mei")
            .AppendLine("		,eigyou_tantousya_simei")
            .AppendLine("		,kyuu_eigyou_tantousya_mei")
            .AppendLine("		,kyuu_eigyou_tantousya_simei")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,koj_uri_syubetsu ") '--�H��������
            .AppendLine("		,koj_uri_syubetsu_mei ") '--�H�������ʖ�
            .AppendLine("		,jiosaki_flg ") '--JIO��t���O
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,kaiyaku_haraimodosi_kkk")
            .AppendLine("		,tou1_syouhin_cd")
            .AppendLine("		,tou1_syouhin_mei")
            .AppendLine("		,tou2_syouhin_cd")
            .AppendLine("		,tou2_syouhin_mei")
            .AppendLine("		,tou3_syouhin_cd")
            .AppendLine("		,tou3_syouhin_mei")
            .AppendLine("		,tys_seikyuu_saki_kbn")
            .AppendLine("		,tys_seikyuu_saki_cd")
            .AppendLine("		,tys_seikyuu_saki_brc")
            .AppendLine("		,tys_seikyuu_saki_mei")
            .AppendLine("		,koj_seikyuu_saki_kbn")
            .AppendLine("		,koj_seikyuu_saki_cd")
            .AppendLine("		,koj_seikyuu_saki_brc")
            .AppendLine("		,koj_seikyuu_saki_mei")
            .AppendLine("		,hansokuhin_seikyuu_saki_kbn")
            .AppendLine("		,hansokuhin_seikyuu_saki_cd")
            .AppendLine("		,hansokuhin_seikyuu_saki_brc")
            .AppendLine("		,hansokuhin_seikyuu_saki_mei")
            .AppendLine("		,tatemono_seikyuu_saki_kbn")
            .AppendLine("		,tatemono_seikyuu_saki_cd")
            .AppendLine("		,tatemono_seikyuu_saki_brc")
            .AppendLine("		,tatemono_seikyuu_saki_mei")
            .AppendLine("		,seikyuu_saki_kbn5")
            .AppendLine("		,seikyuu_saki_cd5")
            .AppendLine("		,seikyuu_saki_brc5")
            .AppendLine("		,seikyuu_saki_mei5")
            .AppendLine("		,seikyuu_saki_kbn6")
            .AppendLine("		,seikyuu_saki_cd6")
            .AppendLine("		,seikyuu_saki_brc6")
            .AppendLine("		,seikyuu_saki_mei6")
            .AppendLine("		,seikyuu_saki_kbn7")
            .AppendLine("		,seikyuu_saki_cd7")
            .AppendLine("		,seikyuu_saki_brc7")
            .AppendLine("		,seikyuu_saki_mei7")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,hosyou_kikan ") '--�ۏ؊���
            .AppendLine("		,hosyousyo_hak_umu ") '--�ۏ؏����s�L��
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,nyuukin_kakunin_jyouken")
            .AppendLine("		,nyuukin_kakunin_oboegaki")
            .AppendLine("		,tys_mitsyo_flg")
            .AppendLine("		,hattyuusyo_flg")
            .AppendLine("		,yuubin_no")
            .AppendLine("		,jyuusyo1")
            .AppendLine("		,jyuusyo2")
            .AppendLine("		,syozaichi_cd")
            .AppendLine("		,syozaichi_mei")
            .AppendLine("		,busyo_mei")
            .AppendLine("		,daihyousya_mei")
            .AppendLine("		,tel_no")
            .AppendLine("		,fax_no")
            .AppendLine("		,mail_address")
            .AppendLine("		,bikou1")
            .AppendLine("		,bikou2")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("		,add_date") '--�o�^��
            .AppendLine("		,seikyuu_umu") '--�����L��
            .AppendLine("		,syouhin_cd") '--���i�R�[�h
            .AppendLine("		,syouhin_mei") '--���i��
            .AppendLine("		,uri_gaku") '--������z
            .AppendLine("		,koumuten_seikyuu_gaku") '--�H���X�������z
            .AppendLine("		,seikyuusyo_hak_date") '--���������s��
            .AppendLine("		,uri_date") '--����N����
            .AppendLine("		,bikou") '--���l
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            '==========2012/05/09 �ԗ� 407553�̑Ή� �C����======================
            .AppendLine("		,kameiten_upd_datetime")
            .AppendLine("		,tatouwari_upd_datetime1")
            .AppendLine("		,tatouwari_upd_datetime2")
            .AppendLine("		,tatouwari_upd_datetime3")
            .AppendLine("		,kameiten_jyuusyo_upd_datetime")
            '==========2012/05/09 �ԗ� 407553�̑Ή� �C����======================
            .AppendLine("		,bikou_syubetu1")
            .AppendLine("		,bikou_syubetu1_mei")
            .AppendLine("		,bikou_syubetu2")
            .AppendLine("		,bikou_syubetu2_mei")
            .AppendLine("		,bikou_syubetu3")
            .AppendLine("		,bikou_syubetu3_mei")
            .AppendLine("		,bikou_syubetu4")
            .AppendLine("		,bikou_syubetu4_mei")
            .AppendLine("		,bikou_syubetu5")
            .AppendLine("		,bikou_syubetu5_mei")
            .AppendLine("		,naiyou1")
            .AppendLine("		,naiyou2")
            .AppendLine("		,naiyou3")
            .AppendLine("		,naiyou4")
            .AppendLine("		,naiyou5")
            .AppendLine("		,add_login_user_id")
            .AppendLine("		,add_datetime")
            .AppendLine("		,upd_login_user_id")
            .AppendLine("		,upd_datetime")
            .AppendLine(" FROM  ")
            .AppendLine("    m_kameiten_info_ittukatu_error WITH(READCOMMITTED) ")  '�����X���ꊇ�捞�G���[���e�[�u��
            .AppendLine(" WHERE ")
            .AppendLine("    edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")   'EDI���쐬��
            .AppendLine(" AND CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime ")  '��������
            .AppendLine(" ORDER BY ")
            .AppendLine("    gyou_no ")
        End With
        'EDI���쐬��
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdidate))
        '��������
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))
        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouError", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>
    ''' �����X�}�X�^�̍X�V�������擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/08 �ԗ� 407553�̑Ή� �ǉ�</history>
    Public Function SelKameitenUpdDate(ByVal strKameitenCd As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet
        'SQL�R�����g
        Dim commandTextSb As New StringBuilder
        ''�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   CASE ")
            .AppendLine("       WHEN upd_datetime IS NOT NULL THEN ")
            .AppendLine("           REPLACE(CONVERT(VARCHAR(10),upd_datetime,112) + '_' + CONVERT(VARCHAR(10),upd_datetime,108),':','') ") '--�X�V����
            .AppendLine("       ELSE ")
            .AppendLine("           REPLACE(CONVERT(VARCHAR(10),add_datetime,112) + '_' + CONVERT(VARCHAR(10),add_datetime,108),':','') ") '--�o�^����
            .AppendLine("       END AS upd_datetime ")
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
        End With

        '�p�����[�^
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKameitenUpdDate", paramList.ToArray)

        Return dsReturn.Tables("dtKameitenUpdDate")
    End Function

    ''' <summary>
    ''' �����X�Z���}�X�^�̍X�V�������擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/08 �ԗ� 407553�̑Ή� �ǉ�</history>
    Public Function SelKameitenJyuusyoUpdDate(ByVal strKameitenCd As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet
        'SQL�R�����g
        Dim commandTextSb As New StringBuilder
        ''�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   CASE ")
            .AppendLine("       WHEN upd_datetime IS NOT NULL THEN ")
            .AppendLine("           REPLACE(CONVERT(VARCHAR(10),upd_datetime,112) + '_' + CONVERT(VARCHAR(10),upd_datetime,108),':','') ") '--�X�V����
            .AppendLine("       ELSE ")
            .AppendLine("           REPLACE(CONVERT(VARCHAR(10),add_datetime,112) + '_' + CONVERT(VARCHAR(10),add_datetime,108),':','') ") '--�o�^����
            .AppendLine("       END AS upd_datetime ")
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten_jyuusyo WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("	AND ")
            .AppendLine("	jyuusyo_no = @jyuusyo_no ")
        End With

        '�p�����[�^
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.VarChar, 1, "1"))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKameitenJyuusyoUpdDate", paramList.ToArray)

        Return dsReturn.Tables("dtKameitenJyuusyoUpdDate")
    End Function

    ''' <summary>
    ''' ���������ݒ�}�X�^�̍X�V�������擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/08 �ԗ� 407553�̑Ή� �ǉ�</history>
    Public Function SelTatouwaribikiSetteiUpdDate(ByVal strKameitenCd As String, ByVal strToukubun As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet
        'SQL�R�����g
        Dim commandTextSb As New StringBuilder
        ''�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   CASE ")
            .AppendLine("       WHEN upd_datetime IS NOT NULL THEN ")
            .AppendLine("           REPLACE(CONVERT(VARCHAR(10),upd_datetime,112) + '_' + CONVERT(VARCHAR(10),upd_datetime,108),':','') ") '--�X�V����
            .AppendLine("       ELSE ")
            .AppendLine("           REPLACE(CONVERT(VARCHAR(10),add_datetime,112) + '_' + CONVERT(VARCHAR(10),add_datetime,108),':','') ") '--�o�^����
            .AppendLine("       END AS upd_datetime ")
            .AppendLine("FROM ")
            .AppendLine("	m_tatouwaribiki_settei WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("	AND ")
            .AppendLine("	toukubun = @toukubun ")
        End With

        '�p�����[�^
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@toukubun", SqlDbType.Int, 10, strToukubun))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTatouwaribikiSetteiUpdDate", paramList.ToArray)

        Return dsReturn.Tables("dtTatouwaribikiSetteiUpdDate")
    End Function

    ''' <summary>
    ''' ���i�̑q�ɃR�[�h���`�F�b�N����
    ''' </summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="strSoukoCd">�q�ɃR�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/08 �ԗ� 407584 �ǉ�</history>
    Public Function SelSyouhinSoukoCdCheck(ByVal strSyouhinCd As String, ByVal strSoukoCd As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet
        'SQL�R�����g
        Dim commandTextSb As New StringBuilder
        ''�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),souko_cd),'') AS souko_cd ") '--�q�ɃR�[�h ")
            .AppendLine("FROM ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ") '--���i�}�X�^ ")
            .AppendLine("WHERE ")
            .AppendLine("	syouhin_cd = @syouhin_cd ") '--���i�R�[�h ")
        End With

        '�p�����[�^
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "dtSyouhinSoukoCdCheck", paramList.ToArray)

        If dsReturn.Tables(0).Rows.Count > 0 Then
            If dsReturn.Tables(0).Rows(0).Item("souko_cd").ToString.Trim.Equals(strSoukoCd) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' ������}�X�^���擾����
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">������R�[�h</param>
    ''' <param name="strSeikyuuSakiBrc">������}��</param>
    ''' <param name="strSeikyuuSakiKbn">������敪</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/08 �ԗ� 407584 �ǉ�</history>
    Public Function SelSeikyuuSaki(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal strSeikyuuSakiKbn As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet
        'SQL�R�����g
        Dim commandTextSb As New StringBuilder
        ''�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	seikyuu_saki_cd ") '--������R�[�h ")
            .AppendLine("	,seikyuu_saki_brc ") '--������}�� ")
            .AppendLine("	,seikyuu_saki_kbn ") '--������敪 ")
            .AppendLine("	,torikesi ") '--��� ")
            .AppendLine("FROM ")
            .AppendLine("	m_seikyuu_saki WITH(READCOMMITTED) ") '--������}�X�^ ")
            .AppendLine("WHERE ")
            .AppendLine("	seikyuu_saki_cd = @seikyuu_saki_cd ") '--������R�[�h ")
            .AppendLine("	AND ")
            .AppendLine("	seikyuu_saki_brc = @seikyuu_saki_brc ") '--������}�� ")
            .AppendLine("	AND ")
            .AppendLine("	seikyuu_saki_kbn = @seikyuu_saki_kbn ") '--������敪 ")
        End With

        '�p�����[�^
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuuSakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuuSakiBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, strSeikyuuSakiKbn))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "dtSeikyuuSaki", paramList.ToArray)

        Return dsReturn.Tables("dtSeikyuuSaki")

    End Function

    ''' <summary>
    ''' ������}�X�^��o�^�A�ύX����
    ''' </summary>
    ''' <param name="dtOk">�f�[�^�e�[�u��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/08 �ԗ� 407584 �ǉ�</history>
    Public Function InsUpdSeikyuuSaki(ByVal dtOk As Data.DataTable) As Boolean

        '�߂�l
        Dim InsUpdCount As Integer = 0

        Dim sqlIns As New System.Text.StringBuilder
        Dim sqlUpd As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlIns
            .AppendLine("INSERT INTO  ")
            .AppendLine("	m_seikyuu_saki WITH(UPDLOCK) ") '--������}�X�^ ")
            .AppendLine("	( ")
            .AppendLine("		seikyuu_saki_cd ") '--������R�[�h ")
            .AppendLine("		,seikyuu_saki_brc ") '--������}�� ")
            .AppendLine("		,seikyuu_saki_kbn ") '--������敪 ")
            .AppendLine("		,torikesi ") '--��� ")
            .AppendLine("		,skk_jigyousyo_cd ") '--�V��v���Ə��R�[�h ")
            .AppendLine("		,kyuu_seikyuu_saki_cd ") '--��������R�[�h ")
            .AppendLine("		,nayose_saki_cd ") '--�����R�[�h ")
            .AppendLine("		,tantousya_mei ") '--�S���Җ� ")
            .AppendLine("		,seikyuusyo_inji_bukken_mei_flg ") '--�������󎚕������t���O ")
            .AppendLine("		,skysy_soufu_jyuusyo1 ") '--���������t��Z��1 ")
            .AppendLine("		,skysy_soufu_jyuusyo2 ") '--���������t��Z��2 ")
            .AppendLine("		,skysy_soufu_yuubin_no ") '--���������t��X�֔ԍ� ")
            .AppendLine("		,skysy_soufu_tel_no ") '--���������t��d�b�ԍ� ")
            .AppendLine("		,skysy_soufu_fax_no ") '--���������t��FAX�ԍ� ")
            .AppendLine("		,nyuukin_kouza_no ") '--���������ԍ� ")
            .AppendLine("		,seikyuu_sime_date ") '--�������ߓ� ")
            .AppendLine("		,senpou_seikyuu_sime_date ") '--����������ߓ� ")
            .AppendLine("		,kessanji_nidosime_flg ") '--���Z����x���߃t���O ")
            .AppendLine("		,tyk_koj_seikyuu_timing_flg ") '--���H�������^�C�~���O�t���O ")
            .AppendLine("		,sousai_flg ") '--���E�t���O ")
            .AppendLine("		,kaisyuu_yotei_gessuu ") '--����\�茎�� ")
            .AppendLine("		,kaisyuu_yotei_date ") '--����\��� ")
            .AppendLine("		,seikyuusyo_hittyk_date ") '--�������K���� ")
            .AppendLine("		,kaisyuu1_syubetu1 ") '--���1���1 ")
            .AppendLine("		,kaisyuu1_wariai1 ") '--���1����1 ")
            .AppendLine("		,kaisyuu1_tegata_site_gessuu ") '--���1��`�T�C�g���� ")
            .AppendLine("		,kaisyuu1_tegata_site_date ") '--���1��`�T�C�g�� ")
            .AppendLine("		,kaisyuu1_seikyuusyo_yousi ") '--���1�������p�� ")
            .AppendLine("		,kaisyuu1_syubetu2 ") '--���1���2 ")
            .AppendLine("		,kaisyuu1_wariai2 ") '--���1����2 ")
            .AppendLine("		,kaisyuu1_syubetu3 ") '--���1���3 ")
            .AppendLine("		,kaisyuu1_wariai3 ") '--���1����3 ")
            .AppendLine("		,kaisyuu_kyoukaigaku ") '--������E�z ")
            .AppendLine("		,kaisyuu2_syubetu1 ") '--���2���1 ")
            .AppendLine("		,kaisyuu2_wariai1 ") '--���2����1 ")
            .AppendLine("		,kaisyuu2_tegata_site_gessuu ") '--���2��`�T�C�g���� ")
            .AppendLine("		,kaisyuu2_tegata_site_date ") '--���2��`�T�C�g�� ")
            .AppendLine("		,kaisyuu2_seikyuusyo_yousi ") '--���2�������p�� ")
            .AppendLine("		,kaisyuu2_syubetu2 ") '--���2���2 ")
            .AppendLine("		,kaisyuu2_wariai2 ") '--���2����2 ")
            .AppendLine("		,kaisyuu2_syubetu3 ") '--���2���3 ")
            .AppendLine("		,kaisyuu2_wariai3 ") '--���2����3 ")
            .AppendLine("		,koufuri_ok_flg ") '--���UOK�t���O ")
            .AppendLine("		,tougou_tokuisaki_cd ") '--������v���Ӑ溰�� ")
            .AppendLine("		,anzen_kaihi_en ") '--���S���͉��_�~ ")
            .AppendLine("		,anzen_kaihi_wari ") '--���S���͉��_���� ")
            .AppendLine("		,bikou ") '--���l ")
            .AppendLine("		,add_login_user_id ") '--�o�^���O�C�����[�U�[ID ")
            .AppendLine("		,add_datetime ") '--�o�^���� ")
            .AppendLine("		,upd_login_user_id ") '--�X�V���O�C�����[�U�[ID ")
            .AppendLine("		,upd_datetime ") '--�X�V���� ")
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            .AppendLine("		,ginkou_siten_cd ") '--��s�x�X�R�[�h ")
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            .AppendLine("	) ")
            .AppendLine("SELECT ")
            .AppendLine("	@seikyuu_saki_cd ") '--������R�[�h ")
            .AppendLine("	,@seikyuu_saki_brc ") '--������}�� ")
            .AppendLine("	,@seikyuu_saki_kbn ") '--������敪 ")
            .AppendLine("	,@torikesi ") '--��� ")
            .AppendLine("	,MSSTH.skk_jigyousyo_cd ") '--�V��v���Ə��R�[�h ")
            .AppendLine("	,NULL AS kyuu_seikyuu_saki_cd ") '--��������R�[�h ")
            .AppendLine("	,NULL AS nayose_saki_cd ") '--�����R�[�h ")
            .AppendLine("	,MSSTH.tantousya_mei ") '--�S���Җ� ")
            .AppendLine("	,MSSTH.seikyuusyo_inji_bukken_mei_flg ") '--�������󎚕������t���O ")
            .AppendLine("	,NULL AS skysy_soufu_jyuusyo1 ") '--���������t��Z��1 ")
            .AppendLine("	,NULL AS skysy_soufu_jyuusyo2 ") '--���������t��Z��2 ")
            .AppendLine("	,NULL AS skysy_soufu_yuubin_no ") '--���������t��X�֔ԍ� ")
            .AppendLine("	,NULL AS skysy_soufu_tel_no ") '--���������t��d�b�ԍ� ")
            .AppendLine("	,NULL AS skysy_soufu_fax_no ") '--���������t��FAX�ԍ� ")
            .AppendLine("	,MSSTH.nyuukin_kouza_no ") '--���������ԍ� ")
            .AppendLine("	,MSSTH.seikyuu_sime_date ") '--�������ߓ� ")
            .AppendLine("	,MSSTH.senpou_seikyuu_sime_date ") '--����������ߓ� ")
            .AppendLine("	,NULL AS kessanji_nidosime_flg ") '--���Z����x���߃t���O ")
            .AppendLine("	,MSSTH.tyk_koj_seikyuu_timing_flg ") '--���H�������^�C�~���O�t���O ")
            .AppendLine("	,MSSTH.sousai_flg ") '--���E�t���O ")
            .AppendLine("	,MSSTH.kaisyuu_yotei_gessuu ") '--����\�茎�� ")
            .AppendLine("	,MSSTH.kaisyuu_yotei_date ") '--����\��� ")
            .AppendLine("	,MSSTH.seikyuusyo_hittyk_date ") '--�������K���� ")
            .AppendLine("	,MSSTH.kaisyuu1_syubetu1 ") '--���1���1 ")
            .AppendLine("	,MSSTH.kaisyuu1_wariai1 ") '--���1����1 ")
            .AppendLine("	,MSSTH.kaisyuu1_tegata_site_gessuu ") '--���1��`�T�C�g���� ")
            .AppendLine("	,MSSTH.kaisyuu1_tegata_site_date ") '--���1��`�T�C�g�� ")
            .AppendLine("	,MSSTH.kaisyuu1_seikyuusyo_yousi ") '--���1�������p�� ")
            .AppendLine("	,MSSTH.kaisyuu1_syubetu2 ") '--���1���2 ")
            .AppendLine("	,MSSTH.kaisyuu1_wariai2 ") '--���1����2 ")
            .AppendLine("	,MSSTH.kaisyuu1_syubetu3 ") '--���1���3 ")
            .AppendLine("	,MSSTH.kaisyuu1_wariai3 ") '--���1����3 ")
            .AppendLine("	,MSSTH.kaisyuu_kyoukaigaku ") '--������E�z ")
            .AppendLine("	,MSSTH.kaisyuu2_syubetu1 ") '--���2���1 ")
            .AppendLine("	,MSSTH.kaisyuu2_wariai1 ") '--���2����1 ")
            .AppendLine("	,MSSTH.kaisyuu2_tegata_site_gessuu ") '--���2��`�T�C�g���� ")
            .AppendLine("	,MSSTH.kaisyuu2_tegata_site_date ") '--���2��`�T�C�g�� ")
            .AppendLine("	,MSSTH.kaisyuu2_seikyuusyo_yousi ") '--���2�������p�� ")
            .AppendLine("	,MSSTH.kaisyuu2_syubetu2 ") '--���2���2 ")
            .AppendLine("	,MSSTH.kaisyuu2_wariai2 ") '--���2����2 ")
            .AppendLine("	,MSSTH.kaisyuu2_syubetu3 ") '--���2���3 ")
            .AppendLine("	,MSSTH.kaisyuu2_wariai3 ") '--���2����3 ")
            .AppendLine("	,NULL AS koufuri_ok_flg ") '--���UOK�t���O ")
            .AppendLine("	,NULL AS tougou_tokuisaki_cd ") '--������v���Ӑ溰�� ")
            .AppendLine("	,NULL AS anzen_kaihi_en ") '--���S���͉��_�~ ")
            .AppendLine("	,NULL AS anzen_kaihi_wari ") '--���S���͉��_���� ")
            .AppendLine("	,NULL AS bikou ") '--���l ")
            .AppendLine("	,@add_login_user_id ") '--�o�^���O�C�����[�U�[ID ")
            .AppendLine("	,GETDATE() AS add_datetime ") '--�o�^���� ")
            .AppendLine("	,NULL AS upd_login_user_id ") '--�X�V���O�C�����[�U�[ID ")
            .AppendLine("	,NULL AS upd_datetime ") '--�X�V���� ")
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            .AppendLine("	,MSSTH.ginkou_siten_cd ") '--��s�x�X�R�[�h ")
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            .AppendLine("FROM ")
            .AppendLine("	m_seikyuu_saki_touroku_hinagata AS MSSTH WITH(READCOMMITTED) ") '--������o�^���`�}�X�^ ")
            .AppendLine("WHERE ")
            .AppendLine("	seikyuu_saki_brc = '00' ") '--������}�� ")
            .AppendLine("	AND ")
            .AppendLine("	torikesi = '0' ") '--��� ")
            .AppendLine("	AND ")
            .AppendLine("	kihon_flg = '1' ") '--��{�׸� ")
        End With

        With sqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_seikyuu_saki WITH(UPDLOCK) ") '--������}�X�^ ")
            .AppendLine("SET ")
            .AppendLine("	torikesi = @torikesi ") '--��� ")
            .AppendLine("WHERE ")
            .AppendLine("	seikyuu_saki_cd = @seikyuu_saki_cd ") '--������R�[�h ")
            .AppendLine("	AND ")
            .AppendLine("	seikyuu_saki_brc = seikyuu_saki_brc ") '--������}�� ")
            .AppendLine("	AND ")
            .AppendLine("	seikyuu_saki_kbn = seikyuu_saki_kbn ") '--������敪 ")
        End With

        '�����X�R�[�h
        Dim strKameitenCd As String
        '���[�U�[ID
        Dim strUserId As String
        '�e�[�u��
        Dim dtSeikyuuSaki As New Data.DataTable

        For i As Integer = 0 To dtOk.Rows.Count - 1
            '�����X�R�[�h
            strKameitenCd = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim
            '���[�U�[ID
            strUserId = dtOk.Rows(i).Item("add_login_user_id").ToString.Trim

            paramList.Clear()
            paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strKameitenCd)) '������R�[�h
            paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, "00")) '������}��
            paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, "0")) '������}��
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, "0")) '���
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId)) '�o�^���O�C�����[�U�[ID

            dtSeikyuuSaki = Me.SelSeikyuuSaki(strKameitenCd, "00", "0")
            Try
                If dtSeikyuuSaki.Rows.Count = 0 Then
                    '�A�b�v���[�h.�����X���ށ@�}�ԁ@00�@�敪�@0�@���@������Ͻ�.�����溰�ށE������}�ԁE������敪�ɑ��݂��Ȃ��ꍇ)	
                    '������o�^���`�}�X�^�̐�����}�� = 00 �A���=0�A��{�׸�=1�@����Y���̃��R�[�h���擾���A�Z�b�g
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, sqlIns.ToString(), paramList.ToArray)
                Else
                    If dtSeikyuuSaki.Rows(0).Item("torikesi").ToString.Trim.Equals("1") Then
                        '�����}�X�^.��� = 1 �� �A�b�v���[�h.�����X���ށ@�}�ԁ@00�@�敪�@0�@��	
                        '������Ͻ�.�����溰�ށE������}�ԁE������敪�ɑ��݂��鎞 �� ��� = 0 ���Z�b�g
                        InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, sqlUpd.ToString(), paramList.ToArray)
                    End If
                End If
            Catch ex As Exception
                Return False
            End Try
        Next

        Return True
    End Function

    ''' <summary>
    ''' �X�ʏ��������e�[�u���̌������擾����
    ''' </summary>
    ''' <param name="strMiseCd">������R�[�h</param>
    ''' <param name="strBunruiCd">������}��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/09 �ԗ� 407584 �ǉ�</history>
    Public Function SelTenbetuSyokiSeikyuuCount(ByVal strMiseCd As String, ByVal strBunruiCd As String) As Integer

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        '����
        Dim intCount As Integer = 0

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(mise_cd) AS CNT ") '--���� ")
            .AppendLine("FROM ")
            .AppendLine("	t_tenbetu_syoki_seikyuu WITH(READCOMMITTED) ") '--�X�ʏ��������e�[�u�� ")
            .AppendLine("WHERE ")
            .AppendLine("	mise_cd = @mise_cd ") '--�X�R�[�h ")
            .AppendLine("	AND ")
            .AppendLine("	bunrui_cd = @bunrui_cd ") '--���ރR�[�h ")
        End With

        '�p�����[�^
        paramList.Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strMiseCd))
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, strBunruiCd))

        '�������s
        intCount = Convert.ToInt32(ExecuteScalar(connStr, CommandType.Text, commandTextSb.ToString, paramList.ToArray))

        '�߂�l
        Return intCount

    End Function

    ''' <summary>
    ''' �X�ʏ��������e�[�u����o�^����
    ''' </summary>
    ''' <param name="dtOk">�f�[�^�e�[�u��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/09 �ԗ� 407584 �ǉ�</history>
    Public Function InsTenbetuSyokiSeikyuu(ByVal dtOk As Data.DataTable) As Boolean

        '�߂�l
        Dim InsUpdCount As Integer = 0

        Dim sqlIns As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim intInsCount As Integer = 0

        With sqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	t_tenbetu_syoki_seikyuu  WITH(UPDLOCK) ") '--�X�ʏ��������e�[�u�� ")
            .AppendLine("	( ")
            .AppendLine("		mise_cd ") '--�X�R�[�h ")
            .AppendLine("		,bunrui_cd ") '--���ރR�[�h ")
            .AppendLine("		,add_date ") '--�o�^�� ")
            .AppendLine("		,seikyuusyo_hak_date ") '--���������s�� ")
            .AppendLine("		,uri_date ") '--����N���� ")
            .AppendLine("		,denpyou_uri_date ") '--�`�[����N���� ")
            .AppendLine("		,seikyuu_umu ") '--�����L�� ")
            .AppendLine("		,uri_keijyou_flg ") '--���㏈��FLG(����v��FLG) ")
            .AppendLine("		,uri_keijyou_date ") '--���㏈����(����v���) ")
            .AppendLine("		,syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("		,uri_gaku ") '--������z ")
            .AppendLine("		,zei_kbn ") '--�ŋ敪 ")
            .AppendLine("		,syouhizei_gaku ") '--����Ŋz ")
            .AppendLine("		,bikou ") '--���l ")
            .AppendLine("		,koumuten_seikyuu_gaku ") '--�H���X�������z ")
            .AppendLine("		,add_login_user_id ") '--�o�^���O�C�����[�U�[ID ")
            .AppendLine("		,add_datetime ") '--�o�^���� ")
            .AppendLine("		,upd_login_user_id ") '--�X�V���O�C�����[�U�[ID ")
            .AppendLine("		,upd_datetime ") '--�X�V���� ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@mise_cd ") '--�X�R�[�h ")
            .AppendLine("	,@bunrui_cd ") '--���ރR�[�h ")
            .AppendLine("	,@add_date ") '--�o�^�� ")
            .AppendLine("	,@seikyuusyo_hak_date ") '--���������s�� ")
            .AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111))") '--����N���� ")
            .AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111)) ") '--�`�[����N���� ")
            .AppendLine("	,@seikyuu_umu ") '--�����L�� ")
            .AppendLine("	,@uri_keijyou_flg ") '--���㏈��FLG(����v��FLG) ")
            .AppendLine("	,NULL ") '--���㏈����(����v���) ")
            .AppendLine("	,@syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("	,@uri_gaku ") '--������z ")
            .AppendLine("	,@zei_kbn ") '--�ŋ敪 ")
            .AppendLine("	,@syouhizei_gaku ") '--����Ŋz ")
            .AppendLine("	,@bikou ") '--���l ")
            .AppendLine("	,@koumuten_seikyuu_gaku ") '--�H���X�������z ")
            .AppendLine("	,@add_login_user_id ") '--�o�^���O�C�����[�U�[ID ")
            .AppendLine("	,GETDATE() ") '--�o�^���� ")
            .AppendLine("	,NULL ") '--�X�V���O�C�����[�U�[ID ")
            .AppendLine("	,NULL ") '--�X�V���� ")
            .AppendLine(") ")
        End With

        '�X�R�[�h
        Dim strMiseCd As String = String.Empty
        '�o�^��
        Dim strAddDate As String = String.Empty
        Dim addDate As New DateTime
        '���������s��
        Dim strSeikyuusyoHakDate As String = String.Empty
        '�����L��
        Dim strSeikyuu_umu As String = String.Empty
        '���i�R�[�h
        Dim strSyouhinCd As String = String.Empty
        '�ŋ敪
        Dim strZeiKbn As String = String.Empty
        '������z
        Dim strUriGaku As String = String.Empty
        '����Ŋz
        Dim strSyouhizeiGaku As String = String.Empty
        '�H���X�������z
        Dim strKoumutenSeikyuuGaku As String = String.Empty

        '����������R�[�h
        Dim strTysSeikyuuSakiCd As String = String.Empty
        '����������}��
        Dim strTysSeikyuuSakiBrc As String = String.Empty
        '����������敪
        Dim strTysSeikyuuSakiKbn As String = String.Empty

        '���i���
        Dim dtSyouhinInfo As New Data.DataTable

        For i As Integer = 0 To dtOk.Rows.Count - 1

            '�X�R�[�h
            strMiseCd = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim

            If Me.SelTenbetuSyokiSeikyuuCount(strMiseCd, "200") > 0 Then
                '(�A�b�v���[�h.�����X���� = �X�ʏ��������e�[�u��.�X�R�[�h ���� �X�ʏ��������e�[�u��. ���ރR�[�h=200�@�ő��݂���ꍇ)�@��	
                '���㏑���X�V�͑Ή��Ƃ��Ȃ�	
            Else
                '(�A�b�v���[�h.�����X����=�X�ʏ��������e�[�u��.�X�R�[�h ���� �X�ʏ��������e�[�u��. ���ރR�[�h=200�@�ő��݂��Ȃ��ꍇ)

                '�����L��
                strSeikyuu_umu = dtOk.Rows(i).Item("seikyuu_umu").ToString.Trim
                If strSeikyuu_umu.Equals(String.Empty) Then
                    strSeikyuu_umu = "0"
                End If

                '�o�^��
                strAddDate = dtOk.Rows(i).Item("add_date").ToString.Trim
                If strAddDate.Equals(String.Empty) Then
                    addDate = Me.GetSysDate()
                Else
                    Try
                        strAddDate = Left(strAddDate, 4) & "/" & Mid(strAddDate, 5, 2) & "/" & Mid(strAddDate, 7, 2)
                        addDate = Convert.ToDateTime(strAddDate)
                    Catch ex As Exception
                        addDate = Me.GetSysDate()
                    End Try
                End If

                '����������R�[�h
                strTysSeikyuuSakiCd = dtOk.Rows(i).Item("tys_seikyuu_saki_cd").ToString.Trim
                '����������}��
                strTysSeikyuuSakiBrc = dtOk.Rows(i).Item("tys_seikyuu_saki_brc").ToString.Trim
                '����������敪
                strTysSeikyuuSakiKbn = dtOk.Rows(i).Item("tys_seikyuu_saki_kbn").ToString.Trim

                If strSeikyuu_umu.Equals("0") Then
                    '�����L�� 0�@(���������I����)

                    '���i�R�[�h
                    strSyouhinCd = "C0099"

                    '���������s��(�X�V�ΏۂƂ��Ȃ�)
                    strSeikyuusyoHakDate = String.Empty

                Else
                    '�����L�� 1�@(�����L��I����)

                    '���i�R�[�h
                    strSyouhinCd = dtOk.Rows(i).Item("syouhin_cd").ToString.Trim

                    '���������s��(���������s�����V�X�e���N���{������}�X�^�D�������ߓ� )
                    strSeikyuusyoHakDate = Me.GetSeikyuusyoHakDate(strTysSeikyuuSakiCd, strTysSeikyuuSakiBrc, strTysSeikyuuSakiKbn)

                End If

                '���i���
                dtSyouhinInfo = Me.SelSyouhinInfo(strSyouhinCd)

                '�ŋ敪
                If dtSyouhinInfo.Rows.Count > 0 Then
                    strZeiKbn = dtSyouhinInfo.Rows(0).Item("zei_kbn").ToString.Trim
                Else
                    strZeiKbn = String.Empty
                End If

                If strSeikyuu_umu.Equals("0") Then
                    '�����L�� 0�@(���������I����)

                    '������z
                    strUriGaku = "0"

                    '����Ŋz
                    strSyouhizeiGaku = "0"

                    '�H���X�������z
                    strKoumutenSeikyuuGaku = "0"
                Else
                    '�����L�� 1�@(�����L��I����)

                    '������z
                    strUriGaku = dtOk.Rows(i).Item("uri_gaku").ToString.Trim
                    If strUriGaku.Equals(String.Empty) Then
                        '������z���󔒂̏ꍇ�A���iϽ�.�W�����i
                        If dtSyouhinInfo.Rows.Count > 0 Then
                            strUriGaku = dtSyouhinInfo.Rows(0).Item("hyoujun_kkk").ToString.Trim
                        Else
                            strUriGaku = String.Empty
                        End If
                    End If

                    '����Ŋz
                    If dtSyouhinInfo.Rows.Count > 0 Then
                        If (Not dtSyouhinInfo.Rows(0).Item("zeiritu").ToString.Trim.Equals(String.Empty)) AndAlso (Not strUriGaku.Equals(String.Empty)) Then
                            '������z �~ �����Ͻ�.�|��
                            strSyouhizeiGaku = Math.Round(Convert.ToDecimal(dtSyouhinInfo.Rows(0).Item("zeiritu")) * Convert.ToDecimal(strUriGaku), 0).ToString

                        Else
                            strSyouhizeiGaku = String.Empty
                        End If
                    Else
                        strSyouhizeiGaku = String.Empty
                    End If

                    '�H���X�������z
                    strKoumutenSeikyuuGaku = dtOk.Rows(i).Item("koumuten_seikyuu_gaku").ToString.Trim
                End If

                paramList.Clear()
                paramList.Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strMiseCd)) '�X�R�[�h
                paramList.Add(MakeParam("@bunrui_cd", SqlDbType.Char, 3, "200")) '���ރR�[�h
                paramList.Add(MakeParam("@add_date", SqlDbType.DateTime, 20, addDate.ToString("yyyy/MM/dd"))) '�o�^��
                paramList.Add(MakeParam("@seikyuusyo_hak_date", SqlDbType.DateTime, 20, InsObj(strSeikyuusyoHakDate))) '���������s��
                paramList.Add(MakeParam("@seikyuu_umu", SqlDbType.Int, 10, strSeikyuu_umu)) '�����L��
                paramList.Add(MakeParam("@uri_keijyou_flg", SqlDbType.Int, 10, "0")) '���㏈��FLG(����v��FLG)
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd)) '���i�R�[�h
                paramList.Add(MakeParam("@uri_gaku", SqlDbType.Int, 10, InsObj(strUriGaku))) '������z
                paramList.Add(MakeParam("@zei_kbn", SqlDbType.VarChar, 1, strZeiKbn)) '�ŋ敪
                paramList.Add(MakeParam("@syouhizei_gaku", SqlDbType.Int, 10, InsObj(strSyouhizeiGaku))) '����Ŋz
                paramList.Add(MakeParam("@bikou", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("bikou").ToString.Trim))) '���l
                paramList.Add(MakeParam("@koumuten_seikyuu_gaku", SqlDbType.Int, 10, InsObj(strKoumutenSeikyuuGaku))) '�H���X�������z
                paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)) '�o�^���O�C�����[�U�[ID

                Try
                    '���s
                    intInsCount = ExecuteNonQuery(connStr, CommandType.Text, sqlIns.ToString(), paramList.ToArray)
                    If intInsCount = 0 Then
                        Return False
                    End If

                    '�X�ʏ��������e�[�u���A�g�Ǘ��e�[�u��
                    If Not Me.InsTenbetuSyokiSeikyuuRenkei(strMiseCd, "200", dtOk.Rows(i).Item("add_login_user_id").ToString.Trim) Then
                        Throw New ApplicationException
                    End If
                Catch ex As Exception
                    Return False
                End Try

            End If
        Next

        Return True
    End Function

    ''' <summary>�X�ʏ��������e�[�u���A�g�Ǘ��e�[�u���𑶍݃`�F�b�N����</summary>
    ''' <history>2013/03/09 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetTenbetuSyokiSeikyuuRenkeiCount(ByVal strMiseCd As String, ByVal strBunruiCd As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim intCount As Integer

        '�ǉ��pSQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(mise_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	t_tenbetu_syoki_seikyuu_renkei WITH(READCOMMITTED) ") '--�X�ʏ��������e�[�u���A�g�Ǘ��e�[�u��
            .AppendLine("WHERE ")
            .AppendLine("	mise_cd = @mise_cd ") '--�X�R�[�h
            .AppendLine("	AND ")
            .AppendLine("	bunrui_cd = @bunrui_cd ") '--���ރR�[�h
        End With

        '�p�����[�^�̐ݒ�
        paramList.Clear()
        paramList.Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strMiseCd)) '--�X�R�[�h
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, strBunruiCd)) '--���ރR�[�h
        ' �������s

        intCount = Convert.ToInt32(ExecuteScalar(connStr, CommandType.Text, commandTextSb.ToString, paramList.ToArray))

        '�߂�l
        If intCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' �X�ʏ��������e�[�u���A�g�Ǘ��e�[�u����o�^����
    ''' </summary>
    ''' <param name="strMiseCd">�X�R�[�h</param>
    ''' <param name="strBunruiCd">���ރR�[�h</param>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/09 �ԗ� 407584 �ǉ�</history>
    Public Function InsTenbetuSyokiSeikyuuRenkei(ByVal strMiseCd As String, ByVal strBunruiCd As String, ByVal strUserId As String) As Boolean

        '�߂�l
        Dim InsUpdCount As Integer = 0

        Dim sqlIns As New System.Text.StringBuilder
        Dim sqlUpd As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim intInsUpdCount As Integer = 0

        With sqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	t_tenbetu_syoki_seikyuu_renkei WITH(UPDLOCK) ") '--�X�ʏ��������e�[�u���A�g�Ǘ��e�[�u�� ")
            .AppendLine("	( ")
            .AppendLine("		mise_cd ") '--�X�R�[�h ")
            .AppendLine("		,bunrui_cd ") '--���ރR�[�h ")
            .AppendLine("		,renkei_siji_cd ") '--�A�g�w���R�[�h ")
            .AppendLine("		,sousin_jyky_cd ") '--���M�󋵃R�[�h ")
            .AppendLine("		,upd_login_user_id ") '--�X�V���O�C�����[�U�[ID ")
            .AppendLine("		,upd_datetime ") '--�X�V���� ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@mise_cd ") '--�X�R�[�h ")
            .AppendLine("	,@bunrui_cd ") '--���ރR�[�h ")
            .AppendLine("	,@renkei_siji_cd ") '--�A�g�w���R�[�h ")
            .AppendLine("	,@sousin_jyky_cd ") '--���M�󋵃R�[�h ")
            .AppendLine("	,@upd_login_user_id ") '--�X�V���O�C�����[�U�[ID ")
            .AppendLine("	,GETDATE() ") '--�X�V���� ")
            .AppendLine(") ")
        End With

        With sqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	t_tenbetu_syoki_seikyuu_renkei WITH(UPDLOCK) ") '--�X�ʏ��������e�[�u���A�g�Ǘ��e�[�u�� ")
            .AppendLine("SET ")
            .AppendLine("	renkei_siji_cd = @renkei_siji_cd ") '--�A�g�w���R�[�h ")
            .AppendLine("	,sousin_jyky_cd = @sousin_jyky_cd ") '--���M�󋵃R�[�h ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ") '--�X�V���O�C�����[�U�[ID ")
            .AppendLine("	,upd_datetime = GETDATE() ") '--�X�V���� ")
            .AppendLine("WHERE ")
            .AppendLine("	mise_cd = @mise_cd ") '--�X�R�[�h ")
            .AppendLine("	AND ")
            .AppendLine("	bunrui_cd = @bunrui_cd ") '--���ރR�[�h ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Clear()
        paramList.Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strMiseCd)) '--�X�R�[�h
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, strBunruiCd)) '--���ރR�[�h
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "1")) '--�A�g�w���R�[�h
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 10, "0")) '--���M�󋵃R�[�h
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId)) '--�X�V���O�C�����[�U�[ID

        Try
            If Me.GetTenbetuSyokiSeikyuuRenkeiCount(strMiseCd, strBunruiCd) Then
                '�X�V
                intInsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, sqlUpd.ToString(), paramList.ToArray)
            Else
                '�o�^
                intInsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, sqlIns.ToString(), paramList.ToArray)
            End If

            If intInsUpdCount = 0 Then
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    ''' <summary>
    ''' �V�X�e������
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/09 �ԗ� 407584 �ǉ�</history>
    Public Function GetSysDate() As DateTime

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
        Sql.AppendLine("SELECT")
        Sql.AppendLine("    getdate()")

        Dim sysDate As New DateTime

        ' �������s
        sysDate = Convert.ToDateTime(ExecuteScalar(connStr, CommandType.Text, Sql.ToString(), paramList.ToArray))

        Return sysDate

    End Function

    ''' <summary>
    ''' ���������s�����擾����
    ''' </summary>
    ''' <param name="strTysSeikyuuSakiCd">����������R�[�h</param>
    ''' <param name="strTysSeikyuuSakiBrc">����������}��</param>
    ''' <param name="strTysSeikyuuSakiKbn">����������敪</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/09 �ԗ� 407584 �ǉ�</history>
    Public Function GetSeikyuusyoHakDate(ByVal strTysSeikyuuSakiCd As String, ByVal strTysSeikyuuSakiBrc As String, ByVal strTysSeikyuuSakiKbn As String) As DateTime

        '���������s��
        Dim seikyuusyoHakDate As New DateTime

        '�������ߓ�
        Dim strSeikyuuSimeDate As String = String.Empty

        Dim sysDate As DateTime = Me.GetSysDate()

        If strTysSeikyuuSakiCd.Equals(String.Empty) OrElse strTysSeikyuuSakiBrc.Equals(String.Empty) OrElse strTysSeikyuuSakiKbn.Equals(String.Empty) Then
            '�������ߓ�
            strSeikyuuSimeDate = String.Empty
        Else
            ' SQL���̐���
            Dim sql As New StringBuilder
            ' �p�����[�^�i�[
            Dim paramList As New List(Of SqlClient.SqlParameter)

            ' SQL��
            With sql
                .AppendLine("SELECT ")
                .AppendLine("	ISNULL(seikyuu_sime_date,'') AS seikyuu_sime_date ") '--�������ߓ� ")
                .AppendLine("FROM ")
                .AppendLine("	m_seikyuu_saki WITH(READCOMMITTED) ") '--������}�X�^ ")
                .AppendLine("WHERE ")
                .AppendLine("	seikyuu_saki_cd = @seikyuu_saki_cd ") '--������R�[�h ")
                .AppendLine("	AND ")
                .AppendLine("	seikyuu_saki_brc = @seikyuu_saki_brc ") '--������}�� ")
                .AppendLine("	AND ")
                .AppendLine("	seikyuu_saki_kbn = @seikyuu_saki_kbn ") '--������敪 ")
            End With

            paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strTysSeikyuuSakiCd))
            paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strTysSeikyuuSakiBrc))
            paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 40, strTysSeikyuuSakiKbn))

            ' �������s(�������ߓ����擾����)
            strSeikyuuSimeDate = Convert.ToString(ExecuteScalar(connStr, CommandType.Text, sql.ToString(), paramList.ToArray))
        End If

        '�i���ߓ������݂��Ȃ����̏ꍇ�A�V�X�e���N���̖����ɐݒ肷��j
        If strSeikyuuSimeDate.Equals(String.Empty) Then
            strSeikyuuSimeDate = Right(StrDup(2, "0") & Convert.ToDateTime(sysDate.Year & "/" & sysDate.Month & "/" & "01").AddMonths(1).AddDays(-1).Day.ToString, 2)
        End If

        Try
            '���������s�� = �V�X�e���N���{�������ߓ� 
            seikyuusyoHakDate = Convert.ToDateTime(sysDate.Year & "/" & sysDate.Month & "/" & strSeikyuuSimeDate)
        Catch ex As Exception
            seikyuusyoHakDate = Convert.ToDateTime(sysDate.Year & "/" & sysDate.Month & "/" & "01").AddMonths(1).AddDays(-1)
        End Try

        '���߂����������s�����V�X�e�����t�̏ꍇ
        If DateTime.Compare(seikyuusyoHakDate, Convert.ToDateTime(sysDate.ToString("yyyy/MM/dd"))) < 0 Then
            '���.���������s�������߂����������s���{1����
            seikyuusyoHakDate = seikyuusyoHakDate.AddMonths(1)
        End If

        '�߂�l
        Return seikyuusyoHakDate

    End Function

    ''' <summary>
    ''' ���i�����擾����
    ''' </summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/09 �ԗ� 407584 �ǉ�</history>
    Public Function SelSyouhinInfo(ByVal strSyouhinCd As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet
        ' SQL���̐���
        Dim sql As New StringBuilder
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        '�W�����i
        Dim strHyoujunKkk As String = String.Empty

        ' SQL��
        With sql
            .AppendLine("SELECT ")
            .AppendLine("	MSH.syouhin_cd ") '--���i�R�[�h ")
            .AppendLine("	,MSH.hyoujun_kkk ") '--�W�����i ")
            .AppendLine("	,MSH.zei_kbn ") '--�ŋ敪 ")
            .AppendLine("	,MSZ.zeiritu ") '--�ŗ� ")
            .AppendLine("FROM ")
            .AppendLine("	m_syouhin AS MSH WITH(READCOMMITTED) ") '--���i�}�X�^ ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_syouhizei AS MSZ WITH(READCOMMITTED) ") '--����Ń}�X�^ ")
            .AppendLine("	ON ")
            .AppendLine("		MSH.zei_kbn = MSZ.zei_kbn ")
            .AppendLine("WHERE ")
            .AppendLine("	syouhin_cd  = @syouhin_cd ") '--���i�R�[�h ")

        End With

        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, sql.ToString(), dsReturn, "dtSyouhinInfo", paramList.ToArray)

        '�߂�l
        Return dsReturn.Tables("dtSyouhinInfo")

    End Function

    ''' <summary>
    ''' �H�������ʖ��擾
    ''' </summary>
    ''' <param name="strKojUriSyubetu">�H��������</param>
    ''' <returns>�H�������ʖ�</returns>
    ''' <history>2013/03/13 �ԗ� 407584 �ǉ�</history>
    Public Function GetKojUriSyubetuMei(ByVal strKojUriSyubetu As String) As Object
        Dim intCode As Integer
        Try
            intCode = CInt(strKojUriSyubetu)
            Dim strSql As String = "SELECT meisyou FROM m_meisyou  WITH(READCOMMITTED) WHERE meisyou_syubetu='55' and  code='" & strKojUriSyubetu & "'"
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return DBNull.Value
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try
    End Function

End Class