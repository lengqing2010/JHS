Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>�����񍐏��Ǘ�</summary>
''' <remarks>�����񍐏��Ǘ��p�@�\��񋟂���</remarks>
''' <history>
''' <para>2015/12/04�@������(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class KensaHoukokusyoKanriSearchListDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �����񍐏��Ǘ��e�[�u�����擾����
    ''' </summary>
    ''' <param name="strKakunouDateFrom">�i�[��From</param>
    ''' <param name="strKakunouDateTo">�i�[��To</param>
    ''' <param name="strSendDateFrom">������From</param>
    ''' <param name="strSendDateTo">������To</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strNoFrom">�ԍ�From</param>
    ''' <param name="strNoTo">�ԍ�To</param>
    ''' <param name="strKameitenCdFrom">�����XCD</param>
    ''' <param name="strKensakuJyouken">�����������</param>
    ''' <param name="blnKensakuTaisyouGai">����͌����ΏۊO</param>
    ''' <param name="blnSendDateTaisyouGai">�������Z�b�g�ς݂͑ΏۊO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function SelKensaHoukokusyoKanriSearch(ByVal strKakunouDateFrom As String, ByVal strKakunouDateTo As String, ByVal strSendDateFrom As String, _
                                                  ByVal strSendDateTo As String, ByVal strKbn As String, ByVal strNoFrom As String, _
                                                  ByVal strNoTo As String, ByVal strKameitenCdFrom As String, ByVal strKensakuJyouken As String, _
                                                  ByVal blnKensakuTaisyouGai As Boolean, ByVal blnSendDateTaisyouGai As Boolean) As Data.DataTable

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
            .AppendLine("	kanri_no   ")
            .AppendLine("	,kbn   ")
            .AppendLine("	,hosyousyo_no   ")
            .AppendLine("	,kameiten_cd   ")
            .AppendLine("	,kakunou_date   ")
            .AppendLine("	,kameiten_mei   ")
            .AppendLine("	,sesyu_mei   ")
            .AppendLine("	,CASE torikesi WHEN '1' THEN '���' ELSE '' end as torikesi   ")
            .AppendLine("	,kensa_hkks_busuu   ")
            .AppendLine("	,kensa_hkks_jyuusyo1   ")
            .AppendLine("	,kensa_hkks_jyuusyo2   ")
            .AppendLine("	,yuubin_no   ")
            .AppendLine("	,tel_no   ")
            .AppendLine("	,busyo_mei   ")
            .AppendLine("	,todouhuken_cd   ")
            .AppendLine("	,todouhuken_mei   ")
            .AppendLine("	,hassou_date   ")
            .AppendLine("	,hassou_date_in_flg   ")
            .AppendLine("	,souhu_tantousya   ")
            .AppendLine("	,bukken_jyuusyo1   ")
            .AppendLine("	,bukken_jyuusyo2   ")
            .AppendLine("	,bukken_jyuusyo3   ")
            .AppendLine("	,tatemono_kouzou   ")
            .AppendLine("	,tatemono_kaisu   ")
            .AppendLine("	,fc_nm   ")
            .AppendLine("	,kameiten_tanto   ")
            .AppendLine("	,tatemono_kameiten_cd   ")
            .AppendLine("	,kanrihyou_out_flg   ")
            .AppendLine("	,kanrihyou_out_date   ")
            .AppendLine("	,souhujyou_out_flg   ")
            .AppendLine("	,souhujyou_out_date   ")
            .AppendLine("	,kensa_hkks_out_flg   ")
            .AppendLine("	,kensa_hkks_out_date   ")
            .AppendLine("	,tooshi_no   ")
            .AppendLine("	,kensa_koutei_nm1   ")
            .AppendLine("	,kensa_koutei_nm2   ")
            .AppendLine("	,kensa_koutei_nm3   ")
            .AppendLine("	,kensa_koutei_nm4   ")
            .AppendLine("	,kensa_koutei_nm5   ")
            .AppendLine("	,kensa_koutei_nm6   ")
            .AppendLine("	,kensa_koutei_nm7   ")
            .AppendLine("	,kensa_koutei_nm8   ")
            .AppendLine("	,kensa_koutei_nm9   ")
            .AppendLine("	,kensa_koutei_nm10   ")
            .AppendLine("	,kensa_start_jissibi1   ")
            .AppendLine("	,kensa_start_jissibi2   ")
            .AppendLine("	,kensa_start_jissibi3   ")
            .AppendLine("	,kensa_start_jissibi4   ")
            .AppendLine("	,kensa_start_jissibi5   ")
            .AppendLine("	,kensa_start_jissibi6   ")
            .AppendLine("	,kensa_start_jissibi7   ")
            .AppendLine("	,kensa_start_jissibi8   ")
            .AppendLine("	,kensa_start_jissibi9   ")
            .AppendLine("	,kensa_start_jissibi10   ")
            .AppendLine("	,kensa_in_nm1   ")
            .AppendLine("	,kensa_in_nm2   ")
            .AppendLine("	,kensa_in_nm3   ")
            .AppendLine("	,kensa_in_nm4   ")
            .AppendLine("	,kensa_in_nm5   ")
            .AppendLine("	,kensa_in_nm6   ")
            .AppendLine("	,kensa_in_nm7   ")
            .AppendLine("	,kensa_in_nm8   ")
            .AppendLine("	,kensa_in_nm9   ")
            .AppendLine("	,kensa_in_nm10   ")
            .AppendLine("	,add_login_user_id   ")
            .AppendLine("	,add_datetime   ")
            .AppendLine("	,upd_login_user_id   ")
            .AppendLine("	,upd_datetime   ")
            .AppendLine("FROM  t_kensa_hkks_kanri WITH(READCOMMITTED) ")
            .AppendLine("WHERE 1 = 1 ")

            '�i�[��
            If (Not strKakunouDateFrom.Trim.Equals(String.Empty)) AndAlso (Not strKakunouDateTo.Trim.Equals(String.Empty)) Then
                '�i�[��FROM�A�i�[��TO���������͂���Ă���ꍇ
                .AppendLine(" AND convert(varchar, kakunou_date,111) BETWEEN @kakunou_date_from AND @kakunou_date_to ")   '�i�[��
            Else
                '�i�[��FROM�݂̂��邢�́A�i�[��TO�����͂���Ă���ꍇ
                If Not strKakunouDateFrom.Trim.Equals(String.Empty) Then
                    .AppendLine(" AND convert(varchar, kakunou_date,111) = @kakunou_date_from ")   '�i�[��From
                Else
                    If Not strKakunouDateTo.Trim.Equals(String.Empty) Then
                        .AppendLine(" AND convert(varchar, kakunou_date,111) = @kakunou_date_to ")   '�i�[��To
                    End If
                End If
            End If

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
            If (Not strNoFrom.Trim.Equals(String.Empty)) AndAlso (Not strNoTo.Trim.Equals(String.Empty)) Then
                '�ԍ�FROM�A�ԍ�TO���������͂���Ă���ꍇ
                .AppendLine(" AND hosyousyo_no BETWEEN @hosyousyo_no_from AND @hosyousyo_no_to ")   '�ԍ�
            Else
                '�ԍ�FROM�݂̂��邢�́A�ԍ�TO�����͂���Ă���ꍇ
                If Not strNoFrom.Trim.Equals(String.Empty) Then
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
            If blnSendDateTaisyouGai.Equals(True) Then
                '�������Z�b�g�ς݂͑ΏۊO=�`�F�b�N�̏ꍇ
                .AppendLine(" AND hassou_date  IS NULL ")
                .AppendLine(" AND hassou_date_in_flg <> 1 ")
            End If
            .AppendLine("ORDER BY ")
            .AppendLine("	kanri_no ") '�Ǘ�No
            .AppendLine("	,kbn ") '�敪
            .AppendLine("	,hosyousyo_no ") '�ۏ؏�NO
            .AppendLine("	,kameiten_cd ")  '�����X�R�[�h        
        End With
        '�i�[��
        If Not strKakunouDateFrom.Equals(String.Empty) Then
            paramList.Add(MakeParam("@kakunou_date_from", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strKakunouDateFrom))))
        End If
        If Not strKakunouDateTo.Equals(String.Empty) Then
            paramList.Add(MakeParam("@kakunou_date_to", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strKakunouDateTo))))
        End If
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
        paramList.Add(MakeParam("@hosyousyo_no_from", SqlDbType.VarChar, 10, strNoFrom))
        paramList.Add(MakeParam("@hosyousyo_no_to", SqlDbType.VarChar, 10, strNoTo))
        '�����X
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCdFrom))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "KensaHoukokusyo", paramList.ToArray)

        Return dsReturn.Tables("KensaHoukokusyo")

    End Function

    ''' <summary>
    ''' �����񍐏��Ǘ��������擾����
    ''' </summary>
    ''' <param name="strKakunouDateFrom">�i�[��From</param>
    ''' <param name="strKakunouDateTo">�i�[��To</param>
    ''' <param name="strSendDateFrom">������From</param>
    ''' <param name="strSendDateTo">������To</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strNoFrom">�ԍ�From</param>
    ''' <param name="strNoTo">�ԍ�To</param>
    ''' <param name="strKameitenCdFrom">�����XCD</param>
    ''' <param name="strKensakuJyouken">�����������</param>
    ''' <param name="blnKensakuTaisyouGai">����͌����ΏۊO</param>
    ''' <param name="blnSendDateTaisyouGai">�������Z�b�g�ς݂͑ΏۊO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function SelKensaHoukokusyoKanriSearchCount(ByVal strKakunouDateFrom As String, ByVal strKakunouDateTo As String, ByVal strSendDateFrom As String, _
                                                  ByVal strSendDateTo As String, ByVal strKbn As String, ByVal strNoFrom As String, _
                                                  ByVal strNoTo As String, ByVal strKameitenCdFrom As String, ByVal strKensakuJyouken As String, _
                                                  ByVal blnKensakuTaisyouGai As Boolean, ByVal blnSendDateTaisyouGai As Boolean) As Integer

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

            '�i�[��
            If (Not strKakunouDateFrom.Trim.Equals(String.Empty)) AndAlso (Not strKakunouDateTo.Trim.Equals(String.Empty)) Then
                '�i�[��FROM�A�i�[��TO���������͂���Ă���ꍇ
                .AppendLine(" AND convert(varchar, kakunou_date,111) BETWEEN @kakunou_date_from AND @kakunou_date_to ")   '�i�[��
            Else
                '�i�[��FROM�݂̂��邢�́A�i�[��TO�����͂���Ă���ꍇ
                If Not strKakunouDateFrom.Trim.Equals(String.Empty) Then
                    .AppendLine(" AND convert(varchar, kakunou_date,111) = @kakunou_date_from ")   '�i�[��From
                Else
                    If Not strKakunouDateTo.Trim.Equals(String.Empty) Then
                        .AppendLine(" AND convert(varchar, kakunou_date,111) = @kakunou_date_to ")   '�i�[��To
                    End If
                End If
            End If

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
            If (Not strNoFrom.Trim.Equals(String.Empty)) AndAlso (Not strNoTo.Trim.Equals(String.Empty)) Then
                '�ԍ�FROM�A�ԍ�TO���������͂���Ă���ꍇ
                .AppendLine(" AND hosyousyo_no BETWEEN @hosyousyo_no_from AND @hosyousyo_no_to ")   '�ԍ�
            Else
                '�ԍ�FROM�݂̂��邢�́A�ԍ�TO�����͂���Ă���ꍇ
                If Not strNoFrom.Trim.Equals(String.Empty) Then
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
            If blnSendDateTaisyouGai.Equals(True) Then
                '�������Z�b�g�ς݂͑ΏۊO=�`�F�b�N�̏ꍇ
                .AppendLine(" AND hassou_date  IS NULL ")
                .AppendLine(" AND hassou_date_in_flg <> 1 ")
            End If

        End With
        '�i�[��
        If Not strKakunouDateFrom.Equals(String.Empty) Then
            paramList.Add(MakeParam("@kakunou_date_from", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strKakunouDateFrom))))
        End If
        If Not strKakunouDateTo.Equals(String.Empty) Then
            paramList.Add(MakeParam("@kakunou_date_to", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strKakunouDateTo))))
        End If
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
        paramList.Add(MakeParam("@hosyousyo_no_from", SqlDbType.VarChar, 10, strNoFrom))
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
    ''' <param name="strHassoudate">������</param>
    ''' <param name="strSouhutantousya">���t�S����</param>
    ''' <param name="strUserId">�X�V���O�C�����[�UID</param>
    ''' <param name="dtKensa">�����񍐏��Ǘ��e�[�u��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function UpdKensahoukokusho(ByVal strHassoudate As String, ByVal strSouhutantousya As String, ByVal strUserId As String, ByVal dtKensa As DataTable) As Boolean

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
            .AppendLine("	hassou_date = @hassou_date ")   '������
            .AppendLine("	,hassou_date_in_flg = '1' ")   '���������̓t���O
            .AppendLine("	,souhu_tantousya = @souhu_tantousya ")   '���t�S����
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")  '�X�V���O�C�����[�UID
            .AppendLine("	,upd_datetime  = GETDATE() ")   '�X�V����
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
            '�p�����[�^�̐ݒ�
            paramList.Add(MakeParam("@hassou_date", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strHassoudate))))
            paramList.Add(MakeParam("@souhu_tantousya", SqlDbType.VarChar, 128, strSouhutantousya)) '���t�S����	
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
    ''' <summary>�����񍐏��Ǘ����X�V����</summary>
    ''' </summary>
    ''' <param name="strUserId">�X�V���O�C�����[�UID</param>
    ''' <param name="dtKensa">�����񍐏��Ǘ��e�[�u��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04�@������(��A���V�X�e����)�@�V�K�쐬</para>
    Public Function UpdKensahoukokushoTorikesi(ByVal strUserId As String, ByVal dtKensa As DataTable) As Boolean

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
            .AppendLine("	torikesi = '1' ")   '���
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")  '�X�V���O�C�����[�UID
            .AppendLine("	,upd_datetime  = GETDATE() ")   '�X�V����
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
    ''' <para>2015/12/04�@������(��A���V�X�e����)�@�V�K�쐬</para>
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
