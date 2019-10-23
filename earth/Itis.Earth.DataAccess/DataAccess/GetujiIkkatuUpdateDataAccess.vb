Imports System.text
Imports System.Data.SqlClient
''' <summary>
''' �����f�[�^���ꊇ�X�V����f�[�^�A�N�Z�X�N���X�ł��B
''' </summary>
''' <remarks></remarks>
Public Class GetujiIkkatuUpdateDataAccess
    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "SQL�p�����[�^"
    Private Const paramProcType As String = "@PROCTYPE"
    Private Const paramBunruiCd As String = "@BUNRUICD"
    Private Const paramUriKeijyouFlg As String = "@URIKEIJYOUFLG"
    Private Const paramDateFrom As String = "@DATEFROM"
    Private Const paramDateTo As String = "@DATETO"
#End Region

#Region "�萔"
    Private Const KAIRYOU_KOUJI As String = "130"
    Private Const TUIKA_KAIRYOU_KOUJI As String = "140"
#End Region

#Region "�񋓌^"
    ''' <summary>
    ''' �X�V�Ώۓ��t
    ''' </summary>
    ''' <remarks></remarks>
    Enum enumTgtDt
        ''' <summary>
        ''' ����N����
        ''' </summary>
        ''' <remarks></remarks>
        Uriage = 0
        ''' <summary>
        ''' ���������s��
        ''' </summary>
        ''' <remarks></remarks>
        Seikyuu = 1
    End Enum
    ''' <summary>
    ''' �X�V�Ώۃe�[�u��
    ''' </summary>
    ''' <remarks></remarks>
    Enum enumTgtTbl
        ''' <summary>
        ''' �@�ʐ����e�[�u��
        ''' </summary>
        ''' <remarks></remarks>
        Teibetu = 0
        ''' <summary>
        ''' �X�ʏ��������e�[�u��
        ''' </summary>
        ''' <remarks></remarks>
        TenbetuSyoki = 1
        ''' <summary>
        ''' �X�ʐ����e�[�u��
        ''' </summary>
        ''' <remarks></remarks>
        Tenbetu = 2
    End Enum
#End Region

#Region "�����o�ϐ�"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "��������"
    ''' <summary>
    ''' �X�V�Ώۂ�A�g�p�e���|�����[�e�[�u���ɓo�^����SQL���쐬���܂�
    ''' </summary>
    ''' M<param name="strTmpTableName">�e���|�����[�e�[�u����</param>
    ''' <param name="strBunruiCd">���ރR�[�h</param>
    ''' <param name="enTgtDt">�X�V�Ώ۔��f�񋓑́i0�F����N�����^1�F���������s���j</param>
    ''' <returns>�X�V�Ώۂ�A�g�p�e���|�����[�e�[�u���ɓo�^����SQL�N�G��</returns>
    ''' <remarks></remarks>
    Public Function GetInsertSqlRenkeiTmpForGetujiUpd(ByVal strTmpTableName As String _
                                                    , ByVal strBunruiCd As String _
                                                    , ByVal enTgtDt As enumTgtDt) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetInsertSqlRenkeiTmpForGetujiUpd" _
                                                , strTmpTableName _
                                                , strBunruiCd _
                                                , enTgtDt)
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" INSERT INTO " & strTmpTableName)
        cmdTextSb.Append(" SELECT T.kbn ")
        cmdTextSb.Append("       ,T.hosyousyo_no ")
        cmdTextSb.Append("       ,T.bunrui_cd ")
        cmdTextSb.Append("       ,T.gamen_hyouji_no ")
        cmdTextSb.Append("       ,ISNULL(R.renkei_siji_cd, -1) renkei_siji_cd ")
        cmdTextSb.Append("       ,ISNULL(R.sousin_jyky_cd, -1) sousin_jyky_cd ")
        cmdTextSb.Append("       ,R.sousin_kanry_datetime ")
        cmdTextSb.Append("       ," & paramProcType)
        cmdTextSb.Append("   FROM ")
        cmdTextSb.Append("        t_teibetu_seikyuu T WITH(UPDLOCK) ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        t_jiban           J ")
        cmdTextSb.Append("     ON T.kbn = J.kbn ")
        cmdTextSb.Append("    AND T.hosyousyo_no = J.hosyousyo_no ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        t_teibetu_seikyuu_renkei   R ")
        cmdTextSb.Append("     ON T.kbn              = R.kbn ")
        cmdTextSb.Append("    AND T.hosyousyo_no     = R.hosyousyo_no ")
        cmdTextSb.Append("    AND T.bunrui_cd        = R.bunrui_cd ")
        cmdTextSb.Append("    AND T.gamen_hyouji_no  = R.gamen_hyouji_no ")

        cmdTextSb.Append("  WHERE T.bunrui_cd = " & paramBunruiCd)
        cmdTextSb.Append("    AND T.uri_keijyou_flg = " & paramUriKeijyouFlg)
        Select Case strBunruiCd
            Case KAIRYOU_KOUJI
                Select Case enTgtDt
                    Case enumTgtDt.Uriage
                        cmdTextSb.Append("    AND LEFT(CONVERT(CHAR, T.uri_date,112),6) > LEFT(CONVERT(CHAR, J.kairy_koj_date,112),6) ")
                        cmdTextSb.Append("    AND (T.uri_date BETWEEN " & paramDateFrom & " AND " & paramDateTo & ")")
                    Case enumTgtDt.Seikyuu
                        cmdTextSb.Append("    AND LEFT(CONVERT(CHAR, T.seikyuusyo_hak_date,112),6) > LEFT(CONVERT(CHAR, J.kairy_koj_date,112),6) ")
                        cmdTextSb.Append("    AND (T.seikyuusyo_hak_date BETWEEN " & paramDateFrom & " AND " & paramDateTo & ")")
                End Select
            Case TUIKA_KAIRYOU_KOUJI
                Select Case enTgtDt
                    Case enumTgtDt.Uriage
                        cmdTextSb.Append("    AND LEFT(CONVERT(CHAR, T.uri_date,112),6) > LEFT(CONVERT(CHAR, J.t_koj_date,112),6) ")
                        cmdTextSb.Append("    AND (T.uri_date BETWEEN " & paramDateFrom & " AND " & paramDateTo & ")")
                    Case enumTgtDt.Seikyuu
                        cmdTextSb.Append("    AND LEFT(CONVERT(CHAR, T.seikyuusyo_hak_date,112),6) > LEFT(CONVERT(CHAR, J.t_koj_date,112),6) ")
                        cmdTextSb.Append("    AND (T.seikyuusyo_hak_date BETWEEN " & paramDateFrom & " AND " & paramDateTo & ")")
                End Select
        End Select

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' SQL�p�����[�^�[���쐬
    ''' </summary>
    ''' <param name="intProcType">�A�g���(�X�V�F0, �V�K�F1, �폜�F9)</param>
    ''' <param name="strBunruiCd">���ރR�[�h</param>
    ''' <param name="intUriKeijouFlg">����v��FLG</param>
    ''' <param name="dtFrom">��ʓ��tFrom</param>
    ''' <param name="dtTo">��ʓ��tTo</param>
    ''' <returns>SQL�p�����[�^</returns>
    ''' <remarks></remarks>
    Public Function GetRenkeiCmdParamsForGetujiUpd(ByVal intProcType As EarthConst.enSqlTypeFlg _
                                    , ByVal strBunruiCd As String _
                                    , ByVal intUriKeijouFlg As Integer _
                                    , ByVal dtFrom As Date _
                                    , ByVal dtTo As Date) As SqlParameter()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetRenkeiCmdParamsForGetujiUpd" _
                                                , intProcType _
                                                , strBunruiCd _
                                                , intUriKeijouFlg _
                                                , dtFrom _
                                                , dtTo)
        Dim cmdParams() As SqlParameter

        ' �p�����[�^���쐬
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(paramProcType, SqlDbType.Int, 4, intProcType), _
            SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, strBunruiCd), _
            SQLHelper.MakeParam(paramUriKeijyouFlg, SqlDbType.Int, 1, intUriKeijouFlg), _
            SQLHelper.MakeParam(paramDateFrom, SqlDbType.DateTime, 16, dtFrom), _
            SQLHelper.MakeParam(paramDateTo, SqlDbType.DateTime, 16, dtTo)}

        Return cmdParams

    End Function

    ''' <summary>
    ''' �@�ʐ����e�[�u�����ꊇ�X�V���܂��B
    ''' </summary>
    ''' <param name="strBunruiCd">���ރR�[�h</param>
    ''' <param name="intUriKeijouFlg">����v��FLG</param>
    ''' <param name="dtFrom">��ʓ��tFrom</param>
    ''' <param name="dtTo">��ʓ��tTo</param>
    ''' <param name="updLoginUserId">�X�V���O�C�����[�U�[</param>
    ''' <param name="enTgtDt">�X�V�Ώ۔��f�񋓑́i0�F����N�����^1�F���������s���j</param>
    ''' <returns>�X�V�Ώۂ̃L�[���i�[�����f�[�^�e�[�u��</returns>
    ''' <remarks>�X�V����</remarks>
    Public Function UpdTeibetuSeikyuuDataAll(ByVal strBunruiCd As String _
                                            , ByVal intUriKeijouFlg As Integer _
                                            , ByVal dtFrom As Date _
                                            , ByVal dtTo As Date _
                                            , ByVal updLoginUserId As String _
                                            , ByVal enTgtDt As enumTgtDt) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTargetKey" _
                                                , strBunruiCd _
                                                , intUriKeijouFlg _
                                                , dtFrom _
                                                , dtTo _
                                                , enTgtDt)

        Dim teibetuDataSet As New TeibetuRenkeiDataSet
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �X�V�pSQL�ݒ�
        cmdTextSb.Append("UPDATE t_teibetu_seikyuu ")
        cmdTextSb.Append("   SET ")
        Select Case enTgtDt
            Case enumTgtDt.Uriage
                cmdTextSb.Append("       uri_date            = DATEADD(dd,-1,CAST((LEFT(CONVERT(CHAR,T1.uri_date,112),6) + '01') AS DATETIME)) ")
                cmdTextSb.Append("      ,denpyou_uri_date    = DATEADD(dd,-1,CAST((LEFT(CONVERT(CHAR,T1.uri_date,112),6) + '01') AS DATETIME)) ")
                cmdTextSb.Append("      ,denpyou_siire_date    = DATEADD(dd,-1,CAST((LEFT(CONVERT(CHAR,T1.uri_date,112),6) + '01') AS DATETIME)) ")
            Case enumTgtDt.Seikyuu
                cmdTextSb.Append("       seikyuusyo_hak_date = DATEADD(dd,-1,CAST((LEFT(CONVERT(CHAR,T1.seikyuusyo_hak_date,112),6) + '01') AS DATETIME)) ")
        End Select
        cmdTextSb.Append("      ,upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("      ,upd_datetime      = @UPDDATETIME ")

        '�X�V�Ώ�KEY�擾SQL
        cmdTextSb.Append(" FROM t_teibetu_seikyuu T1 ")
        cmdTextSb.Append(" INNER JOIN ( ")
        cmdTextSb.Append(" SELECT T.kbn ")
        cmdTextSb.Append("      ,T.hosyousyo_no ")
        cmdTextSb.Append("      ,T.bunrui_cd ")
        cmdTextSb.Append("      ,T.gamen_hyouji_no ")
        cmdTextSb.Append("  FROM ")
        cmdTextSb.Append("       t_teibetu_seikyuu T WITH(UPDLOCK) ")
        cmdTextSb.Append("  LEFT OUTER JOIN ")
        cmdTextSb.Append("       t_jiban           J ")
        cmdTextSb.Append("    ON T.kbn = J.kbn ")
        cmdTextSb.Append("   AND T.hosyousyo_no = J.hosyousyo_no ")
        cmdTextSb.Append(" WHERE T.bunrui_cd = @BUNRUICD ")
        cmdTextSb.Append("   AND T.uri_keijyou_flg = @URIKEIJYOUFLG ")
        Select Case strBunruiCd
            Case KAIRYOU_KOUJI
                Select Case enTgtDt
                    Case enumTgtDt.Uriage
                        cmdTextSb.Append("   AND LEFT(CONVERT(CHAR, T.uri_date,112),6) > LEFT(CONVERT(CHAR, J.kairy_koj_date,112),6) ")
                        cmdTextSb.Append("   AND (T.uri_date BETWEEN @FROMDATE AND @TODATE)")
                    Case enumTgtDt.Seikyuu
                        cmdTextSb.Append("   AND LEFT(CONVERT(CHAR, T.seikyuusyo_hak_date,112),6) > LEFT(CONVERT(CHAR, J.kairy_koj_date,112),6) ")
                        cmdTextSb.Append("   AND (T.seikyuusyo_hak_date BETWEEN @FROMDATE AND @TODATE)")
                End Select
            Case TUIKA_KAIRYOU_KOUJI
                Select Case enTgtDt
                    Case enumTgtDt.Uriage
                        cmdTextSb.Append("   AND LEFT(CONVERT(CHAR, T.uri_date,112),6) > LEFT(CONVERT(CHAR, J.t_koj_date,112),6) ")
                        cmdTextSb.Append("   AND (T.uri_date BETWEEN @FROMDATE AND @TODATE)")
                    Case enumTgtDt.Seikyuu
                        cmdTextSb.Append("   AND LEFT(CONVERT(CHAR, T.seikyuusyo_hak_date,112),6) > LEFT(CONVERT(CHAR, J.t_koj_date,112),6) ")
                        cmdTextSb.Append("   AND (T.seikyuusyo_hak_date BETWEEN @FROMDATE AND @TODATE)")
                End Select
        End Select
        cmdTextSb.Append(" ) T2 ON ")
        cmdTextSb.Append("   T1.kbn = T2.kbn ")
        cmdTextSb.Append("   AND T1.hosyousyo_no = T2.hosyousyo_no ")
        cmdTextSb.Append("   AND T1.bunrui_cd = T2.bunrui_cd ")
        cmdTextSb.Append("   AND T1.gamen_hyouji_no = T2.gamen_hyouji_no ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, strBunruiCd), _
        SQLHelper.MakeParam("@URIKEIJYOUFLG", SqlDbType.Int, 1, intUriKeijouFlg), _
        SQLHelper.MakeParam("@FROMDATE", SqlDbType.DateTime, 16, dtFrom), _
        SQLHelper.MakeParam("@TODATE", SqlDbType.DateTime, 16, dtTo), _
        SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
        SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function

#End Region

#Region "���Z������"

    ''' <summary>
    ''' �X�V�Ώۃe�[�u���̍X�V�Ώۃ��R�[�h����L�[���擾���܂��B
    ''' </summary>
    ''' <param name="strTgtTbl">�X�V�Ώۃe�[�u��</param>
    ''' <param name="dtUriFrom">����N����FROM</param>
    ''' <param name="dtUriTo">����N����TO</param>
    ''' <param name="dtSeikyuuFrom">���������s��FROM</param>
    ''' <param name="dtSeikyuuTo">���������s��TO</param>
    ''' <param name="enTgtTbl">�X�V�Ώ۔��f�񋓑́i0�F�@�ʐ����e�[�u���^1�F�X�ʏ��������e�[�u���^2�F�X�ʐ����e�[�u���j</param>
    ''' <returns>�X�V�Ώۂ̃L�[���i�[�����f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTargetKeyAnyTable(ByVal strTgtTbl As String _
                                        , ByVal dtUriFrom As Date _
                                        , ByVal dtUriTo As Date _
                                        , ByVal dtSeikyuuFrom As Date _
                                        , ByVal dtSeikyuuTo As Date _
                                        , ByVal enTgtTbl As enumTgtTbl) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTargetKeyAnyTable" _
                                                    , strTgtTbl _
                                                    , dtUriFrom _
                                                    , dtUriTo _
                                                    , dtSeikyuuFrom _
                                                    , dtSeikyuuTo _
                                                    , enTgtTbl)

        Dim teibetuDataSet As New TeibetuRenkeiDataSet
        Dim tenbetuDataSet As New TenbetuRenkeiDataSet
        Dim tenbetuSyokiDataSet As New TenbetuSyokiRenkeiDataSet
        Dim updTgtKeyTable As DataTable = Nothing
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        '�X�V�Ώێ擾�pSQL�ݒ�
        cmdTextSb.Append(" SELECT ")
        Select Case enTgtTbl
            Case enumTgtTbl.Teibetu
                cmdTextSb.Append("        kbn ")
                cmdTextSb.Append("       ,hosyousyo_no ")
                cmdTextSb.Append("       ,bunrui_cd ")
                cmdTextSb.Append("       ,gamen_hyouji_no ")
                cmdTextSb.Append("   FROM t_teibetu_seikyuu WITH(UPDLOCK) ")
            Case enumTgtTbl.TenbetuSyoki
                cmdTextSb.Append("        mise_cd ")
                cmdTextSb.Append("       ,bunrui_cd ")
                cmdTextSb.Append("   FROM t_tenbetu_syoki_seikyuu WITH(UPDLOCK) ")
            Case enumTgtTbl.Tenbetu
                cmdTextSb.Append("        mise_cd ")
                cmdTextSb.Append("       ,bunrui_cd ")
                cmdTextSb.Append("       ,nyuuryoku_date ")
                cmdTextSb.Append("       ,nyuuryoku_date_no ")
                cmdTextSb.Append("   FROM t_tenbetu_seikyuu WITH(UPDLOCK) ")
        End Select

        cmdTextSb.Append("  WHERE uri_keijyou_flg = 0 ")
        cmdTextSb.Append("    AND (uri_date BETWEEN @URIDATEFROM AND @URIDATETO) ")
        cmdTextSb.Append("    AND (seikyuusyo_hak_date BETWEEN @SEIKYUUFROM AND @SEIKYUUTO) ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@URIDATEFROM", SqlDbType.DateTime, 16, dtUriFrom), _
        SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtUriTo), _
        SQLHelper.MakeParam("@SEIKYUUFROM", SqlDbType.DateTime, 16, dtSeikyuuFrom), _
        SQLHelper.MakeParam("@SEIKYUUTO", SqlDbType.DateTime, 16, dtSeikyuuTo)}

        Select Case enTgtTbl
            Case enumTgtTbl.Teibetu
                ' �������s
                SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
                    teibetuDataSet, teibetuDataSet.TeibetuKey.TableName, cmdParams)
                updTgtKeyTable = teibetuDataSet.TeibetuKey
            Case enumTgtTbl.TenbetuSyoki
                ' �������s
                SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
                    tenbetuSyokiDataSet, tenbetuSyokiDataSet.TenbetuSyokiKey.TableName, cmdParams)
                updTgtKeyTable = tenbetuSyokiDataSet.TenbetuSyokiKey
            Case enumTgtTbl.Tenbetu
                ' �������s
                SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
                    tenbetuDataSet, tenbetuDataSet.TenbetuKey.TableName, cmdParams)
                updTgtKeyTable = tenbetuDataSet.TenbetuKey
        End Select

        Return updTgtKeyTable
    End Function

    ''' <summary>
    ''' �@�ʐ����e�[�u�����X�V0
    ''' ��������ݒ�ς̑S�Ă̓@�ʐ����f�[�^
    ''' </summary>
    ''' <param name="dtUriFrom">����N����FROM</param>
    ''' <param name="dtUriTo">����N����TO</param>
    ''' <param name="dtSeikyuuFrom">���������s��FROM</param>
    ''' <param name="dtSeikyuuTo">���������s��TO</param>
    ''' <param name="updLoginUserId">�X�V���O�C�����[�U�[</param>
    ''' <param name="dtUpdDate">�ݒ���t</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdTeibetuSeikyuuDataForKessan0(ByVal dtUriFrom As Date _
                                                    , ByVal dtUriTo As Date _
                                                    , ByVal dtSeikyuuFrom As Date _
                                                    , ByVal dtSeikyuuTo As Date _
                                                    , ByVal dtUpdDate As Date _
                                                    , ByVal updLoginUserId As String _
                                                    , ByVal dtNowDateTime As DateTime) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdTeibetuSeikyuuDataForKessan0" _
                                                    , dtUriFrom _
                                                    , dtUriTo _
                                                    , dtSeikyuuFrom _
                                                    , dtSeikyuuTo _
                                                    , dtUpdDate _
                                                    , updLoginUserId _
                                                    , dtNowDateTime)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �X�V�pSQL�ݒ�
        cmdTextSb.Append(" UPDATE ")
        cmdTextSb.Append("      t_teibetu_seikyuu ")
        cmdTextSb.Append(" SET ")
        cmdTextSb.Append("      seikyuusyo_hak_date = @SEIKYUUSYOHAKDATE ")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_teibetu_seikyuu AS TTS ")
        cmdTextSb.Append("           INNER JOIN m_seikyuu_saki AS MSS ")
        cmdTextSb.Append("             ON TTS.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
        cmdTextSb.Append("            AND TTS.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
        cmdTextSb.Append("            AND TTS.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      TTS.uri_keijyou_flg = '0' ")
        cmdTextSb.Append("  AND (TTS.uri_date BETWEEN @URIDATEFROM AND @URIDATETO) ")
        cmdTextSb.Append("  AND (TTS.seikyuusyo_hak_date BETWEEN @SEIKYUUFROM AND @SEIKYUUTO) ")
        cmdTextSb.Append("  AND TTS.seikyuu_saki_cd IS NOT NULL ")
        cmdTextSb.Append("  AND TTS.seikyuu_saki_brc IS NOT NULL ")
        cmdTextSb.Append("  AND TTS.seikyuu_saki_kbn IS NOT NULL ")
        cmdTextSb.Append("  AND ISNULL(MSS.kessanji_nidosime_flg, 0) = '0' ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@URIDATEFROM", SqlDbType.DateTime, 16, dtUriFrom), _
        SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtUriTo), _
        SQLHelper.MakeParam("@SEIKYUUFROM", SqlDbType.DateTime, 16, dtSeikyuuFrom), _
        SQLHelper.MakeParam("@SEIKYUUTO", SqlDbType.DateTime, 16, dtSeikyuuTo), _
        SQLHelper.MakeParam("@SEIKYUUSYOHAKDATE", SqlDbType.DateTime, 16, dtUpdDate), _
        SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
        SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, dtNowDateTime)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function


    ''' <summary>
    ''' �@�ʐ����e�[�u�����X�V1
    ''' �����ރR�[�h=100,110,115,120,190(�������i)�ł��A�����悢���ꂩ���ݒ�
    ''' </summary>
    ''' <param name="dtUriFrom">����N����FROM</param>
    ''' <param name="dtUriTo">����N����TO</param>
    ''' <param name="dtSeikyuuFrom">���������s��FROM</param>
    ''' <param name="dtSeikyuuTo">���������s��TO</param>
    ''' <param name="updLoginUserId">�X�V���O�C�����[�U�[</param>
    ''' <param name="dtUpdDate">�ݒ���t</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdTeibetuSeikyuuDataForKessan1(ByVal dtUriFrom As Date _
                                                    , ByVal dtUriTo As Date _
                                                    , ByVal dtSeikyuuFrom As Date _
                                                    , ByVal dtSeikyuuTo As Date _
                                                    , ByVal dtUpdDate As Date _
                                                    , ByVal updLoginUserId As String _
                                                    , ByVal dtNowDateTime As DateTime) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdTeibetuSeikyuuDataForKessan1" _
                                                    , dtUriFrom _
                                                    , dtUriTo _
                                                    , dtSeikyuuFrom _
                                                    , dtSeikyuuTo _
                                                    , dtUpdDate _
                                                    , updLoginUserId _
                                                    , dtNowDateTime)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �X�V�pSQL�ݒ�
        cmdTextSb.Append(" UPDATE ")
        cmdTextSb.Append("      t_teibetu_seikyuu ")
        cmdTextSb.Append(" SET ")
        cmdTextSb.Append("      seikyuusyo_hak_date = @SEIKYUUSYOHAKDATE ")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_teibetu_seikyuu AS TTS ")
        cmdTextSb.Append("           LEFT OUTER JOIN t_jiban AS TJ ")
        cmdTextSb.Append("             ON TTS.kbn = TJ.kbn ")
        cmdTextSb.Append("            AND TTS.hosyousyo_no = TJ.hosyousyo_no ")
        cmdTextSb.Append("           LEFT OUTER JOIN v_syouhin_seikyuusaki_kameiten AS VSS ")
        cmdTextSb.Append("             ON TTS.syouhin_cd = VSS.syouhin_cd ")
        cmdTextSb.Append("            AND TJ.kameiten_cd = VSS.kameiten_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_seikyuu_saki MSS ")
        cmdTextSb.Append("             ON VSS.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
        cmdTextSb.Append("            AND VSS.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
        cmdTextSb.Append("            AND VSS.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      TTS.uri_keijyou_flg = '0' ")
        cmdTextSb.Append("  AND (TTS.uri_date BETWEEN @URIDATEFROM AND @URIDATETO) ")
        cmdTextSb.Append("  AND (TTS.seikyuusyo_hak_date BETWEEN @SEIKYUUFROM AND @SEIKYUUTO) ")
        cmdTextSb.Append("  AND (TTS.seikyuu_saki_cd IS NULL ")
        cmdTextSb.Append("   OR TTS.seikyuu_saki_brc IS NULL ")
        cmdTextSb.Append("   OR TTS.seikyuu_saki_kbn IS NULL) ")
        cmdTextSb.Append("  AND TTS.bunrui_cd IN('100','110','115','120','190','150','160','170','180') ")
        cmdTextSb.Append("  AND ISNULL(MSS.kessanji_nidosime_flg, 0) = '0' ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@URIDATEFROM", SqlDbType.DateTime, 16, dtUriFrom), _
        SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtUriTo), _
        SQLHelper.MakeParam("@SEIKYUUFROM", SqlDbType.DateTime, 16, dtSeikyuuFrom), _
        SQLHelper.MakeParam("@SEIKYUUTO", SqlDbType.DateTime, 16, dtSeikyuuTo), _
        SQLHelper.MakeParam("@SEIKYUUSYOHAKDATE", SqlDbType.DateTime, 16, dtUpdDate), _
        SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
        SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, dtNowDateTime)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' �@�ʐ����e�[�u�����X�V2
    ''' </summary>
    ''' <param name="dtUriFrom">����N����FROM</param>
    ''' <param name="dtUriTo">����N����TO</param>
    ''' <param name="dtSeikyuuFrom">���������s��FROM</param>
    ''' <param name="dtSeikyuuTo">���������s��TO</param>
    ''' <param name="updLoginUserId">�X�V���O�C�����[�U�[</param>
    ''' <param name="dtUpdDate">�ݒ���t</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdTeibetuSeikyuuDataForKessan2(ByVal dtUriFrom As Date _
                                                    , ByVal dtUriTo As Date _
                                                    , ByVal dtSeikyuuFrom As Date _
                                                    , ByVal dtSeikyuuTo As Date _
                                                    , ByVal dtUpdDate As Date _
                                                    , ByVal updLoginUserId As String _
                                                    , ByVal dtNowDateTime As DateTime) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdTeibetuSeikyuuDataForKessan2" _
                                                    , dtUriFrom _
                                                    , dtUriTo _
                                                    , dtSeikyuuFrom _
                                                    , dtSeikyuuTo _
                                                    , dtUpdDate _
                                                    , updLoginUserId _
                                                    , dtNowDateTime)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �X�V�pSQL�ݒ�
        cmdTextSb.Append(" UPDATE ")
        cmdTextSb.Append("      t_teibetu_seikyuu ")
        cmdTextSb.Append(" SET ")
        cmdTextSb.Append("      seikyuusyo_hak_date = @SEIKYUUSYOHAKDATE ")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_teibetu_seikyuu AS TTS ")
        cmdTextSb.Append("           INNER JOIN t_jiban AS TJ ")
        cmdTextSb.Append("             ON TTS.kbn = TJ.kbn ")
        cmdTextSb.Append("            AND TTS.hosyousyo_no = TJ.hosyousyo_no ")
        cmdTextSb.Append("           LEFT OUTER JOIN v_syouhin_siire_seikyuu_saki_jiban_tyskaisya VSS ")
        cmdTextSb.Append("             ON TTS.kbn = VSS.kbn ")
        cmdTextSb.Append("            AND TTS.hosyousyo_no = VSS.hosyousyo_no ")
        cmdTextSb.Append("            AND TTS.syouhin_cd = VSS.syouhin_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_seikyuu_saki MSS ")
        cmdTextSb.Append("             ON VSS.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
        cmdTextSb.Append("            AND VSS.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
        cmdTextSb.Append("            AND VSS.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      TTS.uri_keijyou_flg = '0' ")
        cmdTextSb.Append("  AND (TTS.uri_date BETWEEN @URIDATEFROM AND @URIDATETO) ")
        cmdTextSb.Append("  AND (TTS.seikyuusyo_hak_date BETWEEN @SEIKYUUFROM AND @SEIKYUUTO) ")
        cmdTextSb.Append("  AND (TTS.seikyuu_saki_cd IS NULL ")
        cmdTextSb.Append("   OR TTS.seikyuu_saki_brc IS NULL ")
        cmdTextSb.Append("   OR TTS.seikyuu_saki_kbn IS NULL) ")
        cmdTextSb.Append("  AND TTS.bunrui_cd = '130' ")
        cmdTextSb.Append("  AND TJ.koj_gaisya_seikyuu_umu = '1' ")
        cmdTextSb.Append("  AND ISNULL(MSS.kessanji_nidosime_flg, 0) = '0' ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@URIDATEFROM", SqlDbType.DateTime, 16, dtUriFrom), _
        SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtUriTo), _
        SQLHelper.MakeParam("@SEIKYUUFROM", SqlDbType.DateTime, 16, dtSeikyuuFrom), _
        SQLHelper.MakeParam("@SEIKYUUTO", SqlDbType.DateTime, 16, dtSeikyuuTo), _
        SQLHelper.MakeParam("@SEIKYUUSYOHAKDATE", SqlDbType.DateTime, 16, dtUpdDate), _
        SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
        SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, dtNowDateTime)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' �@�ʐ����e�[�u�����X�V3
    ''' </summary>
    ''' <param name="dtUriFrom">����N����FROM</param>
    ''' <param name="dtUriTo">����N����TO</param>
    ''' <param name="dtSeikyuuFrom">���������s��FROM</param>
    ''' <param name="dtSeikyuuTo">���������s��TO</param>
    ''' <param name="updLoginUserId">�X�V���O�C�����[�U�[</param>
    ''' <param name="dtUpdDate">�ݒ���t</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdTeibetuSeikyuuDataForKessan3(ByVal dtUriFrom As Date _
                                                    , ByVal dtUriTo As Date _
                                                    , ByVal dtSeikyuuFrom As Date _
                                                    , ByVal dtSeikyuuTo As Date _
                                                    , ByVal dtUpdDate As Date _
                                                    , ByVal updLoginUserId As String _
                                                    , ByVal dtNowDateTime As DateTime) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdTeibetuSeikyuuDataForKessan3" _
                                                    , dtUriFrom _
                                                    , dtUriTo _
                                                    , dtSeikyuuFrom _
                                                    , dtSeikyuuTo _
                                                    , dtUpdDate _
                                                    , updLoginUserId _
                                                    , dtNowDateTime)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �X�V�pSQL�ݒ�
        cmdTextSb.Append(" UPDATE ")
        cmdTextSb.Append("      t_teibetu_seikyuu ")
        cmdTextSb.Append(" SET ")
        cmdTextSb.Append("      seikyuusyo_hak_date = @SEIKYUUSYOHAKDATE ")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_teibetu_seikyuu AS TTS ")
        cmdTextSb.Append("           INNER JOIN t_jiban AS TJ ")
        cmdTextSb.Append("             ON TTS.kbn = TJ.kbn ")
        cmdTextSb.Append("            AND TTS.hosyousyo_no = TJ.hosyousyo_no ")
        cmdTextSb.Append("           LEFT OUTER JOIN v_syouhin_siire_seikyuu_saki_jiban_tyskaisya VSS ")
        cmdTextSb.Append("             ON TTS.kbn = VSS.kbn ")
        cmdTextSb.Append("            AND TTS.hosyousyo_no = VSS.hosyousyo_no ")
        cmdTextSb.Append("            AND TTS.syouhin_cd = VSS.syouhin_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_seikyuu_saki MSS ")
        cmdTextSb.Append("             ON VSS.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
        cmdTextSb.Append("            AND VSS.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
        cmdTextSb.Append("            AND VSS.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      TTS.uri_keijyou_flg = '0' ")
        cmdTextSb.Append("  AND (TTS.uri_date BETWEEN @URIDATEFROM AND @URIDATETO) ")
        cmdTextSb.Append("  AND (TTS.seikyuusyo_hak_date BETWEEN @SEIKYUUFROM AND @SEIKYUUTO) ")
        cmdTextSb.Append("  AND (TTS.seikyuu_saki_cd IS NULL ")
        cmdTextSb.Append("   OR TTS.seikyuu_saki_brc IS NULL ")
        cmdTextSb.Append("   OR TTS.seikyuu_saki_kbn IS NULL) ")
        cmdTextSb.Append("  AND TTS.bunrui_cd = '140' ")
        cmdTextSb.Append("  AND TJ.t_koj_kaisya_seikyuu_umu = '1' ")
        cmdTextSb.Append("  AND ISNULL(MSS.kessanji_nidosime_flg, 0) = '0' ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@URIDATEFROM", SqlDbType.DateTime, 16, dtUriFrom), _
        SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtUriTo), _
        SQLHelper.MakeParam("@SEIKYUUFROM", SqlDbType.DateTime, 16, dtSeikyuuFrom), _
        SQLHelper.MakeParam("@SEIKYUUTO", SqlDbType.DateTime, 16, dtSeikyuuTo), _
        SQLHelper.MakeParam("@SEIKYUUSYOHAKDATE", SqlDbType.DateTime, 16, dtUpdDate), _
        SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
        SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, dtNowDateTime)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' �@�ʐ����e�[�u�����X�V4
    ''' </summary>
    ''' <param name="dtUriFrom">����N����FROM</param>
    ''' <param name="dtUriTo">����N����TO</param>
    ''' <param name="dtSeikyuuFrom">���������s��FROM</param>
    ''' <param name="dtSeikyuuTo">���������s��TO</param>
    ''' <param name="updLoginUserId">�X�V���O�C�����[�U�[</param>
    ''' <param name="dtUpdDate">�ݒ���t</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdTeibetuSeikyuuDataForKessan4(ByVal dtUriFrom As Date _
                                                    , ByVal dtUriTo As Date _
                                                    , ByVal dtSeikyuuFrom As Date _
                                                    , ByVal dtSeikyuuTo As Date _
                                                    , ByVal dtUpdDate As Date _
                                                    , ByVal updLoginUserId As String _
                                                    , ByVal dtNowDateTime As DateTime) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdTeibetuSeikyuuDataForKessan4" _
                                                    , dtUriFrom _
                                                    , dtUriTo _
                                                    , dtSeikyuuFrom _
                                                    , dtSeikyuuTo _
                                                    , dtUpdDate _
                                                    , updLoginUserId _
                                                    , dtNowDateTime)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �X�V�pSQL�ݒ�
        cmdTextSb.Append(" UPDATE ")
        cmdTextSb.Append("      t_teibetu_seikyuu ")
        cmdTextSb.Append(" SET ")
        cmdTextSb.Append("      seikyuusyo_hak_date = @SEIKYUUSYOHAKDATE ")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_teibetu_seikyuu AS TTS ")
        cmdTextSb.Append("           LEFT OUTER JOIN t_jiban AS TJ ")
        cmdTextSb.Append("             ON TTS.kbn = TJ.kbn ")
        cmdTextSb.Append("            AND TTS.hosyousyo_no = TJ.hosyousyo_no ")
        cmdTextSb.Append("           INNER JOIN ")
        cmdTextSb.Append("               (SELECT ")
        cmdTextSb.Append("                     TS.kbn ")
        cmdTextSb.Append("                   , TS.hosyousyo_no ")
        cmdTextSb.Append("                   , TS.bunrui_cd ")
        cmdTextSb.Append("                   , TS.gamen_hyouji_no ")
        cmdTextSb.Append("                FROM ")
        cmdTextSb.Append("                     t_jiban TJI ")
        cmdTextSb.Append("                          LEFT OUTER JOIN t_teibetu_seikyuu TS ")
        cmdTextSb.Append("                            ON TJI.kbn = TS.kbn ")
        cmdTextSb.Append("                           AND TJI.hosyousyo_no = TS.hosyousyo_no ")
        cmdTextSb.Append("                WHERE ")
        cmdTextSb.Append("                     TS.bunrui_cd = '130' ")
        cmdTextSb.Append("                 AND Nullif(TJI.koj_gaisya_seikyuu_umu, '') is null ")
        cmdTextSb.Append("               ) ")
        cmdTextSb.Append("                AS TTS2 ")
        cmdTextSb.Append("             ON TTS.kbn = TTS2.kbn ")
        cmdTextSb.Append("            AND TTS.hosyousyo_no = TTS2.hosyousyo_no ")
        cmdTextSb.Append("            AND TTS.bunrui_cd = TTS2.bunrui_cd ")
        cmdTextSb.Append("            AND TTS.gamen_hyouji_no = TTS2.gamen_hyouji_no ")
        cmdTextSb.Append("           LEFT OUTER JOIN v_syouhin_seikyuusaki_kameiten AS VSS ")
        cmdTextSb.Append("             ON TTS.syouhin_cd = VSS.syouhin_cd ")
        cmdTextSb.Append("            AND TJ.kameiten_cd = VSS.kameiten_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_seikyuu_saki AS MSS ")
        cmdTextSb.Append("             ON VSS.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
        cmdTextSb.Append("            AND VSS.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
        cmdTextSb.Append("            AND VSS.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      TTS.uri_keijyou_flg = '0' ")
        cmdTextSb.Append("  AND (TTS.uri_date BETWEEN @URIDATEFROM AND @URIDATETO) ")
        cmdTextSb.Append("  AND (TTS.seikyuusyo_hak_date BETWEEN @SEIKYUUFROM AND @SEIKYUUTO) ")
        cmdTextSb.Append("  AND (TTS.seikyuu_saki_cd IS NULL ")
        cmdTextSb.Append("   OR TTS.seikyuu_saki_brc IS NULL ")
        cmdTextSb.Append("   OR TTS.seikyuu_saki_kbn IS NULL) ")
        cmdTextSb.Append("  AND ISNULL(MSS.kessanji_nidosime_flg, 0) = '0' ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@URIDATEFROM", SqlDbType.DateTime, 16, dtUriFrom), _
        SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtUriTo), _
        SQLHelper.MakeParam("@SEIKYUUFROM", SqlDbType.DateTime, 16, dtSeikyuuFrom), _
        SQLHelper.MakeParam("@SEIKYUUTO", SqlDbType.DateTime, 16, dtSeikyuuTo), _
        SQLHelper.MakeParam("@SEIKYUUSYOHAKDATE", SqlDbType.DateTime, 16, dtUpdDate), _
        SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
        SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, dtNowDateTime)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' �@�ʐ����e�[�u�����X�V5
    ''' </summary>
    ''' <param name="dtUriFrom">����N����FROM</param>
    ''' <param name="dtUriTo">����N����TO</param>
    ''' <param name="dtSeikyuuFrom">���������s��FROM</param>
    ''' <param name="dtSeikyuuTo">���������s��TO</param>
    ''' <param name="updLoginUserId">�X�V���O�C�����[�U�[</param>
    ''' <param name="dtUpdDate">�ݒ���t</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdTeibetuSeikyuuDataForKessan5(ByVal dtUriFrom As Date _
                                                    , ByVal dtUriTo As Date _
                                                    , ByVal dtSeikyuuFrom As Date _
                                                    , ByVal dtSeikyuuTo As Date _
                                                    , ByVal dtUpdDate As Date _
                                                    , ByVal updLoginUserId As String _
                                                    , ByVal dtNowDateTime As DateTime) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdTeibetuSeikyuuDataForKessan5" _
                                                    , dtUriFrom _
                                                    , dtUriTo _
                                                    , dtSeikyuuFrom _
                                                    , dtSeikyuuTo _
                                                    , dtUpdDate _
                                                    , updLoginUserId _
                                                    , dtNowDateTime)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �X�V�pSQL�ݒ�
        cmdTextSb.Append(" UPDATE ")
        cmdTextSb.Append("      t_teibetu_seikyuu ")
        cmdTextSb.Append(" SET ")
        cmdTextSb.Append("      seikyuusyo_hak_date = @SEIKYUUSYOHAKDATE ")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_teibetu_seikyuu AS TTS ")
        cmdTextSb.Append("           LEFT OUTER JOIN t_jiban AS TJ ")
        cmdTextSb.Append("             ON TTS.kbn = TJ.kbn ")
        cmdTextSb.Append("            AND TTS.hosyousyo_no = TJ.hosyousyo_no ")
        cmdTextSb.Append("           INNER JOIN ")
        cmdTextSb.Append("               (SELECT ")
        cmdTextSb.Append("                     TS.kbn ")
        cmdTextSb.Append("                   , TS.hosyousyo_no ")
        cmdTextSb.Append("                   , TS.bunrui_cd ")
        cmdTextSb.Append("                   , TS.gamen_hyouji_no ")
        cmdTextSb.Append("                FROM ")
        cmdTextSb.Append("                     t_jiban TJI ")
        cmdTextSb.Append("                          LEFT OUTER JOIN t_teibetu_seikyuu TS ")
        cmdTextSb.Append("                            ON TJI.kbn = TS.kbn ")
        cmdTextSb.Append("                           AND TJI.hosyousyo_no = TS.hosyousyo_no ")
        cmdTextSb.Append("                WHERE ")
        cmdTextSb.Append("                     TS.bunrui_cd = '140' ")
        cmdTextSb.Append("                 AND Nullif(TJI.t_koj_kaisya_seikyuu_umu, '') is null ")
        cmdTextSb.Append("               ) ")
        cmdTextSb.Append("                AS TTS3 ")
        cmdTextSb.Append("             ON TTS.kbn = TTS3.kbn ")
        cmdTextSb.Append("            AND TTS.hosyousyo_no = TTS3.hosyousyo_no ")
        cmdTextSb.Append("            AND TTS.bunrui_cd = TTS3.bunrui_cd ")
        cmdTextSb.Append("            AND TTS.gamen_hyouji_no = TTS3.gamen_hyouji_no ")
        cmdTextSb.Append("           LEFT OUTER JOIN v_syouhin_seikyuusaki_kameiten AS VSS ")
        cmdTextSb.Append("             ON TTS.syouhin_cd = VSS.syouhin_cd ")
        cmdTextSb.Append("            AND TJ.kameiten_cd = VSS.kameiten_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_seikyuu_saki AS MSS ")
        cmdTextSb.Append("             ON VSS.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
        cmdTextSb.Append("            AND VSS.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
        cmdTextSb.Append("            AND VSS.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      TTS.uri_keijyou_flg = '0' ")
        cmdTextSb.Append("  AND (TTS.uri_date BETWEEN @URIDATEFROM AND @URIDATETO) ")
        cmdTextSb.Append("  AND (TTS.seikyuusyo_hak_date BETWEEN @SEIKYUUFROM AND @SEIKYUUTO) ")
        cmdTextSb.Append("  AND (TTS.seikyuu_saki_cd IS NULL ")
        cmdTextSb.Append("   OR TTS.seikyuu_saki_brc IS NULL ")
        cmdTextSb.Append("   OR TTS.seikyuu_saki_kbn IS NULL) ")
        cmdTextSb.Append("  AND ISNULL(MSS.kessanji_nidosime_flg, 0) = '0' ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@URIDATEFROM", SqlDbType.DateTime, 16, dtUriFrom), _
        SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtUriTo), _
        SQLHelper.MakeParam("@SEIKYUUFROM", SqlDbType.DateTime, 16, dtSeikyuuFrom), _
        SQLHelper.MakeParam("@SEIKYUUTO", SqlDbType.DateTime, 16, dtSeikyuuTo), _
        SQLHelper.MakeParam("@SEIKYUUSYOHAKDATE", SqlDbType.DateTime, 16, dtUpdDate), _
        SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
        SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, dtNowDateTime)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' �X�ʏ��������e�[�u�����X�V���܂��B
    ''' </summary>
    ''' <param name="dtUriFrom">����N����FROM</param>
    ''' <param name="dtUriTo">����N����TO</param>
    ''' <param name="dtSeikyuuFrom">���������s��FROM</param>
    ''' <param name="dtSeikyuuTo">���������s��TO</param>
    ''' <param name="updLoginUserId">�X�V���O�C�����[�U�[</param>
    ''' <param name="dtUpdDate">�ݒ���t</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdTenbetuSyokiSeikyuuDataForKessan(ByVal dtUriFrom As Date _
                                                        , ByVal dtUriTo As Date _
                                                        , ByVal dtSeikyuuFrom As Date _
                                                        , ByVal dtSeikyuuTo As Date _
                                                        , ByVal dtUpdDate As Date _
                                                        , ByVal UpdLoginUserId As String _
                                                        , ByVal dtNowDateTime As DateTime) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdTenbetuSyokiSeikyuuDataForKessan" _
                                                    , dtUriFrom _
                                                    , dtUriTo _
                                                    , dtSeikyuuFrom _
                                                    , dtSeikyuuTo _
                                                    , dtUpdDate _
                                                    , UpdLoginUserId _
                                                    , dtNowDateTime)

        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �X�V�pSQL�ݒ�
        cmdTextSb.Append(" UPDATE ")
        cmdTextSb.Append("      t_tenbetu_syoki_seikyuu ")
        cmdTextSb.Append(" SET ")
        cmdTextSb.Append("      seikyuusyo_hak_date = @SEIKYUUSYOHAKDATE ")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_tenbetu_syoki_seikyuu TTS ")
        cmdTextSb.Append("           LEFT OUTER JOIN v_syouhin_seikyuusaki_kameiten VSS ")
        cmdTextSb.Append("             ON TTS.mise_cd = VSS.kameiten_cd ")
        cmdTextSb.Append("            AND TTS.syouhin_cd = VSS.syouhin_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_seikyuu_saki MSS ")
        cmdTextSb.Append("             ON VSS.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
        cmdTextSb.Append("            AND VSS.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
        cmdTextSb.Append("            AND VSS.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      uri_keijyou_flg = 0 ")
        cmdTextSb.Append("  AND (uri_date BETWEEN @URIDATEFROM AND @URIDATETO) ")
        cmdTextSb.Append("  AND (seikyuusyo_hak_date BETWEEN @SEIKYUUFROM AND @SEIKYUUTO) ")
        cmdTextSb.Append("  AND ISNULL(MSS.kessanji_nidosime_flg, 0) = '0' ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@URIDATEFROM", SqlDbType.DateTime, 16, dtUriFrom), _
        SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtUriTo), _
        SQLHelper.MakeParam("@SEIKYUUFROM", SqlDbType.DateTime, 16, dtSeikyuuFrom), _
        SQLHelper.MakeParam("@SEIKYUUTO", SqlDbType.DateTime, 16, dtSeikyuuTo), _
        SQLHelper.MakeParam("@SEIKYUUSYOHAKDATE", SqlDbType.DateTime, 16, dtUpdDate), _
        SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, UpdLoginUserId), _
        SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, dtNowDateTime)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' �X�ʐ����e�[�u�����X�V1
    ''' </summary>
    ''' <param name="dtUriFrom">����N����FROM</param>
    ''' <param name="dtUriTo">����N����TO</param>
    ''' <param name="dtSeikyuuFrom">���������s��FROM</param>
    ''' <param name="dtSeikyuuTo">���������s��TO</param>
    ''' <param name="updLoginUserId">�X�V���O�C�����[�U�[</param>
    ''' <param name="dtUpdDate">�ݒ���t</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdTenbetuSeikyuuDataForKessan1(ByVal dtUriFrom As Date _
                                                    , ByVal dtUriTo As Date _
                                                    , ByVal dtSeikyuuFrom As Date _
                                                    , ByVal dtSeikyuuTo As Date _
                                                    , ByVal dtUpdDate As Date _
                                                    , ByVal UpdLoginUserId As String _
                                                    , ByVal dtNowDateTime As DateTime) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdTenbetuSeikyuuDataForKessan1" _
                                                    , dtUriFrom _
                                                    , dtUriTo _
                                                    , dtSeikyuuFrom _
                                                    , dtSeikyuuTo _
                                                    , dtUpdDate _
                                                    , UpdLoginUserId _
                                                    , dtNowDateTime)


        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �X�V�pSQL�ݒ�
        cmdTextSb.Append(" UPDATE ")
        cmdTextSb.Append("      t_tenbetu_seikyuu ")
        cmdTextSb.Append(" SET ")
        cmdTextSb.Append("      seikyuusyo_hak_date = @SEIKYUUSYOHAKDATE ")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_tenbetu_seikyuu AS TTS ")
        cmdTextSb.Append("           LEFT OUTER JOIN v_syouhin_seikyuusaki_kameiten VSS ")
        cmdTextSb.Append("             ON TTS.syouhin_cd = VSS.syouhin_cd ")
        cmdTextSb.Append("            AND TTS.mise_cd = VSS.kameiten_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_seikyuu_saki MSS ")
        cmdTextSb.Append("             ON VSS.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
        cmdTextSb.Append("            AND VSS.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
        cmdTextSb.Append("            AND VSS.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      TTS.uri_keijyou_flg = 0 ")
        cmdTextSb.Append("  AND (TTS.uri_date BETWEEN @URIDATEFROM AND @URIDATETO) ")
        cmdTextSb.Append("  AND (TTS.seikyuusyo_hak_date BETWEEN @SEIKYUUFROM AND @SEIKYUUTO) ")
        cmdTextSb.Append("  AND TTS.bunrui_cd = '220' ")
        cmdTextSb.Append("  AND ISNULL(MSS.kessanji_nidosime_flg, 0) = '0' ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@URIDATEFROM", SqlDbType.DateTime, 16, dtUriFrom), _
        SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtUriTo), _
        SQLHelper.MakeParam("@SEIKYUUFROM", SqlDbType.DateTime, 16, dtSeikyuuFrom), _
        SQLHelper.MakeParam("@SEIKYUUTO", SqlDbType.DateTime, 16, dtSeikyuuTo), _
        SQLHelper.MakeParam("@SEIKYUUSYOHAKDATE", SqlDbType.DateTime, 16, dtUpdDate), _
        SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, UpdLoginUserId), _
        SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, dtNowDateTime)}


        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' �X�ʐ����e�[�u�����X�V2
    ''' </summary>
    ''' <param name="dtUriFrom">����N����FROM</param>
    ''' <param name="dtUriTo">����N����TO</param>
    ''' <param name="dtSeikyuuFrom">���������s��FROM</param>
    ''' <param name="dtSeikyuuTo">���������s��TO</param>
    ''' <param name="updLoginUserId">�X�V���O�C�����[�U�[</param>
    ''' <param name="dtUpdDate">�ݒ���t</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdTenbetuSeikyuuDataForKessan2(ByVal dtUriFrom As Date _
                                                    , ByVal dtUriTo As Date _
                                                    , ByVal dtSeikyuuFrom As Date _
                                                    , ByVal dtSeikyuuTo As Date _
                                                    , ByVal dtUpdDate As Date _
                                                    , ByVal UpdLoginUserId As String _
                                                    , ByVal dtNowDateTime As DateTime) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdTenbetuSeikyuuDataForKessan2" _
                                                    , dtUriFrom _
                                                    , dtUriTo _
                                                    , dtSeikyuuFrom _
                                                    , dtSeikyuuTo _
                                                    , dtUpdDate _
                                                    , UpdLoginUserId _
                                                    , dtNowDateTime)


        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �X�V�pSQL�ݒ�
        cmdTextSb.Append(" UPDATE ")
        cmdTextSb.Append("      t_tenbetu_seikyuu ")
        cmdTextSb.Append(" SET ")
        cmdTextSb.Append("      seikyuusyo_hak_date = @SEIKYUUSYOHAKDATE ")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_tenbetu_seikyuu AS TTS ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_eigyousyo AS ME ")
        cmdTextSb.Append("             ON TTS.mise_cd = ME.eigyousyo_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_seikyuu_saki AS MSS ")
        cmdTextSb.Append("             ON ME.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
        cmdTextSb.Append("            AND ME.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
        cmdTextSb.Append("            AND ME.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      TTS.uri_keijyou_flg = 0 ")
        cmdTextSb.Append("  AND (TTS.uri_date BETWEEN @URIDATEFROM AND @URIDATETO) ")
        cmdTextSb.Append("  AND (TTS.seikyuusyo_hak_date BETWEEN @SEIKYUUFROM AND @SEIKYUUTO) ")
        cmdTextSb.Append("  AND TTS.bunrui_cd <> '220' ")
        cmdTextSb.Append("  AND ISNULL(MSS.kessanji_nidosime_flg, 0) = '0' ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@URIDATEFROM", SqlDbType.DateTime, 16, dtUriFrom), _
        SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtUriTo), _
        SQLHelper.MakeParam("@SEIKYUUFROM", SqlDbType.DateTime, 16, dtSeikyuuFrom), _
        SQLHelper.MakeParam("@SEIKYUUTO", SqlDbType.DateTime, 16, dtSeikyuuTo), _
        SQLHelper.MakeParam("@SEIKYUUSYOHAKDATE", SqlDbType.DateTime, 16, dtUpdDate), _
        SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, UpdLoginUserId), _
        SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, dtNowDateTime)}


        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' �ėp����f�[�^�e�[�u�����X�V���܂��B
    ''' </summary>
    ''' <param name="dtUriFrom">����N����FROM</param>
    ''' <param name="dtUriTo">����N����TO</param>
    ''' <param name="dtSeikyuuFrom">���������s��FROM</param>
    ''' <param name="dtSeikyuuTo">���������s��TO</param>
    ''' <param name="updLoginUserId">�X�V���O�C�����[�U�[</param>
    ''' <param name="dtUpdDate">�ݒ���t</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdHannyouUriageDataForKessan(ByVal dtUriFrom As Date _
                                                , ByVal dtUriTo As Date _
                                                , ByVal dtSeikyuuFrom As Date _
                                                , ByVal dtSeikyuuTo As Date _
                                                , ByVal dtUpdDate As Date _
                                                , ByVal updLoginUserId As String _
                                                , ByVal dtNowDateTime As DateTime) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdHannyouUriageDataForKessan" _
                                                    , dtUriFrom _
                                                    , dtUriTo _
                                                    , dtSeikyuuFrom _
                                                    , dtSeikyuuTo _
                                                    , dtUpdDate _
                                                    , updLoginUserId _
                                                    , dtNowDateTime)
        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" UPDATE ")
        cmdTextSb.Append("      t_hannyou_uriage ")
        cmdTextSb.Append(" SET ")
        cmdTextSb.Append("      seikyuu_date = @SEIKYUUSYOHAKDATE ")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("    , upd_login_user_name = jhs_sys.fnGetAddUpdUserName(@UPDLOGINUSERID) ")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_hannyou_uriage AS THU ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_seikyuu_saki AS MSS ")
        cmdTextSb.Append("             ON THU.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
        cmdTextSb.Append("            AND THU.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
        cmdTextSb.Append("            AND THU.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      THU.uri_keijyou_flg = 0 ")
        cmdTextSb.Append("  AND THU.torikesi = 0 ")
        cmdTextSb.Append("  AND (THU.uri_date BETWEEN @URIDATEFROM AND @URIDATETO) ")
        cmdTextSb.Append("  AND (THU.seikyuu_date BETWEEN @SEIKYUUFROM AND @SEIKYUUTO) ")
        cmdTextSb.Append("  AND ISNULL(MSS.kessanji_nidosime_flg, 0) = '0' ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@URIDATEFROM", SqlDbType.DateTime, 16, dtUriFrom), _
        SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtUriTo), _
        SQLHelper.MakeParam("@SEIKYUUFROM", SqlDbType.DateTime, 16, dtSeikyuuFrom), _
        SQLHelper.MakeParam("@SEIKYUUTO", SqlDbType.DateTime, 16, dtSeikyuuTo), _
        SQLHelper.MakeParam("@SEIKYUUSYOHAKDATE", SqlDbType.DateTime, 16, dtUpdDate), _
        SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
        SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, dtNowDateTime)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)

        Return intResult
    End Function

    ''' <summary>
    ''' ����f�[�^�e�[�u�����X�V���܂��B
    ''' </summary>
    ''' <param name="dtUriFrom">����N����FROM</param>
    ''' <param name="dtUriTo">����N����TO</param>
    ''' <param name="dtSeikyuuFrom">���������s��FROM</param>
    ''' <param name="dtSeikyuuTo">���������s��TO</param>
    ''' <param name="updLoginUserId">�X�V���O�C�����[�U�[</param>
    ''' <param name="dtUpdDate">�ݒ���t</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdUriageDataForKessan(ByVal dtUriFrom As Date _
                                                , ByVal dtUriTo As Date _
                                                , ByVal dtSeikyuuFrom As Date _
                                                , ByVal dtSeikyuuTo As Date _
                                                , ByVal dtUpdDate As Date _
                                                , ByVal updLoginUserId As String _
                                                , ByVal dtNowDateTime As DateTime) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdUriageDataForKessan" _
                                                    , dtUriFrom _
                                                    , dtUriTo _
                                                    , dtSeikyuuFrom _
                                                    , dtSeikyuuTo _
                                                    , dtUpdDate _
                                                    , updLoginUserId _
                                                    , dtNowDateTime)
        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" UPDATE ")
        cmdTextSb.Append("      t_uriage_data ")
        cmdTextSb.Append(" SET ")
        cmdTextSb.Append("      seikyuu_date = @SEIKYUUSYOHAKDATE ")
        cmdTextSb.Append("    , upd_login_user_id = @UPDLOGINUSERID ")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_uriage_data AS TUD ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_seikyuu_saki AS MSS ")
        cmdTextSb.Append("             ON TUD.seikyuu_saki_cd = MSS.seikyuu_saki_cd ")
        cmdTextSb.Append("            AND TUD.seikyuu_saki_brc = MSS.seikyuu_saki_brc ")
        cmdTextSb.Append("            AND TUD.seikyuu_saki_kbn = MSS.seikyuu_saki_kbn ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      TUD.uri_keijyou_flg = 0 ")
        cmdTextSb.Append("  AND (TUD.denpyou_uri_date BETWEEN @URIDATEFROM AND @URIDATETO) ")
        cmdTextSb.Append("  AND (TUD.seikyuu_date BETWEEN @SEIKYUUFROM AND @SEIKYUUTO) ")
        cmdTextSb.Append("  AND ISNULL(MSS.kessanji_nidosime_flg, 0) = '0' ")

        ' �p�����[�^�֐ݒ�
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@URIDATEFROM", SqlDbType.DateTime, 16, dtUriFrom), _
        SQLHelper.MakeParam("@URIDATETO", SqlDbType.DateTime, 16, dtUriTo), _
        SQLHelper.MakeParam("@SEIKYUUFROM", SqlDbType.DateTime, 16, dtSeikyuuFrom), _
        SQLHelper.MakeParam("@SEIKYUUTO", SqlDbType.DateTime, 16, dtSeikyuuTo), _
        SQLHelper.MakeParam("@SEIKYUUSYOHAKDATE", SqlDbType.DateTime, 16, dtUpdDate), _
        SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, updLoginUserId), _
        SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, dtNowDateTime)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)

        Return intResult
    End Function

#End Region

#Region "�����m�菈��"

#Region "�����m�菈���Ǘ��e�[�u���f�[�^�擾"
    ''' <summary>
    ''' �����m�菈���Ǘ��e�[�u���f�[�^�擾
    ''' </summary>
    ''' <param name="targetYM">�Ώ۔N��</param>
    ''' <returns>�����m�菈���Ǘ��e�[�u�� �f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function searchGetujiKakuteiYoyakuData( _
                                                  ByVal targetYM As Date _
                                                  ) As Object

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".searchGetujiKakuteiYoyakuData", _
                                                    targetYM _
                                                    )

        Dim paramTargetYM As String = "@targetYM@"

        ' SQL����
        Dim sqlSb As New StringBuilder()
        sqlSb.Append(" SELECT ")
        sqlSb.Append("     syori_joukyou ")
        sqlSb.Append(" FROM ")
        sqlSb.Append("     [jhs_sys].[t_getuji_kakutei_yoyaku_kanri]  ")

        If targetYM <> Nothing AndAlso targetYM <> Date.MinValue Then
            sqlSb.Append(" WHERE ")
            sqlSb.Append("     syori_nengetu = " & paramTargetYM)
        End If

        ' �p�����[�^�֐ݒ�
        Dim sqlParams() As SqlParameter = Nothing

        ' �p�����[�^��ݒ�
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(paramTargetYM, SqlDbType.DateTime, 1, targetYM) _
                                        }
        '�f�[�^�擾���ԋp
        Return ExecuteScalar(connStr, _
                             CommandType.Text, _
                             sqlSb.ToString(), _
                             sqlParams)
    End Function

#End Region

#Region "�����m�菈���Ǘ��e�[�u���ɗ\���ǉ�����"
    ''' <summary>
    ''' �����m�菈���Ǘ��e�[�u���ɗ\���ǉ�����
    ''' </summary>
    ''' <param name="targetYM">�Ώ۔N��</param>
    ''' <param name="joukyou">������</param>
    ''' <param name="LoginUserId">���[�U�[ID</param>
    ''' <returns>�����m�菈���Ǘ��e�[�u���ɗ\���ǉ�����</returns>
    ''' <remarks></remarks>
    Public Function insertGetujiKakuteiYoyaku(ByVal targetYM As Date _
                                                    , ByVal joukyou As Integer _
                                                    , ByVal loginUserId As String _
                                                    ) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".updateGetujiKakuteiYoyakuJoukyou" _
                                                    , targetYM _
                                                    , joukyou _
                                                    , loginUserId _
                                                    )

        Dim paramTargetYM As String = "@targetYM@"
        Dim paramJoukyou As String = "@joukyou@"
        Dim paramLoginUserId As String = "@LoginUserId@"

        Dim intResult As Integer = 0
        Dim sqlSb As New StringBuilder()
        Dim sqlParams() As SqlClient.SqlParameter

        ' �X�V�pSQL�ݒ�
        sqlSb.Append(" INSERT  ")
        sqlSb.Append(" INTO [jhs_sys].[t_getuji_kakutei_yoyaku_kanri] (  ")
        sqlSb.Append("     syori_nengetu ")
        sqlSb.Append("     , syori_joukyou ")
        sqlSb.Append("     , add_login_user_id ")
        sqlSb.Append("     , add_datetime ")
        sqlSb.Append(" )  ")
        sqlSb.Append(" VALUES (  ")
        sqlSb.Append("     " & paramTargetYM)
        sqlSb.Append("     , " & paramJoukyou)
        sqlSb.Append("     , " & paramLoginUserId)
        sqlSb.Append("     , GETDATE() ")
        sqlSb.Append(" )  ")

        ' �p�����[�^�֐ݒ�
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(paramTargetYM, SqlDbType.DateTime, 1, targetYM), _
                                        SQLHelper.MakeParam(paramJoukyou, SqlDbType.Int, 1, joukyou), _
                                        SQLHelper.MakeParam(paramLoginUserId, SqlDbType.VarChar, 30, loginUserId) _
                                        }

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sqlSb.ToString(), _
                                    sqlParams)
        Return intResult
    End Function
#End Region

#Region "�����m�菈���Ǘ��e�[�u���f�[�^�̏����󋵂��X�V����"
    ''' <summary>
    ''' �����m�菈���Ǘ��e�[�u���f�[�^�̏����󋵂��X�V����
    ''' </summary>
    ''' <param name="targetYM">�Ώ۔N��</param>
    ''' <param name="joukyouFrom">�ύX�O������</param>
    ''' <param name="joukyouTo">�ύX�㏈����</param>
    ''' <param name="LoginUserId">���[�U�[ID</param>
    ''' <returns>�����m�菈���Ǘ��e�[�u���f�[�^�̏����󋵂��X�V����</returns>
    ''' <remarks></remarks>
    Public Function updateGetujiKakuteiYoyakuJoukyou(ByVal targetYM As Date _
                                                    , ByVal joukyouFrom As Integer _
                                                    , ByVal joukyouTo As Integer _
                                                    , ByVal loginUserId As String _
                                                    ) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".updateGetujiKakuteiYoyakuJoukyou" _
                                                    , targetYM _
                                                    , joukyouFrom _
                                                    , joukyouTo _
                                                    , loginUserId _
                                                    )

        Dim paramTargetYM As String = "@targetYM@"
        Dim paramJoukyouFrom As String = "@joukyouFrom@"
        Dim paramJoukyouTo As String = "@joukyouTo@"
        Dim paramLoginUserId As String = "@LoginUserId@"

        Dim intResult As Integer = 0
        Dim sqlSb As New StringBuilder()
        Dim sqlParams() As SqlClient.SqlParameter

        ' �X�V�pSQL�ݒ�
        sqlSb.Append("UPDATE [jhs_sys].[t_getuji_kakutei_yoyaku_kanri]  ")
        sqlSb.Append("   SET ")
        sqlSb.Append("      syori_joukyou       = " & paramJoukyouTo)
        sqlSb.Append("      ,upd_login_user_id  = " & paramLoginUserId)
        sqlSb.Append("      ,upd_datetime       = GETDATE() ")
        sqlSb.Append(" WHERE syori_nengetu      = " & paramTargetYM)
        sqlSb.Append("   AND syori_joukyou      = " & paramJoukyouFrom)

        ' �p�����[�^�֐ݒ�
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(paramTargetYM, SqlDbType.DateTime, 1, targetYM), _
                                        SQLHelper.MakeParam(paramJoukyouFrom, SqlDbType.Int, 1, joukyouFrom), _
                                        SQLHelper.MakeParam(paramJoukyouTo, SqlDbType.Int, 1, joukyouTo), _
                                        SQLHelper.MakeParam(paramLoginUserId, SqlDbType.VarChar, 30, loginUserId) _
                                        }

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sqlSb.ToString(), _
                                    sqlParams)
        Return intResult
    End Function
#End Region

#Region "���߂̌����m�菈���N�������擾����"
    ''' <summary>
    ''' ���߂̌����m�菈���N�������擾����
    ''' </summary>
    ''' <returns>���߂̌����m�菈���N����</returns>
    ''' <remarks></remarks>
    Public Function getGetujiKakuteiLastSyoriDate() As Object
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getGetujiKakuteiLastSyoriDate")

        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      max(syori_nengetu)")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      t_getuji_kakutei_yoyaku_kanri")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      syori_joukyou <> 0")

        '�f�[�^�擾���ԋp
        Return ExecuteScalar(connStr, _
                             CommandType.Text, _
                             cmdTextSb.ToString())
    End Function
#End Region

#End Region

End Class
