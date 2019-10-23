''' <summary>
''' ���t�}�X�^�ҏW�p���W�b�N�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class HidukeLogic

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

    ''' <summary>
    ''' ���tSave�}�X�^�̏����擾���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <returns>���tSave�}�X�^���R�[�h</returns>
    ''' <remarks></remarks>
    Public Function GetHidukeRecord(ByVal kbn As String) As HidukeSaveRecord

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHidukeRecord", _
                                                    kbn)
        Dim record As HidukeSaveRecord = Nothing

        Dim dataaccess As New HidukeSaveDataAccess

        Dim hidukeSaveList As List(Of HidukeSaveRecord) = _
                 DataMappingHelper.Instance.getMapArray(Of HidukeSaveRecord)(GetType(HidukeSaveRecord), _
                 dataaccess.GetHidukeSaveData(kbn))

        If hidukeSaveList.Count > 0 Then
            record = hidukeSaveList(0)
        End If

        Return record

    End Function

#Region "���tSave�}�X�^�X�V"
    ''' <summary>
    ''' ���tSave�}�X�^�̓o�^�E�X�V�E�폜�����{���܂�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="hidukeRec">DB���f�Ώۂ̓��tSave�}�X�^���R�[�h</param>
    ''' <returns>�������� true:���� false:���s</returns>
    ''' <remarks></remarks>
    Public Function EditHidukeSaveRecord(ByVal sender As Object, _
                                       ByVal hidukeRec As HidukeSaveRecord) As Boolean

        ' �n�Ճf�[�^�p�f�[�^�A�N�Z�X�N���X
        Dim dataaccess As New HidukeSaveDataAccess

        ' ���݂̃��R�[�h���擾
        Dim chkHidukeList As List(Of HidukeSaveRecord) = _
                DataMappingHelper.Instance.getMapArray(Of HidukeSaveRecord)( _
                GetType(HidukeSaveRecord), _
                dataaccess.GetHidukeSaveData(hidukeRec.Kbn))

        ' ���݃`�F�b�N�p���R�[�h�ێ�
        Dim checkRec As New HidukeSaveRecord

        ' ���݃f�[�^��ێ�
        If chkHidukeList.Count > 0 Then
            checkRec = chkHidukeList(0)
        End If

        ' ���tSave�}�X�^�f�[�^�̓o�^�E�X�V�E�폜�����{���܂�
        If hidukeRec Is Nothing Then

            ' �폜���ꂽ���R�[�h���m�F����
            If chkHidukeList.Count > 0 Then

                ' ���tSave�}�X�^ �폜���{
                If EditTeibetuSeikyuuData(checkRec, SqlType.SQL_DELETE) = False Then

                    'Utilities��MessegeLogic�N���X
                    Dim messageLogic As New MessageLogic

                    ' �폜���G���[
                    messageLogic.DbErrorMessage(sender, _
                                                "�폜", _
                                                "���tSave�}�X�^", _
                                                "�敪:" & checkRec.Kbn)

                    ' �폜�Ɏ��s�����̂ŏ������f
                    Return False
                End If

            End If

        Else
            If chkHidukeList.Count > 0 Then

                ' �r���`�F�b�N
                If hidukeRec.UpdDatetime = checkRec.UpdDatetime Then

                    '�X�V�����A�X�V���O�C�����[�U�[ID���Z�b�g
                    hidukeRec.UpdDatetime = DateTime.Now
                    hidukeRec.UpdLoginUserId = hidukeRec.UpdLoginUserId

                    ' �X�V
                    If EditTeibetuSeikyuuData(hidukeRec, SqlType.SQL_UPDATE) = False Then

                        'Utilities��MessegeLogic�N���X
                        Dim messageLogic As New MessageLogic

                        ' �X�V���G���[
                        messageLogic.DbErrorMessage(sender, _
                                                    "�X�V", _
                                                    "���tSave�}�X�^", _
                                                    "�敪:" & checkRec.Kbn)
                        ' �X�V���s���A�����𒆒f����
                        Return False
                    End If

                Else
                    ' �r���`�F�b�N�G���[
                    'Utilities��MessegeLogic�N���X
                    Dim messageLogic As New MessageLogic

                    ' �r���`�F�b�N�G���[
                    messageLogic.CallHaitaErrorMessage(sender, _
                                                       checkRec.UpdLoginUserId, _
                                                       checkRec.UpdDatetime, _
                                                       "�敪:" & checkRec.Kbn)
                    Return False
                End If
            Else
                '�o�^�����A�o�^���O�C�����[�U�[ID���Z�b�g
                hidukeRec.AddDatetime = DateTime.Now
                hidukeRec.AddLoginUserId = hidukeRec.UpdLoginUserId

                ' �����f�[�^�������̂œo�^
                If EditTeibetuSeikyuuData(hidukeRec, SqlType.SQL_INSERT) = False Then

                    'Utilities��MessegeLogic�N���X
                    Dim messageLogic As New MessageLogic

                    ' �o�^���G���[
                    messageLogic.DbErrorMessage(sender, _
                                                "�o�^", _
                                                "���tSave�}�X�^", _
                                                "�敪:" & checkRec.Kbn)

                    ' �o�^���s���A�����𒆒f����
                    Return False
                End If
            End If
        End If

        Return True
    End Function

#Region "���tSave�}�X�^�f�[�^�ҏW"
    ''' <summary>
    ''' ���tSave�}�X�^�f�[�^�̒ǉ��E�X�V�E�폜�����s���܂��B
    ''' </summary>
    ''' <param name="hidukeRec">���tSave�}�X�^���R�[�h</param>
    ''' <returns>True:�o�^���� False:�o�^���s</returns>
    ''' <remarks></remarks>
    Private Function EditTeibetuSeikyuuData(ByVal hidukeRec As HidukeSaveRecord, _
                                            ByVal _sqlType As SqlType) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuSeikyuuData", _
                                                    hidukeRec)

        ' SQL����������������Interface
        Dim sqlMake As ISqlStringHelper = Nothing

        ' �����ɂ��C���X�^���X�𐶐�����
        Select Case _sqlType
            Case SqlType.SQL_INSERT
                sqlMake = New InsertStringHelper
            Case SqlType.SQL_UPDATE
                sqlMake = New UpdateStringHelper
            Case SqlType.SQL_DELETE
                sqlMake = New DeleteStringHelper
        End Select

        Dim sqlString As String = ""
        ' �p�����[�^�̏����i�[���� List(Of ParamRecord)
        Dim list As New List(Of ParamRecord)

        sqlString = sqlMake.MakeUpdateInfo(GetType(HidukeSaveRecord), hidukeRec, list)

        ' �n�Ճf�[�^�擾�p�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' DB���f����
        If dataAccess.UpdateJibanData(sqlString, list) = False Then
            Return False
        End If

        Return True

    End Function
#End Region

#End Region

#Region "��ʕ\��I/O"
    ''' <summary>
    ''' ��ʕ\���p�ɃI�u�W�F�N�g�𕶎���ϊ�����
    ''' </summary>
    ''' <param name="obj">�\���Ώۂ̃f�[�^</param>
    ''' <param name="str">���ݒ莞�̏����l</param>
    ''' <returns>�\���`���̕�����</returns>
    ''' <remarks>
    ''' Decimal  : Minvalue���󔒁A����ȊO�͓��͒l��String�^�ŕԋp<br/>
    ''' Integer  : Minvalue���󔒁A����ȊO�͓��͒l��String�^�ŕԋp<br/>
    ''' DateTime : MinDateTime���󔒁A����ȊO�͓��͒l�� YYYY/MM/DD �`����String�^�ŕԋp<br/>
    ''' ��L�ȊO : ���̂܂܁B�K�X�ǉ����Ă�������
    ''' </remarks>
    Public Function GetDisplayString(ByVal obj As Object, _
                                     Optional ByVal str As String = "") As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDisplayString", _
                                                    obj, _
                                                    Str)

        ' �߂�l�ƂȂ�String�f�[�^
        Dim ret As String = str

        If obj Is Nothing Then
            ' NULL �͊�{�I�ɋ󔒂�Ԃ�
            Return ret
        ElseIf obj.GetType().ToString() = GetType(String).ToString Then
            ' String�̏ꍇ
            If obj = "" Then
                Return ret
            Else
                ret = obj.ToString()
            End If
        ElseIf obj.GetType().ToString() = GetType(Integer).ToString Then
            ' Integer�̏ꍇ
            If obj = Integer.MinValue Then
                Return ret
            Else
                Dim intData As Integer = obj
                ret = intData.ToString("###,###,###")
            End If
        ElseIf obj.GetType().ToString() = GetType(Decimal).ToString Then
            ' Decimal�̏ꍇ
            If obj = Decimal.MinValue Then
                Return ret
            Else
                Dim decData As Decimal = obj
                ret = decData.ToString("###,###,###.###")
            End If
        ElseIf obj.GetType().ToString() = GetType(DateTime).ToString Then
            ' DateTime�̏ꍇ
            If obj = DateTime.MinValue Then
                Return ret
            Else
                ret = Format(obj, "yyyy/MM/dd")
            End If
        Else
            ret = obj.ToString()
        End If

        Return ret

    End Function

    ''' <summary>
    ''' ��ʕ\���p��������w�肵���^�ɕϊ�����
    ''' </summary>
    ''' <param name="strData">�ϊ��Ώۂ̃f�[�^</param>
    ''' <param name="objChangeData">�ϊ���̌^�f�[�^�i�Q�Ɓj</param>
    ''' <returns>�ϊ�����</returns>
    ''' <remarks>
    ''' Decimal  : �󔒂�Minvalue�A����ȊO�͓��͒l��Decimal�ɕϊ�<br/>
    ''' Integer  : �󔒂�Minvalue�A����ȊO�͓��͒l��Integer�ɕϊ�<br/>
    ''' DateTime : �󔒂�Minvalue�A����ȊO�͓��͒l��DateTime�ɕϊ�<br/>
    ''' ��L�ȊO : ���̂܂܁B�K�X�ǉ����Ă�������<br/>
    ''' �ϊ��Ɏ��s�����ꍇ��False��Ԃ��A�w��^��MinValue���Z�b�g���܂�
    ''' </remarks>
    Public Function SetDisplayString(ByVal strData As String, _
                                     ByRef objChangeData As Object) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetDisplayString", _
                                                    strData, _
                                                    objChangeData)

        If objChangeData Is Nothing Then
            ' ����String
            objChangeData = strData
        End If

        If objChangeData.GetType().ToString() = GetType(Integer).ToString Then
            ' Integer�֕ϊ�
            If strData.Trim() = "" Then
                objChangeData = Integer.MinValue
                Return True
            Else
                Try
                    objChangeData = Integer.Parse(strData.Replace(",", ""))
                    Return True
                Catch ex As Exception
                    ' �ϊ��Ɏ��s�����ꍇ�AMinValue��ݒ肵�AFalse��ԋp
                    objChangeData = Integer.MinValue
                    Return False
                End Try
            End If
        ElseIf objChangeData.GetType().ToString() = GetType(Decimal).ToString Then
            ' Decimal�֕ϊ�
            If strData.Trim() = "" Then
                objChangeData = Decimal.MinValue
                Return True
            Else
                Try
                    objChangeData = Decimal.Parse(strData.Replace(",", ""))
                    Return True
                Catch ex As Exception
                    ' �ϊ��Ɏ��s�����ꍇ�AMinValue��ݒ肵�AFalse��ԋp
                    objChangeData = Decimal.MinValue
                    Return False
                End Try
            End If
        ElseIf objChangeData.GetType().ToString() = GetType(DateTime).ToString Then
            ' DateTime�֕ϊ�
            If strData.Trim() = "" Then
                objChangeData = DateTime.MinValue
                Return True
            Else
                Try
                    objChangeData = DateTime.Parse(strData)
                    Return True
                Catch ex As Exception
                    ' �ϊ��Ɏ��s�����ꍇ�AMinValue��ݒ肵�AFalse��ԋp
                    objChangeData = DateTime.MinValue
                    Return False
                End Try
            End If
        ElseIf objChangeData.GetType().ToString() = GetType(String).ToString Then
            ' String��Trim����
            objChangeData = strData.Trim()
            Return True
        End If

        ' �ϊ��ΏۈȊO�̌^�̓G���[
        objChangeData = Nothing
        Return False

    End Function
#End Region

End Class
