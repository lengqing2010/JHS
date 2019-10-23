Imports System.Transactions
Imports System.Web.UI
Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' �ۏ؉�ʗp�̃��W�b�N�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class HosyouLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim mLogic As New MessageLogic
    Dim cbLogic As New CommonBizLogic

    '�X�V�����ێ��p
    Dim dateUpdDateTime As DateTime

#Region "�n�Ճf�[�^�o�^"
    ''' <summary>
    ''' �n�Ճe�[�u���E�@�ʐ����e�[�u�����X�V���܂�
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal jibanRec As JibanRecordHosyou, _
                                  ByVal brRec As BukkenRirekiRecord _
                                  ) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SaveJibanData", _
                                                    sender, _
                                                    jibanRec, _
                                                    brRec)

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
        ' �n�Ճ��R�[�h�̓��ꍀ�ڂ𕡐�
        RecordMappingHelper.Instance.CopyRecordData(jibanRec, jibanHaitaRec)

        ' �r���`�F�b�N�pSQL����������
        sqlString = upadteMake.MakeHaitaSQLInfo(GetType(JibanHaitaRecord), jibanHaitaRec, haitaList)

        ' �n�Ճf�[�^�p�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' �A�g�e�[�u���f�[�^�o�^�p�f�[�^�̊i�[�p
        Dim renkeiJibanRec As New JibanRenkeiRecord
        Dim renkeiTeibetuList As New List(Of TeibetuSeikyuuRenkeiRecord)

        Try
            ' �n�Ճe�[�u���Ɠ@�ʐ����̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                Dim jibanLogic As New JibanLogic

                ' ���r���`�F�b�N���{
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

                Dim YosinLogic As New YosinLogic
                Dim intYosinResult As Integer = YosinLogic.YosinCheck(4, jibanRecOld, jibanRec)
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

                ' ���X�V�����e�[�u���̓o�^
                ' ���X�V����A�g�e�[�u���̍X�V
                ' �����X�R�[�h
                If dataAccess.AddKoushinRireki(jibanRec.Kbn, _
                            jibanRec.HosyousyoNo, _
                            JibanDataAccess.enumItemName.KameitenCd, _
                            EarthConst.RIREKI_BUKKEN_KAMEITEN_CD, _
                            jibanRec.KameitenCd, _
                            jibanRec.UpdLoginUserId) = False Then
                    ' �G���[�����������I��
                    Return False
                End If

                ' �{�喼
                If dataAccess.AddKoushinRireki(jibanRec.Kbn, _
                                            jibanRec.HosyousyoNo, _
                                            JibanDataAccess.enumItemName.SesyuMei, _
                                            EarthConst.RIREKI_SESYU_MEI, _
                                            jibanRec.SesyuMei, _
                                            jibanRec.UpdLoginUserId) = False Then
                    ' �G���[�����������I��
                    Return False
                End If

                ' �����Z��
                If dataAccess.AddKoushinRireki(jibanRec.Kbn, _
                                            jibanRec.HosyousyoNo, _
                                            JibanDataAccess.enumItemName.Juusyo, _
                                            EarthConst.RIREKI_BUKKEN_JYUUSYO, _
                                            jibanRec.BukkenJyuusyo1 & jibanRec.BukkenJyuusyo2, _
                                            jibanRec.UpdLoginUserId) = False Then
                    ' �G���[�����������I��
                    Return False
                End If

                ' ���l
                If dataAccess.AddKoushinRireki(jibanRec.Kbn, _
                                            jibanRec.HosyousyoNo, _
                                            JibanDataAccess.enumItemName.Bikou, _
                                            EarthConst.RIREKI_BIKOU, _
                                            jibanRec.Bikou, _
                                            jibanRec.UpdLoginUserId) = False Then
                    ' �G���[�����������I��
                    Return False
                End If

                ' ���l2
                If dataAccess.AddKoushinRireki(jibanRec.Kbn, _
                                            jibanRec.HosyousyoNo, _
                                            JibanDataAccess.enumItemName.Bikou2, _
                                            EarthConst.RIREKI_BIKOU2, _
                                            jibanRec.Bikou2, _
                                            jibanRec.UpdLoginUserId) = False Then
                    ' �G���[�����������I��
                    Return False
                End If

                ' ���n�՘A�g�e�[�u���ɓo�^����i�n�Ձ|�X�V�j
                renkeiJibanRec.Kbn = jibanRec.Kbn
                renkeiJibanRec.HosyousyoNo = jibanRec.HosyousyoNo
                renkeiJibanRec.RenkeiSijiCd = 2
                renkeiJibanRec.SousinJykyCd = 0
                renkeiJibanRec.UpdLoginUserId = jibanRec.UpdLoginUserId
                If dataAccess.EditJibanRenkeiData(renkeiJibanRec, True) <> 1 Then
                    ' �o�^���s���A�����𒆒f����
                    Return False
                End If

                ' �X�V�pUPDATE����������
                updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordHosyou), jibanRec, list)
                ' �X�V�����擾�i�V�X�e�������j
                dateUpdDateTime = DateTime.Now

                ' �n�Ճe�[�u���̍X�V���s���i�V�K�͍̔Ԏ��A�폜�͕ʋ@�\�Ȃ̂Œn�Ֆ{�͍̂X�V�̂݁j
                If dataAccess.UpdateJibanData(updateString, list) = True Then

                    '**************************************************************************
                    ' �@�ʐ����f�[�^�̒ǉ��E�X�V
                    '**************************************************************************
                    '�@�ʐ������W�b�N
                    Dim tLogic As New TeibetuSyuuseiLogic

                    '�ۏ؏��Ĕ��s
                    If tLogic.EditTeibetuRecord(sender, _
                                                jibanRec.HosyousyoRecord, _
                                                EarthConst.SOUKO_CD_HOSYOUSYO, _
                                                1, _
                                                jibanRec, _
                                                renkeiTeibetuList, _
                                                GetType(TeibetuSeikyuuRecordHosyousyoSaihakkou) _
                                                ) = False Then
                        Return False
                    End If

                    '��񕥖�
                    If tLogic.EditTeibetuRecord(sender, _
                                                jibanRec.KaiyakuHaraimodosiRecord, _
                                                EarthConst.SOUKO_CD_KAIYAKU_HARAIMODOSI, _
                                                1, _
                                                jibanRec, _
                                                renkeiTeibetuList, _
                                                GetType(TeibetuSeikyuuRecordKaiyakuHaraimodosi) _
                                                ) = False Then
                        Return False
                    End If

                    ' �@�ʐ����A�g���f�Ώۂ����݂���ꍇ�A���f���s��
                    For Each renkeiTeibetuRec As TeibetuSeikyuuRenkeiRecord In renkeiTeibetuList
                        ' �A�g�p�e�[�u���ɔ��f����i�@�ʐ����j
                        If dataAccess.EditTeibetuRenkeiData(renkeiTeibetuRec) <> 1 Then
                            ' �o�^�Ɏ��s�����̂ŏ������f
                            Return False
                        End If
                    Next

                End If

                ' ���i���e�[�u���ǉ��X�V(ReportIF)
                ' �X�V�ɐ��������ꍇ�A�i���e�[�u���ւ̔��f�y�јA�g�f�[�^�̍쐬���s��
                ' �i���e�[�u���̍X�V(�n��F0,1�̏ꍇ�̂�)
                If jibanRec.Keiyu = 0 Or _
                   jibanRec.Keiyu = 1 Or _
                   jibanRec.Keiyu = 5 Then

                    ' �i���f�[�^�����n�f�[�^�A�N�Z�X�N���X
                    Dim reportAccess As New ReportIfDataAccess

                    ' �A�g������Аݒ�}�X�^�̑��݃`�F�b�N
                    If reportAccess.ChkRenkeiTyousaKaisya(jibanRec.TysKaisyaCd, _
                                                           jibanRec.TysKaisyaJigyousyoCd) = True Then
                        ' ���݂���ꍇ�A�A�g�e�[�u���֔��f���o�R��5�ɕύX
                        If jibanLogic.EditReportIfData(jibanRec) = False Then
                            ' �G���[�����������I��
                            Return False
                        End If

                        ' ���f�����̂Ōo�R��5�ɕύX
                        '�i�f�[�^���e������̏ꍇ�͍X�V����Ȃ����A�����I�ɂ͑��M�ρj
                        jibanRec.Keiyu = 5
                    Else
                        ' �A�g������Аݒ�}�X�^�̑��݃`�F�b�N��NG�ɂȂ����ꍇ�A�o�R��9�ɕύX
                        jibanRec.Keiyu = 9
                    End If
                End If


                ' ���n�Ճe�[�u���X�V�i�o�R�̂݁F�i��T�ւ̍X�V�������j
                If dataAccess.UpdateJibanKeiyu(jibanRec.Kbn, _
                                                               jibanRec.HosyousyoNo, _
                                                               jibanRec.UpdLoginUserId, _
                                                               jibanRec.Keiyu) = False Then
                    ' �G���[�����������I��
                    Return False
                End If

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

#Region "�n�Ճf�[�^.���s�˗��L�����Z�������X�V"
    ''' <summary>
    ''' �n�Ճf�[�^.���s�˗��L�����Z�������݂̂��X�V���܂��B�֘A����@�ʐ����f�[�^�̍X�V�͂��܂���
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateJibanIraiCancel(ByVal sender As Object, _
                                          ByVal jibanRec As JibanRecordHosyou _
                                          ) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibanIraiCancel", _
                                                    sender, _
                                                    jibanRec _
                                                    )

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
        ' �n�Ճ��R�[�h�̓��ꍀ�ڂ𕡐�
        RecordMappingHelper.Instance.CopyRecordData(jibanRec, jibanHaitaRec)

        ' �r���`�F�b�N�pSQL����������
        sqlString = upadteMake.MakeHaitaSQLInfo(GetType(JibanHaitaRecord), jibanHaitaRec, haitaList)

        ' �n�Ճf�[�^�p�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' �A�g�e�[�u���f�[�^�o�^�p�f�[�^�̊i�[�p
        Dim renkeiJibanRec As New JibanRenkeiRecord
        Dim renkeiTeibetuList As New List(Of TeibetuSeikyuuRenkeiRecord)

        Try
            ' �n�Ճe�[�u���Ɠ@�ʐ����̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                Dim jibanLogic As New JibanLogic

                ' ���r���`�F�b�N���{
                Dim haitaKekkaList As List(Of JibanHaitaRecord) = _
                            DataMappingHelper.Instance.getMapArray(Of JibanHaitaRecord)(GetType(JibanHaitaRecord), _
                            dataAccess.CheckHaita(sqlString, haitaList))

                If haitaKekkaList.Count > 0 Then
                    Dim haitaErrorData As JibanHaitaRecord = haitaKekkaList(0)

                    ' �r���`�F�b�N�G���[
                    mLogic.CallHaitaErrorMessage(sender, haitaErrorData.UpdLoginUserId, haitaErrorData.UpdDatetime, "�n�Ճe�[�u��")
                    Return False
                End If

                ' �^�M�`�F�b�N�����F���܂���
                ' ���X�V�����e�[�u���̓o�^�F���܂���
                ' ���X�V����A�g�e�[�u���̍X�V�F���܂���

                ' ���n�՘A�g�e�[�u���ɓo�^����i�n�Ձ|�X�V�j
                renkeiJibanRec.Kbn = jibanRec.Kbn
                renkeiJibanRec.HosyousyoNo = jibanRec.HosyousyoNo
                renkeiJibanRec.RenkeiSijiCd = 2
                renkeiJibanRec.SousinJykyCd = 0
                renkeiJibanRec.UpdLoginUserId = jibanRec.UpdLoginUserId
                If dataAccess.EditJibanRenkeiData(renkeiJibanRec, True) <> 1 Then
                    ' �o�^���s���A�����𒆒f����
                    Return False
                End If

                ' �X�V�pUPDATE����������
                updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordHosyou), jibanRec, list)
                ' �X�V�����擾�i�V�X�e�������j
                dateUpdDateTime = DateTime.Now

                ' �n�Ճe�[�u���̍X�V���s���i�V�K�͍̔Ԏ��A�폜�͕ʋ@�\�Ȃ̂Œn�Ֆ{�͍̂X�V�̂݁j
                If dataAccess.UpdateJibanIraiCancel(jibanRec.Kbn, _
                                                    jibanRec.HosyousyoNo, _
                                                    jibanRec.UpdLoginUserId, _
                                                    jibanRec.HakIraiCanDatetime, _
                                                    jibanRec.UpdDatetime) = True Then

                    '**************************************************************************
                    ' �@�ʐ����f�[�^�̒ǉ��E�X�V�F���܂���
                    '**************************************************************************
                End If

                ' ���i���e�[�u���ǉ��X�V(ReportIF)
                ' �X�V�ɐ��������ꍇ�A�i���e�[�u���ւ̔��f�y�јA�g�f�[�^�̍쐬���s��
                ' �i���e�[�u���̍X�V(�n��F0,1�̏ꍇ�̂�)
                If jibanRec.Keiyu = 0 Or _
                   jibanRec.Keiyu = 1 Or _
                   jibanRec.Keiyu = 5 Then

                    ' �i���f�[�^�����n�f�[�^�A�N�Z�X�N���X
                    Dim reportAccess As New ReportIfDataAccess

                    ' �A�g������Аݒ�}�X�^�̑��݃`�F�b�N
                    If reportAccess.ChkRenkeiTyousaKaisya(jibanRec.TysKaisyaCd, _
                                                           jibanRec.TysKaisyaJigyousyoCd) = True Then
                        ' ���݂���ꍇ�A�A�g�e�[�u���֔��f���o�R��5�ɕύX
                        If jibanLogic.EditReportIfData(jibanRec) = False Then
                            ' �G���[�����������I��
                            Return False
                        End If

                        ' ���f�����̂Ōo�R��5�ɕύX
                        '�i�f�[�^���e������̏ꍇ�͍X�V����Ȃ����A�����I�ɂ͑��M�ρj
                        jibanRec.Keiyu = 5
                    Else
                        ' �A�g������Аݒ�}�X�^�̑��݃`�F�b�N��NG�ɂȂ����ꍇ�A�o�R��9�ɕύX
                        jibanRec.Keiyu = 9
                    End If
                End If


                ' ���n�Ճe�[�u���X�V�i�o�R�̂݁F�i��T�ւ̍X�V�������j
                If dataAccess.UpdateJibanKeiyu(jibanRec.Kbn, _
                                               jibanRec.HosyousyoNo, _
                                               jibanRec.UpdLoginUserId, _
                                               jibanRec.Keiyu) = False Then
                    ' �G���[�����������I��
                    Return False
                End If

                ' �����������e�[�u���ǉ�(�ۏؗL���ύX���̂݁A��������T�ɐV�K�ǉ�����)�F���܂���

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


#Region "���t�}�X�^.�ۏ؏����s���擾"

    Public Function GetHosyousyoHakkouDate(ByVal strKbn As String) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyousyoHakkouDate", _
                                            strKbn _
                                            )

        Dim dataAccess As New HidukeSaveDataAccess
        Dim strReturn As String = ""

        strReturn = dataAccess.GetHosyousyoHakkouDate(strKbn)

        Return strReturn
    End Function
#End Region

#Region "���i���̎擾"

    ''' <summary>
    ''' ��񕥖߉��i���擾���܂�
    ''' </summary>
    ''' <param name="kameitenCd">�����X�R�[�h</param>
    ''' <param name="kbn">�敪</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKaiyakuKakaku(ByVal kameitenCd As String, _
                                     ByVal kbn As String) As Integer

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKaiyakuKakaku", _
                                                    kameitenCd, _
                                                    kbn)

        Dim dataAccess As New KameitenSearchDataAccess

        Return dataAccess.GetKaiyakuKakaku(kameitenCd, kbn)

    End Function

#End Region

    ''' <summary>
    ''' �����X�}�X�^.�����m�F����NO���擾���܂�
    ''' </summary>
    ''' <param name="kameitenCd">�����X�R�[�h</param>
    ''' <param name="kbn">�敪</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNyuukinKakuninJoukenNo(ByVal kameitenCd As String, _
                                     ByVal kbn As String) As Integer

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetNyuukinKakuninJoukenNo", _
                                                    kameitenCd, _
                                                    kbn)

        Dim dataAccess As New KameitenSearchDataAccess

        Return dataAccess.GetNyuukinKakuninJoukenNo(kameitenCd, kbn)

    End Function

    ''' <summary>
    ''' �����X���l�ݒ�}�X�^.���l���(=42)�̃��R�[�h�̑��݃`�F�b�N�����܂�
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExistBikouSyubetu(ByVal strKameitenCd As String) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ExistBikouSyubetu", _
                                                        strKameitenCd _
                                                    )

        Dim dataAccess As New KameitenBikouSetteiDataAccess

        Dim intRet As Integer = dataAccess.ChkBikouSyubetu(strKameitenCd, 42)
        If intRet > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

End Class
