Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>�����񍐏�_�e���[�o�͉��</summary>
''' <remarks>�����񍐏�_�e���[�o�͉�ʗp�@�\��񋟂���</remarks>
''' <history>
''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class KensaHoukokusyoOutputDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �����񍐏��Ǘ��e�[�u�����擾����
    ''' </summary>
    ''' <param name="strSendDateFrom">������From</param>
    ''' <param name="strSendDateTo">������To</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="tbxNoTo">�ԍ�From</param>
    ''' <param name="strNoTo">�ԍ�To</param>
    ''' <param name="strKameitenCdFrom">�����XCD</param>
    ''' <param name="strKensakuJyouken">�����������</param>
    ''' <param name="blnKensakuTaisyouGai">����͌����ΏۊO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function SelKensaHoukokusyoKanriSearch(ByVal strSendDateFrom As String, ByVal strSendDateTo As String, ByVal strKbn As String, _
                                                  ByVal tbxNoTo As String, ByVal strNoTo As String, ByVal strKameitenCdFrom As String, _
                                                  ByVal strKensakuJyouken As String, ByVal blnKensakuTaisyouGai As Boolean) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            '�����������
            If strKensakuJyouken.Trim.Equals("10") Then
                .AppendLine("   TOP 10 ")
            End If
            If strKensakuJyouken.Trim.Equals("100") Then
                .AppendLine("   TOP 100 ")
            End If
            .AppendLine("	 kanri_no   ")
            .AppendLine("	,kbn   ")
            .AppendLine("	,hosyousyo_no   ")
            .AppendLine("	,kameiten_cd   ")
            .AppendLine("	,kameiten_mei   ")
            .AppendLine("	,sesyu_mei   ")
            .AppendLine("	,kensa_hkks_busuu  ")
            .AppendLine("	,convert(varchar,kakunou_date,111) as kakunou_date   ")
            .AppendLine("	,convert(varchar,hassou_date,111) as hassou_date   ")
            .AppendLine("	,kanrihyou_out_flg   ")
            .AppendLine("	,souhujyou_out_flg   ")
            .AppendLine("	,kensa_hkks_out_flg   ")
            .AppendLine("	,souhu_tantousya   ")
            .AppendLine("FROM  t_kensa_hkks_kanri WITH(READCOMMITTED) ")
            .AppendLine("WHERE 1 = 1 ")

            '������
            If (Not strSendDateFrom.Trim.Equals(String.Empty)) AndAlso (Not strSendDateTo.Trim.Equals(String.Empty)) Then
                '������FROM�A������TO���������͂���Ă���ꍇ
                .AppendLine(" AND convert(varchar, hassou_date,111) BETWEEN @hassou_date_from AND @hassou_date_to ")   '������
            Else
                '�i�[��FROM�݂̂��邢�́A�i�[��TO�����͂���Ă���ꍇ
                If Not strSendDateFrom.Trim.Equals(String.Empty) Then
                    .AppendLine(" AND convert(varchar, hassou_date,111) = @hassou_date_from ")   '������From
                Else
                    If Not strSendDateTo.Trim.Equals(String.Empty) Then
                        .AppendLine(" AND convert(varchar, hassou_date,111) = @hassou_date_to ")   '������To
                    End If
                End If
            End If

            '�敪
            If Not strKbn.Trim.Equals(String.Empty) Then
                '�敪=���͂̏ꍇ
                .AppendLine(" AND kbn = @kbn ")   '�敪
            End If

            '�ԍ�
            If (Not tbxNoTo.Trim.Equals(String.Empty)) AndAlso (Not strNoTo.Trim.Equals(String.Empty)) Then
                '�ԍ�FROM�A�ԍ�TO���������͂���Ă���ꍇ
                .AppendLine(" AND hosyousyo_no BETWEEN @hosyousyo_no_from AND @hosyousyo_no_to ")   '�ԍ�
            Else
                '�ԍ�FROM�݂̂��邢�́A�ԍ�TO�����͂���Ă���ꍇ
                If Not tbxNoTo.Trim.Equals(String.Empty) Then
                    .AppendLine(" AND hosyousyo_no = @hosyousyo_no_from ")   '�ԍ�From
                Else
                    If Not strNoTo.Trim.Equals(String.Empty) Then
                        .AppendLine(" AND hosyousyo_no = @hosyousyo_no_to ")   '�ԍ�To
                    End If
                End If
            End If

            '�����XCD
            If Not strKameitenCdFrom.Equals(String.Empty) Then
                '�������@=���͂̏ꍇ
                .AppendLine(" AND kameiten_cd = @kameiten_cd ")   '�����XCD
            End If

            If blnKensakuTaisyouGai.Equals(True) Then
                '����͌����ΏۊO=�`�F�b�N�̏ꍇ
                .AppendLine(" AND torikesi = 0 ")
            End If
            .AppendLine("ORDER BY ")
            .AppendLine("	kanri_no ") '�Ǘ�No
            .AppendLine("	,kbn ") '�敪
            .AppendLine("	,hosyousyo_no ") '�ۏ؏�NO
            .AppendLine("	,kameiten_cd ")  '�����X�R�[�h        
        End With
        '������
        If Not strSendDateFrom.Equals(String.Empty) Then
            paramList.Add(MakeParam("@hassou_date_from", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSendDateFrom))))
        End If
        If Not strSendDateTo.Equals(String.Empty) Then
            paramList.Add(MakeParam("@hassou_date_to", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSendDateTo))))
        End If
        '�敪
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        '�ԍ�
        paramList.Add(MakeParam("@hosyousyo_no_from", SqlDbType.VarChar, 10, tbxNoTo))
        paramList.Add(MakeParam("@hosyousyo_no_to", SqlDbType.VarChar, 10, strNoTo))
        '�����X
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCdFrom))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "KensaHoukokusyo", paramList.ToArray)

        Return dsReturn.Tables("KensaHoukokusyo")

    End Function

    ''' <summary>
    ''' �Ǘ��\EXCEL�o�͂��擾����
    ''' </summary>
    ''' <param name="strKanriNo">�Ǘ�No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function SelKanrihyouExcelInfo(ByVal strKanriNo As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	kensa_hkks_busuu ") '--�����񍐏����s���� ")
            .AppendLine("	,kensa_hkks_jyuusyo1 ") '--�����񍐏����t��Z��1 ")
            .AppendLine("	,kameiten_mei ") '--�����X�� ")
            .AppendLine("	,'' AS check1 ") '--�y�`�F�b�N�z�� �F �Œ� ")
            .AppendLine("	,sesyu_mei ") '--�{�喼 ")
            .AppendLine("	,kbn ") '--�敪 ")
            .AppendLine("	,hosyousyo_no ") '--�ۏ؏�NO ")
            .AppendLine("	,CONVERT(VARCHAR(10),hassou_date,111) AS hassou_date ") '--������ ")
            .AppendLine("	,'' AS tyousa_gaiyou ") '--�����T�v ")
            .AppendLine("	,'' AS bikou ") '--���l ")
            .AppendLine("	,'' AS technical_guide ") '--�e�N�j�J���K�C�h ")
            .AppendLine("	,'' AS check2 ") '--�`�F�b�N ")
            .AppendLine("	,'' AS hassou_syuubetu ") '--������� ")
            .AppendLine("	,'' AS hassou_hiyou ") '--������p ")
            .AppendLine("	,kameiten_tanto ") '--�˗��S���Җ� ")
            .AppendLine("	,kameiten_cd ") '--�����X�R�[�h ")
            .AppendLine("	,busyo_mei ") '--������ ")
            .AppendLine("	,yuubin_no ") '--�X�֔ԍ� ")
            .AppendLine("	,kensa_hkks_jyuusyo2 ") '--�����񍐏����t��Z��2 ")
            .AppendLine("	,tel_no ") '--�d�b�ԍ� ")
            .AppendLine("	,kbn + kameiten_cd + '-' + kbn + hosyousyo_no + '.pdf' ") '--�t�@�C����(�敪�������X�R�[�h���u-�v&�敪���ۏ؏��ԍ����u.pdf�v) ")
            .AppendLine("	,tooshi_no ") '--�ʂ�No ")
            .AppendLine("	,todouhuken_cd ") '--�s���{���R�[�h ")
            .AppendLine("	,todouhuken_mei ") '--�s���{���� ")
            .AppendLine("FROM ")
            .AppendLine("	t_kensa_hkks_kanri WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kanri_no IN (" & strKanriNo & ") ")
            .AppendLine("ORDER BY ")
            .AppendLine("	hassou_date ASC ")
            .AppendLine("	,kameiten_cd ASC ")
            .AppendLine("	,hosyousyo_no ASC ")
        End With

        '�������s
        Itis.ApplicationBlocks.Data.SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "KanrihyouExcelInfo", paramList.ToArray)

        Return dsReturn.Tables("KanrihyouExcelInfo")

    End Function

    ''' <summary>
    ''' ���t��PDF�o�͂��擾����
    ''' </summary>
    ''' <param name="strKanriNo">�Ǘ�No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function SelSyoufujyouPdfInfo(ByVal strKanriNo As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	TKHK.kanri_no  ") '--�Ǘ�No	 ")
            .AppendLine("	,TKHK.kameiten_cd  ") '--�����X�R�[�h ")
            .AppendLine("	,TKHK.tooshi_no  ") '--�ʂ�No ")
            .AppendLine("	,TKHK.kameiten_mei  ") '--�����X�� ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(TKHK.kensa_hkks_jyuusyo1,'') <> '' THEN ")
            .AppendLine("			TKHK.kensa_hkks_jyuusyo1 ")
            .AppendLine("		ELSE ")
            .AppendLine("			MKJ.jyuusyo1 ")
            .AppendLine("		END AS jyuusyo1  ") '--�Z��1 ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(TKHK.kensa_hkks_jyuusyo1,'') <> '' THEN ")
            .AppendLine("			TKHK.kensa_hkks_jyuusyo2 ")
            .AppendLine("		ELSE ")
            .AppendLine("			MKJ.jyuusyo2 ")
            .AppendLine("		END AS jyuusyo2  ") '--�Z��2 ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(TKHK.kensa_hkks_jyuusyo1,'') <> '' THEN ")
            .AppendLine("			TKHK.busyo_mei ")
            .AppendLine("		ELSE ")
            .AppendLine("			MKJ.busyo_mei ")
            .AppendLine("		END AS busyo_mei  ") '--������ ")
            .AppendLine("	,TKHK.yuubin_no  ") '--�X�֔ԍ� ")
            .AppendLine("	,TKHK.tel_no  ") '--�d�b�ԍ� ")
            .AppendLine("	,TKHK.hassou_date  ") '--������ ")
            .AppendLine("	,TKHK.souhu_tantousya  ") '--���t�S���� ")
            .AppendLine("	,TKHK.kbn  ") '--�敪 ")
            .AppendLine("	,TKHK.hosyousyo_no  ") '--�ۏ؏�NO ")
            .AppendLine("	,TKHK.sesyu_mei  ") '--�{�喼 ")
            .AppendLine("	,TKHK.kensa_hkks_busuu  ") '--�����񍐏����s���� ")
            .AppendLine("FROM ")
            .AppendLine("	t_kensa_hkks_kanri as TKHK WITH(READCOMMITTED) ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kameiten_jyuusyo AS MKJ WITH(READCOMMITTED)  ") '--�����X�Z���}�X�^ ")
            .AppendLine("		ON ")
            .AppendLine("		MKJ.kameiten_cd = TKHK.kameiten_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MKJ.jyuusyo_no = 1 ")
            .AppendLine("WHERE ")
            .AppendLine("	TKHK.kanri_no IN (" & strKanriNo & ") ")
            .AppendLine("ORDER BY ")
            .AppendLine("	TKHK.tooshi_no  ") '--�ʂ�No ")
            .AppendLine("	,TKHK.hosyousyo_no  ") '--�ۏ؏�NO ")
        End With

        '�������s
        Itis.ApplicationBlocks.Data.SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "SyoufujyouPdfInfo", paramList.ToArray)

        Return dsReturn.Tables("SyoufujyouPdfInfo")

    End Function

    ''' <summary>
    ''' �񍐏�PDF�o�͂��擾����
    ''' </summary>
    ''' <param name="strKanriNo">�Ǘ�No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function SelHoukokusyoPdfInfo(ByVal strKanriNo As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	kanri_no  ") '--�Ǘ�No ")
            .AppendLine("	,kbn  ") '--�敪 ")
            .AppendLine("	,kameiten_cd  ") '--�����X�R�[�h ")
            .AppendLine("	,hosyousyo_no  ") '--�ۏ؏�No ")
            .AppendLine("	,sesyu_mei  ") '--�{�喼 ")
            .AppendLine("	,kameiten_mei  ") '--�����X�� ")
            .AppendLine("	,bukken_jyuusyo1  ") '--�����Z��1 ")
            .AppendLine("	,bukken_jyuusyo2  ") '--�����Z��2 ")
            .AppendLine("	,bukken_jyuusyo3  ") '--�����Z��3 ")
            .AppendLine("	,gaiyou_you  ") '--�����\��_�T�v�p ")
            .AppendLine("	,tatemono_kaisu  ") '--�����K�� ")
            .AppendLine("	,kensa_koutei_nm1  ") '--�����H����1 ")
            .AppendLine("	,kensa_koutei_nm2  ") '--�����H����2 ")
            .AppendLine("	,kensa_koutei_nm3  ") '--�����H����3 ")
            .AppendLine("	,kensa_koutei_nm4  ") '--�����H����4 ")
            .AppendLine("	,kensa_koutei_nm5  ") '--�����H����5 ")
            .AppendLine("	,kensa_koutei_nm6  ") '--�����H����6 ")
            .AppendLine("	,kensa_koutei_nm7  ") '--�����H����7 ")
            .AppendLine("	,kensa_koutei_nm8  ") '--�����H����8 ")
            .AppendLine("	,kensa_koutei_nm9  ") '--�����H����9 ")
            .AppendLine("	,kensa_koutei_nm10  ") '--�����H����10 ")
            .AppendLine("	,kensa_start_jissibi1  ") '--�������{��1 ")
            .AppendLine("	,kensa_start_jissibi2  ") '--�������{��2 ")
            .AppendLine("	,kensa_start_jissibi3  ") '--�������{��3 ")
            .AppendLine("	,kensa_start_jissibi4  ") '--�������{��4 ")
            .AppendLine("	,kensa_start_jissibi5  ") '--�������{��5 ")
            .AppendLine("	,kensa_start_jissibi6  ") '--�������{��6 ")
            .AppendLine("	,kensa_start_jissibi7  ") '--�������{��7 ")
            .AppendLine("	,kensa_start_jissibi8  ") '--�������{��8 ")
            .AppendLine("	,kensa_start_jissibi9  ") '--�������{��9 ")
            .AppendLine("	,kensa_start_jissibi10  ") '--�������{��10 ")
            .AppendLine("	,kensa_in_nm1  ") '--��������1 ")
            .AppendLine("	,kensa_in_nm2  ") '--��������2 ")
            .AppendLine("	,kensa_in_nm3  ") '--��������3 ")
            .AppendLine("	,kensa_in_nm4  ") '--��������4 ")
            .AppendLine("	,kensa_in_nm5  ") '--��������5 ")
            .AppendLine("	,kensa_in_nm6  ") '--��������6 ")
            .AppendLine("	,kensa_in_nm7  ") '--��������7 ")
            .AppendLine("	,kensa_in_nm8  ") '--��������8 ")
            .AppendLine("	,kensa_in_nm9  ") '--��������9 ")
            .AppendLine("	,kensa_in_nm10  ") '--��������10 ")
            .AppendLine("	,fc_nm  ") '--FC�� ")
            .AppendLine("FROM ")
            .AppendLine("	t_kensa_hkks_kanri WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kanri_no = @kanri_no ")
        End With

        paramList.Add(Itis.ApplicationBlocks.Data.SQLHelper.MakeParam("@kanri_no", SqlDbType.Int, 10, strKanriNo))

        '�������s
        Itis.ApplicationBlocks.Data.SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "HoukokusyoPdfInfo", paramList.ToArray)

        Return dsReturn.Tables("HoukokusyoPdfInfo")

    End Function

    ''' <summary>
    ''' �����񍐏��Ǘ��������擾����
    ''' </summary>
    ''' <param name="strSendDateFrom">������From</param>
    ''' <param name="strSendDateTo">������To</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="tbxNoTo">�ԍ�From</param>
    ''' <param name="strNoTo">�ԍ�To</param>
    ''' <param name="strKameitenCdFrom">�����XCD</param>
    ''' <param name="strKensakuJyouken">�����������</param>
    ''' <param name="blnKensakuTaisyouGai">����͌����ΏۊO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function SelKensaHoukokusyoKanriSearchCount(ByVal strSendDateFrom As String, ByVal strSendDateTo As String, ByVal strKbn As String, _
                                                  ByVal tbxNoTo As String, ByVal strNoTo As String, ByVal strKameitenCdFrom As String, _
                                                  ByVal strKensakuJyouken As String, ByVal blnKensakuTaisyouGai As Boolean) As Integer

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(kanri_no) ")  '--����
            .AppendLine("FROM ")
            .AppendLine("	t_kensa_hkks_kanri WITH(READCOMMITTED) ")    '--�����񍐏��Ǘ�T
            .AppendLine("WHERE 1 = 1 ")

            '������
            If (Not strSendDateFrom.Trim.Equals(String.Empty)) AndAlso (Not strSendDateTo.Trim.Equals(String.Empty)) Then
                '������FROM�A������TO���������͂���Ă���ꍇ
                .AppendLine(" AND convert(varchar, hassou_date,111) BETWEEN @hassou_date_from AND @hassou_date_to ")   '������
            Else
                '�i�[��FROM�݂̂��邢�́A�i�[��TO�����͂���Ă���ꍇ
                If Not strSendDateFrom.Trim.Equals(String.Empty) Then
                    .AppendLine(" AND convert(varchar, hassou_date,111) = @hassou_date_from ")   '������From
                Else
                    If Not strSendDateTo.Trim.Equals(String.Empty) Then
                        .AppendLine(" AND convert(varchar, hassou_date,111) = @hassou_date_to ")   '������To
                    End If
                End If
            End If

            '�敪
            If Not strKbn.Trim.Equals(String.Empty) Then
                '�敪=���͂̏ꍇ
                .AppendLine(" AND kbn = @kbn ")   '�敪
            End If

            '�ԍ�
            If (Not tbxNoTo.Trim.Equals(String.Empty)) AndAlso (Not strNoTo.Trim.Equals(String.Empty)) Then
                '�ԍ�FROM�A�ԍ�TO���������͂���Ă���ꍇ
                .AppendLine(" AND hosyousyo_no BETWEEN @hosyousyo_no_from AND @hosyousyo_no_to ")   '�ԍ�
            Else
                '�ԍ�FROM�݂̂��邢�́A�ԍ�TO�����͂���Ă���ꍇ
                If Not tbxNoTo.Trim.Equals(String.Empty) Then
                    .AppendLine(" AND hosyousyo_no = @hosyousyo_no_from ")   '�ԍ�From
                Else
                    If Not strNoTo.Trim.Equals(String.Empty) Then
                        .AppendLine(" AND hosyousyo_no = @hosyousyo_no_to ")   '�ԍ�To
                    End If
                End If
            End If

            '�����XCD
            If Not strKameitenCdFrom.Equals(String.Empty) Then
                '�������@=���͂̏ꍇ
                .AppendLine(" AND kameiten_cd = @kameiten_cd ")   '�����XCD
            End If

            If blnKensakuTaisyouGai.Equals(True) Then
                '����͌����ΏۊO=�`�F�b�N�̏ꍇ
                .AppendLine(" AND torikesi = 0 ")
            End If

        End With
        '������
        If Not strSendDateFrom.Equals(String.Empty) Then
            paramList.Add(MakeParam("@hassou_date_from", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSendDateFrom))))
        End If
        If Not strSendDateTo.Equals(String.Empty) Then
            paramList.Add(MakeParam("@hassou_date_to", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSendDateTo))))
        End If
        '�敪
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        '�ԍ�
        paramList.Add(MakeParam("@hosyousyo_no_from", SqlDbType.VarChar, 10, tbxNoTo))
        paramList.Add(MakeParam("@hosyousyo_no_to", SqlDbType.VarChar, 10, strNoTo))
        '�����X
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCdFrom))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "KensaHoukokusyo", paramList.ToArray)

        Return CInt(dsReturn.Tables(0).Rows(0).Item(0).ToString.Trim)

    End Function

    ''' <summary>
    ''' <summary>�����񍐏��Ǘ����X�V����</summary>
    ''' </summary>
    ''' <param name="strUserId">�X�V���O�C�����[�UID</param>
    ''' <param name="dtKensa">�����񍐏��Ǘ��e�[�u��</param>
    ''' <param name="strFlg">�{�^���敪(1:�Ǘ��\EXCEL�o��;2:���t��PDF�o��;3:�񍐏�PDF�o��)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function UpdKensahoukokusho(ByVal strUserId As String, ByVal dtKensa As DataTable, ByVal strFlg As String) As Boolean

        '�߂�l
        Dim UpdCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("UPDATE  ")
            .AppendLine("	t_kensa_hkks_kanri WITH(UPDLOCK) ")
            .AppendLine("SET  ")
            .AppendLine("	kensa_hkks_busuu = @kensa_hkks_busuu ")    '�����񍐏����s����
            If strFlg = "1" Then
                .AppendLine("	,kanrihyou_out_flg = '1' ")            '�Ǘ��\�o�̓t���O
                .AppendLine("	,kanrihyou_out_date = GETDATE() ")     '�Ǘ��\�o�͓�
            ElseIf strFlg = "2" Then
                .AppendLine("	,souhujyou_out_flg = '1' ")            '���t��o�̓t���O
                .AppendLine("	,souhujyou_out_date = GETDATE() ")     '���t��o�͓�
            Else
                .AppendLine("	,kensa_hkks_out_flg = '1' ")           '�����񍐏��o�̓t���O
                .AppendLine("	,kensa_hkks_out_date = GETDATE() ")    '�����񍐏��o�͓�
            End If
            .AppendLine("	,tooshi_no = @tooshi_no ")                 '�ʂ�No
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ") '�X�V���O�C�����[�UID
            .AppendLine("	,upd_datetime  = GETDATE() ")              '�X�V����
            .AppendLine("WHERE  ")
            .AppendLine("	kanri_no = @kanri_no ")  '�Ǘ�No
            .AppendLine("	AND ")
            .AppendLine("	kbn = @kbn ")  '�敪
            .AppendLine("	AND ")
            .AppendLine("	hosyousyo_no = @hosyousyo_no ") '�ۏ؏�NO
            .AppendLine("	AND ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")  '�����X�R�[�h

        End With

        For i As Integer = 0 To dtKensa.Rows.Count - 1
            paramList.Clear()
            paramList.Add(MakeParam("@kensa_hkks_busuu", SqlDbType.Int, 1, dtKensa.Rows(i).Item("kensa_hkks_busuu").ToString.Trim)) '�ʂ�No	
            paramList.Add(MakeParam("@tooshi_no", SqlDbType.Int, 1000, dtKensa.Rows(i).Item("tooshi_no").ToString.Trim)) '�ʂ�No	
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId)) '�X�V���O�C�����[�UID
            paramList.Add(MakeParam("kanri_no", SqlDbType.Int, 1000, dtKensa.Rows(i).Item("kanri_no").ToString.Trim))  '�Ǘ�No
            paramList.Add(MakeParam("kbn", SqlDbType.Char, 1, dtKensa.Rows(i).Item("kbn").ToString.Trim))  '�敪
            paramList.Add(MakeParam("hosyousyo_no", SqlDbType.VarChar, 10, dtKensa.Rows(i).Item("hosyousyo_no").ToString.Trim))  '�ۏ؏�NO
            paramList.Add(MakeParam("kameiten_cd", SqlDbType.VarChar, 5, dtKensa.Rows(i).Item("kameiten_cd").ToString.Trim))  '�����X�R�[�h

            '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
            Try
                UpdCount = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
            Catch ex As Exception
                Return False
            End Try

            If Not (UpdCount > 0) Then
                Return False
            End If
        Next

        Return True
    End Function

    ''' <summary>
    ''' �����X�}�X�^���擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����XCD</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function SelMkameiten(ByVal strKameitenCd As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("		SELECT  ")
            .AppendLine("			kameiten_mei1 ")  '--�����X��1
            .AppendLine("		FROM  ")
            .AppendLine("			m_kameiten  WITH(READCOMMITTED) ")    '--�����X�}�X�^
            .AppendLine("WHERE 1 = 1 ")

            '�����X�R�[�h  
            If Not strKameitenCd.Trim.Equals(String.Empty) Then
                '�����X�R�[�h  =���͂̏ꍇ
                .AppendLine(" AND kameiten_cd = @kameiten_cd ")   '�����X�R�[�h      
            End If
            .AppendLine("ORDER BY kameiten_cd ") '�����X�R�[�h
        End With

        '�����XCD
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "Mkameiten", paramList.ToArray)

        Return dsReturn.Tables("Mkameiten")

    End Function

End Class
