Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' �����f�[�^���ꊇ�X�V���郍�W�b�N�N���X�ł��B
''' </summary>
''' <remarks></remarks>
Public Class GetujiIkkatuUpdateLogic
    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    '���b�Z�[�W���W�b�N
    Dim mLogic As New MessageLogic

#Region "�萔"
    Private Const KAIRYOU_KOUJI As String = "130"
    Private Const TUIKA_KAIRYOU_KOUJI As String = "140"
    Private Const URI_KEIJYOU_FLG As Integer = 0
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
#Region "�v���p�e�B"
#Region "��ʏ��̎擾�pSetter�̂�"
    ''' <summary>
    ''' ����N����FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private strUriageFrom As String
    ''' <summary>
    ''' ����N����TO
    ''' </summary>
    ''' <remarks></remarks>
    Private strUriageTo As String
    ''' <summary>
    ''' ���������s��FROM
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuFrom As String
    ''' <summary>
    ''' ���������s��TO
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuTo As String

    ''' <summary>
    ''' ��ʂ���擾�p for ����N����FROM
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccUriageFrom() As String
        Set(ByVal value As String)
            strUriageFrom = value
        End Set
    End Property
    ''' <summary>
    ''' ��ʂ���擾�p for ����N����TO
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccUriageTo() As String
        Set(ByVal value As String)
            strUriageTo = value
        End Set
    End Property
    ''' <summary>
    ''' ��ʂ���擾�p for ���������s��FROM
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccSeikyuuFrom() As String
        Set(ByVal value As String)
            strSeikyuuFrom = value
        End Set
    End Property
    ''' <summary>
    ''' ��ʂ���擾�p for ���������s��TO
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AccSeikyuuTo() As String
        Set(ByVal value As String)
            strSeikyuuTo = value
        End Set
    End Property
#End Region
#Region "���O�C�����[�U�[ID"
    ''' <summary>
    ''' ���O�C�����[�U�[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strLoginUserId As String
    ''' <summary>
    ''' ���O�C�����[�U�[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���O�C�����[�U�[ID</returns>
    ''' <remarks></remarks>
    Public Property LoginUserId() As String
        Get
            Return strLoginUserId
        End Get
        Set(ByVal value As String)
            strLoginUserId = value
        End Set
    End Property
#End Region
#End Region

#Region "��������"
    ''' <summary>
    ''' �����������s���܂��B
    ''' </summary>
    ''' <returns>���v��������</returns>
    ''' <remarks></remarks>
    Public Function GetujiSyori(ByVal sender As System.Object) As Integer
        Dim intResult As Integer = 0
        Dim strBunruiCd As String
        Dim intUriKeijoFlg As Integer
        Dim dtFrom As Date
        Dim dtTo As Date
        Dim enTgtDt As enumTgtDt

        Try
            '�@�ʐ����e�[�u���Ɠ@�ʐ����A�g�Ǘ��e�[�u���̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))
                '�@�ʐ����e�[�u���ɑ΂��A���ǍH���E�ǉ����ǍH���̔���N�����E���������s�����ꊇ�C������B                   
                '�ꊇ�C�����s�����f�[�^�͘A�g�e�[�u���֔��f����                 
                '   1. ���ǍH���f�[�^�̈ꊇ�C��
                '*** 1-1.���ǍH���f�[�^�̔���N�����̕ύX
                '�����̐ݒ�
                strBunruiCd = KAIRYOU_KOUJI
                intUriKeijoFlg = URI_KEIJYOU_FLG
                dtFrom = DateTime.Parse(strUriageFrom)
                dtTo = DateTime.Parse(strUriageTo)
                enTgtDt = enumTgtDt.Uriage
                '�f�[�^�̍X�V
                intResult += GetujiUpdSyori(strBunruiCd, intUriKeijoFlg, dtFrom, dtTo, enTgtDt)

                '*** 1-2.���ǍH���f�[�^�̐��������s���̕ύX 
                '�����̐ݒ�
                strBunruiCd = KAIRYOU_KOUJI
                intUriKeijoFlg = URI_KEIJYOU_FLG
                dtFrom = DateTime.Parse(strSeikyuuFrom)
                dtTo = DateTime.Parse(strSeikyuuTo)
                enTgtDt = enumTgtDt.Seikyuu
                '�f�[�^�̍X�V
                intResult += GetujiUpdSyori(strBunruiCd, intUriKeijoFlg, dtFrom, dtTo, enTgtDt)

                '   2. �ǉ����ǍH���f�[�^�̈ꊇ�C��                
                '*** 2-1.�ǉ����ǍH���f�[�^�̔���N�����̕ύX               
                '�����̐ݒ�
                strBunruiCd = TUIKA_KAIRYOU_KOUJI
                intUriKeijoFlg = URI_KEIJYOU_FLG
                dtFrom = DateTime.Parse(strUriageFrom)
                dtTo = DateTime.Parse(strUriageTo)
                enTgtDt = enumTgtDt.Uriage
                '�f�[�^�̍X�V
                intResult += GetujiUpdSyori(strBunruiCd, intUriKeijoFlg, dtFrom, dtTo, enTgtDt)


                '*** 2-2.�ǉ����ǍH���f�[�^�̐��������s���̕ύX             
                '�����̐ݒ�
                strBunruiCd = TUIKA_KAIRYOU_KOUJI
                intUriKeijoFlg = URI_KEIJYOU_FLG
                dtFrom = DateTime.Parse(strSeikyuuFrom)
                dtTo = DateTime.Parse(strSeikyuuTo)
                enTgtDt = enumTgtDt.Seikyuu
                '�f�[�^�̍X�V
                intResult += GetujiUpdSyori(strBunruiCd, intUriKeijoFlg, dtFrom, dtTo, enTgtDt)

                ' �X�V�ɐ��������ꍇ�A�g�����U�N�V�������R�~�b�g����
                scope.Complete()
            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    '�G���[�L���b�`�F�f�b�h���b�N
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            intResult = Integer.MinValue
        Catch exTransactionException As System.Transactions.TransactionException
            '�G���[�L���b�`�F�g�����U�N�V�����G���[
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            intResult = Integer.MinValue
        End Try

        Return intResult
    End Function

    ''' <summary>
    ''' �@�ʐ����e�[�u���Ɠ@�ʐ����e�[�u���A�g�Ǘ��e�[�u�����ꊇ�X�V���܂��B
    ''' </summary>
    ''' <param name="strBunruiCD">���ރR�[�h</param>
    ''' <param name="intUriKeijoFlg">����v��FLG</param>
    ''' <param name="dtFrom">��ʓ��tFrom</param>
    ''' <param name="dtTo">��ʓ��tTo</param>
    ''' <param name="enTgtDt">�X�V�Ώ۔��f�񋓑́i0�F����N�����^1�F���������s���j</param>
    ''' <returns>���v���������iUpdate,Delete,Insert�j</returns>
    ''' <remarks></remarks>
    Private Function GetujiUpdSyori(ByVal strBunruiCD As String _
                                , ByVal intUriKeijoFlg As Integer _
                                , ByVal dtFrom As Date _
                                , ByVal dtTo As Date _
                                , ByVal enTgtDt As enumTgtDt) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetujiUpdSyori" _
                                                    , strBunruiCD _
                                                    , intUriKeijoFlg _
                                                    , dtFrom _
                                                    , dtTo _
                                                    , enTgtDt)
        Dim clsDataAcc As New GetujiIkkatuUpdateDataAccess
        Dim clsRenkeiLogic As New RenkeiKanriLogic
        'Dim clsRenkeiTeibetuRec As New TeibetuSeikyuuRenkeiRecord
        Dim intResult As Integer = 0

        Dim strInsetTmpSql As String
        Dim cmdParams() As SqlParameter

        '�A�g�p�e���|�����[�e�[�u���̖��O���w��
        Const P_TMP_TABLE_NAME As String = "#TEMP_TEIBETU_RENKEI_WORK"

        '�X�V�Ώۂ�A�g�p�e���|�����[�e�[�u���ɓo�^����SQL���쐬
        strInsetTmpSql = clsDataAcc.GetInsertSqlRenkeiTmpForGetujiUpd(P_TMP_TABLE_NAME, strBunruiCD, enTgtDt)

        '�X�V�Ώۍi���pSQL�p�����[�^�̍쐬
        cmdParams = clsDataAcc.GetRenkeiCmdParamsForGetujiUpd( _
                                                                EarthConst.enSqlTypeFlg.UPDATE, _
                                                                strBunruiCD, _
                                                                intUriKeijoFlg, _
                                                                dtFrom, _
                                                                dtTo)
        '�@�ʐ����A�g�Ǘ��e�[�u���̈ꊇ�X�V
        intResult += clsRenkeiLogic.EditTeibetuSeikyuuRenkeiLump(strInsetTmpSql, cmdParams, P_TMP_TABLE_NAME, strLoginUserId)

        '�@�ʐ����f�[�^�̈ꊇ�X�V
        intResult += clsDataAcc.UpdTeibetuSeikyuuDataAll(strBunruiCD, intUriKeijoFlg, dtFrom, dtTo, strLoginUserId, enTgtDt)

        Return intResult
    End Function
#End Region

#Region "���Z������"
    ''' <summary>
    ''' ���Z���������s���܂��B
    ''' </summary>
    ''' <returns>���v��������</returns>
    ''' <remarks></remarks>
    Public Function KessanSyori(ByVal sender As System.Object) As Integer
        Dim intResult As Integer = 0

        Try
            '�e��e�[�u���Ɗe��A�g�Ǘ��e�[�u���̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))
                '�X�V�������擾
                Dim dtNowDtTime As DateTime = DateTime.Now

                '1.�@�ʐ����A2.�X�ʏ��������A3.�X�ʐ����e�[�u���ɑ΂��A���������s���̐ݒ���s��                   
                '�ꊇ�C�����s�����f�[�^�͘A�g�e�[�u���֔��f����                 
                intResult += KessanUpdSyori(dtNowDtTime)

                '4. �ėp����e�[�u���̈ꊇ�C��
                intResult += KessanUpdSyoriHannyouUriage(dtNowDtTime)

                '5. ����f�[�^�e�[�u���̈ꊇ�C��
                intResult += KessanUpdSyoriUriageData(dtNowDtTime)

                scope.Complete()
            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    '�G���[�L���b�`�F�f�b�h���b�N
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            intResult = Integer.MinValue
        Catch exTransactionException As System.Transactions.TransactionException
            '�G���[�L���b�`�F�g�����U�N�V�����G���[
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            intResult = Integer.MinValue
        End Try

        Return intResult
    End Function

    ''' <summary>
    ''' �@�ʐ����e�[�u���A�X�ʏ��������e�[�u���A�X�ʐ����e�[�u���Ƃ��ꂼ��̘A�g�Ǘ��e�[�u�����ꊇ�X�V���܂��B
    ''' </summary>
    ''' <param name="dtNowDtTime">�X�V����</param>
    ''' <returns>���v���������iUpdate,Delete,Insert�j</returns>
    ''' <remarks></remarks>
    Private Function KessanUpdSyori(ByVal dtNowDtTime As DateTime) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".KessanUpdSyori", dtNowDtTime)

        Dim dataAccess As New GetujiIkkatuUpdateDataAccess
        Dim clsRenkeiLogic As New RenkeiKanriLogic
        Dim clsRenkeiTeibetuRec As New TeibetuSeikyuuRenkeiRecord
        Dim clsRenkeiTenbetuSyokiRec As New TenbetuSyokiSeikyuuRenkeiRecord
        Dim clsRenkeiTenbetuRec As New TenbetuSeikyuuRenkeiRecord
        Dim intResult As Integer
        Dim intRenkeiResult As Integer
        Dim dtUriFrom As Date
        Dim dtUriTo As Date
        Dim dtSeikyuuFrom As Date
        Dim dtSeikyuuTo As Date
        Dim strTgtTbl As String = ""
        Dim dtUpdDate As Date
        Dim renkeiDataAccess As New RenkeiKanriDataAccess

        '���t�̐ݒ�
        Select Case Now.Month
            Case 9, 10
                dtUriFrom = DateTime.Parse(Now.Year & "/09/01")
                dtUriTo = DateTime.Parse(Now.Year & "/09/30")
                dtSeikyuuFrom = DateTime.Parse(Now.Year & "/10/01")
                dtSeikyuuTo = DateTime.Parse(Now.Year & "/10/31")
                dtUpdDate = DateTime.Parse(Now.Year & "/09/30")
            Case 3, 4
                dtUriFrom = DateTime.Parse(Now.Year & "/03/01")
                dtUriTo = DateTime.Parse(Now.Year & "/03/31")
                dtSeikyuuFrom = DateTime.Parse(Now.Year & "/04/01")
                dtSeikyuuTo = DateTime.Parse(Now.Year & "/04/30")
                dtUpdDate = DateTime.Parse(Now.Year & "/03/31")
        End Select

        '�@�ʐ����f�[�^�̍X�V0
        intResult += dataAccess.UpdTeibetuSeikyuuDataForKessan0(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '�@�ʐ����f�[�^�̍X�V1
        intResult += dataAccess.UpdTeibetuSeikyuuDataForKessan1(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '�@�ʐ����f�[�^�̍X�V2
        intResult += dataAccess.UpdTeibetuSeikyuuDataForKessan2(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '�@�ʐ����f�[�^�̍X�V3
        intResult += dataAccess.UpdTeibetuSeikyuuDataForKessan3(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '�@�ʐ����f�[�^�̍X�V4
        intResult += dataAccess.UpdTeibetuSeikyuuDataForKessan4(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '�@�ʐ����f�[�^�̍X�V5
        intResult += dataAccess.UpdTeibetuSeikyuuDataForKessan5(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '�@�ʐ����A�g�Ǘ��e�[�u����UPDATE/INSERT
        intRenkeiResult += renkeiDataAccess.setTeibetuSeikyuuRenkei(dtNowDtTime, LoginUserId)

        '�X�ʏ��������f�[�^�̍X�V
        intResult += dataAccess.UpdTenbetuSyokiSeikyuuDataForKessan(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '�X�ʏ��������A�g�Ǘ��e�[�u����UPDATE/INSERT
        intRenkeiResult += renkeiDataAccess.setTenbetuSyokiSeikyuuRenkei(dtNowDtTime, LoginUserId)

        '�X�ʐ����f�[�^�̍X�V1
        intResult += dataAccess.UpdTenbetuSeikyuuDataForKessan1(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '�X�ʐ����f�[�^�̍X�V2
        intResult += dataAccess.UpdTenbetuSeikyuuDataForKessan2(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)
        '�X�ʐ����A�g�Ǘ��e�[�u����UPDATE/INSERT
        intRenkeiResult += renkeiDataAccess.setTenbetuSeikyuuRenkei(dtNowDtTime, LoginUserId)

        Return intResult
    End Function

    ''' <summary>
    ''' �ėp����f�[�^�e�[�u�����ꊇ�X�V���܂��B
    ''' </summary>
    ''' <param name="dtNowDtTime">�X�V����</param>
    ''' <returns></returns>
    ''' <remarks>��������</remarks>
    Private Function KessanUpdSyoriHannyouUriage(ByVal dtNowDtTime As DateTime) As Integer
        Dim intResult As Integer
        Dim dtUriFrom As Date
        Dim dtUriTo As Date
        Dim dtSeikyuuFrom As Date
        Dim dtSeikyuuTo As Date
        Dim dtUpdDate As Date
        Dim dtAcc As New GetujiIkkatuUpdateDataAccess

        '���t�̐ݒ�
        Select Case Now.Month
            Case 9, 10
                dtUriFrom = DateTime.Parse(Now.Year & "/09/01")
                dtUriTo = DateTime.Parse(Now.Year & "/09/30")
                dtSeikyuuFrom = DateTime.Parse(Now.Year & "/10/01")
                dtSeikyuuTo = DateTime.Parse(Now.Year & "/10/31")
                dtUpdDate = DateTime.Parse(Now.Year & "/09/30")
            Case 3, 4
                dtUriFrom = DateTime.Parse(Now.Year & "/03/01")
                dtUriTo = DateTime.Parse(Now.Year & "/03/31")
                dtSeikyuuFrom = DateTime.Parse(Now.Year & "/04/01")
                dtSeikyuuTo = DateTime.Parse(Now.Year & "/04/30")
                dtUpdDate = DateTime.Parse(Now.Year & "/03/31")
        End Select

        '�ėp����f�[�^�e�[�u���̈ꊇ�X�V
        intResult = dtAcc.UpdHannyouUriageDataForKessan(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)

        Return intResult
    End Function

    ''' <summary>
    ''' ����f�[�^�e�[�u�����ꊇ�X�V���܂��B
    ''' </summary>
    ''' <param name="dtNowDtTime">�X�V����</param>
    ''' <returns></returns>
    ''' <remarks>��������</remarks>
    Private Function KessanUpdSyoriUriageData(ByVal dtNowDtTime As DateTime) As Integer
        Dim intResult As Integer
        Dim dtUriFrom As Date
        Dim dtUriTo As Date
        Dim dtSeikyuuFrom As Date
        Dim dtSeikyuuTo As Date
        Dim dtUpdDate As Date
        Dim dtAcc As New GetujiIkkatuUpdateDataAccess

        '���t�̐ݒ�
        Select Case Now.Month
            Case 9, 10
                dtUriFrom = DateTime.Parse(Now.Year & "/09/01")
                dtUriTo = DateTime.Parse(Now.Year & "/09/30")
                dtSeikyuuFrom = DateTime.Parse(Now.Year & "/10/01")
                dtSeikyuuTo = DateTime.Parse(Now.Year & "/10/31")
                dtUpdDate = DateTime.Parse(Now.Year & "/09/30")
            Case 3, 4
                dtUriFrom = DateTime.Parse(Now.Year & "/03/01")
                dtUriTo = DateTime.Parse(Now.Year & "/03/31")
                dtSeikyuuFrom = DateTime.Parse(Now.Year & "/04/01")
                dtSeikyuuTo = DateTime.Parse(Now.Year & "/04/30")
                dtUpdDate = DateTime.Parse(Now.Year & "/03/31")
        End Select

        '����f�[�^�e�[�u���̈ꊇ�X�V
        intResult = dtAcc.UpdUriageDataForKessan(dtUriFrom, dtUriTo, dtSeikyuuFrom, dtSeikyuuTo, dtUpdDate, LoginUserId, dtNowDtTime)

        Return intResult
    End Function
#End Region

#Region "�����m�菈��"
#Region "���݂̌����m��\��Ǘ��e�[�u������A�����󋵂��擾"
    ''' <summary>
    ''' ���݂̌����m��\��Ǘ��e�[�u������A�����󋵂��擾
    ''' </summary>
    ''' <param name="targetYM">�����N��(YYYY/MM/01)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGetujiKakuteiYoyakuData( _
                                               ByVal targetYM As Date _
                                               ) As Object
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetGetujiKakuteiYoyakuData", _
                                                    targetYM _
                                                    )

        Dim dataAccess As New GetujiIkkatuUpdateDataAccess
        Dim dbResult As Object

        '�f�[�^�擾
        dbResult = dataAccess.searchGetujiKakuteiYoyakuData(targetYM)

        '�߂�
        Return dbResult
    End Function
#End Region

#Region "�����m�菈���\��o�^�E�������C��"
    ''' <summary>
    ''' �����m�菈���\��o�^�E�������C��
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="exeType">���s�^�C�v(1:�\��A2:�\�����)</param>
    ''' <param name="targetYM">�Ώ۔N��(YYYY/MM/����)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EditGetujiKakuteiYoyaku(ByVal sender As System.Object, _
                                            ByVal exeType As Integer, _
                                            ByVal targetYM As Date) As Integer
        Dim intResult As Integer
        Dim nowSyoriJoukyou As Object

        Try

            '�e��e�[�u���Ɗe��A�g�Ǘ��e�[�u���̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                nowSyoriJoukyou = GetGetujiKakuteiYoyakuData(targetYM)

                If exeType = 1 Then
                    '�\��
                    If nowSyoriJoukyou Is Nothing Then
                        '�V�K�o�^
                        intResult = SetGetujiKakuteiYoyaku(targetYM, 1)

                    ElseIf nowSyoriJoukyou = 0 Then
                        '�����󋵍X�V(�\��)
                        intResult = UpdGetujiKakuteiYoyakuJoukyou(targetYM, 0, 1)

                    Else
                        intResult = Integer.MinValue

                    End If

                ElseIf exeType = 2 Then
                    '�\�����
                    If nowSyoriJoukyou IsNot Nothing AndAlso nowSyoriJoukyou = 1 Then
                        '�����󋵍X�V(����)
                        intResult = UpdGetujiKakuteiYoyakuJoukyou(targetYM, 1, 0)

                    Else
                        intResult = Integer.MinValue

                    End If

                End If

                '�g�����U�N�V�����X�R�[�v�̊m��(�R�~�b�g)
                scope.Complete()

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    '�G���[�L���b�`�F�f�b�h���b�N
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            intResult = Integer.MinValue
        Catch exTransactionException As System.Transactions.TransactionException
            '�G���[�L���b�`�F�g�����U�N�V�����G���[
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            intResult = Integer.MinValue
        End Try

        '���ʖ߂�
        Return intResult

    End Function


#End Region

#Region "�����m�菈���\��o�^"
    ''' <summary>
    ''' �����m�菈���\��o�^
    ''' </summary>
    ''' <param name="targetYM">�Ώ۔N��(YYYY/MM/����)</param>
    ''' <param name="joukyou">�Z�b�g���鏈����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetGetujiKakuteiYoyaku(ByVal targetYM As Date, _
                                           ByVal joukyou As Integer _
                                           ) As Integer
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetGetujiKakuteiYoyakuData", _
                                                    targetYM _
                                                    )

        Dim dataAccess As New GetujiIkkatuUpdateDataAccess
        Dim dbResult As Integer

        '�f�[�^�擾
        dbResult = dataAccess.insertGetujiKakuteiYoyaku(targetYM, _
                                                        joukyou, _
                                                        LoginUserId)

        '�߂�
        Return dbResult
    End Function
#End Region

#Region "�����m�菈���\��󋵍X�V"
    ''' <summary>
    ''' �����m�菈���\��󋵍X�V
    ''' </summary>
    ''' <param name="targetYM">�Ώ۔N��(YYYY/MM/����)</param>
    ''' <param name="joukyouFrom">�ύX�O������</param>
    ''' <param name="joukyouTo">�ύX�㏈����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdGetujiKakuteiYoyakuJoukyou(ByVal targetYM As Date, _
                                                  ByVal joukyouFrom As Integer, _
                                                  ByVal joukyouTo As Integer _
                                                  ) As Integer
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetGetujiKakuteiYoyakuData", _
                                                    targetYM _
                                                    )

        Dim dataAccess As New GetujiIkkatuUpdateDataAccess
        Dim dbResult As Integer

        '�f�[�^�擾
        dbResult = dataAccess.updateGetujiKakuteiYoyakuJoukyou(targetYM, _
                                                               joukyouFrom, _
                                                               joukyouTo, _
                                                               LoginUserId)

        '�߂�
        Return dbResult
    End Function
#End Region

#Region "���߂̌����m�菈���N�������擾����"
    ''' <summary>
    ''' ���߂̌����m�菈���N�������擾����
    ''' </summary>
    ''' <returns>���߂̌����m�菈���N����</returns>
    ''' <remarks></remarks>
    Public Function getGetujiKakuteiLastSyoriDate() As Date
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getGetujiKakuteiLastSyoriDate")

        Dim dtAcc As New GetujiIkkatuUpdateDataAccess
        Dim dbResult As Object

        '�f�[�^�擾
        dbResult = dtAcc.getGetujiKakuteiLastSyoriDate()

        If dbResult Is Nothing Then
            Return Date.MinValue
        Else
            Return Date.Parse(dbResult)
        End If


    End Function
#End Region

#End Region

End Class
