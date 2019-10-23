Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' ���i4Logic�N���X
''' </summary>
''' <remarks></remarks>
Public Class Syouhin4Logic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'Utilities��MessegeLogic�N���X
    Dim mLogic As New MessageLogic

#Region "�n�Ճf�[�^�X�V"
    ''' <summary>
    ''' �n�Ճf�[�^���X�V���܂��B�֘A����@�ʐ����f�[�^�̍X�V���s���܂�
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <returns>True:�X�V���� False:�G���[�ɂ��X�V���s</returns>
    ''' <remarks></remarks>
    Public Function SaveJibanData(ByVal sender As Object, _
                                  ByVal jibanRec As JibanRecord, _
                                  ByVal recCnt As Integer) As Boolean
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
        '�X�V����
        Dim intDBCnt As Integer = 0
        'Utilities��MessegeLogic�N���X
        Dim mLogic As New MessageLogic
        'JibanLogic�N���X
        Dim jibanLogic As New JibanLogic
        '�@�ʃf�[�^�C�����W�b�N�N���X
        Dim teiseiLogic As New TeibetuSyuuseiLogic

        Try
            ' �n�Ճe�[�u���Ɠ@�ʐ����̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' �r���`�F�b�N���{
                Dim haitaKekkaList As List(Of JibanHaitaRecord) = _
                            DataMappingHelper.Instance.getMapArray(Of JibanHaitaRecord)(GetType(JibanHaitaRecord), _
                            dataAccess.CheckHaita(sqlString, haitaList))

                If haitaKekkaList.Count > 0 Then
                    Dim haitaErrorData As JibanHaitaRecord = haitaKekkaList(0)

                    ' �r���`�F�b�N�G���[
                    mLogic.CallHaitaErrorMessage(sender, _
                                                       haitaErrorData.UpdLoginUserId, _
                                                       haitaErrorData.UpdDatetime, _
                                                       "�n�Ճe�[�u��")
                    Return False
                End If


                ' �y�@�ʐ����f�[�^�z
                '���݂̉�ʕ\��NO�̍ő�l���擾
                Dim intMaxNo As Integer = Integer.Parse(dataAccess.GetTeibetuSeikyuuMaxNo(jibanRec.Kbn, _
                                                                                          jibanRec.HosyousyoNo, _
                                                                                          EarthConst.SOUKO_CD_SYOUHIN_4))
                ' ���i4
                Dim i As Integer
                Dim tmpTeibetuRec As New TeibetuSeikyuuRecord
                If Not jibanRec.Syouhin4Records Is Nothing Then
                    For i = 1 To recCnt '��ʃ��R�[�h�����[�v
                        If jibanRec.Syouhin4Records.ContainsKey(i) = True Then
                            '��Ɨp���R�[�h�N���X�Ɋi�[
                            tmpTeibetuRec = jibanRec.Syouhin4Records.Item(i)

                            '���i�R�[�h
                            If tmpTeibetuRec.SyouhinCd = String.Empty Then '������
                                tmpTeibetuRec = Nothing '�폜

                            Else '�X�Vor�ǉ�

                                '�ǉ�
                                If tmpTeibetuRec.GamenHyoujiNo = Integer.MinValue Then
                                    '��ʕ\��NO�����݂��Ȃ��i�V�K�o�^�̏ꍇ�j�͎擾�ő�l+1��V�K�̔�
                                    tmpTeibetuRec.GamenHyoujiNo = (intMaxNo + 1)
                                    '���V�K�o�^�p��+1�����Ă���
                                    intMaxNo = intMaxNo + 1
                                End If
                            End If

                            ' ���i4�̓@�ʐ����f�[�^���X�V���܂�
                            If teiseiLogic.EditTeibetuRecord(sender, _
                                                            tmpTeibetuRec, _
                                                            jibanRec.Syouhin4Records.Item(i).BunruiCd, _
                                                            jibanRec.Syouhin4Records.Item(i).GamenHyoujiNo, _
                                                            jibanRec, _
                                                            renkeiTeibetuList _
                                                            ) = False Then
                                Return False
                            End If
                        End If
                    Next
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