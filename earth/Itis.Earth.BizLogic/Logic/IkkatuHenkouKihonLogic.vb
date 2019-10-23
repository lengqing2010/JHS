Imports System.Transactions
Imports System.Web.UI

''' <summary>
''' ���ǍH����ʗp�̃��W�b�N�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class IkkatuHenkouKihonLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'Utilities��MessegeLogic�N���X
    Dim mLogic As New MessageLogic
    Dim strlogic As New StringLogic
    Dim jibanLogic As New JibanLogic

    '�X�V�����ێ��p
    Dim dateUpdDateTime As DateTime

#Region "�f�[�^�X�V"
    ''' <summary>
    ''' �n�Ճe�[�u���E�@�ʐ����e�[�u�����X�V���܂�
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="listJibanRec">�n�Ճ��R�[�h�̃��X�g</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal listJibanRec As List(Of JibanRecordIkkatuHenkouKihon)) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SaveJibanData", _
                                                    sender, _
                                                    listJibanRec)

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

        ' �n�Ճf�[�^�p�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' �A�g�e�[�u���f�[�^�o�^�p�f�[�^�̊i�[�p
        Dim renkeiJibanRec As New JibanRenkeiRecord
        Dim renkeiTeibetuList As New List(Of TeibetuSeikyuuRenkeiRecord)

        ' ReportJHS�A�W�Ώۃt���O(�f�t�H���g�FFalse)
        Dim flgEditReportIf As Boolean

        Dim intDBCnt As Integer = 0 '�X�V����
        Dim jibanRec As New JibanRecordIkkatuHenkouKihon 'TMP�p

        Try
            ' �n�Ճe�[�u���Ɠ@�ʐ����̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '�����������[�v
                For intDBCnt = 0 To listJibanRec.Count - 1

                    '�Ώےn�Ճ��R�[�h����Ɨp�̃��R�[�h�N���X�Ɋi�[����
                    jibanRec = New JibanRecordIkkatuHenkouKihon
                    jibanRec = listJibanRec(intDBCnt)

                    ' �r���`�F�b�N�p���R�[�h�쐬
                    Dim jibanHaitaRec As New JibanHaitaRecord
                    ' �n�Ճ��R�[�h�̓��ꍀ�ڂ𕡐�
                    RecordMappingHelper.Instance.CopyRecordData(jibanRec, jibanHaitaRec)

                    ' �r���`�F�b�N�pSQL����������
                    sqlString = upadteMake.MakeHaitaSQLInfo(GetType(JibanHaitaRecord), jibanHaitaRec, haitaList)

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
                    '�^�M�`�F�b�N�ɕK�v�ȃf�[�^������DB�f�[�^����R�s�[
                    jibanRec.SyoudakusyoTysDate = jibanRecOld.SyoudakusyoTysDate
                    jibanRec.Syouhin1Record = jibanRecOld.Syouhin1Record
                    jibanRec.Syouhin2Records = jibanRecOld.Syouhin2Records
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

                    ' ���X�V�����e�[�u���̓o�^
                    ' ���X�V����A�g�e�[�u���̍X�V
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
                    updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordIkkatuHenkouKihon), jibanRec, list)
                    ' �X�V�����擾�i�V�X�e�������j
                    dateUpdDateTime = DateTime.Now

                    ' �n�Ճe�[�u���̍X�V���s���i�V�K�͍̔Ԏ��A�폜�͕ʋ@�\�Ȃ̂Œn�Ֆ{�͍̂X�V�̂݁j
                    If dataAccess.UpdateJibanData(updateString, list) = True Then
                        '�@�ʐ���T�̍X�V�����͂Ȃ�
                    End If

                    '��Ƀ`�F�b�N���Ă�����ReportJHS�A�W�Ώۃ`�F�b�N�����ɁA�i���f�[�^�e�[�u���X�V�������s��
                    If flgEditReportIf Then
                        ' �i���f�[�^�e�[�u���X�V�����ďo
                        If jibanLogic.EditReportIfData(jibanRec) = False Then
                            ' �G���[�����������I��
                            Return False
                        End If
                    End If

                Next

                '�����������[�v
                For intDBCnt = 0 To listJibanRec.Count - 1

                    '�Ώےn�Ճ��R�[�h����Ɨp�̃��R�[�h�N���X�Ɋi�[����
                    Dim jibanRecTmp = New JibanRecordBase
                    jibanRecTmp = listJibanRec(intDBCnt)

                    '����������󋵂̍ŏI�`�F�b�N
                    If jibanLogic.ChkLatestBukkenNayoseJyky(sender, jibanRecTmp) = False Then
                        ' �G���[�����������I��
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

End Class
