Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

Public Class UriageDataSearchLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'Utilities��MessegeLogic�N���X
    Dim mLogic As New MessageLogic

    'Utilities��StringLogic�N���X
    Dim sLogic As New StringLogic

#Region "����f�[�^�擾"
    ''' <summary>
    ''' ������ʗp����f�[�^���擾���܂�
    ''' </summary>
    ''' <param name="keyRec">����f�[�^Key���R�[�h</param>
    ''' <param name="startRow">�J�n�s</param>
    ''' <param name="endRow">�ŏI�s</param>
    ''' <param name="allCount">�S����</param>
    ''' <returns>����f�[�^�����p���R�[�h��List(Of UriageDataRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetUriageDataInfo(ByVal sender As Object, _
                                       ByVal keyRec As UriageDataKeyRecord, _
                                       ByVal startRow As Integer, _
                                       ByVal endRow As Integer, _
                                       ByRef allCount As Integer) As List(Of UriageSearchResultRecord)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataInfo", _
                                            keyRec, _
                                            startRow, _
                                            endRow, _
                                            allCount)

        '�������s�N���X
        Dim dataAccess As New UriageDataAccess
        '�擾�f�[�^�i�[�p���X�g
        Dim list As New List(Of UriageSearchResultRecord)

        Try
            '���������̎��s
            Dim table As DataTable = dataAccess.GetUriageDataInfo(keyRec)

            ' ���������Z�b�g
            allCount = table.Rows.Count

            ' �������ʂ��i�[�p���X�g�ɃZ�b�g
            list = DataMappingHelper.Instance.getMapArray(Of UriageSearchResultRecord)(GetType(UriageSearchResultRecord), table, startRow, endRow)

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
    ''' ����f�[�^�e�[�u���̏���PK�Ŏ擾���܂�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strDenpyouUnqNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUriageDataRec(ByVal sender As Object, ByVal strDenpyouUnqNo As String) As UriageDataRecord
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataRec", sender, strDenpyouUnqNo)

        '�f�[�^�A�N�Z�X�N���X
        Dim clsDataAcc As New UriageDataAccess
        '�������ʊi�[�p�f�[�^�e�[�u��
        Dim dTblResult As New DataTable
        '�������ʊi�[�p���R�[�h���X�g
        Dim recResult As New UriageDataRecord

        If strDenpyouUnqNo = String.Empty Then
            Return recResult
        End If

        dTblResult = clsDataAcc.GetUriageDataRec(strDenpyouUnqNo)

        If dTblResult.Rows.Count > 0 Then
            ' �������ʂ��i�[�p���X�g�ɃZ�b�g
            recResult = DataMappingHelper.Instance.getMapArray(Of UriageDataRecord)(GetType(UriageDataRecord), dTblResult)(0)
        End If
        Return recResult

    End Function

    ''' <summary>
    ''' CSV�o�͗p����f�[�^���擾���܂�
    ''' </summary>
    ''' <param name="keyRec">����f�[�^Key���R�[�h</param>
    ''' <param name="allCount">�S����</param>
    ''' <returns>����f�[�^CSV�o�͗p�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetUriageDataCsv(ByVal sender As Object, _
                                       ByVal keyRec As UriageDataKeyRecord, _
                                       ByRef allCount As Integer) As DataTable
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataCsv", _
                                            keyRec, _
                                            allCount)

        '�������s�N���X
        Dim dataAccess As New UriageDataAccess
        '�擾�f�[�^�i�[�p���X�g
        Dim list As New List(Of UriageDataRecord)

        Dim dtRet As New DataTable

        Try
            '���������̎��s
            dtRet = dataAccess.GetUriageDataCsv(keyRec)

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
#End Region

#Region "���i���擾"
    ''' <summary>
    ''' ���i�R�[�h�̏���SyouhinMeisaiRecord�N���X��List(Of SyouhinMeisaiRecord)�Ŏ擾���܂�
    ''' </summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="strSyouhinNm">���i��</param>
    ''' <param name="allCount">�������̎擾�S����</param>
    ''' <param name="startRow">�i�C�Ӂj�f�[�^���o���̊J�n�s(1���ڂ�1���w��)Default:1</param>
    ''' <param name="endRow">�i�C�Ӂj�f�[�^���o���̏I���s Default:99999999</param>
    ''' <returns>SyouhinMeisaiRecord�N���X��List(Of SyouhinMeisaiRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinInfo(ByVal strSyouhinCd As String, _
                                 ByVal strSyouhinNm As String, _
                                 ByRef allCount As Integer, _
                                 Optional ByVal startRow As Integer = 1, _
                                 Optional ByVal endRow As Integer = 99999999) As List(Of SyouhinMeisaiRecord)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinInfo", _
                                            strSyouhinCd, _
                                            strSyouhinNm, _
                                            allCount, _
                                            startRow, _
                                            endRow)

        Dim dataAccess As New SyouhinDataAccess
        Dim list As List(Of SyouhinMeisaiRecord)

        Dim table As DataTable = dataAccess.GetSyouhinInfo(strSyouhinCd, strSyouhinNm, EarthEnum.EnumSyouhinKubun.AllSyouhin)

        ' ������ݒ�
        allCount = table.Rows.Count

        ' �f�[�^���擾���AList(Of SyouhinMeisaiRecord)�Ɋi�[����
        list = DataMappingHelper.Instance.getMapArray(Of SyouhinMeisaiRecord)(GetType(SyouhinMeisaiRecord), _
                                                      table, _
                                                      startRow, _
                                                      endRow)
        Return list

    End Function
#End Region

#Region "��������擾"
    ''' <summary>
    ''' ������R�[�h�̏���SeikyuuSakiInfoRecord�N���X��List(Of SeikyuuSakiInfoRecord)�Ŏ擾���܂�
    ''' </summary>
    ''' <param name="seikyuuSakiCd">������R�[�h</param>
    ''' <param name="seikyuuSakiBrc">������}��</param>
    ''' <param name="seikyuuSakiKbn">������敪</param>
    ''' <param name="seikyuuSakiMei">�����於</param>
    ''' <param name="seikyuuSakiKana">������J�i</param>
    ''' <returns>SeikyuuSakiInfoRecord�N���X��List</returns>
    ''' <remarks>�����挟����ʗp</remarks>
    Public Function GetSeikyuuSakiInfo(ByVal seikyuuSakiCd As String, _
                                       ByVal seikyuuSakiBrc As String, _
                                       ByVal seikyuuSakiKbn As String, _
                                       ByVal seikyuuSakiMei As String, _
                                       ByVal seikyuuSakiKana As String, _
                                       ByRef allCount As Integer, _
                                       Optional ByVal startRow As Integer = 1, _
                                       Optional ByVal endRow As Integer = 99999999, _
                                       Optional ByVal blnTorikesi As Boolean = False) As List(Of SeikyuuSakiInfoRecord)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSakiInfo", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    seikyuuSakiMei, _
                                                    seikyuuSakiKana, _
                                                    allCount, _
                                                    startRow, _
                                                    endRow, _
                                                    blnTorikesi)
        Dim dataAccess As New UriageDataAccess
        Dim list As List(Of SeikyuuSakiInfoRecord)

        Dim table As DataTable = dataAccess.searchSeikyuuSakiInfo(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, seikyuuSakiMei, seikyuuSakiKana, blnTorikesi)

        ' ������ݒ�
        allCount = table.Rows.Count

        ' �f�[�^���擾���AList(Of SyouhinMeisaiRecord)�Ɋi�[����
        list = DataMappingHelper.Instance.getMapArray(Of SeikyuuSakiInfoRecord)(GetType(SeikyuuSakiInfoRecord), _
                                                      table, _
                                                      startRow, _
                                                      endRow)

        Return list

    End Function

    ''' <summary>
    ''' ������R�[�h�̏���SeikyuuSakiInfoRecord�N���X��List(Of SeikyuuSakiInfoRecord)�Ŏ擾���܂�[PK]
    ''' </summary>
    ''' <param name="seikyuuSakiCd">������R�[�h</param>
    ''' <param name="seikyuuSakiBrc">������}��</param>
    ''' <param name="seikyuuSakiKbn">������敪</param>
    ''' <param name="blnTorikesi">����t���O</param>
    ''' <returns>SeikyuuSakiInfoRecord�N���X��List</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSakiInfo(ByVal seikyuuSakiCd As String, _
                                       ByVal seikyuuSakiBrc As String, _
                                       ByVal seikyuuSakiKbn As String, _
                                       Optional ByVal blnTorikesi As Boolean = False _
                                       ) As List(Of SeikyuuSakiInfoRecord)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSakiInfo", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    blnTorikesi)

        Dim dataAccess As New UriageDataAccess
        Dim list As List(Of SeikyuuSakiInfoRecord)

        Dim table As DataTable = dataAccess.searchSeikyuuSakiInfo(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, blnTorikesi)

        ' �f�[�^���擾���AList(Of SyouhinMeisaiRecord)�Ɋi�[����
        list = DataMappingHelper.Instance.getMapArray(Of SeikyuuSakiInfoRecord)(GetType(SeikyuuSakiInfoRecord), table)

        Return list

    End Function

    ''' <summary>
    ''' ������}�X�^�̌������ʌ������擾���܂�
    ''' </summary>
    ''' <param name="seikyuuSakiCd">������R�[�h</param>
    ''' <param name="seikyuuSakiBrc">������}��</param>
    ''' <param name="seikyuuSakiKbn">������敪</param>
    ''' <param name="seikyuuSakiMei">�����於</param>
    ''' <param name="seikyuuSakiKana">������J�i</param>
    ''' <returns>������}�X�^�������ʌ���</returns>
    ''' <remarks>�����挟����ʗp</remarks>
    Public Function GetSeikyuuSakiCnt(ByVal seikyuuSakiCd As String, _
                                       ByVal seikyuuSakiBrc As String, _
                                       ByVal seikyuuSakiKbn As String, _
                                       ByVal seikyuuSakiMei As String, _
                                       ByVal seikyuuSakiKana As String, _
                                       Optional ByVal blnTorikesi As Boolean = False) As Integer
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSakiInfo", _
                                                    seikyuuSakiCd, _
                                                    seikyuuSakiBrc, _
                                                    seikyuuSakiKbn, _
                                                    seikyuuSakiMei, _
                                                    seikyuuSakiKana, _
                                                    blnTorikesi)
        Dim dataAccess As New UriageDataAccess
        Dim intCnt As Integer = dataAccess.searchSeikyuuSakiCnt(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, seikyuuSakiMei, seikyuuSakiKana, blnTorikesi)

        Return intCnt

    End Function
#End Region

#Region "�����X��������擾"
    ''' <summary>
    ''' �������Key���������X�R�[�h�Ə��i�R�[�h����擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="blnTorikesi">����t���O</param>
    ''' <returns>KameitenSeikyuuSakiInfoRecord�N���X��List</returns>
    ''' <remarks></remarks>
    Public Function GetKameitenSeikyuuSakiKey(ByVal strKameitenCd As String, _
                                       ByVal strSyouhinCd As String, _
                                       Optional ByVal blnTorikesi As Boolean = False _
                                       ) As List(Of KameitenSeikyuuSakiInfoRecord)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenSeikyuuSakiKey", _
                                                    strKameitenCd, _
                                                    strSyouhinCd, _
                                                    blnTorikesi _
                                                    )

        Dim dataAccess As New UriageDataAccess
        Dim list As List(Of KameitenSeikyuuSakiInfoRecord)

        Dim table As DataTable = dataAccess.searchKameitenSeikyuuSakiInfo(strKameitenCd, strSyouhinCd, blnTorikesi)

        ' �f�[�^���擾���AList(Of KameitenSeikyuuSakiInfoRecord)�Ɋi�[����
        list = DataMappingHelper.Instance.getMapArray(Of KameitenSeikyuuSakiInfoRecord)(GetType(KameitenSeikyuuSakiInfoRecord), table)

        Return list
    End Function

    ''' <summary>
    ''' ������̏��������X�R�[�h�Ə��i�R�[�h����擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="blnTorikesi">����t���O</param>
    ''' <returns>SeikyuuSakiInfoRecord�N���X��List</returns>
    ''' <remarks></remarks>
    Public Function GetKameitenSeikyuuSakiInfo(ByVal strKameitenCd As String, _
                                                ByVal strSyouhinCd As String, _
                                                Optional ByVal blnTorikesi As Boolean = False _
                                                ) As List(Of SeikyuuSakiInfoRecord)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenSeikyuuSakiInfo", _
                                                    strKameitenCd, _
                                                    strSyouhinCd, _
                                                    blnTorikesi _
                                                    )
        Dim recKameiten As KameitenSeikyuuSakiInfoRecord
        Dim retList As List(Of SeikyuuSakiInfoRecord)
        Dim dataAccess As New UriageDataAccess
        Dim dtResult As DataTable

        dtResult = dataAccess.searchSeikyuuSakiFromKameiten(strKameitenCd, strSyouhinCd, blnTorikesi)
        If dtResult.Rows.Count = 1 Then
            recKameiten = DataMappingHelper.Instance.getMapArray(Of KameitenSeikyuuSakiInfoRecord)(GetType(KameitenSeikyuuSakiInfoRecord), dtResult)(0)
            '��������̎擾
            retList = GetSeikyuuSakiInfo(recKameiten.SeikyuuSakiCd, recKameiten.SeikyuuSakiBrc, recKameiten.SeikyuuSakiKbn, blnTorikesi)
        Else
            retList = Nothing
        End If

        Return retList
    End Function
#End Region

#Region "����f�[�^�X�V����"
    ''' <summary>
    ''' ���ヌ�R�[�h��蔄��f�[�^�e�[�u�����X�V����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="recUri">����f�[�^���R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveUriData(ByVal sender As Object, ByVal recUri As UriageDataRecord) As Boolean
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SaveUriData", sender, recUri)

        '�r���p�f�[�^�A�N�Z�X�N���X
        Dim clsJbDataAcc As New JibanDataAccess
        Dim recResult As New UriageDataRecord '���R�[�h�N���X
        Dim updDateTime As DateTime     '�X�V����
        Dim end_count As Integer = EarthConst.MAX_RESULT_COUNT
        Dim total_count As Integer = 0
        'SQL�����������C���^�[�t�F�C�X
        Dim IFsqlMaker As ISqlStringHelper = New UpdateStringHelper '�X�V�̂�
        'SQL��
        Dim strSqlString As String = ""
        '�p�����[�^���R�[�h�̃��X�g
        Dim listParam As New List(Of ParamRecord)
        Dim recType As Type = GetType(UriageDataRecord)

        Try
            '�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' �X�V�����擾�i�V�X�e�������j
                updDateTime = DateTime.Now

                If recUri.DenUnqNo.ToString <> String.Empty Then

                    '�X�V�Ώۂ̃��R�[�h���擾
                    recResult = Me.GetUriageDataRec(sender, recUri.DenUnqNo)

                    '�r���`�F�b�N
                    If recResult.UpdDatetime <> recUri.UpdDatetime Then
                        ' �r���`�F�b�N�G���[
                        mLogic.CallHaitaErrorMessage(sender, recResult.UpdLoginUserId, recResult.UpdDatetime, "����f�[�^�e�[�u��")
                        Return False
                    End If

                    '�X�V������ݒ�
                    recUri.UpdDatetime = updDateTime
                    '�`�[����N����
                    If recUri.DenUriDate = DateTime.MinValue Then
                        recUri.DenUriDate = recResult.DenUriDate
                    End If
                    '�����N����
                    If recUri.SeikyuuDate = DateTime.MinValue Then
                        recUri.SeikyuuDate = recResult.SeikyuuDate
                    End If
                    '�`�[�ԍ�
                    If recUri.DenNo = String.Empty Then
                        recUri.DenNo = Nothing
                    End If

                    '�X�V�p������ƃp�����[�^�̍쐬
                    strSqlString = IFsqlMaker.MakeUpdateInfo(recType, recUri, listParam, GetType(UriageDataRecord))

                    'DB���f����
                    If Not clsJbDataAcc.UpdateJibanData(strSqlString, listParam) Then
                        Return False
                        Exit Function
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


End Class
