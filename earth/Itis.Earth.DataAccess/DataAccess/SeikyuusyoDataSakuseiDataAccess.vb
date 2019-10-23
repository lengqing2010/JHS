Imports System.text
Imports System.Data.SqlClient


''' <summary>
''' �����ӁE��������TBL�ɐ��������쐬����f�[�^�A�N�Z�X�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class SeikyuusyoDataSakuseiDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�����o�ϐ�"
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    Private allSakuseiFlg As Boolean
    Private blnGetHidukeBtn As Boolean = False
    Private listSeikyuuSaki As List(Of SeikyuuSakiInfoRecord)
#End Region

#Region "SQL�p�����[�^"
    Private Const DBparamSeikyuusyoHakkouDate As String = "@SEIKYUUSYOHAKKOUBI"
    Private Const DBparamAddLoginUserId As String = "@ADDLOGINUSERID"
    Private Const DBparamAddDateTime As String = "@ADDDATETIME"

    '������pDB�p�����[�^
    Private Const DBParamSeikyuuSakiCd As String = "@SEIKYUUSAKICD"
    Private Const DBParamSeikyuuSakiBrc As String = "@SEIKYUUSAKIBRC"
    Private Const DBParamSeikyuuSakiKbn As String = "@SEIKYUUSAKIKBN"

    Private ARRAY_SEIKYUU_LINES() As String = New String() {"_1", "_2", "_3", "_4", "_5", "_6", "_7", "_8", "_9", "_10"}

#End Region

    ''' <summary>
    ''' ������/���דo�^�pSQL���s
    ''' </summary>
    ''' <param name="strSeikyuusyoHakDate">���������s��</param>
    ''' <param name="blnAllSakuseiFlg">�����ł̐�����i�荞�݂��s�����ۂ���FLG(True:�i�荞�܂Ȃ� False:�i�荞��)</param>
    ''' <param name="strLoginUserId">���O�C�����[�UID</param>
    Public Sub createKagamiMeisai(ByVal strSeikyuusyoHakDate As String, _
                                       ByVal blnAllSakuseiFlg As Boolean, _
                                       ByVal strLoginUserId As String, _
                                       ByVal listSsi As List(Of SeikyuuSakiInfoRecord), _
                                       ByRef listResult As List(Of Integer), _
                                       ByRef intKagamiSetCount As Integer, _
                                       ByRef intRirekiSetCount As Integer)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".createKagamiMeisai", strSeikyuusyoHakDate, blnAllSakuseiFlg, strLoginUserId, listResult)

        Dim intResult As Integer = 0    '�e�������ʌ����擾�p
        Dim cmdParams() As SqlClient.SqlParameter   'SQL�R�}���h�p�����[�^�ݒ�p
        Dim intPrmCnt As Integer = 0

        ' �p�����[�^�̐ݒ�
        cmdParams = New SqlParameter() {SQLHelper.MakeParam(DBparamSeikyuusyoHakkouDate, SqlDbType.DateTime, 16, strSeikyuusyoHakDate), _
                                        SQLHelper.MakeParam(DBparamAddLoginUserId, SqlDbType.VarChar, 30, strLoginUserId), _
                                        SQLHelper.MakeParam(DBparamAddDateTime, SqlDbType.DateTime, 16, DateTime.Now)}


        '������ʎw�肪����ꍇ�p�����[�^�ǉ�
        If Not listSsi Is Nothing AndAlso listSsi.Count <> 0 Then

            '�p�����[�^�̐ݒ萔���擾
            intPrmCnt = cmdParams.Length

            '�p�����[�^�ǉ����������z����m��
            ReDim Preserve cmdParams(intPrmCnt + listSsi.Count * 3)

            '�p�����[�^�ɐ�������w��
            For j As Integer = 0 To listSsi.Count - 1
                cmdParams(intPrmCnt + 3 * j) = SQLHelper.MakeParam(DBParamSeikyuuSakiCd & ARRAY_SEIKYUU_LINES(j), SqlDbType.VarChar, 5, listSsi(j).SeikyuuSakiCd)
                cmdParams(intPrmCnt + 3 * j + 1) = SQLHelper.MakeParam(DBParamSeikyuuSakiBrc & ARRAY_SEIKYUU_LINES(j), SqlDbType.VarChar, 2, listSsi(j).SeikyuuSakiBrc)
                cmdParams(intPrmCnt + 3 * j + 2) = SQLHelper.MakeParam(DBParamSeikyuuSakiKbn & ARRAY_SEIKYUU_LINES(j), SqlDbType.Char, 1, listSsi(j).SeikyuuSakiKbn)
            Next
        End If

        ' �����ł̐�����i�荞�݂��s�����ۂ���FLG���O���[�o���ϐ��ɃZ�b�g
        allSakuseiFlg = blnAllSakuseiFlg

        ' ������ʎw��̏ꍇ�̃��X�g���O���[�o���ϐ��ɃZ�b�g
        listSeikyuuSaki = listSsi

        ' �ꎞ�e�[�u����DROP
        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    dropTempTable(), _
                                    cmdParams)

        ' ����`�[���[�NTBL�쐬
        ' ����`�[���[�NTBL�ɔ���v��ς݂̔���`�[���i�[
        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    createTempMeisai() & setTempMeisaiTable(), _
                                    cmdParams)
        listResult.Add(intResult)

        ' �����ӂ���ߋ��̐������f�[�^���擾
        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    setZenkaiKagami(), _
                                    cmdParams)
        listResult.Add(intResult)

        ' �����e�[�u�������z���ڂ��擾
        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    setKingakuInfo(), _
                                    cmdParams)
        listResult.Add(intResult)

        ' �e���[�N�e�[�u�����琿���ӓo�^�p�̃��[�N�Ɋi�[
        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    setKagamiData(), _
                                    cmdParams)
        listResult.Add(intResult)

        '********** �Â������Ӄf�[�^��������邽�߂̌J��Ԃ����� �J�n **********
        ' �Â������Ӄf�[�^�����
        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    updOldKagamiTorikesi(), _
                                    cmdParams)
        listResult.Add(intResult)

        If intResult > 0 Then
            '������s�����Ӄf�[�^�����݂����ꍇ�ɂ̂݁A�ꎞ�e�[�u���č쐬�̂��߂̌J��Ԃ��������s��

            ' �č쐬���K�v�Ȉꎞ�e�[�u����DROP
            ' �N�G�����s
            intResult = ExecuteNonQuery(connStr, _
                                        CommandType.Text, _
                                        dropTempTable(), _
                                        cmdParams)
            listResult.Add(intResult)

            ' ����`�[���[�NTBL�쐬
            ' ����`�[���[�NTBL�ɔ���v��ς݂̔���`�[���i�[
            ' �N�G�����s
            intResult = ExecuteNonQuery(connStr, _
                                        CommandType.Text, _
                                        createTempMeisai() & setTempMeisaiTable(), _
                                        cmdParams)
            listResult.Add(intResult)

            ' �����ӂ���ߋ��̐������f�[�^���擾
            ' �N�G�����s
            intResult = ExecuteNonQuery(connStr, _
                                        CommandType.Text, _
                                        setZenkaiKagami(), _
                                        cmdParams)
            listResult.Add(intResult)

            ' �����e�[�u�������z���ڂ��擾
            ' �N�G�����s
            intResult = ExecuteNonQuery(connStr, _
                                        CommandType.Text, _
                                        setKingakuInfo(), _
                                        cmdParams)
            listResult.Add(intResult)

            ' �e���[�N�e�[�u�����琿���ӓo�^�p�̃��[�N�Ɋi�[
            ' �N�G�����s
            intResult = ExecuteNonQuery(connStr, _
                                        CommandType.Text, _
                                        setKagamiData(), _
                                        cmdParams)
            listResult.Add(intResult)
        End If

        '********** �Â������Ӄf�[�^��������邽�߂̌J��Ԃ����� �I�� **********

        ' ������No�A�Ԏ擾�p�̃��[�N�e�[�u��
        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    createTempNumbering(), _
                                    cmdParams)
        listResult.Add(intResult)

        ' �����Ӄe�[�u���֓o�^
        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    insSeikyuuKagami(), _
                                    cmdParams)
        intKagamiSetCount = intResult
        listResult.Add(intResult)

        ' �������׃e�[�u���֓o�^
        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    insSeikyuuMeisai(), _
                                    cmdParams)
        listResult.Add(intResult)

        ' ���ߓ������e�[�u�����X�V
        ' �N�G�����s
        intRirekiSetCount += ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    updSimeDateRireki(), _
                                    cmdParams)
        listResult.Add(intResult)

        ' ���ߓ������e�[�u����o�^
        ' �N�G�����s
        intRirekiSetCount += ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    insSimeDateRireki(), _
                                    cmdParams)
        listResult.Add(intResult)

        ' �ꎞ�e�[�u����DROP
        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    dropTempTable(), _
                                    cmdParams)

    End Sub

    ''' <summary>
    ''' ���������s�Ώۂ̐����N�����ŏ����ߓ����擾����SQL���s
    ''' </summary>
    ''' <param name="strSeikyuusyoHakDate">���(��{�I�ɑS���ΏۂȂ̂ŁADate.MaxValue)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMinSeikyuuSimeDate(ByVal strSeikyuusyoHakDate As String, ByVal listSsi As List(Of SeikyuuSakiInfoRecord)) As Object
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getMinSeikyuuSimeDate", strSeikyuusyoHakDate)

        Dim intResult As Integer = 0    '�e�������ʌ����擾�p
        Dim cmdParams() As SqlClient.SqlParameter   'SQL�R�}���h�p�����[�^�ݒ�p
        Dim intPrmCnt As Integer = 0

        ' �p�����[�^�̐ݒ�
        cmdParams = New SqlParameter() {SQLHelper.MakeParam(DBparamSeikyuusyoHakkouDate, SqlDbType.DateTime, 16, strSeikyuusyoHakDate)}

        '������ʎw�肪����ꍇ�p�����[�^�ǉ�
        If Not listSsi Is Nothing AndAlso listSsi.Count <> 0 Then

            '�p�����[�^�̐ݒ萔���擾
            intPrmCnt = cmdParams.Length

            '�p�����[�^�ǉ����������z����m��
            ReDim Preserve cmdParams(intPrmCnt + listSsi.Count * 3)

            '�p�����[�^�ɐ�������w��
            For j As Integer = 0 To listSsi.Count - 1
                cmdParams(intPrmCnt + 3 * j) = SQLHelper.MakeParam(DBParamSeikyuuSakiCd & ARRAY_SEIKYUU_LINES(j), SqlDbType.VarChar, 5, listSsi(j).SeikyuuSakiCd)
                cmdParams(intPrmCnt + 3 * j + 1) = SQLHelper.MakeParam(DBParamSeikyuuSakiBrc & ARRAY_SEIKYUU_LINES(j), SqlDbType.VarChar, 2, listSsi(j).SeikyuuSakiBrc)
                cmdParams(intPrmCnt + 3 * j + 2) = SQLHelper.MakeParam(DBParamSeikyuuSakiKbn & ARRAY_SEIKYUU_LINES(j), SqlDbType.Char, 1, listSsi(j).SeikyuuSakiKbn)
            Next
        End If

        ' �����ł̐�����i�荞�݂��s�����ۂ���FLG���O���[�o���ϐ��ɃZ�b�g
        ' (��ɍi�荞�݂��s��Ȃ�)
        allSakuseiFlg = True

        ' ���ߓ��ł̍i���݁A�y�ѐ�����M.��x���߃t���O�ɂ�������ǉ����邩
        ' ��ɏ�����ǉ����Ȃ�
        blnGetHidukeBtn = True

        ' ������ʎw��̏ꍇ�̃��X�g���O���[�o���ϐ��ɃZ�b�g
        listSeikyuuSaki = listSsi

        ' �ꎞ�e�[�u����DROP
        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    dropTempTable(), _
                                    cmdParams)

        ' ����`�[���[�NTBL�쐬
        ' ����`�[���[�NTBL�ɔ���v��ς݂̔���`�[���i�[
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    createTempMeisai() & setTempMeisaiTable(), _
                                    cmdParams)

        ' ���������̍ŏ������擾(����f�[�^)
        Dim minSimeDateUriage As Object = ExecuteScalar(connStr, _
                                                        CommandType.Text, _
                                                        getMinSeikyuuSimeDateUriageSql(), _
                                                        cmdParams)

        ' �����ӂ���ߋ��̐������f�[�^���擾
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    setZenkaiKagami(), _
                                    cmdParams)

        ' ���������̍ŏ������擾(�����f�[�^)
        Dim minSimeDateNyuukin As Object = ExecuteScalar(connStr, _
                                                         CommandType.Text, _
                                                         getMinSeikyuuSimeDateNyuukinSql(), _
                                                         cmdParams)


        ' �ꎞ�e�[�u����DROP
        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    dropTempTable(), _
                                    cmdParams)

        '�߂�l��ݒ�
        Dim minSimeDate As Object = DBNull.Value
        If minSimeDateUriage Is DBNull.Value And minSimeDateNyuukin Is DBNull.Value Then
            '����A��������Null�������ꍇ
            minSimeDate = DBNull.Value
        ElseIf minSimeDateUriage Is DBNull.Value Or minSimeDateNyuukin Is DBNull.Value Then
            If minSimeDateUriage Is DBNull.Value Then
                '����̂�Null�������ꍇ�A�������̓��t���Z�b�g
                minSimeDate = minSimeDateNyuukin
            ElseIf minSimeDateNyuukin Is DBNull.Value Then
                '�����̂�Null�������ꍇ�A���㑤�̓��t���Z�b�g
                minSimeDate = minSimeDateUriage
            End If
        Else
            '����Null�ł͖��������ꍇ�A���㑤�A���������r���ăZ�b�g(�Â����̓��t���g�p)
            If minSimeDateUriage >= minSimeDateNyuukin Then
                minSimeDate = minSimeDateNyuukin
            Else
                minSimeDate = minSimeDateUriage
            End If
        End If

        '�l�߂�
        Return minSimeDate

    End Function

    ''' <summary>
    ''' ����`�[���ׂ̃e���|�����[�e�[�u�����쐬
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function createTempMeisai() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".createTempMeisai")
        Dim cmdTextSb As New StringBuilder()

        'TEMP�e�[�u���쐬�p��SQL���쐬�Ǝ��s
        cmdTextSb.AppendLine(" CREATE TABLE ")
        cmdTextSb.AppendLine("      ##TEMP_URI_DATA( ")
        cmdTextSb.AppendLine("                   [seikyuu_saki_cd][varchar](5) ")
        cmdTextSb.AppendLine("                 , [seikyuu_saki_brc][varchar](2) ")
        cmdTextSb.AppendLine("                 , [seikyuu_saki_kbn][char](1) ")
        cmdTextSb.AppendLine("                 , [zei_komi_gaku][bigint] ")
        cmdTextSb.AppendLine("                 , [denpyou_unique_no][int] ")
        cmdTextSb.AppendLine("                 , [denpyou_syubetu][varchar](2) ")
        cmdTextSb.AppendLine("                 , [torikesi_moto_denpyou_unique_no][int] ")
        cmdTextSb.AppendLine("                 , [seikyuu_date][datetime] ")
        cmdTextSb.AppendLine("                 , [inji_taisyo_flg][int]) ")
        cmdTextSb.AppendLine("")

        Return cmdTextSb.ToString()

    End Function

    ''' <summary>
    ''' �`�[���׃��[�NTBL�ɔ���v��ς݂̔���`�[���i�[
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function setTempMeisaiTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".setTempMeisaiTable")
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine("INSERT ")
        cmdTextSb.AppendLine("INTO ##TEMP_URI_DATA( ")
        cmdTextSb.AppendLine("    seikyuu_saki_cd")
        cmdTextSb.AppendLine("    , seikyuu_saki_brc")
        cmdTextSb.AppendLine("    , seikyuu_saki_kbn")
        cmdTextSb.AppendLine("    , zei_komi_gaku")
        cmdTextSb.AppendLine("    , denpyou_unique_no")
        cmdTextSb.AppendLine("    , denpyou_syubetu")
        cmdTextSb.AppendLine("    , torikesi_moto_denpyou_unique_no")
        cmdTextSb.AppendLine("    , seikyuu_date")
        cmdTextSb.AppendLine("    , inji_taisyo_flg")
        cmdTextSb.AppendLine(") ")
        cmdTextSb.AppendLine("SELECT")
        cmdTextSb.AppendLine("    URI.seikyuu_saki_cd")
        cmdTextSb.AppendLine("    , URI.seikyuu_saki_brc")
        cmdTextSb.AppendLine("    , URI.seikyuu_saki_kbn")
        cmdTextSb.AppendLine("    , (URI.uri_gaku + URI.sotozei_gaku) zei_komi_gaku")
        cmdTextSb.AppendLine("    , URI.denpyou_unique_no")
        cmdTextSb.AppendLine("    , URI.denpyou_syubetu")
        cmdTextSb.AppendLine("    , URI.torikesi_moto_denpyou_unique_no")
        cmdTextSb.AppendLine("    , URI.seikyuu_date")
        cmdTextSb.AppendLine("    , CASE ")
        cmdTextSb.AppendLine("        WHEN FLG.denpyou_unique_no IS NULL ")
        cmdTextSb.AppendLine("        THEN 0 ")
        cmdTextSb.AppendLine("        ELSE 1 ")
        cmdTextSb.AppendLine("        END inji_taisyo_flg ")
        cmdTextSb.AppendLine("FROM")
        cmdTextSb.AppendLine("    jhs_sys.v_seikyuusyo_mihakkou_uri_data URI ")
        cmdTextSb.AppendLine("    INNER JOIN jhs_sys.m_seikyuu_saki SSM ")
        cmdTextSb.AppendLine("        ON URI.seikyuu_saki_cd = SSM.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("        AND URI.seikyuu_saki_brc = SSM.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("        AND URI.seikyuu_saki_kbn = SSM.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("    LEFT OUTER JOIN ( ")
        cmdTextSb.AppendLine("        SELECT")
        cmdTextSb.AppendLine("            URI2.denpyou_unique_no ")
        cmdTextSb.AppendLine("        FROM")
        cmdTextSb.AppendLine("            jhs_sys.v_seikyuusyo_mihakkou_uri_data URI2 ")
        cmdTextSb.AppendLine("        WHERE")
        cmdTextSb.AppendLine("            URI2.seikyuu_date IS NOT NULL ")
        cmdTextSb.AppendLine("            AND URI2.seikyuu_date <= " & DBparamSeikyuusyoHakkouDate & " ")
        cmdTextSb.AppendLine("            AND ( ")
        cmdTextSb.AppendLine("                ( ")
        cmdTextSb.AppendLine("                    URI2.denpyou_syubetu = 'UN' ")
        cmdTextSb.AppendLine("                    AND NOT EXISTS( ")
        cmdTextSb.AppendLine("                        SELECT")
        cmdTextSb.AppendLine("                            * ")
        cmdTextSb.AppendLine("                        FROM")
        cmdTextSb.AppendLine("                            jhs_sys.v_seikyuusyo_mihakkou_uri_data URT ")
        cmdTextSb.AppendLine("                        WHERE")
        cmdTextSb.AppendLine("                            URT.denpyou_syubetu = 'UR' ")
        cmdTextSb.AppendLine("                            AND URT.seikyuu_date IS NOT NULL ")
        cmdTextSb.AppendLine("                            AND URT.seikyuu_date <= " & DBparamSeikyuusyoHakkouDate & " ")
        cmdTextSb.AppendLine("                            AND URT.torikesi_moto_denpyou_unique_no = URI2.denpyou_unique_no")
        cmdTextSb.AppendLine("                    )")
        cmdTextSb.AppendLine("                ) ")
        cmdTextSb.AppendLine("                OR ( ")
        cmdTextSb.AppendLine("                    URI2.denpyou_syubetu = 'UR' ")
        cmdTextSb.AppendLine("                    AND ( ")
        cmdTextSb.AppendLine("                        EXISTS( ")
        cmdTextSb.AppendLine("                            SELECT")
        cmdTextSb.AppendLine("                                * ")
        cmdTextSb.AppendLine("                            FROM")
        cmdTextSb.AppendLine("                                jhs_sys.t_seikyuu_meisai MEI ")
        cmdTextSb.AppendLine("                                INNER JOIN jhs_sys.t_seikyuu_kagami KGM ")
        cmdTextSb.AppendLine("                                    ON MEI.seikyuusyo_no = KGM.seikyuusyo_no ")
        cmdTextSb.AppendLine("                            WHERE")
        cmdTextSb.AppendLine("                                MEI.denpyou_unique_no = URI2.torikesi_moto_denpyou_unique_no ")
        cmdTextSb.AppendLine("                                AND KGM.torikesi = 0")
        cmdTextSb.AppendLine("                        ) ")
        cmdTextSb.AppendLine("                        OR URI2.torikesi_moto_denpyou_unique_no IS NULL ")
        cmdTextSb.AppendLine("                        OR URI2.torikesi_moto_denpyou_unique_no = 0")
        cmdTextSb.AppendLine("                        OR (NOT EXISTS ")
        cmdTextSb.AppendLine("                               (SELECT ")
        cmdTextSb.AppendLine("                                     * ")
        cmdTextSb.AppendLine("                                FROM ")
        cmdTextSb.AppendLine("                                     jhs_sys.v_seikyuusyo_mihakkou_uri_data SMU ")
        cmdTextSb.AppendLine("                                WHERE ")
        cmdTextSb.AppendLine("                                     SMU.denpyou_syubetu = 'UN' ")
        cmdTextSb.AppendLine("                                 AND SMU.seikyuu_date IS NOT NULL ")
        cmdTextSb.AppendLine("                                 AND SMU.seikyuu_date <= " & DBparamSeikyuusyoHakkouDate)
        cmdTextSb.AppendLine("                                 AND URI2.torikesi_moto_denpyou_unique_no = SMU.denpyou_unique_no ")
        cmdTextSb.AppendLine("                               ) ")
        cmdTextSb.AppendLine("                         ) ")
        cmdTextSb.AppendLine("                    )")
        cmdTextSb.AppendLine("                )")
        cmdTextSb.AppendLine("            )")
        cmdTextSb.AppendLine("    ) FLG ")
        cmdTextSb.AppendLine("        ON URI.denpyou_unique_no = FLG.denpyou_unique_no ")
        cmdTextSb.AppendLine("WHERE")
        cmdTextSb.AppendLine("    1 = 1")

        '***********************************************************************
        ' ���������s���ƒ��ߓ�����.�������ߓ���������}�X�^.�������ߓ��������f�[�^�̂ݔ��s�ΏۂƂ���
        '***********************************************************************
        If Not blnGetHidukeBtn Then
            '���t�擾�{�^���ȊO�̏ꍇ
            If Not allSakuseiFlg Then
                '�S�Ώۃ`�F�b�N�Ȃ��̏ꍇ
                cmdTextSb.AppendLine("  AND ((jhs_sys.fnGetLastDay(" & DBparamSeikyuusyoHakkouDate & ") = " & DBparamSeikyuusyoHakkouDate)
                cmdTextSb.AppendLine("  AND jhs_sys.fnGetSeikyuuSimeBi(SSM.seikyuu_saki_cd, SSM.seikyuu_saki_brc, SSM.seikyuu_saki_kbn, " & DBparamSeikyuusyoHakkouDate & ") = '31') ")
                cmdTextSb.AppendLine("   OR jhs_sys.fnGetSeikyuuSimeBi(SSM.seikyuu_saki_cd, SSM.seikyuu_saki_brc, SSM.seikyuu_saki_kbn, " & DBparamSeikyuusyoHakkouDate & ") = DATENAME(DAY, " & DBparamSeikyuusyoHakkouDate & ")) ")
            Else
                '�S�Ώۃ`�F�b�N����̏ꍇ
                cmdTextSb.AppendLine("  AND SSM.kessanji_nidosime_flg = '0' ")
            End If
        End If

        cmdTextSb.AppendLine("    AND URI.seikyuu_date IS NOT NULL ")
        cmdTextSb.AppendLine("    AND URI.seikyuu_date <= " & DBparamSeikyuusyoHakkouDate & " ")
        cmdTextSb.AppendLine("    AND ISNULL(URI.uri_keijyou_flg, 0) = 1 ")

        '***********************************************************************
        ' ��ʏ�Ŏw�肪����ꍇ�A���̐�����̂ݑΏۂƂ���
        '***********************************************************************
        If Not listSeikyuuSaki Is Nothing AndAlso listSeikyuuSaki.Count <> 0 Then

            cmdTextSb.AppendLine(" AND ( ")

            For i As Integer = 0 To listSeikyuuSaki.Count - 1
                If i > 0 Then
                    cmdTextSb.AppendLine(" OR ")
                End If
                cmdTextSb.AppendLine(" ( ")
                cmdTextSb.AppendLine(" URI.seikyuu_saki_cd = " & DBParamSeikyuuSakiCd & ARRAY_SEIKYUU_LINES(i))
                cmdTextSb.AppendLine(" AND URI.seikyuu_saki_brc = " & DBParamSeikyuuSakiBrc & ARRAY_SEIKYUU_LINES(i))
                cmdTextSb.AppendLine(" AND URI.seikyuu_saki_kbn = " & DBParamSeikyuuSakiKbn & ARRAY_SEIKYUU_LINES(i))
                cmdTextSb.AppendLine(" ) ")
            Next
            cmdTextSb.AppendLine(" ) ")
        End If


        cmdTextSb.AppendLine("ORDER BY")
        cmdTextSb.AppendLine("    URI.seikyuu_saki_cd")
        cmdTextSb.AppendLine("    , URI.seikyuu_saki_brc")
        cmdTextSb.AppendLine("    , URI.seikyuu_saki_kbn")
        cmdTextSb.AppendLine("    , URI.denpyou_unique_no")
        cmdTextSb.AppendLine("")

        '�C���f�b�N�X�̕t�^
        cmdTextSb.AppendLine(" CREATE CLUSTERED INDEX ix_temp_uri_data ")
        cmdTextSb.AppendLine(" ON ##TEMP_URI_DATA( ")
        cmdTextSb.AppendLine("                      seikyuu_saki_cd ")
        cmdTextSb.AppendLine("                    , seikyuu_saki_brc ")
        cmdTextSb.AppendLine("                    , seikyuu_saki_kbn) ")
        cmdTextSb.AppendLine("")

        Return cmdTextSb.ToString()
    End Function

    ''' <summary>
    ''' �����Ӄe�[�u���̉ߋ��Œ��߂̐������f�[�^���擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function setZenkaiKagami() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".setZenkaiKagami")
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine("SELECT")
        cmdTextSb.AppendLine("    KGM.* ")
        cmdTextSb.AppendLine("INTO ##TEMP_ZENKAI_KAGAMI ")
        cmdTextSb.AppendLine("FROM")
        cmdTextSb.AppendLine("    jhs_sys.t_seikyuu_kagami KGM ")
        cmdTextSb.AppendLine("    INNER JOIN ( ")
        cmdTextSb.AppendLine("        SELECT")
        cmdTextSb.AppendLine("            max(seikyuusyo_no) seikyuusyo_no ")
        cmdTextSb.AppendLine("        FROM")
        cmdTextSb.AppendLine("            jhs_sys.t_seikyuu_kagami KGM1 ")
        cmdTextSb.AppendLine("            INNER JOIN ( ")
        cmdTextSb.AppendLine("                SELECT")
        cmdTextSb.AppendLine("                    seikyuu_saki_cd")
        cmdTextSb.AppendLine("                    , seikyuu_saki_brc")
        cmdTextSb.AppendLine("                    , seikyuu_saki_kbn")
        cmdTextSb.AppendLine("                    , max(seikyuusyo_hak_date) seikyuusyo_hak_date ")
        cmdTextSb.AppendLine("                FROM")
        cmdTextSb.AppendLine("                    jhs_sys.t_seikyuu_kagami ")
        cmdTextSb.AppendLine("                WHERE")
        cmdTextSb.AppendLine("                    torikesi = 0 ")
        cmdTextSb.AppendLine("                GROUP BY")
        cmdTextSb.AppendLine("                    seikyuu_saki_cd")
        cmdTextSb.AppendLine("                    , seikyuu_saki_brc")
        cmdTextSb.AppendLine("                    , seikyuu_saki_kbn")
        cmdTextSb.AppendLine("            ) KGM2 ")
        cmdTextSb.AppendLine("                ON KGM1.seikyuu_saki_cd = KGM2.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("                AND KGM1.seikyuu_saki_brc = KGM2.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("                AND KGM1.seikyuu_saki_kbn = KGM2.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("                AND KGM1.seikyuusyo_hak_date = KGM2.seikyuusyo_hak_date ")
        cmdTextSb.AppendLine("        WHERE")
        cmdTextSb.AppendLine("            KGM1.torikesi = 0 ")
        cmdTextSb.AppendLine("        GROUP BY")
        cmdTextSb.AppendLine("            KGM1.seikyuu_saki_cd")
        cmdTextSb.AppendLine("            , KGM1.seikyuu_saki_brc")
        cmdTextSb.AppendLine("            , KGM1.seikyuu_saki_kbn")
        cmdTextSb.AppendLine("    ) KGM_CD ")
        cmdTextSb.AppendLine("        ON KGM.seikyuusyo_no = KGM_CD.seikyuusyo_no")
        cmdTextSb.AppendLine("")

        'INS����##TEMP_ZENKAI_KAGAMI�ɃC���f�b�N�X�̂̕t�^
        cmdTextSb.AppendLine(" CREATE CLUSTERED INDEX ix_temp_zenkai_kagami ")
        cmdTextSb.AppendLine(" ON ##TEMP_ZENKAI_KAGAMI( ")
        cmdTextSb.AppendLine(" 		                seikyuu_saki_cd ")
        cmdTextSb.AppendLine(" 		              , seikyuu_saki_brc ")
        cmdTextSb.AppendLine(" 		              , seikyuu_saki_kbn) ")

        Return cmdTextSb.ToString()
    End Function

    ''' <summary>
    ''' �����e�[�u�������z���ڂ����[�NT�ɃZ�b�g
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function setKingakuInfo() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".setKingakuInfo")
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine("SELECT")
        cmdTextSb.AppendLine("    KINGAKU.*")
        cmdTextSb.AppendLine("    , dateadd( ")
        cmdTextSb.AppendLine("        MONTH")
        cmdTextSb.AppendLine("        , MSS.kaisyuu_yotei_gessuu")
        cmdTextSb.AppendLine("        , " & DBparamSeikyuusyoHakkouDate)
        cmdTextSb.AppendLine("    ) add_date")
        cmdTextSb.AppendLine("    , ( ")
        cmdTextSb.AppendLine("        CASE ")
        cmdTextSb.AppendLine("            WHEN ISNUMERIC(MSS.kaisyuu_yotei_date) = 1 ")
        cmdTextSb.AppendLine("            THEN MSS.kaisyuu_yotei_date ")
        cmdTextSb.AppendLine("            ELSE NULL ")
        cmdTextSb.AppendLine("            END")
        cmdTextSb.AppendLine("    ) kaisyuu_day")
        cmdTextSb.AppendLine("    , MSS.tantousya_mei")
        cmdTextSb.AppendLine("    , MSS.seikyuusyo_inji_bukken_mei_flg")
        cmdTextSb.AppendLine("    , MSS.nyuukin_kouza_no")
        cmdTextSb.AppendLine("    , MSS.seikyuu_sime_date")
        cmdTextSb.AppendLine("    , MSS.senpou_seikyuu_sime_date")
        cmdTextSb.AppendLine("    , MSS.sousai_flg")
        cmdTextSb.AppendLine("    , MSS.kaisyuu_yotei_gessuu")
        cmdTextSb.AppendLine("    , MSS.kaisyuu_yotei_date")
        cmdTextSb.AppendLine("    , MSS.seikyuusyo_hittyk_date")
        cmdTextSb.AppendLine("    , ( ")
        cmdTextSb.AppendLine("        CASE ")
        cmdTextSb.AppendLine("            WHEN KINGAKU.konkai_goseikyuu_gaku >= MSS.kaisyuu_kyoukaigaku ")
        cmdTextSb.AppendLine("            THEN MSS.kaisyuu2_syubetu1 ")
        cmdTextSb.AppendLine("            ELSE MSS.kaisyuu1_syubetu1 ")
        cmdTextSb.AppendLine("            END")
        cmdTextSb.AppendLine("    ) syubetu1")
        cmdTextSb.AppendLine("    , ( ")
        cmdTextSb.AppendLine("        CASE ")
        cmdTextSb.AppendLine("            WHEN KINGAKU.konkai_goseikyuu_gaku >= MSS.kaisyuu_kyoukaigaku ")
        cmdTextSb.AppendLine("            THEN MSS.kaisyuu2_wariai1 ")
        cmdTextSb.AppendLine("            ELSE MSS.kaisyuu1_wariai1 ")
        cmdTextSb.AppendLine("            END")
        cmdTextSb.AppendLine("    ) wariai1")
        cmdTextSb.AppendLine("    , ( ")
        cmdTextSb.AppendLine("        CASE ")
        cmdTextSb.AppendLine("            WHEN KINGAKU.konkai_goseikyuu_gaku >= MSS.kaisyuu_kyoukaigaku ")
        cmdTextSb.AppendLine("            THEN MSS.kaisyuu2_tegata_site_gessuu ")
        cmdTextSb.AppendLine("            ELSE MSS.kaisyuu1_tegata_site_gessuu ")
        cmdTextSb.AppendLine("            END")
        cmdTextSb.AppendLine("    ) tegata_site_gessuu")
        cmdTextSb.AppendLine("    , ( ")
        cmdTextSb.AppendLine("        CASE ")
        cmdTextSb.AppendLine("            WHEN KINGAKU.konkai_goseikyuu_gaku >= MSS.kaisyuu_kyoukaigaku ")
        cmdTextSb.AppendLine("            THEN MSS.kaisyuu2_tegata_site_date ")
        cmdTextSb.AppendLine("            ELSE MSS.kaisyuu1_tegata_site_date ")
        cmdTextSb.AppendLine("            END")
        cmdTextSb.AppendLine("    ) tegata_site_date")
        cmdTextSb.AppendLine("    , ( ")
        cmdTextSb.AppendLine("        CASE ")
        cmdTextSb.AppendLine("            WHEN KINGAKU.konkai_goseikyuu_gaku >= MSS.kaisyuu_kyoukaigaku ")
        cmdTextSb.AppendLine("            THEN MSS.kaisyuu2_seikyuusyo_yousi ")
        cmdTextSb.AppendLine("            ELSE MSS.kaisyuu1_seikyuusyo_yousi ")
        cmdTextSb.AppendLine("            END")
        cmdTextSb.AppendLine("    ) seikyuusyo_yousi")
        cmdTextSb.AppendLine("    , ( ")
        cmdTextSb.AppendLine("        CASE ")
        cmdTextSb.AppendLine("            WHEN KINGAKU.konkai_goseikyuu_gaku >= MSS.kaisyuu_kyoukaigaku ")
        cmdTextSb.AppendLine("            THEN MSS.kaisyuu2_syubetu2 ")
        cmdTextSb.AppendLine("            ELSE MSS.kaisyuu1_syubetu2 ")
        cmdTextSb.AppendLine("            END")
        cmdTextSb.AppendLine("    ) syubetu2")
        cmdTextSb.AppendLine("    , ( ")
        cmdTextSb.AppendLine("        CASE ")
        cmdTextSb.AppendLine("            WHEN KINGAKU.konkai_goseikyuu_gaku >= MSS.kaisyuu_kyoukaigaku ")
        cmdTextSb.AppendLine("            THEN MSS.kaisyuu2_wariai2 ")
        cmdTextSb.AppendLine("            ELSE MSS.kaisyuu1_wariai2 ")
        cmdTextSb.AppendLine("            END")
        cmdTextSb.AppendLine("    ) wariai2")
        cmdTextSb.AppendLine("    , ( ")
        cmdTextSb.AppendLine("        CASE ")
        cmdTextSb.AppendLine("            WHEN KINGAKU.konkai_goseikyuu_gaku >= MSS.kaisyuu_kyoukaigaku ")
        cmdTextSb.AppendLine("            THEN MSS.kaisyuu2_syubetu3 ")
        cmdTextSb.AppendLine("            ELSE MSS.kaisyuu1_syubetu3 ")
        cmdTextSb.AppendLine("            END")
        cmdTextSb.AppendLine("    ) syubetu3")
        cmdTextSb.AppendLine("    , ( ")
        cmdTextSb.AppendLine("        CASE ")
        cmdTextSb.AppendLine("            WHEN KINGAKU.konkai_goseikyuu_gaku >= MSS.kaisyuu_kyoukaigaku ")
        cmdTextSb.AppendLine("            THEN MSS.kaisyuu2_wariai3 ")
        cmdTextSb.AppendLine("            ELSE MSS.kaisyuu1_wariai3 ")
        cmdTextSb.AppendLine("            END")
        cmdTextSb.AppendLine("    ) wariai3 ")

        cmdTextSb.AppendLine("INTO ##TEMP_KINGAKU_INFO ")
        cmdTextSb.AppendLine("FROM")
        cmdTextSb.AppendLine("    ( ")
        cmdTextSb.AppendLine("        SELECT")
        cmdTextSb.AppendLine("            SSM.seikyuu_saki_cd")
        cmdTextSb.AppendLine("            , SSM.seikyuu_saki_brc")
        cmdTextSb.AppendLine("            , SSM.seikyuu_saki_kbn")
        cmdTextSb.AppendLine("            , ISNULL(URI.konkai_goseikyuu_gaku, 0) konkai_goseikyuu_gaku")
        cmdTextSb.AppendLine("            , ISNULL(KGM.konkai_kurikosi_gaku, 0) kurikosi_gaku")
        cmdTextSb.AppendLine("            , ISNULL(KGM.konkai_goseikyuu_gaku, 0) zenkai_goseikyuu_gaku")
        cmdTextSb.AppendLine("            , KGM.seikyuusyo_hak_date zenkai_seikyuusyo_hak_date")
        cmdTextSb.AppendLine("            , SUM( ")
        cmdTextSb.AppendLine("                ISNULL(NKN.genkin, 0) + ISNULL(NKN.kogitte, 0) + ISNULL(NKN.furikomi, 0) + ISNULL(NKN.tegata, 0) + ISNULL(NKN.nebiki, 0) ")
        cmdTextSb.AppendLine("                + ISNULL(NKN.sonota, 0) + ISNULL(NKN.kouza_furikae, 0)")
        cmdTextSb.AppendLine("            ) gonyuukin_gaku")
        cmdTextSb.AppendLine("            , SUM( ")
        cmdTextSb.AppendLine("                ISNULL(NKN.sousai, 0) + ISNULL(NKN.kyouryoku_kaihi, 0)")
        cmdTextSb.AppendLine("            ) sousai_gaku")
        cmdTextSb.AppendLine("            , SUM(ISNULL(NKN.furikomi_tesuuryou, 0)) tyousei_gaku ")
        cmdTextSb.AppendLine("            , URI.seikyuu_saki_cd uri_ssc ")
        cmdTextSb.AppendLine("        FROM")
        cmdTextSb.AppendLine("            jhs_sys.m_seikyuu_saki SSM ")
        cmdTextSb.AppendLine("            LEFT OUTER JOIN ##TEMP_ZENKAI_KAGAMI KGM ")
        cmdTextSb.AppendLine("                ON SSM.seikyuu_saki_cd = KGM.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("                AND SSM.seikyuu_saki_brc = KGM.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("                AND SSM.seikyuu_saki_kbn = KGM.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("            LEFT OUTER JOIN ( ")
        cmdTextSb.AppendLine("                SELECT")
        cmdTextSb.AppendLine("                    seikyuu_saki_cd")
        cmdTextSb.AppendLine("                    , seikyuu_saki_brc")
        cmdTextSb.AppendLine("                    , seikyuu_saki_kbn")
        cmdTextSb.AppendLine("                    , SUM(zei_komi_gaku) konkai_goseikyuu_gaku ")
        cmdTextSb.AppendLine("                FROM")
        cmdTextSb.AppendLine("                    ##TEMP_URI_DATA ")
        cmdTextSb.AppendLine("                GROUP BY")
        cmdTextSb.AppendLine("                    seikyuu_saki_cd")
        cmdTextSb.AppendLine("                    , seikyuu_saki_brc")
        cmdTextSb.AppendLine("                    , seikyuu_saki_kbn")
        cmdTextSb.AppendLine("            ) URI ")
        cmdTextSb.AppendLine("                ON SSM.seikyuu_saki_cd = URI.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("                AND SSM.seikyuu_saki_brc = URI.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("                AND SSM.seikyuu_saki_kbn = URI.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("            LEFT OUTER JOIN jhs_sys.t_nyuukin_data NKN ")
        cmdTextSb.AppendLine("                ON SSM.seikyuu_saki_cd = NKN.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("                AND SSM.seikyuu_saki_brc = NKN.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("                AND SSM.seikyuu_saki_kbn = NKN.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("                AND ISNULL(KGM.seikyuusyo_hak_date, 0) < ISNULL(NKN.nyuukin_date, 0) ")
        cmdTextSb.AppendLine("                AND NKN.nyuukin_date <= " & DBparamSeikyuusyoHakkouDate)
        cmdTextSb.AppendLine("        WHERE")
        cmdTextSb.AppendLine("            ( ")
        cmdTextSb.AppendLine("                URI.seikyuu_saki_cd IS NOT NULL ")
        cmdTextSb.AppendLine("                OR NKN.seikyuu_saki_cd IS NOT NULL")
        cmdTextSb.AppendLine("            ) ")
        cmdTextSb.AppendLine("            AND SSM.torikesi = 0 ")
        '***********************************************************************
        ' ���������s���ƒ��ߓ�����.�������ߓ���������}�X�^.�������ߓ��������f�[�^�̂ݔ��s�ΏۂƂ���
        '***********************************************************************
        If Not allSakuseiFlg Then
            '�S�Ώۃ`�F�b�N�Ȃ��̏ꍇ
            cmdTextSb.AppendLine("  AND ((jhs_sys.fnGetLastDay(" & DBparamSeikyuusyoHakkouDate & ") = " & DBparamSeikyuusyoHakkouDate)
            cmdTextSb.AppendLine("  AND jhs_sys.fnGetSeikyuuSimeBi(SSM.seikyuu_saki_cd, SSM.seikyuu_saki_brc, SSM.seikyuu_saki_kbn, " & DBparamSeikyuusyoHakkouDate & ") = '31') ")
            cmdTextSb.AppendLine("   OR jhs_sys.fnGetSeikyuuSimeBi(SSM.seikyuu_saki_cd, SSM.seikyuu_saki_brc, SSM.seikyuu_saki_kbn, " & DBparamSeikyuusyoHakkouDate & ") = DATENAME(DAY, " & DBparamSeikyuusyoHakkouDate & ")) ")
        Else
            '�S�Ώۃ`�F�b�N����̏ꍇ
            cmdTextSb.AppendLine("  AND SSM.kessanji_nidosime_flg = '0' ")
        End If

        '***********************************************************************
        ' ��ʏ�Ŏw�肪����ꍇ�A���̐�����̂ݑΏۂƂ���
        '***********************************************************************
        If Not listSeikyuuSaki Is Nothing AndAlso listSeikyuuSaki.Count <> 0 Then
            cmdTextSb.AppendLine(" AND ( ")

            For i As Integer = 0 To listSeikyuuSaki.Count - 1
                If i > 0 Then
                    cmdTextSb.AppendLine(" OR ")
                End If
                cmdTextSb.AppendLine(" ( ")
                cmdTextSb.AppendLine(" SSM.seikyuu_saki_cd = " & DBParamSeikyuuSakiCd & ARRAY_SEIKYUU_LINES(i))
                cmdTextSb.AppendLine(" AND SSM.seikyuu_saki_brc = " & DBParamSeikyuuSakiBrc & ARRAY_SEIKYUU_LINES(i))
                cmdTextSb.AppendLine(" AND SSM.seikyuu_saki_kbn = " & DBParamSeikyuuSakiKbn & ARRAY_SEIKYUU_LINES(i))
                cmdTextSb.AppendLine(" ) ")
            Next
            cmdTextSb.AppendLine(" ) ")
        End If

        cmdTextSb.AppendLine("        GROUP BY")
        cmdTextSb.AppendLine("            SSM.seikyuu_saki_cd")
        cmdTextSb.AppendLine("            , SSM.seikyuu_saki_brc")
        cmdTextSb.AppendLine("            , SSM.seikyuu_saki_kbn")
        cmdTextSb.AppendLine("            , ISNULL(URI.konkai_goseikyuu_gaku, 0)")
        cmdTextSb.AppendLine("            , ISNULL(KGM.konkai_kurikosi_gaku, 0)")
        cmdTextSb.AppendLine("            , ISNULL(KGM.konkai_goseikyuu_gaku, 0)")
        cmdTextSb.AppendLine("            , KGM.seikyuusyo_hak_date")
        cmdTextSb.AppendLine("            , URI.seikyuu_saki_cd")
        cmdTextSb.AppendLine("    ) KINGAKU ")
        cmdTextSb.AppendLine("    LEFT OUTER JOIN m_seikyuu_saki MSS ")
        cmdTextSb.AppendLine("        ON KINGAKU.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("        AND KINGAKU.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("        AND KINGAKU.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn")
        cmdTextSb.AppendLine("")

        '�C���f�b�N�X�̕t�^
        cmdTextSb.AppendLine(" CREATE CLUSTERED INDEX ix_temp_kingaku_info ")
        cmdTextSb.AppendLine(" ON ##TEMP_KINGAKU_INFO( ")
        cmdTextSb.AppendLine("                           seikyuu_saki_cd ")
        cmdTextSb.AppendLine("                         , seikyuu_saki_brc ")
        cmdTextSb.AppendLine("                         , seikyuu_saki_kbn) ")
        cmdTextSb.AppendLine("")


        Return cmdTextSb.ToString()
    End Function

    ''' <summary>
    ''' �����Ӄe�[�u���o�^�p���[�N�e�[�u���ɃZ�b�g
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function setKagamiData() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".setKagamiData")
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine(" SELECT KINGAKU.* ")
        cmdTextSb.AppendLine("      , MKM.hannyou_cd ")
        cmdTextSb.AppendLine("      , SSI.seikyuu_saki_mei ")
        cmdTextSb.AppendLine("      , SSI.seikyuu_saki_mei2 ")
        cmdTextSb.AppendLine("      , SSI.skysy_soufu_yuubin_no ")
        cmdTextSb.AppendLine("      , SSI.skysy_soufu_jyuusyo1 ")
        cmdTextSb.AppendLine("      , SSI.skysy_soufu_jyuusyo2 ")
        cmdTextSb.AppendLine("      , SSI.skysy_soufu_tel_no ")
        cmdTextSb.AppendLine("      , SSI.ginkou_siten_cd ")
        cmdTextSb.AppendLine("      , MKM2.meisyou AS ginkou_siten_mei ")
        cmdTextSb.AppendLine("   INTO ##TEMP_KAGAMI_DATA ")
        cmdTextSb.AppendLine("   FROM ##TEMP_KINGAKU_INFO KINGAKU ")
        cmdTextSb.AppendLine("        LEFT OUTER JOIN jhs_sys.m_kakutyou_meisyou MKM ")
        cmdTextSb.AppendLine("          ON KINGAKU.seikyuusyo_yousi = MKM.code ")
        cmdTextSb.AppendLine("         AND MKM.meisyou_syubetu = '3' ")
        cmdTextSb.AppendLine("        LEFT OUTER JOIN jhs_sys.v_seikyuu_saki_info SSI ")
        cmdTextSb.AppendLine("          ON KINGAKU.seikyuu_saki_cd = SSI.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("         AND KINGAKU.seikyuu_saki_brc = SSI.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("         AND KINGAKU.seikyuu_saki_kbn = SSI.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("        LEFT OUTER JOIN jhs_sys.m_kakutyou_meisyou MKM2 ")
        cmdTextSb.AppendLine("          ON ISNULL(SSI.ginkou_siten_cd,'') = MKM2.code ")
        cmdTextSb.AppendLine("         AND MKM2.meisyou_syubetu = '48' ")

        '�C���f�b�N�X�̂̕t�^
        cmdTextSb.AppendLine(" CREATE CLUSTERED INDEX ix_temp_kagami_data ")
        cmdTextSb.AppendLine(" ON ##TEMP_KAGAMI_DATA( ")
        cmdTextSb.AppendLine(" 		                seikyuu_saki_cd ")
        cmdTextSb.AppendLine(" 		              , seikyuu_saki_brc ")
        cmdTextSb.AppendLine(" 		              , seikyuu_saki_kbn) ")

        Return cmdTextSb.ToString()
    End Function

    ''' <summary>
    ''' �Â������Ӄf�[�^�����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function updOldKagamiTorikesi() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".updOldKagamiTorikesi")
        Dim sqlSb As New StringBuilder()

        '�Â������Ӄf�[�^�����
        sqlSb.AppendLine("UPDATE jhs_sys.t_seikyuu_kagami ")
        sqlSb.AppendLine("SET")
        sqlSb.AppendLine("    torikesi = 1")
        sqlSb.AppendLine("    , upd_login_user_id = " & DBparamAddLoginUserId)
        sqlSb.AppendLine("    , upd_datetime = " & DBparamAddDateTime)
        sqlSb.AppendLine("WHERE")
        sqlSb.AppendLine("    EXISTS( ")
        sqlSb.AppendLine("        SELECT")
        sqlSb.AppendLine("            * ")
        sqlSb.AppendLine("        FROM")
        sqlSb.AppendLine("            ##TEMP_KAGAMI_DATA tk ")
        sqlSb.AppendLine("        WHERE")
        sqlSb.AppendLine("            tk.seikyuu_saki_cd = jhs_sys.t_seikyuu_kagami.seikyuu_saki_cd ")
        sqlSb.AppendLine("            AND tk.seikyuu_saki_brc = jhs_sys.t_seikyuu_kagami.seikyuu_saki_brc ")
        sqlSb.AppendLine("            AND tk.seikyuu_saki_kbn = jhs_sys.t_seikyuu_kagami.seikyuu_saki_kbn")
        sqlSb.AppendLine("    ) ")
        sqlSb.AppendLine("    AND jhs_sys.t_seikyuu_kagami.seikyuusyo_hak_date >= " & DBparamSeikyuusyoHakkouDate)
        sqlSb.AppendLine("    AND jhs_sys.t_seikyuu_kagami.torikesi = 0 ")
        sqlSb.AppendLine("")

        Return sqlSb.ToString()
    End Function

    ''' <summary>
    ''' �č쐬���K�v�Ȉꎞ�e�[�u����DROP
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function dropTempTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".dropTempTable")
        Dim sqlSb As New StringBuilder()

        '�e��f�[�^�č쐬�̂��߁A�e���|�����e�[�u����DROP
        sqlSb.AppendLine("IF  EXISTS (SELECT * FROM tempdb..sysobjects  WHERE id = OBJECT_ID(N'tempdb..##TEMP_URI_DATA'))")
        sqlSb.AppendLine("DROP TABLE ##TEMP_URI_DATA")
        sqlSb.AppendLine("")
        sqlSb.AppendLine("IF  EXISTS (SELECT * FROM tempdb..sysobjects  WHERE id = OBJECT_ID(N'tempdb..##TEMP_ZENKAI_KAGAMI'))")
        sqlSb.AppendLine("DROP TABLE ##TEMP_ZENKAI_KAGAMI")
        sqlSb.AppendLine("")
        sqlSb.AppendLine("IF  EXISTS (SELECT * FROM tempdb..sysobjects  WHERE id = OBJECT_ID(N'tempdb..##TEMP_KINGAKU_INFO'))")
        sqlSb.AppendLine("DROP TABLE ##TEMP_KINGAKU_INFO")
        sqlSb.AppendLine("")
        sqlSb.AppendLine("IF  EXISTS (SELECT * FROM tempdb..sysobjects  WHERE id = OBJECT_ID(N'tempdb..##TEMP_KAGAMI_DATA'))")
        sqlSb.AppendLine("DROP TABLE ##TEMP_KAGAMI_DATA")
        sqlSb.AppendLine("")

        Return sqlSb.ToString()
    End Function

    ''' <summary>
    ''' �A�Ԏ擾�p�̃��[�N�e�[�u���쐬�Ɗi�[
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function createTempNumbering() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".createTempNumbering")
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine(" CREATE TABLE ")
        cmdTextSb.AppendLine("      ##TEMP_NUMBERING( ")
        cmdTextSb.AppendLine("                       [seikyuusyo_no][int] IDENTITY(1, 1) ")
        cmdTextSb.AppendLine("                     , [seikyuu_saki_cd][varchar](5) ")
        cmdTextSb.AppendLine("                     , [seikyuu_saki_brc][varchar](2) ")
        cmdTextSb.AppendLine("                     , [seikyuu_saki_kbn][char](1)) ")

        '�������ԍ��t�^
        cmdTextSb.AppendLine(" INSERT INTO ")
        cmdTextSb.AppendLine("      ##TEMP_NUMBERING ")
        cmdTextSb.AppendLine(" ( ")
        cmdTextSb.AppendLine("      seikyuu_saki_cd ")
        cmdTextSb.AppendLine("    , seikyuu_saki_brc ")
        cmdTextSb.AppendLine("    , seikyuu_saki_kbn ")
        cmdTextSb.AppendLine(" ) ")
        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("      seikyuu_saki_cd ")
        cmdTextSb.AppendLine("    , seikyuu_saki_brc ")
        cmdTextSb.AppendLine("    , seikyuu_saki_kbn ")
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      ##TEMP_KAGAMI_DATA ")
        cmdTextSb.AppendLine(" GROUP BY ")
        cmdTextSb.AppendLine("      seikyuu_saki_cd ")
        cmdTextSb.AppendLine("    , seikyuu_saki_brc ")
        cmdTextSb.AppendLine("    , seikyuu_saki_kbn ")

        '�C���f�b�N�X�̕t�^
        cmdTextSb.AppendLine(" CREATE CLUSTERED INDEX ix_temp_numbering ")
        cmdTextSb.AppendLine(" ON ##TEMP_NUMBERING( ")
        cmdTextSb.AppendLine(" 	                seikyuu_saki_cd ")
        cmdTextSb.AppendLine(" 	              , seikyuu_saki_brc ")
        cmdTextSb.AppendLine(" 	              , seikyuu_saki_kbn) ")
        cmdTextSb.AppendLine("  ")

        Return cmdTextSb.ToString()
    End Function

    ''' <summary>
    ''' �����Ӄe�[�u���ւ̓o�^����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function insSeikyuuKagami() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".insSeikyuuKagami")
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine(" INSERT INTO ")
        cmdTextSb.AppendLine("      jhs_sys.t_seikyuu_kagami ")
        cmdTextSb.AppendLine(" ( ")
        cmdTextSb.AppendLine("      seikyuusyo_no ")
        cmdTextSb.AppendLine("    , torikesi ")
        cmdTextSb.AppendLine("    , seikyuu_saki_cd ")
        cmdTextSb.AppendLine("    , seikyuu_saki_brc ")
        cmdTextSb.AppendLine("    , seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("    , seikyuu_saki_mei ")
        cmdTextSb.AppendLine("    , seikyuu_saki_mei2 ")
        cmdTextSb.AppendLine("    , yuubin_no ")
        cmdTextSb.AppendLine("    , jyuusyo1 ")
        cmdTextSb.AppendLine("    , jyuusyo2 ")
        cmdTextSb.AppendLine("    , tel_no ")
        cmdTextSb.AppendLine("    , zenkai_goseikyuu_gaku ")
        cmdTextSb.AppendLine("    , gonyuukin_gaku ")
        cmdTextSb.AppendLine("    , sousai_gaku ")
        cmdTextSb.AppendLine("    , tyousei_gaku ")
        cmdTextSb.AppendLine("    , kurikosi_gaku ")
        cmdTextSb.AppendLine("    , konkai_goseikyuu_gaku ")
        cmdTextSb.AppendLine("    , konkai_kurikosi_gaku ")
        cmdTextSb.AppendLine("    , konkai_kaisyuu_yotei_date ")
        cmdTextSb.AppendLine("    , seikyuusyo_insatu_date ")
        cmdTextSb.AppendLine("    , seikyuusyo_hak_date ")
        cmdTextSb.AppendLine("    , tantousya_mei ")
        cmdTextSb.AppendLine("    , seikyuusyo_inji_bukken_mei_flg ")
        cmdTextSb.AppendLine("    , nyuukin_kouza_no ")
        cmdTextSb.AppendLine("    , seikyuu_sime_date ")
        cmdTextSb.AppendLine("    , senpou_seikyuu_sime_date ")
        cmdTextSb.AppendLine("    , sousai_flg ")
        cmdTextSb.AppendLine("    , kaisyuu_yotei_gessuu ")
        cmdTextSb.AppendLine("    , kaisyuu_yotei_date ")
        cmdTextSb.AppendLine("    , seikyuusyo_hittyk_date ")
        cmdTextSb.AppendLine("    , kaisyuu_syubetu1 ")
        cmdTextSb.AppendLine("    , kaisyuu_wariai1 ")
        cmdTextSb.AppendLine("    , kaisyuu_tegata_site_gessuu ")
        cmdTextSb.AppendLine("    , kaisyuu_tegata_site_date ")
        cmdTextSb.AppendLine("    , kaisyuu_seikyuusyo_yousi ")
        cmdTextSb.AppendLine("    , kaisyuu_seikyuusyo_yousi_hannyou_cd ")
        cmdTextSb.AppendLine("    , kaisyuu_syubetu2 ")
        cmdTextSb.AppendLine("    , kaisyuu_wariai2 ")
        cmdTextSb.AppendLine("    , kaisyuu_syubetu3 ")
        cmdTextSb.AppendLine("    , kaisyuu_wariai3 ")
        cmdTextSb.AppendLine("    , ginkou_siten_cd ")
        cmdTextSb.AppendLine("    , ginkou_siten_mei ")
        cmdTextSb.AppendLine("    , add_login_user_id ")
        cmdTextSb.AppendLine("    , add_datetime ")
        cmdTextSb.AppendLine(" ) ")
        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("      seikyuusyo_no ")
        cmdTextSb.AppendLine("    , '0' torikesi ")
        cmdTextSb.AppendLine("    , KGM.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("    , KGM.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("    , KGM.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("    , KGM.seikyuu_saki_mei ")
        cmdTextSb.AppendLine("    , KGM.seikyuu_saki_mei2 ")
        cmdTextSb.AppendLine("    , KGM.skysy_soufu_yuubin_no ")
        cmdTextSb.AppendLine("    , KGM.skysy_soufu_jyuusyo1 ")
        cmdTextSb.AppendLine("    , KGM.skysy_soufu_jyuusyo2 ")
        cmdTextSb.AppendLine("    , KGM.skysy_soufu_tel_no ")
        cmdTextSb.AppendLine("    , CAST(KGM.zenkai_goseikyuu_gaku AS int) zenkai_goseikyuu_gaku ")
        cmdTextSb.AppendLine("    , CAST(KGM.gonyuukin_gaku AS int)        gonyuukin_gaku ")
        cmdTextSb.AppendLine("    , CAST(KGM.sousai_gaku AS int)           sousai_gaku ")
        cmdTextSb.AppendLine("    , CAST(KGM.tyousei_gaku AS int)          tyousei_gaku ")
        cmdTextSb.AppendLine("    , CAST(KGM.kurikosi_gaku AS int)         kurikosi_gaku ")
        cmdTextSb.AppendLine("    , CAST(KGM.konkai_goseikyuu_gaku AS int)          konkai_goseikyuu_gaku ")
        cmdTextSb.AppendLine("    , CAST( ")
        cmdTextSb.AppendLine("                ( ")
        cmdTextSb.AppendLine("                     ISNULL(KGM.kurikosi_gaku, 0) ")
        cmdTextSb.AppendLine("                  - ISNULL(KGM.gonyuukin_gaku, 0) ")
        cmdTextSb.AppendLine("                  - ISNULL(KGM.sousai_gaku, 0) ")
        cmdTextSb.AppendLine("                  - ISNULL(KGM.tyousei_gaku, 0) ")
        cmdTextSb.AppendLine("                  + ISNULL(KGM.konkai_goseikyuu_gaku, 0) ")
        cmdTextSb.AppendLine("                ) AS int)                       konkai_kurikosi_gaku ")
        cmdTextSb.AppendLine("    , ")
        cmdTextSb.AppendLine("      ( ")
        cmdTextSb.AppendLine("      CASE ")
        cmdTextSb.AppendLine("           WHEN KGM.kaisyuu_day >= DAY(KGM.matu_date) ")
        cmdTextSb.AppendLine("           THEN KGM.matu_date ")
        cmdTextSb.AppendLine("           ELSE ( ")
        cmdTextSb.AppendLine("                 CASE ")
        cmdTextSb.AppendLine("                      WHEN ISDATE(KGM.mst_date) = 1 ")
        cmdTextSb.AppendLine("                      THEN cast(KGM.mst_date AS datetime) ")
        cmdTextSb.AppendLine("                      ELSE NULL ")
        cmdTextSb.AppendLine("                 END) ")
        cmdTextSb.AppendLine("      END)                                   konkai_kaisyuu_yotei_date ")
        cmdTextSb.AppendLine("    , ( ")
        cmdTextSb.AppendLine("           CASE ")
        cmdTextSb.AppendLine("                WHEN KGM.uri_ssc is null ")
        cmdTextSb.AppendLine("                THEN " & DBparamAddDateTime)
        cmdTextSb.AppendLine("                ELSE ( ")
        cmdTextSb.AppendLine("                          CASE ")
        cmdTextSb.AppendLine("                               WHEN KGM.hannyou_cd IS NULL ")
        cmdTextSb.AppendLine("                               THEN NULL ")
        cmdTextSb.AppendLine("                               ELSE ( ")
        cmdTextSb.AppendLine("                                         CASE ")
        cmdTextSb.AppendLine("                                              WHEN SUBSTRING(KGM.hannyou_cd, 1, 1) = '9' ")
        cmdTextSb.AppendLine("                                              THEN " & DBparamAddDateTime)
        cmdTextSb.AppendLine("                                              ELSE NULL ")
        cmdTextSb.AppendLine("                                         END) ")
        cmdTextSb.AppendLine("                          END) ")
        cmdTextSb.AppendLine("           END) seikyuusyo_insatu_date ")
        cmdTextSb.AppendLine("    , " & DBparamSeikyuusyoHakkouDate & "    seikyuusyo_hak_date ")
        cmdTextSb.AppendLine("    , KGM.tantousya_mei ")
        cmdTextSb.AppendLine("    , KGM.seikyuusyo_inji_bukken_mei_flg ")
        cmdTextSb.AppendLine("    , KGM.nyuukin_kouza_no ")
        cmdTextSb.AppendLine("    , KGM.seikyuu_sime_date ")
        cmdTextSb.AppendLine("    , KGM.senpou_seikyuu_sime_date ")
        cmdTextSb.AppendLine("    , KGM.sousai_flg ")
        cmdTextSb.AppendLine("    , KGM.kaisyuu_yotei_gessuu ")
        cmdTextSb.AppendLine("    , KGM.kaisyuu_yotei_date ")
        cmdTextSb.AppendLine("    , KGM.seikyuusyo_hittyk_date ")
        cmdTextSb.AppendLine("    , KGM.syubetu1 ")
        cmdTextSb.AppendLine("    , KGM.wariai1 ")
        cmdTextSb.AppendLine("    , KGM.tegata_site_gessuu ")
        cmdTextSb.AppendLine("    , KGM.tegata_site_date ")
        cmdTextSb.AppendLine("    , KGM.seikyuusyo_yousi ")
        cmdTextSb.AppendLine("    , KGM.hannyou_cd ")
        cmdTextSb.AppendLine("    , KGM.syubetu2 ")
        cmdTextSb.AppendLine("    , KGM.wariai2 ")
        cmdTextSb.AppendLine("    , KGM.syubetu3 ")
        cmdTextSb.AppendLine("    , KGM.wariai3 ")
        cmdTextSb.AppendLine("    , KGM.ginkou_siten_cd ")
        cmdTextSb.AppendLine("    , KGM.ginkou_siten_mei ")
        cmdTextSb.AppendLine("    , " & DBparamAddLoginUserId & " add_login_uer_id ")
        cmdTextSb.AppendLine("    , " & DBparamAddDateTime & " add_datetime ")
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("     (SELECT ")
        cmdTextSb.AppendLine("           RIGHT( ")
        cmdTextSb.AppendLine("                     '00000000000000' ")
        cmdTextSb.AppendLine("                  + CAST(cast(NUM.seikyuusyo_no AS BIGINT) + (SELECT  ")
        cmdTextSb.AppendLine("                                                                     COALESCE(MAX(CAST(seikyuusyo_no AS BIGINT)), 0) ")
        cmdTextSb.AppendLine("                                                                FROM ")
        cmdTextSb.AppendLine("                                                                   jhs_sys.t_seikyuu_kagami) AS varchar) ")
        cmdTextSb.AppendLine("                , 15) seikyuusyo_no ")
        cmdTextSb.AppendLine("         , KGM.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("         , KGM.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("         , KGM.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("         , KGM.gonyuukin_gaku ")
        cmdTextSb.AppendLine("         , KGM.sousai_gaku ")
        cmdTextSb.AppendLine("         , KGM.tyousei_gaku ")
        cmdTextSb.AppendLine("         , KGM.kurikosi_gaku ")
        cmdTextSb.AppendLine("         , KGM.zenkai_goseikyuu_gaku ")
        cmdTextSb.AppendLine("         , KGM.konkai_goseikyuu_gaku ")
        '***********************************************************************
        ' �S������`�F�b�N���́A���ߓ���31�ȊO�̓����\�����+1��������
        '***********************************************************************
        If Not allSakuseiFlg Then
            cmdTextSb.AppendLine("         , jhs_sys.fnGetLastDay(KGM.add_date) matu_date ")
            cmdTextSb.AppendLine("         , ( ")
            cmdTextSb.AppendLine("                   cast(Year(KGM.add_date) AS varchar) ")
            cmdTextSb.AppendLine("               + '-' ")
            cmdTextSb.AppendLine("                + cast(Month(KGM.add_date) AS varchar) ")
            cmdTextSb.AppendLine("                + '-' ")
            cmdTextSb.AppendLine("                + KGM.kaisyuu_day) mst_date ")
        Else
            cmdTextSb.AppendLine("   , (    CASE")
            cmdTextSb.AppendLine("               WHEN ISNULL(KGM.seikyuu_sime_date, 0) = '31'")
            cmdTextSb.AppendLine("               THEN jhs_sys.fnGetLastDay(KGM.add_date)")
            cmdTextSb.AppendLine("               ELSE jhs_sys.fnGetLastDay(DATEADD(MONTH, 1, KGM.add_date))")
            cmdTextSb.AppendLine("          END) matu_date")
            cmdTextSb.AppendLine("   , (    CASE")
            cmdTextSb.AppendLine("               WHEN ISNULL(KGM.seikyuu_sime_date, 0) = '31'")
            cmdTextSb.AppendLine("               THEN (cast(Year(KGM.add_date) AS varchar) + '-' ")
            cmdTextSb.AppendLine("                    + cast(Month(KGM.add_date) AS varchar) + '-' ")
            cmdTextSb.AppendLine("                    + KGM.kaisyuu_day)")
            cmdTextSb.AppendLine("               ELSE (cast(Year(KGM.add_date) AS varchar) + '-' ")
            cmdTextSb.AppendLine("                    + cast(Month(DATEADD(MONTH, 1, KGM.add_date)) AS varchar) + '-' ")
            cmdTextSb.AppendLine("                    + KGM.kaisyuu_day)")
            cmdTextSb.AppendLine("          END) mst_date")
        End If
        cmdTextSb.AppendLine("         , KGM.kaisyuu_day ")
        cmdTextSb.AppendLine("         , KGM.zenkai_seikyuusyo_hak_date ")
        cmdTextSb.AppendLine("         , KGM.tantousya_mei ")
        cmdTextSb.AppendLine("         , KGM.seikyuusyo_inji_bukken_mei_flg ")
        cmdTextSb.AppendLine("         , KGM.nyuukin_kouza_no ")
        cmdTextSb.AppendLine("         , KGM.seikyuu_sime_date ")
        cmdTextSb.AppendLine("         , KGM.senpou_seikyuu_sime_date ")
        cmdTextSb.AppendLine("         , KGM.sousai_flg ")
        cmdTextSb.AppendLine("         , KGM.kaisyuu_yotei_gessuu ")
        cmdTextSb.AppendLine("         , KGM.kaisyuu_yotei_date ")
        cmdTextSb.AppendLine("         , KGM.seikyuusyo_hittyk_date ")
        cmdTextSb.AppendLine("         , KGM.syubetu1 ")
        cmdTextSb.AppendLine("         , KGM.wariai1 ")
        cmdTextSb.AppendLine("         , KGM.tegata_site_gessuu ")
        cmdTextSb.AppendLine("         , KGM.tegata_site_date ")
        cmdTextSb.AppendLine("         , KGM.seikyuusyo_yousi ")
        cmdTextSb.AppendLine("         , KGM.hannyou_cd ")
        cmdTextSb.AppendLine("         , KGM.syubetu2 ")
        cmdTextSb.AppendLine("         , KGM.wariai2 ")
        cmdTextSb.AppendLine("         , KGM.syubetu3 ")
        cmdTextSb.AppendLine("         , KGM.wariai3 ")
        cmdTextSb.AppendLine("         , KGM.ginkou_siten_cd ")
        cmdTextSb.AppendLine("         , KGM.ginkou_siten_mei ")
        cmdTextSb.AppendLine("         , KGM.seikyuu_saki_mei ")
        cmdTextSb.AppendLine("         , KGM.seikyuu_saki_mei2 ")
        cmdTextSb.AppendLine("         , KGM.skysy_soufu_yuubin_no ")
        cmdTextSb.AppendLine("         , KGM.skysy_soufu_jyuusyo1 ")
        cmdTextSb.AppendLine("         , KGM.skysy_soufu_jyuusyo2 ")
        cmdTextSb.AppendLine("         , KGM.skysy_soufu_tel_no ")
        cmdTextSb.AppendLine("         , KGM.uri_ssc ")
        cmdTextSb.AppendLine("      FROM ")
        cmdTextSb.AppendLine("          ##TEMP_KAGAMI_DATA KGM ")
        cmdTextSb.AppendLine("           INNER JOIN ##TEMP_NUMBERING NUM ")
        cmdTextSb.AppendLine("             ON KGM.seikyuu_saki_cd = NUM.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("            AND KGM.seikyuu_saki_brc = NUM.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("            AND KGM.seikyuu_saki_kbn = NUM.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("     ) KGM ")

        Return cmdTextSb.ToString()
    End Function

    ''' <summary>
    ''' �������׃e�[�u���ւ̓o�^����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function insSeikyuuMeisai() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".insSeikyuuMeisai")
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine(" INSERT INTO ")
        cmdTextSb.AppendLine("      jhs_sys.t_seikyuu_meisai ")
        cmdTextSb.AppendLine(" ( ")
        cmdTextSb.AppendLine("      seikyuusyo_no ")
        cmdTextSb.AppendLine("    , denpyou_unique_no ")
        cmdTextSb.AppendLine("    , inji_taisyo_flg ")
        cmdTextSb.AppendLine("    , add_login_user_id ")
        cmdTextSb.AppendLine("    , add_datetime ")
        cmdTextSb.AppendLine(" ) ")
        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("      KGM.seikyuusyo_no ")
        cmdTextSb.AppendLine("    , URI.denpyou_unique_no ")
        cmdTextSb.AppendLine("    , URI.inji_taisyo_flg ")
        cmdTextSb.AppendLine("    , " & DBparamAddLoginUserId & " add_login_user_id ")
        cmdTextSb.AppendLine("    , " & DBparamAddDateTime & "     add_datetime ")
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("     ##TEMP_URI_DATA URI ")
        cmdTextSb.AppendLine("       INNER JOIN ")
        cmdTextSb.AppendLine("           (SELECT ")
        cmdTextSb.AppendLine("                 seikyuu_saki_cd ")
        cmdTextSb.AppendLine("               , seikyuu_saki_brc ")
        cmdTextSb.AppendLine("               , seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("               , max(seikyuusyo_no) seikyuusyo_no ")
        cmdTextSb.AppendLine("            FROM ")
        cmdTextSb.AppendLine("                 jhs_sys.t_seikyuu_kagami KGM ")
        cmdTextSb.AppendLine("            GROUP BY ")
        cmdTextSb.AppendLine("                 seikyuu_saki_cd ")
        cmdTextSb.AppendLine("               , seikyuu_saki_brc ")
        cmdTextSb.AppendLine("               , seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("           ) ")
        cmdTextSb.AppendLine("            KGM ")
        cmdTextSb.AppendLine("         ON URI.seikyuu_saki_cd = KGM.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("        AND URI.seikyuu_saki_brc = KGM.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("        AND URI.seikyuu_saki_kbn = KGM.seikyuu_saki_kbn ")

        Return cmdTextSb.ToString()
    End Function

    ''' <summary>
    ''' ���ߓ������e�[�u���̍X�V����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function updSimeDateRireki() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "updSimeDateRireki")
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine(" UPDATE ")
        cmdTextSb.AppendLine("      jhs_sys.t_seikyuusyo_sime_date_rireki ")
        cmdTextSb.AppendLine(" SET ")
        cmdTextSb.AppendLine("      torikesi = 0 ")
        cmdTextSb.AppendLine("    , seikyuusyo_no = SK.seikyuusyo_no ")
        cmdTextSb.AppendLine("    , upd_login_user_id = " & DBparamAddLoginUserId)
        cmdTextSb.AppendLine("    , upd_datetime = " & DBparamAddDateTime)
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      jhs_sys.t_seikyuusyo_sime_date_rireki SD ")
        cmdTextSb.AppendLine("           LEFT OUTER JOIN jhs_sys.t_seikyuu_kagami SK ")
        cmdTextSb.AppendLine("             ON SK.torikesi = 0 ")
        cmdTextSb.AppendLine("            AND SD.seikyuu_saki_cd = SK.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("            AND SD.seikyuu_saki_brc = SK.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("            AND SD.seikyuu_saki_kbn = SK.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("            AND SK.add_login_user_id = " & DBparamAddLoginUserId)
        cmdTextSb.AppendLine("            AND SK.add_datetime = " & DBparamAddDateTime)
        cmdTextSb.AppendLine("           LEFT OUTER JOIN jhs_sys.m_seikyuu_saki MS ")
        cmdTextSb.AppendLine("             ON SD.seikyuu_saki_cd = MS.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("            AND SD.seikyuu_saki_brc = MS.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("            AND SD.seikyuu_saki_kbn = MS.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine(" WHERE SD.seikyuusyo_hak_nengetu = CONVERT(datetime, CONVERT(varchar, YEAR(" & DBparamSeikyuusyoHakkouDate & ")) + '/' + CONVERT(varchar, MONTH(" & DBparamSeikyuusyoHakkouDate & ")) + '/' + '01', 111) ")
        cmdTextSb.AppendLine("  AND SD.seikyuu_sime_date = RIGHT ('00' + DAY(" & DBparamSeikyuusyoHakkouDate & "), 2) ")
        If Not allSakuseiFlg Then
            '�S�Ώۃ`�F�b�N�Ȃ��̏ꍇ
            cmdTextSb.AppendLine("  AND ((jhs_sys.fnGetLastDay(" & DBparamSeikyuusyoHakkouDate & ") = " & DBparamSeikyuusyoHakkouDate & " ")
            cmdTextSb.AppendLine("  AND jhs_sys.fnGetSeikyuuSimeBi(MS.seikyuu_saki_cd, MS.seikyuu_saki_brc, MS.seikyuu_saki_kbn, " & DBparamSeikyuusyoHakkouDate & ") = '31') ")
            cmdTextSb.AppendLine("   OR jhs_sys.fnGetSeikyuuSimeBi(MS.seikyuu_saki_cd, MS.seikyuu_saki_brc, MS.seikyuu_saki_kbn, " & DBparamSeikyuusyoHakkouDate & ") = DATENAME(DAY, " & DBparamSeikyuusyoHakkouDate & ")) ")
        Else
            '�S�Ώۃ`�F�b�N����̏ꍇ
            cmdTextSb.AppendLine("  AND MS.kessanji_nidosime_flg = '0' ")
        End If
        '***********************************************************************
        ' ��ʏ�Ŏw�肪����ꍇ�A���̐�����̂ݑΏۂƂ���
        '***********************************************************************
        If Not listSeikyuuSaki Is Nothing AndAlso listSeikyuuSaki.Count <> 0 Then

            cmdTextSb.AppendLine(" AND ( ")

            For i As Integer = 0 To listSeikyuuSaki.Count - 1
                If i > 0 Then
                    cmdTextSb.AppendLine(" OR ")
                End If
                cmdTextSb.AppendLine(" ( ")
                cmdTextSb.AppendLine(" MS.seikyuu_saki_cd = " & DBParamSeikyuuSakiCd & ARRAY_SEIKYUU_LINES(i))
                cmdTextSb.AppendLine(" AND MS.seikyuu_saki_brc = " & DBParamSeikyuuSakiBrc & ARRAY_SEIKYUU_LINES(i))
                cmdTextSb.AppendLine(" AND MS.seikyuu_saki_kbn = " & DBParamSeikyuuSakiKbn & ARRAY_SEIKYUU_LINES(i))
                cmdTextSb.AppendLine(" ) ")
            Next
            cmdTextSb.AppendLine(" ) ")
        End If

        Return cmdTextSb.ToString()
    End Function

    ''' <summary>
    ''' ���ߓ������e�[�u���̓o�^����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function insSimeDateRireki() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & "insSimeDateRireki")
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine(" INSERT INTO ")
        cmdTextSb.AppendLine("      jhs_sys.t_seikyuusyo_sime_date_rireki ")
        cmdTextSb.AppendLine(" ( ")
        cmdTextSb.AppendLine("      torikesi ")
        cmdTextSb.AppendLine("    , seikyuu_saki_cd ")
        cmdTextSb.AppendLine("    , seikyuu_saki_brc ")
        cmdTextSb.AppendLine("    , seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("    , seikyuusyo_hak_nengetu ")
        cmdTextSb.AppendLine("    , seikyuu_sime_date ")
        cmdTextSb.AppendLine("    , zen_taisyou_flg ")
        cmdTextSb.AppendLine("    , seikyuusyo_no ")
        cmdTextSb.AppendLine("    , add_login_user_id ")
        cmdTextSb.AppendLine("    , add_datetime ")
        cmdTextSb.AppendLine(" ) ")
        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("      0 AS torikesi ")
        cmdTextSb.AppendLine("    , MS.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("    , MS.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("    , MS.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("    , CONVERT ")
        cmdTextSb.AppendLine(" ( ")
        cmdTextSb.AppendLine("      datetime ")
        cmdTextSb.AppendLine("    , CONVERT(varchar, YEAR(" & DBparamSeikyuusyoHakkouDate & ")) + '/' + CONVERT(varchar, MONTH(" & DBparamSeikyuusyoHakkouDate & ")) + '/' + '01' ")
        cmdTextSb.AppendLine("    , 111 ")
        cmdTextSb.AppendLine(" ) ")
        cmdTextSb.AppendLine("      AS seikyuusyo_hak_nengetu ")
        cmdTextSb.AppendLine("    , RIGHT ")
        cmdTextSb.AppendLine(" ( ")
        cmdTextSb.AppendLine("      '00' + DAY(" & DBparamSeikyuusyoHakkouDate & ") ")
        cmdTextSb.AppendLine("    , 2 ")
        cmdTextSb.AppendLine(" ) ")
        cmdTextSb.AppendLine("      AS seikyuu_sime_date ")
        If Not allSakuseiFlg Then
            '�S�Ώۃ`�F�b�N�Ȃ��̏ꍇ
            cmdTextSb.AppendLine("    , '0' AS zen_taisyou_flg ")
        Else
            '�S�Ώۃ`�F�b�N����̏ꍇ
            cmdTextSb.AppendLine("    , '1' AS zen_taisyou_flg ")
        End If
        cmdTextSb.AppendLine("    , SK.seikyuusyo_no ")
        cmdTextSb.AppendLine("    , " & DBparamAddLoginUserId & " AS add_login_user_id ")
        cmdTextSb.AppendLine("    , " & DBparamAddDateTime & " AS add_datetime ")
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      jhs_sys.m_seikyuu_saki MS ")
        cmdTextSb.AppendLine("           LEFT OUTER JOIN jhs_sys.t_seikyuu_kagami SK ")
        cmdTextSb.AppendLine("             ON SK.torikesi = 0 ")
        cmdTextSb.AppendLine("            AND MS.seikyuu_saki_cd = SK.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("            AND MS.seikyuu_saki_brc = SK.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("            AND MS.seikyuu_saki_kbn = SK.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("            AND SK.add_login_user_id = " & DBparamAddLoginUserId)
        cmdTextSb.AppendLine("            AND SK.add_datetime = " & DBparamAddDateTime)
        cmdTextSb.AppendLine("           LEFT OUTER JOIN jhs_sys.t_seikyuusyo_sime_date_rireki SD ")
        cmdTextSb.AppendLine("            ON MS.seikyuu_saki_cd = SD.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("            AND MS.seikyuu_saki_brc = SD.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("            AND MS.seikyuu_saki_kbn = SD.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("            AND SD.seikyuusyo_hak_nengetu = CONVERT(datetime, CONVERT(varchar, YEAR(" & DBparamSeikyuusyoHakkouDate & ")) + '/' + CONVERT(varchar, MONTH(" & DBparamSeikyuusyoHakkouDate & ")) + '/' + '01', 111) ")
        cmdTextSb.AppendLine("            AND SD.seikyuu_sime_date = RIGHT ('00' + DAY(" & DBparamSeikyuusyoHakkouDate & "), 2) ")
        cmdTextSb.AppendLine(" WHERE SD.rireki_no is null ")
        If Not allSakuseiFlg Then
            '�S�Ώۃ`�F�b�N�Ȃ��̏ꍇ
            cmdTextSb.AppendLine("  AND ((jhs_sys.fnGetLastDay(" & DBparamSeikyuusyoHakkouDate & ") = " & DBparamSeikyuusyoHakkouDate & " ")
            cmdTextSb.AppendLine("  AND jhs_sys.fnGetSeikyuuSimeBi(MS.seikyuu_saki_cd, MS.seikyuu_saki_brc, MS.seikyuu_saki_kbn, " & DBparamSeikyuusyoHakkouDate & ") = '31') ")
            cmdTextSb.AppendLine("   OR jhs_sys.fnGetSeikyuuSimeBi(MS.seikyuu_saki_cd, MS.seikyuu_saki_brc, MS.seikyuu_saki_kbn, " & DBparamSeikyuusyoHakkouDate & ") = DATENAME(DAY, " & DBparamSeikyuusyoHakkouDate & ")) ")
        Else
            '�S�Ώۃ`�F�b�N����̏ꍇ
            cmdTextSb.AppendLine("  AND MS.kessanji_nidosime_flg = '0' ")
        End If
        '***********************************************************************
        ' ��ʏ�Ŏw�肪����ꍇ�A���̐�����̂ݑΏۂƂ���
        '***********************************************************************
        If Not listSeikyuuSaki Is Nothing AndAlso listSeikyuuSaki.Count <> 0 Then

            cmdTextSb.AppendLine(" AND ( ")

            For i As Integer = 0 To listSeikyuuSaki.Count - 1
                If i > 0 Then
                    cmdTextSb.AppendLine(" OR ")
                End If
                cmdTextSb.AppendLine(" ( ")
                cmdTextSb.AppendLine(" MS.seikyuu_saki_cd = " & DBParamSeikyuuSakiCd & ARRAY_SEIKYUU_LINES(i))
                cmdTextSb.AppendLine(" AND MS.seikyuu_saki_brc = " & DBParamSeikyuuSakiBrc & ARRAY_SEIKYUU_LINES(i))
                cmdTextSb.AppendLine(" AND MS.seikyuu_saki_kbn = " & DBParamSeikyuuSakiKbn & ARRAY_SEIKYUU_LINES(i))
                cmdTextSb.AppendLine(" ) ")
            Next
            cmdTextSb.AppendLine(" ) ")
        End If

        Return cmdTextSb.ToString()
    End Function

    ''' <summary>
    ''' ���������s�Ώۂ̐����N�����ŏ����ߓ����擾����(����f�[�^��Ώ�)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getMinSeikyuuSimeDateUriageSql() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getMinSeikyuuSimeDateUriageSql")
        Dim sqlSb As New StringBuilder()

        sqlSb.AppendLine("SELECT")
        sqlSb.AppendLine("    min( ")
        sqlSb.AppendLine("        jhs_sys.fnGetSeikyuuSimeDate( ")
        sqlSb.AppendLine("            seikyuu_saki_cd")
        sqlSb.AppendLine("            , seikyuu_saki_brc")
        sqlSb.AppendLine("            , seikyuu_saki_kbn")
        sqlSb.AppendLine("            , seikyuu_date")
        sqlSb.AppendLine("        )")
        sqlSb.AppendLine("    ) min_seikyuu_sime_bi ")
        sqlSb.AppendLine("FROM")
        sqlSb.AppendLine("    ##TEMP_URI_DATA")
        sqlSb.AppendLine("")

        Return sqlSb.ToString()
    End Function

    ''' <summary>
    ''' ���������s�Ώۂ̐����N�����ŏ����ߓ����擾����(�����f�[�^��Ώ�)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getMinSeikyuuSimeDateNyuukinSql() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getMinSeikyuuSimeDateNyuukinSql")
        Dim sqlSb As New StringBuilder()

        sqlSb.AppendLine("SELECT")
        sqlSb.AppendLine("    min( ")
        sqlSb.AppendLine("        jhs_sys.fnGetSeikyuuSimeDate( ")
        sqlSb.AppendLine("            tn.seikyuu_saki_cd")
        sqlSb.AppendLine("            , tn.seikyuu_saki_brc")
        sqlSb.AppendLine("            , tn.seikyuu_saki_kbn")
        sqlSb.AppendLine("            , tn.nyuukin_date")
        sqlSb.AppendLine("        )")
        sqlSb.AppendLine("    ) ")
        sqlSb.AppendLine("FROM")
        sqlSb.AppendLine("    jhs_sys.m_seikyuu_saki ms ")
        sqlSb.AppendLine("    INNER JOIN jhs_sys.t_nyuukin_data tn ")
        sqlSb.AppendLine("        ON ms.seikyuu_saki_cd = tn.seikyuu_saki_cd ")
        sqlSb.AppendLine("        AND ms.seikyuu_saki_brc = tn.seikyuu_saki_brc ")
        sqlSb.AppendLine("        AND ms.seikyuu_saki_kbn = tn.seikyuu_saki_kbn ")
        sqlSb.AppendLine("    LEFT OUTER JOIN ##TEMP_ZENKAI_KAGAMI tzk ")
        sqlSb.AppendLine("        ON tn.seikyuu_saki_cd = tzk.seikyuu_saki_cd ")
        sqlSb.AppendLine("        AND tn.seikyuu_saki_brc = tzk.seikyuu_saki_brc ")
        sqlSb.AppendLine("        AND tn.seikyuu_saki_kbn = tzk.seikyuu_saki_kbn ")
        sqlSb.AppendLine("WHERE")
        sqlSb.AppendLine("    ms.torikesi = 0 ")
        sqlSb.AppendLine("    AND ISNULL(tzk.seikyuusyo_hak_date, 0) < tn.nyuukin_date")
        '***********************************************************************
        ' ��ʏ�Ŏw�肪����ꍇ�A���̐�����̂ݑΏۂƂ���
        '***********************************************************************
        If Not listSeikyuuSaki Is Nothing AndAlso listSeikyuuSaki.Count <> 0 Then
            sqlSb.AppendLine(" AND ( ")

            For i As Integer = 0 To listSeikyuuSaki.Count - 1
                If i > 0 Then
                    sqlSb.AppendLine(" OR ")
                End If
                sqlSb.AppendLine(" ( ")
                sqlSb.AppendLine(" ms.seikyuu_saki_cd = " & DBParamSeikyuuSakiCd & ARRAY_SEIKYUU_LINES(i))
                sqlSb.AppendLine(" AND ms.seikyuu_saki_brc = " & DBParamSeikyuuSakiBrc & ARRAY_SEIKYUU_LINES(i))
                sqlSb.AppendLine(" AND ms.seikyuu_saki_kbn = " & DBParamSeikyuuSakiKbn & ARRAY_SEIKYUU_LINES(i))
                sqlSb.AppendLine(" ) ")
            Next
            sqlSb.AppendLine(" ) ")
        End If
        sqlSb.AppendLine("")

        Return sqlSb.ToString()
    End Function

End Class
