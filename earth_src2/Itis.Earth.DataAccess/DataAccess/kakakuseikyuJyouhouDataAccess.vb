
Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

Public Class KakakuseikyuJyouhouDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ':)  :)100713����������������������������������������������������������������������������������������

    ''' <summary>
    ''' [���i�E�������]�p�@�c�Ə��}�X�^����
    ''' </summary>
    ''' <param name="eigyousyo_cd">�c�Ə��R�[�h</param>
    Public Function SelEigyousyoForSeikyusaki(ByVal eigyousyo_cd As String) As Data.DataTable

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New Data.DataSet
        dsReturn.Tables.Add()

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT ")
        '	SELECT �c�D������敪�A�c�D������R�[�h�A�c�D������}�ԁA���D�������ߓ�	
        commandTextSb.AppendLine("  EIG.seikyuu_saki_kbn ")
        commandTextSb.AppendLine(" , EIG.seikyuu_saki_cd ")
        commandTextSb.AppendLine(" , EIG.seikyuu_saki_brc ")
        commandTextSb.AppendLine(" , EIG.seikyuu_saki_mei ")
        commandTextSb.AppendLine(" , SKU.seikyuu_sime_date ")
        '====================��2016/01/29 chel1 �ǉ���====================
        '�u������6 �˓y�n���|�v�̎}�ԁu30�v�̓��ʏ���
        commandTextSb.AppendLine(" , SKU_brc30.seikyuu_sime_date as seikyuu_sime_date_brc30 ")
        '====================��2016/01/29 chel1 �ǉ���====================

        '	FROM �c�Ə��}�X�^ �c		
        '	LEFT JOIN ������}�X�^ ��	
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine(" m_eigyousyo AS EIG WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT JOIN m_seikyuu_saki AS SKU WITH (READCOMMITTED)")
        commandTextSb.AppendLine(" ON ")
        '	     ON �c�D������敪 = ���D������敪 
        '        AND �c�D������R�[�h = ���D������R�[�h 
        '        AND �c�D������}�� = ���D������}��	
        commandTextSb.AppendLine(" EIG.seikyuu_saki_kbn = SKU.seikyuu_saki_kbn ")
        commandTextSb.AppendLine(" AND EIG.seikyuu_saki_cd = SKU.seikyuu_saki_cd ")
        commandTextSb.AppendLine(" AND EIG.seikyuu_saki_brc = SKU.seikyuu_saki_brc ")

        '====================��2016/01/29 chel1 �ǉ���====================
        '�u������6 �˓y�n���|�v�̎}�ԁu30�v�̓��ʏ���
        commandTextSb.AppendLine(" LEFT JOIN m_seikyuu_saki AS SKU_brc30 WITH (READCOMMITTED)")
        commandTextSb.AppendLine(" ON ")
        commandTextSb.AppendLine(" EIG.seikyuu_saki_kbn = SKU_brc30.seikyuu_saki_kbn ")
        commandTextSb.AppendLine(" AND EIG.seikyuu_saki_cd = SKU_brc30.seikyuu_saki_cd ")
        commandTextSb.AppendLine(" AND SKU_brc30.seikyuu_saki_brc = '30' ")
        '====================��2016/01/29 chel1 �ǉ���====================

        '	WHERE �c�D�c�Ə��R�[�h = ��ʁD�c�Ə��R�[�h	
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" EIG.eigyousyo_cd = @eigyousyo_cd ")

        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, eigyousyo_cd))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, dsReturn.Tables(0).TableName, paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ':)  :)100713����������������������������������������������������������������������������������������

    '�F�j100606�@������@�ǉ��@��������������������������������������������������������������������������������������

    ''' <summary>
    ''' [���i�E�������]�p�@������o�^���^�}�X�^����
    ''' </summary>
    ''' <param name="seikyuu_saki_brc">������ЃR�[�h</param>
    ''' <param name="torikesi">���</param>
    Public Function SelSeikyusakiHinaTouroku(ByVal seikyuu_saki_brc As String, _
                                             ByVal torikesi As Integer) As Data.DataTable

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New Data.DataSet
        dsReturn.Tables.Add()

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT ")

        commandTextSb.AppendLine(" seikyuu_saki_brc ")
        commandTextSb.AppendLine(" , torikesi ")
        commandTextSb.AppendLine(" , seikyuu_sime_date ")
        commandTextSb.AppendLine(" , senpou_seikyuu_sime_date ")
        commandTextSb.AppendLine(" , tyk_koj_seikyuu_timing_flg ")
        commandTextSb.AppendLine(" , sousai_flg ")
        commandTextSb.AppendLine(" , kaisyuu_yotei_gessuu ")
        commandTextSb.AppendLine(" , kaisyuu_yotei_date ")
        commandTextSb.AppendLine(" , seikyuusyo_hittyk_date ")
        commandTextSb.AppendLine(" , kaisyuu1_syubetu1 ")
        commandTextSb.AppendLine(" , kaisyuu1_wariai1 ")
        commandTextSb.AppendLine(" , kaisyuu1_tegata_site_gessuu ")
        commandTextSb.AppendLine(" , kaisyuu1_tegata_site_date ")
        commandTextSb.AppendLine(" , kaisyuu1_seikyuusyo_yousi ")
        commandTextSb.AppendLine(" , kaisyuu1_syubetu2 ")
        commandTextSb.AppendLine(" , kaisyuu1_wariai2 ")
        commandTextSb.AppendLine(" , kaisyuu1_syubetu3 ")
        commandTextSb.AppendLine(" , kaisyuu1_wariai3 ")
        commandTextSb.AppendLine(" , kaisyuu_kyoukaigaku ")
        commandTextSb.AppendLine(" , kaisyuu2_syubetu1 ")
        commandTextSb.AppendLine(" , kaisyuu2_wariai1 ")
        commandTextSb.AppendLine(" , kaisyuu2_tegata_site_gessuu ")
        commandTextSb.AppendLine(" , kaisyuu2_tegata_site_date ")
        commandTextSb.AppendLine(" , kaisyuu2_seikyuusyo_yousi ")
        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
        commandTextSb.AppendLine(" , ginkou_siten_cd ")           '��s�x�X�R�[�h
        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
        commandTextSb.AppendLine(" FROM ")

        commandTextSb.AppendLine(" m_seikyuu_saki_touroku_hinagata WITH (READCOMMITTED) ")

        commandTextSb.AppendLine(" WHERE ")

        commandTextSb.AppendLine(" seikyuu_saki_brc = @seikyuu_saki_brc ")
        commandTextSb.AppendLine(" AND torikesi = @torikesi ")

        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, seikyuu_saki_brc))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 5, torikesi))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, dsReturn.Tables(0).TableName, paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@�����搗�`�}�X�^����
    ''' </summary>
    ''' <param name="seikyuu_saki_brc">������}��</param>
    ''' <param name="torikesi">���</param>
    Public Function SelSeikyusakiHina(ByVal seikyuu_saki_brc As String, _
                                    ByVal torikesi As Integer) As Data.DataTable

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New Data.DataSet
        dsReturn.Tables.Add()

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  seikyuu_saki_brc ")
        commandTextSb.AppendLine(" , torikesi ")
        commandTextSb.AppendLine(" , hyouji_naiyou ")
        commandTextSb.AppendLine(" , seikyuu_sime_date ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine(" m_seikyuu_saki_touroku_hinagata WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE ")

        commandTextSb.AppendLine(" seikyuu_saki_brc = @seikyuu_saki_brc ")
        commandTextSb.AppendLine(" AND torikesi = @torikesi ")

        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, seikyuu_saki_brc))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 5, torikesi))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, dsReturn.Tables(0).TableName, paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@�c�Ə��}�X�^����
    ''' </summary>
    ''' <param name="eigyousyo_cd">�c�Ə��R�[�h</param>
    ''' <param name="torikesi">���</param>
    Public Function SelEigyousyo(ByVal eigyousyo_cd As String, _
                                    ByVal torikesi As Integer) As Data.DataTable

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New Data.DataSet
        dsReturn.Tables.Add()

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT ")

        commandTextSb.AppendLine("  eigyousyo_cd ")
        commandTextSb.AppendLine(" , torikesi ")
        commandTextSb.AppendLine(" , eigyousyo_mei ")
        commandTextSb.AppendLine(" , eigyousyo_kana ")
        commandTextSb.AppendLine(" , seikyuu_saki_mei ")
        commandTextSb.AppendLine(" , seikyuu_saki_kana ")

        commandTextSb.AppendLine(" FROM ")

        commandTextSb.AppendLine(" m_eigyousyo WITH (READCOMMITTED) ")

        commandTextSb.AppendLine(" WHERE ")

        commandTextSb.AppendLine(" eigyousyo_cd = @eigyousyo_cd ")
        If torikesi >= 0 Then
            commandTextSb.AppendLine(" AND torikesi = @torikesi ")
        End If


        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, eigyousyo_cd))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 5, torikesi))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, dsReturn.Tables(0).TableName, paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@������Ѓ}�X�^����
    ''' </summary>
    ''' <param name="tys_kaisya_cd">������ЃR�[�h</param>
    ''' <param name="jigyousyo_cd">���Ə��R�[�h</param>
    ''' <param name="torikesi">���</param>
    Public Function SelTyousakaisya(ByVal tys_kaisya_cd As String, _
                                    ByVal jigyousyo_cd As String, _
                                    ByVal torikesi As Integer) As Data.DataTable

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New Data.DataSet
        dsReturn.Tables.Add()

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT ")

        commandTextSb.AppendLine("  tys_kaisya_cd ")
        commandTextSb.AppendLine(" , jigyousyo_cd ")
        commandTextSb.AppendLine(" , torikesi ")
        commandTextSb.AppendLine(" , tys_kaisya_mei ")
        commandTextSb.AppendLine(" , tys_kaisya_mei_kana ")
        commandTextSb.AppendLine(" , seikyuu_saki_shri_saki_mei ")
        commandTextSb.AppendLine(" , seikyuu_saki_shri_saki_kana ")

        commandTextSb.AppendLine(" FROM ")

        commandTextSb.AppendLine(" m_tyousakaisya WITH (READCOMMITTED) ")

        commandTextSb.AppendLine(" WHERE ")

        commandTextSb.AppendLine(" tys_kaisya_cd = @tys_kaisya_cd ")
        commandTextSb.AppendLine(" AND jigyousyo_cd = @jigyousyo_cd ")
        If torikesi >= 0 Then
            commandTextSb.AppendLine(" AND torikesi = @torikesi ")
        End If


        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, tys_kaisya_cd))
        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, jigyousyo_cd))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 5, torikesi))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, dsReturn.Tables(0).TableName, paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@������}�X�^����
    ''' </summary>
    ''' <param name="seikyuu_saki_kbn">������敪</param>
    ''' <param name="seikyuu_saki_cd">������R�[�h</param>
    ''' <param name="seikyuu_saki_brc">������}��</param>
    ''' <param name="torikesi">���</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyusaki(ByVal seikyuu_saki_kbn As String, _
                                    ByVal seikyuu_saki_cd As String, _
                                    ByVal seikyuu_saki_brc As String, _
                                    ByVal torikesi As Integer, _
                           Optional ByVal torikesiKbn As Boolean = True) As Data.DataTable

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New Data.DataSet
        dsReturn.Tables.Add()

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  seikyuu_saki_cd ")
        commandTextSb.AppendLine(" , seikyuu_saki_brc ")
        commandTextSb.AppendLine(" , seikyuu_saki_kbn ")
        commandTextSb.AppendLine(" , torikesi ")
        commandTextSb.AppendLine(" , seikyuu_sime_date ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine(" m_seikyuu_saki WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" seikyuu_saki_kbn IS NOT NULL ")


        If torikesiKbn Then
            commandTextSb.AppendLine(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 5, torikesi))
        End If
        If seikyuu_saki_kbn.Trim <> "" Then
            commandTextSb.AppendLine(" AND seikyuu_saki_kbn = @seikyuu_saki_kbn ")
            paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, seikyuu_saki_kbn))
        End If

        If seikyuu_saki_cd.Trim <> "" Then
            commandTextSb.AppendLine(" AND seikyuu_saki_cd = @seikyuu_saki_cd ")
            paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, seikyuu_saki_cd))
        End If

        If seikyuu_saki_brc.Trim <> "" Then
            commandTextSb.AppendLine(" AND seikyuu_saki_brc = @seikyuu_saki_brc ")
            paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, seikyuu_saki_brc))
        End If



        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, dsReturn.Tables(0).TableName, paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@��{�Z�[�g�p�f�[�^����
    ''' </summary>
    ''' <param name="seikyuuSakiCd">������R�[�h:��ʉ����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelKihonsetData(ByVal seikyuuSakiCd As String) As Data.DataTable

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New Data.DataSet
        dsReturn.Tables.Add()

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT TOP 1 ")
        commandTextSb.AppendLine(" SKU.seikyuu_saki_kbn ")
        commandTextSb.AppendLine(" ,SKU.seikyuu_sime_date AS SKU_sime_date")
        commandTextSb.AppendLine(" ,HINA.seikyuu_saki_brc ")
        commandTextSb.AppendLine(" ,HINA.kihon_flg ")
        commandTextSb.AppendLine(" ,HINA.seikyuu_sime_date AS HINA_sime_date")
        commandTextSb.AppendLine(" ,HINA.hyouji_naiyou ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine(" m_seikyuu_saki_touroku_hinagata AS HINA WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT JOIN m_seikyuu_saki AS SKU WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON SKU.seikyuu_saki_brc = HINA.seikyuu_saki_brc ")
        commandTextSb.AppendLine(" AND SKU.seikyuu_saki_kbn = 0 ")
        commandTextSb.AppendLine(" AND SKU.seikyuu_saki_cd = @seikyuu_saki_cd ")
        commandTextSb.AppendLine(" AND SKU.torikesi = 0 ")
        commandTextSb.AppendLine(" WHERE HINA.kihon_flg = 1 ")
        commandTextSb.AppendLine(" ORDER BY HINA.seikyuu_saki_brc ")

        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, seikyuuSakiCd))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, dsReturn.Tables(0).TableName, paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function
    ''' <summary>
    ''' [���i�E�������]�p�@��{�Z�[�g�p�f�[�^����
    ''' (�u������6 �˓y�n���|�v�̎}�ԁu30�v�̓��ʏ���)
    ''' </summary>
    '''<history>2016/01/29 chel1 �ǉ�</history>
    Public Function SelKihonsetDataBrc30(ByVal seikyuuSakiCd As String) As Data.DataTable

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New Data.DataSet
        dsReturn.Tables.Add()

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" SKU.seikyuu_saki_kbn ")
        commandTextSb.AppendLine(" ,SKU.seikyuu_sime_date AS SKU_sime_date")
        commandTextSb.AppendLine(" ,HINA.seikyuu_saki_brc ")
        commandTextSb.AppendLine(" ,HINA.kihon_flg ")
        commandTextSb.AppendLine(" ,HINA.seikyuu_sime_date AS HINA_sime_date")
        commandTextSb.AppendLine(" ,HINA.hyouji_naiyou ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine(" m_seikyuu_saki_touroku_hinagata AS HINA WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT JOIN m_seikyuu_saki AS SKU WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON SKU.seikyuu_saki_brc = HINA.seikyuu_saki_brc ")
        commandTextSb.AppendLine(" AND SKU.seikyuu_saki_kbn = 0 ")
        commandTextSb.AppendLine(" AND SKU.seikyuu_saki_cd = @seikyuu_saki_cd ")
        commandTextSb.AppendLine(" AND SKU.torikesi = 0 ")
        commandTextSb.AppendLine(" WHERE HINA.seikyuu_saki_brc = '30' ")

        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, seikyuuSakiCd))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, dsReturn.Tables(0).TableName, paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@�g�����̃}�X�^.������敪����
    ''' </summary>
    ''' <param name="Syubetu">���</param>
    ''' <returns>�g�����̃}�X�^</returns>
    ''' <remarks></remarks>
    Public Function SelSeikyusakiKbn(ByVal Syubetu As String) As Data.DataTable

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New Data.DataSet
        dsReturn.Tables.Add()

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  meisyou_syubetu ")
        commandTextSb.AppendLine(" , code ")
        commandTextSb.AppendLine(" , meisyou ")
        commandTextSb.AppendLine(" , hyouji_jyun ")
        'commandTextSb.AppendLine(" , hannyou_cd ")
        'commandTextSb.AppendLine(" , hannyou_no ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine(" m_kakutyou_meisyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" meisyou_syubetu = @Syubetu ")

        paramList.Add(MakeParam("@Syubetu", SqlDbType.VarChar, 10, Syubetu))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, dsReturn.Tables(0).TableName, paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    '������@�ǉ��@��������������������������������������������������������������������������������������������������

    ''' <summary>
    ''' [���i�E�������]�p�@�����X����
    ''' </summary>
    ''' <param name="KametenCd">�����X�R�[�h</param>
    ''' <returns>�����X�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function SelKameiten(ByVal KametenCd As String) As KameitenDataSet.m_kameitenTableDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New KameitenDataSet
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" kbn ")
        commandTextSb.AppendLine(" , kameiten_cd ")
        commandTextSb.AppendLine(" , torikesi ")
        commandTextSb.AppendLine(" , kameiten_mei1 ")
        commandTextSb.AppendLine(" , tenmei_kana1 ")
        commandTextSb.AppendLine(" , kameiten_mei2 ")
        commandTextSb.AppendLine(" , tenmei_kana2 ")
        commandTextSb.AppendLine(" , kameiten_seisiki_mei ")
        commandTextSb.AppendLine(" , kameiten_seisiki_mei_kana ")
        commandTextSb.AppendLine(" , eigyousyo_cd ")
        commandTextSb.AppendLine(" , keiretu_cd ")
        commandTextSb.AppendLine(" , th_kasi_cd ")
        commandTextSb.AppendLine(" , danmenzu1 ")
        commandTextSb.AppendLine(" , danmenzu2 ")
        commandTextSb.AppendLine(" , danmenzu3 ")
        commandTextSb.AppendLine(" , danmenzu4 ")
        commandTextSb.AppendLine(" , danmenzu5 ")
        commandTextSb.AppendLine(" , danmenzu6 ")
        commandTextSb.AppendLine(" , danmenzu7 ")
        commandTextSb.AppendLine(" , tys_seikyuu_saki ")
        commandTextSb.AppendLine(" , tys_seikyuu_saki_cd ")
        commandTextSb.AppendLine(" , tys_seikyuu_saki_brc ")
        commandTextSb.AppendLine(" , tys_seikyuu_saki_kbn ")
        commandTextSb.AppendLine(" , tys_seikyuu_sime_date ")
        commandTextSb.AppendLine(" , koj_seikyuusaki ")
        commandTextSb.AppendLine(" , koj_seikyuu_saki_cd ")
        commandTextSb.AppendLine(" , koj_seikyuu_saki_brc ")
        commandTextSb.AppendLine(" , koj_seikyuu_saki_kbn ")
        commandTextSb.AppendLine(" , koj_seikyuu_sime_date ")
        commandTextSb.AppendLine(" , hansokuhin_seikyuusaki ")
        commandTextSb.AppendLine(" , hansokuhin_seikyuu_saki_cd ")
        commandTextSb.AppendLine(" , hansokuhin_seikyuu_saki_brc ")
        commandTextSb.AppendLine(" , hansokuhin_seikyuu_saki_kbn ")
        commandTextSb.AppendLine(" , hansokuhin_seikyuu_sime_date ")
        commandTextSb.AppendLine(" , tatemono_seikyuu_saki_cd ")
        commandTextSb.AppendLine(" , tatemono_seikyuu_saki_brc ")
        commandTextSb.AppendLine(" , tatemono_seikyuu_saki_kbn ")
        '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
        commandTextSb.AppendLine(" , seikyuu_saki_cd5 ")
        commandTextSb.AppendLine(" , seikyuu_saki_brc5 ")
        commandTextSb.AppendLine(" , seikyuu_saki_kbn5 ")
        commandTextSb.AppendLine(" , seikyuu_saki_cd6 ")
        commandTextSb.AppendLine(" , seikyuu_saki_brc6 ")
        commandTextSb.AppendLine(" , seikyuu_saki_kbn6 ")
        commandTextSb.AppendLine(" , seikyuu_saki_cd7 ")
        commandTextSb.AppendLine(" , seikyuu_saki_brc7 ")
        commandTextSb.AppendLine(" , seikyuu_saki_kbn7 ")
        '==================2011/06/28 �ԗ� �ǉ� �I����==========================
        commandTextSb.AppendLine(" , web_moushikomi_saiban_hanbetu_flg ")
        commandTextSb.AppendLine(" , hattyuusyo_michaku_renkei_taisyougai_flg ")



        commandTextSb.AppendLine(" , ss_kkk ")
        commandTextSb.AppendLine(" , sai_tys_kkk ")
        commandTextSb.AppendLine(" , ssgr_kkk ")
        commandTextSb.AppendLine(" , kaiseki_hosyou_kkk ")
        commandTextSb.AppendLine(" , hosyounasi_umu ")
        commandTextSb.AppendLine(" , kaiyaku_haraimodosi_kkk ")
        commandTextSb.AppendLine(" , todouhuken_cd ")
        commandTextSb.AppendLine(" , hosyou_kikan ")
        commandTextSb.AppendLine(" , hosyousyo_hak_umu ")
        commandTextSb.AppendLine(" , builder_no ")
        commandTextSb.AppendLine(" , koj_gaisya_seikyuu_umu ")
        commandTextSb.AppendLine(" , koj_tantou_flg ")
        commandTextSb.AppendLine(" , nenkan_tousuu ")
        commandTextSb.AppendLine(" , nyuukin_kakunin_jyouken ")
        commandTextSb.AppendLine(" , nyuukin_kakunin_oboegaki ")
        commandTextSb.AppendLine(" , eigyou_tantousya_mei ")
        commandTextSb.AppendLine(" , tys_mitsyo_flg ")
        commandTextSb.AppendLine(" , hattyuusyo_flg ")
        commandTextSb.AppendLine(" , mitsyo_file_mei ")
        commandTextSb.AppendLine(" , jizen_tys_kkk ")
        commandTextSb.AppendLine(" , jisin_hosyou_flg ")
        commandTextSb.AppendLine(" , jisin_hosyou_add_date ")
        commandTextSb.AppendLine(" , add_login_user_id ")
        commandTextSb.AppendLine(" , add_datetime ")
        commandTextSb.AppendLine(" , upd_login_user_id ")
        commandTextSb.AppendLine(" , upd_datetime ")

        '==================2017/01/01 ������ �ǉ� �t�󉻓���Ǘ� �V����ؑ֓���==========================
        commandTextSb.AppendLine(" , ekijyouka_tokuyaku_kanri ")
        commandTextSb.AppendLine(" , shintokuyaku_kirikaedate ")
        '==================2017/01/01 ������ �ǉ� �t�󉻓���Ǘ� �V����ؑ֓���==========================
        commandTextSb.AppendLine(" , shitei_seikyuusyo_umu ")
        commandTextSb.AppendLine(" , shiroari_kensa_hyouji ")

        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine(" m_kameiten WITH (READCOMMITTED)")
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd  ")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, KametenCd))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "m_kameitenTable", paramList.ToArray)

        Return dsReturn.m_kameitenTable

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@������������
    ''' </summary>
    ''' <param name="KametenCd">�����X�R�[�h</param>
    ''' <returns>�����e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function SelTatou(ByVal KametenCd As String) As KameitenDataSet.m_tatouwaribiki_setteiTableDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New KameitenDataSet
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  tatou.toukubun ")
        commandTextSb.AppendLine(" , tatou.syouhin_cd ")

        commandTextSb.AppendLine(" , tatou.add_login_user_id ")
        commandTextSb.AppendLine(" , tatou.add_datetime ")
        commandTextSb.AppendLine(" , tatou.upd_login_user_id ")
        commandTextSb.AppendLine(" , tatou.upd_datetime ")
        commandTextSb.AppendLine(" , syouhin.syouhin_mei ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine(" m_tatouwaribiki_settei AS tatou  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT JOIN m_syouhin AS syouhin  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON tatou.syouhin_cd = syouhin.syouhin_cd ")
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" tatou.kameiten_cd = @kameiten_cd ")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, KametenCd))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "m_tatouwaribiki_setteiTable", paramList.ToArray)

        Return dsReturn.m_tatouwaribiki_setteiTable

    End Function

    '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
    ''' <summary>
    ''' [���i�E�������]�p�@������������
    ''' </summary>
    ''' <param name="KametenCd">�����X�R�[�h</param>
    ''' <returns>�����e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function SelTatouKakaku(ByVal KametenCd As String) As Data.DataTable

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New Data.DataSet
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  tatou.toukubun ")
        commandTextSb.AppendLine(" , tatou.syouhin_cd ")

        commandTextSb.AppendLine(" , tatou.add_login_user_id ")
        commandTextSb.AppendLine(" , tatou.add_datetime ")
        commandTextSb.AppendLine(" , tatou.upd_login_user_id ")
        commandTextSb.AppendLine(" , tatou.upd_datetime ")
        commandTextSb.AppendLine(" , ISNULL(syouhin.syouhin_mei,'') AS syouhin_mei ")
        commandTextSb.AppendLine(" , ISNULL(CONVERT(VARCHAR(10),syouhin.hyoujun_kkk),'') AS hyoujun_kkk ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine(" m_tatouwaribiki_settei AS tatou  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT JOIN m_syouhin AS syouhin  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON tatou.syouhin_cd = syouhin.syouhin_cd ")
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" tatou.kameiten_cd = @kameiten_cd ")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, KametenCd))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "m_tatouwaribiki_setteiTable", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function
    '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================

    ''' <summary>
    ''' [���i�E�������]�p�@���i�}�X�^�����A���i�R�[�h�`�F�b�N
    ''' </summary>
    ''' <param name="syouhinCd">���i�R�[�h</param>
    ''' <returns>TRUE:����,FALSE:���s</returns>
    ''' <remarks></remarks>
    Public Function SelsyouhinCd(ByVal syouhinCd As String) As Boolean

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New DataSet
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  COUNT (syouhin_cd) AS cnt ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine(" m_syouhin WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" syouhin_cd = @syouhin_cd ")

        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 5, syouhinCd))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtReturn", paramList.ToArray)

        Return dsReturn.Tables(0).Rows(0).Item("cnt")

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@���������}�X�^�A�r���`�F�b�N
    ''' </summary>
    ''' <param name="KametenCd">�����X�R�[�h</param>
    ''' <param name="toukubun">���敪</param>
    ''' <returns>�r���f�[�^</returns>
    ''' <remarks></remarks>
    Public Function SelTatouHaita(ByVal KametenCd As String, ByVal toukubun As Int16) As String

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New DataSet
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  kameiten_cd ")
        commandTextSb.AppendLine(" , toukubun ")
        commandTextSb.AppendLine(" , ISNULL(upd_login_user_id,'') ")
        commandTextSb.AppendLine(" , ISNULL(upd_datetime,'')  ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("  m_tatouwaribiki_settei WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd ")
        commandTextSb.AppendLine(" AND toukubun = @toukubun ")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, KametenCd))
        paramList.Add(MakeParam("@toukubun", SqlDbType.SmallInt, 1, toukubun))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtReturn", paramList.ToArray)

        If dsReturn.Tables(0).Rows.Count <> 0 Then
            Return dsReturn.Tables(0).Rows(0).Item(2) & "," & dsReturn.Tables(0).Rows(0).Item(3)
        Else
            Return ""
        End If

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@���������}�X�^�A�d���`�F�b�N(1,2,3�̂�)
    ''' </summary>
    ''' <param name="KametenCd">�����X�R�[�h</param>
    ''' <returns>���ݔz��</returns>
    ''' <remarks></remarks>
    Public Function SelTatouJyufuku(ByVal KametenCd As String) As Boolean()

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New DataSet
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim kubun As Integer

        For kubun = 1 To 3

            commandTextSb.AppendLine(" SELECT ")
            commandTextSb.AppendLine("  COUNT(*) AS cnt ")
            commandTextSb.AppendLine(" FROM ")
            commandTextSb.AppendLine("  m_tatouwaribiki_settei WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" WHERE ")
            commandTextSb.AppendLine("  kameiten_cd = @kameiten_cd ")
            commandTextSb.AppendLine(" AND toukubun = '" & kubun & "' ")
            If kubun = 3 Then
                Exit For
            End If
            commandTextSb.AppendLine(" UNION ALL")
        Next

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, KametenCd))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtReturn", paramList.ToArray)

        Dim ArrReturn(2) As Boolean

        For kubun = 0 To 2

            ArrReturn(kubun) = dsReturn.Tables(0).Rows(kubun).Item(0)

        Next

        Return ArrReturn

    End Function

    ''' <summary>
    ''' [���i�E�������]������e�[�u���A�}���܂��X�V
    ''' </summary>
    ''' <param name="dtSKU">�����e�[�u��</param>
    ''' <returns>TRUE:����,FALSE:���s</returns>
    ''' <remarks></remarks>
    Public Function InsSeikyusaki_for_kakaku(ByVal dtSKU As Data.DataTable) As Boolean

        '�߂�l
        InsSeikyusaki_for_kakaku = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" m_seikyuu_saki ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" (")
        commandTextSb.AppendLine(" seikyuu_saki_cd, ")
        commandTextSb.AppendLine(" seikyuu_saki_brc, ")
        commandTextSb.AppendLine(" seikyuu_saki_kbn, ")
        commandTextSb.AppendLine(" torikesi, ")
        commandTextSb.AppendLine(" seikyuu_sime_date, ")
        commandTextSb.AppendLine(" senpou_seikyuu_sime_date, ")
        commandTextSb.AppendLine(" tyk_koj_seikyuu_timing_flg, ")
        commandTextSb.AppendLine(" sousai_flg, ")
        commandTextSb.AppendLine(" kaisyuu_yotei_gessuu, ")
        commandTextSb.AppendLine(" kaisyuu_yotei_date, ")
        commandTextSb.AppendLine(" seikyuusyo_hittyk_date, ")
        commandTextSb.AppendLine(" kaisyuu1_syubetu1, ")
        commandTextSb.AppendLine(" kaisyuu1_wariai1, ")
        commandTextSb.AppendLine(" kaisyuu1_tegata_site_gessuu, ")
        commandTextSb.AppendLine(" kaisyuu1_tegata_site_date, ")
        commandTextSb.AppendLine(" kaisyuu1_seikyuusyo_yousi, ")
        commandTextSb.AppendLine(" kaisyuu1_syubetu2, ")
        commandTextSb.AppendLine(" kaisyuu1_wariai2, ")
        commandTextSb.AppendLine(" kaisyuu1_syubetu3, ")
        commandTextSb.AppendLine(" kaisyuu1_wariai3, ")
        commandTextSb.AppendLine(" kaisyuu_kyoukaigaku, ")
        commandTextSb.AppendLine(" kaisyuu2_syubetu1, ")
        commandTextSb.AppendLine(" kaisyuu2_wariai1, ")
        commandTextSb.AppendLine(" kaisyuu2_tegata_site_gessuu, ")
        commandTextSb.AppendLine(" kaisyuu2_tegata_site_date, ")
        commandTextSb.AppendLine(" kaisyuu2_seikyuusyo_yousi, ")
        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
        commandTextSb.AppendLine(" ginkou_siten_cd, ")        '��s�x�X�R�[�h
        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
        commandTextSb.AppendLine(" add_login_user_id, ")
        commandTextSb.AppendLine(" add_datetime")
        commandTextSb.AppendLine(" ) ")
        commandTextSb.AppendLine(" SELECT")
        commandTextSb.AppendLine(" @seikyuu_saki_cd, ")
        commandTextSb.AppendLine(" @seikyuu_saki_brc, ")
        commandTextSb.AppendLine(" @seikyuu_saki_kbn, ")
        commandTextSb.AppendLine(" @torikesi, ")
        commandTextSb.AppendLine(" @seikyuu_sime_date, ")
        commandTextSb.AppendLine(" @senpou_seikyuu_sime_date, ")
        commandTextSb.AppendLine(" @tyk_koj_seikyuu_timing_flg, ")
        commandTextSb.AppendLine(" @sousai_flg, ")
        commandTextSb.AppendLine(" @kaisyuu_yotei_gessuu, ")
        commandTextSb.AppendLine(" @kaisyuu_yotei_date, ")
        commandTextSb.AppendLine(" @seikyuusyo_hittyk_date, ")
        commandTextSb.AppendLine(" @kaisyuu1_syubetu1, ")
        commandTextSb.AppendLine(" @kaisyuu1_wariai1, ")
        commandTextSb.AppendLine(" @kaisyuu1_tegata_site_gessuu, ")
        commandTextSb.AppendLine(" @kaisyuu1_tegata_site_date, ")
        commandTextSb.AppendLine(" @kaisyuu1_seikyuusyo_yousi, ")
        commandTextSb.AppendLine(" @kaisyuu1_syubetu2, ")
        commandTextSb.AppendLine(" @kaisyuu1_wariai2, ")
        commandTextSb.AppendLine(" @kaisyuu1_syubetu3, ")
        commandTextSb.AppendLine(" @kaisyuu1_wariai3, ")
        commandTextSb.AppendLine(" @kaisyuu_kyoukaigaku, ")
        commandTextSb.AppendLine(" @kaisyuu2_syubetu1, ")
        commandTextSb.AppendLine(" @kaisyuu2_wariai1, ")
        commandTextSb.AppendLine(" @kaisyuu2_tegata_site_gessuu, ")
        commandTextSb.AppendLine(" @kaisyuu2_tegata_site_date, ")
        commandTextSb.AppendLine(" @kaisyuu2_seikyuusyo_yousi, ")
        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
        commandTextSb.AppendLine(" @ginkou_siten_cd, ")        '��s�x�X�R�[�h
        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
        commandTextSb.AppendLine(" @add_login_user_id, ")
        commandTextSb.AppendLine(" getdate() ;")

        '�p�����[�^�̐ݒ�

        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, dtSKU.Rows(0).Item("seikyuu_saki_cd")))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, dtSKU.Rows(0).Item("seikyuu_saki_brc")))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, dtSKU.Rows(0).Item("seikyuu_saki_kbn")))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 3, dtSKU.Rows(0).Item("torikesi")))
        paramList.Add(MakeParam("@seikyuu_sime_date", SqlDbType.VarChar, 2, dtSKU.Rows(0).Item("seikyuu_sime_date")))
        paramList.Add(MakeParam("@senpou_seikyuu_sime_date", SqlDbType.VarChar, 2, dtSKU.Rows(0).Item("senpou_seikyuu_sime_date")))
        paramList.Add(MakeParam("@tyk_koj_seikyuu_timing_flg", SqlDbType.Int, 8, dtSKU.Rows(0).Item("tyk_koj_seikyuu_timing_flg")))
        paramList.Add(MakeParam("@sousai_flg", SqlDbType.Int, 8, dtSKU.Rows(0).Item("sousai_flg")))
        paramList.Add(MakeParam("@kaisyuu_yotei_gessuu", SqlDbType.Int, 8, dtSKU.Rows(0).Item("kaisyuu_yotei_gessuu")))
        paramList.Add(MakeParam("@kaisyuu_yotei_date", SqlDbType.VarChar, 2, dtSKU.Rows(0).Item("kaisyuu_yotei_date")))
        paramList.Add(MakeParam("@seikyuusyo_hittyk_date", SqlDbType.VarChar, 2, dtSKU.Rows(0).Item("seikyuusyo_hittyk_date")))


        paramList.Add(MakeParam("@kaisyuu1_syubetu1", SqlDbType.VarChar, 10, dtSKU.Rows(0).Item("kaisyuu1_syubetu1")))
        paramList.Add(MakeParam("@kaisyuu1_wariai1", SqlDbType.Int, 8, dtSKU.Rows(0).Item("kaisyuu1_wariai1")))
        paramList.Add(MakeParam("@kaisyuu1_tegata_site_gessuu", SqlDbType.Int, 8, dtSKU.Rows(0).Item("kaisyuu1_tegata_site_gessuu")))
        paramList.Add(MakeParam("@kaisyuu1_tegata_site_date", SqlDbType.VarChar, 2, dtSKU.Rows(0).Item("kaisyuu1_tegata_site_date")))
        paramList.Add(MakeParam("@kaisyuu1_seikyuusyo_yousi", SqlDbType.VarChar, 10, dtSKU.Rows(0).Item("kaisyuu1_seikyuusyo_yousi")))

        paramList.Add(MakeParam("@kaisyuu1_syubetu2", SqlDbType.VarChar, 10, dtSKU.Rows(0).Item("kaisyuu1_syubetu2")))
        paramList.Add(MakeParam("@kaisyuu1_wariai2", SqlDbType.Int, 8, dtSKU.Rows(0).Item("kaisyuu1_wariai2")))
        paramList.Add(MakeParam("@kaisyuu1_syubetu3", SqlDbType.VarChar, 10, dtSKU.Rows(0).Item("kaisyuu1_syubetu3")))
        paramList.Add(MakeParam("@kaisyuu1_wariai3", SqlDbType.Int, 8, dtSKU.Rows(0).Item("kaisyuu1_wariai3")))
        paramList.Add(MakeParam("@kaisyuu_kyoukaigaku", SqlDbType.Int, 8, dtSKU.Rows(0).Item("kaisyuu_kyoukaigaku")))

        paramList.Add(MakeParam("@kaisyuu2_syubetu1", SqlDbType.VarChar, 10, dtSKU.Rows(0).Item("kaisyuu2_syubetu1")))
        paramList.Add(MakeParam("@kaisyuu2_wariai1", SqlDbType.Int, 8, dtSKU.Rows(0).Item("kaisyuu2_wariai1")))
        paramList.Add(MakeParam("@kaisyuu2_tegata_site_gessuu", SqlDbType.Int, 8, dtSKU.Rows(0).Item("kaisyuu2_tegata_site_gessuu")))
        paramList.Add(MakeParam("@kaisyuu2_tegata_site_date", SqlDbType.VarChar, 2, dtSKU.Rows(0).Item("kaisyuu2_tegata_site_date")))
        paramList.Add(MakeParam("@kaisyuu2_seikyuusyo_yousi", SqlDbType.VarChar, 10, dtSKU.Rows(0).Item("kaisyuu2_seikyuusyo_yousi")))
        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
        paramList.Add(MakeParam("@ginkou_siten_cd", SqlDbType.VarChar, 3, dtSKU.Rows(0).Item("ginkou_siten_cd"))) '��s�x�X�R�[�h
        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dtSKU.Rows(0).Item("add_login_user_id")))
        'paramList.Add(MakeParam("@add_datetime", SqlDbType.VarChar, 1, dtSKU.Rows(0).Item("add_datetime")))

        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        InsSeikyusaki_for_kakaku = True

    End Function

    ''' <summary>
    ''' [���i�E�������]�p���������}�X�^,���������}�X�^�A�g�Ǘ��e�[�u���A�}���܂��X�V
    ''' </summary>
    ''' <param name="dtTatou">�����e�[�u��</param>
    ''' <param name="kousinFlg">�X�V�t���O</param>
    ''' <returns>TRUE:����,FALSE:���s</returns>
    ''' <remarks></remarks>
    Public Function InsUpdTatou(ByVal dtTatou As TatouwaribikiSetteiDataSet.m_tatouwaribiki_setteiDataTable, ByVal kousinFlg As String) As Boolean

        '�߂�l
        InsUpdTatou = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        If kousinFlg = "ins" Then

            commandTextSb.AppendLine(" INSERT INTO ")
            commandTextSb.AppendLine(" m_tatouwaribiki_settei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" (kameiten_cd,")
            commandTextSb.AppendLine(" toukubun, ")
            commandTextSb.AppendLine(" syouhin_cd, ")
            commandTextSb.AppendLine(" add_login_user_id, ")
            commandTextSb.AppendLine(" add_datetime, ")
            commandTextSb.AppendLine(" upd_login_user_id, ")
            commandTextSb.AppendLine(" upd_datetime) ")
            commandTextSb.AppendLine(" SELECT")
            commandTextSb.AppendLine(" @kameiten_cd, ")
            commandTextSb.AppendLine(" @toukubun, ")
            commandTextSb.AppendLine(" @syouhin_cd, ")
            commandTextSb.AppendLine(" @add_login_user_id, ")
            commandTextSb.AppendLine(" getdate() ,")
            commandTextSb.AppendLine(" @add_login_user_id, ")
            commandTextSb.AppendLine(" getdate() ;")

            commandTextSb.AppendLine(" DELETE FROM ")
            commandTextSb.AppendLine(" m_tatouwaribiki_settei_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" WHERE ")
            commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd")
            commandTextSb.AppendLine(" AND ")
            commandTextSb.AppendLine(" toukubun = @toukubun ;")

            commandTextSb.AppendLine(" INSERT INTO ")
            commandTextSb.AppendLine(" m_tatouwaribiki_settei_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" (kameiten_cd,")
            commandTextSb.AppendLine(" toukubun, ")
            commandTextSb.AppendLine(" renkei_siji_cd, ")
            commandTextSb.AppendLine(" sousin_jyky_cd, ")
            commandTextSb.AppendLine(" upd_login_user_id, ")
            commandTextSb.AppendLine(" upd_datetime) ")
            commandTextSb.AppendLine(" SELECT")
            commandTextSb.AppendLine(" @kameiten_cd, ")
            commandTextSb.AppendLine(" @toukubun, ")
            commandTextSb.AppendLine(" @renkei_siji_cd_ins, ")
            commandTextSb.AppendLine(" @sousin_jyky_cd, ")
            commandTextSb.AppendLine(" @add_login_user_id, ")
            commandTextSb.AppendLine(" getdate() ;")

        ElseIf kousinFlg = "upd" Then

            commandTextSb.AppendLine(" UPDATE ")
            commandTextSb.AppendLine(" m_tatouwaribiki_settei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" SET")
            commandTextSb.AppendLine(" syouhin_cd = @syouhin_cd, ")
            commandTextSb.AppendLine(" upd_login_user_id = @upd_login_user_id, ")
            commandTextSb.AppendLine(" upd_datetime = getdate() ")
            commandTextSb.AppendLine(" WHERE ")
            commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd")
            commandTextSb.AppendLine(" AND ")
            commandTextSb.AppendLine(" toukubun = @toukubun ;")

            commandTextSb.AppendLine(" DELETE FROM ")
            commandTextSb.AppendLine(" m_tatouwaribiki_settei_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" WHERE ")
            commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd")
            commandTextSb.AppendLine(" AND ")
            commandTextSb.AppendLine(" toukubun = @toukubun ;")

            commandTextSb.AppendLine(" INSERT INTO ")
            commandTextSb.AppendLine(" m_tatouwaribiki_settei_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" (kameiten_cd,")
            commandTextSb.AppendLine(" toukubun, ")
            commandTextSb.AppendLine(" renkei_siji_cd, ")
            commandTextSb.AppendLine(" sousin_jyky_cd, ")
            commandTextSb.AppendLine(" upd_login_user_id, ")
            commandTextSb.AppendLine(" upd_datetime) ")
            commandTextSb.AppendLine(" SELECT")
            commandTextSb.AppendLine(" @kameiten_cd, ")
            commandTextSb.AppendLine(" @toukubun, ")
            commandTextSb.AppendLine(" @renkei_siji_cd_upd, ")
            commandTextSb.AppendLine(" @sousin_jyky_cd, ")
            commandTextSb.AppendLine(" @upd_login_user_id, ")
            commandTextSb.AppendLine(" getdate() ;")

        ElseIf kousinFlg = "del" Then

            commandTextSb.AppendLine(" DELETE FROM  ")
            commandTextSb.AppendLine(" m_tatouwaribiki_settei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" WHERE ")
            commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd")
            commandTextSb.AppendLine(" AND ")
            commandTextSb.AppendLine(" toukubun = @toukubun ;")

            commandTextSb.AppendLine(" DELETE FROM ")
            commandTextSb.AppendLine(" m_tatouwaribiki_settei_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" WHERE ")
            commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd")
            commandTextSb.AppendLine(" AND ")
            commandTextSb.AppendLine(" toukubun = @toukubun ;")

            commandTextSb.AppendLine(" INSERT INTO ")
            commandTextSb.AppendLine(" m_tatouwaribiki_settei_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" (kameiten_cd,")
            commandTextSb.AppendLine(" toukubun, ")
            commandTextSb.AppendLine(" renkei_siji_cd, ")
            commandTextSb.AppendLine(" sousin_jyky_cd, ")
            commandTextSb.AppendLine(" upd_login_user_id, ")
            commandTextSb.AppendLine(" upd_datetime) ")
            commandTextSb.AppendLine(" SELECT")
            commandTextSb.AppendLine(" @kameiten_cd, ")
            commandTextSb.AppendLine(" @toukubun, ")
            commandTextSb.AppendLine(" @renkei_siji_cd_del, ")
            commandTextSb.AppendLine(" @sousin_jyky_cd, ")
            commandTextSb.AppendLine(" @upd_login_user_id, ")
            commandTextSb.AppendLine(" getdate() ;")

        End If

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dtTatou.Rows(0).Item("kameiten_cd")))
        paramList.Add(MakeParam("@toukubun", SqlDbType.Int, 1, dtTatou.Rows(0).Item("toukubun")))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, dtTatou.Rows(0).Item("syouhin_cd")))

        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dtTatou.Rows(0).Item("add_login_user_id")))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, dtTatou.Rows(0).Item("upd_login_user_id")))

        paramList.Add(MakeParam("@renkei_siji_cd_ins", SqlDbType.Int, 4, 1))
        paramList.Add(MakeParam("@renkei_siji_cd_upd", SqlDbType.Int, 4, 2))
        paramList.Add(MakeParam("@renkei_siji_cd_del", SqlDbType.Int, 4, 9))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, 0))

        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        InsUpdTatou = True

    End Function

    ''' <summary>
    ''' [���i�E�������]�p�@�����X�}�X�^,�����X�}�X�^�A�g�Ǘ��e�[�u���A�X�V
    ''' </summary>
    ''' <param name="dtKameiten">�����X�e�[�u��</param>
    ''' <returns>TRUE:����,FALSE:���s</returns>
    ''' <remarks></remarks>
    Public Function UpdKameiten(ByVal dtKameiten As KameitenDataSet.m_kameitenTableDataTable) As Boolean

        '�߂�l
        UpdKameiten = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" UPDATE m_kameiten ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        '2011/03/01 ���i�E�������̍��ڂ��폜���� �t��(��A) ��
        'commandTextSb.AppendLine(" ss_kkk = @ss_kkk ")
        'commandTextSb.AppendLine(" ,sai_tys_kkk = @sai_tys_kkk ")
        '13/06/13 �k�o ��͕i���ۏؗ�  �Ƃ������ڂ𕜊�����@-------------��
        commandTextSb.AppendLine(" ssgr_kkk = @ssgr_kkk ")
        commandTextSb.AppendLine(" ,kaiseki_hosyou_kkk = @kaiseki_hosyou_kkk ")
        '13/06/13 �k�o ��͕i���ۏؗ�  �Ƃ������ڂ𕜊�����@-------------��
        'commandTextSb.AppendLine(" ,hosyounasi_umu = @hosyounasi_umu ")
        commandTextSb.AppendLine(" ,kaiyaku_haraimodosi_kkk = @kaiyaku_haraimodosi_kkk ")
        'commandTextSb.AppendLine(" ,jizen_tys_kkk = @jizen_tys_kkk ")
        'commandTextSb.AppendLine(" ,tys_seikyuu_saki = @tys_seikyuu_saki ")
        'commandTextSb.AppendLine(" ,tys_seikyuu_sime_date = @tys_seikyuu_sime_date ")
        'commandTextSb.AppendLine(" ,koj_seikyuusaki = @koj_seikyuusaki ")
        'commandTextSb.AppendLine(" ,koj_seikyuu_sime_date = @koj_seikyuu_sime_date ")
        'commandTextSb.AppendLine(" ,hansokuhin_seikyuusaki = @hansokuhin_seikyuusaki ")
        'commandTextSb.AppendLine(" ,hansokuhin_seikyuu_sime_date = @hansokuhin_seikyuu_sime_date ")
        '2011/03/01 ���i�E�������̍��ڂ��폜���� �t��(��A) ��

        commandTextSb.AppendLine(" , tys_seikyuu_saki_cd = @tys_seikyuu_saki_cd")
        commandTextSb.AppendLine(" , tys_seikyuu_saki_brc = @tys_seikyuu_saki_brc")
        commandTextSb.AppendLine(" , tys_seikyuu_saki_kbn = @tys_seikyuu_saki_kbn")

        commandTextSb.AppendLine(" , koj_seikyuu_saki_cd = @koj_seikyuu_saki_cd")
        commandTextSb.AppendLine(" , koj_seikyuu_saki_brc = @koj_seikyuu_saki_brc")
        commandTextSb.AppendLine(" , koj_seikyuu_saki_kbn = @koj_seikyuu_saki_kbn")

        commandTextSb.AppendLine(" , hansokuhin_seikyuu_saki_cd = @hansokuhin_seikyuu_saki_cd")
        commandTextSb.AppendLine(" , hansokuhin_seikyuu_saki_brc = @hansokuhin_seikyuu_saki_brc")
        commandTextSb.AppendLine(" , hansokuhin_seikyuu_saki_kbn = @hansokuhin_seikyuu_saki_kbn")

        commandTextSb.AppendLine(" , tatemono_seikyuu_saki_cd = @tatemono_seikyuu_saki_cd")
        commandTextSb.AppendLine(" , tatemono_seikyuu_saki_brc = @tatemono_seikyuu_saki_brc")
        commandTextSb.AppendLine(" , tatemono_seikyuu_saki_kbn = @tatemono_seikyuu_saki_kbn")
        '======================2011/06/28 �ԗ� �ǉ� �J�n��==================================
        commandTextSb.AppendLine(" , seikyuu_saki_cd5 = @seikyuu_saki_cd5")
        commandTextSb.AppendLine(" , seikyuu_saki_brc5 = @seikyuu_saki_brc5")
        commandTextSb.AppendLine(" , seikyuu_saki_kbn5 = @seikyuu_saki_kbn5")
        commandTextSb.AppendLine(" , seikyuu_saki_cd6 = @seikyuu_saki_cd6")
        commandTextSb.AppendLine(" , seikyuu_saki_brc6 = @seikyuu_saki_brc6")
        commandTextSb.AppendLine(" , seikyuu_saki_kbn6 = @seikyuu_saki_kbn6")
        commandTextSb.AppendLine(" , seikyuu_saki_cd7 = @seikyuu_saki_cd7")
        commandTextSb.AppendLine(" , seikyuu_saki_brc7 = @seikyuu_saki_brc7")
        commandTextSb.AppendLine(" , seikyuu_saki_kbn7 = @seikyuu_saki_kbn7")
        '======================2011/06/28 �ԗ� �ǉ� �I����==================================
        commandTextSb.AppendLine(" ,upd_login_user_id = @upd_login_user_id ")
        commandTextSb.AppendLine(" ,upd_datetime = getdate() ")
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd ;")

        commandTextSb.AppendLine(" DELETE FROM ")
        commandTextSb.AppendLine(" m_kameiten_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd;")

        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" m_kameiten_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" (kameiten_cd,")
        commandTextSb.AppendLine(" renkei_siji_cd, ")
        commandTextSb.AppendLine(" sousin_jyky_cd, ")
        commandTextSb.AppendLine(" upd_login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime) ")
        commandTextSb.AppendLine(" SELECT")
        commandTextSb.AppendLine(" @kameiten_cd, ")
        commandTextSb.AppendLine(" @renkei_siji_cd, ")
        commandTextSb.AppendLine(" @sousin_jyky_cd, ")
        commandTextSb.AppendLine(" @upd_login_user_id, ")
        commandTextSb.AppendLine(" getdate() ;")

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@ss_kkk", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("ss_kkk")))
        paramList.Add(MakeParam("@sai_tys_kkk", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("sai_tys_kkk")))
        paramList.Add(MakeParam("@ssgr_kkk", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("ssgr_kkk")))
        paramList.Add(MakeParam("@kaiseki_hosyou_kkk", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("kaiseki_hosyou_kkk")))
        paramList.Add(MakeParam("@hosyounasi_umu", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("hosyounasi_umu")))
        paramList.Add(MakeParam("@kaiyaku_haraimodosi_kkk", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("kaiyaku_haraimodosi_kkk")))
        paramList.Add(MakeParam("@jizen_tys_kkk", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("jizen_tys_kkk")))
        paramList.Add(MakeParam("@tys_seikyuu_saki", SqlDbType.VarChar, 5, dtKameiten.Rows(0).Item("tys_seikyuu_saki")))
        paramList.Add(MakeParam("@tys_seikyuu_sime_date", SqlDbType.VarChar, 2, dtKameiten.Rows(0).Item("tys_seikyuu_sime_date")))
        paramList.Add(MakeParam("@koj_seikyuusaki", SqlDbType.VarChar, 5, dtKameiten.Rows(0).Item("koj_seikyuusaki")))
        paramList.Add(MakeParam("@koj_seikyuu_sime_date", SqlDbType.VarChar, 2, dtKameiten.Rows(0).Item("koj_seikyuu_sime_date")))
        paramList.Add(MakeParam("@hansokuhin_seikyuusaki", SqlDbType.VarChar, 5, dtKameiten.Rows(0).Item("hansokuhin_seikyuusaki")))
        paramList.Add(MakeParam("@hansokuhin_seikyuu_sime_date", SqlDbType.VarChar, 2, dtKameiten.Rows(0).Item("hansokuhin_seikyuu_sime_date")))

        paramList.Add(MakeParam("@tys_seikyuu_saki_cd", SqlDbType.VarChar, 5, dtKameiten.Rows(0).Item("tys_seikyuu_saki_cd")))
        paramList.Add(MakeParam("@tys_seikyuu_saki_brc", SqlDbType.VarChar, 2, dtKameiten.Rows(0).Item("tys_seikyuu_saki_brc")))
        paramList.Add(MakeParam("@tys_seikyuu_saki_kbn", SqlDbType.VarChar, 1, dtKameiten.Rows(0).Item("tys_seikyuu_saki_kbn")))
        paramList.Add(MakeParam("@koj_seikyuu_saki_cd", SqlDbType.VarChar, 5, dtKameiten.Rows(0).Item("koj_seikyuu_saki_cd")))
        paramList.Add(MakeParam("@koj_seikyuu_saki_brc", SqlDbType.VarChar, 2, dtKameiten.Rows(0).Item("koj_seikyuu_saki_brc")))
        paramList.Add(MakeParam("@koj_seikyuu_saki_kbn", SqlDbType.VarChar, 1, dtKameiten.Rows(0).Item("koj_seikyuu_saki_kbn")))
        paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_cd", SqlDbType.VarChar, 5, dtKameiten.Rows(0).Item("hansokuhin_seikyuu_saki_cd")))
        paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_brc", SqlDbType.VarChar, 2, dtKameiten.Rows(0).Item("hansokuhin_seikyuu_saki_brc")))
        paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_kbn", SqlDbType.VarChar, 1, dtKameiten.Rows(0).Item("hansokuhin_seikyuu_saki_kbn")))
        paramList.Add(MakeParam("@tatemono_seikyuu_saki_cd", SqlDbType.VarChar, 5, dtKameiten.Rows(0).Item("tatemono_seikyuu_saki_cd")))
        paramList.Add(MakeParam("@tatemono_seikyuu_saki_brc", SqlDbType.VarChar, 2, dtKameiten.Rows(0).Item("tatemono_seikyuu_saki_brc")))
        paramList.Add(MakeParam("@tatemono_seikyuu_saki_kbn", SqlDbType.VarChar, 1, dtKameiten.Rows(0).Item("tatemono_seikyuu_saki_kbn")))
        '======================2011/06/28 �ԗ� �ǉ� �J�n��==================================
        paramList.Add(MakeParam("@seikyuu_saki_cd5", SqlDbType.VarChar, 5, dtKameiten.Rows(0).Item("seikyuu_saki_cd5")))
        paramList.Add(MakeParam("@seikyuu_saki_brc5", SqlDbType.VarChar, 2, dtKameiten.Rows(0).Item("seikyuu_saki_brc5")))
        paramList.Add(MakeParam("@seikyuu_saki_kbn5", SqlDbType.VarChar, 1, dtKameiten.Rows(0).Item("seikyuu_saki_kbn5")))
        paramList.Add(MakeParam("@seikyuu_saki_cd6", SqlDbType.VarChar, 5, dtKameiten.Rows(0).Item("seikyuu_saki_cd6")))
        paramList.Add(MakeParam("@seikyuu_saki_brc6", SqlDbType.VarChar, 2, dtKameiten.Rows(0).Item("seikyuu_saki_brc6")))
        paramList.Add(MakeParam("@seikyuu_saki_kbn6", SqlDbType.VarChar, 1, dtKameiten.Rows(0).Item("seikyuu_saki_kbn6")))
        paramList.Add(MakeParam("@seikyuu_saki_cd7", SqlDbType.VarChar, 5, dtKameiten.Rows(0).Item("seikyuu_saki_cd7")))
        paramList.Add(MakeParam("@seikyuu_saki_brc7", SqlDbType.VarChar, 2, dtKameiten.Rows(0).Item("seikyuu_saki_brc7")))
        paramList.Add(MakeParam("@seikyuu_saki_kbn7", SqlDbType.VarChar, 1, dtKameiten.Rows(0).Item("seikyuu_saki_kbn7")))
        '======================2011/06/28 �ԗ� �ǉ� �I����==================================
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, dtKameiten.Rows(0).Item("upd_login_user_id")))

        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, 2))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, 0))

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dtKameiten.Rows(0).Item("kameiten_cd")))

        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        UpdKameiten = True

    End Function

    '==================2011/06/28 �ԗ� �ǉ� �J�n��==========================
    ''' <summary>
    ''' �����捀�ږ����擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuusakiKoumokuMei() As Data.DataTable

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New Data.DataSet

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	SUB_NUM.code ")
            .AppendLine("	,ISNULL(SUB_MM.meisyou,'') AS meisyou ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			'1' AS code ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'2' AS code ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'3' AS code ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'4' AS code ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'5' AS code ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'6' AS code ")
            .AppendLine("		UNION ALL ")
            .AppendLine("		SELECT ")
            .AppendLine("			'7' AS code ")
            .AppendLine("	) AS SUB_NUM ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	( ")
            .AppendLine("		SELECT  ")
            .AppendLine("			code ")
            .AppendLine("			,ISNULL(meisyou,'') AS meisyou ")
            .AppendLine("		FROM  ")
            .AppendLine("			m_meisyou WITH (READCOMMITTED) ")
            .AppendLine("		where ")
            .AppendLine("			meisyou_syubetu = '57' ")
            .AppendLine("			AND ")
            .AppendLine("			code >=1 ")
            .AppendLine("			AND ")
            .AppendLine("			code <=7 ")
            .AppendLine("	) AS SUB_MM ")
            .AppendLine("	ON ")
            .AppendLine("		SUB_NUM.code = SUB_MM.code ")
        End With

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSeikyuusakiKoumokuMei")

        Return dsReturn.Tables(0)

    End Function
    '==================2011/06/28 �ԗ� �ǉ� �I����==========================

End Class
