Imports System.Transactions
Imports System.Web.UI

''' <summary>
''' �񍐏���ʗp�̃��W�b�N�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class HoukokusyoLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'Utilities��MessegeLogic�N���X
    Dim mLogic As New MessageLogic

    '�X�V�����ێ��p
    Dim dateUpdDateTime As DateTime

    Dim cbLogic As New CommonBizLogic

#Region "�f�[�^�X�V"
    ''' <summary>
    ''' �n�Ճe�[�u���E�@�ʐ����e�[�u�����X�V���܂�
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal jibanRec As JibanRecordHoukokusyo, _
                                  ByVal brRec As BukkenRirekiRecord, _
                                  ByVal brRecHantei As BukkenRirekiRecord) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SaveJibanData", _
                                                    sender, _
                                                    jibanRec)

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
                Dim intYosinResult As Integer = YosinLogic.YosinCheck(2, jibanRecOld, jibanRec)
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

                ' �X�V�pUPDATE����������
                updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordHoukokusyo), jibanRec, list)
                ' �X�V�����擾�i�V�X�e�������j
                dateUpdDateTime = DateTime.Now

                ' �n�Ճe�[�u���̍X�V���s���i�V�K�͍̔Ԏ��A�폜�͕ʋ@�\�Ȃ̂Œn�Ֆ{�͍̂X�V�̂݁j
                If dataAccess.UpdateJibanData(updateString, list) = True Then

                    '**************************************************************************
                    ' �@�ʐ����f�[�^�̒ǉ��E�X�V
                    '**************************************************************************
                    '�@�ʐ������W�b�N
                    Dim tLogic As New TeibetuSyuuseiLogic

                    '�񍐏�
                    If tLogic.EditTeibetuRecord(sender, _
                                                jibanRec.TyousaHoukokusyoRecord, _
                                                EarthConst.SOUKO_CD_TYOUSA_HOUKOKUSYO, _
                                                1, _
                                                jibanRec, _
                                                renkeiTeibetuList, _
                                                GetType(TeibetuSeikyuuRecordTysHoukokusyo) _
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

                ' �����������e�[�u���ǉ�(����ύX���̂݁A��������T�ɐV�K�ǉ�����)
                If Not brRecHantei Is Nothing Then
                    Dim brLogic As New BukkenRirekiLogic

                    '�V�K�ǉ��p�X�N���v�g����ю��s
                    If brLogic.InsertBukkenRireki(brRecHantei) = False Then
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

#Region "���t�}�X�^.�񍐏�������"
    ''' <summary>
    ''' ���t�}�X�^�̕񍐏����������Z�o���܂�
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strCmpDate">��r�Ώۂ̓��t</param>
    ''' <returns>�񍐏�������</returns>
    ''' <remarks>���t�}�X�^�̕񍐏���������莟����ߓ����Z�o���ԋp���܂�<br/>
    ''' <example>
    ''' ����:2009/02/15 ���ߓ�:20 �̏ꍇ    �� 2009/02/20 <br/>
    ''' ����:2009/02/25 ���ߓ�:20 �̏ꍇ    �� 2009/03/20 <br/>
    ''' ����:2009/02/25 ���ߓ�:�s���l�̏ꍇ �� 2009/02/28 (������)<br/>
    ''' </example>
    ''' </remarks>
    Public Function GetHoukokusyoHassoudate( _
                                            ByVal strKbn As String _
                                            , ByVal strCmpDate As String _
                                            ) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHoukokusyoHassoudate", _
                                            strKbn, _
                                            strCmpDate)

        ' �߂�l
        Dim editDate As Date
        Dim strRet As String = String.Empty

        ' �񍐏����������擾
        Dim dataAccess As New HidukeSaveDataAccess
        Dim datSstring As String = dataAccess.GetHoukokusyoHassouDate(strKbn)
        '�擾�ł��������ꍇ�͏����𔲂���
        If datSstring = String.Empty Then
            Return strRet
        End If

        ' ������
        Dim tougetuDate As Date = Date.Parse(Date.Now.Year.ToString & "/" & _
                                   Date.Now.Month.ToString("00") & "/" & _
                                   "01")

        Try
            ' ���t�^�ɕϊ�����
            editDate = Date.Parse(datSstring)
        Catch ex As Exception
            ' �s���l�̏ꍇ�A����������ߓ��Ƃ���
            editDate = tougetuDate.AddMonths(1).AddDays(-1)
        End Try

        Dim dtCmp As Date

        If strCmpDate = String.Empty Then
            dtCmp = Today
        Else
            dtCmp = Date.Parse(strCmpDate)
        End If

        '�������񍐏��󗝓�����L�̓��t�}�X�^.�񍐏��������̏ꍇ�́A���t�}�X�^.�񍐏��������{1������ҏW����
        If dtCmp > editDate Then
            Try
                datSstring = editDate.AddMonths(1).Year.ToString & "/" & _
                                                          editDate.AddMonths(1).Month.ToString("00") & "/" & _
                                                          editDate.Day.ToString("00")

                ' ���t�^�ɕϊ�����
                editDate = Date.Parse(datSstring)
            Catch ex As Exception
                ' �s���l�̏ꍇ�A����������ߓ��Ƃ���
                editDate = tougetuDate.AddMonths(2).AddDays(-1)
            End Try

        End If

        strRet = Format(editDate, EarthConst.FORMAT_DATE_TIME_9)

        Return strRet

    End Function
#End Region

#Region "�H�����茋��FLG���f"

    ''' <summary>
    ''' ����֘A���擾���A�H�����茋��FLG��Ԃ�
    ''' </summary>
    ''' <param name="strHantei1Cd">����R�[�h1</param>
    ''' <param name="strHantei2Cd">����R�[�h2</param>
    ''' <param name="strSetuzokusi">����ڑ���</param>
    ''' <returns>�H�����茋��FLG(Integer:0 or 1)</returns>
    ''' <remarks></remarks>
    Public Function GetKojHanteiKekkaFlg(ByVal strHantei1Cd As String, ByVal strHantei2Cd As String, ByVal strSetuzokusi As String) As Integer
        Dim intRet As Integer = Integer.MinValue '�߂�l

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKojHanteiKekkaFlg", _
                                                    strHantei1Cd, _
                                                    strHantei2Cd, _
                                                    strSetuzokusi)
        Dim ksLogic As New KisoSiyouLogic
        Dim intKojHanteiFlg1 As Integer = Integer.MinValue
        Dim intKojHanteiFlg2 As Integer = Integer.MinValue

        Dim rec1 As KisoSiyouRecord = Nothing '����1
        Dim rec2 As KisoSiyouRecord = Nothing '����2

        '����1
        If strHantei1Cd <> String.Empty Then
            rec1 = ksLogic.GetKisoSiyouRec(CInt(strHantei1Cd))
            If Not rec1 Is Nothing Then
                intKojHanteiFlg1 = rec1.KojHanteiFlg
            End If
        End If
        '����2
        If strHantei2Cd <> String.Empty Then
            rec2 = ksLogic.GetKisoSiyouRec(CInt(strHantei2Cd))
            If Not rec2 Is Nothing Then
                intKojHanteiFlg2 = rec2.KojHanteiFlg
            End If
        End If

        '�u�ڑ�������R�[�h=1��3��Null ���� (����1�̍H������FLG=1������2�̍H������FLG=1)���ڑ�������R�[�h=2 ���� ����1�̍H������FLG=1 ���� (����2�̍H������FLG=1��Null���j�̂Ƃ�1�A�ȊO0�v
        ' ���i����2�̍H������FLG=1��NULL�j�F����2=NULL���܂�
        If ((strSetuzokusi = "1" Or strSetuzokusi = "3" Or strSetuzokusi = String.Empty) _
            And (intKojHanteiFlg1 = 1 Or intKojHanteiFlg2 = 1)) _
            Or (strSetuzokusi = "2" And intKojHanteiFlg1 = 1 And (intKojHanteiFlg2 = 1 Or intKojHanteiFlg2 = Integer.MinValue Or strHantei2Cd = String.Empty)) Then
            intRet = 1
        Else
            intRet = 0
        End If

        Return intRet
    End Function
#End Region
End Class
