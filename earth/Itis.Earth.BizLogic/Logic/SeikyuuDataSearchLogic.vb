Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

Public Class SeikyuuDataSearchLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'Utilities��MessegeLogic�N���X
    Dim mLogic As New MessageLogic

    'Utilities��StringLogic�N���X
    Dim sLogic As New StringLogic

    '�X�V�����ێ��p
    Dim pUpdDateTime As DateTime

#Region "�����f�[�^�擾"
    ''' <summary>
    ''' ������ʗp�����f�[�^���擾���܂�
    ''' </summary>
    ''' <param name="keyRec">�����f�[�^Key���R�[�h</param>
    ''' <param name="startRow">�J�n�s</param>
    ''' <param name="endRow">�ŏI�s</param>
    ''' <param name="allCount">�S����</param>
    ''' <param name="emType">�����f�[�^�̌����^�C�v</param>
    ''' <returns>�����f�[�^�����p���R�[�h��List(Of SeikyuuDataRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuDataInfo(ByVal sender As Object, _
                                       ByVal keyRec As SeikyuuDataKeyRecord, _
                                       ByVal startRow As Integer, _
                                       ByVal endRow As Integer, _
                                       ByRef allCount As Integer, _
                                       ByVal emType As EarthEnum.emSeikyuuSearchType _
                                       ) As List(Of SeikyuuDataRecord)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuDataInfo", _
                                            keyRec, _
                                            startRow, _
                                            endRow, _
                                            allCount, _
                                            emType _
                                            )

        '�������s�N���X
        Dim dataAccess As New SeikyuuDataAccess
        '�擾�f�[�^�i�[�p���X�g
        Dim list As New List(Of SeikyuuDataRecord)

        Try
            '���������̎��s
            Dim table As DataTable = dataAccess.GetSearchSeikyuusyoTbl(keyRec, emType)

            ' ���������Z�b�g
            allCount = table.Rows.Count

            ' �������ʂ��i�[�p���X�g�ɃZ�b�g
            list = DataMappingHelper.Instance.getMapArray(Of SeikyuuDataRecord)(GetType(SeikyuuDataRecord), table, startRow, endRow)

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' �������J�E���g��-1���Z�b�g
            allCount = -1
        End Try

        Return list

    End Function

    ''' <summary>
    ''' �Y���e�[�u���̏���PK�Ŏ擾���܂�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strSeikyuusyoNo">������NO</param>
    ''' <returns>DB�X�V�p���R�[�h���i�[�������X�g �^ 0���̏ꍇ��Nothing</returns>
    ''' <remarks>PK�ŊY���e�[�u����1���R�[�h���擾</remarks>
    Public Function GetSeikyuuDataRec(ByVal sender As Object _
                                                , ByVal strSeikyuusyoNo As String _
                                                ) As SeikyuuDataRecord

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuDataRec", sender, strSeikyuusyoNo)

        '�f�[�^�A�N�Z�X�N���X
        Dim clsDataAcc As New SeikyuuDataAccess
        '�������ʊi�[�p�f�[�^�e�[�u��
        Dim dTblResult As New DataTable
        '�������ʊi�[�p���R�[�h���X�g
        Dim recResult As New SeikyuuDataRecord

        If strSeikyuusyoNo = String.Empty Then
            Return recResult
        End If

        dTblResult = clsDataAcc.GetSeikyuusyoSyuuseiRec(strSeikyuusyoNo)

        If dTblResult.Rows.Count > 0 Then
            ' �������ʂ��i�[�p���X�g�ɃZ�b�g
            recResult = DataMappingHelper.Instance.getMapArray(Of SeikyuuDataRecord)(GetType(SeikyuuDataRecord), dTblResult)(0)
        End If
        Return recResult
    End Function

    ''' <summary>
    ''' CSV�o�͗p�����f�[�^���擾���܂�
    ''' </summary>
    ''' <param name="keyRec">�����f�[�^Key���R�[�h</param>
    ''' <param name="allCount">�S����</param>
    ''' <param name="emType">�����f�[�^�̌����^�C�v</param>
    ''' <returns>�����f�[�^CSV�o�͗p�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuusyoDataCsv(ByVal sender As Object, _
                                       ByVal keyRec As SeikyuuDataKeyRecord, _
                                       ByRef allCount As Integer, _
                                       ByVal emType As EarthEnum.emSeikyuuSearchType _
                                       ) As DataTable
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuusyoDataCsv", _
                                            keyRec, _
                                            allCount, _
                                            emType)

        '�������s�N���X
        Dim dataAccess As New SeikyuuDataAccess
        '�擾�f�[�^�i�[�p���X�g
        Dim list As New List(Of SeikyuuDataRecord)

        Dim dtRet As New DataTable

        Try
            '���������̎��s
            dtRet = dataAccess.GetSearchSeikyuuDataCsv(keyRec, emType)

            ' ���������Z�b�g
            allCount = dtRet.Rows.Count

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' �������J�E���g��-1���Z�b�g
            allCount = -1
        End Try

        Return dtRet
    End Function

    ''' <summary>
    ''' �����ӂɕR�t���������׃f�[�^�̖����s�������擾���܂�
    ''' ��������T.���������=NULL�ł���������T.�󎚑Ώۃt���O=1
    ''' </summary>
    ''' <param name="strAllCount">�����s�f�[�^�̑�����</param>
    ''' <remarks></remarks>
    Public Sub GetMihakkouCnt(ByVal sender As Object, ByRef strAllCount As String)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetMihakkouCnt", sender, strAllCount)

        '�������s�N���X
        Dim dataAccess As New SeikyuuDataAccess

        Try
            '���������̎��s
            Dim table As DataTable = dataAccess.GetMihakkouCnt()

            ' �擾�ł��Ȃ������ꍇ�͋󔒂�ԋp
            If table.Rows.Count > 0 Then
                Dim row As DataRow = table.Rows(0)
                ' ���������Z�b�g
                strAllCount = row("mihakkou_cnt").ToString()
            Else
                strAllCount = "0"
            End If

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' �������J�E���g��""(��)���Z�b�g
            strAllCount = String.Empty
        End Try
    End Sub

    ''' <summary>
    ''' �����ӂɕR�t���������ׂ̒��ŏd�����Ă���`�[�̌������擾���܂�
    ''' </summary>
    ''' <param name="strSeikyuusyoNo">������No</param>
    ''' <returns>�������׎擾����</returns>
    ''' <remarks></remarks>
    Public Function GetDenpyouExistsCnt(ByVal sender As Object, ByVal strSeikyuusyoNo As String) As Integer
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDenpyouExistsCnt", strSeikyuusyoNo)

        '�擾����
        Dim intCnt As Integer = 0
        '�������s�N���X
        Dim dataAccess As New SeikyuuDataAccess
        '�擾�f�[�^�i�[�p�e�[�u��
        Dim dtRet As New DataTable

        Try
            '���������̎��s
            dtRet = dataAccess.GetDenpyouExistsCnt(strSeikyuusyoNo)

            ' ���������Z�b�g
            intCnt = dtRet.Rows.Count

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' �������J�E���g��-1���Z�b�g
            intCnt = -1
        End Try

        Return intCnt
    End Function

    ''' <summary>
    ''' �������ׂ̓`�[���j�[�NNO�ɕR�t���A�ŐV�̐����Ӄ��R�[�h�̐�����NO���擾���܂�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strDenUnqNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuusyoKagamiNo(ByVal sender As Object, ByVal strDenUnqNo As String) As String
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuusyoKagamiNo", strDenUnqNo)

        '������NO
        Dim strSeikyuusyoNo As String = String.Empty
        '�������s�N���X
        Dim clsDataAcc As New SeikyuuDataAccess
        '�������ʊi�[�p�f�[�^�e�[�u��
        Dim dtResult As New DataTable
        '�������ʊi�[�p���R�[�h
        Dim recResult As New SeikyuuDataRecord

        Try
            dtResult = clsDataAcc.GetSeikyuusyoMaxRec(strDenUnqNo)

            If dtResult.Rows.Count > 0 Then
                ' �������ʂ��i�[�p���X�g�ɃZ�b�g
                recResult = DataMappingHelper.Instance.getMapArray(Of SeikyuuDataRecord)(GetType(SeikyuuDataRecord), dtResult)(0)
                ' ������NO���擾
                strSeikyuusyoNo = recResult.SeikyuusyoNo
            End If

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
        End Try

        Return strSeikyuusyoNo
    End Function

#End Region

#Region "�����f�[�^�X�V"

    ''' <summary>
    ''' �f�[�^�X�V����
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="listData">�����f�[�^�̃��X�g</param>
    ''' <param name="emType">�����f�[�^�̍X�V�^�C�v</param>
    ''' <returns>��������(Boolean)</returns>
    ''' <remarks>�f�[�^�X�V���f�p�񋓑̂̒l�ŁA�����𕪊򂷂�</remarks>
    Public Function saveData(ByVal sender As Object, ByRef listData As List(Of SeikyuuDataRecord), ByVal emType As EarthEnum.emSeikyuusyoUpdType) As Boolean
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".saveData", _
                                            sender, _
                                            listData, _
                                            emType)

        '�r���p�f�[�^�A�N�Z�X�N���X
        Dim clsJbDataAcc As New JibanDataAccess
        Dim recResult As New SeikyuuDataRecord '���R�[�h�N���X
        Dim recTmp As New SeikyuuDataRecord '��Ɨp
        'SQL�����������C���^�[�t�F�C�X
        Dim IFsqlMaker As ISqlStringHelper = New UpdateStringHelper '�X�V�̂�
        'SQL��
        Dim strSqlString As String = ""
        '�p�����[�^���R�[�h�̃��X�g
        Dim listParam As New List(Of ParamRecord)

        Dim recType As Type = Nothing
        '���R�[�h�X�V�^�C�v��ݒ�
        If emType = EarthEnum.emSeikyuusyoUpdType.SeikyuusyoTorikesi Then '���
            recType = GetType(SeikyuuDataRecord)
        ElseIf emType = EarthEnum.emSeikyuusyoUpdType.SeikyuusyoPrint Then '���������
            recType = GetType(SeikyuuDataHakkouRecord)
        ElseIf emType = EarthEnum.emSeikyuusyoUpdType.SeikyuusyoSyuusei Then '�������C��
            recType = GetType(SeikyuuDataSyuuseiRecord)
        Else
            Return False
            Exit Function
        End If

        Try
            '�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' �X�V�����擾�i�V�X�e�������j
                pUpdDateTime = DateTime.Now

                For Each recTmp In listData

                    If recTmp.SeikyuusyoNo <> String.Empty Then 'UPDATE

                        '�X�V�Ώۂ̃��R�[�h���擾
                        recResult = Me.GetSeikyuuDataRec(sender, recTmp.SeikyuusyoNo)

                        '���r���`�F�b�N
                        If recResult.UpdDatetime <> recTmp.UpdDatetime Then
                            ' �r���`�F�b�N�G���[
                            mLogic.CallHaitaErrorMessage(sender, recResult.UpdLoginUserId, recResult.UpdDatetime, "�����Ӄe�[�u��")
                            Return False
                        End If

                        '���������
                        If emType = EarthEnum.emSeikyuusyoUpdType.SeikyuusyoPrint Then
                            '�������������
                            If recResult.SeikyuusyoInsatuDate = DateTime.MinValue Then
                            Else
                                '�����������<>NULL�Ő���������͍s��Ȃ����߁A�������Ƃ΂�
                                Continue For
                            End If

                            '�����
                            If recResult.Torikesi <> 0 Then
                                '����Ő���������͍s��Ȃ����߁A�������Ƃ΂�
                                Continue For
                            End If

                            '���������p���ėp�R�[�h
                            If recResult.PrintTaisyougaiFlg = 1 Then
                                '����ΏۊO�Ő���������͍s��Ȃ����߁A�������Ƃ΂�
                                Continue For
                            End If

                            '���������p��
                            If recResult.KaisyuuSeikyuusyoYousi Is Nothing OrElse recResult.KaisyuuSeikyuusyoYousi = String.Empty Then
                                '�����������ݒ�Ő���������͍s��Ȃ����߁A�������Ƃ΂�
                                Continue For
                            End If

                        End If

                        '�X�V������ݒ�
                        recTmp.UpdDatetime = pUpdDateTime

                        '�X�V�p������ƃp�����[�^�̍쐬
                        strSqlString = IFsqlMaker.MakeUpdateInfo(recType, recTmp, listParam, GetType(SeikyuuDataRecord))

                        'DB���f����
                        If Not clsJbDataAcc.UpdateJibanData(strSqlString, listParam) Then
                            Return False
                            Exit Function
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
    ''' �����Ӄ��R�[�h�̎��������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strSeikyuusyoNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdKagamiTorikesi(ByVal sender As System.Object, ByVal strSeikyuusyoNo As String, ByVal strLoginUserId As String) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdKagamiTorikesi", sender, strSeikyuusyoNo, strLoginUserId)

        Dim strResultMsg As String = String.Empty
        Dim blnUpdResult As Boolean = False
        Dim dtAcc As New SeikyuuDataAccess

        Try
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                blnUpdResult = dtAcc.UpdKagamiTorikesi(strSeikyuusyoNo, strLoginUserId)

                If blnUpdResult = False Then
                    Return Messages.MSG147E.Replace("@PARAM1", "���������")
                End If

                '�g�����U�N�V�����X�R�[�v �R���v���[�g
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

        Return String.Empty
    End Function

#End Region

End Class
