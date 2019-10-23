Imports System.Transactions
Imports System.Web.UI

''' <summary>
''' ���ǍH����ʗp�̃��W�b�N�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class KairyouKoujiLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'Utilities��MessegeLogic�N���X
    Dim mLogic As New MessageLogic
    Dim cbLogic As New CommonBizLogic

    '�H�����i�f�[�^�A�N�Z�X�N���X
    Dim kojDataAcc As New KoujiKakakuDataAccess
    '���i�f�[�^�A�N�Z�X�N���X
    Dim SyouhinDataAcc As New SyouhinDataAccess

    '�X�V�����ێ��p
    Dim dateUpdDateTime As DateTime

    Private Const pStrKojGaisyaAll As String = "ALLAL"
    Private Const pStrTyokuKouji As String = "1"
    Private Const pStrJioTyokuKouji As String = "1J"

#Region "�f�[�^�X�V"
    ''' <summary>
    ''' �n�Ճe�[�u���E�@�ʐ����e�[�u�����X�V���܂�
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal jibanRec As JibanRecordKairyouKouji, _
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
                Dim intYosinResult As Integer = YosinLogic.YosinCheck(3, jibanRecOld, jibanRec)
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
                updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordKairyouKouji), jibanRec, list)
                ' �X�V�����擾�i�V�X�e�������j
                dateUpdDateTime = DateTime.Now

                ' �n�Ճe�[�u���̍X�V���s���i�V�K�͍̔Ԏ��A�폜�͕ʋ@�\�Ȃ̂Œn�Ֆ{�͍̂X�V�̂݁j
                If dataAccess.UpdateJibanData(updateString, list) = True Then
                    '**************************************************************************
                    ' �@�ʐ����f�[�^�̒ǉ��E�X�V
                    '**************************************************************************
                    '�@�ʐ������W�b�N
                    Dim tLogic As New TeibetuSyuuseiLogic

                    '���ǍH��
                    If tLogic.EditTeibetuRecord(sender, _
                                                jibanRec.KairyouKoujiRecord, _
                                                EarthConst.SOUKO_CD_KAIRYOU_KOUJI, _
                                                1, _
                                                jibanRec, _
                                                renkeiTeibetuList, _
                                                GetType(TeibetuSeikyuuRecordKairyouKouji) _
                                                ) = False Then
                        Return False
                    End If

                    '�ǉ��H��
                    If tLogic.EditTeibetuRecord(sender, _
                                                jibanRec.TuikaKoujiRecord, _
                                                EarthConst.SOUKO_CD_TUIKA_KOUJI, _
                                                1, _
                                                jibanRec, _
                                                renkeiTeibetuList, _
                                                GetType(TeibetuSeikyuuRecordTuikaKouji) _
                                                ) = False Then
                        Return False
                    End If

                    '�H���񍐏��Ĕ��s
                    If tLogic.EditTeibetuRecord(sender, _
                                                jibanRec.KoujiHoukokusyoRecord, _
                                                EarthConst.SOUKO_CD_KOUJI_HOUKOKUSYO, _
                                                1, _
                                                jibanRec, _
                                                renkeiTeibetuList, _
                                                GetType(TeibetuSeikyuuRecordKoujiHoukokusyo) _
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

#Region "�ǉ��H���f�[�^�����ݒ�`�F�b�N"

    ''' <summary>
    ''' �ǉ��H���f�[�^�����ݒ�`�F�b�N���s�Ȃ��B
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBangou">�ԍ�[���ۏ؏�NO]</param>
    ''' <returns>[Boolean]True or False</returns>
    ''' <remarks>
    ''' �@�ʐ����e�[�u�����A�ȉ��̏����ƈ�v����f�[�^�𒊏o����B<br/>
    ''' �����F�@�ʐ����e�[�u��.�敪�����.�敪<br/>
    ''' �@�ʐ����e�[�u��.�ۏ؏�NO�����.�ۏ؏�NO<br/>
    ''' �@�ʐ����e�[�u��.���޺��ށ�"140"<br/>
    ''' <br/>
    ''' return False:�f�[�^�����݂���ꍇ�A�ǉ��H���f�[�^�����ݒ�ΏۊO�Ƃ���B<br/>
    ''' return True:�f�[�^�����݂��Ȃ��ꍇ�A�ǉ��H���f�[�^�����ݒ�ΏۂƂ���B<br/>
    ''' </remarks>
    Public Function ChkTjDataAutoSetting(ByVal strKbn As String, ByVal strBangou As String) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkTjDataAutoSetting", _
                                                    strKbn, _
                                                    strBangou)

        ' �n�Ճf�[�^�p�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        Dim teibetuSeikyuuList As List(Of TeibetuSeikyuuRecord) = _
                    DataMappingHelper.Instance.getMapArray(Of TeibetuSeikyuuRecord)(GetType(TeibetuSeikyuuRecord), _
                    dataAccess.GetTeibetuSeikyuuData(strKbn, strBangou, EarthConst.SOUKO_CD_TUIKA_KOUJI))


        ' �擾���R�[�h���ݒ���s��
        For Each rec As TeibetuSeikyuuRecord In teibetuSeikyuuList

            ' ���ނɂ��i�[�ꏊ��ύX
            Select Case rec.BunruiCd
                Case EarthConst.SOUKO_CD_TUIKA_KOUJI
                    Return False

                Case Else

            End Select
        Next

        Return True
    End Function
#End Region

#Region "���ǍH����ʕύX������"

    ''' <summary>
    ''' ���ǍH����ʕύX������
    ''' </summary>
    ''' <param name="strHanteiSetuzokuMoji"></param>
    ''' <param name="strKjSyubetu"></param>
    ''' <param name="strKisoSiyouNo1"></param>
    ''' <param name="strKisoSiyouNo2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKairyouKoujiSyubetu( _
                                     ByVal strHanteiSetuzokuMoji As String _
                                     , ByVal strKjSyubetu As String _
                                     , ByVal strKisoSiyouNo1 As String _
                                     , Optional ByVal strKisoSiyouNo2 As String = "" _
                                     ) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKairyouKoujiSyubetu", _
                                            strHanteiSetuzokuMoji, _
                                            strKjSyubetu, _
                                            strKisoSiyouNo1, _
                                            strKisoSiyouNo2)

        '���ǍH����ʋ󔒎����邢�͔���ڑ�����=2(����)�̏ꍇ�͖��`�F�b�N
        If strKjSyubetu = "" Or strHanteiSetuzokuMoji = "2" Then
            Return False
        End If

        ' ��b�d�l�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As New KisoSiyouDataAccess()

        Return dataAccess.GetHanteiKoujiSyubetuSettei(strHanteiSetuzokuMoji, strKjSyubetu, strKisoSiyouNo1, strKisoSiyouNo2)

    End Function

#End Region

#Region "�H���R�s�[�����`�F�b�N�p"
    ''' <summary>
    ''' �H���R�s�[�����`�F�b�N�p
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <returns>�n�Ճ��R�[�h</returns>
    ''' <remarks>�n�Ճ��R�[�h�ɂ͕R�t���@�ʐ����f�[�^���i�[����Ă���܂�</remarks>
    Public Function GetKoujiCopyData(ByVal kbn, _
                                 ByVal hosyousyoNo) As KoujiCopyRecord

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanData", _
                                            kbn, _
                                            hosyousyoNo)

        ' �n�Ճf�[�^�擾�p�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' �n�Ճf�[�^�̎擾
        Dim jibanList As List(Of KoujiCopyRecord) = DataMappingHelper.Instance.getMapArray(Of KoujiCopyRecord)(GetType(KoujiCopyRecord), _
        dataAccess.GetKoujiCopyCheckData(kbn, hosyousyoNo))

        ' �n�Ճf�[�^�ێ��p�̃��R�[�h�N���X
        Dim copyRec As New KoujiCopyRecord

        If jibanList.Count > 0 Then
            copyRec = jibanList(0)
        End If

        Return copyRec

    End Function
#End Region

#Region "�w��H����Ђ̑��݃`�F�b�N"

    ''' <summary>
    ''' �w��H����Ђ̑��݃`�F�b�N
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strKoujiGaisyaCd">�H����ЃR�[�h(������ЃR�[�h�{���Ə��R�[�h)</param>
    ''' <returns>True or False</returns>
    ''' <remarks>�Y���̒�����ЃR�[�h�������X������Аݒ�}�X�^.�ɑ��݂��邩�𔻒肷��</remarks>
    Public Function ChkExistSiteiKoujiGaisya( _
                                     ByVal strKameitenCd As String _
                                     , ByVal strKoujiGaisyaCd As String _
                                     ) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkExistSiteiKoujiGaisya", _
                                            strKameitenCd _
                                            , strKoujiGaisyaCd _
                                            )

        '�H����ЃR�[�h=�����͎��̓`�F�b�N�s�v
        If strKoujiGaisyaCd = "" Then
            Return True
            Exit Function
        End If

        '������ЃR�[�h���擾
        Dim strTyousakaisyaCd As String = strKoujiGaisyaCd.Substring(0, strKoujiGaisyaCd.Length - 2)

        Dim dataAccess As New TyousakaisyaSearchDataAccess

        Return dataAccess.ExistTyousakaisyaCd(strKameitenCd, strKoujiGaisyaCd)

    End Function

#End Region

#Region "�H�����i�}�X�^���R�[�h�擾"
    ''' <summary>
    ''' �H�����i�}�X�^���R�[�h�擾
    ''' </summary>
    ''' <param name="keyRec">�����pKEY���R�[�h�N���X</param>
    ''' <param name="resultRec">���ʊi�[�p���R�[�h�N���X</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKoujiKakakuRecord(ByVal keyRec As KoujiKakakuKeyRecord, _
                                         ByRef resultRec As KoujiKakakuRecord _
                                         ) As EarthEnum.emKoujiKakaku
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKoujiKakakuRecord", _
                                                    keyRec, _
                                                    resultRec)

        '�������ʊi�[�p�f�[�^�e�[�u��
        Dim table As DataTable
        '�������ʊi�[�p���R�[�h���X�g
        Dim syouhinRec As New SyouhinMeisaiRecord

        '�p�����[�^�`�F�b�N
        If (keyRec.KameitenCd = String.Empty AndAlso keyRec.EigyousyoCd = String.Empty AndAlso keyRec.KeiretuCd = String.Empty) _
            OrElse keyRec.SyouhinCd = String.Empty _
            OrElse keyRec.KojGaisyaCd = String.Empty Then

            ' ���ݒ�̏ꍇ�A�����I��
            Return EarthEnum.emKoujiKakaku.PrmError
        End If

        '�H���^�C�v����
        table = SyouhinDataAcc.GetSyouhinInfo(keyRec.SyouhinCd)
        If table.Rows.Count > 0 Then
            ' �������ʂ��i�[�p���X�g�ɃZ�b�g
            syouhinRec = DataMappingHelper.Instance.getMapArray(Of SyouhinMeisaiRecord)(GetType(SyouhinMeisaiRecord), table)(0)
        End If

        '�H���^�C�v�����H���̏ꍇ�A�H�����i�}�X�^���獀�ڂ��擾����
        If syouhinRec.KojType = pStrTyokuKouji OrElse syouhinRec.KojType = pStrJioTyokuKouji Then

            '***************************************************************
            ' �H�����i�}�X�^���甄����z�E�����L���E�H����А����L���̎擾
            '***************************************************************

            '�u�����X�v�̍H�����i�Z�o
            If Me.GetKoujiKakakuInfo(EarthConst.AITESAKI_KAMEITEN, _
                                             keyRec.KameitenCd, _
                                             keyRec.SyouhinCd, _
                                             keyRec.KojGaisyaCd, _
                                             resultRec _
                                             ) Then
                Return EarthEnum.emKoujiKakaku.Kameiten
            End If

            '�u�c�Ə��v�̍H�����i�Z�o
            If Me.GetKoujiKakakuInfo(EarthConst.AITESAKI_EIGYOUSYO, _
                                     keyRec.EigyousyoCd, _
                                     keyRec.SyouhinCd, _
                                     keyRec.KojGaisyaCd, _
                                     resultRec _
                                     ) Then
                Return EarthEnum.emKoujiKakaku.Eigyousyo
            End If

            '�u�n��v�̍H�����i�Z�o
            If Me.GetKoujiKakakuInfo(EarthConst.AITESAKI_KEIRETU, _
                                     keyRec.KeiretuCd, _
                                     keyRec.SyouhinCd, _
                                     keyRec.KojGaisyaCd, _
                                     resultRec _
                                     ) Then
                Return EarthEnum.emKoujiKakaku.Keiretu
            End If

            '�u�w�薳�v�̍H�����i�Z�o
            If Me.GetKoujiKakakuInfo(EarthConst.AITESAKI_NASI, _
                                             EarthConst.AITESAKI_NASI_CD, _
                                             keyRec.SyouhinCd, _
                                             keyRec.KojGaisyaCd, _
                                             resultRec _
                                             ) Then
                Return EarthEnum.emKoujiKakaku.SiteiNasi
            End If

            '�H�����i�}�X�^�ɂȂ��g�ݍ��킹�́A���i�}�X�^�����ĉ��i�݂̂��擾
            If syouhinRec.Torikesi = 0 Then
                '������z�i���iM.�W�����i�j
                resultRec.UriGaku = syouhinRec.HyoujunKkk
                '�ŋ敪
                resultRec.ZeiKbn = syouhinRec.ZeiKbn
                '�ŗ�
                resultRec.Zeiritu = syouhinRec.Zeiritu

                '�H����А����L��
                If keyRec.SyouhinCd = EarthConst.SH_CD_JIO2 Then
                    resultRec.KojGaisyaSeikyuuUmu = 1
                Else
                    resultRec.KojGaisyaSeikyuuUmu = Integer.MinValue
                End If
                '�����L��
                resultRec.SeikyuuUmu = 1

                Return EarthEnum.emKoujiKakaku.TyokuKojSonota
            End If
        Else
            '���H���ȊO�́A���܂Œʂ�̎擾���@
            Return EarthEnum.emKoujiKakaku.Syouhin
        End If

        Return EarthEnum.emKoujiKakaku.Syouhin
    End Function

    ''' <summary>
    ''' �H�����i�̎擾
    ''' </summary>
    ''' <param name="intAitesakiSyubetu">�������</param>
    ''' <param name="strAitesakiCd">�����R�[�h</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="strKojGaisyaCd">�H����ЃR�[�h</param>
    '''<param name="resultRec">���ʊi�[�p���R�[�h�N���X</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKoujiKakakuInfo(ByVal intAitesakiSyubetu As Integer, _
                                       ByVal strAitesakiCd As String, _
                                       ByVal strSyouhinCd As String, _
                                       ByVal strKojGaisyaCd As String, _
                                       ByRef resultRec As KoujiKakakuRecord _
                                       ) As Boolean
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKoujiKakakuInfo", _
                                                    intAitesakiSyubetu, _
                                                    strAitesakiCd, _
                                                    strSyouhinCd, _
                                                    strKojGaisyaCd, _
                                                    resultRec _
                                                    )
        Dim dt As New DataTable

        '�H�����i�Z�o
        dt = kojDataAcc.GetKoujiKakakuInfo(intAitesakiSyubetu, _
                                         strAitesakiCd, _
                                         strSyouhinCd, _
                                         strKojGaisyaCd _
                                         )

        If dt.Rows.Count > 0 Then
            ' �������ʂ��i�[�p���X�g�ɃZ�b�g
            resultRec = DataMappingHelper.Instance.getMapArray(Of KoujiKakakuRecord)(GetType(KoujiKakakuRecord), dt)(0)
            Return True
        End If

        '�H�����i�Z�o(�H�����'ALL')
        dt = kojDataAcc.GetKoujiKakakuInfo(intAitesakiSyubetu, _
                                         strAitesakiCd, _
                                         strSyouhinCd, _
                                         pStrKojGaisyaAll _
                                         )
        If dt.Rows.Count > 0 Then
            ' �������ʂ��i�[�p���X�g�ɃZ�b�g
            resultRec = DataMappingHelper.Instance.getMapArray(Of KoujiKakakuRecord)(GetType(KoujiKakakuRecord), dt)(0)
            Return True
        End If

        Return False
    End Function

#End Region

End Class
