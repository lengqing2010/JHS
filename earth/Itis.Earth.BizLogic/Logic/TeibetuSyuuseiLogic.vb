Imports System.Transactions
Imports System.Web.UI
Imports System.text

''' <summary>
''' �@�ʃf�[�^�C���p�̃��W�b�N�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class TeibetuSyuuseiLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'Utilities��MessegeLogic�N���X
    Dim mLogic As New MessageLogic
    Dim cbLogic As New CommonBizLogic

#Region "SQL�쐬���"
    ''' <summary>
    ''' SQL�쐬���
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum SqlType
        ''' <summary>
        ''' �o�^SQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_INSERT = 1
        ''' <summary>
        ''' �X�VSQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_UPDATE = 2
        ''' <summary>
        ''' �폜SQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_DELETE = 3
    End Enum
#End Region

#Region "�@�ʐ����e�[�u��/���i���ޔ��f"
    ''' <summary>
    ''' �@�ʐ����e�[�u��/���i���ޔ��f
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum TeiberuSyouhinType
        ''' <summary>
        ''' ���i1
        ''' </summary>
        ''' <remarks></remarks>
        SQL_INSERT = 1
        ''' <summary>
        ''' �X�VSQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_UPDATE = 2
        ''' <summary>
        ''' �폜SQL
        ''' </summary>
        ''' <remarks></remarks>
        SQL_DELETE = 3
    End Enum
#End Region

#Region "���i���̎擾"
    ''' <summary>
    ''' ���i�R�[�h�A���i�^�C�v��Key�ɏ��i�����擾���܂�
    ''' </summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="strSyouhinType">���i�^�C�v(SyouhinDataAccess.enumSyouhinKubun)</param>
    ''' <returns>���i��񃌃R�[�h</returns>
    ''' <remarks>�擾�ł��Ȃ��ꍇ��Nothing��ԋp���܂�</remarks>
    Public Function GetSyouhinInfo(ByVal strSyouhinCd As String _
                                    , ByVal strSyouhinType As EarthEnum.EnumSyouhinKubun _
                                    , Optional ByVal strKameitenCd As String = "") As SyouhinMeisaiRecord

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinInfo" _
                                                    , strSyouhinCd _
                                                    , strSyouhinType _
                                                    , strKameitenCd)

        Dim dataAccess As New SyouhinDataAccess
        Dim list As List(Of SyouhinMeisaiRecord)
        Dim table As DataTable = dataAccess.GetSyouhinInfo(strSyouhinCd, "", strSyouhinType, strKameitenCd)

        ' �f�[�^���擾���AList(Of SyouhinRecord)�Ɋi�[����
        list = DataMappingHelper.Instance.getMapArray(Of SyouhinMeisaiRecord)(GetType(SyouhinMeisaiRecord), _
                                                      table)

        ' �P���擾��ړI�Ƃ��Ă���̂Ŗ������ɂP���ڂ�ԋp
        If list.Count > 0 Then
            Return list(0)
        End If

        Return Nothing
    End Function

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

    ''' <summary>
    ''' ����f�[�^���s�ς݃`�F�b�N�f�[�^�擾
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBangou">�ԍ�</param>
    ''' <returns>String(����f�[�^���K�v�����A���݂��Ȃ��@�ʃf�[�^�L�[���(�J���}��؂�))</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuDenpyouHakkouZumiUriageData(ByVal strKbn As String, _
                                                                 ByVal strBangou As String _
                                                                ) As String
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuDenpyouHakkouZumiUriageData", _
                                                    strKbn, _
                                                    strBangou _
                                                    )

        Dim dataAccess As New UriageDataAccess
        Dim dtResult As DataTable
        Dim row As DataRow
        Dim retSB As New StringBuilder

        '�f�[�^�擾
        dtResult = dataAccess.searchTeibetuSeikyuuDenpyouHakkouZumiUriageData(strKbn, strBangou)

        '�擾�f�[�^��List�ɃZ�b�g
        If dtResult.Rows.Count >= 1 Then
            For Each row In dtResult.Rows
                retSB.Append(row(0).ToString & EarthConst.SEP_STRING)
                retSB.Append(row(1).ToString & EarthConst.SEP_STRING)
                retSB.Append(row(2).ToString & EarthConst.SEP_STRING)
                retSB.Append(row(3).ToString & ",")
            Next
        Else
            retSB.Append(String.Empty)
        End If

        '�߂�
        Return retSB.ToString
    End Function

#End Region

#Region "�f�[�^�X�V"
    ''' <summary>
    ''' �n�Ճe�[�u���E�@�ʐ����e�[�u�����X�V���܂�
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <param name="brRec">�����������R�[�h</param>
    ''' <returns>�@�ʃf�[�^�C����ʐ�p</returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal jibanRec As JibanRecordTeibetuSyuusei, _
                                  ByVal brRec As BukkenRirekiRecord, _
                                  ByVal listRec As List(Of TokubetuTaiouRecordBase) _
                                  ) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SaveJibanData", _
                                                    sender, _
                                                    jibanRec, _
                                                    brRec, _
                                                    listRec)


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

        Dim pUpdDateTime As DateTime
        '�X�V�����擾�i�V�X�e�������j
        pUpdDateTime = DateTime.Now

        Try
            ' �n�Ճe�[�u���Ɠ@�ʐ����̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                'JibanLogic�N���X
                Dim jibanLogic As New JibanLogic

                ' �r���`�F�b�N���{
                Dim haitaKekkaList As List(Of JibanHaitaRecord) = _
                            DataMappingHelper.Instance.getMapArray(Of JibanHaitaRecord)(GetType(JibanHaitaRecord), _
                            dataAccess.CheckHaita(sqlString, haitaList))

                If haitaKekkaList.Count > 0 Then
                    Dim haitaErrorData As JibanHaitaRecord = haitaKekkaList(0)

                    ' �r���`�F�b�N�G���[
                    ' �r���`�F�b�N�G���[
                    mLogic.CallHaitaErrorMessage(sender, _
                                                       haitaErrorData.UpdLoginUserId, _
                                                       haitaErrorData.UpdDatetime, _
                                                       "�n�Ճe�[�u��")

                    Return False
                End If

                '�^�M�`�F�b�N����
                Dim jibanRecOld As New JibanRecord
                jibanRecOld = jibanLogic.GetJibanData(jibanRec.Kbn, jibanRec.HosyousyoNo)

                Dim YosinLogic As New YosinLogic
                Dim intYosinResult As Integer = YosinLogic.YosinCheck(5, jibanRecOld, jibanRec)
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

                ' �X�V����
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

                ' �n�Ճf�[�^
                ' �A�g�p�e�[�u���ɓo�^����i�n�Ձ|�X�V�j
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
                updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordTeibetuSyuusei), jibanRec, list)

                ' �n�Ճe�[�u���̍X�V���s��
                If dataAccess.UpdateJibanData(updateString, list) = False Then
                    Return False
                End If

                '*************************
                '* �@�ʐ����f�[�^
                '*************************

                '��{������̃Z�b�g(���i�P�j
                setDefaultSeikyuuSaki(jibanRec.Syouhin1Record, jibanRec, jibanRecOld.Syouhin1Record)

                ' ���i�P
                If EditTeibetuRecord(sender, _
                                    jibanRec.Syouhin1Record, _
                                    EarthConst.SOUKO_CD_SYOUHIN_1, _
                                    1, _
                                    jibanRec, _
                                    renkeiTeibetuList) = False Then
                    Return False
                End If

                ' ���i�Q
                Dim i As Integer
                Dim tmpTeibetuRec As TeibetuSeikyuuRecord
                If Not jibanRec.Syouhin2Records Is Nothing Then
                    For i = 1 To EarthConst.SYOUHIN2_COUNT
                        If jibanRec.Syouhin2Records.ContainsKey(i) = True Then
                            'DB�����i�[�����@�ʐ������R�[�h�̎擾
                            If jibanRecOld.Syouhin2Records IsNot Nothing Then
                                If jibanRecOld.Syouhin2Records.ContainsKey(i) = True Then
                                    tmpTeibetuRec = jibanRecOld.Syouhin2Records.Item(i)
                                Else
                                    tmpTeibetuRec = Nothing
                                End If
                            Else
                                tmpTeibetuRec = Nothing
                            End If
                            '��{������̃Z�b�g(���i�Q�j
                            setDefaultSeikyuuSaki(jibanRec.Syouhin2Records.Item(i), jibanRec, tmpTeibetuRec)

                            ' ���i�Q�̓@�ʐ����f�[�^���X�V���܂�
                            If EditTeibetuRecord(sender, _
                                                 jibanRec.Syouhin2Records.Item(i), _
                                                 jibanRec.Syouhin2Records.Item(i).BunruiCd, _
                                                 i, _
                                                 jibanRec, _
                                                 renkeiTeibetuList) = False Then
                                Return False
                            End If
                        Else
                            ' �폜�����p(�폜�m�F�͏��i�Q�̕��ރR�[�h���ꂩ�Ŗ��Ȃ�)
                            If EditTeibetuRecord(sender, _
                                                 Nothing, _
                                                 EarthConst.SOUKO_CD_SYOUHIN_2_110, _
                                                 i, _
                                                 jibanRec, _
                                                 renkeiTeibetuList) = False Then
                                Return False
                            End If
                        End If
                    Next
                End If

                If Not jibanRec.Syouhin3Records Is Nothing Then
                    For i = 1 To EarthConst.SYOUHIN3_COUNT
                        If jibanRec.Syouhin3Records.ContainsKey(i) = True Then
                            'DB�����i�[�����@�ʐ������R�[�h�̎擾
                            If jibanRecOld.Syouhin3Records IsNot Nothing Then
                                If jibanRecOld.Syouhin3Records.ContainsKey(i) = True Then
                                    tmpTeibetuRec = jibanRecOld.Syouhin3Records.Item(i)
                                Else
                                    tmpTeibetuRec = Nothing
                                End If
                            Else
                                tmpTeibetuRec = Nothing
                            End If
                            '��{������̃Z�b�g(���i�R�j
                            setDefaultSeikyuuSaki(jibanRec.Syouhin3Records.Item(i), jibanRec, tmpTeibetuRec)

                            ' ���i�R�̓@�ʐ����f�[�^���X�V���܂�
                            If EditTeibetuRecord(sender, _
                                                jibanRec.Syouhin3Records.Item(i), _
                                                jibanRec.Syouhin3Records.Item(i).BunruiCd, _
                                                i, _
                                                jibanRec, _
                                                renkeiTeibetuList) = False Then
                                Return False
                            End If
                        Else
                            ' �폜�����p(�폜�m�F�͏��i�R�̕��ރR�[�h���ꂩ�Ŗ��Ȃ�)
                            If EditTeibetuRecord(sender, _
                                                Nothing, _
                                                EarthConst.SOUKO_CD_SYOUHIN_3, _
                                                i, _
                                                jibanRec, _
                                                renkeiTeibetuList) = False Then
                                Return False
                            End If
                        End If
                    Next
                End If

                '��{������̃Z�b�g(���ǍH���j
                setDefaultSeikyuuSaki(jibanRec.KairyouKoujiRecord, jibanRec, jibanRecOld.KairyouKoujiRecord)

                ' ���ǍH���̓@�ʐ����f�[�^���X�V���܂�
                If EditTeibetuRecord(sender, _
                                    jibanRec.KairyouKoujiRecord, _
                                    EarthConst.SOUKO_CD_KAIRYOU_KOUJI, _
                                    1, _
                                    jibanRec, _
                                    renkeiTeibetuList) = False Then
                    Return False
                End If

                '��{������̃Z�b�g(�ǉ��H���j
                setDefaultSeikyuuSaki(jibanRec.TuikaKoujiRecord, jibanRec, jibanRecOld.TuikaKoujiRecord)

                ' �ǉ��H���̓@�ʐ����f�[�^���X�V���܂�
                If EditTeibetuRecord(sender, _
                                    jibanRec.TuikaKoujiRecord, _
                                    EarthConst.SOUKO_CD_TUIKA_KOUJI, _
                                    1, _
                                    jibanRec, _
                                    renkeiTeibetuList) = False Then
                    Return False
                End If

                '��{������̃Z�b�g(�ǉ��H���j
                setDefaultSeikyuuSaki(jibanRec.TyousaHoukokusyoRecord, jibanRec, jibanRecOld.TyousaHoukokusyoRecord)

                ' �����񍐏��̓@�ʐ����f�[�^���X�V���܂�
                If EditTeibetuRecord(sender, _
                                    jibanRec.TyousaHoukokusyoRecord, _
                                    EarthConst.SOUKO_CD_TYOUSA_HOUKOKUSYO, _
                                    1, _
                                    jibanRec, _
                                    renkeiTeibetuList) = False Then
                    Return False
                End If

                '��{������̃Z�b�g(�ǉ��H���j
                setDefaultSeikyuuSaki(jibanRec.KoujiHoukokusyoRecord, jibanRec, jibanRecOld.KoujiHoukokusyoRecord)

                ' �H���񍐏��̓@�ʐ����f�[�^���X�V���܂�
                If EditTeibetuRecord(sender, _
                                    jibanRec.KoujiHoukokusyoRecord, _
                                    EarthConst.SOUKO_CD_KOUJI_HOUKOKUSYO, _
                                    1, _
                                    jibanRec, _
                                    renkeiTeibetuList) = False Then
                    Return False
                End If

                '��{������̃Z�b�g(�ۏ؏��j
                setDefaultSeikyuuSaki(jibanRec.HosyousyoRecord, jibanRec, jibanRecOld.HosyousyoRecord)

                ' �ۏ؏��̓@�ʐ����f�[�^���X�V���܂�
                If EditTeibetuRecord(sender, _
                                    jibanRec.HosyousyoRecord, _
                                    EarthConst.SOUKO_CD_HOSYOUSYO, _
                                    1, _
                                    jibanRec, _
                                    renkeiTeibetuList) = False Then
                    Return False
                End If

                '��{������̃Z�b�g(��񕥂��߂��j
                setDefaultSeikyuuSaki(jibanRec.KaiyakuHaraimodosiRecord, jibanRec, jibanRecOld.KaiyakuHaraimodosiRecord)

                ' ��񕥂��߂��̓@�ʐ����f�[�^���X�V���܂�
                If EditTeibetuRecord(sender, _
                                    jibanRec.KaiyakuHaraimodosiRecord, _
                                    EarthConst.SOUKO_CD_KAIYAKU_HARAIMODOSI, _
                                    1, _
                                    jibanRec, _
                                    renkeiTeibetuList) = False Then
                    Return False
                End If


                ' �@�ʐ����A�g���f�Ώۂ����݂���ꍇ�A���f���s��
                For Each renkeiTeibetuRec As TeibetuSeikyuuRenkeiRecord In renkeiTeibetuList
                    ' �A�g�p�e�[�u���ɔ��f����i�@�ʐ����j
                    If dataAccess.EditTeibetuRenkeiData(renkeiTeibetuRec) <> 1 Then

                        ' �o�^���G���[
                        mLogic.DbErrorMessage(sender, _
                                                    "�o�^", _
                                                    "�@�ʐ����A�g", _
                                                    String.Format(EarthConst.TEIBETU_KEY, _
                                                                 New String() {renkeiTeibetuRec.Kbn, _
                                                                               renkeiTeibetuRec.HosyousyoNo, _
                                                                               renkeiTeibetuRec.BunruiCd, _
                                                                               renkeiTeibetuRec.GamenHyoujiNo}))
                        ' �o�^�Ɏ��s�����̂ŏ������f
                        Return False
                    End If
                Next

                '���ʑΉ����i�Ή�
                If Not listRec Is Nothing Then
                    '�Ώۃf�[�^�̂ݍX�V
                    For intTokuCnt As Integer = 0 To listRec.Count - 1
                        listRec(intTokuCnt).UpdFlg = 1
                        listRec(intTokuCnt).KkkSetFlg = True
                    Next

                    '���ʑΉ��f�[�^�X�V
                    If cbLogic.pf_setKey_TokubetuTaiou_TeibetuSeikyuu(sender, listRec, jibanRec.UpdLoginUserId, EarthEnum.emGamenInfo.TeibetuSyuusei) = False Then
                        Return False
                        Exit Function
                    End If
                End If

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

                ' �o�R�̂ݒn�Ճe�[�u���̍X�V���s��
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

    ''' <summary>
    ''' �@�ʐ����f�[�^�̓o�^�E�X�V�E�폜�����{���܂�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="teibetuRec">DB���f�Ώۂ̓@�ʐ������R�[�h</param>
    ''' <param name="bunruCd">���ރR�[�h</param>
    ''' <param name="gamenHyoujiNo">��ʕ\��NO</param>
    ''' <param name="jibanRec">�@�ʃf�[�^�p�n�Ճ��R�[�h</param>
    ''' <param name="renkeiRecList">�@�ʘA�g�e�[�u�����R�[�h�̃��X�g�i�Q�Ɠn���j</param>
    ''' <param name="teibetuType">�@�ʐ���T�̃��R�[�h�^�C�v</param>
    ''' <returns>�������� true:���� false:���s</returns>
    ''' <remarks></remarks>
    Public Function EditTeibetuRecord(ByVal sender As Object, _
                                      ByVal teibetuRec As TeibetuSeikyuuRecord, _
                                      ByVal bunruCd As String, _
                                      ByVal gamenHyoujiNo As Integer, _
                                      ByVal jibanRec As JibanRecordBase, _
                                      ByRef renkeiRecList As List(Of TeibetuSeikyuuRenkeiRecord), _
                                      Optional ByVal teibetuType As Type = Nothing _
                                      ) As Boolean

        ' �n�Ճf�[�^�p�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' ���݂̃��R�[�h���擾
        Dim chkTeibetuList As List(Of TeibetuSeikyuuRecord) = _
                DataMappingHelper.Instance.getMapArray(Of TeibetuSeikyuuRecord)( _
                GetType(TeibetuSeikyuuRecord), _
                dataAccess.GetTeibetuSeikyuuData(jibanRec.Kbn, _
                                                 jibanRec.HosyousyoNo, _
                                                 bunruCd, _
                                                 gamenHyoujiNo))

        ' ���݃`�F�b�N�p���R�[�h�ێ�
        Dim checkRec As New TeibetuSeikyuuRecord

        '�o�^/�X�V�����A���O�C�����[�U�[ID���Z�b�g
        Dim newUpdateDatetime As DateTime = DateTime.Now
        Dim newUpdLoginUserId As String = jibanRec.UpdLoginUserId

        ' ���݃f�[�^��ێ�
        If chkTeibetuList.Count > 0 Then        'DB�l������ꍇ
            checkRec = chkTeibetuList(0)

            ' �r���`�F�b�N
            ' ���݂�DB�X�V(�ǉ�)�������擾
            Dim dbDate As DateTime = IIf(checkRec.UpdDatetime = DateTime.MinValue, checkRec.AddDatetime, checkRec.UpdDatetime)
            If teibetuRec IsNot Nothing Then
                If teibetuRec.UpdDatetime = dbDate Then
                Else
                    ' �r���`�F�b�N�G���[
                    mLogic.CallHaitaErrorMessage(sender, _
                                                 IIf(checkRec.UpdLoginUserId = String.Empty, checkRec.AddLoginUserId, checkRec.UpdLoginUserId), _
                                                 dbDate, _
                                                 String.Format(EarthConst.TEIBETU_KEY, _
                                                               New String() {checkRec.Kbn, _
                                                                             checkRec.HosyousyoNo, _
                                                                             checkRec.BunruiCd, _
                                                                             checkRec.GamenHyoujiNo}))
                    Return False
                End If
            End If
        End If



        ' �@�ʐ����f�[�^�̓o�^�E�X�V�E�폜�����{���܂�
        If teibetuRec Is Nothing Then

            ' �폜���ꂽ���R�[�h���m�F����
            If chkTeibetuList.Count > 0 Then

                ' �@�ʐ���DELETE
                If EditTeibetuSeikyuuData(checkRec, SqlType.SQL_DELETE, newUpdLoginUserId) = False Then

                    ' �폜���G���[
                    mLogic.DbErrorMessage(sender, _
                                                "�폜", _
                                                "�@�ʐ���", _
                                                String.Format(EarthConst.TEIBETU_KEY, _
                                                             New String() {checkRec.Kbn, _
                                                                           checkRec.HosyousyoNo, _
                                                                           checkRec.BunruiCd, _
                                                                           checkRec.GamenHyoujiNo}))

                    ' �폜�Ɏ��s�����̂ŏ������f
                    Return False
                End If

                ' �A�g�e�[�u���ɓo�^�i�폜�|�@�ʐ����j
                Dim renkeiTeibetuRec As New TeibetuSeikyuuRenkeiRecord
                renkeiTeibetuRec.Kbn = checkRec.Kbn
                renkeiTeibetuRec.HosyousyoNo = checkRec.HosyousyoNo
                renkeiTeibetuRec.BunruiCd = checkRec.BunruiCd
                renkeiTeibetuRec.GamenHyoujiNo = checkRec.GamenHyoujiNo
                renkeiTeibetuRec.RenkeiSijiCd = 9
                renkeiTeibetuRec.SousinJykyCd = 0
                renkeiTeibetuRec.UpdLoginUserId = newUpdLoginUserId
                renkeiTeibetuRec.IsUpdate = True ' �n��S�ɗL��

                ' �X�V�p���X�g�Ɋi�[
                renkeiRecList.Add(renkeiTeibetuRec)

            End If

        Else
            ' �X�Vor�ǉ�
            If chkTeibetuList.Count > 0 Then
                ' �����f�[�^���L��̂ōX�V

                '�X�V�����A�X�V���O�C�����[�U�[ID���Z�b�g
                teibetuRec.UpdDatetime = newUpdateDatetime
                teibetuRec.UpdLoginUserId = newUpdLoginUserId
                '�W���f�[�^���Z�b�g
                teibetuRec.UriKeijyouFlg = IIf(teibetuRec.UriKeijyouFlg = 1, teibetuRec.UriKeijyouFlg, 0)
                teibetuRec.IkkatuNyuukinFlg = IIf(teibetuRec.IkkatuNyuukinFlg = 1, teibetuRec.IkkatuNyuukinFlg, Integer.MinValue)
                '���`�[����N�����̃Z�b�g
                cbLogic.SetAutoDenUriSiireDate(teibetuRec, checkRec, teibetuType)

                '�@�ʐ���UPDATE
                If EditTeibetuSeikyuuData(teibetuRec, SqlType.SQL_UPDATE, Nothing, teibetuType) = False Then

                    ' �X�V���G���[
                    mLogic.DbErrorMessage(sender, _
                                                "�X�V", _
                                                "�@�ʐ���", _
                                                String.Format(EarthConst.TEIBETU_KEY, _
                                                             New String() {teibetuRec.Kbn, _
                                                                           teibetuRec.HosyousyoNo, _
                                                                           teibetuRec.BunruiCd, _
                                                                           teibetuRec.GamenHyoujiNo}))
                    ' �X�V���s���A�����𒆒f����
                    Return False
                End If

                ' �A�g�p�e�[�u���ɓo�^����i�X�V�|�@�ʐ����j
                Dim renkeiTeibetuRec As New TeibetuSeikyuuRenkeiRecord
                renkeiTeibetuRec.Kbn = teibetuRec.Kbn
                renkeiTeibetuRec.HosyousyoNo = teibetuRec.HosyousyoNo
                renkeiTeibetuRec.BunruiCd = teibetuRec.BunruiCd
                renkeiTeibetuRec.GamenHyoujiNo = teibetuRec.GamenHyoujiNo
                renkeiTeibetuRec.RenkeiSijiCd = 2
                renkeiTeibetuRec.SousinJykyCd = 0
                renkeiTeibetuRec.UpdLoginUserId = newUpdLoginUserId
                renkeiTeibetuRec.IsUpdate = True ' �n��S�ɗL��

                ' �X�V�p���X�g�Ɋi�[
                renkeiRecList.Add(renkeiTeibetuRec)

            Else
                ' �����f�[�^�������̂œo�^

                '�o�^�����A�o�^���O�C�����[�U�[ID���Z�b�g
                teibetuRec.AddDatetime = newUpdateDatetime
                teibetuRec.AddLoginUserId = newUpdLoginUserId
                '�X�V�����A�X�V���O�C�����[�U�[ID���N���A
                teibetuRec.UpdDatetime = DateTime.MinValue
                teibetuRec.UpdLoginUserId = Nothing
                '�W���f�[�^���Z�b�g
                teibetuRec.UriKeijyouFlg = IIf(teibetuRec.UriKeijyouFlg = 1, teibetuRec.UriKeijyouFlg, 0)
                teibetuRec.IkkatuNyuukinFlg = IIf(teibetuRec.IkkatuNyuukinFlg = 1, teibetuRec.IkkatuNyuukinFlg, Integer.MinValue)
                '���`�[����N�����̃Z�b�g
                cbLogic.SetAutoDenUriSiireDate(teibetuRec, checkRec, teibetuType)

                '�@�ʐ���INSERT
                If EditTeibetuSeikyuuData(teibetuRec, SqlType.SQL_INSERT, Nothing, teibetuType) = False Then

                    ' �o�^���G���[
                    mLogic.DbErrorMessage(sender, _
                                                "�o�^", _
                                                "�@�ʐ���", _
                                                String.Format(EarthConst.TEIBETU_KEY, _
                                                             New String() {teibetuRec.Kbn, _
                                                                           teibetuRec.HosyousyoNo, _
                                                                           teibetuRec.BunruiCd, _
                                                                           teibetuRec.GamenHyoujiNo}))

                    ' �o�^���s���A�����𒆒f����
                    Return False
                End If

                ' �A�g�p�e�[�u���ɓo�^����i�V�K�|�@�ʐ����j
                Dim renkeiTeibetuRec As New TeibetuSeikyuuRenkeiRecord
                renkeiTeibetuRec.Kbn = teibetuRec.Kbn
                renkeiTeibetuRec.HosyousyoNo = teibetuRec.HosyousyoNo
                renkeiTeibetuRec.BunruiCd = teibetuRec.BunruiCd
                renkeiTeibetuRec.GamenHyoujiNo = teibetuRec.GamenHyoujiNo
                renkeiTeibetuRec.RenkeiSijiCd = 1
                renkeiTeibetuRec.SousinJykyCd = 0
                renkeiTeibetuRec.UpdLoginUserId = newUpdLoginUserId
                renkeiTeibetuRec.IsUpdate = False ' �S���̐V�K

                ' �X�V�p���X�g�Ɋi�[
                renkeiRecList.Add(renkeiTeibetuRec)
            End If
        End If

        Return True
    End Function

#Region "�@�ʐ����f�[�^�ҏW"
    ''' <summary>
    ''' �@�ʐ����f�[�^�̒ǉ��E�X�V�E�폜�����s���܂��B
    ''' </summary>
    ''' <param name="teibetuSeikyuuRec">�@�ʐ������R�[�h</param>
    ''' <param name="_sqlType">SQL Type</param>
    ''' <param name="loginUserId">���O�C�����[�UID</param>
    ''' <param name="teibetuType">�@�ʐ���T�̃��R�[�h�^�C�v</param>
    ''' <returns>True:�o�^���� False:�o�^���s</returns>
    ''' <remarks></remarks>
    Private Function EditTeibetuSeikyuuData(ByVal teibetuSeikyuuRec As TeibetuSeikyuuRecord, _
                                            ByVal _sqlType As SqlType, _
                                            Optional ByVal loginUserId As String = Nothing, _
                                            Optional ByVal teibetuType As Type = Nothing _
                                            ) As Boolean
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuSeikyuuData", _
                                            teibetuSeikyuuRec, _
                                            _sqlType, _
                                            loginUserId, _
                                            teibetuType)

        ' SQL����������������Interface
        Dim sqlMake As ISqlStringHelper = Nothing
        ' SQL�i�[�p
        Dim sqlString As String = ""
        ' �p�����[�^�̏����i�[���� List(Of ParamRecord)
        Dim list As New List(Of ParamRecord)
        ' �n�Ճf�[�^�擾�p�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' �����ɂ��C���X�^���X�𐶐�����
        Select Case _sqlType
            Case SqlType.SQL_INSERT
                sqlMake = New InsertStringHelper
            Case SqlType.SQL_UPDATE
                sqlMake = New UpdateStringHelper
            Case SqlType.SQL_DELETE
                sqlMake = New DeleteStringHelper
                If loginUserId IsNot Nothing Then
                    '�폜�������́A�g���K�p���[�J���ꎞ�e�[�u���𐶐�����SQL��ǉ�
                    sqlString &= dataAccess.CreateUserInfoTempTableSQL(loginUserId)
                End If
        End Select

        ' �f�[�^�A�N�Z�X�pSQL���𐶐�
        If teibetuType Is Nothing Then
            teibetuType = GetType(TeibetuSeikyuuRecord)
        End If

        'TeibetuSeikyuuRecord�N���X�Ƃ̃}�b�s���O���s���A�N�G���𐶐�����
        sqlString &= sqlMake.MakeUpdateInfo(teibetuType, teibetuSeikyuuRec, list, GetType(TeibetuSeikyuuRecord))

        ' DB���f����
        If dataAccess.UpdateJibanData(sqlString, list) = False Then
            Return False
        End If

        Return True
    End Function
#End Region

    ''' <summary>
    ''' ��{��������ݒ菈��
    ''' </summary>
    ''' <param name="dispRec">��ʏ����i�[�����@�ʐ������R�[�h</param>
    ''' <param name="dbJrec">DB�����i�[�����n�Ճ��R�[�h</param>
    ''' <param name="dbTrec">DB�����i�[�����@�ʐ������R�[�h</param>
    ''' <remarks></remarks>
    Private Sub setDefaultSeikyuuSaki(ByRef dispRec As TeibetuSeikyuuRecord, ByVal dbJrec As JibanRecordBase, ByVal dbTrec As TeibetuSeikyuuRecord)
        Dim dicSeikyuuSaki As Dictionary(Of String, String) = Nothing   '��{������i�[Dictionary

        '�Ώۃ��R�[�h�Ȃ�
        If dispRec Is Nothing And dbTrec Is Nothing Then
            Exit Sub
        End If
        '�Ώۃ��R�[�h���폜�̏ꍇ(�폜�Ɋւ��Ă͏�w�̃��W�b�N�ŏ������珜�O����邪�O�̈�)
        If dispRec Is Nothing And dbTrec IsNot Nothing Then
            Exit Sub
        End If


        '****************************************************************
        '* �ǉ����X�V�̃p�^�[��(dispRec Isnot Nothing)�ŁA�v��ς݂̏ꍇ
        '****************************************************************
        '��ʂɐ������񂪃Z�b�g����Ă���ꍇ
        If dispRec.SeikyuuSakiCd <> String.Empty _
                Or dispRec.SeikyuuSakiBrc <> String.Empty _
                Or dispRec.SeikyuuSakiKbn <> String.Empty Then
            Exit Sub
        End If

        '�Ώۃ��R�[�h���X�V�̏ꍇ
        If dbTrec IsNot Nothing Then
            '��ʂ��v��ς݂łȂ��ꍇ
            If dispRec.UriKeijyouDate = DateTime.MinValue Or dispRec.UriKeijyouFlg <> 1 Then
                Exit Sub
            End If
            '����DB�̒l���v��ς݂̏ꍇ
            If dbTrec.UriKeijyouDate <> DateTime.MinValue Or dbTrec.UriKeijyouFlg = 1 Then
                Exit Sub
            End If
        End If

        '��ʂ��v��ς݂łȂ��ꍇ(�Ώۃ��R�[�h���ǉ��̏ꍇ)
        If dispRec.UriKeijyouDate = DateTime.MinValue Or dispRec.UriKeijyouFlg <> 1 Then
            Exit Sub
        End If

        '���v��ˌv��ςɂ����ꍇ�A��������NULL�������ꍇ�́A�f�t�H���g�̐������ݒ肷��
        '�H����Ђ̊�{��������擾
        Select Case dispRec.BunruiCd
            Case "130"
                If dbJrec.KojGaisyaSeikyuuUmu = 1 Then
                    dicSeikyuuSaki = cbLogic.getDefaultSeikyuuSaki(dbJrec.KojGaisyaCd + dbJrec.KojGaisyaJigyousyoCd)
                End If
            Case "140"
                If dbJrec.TKojKaisyaSeikyuuUmu = 1 Then
                    dicSeikyuuSaki = cbLogic.getDefaultSeikyuuSaki(dbJrec.TKojKaisyaCd + dbJrec.TKojKaisyaJigyousyoCd)
                End If
        End Select
        '�����X�����{��������擾
        If dicSeikyuuSaki Is Nothing OrElse dicSeikyuuSaki(cbLogic.dicKeySeikyuuSakiCd) = String.Empty Then
            dicSeikyuuSaki = cbLogic.getDefaultSeikyuuSaki(dbJrec.KameitenCd, dispRec.SyouhinCd)
        End If

        dispRec.SeikyuuSakiCd = dicSeikyuuSaki(cbLogic.dicKeySeikyuuSakiCd)
        dispRec.SeikyuuSakiBrc = dicSeikyuuSaki(cbLogic.dicKeySeikyuuSakiBrc)
        dispRec.SeikyuuSakiKbn = dicSeikyuuSaki(cbLogic.dicKeySeikyuuSakiKbn)

    End Sub
#End Region

#Region "��ʂ̕ύX�ӏ��擾�p"
    ''' <summary>
    ''' �@�ʏ��i�pHidden�̒l���i�[�����z���Dictionary�ɓo�^
    ''' </summary>
    ''' <param name="strItem1ChgValues">�v�Z�����pHidden�̒l���i�[�����z��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDicItem(ByVal strItem1ChgValues() As String) As Dictionary(Of String, String)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getDicItem", _
                                            strItem1ChgValues)

        Dim dic As New Dictionary(Of String, String)

        With dic
            '����ʂ̍��ڂƏ��Ԃ̓�����K����邱��!
            For intCnt As Integer = 0 To strItem1ChgValues.Length - 1
                .Add(CStr(intCnt), strItem1ChgValues(intCnt))
            Next
        End With

        Return dic
    End Function

#End Region

End Class
