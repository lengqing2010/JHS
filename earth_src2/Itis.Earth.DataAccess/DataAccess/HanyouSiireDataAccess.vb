Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports System.Text

''' <summary>
''' �ėp�d���f�[�^�捞���DataAccess
''' </summary>
''' <remarks>2012/01/11 �� �V�K�쐬</remarks>
Public Class HanyouSiireDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

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
            .AppendLine("	CONVERT(varchar(100),syori_datetime,111) + ' ' + CONVERT(varchar(100),syori_datetime,108) AS syori_datetime ")              '��������
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
            .AppendLine("	file_kbn = 7 ")         '�t�@�C���敪
            .AppendLine(" ORDER BY ")
            .AppendLine("	syori_datetime DESC ")  '��������(�~��)
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri")

        Return dsHanbaiKakaku.Tables("dtUploadKanri")

    End Function
    Public Function SelSesyuMei(ByVal kbn As String, ByVal strHosyousyoNo As String) As String

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("      sesyu_mei ")
            .AppendLine(" FROM ")
            .AppendLine("      t_jiban WITH(READCOMMITTED) ")
            .AppendLine(" WHERE  ")
            .AppendLine("  kbn = @kbn ")
            .AppendLine(" AND hosyousyo_no = @hosyousyo_no ")
        End With
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, kbn))
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strHosyousyoNo))
        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtJiban", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count = 0 Then
            Return ""
        Else
            Return dsReturn.Tables(0).Rows(0).Item(0).ToString
        End If


    End Function
    Public Function SelLoginMei(ByVal strUserId As String) As String

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")

            .AppendLine("      DisplayName AS cd ")
            .AppendLine(" FROM ")
            .AppendLine("      m_jhs_mailbox WITH(READCOMMITTED) ")
            .AppendLine(" WHERE  ")
            .AppendLine("  PrimaryWindowsNTAccount = @PrimaryWindowsNTAccount ")

        End With
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@PrimaryWindowsNTAccount", SqlDbType.VarChar, 64, strUserId))
        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKameiten", paramList.ToArray)

        '�߂�l
        Return dsReturn.Tables(0).Rows(0).Item(0).ToString

    End Function
    ''' <summary>������Ж����擾����</summary>
    ''' <returns>������Ж��f�[�^�e�[�u��</returns>
    ''' <history>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKameitenInput(ByVal strCd As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")

            .AppendLine("      kameiten_cd AS cd ")
            .AppendLine(" FROM ")
            .AppendLine("      m_kameiten WITH(READCOMMITTED) ")
            .AppendLine(" WHERE  ")
            .AppendLine("  kameiten_cd = @kameiten_cd ")

        End With
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strCd))
        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKameiten", paramList.ToArray)

        '�߂�l
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

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
            .AppendLine("	file_kbn =7 ")                          '�t�@�C���敪
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri")

        Return CInt(dsHanbaiKakaku.Tables("dtUploadKanri").Rows(0).Item("count").ToString)

    End Function
    ''' <summary>�A�b�v���[�h�Ǘ��̌������擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��̌���</returns>
    Public Function SelUploadKanri(ByVal strKbn As String, ByVal strFileName As String) As Integer

        'DataSet�C���X�^���X�̐���
        Dim dsHanbaiKakaku As New Data.DataSet
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("	COUNT(*) AS count ")                    '����
            .AppendLine(" FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")   '�A�b�v���[�h�Ǘ��e�[�u��
            .AppendLine(" WHERE ")
            .AppendLine("	file_kbn =@file_kbn AND error_umu=0 AND nyuuryoku_file_mei=@nyuuryoku_file_mei ")                          '�t�@�C���敪
        End With
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@file_kbn", SqlDbType.VarChar, 1, strKbn))
        paramList.Add(MakeParam("@nyuuryoku_file_mei", SqlDbType.VarChar, 128, strFileName))
        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri", paramList.ToArray)

        Return CInt(dsHanbaiKakaku.Tables("dtUploadKanri").Rows(0).Item("count").ToString)

    End Function
    ''' <summary>�ėp�d���}�X�^���e�[�u����o�^����</summary>
    ''' <param name="dtHanyouSiireOk">�}�X�^�f�[�^���</param>
    ''' <returns>�������s�敪</returns>
    Public Function InsHanyouSiire(ByVal dtHanyouSiireOk As Data.DataTable, _
                                   ByVal strUserId As String) As Boolean

        '�߂�l
        Dim InsCount As Integer = 0
        Dim strLoginMei As String = SelLoginMei(strUserId)
        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" INSERT INTO  ")
            .AppendLine("   t_hannyou_siire WITH(UPDLOCK) ")
            .AppendLine("   ( ")
            .AppendLine("		torikesi ")                            '���
            .AppendLine("		,tekiyou ")                             '�E�v
            .AppendLine("		,siire_date ")                          '�d���N����
            .AppendLine("		,denpyou_siire_date ")                  '�`�[�d���N����
            .AppendLine("		,tys_kaisya_cd ")                       '������ЃR�[�h
            .AppendLine("		,tys_kaisya_jigyousyo_cd ")             '������Ў��Ə��R�[�h
            .AppendLine("		,kameiten_cd ")                         '�����X�R�[�h
            .AppendLine("		,syouhin_cd ")                          '���i�R�[�h
            .AppendLine("		,suu ")                                 '����
            .AppendLine("		,tanka ")                               '�P��
            .AppendLine("		,zei_kbn ")                             '�ŋ敪
            .AppendLine("		,syouhizei_gaku ")                      '����Ŋz
            .AppendLine("		,siire_keijyou_flg ")                   '�d������FLG	
            .AppendLine("		,siire_keijyou_date ")                  '�d��������
            .AppendLine("		,kbn ")                                 '�敪	
            .AppendLine("		,bangou ")                              '�ԍ�	
            .AppendLine("		,sesyu_mei ")                           '�{�喼
            .AppendLine("		,add_login_user_id ")                   '�o�^���O�C�����[�UID
            .AppendLine("		,add_login_user_name ")                 '�o�^���O�C�����[�U��
            .AppendLine("		,add_datetime ")                        '�o�^����
            .AppendLine("		,upd_login_user_id ")                   '�X�V���O�C�����[�UID
            .AppendLine("		,upd_login_user_name ")                 '�X�V���O�C�����[�U��
            .AppendLine("		,upd_datetime ")                        '�X�V����
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@torikesi ")
            .AppendLine("	,@tekiyou ")
            .AppendLine("	,@siire_date ")
            .AppendLine("	,@denpyou_siire_date ")
            .AppendLine("	,@tys_kaisya_cd ")
            .AppendLine("	,@tys_kaisya_jigyousyo_cd ")
            .AppendLine("	,@kameiten_cd ")
            .AppendLine("	,@syouhin_cd ")
            .AppendLine("	,@suu ")
            .AppendLine("	,@tanka ")
            .AppendLine("	,@zei_kbn ")
            .AppendLine("	,@syouhizei_gaku ")
            .AppendLine("	,@siire_keijyou_flg ")
            .AppendLine("	,@siire_keijyou_date ")
            .AppendLine("	,@kbn ")
            .AppendLine("	,@bangou ")
            .AppendLine("	,@sesyu_mei ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,@add_login_user_name ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(") ")
        End With

 
        For i As Integer = 0 To dtHanyouSiireOk.Rows.Count - 1

            '�p�����[�^�̐ݒ�
            paramList.Clear()
            If dtHanyouSiireOk.Rows(i).Item("torikesi").ToString.Trim = "" Then
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
            Else
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, dtHanyouSiireOk.Rows(i).Item("torikesi").ToString.Trim))
            End If

            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtHanyouSiireOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, InsObj(dtHanyouSiireOk.Rows(i).Item("tys_kaisya_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_kaisya_jigyousyo_cd", SqlDbType.VarChar, 2, InsObj(dtHanyouSiireOk.Rows(i).Item("tys_kaisya_jigyousyo_cd").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtHanyouSiireOk.Rows(i).Item("syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@suu", SqlDbType.Int, 10, InsObj(dtHanyouSiireOk.Rows(i).Item("suu").ToString.Trim)))
            paramList.Add(MakeParam("@tanka", SqlDbType.Int, 10, InsObj(dtHanyouSiireOk.Rows(i).Item("tanka").ToString.Trim)))
            paramList.Add(MakeParam("@zei_kbn", SqlDbType.VarChar, 1, InsObj(dtHanyouSiireOk.Rows(i).Item("zei_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@syouhizei_gaku", SqlDbType.Int, 10, InsObj(dtHanyouSiireOk.Rows(i).Item("syouhizei_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@siire_date", SqlDbType.DateTime, 30, InsObj(dtHanyouSiireOk.Rows(i).Item("siire_date").ToString.Trim)))
            paramList.Add(MakeParam("@denpyou_siire_date", SqlDbType.DateTime, 30, InsObj(dtHanyouSiireOk.Rows(i).Item("denpyou_siire_date").ToString.Trim)))
            paramList.Add(MakeParam("@siire_keijyou_flg", SqlDbType.Int, 10, InsObj(dtHanyouSiireOk.Rows(i).Item("siire_keijyou_flg").ToString.Trim)))
            paramList.Add(MakeParam("@siire_keijyou_date", SqlDbType.DateTime, 30, InsObj(dtHanyouSiireOk.Rows(i).Item("siire_keijyou_date").ToString.Trim)))
            paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, InsObj(dtHanyouSiireOk.Rows(i).Item("kbn").ToString.Trim)))
            paramList.Add(MakeParam("@bangou", SqlDbType.VarChar, 10, InsObj(dtHanyouSiireOk.Rows(i).Item("bangou").ToString.Trim)))
            paramList.Add(MakeParam("@sesyu_mei", SqlDbType.VarChar, 50, InsObj(dtHanyouSiireOk.Rows(i).Item("sesyu_mei").ToString.Trim)))
            paramList.Add(MakeParam("@tekiyou", SqlDbType.VarChar, 512, InsObj(dtHanyouSiireOk.Rows(i).Item("tekiyou").ToString.Trim)))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
            paramList.Add(MakeParam("@add_login_user_name", SqlDbType.VarChar, 128, strLoginMei))
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


    ''' <summary>�ėp�d���G���[���e�[�u����o�^����</summary>
    ''' <param name="dtHanyouSiireErr">�G���[�f�[�^���</param>
    ''' <returns>�������s�敪</returns>
    Public Function InsHanyouSiireErr(ByVal dtHanyouSiireErr As Data.DataTable, _
                                      ByVal strUploadDate As String, _
                                      ByVal strUserId As String) As Boolean

        '�߂�l
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder
        Dim strLoginMei As String = SelLoginMei(strUserId)
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" INSERT INTO  ")
            .AppendLine("   t_hannyou_siire_error WITH(UPDLOCK) ")
            .AppendLine("   ( ")
            .AppendLine("		edi_jouhou_sakusei_date ")              'EDI���쐬��
            .AppendLine("		,gyou_no ")                             '�sNO
            .AppendLine("		,syori_datetime ")                      '��������
            .AppendLine("		,torikesi ")                            '���
            .AppendLine("		,kameiten_cd ")                         '�����X�R�[�h
            .AppendLine("		,tys_kaisya_cd ")                       '������ЃR�[�h
            .AppendLine("		,tys_kaisya_jigyousyo_cd ")             '������Ў��Ə��R�[�h
            .AppendLine("		,syouhin_cd ")                          '���i�R�[�h
            .AppendLine("		,suu ")                                 '����
            .AppendLine("		,tanka ")                               '�P��
            .AppendLine("		,zei_kbn ")                             '�ŋ敪	
            .AppendLine("		,syouhizei_gaku ")                      '����Ŋz
            .AppendLine("		,siire_date ")                          '�d���N����	
            .AppendLine("		,denpyou_siire_date ")                  '�`�[�d���N����	
            .AppendLine("		,siire_keijyou_flg ")                   '�d������FLG	
            .AppendLine("		,siire_keijyou_date ")                  '�d��������	
            .AppendLine("		,kbn ")                                 '�敪	
            .AppendLine("		,bangou ")                              '�ԍ�	
            .AppendLine("		,sesyu_mei ")                           '�{�喼	
            .AppendLine("		,tekiyou ")                             '�E�v	
            .AppendLine("		,add_login_user_id ")                   '�o�^���O�C�����[�UID
            .AppendLine("		,add_login_user_name ")                 '�o�^���O�C�����[�U��
            .AppendLine("		,add_datetime ")                        '�o�^����
            .AppendLine("		,upd_login_user_id ")                   '�X�V���O�C�����[�UID
            .AppendLine("		,upd_login_user_name ")                 '�X�V���O�C�����[�U��
            .AppendLine("		,upd_datetime ")                        '�X�V����
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@edi_jouhou_sakusei_date ")
            .AppendLine("	,@gyou_no ")
            .AppendLine("	,@syori_datetime ")
            .AppendLine("	,@torikesi ")
            .AppendLine("	,@kameiten_cd ")
            .AppendLine("	,@tys_kaisya_cd ")
            .AppendLine("	,@tys_kaisya_jigyousyo_cd ")
            .AppendLine("	,@syouhin_cd ")
            .AppendLine("	,@suu ")
            .AppendLine("	,@tanka ")
            .AppendLine("	,@zei_kbn ")
            .AppendLine("	,@syouhizei_gaku ")
            .AppendLine("	,@siire_date ")
            .AppendLine("	,@denpyou_siire_date ")
            .AppendLine("	,@siire_keijyou_flg ")
            .AppendLine("	,@siire_keijyou_date")
            .AppendLine("	,@kbn ")
            .AppendLine("	,@bangou ")
            .AppendLine("	,@sesyu_mei ")
            .AppendLine("	,@tekiyou ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,@add_login_user_name ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(") ")
        End With

        For i As Integer = 0 To dtHanyouSiireErr.Rows.Count - 1

            '�p�����[�^�̐ݒ�
            paramList.Clear()
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, dtHanyouSiireErr.Rows(i).Item("edi_jouhou_sakusei_date").ToString.Trim))
            paramList.Add(MakeParam("@gyou_no", SqlDbType.VarChar, 10, dtHanyouSiireErr.Rows(i).Item("gyou_no").ToString.Trim))
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 30, strUploadDate))
         
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 10, InsObj(dtHanyouSiireErr.Rows(i).Item("torikesi").ToString.Trim)))


            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtHanyouSiireErr.Rows(i).Item("kameiten_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, InsObj(dtHanyouSiireErr.Rows(i).Item("tys_kaisya_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_kaisya_jigyousyo_cd", SqlDbType.VarChar, 2, InsObj(dtHanyouSiireErr.Rows(i).Item("tys_kaisya_jigyousyo_cd").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtHanyouSiireErr.Rows(i).Item("syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@suu", SqlDbType.VarChar, 10, InsObj(dtHanyouSiireErr.Rows(i).Item("suu").ToString.Trim)))
            paramList.Add(MakeParam("@tanka", SqlDbType.VarChar, 10, InsObj(dtHanyouSiireErr.Rows(i).Item("tanka").ToString.Trim)))
            paramList.Add(MakeParam("@zei_kbn", SqlDbType.VarChar, 1, InsObj(dtHanyouSiireErr.Rows(i).Item("zei_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@syouhizei_gaku", SqlDbType.VarChar, 10, InsObj(dtHanyouSiireErr.Rows(i).Item("syouhizei_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@siire_date", SqlDbType.VarChar, 30, InsObj(dtHanyouSiireErr.Rows(i).Item("siire_date").ToString.Trim)))
            paramList.Add(MakeParam("@denpyou_siire_date", SqlDbType.VarChar, 30, InsObj(dtHanyouSiireErr.Rows(i).Item("denpyou_siire_date").ToString.Trim)))
            paramList.Add(MakeParam("@siire_keijyou_flg", SqlDbType.VarChar, 10, InsObj(dtHanyouSiireErr.Rows(i).Item("siire_keijyou_flg").ToString.Trim)))
            paramList.Add(MakeParam("@siire_keijyou_date", SqlDbType.VarChar, 30, InsObj(dtHanyouSiireErr.Rows(i).Item("siire_keijyou_date").ToString.Trim)))
            paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, InsObj(dtHanyouSiireErr.Rows(i).Item("kbn").ToString.Trim)))
            paramList.Add(MakeParam("@bangou", SqlDbType.VarChar, 10, InsObj(dtHanyouSiireErr.Rows(i).Item("bangou").ToString.Trim)))
            paramList.Add(MakeParam("@sesyu_mei", SqlDbType.VarChar, 50, InsObj(dtHanyouSiireErr.Rows(i).Item("sesyu_mei").ToString.Trim)))
            paramList.Add(MakeParam("@tekiyou", SqlDbType.VarChar, 512, InsObj(dtHanyouSiireErr.Rows(i).Item("tekiyou").ToString.Trim)))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
            paramList.Add(MakeParam("@add_login_user_name", SqlDbType.VarChar, 128, strLoginMei))
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
    Function InsObj(ByVal str As String) As Object
        If str = "" Then
            Return DBNull.Value
        Else
            Return str
        End If
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
            .AppendLine("	,7 ")
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

    ''' <summary>�ėp�d���G���[�����擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <param name="intTopCount">�擾�̍ő�s��</param>
    ''' <returns>�ėp�d���G���[�f�[�^�e�[�u��</returns>
    Public Function SelHanyouSiireErr(ByVal strEdiJouhouSakuseiDate As String, _
                                      ByVal strSyoridate As String, _
                                      ByVal intTopCount As Integer) As DataTable
        'DataSet�C���X�^���X�̐���
        Dim dsHanyouSiireErr As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP " & intTopCount & "")
            .AppendLine("   edi_jouhou_sakusei_date AS edi_jouhou_sakusei_date ")
            .AppendLine("   ,gyou_no AS gyou_no ")
            .AppendLine("   ,syori_datetime AS syori_datetime ")
            .AppendLine("   ,torikesi AS torikesi  ")
            .AppendLine("   ,kameiten_cd AS kameiten_cd ")
            .AppendLine("   ,tys_kaisya_cd AS tys_kaisya_cd ")
            .AppendLine("   ,tys_kaisya_jigyousyo_cd AS tys_kaisya_jigyousyo_cd ")
            .AppendLine("   ,syouhin_cd AS syouhin_cd ")
            .AppendLine("   ,suu AS suu ")
            .AppendLine("   ,tanka AS tanka ")
            .AppendLine("   ,zei_kbn AS zei_kbn ")
            .AppendLine("   ,syouhizei_gaku AS syouhizei_gaku ")
            .AppendLine("   ,siire_date AS siire_date ")
            .AppendLine("   ,denpyou_siire_date AS denpyou_siire_date ")
            .AppendLine("   ,'' AS siire_keijyou_flg ")
            .AppendLine("   ,'' AS siire_keijyou_date ")
            .AppendLine("   ,kbn AS kbn ")
            .AppendLine("   ,bangou AS bangou ")
            .AppendLine("   ,sesyu_mei AS sesyu_mei ")
            .AppendLine("   ,tekiyou AS tekiyou ")
            .AppendLine("   ,add_login_user_id AS add_login_user_id ")
            .AppendLine("   ,add_login_user_name AS add_login_user_name ")
            .AppendLine("   ,add_datetime AS add_datetime ")
            .AppendLine("   ,upd_login_user_id AS upd_login_user_id ")
            .AppendLine("   ,upd_login_user_name AS upd_login_user_name ")
            .AppendLine("   ,upd_datetime AS upd_datetime ")
            .AppendLine(" FROM ")
            .AppendLine("   t_hannyou_siire_error AS THSE WITH(READCOMMITTED) ")
            .AppendLine(" WHERE ")
            .AppendLine("    ")

            'EDI���쐬��
            .AppendLine(" THSE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate.Trim))

            '��������
            .AppendLine(" AND CONVERT(varchar(100),THSE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),THSE.syori_datetime,114),':','') = @syori_datetime")

            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))

            '�\�[�g��
            .AppendLine(" ORDER BY ")
            .AppendLine("      THSE.edi_jouhou_sakusei_date, ")
            .AppendLine("      THSE.gyou_no ")
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanyouSiireErr, _
                    "dsHanyouSiireErr", paramList.ToArray)

        Return dsHanyouSiireErr.Tables("dsHanyouSiireErr")

    End Function

    ''' <summary>�ėp�d���G���[�������擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�ėp�d���G���[����</returns>
    Public Function SelHanyouSiireErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As Integer

        'DataSet�C���X�^���X�̐���
        Dim dsHanyouSiireErr As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   Count(edi_jouhou_sakusei_date) AS count ")
            .AppendLine(" FROM ")
            .AppendLine("   t_hannyou_siire_error WITH(READCOMMITTED) ")   '�ėp�d���G���[���T
            .AppendLine(" WHERE ")

            'EDI���
            .AppendLine(" edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate.Trim))

            '��������
            .AppendLine(" AND CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime")
            '.AppendLine(" AND syori_datetime = @syori_datetime")
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoriDate))

        End With
        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanyouSiireErr, _
                    "dsHanyouSiireErr", paramList.ToArray)

        Return dsHanyouSiireErr.Tables("dsHanyouSiireErr").Rows(0).Item("count")



    End Function

End Class
