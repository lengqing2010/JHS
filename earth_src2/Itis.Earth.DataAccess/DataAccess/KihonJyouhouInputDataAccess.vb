Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>�����X����V�K�o�^����</summary>
''' <remarks>�����X���V�K�o�^��񋟂���</remarks>
''' <history>
''' <para>2009/07/15�@�t��(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class KihonJyouhouInputDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>�����X�R�[�h�̍ő�l���擾����</summary>
    ''' <param name="strKbn">�p�����[�^</param>
    ''' <returns>�����X�R�[�h�ő�l�f�[�^�e�[�u��</returns>
    Public Function SelMaxKameitenCd(ByVal strKbn As String, _
                                    Optional ByVal strCdFrom As String = "", _
                                    Optional ByVal strCdTo As String = "") As KihonJyouhouInputDataSet.KameitenCdTableDataTable
        ' DataSet�C���X�^���X�̐���()
        Dim dsKihonJyouhouInputDataSet As New KihonJyouhouInputDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   MAX(kameiten_cd) AS kameiten_cd ")
            .AppendLine("FROM ")
            .AppendLine("   m_kameiten WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   kbn = @kbn ")
            .AppendLine("   AND UPPER(kameiten_cd) = LOWER(kameiten_cd) COLLATE Japanese_CS_AS_KS_WS ")
            If strCdFrom <> String.Empty Then
                .AppendLine("   AND kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, strCdFrom))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 5, strCdTo))
            End If
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsKihonJyouhouInputDataSet, _
                    dsKihonJyouhouInputDataSet.KameitenCdTable.TableName, paramList.ToArray)

        Return dsKihonJyouhouInputDataSet.KameitenCdTable

    End Function

    ''' <summary>�����X�R�[�h�̍ő�l�ƍ̔Ԑݒ�͈̔͂��擾����</summary>
    ''' <param name="strKbn">�敪</param>
    ''' <returns>�����X�R�[�h�̍ő�l�ƍ̔Ԑݒ�͈̔͂��擾����</returns>
    ''' <history>
    ''' <para>2012/11/19�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Public Function SelMaxKameitenCd1(ByVal strKbn As String) As Data.DataTable

        ' DataSet�C���X�^���X�̐���()
        Dim dsKihonJyouhouInputDataSet As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	ISNULL(MK.kameiten_cd_max,'') AS kameiten_cd_max ")
            .AppendLine("	,ISNULL(MDK.kameiten_saiban_from,'') AS kameiten_saiban_from ")
            .AppendLine("	,ISNULL(MDK.kameiten_saiban_to,'') AS kameiten_saiban_to ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			@kbn AS kbn ")
            .AppendLine("			,( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MAX(SUB_MK.kameiten_cd)  ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kameiten AS SUB_MK WITH(READCOMMITTED) ")
            .AppendLine("					LEFT JOIN ")
            .AppendLine("					m_data_kbn AS SUB_MDK WITH(READCOMMITTED) ")
            .AppendLine("					ON ")
            .AppendLine("						SUB_MK.kbn = SUB_MDK.kbn ")
            .AppendLine("				WHERE ")
            .AppendLine("					SUB_MK.kbn = @kbn ")
            .AppendLine("					AND ")
            .AppendLine("					PATINDEX('%[^0-9]%',convert(VARCHAR(9),ISNULL(SUB_MK.kameiten_cd,''))) = 0 ")
            .AppendLine("					AND ")
            .AppendLine("					SUB_MK.kameiten_cd BETWEEN SUB_MDK.kameiten_saiban_from AND SUB_MDK.kameiten_saiban_to ")
            .AppendLine("				GROUP BY ")
            .AppendLine("					SUB_MK.kbn ")
            .AppendLine("			) AS kameiten_cd_max ")
            .AppendLine("	) AS MK ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_data_kbn AS MDK WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		MK.kbn = MDK.kbn ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsKihonJyouhouInputDataSet, _
                    "dtMaxKameitenCd", paramList.ToArray)

        Return dsKihonJyouhouInputDataSet.Tables("dtMaxKameitenCd")

    End Function

    ''' <summary>�����X�R�[�h���擾����</summary>
    ''' <param name="strKbn">�p�����[�^</param>
    ''' <param name="strCd">�p�����[�^</param>
    ''' <returns>�����X�R�[�h�f�[�^�e�[�u��</returns>
    Public Function SelKameitenCd(ByVal strKbn As String, ByVal strCd As String) As KihonJyouhouInputDataSet.KameitenCdTableDataTable
        ' DataSet�C���X�^���X�̐���()
        Dim dsKihonJyouhouInputDataSet As New KihonJyouhouInputDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   kameiten_cd ")
            .AppendLine("FROM ")
            .AppendLine("   m_kameiten WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   kameiten_cd = @kameiten_cd ")
            If strKbn <> String.Empty Then
                .AppendLine("   AND kbn = @kbn ")
            End If
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strCd))
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsKihonJyouhouInputDataSet, _
                    dsKihonJyouhouInputDataSet.KameitenCdTable.TableName, paramList.ToArray)

        Return dsKihonJyouhouInputDataSet.KameitenCdTable

    End Function

    ''' <summary>�����X�}�X�^�e�[�u���ɓo�^����</summary>
    ''' <param name="dtParamKameitenInfo">�p�����[�^</param>
    ''' <returns>����</returns>
    Public Function InsKameitenInfo(ByVal dtParamKameitenInfo As KihonJyouhouInputDataSet.Param_KameitenInfoDataTable) As Boolean
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" INSERT INTO ")
            .AppendLine("   m_kameiten WITH(UPDLOCK) ")
            .AppendLine("   (kameiten_cd, ")
            .AppendLine("   kbn, ")
            .AppendLine("   torikesi, ")
            .AppendLine("   kameiten_mei1, ")
            .AppendLine("   tenmei_kana1, ")
            .AppendLine("   kameiten_mei2, ")
            .AppendLine("   tenmei_kana2, ")
            .AppendLine("   builder_no, ")
            .AppendLine("   keiretu_cd, ")
            .AppendLine("   eigyousyo_cd, ")
            .AppendLine("   th_kasi_cd, ")
            '==========================2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���==========================================
            commandTextSb.AppendLine(" torikesi_set_date, ")
            '==========================2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���==========================================
            .AppendLine("   add_login_user_id, ")
            .AppendLine("   add_datetime, ")
            .AppendLine("   upd_login_user_id, ")
            .AppendLine("   upd_datetime) ")
            .AppendLine(" VALUES( ")
            .AppendLine("   @kameiten_cd, ")
            .AppendLine("   @kbn, ")
            .AppendLine("   @torikesi, ")
            .AppendLine("   @kameiten_mei1, ")
            .AppendLine("   @tenmei_kana1, ")
            .AppendLine("   @kameiten_mei2, ")
            .AppendLine("   @tenmei_kana2, ")
            .AppendLine("   @builder_no, ")
            .AppendLine("   @keiretu_cd, ")
            .AppendLine("   @eigyousyo_cd, ")
            .AppendLine("   @th_kasi_cd, ")
            '==========================2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���==========================================
            If Not dtParamKameitenInfo(0).torikesi.ToString.Trim.Equals("0") Then
                commandTextSb.AppendLine(" GETDATE(), ")
            Else
                commandTextSb.AppendLine(" NULL, ")
            End If
            '==========================2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���==========================================
            .AppendLine("   @add_login_user_id, ")
            .AppendLine("   GETDATE(), ")
            .AppendLine("   @upd_login_user_id, ")
            .AppendLine("   GETDATE()) ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd))
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, dtParamKameitenInfo(0).kbn))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, dtParamKameitenInfo(0).torikesi))
        paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 40, dtParamKameitenInfo(0).kameiten_mei1))
        paramList.Add(MakeParam("@tenmei_kana1", SqlDbType.VarChar, 20, dtParamKameitenInfo(0).tenmei_kana1))
        paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 40, IIf(dtParamKameitenInfo(0).kameiten_mei2 = "", DBNull.Value, dtParamKameitenInfo(0).kameiten_mei2)))
        paramList.Add(MakeParam("@tenmei_kana2", SqlDbType.VarChar, 20, IIf(dtParamKameitenInfo(0).tenmei_kana2 = "", DBNull.Value, dtParamKameitenInfo(0).tenmei_kana2)))
        paramList.Add(MakeParam("@builder_no", SqlDbType.VarChar, 9, IIf(dtParamKameitenInfo(0).builder_no = "", DBNull.Value, dtParamKameitenInfo(0).builder_no)))
        paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, IIf(dtParamKameitenInfo(0).keiretu_cd = "", DBNull.Value, dtParamKameitenInfo(0).keiretu_cd)))
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, IIf(dtParamKameitenInfo(0).eigyousyo_cd = "", DBNull.Value, dtParamKameitenInfo(0).eigyousyo_cd)))
        paramList.Add(MakeParam("@th_kasi_cd", SqlDbType.VarChar, 7, IIf(dtParamKameitenInfo(0).th_kasi_cd = "", DBNull.Value, dtParamKameitenInfo(0).th_kasi_cd)))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dtParamKameitenInfo(0).add_login_user_id))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, dtParamKameitenInfo(0).add_login_user_id))

        Try
            '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
            ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString, paramList.ToArray)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function InsKameitenRenkeiInfo(ByVal dtParamKameitenInfo As KihonJyouhouInputDataSet.Param_KameitenInfoDataTable) As Boolean
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" DELETE FROM ")
            .AppendLine("   m_kameiten_renkei ")
            .AppendLine(" WHERE ")
            .AppendLine("   kameiten_cd = @kameiten_cd ")
            .AppendLine(" INSERT INTO ")
            .AppendLine("   m_kameiten_renkei WITH(UPDLOCK) ")
            .AppendLine("   (kameiten_cd, ")
            .AppendLine("   renkei_siji_cd, ")
            .AppendLine("   sousin_jyky_cd, ")
            .AppendLine("   sousin_kanry_datetime, ")
            .AppendLine("   upd_login_user_id, ")
            .AppendLine("   upd_datetime) ")
            .AppendLine(" VALUES( ")
            .AppendLine("   @kameiten_cd, ")
            .AppendLine("   @renkei_siji_cd, ")
            .AppendLine("   @sousin_jyky_cd, ")
            .AppendLine("   @sousin_kanry_datetime, ")
            .AppendLine("   @upd_login_user_id, ")
            .AppendLine("   GETDATE()) ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd))
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, 1))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
        paramList.Add(MakeParam("@sousin_kanry_datetime", SqlDbType.DateTime, 20, DBNull.Value))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, dtParamKameitenInfo(0).add_login_user_id))

        Try
            '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
            ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString, paramList.ToArray)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    ''' <summary>
    ''' �u����vddl�̃f�[�^���擾����
    ''' </summary>
    ''' <returns>�u����vddl�̃f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    ''' <history>2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ�</history>
    Public Function SelTorikesiList(ByVal strCd As String) As Data.DataTable

        ' DataSet�C���X�^���X�̐���()
        Dim dsDataSet As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	code AS code  ")
            .AppendLine("	,(code + ':' + ISNULL(meisyou,'')) AS meisyou ")
            .AppendLine("FROM ")
            .AppendLine("	m_kakutyou_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	meisyou_syubetu = @meisyou_syubetu ")
            If Not strCd.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	code = @code ")
            End If
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, 56))
        paramList.Add(MakeParam("@code", SqlDbType.VarChar, 10, strCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtTorikesi", paramList.ToArray)

        Return dsDataSet.Tables("dtTorikesi")

    End Function

End Class
