Imports System.Transactions
Imports System.Web.UI

''' <summary>
''' �@�ʓ����f�[�^�C�����W�b�N�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class TeibetuNyuukinLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

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

#Region "�f�[�^�擾"
    ''' <summary>
    ''' �n�ՂƓ@�ʐ����ɕR�Â��@�ʓ����f�[�^���擾�i�n�Ղɂ����ē@�ʐ����ɂȂ��@�ʓ����f�[�^���擾�j
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strHosyousyoNo">�ۏ؏�NO</param>
    ''' <returns>�@�ʓ������R�[�h�N���X�̃��X�g</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuNyuukinData(ByVal strKbn As String _
                                                , ByVal strHosyousyoNo As String) As List(Of TeibetuNyuukinRecord)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuNyuukinData", _
                                                   strKbn, _
                                                   strHosyousyoNo)
        Dim listNyuukinRec As List(Of TeibetuNyuukinRecord)
        Dim jDtAcc As New JibanDataAccess

        listNyuukinRec = DataMappingHelper.Instance.getMapArray(Of TeibetuNyuukinRecord)(GetType(TeibetuNyuukinRecord) _
                                                            , jDtAcc.GetTeibetuSeikyuuNyuukinData(strKbn, strHosyousyoNo))
        Return listNyuukinRec
    End Function
#End Region

#Region "�f�[�^�X�V"
    ''' <summary>
    ''' �n�Ճe�[�u���E�@�ʓ����e�[�u�����X�V���܂�
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal jibanRec As JibanRecordTeibetuNyuukin) As Boolean

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
        Dim renkeiTeibetuList As New List(Of TeibetuNyuukinRenkeiRecord)

        'Utilities��MessegeLogic�N���X
        Dim mLogic As New MessageLogic

        Try
            ' �n�Ճe�[�u���Ɠ@�ʓ����̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

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
                updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecordTeibetuNyuukin), jibanRec, list)

                ' �n�Ճe�[�u���̍X�V���s��
                If dataAccess.UpdateJibanData(updateString, list) = False Then
                    Return False
                End If

                ' �@�ʓ����f�[�^
                If Not jibanRec.TeibetuNyuukinLists Is Nothing Then

                    ' �f�[�^�ݒ�p��Dictionary�ł�
                    Dim nyuukinRecords As List(Of TeibetuNyuukinUpdateRecord) = jibanRec.TeibetuNyuukinLists
                    For Each rec As TeibetuNyuukinUpdateRecord In nyuukinRecords
                        If EditTeibetuRecord(sender _
                                            , rec.TeibetuNyuukinrecord _
                                            , rec.BunruiCd _
                                            , rec.GamenHyoujiNo _
                                            , jibanRec _
                                            , renkeiTeibetuList) = False Then
                            Return False
                        End If
                    Next
                End If


                ' �@�ʓ����A�g���f�Ώۂ����݂���ꍇ�A���f���s��
                For Each renkeiTeibetuRec As TeibetuNyuukinRenkeiRecord In renkeiTeibetuList

                    Dim renkeiDataAccess As RenkeiKanriDataAccess = New RenkeiKanriDataAccess()

                    'Utilities��MessegeLogic�N���X
                    Dim messageLogic As New MessageLogic

                    ' �A�g�f�[�^�̑��݃`�F�b�N
                    If renkeiDataAccess.SelectTeibetuNyuukinRenkeiData(renkeiTeibetuRec).Rows.Count = 0 Then
                        If renkeiDataAccess.InsertTeibetuNyuukinRenkeiData(renkeiTeibetuRec) < 1 Then
                            ' �o�^���G���[
                            messageLogic.DbErrorMessage(sender, _
                                                        "�o�^", _
                                                        "�@�ʓ����A�g", _
                                                        String.Format(EarthConst.TEIBETU_KEY2, _
                                                                     New String() {renkeiTeibetuRec.Kbn, _
                                                                                   renkeiTeibetuRec.HosyousyoNo, _
                                                                                   renkeiTeibetuRec.BunruiCd}))
                            Return False
                        End If
                    Else
                        ' ���݂��Ă�����X�V
                        If renkeiTeibetuRec.RenkeiSijiCd <> 9 Then
                            '�w���R�[�h���폜�ȊO�̏ꍇ�A�X�V�ɓ���i�V�K�f�[�^�Ή��j
                            renkeiTeibetuRec.RenkeiSijiCd = 2
                        End If
                        If renkeiDataAccess.UpdateTeibetuNyuukinRenkeiData(renkeiTeibetuRec, True) < 1 Then
                            ' �X�V���G���[
                            messageLogic.DbErrorMessage(sender, _
                                                        "�X�V", _
                                                        "�@�ʓ����A�g", _
                                                        String.Format(EarthConst.TEIBETU_KEY2, _
                                                                     New String() {renkeiTeibetuRec.Kbn, _
                                                                                   renkeiTeibetuRec.HosyousyoNo, _
                                                                                   renkeiTeibetuRec.BunruiCd}))
                            Return False
                        End If
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

    ''' <summary>
    ''' �@�ʓ����f�[�^�̓o�^�E�X�V�E�폜�����{���܂�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="teibetuRec">DB���f�Ώۂ̓@�ʓ������R�[�h</param>
    ''' <param name="bunruCd">���ރR�[�h</param>
    ''' <param name="jibanRec">�@�ʃf�[�^�p�n�Ճ��R�[�h</param>
    ''' <param name="renkeiRecList">�@�ʘA�g�e�[�u�����R�[�h�̃��X�g�i�Q�Ɠn���j</param>
    ''' <returns>�������� true:���� false:���s</returns>
    ''' <remarks></remarks>
    Private Function EditTeibetuRecord(ByVal sender As Object, _
                                       ByVal teibetuRec As TeibetuNyuukinRecord, _
                                       ByVal bunruCd As String, _
                                       ByVal gamenHyoujiNo As Integer, _
                                       ByVal jibanRec As JibanRecordTeibetuNyuukin, _
                                       ByRef renkeiRecList As List(Of TeibetuNyuukinRenkeiRecord)) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuRecord", _
                                                    sender, _
                                                    teibetuRec, _
                                                    bunruCd, _
                                                    jibanRec, _
                                                    renkeiRecList)

        ' �n�Ճf�[�^�p�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As JibanDataAccess = New JibanDataAccess
        Dim nyuukinDtAcc As New TeibetuNyuukinSyuuseiDataAccess

        ' ���݂̃��R�[�h���擾
        Dim chkTeibetuList As List(Of TeibetuNyuukinRecord) = _
                DataMappingHelper.Instance.getMapArray(Of TeibetuNyuukinRecord)( _
                GetType(TeibetuNyuukinRecord), _
                dataAccess.GetTeibetuNyuukinDataKey(jibanRec.Kbn _
                                                    , jibanRec.HosyousyoNo _
                                                    , bunruCd _
                                                    , gamenHyoujiNo))

        ' ���݃`�F�b�N�p���R�[�h�ێ�
        Dim checkRec As New TeibetuNyuukinRecord

        ' ���݃f�[�^��ێ�
        If chkTeibetuList.Count > 0 Then
            checkRec = chkTeibetuList(0)
        End If


        ' �@�ʓ����f�[�^�̓o�^�E�X�V�E�폜�����{���܂�
        If teibetuRec Is Nothing Then

            ' �폜���ꂽ���R�[�h���m�F����
            If chkTeibetuList.Count > 0 Then

                ' �@�ʓ��� �폜���{
                If EditTeibetuNyuukinData(checkRec, SqlType.SQL_DELETE) = False Then

                    'Utilities��MessegeLogic�N���X
                    Dim messageLogic As New MessageLogic

                    ' �폜���G���[
                    messageLogic.DbErrorMessage(sender, _
                                                "�폜", _
                                                "�@�ʓ���", _
                                                String.Format(EarthConst.TEIBETU_KEY, _
                                                             New String() {checkRec.Kbn, _
                                                                           checkRec.HosyousyoNo, _
                                                                           checkRec.BunruiCd, _
                                                                           checkRec.GamenHyoujiNo}))

                    ' �폜�Ɏ��s�����̂ŏ������f
                    Return False
                End If

                ' �A�g�e�[�u���ɓo�^�i�폜�|�@�ʓ����j
                Dim renkeiTeibetuRec As New TeibetuNyuukinRenkeiRecord
                renkeiTeibetuRec.Kbn = checkRec.Kbn
                renkeiTeibetuRec.HosyousyoNo = checkRec.HosyousyoNo
                renkeiTeibetuRec.BunruiCd = checkRec.BunruiCd
                renkeiTeibetuRec.GamenHyoujiNo = checkRec.GamenHyoujiNo
                renkeiTeibetuRec.RenkeiSijiCd = 9
                renkeiTeibetuRec.SousinJykyCd = 0
                renkeiTeibetuRec.UpdLoginUserId = jibanRec.UpdLoginUserId
                renkeiTeibetuRec.IsUpdate = True ' �n��S�ɗL��

                ' �X�V�p���X�g�Ɋi�[
                renkeiRecList.Add(renkeiTeibetuRec)

            End If

        Else
            If chkTeibetuList.Count > 0 Then

                ' �r���`�F�b�N
                If teibetuRec.UpdDatetime = checkRec.UpdDatetime Then
                    ' �X�V
                    If nyuukinDtAcc.UpdTeibetuNyuukin(teibetuRec) <> 1 Then

                        'Utilities��MessegeLogic�N���X
                        Dim messageLogic As New MessageLogic

                        ' �X�V���G���[
                        messageLogic.DbErrorMessage(sender, _
                                                    "�X�V", _
                                                    "�@�ʓ���", _
                                                    String.Format(EarthConst.TEIBETU_KEY, _
                                                                 New String() {teibetuRec.Kbn, _
                                                                               teibetuRec.HosyousyoNo, _
                                                                               teibetuRec.BunruiCd, _
                                                                               teibetuRec.GamenHyoujiNo}))
                        ' �X�V���s���A�����𒆒f����
                        Return False
                    End If

                    ' �A�g�p�e�[�u���ɓo�^����i�X�V�|�@�ʓ����j
                    Dim renkeiTeibetuRec As New TeibetuNyuukinRenkeiRecord
                    renkeiTeibetuRec.Kbn = teibetuRec.Kbn
                    renkeiTeibetuRec.HosyousyoNo = teibetuRec.HosyousyoNo
                    renkeiTeibetuRec.BunruiCd = teibetuRec.BunruiCd
                    renkeiTeibetuRec.GamenHyoujiNo = teibetuRec.GamenHyoujiNo
                    renkeiTeibetuRec.RenkeiSijiCd = 2
                    renkeiTeibetuRec.SousinJykyCd = 0
                    renkeiTeibetuRec.UpdLoginUserId = teibetuRec.UpdLoginUserId
                    renkeiTeibetuRec.IsUpdate = True ' �n��S�ɗL��

                    ' �X�V�p���X�g�Ɋi�[
                    renkeiRecList.Add(renkeiTeibetuRec)
                Else
                    ' �r���`�F�b�N�G���[
                    'Utilities��MessegeLogic�N���X
                    Dim messageLogic As New MessageLogic

                    ' �r���`�F�b�N�G���[
                    messageLogic.CallHaitaErrorMessage(sender, _
                                                       checkRec.UpdLoginUserId, _
                                                       checkRec.UpdDatetime, _
                                                       String.Format(EarthConst.TEIBETU_KEY, _
                                                                     New String() {checkRec.Kbn, _
                                                                                   checkRec.HosyousyoNo, _
                                                                                   checkRec.BunruiCd, _
                                                                                   checkRec.GamenHyoujiNo}))
                    Return False
                End If
            Else
                ' �����f�[�^�������̂œo�^
                If EditTeibetuNyuukinData(teibetuRec, SqlType.SQL_INSERT) = False Then

                    'Utilities��MessegeLogic�N���X
                    Dim messageLogic As New MessageLogic

                    ' �o�^���G���[
                    messageLogic.DbErrorMessage(sender, _
                                                "�o�^", _
                                                "�@�ʓ���", _
                                                String.Format(EarthConst.TEIBETU_KEY, _
                                                             New String() {teibetuRec.Kbn, _
                                                                           teibetuRec.HosyousyoNo, _
                                                                           teibetuRec.BunruiCd, _
                                                                           teibetuRec.GamenHyoujiNo}))

                    ' �o�^���s���A�����𒆒f����
                    Return False
                End If

                ' �A�g�p�e�[�u���ɓo�^����i�V�K�|�@�ʓ����j
                Dim renkeiTeibetuRec As New TeibetuNyuukinRenkeiRecord
                renkeiTeibetuRec.Kbn = teibetuRec.Kbn
                renkeiTeibetuRec.HosyousyoNo = teibetuRec.HosyousyoNo
                renkeiTeibetuRec.BunruiCd = teibetuRec.BunruiCd
                renkeiTeibetuRec.GamenHyoujiNo = teibetuRec.GamenHyoujiNo
                renkeiTeibetuRec.RenkeiSijiCd = 1
                renkeiTeibetuRec.SousinJykyCd = 0
                renkeiTeibetuRec.UpdLoginUserId = jibanRec.UpdLoginUserId
                renkeiTeibetuRec.IsUpdate = False ' �S���̐V�K

                ' �X�V�p���X�g�Ɋi�[
                renkeiRecList.Add(renkeiTeibetuRec)
            End If
        End If

        Return True
    End Function
#End Region

#Region "�@�ʓ����f�[�^�ҏW"
    ''' <summary>
    ''' �@�ʓ����f�[�^�̒ǉ��E�X�V�E�폜�����s���܂��B
    ''' </summary>
    ''' <param name="teibetuNyuukinRec">�@�ʓ������R�[�h</param>
    ''' <returns>True:�o�^���� False:�o�^���s</returns>
    ''' <remarks></remarks>
    Private Function EditTeibetuNyuukinData(ByVal teibetuNyuukinRec As TeibetuNyuukinRecord, _
                                            ByVal _sqlType As SqlType) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuNyuukinData", _
                                            teibetuNyuukinRec)

        ' SQL����������������Interface
        Dim sqlMake As ISqlStringHelper = Nothing

        ' �����ɂ��C���X�^���X�𐶐�����
        Select Case _sqlType
            Case SqlType.SQL_INSERT
                sqlMake = New InsertStringHelper

                ' Insert���͍X�V���t����o�^�Ɉړ�
                teibetuNyuukinRec.AddDatetime = Now
                teibetuNyuukinRec.AddLoginUserId = teibetuNyuukinRec.UpdLoginUserId
                teibetuNyuukinRec.UpdDatetime = DateTime.MinValue
                teibetuNyuukinRec.UpdLoginUserId = ""

            Case SqlType.SQL_UPDATE
                sqlMake = New UpdateStringHelper
            Case SqlType.SQL_DELETE
                sqlMake = New DeleteStringHelper
        End Select

        Dim sqlString As String = ""
        ' �p�����[�^�̏����i�[���� List(Of ParamRecord)
        Dim list As New List(Of ParamRecord)

        sqlString = sqlMake.MakeUpdateInfo(GetType(TeibetuNyuukinRecord), teibetuNyuukinRec, list)

        ' �n�Ճf�[�^�擾�p�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' DB���f����
        If dataAccess.UpdateJibanData(sqlString, list) = False Then
            Return False
        End If

        Return True

    End Function
#End Region

    ''' <summary>
    ''' �c�z���Čv�Z���܂�
    ''' </summary>
    ''' <param name="strSeikyuuGaku"></param>
    ''' <param name="strNyuukinGaku"></param>
    ''' <param name="strHenkinGaku"></param>
    ''' <returns>�c�z(������)</returns>
    ''' <remarks></remarks>
    Public Function CalcZanGaku(ByVal strSeikyuuGaku As String, _
                                 ByVal strNyuukinGaku As String, _
                                 ByVal strHenkinGaku As String) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CalcZangaku", _
                                                    strSeikyuuGaku, _
                                                    strNyuukinGaku, _
                                                    strHenkinGaku)
        Dim retZanGaku As String = ""

        Dim intSeikyuuGaku As Integer
        Dim intNyuukinGaku As Integer
        Dim intHenkinGaku As Integer

        strSeikyuuGaku = strSeikyuuGaku.Replace(",", "")
        strNyuukinGaku = strNyuukinGaku.Replace(",", "")
        strHenkinGaku = strHenkinGaku.Replace(",", "")

        If strSeikyuuGaku = "" Then
            intSeikyuuGaku = 0
        Else
            intSeikyuuGaku = Integer.Parse(strSeikyuuGaku)
        End If

        If strNyuukinGaku = "" Then
            intNyuukinGaku = 0
        Else
            intNyuukinGaku = Integer.Parse(strNyuukinGaku)
        End If

        If strHenkinGaku = "" Then
            intHenkinGaku = 0
        Else
            intHenkinGaku = Integer.Parse(strHenkinGaku)
        End If

        Dim intZanGaku As Integer = intSeikyuuGaku - (intNyuukinGaku - intHenkinGaku)

        retZanGaku = intZanGaku.ToString(EarthConst.FORMAT_KINGAKU_1)

        Return retZanGaku

    End Function
End Class
