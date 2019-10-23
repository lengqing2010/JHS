Imports System.text
Imports System.Transactions
Imports itis.Earth.Utilities.StringLogic

Public Class SeikyuusyoDataSakuseiLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    '���b�Z�[�W���W�b�N
    Private mLogic As New MessageLogic
    Private sLogic As New StringLogic

#Region "�v���p�e�B"
    ''' <summary>
    ''' ���������s��
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuusyoHakDate As String

    Public WriteOnly Property AccSeikyuusyoHakDate() As String
        Set(ByVal value As String)
            strSeikyuusyoHakDate = value
        End Set
    End Property

    ''' <summary>
    ''' ���O�C�����[�U�[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strLoginUserId As String
    ''' <summary>
    ''' ���O�C�����[�U�[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���O�C�����[�U�[ID</returns>
    ''' <remarks></remarks>
    Public Property LoginUserId() As String
        Get
            Return strLoginUserId
        End Get
        Set(ByVal value As String)
            strLoginUserId = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' ���������s�Ώۂ̐����N�����ŏ����ߓ����擾����
    ''' </summary>
    ''' <param name="listSsi">�w�萿���惊�X�g</param>
    ''' <param name="strResultDate">�ŏ������N����</param>
    ''' <param name="flgResult">�`�F�b�N�{�b�N�X�L��</param>
    ''' <remarks></remarks>
    Public Sub getMinSeikyuuSimeDate(ByVal listSsi As List(Of SeikyuuSakiInfoRecord), ByRef strResultDate As String, ByRef flgResult As Integer, ByRef intErrFlg As EarthEnum.emHidukeSyutokuErr)

        Dim minSimeDate As Object = Nothing
        Dim dtAcc As New SeikyuusyoDataSakuseiDataAccess

        Try
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '���������s�Ώۂ̐����N�����ŏ����ߓ����擾����
                minSimeDate = dtAcc.getMinSeikyuuSimeDate(Date.MaxValue.ToString(EarthConst.FORMAT_DATE_TIME_9), listSsi)

                '�g�����U�N�V�����X�R�[�v �R���v���[�g
                scope.Complete()

            End Using

            '���������s���擾�`�F�b�N
            If minSimeDate Is DBNull.Value OrElse minSimeDate.Length < 9 Then
                intErrFlg = EarthEnum.emHidukeSyutokuErr.SyutokuErr
            Else
                '�����N�����ƑS�Ώ�FLG��؂蕪����
                strResultDate = minSimeDate.substring(0, 4) + "/" + minSimeDate.substring(4, 2) + "/" + minSimeDate.substring(6, 2)
                flgResult = CType(minSimeDate, String).Substring(8, 1)
            End If

        Catch sqlTimeOut As System.Data.SqlClient.SqlException
            If sqlTimeOut.ErrorCode = -2146232060 _
            AndAlso sqlTimeOut.State = 0 _
            AndAlso sqlTimeOut.Number = -2 _
            AndAlso sqlTimeOut.Message.Contains(EarthConst.TIMEOUT_KEYWORD) = True Then
                UnTrappedExceptionManager.Publish(sqlTimeOut)
                intErrFlg = EarthEnum.emHidukeSyutokuErr.SqlErr
                Exit Sub
            Else
                UnTrappedExceptionManager.Publish(sqlTimeOut)
                intErrFlg = EarthEnum.emHidukeSyutokuErr.SqlErr
                Exit Sub
            End If
        Catch tranTimeOut As System.Transactions.TransactionException
            UnTrappedExceptionManager.Publish(tranTimeOut)
            intErrFlg = EarthEnum.emHidukeSyutokuErr.SqlErr
            Exit Sub
        Catch ex As Exception
            UnTrappedExceptionManager.Publish(ex)
            intErrFlg = EarthEnum.emHidukeSyutokuErr.SqlErr
            Exit Sub

        Finally

            '���t�`�F�b�N
            If strResultDate IsNot Nothing AndAlso IsDate(strResultDate) Then
            Else
                '�擾�ł��Ȃ������ꍇ�A�������t��߂�
                strResultDate = Date.Now.ToShortDateString.ToString
                '�擾�ł��Ȃ������ꍇ�A�`�F�b�N���ݒ��߂�
                flgResult = "0"
                '���t�`���G���[�t���O
                If intErrFlg = EarthEnum.emHidukeSyutokuErr.OK Then
                    intErrFlg = EarthEnum.emHidukeSyutokuErr.HidukeErr
                End If
            End If

        End Try

    End Sub

    ''' <summary>
    ''' �������f�[�^�쐬����
    ''' </summary>
    ''' <returns>�������b�Z�[�W</returns>
    ''' <remarks></remarks>
    Public Function SeikyuusyoDataSakuseiSyori(ByVal strSeikyuusyoHakDate As String, ByVal blnAllSakuseiFlg As Boolean, ByVal listSsi As List(Of SeikyuuSakiInfoRecord)) As String
        Dim strResultMsg As String = String.Empty
        Dim listResult As New List(Of Integer)
        Dim intKagamiSetCount As Integer = 0    '�����ӃZ�b�g�����擾�p
        Dim intRirekiSetCount As Integer = 0    '�������ߓ������Z�b�g�����擾�p
        Dim dtAcc As New SeikyuusyoDataSakuseiDataAccess

        Try
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '������/�������ׂւ̓o�^����
                dtAcc.createKagamiMeisai(strSeikyuusyoHakDate, blnAllSakuseiFlg, strLoginUserId, listSsi, listResult, intKagamiSetCount, intRirekiSetCount)

                '�����ӁE���ߓ�����o�^�����`�F�b�N
                If intKagamiSetCount < 1 And intRirekiSetCount < 1 Then
                    '�����ӁE���ߓ�����o�^������1�����̏ꍇ�A�Y���f�[�^���� �̃��b�Z�[�W��߂�
                    Return Messages.MSG020E
                    Exit Function
                End If

                '�g�����U�N�V�����X�R�[�v �R���v���[�g
                scope.Complete()
            End Using

        Catch sqlTimeOut As System.Data.SqlClient.SqlException
            If sqlTimeOut.ErrorCode = -2146232060 _
            AndAlso sqlTimeOut.State = 0 _
            AndAlso sqlTimeOut.Number = -2 _
            AndAlso sqlTimeOut.Message.Contains(EarthConst.TIMEOUT_KEYWORD) = True Then
                strResultMsg = Messages.MSG116E & sLogic.RemoveSpecStr(sqlTimeOut.Message)
                UnTrappedExceptionManager.AddMethodEntrance("CatchEx", sLogic.listToString(listResult))
                UnTrappedExceptionManager.Publish(sqlTimeOut)
                Return strResultMsg
                Exit Function
            Else
                strResultMsg = Messages.MSG118E & sLogic.RemoveSpecStr(sqlTimeOut.Message)
                UnTrappedExceptionManager.AddMethodEntrance("CatchEx", sLogic.listToString(listResult))
                UnTrappedExceptionManager.Publish(sqlTimeOut)
                Return strResultMsg
                Exit Function
            End If
        Catch tranTimeOut As System.Transactions.TransactionException
            strResultMsg = Messages.MSG117E & sLogic.RemoveSpecStr(tranTimeOut.Message)
            UnTrappedExceptionManager.AddMethodEntrance("CatchEx", sLogic.listToString(listResult))
            UnTrappedExceptionManager.Publish(tranTimeOut)
            Return strResultMsg
            Exit Function
        Catch ex As Exception
            strResultMsg = Messages.MSG118E & sLogic.RemoveSpecStr(ex.Message)
            UnTrappedExceptionManager.AddMethodEntrance("CatchEx", sLogic.listToString(listResult))
            UnTrappedExceptionManager.Publish(ex)
            Return strResultMsg
            Exit Function
        End Try

        '�����������b�Z�[�W��߂�
        strResultMsg = Messages.MSG018S.Replace("@PARAM1", "�������f�[�^�쐬") & intKagamiSetCount.ToString & "���̐����Ӄf�[�^��o�^���܂����B\r\n"
        strResultMsg += Messages.MSG018S.Replace("@PARAM1", "���ߓ�����o�^") & intRirekiSetCount.ToString & "���̒��ߓ������f�[�^��o�^���܂����B\r\n"
        Return strResultMsg

    End Function

End Class
