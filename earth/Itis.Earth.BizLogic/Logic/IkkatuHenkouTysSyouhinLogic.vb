Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' �n�Ջ��ʏ����N���X
''' </summary>
''' <remarks></remarks>
Public Class IkkatuHenkouTysSyouhinLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'Utilities��MessegeLogic�N���X
    Dim mLogic As New MessageLogic

    'Utilities��StringLogic�N���X
    Dim sLogic As New StringLogic
    Dim cbLogic As New CommonBizLogic

#Region "Dictionary�pKey������萔"
    Public ReadOnly dkAddDatetime As String = "AddDatetime"
    Public ReadOnly dkAddDatetimeItem As String = "AddDatetimeItem"
    Public ReadOnly dkAddDatetimeJiban As String = "AddDatetimeJiban"
    Public ReadOnly dkAddLoginUserId As String = "AddLoginUserId"
    Public ReadOnly dkBikou As String = "Bikou"
    Public ReadOnly dkBunruiCd As String = "BunruiCd"
    Public ReadOnly dkGamenHyoujiNo As String = "GamenHyoujiNo"
    Public ReadOnly dkHattyuusyoGaku As String = "HattyuusyoGaku"
    Public ReadOnly dkHattyuusyoKakuninDate As String = "HattyuusyoKakuninDate"
    Public ReadOnly dkHattyuusyoKakuteiFlg As String = "HattyuusyoKakuteiFlg"
    Public ReadOnly dkHosyousyoNo As String = "HosyousyoNo"
    Public ReadOnly dkIkkatuNyuukinFlg As String = "IkkatuNyuukinFlg"
    Public ReadOnly dkIraiTousuu As String = "IraiTousuu"
    Public ReadOnly dkKakakuSetteiBasyo As String = "KakakuSetteiBasyo"
    Public ReadOnly dkKakuteiKbn As String = "KakuteiKbn"
    Public ReadOnly dkKameitenCd As String = "KameitenCd"
    Public ReadOnly dkKbn As String = "Kbn"
    Public ReadOnly dkKoumutenSeikyuuGaku As String = "KoumutenSeikyuuGaku"
    Public ReadOnly dkSeikyuuUmu As String = "SeikyuuUmu"
    Public ReadOnly dkSeikyuusyoHakDate As String = "SeikyuusyoHakDate"
    Public ReadOnly dkSesyuMei As String = "SesyuMei"
    Public ReadOnly dkSiireGaku As String = "SiireGaku"
    Public ReadOnly dkStatusIf As String = "StatusIf"
    Public ReadOnly dkSyouhinCd As String = "SyouhinCd"
    Public ReadOnly dkSyouhinKbn As String = "SyouhinKbn"
    Public ReadOnly dkSyouhinMei As String = "SyouhinMei"
    Public ReadOnly dkTatemonoYoutoNo As String = "TatemonoYoutoNo"
    Public ReadOnly dkTysGaiyou As String = "TysGaiyou"
    Public ReadOnly dkTysHouhou As String = "TysHouhou"
    Public ReadOnly dkTysKaisyaCd As String = "TysKaisyaCd"
    Public ReadOnly dkTysKaisyaJigyousyoCd As String = "TysKaisyaJigyousyoCd"
    Public ReadOnly dkTysMitsyoSakuseiDate As String = "TysMitsyoSakuseiDate"
    Public ReadOnly dkUpdDatetime As String = "UpdDatetime"
    Public ReadOnly dkUpdDatetimeItem As String = "UpdDatetimeItem"
    Public ReadOnly dkUpdDatetimeJiban As String = "UpdDatetimeJiban"
    Public ReadOnly dkUpdLoginUserId As String = "UpdLoginUserId"
    Public ReadOnly dkUriDate As String = "UriDate"
    Public ReadOnly dkUriDateItem1 As String = "UriDateItem1"
    Public ReadOnly dkUriGaku As String = "UriGaku"
    Public ReadOnly dkUriKeijouDate As String = "UriKeijouDate"
    Public ReadOnly dkUriKeijyouFlg As String = "UriKeijyouFlg"
    Public ReadOnly dkUriKeijyouFlgItem1 As String = "UriKeijyouFlgItem1"
    Public ReadOnly dkZeiKbn As String = "ZeiKbn"
    Public ReadOnly dkSeikyuuSaki As String = "SeikyuuSaki"
#End Region

#Region "�n�Ճf�[�^�X�V"
    ''' <summary>
    ''' �n�Ճf�[�^���X�V���܂��B�֘A����@�ʐ����f�[�^�̍X�V���s���܂�
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="listJibanRec">�X�V�Ώۂ̒n�Ճ��R�[�h</param>
    ''' <returns>True:�X�V���� False:�G���[�ɂ��X�V���s</returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal listJibanRec As List(Of JibanRecordIkkatuHenkouTysSyouhin), _
                                  ByVal listBrRec As List(Of BukkenRirekiRecord), _
                                  ByVal listTokuRec As List(Of TokubetuTaiouRecordBase)) As Boolean
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SaveJibanData", _
                                            sender, _
                                            listJibanRec, _
                                            listBrRec, _
                                            listTokuRec)

        ' Update�ɕK�v��SQL����������������N���X
        Dim upadteMake As New UpdateStringHelper
        ' �r������pSQL��
        Dim sqlString As String = ""
        ' Update��
        Dim updateString As String = ""
        ' �r���`�F�b�N�p�p�����[�^�̏����i�[����List(Of ParamRecord)
        Dim haitaList As New List(Of ParamRecord)
        ' �X�V�p�p�����[�^�̏����i�[����List(Of ParamRecord)
        Dim list As New List(Of ParamRecord)

        ' �A�g�e�[�u���f�[�^�o�^�p�f�[�^�̊i�[�p
        Dim renkeiJibanRec As New JibanRenkeiRecord
        Dim renkeiTeibetuList As New List(Of TeibetuSeikyuuRenkeiRecord)

        ' �X�V���t�擾
        Dim updDateTime As DateTime = DateTime.Now

        ' ReportJHS�A�W�Ώۃt���O(�f�t�H���g�FFalse)
        Dim flgEditReportIf As Boolean

        Dim intDBCnt As Integer = 0 '�X�V����
        Dim jibanRec As New JibanRecordIkkatuHenkouTysSyouhin  'TMP�p

        Dim jibanLogic As New JibanLogic

        Dim pUpdDateTime As DateTime
        '�X�V�����擾�i�V�X�e�������j
        pUpdDateTime = DateTime.Now

        Try
            ' �n�Ճe�[�u���Ɠ@�ʐ����̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '�����������[�v
                For intDBCnt = 0 To listJibanRec.Count - 1

                    '�Ώےn�Ճ��R�[�h����Ɨp�̃��R�[�h�N���X�Ɋi�[����
                    jibanRec = New JibanRecordIkkatuHenkouTysSyouhin
                    jibanRec = listJibanRec(intDBCnt)

                    ' �r���`�F�b�N�p���R�[�h�쐬
                    Dim jibanHaitaRec As New JibanHaitaRecord

                    ' �n�Ճf�[�^�p�f�[�^�A�N�Z�X�N���X
                    Dim dataAccess As JibanDataAccess = New JibanDataAccess

                    ' �V�K�o�^�A�X�V�̔���i�ۏ؏�NO�̔Ԏ��̐V�K�o�^�������j
                    Dim isNew As Boolean
                    isNew = dataAccess.ChkJibanNew(jibanRec.Kbn, jibanRec.HosyousyoNo)

                    ' �n�Ճ��R�[�h�̓��ꍀ�ڂ𕡐�
                    RecordMappingHelper.Instance.CopyRecordData(jibanRec, jibanHaitaRec)

                    ' �r���`�F�b�N�pSQL����������
                    sqlString = upadteMake.MakeHaitaSQLInfo(GetType(JibanHaitaRecord), jibanHaitaRec, haitaList)

                    ' �r���`�F�b�N���{
                    Dim haitaKekkaList As List(Of JibanHaitaRecord) = _
                                DataMappingHelper.Instance.getMapArray(Of JibanHaitaRecord)(GetType(JibanHaitaRecord), _
                                dataAccess.CheckHaita(sqlString, haitaList))

                    If haitaKekkaList.Count > 0 Then
                        Dim haitaErrorData As JibanHaitaRecord = haitaKekkaList(0)

                        ' �r���`�F�b�N�G���[
                        mLogic.CallHaitaErrorMessage(sender, haitaErrorData.UpdLoginUserId, haitaErrorData.UpdDatetime, "�n�Ճe�[�u��")
                        Return False

                    End If

                    '�^�M�`�F�b�N����
                    Dim jibanRecOld As New JibanRecord
                    jibanRecOld = jibanLogic.GetJibanData(jibanRec.Kbn, jibanRec.HosyousyoNo)
                    '�^�M�`�F�b�N�ɕK�v�ȃf�[�^������DB�f�[�^����R�s�[
                    jibanRec.TysKibouDate = jibanRecOld.TysKibouDate
                    jibanRec.SyoudakusyoTysDate = jibanRecOld.SyoudakusyoTysDate
                    jibanRec.Syouhin3Records = jibanRecOld.Syouhin3Records



                    Dim YosinLogic As New YosinLogic
                    Dim intYosinResult As Integer = YosinLogic.YosinCheck(1, jibanRecOld, jibanRec)
                    Select Case intYosinResult
                        Case EarthConst.YOSIN_ERROR_YOSIN_OK
                            '�G���[�Ȃ�
                        Case EarthConst.YOSIN_ERROR_YOSIN_NG
                            '�^�M���x�z����
                            mLogic.AlertMessage(sender, Messages.MSG089E, 1)
                            Return False
                        Case Else
                            '6:�^�M�Ǘ����擾�G���[
                            '7:�^�M�Ǘ��e�[�u���X�V�G���[
                            '8:���[�����M�����G���[
                            '9:���̑��G���[
                            mLogic.AlertMessage(sender, String.Format(Messages.MSG090E, intYosinResult.ToString()), 1)
                            Return False
                    End Select

                    ' �X�V�����e�[�u���̓o�^
                    ' �������
                    If dataAccess.AddKoushinRireki(jibanRec.Kbn, _
                                                   jibanRec.HosyousyoNo, _
                                                   JibanDataAccess.enumItemName.TyousaKaisya, _
                                                   EarthConst.RIREKI_TYOUSA_KAISYA, _
                                                   jibanRec.TysKaisyaCd & jibanRec.TysKaisyaJigyousyoCd, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' �G���[�����������I��
                        Return False
                    End If

                    ' �A�g�p�e�[�u���ɓo�^����i�n�Ձ|�X�V�j
                    renkeiJibanRec.Kbn = jibanRec.Kbn
                    renkeiJibanRec.HosyousyoNo = jibanRec.HosyousyoNo
                    renkeiJibanRec.RenkeiSijiCd = 2
                    renkeiJibanRec.SousinJykyCd = 0
                    renkeiJibanRec.UpdLoginUserId = jibanRec.UpdLoginUserId
                    If dataAccess.EditJibanRenkeiData(renkeiJibanRec, True) <> 1 Then
                        ' �o�^�Ɏ��s�����̂ŏ������f
                        Return False
                    End If

                    ' ReportJHS�A�W�Ώۃ`�F�b�N���s��(�o�R�F0,1,5�̏ꍇ�̂�)
                    flgEditReportIf = False '������
                    If jibanRec.Keiyu = 0 Or _
                       jibanRec.Keiyu = 1 Or _
                       jibanRec.Keiyu = 5 Then

                        ' �i���f�[�^�����n�f�[�^�A�N�Z�X�N���X
                        Dim reportAccess As New ReportIfDataAccess

                        ' �A�g������Аݒ�}�X�^�̑��݃`�F�b�N
                        If reportAccess.ChkRenkeiTyousaKaisya(jibanRec.TysKaisyaCd, _
                                                               jibanRec.TysKaisyaJigyousyoCd) = True Then
                            ' �Ώۂ̏ꍇ�A�t���O�𗧂Ă�
                            flgEditReportIf = True
                            ' ���f�ΏۂȂ̂Ōo�R��5�ɕύX
                            jibanRec.Keiyu = 5
                        Else
                            ' �A�g������Аݒ�}�X�^�̑��݃`�F�b�N��NG�ɂȂ����ꍇ�A�o�R��9�ɕύX
                            jibanRec.Keiyu = 9
                        End If
                    End If

                    ' �X�V�pUPDATE����������
                    updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordIkkatuHenkouTysSyouhin), jibanRec, list)

                    ' �n�Ճe�[�u���̍X�V���s���i�V�K�͍̔Ԏ��A�폜�͕ʋ@�\�Ȃ̂Œn�Ֆ{�͍̂X�V�̂݁j
                    If dataAccess.UpdateJibanData(updateString, list) = True Then

                        '**************************************************************************
                        ' �@�ʐ����f�[�^�̒ǉ��E�X�V
                        '**************************************************************************
                        '�@�ʐ����e�[�u���f�[�^����p
                        Dim TeibetuSyuuseiLogic As New TeibetuSyuuseiLogic
                        Dim tempTeibetuRec As New TeibetuSeikyuuRecord

                        '***************************
                        ' ���i�R�[�h�P�̒ǉ��E�X�V
                        '***************************
                        '���i�P���R�[�h���e���|�����ɃZ�b�g
                        tempTeibetuRec = jibanRec.Syouhin1Record

                        '��������/�d����̐ݒ�
                        cbLogic.SetSeikyuuSiireSakiInfo(tempTeibetuRec, jibanRecOld.Syouhin1Record)

                        '������Ŋz�̐ݒ�
                        cbLogic.SetUriageSyouhiZeiGaku(tempTeibetuRec, jibanRecOld.Syouhin1Record)

                        '���i�P�f�[�^����
                        If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                 tempTeibetuRec, _
                                                                 EarthConst.SOUKO_CD_SYOUHIN_1, _
                                                                 1, _
                                                                 jibanRec, _
                                                                 renkeiTeibetuList, _
                                                                 GetType(TeibetuSeikyuuRecord) _
                                                                 ) = False Then
                            Return False
                        End If

                        '***************************
                        ' ���i�R�[�h�Q�̒ǉ��E�X�V
                        '***************************
                        Dim i As Integer
                        Dim recOldTeibetuForSyouhiZei As TeibetuSeikyuuRecord = Nothing     '����Ōv�Z�p�@�ʐ������R�[�h
                        For i = 1 To EarthConst.SYOUHIN2_COUNT
                            If jibanRec.Syouhin2Records.ContainsKey(i) = True Then

                                '���i�Q���R�[�h���e���|�����ɃZ�b�g
                                tempTeibetuRec = jibanRec.Syouhin2Records.Item(i)

                                If jibanRecOld.Syouhin2Records IsNot Nothing Then
                                    If jibanRecOld.Syouhin2Records.ContainsKey(i) = True Then
                                        '��������/�d����̐ݒ�
                                        cbLogic.SetSeikyuuSiireSakiInfo(tempTeibetuRec, jibanRecOld.Syouhin2Records.Item(i))
                                        '����Ōv�Z�p
                                        recOldTeibetuForSyouhiZei = jibanRecOld.Syouhin2Records.Item(i)
                                    Else
                                        '����Ōv�Z�p
                                        recOldTeibetuForSyouhiZei = Nothing
                                    End If
                                Else
                                    '����Ōv�Z�p
                                    recOldTeibetuForSyouhiZei = Nothing
                                End If

                                '������Ŋz�̐ݒ�
                                cbLogic.SetUriageSyouhiZeiGaku(tempTeibetuRec, recOldTeibetuForSyouhiZei)

                                ' ���i�Q�̓@�ʐ����f�[�^���X�V���܂�
                                If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                         tempTeibetuRec, _
                                                                         tempTeibetuRec.BunruiCd, _
                                                                         i, _
                                                                         jibanRec, _
                                                                         renkeiTeibetuList, _
                                                                         GetType(TeibetuSeikyuuRecord) _
                                                                         ) = False Then
                                    Return False
                                End If
                            Else
                                ' �폜�����p(�폜�m�F�͏��i�Q�̕��ރR�[�h���ꂩ�Ŗ��Ȃ�)
                                If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                         Nothing, _
                                                                         EarthConst.SOUKO_CD_SYOUHIN_2_110, _
                                                                         i, _
                                                                         jibanRec, _
                                                                         renkeiTeibetuList _
                                                                         ) = False Then
                                    Return False
                                End If
                            End If

                        Next

                        ' �@�ʐ����A�g���f�Ώۂ����݂���ꍇ�A���f���s��
                        For Each renkeiTeibetuRec As TeibetuSeikyuuRenkeiRecord In renkeiTeibetuList
                            ' �A�g�p�e�[�u���ɔ��f����i�@�ʐ����j
                            If dataAccess.EditTeibetuRenkeiData(renkeiTeibetuRec) <> 1 Then
                                ' �o�^�Ɏ��s�����̂ŏ������f
                                Return False
                            End If
                        Next

                        '��Ƀ`�F�b�N���Ă�����ReportJHS�A�W�Ώۃ`�F�b�N�����ɁA�i���f�[�^�e�[�u���X�V�������s��
                        If flgEditReportIf Then
                            ' �i���f�[�^�e�[�u���X�V�����ďo
                            If jibanLogic.EditReportIfData(jibanRec) = False Then
                                ' �G���[�����������I��
                                Return False
                            End If
                        End If

                    Else
                        Return False
                    End If
                Next

                For intDBCnt = 0 To listBrRec.Count - 1
                    Dim brRec As BukkenRirekiRecord = listBrRec(intDBCnt)

                    ' �����������e�[�u���ǉ�(�ۏؗL���ύX���̂݁A��������T�ɐV�K�ǉ�����)
                    If Not brRec Is Nothing Then
                        Dim brLogic As New BukkenRirekiLogic

                        '�V�K�ǉ��p�X�N���v�g����ю��s
                        If brLogic.InsertBukkenRireki(brRec) = False Then
                            Return False
                        End If
                    End If
                Next

                '**********************************************
                ' ���ʑΉ��f�[�^�̍X�V(�@�ʐ����f�[�^�Ƃ̕R�t)
                '**********************************************
                If Not listTokuRec Is Nothing Then
                    If cbLogic.pf_setKey_TokubetuTaiou_TeibetuSeikyuu(sender, listTokuRec, jibanRec.UpdLoginUserId, EarthEnum.emGamenInfo.IkkatuTysSyouhinInfo) = False Then
                        Return False
                        Exit Function
                    End If
                End If


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
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            '�G���[�L���b�`�F�g�����U�N�V�����G���[
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True

    End Function
#End Region

    ''' <summary>
    ''' ������^�񋓑̘̂A������
    ''' </summary>
    ''' <param name="IEnum">�񋓑́i������^�j</param>
    ''' <returns>�A����̕�����</returns>
    ''' <remarks></remarks>
    Public Function getJoinString(ByVal IEnum As IEnumerator(Of String)) As String
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getJoinString", _
                                            IEnum)
        Dim strJoinStr As String = ""

        IEnum.MoveNext()
        strJoinStr = IEnum.Current

        Do While IEnum.MoveNext
            strJoinStr &= EarthConst.SEP_STRING & IEnum.Current
        Loop

        Return strJoinStr

    End Function

    ''' <summary>
    ''' �_���[($$$)��؂�̕������z��ɕϊ�
    ''' </summary>
    ''' <param name="strDollar">�_���[($$$)��؂�̕�����</param>
    ''' <returns>�_���[($$$)�ŋ�؂����z��</returns>
    ''' <remarks></remarks>
    Public Function getArrayFromDollarSep(ByVal strDollar As String, Optional ByVal delSpace As Boolean = False) As String()
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getArrayFromDollarSep", _
                                            strDollar)
        Dim strWk() As String
        Dim strSep(0) As String
        strSep(0) = EarthConst.SEP_STRING

        If delSpace Then
            strWk = strDollar.Split(strSep, System.StringSplitOptions.RemoveEmptyEntries)
        Else
            strWk = strDollar.Split(strSep, System.StringSplitOptions.None)
        End If


        Return strWk
    End Function

    ''' <summary>
    ''' �v�Z�����pHidden�̒l���i�[�����z���Dictionary�ɓo�^
    ''' </summary>
    ''' <param name="strItem1ChgValues">�v�Z�����pHidden�̒l���i�[�����z��</param>
    ''' <returns>�v�Z�����pHidden�̒l���i�[����Dictionary</returns>
    ''' <remarks>����ʂɕ\������Ă�����z�ȊO��o�^ �� �v�Z������ɔ���������z�݂̂̕ύX�͍Čv�Z���s�v�Ȉ�</remarks>
    Public Function getDicItem1(ByVal strItem1ChgValues() As String) As Dictionary(Of String, String)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getDicItem1", _
                                            strItem1ChgValues)

        Dim dic As New Dictionary(Of String, String)

        'IkkatuHenkouTysSyouhin1RecordCtrl.setCtrlFromJibanRec���\�b�h�Ɠ������Ԃœo�^���邱��
        With dic
            '�n�Ճe�[�u���p�f�[�^�̎擾
            .Add(dkKbn, strItem1ChgValues(0))
            .Add(dkHosyousyoNo, strItem1ChgValues(1))
            .Add(dkTysHouhou, strItem1ChgValues(2))
            .Add(dkTysKaisyaCd, strItem1ChgValues(3))
            .Add(dkTysKaisyaJigyousyoCd, strItem1ChgValues(4))
            .Add(dkKameitenCd, strItem1ChgValues(5))
            .Add(dkSesyuMei, strItem1ChgValues(6))
            .Add(dkSyouhinKbn, strItem1ChgValues(7))
            .Add(dkTysGaiyou, strItem1ChgValues(8))
            .Add(dkIraiTousuu, strItem1ChgValues(9))
            .Add(dkKakakuSetteiBasyo, strItem1ChgValues(10))
            .Add(dkTatemonoYoutoNo, strItem1ChgValues(11))
            .Add(dkStatusIf, strItem1ChgValues(12))
            .Add(dkAddDatetimeJiban, strItem1ChgValues(13))
            .Add(dkUpdDatetimeJiban, strItem1ChgValues(14))
            '�@�ʐ����e�[�u���p�f�[�^�̎擾
            .Add(dkBunruiCd, strItem1ChgValues(15))
            .Add(dkGamenHyoujiNo, strItem1ChgValues(16))
            .Add(dkSyouhinCd, strItem1ChgValues(17))
            .Add(dkZeiKbn, strItem1ChgValues(18))
            .Add(dkSeikyuusyoHakDate, strItem1ChgValues(19))
            .Add(dkUriDate, strItem1ChgValues(20))
            .Add(dkSeikyuuUmu, strItem1ChgValues(21))
            .Add(dkKakuteiKbn, strItem1ChgValues(22))
            .Add(dkUriKeijyouFlg, strItem1ChgValues(23))
            .Add(dkUriKeijouDate, strItem1ChgValues(24))
            .Add(dkBikou, strItem1ChgValues(25))
            .Add(dkHattyuusyoGaku, strItem1ChgValues(26))
            .Add(dkHattyuusyoKakuninDate, strItem1ChgValues(27))
            .Add(dkIkkatuNyuukinFlg, strItem1ChgValues(28))
            .Add(dkTysMitsyoSakuseiDate, strItem1ChgValues(29))
            .Add(dkHattyuusyoKakuteiFlg, strItem1ChgValues(30))
            .Add(dkAddLoginUserId, strItem1ChgValues(31))
            .Add(dkAddDatetimeItem, strItem1ChgValues(32))
            .Add(dkUpdLoginUserId, strItem1ChgValues(33))
            .Add(dkUpdDatetimeItem, strItem1ChgValues(34))
        End With

        Return dic

    End Function

    ''' <summary>
    ''' �ύX�m�F�pHidden�̋��z���i�[�����z���Dictionary�ɓo�^
    ''' </summary>
    ''' <param name="strItem1ChgValues">�ύX�m�F�pHidden�̒l���i�[�����z��</param>
    ''' <returns>�ύX�m�F�pHidden�̒l���i�[����Dictionary</returns>
    ''' <remarks></remarks>
    Public Function getDicItem1Kingaku(ByVal strItem1ChgValues) As Dictionary(Of String, String)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getDicItem1Kingaku", _
                                            strItem1ChgValues)
        Dim dic As New Dictionary(Of String, String)
        'IkkatuHenkouTysSyouhin1RecordCtrl.setCtrlFromJibanRec���\�b�h�Ɠ������Ԃœo�^���邱��
        With dic
            .Add(dkKoumutenSeikyuuGaku, strItem1ChgValues(0))
            .Add(dkUriGaku, strItem1ChgValues(1))
            .Add(dkSiireGaku, strItem1ChgValues(2))
        End With

        Return dic

    End Function

    ''' <summary>
    ''' �v�Z�����pHidden�̒l���i�[�����z���Dictionary�ɓo�^
    ''' </summary>
    ''' <param name="strItem2ChgValues">�v�Z�����pHidden�̒l���i�[�����z��</param>
    ''' <param name="isMakeBlank">���Dictionary�쐬���f�t���O</param>
    ''' <returns>�v�Z�����pHidden�̒l���i�[����Dictionary</returns>
    ''' <remarks></remarks>
    Public Function getDicItem2(ByVal strItem2ChgValues() As String, ByVal isMakeBlank As Boolean) As Dictionary(Of String, String)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getDicItem2", _
                                                    strItem2ChgValues, _
                                                    isMakeBlank)
        Dim dic As New Dictionary(Of String, String)
        'IkkatuHenkouTysSyouhin2RecordCtrl.setCtrlFromJibanRec���\�b�h�Ɠ������Ԃœo�^���邱��
        With dic
            If isMakeBlank Then
                '�n�Ճe�[�u���p�f�[�^�̎擾
                .Add(dkKameitenCd, String.Empty)
                '�@�ʐ����e�[�u���p�f�[�^�̎擾(���i�P)
                .Add(dkUriKeijyouFlgItem1, String.Empty)
                .Add(dkUriDateItem1, String.Empty)
                '�@�ʐ����e�[�u���p�f�[�^�̎擾(���i�Q)
                .Add(dkKbn, String.Empty)
                .Add(dkHosyousyoNo, String.Empty)
                .Add(dkBunruiCd, String.Empty)
                .Add(dkGamenHyoujiNo, String.Empty)
                .Add(dkSyouhinCd, String.Empty)
                .Add(dkZeiKbn, String.Empty)
                .Add(dkSeikyuusyoHakDate, String.Empty)
                .Add(dkUriDate, String.Empty)
                .Add(dkSeikyuuUmu, String.Empty)
                .Add(dkKakuteiKbn, String.Empty)
                .Add(dkUriKeijyouFlg, String.Empty)
                .Add(dkUriKeijouDate, String.Empty)
                .Add(dkBikou, String.Empty)
                .Add(dkHattyuusyoGaku, String.Empty)
                .Add(dkHattyuusyoKakuninDate, String.Empty)
                .Add(dkIkkatuNyuukinFlg, String.Empty)
                .Add(dkTysMitsyoSakuseiDate, String.Empty)
                .Add(dkHattyuusyoKakuteiFlg, String.Empty)
                .Add(dkAddLoginUserId, String.Empty)
                .Add(dkAddDatetime, String.Empty)
                .Add(dkUpdLoginUserId, String.Empty)
                .Add(dkUpdDatetime, String.Empty)
                .Add(dkSeikyuuSaki, String.Empty)
            Else
                '�n�Ճe�[�u���p�f�[�^�̎擾
                .Add(dkKameitenCd, strItem2ChgValues(0))
                '�@�ʐ����e�[�u���p�f�[�^�̎擾(���i�P)
                .Add(dkUriKeijyouFlgItem1, strItem2ChgValues(1))
                .Add(dkUriDateItem1, strItem2ChgValues(2))
                '�@�ʐ����e�[�u���p�f�[�^�̎擾(���i�Q)
                .Add(dkKbn, strItem2ChgValues(3))
                .Add(dkHosyousyoNo, strItem2ChgValues(4))
                .Add(dkBunruiCd, strItem2ChgValues(5))
                .Add(dkGamenHyoujiNo, strItem2ChgValues(6))
                .Add(dkSyouhinCd, strItem2ChgValues(7))
                .Add(dkZeiKbn, strItem2ChgValues(8))
                .Add(dkSeikyuusyoHakDate, strItem2ChgValues(9))
                .Add(dkUriDate, strItem2ChgValues(10))
                .Add(dkSeikyuuUmu, strItem2ChgValues(11))
                .Add(dkKakuteiKbn, strItem2ChgValues(12))
                .Add(dkUriKeijyouFlg, strItem2ChgValues(13))
                .Add(dkUriKeijouDate, strItem2ChgValues(14))
                .Add(dkBikou, strItem2ChgValues(15))
                .Add(dkHattyuusyoGaku, strItem2ChgValues(16))
                .Add(dkHattyuusyoKakuninDate, strItem2ChgValues(17))
                .Add(dkIkkatuNyuukinFlg, strItem2ChgValues(18))
                .Add(dkTysMitsyoSakuseiDate, strItem2ChgValues(19))
                .Add(dkHattyuusyoKakuteiFlg, strItem2ChgValues(20))
                .Add(dkAddLoginUserId, strItem2ChgValues(21))
                .Add(dkAddDatetime, strItem2ChgValues(22))
                .Add(dkUpdLoginUserId, strItem2ChgValues(23))
                .Add(dkUpdDatetime, strItem2ChgValues(24))
                .Add(dkSeikyuuSaki, strItem2ChgValues(25))
            End If
        End With

        Return dic

    End Function

    ''' <summary>
    ''' �ύX�m�F�pHidden�̋��z���i�[�����z���Dictionary�ɓo�^
    ''' </summary>
    ''' <param name="strItem2ChgValues">�ύX�m�F�pHidden�̒l���i�[�����z��</param>
    ''' <param name="isMakeBlank">���Dictionary�쐬���f�t���O</param>
    ''' <returns>�ύX�m�F�pHidden�̒l���i�[����Dictionary</returns>
    ''' <remarks></remarks>
    Public Function getDicItem2Kingaku(ByVal strItem2ChgValues() As String, ByVal isMakeBlank As Boolean) As Dictionary(Of String, String)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getDicItem2Kingaku", _
                                                    strItem2ChgValues, _
                                                    isMakeBlank)
        Dim dic As New Dictionary(Of String, String)

        'IkkatuHenkouTysSyouhin2RecordCtrl.setCtrlFromJibanRec���\�b�h�Ɠ������Ԃœo�^���邱��
        With dic
            If isMakeBlank Then
                .Add(dkGamenHyoujiNo, String.Empty)
                .Add(dkKoumutenSeikyuuGaku, String.Empty)
                .Add(dkUriGaku, String.Empty)
                .Add(dkSiireGaku, String.Empty)
            Else
                .Add(dkGamenHyoujiNo, strItem2ChgValues(0))
                .Add(dkKoumutenSeikyuuGaku, strItem2ChgValues(1))
                .Add(dkUriGaku, strItem2ChgValues(2))
                .Add(dkSiireGaku, strItem2ChgValues(3))
            End If
        End With

        Return dic

    End Function

End Class
