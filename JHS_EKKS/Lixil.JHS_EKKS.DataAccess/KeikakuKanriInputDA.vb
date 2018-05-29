Option Explicit On
Option Strict On
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' �v��Ǘ� CSV�捞
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKanriInputDA

    ''' <summary>
    ''' �������ʎ擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Public Function SelInputKanri() As Data.DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT TOP 100 ")
            .AppendLine("	CONVERT(varchar(100),syori_datetime,111) + ' ' + CONVERT(varchar(100),syori_datetime,108) AS syori_datetime ") '��������
            .AppendLine("	,CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') AS torikomi_datetime") '�捞����
            .AppendLine("	,nyuuryoku_file_mei ")                      '���̓t�@�C����
            .AppendLine("	,CASE error_umu ")
            .AppendLine("    WHEN '1' THEN '�L��' ")
            .AppendLine("    WHEN '0' THEN '����' ")
            .AppendLine("    ELSE '' ")
            .AppendLine("    END AS error_umu ")                        '�G���[�L��
            .AppendLine("    ,edi_jouhou_sakusei_date ")                'EDI���쐬��
            .AppendLine(" FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")       '�A�b�v���[�h�Ǘ��e�[�u��
            .AppendLine(" WHERE ")
            .AppendLine("	file_kbn = 2 ")                             '�t�@�C���敪(2�F�v��Ǘ�CSV�捞�p)
            .AppendLine(" ORDER BY ")
            .AppendLine("	syori_datetime DESC ")                      '��������(�~��)
        End With

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtInputKanri")

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>
    ''' �S�������ʌ����擾
    ''' </summary>
    ''' <returns>�S�������ʌ���</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Public Function SelInputKanriCount() As String
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(syori_datetime) ")                    '����
            .AppendLine("FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")       '�A�b�v���[�h�Ǘ��e�[�u��
            .AppendLine("WHERE ")
            .AppendLine("	file_kbn = 2 ")                             '�t�@�C���敪
        End With

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtInputKanri")

        Return dsReturn.Tables(0).Rows(0).Item(0).ToString

    End Function

    ''' <summary>
    ''' �v��Ǘ�_�����X�}�X�^��������X���ނ���������
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <param name="strKameitenCd">�����X����</param>
    ''' <returns>��������</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/19 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Public Function SelKameitenCd(ByVal strKeikakuNendo As String, ByVal strKameitenCd As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKameitenCd)

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	kameiten_cd ")                              '�����X����
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten WITH(READCOMMITTED) ")   '�v��Ǘ�_�����XϽ�
            .AppendLine("WHERE ")
            .AppendLine("	 keikaku_nendo = @keikaku_nendo ")
            .AppendLine("AND kameiten_cd = @kameiten_cd ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))   '�v��N�x
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, strKameitenCd))    '�����X����

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKameitenCd", paramList.ToArray)

        If dsReturn.Tables("dtKameitenCd").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    '''' <summary>
    '''' �v��Ǘ��p_���i�}�X�^����v��Ǘ�_���i�R�[�h����������
    '''' </summary>
    '''' <param name="strKeikakuNendo">�v��N�x</param>
    '''' <param name="strKeikakuKanriSyouhinCd">�v��Ǘ�_���i�R�[�h</param>
    '''' <returns>��������</returns>
    '''' <remarks></remarks>
    '''' <history>																
    '''' <para>2012/12/19 P-44979 ���h�m �V�K�쐬 </para>																															
    '''' </history>
    'Public Function SelKeikakuKanriSyouhinCd(ByVal strKeikakuNendo As String, ByVal strKeikakuKanriSyouhinCd As String) As Boolean
    '    'EMAB��Q�Ή����̊i�[����
    '    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKeikakuKanriSyouhinCd)

    '    'DataSet�C���X�^���X�̐���
    '    Dim dsReturn As New Data.DataSet

    '    'SQL���̐���
    '    Dim commandTextSb As New StringBuilder

    '    '�p�����[�^�i�[
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL��
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        .AppendLine("	keikaku_kanri_syouhin_cd ")                      '�v��Ǘ�_���i�R�[�h
    '        .AppendLine("FROM ")
    '        .AppendLine("	m_keikaku_kanri_syouhin WITH(READCOMMITTED) ")   '�v��Ǘ��p_���i�}�X�^
    '        .AppendLine("WHERE ")
    '        .AppendLine("	 keikaku_nendo = @keikaku_nendo ")
    '        .AppendLine("AND keikaku_kanri_syouhin_cd = @keikaku_kanri_syouhin_cd ")
    '        .AppendLine("AND torikesi = @torikesi ")
    '    End With

    '    '�p�����[�^�̐ݒ�
    '    paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))                          '�v��N�x
    '    paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, strKeikakuKanriSyouhinCd))   '�v��Ǘ�_���i�R�[�h
    '    paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, 0))                                              '���

    '    ' �������s
    '    FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKeikakuKanriSyouhinCd", paramList.ToArray)

    '    If dsReturn.Tables("dtKeikakuKanriSyouhinCd").Rows.Count > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If

    'End Function

    ''' <summary>
    ''' �v��Ǘ��e�[�u������v��m��f�[�^����������
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <param name="strKameitenCd">�����X����</param>
    ''' <param name="strSyouhinCd">�v��Ǘ����i�R�[�h</param>
    ''' <returns>�v��m��f�[�^����</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/12 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Public Function SelKeikakuKakuteiCount(ByVal strKeikakuNendo As String, _
                                           ByVal strKameitenCd As String, _
                                           ByVal strSyouhinCd As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKameitenCd, strSyouhinCd)

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(keikaku_kanri_syouhin_cd) ")          '����
            .AppendLine("FROM ")
            .AppendLine("	t_keikaku_kanri WITH(READCOMMITTED) ")      '�v��Ǘ��e�[�u��
            .AppendLine("WHERE ")
            .AppendLine("	  keikaku_nendo = @keikaku_nendo ")                         '�v��_�N�x
            .AppendLine("AND  kameiten_cd = @kameiten_cd ")                             '�����X����
            .AppendLine("AND  keikaku_kanri_syouhin_cd = @keikaku_kanri_syouhin_cd ")   '�v��Ǘ�_���i�R�[�h
            .AppendLine("AND  (keikaku_kakutei_flg = @keikaku_kakutei_flg ")            '�v��m��FLG
            .AppendLine("OR  keikaku_huhen_flg = @keikaku_huhen_flg) ")                 '�v��l�s��FLG
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))               '�v��N�x(YYYY)
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, strKameitenCd))                '�����X����
        paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))    '�v��Ǘ�_���i�R�[�h
        paramList.Add(MakeParam("@keikaku_kakutei_flg", SqlDbType.Int, 1, "1"))                      '�v��m��FLG
        paramList.Add(MakeParam("@keikaku_huhen_flg", SqlDbType.Int, 1, "1"))                        '�v��l�s��FLG

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKeikakuKanri", paramList.ToArray)

        If Convert.ToInt32(dsReturn.Tables(0).Rows(0).Item(0)) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' FC�p�v��Ǘ��e�[�u������v��m��f�[�^����������
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <param name="strBusyoCd">��������</param>
    ''' <param name="strSyouhinCd">�v��Ǘ�_���i�R�[�h</param>
    ''' <returns>�v��m��f�[�^����</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/12 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Public Function SelFCKeikakuKakuteiCount(ByVal strKeikakuNendo As String, _
                                             ByVal strBusyoCd As String, _
                                             ByVal strSyouhinCd As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strBusyoCd, strSyouhinCd)

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(keikaku_kanri_syouhin_cd) ")              '����
            .AppendLine("FROM ")
            .AppendLine("	t_fc_keikaku_kanri WITH(READCOMMITTED) ")       'FC�p�v��Ǘ��e�[�u��
            .AppendLine("WHERE ")
            .AppendLine("	  keikaku_nendo = @keikaku_nendo ")                         '�v��_�N�x
            .AppendLine("AND  busyo_cd = @busyo_cd ")                                   '��������
            .AppendLine("AND  keikaku_kanri_syouhin_cd = @keikaku_kanri_syouhin_cd ")   '�v��Ǘ�_���i�R�[�h
            .AppendLine("AND  (keikaku_kakutei_flg = @keikaku_kakutei_flg ")            '�v��m��FLG
            .AppendLine("OR  keikaku_huhen_flg = @keikaku_huhen_flg) ")                 '�v��l�s��FLG
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))               '�v��N�x(YYYY)
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, strBusyoCd))                      '��������
        paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))    '�v��Ǘ�_���i�R�[�h
        paramList.Add(MakeParam("@keikaku_kakutei_flg", SqlDbType.Int, 1, "1"))                      '�v��m��FLG
        paramList.Add(MakeParam("@keikaku_huhen_flg", SqlDbType.Int, 1, "1"))                        '�v��l�s��FLG

        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtFCKeikakuKanri", paramList.ToArray)

        If Convert.ToInt32(dsReturn.Tables(0).Rows(0).Item(0)) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' �G���[���e���v��Ǘ��\_�捞�G���[���e�[�u���ɓo�^����
    ''' </summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <param name="drValue">�o�^�f�[�^���R�[�h</param>
    ''' <param name="strLoginUserId">���O�C�����[�U�[ID</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Function InsKeikakuTorikomiError(ByVal strEdiJouhouSakuseiDate As String, _
                                            ByVal strSyoriDatetime As String, _
                                            ByVal drValue As DataRow, _
                                            ByVal strLoginUserId As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                strEdiJouhouSakuseiDate, strSyoriDatetime, drValue, strLoginUserId)

        '�߂�l
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("INSERT INTO  ")
            .AppendLine("	t_keikaku_torikomi_error WITH(UPDLOCK)")
            .AppendLine("	( ")
            .AppendLine("		edi_jouhou_sakusei_date ")          'EDI���쐬��
            .AppendLine("		,gyou_no ")                         '�sNO
            .AppendLine("		,syori_datetime ")                  '��������
            .AppendLine("		,error_naiyou ")                    '�G���[���e
            .AppendLine("		,add_login_user_id ")               '�o�^���O�C�����[�U�[ID
            .AppendLine("		,add_datetime ")                    '�o�^����
            .AppendLine("		,upd_login_user_id ")               '�X�V���O�C�����[�U�[ID
            .AppendLine("		,upd_datetime ")                    '�X�V����
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@edi_jouhou_sakusei_date ")
            .AppendLine("	,@gyou_no ")
            .AppendLine("	,@syori_datetime ")
            .AppendLine("	,@error_naiyou ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))                'EDI���쐬��
        paramList.Add(MakeParam("@gyou_no", SqlDbType.BigInt, 12, drValue("gyou_no")))                                      '�sNO
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSyoriDatetime))))    '��������
        paramList.Add(MakeParam("@error_naiyou", SqlDbType.VarChar, 255, drValue("error_naiyou")))                          '�G���[���e
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strLoginUserId))                               '���O�C�����[�U�[ID

        '�o�^���s
        InsCount = ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        If InsCount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' �v��Ǘ��e�[�u���ւ̃f�[�^�o�^
    ''' </summary>
    ''' <param name="drValue">�o�^�f�[�^���R�[�h</param>
    ''' <param name="strLoginUserId">���O�C�����[�U�[ID</param>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/27 P-44979 ���h�m �V�K�쐬</para>																															
    ''' </history>
    Public Function InsKeikakuKanriData(ByVal drValue As DataRow, _
                                        ByVal strLoginUserId As String, _
                                        ByVal strSyoriDatetime As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, drValue, strLoginUserId, strSyoriDatetime)

        Dim sqlBuffer As New System.Text.StringBuilder
        Dim insCount As Integer

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" INSERT INTO t_keikaku_kanri WITH(UPDLOCK) ( ")
            .AppendLine("     [keikaku_nendo], ")                             '�v��_�N�x
            .AppendLine("     [add_datetime], ")                              '�o�^����
            .AppendLine("     [kameiten_cd], ")                               '�����X����
            .AppendLine("     [keikaku_kanri_syouhin_cd], ")                  '�v��Ǘ�_���i�R�[�h
            .AppendLine("     [kameiten_mei], ")                              '�����X��
            .AppendLine("     [bunbetu_cd], ")                                '���ʃR�[�h

            .AppendLine("     [4gatu_keisanyou_uri_heikin_tanka], ")          '4��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [4gatu_keisanyou_siire_heikin_tanka], ")        '4��_�v�Z�p__�d�����ϒP��

            .AppendLine("     [4gatu_keisanyou_koj_hantei_ritu], ")           '4��_�v�Z�p_�H�����藦
            .AppendLine("     [4gatu_keisanyou_koj_jyuchuu_ritu], ")          '4��_�v�Z�p_�H���󒍗�
            .AppendLine("     [4gatu_keisanyou_tyoku_koj_ritu], ")            '4��_�v�Z�p_���H����
            .AppendLine("     [4gatu_keikaku_kensuu], ")                      '4��_�v�挏��
            .AppendLine("     [4gatu_keikaku_kingaku], ")                     '4��_�v����z
            .AppendLine("     [4gatu_keikaku_arari], ")                       '4��_�v��e��

            .AppendLine("     [5gatu_keisanyou_uri_heikin_tanka], ")          '5��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [5gatu_keisanyou_siire_heikin_tanka], ")        '5��_�v�Z�p__�d�����ϒP��

            .AppendLine("     [5gatu_keisanyou_koj_hantei_ritu], ")           '5��_�v�Z�p_�H�����藦
            .AppendLine("     [5gatu_keisanyou_koj_jyuchuu_ritu], ")          '5��_�v�Z�p_�H���󒍗�
            .AppendLine("     [5gatu_keisanyou_tyoku_koj_ritu], ")            '5��_�v�Z�p_���H����
            .AppendLine("     [5gatu_keikaku_kensuu], ")                      '5��_�v�挏��
            .AppendLine("     [5gatu_keikaku_kingaku], ")                     '5��_�v����z
            .AppendLine("     [5gatu_keikaku_arari], ")                       '5��_�v��e��

            .AppendLine("     [6gatu_keisanyou_uri_heikin_tanka], ")          '6��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [6gatu_keisanyou_siire_heikin_tanka], ")        '6��_�v�Z�p__�d�����ϒP��

            .AppendLine("     [6gatu_keisanyou_koj_hantei_ritu], ")           '6��_�v�Z�p_�H�����藦
            .AppendLine("     [6gatu_keisanyou_koj_jyuchuu_ritu], ")          '6��_�v�Z�p_�H���󒍗�
            .AppendLine("     [6gatu_keisanyou_tyoku_koj_ritu], ")            '6��_�v�Z�p_���H����
            .AppendLine("     [6gatu_keikaku_kensuu], ")                      '6��_�v�挏��
            .AppendLine("     [6gatu_keikaku_kingaku], ")                     '6��_�v����z
            .AppendLine("     [6gatu_keikaku_arari], ")                       '6��_�v��e��

            .AppendLine("     [7gatu_keisanyou_uri_heikin_tanka], ")          '7��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [7gatu_keisanyou_siire_heikin_tanka], ")        '7��_�v�Z�p__�d�����ϒP��

            .AppendLine("     [7gatu_keisanyou_koj_hantei_ritu], ")           '7��_�v�Z�p_�H�����藦
            .AppendLine("     [7gatu_keisanyou_koj_jyuchuu_ritu], ")          '7��_�v�Z�p_�H���󒍗�
            .AppendLine("     [7gatu_keisanyou_tyoku_koj_ritu], ")            '7��_�v�Z�p_���H����
            .AppendLine("     [7gatu_keikaku_kensuu], ")                      '7��_�v�挏��
            .AppendLine("     [7gatu_keikaku_kingaku], ")                     '7��_�v����z
            .AppendLine("     [7gatu_keikaku_arari], ")                       '7��_�v��e��

            .AppendLine("     [8gatu_keisanyou_uri_heikin_tanka], ")          '8��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [8gatu_keisanyou_siire_heikin_tanka], ")        '8��_�v�Z�p__�d�����ϒP��

            .AppendLine("     [8gatu_keisanyou_koj_hantei_ritu], ")           '8��_�v�Z�p_�H�����藦
            .AppendLine("     [8gatu_keisanyou_koj_jyuchuu_ritu], ")          '8��_�v�Z�p_�H���󒍗�
            .AppendLine("     [8gatu_keisanyou_tyoku_koj_ritu], ")            '8��_�v�Z�p_���H����
            .AppendLine("     [8gatu_keikaku_kensuu], ")                      '8��_�v�挏��
            .AppendLine("     [8gatu_keikaku_kingaku], ")                     '8��_�v����z
            .AppendLine("     [8gatu_keikaku_arari], ")                       '8��_�v��e��

            .AppendLine("     [9gatu_keisanyou_uri_heikin_tanka], ")          '9��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [9gatu_keisanyou_siire_heikin_tanka], ")        '9��_�v�Z�p__�d�����ϒP��

            .AppendLine("     [9gatu_keisanyou_koj_hantei_ritu], ")           '9��_�v�Z�p_�H�����藦
            .AppendLine("     [9gatu_keisanyou_koj_jyuchuu_ritu], ")          '9��_�v�Z�p_�H���󒍗�
            .AppendLine("     [9gatu_keisanyou_tyoku_koj_ritu], ")            '9��_�v�Z�p_���H����
            .AppendLine("     [9gatu_keikaku_kensuu], ")                      '9��_�v�挏��
            .AppendLine("     [9gatu_keikaku_kingaku], ")                     '9��_�v����z
            .AppendLine("     [9gatu_keikaku_arari], ")                       '9��_�v��e��

            .AppendLine("     [10gatu_keisanyou_uri_heikin_tanka], ")          '10��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [10gatu_keisanyou_siire_heikin_tanka], ")        '10��_�v�Z�p__�d�����ϒP��

            .AppendLine("     [10gatu_keisanyou_koj_hantei_ritu], ")          '10��_�v�Z�p_�H�����藦
            .AppendLine("     [10gatu_keisanyou_koj_jyuchuu_ritu], ")         '10��_�v�Z�p_�H���󒍗�
            .AppendLine("     [10gatu_keisanyou_tyoku_koj_ritu], ")           '10��_�v�Z�p_���H����
            .AppendLine("     [10gatu_keikaku_kensuu], ")                     '10��_�v�挏��
            .AppendLine("     [10gatu_keikaku_kingaku], ")                    '10��_�v����z
            .AppendLine("     [10gatu_keikaku_arari], ")                      '10��_�v��e��

            .AppendLine("     [11gatu_keisanyou_uri_heikin_tanka], ")          '11��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [11gatu_keisanyou_siire_heikin_tanka], ")        '11��_�v�Z�p__�d�����ϒP��

            .AppendLine("     [11gatu_keisanyou_koj_hantei_ritu], ")          '11��_�v�Z�p_�H�����藦
            .AppendLine("     [11gatu_keisanyou_koj_jyuchuu_ritu], ")         '11��_�v�Z�p_�H���󒍗�
            .AppendLine("     [11gatu_keisanyou_tyoku_koj_ritu], ")           '11��_�v�Z�p_���H����
            .AppendLine("     [11gatu_keikaku_kensuu], ")                     '11��_�v�挏��
            .AppendLine("     [11gatu_keikaku_kingaku], ")                    '11��_�v����z
            .AppendLine("     [11gatu_keikaku_arari], ")                      '11��_�v��e��

            .AppendLine("     [12gatu_keisanyou_uri_heikin_tanka], ")          '12��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [12gatu_keisanyou_siire_heikin_tanka], ")        '12��_�v�Z�p__�d�����ϒP��

            .AppendLine("     [12gatu_keisanyou_koj_hantei_ritu], ")          '12��_�v�Z�p_�H�����藦
            .AppendLine("     [12gatu_keisanyou_koj_jyuchuu_ritu], ")         '12��_�v�Z�p_�H���󒍗�
            .AppendLine("     [12gatu_keisanyou_tyoku_koj_ritu], ")           '12��_�v�Z�p_���H����
            .AppendLine("     [12gatu_keikaku_kensuu], ")                     '12��_�v�挏��
            .AppendLine("     [12gatu_keikaku_kingaku], ")                    '12��_�v����z
            .AppendLine("     [12gatu_keikaku_arari], ")                      '12��_�v��e��

            .AppendLine("     [1gatu_keisanyou_uri_heikin_tanka], ")          '1��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [1gatu_keisanyou_siire_heikin_tanka], ")        '1��_�v�Z�p__�d�����ϒP��

            .AppendLine("     [1gatu_keisanyou_koj_hantei_ritu], ")           '1��_�v�Z�p_�H�����藦
            .AppendLine("     [1gatu_keisanyou_koj_jyuchuu_ritu], ")          '1��_�v�Z�p_�H���󒍗�
            .AppendLine("     [1gatu_keisanyou_tyoku_koj_ritu], ")            '1��_�v�Z�p_���H����
            .AppendLine("     [1gatu_keikaku_kensuu], ")                      '1��_�v�挏��
            .AppendLine("     [1gatu_keikaku_kingaku], ")                     '1��_�v����z
            .AppendLine("     [1gatu_keikaku_arari], ")                       '1��_�v��e��

            .AppendLine("     [2gatu_keisanyou_uri_heikin_tanka], ")          '2��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [2gatu_keisanyou_siire_heikin_tanka], ")        '2��_�v�Z�p__�d�����ϒP��

            .AppendLine("     [2gatu_keisanyou_koj_hantei_ritu], ")           '2��_�v�Z�p_�H�����藦
            .AppendLine("     [2gatu_keisanyou_koj_jyuchuu_ritu], ")          '2��_�v�Z�p_�H���󒍗�
            .AppendLine("     [2gatu_keisanyou_tyoku_koj_ritu], ")            '2��_�v�Z�p_���H����
            .AppendLine("     [2gatu_keikaku_kensuu], ")                      '2��_�v�挏��
            .AppendLine("     [2gatu_keikaku_kingaku], ")                     '2��_�v����z
            .AppendLine("     [2gatu_keikaku_arari], ")                       '2��_�v��e��

            .AppendLine("     [3gatu_keisanyou_uri_heikin_tanka], ")          '3��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [3gatu_keisanyou_siire_heikin_tanka], ")        '3��_�v�Z�p__�d�����ϒP��

            .AppendLine("     [3gatu_keisanyou_koj_hantei_ritu], ")           '3��_�v�Z�p_�H�����藦
            .AppendLine("     [3gatu_keisanyou_koj_jyuchuu_ritu], ")          '3��_�v�Z�p_�H���󒍗�
            .AppendLine("     [3gatu_keisanyou_tyoku_koj_ritu], ")            '3��_�v�Z�p_���H����
            .AppendLine("     [3gatu_keikaku_kensuu], ")                      '3��_�v�挏��
            .AppendLine("     [3gatu_keikaku_kingaku], ")                     '3��_�v����z
            .AppendLine("     [3gatu_keikaku_arari], ")                       '3��_�v��e��
            .AppendLine("     [keikaku_kakutei_flg], ")                       '�v��m��FLG
            .AppendLine("     [keikaku_kakutei_id], ")                        '�v��m���ID
            .AppendLine("     [keikaku_kakutei_datetime], ")                  '�v��m�����
            .AppendLine("     [kakutei_minaosi_id], ")                        '�m�茩������ID
            .AppendLine("     [kakutei_minaosi_datetime], ")                  '�m�茩��������
            .AppendLine("     [keikaku_minaosi_flg], ")                       '�v�挩����FLG
            .AppendLine("     [keikaku_huhen_flg], ")                         '�v��l�s��FLG
            '2013/10/14 ���F�ǉ��@��
            .AppendLine("     UCCRPDEV,")                                     '��A��_�ڋq�敪
            .AppendLine("     UCCRPSEQ,")                                     '��A��_�ڋq�R�[�hSEQ
            '2013/10/14 ���F�ǉ��@��
            .AppendLine("     [add_login_user_id], ")                         '�o�^���O�C�����[�U�[ID
            .AppendLine("     [upd_login_user_id], ")                         '�X�V���O�C�����[�U�[ID
            .AppendLine("     [upd_datetime] ")                               '�X�V����

            .AppendLine("    )VALUES( ")
            .AppendLine("     @keikaku_nendo, ")
            .AppendLine("     @add_datetime, ")
            .AppendLine("     @kameiten_cd, ")
            .AppendLine("     @keikaku_kanri_syouhin_cd, ")
            .AppendLine("     @kameiten_mei, ")
            .AppendLine("     @bunbetu_cd, ")

            .AppendLine("     @4gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @4gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @4gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @4gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @4gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @4gatu_keikaku_kensuu, ")
            .AppendLine("     @4gatu_keikaku_kingaku, ")
            .AppendLine("     @4gatu_keikaku_arari, ")

            .AppendLine("     @5gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @5gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @5gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @5gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @5gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @5gatu_keikaku_kensuu, ")
            .AppendLine("     @5gatu_keikaku_kingaku, ")
            .AppendLine("     @5gatu_keikaku_arari, ")

            .AppendLine("     @6gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @6gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @6gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @6gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @6gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @6gatu_keikaku_kensuu, ")
            .AppendLine("     @6gatu_keikaku_kingaku, ")
            .AppendLine("     @6gatu_keikaku_arari, ")

            .AppendLine("     @7gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @7gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @7gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @7gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @7gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @7gatu_keikaku_kensuu, ")
            .AppendLine("     @7gatu_keikaku_kingaku, ")
            .AppendLine("     @7gatu_keikaku_arari, ")

            .AppendLine("     @8gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @8gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @8gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @8gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @8gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @8gatu_keikaku_kensuu, ")
            .AppendLine("     @8gatu_keikaku_kingaku, ")
            .AppendLine("     @8gatu_keikaku_arari, ")

            .AppendLine("     @9gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @9gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @9gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @9gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @9gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @9gatu_keikaku_kensuu, ")
            .AppendLine("     @9gatu_keikaku_kingaku, ")
            .AppendLine("     @9gatu_keikaku_arari, ")

            .AppendLine("     @10gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @10gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @10gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @10gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @10gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @10gatu_keikaku_kensuu, ")
            .AppendLine("     @10gatu_keikaku_kingaku, ")
            .AppendLine("     @10gatu_keikaku_arari, ")

            .AppendLine("     @11gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @11gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @11gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @11gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @11gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @11gatu_keikaku_kensuu, ")
            .AppendLine("     @11gatu_keikaku_kingaku, ")
            .AppendLine("     @11gatu_keikaku_arari, ")

            .AppendLine("     @12gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @12gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @12gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @12gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @12gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @12gatu_keikaku_kensuu, ")
            .AppendLine("     @12gatu_keikaku_kingaku, ")
            .AppendLine("     @12gatu_keikaku_arari, ")

            .AppendLine("     @1gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @1gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @1gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @1gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @1gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @1gatu_keikaku_kensuu, ")
            .AppendLine("     @1gatu_keikaku_kingaku, ")
            .AppendLine("     @1gatu_keikaku_arari, ")

            .AppendLine("     @2gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @2gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @2gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @2gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @2gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @2gatu_keikaku_kensuu, ")
            .AppendLine("     @2gatu_keikaku_kingaku, ")
            .AppendLine("     @2gatu_keikaku_arari, ")

            .AppendLine("     @3gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @3gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @3gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @3gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @3gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @3gatu_keikaku_kensuu, ")
            .AppendLine("     @3gatu_keikaku_kingaku, ")
            .AppendLine("     @3gatu_keikaku_arari, ")
            .AppendLine("     NULL, ")
            .AppendLine("     NULL, ")
            .AppendLine("     NULL, ")
            .AppendLine("     NULL, ")
            .AppendLine("     NULL, ")
            .AppendLine("     NULL, ")
            .AppendLine("     NULL, ")
            '2013/10/14 ���F�ǉ��@��
            .AppendLine("     @UCCRPDEV,")                                      '��A��_�ڋq�敪
            .AppendLine("     @UCCRPSEQ,")                                      '��A��_�ڋq�R�[�hSEQ
            '2013/10/14 ���F�ǉ��@��
            .AppendLine("     @add_login_user_id, ")
            .AppendLine("     @add_login_user_id, ")
            .AppendLine("     GETDATE() ")

            .AppendLine(" 	) ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, drValue("keikaku_nendo")))                         '�v��N�x(YYYY)
        paramList.Add(MakeParam("@add_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSyoriDatetime))))  '�o�^����
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, drValue("kameiten_cd")))                          '�����X����
        paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, drValue("keikaku_kanri_syouhin_cd")))                        '�v��Ǘ�_���i�R�[�h
        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, drValue("kameiten_mei")))                       '�����X��
        paramList.Add(MakeParam("@bunbetu_cd", SqlDbType.VarChar, 3, drValue("bunbetu_cd")))                            '���ʃR�[�h

        paramList.Add(MakeParam("@4gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("4gatu_keisanyou_uri_heikin_tanka")))        '4��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@4gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("4gatu_keisanyou_siire_heikin_tanka")))    '4��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@4gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("4gatu_keisanyou_koj_hantei_ritu")))          '4��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@4gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("4gatu_keisanyou_koj_jyuchuu_ritu")))        '4��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@4gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("4gatu_keisanyou_tyoku_koj_ritu")))            '4��_�v�Z�p_���H����
        paramList.Add(MakeParam("@4gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("4gatu_keikaku_kensuu")))        '4��_�v�挏��
        paramList.Add(MakeParam("@4gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("4gatu_keikaku_kingaku")))      '4��_�v����z
        paramList.Add(MakeParam("@4gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("4gatu_keikaku_arari")))          '4��_�v��e��

        paramList.Add(MakeParam("@5gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("5gatu_keisanyou_uri_heikin_tanka")))        '5��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@5gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("5gatu_keisanyou_siire_heikin_tanka")))    '5��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@5gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("5gatu_keisanyou_koj_hantei_ritu")))          '5��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@5gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("5gatu_keisanyou_koj_jyuchuu_ritu")))        '5��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@5gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("5gatu_keisanyou_tyoku_koj_ritu")))            '5��_�v�Z�p_���H����
        paramList.Add(MakeParam("@5gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("5gatu_keikaku_kensuu")))        '5��_�v�挏��
        paramList.Add(MakeParam("@5gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("5gatu_keikaku_kingaku")))      '5��_�v����z
        paramList.Add(MakeParam("@5gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("5gatu_keikaku_arari")))          '5��_�v��e��

        paramList.Add(MakeParam("@6gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("6gatu_keisanyou_uri_heikin_tanka")))        '6��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@6gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("6gatu_keisanyou_siire_heikin_tanka")))    '6��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@6gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("6gatu_keisanyou_koj_hantei_ritu")))          '6��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@6gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("6gatu_keisanyou_koj_jyuchuu_ritu")))        '6��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@6gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("6gatu_keisanyou_tyoku_koj_ritu")))            '6��_�v�Z�p_���H����
        paramList.Add(MakeParam("@6gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("6gatu_keikaku_kensuu")))        '6��_�v�挏��
        paramList.Add(MakeParam("@6gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("6gatu_keikaku_kingaku")))      '6��_�v����z
        paramList.Add(MakeParam("@6gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("6gatu_keikaku_arari")))          '6��_�v��e��

        paramList.Add(MakeParam("@7gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("7gatu_keisanyou_uri_heikin_tanka")))        '7��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@7gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("7gatu_keisanyou_siire_heikin_tanka")))    '7��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@7gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("7gatu_keisanyou_koj_hantei_ritu")))          '7��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@7gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("7gatu_keisanyou_koj_jyuchuu_ritu")))        '7��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@7gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("7gatu_keisanyou_tyoku_koj_ritu")))            '7��_�v�Z�p_���H����
        paramList.Add(MakeParam("@7gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("7gatu_keikaku_kensuu")))        '7��_�v�挏��
        paramList.Add(MakeParam("@7gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("7gatu_keikaku_kingaku")))      '7��_�v����z
        paramList.Add(MakeParam("@7gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("7gatu_keikaku_arari")))          '7��_�v��e��

        paramList.Add(MakeParam("@8gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("8gatu_keisanyou_uri_heikin_tanka")))        '8��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@8gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("8gatu_keisanyou_siire_heikin_tanka")))    '8��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@8gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("8gatu_keisanyou_koj_hantei_ritu")))          '8��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@8gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("8gatu_keisanyou_koj_jyuchuu_ritu")))        '8��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@8gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("8gatu_keisanyou_tyoku_koj_ritu")))            '8��_�v�Z�p_���H����
        paramList.Add(MakeParam("@8gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("8gatu_keikaku_kensuu")))        '8��_�v�挏��
        paramList.Add(MakeParam("@8gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("8gatu_keikaku_kingaku")))      '8��_�v����z
        paramList.Add(MakeParam("@8gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("8gatu_keikaku_arari")))          '8��_�v��e��

        paramList.Add(MakeParam("@9gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("9gatu_keisanyou_uri_heikin_tanka")))        '9��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@9gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("9gatu_keisanyou_siire_heikin_tanka")))    '9��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@9gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("9gatu_keisanyou_koj_hantei_ritu")))          '9��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@9gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("9gatu_keisanyou_koj_jyuchuu_ritu")))        '9��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@9gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("9gatu_keisanyou_tyoku_koj_ritu")))            '9��_�v�Z�p_���H����
        paramList.Add(MakeParam("@9gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("9gatu_keikaku_kensuu")))        '9��_�v�挏��
        paramList.Add(MakeParam("@9gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("9gatu_keikaku_kingaku")))      '9��_�v����z
        paramList.Add(MakeParam("@9gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("9gatu_keikaku_arari")))          '9��_�v��e��

        paramList.Add(MakeParam("@10gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("10gatu_keisanyou_uri_heikin_tanka")))        '10��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@10gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("10gatu_keisanyou_siire_heikin_tanka")))    '10��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@10gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("10gatu_keisanyou_koj_hantei_ritu")))        '10��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@10gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("10gatu_keisanyou_koj_jyuchuu_ritu")))      '10��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@10gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("10gatu_keisanyou_tyoku_koj_ritu")))          '10��_�v�Z�p_���H����
        paramList.Add(MakeParam("@10gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("10gatu_keikaku_kensuu")))      '10��_�v�挏��
        paramList.Add(MakeParam("@10gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("10gatu_keikaku_kingaku")))    '10��_�v����z
        paramList.Add(MakeParam("@10gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("10gatu_keikaku_arari")))        '10��_�v��e��

        paramList.Add(MakeParam("@11gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("11gatu_keisanyou_uri_heikin_tanka")))        '11��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@11gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("11gatu_keisanyou_siire_heikin_tanka")))    '11��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@11gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("11gatu_keisanyou_koj_hantei_ritu")))        '11��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@11gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("11gatu_keisanyou_koj_jyuchuu_ritu")))      '11��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@11gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("11gatu_keisanyou_tyoku_koj_ritu")))          '11��_�v�Z�p_���H����
        paramList.Add(MakeParam("@11gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("11gatu_keikaku_kensuu")))      '11��_�v�挏��
        paramList.Add(MakeParam("@11gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("11gatu_keikaku_kingaku")))    '11��_�v����z
        paramList.Add(MakeParam("@11gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("11gatu_keikaku_arari")))        '11��_�v��e��

        paramList.Add(MakeParam("@12gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("12gatu_keisanyou_uri_heikin_tanka")))        '12��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@12gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("12gatu_keisanyou_siire_heikin_tanka")))    '12��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@12gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("12gatu_keisanyou_koj_hantei_ritu")))        '12��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@12gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("12gatu_keisanyou_koj_jyuchuu_ritu")))      '12��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@12gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("12gatu_keisanyou_tyoku_koj_ritu")))          '12��_�v�Z�p_���H����
        paramList.Add(MakeParam("@12gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("12gatu_keikaku_kensuu")))      '12��_�v�挏��
        paramList.Add(MakeParam("@12gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("12gatu_keikaku_kingaku")))    '12��_�v����z
        paramList.Add(MakeParam("@12gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("12gatu_keikaku_arari")))        '12��_�v��e��

        paramList.Add(MakeParam("@1gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("1gatu_keisanyou_uri_heikin_tanka")))        '1��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@1gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("1gatu_keisanyou_siire_heikin_tanka")))    '1��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@1gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("1gatu_keisanyou_koj_hantei_ritu")))          '1��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@1gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("1gatu_keisanyou_koj_jyuchuu_ritu")))        '1��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@1gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("1gatu_keisanyou_tyoku_koj_ritu")))            '1��_�v�Z�p_���H����
        paramList.Add(MakeParam("@1gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("1gatu_keikaku_kensuu")))        '1��_�v�挏��
        paramList.Add(MakeParam("@1gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("1gatu_keikaku_kingaku")))      '1��_�v����z
        paramList.Add(MakeParam("@1gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("1gatu_keikaku_arari")))          '1��_�v��e��

        paramList.Add(MakeParam("@2gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("2gatu_keisanyou_uri_heikin_tanka")))        '2��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@2gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("2gatu_keisanyou_siire_heikin_tanka")))    '2��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@2gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("2gatu_keisanyou_koj_hantei_ritu")))          '2��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@2gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("2gatu_keisanyou_koj_jyuchuu_ritu")))        '2��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@2gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("2gatu_keisanyou_tyoku_koj_ritu")))            '2��_�v�Z�p_���H����
        paramList.Add(MakeParam("@2gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("2gatu_keikaku_kensuu")))        '2��_�v�挏��
        paramList.Add(MakeParam("@2gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("2gatu_keikaku_kingaku")))      '2��_�v����z
        paramList.Add(MakeParam("@2gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("2gatu_keikaku_arari")))          '2��_�v��e��

        paramList.Add(MakeParam("@3gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("3gatu_keisanyou_uri_heikin_tanka")))        '3��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@3gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("3gatu_keisanyou_siire_heikin_tanka")))    '3��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@3gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("3gatu_keisanyou_koj_hantei_ritu")))          '3��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@3gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("3gatu_keisanyou_koj_jyuchuu_ritu")))        '3��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@3gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("3gatu_keisanyou_tyoku_koj_ritu")))            '3��_�v�Z�p_���H����
        paramList.Add(MakeParam("@3gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("3gatu_keikaku_kensuu")))        '3��_�v�挏��
        paramList.Add(MakeParam("@3gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("3gatu_keikaku_kingaku")))      '3��_�v����z
        paramList.Add(MakeParam("@3gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("3gatu_keikaku_arari")))          '3��_�v��e��
        '2013/10/14 ���F�ǉ��@��
        paramList.Add(MakeParam("@UCCRPDEV", SqlDbType.VarChar, 2, drValue("UCCRPDEV")))      '��A��_�ڋq�敪
        paramList.Add(MakeParam("@UCCRPSEQ", SqlDbType.Decimal, 10, drValue("UCCRPSEQ")))     '��A��_�ڋq�R�[�hSEQ
        '2013/10/14 ���F�ǉ��@��
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strLoginUserId))                           '�o�^���O�C�����[�U�[ID

        '�o�^���s
        Call GetDebugSql(sqlBuffer.ToString, paramList)
        insCount = SQLHelper.ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, paramList.ToArray())

        If insCount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' �\�茩���Ǘ��e�[�u���ւ̃f�[�^�o�^
    ''' </summary>
    ''' <param name="drValue">�o�^�f�[�^���R�[�h</param>
    ''' <param name="strLoginUserId">���O�C�����[�U�[ID</param>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/27 P-44979 ���h�m �V�K�쐬</para>																															
    ''' </history>
    Public Function InsYoteiMikomiKanriData(ByVal drValue As DataRow, _
                                            ByVal strLoginUserId As String, _
                                            ByVal strSyoriDatetime As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, drValue, strLoginUserId, strSyoriDatetime)

        Dim sqlBuffer As New System.Text.StringBuilder
        Dim insCount As Integer

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" INSERT INTO t_yotei_mikomi_kanri WITH(UPDLOCK)( ")
            .AppendLine("     [keikaku_nendo], ")                            '�v��_�N�x
            .AppendLine("     [add_datetime], ")                             '�o�^����
            .AppendLine("     [kameiten_cd], ")                              '�����X����
            .AppendLine("     [keikaku_kanri_syouhin_cd], ")                 '�v��Ǘ�_���i�R�[�h
            .AppendLine("     [kameiten_mei], ")                             '�����X��
            .AppendLine("     [bunbetu_cd], ")                               '���ʃR�[�h
            .AppendLine("     [4gatu_mikomi_kensuu], ")                      '4��_��������
            .AppendLine("     [4gatu_mikomi_kingaku], ")                     '4��_�������z
            .AppendLine("     [4gatu_mikomi_arari], ")                       '4��_�����e��
            .AppendLine("     [5gatu_mikomi_kensuu], ")                      '5��_��������
            .AppendLine("     [5gatu_mikomi_kingaku], ")                     '5��_�������z
            .AppendLine("     [5gatu_mikomi_arari], ")                       '5��_�����e��
            .AppendLine("     [6gatu_mikomi_kensuu], ")                      '6��_��������
            .AppendLine("     [6gatu_mikomi_kingaku], ")                     '6��_�������z
            .AppendLine("     [6gatu_mikomi_arari], ")                       '6��_�����e��
            .AppendLine("     [7gatu_mikomi_kensuu], ")                      '7��_��������
            .AppendLine("     [7gatu_mikomi_kingaku], ")                     '7��_�������z
            .AppendLine("     [7gatu_mikomi_arari], ")                       '7��_�����e��
            .AppendLine("     [8gatu_mikomi_kensuu], ")                      '8��_��������
            .AppendLine("     [8gatu_mikomi_kingaku], ")                     '8��_�������z
            .AppendLine("     [8gatu_mikomi_arari], ")                       '8��_�����e��
            .AppendLine("     [9gatu_mikomi_kensuu], ")                      '9��_��������
            .AppendLine("     [9gatu_mikomi_kingaku], ")                     '9��_�������z
            .AppendLine("     [9gatu_mikomi_arari], ")                       '9��_�����e��
            .AppendLine("     [10gatu_mikomi_kensuu], ")                     '10��_��������
            .AppendLine("     [10gatu_mikomi_kingaku], ")                    '10��_�������z
            .AppendLine("     [10gatu_mikomi_arari], ")                      '10��_�����e��
            .AppendLine("     [11gatu_mikomi_kensuu], ")                     '11��_��������
            .AppendLine("     [11gatu_mikomi_kingaku], ")                    '11��_�������z
            .AppendLine("     [11gatu_mikomi_arari], ")                      '11��_�����e��
            .AppendLine("     [12gatu_mikomi_kensuu], ")                     '12��_��������
            .AppendLine("     [12gatu_mikomi_kingaku], ")                    '12��_�������z
            .AppendLine("     [12gatu_mikomi_arari], ")                      '12��_�����e��
            .AppendLine("     [1gatu_mikomi_kensuu], ")                      '1��_��������
            .AppendLine("     [1gatu_mikomi_kingaku], ")                     '1��_�������z
            .AppendLine("     [1gatu_mikomi_arari], ")                       '1��_�����e��
            .AppendLine("     [2gatu_mikomi_kensuu], ")                      '2��_��������
            .AppendLine("     [2gatu_mikomi_kingaku], ")                     '2��_�������z
            .AppendLine("     [2gatu_mikomi_arari], ")                       '2��_�����e��
            .AppendLine("     [3gatu_mikomi_kensuu], ")                      '3��_��������
            .AppendLine("     [3gatu_mikomi_kingaku], ")                     '3��_�������z
            .AppendLine("     [3gatu_mikomi_arari], ")                       '3��_�����e��
            .AppendLine("     [add_login_user_id], ")                        '�o�^���O�C�����[�U�[ID
            .AppendLine("     [upd_login_user_id], ")                        '�X�V���O�C�����[�U�[ID
            .AppendLine("     [upd_datetime] ")                              '�X�V����
            .AppendLine("    )VALUES( ")
            .AppendLine("     @keikaku_nendo, ")
            .AppendLine("     @add_datetime, ")
            .AppendLine("     @kameiten_cd, ")
            .AppendLine("     @keikaku_kanri_syouhin_cd, ")
            .AppendLine("     @kameiten_mei, ")
            .AppendLine("     @bunbetu_cd, ")
            .AppendLine("     @4gatu_mikomi_kensuu, ")
            .AppendLine("     @4gatu_mikomi_kingaku, ")
            .AppendLine("     @4gatu_mikomi_arari, ")
            .AppendLine("     @5gatu_mikomi_kensuu, ")
            .AppendLine("     @5gatu_mikomi_kingaku, ")
            .AppendLine("     @5gatu_mikomi_arari, ")
            .AppendLine("     @6gatu_mikomi_kensuu, ")
            .AppendLine("     @6gatu_mikomi_kingaku, ")
            .AppendLine("     @6gatu_mikomi_arari, ")
            .AppendLine("     @7gatu_mikomi_kensuu, ")
            .AppendLine("     @7gatu_mikomi_kingaku, ")
            .AppendLine("     @7gatu_mikomi_arari, ")
            .AppendLine("     @8gatu_mikomi_kensuu, ")
            .AppendLine("     @8gatu_mikomi_kingaku, ")
            .AppendLine("     @8gatu_mikomi_arari, ")
            .AppendLine("     @9gatu_mikomi_kensuu, ")
            .AppendLine("     @9gatu_mikomi_kingaku, ")
            .AppendLine("     @9gatu_mikomi_arari, ")
            .AppendLine("     @10gatu_mikomi_kensuu, ")
            .AppendLine("     @10gatu_mikomi_kingaku, ")
            .AppendLine("     @10gatu_mikomi_arari, ")
            .AppendLine("     @11gatu_mikomi_kensuu, ")
            .AppendLine("     @11gatu_mikomi_kingaku, ")
            .AppendLine("     @11gatu_mikomi_arari, ")
            .AppendLine("     @12gatu_mikomi_kensuu, ")
            .AppendLine("     @12gatu_mikomi_kingaku, ")
            .AppendLine("     @12gatu_mikomi_arari, ")
            .AppendLine("     @1gatu_mikomi_kensuu, ")
            .AppendLine("     @1gatu_mikomi_kingaku, ")
            .AppendLine("     @1gatu_mikomi_arari, ")
            .AppendLine("     @2gatu_mikomi_kensuu, ")
            .AppendLine("     @2gatu_mikomi_kingaku, ")
            .AppendLine("     @2gatu_mikomi_arari, ")
            .AppendLine("     @3gatu_mikomi_kensuu, ")
            .AppendLine("     @3gatu_mikomi_kingaku, ")
            .AppendLine("     @3gatu_mikomi_arari, ")
            .AppendLine("     @add_login_user_id, ")
            .AppendLine("     @add_login_user_id, ")
            .AppendLine("     GETDATE() ")
            .AppendLine(" 	) ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, drValue("keikaku_nendo")))                       '�v��N�x(YYYY)
        paramList.Add(MakeParam("@add_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSyoriDatetime))))  '�o�^����
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, drValue("kameiten_cd")))                        '�����X����
        paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, drValue("keikaku_kanri_syouhin_cd")))       '�v��Ǘ�_���i�R�[�h
        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, drValue("kameiten_mei")))                     '�����X��
        paramList.Add(MakeParam("@bunbetu_cd", SqlDbType.VarChar, 3, drValue("bunbetu_cd")))                          '���ʃR�[�h
        paramList.Add(MakeParam("@4gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("4gatu_mikomi_kensuu")))        '4��_��������
        paramList.Add(MakeParam("@4gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("4gatu_mikomi_kingaku")))      '4��_�������z
        paramList.Add(MakeParam("@4gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("4gatu_mikomi_arari")))          '4��_�����e��
        paramList.Add(MakeParam("@5gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("5gatu_mikomi_kensuu")))        '5��_��������
        paramList.Add(MakeParam("@5gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("5gatu_mikomi_kingaku")))      '5��_�������z
        paramList.Add(MakeParam("@5gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("5gatu_mikomi_arari")))          '5��_�����e��
        paramList.Add(MakeParam("@6gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("6gatu_mikomi_kensuu")))        '6��_��������
        paramList.Add(MakeParam("@6gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("6gatu_mikomi_kingaku")))      '6��_�������z
        paramList.Add(MakeParam("@6gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("6gatu_mikomi_arari")))          '6��_�����e��
        paramList.Add(MakeParam("@7gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("7gatu_mikomi_kensuu")))        '7��_��������
        paramList.Add(MakeParam("@7gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("7gatu_mikomi_kingaku")))      '7��_�������z
        paramList.Add(MakeParam("@7gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("7gatu_mikomi_arari")))          '7��_�����e��
        paramList.Add(MakeParam("@8gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("8gatu_mikomi_kensuu")))        '8��_��������
        paramList.Add(MakeParam("@8gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("8gatu_mikomi_kingaku")))      '8��_�������z
        paramList.Add(MakeParam("@8gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("8gatu_mikomi_arari")))          '8��_�����e��
        paramList.Add(MakeParam("@9gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("9gatu_mikomi_kensuu")))        '9��_��������
        paramList.Add(MakeParam("@9gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("9gatu_mikomi_kingaku")))      '9��_�������z
        paramList.Add(MakeParam("@9gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("9gatu_mikomi_arari")))          '9��_�����e��
        paramList.Add(MakeParam("@10gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("10gatu_mikomi_kensuu")))      '10��_��������
        paramList.Add(MakeParam("@10gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("10gatu_mikomi_kingaku")))    '10��_�������z
        paramList.Add(MakeParam("@10gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("10gatu_mikomi_arari")))        '10��_�����e��
        paramList.Add(MakeParam("@11gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("11gatu_mikomi_kensuu")))      '11��_��������
        paramList.Add(MakeParam("@11gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("11gatu_mikomi_kingaku")))    '11��_�������z
        paramList.Add(MakeParam("@11gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("11gatu_mikomi_arari")))        '11��_�����e��
        paramList.Add(MakeParam("@12gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("12gatu_mikomi_kensuu")))      '12��_��������
        paramList.Add(MakeParam("@12gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("12gatu_mikomi_kingaku")))    '12��_�������z
        paramList.Add(MakeParam("@12gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("12gatu_mikomi_arari")))        '12��_�����e��
        paramList.Add(MakeParam("@1gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("1gatu_mikomi_kensuu")))        '1��_��������
        paramList.Add(MakeParam("@1gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("1gatu_mikomi_kingaku")))      '1��_�������z
        paramList.Add(MakeParam("@1gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("1gatu_mikomi_arari")))          '1��_�����e��
        paramList.Add(MakeParam("@2gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("2gatu_mikomi_kensuu")))        '2��_��������
        paramList.Add(MakeParam("@2gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("2gatu_mikomi_kingaku")))      '2��_�������z
        paramList.Add(MakeParam("@2gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("2gatu_mikomi_arari")))          '2��_�����e��
        paramList.Add(MakeParam("@3gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("3gatu_mikomi_kensuu")))        '3��_��������
        paramList.Add(MakeParam("@3gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("3gatu_mikomi_kingaku")))      '3��_�������z
        paramList.Add(MakeParam("@3gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("3gatu_mikomi_arari")))          '3��_�����e��
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strLoginUserId))                         '�o�^���O�C�����[�U�[ID

        '�o�^���s
        insCount = SQLHelper.ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, paramList.ToArray())

        If insCount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' FC�p�v��Ǘ��e�[�u���ւ̃f�[�^�o�^
    ''' </summary>
    ''' <param name="drValue">�o�^�f�[�^���R�[�h</param>
    ''' <param name="strLoginUserId">���O�C�����[�U�[ID</param>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/30 P-44979 ���h�m �V�K�쐬</para>																															
    ''' </history>
    Public Function InsFCKeikakuKanriData(ByVal drValue As DataRow, _
                                          ByVal strLoginUserId As String, _
                                          ByVal strSyoriDatetime As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, drValue, strLoginUserId, strSyoriDatetime)

        Dim sqlBuffer As New System.Text.StringBuilder
        Dim insCount As Integer

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine("INSERT INTO  ")
            .AppendLine("	t_fc_keikaku_kanri WITH(UPDLOCK)")
            .AppendLine("	( ")
            .AppendLine("    [keikaku_nendo], ")                           '�v��_�N�x
            .AppendLine("    [add_datetime], ")                            '�o�^����
            .AppendLine("    [busyo_cd], ")                                '��������
            .AppendLine("    [keikaku_kanri_syouhin_cd], ")                '�v��Ǘ�_���i�R�[�h
            .AppendLine("    [siten_mei], ")                               '�x�X��
            .AppendLine("    [bunbetu_cd], ")                              '���ʃR�[�h

            .AppendLine("     [4gatu_keisanyou_uri_heikin_tanka], ")          '4��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [4gatu_keisanyou_siire_heikin_tanka], ")        '4��_�v�Z�p__�d�����ϒP��

            .AppendLine("    [4gatu_keisanyou_koj_hantei_ritu], ")         '4��_�v�Z�p_�H�����藦
            .AppendLine("    [4gatu_keisanyou_koj_jyuchuu_ritu], ")        '4��_�v�Z�p_�H���󒍗�
            .AppendLine("    [4gatu_keisanyou_tyoku_koj_ritu], ")          '4��_�v�Z�p_���H����
            .AppendLine("    [4gatu_keikaku_kensuu], ")                    '4��_�v�挏��
            .AppendLine("    [4gatu_keikaku_kingaku], ")                   '4��_�v����z
            .AppendLine("    [4gatu_keikaku_arari], ")                     '4��_�v��e��

            .AppendLine("     [5gatu_keisanyou_uri_heikin_tanka], ")          '5��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [5gatu_keisanyou_siire_heikin_tanka], ")        '5��_�v�Z�p__�d�����ϒP��

            .AppendLine("    [5gatu_keisanyou_koj_hantei_ritu], ")         '5��_�v�Z�p_�H�����藦
            .AppendLine("    [5gatu_keisanyou_koj_jyuchuu_ritu], ")        '5��_�v�Z�p_�H���󒍗�
            .AppendLine("    [5gatu_keisanyou_tyoku_koj_ritu], ")          '5��_�v�Z�p_���H����
            .AppendLine("    [5gatu_keikaku_kensuu], ")                    '5��_�v�挏��
            .AppendLine("    [5gatu_keikaku_kingaku], ")                   '5��_�v����z
            .AppendLine("    [5gatu_keikaku_arari], ")                     '5��_�v��e��

            .AppendLine("     [6gatu_keisanyou_uri_heikin_tanka], ")          '6��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [6gatu_keisanyou_siire_heikin_tanka], ")        '6��_�v�Z�p__�d�����ϒP��

            .AppendLine("    [6gatu_keisanyou_koj_hantei_ritu], ")         '6��_�v�Z�p_�H�����藦
            .AppendLine("    [6gatu_keisanyou_koj_jyuchuu_ritu], ")        '6��_�v�Z�p_�H���󒍗�
            .AppendLine("    [6gatu_keisanyou_tyoku_koj_ritu], ")          '6��_�v�Z�p_���H����
            .AppendLine("    [6gatu_keikaku_kensuu], ")                    '6��_�v�挏��
            .AppendLine("    [6gatu_keikaku_kingaku], ")                   '6��_�v����z
            .AppendLine("    [6gatu_keikaku_arari], ")                     '6��_�v��e��

            .AppendLine("     [7gatu_keisanyou_uri_heikin_tanka], ")          '7��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [7gatu_keisanyou_siire_heikin_tanka], ")        '7��_�v�Z�p__�d�����ϒP��

            .AppendLine("    [7gatu_keisanyou_koj_hantei_ritu], ")         '7��_�v�Z�p_�H�����藦
            .AppendLine("    [7gatu_keisanyou_koj_jyuchuu_ritu], ")        '7��_�v�Z�p_�H���󒍗�
            .AppendLine("    [7gatu_keisanyou_tyoku_koj_ritu], ")          '7��_�v�Z�p_���H����
            .AppendLine("    [7gatu_keikaku_kensuu], ")                    '7��_�v�挏��
            .AppendLine("    [7gatu_keikaku_kingaku], ")                   '7��_�v����z
            .AppendLine("    [7gatu_keikaku_arari], ")                     '7��_�v��e��

            .AppendLine("     [8gatu_keisanyou_uri_heikin_tanka], ")          '8��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [8gatu_keisanyou_siire_heikin_tanka], ")        '8��_�v�Z�p__�d�����ϒP��

            .AppendLine("    [8gatu_keisanyou_koj_hantei_ritu], ")         '8��_�v�Z�p_�H�����藦
            .AppendLine("    [8gatu_keisanyou_koj_jyuchuu_ritu], ")        '8��_�v�Z�p_�H���󒍗�
            .AppendLine("    [8gatu_keisanyou_tyoku_koj_ritu], ")          '8��_�v�Z�p_���H����
            .AppendLine("    [8gatu_keikaku_kensuu], ")                    '8��_�v�挏��
            .AppendLine("    [8gatu_keikaku_kingaku], ")                   '8��_�v����z
            .AppendLine("    [8gatu_keikaku_arari], ")                     '8��_�v��e��

            .AppendLine("     [9gatu_keisanyou_uri_heikin_tanka], ")          '9��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [9gatu_keisanyou_siire_heikin_tanka], ")        '9��_�v�Z�p__�d�����ϒP��

            .AppendLine("    [9gatu_keisanyou_koj_hantei_ritu], ")         '9��_�v�Z�p_�H�����藦
            .AppendLine("    [9gatu_keisanyou_koj_jyuchuu_ritu], ")        '9��_�v�Z�p_�H���󒍗�
            .AppendLine("    [9gatu_keisanyou_tyoku_koj_ritu], ")          '9��_�v�Z�p_���H����
            .AppendLine("    [9gatu_keikaku_kensuu], ")                    '9��_�v�挏��
            .AppendLine("    [9gatu_keikaku_kingaku], ")                   '9��_�v����z
            .AppendLine("    [9gatu_keikaku_arari], ")                     '9��_�v��e��

            .AppendLine("     [10gatu_keisanyou_uri_heikin_tanka], ")          '10��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [10gatu_keisanyou_siire_heikin_tanka], ")        '10��_�v�Z�p__�d�����ϒP��

            .AppendLine("    [10gatu_keisanyou_koj_hantei_ritu], ")        '10��_�v�Z�p_�H�����藦
            .AppendLine("    [10gatu_keisanyou_koj_jyuchuu_ritu], ")       '10��_�v�Z�p_�H���󒍗�
            .AppendLine("    [10gatu_keisanyou_tyoku_koj_ritu], ")         '10��_�v�Z�p_���H����
            .AppendLine("    [10gatu_keikaku_kensuu], ")                   '10��_�v�挏��
            .AppendLine("    [10gatu_keikaku_kingaku], ")                  '10��_�v����z
            .AppendLine("    [10gatu_keikaku_arari], ")                    '10��_�v��e��

            .AppendLine("     [11gatu_keisanyou_uri_heikin_tanka], ")          '11��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [11gatu_keisanyou_siire_heikin_tanka], ")        '11��_�v�Z�p__�d�����ϒP��

            .AppendLine("    [11gatu_keisanyou_koj_hantei_ritu], ")        '11��_�v�Z�p_�H�����藦
            .AppendLine("    [11gatu_keisanyou_koj_jyuchuu_ritu], ")       '11��_�v�Z�p_�H���󒍗�
            .AppendLine("    [11gatu_keisanyou_tyoku_koj_ritu], ")         '11��_�v�Z�p_���H����
            .AppendLine("    [11gatu_keikaku_kensuu], ")                   '11��_�v�挏��
            .AppendLine("    [11gatu_keikaku_kingaku], ")                  '11��_�v����z
            .AppendLine("    [11gatu_keikaku_arari], ")                    '11��_�v��e��

            .AppendLine("     [12gatu_keisanyou_uri_heikin_tanka], ")          '12��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [12gatu_keisanyou_siire_heikin_tanka], ")        '12��_�v�Z�p__�d�����ϒP��

            .AppendLine("    [12gatu_keisanyou_koj_hantei_ritu], ")        '12��_�v�Z�p_�H�����藦
            .AppendLine("    [12gatu_keisanyou_koj_jyuchuu_ritu], ")       '12��_�v�Z�p_�H���󒍗�
            .AppendLine("    [12gatu_keisanyou_tyoku_koj_ritu], ")         '12��_�v�Z�p_���H����
            .AppendLine("    [12gatu_keikaku_kensuu], ")                   '12��_�v�挏��
            .AppendLine("    [12gatu_keikaku_kingaku], ")                  '12��_�v����z
            .AppendLine("    [12gatu_keikaku_arari], ")                    '12��_�v��e��

            .AppendLine("     [1gatu_keisanyou_uri_heikin_tanka], ")          '1��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [1gatu_keisanyou_siire_heikin_tanka], ")        '1��_�v�Z�p__�d�����ϒP��

            .AppendLine("    [1gatu_keisanyou_koj_hantei_ritu], ")         '1��_�v�Z�p_�H�����藦
            .AppendLine("    [1gatu_keisanyou_koj_jyuchuu_ritu], ")        '1��_�v�Z�p_�H���󒍗�
            .AppendLine("    [1gatu_keisanyou_tyoku_koj_ritu], ")          '1��_�v�Z�p_���H����
            .AppendLine("    [1gatu_keikaku_kensuu], ")                    '1��_�v�挏��
            .AppendLine("    [1gatu_keikaku_kingaku], ")                   '1��_�v����z
            .AppendLine("    [1gatu_keikaku_arari], ")                     '1��_�v��e��

            .AppendLine("     [2gatu_keisanyou_uri_heikin_tanka], ")          '2��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [2gatu_keisanyou_siire_heikin_tanka], ")        '2��_�v�Z�p__�d�����ϒP��

            .AppendLine("    [2gatu_keisanyou_koj_hantei_ritu], ")         '2��_�v�Z�p_�H�����藦
            .AppendLine("    [2gatu_keisanyou_koj_jyuchuu_ritu], ")        '2��_�v�Z�p_�H���󒍗�
            .AppendLine("    [2gatu_keisanyou_tyoku_koj_ritu], ")          '2��_�v�Z�p_���H����
            .AppendLine("    [2gatu_keikaku_kensuu], ")                    '2��_�v�挏��
            .AppendLine("    [2gatu_keikaku_kingaku], ")                   '2��_�v����z
            .AppendLine("    [2gatu_keikaku_arari], ")                     '2��_�v��e��

            .AppendLine("     [3gatu_keisanyou_uri_heikin_tanka], ")          '3��_�v�Z�p__���㕽�ϒP��
            .AppendLine("     [3gatu_keisanyou_siire_heikin_tanka], ")        '3��_�v�Z�p__�d�����ϒP��

            .AppendLine("    [3gatu_keisanyou_koj_hantei_ritu], ")         '3��_�v�Z�p_�H�����藦
            .AppendLine("    [3gatu_keisanyou_koj_jyuchuu_ritu], ")        '3��_�v�Z�p_�H���󒍗�
            .AppendLine("    [3gatu_keisanyou_tyoku_koj_ritu], ")          '3��_�v�Z�p_���H����
            .AppendLine("    [3gatu_keikaku_kensuu], ")                    '3��_�v�挏��
            .AppendLine("    [3gatu_keikaku_kingaku], ")                   '3��_�v����z
            .AppendLine("    [3gatu_keikaku_arari], ")                     '3��_�v��e��
            .AppendLine("    [keikaku_kakutei_flg], ")                     '�v��m��FLG
            .AppendLine("    [keikaku_kakutei_id], ")                      '�v��m���ID
            .AppendLine("    [keikaku_kakutei_datetime], ")                '�v��m�����
            .AppendLine("    [kakutei_minaosi_id], ")                      '�m�茩������ID
            .AppendLine("    [kakutei_minaosi_datetime], ")                '�m�茩��������
            .AppendLine("    [keikaku_minaosi_flg], ")                     '�v�挩����FLG
            .AppendLine("    [keikaku_huhen_flg], ")                       '�v��l�s��FLG
            .AppendLine("    [add_login_user_id], ")                       '�o�^���O�C�����[�U�[ID
            .AppendLine("    [upd_login_user_id], ")                       '�X�V���O�C�����[�U�[ID
            .AppendLine("    [upd_datetime] ")                             '�X�V����
            .AppendLine("    )VALUES( ")
            .AppendLine("    @keikaku_nendo, ")
            .AppendLine("    @add_datetime, ")
            .AppendLine("    @busyo_cd, ")
            .AppendLine("    @keikaku_kanri_syouhin_cd, ")
            .AppendLine("    @siten_mei, ")
            .AppendLine("    @bunbetu_cd, ")

            .AppendLine("     @4gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @4gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @4gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @4gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @4gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @4gatu_keikaku_kensuu, ")
            .AppendLine("    @4gatu_keikaku_kingaku, ")
            .AppendLine("    @4gatu_keikaku_arari, ")

            .AppendLine("     @5gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @5gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @5gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @5gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @5gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @5gatu_keikaku_kensuu, ")
            .AppendLine("    @5gatu_keikaku_kingaku, ")
            .AppendLine("    @5gatu_keikaku_arari, ")

            .AppendLine("     @6gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @6gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @6gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @6gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @6gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @6gatu_keikaku_kensuu, ")
            .AppendLine("    @6gatu_keikaku_kingaku, ")
            .AppendLine("    @6gatu_keikaku_arari, ")

            .AppendLine("     @7gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @7gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @7gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @7gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @7gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @7gatu_keikaku_kensuu, ")
            .AppendLine("    @7gatu_keikaku_kingaku, ")
            .AppendLine("    @7gatu_keikaku_arari, ")

            .AppendLine("     @8gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @8gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @8gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @8gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @8gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @8gatu_keikaku_kensuu, ")
            .AppendLine("    @8gatu_keikaku_kingaku, ")
            .AppendLine("    @8gatu_keikaku_arari, ")

            .AppendLine("     @9gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @9gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @9gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @9gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @9gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @9gatu_keikaku_kensuu, ")
            .AppendLine("    @9gatu_keikaku_kingaku, ")
            .AppendLine("    @9gatu_keikaku_arari, ")

            .AppendLine("     @10gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @10gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @10gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @10gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @10gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @10gatu_keikaku_kensuu, ")
            .AppendLine("    @10gatu_keikaku_kingaku, ")
            .AppendLine("    @10gatu_keikaku_arari, ")

            .AppendLine("     @11gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @11gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @11gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @11gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @11gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @11gatu_keikaku_kensuu, ")
            .AppendLine("    @11gatu_keikaku_kingaku, ")
            .AppendLine("    @11gatu_keikaku_arari, ")

            .AppendLine("     @12gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @12gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @12gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @12gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @12gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @12gatu_keikaku_kensuu, ")
            .AppendLine("    @12gatu_keikaku_kingaku, ")
            .AppendLine("    @12gatu_keikaku_arari, ")

            .AppendLine("     @1gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @1gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @1gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @1gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @1gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @1gatu_keikaku_kensuu, ")
            .AppendLine("    @1gatu_keikaku_kingaku, ")
            .AppendLine("    @1gatu_keikaku_arari, ")

            .AppendLine("     @2gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @2gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @2gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @2gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @2gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @2gatu_keikaku_kensuu, ")
            .AppendLine("    @2gatu_keikaku_kingaku, ")
            .AppendLine("    @2gatu_keikaku_arari, ")

            .AppendLine("     @3gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @3gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @3gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @3gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @3gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @3gatu_keikaku_kensuu, ")
            .AppendLine("    @3gatu_keikaku_kingaku, ")
            .AppendLine("    @3gatu_keikaku_arari, ")
            .AppendLine("    NULL, ")
            .AppendLine("    NULL, ")
            .AppendLine("    NULL, ")
            .AppendLine("    NULL, ")
            .AppendLine("    NULL, ")
            .AppendLine("    NULL, ")
            .AppendLine("    NULL, ")
            .AppendLine("    @add_login_user_id, ")
            .AppendLine("    @add_login_user_id, ")
            .AppendLine("    GETDATE() ")
            .AppendLine(" ) ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, drValue("keikaku_nendo")))                                                 '�v��_�N�x
        paramList.Add(MakeParam("@add_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSyoriDatetime))))                          '�o�^����
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, drValue("busyo_cd")))                                                        '��������
        paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, drValue("keikaku_kanri_syouhin_cd")))                        '�v��Ǘ�_���i�R�[�h
        paramList.Add(MakeParam("@siten_mei", SqlDbType.VarChar, 40, drValue("siten_mei")))                                                     '�x�X��
        paramList.Add(MakeParam("@bunbetu_cd", SqlDbType.VarChar, 3, drValue("bunbetu_cd")))                                                    '���ʃR�[�h

        paramList.Add(MakeParam("@4gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("4gatu_keisanyou_uri_heikin_tanka")))        '4��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@4gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("4gatu_keisanyou_siire_heikin_tanka")))    '4��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@4gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("4gatu_keisanyou_koj_hantei_ritu")))          '4��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@4gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("4gatu_keisanyou_koj_jyuchuu_ritu")))        '4��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@4gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("4gatu_keisanyou_tyoku_koj_ritu")))            '4��_�v�Z�p_���H����
        paramList.Add(MakeParam("@4gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("4gatu_keikaku_kensuu")))                                '4��_�v�挏��
        paramList.Add(MakeParam("@4gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("4gatu_keikaku_kingaku")))                              '4��_�v����z
        paramList.Add(MakeParam("@4gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("4gatu_keikaku_arari")))                                  '4��_�v��e��

        paramList.Add(MakeParam("@5gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("5gatu_keisanyou_uri_heikin_tanka")))        '5��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@5gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("5gatu_keisanyou_siire_heikin_tanka")))    '5��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@5gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("5gatu_keisanyou_koj_hantei_ritu")))          '5��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@5gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("5gatu_keisanyou_koj_jyuchuu_ritu")))        '5��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@5gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("5gatu_keisanyou_tyoku_koj_ritu")))            '5��_�v�Z�p_���H����
        paramList.Add(MakeParam("@5gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("5gatu_keikaku_kensuu")))                                '5��_�v�挏��
        paramList.Add(MakeParam("@5gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("5gatu_keikaku_kingaku")))                              '5��_�v����z
        paramList.Add(MakeParam("@5gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("5gatu_keikaku_arari")))                                  '5��_�v��e��

        paramList.Add(MakeParam("@6gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("6gatu_keisanyou_uri_heikin_tanka")))        '6��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@6gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("6gatu_keisanyou_siire_heikin_tanka")))    '6��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@6gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("6gatu_keisanyou_koj_hantei_ritu")))          '6��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@6gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("6gatu_keisanyou_koj_jyuchuu_ritu")))        '6��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@6gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("6gatu_keisanyou_tyoku_koj_ritu")))            '6��_�v�Z�p_���H����
        paramList.Add(MakeParam("@6gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("6gatu_keikaku_kensuu")))                                '6��_�v�挏��
        paramList.Add(MakeParam("@6gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("6gatu_keikaku_kingaku")))                              '6��_�v����z
        paramList.Add(MakeParam("@6gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("6gatu_keikaku_arari")))                                  '6��_�v��e��

        paramList.Add(MakeParam("@7gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("7gatu_keisanyou_uri_heikin_tanka")))        '7��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@7gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("7gatu_keisanyou_siire_heikin_tanka")))    '7��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@7gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("7gatu_keisanyou_koj_hantei_ritu")))          '7��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@7gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("7gatu_keisanyou_koj_jyuchuu_ritu")))        '7��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@7gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("7gatu_keisanyou_tyoku_koj_ritu")))            '7��_�v�Z�p_���H����
        paramList.Add(MakeParam("@7gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("7gatu_keikaku_kensuu")))                                '7��_�v�挏��
        paramList.Add(MakeParam("@7gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("7gatu_keikaku_kingaku")))                              '7��_�v����z
        paramList.Add(MakeParam("@7gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("7gatu_keikaku_arari")))                                  '7��_�v��e��

        paramList.Add(MakeParam("@8gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("8gatu_keisanyou_uri_heikin_tanka")))        '8��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@8gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("8gatu_keisanyou_siire_heikin_tanka")))    '8��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@8gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 12, drValue("8gatu_keisanyou_koj_hantei_ritu")))         '8��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@8gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 12, drValue("8gatu_keisanyou_koj_jyuchuu_ritu")))       '8��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@8gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 12, drValue("8gatu_keisanyou_tyoku_koj_ritu")))           '8��_�v�Z�p_���H����
        paramList.Add(MakeParam("@8gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("8gatu_keikaku_kensuu")))                                '8��_�v�挏��
        paramList.Add(MakeParam("@8gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("8gatu_keikaku_kingaku")))                              '8��_�v����z
        paramList.Add(MakeParam("@8gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("8gatu_keikaku_arari")))                                  '8��_�v��e��

        paramList.Add(MakeParam("@9gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("9gatu_keisanyou_uri_heikin_tanka")))        '9��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@9gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("9gatu_keisanyou_siire_heikin_tanka")))    '9��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@9gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("9gatu_keisanyou_koj_hantei_ritu")))          '9��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@9gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("9gatu_keisanyou_koj_jyuchuu_ritu")))        '9��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@9gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("9gatu_keisanyou_tyoku_koj_ritu")))            '9��_�v�Z�p_���H����
        paramList.Add(MakeParam("@9gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("9gatu_keikaku_kensuu")))                                '9��_�v�挏��
        paramList.Add(MakeParam("@9gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("9gatu_keikaku_kingaku")))                              '9��_�v����z
        paramList.Add(MakeParam("@9gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("9gatu_keikaku_arari")))                                  '9��_�v��e��

        paramList.Add(MakeParam("@10gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("10gatu_keisanyou_uri_heikin_tanka")))        '10��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@10gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("10gatu_keisanyou_siire_heikin_tanka")))    '10��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@10gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("10gatu_keisanyou_koj_hantei_ritu")))        '10��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@10gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("10gatu_keisanyou_koj_jyuchuu_ritu")))      '10��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@10gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("10gatu_keisanyou_tyoku_koj_ritu")))          '10��_�v�Z�p_���H����
        paramList.Add(MakeParam("@10gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("10gatu_keikaku_kensuu")))                              '10��_�v�挏��
        paramList.Add(MakeParam("@10gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("10gatu_keikaku_kingaku")))                            '10��_�v����z
        paramList.Add(MakeParam("@10gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("10gatu_keikaku_arari")))                                '10��_�v��e��

        paramList.Add(MakeParam("@11gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("11gatu_keisanyou_uri_heikin_tanka")))        '11��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@11gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("11gatu_keisanyou_siire_heikin_tanka")))    '11��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@11gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("11gatu_keisanyou_koj_hantei_ritu")))        '11��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@11gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("11gatu_keisanyou_koj_jyuchuu_ritu")))      '11��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@11gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("11gatu_keisanyou_tyoku_koj_ritu")))          '11��_�v�Z�p_���H����
        paramList.Add(MakeParam("@11gatu_keikaku_kensuu", SqlDbType.BigInt, 8, drValue("11gatu_keikaku_kensuu")))                               '11��_�v�挏��
        paramList.Add(MakeParam("@11gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("11gatu_keikaku_kingaku")))                            '11��_�v����z
        paramList.Add(MakeParam("@11gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("11gatu_keikaku_arari")))                                '11��_�v��e��

        paramList.Add(MakeParam("@12gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("12gatu_keisanyou_uri_heikin_tanka")))        '12��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@12gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("12gatu_keisanyou_siire_heikin_tanka")))    '12��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@12gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("12gatu_keisanyou_koj_hantei_ritu")))        '12��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@12gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("12gatu_keisanyou_koj_jyuchuu_ritu")))      '12��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@12gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("12gatu_keisanyou_tyoku_koj_ritu")))          '12��_�v�Z�p_���H����
        paramList.Add(MakeParam("@12gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("12gatu_keikaku_kensuu")))                              '12��_�v�挏��
        paramList.Add(MakeParam("@12gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("12gatu_keikaku_kingaku")))                            '12��_�v����z
        paramList.Add(MakeParam("@12gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("12gatu_keikaku_arari")))                                '12��_�v��e��

        paramList.Add(MakeParam("@1gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("1gatu_keisanyou_uri_heikin_tanka")))        '1��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@1gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("1gatu_keisanyou_siire_heikin_tanka")))    '1��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@1gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("1gatu_keisanyou_koj_hantei_ritu")))          '1��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@1gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("1gatu_keisanyou_koj_jyuchuu_ritu")))        '1��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@1gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("1gatu_keisanyou_tyoku_koj_ritu")))            '1��_�v�Z�p_���H����
        paramList.Add(MakeParam("@1gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("1gatu_keikaku_kensuu")))                                '1��_�v�挏��
        paramList.Add(MakeParam("@1gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("1gatu_keikaku_kingaku")))                              '1��_�v����z
        paramList.Add(MakeParam("@1gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("1gatu_keikaku_arari")))                                  '1��_�v��e��

        paramList.Add(MakeParam("@2gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("2gatu_keisanyou_uri_heikin_tanka")))        '2��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@2gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("2gatu_keisanyou_siire_heikin_tanka")))    '2��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@2gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("2gatu_keisanyou_koj_hantei_ritu")))          '2��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@2gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("2gatu_keisanyou_koj_jyuchuu_ritu")))        '2��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@2gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("2gatu_keisanyou_tyoku_koj_ritu")))            '2��_�v�Z�p_���H����
        paramList.Add(MakeParam("@2gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("2gatu_keikaku_kensuu")))                                '2��_�v�挏��
        paramList.Add(MakeParam("@2gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("2gatu_keikaku_kingaku")))                              '2��_�v����z
        paramList.Add(MakeParam("@2gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("2gatu_keikaku_arari")))                                  '2��_�v��e��

        paramList.Add(MakeParam("@3gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("3gatu_keisanyou_uri_heikin_tanka")))        '3��_�v�Z�p__���㕽�ϒP��
        paramList.Add(MakeParam("@3gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("3gatu_keisanyou_siire_heikin_tanka")))    '3��_�v�Z�p__�d�����ϒP��

        paramList.Add(MakeParam("@3gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("3gatu_keisanyou_koj_hantei_ritu")))          '3��_�v�Z�p_�H�����藦
        paramList.Add(MakeParam("@3gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("3gatu_keisanyou_koj_jyuchuu_ritu")))        '3��_�v�Z�p_�H���󒍗�
        paramList.Add(MakeParam("@3gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("3gatu_keisanyou_tyoku_koj_ritu")))            '3��_�v�Z�p_���H����
        paramList.Add(MakeParam("@3gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("3gatu_keikaku_kensuu")))                                '3��_�v�挏��
        paramList.Add(MakeParam("@3gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("3gatu_keikaku_kingaku")))                              '3��_�v����z
        paramList.Add(MakeParam("@3gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("3gatu_keikaku_arari")))                                  '3��_�v��e��
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strLoginUserId))                                                   '�o�^���O�C�����[�U�[ID

        '�o�^���s
        insCount = ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString(), paramList.ToArray)

        If insCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' FC�p�\�茩���Ǘ��e�[�u���ւ̃f�[�^�o�^
    ''' </summary>
    ''' <param name="drValue">�o�^�f�[�^���R�[�h</param>
    ''' <param name="strLoginUserId">���O�C�����[�U�[ID</param>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/27 P-44979 ���h�m �V�K�쐬</para>																															
    ''' </history>
    Public Function InsFCYoteiMikomiKanriData(ByVal drValue As DataRow, _
                                              ByVal strLoginUserId As String, _
                                              ByVal strSyoriDatetime As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, drValue, strLoginUserId, strSyoriDatetime)

        Dim sqlBuffer As New System.Text.StringBuilder
        Dim insCount As Integer

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" INSERT INTO t_fc_yotei_mikomi_kanri WITH(UPDLOCK)( ")
            .AppendLine("     [keikaku_nendo], ")                            '�v��_�N�x
            .AppendLine("     [add_datetime], ")                             '�o�^����
            .AppendLine("     [busyo_cd], ")                                 '��������
            .AppendLine("     [keikaku_kanri_syouhin_cd], ")                 '�v��Ǘ�_���i�R�[�h
            .AppendLine("     [siten_mei], ")                                '�x�X��
            .AppendLine("     [bunbetu_cd], ")                               '���ʃR�[�h
            .AppendLine("     [4gatu_mikomi_kensuu], ")                      '4��_��������
            .AppendLine("     [4gatu_mikomi_kingaku], ")                     '4��_�������z
            .AppendLine("     [4gatu_mikomi_arari], ")                       '4��_�����e��
            .AppendLine("     [5gatu_mikomi_kensuu], ")                      '5��_��������
            .AppendLine("     [5gatu_mikomi_kingaku], ")                     '5��_�������z
            .AppendLine("     [5gatu_mikomi_arari], ")                       '5��_�����e��
            .AppendLine("     [6gatu_mikomi_kensuu], ")                      '6��_��������
            .AppendLine("     [6gatu_mikomi_kingaku], ")                     '6��_�������z
            .AppendLine("     [6gatu_mikomi_arari], ")                       '6��_�����e��
            .AppendLine("     [7gatu_mikomi_kensuu], ")                      '7��_��������
            .AppendLine("     [7gatu_mikomi_kingaku], ")                     '7��_�������z
            .AppendLine("     [7gatu_mikomi_arari], ")                       '7��_�����e��
            .AppendLine("     [8gatu_mikomi_kensuu], ")                      '8��_��������
            .AppendLine("     [8gatu_mikomi_kingaku], ")                     '8��_�������z
            .AppendLine("     [8gatu_mikomi_arari], ")                       '8��_�����e��
            .AppendLine("     [9gatu_mikomi_kensuu], ")                      '9��_��������
            .AppendLine("     [9gatu_mikomi_kingaku], ")                     '9��_�������z
            .AppendLine("     [9gatu_mikomi_arari], ")                       '9��_�����e��
            .AppendLine("     [10gatu_mikomi_kensuu], ")                     '10��_��������
            .AppendLine("     [10gatu_mikomi_kingaku], ")                    '10��_�������z
            .AppendLine("     [10gatu_mikomi_arari], ")                      '10��_�����e��
            .AppendLine("     [11gatu_mikomi_kensuu], ")                     '11��_��������
            .AppendLine("     [11gatu_mikomi_kingaku], ")                    '11��_�������z
            .AppendLine("     [11gatu_mikomi_arari], ")                      '11��_�����e��
            .AppendLine("     [12gatu_mikomi_kensuu], ")                     '12��_��������
            .AppendLine("     [12gatu_mikomi_kingaku], ")                    '12��_�������z
            .AppendLine("     [12gatu_mikomi_arari], ")                      '12��_�����e��
            .AppendLine("     [1gatu_mikomi_kensuu], ")                      '1��_��������
            .AppendLine("     [1gatu_mikomi_kingaku], ")                     '1��_�������z
            .AppendLine("     [1gatu_mikomi_arari], ")                       '1��_�����e��
            .AppendLine("     [2gatu_mikomi_kensuu], ")                      '2��_��������
            .AppendLine("     [2gatu_mikomi_kingaku], ")                     '2��_�������z
            .AppendLine("     [2gatu_mikomi_arari], ")                       '2��_�����e��
            .AppendLine("     [3gatu_mikomi_kensuu], ")                      '3��_��������
            .AppendLine("     [3gatu_mikomi_kingaku], ")                     '3��_�������z
            .AppendLine("     [3gatu_mikomi_arari], ")                       '3��_�����e��
            .AppendLine("     [add_login_user_id], ")                        '�o�^���O�C�����[�U�[ID
            .AppendLine("     [upd_login_user_id], ")                        '�X�V���O�C�����[�U�[ID
            .AppendLine("     [upd_datetime] ")                              '�X�V����
            .AppendLine("    )VALUES( ")
            .AppendLine("     @keikaku_nendo, ")
            .AppendLine("     @add_datetime, ")
            .AppendLine("     @busyo_cd, ")
            .AppendLine("     @keikaku_kanri_syouhin_cd, ")
            .AppendLine("     @siten_mei, ")
            .AppendLine("     @bunbetu_cd, ")
            .AppendLine("     @4gatu_mikomi_kensuu, ")
            .AppendLine("     @4gatu_mikomi_kingaku, ")
            .AppendLine("     @4gatu_mikomi_arari, ")
            .AppendLine("     @5gatu_mikomi_kensuu, ")
            .AppendLine("     @5gatu_mikomi_kingaku, ")
            .AppendLine("     @5gatu_mikomi_arari, ")
            .AppendLine("     @6gatu_mikomi_kensuu, ")
            .AppendLine("     @6gatu_mikomi_kingaku, ")
            .AppendLine("     @6gatu_mikomi_arari, ")
            .AppendLine("     @7gatu_mikomi_kensuu, ")
            .AppendLine("     @7gatu_mikomi_kingaku, ")
            .AppendLine("     @7gatu_mikomi_arari, ")
            .AppendLine("     @8gatu_mikomi_kensuu, ")
            .AppendLine("     @8gatu_mikomi_kingaku, ")
            .AppendLine("     @8gatu_mikomi_arari, ")
            .AppendLine("     @9gatu_mikomi_kensuu, ")
            .AppendLine("     @9gatu_mikomi_kingaku, ")
            .AppendLine("     @9gatu_mikomi_arari, ")
            .AppendLine("     @10gatu_mikomi_kensuu, ")
            .AppendLine("     @10gatu_mikomi_kingaku, ")
            .AppendLine("     @10gatu_mikomi_arari, ")
            .AppendLine("     @11gatu_mikomi_kensuu, ")
            .AppendLine("     @11gatu_mikomi_kingaku, ")
            .AppendLine("     @11gatu_mikomi_arari, ")
            .AppendLine("     @12gatu_mikomi_kensuu, ")
            .AppendLine("     @12gatu_mikomi_kingaku, ")
            .AppendLine("     @12gatu_mikomi_arari, ")
            .AppendLine("     @1gatu_mikomi_kensuu, ")
            .AppendLine("     @1gatu_mikomi_kingaku, ")
            .AppendLine("     @1gatu_mikomi_arari, ")
            .AppendLine("     @2gatu_mikomi_kensuu, ")
            .AppendLine("     @2gatu_mikomi_kingaku, ")
            .AppendLine("     @2gatu_mikomi_arari, ")
            .AppendLine("     @3gatu_mikomi_kensuu, ")
            .AppendLine("     @3gatu_mikomi_kingaku, ")
            .AppendLine("     @3gatu_mikomi_arari, ")
            .AppendLine("     @add_login_user_id, ")
            .AppendLine("     @add_login_user_id, ")
            .AppendLine("     GETDATE() ")
            .AppendLine(" 	) ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, drValue("keikaku_nendo")))                         '�v��N�x(YYYY)
        paramList.Add(MakeParam("@add_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSyoriDatetime))))  '�o�^����
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, drValue("busyo_cd")))                                '��������
        paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, drValue("keikaku_kanri_syouhin_cd")))       '�v��Ǘ�_���i�R�[�h
        paramList.Add(MakeParam("@siten_mei", SqlDbType.VarChar, 40, drValue("siten_mei")))                             '�x�X��
        paramList.Add(MakeParam("@bunbetu_cd", SqlDbType.VarChar, 3, drValue("bunbetu_cd")))                            '���ʃR�[�h
        paramList.Add(MakeParam("@4gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("4gatu_mikomi_kensuu")))          '4��_��������
        paramList.Add(MakeParam("@4gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("4gatu_mikomi_kingaku")))        '4��_�������z
        paramList.Add(MakeParam("@4gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("4gatu_mikomi_arari")))            '4��_�����e��
        paramList.Add(MakeParam("@5gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("5gatu_mikomi_kensuu")))          '5��_��������
        paramList.Add(MakeParam("@5gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("5gatu_mikomi_kingaku")))        '5��_�������z
        paramList.Add(MakeParam("@5gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("5gatu_mikomi_arari")))            '5��_�����e��
        paramList.Add(MakeParam("@6gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("6gatu_mikomi_kensuu")))          '6��_��������
        paramList.Add(MakeParam("@6gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("6gatu_mikomi_kingaku")))        '6��_�������z
        paramList.Add(MakeParam("@6gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("6gatu_mikomi_arari")))            '6��_�����e��
        paramList.Add(MakeParam("@7gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("7gatu_mikomi_kensuu")))          '7��_��������
        paramList.Add(MakeParam("@7gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("7gatu_mikomi_kingaku")))        '7��_�������z
        paramList.Add(MakeParam("@7gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("7gatu_mikomi_arari")))            '7��_�����e��
        paramList.Add(MakeParam("@8gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("8gatu_mikomi_kensuu")))          '8��_��������
        paramList.Add(MakeParam("@8gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("8gatu_mikomi_kingaku")))        '8��_�������z
        paramList.Add(MakeParam("@8gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("8gatu_mikomi_arari")))            '8��_�����e��
        paramList.Add(MakeParam("@9gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("9gatu_mikomi_kensuu")))          '9��_��������
        paramList.Add(MakeParam("@9gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("9gatu_mikomi_kingaku")))        '9��_�������z
        paramList.Add(MakeParam("@9gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("9gatu_mikomi_arari")))            '9��_�����e��
        paramList.Add(MakeParam("@10gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("10gatu_mikomi_kensuu")))        '10��_��������
        paramList.Add(MakeParam("@10gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("10gatu_mikomi_kingaku")))      '10��_�������z
        paramList.Add(MakeParam("@10gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("10gatu_mikomi_arari")))          '10��_�����e��
        paramList.Add(MakeParam("@11gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("11gatu_mikomi_kensuu")))        '11��_��������
        paramList.Add(MakeParam("@11gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("11gatu_mikomi_kingaku")))      '11��_�������z
        paramList.Add(MakeParam("@11gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("11gatu_mikomi_arari")))          '11��_�����e��
        paramList.Add(MakeParam("@12gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("12gatu_mikomi_kensuu")))        '12��_��������
        paramList.Add(MakeParam("@12gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("12gatu_mikomi_kingaku")))      '12��_�������z
        paramList.Add(MakeParam("@12gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("12gatu_mikomi_arari")))          '12��_�����e��
        paramList.Add(MakeParam("@1gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("1gatu_mikomi_kensuu")))          '1��_��������
        paramList.Add(MakeParam("@1gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("1gatu_mikomi_kingaku")))        '1��_�������z
        paramList.Add(MakeParam("@1gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("1gatu_mikomi_arari")))            '1��_�����e��
        paramList.Add(MakeParam("@2gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("2gatu_mikomi_kensuu")))          '2��_��������
        paramList.Add(MakeParam("@2gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("2gatu_mikomi_kingaku")))        '2��_�������z
        paramList.Add(MakeParam("@2gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("2gatu_mikomi_arari")))            '2��_�����e��
        paramList.Add(MakeParam("@3gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("3gatu_mikomi_kensuu")))          '3��_��������
        paramList.Add(MakeParam("@3gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("3gatu_mikomi_kingaku")))        '3��_�������z
        paramList.Add(MakeParam("@3gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("3gatu_mikomi_arari")))            '3��_�����e��
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strLoginUserId))                           '�o�^���O�C�����[�U�[ID

        '�o�^���s
        insCount = SQLHelper.ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, paramList.ToArray())

        If insCount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' �A�b�v���[�h�Ǘ��e�[�u���ւ̃f�[�^�o�^
    ''' </summary>
    ''' <param name="strSyoriDatetime">��������</param>
    ''' <param name="strNyuuryokuFileMei">���̓t�@�C����</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strErrorUmu">�G���[�L��</param>
    ''' <param name="strLoginUserId">���O�C�����[�U�[ID</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Function InsInputKanri(ByVal strSyoriDatetime As String, _
                                  ByVal strNyuuryokuFileMei As String, _
                                  ByVal strEdiJouhouSakuseiDate As String, _
                                  ByVal strErrorUmu As Integer, _
                                  ByVal strLoginUserId As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                strSyoriDatetime, strNyuuryokuFileMei, strEdiJouhouSakuseiDate, strErrorUmu, strLoginUserId)

        '�߂�l
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("INSERT INTO  ")
            .AppendLine("	t_upload_kanri WITH(UPDLOCK)")
            .AppendLine("	( ")
            .AppendLine("		syori_datetime ")                   '��������
            .AppendLine("		,nyuuryoku_file_mei ")              '���̓t�@�C����
            .AppendLine("		,edi_jouhou_sakusei_date ")         'EDI���쐬��
            .AppendLine("		,error_umu ")                       '�G���[�L��
            .AppendLine("		,file_kbn ")                        '�t�@�C���敪
            .AppendLine("		,add_login_user_id ")               '�o�^���O�C�����[�U�[ID
            .AppendLine("		,add_datetime ")                    '�o�^����
            .AppendLine("		,upd_login_user_id ")               '�X�V���O�C�����[�U�[ID
            .AppendLine("		,upd_datetime ")                    '�X�V����
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@syori_datetime ")
            .AppendLine("	,@nyuuryoku_file_mei ")
            .AppendLine("	,@edi_jouhou_sakusei_date ")
            .AppendLine("	,@error_umu ")
            .AppendLine("	,@file_kbn ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSyoriDatetime))))    '��������
        paramList.Add(MakeParam("@nyuuryoku_file_mei", SqlDbType.VarChar, 128, strNyuuryokuFileMei))                        '���̓t�@�C����
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))                'EDI���쐬��
        paramList.Add(MakeParam("@error_umu", SqlDbType.Int, 1, strErrorUmu))                                               '�G���[�L��
        paramList.Add(MakeParam("@file_kbn", SqlDbType.Int, 2, 2))                                                          '�t�@�C���敪
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strLoginUserId))                               '���O�C�����[�U�[ID

        '�o�^���s
        InsCount = ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        If InsCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Debug�pSQL�����擾
    ''' </summary>
    ''' <param name="strSql">�������ꂽSQL��</param>
    ''' <param name="ParamList">�p�����[�^���X�g</param>
    ''' <returns>Debug�pSQL��</returns>
    ''' <remarks></remarks>
    Public Function GetDebugSql(ByVal strSql As String, ByVal ParamList As List(Of SqlClient.SqlParameter)) As String
        Dim i As Integer
        Dim result As String = String.Empty
        Dim objSqlParam As SqlClient.SqlParameter
        Dim strParamName As String
        Dim strParamValue As String

        ''�p�����[�^�̖��O���\�[�g����
        'Call ParamList.Sort()

        For i = 0 To ParamList.Count - 1
            objSqlParam = ParamList(i)
            strParamName = objSqlParam.ParameterName
            strParamValue = objSqlParam.Value.ToString
            strSql = strSql.Replace(strParamName, "'" & strParamValue & "'")
        Next

        result = strSql

        Return result
    End Function

    ''' <summary>
    ''' ��A���̃f�[�^���擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/10/14�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetHorennSou(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                strKameitenCd)
        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	uccrpdev")
            .AppendLine("	,uccrpseq")
            .AppendLine("FROM ")
            .AppendLine("	sfamt_usrcorp_mvr")
            .AppendLine("WHERE ")
            .AppendLine("	ucdelflg = '0'")
            .AppendLine("	AND len(ISNULL(@kameitenCd ,'')) = 8")
            .AppendLine("	AND uccrpcod = @kameitenCd")
        End With
        '�o�����^
        paramList.Add(MakeParam("@kameitenCd", SqlDbType.VarChar, 16, strKameitenCd))    '�����X�R�[�h
        '�������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtHorennSou", paramList.ToArray())

        Return dsReturn.Tables("dtHorennSou")

    End Function
End Class
