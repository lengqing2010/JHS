Imports System.Transactions

Public Class BukkenSintyokuJykyLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'Utilities��MessegeLogic�N���X
    Dim mLogic As New MessageLogic


    ''' <summary>
    ''' �ۏ؏��Ǘ��e�[�u���̏����擾���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <returns>�Y�����R�[�h���i�[�������X�g �^ 0���̏ꍇ��Nothing</returns>
    ''' <remarks>����������KEY�ɂ��Ď擾</remarks>
    Public Function getSearchKeyDataRec(ByVal sender As Object _
                                                , ByVal kbn As String _
                                                , ByVal hosyousyoNo As String) As HosyousyoKanriRecord

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSearchKeyDataRec", _
                                            kbn, _
                                            hosyousyoNo)

        '�f�[�^�A�N�Z�X�N���X
        Dim clsDataAcc As New BukkenSintyokuJykyDataAccess
        '�������ʊi�[�p�f�[�^�e�[�u��
        Dim dTblResult As New DataTable
        '�������ʊi�[�p���R�[�h���X�g
        Dim recResult As New HosyousyoKanriRecord

        dTblResult = clsDataAcc.getSearchTable(kbn, hosyousyoNo)

        If dTblResult.Rows.Count > 0 Then
            ' �������ʂ��i�[�p���X�g�ɃZ�b�g
            recResult = DataMappingHelper.Instance.getMapArray(Of HosyousyoKanriRecord)(GetType(HosyousyoKanriRecord), dTblResult)(0)
        End If
        Return recResult

    End Function

    ''' <summary>
    ''' �ۏ؏��Ǘ��e�[�u���ǉ�/�X�V����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="hkUpdDatetime">�ۏ؏��Ǘ��e�[�u���X�V����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function setHosyousyoKanriBukken(ByVal sender As Object _
                                                , ByVal kbn As String _
                                                , ByVal hosyousyoNo As String _
                                                , ByVal hkUpdDatetime As DateTime) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".setHosyousyoKanriBukken", _
                                                    kbn, _
                                                    hosyousyoNo, _
                                                    hkUpdDatetime)
        '�f�[�^�A�N�Z�X�N���X
        Dim clsDataAcc As New BukkenSintyokuJykyDataAccess
        Dim hkRec As New HosyousyoKanriRecord
        Dim intResult As Integer = 0

        Const ERRMSG As String = "�X�g�A�h�v���V�[�W���ُ�I��\r\n�G���[�R�[�h�F[{0}]\r\n�G���[���b�Z�[�W�F[{1}]"

        Try
            '�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                If kbn <> String.Empty OrElse Not kbn Is Nothing _
                    OrElse hosyousyoNo <> String.Empty OrElse Not hosyousyoNo Is Nothing Then

                    '�Y���̃��R�[�h������
                    hkRec = Me.getSearchKeyDataRec(sender, kbn, hosyousyoNo)

                    '�r���`�F�b�N
                    If hkUpdDatetime <> hkRec.UpdDateTime Then
                        '�r���`�F�b�N�G���[
                        mLogic.CallHaitaErrorMessage(sender, hkRec.UpdLoginUserId, hkRec.UpdDateTime, "�ۏ؏��Ǘ��e�[�u��")
                        Return False
                    End If

                    '�X�g�A�h�v���V�[�W�����ďo
                    intResult = clsDataAcc.setHosyousyoKanriData(kbn, hosyousyoNo)

                    If intResult = 0 Then
                        ' �X�V�ɐ��������ꍇ�A�g�����U�N�V�������R�~�b�g����
                        scope.Complete()
                        Return True
                    Else
                        '�������s
                        Return False
                    End If
                End If

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    '�G���[�L���b�`�F�f�b�h���b�N
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                mLogic.AlertMessage(sender, String.Format(ERRMSG, exSqlException.Number, exSqlException.Message), 0, "SqlException")
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            '�G���[�L���b�`�F�g�����U�N�V�����G���[
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' �����\������擾
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="kbn"></param>
    ''' <param name="hosyousyoNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function setNyuukinYoteiDate(ByVal sender As Object _
                                        , ByVal kbn As String _
                                        , ByVal hosyousyoNo As String _
                                        , ByVal dtHkUpdDatetime As DateTime) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".setNyuukinYoteiDate", _
                                            kbn, _
                                            hosyousyoNo)
        '�f�[�^�A�N�Z�X�N���X
        Dim clsDataAcc As New BukkenSintyokuJykyDataAccess
        Dim strData As String

        strData = clsDataAcc.setNyuukinYoteiDate(kbn, hosyousyoNo, dtHkUpdDatetime)

        Return strData
    End Function
End Class