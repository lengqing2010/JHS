Imports System.Transactions
Imports System.Web.UI

''' <summary>
''' �\�����͉�ʗp�̃��W�b�N�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class MousikomiInputLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'Utilities��MessegeLogic�N���X
    Dim mLogic As New MessageLogic

    Dim strlogic As New StringLogic

    '�X�V�����ێ��p
    Dim dateUpdDateTime As DateTime

    '���i�ݒ�ꏊ
    Dim intKakakuSetteiBasyo As Integer = Integer.MinValue

    '���ʃ��W�b�N�N���X
    Private cbLogic As New CommonBizLogic

#Region "�f�[�^�X�V"


    ''' <summary>
    ''' �n�Ճe�[�u���̒ǉ��X�V���s���܂��B
    ''' ���敪+�ԍ��̎����̔Ԃ���ђn�ՐV�K�ǉ��A�X�V�������s�Ȃ��B
    ''' </summary>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <param name="listBr">�����������R�[�h�̃��X�g</param>
    ''' <returns>True or False</returns>
    ''' <remarks></remarks>
    Public Function saveData( _
                                    ByVal sender As Object, _
                                    ByRef jibanRec As JibanRecordMousikomiInput, _
                                    ByRef intGenzaiSyoriKensuu As Integer, _
                                    ByRef listBr As List(Of BukkenRirekiRecord) _
                                  ) As Boolean

        Try
            ' �n�Ճe�[�u���Ɠ@�ʐ����̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '�n�ՍX�V
                If Me.UpdateJibanData(sender, jibanRec, listBr) = False Then
                    Return False
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

        '��������(���v) += ��������(������)
        intGenzaiSyoriKensuu = jibanRec.SyoriKensuu

        Return True
    End Function

    ''' <summary>
    ''' �n�Ճf�[�^���X�V���܂��B�֘A����@�ʐ����f�[�^�̍X�V���s���܂�
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="jibanRec">�X�V�Ώۂ̒n�Ճ��R�[�h</param>
    ''' <param name="listBr">�o�^�Ώۂ̕����������R�[�h�̃��X�g</param>
    ''' <returns>True:�X�V���� False:�G���[�ɂ��X�V���s</returns>
    ''' <remarks></remarks>
    Public Function UpdateJibanData(ByVal sender As Object, _
                                    ByRef jibanRec As JibanRecordMousikomiInput, _
                                    ByRef listBr As List(Of BukkenRirekiRecord) _
                                    ) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibanData", _
                                            sender, _
                                            jibanRec, _
                                            listBr)

        Dim jLogic As New JibanLogic

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

        ' �r���`�F�b�N�p���R�[�h�쐬
        Dim jibanHaitaRec As New JibanHaitaRecord

        ' �n�Ճf�[�^�p�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' �V�K�o�^�A�X�V�̔���i�ۏ؏�NO�̔Ԏ��̐V�K�o�^�������j
        Dim isNew As Boolean
        isNew = dataAccess.ChkJibanNew(jibanRec.Kbn, jibanRec.HosyousyoNo)

        ' �A�g�e�[�u���f�[�^�o�^�p�f�[�^�̊i�[�p
        Dim renkeiJibanRec As New JibanRenkeiRecord
        Dim renkeiTeibetuList As New List(Of TeibetuSeikyuuRenkeiRecord)

        ' ReportJHS�A�W�Ώۃt���O(�f�t�H���g�FFalse)
        Dim flgEditReportIf As Boolean

        ' �A���������`�F�b�N
        If jibanRec.RentouBukkenSuu = Nothing OrElse jibanRec.RentouBukkenSuu <= 0 Then
            jibanRec.RentouBukkenSuu = 1
        End If
        ' ���������`�F�b�N
        If jibanRec.SyoriKensuu = Nothing OrElse jibanRec.SyoriKensuu <= 0 Then
            jibanRec.SyoriKensuu = 0
        End If

        ' �ԍ�(�ۏ؏�NO) ��Ɨp
        Dim intTmpBangou As Integer = CInt(jibanRec.HosyousyoNo) + jibanRec.SyoriKensuu '�ԍ�+��������
        Dim strTmpBangou As String = Format(intTmpBangou, "0000000000") '�t�H�[�}�b�g

        Dim strRetBangou As String = jibanRec.HosyousyoNo '��ʍĕ`��p

        ' �敪�A�ۏ؏�NO
        Dim kbn As String = jibanRec.Kbn
        Dim hosyousyoNo As String = jibanRec.HosyousyoNo

        ' ���ʑΉ��f�[�^�擾�p�̃��W�b�N�N���X
        Dim ttMLogic As New TokubetuTaiouMstLogic
        ' ���ʑΉ��f�[�^�i�[�p�̃��X�g
        Dim listRec As New List(Of TokubetuTaiouRecordBase)

        ' �o�R�ޔ�p
        Dim intInitKeiyu As Integer = jibanRec.Keiyu

        Dim intCnt As Integer  '�����J�E���^
        Dim intMax As Integer = 20 '�����ő吔

        ' �X�V���t�擾
        Dim updDateTime As DateTime = DateTime.Now

        Try
            ' �n�Ճe�[�u���Ɠ@�ʐ����̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '*************************
                ' ���ʑΉ��f�[�^�̃}�X�^�擾���f�[�^�o�^(���[�v�̊O�Ŏ��s)
                '*************************
                '�n��T�ւ͓o�^�ς݂Ȃ̂ŁA����̂ݑS���ɑ΂��ăf�t�H���g�o�^����
                If jibanRec.SyoriKensuu = 0 Then
                    If jLogic.insertTokubetuTaiouLogic(sender, _
                                                  kbn, _
                                                  strTmpBangou, _
                                                  jibanRec.KameitenCd, _
                                                  jibanRec.SyouhinCd1, _
                                                  jibanRec.TysHouhou, _
                                                  jibanRec.UpdLoginUserId, _
                                                  updDateTime, _
                                                  jibanRec.RentouBukkenSuu) Then
                    Else
                        '�f�t�H���g�o�^���s
                        mLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "���ʑΉ��f�[�^�̓o�^"), 1)
                        Return False
                    End If
                End If

                '************************************************
                ' ���ʑΉ��f�[�^�̎擾
                '************************************************
                Dim total_count As Integer = 0 '�擾����
                listRec = ttMLogic.GetTokubetuTaiouInfo(sender, _
                                                        kbn, _
                                                        strTmpBangou, _
                                                        jibanRec.KameitenCd, _
                                                        jibanRec.SyouhinCd1, _
                                                        jibanRec.TysHouhou, _
                                                        total_count)
                If total_count < 0 Then
                    Return False
                End If

                '************************************************
                ' ���ʑΉ����i�̎Z�o����
                '************************************************
                Dim intTmpKingakuAction As EarthEnum.emKingakuAction = EarthEnum.emKingakuAction.KINGAKU_NOT_ACTION

                '���ʑΉ����i���f����
                intTmpKingakuAction = cbLogic.pf_ChkTokubetuTaiouKkk(Me, listRec, jibanRec, False)
                If intTmpKingakuAction = EarthEnum.emKingakuAction.KINGAKU_ALERT Then
                    mLogic.AlertMessage(sender, Messages.MSG200W.Replace("@PARAM1", cbLogic.AccTokubetuTaiouKkkMsg), 0, "KkkException")
                End If

                '20��������
                For intCnt = 1 To intMax

                    '*************************
                    ' �A�������Ή�
                    '*************************
                    '�������� >= �A�������� �̏ꍇ�A�S�����I��
                    If jibanRec.SyoriKensuu >= jibanRec.RentouBukkenSuu Then
                        jibanRec.SyoriKensuu = jibanRec.RentouBukkenSuu '�������� = �A��������
                        Exit For
                    End If

                    '�X�V�Ώۂ̔ԍ����w��
                    jibanRec.HosyousyoNo = strTmpBangou '�n�Ճe�[�u��

                    ' �n�Ճf�[�^���敪�A�ۏ؏�NO���擾�i�󃌃R�[�h�m�F�p�j
                    kbn = jibanRec.Kbn
                    hosyousyoNo = jibanRec.HosyousyoNo

                    '����������R�[�h�̎����ݒ�
                    If jibanRec.BukkenNayoseCdFlg Then
                        jibanRec.BukkenNayoseCd = kbn & hosyousyoNo '������NO���Z�b�g
                    End If

                    '*************************
                    ' �r���`�F�b�N����
                    '*************************
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

                    '*************************
                    ' �^�M�`�F�b�N����
                    '*************************
                    Dim jibanRecOld As New JibanRecord
                    jibanRecOld = jLogic.GetJibanData(kbn, hosyousyoNo)

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

                    '*************************
                    ' �X�V�����e�[�u���̓o�^
                    '*************************
                    ' �{�喼
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.SesyuMei, _
                                                   EarthConst.RIREKI_SESYU_MEI, _
                                                   jibanRec.SesyuMei, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' �G���[�����������I��
                        Return False
                    End If

                    ' �����Z��
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.Juusyo, _
                                                   EarthConst.RIREKI_BUKKEN_JYUUSYO, _
                                                   jibanRec.BukkenJyuusyo1 & jibanRec.BukkenJyuusyo2, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' �G���[�����������I��
                        Return False
                    End If

                    ' �����X�R�[�h
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.KameitenCd, _
                                                   EarthConst.RIREKI_BUKKEN_KAMEITEN_CD, _
                                                   jibanRec.KameitenCd, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' �G���[�����������I��
                        Return False
                    End If

                    ' �������
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.TyousaKaisya, _
                                                   EarthConst.RIREKI_TYOUSA_KAISYA, _
                                                   jibanRec.TysKaisyaCd & jibanRec.TysKaisyaJigyousyoCd, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' �G���[�����������I��
                        Return False
                    End If

                    ' ���l
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.Bikou, _
                                                   EarthConst.RIREKI_BIKOU, _
                                                   jibanRec.Bikou, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' �G���[�����������I��
                        Return False
                    End If

                    ' �A�g�p�e�[�u���ɓo�^����i�n�Ձ|�X�V�j
                    renkeiJibanRec.Kbn = kbn
                    renkeiJibanRec.HosyousyoNo = hosyousyoNo
                    renkeiJibanRec.RenkeiSijiCd = 2
                    renkeiJibanRec.SousinJykyCd = 0
                    renkeiJibanRec.UpdLoginUserId = jibanRec.UpdLoginUserId
                    If dataAccess.EditJibanRenkeiData(renkeiJibanRec, True) <> 1 Then
                        ' �o�^�Ɏ��s�����̂ŏ������f
                        Return False
                    End If

                    '*************************
                    ' R-JHS�A�g�`�F�b�N
                    '*************************
                    ' ReportJHS�A�W�Ώۃ`�F�b�N���s��(�o�R�F0,1,5�̏ꍇ�̂�)
                    flgEditReportIf = False '������

                    '�o�R=0or1or5(�擪���������f�ΏۊO(�o�R=9)�Ɣ��肳�ꂽ�ꍇ�A�Ȍ�ȉ��̏����̓X���[)
                    If jibanRec.Keiyu = 0 Or _
                        jibanRec.Keiyu = 1 Or _
                        jibanRec.Keiyu = 5 Then

                        'R-JHS�A�g�`�F�b�N
                        If jLogic.ChkRJhsRenkei(jibanRec.TysKaisyaCd, jibanRec.TysKaisyaJigyousyoCd) Then
                            ' �Ώۂ̏ꍇ�A�t���O�𗧂Ă�
                            flgEditReportIf = True
                            ' ���f�ΏۂȂ̂Ōo�R��5�ɕύX
                            jibanRec.Keiyu = 5
                        Else
                            ' �A�g������Аݒ�}�X�^�̑��݃`�F�b�N��NG�ɂȂ����ꍇ�A�o�R��9�ɕύX
                            jibanRec.Keiyu = 9
                        End If
                    End If

                    '�A�������̏ꍇ
                    If jibanRec.RentouBukkenSuu > 1 Then
                        'R-JHS�A�g�Ώۂ̏ꍇ
                        If flgEditReportIf Then
                            If intInitKeiyu = 0 AndAlso jibanRec.SyoriKensuu > 0 Then '���.�o�R=0�ł��A2���ڈȍ~�̏ꍇ
                                '�A�����̘A�g�Ōo�R=0�̏ꍇ�A1���ڂ̂ݘA�g
                                '1���ځF�A�g���A�o�R��5or9�ɂȂ�
                                '2���ڈȍ~�F�A�g�����A�o�R��0�̂܂�
                                jibanRec.Keiyu = intInitKeiyu
                                flgEditReportIf = False
                            End If
                        End If
                    End If

                    '************************************************
                    ' �ۏ؏����s�󋵁A�ۏ؏����s�󋵐ݒ���A�ۏ؏��i�L���̎����ݒ�
                    '************************************************
                    '���i�̎����ݒ��ɍs�Ȃ�
                    cbLogic.ps_AutoSetHosyousyoHakJyjy(EarthEnum.emGamenInfo.MousikomiInput, jibanRec)

                    '���������f�[�^�̎����Z�b�g
                    Dim brRec As BukkenRirekiRecord = cbLogic.MakeBukkenRirekiRecHosyouUmu(jibanRec, cbLogic.GetDisplayString(jibanRecOld.HosyouSyouhinUmu), cbLogic.GetDisplayString(jibanRecOld.KeikakusyoSakuseiDate))

                    If Not brRec Is Nothing Then
                        '�����������R�[�h�̃`�F�b�N
                        Dim strErrMsg As String = String.Empty
                        If cbLogic.checkInputBukkenRireki(brRec, strErrMsg) = False Then
                            mLogic.AlertMessage(sender, strErrMsg, 0, "ErrBukkenRireki")
                            Exit Function
                        End If
                    End If

                    '*************************
                    ' �n�Ճe�[�u���̍X�V
                    '*************************                  
                    ' �X�V�pUPDATE����������
                    updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordMousikomiInput), jibanRec, list)

                    ' �n�Ճe�[�u���̍X�V���s���i�V�K�͍̔Ԏ��A�폜�͕ʋ@�\�Ȃ̂Œn�Ֆ{�͍̂X�V�̂݁j
                    If dataAccess.UpdateJibanData(updateString, list) = True Then

                        '**************************************************************************
                        ' �@�ʐ����f�[�^�̒ǉ�
                        '**************************************************************************
                        '�@�ʐ����e�[�u���f�[�^����p
                        Dim TeibetuSyuuseiLogic As New TeibetuSyuuseiLogic
                        Dim tempTeibetuRec As New TeibetuSeikyuuRecord

                        '***************************
                        ' ���i�R�[�h�P�̒ǉ�
                        '***************************
                        '���i�P���R�[�h���e���|�����ɃZ�b�g
                        tempTeibetuRec = jibanRec.Syouhin1Record

                        If tempTeibetuRec IsNot Nothing Then
                            '�A����������2�ȏ�̏ꍇ�A��L�R�s�[��`�F�b�N�Ώۂ̔ԍ����J�E���g�A�b�v
                            If jibanRec.RentouBukkenSuu > 1 Then
                                tempTeibetuRec.HosyousyoNo = strTmpBangou
                            End If
                        End If

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
                        ' ���i�R�[�h�Q�̒ǉ�
                        '***************************
                        Dim i As Integer
                        For i = 1 To EarthConst.SYOUHIN2_COUNT
                            If jibanRec.Syouhin2Records.ContainsKey(i) = True Then

                                '���i�Q���R�[�h���e���|�����ɃZ�b�g
                                tempTeibetuRec = jibanRec.Syouhin2Records.Item(i)

                                '�A����������2�ȏ�̏ꍇ�A��L�R�s�[��`�F�b�N�Ώۂ̔ԍ����J�E���g�A�b�v
                                If jibanRec.RentouBukkenSuu > 1 Then
                                    tempTeibetuRec.HosyousyoNo = strTmpBangou
                                End If

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
                            End If
                        Next

                        '***************************
                        ' ���i�R�[�h�R�̒ǉ�
                        '***************************
                        For i = 1 To EarthConst.SYOUHIN3_COUNT
                            If Not jibanRec.Syouhin3Records Is Nothing AndAlso jibanRec.Syouhin3Records.ContainsKey(i) = True Then

                                '���i�R���R�[�h���e���|�����ɃZ�b�g
                                tempTeibetuRec = jibanRec.Syouhin3Records.Item(i)

                                '�A����������2�ȏ�̏ꍇ�A��L�R�s�[��`�F�b�N�Ώۂ̔ԍ����J�E���g�A�b�v
                                If jibanRec.RentouBukkenSuu > 1 Then
                                    tempTeibetuRec.HosyousyoNo = strTmpBangou
                                End If

                                ' ���i�R�̓@�ʐ����f�[�^���X�V���܂�
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

                        ' �����������e�[�u���ǉ�(�ۏؗL���ύX���̂݁A��������T�ɐV�K�ǉ�����)
                        If Not brRec Is Nothing Then
                            Dim brLogic As New BukkenRirekiLogic

                            '�V�K�ǉ��p�X�N���v�g����ю��s
                            brRec.Kbn = jibanRec.Kbn
                            brRec.HosyousyoNo = jibanRec.HosyousyoNo
                            If brLogic.InsertBukkenRireki(brRec) = False Then
                                Return False
                            End If
                        End If

                        ' �����������e�[�u���ǉ�(��ʏ������ɁA��������T�ɐV�K�ǉ�����)
                        If Not listBr Is Nothing AndAlso listBr.Count > 0 Then
                            Dim brLogic As New BukkenRirekiLogic
                            Dim brRecUI = New BukkenRirekiRecord
                            Dim intBrCnt As Integer = 0

                            For intBrCnt = 0 To listBr.Count - 1
                                brRecUI = listBr(intBrCnt)

                                '�V�K�ǉ��p�X�N���v�g����ю��s
                                brRecUI.Kbn = jibanRec.Kbn
                                brRecUI.HosyousyoNo = jibanRec.HosyousyoNo
                                If brLogic.InsertBukkenRireki(brRecUI) = False Then
                                    mLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "���������f�[�^�̓o�^"), 1)
                                    Return False
                                End If
                            Next

                        End If

                        '***************************
                        ' ���ʑΉ��e�[�u���̍X�V
                        '***************************
                        If Not listRec Is Nothing AndAlso listRec.Count > 0 Then
                            '�A���p�̕ۏ؏�No��A�Ԃłӂ�Ȃ���
                            For intTokuCnt As Integer = 0 To listRec.Count - 1
                                listRec(intTokuCnt).HosyousyoNo = strTmpBangou
                                listRec(intTokuCnt).UpdFlg = 1
                            Next

                            '���ʑΉ��f�[�^�X�V
                            If cbLogic.pf_setKey_TokubetuTaiou_TeibetuSeikyuu(sender, listRec, jibanRec.UpdLoginUserId, EarthEnum.emGamenInfo.MousikomiInput) = False Then
                                mLogic.AlertMessage(sender, Messages.MSG147E.Replace("@PARAM1", "���ʑΉ��f�[�^�̍X�V"), 1)
                                Return False
                            End If
                        End If

                        '��Ƀ`�F�b�N���Ă�����ReportJHS�A�W�Ώۃ`�F�b�N�����ɁA�i���f�[�^�e�[�u���X�V�������s��
                        '��ReportJHS�A�g�ɂē��ʑΉ��R�[�h���A�g�Ώۂ̂��߁A�Ō�ɏ�������
                        If flgEditReportIf Then
                            ' �i���f�[�^�e�[�u���X�V�����ďo
                            If jLogic.EditReportIfData(jibanRec) = False Then
                                ' �G���[�����������I��
                                Return False
                            End If
                        End If

                        '�ԍ����J�E���g�A�b�v
                        intTmpBangou = CInt(strTmpBangou) + 1 '�J�E���^
                        strTmpBangou = Format(intTmpBangou, "0000000000") '�t�H�[�}�b�g

                        jibanRec.SyoriKensuu += 1 '��������

                    Else
                        Return False
                    End If

                Next

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

#Region "������Ж��w�莞�̎����ݒ�"

    ''' <summary>
    ''' �w�蒲����ЁA�D�撲����Ђ̑��݃`�F�b�N
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strTysGaisyaCd">������ЃR�[�h(������ЃR�[�h�{���Ə��R�[�h)</param>
    ''' <returns>True or False</returns>
    ''' <remarks>�Y���̒�����ЃR�[�h�������X������Аݒ�}�X�^.�ɑ��݂��邩�𔻒肷��</remarks>
    Public Function ChkExistSiteiTysGaisya( _
                                     ByVal strKameitenCd As String _
                                     , ByRef strTysGaisyaCd As String _
                                     ) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkExistSiteiTysGaisya", _
                                            strKameitenCd _
                                            , strTysGaisyaCd _
                                            )

        Dim blnResult As Boolean = False
        Dim dataAccess As New TyousakaisyaSearchDataAccess

        '������Аݒ�M���w�蒲����Ђ̑��݃`�F�b�N���s�Ȃ�
        blnResult = dataAccess.GetSiteiTyousakaisyaCd(strKameitenCd, strTysGaisyaCd)

        Return blnResult
    End Function

#End Region

End Class
