Imports System
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �x���f�[�^�̎擾�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class SiharaiDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�����o�ϐ�"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim strLogic As New StringLogic

#End Region

#Region "�x���f�[�^�̎擾"
    ''' <summary>
    ''' �x���f�[�^���擾���܂�
    ''' </summary>
    ''' <param name="keyRec">�x���f�[�^Key���R�[�h</param>
    ''' <returns>�x���f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetSiharaiDataInfo(ByVal keyRec As SiharaiDataKeyRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiharaiDataInfo", keyRec)

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder
        Dim strCmnSql As String = GetCmnSql(keyRec)
        Dim cmdParams() As SqlParameter = GetSqlCmnParams(keyRec)
        Dim cmnDtAcc As New CmnDataAccess

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append(" SD.denpyou_unique_no ")
        cmdTextSb.Append(" ,SD.denpyou_no ")
        cmdTextSb.Append(" ,SD.denpyou_syubetu ")
        cmdTextSb.Append(" ,SD.torikesi_moto_denpyou_unique_no ")
        cmdTextSb.Append(" ,TK.tys_kaisya_cd ")
        cmdTextSb.Append(" ,TK.jigyousyo_cd ")
        cmdTextSb.Append(" ,SD.skk_siwake_unique_no ")
        cmdTextSb.Append(" ,SD.skk_jigyou_cd ")
        cmdTextSb.Append(" ,SD.skk_shri_saki_cd ")
        cmdTextSb.Append(" ,SD.shri_saki_mei ")
        cmdTextSb.Append(" ,SD.siharai_date ")
        cmdTextSb.Append(" ,SD.furikomi ")
        cmdTextSb.Append(" ,SD.sousai ")
        cmdTextSb.Append(" ,SD.tekiyou_mei ")
        cmdTextSb.Append(" ,SD.add_login_user_id ")
        cmdTextSb.Append(" ,SD.add_login_user_name ")
        cmdTextSb.Append(" ,SD.add_datetime ")
        cmdTextSb.Append(" ,SD.upd_login_user_id ")
        cmdTextSb.Append(" ,SD.upd_datetime ")
        cmdTextSb.Append(strCmnSql)

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' �x���f�[�^[CSV�o�͗p]���擾���܂�
    ''' </summary>
    ''' <param name="keyRec">�x���f�[�^Key���R�[�h</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetSiharaiDataCsv(ByVal keyRec As SiharaiDataKeyRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSiharaiDataCsv", keyRec)

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder
        Dim strCmnSql As String = GetCmnSql(keyRec)
        Dim cmdParams() As SqlParameter = GetSqlCmnParams(keyRec)
        Dim cmnDtAcc As New CmnDataAccess

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append(" SD.denpyou_unique_no '�`�[���j�[�NNO' ")
        cmdTextSb.Append(" ,SD.denpyou_no '�`�[�ԍ�' ")
        cmdTextSb.Append(" ,SD.denpyou_syubetu '�`�[��� '")
        cmdTextSb.Append(" ,SD.torikesi_moto_denpyou_unique_no'������`�[���j�[�NNO' ")
        cmdTextSb.Append(" ,TK.tys_kaisya_cd '������ЃR�[�h'")
        cmdTextSb.Append(" ,SD.skk_siwake_unique_no '�V��v�d�󃆃j�[�NNO' ")
        cmdTextSb.Append(" ,SD.skk_jigyou_cd '�V��v���Ə��R�[�h' ")
        cmdTextSb.Append(" ,SD.skk_shri_saki_cd '�V��v�x����R�[�h' ")
        cmdTextSb.Append(" ,SD.shri_saki_mei '�x���於' ")
        cmdTextSb.Append(" ,CONVERT(VARCHAR, SD.siharai_date, 111) '�x���N����' ")
        cmdTextSb.Append(" ,SD.furikomi '�U���z' ")
        cmdTextSb.Append(" ,SD.sousai '���E�z' ")
        cmdTextSb.Append(" ,SD.tekiyou_mei '�E�v' ")
        cmdTextSb.Append(" ,SD.add_login_user_id '�o�^���O�C�����[�U�[ID' ")
        cmdTextSb.Append(" ,SD.add_login_user_name '�o�^���O�C�����[�U�[��' ")
        cmdTextSb.Append(" ,SD.add_datetime '�o�^����' ")
        cmdTextSb.Append(strCmnSql)

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' �f�[�^�擾�p�̋���SQL�N�G�����擾
    ''' </summary>
    ''' <param name="keyRec">����f�[�^Key���R�[�h</param>
    ''' <returns>�f�[�^�擾�p�̋���SQL�N�G��</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSql(ByVal keyRec As SiharaiDataKeyRecord) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSql", keyRec)

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" FROM  ")
        cmdTextSb.Append(" t_siharai_data SD WITH(READCOMMITTED) ")
        cmdTextSb.Append(" LEFT OUTER JOIN ")
        cmdTextSb.Append(" m_tyousakaisya TK WITH(READCOMMITTED) ")
        cmdTextSb.Append(" ON SD.skk_jigyou_cd = TK.skk_jigyousyo_cd ")
        cmdTextSb.Append(" AND SD.skk_shri_saki_cd = TK.skk_shri_saki_cd ")
        cmdTextSb.Append(" AND TK.jigyousyo_cd = TK.shri_jigyousyo_cd ")

        cmdTextSb.Append(" WHERE 1 = 1 ")

        '***********************************************************************
        ' �x���N����
        '***********************************************************************
        If keyRec.ShriDateFrom <> DateTime.MinValue And _
            keyRec.ShriDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")
            cmdTextSb.Append(" CONVERT(VARCHAR, SD.siharai_date ,111) BETWEEN " & DBparamShriDateFrom)
            cmdTextSb.Append(" AND ")
            cmdTextSb.Append(DBparamShriDateTo)
        End If

        '***********************************************************************
        '�`�[�ԍ�
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.DenNoFrom) Or Not String.IsNullOrEmpty(keyRec.DenNoTo) Then

            cmdTextSb.Append(" AND ")

            If Not (keyRec.DenNoFrom Is String.Empty) And Not (keyRec.DenNoTo Is String.Empty) Then
                ' �����w��L���BETWEEN
                cmdTextSb.Append(" SD.denpyou_no BETWEEN " & DBparamDenNoFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBparamDenNoTo)
            Else
                If Not keyRec.DenNoFrom Is String.Empty Then
                    ' �`�[�ԍ�From�̂�
                    cmdTextSb.Append(" SD.denpyou_no >= " & DBparamDenNoFrom)
                Else
                    ' �`�[�ԍ�To�̂�
                    cmdTextSb.Append(" SD.denpyou_no <= " & DBparamDenNoTo)
                End If
            End If
        End If

        '***********************************************************************
        ' ������ЃR�[�h + ���Ə��R�[�h
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.TysKaisyaCd) Then
            cmdTextSb.Append(" AND TK.tys_kaisya_cd + TK.jigyousyo_cd = " & DBparamTysKaisyaCd)
        End If

        '***********************************************************************
        ' �V��v���Ə��R�[�h
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SkkJigyouCd) Then
            cmdTextSb.Append(" AND SD.skk_jigyou_cd = " & DBparamSkkJigyousyoCd)
        End If

        '***********************************************************************
        ' �V��v�x����R�[�h
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SkkShriSakiCd) Then
            cmdTextSb.Append(" AND SD.skk_shri_saki_cd = " & DBparamSkkShriSakiCd)
        End If

        '***********************************************************************
        '�ŐV�`�[�̂ݕ\��
        '***********************************************************************
        If keyRec.NewDenDisp = 0 Then
            cmdTextSb.Append(" AND SD.denpyou_unique_no IN ")
            cmdTextSb.Append("    (SELECT ")
            cmdTextSb.Append("          MAX(SD2.denpyou_unique_no) ")
            cmdTextSb.Append("     FROM ")
            cmdTextSb.Append("          t_siharai_data SD2 ")
            cmdTextSb.Append("     GROUP BY ")
            cmdTextSb.Append("          SD2.skk_siwake_unique_no ")
            cmdTextSb.Append("    ) ")
        End If

        '***********************************************************************
        '�\�������̕t�^�i�`�[���j�[�NNo���x���N�������`�[�ԍ��j
        '***********************************************************************
        cmdTextSb.Append(" ORDER BY ")
        cmdTextSb.Append("     SD.denpyou_unique_no ")
        cmdTextSb.Append("   , SD.siharai_date ")
        cmdTextSb.Append("   , SD.denpyou_no ")

        Return cmdTextSb.ToString

    End Function

    ''' <summary>
    ''' �f�[�^�擾�p�̋���SQL�p�����[�^���擾
    ''' </summary>
    ''' <param name="keyRec">����f�[�^Key���R�[�h</param>
    ''' <returns>�f�[�^�擾�p�̋���SQL�N�G��</returns>
    ''' <remarks></remarks>
    Private Function GetSqlCmnParams(ByVal keyRec As SiharaiDataKeyRecord) As SqlParameter()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSqlCmnParams", keyRec)

        Dim dtShriFrom As Object = IIf(keyRec.ShriDateFrom = DateTime.MinValue, DBNull.Value, keyRec.ShriDateFrom)
        Dim dtShriTo As Object = IIf(keyRec.ShriDateTo = DateTime.MinValue, DBNull.Value, keyRec.ShriDateTo)

        '�p�����[�^�֐ݒ�
        Dim cmdParams() As SqlParameter = { _
                                         SQLHelper.MakeParam(DBparamShriDateFrom, SqlDbType.DateTime, 16, dtShriFrom), _
                                         SQLHelper.MakeParam(DBparamShriDateTo, SqlDbType.DateTime, 16, dtShriTo), _
                                         SQLHelper.MakeParam(DBparamDenNoFrom, SqlDbType.Char, 5, keyRec.DenNoFrom), _
                                         SQLHelper.MakeParam(DBparamDenNoTo, SqlDbType.Char, 5, keyRec.DenNoTo), _
                                         SQLHelper.MakeParam(DBparamTysKaisyaCd, SqlDbType.Char, 7, keyRec.TysKaisyaCd), _
                                         SQLHelper.MakeParam(DBparamSkkJigyousyoCd, SqlDbType.VarChar, 10, keyRec.SkkJigyouCd), _
                                         SQLHelper.MakeParam(DBparamSkkShriSakiCd, SqlDbType.VarChar, 10, keyRec.SkkShriSakiCd) _
                                        }
        Return cmdParams

    End Function

#Region "SQL�p�����[�^"
    Private Const DBparamShriDateFrom As String = "@SHRI_SAKI_DATE_FROM"
    Private Const DBparamShriDateTo As String = "@SHRI_SAKI_DATE_TO"
    Private Const DBparamDenNoFrom As String = "@DENNO_FROM"
    Private Const DBparamDenNoTo As String = "@DENNO_TO"
    Private Const DBparamTysKaisyaCd As String = "@TYOUSA_KAISYA_CD"
    Private Const DBparamSkkJigyousyoCd As String = "@SKK_JIGYOU_CD"
    Private Const DBparamSkkShriSakiCd As String = "@SKK_SHRI_SAKI_CD"

#End Region

#End Region

End Class
