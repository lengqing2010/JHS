Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

Public Class SeikyuuDateIkkatuHenkouLogic
    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    '���b�Z�[�W���W�b�N
    Dim mLogic As New MessageLogic

#Region "���������s���ꊇ�X�V�������C��"
    ''' <summary>
    ''' ���������s���ꊇ�X�V�������C��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="seiCd"></param>
    ''' <param name="seiBrc"></param>
    ''' <param name="seiKbn"></param>
    ''' <param name="updDate"></param>
    ''' <param name="loginUserId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SeikyuuDateIkkatuHenkou(ByVal sender As System.Object, _
                                            ByVal seiCd As String, _
                                            ByVal seiBrc As String, _
                                            ByVal seiKbn As String, _
                                            ByVal seiDate As Date, _
                                            ByVal updDate As DateTime, _
                                            ByVal loginUserId As String) As List(Of Integer)

        Dim listResult As New List(Of Integer)
        Dim intResult As Integer = 0
        listResult.Add(intResult)

        Try

            '�e��e�[�u���Ɗe��A�g�Ǘ��e�[�u���̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                Dim dataAccess As New SeikyuuDateIkkatuHenkouDataAccess(seiCd, seiBrc, seiKbn, seiDate, updDate, loginUserId)
                Dim renkeiDataAccess As New RenkeiKanriDataAccess

                '***********************************************
                ' �ȉ��̍X�V���͉�ʑ��\�����ڏ��Ɠ������Ƃ邱��
                '***********************************************

                '1.���������s���ꊇ�X�V���@�ʐ����e�[�u��
                intResult = DataAccess.updSeikyuuDateTeibetuSeikyuu()
                listResult(0) = intResult
                listResult.Add(intResult)

                '2.���������s���ꊇ�X�V���X�ʐ����e�[�u��
                intResult = DataAccess.updSeikyuuDateTenbetuSeikyuu()
                listResult(0) = intResult
                listResult.Add(intResult)

                '3.���������s���ꊇ�X�V���X�ʏ��������e�[�u��
                intResult = DataAccess.updSeikyuuDateTenbetuSyokiSeikyuu()
                listResult(0) = intResult
                listResult.Add(intResult)

                '4.���������s���ꊇ�X�V���ėp����e�[�u��
                intResult = DataAccess.updSeikyuuDateHannyouUriage()
                listResult(0) = intResult
                listResult.Add(intResult)

                '5.����f�[�^�e�[�u���̐����N������UPDATE(���e�[�u�����폜���ꂽ�}�C�i�X�`�[��Ώ�)
                intResult = DataAccess.updateUriDataSeikyuuDate()
                listResult(0) = intResult
                listResult.Add(intResult)

                '6.����f�[�^�e�[�u���̐����N������UPDATE(�v��ςł����������쐬�̃}�C�i�X�`�[��Ώ�)
                intResult = dataAccess.updateTorikesiUriDataSeikyuuDate()
                listResult(0) = intResult
                listResult.Add(intResult)

                '7.�@�ʐ����A�g�Ǘ��e�[�u����UPDATE/INSERT
                intResult = renkeiDataAccess.setTeibetuSeikyuuRenkei(updDate, loginUserId)
                listResult(0) = intResult
                listResult.Add(intResult)

                '8.�X�ʐ����A�g�Ǘ��e�[�u����UPDATE/INSERT
                intResult = renkeiDataAccess.setTenbetuSeikyuuRenkei(updDate, loginUserId)
                listResult(0) = intResult
                listResult.Add(intResult)

                '9.�X�ʏ��������A�g�Ǘ��e�[�u����UPDATE/INSERT
                intResult = renkeiDataAccess.setTenbetuSyokiSeikyuuRenkei(updDate, loginUserId)
                listResult(0) = intResult
                listResult.Add(intResult)

                '�g�����U�N�V�����X�R�[�v�̊m��(�R�~�b�g)
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
            listResult(0) = Integer.MinValue
        Catch exTransactionException As System.Transactions.TransactionException
            '�G���[�L���b�`�F�g�����U�N�V�����G���[
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            listResult(0) = Integer.MinValue
        End Try

        '���ʖ߂�
        Return listResult

    End Function

#End Region

#Region "�ꊇ�ύX�Ώۃf�[�^�擾"
    ''' <summary>
    ''' �ꊇ�ύX�Ώۃf�[�^���擾���܂�
    ''' </summary>
    ''' <param name="seiCd">������R�[�h</param>
    ''' <param name="seiBrc">������}��</param>
    ''' <param name="seiKbn">������敪</param>
    ''' <param name="startRow">�J�n�s</param>
    ''' <param name="endRow">�ŏI�s</param>
    ''' <param name="allCount">�S����</param>
    ''' <returns>�ꊇ�ύX�Ώۃf�[�^�p���R�[�h��List(Of )</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuDateIkkatuHenkou(ByVal sender As System.Object, _
                                               ByVal seiCd As String, _
                                               ByVal seiBrc As String, _
                                               ByVal seiKbn As String, _
                                               ByVal startRow As Integer, _
                                               ByVal endRow As Integer, _
                                               ByRef allCount As Integer, _
                                               ByVal emType As EarthEnum.emIkkatuHenkouDataSearchType _
                                               ) As List(Of SeikyuuDateIkkatuHenkouRecord)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuDateIkkatuHenkou", _
                                                    seiCd, _
                                                    seiBrc, _
                                                    seiKbn, _
                                                    startRow, _
                                                    endRow, _
                                                    allCount, _
                                                    emType)

        '�������s�N���X
        Dim dataAccess As New SeikyuuDateIkkatuHenkouDataAccess(seiCd, seiBrc, seiKbn)
        '�擾�f�[�^�i�[�p���X�g
        Dim list As New List(Of SeikyuuDateIkkatuHenkouRecord)
        '�擾�f�[�^�i�[�p�f�[�^�e�[�u��
        Dim dtResult As DataTable

        Try
            '���������̎��s
            Select Case emType

                Case EarthEnum.emIkkatuHenkouDataSearchType.TeibetuSeikyuu      '�@�ʐ����e�[�u��
                    dtResult = dataAccess.GetTeibetuSeikyuuTbl

                Case EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSeikyuu      '�X�ʐ����e�[�u��
                    dtResult = dataAccess.GetTenbetuSeikyuuTbl

                Case EarthEnum.emIkkatuHenkouDataSearchType.TenbetuSyoki        '�X�ʏ��������e�[�u��
                    dtResult = dataAccess.GetTenbetuSyokiSeikyuuTbl

                Case EarthEnum.emIkkatuHenkouDataSearchType.HannyouUriage       '�ėp����e�[�u��
                    dtResult = dataAccess.GetHannyouUriageTbl

                Case EarthEnum.emIkkatuHenkouDataSearchType.UriageData          '����f�[�^�e�[�u��
                    dtResult = dataAccess.GetUriageDataTbl

                Case EarthEnum.emIkkatuHenkouDataSearchType.UriageDataTorikesiSeikyuuDate          '����f�[�^�e�[�u��
                    dtResult = dataAccess.GetUriageDataSeikyuuDateTbl

                Case Else
                    Return list
            End Select

            allCount = dtResult.Rows.Count
            list = DataMappingHelper.Instance.getMapArray(Of SeikyuuDateIkkatuHenkouRecord)(GetType(SeikyuuDateIkkatuHenkouRecord), dtResult, startRow, endRow)

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

#End Region
End Class
