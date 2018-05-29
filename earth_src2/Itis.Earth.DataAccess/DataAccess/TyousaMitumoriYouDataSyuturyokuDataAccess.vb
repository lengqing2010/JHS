Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class TyousaMitumoriYouDataSyuturyokuDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �������σf�[�^�o��
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <param name="strHosyousyoNo1">�ۏ؏�NO1</param>
    ''' <param name="strHosyousyoNo2">�ۏ؏�NO2</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strMitumoriFlg">���Ϗ��쐬�t���O</param>
    ''' <history>20100925�@�n���R</history>
    Public Function SelTyousaMitumoriInfo(ByVal strKubun As String, _
                                            ByVal strHosyousyoNo1 As String, _
                                            ByVal strHosyousyoNo2 As String, _
                                            ByVal strKameitenCd As String, _
                                            ByVal strMitumoriFlg As String, _
                                            ByVal strKoFlg As String, _
                                            ByVal strSesyuMei As String, _
                                            ByVal strKeiretuCd As String, _
                                            ByVal strTS As String) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            If strKoFlg = "10" Then
                .AppendLine("	TOP 10 ")
            ElseIf strKoFlg = "50" Then
                .AppendLine("	TOP 50 ")
            End If
            .AppendLine("	TBL1.kbn, ")
            .AppendLine("	TBL1.hosyousyo_no, ")
            .AppendLine("	TBL1.sesyu_mei, ")
            .AppendLine("	TBL1.tys_mitsyo_sakusei_date, ")
            .AppendLine("	TBL1.kameiten_cd, ")
            .AppendLine("	TBL1.kameiten_mei1 ")
            .AppendLine("FROM ")

            .AppendLine("(")
            .AppendLine("SELECT ")
            'If strKoFlg = "10" Then
            '    .AppendLine("	TOP 10 ")
            'ElseIf strKoFlg = "50" Then
            '    .AppendLine("	TOP 50 ")
            'End If
            .AppendLine("	TJ.kbn, ")
            .AppendLine("	TJ.hosyousyo_no, ")
            .AppendLine("	TJ.sesyu_mei, ")
            .AppendLine("	MAX(ISNULL(CONVERT(CHAR(10),TTS.tys_mitsyo_sakusei_date,111),'')) AS tys_mitsyo_sakusei_date, ")
            .AppendLine("	TJ.kameiten_cd, ")
            .AppendLine("	MK.kameiten_mei1 ")
            .AppendLine("FROM ")
            .AppendLine("	t_jiban TJ WITH(READCOMMITTED) ")
            .AppendLine("INNER JOIN ")
            .AppendLine("	t_teibetu_seikyuu TTS WITH(READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("	TJ.kbn = TTS.kbn ")
            .AppendLine("AND ")
            .AppendLine("	TJ.hosyousyo_no = TTS.hosyousyo_no ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_kameiten MK WITH(READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("	TJ.kameiten_cd = MK.kameiten_cd ")

            '2017/12/11 ������ǉ����� ��(��A) ��
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_todoufuken MT WITH(READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("	MT.todouhuken_cd = MK.todouhuken_cd ")
            '2017/12/11 ������ǉ����� ��(��A) ��

            .AppendLine("WHERE ")
            .AppendLine("   TJ.kbn IS NOT NULL ")
            '2017/12/11 ������ǉ����� ��(��A) ��
            '�{�喼
            If strSesyuMei <> "" Then
                .AppendLine("AND TJ.sesyu_mei like '%" & strSesyuMei & "%' ")
            End If
            '�n��R�[�h
            If strKeiretuCd <> "" Then
                .AppendLine("AND MK.keiretu_cd = '" & strKeiretuCd & "' ")
            End If
            '�E�����i�������W�I�{�^���ɓ��͗L��̏ꍇ�j
            If strTS <> "" Then
                .AppendLine("AND MT.touzai_flg = '" & strTS & "' ")
            End If
            '2017/12/11 ������ǉ����� ��(��A) ��

            '�敪
            If strKubun <> "" Then
                .AppendLine("AND ")
                .AppendLine("	TJ.kbn = @kbn ")
            End If
            '�ԍ�
            If strHosyousyoNo1 <> "" And strHosyousyoNo2 <> "" Then
                .AppendLine("AND ")
                .AppendLine("	RIGHT('0000000000' + TJ.hosyousyo_no,10) BETWEEN RIGHT('0000000000' + @hosyousyo_no1,10) AND RIGHT('0000000000' + @hosyousyo_no2,10)  ")
            ElseIf strHosyousyoNo1 <> "" And strHosyousyoNo2 = "" Then
                .AppendLine("AND ")
                .AppendLine("	((RIGHT('0000000000' + TJ.hosyousyo_no,10) >= @hosyousyo_no1 AND datalength(@hosyousyo_no1)='10') OR (TJ.hosyousyo_no >=  @hosyousyo_no1 AND datalength(@hosyousyo_no1)<>'10')) ")
            ElseIf strHosyousyoNo2 <> "" And strHosyousyoNo1 = "" Then
                .AppendLine("AND ")
                .AppendLine("	((LEFT(TJ.hosyousyo_no + '0000000000',10) <= @hosyousyo_no2 AND datalength(@hosyousyo_no2)='10') OR (RIGHT('0000000000' + TJ.hosyousyo_no,10) <=  RIGHT('0000000000' + @hosyousyo_no2,10) AND datalength(@hosyousyo_no2)<>'10')) ")
            End If
            '�����X
            If strKameitenCd <> "" Then
                .AppendLine("AND ")
                .AppendLine("	TJ.kameiten_cd = @kameiten_cd ")
            End If
            ''���Ϗ�
            'If strMitumoriFlg = "" Then
            '    .AppendLine("AND ")
            '    .AppendLine("	TTS.tys_mitsyo_sakusei_date IS NULL ")
            'Else
            '    .AppendLine("AND ")
            '    .AppendLine("	TTS.tys_mitsyo_sakusei_date IS NOT NULL ")
            'End If
            '���ރR�[�h
            .AppendLine("AND ")
            .AppendLine("	TTS.bunrui_cd IN ('100','110','115','120','190') ")
            '�f�[�^�j�����
            .AppendLine("AND ")
            .AppendLine("	TJ.data_haki_syubetu = '0' ")

            .AppendLine("GROUP BY ")
            .AppendLine("	TJ.kbn, ")
            .AppendLine("	TJ.hosyousyo_no, ")
            .AppendLine("	TJ.sesyu_mei, ")
            .AppendLine("	TJ.kameiten_cd, ")
            .AppendLine("	MK.kameiten_mei1 ")

            .AppendLine(") AS TBL1")
            '���Ϗ�
            If strMitumoriFlg = "" Then
                .AppendLine("WHERE ")
                .AppendLine("	(TBL1.tys_mitsyo_sakusei_date IS NULL OR TBL1.tys_mitsyo_sakusei_date = '') ")
            Else
                .AppendLine("WHERE ")
                .AppendLine("	(TBL1.tys_mitsyo_sakusei_date IS NOT NULL AND TBL1.tys_mitsyo_sakusei_date <> '') ")
            End If

            .AppendLine("ORDER BY ")
            .AppendLine("	TBL1.kbn, ")
            .AppendLine("	TBL1.hosyousyo_no ")
        End With

        '�p�����[�^�̐ݒ�
        '�敪
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKubun))
        '�ԍ�1
        paramList.Add(MakeParam("@hosyousyo_no1", SqlDbType.VarChar, 10, strHosyousyoNo1))
        '�ԍ�2
        paramList.Add(MakeParam("@hosyousyo_no2", SqlDbType.VarChar, 10, strHosyousyoNo2))
        '�����X
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �������σf�[�^�o�͑�����
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <param name="strHosyousyoNo1">�ۏ؏�NO1</param>
    ''' <param name="strHosyousyoNo2">�ۏ؏�NO2</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strMitumoriFlg">���Ϗ��쐬�t���O</param>
    ''' <history>20100925�@�n���R</history>
    Public Function SelTyousaMitumoriCount(ByVal strKubun As String, _
                                          ByVal strHosyousyoNo1 As String, _
                                          ByVal strHosyousyoNo2 As String, _
                                          ByVal strKameitenCd As String, _
                                          ByVal strMitumoriFlg As String, _
                                          ByVal strKoFlg As String, _
                                            ByVal strSesyuMei As String, _
                                            ByVal strKeiretuCd As String, _
                                            ByVal strTS As String) As Int64

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(hosyousyo_no) AS COUNT ")
            .AppendLine("FROM ")
            .AppendLine("(")

            .AppendLine("SELECT ")
            .AppendLine("	TBL1.kbn, ")
            .AppendLine("	TBL1.hosyousyo_no, ")
            .AppendLine("	TBL1.sesyu_mei, ")
            .AppendLine("	TBL1.tys_mitsyo_sakusei_date, ")
            .AppendLine("	TBL1.kameiten_cd, ")
            .AppendLine("	TBL1.kameiten_mei1 ")
            .AppendLine("FROM ")
            .AppendLine("(")

            .AppendLine("SELECT ")
            .AppendLine("	TJ.kbn, ")
            .AppendLine("	TJ.hosyousyo_no, ")
            .AppendLine("	TJ.sesyu_mei, ")
            .AppendLine("	MAX(ISNULL(CONVERT(CHAR(10),TTS.tys_mitsyo_sakusei_date,111),'')) AS tys_mitsyo_sakusei_date, ")
            .AppendLine("	TJ.kameiten_cd, ")
            .AppendLine("	MK.kameiten_mei1 ")
            .AppendLine("FROM ")
            .AppendLine("	t_jiban TJ WITH(READCOMMITTED) ")
            .AppendLine("INNER JOIN ")
            .AppendLine("	t_teibetu_seikyuu TTS WITH(READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("	TJ.kbn = TTS.kbn ")
            .AppendLine("AND ")
            .AppendLine("	TJ.hosyousyo_no = TTS.hosyousyo_no ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_kameiten MK WITH(READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("	TJ.kameiten_cd = MK.kameiten_cd ")

            '2017/12/11 ������ǉ����� ��(��A) ��
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_todoufuken MT WITH(READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("	MT.todouhuken_cd = MK.todouhuken_cd ")
            '2017/12/11 ������ǉ����� ��(��A) ��


            .AppendLine("WHERE ")
            .AppendLine("   TJ.kbn IS NOT NULL ")

            '2017/12/11 ������ǉ����� ��(��A) ��
            '�{�喼
            If strSesyuMei <> "" Then
                .AppendLine("AND TJ.sesyu_mei like '%" & strSesyuMei & "%' ")
            End If
            '�n��R�[�h
            If strKeiretuCd <> "" Then
                .AppendLine("AND MK.keiretu_cd = '" & strKeiretuCd & "' ")
            End If
            '�n��R�[�h
            If strTS <> "" Then
                .AppendLine("AND MT.touzai_flg = '" & strTS & "' ")
            End If
            '2017/12/11 ������ǉ����� ��(��A) ��

            '�敪
            If strKubun <> "" Then
                .AppendLine("AND ")
                .AppendLine("	TJ.kbn = @kbn ")
            End If
            '�ԍ�
            If strHosyousyoNo1 <> "" And strHosyousyoNo2 <> "" Then
                .AppendLine("AND ")
                .AppendLine("	RIGHT('0000000000' + TJ.hosyousyo_no,10) BETWEEN RIGHT('0000000000' + @hosyousyo_no1,10) AND RIGHT('0000000000' + @hosyousyo_no2,10)  ")
            ElseIf strHosyousyoNo1 <> "" And strHosyousyoNo2 = "" Then
                .AppendLine("AND ")
                .AppendLine("	((RIGHT('0000000000' + TJ.hosyousyo_no,10) >= @hosyousyo_no1 AND datalength(@hosyousyo_no1)='10') OR (TJ.hosyousyo_no >=  @hosyousyo_no1 AND datalength(@hosyousyo_no1)<>'10')) ")
            ElseIf strHosyousyoNo2 <> "" And strHosyousyoNo1 = "" Then
                .AppendLine("AND ")
                .AppendLine("	((LEFT(TJ.hosyousyo_no + '0000000000',10) <= @hosyousyo_no2 AND datalength(@hosyousyo_no2)='10') OR (RIGHT('0000000000' + TJ.hosyousyo_no,10) <=  RIGHT('0000000000' + @hosyousyo_no2,10) AND datalength(@hosyousyo_no2)<>'10')) ")
            End If
            '�����X
            If strKameitenCd <> "" Then
                .AppendLine("AND ")
                .AppendLine("	TJ.kameiten_cd = @kameiten_cd ")
            End If
            ''���Ϗ�
            'If strMitumoriFlg = "" Then
            '    .AppendLine("AND ")
            '    .AppendLine("	TTS.tys_mitsyo_sakusei_date IS NULL ")
            'Else
            '    .AppendLine("AND ")
            '    .AppendLine("	TTS.tys_mitsyo_sakusei_date IS NOT NULL ")
            'End If
            '���ރR�[�h
            .AppendLine("AND ")
            .AppendLine("	TTS.bunrui_cd IN ('100','110','115','120','190') ")
            '�f�[�^�j�����
            .AppendLine("AND ")
            .AppendLine("	TJ.data_haki_syubetu = '0' ")

            .AppendLine("GROUP BY ")
            .AppendLine("	TJ.kbn, ")
            .AppendLine("	TJ.hosyousyo_no, ")
            .AppendLine("	TJ.sesyu_mei, ")
            .AppendLine("	TJ.kameiten_cd, ")
            .AppendLine("	MK.kameiten_mei1 ")

            .AppendLine(") AS TBL1")
            '���Ϗ�
            If strMitumoriFlg = "" Then
                .AppendLine("WHERE ")
                .AppendLine("	(TBL1.tys_mitsyo_sakusei_date IS NULL OR TBL1.tys_mitsyo_sakusei_date = '') ")
            Else
                .AppendLine("WHERE ")
                .AppendLine("	(TBL1.tys_mitsyo_sakusei_date IS NOT NULL AND TBL1.tys_mitsyo_sakusei_date <> '') ")
            End If


            .AppendLine(") TBL2")
        End With

        '�p�����[�^�̐ݒ�
        '�敪
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKubun))
        '�ԍ�1
        paramList.Add(MakeParam("@hosyousyo_no1", SqlDbType.VarChar, 10, strHosyousyoNo1))
        '�ԍ�2
        paramList.Add(MakeParam("@hosyousyo_no2", SqlDbType.VarChar, 10, strHosyousyoNo2))
        '�����X
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0).Rows(0).Item("COUNT")

    End Function

    ''' <summary>
    ''' CSV�f�[�^�i�ʂ̏ꍇ�j
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <param name="strHosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strMitumoriFlg">���Ϗ��쐬�t���O</param>
    ''' <history>20100925�@�n���R</history>
    Public Function SelCsvDataKobetu(ByVal strKubun As String, _
                               ByVal strHosyousyoNo As String, _
                               ByVal strKameitenCd As String, _
                               ByVal strMitumoriFlg As String, _
                                ByVal strSesyuMei As String, _
                                ByVal strKeiretuCd As String, _
                                ByVal strTS As String) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	TJ.kbn, ")
            .AppendLine("	TJ.hosyousyo_no, ")
            .AppendLine("	TJ.sesyu_mei, ")
            .AppendLine("	TJ.kameiten_cd, ")
            .AppendLine("	MK.kameiten_mei1, ")
            .AppendLine("	ISNULL(TJ.irai_tantousya_mei,'���S����') AS irai_tantousya_mei, ")
            .AppendLine("	TJ.bukken_jyuusyo1, ")
            .AppendLine("	TJ.bukken_jyuusyo2, ")
            .AppendLine("	TJ.bukken_jyuusyo3, ")
            .AppendLine("	TTS.bunrui_cd, ")
            .AppendLine("	TTS.syouhin_cd, ")
            '.AppendLine("	MS.syouhin_mei, ")

            .AppendLine("	CASE ")
            .AppendLine("	WHEN km1.code+km2.code IS NULL THEN")
            .AppendLine("          MS.syouhin_mei ")
            .AppendLine("	ELSE")
            .AppendLine("	RTRIM(MS.syouhin_mei)+'['+m_tyousahouhou.tys_houhou_mei_ryaku+']'")
            .AppendLine("	END AS syouhin_mei, ")

            .AppendLine("	'1' AS suuryou, ")
            .AppendLine("	MS.tani, ")
            '====================��2015/06/18 430002 �C����====================
            '.AppendLine("	CONVERT(BIGINT,ISNULL(TTS.koumuten_seikyuu_gaku,0) * (1 + MSZ.zeiritu)) AS tanka, ")
            '.AppendLine("	CONVERT(BIGINT,ISNULL(TTS.koumuten_seikyuu_gaku,0) * (1 + MSZ.zeiritu)) AS kingaku ")
            .AppendLine("	CONVERT(BIGINT,ISNULL(TTS.koumuten_seikyuu_gaku,0)) AS tanka, ")
            .AppendLine("	CONVERT(BIGINT,ISNULL(TTS.koumuten_seikyuu_gaku,0)) AS kingaku, ")
            .AppendLine("	TTS.syouhizei_gaku AS syouhizei ")
            '====================��2015/06/18 430002 �C����====================
            .AppendLine("FROM ")
            .AppendLine("	t_jiban TJ WITH(READCOMMITTED) ")
            .AppendLine("INNER JOIN ")
            .AppendLine("	t_teibetu_seikyuu TTS WITH(READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("	TJ.kbn = TTS.kbn ")
            .AppendLine("AND ")
            .AppendLine("	TJ.hosyousyo_no = TTS.hosyousyo_no ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_kameiten MK WITH(READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("	TJ.kameiten_cd = MK.kameiten_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_syouhin MS WITH(READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("	TTS.syouhin_cd = MS.syouhin_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_syouhizei MSZ  WITH(READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("	TTS.zei_kbn = MSZ.zei_kbn ")

            .AppendLine("LEFT JOIN m_tyousahouhou")
            .AppendLine("	ON")
            .AppendLine("	TJ.tys_houhou = m_tyousahouhou.tys_houhou_no")
            .AppendLine("LEFT JOIN m_kakutyou_meisyou AS km1 ")
            .AppendLine("	ON")
            .AppendLine("	           TTS.syouhin_cd = km1.code")
            .AppendLine("	       AND km1.meisyou_syubetu = 26		")
            .AppendLine("LEFT JOIN m_kakutyou_meisyou AS km2")
            .AppendLine("	ON")
            .AppendLine("	           TJ.tys_houhou = km2.code")
            .AppendLine("	      AND km2.meisyou_syubetu = 27	")

            ''2017/12/11 ������ǉ����� ��(��A) ��
            '.AppendLine("LEFT JOIN ")
            '.AppendLine("	m_todoufuken MT WITH(READCOMMITTED) ")
            '.AppendLine("ON ")
            '.AppendLine("	MT.todouhuken_cd = MK.todouhuken_cd ")
            ''2017/12/11 ������ǉ����� ��(��A) ��


            .AppendLine("WHERE ")
            .AppendLine("	TJ.kbn = @kbn ")

            ''2017/12/11 ������ǉ����� ��(��A) ��
            ''�{�喼
            'If strSesyuMei <> "" Then
            '    .AppendLine("AND TJ.sesyu_mei like '%" & strSesyuMei & "%' ")
            'End If
            ''�n��R�[�h
            'If strKeiretuCd <> "" Then
            '    .AppendLine("AND MK.keiretu_cd = '" & strKeiretuCd & "' ")
            'End If
            ''�n��R�[�h
            'If strKeiretuCd <> "" Then
            '    .AppendLine("AND MT.touzai_flg = '" & strTS & "' ")
            'End If
            ''2017/12/11 ������ǉ����� ��(��A) ��



            .AppendLine("AND ")
            .AppendLine("	TTS.hosyousyo_no = @hosyousyo_no ")

            '�����X
            If strKameitenCd <> "" Then
                .AppendLine("AND ")
                .AppendLine("	TJ.kameiten_cd = @kameiten_cd ")
            End If
            ''���Ϗ�
            'If strMitumoriFlg = "" Then
            '    .AppendLine("AND ")
            '    .AppendLine("	TTS.tys_mitsyo_sakusei_date IS NULL ")
            'Else
            '    .AppendLine("AND ")
            '    .AppendLine("	TTS.tys_mitsyo_sakusei_date IS NOT NULL ")
            'End If

            .AppendLine("AND ")
            .AppendLine("	TTS.bunrui_cd IN ('100','110','115','120','190') ")
            '�f�[�^�j�����
            .AppendLine("AND ")
            .AppendLine("	TJ.data_haki_syubetu = '0' ")

            .AppendLine("ORDER BY ")
            .AppendLine("	TJ.kbn, ")
            .AppendLine("	TJ.hosyousyo_no, ")
            .AppendLine("	TTS.bunrui_cd, ")
            .AppendLine("	TTS.syouhin_cd ")
        End With

        '�p�����[�^�̐ݒ�
        '�敪
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKubun))
        '�ԍ�
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strHosyousyoNo))
        '�����X
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' CSV�f�[�^�i�A���̏ꍇ�j
    ''' </summary>
    ''' <param name="strKubun_HosyousyoNo">�敪_�ۏ؏�NO</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strMitumoriFlg">���Ϗ��쐬�t���O</param>
    ''' <history>20100925�@�n���R</history>
    Public Function SelCsvDataRentou(ByVal strKubun_HosyousyoNo As String, _
                               ByVal strKameitenCd As String, _
                               ByVal intCount As Integer, _
                               ByVal strMitumoriFlg As String, _
                                ByVal strSesyuMei As String, _
                                ByVal strKeiretuCd As String, _
                                ByVal strTS As String) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	TJ.kbn, ")
            .AppendLine("	TJ.hosyousyo_no, ")
            If intCount = 1 Then
                .AppendLine("	TJ.sesyu_mei AS sesyu_mei, ")
            Else
                .AppendLine("	(TJ.sesyu_mei + ' ���̑�' + CONVERT(VARCHAR,(@suuryou-1)) + '��') AS sesyu_mei, ")
            End If
            .AppendLine("	TJ.kameiten_cd, ")
            .AppendLine("	MK.kameiten_mei1, ")
            .AppendLine("	ISNULL(TJ.irai_tantousya_mei,'���S����') AS irai_tantousya_mei, ")
            .AppendLine("	TJ.bukken_jyuusyo1, ")
            .AppendLine("	TJ.bukken_jyuusyo2, ")
            .AppendLine("	TJ.bukken_jyuusyo3, ")
            .AppendLine("	TTS.bunrui_cd, ")
            .AppendLine("	TTS.syouhin_cd, ")
            '.AppendLine("	MS.syouhin_mei, ")
            .AppendLine("	CASE ")
            .AppendLine("	WHEN km1.code+km2.code IS NULL THEN")
            .AppendLine("          MS.syouhin_mei ")
            .AppendLine("	ELSE")
            .AppendLine("	MS.syouhin_mei+'['+m_tyousahouhou.tys_houhou_mei_ryaku+']'")
            .AppendLine("	END AS syouhin_mei, ")

            .AppendLine("	@suuryou AS suuryou, ")
            .AppendLine("	MS.tani, ")
            '====================��2015/06/18 430002 �C����====================
            '.AppendLine("	CONVERT(BIGINT,ISNULL(TTS.koumuten_seikyuu_gaku,0) * (1 + MSZ.zeiritu)) AS tanka, ")
            '.AppendLine("	(@suuryou * CONVERT(BIGINT,ISNULL(TTS.koumuten_seikyuu_gaku,0) * (1 + MSZ.zeiritu))) AS kingaku ")
            .AppendLine("	CONVERT(BIGINT,ISNULL(TTS.koumuten_seikyuu_gaku,0)) AS tanka, ")
            .AppendLine("	(@suuryou * CONVERT(BIGINT,ISNULL(TTS.koumuten_seikyuu_gaku,0))) AS kingaku, ")
            .AppendLine("	(@suuryou * TTS.syouhizei_gaku) AS syouhizei ")
            '====================��2015/06/18 430002 �C����====================
            .AppendLine("FROM ")
            .AppendLine("	t_jiban TJ WITH(READCOMMITTED) ")
            .AppendLine("INNER JOIN ")
            .AppendLine("	t_teibetu_seikyuu TTS WITH(READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("	TJ.kbn = TTS.kbn ")
            .AppendLine("AND ")
            .AppendLine("	TJ.hosyousyo_no = TTS.hosyousyo_no ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_kameiten MK WITH(READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("	TJ.kameiten_cd = MK.kameiten_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_syouhin MS WITH(READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("	TTS.syouhin_cd = MS.syouhin_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_syouhizei MSZ  WITH(READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("	TTS.zei_kbn = MSZ.zei_kbn ")

            .AppendLine("LEFT JOIN m_tyousahouhou")
            .AppendLine("	ON")
            .AppendLine("	TJ.tys_houhou = m_tyousahouhou.tys_houhou_no")
            .AppendLine("LEFT JOIN m_kakutyou_meisyou AS km1 ")
            .AppendLine("	ON")
            .AppendLine("	           TTS.syouhin_cd = km1.code")
            .AppendLine("	       AND km1.meisyou_syubetu = 26		")
            .AppendLine("LEFT JOIN m_kakutyou_meisyou AS km2")
            .AppendLine("	ON")
            .AppendLine("	           TJ.tys_houhou = km2.code")
            .AppendLine("	      AND km2.meisyou_syubetu = 27	")


            ''2017/12/11 ������ǉ����� ��(��A) ��
            '.AppendLine("LEFT JOIN ")
            '.AppendLine("	m_todoufuken MT WITH(READCOMMITTED) ")
            '.AppendLine("ON ")
            '.AppendLine("	MT.todouhuken_cd = MK.todouhuken_cd ")
            ''2017/12/11 ������ǉ����� ��(��A) ��

            .AppendLine("WHERE ")
            .AppendLine("	TJ.kbn + TTS.hosyousyo_no = @Kbn_HosyousyoNo ")


            ''2017/12/11 ������ǉ����� ��(��A) ��
            ''�{�喼
            'If strSesyuMei <> "" Then
            '    .AppendLine("AND TJ.sesyu_mei like '%" & strSesyuMei & "%' ")
            'End If
            ''�n��R�[�h
            'If strKeiretuCd <> "" Then
            '    .AppendLine("AND MK.keiretu_cd = '" & strKeiretuCd & "' ")
            'End If
            ''�n��R�[�h
            'If strKeiretuCd <> "" Then
            '    .AppendLine("AND MT.touzai_flg = '" & strTS & "' ")
            'End If
            ''2017/12/11 ������ǉ����� ��(��A) ��

            '�����X
            If strKameitenCd <> "" Then
                .AppendLine("AND ")
                .AppendLine("	TJ.kameiten_cd = @kameiten_cd ")
            End If
            ''���Ϗ�
            'If strMitumoriFlg = "" Then
            '    .AppendLine("AND ")
            '    .AppendLine("	TTS.tys_mitsyo_sakusei_date IS NULL ")
            'Else
            '    .AppendLine("AND ")
            '    .AppendLine("	TTS.tys_mitsyo_sakusei_date IS NOT NULL ")
            'End If

            .AppendLine("AND ")
            .AppendLine("	TTS.bunrui_cd IN ('100','110','115','120','190') ")
            '�f�[�^�j�����
            .AppendLine("AND ")
            .AppendLine("	TJ.data_haki_syubetu = '0' ")
        End With

        '�p�����[�^�̐ݒ�
        '����
        paramList.Add(MakeParam("@suuryou", SqlDbType.Int, 5, intCount))
        '�敪
        paramList.Add(MakeParam("@Kbn_HosyousyoNo", SqlDbType.Char, 11, strKubun_HosyousyoNo))
        '�����X
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function


    ''' <summary>
    ''' ��������\�ɁA�f�[�^�����݃`�F�b�N
    ''' </summary>
    ''' <param name="drRow">��������</param>
    ''' <history>20100925�@�n���R</history>
    Public Function SelBukkenRirekiChk(ByVal drRow As DataRow) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	kbn, ")
            .AppendLine("	hosyousyo_no, ")
            .AppendLine("	rireki_syubetu, ")
            .AppendLine("	rireki_no, ")
            .AppendLine("	nyuuryoku_no ")
            .AppendLine("FROM ")
            .AppendLine("	t_bukken_rireki WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kbn = @kbn ")
            .AppendLine("AND ")
            .AppendLine("	hosyousyo_no = @hosyousyo_no ")
            .AppendLine("AND ")
            .AppendLine("	rireki_syubetu = @rireki_syubetu ")
            .AppendLine("AND ")
            .AppendLine("	rireki_no = @rireki_no ")
        End With

        '�p�����[�^�̐ݒ�
        '�敪
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, drRow.Item("kbn")))
        '�ԍ�
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, drRow.Item("hosyousyo_no")))
        '�������
        paramList.Add(MakeParam("@rireki_syubetu", SqlDbType.VarChar, 2, drRow.Item("rireki_syubetu")))
        '����NO
        paramList.Add(MakeParam("@rireki_no", SqlDbType.Int, 1, drRow.Item("rireki_no")))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' ��������\�ɁA����NO�̎擾
    ''' </summary>
    ''' <param name="drRow">��������</param>
    ''' <history>20100925�@�n���R</history>
    Public Function SelBukkenRirekiNo(ByVal drRow As DataRow) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	kbn, ")
            .AppendLine("	hosyousyo_no, ")
            .AppendLine("	MAX(nyuuryoku_no) AS nyuuryoku_no ")
            .AppendLine("FROM ")
            .AppendLine("	t_bukken_rireki WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kbn = @kbn ")
            .AppendLine("AND ")
            .AppendLine("	hosyousyo_no = @hosyousyo_no ")
            .AppendLine("GROUP BY ")
            .AppendLine("	kbn,hosyousyo_no ")
        End With

        '�p�����[�^�̐ݒ�
        '�敪
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, drRow.Item("kbn")))
        '�ԍ�
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, drRow.Item("hosyousyo_no")))
        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �f�[�^�����݂��Ȃ����A�V�K�f�[�^��o�^����
    ''' </summary>
    ''' <param name="drRow">�V�K�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsBukkenRireki(ByVal drRow As DataRow, Optional ByVal bloSonzaiFlg As Boolean = True) As Boolean

        'SQL�R�����g
        Dim commandTextSb As New System.Text.StringBuilder
        '�߂�f�[�^�Z�b�g

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL�R�����g�쐬
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("t_bukken_rireki ")
            .AppendLine("   (")
            .AppendLine("   kbn, ")
            .AppendLine("   hosyousyo_no, ")
            .AppendLine("   rireki_syubetu, ")
            .AppendLine("   rireki_no, ")
            .AppendLine("   nyuuryoku_no, ")
            .AppendLine("   naiyou, ")
            .AppendLine("   hanyou_date, ")
            .AppendLine("   hanyou_cd, ")
            .AppendLine("   henkou_kahi_flg, ")
            .AppendLine("   torikesi, ")
            .AppendLine("   add_login_user_id, ")
            .AppendLine("   add_datetime) ")
            .AppendLine(" VALUES(")
            .AppendLine("   @kbn, ")
            .AppendLine("   @hosyousyo_no, ")
            .AppendLine("   @rireki_syubetu, ")
            .AppendLine("   @rireki_no, ")
            .AppendLine("   @nyuuryoku_no, ")
            If bloSonzaiFlg = True Then
                .AppendLine("   '���Ϗ��쐬��' + CONVERT(CHAR(10),GETDATE(),111) + '�č쐬', ")
            Else
                .AppendLine("   '���Ϗ��쐬��' + CONVERT(CHAR(10),GETDATE(),111), ")
            End If
            .AppendLine("   CONVERT(CHAR(10),GETDATE(),111), ")
            .AppendLine("   @hanyou_cd, ")
            .AppendLine("   @henkou_kahi_flg, ")
            .AppendLine("   @torikesi, ")
            .AppendLine("   @add_login_user_id, ")
            .AppendLine("   GETDATE()")
            .AppendLine(" )")
        End With

        '�p�����[�^����
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, drRow.Item("kbn")))
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, drRow.Item("hosyousyo_no")))
        paramList.Add(MakeParam("@rireki_syubetu", SqlDbType.VarChar, 2, drRow.Item("rireki_syubetu")))
        paramList.Add(MakeParam("@rireki_no", SqlDbType.Int, 5, drRow.Item("rireki_no")))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 5, drRow.Item("nyuuryoku_no")))
        paramList.Add(MakeParam("@hanyou_cd", SqlDbType.VarChar, 20, drRow.Item("hanyou_cd")))
        paramList.Add(MakeParam("@henkou_kahi_flg", SqlDbType.Int, 1, drRow.Item("henkou_kahi_flg")))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, drRow.Item("torikesi")))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, drRow.Item("add_login_user_id")))

        'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' �f�[�^�����݂��鎞�A�f�[�^���X�V����
    ''' </summary>
    ''' <param name="drRow">�V�K�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdBukkenRireki(ByVal drRow As DataRow) As Boolean

        'SQL�R�����g
        Dim commandTextSb As New System.Text.StringBuilder
        '�߂�f�[�^�Z�b�g

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL�R�����g�쐬
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("t_bukken_rireki SET ")
            .AppendLine("   naiyou = '���Ϗ��쐬��' + CONVERT(CHAR(10),GETDATE(),111) + '�č쐬', ")
            .AppendLine("   hanyou_date = CONVERT(CHAR(10),GETDATE(),111), ")
            .AppendLine("   upd_login_user_id = @upd_login_user_id, ")
            .AppendLine("   upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	kbn = @kbn ")
            .AppendLine("AND ")
            .AppendLine("	hosyousyo_no = @hosyousyo_no ")
            .AppendLine("AND ")
            .AppendLine("	rireki_syubetu = @rireki_syubetu ")
            .AppendLine("AND ")
            .AppendLine("	rireki_no = @rireki_no ")
        End With

        '�p�����[�^����
        '�敪
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, drRow.Item("kbn")))
        '�ԍ�
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, drRow.Item("hosyousyo_no")))
        '�������
        paramList.Add(MakeParam("@rireki_syubetu", SqlDbType.VarChar, 2, drRow.Item("rireki_syubetu")))
        '����NO
        paramList.Add(MakeParam("@rireki_no", SqlDbType.Int, 1, drRow.Item("rireki_no")))
        '�X�V��
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, drRow.Item("upd_login_user_id")))

        'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' �J�[�\���ړ����A�����X���擾
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <history>20100925�@�n���R</history>
    Public Function SelKameitenMei(ByVal strKameitenCd As String) As DataTable

        ' DataSet�C���X�^���X�̐���
        Dim dsDataSet As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("      MK.kameiten_cd, ")
            .AppendLine("      MK.kameiten_mei1, ")
            .AppendLine("      MK.torikesi, ")
            .AppendLine("      ISNULL(MKM.meisyou,'') AS torikesi_txt ")
            .AppendLine(" FROM ")
            .AppendLine("      m_kameiten AS MK WITH(READCOMMITTED) ")
            '==================2012/03/28 �ԗ� 405821�Č��̑Ή� �C����====================================
            .AppendLine("      LEFT OUTER JOIN ")
            .AppendLine("      m_kakutyou_meisyou MKM WITH(READCOMMITTED) ")
            .AppendLine("      ON ")
            .AppendLine("          MKM.meisyou_syubetu = 56 ")
            .AppendLine("          AND ")
            .AppendLine("          MK.torikesi = MKM.code ")
            '.AppendLine(" WHERE torikesi = @torikesi ")
            '.AppendLine(" AND kameiten_cd = @kameiten_cd ")
            .AppendLine(" WHERE ")
            .AppendLine(" MK.kameiten_cd = @kameiten_cd ")
            '==================2012/03/28 �ԗ� 405821�Č��̑Ή� �C����====================================
        End With

        '�p�����[�^�̐ݒ�
        '==================2012/03/28 �ԗ� 405821�Č��̑Ή� �폜��====================================
        'paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        '==================2012/03/28 �ԗ� 405821�Č��̑Ή� �폜��====================================
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        '�߂�
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' �@�ʐ����e�[�u���̃f�[�^���X�V����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdTeibetuSeikyuu(ByVal kbn As String, ByVal hosyousyo_no As String, ByVal bunrui_cd As String, ByVal upd_login_user_id As String) As Boolean

        'SQL�R�����g
        Dim commandTextSb As New System.Text.StringBuilder
        '�߂�f�[�^�Z�b�g

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL�R�����g�쐬
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("t_teibetu_seikyuu SET ")
            .AppendLine("   tys_mitsyo_sakusei_date = CONVERT(CHAR(10),GETDATE(),111), ")
            .AppendLine("   upd_login_user_id = @upd_login_user_id, ")
            .AppendLine("   upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	kbn = @kbn ")
            .AppendLine("AND ")
            .AppendLine("	hosyousyo_no = @hosyousyo_no ")
            .AppendLine("AND ")
            .AppendLine("	bunrui_cd = @bunrui_cd ")
        End With

        '�p�����[�^����
        '�敪
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, kbn))
        '�ԍ�
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, hosyousyo_no))
        '���ރR�[�h
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, bunrui_cd))
        '�X�V��
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, upd_login_user_id))

        'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

End Class
