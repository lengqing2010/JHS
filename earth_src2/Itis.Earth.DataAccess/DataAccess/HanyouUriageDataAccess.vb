Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports System.Text

''' <summary>
''' �ėp����f�[�^�捞���DataAccess
''' </summary>
''' <remarks>2012/01/12 �� �V�K�쐬</remarks>
Public Class HanyouUriageDataAccess

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
            .AppendLine("	file_kbn = 6 ")         '�t�@�C���敪
            .AppendLine(" ORDER BY ")
            .AppendLine("	syori_datetime DESC ")  '��������(�~��)
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri")

        Return dsHanbaiKakaku.Tables("dtUploadKanri")

    End Function
    ''' <summary>���i�R�[�h���擾����</summary>
    ''' <history>2011/03/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelSyouhinCdInput(ByVal strSyouhinCd As String) As DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	syouhin_mei ")
            .AppendLine("FROM  ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
        End With

        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '�߂�l
        Return dsReturn.Tables(0)
   

    End Function
    ''' <summary>������Ж����擾����</summary>
    ''' <returns>������Ж��f�[�^�e�[�u��</returns>
    ''' <history>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelSeikyuuSakiInput(ByVal strCd As String, ByVal strBrc As String, ByVal strKbn As String) As Boolean

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")

            .AppendLine("      seikyuu_saki_cd AS cd ")
            .AppendLine(" FROM ")
            .AppendLine("      m_seikyuu_saki WITH(READCOMMITTED) ")
            .AppendLine(" WHERE  ")
            .AppendLine("  seikyuu_saki_cd = @seikyuu_saki_cd ")
            .AppendLine(" AND seikyuu_saki_brc = @seikyuu_saki_brc ")
            .AppendLine(" AND seikyuu_saki_kbn = @seikyuu_saki_kbn ")
        End With
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, strKbn))
        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSeikyuuSaki", paramList.ToArray)

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
            .AppendLine("	file_kbn =6 ")                          '�t�@�C���敪
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri")

        Return CInt(dsHanbaiKakaku.Tables("dtUploadKanri").Rows(0).Item("count").ToString)

    End Function

    ''' <summary>�ėp����}�X�^���e�[�u����o�^����</summary>
    ''' <param name="dtHanyouUriageOk">�}�X�^�f�[�^���</param>
    ''' <returns>�������s�敪</returns>
    Public Function InsHanyouUriage(ByVal dtHanyouUriageOk As Data.DataTable, _
                                    ByVal strUserId As String) As Boolean

        '�߂�l
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder
        Dim HanyouSiireDataAccess As New HanyouSiireDataAccess
        Dim strLoginMei As String = HanyouSiireDataAccess.SelLoginMei(strUserId)
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" INSERT INTO  ")
            .AppendLine("   t_hannyou_uriage WITH(UPDLOCK) ")
            .AppendLine("   ( ")
            .AppendLine("		torikesi ")                            '���
            .AppendLine("		,tekiyou ")                             '�E�v
            .AppendLine("		,uri_date ")                            '����N����
            .AppendLine("		,denpyou_uri_date ")                    '�`�[����N����
            .AppendLine("		,seikyuu_date ")                        '�����N����
            .AppendLine("		,seikyuu_saki_cd ")                     '������R�[�h
            .AppendLine("		,seikyuu_saki_brc ")                    '������}��
            .AppendLine("		,seikyuu_saki_kbn ")                    '������敪
            .AppendLine("		,syouhin_cd ")                          '���i�R�[�h
            .AppendLine("		,hin_mei ")                             '�i��
            .AppendLine("		,suu ")                                 '����
            .AppendLine("		,tanka ")                               '�P��
            .AppendLine("		,syanai_genka ")                        '�Г�����
            .AppendLine("		,zei_kbn ")                             '�ŋ敪
            .AppendLine("		,syouhizei_gaku ")                      '����Ŋz
            .AppendLine("		,uri_keijyou_flg ")                     '���㏈��FLG(����v��FLG)	
            .AppendLine("		,uri_keijyou_date ")                    '���㏈����(����v���)
            .AppendLine("		,kbn ")                                 '�敪	
            .AppendLine("		,bangou ")                              '�ԍ�	
            .AppendLine("		,sesyu_mei ")                           '�{�喼
            '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
            .AppendLine("		,uriage_ten_kbn ")                      '����X�敪
            .AppendLine("		,uriage_ten_cd ")                       '����X����
            '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
            .AppendLine("		,add_login_user_id ")                   '�o�^���O�C�����[�UID
            .AppendLine("		,add_login_user_name ")                 '�o�^���O�C�����[�U��
            .AppendLine("		,add_datetime ")                        '�o�^����
            .AppendLine("		,upd_login_user_id ")                   '�X�V���O�C�����[�UID
            .AppendLine("		,upd_login_user_name ")                 '�X�V���O�C�����[�U��
            .AppendLine("		,upd_datetime ")                        '�X�V����
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("		@torikesi ")
            .AppendLine("		,@tekiyou ")
            .AppendLine("		,@uri_date ")
            .AppendLine("		,@denpyou_uri_date ")
            .AppendLine("		,@seikyuu_date ")
            .AppendLine("		,@seikyuu_saki_cd ")
            .AppendLine("		,@seikyuu_saki_brc ")
            .AppendLine("		,@seikyuu_saki_kbn ")
            .AppendLine("		,@syouhin_cd ")
            .AppendLine("		,@hin_mei ")
            .AppendLine("		,@suu ")
            .AppendLine("		,@tanka ")
            .AppendLine("		,@syanai_genka ")
            .AppendLine("		,@zei_kbn ")
            .AppendLine("		,@syouhizei_gaku ")
            .AppendLine("		,@uri_keijyou_flg ")
            .AppendLine("		,@uri_keijyou_date ")
            .AppendLine("		,@kbn ")
            .AppendLine("		,@bangou ")
            .AppendLine("		,@sesyu_mei ")
            '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
            .AppendLine("		,@uriage_ten_kbn ")
            .AppendLine("		,@uriage_ten_cd ")
            '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
            .AppendLine("		,@add_login_user_id ")
            .AppendLine("	,@add_login_user_name ")
            .AppendLine("		,GETDATE() ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine(") ")
        End With


        For i As Integer = 0 To dtHanyouUriageOk.Rows.Count - 1

            '�p�����[�^�̐ݒ�
            paramList.Clear()
            If dtHanyouUriageOk.Rows(i).Item("torikesi").ToString.Trim = "" Then
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
            Else
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, dtHanyouUriageOk.Rows(i).Item("torikesi").ToString.Trim))
            End If

            paramList.Add(MakeParam("@tekiyou", SqlDbType.VarChar, 512, InsObj(dtHanyouUriageOk.Rows(i).Item("tekiyou").ToString.Trim)))
            paramList.Add(MakeParam("@uri_date", SqlDbType.DateTime, 30, InsObj(dtHanyouUriageOk.Rows(i).Item("uri_date").ToString.Trim)))
            paramList.Add(MakeParam("@denpyou_uri_date", SqlDbType.DateTime, 30, InsObj(dtHanyouUriageOk.Rows(i).Item("denpyou_uri_date").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_date", SqlDbType.DateTime, 30, InsObj(dtHanyouUriageOk.Rows(i).Item("seikyuu_date").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 40, InsObj(dtHanyouUriageOk.Rows(i).Item("seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 5, InsObj(dtHanyouUriageOk.Rows(i).Item("seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.Char, 5, InsObj(dtHanyouUriageOk.Rows(i).Item("seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtHanyouUriageOk.Rows(i).Item("syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@hin_mei", SqlDbType.VarChar, 40, InsObj(dtHanyouUriageOk.Rows(i).Item("hin_mei").ToString.Trim)))
            paramList.Add(MakeParam("@suu", SqlDbType.Int, 10, InsObj(dtHanyouUriageOk.Rows(i).Item("suu").ToString.Trim)))
            paramList.Add(MakeParam("@tanka", SqlDbType.Int, 10, InsObj(dtHanyouUriageOk.Rows(i).Item("tanka").ToString.Trim)))
            paramList.Add(MakeParam("@syanai_genka", SqlDbType.Int, 10, InsObj(dtHanyouUriageOk.Rows(i).Item("syanai_genka").ToString.Trim)))
            paramList.Add(MakeParam("@zei_kbn", SqlDbType.VarChar, 1, InsObj(dtHanyouUriageOk.Rows(i).Item("zei_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@syouhizei_gaku", SqlDbType.Int, 10, InsObj(dtHanyouUriageOk.Rows(i).Item("syouhizei_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@uri_keijyou_flg", SqlDbType.Int, 10, InsObj(dtHanyouUriageOk.Rows(i).Item("uri_keijyou_flg").ToString.Trim)))
            paramList.Add(MakeParam("@uri_keijyou_date", SqlDbType.DateTime, 30, InsObj(dtHanyouUriageOk.Rows(i).Item("uri_keijyou_date").ToString.Trim)))
            paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, InsObj(dtHanyouUriageOk.Rows(i).Item("kbn").ToString.Trim)))
            paramList.Add(MakeParam("@bangou", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageOk.Rows(i).Item("bangou").ToString.Trim)))
            paramList.Add(MakeParam("@sesyu_mei", SqlDbType.VarChar, 50, InsObj(dtHanyouUriageOk.Rows(i).Item("sesyu_mei").ToString.Trim)))
            '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
            paramList.Add(MakeParam("@uriage_ten_kbn", SqlDbType.Char, 1, InsObj(dtHanyouUriageOk.Rows(i).Item("uriage_ten_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@uriage_ten_cd", SqlDbType.VarChar, 7, InsObj(dtHanyouUriageOk.Rows(i).Item("uriage_ten_cd").ToString.Trim)))
            '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
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


    ''' <summary>�ėp����G���[���e�[�u����o�^����</summary>
    ''' <param name="dtHanyouUriageErr">�G���[�f�[�^���</param>
    ''' <returns>�������s�敪</returns>
    Public Function InsHanyouUriageErr(ByVal dtHanyouUriageErr As Data.DataTable, _
                                       ByVal strUploadDate As String, _
                                       ByVal strUserId As String) As Boolean

        '�߂�l
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder
        Dim HanyouSiireDataAccess As New HanyouSiireDataAccess
        Dim strLoginMei As String = HanyouSiireDataAccess.SelLoginMei(strUserId)
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" INSERT INTO  ")
            .AppendLine("   t_hannyou_uriage_error WITH(UPDLOCK) ")
            .AppendLine("   ( ")
            .AppendLine("		edi_jouhou_sakusei_date ")              'EDI���쐬��
            .AppendLine("		,gyou_no ")                             '�sNO
            .AppendLine("		,syori_datetime ")                      '��������
            .AppendLine("		,torikesi ")                            '���
            .AppendLine("		,seikyuu_saki_kbn ")                    '������敪
            .AppendLine("		,seikyuu_saki_cd ")                     '������R�[�h
            .AppendLine("		,seikyuu_saki_brc ")                    '������}��
            .AppendLine("		,syouhin_cd ")                          '���i�R�[�h
            .AppendLine("		,hin_mei ")                             '�i��
            .AppendLine("		,suu ")                                 '����
            .AppendLine("		,tanka ")                               '�P��
            .AppendLine("		,syanai_genka ")                        '�Г�����
            .AppendLine("		,zei_kbn ")                             '�ŋ敪
            .AppendLine("		,syouhizei_gaku ")                      '����Ŋz
            .AppendLine("		,uri_date ")                            '����N����
            .AppendLine("		,seikyuu_date ")                        '�����N����
            .AppendLine("		,denpyou_uri_date ")                    '�`�[����N����
            .AppendLine("		,uri_keijyou_flg ")                     '���㏈��FLG(����v��FLG)	
            .AppendLine("		,uri_keijyou_date ")                    '���㏈����(����v���)
            .AppendLine("		,kbn ")                                 '�敪	
            .AppendLine("		,bangou ")                              '�ԍ�	
            .AppendLine("		,sesyu_mei ")                           '�{�喼
            '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
            .AppendLine("		,uriage_ten_kbn ")                      '����X�敪
            .AppendLine("		,uriage_ten_cd ")                       '����X����
            '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
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
            .AppendLine("		@edi_jouhou_sakusei_date ")
            .AppendLine("		,@gyou_no ")
            .AppendLine("		,@syori_datetime ")
            .AppendLine("		,@torikesi ")
            .AppendLine("		,@seikyuu_saki_kbn ")
            .AppendLine("		,@seikyuu_saki_cd ")
            .AppendLine("		,@seikyuu_saki_brc ")
            .AppendLine("		,@syouhin_cd ")
            .AppendLine("		,@hin_mei ")
            .AppendLine("		,@suu ")
            .AppendLine("		,@tanka ")
            .AppendLine("		,@syanai_genka ")
            .AppendLine("		,@zei_kbn ")
            .AppendLine("		,@syouhizei_gaku ")
            .AppendLine("		,@uri_date ")
            .AppendLine("		,@seikyuu_date ")
            .AppendLine("		,@denpyou_uri_date ")
            .AppendLine("		,@uri_keijyou_flg ")
            .AppendLine("		,@uri_keijyou_date")
            .AppendLine("		,@kbn ")
            .AppendLine("		,@bangou ")
            .AppendLine("		,@sesyu_mei ")
            '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
            .AppendLine("		,@uriage_ten_kbn ")
            .AppendLine("		,@uriage_ten_cd ")
            '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
            .AppendLine("		,@tekiyou ")
            .AppendLine("		,@add_login_user_id ")
            .AppendLine("	,@add_login_user_name ")
            .AppendLine("		,GETDATE() ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine(") ")
        End With

        For i As Integer = 0 To dtHanyouUriageErr.Rows.Count - 1
            '�p�����[�^�̐ݒ�
            paramList.Clear()
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, dtHanyouUriageErr.Rows(i).Item("edi_jouhou_sakusei_date").ToString.Trim))
            paramList.Add(MakeParam("@gyou_no", SqlDbType.VarChar, 10, dtHanyouUriageErr.Rows(i).Item("gyou_no").ToString.Trim))
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 30, strUploadDate))

            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageErr.Rows(i).Item("torikesi").ToString.Trim)))
            paramList.Add(MakeParam("@tekiyou", SqlDbType.VarChar, 512, InsObj(dtHanyouUriageErr.Rows(i).Item("tekiyou").ToString.Trim)))
            paramList.Add(MakeParam("@uri_date", SqlDbType.VarChar, 30, InsObj(dtHanyouUriageErr.Rows(i).Item("uri_date").ToString.Trim)))
            paramList.Add(MakeParam("@denpyou_uri_date", SqlDbType.VarChar, 30, InsObj(dtHanyouUriageErr.Rows(i).Item("denpyou_uri_date").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_date", SqlDbType.VarChar, 30, InsObj(dtHanyouUriageErr.Rows(i).Item("seikyuu_date").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 40, InsObj(dtHanyouUriageErr.Rows(i).Item("seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 5, InsObj(dtHanyouUriageErr.Rows(i).Item("seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.Char, 5, InsObj(dtHanyouUriageErr.Rows(i).Item("seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtHanyouUriageErr.Rows(i).Item("syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@hin_mei", SqlDbType.VarChar, 40, InsObj(dtHanyouUriageErr.Rows(i).Item("hin_mei").ToString.Trim)))
            paramList.Add(MakeParam("@suu", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageErr.Rows(i).Item("suu").ToString.Trim)))
            paramList.Add(MakeParam("@tanka", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageErr.Rows(i).Item("tanka").ToString.Trim)))
            paramList.Add(MakeParam("@syanai_genka", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageErr.Rows(i).Item("syanai_genka").ToString.Trim)))
            paramList.Add(MakeParam("@zei_kbn", SqlDbType.VarChar, 1, InsObj(dtHanyouUriageErr.Rows(i).Item("zei_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@syouhizei_gaku", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageErr.Rows(i).Item("syouhizei_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@uri_keijyou_flg", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageErr.Rows(i).Item("uri_keijyou_flg").ToString.Trim)))
            paramList.Add(MakeParam("@uri_keijyou_date", SqlDbType.VarChar, 30, InsObj(dtHanyouUriageErr.Rows(i).Item("uri_keijyou_date").ToString.Trim)))
            paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, InsObj(dtHanyouUriageErr.Rows(i).Item("kbn").ToString.Trim)))
            paramList.Add(MakeParam("@bangou", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageErr.Rows(i).Item("bangou").ToString.Trim)))
            paramList.Add(MakeParam("@sesyu_mei", SqlDbType.VarChar, 50, InsObj(dtHanyouUriageErr.Rows(i).Item("sesyu_mei").ToString.Trim)))
            '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
            paramList.Add(MakeParam("@uriage_ten_kbn", SqlDbType.Char, 1, InsObj(dtHanyouUriageErr.Rows(i).Item("uriage_ten_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@uriage_ten_cd", SqlDbType.VarChar, 7, InsObj(dtHanyouUriageErr.Rows(i).Item("uriage_ten_cd").ToString.Trim)))
            '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
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
            .AppendLine("	,6 ")
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

    ''' <summary>�ėp����G���[�����擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <param name="intTopCount">�擾�̍ő�s��</param>
    ''' <returns>�ėp����G���[�f�[�^�e�[�u��</returns>
    Public Function SelHanyouUriageErr(ByVal strEdiJouhouSakuseiDate As String, _
                                       ByVal strSyoridate As String, _
                                       ByVal intTopCount As Integer) As DataTable
        'DataSet�C���X�^���X�̐���
        Dim dsHanyouUriageErr As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP " & intTopCount & "")
            .AppendLine("		edi_jouhou_sakusei_date AS edi_jouhou_sakusei_date ")           'EDI���쐬��
            .AppendLine("		,gyou_no AS gyou_no ")                                          '�sNO
            .AppendLine("		,syori_datetime AS syori_datetime ")                            '��������
            .AppendLine("		,torikesi AS torikesi  ")                                        '���
            '==========��2015/11/19 ������ add����X�敪,����X����  �C����========================================
            .AppendLine("		,uriage_ten_kbn AS uriage_ten_kbn  ")                           '����X�敪
            .AppendLine("		,uriage_ten_cd AS uriage_ten_cd  ")                             '����X�R�[�h
            '==========��2015/11/19 ������ add����X�敪,����X����  �C����========================================
            .AppendLine("		,seikyuu_saki_kbn AS seikyuu_saki_kbn ")                        '������敪
            .AppendLine("		,seikyuu_saki_cd AS seikyuu_saki_cd ")                          '������R�[�h
            .AppendLine("		,seikyuu_saki_brc AS seikyuu_saki_brc ")                        '������}��
            .AppendLine("		,syouhin_cd AS syouhin_cd ")                                    '���i�R�[�h
            .AppendLine("		,hin_mei AS hin_mei ")                                          '�i��
            .AppendLine("		,suu AS suu ")                                                  '����
            .AppendLine("		,tanka AS tanka ")                                              '�P��
            .AppendLine("		,syanai_genka AS syanai_genka ")                                '�Г�����
            .AppendLine("		,zei_kbn AS zei_kbn ")                                          '�ŋ敪
            .AppendLine("		,syouhizei_gaku AS syouhizei_gaku ")                            '����Ŋz
            .AppendLine("		,uri_date AS uri_date ")                                        '����N����
            .AppendLine("		,seikyuu_date AS seikyuu_date ")                                '�����N����
            .AppendLine("		,denpyou_uri_date AS denpyou_uri_date ")                        '�`�[����N����
            .AppendLine("		,uri_keijyou_flg AS uri_keijyou_flg ")                          '���㏈��FLG(����v��FLG)	
            .AppendLine("		,uri_keijyou_date AS uri_keijyou_date ")                        '���㏈����(����v���)
            .AppendLine("		,kbn AS kbn ")                                                  '�敪	
            .AppendLine("		,bangou AS bangou ")                                            '�ԍ�	
            .AppendLine("		,sesyu_mei AS sesyu_mei ")                                      '�{�喼
            .AppendLine("		,tekiyou AS tekiyou")                                           '�E�v
            .AppendLine("		,add_login_user_id AS add_login_user_id ")                      '�o�^���O�C�����[�UID
            .AppendLine("		,add_login_user_name AS add_login_user_name ")                  '�o�^���O�C�����[�U��
            .AppendLine("		,add_datetime AS add_datetime ")                                '�o�^����
            .AppendLine("		,upd_login_user_id AS upd_login_user_id ")                      '�X�V���O�C�����[�UID
            .AppendLine("		,upd_login_user_name AS upd_login_user_name ")                  '�X�V���O�C�����[�U��
            .AppendLine("		,upd_datetime AS upd_datetime ")                                '�X�V����
            .AppendLine(" FROM ")
            .AppendLine("   t_hannyou_uriage_error AS THUE WITH(READCOMMITTED) ")
            .AppendLine(" WHERE ")
            .AppendLine("    ")

            'EDI���쐬��
            .AppendLine(" THUE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate.Trim))

            '��������
            .AppendLine(" AND CONVERT(varchar(100),THUE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),THUE.syori_datetime,114),':','') = @syori_datetime")

            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))

            '�\�[�g��
            .AppendLine(" ORDER BY ")
            .AppendLine("      THUE.edi_jouhou_sakusei_date, ")
            .AppendLine("      THUE.gyou_no ")
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanyouUriageErr, _
                    "dsHanyouUriageErr", paramList.ToArray)

        Return dsHanyouUriageErr.Tables("dsHanyouUriageErr")

    End Function

    ''' <summary>�ėp����G���[�������擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�ėp����G���[����</returns>
    Public Function SelHanyouUriageErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As Integer

        'DataSet�C���X�^���X�̐���
        Dim dsHanyouUriageErr As New DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   Count(edi_jouhou_sakusei_date) AS count ")
            .AppendLine(" FROM ")
            .AppendLine("   t_hannyou_uriage_error WITH(READCOMMITTED) ")   '�ėp����G���[���T
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
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanyouUriageErr, _
                    "dsHanyouUriageErr", paramList.ToArray)

        Return dsHanyouUriageErr.Tables("dsHanyouUriageErr").Rows(0).Item("count")

    End Function

End Class
