Imports System
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �����f�[�^�̎擾�N���X
''' </summary>
''' <remarks></remarks>
Public Class SeikyuuDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�����o�ϐ�"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim SLogic As New StringLogic
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "�������f�[�^�̎擾"

    ''' <summary>
    ''' (�ߋ�)�������ꗗ���/�����f�[�^���擾���܂�
    ''' </summary>
    ''' <param name="keyRec">����KEY���R�[�h�N���X</param>
    ''' <param name="emType">�����f�[�^�̌����^�C�v</param>
    ''' <returns>�����f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetSearchSeikyuusyoTbl(ByVal keyRec As SeikyuuDataKeyRecord, ByVal emType As EarthEnum.emSeikyuuSearchType) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSearchSeikyuusyoTbl", keyRec, emType)

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder
        '�p�����[�^
        Dim cmdParams() As SqlParameter = Me.GetSeikyuuSqlCmnParams(keyRec)

        'SELECT��
        Dim strCmnSelect As String = Me.GetSearchSeikyuusyoSqlSelect()
        'TABLE��
        Dim strCmnTable As String = Me.GetCmnSqlTable(keyRec, emType)
        'WHERE��
        Dim strCmnWhere As String = Me.GetCmnSqlWhere(keyRec, emType)
        'ORDER BY��
        Dim strCmnOrderBy As String = Me.GetCmnSqlOrderBy()

        '****************
        '* SELECT����
        '****************
        cmdTextSb.Append(strCmnSelect)

        '****************
        '* TABLE����
        '****************
        cmdTextSb.Append(strCmnTable)

        '****************
        '* WHERE��
        '****************
        cmdTextSb.Append(strCmnWhere)

        '***********************************************************************
        ' ORDER BY��i���������s����������NO�j
        '***********************************************************************
        cmdTextSb.Append(strCmnOrderBy)

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)
    End Function

    ''' <summary>
    ''' (�ߋ�)�������ꗗ���/�����f�[�^[CSV�o�͗p]���擾���܂�
    ''' </summary>
    ''' <param name="keyRec">�����f�[�^Key���R�[�h</param>
    ''' <param name="emType">�����f�[�^�̌����^�C�v</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetSearchSeikyuuDataCsv(ByVal keyRec As SeikyuuDataKeyRecord, ByVal emType As EarthEnum.emSeikyuuSearchType) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSearchSeikyuuDataCsv", keyRec, emType)

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder

        Dim cmdParams() As SqlParameter = Me.GetSeikyuuSqlCmnParams(keyRec)

        'SELECT��
        Dim strCmnSelect As String = Me.GetCmnSqlSelectCsv()
        'TABLE��
        Dim strCmnTable As String = Me.GetCmnSqlTableCsv(keyRec, emType)
        'WHERE��
        Dim strCmnWhere As String = Me.GetCmnSqlWhere(keyRec, emType)
        'ORDER BY��
        Dim strCmnOrderBy As String = Me.GetCmnSqlOrderBy()

        '****************
        '* SELECT����
        '****************
        cmdTextSb.Append(strCmnSelect)

        '****************
        '* TABLE����
        '****************
        cmdTextSb.Append(strCmnTable)

        '****************
        '* WHERE��
        '****************
        cmdTextSb.Append(strCmnWhere)
        cmdTextSb.Append(" AND SK.seikyuusyo_no IN ('" & keyRec.ArrSeikyuuSakiNo.Replace(EarthConst.SEP_STRING, "','") & "') ") '�w��̐�����NO�݂̂𒊏o

        '***********************************************************************
        ' ORDER BY��i���������s����������NO�j
        '***********************************************************************
        cmdTextSb.Append(strCmnOrderBy)

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' �������󎚓��e�ҏW���/�����f�[�^��PK�Ŏ擾���܂�
    ''' </summary>
    ''' <param name="strSeikyuusyoNo">��L�[���ڒl</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks>�����L�[���R�[�h�N���X��KEY�ɂ��Ď擾</remarks>
    Public Function GetSeikyuusyoSyuuseiRec(ByVal strSeikyuusyoNo As String) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuusyoSyuuseiRec" _
                                                    , strSeikyuusyoNo _
                                                    )
        ' �p�����[�^
        Dim cmdParams() As SqlClient.SqlParameter

        'SELECT��
        Dim strCmnSelect As String = Me.GetCmnSqlSelectSeikyuusyoSyuusei()

        'SQL�N�G������
        Dim cmdTextSb As New StringBuilder()

        '****************
        '* SELECT����
        '****************
        cmdTextSb.Append(strCmnSelect)

        '****************
        '* TABLE����
        '****************
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("   t_seikyuu_kagami SK WITH (READCOMMITTED) ")
        '***********************************************************************
        ' VIEW������F�����於(�O������)
        '***********************************************************************
        cmdTextSb.Append("      LEFT OUTER JOIN v_seikyuu_saki_info VIW WITH (READCOMMITTED) ")
        cmdTextSb.Append("          ON SK.seikyuu_saki_cd = VIW.seikyuu_saki_cd ")
        cmdTextSb.Append("          AND SK.seikyuu_saki_brc = VIW.seikyuu_saki_brc ")
        cmdTextSb.Append("          AND SK.seikyuu_saki_kbn = VIW.seikyuu_saki_kbn ")

        '***********************************************************************
        ' ������}�X�^�F�������ߓ�(�O������)
        '***********************************************************************
        cmdTextSb.Append("      LEFT OUTER JOIN m_seikyuu_saki MSM WITH (READCOMMITTED) ")
        cmdTextSb.Append("          ON SK.seikyuu_saki_cd = MSM.seikyuu_saki_cd ")
        cmdTextSb.Append("          AND SK.seikyuu_saki_brc = MSM.seikyuu_saki_brc ")
        cmdTextSb.Append("          AND SK.seikyuu_saki_kbn = MSM.seikyuu_saki_kbn ")

        '***********************************************************************
        ' �����Ӗ��̖��׌���(�O������)
        '***********************************************************************
        cmdTextSb.Append("      LEFT OUTER JOIN ")
        cmdTextSb.Append("          ( SELECT  ")
        cmdTextSb.Append("              SM.seikyuusyo_no ")
        cmdTextSb.Append("              ,COUNT(SM.seikyuusyo_no) AS meisai_kensuu ")
        cmdTextSb.Append("          FROM ")
        cmdTextSb.Append("              t_seikyuu_meisai SM WITH (READCOMMITTED) ")
        cmdTextSb.Append("          WHERE ")
        cmdTextSb.Append("              SM.inji_taisyo_flg = 1 ")
        cmdTextSb.Append("          GROUP BY SM.seikyuusyo_no ")
        cmdTextSb.Append("          ) AS SUB ")
        cmdTextSb.Append("          ON SK.seikyuusyo_no = SUB.seikyuusyo_no ")

        '****************
        '* WHERE����
        '****************
        cmdTextSb.Append("  WHERE SK.seikyuusyo_no = " & DBprmSeikyuusyoNo)

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(DBprmSeikyuusyoNo, SqlDbType.VarChar, 15, strSeikyuusyoNo) _
        }

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)
    End Function

    ''' <summary>
    ''' �������󎚓��e�ҏW���/�������ׂ̏d���f�[�^���擾���܂�
    ''' </summary>
    ''' <param name="strSeikyuusyoNo">������No</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetDenpyouExistsCnt(ByVal strSeikyuusyoNo As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDenpyouExistsCnt" _
                                                    , strSeikyuusyoNo)
        ' �p�����[�^
        Dim cmdParams() As SqlClient.SqlParameter

        'SQL�N�G������
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      * ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_seikyuu_meisai SM ")
        cmdTextSb.Append("           INNER JOIN t_seikyuu_kagami SK ")
        cmdTextSb.Append("             ON SM.seikyuusyo_no = SK.seikyuusyo_no ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      EXISTS ")
        cmdTextSb.Append("     (SELECT ")
        cmdTextSb.Append("           * ")
        cmdTextSb.Append("      FROM ")
        cmdTextSb.Append("           t_seikyuu_meisai TSM ")
        cmdTextSb.Append("      WHERE ")
        cmdTextSb.Append("           SM.denpyou_unique_no = TSM.denpyou_unique_no ")
        cmdTextSb.Append("       AND TSM.seikyuusyo_no = " & DBprmSeikyuusyoNo)
        cmdTextSb.Append("     ) ")
        cmdTextSb.Append("  AND SK.torikesi = '0' ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(DBprmSeikyuusyoNo, SqlDbType.VarChar, 15, strSeikyuusyoNo) _
        }

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' �����f�[�^[�����s�����p]���擾���܂�
    ''' </summary>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetMihakkouCnt() As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetMihakkouCnt")

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder
        Dim cmdParams() As SqlParameter = {}

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      COUNT(SK.seikyuusyo_no) AS mihakkou_cnt ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_seikyuu_kagami SK ")
        cmdTextSb.Append("           INNER JOIN ")
        cmdTextSb.Append("               (SELECT ")
        cmdTextSb.Append("                     TSM.seikyuusyo_no ")
        cmdTextSb.Append("                FROM ")
        cmdTextSb.Append("                     t_seikyuu_meisai TSM ")
        cmdTextSb.Append("                WHERE ")
        cmdTextSb.Append("                     TSM.inji_taisyo_flg = '1' ")
        cmdTextSb.Append("                GROUP BY ")
        cmdTextSb.Append("                     TSM.seikyuusyo_no ")
        cmdTextSb.Append("               ) ")
        cmdTextSb.Append("                AS SUB ")
        cmdTextSb.Append("             ON SK.seikyuusyo_no = SUB.seikyuusyo_no ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      SK.seikyuusyo_insatu_date IS NULL ")
        cmdTextSb.Append("  AND SK.torikesi = 0 ")

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' �������ׂ̓`�[���j�[�NNO�ɕR�t���A�ŐV�̐����Ӄ��R�[�h���擾���܂�
    ''' </summary>
    ''' <param name="strDenUnqNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuusyoMaxRec(ByVal strDenUnqNo As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuusyoMaxRec", strDenUnqNo)

        ' �p�����[�^
        Dim cmdParams() As SqlClient.SqlParameter
        'SQL�N�G������
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine("SELECT")
        cmdTextSb.AppendLine("     MAX(TSK.seikyuusyo_no) seikyuusyo_no")
        cmdTextSb.AppendLine("FROM")
        cmdTextSb.AppendLine("     t_seikyuu_kagami TSK")
        cmdTextSb.AppendLine("          INNER JOIN t_seikyuu_meisai TSM")
        cmdTextSb.AppendLine("            ON TSK.seikyuusyo_no = TSM.seikyuusyo_no")
        cmdTextSb.AppendLine("WHERE")
        cmdTextSb.AppendLine("     TSK.torikesi = '0'")
        cmdTextSb.AppendLine(" AND TSM.denpyou_unique_no = " & DBprmMeisaiDenUnqNo)

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(DBprmMeisaiDenUnqNo, SqlDbType.Int, 4, strDenUnqNo) _
        }

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)
    End Function

#End Region

#Region "�������f�[�^�̍X�V"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="strSeikyuusyoNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdKagamiTorikesi(ByVal strSeikyuusyoNo As String, ByVal strLoginUserId As String) As Boolean
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdKagamiTorikesi", strSeikyuusyoNo, strLoginUserId)

        ' SQL�R�}���h�p�����[�^�ݒ�p
        Dim cmdParams() As SqlClient.SqlParameter
        ' SQL�N�G������
        Dim cmdTextSb As New StringBuilder()
        ' �X�V���ʌ���
        Dim intResult As Integer = 0

        cmdTextSb.AppendLine("UPDATE")
        cmdTextSb.AppendLine("     t_seikyuu_kagami")
        cmdTextSb.AppendLine("SET")
        cmdTextSb.AppendLine("     torikesi = 1")
        cmdTextSb.AppendLine("   , upd_login_user_id = " & DBparamAddLoginUserId)
        cmdTextSb.AppendLine("   , upd_datetime = " & DBparamAddDateTime)
        cmdTextSb.AppendLine("WHERE")
        cmdTextSb.AppendLine("     seikyuusyo_no = " & DBprmSeikyuusyoNo)

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(DBprmSeikyuusyoNo, SqlDbType.VarChar, 15, strSeikyuusyoNo), _
            SQLHelper.MakeParam(DBparamAddLoginUserId, SqlDbType.VarChar, 30, strLoginUserId), _
            SQLHelper.MakeParam(DBparamAddDateTime, SqlDbType.DateTime, 16, DateTime.Now)}

        intResult = ExecuteNonQuery(connStr, CommandType.Text, cmdTextSb.ToString, cmdParams)

        If intResult < 1 Then
            Return False
        End If

        Return True
    End Function

#End Region

#Region "���ʃN�G��"

#Region "SELECT��"
    ''' <summary>
    ''' �����f�[�^�擾�p��SELECT�N�G�����擾
    ''' </summary>
    ''' <returns>�����f�[�^�擾�p�̋���SELECT�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetSearchSeikyuusyoSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSearchSeikyuusyoSqlSelect")

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder
        'SELECT��
        Dim strCmnSelect As String = Me.GetCmnSqlSelect()

        '****************
        '* SELECT����
        '****************
        cmdTextSb.Append(strCmnSelect)
        cmdTextSb.Append("  ,ISNULL(WK.seikyuu_date_sai_flg, 0) AS seikyuu_date_sai_flg ")
        cmdTextSb.Append("  ,ISNULL(SUB.meisai_kensuu, 0) AS meisai_kensuu ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' �����f�[�^�擾�p�̋���SELECT�N�G�����擾[CSV]
    ''' </summary>
    ''' <returns>�����f�[�^�擾�p�̋���SELECT�N�G�����擾[CSV]</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlSelectCsv() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlSelectCsv")

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder

        '****************
        '* SELECT����
        '****************
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("    SK.seikyuusyo_no '������NO' ")
        cmdTextSb.Append("    , SK.torikesi '���' ")
        cmdTextSb.Append("    , SK.seikyuu_saki_cd '������R�[�h' ")
        cmdTextSb.Append("    , SK.seikyuu_saki_brc '������}��' ")
        cmdTextSb.Append("    , SK.seikyuu_saki_kbn '������敪' ")
        cmdTextSb.Append("    , SK.seikyuu_saki_mei '�����於' ")
        cmdTextSb.Append("    , SK.seikyuu_saki_mei2 '�����於�Q' ")
        cmdTextSb.Append("    , SK.yuubin_no '�X�֔ԍ�' ")
        cmdTextSb.Append("    , SK.jyuusyo1 '�Z��1' ")
        cmdTextSb.Append("    , SK.jyuusyo2 '�Z��2' ")
        cmdTextSb.Append("    , SK.tel_no '�d�b�ԍ�' ")
        cmdTextSb.Append("    , SK.zenkai_goseikyuu_gaku '�O��䐿���z' ")
        cmdTextSb.Append("    , SK.gonyuukin_gaku '������z' ")
        cmdTextSb.Append("    , SK.sousai_gaku '���E�z' ")
        cmdTextSb.Append("    , SK.tyousei_gaku '�����z' ")
        cmdTextSb.Append("    , SK.kurikosi_gaku '�O��J�z�c��' ")
        cmdTextSb.Append("    , SK.konkai_goseikyuu_gaku '����䐿���z' ")
        cmdTextSb.Append("    , SK.konkai_kurikosi_gaku '�J�z�c��' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, SK.konkai_kaisyuu_yotei_date, 111) '�����\���' ")
        cmdTextSb.Append("    , SK.seikyuusyo_insatu_date '�����������' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, SK.seikyuusyo_hak_date, 111) '���������s��' ")
        cmdTextSb.Append("    , SK.tantousya_mei '�S���Җ�' ")
        cmdTextSb.Append("    , SK.seikyuusyo_inji_bukken_mei_flg '�������󎚕������t���O' ")
        cmdTextSb.Append("    , SK.nyuukin_kouza_no '���������ԍ�' ")
        cmdTextSb.Append("    , SK.seikyuu_sime_date '�������ߓ�' ")
        cmdTextSb.Append("    , SK.senpou_seikyuu_sime_date '����������ߓ�' ")
        cmdTextSb.Append("    , SK.sousai_flg '���E�t���O' ")
        cmdTextSb.Append("    , SK.kaisyuu_yotei_gessuu '����\�茎��' ")
        cmdTextSb.Append("    , SK.kaisyuu_yotei_date '����\���' ")
        cmdTextSb.Append("    , SK.seikyuusyo_hittyk_date '�������K����' ")
        cmdTextSb.Append("    , SK.kaisyuu_syubetu1 '���1' ")
        cmdTextSb.Append("    , SK.kaisyuu_wariai1 '����1' ")
        cmdTextSb.Append("    , SK.kaisyuu_tegata_site_gessuu '��`�T�C�g����' ")
        cmdTextSb.Append("    , SK.kaisyuu_tegata_site_date '��`�T�C�g��' ")
        cmdTextSb.Append("    , SK.kaisyuu_seikyuusyo_yousi '�������p��' ")
        cmdTextSb.Append("    , SK.kaisyuu_seikyuusyo_yousi_hannyou_cd '�������p���ėp�R�[�h' ")
        cmdTextSb.Append("    , SK.kaisyuu_syubetu2 '���2' ")
        cmdTextSb.Append("    , SK.kaisyuu_wariai2 '����2' ")
        cmdTextSb.Append("    , SK.kaisyuu_syubetu3 '���3' ")
        cmdTextSb.Append("    , SK.kaisyuu_wariai3 '����3' ")
        cmdTextSb.Append("    , URI.denpyou_unique_no '�`�[���j�[�NNO' ")
        cmdTextSb.Append("    , URI.denpyou_no '�`�[�ԍ�' ")
        cmdTextSb.Append("    , URI.denpyou_syubetu '�`�[���' ")
        cmdTextSb.Append("    , URI.torikesi_moto_denpyou_unique_no '������`�[���j�[�NNO' ")
        cmdTextSb.Append("    , URI.kbn '�敪' ")
        cmdTextSb.Append("    , URI.bangou '�ԍ�' ")
        cmdTextSb.Append("    , URI.himoduke_cd '�R�t���R�[�h' ")
        cmdTextSb.Append("    , URI.himoduke_table_type '�R�t�����e�[�u�����' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, URI.uri_date, 111) '����N����' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, URI.denpyou_uri_date, 111) '�`�[����N����' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, URI.seikyuu_date, 111) '�����N����' ")
        cmdTextSb.Append("    , URI.seikyuu_saki_mei '����_�����於' ")
        cmdTextSb.Append("    , URI.syouhin_cd '���i�R�[�h' ")
        cmdTextSb.Append("    , URI.hinmei '�i��' ")
        cmdTextSb.Append("    , URI.suu '����' ")
        cmdTextSb.Append("    , URI.tani '�P��' ")
        cmdTextSb.Append("    , URI.tanka '�P��' ")
        cmdTextSb.Append("    , URI.syanai_genka '�Г�����' ")
        cmdTextSb.Append("    , URI.uri_gaku '������z' ")
        cmdTextSb.Append("    , URI.sotozei_gaku '�O�Ŋz' ")
        cmdTextSb.Append("    , URI.zei_kbn '�ŋ敪' ")
        cmdTextSb.Append("    , URI.add_login_user_id '�o�^���O�C�����[�U�[ID' ")
        cmdTextSb.Append("    , URI.add_login_user_name '�o�^���O�C�����[�U�[��' ")
        cmdTextSb.Append("    , URI.add_datetime '�o�^����' ")
        cmdTextSb.Append("    , URI.upd_login_user_id '�X�V���O�C�����[�U�[ID' ")
        cmdTextSb.Append("    , URI.upd_datetime '�X�V����' ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' �����f�[�^�擾�p��SELECT�N�G�����擾
    ''' </summary>
    ''' <returns>�����f�[�^�擾�p�̋���SELECT�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlSelectSeikyuusyoSyuusei() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlSelectSeikyuusyoSyuusei")

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder

        'SELECT��
        Dim strCmnSelect As String = Me.GetCmnSqlSelect()

        '****************
        '* SELECT����
        '****************
        cmdTextSb.Append(strCmnSelect)
        cmdTextSb.Append("  ,VIW.seikyuu_saki_mei AS view_seikyuu_saki_mei ")
        cmdTextSb.Append("  ,SUB.meisai_kensuu AS meisai_kensuu ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' �����f�[�^�擾�p�̋���SELECT�N�G�����擾
    ''' </summary>
    ''' <returns>�����f�[�^�擾�p�̋���SELECT�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlSelect")

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder

        '****************
        '* SELECT����
        '****************
        cmdTextSb.Append("SELECT")
        cmdTextSb.Append("  SK.seikyuusyo_no")
        cmdTextSb.Append("  ,SK.torikesi")
        cmdTextSb.Append("  ,SK.seikyuu_saki_cd")
        cmdTextSb.Append("  ,SK.seikyuu_saki_brc")
        cmdTextSb.Append("  ,SK.seikyuu_saki_kbn")
        cmdTextSb.Append("  ,SK.seikyuu_saki_mei")
        cmdTextSb.Append("  ,SK.seikyuu_saki_mei2")
        cmdTextSb.Append("  ,SK.yuubin_no")
        cmdTextSb.Append("  ,SK.jyuusyo1")
        cmdTextSb.Append("  ,SK.jyuusyo2")
        cmdTextSb.Append("  ,SK.tel_no")
        cmdTextSb.Append("  ,SK.zenkai_goseikyuu_gaku")
        cmdTextSb.Append("  ,SK.gonyuukin_gaku")
        cmdTextSb.Append("  ,SK.sousai_gaku")
        cmdTextSb.Append("  ,SK.tyousei_gaku")
        cmdTextSb.Append("  ,SK.kurikosi_gaku")
        cmdTextSb.Append("  ,SK.konkai_goseikyuu_gaku")
        cmdTextSb.Append("  ,SK.konkai_kurikosi_gaku")
        cmdTextSb.Append("  ,SK.konkai_kaisyuu_yotei_date")
        cmdTextSb.Append("  ,SK.seikyuusyo_insatu_date")
        cmdTextSb.Append("  ,SK.seikyuusyo_hak_date")
        cmdTextSb.Append("  ,SK.tantousya_mei")
        cmdTextSb.Append("  ,SK.seikyuusyo_inji_bukken_mei_flg")
        cmdTextSb.Append("  ,SK.nyuukin_kouza_no")
        cmdTextSb.Append("  ,SK.seikyuu_sime_date")
        cmdTextSb.Append("  ,SK.senpou_seikyuu_sime_date")
        cmdTextSb.Append("  ,SK.sousai_flg")
        cmdTextSb.Append("  ,SK.kaisyuu_yotei_gessuu")
        cmdTextSb.Append("  ,SK.kaisyuu_yotei_date")
        cmdTextSb.Append("  ,SK.seikyuusyo_hittyk_date")
        cmdTextSb.Append("  ,SK.kaisyuu_syubetu1")
        cmdTextSb.Append("  ,SK.kaisyuu_wariai1")
        cmdTextSb.Append("  ,SK.kaisyuu_tegata_site_gessuu")
        cmdTextSb.Append("  ,SK.kaisyuu_tegata_site_date")
        cmdTextSb.Append("  ,SK.kaisyuu_seikyuusyo_yousi")
        cmdTextSb.Append("  ,ISNULL(SK.kaisyuu_seikyuusyo_yousi_hannyou_cd,'') AS kaisyuu_seikyuusyo_yousi_hannyou_cd")
        cmdTextSb.Append("  ,CASE ")
        cmdTextSb.Append("      WHEN SK.kaisyuu_seikyuusyo_yousi_hannyou_cd IS NULL THEN 0 ")
        cmdTextSb.Append("      WHEN SUBSTRING(SK.kaisyuu_seikyuusyo_yousi_hannyou_cd, 1, 1) = '9' THEN 1 ")
        cmdTextSb.Append("      ELSE 0 ")
        cmdTextSb.Append("      END AS print_taigyougai_flg")
        cmdTextSb.Append("  ,SK.kaisyuu_syubetu2")
        cmdTextSb.Append("  ,SK.kaisyuu_wariai2")
        cmdTextSb.Append("  ,SK.kaisyuu_syubetu3")
        cmdTextSb.Append("  ,SK.kaisyuu_wariai3")
        cmdTextSb.Append("  ,SK.add_login_user_id")
        cmdTextSb.Append("  ,SK.add_datetime")
        cmdTextSb.Append("  ,ISNULL(SK.upd_login_user_id, SK.add_login_user_id) AS upd_login_user_id")
        cmdTextSb.Append("  ,ISNULL(SK.upd_datetime, SK.add_datetime) AS upd_datetime")
        cmdTextSb.Append("  ,MSM.seikyuu_sime_date AS mst_seikyuu_sime_date")

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "TABLE��"
    ''' <summary>
    ''' �����f�[�^�擾�p�̋���TABLE�N�G�����擾
    ''' </summary>
    ''' <param name="keyRec">�����f�[�^KEY���R�[�h�N���X</param>
    ''' <param name="emType">�����f�[�^�̌����^�C�v</param>
    ''' <returns>�����f�[�^�擾�p�̋���TABLE�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlTable(ByVal keyRec As SeikyuuDataKeyRecord, ByVal emType As EarthEnum.emSeikyuuSearchType) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlTable", keyRec, emType)

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder

        '****************
        '* TABLE����(���C��)
        '****************
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("   t_seikyuu_kagami SK WITH (READCOMMITTED) ")

        '***********************************************************************
        ' ����`�[�̐����N�����Ɛ��������s���̔�r�p�F���������s�����كt���O(�O������)
        '***********************************************************************
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("               (SELECT ")
        cmdTextSb.Append("                     WK.seikyuusyo_no ")
        cmdTextSb.Append("                   , MAX( ")
        cmdTextSb.Append("                          CASE ")
        cmdTextSb.Append("                               WHEN ISNULL(WK.seikyuusyo_hak_date, '') <> ISNULL(TU.seikyuu_date, '') ")
        cmdTextSb.Append("                               THEN 1 ")
        cmdTextSb.Append("                               ELSE 0 ")
        cmdTextSb.Append("                          END) seikyuu_date_sai_flg ")
        cmdTextSb.Append("                FROM ")
        cmdTextSb.Append("                     t_uriage_data TU WITH (READCOMMITTED) ")
        cmdTextSb.Append("                          INNER JOIN ")
        cmdTextSb.Append("                              (SELECT ")
        cmdTextSb.Append("                                    SM.seikyuusyo_no ")
        cmdTextSb.Append("                                  , SK.seikyuusyo_hak_date ")
        cmdTextSb.Append("                                  , SM.denpyou_unique_no ")
        cmdTextSb.Append("                               FROM ")
        cmdTextSb.Append("                                    t_seikyuu_kagami SK WITH (READCOMMITTED) ")
        cmdTextSb.Append("                                         INNER JOIN t_seikyuu_meisai SM ")
        cmdTextSb.Append("                                           ON SK.seikyuusyo_no = SM.seikyuusyo_no ")
        cmdTextSb.Append("                                          AND SM.inji_taisyo_flg = 1 ")
        cmdTextSb.Append("                              ) WK ")
        cmdTextSb.Append("                            ON TU.denpyou_unique_no = WK.denpyou_unique_no ")
        cmdTextSb.Append("                GROUP BY ")
        cmdTextSb.Append("                     WK.seikyuusyo_no ")
        cmdTextSb.Append("               ) AS WK ")
        cmdTextSb.Append("             ON SK.seikyuusyo_no = WK.seikyuusyo_no ")

        '***********************************************************************
        ' ��������擾�r���[�F�����於�J�i(�O������)
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiMeiKana) Then
            cmdTextSb.Append(" LEFT OUTER JOIN v_seikyuu_saki_info VIW WITH (READCOMMITTED) ")
            cmdTextSb.Append("    ON SK.seikyuu_saki_cd = VIW.seikyuu_saki_cd ")
            cmdTextSb.Append("   AND SK.seikyuu_saki_brc = VIW.seikyuu_saki_brc ")
            cmdTextSb.Append("   AND SK.seikyuu_saki_kbn = VIW.seikyuu_saki_kbn ")
        End If

        '***********************************************************************
        ' �������׃e�[�u���F���׌���(�O������)��
        '***********************************************************************
        cmdTextSb.Append(" LEFT OUTER JOIN ")
        cmdTextSb.Append(" ( SELECT  ")
        cmdTextSb.Append("      SM.seikyuusyo_no ")
        cmdTextSb.Append("      ,COUNT(SM.seikyuusyo_no) AS meisai_kensuu ")
        cmdTextSb.Append("  FROM ")
        cmdTextSb.Append("      t_seikyuu_meisai SM WITH (READCOMMITTED) ")
        cmdTextSb.Append("  WHERE ")
        cmdTextSb.Append("      SM.inji_taisyo_flg = 1 ")
        cmdTextSb.Append("  GROUP BY SM.seikyuusyo_no ")
        cmdTextSb.Append("  ) AS SUB ")
        cmdTextSb.Append("  ON SK.seikyuusyo_no = SUB.seikyuusyo_no ")

        '***********************************************************************
        ' ������}�X�^�F�������ߓ�(�O������)
        '***********************************************************************
        cmdTextSb.Append(" LEFT OUTER JOIN m_seikyuu_saki MSM WITH (READCOMMITTED) ")
        cmdTextSb.Append("    ON SK.seikyuu_saki_cd = MSM.seikyuu_saki_cd ")
        cmdTextSb.Append("   AND SK.seikyuu_saki_brc = MSM.seikyuu_saki_brc ")
        cmdTextSb.Append("   AND SK.seikyuu_saki_kbn = MSM.seikyuu_saki_kbn ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' �����f�[�^�擾�p�̋���TABLE�N�G�����擾[CSV]
    ''' </summary>
    ''' <param name="keyRec">�����f�[�^KEY���R�[�h�N���X</param>
    ''' <param name="emType">�����f�[�^�̌����^�C�v</param>
    ''' <returns>�����f�[�^�擾�p�̋���TABLE�N�G�����擾[CSV]</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlTableCsv(ByVal keyRec As SeikyuuDataKeyRecord, ByVal emType As EarthEnum.emSeikyuuSearchType) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlTableCsv", keyRec, emType)

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("   t_seikyuu_kagami SK WITH (READCOMMITTED) ")

        '***********************************************************************
        ' ��������T(�O������)
        '***********************************************************************
        cmdTextSb.Append(" INNER JOIN t_seikyuu_meisai SM WITH (READCOMMITTED) ")
        cmdTextSb.Append("    ON SK.seikyuusyo_no = SM.seikyuusyo_no ")
        cmdTextSb.Append("    AND SM.inji_taisyo_flg = 1 ")

        '***********************************************************************
        ' ����f�[�^T(��������)
        '***********************************************************************
        cmdTextSb.Append(" INNER JOIN t_uriage_data URI WITH (READCOMMITTED) ")
        cmdTextSb.Append("    ON SM.denpyou_unique_no = URI.denpyou_unique_no ")

        '***********************************************************************
        ' ��������擾�r���[�F�����於�J�i(�O������)
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiMeiKana) Then
            cmdTextSb.Append(" LEFT OUTER JOIN v_seikyuu_saki_info VIW WITH (READCOMMITTED) ")
            cmdTextSb.Append("    ON SK.seikyuu_saki_cd = VIW.seikyuu_saki_cd ")
            cmdTextSb.Append("   AND SK.seikyuu_saki_brc = VIW.seikyuu_saki_brc ")
            cmdTextSb.Append("   AND SK.seikyuu_saki_kbn = VIW.seikyuu_saki_kbn ")
        End If

        '***********************************************************************
        ' �������׃e�[�u���F���׌���(�T�u�N�G��)��
        '***********************************************************************
        If emType = EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo Then '�������ꗗ���
            If Not (keyRec.MeisaiKensuuFrom = Integer.MinValue) Or Not (keyRec.MeisaiKensuuTo = Integer.MinValue) Then
                cmdTextSb.Append(" INNER JOIN ")
                cmdTextSb.Append(" ( SELECT  ")
                cmdTextSb.Append("      SM.seikyuusyo_no ")
                cmdTextSb.Append("      ,COUNT(SM.seikyuusyo_no) AS meisai_kensuu ")
                cmdTextSb.Append("  FROM ")
                cmdTextSb.Append("      t_seikyuu_meisai SM WITH (READCOMMITTED) ")
                cmdTextSb.Append("  WHERE ")
                cmdTextSb.Append("      SM.inji_taisyo_flg = 1 ")
                cmdTextSb.Append("  GROUP BY SM.seikyuusyo_no ")
                cmdTextSb.Append("  ) AS SUB ")
                cmdTextSb.Append("  ON SK.seikyuusyo_no = SUB.seikyuusyo_no ")
            End If
        End If

        '***********************************************************************
        ' ������}�X�^�F�������ߓ�(�O������)
        '***********************************************************************
        cmdTextSb.Append(" LEFT OUTER JOIN m_seikyuu_saki MSM WITH (READCOMMITTED) ")
        cmdTextSb.Append("    ON SK.seikyuu_saki_cd = MSM.seikyuu_saki_cd ")
        cmdTextSb.Append("   AND SK.seikyuu_saki_brc = MSM.seikyuu_saki_brc ")
        cmdTextSb.Append("   AND SK.seikyuu_saki_kbn = MSM.seikyuu_saki_kbn ")

        Return cmdTextSb.ToString
    End Function

#End Region

#Region "WHERE��"
    ''' <summary>
    ''' �����f�[�^�擾�p�̋���WHERE�N�G�����擾
    ''' </summary>
    ''' <param name="keyRec">�����f�[�^KEY���R�[�h�N���X</param>
    ''' <param name="emType">�����f�[�^�̌����^�C�v</param>
    ''' <returns>�����f�[�^�擾�p�̋���WHERE�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlWhere(ByVal keyRec As SeikyuuDataKeyRecord, ByVal emType As EarthEnum.emSeikyuuSearchType) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlWhere", keyRec, emType)

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" WHERE 1 = 1 ")

        '***********************************************************************
        ' �����於�J�i
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiMeiKana) Then
            cmdTextSb.Append("   AND VIW.seikyuu_saki_kana like " & DBprmSeikyuuSakiMeiKana)
        End If

        '***********************************************************************
        ' ������敪
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiKbn) Then
            cmdTextSb.Append(" AND SK.seikyuu_saki_kbn = " & DBprmSeikyuuSakiKbn)
        End If

        '***********************************************************************
        ' ������R�[�h
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiCd) Then
            cmdTextSb.Append(" AND SK.seikyuu_saki_cd = " & DBprmSeikyuuSakiCd)
        End If

        '***********************************************************************
        ' ������}��
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiBrc) Then
            cmdTextSb.Append(" AND SK.seikyuu_saki_brc = " & DBprmSeikyuuSakiBrc)
        End If

        '***********************************************************************
        ' ���������s��
        '***********************************************************************
        If keyRec.SeikyuusyoHakDateFrom <> DateTime.MinValue Or _
            keyRec.SeikyuusyoHakDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.SeikyuusyoHakDateFrom <> DateTime.MinValue And _
                keyRec.SeikyuusyoHakDateTo <> DateTime.MinValue Then
                '�����w�肠���BETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, SK.seikyuusyo_hak_date ,111) BETWEEN " & DBprmSeikyuusyoHakDateFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBprmSeikyuusyoHakDateTo)
            Else
                If keyRec.SeikyuusyoHakDateFrom <> DateTime.MinValue Then
                    'From�̂�
                    cmdTextSb.Append(" CONVERT(VARCHAR, SK.seikyuusyo_hak_date ,111) >= " & DBprmSeikyuusyoHakDateFrom)
                Else
                    'To�̂�
                    cmdTextSb.Append(" CONVERT(VARCHAR, SK.seikyuusyo_hak_date ,111) <= " & DBprmSeikyuusyoHakDateTo)
                End If
            End If
        End If



        '***********************************************************************
        ' �������ߓ����������ꗗ���,�ߋ��������ꗗ���
        '***********************************************************************
        If keyRec.SeikyuuSimeDate <> String.Empty Then
            cmdTextSb.Append(" AND RIGHT ('00' + MSM.seikyuu_sime_date, 2) = " & DBprmSeikyuuSimeDate)
        End If

        '***********************************************************************
        ' �������p�����������ꗗ���,�ߋ��������ꗗ���
        '***********************************************************************
        If keyRec.SeikyuuSyosiki <> String.Empty Then
            If keyRec.SeikyuuSyosiki = EarthConst.ISNULL Then
                cmdTextSb.Append(" AND SK.kaisyuu_seikyuusyo_yousi IS NULL ")
            Else
                cmdTextSb.Append(" AND SK.kaisyuu_seikyuusyo_yousi = " & DBprmSeikyuuSyosiki)
            End If

        End If

        If emType = EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo Then '1.�������ꗗ���

            '***********************************************************************
            ' ���׌���(�T�u�N�G��)��1.�������ꗗ���
            '***********************************************************************
            If Not (keyRec.MeisaiKensuuFrom = Integer.MinValue) Or Not (keyRec.MeisaiKensuuTo = Integer.MinValue) Then

                cmdTextSb.Append(" AND ")

                If Not (keyRec.MeisaiKensuuFrom = Integer.MinValue) And Not (keyRec.MeisaiKensuuTo = Integer.MinValue) Then
                    ' �����w��L���BETWEEN
                    cmdTextSb.Append(" SUB.meisai_kensuu BETWEEN " & DBprmMeisaiKensuuFrom)
                    cmdTextSb.Append(" AND ")
                    cmdTextSb.Append(DBprmMeisaiKensuuTo)
                Else
                    If Not keyRec.MeisaiKensuuFrom = Integer.MinValue Then
                        ' From�̂�
                        cmdTextSb.Append(" SUB.meisai_kensuu >= " & DBprmMeisaiKensuuFrom)
                    Else
                        ' To�̂�
                        cmdTextSb.Append(" SUB.meisai_kensuu <= " & DBprmMeisaiKensuuTo)
                    End If
                End If
            End If

        End If

        '***********************************************************************
        ' ���
        '***********************************************************************
        If keyRec.Torikesi = 0 Then
            cmdTextSb.Append("  AND SK.torikesi = 0 ")
        End If

        '***********************************************************************
        ' �󎚗p���ΏۊO���ߋ��������ꗗ���
        '***********************************************************************
        If emType = EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo Then
            If keyRec.InjiYousi = 0 Then
                cmdTextSb.Append("  AND (SK.kaisyuu_seikyuusyo_yousi_hannyou_cd IS NULL OR SUBSTRING(SK.kaisyuu_seikyuusyo_yousi_hannyou_cd, 1, 1) <> '9') ")
            End If
        End If

        '***********************************************************************
        ' �����������(�ďo����ʕʏ���)���������ꗗ���,�ߋ��������ꗗ���
        '***********************************************************************
        If emType = EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo Then '�������ꗗ���
            cmdTextSb.Append("  AND SK.seikyuusyo_insatu_date IS NULL ")
        ElseIf emType = EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo Then '�ߋ��������ꗗ���
            cmdTextSb.Append("  AND SK.seikyuusyo_insatu_date IS NOT NULL ")
        End If

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "ORDER BY��"
    ''' <summary>
    ''' �����f�[�^�擾�p�̋���ORDER BY�N�G�����擾
    ''' </summary>
    ''' <returns>�����f�[�^�擾�p�̋���ORDER BY�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlOrderBy")

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" ORDER BY ")
        cmdTextSb.Append("   SK.seikyuusyo_hak_date ")
        cmdTextSb.Append("   , SK.seikyuusyo_no ")

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "SQL�p�����[�^"

#Region "�p�����[�^��`"
    '������NO
    Private Const DBprmSeikyuusyoNo As String = "@SEIKYUU_NO"
    '���������s��From,To
    Private Const DBprmSeikyuusyoHakDateFrom As String = "@SEIKYUU_DATE_FROM"
    Private Const DBprmSeikyuusyoHakDateTo As String = "@SEIKYUU_DATE_TO"
    '������R�[�h
    Private Const DBprmSeikyuuSakiCd As String = "@SEIKYUUSAKI_CD"
    '������}��
    Private Const DBprmSeikyuuSakiBrc As String = "@SEIKYUUSAKI_BRC"
    '������敪
    Private Const DBprmSeikyuuSakiKbn As String = "@SEIKYUUSAKI_KBN"
    '�����於�J�i
    Private Const DBprmSeikyuuSakiMeiKana As String = "@SEIKYUUSAKIMEI_KANA"
    '��������
    Private Const DBprmSeikyuuSimeDate As String = "@SEIKYUU_SIME_DATE"
    '��������
    Private Const DBprmSeikyuuSyosiki As String = "@SEIKYUU_SYOSIKI"
    '���׌���From,To
    Private Const DBprmMeisaiKensuuFrom As String = "@MEISAI_KENSUU_FROM"
    Private Const DBprmMeisaiKensuuTo As String = "@MEISAI_KENSUU_TO"
    '�`�[���j�[�NNO
    Private Const DBprmMeisaiDenUnqNo As String = "@MEISAI_DEN_UNQ_NO"
    '�X�V����
    Private Const DBparamAddDateTime As String = "@ADD_DATETIME"
    '�X�V��ID
    Private Const DBparamAddLoginUserId As String = "@ADD_LOGINU_SER_ID"
#End Region

    ''' <summary>
    ''' �����f�[�^�擾�p�̋���SQL�p�����[�^���擾
    ''' </summary>
    ''' <param name="keyRec">�����f�[�^KEY���R�[�h�N���X</param>
    ''' <returns>�����f�[�^�擾�p�̋���SQL�N�G��</returns>
    ''' <remarks></remarks>
    Private Function GetSeikyuuSqlCmnParams(ByVal keyRec As SeikyuuDataKeyRecord) As SqlParameter()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSqlCmnParams", keyRec)

        '���������s��From,To
        Dim objSeikyuusyoHakDateFrom As Object = IIf(keyRec.SeikyuusyoHakDateFrom = DateTime.MinValue, DBNull.Value, keyRec.SeikyuusyoHakDateFrom)
        Dim objSeikyuusyoHakDateTo As Object = IIf(keyRec.SeikyuusyoHakDateTo = DateTime.MinValue, DBNull.Value, keyRec.SeikyuusyoHakDateTo)
        '������
        Dim objSeikyuuSakiKbn As Object = IIf(keyRec.SeikyuuSakiKbn = String.Empty, DBNull.Value, keyRec.SeikyuuSakiKbn)
        Dim objSeikyuuSakiCd As Object = IIf(keyRec.SeikyuuSakiCd = String.Empty, DBNull.Value, keyRec.SeikyuuSakiCd)
        Dim objSeikyuuSakiBrc As Object = IIf(keyRec.SeikyuuSakiBrc = String.Empty, DBNull.Value, keyRec.SeikyuuSakiBrc)
        '�����於�J�i
        Dim objSeikyuuSakiMeiKana As Object = IIf(keyRec.SeikyuuSakiMeiKana = String.Empty, DBNull.Value, keyRec.SeikyuuSakiMeiKana)
        '��������
        Dim objSeikyuuSimeDate As Object = IIf(keyRec.SeikyuuSimeDate = String.Empty, DBNull.Value, keyRec.SeikyuuSimeDate)
        '��������
        Dim objSeikyuuSyosiki As Object = IIf(keyRec.SeikyuuSyosiki = String.Empty, DBNull.Value, keyRec.SeikyuuSyosiki)
        '���׌���From,To
        Dim objMeisaiKensuuFrom As Object = IIf(keyRec.MeisaiKensuuFrom = Integer.MinValue, DBNull.Value, keyRec.MeisaiKensuuFrom)
        Dim objMeisaiKensuuTo As Object = IIf(keyRec.MeisaiKensuuTo = Integer.MinValue, DBNull.Value, keyRec.MeisaiKensuuTo)

        '�p�����[�^�֐ݒ�
        Dim cmdParams() As SqlParameter = { _
                 SQLHelper.MakeParam(DBprmSeikyuusyoHakDateFrom, SqlDbType.DateTime, 16, objSeikyuusyoHakDateFrom), _
                 SQLHelper.MakeParam(DBprmSeikyuusyoHakDateTo, SqlDbType.DateTime, 16, objSeikyuusyoHakDateTo), _
                 SQLHelper.MakeParam(DBprmSeikyuuSakiKbn, SqlDbType.Char, 1, objSeikyuuSakiKbn), _
                 SQLHelper.MakeParam(DBprmSeikyuuSakiCd, SqlDbType.VarChar, 5, objSeikyuuSakiCd), _
                 SQLHelper.MakeParam(DBprmSeikyuuSakiBrc, SqlDbType.VarChar, 2, objSeikyuuSakiBrc), _
                 SQLHelper.MakeParam(DBprmSeikyuuSakiMeiKana, SqlDbType.VarChar, 40, objSeikyuuSakiMeiKana & Chr(37)), _
                 SQLHelper.MakeParam(DBprmSeikyuuSimeDate, SqlDbType.VarChar, 2, objSeikyuuSimeDate), _
                 SQLHelper.MakeParam(DBprmSeikyuuSyosiki, SqlDbType.VarChar, 10, objSeikyuuSyosiki), _
                 SQLHelper.MakeParam(DBprmMeisaiKensuuFrom, SqlDbType.Int, 4, objMeisaiKensuuFrom), _
                 SQLHelper.MakeParam(DBprmMeisaiKensuuTo, SqlDbType.Int, 4, objMeisaiKensuuTo) _
        }

        Return cmdParams
    End Function

#End Region

#End Region

End Class
